const express = require("express");
const router = express.Router();
const { sql } = require("../db");

// Helper to pad voucher numbers
function padZero(num, size) {
  let s = num + "";
  while (s.length < size) s = "0" + s;
  return s;
}

// 1. GET next installment number
router.get("/next-number", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query(
      "SELECT TOP 1 NextInstallmentNo FROM tblCompany"
    );

    if (result.recordset.length > 0) {
      const nextNo = result.recordset[0].NextInstallmentNo || 1;
      const formattedNo = `IN-${nextNo}`;
      res.json({ success: true, installmentNo: formattedNo });
    } else {
      res.json({ success: true, installmentNo: "IN-1" });
    }
  } catch (err) {
    console.error("Fetch Next Installment Number Error:", err);
    res.status(500).json({ success: false, message: "Failed to generate next installment number", error: err.message });
  }
});

// 1.5 GET all agreements for LOV lookup
router.get("/agreements", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query(`
      SELECT InsAgreementNo, CustomerCode, Name, InstallmentAmt, TotalCost 
      FROM tblInstallmentEntryHeader
      ORDER BY CreateDate DESC
    `);
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Agreements Error:", err);
    res.status(500).json({ success: false, message: "Failed to fetch agreements", error: err.message });
  }
});

// 2. GET agreement details
router.get("/agreement/:agreementNo", async (req, res) => {
  const { agreementNo } = req.params;
  try {
    const pool = req.pool;
    const request = pool.request();
    request.input("InsAgreementNo", sql.VarChar, agreementNo);

    // From legacy logic: GetInstallmentHdr
    const result = await request.query(`
      SELECT InsAgreementNo, CustomerCode, MemberCode, Name, Address1, Address2, Address3, 
             InstallmentAmt, InsTotal, InsPaid, TotalCost, DepositAmt, AmtPaid
      FROM tblInstallmentEntryHeader 
      WHERE InsAgreementNo = @InsAgreementNo
    `);

    if (result.recordset.length > 0) {
      res.json({ success: true, data: result.recordset[0] });
    } else {
      res.status(404).json({ success: false, message: "Agreement not found" });
    }
  } catch (err) {
    console.error("Fetch Agreement Error:", err);
    res.status(500).json({ success: false, message: "Failed to fetch agreement details", error: err.message });
  }
});

// 3. GET balance and amount paid
router.get("/balance/:agreementNo", async (req, res) => {
  const { agreementNo } = req.params;
  try {
    const pool = req.pool;
    const request = pool.request();
    request.input("InsAgreementNo", sql.VarChar, agreementNo);

    // GetInstallmentBalance equivalent
    const result = await request.query(`
      SELECT SUM(AmtPaid) as AmountPaid
      FROM tblInstallmentEntryHeader 
      WHERE InsAgreementNo = @InsAgreementNo
    `);

    res.json({ success: true, data: result.recordset[0] });
  } catch (err) {
    console.error("Fetch Balance Error:", err);
    res.status(500).json({ success: false, message: "Failed to fetch balance details", error: err.message });
  }
});

// 4. GET installment history
router.get("/installment-details/:agreementNo", async (req, res) => {
  const { agreementNo } = req.params;
  try {
    const pool = req.pool;
    const request = pool.request();
    request.input("InsAgreementNo", sql.VarChar, agreementNo);

    // GetInstallmentDetail equivalent
    const result = await request.query(`
      SELECT InstallmentNo, CreateDate, InsPaid, InstallmentAmt, AmtPaid
      FROM tblInstallmentDetail
      WHERE InsAgreementNo = @InsAgreementNo
      ORDER BY CreateDate ASC
    `);

    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Installment Details Error:", err);
    res.status(500).json({ success: false, message: "Failed to fetch installment history", error: err.message });
  }
});

// 5. POST pay installment
router.post("/pay", async (req, res) => {
  const {
    agreementNo,
    installmentNo,
    noOfInstallment,
    installmentAmount,
    totalAmount,
    tradeInSales,
    tradeInGST,
    paymodeDetails, // array of paymode details
    createDate, // today's date format string
    createUser
  } = req.body;

  const pool = req.pool;
  const transaction = new sql.Transaction(pool);

  try {
    await transaction.begin();

    // Update Header
    const updateHeaderReq = new sql.Request(transaction);
    updateHeaderReq.input("InsAgreementNo", sql.VarChar, agreementNo);
    updateHeaderReq.input("NoOfInstallment", sql.Int, parseInt(noOfInstallment));
    updateHeaderReq.input("InstallmentAmt", sql.Decimal(18, 2), parseFloat(installmentAmount));
    updateHeaderReq.input("TotalAmount", sql.Decimal(18, 2), parseFloat(totalAmount));
    updateHeaderReq.input("TradeInSales", sql.Decimal(18, 2), parseFloat(tradeInSales || 0));
    updateHeaderReq.input("TradeInGST", sql.Decimal(18, 2), parseFloat(tradeInGST || 0));
    
    await updateHeaderReq.query(`
      UPDATE tblInstallmentEntryHeader 
      SET InsPaid = ISNULL(InsPaid, 0) + @NoOfInstallment, 
          AmtPaid = ISNULL(AmtPaid, 0) + @TotalAmount,
          TradeInAmt = ISNULL(TradeInAmt, 0) + @TradeInSales,
          TradeInGST = ISNULL(TradeInGST, 0) + @TradeInGST
      WHERE InsAgreementNo = @InsAgreementNo
    `);

    // Insert Detail
    const insertDetailReq = new sql.Request(transaction);
    insertDetailReq.input("InsAgreementNo", sql.VarChar, agreementNo);
    insertDetailReq.input("InstallmentNo", sql.VarChar, installmentNo);
    insertDetailReq.input("NoOfInstallment", sql.Int, parseInt(noOfInstallment));
    insertDetailReq.input("InstallmentAmt", sql.Decimal(18, 2), parseFloat(installmentAmount));
    insertDetailReq.input("AmtPaid", sql.Decimal(18, 2), parseFloat(totalAmount));
    insertDetailReq.input("CreateDate", sql.DateTime, new Date());

    await insertDetailReq.query(`
      INSERT INTO tblInstallmentDetail (
        InsAgreementNo, InstallmentNo, CreateDate, InsPaid, InstallmentAmt, AmtPaid
      ) VALUES (
        @InsAgreementNo, @InstallmentNo, @CreateDate, @NoOfInstallment, @InstallmentAmt, @AmtPaid
      )
    `);

    // Handle Paymode Details
    if (paymodeDetails && paymodeDetails.length > 0) {
      for (const paymode of paymodeDetails) {
        const pmReq = new sql.Request(transaction);
        pmReq.input("RefNo", sql.VarChar, installmentNo);
        pmReq.input("PayDate", sql.DateTime, new Date());
        pmReq.input("PayMode", sql.VarChar, paymode.payMode);
        pmReq.input("Amount", sql.Decimal(18, 2), parseFloat(paymode.amount));
        pmReq.input("Remarks", sql.VarChar, paymode.remarks || 'INSTALLMENT');
        
        await pmReq.query(`
          INSERT INTO tblPayModeDetail (
            InvoiceNo, InvoiceDate, PayMode, Amount, CreateUser, CreateDate, LocationCode, SalesType, IsPaid
          ) VALUES (
            @RefNo, @PayDate, @PayMode, @Amount, 'SYS', @PayDate, 'HQ', 'INSTALLMENT', 1
          )
        `);
      }
    }

    // Update Company Counter
    const counterReq = new sql.Request(transaction);
    await counterReq.query("UPDATE tblCompany SET NextInstallmentNo = NextInstallmentNo + 1");

    await transaction.commit();
    res.json({ success: true, message: "Installment payment saved successfully" });
  } catch (err) {
    await transaction.rollback();
    console.error("Installment Save Error:", err);
    res.status(500).json({ success: false, message: "Failed to save installment payment", error: err.message });
  }
});

module.exports = router;
