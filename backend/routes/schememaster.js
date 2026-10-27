const express = require("express");
const router = express.Router();
const { sql } = require("../db");

// Helper to ensure table exists
async function ensureTableExists(pool) {
  const checkTableQuery = `
    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='tblSchemeMaster' AND xtype='U')
    BEGIN
        CREATE TABLE tblSchemeMaster (
            Code VARCHAR(50) PRIMARY KEY,
            Description VARCHAR(100),
            Remarks VARCHAR(255),
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
    console.error("Error ensuring tblSchemeMaster exists:", err);
  }
}

// GET schema
router.get("/schema", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query("SELECT COLUMN_NAME, CHARACTER_MAXIMUM_LENGTH, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tblSchemeMaster'");
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    res.status(500).json({ success: false, error: err.message });
  }
});

// GET all schemes
router.get("/", async (req, res) => {
  try {
    const pool = req.pool;
    await ensureTableExists(pool);

    const result = await pool.request().query("SELECT * FROM tblSchemeMaster ORDER BY Code");
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Scheme Master Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// POST insert new scheme
router.post("/", async (req, res) => {
  try {
    const pool = req.pool;
    await ensureTableExists(pool);
    const { Code, Description, Remarks } = req.body;

    if (!Code || !Description) {
      return res.status(400).json({ success: false, message: "Code and Description are required." });
    }

    // Check if exists
    const checkResult = await pool.request()
      .input('Code', sql.VarChar, Code)
      .query("SELECT Code FROM tblSchemeMaster WHERE Code = @Code");
    
    if (checkResult.recordset.length > 0) {
      return res.status(400).json({ success: false, message: "Code already exists." });
    }

    const insertQuery = `
      INSERT INTO tblSchemeMaster (
        Code, Description, Remarks,
        CreateUser, CreateDate, ModifyUser, ModifyDate
      ) VALUES (
        @Code, @Description, @Remarks,
        'ups', GETDATE(), 'ups', GETDATE()
      )
    `;

    await pool.request()
      .input('Code', sql.VarChar, Code.substring(0, 50))
      .input('Description', sql.VarChar, Description.substring(0, 100))
      .input('Remarks', sql.VarChar, Remarks ? Remarks.substring(0, 255) : '')
      .query(insertQuery);

    res.json({ success: true, message: "Scheme created successfully." });
  } catch (err) {
    console.error("Insert Scheme Error:", err);
    res.status(500).json({ success: false, message: "Failed to create scheme", error: err.message });
  }
});

// PUT update scheme
router.put("/", async (req, res) => {
  try {
    const pool = req.pool;
    const { Code, Description, Remarks } = req.body;
    
    if (!Code) return res.status(400).json({ success: false, message: "Code is required" });

    const updateQuery = `
      UPDATE tblSchemeMaster SET 
        Description = @Description,
        Remarks = @Remarks,
        ModifyUser = 'sys',
        ModifyDate = GETDATE()
      WHERE Code = @Code
    `;

    const result = await pool.request()
      .input('Code', sql.VarChar, Code)
      .input('Description', sql.VarChar, Description ? Description.substring(0, 100) : '')
      .input('Remarks', sql.VarChar, Remarks || '')
      .query(updateQuery);

    if (result.rowsAffected[0] === 0) {
      return res.status(404).json({ success: false, message: "Scheme not found." });
    }

    res.json({ success: true, message: "Scheme updated successfully." });
  } catch (err) {
    console.error("Update Scheme Error:", err);
    res.status(500).json({ success: false, message: "Failed to update scheme", error: err.message });
  }
});

// DELETE scheme
router.delete("/", async (req, res) => {
  try {
    const pool = req.pool;
    const { id } = req.query; // Use query param for DELETE
    console.log(`[DELETE] Scheme Code requested: '${id}'`);

    const deleteQuery = "DELETE FROM tblSchemeMaster WHERE Code = @Code";
    const result = await pool.request()
      .input('Code', sql.VarChar, id)
      .query(deleteQuery);

    console.log(`[DELETE] rowsAffected:`, result.rowsAffected);
    if (result.rowsAffected[0] === 0) {
      return res.status(404).json({ success: false, message: "Scheme not found." });
    }

    res.json({ success: true, message: "Scheme deleted successfully." });
  } catch (err) {
    console.error("Delete Scheme Error:", err);
    res.status(500).json({ success: false, message: "Failed to delete scheme", error: err.message });
  }
});

module.exports = router;
