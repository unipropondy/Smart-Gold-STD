const express = require("express");
const router = express.Router();
const { sql } = require("../db");
const { generateReportHtml, formatColumnHeader } = require("../utils/reportHtmlTemplate");
const { handleExportExcel, handleExportPdf } = require("../utils/reportExportHandlers");
const { validateBackendDates } = require("../utils/validateReportDates");

let memoryReports = [];
let nextReportId = 1;

// Helper to parse date string (YYYY-MM-DD or DD/MM/YYYY) to Date
const parseDateParam = (dateStr) => {
  if (!dateStr) return new Date();
  const s = String(dateStr).trim();
  if (s.includes("/")) {
    const parts = s.split("/");
    if (parts.length === 3) {
      const d = parseInt(parts[0], 10);
      const m = parseInt(parts[1], 10) - 1;
      const y = parseInt(parts[2], 10);
      return new Date(y, m, d);
    }
  }
  if (s.includes("-")) {
    const parts = s.split("-");
    if (parts.length === 3) {
      if (parts[0].length === 4) {
        return new Date(parseInt(parts[0], 10), parseInt(parts[1], 10) - 1, parseInt(parts[2], 10));
      } else {
        return new Date(parseInt(parts[2], 10), parseInt(parts[1], 10) - 1, parseInt(parts[0], 10));
      }
    }
  }
  return new Date(s);
};

// Helper for inclusive date range: [FromDate, ToDateExclusive)
const getInclusiveRange = (fromDateStr, toDateStr) => {
  const from = parseDateParam(fromDateStr);
  from.setHours(0, 0, 0, 0);

  const to = toDateStr ? parseDateParam(toDateStr) : new Date(from);
  to.setHours(0, 0, 0, 0);

  const toExclusive = new Date(to);
  toExclusive.setDate(toExclusive.getDate() + 1);

  return { fromDate: from, toDateExclusive: toExclusive, toDate: to };
};

// GET all reports
router.get("/", async (req, res) => {
  try {
    res.json({ success: true, data: memoryReports.slice().reverse() });
  } catch (err) {
    console.error("Fetch Transfer Reports Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// POST insert new report
router.post("/", async (req, res) => {
  try {
    const body = req.body;

    const newReport = {
      ReportID: nextReportId++,
      ReportName: body.ReportName || '',
      SelectOption: body.SelectOption || '',
      FromLocation: body.FromLocation || '',
      ToLocation: body.ToLocation || '',
      DateFrom: body.DateFrom || '',
      DateTo: body.DateTo || '',
      ReportType: body.ReportType || '',
      TransferStatus: body.TransferStatus || ''
    };

    memoryReports.push(newReport);
    res.json({ success: true, message: "Transfer Report saved successfully" });
  } catch (err) {
    console.error("Create Transfer Report Error:", err);
    res.status(500).json({ success: false, message: err.message, error: err.message });
  }
});

// PUT update report
router.put("/:id", async (req, res) => {
  try {
    const id = parseInt(req.params.id);
    const body = req.body;

    const index = memoryReports.findIndex(r => r.ReportID === id);
    if (index === -1) {
      return res.status(404).json({ success: false, message: "Report not found" });
    }

    memoryReports[index] = {
      ...memoryReports[index],
      ...body,
      ReportID: id
    };

    res.json({ success: true, message: "Transfer Report updated successfully" });
  } catch (err) {
    console.error("Update Transfer Report Error:", err);
    res.status(500).json({ success: false, message: err.message, error: err.message });
  }
});

// DELETE report
router.delete("/:id", async (req, res) => {
  try {
    const id = parseInt(req.params.id);
    const index = memoryReports.findIndex(r => r.ReportID === id);

    if (index !== -1) {
      memoryReports.splice(index, 1);
      res.json({ success: true, message: "Transfer Report deleted successfully" });
    } else {
      res.status(404).json({ success: false, message: "Report not found" });
    }
  } catch (err) {
    console.error("Delete Transfer Report Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// Helper to query transfer report records safely using parameterized inputs
async function queryTransferRecords({ pool, SelectOption, FromLocation, ToLocation, DateFrom, DateTo, ReportType, TransferStatus }) {
  const request = pool.request();
  let condition = "1=1";

  if (DateFrom && DateTo) {
    const { fromDate: fDate, toDateExclusive: tDateEx } = getInclusiveRange(DateFrom, DateTo);
    request.input("FromDate", sql.DateTime, fDate);
    request.input("ToDateExclusive", sql.DateTime, tDateEx);
    condition += ` AND h.TransferDate >= @FromDate AND h.TransferDate < @ToDateExclusive`;
  }

  if (FromLocation && FromLocation !== 'ALL') {
    request.input("FromLocation", sql.VarChar(50), FromLocation.trim());
    condition += ` AND RTRIM(h.FromLocation) = @FromLocation`;
  }
  if (ToLocation && ToLocation !== 'ALL') {
    request.input("ToLocation", sql.VarChar(50), ToLocation.trim());
    condition += ` AND RTRIM(h.ToLocation) = @ToLocation`;
  }
  if (SelectOption !== 'Transfer In') {
    if (TransferStatus === 'PENDING') {
      condition += ` AND h.ToLocStatus = '0'`;
    } else if (TransferStatus === 'ACCEPTED') {
      condition += ` AND h.ToLocStatus = '2'`;
    }
  }

  let query = "";
  const isSummary = ReportType === 'Summary';

  if (SelectOption === 'Transfer Out') {
    if (isSummary) {
      query = `
        SELECT h.TransferNo, h.FromLocation, h.ToLocation, h.FromLocStatus, h.ToLocStatus, h.TotalWeight, h.TotalPcs, h.TotalCost
        FROM tblInventoryTransferHeader h WITH (NOLOCK)
        WHERE ${condition}
        ORDER BY h.FromLocation, h.ToLocation, h.TransferNo
      `;
    } else {
      query = `
        SELECT d.TotalCost, d.PCS, d.Weight, d.Description, d.TransferNo, i.DiamondDescription, i.SupplierCode, d.InventoryCode, i.SizeCode, i.SizeType, h.ToLocation, h.FromLocation, i.Weight as InvWeight, i.StoneWeight, h.TransferDate, h.Remarks
        FROM tblInventoryTransferDetail d WITH (NOLOCK)
        INNER JOIN tblInventoryTransferHeader h WITH (NOLOCK) ON d.TransferNo=h.TransferNo 
        LEFT JOIN tblInventory i WITH (NOLOCK) ON d.InventoryCode=i.InventoryCode AND h.FromLocation=i.LocationCode
        WHERE ${condition}
        ORDER BY d.TransferNo, d.InventoryCode
      `;
    }
  } else if (SelectOption === 'TradeIn Transfer Out') {
    if (isSummary) {
      query = `
        SELECT h.TransferNo, h.FromLocation, h.ToLocation, h.FromLocStatus, h.ToLocStatus, h.TotalWeight, h.TotalPcs, h.TotalCost
        FROM tblInventoryTradeInTransferHeader h WITH (NOLOCK)
        WHERE ${condition}
        ORDER BY h.FromLocation, h.ToLocation, h.TransferNo
      `;
    } else {
      query = `
        SELECT d.TotalCost, d.PCS, d.Weight, d.Description, d.TransferNo, i.DiamondDescription, i.SupplierCode, d.InventoryCode, i.SizeCode, i.SizeType, h.ToLocation, h.FromLocation, i.Weight as InvWeight, i.StoneWeight, h.TransferDate, h.Remarks
        FROM tblInventoryTradeInTransferDetail d WITH (NOLOCK)
        INNER JOIN tblInventoryTradeInTransferHeader h WITH (NOLOCK) ON d.TransferNo=h.TransferNo 
        LEFT JOIN tblInventory i WITH (NOLOCK) ON d.InventoryCode=i.InventoryCode AND h.FromLocation=i.LocationCode
        WHERE ${condition}
        ORDER BY d.TransferNo, d.InventoryCode
      `;
    }
  } else if (SelectOption === 'Transfer In') {
    query = `
      SELECT d.TotalCost, d.PCS, d.Weight, d.Description, d.TransferNo, d.InventoryCode, d.QtyOnHand, h.FromLocation, h.ToLocation
      FROM localinventorytransferdetail d WITH (NOLOCK)
      INNER JOIN tblInventoryTransferHeader h WITH (NOLOCK) ON d.TransferNo = h.TransferNo
      WHERE ${condition}
      ORDER BY d.TransferNo, d.InventoryCode
    `;
  }

  const dbResult = await request.query(query);
  return dbResult.recordset || [];
}

// 1. POST /api/transferreports/generate (Standardized JSON endpoint matching Master Reports)
router.post("/generate", async (req, res) => {
  try {
    const { SelectOption, FromLocation, ToLocation, DateFrom, DateTo, ReportType, TransferStatus } = req.body;

    const dateVal = validateBackendDates(DateFrom, DateTo);
    if (!dateVal.isValid) {
      return res.status(400).json({ success: false, message: dateVal.message });
    }

    const pool = req.pool;
    const records = await queryTransferRecords({
      pool,
      SelectOption: SelectOption || 'Transfer Out',
      FromLocation,
      ToLocation,
      DateFrom,
      DateTo,
      ReportType: ReportType || 'Summary',
      TransferStatus,
    });

    const isSummary = (ReportType || 'Summary') === 'Summary';
    let title = "";
    let columns = [];
    let formattedData = [];
    let totals = {};

    // 1. TRANSFER OUT SUMMARY & TRADEIN TRANSFER OUT SUMMARY
    if ((SelectOption === 'Transfer Out' || SelectOption === 'TradeIn Transfer Out') && isSummary) {
      title = SelectOption === 'TradeIn Transfer Out'
        ? 'TRADE-IN TRANSFER OUT SUMMARY'
        : 'TRANSFER OUT SUMMARY';

      columns = [
        { key: 'Location', label: 'LOCATION', align: 'left' },
        { key: 'TransferNo', label: 'TRANSFER NO.', align: 'left' },
        { key: 'FromLocStatus', label: 'FROM LOC STATUS', align: 'center' },
        { key: 'ToLocStatus', label: 'TO LOC STATUS', align: 'center' },
        { key: 'TotalWeight', label: 'TOTAL WT.', align: 'right' },
        { key: 'TotalPcs', label: 'TOTAL QTY.', align: 'right' },
        { key: 'TotalCost', label: 'TOTAL COST', align: 'right' },
      ];

      let sumWeight = 0;
      let sumPcs = 0;
      let sumCost = 0;

      formattedData = records.map((r) => {
        const fromStat = r.FromLocStatus === '2' ? 'ACCEPTED' : (r.FromLocStatus === '0' ? 'PENDING' : r.FromLocStatus || 'ACCEPTED');
        const toStat = r.ToLocStatus === '2' ? 'ACCEPTED' : (r.ToLocStatus === '0' ? 'PENDING' : r.ToLocStatus || 'PENDING');
        const w = parseFloat(r.TotalWeight) || 0;
        const p = parseInt(r.TotalPcs) || 0;
        const c = parseFloat(r.TotalCost) || 0;

        sumWeight += w;
        sumPcs += p;
        sumCost += c;

        return {
          Location: r.FromLocation || '',
          TransferNo: r.TransferNo || '',
          FromLocStatus: fromStat,
          ToLocStatus: toStat,
          TotalWeight: w.toFixed(2),
          TotalPcs: p,
          TotalCost: c.toFixed(2),
        };
      });

      totals = {
        totalItems: formattedData.length,
        totalWeight: sumWeight.toFixed(2),
        totalQty: sumPcs,
        totalCost: sumCost.toFixed(2),
      };
    }
    // 2. TRANSFER IN SUMMARY
    else if (SelectOption === 'Transfer In' && isSummary) {
      title = 'TRANSFER IN SUMMARY';

      columns = [
        { key: 'InventoryCode', label: 'INV. CODE', align: 'left' },
        { key: 'Description', label: 'DESC.', align: 'left' },
        { key: 'Weight', label: 'TOTAL WT.', align: 'right' },
        { key: 'PCS', label: 'TOTAL QTY.', align: 'right' },
      ];

      let sumWeight = 0;
      let sumPcs = 0;

      formattedData = records.map((r) => {
        const w = parseFloat(r.Weight) || 0;
        const p = parseInt(r.PCS) || 0;
        sumWeight += w;
        sumPcs += p;

        return {
          InventoryCode: r.InventoryCode || '',
          Description: r.Description || '',
          Weight: w.toFixed(2),
          PCS: p,
        };
      });

      totals = {
        totalItems: formattedData.length,
        totalWeight: sumWeight.toFixed(2),
        totalQty: sumPcs,
      };
    }
    // 3. TRANSFER OUT DETAIL & TRADEIN TRANSFER OUT DETAIL
    else if ((SelectOption === 'Transfer Out' || SelectOption === 'TradeIn Transfer Out') && !isSummary) {
      title = SelectOption === 'TradeIn Transfer Out'
        ? 'TRADE-IN TRANSFER OUT DETAIL'
        : 'TRANSFER OUT DETAIL';

      columns = [
        { key: 'TransferNo', label: 'TRANSFER NO.', align: 'left' },
        { key: 'InventoryCode', label: 'INV. CODE', align: 'left' },
        { key: 'Description', label: 'DESC.', align: 'left' },
        { key: 'SizeCode', label: 'SIZE', align: 'center' },
        { key: 'SupplierCode', label: 'SUPP. CODE', align: 'left' },
        { key: 'GrossWeight', label: 'GROSS WT.', align: 'right' },
        { key: 'NetWeight', label: 'NET WT.', align: 'right' },
        { key: 'PCS', label: 'PCS', align: 'right' },
        { key: 'TotalCost', label: 'TOTAL COST', align: 'right' },
      ];

      let sumGross = 0;
      let sumNet = 0;
      let sumPcs = 0;
      let sumCost = 0;

      formattedData = records.map((r) => {
        const grossW = parseFloat(r.Weight) || 0;
        const netW = parseFloat(r.InvWeight) || parseFloat(r.Weight) || 0;
        const p = parseInt(r.PCS) || 0;
        const c = parseFloat(r.TotalCost) || 0;

        sumGross += grossW;
        sumNet += netW;
        sumPcs += p;
        sumCost += c;

        return {
          TransferNo: r.TransferNo || '',
          InventoryCode: r.InventoryCode || '',
          Description: r.Description || r.DiamondDescription || '',
          SizeCode: r.SizeCode || r.SizeType || '0.00',
          SupplierCode: r.SupplierCode || '',
          GrossWeight: grossW.toFixed(2),
          NetWeight: netW.toFixed(2),
          PCS: p > 0 ? p : '',
          TotalCost: c.toFixed(2),
        };
      });

      totals = {
        totalItems: formattedData.length,
        totalWeight: sumGross.toFixed(2),
        totalNetWeight: sumNet.toFixed(2),
        totalQty: sumPcs,
        totalCost: sumCost.toFixed(2),
      };
    }
    // 4. TRANSFER IN DETAIL
    else if (SelectOption === 'Transfer In' && !isSummary) {
      title = 'TRANSFER IN DETAIL';

      columns = [
        { key: 'TransferNo', label: 'TRANSFER NO.', align: 'left' },
        { key: 'InventoryCode', label: 'INV. CODE', align: 'left' },
        { key: 'Description', label: 'DESC.', align: 'left' },
        { key: 'Weight', label: 'TOTAL WT.', align: 'right' },
        { key: 'PCS', label: 'TOTAL QTY.', align: 'right' },
        { key: 'QtyOnHand', label: 'QTY ON HAND', align: 'right' },
        { key: 'TotalCost', label: 'TOTAL COST', align: 'right' },
      ];

      let sumWeight = 0;
      let sumPcs = 0;
      let sumCost = 0;

      formattedData = records.map((r) => {
        const w = parseFloat(r.Weight) || 0;
        const p = parseInt(r.PCS) || 0;
        const c = parseFloat(r.TotalCost) || 0;
        const qoh = r.QtyOnHand || '0';

        sumWeight += w;
        sumPcs += p;
        sumCost += c;

        return {
          TransferNo: r.TransferNo || '',
          InventoryCode: r.InventoryCode || '',
          Description: r.Description || '',
          Weight: w.toFixed(2),
          PCS: p,
          QtyOnHand: qoh,
          TotalCost: c.toFixed(2),
        };
      });

      totals = {
        totalItems: formattedData.length,
        totalWeight: sumWeight.toFixed(2),
        totalQty: sumPcs,
        totalCost: sumCost.toFixed(2),
      };
    }

    let subtitle = "";
    if (FromLocation && FromLocation !== 'ALL' || ToLocation && ToLocation !== 'ALL') {
      subtitle = `From: ${FromLocation || 'ALL'}  |  To: ${ToLocation || 'ALL'}`;
    }

    res.json({
      success: true,
      title,
      subtitle,
      columns,
      data: formattedData,
      totals,
    });
  } catch (err) {
    console.error("Transfer Reports Generate Error:", err);
    res.status(500).json({ success: false, message: err.message });
  }
});

// 2. POST /api/transferreports/download-html (Standardized HTML template matching Master Reports)
router.post("/download-html", async (req, res) => {
  try {
    const { title, subtitle, columns, data, totals, companyInfo, fromDate, toDate, showPrintButton } = req.body;
    const html = await generateReportHtml({
      title: title || "TRANSFER REPORT",
      subtitle: subtitle || "",
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
    console.error("Transfer Reports Download HTML Error:", err);
    res.status(500).send("Error generating report HTML: " + err.message);
  }
});

// 3. POST /api/transferreports/export-excel (Genuine .xlsx export matching Master Reports)
router.post("/export-excel", (req, res) => {
  handleExportExcel({ req, res, defaultTitle: "Transfer_Report" });
});

// 4. POST /api/transferreports/export-pdf (Headless Puppeteer PDF matching Master Reports)
router.post("/export-pdf", (req, res) => {
  handleExportPdf({ req, res, defaultTitle: "Transfer_Report" });
});

// 5. POST /api/transferreports/report (Legacy endpoint maintained for backward compatibility, now formatted in standardized template)
router.post("/report", async (req, res) => {
  try {
    const { SelectOption, FromLocation, ToLocation, DateFrom, DateTo, ReportType, TransferStatus } = req.body;
    const pool = req.pool;
    const records = await queryTransferRecords({
      pool,
      SelectOption: SelectOption || 'Transfer Out',
      FromLocation,
      ToLocation,
      DateFrom,
      DateTo,
      ReportType: ReportType || 'Summary',
      TransferStatus,
    });

    const isSummary = (ReportType || 'Summary') === 'Summary';
    let title = "";
    let columns = [];
    let formattedData = [];
    let totals = {};

    if ((SelectOption === 'Transfer Out' || SelectOption === 'TradeIn Transfer Out') && isSummary) {
      title = SelectOption === 'TradeIn Transfer Out' ? 'TRADE-IN TRANSFER OUT SUMMARY' : 'TRANSFER OUT SUMMARY';
      columns = [
        { key: 'Location', label: 'LOCATION', align: 'left' },
        { key: 'TransferNo', label: 'TRANSFER NO.', align: 'left' },
        { key: 'FromLocStatus', label: 'FROM LOC STATUS', align: 'center' },
        { key: 'ToLocStatus', label: 'TO LOC STATUS', align: 'center' },
        { key: 'TotalWeight', label: 'TOTAL WT.', align: 'right' },
        { key: 'TotalPcs', label: 'TOTAL QTY.', align: 'right' },
        { key: 'TotalCost', label: 'TOTAL COST', align: 'right' },
      ];
      let sumWeight = 0; let sumPcs = 0; let sumCost = 0;
      formattedData = records.map((r) => {
        const fromStat = r.FromLocStatus === '2' ? 'ACCEPTED' : (r.FromLocStatus === '0' ? 'PENDING' : r.FromLocStatus || 'ACCEPTED');
        const toStat = r.ToLocStatus === '2' ? 'ACCEPTED' : (r.ToLocStatus === '0' ? 'PENDING' : r.ToLocStatus || 'PENDING');
        const w = parseFloat(r.TotalWeight) || 0;
        const p = parseInt(r.TotalPcs) || 0;
        const c = parseFloat(r.TotalCost) || 0;
        sumWeight += w; sumPcs += p; sumCost += c;
        return {
          Location: r.FromLocation || '',
          TransferNo: r.TransferNo || '',
          FromLocStatus: fromStat,
          ToLocStatus: toStat,
          TotalWeight: w.toFixed(2),
          TotalPcs: p,
          TotalCost: c.toFixed(2),
        };
      });
      totals = { totalItems: formattedData.length, totalWeight: sumWeight.toFixed(2), totalQty: sumPcs, totalCost: sumCost.toFixed(2) };
    } else if (SelectOption === 'Transfer In' && isSummary) {
      title = 'TRANSFER IN SUMMARY';
      columns = [
        { key: 'InventoryCode', label: 'INV. CODE', align: 'left' },
        { key: 'Description', label: 'DESC.', align: 'left' },
        { key: 'Weight', label: 'TOTAL WT.', align: 'right' },
        { key: 'PCS', label: 'TOTAL QTY.', align: 'right' },
      ];
      let sumWeight = 0; let sumPcs = 0;
      formattedData = records.map((r) => {
        const w = parseFloat(r.Weight) || 0;
        const p = parseInt(r.PCS) || 0;
        sumWeight += w; sumPcs += p;
        return { InventoryCode: r.InventoryCode || '', Description: r.Description || '', Weight: w.toFixed(2), PCS: p };
      });
      totals = { totalItems: formattedData.length, totalWeight: sumWeight.toFixed(2), totalQty: sumPcs };
    } else if ((SelectOption === 'Transfer Out' || SelectOption === 'TradeIn Transfer Out') && !isSummary) {
      title = SelectOption === 'TradeIn Transfer Out' ? 'TRADE-IN TRANSFER OUT DETAIL' : 'TRANSFER OUT DETAIL';
      columns = [
        { key: 'TransferNo', label: 'TRANSFER NO.', align: 'left' },
        { key: 'InventoryCode', label: 'INV. CODE', align: 'left' },
        { key: 'Description', label: 'DESC.', align: 'left' },
        { key: 'SizeCode', label: 'SIZE', align: 'center' },
        { key: 'SupplierCode', label: 'SUPP. CODE', align: 'left' },
        { key: 'GrossWeight', label: 'GROSS WT.', align: 'right' },
        { key: 'NetWeight', label: 'NET WT.', align: 'right' },
        { key: 'PCS', label: 'PCS', align: 'right' },
        { key: 'TotalCost', label: 'TOTAL COST', align: 'right' },
      ];
      let sumGross = 0; let sumNet = 0; let sumPcs = 0; let sumCost = 0;
      formattedData = records.map((r) => {
        const grossW = parseFloat(r.Weight) || 0;
        const netW = parseFloat(r.InvWeight) || parseFloat(r.Weight) || 0;
        const p = parseInt(r.PCS) || 0;
        const c = parseFloat(r.TotalCost) || 0;
        sumGross += grossW; sumNet += netW; sumPcs += p; sumCost += c;
        return {
          TransferNo: r.TransferNo || '',
          InventoryCode: r.InventoryCode || '',
          Description: r.Description || r.DiamondDescription || '',
          SizeCode: r.SizeCode || r.SizeType || '0.00',
          SupplierCode: r.SupplierCode || '',
          GrossWeight: grossW.toFixed(2),
          NetWeight: netW.toFixed(2),
          PCS: p > 0 ? p : '',
          TotalCost: c.toFixed(2),
        };
      });
      totals = { totalItems: formattedData.length, totalWeight: sumGross.toFixed(2), totalNetWeight: sumNet.toFixed(2), totalQty: sumPcs, totalCost: sumCost.toFixed(2) };
    } else if (SelectOption === 'Transfer In' && !isSummary) {
      title = 'TRANSFER IN DETAIL';
      columns = [
        { key: 'TransferNo', label: 'TRANSFER NO.', align: 'left' },
        { key: 'InventoryCode', label: 'INV. CODE', align: 'left' },
        { key: 'Description', label: 'DESC.', align: 'left' },
        { key: 'Weight', label: 'TOTAL WT.', align: 'right' },
        { key: 'PCS', label: 'TOTAL QTY.', align: 'right' },
        { key: 'QtyOnHand', label: 'QTY ON HAND', align: 'right' },
        { key: 'TotalCost', label: 'TOTAL COST', align: 'right' },
      ];
      let sumWeight = 0; let sumPcs = 0; let sumCost = 0;
      formattedData = records.map((r) => {
        const w = parseFloat(r.Weight) || 0;
        const p = parseInt(r.PCS) || 0;
        const c = parseFloat(r.TotalCost) || 0;
        sumWeight += w; sumPcs += p; sumCost += c;
        return {
          TransferNo: r.TransferNo || '',
          InventoryCode: r.InventoryCode || '',
          Description: r.Description || '',
          Weight: w.toFixed(2),
          PCS: p,
          QtyOnHand: r.QtyOnHand || '0',
          TotalCost: c.toFixed(2),
        };
      });
      totals = { totalItems: formattedData.length, totalWeight: sumWeight.toFixed(2), totalQty: sumPcs, totalCost: sumCost.toFixed(2) };
    }

    let subtitle = "";
    if (FromLocation && FromLocation !== 'ALL' || ToLocation && ToLocation !== 'ALL') {
      subtitle = `From: ${FromLocation || 'ALL'}  |  To: ${ToLocation || 'ALL'}`;
    }

    const html = await generateReportHtml({
      title,
      subtitle,
      columns,
      data: formattedData,
      totals,
      fromDate: DateFrom,
      toDate: DateTo,
      pool,
      showPrintButton: false,
    });

    res.setHeader("Content-Type", "text/html; charset=utf-8");
    res.send(html);
  } catch (err) {
    console.error("Transfer Report Legacy Error:", err);
    res.status(500).send("Server error generating report.");
  }
});

module.exports = router;
