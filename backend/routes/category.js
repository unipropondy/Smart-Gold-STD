const express = require("express");
const router = express.Router();
const { sql } = require("../db");

// Helper to ensure table exists
async function ensureTableExists(pool) {
  const checkTableQuery = `
    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='tblCategory' AND xtype='U')
    BEGIN
        CREATE TABLE tblCategory (
            CategoryCode VARCHAR(50) PRIMARY KEY,
            CategoryName VARCHAR(100),
            ClassCode VARCHAR(50),
            InventoryPrefix VARCHAR(50),
            FromInventoryCode VARCHAR(50),
            ToInventoryCode VARCHAR(50),
            NextInventoryCode VARCHAR(50),
            Remarks VARCHAR(255),
            SizeFactor BIT DEFAULT 0,
            ShowInReceipt BIT DEFAULT 0,
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
    console.error("Error ensuring tblCategory exists:", err);
  }
}

// GET schema
router.get("/schema", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query("SELECT COLUMN_NAME, CHARACTER_MAXIMUM_LENGTH, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tblCategory'");
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    res.status(500).json({ success: false, error: err.message });
  }
});

// GET all categories
router.get("/", async (req, res) => {
  try {
    const pool = req.pool;
    await ensureTableExists(pool);

    const result = await pool.request().query("SELECT * FROM tblCategory ORDER BY CategoryCode");
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Categories Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// GET category by code
router.get("/:id", async (req, res) => {
  try {
    const pool = req.pool;
    await ensureTableExists(pool);
    const { id } = req.params;

    const result = await pool.request()
      .input('CategoryCode', sql.VarChar, id)
      .query("SELECT * FROM tblCategory WHERE CategoryCode = @CategoryCode");

    if (result.recordset.length === 0) {
      return res.status(404).json({ success: false, message: "Category not found" });
    }
    res.json({ success: true, data: result.recordset[0] });
  } catch (err) {
    console.error("Fetch Category by Code Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// POST insert new category
router.post("/", async (req, res) => {
  try {
    const pool = req.pool;
    await ensureTableExists(pool);
    const { CategoryCode, CategoryName, ClassCode, InventoryPrefix, FromInventoryCode, ToInventoryCode, NextInventoryCode, Remarks, SizeFactor, ShowInReceipt } = req.body;

    if (!CategoryCode || !CategoryName) {
      return res.status(400).json({ success: false, message: "CategoryCode and CategoryName are required." });
    }

    // Check if exists
    const checkResult = await pool.request()
      .input('CategoryCode', sql.VarChar, CategoryCode)
      .query("SELECT CategoryCode FROM tblCategory WHERE CategoryCode = @CategoryCode");
    
    if (checkResult.recordset.length > 0) {
      return res.status(400).json({ success: false, message: "CategoryCode already exists." });
    }

    const insertQuery = `
      INSERT INTO tblCategory (
        CategoryCode, CategoryName, ClassCode, InventoryPrefix, FromInventoryCode, 
        ToInventoryCode, NextInventoryCode, Remarks, SizeFactor, ShowInReceipt,
        CreateUser, CreateDate, ModifyUser, ModifyDate
      ) VALUES (
        @CategoryCode, @CategoryName, @ClassCode, @InventoryPrefix, @FromInventoryCode,
        @ToInventoryCode, @NextInventoryCode, @Remarks, @SizeFactor, @ShowInReceipt,
        'ups', GETDATE(), 'ups', GETDATE()
      )
    `;

    await pool.request()
      .input('CategoryCode', sql.VarChar, CategoryCode.substring(0, 50))
      .input('CategoryName', sql.VarChar, CategoryName.substring(0, 100))
      .input('ClassCode', sql.VarChar, ClassCode ? ClassCode.substring(0, 50) : '')
      .input('InventoryPrefix', sql.VarChar, InventoryPrefix ? InventoryPrefix.substring(0, 50) : '')
      .input('FromInventoryCode', sql.VarChar, FromInventoryCode ? FromInventoryCode.substring(0, 50) : '')
      .input('ToInventoryCode', sql.VarChar, ToInventoryCode ? ToInventoryCode.substring(0, 50) : '')
      .input('NextInventoryCode', sql.VarChar, NextInventoryCode ? NextInventoryCode.substring(0, 50) : '')
      .input('Remarks', sql.VarChar, Remarks ? Remarks.substring(0, 255) : '')
      .input('SizeFactor', sql.Bit, SizeFactor ? 1 : 0)
      .input('ShowInReceipt', sql.Bit, ShowInReceipt ? 1 : 0)
      .query(insertQuery);

    res.json({ success: true, message: "Category created successfully." });
  } catch (err) {
    console.error("Insert Category Error:", err);
    res.status(500).json({ success: false, message: "Failed to create category", error: err.message });
  }
});

// PUT update category
router.put("/", async (req, res) => {
  try {
    const pool = req.req ? req.pool : req.pool; // req.pool is set by middleware
    const { CategoryCode, CategoryName, ClassCode, InventoryPrefix, FromInventoryCode, ToInventoryCode, NextInventoryCode, Remarks, SizeFactor, ShowInReceipt } = req.body;
    
    if (!CategoryCode) return res.status(400).json({ success: false, message: "CategoryCode is required" });

    const updateQuery = `
      UPDATE tblCategory SET 
        CategoryName = @CategoryName,
        ClassCode = @ClassCode,
        InventoryPrefix = @InventoryPrefix,
        FromInventoryCode = @FromInventoryCode,
        ToInventoryCode = @ToInventoryCode,
        NextInventoryCode = @NextInventoryCode,
        Remarks = @Remarks,
        SizeFactor = @SizeFactor,
        ShowInReceipt = @ShowInReceipt,
        ModifyUser = 'sys',
        ModifyDate = GETDATE()
      WHERE CategoryCode = @CategoryCode
    `;

    const result = await pool.request()
      .input('CategoryCode', sql.VarChar, CategoryCode)
      .input('CategoryName', sql.VarChar, CategoryName ? CategoryName.substring(0, 100) : '')
      .input('ClassCode', sql.VarChar, ClassCode || '')
      .input('InventoryPrefix', sql.VarChar, InventoryPrefix || '')
      .input('FromInventoryCode', sql.VarChar, FromInventoryCode || '')
      .input('ToInventoryCode', sql.VarChar, ToInventoryCode || '')
      .input('NextInventoryCode', sql.VarChar, NextInventoryCode || '')
      .input('Remarks', sql.VarChar, Remarks || '')
      .input('SizeFactor', sql.Bit, SizeFactor ? 1 : 0)
      .input('ShowInReceipt', sql.Bit, ShowInReceipt ? 1 : 0)
      .query(updateQuery);

    if (result.rowsAffected[0] === 0) {
      return res.status(404).json({ success: false, message: "Category not found." });
    }

    res.json({ success: true, message: "Category updated successfully." });
  } catch (err) {
    console.error("Update Category Error:", err);
    res.status(500).json({ success: false, message: "Failed to update category", error: err.message });
  }
});

// DELETE category
router.delete("/", async (req, res) => {
  try {
    const pool = req.pool;
    const { id } = req.query; // Use query param for DELETE
    console.log(`[DELETE] CategoryCode requested: '${id}'`);

    const deleteQuery = "DELETE FROM tblCategory WHERE CategoryCode = @CategoryCode";
    const result = await pool.request()
      .input('CategoryCode', sql.VarChar, id)
      .query(deleteQuery);

    console.log(`[DELETE] rowsAffected:`, result.rowsAffected);
    if (result.rowsAffected[0] === 0) {
      return res.status(404).json({ success: false, message: "Category not found." });
    }

    res.json({ success: true, message: "Category deleted successfully." });
  } catch (err) {
    console.error("Delete Category Error:", err);
    res.status(500).json({ success: false, message: "Failed to delete category", error: err.message });
  }
});

module.exports = router;
