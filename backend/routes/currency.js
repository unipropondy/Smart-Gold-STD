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

// GET all Currency or single Currency by code
router.get("/", async (req, res) => {
  try {
    await ensureTableExists(req.pool);
    const { code } = req.query;

    if (code) {
      const result = await req.pool.request()
        .input("CurrencyCode", sql.VarChar, code.trim())
        .query(`
          SELECT 
            CurrencyCode,
            CurrencyName,
            CurrencyRate,
            Remarks
          FROM tblCurrency
          WHERE CurrencyCode = @CurrencyCode
        `);
      if (result.recordset.length > 0) {
        return res.json({ success: true, data: result.recordset[0] });
      } else {
        return res.status(404).json({ success: false, message: "Set Default Currency in Currency Master" });
      }
    }

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

// GET Currency by code
router.get("/by-code/:code", async (req, res) => {
  try {
    await ensureTableExists(req.pool);
    const { code } = req.params;
    const result = await req.pool.request()
      .input("CurrencyCode", sql.VarChar, (code || '').trim())
      .query(`
        SELECT 
          CurrencyCode,
          CurrencyName,
          CurrencyRate,
          Remarks
        FROM tblCurrency
        WHERE CurrencyCode = @CurrencyCode
      `);
    if (result.recordset.length > 0) {
      return res.json({ success: true, data: result.recordset[0] });
    } else {
      return res.status(404).json({ success: false, message: "Set Default Currency in Currency Master" });
    }
  } catch (error) {
    console.error("Fetch Currency By Code Error:", error);
    res.status(500).json({ success: false, message: "Database Error", error: error.message });
  }
});

// POST (Create) a Currency
router.post("/", async (req, res) => {
  try {
    await ensureTableExists(req.pool);
    const { CurrencyCode, CurrencyName, CurrencyRate, Remarks } = req.body;

    const trimmedCode = (CurrencyCode || '').trim();
    const trimmedName = (CurrencyName || '').trim();
    const trimmedRate = (CurrencyRate !== undefined && CurrencyRate !== null) ? String(CurrencyRate).trim() : '';

    if (!trimmedCode) {
      return res.status(400).json({ success: false, message: "Please enter Currency Code !" });
    }

    if (!trimmedName) {
      return res.status(400).json({ success: false, message: "Please enter Currency Name !" });
    }

    if (!trimmedRate) {
      return res.status(400).json({ success: false, message: "Please enter Rate !" });
    }

    if (isNaN(Number(trimmedRate))) {
      return res.status(400).json({ success: false, message: "Please enter numeric value to Rate !" });
    }

    const checkResult = await req.pool.request()
      .input("CurrencyCode", sql.VarChar, trimmedCode)
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
      .input("CurrencyCode", sql.VarChar, trimmedCode)
      .input("CurrencyName", sql.VarChar, trimmedName || null)
      .input("CurrencyRate", sql.Decimal(18, 8), parseFloat(trimmedRate))
      .input("Remarks", sql.VarChar, Remarks ? Remarks.trim() : null)
      .query(query);

    res.json({ success: true, message: "Saved Successfully!" });
  } catch (error) {
    console.error("Add Currency Error:", error);
    res.status(500).json({ success: false, message: "Error in Save/Update !", error: error.message });
  }
});

// PUT (Update) a Currency
router.put("/", async (req, res) => {
  try {
    const { CurrencyCode, CurrencyName, CurrencyRate, Remarks } = req.body;

    const trimmedCode = (CurrencyCode || '').trim();
    const trimmedName = (CurrencyName || '').trim();
    const trimmedRate = (CurrencyRate !== undefined && CurrencyRate !== null) ? String(CurrencyRate).trim() : '';

    if (!trimmedCode) {
      return res.status(400).json({ success: false, message: "Please enter Currency Code !" });
    }

    if (!trimmedName) {
      return res.status(400).json({ success: false, message: "Please enter Currency Name !" });
    }

    if (!trimmedRate) {
      return res.status(400).json({ success: false, message: "Please enter Rate !" });
    }

    if (isNaN(Number(trimmedRate))) {
      return res.status(400).json({ success: false, message: "Please enter numeric value to Rate !" });
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
      .input("CurrencyCode", sql.VarChar, trimmedCode)
      .input("CurrencyName", sql.VarChar, trimmedName || null)
      .input("CurrencyRate", sql.Decimal(18, 8), parseFloat(trimmedRate))
      .input("Remarks", sql.VarChar, Remarks ? Remarks.trim() : null)
      .query(query);

    if (result.rowsAffected[0] === 0) {
      return res.status(404).json({ success: false, message: "No such user!" });
    }

    res.json({ success: true, message: "Saved Successfully!" });
  } catch (error) {
    console.error("Update Currency Error:", error);
    res.status(500).json({ success: false, message: "Error in Save/Update !", error: error.message });
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
