const express = require("express");
const router = express.Router();
const sql = require("mssql");

// Active purchase take session state in memory
let currentPurchaseSession = {
  isPrepared: false,
  jobSheetNo: "",
  categoryCode: "",
  userRole: "",
  userName: "",
};

// Helper function to fetch items belonging to a specific tray in tblPurchaseStock
async function fetchTrayItems(pool, trayId, jobSheetNo = "") {
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
    FROM tblPurchaseStock
    WHERE UPPER(RTRIM(TrayID)) = UPPER(RTRIM(@trayId))
  `;

  if (trayId.trim().toUpperCase() === "X") {
    query += ` AND Scanned = 1`;
  }

  if (jobSheetNo && jobSheetNo !== "ALL") {
    req.input("jobSheetNo", sql.VarChar, jobSheetNo.trim());
    query += ` AND RTRIM(JobSheetNo) = RTRIM(@jobSheetNo)`;
  }

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

// Helper function to calculate tray totals exclusively from tblPurchaseStock
async function fetchTrayTotals(pool, trayId = "", jobSheetNo = "") {
  const trimmedTray = trayId ? String(trayId).trim() : "";

  // 1. TOTAL: Complete stock from tblPurchaseStock
  let totalReq = pool.request();
  let totalQuery = `
    SELECT 
      SUM(ISNULL(Weight, 0)) AS TotalWeight,
      SUM(ISNULL(PCS, 0)) AS TotalPcs
    FROM tblPurchaseStock
    WHERE 1=1
  `;
  if (jobSheetNo && jobSheetNo !== "ALL") {
    totalReq.input("jobSheetNo", sql.VarChar, jobSheetNo.trim());
    totalQuery += ` AND RTRIM(JobSheetNo) = RTRIM(@jobSheetNo)`;
  }

  const totalRes = await totalReq.query(totalQuery);
  const totRow = totalRes.recordset[0] || {};
  const totalWeightNum = Number(totRow.TotalWeight) || 0;
  const totalPcsNum = Number(totRow.TotalPcs) || 0;

  // 2. SCANNED: Stock scanned in tblPurchaseStock
  let scannedReq = pool.request();
  let scannedQuery = `
    SELECT 
      SUM(ISNULL(Weight, 0)) AS ScannedWeight,
      SUM(ISNULL(PCS, 0)) AS ScannedPcs
    FROM tblPurchaseStock
    WHERE (Scanned = 1 OR (TrayID IS NOT NULL AND RTRIM(TrayID) <> '' AND UPPER(RTRIM(TrayID)) <> 'X'))
  `;

  if (trimmedTray && trimmedTray.toUpperCase() !== "X") {
    scannedReq.input("trayId", sql.VarChar, trimmedTray);
    scannedQuery += ` AND UPPER(RTRIM(TrayID)) = UPPER(RTRIM(@trayId))`;
  }

  if (jobSheetNo && jobSheetNo !== "ALL") {
    scannedReq.input("jobSheetNo", sql.VarChar, jobSheetNo.trim());
    scannedQuery += ` AND RTRIM(JobSheetNo) = RTRIM(@jobSheetNo)`;
  }

  const scannedRes = await scannedReq.query(scannedQuery);
  const scRow = scannedRes.recordset[0] || {};
  const scannedWeightNum = Number(scRow.ScannedWeight) || 0;
  const scannedPcsNum = Number(scRow.ScannedPcs) || 0;

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

// GET /api/purchasetake/categories
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
    console.error("Purchase Take Categories Error:", err);
    res.status(500).json({
      success: false,
      message: "Failed to fetch categories from database.",
      error: err.message,
    });
  }
});

// GET /api/purchasetake/jobsheets
router.get("/jobsheets", async (req, res) => {
  try {
    const pool = req.pool;
    const query = `
      SELECT TOP 100 
        RTRIM(JobSheetNo) AS JobSheetNo, 
        JobSheetDate, 
        RTRIM(SupplierCode) AS SupplierCode, 
        ISNULL(TotalWeight24K, 0) AS TotalWeight24K, 
        ISNULL(TotalPCS, 0) AS TotalPCS, 
        ISNULL(TotalCost, 0) AS TotalCost
      FROM tblJobSheetHeader
      ORDER BY JobSheetDate DESC, JobSheetNo DESC
    `;
    const result = await pool.request().query(query);

    const jobsheets = (result.recordset || []).map((j) => ({
      jobSheetNo: j.JobSheetNo ? j.JobSheetNo.trim() : "",
      jobSheetDate: j.JobSheetDate,
      supplierCode: j.SupplierCode ? j.SupplierCode.trim() : "",
      totalWeight: Number(j.TotalWeight24K) || 0,
      totalPcs: Number(j.TotalPCS) || 0,
      totalCost: Number(j.TotalCost) || 0,
    }));

    res.json({
      success: true,
      jobsheets,
    });
  } catch (err) {
    console.error("Purchase Take JobSheets Error:", err);
    res.status(500).json({
      success: false,
      message: "Failed to fetch JobSheets from database.",
      error: err.message,
    });
  }
});

// POST /api/purchasetake/validate-password
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
        salesManName,
      });
    }

    const uReq = pool.request();
    uReq.input("password", sql.VarChar, trimmedPass);
    const uRes = await uReq.query(`
      SELECT TOP 1 RTRIM(UserCode) AS UserCode, RTRIM(UserName) AS UserName 
      FROM tblUserMaster WHERE RTRIM(Password) = @password OR RTRIM(UserCode) = @password
    `);

    if (uRes.recordset && uRes.recordset.length > 0) {
      const u = uRes.recordset[0];
      return res.json({
        success: true,
        salesManCode: u.UserCode ? u.UserCode.trim() : "",
        salesManName: u.UserName ? u.UserName.trim() : "Admin",
      });
    }

    return res.status(401).json({
      success: false,
      message: "Invalid Purchase Take Password.",
    });
  } catch (err) {
    console.error("Purchase Take Password Validation Error:", err);
    res.status(500).json({
      success: false,
      message: "Password validation failed.",
      error: err.message,
    });
  }
});

// Helper function to fetch real-time summary and scanned item list directly from tblPurchaseStock
async function fetchDatabaseState(pool, categoryCode = "ALL", trayId = "", jobSheetNo = "") {
  const isAll = !categoryCode || categoryCode.trim().toUpperCase() === "ALL";
  const cat = isAll ? "" : categoryCode.trim();

  const summary = await fetchTrayTotals(pool, trayId, jobSheetNo);

  const listReq = pool.request();
  listReq.input("categoryCode", sql.VarChar, cat);
  let listQuery = `
    SELECT 
      RTRIM(tblPurchaseStock.TrayID) AS TrayID,
      RTRIM(tblPurchaseStock.InventoryCode) AS InventoryCode,
      RTRIM(tblPurchaseStock.CatalogueCode) AS CatalogueCode,
      RTRIM(tblPurchaseStock.Description) AS Description,
      tblPurchaseStock.Weight,
      tblPurchaseStock.CostPerGram,
      tblPurchaseStock.TotalCost,
      RTRIM(tblPurchaseStock.CategoryCode) AS CategoryCode,
      tblPurchaseStock.PCS
    FROM tblPurchaseStock
    WHERE (tblPurchaseStock.Scanned = 1 OR (tblPurchaseStock.TrayID IS NOT NULL AND RTRIM(tblPurchaseStock.TrayID) <> '' AND RTRIM(tblPurchaseStock.TrayID) <> 'X'))
      AND (@categoryCode = '' OR RTRIM(tblPurchaseStock.CategoryCode) = RTRIM(@categoryCode))
  `;

  if (jobSheetNo && jobSheetNo !== "ALL") {
    listReq.input("jobSheetNo", sql.VarChar, jobSheetNo.trim());
    listQuery += ` AND RTRIM(tblPurchaseStock.JobSheetNo) = RTRIM(@jobSheetNo)`;
  }

  listQuery += ` ORDER BY tblPurchaseStock.ModifyDate DESC, tblPurchaseStock.InventoryCode ASC`;

  const listRes = await listReq.query(listQuery);

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

  return { summary, scannedList };
}

// POST /api/purchasetake/proceed
// Initialize Purchase Take preparation flow & summary totals dynamically from tblPurchaseStock
router.post("/proceed", async (req, res) => {
  try {
    const { password, categoryCode, jobSheetNo, forceNew } = req.body;

    if (!password || !String(password).trim()) {
      return res.status(400).json({ success: false, message: "Password is required." });
    }

    const pool = req.pool;
    const trimmedPass = String(password).trim();
    const selJobSheet = (jobSheetNo || "").trim();

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
    let authRes = await salesmanReq.query(salesmanQuery);

    let userRole = "";
    let userName = "";

    if (authRes.recordset && authRes.recordset.length > 0) {
      const sm = authRes.recordset[0];
      userRole = sm.SalesManName ? sm.SalesManName.trim() : (sm.SalesManCode ? sm.SalesManCode.trim() : "");
      userName = userRole;
    } else {
      const uReq = pool.request();
      uReq.input("password", sql.VarChar, trimmedPass);
      const uRes = await uReq.query(`
        SELECT TOP 1 RTRIM(UserCode) AS UserCode, RTRIM(UserName) AS UserName 
        FROM tblUserMaster WHERE RTRIM(Password) = @password OR RTRIM(UserCode) = @password
      `);
      if (uRes.recordset && uRes.recordset.length > 0) {
        userRole = uRes.recordset[0].UserName ? uRes.recordset[0].UserName.trim() : "Admin";
        userName = userRole;
      } else {
        return res.status(401).json({
          success: false,
          message: "Invalid Purchase Take Password.",
        });
      }
    }

    const isAll = !categoryCode || categoryCode.trim().toUpperCase() === "ALL";
    const cat = isAll ? "" : categoryCode.trim();

    // Check if an active purchase take session already exists in tblPurchaseStock
    if (forceNew === undefined || forceNew === null) {
      const checkReq = pool.request();
      checkReq.input("categoryCode", sql.VarChar, cat);
      let checkQuery = `
        SELECT COUNT(*) AS ScannedCount
        FROM tblPurchaseStock
        WHERE (Scanned = 1 OR (TrayID IS NOT NULL AND RTRIM(TrayID) <> '' AND RTRIM(TrayID) <> 'X'))
          AND (@categoryCode = '' OR RTRIM(CategoryCode) = RTRIM(@categoryCode))
      `;
      if (selJobSheet && selJobSheet !== "ALL") {
        checkReq.input("jobSheetNo", sql.VarChar, selJobSheet);
        checkQuery += ` AND RTRIM(JobSheetNo) = RTRIM(@jobSheetNo)`;
      }
      const activeCheck = await checkReq.query(checkQuery);
      const scannedCount = activeCheck.recordset[0] ? Number(activeCheck.recordset[0].ScannedCount) || 0 : 0;
      if (scannedCount > 0) {
        const dbState = await fetchDatabaseState(pool, categoryCode, "", selJobSheet);
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

    // SQL Transaction for Purchase Stock initialization (CheckPurchaseStock -> PurchaseStockDelete -> subCreatePurchaseStock)
    const transaction = new sql.Transaction(pool);
    await transaction.begin();

    try {
      // If forceNew is true (NEW Purchase Take chosen by user): Reset existing scanned items back to 'X' and Scanned = 0
      if (forceNew === true) {
        const resetReq = new sql.Request(transaction);
        resetReq.input("categoryCode", sql.VarChar, cat);
        resetReq.input("userName", sql.VarChar, userName);
        let resetQuery = `
          UPDATE tblPurchaseStock
          SET 
            TrayID = 'X',
            Scanned = 0,
            ModifyUser = @userName,
            ModifyDate = GETDATE()
          WHERE (@categoryCode = '' OR RTRIM(CategoryCode) = RTRIM(@categoryCode))
        `;
        if (selJobSheet && selJobSheet !== "ALL") {
          resetReq.input("jobSheetNo", sql.VarChar, selJobSheet);
          resetQuery += ` AND RTRIM(JobSheetNo) = RTRIM(@jobSheetNo)`;
        }
        await resetReq.query(resetQuery);
      }

      // Check count in tblPurchaseStock; if empty, populate from tblJobSheetDetail (subCreatePurchaseStock)
      const countReq = new sql.Request(transaction);
      countReq.input("categoryCode", sql.VarChar, cat);
      let countQuery = `SELECT COUNT(*) AS TotalCount FROM tblPurchaseStock WHERE 1=1`;
      if (selJobSheet && selJobSheet !== "ALL") {
        countReq.input("jobSheetNo", sql.VarChar, selJobSheet);
        countQuery += ` AND RTRIM(JobSheetNo) = RTRIM(@jobSheetNo)`;
      }
      const countRes = await countReq.query(countQuery);
      const totalCount = countRes.recordset[0] ? Number(countRes.recordset[0].TotalCount) || 0 : 0;

      if (totalCount === 0) {
        const popReq = new sql.Request(transaction);
        popReq.input("categoryCode", sql.VarChar, cat);
        popReq.input("userName", sql.VarChar, userName);

        let popQuery = `
          INSERT INTO tblPurchaseStock (
            JobSheetNo, CategoryCode, TrayID, InventoryCode, CatalogueCode, Description, Weight, CostPerGram, PCS, TotalCost, ModifyUser, ModifyDate, Scanned
          )
          SELECT 
            RTRIM(d.JobSheetNo),
            RTRIM(d.CategoryCode),
            'X',
            RTRIM(d.JobSheetNo) + '-' + CAST(ROW_NUMBER() OVER(ORDER BY d.CatalogueCode) AS varchar(10)),
            RTRIM(d.CatalogueCode),
            RTRIM(d.Description),
            ISNULL(d.Weight, 0),
            ISNULL(d.CostPerGram, 0),
            ISNULL(d.PCS, 1),
            ISNULL(d.TotalCost, 0),
            @userName,
            GETDATE(),
            0
          FROM tblJobSheetDetail d
          WHERE (@categoryCode = '' OR RTRIM(d.CategoryCode) = RTRIM(@categoryCode))
        `;
        if (selJobSheet && selJobSheet !== "ALL") {
          popReq.input("jobSheetNo", sql.VarChar, selJobSheet);
          popQuery += ` AND RTRIM(d.JobSheetNo) = RTRIM(@jobSheetNo)`;
        }
        await popReq.query(popQuery);
      }

      // UpdatePurchaseModifyUser equivalent
      const touchReq = new sql.Request(transaction);
      touchReq.input("categoryCode", sql.VarChar, cat);
      touchReq.input("userName", sql.VarChar, userName);
      let touchQuery = `
        UPDATE tblPurchaseStock
        SET 
          ModifyUser = @userName,
          ModifyDate = GETDATE()
        WHERE (@categoryCode = '' OR RTRIM(CategoryCode) = RTRIM(@categoryCode))
      `;
      if (selJobSheet && selJobSheet !== "ALL") {
        touchReq.input("jobSheetNo", sql.VarChar, selJobSheet);
        touchQuery += ` AND RTRIM(JobSheetNo) = RTRIM(@jobSheetNo)`;
      }
      await touchReq.query(touchQuery);

      await transaction.commit();
    } catch (txErr) {
      await transaction.rollback();
      throw txErr;
    }

    currentPurchaseSession = {
      isPrepared: true,
      jobSheetNo: selJobSheet,
      categoryCode: categoryCode || "ALL",
      userRole,
      userName,
    };

    const { summary, scannedList } = await fetchDatabaseState(pool, currentPurchaseSession.categoryCode, "", selJobSheet);

    res.json({
      success: true,
      message: forceNew ? "New Purchase Take session started." : "Purchase Take session initialized from database.",
      userRole,
      userName,
      summary,
      scannedList,
    });
  } catch (err) {
    console.error("Purchase Take Proceed Error:", err);
    res.status(500).json({
      success: false,
      message: "Failed to proceed with Purchase Take preparation.",
      error: err.message,
    });
  }
});

// POST /api/purchasetake/scan
// Purchase Scan API (exclusively on tblPurchaseStock and common tblTrayMaster validation)
router.post("/scan", async (req, res) => {
  try {
    const { trayId, inventoryCode } = req.body;

    if (!trayId || !inventoryCode) {
      return res.status(400).json({
        success: false,
        message: "Both Tray ID and Inventory Code are required for Purchase Take scan.",
      });
    }

    if (!currentPurchaseSession.isPrepared) {
      return res.status(400).json({
        success: false,
        message: "Purchase Take session is not active. Please complete Download Purchase Take & Proceed setup first.",
      });
    }

    const trimmedTray = trayId.trim();
    const trimmedCode = inventoryCode.trim();


    const pool = req.pool;

    // 1. TRAY ID VALIDATION: Verify entered Tray ID against COMMON tblTrayMaster (match TrayID or TrayName)
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

    // 2. INVENTORY CODE VALIDATION on tblPurchaseStock ONLY (GetInventoryFromStockPurchase)
    const transaction = new sql.Transaction(pool);
    await transaction.begin();

    try {
      const checkReq = new sql.Request(transaction);
      checkReq.input("inventoryCode", sql.VarChar, trimmedCode);
      let checkQuery = `
        SELECT TOP 1
          ID,
          RTRIM(TrayID) AS TrayID,
          RTRIM(InventoryCode) AS InventoryCode,
          RTRIM(CatalogueCode) AS CatalogueCode,
          RTRIM(Description) AS Description,
          Weight,
          CostPerGram,
          TotalCost,
          RTRIM(CategoryCode) AS CategoryCode,
          PCS
        FROM tblPurchaseStock WITH (UPDLOCK, HOLDLOCK)
        WHERE (UPPER(RTRIM(InventoryCode)) = UPPER(RTRIM(@inventoryCode)) OR UPPER(RTRIM(CatalogueCode)) = UPPER(RTRIM(@inventoryCode)))
      `;
      if (currentPurchaseSession.jobSheetNo && currentPurchaseSession.jobSheetNo !== "ALL") {
        checkReq.input("jobSheetNo", sql.VarChar, currentPurchaseSession.jobSheetNo);
        checkQuery += ` AND RTRIM(JobSheetNo) = RTRIM(@jobSheetNo)`;
      }

      let existingRes = await checkReq.query(checkQuery);

      if (!existingRes.recordset || existingRes.recordset.length === 0) {
        // Fallback: Check tblInventory, tblStock, or tblJobSheetDetail if item exists elsewhere
        const fbReq = new sql.Request(transaction);
        fbReq.input("inventoryCode", sql.VarChar, trimmedCode);
        
        let invMatch = await fbReq.query(`
          SELECT TOP 1
            RTRIM(InventoryCode) AS InventoryCode,
            RTRIM(CatalogueCode) AS CatalogueCode,
            RTRIM(Description) AS Description,
            ISNULL(Weight, 0) AS Weight,
            ISNULL(CostPerGram, 0) AS CostPerGram,
            ISNULL(TotalCost, 0) AS TotalCost,
            RTRIM(CategoryCode) AS CategoryCode,
            ISNULL(JobSheetNo, '') AS JobSheetNo
          FROM tblInventory
          WHERE (UPPER(RTRIM(InventoryCode)) = UPPER(RTRIM(@inventoryCode)) OR UPPER(RTRIM(CatalogueCode)) = UPPER(RTRIM(@inventoryCode)))
        `);

        if (!invMatch.recordset || invMatch.recordset.length === 0) {
          invMatch = await fbReq.query(`
            SELECT TOP 1
              RTRIM(InventoryCode) AS InventoryCode,
              RTRIM(CatalogueCode) AS CatalogueCode,
              RTRIM(Description) AS Description,
              ISNULL(Weight, 0) AS Weight,
              ISNULL(CostPerGram, 0) AS CostPerGram,
              ISNULL(TotalCost, 0) AS TotalCost,
              RTRIM(CategoryCode) AS CategoryCode,
              '' AS JobSheetNo
            FROM tblStock
            WHERE (UPPER(RTRIM(InventoryCode)) = UPPER(RTRIM(@inventoryCode)) OR UPPER(RTRIM(CatalogueCode)) = UPPER(RTRIM(@inventoryCode)))
          `);
        }

        if (!invMatch.recordset || invMatch.recordset.length === 0) {
          invMatch = await fbReq.query(`
            SELECT TOP 1
              RTRIM(JobSheetNo) + '-' + CAST(ID AS varchar(10)) AS InventoryCode,
              RTRIM(CatalogueCode) AS CatalogueCode,
              RTRIM(Description) AS Description,
              ISNULL(Weight, 0) AS Weight,
              ISNULL(CostPerGram, 0) AS CostPerGram,
              ISNULL(TotalCost, 0) AS TotalCost,
              RTRIM(CategoryCode) AS CategoryCode,
              RTRIM(JobSheetNo) AS JobSheetNo
            FROM tblJobSheetDetail
            WHERE (UPPER(RTRIM(CatalogueCode)) = UPPER(RTRIM(@inventoryCode)) OR UPPER(RTRIM(JobSheetNo)) = UPPER(RTRIM(@inventoryCode)))
          `);
        }

        if (invMatch.recordset && invMatch.recordset.length > 0) {
          const item = invMatch.recordset[0];
          const insReq = new sql.Request(transaction);
          insReq.input("jobSheetNo", sql.VarChar, item.JobSheetNo || currentPurchaseSession.jobSheetNo || "");
          insReq.input("categoryCode", sql.VarChar, item.CategoryCode || "");
          insReq.input("trayId", sql.VarChar, matchedTray || trimmedTray);
          insReq.input("inventoryCode", sql.VarChar, item.InventoryCode || trimmedCode);
          insReq.input("catalogueCode", sql.VarChar, item.CatalogueCode || "");
          insReq.input("description", sql.VarChar, item.Description || "");
          insReq.input("weight", sql.Float, Number(item.Weight) || 0);
          insReq.input("costPerGram", sql.Float, Number(item.CostPerGram) || 0);
          insReq.input("totalCost", sql.Float, Number(item.TotalCost) || 0);
          insReq.input("userName", sql.VarChar, currentPurchaseSession.userName || "SYSTEM");

          const insRes = await insReq.query(`
            INSERT INTO tblPurchaseStock (
              JobSheetNo, CategoryCode, TrayID, InventoryCode, CatalogueCode, Description, Weight, CostPerGram, PCS, TotalCost, ModifyUser, ModifyDate, Scanned
            )
            OUTPUT INSERTED.ID, RTRIM(INSERTED.TrayID) AS TrayID, RTRIM(INSERTED.InventoryCode) AS InventoryCode, RTRIM(INSERTED.CatalogueCode) AS CatalogueCode, RTRIM(INSERTED.Description) AS Description, INSERTED.Weight, INSERTED.CostPerGram, INSERTED.TotalCost, RTRIM(INSERTED.CategoryCode) AS CategoryCode, INSERTED.PCS
            VALUES (
              @jobSheetNo, @categoryCode, @trayId, @inventoryCode, @catalogueCode, @description, @weight, @costPerGram, 1, @totalCost, @userName, GETDATE(), 1
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

      // 3. ALREADY SCANNED CHECK in tblPurchaseStock
      // Note: Only reject if currentTrayID is NOT "X" and NOT empty/null
      if (currentTrayID !== "" && currentTrayID.toUpperCase() !== "X") {
        await transaction.rollback();
        const trayItems = await fetchTrayItems(pool, trimmedTray, currentPurchaseSession.jobSheetNo);
        const summary = await fetchTrayTotals(pool, trimmedTray, currentPurchaseSession.jobSheetNo);

        return res.json({
          success: false,
          alreadyScanned: true,
          message: `${trimmedCode} Already Scanned`,
          selectedTrayId: trimmedTray,
          scannedList: trayItems,
          summary,
        });
      }

      // 4. UpdatePurchaseStock and tblPurchase
      const updateReq = new sql.Request(transaction);
      updateReq.input("trayId", sql.VarChar, matchedTray || trimmedTray);
      updateReq.input("id", sql.Int, row.ID);
      updateReq.input("userName", sql.VarChar, currentPurchaseSession.userName || "SYSTEM");
      const updateRes = await updateReq.query(`
        UPDATE tblPurchaseStock
        SET 
          TrayID = @trayId,
          Scanned = 1,
          ModifyUser = @userName,
          ModifyDate = GETDATE()
        WHERE ID = @id
      `);

      // Also update tblPurchase table
      const updatePurReq = new sql.Request(transaction);
      updatePurReq.input("trayId", sql.VarChar, matchedTray || trimmedTray);
      updatePurReq.input("inventoryCode", sql.VarChar, trimmedCode);
      updatePurReq.input("userName", sql.VarChar, currentPurchaseSession.userName || "SYSTEM");
      await updatePurReq.query(`
        UPDATE tblPurchase
        SET 
          TrayId = @trayId,
          ModifyUser = @userName,
          ModifyDate = GETDATE()
        WHERE UPPER(RTRIM(InventoryCode)) = UPPER(RTRIM(@inventoryCode)) OR UPPER(RTRIM(CatalogueCode)) = UPPER(RTRIM(@inventoryCode))
      `);

      const affected = updateRes.rowsAffected ? updateRes.rowsAffected[0] : 0;
      if (affected === 0) {
        await transaction.rollback();
        return res.status(400).json({
          success: false,
          message: `Purchase Stock update failed for item ${trimmedCode}. No rows modified.`,
        });
      }

      await transaction.commit();
    } catch (txErr) {
      await transaction.rollback();
      throw txErr;
    }

    // Read exact updated item from tblPurchaseStock
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
      FROM tblPurchaseStock
      WHERE (RTRIM(InventoryCode) = RTRIM(@inventoryCode) OR RTRIM(CatalogueCode) = RTRIM(@inventoryCode))
        AND RTRIM(TrayID) = RTRIM(@trayId)
    `);

    const updatedRow = readRes.recordset ? readRes.recordset[0] : null;
    const weight = updatedRow && updatedRow.Weight != null ? Number(updatedRow.Weight) || 0 : 0;
    const totalCost = updatedRow && updatedRow.TotalCost != null ? Number(updatedRow.TotalCost) || 0 : 0;
    const costPerGram =
      updatedRow && updatedRow.CostPerGram != null && Number(updatedRow.CostPerGram) > 0
        ? Number(updatedRow.CostPerGram)
        : weight > 0
          ? totalCost / weight
          : 0;

    const updatedItem = {
      trayId: updatedRow && updatedRow.TrayID ? updatedRow.TrayID.trim() : trimmedTray,
      inventoryCode: updatedRow && updatedRow.InventoryCode ? updatedRow.InventoryCode.trim() : trimmedCode,
      catalogueCode: updatedRow && updatedRow.CatalogueCode ? updatedRow.CatalogueCode.trim() : "",
      description: updatedRow && updatedRow.Description ? updatedRow.Description.trim() : "",
      weight,
      costPerGram,
      totalCost,
      categoryCode: updatedRow && updatedRow.CategoryCode ? updatedRow.CategoryCode.trim() : "",
      pcs: updatedRow && updatedRow.PCS != null ? Number(updatedRow.PCS) || 0 : 1,
      scanned: true,
    };

    const trayItems = await fetchTrayItems(pool, trimmedTray, currentPurchaseSession.jobSheetNo);
    const summary = await fetchTrayTotals(pool, trimmedTray, currentPurchaseSession.jobSheetNo);

    return res.json({
      success: true,
      message: `Item ${trimmedCode} assigned to Tray ${trimmedTray}.`,
      item: updatedItem,
      selectedTrayId: trimmedTray,
      scannedList: trayItems,
      summary,
    });
  } catch (err) {
    console.error("Purchase Take Scan Error:", err);
    res.status(500).json({
      success: false,
      message: "Failed to save Purchase Take entry to database.",
      error: err.message,
    });
  }
});

let isSaveInProgress = false;

// POST /api/purchasetake/save
router.post("/save", async (req, res) => {
  try {
    if (!currentPurchaseSession.isPrepared) {
      return res.status(400).json({
        success: false,
        message: "No active Purchase Take session to save. Please start a session first.",
      });
    }

    if (isSaveInProgress) {
      return res.status(409).json({
        success: false,
        message: "A Purchase Take save operation is currently in progress. Please wait.",
      });
    }

    isSaveInProgress = true;
    currentPurchaseSession.isPrepared = false;

    const pool = req.pool;
    const { summary } = await fetchDatabaseState(pool, currentPurchaseSession.categoryCode, "", currentPurchaseSession.jobSheetNo);

    isSaveInProgress = false;
    return res.json({
      success: true,
      message: "Purchase Take saved to database successfully!",
      summary,
      scannedList: [],
      selectedTrayId: "",
    });
  } catch (err) {
    isSaveInProgress = false;
    console.error("Purchase Take Save Error:", err);
    return res.status(500).json({
      success: false,
      message: err.message || "Failed to save Purchase Take to database.",
    });
  }
});

// GET /api/purchasetake/summary
router.get("/summary", async (req, res) => {
  try {
    const pool = req.pool;
    const { summary, scannedList } = await fetchDatabaseState(pool, currentPurchaseSession.categoryCode, "", currentPurchaseSession.jobSheetNo);
    res.json({
      success: true,
      isPrepared: currentPurchaseSession.isPrepared,
      summary,
      scannedList,
    });
  } catch (err) {
    res.status(500).json({
      success: false,
      message: "Failed to fetch Purchase Take summary from database.",
      error: err.message,
    });
  }
});

// GET /api/purchasetake/list?trayId=...
router.get("/list", async (req, res) => {
  try {
    const { trayId } = req.query;

    if (!trayId || !String(trayId).trim()) {
      return res.status(400).json({
        success: false,
        message: "Please enter Tray ID to list purchase items.",
      });
    }

    const trimmedTray = String(trayId).trim();


    const pool = req.pool;

    // Validate Tray ID in tblTrayMaster (match TrayID or TrayName)
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
    const scannedList = await fetchTrayItems(pool, targetTray, currentPurchaseSession.jobSheetNo);
    const summary = await fetchTrayTotals(pool, targetTray, currentPurchaseSession.jobSheetNo);

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
    console.error("Purchase Take List Error:", err);
    res.status(500).json({
      success: false,
      message: "Failed to fetch purchase take list for specified Tray ID.",
      error: err.message,
    });
  }
});

// DELETE /api/purchasetake/item/:inventoryCode
async function handleDeletePurchaseItem(req, res) {
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

    const transaction = new sql.Transaction(pool);
    await transaction.begin();

    try {
      const deleteReq = new sql.Request(transaction);
      deleteReq.input("inventoryCode", sql.VarChar, trimmedCode);
      deleteReq.input("userName", sql.VarChar, currentPurchaseSession.userName || "SYSTEM");
      await deleteReq.query(`
        UPDATE tblPurchaseStock
        SET 
          TrayID = 'X',
          Scanned = 0,
          ModifyUser = @userName,
          ModifyDate = GETDATE()
        WHERE RTRIM(InventoryCode) = RTRIM(@inventoryCode) OR RTRIM(CatalogueCode) = RTRIM(@inventoryCode)
      `);
      await transaction.commit();
    } catch (txErr) {
      await transaction.rollback();
      throw txErr;
    }

    let summary, scannedList;
    if (trayId && String(trayId).trim() && String(trayId).trim().toUpperCase() !== "X") {
      const trimmedTray = String(trayId).trim();
      scannedList = await fetchTrayItems(pool, trimmedTray, currentPurchaseSession.jobSheetNo);
      summary = await fetchTrayTotals(pool, trimmedTray, currentPurchaseSession.jobSheetNo);
    } else {
      const state = await fetchDatabaseState(pool, currentPurchaseSession.categoryCode, "", currentPurchaseSession.jobSheetNo);
      summary = state.summary;
      scannedList = state.scannedList;
    }

    return res.json({
      success: true,
      message: `Item ${trimmedCode} removed from Purchase Take.`,
      scannedList,
      summary,
    });
  } catch (err) {
    console.error("Delete Purchase Item Error:", err);
    res.status(500).json({
      success: false,
      message: "Failed to delete item from database.",
      error: err.message,
    });
  }
}

// POST /api/purchasetake/update-item & PUT /api/purchasetake/item/:inventoryCode
async function handleUpdatePurchaseItem(req, res) {
  try {
    const inventoryCode = req.params.inventoryCode || (req.body ? req.body.inventoryCode : "");
    const { trayId, weight, pcs, currentViewTrayId } = req.body || {};

    if (!inventoryCode || !String(inventoryCode).trim()) {
      return res.status(400).json({
        success: false,
        message: "Inventory Code is required to update item.",
      });
    }

    const trimmedCode = String(inventoryCode).trim();
    const newTrayId = trayId !== undefined && trayId !== null ? String(trayId).trim() : "X";
    const newWeight = weight != null && weight !== "" ? Number(weight) || 0 : null;
    const newPcs = pcs != null && pcs !== "" ? Number(pcs) || 1 : null;
    const pool = req.pool;

    if (newTrayId.toUpperCase() !== "X" && newTrayId !== "") {
      const trayCheckReq = pool.request();
      trayCheckReq.input("trayId", sql.VarChar, newTrayId);
      const trayRes = await trayCheckReq.query(`
        SELECT TOP 1 RTRIM(TrayID) AS TrayID
        FROM tblTrayMaster
        WHERE UPPER(RTRIM(TrayID)) = UPPER(RTRIM(@trayId))
      `);
      if (!trayRes.recordset || trayRes.recordset.length === 0) {
        return res.status(400).json({
          success: false,
          message: "Tray Not Found!",
        });
      }
    }

    const transaction = new sql.Transaction(pool);
    await transaction.begin();

    try {
      const updateReq = new sql.Request(transaction);
      updateReq.input("inventoryCode", sql.VarChar, trimmedCode);
      updateReq.input("trayId", sql.VarChar, newTrayId);
      updateReq.input("weight", sql.Numeric(18, 4), newWeight);
      updateReq.input("pcs", sql.Numeric(18, 0), newPcs);
      updateReq.input("userName", sql.VarChar, currentPurchaseSession.userName || "SYSTEM");

      const updateRes = await updateReq.query(`
        UPDATE tblPurchaseStock
        SET 
          TrayID = @trayId,
          Weight = COALESCE(@weight, Weight),
          PCS = COALESCE(@pcs, PCS),
          ModifyUser = @userName,
          ModifyDate = GETDATE()
        WHERE RTRIM(InventoryCode) = RTRIM(@inventoryCode) OR RTRIM(CatalogueCode) = RTRIM(@inventoryCode)
      `);

      const affected = updateRes.rowsAffected ? updateRes.rowsAffected[0] : 0;
      if (affected === 0) {
        await transaction.rollback();
        return res.status(404).json({
          success: false,
          message: `Purchase item ${trimmedCode} not found in database.`,
        });
      }

      await transaction.commit();
    } catch (txErr) {
      await transaction.rollback();
      throw txErr;
    }

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
        FROM tblPurchaseStock
        WHERE RTRIM(InventoryCode) = RTRIM(@inventoryCode) OR RTRIM(CatalogueCode) = RTRIM(@inventoryCode)
      `);
    const updatedRow = updatedRowRes.recordset[0] || {};
    const updatedTrayId = updatedRow.TrayID || (newTrayId.toUpperCase() !== "X" ? newTrayId : "");

    let summary, scannedList;
    if (updatedTrayId && updatedTrayId.toUpperCase() !== "X") {
      scannedList = await fetchTrayItems(pool, updatedTrayId, currentPurchaseSession.jobSheetNo);
      summary = await fetchTrayTotals(pool, updatedTrayId, currentPurchaseSession.jobSheetNo);
    } else {
      const state = await fetchDatabaseState(pool, currentPurchaseSession.categoryCode, "", currentPurchaseSession.jobSheetNo);
      summary = state.summary;
      scannedList = state.scannedList;
    }

    const updatedItem = {
      trayId: updatedTrayId,
      inventoryCode: updatedRow.InventoryCode || trimmedCode,
      description: updatedRow.Description || "",
      weight: Number(updatedRow.Weight) || 0,
      costPerGram: Number(updatedRow.CostPerGram) || 0,
      pcs: Number(updatedRow.PCS) || 0,
      totalCost: Number(updatedRow.TotalCost) || 0,
    };

    return res.json({
      success: true,
      message: `Item ${trimmedCode} updated in database successfully!`,
      updatedItem,
      scannedList,
      summary,
    });
  } catch (err) {
    console.error("Update Purchase Item Error:", err);
    res.status(500).json({
      success: false,
      message: "Failed to update item in database.",
      error: err.message,
    });
  }
}

router.delete("/item/:inventoryCode", handleDeletePurchaseItem);
router.post("/delete-item", handleDeletePurchaseItem);
router.post("/update-item", handleUpdatePurchaseItem);
router.put("/item/:inventoryCode", handleUpdatePurchaseItem);

module.exports = router;


