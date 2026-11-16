const express = require("express");
const router = express.Router();
const { sql } = require("../db");

// Helper to ensure table schema
async function ensureTableSchema(pool) {
  try {
    const checkTableQuery = `
      IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='tblUserpermission' AND xtype='U')
      BEGIN
          CREATE TABLE tblUserpermission (
              UserGroupCode VARCHAR(50),
              FormCode VARCHAR(50),
              AllowAdd VARCHAR(1) DEFAULT 'N',
              AllowUpdate VARCHAR(1) DEFAULT 'N',
              AllowDelete VARCHAR(1) DEFAULT 'N',
              AllowRead VARCHAR(1) DEFAULT 'N',
              CreateUser VARCHAR(50) DEFAULT 'sys',
              CreateDate DATETIME DEFAULT GETDATE(),
              ModifyUser VARCHAR(50) DEFAULT 'sys',
              ModifyDate DATETIME DEFAULT GETDATE(),
              PRIMARY KEY (UserGroupCode, FormCode)
          )
      END
    `;
    await pool.request().query(checkTableQuery);

    try {
      await pool.request().query(`
        IF EXISTS (
          SELECT * FROM sys.columns 
          WHERE object_id = OBJECT_ID('tblUserpermission') 
            AND name = 'FormCode' 
            AND max_length < 50
        )
        BEGIN
          ALTER TABLE tblUserpermission ALTER COLUMN FormCode VARCHAR(50) NOT NULL;
        END
      `);
    } catch (_) {}
  } catch (err) {
    console.error("Error ensuring tblUserpermission schema:", err);
  }
}

// GET all user groups for dropdown
router.get("/groups", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query(`
      SELECT DISTINCT RTRIM(UserGroupCode) AS UserGroupCode, RTRIM(UserGroupName) AS UserGroupName 
      FROM tblusergroup 
      ORDER BY UserGroupCode ASC
    `);
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch User Groups Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// GET permissions for a specific UserGroup (or all)
router.get("/", async (req, res) => {
  try {
    const pool = req.pool;
    await ensureTableSchema(pool);

    const { groupCode } = req.query;
    let query = `
      SELECT 
        RTRIM(UserGroupCode) AS UserGroupCode,
        RTRIM(FormCode) AS FormCode,
        RTRIM(ISNULL(AllowRead, 'N')) AS AllowRead,
        RTRIM(ISNULL(AllowAdd, 'N')) AS AllowAdd,
        RTRIM(ISNULL(AllowUpdate, 'N')) AS AllowUpdate,
        RTRIM(ISNULL(AllowDelete, 'N')) AS AllowDelete
      FROM tblUserpermission
    `;

    const request = pool.request();
    if (groupCode) {
      query += " WHERE RTRIM(UserGroupCode) = @groupCode";
      request.input("groupCode", sql.VarChar(50), groupCode.trim());
    }
    query += " ORDER BY UserGroupCode ASC, FormCode ASC";

    const result = await request.query(query);
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch UserPermission Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// GET permitted node names for a specific User Group
router.get("/by-group/:groupCode", async (req, res) => {
  try {
    const pool = req.pool;
    await ensureTableSchema(pool);
    const groupCode = (req.params.groupCode || '').trim();

    if (!groupCode) {
      return res.status(400).json({ success: false, message: "Group code is required" });
    }

    const result = await pool.request()
      .input("groupCode", sql.VarChar(50), groupCode)
      .query(`
        SELECT 
          RTRIM(FormCode) AS FormCode,
          RTRIM(ISNULL(AllowRead, 'N')) AS AllowRead
        FROM tblUserpermission
        WHERE RTRIM(UserGroupCode) = @groupCode
      `);

    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Permissions By Group Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// POST save bulk node permissions for a User Group
router.post("/batch", async (req, res) => {
  const pool = req.pool;
  const transaction = new sql.Transaction(pool);
  try {
    await ensureTableSchema(pool);
    const { UserGroupCode, permissions, userId } = req.body;

    if (!UserGroupCode || !Array.isArray(permissions)) {
      return res.status(400).json({ success: false, message: "UserGroupCode and permissions array required" });
    }

    const cleanGroup = String(UserGroupCode).trim();
    const modUser = (userId || 'sys').substring(0, 10);
    const toYorN = (val) => (val === 'Y' || val === '1' || val === true) ? 'Y' : 'N';

    await transaction.begin();

    for (const p of permissions) {
      if (!p.FormCode) continue;
      const cleanForm = String(p.FormCode).trim();
      const aRead = toYorN(p.AllowRead);

      const checkReq = new sql.Request(transaction);
      const checkRes = await checkReq
        .input('UserGroupCode', sql.VarChar(50), cleanGroup)
        .input('FormCode', sql.VarChar(50), cleanForm)
        .query("SELECT UserGroupCode FROM tblUserpermission WHERE RTRIM(UserGroupCode) = @UserGroupCode AND RTRIM(FormCode) = @FormCode");

      const saveReq = new sql.Request(transaction);
      saveReq.input('UserGroupCode', sql.VarChar(50), cleanGroup);
      saveReq.input('FormCode', sql.VarChar(50), cleanForm);
      saveReq.input('AllowRead', sql.VarChar(1), aRead);
      saveReq.input('User', sql.VarChar(10), modUser);

      if (checkRes.recordset.length > 0) {
        await saveReq.query(`
          UPDATE tblUserpermission SET
            AllowRead = @AllowRead,
            ModifyUser = @User,
            ModifyDate = GETDATE()
          WHERE RTRIM(UserGroupCode) = @UserGroupCode AND RTRIM(FormCode) = @FormCode
        `);
      } else {
        await saveReq.query(`
          INSERT INTO tblUserpermission (
            UserGroupCode, FormCode, AllowAdd, AllowUpdate, AllowDelete, AllowRead,
            CreateUser, CreateDate, ModifyUser, ModifyDate
          ) VALUES (
            @UserGroupCode, @FormCode, 'N', 'N', 'N', @AllowRead,
            @User, GETDATE(), @User, GETDATE()
          )
        `);
      }
    }

    await transaction.commit();
    res.json({ success: true, message: "Permissions saved successfully." });
  } catch (err) {
    if (transaction) await transaction.rollback();
    console.error("Batch UserPermission Error:", err);
    res.status(500).json({ success: false, message: "Failed to save permissions batch", error: err.message });
  }
});

// Single insert/update
router.post("/", async (req, res) => {
  try {
    const pool = req.pool;
    await ensureTableSchema(pool);
    const { UserGroupCode, FormCode, AllowRead, userId } = req.body;

    if (!UserGroupCode || !FormCode) {
      return res.status(400).json({ success: false, message: "User Group Code and Form Code are required." });
    }

    const cleanGroup = String(UserGroupCode).trim();
    const cleanForm = String(FormCode).trim();
    const aRead = (AllowRead === 'Y' || AllowRead === true || AllowRead === '1') ? 'Y' : 'N';
    const modUser = (userId || 'sys').substring(0, 10);

    const checkResult = await pool.request()
      .input('UserGroupCode', sql.VarChar(50), cleanGroup)
      .input('FormCode', sql.VarChar(50), cleanForm)
      .query("SELECT UserGroupCode FROM tblUserpermission WHERE RTRIM(UserGroupCode) = @UserGroupCode AND RTRIM(FormCode) = @FormCode");

    if (checkResult.recordset.length > 0) {
      await pool.request()
        .input('UserGroupCode', sql.VarChar(50), cleanGroup)
        .input('FormCode', sql.VarChar(50), cleanForm)
        .input('AllowRead', sql.VarChar(1), aRead)
        .input('ModifyUser', sql.VarChar(10), modUser)
        .query(`
          UPDATE tblUserpermission SET 
            AllowRead = @AllowRead,
            ModifyUser = @ModifyUser,
            ModifyDate = GETDATE()
          WHERE RTRIM(UserGroupCode) = @UserGroupCode AND RTRIM(FormCode) = @FormCode
        `);
      res.json({ success: true, message: "Permission updated successfully." });
    } else {
      await pool.request()
        .input('UserGroupCode', sql.VarChar(50), cleanGroup)
        .input('FormCode', sql.VarChar(50), cleanForm)
        .input('AllowRead', sql.VarChar(1), aRead)
        .input('CreateUser', sql.VarChar(10), modUser)
        .query(`
          INSERT INTO tblUserpermission (
            UserGroupCode, FormCode, AllowAdd, AllowUpdate, AllowDelete, AllowRead,
            CreateUser, CreateDate, ModifyUser, ModifyDate
          ) VALUES (
            @UserGroupCode, @FormCode, 'N', 'N', 'N', @AllowRead,
            @CreateUser, GETDATE(), @CreateUser, GETDATE()
          )
        `);
      res.json({ success: true, message: "Permission created successfully." });
    }
  } catch (err) {
    console.error("Save UserPermission Error:", err);
    res.status(500).json({ success: false, message: "Failed to save permission", error: err.message });
  }
});

// DELETE permission
router.delete("/", async (req, res) => {
  try {
    const pool = req.pool;
    const { groupCode, formCode } = req.query;

    if (!groupCode || !formCode) {
      return res.status(400).json({ success: false, message: "groupCode and formCode are required." });
    }

    const result = await pool.request()
      .input('UserGroupCode', sql.VarChar(50), groupCode.trim())
      .input('FormCode', sql.VarChar(50), formCode.trim())
      .query("DELETE FROM tblUserpermission WHERE RTRIM(UserGroupCode) = @UserGroupCode AND RTRIM(FormCode) = @FormCode");

    res.json({ success: true, message: "Permission deleted successfully." });
  } catch (err) {
    console.error("Delete UserPermission Error:", err);
    res.status(500).json({ success: false, message: "Failed to delete permission", error: err.message });
  }
});

module.exports = router;
