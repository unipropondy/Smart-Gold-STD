const express = require("express");
const router = express.Router();
const { sql } = require("../db");

// LOV for Inventory search
router.get("/lov/inventory", async (req, res) => {
  try {
    const pool = req.pool;
    const { SupplierCode } = req.query;

    let query = `
      SELECT 
        a.InventoryCode, 
        a.Description, 
        a.Weight,
        b.QtyOnHand,
        a.SupplierCode,
        a.PurchaseBillNo
      FROM tblInventory a
      LEFT JOIN tblInventoryStock b ON a.InventoryCode = b.InventoryCode AND a.LocationCode = b.LocationCode
      WHERE ISNULL(b.QtyOnHand, 0) >= 1
    `;

    const request = pool.request();
    if (SupplierCode && SupplierCode.trim()) {
      query += " AND a.SupplierCode = @SupplierCode";
      request.input("SupplierCode", sql.VarChar, SupplierCode);
    }

    const result = await request.query(query);
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("LOV Inventory Error:", err);
    res.status(500).json({ success: false, message: "Database Error", error: err.message });
  }
});

// Fetch inventory details for single inventory item
router.get("/inventoryDetail", async (req, res) => {
  try {
    const pool = req.pool;
    const { InventoryCode } = req.query;

    if (!InventoryCode) {
      return res.status(400).json({ success: false, message: "InventoryCode is required." });
    }

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
        a.ClassCode,
        b.QtyOnHand,
        c.PurchaseType
      FROM tblInventory a
      LEFT JOIN tblInventoryStock b ON a.InventoryCode = b.InventoryCode AND a.LocationCode = b.LocationCode
      LEFT JOIN tblClass c ON a.ClassCode = c.ClassCode
      WHERE a.InventoryCode = @InventoryCode
    `;

    const result = await pool.request()
      .input("InventoryCode", sql.VarChar, InventoryCode)
      .query(query);

    if (result.recordset.length > 0) {
      res.json({ success: true, data: result.recordset[0] });
    } else {
      res.json({ success: false, message: "Inventory Not Found." });
    }
  } catch (err) {
    console.error("Get Inventory Detail Error:", err);
    res.status(500).json({ success: false, message: "Database Error", error: err.message });
  }
});

// Save Purchase Return (Transaction)
router.post("/", async (req, res) => {
  const pool = req.pool;
  const transaction = new sql.Transaction(pool);
  try {
    const { header, details, userId } = req.body;
    const sysUser = userId || "sys";

    if (!header || !details || details.length === 0) {
      return res.status(400).json({ success: false, message: "Header and details are required." });
    }

    await transaction.begin();

    // 1. Generate Return No
    const prefixResult = await new sql.Request(transaction).query(`
      SELECT LocationCode, NextPurReNo FROM tblCompany
    `);
    
    let returnNo = "PR" + String(Date.now()).substring(4);
    if (prefixResult.recordset.length > 0) {
      const comp = prefixResult.recordset[0];
      const loc = comp.LocationCode || "HQ";
      const num = String(comp.NextPurReNo || 1).padStart(8, "0");
      returnNo = `${loc}${num}`;
    }

    // 2. Insert Header
    const reqHeader = new sql.Request(transaction);
    await reqHeader
      .input("ReturnNo", sql.VarChar, returnNo)
      .input("SupplierCode", sql.VarChar, header.SupplierCode)
      .input("ReturnDate", sql.SmallDateTime, header.ReturnDate ? new Date(header.ReturnDate) : new Date())
      .input("LocationCode", sql.VarChar, header.LocationCode || "HQ")
      .input("TotalWeight", sql.Numeric(18, 2), parseFloat(header.TotalWeight) || 0)
      .input("TotalPCS", sql.Numeric(18, 2), parseFloat(header.TotalPCS) || 0)
      .input("TotalValue", sql.Numeric(18, 2), parseFloat(header.TotalValue) || 0)
      .input("TotalGst", sql.Numeric(18, 2), parseFloat(header.TotalGST) || 0)
      .input("NetAmount", sql.Numeric(18, 2), parseFloat(header.NetTotal) || 0)
      .input("Remarks", sql.VarChar, header.Remarks || "")
      .input("CreateUser", sql.VarChar, sysUser)
      .query(`
        INSERT INTO tblPurchaseReturnHeader (
          ReturnNo, SupplierCode, ReturnDate, LocationCode, TotalWeight,
          TotalPCS, TotalValue, TotalGst, NetAmount, Remarks, CreateUser, CreateDate
        ) VALUES (
          @ReturnNo, @SupplierCode, @ReturnDate, @LocationCode, @TotalWeight,
          @TotalPCS, @TotalValue, @TotalGst, @NetAmount, @Remarks, @CreateUser, GETDATE()
        )
      `);

    // 3. Insert Details and Update inventory stocks
    for (let i = 0; i < details.length; i++) {
      const item = details[i];
      const reqDetail = new sql.Request(transaction);

      await reqDetail
        .input("ReturnNo", sql.VarChar, returnNo)
        .input("InventoryCode", sql.VarChar, item.InventoryCode)
        .input("Description", sql.VarChar, item.Description || "")
        .input("LocationCode", sql.VarChar, header.LocationCode || "HQ")
        .input("Weight", sql.Numeric(18, 3), parseFloat(item.Weight) || 0)
        .input("GoldLoss", sql.Numeric(18, 3), parseFloat(item.GoldLoss) || 0)
        .input("MC", sql.Numeric(18, 2), parseFloat(item.MC) || 0)
        .input("CostPerGram", sql.Numeric(18, 2), parseFloat(item.CostPerGram) || 0)
        .input("PCS", sql.Numeric(18, 0), parseInt(item.PCS) || 0)
        .input("TotalValue", sql.Numeric(18, 2), parseFloat(item.TotalCost) || 0)
        .input("CreateUser", sql.VarChar, sysUser)
        .query(`
          INSERT INTO tblPurchaseReturnDetail (
            ReturnNo, InventoryCode, Description, LocationCode, Weight,
            GoldLoss, MC, CostPerGram, PCS, TotalValue, CreateUser, CreateDate
          ) VALUES (
            @ReturnNo, @InventoryCode, @Description, @LocationCode, @Weight,
            @GoldLoss, @MC, @CostPerGram, @PCS, @TotalValue, @CreateUser, GETDATE()
          )
        `);

      // Decrease stock
      const reqStockUpdate = new sql.Request(transaction);
      await reqStockUpdate
        .input("InventoryCode", sql.VarChar, item.InventoryCode)
        .input("LocationCode", sql.VarChar, header.LocationCode || "HQ")
        .input("PCS", sql.Numeric(18, 0), parseInt(item.PCS) || 0)
        .input("Weight", sql.Numeric(18, 3), parseFloat(item.Weight) || 0)
        .query(`
          UPDATE tblInventoryStock
          SET QtyOnHand = ISNULL(QtyOnHand, 0) - @PCS,
              QtySold = ISNULL(QtySold, 0) + @PCS,
              Weight = ISNULL(Weight, 0) - @Weight
          WHERE InventoryCode = @InventoryCode AND LocationCode = @LocationCode
        `);

      // Add movement log
      const reqMv = new sql.Request(transaction);
      await reqMv
        .input("LocationCode", sql.VarChar, header.LocationCode || "HQ")
        .input("InventoryCode", sql.VarChar, item.InventoryCode)
        .input("Description", sql.VarChar, item.Description || "")
        .input("RefNumber", sql.VarChar, returnNo)
        .input("RefDate", sql.SmallDateTime, header.ReturnDate ? new Date(header.ReturnDate) : new Date())
        .input("PCS", sql.Numeric(18, 0), parseInt(item.PCS) || 0)
        .input("Weight", sql.Numeric(18, 3), parseFloat(item.Weight) || 0)
        .input("PMC", sql.Numeric(18, 2), parseFloat(item.MC) || 0)
        .input("CostPerGram", sql.Numeric(18, 2), parseFloat(item.CostPerGram) || 0)
        .input("TotalCost", sql.Numeric(18, 2), parseFloat(item.TotalCost) || 0)
        .input("SupplierCode", sql.VarChar, header.SupplierCode)
        .input("CreateUser", sql.VarChar, sysUser)
        .query(`
          INSERT INTO tblInventoryMovement (
            LocationCode, InventoryCode, Description, Status, RefNumber, RefDate,
            Qty, Weight, PMC, CostPerGram, TotalCost, SupplierCode, CreateUser, CreateDate
          ) VALUES (
            @LocationCode, @InventoryCode, @Description, 'PR', @RefNumber, @RefDate,
            -@PCS, -@Weight, @PMC, @CostPerGram, @TotalCost, @SupplierCode, @CreateUser, GETDATE()
          )
        `);
    }

    // 4. Update next serial code
    await new sql.Request(transaction).query(`
      UPDATE tblCompany SET NextPurReNo = ISNULL(NextPurReNo, 1) + 1
    `);

    await transaction.commit();
    res.json({ success: true, returnNo, message: "Purchase Return Saved Successfully." });
  } catch (err) {
    console.error("Save Purchase Return Error:", err);
    try {
      await transaction.rollback();
    } catch (e) {}
    res.status(500).json({ success: false, message: "Database Error", error: err.message });
  }
});

module.exports = router;
