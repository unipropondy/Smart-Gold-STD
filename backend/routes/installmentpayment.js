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
  let s = num + ""; b 
  while (s.length < size) s = "0" + s;
  return s;
}

// 1. GET next installment number
router.get("/next-number", async (req, res) => {
  try {
    const pool = await getPool(req);
    const result = await pool.request().query(
      "SELECT TOP 1 NextInstallmentNo FROM tblCompany"
    );

    if (result.recordset.length > 0) {
      const nextNo = result.recordset[0].NextInstallmentNo || 1;
      const formattedNo = `IN-${nextNo}`;
      res.json({ success: true, installmentNo: formattedNo, rawNo: nextNo });
    } else {
      res.json({ success: true, installmentNo: "IN-1", rawNo: 1 });
    }
  } catch (err) {
    console.error("Fetch Next Installment Number Error:", err);
    res.status(500).json({ success: false, message: "Failed to generate next installment number", error: err.message });
  }
});

// 1.5 GET all agreements for LOV lookup matching original VB filter (DepositAmt + AmtPaid <> TotalCost)
router.get("/agreements", async (req, res) => {
  try {
    const pool = await getPool(req);
    const { search } = req.query;
    let query = `
      SELECT InsAgreementNo, CustomerCode, MemberCode, Name, TotalCost, DepositAmt, AmtPaid, InstallmentAmt, InsTotal, InsPaid, LocationCode
      FROM tblInstallmentEntryHeader
      WHERE (ISNULL(DepositAmt, 0) + ISNULL(AmtPaid, 0)) < ISNULL(TotalCost, 0)
    `;

    if (search) {
      const safeSearch = search.replace(/'/g, "''");
      query += ` AND (InsAgreementNo LIKE '%${safeSearch}%' OR CustomerCode LIKE '%${safeSearch}%' OR Name LIKE '%${safeSearch}%')`;
    }

    query += " ORDER BY CreateDate DESC";

    const result = await pool.request().query(query);
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Agreements Error:", err);
    res.status(500).json({ success: false, message: "Failed to fetch agreements", error: err.message });
  }
});

// 2. GET agreement details matching original GetInstallmentHdr
router.get("/agreement/:agreementNo", async (req, res) => {
  const { agreementNo } = req.params;
  try {
    const pool = await getPool(req);
    const request = pool.request();
    request.input("InsAgreementNo", sql.VarChar, agreementNo);

    // From legacy logic: GetInstallmentHdr
    const result = await request.query(`
      SELECT h.InsAgreementNo, h.CustomerCode, h.MemberCode, h.Name, h.Address1, h.Address2, h.Address3, 
             h.InstallmentAmt, h.InsTotal, h.InsPaid, h.TotalCost, h.DepositAmt, h.AmtPaid, h.SalesMan as SalesManCode,
             c.CustomerType, c.MemberType
      FROM tblInstallmentEntryHeader h
      LEFT JOIN tblCustomer c ON c.CustomerCode = h.CustomerCode
      WHERE h.InsAgreementNo = @InsAgreementNo
    `);

    if (result.recordset.length > 0) {
      res.json({ success: true, data: result.recordset[0] });
    } else {
      res.status(404).json({ success: false, message: "This Installment Agreement No Not Found... Please Give Correct Installment Agreement No" });
    }
  } catch (err) {
    console.error("Fetch Agreement Error:", err);
    res.status(500).json({ success: false, message: "Failed to fetch agreement details", error: err.message });
  }
});

// 3. GET balance and amount paid matching GetInstallmentBalance
router.get("/balance/:agreementNo", async (req, res) => {
  const { agreementNo } = req.params;
  try {
    const pool = await getPool(req);
    const request = pool.request();
    request.input("InsAgreementNo", sql.VarChar, agreementNo);

    // GetInstallmentBalance equivalent from ClsInstallment.vb
    const result = await request.query(`
      SELECT ISNULL(SUM(AmtPaid), 0) as AmountPaid
      FROM tblInstallmentDetail 
      WHERE InsAgreementNo = @InsAgreementNo
    `);

    let amtPaid = 0;
    if (result.recordset.length > 0 && result.recordset[0].AmountPaid !== null) {
      amtPaid = parseFloat(result.recordset[0].AmountPaid) || 0;
    }

    res.json({ success: true, data: { AmountPaid: amtPaid } });
  } catch (err) {
    console.error("Fetch Balance Error:", err);
    res.status(500).json({ success: false, message: "Failed to fetch balance details", error: err.message });
  }
});

// 4. GET installment history matching GetInstallmentDetail
router.get("/installment-details/:agreementNo", async (req, res) => {
  const { agreementNo } = req.params;
  try {
    const pool = await getPool(req);
    const request = pool.request();
    request.input("InsAgreementNo", sql.VarChar, agreementNo);

    // GetInstallmentDetail equivalent
    const result = await request.query(`
      SELECT InstallmentNo, CreateDate, InsPaid, InstallmentAmt, AmtPaid
      FROM tblInstallmentDetail
      WHERE InsAgreementNo = @InsAgreementNo
      ORDER BY CreateDate ASC, InstallmentNo ASC
    `);

    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Installment Details Error:", err);
    res.status(500).json({ success: false, message: "Failed to fetch installment history", error: err.message });
  }
});

// 4.5 CANCEL Installment Bill (matching frmInstallmentPayment btnBillCancelOK_Click)
router.post("/cancel", async (req, res) => {
  const { installmentNo } = req.body;
  if (!installmentNo) {
    return res.status(400).json({ success: false, message: "Please provide Installment Bill No" });
  }

  const pool = await getPool(req);
  const transaction = new sql.Transaction(pool);

  try {
    await transaction.begin();

    // 1. Check if bill exists
    const checkReq = new sql.Request(transaction);
    checkReq.input("InstallmentNo", sql.VarChar, installmentNo);
    const insRes = await checkReq.query(`
      SELECT InsAgreementNo, InstallmentNo, CreateDate, InsPaid, InstallmentAmt, AmtPaid 
      FROM tblInstallmentDetail 
      WHERE InstallmentNo = @InstallmentNo
    `);

    if (insRes.recordset.length === 0) {
      await transaction.rollback();
      return res.status(404).json({ success: false, message: "This Installment Bill Not Found... Please Give Correct Installment Bill No" });
    }

    const bill = insRes.recordset[0];
    const billDate = new Date(bill.CreateDate).toDateString();
    const today = new Date().toDateString();

    if (billDate !== today) {
      await transaction.rollback();
      return res.status(400).json({ success: false, message: "You cannot cancel History Inventory Bills ...." });
    }

    // 2. Decrement Header InsPaid and AmtPaid
    const rollbackHdr = new sql.Request(transaction);
    rollbackHdr.input("InsAgreementNo", sql.VarChar, bill.InsAgreementNo);
    rollbackHdr.input("InsPaid", sql.Int, parseInt(bill.InsPaid, 10) || 0);
    rollbackHdr.input("AmtPaid", sql.Decimal(18, 2), parseFloat(bill.AmtPaid) || 0);
    await rollbackHdr.query(`
      UPDATE tblInstallmentEntryHeader
      SET InsPaid = CASE WHEN (ISNULL(InsPaid, 0) - @InsPaid) < 0 THEN 0 ELSE (ISNULL(InsPaid, 0) - @InsPaid) END,
          AmtPaid = CASE WHEN (ISNULL(AmtPaid, 0) - @AmtPaid) < 0 THEN 0 ELSE (ISNULL(AmtPaid, 0) - @AmtPaid) END
      WHERE InsAgreementNo = @InsAgreementNo
    `);

    // 3. Delete Detail
    const delDetail = new sql.Request(transaction);
    delDetail.input("InstallmentNo", sql.VarChar, installmentNo);
    await delDetail.query("DELETE FROM tblInstallmentDetail WHERE InstallmentNo = @InstallmentNo");

    // 4. Delete Paymode Details
    const delPm = new sql.Request(transaction);
    delPm.input("InstallmentNo", sql.VarChar, installmentNo);
    await delPm.query("DELETE FROM tblPayModeDetail WHERE InvoiceNo = @InstallmentNo AND SalesType = 'INSTALLMENT'");

    await transaction.commit();
    res.json({ success: true, message: `Installment Bill ${installmentNo} cancelled successfully`, agreementNo: bill.InsAgreementNo });
  } catch (err) {
    try { await transaction.rollback(); } catch (e) {}
    console.error("Cancel Bill Error:", err);
    res.status(500).json({ success: false, message: err.message || "Failed to cancel bill" });
  }
});

// 5. POST pay installment (Complete transactional matching of VB.NET btnSAve_Click)
router.post("/pay", async (req, res) => {
  const {
    agreementNo,
    installmentNo,
    noOfInstallment,
    installmentAmount,
    totalAmount,
    tradeInSales,
    tradeInGST,
    paymodeDetails,
    createUser,
    locationCode
  } = req.body;

  const pool = await getPool(req);
  const transaction = new sql.Transaction(pool);

  try {
    await transaction.begin();

    // 1. Generate or verify the next installment number from tblCompany
    let activeInsNo = installmentNo;
    const nextNoReq = new sql.Request(transaction);
    const compRes = await nextNoReq.query("SELECT TOP 1 ISNULL(NextInstallmentNo, 1) as NextInstallmentNo, LocationCode FROM tblCompany");
    if (compRes.recordset.length > 0) {
      const nextNo = compRes.recordset[0].NextInstallmentNo;
      activeInsNo = `IN-${nextNo}`;
    }

    const nQty = parseInt(noOfInstallment, 10) || 1;
    const nInstAmt = parseFloat(installmentAmount) || 0;
    const nTotAmt = parseFloat(totalAmount) || 0;
    const nTradeInSales = parseFloat(tradeInSales) || 0;
    const nTradeInGST = parseFloat(tradeInGST) || 0;
    const loc = locationCode || (compRes.recordset.length > 0 ? compRes.recordset[0].LocationCode : 'HQ') || 'HQ';
    const user = createUser || 'ADMIN';

    // 2. Update Header
    const updateHeaderReq = new sql.Request(transaction);
    updateHeaderReq.input("InsAgreementNo", sql.VarChar(20), agreementNo);
    updateHeaderReq.input("NoOfInstallment", sql.Int, nQty);
    updateHeaderReq.input("TotalAmount", sql.Decimal(18, 2), nTotAmt);
    updateHeaderReq.input("TradeInSales", sql.Decimal(18, 2), nTradeInSales);
    updateHeaderReq.input("TradeInGST", sql.Decimal(18, 2), nTradeInGST);
    
    await updateHeaderReq.query(`
      UPDATE tblInstallmentEntryHeader 
      SET InsPaid = ISNULL(InsPaid, 0) + @NoOfInstallment, 
          AmtPaid = ISNULL(AmtPaid, 0) + @TotalAmount,
          TradeInAmt = ISNULL(TradeInAmt, 0) + @TradeInSales,
          TradeInGST = ISNULL(TradeInGST, 0) + @TradeInGST
      WHERE InsAgreementNo = @InsAgreementNo
    `);

    // 3. Insert Installment Detail (tblInstallmentDetail)
    const insertDetailReq = new sql.Request(transaction);
    insertDetailReq.input("InsAgreementNo", sql.VarChar(20), agreementNo);
    insertDetailReq.input("InstallmentNo", sql.VarChar(20), activeInsNo);
    insertDetailReq.input("NoOfInstallment", sql.Int, nQty);
    insertDetailReq.input("InstallmentAmt", sql.Decimal(18, 2), nInstAmt);
    insertDetailReq.input("AmtPaid", sql.Decimal(18, 2), nTotAmt);
    insertDetailReq.input("CreateDate", sql.DateTime, new Date());

    await insertDetailReq.query(`
      INSERT INTO tblInstallmentDetail (
        InsAgreementNo, InstallmentNo, CreateDate, InsPaid, InstallmentAmt, AmtPaid
      ) VALUES (
        @InsAgreementNo, @InstallmentNo, @CreateDate, @NoOfInstallment, @InstallmentAmt, @AmtPaid
      )
    `);

    // 4. Handle Paymode Details (tblPayModeDetail)
    if (paymodeDetails && paymodeDetails.length > 0) {
      for (const paymode of paymodeDetails) {
        const pmAmt = parseFloat(paymode.amount) || 0;
        if (pmAmt > 0) {
          const pmReq = new sql.Request(transaction);
          pmReq.input("RefNo", sql.VarChar(20), activeInsNo);
          pmReq.input("PayDate", sql.DateTime, new Date());
          pmReq.input("PayMode", sql.VarChar(20), paymode.payMode || 'CASH');
          pmReq.input("Amount", sql.Decimal(18, 2), pmAmt);
          pmReq.input("Remarks", sql.VarChar(100), paymode.remarks || 'INSTALLMENT');
          pmReq.input("User", sql.VarChar(20), user);
          pmReq.input("Loc", sql.VarChar(20), loc);
          
          await pmReq.query(`
            INSERT INTO tblPayModeDetail (
              InvoiceNo, InvoiceDate, PayMode, Amount, CreateUser, CreateDate, LocationCode, SalesType, IsPaid
            ) VALUES (
              @RefNo, @PayDate, @PayMode, @Amount, @User, @PayDate, @Loc, 'INSTALLMENT', 1
            )
          `);
        }
      }
    } else {
      // Default to CASH if no specific payment modes passed
      const pmReq = new sql.Request(transaction);
      pmReq.input("RefNo", sql.VarChar(20), activeInsNo);
      pmReq.input("PayDate", sql.DateTime, new Date());
      pmReq.input("Amount", sql.Decimal(18, 2), nTotAmt);
      pmReq.input("User", sql.VarChar(20), user);
      pmReq.input("Loc", sql.VarChar(20), loc);
      
      await pmReq.query(`
        INSERT INTO tblPayModeDetail (
          InvoiceNo, InvoiceDate, PayMode, Amount, CreateUser, CreateDate, LocationCode, SalesType, IsPaid
        ) VALUES (
          @RefNo, @PayDate, 'CASH', @Amount, @User, @PayDate, @Loc, 'INSTALLMENT', 1
        )
      `);
    }

    // 5. Update Company Counter (NextInstallmentNo)
    const counterReq = new sql.Request(transaction);
    await counterReq.query("UPDATE tblCompany SET NextInstallmentNo = ISNULL(NextInstallmentNo, 0) + 1");

    // 6. Check if fully settled (subChecktheAmount logic in VB.NET)
    const checkFullReq = new sql.Request(transaction);
    checkFullReq.input("InsAgreementNo", sql.VarChar(20), agreementNo);
    const checkHdr = await checkFullReq.query(`
      SELECT TotalCost, DepositAmt, AmtPaid FROM tblInstallmentEntryHeader WHERE InsAgreementNo = @InsAgreementNo
    `);

    let isFullySettled = false;
    if (checkHdr.recordset.length > 0) {
      const h = checkHdr.recordset[0];
      const totCost = parseFloat(h.TotalCost) || 0;
      const depAmt = parseFloat(h.DepositAmt) || 0;
      const paidAmt = parseFloat(h.AmtPaid) || 0;
      if ((depAmt + paidAmt) >= totCost && totCost > 0) {
        isFullySettled = true;
      }
    }

    await transaction.commit();
    res.json({
      success: true,
      message: "Transaction Saved Successfully",
      installmentNo: activeInsNo,
      invoiceNo: activeInsNo,
      isFullySettled
    });
  } catch (err) {
    try { await transaction.rollback(); } catch (e) {}
    console.error("Installment Save Error:", err);
    res.status(500).json({ success: false, message: "Failed to save installment payment", error: err.message });
  }
});

// 6. GET Print & RePrint Data matching frmInstallmentPayment.vb subPrintInstallment
// Selection Formula: {tblInstallmentDetail.InstallmentNo} = sInvoiceNo
// Reports: rptInstallmentPayment_AJ.rpt / rptInstallmentPayment_MD.rpt / rptInstallmentPayment_MJ.rpt
router.get("/print-data", async (req, res) => {
  const installmentNo = (req.query.installmentNo || req.query.billNo || "").trim();
  if (!installmentNo) {
    return res.status(400).json({ success: false, message: "Please provide an Installment Number." });
  }

  try {
    const pool = await getPool(req);

    // 1. Fetch Company Information (tblCompany)
    const compResult = await pool.request().query(`
      SELECT TOP 1 Name, Address1, Address2, City, State, Country, Phone, Fax, CompanyShortCode, CurrencySign
      FROM tblCompany
    `);
    const compRow = compResult.recordset[0] || {};
    const company = {
      name: (compRow.Name || "SMART").trim(),
      address1: (compRow.Address1 || "").trim(),
      address2: (compRow.Address2 || "").trim(),
      city: (compRow.City || "").trim(),
      state: (compRow.State || "").trim(),
      country: (compRow.Country || "").trim(),
      phone: (compRow.Phone || "").trim(),
      fax: (compRow.Fax || "").trim(),
      companyShortCode: (compRow.CompanyShortCode || "MJ").trim(),
      currencySign: (compRow.CurrencySign || "SGD").trim(),
    };

    // 2. Fetch Installment Detail joined with Agreement Header and Customer
    const detailReq = pool.request();
    detailReq.input("InstallmentNo", sql.VarChar(50), installmentNo);
    const detailResult = await detailReq.query(`
      SELECT 
        d.InstallmentNo, d.InsAgreementNo, d.InstallmentAmt, d.AmtPaid, d.InsPaid, d.Remarks as DetailRemarks,
        d.CreateUser, d.CreateDate,
        h.InvoiceNo, h.InvoiceDate, h.CustomerCode, h.Name as HeaderCustomerName,
        h.Address1 as HeaderAddress1, h.Address2 as HeaderAddress2, h.Address3 as HeaderAddress3,
        h.TotalCost, h.DepositAmt, h.AmtPaid as HeaderAmtPaid, h.TotalInstallment, h.InsPaid as HeaderInsPaid,
        h.Remarks as HeaderRemarks, h.LocationCode, h.SalesMan,
        c.CustomerName, c.Address1 as CustAddress1, c.Address2 as CustAddress2, c.Address3 as CustAddress3,
        c.Phone1, c.Mobile, c.CustomerType
      FROM tblInstallmentDetail d
      LEFT JOIN tblInstallmentEntryHeader h ON RTRIM(LTRIM(h.InsAgreementNo)) = RTRIM(LTRIM(d.InsAgreementNo))
      LEFT JOIN tblCustomer c ON RTRIM(LTRIM(c.CustomerCode)) = RTRIM(LTRIM(h.CustomerCode))
      WHERE RTRIM(LTRIM(d.InstallmentNo)) = @InstallmentNo
    `);

    if (!detailResult.recordset.length) {
      return res.status(404).json({
        success: false,
        message: `Installment Payment record for "${installmentNo}" not found in database.`,
      });
    }

    const row = detailResult.recordset[0];
    const agreementNo = (row.InsAgreementNo || "").trim();

    // 3. Fetch Full Installment History for this agreement
    let history = [];
    if (agreementNo) {
      const histReq = pool.request();
      histReq.input("InsAgreementNo", sql.VarChar(50), agreementNo);
      const histResult = await histReq.query(`
        SELECT InstallmentNo, CreateDate, InsPaid, InstallmentAmt, AmtPaid, Remarks, CreateUser
        FROM tblInstallmentDetail
        WHERE RTRIM(LTRIM(InsAgreementNo)) = @InsAgreementNo
        ORDER BY CreateDate ASC, InstallmentNo ASC
      `);
      history = histResult.recordset.map((h) => ({
        InstallmentNo: (h.InstallmentNo || "").trim(),
        CreateDate: h.CreateDate,
        InsPaid: h.InsPaid || 1,
        InstallmentAmt: parseFloat(h.InstallmentAmt) || 0,
        AmtPaid: parseFloat(h.AmtPaid) || 0,
        Remarks: (h.Remarks || "").trim(),
        CreateUser: (h.CreateUser || "").trim(),
      }));
    }

    // 4. Fetch Gold Rates (Rate1, Rate2, Rate3 from tblClass)
    const ratesResult = await pool.request().query(`
      SELECT ClassCode, Rate FROM tblClass WHERE ClassCode IN ('24K', '22K', '835', '999', '916', '18K', 'SIL')
    `);
    const ratesMap = {};
    ratesResult.recordset.forEach((r) => {
      ratesMap[(r.ClassCode || "").trim().toUpperCase()] = r.Rate;
    });

    const rates = {
      rate999: ratesMap["24K"] !== undefined ? ratesMap["24K"] : (ratesMap["999"] || 190),
      rate916: ratesMap["22K"] !== undefined ? ratesMap["22K"] : (ratesMap["916"] || 176),
      rate835: ratesMap["835"] !== undefined ? ratesMap["835"] : (ratesMap["18K"] || 0),
    };

    // 5. Fetch PayMode Details (tblPayModeDetail) if recorded
    const payModeReq = pool.request();
    payModeReq.input("InvoiceNo", sql.VarChar(50), installmentNo);
    const payModeResult = await payModeReq.query(`
      SELECT PayMode, Amount FROM tblPayModeDetail WHERE RTRIM(LTRIM(InvoiceNo)) = @InvoiceNo
    `);
    const payModes = payModeResult.recordset.map((p) => ({
      payMode: (p.PayMode || "").trim(),
      amount: parseFloat(p.Amount) || 0,
    }));

    // Construct response
    const payment = {
      InstallmentNo: (row.InstallmentNo || "").trim(),
      InsAgreementNo: agreementNo,
      InstallmentAmt: parseFloat(row.InstallmentAmt) || 0,
      AmtPaid: parseFloat(row.AmtPaid) || 0,
      InsPaid: row.InsPaid || 1,
      Remarks: (row.DetailRemarks || "").trim(),
      CreateUser: (row.CreateUser || "").trim(),
      CreateDate: row.CreateDate,
      // Agreement Header Details
      CustomerCode: (row.CustomerCode || "").trim(),
      CustomerName: (row.CustomerName || row.HeaderCustomerName || "").trim(),
      CustomerAddress1: (row.CustAddress1 || row.HeaderAddress1 || "").trim(),
      CustomerAddress2: (row.CustAddress2 || row.HeaderAddress2 || "").trim(),
      CustomerAddress3: (row.CustAddress3 || row.HeaderAddress3 || "").trim(),
      CustomerPhone: (row.Mobile || row.Phone1 || "").trim(),
      CustomerType: (row.CustomerType || "").trim(),
      TotalCost: parseFloat(row.TotalCost) || 0,
      DepositAmt: parseFloat(row.DepositAmt) || 0,
      HeaderAmtPaid: parseFloat(row.HeaderAmtPaid) || 0,
      TotalInstallment: row.TotalInstallment || 0,
      HeaderInsPaid: row.HeaderInsPaid || 0,
      HeaderRemarks: (row.HeaderRemarks || "").trim(),
      LocationCode: (row.LocationCode || "").trim(),
      SalesMan: (row.SalesMan || "").trim(),
    };

    res.json({
      success: true,
      data: {
        company,
        payment,
        history,
        rates,
        payModes,
      },
    });
  } catch (err) {
    console.error("Installment Print Data Error:", err);
    res.status(500).json({
      success: false,
      message: "Failed to retrieve installment print data.",
      error: err.message,
    });
  }
});

module.exports = router;







