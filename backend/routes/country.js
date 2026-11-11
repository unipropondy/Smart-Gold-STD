const express = require("express");
const router = express.Router();
const { sql } = require("../db");

// Helper to ensure table exists
async function ensureTableExists(pool) {
  const checkTableQuery = `
    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='tblCountry' AND xtype='U')
    BEGIN
        CREATE TABLE tblCountry (
            CountryCode VARCHAR(50) PRIMARY KEY,
            CountryName VARCHAR(100),
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
    console.error("Error ensuring tblCountry exists:", err);
  }
}

// GET all countries
router.get("/", async (req, res) => {
  try {
    const pool = req.pool;
    await ensureTableExists(pool);

    const result = await pool.request().query("SELECT * FROM tblCountry ORDER BY CountryCode ASC");
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Country Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// POST insert new country
router.post("/", async (req, res) => {
  try {
    const pool = req.pool;
    await ensureTableExists(pool);
    const { CountryCode, CountryName, Type } = req.body;

    const trimmedCode = (CountryCode || '').trim();
    const trimmedName = (CountryName || '').trim();
    const trimmedType = (Type || '').trim();

    if (!trimmedCode) {
      return res.status(400).json({ success: false, message: "Please enter Country Code !" });
    }

    if (!trimmedName) {
      return res.status(400).json({ success: false, message: "Please enter Country Name !" });
    }

    if (!trimmedType) {
      return res.status(400).json({ success: false, message: "Please enter Type !" });
    }

    // Check if exists
    const checkResult = await pool.request()
      .input('CountryCode', sql.VarChar, trimmedCode)
      .query("SELECT CountryCode FROM tblCountry WHERE CountryCode = @CountryCode");
    
    if (checkResult.recordset.length > 0) {
      return res.status(400).json({ success: false, message: "CountryCode already exists." });
    }

    const insertQuery = `
      INSERT INTO tblCountry (
        CountryCode, CountryName, Type,
        CreateUser, CreateDate, ModifyUser, ModifyDate
      ) VALUES (
        @CountryCode, @CountryName, @Type,
        'ups', GETDATE(), 'ups', GETDATE()
      )
    `;

    await pool.request()
      .input('CountryCode', sql.VarChar, trimmedCode.substring(0, 50))
      .input('CountryName', sql.VarChar, trimmedName ? trimmedName.substring(0, 100) : '')
      .input('Type', sql.VarChar, trimmedType ? trimmedType.substring(0, 50) : '')
      .query(insertQuery);

    res.json({ success: true, message: "Saved Successfully!" });
  } catch (err) {
    console.error("Insert Country Error:", err);
    res.status(500).json({ success: false, message: "Error in Save/Update !", error: err.message });
  }
});

// PUT update country
router.put("/", async (req, res) => {
  try {
    const pool = req.pool;
    const { CountryCode, CountryName, Type } = req.body;
    
    const trimmedCode = (CountryCode || '').trim();
    const trimmedName = (CountryName || '').trim();
    const trimmedType = (Type || '').trim();

    if (!trimmedCode) {
      return res.status(400).json({ success: false, message: "Please enter Country Code !" });
    }

    if (!trimmedName) {
      return res.status(400).json({ success: false, message: "Please enter Country Name !" });
    }

    if (!trimmedType) {
      return res.status(400).json({ success: false, message: "Please enter Type !" });
    }

    const updateQuery = `
      UPDATE tblCountry SET 
        CountryName = @CountryName,
        Type = @Type,
        ModifyUser = 'sys',
        ModifyDate = GETDATE()
      WHERE CountryCode = @CountryCode
    `;

    const result = await pool.request()
      .input('CountryCode', sql.VarChar, trimmedCode)
      .input('CountryName', sql.VarChar, trimmedName ? trimmedName.substring(0, 100) : '')
      .input('Type', sql.VarChar, trimmedType ? trimmedType.substring(0, 50) : '')
      .query(updateQuery);

    if (result.rowsAffected[0] === 0) {
      return res.status(404).json({ success: false, message: "No such user!" });
    }

    res.json({ success: true, message: "Saved Successfully!" });
  } catch (err) {
    console.error("Update Country Error:", err);
    res.status(500).json({ success: false, message: "Error in Save/Update !", error: err.message });
  }
});

// DELETE country
router.delete("/", async (req, res) => {
  try {
    const pool = req.pool;
    const { id } = req.query; // Use query param for DELETE
    console.log(`[DELETE] Country Code requested: '${id}'`);

    const deleteQuery = "DELETE FROM tblCountry WHERE CountryCode = @CountryCode";
    const result = await pool.request()
      .input('CountryCode', sql.VarChar, id)
      .query(deleteQuery);

    console.log(`[DELETE] rowsAffected:`, result.rowsAffected);
    if (result.rowsAffected[0] === 0) {
      return res.status(404).json({ success: false, message: "Country not found." });
    }

    res.json({ success: true, message: "Country deleted successfully." });
  } catch (err) {
    console.error("Delete Country Error:", err);
    res.status(500).json({ success: false, message: "Failed to delete country", error: err.message });
  }
});

module.exports = router;
