const express = require("express");
const router = express.Router();
const { sql } = require("../db");

// Helper to ensure table exists
async function ensureTableExists(pool) {
  const checkTableQuery = `
    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='tblRefiner' AND xtype='U')
    BEGIN
        CREATE TABLE tblRefiner (
            RefinerCode VARCHAR(50) PRIMARY KEY,
            RefinerName VARCHAR(100),
            Type VARCHAR(50),
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
    console.error("Error ensuring tblRefiner exists:", err);
  }
}

// GET all refiners
router.get("/", async (req, res) => {
  try {
    const pool = req.pool;
    await ensureTableExists(pool);

    const result = await pool.request().query("SELECT * FROM tblRefiner ORDER BY RefinerCode ASC");
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Refiner Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// POST insert new refiner
router.post("/", async (req, res) => {
  try {
    const pool = req.pool;
    await ensureTableExists(pool);
    const { RefinerCode, RefinerName, Type } = req.body;

    if (!RefinerCode) {
      return res.status(400).json({ success: false, message: "Refiner Code is required." });
    }

    // Check if exists
    const checkResult = await pool.request()
      .input('RefinerCode', sql.VarChar, RefinerCode)
      .query("SELECT RefinerCode FROM tblRefiner WHERE RefinerCode = @RefinerCode");
    
    if (checkResult.recordset.length > 0) {
      return res.status(400).json({ success: false, message: "RefinerCode already exists." });
    }

    const insertQuery = `
      INSERT INTO tblRefiner (
        RefinerCode, RefinerName, Type,
        CreateUser, CreateDate, ModifyUser, ModifyDate
      ) VALUES (
        @RefinerCode, @RefinerName, @Type,
        'ups', GETDATE(), 'ups', GETDATE()
      )
    `;

    await pool.request()
      .input('RefinerCode', sql.VarChar, RefinerCode.substring(0, 50))
      .input('RefinerName', sql.VarChar, RefinerName ? RefinerName.substring(0, 100) : '')
      .input('Type', sql.VarChar, Type ? Type.substring(0, 50) : '')
      .query(insertQuery);

    res.json({ success: true, message: "Refiner created successfully." });
  } catch (err) {
    console.error("Insert Refiner Error:", err);
    res.status(500).json({ success: false, message: "Failed to create refiner", error: err.message });
  }
});

// PUT update refiner
router.put("/", async (req, res) => {
  try {
    const pool = req.pool;
    const { RefinerCode, RefinerName, Type } = req.body;
    
    if (!RefinerCode) return res.status(400).json({ success: false, message: "Refiner Code is required" });

    const updateQuery = `
      UPDATE tblRefiner SET 
        RefinerName = @RefinerName,
        Type = @Type,
        ModifyUser = 'sys',
        ModifyDate = GETDATE()
      WHERE RefinerCode = @RefinerCode
    `;

    const result = await pool.request()
      .input('RefinerCode', sql.VarChar, RefinerCode)
      .input('RefinerName', sql.VarChar, RefinerName ? RefinerName.substring(0, 100) : '')
      .input('Type', sql.VarChar, Type ? Type.substring(0, 50) : '')
      .query(updateQuery);

    if (result.rowsAffected[0] === 0) {
      return res.status(404).json({ success: false, message: "Refiner not found." });
    }

    res.json({ success: true, message: "Refiner updated successfully." });
  } catch (err) {
    console.error("Update Refiner Error:", err);
    res.status(500).json({ success: false, message: "Failed to update refiner", error: err.message });
  }
});

// DELETE refiner
router.delete("/", async (req, res) => {
  try {
    const pool = req.pool;
    const { id } = req.query; // Use query param for DELETE
    console.log(`[DELETE] Refiner Code requested: '${id}'`);

    const deleteQuery = "DELETE FROM tblRefiner WHERE RefinerCode = @RefinerCode";
    const result = await pool.request()
      .input('RefinerCode', sql.VarChar, id)
      .query(deleteQuery);

    console.log(`[DELETE] rowsAffected:`, result.rowsAffected);
    if (result.rowsAffected[0] === 0) {
      return res.status(404).json({ success: false, message: "Refiner not found." });
    }

    res.json({ success: true, message: "Refiner deleted successfully." });
  } catch (err) {
    console.error("Delete Refiner Error:", err);
    res.status(500).json({ success: false, message: "Failed to delete refiner", error: err.message });
  }
});

module.exports = router;
