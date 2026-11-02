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

const isNumericType = (type) => {
  if (!type) return false;
  const t = type.toLowerCase();
  return t.includes('int') || t.includes('decimal') || t.includes('numeric') || 
         t.includes('float') || t.includes('real') || t.includes('money');
};

// 1. GET /api/transferout/locations
// Fetch locations for LOV (To Location)
// VB: Select * from tblLocation where locationcode not in(select locationcode from tblcompany) Order by SortCode
router.get("/locations", async (req, res) => {
  try {
    const pool = await getPool(req);
    const query = `
      SELECT 
        RTRIM(LocationCode) as LocationCode, 
        RTRIM(LocationName) as LocationName,
        SortCode
      FROM tblLocation 
      WHERE LocationCode NOT IN (SELECT LocationCode FROM tblCompany)
      ORDER BY SortCode
    `;
    const result = await pool.request().query(query);
    res.json({ success: true, data: result.recordset });
  } catch (error) {
    console.error("Error fetching locations:", error);
    res.status(500).json({ success: false, message: "Error fetching locations: " + error.message });
  }
});

// 2. GET /api/transferout/next-no
// Generate next transfer number based on tblCompany NextTransferNo
router.get("/next-no", async (req, res) => {
  try {
    const pool = await getPool(req);
    let loc = "HQ";
    let nextNo = 1;

    try {
      const compRes = await pool.request().query("SELECT TOP 1 LocationCode, NextTransferNo FROM tblCompany");
      if (compRes.recordset.length > 0) {
        const comp = compRes.recordset[0];
        loc = (comp.LocationCode || "HQ").trim();
        if (comp.NextTransferNo != null) {
          nextNo = parseInt(comp.NextTransferNo, 10) || 1;
        }
      }
    } catch (e1) {
      console.warn("tblCompany next-no check warning:", e1.message);
    }

    // Also check max from tblInventoryTransferHeader if counter was 1
    if (nextNo === 1) {
      try {
        const maxRes = await pool.request().query(`
          SELECT TOP 1 TransferNo FROM tblInventoryTransferHeader ORDER BY TransferNo DESC
        `);
        if (maxRes.recordset.length > 0 && maxRes.recordset[0].TransferNo) {
          const match = maxRes.recordset[0].TransferNo.match(/\d+/);
          if (match) {
            nextNo = parseInt(match[0], 10) + 1;
          }
        }
      } catch (e2) {}
    }

    const formattedNo = `TO-${loc}${padZero(nextNo, 8)}`;
    res.json({ success: true, transferNo: formattedNo, rawNo: nextNo, locationCode: loc });
  } catch (err) {
    console.error("Fetch Next Transfer No Error:", err);
    res.json({ success: true, transferNo: "TO-HQ00000001", rawNo: 1, locationCode: "HQ" });
  }
});

// 3. GET /api/transferout/inventory/:code
// Matching fncGetInventory(sinventorycode) & subGetClass in frmTransferOut.vb
router.get("/inventory/:code", async (req, res) => {
  try {
    const { code } = req.params;
    if (!code || !code.trim()) {
      return res.status(400).json({ success: false, message: "Inventory code is required." });
    }

    const pool = await getPool(req);
    const sCode = code.trim();

    // 1. Fetch item from tblInventory using SELECT TOP 1 * to safely adapt to the exact schema
    const invResult = await pool.request()
      .input("InventoryCode", sql.VarChar(50), sCode)
      .query(`
        SELECT TOP 1 *
        FROM tblInventory
        WHERE RTRIM(LTRIM(CAST(InventoryCode AS VARCHAR(50)))) = @InventoryCode
      `);

    if (invResult.recordset.length === 0) {
      return res.json({ success: false, message: "Inventory Not Found ..." });
    }

    const inv = invResult.recordset[0];

    // Determine quantity using any available quantity/pcs column
    const rawQty = findVal(inv, 'QtyOnHand', 'QtyonHand', 'Qty', 'Quantity', 'Pcs', 'PCS', 'StockQty');
    let qty = rawQty !== undefined && rawQty !== null && !isNaN(parseFloat(rawQty)) ? parseFloat(rawQty) : 1;

    // Check if separate tblInventoryStock table exists with stock info
    try {
      const stockRes = await pool.request()
        .input("InventoryCode", sql.VarChar(50), sCode)
        .query(`
          SELECT TOP 1 *
          FROM tblInventoryStock
          WHERE RTRIM(LTRIM(CAST(InventoryCode AS VARCHAR(50)))) = @InventoryCode
        `);
      if (stockRes.recordset.length > 0) {
        const sQty = findVal(stockRes.recordset[0], 'QtyOnHand', 'QtyonHand', 'Qty', 'Quantity', 'Pcs', 'PCS', 'StockQty');
        if (sQty !== undefined && sQty !== null && !isNaN(parseFloat(sQty))) {
          qty = parseFloat(sQty);
        }
      }
    } catch (eStock) {
      // tblInventoryStock not present or not used
    }

    // VB rule: If (Val(dsInv.Tables(0).Rows(0)("QtyOnHand") & "") < 1) Then MsgBox("No Stock for this Inventory!")
    if (qty < 1) {
      return res.json({ success: false, message: "No Stock for this Inventory!", outOfStock: true });
    }

    // Class details (Rate and SalesType) matching subGetClass:
    // txtCostPerGram.Text = Val(dsCls.Tables(0).Rows(0)("Rate") & "")
    // msClsSalesType = Trim$(dsCls.Tables(0).Rows(0)("SalesType") & "")
    const classCode = String(findVal(inv, 'ClassCode') || '').trim();
    let classRate = null;
    let salesType = '';

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
          salesType = String(findVal(cls, 'SalesType') || '').trim();
        }
      } catch (eCls) {
        console.warn("Class lookup warning:", eCls.message);
      }
    }

    const defaultCostPerGram = parseFloat(findVal(inv, 'CostPerGram', 'Rate') || 0);
    const costPerGram = classRate !== null ? classRate : defaultCostPerGram;

    const resultData = {
      InventoryCode: String(findVal(inv, 'InventoryCode') || sCode).trim(),
      Description: String(findVal(inv, 'Description') || '').trim(),
      ClassCode: classCode,
      CategoryCode: String(findVal(inv, 'CategoryCode') || '').trim(),
      CatalogueCode: String(findVal(inv, 'CatalogueCode') || '').trim(),
      Weight: parseFloat(findVal(inv, 'Weight', 'TotalWeight', 'GrossWeight') || 0),
      SizeCode: String(findVal(inv, 'SizeCode', 'Size') || '').trim(),
      QtyonHand: qty,
      PCS: qty > 0 ? qty : 1,
      CostPerGram: costPerGram,
      PMC: parseFloat(findVal(inv, 'PMC') || 0),
      SMC: parseFloat(findVal(inv, 'SMC', 'sMc') || 0),
      TotalCost: parseFloat(findVal(inv, 'TotalCost') || 0),
      SalesType: salesType,
      LocationCode: String(findVal(inv, 'LocationCode') || '').trim()
    };

    res.json({
      success: true,
      data: resultData
    });
  } catch (error) {
    console.error("Error fetching inventory:", error);
    res.status(500).json({ success: false, message: "Error fetching inventory: " + error.message });
  }
});

// 4. GET /api/transferout/scrap-inventory
// Matching subAddGridScrapInventory / objTran.GetScrapInventory in frmTransferOut.vb
router.get("/scrap-inventory", async (req, res) => {
  try {
    const pool = await getPool(req);
    // Scrap items query safely without hardcoded columns
    const query = `
      SELECT TOP 200 *
      FROM tblInventory
      WHERE (
        UPPER(ISNULL(CategoryCode, '')) LIKE '%SCRAP%' OR 
        UPPER(ISNULL(Description, '')) LIKE '%SCRAP%' OR 
        UPPER(ISNULL(ClassCode, '')) LIKE '%SCRAP%'
      )
      ORDER BY InventoryCode ASC
    `;
    const result = await pool.request().query(query);
    const scrapItems = result.recordset.map(inv => {
      const rawQty = findVal(inv, 'QtyOnHand', 'QtyonHand', 'Qty', 'Quantity', 'Pcs', 'PCS', 'StockQty');
      const qty = rawQty !== undefined && rawQty !== null && !isNaN(parseFloat(rawQty)) ? parseFloat(rawQty) : 1;
      return {
        InventoryCode: String(findVal(inv, 'InventoryCode') || '').trim(),
        Description: String(findVal(inv, 'Description') || '').trim(),
        ClassCode: String(findVal(inv, 'ClassCode') || '').trim(),
        CategoryCode: String(findVal(inv, 'CategoryCode') || '').trim(),
        CatalogueCode: String(findVal(inv, 'CatalogueCode') || '').trim(),
        Weight: parseFloat(findVal(inv, 'Weight') || 0),
        SizeCode: String(findVal(inv, 'SizeCode') || '').trim(),
        QtyonHand: qty,
        PCS: qty > 0 ? qty : 1,
        CostPerGram: parseFloat(findVal(inv, 'CostPerGram') || 0),
        PMC: parseFloat(findVal(inv, 'PMC') || 0),
        SMC: parseFloat(findVal(inv, 'SMC', 'sMc') || 0),
        TotalCost: parseFloat(findVal(inv, 'TotalCost') || 0)
      };
    }).filter(item => item.QtyonHand >= 1);

    res.json({ success: true, data: scrapItems });
  } catch (error) {
    console.error("Error fetching scrap inventory:", error);
    res.status(500).json({ success: false, message: "Error fetching scrap inventory: " + error.message });
  }
});

// 5. POST /api/transferout/verify-salesman
// Matching PnlSalesManPwd / subGetPassword in frmTransferOut.vb
router.post("/verify-salesman", async (req, res) => {
  try {
    const { password } = req.body;
    if (!password || !password.trim()) {
      return res.status(400).json({ success: false, message: "SalesMan Password is required!" });
    }

    const pool = await getPool(req);
    const result = await pool.request()
      .input("Password", sql.VarChar(50), password.trim())
      .query(`
        SELECT TOP 1 SalesmanCode, SalesManName 
        FROM tblSalesman 
        WHERE RTRIM(LTRIM(Password)) = @Password
      `);

    if (result.recordset.length > 0) {
      res.json({ 
        success: true, 
        salesman: {
          SalesmanCode: (result.recordset[0].SalesmanCode || '').trim(),
          SalesManName: (result.recordset[0].SalesManName || '').trim()
        } 
      });
    } else {
      res.json({ success: false, message: "SalesMan Password is Incorrect!" });
    }
  } catch (error) {
    console.error("Verify Salesman Error:", error);
    res.status(500).json({ success: false, message: "Error verifying salesman password: " + error.message });
  }
});

// 6. GET /api/transferout/fetch
// Matching fncheckTansferNoExists & FetchTransferDetails in frmTransferOut.vb
router.get("/fetch", async (req, res) => {
  try {
    const { transferNo } = req.query;
    if (!transferNo || !transferNo.trim()) {
      return res.status(400).json({ success: false, message: "Transfer Number is required." });
    }

    const pool = await getPool(req);

    // Fetch Header
    const headerQuery = `
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
        RTRIM(ISNULL(SalesManCode, '')) as SalesManCode
      FROM tblInventoryTransferHeader 
      WHERE RTRIM(LTRIM(TransferNo)) = @TransferNo
    `;
    const headerResult = await pool.request()
      .input("TransferNo", sql.VarChar(50), transferNo.trim())
      .query(headerQuery);

    if (headerResult.recordset.length === 0) {
      return res.json({ success: false, message: "Transfer number does not exists." });
    }

    // Fetch Detail
    const detailQuery = `
      SELECT 
        RTRIM(InventoryCode) AS InventoryCode,
        RTRIM(ISNULL(Description, '')) AS Description,
        RTRIM(ISNULL(ClassCode, '')) AS ClassCode,
        RTRIM(ISNULL(CategoryCode, '')) AS CategoryCode,
        RTRIM(ISNULL(CatalogueCode, '')) AS CatalogueCode,
        ISNULL(Weight, 0) AS Weight,
        RTRIM(ISNULL(SizeCode, '')) AS SizeCode,
        ISNULL(QtyonHand, 0) AS QtyonHand,
        ISNULL(CostPerGram, 0) AS CostPerGram,
        ISNULL(PMC, 0) AS PMC,
        ISNULL(SMC, 0) AS SMC,
        ISNULL(TotalCost, 0) AS TotalCost
      FROM tblInventoryTransferDetail 
      WHERE RTRIM(LTRIM(TransferNo)) = @TransferNo
    `;
    const detailResult = await pool.request()
      .input("TransferNo", sql.VarChar(50), transferNo.trim())
      .query(detailQuery);

    res.json({ 
      success: true, 
      header: headerResult.recordset[0],
      details: detailResult.recordset 
    });
  } catch (error) {
    console.error("Error fetching transfer:", error);
    res.status(500).json({ success: false, message: "Error fetching transfer: " + error.message });
  }
});

// 7. POST /api/transferout/transfer
// Execute Transfer Out (matching fncUpdateTransfer in frmTransferOut.vb)
// 1. Validates ToLocation and grid items
// 2. Generates TransferNo from tblCompany (NextTransferNo)
// 3. Inserts tblInventoryTransferHeader (FromLocStatus='2', ToLocStatus='0')
// 4. Inserts tblInventoryTransferDetail
// 5. Updates tblCompany NextTransferNo
// 6. Updates tblInventory stock (QtyOnHand reduced/marked)
// 7. Saves movement into tblInventoryMovement with TransType='TO'
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
      salesManCode,
      items, 
      userId 
    } = req.body;

    if (!toLocation || !toLocation.trim()) {
      return res.status(400).json({ success: false, message: "Select the ToLocation Code" });
    }
    if (!items || !Array.isArray(items) || items.length === 0) {
      return res.status(400).json({ success: false, message: "No Item To Transfer" });
    }

    await transaction.begin();

    // 1. Generate Transfer No from tblCompany
    let loc = (fromLocation || "HQ").trim();
    let nextNo = 1;
    const compRes = await new sql.Request(transaction).query(
      "SELECT TOP 1 LocationCode, NextTransferNo FROM tblCompany"
    );
    if (compRes.recordset.length > 0) {
      loc = (compRes.recordset[0].LocationCode || loc).trim();
      const rawCompNo = compRes.recordset[0].NextTransferNo;
      if (rawCompNo != null) {
        const numMatch = String(rawCompNo).match(/\d+/);
        if (numMatch) {
          nextNo = parseInt(numMatch[0], 10);
        }
      }
    }

    const transferNo = `TO-${loc}${padZero(nextNo, 8)}`;
    const sUser = userId || "ADMIN";
    const sSalesMan = salesManCode || sUser;
    const tDate = transferDate ? new Date(transferDate) : new Date();

    // Inspect table column types to prevent any "Error converting data type varchar to numeric"
    const headerColTypes = await getTableColumnTypes(pool, 'tblInventoryTransferHeader');
    const detailColTypes = await getTableColumnTypes(pool, 'tblInventoryTransferDetail');
    const movementColTypes = await getTableColumnTypes(pool, 'tblInventoryMovement');
    const companyColTypes = await getTableColumnTypes(pool, 'tblCompany');

    const fromLocStatusVal = isNumericType(headerColTypes['fromlocstatus']) ? 2 : "2";
    const toLocStatusVal = isNumericType(headerColTypes['tolocstatus']) ? 0 : "0";

    // 2. Insert into tblInventoryTransferHeader
    const insertHeaderReq = new sql.Request(transaction);
    await insertHeaderReq
      .input("TransferNo", sql.VarChar(50), transferNo)
      .input("TransferDate", sql.DateTime, !isNaN(tDate.getTime()) ? tDate : new Date())
      .input("FromLocation", sql.VarChar(50), loc)
      .input("ToLocation", sql.VarChar(50), toLocation.trim())
      .input("TotalWeight", sql.Decimal(18, 3), parseFloat(totalWeight) || 0)
      .input("TotalPcs", sql.Int, parseInt(totalPcs, 10) || items.length)
      .input("TotalCost", sql.Decimal(18, 2), parseFloat(totalCost) || 0)
      .input("TotalPMC", sql.Decimal(18, 2), parseFloat(totalPMC) || 0)
      .input("TotalSMC", sql.Decimal(18, 2), parseFloat(totalSMC) || 0)
      .input("FromLocStatus", isNumericType(headerColTypes['fromlocstatus']) ? sql.Int : sql.VarChar(5), fromLocStatusVal)
      .input("ToLocStatus", isNumericType(headerColTypes['tolocstatus']) ? sql.Int : sql.VarChar(5), toLocStatusVal)
      .input("Remarks", sql.VarChar(200), (remarks || '').trim())
      .input("SalesManCode", sql.VarChar(50), sSalesMan)
      .input("CreateUser", sql.VarChar(50), sUser)
      .query(`
        INSERT INTO tblInventoryTransferHeader (
          TransferNo, TransferDate, FromLocation, ToLocation, 
          TotalWeight, TotalPcs, TotalCost, TotalPMC, TotalSMC, 
          FromLocStatus, ToLocStatus, Remarks, SalesManCode, 
          CreateUser, ModifyUser, CreateDate, ModifyDate
        ) VALUES (
          @TransferNo, @TransferDate, @FromLocation, @ToLocation, 
          @TotalWeight, @TotalPcs, @TotalCost, @TotalPMC, @TotalSMC, 
          @FromLocStatus, @ToLocStatus, @Remarks, @SalesManCode, 
          @CreateUser, @CreateUser, GETDATE(), GETDATE()
        )
      `);

    // 3. Insert Details, Update Inventory, and Log InventoryMovement
    const invCols = await getTableColumns(pool, 'tblInventory');
    const stockCols = await getTableColumns(pool, 'tblInventoryStock');
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

      // Safe Size value (number if column is numeric, string if varchar)
      const rawSize = item.SizeCode !== undefined && item.SizeCode !== null ? item.SizeCode : '';
      const sizeNumericVal = parseFloat(rawSize) || 0;
      const sizeStringVal = String(rawSize || '').trim();

      // Insert Detail
      const insertDetailReq = new sql.Request(transaction);
      insertDetailReq
        .input("TransferNo", sql.VarChar(50), transferNo)
        .input("InventoryCode", sql.VarChar(50), invCode)
        .input("ClassCode", sql.VarChar(50), (item.ClassCode || '').trim())
        .input("CategoryCode", sql.VarChar(50), (item.CategoryCode || '').trim())
        .input("CatalogueCode", sql.VarChar(50), (item.CatalogueCode || '').trim())
        .input("Description", sql.VarChar(200), itemDesc)
        .input("QtyonHand", sql.Int, itemQty)
        .input("Weight", sql.Decimal(18, 3), itemWeight);

      if (isDetailSizeNumeric) {
        insertDetailReq.input("SizeCode", sql.Decimal(18, 2), sizeNumericVal);
      } else {
        insertDetailReq.input("SizeCode", sql.VarChar(50), sizeStringVal);
      }

      insertDetailReq
        .input("CostPerGram", sql.Decimal(18, 2), itemCostPerGram)
        .input("PMC", sql.Decimal(18, 2), itemPMC)
        .input("SMC", sql.Decimal(18, 2), itemSMC)
        .input("TotalCost", sql.Decimal(18, 2), itemTotalCost)
        .input("CreateUser", sql.VarChar(50), sUser);

      await insertDetailReq.query(`
        INSERT INTO tblInventoryTransferDetail (
          TransferNo, InventoryCode, ClassCode, CategoryCode, CatalogueCode, Description, 
          QtyonHand, Weight, SizeCode, CostPerGram, PMC, SMC, TotalCost, TransferTag, 
          CreateUser, ModifyUser, CreateDate, ModifyDate
        ) VALUES (
          @TransferNo, @InventoryCode, @ClassCode, @CategoryCode, @CatalogueCode, @Description, 
          @QtyonHand, @Weight, @SizeCode, @CostPerGram, @PMC, @SMC, @TotalCost, 0, 
          @CreateUser, @CreateUser, GETDATE(), GETDATE()
        )
      `);

      // Update tblInventory stock (matching fncupdateInventoryOnTransfer)
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

      // Record in tblInventoryMovement (TransType = 'TO')
      const movementReq = new sql.Request(transaction);
      movementReq
        .input("TransferNo", sql.VarChar(50), transferNo)
        .input("InventoryCode", sql.VarChar(50), invCode)
        .input("ToLocation", sql.VarChar(50), toLocation.trim())
        .input("Description", sql.VarChar(200), itemDesc)
        .input("Qty", sql.Int, itemQty)
        .input("Weight", sql.Decimal(18, 3), itemWeight);

      if (isMovSizeNumeric) {
        movementReq.input("SizeCode", sql.Decimal(18, 2), sizeNumericVal);
      } else {
        movementReq.input("SizeCode", sql.VarChar(50), sizeStringVal);
      }

      movementReq
        .input("PMC", sql.Decimal(18, 2), itemPMC)
        .input("SMC", sql.Decimal(18, 2), itemSMC)
        .input("CostPerGram", sql.Decimal(18, 2), itemCostPerGram)
        .input("TotalCost", sql.Decimal(18, 2), itemTotalCost)
        .input("UserId", sql.VarChar(50), sUser);

      await movementReq.query(`
        INSERT INTO tblInventoryMovement (
          LocationCode, InventoryCode, Description, TransType, 
          TransNo, TransDate, Qty, Weight, SizeCode, PMC, SMC, 
          CostPerGram, TotalCost, EntryDate, EntryUser
        ) VALUES (
          @ToLocation, @InventoryCode, @Description, 'TO',
          @TransferNo, GETDATE(), @Qty, @Weight, @SizeCode, @PMC, @SMC, 
          @CostPerGram, @TotalCost, GETDATE(), @UserId
        )
      `);
    }

    // 4. Increment NextTransferNo in tblCompany safely without SQL varchar-to-numeric crash
    const compNextNoType = companyColTypes['nexttransferno'] || '';
    const nextVal = nextNo + 1;
    let nextTransferNoParam = nextVal;
    if (compNextNoType.includes('char') || compNextNoType.includes('text')) {
      nextTransferNoParam = padZero(nextVal, 8);
    }

    try {
      await new sql.Request(transaction)
        .input("NextTransferNo", nextTransferNoParam)
        .query("UPDATE tblCompany SET NextTransferNo = @NextTransferNo");
    } catch (eCompUp) {
      console.warn("Update tblCompany NextTransferNo warning:", eCompUp.message);
    }

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
    console.error("Transfer Out Error:", error);
    res.status(500).json({ success: false, message: "Error during transfer: " + error.message, error: error.message });
  }
});

module.exports = router;
