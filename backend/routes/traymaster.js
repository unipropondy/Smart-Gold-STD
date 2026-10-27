const express = require("express");
const router = express.Router();
const { sql } = require("../db");

// Helper to ensure table exists
async function ensureTableExists(pool) {
  const checkTableQuery = `
    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='tblTrayMaster' AND xtype='U')
    BEGIN
        CREATE TABLE tblTrayMaster (
            TrayID VARCHAR(50) PRIMARY KEY,
            TrayName VARCHAR(100),
            TrayGroup VARCHAR(50),
            SalesManID VARCHAR(50),
            SalesManName VARCHAR(100),
            IsCustomerTray VARCHAR(1) DEFAULT 'N',
            CreateUser VARCHAR(50) DEFAULT 'sys',
            CreateDate DATETIME DEFAULT GETDATE(),
            ModifyUser VARCHAR(50) DEFAULT 'sys',
            ModifyDate DATETIME DEFAULT GETDATE()
        )
    END
  `;
  try {
    await pool.request().query(checkTableQuery);
  } catch (err) {
    console.error("Error ensuring tblTrayMaster exists:", err);
  }
}

// GET all trays
router.get("/", async (req, res) => {
  try {
    const pool = req.pool;
    await ensureTableExists(pool);

    // Check if table has records, or check both tblTraymaster / tblTrayMaster
    let result = await pool.request().query("SELECT * FROM tblTrayMaster ORDER BY TrayID ASC");
    
    // If empty, check if tblTraymaster with different case has records
    if (!result.recordset || result.recordset.length === 0) {
      try {
        result = await pool.request().query("SELECT * FROM tblTraymaster ORDER BY TrayId ASC");
      } catch (e) {
        // Ignore fallback error
      }
    }

    // If still empty, ensure a default tray exists (like 'DEF' used in directpurchase reference code)
    if (!result.recordset || result.recordset.length === 0) {
      try {
        await pool.request().query(`
          INSERT INTO tblTrayMaster (TrayID, TrayName, TrayGroup, SalesManID, SalesManName, IsCustomerTray, CreateUser, CreateDate, ModifyUser, ModifyDate)
          VALUES ('DEF', 'Default Tray', 'GENERAL', '', '', 'N', 'sys', GETDATE(), 'sys', GETDATE())
        `);
        result = await pool.request().query("SELECT * FROM tblTrayMaster ORDER BY TrayID ASC");
      } catch (insertErr) {
        console.error("Error inserting default tray:", insertErr);
      }
    }

    // Normalize property names so both TrayID/TrayId and TrayName/Description work seamlessly
    const normalized = (result.recordset || []).map((t) => ({
      ...t,
      TrayId: t.TrayID || t.TrayId || '',
      TrayID: t.TrayID || t.TrayId || '',
      TrayName: t.TrayName || t.Trayname || t.Description || '',
      Description: t.TrayName || t.Trayname || t.Description || '',
    }));

    res.json({ success: true, data: normalized });
  } catch (err) {
    console.error("Fetch Tray Master Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// POST insert new tray
router.post("/", async (req, res) => {
  try {
    const pool = req.pool;
    await ensureTableExists(pool);
    const { TrayID, TrayName, TrayGroup, SalesManID, SalesManName, IsCustomerTray } = req.body;

    if (!TrayID) {
      return res.status(400).json({ success: false, message: "TrayID is required." });
    }

    // Check if exists
    const checkResult = await pool.request()
      .input('TrayID', sql.VarChar, TrayID)
      .query("SELECT TrayID FROM tblTrayMaster WHERE TrayID = @TrayID");
    
    if (checkResult.recordset.length > 0) {
      return res.status(400).json({ success: false, message: "TrayID already exists." });
    }

    const insertQuery = `
      INSERT INTO tblTrayMaster (
        TrayID, TrayName, TrayGroup, SalesManID, SalesManName, IsCustomerTray,
        CreateUser, CreateDate, ModifyUser, ModifyDate
      ) VALUES (
        @TrayID, @TrayName, @TrayGroup, @SalesManID, @SalesManName, @IsCustomerTray,
        'sys', GETDATE(), 'sys', GETDATE()
      )
    `;

    await pool.request()
      .input('TrayID', sql.VarChar, TrayID.substring(0, 50))
      .input('TrayName', sql.VarChar, TrayName ? TrayName.substring(0, 100) : '')
      .input('TrayGroup', sql.VarChar, TrayGroup ? TrayGroup.substring(0, 50) : '')
      .input('SalesManID', sql.VarChar, SalesManID ? SalesManID.substring(0, 50) : '')
      .input('SalesManName', sql.VarChar, SalesManName ? SalesManName.substring(0, 100) : '')
      .input('IsCustomerTray', sql.VarChar, IsCustomerTray ? IsCustomerTray.substring(0, 1) : 'N')
      .query(insertQuery);

    res.json({ success: true, message: "Tray created successfully." });
  } catch (err) {
    console.error("Insert Tray Error:", err);
    res.status(500).json({ success: false, message: "Failed to create tray", error: err.message });
  }
});

// PUT update tray
router.put("/", async (req, res) => {
  try {
    const pool = req.pool;
    const { TrayID, TrayName, TrayGroup, SalesManID, SalesManName, IsCustomerTray } = req.body;
    
    if (!TrayID) return res.status(400).json({ success: false, message: "TrayID is required" });

    const updateQuery = `
      UPDATE tblTrayMaster SET 
        TrayName = @TrayName,
        TrayGroup = @TrayGroup,
        SalesManID = @SalesManID,
        SalesManName = @SalesManName,
        IsCustomerTray = @IsCustomerTray,
        ModifyUser = 'sys',
        ModifyDate = GETDATE()
      WHERE TrayID = @TrayID
    `;

    const result = await pool.request()
      .input('TrayID', sql.VarChar, TrayID.substring(0, 50))
      .input('TrayName', sql.VarChar, TrayName ? TrayName.substring(0, 100) : '')
      .input('TrayGroup', sql.VarChar, TrayGroup ? TrayGroup.substring(0, 50) : '')
      .input('SalesManID', sql.VarChar, SalesManID ? SalesManID.substring(0, 50) : '')
      .input('SalesManName', sql.VarChar, SalesManName ? SalesManName.substring(0, 100) : '')
      .input('IsCustomerTray', sql.VarChar, IsCustomerTray ? IsCustomerTray.substring(0, 1) : 'N')
      .query(updateQuery);

    if (result.rowsAffected[0] === 0) {
      return res.status(404).json({ success: false, message: "Tray not found." });
    }

    res.json({ success: true, message: "Tray updated successfully." });
  } catch (err) {
    console.error("Update Tray Error:", err);
    res.status(500).json({ success: false, message: "Failed to update tray", error: err.message });
  }
});

// DELETE tray
router.delete("/", async (req, res) => {
  try {
    const pool = req.pool;
    const { id } = req.query;

    const deleteQuery = "DELETE FROM tblTrayMaster WHERE TrayID = @TrayID";
    const result = await pool.request()
      .input('TrayID', sql.VarChar, id)
      .query(deleteQuery);

    if (result.rowsAffected[0] === 0) {
      return res.status(404).json({ success: false, message: "Tray not found." });
    }

    res.json({ success: true, message: "Tray deleted successfully." });
  } catch (err) {
    console.error("Delete Tray Error:", err);
    res.status(500).json({ success: false, message: "Failed to delete tray", error: err.message });
  }
});

module.exports = router;
