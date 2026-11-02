const express = require("express");
const router = express.Router();
const sql = require("mssql");
const { validateBackendDates } = require("../utils/validateReportDates");
const { formatDateDisp, formatColumnHeader } = require("../utils/reportHtmlTemplate");

// Active stock take session state in memory
let currentSession = {
  isPrepared: false,
  categoryCode: "",
  userRole: "",
  userName: "",
};

// Helper function to fetch items belonging to a specific tray
async function fetchTrayItems(pool, trayId) {
  const req = pool.request();
  req.input("trayId", sql.VarChar, trayId.trim());
  let query = `
    SELECT
      RTRIM(TrayID) AS TrayID,
      RTRIM(InventoryCode) AS InventoryCode,
      RTRIM(CatalogueCode) AS CatalogueCode,
      RTRIM(Description) AS Description,
      Weight,
      CostPerGram,
      TotalCost,
      RTRIM(CategoryCode) AS CategoryCode,
      PCS
    FROM tblStock
    WHERE UPPER(RTRIM(TrayID)) = UPPER(RTRIM(@trayId))
  `;

  query += ` ORDER BY ModifyDate DESC, InventoryCode ASC`;
  const res = await req.query(query);

  return (res.recordset || []).map((row) => {
    const weight = row.Weight != null ? Number(row.Weight) || 0 : 0;
    const totalCost = row.TotalCost != null ? Number(row.TotalCost) || 0 : 0;
    const costPerGram =
      row.CostPerGram != null && Number(row.CostPerGram) > 0
        ? Number(row.CostPerGram)
        : weight > 0
          ? totalCost / weight
          : 0;

    return {
      trayId: row.TrayID ? row.TrayID.trim() : "",
      inventoryCode: row.InventoryCode ? row.InventoryCode.trim() : "",
      catalogueCode: row.CatalogueCode ? row.CatalogueCode.trim() : "",
      description: row.Description ? row.Description.trim() : "",
      weight,
      costPerGram,
      totalCost,
      categoryCode: row.CategoryCode ? row.CategoryCode.trim() : "",
      pcs: row.PCS != null ? Number(row.PCS) || 0 : 1,
      scanned: true,
    };
  });
}

// Helper function to calculate tray totals (matching legacy VB.NET Stock Take 3 summary cards)
async function fetchTrayTotals(pool, trayId = '') {
  const trimmedTray = trayId ? String(trayId).trim() : '';

  // 1. TOTAL: Complete stock from tblStock (Legacy requirement: No Category filter)
  const totalRes = await pool.request().query(`
    SELECT 
      SUM(ISNULL(Weight, 0)) AS TotalWeight,
      SUM(ISNULL(PCS, 0)) AS TotalPcs
    FROM tblStock
  `);
  const totRow = totalRes.recordset[0] || {};
  const totalWeightNum = Number(totRow.TotalWeight) || 0;
  const totalPcsNum = Number(totRow.TotalPcs) || 0;

  // 2. SCANNED: Stock belonging to the currently selected Tray ID or overall scanned items
  let scannedWeightNum = 0;
  let scannedPcsNum = 0;

  if (trimmedTray) {
    const scannedReq = pool.request();
    scannedReq.input('trayId', sql.VarChar, trimmedTray);
    let scannedQuery = `
      SELECT 
        SUM(ISNULL(Weight, 0)) AS ScannedWeight,
        SUM(ISNULL(PCS, 0)) AS ScannedPcs
      FROM tblStock
      WHERE UPPER(RTRIM(TrayID)) = UPPER(RTRIM(@trayId))
    `;
    const scannedRes = await scannedReq.query(scannedQuery);
    const scRow = scannedRes.recordset[0] || {};
    scannedWeightNum = Number(scRow.ScannedWeight) || 0;
    scannedPcsNum = Number(scRow.ScannedPcs) || 0;
  }

  // 3. UNSCANNED: TOTAL - SCANNED
  const unscannedWeightNum = Math.max(0, totalWeightNum - scannedWeightNum);
  const unscannedPcsNum = Math.max(0, totalPcsNum - scannedPcsNum);

  return {
    totalWeight: totalWeightNum.toFixed(2),
    totalPcs: totalPcsNum,
    scannedWeight: scannedWeightNum.toFixed(2),
    scannedPcs: scannedPcsNum,
    unscannedWeight: unscannedWeightNum.toFixed(2),
    unscannedPcs: unscannedPcsNum,
  };
}

// 1. GET /api/stocktake/categories
// Fetch category list dynamically from tblCategory
router.get("/categories", async (req, res) => {
  try {
    const pool = req.pool;
    const query = `
      SELECT RTRIM(CategoryCode) AS CategoryCode, RTRIM(CategoryName) AS CategoryName 
      FROM tblCategory 
      ORDER BY CategoryCode
    `;
    const result = await pool.request().query(query);

    const categories = (result.recordset || []).map((c) => ({
      categoryCode: c.CategoryCode != null ? c.CategoryCode.trim() : "",
      categoryName: c.CategoryName != null ? c.CategoryName.trim() : "",
    }));

    res.json({
      success: true,
      categories,
    });
  } catch (err) {
    console.error("Stock Take Categories Error:", err);
    res.status(500).json({
      success: false,
      message: "Failed to fetch categories from database.",
      error: err.message,
    });
  }
});

// 2. POST /api/stocktake/validate-password
// Validate entered password dynamically against tblSalesMan
router.post("/validate-password", async (req, res) => {
  try {
    const { password } = req.body;
    if (!password || !String(password).trim()) {
      return res.status(400).json({ success: false, message: "Password is required." });
    }

    const pool = req.pool;
    const trimmedPass = String(password).trim();

    const salesmanQuery = `
      SELECT TOP 1 
          RTRIM(SalesManCode) AS SalesManCode, 
          RTRIM(SalesManName) AS SalesManName 
      FROM tblSalesMan 
      WHERE LTRIM(RTRIM(CONVERT(VARCHAR(255), Password))) = @password
    `;

    const request = pool.request();
    request.input("password", sql.VarChar, trimmedPass);
    const result = await request.query(salesmanQuery);

    if (result.recordset && result.recordset.length > 0) {
      const sm = result.recordset[0];
      const salesManCode = sm.SalesManCode ? sm.SalesManCode.trim() : "";
      const salesManName = sm.SalesManName ? sm.SalesManName.trim() : salesManCode;

      return res.json({
        success: true,
        salesManCode,
        salesManName
      });
    }

    return res.status(401).json({
      success: false,
      message: "Invalid Stock Take Password."
    });
  } catch (err) {
    console.error("Stock Take Password Validation Error:", err);
    res.status(500).json({
      success: false,
      message: "Password validation failed.",
      error: err.message
    });
  }
});

// Helper function to fetch real-time summary and scanned item list directly from tblStock in SQL Server
async function fetchDatabaseState(pool, categoryCode = 'ALL', trayId = '') {
  const isAll = !categoryCode || categoryCode.trim().toUpperCase() === 'ALL';
  const cat = isAll ? '' : categoryCode.trim();

  // 1. Calculate database summary totals using fetchTrayTotals (consistent 3-card logic)
  const summary = await fetchTrayTotals(pool, trayId);

  // 2. Fetch list of scanned items (TrayID IS NOT NULL AND TrayID <> 'X')
  const listReq = pool.request();
  listReq.input('categoryCode', sql.VarChar, cat);
  const listRes = await listReq.query(`
    SELECT 
      RTRIM(tblStock.TrayID) AS TrayID,
      RTRIM(tblStock.InventoryCode) AS InventoryCode,
      RTRIM(tblStock.CatalogueCode) AS CatalogueCode,
      RTRIM(tblStock.Description) AS Description,
      tblStock.Weight,
      tblStock.CostPerGram,
      tblStock.TotalCost,
      RTRIM(tblStock.CategoryCode) AS CategoryCode,
      tblStock.PCS
    FROM tblStock
    WHERE tblStock.TrayID IS NOT NULL 
      AND RTRIM(tblStock.TrayID) <> '' 
      AND RTRIM(tblStock.TrayID) <> 'X'
      AND (@categoryCode = '' OR RTRIM(tblStock.CategoryCode) = RTRIM(@categoryCode))
    ORDER BY tblStock.ModifyDate DESC, tblStock.InventoryCode ASC
  `);

  const scannedList = (listRes.recordset || []).map((row) => {
    const weight = row.Weight != null ? Number(row.Weight) || 0 : 0;
    const totalCost = row.TotalCost != null ? Number(row.TotalCost) || 0 : 0;
    const costPerGram = row.CostPerGram != null && Number(row.CostPerGram) > 0
      ? Number(row.CostPerGram)
      : (weight > 0 ? totalCost / weight : 0);

    return {
      trayId: row.TrayID ? row.TrayID.trim() : '',
      inventoryCode: row.InventoryCode ? row.InventoryCode.trim() : '',
      catalogueCode: row.CatalogueCode ? row.CatalogueCode.trim() : '',
      description: row.Description ? row.Description.trim() : '',
      weight,
      costPerGram,
      totalCost,
      categoryCode: row.CategoryCode ? row.CategoryCode.trim() : '',
      pcs: row.PCS != null ? Number(row.PCS) || 0 : 1,
      scanned: true,
    };
  });

  return { summary, scannedList };
}

// 3. POST /api/stocktake/proceed
// Initialize Stock Take preparation flow & summary totals dynamically from database
router.post("/proceed", async (req, res) => {
  try {
    const { password, categoryCode, forceNew } = req.body;

    if (!password || !String(password).trim()) {
      return res.status(400).json({ success: false, message: "Password is required." });
    }

    const pool = req.pool;
    const trimmedPass = String(password).trim();

    // Authenticate user
    const salesmanQuery = `
      SELECT TOP 1 
          RTRIM(SalesManCode) AS SalesManCode, 
          RTRIM(SalesManName) AS SalesManName 
      FROM tblSalesMan 
      WHERE LTRIM(RTRIM(CONVERT(VARCHAR(255), Password))) = @password
    `;
    const salesmanReq = pool.request();
    salesmanReq.input("password", sql.VarChar, trimmedPass);
    const authRes = await salesmanReq.query(salesmanQuery);

    if (!authRes.recordset || authRes.recordset.length === 0) {
      return res.status(401).json({
        success: false,
        message: "Invalid Stock Take Password."
      });
    }

    const sm = authRes.recordset[0];
    const userRole = sm.SalesManName ? sm.SalesManName.trim() : (sm.SalesManCode ? sm.SalesManCode.trim() : "");
    const userName = userRole;

    const isAll = !categoryCode || categoryCode.trim().toUpperCase() === 'ALL';
    const cat = isAll ? '' : categoryCode.trim();

    // Check if an active stock take session already exists in tblStock
    if (forceNew === undefined || forceNew === null) {
      const checkReq = pool.request();
      checkReq.input('categoryCode', sql.VarChar, cat);
      const activeCheck = await checkReq.query(`
        SELECT COUNT(*) AS ScannedCount
        FROM tblStock
        WHERE (TrayID IS NOT NULL AND RTRIM(TrayID) <> '' AND RTRIM(TrayID) <> 'X')
          AND (@categoryCode = '' OR RTRIM(CategoryCode) = RTRIM(@categoryCode))
      `);
      const scannedCount = activeCheck.recordset[0] ? Number(activeCheck.recordset[0].ScannedCount) || 0 : 0;
      if (scannedCount > 0) {
        const dbState = await fetchDatabaseState(pool, categoryCode);
        return res.json({
          success: true,
          alreadyStarted: true,
          userRole,
          userName,
          summary: dbState.summary,
          scannedList: dbState.scannedList,
        });
      }
    }

    // Wrap preparation DB reset/updates inside a SQL transaction
    const transaction = new sql.Transaction(pool);
    await transaction.begin();

    try {
      // If forceNew is true (NEW Stock Take chosen by user):
      // Reset existing scanned items in tblStock back to 'X' for the selected category (StockDelete equivalent)
      if (forceNew === true) {
        const resetReq = new sql.Request(transaction);
        resetReq.input('categoryCode', sql.VarChar, cat);
        resetReq.input('userName', sql.VarChar, userName);
        await resetReq.query(`
          UPDATE tblStock
          SET 
            TrayID = 'X',
            ModifyUser = @userName,
            ModifyDate = GETDATE()
          WHERE TrayID IS NOT NULL 
            AND RTRIM(TrayID) <> '' 
            AND RTRIM(TrayID) <> 'X'
            AND (@categoryCode = '' OR RTRIM(CategoryCode) = RTRIM(@categoryCode))
        `);
      }

      // Update modify user and date on tblStock for selected category (UpdateModifyUser equivalent)
      const touchReq = new sql.Request(transaction);
      touchReq.input('categoryCode', sql.VarChar, cat);
      touchReq.input('userName', sql.VarChar, userName);
      await touchReq.query(`
        UPDATE tblStock
        SET 
          ModifyUser = @userName,
          ModifyDate = GETDATE()
        WHERE (@categoryCode = '' OR RTRIM(CategoryCode) = RTRIM(@categoryCode))
      `);

      await transaction.commit();
    } catch (txErr) {
      await transaction.rollback();
      throw txErr;
    }

    // Update active session state ONLY after successful DB transaction execution
    currentSession = {
      isPrepared: true,
      categoryCode: categoryCode || "ALL",
      userRole,
      userName,
    };

    const { summary, scannedList } = await fetchDatabaseState(pool, currentSession.categoryCode);

    res.json({
      success: true,
      message: forceNew ? "New Stock Take session started." : "Stock Take session initialized from database.",
      userRole,
      userName,
      summary,
      scannedList,
    });
  } catch (err) {
    console.error("Stock Take Proceed Error:", err);
    // Ensure isPrepared stays false on failure
    res.status(500).json({
      success: false,
      message: "Failed to proceed with Stock Take preparation.",
      error: err.message,
    });
  }
});

// 4. POST /api/stocktake/scan
// Legacy behavior Stock Take Scan API
router.post("/scan", async (req, res) => {
  try {
    const { trayId, inventoryCode } = req.body;

    if (!trayId || !inventoryCode) {
      return res.status(400).json({
        success: false,
        message: "Both Tray ID and Inventory Code are required for Stock Take scan.",
      });
    }

    // Server-side check: Must complete Proceed Stock Take first!
    if (!currentSession.isPrepared) {
      return res.status(400).json({
        success: false,
        message: "Stock Take session is not active. Please complete Download Stock Take & Proceed setup first.",
      });
    }

    const trimmedTray = trayId.trim();
    const trimmedCode = inventoryCode.trim();


    const pool = req.pool;

    // 1. TRAY ID VALIDATION: Verify whether entered Tray ID exists in tblTrayMaster / tblTraymaster
    const trayCheckReq = pool.request();
    trayCheckReq.input("trayId", sql.VarChar, trimmedTray);
    const trayRes = await trayCheckReq.query(`
      SELECT TOP 1 RTRIM(TrayID) AS TrayID, RTRIM(TrayName) AS TrayName
      FROM tblTrayMaster
      WHERE UPPER(RTRIM(TrayID)) = UPPER(RTRIM(@trayId)) OR UPPER(RTRIM(TrayName)) = UPPER(RTRIM(@trayId))
    `);

    if (!trayRes.recordset || trayRes.recordset.length === 0) {
      return res.json({
        success: false,
        message: "Tray Not Found!",
      });
    }

    const matchedTray = trayRes.recordset[0].TrayID.trim();

    // 2. INVENTORY CODE VALIDATION & TRANSACTION SAFETY (Items 4 & 8)
    // Use an explicit SQL transaction with UPDLOCK/HOLDLOCK to prevent race conditions
    const transaction = new sql.Transaction(pool);
    await transaction.begin();

    try {
      const checkReq = new sql.Request(transaction);
      checkReq.input("inventoryCode", sql.VarChar, trimmedCode);
      let existingRes = await checkReq.query(`
        SELECT TOP 1
          RTRIM(TrayID) AS TrayID,
          RTRIM(LocationCode) AS LocationCode,
          RTRIM(InventoryCode) AS InventoryCode,
          RTRIM(CatalogueCode) AS CatalogueCode,
          RTRIM(Description) AS Description,
          Weight,
          CostPerGram,
          TotalCost,
          RTRIM(CategoryCode) AS CategoryCode,
          PCS
        FROM tblStock WITH (UPDLOCK, HOLDLOCK)
        WHERE (UPPER(RTRIM(InventoryCode)) = UPPER(RTRIM(@inventoryCode)) OR UPPER(RTRIM(CatalogueCode)) = UPPER(RTRIM(@inventoryCode)))
      `);

      if (!existingRes.recordset || existingRes.recordset.length === 0) {
        // Fallback: Check tblInventory if item exists in master inventory table
        const fbReq = new sql.Request(transaction);
        fbReq.input("inventoryCode", sql.VarChar, trimmedCode);
        const invMatch = await fbReq.query(`
          SELECT TOP 1
            RTRIM(InventoryCode) AS InventoryCode,
            RTRIM(CatalogueCode) AS CatalogueCode,
            RTRIM(Description) AS Description,
            ISNULL(Weight, 0) AS Weight,
            ISNULL(CostPerGram, 0) AS CostPerGram,
            ISNULL(TotalCost, 0) AS TotalCost,
            RTRIM(CategoryCode) AS CategoryCode,
            ISNULL(LocationCode, 'HQ') AS LocationCode
          FROM tblInventory
          WHERE (UPPER(RTRIM(InventoryCode)) = UPPER(RTRIM(@inventoryCode)) OR UPPER(RTRIM(CatalogueCode)) = UPPER(RTRIM(@inventoryCode)))
        `);

        if (invMatch.recordset && invMatch.recordset.length > 0) {
          const item = invMatch.recordset[0];
          const insReq = new sql.Request(transaction);
          insReq.input("trayId", sql.VarChar, matchedTray || trimmedTray);
          insReq.input("locationCode", sql.VarChar, item.LocationCode || 'HQ');
          insReq.input("inventoryCode", sql.VarChar, item.InventoryCode || trimmedCode);
          insReq.input("catalogueCode", sql.VarChar, item.CatalogueCode || "");
          insReq.input("description", sql.VarChar, item.Description || "");
          insReq.input("weight", sql.Float, Number(item.Weight) || 0);
          insReq.input("costPerGram", sql.Float, Number(item.CostPerGram) || 0);
          insReq.input("totalCost", sql.Float, Number(item.TotalCost) || 0);
          insReq.input("categoryCode", sql.VarChar, item.CategoryCode || "");
          insReq.input("userName", sql.VarChar, currentSession.userName || "SYSTEM");

          const insRes = await insReq.query(`
            INSERT INTO tblStock (
              TrayID, LocationCode, InventoryCode, CatalogueCode, Description, Weight, CostPerGram, TotalCost, CategoryCode, PCS, ModifyUser, ModifyDate
            )
            OUTPUT RTRIM(INSERTED.TrayID) AS TrayID, RTRIM(INSERTED.LocationCode) AS LocationCode, RTRIM(INSERTED.InventoryCode) AS InventoryCode, RTRIM(INSERTED.CatalogueCode) AS CatalogueCode, RTRIM(INSERTED.Description) AS Description, INSERTED.Weight, INSERTED.CostPerGram, INSERTED.TotalCost, RTRIM(INSERTED.CategoryCode) AS CategoryCode, INSERTED.PCS
            VALUES (
              @trayId, @locationCode, @inventoryCode, @catalogueCode, @description, @weight, @costPerGram, @totalCost, @categoryCode, 1, @userName, GETDATE()
            )
          `);

          if (insRes.recordset && insRes.recordset.length > 0) {
            existingRes = insRes;
          }
        }
      }

      if (!existingRes.recordset || existingRes.recordset.length === 0) {
        await transaction.rollback();
        return res.json({
          success: false,
          message: `${trimmedCode} Not Found`,
        });
      }

      const row = existingRes.recordset[0];
      const currentTrayID = row.TrayID ? row.TrayID.trim() : "";
      const locationCode = row.LocationCode ? row.LocationCode.trim() : "";

      // 3. ALREADY SCANNED CHECK
      // If TrayID is NOT "X" and NOT empty/null (i.e. ANY valid Tray ID including the same TrayID):
      // DO NOT UPDATE IT.
      if (currentTrayID !== "" && currentTrayID.toUpperCase() !== "X") {
        await transaction.rollback();
        const trayItems = await fetchTrayItems(pool, trimmedTray);
        const summary = await fetchTrayTotals(pool, trimmedTray);

        return res.json({
          success: false,
          alreadyScanned: true,
          message: `${trimmedCode} Already Scanned`,
          selectedTrayId: trimmedTray,
          scannedList: trayItems,
          summary,
        });
      }

      // 4 & 5. UPDATE EXACT STOCK RECORD
      const updateReq = new sql.Request(transaction);
      updateReq.input("trayId", sql.VarChar, matchedTray || trimmedTray);
      updateReq.input("inventoryCode", sql.VarChar, trimmedCode);
      updateReq.input("locationCode", sql.VarChar, locationCode);
      updateReq.input("userName", sql.VarChar, currentSession.userName || 'SYSTEM');
      const updateRes = await updateReq.query(`
        UPDATE tblStock
        SET 
          TrayID = @trayId,
          ModifyUser = @userName,
          ModifyDate = GETDATE()
        WHERE RTRIM(InventoryCode) = RTRIM(@inventoryCode)
          AND (@locationCode = '' OR RTRIM(LocationCode) = RTRIM(@locationCode))
      `);

      const affected = updateRes.rowsAffected ? updateRes.rowsAffected[0] : 0;
      if (affected === 0) {
        await transaction.rollback();
        return res.status(400).json({
          success: false,
          message: `Stock update failed for item ${trimmedCode}. No rows modified.`,
        });
      }

      if (affected > 1) {
        await transaction.rollback();
        return res.status(500).json({
          success: false,
          message: `Stock update aborted for item ${trimmedCode}. Multiple rows (${affected}) matched, update blocked for database safety.`,
        });
      }

      await transaction.commit();
    } catch (txErr) {
      await transaction.rollback();
      throw txErr;
    }

    // Read exact updated item using parameterized query (Item 7 requirement)
    const readSavedReq = pool.request();
    readSavedReq.input("inventoryCode", sql.VarChar, trimmedCode);
    readSavedReq.input("trayId", sql.VarChar, trimmedTray);
    const readRes = await readSavedReq.query(`
      SELECT TOP 1
        RTRIM(TrayID) AS TrayID,
        RTRIM(InventoryCode) AS InventoryCode,
        RTRIM(CatalogueCode) AS CatalogueCode,
        RTRIM(Description) AS Description,
        Weight,
        CostPerGram,
        TotalCost,
        RTRIM(CategoryCode) AS CategoryCode,
        PCS
      FROM tblStock
      WHERE RTRIM(InventoryCode) = RTRIM(@inventoryCode)
        AND RTRIM(TrayID) = RTRIM(@trayId)
    `);

    const updatedRow = readRes.recordset[0];
    const weight = updatedRow.Weight != null ? Number(updatedRow.Weight) || 0 : 0;
    const totalCost = updatedRow.TotalCost != null ? Number(updatedRow.TotalCost) || 0 : 0;
    const costPerGram = updatedRow.CostPerGram != null && Number(updatedRow.CostPerGram) > 0
      ? Number(updatedRow.CostPerGram)
      : (weight > 0 ? totalCost / weight : 0);

    const updatedItem = {
      trayId: updatedRow.TrayID ? updatedRow.TrayID.trim() : trimmedTray,
      inventoryCode: updatedRow.InventoryCode ? updatedRow.InventoryCode.trim() : trimmedCode,
      catalogueCode: updatedRow.CatalogueCode ? updatedRow.CatalogueCode.trim() : "",
      description: updatedRow.Description ? updatedRow.Description.trim() : "",
      weight,
      costPerGram,
      totalCost,
      categoryCode: updatedRow.CategoryCode ? updatedRow.CategoryCode.trim() : "",
      pcs: updatedRow.PCS != null ? Number(updatedRow.PCS) || 0 : 1,
      scanned: true,
    };

    // 6. AFTER SUCCESSFUL SCAN: Refresh selected Tray list and totals
    const trayItems = await fetchTrayItems(pool, trimmedTray);
    const summary = await fetchTrayTotals(pool, trimmedTray);

    return res.json({
      success: true,
      message: `Item ${trimmedCode} assigned to Tray ${trimmedTray}.`,
      item: updatedItem,
      selectedTrayId: trimmedTray,
      scannedList: trayItems,
      summary,
    });
  } catch (err) {
    console.error("Stock Take Scan Error:", err);
    res.status(500).json({
      success: false,
      message: "Failed to save Stock Take entry to database.",
      error: err.message,
    });
  }
});

let isSaveInProgress = false;

// 5. POST /api/stocktake/save
// Save current Stock Take session permanently in database using SQL Server stored procedure sp_UpdateStockTake / tblStockLog
router.post("/save", async (req, res) => {
  try {
    // 8. VERIFY SESSION STATE
    if (!currentSession.isPrepared) {
      return res.status(400).json({
        success: false,
        message: "No active Stock Take session to save. Please start a session first.",
      });
    }

    // 7. PREVENT DUPLICATE SAVE (Concurrency lock)
    if (isSaveInProgress) {
      return res.status(409).json({
        success: false,
        message: "A Stock Take save operation is currently in progress. Please wait.",
      });
    }

    isSaveInProgress = true;
    const { trayId } = req.body || {};
    const pool = req.pool;

    // 4. SAVE TRANSACTION SAFETY
    const transaction = new sql.Transaction(pool);
    await transaction.begin();

    try {
      const spReq = new sql.Request(transaction);
      await spReq.query("EXEC sp_UpdateStockTake");

      await transaction.commit();
    } catch (txErr) {
      await transaction.rollback();

      // Log full SQL Server error details for diagnosis (Section 7)
      console.error("=== STOCK TAKE SAVE ERROR DETAILS ===");
      console.error("Error Message:", txErr.message);
      console.error("Error Number:", txErr.number || (txErr.originalError ? txErr.originalError.number : null));
      console.error("Procedure Name:", txErr.procName || "sp_UpdateStockTake");
      console.error("Line Number:", txErr.lineNumber);
      console.error("State:", txErr.state);
      console.error("Class / Severity:", txErr.class);
      console.error("Full Trace:", txErr);

      return res.status(500).json({
        success: false,
        message: txErr.message || "Failed to save Stock Take to database.",
      });
    } finally {
      isSaveInProgress = false;
    }

    // Reset session preparation state ONLY AFTER successful transaction commit
    currentSession.isPrepared = false;

    // 3. FIX SAVE SUMMARY - Post-save state clears active session/tray items
    const { summary } = await fetchDatabaseState(pool, currentSession.categoryCode, '');

    return res.json({
      success: true,
      message: "Stock Take saved to database successfully!",
      summary,
      scannedList: [],
      selectedTrayId: '',
    });
  } catch (err) {
    isSaveInProgress = false;
    console.error("Stock Take Save Error:", err);
    return res.status(500).json({
      success: false,
      message: err.message || "Failed to save Stock Take to database.",
    });
  }
});

// 6. GET /api/stocktake/summary
// Get current session summary & scanned list directly from database
router.get("/summary", async (req, res) => {
  try {
    const pool = req.pool;
    const { summary, scannedList } = await fetchDatabaseState(pool, currentSession.categoryCode);
    res.json({
      success: true,
      isPrepared: currentSession.isPrepared,
      summary,
      scannedList,
    });
  } catch (err) {
    res.status(500).json({
      success: false,
      message: "Failed to fetch Stock Take summary from database.",
      error: err.message,
    });
  }
});

// 6b. GET /api/stocktake/list?trayId=...
// Fetch stock items belonging ONLY to the specified Tray ID (Legacy List button behavior)
router.get("/list", async (req, res) => {
  try {
    const { trayId } = req.query;

    if (!trayId || !String(trayId).trim()) {
      return res.status(400).json({
        success: false,
        message: "Please enter Tray ID to list stock items.",
      });
    }

    const trimmedTray = String(trayId).trim();


    const pool = req.pool;

    // Validate if Tray ID exists in Tray Master (match TrayID or TrayName)
    const trayCheckReq = pool.request();
    trayCheckReq.input("trayId", sql.VarChar, trimmedTray);
    const trayRes = await trayCheckReq.query(`
      SELECT TOP 1 RTRIM(TrayID) AS TrayID, RTRIM(TrayName) AS TrayName
      FROM tblTrayMaster
      WHERE UPPER(RTRIM(TrayID)) = UPPER(RTRIM(@trayId)) OR UPPER(RTRIM(TrayName)) = UPPER(RTRIM(@trayId))
    `);

    if (!trayRes.recordset || trayRes.recordset.length === 0) {
      return res.json({
        success: false,
        message: "Tray Not Found!",
        scannedList: [],
      });
    }

    const targetTray = trayRes.recordset[0].TrayID.trim();

    // Query stock records belonging ONLY to specified Tray ID
    const listReq = pool.request();
    listReq.input("trayId", sql.VarChar, targetTray);
    const listRes = await listReq.query(`
      SELECT
        RTRIM(TrayID) AS TrayID,
        RTRIM(InventoryCode) AS InventoryCode,
        RTRIM(CatalogueCode) AS CatalogueCode,
        RTRIM(Description) AS Description,
        Weight,
        CostPerGram,
        TotalCost,
        RTRIM(CategoryCode) AS CategoryCode,
        PCS
      FROM tblStock
      WHERE UPPER(RTRIM(TrayID)) = UPPER(RTRIM(@trayId))
      ORDER BY ModifyDate DESC, InventoryCode ASC
    `);

    const scannedList = (listRes.recordset || []).map((row) => {
      const weight = row.Weight != null ? Number(row.Weight) || 0 : 0;
      const totalCost = row.TotalCost != null ? Number(row.TotalCost) || 0 : 0;
      const costPerGram =
        row.CostPerGram != null && Number(row.CostPerGram) > 0
          ? Number(row.CostPerGram)
          : weight > 0
            ? totalCost / weight
            : 0;

      return {
        trayId: row.TrayID ? row.TrayID.trim() : "",
        inventoryCode: row.InventoryCode ? row.InventoryCode.trim() : "",
        catalogueCode: row.CatalogueCode ? row.CatalogueCode.trim() : "",
        description: row.Description ? row.Description.trim() : "",
        weight,
        costPerGram,
        totalCost,
        categoryCode: row.CategoryCode ? row.CategoryCode.trim() : "",
        pcs: row.PCS != null ? Number(row.PCS) || 0 : 1,
        scanned: true,
      };
    });

    const summary = await fetchTrayTotals(pool, trimmedTray);

    if (scannedList.length === 0) {
      return res.json({
        success: true,
        message: `No records found for Tray ID ${trimmedTray}.`,
        scannedList: [],
        summary,
      });
    }

    return res.json({
      success: true,
      message: `Displaying ${scannedList.length} items for Tray ${trimmedTray}.`,
      scannedList,
      summary,
    });
  } catch (err) {
    console.error("Stock Take List Error:", err);
    res.status(500).json({
      success: false,
      message: "Failed to fetch stock take list for specified Tray ID.",
      error: err.message,
    });
  }
});

// 7. DELETE /api/stocktake/item/:inventoryCode (also POST /api/stocktake/delete-item)
// Delete / Unscan item by resetting its TrayID to 'X' in tblStock database table safely
async function handleDeleteStockItem(req, res) {
  try {
    const inventoryCode = req.params.inventoryCode || req.body.inventoryCode;
    const trayId = req.query.trayId || req.body.trayId;

    if (!inventoryCode || !String(inventoryCode).trim()) {
      return res.status(400).json({
        success: false,
        message: "Inventory Code is required to delete item.",
      });
    }

    const trimmedCode = String(inventoryCode).trim();
    const pool = req.pool;

    // Use transaction and row-level locking for delete/unscan operation
    const transaction = new sql.Transaction(pool);
    await transaction.begin();

    try {
      const checkReq = new sql.Request(transaction);
      checkReq.input("inventoryCode", sql.VarChar, trimmedCode);
      const selRes = await checkReq.query(`
        SELECT TOP 1 RTRIM(LocationCode) AS LocationCode
        FROM tblStock WITH (UPDLOCK, HOLDLOCK)
        WHERE RTRIM(InventoryCode) = RTRIM(@inventoryCode)
      `);

      if (selRes.recordset && selRes.recordset.length > 0) {
        const locationCode = selRes.recordset[0].LocationCode ? selRes.recordset[0].LocationCode.trim() : "";
        const deleteReq = new sql.Request(transaction);
        deleteReq.input("inventoryCode", sql.VarChar, trimmedCode);
        deleteReq.input("locationCode", sql.VarChar, locationCode);
        deleteReq.input("userName", sql.VarChar, currentSession.userName || "SYSTEM");
        const delRes = await deleteReq.query(`
          UPDATE tblStock
          SET 
            TrayID = 'X',
            ModifyUser = @userName,
            ModifyDate = GETDATE()
          WHERE RTRIM(InventoryCode) = RTRIM(@inventoryCode)
            AND (@locationCode = '' OR RTRIM(LocationCode) = RTRIM(@locationCode))
        `);

        const affected = delRes.rowsAffected ? delRes.rowsAffected[0] : 0;
        if (affected > 1) {
          await transaction.rollback();
          return res.status(500).json({
            success: false,
            message: `Delete aborted for item ${trimmedCode}. Multiple rows (${affected}) matched, action blocked for database safety.`,
          });
        }
      }
      await transaction.commit();
    } catch (txErr) {
      await transaction.rollback();
      throw txErr;
    }

    // Fetch refreshed state (if a specific tray was being viewed, return tray list, else full state)
    let summary, scannedList;
    if (trayId && String(trayId).trim() && String(trayId).trim().toUpperCase() !== 'X') {
      const trimmedTray = String(trayId).trim();
      scannedList = await fetchTrayItems(pool, trimmedTray);
      summary = await fetchTrayTotals(pool, trimmedTray);
    } else {
      const state = await fetchDatabaseState(pool, currentSession.categoryCode);
      summary = state.summary;
      scannedList = state.scannedList;
    }

    return res.json({
      success: true,
      message: `Item ${trimmedCode} removed from Stock Take.`,
      scannedList,
      summary,
    });
  } catch (err) {
    console.error("Delete Stock Item Error:", err);
    res.status(500).json({
      success: false,
      message: "Failed to delete item from database.",
      error: err.message,
    });
  }
}

// 8. POST /api/stocktake/update-item & PUT /api/stocktake/item/:inventoryCode
// Update Stock Take item details (TrayID, Weight, PCS) directly in tblStock database table
async function handleUpdateStockItem(req, res) {
  try {
    const inventoryCode = req.params.inventoryCode || (req.body ? req.body.inventoryCode : '');
    const { trayId, weight, pcs, currentViewTrayId } = req.body || {};

    if (!inventoryCode || !String(inventoryCode).trim()) {
      return res.status(400).json({
        success: false,
        message: "Inventory Code is required to update item.",
      });
    }

    const trimmedCode = String(inventoryCode).trim();
    const newTrayId = trayId !== undefined && trayId !== null ? String(trayId).trim() : 'X';
    const newWeight = weight != null && weight !== '' ? Number(weight) || 0 : null;
    const newPcs = pcs != null && pcs !== '' ? Number(pcs) || 1 : null;
    const pool = req.pool;

    // Validate Tray ID exists in Tray Master if not 'X'
    if (newTrayId.toUpperCase() !== 'X' && newTrayId !== '') {
      const trayCheckReq = pool.request();
      trayCheckReq.input("trayId", sql.VarChar, newTrayId);
      const trayRes = await trayCheckReq.query(`
        SELECT TOP 1 RTRIM(TrayID) AS TrayID
        FROM tblTrayMaster
        WHERE RTRIM(TrayID) = RTRIM(@trayId)
      `);
      if (!trayRes.recordset || trayRes.recordset.length === 0) {
        return res.status(400).json({
          success: false,
          message: "Tray Not Found!",
        });
      }
    }

    // Use transaction and row-level locking for update operation
    const transaction = new sql.Transaction(pool);
    await transaction.begin();

    try {
      const updateReq = new sql.Request(transaction);
      updateReq.input("inventoryCode", sql.VarChar, trimmedCode);
      updateReq.input("trayId", sql.VarChar, newTrayId);
      updateReq.input("weight", sql.Numeric(18, 4), newWeight);
      updateReq.input("pcs", sql.Numeric(18, 0), newPcs);
      updateReq.input("userName", sql.VarChar, currentSession.userName || "SYSTEM");

      const updateRes = await updateReq.query(`
        UPDATE tblStock
        SET 
          TrayID = @trayId,
          Weight = COALESCE(@weight, Weight),
          PCS = COALESCE(@pcs, PCS),
          ModifyUser = @userName,
          ModifyDate = GETDATE()
        WHERE RTRIM(InventoryCode) = RTRIM(@inventoryCode)
      `);

      const affected = updateRes.rowsAffected ? updateRes.rowsAffected[0] : 0;
      if (affected === 0) {
        await transaction.rollback();
        return res.status(404).json({
          success: false,
          message: `Stock item ${trimmedCode} not found in database.`,
        });
      }

      if (affected > 1) {
        await transaction.rollback();
        return res.status(500).json({
          success: false,
          message: `Update aborted for item ${trimmedCode}. Multiple rows (${affected}) matched, action blocked for database safety.`,
        });
      }

      await transaction.commit();
    } catch (txErr) {
      await transaction.rollback();
      throw txErr;
    }

    // Fetch actual updated item details from database
    const updatedRowRes = await pool.request()
      .input("inventoryCode", sql.VarChar, trimmedCode)
      .query(`
        SELECT TOP 1
          RTRIM(TrayID) AS TrayID,
          RTRIM(InventoryCode) AS InventoryCode,
          RTRIM(Description) AS Description,
          ISNULL(Weight, 0) AS Weight,
          ISNULL(CostPerGram, 0) AS CostPerGram,
          ISNULL(PCS, 0) AS PCS,
          ISNULL(TotalCost, 0) AS TotalCost
        FROM tblStock
        WHERE RTRIM(InventoryCode) = RTRIM(@inventoryCode)
      `);
    const updatedRow = updatedRowRes.recordset[0] || {};
    const updatedTrayId = updatedRow.TrayID || (newTrayId.toUpperCase() !== 'X' ? newTrayId : '');

    // Fetch refreshed state directly for the updated Tray ID
    let summary, scannedList;
    if (updatedTrayId && updatedTrayId.toUpperCase() !== 'X') {
      scannedList = await fetchTrayItems(pool, updatedTrayId);
      summary = await fetchTrayTotals(pool, updatedTrayId);
    } else {
      const state = await fetchDatabaseState(pool, currentSession.categoryCode);
      summary = state.summary;
      scannedList = state.scannedList;
    }

    const updatedItem = {
      trayId: updatedTrayId,
      inventoryCode: updatedRow.InventoryCode || trimmedCode,
      description: updatedRow.Description || '',
      weight: Number(updatedRow.Weight) || 0,
      costPerGram: Number(updatedRow.CostPerGram) || 0,
      pcs: Number(updatedRow.PCS) || 0,
      totalCost: Number(updatedRow.TotalCost) || 0,
    };

    console.log("UPDATED ITEM:", updatedItem);
    console.log("UPDATED TRAY ID:", updatedItem.trayId);
    console.log("REFRESHING SUMMARY FOR TRAY:", updatedItem.trayId);
    console.log("UPDATED SUMMARY:", summary);

    return res.json({
      success: true,
      message: `Item ${trimmedCode} updated in database successfully!`,
      updatedItem,
      scannedList,
      summary,
    });
  } catch (err) {
    console.error("Update Stock Item Error:", err);
    res.status(500).json({
      success: false,
      message: "Failed to update item in database.",
      error: err.message,
    });
  }
}

router.delete("/item/:inventoryCode", handleDeleteStockItem);
router.post("/delete-item", handleDeleteStockItem);
router.post("/update-item", handleUpdateStockItem);
router.put("/item/:inventoryCode", handleUpdateStockItem);

// ==========================================
// STOCK TAKE REPORT API (Matching VB.NET frmStockTakeReport)
// ==========================================

// Helper: parse date to inclusive start or end of day in YYYY-MM-DD format
function parseDateParam(dateStr) {
  if (!dateStr) return null;
  if (typeof dateStr === "string" && dateStr.includes("/")) {
    const parts = dateStr.split("/");
    if (parts.length === 3) {
      // DD/MM/YYYY -> YYYY-MM-DD
      return `${parts[2]}-${parts[1].padStart(2, "0")}-${parts[0].padStart(2, "0")}`;
    }
  }
  return dateStr;
}

// 7. GET /api/stocktake/trays - List all available trays for dropdown/search
router.get("/trays", async (req, res) => {
  try {
    const pool = req.pool;
    const trayRes = await pool.request().query(`
      SELECT DISTINCT
        RTRIM(TrayID) AS TrayID,
        RTRIM(ISNULL(TrayName, TrayID)) AS TrayName
      FROM tblTrayMaster
      WHERE TrayID IS NOT NULL AND RTRIM(TrayID) <> ''
      ORDER BY TrayID ASC
    `);
    res.json({
      success: true,
      data: trayRes.recordset || [],
    });
  } catch (err) {
    console.error("Stock Take Get Trays Error:", err);
    res.status(500).json({ success: false, message: err.message, data: [] });
  }
});

// 8. POST /api/stocktake/report - Fetch Stock Take Report based on Mode, Date Range, and optional Tray ID
router.post("/report", async (req, res) => {
  try {
    const { fromDate, toDate, mode = "all", trayId = "" } = req.body;
    const pool = req.pool;

    // Date range validation
    const dateVal = validateBackendDates(fromDate, toDate);
    if (!dateVal.isValid) {
      return res.status(400).json({ success: false, message: dateVal.message });
    }

    const sFrom = parseDateParam(fromDate);
    const sTo = parseDateParam(toDate);

    const fromIso = `${sFrom} 00:00:00`;
    const toIso = `${sTo} 23:59:59`;

    const request = pool.request();
    request.input("FromDate", sql.DateTime, new Date(fromIso));
    request.input("ToDate", sql.DateTime, new Date(toIso));

    let whereClause = "WHERE s.ModifyDate >= @FromDate AND s.ModifyDate <= @ToDate";
    let reportModeTitle = "Stock Take Report  -All";

    const normalizedMode = String(mode).toLowerCase().trim();
    if (normalizedMode === "scanned") {
      whereClause += " AND s.TrayID <> 'X'";
      reportModeTitle = "Stock Take Report  -Scanned Items";
    } else if (normalizedMode === "unscanned") {
      whereClause += " AND s.TrayID = 'X'";
      reportModeTitle = "Stock Take Report  -UnScanned Items";
    }

    if (trayId && String(trayId).trim()) {
      const trimmedTray = String(trayId).trim();
      request.input("TrayID", sql.VarChar, trimmedTray);
      whereClause += " AND UPPER(RTRIM(s.TrayID)) = UPPER(RTRIM(@TrayID))";
    }

    // Format display date range for title
    const fDisp = formatDateDisp(sFrom);
    const tDisp = formatDateDisp(sTo);
    const reportTitle = `${reportModeTitle} From ${fDisp} To ${tDisp}`;

    const query = `
      SELECT
        RTRIM(s.TrayID) AS TrayID,
        RTRIM(s.InventoryCode) AS InventoryCode,
        RTRIM(ISNULL(s.Description, '')) AS Description,
        RTRIM(ISNULL(s.CategoryCode, '')) AS CategoryCode,
        RTRIM(ISNULL(s.CatalogueCode, '')) AS CatalogueCode,
        ISNULL(s.PCS, 1) AS PCS,
        ISNULL(s.Weight, 0) AS Weight,
        ISNULL(s.CostPerGram, 0) AS CostPerGram,
        ISNULL(s.TotalCost, 0) AS TotalCost,
        s.ModifyDate,
        CASE 
          WHEN s.TrayID IS NOT NULL AND RTRIM(s.TrayID) <> 'X' AND RTRIM(s.TrayID) <> '' THEN 'Scanned'
          ELSE 'Unscanned'
        END AS Status
      FROM tblStock s WITH (NOLOCK)
      ${whereClause}
      ORDER BY s.TrayID ASC, s.InventoryCode ASC
    `;

    const result = await request.query(query);
    const rows = result.recordset || [];

    let totalWeight = 0;
    let totalPcs = 0;
    let totalCost = 0;

    rows.forEach((r) => {
      totalWeight += Number(r.Weight) || 0;
      totalPcs += Number(r.PCS) || 0;
      totalCost += Number(r.TotalCost) || 0;
    });

    const columns = [
      { key: "TrayID", label: "TRAY ID", width: 110, align: "center" },
      { key: "InventoryCode", label: "INVENTORY CODE", width: 160, align: "left" },
      { key: "Description", label: "DESCRIPTION", width: 240, align: "left" },
      { key: "Weight", label: "WEIGHT", width: 120, align: "right", format: "decimal3" },
      { key: "CostPerGram", label: "COST", width: 120, align: "right", format: "currency" },
      { key: "PCS", label: "PCS", width: 80, align: "center" },
      { key: "TotalCost", label: "TOTAL COST", width: 140, align: "right", format: "currency" },
    ];

    res.json({
      success: true,
      title: reportTitle,
      columns,
      data: rows,
      totals: {
        totalRecords: rows.length,
        totalItems: rows.length,
        totalPcs,
        totalWeight: Number(totalWeight.toFixed(3)),
        totalCost: Number(totalCost.toFixed(2)),
      },
    });
  } catch (err) {
    console.error("Stock Take Report Generation Error:", err);
    res.status(500).json({ success: false, message: err.message });
  }
});

// POST /api/stocktake/download-html
router.post("/download-html", async (req, res) => {
  try {
    const { generateReportHtml } = require("../utils/reportHtmlTemplate");
    const { title, subtitle, columns, data, totals, companyInfo, fromDate, toDate, showPrintButton } = req.body;
    const html = await generateReportHtml({
      title: title || "Stock Take Report",
      subtitle: subtitle || "",
      columns,
      data,
      totals,
      companyInfo,
      fromDate,
      toDate,
      showPrintButton: showPrintButton ?? false,
      pool: req.pool,
    });
    res.setHeader("Content-Type", "text/html; charset=utf-8");
    res.send(html);
  } catch (err) {
    console.error("Stock Take Download HTML Error:", err);
    res.status(500).send("Error generating report HTML: " + err.message);
  }
});

// POST /api/stocktake/export-excel
router.post("/export-excel", (req, res) => {
  const { handleExportExcel } = require("../utils/reportExportHandlers");
  handleExportExcel({ req, res, defaultTitle: "Stock_Take_Report" });
});

// POST /api/stocktake/export-pdf
router.post("/export-pdf", (req, res) => {
  const { handleExportPdf } = require("../utils/reportExportHandlers");
  handleExportPdf({ req, res, defaultTitle: "Stock_Take_Report" });
});

module.exports = router;



