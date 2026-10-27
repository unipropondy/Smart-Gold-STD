const express = require("express");
const router = express.Router();
const { sql } = require("../db");

// GET /api/transferin/fetch
// Fetch Transfer Header and Details
router.get("/fetch", async (req, res) => {
  try {
    const { transferNo, location } = req.query;
    if (!transferNo) {
      return res.status(400).json({ success: false, message: "Transfer Number is required." });
    }

    const pool = req.pool;

    // Fetch Header
    let headerQuery = `
      SELECT 
        TransferNo, 
        FromLocation, 
        TransferDate,
        Remarks,
        TotalWeight,
        TotalCost,
        TotalPCS,
        TotalPMC,
        TotalSMC,
        FromLocStatus,
        ToLocStatus
      FROM tblInventoryTransferHeader
      WHERE TransferNo = @TransferNo
    `;
    
    // Add location check if provided (based on VB code logic 'and ToLocation = gthisLocation')
    if (location && location !== 'HQ') {
       headerQuery += ` AND ToLocation = @ToLocation`;
    }

    const headerResult = await pool.request()
      .input("TransferNo", sql.VarChar, transferNo.trim())
      .input("ToLocation", sql.VarChar, location ? location.trim() : '')
      .query(headerQuery);

    if (headerResult.recordset.length === 0) {
      return res.json({ success: false, message: "No data found in header or invalid location." });
    }

    const header = headerResult.recordset[0];

    // Fetch Details
    const detailQuery = `
      SELECT 
        TransferNo,
        InventoryCode,
        Description,
        QtyOnHand,
        Weight,
        SizeCode,
        CostPerGram,
        PMC,
        SMC,
        PCS,
        TotalCost,
        TransferTag = CAST(0 AS BIT) -- Add TransferTag field initialized to false
      FROM tblInventoryTransferDetail
      WHERE TransferNo = @TransferNo
    `;

    const detailResult = await pool.request()
      .input("TransferNo", sql.VarChar, transferNo.trim())
      .query(detailQuery);

    if (detailResult.recordset.length === 0) {
      return res.json({ success: false, message: "No data found in detail." });
    }

    res.json({ 
      success: true, 
      data: {
        header: header,
        details: detailResult.recordset
      }
    });
  } catch (err) {
    console.error("Fetch Transfer In Error:", err);
    res.status(500).json({ success: false, message: "Database error", error: err.message });
  }
});

// POST /api/transferin/accept
// Accept Transfer In
router.post("/accept", async (req, res) => {
  const pool = req.pool;
  const transaction = new sql.Transaction(pool);

  try {
    const { transferNo, userId, location, items } = req.body;
    
    if (!transferNo) {
      return res.status(400).json({ success: false, message: "Transfer Number is required." });
    }
    
    if (!items || items.length === 0) {
      return res.status(400).json({ success: false, message: "No items to accept." });
    }

    await transaction.begin();

    // 1. Update tblInventoryTransferHeader status to accepted (2)
    const updateHeaderReq = new sql.Request(transaction);
    await updateHeaderReq
      .input("TransferNo", sql.VarChar, transferNo)
      .query(`
        UPDATE tblInventoryTransferHeader
        SET ToLocStatus = '2', ModifyDate = GETDATE(), ModifyUser = @ModifyUser
        WHERE TransferNo = @TransferNo
      `);

    // We simulate the stock update logic here. In a real system, there might be complex 
    // triggers or stored procedures for 'vwInventoryStockTransfer'. 
    // Based on standard inventory systems, a transfer usually updates the location of existing items.
    
    for (const item of items) {
      const updateItemReq = new sql.Request(transaction);
      
      // Update inventory location to current location
      // In a real system, you might also insert if it doesn't exist, but typically 
      // transfers move existing inventory. We will update LocationCode and TrayId if needed.
      await updateItemReq
        .input("InventoryCode", sql.VarChar, item.InventoryCode)
        .input("LocationCode", sql.VarChar, location)
        .input("TransferNo", sql.VarChar, transferNo)
        .input("UserId", sql.VarChar, userId || 'sys')
        .query(`
          UPDATE tblInventory
          SET 
            LocationCode = @LocationCode,
            ModifyDate = GETDATE(),
            ModifyUser = @UserId
          WHERE InventoryCode = @InventoryCode;
          
          -- Insert movement history
          INSERT INTO tblInventoryMovement (
            LocationCode, InventoryCode, Description, TransType, 
            TransNo, TransDate, Qty, Weight, SizeCode, PMC, SMC, 
            CostPerGram, TotalCost, EntryDate, EntryUser
          ) VALUES (
            @LocationCode, @InventoryCode, (SELECT Description FROM tblInventory WHERE InventoryCode = @InventoryCode), 'TI',
            @TransferNo, GETDATE(), ${item.QtyOnHand || 1}, ${item.Weight || 0}, '${item.SizeCode || ''}', ${item.PMC || 0}, ${item.SMC || 0},
            ${item.CostPerGram || 0}, ${item.TotalCost || 0}, GETDATE(), @UserId
          );
        `);
    }

    await transaction.commit();
    res.json({ success: true, message: "Inventory Transferred Successfully." });

  } catch (err) {
    if (transaction) await transaction.rollback();
    console.error("Accept Transfer In Error:", err);
    res.status(500).json({ success: false, message: "Error accepting transfer.", error: err.message });
  }
});

module.exports = router;
