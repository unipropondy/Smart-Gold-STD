/**
 * Developer: durairaj
 */

const express = require("express");
const router = express.Router();
const { sql, poolPromise } = require("../db");

// Safe Date Parsing Helper to prevent Invalid Date crashes with node-mssql
function parseSafeDate(val) {
  if (!val) return new Date();
  if (val instanceof Date) return isNaN(val.getTime()) ? new Date() : val;
  const s = String(val).trim();
  if (s.includes('/')) {
    const parts = s.split('/');
    if (parts.length === 3) {
      const d = parseInt(parts[0], 10);
      const m = parseInt(parts[1], 10) - 1;
      const y = parseInt(parts[2], 10);
      const dt = new Date(y, m, d);
      if (!isNaN(dt.getTime())) return dt;
    }
  } else if (s.includes('-')) {
    const parts = s.split('-');
    if (parts.length === 3) {
      if (parts[0].length === 4) {
        // YYYY-MM-DD
        const dt = new Date(parseInt(parts[0], 10), parseInt(parts[1], 10) - 1, parseInt(parts[2], 10));
        if (!isNaN(dt.getTime())) return dt;
      } else {
        // DD-MM-YYYY
        const dt = new Date(parseInt(parts[2], 10), parseInt(parts[1], 10) - 1, parseInt(parts[0], 10));
        if (!isNaN(dt.getTime())) return dt;
      }
    }
  } else if (/^\d{8}$/.test(s)) {
    // YYYYMMDD
    const y = parseInt(s.substring(0, 4), 10);
    const m = parseInt(s.substring(4, 6), 10) - 1;
    const d = parseInt(s.substring(6, 8), 10);
    const dt = new Date(y, m, d);
    if (!isNaN(dt.getTime())) return dt;
  }
  const dt = new Date(s);
  return isNaN(dt.getTime()) ? new Date() : dt;
}

// 1. Get next Supplier Payment Number
// Matches: objTran.fncGetSupplierPaymentNo() from tblCompany
router.get("/nextNo", async (req, res) => {
  try {
    const pool = req.pool || (await poolPromise);
    const result = await pool.request().query(`
      SELECT LocationCode, NextSupplierPayment
      FROM tblCompany
    `);

    if (result.recordset.length > 0) {
      const company = result.recordset[0];
      const loc = (company.LocationCode || "HQ").trim();
      const nextNo = String(company.NextSupplierPayment || 1).padStart(8, "0");
      res.json({ success: true, nextNo: `${loc}${nextNo}` });
    } else {
      res.json({ success: true, nextNo: "HQ00000001" });
    }
  } catch (err) {
    console.error("Get next Supplier Payment No Error:", err);
    res.status(500).json({ success: false, message: "Database Error", error: err.message });
  }
});

// 2. LOV for Suppliers
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
    console.error("LOV Supplier Error:", err);
    res.status(500).json({ success: false, message: "Database Error", error: err.message });
  }
});

// 3. Verify / Fetch Single Supplier Details
// Matches: subGetSupplier() in frmSupplierPayment.vb
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
    console.error("Fetch Single Supplier Error:", err);
    res.status(500).json({ success: false, message: "Database Error", error: err.message });
  }
});

// 4. LOV for GoldBar Issue
// Matches: frmHelpSearch(0, 0, "tblGoldBarIssued", "L", 4, "IssueNo", "", "IssueNo", ...)
router.get("/lov/goldbarissue", async (req, res) => {
  try {
    const pool = req.pool || (await poolPromise);
    const { SupplierCode } = req.query;

    let query = `
      SELECT TOP 200 
        RTRIM(IssueNo) AS IssueNo, 
        CONVERT(varchar, PDate, 103) AS PDate, 
        RTRIM(SupplierCode) AS SupplierCode, 
        IssuedWeight 
      FROM tblGoldBarIssued
    `;
    const request = pool.request();
    if (SupplierCode && SupplierCode.trim()) {
      query += " WHERE RTRIM(SupplierCode) = @SupplierCode";
      request.input("SupplierCode", sql.VarChar, SupplierCode.trim());
    }
    query += " ORDER BY IssueNo DESC";

    let result = await request.query(query);

    if (result.recordset.length === 0 && SupplierCode) {
      result = await pool.request().query(`
        SELECT TOP 200 
          RTRIM(IssueNo) AS IssueNo, 
          CONVERT(varchar, PDate, 103) AS PDate, 
          RTRIM(SupplierCode) AS SupplierCode, 
          IssuedWeight 
        FROM tblGoldBarIssued
        ORDER BY IssueNo DESC
      `);
    }

    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch GoldBar Issue LOV Error:", err);
    res.status(500).json({ success: false, message: "Database Error", error: err.message });
  }
});

// 5. LOV for Trade-In Issue
// Matches: frmHelpSearch(0, 0, "tblTradeissuehistoryHeader", "L", 4, "TradeIssueNo", "", ...)
router.get("/lov/tradeinissue", async (req, res) => {
  try {
    const pool = req.pool || (await poolPromise);
    const { SupplierCode } = req.query;

    let query = `
      SELECT TOP 200 
        RTRIM(TradeIssueNo) AS TradeIssueNo, 
        CONVERT(varchar, TradeIssueDate, 103) AS TradeIssueDate, 
        RTRIM(SupplierCode) AS SupplierCode, 
        TotalWeight 
      FROM tblTradeissuehistoryHeader
    `;
    const request = pool.request();
    if (SupplierCode && SupplierCode.trim()) {
      query += " WHERE RTRIM(SupplierCode) = @SupplierCode";
      request.input("SupplierCode", sql.VarChar, SupplierCode.trim());
    }
    query += " ORDER BY TradeIssueNo DESC";

    let result = await request.query(query);

    if (result.recordset.length === 0 && SupplierCode) {
      result = await pool.request().query(`
        SELECT TOP 200 
          RTRIM(TradeIssueNo) AS TradeIssueNo, 
          CONVERT(varchar, TradeIssueDate, 103) AS TradeIssueDate, 
          RTRIM(SupplierCode) AS SupplierCode, 
          TotalWeight 
        FROM tblTradeissuehistoryHeader
        ORDER BY TradeIssueNo DESC
      `);
    }

    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Trade-In Issue LOV Error:", err);
    res.status(500).json({ success: false, message: "Database Error", error: err.message });
  }
});

// 6. Fetch Issue Detail for Gold Bar / Trade-In
// Matches: subGetIssueDetail() & subGetTradeInIssueDetail() in frmSupplierPayment.vb
router.get("/issueDetail", async (req, res) => {
  try {
    const pool = req.pool || (await poolPromise);
    const { Type, IssueNo } = req.query;

    if (!Type || !IssueNo || !IssueNo.trim()) {
      return res.status(400).json({ success: false, message: "Type and IssueNo are required." });
    }

    const trimmedIssueNo = IssueNo.trim();

    if (Type.toUpperCase() === "GOLD BAR") {
      const result = await pool.request()
        .input("IssueNo", sql.VarChar, trimmedIssueNo)
        .query(`
          SELECT TOP 1 IssuedWeight 
          FROM tblGoldBarIssued 
          WHERE RTRIM(IssueNo) = @IssueNo
        `);
      
      if (result.recordset.length > 0) {
        const wt = parseFloat(result.recordset[0].IssuedWeight) || 0;
        return res.json({ success: true, data: { Remarks: wt.toString() } });
      }
    } else if (Type.toUpperCase() === "TRADE-IN") {
      const result = await pool.request()
        .input("IssueNo", sql.VarChar, trimmedIssueNo)
        .query(`
          SELECT TOP 1 TotalWeight, Purity, TotalPrice 
          FROM tblTradeissuehistoryHeader 
          WHERE RTRIM(TradeIssueNo) = @IssueNo
        `);
      
      if (result.recordset.length > 0) {
        const row = result.recordset[0];
        const wt = parseFloat(row.TotalWeight) || 0;
        const purity = (row.Purity || "").trim();
        const price = parseFloat(row.TotalPrice) || 0;
        return res.json({ 
          success: true, 
          data: { 
            Remarks: `${wt} Purity ${purity}`.trim(),
            Amount: price
          } 
        });
      }
    }
    res.json({ success: false, message: "Issue details not found." });
  } catch (err) {
    console.error("Fetch Issue details error:", err);
    res.status(500).json({ success: false, message: "Database Error", error: err.message });
  }
});

// 7. Fetch jobsheets list matching filter criteria
// Matches: subLoadJobSheet() in frmSupplierPayment.vb
router.get("/fetch", async (req, res) => {
  try {
    const pool = req.pool || (await poolPromise);
    const { SupplierCode, FromDate, ToDate, PaidStatus, InvoiceNo } = req.query;

    if (!SupplierCode || !SupplierCode.trim()) {
      return res.status(400).json({ success: false, message: "SupplierCode is required." });
    }

    let query = `
      SELECT 
        RTRIM(JobSheetNo) AS JobSheetNo,
        RTRIM(PurchaseBillNo) AS PurchaseBillNo,
        CONVERT(varchar, PurchaseBillDate, 103) AS PurchaseBillDate,
        (ISNULL(TotalCost, 0) + ISNULL(GST, 0) + ISNULL(ManualAdjustment, 0)) AS TotalCost,
        ISNULL(PaidAmount, 0) AS PaidAmount,
        ISNULL(BalanceAmount, 0) AS BalanceAmount,
        CASE 
          WHEN (ISNULL(TotalCost, 0) + ISNULL(GST, 0) + ISNULL(ManualAdjustment, 0)) = ISNULL(PaidAmount, 0) AND ISNULL(PaidAmount, 0) > 0 THEN 'PAID'
          WHEN ISNULL(PaidAmount, 0) > 0 AND (ISNULL(TotalCost, 0) + ISNULL(GST, 0) + ISNULL(ManualAdjustment, 0)) <> ISNULL(PaidAmount, 0) THEN 'PARTIAL'
          ELSE 'UNPAID'
        END AS Status,
        ISNULL(RTRIM(Remarks), '') AS Remarks 
      FROM tblJobSheetHeader
      WHERE RTRIM(SupplierCode) = @SupplierCode
    `;

    const request = pool.request().input("SupplierCode", sql.VarChar, SupplierCode.trim());

    if (FromDate && FromDate.trim()) {
      query += " AND CONVERT(CHAR(10), JobSheetDate, 112) >= @FromDate";
      request.input("FromDate", sql.VarChar, FromDate.trim());
    }
    if (ToDate && ToDate.trim()) {
      query += " AND CONVERT(CHAR(10), JobSheetDate, 112) <= @ToDate";
      request.input("ToDate", sql.VarChar, ToDate.trim());
    }

    if (PaidStatus === "Paid") {
      query += " AND ISNULL(BalanceAmount, 0) = 0";
    } else if (PaidStatus === "UnPaid") {
      query += " AND ISNULL(BalanceAmount, 0) > 0";
    }

    if (InvoiceNo && InvoiceNo.trim()) {
      query += " AND RTRIM(PurchaseBillNo) LIKE @InvoiceNo";
      request.input("InvoiceNo", sql.VarChar, `%${InvoiceNo.trim()}%`);
    }

    query += " ORDER BY JobSheetNo DESC";

    const result = await request.query(query);
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Jobsheets error for supplier payment:", err);
    res.status(500).json({ success: false, message: "Database Error", error: err.message });
  }
});

// 8. Add payment transaction
// Matches: cmdUpdate_Click in frmSupplierPayment.vb
router.post("/", async (req, res) => {
  const pool = req.pool || (await poolPromise);
  const transaction = new sql.Transaction(pool);
  try {
    const {
      PaymentNo, PaymentDate, PaymentAmount, Remarks, InvoiceNo,
      InvoiceDate, SupplierCode, Paymode, UserID, JobSheetNo, Bank
    } = req.body;

    const sysUser = UserID || "sys";
    const valAmt = parseFloat(PaymentAmount) || 0;

    // Field Validations matching VB.NET
    if (Paymode === "CHEQUE" && (!Remarks || !Remarks.trim())) {
      return res.status(400).json({ success: false, message: "Please Enter the Cheque No" });
    }
    if (Paymode === "GOLD BAR" && (!Remarks || !Remarks.trim())) {
      return res.status(400).json({ success: false, message: "Please Enter the Bar Weight" });
    }
    if (Paymode === "TRADE-IN" && (!Remarks || !Remarks.trim())) {
      return res.status(400).json({ success: false, message: "Please Enter the TradeIn Weight" });
    }
    if (!Paymode || !Paymode.trim()) {
      return res.status(400).json({ success: false, message: "Please Enter the Paymode" });
    }
    if (valAmt <= 0) {
      return res.status(400).json({ success: false, message: "Please Enter the Amount" });
    }
    if (!InvoiceNo || !InvoiceNo.trim()) {
      return res.status(400).json({ success: false, message: "Please Select The Invoice details to update" });
    }

    await transaction.begin();

    // Dynamically check columns and data types of tbljobsheetpaymentdetail
    let colMap = {};
    try {
      const colRes = await new sql.Request(transaction).query(`
        SELECT COLUMN_NAME, DATA_TYPE 
        FROM INFORMATION_SCHEMA.COLUMNS 
        WHERE LOWER(TABLE_NAME) = 'tbljobsheetpaymentdetail'
      `);
      colRes.recordset.forEach(c => {
        colMap[c.COLUMN_NAME.toLowerCase()] = (c.DATA_TYPE || '').toLowerCase();
      });
    } catch (e) {
      console.warn("Could not query tbljobsheetpaymentdetail columns:", e.message);
    }

    const hasCols = Object.keys(colMap).length > 0;
    const hasCol = (name) => !hasCols || !!colMap[name.toLowerCase()];
    const getColType = (name) => colMap[name.toLowerCase()] || '';

    // Check if PaymentNo already exists to avoid duplicate key violations
    let actualPaymentNo = (PaymentNo || "").trim();
    if (!actualPaymentNo) {
      const prefixResult = await new sql.Request(transaction).query(`
        SELECT LocationCode, NextSupplierPayment FROM tblCompany
      `);
      const comp = prefixResult.recordset[0] || {};
      const loc = (comp.LocationCode || "HQ").trim();
      const nextNo = parseInt(comp.NextSupplierPayment) || 1;
      actualPaymentNo = `${loc}${String(nextNo).padStart(8, "0")}`;
    } else {
      const dupCheck = await new sql.Request(transaction)
        .input("CheckPNo", sql.VarChar, actualPaymentNo)
        .query(`SELECT TOP 1 PaymentNo FROM tbljobsheetpaymentdetail WHERE RTRIM(PaymentNo) = @CheckPNo`);
      
      if (dupCheck.recordset.length > 0) {
        // Generate fresh payment number
        const prefixResult = await new sql.Request(transaction).query(`
          SELECT LocationCode, NextSupplierPayment FROM tblCompany
        `);
        const comp = prefixResult.recordset[0] || {};
        const loc = (comp.LocationCode || "HQ").trim();
        const nextNo = (parseInt(comp.NextSupplierPayment) || 1) + 1;
        actualPaymentNo = `${loc}${String(nextNo).padStart(8, "0")}`;
      }
    }

    // Construct Remarks according to Paymode matching VB
    let finalRemarks = (Remarks || "").trim();
    if (Paymode === "CHEQUE") {
      finalRemarks = `${(Bank || "").trim()} ${finalRemarks}`.trim();
    }

    // 1. Insert into tbljobsheetpaymentdetail
    const req1 = new sql.Request(transaction);
    const insertCols = [];
    const insertVals = [];

    insertCols.push('PaymentNo'); insertVals.push('@PaymentNo');
    req1.input("PaymentNo", sql.VarChar, actualPaymentNo);

    insertCols.push('PaymentDate'); insertVals.push('@PaymentDate');
    const pDateType = getColType('PaymentDate');
    if (['datetime', 'smalldatetime', 'date'].includes(pDateType)) {
      req1.input("PaymentDate", sql.SmallDateTime, parseSafeDate(PaymentDate));
    } else {
      req1.input("PaymentDate", sql.VarChar, PaymentDate ? String(PaymentDate).replace(/-/g, '') : "");
    }

    insertCols.push('PaymentAmount'); insertVals.push('@PaymentAmount');
    req1.input("PaymentAmount", sql.Numeric(18, 2), valAmt);

    insertCols.push('Remarks'); insertVals.push('@Remarks');
    req1.input("Remarks", sql.VarChar, finalRemarks.substring(0, 50));

    insertCols.push('InvoiceNo'); insertVals.push('@InvoiceNo');
    req1.input("InvoiceNo", sql.VarChar, InvoiceNo.trim());

    if (hasCol('InvoiceDate')) {
      insertCols.push('InvoiceDate'); insertVals.push('@InvoiceDate');
      const invDateType = getColType('InvoiceDate');
      if (['datetime', 'smalldatetime', 'date'].includes(invDateType) || !hasCols) {
        req1.input("InvoiceDate", sql.SmallDateTime, parseSafeDate(InvoiceDate));
      } else {
        req1.input("InvoiceDate", sql.VarChar, String(InvoiceDate || ''));
      }
    }

    if (hasCol('VendorCode')) {
      insertCols.push('VendorCode'); insertVals.push('@VendorCode');
      req1.input("VendorCode", sql.VarChar, (SupplierCode || "").trim());
    } else if (hasCol('SupplierCode')) {
      insertCols.push('SupplierCode'); insertVals.push('@SupplierCode');
      req1.input("SupplierCode", sql.VarChar, (SupplierCode || "").trim());
    }

    insertCols.push('Paymode'); insertVals.push('@Paymode');
    req1.input("Paymode", sql.VarChar, Paymode.trim());

    if (hasCol('CreateUser')) {
      insertCols.push('CreateUser'); insertVals.push('@CreateUser');
      req1.input("CreateUser", sql.VarChar, sysUser);
    }
    if (hasCol('CreateDate')) {
      insertCols.push('CreateDate'); insertVals.push('GETDATE()');
    }
    if (hasCol('ModifyUser')) {
      insertCols.push('ModifyUser'); insertVals.push('@ModifyUser');
      req1.input("ModifyUser", sql.VarChar, sysUser);
    }
    if (hasCol('ModifyDate')) {
      insertCols.push('ModifyDate'); insertVals.push('GETDATE()');
    }

    await req1.query(`
      INSERT INTO tbljobsheetpaymentdetail (${insertCols.join(', ')})
      VALUES (${insertVals.join(', ')})
    `);

    // 2. Update tblJobSheetHeader PaidAmount and BalanceAmount
    // Matches VB.NET: Update tblJobSheetHeader set PaidAmount=PaidAMount + Val(txtAmount), BalanceAmount=BalanceAmount - Val(txtAmount) Where Jobsheetno = txtJobSheetNo
    // Check if PaidAmount and BalanceAmount columns exist in tblJobSheetHeader
    let jsColMap = {};
    try {
      const jsColRes = await new sql.Request(transaction).query(`
        SELECT COLUMN_NAME 
        FROM INFORMATION_SCHEMA.COLUMNS 
        WHERE LOWER(TABLE_NAME) = 'tbljobsheetheader'
      `);
      jsColRes.recordset.forEach(c => {
        jsColMap[c.COLUMN_NAME.toLowerCase()] = true;
      });
    } catch (e) {}

    const hasJsCols = Object.keys(jsColMap).length > 0;
    const hasPaidAmount = !hasJsCols || !!jsColMap['paidamount'];
    const hasBalanceAmount = !hasJsCols || !!jsColMap['balanceamount'];

    if (hasPaidAmount || hasBalanceAmount) {
      const req2 = new sql.Request(transaction);
      req2.input("PaymentAmount", sql.Numeric(18, 2), valAmt);

      const setClauses = [];
      if (hasPaidAmount) setClauses.push("PaidAmount = ISNULL(PaidAmount, 0) + @PaymentAmount");
      if (hasBalanceAmount) setClauses.push("BalanceAmount = ISNULL(BalanceAmount, 0) - @PaymentAmount");

      let whereClause = "";
      if (JobSheetNo && JobSheetNo.trim()) {
        req2.input("JobSheetNo", sql.VarChar, JobSheetNo.trim());
        whereClause = "WHERE RTRIM(JobSheetNo) = @JobSheetNo";
      } else {
        req2.input("InvoiceNo", sql.VarChar, InvoiceNo.trim());
        req2.input("SupplierCode", sql.VarChar, (SupplierCode || "").trim());
        whereClause = "WHERE RTRIM(PurchaseBillNo) = @InvoiceNo AND RTRIM(SupplierCode) = @SupplierCode";
      }

      await req2.query(`
        UPDATE tblJobSheetHeader 
        SET ${setClauses.join(', ')}
        ${whereClause}
      `);
    }

    // 3. Increment NextSupplierPayment in tblCompany
    try {
      const req3 = new sql.Request(transaction);
      await req3.query(`
        UPDATE tblCompany 
        SET NextSupplierPayment = ISNULL(NextSupplierPayment, 1) + 1
      `);
    } catch (compErr) {
      console.warn("Could not increment NextSupplierPayment in tblCompany:", compErr.message);
    }

    await transaction.commit();
    res.json({ success: true, message: "Payment Successful", paymentNo: actualPaymentNo });
  } catch (err) {
    console.error("Save Supplier Payment Error:", err);
    try {
      await transaction.rollback();
    } catch (ex) {}
    res.status(500).json({ 
      success: false, 
      message: err.message || "Transaction failed", 
      error: err.message || String(err)
    });
  }
});

// 9. Fetch Payment History / Voucher for Reprint
// Matches: subPrint() in frmSupplierPayment.vb
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
        p.InvoiceDate,
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
