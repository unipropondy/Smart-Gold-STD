const express = require("express");
const router = express.Router();
const { sql } = require("../db");

// GET /api/traytransfer/trays
// LOV for Traymaster
router.get("/trays", async (req, res) => {
  try {
    const pool = req.pool;
    const query = `
      SELECT 
        TrayId, 
        TrayName, 
        SalesManName, 
        IsCustomerTray
      FROM tblTraymaster
      ORDER BY TrayName ASC
    `;
    const result = await pool.request().query(query);
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Trays Error:", err);
    res.status(500).json({ success: false, message: "Database error", error: err.message });
  }
});

// GET /api/traytransfer/inventory_by_tray?trayId=...
// Fetch all available inventory for an old tray
router.get("/inventory_by_tray", async (req, res) => {
  try {
    const { trayId } = req.query;
    if (!trayId) {
      return res.status(400).json({ success: false, message: "Tray ID is required." });
    }

    const pool = req.pool;
    const query = `
      SELECT 
        InventoryCode, 
        Description, 
        Weight,
        QtyOnHand
      FROM tblInventory
      WHERE TrayId = @TrayId AND ISNULL(QtyOnHand, 0) > 0
      ORDER BY InventoryCode ASC
    `;
    const result = await pool.request()
      .input("TrayId", sql.VarChar, trayId.trim())
      .query(query);

    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Inventory By Tray Error:", err);
    res.status(500).json({ success: false, message: "Database error", error: err.message });
  }
});

// GET /api/traytransfer/inventory?code=...
// Fetch a single inventory item by code
router.get("/inventory", async (req, res) => {
  try {
    const { code } = req.query;
    if (!code) {
      return res.status(400).json({ success: false, message: "Inventory Code is required." });
    }

    const pool = req.pool;
    const query = `
      SELECT 
        InventoryCode, 
        Description, 
        Weight,
        QtyOnHand,
        TrayId
      FROM tblInventory
      WHERE InventoryCode = @Code
    `;
    const result = await pool.request()
      .input("Code", sql.VarChar, code.trim())
      .query(query);

    if (result.recordset.length === 0) {
      return res.json({ success: false, message: "Inventory does not exists" });
    }

    const item = result.recordset[0];
    if (parseFloat(item.QtyOnHand || 0) <= 0) {
      return res.json({ success: false, message: "Inventory already sold" });
    }

    res.json({ success: true, data: item });
  } catch (err) {
    console.error("Fetch Single Inventory Error:", err);
    res.status(500).json({ success: false, message: "Database error", error: err.message });
  }
});

// POST /api/traytransfer/save
// Perform Tray Transfer
router.post("/save", async (req, res) => {
  const pool = req.pool;
  const transaction = new sql.Transaction(pool);

  try {
    const {
      newTrayId,
      oldTrayId,
      remarks,
      inventoryCodes,
      userId
    } = req.body;

    if (!newTrayId) {
      return res.status(400).json({ success: false, message: "Please select New tray to Transfer." });
    }
    if (!inventoryCodes || inventoryCodes.length === 0) {
      return res.status(400).json({ success: false, message: "Please enter Inventorycode to Transfer." });
    }

    await transaction.begin();

    for (const code of inventoryCodes) {
      const reqUpdate = new sql.Request(transaction);
      
      // The VB code calls objClsTray.TrayTransfer and objClsTray.RFIDItemStatusUpdate
      // Standard procedure is updating tblInventory's TrayId
      await reqUpdate
        .input("NewTrayId", sql.VarChar, newTrayId.trim())
        .input("OldTrayId", sql.VarChar, oldTrayId ? oldTrayId.trim() : "")
        .input("Remarks", sql.VarChar, remarks || "")
        .input("ModifyUser", sql.VarChar, (userId || "sys").trim())
        .input("InventoryCode", sql.VarChar, code.trim())
        .query(`
          UPDATE tblInventory 
          SET 
            TrayId = @NewTrayId,
            ModifyDate = GETDATE(),
            ModifyUser = @ModifyUser
          WHERE InventoryCode = @InventoryCode
        `);
    }

    await transaction.commit();
    res.json({ success: true, message: "Tray Transferred successfully." });

  } catch (err) {
    if (transaction) await transaction.rollback();
    console.error("Tray Transfer Save Error:", err);
    res.status(500).json({ success: false, message: "Error in updation.", error: err.message });
  }
});

module.exports = router;
