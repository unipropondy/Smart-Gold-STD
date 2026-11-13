/** Developer: durairaj */
const express = require("express");
const router = express.Router();
const { sql, poolPromise } = require("../db");

// Helper to acquire database connection pool reliably
const getPool = async (req) => {
  if (req && req.pool) return req.pool;
  if (poolPromise) return await poolPromise;
  throw new Error("Database pool is not available.");
};

// GET Company Info (GST % and Company Short Code)
router.get("/company-info", async (req, res) => {
  try {
    const pool = await getPool(req);
    const result = await pool.request().query("SELECT TOP 1 GSTPerc, CompanyShortCode FROM tblCompany");
    if (result.recordset && result.recordset.length > 0) {
      res.json({
        success: true,
        GSTPerc: result.recordset[0].GSTPerc || 0,
        CompanyShortCode: (result.recordset[0].CompanyShortCode || "").trim()
      });
    } else {
      res.json({ success: true, GSTPerc: 0, CompanyShortCode: "" });
    }
  } catch (err) {
    console.error("Fetch Company Info Error:", err);
    res.status(500).json({ success: false, message: "Failed to fetch company configuration", error: err.message });
  }
});

// GET Next Voucher Number based on Type
router.get("/next-voucher", async (req, res) => {
  try {
    const pool = await getPool(req);
    const { type } = req.query;
    if (!type) {
      return res.status(400).json({ success: false, message: "Type is required" });
    }

    const result = await pool.request()
      .input("Type", sql.VarChar, type)
      .query("SELECT ISNULL(MAX(VoucherNo), 0) + 1 AS NextVoucherNo FROM tblMisc WHERE Type = @Type");

    res.json({ success: true, VoucherNo: result.recordset[0].NextVoucherNo });
  } catch (err) {
    console.error("Fetch Next Voucher Error:", err);
    res.status(500).json({ success: false, message: "Failed to generate next voucher number", error: err.message });
  }
});

// GET/Fetch specific Miscellaneous Entry
router.get("/fetch", async (req, res) => {
  try {
    const pool = await getPool(req);
    const { type, voucherNo } = req.query;
    if (!type || !voucherNo) {
      return res.status(400).json({ success: false, message: "Type and VoucherNo are required" });
    }

    const result = await pool.request()
      .input("Type", sql.VarChar, type)
      .input("VoucherNo", sql.Int, parseInt(voucherNo))
      .query("SELECT * FROM tblMisc WHERE Type = @Type AND VoucherNo = @VoucherNo");

    if (result.recordset && result.recordset.length > 0) {
      res.json({ success: true, data: result.recordset[0] });
    } else {
      res.status(200).json({ success: false, message: "Miscellaneous entry not found" });
    }
  } catch (err) {
    console.error("Fetch Misc Entry Error:", err);
    res.status(500).json({ success: false, message: "Failed to fetch entry", error: err.message });
  }
});

// GET /api/misc/print-data?type=...&voucherNo=...
// Crystal Report: Reports/rptMiscJournalNew.rpt
// Selection: {tblMisc.VoucherNo} = @VoucherNo and {tblMisc.Type} = @Type
// Company info matching ds = SBll.getCompanyName
router.get("/print-data", async (req, res) => {
  try {
    const pool = await getPool(req);
    const type = (req.query.type || "").trim();
    const voucherNo = req.query.voucherNo;

    if (!type || voucherNo === undefined || voucherNo === null || String(voucherNo).trim() === "") {
      return res.status(400).json({ success: false, message: "Type and VoucherNo are required for print." });
    }

    const vNoInt = parseInt(String(voucherNo).trim(), 10);
    if (isNaN(vNoInt)) {
      return res.status(400).json({ success: false, message: "Invalid VoucherNo format." });
    }

    // 1. Fetch Company details matching SBll.getCompanyName
    const compRes = await pool.request().query(`
      SELECT TOP 1 Name, Address1, Address2, City, State, Country, Phone, Fax, CompanyShortCode, CurrencySign
      FROM tblCompany
    `);
    const compRow = compRes.recordset[0] || {};
    const company = {
      name: (compRow.Name || "SMART").trim(),
      address1: (compRow.Address1 || "").trim(),
      address2: (compRow.Address2 || "").trim(),
      city: (compRow.City || "").trim(),
      state: (compRow.State || "").trim(),
      country: (compRow.Country || "").trim(),
      phone: (compRow.Phone || "").trim(),
      fax: (compRow.Fax || "").trim(),
      companyShortCode: (compRow.CompanyShortCode || "").trim(),
      currencySign: (compRow.CurrencySign || "SGD").trim()
    };

    // 2. Fetch Miscellaneous Voucher Data from tblMisc matching original Crystal Report filter:
    // {tblMisc.VoucherNo} = Val(txtVoucherNo.Text.Trim) and {tblMisc.Type} = SType
    const result = await pool.request()
      .input("Type", sql.VarChar(10), type)
      .input("VoucherNo", sql.Int, vNoInt)
      .query(`
        SELECT 
          RTRIM(LTRIM(Type)) AS Type,
          VoucherNo,
          Date,
          RTRIM(LTRIM(ISNULL(Person, ''))) AS Person,
          ISNULL(Amount, 0) AS Amount,
          ISNULL(GSTPerc, 0) AS GSTPerc,
          ISNULL(GST, 0) AS GST,
          ISNULL(NetAmount, 0) AS NetAmount,
          ISNULL(Weight, 0) AS Weight,
          RTRIM(LTRIM(ISNULL(Remarks, ''))) AS Remarks,
          DateOfTransaction,
          RTRIM(LTRIM(ISNULL(CreateUser, 'sys'))) AS CreateUser,
          CreateDate,
          RTRIM(LTRIM(ISNULL(ModifyUser, ''))) AS ModifyUser,
          ModifyDate
        FROM tblMisc
        WHERE Type = @Type AND VoucherNo = @VoucherNo
      `);

    if (!result.recordset || result.recordset.length === 0) {
      return res.status(404).json({
        success: false,
        message: `Miscellaneous voucher record not found for Type '${type}' and Voucher No '${vNoInt}'.`
      });
    }

    // Type descriptions mapping for report header
    const typeNames = {
      PN: "Purchase Note",
      CN: "Credit Note",
      MC: "Misc. Note",
      PC: "Petty Cash",
      SR: "Services"
    };

    res.json({
      success: true,
      data: {
        company,
        voucher: result.recordset[0],
        typeName: typeNames[type] || type
      }
    });
  } catch (err) {
    console.error("Print Data Misc Error:", err);
    res.status(500).json({ success: false, message: "Failed to fetch print data", error: err.message });
  }
});

// POST - Insert new entry
router.post("/", async (req, res) => {
  let transaction;
  try {
    const pool = await getPool(req);
    const {
      Type,
      VoucherNo,
      Date: dateVal,
      Person,
      Amount,
      GSTPerc,
      GST,
      NetAmount,
      Weight,
      Remarks,
      CreateUser
    } = req.body;

    if (!Type || !VoucherNo || Amount === undefined || Amount === null) {
      return res.status(400).json({ success: false, message: "Type, VoucherNo, and Amount are required fields." });
    }

    // Check if VoucherNo already exists for this Type
    const checkResult = await pool.request()
      .input("Type", sql.VarChar, Type)
      .input("VoucherNo", sql.Int, parseInt(VoucherNo))
      .query("SELECT VoucherNo FROM tblMisc WHERE Type = @Type AND VoucherNo = @VoucherNo");

    if (checkResult.recordset && checkResult.recordset.length > 0) {
      return res.status(400).json({ success: false, message: `Voucher No ${VoucherNo} already exists for type ${Type}.` });
    }

    const user = CreateUser || "sys";
    const formattedDate = dateVal ? new Date(dateVal) : new Date();

    transaction = new sql.Transaction(pool);
    await transaction.begin();

    const insertQuery = `
      INSERT INTO tblMisc (
        Type, VoucherNo, Date, Person, Amount, GSTPerc, GST, NetAmount, Weight, Remarks,
        DateOfTransaction, CreateUser, CreateDate, ModifyUser, ModifyDate
      ) VALUES (
        @Type, @VoucherNo, @Date, @Person, @Amount, @GSTPerc, @GST, @NetAmount, @Weight, @Remarks,
        GETDATE(), @CreateUser, GETDATE(), @CreateUser, GETDATE()
      );

      INSERT INTO tblMiscHistory (
        Type, VoucherNo, Date, Person, Amount, GSTPerc, GST, NetAmount, Weight, Remarks,
        DateOfTransaction, CreateUser, CreateDate, ModifyUser, ModifyDate
      ) VALUES (
        @Type, @VoucherNo, @Date, @Person, @Amount, @GSTPerc, @GST, @NetAmount, @Weight, @Remarks,
        GETDATE(), @CreateUser, GETDATE(), @CreateUser, GETDATE()
      );
    `;

    const request = new sql.Request(transaction);
    await request
      .input("Type", sql.VarChar, Type)
      .input("VoucherNo", sql.Int, parseInt(VoucherNo))
      .input("Date", sql.DateTime, formattedDate)
      .input("Person", sql.VarChar, Person ? String(Person).substring(0, 50) : "")
      .input("Amount", sql.Decimal(18, 4), parseFloat(Amount || 0))
      .input("GSTPerc", sql.Decimal(18, 4), parseFloat(GSTPerc || 0))
      .input("GST", sql.Decimal(18, 4), parseFloat(GST || 0))
      .input("NetAmount", sql.Decimal(18, 4), parseFloat(NetAmount || 0))
      .input("Weight", sql.Decimal(18, 4), parseFloat(Weight || 0))
      .input("Remarks", sql.VarChar, Remarks ? String(Remarks).substring(0, 50) : "")
      .input("CreateUser", sql.VarChar, user)
      .query(insertQuery);

    await transaction.commit();
    res.json({ success: true, message: "Miscellaneous details saved successfully" });
  } catch (err) {
    if (transaction) {
      try { await transaction.rollback(); } catch (rErr) { console.error("Rollback error:", rErr); }
    }
    console.error("Insert Misc Error:", err);
    res.status(500).json({ success: false, message: "Failed to save miscellaneous details", error: err.message });
  }
});

// PUT - Update entry
router.put("/", async (req, res) => {
  let transaction;
  try {
    const pool = await getPool(req);
    const {
      Type,
      VoucherNo,
      Date: dateVal,
      Person,
      Amount,
      GSTPerc,
      GST,
      NetAmount,
      Weight,
      Remarks,
      ModifyUser
    } = req.body;

    if (!Type || !VoucherNo || Amount === undefined || Amount === null) {
      return res.status(400).json({ success: false, message: "Type, VoucherNo, and Amount are required fields." });
    }

    const user = ModifyUser || "sys";
    const formattedDate = dateVal ? new Date(dateVal) : new Date();

    transaction = new sql.Transaction(pool);
    await transaction.begin();

    const updateQuery = `
      UPDATE tblMisc SET
        Date = @Date,
        Person = @Person,
        Amount = @Amount,
        GSTPerc = @GSTPerc,
        GST = @GST,
        NetAmount = @NetAmount,
        Weight = @Weight,
        Remarks = @Remarks,
        ModifyUser = @ModifyUser,
        ModifyDate = GETDATE()
      WHERE Type = @Type AND VoucherNo = @VoucherNo;

      UPDATE tblMiscHistory SET
        Date = @Date,
        Person = @Person,
        Amount = @Amount,
        GSTPerc = @GSTPerc,
        GST = @GST,
        NetAmount = @NetAmount,
        Weight = @Weight,
        Remarks = @Remarks,
        ModifyUser = @ModifyUser,
        ModifyDate = GETDATE()
      WHERE Type = @Type AND VoucherNo = @VoucherNo;
    `;

    const request = new sql.Request(transaction);
    const result = await request
      .input("Type", sql.VarChar, Type)
      .input("VoucherNo", sql.Int, parseInt(VoucherNo))
      .input("Date", sql.DateTime, formattedDate)
      .input("Person", sql.VarChar, Person ? String(Person).substring(0, 50) : "")
      .input("Amount", sql.Decimal(18, 4), parseFloat(Amount || 0))
      .input("GSTPerc", sql.Decimal(18, 4), parseFloat(GSTPerc || 0))
      .input("GST", sql.Decimal(18, 4), parseFloat(GST || 0))
      .input("NetAmount", sql.Decimal(18, 4), parseFloat(NetAmount || 0))
      .input("Weight", sql.Decimal(18, 4), parseFloat(Weight || 0))
      .input("Remarks", sql.VarChar, Remarks ? String(Remarks).substring(0, 50) : "")
      .input("ModifyUser", sql.VarChar, user)
      .query(updateQuery);

    if (result.rowsAffected && result.rowsAffected[0] === 0) {
      await transaction.rollback();
      return res.status(200).json({ success: false, message: "Miscellaneous entry not found for update" });
    }

    await transaction.commit();
    res.json({ success: true, message: "Miscellaneous details updated successfully" });
  } catch (err) {
    if (transaction) {
      try { await transaction.rollback(); } catch (rErr) { console.error("Rollback error:", rErr); }
    }
    console.error("Update Misc Error:", err);
    res.status(500).json({ success: false, message: "Failed to update miscellaneous details", error: err.message });
  }
});

// DELETE - Delete entry
router.delete("/", async (req, res) => {
  let transaction;
  try {
    const pool = await getPool(req);
    const { type, voucherNo } = req.query;

    if (!type || !voucherNo) {
      return res.status(400).json({ success: false, message: "Type and VoucherNo are required" });
    }

    transaction = new sql.Transaction(pool);
    await transaction.begin();

    const deleteQuery = `
      DELETE FROM tblMisc WHERE Type = @Type AND VoucherNo = @VoucherNo;
      DELETE FROM tblMiscHistory WHERE Type = @Type AND VoucherNo = @VoucherNo;
    `;

    const request = new sql.Request(transaction);
    const result = await request
      .input("Type", sql.VarChar, type)
      .input("VoucherNo", sql.Int, parseInt(voucherNo))
      .query(deleteQuery);

    const totalAffected = result.rowsAffected ? result.rowsAffected.reduce((a, b) => a + b, 0) : 0;
    if (totalAffected === 0) {
      await transaction.rollback();
      return res.status(200).json({ success: false, message: "Miscellaneous entry not found to delete" });
    }

    await transaction.commit();
    res.json({ success: true, message: "Miscellaneous details deleted successfully" });
  } catch (err) {
    if (transaction) {
      try { await transaction.rollback(); } catch (rErr) { console.error("Rollback error:", rErr); }
    }
    console.error("Delete Misc Error:", err);
    res.status(500).json({ success: false, message: "Failed to delete miscellaneous details", error: err.message });
  }
});

module.exports = router;

module.exports = router;
