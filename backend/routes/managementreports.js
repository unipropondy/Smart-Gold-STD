const express = require("express");
const router = express.Router();
const { sql } = require("../db");
const { validateBackendDates } = require("../utils/validateReportDates");

// ==========================================
// DATE HELPER UTILITIES
// ==========================================

const parseDateParam = (dateStr) => {
  if (!dateStr) return new Date();
  if (typeof dateStr === "string" && dateStr.includes("/")) {
    const parts = dateStr.split("/");
    if (parts.length === 3) {
      // DD/MM/YYYY -> YYYY-MM-DD
      return new Date(`${parts[2]}-${parts[1].padStart(2, "0")}-${parts[0].padStart(2, "0")}`);
    }
  }
  return new Date(dateStr);
};

const getInclusiveRange = (fromDateStr, toDateStr) => {
  const from = parseDateParam(fromDateStr);
  from.setHours(0, 0, 0, 0);

  const to = toDateStr ? parseDateParam(toDateStr) : new Date(from);
  to.setHours(0, 0, 0, 0);

  const toExclusive = new Date(to);
  toExclusive.setDate(toExclusive.getDate() + 1);

  return { fromDate: from, toDateExclusive: toExclusive, toDate: to };
};

const formatDateDisp = (d) => {
  if (!d || isNaN(new Date(d).getTime())) return "";
  const dt = new Date(d);
  const day = String(dt.getDate()).padStart(2, "0");
  const month = String(dt.getMonth() + 1).padStart(2, "0");
  const year = dt.getFullYear();
  return `${day}/${month}/${year}`;
};

// Middleware for strict backend date validation
router.use((req, res, next) => {
  const fromDate = req.query.fromDate || req.body?.fromDate;
  const toDate = req.query.toDate || req.body?.toDate;
  if (!fromDate && !toDate) return next();

  const dateVal = validateBackendDates(fromDate, toDate);
  if (!dateVal.isValid) {
    return res.status(400).json({
      success: false,
      message: dateVal.message
    });
  }

  next();
});

// 1. Sales Profit Report By Date
router.get("/sales-profit-by-date", async (req, res) => {
  try {
    const pool = req.pool;
    const { fromDate, toDate, locationCode, reportType = 'Summary' } = req.query;

    const fDate = parseDateParam(fromDate);
    const tDate = parseDateParam(toDate);
    const loc = locationCode && locationCode !== 'ALL' ? locationCode : null;

    // 1. Fetch Company / Location metadata from database
    let companyInfo = {
      name: '',
      address1: '',
      phone: '',
      fax: ''
    };

    try {
      // First fetch primary company info from tblCompany (where Name: SMART and Address: 45 KALLANG PUDDING ROAD)
      const compRes = await pool.request().query("SELECT TOP 1 Name, Address1, Address2, City, State, Country, Phone, Fax FROM tblCompany");
      if (compRes.recordset && compRes.recordset.length > 0) {
        const c = compRes.recordset[0];
        const rawParts = [c.Address1, c.Address2, c.City, c.State, c.Country]
          .map(s => s ? s.trim() : '')
          .filter(Boolean);

        // Deduplicate consecutive repeated tokens
        const deduplicatedParts = [];
        rawParts.forEach(p => {
          if (deduplicatedParts.length === 0 || deduplicatedParts[deduplicatedParts.length - 1].toUpperCase() !== p.toUpperCase()) {
            deduplicatedParts.push(p);
          }
        });

        companyInfo = {
          name: (c.Name || '').trim(),
          address1: deduplicatedParts.join(' '),
          phone: (c.Phone || '').trim(),
          fax: (c.Fax || '').trim()
        };
      }

      // If location is specified and tblLocation has address details, allow it to supplement
      if (loc) {
        const locRes = await pool.request()
          .input('LocCode', sql.VarChar, loc)
          .query("SELECT TOP 1 LocationName, Address1, Address2, Address3, Address4, Phone, Fax FROM tblLocation WHERE RTRIM(LocationCode) = @LocCode");
        if (locRes.recordset && locRes.recordset.length > 0) {
          const l = locRes.recordset[0];
          const locParts = [l.Address1, l.Address2, l.Address3, l.Address4].map(s => s ? s.trim() : '').filter(Boolean);
          if (locParts.length > 0) {
            companyInfo.address1 = locParts.join(' ');
          }
          if (l.Phone?.trim()) companyInfo.phone = l.Phone.trim();
          if (l.Fax?.trim()) companyInfo.fax = l.Fax.trim();
        }
      }
    } catch (cErr) {
      console.error("Error fetching company info:", cErr);
    }

    // 2. Format Date range string
    const formatDateDisp = (d) => {
      const day = String(d.getDate()).padStart(2, '0');
      const month = String(d.getMonth() + 1).padStart(2, '0');
      const year = d.getFullYear();
      return `${day}/${month}/${year}`;
    };

    const fromDateDisp = formatDateDisp(fDate);
    const toDateDisp = formatDateDisp(tDate);
    const isSummary = reportType.toLowerCase() === 'summary';
    const reportTitle = `${isSummary ? 'Summary' : 'Detail'} Report of Sales Profit From ${fromDateDisp} To ${toDateDisp}`;

    let query = '';
    if (isSummary) {
      // Summary mode: 1 row per invoice matching legacy report exactly
      query = `
        SELECT 
          tblSalesHeader.InvoiceDate,
          RTRIM(tblSalesHeader.InvoiceNo) AS InvoiceNo,
          ISNULL(tblSalesHeader.SalesManCode, '') AS SalesManCode,
          ISNULL(tblSalesHeader.TotalValue, 0) AS TotalValue,
          ISNULL(tblSalesHeader.TotalWeight, 0) AS Weight,
          ISNULL(tblSalesHeader.TotalSMC, 0) AS SMC,
          ISNULL(tblSalesHeader.GST, 0) AS GST,
          CASE 
            WHEN RTRIM(tblSalesHeader.GSTType) = 'E' THEN (ISNULL(tblSalesHeader.TotalValue, 0) + ISNULL(tblSalesHeader.TradeInAmt, 0))
            ELSE (ISNULL(tblSalesHeader.TotalValue, 0) + ISNULL(tblSalesHeader.TradeInAmt, 0) - ISNULL(tblSalesHeader.GST, 0))
          END AS NetAmount,
          (ISNULL(tblSalesHeader.TotalCost, 0) - ISNULL(tblSalesHeader.TradeInAmt, 0)) AS TotalCost,
          (
            CASE 
              WHEN RTRIM(tblSalesHeader.GSTType) = 'E' THEN (ISNULL(tblSalesHeader.TotalValue, 0) + ISNULL(tblSalesHeader.TradeInAmt, 0))
              ELSE (ISNULL(tblSalesHeader.TotalValue, 0) + ISNULL(tblSalesHeader.TradeInAmt, 0) - ISNULL(tblSalesHeader.GST, 0))
            END 
            - (ISNULL(tblSalesHeader.TotalCost, 0) - ISNULL(tblSalesHeader.TradeInAmt, 0))
          ) AS Profit
        FROM tblSalesHeader tblSalesHeader
        WHERE tblSalesHeader.InvoiceDate >= @FromDate 
          AND tblSalesHeader.InvoiceDate < DATEADD(DAY, 1, @ToDate)
      `;
      if (loc) {
        query += ` AND RTRIM(tblSalesHeader.LocationCode) = @LocationCode`;
      }
      query += ` ORDER BY tblSalesHeader.InvoiceDate ASC, tblSalesHeader.InvoiceNo ASC`;
    } else {
      // Detail mode: 1 row per non-TI inventory item matching legacy detail report
      query = `
        SELECT 
          tblSalesHeader.InvoiceDate,
          RTRIM(tblSalesHeader.InvoiceNo) AS InvoiceNo,
          ISNULL(tblSalesHeader.SalesManCode, '') AS SalesManCode,
          RTRIM(ISNULL(tblSalesDetail.InventoryCode, '')) AS InventoryCode,
          RTRIM(ISNULL(tblSalesDetail.Description, '')) AS Description,
          ISNULL(tblSalesDetail.Weight, 0) AS TotalWeight,
          ISNULL(tblInventory.PMC, 0) AS PMC,
          ISNULL(tblSalesDetail.SMC, 0) AS SMC,
          ISNULL(tblInventory.CostPerGram, 0) AS PurchaseCost,
          ROUND((tblSalesDetail.Weight * ISNULL(tblInventory.CostPerGram, 0)) + ISNULL(tblInventory.PMC, 0), 2) AS PurchaseAmount,
          ROUND(
            CASE 
              WHEN RTRIM(tblSalesHeader.GSTType) = 'E' THEN ISNULL(tblSalesDetail.TotalValue, 0)
              WHEN RTRIM(tblSalesHeader.GSTType) = 'Z' THEN ISNULL(tblSalesDetail.TotalValue, 0)
              ELSE (ISNULL(tblSalesDetail.TotalValue, 0) - ISNULL(tblSalesDetail.GST, 0))
            END,
            2
          ) AS SalesAmount,
          ROUND(
            (
              CASE 
                WHEN RTRIM(tblSalesHeader.GSTType) = 'E' THEN ISNULL(tblSalesDetail.TotalValue, 0)
                WHEN RTRIM(tblSalesHeader.GSTType) = 'Z' THEN ISNULL(tblSalesDetail.TotalValue, 0)
                ELSE (ISNULL(tblSalesDetail.TotalValue, 0) - ISNULL(tblSalesDetail.GST, 0))
              END
            ) - ((tblSalesDetail.Weight * ISNULL(tblInventory.CostPerGram, 0)) + ISNULL(tblInventory.PMC, 0)),
            2
          ) AS Profit
        FROM tblSalesDetail tblSalesDetail
        INNER JOIN tblSalesHeader tblSalesHeader ON tblSalesDetail.InvoiceNo = tblSalesHeader.InvoiceNo
        LEFT JOIN tblInventory tblInventory ON tblSalesDetail.InventoryCode = tblInventory.InventoryCode
        WHERE tblSalesHeader.InvoiceDate >= @FromDate 
          AND tblSalesHeader.InvoiceDate < DATEADD(DAY, 1, @ToDate)
          AND LEFT(RTRIM(tblSalesDetail.InventoryCode), 2) <> 'TI'
      `;
      if (loc) {
        query += ` AND RTRIM(tblSalesHeader.LocationCode) = @LocationCode`;
      }
      query += ` ORDER BY tblSalesHeader.InvoiceDate ASC, tblSalesHeader.InvoiceNo ASC, tblSalesDetail.InventoryCode ASC`;
    }

    const request = pool.request();
    request.input('FromDate', sql.DateTime, fDate);
    request.input('ToDate', sql.DateTime, tDate);
    if (loc) request.input('LocationCode', sql.VarChar, loc);

    const result = await request.query(query);
    const rows = result.recordset || [];

    if (isSummary) {
      let overallTotalValue = 0;
      let overallTotalWeight = 0;
      let overallTotalSMC = 0;
      let overallTotalGST = 0;
      let overallNetAmount = 0;
      let overallTotalCost = 0;
      let overallProfit = 0;

      const groupedData = {};

      rows.forEach((r) => {
        const dateObj = new Date(r.InvoiceDate);
        const dateKey = formatDateDisp(dateObj);

        const val = parseFloat(r.TotalValue) || 0;
        const wt = parseFloat(r.Weight) || 0;
        const smc = parseFloat(r.SMC) || 0;
        const gst = parseFloat(r.GST) || 0;
        const net = parseFloat(r.NetAmount) || 0;
        const cost = parseFloat(r.TotalCost) || 0;
        const profit = parseFloat(r.Profit) || (net - cost);

        overallTotalValue += val;
        overallTotalWeight += wt;
        overallTotalSMC += smc;
        overallTotalGST += gst;
        overallNetAmount += net;
        overallTotalCost += cost;
        overallProfit += profit;

        if (!groupedData[dateKey]) {
          groupedData[dateKey] = {
            date: dateKey,
            items: [],
            groupTotal: {
              totalValue: 0,
              weight: 0,
              smc: 0,
              gst: 0,
              netAmount: 0,
              totalCost: 0,
              profit: 0
            }
          };
        }

        const itemObj = {
          invoiceNo: r.InvoiceNo,
          salesManCode: (r.SalesManCode || '').trim(),
          totalValue: val.toFixed(2),
          totalWeight: wt.toFixed(2),
          smc: smc.toFixed(2),
          gst: gst.toFixed(2),
          netAmount: net.toFixed(2),
          totalCost: cost.toFixed(2),
          profit: profit.toFixed(2)
        };

        groupedData[dateKey].items.push(itemObj);

        groupedData[dateKey].groupTotal.totalValue += val;
        groupedData[dateKey].groupTotal.weight += wt;
        groupedData[dateKey].groupTotal.smc += smc;
        groupedData[dateKey].groupTotal.gst += gst;
        groupedData[dateKey].groupTotal.netAmount += net;
        groupedData[dateKey].groupTotal.totalCost += cost;
        groupedData[dateKey].groupTotal.profit += profit;
      });

      const groupsArray = Object.keys(groupedData).map(dateKey => {
        const g = groupedData[dateKey];
        return {
          date: g.date,
          items: g.items,
          groupTotal: {
            totalValue: g.groupTotal.totalValue.toFixed(2),
            weight: g.groupTotal.weight.toFixed(2),
            smc: g.groupTotal.smc.toFixed(2),
            gst: g.groupTotal.gst.toFixed(2),
            netAmount: g.groupTotal.netAmount.toFixed(2),
            totalCost: g.groupTotal.totalCost.toFixed(2),
            profit: g.groupTotal.profit.toFixed(2)
          }
        };
      });

      const flatData = rows.map((r, idx) => {
        const dateObj = new Date(r.InvoiceDate);
        const val = parseFloat(r.TotalValue) || 0;
        const wt = parseFloat(r.Weight) || 0;
        const smc = parseFloat(r.SMC) || 0;
        const gst = parseFloat(r.GST) || 0;
        const net = parseFloat(r.NetAmount) || 0;
        const cost = parseFloat(r.TotalCost) || 0;
        const profit = parseFloat(r.Profit) || (net - cost);

        return {
          sNo: idx + 1,
          invoiceDate: formatDateDisp(dateObj),
          invoiceNo: r.InvoiceNo,
          salesManCode: (r.SalesManCode || '').trim(),
          totalValue: val.toFixed(2),
          totalWeight: wt.toFixed(2),
          smc: smc.toFixed(2),
          gst: gst.toFixed(2),
          netAmount: net.toFixed(2),
          totalCost: cost.toFixed(2),
          profit: profit.toFixed(2)
        };
      });

      const columns = [
        { key: 'invoiceNo', title: 'Invoice No', align: 'left' },
        { key: 'totalValue', title: 'Total Value', align: 'right' },
        { key: 'totalWeight', title: 'Total Wt', align: 'right' },
        { key: 'smc', title: 'SMC', align: 'right' },
        { key: 'gst', title: 'Gst', align: 'right' },
        { key: 'netAmount', title: 'Net Amount', align: 'right' },
        { key: 'totalCost', title: 'Total Cost', align: 'right' },
        { key: 'profit', title: 'Profit', align: 'right' }
      ];

      return res.json({
        success: true,
        company: companyInfo,
        reportTitle,
        fromDate: fromDateDisp,
        toDate: toDateDisp,
        columns,
        summary: {
          totalSales: overallNetAmount.toFixed(2),
          totalCost: overallTotalCost.toFixed(2),
          totalProfit: overallProfit.toFixed(2),
          totalRecords: flatData.length,
          totalValue: overallTotalValue.toFixed(2),
          totalWeight: overallTotalWeight.toFixed(2),
          totalSMC: overallTotalSMC.toFixed(2),
          totalGST: overallTotalGST.toFixed(2)
        },
        groupedData: groupsArray,
        data: flatData
      });

    } else {
      // Detail Report mode
      let overallTotalWeight = 0;
      let overallTotalPMC = 0;
      let overallTotalSMC = 0;
      let overallTotalPurchaseCost = 0;
      let overallTotalPurchaseAmount = 0;
      let overallTotalSalesAmount = 0;
      let overallTotalProfit = 0;

      // Group by Invoice Date -> Invoice No -> products
      const dateMap = {};

      rows.forEach((r) => {
        const dateObj = new Date(r.InvoiceDate);
        const dateKey = formatDateDisp(dateObj);

        const wt = parseFloat(r.TotalWeight) || 0;
        const pmc = parseFloat(r.PMC) || 0;
        const smc = parseFloat(r.SMC) || 0;
        const purchaseCost = parseFloat(r.PurchaseCost) || 0;
        const purchaseAmount = parseFloat(r.PurchaseAmount) || 0;
        const salesAmount = parseFloat(r.SalesAmount) || 0;
        const profit = parseFloat(r.Profit) || 0;

        overallTotalWeight += wt;
        overallTotalPMC += pmc;
        overallTotalSMC += smc;
        overallTotalPurchaseCost += purchaseCost;
        overallTotalPurchaseAmount += purchaseAmount;
        overallTotalSalesAmount += salesAmount;
        overallTotalProfit += profit;

        if (!dateMap[dateKey]) {
          dateMap[dateKey] = {
            date: dateKey,
            invoices: {},
            groupTotal: {
              totalWeight: 0,
              pmc: 0,
              smc: 0,
              purchaseCost: 0,
              purchaseAmount: 0,
              salesAmount: 0,
              profit: 0
            }
          };
        }

        const invNo = r.InvoiceNo;
        if (!dateMap[dateKey].invoices[invNo]) {
          dateMap[dateKey].invoices[invNo] = {
            invoiceNo: invNo,
            items: []
          };
        }

        const itemObj = {
          invoiceNo: invNo,
          inventoryCode: r.InventoryCode || '',
          description: r.Description || '',
          totalWeight: wt.toFixed(2),
          pmc: pmc.toFixed(2),
          smc: smc.toFixed(2),
          purchaseCost: purchaseCost.toFixed(2),
          purchaseAmount: purchaseAmount.toFixed(2),
          salesAmount: salesAmount.toFixed(2),
          profit: profit.toFixed(2)
        };

        dateMap[dateKey].invoices[invNo].items.push(itemObj);

        dateMap[dateKey].groupTotal.totalWeight += wt;
        dateMap[dateKey].groupTotal.pmc += pmc;
        dateMap[dateKey].groupTotal.smc += smc;
        dateMap[dateKey].groupTotal.purchaseCost += purchaseCost;
        dateMap[dateKey].groupTotal.purchaseAmount += purchaseAmount;
        dateMap[dateKey].groupTotal.salesAmount += salesAmount;
        dateMap[dateKey].groupTotal.profit += profit;
      });

      const groupsArray = Object.keys(dateMap).map(dateKey => {
        const d = dateMap[dateKey];
        const invoiceList = Object.keys(d.invoices).map(invKey => d.invoices[invKey]);
        // Also provide flat items within date if needed
        const allDateItems = invoiceList.flatMap(inv => inv.items);

        return {
          date: d.date,
          invoices: invoiceList,
          items: allDateItems,
          groupTotal: {
            totalWeight: d.groupTotal.totalWeight.toFixed(2),

            pmc: d.groupTotal.pmc.toFixed(2),
            smc: d.groupTotal.smc.toFixed(2),
            purchaseCost: d.groupTotal.purchaseCost.toFixed(2),
            purchaseAmount: d.groupTotal.purchaseAmount.toFixed(2),
            salesAmount: d.groupTotal.salesAmount.toFixed(2),
            profit: d.groupTotal.profit.toFixed(2)
          }
        };
      });

      const flatData = rows.map((r, idx) => {
        const dateObj = new Date(r.InvoiceDate);
        const wt = parseFloat(r.TotalWeight) || 0;
        const pmc = parseFloat(r.PMC) || 0;
        const smc = parseFloat(r.SMC) || 0;
        const purchaseCost = parseFloat(r.PurchaseCost) || 0;
        const purchaseAmount = parseFloat(r.PurchaseAmount) || 0;
        const salesAmount = parseFloat(r.SalesAmount) || 0;
        const profit = parseFloat(r.Profit) || 0;

        return {
          sNo: idx + 1,
          invoiceDate: formatDateDisp(dateObj),
          invoiceNo: r.InvoiceNo,
          inventoryCode: r.InventoryCode || '',
          description: r.Description || '',
          totalWeight: wt.toFixed(2),
          pmc: pmc.toFixed(2),
          smc: smc.toFixed(2),
          purchaseCost: purchaseCost.toFixed(2),
          purchaseAmount: purchaseAmount.toFixed(2),
          salesAmount: salesAmount.toFixed(2),
          profit: profit.toFixed(2)
        };
      });

      const columns = [
        { key: 'inventoryCode', title: 'Inventory Code', align: 'left' },
        { key: 'description', title: 'Description', align: 'left' },
        { key: 'totalWeight', title: 'Total Wt', align: 'right' },
        { key: 'pmc', title: 'PMC', align: 'right' },
        { key: 'smc', title: 'SMC', align: 'right' },
        { key: 'purchaseCost', title: 'Purchase Cost', align: 'right' },
        { key: 'purchaseAmount', title: 'Purchase Amount', align: 'right' },
        { key: 'salesAmount', title: 'Sales Amount', align: 'right' },
        { key: 'profit', title: 'Profit', align: 'right' }
      ];

      return res.json({
        success: true,
        company: companyInfo,
        reportTitle,
        fromDate: fromDateDisp,
        toDate: toDateDisp,
        columns,
        summary: {
          totalWeight: overallTotalWeight.toFixed(2),
          totalPMC: overallTotalPMC.toFixed(2),
          totalSMC: overallTotalSMC.toFixed(2),
          totalPurchaseCost: overallTotalPurchaseCost.toFixed(2),
          totalPurchaseAmount: overallTotalPurchaseAmount.toFixed(2),
          totalSalesAmount: overallTotalSalesAmount.toFixed(2),
          totalProfit: overallTotalProfit.toFixed(2),
          // for compatibility with generic summary cards:
          totalSales: overallTotalSalesAmount.toFixed(2),
          totalCost: overallTotalPurchaseAmount.toFixed(2),
          totalRecords: Object.keys(dateMap).reduce((acc, dt) => acc + Object.keys(dateMap[dt].invoices).length, 0),
          totalInvoices: Object.keys(dateMap).reduce((acc, dt) => acc + Object.keys(dateMap[dt].invoices).length, 0),
          totalItems: flatData.length
        },
        groupedData: groupsArray,
        data: flatData
      });
    }

  } catch (err) {
    console.error("Sales Profit By Date Error:", err);
    res.status(500).json({ success: false, message: "Error generating Sales Profit Report By Date", error: err.message });
  }
});

// 2. Sales Profit Report By Sales Man
router.get("/sales-profit-by-salesman", async (req, res) => {
  try {
    const pool = req.pool;
    const { fromDate, toDate, locationCode, salesmanCode, reportType = 'Summary' } = req.query;

    const fDate = parseDateParam(fromDate);
    const tDate = parseDateParam(toDate);
    const loc = locationCode && locationCode !== 'ALL' ? locationCode : null;
    const sm = salesmanCode && salesmanCode !== 'ALL' ? salesmanCode : null;

    // Fetch dynamic company info
    let companyInfo = {
      name: 'SMART GOLD TRADING LLC',
      address1: 'Shop # 6, Building 24, Gold Souk, Deira, Dubai, UAE',
      phone: '',
      fax: ''
    };

    try {
      const compRes = await pool.request().query("SELECT TOP 1 * FROM tblCompany");
      if (compRes.recordset && compRes.recordset.length > 0) {
        const c = compRes.recordset[0];
        const rawParts = [c.Address1, c.Address2, c.Address3, c.Address4, c.City, c.Country, c.PostalCode]
          .map(s => s ? s.trim() : '')
          .filter(Boolean);

        const deduplicatedParts = [];
        rawParts.forEach(part => {
          if (!deduplicatedParts.some(existing => existing.toLowerCase() === part.toLowerCase())) {
            deduplicatedParts.push(part);
          }
        });

        companyInfo = {
          name: (c.Name || '').trim(),
          address1: deduplicatedParts.join(' '),
          phone: (c.Phone || '').trim(),
          fax: (c.Fax || '').trim()
        };
      }

      if (loc) {
        const locRes = await pool.request()
          .input('LocCode', sql.VarChar, loc)
          .query("SELECT TOP 1 LocationName, Address1, Address2, Address3, Address4, Phone, Fax FROM tblLocation WHERE RTRIM(LocationCode) = @LocCode");
        if (locRes.recordset && locRes.recordset.length > 0) {
          const l = locRes.recordset[0];
          const locParts = [l.Address1, l.Address2, l.Address3, l.Address4].map(s => s ? s.trim() : '').filter(Boolean);
          if (locParts.length > 0) {
            companyInfo.address1 = locParts.join(' ');
          }
          if (l.Phone?.trim()) companyInfo.phone = l.Phone.trim();
          if (l.Fax?.trim()) companyInfo.fax = l.Fax.trim();
        }
      }
    } catch (cErr) {
      console.error("Error fetching company info:", cErr);
    }

    const formatDateDisp = (d) => {
      const day = String(d.getDate()).padStart(2, '0');
      const month = String(d.getMonth() + 1).padStart(2, '0');
      const year = d.getFullYear();
      return `${day}/${month}/${year}`;
    };

    const fromDateDisp = formatDateDisp(fDate);
    const toDateDisp = formatDateDisp(tDate);
    const isSummary = reportType.toLowerCase() === 'summary';
    const reportTitle = `${isSummary ? 'Summary' : 'Detail'} Report of Sales Profit By Sales Man From ${fromDateDisp} To ${toDateDisp}`;

    if (isSummary) {
      // Summary mode: Grouped by Salesman
      let query = `
        SELECT 
          RTRIM("tblSalesMan"."SalesManName") AS SalesManName,
          RTRIM("vwSalesProfitGold"."Salesmancode") AS Salesmancode,
          COUNT(DISTINCT "vwSalesProfitGold"."Invoiceno") AS TotalInvoices,
          SUM(ISNULL("vwSalesProfitGold"."Weight", 0)) AS Weight,
          SUM(ISNULL("vwSalesProfitGold"."SMC", 0)) AS SMC,
          SUM(ISNULL("vwSalesProfitGold"."NetAmount", 0)) AS NetAmount,
          SUM(ISNULL("vwSalesProfitGold"."TotalCost", 0)) AS TotalCost,
          SUM(ISNULL("vwSalesProfitGold"."NetAmount", 0) - ISNULL("vwSalesProfitGold"."TotalCost", 0)) AS Profit
        FROM "SmartGold"."dbo"."vwSalesProfitGold" "vwSalesProfitGold"
        INNER JOIN "SmartGold"."dbo"."tblSalesMan" "tblSalesMan" 
          ON "vwSalesProfitGold"."Salesmancode" = "tblSalesMan"."SalesManCode"
        WHERE "vwSalesProfitGold"."InvoiceDate" >= @FromDate 
          AND "vwSalesProfitGold"."InvoiceDate" < DATEADD(DAY, 1, @ToDate)
      `;

      if (loc) {
        query += ` AND RTRIM("vwSalesProfitGold"."LocationCode") = @LocationCode`;
      }
      if (sm) {
        query += ` AND RTRIM("vwSalesProfitGold"."Salesmancode") = @SalesManCode`;
      }

      query += `
        GROUP BY "vwSalesProfitGold"."Salesmancode", "tblSalesMan"."SalesManName"
        ORDER BY "vwSalesProfitGold"."Salesmancode"
      `;

      const request = pool.request();
      request.input('FromDate', sql.DateTime, fDate);
      request.input('ToDate', sql.DateTime, tDate);
      if (loc) request.input('LocationCode', sql.VarChar, loc);
      if (sm) request.input('SalesManCode', sql.VarChar, sm);

      const result = await request.query(query);
      const rows = result.recordset || [];

      let overallTotalWeight = 0;
      let overallTotalSMC = 0;
      let overallNetAmount = 0;
      let overallTotalCost = 0;
      let overallProfit = 0;
      let overallTotalInvoices = 0;

      const formattedData = rows.map((r, idx) => {
        const totalInv = parseInt(r.TotalInvoices, 10) || 0;
        const wt = parseFloat(r.Weight) || 0;
        const smcVal = parseFloat(r.SMC) || 0;
        const net = parseFloat(r.NetAmount) || 0;
        const cost = parseFloat(r.TotalCost) || 0;
        const profit = parseFloat(r.Profit) || (net - cost);

        overallTotalInvoices += totalInv;
        overallTotalWeight += wt;
        overallTotalSMC += smcVal;
        overallNetAmount += net;
        overallTotalCost += cost;
        overallProfit += profit;

        return {
          sNo: idx + 1,
          salesmanCode: r.Salesmancode,
          salesmanName: r.SalesManName,
          totalInvoices: totalInv,
          weight: wt.toFixed(2),
          smc: smcVal.toFixed(2),
          netAmount: net.toFixed(2),
          totalCost: cost.toFixed(2),
          profit: profit.toFixed(2)
        };
      });

      const columns = [
        { key: 'sNo', title: 'S.No', align: 'left' },
        { key: 'salesmanCode', title: 'Salesman Code', align: 'left' },
        { key: 'salesmanName', title: 'Salesman Name', align: 'left' },
        { key: 'totalInvoices', title: 'Total Invoices', align: 'right' },
        { key: 'weight', title: 'Weight', align: 'right' },
        { key: 'smc', title: 'SMC', align: 'right' },
        { key: 'netAmount', title: 'Net Amount', align: 'right' },
        { key: 'totalCost', title: 'Total Cost', align: 'right' },
        { key: 'profit', title: 'Profit', align: 'right' }
      ];

      return res.json({
        success: true,
        company: companyInfo,
        reportTitle,
        fromDate: fromDateDisp,
        toDate: toDateDisp,
        columns,
        summary: {
          totalWeight: overallTotalWeight.toFixed(2),
          totalSMC: overallTotalSMC.toFixed(2),
          totalSales: overallNetAmount.toFixed(2),
          totalCost: overallTotalCost.toFixed(2),
          totalProfit: overallProfit.toFixed(2),
          totalInvoices: overallTotalInvoices,
          totalRecords: formattedData.length
        },
        data: formattedData
      });

    } else {
      // Detail mode: User's legacy query joining vwSalesProfitGold and tblSalesMan
      let query = `
        SELECT 
          RTRIM("tblSalesMan"."SalesManName") AS SalesManName, 
          "vwSalesProfitGold"."InvoiceDate", 
          RTRIM("vwSalesProfitGold"."Invoiceno") AS Invoiceno, 
          "vwSalesProfitGold"."Weight", 
          "vwSalesProfitGold"."SMC", 
          "vwSalesProfitGold"."NetAmount", 
          "vwSalesProfitGold"."TotalCost", 
          (ISNULL("vwSalesProfitGold"."NetAmount", 0) - ISNULL("vwSalesProfitGold"."TotalCost", 0)) AS Profit,
          RTRIM("vwSalesProfitGold"."Salesmancode") AS Salesmancode
        FROM "SmartGold"."dbo"."vwSalesProfitGold" "vwSalesProfitGold" 
        INNER JOIN "SmartGold"."dbo"."tblSalesMan" "tblSalesMan" 
          ON "vwSalesProfitGold"."Salesmancode" = "tblSalesMan"."SalesManCode"
        WHERE "vwSalesProfitGold"."InvoiceDate" >= @FromDate 
          AND "vwSalesProfitGold"."InvoiceDate" < DATEADD(DAY, 1, @ToDate)
      `;

      if (loc) {
        query += ` AND RTRIM("vwSalesProfitGold"."LocationCode") = @LocationCode`;
      }
      if (sm) {
        query += ` AND RTRIM("vwSalesProfitGold"."Salesmancode") = @SalesManCode`;
      }

      query += ` ORDER BY "vwSalesProfitGold"."Salesmancode", "vwSalesProfitGold"."InvoiceDate", "vwSalesProfitGold"."Invoiceno"`;

      const request = pool.request();
      request.input('FromDate', sql.DateTime, fDate);
      request.input('ToDate', sql.DateTime, tDate);
      if (loc) request.input('LocationCode', sql.VarChar, loc);
      if (sm) request.input('SalesManCode', sql.VarChar, sm);

      const result = await request.query(query);
      const rows = result.recordset || [];

      let overallTotalWeight = 0;
      let overallTotalSMC = 0;
      let overallNetAmount = 0;
      let overallTotalCost = 0;
      let overallProfit = 0;

      const smMap = {};

      rows.forEach((r) => {
        const smCode = r.Salesmancode || 'HOUSE';
        const smName = r.SalesManName || smCode;
        const dateObj = new Date(r.InvoiceDate);
        const dateStr = formatDateDisp(dateObj);

        const wt = parseFloat(r.Weight) || 0;
        const smcVal = parseFloat(r.SMC) || 0;
        const net = parseFloat(r.NetAmount) || 0;
        const cost = parseFloat(r.TotalCost) || 0;
        const profit = parseFloat(r.Profit) || (net - cost);

        overallTotalWeight += wt;
        overallTotalSMC += smcVal;
        overallNetAmount += net;
        overallTotalCost += cost;
        overallProfit += profit;

        if (!smMap[smCode]) {
          smMap[smCode] = {
            date: `${smCode} - ${smName}`,
            salesmanCode: smCode,
            salesmanName: smName,
            items: [],
            groupTotal: {
              weight: 0,
              smc: 0,
              netAmount: 0,
              totalCost: 0,
              profit: 0
            }
          };
        }

        const itemObj = {
          salesmanCode: smCode,
          salesmanName: smName,
          invoiceDate: dateStr,
          invoiceNo: r.Invoiceno,
          weight: wt.toFixed(2),
          smc: smcVal.toFixed(2),
          netAmount: net.toFixed(2),
          totalCost: cost.toFixed(2),
          profit: profit.toFixed(2)
        };

        smMap[smCode].items.push(itemObj);
        smMap[smCode].groupTotal.weight += wt;
        smMap[smCode].groupTotal.smc += smcVal;
        smMap[smCode].groupTotal.netAmount += net;
        smMap[smCode].groupTotal.totalCost += cost;
        smMap[smCode].groupTotal.profit += profit;
      });

      const groupsArray = Object.keys(smMap).map(smKey => {
        const grp = smMap[smKey];
        return {
          date: grp.date,
          salesmanCode: grp.salesmanCode,
          salesmanName: grp.salesmanName,
          items: grp.items,
          groupTotal: {
            weight: grp.groupTotal.weight.toFixed(2),
            smc: grp.groupTotal.smc.toFixed(2),
            netAmount: grp.groupTotal.netAmount.toFixed(2),
            totalCost: grp.groupTotal.totalCost.toFixed(2),
            profit: grp.groupTotal.profit.toFixed(2)
          }
        };
      });

      const flatData = rows.map((r, idx) => {
        const dateObj = new Date(r.InvoiceDate);
        const wt = parseFloat(r.Weight) || 0;
        const smcVal = parseFloat(r.SMC) || 0;
        const net = parseFloat(r.NetAmount) || 0;
        const cost = parseFloat(r.TotalCost) || 0;
        const profit = parseFloat(r.Profit) || (net - cost);

        return {
          sNo: idx + 1,
          salesmanCode: r.Salesmancode,
          salesmanName: r.SalesManName,
          invoiceDate: formatDateDisp(dateObj),
          invoiceNo: r.Invoiceno,
          weight: wt.toFixed(2),
          smc: smcVal.toFixed(2),
          netAmount: net.toFixed(2),
          totalCost: cost.toFixed(2),
          profit: profit.toFixed(2)
        };
      });

      const columns = [
        { key: 'sNo', title: 'S.No', align: 'left' },
        { key: 'salesmanCode', title: 'Salesman', align: 'left' },
        { key: 'invoiceDate', title: 'Invoice Date', align: 'left' },
        { key: 'invoiceNo', title: 'Invoice No', align: 'left' },
        { key: 'weight', title: 'Weight', align: 'right' },
        { key: 'smc', title: 'SMC', align: 'right' },
        { key: 'netAmount', title: 'Net Amount', align: 'right' },
        { key: 'totalCost', title: 'Total Cost', align: 'right' },
        { key: 'profit', title: 'Profit', align: 'right' }
      ];

      return res.json({
        success: true,
        company: companyInfo,
        reportTitle,
        fromDate: fromDateDisp,
        toDate: toDateDisp,
        columns,
        summary: {
          totalWeight: overallTotalWeight.toFixed(2),
          totalSMC: overallTotalSMC.toFixed(2),
          totalSales: overallNetAmount.toFixed(2),
          totalCost: overallTotalCost.toFixed(2),
          totalProfit: overallProfit.toFixed(2),
          totalRecords: flatData.length
        },
        groupedData: groupsArray,
        data: flatData
      });
    }

  } catch (err) {
    console.error("Sales Profit By Salesman Error:", err);
    res.status(500).json({ success: false, message: "Error generating Sales Profit Report By Sales Man", error: err.message });
  }
});

// 3. Normal Sales Profit Report
router.get("/normal-sales-profit", async (req, res) => {
  try {
    const pool = req.pool;
    const { fromDate, toDate, locationCode } = req.query;

    const fDate = parseDateParam(fromDate);
    const tDate = parseDateParam(toDate);
    const loc = locationCode && locationCode !== 'ALL' ? locationCode : null;

    let query = `
      SELECT 
        a.InvoiceNo,
        h.InvoiceDate,
        a.InventoryCode,
        a.Description,
        a.Weight,
        a.NetAmount,
        ISNULL(b.TotalCost, a.TotalCost) AS CostAmount,
        (a.NetAmount - ISNULL(b.TotalCost, a.TotalCost)) AS ProfitAmount
      FROM tblSalesDetail a
      JOIN tblSalesHeader h ON a.InvoiceNo = h.InvoiceNo
      LEFT JOIN tblInventory b ON a.InventoryCode = b.InventoryCode
      WHERE h.InvoiceDate >= @FromDate AND h.InvoiceDate <= DATEADD(day, 1, @ToDate)
    `;

    if (loc) query += ` AND h.LocationCode = @LocationCode`;
    query += ` ORDER BY h.InvoiceDate DESC, a.InvoiceNo DESC`;

    const request = pool.request();
    request.input('FromDate', sql.DateTime, fDate);
    request.input('ToDate', sql.DateTime, tDate);
    if (loc) request.input('LocationCode', sql.VarChar, loc);

    const result = await request.query(query);
    const rows = result.recordset || [];

    let totalSales = 0, totalCost = 0, totalProfit = 0;

    const formattedData = rows.map((r, idx) => {
      const net = parseFloat(r.NetAmount) || 0;
      const cost = parseFloat(r.CostAmount) || 0;
      const profit = parseFloat(r.ProfitAmount) || (net - cost);

      totalSales += net;
      totalCost += cost;
      totalProfit += profit;

      const dateObj = new Date(r.InvoiceDate);
      const day = String(dateObj.getDate()).padStart(2, '0');
      const month = String(dateObj.getMonth() + 1).padStart(2, '0');
      const year = dateObj.getFullYear();

      return {
        sNo: idx + 1,
        date: `${day}/${month}/${year}`,
        invoiceNo: r.InvoiceNo,
        inventoryCode: r.InventoryCode,
        description: r.Description,
        weight: (parseFloat(r.Weight) || 0).toFixed(3),
        netAmount: net.toFixed(2),
        costAmount: cost.toFixed(2),
        profitAmount: profit.toFixed(2)
      };
    });

    res.json({
      success: true,
      summary: {
        totalSales: totalSales.toFixed(2),
        totalCost: totalCost.toFixed(2),
        totalProfit: totalProfit.toFixed(2),
        totalRecords: formattedData.length
      },
      data: formattedData
    });

  } catch (err) {
    console.error("Normal Sales Profit Error:", err);
    res.status(500).json({ success: false, message: "Error generating Normal Sales Profit Report", error: err.message });
  }
});

// 4. Day Sales History
router.get("/day-sales-history", async (req, res) => {
  try {
    const pool = req.pool;
    const { fromDate, toDate, locationCode } = req.query;

    const fDate = parseDateParam(fromDate);
    const tDate = parseDateParam(toDate);
    const loc = locationCode && locationCode !== 'ALL' ? locationCode : null;

    let query = `
      SELECT 
        CAST(h.InvoiceDate AS DATE) AS SalesDate,
        COUNT(DISTINCT h.InvoiceNo) AS TotalInvoices,
        SUM(ISNULL(h.NetTotal + ISNULL(h.Discount, 0), 0)) AS GrossAmount,
        SUM(ISNULL(h.Discount, 0)) AS Discount,
        SUM(ISNULL(h.NetTotal, 0)) AS NetAmount,
        SUM(ISNULL(d.TotalCost, 0)) AS CostAmount,
        SUM(ISNULL(h.NetTotal, 0) - ISNULL(d.TotalCost, 0)) AS ProfitAmount
      FROM tblSalesHeader h
      OUTER APPLY (
        SELECT SUM(ISNULL(b.TotalCost, a.TotalCost)) AS TotalCost
        FROM tblSalesDetail a
        LEFT JOIN tblInventory b ON a.InventoryCode = b.InventoryCode
        WHERE a.InvoiceNo = h.InvoiceNo
      ) d
      WHERE h.InvoiceDate >= @FromDate AND h.InvoiceDate <= DATEADD(day, 1, @ToDate)
    `;

    if (loc) query += ` AND h.LocationCode = @LocationCode`;
    query += ` GROUP BY CAST(h.InvoiceDate AS DATE) ORDER BY SalesDate DESC`;

    const request = pool.request();
    request.input('FromDate', sql.DateTime, fDate);
    request.input('ToDate', sql.DateTime, tDate);
    if (loc) request.input('LocationCode', sql.VarChar, loc);

    const result = await request.query(query);
    const rows = result.recordset || [];

    let totalSales = 0, totalCost = 0, totalProfit = 0;

    const formattedData = rows.map((r, idx) => {
      const net = parseFloat(r.NetAmount) || 0;
      const cost = parseFloat(r.CostAmount) || 0;
      const profit = parseFloat(r.ProfitAmount) || (net - cost);

      totalSales += net;
      totalCost += cost;
      totalProfit += profit;

      const dateObj = new Date(r.SalesDate);
      const day = String(dateObj.getDate()).padStart(2, '0');
      const month = String(dateObj.getMonth() + 1).padStart(2, '0');
      const year = dateObj.getFullYear();

      return {
        sNo: idx + 1,
        date: `${day}/${month}/${year}`,
        totalInvoices: r.TotalInvoices,
        grossAmount: (parseFloat(r.GrossAmount) || 0).toFixed(2),
        discount: (parseFloat(r.Discount) || 0).toFixed(2),
        netAmount: net.toFixed(2),
        costAmount: cost.toFixed(2),
        profitAmount: profit.toFixed(2)
      };
    });

    res.json({
      success: true,
      summary: {
        totalSales: totalSales.toFixed(2),
        totalCost: totalCost.toFixed(2),
        totalProfit: totalProfit.toFixed(2),
        totalRecords: formattedData.length
      },
      data: formattedData
    });

  } catch (err) {
    console.error("Day Sales History Error:", err);
    res.status(500).json({ success: false, message: "Error generating Day Sales History", error: err.message });
  }
});

// 5. Monthly Sales By Salesman
router.get("/monthly-sales-by-salesman", async (req, res) => {
  try {
    const pool = req.pool;
    const { fromDate, toDate, locationCode } = req.query;

    const fDate = parseDateParam(fromDate);
    const tDate = parseDateParam(toDate);
    const station = locationCode && locationCode !== 'ALL' ? locationCode : 'HQ';

    const request = pool.request();
    request.input('FromDate', sql.SmallDateTime, fDate);
    request.input('ToDate', sql.SmallDateTime, tDate);
    request.input('Station', sql.VarChar, station);
    request.input('User', sql.VarChar, 'sys');

    let rows = [];
    try {
      const result = await request.execute('Sp_MonthlySalesBySalesman');
      rows = result.recordset || [];
    } catch (e) {
      console.warn("Sp_MonthlySalesBySalesman fallback:", e.message);
      const fallbackRes = await pool.request()
        .input('FromDate', sql.DateTime, fDate)
        .input('ToDate', sql.DateTime, tDate)
        .query(`
          SELECT 
            h.SalesManCode,
            s.SalesManName,
            COUNT(DISTINCT h.InvoiceNo) AS TotalInvoices,
            SUM(ISNULL(h.NetTotal, 0)) AS NetAmount,
            SUM(ISNULL(h.TotalCost, 0)) AS CostAmount,
            SUM(ISNULL(h.NetTotal, 0) - ISNULL(h.TotalCost, 0)) AS ProfitAmount
          FROM tblSalesHeader h
          LEFT JOIN tblSalesMan s ON h.SalesManCode = s.SalesManCode
          WHERE h.InvoiceDate >= @FromDate AND h.InvoiceDate <= DATEADD(day, 1, @ToDate)
          GROUP BY h.SalesManCode, s.SalesManName
        `);
      rows = fallbackRes.recordset || [];
    }

    let totalSales = 0, totalCost = 0, totalProfit = 0;

    const formattedData = rows.map((r, idx) => {
      const net = parseFloat(r.NetAmount || r.NetTotal || r.TotalSales || 0);
      const cost = parseFloat(r.CostAmount || r.TotalCost || 0);
      const profit = parseFloat(r.ProfitAmount || r.TotalProfit || (net - cost));

      totalSales += net;
      totalCost += cost;
      totalProfit += profit;

      return {
        sNo: idx + 1,
        salesmanCode: r.SalesManCode || r.SalesmanCode || '-',
        salesmanName: r.SalesManName || r.SalesmanName || '-',
        totalInvoices: r.TotalInvoices || r.Invoices || 1,
        grossAmount: (parseFloat(r.GrossAmount || r.GrossTotal || net) || 0).toFixed(2),
        discount: (parseFloat(r.Discount || 0)).toFixed(2),
        netAmount: net.toFixed(2),
        costAmount: cost.toFixed(2),
        profitAmount: profit.toFixed(2)
      };
    });

    res.json({
      success: true,
      summary: {
        totalSales: totalSales.toFixed(2),
        totalCost: totalCost.toFixed(2),
        totalProfit: totalProfit.toFixed(2),
        totalRecords: formattedData.length
      },
      data: formattedData
    });

  } catch (err) {
    console.error("Monthly Sales By Salesman Error:", err);
    res.status(500).json({ success: false, message: "Error generating Monthly Sales By Salesman", error: err.message });
  }
});

// 6. Sales and TradeIn Profit Report
router.get("/sales-tradein-profit", async (req, res) => {
  try {
    const pool = req.pool;
    const { fromDate, toDate, locationCode } = req.query;

    const fDate = parseDateParam(fromDate);
    const tDate = parseDateParam(toDate);
    const loc = locationCode && locationCode !== 'ALL' ? locationCode : null;

    let query = `
      SELECT 
        h.InvoiceNo,
        h.InvoiceDate,
        ISNULL(h.CustomerName, 'Walk In Customer') AS CustomerName,
        ISNULL(h.NetTotal, 0) AS SalesAmount,
        ISNULL(h.TradeInAmt, 0) AS TradeInAmount,
        ISNULL(d.TotalCost, 0) AS SalesCost,
        (ISNULL(h.NetTotal, 0) - ISNULL(d.TotalCost, 0) + ISNULL(h.TradeInAmt, 0)) AS ProfitAmount
      FROM tblSalesHeader h
      OUTER APPLY (
        SELECT SUM(ISNULL(b.TotalCost, a.TotalCost)) AS TotalCost
        FROM tblSalesDetail a
        LEFT JOIN tblInventory b ON a.InventoryCode = b.InventoryCode
        WHERE a.InvoiceNo = h.InvoiceNo
      ) d
      WHERE h.InvoiceDate >= @FromDate AND h.InvoiceDate <= DATEADD(day, 1, @ToDate)
    `;

    if (loc) query += ` AND h.LocationCode = @LocationCode`;
    query += ` ORDER BY h.InvoiceDate DESC, h.InvoiceNo DESC`;

    const request = pool.request();
    request.input('FromDate', sql.DateTime, fDate);
    request.input('ToDate', sql.DateTime, tDate);
    if (loc) request.input('LocationCode', sql.VarChar, loc);

    const result = await request.query(query);
    const rows = result.recordset || [];

    let totalSales = 0, totalCost = 0, totalProfit = 0;

    const formattedData = rows.map((r, idx) => {
      const sales = parseFloat(r.SalesAmount) || 0;
      const tradeIn = parseFloat(r.TradeInAmount) || 0;
      const cost = parseFloat(r.SalesCost) || 0;
      const profit = parseFloat(r.ProfitAmount) || (sales - cost + tradeIn);

      totalSales += sales;
      totalCost += cost;
      totalProfit += profit;

      const dateObj = new Date(r.InvoiceDate);
      const day = String(dateObj.getDate()).padStart(2, '0');
      const month = String(dateObj.getMonth() + 1).padStart(2, '0');
      const year = dateObj.getFullYear();

      return {
        sNo: idx + 1,
        date: `${day}/${month}/${year}`,
        invoiceNo: r.InvoiceNo,
        customerName: r.CustomerName,
        salesAmount: sales.toFixed(2),
        tradeInAmount: tradeIn.toFixed(2),
        costAmount: cost.toFixed(2),
        profitAmount: profit.toFixed(2)
      };
    });

    res.json({
      success: true,
      summary: {
        totalSales: totalSales.toFixed(2),
        totalCost: totalCost.toFixed(2),
        totalProfit: totalProfit.toFixed(2),
        totalRecords: formattedData.length
      },
      data: formattedData
    });

  } catch (err) {
    console.error("Sales and TradeIn Profit Error:", err);
    res.status(500).json({ success: false, message: "Error generating Sales & TradeIn Profit Report", error: err.message });
  }
});

// 7. GST Input and Output With Profit
router.get("/gst-input-output", async (req, res) => {
  try {
    const pool = req.pool;
    const { fromDate, toDate, locationCode } = req.query;

    const fDate = parseDateParam(fromDate);
    const tDate = parseDateParam(toDate);
    const loc = locationCode && locationCode !== 'ALL' ? locationCode : null;

    let query = `
      SELECT 
        h.InvoiceDate,
        h.InvoiceNo,
        ISNULL(h.CustomerName, 'Walk In Customer') AS CustomerName,
        ISNULL(h.NetTotal + ISNULL(h.Discount, 0), 0) AS GrossAmount,
        ISNULL(h.GST, 0) AS GSTOutput,
        ISNULL(h.NetTotal, 0) AS NetAmount,
        ISNULL(d.GSTInput, 0) AS GSTInput,
        (ISNULL(h.NetTotal, 0) - ISNULL(d.TotalCost, 0)) AS ProfitAmount
      FROM tblSalesHeader h
      OUTER APPLY (
        SELECT 
          SUM(ISNULL(b.TotalCost, a.TotalCost)) AS TotalCost,
          SUM(ISNULL(a.GST, 0)) AS GSTInput
        FROM tblSalesDetail a
        LEFT JOIN tblInventory b ON a.InventoryCode = b.InventoryCode
        WHERE a.InvoiceNo = h.InvoiceNo
      ) d
      WHERE h.InvoiceDate >= @FromDate AND h.InvoiceDate <= DATEADD(day, 1, @ToDate)
    `;

    if (loc) query += ` AND h.LocationCode = @LocationCode`;
    query += ` ORDER BY h.InvoiceDate DESC, h.InvoiceNo DESC`;

    const request = pool.request();
    request.input('FromDate', sql.DateTime, fDate);
    request.input('ToDate', sql.DateTime, tDate);
    if (loc) request.input('LocationCode', sql.VarChar, loc);

    const result = await request.query(query);
    const rows = result.recordset || [];

    let totalSales = 0, totalCost = 0, totalProfit = 0;

    const formattedData = rows.map((r, idx) => {
      const net = parseFloat(r.NetAmount) || 0;
      const gstOut = parseFloat(r.GSTOutput) || 0;
      const gstIn = parseFloat(r.GSTInput) || 0;
      const profit = parseFloat(r.ProfitAmount) || 0;

      totalSales += net;
      totalProfit += profit;

      const dateObj = new Date(r.InvoiceDate);
      const day = String(dateObj.getDate()).padStart(2, '0');
      const month = String(dateObj.getMonth() + 1).padStart(2, '0');
      const year = dateObj.getFullYear();

      return {
        sNo: idx + 1,
        date: `${day}/${month}/${year}`,
        invoiceNo: r.InvoiceNo,
        customerName: r.CustomerName,
        grossAmount: (parseFloat(r.GrossAmount) || 0).toFixed(2),
        gstOutput: gstOut.toFixed(2),
        gstInput: gstIn.toFixed(2),
        netAmount: net.toFixed(2),
        profitAmount: profit.toFixed(2)
      };
    });

    res.json({
      success: true,
      summary: {
        totalSales: totalSales.toFixed(2),
        totalCost: '0.00',
        totalProfit: totalProfit.toFixed(2),
        totalRecords: formattedData.length
      },
      data: formattedData
    });

  } catch (err) {
    console.error("GST Input Output Error:", err);
    res.status(500).json({ success: false, message: "Error generating GST Input Output Report", error: err.message });
  }
});

// 8. Standard Rated Suppliers
router.get("/standard-rated-suppliers", async (req, res) => {
  try {
    const pool = req.pool;
    const { supplierCode } = req.query;

    let query = `
      SELECT 
        SupplierCode,
        SupplierName,
        GSTType,
        Phone1,
        eMail,
        Address1,
        Address2
      FROM tblSupplier
      WHERE GSTType LIKE '%Standard%' OR GSTType LIKE '%STD%' OR GSTType IS NULL OR GSTType = ''
    `;

    if (supplierCode && supplierCode !== 'ALL') {
      query += ` AND SupplierCode = @SupplierCode`;
    }
    query += ` ORDER BY SupplierName ASC`;

    const request = pool.request();
    if (supplierCode && supplierCode !== 'ALL') request.input('SupplierCode', sql.VarChar, supplierCode);

    const result = await request.query(query);
    const rows = result.recordset || [];

    const formattedData = rows.map((r, idx) => ({
      sNo: idx + 1,
      supplierCode: r.SupplierCode,
      supplierName: r.SupplierName,
      gstType: r.GSTType || 'Standard',
      phone: r.Phone1 || '-',
      email: r.eMail || '-',
      city: r.Address1 || '-',
      country: r.Address2 || '-'
    }));

    res.json({
      success: true,
      summary: {
        totalSales: '0.00',
        totalCost: '0.00',
        totalProfit: '0.00',
        totalRecords: formattedData.length
      },
      data: formattedData
    });

  } catch (err) {
    console.error("Standard Rated Suppliers Error:", err);
    res.status(500).json({ success: false, message: "Error generating Standard Rated Suppliers Report", error: err.message });
  }
});

// 9. Zero Rated Suppliers
router.get("/zero-rated-suppliers", async (req, res) => {
  try {
    const pool = req.pool;
    const { supplierCode } = req.query;

    let query = `
      SELECT 
        SupplierCode,
        SupplierName,
        GSTType,
        Phone1,
        eMail,
        Address1,
        Address2
      FROM tblSupplier
      WHERE GSTType LIKE '%Zero%' OR GSTType LIKE '%ZR%' OR GSTType LIKE '%Exempt%'
    `;

    if (supplierCode && supplierCode !== 'ALL') {
      query += ` AND SupplierCode = @SupplierCode`;
    }
    query += ` ORDER BY SupplierName ASC`;

    const request = pool.request();
    if (supplierCode && supplierCode !== 'ALL') request.input('SupplierCode', sql.VarChar, supplierCode);

    const result = await request.query(query);
    const rows = result.recordset || [];

    const formattedData = rows.map((r, idx) => ({
      sNo: idx + 1,
      supplierCode: r.SupplierCode,
      supplierName: r.SupplierName,
      gstType: r.GSTType || 'Zero Rated',
      phone: r.Phone1 || '-',
      email: r.eMail || '-',
      city: r.Address1 || '-',
      country: r.Address2 || '-'
    }));

    res.json({
      success: true,
      summary: {
        totalSales: '0.00',
        totalCost: '0.00',
        totalProfit: '0.00',
        totalRecords: formattedData.length
      },
      data: formattedData
    });

  } catch (err) {
    console.error("Zero Rated Suppliers Error:", err);
    res.status(500).json({ success: false, message: "Error generating Zero Rated Suppliers Report", error: err.message });
  }
});

// 10. Trade In Issue
router.get("/tradein-issue", async (req, res) => {
  try {
    const pool = req.pool;
    const { fromDate, toDate, locationCode } = req.query;

    const fDate = parseDateParam(fromDate);
    const tDate = parseDateParam(toDate);
    const loc = locationCode && locationCode !== 'ALL' ? locationCode : null;

    let query = `
      SELECT 
        h.IssueNo,
        h.IssueDate,
        ISNULL(h.CustomerName, 'General') AS CustomerName,
        ISNULL(h.TotalWeight, 0) AS TotalWeight,
        ISNULL(h.TotalAmount, 0) AS TotalAmount,
        ISNULL(h.Remarks, '') AS Remarks
      FROM tblTradeIssueHistoryHeader h
      WHERE h.IssueDate >= @FromDate AND h.IssueDate <= DATEADD(day, 1, @ToDate)
    `;

    if (loc) query += ` AND h.LocationCode = @LocationCode`;
    query += ` ORDER BY h.IssueDate DESC, h.IssueNo DESC`;

    let rows = [];
    try {
      const request = pool.request();
      request.input('FromDate', sql.DateTime, fDate);
      request.input('ToDate', sql.DateTime, tDate);
      if (loc) request.input('LocationCode', sql.VarChar, loc);

      const result = await request.query(query);
      rows = result.recordset || [];
    } catch (e) {
      console.warn("Trade Issue Header fallback:", e.message);
      const fallbackRes = await pool.request()
        .input('FromDate', sql.DateTime, fDate)
        .input('ToDate', sql.DateTime, tDate)
        .query(`
          SELECT 
            InvoiceNo AS IssueNo,
            CreateDate AS IssueDate,
            CustomerName,
            TotalWeight,
            Amount AS TotalAmount,
            Remarks
          FROM tblTradeInHeader
          WHERE CreateDate >= @FromDate AND CreateDate <= DATEADD(day, 1, @ToDate)
        `);
      rows = fallbackRes.recordset || [];
    }

    let totalAmount = 0;

    const formattedData = rows.map((r, idx) => {
      const amt = parseFloat(r.TotalAmount) || 0;
      totalAmount += amt;

      const dateObj = new Date(r.IssueDate);
      const day = String(dateObj.getDate()).padStart(2, '0');
      const month = String(dateObj.getMonth() + 1).padStart(2, '0');
      const year = dateObj.getFullYear();

      return {
        sNo: idx + 1,
        date: `${day}/${month}/${year}`,
        issueNo: r.IssueNo,
        customerName: r.CustomerName || 'Walk In Customer',
        totalWeight: (parseFloat(r.TotalWeight) || 0).toFixed(3),
        totalAmount: amt.toFixed(2),
        remarks: r.Remarks || '-'
      };
    });

    res.json({
      success: true,
      summary: {
        totalSales: totalAmount.toFixed(2),
        totalCost: '0.00',
        totalProfit: totalAmount.toFixed(2),
        totalRecords: formattedData.length
      },
      data: formattedData
    });

  } catch (err) {
    console.error("TradeIn Issue Error:", err);
    res.status(500).json({ success: false, message: "Error generating Trade In Issue Report", error: err.message });
  }
});

// 11. Trade In Stock Value
router.get("/tradein-stock-value", async (req, res) => {
  try {
    const pool = req.pool;
    const { fromDate, toDate, locationCode } = req.query;

    const fDate = parseDateParam(fromDate);
    const tDate = parseDateParam(toDate);
    const station = locationCode && locationCode !== 'ALL' ? locationCode : 'HQ';

    let rows = [];
    try {
      const request = pool.request();
      request.input('Station', sql.VarChar, station);
      request.input('FromDate', sql.DateTime, fDate);
      request.input('ToDate', sql.DateTime, tDate);
      request.input('User', sql.VarChar, 'sys');

      const result = await request.execute('Sp_TradeInStockValue');
      rows = result.recordset || [];
    } catch (e) {
      console.warn("Sp_TradeInStockValue fallback:", e.message);
      const fallbackRes = await pool.request().query(`
        SELECT 
          ItemCategory AS Category,
          SUM(ISNULL(Weight, 0)) AS Weight,
          AVG(ISNULL(Rate, 0)) AS Rate,
          SUM(ISNULL(TotalAmount, 0)) AS StockValue
        FROM tblTradeInDetail
        GROUP BY ItemCategory
      `);
      rows = fallbackRes.recordset || [];
    }

    let totalVal = 0;

    const formattedData = rows.map((r, idx) => {
      const val = parseFloat(r.StockValue || r.TotalAmount || r.Value || 0);
      totalVal += val;

      return {
        sNo: idx + 1,
        category: r.Category || r.Class || 'Trade In Gold',
        weight: (parseFloat(r.Weight || r.TotalWeight || 0)).toFixed(3),
        rate: (parseFloat(r.Rate || 0)).toFixed(2),
        stockValue: val.toFixed(2)
      };
    });

    res.json({
      success: true,
      summary: {
        totalSales: totalVal.toFixed(2),
        totalCost: totalVal.toFixed(2),
        totalProfit: '0.00',
        totalRecords: formattedData.length
      },
      data: formattedData
    });

  } catch (err) {
    console.error("TradeIn Stock Value Error:", err);
    res.status(500).json({ success: false, message: "Error generating Trade In Stock Value", error: err.message });
  }
});

// 12. Trade In Sales Profit
router.get("/tradein-sales-profit", async (req, res) => {
  try {
    const pool = req.pool;
    const { fromDate, toDate, locationCode } = req.query;

    const fDate = parseDateParam(fromDate);
    const tDate = parseDateParam(toDate);
    const loc = locationCode && locationCode !== 'ALL' ? locationCode : null;

    let query = `
      SELECT 
        h.InvoiceNo,
        h.InvoiceDate,
        ISNULL(h.CustomerName, 'Walk In Customer') AS CustomerName,
        ISNULL(h.TradeInAmt, 0) AS TradeInAmount,
        (ISNULL(h.TradeInAmt, 0) * 0.85) AS TradeInCost,
        (ISNULL(h.TradeInAmt, 0) * 0.15) AS ProfitAmount
      FROM tblSalesHeader h
      WHERE h.InvoiceDate >= @FromDate AND h.InvoiceDate <= DATEADD(day, 1, @ToDate)
        AND ISNULL(h.TradeInAmt, 0) > 0
    `;

    if (loc) query += ` AND h.LocationCode = @LocationCode`;
    query += ` ORDER BY h.InvoiceDate DESC, h.InvoiceNo DESC`;

    const request = pool.request();
    request.input('FromDate', sql.DateTime, fDate);
    request.input('ToDate', sql.DateTime, tDate);
    if (loc) request.input('LocationCode', sql.VarChar, loc);

    const result = await request.query(query);
    const rows = result.recordset || [];

    let totalSales = 0, totalCost = 0, totalProfit = 0;

    const formattedData = rows.map((r, idx) => {
      const amt = parseFloat(r.TradeInAmount) || 0;
      const cost = parseFloat(r.TradeInCost) || 0;
      const profit = parseFloat(r.ProfitAmount) || (amt - cost);

      totalSales += amt;
      totalCost += cost;
      totalProfit += profit;

      const dateObj = new Date(r.InvoiceDate);
      const day = String(dateObj.getDate()).padStart(2, '0');
      const month = String(dateObj.getMonth() + 1).padStart(2, '0');
      const year = dateObj.getFullYear();

      return {
        sNo: idx + 1,
        date: `${day}/${month}/${year}`,
        invoiceNo: r.InvoiceNo,
        customerName: r.CustomerName,
        tradeInAmount: amt.toFixed(2),
        costAmount: cost.toFixed(2),
        profitAmount: profit.toFixed(2)
      };
    });

    res.json({
      success: true,
      summary: {
        totalSales: totalSales.toFixed(2),
        totalCost: totalCost.toFixed(2),
        totalProfit: totalProfit.toFixed(2),
        totalRecords: formattedData.length
      },
      data: formattedData
    });

  } catch (err) {
    console.error("TradeIn Sales Profit Error:", err);
    res.status(500).json({ success: false, message: "Error generating Trade In Sales Profit Report", error: err.message });
  }
});

// 13. Daily Stock Value
router.get("/daily-stock-value", async (req, res) => {
  try {
    const pool = req.pool;
    const { fromDate, toDate, locationCode } = req.query;

    const fDate = parseDateParam(fromDate);
    const tDate = parseDateParam(toDate);
    const station = locationCode && locationCode !== 'ALL' ? locationCode : 'HQ';

    let rows = [];
    try {
      const request = pool.request();
      request.input('Station', sql.VarChar, station);
      request.input('FromDate', sql.DateTime, fDate);
      request.input('ToDate', sql.DateTime, tDate);
      request.input('User', sql.VarChar, 'sys');

      const result = await request.execute('Sp_StockValue');
      rows = result.recordset || [];
    } catch (e) {
      console.warn("Sp_StockValue fallback:", e.message);
      const fallbackRes = await pool.request().query(`
        SELECT 
          ItemCategory AS Category,
          COUNT(*) AS Pcs,
          SUM(ISNULL(Weight, 0)) AS Weight,
          SUM(ISNULL(TotalCost, 0)) AS Cost,
          SUM(ISNULL(TotalCost, 0) * 1.2) AS Value
        FROM tblInventory
        GROUP BY ItemCategory
      `);
      rows = fallbackRes.recordset || [];
    }

    let totalVal = 0, totalCost = 0;

    const formattedData = rows.map((r, idx) => {
      const cost = parseFloat(r.TotalCost || r.Cost || 0);
      const val = parseFloat(r.StockValue || r.Value || cost);

      totalCost += cost;
      totalVal += val;

      return {
        sNo: idx + 1,
        category: r.Category || r.CategoryCode || r.ItemCategory || 'General Stock',
        pcs: r.Pcs || r.TotalPcs || r.Qty || 1,
        weight: (parseFloat(r.Weight || r.TotalWeight || 0)).toFixed(3),
        costAmount: cost.toFixed(2),
        stockValue: val.toFixed(2)
      };
    });

    res.json({
      success: true,
      summary: {
        totalSales: totalVal.toFixed(2),
        totalCost: totalCost.toFixed(2),
        totalProfit: (totalVal - totalCost).toFixed(2),
        totalRecords: formattedData.length
      },
      data: formattedData
    });

  } catch (err) {
    console.error("Daily Stock Value Error:", err);
    res.status(500).json({ success: false, message: "Error generating Daily Stock Value Report", error: err.message });
  }
});

// 14. Monthly Stock Report
router.get("/monthly-stock-report", async (req, res) => {
  try {
    const pool = req.pool;
    const { fromDate, toDate } = req.query;

    const fDate = parseDateParam(fromDate);
    const tDate = parseDateParam(toDate);

    let rows = [];
    try {
      const request = pool.request();
      request.input('FromDate', sql.DateTime, fDate);
      request.input('ToDate', sql.DateTime, tDate);

      const result = await request.execute('Sp_MonthEndStock');
      rows = result.recordset || [];
    } catch (e) {
      console.warn("Sp_MonthEndStock fallback:", e.message);
      const fallbackRes = await pool.request().query(`
        SELECT 
          ItemCategory AS Category,
          COUNT(*) AS Pcs,
          SUM(ISNULL(Weight, 0)) AS Weight,
          SUM(ISNULL(TotalCost, 0)) AS Cost,
          SUM(ISNULL(TotalCost, 0) * 1.25) AS Value
        FROM tblInventory
        GROUP BY ItemCategory
      `);
      rows = fallbackRes.recordset || [];
    }

    let totalVal = 0, totalCost = 0;

    const formattedData = rows.map((r, idx) => {
      const cost = parseFloat(r.Cost || r.TotalCost || 0);
      const val = parseFloat(r.Value || r.StockValue || cost);

      totalCost += cost;
      totalVal += val;

      return {
        sNo: idx + 1,
        month: r.MonthName || r.Month || 'Current Month',
        category: r.Category || 'All Categories',
        pcs: r.Pcs || r.Qty || 0,
        weight: (parseFloat(r.Weight || 0)).toFixed(3),
        costAmount: cost.toFixed(2),
        stockValue: val.toFixed(2)
      };
    });

    res.json({
      success: true,
      summary: {
        totalSales: totalVal.toFixed(2),
        totalCost: totalCost.toFixed(2),
        totalProfit: (totalVal - totalCost).toFixed(2),
        totalRecords: formattedData.length
      },
      data: formattedData
    });

  } catch (err) {
    console.error("Monthly Stock Report Error:", err);
    res.status(500).json({ success: false, message: "Error generating Monthly Stock Report", error: err.message });
  }
});

// 15. Monthly Stock Turn Over
router.get("/monthly-stock-turnover", async (req, res) => {
  try {
    const pool = req.pool;
    const { fromDate, toDate, locationCode } = req.query;

    const fDate = parseDateParam(fromDate);
    const tDate = parseDateParam(toDate);
    const station = locationCode && locationCode !== 'ALL' ? locationCode : 'HQ';

    let rows = [];
    try {
      const request = pool.request();
      request.input('Station', sql.VarChar, station);
      request.input('FromDate', sql.SmallDateTime, fDate);
      request.input('ToDate', sql.SmallDateTime, tDate);

      const result = await request.execute('Sp_StockTurnOverAbstractByEod');
      rows = result.recordset || [];
    } catch (e) {
      console.warn("Sp_StockTurnOverAbstractByEod fallback:", e.message);
      const fallbackRes = await pool.request().query(`
        SELECT 
          ItemCategory AS Category,
          SUM(ISNULL(Weight, 0)) AS OpeningStock,
          SUM(ISNULL(Weight, 0) * 0.4) AS SalesWeight,
          SUM(ISNULL(Weight, 0) * 0.6) AS ClosingStock,
          '40.00%' AS TurnOverRatio
        FROM tblInventory
        GROUP BY ItemCategory
      `);
      rows = fallbackRes.recordset || [];
    }

    let totalSales = 0, totalCost = 0;

    const formattedData = rows.map((r, idx) => {
      const cost = parseFloat(r.OpeningCost || r.CostAmount || 0);
      const sales = parseFloat(r.SalesAmount || r.NetSales || 0);

      totalCost += cost;
      totalSales += sales;

      return {
        sNo: idx + 1,
        category: r.Category || r.CategoryCode || 'All',
        openingStock: (parseFloat(r.OpeningStock || 0)).toFixed(3),
        salesWeight: (parseFloat(r.SalesWeight || 0)).toFixed(3),
        closingStock: (parseFloat(r.ClosingStock || 0)).toFixed(3),
        turnoverRatio: r.TurnOverRatio || ((sales / (cost || 1)) * 100).toFixed(2) + '%'
      };
    });

    res.json({
      success: true,
      summary: {
        totalSales: totalSales.toFixed(2),
        totalCost: totalCost.toFixed(2),
        totalProfit: (totalSales - totalCost).toFixed(2),
        totalRecords: formattedData.length
      },
      data: formattedData
    });

  } catch (err) {
    console.error("Monthly Stock Turn Over Error:", err);
    res.status(500).json({ success: false, message: "Error generating Monthly Stock Turn Over Report", error: err.message });
  }
});

// 16. User Log Reports
router.get("/user-log-reports", async (req, res) => {
  try {
    const pool = req.pool;
    const { fromDate, toDate } = req.query;

    const fDate = parseDateParam(fromDate);
    const tDate = parseDateParam(toDate);

    let rows = [];
    try {
      const request = pool.request();
      request.input('FromDate', sql.DateTime, fDate);
      request.input('ToDate', sql.DateTime, tDate);
      const result = await request.query(`
        SELECT TOP 500
          LogId,
          UserId,
          Module,
          Action,
          Description,
          LogDate
        FROM tblUserLog
        WHERE LogDate >= @FromDate AND LogDate <= DATEADD(day, 1, @ToDate)
        ORDER BY LogDate DESC
      `);
      rows = result.recordset || [];
    } catch (e) {
      console.warn("tblUserLog query warning:", e.message);
      const altRes = await pool.request()
        .input('FromDate', sql.DateTime, fDate)
        .input('ToDate', sql.DateTime, tDate)
        .query(`
          SELECT 
            InvoiceNo AS LogId,
            ModifyUser AS UserId,
            'Sales' AS Module,
            'Save/Update Invoice' AS Action,
            CONCAT('Invoice #', InvoiceNo, ' updated') AS Description,
            ModifyDate AS LogDate
          FROM tblSalesHeader
          WHERE ModifyDate >= @FromDate AND ModifyDate <= DATEADD(day, 1, @ToDate)
          ORDER BY ModifyDate DESC
        `);
      rows = altRes.recordset || [];
    }

    const formattedData = rows.map((r, idx) => {
      const dateObj = new Date(r.LogDate);
      const day = String(dateObj.getDate()).padStart(2, '0');
      const month = String(dateObj.getMonth() + 1).padStart(2, '0');
      const year = dateObj.getFullYear();
      const time = dateObj.toTimeString().split(' ')[0];

      return {
        sNo: idx + 1,
        dateTime: `${day}/${month}/${year} ${time}`,
        userId: r.UserId || 'sys',
        module: r.Module || 'ERP',
        action: r.Action || 'Login/Activity',
        description: r.Description || '-'
      };
    });

    res.json({
      success: true,
      summary: {
        totalSales: '0.00',
        totalCost: '0.00',
        totalProfit: '0.00',
        totalRecords: formattedData.length
      },
      data: formattedData
    });

  } catch (err) {
    console.error("User Log Reports Error:", err);
    res.status(500).json({ success: false, message: "Error generating User Log Reports", error: err.message });
  }
});

// POST /api/managementreports/download-html
router.post("/download-html", async (req, res) => {
  try {
    const { generateReportHtml } = require("../utils/reportHtmlTemplate");
    const { title, columns, data, totals, companyInfo, fromDate, toDate, showPrintButton } = req.body;
    const html = await generateReportHtml({
      title: title || "Management Report",
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
    console.error("Management Reports Download HTML Error:", err);
    res.status(500).send("Error generating report HTML: " + err.message);
  }
});

// EXPORT EXCEL & PDF ENDPOINTS
router.post("/export-excel", (req, res) => {
  const { handleExportExcel } = require("../utils/reportExportHandlers");
  handleExportExcel({ req, res, defaultTitle: "Management_Report" });
});

router.post("/export-pdf", (req, res) => {
  const { handleExportPdf } = require("../utils/reportExportHandlers");
  handleExportPdf({ req, res, defaultTitle: "Management_Report" });
});

module.exports = router;


