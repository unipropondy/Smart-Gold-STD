/**
 * Developer: durairaj
 */

const express = require("express");
const router = express.Router();
const { sql, poolPromise } = require("../db");

// LOV for Supplier search matching frmPurchaseReturn.vb: btnSupplierCodeSearch_Click
router.get("/lov/supplier", async (req, res) => {
  try {
    const pool = req.pool || (await poolPromise);
    let query = `
      SELECT TOP 200 
        SupplierCode, 
        SupplierName, 
        Address1, 
        Address2
      FROM tblSupplier 
      ORDER BY SupplierCode ASC
    `;
    let result;
    try {
      result = await pool.request().query(query);
    } catch (colErr) {
      // Fallback if specific columns differ
      result = await pool.request().query(`
        SELECT TOP 200 * FROM tblSupplier ORDER BY SupplierCode ASC
      `);
    }
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("LOV Supplier Error:", err);
    res.status(500).json({ success: false, message: "Database Error", error: err.message });
  }
});

// LOV for Inventory search matching frmPurchaseReturn.vb: btnInventoryCodeSearch_Click
// Query: QtyOnHand >= 1 and (optional) SupplierCode = @SupplierCode
router.get("/lov/inventory", async (req, res) => {
  try {
    const pool = req.pool || (await poolPromise);
    const { SupplierCode } = req.query;

    let query = `
      SELECT TOP 200
        a.InventoryCode, 
        a.Description, 
        a.Weight,
        ISNULL(b.QtyOnHand, 1) as QtyOnHand,
        a.SupplierCode,
        a.JobSheetNo,
        a.PurchaseBillNo
      FROM tblInventory a
      LEFT JOIN tblInventoryStock b ON a.InventoryCode = b.InventoryCode
      WHERE (b.QtyOnHand IS NULL OR b.QtyOnHand >= 1)
        AND (a.SalesBillNo IS NULL OR LTRIM(RTRIM(a.SalesBillNo)) = '')
    `;

    const request = pool.request();
    if (SupplierCode && SupplierCode.trim()) {
      query += " AND a.SupplierCode = @SupplierCode";
      request.input("SupplierCode", sql.VarChar, SupplierCode.trim());
    }

    query += " ORDER BY a.InventoryCode ASC";

    const result = await request.query(query);
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("LOV Inventory Error:", err);
    res.status(500).json({ success: false, message: "Database Error", error: err.message });
  }
});

// Fetch inventory details for single inventory item
// Matches subGetInventory() & subGetClass() in frmPurchaseReturn.vb
router.get("/inventoryDetail", async (req, res) => {
  try {
    const pool = req.pool || (await poolPromise);
    const { InventoryCode } = req.query;

    if (!InventoryCode || !InventoryCode.trim()) {
      return res.status(400).json({ success: false, message: "InventoryCode is required." });
    }

    const trimmedCode = InventoryCode.trim();

    const query = `
      SELECT 
        a.InventoryCode,
        a.Description,
        a.Weight,
        a.GoldLoss,
        a.CostPerGram,
        a.PMC,
        a.TotalCost,
        a.SupplierCode,
        s.SupplierName,
        a.ClassCode,
        ISNULL(b.QtyOnHand, 1) as QtyOnHand,
        ISNULL(c.PurchaseType, 'W') as PurchaseType
      FROM tblInventory a
      LEFT JOIN tblInventoryStock b ON a.InventoryCode = b.InventoryCode
      LEFT JOIN tblSupplier s ON a.SupplierCode = s.SupplierCode
      LEFT JOIN tblClass c ON a.ClassCode = c.ClassCode
      WHERE a.InventoryCode = @InventoryCode
    `;

    const result = await pool.request()
      .input("InventoryCode", sql.VarChar, trimmedCode)
      .query(query);

    if (result.recordset.length > 0) {
      const item = result.recordset[0];
      if (item.QtyOnHand <= 0) {
        return res.json({ success: false, message: "No Stock for this Inventory!" });
      }
      res.json({ success: true, data: item });
    } else {
      res.json({ success: false, message: "This Inventory Not Found..." });
    }
  } catch (err) {
    console.error("Get Inventory Detail Error:", err);
    res.status(500).json({ success: false, message: "Database Error", error: err.message });
  }
});

// Save Purchase Return (Transaction)
// Matches btnSAve_Click in frmPurchaseReturn.vb
router.post("/", async (req, res) => {
  const pool = req.pool || (await poolPromise);
  const transaction = new sql.Transaction(pool);
  try {
    const { header, details, userId } = req.body;
    const sysUser = userId || "sys";

    if (!header || !details || details.length === 0) {
      return res.status(400).json({ success: false, message: "Header and details are required." });
    }

    await transaction.begin();

    // 1. Fetch Company Config and Generate Return No
    let loc = "HQ";
    let nextNo = 1;
    try {
      const prefixResult = await new sql.Request(transaction).query(`
        SELECT LocationCode, NextPurReNo FROM tblCompany
      `);
      if (prefixResult.recordset.length > 0) {
        const comp = prefixResult.recordset[0];
        loc = (comp.LocationCode || "HQ").trim();
        nextNo = parseInt(comp.NextPurReNo) || 1;
      }
    } catch (compErr) {
      console.warn("Could not query tblCompany for NextPurReNo:", compErr.message);
    }

    const returnNo = `${loc}${String(nextNo).padStart(8, "0")}`;
    const returnLocation = header.LocationCode || loc;

    // Inspect columns of tblPurchaseReturnHeader and tblPurchaseReturnDetail dynamically
    let headerCols = [];
    let detailCols = [];
    try {
      const hRes = await new sql.Request(transaction).query(`
        SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tblPurchaseReturnHeader'
      `);
      headerCols = hRes.recordset.map(c => c.COLUMN_NAME.toLowerCase());
    } catch (e) { }

    try {
      const dRes = await new sql.Request(transaction).query(`
        SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tblPurchaseReturnDetail'
      `);
      detailCols = dRes.recordset.map(c => c.COLUMN_NAME.toLowerCase());
    } catch (e) { }

    const hasHeaderCol = (name) => headerCols.length === 0 || headerCols.includes(name.toLowerCase());
    const hasDetailCol = (name) => detailCols.length === 0 || detailCols.includes(name.toLowerCase());

    // 2. Insert Header (matching tblPurchaseReturnHeader)
    const reqHeader = new sql.Request(transaction);
    const hCols = ['ReturnNo', 'SupplierCode', 'ReturnDate', 'LocationCode', 'TotalWeight', 'TotalPCS', 'TotalValue', 'TotalGst', 'NetAmount'];
    const hVals = ['@ReturnNo', '@SupplierCode', '@ReturnDate', '@LocationCode', '@TotalWeight', '@TotalPCS', '@TotalValue', '@TotalGst', '@NetAmount'];

    reqHeader.input("ReturnNo", sql.VarChar, returnNo);
    reqHeader.input("SupplierCode", sql.VarChar, header.SupplierCode ? header.SupplierCode.trim() : "");
    reqHeader.input("ReturnDate", sql.SmallDateTime, header.ReturnDate ? new Date(header.ReturnDate) : new Date());
    reqHeader.input("LocationCode", sql.VarChar, returnLocation);
    reqHeader.input("TotalWeight", sql.Numeric(18, 2), parseFloat(header.TotalWeight) || 0);
    reqHeader.input("TotalPCS", sql.Numeric(18, 2), parseFloat(header.TotalPCS) || 0);
    reqHeader.input("TotalValue", sql.Numeric(18, 2), parseFloat(header.TotalValue) || 0);
    reqHeader.input("TotalGst", sql.Numeric(18, 2), parseFloat(header.TotalGST) || 0);
    reqHeader.input("NetAmount", sql.Numeric(18, 2), parseFloat(header.NetTotal) || 0);

    if (hasHeaderCol('Remarks')) {
      hCols.push('Remarks');
      hVals.push('@Remarks');
      reqHeader.input("Remarks", sql.VarChar, header.Remarks || "");
    }
    if (hasHeaderCol('CreateUser')) {
      hCols.push('CreateUser');
      hVals.push('@CreateUser');
      reqHeader.input("CreateUser", sql.VarChar, sysUser);
    }
    if (hasHeaderCol('CreateDate')) {
      hCols.push('CreateDate');
      hVals.push('GETDATE()');
    }

    await reqHeader.query(`
      INSERT INTO tblPurchaseReturnHeader (${hCols.join(', ')})
      VALUES (${hVals.join(', ')})
    `);

    // 3. Insert Details and Update inventory stocks & movements
    for (let i = 0; i < details.length; i++) {
      const item = details[i];
      const invCode = item.InventoryCode.trim();
      const reqDetail = new sql.Request(transaction);

      const dCols = ['ReturnNo', 'InventoryCode', 'Description', 'LocationCode', 'Weight', 'GoldLoss', 'MC', 'CostPerGram', 'PCS', 'TotalValue'];
      const dVals = ['@ReturnNo', '@InventoryCode', '@Description', '@LocationCode', '@Weight', '@GoldLoss', '@MC', '@CostPerGram', '@PCS', '@TotalValue'];

      reqDetail.input("ReturnNo", sql.VarChar, returnNo);
      reqDetail.input("InventoryCode", sql.VarChar, invCode);
      reqDetail.input("Description", sql.VarChar, item.Description || "");
      reqDetail.input("LocationCode", sql.VarChar, returnLocation);
      reqDetail.input("Weight", sql.Numeric(18, 3), parseFloat(item.Weight) || 0);
      reqDetail.input("GoldLoss", sql.Numeric(18, 3), parseFloat(item.GoldLoss) || 0);
      reqDetail.input("MC", sql.Numeric(18, 2), parseFloat(item.MC) || 0);
      reqDetail.input("CostPerGram", sql.Numeric(18, 2), parseFloat(item.CostPerGram) || 0);
      reqDetail.input("PCS", sql.Numeric(18, 0), parseInt(item.PCS) || 0);
      reqDetail.input("TotalValue", sql.Numeric(18, 2), parseFloat(item.TotalCost) || 0);

      if (hasDetailCol('Remarks')) {
        dCols.push('Remarks');
        dVals.push('@Remarks');
        reqDetail.input("Remarks", sql.VarChar, header.Remarks || "");
      }
      if (hasDetailCol('CreateUser')) {
        dCols.push('CreateUser');
        dVals.push('@CreateUser');
        reqDetail.input("CreateUser", sql.VarChar, sysUser);
      }
      if (hasDetailCol('CreateDate')) {
        dCols.push('CreateDate');
        dVals.push('GETDATE()');
      }

      await reqDetail.query(`
        INSERT INTO tblPurchaseReturnDetail (${dCols.join(', ')})
        VALUES (${dVals.join(', ')})
      `);

      // Decrease stock in tblInventoryStock
      const reqStockUpdate = new sql.Request(transaction);
      await reqStockUpdate
        .input("InventoryCode", sql.VarChar, invCode)
        .input("PCS", sql.Numeric(18, 0), parseInt(item.PCS) || 0)
        .input("Weight", sql.Numeric(18, 3), parseFloat(item.Weight) || 0)
        .query(`
          UPDATE tblInventoryStock
          SET QtyOnHand = CASE WHEN ISNULL(QtyOnHand, 0) - @PCS < 0 THEN 0 ELSE ISNULL(QtyOnHand, 0) - @PCS END,
              QtySold = ISNULL(QtySold, 0) + @PCS,
              Weight = CASE WHEN ISNULL(Weight, 0) - @Weight < 0 THEN 0 ELSE ISNULL(Weight, 0) - @Weight END,
              ModifyDate = GETDATE()
          WHERE InventoryCode = @InventoryCode
        `);

      // Add movement log matching InventoryMovementSave: Status = 'PR'
      const reqMv = new sql.Request(transaction);
      await reqMv
        .input("LocationCode", sql.VarChar, returnLocation)
        .input("InventoryCode", sql.VarChar, invCode)
        .input("Description", sql.VarChar, item.Description || "")
        .input("RefNumber", sql.VarChar, returnNo)
        .input("RefDate", sql.SmallDateTime, header.ReturnDate ? new Date(header.ReturnDate) : new Date())
        .input("PCS", sql.Numeric(18, 0), parseInt(item.PCS) || 0)
        .input("Weight", sql.Numeric(18, 3), parseFloat(item.Weight) || 0)
        .input("PMC", sql.Numeric(18, 2), parseFloat(item.MC) || 0)
        .input("CostPerGram", sql.Numeric(18, 2), parseFloat(item.CostPerGram) || 0)
        .input("TotalCost", sql.Numeric(18, 2), parseFloat(item.TotalCost) || 0)
        .input("SupplierCode", sql.VarChar, header.SupplierCode ? header.SupplierCode.trim() : "")
        .input("CreateUser", sql.VarChar, sysUser)
        .query(`
          INSERT INTO tblInventoryMovement (
            LocationCode, InventoryCode, Description, Status, RefNumber, RefDate,
            Qty, Weight, PMC, CostPerGram, TotalCost, SupplierCode, CreateUser, CreateDate
          ) VALUES (
            @LocationCode, @InventoryCode, @Description, 'PR', @RefNumber, @RefDate,
            @PCS, @Weight, @PMC, @CostPerGram, @TotalCost, @SupplierCode, @CreateUser, GETDATE()
          )
        `);
    }

    // 4. Update next serial code in tblCompany
    try {
      await new sql.Request(transaction).query(`
        UPDATE tblCompany SET NextPurReNo = ISNULL(NextPurReNo, 1) + 1
      `);
    } catch (updErr) {
      console.warn("Could not increment NextPurReNo on tblCompany:", updErr.message);
    }

    await transaction.commit();
    res.json({ success: true, returnNo, message: `Please take Note Reference No : ${returnNo}` });
  } catch (err) {
    console.error("Save Purchase Return Error:", err);
    try {
      await transaction.rollback();
    } catch (e) { }
    res.status(500).json({ success: false, message: "Database Error", error: err.message });
  }
});

module.exports = router;
