const express = require("express");
const router = express.Router();
const { validateBackendDates } = require("../utils/validateReportDates");

router.post("/report", async (req, res) => {
  try {
    const {
      ReportOption, // 'Trade In Merge' or 'Trade In Issue'
      ReportType,   // 'Summary' or 'Detail'
      PrintOption,  // 'Preview' or 'Print'
      DateFrom,     // YYYY-MM-DD
      DateTo,       // YYYY-MM-DD
      IsPdfDownload // boolean flag if direct pdf trigger is requested
    } = req.body;

    const dateVal = validateBackendDates(DateFrom, DateTo);
    if (!dateVal.isValid) {
      return res.status(400).send(`<html><body style="font-family:sans-serif;padding:30px;color:#c00;"><h3>Invalid Date Range</h3><p>${dateVal.message}</p></body></html>`);
    }

    const pool = req.pool;

    // Dates formatted for display (DD/MM/YYYY)
    const formatDisplayDate = (dStr) => {
      if (!dStr) return '';
      const parts = dStr.split('-');
      if (parts.length === 3) return `${parts[2]}/${parts[1]}/${parts[0]}`;
      return dStr;
    };

    const sPrintFromDate = formatDisplayDate(DateFrom);
    const sPrintToDate = formatDisplayDate(DateTo);

    const safeDateFrom = DateFrom ? DateFrom.replace(/-/g, '') : '';
    const safeDateTo = DateTo ? DateTo.replace(/-/g, '') : '';

    // Fetch company info
    let companyName = "SMART GOLD";
    let companyAddress = "";
    let companyPhone = "";
    let companyFax = "";
    let companyShortCode = "";

    try {
      const companyResult = await pool.request().query("SELECT TOP 1 * FROM tblCompany");
      if (companyResult.recordset.length > 0) {
        const comp = companyResult.recordset[0];
        companyName = (comp.Name || companyName).trim();
        const addrParts = [];
        if (comp.Address1) addrParts.push(comp.Address1.trim());
        if (comp.Address2) addrParts.push(comp.Address2.trim());
        if (comp.City) addrParts.push(comp.City.trim());
        if (comp.State && comp.State.trim() !== comp.City?.trim()) addrParts.push(comp.State.trim());
        if (comp.Country && comp.Country.trim() !== comp.City?.trim() && comp.Country.trim() !== comp.State?.trim()) addrParts.push(comp.Country.trim());
        companyAddress = addrParts.join(" ");
        companyPhone = (comp.Phone || "").trim();
        companyFax = (comp.Fax || "").trim();
        companyShortCode = (comp.CompanyShortCode || comp.ShortCode || "").trim().toUpperCase();
      }
    } catch (e) {
      console.error("Failed to fetch company info:", e);
    }

    let reportTitle = "";
    let query = "";
    const isSummary = ReportType === "Summary";

    // 1. Trade In Merge Reports
    if (ReportOption === "Trade In Merge") {
      let dateFilter = "";
      if (safeDateFrom && safeDateTo) {
        dateFilter = `WHERE h.CreateDate >= '${safeDateFrom} 00:00:00' AND h.CreateDate <= '${safeDateTo} 23:59:59'`;
      } else if (safeDateFrom) {
        dateFilter = `WHERE h.CreateDate >= '${safeDateFrom} 00:00:00'`;
      }

      if (isSummary) {
        reportTitle = `Trade MergeInventory Report Summary From ${sPrintFromDate} To ${sPrintToDate}`;
        query = `
          SELECT 
            h.TradeInInventoryCode AS HeaderCode, 
            d.TradeInInventoryCode AS DetailCode, 
            d.TradeInWeight, 
            d.TradeInStoneWeight, 
            h.CreateDate, 
            h.Purity
          FROM tblTradeMergeInventoryHeader h 
          INNER JOIN tblTradeMergeInventoryDetail d 
            ON h.TradeInInventoryCode = d.TradeInMergeInventoryCode
          ${dateFilter}
          ORDER BY h.TradeInInventoryCode
        `;
      } else {
        reportTitle = `Trade MergeInventory Report Detail From ${sPrintFromDate} To ${sPrintToDate}`;
        query = `
          SELECT 
            h.TradeInInventoryCode AS HeaderCode, 
            d.TradeInInventoryCode AS DetailCode, 
            d.TradeInWeight, 
            d.TradeInStoneWeight, 
            h.CreateDate, 
            h.Purity
          FROM tblTradeMergeInventoryHeader h 
          INNER JOIN tblTradeMergeInventoryDetail d 
            ON h.TradeInInventoryCode = d.TradeInMergeInventoryCode
          ${dateFilter}
          ORDER BY h.TradeInInventoryCode
        `;
      }
    } 
    // 2. Trade In Issue Reports
    else {
      reportTitle = `Trade In Issue Report From ${sPrintFromDate} To ${sPrintToDate}`;

      if (companyShortCode === "HN") {
        let dateFilter = "";
        if (safeDateFrom && safeDateTo) {
          dateFilter = `WHERE h.ModifyDate >= '${safeDateFrom} 00:00:00' AND h.ModifyDate <= '${safeDateTo} 23:59:59'`;
        } else if (safeDateFrom) {
          dateFilter = `WHERE h.ModifyDate >= '${safeDateFrom} 00:00:00'`;
        }

        query = `
          SELECT 
            s.NRICNo, 
            s.SupplierName, 
            s.Address1, 
            s.Address2, 
            s.Address4, 
            s.Gender, 
            s.Remarks, 
            d.Class, 
            d.InventoryCode, 
            d.Description, 
            d.Weight, 
            d.TotalCost, 
            h.ModifyDate, 
            s.DOB, 
            s.PassportNo
          FROM tblTradeIssueHistory hist
          INNER JOIN tblSupplier s ON hist.SupplierCode = s.SupplierCode
          INNER JOIN tblTradeInDetail d ON hist.InventoryCode = d.InventoryCode
          INNER JOIN tblTradeInHeader h ON hist.InventoryCode = h.InventoryCode
          ${dateFilter}
          ORDER BY h.ModifyDate
        `;
      } else if (companyShortCode === "RJ") {
        let dateFilter = "";
        if (safeDateFrom && safeDateTo) {
          dateFilter = `WHERE hist.CreateDate >= '${safeDateFrom} 00:00:00' AND hist.CreateDate <= '${safeDateTo} 23:59:59'`;
        } else if (safeDateFrom) {
          dateFilter = `WHERE hist.CreateDate >= '${safeDateFrom} 00:00:00'`;
        }

        query = `
          SELECT 
            s.SupplierName, 
            hdr.TradeIssueNo, 
            hdr.TradeIssueDate, 
            hdr.TotalPrice, 
            hdr.TotalWeight
          FROM tblTradeIssueHistoryHeader hdr
          INNER JOIN tblSupplier s ON hdr.SupplierCode = s.SupplierCode
          ${dateFilter ? dateFilter.replace(/hist\.CreateDate/g, 'hdr.TradeIssueDate') : ''}
          ORDER BY s.SupplierName
        `;
      } else {
        // Standard / Default (AJ or other)
        let dateFilter = "";
        if (safeDateFrom && safeDateTo) {
          dateFilter = `WHERE hdr.TradeIssueDate >= '${safeDateFrom} 00:00:00' AND hdr.TradeIssueDate <= '${safeDateTo} 23:59:59'`;
        } else if (safeDateFrom) {
          dateFilter = `WHERE hdr.TradeIssueDate >= '${safeDateFrom} 00:00:00'`;
        }

        // Check if tblTradeIssueHistoryHeader exists, fallback to tblTradeInHeader if needed
        query = `
          SELECT 
            s.SupplierName, 
            hdr.TradeIssueNo, 
            hdr.TradeIssueDate, 
            hdr.TotalPrice, 
            hdr.TotalWeight
          FROM tblTradeIssueHistoryHeader hdr
          INNER JOIN tblSupplier s ON hdr.SupplierCode = s.SupplierCode
          ${dateFilter}
          ORDER BY s.SupplierName
        `;
      }
    }

    let records = [];
    try {
      const dbResult = await pool.request().query(query);
      records = dbResult.recordset;
    } catch (e) {
      console.error("TradeIn Query Error, attempting fallback query:", e.message);
      // Fallback for Trade In Issue if tblTradeIssueHistoryHeader is not populated yet or differs
      if (ReportOption === "Trade In Issue") {
        try {
          const fallbackFilter = (safeDateFrom && safeDateTo) 
            ? `AND h.ModifyDate >= '${safeDateFrom} 00:00:00' AND h.ModifyDate <= '${safeDateTo} 23:59:59'`
            : '';
          const fallbackQuery = `
            SELECT 
              s.SupplierName, 
              h.TradeIssueNo, 
              h.ModifyDate AS TradeIssueDate, 
              h.Amount AS TotalPrice, 
              h.IssuedWeight AS TotalWeight,
              h.InvoiceNo,
              h.InventoryCode
            FROM tblTradeInHeader h
            LEFT JOIN tblSupplier s ON h.SupplierCode = s.SupplierCode
            WHERE (h.IssueFlag = 'Y' OR h.TradeIssueNo IS NOT NULL) ${fallbackFilter}
            ORDER BY s.SupplierName, h.TradeIssueNo
          `;
          const fbRes = await pool.request().query(fallbackQuery);
          records = fbRes.recordset;
        } catch (fbErr) {
          return res.send(`<html><body><h2>SQL Execution Error</h2><p><strong>Error:</strong> ${e.message}</p><p><strong>Query:</strong> ${query}</p></body></html>`);
        }
      } else {
        return res.send(`<html><body><h2>SQL Execution Error</h2><p><strong>Error:</strong> ${e.message}</p><p><strong>Query:</strong> ${query}</p></body></html>`);
      }
    }

    // Format Report Body
    let reportBody = "";

    if (records.length === 0) {
      reportBody = `<div style="text-align: center; padding-top: 30px; font-size: 13px; color: #555;">No records found for the selected date range.</div>`;
    } else if (ReportOption === "Trade In Merge") {
      // Group by HeaderCode
      const groups = {};
      let grandTotalWeight = 0;
      let grandTotalStoneWeight = 0;

      records.forEach(r => {
        const hCode = r.HeaderCode || "N/A";
        if (!groups[hCode]) {
          groups[hCode] = {
            headerCode: hCode,
            createDate: r.CreateDate ? new Date(r.CreateDate).toLocaleDateString('en-GB') : '',
            purity: r.Purity || '',
            items: [],
            subTotalWeight: 0,
            subTotalStoneWeight: 0
          };
        }
        const w = parseFloat(r.TradeInWeight) || 0;
        const sw = parseFloat(r.TradeInStoneWeight) || 0;
        groups[hCode].items.push(r);
        groups[hCode].subTotalWeight += w;
        groups[hCode].subTotalStoneWeight += sw;
        grandTotalWeight += w;
        grandTotalStoneWeight += sw;
      });

      for (const [hCode, g] of Object.entries(groups)) {
        reportBody += `
          <div style="margin-top: 15px; margin-bottom: 4px; font-weight: bold; font-size: 12px;">
            <span>Merge Inventory Code: ${hCode}</span> &nbsp;&nbsp;&nbsp;&nbsp; 
            <span>Date: ${g.createDate}</span> &nbsp;&nbsp;&nbsp;&nbsp; 
            <span>Purity: ${g.purity}</span>
          </div>
          <table>
            <thead>
              <tr>
                <th style="width: 35%;">TRADE-IN ITEM CODE</th>
                <th class="right" style="width: 30%;">TOTAL WT. (G)</th>
                <th class="right" style="width: 35%;">STONE WT. (G)</th>
              </tr>
            </thead>
            <tbody>
        `;

        g.items.forEach(item => {
          const w = (parseFloat(item.TradeInWeight) || 0).toFixed(3);
          const sw = (parseFloat(item.TradeInStoneWeight) || 0).toFixed(3);
          reportBody += `
            <tr>
              <td>${item.DetailCode || ''}</td>
              <td class="right">${w}</td>
              <td class="right">${sw}</td>
            </tr>
          `;
        });

        reportBody += `
            </tbody>
            <tfoot>
              <tr style="border-top: 1px solid #333; font-weight: bold;">
                <td>Subtotal for ${hCode}:</td>
                <td class="right">${g.subTotalWeight.toFixed(3)}</td>
                <td class="right">${g.subTotalStoneWeight.toFixed(3)}</td>
              </tr>
            </tfoot>
          </table>
          <div style="height: 10px;"></div>
        `;
      }

      reportBody += `
        <div style="border-top: 2px solid black; margin-top: 20px; padding-top: 8px;">
          <table style="width: 100%; border: none;">
            <tr style="font-weight: bold; font-size: 13px;">
              <td style="width: 35%; border: none;">Grand Total:</td>
              <td class="right" style="width: 30%; border: none;">${grandTotalWeight.toFixed(3)} g</td>
              <td class="right" style="width: 35%; border: none;">${grandTotalStoneWeight.toFixed(3)} g</td>
            </tr>
          </table>
        </div>
      `;
    } else {
      // Trade In Issue Report
      let grandTotalWeight = 0;
      let grandTotalPrice = 0;

      // Check if it's the detailed HN schema or header summary
      const hasDetailedColumns = records[0] && (records[0].Class !== undefined || records[0].Description !== undefined);

      if (hasDetailedColumns) {
        reportBody += `
          <table>
            <thead>
              <tr>
                <th style="width: 15%;">SUPPLIER</th>
                <th style="width: 12%;">CLASS</th>
                <th style="width: 15%;">INV. CODE</th>
                <th style="width: 26%;">DESC.</th>
                <th class="right" style="width: 16%;">TOTAL WT. (G)</th>
                <th class="right" style="width: 16%;">TOTAL COST</th>
              </tr>
            </thead>
            <tbody>
        `;
        records.forEach(r => {
          const w = parseFloat(r.Weight) || 0;
          const cost = parseFloat(r.TotalCost) || 0;
          grandTotalWeight += w;
          grandTotalPrice += cost;

          reportBody += `
            <tr>
              <td>${r.SupplierName || ''}</td>
              <td>${r.Class || ''}</td>
              <td>${r.InventoryCode || ''}</td>
              <td>${r.Description || ''}</td>
              <td class="right">${w.toFixed(3)}</td>
              <td class="right">${cost.toFixed(2)}</td>
            </tr>
          `;
        });
        reportBody += `
            </tbody>
            <tfoot>
              <tr style="border-top: 2px solid black; font-weight: bold;">
                <td colspan="4">Grand Total:</td>
                <td class="right">${grandTotalWeight.toFixed(3)}</td>
                <td class="right">${grandTotalPrice.toFixed(2)}</td>
              </tr>
            </tfoot>
          </table>
        `;
      } else {
        // Group by SupplierName
        const groups = {};
        records.forEach(r => {
          const sName = r.SupplierName || 'Unknown Supplier';
          if (!groups[sName]) groups[sName] = [];
          groups[sName].push(r);
        });

        for (const [sName, sRecords] of Object.entries(groups)) {
          let subWeight = 0;
          let subPrice = 0;

          reportBody += `
            <div style="margin-top: 15px; margin-bottom: 5px; font-weight: bold; font-size: 13px;">
              Supplier: ${sName}
            </div>
            <table>
              <thead>
                <tr>
                  <th style="width: 25%;">TRADE ISSUE NO.</th>
                  <th style="width: 25%;">ISSUE DATE</th>
                  <th class="right" style="width: 25%;">TOTAL WT. (G)</th>
                  <th class="right" style="width: 25%;">TOTAL PRICE</th>
                </tr>
              </thead>
              <tbody>
          `;

          sRecords.forEach(r => {
            const w = parseFloat(r.TotalWeight) || 0;
            const p = parseFloat(r.TotalPrice) || 0;
            subWeight += w;
            subPrice += p;
            grandTotalWeight += w;
            grandTotalPrice += p;

            const dStr = r.TradeIssueDate ? new Date(r.TradeIssueDate).toLocaleDateString('en-GB') : '';

            reportBody += `
              <tr>
                <td>${r.TradeIssueNo || 'N/A'}</td>
                <td>${dStr}</td>
                <td class="right">${w.toFixed(3)}</td>
                <td class="right">${p.toFixed(2)}</td>
              </tr>
            `;
          });

          reportBody += `
              </tbody>
              <tfoot>
                <tr style="border-top: 1px solid #333; font-weight: bold;">
                  <td colspan="2">Subtotal for ${sName}:</td>
                  <td class="right">${subWeight.toFixed(3)}</td>
                  <td class="right">${subPrice.toFixed(2)}</td>
                </tr>
              </tfoot>
            </table>
            <div style="height: 10px;"></div>
          `;
        }

        reportBody += `
          <div style="border-top: 2px solid black; margin-top: 15px; padding-top: 8px;">
            <table style="width: 100%; border: none;">
              <tr style="font-weight: bold; font-size: 13px;">
                <td style="width: 50%; border: none;">Grand Total:</td>
                <td class="right" style="width: 25%; border: none;">${grandTotalWeight.toFixed(3)} g</td>
                <td class="right" style="width: 25%; border: none;">${grandTotalPrice.toFixed(2)}</td>
              </tr>
            </table>
          </div>
        `;
      }
    }

    const html = `
    <!DOCTYPE html>
    <html>
      <head>
        <meta charset="utf-8" />
        <title>${reportTitle}</title>
        <style>
          @page {
            size: A4 portrait;
            margin: 12mm 10mm;
          }
          body { 
            font-family: 'Courier New', Courier, monospace; 
            font-size: 12px; 
            color: #111;
            padding: 20px; 
            margin: 0;
            background: #fff;
          }
          .header { 
            display: flex; 
            justify-content: space-between; 
            font-size: 12px; 
            margin-bottom: 15px;
            border-bottom: 1px solid #ccc;
            padding-bottom: 10px;
          }
          .title { 
            font-weight: bold; 
            font-size: 15px; 
            margin-top: 10px; 
            margin-bottom: 12px;
            text-align: center;
            text-transform: uppercase;
            letter-spacing: 0.5px;
          }
          table { 
            width: 100%; 
            border-collapse: collapse; 
            margin-top: 6px;
          }
          th { white-space: nowrap !important; text-transform: uppercase !important; border-top: 2px solid #000; 
            border-bottom: 2px solid #000; 
            padding: 6px 4px; 
            text-align: left; 
            font-size: 12px;
            font-weight: bold;
          }
          td { 
            padding: 5px 4px; 
            text-align: left; 
            font-size: 12px;
          }
          .right { text-align: right; }
          .bold { font-weight: bold; }
          @media print {
            body { padding: 0; }
          }
        </style>
      </head>
      <body>
        <div class="header">
          <div style="flex: 1;"></div>
          <div style="text-align: center; flex: 2;">
            <strong style="font-size: 15px;">${companyName}</strong><br/>
            <span>${companyAddress}</span><br/>
            <span>Phone: ${companyPhone || ' - '} &nbsp;&nbsp;&nbsp;&nbsp; Fax: ${companyFax || ' - '}</span>
          </div>
          <div style="flex: 1; display: flex; justify-content: flex-end;">
            <table style="width: auto; border: none; margin: 0;">
              <tr>
                <td style="text-align: right; border: none; padding: 0 4px 2px 0;">Date</td>
                <td style="border: none; padding: 0 4px 2px 0;">:</td>
                <td style="text-align: right; border: none; padding: 0 0 2px 0;">${new Date().toLocaleDateString('en-GB')}</td>
              </tr>
              <tr>
                <td style="text-align: right; border: none; padding: 0 4px 2px 0;">Time</td>
                <td style="border: none; padding: 0 4px 2px 0;">:</td>
                <td style="text-align: right; border: none; padding: 0 0 2px 0;">${new Date().toLocaleTimeString('en-GB')}</td>
              </tr>
              <tr>
                <td style="text-align: right; border: none; padding: 0 4px 0 0;">Page</td>
                <td style="border: none; padding: 0 4px 0 0;">:</td>
                <td style="text-align: right; border: none; padding: 0;">1</td>
              </tr>
            </table>
          </div>
        </div>

        <div class="title">
          ${reportTitle}
        </div>

        ${reportBody}

        <script>
          ${PrintOption === 'Print' || IsPdfDownload ? 'window.onload = function() { window.print(); };' : ''}
        </script>
      </body>
    </html>
    `;

    res.send(html);
  } catch (err) {
    console.error("Generate TradeIn Report Error:", err);
    res.status(500).send("Server error generating Trade-In report.");
  }
});

// POST /api/tradeinreports/generate
router.post("/generate", async (req, res) => {
  try {
    const { ReportOption, ReportType = 'Summary', filters = {} } = req.body;
    const { fromDate, toDate } = filters;

    const dateVal = validateBackendDates(fromDate, toDate);
    if (!dateVal.isValid) {
      return res.status(400).json({ success: false, message: dateVal.message });
    }

    const pool = req.pool;

    const toDbDate = (dStr, isEnd = false) => {
      if (!dStr) return '';
      if (typeof dStr === 'string' && dStr.includes('/')) {
        const parts = dStr.split('/');
        if (parts.length === 3) {
          return `${parts[2]}-${parts[1].padStart(2, '0')}-${parts[0].padStart(2, '0')}${isEnd ? ' 23:59:59' : ' 00:00:00'}`;
        }
      }
      return `${dStr}${isEnd ? ' 23:59:59' : ' 00:00:00'}`;
    };

    const safeDateFrom = toDbDate(fromDate, false);
    const safeDateTo = toDbDate(toDate, true);

    let query = "";
    let reportTitle = "";

    if (ReportOption === "Trade In Merge") {
      let dateFilter = "";
      if (safeDateFrom && safeDateTo) {
        dateFilter = `WHERE h.CreateDate >= '${safeDateFrom}' AND h.CreateDate <= '${safeDateTo}'`;
      } else if (safeDateFrom) {
        dateFilter = `WHERE h.CreateDate >= '${safeDateFrom}'`;
      }

      reportTitle = `Trade Merge Inventory Report (${ReportType}) From ${fromDate || ''} To ${toDate || ''}`;
      query = `
        SELECT 
          RTRIM(h.TradeInInventoryCode) AS HeaderCode, 
          RTRIM(d.TradeInInventoryCode) AS DetailCode, 
          ISNULL(d.TradeInWeight, 0) AS TradeInWeight, 
          ISNULL(d.TradeInStoneWeight, 0) AS TradeInStoneWeight, 
          h.CreateDate, 
          RTRIM(h.Purity) AS Purity
        FROM tblTradeMergeInventoryHeader h 
        INNER JOIN tblTradeMergeInventoryDetail d 
          ON h.TradeInInventoryCode = d.TradeInMergeInventoryCode
        ${dateFilter}
        ORDER BY h.TradeInInventoryCode
      `;
    } else {
      reportTitle = `Trade In Issue Report From ${fromDate || ''} To ${toDate || ''}`;
      let dateFilter = "";
      if (safeDateFrom && safeDateTo) {
        dateFilter = `WHERE hdr.TradeIssueDate >= '${safeDateFrom}' AND hdr.TradeIssueDate <= '${safeDateTo}'`;
      } else if (safeDateFrom) {
        dateFilter = `WHERE hdr.TradeIssueDate >= '${safeDateFrom}'`;
      }

      query = `
        SELECT 
          RTRIM(s.SupplierName) AS SupplierName, 
          RTRIM(hdr.TradeIssueNo) AS TradeIssueNo, 
          hdr.TradeIssueDate, 
          ISNULL(hdr.TotalPrice, 0) AS TotalPrice, 
          ISNULL(hdr.TotalWeight, 0) AS TotalWeight
        FROM tblTradeIssueHistoryHeader hdr
        INNER JOIN tblSupplier s ON hdr.SupplierCode = s.SupplierCode
        ${dateFilter}
        ORDER BY s.SupplierName
      `;
    }

    let records = [];
    try {
      const dbResult = await pool.request().query(query);
      records = dbResult.recordset || [];
    } catch (e) {
      if (ReportOption !== "Trade In Merge") {
        const fallbackFilter = (safeDateFrom && safeDateTo)
          ? `AND h.ModifyDate >= '${safeDateFrom}' AND h.ModifyDate <= '${safeDateTo}'`
          : '';
        const fallbackQuery = `
          SELECT 
            RTRIM(s.SupplierName) AS SupplierName, 
            RTRIM(h.TradeIssueNo) AS TradeIssueNo, 
            h.ModifyDate AS TradeIssueDate, 
            ISNULL(h.Amount, 0) AS TotalPrice, 
            ISNULL(h.IssuedWeight, 0) AS TotalWeight,
            RTRIM(h.InvoiceNo) AS InvoiceNo,
            RTRIM(h.InventoryCode) AS InventoryCode
          FROM tblTradeInHeader h
          LEFT JOIN tblSupplier s ON h.SupplierCode = s.SupplierCode
          WHERE (h.IssueFlag = 'Y' OR h.TradeIssueNo IS NOT NULL) ${fallbackFilter}
          ORDER BY s.SupplierName, h.TradeIssueNo
        `;
        const fbRes = await pool.request().query(fallbackQuery);
        records = fbRes.recordset || [];
      } else {
        throw e;
      }
    }

    let totalWeight = 0;
    let totalPrice = 0;
    records.forEach(r => {
      totalWeight += Number(r.TotalWeight || r.TradeInWeight || 0);
      totalPrice += Number(r.TotalPrice || 0);
    });

    const columns = records.length > 0
      ? Object.keys(records[0]).map(k => ({ key: k, label: k }))
      : [{ key: "Result", label: "Result" }];

    res.json({
      success: true,
      title: reportTitle,
      data: records,
      columns,
      totals: {
        totalRecords: records.length,
        totalItems: records.length,
        totalWeight: Number(totalWeight.toFixed(3)),
        totalCost: Number(totalPrice.toFixed(2)),
        totalAmount: Number(totalPrice.toFixed(2))
      }
    });
  } catch (err) {
    console.error("Trade-In Generate Error:", err);
    res.status(500).json({ success: false, message: err.message });
  }
});

// POST /api/tradeinreports/download-html
router.post("/download-html", async (req, res) => {
  try {
    const { generateReportHtml } = require("../utils/reportHtmlTemplate");
    const { title, columns, data, totals, companyInfo, fromDate, toDate, showPrintButton } = req.body;
    const html = await generateReportHtml({
      title: title || "Trade-In Report",
      columns,
      data,
      totals,
      companyInfo,
      fromDate,
      toDate,
      pool: req.pool,
      showPrintButton: showPrintButton ?? false,
    });
    res.setHeader("Content-Type", "text/html; charset=utf-8");
    res.send(html);
  } catch (err) {
    console.error("Trade-In Reports Download HTML Error:", err);
    res.status(500).send("Error generating report HTML: " + err.message);
  }
});

// POST /api/tradeinreports/export-excel
router.post("/export-excel", (req, res) => {
  const { handleExportExcel } = require("../utils/reportExportHandlers");
  handleExportExcel({ req, res, defaultTitle: "TradeIn_Report" });
});

// POST /api/tradeinreports/export-pdf
router.post("/export-pdf", (req, res) => {
  const { handleExportPdf } = require("../utils/reportExportHandlers");
  handleExportPdf({ req, res, defaultTitle: "TradeIn_Report" });
});

module.exports = router;
