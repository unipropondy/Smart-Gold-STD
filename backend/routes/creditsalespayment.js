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

// Helper to pad voucher numbers
function padZero(num, size) {
  let s = num + "";
  while (s.length < size) s = "0" + s;
  return s;
}

// 1. GET next payment number
// Matches: objTran.fncGetCreditSalesPaymentNo()
router.get("/next-number", async (req, res) => {
  try {
    const pool = await getPool(req);
    const result = await pool.request().query(
      "SELECT TOP 1 LocationCode, NextCreditSalesPaymentNo FROM tblCompany"
    );

    if (result.recordset.length > 0) {
      const loc = (result.recordset[0].LocationCode || "HQ").trim();
      const nextNo = parseInt(result.recordset[0].NextCreditSalesPaymentNo) || 1;
      const formattedNo = `${loc}08${padZero(nextNo, 6)}`;
      res.json({ success: true, paymentNo: formattedNo });
    } else {
      res.json({ success: true, paymentNo: "HQ08000001" });
    }
  } catch (err) {
    console.error("Fetch Next Credit Sales Payment Number Error:", err);
    res.status(500).json({ success: false, message: "Failed to generate next payment number", error: err.message });
  }
});

// 2. GET invoices based on filters
// Matches subLoadInvoice() in frmCreditSalesPayment.vb:
// Select InvoiceNo, InvoiceDate, CustomerCode, CustomerName, NetTotal, PaidAmt, BalanceAmt, Remarks from tblCreditSalesHeader
// where InvoiceDate >= sFromDate and InvoiceDate <= sToDate
// If optPaid.Checked = True Then BalanceAmt=0 ElseIf optUnPaid.Checked = True Then BalanceAmt>0
// If CustomerCode is specified then CustomerCode = @customerCode
router.get("/invoices", async (req, res) => {
  const { fromDate, toDate, status, customerCode } = req.query;

  if (!fromDate || !toDate) {
    return res.status(400).json({ success: false, message: "From Date and To Date are required" });
  }

  try {
    const pool = await getPool(req);

    // Clean date strings (support YYYYMMDD or ISO/YYYY-MM-DD)
    const cleanFrom = fromDate.replace(/[-/]/g, "").substring(0, 8);
    const cleanTo = toDate.replace(/[-/]/g, "").substring(0, 8);

    const fYear = cleanFrom.substring(0, 4);
    const fMonth = cleanFrom.substring(4, 6);
    const fDay = cleanFrom.substring(6, 8);
    const formattedFromDate = `${fYear}-${fMonth}-${fDay} 00:00:00`;

    const tYear = cleanTo.substring(0, 4);
    const tMonth = cleanTo.substring(4, 6);
    const tDay = cleanTo.substring(6, 8);
    const formattedToDate = `${tYear}-${tMonth}-${tDay} 23:59:59`;

    let queryStr = `
      SELECT 
        InvoiceNo, 
        InvoiceDate, 
        CustomerCode, 
        CustomerName, 
        NetTotal, 
        PaidAmt, 
        BalanceAmt, 
        Remarks 
      FROM tblCreditSalesHeader 
      WHERE 1=1
    `;

    const request = pool.request();

    // In VB: If Trim(txtCustomerCode.Text) <= "" Then ... (which was a VB typo meaning only when empty did it filter; but if a customer code is provided and we want all invoices or customer-specific invoices):
    // In frmCreditSalesPayment.vb:
    // sSql = "Select InvoiceNo,InvoiceDate,CustomerCode,CustomerName,NetTotal,PaidAmt,BalanceAmt,Remarks from tblCreditSalesHeader"
    // sSql = sSql & " where  InvoiceDate >= " & fncFormatSQLText(sFromDate)
    // sSql = sSql & " and  InvoiceDate <= " & fncFormatSQLText(sToDate)
    // If optPaid.Checked = True Then sSql = sSql & " and  BalanceAmt=0"
    // ElseIf optUnPaid.Checked = True Then sSql = sSql & " and  BalanceAmt>0"
    // And in VB screenshot 2, when customer "+335313104464" was typed, it loaded invoices for TGL, AURIEL, PTC, SRY (i.e. ALL customers!)
    // If customerCode is passed and NOT explicitly requested to filter by it (or if it doesn't match), the user expects to see all eligible credit invoices for the date range, just like in VB!
    
    // Date range filter
    if (cleanFrom && cleanTo) {
      queryStr += ` AND (
        (CONVERT(VARCHAR(8), InvoiceDate, 112) >= @cleanFrom AND CONVERT(VARCHAR(8), InvoiceDate, 112) <= @cleanTo)
        OR (InvoiceDate >= @fromDate AND InvoiceDate <= @toDate)
        OR (CAST(InvoiceDate AS DATE) >= CAST(@formattedFromDate AS DATE) AND CAST(InvoiceDate AS DATE) <= CAST(@formattedToDate AS DATE))
        OR (CONVERT(VARCHAR(10), InvoiceDate, 120) >= @formattedFromDate AND CONVERT(VARCHAR(10), InvoiceDate, 120) <= @formattedToDate)
      )`;
      request.input("fromDate", sql.SmallDateTime, formattedFromDate);
      request.input("toDate", sql.SmallDateTime, formattedToDate);
      request.input("formattedFromDate", sql.VarChar, `${fYear}-${fMonth}-${fDay}`);
      request.input("formattedToDate", sql.VarChar, `${tYear}-${tMonth}-${tDay}`);
      request.input("cleanFrom", sql.VarChar, cleanFrom);
      request.input("cleanTo", sql.VarChar, cleanTo);
    }

    if (status === "PAID") {
      queryStr += " AND BalanceAmt = 0";
    } else if (status === "UNPAID") {
      queryStr += " AND BalanceAmt > 0";
    }

    // Notice: In the VB.NET frmCreditSalesPayment.vb:
    // Line 44: If Trim(txtCustomerCode.Text) <= "" Then sSql = sSql & " and CustomerCode = " & fncFormatSQLText(txtCustomerCode.Text)
    // Because of this line in VB, selecting a Customer Code in the search bar NEVER filtered out invoices for other customers!
    // In screenshot 2, the Customer Code is "+335313104464" (RAVIKUMAR VELAYUTHAM), but the invoices loaded are for TGL, AURIEL, PTC, SRY.
    // Therefore, do NOT restrict by CustomerCode unless exact match exists or if customer filter is explicitly requested.
    // If customerCode is provided, we can check if any invoices exist for that customer; if not, load all credit invoices in that date range!
    if (customerCode && customerCode.trim() !== "") {
      const custTrim = customerCode.trim();
      // Only filter by customer if there are actually invoices matching that customer code
      const custCheck = await pool.request()
        .input("cCode", sql.VarChar, custTrim)
        .query("SELECT COUNT(*) AS cnt FROM tblCreditSalesHeader WHERE CustomerCode = @cCode");
      if (custCheck.recordset && custCheck.recordset[0].cnt > 0) {
        queryStr += " AND CustomerCode = @customerCode";
        request.input("customerCode", sql.VarChar, custTrim);
      }
    }

    queryStr += " ORDER BY InvoiceDate DESC, InvoiceNo DESC";

    const result = await request.query(queryStr);
    res.json({ success: true, data: result.recordset || [] });
  } catch (err) {
    console.error("Fetch Credit Invoices Error:", err);
    res.status(500).json({ success: false, message: "Failed to fetch invoices", error: err.message });
  }
});

// Helper for date parsing into Date object or ISO
function parseDateParam(val) {
  if (!val) return new Date();
  if (val instanceof Date) return isNaN(val.getTime()) ? new Date() : val;
  const s = String(val).trim();
  if (/^\d{8}$/.test(s)) {
    const y = parseInt(s.substring(0, 4), 10);
    const m = parseInt(s.substring(4, 6), 10) - 1;
    const d = parseInt(s.substring(6, 8), 10);
    return new Date(y, m, d);
  }
  const parsed = new Date(s);
  return isNaN(parsed.getTime()) ? new Date() : parsed;
}

// 3. POST /save make payment (with SQL transaction) matching cmdUpdate_Click in frmCreditSalesPayment.vb
router.post("/save", async (req, res) => {
  const {
    PaymentNo,
    PaymentDate,
    Amount,
    Remarks,
    InvoiceNo,
    InvoiceDate,
    CustCode,
    Paymode,
    CreateUser
  } = req.body;

  if (!PaymentNo || !InvoiceNo || !Amount || !Paymode) {
    return res.status(400).json({ success: false, message: "Missing required payment details" });
  }

  const payAmt = parseFloat(Amount);
  if (isNaN(payAmt) || payAmt <= 0) {
    return res.status(400).json({ success: false, message: "Please Enter a valid Amount greater than zero" });
  }

  if (String(Paymode).trim().toUpperCase() === "CHEQUE" && (!Remarks || !Remarks.trim())) {
    return res.status(400).json({ success: false, message: "Please Enter the Remarks (Cheque Details)" });
  }

  const pool = await getPool(req);
  const transaction = new sql.Transaction(pool);

  try {
    await transaction.begin();

    // Fetch the invoice header to validate total and balance
    const checkReq = new sql.Request(transaction);
    checkReq.input("InvoiceNo", sql.VarChar, InvoiceNo.trim());
    const checkRes = await checkReq.query(
      "SELECT NetTotal, PaidAmt, BalanceAmt FROM tblCreditSalesHeader WHERE InvoiceNo = @InvoiceNo"
    );

    if (!checkRes.recordset || checkRes.recordset.length === 0) {
      throw new Error(`Invoice ${InvoiceNo} not found`);
    }

    const { NetTotal, PaidAmt } = checkRes.recordset[0];
    const totalVal = parseFloat(NetTotal) || 0;
    const currentPaid = parseFloat(PaidAmt) || 0;

    if (payAmt + currentPaid > totalVal) {
      throw new Error("Please Check the Amount, Total Amount Greater than Paid Amount");
    }

    const parsedPaymentDate = parseDateParam(PaymentDate);
    const parsedInvoiceDate = parseDateParam(InvoiceDate);
    const sysUser = (CreateUser || "pos").substring(0, 10);

    // Insert payment detail matching:
    // INSERT into tblCreditSalespaymentdetail Values (txtPaymentNo, sPaymentDate, Val(txtAmount), txtRemarks, txtInvoiceNo, txtInvoiceDate, txtCustCode, cboPaymode, sUserID, Getdate(), sUserID, Getdate())
    const insertReq = new sql.Request(transaction);
    insertReq.input("PaymentNo", sql.VarChar, PaymentNo.trim());
    insertReq.input("PaymentDate", sql.DateTime, parsedPaymentDate);
    insertReq.input("PaymentAmount", sql.Decimal(18, 4), payAmt);
    insertReq.input("Remarks", sql.VarChar, Remarks || "");
    insertReq.input("InvoiceNo", sql.VarChar, InvoiceNo.trim());
    insertReq.input("InvoiceDate", sql.DateTime, parsedInvoiceDate);
    insertReq.input("CustCode", sql.VarChar, CustCode ? CustCode.trim() : "");
    insertReq.input("Paymode", sql.VarChar, Paymode.trim());
    insertReq.input("CreateUser", sql.VarChar, sysUser);

    await insertReq.query(`
      INSERT INTO tblCreditSalespaymentdetail (
        PaymentNo, PaymentDate, PaymentAmount, Remarks, Invoiceno, InvoiceDate, CustCode, Paymode, CreateUser, CreateDate, ModifyUser, ModifyDate
      ) VALUES (
        @PaymentNo, @PaymentDate, @PaymentAmount, @Remarks, @InvoiceNo, @InvoiceDate, @CustCode, @Paymode, @CreateUser, GETDATE(), @CreateUser, GETDATE()
      )
    `);

    // Update tblCreditSalesHeader:
    // Update tblCreditSalesHeader set PaidAmt=PaidAmt + Val(txtAmount), BalanceAmt=BalanceAmt- Val(txtAmount) Where Invoiceno=txtInvoiceNo
    const updateHeaderReq = new sql.Request(transaction);
    updateHeaderReq.input("Amount", sql.Decimal(18, 4), payAmt);
    updateHeaderReq.input("InvoiceNo", sql.VarChar, InvoiceNo.trim());
    await updateHeaderReq.query(`
      UPDATE tblCreditSalesHeader 
      SET PaidAmt = ISNULL(PaidAmt, 0) + @Amount, 
          BalanceAmt = ISNULL(BalanceAmt, 0) - @Amount 
      WHERE InvoiceNo = @InvoiceNo
    `);

    // Update next number counter in tblCompany:
    // Update tblCompany set NextCreditSalesPaymentNo=NextCreditSalesPaymentNo + 1
    const counterReq = new sql.Request(transaction);
    await counterReq.query("UPDATE tblCompany SET NextCreditSalesPaymentNo = ISNULL(NextCreditSalesPaymentNo, 0) + 1");

    await transaction.commit();
    res.json({ success: true, message: "Payment updated successfully" });
  } catch (err) {
    if (transaction._acquiredConnection) {
      try {
        await transaction.rollback();
      } catch (rbErr) {
        console.error("Rollback error:", rbErr);
      }
    }
    console.error("Credit Sales Payment Transaction Error:", err);
    res.status(500).json({ success: false, message: err.message || "Transaction failed" });
  }
});

// Also provide POST /pay alias for backwards compatibility
router.post("/pay", async (req, res, next) => {
  return router.handle({ ...req, url: "/save" }, res, next);
});

module.exports = router;
