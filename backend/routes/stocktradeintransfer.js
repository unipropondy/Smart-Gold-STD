const express = require("express");
const router = express.Router();
const { sql } = require("../db");

// Helper function to generate Transfer No
const generateTransferNo = async (pool) => {
  // Using NextTradeInTransferNo logic conceptually, but generating dynamically here for safety
  const query = `SELECT ISNULL(MAX(CAST(TransferNo AS BIGINT)), 10000) + 1 AS NextNo FROM tblInventoryTradeInTransferHeader WHERE ISNUMERIC(TransferNo) = 1`;
  const result = await pool.request().query(query);
  return result.recordset[0].NextNo.toString();
};

// GET /api/stocktradeintransfer/locations
// Fetch locations for LOV (To Location)
router.get("/locations", async (req, res) => {
  try {
    const pool = req.pool;
    const query = `
      SELECT RTRIM(LocationCode) as LocationCode, RTRIM(LocationName) as LocationName 
      FROM tblLocation 
      ORDER BY SortCode
    `;
    const result = await pool.request().query(query);
    res.json({ success: true, data: result.recordset });
  } catch (error) {
    console.error("Error fetching locations:", error);
    res.status(500).json({ success: false, message: "Error fetching locations: " + error.message });
  }
});

// GET /api/stocktradeintransfer/inventory/:code
// Fetch inventory details when barcode is scanned
router.get("/inventory/:code", async (req, res) => {
  try {
    const { code } = req.params;
    const pool = req.pool;
    
    // Fetch from tblInventory
    const query = `
      SELECT 
        a.InventoryCode, a.Description, a.ClassCode, a.CategoryCode, a.CatalogueCode, 
        a.Weight, a.SizeCode, ISNULL(b.QtyOnHand, 1) AS QtyonHand, a.CostPerGram, a.PMC, a.SMC, a.TotalCost, a.LocationCode
      FROM tblInventory a
      LEFT JOIN tblInventoryStock b ON a.InventoryCode = b.InventoryCode AND a.LocationCode = b.LocationCode
      WHERE a.InventoryCode = @InventoryCode
    `;
    const result = await pool.request()
      .input("InventoryCode", sql.VarChar, code)
      .query(query);
      
    if (result.recordset.length === 0) {
      return res.status(404).json({ success: false, message: "Inventory Not Found" });
    }
    
    const inv = result.recordset[0];
    
    // Check Qty
    if (parseFloat(inv.QtyonHand) < 1) {
      return res.status(400).json({ success: false, message: "No Stock for this Inventory!" });
    }

    res.json({ success: true, data: inv });
  } catch (error) {
    console.error("Error fetching inventory:", error);
    res.status(500).json({ success: false, message: "Error fetching inventory: " + error.message });
  }
});

// GET /api/stocktradeintransfer/fetch
// Fetch existing Transfer details
router.get("/fetch", async (req, res) => {
  try {
    const { transferNo } = req.query;
    if (!transferNo) {
      return res.status(400).json({ success: false, message: "Transfer Number is required." });
    }

    const pool = req.pool;

    // Fetch Header
    const headerQuery = `
      SELECT 
        TransferNo, TransferDate, FromLocation, ToLocation, 
        Remarks, TotalWeight, TotalCost, TotalPCS, TotalPMC, TotalSMC
      FROM tblInventoryTradeInTransferHeader 
      WHERE TransferNo = @TransferNo
    `;
    const headerResult = await pool.request()
      .input("TransferNo", sql.VarChar, transferNo.trim())
      .query(headerQuery);

    if (headerResult.recordset.length === 0) {
      return res.status(404).json({ success: false, message: "Transfer number does not exist." });
    }

    // Fetch Detail
    const detailQuery = `
      SELECT * 
      FROM tblInventoryTradeInTransferDetail 
      WHERE TransferNo = @TransferNo
    `;
    const detailResult = await pool.request()
      .input("TransferNo", sql.VarChar, transferNo.trim())
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

// POST /api/stocktradeintransfer/verifypassword
router.post("/verifypassword", async (req, res) => {
  try {
    const { password } = req.body;
    if (!password) return res.status(400).json({ success: false, message: "Password required" });
    const pool = req.pool;
    const result = await pool.request()
      .input("Password", sql.VarChar, password)
      .query("SELECT SalesManCode, SalesManName FROM tblSalesman WHERE Password = @Password");
    if (result.recordset.length > 0) {
      res.json({ success: true, salesman: result.recordset[0] });
    } else {
      res.json({ success: false, message: "Invalid password" });
    }
  } catch (err) {
    res.status(500).json({ success: false, message: err.message });
  }
});

// POST /api/stocktradeintransfer/transfer
// Create a new Trade In Transfer
router.post("/transfer", async (req, res) => {
  let transaction;
  try {
    const { 
      transferDate, fromLocation, toLocation, remarks, 
      totalWeight, totalPcs, totalCost, totalPMC, totalSMC,
      items, userId, salesManCode
    } = req.body;

    if (!toLocation || !items || items.length === 0) {
      return res.status(400).json({ success: false, message: "Invalid payload." });
    }

    const pool = req.pool;
    transaction = new sql.Transaction(pool);
    transaction.on('rollback', aborted => {
      console.log('Transaction automatically rolled back due to error.');
    });
    transaction.on('error', err => {
      console.log('Transaction error:', err);
    });
    await transaction.begin();

    // Generate Transfer No
    const transferNo = await generateTransferNo(pool);

    // Insert Header
    const insertHeaderReq = new sql.Request(transaction);
    await insertHeaderReq
      .input("TransferNo", sql.VarChar, transferNo)
      .input("TransferDate", sql.Date, transferDate)
      .input("FromLocation", sql.VarChar, fromLocation)
      .input("ToLocation", sql.VarChar, toLocation)
      .input("TotalWeight", sql.Decimal(18,3), totalWeight)
      .input("TotalPcs", sql.Int, totalPcs)
      .input("TotalCost", sql.Decimal(18,2), totalCost)
      .input("TotalPMC", sql.Decimal(18,2), totalPMC)
      .input("TotalSMC", sql.Decimal(18,2), totalSMC)
      .input("FromLocStatus", sql.VarChar, '2') // 2 = Transferred
      .input("ToLocStatus", sql.VarChar, '0') // 0 = Pending
      .input("Remarks", sql.VarChar, remarks || '')
      .input("SalesManCode", sql.VarChar, salesManCode || '')
      .input("CreateUser", sql.VarChar, userId || 'sys')
      .query(`
        INSERT INTO tblInventoryTradeInTransferHeader (
          TransferNo, TransferDate, FromLocation, ToLocation, TotalWeight, TotalPcs, 
          TotalCost, TotalPMC, TotalSMC, FromLocStatus, ToLocStatus, Remarks, SalesManCode, CreateUser, ModifyUser, CreateDate, ModifyDate
        ) VALUES (
          @TransferNo, @TransferDate, @FromLocation, @ToLocation, @TotalWeight, @TotalPcs, 
          @TotalCost, @TotalPMC, @TotalSMC, @FromLocStatus, @ToLocStatus, @Remarks, @SalesManCode, @CreateUser, @CreateUser, GETDATE(), GETDATE()
        )
      `);

    // Process Items
    for (const item of items) {
      const insertDetailReq = new sql.Request(transaction);
      
      // Insert Detail
      await insertDetailReq
        .input("TransferNo", sql.VarChar, transferNo)
        .input("InventoryCode", sql.VarChar, item.InventoryCode)
        .input("ClassCode", sql.VarChar, item.ClassCode != null ? String(item.ClassCode) : '')
        .input("CategoryCode", sql.VarChar, item.CategoryCode != null ? String(item.CategoryCode) : '')
        .input("CatalogueCode", sql.VarChar, item.CatalogueCode != null ? String(item.CatalogueCode) : '')
        .input("Description", sql.VarChar, item.Description != null ? String(item.Description) : '')
        .input("QtyonHand", sql.Int, Number(item.Qty) || 1)
        .input("Weight", sql.Decimal(18,3), Number(item.Weight) || 0)
        .input("SizeCode", sql.VarChar, item.SizeCode != null ? String(item.SizeCode) : '0')
        .input("CostPerGram", sql.Decimal(18,2), Number(item.CostPerGram) || 0)
        .input("PMC", sql.Decimal(18,2), Number(item.PMC) || 0)
        .input("SMC", sql.Decimal(18,2), Number(item.SMC) || 0)
        .input("TotalCost", sql.Decimal(18,2), Number(item.TotalCost) || 0)
        .input("CreateUser", sql.VarChar, userId || 'sys')
        .query(`
          INSERT INTO tblInventoryTradeInTransferDetail (
            TransferNo, InventoryCode, ClassCode, CategoryCode, CatalogueCode, Description, 
            QtyonHand, Weight, SizeCode, CostPerGram, PMC, SMC, TotalCost, TransferTag, CreateUser, ModifyUser, CreateDate, ModifyDate
          ) VALUES (
            @TransferNo, @InventoryCode, @ClassCode, @CategoryCode, @CatalogueCode, @Description, 
            @QtyonHand, @Weight, @SizeCode, @CostPerGram, @PMC, @SMC, @TotalCost, 0, @CreateUser, @CreateUser, GETDATE(), GETDATE()
          )
        `);
        
      // Movement logic
      const movementReq = new sql.Request(transaction);
      await movementReq
        .input("TransferNo", sql.VarChar, transferNo)
        .input("InventoryCode", sql.VarChar, item.InventoryCode)
        .input("ToLocation", sql.VarChar, toLocation)
        .input("Description", sql.VarChar, item.Description != null ? String(item.Description) : '')
        .input("Qty", sql.Int, Number(item.Qty) || 1)
        .input("Weight", sql.Decimal(18,3), Number(item.Weight) || 0)
        .input("SizeCode", sql.VarChar, item.SizeCode != null ? String(item.SizeCode) : '0')
        .input("PMC", sql.Decimal(18,2), Number(item.PMC) || 0)
        .input("SMC", sql.Decimal(18,2), Number(item.SMC) || 0)
        .input("CostPerGram", sql.Decimal(18,2), Number(item.CostPerGram) || 0)
        .input("TotalCost", sql.Decimal(18,2), Number(item.TotalCost) || 0)
        .input("UserId", sql.VarChar, userId || 'sys')
        .query(`
          INSERT INTO tblInventoryMovement (
            LocationCode, InventoryCode, Description, Status, 
            RefNumber, RefDate, Qty, Weight, PMC, SMC, 
            CostPerGram, TotalCost, CreateDate, CreateUser
          ) VALUES (
            @ToLocation, @InventoryCode, @Description, 'TO',
            @TransferNo, GETDATE(), @Qty, @Weight, @PMC, @SMC,
            @CostPerGram, @TotalCost, GETDATE(), @UserId
          );
        `);
    }

    await transaction.commit();
    res.json({ success: true, message: "Inventory Transferred Successfully.", transferNo });
  } catch (error) {
    console.error("Transfer Out Error:", error.message, error);
    if (transaction) {
      try { await transaction.rollback(); } catch (e) { }
    }
    res.status(500).json({ success: false, message: error.message, error: error.message });
  }
});

module.exports = router;
