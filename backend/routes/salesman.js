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

    const trimmedCode = (SalesManCode || '').trim();
    const trimmedName = (SalesManName || '').trim();
    const trimmedGroup = (SalesManGroup || '').trim();
    const nSortOrder = parseInt(SortOrder) || 0;

    if (!trimmedCode) {
      return res.status(400).json({ success: false, message: "SaleManCode should not be blank." });
    }

    if (!trimmedName) {
      return res.status(400).json({ success: false, message: "SalesManName should not be blank." });
    }

    if (!trimmedGroup) {
      return res.status(400).json({ success: false, message: "SalesManGroup should not be blank." });
    }

    if (nSortOrder === 0) {
      return res.status(400).json({ success: false, message: "Sort Order Can Not Be Zero." });
    }

    // Check if SalesManCode already exists
    const checkResult = await pool.request()
      .input('SalesManCode', sql.VarChar, trimmedCode)
      .query("SELECT SalesManCode FROM tblSalesMan WHERE UPPER(RTRIM(LTRIM(SalesManCode))) = UPPER(@SalesManCode)");
    
    if (checkResult.recordset.length > 0) {
      return res.status(400).json({ success: false, message: "SalesManCode already exists." });
    }

    // Check if SortOrder already exists (matching subSortOrderExists in frmSalesman.vb)
    const checkSort = await pool.request()
      .input('SalesManCode', sql.VarChar, trimmedCode)
      .input('SortOrder', sql.Int, nSortOrder)
      .query("SELECT SalesManCode FROM tblSalesMan WHERE UPPER(RTRIM(LTRIM(SalesManCode))) != UPPER(@SalesManCode) AND SortOrder = @SortOrder");

    if (checkSort.recordset.length > 0) {
      return res.status(400).json({ success: false, message: "Sort Order Already Exists." });
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
      .input('SalesManCode', sql.VarChar, trimmedCode.substring(0, 50))
      .input('SalesManName', sql.VarChar, trimmedName.substring(0, 100))
      .input('Password', sql.VarChar, Password ? Password.trim().substring(0, 100) : '')
      .input('SalesManGroup', sql.VarChar, trimmedGroup.substring(0, 50))
      .input('Remarks', sql.VarChar, Remarks ? Remarks.trim().substring(0, 255) : '')
      .input('SortOrder', sql.Int, nSortOrder)
      .input('ShowInReport', sql.Int, ShowInReport ? 1 : 0)
      .input('AllowSalesReturn', sql.Int, AllowSalesReturn ? 1 : 0)
      .query(insertQuery);

    res.json({ success: true, message: "SalesMan detail saved sucssfully." });
  } catch (err) {
    console.error("Insert Salesman Error:", err);
    res.status(500).json({ success: false, message: "Error in Save", error: err.message });
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
    
    const trimmedCode = (SalesManCode || '').trim();
    const trimmedName = (SalesManName || '').trim();
    const trimmedGroup = (SalesManGroup || '').trim();
    const nSortOrder = parseInt(SortOrder) || 0;

    if (!trimmedCode) {
      return res.status(400).json({ success: false, message: "SaleManCode should not be blank." });
    }

    if (!trimmedName) {
      return res.status(400).json({ success: false, message: "SalesManName should not be blank." });
    }

    if (!trimmedGroup) {
      return res.status(400).json({ success: false, message: "SalesManGroup should not be blank." });
    }

    if (nSortOrder === 0) {
      return res.status(400).json({ success: false, message: "Sort Order Can Not Be Zero." });
    }

    // Check if SortOrder already exists on another salesman
    const checkSort = await pool.request()
      .input('SalesManCode', sql.VarChar, trimmedCode)
      .input('SortOrder', sql.Int, nSortOrder)
      .query("SELECT SalesManCode FROM tblSalesMan WHERE UPPER(RTRIM(LTRIM(SalesManCode))) != UPPER(@SalesManCode) AND SortOrder = @SortOrder");

    if (checkSort.recordset.length > 0) {
      return res.status(400).json({ success: false, message: "Sort Order Already Exists." });
    }

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
      WHERE UPPER(RTRIM(LTRIM(SalesManCode))) = UPPER(@SalesManCode)
    `;

    const result = await pool.request()
      .input('SalesManCode', sql.VarChar, trimmedCode)
      .input('SalesManName', sql.VarChar, trimmedName ? trimmedName.substring(0, 100) : '')
      .input('Password', sql.VarChar, Password ? Password.trim().substring(0, 100) : '')
      .input('SalesManGroup', sql.VarChar, trimmedGroup ? trimmedGroup.substring(0, 50) : '')
      .input('Remarks', sql.VarChar, Remarks ? Remarks.trim().substring(0, 255) : '')
      .input('SortOrder', sql.Int, nSortOrder)
      .input('ShowInReport', sql.Int, ShowInReport ? 1 : 0)
      .input('AllowSalesReturn', sql.Int, AllowSalesReturn ? 1 : 0)
      .query(updateQuery);

    if (result.rowsAffected[0] === 0) {
      return res.status(404).json({ success: false, message: "Salesman not found." });
    }

    res.json({ success: true, message: "SalesMan detail saved sucssfully." });
  } catch (err) {
    console.error("Update Salesman Error:", err);
    res.status(500).json({ success: false, message: "Error in Save", error: err.message });
  }
});

// DELETE salesman
router.delete("/", async (req, res) => {
  try {
    const pool = req.pool;
    const { id } = req.query;

    if (!id || !id.trim()) {
      return res.status(400).json({ success: false, message: "SalesMan Code is required." });
    }

    const trimmedCode = id.trim();
    console.log(`[DELETE] SalesMan Code requested: '${trimmedCode}'`);

    const deleteQuery = "DELETE FROM tblSalesMan WHERE UPPER(RTRIM(LTRIM(SalesManCode))) = UPPER(@SalesManCode)";
    const result = await pool.request()
      .input('SalesManCode', sql.VarChar, trimmedCode)
      .query(deleteQuery);

    console.log(`[DELETE] rowsAffected:`, result.rowsAffected);
    if (result.rowsAffected[0] === 0) {
      return res.status(404).json({ success: false, message: "Error in deleting Salesman detail." });
    }

    res.json({ success: true, message: "SalesMan detail deleted." });
  } catch (err) {
    console.error("Delete Salesman Error:", err);
    res.status(500).json({ success: false, message: "Error in deleting Salesman detail.", error: err.message });
  }
});

module.exports = router;
