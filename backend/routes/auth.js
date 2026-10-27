const express = require("express");
const { sql } = require("../db");
const router = express.Router();


// ================= LOGIN ================= //
router.post("/login", async (req, res) => {
  try {
    let { username, password } = req.body;

    // ✅ Validation
    username = username?.trim();
    password = password?.trim();

    if (!username || !password) {
      return res.status(400).json({
        success: false,
        message: "Username and Password are required"
      });
    }

    const pool = req.pool;

    const result = await pool.request()
      .input("username", sql.VarChar(50), username)
      .input("password", sql.VarChar(10), password)
      .query(`
        SELECT 
          UserCode,
          UserGroupCode,
          UserName,
          Designation,
          Email,
          Phone,
          StoreCode,
          Admin
        FROM tblUserMaster
        WHERE UserName = @username 
        AND Password = @password
      `);

    // ❌ Invalid credentials
    if (result.recordset.length === 0) {
      return res.status(401).json({
        success: false,
        message: "Invalid username or password"
      });
    }

    const user = result.recordset[0];

    // ✅ Success
    return res.status(200).json({
      success: true,
      message: "Login successful",
      user: {
        userCode: user.UserCode,
        userName: user.UserName,
        userGroupCode: user.UserGroupCode,
        designation: user.Designation,
        email: user.Email,
        phone: user.Phone,
        storeCode: user.StoreCode,
        admin: user.Admin
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

    // Verify current password
    const result = await pool.request()
      .input("username", sql.VarChar(50), userId)
      .input("password", sql.VarChar(10), currentPassword)
      .query(`
        SELECT UserCode 
        FROM tblUserMaster
        WHERE UserName = @username 
        AND Password = @password
      `);
      
    console.log("Verify result:", result.recordset);

    if (result.recordset.length === 0) {
      return res.status(401).json({
        success: false,
        message: "Invalid current password"
      });
    }

    console.log("Updating password...");
    // Update password
    await pool.request()
      .input("username", sql.VarChar(50), userId)
      .input("newPassword", sql.VarChar(10), newPassword)
      .query(`
        UPDATE tblUserMaster
        SET Password = @newPassword,
            ChangePassword = 'Y',
            ChangeDate = GETDATE()
        WHERE UserName = @username
      `);
      
    console.log("Password updated successfully.");

    return res.status(200).json({
      success: true,
      message: "Password changed successfully"
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
