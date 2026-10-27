const express = require("express");
const router = express.Router();
const { sql } = require("../db");

// Helper to ensure table exists (fallback if not present)
async function ensureTableExists(pool) {
  const checkTableQuery = `
    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='tblCurrency' AND xtype='U')
    BEGIN
        CREATE TABLE tblCurrency (
            CurrencyCode VARCHAR(50) PRIMARY KEY,
            CurrencyName VARCHAR(100),
            CurrencyRate DECIMAL(18,8),
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
    console.error("Error ensuring tblCurrency exists:", err);
  }
}

// GET all Currency
router.get("/", async (req, res) => {
  try {
    await ensureTableExists(req.pool);
    const result = await req.pool.request().query(`
      SELECT 
        CurrencyCode,
        CurrencyName,
        CurrencyRate,
        Remarks
      FROM tblCurrency
      ORDER BY CurrencyCode
    `);
    res.json({ success: true, data: result.recordset });
  } catch (error) {
    console.error("Fetch Currency Error:", error);
    res.status(500).json({ success: false, message: "Database Error", error: error.message });
  }
});

// POST (Create) a Currency
router.post("/", async (req, res) => {
  try {
    await ensureTableExists(req.pool);
    const { CurrencyCode, CurrencyName, CurrencyRate, Remarks } = req.body;

    if (!CurrencyCode) {
      return res.status(400).json({ success: false, message: "Currency Code is required" });
    }

    const checkResult = await req.pool.request()
      .input("CurrencyCode", sql.VarChar, CurrencyCode)
      .query("SELECT CurrencyCode FROM tblCurrency WHERE CurrencyCode = @CurrencyCode");
    
    if (checkResult.recordset.length > 0) {
      return res.status(400).json({ success: false, message: "Currency Code already exists" });
    }

    const query = `
      INSERT INTO tblCurrency (
        CurrencyCode, 
        CurrencyName, 
        CurrencyRate, 
        Remarks,
        CreateUser,
        CreateDate,
        ModifyUser,
        ModifyDate
      )
      VALUES (
        @CurrencyCode, 
        @CurrencyName, 
        @CurrencyRate, 
        @Remarks,
        'sys',
        GETDATE(),
        'sys',
        GETDATE()
      )
    `;

    await req.pool.request()
      .input("CurrencyCode", sql.VarChar, CurrencyCode)
      .input("CurrencyName", sql.VarChar, CurrencyName || null)
      .input("CurrencyRate", sql.Decimal(18, 8), CurrencyRate ? parseFloat(CurrencyRate) : 0)
      .input("Remarks", sql.VarChar, Remarks || null)
      .query(query);

    res.json({ success: true, message: "Currency added successfully" });
  } catch (error) {
    console.error("Add Currency Error:", error);
    res.status(500).json({ success: false, message: "Database Error", error: error.message });
  }
});

// PUT (Update) a Currency
router.put("/", async (req, res) => {
  try {
    const { CurrencyCode, CurrencyName, CurrencyRate, Remarks } = req.body;

    if (!CurrencyCode) {
      return res.status(400).json({ success: false, message: "Currency Code is required" });
    }

    const query = `
      UPDATE tblCurrency
      SET 
        CurrencyName = @CurrencyName,
        CurrencyRate = @CurrencyRate,
        Remarks = @Remarks,
        ModifyUser = 'sys',
        ModifyDate = GETDATE()
      WHERE 
        CurrencyCode = @CurrencyCode
    `;

    const result = await req.pool.request()
      .input("CurrencyCode", sql.VarChar, CurrencyCode)
      .input("CurrencyName", sql.VarChar, CurrencyName || null)
      .input("CurrencyRate", sql.Decimal(18, 8), CurrencyRate ? parseFloat(CurrencyRate) : 0)
      .input("Remarks", sql.VarChar, Remarks || null)
      .query(query);

    if (result.rowsAffected[0] === 0) {
      return res.status(404).json({ success: false, message: "Currency not found" });
    }

    res.json({ success: true, message: "Currency updated successfully" });
  } catch (error) {
    console.error("Update Currency Error:", error);
    res.status(500).json({ success: false, message: "Database Error", error: error.message });
  }
});

// DELETE a Currency
router.delete("/", async (req, res) => {
  try {
    const { id } = req.query;

    if (!id) {
      return res.status(400).json({ success: false, message: "Currency Code is required" });
    }

    const query = `
      DELETE FROM tblCurrency 
      WHERE CurrencyCode = @CurrencyCode
    `;

    const result = await req.pool.request()
      .input("CurrencyCode", sql.VarChar, id)
      .query(query);

    if (result.rowsAffected[0] === 0) {
      return res.status(404).json({ success: false, message: "Currency not found" });
    }

    res.json({ success: true, message: "Currency deleted successfully" });
  } catch (error) {
    console.error("Delete Currency Error:", error);
    res.status(500).json({ success: false, message: "Database Error", error: error.message });
  }
});

module.exports = router;
