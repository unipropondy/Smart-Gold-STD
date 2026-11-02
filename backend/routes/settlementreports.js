const express = require('express');
const router = express.Router();
const { validateBackendDates } = require('../utils/validateReportDates');
const { sql } = require('../db');

// Helper to format date as DD/MM/YYYY
const formatDateDMY = (dateVal) => {
  if (!dateVal) return '';
  const d = new Date(dateVal);
  if (isNaN(d.getTime())) return '';
  const day = String(d.getDate()).padStart(2, '0');
  const month = String(d.getMonth() + 1).padStart(2, '0');
  const year = d.getFullYear();
  return `${day}/${month}/${year}`;
};

// Parse DD/MM/YYYY or YYYY-MM-DD to Date object
const parseInputDate = (dStr) => {
  if (!dStr) return null;
  if (typeof dStr === 'string' && dStr.includes('/')) {
    const parts = dStr.trim().split('/');
    if (parts.length === 3) {
      const day = parseInt(parts[0], 10);
      const month = parseInt(parts[1], 10) - 1;
      const year = parseInt(parts[2], 10);
      const dt = new Date(year, month, day);
      if (!isNaN(dt.getTime()) && dt.getDate() === day && dt.getMonth() === month) {
        return dt;
      }
    }
    return null;
  }
  const dt = new Date(dStr);
  return isNaN(dt.getTime()) ? null : dt;
};

// ==========================================
// 1. GET LOCATIONS
// GET /api/settlementreports/locations
// ==========================================
router.get('/locations', async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query(`
      SELECT 
        RTRIM(LocationCode) AS LocationCode,
        RTRIM(LocationName) AS LocationName
      FROM tblLocation
      ORDER BY SortCode, LocationCode
    `);

    res.json({
      success: true,
      locations: result.recordset
    });
  } catch (err) {
    console.error('Error fetching settlement locations:', err);
    res.status(500).json({
      success: false,
      message: 'Failed to fetch locations: ' + (err.message || 'Database error')
    });
  }
});

// ==========================================
// 2. GENERATE SETTLEMENT REPORT
// POST /api/settlementreports/generate
// ==========================================
router.post('/generate', async (req, res) => {
  try {
    const pool = req.pool;
    const {
      settlementType = 'computer', // 'computer' | 'manual'
      location = 'ALL',
      fromDate,
      toDate
    } = req.body;

    // 1. Validate dates
    const dFrom = parseInputDate(fromDate);
    if (!dFrom) {
      return res.status(400).json({
        success: false,
        message: 'Enter Valid From Date..'
      });
    }

    const dateVal = validateBackendDates(fromDate, toDate);
    if (!dateVal.isValid) {
      return res.status(400).json({ success: false, message: dateVal.message });
    }

    const dThru = parseInputDate(toDate);
    if (!dThru) {
      return res.status(400).json({
        success: false,
        message: 'Enter Valid To Date..'
      });
    }

    // Set time boundaries: From date 00:00:00.000, To date 23:59:59.997
    const fromStart = new Date(dFrom.getFullYear(), dFrom.getMonth(), dFrom.getDate(), 0, 0, 0, 0);
    const toEnd = new Date(dThru.getFullYear(), dThru.getMonth(), dThru.getDate(), 23, 59, 59, 997);

    if (toEnd < fromStart) {
      return res.status(400).json({
        success: false,
        message: 'To Date is lesser then From Date'
      });
    }

    // 2. Fetch Company Master info
    const compResult = await pool.request().query(`
      SELECT TOP 1 
        RTRIM(Name) AS Name,
        RTRIM(Address1) AS Address1,
        RTRIM(Address2) AS Address2,
        RTRIM(City) AS City,
        RTRIM(State) AS State,
        RTRIM(Country) AS Country,
        RTRIM(PostalCode) AS PostalCode,
        RTRIM(Phone) AS Phone,
        RTRIM(CurrencySign) AS CurrencySign
      FROM tblCompany
    `);
    const company = compResult.recordset[0] || {
      Name: 'SMART GOLD',
      Address1: '',
      Address2: '',
      City: '',
      Phone: '',
      CurrencySign: 'SGD'
    };

    // 3. Execute sp_rptTerminalSalesLoc (@DFrom, @DThru)
    const spReq = pool.request();
    spReq.input('DFrom', sql.DateTime, fromStart);
    spReq.input('DThru', sql.DateTime, toEnd);
    await spReq.execute('sp_rptTerminalSalesLoc');

    // 4. Query tblRptTerminalSalesLoc
    let query = `
      SELECT 
        r.Date,
        RTRIM(r.LocationCode) AS LocationCode,
        RTRIM(l.LocationName) AS LocationName,
        RTRIM(r.PayMode1) AS PayMode1,
        ISNULL(r.CAmount1, 0) AS CAmount1,
        ISNULL(r.MAmount1, 0) AS MAmount1,
        RTRIM(r.PayMode2) AS PayMode2,
        ISNULL(r.CAmount2, 0) AS CAmount2,
        ISNULL(r.MAmount2, 0) AS MAmount2,
        RTRIM(r.PayMode3) AS PayMode3,
        ISNULL(r.CAmount3, 0) AS CAmount3,
        ISNULL(r.MAmount3, 0) AS MAmount3,
        RTRIM(r.PayMode4) AS PayMode4,
        ISNULL(r.CAmount4, 0) AS CAmount4,
        ISNULL(r.MAmount4, 0) AS MAmount4,
        RTRIM(r.PayMode5) AS PayMode5,
        ISNULL(r.CAmount5, 0) AS CAmount5,
        ISNULL(r.MAmount5, 0) AS MAmount5,
        RTRIM(r.PayMode6) AS PayMode6,
        ISNULL(r.CAmount6, 0) AS CAmount6,
        ISNULL(r.MAmount6, 0) AS MAmount6,
        RTRIM(r.PayMode7) AS PayMode7,
        ISNULL(r.CAmount7, 0) AS CAmount7,
        ISNULL(r.MAmount7, 0) AS MAmount7,
        RTRIM(r.PayMode8) AS PayMode8,
        ISNULL(r.CAmount8, 0) AS CAmount8,
        ISNULL(r.MAmount8, 0) AS MAmount8,
        RTRIM(r.PayMode9) AS PayMode9,
        ISNULL(r.CAmount9, 0) AS CAmount9,
        ISNULL(r.MAmount9, 0) AS MAmount9,
        RTRIM(r.PayMode10) AS PayMode10,
        ISNULL(r.CAmount10, 0) AS CAmount10,
        ISNULL(r.MAmount10, 0) AS MAmount10,
        ISNULL(r.TotalSales, 0) AS TotalSales,
        ISNULL(r.ExcessOrShortage, 0) AS ExcessOrShortage
      FROM tblRptTerminalSalesLoc r
      LEFT JOIN tblLocation l ON RTRIM(r.LocationCode) = RTRIM(l.LocationCode)
    `;

    const locCode = (location || '').trim();
    if (locCode && locCode !== 'ALL') {
      query += ` WHERE RTRIM(r.LocationCode) = @LocationCode `;
    }
    
    query += ` ORDER BY r.Date, r.LocationCode `;

    const qReq = pool.request();
    if (locCode && locCode !== 'ALL') {
      qReq.input('LocationCode', sql.VarChar, locCode);
    }
    const reportData = await qReq.query(query);

    // 5. Query active PayMode list for column headers (up to 10 paymodes)
    const payModesRes = await pool.request().query(`
      SELECT TOP 10 RTRIM(PayMode) AS PayMode, RTRIM(Description) AS Description, Position 
      FROM tblPayMode 
      ORDER BY Position
    `);
    const activePayModes = payModesRes.recordset.map((p, idx) => ({
      index: idx + 1,
      code: p.PayMode,
      label: p.Description || p.PayMode
    }));

    // 6. Process rows and determine dynamic amounts based on settlementType ('computer' vs 'manual')
    const isManual = settlementType.toLowerCase() === 'manual';
    const amountPrefix = isManual ? 'MAmount' : 'CAmount';

    const rows = reportData.recordset.map((row) => {
      let rowTotal = 0;
      const amounts = {};

      for (let i = 1; i <= 10; i++) {
        const amt = Number(row[`${amountPrefix}${i}`]) || 0;
        amounts[`amount${i}`] = amt;
        rowTotal += amt;
      }

      return {
        date: formatDateDMY(row.Date),
        rawDate: row.Date,
        locationCode: row.LocationCode,
        locationName: row.LocationName || row.LocationCode,
        amounts,
        totalSales: Number(row.TotalSales) || rowTotal,
        excessOrShortage: Number(row.ExcessOrShortage) || 0
      };
    });

    // 7. Calculate column totals
    const columnTotals = {};
    for (let i = 1; i <= 10; i++) {
      columnTotals[`amount${i}`] = 0;
    }
    let grandTotal = 0;

    rows.forEach((r) => {
      for (let i = 1; i <= 10; i++) {
        columnTotals[`amount${i}`] += r.amounts[`amount${i}`];
      }
      grandTotal += r.totalSales;
    });

    res.json({
      success: true,
      reportTitle: 'Outlet Sales By Credit Card',
      settlementType: isManual ? 'Manual Settlement' : 'Computer Settlement',
      fromDate: formatDateDMY(fromStart),
      toDate: formatDateDMY(toEnd),
      company,
      payModes: activePayModes,
      data: rows,
      columnTotals,
      grandTotal
    });
  } catch (err) {
    console.error('Error generating settlement report:', err);
    res.status(500).json({
      success: false,
      message: 'Failed to generate settlement report: ' + (err.message || 'Database error')
    });
  }
});

// POST /api/settlementreports/download-html
router.post("/download-html", async (req, res) => {
  try {
    const { generateReportHtml } = require("../utils/reportHtmlTemplate");
    const { title, columns, data, totals, companyInfo, fromDate, toDate, showPrintButton } = req.body;
    const html = await generateReportHtml({
      title: title || "Settlement Report",
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
    console.error("Settlement Reports Download HTML Error:", err);
    res.status(500).send("Error generating report HTML: " + err.message);
  }
});

// EXPORT EXCEL & PDF ENDPOINTS
router.post("/export-excel", (req, res) => {
  const { handleExportExcel } = require("../utils/reportExportHandlers");
  handleExportExcel({ req, res, defaultTitle: "Settlement_Report" });
});

router.post("/export-pdf", (req, res) => {
  const { handleExportPdf } = require("../utils/reportExportHandlers");
  handleExportPdf({ req, res, defaultTitle: "Settlement_Report" });
});

module.exports = router;


