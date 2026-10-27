const express = require("express");
const router = express.Router();
const { sql } = require("../db");

// Helper to ensure table exists
async function ensureTableExists(pool) {
  const checkTableQuery = `
    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='tblTrayGroup' AND xtype='U')
    BEGIN
        CREATE TABLE tblTrayGroup (
            TrayGroupCode VARCHAR(50) PRIMARY KEY,
            TrayGroupName VARCHAR(100),
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
    console.error("Error ensuring tblTrayGroup exists:", err);
  }
}

// GET all tray groups
router.get("/", async (req, res) => {
  try {
    const pool = req.pool;
    await ensureTableExists(pool);

    const result = await pool.request().query("SELECT * FROM tblTrayGroup ORDER BY TrayGroupCode ASC");
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Tray Group Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

module.exports = router;
