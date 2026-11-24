const express = require("express");
const { sql } = require("../db");
const router = express.Router();

// Helper to execute customer expiry updates equivalent to frmSignon.vb:
// objCustomer.Customer180ExpiryUpdate() -> EXEC sp_CustomerUpdate180
// objCustomer.Customer270ExpiryUpdate() -> EXEC sp_CustomerUpdate270
async function executeCustomerExpiryUpdates(pool) {
  try {
    const sp180Check = await pool.request().query("SELECT 1 FROM sys.procedures WHERE name = 'sp_CustomerUpdate180'");
    if (sp180Check.recordset.length > 0) {
      await pool.request().query("EXEC sp_CustomerUpdate180");
    }
  } catch (err180) {
    // Non-fatal, log for audit
    console.warn("[Login Init] sp_CustomerUpdate180 warning:", err180.message);
  }

  try {
    const sp270Check = await pool.request().query("SELECT 1 FROM sys.procedures WHERE name = 'sp_CustomerUpdate270'");
    if (sp270Check.recordset.length > 0) {
      await pool.request().query("EXEC sp_CustomerUpdate270");
    }
  } catch (err270) {
    console.warn("[Login Init] sp_CustomerUpdate270 warning:", err270.message);
  }
}

// ================= LOGIN ================= //
router.post("/login", async (req, res) => {
  try {
    let { username, password, workstation } = req.body;

    // ✅ Validation
    username = username ? String(username).trim() : '';
    password = password ? String(password).trim() : '';
    workstation = workstation ? String(workstation).trim() : '';

    if (!username || !password) {
      return res.status(400).json({
        success: false,
        message: "Please enter both username and password."
      });
    }

    const pool = req.pool;

    // Run background customer expiry maintenance (same as frmSignon.vb)
    executeCustomerExpiryUpdates(pool).catch(() => {});

    // Step 1: Query user from tblUserMaster by UserCode or UserName
    let userResult = await pool.request()
      .input("username", sql.VarChar(50), username)
      .query(`
        SELECT 
          RTRIM(ISNULL(UserCode, '')) AS UserCode,
          RTRIM(ISNULL(UserGroupCode, '')) AS UserGroupCode,
          RTRIM(ISNULL(UserName, '')) AS UserName,
          RTRIM(ISNULL(Designation, '')) AS Designation,
          RTRIM(ISNULL(Email, '')) AS Email,
          RTRIM(ISNULL(Phone, '')) AS Phone,
          RTRIM(ISNULL(StoreCode, '')) AS StoreCode,
          ISNULL(Admin, 0) AS Admin,
          RTRIM(ISNULL(Password, '')) AS Password,
          ExpiryDate,
          ISNULL(DaysOfWarning, 0) AS DaysOfWarning,
          RTRIM(ISNULL(ChangePassword, '')) AS ChangePassword,
          LastLogDate
        FROM tblUserMaster
        WHERE (RTRIM(UserCode) = @username OR RTRIM(UserName) = @username)
      `);

    let matchedUser = null;
    let isSalesManFallback = false;

    if (userResult.recordset.length > 0) {
      const dbUser = userResult.recordset[0];
      
      // If user has no password set in database
      if (!dbUser.Password) {
        return res.status(401).json({
          success: false,
          message: "Please set your password."
        });
      }

      // Password comparison (case-sensitive check against DB)
      if (dbUser.Password !== password) {
        return res.status(401).json({
          success: false,
          message: "Invalid UserID or Password"
        });
      }

      // Check account expiration if ExpiryDate is defined
      if (dbUser.ExpiryDate) {
        const expDate = new Date(dbUser.ExpiryDate);
        const today = new Date();
        today.setHours(0, 0, 0, 0);
        if (expDate < today) {
          return res.status(403).json({
            success: false,
            message: "User account has expired. Please contact system administrator."
          });
        }
      }

      matchedUser = dbUser;
    } else {
      // Step 2: Fallback check against tblSalesMan if not in tblUserMaster
      const smResult = await pool.request()
        .input("username", sql.VarChar(50), username)
        .query(`
          SELECT 
            RTRIM(ISNULL(SalesManCode, '')) AS UserCode,
            RTRIM(ISNULL(SalesManGroup, '')) AS UserGroupCode,
            RTRIM(ISNULL(SalesManName, '')) AS UserName,
            RTRIM(ISNULL(SalesManGroup, '')) AS Designation,
            '' AS Email,
            '' AS Phone,
            'HQ' AS StoreCode,
            CASE WHEN UPPER(RTRIM(ISNULL(SalesManGroup, ''))) IN ('ADMIN', 'SUPER ADMIN') THEN 1 ELSE 0 END AS Admin,
            RTRIM(ISNULL(Password, '')) AS Password,
            NULL AS ExpiryDate,
            0 AS DaysOfWarning,
            '' AS ChangePassword,
            NULL AS LastLogDate
          FROM tblSalesMan
          WHERE (RTRIM(SalesManCode) = @username OR RTRIM(SalesManName) = @username)
        `);

      if (smResult.recordset.length > 0) {
        const smUser = smResult.recordset[0];
        if (!smUser.Password) {
          return res.status(401).json({
            success: false,
            message: "Please set your password."
          });
        }
        if (smUser.Password !== password) {
          return res.status(401).json({
            success: false,
            message: "Invalid UserID or Password"
          });
        }
        matchedUser = smUser;
        isSalesManFallback = true;
      }
    }

    // ❌ Invalid credentials / User not found
    if (!matchedUser) {
      return res.status(401).json({
        success: false,
        message: "Invalid UserID or Password"
      });
    }

    // Step 3: Fetch Company configuration (clsCompany1.GetCompanyList in frmSignon.vb)
    let companyConfig = {};
    try {
      const compResult = await pool.request().query("SELECT TOP 1 * FROM tblCompany ORDER BY CompanyID ASC");
      if (compResult.recordset.length > 0) {
        const rawComp = compResult.recordset[0];
        companyConfig = {
          companyId: rawComp.CompanyID || 1,
          companyName: String(rawComp.Name || '').trim(),
          companyShortCode: String(rawComp.CompanyShortCode || '').trim().substring(0, 3),
          address1: String(rawComp.Address1 || '').trim(),
          address2: String(rawComp.Address2 || '').trim(),
          phone: String(rawComp.Phone || '').trim(),
          fax: String(rawComp.Fax || '').trim(),
          billMessage1: String(rawComp.Message1 || '').trim(),
          billMessage2: String(rawComp.Message2 || '').trim(),
          gstRegNo: String(rawComp.GSTREGNO || '').trim(),
          gstPercentage: parseFloat(rawComp.GstPerc || rawComp.GSTPerc || 0) || 0,
          serviceCharge: parseFloat(rawComp.ServiceCharge || 0) || 0,
          locationCode: String(rawComp.LocationCode || 'HQ').trim(),
          mainLocationCode: String(rawComp.MainLocationCode || 'HQ').trim(),
          defaultCurrency: String(rawComp.DefaultCurrency || 'SGD').trim(),
          purchaseCostingGL: String(rawComp.PurchaseCostingGL || '').trim(),
          purWtVarPerc: parseFloat(rawComp.PurWtVarPerc || 0) || 0,
          memberModule: String(rawComp.MemberModule || '0').trim(),
          accountsModule: String(rawComp.AccountsModule || '0').trim(),
          rfidModule: String(rawComp.RFIDMOdule || '0').trim(),
          isDetailDiscountAllowed: String(rawComp.IsDetailDiscountAllowed || '0').trim(),
          member: rawComp.Member === true || rawComp.Member === 1,
          allowMultipleDiscount: rawComp.AllowMultipleDiscount === true || rawComp.AllowMultipleDiscount === 1,
          rental: rawComp.Rental === true || rawComp.Rental === 1,
          billPrint: String(rawComp.BillPrint || '0').trim()
        };
      }
    } catch (errComp) {
      console.warn("[Login Init] Failed to load company settings:", errComp.message);
    }

    // Step 4: Resolve Location Type (clsLocation.GetLocationwithCode in frmSignon.vb)
    let locationType = '';
    const activeLocationCode = matchedUser.StoreCode || companyConfig.locationCode || 'HQ';
    try {
      const locResult = await pool.request()
        .input("locCode", sql.VarChar(50), activeLocationCode)
        .query("SELECT TOP 1 RTRIM(ISNULL(LocationType, '')) AS LocationType FROM tblLocation WHERE LocationCode = @locCode");
      if (locResult.recordset.length > 0) {
        locationType = locResult.recordset[0].LocationType;
      }
    } catch (errLoc) {
      console.warn("[Login Init] Failed to resolve location type:", errLoc.message);
    }

    // Step 5: Resolve Terminal by Workstation/Computer Name (clsTerminal.GetTerminalByComputer in frmSignon.vb)
    let terminalInfo = { terminalCode: 'T1', terminalName: 'Terminal 1' };
    if (workstation) {
      try {
        const termResult = await pool.request()
          .input("computerName", sql.VarChar(100), workstation)
          .query("SELECT TOP 1 RTRIM(TerminalCode) AS TerminalCode, RTRIM(TerminalName) AS TerminalName FROM tblTerminal WHERE ComputerName = @computerName");
        if (termResult.recordset.length > 0) {
          terminalInfo = {
            terminalCode: termResult.recordset[0].TerminalCode,
            terminalName: termResult.recordset[0].TerminalName
          };
        }
      } catch (errTerm) {
        console.warn("[Login Init] Failed to resolve terminal by computer name:", errTerm.message);
      }
    }

    // Step 6: Fetch Permissions for User Group (tblUserpermission)
    let permissions = [];
    const userGroup = (matchedUser.UserGroupCode || '').trim();
    if (userGroup) {
      try {
        const permResult = await pool.request()
          .input("groupCode", sql.VarChar(50), userGroup)
          .query(`
            SELECT 
              RTRIM(FormCode) AS FormCode,
              RTRIM(ISNULL(AllowRead, 'N')) AS AllowRead,
              RTRIM(ISNULL(AllowAdd, 'N')) AS AllowAdd,
              RTRIM(ISNULL(AllowUpdate, 'N')) AS AllowUpdate,
              RTRIM(ISNULL(AllowDelete, 'N')) AS AllowDelete
            FROM tblUserpermission
            WHERE RTRIM(UserGroupCode) = @groupCode
          `);
        permissions = permResult.recordset;
      } catch (errPerm) {
        console.warn("[Login Init] Failed to fetch user group permissions:", errPerm.message);
      }
    }

    // Step 7: Update LastLogDate in tblUserMaster
    if (!isSalesManFallback && matchedUser.UserCode) {
      try {
        await pool.request()
          .input("userCode", sql.VarChar(50), matchedUser.UserCode)
          .query("UPDATE tblUserMaster SET LastLogDate = GETDATE() WHERE UserCode = @userCode");
      } catch (errLog) {
        console.warn("[Login Init] Failed to update LastLogDate:", errLog.message);
      }
    }

    // Calculate GST Tael Factor matching GstTaelPercentage = Val(1 + (Val(Objuser.GstPercentage) / 100))
    const gstPerc = companyConfig.gstPercentage || 9;
    const gstTaelFactor = 1 + (gstPerc / 100);

    // ✅ Complete Login Response matching VB.NET frmSignon session payload
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
        storeCode: activeLocationCode,
        admin: matchedUser.Admin,
        expiryDate: matchedUser.ExpiryDate,
        changePasswordPrompt: matchedUser.UserGroupCode === 'N' || matchedUser.ChangePassword === 'Y',
        lastLogDate: matchedUser.LastLogDate
      },
      company: {
        ...companyConfig,
        gstTaelFactor: Number(gstTaelFactor.toFixed(4)),
        locationType: locationType
      },
      terminal: terminalInfo,
      permissions: permissions
    });

  } catch (err) {
    console.error("🔥 LOGIN ERROR:", err);

    return res.status(500).json({
      success: false,
      message: "Internal server error: " + (err.message || 'Database error'),
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
