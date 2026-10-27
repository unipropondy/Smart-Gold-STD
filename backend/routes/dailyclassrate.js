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

    const query = `
    SELECT *
FROM tblClassRate
WHERE CAST(CreateDate AS DATE) = CAST(GETDATE() AS DATE)
ORDER BY ModifyDate DESC
    `;
    const result = await pool.request().query(query);
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Live Class Rates Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});
// GET today's live rates
router.get("/today", async (req, res) => {
  try {
    const pool = req.pool;
    await ensureTableExists(pool);

    const query = `
      SELECT *
      FROM tblClassRate
      WHERE CAST(CreateDate AS DATE) = CAST(GETDATE() AS DATE)
      ORDER BY ModifyDate DESC
    `;
    const result = await pool.request().query(query);
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Live Class Rates Error:", err);
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

    if (!ClassCode || !FromDate || Rate === undefined) {
      return res.status(400).json({ success: false, message: "ClassCode, FromDate, and Rate are required." });
    }

    // Check if exists
    const checkResult = await pool.request()
      .input('ClassCode', sql.VarChar, ClassCode)
      .input('FromDate', sql.DateTime, new Date(FromDate))
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
      .input('ClassCode', sql.VarChar, ClassCode.substring(0, 50))
      .input('ClassName', sql.VarChar, ClassName ? ClassName.substring(0, 100) : '')
      .input('FromDate', sql.DateTime, new Date(FromDate))
      .input('ToDate', sql.DateTime, ToDate ? new Date(ToDate) : null)
      .input('Rate', sql.Decimal(18, 2), Rate)
      .query(insertQuery);

    res.json({ success: true, message: "Class Rate created successfully." });
  } catch (err) {
    console.error("Insert Class Rate Error:", err);
    res.status(500).json({ success: false, message: "Failed to create class rate", error: err.message });
  }
});

// PUT update class rate (Update uses OriginalFromDate and ClassCode to identify the record)
router.put("/", async (req, res) => {
  try {
    const pool = req.pool;
    const { ClassCode, OriginalFromDate, ClassName, FromDate, ToDate, Rate } = req.body;

    if (!ClassCode || !OriginalFromDate) return res.status(400).json({ success: false, message: "ClassCode and OriginalFromDate are required" });

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
      .input('ClassCode', sql.VarChar, ClassCode.substring(0, 50))
      .input('OriginalFromDate', sql.DateTime, new Date(OriginalFromDate))
      .input('ClassName', sql.VarChar, ClassName ? ClassName.substring(0, 100) : '')
      .input('FromDate', sql.DateTime, new Date(FromDate))
      .input('ToDate', sql.DateTime, ToDate ? new Date(ToDate) : null)
      .input('Rate', sql.Decimal(18, 2), Rate)
      .query(updateQuery);

    if (result.rowsAffected[0] === 0) {
      return res.status(404).json({ success: false, message: "Class Rate not found." });
    }

    res.json({ success: true, message: "Class Rate updated successfully." });
  } catch (err) {
    console.error("Update Class Rate Error:", err);
    res.status(500).json({ success: false, message: "Failed to update class rate", error: err.message });
  }
});

// DELETE class rate
router.delete("/", async (req, res) => {
  try {
    const pool = req.pool;
    const { code, date } = req.query; // Use query params
    console.log(`[DELETE] Class Rate requested: Code='${code}', Date='${date}'`);

    const deleteQuery = "DELETE FROM tblClassRate WHERE ClassCode = @ClassCode AND CAST(FromDate AS DATE) = CAST(@FromDate AS DATE)";
    const result = await pool.request()
      .input('ClassCode', sql.VarChar, code)
      .input('FromDate', sql.DateTime, new Date(date))
      .query(deleteQuery);

    console.log(`[DELETE] rowsAffected:`, result.rowsAffected);
    if (result.rowsAffected[0] === 0) {
      return res.status(404).json({ success: false, message: "Class Rate not found." });
    }

    res.json({ success: true, message: "Class Rate deleted successfully." });
  } catch (err) {
    console.error("Delete Class Rate Error:", err);
    res.status(500).json({ success: false, message: "Failed to delete class rate", error: err.message });
  }
});

module.exports = router;
