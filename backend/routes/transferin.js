const express = require("express");
const router = express.Router();
const { sql, getPool: dbGetPool } = require("../db");

// Helper to acquire database connection pool reliably
const getPool = async (req) => {
  if (req && req.pool) {
    if (req.pool.connected && !req.pool.closed) {
      return req.pool;
    }
  }
  return await dbGetPool(process.env.DB_NAME || 'smartgold');
};

// Helper to inspect table columns dynamically
async function getTableColumns(pool, tableName) {
  try {
    const res = await pool.request()
      .input("tbl", sql.VarChar(128), tableName)
      .query(`
        SELECT COLUMN_NAME
        FROM INFORMATION_SCHEMA.COLUMNS
        WHERE TABLE_NAME = @tbl
      `);
    return res.recordset.map(r => r.COLUMN_NAME.toLowerCase());
  } catch (err) {
    console.warn(`Could not inspect columns for ${tableName}:`, err.message);
    return [];
  }
}

// Helper to inspect table column data types
async function getTableColumnTypes(pool, tableName) {
  try {
    const res = await pool.request()
      .input("tbl", sql.VarChar(128), tableName)
      .query(`
        SELECT COLUMN_NAME, DATA_TYPE
        FROM INFORMATION_SCHEMA.COLUMNS
        WHERE TABLE_NAME = @tbl
      `);
    const map = {};
    for (const r of res.recordset) {
      map[r.COLUMN_NAME.toLowerCase()] = r.DATA_TYPE.toLowerCase();
    }
    return map;
  } catch (err) {
    console.warn(`Could not inspect column types for ${tableName}:`, err.message);
    return {};
  }
}

function isNumericType(dt) {
  if (!dt) return false;
  return ['int', 'smallint', 'tinyint', 'bigint', 'decimal', 'numeric', 'float', 'real', 'money', 'smallmoney'].includes(dt);
}

// 1. GET /api/transferin/pending-list
// Browse inbound pending transfers matching user destination location
// In VB: select * from tblInventoryTransferHeader where ToLocStatus = '0' and FromLocStatus = '2'
router.get("/pending-list", async (req, res) => {
  try {
    const { location, search } = req.query;
    const pool = await getPool(req);

    let query = `
      SELECT TOP 100
        RTRIM(H.TransferNo) AS TransferNo,
        H.TransferDate,
        RTRIM(H.FromLocation) AS FromLocation,
        RTRIM(H.ToLocation) AS ToLocation,
        ISNULL(H.TotalPcs, 0) AS TotalPcs,
        ISNULL(H.TotalWeight, 0) AS TotalWeight,
        ISNULL(H.TotalCost, 0) AS TotalCost,
        RTRIM(ISNULL(H.Remarks, '')) AS Remarks,
        H.FromLocStatus,
        H.ToLocStatus
      FROM tblInventoryTransferHeader H
      WHERE H.ToLocStatus = 0 OR RTRIM(CAST(H.ToLocStatus AS VARCHAR(10))) = '0'
    `;

    const request = pool.request();

    if (location && location.trim() && location.trim().toUpperCase() !== 'HQ') {
      query += ` AND (RTRIM(H.ToLocation) = @Location OR RTRIM(H.ToLocation) = '' OR H.ToLocation IS NULL)`;
      request.input("Location", sql.VarChar(50), location.trim());
    }

    if (search && search.trim()) {
      query += ` AND (RTRIM(H.TransferNo) LIKE @Search OR RTRIM(H.FromLocation) LIKE @Search OR RTRIM(H.Remarks) LIKE @Search)`;
      request.input("Search", sql.VarChar(100), `%${search.trim()}%`);
    }

    query += ` ORDER BY H.TransferDate DESC, H.TransferNo DESC`;

    const result = await request.query(query);
    res.json({ success: true, list: result.recordset });
  } catch (err) {
    console.error("Error fetching pending transfer list:", err);
    res.status(500).json({ success: false, message: "Error fetching pending transfer list: " + err.message });
  }
});

// 2. GET /api/transferin/fetch
// Matching fncValidateTransferNo & fncTransferInventoryToLocal & fncFetchTransferDetails in frmTransferIn.vb
router.get("/fetch", async (req, res) => {
  try {
    const { transferNo, location } = req.query;
    if (!transferNo || !transferNo.trim()) {
      return res.status(400).json({ success: false, message: "Please enter the transfer no." });
    }

    const pool = await getPool(req);
    const cleanTransferNo = transferNo.trim();

    // 1. Fetch Header
    let headerQuery = `
      SELECT 
        RTRIM(TransferNo) as TransferNo, 
        TransferDate,
        RTRIM(FromLocation) as FromLocation, 
        RTRIM(ToLocation) as ToLocation, 
        RTRIM(ISNULL(Remarks, '')) as Remarks,
        ISNULL(TotalWeight, 0) as TotalWeight,
        ISNULL(TotalCost, 0) as TotalCost,
        ISNULL(TotalPcs, 0) as TotalPcs,
        ISNULL(TotalPMC, 0) as TotalPMC,
        ISNULL(TotalSMC, 0) as TotalSMC,
        FromLocStatus,
        ToLocStatus
      FROM tblInventoryTransferHeader
      WHERE RTRIM(LTRIM(TransferNo)) = @TransferNo
    `;

    const headerReq = pool.request().input("TransferNo", sql.VarChar(50), cleanTransferNo);

    // If destination location is specified and not HQ, verify ToLocation
    if (location && location.trim() && location.trim().toUpperCase() !== 'HQ') {
      headerQuery += ` AND (RTRIM(ToLocation) = @ToLocation OR RTRIM(ToLocation) = '' OR ToLocation IS NULL)`;
      headerReq.input("ToLocation", sql.VarChar(50), location.trim());
    }

    let headerResult;
    try {
      headerResult = await pool.request()
        .input("TransferNo", sql.VarChar(50), cleanTransferNo)
        .query(`SELECT TOP 1 * FROM tblInventoryTransferHeader WHERE RTRIM(LTRIM(TransferNo)) = @TransferNo`);
    } catch (hErr) {
      console.error("Header Query Error:", hErr);
      return res.status(500).json({ success: false, message: "Header Query Error: " + hErr.message });
    }

    if (headerResult.recordset.length === 0) {
      // Let's check whether it exists with a different destination
      const checkExists = await pool.request()
        .input("TransferNo", sql.VarChar(50), cleanTransferNo)
        .query(`SELECT RTRIM(ToLocation) AS ToLocation FROM tblInventoryTransferHeader WHERE RTRIM(LTRIM(TransferNo)) = @TransferNo`);

      if (checkExists.recordset.length > 0) {
        return res.json({ 
          success: false, 
          message: `Transfer No '${cleanTransferNo}' is designated for Location '${checkExists.recordset[0].ToLocation}', not '${location}'.` 
        });
      }

      return res.json({ success: false, message: "Enter valid Transfer No. Transfer docket not found." });
    }

    const header = headerResult.recordset[0];

    // Status checks matching fncCheckAcceptanceStatus in frmTransferIn.vb:
    // If ToLocStatus = 2 or '2', transfer is already accepted
    const isToLocAccepted = header.ToLocStatus == 2 || String(header.ToLocStatus).trim() === '2';
    if (isToLocAccepted) {
      return res.json({
        success: false,
        message: "Transfer Already Accepted.",
        isAlreadyAccepted: true,
        header
      });
    }

    // 2. Fetch Details matching tblInventoryTransferDetail
    // Fetch columns cleanly without any unsupported conversions
    const detailQuery = `
      SELECT 
        RTRIM(InventoryCode) AS InventoryCode,
        RTRIM(ISNULL(Description, '')) AS Description,
        RTRIM(ISNULL(ClassCode, '')) AS ClassCode,
        RTRIM(ISNULL(CategoryCode, '')) AS CategoryCode,
        RTRIM(ISNULL(CatalogueCode, '')) AS CatalogueCode,
        ISNULL(Weight, 0) AS Weight,
        RTRIM(ISNULL(SizeCode, '')) AS SizeCode,
        ISNULL(QtyonHand, 0) AS QtyOnHand,
        ISNULL(CostPerGram, 0) AS CostPerGram,
        ISNULL(PMC, 0) AS PMC,
        ISNULL(SMC, 0) AS SMC,
        ISNULL(TotalCost, 0) AS TotalCost
      FROM tblInventoryTransferDetail
      WHERE RTRIM(LTRIM(TransferNo)) = @TransferNo
    `;

    let detailResult;
    try {
      detailResult = await pool.request()
        .input("TransferNo", sql.VarChar(50), cleanTransferNo)
        .query(`SELECT * FROM tblInventoryTransferDetail WHERE RTRIM(LTRIM(TransferNo)) = @TransferNo`);
    } catch (dErr) {
      console.error("Detail Query Error:", dErr);
      return res.status(500).json({ success: false, message: "Detail Query Error: " + dErr.message });
    }

    if (detailResult.recordset.length === 0) {
      return res.json({ success: false, message: "No data found in transfer detail items." });
    }

    // Map rows cleanly with TransferTag = false and PCS fallback
    const mappedDetails = detailResult.recordset.map(item => ({
      ...item,
      PCS: item.QtyOnHand || 1,
      TransferTag: false
    }));

    res.json({ 
      success: true, 
      data: {
        header: header,
        details: mappedDetails
      }
    });
  } catch (err) {
    console.error("Fetch Transfer In Error:", err);
    res.status(500).json({ success: false, message: "Database error: " + err.message });
  }
});

// 3. POST /api/transferin/verify-salesman
// Matching subGetPassword in frmTransferIn.vb
router.post("/verify-salesman", async (req, res) => {
  try {
    const { password } = req.body;
    if (!password || !password.trim()) {
      return res.status(400).json({ success: false, message: "Password is required" });
    }

    const pool = await getPool(req);
    const result = await pool.request()
      .input("Password", sql.VarChar(100), password.trim())
      .query(`
        SELECT TOP 1 
          RTRIM(SalesmanCode) as SalesmanCode, 
          RTRIM(SalesManName) as SalesManName
        FROM tblSalesman 
        WHERE Password = @Password
      `);

    if (result.recordset.length > 0) {
      return res.json({
        success: true,
        salesman: result.recordset[0]
      });
    } else {
      return res.json({
        success: false,
        message: "SalesMan Password is Incorrect!"
      });
    }
  } catch (error) {
    console.error("Error verifying salesman password:", error);
    res.status(500).json({ success: false, message: "Error verifying salesman password: " + error.message });
  }
});

// 4. POST /api/transferin/accept
// Accept Transfer In
// Matching fncAcceptTransfer & fncInventoryMovementSave in frmTransferIn.vb
// 1. Verifies TransferNo and acceptance status
// 2. Updates tblInventoryTransferHeader: ToLocStatus = '2', ModifyDate = GETDATE(), ModifyUser = @UserId
// 3. Updates tblInventory: LocationCode = @Location, restores QtyOnHand
// 4. Updates tblInventoryStock if present
// 5. Inserts movement into tblInventoryMovement with TransType = 'TI' (Transfer In)
router.post("/accept", async (req, res) => {
  let pool;
  try {
    pool = await getPool(req);
  } catch (poolErr) {
    return res.status(500).json({ success: false, message: poolErr.message });
  }

  const transaction = new sql.Transaction(pool);

  try {
    const { transferNo, userId, location, salesmanCode, items } = req.body;
    
    if (!transferNo || !transferNo.trim()) {
      return res.status(400).json({ success: false, message: "Enter Transfer Number" });
    }
    
    if (!items || !Array.isArray(items) || items.length === 0) {
      return res.status(400).json({ success: false, message: "No items to accept." });
    }

    const cleanTransferNo = transferNo.trim();
    const sLocation = (location || 'HQ').trim();
    const sUser = (userId || 'ADMIN').trim();
    const sSalesman = (salesmanCode || sUser).trim();

    // Check status in database first
    const checkStatusRes = await pool.request()
      .input("TransferNo", sql.VarChar(50), cleanTransferNo)
      .query(`
        SELECT 
          FromLocStatus,
          ToLocStatus
        FROM tblInventoryTransferHeader
        WHERE RTRIM(LTRIM(TransferNo)) = @TransferNo
      `);

    if (checkStatusRes.recordset.length === 0) {
      return res.status(404).json({ success: false, message: "Transfer docket not found." });
    }

    const currentStatus = checkStatusRes.recordset[0];
    if (currentStatus.ToLocStatus == 2 || String(currentStatus.ToLocStatus).trim() === '2') {
      return res.status(400).json({ success: false, message: "Transfer Already Accepted." });
    }

    // Inspect table schema dynamically for tblInventory, tblInventoryStock, tblInventoryMovement, tblInventoryTransferHeader
    const [invCols, stockCols, movCols, movColTypes, headerColTypes, headerCols] = await Promise.all([
      getTableColumns(pool, 'tblInventory'),
      getTableColumns(pool, 'tblInventoryStock'),
      getTableColumns(pool, 'tblInventoryMovement'),
      getTableColumnTypes(pool, 'tblInventoryMovement'),
      getTableColumnTypes(pool, 'tblInventoryTransferHeader'),
      getTableColumns(pool, 'tblInventoryTransferHeader')
    ]);

    await transaction.begin();

    // 1. Update tblInventoryTransferHeader status to accepted (2)
    const toLocStatusVal = isNumericType(headerColTypes['tolocstatus']) ? 2 : "2";
    let updateHeaderSql = `
      UPDATE tblInventoryTransferHeader
      SET ToLocStatus = @ToLocStatus, ModifyDate = GETDATE(), ModifyUser = @ModifyUser
    `;
    if (headerCols.includes('salesmancode')) {
      updateHeaderSql += `, SalesManCode = @SalesManCode`;
    }
    updateHeaderSql += ` WHERE RTRIM(LTRIM(TransferNo)) = @TransferNo`;

    const updateHeaderReq = new sql.Request(transaction)
      .input("ToLocStatus", isNumericType(headerColTypes['tolocstatus']) ? sql.Int : sql.VarChar(10), toLocStatusVal)
      .input("ModifyUser", sql.VarChar(50), sUser)
      .input("TransferNo", sql.VarChar(50), cleanTransferNo);

    if (headerCols.includes('salesmancode')) {
      updateHeaderReq.input("SalesManCode", sql.VarChar(50), sSalesman);
    }

    await updateHeaderReq.query(updateHeaderSql);

    // 2. Loop each item: Update tblInventory, tblInventoryStock, and insert tblInventoryMovement (TI)
    const isMovSizeNumeric = isNumericType(movColTypes['sizecode']);

    for (const item of items) {
      const invCode = String(item.InventoryCode || '').trim();
      const itemDesc = (item.Description || '').trim();
      const itemQty = parseInt(item.QtyOnHand || item.Qty || item.PCS || 1, 10) || 1;
      const itemWeight = parseFloat(item.Weight) || 0;
      const itemCostPerGram = parseFloat(item.CostPerGram) || 0;
      const itemPMC = parseFloat(item.PMC) || 0;
      const itemSMC = parseFloat(item.SMC) || 0;
      const itemTotalCost = parseFloat(item.TotalCost) || 0;

      const rawSize = item.SizeCode !== undefined && item.SizeCode !== null ? item.SizeCode : '';
      const sizeNumericVal = parseFloat(rawSize) || 0;
      const sizeStringVal = String(rawSize || '').trim();

      // Check qty column in tblInventory
      let qtyCol = null;
      if (invCols.includes('qtyonhand')) qtyCol = 'QtyOnHand';
      else if (invCols.includes('qty')) qtyCol = 'Qty';
      else if (invCols.includes('pcs')) qtyCol = 'Pcs';
      else if (invCols.includes('quantity')) qtyCol = 'Quantity';

      let updateInvSql = 'UPDATE tblInventory SET ModifyDate = GETDATE(), ModifyUser = @ModifyUser';
      if (invCols.includes('locationcode')) {
        updateInvSql += ', LocationCode = @LocationCode';
      }
      if (qtyCol) {
        updateInvSql += `, ${qtyCol} = ISNULL(${qtyCol}, 0) + @Qty`;
      }
      updateInvSql += ' WHERE RTRIM(LTRIM(CAST(InventoryCode AS VARCHAR(50)))) = @InventoryCode';

      const updateInvReq = new sql.Request(transaction)
        .input("InventoryCode", sql.VarChar(50), invCode)
        .input("LocationCode", sql.VarChar(50), sLocation)
        .input("Qty", sql.Int, itemQty)
        .input("ModifyUser", sql.VarChar(50), sUser);

      await updateInvReq.query(updateInvSql);

      // Update tblInventoryStock if table exists
      if (stockCols.length > 0) {
        let sCol = null;
        if (stockCols.includes('qtyonhand')) sCol = 'QtyOnHand';
        else if (stockCols.includes('qty')) sCol = 'Qty';
        else if (stockCols.includes('pcs')) sCol = 'Pcs';
        else if (stockCols.includes('quantity')) sCol = 'Quantity';

        let updateStockSql = 'UPDATE tblInventoryStock SET ModifyDate = GETDATE()';
        if (stockCols.includes('locationcode')) {
          updateStockSql += ', LocationCode = @LocationCode';
        }
        if (sCol) {
          updateStockSql += `, ${sCol} = ISNULL(${sCol}, 0) + @Qty`;
        }
        updateStockSql += ' WHERE RTRIM(LTRIM(CAST(InventoryCode AS VARCHAR(50)))) = @InventoryCode';

        try {
          await new sql.Request(transaction)
            .input("InventoryCode", sql.VarChar(50), invCode)
            .input("LocationCode", sql.VarChar(50), sLocation)
            .input("Qty", sql.Int, itemQty)
            .query(updateStockSql);
        } catch (eStock) {
          console.warn("tblInventoryStock update warning:", eStock.message);
        }
      }

      // Insert record into tblInventoryMovement (TransType = 'TI')
      if (movCols.length > 0) {
        let locCol = 'LocationCode';
        if (!movCols.includes('locationcode') && movCols.includes('translocation')) {
          locCol = 'TransLocation';
        }

        const movData = {
          InventoryCode: invCode,
          Description: itemDesc,
          TransType: 'TI',
          TransNo: cleanTransferNo,
          TransDate: new Date(),
          Qty: itemQty,
          Weight: itemWeight,
          PMC: itemPMC,
          SMC: itemSMC,
          CostPerGram: itemCostPerGram,
          TotalCost: itemTotalCost,
        };

        if (movCols.includes(locCol.toLowerCase())) {
          movData[locCol] = sLocation;
        }

        if (movCols.includes('sizecode')) {
          movData['SizeCode'] = isMovSizeNumeric ? sizeNumericVal : sizeStringVal;
        }

        // Handle user column
        if (movCols.includes('createuser')) movData['CreateUser'] = sUser;
        else if (movCols.includes('entryuser')) movData['EntryUser'] = sUser;
        else if (movCols.includes('userid')) movData['UserID'] = sUser;
        else if (movCols.includes('modifyuser')) movData['ModifyUser'] = sUser;

        // Handle date column
        if (movCols.includes('createdate')) movData['CreateDate'] = new Date();
        else if (movCols.includes('entrydate')) movData['EntryDate'] = new Date();
        else if (movCols.includes('modifydate')) movData['ModifyDate'] = new Date();

        const insertCols = [];
        const paramPlaceholders = [];
        const movementReq = new sql.Request(transaction);

        for (const [col, val] of Object.entries(movData)) {
          if (movCols.includes(col.toLowerCase())) {
            insertCols.push(`[${col}]`);
            paramPlaceholders.push(`@${col}`);
            movementReq.input(col, val);
          }
        }

        if (insertCols.length > 0) {
          try {
            await movementReq.query(`
              INSERT INTO tblInventoryMovement (${insertCols.join(", ")})
              VALUES (${paramPlaceholders.join(", ")})
            `);
          } catch (eMov) {
            console.warn("tblInventoryMovement insert warning:", eMov.message);
          }
        }
      }
    }

    await transaction.commit();
    res.json({ success: true, message: "Inventory Transferred Successfully." });

  } catch (err) {
    if (transaction) {
      try {
        await transaction.rollback();
      } catch (rbErr) {}
    }
    console.error("Accept Transfer In Error:", err);
    res.status(500).json({ success: false, message: "Error accepting transfer: " + err.message });
  }
});

module.exports = router;
