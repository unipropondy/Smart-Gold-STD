const express = require("express");
const router = express.Router();
const { sql } = require("../db");

// Helper to ensure table exists
async function ensureTableExists(pool) {
  const checkTableQuery = `
    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='tblClass' AND xtype='U')
    BEGIN
        CREATE TABLE tblClass (
            ClassCode VARCHAR(50) PRIMARY KEY,
            ClassName VARCHAR(100),
            SalesType VARCHAR(10) DEFAULT 'W',
            PurchaseType VARCHAR(10) DEFAULT 'W',
            GoldLoss VARCHAR(10) DEFAULT 'N',
            DisplayCost VARCHAR(10) DEFAULT 'Y',
            GoldBarCode VARCHAR(10) DEFAULT 'N',
            Rate DECIMAL(18,2) DEFAULT 0,
            MCDiscount DECIMAL(18,2) DEFAULT 0,
            Remarks VARCHAR(255) DEFAULT '',
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
    console.error("Error ensuring tblClass exists:", err);
  }
}

// GET all classes
router.get("/", async (req, res) => {
  try {
    const pool = req.pool;
    await ensureTableExists(pool);

    const result = await pool.request().query("SELECT * FROM tblClass ORDER BY ClassCode");
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Classes Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// POST insert new class
router.post("/", async (req, res) => {
  try {
    const pool = req.pool;
    await ensureTableExists(pool);
    const { ClassCode, ClassName, SalesType, PurchaseType, GoldLoss, DisplayCost, GoldBarCode, Remarks, Rate, MCDiscount } = req.body;

    if (!ClassCode || !ClassName) {
      return res.status(400).json({ success: false, message: "ClassCode and ClassName are required." });
    }

    // Check if exists
    const checkResult = await pool.request()
      .input('ClassCode', sql.VarChar, ClassCode)
      .query("SELECT ClassCode FROM tblClass WHERE ClassCode = @ClassCode");
    
    if (checkResult.recordset.length > 0) {
      return res.status(400).json({ success: false, message: "ClassCode already exists." });
    }

    const insertQuery = `
      INSERT INTO tblClass (
        ClassCode, ClassName, SalesType, PurchaseType, GoldLoss, 
        DisplayCost, GoldBarCode, Remarks, Rate, MCDiscount,
        CreateUser, CreateDate, ModifyUser, ModifyDate
      ) 
      VALUES (
        @ClassCode, @ClassName, @SalesType, @PurchaseType, @GoldLoss, 
        @DisplayCost, @GoldBarCode, @Remarks, @Rate, @MCDiscount,
        'sys', GETDATE(), 'sys', GETDATE()
      )
    `;
    await pool.request()
      .input('ClassCode', sql.VarChar, ClassCode)
      .input('ClassName', sql.VarChar, ClassName)
      .input('SalesType', sql.VarChar, SalesType || 'W')
      .input('PurchaseType', sql.VarChar, PurchaseType || 'W')
      .input('GoldLoss', sql.VarChar, GoldLoss || 'N')
      .input('DisplayCost', sql.VarChar, DisplayCost || 'N')
      .input('GoldBarCode', sql.VarChar, GoldBarCode || 'N')
      .input('Remarks', sql.VarChar, Remarks || '')
      .input('Rate', sql.Decimal(18, 2), Rate || 0)
      .input('MCDiscount', sql.Decimal(18, 2), MCDiscount || 0)
      .query(insertQuery);

    res.status(201).json({ success: true, message: "Class created successfully" });
  } catch (err) {
    console.error("Insert Class Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// PUT update class
router.put("/:id", async (req, res) => {
  try {
    const pool = req.pool;
    const oldClassCode = req.params.id;
    const { ClassCode, ClassName, SalesType, PurchaseType, GoldLoss, DisplayCost, GoldBarCode, Remarks, Rate, MCDiscount } = req.body;

    if (!ClassCode || !ClassName) {
      return res.status(400).json({ success: false, message: "ClassCode and ClassName are required." });
    }

    const updateQuery = `
      UPDATE tblClass 
      SET 
        ClassCode = @ClassCode, 
        ClassName = @ClassName,
        SalesType = @SalesType,
        PurchaseType = @PurchaseType,
        GoldLoss = @GoldLoss,
        DisplayCost = @DisplayCost,
        GoldBarCode = @GoldBarCode,
        Remarks = @Remarks,
        Rate = @Rate,
        MCDiscount = @MCDiscount,
        ModifyUser = 'sys',
        ModifyDate = GETDATE()
      WHERE ClassCode = @OldClassCode
    `;
    const result = await pool.request()
      .input('ClassCode', sql.VarChar, ClassCode)
      .input('ClassName', sql.VarChar, ClassName)
      .input('SalesType', sql.VarChar, SalesType || 'W')
      .input('PurchaseType', sql.VarChar, PurchaseType || 'W')
      .input('GoldLoss', sql.VarChar, GoldLoss || 'N')
      .input('DisplayCost', sql.VarChar, DisplayCost || 'N')
      .input('GoldBarCode', sql.VarChar, GoldBarCode || 'N')
      .input('Remarks', sql.VarChar, Remarks || '')
      .input('Rate', sql.Decimal(18, 2), Rate || 0)
      .input('MCDiscount', sql.Decimal(18, 2), MCDiscount || 0)
      .input('OldClassCode', sql.VarChar, oldClassCode)
      .query(updateQuery);

    if (result.rowsAffected[0] === 0) {
      return res.status(404).json({ success: false, message: "Class not found." });
    }

    res.json({ success: true, message: "Class updated successfully" });
  } catch (err) {
    console.error("Update Class Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// DELETE class
router.delete("/:id", async (req, res) => {
  try {
    const pool = req.pool;
    const { id } = req.params;
    console.log(`[DELETE] Received request to delete ClassCode: '${id}'`);

    const result = await pool.request()
      .input('ClassCode', sql.VarChar, id)
      .query("DELETE FROM tblClass WHERE ClassCode = @ClassCode");
    
    console.log(`[DELETE] rowsAffected:`, result.rowsAffected);

    if (result.rowsAffected[0] === 0) {
      console.log(`[DELETE] Class not found for id: '${id}'`);
      return res.status(404).json({ success: false, message: "Class not found." });
    }

    res.json({ success: true, message: "Class deleted successfully" });
  } catch (err) {
    console.error("Delete Class Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

module.exports = router;
