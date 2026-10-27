const express = require("express");
const router = express.Router();
const { sql } = require("../db");

// Helper to ensure table exists
async function ensureTableExists(pool) {
  const checkTableQuery = `
    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='tblSalesMan' AND xtype='U')
    BEGIN
        CREATE TABLE tblSalesMan (
            SalesManCode VARCHAR(50) PRIMARY KEY,
            SalesManName VARCHAR(100),
            Password VARCHAR(100),
            Remarks VARCHAR(255),
            LastSalesAmount DECIMAL(18, 2),
            InvoiceNo VARCHAR(50),
            ShowInReport INT,
            SortOrder INT,
            CreateUser VARCHAR(50) DEFAULT 'sys',
            CreateDate DATETIME DEFAULT GETDATE(),
            ModifyUser VARCHAR(50) DEFAULT 'sys',
            ModifyDate DATETIME DEFAULT GETDATE(),
            AllowSalesReturn INT,
            SalesManGroup VARCHAR(50)
        )
    END
  `;
  try {
    await pool.request().query(checkTableQuery);
  } catch (err) {
    console.error("Error ensuring tblSalesMan exists:", err);
  }
}

// GET all salesmen
router.get("/", async (req, res) => {
  try {
    const pool = req.pool;
    await ensureTableExists(pool);

    const result = await pool.request().query("SELECT * FROM tblSalesMan ORDER BY SortOrder ASC, SalesManCode ASC");
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Salesman Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// GET distinct salesman groups
router.get("/groups", async (req, res) => {
  try {
    const pool = req.pool;
    await ensureTableExists(pool);

    const result = await pool.request().query("SELECT DISTINCT SalesManGroup FROM tblSalesMan WHERE SalesManGroup IS NOT NULL AND SalesManGroup != '' ORDER BY SalesManGroup");
    res.json({ success: true, data: result.recordset.map(r => r.SalesManGroup) });
  } catch (err) {
    console.error("Fetch Salesman Groups Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// POST insert new salesman
router.post("/", async (req, res) => {
  try {
    const pool = req.pool;
    await ensureTableExists(pool);
    const { 
        SalesManCode, SalesManName, Password, SalesManGroup, 
        Remarks, SortOrder, ShowInReport, AllowSalesReturn 
    } = req.body;

    if (!SalesManCode || !SalesManName) {
      return res.status(400).json({ success: false, message: "SalesMan Code and Name are required." });
    }

    // Check if exists
    const checkResult = await pool.request()
      .input('SalesManCode', sql.VarChar, SalesManCode)
      .query("SELECT SalesManCode FROM tblSalesMan WHERE SalesManCode = @SalesManCode");
    
    if (checkResult.recordset.length > 0) {
      return res.status(400).json({ success: false, message: "SalesManCode already exists." });
    }

    const insertQuery = `
      INSERT INTO tblSalesMan (
        SalesManCode, SalesManName, Password, SalesManGroup, Remarks, SortOrder, ShowInReport, AllowSalesReturn,
        CreateUser, CreateDate, ModifyUser, ModifyDate
      ) VALUES (
        @SalesManCode, @SalesManName, @Password, @SalesManGroup, @Remarks, @SortOrder, @ShowInReport, @AllowSalesReturn,
        'ups', GETDATE(), 'ups', GETDATE()
      )
    `;

    await pool.request()
      .input('SalesManCode', sql.VarChar, SalesManCode.substring(0, 50))
      .input('SalesManName', sql.VarChar, SalesManName.substring(0, 100))
      .input('Password', sql.VarChar, Password ? Password.substring(0, 100) : '')
      .input('SalesManGroup', sql.VarChar, SalesManGroup ? SalesManGroup.substring(0, 50) : '')
      .input('Remarks', sql.VarChar, Remarks ? Remarks.substring(0, 255) : '')
      .input('SortOrder', sql.Int, parseInt(SortOrder) || 0)
      .input('ShowInReport', sql.Int, ShowInReport ? 1 : 0)
      .input('AllowSalesReturn', sql.Int, AllowSalesReturn ? 1 : 0)
      .query(insertQuery);

    res.json({ success: true, message: "Salesman created successfully." });
  } catch (err) {
    console.error("Insert Salesman Error:", err);
    res.status(500).json({ success: false, message: "Failed to create salesman", error: err.message });
  }
});

// PUT update salesman
router.put("/", async (req, res) => {
  try {
    const pool = req.pool;
    const { 
        SalesManCode, SalesManName, Password, SalesManGroup, 
        Remarks, SortOrder, ShowInReport, AllowSalesReturn 
    } = req.body;
    
    if (!SalesManCode) return res.status(400).json({ success: false, message: "SalesMan Code is required" });

    const updateQuery = `
      UPDATE tblSalesMan SET 
        SalesManName = @SalesManName,
        Password = @Password,
        SalesManGroup = @SalesManGroup,
        Remarks = @Remarks,
        SortOrder = @SortOrder,
        ShowInReport = @ShowInReport,
        AllowSalesReturn = @AllowSalesReturn,
        ModifyUser = 'sys',
        ModifyDate = GETDATE()
      WHERE SalesManCode = @SalesManCode
    `;

    const result = await pool.request()
      .input('SalesManCode', sql.VarChar, SalesManCode)
      .input('SalesManName', sql.VarChar, SalesManName ? SalesManName.substring(0, 100) : '')
      .input('Password', sql.VarChar, Password ? Password.substring(0, 100) : '')
      .input('SalesManGroup', sql.VarChar, SalesManGroup ? SalesManGroup.substring(0, 50) : '')
      .input('Remarks', sql.VarChar, Remarks || '')
      .input('SortOrder', sql.Int, parseInt(SortOrder) || 0)
      .input('ShowInReport', sql.Int, ShowInReport ? 1 : 0)
      .input('AllowSalesReturn', sql.Int, AllowSalesReturn ? 1 : 0)
      .query(updateQuery);

    if (result.rowsAffected[0] === 0) {
      return res.status(404).json({ success: false, message: "Salesman not found." });
    }

    res.json({ success: true, message: "Salesman updated successfully." });
  } catch (err) {
    console.error("Update Salesman Error:", err);
    res.status(500).json({ success: false, message: "Failed to update salesman", error: err.message });
  }
});

// DELETE salesman
router.delete("/", async (req, res) => {
  try {
    const pool = req.pool;
    const { id } = req.query;
    console.log(`[DELETE] SalesMan Code requested: '${id}'`);

    const deleteQuery = "DELETE FROM tblSalesMan WHERE SalesManCode = @SalesManCode";
    const result = await pool.request()
      .input('SalesManCode', sql.VarChar, id)
      .query(deleteQuery);

    console.log(`[DELETE] rowsAffected:`, result.rowsAffected);
    if (result.rowsAffected[0] === 0) {
      return res.status(404).json({ success: false, message: "Salesman not found." });
    }

    res.json({ success: true, message: "Salesman deleted successfully." });
  } catch (err) {
    console.error("Delete Salesman Error:", err);
    res.status(500).json({ success: false, message: "Failed to delete salesman", error: err.message });
  }
});

module.exports = router;
