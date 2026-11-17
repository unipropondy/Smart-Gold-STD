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

// GET /api/payout/nextCode
router.get("/nextCode", async (req, res) => {
  try {
    const pool = await getPool(req);
    const result = await pool.request().query("SELECT TOP 1 LocationCode, NextPayOutNo FROM tblCompany");

    let payoutNo = 1;
    let loc = "HQ";
    if (result.recordset && result.recordset.length > 0) {
      payoutNo = parseInt(result.recordset[0].NextPayOutNo) || 1;
      loc = (result.recordset[0].LocationCode || "HQ").trim();
    }
    const nextCode = `PO-${loc}${String(payoutNo).padStart(8, "0")}`;
    res.json({ success: true, data: nextCode });
  } catch (err) {
    console.error("Get Next Payout Code Error:", err);
    res.status(500).json({ success: false, message: "Database error", error: err.message });
  }
});

// POST /api/payout/save
router.post("/save", async (req, res) => {
  const pool = await getPool(req);
  const transaction = new sql.Transaction(pool);

  try {
    let { Code, sDescription, PayOutDescription, Amount, Remarks, userId } = req.body;

    Code = (Code || "").trim();
    sDescription = (sDescription || "").trim();
    PayOutDescription = (PayOutDescription || "").trim();
    Remarks = (Remarks || "").trim();
    const payAmount = parseFloat(Amount) || 0;
    const sysUser = (userId || "sys").substring(0, 10);

    if (!Code) {
      return res.status(400).json({ success: false, message: "PayOutCode should not be blank." });
    }
    if (!PayOutDescription) {
      return res.status(400).json({ success: false, message: "PayOut Description should not be blank." });
    }
    if (payAmount <= 0) {
      return res.status(400).json({ success: false, message: "Amount must be greater than zero." });
    }

    await transaction.begin();

    // 1. Fetch Company Config and current sequence
    const compResult = await new sql.Request(transaction).query(`
      SELECT TOP 1 LocationCode, NextPayOutNo FROM tblCompany
    `);
    const comp = compResult.recordset && compResult.recordset.length > 0 ? compResult.recordset[0] : {};
    const loc = (comp.LocationCode || "HQ").trim();
    const nextSeq = parseInt(comp.NextPayOutNo) || 1;

    const payOutNo = `PO-${loc}${String(nextSeq).padStart(8, "0")}`;

    // 2. Insert Payout details
    await new sql.Request(transaction)
      .input("PayOutNo", sql.VarChar, payOutNo)
      .input("Code", sql.Char, Code.substring(0, 10))
      .input("Description", sql.VarChar, sDescription.substring(0, 500))
      .input("PayOutDescription", sql.VarChar, PayOutDescription.substring(0, 500))
      .input("Amount", sql.Numeric(18, 4), payAmount)
      .input("Remarks", sql.VarChar, Remarks.substring(0, 50))
      .input("CreateUser", sql.VarChar, sysUser)
      .query(`
        INSERT INTO tblPayOutDetail (
          PayOutNo, PayOutDate, Code, Description, PayOutDescription, Amount, Remarks,
          CreateUser, CreateDate, ModifyUser, ModifyDate
        ) VALUES (
          @PayOutNo, GETDATE(), @Code, @Description, @PayOutDescription, @Amount, @Remarks,
          @CreateUser, GETDATE(), @CreateUser, GETDATE()
        )
      `);

    // 3. Insert Paymode details matching clsPayMode.PayModeDetailSave
    await new sql.Request(transaction)
      .input("InvoiceNo", sql.Char, payOutNo.substring(0, 20))
      .input("PayMode", sql.VarChar, "CASH")
      .input("Amount", sql.Numeric(18, 4), -payAmount)
      .input("CreateUser", sql.Char, sysUser)
      .input("LocationCode", sql.VarChar, loc)
      .query(`
        INSERT INTO tblPayModeDetail (
          InvoiceNo, InvoiceDate, PayMode, Amount, Commision, SlipNo,
          CreateUser, CreateDate, ModifyUser, ModifyDate, LocationCode, SalesType, IsPaid
        ) VALUES (
          @InvoiceNo, GETDATE(), @PayMode, @Amount, 0, '',
          @CreateUser, GETDATE(), @CreateUser, GETDATE(), @LocationCode, 'PAYOUT', 1
        )
      `);

    // 4. Increment NextPayOutNo sequence
    await new sql.Request(transaction).query(`
      UPDATE tblCompany SET NextPayOutNo = ISNULL(NextPayOutNo, 0) + 1
    `);

    await transaction.commit();
    res.json({ success: true, message: "PayOut detail saved successfully.", data: payOutNo, payoutNo: payOutNo });

  } catch (err) {
    console.error("Save Payout Transaction Error:", err);
    try {
      await transaction.rollback();
    } catch (rErr) {
      console.error("Rollback failed:", rErr);
    }
    res.status(500).json({ success: false, message: "Database transaction failed", error: err.message });
  }
});

// 3. GET /api/payout/print-data?payoutNo=...
// Crystal Report: Reports/rptPayOut.rpt
// Selection: {tblPayoutdetail.PayOutNo} = @PayOutNo
// Formula Fields: CompAddress1, CompAddress2, CompAddress3
router.get("/print-data", async (req, res) => {
  const payoutNo = (req.query.payoutNo || req.query.invoiceNo || "").trim();
  if (!payoutNo) {
    return res.status(400).json({ success: false, message: "PayOut Number is required." });
  }

  try {
    const pool = await getPool(req);

    // 1. Fetch Company Information matching objClsCompany.getCompanyName
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

    // 2. Fetch PayOut Detail matching {tblPayoutdetail.PayOutNo} = sInvoiceNo
    const payoutReq = pool.request();
    payoutReq.input("PayOutNo", sql.VarChar(50), payoutNo);
    const payoutResult = await payoutReq.query(`
      SELECT 
        PayOutNo, PayOutDate, Code, Description, PayOutDescription,
        Amount, Remarks, CreateUser, CreateDate, ModifyUser, ModifyDate
      FROM tblPayOutDetail
      WHERE RTRIM(LTRIM(PayOutNo)) = @PayOutNo
    `);

    if (!payoutResult.recordset.length) {
      return res.status(404).json({
        success: false,
        message: `PayOut record "${payoutNo}" not found in database.`,
      });
    }

    const row = payoutResult.recordset[0];

    // 3. Fetch linked Paymode if recorded in tblPayModeDetail
    const pmReq = pool.request();
    pmReq.input("InvoiceNo", sql.VarChar(50), payoutNo);
    const pmResult = await pmReq.query(`
      SELECT PayMode, Amount, SlipNo
      FROM tblPayModeDetail
      WHERE RTRIM(LTRIM(InvoiceNo)) = @InvoiceNo
    `);
    const payModes = pmResult.recordset.map((p) => ({
      payMode: (p.PayMode || "").trim(),
      amount: Math.abs(parseFloat(p.Amount) || 0),
      slipNo: (p.SlipNo || "").trim(),
    }));

    // 4. Fetch Gold Rates (Rate1, Rate2, Rate3 from tblClass) for standard gold strip
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

    const payout = {
      PayOutNo: (row.PayOutNo || "").trim(),
      PayOutDate: row.PayOutDate || row.CreateDate,
      Code: (row.Code || "").trim(),
      Description: (row.Description || "").trim(),
      PayOutDescription: (row.PayOutDescription || "").trim(),
      Amount: parseFloat(row.Amount) || 0,
      Remarks: (row.Remarks || "").trim(),
      CreateUser: (row.CreateUser || "sys").trim(),
      CreateDate: row.CreateDate,
    };

    res.json({
      success: true,
      data: {
        company,
        payout,
        payModes,
        rates,
      },
    });

  } catch (err) {
    console.error("PayOut Print Data Error:", err);
    res.status(500).json({
      success: false,
      message: "Failed to retrieve payout print data.",
      error: err.message,
    });
  }
});

// ================= EXPORT PDF ENDPOINT =================
// POST /api/payout/export-pdf
router.post("/export-pdf", async (req, res) => {
  try {
    const { html, filename } = req.body;
    if (!html) {
      return res.status(400).json({ success: false, message: "HTML content is required for PDF export." });
    }

    const { convertHtmlToPdf } = require("../utils/htmlToPdf");
    const pdfBuffer = await convertHtmlToPdf(html);
    const downloadName = (filename || "Payout_Voucher.pdf").replace(/[^a-zA-Z0-9_\-\.]/g, "_");

    res.setHeader("Content-Type", "application/pdf");
    res.setHeader("Content-Disposition", `attachment; filename="${downloadName}"`);
    res.setHeader("Content-Length", pdfBuffer.length);
    return res.send(pdfBuffer);
  } catch (err) {
    console.error("Payout Export PDF Error:", err);
    return res.status(500).json({ success: false, message: "Failed to generate PDF: " + err.message });
  }
});

// ================= EXPORT EXCEL ENDPOINT =================
// POST /api/payout/export-excel
router.post("/export-excel", async (req, res) => {
  try {
    const { payoutNo, isReprint } = req.body;
    const rawPayoutNo = (payoutNo || "").trim();
    if (!rawPayoutNo) {
      return res.status(400).json({ success: false, message: "PayOut Number is required." });
    }

    const pool = await getPool(req);

    // 1. Fetch Company Information
    const compRes = await pool.request().query("SELECT TOP 1 * FROM tblCompany");
    const compRow = compRes.recordset[0] || {};
    const companyName = (compRow.Name || "SMART").trim();
    const companyAddress = [compRow.Address1, compRow.Address2, compRow.City, compRow.Country].filter(Boolean).map(s => String(s).trim()).join(" ");

    // 2. Fetch Payout Detail
    const payoutReq = pool.request();
    payoutReq.input("PayOutNo", sql.VarChar(50), rawPayoutNo);
    const payoutResult = await payoutReq.query(`
      SELECT 
        PayOutNo, PayOutDate, Code, Description, PayOutDescription,
        Amount, Remarks, CreateUser, CreateDate, ModifyUser, ModifyDate
      FROM tblPayOutDetail
      WHERE RTRIM(LTRIM(PayOutNo)) = @PayOutNo
    `);

    if (!payoutResult.recordset.length) {
      return res.status(404).json({
        success: false,
        message: `PayOut record "${rawPayoutNo}" not found.`,
      });
    }

    const row = payoutResult.recordset[0];

    // 3. Fetch linked Paymode
    const pmReq = pool.request();
    pmReq.input("InvoiceNo", sql.VarChar(50), rawPayoutNo);
    const pmResult = await pmReq.query(`
      SELECT PayMode, Amount, SlipNo
      FROM tblPayModeDetail
      WHERE RTRIM(LTRIM(InvoiceNo)) = @InvoiceNo
    `);
    const payModes = pmResult.recordset.map((p) => ({
      payMode: (p.PayMode || "").trim(),
      amount: Math.abs(parseFloat(p.Amount) || 0),
    }));
    const disbursementText = payModes.length > 0 ? payModes.map(p => p.payMode).join(", ") : "CASH";

    // 4. Fetch Gold Rates
    const ratesResult = await pool.request().query(`
      SELECT ClassCode, Rate FROM tblClass WHERE ClassCode IN ('24K', '22K', '835', '999', '916', '18K', 'SIL')
    `);
    const ratesMap = {};
    ratesResult.recordset.forEach((r) => {
      ratesMap[(r.ClassCode || "").trim().toUpperCase()] = r.Rate;
    });

    const s999 = ratesMap["24K"] !== undefined ? parseFloat(ratesMap["24K"]).toFixed(2) : (ratesMap["999"] ? parseFloat(ratesMap["999"]).toFixed(2) : "-");
    const s916 = ratesMap["22K"] !== undefined ? parseFloat(ratesMap["22K"]).toFixed(2) : (ratesMap["916"] ? parseFloat(ratesMap["916"]).toFixed(2) : "-");
    const s835 = ratesMap["835"] !== undefined ? parseFloat(ratesMap["835"]).toFixed(2) : (ratesMap["18K"] ? parseFloat(ratesMap["18K"]).toFixed(2) : "-");

    // Format Dates
    const rawDate = row.PayOutDate || row.CreateDate;
    let payoutDateStr = "-";
    if (rawDate) {
      const d = new Date(rawDate);
      if (!isNaN(d.getTime())) {
        payoutDateStr = `${String(d.getDate()).padStart(2, '0')}/${String(d.getMonth() + 1).padStart(2, '0')}/${d.getFullYear()}`;
      }
    }
    const printTimeStr = new Date().toLocaleTimeString('en-US', { hour: '2-digit', minute: '2-digit', hour12: true });
    const payoutAmount = parseFloat(row.Amount) || 0;

    const reportTitle = "PAYOUT";

    // Build Excel Workbook
    const XLSX = require("xlsx");
    const wb = XLSX.utils.book_new();

    const rows = [];
    rows.push([companyName]);
    rows.push([companyAddress]);
    rows.push([]);
    rows.push([reportTitle]);
    rows.push([`Printed: ${payoutDateStr} ${printTimeStr}`]);
    rows.push([]);

    // Two Panels: Expense Classification & Voucher Audit Details
    rows.push(["EXPENSE CLASSIFICATION", "", "", "VOUCHER AUDIT DETAILS"]);
    rows.push(["Expense Code:", (row.Code || "-").trim(), "", "Voucher No:", (row.PayOutNo || rawPayoutNo).trim()]);
    rows.push(["Category:", (row.Description || "-").trim(), "", "Issued Date:", payoutDateStr]);
    rows.push(["Disbursement:", disbursementText, "", "Processed By:", (row.CreateUser || "sys").trim()]);
    rows.push([]);

    // Prominent Highlight: Total Amount Paid Out
    rows.push(["TOTAL AMOUNT PAID OUT:", `$${Number(payoutAmount.toFixed(2))}`, "", `SETTLED IN FULL VIA ${disbursementText}`]);
    rows.push([]);

    // Payout Particulars & Details Table
    rows.push([
      "#",
      "CODE / CATEGORY",
      "PAYOUT DESCRIPTION / PURPOSE",
      "AMOUNT ($)"
    ]);

    const codeCat = `${(row.Code || "").trim()} - ${(row.Description || "").trim()}`.replace(/^ - | - $/g, "");
    const purpose = (row.PayOutDescription || row.Remarks || row.Description || "Payout disbursement").trim();

    rows.push([
      1,
      codeCat || "-",
      purpose,
      Number(payoutAmount.toFixed(2))
    ]);

    rows.push([]);

    // Totals Section
    rows.push(["", "", "Subtotal Payout:", Number(payoutAmount.toFixed(2))]);
    rows.push(["", "", "NET CASH DISBURSED:", Number(payoutAmount.toFixed(2))]);
    rows.push([]);

    // Signatures
    rows.push(["PREPARED BY (STAFF)", "", "", "SUPPLIER / AUTHORIZED RECEIVER"]);
    rows.push(["Staff Name:", (row.CreateUser || "sys").trim(), "", "Receiver Name:", ""]);
    rows.push(["Signature:", "______________________", "", "Signature:", "______________________"]);
    rows.push(["Date:", "______________________", "", "Date:", "______________________"]);
    rows.push([]);
    rows.push(["*** System Generated Report ***"]);
    rows.push(["Powered by Unipro SG"]);

    const ws = XLSX.utils.aoa_to_sheet(rows);

    // Set Column Widths
    ws["!cols"] = [
      { wch: 6 },
      { wch: 28 },
      { wch: 38 },
      { wch: 18 }
    ];

    XLSX.utils.book_append_sheet(wb, ws, "Payout Voucher");

    const excelBuffer = XLSX.write(wb, { type: "buffer", bookType: "xlsx" });
    const downloadName = `Payout_Voucher_${rawPayoutNo.replace(/[^a-zA-Z0-9_\-]/g, "_")}.xlsx`;

    res.setHeader("Content-Type", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
    res.setHeader("Content-Disposition", `attachment; filename="${downloadName}"`);
    res.setHeader("Content-Length", excelBuffer.length);
    return res.send(excelBuffer);
  } catch (err) {
    console.error("Payout Export Excel Error:", err);
    return res.status(500).json({ success: false, message: "Failed to generate Excel: " + err.message });
  }
});

module.exports = router;

