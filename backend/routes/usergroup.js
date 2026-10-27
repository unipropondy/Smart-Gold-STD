const express = require("express");
const router = express.Router();
const { sql } = require("../db");

// Helper to ensure table exists
async function ensureTableExists(pool) {
  const checkTableQuery = `
    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='tblusergroup' AND xtype='U')
    BEGIN
        CREATE TABLE tblusergroup (
            UserGroupCode VARCHAR(50) PRIMARY KEY,
            UserGroupName VARCHAR(100),
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
    console.error("Error ensuring tblusergroup exists:", err);
  }
}

// GET all user groups
router.get("/", async (req, res) => {
  try {
    const pool = req.pool;
    await ensureTableExists(pool);

    const result = await pool.request().query("SELECT * FROM tblusergroup ORDER BY UserGroupCode ASC");
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch UserGroup Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// POST insert new user group
router.post("/", async (req, res) => {
  try {
    const pool = req.pool;
    await ensureTableExists(pool);
    const { UserGroupCode, UserGroupName } = req.body;

    if (!UserGroupCode) {
      return res.status(400).json({ success: false, message: "User Group Code is required." });
    }

    // Check if exists
    const checkResult = await pool.request()
      .input('UserGroupCode', sql.VarChar, UserGroupCode)
      .query("SELECT UserGroupCode FROM tblusergroup WHERE UserGroupCode = @UserGroupCode");
    
    if (checkResult.recordset.length > 0) {
      return res.status(400).json({ success: false, message: "UserGroupCode already exists." });
    }

    const insertQuery = `
      INSERT INTO tblusergroup (
        UserGroupCode, UserGroupName,
        CreateUser, CreateDate, ModifyUser, ModifyDate
      ) VALUES (
        @UserGroupCode, @UserGroupName,
        'ups', GETDATE(), 'ups', GETDATE()
      )
    `;

    await pool.request()
      .input('UserGroupCode', sql.VarChar, UserGroupCode.substring(0, 50))
      .input('UserGroupName', sql.VarChar, UserGroupName ? UserGroupName.substring(0, 100) : '')
      .query(insertQuery);

    res.json({ success: true, message: "User Group created successfully." });
  } catch (err) {
    console.error("Insert UserGroup Error:", err);
    res.status(500).json({ success: false, message: "Failed to create user group", error: err.message });
  }
});

// PUT update user group
router.put("/", async (req, res) => {
  try {
    const pool = req.pool;
    const { UserGroupCode, UserGroupName } = req.body;
    
    if (!UserGroupCode) return res.status(400).json({ success: false, message: "User Group Code is required" });

    const updateQuery = `
      UPDATE tblusergroup SET 
        UserGroupName = @UserGroupName,
        ModifyUser = 'sys',
        ModifyDate = GETDATE()
      WHERE UserGroupCode = @UserGroupCode
    `;

    const result = await pool.request()
      .input('UserGroupCode', sql.VarChar, UserGroupCode)
      .input('UserGroupName', sql.VarChar, UserGroupName ? UserGroupName.substring(0, 100) : '')
      .query(updateQuery);

    if (result.rowsAffected[0] === 0) {
      return res.status(404).json({ success: false, message: "User Group not found." });
    }

    res.json({ success: true, message: "User Group updated successfully." });
  } catch (err) {
    console.error("Update UserGroup Error:", err);
    res.status(500).json({ success: false, message: "Failed to update user group", error: err.message });
  }
});

// DELETE user group
router.delete("/", async (req, res) => {
  try {
    const pool = req.pool;
    const { id } = req.query; // Use query param for DELETE
    console.log(`[DELETE] UserGroup Code requested: '${id}'`);

    const deleteQuery = "DELETE FROM tblusergroup WHERE UserGroupCode = @UserGroupCode";
    const result = await pool.request()
      .input('UserGroupCode', sql.VarChar, id)
      .query(deleteQuery);

    console.log(`[DELETE] rowsAffected:`, result.rowsAffected);
    if (result.rowsAffected[0] === 0) {
      return res.status(404).json({ success: false, message: "User Group not found." });
    }

    res.json({ success: true, message: "User Group deleted successfully." });
  } catch (err) {
    console.error("Delete UserGroup Error:", err);
    res.status(500).json({ success: false, message: "Failed to delete user group", error: err.message });
  }
});

module.exports = router;
