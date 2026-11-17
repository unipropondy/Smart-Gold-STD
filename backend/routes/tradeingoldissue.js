const express = require("express");
const router = express.Router();
const { sql, poolPromise } = require("../db");

// Helper to acquire database connection pool reliably
const getPool = async (req) => {
  if (req && req.pool) return req.pool;
  if (poolPromise) return await poolPromise;
  throw new Error("Database pool is not available.");
};

// GET /api/tradeingoldissue/customers
// LOV for Customers
router.get("/customers", async (req, res) => {
  try {
    const pool = req.pool;
    const query = `
      SELECT 
        CustomerCode, 
        CustomerName, 
        Address1, 
        Address2, 
        Address3
      FROM tblCustomer
      UNION ALL
      SELECT 
        MemberCode AS CustomerCode, 
        MemberName AS CustomerName, 
        Address1, 
        Address2, 
        Address3
      FROM tblMember
      ORDER BY CustomerName ASC
    `;
    const result = await pool.request().query(query);
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Customers Error:", err);
    res.status(500).json({ success: false, message: "Error fetching customers" });
  }
});

// GET /api/tradeingoldissue/costpergram/:classcode
// Get Cost per gram based on Class Code
router.get("/costpergram/:classcode", async (req, res) => {
  try {
    const pool = req.pool;
    const { classcode } = req.params;
    let queryClass = classcode;

    // Handle mapping exactly like VB code
    if (classcode === "22K") queryClass = "22K%";
    else if (classcode === "18K") queryClass = "18K%";
    else queryClass = classcode + "%";

    const query = `SELECT Rate FROM tblClass WHERE ClassCode LIKE @ClassCode`;
    const result = await pool.request()
      .input('ClassCode', sql.VarChar, queryClass)
      .query(query);

    if (result.recordset.length > 0) {
      res.json({ success: true, Rate: result.recordset[0].Rate });
    } else {
      // Fallback
      const fallbackQuery = `SELECT TOP 1 Rate FROM tblClass`;
      const fbResult = await pool.request().query(fallbackQuery);
      if (fbResult.recordset.length > 0) {
        res.json({ success: true, Rate: fbResult.recordset[0].Rate });
      } else {
        res.json({ success: true, Rate: 0 });
      }
    }
  } catch (err) {
    console.error("Cost Per Gram Error:", err);
    res.status(500).json({ success: false, message: "Error fetching cost per gram" });
  }
});

// POST /api/tradeingoldissue/save
// Save Trade In Gold Issue Transaction
router.post("/save", async (req, res) => {
  let transaction;
  try {
    const pool = req.pool;
    transaction = new sql.Transaction(pool);

    const {
      customerCode,
      customerName,
      address1,
      address2,
      address3,
      lossPerc,
      costPerGram,
      remarks,
      items // The grid items
    } = req.body;

    if (!items || items.length === 0) {
      return res.status(400).json({ success: false, message: "At least one item is required in the grid." });
    }

    await transaction.begin();

    // Generate Invoice No (TI-XXXX)
    const seqReq = new sql.Request(transaction);
    const seqRes = await seqReq.query(`
      SELECT ISNULL(NextTradeInRefNo, 1) as NextNo FROM tblCompany
    `);
    const nextNo = seqRes.recordset[0].NextNo;
    const invoiceNo = nextNo.toString();
    const inventoryCode = "TI-" + invoiceNo;

    // Update Sequence
    const updSeqReq = new sql.Request(transaction);
    await updSeqReq.query(`
      UPDATE tblCompany SET NextTradeInRefNo = ISNULL(NextTradeInRefNo, 1) + 1
    `);

    // Delete existing if any (just in case)
    const delHeader = new sql.Request(transaction);
    await delHeader.input("InventoryCode", sql.VarChar, inventoryCode)
      .query(`DELETE FROM tblTradeInHeader WHERE InventoryCode = @InventoryCode`);

    const delDetail = new sql.Request(transaction);
    await delDetail.input("InventoryCode", sql.VarChar, inventoryCode)
      .query(`DELETE FROM tblTradeInDetail WHERE InventoryCode = @InventoryCode`);

    // Calculate Totals
    let totalWeight = 0;
    let totalGoldLoss = 0;
    let totalStoneWeight = 0;
    let totalPCS = 0;
    let totalAmount = 0;
    let totalMC = 0;

    items.forEach(item => {
      totalWeight += parseFloat(item.weight) || 0;
      totalGoldLoss += parseFloat(item.goldLoss) || 0;
      totalStoneWeight += parseFloat(item.stoneWeight) || 0;
      totalPCS += 1;
      totalAmount += parseFloat(item.totalCost) || 0;
      totalMC += parseFloat(item.pmc) || 0;
    });

    // Insert Header
    const insHeader = new sql.Request(transaction);
    await insHeader
      .input("InventoryCode", sql.VarChar, inventoryCode)
      .input("InvoiceNo", sql.VarChar, invoiceNo)
      .input("CustomerCode", sql.VarChar, customerCode || "")
      .input("CustomerName", sql.VarChar, customerName || "")
      .input("Address1", sql.VarChar, address1 || "")
      .input("Address2", sql.VarChar, address2 || "")
      .input("Address3", sql.VarChar, address3 || "")
      .input("CostPerGram", sql.Numeric(18, 4), parseFloat(costPerGram) || 0)
      .input("Weight", sql.Numeric(18, 4), totalWeight)
      .input("GoldLoss", sql.Numeric(18, 4), totalGoldLoss)
      .input("StoneWeight", sql.Numeric(18, 4), totalStoneWeight)
      .input("PCS", sql.Int, totalPCS)
      .input("TotalCost", sql.Numeric(18, 2), totalAmount)
      .input("TradeInAmount", sql.Numeric(18, 2), totalAmount)
      .input("Date", sql.DateTime, new Date())
      .query(`
        INSERT INTO tblTradeInHeader (
          InventoryCode, InvoiceNo, CustomerCode, CustomerName, Address1, Address2, Address3,
          CostPerGram, Weight, GoldLoss, StoneWeight, PCS, TotalCost, TradeInAmount, Date, IssueFlag
        ) VALUES (
          @InventoryCode, @InvoiceNo, @CustomerCode, @CustomerName, @Address1, @Address2, @Address3,
          @CostPerGram, @Weight, @GoldLoss, @StoneWeight, @PCS, @TotalCost, @TradeInAmount, @Date, 'N'
        )
      `);

    // Insert Details
    let sNo = 1;
    for (const item of items) {
      const insDetail = new sql.Request(transaction);
      await insDetail
        .input("InventoryCode", sql.VarChar, inventoryCode)
        .input("SNo", sql.Int, sNo++)
        .input("Description", sql.VarChar, item.description || "")
        .input("DiamondDescription", sql.VarChar, item.stoneDescription || "")
        .input("Weight", sql.Numeric(18, 4), parseFloat(item.weight) || 0)
        .input("StoneWeight", sql.Numeric(18, 4), parseFloat(item.stoneWeight) || 0)
        .input("GoldLoss", sql.Numeric(18, 4), parseFloat(item.goldLoss) || 0)
        .input("CostPerGram", sql.Numeric(18, 4), parseFloat(item.costPerGram) || 0)
        .input("TotalCost", sql.Numeric(18, 2), parseFloat(item.totalCost) || 0)
        .input("Class", sql.VarChar, item.class || "")
        .query(`
          INSERT INTO tblTradeInDetail (
            InventoryCode, SNo, Description, DiamondDescription, Weight, StoneWeight, 
            GoldLoss, CostPerGram, TotalCost, Class
          ) VALUES (
            @InventoryCode, @SNo, @Description, @DiamondDescription, @Weight, @StoneWeight,
            @GoldLoss, @CostPerGram, @TotalCost, @Class
          )
        `);
    }

    await transaction.commit();
    res.json({ success: true, message: "Trade In Gold Saved Successfully", invoiceNo: inventoryCode });

  } catch (err) {
    if (transaction) await transaction.rollback();
    console.error("TradeIn Gold Save Error:", err);
    res.status(500).json({ success: false, message: "Error saving trade in gold", error: err.message });
  }
});

// GET /api/tradeingoldissue/print-data
// Fetch actual Trade-In voucher data from SQL Server for printing
router.get("/print-data", async (req, res) => {
  try {
    const { invoiceNo } = req.query;
    if (!invoiceNo || !invoiceNo.trim()) {
      return res.status(400).json({ success: false, message: "Voucher / Invoice Number is required for print." });
    }

    const pool = req.pool;
    const cleanNo = invoiceNo.trim();

    // 1. Fetch Company Information
    const compRes = await pool.request().query("SELECT TOP 1 * FROM tblCompany");
    const rawCompany = compRes.recordset[0] || {};
    const company = {
      name: String(rawCompany.Name || rawCompany.CompanyName || '').trim(),
      address1: String(rawCompany.Address1 || '').trim(),
      address2: String(rawCompany.Address2 || '').trim(),
      city: String(rawCompany.City || '').trim(),
      state: String(rawCompany.State || '').trim(),
      country: String(rawCompany.Country || '').trim(),
      phone: String(rawCompany.Phone || '').trim(),
      fax: String(rawCompany.Fax || '').trim(),
    };

    // 2. Fetch Header Information from tblTradeInHeader
    let headerQuery = `
      SELECT TOP 1 *
      FROM tblTradeInHeader
      WHERE RTRIM(LTRIM(CAST(InvoiceNo AS VARCHAR(50)))) = @InvoiceNo
         OR RTRIM(LTRIM(CAST(InventoryCode AS VARCHAR(50)))) = @InvoiceNo
         OR RTRIM(LTRIM(CAST(InventoryCode AS VARCHAR(50)))) = 'TI-' + @InvoiceNo
    `;
    let headerRes = await pool.request()
      .input("InvoiceNo", sql.VarChar(50), cleanNo)
      .query(headerQuery);

    if (headerRes.recordset.length === 0) {
      return res.status(404).json({ success: false, message: `Trade-in voucher not found for: ${cleanNo}` });
    }

    const rawHeader = headerRes.recordset[0];
    const header = {
      inventoryCode: String(rawHeader.InventoryCode || '').trim(),
      invoiceNo: String(rawHeader.InvoiceNo || cleanNo).trim(),
      customerCode: String(rawHeader.CustomerCode || '').trim(),
      customerName: String(rawHeader.CustomerName || '').trim(),
      address1: String(rawHeader.Address1 || '').trim(),
      address2: String(rawHeader.Address2 || '').trim(),
      address3: String(rawHeader.Address3 || '').trim(),
      costPerGram: parseFloat(rawHeader.CostPerGram || 0),
      totalWeight: parseFloat(rawHeader.TotalWeight || 0),
      totalGoldLoss: parseFloat(rawHeader.TotalGoldLoss || 0),
      totalStoneWeight: parseFloat(rawHeader.TotalStoneWeight || 0),
      totalPCS: parseInt(rawHeader.TotalPCS || 0, 10),
      totalCost: parseFloat(rawHeader.TotalCost || 0),
      amount: parseFloat(rawHeader.Amount || rawHeader.TradeInAmount || rawHeader.TotalCost || 0),
      remarks: String(rawHeader.Remarks || '').trim(),
      date: rawHeader.CreateDate || rawHeader.Date || null,
    };

    // 3. Fetch Detail items from tblTradeInDetail
    const targetInvCode = header.inventoryCode || cleanNo;
    let detailQuery = `
      SELECT *
      FROM tblTradeInDetail
      WHERE RTRIM(LTRIM(CAST(InventoryCode AS VARCHAR(50)))) = @InventoryCode
         OR RTRIM(LTRIM(CAST(InvoiceNo AS VARCHAR(50)))) = @InvoiceNo
      ORDER BY ItemNo ASC
    `;
    let detailRes = await pool.request()
      .input("InventoryCode", sql.VarChar(50), targetInvCode)
      .input("InvoiceNo", sql.VarChar(50), header.invoiceNo)
      .query(detailQuery);

    const details = detailRes.recordset.map((row, idx) => ({
      itemNo: row.ItemNo !== null && row.ItemNo !== undefined ? row.ItemNo : (idx + 1),
      description: String(row.Description || '').trim(),
      diamondDescription: String(row.DiamondDescription || '').trim(),
      weight: parseFloat(row.Weight || 0),
      stoneWeight: parseFloat(row.StoneWeight || 0),
      goldLoss: parseFloat(row.GoldLoss || 0),
      costPerGram: parseFloat(row.CostPerGram || 0),
      totalCost: parseFloat(row.TotalCost || 0),
      class: String(row.Class || '').trim(),
      remarks: String(row.Remarks || '').trim(),
    }));

    // 4. Fetch Gold Purity Benchmark Rates from tblClass
    let rates = { rate999: '', rate916: '', rate835: '', rate22k: '' };
    try {
      const classRes = await pool.request().query("SELECT ClassCode, Rate FROM tblClass");
      classRes.recordset.forEach(r => {
        const code = String(r.ClassCode || '').trim().toUpperCase();
        const val = parseFloat(r.Rate || 0).toFixed(2);
        if (code.startsWith('999') || code.startsWith('24K')) rates.rate999 = val;
        else if (code.startsWith('916') || code.startsWith('22K')) {
          rates.rate916 = val;
          rates.rate22k = val;
        } else if (code.startsWith('835')) rates.rate835 = val;
      });
    } catch (eRates) {}

    res.json({
      success: true,
      data: {
        company,
        header,
        details,
        rates,
      }
    });

  } catch (error) {
    console.error("Fetch Trade In Print Data Error:", error);
    res.status(500).json({ success: false, message: "Error fetching trade-in print data: " + error.message });
  }
});

// ================= EXPORT PDF ENDPOINT =================
// POST /api/tradeingoldissue/export-pdf
router.post("/export-pdf", async (req, res) => {
  try {
    const { html, filename } = req.body;
    if (!html) {
      return res.status(400).json({ success: false, message: "HTML content is required for PDF export." });
    }

    const { convertHtmlToPdf } = require("../utils/htmlToPdf");
    const pdfBuffer = await convertHtmlToPdf(html);
    const downloadName = (filename || "Trade_In_Gold_Issue_Voucher.pdf").replace(/[^a-zA-Z0-9_\-\.]/g, "_");

    res.setHeader("Content-Type", "application/pdf");
    res.setHeader("Content-Disposition", `attachment; filename="${downloadName}"`);
    res.setHeader("Content-Length", pdfBuffer.length);
    return res.send(pdfBuffer);
  } catch (err) {
    console.error("Trade In Gold Issue Export PDF Error:", err);
    return res.status(500).json({ success: false, message: "Failed to generate PDF: " + err.message });
  }
});

// ================= EXPORT EXCEL ENDPOINT =================
// POST /api/tradeingoldissue/export-excel
router.post("/export-excel", async (req, res) => {
  try {
    const { invoiceNo, isReprint } = req.body;
    const cleanNo = (invoiceNo || "").trim();
    if (!cleanNo) {
      return res.status(400).json({ success: false, message: "Voucher / Invoice Number is required." });
    }

    const pool = await getPool(req);

    // 1. Company
    const compResult = await pool.request().query("SELECT TOP 1 * FROM tblCompany");
    const compRow = compResult.recordset[0] || {};
    const companyName = String(compRow.Name || compRow.CompanyName || "SMART").trim();
    const companyAddress = [compRow.Address1, compRow.Address2, compRow.City, compRow.Country].filter(Boolean).map(s => String(s).trim()).join(" ");

    // 2. Header
    let headerQuery = `
      SELECT TOP 1 *
      FROM tblTradeInHeader
      WHERE RTRIM(LTRIM(CAST(InvoiceNo AS VARCHAR(50)))) = @InvoiceNo
         OR RTRIM(LTRIM(CAST(InventoryCode AS VARCHAR(50)))) = @InvoiceNo
         OR RTRIM(LTRIM(CAST(InventoryCode AS VARCHAR(50)))) = 'TI-' + @InvoiceNo
    `;
    let headerRes = await pool.request()
      .input("InvoiceNo", sql.VarChar(50), cleanNo)
      .query(headerQuery);

    if (headerRes.recordset.length === 0) {
      return res.status(404).json({ success: false, message: `Trade In Gold Issue record "${cleanNo}" not found.` });
    }

    const header = headerRes.recordset[0];
    const targetInvCode = String(header.InventoryCode || cleanNo).trim();

    // 3. Details
    let detailQuery = `
      SELECT *
      FROM tblTradeInDetail
      WHERE RTRIM(LTRIM(CAST(InventoryCode AS VARCHAR(50)))) = @InventoryCode
         OR RTRIM(LTRIM(CAST(InvoiceNo AS VARCHAR(50)))) = @InvoiceNo
    `;
    let detailRes = await pool.request()
      .input("InventoryCode", sql.VarChar(50), targetInvCode)
      .input("InvoiceNo", sql.VarChar(50), String(header.InvoiceNo || cleanNo).trim())
      .query(detailQuery);
    const details = detailRes.recordset || [];

    // 4. Gold Rates
    const ratesResult = await pool.request().query("SELECT ClassCode, Rate FROM tblClass WHERE ClassCode IN ('24K', '22K', '835', '999', '916', '18K', 'SIL')");
    const ratesMap = {};
    ratesResult.recordset.forEach((r) => {
      ratesMap[(r.ClassCode || "").trim().toUpperCase()] = r.Rate;
    });
    const s999 = ratesMap["24K"] !== undefined ? parseFloat(ratesMap["24K"]).toFixed(2) : (ratesMap["999"] ? parseFloat(ratesMap["999"]).toFixed(2) : "-");
    const s916 = ratesMap["22K"] !== undefined ? parseFloat(ratesMap["22K"]).toFixed(2) : (ratesMap["916"] ? parseFloat(ratesMap["916"]).toFixed(2) : "-");
    const s835 = ratesMap["835"] !== undefined ? parseFloat(ratesMap["835"]).toFixed(2) : (ratesMap["18K"] ? parseFloat(ratesMap["18K"]).toFixed(2) : "-");

    // Dates
    const rawDate = header.Date || header.CreateDate;
    let voucherDateStr = "-";
    if (rawDate) {
      const d = new Date(rawDate);
      if (!isNaN(d.getTime())) {
        voucherDateStr = `${String(d.getDate()).padStart(2, '0')}/${String(d.getMonth() + 1).padStart(2, '0')}/${d.getFullYear()}`;
      }
    }
    const printTimeStr = new Date().toLocaleTimeString('en-US', { hour: '2-digit', minute: '2-digit', hour12: true });

    const reportTitle = "TRADE-IN GOLD ISSUE";

    const XLSX = require("xlsx");
    const wb = XLSX.utils.book_new();
    const rows = [];

    rows.push([companyName]);
    rows.push([companyAddress]);
    rows.push([]);
    rows.push([reportTitle]);
    rows.push([`Printed: ${voucherDateStr} ${printTimeStr}`]);
    rows.push([]);

    // Two panels
    const custName = (header.CustomerName || "").trim();
    const custAddress = [header.Address1, header.Address2, header.Address3].filter(Boolean).map(s => String(s).trim()).join(" ");

    rows.push(["CUSTOMER INFORMATION", "", "", "TRADE-IN VOUCHER DETAILS"]);
    rows.push(["Customer Code:", (header.CustomerCode || "-").trim(), "", "Voucher / Invoice No:", String(header.InvoiceNo || header.InventoryCode || cleanNo).trim()]);
    rows.push(["Customer Name:", custName || "CASH CUSTOMER", "", "Voucher Date:", voucherDateStr]);
    rows.push(["Address:", custAddress || "-", "", "Status:", "ISSUED"]);
    rows.push(["Remarks:", (header.Remarks || "-").trim(), "", "Purity / Class:", (details[0]?.Class || "22K")]);
    rows.push([]);

    // Items table
    rows.push(["#", "ITEM DESCRIPTION", "PURITY / CLASS", "GROSS WT (G)", "STONE WT (G)", "LOSS (G)", "RATE / G ($)", "TOTAL COST ($)"]);
    let sumGross = 0;
    let sumStone = 0;
    let sumLoss = 0;
    let sumCost = 0;

    if (details.length > 0) {
      details.forEach((item, idx) => {
        const wt = parseFloat(item.Weight) || 0;
        const st = parseFloat(item.StoneWeight) || 0;
        const loss = parseFloat(item.GoldLoss) || 0;
        const cost = parseFloat(item.TotalCost) || 0;
        sumGross += wt;
        sumStone += st;
        sumLoss += loss;
        sumCost += cost;

        rows.push([
          idx + 1,
          (item.Description || "Trade-In Item").trim(),
          (item.Class || "22K").trim(),
          wt.toFixed(3),
          st.toFixed(3),
          loss.toFixed(3),
          parseFloat(item.CostPerGram || 0),
          cost
        ]);
      });
    } else {
      const hWt = parseFloat(header.Weight || header.TotalWeight || 0);
      const hLoss = parseFloat(header.GoldLoss || header.TotalGoldLoss || 0);
      const hSt = parseFloat(header.StoneWeight || header.TotalStoneWeight || 0);
      const hCost = parseFloat(header.TotalCost || header.TradeInAmount || 0);
      sumGross = hWt;
      sumStone = hSt;
      sumLoss = hLoss;
      sumCost = hCost;

      rows.push([1, "TRADE-IN GOLD ITEMS", "22K", hWt.toFixed(3), hSt.toFixed(3), hLoss.toFixed(3), parseFloat(header.CostPerGram || 0), hCost]);
    }
    rows.push([]);

    // Summary totals
    rows.push(["", "", "", "Total Gross Weight:", `${sumGross.toFixed(3)} g`]);
    rows.push(["", "", "", "Total Stone Weight:", `${sumStone.toFixed(3)} g`]);
    rows.push(["", "", "", "Total Gold Loss:", `${sumLoss.toFixed(3)} g`]);
    rows.push(["", "", "", "TOTAL SETTLEMENT VALUE:", sumCost]);
    rows.push([]);

    // Signatures
    rows.push(["PREPARED BY (STAFF)", "", "", "SUPPLIER / AUTHORIZED RECEIVER"]);
    rows.push(["Staff Name:", "Staff", "", "Receiver Name:", custName || "CASH CUSTOMER"]);
    rows.push(["Signature:", "______________________", "", "Signature:", "______________________"]);
    rows.push(["Date:", "______________________", "", "Date:", "______________________"]);
    rows.push([]);
    rows.push(["*** System Generated Report ***"]);
    rows.push(["Powered by Unipro SG"]);

    const ws = XLSX.utils.aoa_to_sheet(rows);
    ws["!cols"] = [{ wch: 6 }, { wch: 28 }, { wch: 14 }, { wch: 14 }, { wch: 14 }, { wch: 14 }, { wch: 14 }, { wch: 18 }];
    XLSX.utils.book_append_sheet(wb, ws, "Trade In Gold Issue");

    const excelBuffer = XLSX.write(wb, { type: "buffer", bookType: "xlsx" });
    const downloadName = `Trade_In_Gold_Issue_${cleanNo.replace(/[^a-zA-Z0-9_\-]/g, "_")}.xlsx`;

    res.setHeader("Content-Type", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
    res.setHeader("Content-Disposition", `attachment; filename="${downloadName}"`);
    res.setHeader("Content-Length", excelBuffer.length);
    return res.send(excelBuffer);
  } catch (err) {
    console.error("Trade In Gold Issue Export Excel Error:", err);
    return res.status(500).json({ success: false, message: "Failed to generate Excel: " + err.message });
  }
});

module.exports = router;
