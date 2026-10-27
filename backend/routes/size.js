const express = require("express");
const router = express.Router();
const { sql } = require("../db");

// Helper to ensure table exists
async function ensureTableExists(pool) {
  const checkTableQuery = `
    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='tblSize' AND xtype='U')
    BEGIN
        CREATE TABLE tblSize (
            SizeCode VARCHAR(50) PRIMARY KEY,
            CategoryCode VARCHAR(50),
            SizeType VARCHAR(50),
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
    console.error("Error ensuring tblSize exists:", err);
  }
}

// GET all sizes
router.get("/", async (req, res) => {
  try {
    const pool = req.pool;
    await ensureTableExists(pool);

    const result = await pool.request().query("SELECT * FROM tblSize ORDER BY SizeCode ASC");
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Size Master Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// POST insert new size
router.post("/", async (req, res) => {
  try {
    const pool = req.pool;
    await ensureTableExists(pool);
    const { SizeCode, CategoryCode, SizeType, Remarks } = req.body;

    if (!SizeCode) {
      return res.status(400).json({ success: false, message: "SizeCode is required." });
    }

    // Check if exists
    const checkResult = await pool.request()
      .input('SizeCode', sql.VarChar, SizeCode)
      .query("SELECT SizeCode FROM tblSize WHERE SizeCode = @SizeCode");
    
    if (checkResult.recordset.length > 0) {
      return res.status(400).json({ success: false, message: "SizeCode already exists." });
    }

    const insertQuery = `
      INSERT INTO tblSize (
        SizeCode, CategoryCode, SizeType, Remarks,
        CreateUser, CreateDate, ModifyUser, ModifyDate
      ) VALUES (
        @SizeCode, @CategoryCode, @SizeType, @Remarks,
        'sys', GETDATE(), 'sys', GETDATE()
      )
    `;

    await pool.request()
      .input('SizeCode', sql.VarChar, SizeCode.substring(0, 50))
      .input('CategoryCode', sql.VarChar, CategoryCode ? CategoryCode.substring(0, 50) : '')
      .input('SizeType', sql.VarChar, SizeType ? SizeType.substring(0, 50) : '')
      .input('Remarks', sql.VarChar, Remarks ? Remarks.substring(0, 255) : '')
      .query(insertQuery);

    res.json({ success: true, message: "Size created successfully." });
  } catch (err) {
    console.error("Insert Size Error:", err);
    res.status(500).json({ success: false, message: "Failed to create size", error: err.message });
  }
});

// PUT update size
router.put("/", async (req, res) => {
  try {
    const pool = req.pool;
    const { SizeCode, CategoryCode, SizeType, Remarks } = req.body;
    
    if (!SizeCode) return res.status(400).json({ success: false, message: "SizeCode is required" });

    const updateQuery = `
      UPDATE tblSize SET 
        CategoryCode = @CategoryCode,
        SizeType = @SizeType,
        Remarks = @Remarks,
        ModifyUser = 'sys',
        ModifyDate = GETDATE()
      WHERE SizeCode = @SizeCode
    `;

    const result = await pool.request()
      .input('SizeCode', sql.VarChar, SizeCode.substring(0, 50))
      .input('CategoryCode', sql.VarChar, CategoryCode ? CategoryCode.substring(0, 50) : '')
      .input('SizeType', sql.VarChar, SizeType ? SizeType.substring(0, 50) : '')
      .input('Remarks', sql.VarChar, Remarks ? Remarks.substring(0, 255) : '')
      .query(updateQuery);

    if (result.rowsAffected[0] === 0) {
      return res.status(404).json({ success: false, message: "Size not found." });
    }

    res.json({ success: true, message: "Size updated successfully." });
  } catch (err) {
    console.error("Update Size Error:", err);
    res.status(500).json({ success: false, message: "Failed to update size", error: err.message });
  }
});

// DELETE size
router.delete("/", async (req, res) => {
  try {
    const pool = req.pool;
    const { id } = req.query;

    const deleteQuery = "DELETE FROM tblSize WHERE SizeCode = @SizeCode";
    const result = await pool.request()
      .input('SizeCode', sql.VarChar, id)
      .query(deleteQuery);

    if (result.rowsAffected[0] === 0) {
      return res.status(404).json({ success: false, message: "Size not found." });
    }

    res.json({ success: true, message: "Size deleted successfully." });
  } catch (err) {
    console.error("Delete Size Error:", err);
    res.status(500).json({ success: false, message: "Failed to delete size", error: err.message });
  }
});

module.exports = router;
