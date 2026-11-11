const express = require("express");
const router = express.Router();
const { sql } = require("../db");

// Helper to ensure table exists and columns are present
async function ensureTableExists(pool) {
  const checkTableQuery = `
    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='tblCategory' AND xtype='U')
    BEGIN
        CREATE TABLE tblCategory (
            CategoryCode VARCHAR(50) PRIMARY KEY,
            CategoryName VARCHAR(100),
            ClassCode VARCHAR(50),
            InventoryPrefix VARCHAR(50) DEFAULT '',
            FromInventoryCode VARCHAR(50) DEFAULT '',
            ToInventoryCode VARCHAR(50) DEFAULT '',
            NextInventoryCode VARCHAR(50) DEFAULT '',
            Remarks VARCHAR(255) DEFAULT '',
            SizeFactor BIT DEFAULT 0,
            ShowInReceipt BIT DEFAULT 0,
            CreateUser VARCHAR(50) DEFAULT 'sys',
            CreateDate DATETIME DEFAULT GETDATE(),
            ModifyUser VARCHAR(50) DEFAULT 'sys',
            ModifyDate DATETIME DEFAULT GETDATE()
        )
    END
    ELSE
    BEGIN
        IF COL_LENGTH('tblCategory', 'InventoryPrefix') IS NULL ALTER TABLE tblCategory ADD InventoryPrefix VARCHAR(50) DEFAULT '';
        IF COL_LENGTH('tblCategory', 'FromInventoryCode') IS NULL ALTER TABLE tblCategory ADD FromInventoryCode VARCHAR(50) DEFAULT '';
        IF COL_LENGTH('tblCategory', 'ToInventoryCode') IS NULL ALTER TABLE tblCategory ADD ToInventoryCode VARCHAR(50) DEFAULT '';
        IF COL_LENGTH('tblCategory', 'NextInventoryCode') IS NULL ALTER TABLE tblCategory ADD NextInventoryCode VARCHAR(50) DEFAULT '';
        IF COL_LENGTH('tblCategory', 'Remarks') IS NULL ALTER TABLE tblCategory ADD Remarks VARCHAR(255) DEFAULT '';
        IF COL_LENGTH('tblCategory', 'SizeFactor') IS NULL ALTER TABLE tblCategory ADD SizeFactor BIT DEFAULT 0;
        IF COL_LENGTH('tblCategory', 'ShowInReceipt') IS NULL ALTER TABLE tblCategory ADD ShowInReceipt BIT DEFAULT 0;
        IF COL_LENGTH('tblCategory', 'CreateUser') IS NULL ALTER TABLE tblCategory ADD CreateUser VARCHAR(50) DEFAULT 'sys';
        IF COL_LENGTH('tblCategory', 'CreateDate') IS NULL ALTER TABLE tblCategory ADD CreateDate DATETIME DEFAULT GETDATE();
        IF COL_LENGTH('tblCategory', 'ModifyUser') IS NULL ALTER TABLE tblCategory ADD ModifyUser VARCHAR(50) DEFAULT 'sys';
        IF COL_LENGTH('tblCategory', 'ModifyDate') IS NULL ALTER TABLE tblCategory ADD ModifyDate DATETIME DEFAULT GETDATE();
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

// ==========================================
// 1. LOV / MASTER DATA & FIELD-LEVEL VALIDATION ENDPOINTS
// ==========================================

// GET /api/category/classes - Class LOV matching frmHelpSearch("tblClass")
router.get("/classes", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query(`
      SELECT 
        RTRIM(ClassCode) AS ClassCode, 
        RTRIM(ClassName) AS ClassName, 
        ISNULL(Rate, 0) AS Rate, 
        ISNULL(RTRIM(Remarks), '') AS Remarks
      FROM tblClass WITH (NOLOCK)
      ORDER BY ClassCode
    `);
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Classes LOV Error:", err);
    res.status(500).json({ success: false, message: "Error fetching classes", error: err.message });
  }
});

// GET /api/category/validate-class/:classCode - Field validation matching objClsClass.GetClassName
router.get("/validate-class/:classCode", async (req, res) => {
  try {
    const pool = req.pool;
    const { classCode } = req.params;
    const result = await pool.request()
      .input('ClassCode', sql.VarChar, (classCode || '').trim())
      .query(`
        SELECT RTRIM(ClassCode) AS ClassCode, RTRIM(ClassName) AS ClassName 
        FROM tblClass WITH (NOLOCK) 
        WHERE ClassCode = @ClassCode
      `);

    if (result.recordset.length === 0) {
      return res.status(404).json({ success: false, message: "Pleae enter valid ClassCode." });
    }
    res.json({ success: true, data: result.recordset[0] });
  } catch (err) {
    res.status(500).json({ success: false, message: "Error validating class", error: err.message });
  }
});

// GET /api/category/check-range - Validation matching objClsCategory.GetInventoryRange
router.get("/check-range", async (req, res) => {
  try {
    const pool = req.pool;
    await ensureTableExists(pool);
    const { categoryCode, fromCode, toCode, prefix } = req.query;

    const fromVal = (fromCode || '').trim();
    const toVal = (toCode || '').trim();
    const prefixVal = (prefix || '').trim();
    const catCode = (categoryCode || '').trim();

    const fromNum = parseInt(fromVal, 10);
    const toNum = parseInt(toVal, 10);

    let query = `
      SELECT CategoryCode, CategoryName, FromInventoryCode, ToInventoryCode, InventoryPrefix
      FROM tblCategory WITH (NOLOCK)
      WHERE CategoryCode <> @CategoryCode
        AND ISNULL(RTRIM(InventoryPrefix), '') = @InventoryPrefix
    `;

    const request = pool.request()
      .input('CategoryCode', sql.VarChar, catCode)
      .input('InventoryPrefix', sql.VarChar, prefixVal);

    const result = await request.query(query);

    let overlaps = false;
    let conflictCategory = null;

    if (!isNaN(fromNum) && !isNaN(toNum)) {
      for (const row of result.recordset) {
        const existFrom = parseInt(row.FromInventoryCode, 10);
        const existTo = parseInt(row.ToInventoryCode, 10);
        if (!isNaN(existFrom) && !isNaN(existTo)) {
          if (!(toNum < existFrom || fromNum > existTo)) {
            overlaps = true;
            conflictCategory = row;
            break;
          }
        }
      }
    } else {
      for (const row of result.recordset) {
        if ((fromVal >= row.FromInventoryCode && fromVal <= row.ToInventoryCode) ||
            (toVal >= row.FromInventoryCode && toVal <= row.ToInventoryCode)) {
          overlaps = true;
          conflictCategory = row;
          break;
        }
      }
    }

    if (overlaps) {
      return res.json({
        success: false,
        overlapping: true,
        message: "This Inventory code range already exist.",
        conflict: conflictCategory
      });
    }

    res.json({ success: true, overlapping: false });
  } catch (err) {
    res.status(500).json({ success: false, message: "Error checking range", error: err.message });
  }
});

// ==========================================
// 2. CRUD OPERATIONS WITH FIELD-LEVEL VALIDATIONS
// ==========================================

// GET all categories
router.get("/", async (req, res) => {
  try {
    const pool = req.pool;
    await ensureTableExists(pool);

    const result = await pool.request().query(`
      SELECT 
        RTRIM(cat.CategoryCode) AS CategoryCode,
        RTRIM(cat.CategoryName) AS CategoryName,
        ISNULL(RTRIM(cat.ClassCode), '') AS ClassCode,
        ISNULL(RTRIM(c.ClassName), '') AS ClassName,
        ISNULL(RTRIM(cat.FromInventoryCode), '') AS FromInventoryCode,
        ISNULL(RTRIM(cat.ToInventoryCode), '') AS ToInventoryCode,
        ISNULL(RTRIM(cat.NextInventoryCode), '') AS NextInventoryCode,
        ISNULL(RTRIM(cat.InventoryPrefix), '') AS InventoryPrefix,
        ISNULL(RTRIM(cat.Remarks), '') AS Remarks,
        CASE WHEN cat.SizeFactor = 1 OR cat.SizeFactor = '1' THEN 1 ELSE 0 END AS SizeFactor,
        CASE WHEN cat.ShowInReceipt = 1 OR cat.ShowInReceipt = '1' THEN 1 ELSE 0 END AS ShowInReceipt,
        cat.CreateDate,
        cat.ModifyDate
      FROM tblCategory cat WITH (NOLOCK)
      LEFT JOIN tblClass c WITH (NOLOCK) ON RTRIM(cat.ClassCode) = RTRIM(c.ClassCode)
      ORDER BY cat.CategoryCode
    `);
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
      .input('CategoryCode', sql.VarChar, (id || '').trim())
      .query(`
        SELECT 
          RTRIM(cat.CategoryCode) AS CategoryCode,
          RTRIM(cat.CategoryName) AS CategoryName,
          ISNULL(RTRIM(cat.ClassCode), '') AS ClassCode,
          ISNULL(RTRIM(c.ClassName), '') AS ClassName,
          ISNULL(RTRIM(cat.FromInventoryCode), '') AS FromInventoryCode,
          ISNULL(RTRIM(cat.ToInventoryCode), '') AS ToInventoryCode,
          ISNULL(RTRIM(cat.NextInventoryCode), '') AS NextInventoryCode,
          ISNULL(RTRIM(cat.InventoryPrefix), '') AS InventoryPrefix,
          ISNULL(RTRIM(cat.Remarks), '') AS Remarks,
          CASE WHEN cat.SizeFactor = 1 OR cat.SizeFactor = '1' THEN 1 ELSE 0 END AS SizeFactor,
          CASE WHEN cat.ShowInReceipt = 1 OR cat.ShowInReceipt = '1' THEN 1 ELSE 0 END AS ShowInReceipt,
          cat.CreateDate,
          cat.ModifyDate
        FROM tblCategory cat WITH (NOLOCK)
        LEFT JOIN tblClass c WITH (NOLOCK) ON RTRIM(cat.ClassCode) = RTRIM(c.ClassCode)
        WHERE cat.CategoryCode = @CategoryCode
      `);

    if (result.recordset.length === 0) {
      return res.status(404).json({ success: false, message: "Category not found" });
    }
    res.json({ success: true, data: result.recordset[0] });
  } catch (err) {
    console.error("Fetch Category by Code Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// POST insert new category with full field-level validations matching ValidateMe
router.post("/", async (req, res) => {
  try {
    const pool = req.pool;
    await ensureTableExists(pool);
    const { 
      CategoryCode, 
      CategoryName, 
      ClassCode, 
      InventoryPrefix, 
      FromInventoryCode, 
      ToInventoryCode, 
      NextInventoryCode, 
      Remarks, 
      SizeFactor, 
      ShowInReceipt,
      userId 
    } = req.body;

    // 1. txtCategoryCode validation (line 918)
    if (!CategoryCode || !String(CategoryCode).trim()) {
      return res.status(400).json({ success: false, message: "CategoryCode should not be blank." });
    }

    // 2. txtCategoryName validation (line 923)
    if (!CategoryName || !String(CategoryName).trim()) {
      return res.status(400).json({ success: false, message: "CategoryName should not be blank." });
    }

    // 3. txtClassCode validation (line 928)
    if (!ClassCode || !String(ClassCode).trim()) {
      return res.status(400).json({ success: false, message: "ClassCode should not be blank." });
    }

    // 4. Valid ClassCode check against tblClass (line 934)
    const classCheck = await pool.request()
      .input('ClassCode', sql.VarChar, String(ClassCode).trim())
      .query("SELECT ClassCode, ClassName FROM tblClass WHERE ClassCode = @ClassCode");
    
    if (classCheck.recordset.length === 0) {
      return res.status(400).json({ success: false, message: "Pleae enter valid ClassCode." });
    }

    // 5. txtFromInventoryCode validation (line 940)
    if (!FromInventoryCode || !String(FromInventoryCode).trim()) {
      return res.status(400).json({ success: false, message: "From Inventory code should not be blank." });
    }

    // 6. txtToInventoryCode validation (line 949)
    if (!ToInventoryCode || !String(ToInventoryCode).trim()) {
      return res.status(400).json({ success: false, message: "To Inventory code should not be blank." });
    }

    const fromNum = parseInt(String(FromInventoryCode).trim(), 10);
    const toNum = parseInt(String(ToInventoryCode).trim(), 10);

    // 7. ToInventoryCode >= FromInventoryCode check (line 958)
    if (!isNaN(fromNum) && !isNaN(toNum)) {
      if (toNum < fromNum) {
        return res.status(400).json({ success: false, message: "ToInventoryCode should be greater than FromInventoryCode." });
      }
    }

    // 8. NextInventoryCode range check (lines 968-980)
    if (NextInventoryCode && String(NextInventoryCode).trim() !== '') {
      const nextNum = parseInt(String(NextInventoryCode).trim(), 10);
      if (!isNaN(nextNum) && !isNaN(fromNum) && !isNaN(toNum)) {
        if (nextNum < fromNum || nextNum > toNum) {
          return res.status(400).json({
            success: false,
            message: "Next inventorycode should be greaterthan From InventoryCode and Less than To InventoryCode."
          });
        }
      }
    }

    const prefixVal = (InventoryPrefix || '').trim();
    const catCodeVal = String(CategoryCode).trim().toUpperCase();

    // 9. Duplicate CategoryCode check
    const checkResult = await pool.request()
      .input('CategoryCode', sql.VarChar, catCodeVal)
      .query("SELECT CategoryCode FROM tblCategory WHERE CategoryCode = @CategoryCode");
    
    if (checkResult.recordset.length > 0) {
      return res.status(400).json({ success: false, message: `Category Code '${catCodeVal}' already exists.` });
    }

    // 10. Inventory Range Overlap Check matching objClsCategory.GetInventoryRange (line 963)
    if (!isNaN(fromNum) && !isNaN(toNum)) {
      const rangeCheck = await pool.request()
        .input('InventoryPrefix', sql.VarChar, prefixVal)
        .query(`
          SELECT CategoryCode, CategoryName, FromInventoryCode, ToInventoryCode
          FROM tblCategory WITH (NOLOCK)
          WHERE ISNULL(RTRIM(InventoryPrefix), '') = @InventoryPrefix
        `);

      for (const row of rangeCheck.recordset) {
        const existFrom = parseInt(row.FromInventoryCode, 10);
        const existTo = parseInt(row.ToInventoryCode, 10);
        if (!isNaN(existFrom) && !isNaN(existTo)) {
          if (!(toNum < existFrom || fromNum > existTo)) {
            return res.status(400).json({ 
              success: false, 
              message: "This Inventory code range already exist." 
            });
          }
        }
      }
    }

    const nextInv = (NextInventoryCode && String(NextInventoryCode).trim()) 
      ? String(NextInventoryCode).trim() 
      : String(FromInventoryCode).trim();

    const currentUser = userId || 'sys';

    const insertQuery = `
      INSERT INTO tblCategory (
        CategoryCode, CategoryName, ClassCode, InventoryPrefix, FromInventoryCode, 
        ToInventoryCode, NextInventoryCode, Remarks, SizeFactor, ShowInReceipt,
        CreateUser, CreateDate, ModifyUser, ModifyDate
      ) VALUES (
        @CategoryCode, @CategoryName, @ClassCode, @InventoryPrefix, @FromInventoryCode,
        @ToInventoryCode, @NextInventoryCode, @Remarks, @SizeFactor, @ShowInReceipt,
        @User, GETDATE(), @User, GETDATE()
      )
    `;

    await pool.request()
      .input('CategoryCode', sql.VarChar, catCodeVal.substring(0, 50))
      .input('CategoryName', sql.VarChar, String(CategoryName).trim().substring(0, 100))
      .input('ClassCode', sql.VarChar, String(ClassCode).trim().substring(0, 50))
      .input('InventoryPrefix', sql.VarChar, prefixVal.substring(0, 50))
      .input('FromInventoryCode', sql.VarChar, String(FromInventoryCode).trim().substring(0, 50))
      .input('ToInventoryCode', sql.VarChar, String(ToInventoryCode).trim().substring(0, 50))
      .input('NextInventoryCode', sql.VarChar, nextInv.substring(0, 50))
      .input('Remarks', sql.VarChar, Remarks ? String(Remarks).trim().substring(0, 255) : '')
      .input('SizeFactor', sql.Bit, (SizeFactor === 1 || SizeFactor === true || SizeFactor === '1') ? 1 : 0)
      .input('ShowInReceipt', sql.Bit, (ShowInReceipt === 1 || ShowInReceipt === true || ShowInReceipt === '1') ? 1 : 0)
      .input('User', sql.VarChar, currentUser)
      .query(insertQuery);

    res.json({ success: true, message: "Category detail saved sucssfully." });
  } catch (err) {
    console.error("Insert Category Error:", err);
    res.status(500).json({ success: false, message: "Error in Save", error: err.message });
  }
});

// PUT update category with full field-level validations
router.put("/", async (req, res) => {
  try {
    const pool = req.pool;
    await ensureTableExists(pool);
    const { 
      CategoryCode, 
      CategoryName, 
      ClassCode, 
      InventoryPrefix, 
      FromInventoryCode, 
      ToInventoryCode, 
      NextInventoryCode, 
      Remarks, 
      SizeFactor, 
      ShowInReceipt,
      userId 
    } = req.body;
    
    // 1. txtCategoryCode validation (line 918)
    if (!CategoryCode || !String(CategoryCode).trim()) {
      return res.status(400).json({ success: false, message: "CategoryCode should not be blank." });
    }

    // 2. txtCategoryName validation (line 923)
    if (!CategoryName || !String(CategoryName).trim()) {
      return res.status(400).json({ success: false, message: "CategoryName should not be blank." });
    }

    // 3. txtClassCode validation (line 928)
    if (!ClassCode || !String(ClassCode).trim()) {
      return res.status(400).json({ success: false, message: "ClassCode should not be blank." });
    }

    // 4. Valid ClassCode check against tblClass (line 934)
    const classCheck = await pool.request()
      .input('ClassCode', sql.VarChar, String(ClassCode).trim())
      .query("SELECT ClassCode, ClassName FROM tblClass WHERE ClassCode = @ClassCode");
    
    if (classCheck.recordset.length === 0) {
      return res.status(400).json({ success: false, message: "Pleae enter valid ClassCode." });
    }

    // 5. txtFromInventoryCode validation (line 940)
    if (!FromInventoryCode || !String(FromInventoryCode).trim()) {
      return res.status(400).json({ success: false, message: "From Inventory code should not be blank." });
    }

    // 6. txtToInventoryCode validation (line 949)
    if (!ToInventoryCode || !String(ToInventoryCode).trim()) {
      return res.status(400).json({ success: false, message: "To Inventory code should not be blank." });
    }

    const fromNum = parseInt(String(FromInventoryCode).trim(), 10);
    const toNum = parseInt(String(ToInventoryCode).trim(), 10);

    // 7. ToInventoryCode >= FromInventoryCode check (line 958)
    if (!isNaN(fromNum) && !isNaN(toNum)) {
      if (toNum < fromNum) {
        return res.status(400).json({ success: false, message: "ToInventoryCode should be greater than FromInventoryCode." });
      }
    }

    // 8. NextInventoryCode range check (lines 968-980)
    if (NextInventoryCode && String(NextInventoryCode).trim() !== '') {
      const nextNum = parseInt(String(NextInventoryCode).trim(), 10);
      if (!isNaN(nextNum) && !isNaN(fromNum) && !isNaN(toNum)) {
        if (nextNum < fromNum || nextNum > toNum) {
          return res.status(400).json({
            success: false,
            message: "Next inventorycode should be greaterthan From InventoryCode and Less than To InventoryCode."
          });
        }
      }
    }

    const prefixVal = (InventoryPrefix || '').trim();
    const catCodeVal = String(CategoryCode).trim();

    // 9. Inventory Range Overlap Check against OTHER categories (line 963)
    if (!isNaN(fromNum) && !isNaN(toNum)) {
      const rangeCheck = await pool.request()
        .input('CategoryCode', sql.VarChar, catCodeVal)
        .input('InventoryPrefix', sql.VarChar, prefixVal)
        .query(`
          SELECT CategoryCode, CategoryName, FromInventoryCode, ToInventoryCode
          FROM tblCategory WITH (NOLOCK)
          WHERE CategoryCode <> @CategoryCode
            AND ISNULL(RTRIM(InventoryPrefix), '') = @InventoryPrefix
        `);

      for (const row of rangeCheck.recordset) {
        const existFrom = parseInt(row.FromInventoryCode, 10);
        const existTo = parseInt(row.ToInventoryCode, 10);
        if (!isNaN(existFrom) && !isNaN(existTo)) {
          if (!(toNum < existFrom || fromNum > existTo)) {
            return res.status(400).json({ 
              success: false, 
              message: "This Inventory code range already exist." 
            });
          }
        }
      }
    }

    const nextInv = (NextInventoryCode && String(NextInventoryCode).trim()) 
      ? String(NextInventoryCode).trim() 
      : String(FromInventoryCode).trim();

    const currentUser = userId || 'sys';

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
        ModifyUser = @User,
        ModifyDate = GETDATE()
      WHERE CategoryCode = @CategoryCode
    `;

    const result = await pool.request()
      .input('CategoryCode', sql.VarChar, catCodeVal)
      .input('CategoryName', sql.VarChar, String(CategoryName).trim().substring(0, 100))
      .input('ClassCode', sql.VarChar, String(ClassCode).trim().substring(0, 50))
      .input('InventoryPrefix', sql.VarChar, prefixVal.substring(0, 50))
      .input('FromInventoryCode', sql.VarChar, String(FromInventoryCode).trim().substring(0, 50))
      .input('ToInventoryCode', sql.VarChar, String(ToInventoryCode).trim().substring(0, 50))
      .input('NextInventoryCode', sql.VarChar, nextInv.substring(0, 50))
      .input('Remarks', sql.VarChar, Remarks ? String(Remarks).trim().substring(0, 255) : '')
      .input('SizeFactor', sql.Bit, (SizeFactor === 1 || SizeFactor === true || SizeFactor === '1') ? 1 : 0)
      .input('ShowInReceipt', sql.Bit, (ShowInReceipt === 1 || ShowInReceipt === true || ShowInReceipt === '1') ? 1 : 0)
      .input('User', sql.VarChar, currentUser)
      .query(updateQuery);

    if (result.rowsAffected[0] === 0) {
      return res.status(404).json({ success: false, message: "Category not found." });
    }

    res.json({ success: true, message: "Category detail saved sucssfully." });
  } catch (err) {
    console.error("Update Category Error:", err);
    res.status(500).json({ success: false, message: "Error in Save", error: err.message });
  }
});

// DELETE category with inventory check matching fncGetCategoryTran (line 653-684)
router.delete("/", async (req, res) => {
  try {
    const pool = req.pool;
    await ensureTableExists(pool);
    const code = (req.query.id || req.body.id || '').trim();

    if (!code) {
      return res.status(400).json({ success: false, message: "CategoryCode is required for deletion." });
    }

    // fncGetCategoryTran: DvCustomer = DM.RowReturnProjExecNonQryWithDataRow("Select * from tblInventory where CategoryCode=" & fncFormatSQLText(sCode))
    const invCheck = await pool.request()
      .input('CategoryCode', sql.VarChar, code)
      .query("SELECT TOP 1 InventoryCode FROM tblInventory WITH (NOLOCK) WHERE CategoryCode = @CategoryCode");

    if (invCheck.recordset && invCheck.recordset.length > 0) {
      return res.status(400).json({ 
        success: false, 
        message: "Can Not Delete Category , Beacuse This Category Is Having Some Inventory" 
      });
    }

    // Also check Catalogue references
    try {
      const catCheck = await pool.request()
        .input('CategoryCode', sql.VarChar, code)
        .query("SELECT TOP 1 CatalogueCode FROM tblCatalogue WITH (NOLOCK) WHERE CategoryCode = @CategoryCode");
      
      if (catCheck.recordset && catCheck.recordset.length > 0) {
        return res.status(400).json({ 
          success: false, 
          message: "Can Not Delete Category , Beacuse This Category Is Having Some Catalogue Items" 
        });
      }
    } catch (_) {}

    const deleteQuery = "DELETE FROM tblCategory WHERE CategoryCode = @CategoryCode";
    const result = await pool.request()
      .input('CategoryCode', sql.VarChar, code)
      .query(deleteQuery);

    if (result.rowsAffected[0] === 0) {
      return res.status(404).json({ success: false, message: "Category not found." });
    }

    res.json({ success: true, message: "Category detail deleted." });
  } catch (err) {
    console.error("Delete Category Error:", err);
    res.status(500).json({ success: false, message: "Error in deleting Category detail.", error: err.message });
  }
});

module.exports = router;
