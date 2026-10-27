const express = require("express");
const router = express.Router();
const { sql } = require("../db");

// Helper to ensure table exists
async function ensureTableExists(pool) {
  const checkTableQuery = `
    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='tbluserpermission' AND xtype='U')
    BEGIN
        CREATE TABLE tbluserpermission (
            UserGroupCode VARCHAR(6),
            FormCode VARCHAR(50),
            AllowAdd VARCHAR(1),
            AllowUpdate VARCHAR(1),
            AllowDelete VARCHAR(1),
            AllowRead VARCHAR(1),
            CreateUser VARCHAR(10) DEFAULT 'sys',
            CreateDate DATETIME DEFAULT GETDATE(),
            ModifyUser VARCHAR(10) DEFAULT 'sys',
            ModifyDate DATETIME DEFAULT GETDATE(),
            PRIMARY KEY (UserGroupCode, FormCode)
        )
    END
  `;
  try {
    await pool.request().query(checkTableQuery);
  } catch (err) {
    console.error("Error ensuring tbluserpermission exists:", err);
  }
}

// GET User Groups for dropdown
router.get("/groups", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query("SELECT UserGroupCode, UserGroupName FROM tblusergroup ORDER BY UserGroupCode");
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch User Groups Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// GET all permissions
router.get("/", async (req, res) => {
  try {
    const pool = req.pool;
    await ensureTableExists(pool);

    const result = await pool.request().query("SELECT * FROM tbluserpermission ORDER BY UserGroupCode ASC, FormCode ASC");
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch UserPermission Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// POST insert or update permission
router.post("/", async (req, res) => {
  try {
    const pool = req.pool;
    await ensureTableExists(pool);
    const { UserGroupCode, FormCode, AllowAdd, AllowUpdate, AllowDelete, AllowRead } = req.body;

    if (!UserGroupCode || !FormCode) {
      return res.status(400).json({ success: false, message: "User Group Code and Form Code are required." });
    }

    // Check if exists
    const checkResult = await pool.request()
      .input('UserGroupCode', sql.VarChar, UserGroupCode)
      .input('FormCode', sql.VarChar, FormCode)
      .query("SELECT UserGroupCode FROM tbluserpermission WHERE UserGroupCode = @UserGroupCode AND FormCode = @FormCode");
    
    if (checkResult.recordset.length > 0) {
      // Update
      const updateQuery = `
        UPDATE tbluserpermission SET 
          AllowAdd = @AllowAdd,
          AllowUpdate = @AllowUpdate,
          AllowDelete = @AllowDelete,
          AllowRead = @AllowRead,
          ModifyUser = 'sys',
          ModifyDate = GETDATE()
        WHERE UserGroupCode = @UserGroupCode AND FormCode = @FormCode
      `;
      await pool.request()
        .input('UserGroupCode', sql.VarChar, UserGroupCode)
        .input('FormCode', sql.VarChar, FormCode.substring(0, 6))
        .input('AllowAdd', sql.VarChar, AllowAdd ? AllowAdd.substring(0, 1) : ' ')
        .input('AllowUpdate', sql.VarChar, AllowUpdate ? AllowUpdate.substring(0, 1) : ' ')
        .input('AllowDelete', sql.VarChar, AllowDelete ? AllowDelete.substring(0, 1) : ' ')
        .input('AllowRead', sql.VarChar, AllowRead ? AllowRead.substring(0, 1) : ' ')
        .query(updateQuery);
        
      res.json({ success: true, message: "Permission updated successfully." });
    } else {
      // Insert
      const insertQuery = `
        INSERT INTO tbluserpermission (
          UserGroupCode, FormCode, AllowAdd, AllowUpdate, AllowDelete, AllowRead,
          CreateUser, CreateDate, ModifyUser, ModifyDate
        ) VALUES (
          @UserGroupCode, @FormCode, @AllowAdd, @AllowUpdate, @AllowDelete, @AllowRead,
          'sys', GETDATE(), 'sys', GETDATE()
        )
      `;

      await pool.request()
        .input('UserGroupCode', sql.VarChar, UserGroupCode.substring(0, 6))
        .input('FormCode', sql.VarChar, FormCode.substring(0, 6))
        .input('AllowAdd', sql.VarChar, AllowAdd ? AllowAdd.substring(0, 1) : ' ')
        .input('AllowUpdate', sql.VarChar, AllowUpdate ? AllowUpdate.substring(0, 1) : ' ')
        .input('AllowDelete', sql.VarChar, AllowDelete ? AllowDelete.substring(0, 1) : ' ')
        .input('AllowRead', sql.VarChar, AllowRead ? AllowRead.substring(0, 1) : ' ')
        .query(insertQuery);

      res.json({ success: true, message: "Permission created successfully." });
    }
  } catch (err) {
    console.error("Insert/Update UserPermission Error:", err);
    res.status(500).json({ success: false, message: "Failed to save permission", error: err.message });
  }
});

// DELETE permission
router.delete("/", async (req, res) => {
  try {
    const pool = req.pool;
    const { groupCode, formCode } = req.query; // Use query params for DELETE
    console.log(`[DELETE] Permission requested: Group '${groupCode}', Form '${formCode}'`);

    const deleteQuery = "DELETE FROM tbluserpermission WHERE UserGroupCode = @UserGroupCode AND FormCode = @FormCode";
    const result = await pool.request()
      .input('UserGroupCode', sql.VarChar, groupCode)
      .input('FormCode', sql.VarChar, formCode)
      .query(deleteQuery);

    console.log(`[DELETE] rowsAffected:`, result.rowsAffected);
    if (result.rowsAffected[0] === 0) {
      return res.status(404).json({ success: false, message: "Permission not found." });
    }

    res.json({ success: true, message: "Permission deleted successfully." });
  } catch (err) {
    console.error("Delete UserPermission Error:", err);
    res.status(500).json({ success: false, message: "Failed to delete permission", error: err.message });
  }
});

module.exports = router;
