const express = require("express");
const { sql } = require("../db");
const router = express.Router();


// ================= LOGIN ================= //
router.post("/login", async (req, res) => {
  try {
    let { username, password } = req.body;

    // ✅ Validation
    username = username ? String(username).trim() : '';
    password = password ? String(password).trim() : '';

    if (!username || !password) {
      return res.status(400).json({
        success: false,
        message: "Username and Password are required"
      });
    }

    const pool = req.pool;

    // Step 1: Query user from tblUserMaster by UserName or UserCode
    let userResult = await pool.request()
      .input("username", sql.VarChar(50), username)
      .query(`
        SELECT 
          RTRIM(UserCode) AS UserCode,
          RTRIM(UserGroupCode) AS UserGroupCode,
          RTRIM(UserName) AS UserName,
          RTRIM(Designation) AS Designation,
          RTRIM(Email) AS Email,
          RTRIM(Phone) AS Phone,
          RTRIM(StoreCode) AS StoreCode,
          Admin,
          RTRIM(ISNULL(Password, '')) AS Password
        FROM tblUserMaster
        WHERE (RTRIM(UserName) = @username OR RTRIM(UserCode) = @username)
      `);

    let matchedUser = null;

    if (userResult.recordset.length > 0) {
      const dbUser = userResult.recordset[0];
      // If the user exists in tblUserMaster:
      // If password is blank or NULL in database, or password does NOT match entered password, REJECT!
      if (!dbUser.Password || dbUser.Password !== password) {
        return res.status(401).json({
          success: false,
          message: !dbUser.Password ? "Account has no password set. Please contact administrator." : "Invalid username or password"
        });
      }
      matchedUser = dbUser;
    } else {
      // Step 2: Check tblSalesMan if not in tblUserMaster
      const smResult = await pool.request()
        .input("username", sql.VarChar(50), username)
        .query(`
          SELECT 
            RTRIM(SalesManCode) AS UserCode,
            RTRIM(SalesManGroup) AS UserGroupCode,
            RTRIM(SalesManName) AS UserName,
            RTRIM(SalesManGroup) AS Designation,
            NULL AS Email,
            NULL AS Phone,
            'HQ' AS StoreCode,
            CASE WHEN UPPER(RTRIM(SalesManGroup)) IN ('ADMIN', 'SUPER ADMIN') THEN 1 ELSE 0 END AS Admin,
            RTRIM(ISNULL(Password, '')) AS Password
          FROM tblSalesMan
          WHERE (
            RTRIM(SalesManCode) = @username 
            OR RTRIM(SalesManName) = @username
          )
        `);

      if (smResult.recordset.length > 0) {
        const smUser = smResult.recordset[0];
        if (!smUser.Password || smUser.Password !== password) {
          return res.status(401).json({
            success: false,
            message: !smUser.Password ? "Account has no password set. Please contact administrator." : "Invalid username or password"
          });
        }
        matchedUser = smUser;
      }
    }

    // ❌ Invalid credentials / User not found
    if (!matchedUser) {
      return res.status(401).json({
        success: false,
        message: "Invalid username or password"
      });
    }

    // ✅ Success
    return res.status(200).json({
      success: true,
      message: "Login successful",
      user: {
        userCode: matchedUser.UserCode,
        userName: matchedUser.UserName,
        userGroupCode: matchedUser.UserGroupCode,
        designation: matchedUser.Designation,
        email: matchedUser.Email,
        phone: matchedUser.Phone,
        storeCode: matchedUser.StoreCode,
        admin: matchedUser.Admin
      }
    });

  } catch (err) {
    console.error("🔥 LOGIN ERROR:", err);

    return res.status(500).json({
      success: false,
      message: "Internal server error",
      error: err.message
    });
  }
});

// ================= CHANGE PASSWORD ================= //
router.post("/changepassword", async (req, res) => {
  try {
    let { userId, currentPassword, newPassword } = req.body;

    userId = userId?.trim();
    currentPassword = currentPassword?.trim();
    newPassword = newPassword?.trim();

    if (!userId || !currentPassword || !newPassword) {
      return res.status(400).json({
        success: false,
        message: "User ID, Current Password, and New Password are required"
      });
    }

    const pool = req.pool;
    console.log("Changing password for user:", userId);

    // Verify current password - check against UserName OR UserCode in tblUserMaster
    let result = await pool.request()
      .input("username", sql.VarChar(50), userId)
      .input("password", sql.VarChar(50), currentPassword)
      .query(`
        SELECT UserCode, UserName 
        FROM tblUserMaster
        WHERE (RTRIM(UserName) = @username OR RTRIM(UserCode) = @username)
        AND RTRIM(Password) = @password
      `);
      
    console.log("Verify result tblUserMaster:", result.recordset);

    if (result.recordset.length > 0) {
      const actualUserName = result.recordset[0].UserName;
      console.log("Updating password in tblUserMaster for:", actualUserName);
      await pool.request()
        .input("username", sql.VarChar(50), actualUserName)
        .input("newPassword", sql.VarChar(50), newPassword)
        .query(`
          UPDATE tblUserMaster
          SET Password = @newPassword,
              ChangePassword = 'Y',
              ChangeDate = GETDATE()
          WHERE RTRIM(UserName) = @username
        `);

      return res.status(200).json({
        success: true,
        message: "Password changed successfully"
      });
    }

    // Check tblSalesMan if not found in tblUserMaster
    const smResult = await pool.request()
      .input("username", sql.VarChar(50), userId)
      .input("password", sql.VarChar(50), currentPassword)
      .query(`
        SELECT SalesManCode, SalesManName 
        FROM tblSalesMan
        WHERE (RTRIM(SalesManName) = @username OR RTRIM(SalesManCode) = @username)
        AND RTRIM(Password) = @password
      `);

    if (smResult.recordset.length > 0) {
      const smCode = smResult.recordset[0].SalesManCode;
      console.log("Updating password in tblSalesMan for:", smCode);
      await pool.request()
        .input("code", sql.VarChar(50), smCode)
        .input("newPassword", sql.VarChar(50), newPassword)
        .query(`
          UPDATE tblSalesMan
          SET Password = @newPassword
          WHERE RTRIM(SalesManCode) = @code
        `);

      return res.status(200).json({
        success: true,
        message: "Password changed successfully"
      });
    }

    return res.status(401).json({
      success: false,
      message: "Invalid current password"
    });

  } catch (err) {
    console.error("🔥 CHANGE PASSWORD ERROR:", err);
    return res.status(500).json({
      success: false,
      message: "Internal server error",
      error: err.message
    });
  }
});

module.exports = router;
