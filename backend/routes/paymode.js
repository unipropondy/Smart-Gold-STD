const express = require("express");
const router = express.Router();
const { sql } = require("../db");

// Helper to ensure table exists
async function ensureTableExists(pool) {
  const checkTableQuery = `
    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='tblPaymode' AND xtype='U')
    BEGIN
        CREATE TABLE tblPaymode (
            PayMode VARCHAR(50) PRIMARY KEY,
            Position INT,
            Description VARCHAR(100),
            Commission DECIMAL(18, 2),
            GLAccountCode VARCHAR(50),
            Active INT,
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
    console.error("Error ensuring tblPaymode exists:", err);
  }
}

// GET all paymodes
router.get("/", async (req, res) => {
  try {
    const pool = req.pool;
    await ensureTableExists(pool);

    const result = await pool.request().query("SELECT * FROM tblPaymode ORDER BY Position ASC");
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Paymode Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// POST insert new paymode
router.post("/", async (req, res) => {
  try {
    const pool = req.pool;
    await ensureTableExists(pool);
    const { PayMode, Position, Description, Commission, Active } = req.body;

    const trimmedPosition = (Position !== undefined && Position !== null ? String(Position).trim() : '');
    const trimmedPayMode = (PayMode || '').trim();
    const trimmedDescription = (Description || '').trim();
    const trimmedCommission = (Commission !== undefined && Commission !== null ? String(Commission).trim() : '');

    // 1. Position validation
    if (!trimmedPosition) {
      return res.status(400).json({ success: false, message: "Position should not be blank! " });
    }
    if (isNaN(Number(trimmedPosition))) {
      return res.status(400).json({ success: false, message: "Position should be Numeric!" });
    }

    // 2. PayMode validation
    if (!trimmedPayMode) {
      return res.status(400).json({ success: false, message: "PayMode should not be blank!" });
    }

    // 3. Description validation
    if (!trimmedDescription) {
      return res.status(400).json({ success: false, message: "Description should not be blank!" });
    }

    // 4. Commission validation
    if (!trimmedCommission) {
      return res.status(400).json({ success: false, message: "Commision code should not be blank!" });
    }
    if (isNaN(Number(trimmedCommission))) {
      return res.status(400).json({ success: false, message: "Commision should be Numeric!" });
    }

    // Check if PayMode exists
    const checkResult = await pool.request()
      .input('PayMode', sql.VarChar, trimmedPayMode)
      .query("SELECT PayMode FROM tblPaymode WHERE UPPER(RTRIM(LTRIM(PayMode))) = UPPER(@PayMode)");
    
    if (checkResult.recordset.length > 0) {
      return res.status(400).json({ success: false, message: "PayMode already exists." });
    }

    const insertQuery = `
      INSERT INTO tblPaymode (
        PayMode, Position, Description, Commission, Active,
        CreateUser, CreateDate, ModifyUser, ModifyDate
      ) VALUES (
        @PayMode, @Position, @Description, @Commission, @Active,
        'ups', GETDATE(), 'ups', GETDATE()
      )
    `;

    await pool.request()
      .input('PayMode', sql.VarChar, trimmedPayMode.substring(0, 50))
      .input('Position', sql.Int, parseInt(trimmedPosition) || 0)
      .input('Description', sql.VarChar, trimmedDescription.substring(0, 100))
      .input('Commission', sql.Decimal(18, 2), parseFloat(trimmedCommission) || 0)
      .input('Active', sql.Int, Active ? 1 : 0)
      .query(insertQuery);

    res.json({ success: true, message: "Pay Mode saved successfully!" });
  } catch (err) {
    console.error("Insert Paymode Error:", err);
    res.status(500).json({ success: false, message: "Error in Save !", error: err.message });
  }
});

// PUT update paymode
router.put("/", async (req, res) => {
  try {
    const pool = req.pool;
    const { PayMode, Position, Description, Commission, Active } = req.body;
    
    const trimmedPosition = (Position !== undefined && Position !== null ? String(Position).trim() : '');
    const trimmedPayMode = (PayMode || '').trim();
    const trimmedDescription = (Description || '').trim();
    const trimmedCommission = (Commission !== undefined && Commission !== null ? String(Commission).trim() : '');

    // 1. Position validation
    if (!trimmedPosition) {
      return res.status(400).json({ success: false, message: "Position should not be blank! " });
    }
    if (isNaN(Number(trimmedPosition))) {
      return res.status(400).json({ success: false, message: "Position should be Numeric!" });
    }

    // 2. PayMode validation
    if (!trimmedPayMode) {
      return res.status(400).json({ success: false, message: "PayMode should not be blank!" });
    }

    // 3. Description validation
    if (!trimmedDescription) {
      return res.status(400).json({ success: false, message: "Description should not be blank!" });
    }

    // 4. Commission validation
    if (!trimmedCommission) {
      return res.status(400).json({ success: false, message: "Commision code should not be blank!" });
    }
    if (isNaN(Number(trimmedCommission))) {
      return res.status(400).json({ success: false, message: "Commision should be Numeric!" });
    }

    const updateQuery = `
      UPDATE tblPaymode SET 
        Position = @Position,
        Description = @Description,
        Commission = @Commission,
        Active = @Active,
        ModifyUser = 'sys',
        ModifyDate = GETDATE()
      WHERE UPPER(RTRIM(LTRIM(PayMode))) = UPPER(@PayMode)
    `;

    const result = await pool.request()
      .input('PayMode', sql.VarChar, trimmedPayMode)
      .input('Position', sql.Int, parseInt(trimmedPosition) || 0)
      .input('Description', sql.VarChar, trimmedDescription.substring(0, 100))
      .input('Commission', sql.Decimal(18, 2), parseFloat(trimmedCommission) || 0)
      .input('Active', sql.Int, Active ? 1 : 0)
      .query(updateQuery);

    if (result.rowsAffected[0] === 0) {
      return res.status(404).json({ success: false, message: "Paymode not found." });
    }

    res.json({ success: true, message: "Pay Mode saved successfully!" });
  } catch (err) {
    console.error("Update Paymode Error:", err);
    res.status(500).json({ success: false, message: "Error in Save !", error: err.message });
  }
});

// DELETE paymode
router.delete("/", async (req, res) => {
  try {
    const pool = req.pool;
    const { id } = req.query; // Use query param for DELETE
    if (!id || !id.trim()) {
      return res.status(400).json({ success: false, message: "PayMode is required." });
    }

    const trimmedCode = id.trim();
    console.log(`[DELETE] PayMode requested: '${trimmedCode}'`);

    const deleteQuery = "DELETE FROM tblPaymode WHERE UPPER(RTRIM(LTRIM(PayMode))) = UPPER(@PayMode)";
    const result = await pool.request()
      .input('PayMode', sql.VarChar, trimmedCode)
      .query(deleteQuery);

    console.log(`[DELETE] rowsAffected:`, result.rowsAffected);
    if (result.rowsAffected[0] === 0) {
      return res.status(404).json({ success: false, message: "Error in Delete !" });
    }

    res.json({ success: true, message: "Paymode deleted successfully." });
  } catch (err) {
    console.error("Delete Paymode Error:", err);
    res.status(500).json({ success: false, message: "Error in Delete !", error: err.message });
  }
});

module.exports = router;
