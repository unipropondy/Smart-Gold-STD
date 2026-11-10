const express = require("express");
const router = express.Router();
const fs = require("fs");
const path = require("path");
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

// Safe date formatter to YYYY-MM-DD
function formatDate(d) {
  if (!d) return null;
  const dateObj = new Date(d);
  if (isNaN(dateObj.getTime())) return null;
  return dateObj.toISOString().split('T')[0];
}

// Pad voucher numbers
function padZero(num, size) {
  let s = num + "";
  while (s.length < size) s = "0" + s;
  return s;
}

// Case-insensitive key lookup helper
const findVal = (obj, ...keys) => {
  if (!obj) return undefined;
  for (const k of keys) {
    const matchKey = Object.keys(obj).find(x => x.toLowerCase() === k.toLowerCase());
    if (matchKey !== undefined && obj[matchKey] !== null && obj[matchKey] !== undefined) {
      return obj[matchKey];
    }
  }
  return undefined;
};

// Helper to get lowercase column names of a table
const getTableColumns = async (pool, tableName) => {
  try {
    const res = await pool.request()
      .input("tbl", sql.VarChar(100), tableName)
      .query("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @tbl");
    return res.recordset.map(r => r.COLUMN_NAME.toLowerCase());
  } catch (e) {
    return [];
  }
};

// Helper to get lowercase column types of a table
const getTableColumnTypes = async (pool, tableName) => {
  try {
    const res = await pool.request()
      .input("tbl", sql.VarChar(100), tableName)
      .query("SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @tbl");
    const map = {};
    for (const r of res.recordset) {
      map[r.COLUMN_NAME.toLowerCase()] = r.DATA_TYPE.toLowerCase();
    }
    return map;
  } catch (e) {
    return {};
  }
};

const isNumericType = (dt) => {
  if (!dt) return false;
  return ['int', 'smallint', 'tinyint', 'bigint', 'decimal', 'numeric', 'float', 'real', 'money', 'smallmoney'].includes(dt);
};

// 1. GET /api/stocktradeintransfer/locations
// Matching AddItemstoComboL in frmStockTradeInTransfer.vb:
// Select * from tblLocation Order by SortCode
router.get("/locations", async (req, res) => {
  try {
    const pool = await getPool(req);
    const query = `
      SELECT 
        RTRIM(LocationCode) as LocationCode, 
        RTRIM(LocationName) as LocationName 
      FROM tblLocation 
      WHERE LocationCode NOT IN (SELECT LocationCode FROM tblCompany WHERE LocationCode IS NOT NULL)
      ORDER BY SortCode ASC
    `;
    const result = await pool.request().query(query);
    res.json({ success: true, data: result.recordset });
  } catch (error) {
    console.error("Error fetching locations:", error);
    res.status(500).json({ success: false, message: "Error fetching locations: " + error.message });
  }
});

// 2. GET /api/stocktradeintransfer/inventory/:code
// Matching fncGetInventory & subGetClass in frmStockTradeInTransfer.vb
router.get("/inventory/:code", async (req, res) => {
  try {
    const { code } = req.params;
    const { fromLocation } = req.query;
    if (!code || !code.trim()) {
      return res.status(400).json({ success: false, message: "Barcode/Inventory Code is required." });
    }

    const pool = await getPool(req);
    const cleanCode = code.trim();

    // Query tblInventory using SELECT TOP 1 * to safely adapt to any schema differences
    const invRes = await pool.request()
      .input("Code", sql.VarChar(50), cleanCode)
      .query(`
        SELECT TOP 1 *
        FROM tblInventory
        WHERE RTRIM(LTRIM(CAST(InventoryCode AS VARCHAR(50)))) = @Code
      `);

    if (invRes.recordset.length === 0) {
      return res.json({ success: false, message: "Inventory Not Found ..." });
    }

    const inv = invRes.recordset[0];

    // Determine quantity using any available quantity/pcs column
    const rawQty = findVal(inv, 'QtyOnHand', 'QtyonHand', 'Qty', 'Quantity', 'Pcs', 'PCS', 'StockQty');
    let qty = rawQty !== undefined && rawQty !== null && !isNaN(parseFloat(rawQty)) ? parseFloat(rawQty) : 1;

    // Check if separate tblInventoryStock table exists with stock info
    try {
      const stockRes = await pool.request()
        .input("Code", sql.VarChar(50), cleanCode)
        .query(`
          SELECT TOP 1 *
          FROM tblInventoryStock
          WHERE RTRIM(LTRIM(CAST(InventoryCode AS VARCHAR(50)))) = @Code
        `);
      if (stockRes.recordset.length > 0) {
        const sQty = findVal(stockRes.recordset[0], 'QtyOnHand', 'QtyonHand', 'Qty', 'Quantity', 'Pcs', 'PCS', 'StockQty');
        if (sQty !== undefined && sQty !== null && !isNaN(parseFloat(sQty))) {
          qty = parseFloat(sQty);
        }
      }
    } catch (eStock) {}

    // Check stock: If Val(QtyOnHand) < 1 Then MsgBox("No Stock for this Inventory!")
    if (qty < 1) {
      return res.json({ success: false, message: "No Stock for this Inventory!" });
    }

    // Verify location if provided
    const itemLoc = String(findVal(inv, 'LocationCode') || '').trim();
    if (fromLocation && fromLocation.trim() && fromLocation.trim().toUpperCase() !== 'HQ') {
      if (itemLoc && itemLoc.toUpperCase() !== fromLocation.trim().toUpperCase()) {
        return res.json({ 
          success: false, 
          message: `The selected Inventory belongs to Location ${itemLoc}!` 
        });
      }
    }

    // Class details (Rate and SalesType) matching subGetClass
    const classCode = String(findVal(inv, 'ClassCode') || '').trim();
    let classRate = null;
    let salesType = 'W';

    if (classCode) {
      try {
        const clsRes = await pool.request()
          .input("ClassCode", sql.VarChar(50), classCode)
          .query(`
            SELECT TOP 1 *
            FROM tblClass
            WHERE RTRIM(LTRIM(CAST(ClassCode AS VARCHAR(50)))) = @ClassCode
          `);
        if (clsRes.recordset.length > 0) {
          const cls = clsRes.recordset[0];
          const r = findVal(cls, 'Rate', 'ClassRate');
          if (r !== undefined && r !== null && !isNaN(parseFloat(r))) {
            classRate = parseFloat(r);
          }
          salesType = String(findVal(cls, 'SalesType') || 'W').trim();
        }
      } catch (eCls) {}
    }

    const defaultCostPerGram = parseFloat(findVal(inv, 'CostPerGram', 'Rate') || 0);
    const costPerGram = classRate !== null && classRate > 0 ? classRate : defaultCostPerGram;
    const weightVal = parseFloat(findVal(inv, 'Weight', 'TotalWeight', 'GrossWeight') || 0);
    const pmcVal = parseFloat(findVal(inv, 'PMC') || 0);
    const smcVal = parseFloat(findVal(inv, 'SMC', 'sMc') || 0);
    let totalCostVal = parseFloat(findVal(inv, 'TotalCost') || 0);

    if (!totalCostVal || totalCostVal === 0) {
      if (salesType === 'P') {
        totalCostVal = smcVal;
      } else {
        totalCostVal = weightVal * costPerGram + smcVal;
      }
    }

    const mappedItem = {
      InventoryCode: String(findVal(inv, 'InventoryCode') || cleanCode).trim(),
      Description: String(findVal(inv, 'Description') || '').trim(),
      ClassCode: classCode,
      CategoryCode: String(findVal(inv, 'CategoryCode') || '').trim(),
      CatalogueCode: String(findVal(inv, 'CatalogueCode') || '').trim(),
      Weight: weightVal,
      SizeCode: String(findVal(inv, 'SizeCode', 'Size') || '').trim(),
      QtyOnHand: qty,
      Pcs: qty > 0 ? qty : 1,
      CostPerGram: costPerGram,
      PMC: pmcVal,
      SMC: smcVal,
      TotalCost: totalCostVal,
      LocationCode: itemLoc,
      SalesType: salesType,
      ClassRate: classRate || 0
    };

    res.json({ success: true, data: mappedItem });
  } catch (error) {
    console.error("Error looking up inventory item:", error);
    res.status(500).json({ success: false, message: "Error fetching inventory: " + error.message });
  }
});

// 3. GET /api/stocktradeintransfer/scrap-inventory
// Matching subAddGridScrapInventory in frmStockTradeInTransfer.vb (GetScrapInventory)
router.get("/scrap-inventory", async (req, res) => {
  try {
    const { fromLocation } = req.query;
    const pool = await getPool(req);

    let query = `
      SELECT TOP 200 *
      FROM tblInventory
      WHERE (CategoryCode LIKE '%SCRAP%' OR Description LIKE '%SCRAP%')
    `;

    if (fromLocation && fromLocation.trim() && fromLocation.trim().toUpperCase() !== 'HQ') {
      query += ` AND RTRIM(LocationCode) = '${fromLocation.trim()}'`;
    }

    const result = await pool.request().query(query);
    const mappedList = result.recordset.map(inv => {
      const rawQty = findVal(inv, 'QtyOnHand', 'QtyonHand', 'Qty', 'Quantity', 'Pcs', 'PCS');
      const qty = rawQty !== undefined && rawQty !== null && !isNaN(parseFloat(rawQty)) ? parseFloat(rawQty) : 1;
      return {
        InventoryCode: String(findVal(inv, 'InventoryCode') || '').trim(),
        Description: String(findVal(inv, 'Description') || '').trim(),
        ClassCode: String(findVal(inv, 'ClassCode') || '').trim(),
        CategoryCode: String(findVal(inv, 'CategoryCode') || '').trim(),
        CatalogueCode: String(findVal(inv, 'CatalogueCode') || '').trim(),
        Weight: parseFloat(findVal(inv, 'Weight') || 0),
        SizeCode: String(findVal(inv, 'SizeCode', 'Size') || '').trim(),
        QtyOnHand: qty,
        Pcs: qty > 0 ? qty : 1,
        CostPerGram: parseFloat(findVal(inv, 'CostPerGram', 'Rate') || 0),
        PMC: parseFloat(findVal(inv, 'PMC') || 0),
        SMC: parseFloat(findVal(inv, 'SMC', 'sMc') || 0),
        TotalCost: parseFloat(findVal(inv, 'TotalCost') || 0),
        SalesType: 'W'
      };
    }).filter(i => i.QtyOnHand > 0);

    res.json({ success: true, data: mappedList });
  } catch (error) {
    console.error("Error fetching scrap inventory:", error);
    res.status(500).json({ success: false, message: "Error fetching scrap inventory: " + error.message });
  }
});

// 4. GET /api/stocktradeintransfer/fetch
// Matching fncheckTansferNoExists & FetchTransferDetails in frmStockTradeInTransfer.vb
router.get("/fetch", async (req, res) => {
  try {
    const { transferNo } = req.query;
    if (!transferNo || !transferNo.trim()) {
      return res.status(400).json({ success: false, message: "Transfer Number is required." });
    }

    const pool = await getPool(req);
    const cleanNo = transferNo.trim();

    // Check Header using SELECT TOP 1 * and CONVERT(VARCHAR(50), TransferNo)
    // to avoid "Error converting data type varchar to numeric"
    const headerQuery = `
      SELECT TOP 1 *
      FROM tblInventoryTradeInTransferHeader
      WHERE RTRIM(LTRIM(CONVERT(VARCHAR(50), TransferNo))) = @TransferNo
    `;

    const headerRes = await pool.request()
      .input("TransferNo", sql.VarChar(50), cleanNo)
      .query(headerQuery);

    if (headerRes.recordset.length === 0) {
      return res.json({ success: false, message: "Transfer number does not exist." });
    }

    const rawHeader = headerRes.recordset[0];
    const header = {
      TransferNo: String(findVal(rawHeader, 'TransferNo') || cleanNo).trim(),
      TransferDate: findVal(rawHeader, 'TransferDate'),
      FromLocation: String(findVal(rawHeader, 'FromLocation') || '').trim(),
      ToLocation: String(findVal(rawHeader, 'ToLocation') || '').trim(),
      Remarks: String(findVal(rawHeader, 'Remarks') || '').trim(),
      TotalWeight: parseFloat(findVal(rawHeader, 'TotalWeight') || 0),
      TotalCost: parseFloat(findVal(rawHeader, 'TotalCost') || 0),
      TotalPcs: parseInt(findVal(rawHeader, 'TotalPcs') || 0, 10),
      TotalPMC: parseFloat(findVal(rawHeader, 'TotalPMC') || 0),
      TotalSMC: parseFloat(findVal(rawHeader, 'TotalSMC') || 0),
      SalesManCode: String(findVal(rawHeader, 'SalesManCode') || '').trim()
    };

    // Check Detail using SELECT * and CONVERT(VARCHAR(50), TransferNo)
    let detailQuery = `
      SELECT d.*, ISNULL(RTRIM(i.SupplierCode), '') as InvSupplierCode
      FROM tblInventoryTradeInTransferDetail d
      LEFT JOIN tblInventory i ON RTRIM(LTRIM(CAST(d.InventoryCode AS VARCHAR(50)))) = RTRIM(LTRIM(CAST(i.InventoryCode AS VARCHAR(50))))
      WHERE RTRIM(LTRIM(CONVERT(VARCHAR(50), d.TransferNo))) = @TransferNo
    `;

    let detailRes;
    try {
      detailRes = await pool.request()
        .input("TransferNo", sql.VarChar(50), cleanNo)
        .query(detailQuery);
    } catch (eDet) {
      detailQuery = `
        SELECT *
        FROM tblInventoryTradeInTransferDetail
        WHERE RTRIM(LTRIM(CONVERT(VARCHAR(50), TransferNo))) = @TransferNo
      `;
      detailRes = await pool.request()
        .input("TransferNo", sql.VarChar(50), cleanNo)
        .query(detailQuery);
    }

    const details = detailRes.recordset.map(row => {
      const rawSize = findVal(row, 'SizeCode', 'Size');
      const rawQty = findVal(row, 'QtyOnHand', 'QtyonHand', 'Qty', 'Pcs', 'PCS');
      return {
        InventoryCode: String(findVal(row, 'InventoryCode') || '').trim(),
        Description: String(findVal(row, 'Description') || '').trim(),
        ClassCode: String(findVal(row, 'ClassCode') || '').trim(),
        CategoryCode: String(findVal(row, 'CategoryCode') || '').trim(),
        CatalogueCode: String(findVal(row, 'CatalogueCode') || '').trim(),
        SupplierCode: String(findVal(row, 'SupplierCode', 'InvSupplierCode') || '').trim(),
        Weight: parseFloat(findVal(row, 'Weight') || 0),
        SizeCode: rawSize !== null && rawSize !== undefined ? String(rawSize).trim() : '',
        QtyOnHand: parseInt(rawQty || 1, 10),
        Pcs: parseInt(rawQty || 1, 10),
        CostPerGram: parseFloat(findVal(row, 'CostPerGram') || 0),
        PMC: parseFloat(findVal(row, 'PMC') || 0),
        SMC: parseFloat(findVal(row, 'SMC') || 0),
        TotalCost: parseFloat(findVal(row, 'TotalCost') || 0)
      };
    });

    res.json({
      success: true,
      header,
      details
    });
  } catch (error) {
    console.error("Error fetching transfer details:", error);
    res.status(500).json({ success: false, message: "Error fetching transfer: " + error.message });
  }
});

// 5. GET /api/stocktradeintransfer/past-list
// Browse past stock trade-in transfers
router.get("/past-list", async (req, res) => {
  try {
    const { fromLocation, search } = req.query;
    const pool = await getPool(req);

    let query = `
      SELECT TOP 100 *
      FROM tblInventoryTradeInTransferHeader
      WHERE 1=1
    `;

    const request = pool.request();

    if (fromLocation && fromLocation.trim() && fromLocation.trim().toUpperCase() !== 'HQ') {
      query += ` AND RTRIM(FromLocation) = @FromLoc`;
      request.input("FromLoc", sql.VarChar(50), fromLocation.trim());
    }

    if (search && search.trim()) {
      query += ` AND (RTRIM(LTRIM(CONVERT(VARCHAR(50), TransferNo))) LIKE @Search OR RTRIM(ToLocation) LIKE @Search OR RTRIM(Remarks) LIKE @Search)`;
      request.input("Search", sql.VarChar(100), `%${search.trim()}%`);
    }

    query += ` ORDER BY TransferDate DESC`;

    const result = await request.query(query);
    const mapped = result.recordset.map(r => ({
      TransferNo: String(findVal(r, 'TransferNo') || '').trim(),
      TransferDate: findVal(r, 'TransferDate'),
      FromLocation: String(findVal(r, 'FromLocation') || '').trim(),
      ToLocation: String(findVal(r, 'ToLocation') || '').trim(),
      TotalPcs: parseInt(findVal(r, 'TotalPcs') || 0, 10),
      TotalWeight: parseFloat(findVal(r, 'TotalWeight') || 0),
      TotalCost: parseFloat(findVal(r, 'TotalCost') || 0),
      Remarks: String(findVal(r, 'Remarks') || '').trim()
    }));

    res.json({ success: true, data: mapped, list: mapped });
  } catch (error) {
    console.error("Error fetching past transfers:", error);
    res.status(500).json({ success: false, message: "Error fetching past transfers: " + error.message });
  }
});

// 6. POST /api/stocktradeintransfer/verifypassword
// Matching subGetPassword in frmStockTradeInTransfer.vb
router.post("/verifypassword", async (req, res) => {
  try {
    const { password } = req.body;
    if (!password || !password.trim()) {
      return res.status(400).json({ success: false, message: "Password is required" });
    }

    const pool = await getPool(req);
    const cleanPwd = password.trim();

    const query = `
      SELECT TOP 1 
        RTRIM(SalesManCode) AS SalesManCode, 
        RTRIM(ISNULL(SalesManName, '')) AS SalesManName 
      FROM tblSalesMan 
      WHERE RTRIM(Password) = @Pwd OR RTRIM(SalesManCode) = @Pwd
    `;

    const result = await pool.request()
      .input("Pwd", sql.VarChar(50), cleanPwd)
      .query(query);

    if (result.recordset.length > 0) {
      res.json({ success: true, salesman: result.recordset[0] });
    } else {
      res.json({ success: false, message: "SalesMan Password is Incorrect!" });
    }
  } catch (err) {
    console.error("Password verification error:", err);
    res.status(500).json({ success: false, message: "Error verifying password: " + err.message });
  }
});

// 7. POST /api/stocktradeintransfer/transfer
// Execute Stock Trade-In Transfer matching fncUpdateTransfer in frmStockTradeInTransfer.vb
// 1. Generates TransferNo from tblCompany (NextTradeInTransferNo / NextTransferNo)
// 2. Inserts tblInventoryTradeInTransferHeader
// 3. Inserts tblInventoryTradeInTransferDetail
// 4. Updates tblCompany counter
// 5. Updates tblInventory stock (QtyOnHand reduced)
// 6. Inserts movement into tblInventoryMovement with TransType = 'TO'
router.post("/transfer", async (req, res) => {
  let pool;
  try {
    pool = await getPool(req);
  } catch (poolErr) {
    return res.status(500).json({ success: false, message: poolErr.message });
  }

  const transaction = new sql.Transaction(pool);
  try {
    const {
      transferDate,
      fromLocation,
      toLocation,
      remarks,
      totalWeight,
      totalPcs,
      totalCost,
      totalPMC,
      totalSMC,
      items,
      userId,
      salesManCode
    } = req.body;

    if (!toLocation || !toLocation.trim()) {
      return res.status(400).json({ success: false, message: "Select the ToLocation Code" });
    }
    if (!items || !Array.isArray(items) || items.length === 0) {
      return res.status(400).json({ success: false, message: "No Item To Transfer" });
    }

    await transaction.begin();

    // 1. Generate Transfer No from tblCompany (matching fncGetTradeInTransferNo)
    let loc = (fromLocation || "HQ").trim();
    let nextNo = 1;
    let targetCompanyCol = 'NextTransferNo';

    const compCols = await getTableColumns(pool, 'tblCompany');
    if (compCols.includes('nexttradeintransferno')) {
      targetCompanyCol = 'NextTradeInTransferNo';
    } else if (compCols.includes('nextstocktradeintransferno')) {
      targetCompanyCol = 'NextStockTradeInTransferNo';
    }

    const compRes = await new sql.Request(transaction).query(
      `SELECT TOP 1 LocationCode, ${targetCompanyCol} AS NextVal FROM tblCompany`
    );

    if (compRes.recordset.length > 0) {
      loc = (compRes.recordset[0].LocationCode || loc).trim();
      const rawCompNo = compRes.recordset[0].NextVal;
      if (rawCompNo != null) {
        const numMatch = String(rawCompNo).match(/\d+/);
        if (numMatch) {
          nextNo = parseInt(numMatch[0], 10);
        }
      }
    }

    // Check max in header table if counter is 1
    if (nextNo === 1) {
      try {
        const maxHeaderRes = await new sql.Request(transaction).query(
          `SELECT TOP 1 TransferNo FROM tblInventoryTradeInTransferHeader ORDER BY TransferNo DESC`
        );
        if (maxHeaderRes.recordset.length > 0 && maxHeaderRes.recordset[0].TransferNo) {
          const numMatch = String(maxHeaderRes.recordset[0].TransferNo).match(/\d+/);
          if (numMatch) {
            nextNo = parseInt(numMatch[0], 10) + 1;
          }
        }
      } catch (eMax) {}
    }

    const transferNo = `TIT-${loc}${padZero(nextNo, 8)}`;
    const sUser = (userId || "ADMIN").trim();
    const tDate = transferDate ? new Date(transferDate) : new Date();

    // Inspect table column types dynamically
    const [headerColTypes, detailColTypes, movementColTypes, headerCols, detailCols, invCols, stockCols, movCols] = await Promise.all([
      getTableColumnTypes(pool, 'tblInventoryTradeInTransferHeader'),
      getTableColumnTypes(pool, 'tblInventoryTradeInTransferDetail'),
      getTableColumnTypes(pool, 'tblInventoryMovement'),
      getTableColumns(pool, 'tblInventoryTradeInTransferHeader'),
      getTableColumns(pool, 'tblInventoryTradeInTransferDetail'),
      getTableColumns(pool, 'tblInventory'),
      getTableColumns(pool, 'tblInventoryStock'),
      getTableColumns(pool, 'tblInventoryMovement')
    ]);

    const fromLocStatusVal = isNumericType(headerColTypes['fromlocstatus']) ? 2 : "2";
    const toLocStatusVal = isNumericType(headerColTypes['tolocstatus']) ? 0 : "0";

    // 2. Insert into tblInventoryTradeInTransferHeader
    const headerObj = {
      TransferNo: transferNo,
      TransferDate: !isNaN(tDate.getTime()) ? tDate : new Date(),
      FromLocation: loc,
      ToLocation: toLocation.trim(),
      TotalWeight: parseFloat(totalWeight) || 0,
      TotalPcs: parseInt(totalPcs, 10) || items.length,
      TotalCost: parseFloat(totalCost) || 0,
      TotalPMC: parseFloat(totalPMC) || 0,
      TotalSMC: parseFloat(totalSMC) || 0,
      FromLocStatus: fromLocStatusVal,
      ToLocStatus: toLocStatusVal,
      Remarks: (remarks || '').trim(),
      SalesManCode: (salesManCode || '').trim(),
      CreateUser: sUser,
      ModifyUser: sUser,
      CreateDate: new Date(),
      ModifyDate: new Date()
    };

    const headerInsertCols = [];
    const headerPlaceholders = [];
    const insertHeaderReq = new sql.Request(transaction);

    for (const [col, val] of Object.entries(headerObj)) {
      if (headerCols.includes(col.toLowerCase())) {
        headerInsertCols.push(`[${col}]`);
        headerPlaceholders.push(`@${col}`);
        insertHeaderReq.input(col, val);
      }
    }

    await insertHeaderReq.query(`
      INSERT INTO tblInventoryTradeInTransferHeader (${headerInsertCols.join(", ")})
      VALUES (${headerPlaceholders.join(", ")})
    `);

    // 3. Insert Details, Update Inventory, and Log tblInventoryMovement
    const isDetailSizeNumeric = isNumericType(detailColTypes['sizecode']);
    const isMovSizeNumeric = isNumericType(movementColTypes['sizecode']);

    for (const item of items) {
      const invCode = String(item.InventoryCode || '').trim();
      const itemDesc = (item.Description || '').trim();
      const itemQty = parseInt(item.Qty || item.QtyonHand || item.PCS || 1, 10) || 1;
      const itemWeight = parseFloat(item.Weight) || 0;
      const itemCostPerGram = parseFloat(item.CostPerGram) || 0;
      const itemPMC = parseFloat(item.PMC) || 0;
      const itemSMC = parseFloat(item.SMC) || 0;
      const itemTotalCost = parseFloat(item.TotalCost) || 0;

      const rawSize = item.SizeCode !== undefined && item.SizeCode !== null ? item.SizeCode : '';
      const sizeNumericVal = parseFloat(rawSize) || 0;
      const sizeStringVal = String(rawSize || '').trim();

      const detailObj = {
        TransferNo: transferNo,
        InventoryCode: invCode,
        ClassCode: (item.ClassCode || '').trim(),
        CategoryCode: (item.CategoryCode || '').trim(),
        CatalogueCode: (item.CatalogueCode || '').trim(),
        Description: itemDesc,
        QtyonHand: itemQty,
        Weight: itemWeight,
        SizeCode: isDetailSizeNumeric ? sizeNumericVal : sizeStringVal,
        CostPerGram: itemCostPerGram,
        PMC: itemPMC,
        SMC: itemSMC,
        TotalCost: itemTotalCost,
        TransferTag: 0,
        CreateUser: sUser,
        ModifyUser: sUser,
        CreateDate: new Date(),
        ModifyDate: new Date()
      };

      const detailInsertCols = [];
      const detailPlaceholders = [];
      const insertDetailReq = new sql.Request(transaction);

      for (const [col, val] of Object.entries(detailObj)) {
        if (detailCols.includes(col.toLowerCase())) {
          detailInsertCols.push(`[${col}]`);
          detailPlaceholders.push(`@${col}`);
          insertDetailReq.input(col, val);
        }
      }

      await insertDetailReq.query(`
        INSERT INTO tblInventoryTradeInTransferDetail (${detailInsertCols.join(", ")})
        VALUES (${detailPlaceholders.join(", ")})
      `);

      // Update tblInventory stock (matching fncUpdateInventoryOnTransfer)
      let updateCol = null;
      if (invCols.includes('qtyonhand')) updateCol = 'QtyOnHand';
      else if (invCols.includes('qty')) updateCol = 'Qty';
      else if (invCols.includes('pcs')) updateCol = 'Pcs';
      else if (invCols.includes('quantity')) updateCol = 'Quantity';

      let updateInvSql = 'UPDATE tblInventory SET ModifyDate = GETDATE(), ModifyUser = @ModifyUser';
      if (updateCol) {
        updateInvSql += `, ${updateCol} = CASE WHEN ${updateCol} >= @Qty THEN ${updateCol} - @Qty ELSE 0 END`;
      }
      updateInvSql += ' WHERE RTRIM(LTRIM(CAST(InventoryCode AS VARCHAR(50)))) = @InventoryCode';

      const updateStockReq = new sql.Request(transaction);
      await updateStockReq
        .input("InventoryCode", sql.VarChar(50), invCode)
        .input("Qty", sql.Int, itemQty)
        .input("ModifyUser", sql.VarChar(50), sUser)
        .query(updateInvSql);

      // Also update tblInventoryStock if present
      if (stockCols.length > 0) {
        let sCol = null;
        if (stockCols.includes('qtyonhand')) sCol = 'QtyOnHand';
        else if (stockCols.includes('qty')) sCol = 'Qty';
        else if (stockCols.includes('pcs')) sCol = 'Pcs';
        else if (stockCols.includes('quantity')) sCol = 'Quantity';

        if (sCol) {
          try {
            await new sql.Request(transaction)
              .input("InventoryCode", sql.VarChar(50), invCode)
              .input("Qty", sql.Int, itemQty)
              .query(`
                UPDATE tblInventoryStock 
                SET ${sCol} = CASE WHEN ${sCol} >= @Qty THEN ${sCol} - @Qty ELSE 0 END
                WHERE RTRIM(LTRIM(CAST(InventoryCode AS VARCHAR(50)))) = @InventoryCode
              `);
          } catch (eStockUpdate) {}
        }
      }

      // Record in tblInventoryMovement (TransType = 'TO' matching VB code: fncInventoryMovementSave "TO")
      if (movCols.length > 0) {
        let locCol = 'LocationCode';
        if (!movCols.includes('locationcode') && movCols.includes('translocation')) {
          locCol = 'TransLocation';
        }

        const movData = {
          InventoryCode: invCode,
          Description: itemDesc,
          TransType: 'TO',
          TransNo: transferNo,
          TransDate: new Date(),
          Qty: itemQty,
          Weight: itemWeight,
          PMC: itemPMC,
          SMC: itemSMC,
          CostPerGram: itemCostPerGram,
          TotalCost: itemTotalCost,
        };

        if (movCols.includes(locCol.toLowerCase())) {
          movData[locCol] = toLocation.trim();
        }

        if (movCols.includes('sizecode')) {
          movData['SizeCode'] = isMovSizeNumeric ? sizeNumericVal : sizeStringVal;
        }

        if (movCols.includes('createuser')) movData['CreateUser'] = sUser;
        else if (movCols.includes('entryuser')) movData['EntryUser'] = sUser;
        else if (movCols.includes('userid')) movData['UserID'] = sUser;
        else if (movCols.includes('modifyuser')) movData['ModifyUser'] = sUser;

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

    // 4. Update NextTradeInTransferNo in tblCompany
    await new sql.Request(transaction).query(
      `UPDATE tblCompany SET ${targetCompanyCol} = ${nextNo + 1}`
    );

    await transaction.commit();
    res.json({
      success: true,
      message: `Please take note reference no. ${transferNo}`,
      transferNo
    });

  } catch (error) {
    if (transaction) {
      try {
        await transaction.rollback();
      } catch (rbErr) {}
    }
    console.error("Execute Stock Trade-In Transfer Error:", error);
    res.status(500).json({ success: false, message: "Error during transfer: " + error.message });
  }
});

// 8. GET /api/stocktradeintransfer/print-data
// Matching subPrintTransferOut and rptTradeInTransferOutDetail in frmStockTradeInTransfer.vb
router.get("/print-data", async (req, res) => {
  try {
    const { transferNo } = req.query;
    if (!transferNo || !transferNo.trim()) {
      return res.status(400).json({ success: false, message: "Transfer Number is required for print." });
    }

    const pool = await getPool(req);
    const cleanNo = transferNo.trim();

    // 1. Fetch Company Information
    let logoBase64 = "";
    try {
      const logoPath = path.join(__dirname, "../unipro_logo_base64.txt");
      if (fs.existsSync(logoPath)) {
        logoBase64 = fs.readFileSync(logoPath, "utf8").trim();
      }
    } catch (eLogo) {}

    const compRes = await pool.request().query("SELECT TOP 1 * FROM tblCompany");
    const rawCompany = compRes.recordset[0] || {};
    const company = {
      name: String(findVal(rawCompany, 'Name', 'CompanyName') || '').trim(),
      address1: String(findVal(rawCompany, 'Address1') || '').trim(),
      address2: String(findVal(rawCompany, 'Address2') || '').trim(),
      city: String(findVal(rawCompany, 'City') || '').trim(),
      state: String(findVal(rawCompany, 'State') || '').trim(),
      country: String(findVal(rawCompany, 'Country') || '').trim(),
      phone: String(findVal(rawCompany, 'Phone') || '').trim(),
      fax: String(findVal(rawCompany, 'Fax') || '').trim(),
      logo: logoBase64 ? `data:image/png;base64,${logoBase64}` : '',
      logoBase64: logoBase64
    };

    // 2. Fetch Header Information
    let headerQuery = `
      SELECT TOP 1 *
      FROM tblInventoryTradeInTransferHeader
      WHERE RTRIM(LTRIM(CONVERT(VARCHAR(50), TransferNo))) = @TransferNo
    `;
    let headerRes = await pool.request()
      .input("TransferNo", sql.VarChar(50), cleanNo)
      .query(headerQuery);

    if (headerRes.recordset.length === 0) {
      headerQuery = `
        SELECT TOP 1 *
        FROM tblInventoryTransferHeader
        WHERE RTRIM(LTRIM(CONVERT(VARCHAR(50), TransferNo))) = @TransferNo
      `;
      headerRes = await pool.request()
        .input("TransferNo", sql.VarChar(50), cleanNo)
        .query(headerQuery);
    }

    if (headerRes.recordset.length === 0) {
      return res.status(404).json({ success: false, message: `Transfer Note not found for Transfer No: ${cleanNo}` });
    }

    const rawHeader = headerRes.recordset[0];
    const header = {
      transferNo: String(findVal(rawHeader, 'TransferNo') || cleanNo).trim(),
      transferDate: findVal(rawHeader, 'TransferDate'),
      fromLocation: String(findVal(rawHeader, 'FromLocation') || '').trim(),
      toLocation: String(findVal(rawHeader, 'ToLocation') || '').trim(),
      remarks: String(findVal(rawHeader, 'Remarks') || '').trim(),
      totalWeight: parseFloat(findVal(rawHeader, 'TotalWeight', 'Totalweight') || 0),
      totalPcs: parseInt(findVal(rawHeader, 'TotalPcs', 'TotalPCS') || 0, 10),
      totalCost: parseFloat(findVal(rawHeader, 'TotalCost') || 0),
      salesManCode: String(findVal(rawHeader, 'SalesManCode') || '').trim(),
    };

    // 3. Fetch Detail Information joined with tblInventory for SupplierCode
    let detailQuery = `
      SELECT 
        d.*,
        ISNULL(RTRIM(i.SupplierCode), '') as InvSupplierCode
      FROM tblInventoryTradeInTransferDetail d
      LEFT JOIN tblInventory i ON RTRIM(LTRIM(CAST(d.InventoryCode AS VARCHAR(50)))) = RTRIM(LTRIM(CAST(i.InventoryCode AS VARCHAR(50))))
      WHERE RTRIM(LTRIM(CONVERT(VARCHAR(50), d.TransferNo))) = @TransferNo
      ORDER BY d.InventoryCode ASC
    `;
    let detailRes;
    try {
      detailRes = await pool.request()
        .input("TransferNo", sql.VarChar(50), cleanNo)
        .query(detailQuery);
    } catch (eDetQ) {
      detailQuery = `
        SELECT *
        FROM tblInventoryTradeInTransferDetail
        WHERE RTRIM(LTRIM(CONVERT(VARCHAR(50), TransferNo))) = @TransferNo
        ORDER BY InventoryCode ASC
      `;
      detailRes = await pool.request()
        .input("TransferNo", sql.VarChar(50), cleanNo)
        .query(detailQuery);
    }

    if (detailRes.recordset.length === 0) {
      try {
        detailRes = await pool.request()
          .input("TransferNo", sql.VarChar(50), cleanNo)
          .query(`
            SELECT 
              d.*,
              ISNULL(RTRIM(i.SupplierCode), '') as InvSupplierCode
            FROM tblInventoryTransferDetail d
            LEFT JOIN tblInventory i ON RTRIM(LTRIM(CAST(d.InventoryCode AS VARCHAR(50)))) = RTRIM(LTRIM(CAST(i.InventoryCode AS VARCHAR(50))))
            WHERE RTRIM(LTRIM(CONVERT(VARCHAR(50), d.TransferNo))) = @TransferNo
            ORDER BY d.InventoryCode ASC
          `);
      } catch (eDetFallback) {}
    }

    const details = await Promise.all(detailRes.recordset.map(async (row) => {
      const rawSize = findVal(row, 'SizeCode', 'Size');
      const sizeVal = rawSize !== null && rawSize !== undefined && !isNaN(parseFloat(rawSize)) ? parseFloat(rawSize) : 0;
      const grossWeight = parseFloat(findVal(row, 'GrossWeight', 'Weight') || 0);
      const netWeight = parseFloat(findVal(row, 'NetWeight', 'Weight') || 0);
      const rawPcs = findVal(row, 'PCS', 'QtyOnHand', 'Qty');
      const pcsVal = (rawPcs !== null && rawPcs !== undefined && rawPcs !== '') ? (parseInt(rawPcs, 10) || '') : '';
      const totalCost = parseFloat(findVal(row, 'TotalCost') || 0);
      let sCode = String(findVal(row, 'SupplierCode', 'InvSupplierCode') || '').trim();

      if (!sCode && row.InventoryCode) {
        try {
          const invLookup = await pool.request()
            .input("Code", sql.VarChar(50), String(row.InventoryCode).trim())
            .query("SELECT TOP 1 SupplierCode FROM tblInventory WHERE RTRIM(LTRIM(CAST(InventoryCode AS VARCHAR(50)))) = @Code");
          if (invLookup.recordset.length > 0 && invLookup.recordset[0].SupplierCode) {
            sCode = String(invLookup.recordset[0].SupplierCode).trim();
          }
        } catch (eInv) {}
      }

      return {
        inventoryCode: String(findVal(row, 'InventoryCode') || '').trim(),
        description: String(findVal(row, 'Description') || '').trim(),
        size: sizeVal.toFixed(2),
        supplierCode: sCode,
        grossWeight: grossWeight,
        netWeight: netWeight,
        pcs: pcsVal,
        totalCost: totalCost
      };
    }));

    res.json({
      success: true,
      data: {
        company,
        header,
        details
      }
    });

  } catch (error) {
    console.error("Fetch Transfer Note Print Data Error:", error);
    res.status(500).json({ success: false, message: "Error fetching print data: " + error.message });
  }
});

module.exports = router;
