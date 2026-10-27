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

    if (!PayMode) {
      return res.status(400).json({ success: false, message: "PayMode is required." });
    }

    // Check if exists
    const checkResult = await pool.request()
      .input('PayMode', sql.VarChar, PayMode)
      .query("SELECT PayMode FROM tblPaymode WHERE PayMode = @PayMode");
    
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
      .input('PayMode', sql.VarChar, PayMode.substring(0, 50))
      .input('Position', sql.Int, parseInt(Position) || 0)
      .input('Description', sql.VarChar, Description ? Description.substring(0, 100) : '')
      .input('Commission', sql.Decimal(18, 2), parseFloat(Commission) || 0)
      .input('Active', sql.Int, Active ? 1 : 0)
      .query(insertQuery);

    res.json({ success: true, message: "Paymode created successfully." });
  } catch (err) {
    console.error("Insert Paymode Error:", err);
    res.status(500).json({ success: false, message: "Failed to create paymode", error: err.message });
  }
});

// PUT update paymode
router.put("/", async (req, res) => {
  try {
    const pool = req.pool;
    const { PayMode, Position, Description, Commission, Active } = req.body;
    
    if (!PayMode) return res.status(400).json({ success: false, message: "PayMode is required" });

    const updateQuery = `
      UPDATE tblPaymode SET 
        Position = @Position,
        Description = @Description,
        Commission = @Commission,
        Active = @Active,
        ModifyUser = 'sys',
        ModifyDate = GETDATE()
      WHERE PayMode = @PayMode
    `;

    const result = await pool.request()
      .input('PayMode', sql.VarChar, PayMode)
      .input('Position', sql.Int, parseInt(Position) || 0)
      .input('Description', sql.VarChar, Description ? Description.substring(0, 100) : '')
      .input('Commission', sql.Decimal(18, 2), parseFloat(Commission) || 0)
      .input('Active', sql.Int, Active ? 1 : 0)
      .query(updateQuery);

    if (result.rowsAffected[0] === 0) {
      return res.status(404).json({ success: false, message: "Paymode not found." });
    }

    res.json({ success: true, message: "Paymode updated successfully." });
  } catch (err) {
    console.error("Update Paymode Error:", err);
    res.status(500).json({ success: false, message: "Failed to update paymode", error: err.message });
  }
});

// DELETE paymode
router.delete("/", async (req, res) => {
  try {
    const pool = req.pool;
    const { id } = req.query; // Use query param for DELETE
    console.log(`[DELETE] PayMode requested: '${id}'`);

    const deleteQuery = "DELETE FROM tblPaymode WHERE PayMode = @PayMode";
    const result = await pool.request()
      .input('PayMode', sql.VarChar, id)
      .query(deleteQuery);

    console.log(`[DELETE] rowsAffected:`, result.rowsAffected);
    if (result.rowsAffected[0] === 0) {
      return res.status(404).json({ success: false, message: "Paymode not found." });
    }

    res.json({ success: true, message: "Paymode deleted successfully." });
  } catch (err) {
    console.error("Delete Paymode Error:", err);
    res.status(500).json({ success: false, message: "Failed to delete paymode", error: err.message });
  }
});

module.exports = router;
