const express = require("express");
const router = express.Router();
const { sql } = require("../db");

// Helper function to generate Transfer No (simulating fncGetTransferNo)
const generateTransferNo = async (pool) => {
  // In a real system, this might use a sequence or a specific table for running numbers.
  // We'll generate one based on timestamp for uniqueness in this mock, or query max.
  const query = `SELECT ISNULL(MAX(CAST(TransferNo AS BIGINT)), 10000) + 1 AS NextNo FROM tblInventoryTransferHeader WHERE ISNUMERIC(TransferNo) = 1`;
  const result = await pool.request().query(query);
  return result.recordset[0].NextNo.toString();
};

// GET /api/transferout/locations
// Fetch locations for LOV (To Location)
router.get("/locations", async (req, res) => {
  try {
    const pool = req.pool;
    // As per VB: Select * from tblLocation where locationcode not in(select locationcode from tblcompany) Order by SortCode
    const query = `
      SELECT RTRIM(LocationCode) as LocationCode, RTRIM(LocationName) as LocationName 
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

// GET /api/transferout/inventory/:code
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

// GET /api/transferout/fetch
// Fetch existing Transfer Out details
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
      FROM tblInventoryTransferHeader 
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
      FROM tblInventoryTransferDetail 
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

// POST /api/transferout/transfer
// Create a new Transfer Out
router.post("/transfer", async (req, res) => {
  let transaction;
  try {
    const { 
      transferDate, fromLocation, toLocation, remarks, 
      totalWeight, totalPcs, totalCost, totalPMC, totalSMC,
      items, userId 
    } = req.body;

    if (!toLocation || !items || items.length === 0) {
      return res.status(400).json({ success: false, message: "Invalid payload." });
    }

    const pool = req.pool;
    transaction = new sql.Transaction(pool);
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
      .input("CreateUser", sql.VarChar, userId || 'sys')
      .query(`
        INSERT INTO tblInventoryTransferHeader (
          TransferNo, TransferDate, FromLocation, ToLocation, TotalWeight, TotalPcs, 
          TotalCost, TotalPMC, TotalSMC, FromLocStatus, ToLocStatus, Remarks, CreateUser, ModifyUser, CreateDate, ModifyDate
        ) VALUES (
          @TransferNo, @TransferDate, @FromLocation, @ToLocation, @TotalWeight, @TotalPcs, 
          @TotalCost, @TotalPMC, @TotalSMC, @FromLocStatus, @ToLocStatus, @Remarks, @CreateUser, @CreateUser, GETDATE(), GETDATE()
        )
      `);

    // Process Items
    for (const item of items) {
      const insertDetailReq = new sql.Request(transaction);
      
      // Insert Detail
      await insertDetailReq
        .input("TransferNo", sql.VarChar, transferNo)
        .input("InventoryCode", sql.VarChar, item.InventoryCode)
        .input("ClassCode", sql.VarChar, item.ClassCode || '')
        .input("CategoryCode", sql.VarChar, item.CategoryCode || '')
        .input("CatalogueCode", sql.VarChar, item.CatalogueCode || '')
        .input("Description", sql.VarChar, item.Description || '')
        .input("QtyonHand", sql.Int, item.Qty || 1)
        .input("Weight", sql.Decimal(18,3), item.Weight || 0)
        .input("SizeCode", sql.VarChar, item.SizeCode || '')
        .input("CostPerGram", sql.Decimal(18,2), item.CostPerGram || 0)
        .input("PMC", sql.Decimal(18,2), item.PMC || 0)
        .input("SMC", sql.Decimal(18,2), item.SMC || 0)
        .input("TotalCost", sql.Decimal(18,2), item.TotalCost || 0)
        .input("CreateUser", sql.VarChar, userId || 'sys')
        .query(`
          INSERT INTO tblInventoryTransferDetail (
            TransferNo, InventoryCode, ClassCode, CategoryCode, CatalogueCode, Description, 
            QtyonHand, Weight, SizeCode, CostPerGram, PMC, SMC, TotalCost, TransferTag, CreateUser, ModifyUser, CreateDate, ModifyDate
          ) VALUES (
            @TransferNo, @InventoryCode, @ClassCode, @CategoryCode, @CatalogueCode, @Description, 
            @QtyonHand, @Weight, @SizeCode, @CostPerGram, @PMC, @SMC, @TotalCost, 0, @CreateUser, @CreateUser, GETDATE(), GETDATE()
          )
        `);
        
      // Movement logic (VB: objTran.fncInventoryMovementSave TransType='TO')
      const movementReq = new sql.Request(transaction);
      await movementReq
        .input("TransferNo", sql.VarChar, transferNo)
        .input("InventoryCode", sql.VarChar, item.InventoryCode)
        .input("ToLocation", sql.VarChar, toLocation)
        .input("Description", sql.VarChar, item.Description || '')
        .input("UserId", sql.VarChar, userId || 'sys')
        .query(`
          INSERT INTO tblInventoryMovement (
            LocationCode, InventoryCode, Description, TransType, 
            TransNo, TransDate, Qty, Weight, SizeCode, PMC, SMC, 
            CostPerGram, TotalCost, EntryDate, EntryUser
          ) VALUES (
            @ToLocation, @InventoryCode, @Description, 'TO',
            @TransferNo, GETDATE(), ${item.Qty || 1}, ${item.Weight || 0}, '${item.SizeCode || ''}', ${item.PMC || 0}, ${item.SMC || 0},
            ${item.CostPerGram || 0}, ${item.TotalCost || 0}, GETDATE(), @UserId
          );
        `);
    }

    await transaction.commit();
    res.json({ success: true, message: "Inventory Transferred Successfully.", transferNo });
  } catch (error) {
    if (transaction) await transaction.rollback();
    console.error("Transfer Out Error:", error);
    res.status(500).json({ success: false, message: "Error during transfer.", error: error.message });
  }
});

module.exports = router;
