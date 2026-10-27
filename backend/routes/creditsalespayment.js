const express = require("express");
const router = express.Router();
const { sql } = require("../db");

// Helper to pad voucher numbers
function padZero(num, size) {
  let s = num + "";
  while (s.length < size) s = "0" + s;
  return s;
}

// 1. GET next payment number
router.get("/next-number", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query(
      "SELECT TOP 1 LocationCode, NextCreditSalesPaymentNo FROM tblCompany"
    );

    if (result.recordset.length > 0) {
      const loc = (result.recordset[0].LocationCode || "HQ").trim();
      const nextNo = result.recordset[0].NextCreditSalesPaymentNo || 1;
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
router.get("/invoices", async (req, res) => {
  const { fromDate, toDate, status, customerCode } = req.query;

  if (!fromDate || !toDate) {
    return res.status(400).json({ success: false, message: "From Date and To Date are required" });
  }

  try {
    const pool = req.pool;
    
    // Parse YYYYMMDD into YYYY-MM-DD for standard implicit conversion
    const fYear = fromDate.substring(0, 4);
    const fMonth = fromDate.substring(4, 6);
    const fDay = fromDate.substring(6, 8);
    const formattedFromDate = `${fYear}-${fMonth}-${fDay} 00:00:00`;

    const tYear = toDate.substring(0, 4);
    const tMonth = toDate.substring(4, 6);
    const tDay = toDate.substring(6, 8);
    const formattedToDate = `${tYear}-${tMonth}-${tDay} 23:59:59`;

    let queryStr = `
      SELECT InvoiceNo, InvoiceDate, CustomerCode, CustomerName, NetTotal, PaidAmt, BalanceAmt, Remarks 
      FROM tblCreditSalesHeader 
      WHERE InvoiceDate >= @fromDate 
        AND InvoiceDate <= @toDate
    `;

    const request = pool.request();
    request.input("fromDate", sql.VarChar, formattedFromDate);
    request.input("toDate", sql.VarChar, formattedToDate);

    if (status === "PAID") {
      queryStr += " AND BalanceAmt = 0";
    } else if (status === "UNPAID") {
      queryStr += " AND BalanceAmt > 0";
    }

    queryStr += " ORDER BY InvoiceDate DESC";

    const result = await request.query(queryStr);
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Credit Invoices Error:", err);
    res.status(500).json({ success: false, message: "Failed to fetch invoices", error: err.message });
  }
});

// 3. POST make payment (with SQL transaction)
router.post("/pay", async (req, res) => {
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
    return res.status(400).json({ success: false, message: "Missing required details" });
  }

  const payAmt = parseFloat(Amount);
  if (isNaN(payAmt) || payAmt <= 0) {
    return res.status(400).json({ success: false, message: "Amount must be greater than zero" });
  }

  const pool = req.pool;
  const transaction = new sql.Transaction(pool);

  try {
    await transaction.begin();

    // Fetch the invoice header to validate total and balance
    const checkReq = new sql.Request(transaction);
    checkReq.input("InvoiceNo", sql.VarChar, InvoiceNo);
    const checkRes = await checkReq.query(
      "SELECT NetTotal, PaidAmt, BalanceAmt FROM tblCreditSalesHeader WHERE InvoiceNo = @InvoiceNo"
    );

    if (checkRes.recordset.length === 0) {
      throw new Error(`Invoice ${InvoiceNo} not found`);
    }

    const { NetTotal, PaidAmt, BalanceAmt } = checkRes.recordset[0];
    if (payAmt + PaidAmt > NetTotal) {
      throw new Error("Please Check the Amount, Total Amount Greater than Paid Amount");
    }

    // Format PaymentDate (YYYYMMDD) to date object
    const pYear = PaymentDate.substring(0, 4);
    const pMonth = PaymentDate.substring(4, 6);
    const pDay = PaymentDate.substring(6, 8);
    const parsedPaymentDate = new Date(parseInt(pYear), parseInt(pMonth) - 1, parseInt(pDay));

    // Insert payment detail
    const insertReq = new sql.Request(transaction);
    insertReq.input("PaymentNo", sql.VarChar, PaymentNo);
    insertReq.input("PaymentDate", sql.DateTime, parsedPaymentDate);
    insertReq.input("PaymentAmount", sql.Decimal(18, 4), payAmt);
    insertReq.input("Remarks", sql.VarChar, Remarks || "");
    insertReq.input("InvoiceNo", sql.VarChar, InvoiceNo);
    insertReq.input("InvoiceDate", sql.DateTime, new Date(InvoiceDate));
    insertReq.input("CustCode", sql.VarChar, CustCode);
    insertReq.input("Paymode", sql.VarChar, Paymode);
    insertReq.input("CreateUser", sql.VarChar, CreateUser || "pos");

    await insertReq.query(`
      INSERT INTO tblCreditSalespaymentdetail (
        PaymentNo, PaymentDate, PaymentAmount, Remarks, Invoiceno, InvoiceDate, CustCode, Paymode, CreateUser, CreateDate, ModifyUser, ModifyDate
      ) VALUES (
        @PaymentNo, @PaymentDate, @PaymentAmount, @Remarks, @InvoiceNo, @InvoiceDate, @CustCode, @Paymode, @CreateUser, GETDATE(), @CreateUser, GETDATE()
      )
    `);

    // Update tblCreditSalesHeader
    const updateHeaderReq = new sql.Request(transaction);
    updateHeaderReq.input("Amount", sql.Decimal(18, 4), payAmt);
    updateHeaderReq.input("InvoiceNo", sql.VarChar, InvoiceNo);
    await updateHeaderReq.query(`
      UPDATE tblCreditSalesHeader 
      SET PaidAmt = PaidAmt + @Amount, 
          BalanceAmt = BalanceAmt - @Amount 
      WHERE InvoiceNo = @InvoiceNo
    `);

    // Update next number counter in tblCompany
    const counterReq = new sql.Request(transaction);
    await counterReq.query("UPDATE tblCompany SET NextCreditSalesPaymentNo = NextCreditSalesPaymentNo + 1");

    await transaction.commit();
    res.json({ success: true, message: "Payment processed successfully" });
  } catch (err) {
    await transaction.rollback();
    console.error("Credit Sales Payment Transaction Error:", err);
    res.status(500).json({ success: false, message: err.message || "Transaction failed" });
  }
});

module.exports = router;
