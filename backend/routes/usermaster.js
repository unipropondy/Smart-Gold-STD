const express = require("express");
const router = express.Router();
const { sql } = require("../db");

// Helper to ensure table exists
async function ensureTableExists(pool) {
  const checkTableQuery = `
    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='tblUserMaster' AND xtype='U')
    BEGIN
        CREATE TABLE tblUserMaster (
            UserCode VARCHAR(10) PRIMARY KEY,
            UserGroupCode VARCHAR(6),
            UserName VARCHAR(50),
            Password VARCHAR(10),
            ExpiryDate SMALLDATETIME,
            Designation VARCHAR(50),
            Email VARCHAR(50),
            Phone VARCHAR(20),
            Fax VARCHAR(20),
            StoreCode VARCHAR(15),
            POLimit MONEY,
            Admin SMALLINT DEFAULT 0,
            UpdateApprovedPo SMALLINT,
            UpdateGst SMALLINT,
            DayOfValid SMALLINT,
            DaysOfWarning SMALLINT,
            ChangePassword VARCHAR(1),
            ChangeDate SMALLDATETIME,
            FirstLogDate SMALLDATETIME,
            LastLogDate SMALLDATETIME,
            CreateDate SMALLDATETIME DEFAULT GETDATE(),
            CreateUser VARCHAR(10) DEFAULT 'sys',
            ModifyDate SMALLDATETIME DEFAULT GETDATE(),
            ModifyUser VARCHAR(10) DEFAULT 'sys'
        )
    END
  `;
  try {
    await pool.request().query(checkTableQuery);
  } catch (err) {
    console.error("Error ensuring tblUserMaster exists:", err);
  }
}

// GET User Groups for dropdown
router.get("/groups", async (req, res) => {
  try {
    const pool = req.pool;
    // ensure tblusergroup exists (it's handled in usergroup.js but good practice)
    const result = await pool.request().query("SELECT UserGroupCode, UserGroupName FROM tblusergroup ORDER BY UserGroupCode");
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch User Groups Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// GET all users
router.get("/", async (req, res) => {
  try {
    const pool = req.pool;
    await ensureTableExists(pool);

    const result = await pool.request().query("SELECT * FROM tblUserMaster ORDER BY UserCode ASC");
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch UserMaster Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// POST insert new user
router.post("/", async (req, res) => {
  try {
    const pool = req.pool;
    await ensureTableExists(pool);
    const { UserCode, UserName, UserGroupCode, ExpiryDate, DaysOfWarning } = req.body;

    if (!UserCode) {
      return res.status(400).json({ success: false, message: "User Code is required." });
    }

    // Check if exists
    const checkResult = await pool.request()
      .input('UserCode', sql.VarChar, UserCode)
      .query("SELECT UserCode FROM tblUserMaster WHERE UserCode = @UserCode");
    
    if (checkResult.recordset.length > 0) {
      return res.status(400).json({ success: false, message: "UserCode already exists." });
    }

    const insertQuery = `
      INSERT INTO tblUserMaster (
        UserCode, UserName, UserGroupCode, ExpiryDate, DaysOfWarning, Admin,
        CreateUser, CreateDate, ModifyUser, ModifyDate
      ) VALUES (
        @UserCode, @UserName, @UserGroupCode, @ExpiryDate, @DaysOfWarning, 0,
        'ups', GETDATE(), 'ups', GETDATE()
      )
    `;

    await pool.request()
      .input('UserCode', sql.VarChar, UserCode.substring(0, 10))
      .input('UserName', sql.VarChar, UserName ? UserName.substring(0, 50) : '')
      .input('UserGroupCode', sql.VarChar, UserGroupCode ? UserGroupCode.substring(0, 6) : null)
      .input('ExpiryDate', sql.DateTime, ExpiryDate ? new Date(ExpiryDate) : null)
      .input('DaysOfWarning', sql.SmallInt, parseInt(DaysOfWarning) || 0)
      .query(insertQuery);

    res.json({ success: true, message: "User Master created successfully." });
  } catch (err) {
    console.error("Insert UserMaster Error:", err);
    res.status(500).json({ success: false, message: "Failed to create user", error: err.message });
  }
});

// PUT update user
router.put("/", async (req, res) => {
  try {
    const pool = req.pool;
    const { UserCode, UserName, UserGroupCode, ExpiryDate, DaysOfWarning } = req.body;
    
    if (!UserCode) return res.status(400).json({ success: false, message: "User Code is required" });

    const updateQuery = `
      UPDATE tblUserMaster SET 
        UserName = @UserName,
        UserGroupCode = @UserGroupCode,
        ExpiryDate = @ExpiryDate,
        DaysOfWarning = @DaysOfWarning,
        ModifyUser = 'sys',
        ModifyDate = GETDATE()
      WHERE UserCode = @UserCode
    `;

    const result = await pool.request()
      .input('UserCode', sql.VarChar, UserCode)
      .input('UserName', sql.VarChar, UserName ? UserName.substring(0, 50) : '')
      .input('UserGroupCode', sql.VarChar, UserGroupCode ? UserGroupCode.substring(0, 6) : null)
      .input('ExpiryDate', sql.DateTime, ExpiryDate ? new Date(ExpiryDate) : null)
      .input('DaysOfWarning', sql.SmallInt, parseInt(DaysOfWarning) || 0)
      .query(updateQuery);

    if (result.rowsAffected[0] === 0) {
      return res.status(404).json({ success: false, message: "User not found." });
    }

    res.json({ success: true, message: "User updated successfully." });
  } catch (err) {
    console.error("Update UserMaster Error:", err);
    res.status(500).json({ success: false, message: "Failed to update user", error: err.message });
  }
});

// DELETE user
router.delete("/", async (req, res) => {
  try {
    const pool = req.pool;
    const { id } = req.query; // Use query param for DELETE
    console.log(`[DELETE] User Code requested: '${id}'`);

    const deleteQuery = "DELETE FROM tblUserMaster WHERE UserCode = @UserCode";
    const result = await pool.request()
      .input('UserCode', sql.VarChar, id)
      .query(deleteQuery);

    console.log(`[DELETE] rowsAffected:`, result.rowsAffected);
    if (result.rowsAffected[0] === 0) {
      return res.status(404).json({ success: false, message: "User not found." });
    }

    res.json({ success: true, message: "User deleted successfully." });
  } catch (err) {
    console.error("Delete UserMaster Error:", err);
    res.status(500).json({ success: false, message: "Failed to delete user", error: err.message });
  }
});

module.exports = router;
