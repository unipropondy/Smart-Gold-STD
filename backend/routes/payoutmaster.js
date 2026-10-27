const express = require("express");
const router = express.Router();
const { sql } = require("../db");

// Helper to ensure table exists
async function ensureTableExists(pool) {
  const checkTableQuery = `
    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='tblPayOutMaster' AND xtype='U')
    BEGIN
        CREATE TABLE tblPayOutMaster (
            Code VARCHAR(50) PRIMARY KEY,
            Description VARCHAR(255),
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
    console.error("Error ensuring tblPayOutMaster exists:", err);
  }
}

// GET all payout masters
router.get("/", async (req, res) => {
  try {
    const pool = req.pool;
    await ensureTableExists(pool);

    const result = await pool.request().query("SELECT * FROM tblPayOutMaster ORDER BY Code ASC");
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Payout Master Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// POST insert new payout master
router.post("/", async (req, res) => {
  try {
    const pool = req.pool;
    await ensureTableExists(pool);
    const { Code, Description, Remarks } = req.body;

    if (!Code) {
      return res.status(400).json({ success: false, message: "Code is required." });
    }

    // Check if exists
    const checkResult = await pool.request()
      .input('Code', sql.VarChar, Code)
      .query("SELECT Code FROM tblPayOutMaster WHERE Code = @Code");
    
    if (checkResult.recordset.length > 0) {
      return res.status(400).json({ success: false, message: "Code already exists." });
    }

    const insertQuery = `
      INSERT INTO tblPayOutMaster (
        Code, Description, Remarks,
        CreateUser, CreateDate, ModifyUser, ModifyDate
      ) VALUES (
        @Code, @Description, @Remarks,
        'sys', GETDATE(), 'sys', GETDATE()
      )
    `;

    await pool.request()
      .input('Code', sql.VarChar, Code.substring(0, 50))
      .input('Description', sql.VarChar, Description ? Description.substring(0, 255) : '')
      .input('Remarks', sql.VarChar, Remarks ? Remarks.substring(0, 255) : '')
      .query(insertQuery);

    res.json({ success: true, message: "Payout Master created successfully." });
  } catch (err) {
    console.error("Insert Payout Master Error:", err);
    res.status(500).json({ success: false, message: "Failed to create payout master", error: err.message });
  }
});

// PUT update payout master
router.put("/", async (req, res) => {
  try {
    const pool = req.pool;
    const { Code, Description, Remarks } = req.body;
    
    if (!Code) return res.status(400).json({ success: false, message: "Code is required" });

    const updateQuery = `
      UPDATE tblPayOutMaster SET 
        Description = @Description,
        Remarks = @Remarks,
        ModifyUser = 'sys',
        ModifyDate = GETDATE()
      WHERE Code = @Code
    `;

    const result = await pool.request()
      .input('Code', sql.VarChar, Code.substring(0, 50))
      .input('Description', sql.VarChar, Description ? Description.substring(0, 255) : '')
      .input('Remarks', sql.VarChar, Remarks ? Remarks.substring(0, 255) : '')
      .query(updateQuery);

    if (result.rowsAffected[0] === 0) {
      return res.status(404).json({ success: false, message: "Payout Master not found." });
    }

    res.json({ success: true, message: "Payout Master updated successfully." });
  } catch (err) {
    console.error("Update Payout Master Error:", err);
    res.status(500).json({ success: false, message: "Failed to update payout master", error: err.message });
  }
});

// DELETE payout master
router.delete("/", async (req, res) => {
  try {
    const pool = req.pool;
    const { id } = req.query;

    const deleteQuery = "DELETE FROM tblPayOutMaster WHERE Code = @Code";
    const result = await pool.request()
      .input('Code', sql.VarChar, id)
      .query(deleteQuery);

    if (result.rowsAffected[0] === 0) {
      return res.status(404).json({ success: false, message: "Payout Master not found." });
    }

    res.json({ success: true, message: "Payout Master deleted successfully." });
  } catch (err) {
    console.error("Delete Payout Master Error:", err);
    res.status(500).json({ success: false, message: "Failed to delete payout master", error: err.message });
  }
});

module.exports = router;
