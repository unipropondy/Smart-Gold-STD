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

// 1. GET /api/traytransfer/trays
// LOV for Traymaster (matching btnTraySearch_Click and btnOldTraySearch_Click in frmTrayTransfer.vb)
// Queries tblTraymaster: TrayId, TrayName, SalesManName, IsCustomerTray
router.get("/trays", async (req, res) => {
  try {
    const pool = await getPool(req);
    const query = `
      SELECT 
        RTRIM(LTRIM(TrayId)) AS TrayId, 
        RTRIM(LTRIM(TrayName)) AS TrayName, 
        RTRIM(LTRIM(ISNULL(SalesManName, ''))) AS SalesManName, 
        RTRIM(LTRIM(ISNULL(IsCustomerTray, 'N'))) AS IsCustomerTray
      FROM tblTraymaster
      ORDER BY TrayName ASC, TrayId ASC
    `;
    const result = await pool.request().query(query);
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Trays Error:", err);
    res.status(500).json({ success: false, message: "Database error", error: err.message });
  }
});

// 2. GET /api/traytransfer/inventory_by_tray?trayId=...
// Fetch all inventory items currently on a tray with QtyOnHand > 0
// (matching subGetInventoryByTray -> objClsInventory.GetInventoryDetailByTray in frmTrayTransfer.vb)
router.get("/inventory_by_tray", async (req, res) => {
  try {
    const { trayId } = req.query;
    if (!trayId || !trayId.trim()) {
      return res.status(400).json({ success: false, message: "Tray ID is required." });
    }

    const pool = await getPool(req);
    const query = `
      SELECT 
        RTRIM(LTRIM(InventoryCode)) AS InventoryCode, 
        RTRIM(LTRIM(ISNULL(Description, ''))) AS Description, 
        ISNULL(Weight, 0) AS Weight,
        ISNULL(QtyOnHand, 1) AS QtyOnHand
      FROM tblInventory
      WHERE RTRIM(LTRIM(TrayId)) = @TrayId 
        AND ISNULL(QtyOnHand, 0) > 0
      ORDER BY InventoryCode ASC
    `;
    const result = await pool.request()
      .input("TrayId", sql.VarChar(50), trayId.trim())
      .query(query);

    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Inventory By Tray Error:", err);
    res.status(500).json({ success: false, message: "Database error", error: err.message });
  }
});

// 3. GET /api/traytransfer/inventory?code=...
// Fetch a single inventory item by code (matching subGetInventoryDetail in frmTrayTransfer.vb)
// Checks QtyOnHand <> 0 and whether already sold
router.get("/inventory", async (req, res) => {
  try {
    const { code } = req.query;
    if (!code || !code.trim()) {
      return res.status(400).json({ success: false, message: "Inventory Code is required." });
    }

    const pool = await getPool(req);
    const result = await pool.request()
      .input("Code", sql.VarChar(50), code.trim())
      .query(`
        SELECT TOP 1
          InventoryCode, 
          Description, 
          Weight,
          QtyOnHand,
          TrayId
        FROM tblInventory
        WHERE RTRIM(LTRIM(CAST(InventoryCode AS VARCHAR(50)))) = @Code
      `);

    if (result.recordset.length === 0) {
      return res.json({ success: false, message: "Inventory does not exists" });
    }

    const item = result.recordset[0];
    const qty = parseFloat(item.QtyOnHand) || 0;
    if (qty <= 0) {
      return res.json({ success: false, message: "Inventory already sold" });
    }

    res.json({ 
      success: true, 
      data: {
        InventoryCode: (item.InventoryCode || '').trim(),
        Description: (item.Description || '').trim(),
        Weight: parseFloat(item.Weight) || 0,
        QtyOnHand: qty,
        TrayId: (item.TrayId || '').trim()
      } 
    });
  } catch (err) {
    console.error("Fetch Single Inventory Error:", err);
    res.status(500).json({ success: false, message: "Database error", error: err.message });
  }
});

// POST /api/traytransfer/save
// Perform Tray Transfer (matching btnSave_Click in frmTrayTransfer.vb)
// Validates: Inventory code required, New tray required, Old tray <> New tray
// Updates tblInventory with New TrayId, ModifyDate, ModifyUser
router.post("/save", async (req, res) => {
  let pool;
  try {
    pool = await getPool(req);
  } catch (poolErr) {
    return res.status(500).json({ success: false, message: poolErr.message });
  }

  const transaction = new sql.Transaction(pool);

  try {
    const {
      newTrayId,
      oldTrayId,
      remarks,
      inventoryCodes,
      userId
    } = req.body;

    const sNewTray = (newTrayId || "").trim();
    const sOldTray = (oldTrayId || "").trim();
    const sUser = (userId || "admin").trim();

    if (!inventoryCodes || !Array.isArray(inventoryCodes) || inventoryCodes.length === 0) {
      return res.status(400).json({ success: false, message: "Please enter Inventorycode to Transfer." });
    }
    if (!sNewTray) {
      return res.status(400).json({ success: false, message: "Please select New tray to Transfer." });
    }
    if (sOldTray && sNewTray.toLowerCase() === sOldTray.toLowerCase()) {
      return res.status(400).json({ success: false, message: "Source and Destination trays cannot be the same." });
    }

    await transaction.begin();

    for (const rawCode of inventoryCodes) {
      const code = (rawCode || "").trim();
      if (!code) continue;

      const reqUpdate = new sql.Request(transaction);
      reqUpdate.input("NewTrayId", sql.VarChar(50), sNewTray);
      reqUpdate.input("InventoryCode", sql.VarChar(50), code);
      reqUpdate.input("ModifyUser", sql.VarChar(50), sUser);

      await reqUpdate.query(`
        UPDATE tblInventory 
        SET 
          TrayId = @NewTrayId,
          ModifyDate = GETDATE(),
          ModifyUser = @ModifyUser
        WHERE RTRIM(LTRIM(CAST(InventoryCode AS VARCHAR(50)))) = @InventoryCode
      `);
    }

    await transaction.commit();
    res.json({ success: true, message: "Tray Transferred successfully." });

  } catch (err) {
    try {
      if (transaction) await transaction.rollback();
    } catch (rbErr) {}
    console.error("Tray Transfer Save Error:", err);
    res.status(500).json({ success: false, message: "Error in updation.", error: err.message });
  }
});

module.exports = router;
