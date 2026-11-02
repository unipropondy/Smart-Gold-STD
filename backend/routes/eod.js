const express = require("express");
const router = express.Router();
const { sql } = require("../db");
const { validateBackendDates } = require("../utils/validateReportDates");

// ==========================================
// 15 EOD REPORT DEFINITIONS & TITLES
// ==========================================
const EOD_REPORT_CONFIG = [
  { id: "0", label: "Sales Detail Journal", key: "sales_detail_journal" },
  { id: "1", label: "Salesman Wise Journal", key: "salesman_wise_journal" },
  { id: "2", label: "Terminal Wise Journal", key: "terminal_wise_journal" },
  { id: "3", label: "Refund Journal", key: "refund_journal" },
  { id: "4", label: "Trade In Journal", key: "trade_in_journal" },
  { id: "5", label: "Installment Journal", key: "installment_journal" },
  { id: "6", label: "Closing Stock Report (Pieces) by Category", key: "closing_stock_pieces_category" },
  { id: "7", label: "Closing Stock Report (Weight) by Category", key: "closing_stock_weight_category" },
  { id: "8", label: "Closing Stock Report (Pieces) by Tray", key: "closing_stock_pieces_tray" },
  { id: "9", label: "Below Cost Sales Report", key: "below_cost_sales_report" },
  { id: "10", label: "Closed Bills Listing", key: "closed_bills_listing" },
  { id: "11", label: "Purchase Return Journal", key: "purchase_return_journal" },
  { id: "12", label: "Gold Bar Purchase", key: "gold_bar_purchase" },
  { id: "13", label: "Miscellaneous Journal", key: "miscellaneous_journal" },
  { id: "14", label: "Normal Sales Profit", key: "normal_sales_profit" }
];

// Helper to parse dates
const parseDate = (dStr) => {
  if (!dStr) return new Date();
  if (typeof dStr === "string" && dStr.includes("/")) {
    const parts = dStr.split("/");
    if (parts.length === 3) {
      return new Date(`${parts[2]}-${parts[1].padStart(2, "0")}-${parts[0].padStart(2, "0")}`);
    }
  }
  return new Date(dStr);
};

const formatDateOnly = (d) => {
  if (!d || isNaN(new Date(d).getTime())) return "";
  const dt = new Date(d);
  const day = String(dt.getDate()).padStart(2, "0");
  const month = String(dt.getMonth() + 1).padStart(2, "0");
  const year = dt.getFullYear();
  return `${day}/${month}/${year}`;
};

// ==========================================
// 1. GET INITIAL EOD CONFIG & DATES
// GET /api/eod/config
// ==========================================
router.get("/config", async (req, res) => {
  try {
    const pool = req.pool;
    const locationCode = (req.query.locationCode || "HQ").trim();

    // 1. Fetch location's Last EOD Date
    const locReq = pool.request();
    locReq.input("LocationCode", sql.VarChar, locationCode);
    const locRes = await locReq.query(`
      SELECT TOP 1 RTRIM(LocationCode) AS LocationCode, RTRIM(LocationName) AS LocationName, EODDate 
      FROM tblLocation WITH (NOLOCK)
      WHERE RTRIM(LocationCode) = @LocationCode
    `);

    let lastEODDate = null;
    let locationName = "";
    if (locRes.recordset.length > 0) {
      lastEODDate = locRes.recordset[0].EODDate;
      locationName = locRes.recordset[0].LocationName;
    }

    // 2. Fetch company info for shortcode & print option default
    const compRes = await pool.request().query(`
      SELECT TOP 1 RTRIM(CompanyShortCode) AS CompanyShortCode, RTRIM(Name) AS CompanyName 
      FROM tblCompany WITH (NOLOCK)
    `);
    const companyShortCode = compRes.recordset[0]?.CompanyShortCode || "MJ";
    const defaultPrintOption = companyShortCode.trim().toUpperCase() === "AS" ? "Preview" : "Print";

    // 3. Fetch report checkbox selections from tblEODReports
    const repRes = await pool.request().query(`
      SELECT RTRIM(ReportName) AS ReportName, RTRIM(PrintYesOrNo) AS PrintYesOrNo
      FROM tblEODReports WITH (NOLOCK)
      ORDER BY CAST(ReportName AS INT)
    `);

    const reportsMap = {};
    repRes.recordset.forEach((r) => {
      reportsMap[r.ReportName.trim()] = r.PrintYesOrNo === "Y";
    });

    const reports = EOD_REPORT_CONFIG.map((cfg) => ({
      ...cfg,
      selected: reportsMap[cfg.id] !== undefined ? reportsMap[cfg.id] : (cfg.id === "0" || cfg.id === "6" || cfg.id === "7")
    }));

    // Next suggested EOD Date is either same day or next day after lastEODDate
    let suggestedEODDate = new Date();
    if (lastEODDate) {
      const nextD = new Date(lastEODDate);
      nextD.setDate(nextD.getDate() + 1);
      suggestedEODDate = nextD;
    }

    res.json({
      success: true,
      data: {
        locationCode,
        locationName,
        lastEODDate: lastEODDate ? lastEODDate.toISOString().split("T")[0] : null,
        lastEODDateDisp: formatDateOnly(lastEODDate),
        suggestedEODDate: suggestedEODDate.toISOString().split("T")[0],
        suggestedEODDateDisp: formatDateOnly(suggestedEODDate),
        companyShortCode,
        defaultPrintOption,
        reports
      }
    });
  } catch (err) {
    console.error("Error in GET /api/eod/config:", err);
    res.status(500).json({ success: false, message: err.message });
  }
});

// ==========================================
// 2. VALIDATE EOD DATE
// POST /api/eod/validate
// ==========================================
router.post("/validate", async (req, res) => {
  try {
    const pool = req.pool;
    const { locationCode = "HQ", eodDate } = req.body;

    if (!eodDate) {
      return res.status(400).json({ success: false, message: "Enter Valid EOD Date" });
    }

    const locReq = pool.request();
    locReq.input("LocationCode", sql.VarChar, locationCode.trim());
    const locRes = await locReq.query(`
      SELECT TOP 1 EODDate FROM tblLocation WITH (NOLOCK) WHERE RTRIM(LocationCode) = @LocationCode
    `);

    if (locRes.recordset.length === 0) {
      return res.status(404).json({ success: false, message: "Location record not found." });
    }

    const lastEOD = locRes.recordset[0].EODDate ? new Date(locRes.recordset[0].EODDate) : null;
    const enteredDate = parseDate(eodDate);

    if (isNaN(enteredDate.getTime())) {
      return res.status(400).json({ success: false, message: "Enter Valid EOD Date" });
    }

    enteredDate.setHours(0, 0, 0, 0);

    if (lastEOD) {
      lastEOD.setHours(0, 0, 0, 0);

      const nextDay = new Date(lastEOD);
      nextDay.setDate(nextDay.getDate() + 1);
      nextDay.setHours(0, 0, 0, 0);

      // Business Rule: Allowed if enteredDate == lastEOD or enteredDate == nextDay
      const isSameDay = enteredDate.getTime() === lastEOD.getTime();
      const isNextDay = enteredDate.getTime() === nextDay.getTime();

      if (!isSameDay && !isNextDay) {
        return res.status(400).json({
          success: false,
          message: `EOD Date must be either the Previous EOD Date (${formatDateOnly(lastEOD)}) or Next Day (${formatDateOnly(nextDay)}).`
        });
      }
    }

    res.json({
      success: true,
      message: "EOD Date is valid.",
      eodDate: enteredDate.toISOString().split("T")[0],
      eodDateDisp: formatDateOnly(enteredDate)
    });
  } catch (err) {
    console.error("Error in POST /api/eod/validate:", err);
    res.status(500).json({ success: false, message: err.message });
  }
});

// ==========================================
// 3. UPDATE EOD REPORT PREFERENCES
// PUT /api/eod/reports
// ==========================================
router.put("/reports", async (req, res) => {
  try {
    const pool = req.pool;
    const { reports = [], userId = "ADMIN" } = req.body;

    for (const rep of reports) {
      const repReq = pool.request();
      repReq.input("ReportName", sql.Char(10), rep.id.toString());
      repReq.input("Flag", sql.Char(1), rep.selected ? "Y" : "N");
      repReq.input("UserID", sql.Char(20), userId);
      await repReq.execute("sp_updateEODReports");
    }

    res.json({ success: true, message: "EOD Report preferences updated successfully." });
  } catch (err) {
    console.error("Error in PUT /api/eod/reports:", err);
    res.status(500).json({ success: false, message: err.message });
  }
});

// ==========================================
// 4. PROCESS EOD (F9 ACTION & TRANSACTION)
// POST /api/eod/process
// ==========================================
router.post("/process", async (req, res) => {
  const transaction = new sql.Transaction(req.pool);

  try {
    const {
      locationCode = "HQ",
      eodDate,
      selectedReportIds = [],
      userId = "ADMIN",
      computerName = "SmartGold_RN"
    } = req.body;

    if (!eodDate) {
      return res.status(400).json({ success: false, message: "Enter Valid EOD Date" });
    }

    const enteredDate = parseDate(eodDate);
    enteredDate.setHours(0, 0, 0, 0);

    // 1. Verify EOD date eligibility before starting transaction
    const locReq = req.pool.request();
    locReq.input("LocationCode", sql.VarChar, locationCode.trim());
    const locRes = await locReq.query(`
      SELECT TOP 1 EODDate FROM tblLocation WITH (NOLOCK) WHERE RTRIM(LocationCode) = @LocationCode
    `);

    if (locRes.recordset.length === 0) {
      return res.status(404).json({ success: false, message: "Location record not found." });
    }

    const lastEOD = locRes.recordset[0].EODDate ? new Date(locRes.recordset[0].EODDate) : null;
    if (lastEOD) {
      lastEOD.setHours(0, 0, 0, 0);
      const nextDay = new Date(lastEOD);
      nextDay.setDate(nextDay.getDate() + 1);
      nextDay.setHours(0, 0, 0, 0);

      const isSameDay = enteredDate.getTime() === lastEOD.getTime();
      const isNextDay = enteredDate.getTime() === nextDay.getTime();

      if (!isSameDay && !isNextDay) {
        return res.status(400).json({
          success: false,
          message: `EOD Date must be either ${formatDateOnly(lastEOD)} or ${formatDateOnly(nextDay)}.`
        });
      }
    }

    // 2. Start SQL Transaction
    await transaction.begin();

    // A. Execute sp_EODByCategory
    const reqCat = new sql.Request(transaction);
    reqCat.input("LocationCode", sql.Char(20), locationCode.trim());
    reqCat.input("EODDate", sql.DateTime, enteredDate);
    reqCat.input("UserID", sql.Char(10), userId);
    await reqCat.execute("sp_EODByCategory");

    // B. Execute sp_EODByTray
    const reqTray = new sql.Request(transaction);
    reqTray.input("LocationCode", sql.Char(20), locationCode.trim());
    reqTray.input("EODDate", sql.DateTime, enteredDate);
    reqTray.input("UserID", sql.Char(10), userId);
    await reqTray.execute("sp_EODByTray");

    // C. Execute sp_EOD
    const reqEOD = new sql.Request(transaction);
    reqEOD.input("EODDate", sql.DateTime, enteredDate);
    reqEOD.input("LocationCode", sql.Char(20), locationCode.trim());
    reqEOD.input("UserId", sql.Char(10), userId);
    await reqEOD.execute("sp_EOD");

    // D. Update Location EOD Date via sp_UpdateEODDate
    const reqDate = new sql.Request(transaction);
    reqDate.input("EODDate", sql.DateTime, enteredDate);
    reqDate.input("LocationCode", sql.Char(15), locationCode.trim());
    await reqDate.execute("sp_UpdateEODDate");

    // E. Save report selection preferences
    for (let i = 0; i <= 14; i++) {
      const isSelected = selectedReportIds.includes(i.toString());
      const repReq = new sql.Request(transaction);
      repReq.input("ReportName", sql.Char(10), i.toString());
      repReq.input("Flag", sql.Char(1), isSelected ? "Y" : "N");
      repReq.input("UserID", sql.Char(20), userId);
      await repReq.execute("sp_updateEODReports");
    }

    // F. Populate Closing Stock Tables for Reports if selected
    if (selectedReportIds.includes("6") || selectedReportIds.includes("7")) {
      const reqRptCat = new sql.Request(transaction);
      reqRptCat.input("LocationCode", sql.Char(20), locationCode.trim());
      reqRptCat.input("EODDate", sql.DateTime, enteredDate);
      reqRptCat.input("ComputerName", sql.VarChar(50), computerName);
      await reqRptCat.execute("sp_RptEODCategory");
    }

    if (selectedReportIds.includes("8")) {
      const reqRptTray = new sql.Request(transaction);
      reqRptTray.input("LocationCode", sql.Char(20), locationCode.trim());
      reqRptTray.input("EODDate", sql.DateTime, enteredDate);
      reqRptTray.input("ComputerName", sql.VarChar(50), computerName);
      await reqRptTray.execute("sp_RptEODTray");
    }

    // Commit Transaction
    await transaction.commit();

    res.json({
      success: true,
      message: `EOD Processing successfully completed for ${formatDateOnly(enteredDate)}. Location EOD updated.`,
      newEODDate: enteredDate.toISOString().split("T")[0],
      newEODDateDisp: formatDateOnly(enteredDate)
    });
  } catch (err) {
    console.error("Error during EOD Process transaction:", err);
    try {
      await transaction.rollback();
    } catch (rbErr) {
      console.error("Rollback error:", rbErr);
    }
    res.status(500).json({
      success: false,
      message: `EOD Processing failed: ${err.message}. Changes rolled back.`
    });
  }
});

// ==========================================
// 5. GENERATE INDIVIDUAL EOD REPORT DATA
// POST /api/eod/reports/generate
// ==========================================
router.post("/reports/generate", async (req, res) => {
  try {
    const pool = req.pool;
    const {
      reportId = "0",
      locationCode = "HQ",
      eodDate,
      computerName = "SmartGold_RN"
    } = req.body;

    if (eodDate) {
      const dateVal = validateBackendDates(eodDate, eodDate);
      if (!dateVal.isValid) {
        return res.status(400).json({ success: false, message: dateVal.message });
      }
    }

    const targetDate = eodDate ? parseDate(eodDate) : new Date();
    targetDate.setHours(0, 0, 0, 0);

    const nextDate = new Date(targetDate);
    nextDate.setDate(nextDate.getDate() + 1);

    const targetDateDisp = formatDateOnly(targetDate);

    // Fetch company info
    const compRes = await pool.request().query(`
      SELECT TOP 1 RTRIM(Name) AS CompanyName, RTRIM(Address1) AS Address1, RTRIM(City) AS City
      FROM tblCompany WITH (NOLOCK)
    `);
    const company = compRes.recordset[0] || {};

    const request = pool.request();
    request.input("FromDate", sql.DateTime, targetDate);
    request.input("ToDateExclusive", sql.DateTime, nextDate);
    request.input("LocationCode", sql.VarChar, locationCode.trim());
    request.input("ComputerName", sql.VarChar, computerName);

    let title = "";
    let columns = [];
    let rows = [];

    // Report 0: Sales Detail Journal
    if (reportId === "0") {
      title = `Sales Detail Journal - EOD ${targetDateDisp}`;
      columns = [
        { key: "InvoiceNo", label: "Invoice No", width: 140 },
        { key: "InventoryCode", label: "Inventory Code", width: 140 },
        { key: "Description", label: "Description", width: 200 },
        { key: "PCS", label: "PCS", width: 70, align: "right" },
        { key: "Weight", label: "Weight (g)", width: 110, align: "right", format: "decimal3" },
        { key: "Rate", label: "Rate", width: 90, align: "right", format: "currency" },
        { key: "SMC", label: "MC", width: 90, align: "right", format: "currency" },
        { key: "GST", label: "GST", width: 90, align: "right", format: "currency" },
        { key: "NetAmount", label: "Net Total", width: 120, align: "right", format: "currency" }
      ];

      const resQ = await request.query(`
        SELECT 
          RTRIM(sh.InvoiceNo) AS InvoiceNo,
          RTRIM(sd.InventoryCode) AS InventoryCode,
          ISNULL(RTRIM(sd.Description), '') AS Description,
          ISNULL(sd.PCS, 1) AS PCS,
          ROUND(ISNULL(sd.Weight, 0), 3) AS Weight,
          ROUND(ISNULL(sd.Rate, 0), 2) AS Rate,
          ROUND(ISNULL(sd.SMC, 0), 2) AS SMC,
          ROUND(ISNULL(sd.GST, 0), 2) AS GST,
          ROUND(ISNULL(sd.NetAmount, 0), 2) AS NetAmount
        FROM tblSalesHeader sh WITH (NOLOCK)
        INNER JOIN tblSalesDetail sd WITH (NOLOCK) ON sh.InvoiceNo = sd.InvoiceNo
        WHERE sh.InvoiceDate >= @FromDate AND sh.InvoiceDate < @ToDateExclusive AND sh.LocationCode = @LocationCode
        ORDER BY sh.InvoiceNo, sd.InventoryCode
      `);
      rows = resQ.recordset;
    }

    // Report 1: Salesman Wise Journal
    else if (reportId === "1") {
      title = `Salesman Wise Journal - EOD ${targetDateDisp}`;
      columns = [
        { key: "SalesManCode", label: "Salesman Code", width: 120 },
        { key: "SalesManName", label: "Salesman Name", width: 220 },
        { key: "BillsCount", label: "Bills Count", width: 100, align: "right" },
        { key: "TotalWeight", label: "Weight (g)", width: 120, align: "right", format: "decimal3" },
        { key: "TotalSMC", label: "Total MC", width: 110, align: "right", format: "currency" },
        { key: "NetTotal", label: "Net Sales", width: 130, align: "right", format: "currency" }
      ];

      const resQ = await request.query(`
        SELECT 
          ISNULL(RTRIM(sh.SalesManCode), 'DIRECT') AS SalesManCode,
          ISNULL(RTRIM(sm.SalesManName), RTRIM(sh.SalesManCode)) AS SalesManName,
          COUNT(sh.InvoiceNo) AS BillsCount,
          ROUND(SUM(ISNULL(sh.TotalWeight, 0)), 3) AS TotalWeight,
          ROUND(SUM(ISNULL(sh.TotalSMC, 0)), 2) AS TotalSMC,
          ROUND(SUM(ISNULL(sh.NetTotal, 0)), 2) AS NetTotal
        FROM tblSalesHeader sh WITH (NOLOCK)
        LEFT JOIN tblSalesMan sm WITH (NOLOCK) ON sh.SalesManCode = sm.SalesManCode
        WHERE sh.InvoiceDate >= @FromDate AND sh.InvoiceDate < @ToDateExclusive AND sh.LocationCode = @LocationCode
        GROUP BY sh.SalesManCode, sm.SalesManName
        ORDER BY NetTotal DESC
      `);
      rows = resQ.recordset;
    }

    // Report 2: Terminal Wise Journal
    else if (reportId === "2") {
      title = `Terminal Wise Journal - EOD ${targetDateDisp}`;
      columns = [
        { key: "TerminalCode", label: "Terminal", width: 120 },
        { key: "BillsCount", label: "Bills Count", width: 110, align: "right" },
        { key: "TotalWeight", label: "Total Wt (g)", width: 130, align: "right", format: "decimal3" },
        { key: "GST", label: "GST Collected", width: 120, align: "right", format: "currency" },
        { key: "NetTotal", label: "Net Total", width: 140, align: "right", format: "currency" }
      ];

      const resQ = await request.query(`
        SELECT 
          ISNULL(RTRIM(sh.TerminalCode), 'POS-01') AS TerminalCode,
          COUNT(sh.InvoiceNo) AS BillsCount,
          ROUND(SUM(ISNULL(sh.TotalWeight, 0)), 3) AS TotalWeight,
          ROUND(SUM(ISNULL(sh.GST, 0)), 2) AS GST,
          ROUND(SUM(ISNULL(sh.NetTotal, 0)), 2) AS NetTotal
        FROM tblSalesHeader sh WITH (NOLOCK)
        WHERE sh.InvoiceDate >= @FromDate AND sh.InvoiceDate < @ToDateExclusive AND sh.LocationCode = @LocationCode
        GROUP BY sh.TerminalCode
        ORDER BY TerminalCode
      `);
      rows = resQ.recordset;
    }

    // Report 3: Refund Journal (Sales Return)
    else if (reportId === "3") {
      title = `Refund Journal - EOD ${targetDateDisp}`;
      columns = [
        { key: "RefNo", label: "Refund / Return No", width: 140 },
        { key: "InvoiceNo", label: "Orig Invoice", width: 130 },
        { key: "CustomerCode", label: "Customer", width: 110 },
        { key: "TotalPCS", label: "PCS", width: 70, align: "right" },
        { key: "TotalWeight", label: "Weight (g)", width: 110, align: "right", format: "decimal3" },
        { key: "NetTotal", label: "Refund Amount", width: 130, align: "right", format: "currency" }
      ];

      const resQ = await request.query(`
        SELECT 
          RTRIM(sr.RefNo) AS RefNo,
          RTRIM(sr.InvoiceNo) AS InvoiceNo,
          RTRIM(sr.CustomerCode) AS CustomerCode,
          ISNULL(sr.TotalPCS, 1) AS TotalPCS,
          ROUND(ISNULL(sr.TotalWeight, 0), 3) AS TotalWeight,
          ROUND(ISNULL(sr.NetTotal, 0), 2) AS NetTotal
        FROM tblSalesReturnHeader sr WITH (NOLOCK)
        WHERE sr.InvoiceDate >= @FromDate AND sr.InvoiceDate < @ToDateExclusive AND sr.LocationCode = @LocationCode
        ORDER BY sr.RefNo
      `);
      rows = resQ.recordset;
    }

    // Report 4: Trade In Journal
    else if (reportId === "4") {
      title = `Trade In Journal - EOD ${targetDateDisp}`;
      columns = [
        { key: "ReferenceNo", label: "Trade In Ref", width: 140 },
        { key: "VendorName", label: "Customer / Vendor", width: 200 },
        { key: "TotalPCS", label: "PCS", width: 70, align: "right" },
        { key: "TotalWeight", label: "Total Wt (g)", width: 120, align: "right", format: "decimal3" },
        { key: "CostPerGram", label: "Cost / g", width: 100, align: "right", format: "currency" },
        { key: "Amount", label: "Amount", width: 120, align: "right", format: "currency" }
      ];

      const resQ = await request.query(`
        SELECT 
          RTRIM(th.ReferenceNo) AS ReferenceNo,
          ISNULL(RTRIM(th.VendorName), RTRIM(th.VendorCode)) AS VendorName,
          ISNULL(th.TotalPCS, 1) AS TotalPCS,
          ROUND(ISNULL(th.TotalWeight, 0), 3) AS TotalWeight,
          ROUND(ISNULL(th.CostPerGram, 0), 2) AS CostPerGram,
          ROUND(ISNULL(th.Amount, 0), 2) AS Amount
        FROM tblTradeInSalesHeader th WITH (NOLOCK)
        WHERE th.SalesDate >= @FromDate AND th.SalesDate < @ToDateExclusive AND th.LocationCode = @LocationCode
        ORDER BY th.ReferenceNo
      `);
      rows = resQ.recordset;
    }

    // Report 5: Installment Journal
    else if (reportId === "5") {
      title = `Installment Journal - EOD ${targetDateDisp}`;
      columns = [
        { key: "InvoiceNo", label: "Agreement / Inv #", width: 140 },
        { key: "Name", label: "Customer Name", width: 200 },
        { key: "InsAgreementNo", label: "Agreement No", width: 120 },
        { key: "DepositAmt", label: "Deposit", width: 110, align: "right", format: "currency" },
        { key: "InsTotal", label: "Agreement Value", width: 130, align: "right", format: "currency" },
        { key: "AmtPaid", label: "Amount Paid", width: 120, align: "right", format: "currency" }
      ];

      const resQ = await request.query(`
        SELECT 
          RTRIM(ie.InvoiceNo) AS InvoiceNo,
          ISNULL(RTRIM(ie.Name), RTRIM(ie.CustomerCode)) AS Name,
          ISNULL(RTRIM(ie.InsAgreementNo), '-') AS InsAgreementNo,
          ROUND(ISNULL(ie.DepositAmt, 0), 2) AS DepositAmt,
          ROUND(ISNULL(ie.InsTotal, 0), 2) AS InsTotal,
          ROUND(ISNULL(ie.AmtPaid, 0), 2) AS AmtPaid
        FROM tblInstallmentEntryHeader ie WITH (NOLOCK)
        WHERE ie.InvoiceDate >= @FromDate AND ie.InvoiceDate < @ToDateExclusive AND ie.LocationCode = @LocationCode
        ORDER BY ie.InvoiceNo
      `);
      rows = resQ.recordset;
    }

    // Report 6: Closing Stock Report (Pieces) by Category
    else if (reportId === "6") {
      title = `Closing Stock (Pieces) by Category - EOD ${targetDateDisp}`;
      columns = [
        { key: "CategoryCode", label: "Category Code", width: 130 },
        { key: "CategoryName", label: "Category Name", width: 220 },
        { key: "OpeningStock", label: "Opening Pcs", width: 110, align: "right" },
        { key: "PurchaseStock", label: "Pur Pcs", width: 100, align: "right" },
        { key: "SalesStock", label: "Sales Pcs", width: 100, align: "right" },
        { key: "ClosingStock", label: "Closing Pcs", width: 120, align: "right" }
      ];

      const resQ = await request.query(`
        SELECT 
          ISNULL(RTRIM(r.CategoryCode), 'CAT') AS CategoryCode,
          ISNULL(RTRIM(c.CategoryName), RTRIM(r.CategoryCode)) AS CategoryName,
          ISNULL(r.OpeningStock, 0) AS OpeningStock,
          ISNULL(r.PurchaseStock, 0) AS PurchaseStock,
          ISNULL(r.SalesStock, 0) AS SalesStock,
          ISNULL(r.ClosingStock, 0) AS ClosingStock
        FROM tblRptEODCategory r WITH (NOLOCK)
        LEFT JOIN tblCategory c WITH (NOLOCK) ON r.CategoryCode = c.CategoryCode
        WHERE r.EODDate = @FromDate AND r.ComputerName = @ComputerName
        ORDER BY r.CategoryCode
      `);
      rows = resQ.recordset;

      // Fallback if table wasn't populated yet
      if (rows.length === 0) {
        const fbRes = await pool.request().query(`
          SELECT TOP 20 
            RTRIM(c.CategoryCode) AS CategoryCode, 
            RTRIM(c.CategoryName) AS CategoryName,
            COUNT(i.InventoryCode) AS ClosingStock,
            0 AS OpeningStock,
            0 AS PurchaseStock,
            0 AS SalesStock
          FROM tblCategory c WITH (NOLOCK)
          LEFT JOIN tblInventory i WITH (NOLOCK) ON c.CategoryCode = i.CategoryCode
          GROUP BY c.CategoryCode, c.CategoryName
          ORDER BY c.CategoryCode
        `);
        rows = fbRes.recordset;
      }
    }

    // Report 7: Closing Stock Report (Weight) by Category
    else if (reportId === "7") {
      title = `Closing Stock (Weight) by Category - EOD ${targetDateDisp}`;
      columns = [
        { key: "CategoryCode", label: "Category Code", width: 130 },
        { key: "CategoryName", label: "Category Name", width: 220 },
        { key: "OpeningWeight", label: "Opening Wt (g)", width: 130, align: "right", format: "decimal3" },
        { key: "PurchaseWeight", label: "Pur Wt (g)", width: 120, align: "right", format: "decimal3" },
        { key: "SalesWeight", label: "Sales Wt (g)", width: 120, align: "right", format: "decimal3" },
        { key: "ClosingWeight", label: "Closing Wt (g)", width: 130, align: "right", format: "decimal3" }
      ];

      const resQ = await request.query(`
        SELECT 
          ISNULL(RTRIM(r.CategoryCode), 'CAT') AS CategoryCode,
          ISNULL(RTRIM(c.CategoryName), RTRIM(r.CategoryCode)) AS CategoryName,
          ROUND(ISNULL(r.OpeningWeight, 0), 3) AS OpeningWeight,
          ROUND(ISNULL(r.PurchaseWeight, 0), 3) AS PurchaseWeight,
          ROUND(ISNULL(r.SalesWeight, 0), 3) AS SalesWeight,
          ROUND(ISNULL(r.ClosingWeight, 0), 3) AS ClosingWeight
        FROM tblRptEODCategory r WITH (NOLOCK)
        LEFT JOIN tblCategory c WITH (NOLOCK) ON r.CategoryCode = c.CategoryCode
        WHERE r.EODDate = @FromDate AND r.ComputerName = @ComputerName
        ORDER BY r.CategoryCode
      `);
      rows = resQ.recordset;

      if (rows.length === 0) {
        const fbRes = await pool.request().query(`
          SELECT TOP 20 
            RTRIM(c.CategoryCode) AS CategoryCode, 
            RTRIM(c.CategoryName) AS CategoryName,
            ROUND(SUM(ISNULL(i.Weight, 0)), 3) AS ClosingWeight,
            0 AS OpeningWeight,
            0 AS PurchaseWeight,
            0 AS SalesWeight
          FROM tblCategory c WITH (NOLOCK)
          LEFT JOIN tblInventory i WITH (NOLOCK) ON c.CategoryCode = i.CategoryCode
          GROUP BY c.CategoryCode, c.CategoryName
          ORDER BY c.CategoryCode
        `);
        rows = fbRes.recordset;
      }
    }

    // Report 8: Closing Stock Report (Pieces) by Tray
    else if (reportId === "8") {
      title = `Closing Stock (Pieces) by Tray - EOD ${targetDateDisp}`;
      columns = [
        { key: "TrayID", label: "Tray ID", width: 140 },
        { key: "OpeningStock", label: "Opening Pcs", width: 120, align: "right" },
        { key: "PurchaseStock", label: "Pur Pcs", width: 110, align: "right" },
        { key: "SalesStock", label: "Sales Pcs", width: 110, align: "right" },
        { key: "ClosingStock", label: "Closing Pcs", width: 130, align: "right" }
      ];

      const resQ = await request.query(`
        SELECT 
          ISNULL(RTRIM(r.TrayID), '-') AS TrayID,
          ISNULL(r.OpeningStock, 0) AS OpeningStock,
          ISNULL(r.PurchaseStock, 0) AS PurchaseStock,
          ISNULL(r.SalesStock, 0) AS SalesStock,
          ISNULL(r.ClosingStock, 0) AS ClosingStock
        FROM tblRptEODTray r WITH (NOLOCK)
        WHERE r.EODDate = @FromDate AND r.ComputerName = @ComputerName
        ORDER BY r.TrayID
      `);
      rows = resQ.recordset;
    }

    // Report 9: Below Cost Sales Report
    else if (reportId === "9") {
      title = `Below Cost Sales Report - EOD ${targetDateDisp}`;
      columns = [
        { key: "InvoiceNo", label: "Invoice No", width: 140 },
        { key: "InventoryCode", label: "Inventory Code", width: 140 },
        { key: "ActualSales", label: "Sales Price", width: 120, align: "right", format: "currency" },
        { key: "TotalCost", label: "Item Cost", width: 120, align: "right", format: "currency" },
        { key: "LossAmt", label: "Loss Amount", width: 130, align: "right", format: "currency" }
      ];

      const resQ = await request.query(`
        SELECT 
          RTRIM(sh.InvoiceNo) AS InvoiceNo,
          RTRIM(sd.InventoryCode) AS InventoryCode,
          ROUND(ISNULL(sd.ActualSales, 0), 2) AS ActualSales,
          ROUND(ISNULL(sd.TotalCost, 0), 2) AS TotalCost,
          ROUND(ISNULL(sd.TotalCost, 0) - ISNULL(sd.ActualSales, 0), 2) AS LossAmt
        FROM tblSalesHeader sh WITH (NOLOCK)
        INNER JOIN tblSalesDetail sd WITH (NOLOCK) ON sh.InvoiceNo = sd.InvoiceNo
        WHERE sh.InvoiceDate >= @FromDate AND sh.InvoiceDate < @ToDateExclusive 
          AND sh.LocationCode = @LocationCode 
          AND sd.ActualSales < sd.TotalCost
        ORDER BY LossAmt DESC
      `);
      rows = resQ.recordset;
    }

    // Report 10: Closed Bills Listing
    else if (reportId === "10") {
      title = `Closed Bills Listing - EOD ${targetDateDisp}`;
      columns = [
        { key: "InvoiceNo", label: "Invoice No", width: 140 },
        { key: "CustomerName", label: "Customer Name", width: 220 },
        { key: "TotalWeight", label: "Weight (g)", width: 120, align: "right", format: "decimal3" },
        { key: "GST", label: "GST", width: 100, align: "right", format: "currency" },
        { key: "NetTotal", label: "Paid In Full", width: 140, align: "right", format: "currency" }
      ];

      const resQ = await request.query(`
        SELECT 
          RTRIM(sh.InvoiceNo) AS InvoiceNo,
          ISNULL(RTRIM(sh.CustomerName), RTRIM(sh.CustomerCode)) AS CustomerName,
          ROUND(ISNULL(sh.TotalWeight, 0), 3) AS TotalWeight,
          ROUND(ISNULL(sh.GST, 0), 2) AS GST,
          ROUND(ISNULL(sh.NetTotal, 0), 2) AS NetTotal
        FROM tblSalesHeader sh WITH (NOLOCK)
        WHERE sh.InvoiceDate >= @FromDate AND sh.InvoiceDate < @ToDateExclusive 
          AND sh.LocationCode = @LocationCode 
          AND (sh.BalanceAmt IS NULL OR sh.BalanceAmt <= 0)
        ORDER BY sh.InvoiceNo
      `);
      rows = resQ.recordset;
    }

    // Report 11: Purchase Return Journal
    else if (reportId === "11") {
      title = `Purchase Return Journal - EOD ${targetDateDisp}`;
      columns = [
        { key: "ReturnNo", label: "Return No", width: 140 },
        { key: "SupplierCode", label: "Supplier", width: 120 },
        { key: "TotalPCS", label: "PCS", width: 70, align: "right" },
        { key: "TotalWeight", label: "Total Wt (g)", width: 130, align: "right", format: "decimal3" },
        { key: "NetAmount", label: "Net Amount", width: 130, align: "right", format: "currency" }
      ];

      const resQ = await request.query(`
        SELECT 
          RTRIM(pr.ReturnNo) AS ReturnNo,
          RTRIM(pr.SupplierCode) AS SupplierCode,
          ISNULL(pr.TotalPCS, 1) AS TotalPCS,
          ROUND(ISNULL(pr.TotalWeight, 0), 3) AS TotalWeight,
          ROUND(ISNULL(pr.NetAmount, 0), 2) AS NetAmount
        FROM tblPurchaseReturnHeader pr WITH (NOLOCK)
        WHERE pr.ReturnDate >= @FromDate AND pr.ReturnDate < @ToDateExclusive
        ORDER BY pr.ReturnNo
      `);
      rows = resQ.recordset;
    }

    // Report 12: Gold Bar Purchase
    else if (reportId === "12") {
      title = `Gold Bar Purchase Journal - EOD ${targetDateDisp}`;
      columns = [
        { key: "PurchaseNo", label: "Purchase No", width: 140 },
        { key: "SupplierCode", label: "Supplier", width: 120 },
        { key: "Weight", label: "Weight (g)", width: 130, align: "right", format: "decimal3" },
        { key: "Amount", label: "Amount", width: 130, align: "right", format: "currency" }
      ];

      const resQ = await request.query(`
        SELECT 
          RTRIM(gb.JobSheetNo) AS PurchaseNo,
          RTRIM(gb.SupplierCode) AS SupplierCode,
          ROUND(ISNULL(gb.TotalWeight24K, 0), 3) AS Weight,
          ROUND(ISNULL(gb.TotalCost, 0), 2) AS Amount
        FROM tblJobSheetHeader gb WITH (NOLOCK)
        WHERE gb.JobSheetDate >= @FromDate AND gb.JobSheetDate < @ToDateExclusive AND gb.ClassType = 'GB'
        ORDER BY gb.JobSheetNo
      `);
      rows = resQ.recordset;
    }

    // Report 13: Miscellaneous Journal
    else if (reportId === "13") {
      title = `Miscellaneous & Expense Journal - EOD ${targetDateDisp}`;
      columns = [
        { key: "PayOutNo", label: "Voucher No", width: 140 },
        { key: "Description", label: "Category", width: 180 },
        { key: "Amount", label: "Amount", width: 130, align: "right", format: "currency" },
        { key: "Remarks", label: "Remarks", width: 200 }
      ];

      const resQ = await request.query(`
        SELECT 
          RTRIM(po.PayOutNo) AS PayOutNo,
          ISNULL(RTRIM(po.Description), RTRIM(po.Code)) AS Description,
          ROUND(ISNULL(po.Amount, 0), 2) AS Amount,
          ISNULL(RTRIM(po.Remarks), '') AS Remarks
        FROM tblPayOutDetail po WITH (NOLOCK)
        WHERE po.PayOutDate >= @FromDate AND po.PayOutDate < @ToDateExclusive
        ORDER BY po.PayOutNo
      `);
      rows = resQ.recordset;
    }

    // Report 14: Normal Sales Profit
    else if (reportId === "14") {
      title = `Normal Sales Profit Analysis - EOD ${targetDateDisp}`;
      columns = [
        { key: "InvoiceNo", label: "Invoice No", width: 140 },
        { key: "ActualSales", label: "Sales Revenue", width: 130, align: "right", format: "currency" },
        { key: "TotalCost", label: "Cost of Goods", width: 130, align: "right", format: "currency" },
        { key: "Profit", label: "Gross Profit", width: 130, align: "right", format: "currency" },
        { key: "MarginPerc", label: "Margin %", width: 100, align: "right" }
      ];

      const resQ = await request.query(`
        SELECT 
          RTRIM(sh.InvoiceNo) AS InvoiceNo,
          ROUND(SUM(ISNULL(sd.ActualSales, 0)), 2) AS ActualSales,
          ROUND(SUM(ISNULL(sd.TotalCost, 0)), 2) AS TotalCost,
          ROUND(SUM(ISNULL(sd.ActualSales, 0)) - SUM(ISNULL(sd.TotalCost, 0)), 2) AS Profit,
          CASE 
            WHEN SUM(ISNULL(sd.ActualSales, 0)) > 0 
            THEN ROUND(((SUM(ISNULL(sd.ActualSales, 0)) - SUM(ISNULL(sd.TotalCost, 0))) / SUM(ISNULL(sd.ActualSales, 0))) * 100, 1)
            ELSE 0 
          END AS MarginPerc
        FROM tblSalesHeader sh WITH (NOLOCK)
        INNER JOIN tblSalesDetail sd WITH (NOLOCK) ON sh.InvoiceNo = sd.InvoiceNo
        WHERE sh.InvoiceDate >= @FromDate AND sh.InvoiceDate < @ToDateExclusive AND sh.LocationCode = @LocationCode
        GROUP BY sh.InvoiceNo
        ORDER BY Profit DESC
      `);
      rows = resQ.recordset;
    }

    // Calculate totals across rows
    const totals = {
      totalRecords: rows.length,
      totalPcs: 0,
      totalWeight: 0,
      totalAmount: 0
    };

    rows.forEach((r) => {
      if (r.PCS !== undefined) totals.totalPcs += Number(r.PCS) || 0;
      else if (r.TotalPCS !== undefined) totals.totalPcs += Number(r.TotalPCS) || 0;
      else if (r.ClosingStock !== undefined) totals.totalPcs += Number(r.ClosingStock) || 0;

      if (r.Weight !== undefined) totals.totalWeight += Number(r.Weight) || 0;
      else if (r.TotalWeight !== undefined) totals.totalWeight += Number(r.TotalWeight) || 0;
      else if (r.ClosingWeight !== undefined) totals.totalWeight += Number(r.ClosingWeight) || 0;

      if (r.NetAmount !== undefined) totals.totalAmount += Number(r.NetAmount) || 0;
      else if (r.NetTotal !== undefined) totals.totalAmount += Number(r.NetTotal) || 0;
      else if (r.Amount !== undefined) totals.totalAmount += Number(r.Amount) || 0;
      else if (r.ActualSales !== undefined) totals.totalAmount += Number(r.ActualSales) || 0;
    });

    totals.totalWeight = Number(totals.totalWeight.toFixed(3));
    totals.totalAmount = Number(totals.totalAmount.toFixed(2));

    res.json({
      success: true,
      title,
      company,
      eodDate: targetDateDisp,
      columns,
      data: rows,
      totals
    });
  } catch (err) {
    console.error("Error in POST /api/eod/reports/generate:", err);
    res.status(500).json({ success: false, message: err.message });
  }
});

// POST /api/eod/download-html
router.post("/download-html", async (req, res) => {
  try {
    const { generateReportHtml } = require("../utils/reportHtmlTemplate");
    const { title, columns, data, totals, companyInfo, fromDate, toDate, showPrintButton } = req.body;
    const html = await generateReportHtml({
      title: title || "Day End Report",
      columns,
      data,
      totals,
      companyInfo,
      fromDate,
      showPrintButton: showPrintButton ?? false,
      toDate,
      pool: req.pool,
    });
    res.setHeader("Content-Type", "text/html; charset=utf-8");
    res.send(html);
  } catch (err) {
    console.error("EOD Download HTML Error:", err);
    res.status(500).send("Error generating report HTML: " + err.message);
  }
});

// EXPORT EXCEL & PDF ENDPOINTS
router.post("/export-excel", (req, res) => {
  const { handleExportExcel } = require("../utils/reportExportHandlers");
  handleExportExcel({ req, res, defaultTitle: "EOD_Report" });
});

router.post("/export-pdf", (req, res) => {
  const { handleExportPdf } = require("../utils/reportExportHandlers");
  handleExportPdf({ req, res, defaultTitle: "EOD_Report" });
});

module.exports = router;

