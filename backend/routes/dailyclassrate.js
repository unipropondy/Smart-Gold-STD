const express = require("express");
const router = express.Router();
const { sql } = require("../db");

// Helper to ensure table exists
async function ensureTableExists(pool) {
  const checkTableQuery = `
    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='tblClassRate' AND xtype='U')
    BEGIN
        CREATE TABLE tblClassRate (
            ClassCode VARCHAR(50),
            ClassName VARCHAR(100),
            FromDate DATETIME,
            ToDate DATETIME,
            Rate DECIMAL(18,2),
            CreateUser VARCHAR(50) DEFAULT 'sys',
            CreateDate DATETIME DEFAULT GETDATE(),
            ModifyUser VARCHAR(50) DEFAULT 'sys',
            ModifyDate DATETIME DEFAULT GETDATE(),
            PRIMARY KEY (ClassCode, FromDate)
        )
    END
  `;
  try {
    await pool.request().query(checkTableQuery);
  } catch (err) {
    console.error("Error ensuring tblClassRate exists:", err);
  }
}

// GET today's live rates
router.get("/today", async (req, res) => {
  try {
    const pool = req.pool;
    await ensureTableExists(pool);

    // Try finding today's rates; if none recorded today, return latest recorded rates
    const query = `
      SELECT *
FROM tblClassRate
WHERE CAST(FromDate AS DATE) <= CAST(GETDATE() AS DATE)
  AND CAST(ToDate AS DATE) >= CAST(GETDATE() AS DATE)
ORDER BY FromDate DESC, ModifyDate DESC;
    `;
    const result = await pool.request().query(query);
    res.json({ success: true, data: result.recordset || [] });
  } catch (err) {
    console.error("Fetch Live Class Rates Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message, data: [] });
  }
});

// GET /api/dailyclassrate/validate-class/:code
router.get("/validate-class/:code", async (req, res) => {
  try {
    const pool = req.pool;
    const { code } = req.params;
    if (!code || !code.trim()) {
      return res.status(400).json({ success: false, message: "ClassCode should not be blank." });
    }
    const result = await pool.request()
      .input("code", sql.VarChar(50), code.trim())
      .query("SELECT TOP 1 ClassCode, ClassName FROM tblClass WHERE RTRIM(LTRIM(ClassCode)) = RTRIM(LTRIM(@code))");

    if (result.recordset.length > 0) {
      res.json({ success: true, data: result.recordset[0] });
    } else {
      res.json({ success: false, message: "Please enter valid ClassCode." });
    }
  } catch (err) {
    console.error("Validate Class Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// GET all class rates
router.get("/", async (req, res) => {
  try {
    const pool = req.pool;
    await ensureTableExists(pool);

    const result = await pool.request().query("SELECT * FROM tblClassRate ORDER BY FromDate DESC, ClassCode ASC");
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Class Rates Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// POST insert new class rate
router.post("/", async (req, res) => {
  try {
    const pool = req.pool;
    await ensureTableExists(pool);
    const { ClassCode, ClassName, FromDate, ToDate, Rate } = req.body;

    if (!ClassCode || !ClassCode.trim()) {
      return res.status(400).json({ success: false, message: "ClassCode should not be blank." });
    }
    if (!FromDate) {
      return res.status(400).json({ success: false, message: "Please enter valid from date." });
    }
    if (!ToDate) {
      return res.status(400).json({ success: false, message: "Please enter valid to date." });
    }

    const fDate = new Date(FromDate);
    const tDate = new Date(ToDate);
    if (isNaN(fDate.getTime())) {
      return res.status(400).json({ success: false, message: "Please enter valid from date." });
    }
    if (isNaN(tDate.getTime())) {
      return res.status(400).json({ success: false, message: "Please enter valid to date." });
    }
    if (tDate.getTime() < fDate.getTime()) {
      return res.status(400).json({ success: false, message: "To date should be greater than From date." });
    }

    if (Rate === undefined || Rate === null || String(Rate).trim() === '') {
      return res.status(400).json({ success: false, message: "Rate Should not be empty." });
    }
    const numRate = parseFloat(Rate);
    if (isNaN(numRate)) {
      return res.status(400).json({ success: false, message: "Rate Should be Numeric." });
    }

    // Check if exists
    const checkResult = await pool.request()
      .input('ClassCode', sql.VarChar, ClassCode.trim())
      .input('FromDate', sql.DateTime, fDate)
      .query("SELECT ClassCode FROM tblClassRate WHERE ClassCode = @ClassCode AND CAST(FromDate AS DATE) = CAST(@FromDate AS DATE)");

    if (checkResult.recordset.length > 0) {
      return res.status(400).json({ success: false, message: "A rate for this Class on this From Date already exists." });
    }

    const insertQuery = `
      INSERT INTO tblClassRate (
        ClassCode, ClassName, FromDate, ToDate, Rate,
        CreateUser, CreateDate, ModifyUser, ModifyDate
      ) VALUES (
        @ClassCode, @ClassName, @FromDate, @ToDate, @Rate,
        'sys', GETDATE(), 'sys', GETDATE()
      )
    `;

    await pool.request()
      .input('ClassCode', sql.VarChar, ClassCode.trim().substring(0, 50))
      .input('ClassName', sql.VarChar, ClassName ? ClassName.substring(0, 100) : '')
      .input('FromDate', sql.DateTime, fDate)
      .input('ToDate', sql.DateTime, tDate)
      .input('Rate', sql.Decimal(18, 2), numRate)
      .query(insertQuery);

    res.json({ success: true, message: "Class Rate detail saved sucssfully." });
  } catch (err) {
    console.error("Insert Class Rate Error:", err);
    res.status(500).json({ success: false, message: "Error in Save", error: err.message });
  }
});

// PUT update class rate (Update uses OriginalFromDate and ClassCode to identify the record)
router.put("/", async (req, res) => {
  try {
    const pool = req.pool;
    const { ClassCode, OriginalFromDate, ClassName, FromDate, ToDate, Rate } = req.body;

    if (!ClassCode || !ClassCode.trim()) {
      return res.status(400).json({ success: false, message: "ClassCode should not be blank." });
    }
    if (!OriginalFromDate) {
      return res.status(400).json({ success: false, message: "OriginalFromDate is required." });
    }
    if (!FromDate) {
      return res.status(400).json({ success: false, message: "Please enter valid from date." });
    }
    if (!ToDate) {
      return res.status(400).json({ success: false, message: "Please enter valid to date." });
    }

    const fDate = new Date(FromDate);
    const tDate = new Date(ToDate);
    if (isNaN(fDate.getTime())) {
      return res.status(400).json({ success: false, message: "Please enter valid from date." });
    }
    if (isNaN(tDate.getTime())) {
      return res.status(400).json({ success: false, message: "Please enter valid to date." });
    }
    if (tDate.getTime() < fDate.getTime()) {
      return res.status(400).json({ success: false, message: "To date should be greater than From date." });
    }

    if (Rate === undefined || Rate === null || String(Rate).trim() === '') {
      return res.status(400).json({ success: false, message: "Rate Should not be empty." });
    }
    const numRate = parseFloat(Rate);
    if (isNaN(numRate)) {
      return res.status(400).json({ success: false, message: "Rate Should be Numeric." });
    }

    const updateQuery = `
      UPDATE tblClassRate SET 
        ClassName = @ClassName,
        FromDate = @FromDate,
        ToDate = @ToDate,
        Rate = @Rate,
        ModifyUser = 'sys',
        ModifyDate = GETDATE()
      WHERE ClassCode = @ClassCode AND CAST(FromDate AS DATE) = CAST(@OriginalFromDate AS DATE)
    `;

    const result = await pool.request()
      .input('ClassCode', sql.VarChar, ClassCode.trim().substring(0, 50))
      .input('OriginalFromDate', sql.DateTime, new Date(OriginalFromDate))
      .input('ClassName', sql.VarChar, ClassName ? ClassName.substring(0, 100) : '')
      .input('FromDate', sql.DateTime, fDate)
      .input('ToDate', sql.DateTime, tDate)
      .input('Rate', sql.Decimal(18, 2), numRate)
      .query(updateQuery);

    if (result.rowsAffected[0] === 0) {
      return res.status(404).json({ success: false, message: "Class Rate not found." });
    }

    res.json({ success: true, message: "Class Rate detail saved sucssfully." });
  } catch (err) {
    console.error("Update Class Rate Error:", err);
    res.status(500).json({ success: false, message: "Error in Save", error: err.message });
  }
});

// DELETE class rate
router.delete("/", async (req, res) => {
  try {
    const pool = req.pool;
    const { code, date } = req.query; // Use query params
    if (!code || !code.trim() || !date || !date.trim()) {
      return res.status(400).json({ success: false, message: "Please select the Class Rate to Delete?" });
    }

    const deleteQuery = "DELETE FROM tblClassRate WHERE ClassCode = @ClassCode AND CAST(FromDate AS DATE) = CAST(@FromDate AS DATE)";
    const result = await pool.request()
      .input('ClassCode', sql.VarChar, code.trim())
      .input('FromDate', sql.DateTime, new Date(date))
      .query(deleteQuery);

    if (result.rowsAffected[0] === 0) {
      return res.status(404).json({ success: false, message: "Class Rate not found." });
    }

    res.json({ success: true, message: "Class Rate Deleted." });
  } catch (err) {
    console.error("Delete Class Rate Error:", err);
    res.status(500).json({ success: false, message: "Error in Delete", error: err.message });
  }
});

module.exports = router;
