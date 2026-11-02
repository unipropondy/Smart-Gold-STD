/**
 * Developer: durairaj
 */

const express = require("express");
const router = express.Router();
const { sql, poolPromise } = require("../db");

// 1. LOV for Suppliers
// Matches: frmHelpSearch(0, 0, "tblSupplier", "L", 4, "SupplierName", "", "SupplierCode", ...)
router.get("/lov/supplier", async (req, res) => {
  try {
    const pool = req.pool || (await poolPromise);
    let result;
    try {
      result = await pool.request().query(`
        SELECT TOP 300 
          RTRIM(SupplierCode) AS SupplierCode, 
          RTRIM(SupplierName) AS SupplierName, 
          ISNULL(RTRIM(Address1), '') AS Address1, 
          ISNULL(RTRIM(Address2), '') AS Address2
        FROM tblSupplier 
        ORDER BY SupplierCode ASC
      `);
    } catch (colErr) {
      result = await pool.request().query(`
        SELECT TOP 300 
          RTRIM(SupplierCode) AS SupplierCode, 
          RTRIM(SupplierName) AS SupplierName
        FROM tblSupplier 
        ORDER BY SupplierCode ASC
      `);
    }
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("LOV Supplier Error in SupplierPaymentAmendment:", err);
    res.status(500).json({ success: false, message: "Database Error", error: err.message });
  }
});

// 2. Fetch Single Supplier by Code (for LostFocus / Blur check)
router.get("/supplier/:code", async (req, res) => {
  try {
    const { code } = req.params;
    if (!code || !code.trim()) {
      return res.status(400).json({ success: false, message: "SupplierCode is required." });
    }
    const pool = req.pool || (await poolPromise);
    const result = await pool.request()
      .input("SupplierCode", sql.VarChar, code.trim())
      .query(`
        SELECT TOP 1
          RTRIM(SupplierCode) AS SupplierCode,
          RTRIM(SupplierName) AS SupplierName,
          ISNULL(RTRIM(Address1), '') AS Address1,
          ISNULL(RTRIM(Address2), '') AS Address2
        FROM tblSupplier
        WHERE SupplierCode = @SupplierCode
      `);

    if (result.recordset.length > 0) {
      res.json({ success: true, data: result.recordset[0] });
    } else {
      res.json({ success: false, message: "Invalid supplier code!" });
    }
  } catch (err) {
    console.error("Fetch Single Supplier Error in SupplierPaymentAmendment:", err);
    res.status(500).json({ success: false, message: "Database Error", error: err.message });
  }
});

// 3. LOV for Invoices for a selected Supplier
router.get("/lov/invoices", async (req, res) => {
  try {
    const pool = req.pool || (await poolPromise);
    const { SupplierCode } = req.query;

    let query = `
      SELECT DISTINCT TOP 200
        RTRIM(PurchaseBillNo) AS InvoiceNo,
        CONVERT(varchar, PurchaseBillDate, 103) AS InvoiceDate,
        RTRIM(JobSheetNo) AS JobSheetNo,
        ISNULL(PaidAmount, 0) AS PaidAmount,
        ISNULL(BalanceAmount, 0) AS BalanceAmount
      FROM tblJobSheetHeader
      WHERE RTRIM(PurchaseBillNo) <> '' AND PurchaseBillNo IS NOT NULL
    `;

    const request = pool.request();
    if (SupplierCode && SupplierCode.trim()) {
      query += " AND RTRIM(SupplierCode) = @SupplierCode";
      request.input("SupplierCode", sql.VarChar, SupplierCode.trim());
    }
    query += " ORDER BY InvoiceNo DESC";

    const result = await request.query(query);
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("LOV Invoices Error in SupplierPaymentAmendment:", err);
    res.status(500).json({ success: false, message: "Database Error", error: err.message });
  }
});

// 4. Fetch payment details by SupplierCode & InvoiceNo
// Matches: subLoadPaymentDetails() in frmSupplierPaymentAmendment.vb
router.get("/fetch", async (req, res) => {
  try {
    const pool = req.pool || (await poolPromise);
    const { SupplierCode, InvoiceNo } = req.query;

    if (!SupplierCode || !SupplierCode.trim()) {
      return res.status(400).json({ success: false, message: "SupplierCode is required." });
    }
    if (!InvoiceNo || !InvoiceNo.trim()) {
      return res.status(400).json({ success: false, message: "InvoiceNo is required." });
    }

    const query = `
      SELECT 
        RTRIM(PaymentNo) AS PaymentNo, 
        CASE 
          WHEN ISDATE(PaymentDate) = 1 THEN CONVERT(varchar, CAST(PaymentDate AS datetime), 103)
          ELSE RTRIM(PaymentDate)
        END AS PaymentDate,
        RTRIM(Paymode) AS Paymode, 
        ISNULL(PaymentAmount, 0) AS PaymentAmount, 
        ISNULL(RTRIM(Remarks), '') AS Remarks 
      FROM tbljobsheetpaymentdetail
      WHERE (RTRIM(VendorCode) = @SupplierCode OR RTRIM(VendorCode) = @SupplierCode)
        AND RTRIM(InvoiceNo) = @InvoiceNo
      ORDER BY PaymentNo DESC
    `;

    const result = await pool.request()
      .input("SupplierCode", sql.VarChar, SupplierCode.trim())
      .input("InvoiceNo", sql.VarChar, InvoiceNo.trim())
      .query(query);

    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Supplier Payment Amendment Error:", err);
    res.status(500).json({ success: false, message: err.message || "Server error", error: err.message });
  }
});

// 5. Update payment details (Remarks / details)
// Matches: cmdUpdate_Click in frmSupplierPaymentAmendment.vb
router.put("/update", async (req, res) => {
  try {
    const pool = req.pool || (await poolPromise);
    const { PaymentNo, Remarks, UserID } = req.body;
    const sysUser = UserID || "sys";

    if (!PaymentNo || !PaymentNo.trim()) {
      return res.status(400).json({ success: false, message: "PaymentNo is required." });
    }
    if (!Remarks || !Remarks.trim()) {
      return res.status(400).json({ success: false, message: "Please Enter the Details" });
    }

    // Check if table has ModifyUser / ModifyDate columns
    let colNames = [];
    try {
      const colRes = await pool.request().query(`
        SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE LOWER(TABLE_NAME) = 'tbljobsheetpaymentdetail'
      `);
      colNames = colRes.recordset.map(c => c.COLUMN_NAME.toLowerCase());
    } catch (e) {}

    let updateQuery = "UPDATE tbljobsheetpaymentdetail SET Remarks = @Remarks";
    const request = pool.request()
      .input("PaymentNo", sql.VarChar, PaymentNo.trim())
      .input("Remarks", sql.VarChar, Remarks.trim().substring(0, 50));

    if (colNames.includes('modifyuser')) {
      updateQuery += ", ModifyUser = @ModifyUser";
      request.input("ModifyUser", sql.VarChar, sysUser);
    }
    if (colNames.includes('modifydate')) {
      updateQuery += ", ModifyDate = GETDATE()";
    }

    updateQuery += " WHERE RTRIM(PaymentNo) = @PaymentNo";

    await request.query(updateQuery);

    res.json({ success: true, message: "Payment Details Update" });
  } catch (err) {
    console.error("Update Supplier Payment Amendment Error:", err);
    res.status(500).json({ success: false, message: err.message || "Server error", error: err.message });
  }
});

// 6. Fetch Payment Voucher for Reprint
// Matches: subPrint() in frmSupplierPaymentAmendment.vb
router.get("/voucher/:invoiceNo", async (req, res) => {
  try {
    const { invoiceNo } = req.params;
    const { SupplierCode } = req.query;
    const pool = req.pool || (await poolPromise);

    let query = `
      SELECT 
        RTRIM(p.PaymentNo) AS PaymentNo,
        p.PaymentDate,
        p.PaymentAmount,
        ISNULL(RTRIM(p.Remarks), '') AS Remarks,
        RTRIM(p.InvoiceNo) AS InvoiceNo,
        RTRIM(p.Paymode) AS Paymode,
        ISNULL(RTRIM(s.SupplierName), '') AS SupplierName,
        ISNULL(RTRIM(s.SupplierCode), '') AS SupplierCode
      FROM tbljobsheetpaymentdetail p
      LEFT JOIN tblSupplier s ON RTRIM(p.VendorCode) = RTRIM(s.SupplierCode)
      WHERE RTRIM(p.InvoiceNo) = @InvoiceNo
    `;

    const request = pool.request().input("InvoiceNo", sql.VarChar, invoiceNo.trim());
    if (SupplierCode && SupplierCode.trim()) {
      query += " AND RTRIM(p.VendorCode) = @SupplierCode";
      request.input("SupplierCode", sql.VarChar, SupplierCode.trim());
    }

    query += " ORDER BY p.CreateDate DESC";

    const result = await request.query(query);
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Payment Voucher Error:", err);
    res.status(500).json({ success: false, message: "Database Error", error: err.message });
  }
});

module.exports = router;
