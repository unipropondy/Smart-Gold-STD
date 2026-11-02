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

// ==========================================
// 1. MASTER DATA / LOV ENDPOINTS
// ==========================================

// GET /api/salesreports/locations
router.get("/locations", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query(`
      SELECT RTRIM(LocationCode) AS LocationCode, RTRIM(LocationName) AS LocationName
      FROM tblLocation WITH (NOLOCK)
      ORDER BY SortCode, LocationCode
    `);

    const data = [
      { LocationCode: "ALL", LocationName: "ALL - All Locations" },
      ...result.recordset
    ];
    res.json({ success: true, data });
  } catch (err) {
    console.error("Error fetching sales locations:", err);
    res.status(500).json({ success: false, message: err.message });
  }
});

// GET /api/salesreports/suppliers
router.get("/suppliers", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query(`
      SELECT RTRIM(SupplierCode) AS SupplierCode, RTRIM(SupplierName) AS SupplierName
      FROM tblSupplier WITH (NOLOCK)
      ORDER BY SupplierCode
    `);

    const data = [
      { SupplierCode: "ALL", SupplierName: "ALL - All Suppliers" },
      ...result.recordset
    ];
    res.json({ success: true, data });
  } catch (err) {
    console.error("Error fetching sales suppliers:", err);
    res.status(500).json({ success: false, message: err.message });
  }
});

// GET /api/salesreports/classes
router.get("/classes", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query(`
      SELECT RTRIM(ClassCode) AS ClassCode, RTRIM(ClassName) AS ClassName
      FROM tblClass WITH (NOLOCK)
      ORDER BY ClassCode
    `);

    const data = [
      { ClassCode: "ALL", ClassName: "ALL - All Classes" },
      ...result.recordset
    ];
    res.json({ success: true, data });
  } catch (err) {
    console.error("Error fetching sales classes:", err);
    res.status(500).json({ success: false, message: err.message });
  }
});

// GET /api/salesreports/categories
router.get("/categories", async (req, res) => {
  try {
    const pool = req.pool;
    const { classCode } = req.query;

    const request = pool.request();
    let query = `
      SELECT RTRIM(CategoryCode) AS CategoryCode, RTRIM(CategoryName) AS CategoryName, RTRIM(ClassCode) AS ClassCode
      FROM tblCategory WITH (NOLOCK)
      WHERE 1=1
    `;

    if (classCode && classCode !== "ALL") {
      request.input("ClassCode", sql.VarChar, classCode.trim());
      query += ` AND RTRIM(ClassCode) = @ClassCode`;
    }

    query += ` ORDER BY CategoryCode`;
    const result = await request.query(query);

    const data = [
      { CategoryCode: "ALL", CategoryName: "ALL - All Categories" },
      ...result.recordset
    ];
    res.json({ success: true, data });
  } catch (err) {
    console.error("Error fetching sales categories:", err);
    res.status(500).json({ success: false, message: err.message });
  }
});

// GET /api/salesreports/catalogues
router.get("/catalogues", async (req, res) => {
  try {
    const pool = req.pool;
    const { categoryCode } = req.query;

    const request = pool.request();
    let query = `
      SELECT RTRIM(CatalogueCode) AS CatalogueCode, RTRIM(CatalogueName) AS CatalogueName, RTRIM(CategoryCode) AS CategoryCode
      FROM tblCatalogue WITH (NOLOCK)
      WHERE 1=1
    `;

    if (categoryCode && categoryCode !== "ALL") {
      request.input("CategoryCode", sql.VarChar, categoryCode.trim());
      query += ` AND RTRIM(CategoryCode) = @CategoryCode`;
    }

    query += ` ORDER BY CatalogueCode`;
    const result = await request.query(query);

    const data = [
      { CatalogueCode: "ALL", CatalogueName: "ALL - All Catalogues" },
      ...result.recordset
    ];
    res.json({ success: true, data });
  } catch (err) {
    console.error("Error fetching sales catalogues:", err);
    res.status(500).json({ success: false, message: err.message });
  }
});

// GET /api/salesreports/sizes
router.get("/sizes", async (req, res) => {
  try {
    const pool = req.pool;
    const { categoryCode } = req.query;

    const request = pool.request();
    let query = `
      SELECT DISTINCT RTRIM(CAST(SizeCode AS VARCHAR(20))) AS SizeCode, RTRIM(SizeType) AS SizeType, RTRIM(CategoryCode) AS CategoryCode
      FROM tblSize WITH (NOLOCK)
      WHERE 1=1
    `;

    if (categoryCode && categoryCode !== "ALL") {
      request.input("CategoryCode", sql.VarChar, categoryCode.trim());
      query += ` AND RTRIM(CategoryCode) = @CategoryCode`;
    }

    query += ` ORDER BY SizeCode`;
    const result = await request.query(query);

    const data = [
      { SizeCode: "ALL", SizeType: "ALL - All Sizes" },
      ...result.recordset
    ];
    res.json({ success: true, data });
  } catch (err) {
    console.error("Error fetching sales sizes:", err);
    res.status(500).json({ success: false, message: err.message });
  }
});

// GET /api/salesreports/paymodes
router.get("/paymodes", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query(`
      SELECT DISTINCT RTRIM(PayMode) AS PayMode
      FROM tblPayModeDetail WITH (NOLOCK)
      WHERE PayMode IS NOT NULL AND RTRIM(PayMode) <> ''
      UNION
      SELECT DISTINCT RTRIM(PayMode) AS PayMode
      FROM tblPaymode WITH (NOLOCK)
      ORDER BY PayMode
    `);

    const data = [
      { PayMode: "ALL", PayModeName: "ALL - All Paymodes" },
      ...result.recordset.map((r) => ({ PayMode: r.PayMode, PayModeName: r.PayMode }))
    ];
    res.json({ success: true, data });
  } catch (err) {
    console.error("Error fetching paymodes:", err);
    res.status(500).json({ success: false, message: err.message });
  }
});

// GET /api/salesreports/trade-in-options
router.get("/trade-in-options", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query(`
      SELECT DISTINCT RTRIM(InventoryCode) AS Code, RTRIM(Description) AS Name
      FROM tblInventory WITH (NOLOCK)
      WHERE ClassCode = 'TI' OR InventoryCode LIKE 'TI%'
      ORDER BY Code
    `);

    const data = [
      { Code: "ALL", Name: "ALL - All Trade In Categories" },
      ...result.recordset
    ];
    res.json({ success: true, data });
  } catch (err) {
    console.error("Error fetching trade in options:", err);
    res.status(500).json({ success: false, message: err.message });
  }
});

// GET /api/salesreports/salesmen
router.get("/salesmen", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query(`
      SELECT RTRIM(SalesManCode) AS SalesManCode, RTRIM(SalesManName) AS SalesManName
      FROM tblSalesMan WITH (NOLOCK)
      ORDER BY SalesManCode
    `);

    const data = [
      { SalesManCode: "ALL", SalesManName: "ALL - All Salesmen" },
      ...result.recordset
    ];
    res.json({ success: true, data });
  } catch (err) {
    console.error("Error fetching salesmen:", err);
    res.status(500).json({ success: false, message: err.message });
  }
});

// GET /api/salesreports/customers
router.get("/customers", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query(`
      SELECT RTRIM(CustomerCode) AS CustomerCode, RTRIM(CustomerName) AS CustomerName
      FROM tblCustomer WITH (NOLOCK)
      ORDER BY CustomerName
    `);

    const data = [
      { CustomerCode: "ALL", CustomerName: "ALL - All Customers" },
      ...result.recordset
    ];
    res.json({ success: true, data });
  } catch (err) {
    console.error("Error fetching customers:", err);
    res.status(500).json({ success: false, message: err.message });
  }
});

// GET /api/salesreports/trays
router.get("/trays", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query(`
      SELECT RTRIM(TrayId) AS TrayId, RTRIM(TrayName) AS TrayName
      FROM tblTrayMaster WITH (NOLOCK)
      ORDER BY TrayId
    `);

    const data = [
      { TrayId: "ALL", TrayName: "ALL - All Trays" },
      ...result.recordset
    ];
    res.json({ success: true, data });
  } catch (err) {
    console.error("Error fetching trays:", err);
    res.status(500).json({ success: false, message: err.message });
  }
});

// ==========================================
// 2. REPORT GENERATION ENGINE
// ==========================================

const SUPPORTED_REPORTS = [
  "sales_journal",
  "sales_by_weight_pcs",
  "trade_in_sales",
  "trade_in_only",
  "trade_in_sold",
  "installment_entry",
  "installment_received",
  "pending_installment",
  "sales_return",
  "bill_cancel",
  "daily_sales",
  "sales_by_supplier",
  "sales_by_customer",
  "pending_order",
  "sales_journal_by_category",
  "sales_journal_by_catalogue",
  "paymode_wise_sales",
  "salesman_wise_journal",
  "salesman_analysis_smc",
  "sales_journal_market_rate",
  "below_cost_sales",
  "tray_wise_sales",
  "pay_out",
  "gst_report",
  "flexi_payments",
  "deposit_journal",
  "etrs_statement",
  "misc_journal"
];

router.post("/generate", async (req, res) => {
  try {
    const pool = req.pool;
    const {
      reportId = "sales_journal",
      reportType = "Summary", // "Summary" or "Detail"
      filters = {}
    } = req.body;

    if (!SUPPORTED_REPORTS.includes(reportId)) {
      return res.status(400).json({
        success: false,
        message: `Unsupported report: ${reportId}. Supported options: ${SUPPORTED_REPORTS.join(", ")}`
      });
    }

    const {
      locationCode = "ALL",
      supplierCode = "ALL",
      customerCode = "ALL",
      trayId = "ALL",
      classCode,
      categoryCode,
      catalogueCode,
      sizeCode,
      paymode = "ALL",
      salesmanCode = "ALL",
      tradeInOption = "ALL",
      purity = "ALL",
      tradeInStatus = "All", // "All", "Stock", "Sold"
      miscType = "ALL", // "Purchase Note", "Credit Note", "Misc.Note", "Petty Cash"
      fromDate,
      toDate,
      gstOption = "All", // "All", "With GST", "Zero Rated"
      weightPcsChoice = "2", // "2", "4", "6", "8"
      akshayaTritiya = false,
      voidFilter = false,
      taggingFilter = false,
      etrsStatus = "All" // "All", "eTRS Ticket Issued", "Not for eTRS", "eTRS Eligible"
    } = filters;

    // Date range validation
    if (!fromDate) {
      return res.status(400).json({
        success: false,
        message: "From Date is required."
      });
    }

    const dateVal = validateBackendDates(fromDate, toDate);
    if (!dateVal.isValid) {
      return res.status(400).json({ success: false, message: dateVal.message });
    }

    const range = getInclusiveRange(fromDate, toDate);
    if (range.fromDate > range.toDate) {
      return res.status(400).json({
        success: false,
        message: "From Date must be less than or equal to To Date."
      });
    }

    const fDate = range.fromDate;
    const tDateEx = range.toDateExclusive;
    const fromDateDisp = formatDateDisp(fDate);
    const toDateDisp = formatDateDisp(range.toDate);

    const request = pool.request();
    request.input("FromDate", sql.DateTime, fDate);
    request.input("ToDateExclusive", sql.DateTime, tDateEx);

    const isSummary = (reportType || "Summary").toLowerCase() === "summary";

    // Fetch company info
    let companyInfo = {};
    try {
      const compRes = await pool.request().query(`
        SELECT TOP 1 
          RTRIM(Name) AS CompanyName,
          RTRIM(Address1) AS Address1,
          RTRIM(Address2) AS Address2,
          RTRIM(City) AS City,
          RTRIM(Country) AS Country,
          RTRIM(Phone) AS Phone,
          RTRIM(Fax) AS Fax,
          RTRIM(CurrencySign) AS CurrencySign
        FROM tblCompany WITH (NOLOCK)
      `);
      if (compRes.recordset.length > 0) companyInfo = compRes.recordset[0];
    } catch (e) {
      console.warn("Could not fetch company info:", e.message);
    }

    let title = "";
    let columns = [];
    let rows = [];

    // Helper to build standard Sales Header WHERE clause
    const buildSalesHeaderWhere = (alias = "sh") => {
      let where = ` WHERE ${alias}.InvoiceDate >= @FromDate AND ${alias}.InvoiceDate < @ToDateExclusive`;

      if (locationCode && locationCode !== "ALL" && locationCode.trim() !== "") {
        request.input("LocationCode", sql.VarChar, locationCode.trim());
        where += ` AND ${alias}.LocationCode = @LocationCode`;
      }

      if (gstOption === "With GST") {
        where += ` AND (${alias}.GSTType <> 'Z')`;
      } else if (gstOption === "Zero Rated") {
        where += ` AND (${alias}.GSTType = 'Z')`;
      }

      if (etrsStatus === "eTRS Ticket Issued") {
        where += ` AND ${alias}.JournalNo = 'Y'`;
      } else if (etrsStatus === "eTRS Eligible") {
        where += ` AND ${alias}.JournalNo = 'E'`;
      } else if (etrsStatus === "Not for eTRS") {
        where += ` AND ${alias}.JournalNo = 'N'`;
      }

      return where;
    };

    // ==========================================
    // 1. SALES JOURNAL (Summary & Detail)
    // ==========================================
    if (reportId === "sales_journal") {
      title = `${isSummary ? "Summary" : "Detail"} Sales Journal From ${fromDateDisp} To ${toDateDisp}`;
      if (locationCode && locationCode !== "ALL") title += ` (Location: ${locationCode})`;

      let where = buildSalesHeaderWhere("sh");

      if (classCode && classCode !== "ALL" && classCode.trim() !== "") {
        request.input("ClassCode", sql.VarChar, classCode.trim());
        where += ` AND inv.ClassCode = @ClassCode`;
      }
      if (categoryCode && categoryCode !== "ALL" && categoryCode.trim() !== "") {
        request.input("CategoryCode", sql.VarChar, categoryCode.trim());
        where += ` AND inv.CategoryCode = @CategoryCode`;
      }
      if (catalogueCode && catalogueCode !== "ALL" && catalogueCode.trim() !== "") {
        request.input("CatalogueCode", sql.VarChar, catalogueCode.trim());
        where += ` AND inv.CatalogueCode = @CatalogueCode`;
      }
      if (sizeCode && sizeCode !== "ALL" && sizeCode.trim() !== "") {
        request.input("SizeCode", sql.VarChar, sizeCode.trim());
        where += ` AND inv.SizeCode = @SizeCode`;
      }

      if (isSummary) {
        // Sales Journal — Summary Report matching legacy Crystal Report exactly:
        // Columns:
        // 1. INVOICE DATE
        // 2. INVOICE NO
        // 3. CUSTOMER NAME
        // 4. WEIGHT
        // 5. VALUE
        // 6. T-IN WEIGHT
        // 7. T-IN VALUE
        // 8. BEFORE GST
        // 9. GST @ 9%
        // 10. GRAND TOTAL
        columns = [
          { key: "InvoiceDate", label: "INVOICE DATE", width: 110, format: "date" },
          { key: "InvoiceNo", label: "INVOICE NO", width: 130 },
          { key: "CustomerName", label: "CUSTOMER NAME", width: 180 },
          { key: "Weight", label: "WEIGHT", width: 100, align: "right", format: "decimal2" },
          { key: "Value", label: "VALUE", width: 110, align: "right", format: "currency" },
          { key: "TInWeight", label: "T-IN WEIGHT", width: 100, align: "right", format: "decimal2" },
          { key: "TInValue", label: "T-IN VALUE", width: 100, align: "right", format: "currency" },
          { key: "BeforeGST", label: "BEFORE GST", width: 120, align: "right", format: "currency" },
          { key: "GST", label: "GST @ 9%", width: 100, align: "right", format: "currency" },
          { key: "GrandTotal", label: "GRAND TOTAL", width: 120, align: "right", format: "currency" }
        ];

        let query = `
          SELECT 
            sh.InvoiceDate,
            RTRIM(sh.InvoiceNo) AS InvoiceNo,
            ISNULL(RTRIM(c.CustomerName), ISNULL(RTRIM(sh.CustomerName), RTRIM(sh.CustomerCode))) AS CustomerName,
            ROUND(ISNULL(sh.TotalWeight, 0), 2) AS Weight,
            CASE 
              WHEN sh.TotalValue > 0 THEN ROUND(ISNULL(sh.TotalValue, 0) - ISNULL(sh.GST, 0), 2)
              ELSE NULL 
            END AS Value,
            CASE 
              WHEN sh.TotalValue > 0 THEN ROUND(ISNULL(ti.TInWeight, 0), 2)
              ELSE NULL 
            END AS TInWeight,
            CASE 
              WHEN sh.TotalValue > 0 THEN ROUND(ISNULL(ti.TInValue, 0), 2)
              ELSE NULL 
            END AS TInValue,
            CASE 
              WHEN sh.TotalValue > 0 THEN ROUND(ISNULL(sh.TotalValue, 0) - ISNULL(sh.GST, 0), 2)
              ELSE ROUND(ISNULL(sh.TotalValue, 0), 2)
            END AS BeforeGST,
            ROUND(ISNULL(sh.GST, 0), 2) AS GST,
            ROUND(ISNULL(sh.NetTotal, 0), 2) AS GrandTotal,
            ISNULL(sh.TotalPCS, 1) AS TotalPCS
          FROM SmartGold.dbo.tblSalesHeader AS sh WITH (NOLOCK)
          LEFT OUTER JOIN SmartGold.dbo.tblCustomer AS c WITH (NOLOCK) 
            ON RTRIM(sh.CustomerCode) = RTRIM(c.CustomerCode)
          LEFT OUTER JOIN (
            SELECT 
              RTRIM(InvoiceNo) AS InvoiceNo,
              SUM(ABS(Weight)) AS TInWeight,
              SUM(ABS(TotalValue)) AS TInValue
            FROM SmartGold.dbo.tblSalesDetail WITH (NOLOCK)
            WHERE LEFT(InventoryCode, 2) = 'TI'
            GROUP BY InvoiceNo
          ) AS ti ON RTRIM(sh.InvoiceNo) = ti.InvoiceNo
        `;

        if (classCode || categoryCode || catalogueCode || sizeCode) {
          query += `
            INNER JOIN SmartGold.dbo.vsSalesJournalRpt sd WITH (NOLOCK) ON RTRIM(sh.InvoiceNo) = RTRIM(sd.InvoiceNo)
            LEFT JOIN SmartGold.dbo.tblInventory inv WITH (NOLOCK) ON RTRIM(sd.InventoryCode) = RTRIM(inv.InventoryCode)
          `;
        }

        query += `
          ${where}
          GROUP BY sh.InvoiceDate, sh.InvoiceNo, c.CustomerName, sh.CustomerName, sh.CustomerCode, sh.TotalWeight, sh.TotalValue, sh.NetTotal, sh.GST, sh.TotalPCS, ti.TInWeight, ti.TInValue
          ORDER BY sh.InvoiceDate ASC, sh.InvoiceNo ASC
        `;
        const resSJ = await request.query(query);
        rows = resSJ.recordset;
      } else {
        // Sales Journal — Detail Report matching legacy Crystal Report exactly:
        // Columns:
        // 1. Date
        // 2. Invoice #
        // 3. Inventory #
        // 4. Description
        // 5. Weight
        // 6. PCS
        // 7. TotalValue
        // 8. NetDisc.
        // 9. SMC
        // 10. GST
        // 11. Net Amount
        columns = [
          { key: "InvoiceDate", label: "Date", width: 100, format: "date" },
          { key: "InvoiceNo", label: "Invoice #", width: 110 },
          { key: "InventoryCode", label: "Inventory #", width: 130 },
          { key: "Description", label: "Description", width: 180 },
          { key: "Weight", label: "Weight", width: 90, align: "right", format: "decimal2" },
          { key: "PCS", label: "PCS", width: 60, align: "right" },
          { key: "TotalValue", label: "TotalValue", width: 100, align: "right", format: "currency" },
          { key: "NetDisc", label: "NetDisc.", width: 85, align: "right", format: "currency" },
          { key: "SMC", label: "SMC", width: 80, align: "right", format: "currency" },
          { key: "GST", label: "GST", width: 85, align: "right", format: "currency" },
          { key: "NetAmount", label: "Net Amount", width: 105, align: "right", format: "currency" }
        ];

        const query = `
          SELECT 
            sh.InvoiceDate,
            RTRIM(sh.InvoiceNo) AS InvoiceNo,
            ISNULL(RTRIM(c.CustomerName), ISNULL(RTRIM(sh.CustomerName), RTRIM(sh.CustomerCode))) AS CustomerName,
            RTRIM(sh.CustomerCode) AS CustomerCode,
            RTRIM(sh.LocationCode) AS LocationCode,
            RTRIM(sd.InventoryCode) AS InventoryCode,
            ISNULL(RTRIM(sd.Description), '') AS Description,
            ROUND(ISNULL(sd.Weight, 0), 2) AS Weight,
            ISNULL(sd.PCS, 1) AS PCS,
            ROUND(ISNULL(sd.TotalValue, 0), 2) AS TotalValue,
            ROUND(ISNULL(sd.NetDiscount, 0), 2) AS NetDisc,
            ROUND(ISNULL(sd.SMC, 0), 2) AS SMC,
            ROUND(ISNULL(sd.GST, 0), 2) AS GST,
            ROUND(ISNULL(sd.NetAmount, 0), 2) AS NetAmount
          FROM SmartGold.dbo.tblSalesHeader AS sh WITH (NOLOCK)
          INNER JOIN SmartGold.dbo.tblSalesDetail AS sd WITH (NOLOCK) 
            ON RTRIM(sh.InvoiceNo) = RTRIM(sd.InvoiceNo)
          LEFT OUTER JOIN SmartGold.dbo.tblCustomer AS c WITH (NOLOCK) 
            ON RTRIM(sh.CustomerCode) = RTRIM(c.CustomerCode)
          LEFT OUTER JOIN SmartGold.dbo.tblInventory AS inv WITH (NOLOCK) 
            ON RTRIM(sd.InventoryCode) = RTRIM(inv.InventoryCode)
          ${where}
          ORDER BY sh.InvoiceDate ASC, sh.InvoiceNo ASC, sd.SortCode ASC, sd.InventoryCode ASC
        `;
        const resSJDet = await request.query(query);
        rows = resSJDet.recordset;
      }
    }

    // ==========================================
    // 2. SALES BY WEIGHT AND PCS (Dynamic Intervals)
    // ==========================================
    else if (reportId === "sales_by_weight_pcs") {
      title = `Sales By Weight & PCS (Interval ${weightPcsChoice}g) From ${fromDateDisp} To ${toDateDisp}`;

      let rangeCases = "";
      if (weightPcsChoice === "2") {
        rangeCases = `
          CASE 
            WHEN B.Weight >= 0 AND B.Weight <= 2 THEN '0-2'
            WHEN B.Weight > 2 AND B.Weight <= 4 THEN '3-4'
            WHEN B.Weight > 4 AND B.Weight <= 6 THEN '5-6'
            WHEN B.Weight > 6 AND B.Weight <= 8 THEN '7-8'
            WHEN B.Weight > 8 AND B.Weight <= 10 THEN '9-10'
            WHEN B.Weight > 10 AND B.Weight <= 12 THEN '11-12'
            ELSE 'Above 12'
          END
        `;
      } else if (weightPcsChoice === "6") {
        rangeCases = `
          CASE 
            WHEN B.Weight >= 0 AND B.Weight <= 6 THEN '0-6'
            WHEN B.Weight > 6 AND B.Weight <= 12 THEN '7-12'
            WHEN B.Weight > 12 AND B.Weight <= 18 THEN '13-18'
            WHEN B.Weight > 18 AND B.Weight <= 24 THEN '19-24'
            WHEN B.Weight > 24 AND B.Weight <= 30 THEN '25-30'
            WHEN B.Weight > 30 AND B.Weight <= 36 THEN '31-36'
            ELSE 'Above 36'
          END
        `;
      } else if (weightPcsChoice === "8") {
        rangeCases = `
          CASE 
            WHEN B.Weight >= 0 AND B.Weight <= 8 THEN '0-8'
            WHEN B.Weight > 8 AND B.Weight <= 16 THEN '9-16'
            WHEN B.Weight > 16 AND B.Weight <= 24 THEN '17-24'
            WHEN B.Weight > 24 AND B.Weight <= 32 THEN '25-32'
            WHEN B.Weight > 32 AND B.Weight <= 40 THEN '33-40'
            WHEN B.Weight > 40 AND B.Weight <= 48 THEN '41-48'
            ELSE 'Above 48'
          END
        `;
      } else {
        // scale 4
        rangeCases = `
          CASE 
            WHEN B.Weight >= 0 AND B.Weight <= 4 THEN '0-4'
            WHEN B.Weight > 4 AND B.Weight <= 8 THEN '5-8'
            WHEN B.Weight > 8 AND B.Weight <= 12 THEN '9-12'
            WHEN B.Weight > 12 AND B.Weight <= 16 THEN '13-16'
            WHEN B.Weight > 16 AND B.Weight <= 20 THEN '17-20'
            WHEN B.Weight > 20 AND B.Weight <= 24 THEN '21-24'
            ELSE 'Above 24'
          END
        `;
      }

      let extraWhere = "";
      if (locationCode && locationCode !== "ALL" && locationCode.trim() !== "") {
        request.input("LocationCode", sql.VarChar, locationCode.trim());
        extraWhere += ` AND C.LocationCode = @LocationCode`;
      }
      if (categoryCode && categoryCode !== "ALL" && categoryCode.trim() !== "") {
        request.input("CategoryCode", sql.VarChar, categoryCode.trim());
        extraWhere += ` AND A.CategoryCode = @CategoryCode`;
      }
      if (catalogueCode && catalogueCode !== "ALL" && catalogueCode.trim() !== "") {
        request.input("CatalogueCode", sql.VarChar, catalogueCode.trim());
        extraWhere += ` AND A.CatalogueCode = @CatalogueCode`;
      }

      columns = [
        { key: "CatalogueCode", label: "Catalogue", width: 140 },
        { key: "CategoryCode", label: "Category", width: 140 },
        { key: "Range", label: "Weight Scale (g)", width: 130, align: "center" },
        { key: "TotalPCS", label: "Total PCS", width: 90, align: "right" },
        { key: "TotalWeight", label: "Total Weight (g)", width: 130, align: "right", format: "decimal3" }
      ];

      const query = `
        SELECT 
          RTRIM(A.CatalogueCode) AS CatalogueCode,
          RTRIM(A.CategoryCode) AS CategoryCode,
          ${rangeCases} AS [Range],
          SUM(ISNULL(B.PCS, 1)) AS TotalPCS,
          ROUND(SUM(ISNULL(B.Weight, 0)), 3) AS TotalWeight
        FROM tblInventory A WITH (NOLOCK)
        INNER JOIN tblSalesDetail B WITH (NOLOCK) ON A.InventoryCode = B.InventoryCode
        INNER JOIN tblSalesHeader C WITH (NOLOCK) ON B.InvoiceNo = C.InvoiceNo
        WHERE C.InvoiceDate >= @FromDate AND C.InvoiceDate < @ToDateExclusive
        ${extraWhere}
        GROUP BY A.CatalogueCode, A.CategoryCode, ${rangeCases}
        ORDER BY A.CatalogueCode, A.CategoryCode, [Range]
      `;

      const resWP = await request.query(query);
      rows = resWP.recordset;
    }

    // ==========================================
    // 3. TRADE IN ONLY / TRADE IN INTAKE REGISTER
    // ==========================================
    else if (reportId === "trade_in_only" || reportId === "trade_in_sales") {
      title = `Trade In Gold Register From ${fromDateDisp} To ${toDateDisp}`;

      let where = ` WHERE th.CreateDate >= @FromDate AND th.CreateDate < @ToDateExclusive`;
      if (locationCode && locationCode !== "ALL" && locationCode.trim() !== "") {
        request.input("LocationCode", sql.VarChar, locationCode.trim());
        where += ` AND th.LocationCode = @LocationCode`;
      }

      if (tradeInStatus === "Stock") {
        where += ` AND (th.SoldFlag <> 'Y' OR th.SoldFlag IS NULL)`;
      } else if (tradeInStatus === "Sold") {
        where += ` AND th.SoldFlag = 'Y'`;
      }

      if (purity && purity !== "ALL" && purity.trim() !== "") {
        request.input("Purity", sql.VarChar, purity.trim());
        where += ` AND td.Class = @Purity`;
      }

      columns = [
        { key: "InventoryCode", label: "Trade In Item", width: 140 },
        { key: "InvoiceNo", label: "Bill / Invoice Ref", width: 130 },
        { key: "CreateDate", label: "Date", width: 110, format: "date" },
        { key: "CustomerName", label: "Customer Name", width: 200 },
        { key: "TotalPCS", label: "PCS", width: 70, align: "right" },
        { key: "TotalWeight", label: "Weight (g)", width: 120, align: "right", format: "decimal3" },
        { key: "CostPerGram", label: "Rate / g", width: 100, align: "right", format: "currency" },
        { key: "TotalCost", label: "Total Value", width: 130, align: "right", format: "currency" },
        { key: "SoldFlag", label: "Status", width: 100, align: "center" }
      ];

      const query = `
        SELECT 
          RTRIM(th.InventoryCode) AS InventoryCode,
          RTRIM(th.InvoiceNo) AS InvoiceNo,
          th.CreateDate,
          ISNULL(RTRIM(th.CustomerName), RTRIM(th.CustomerCode)) AS CustomerName,
          ISNULL(th.TotalPCS, 1) AS TotalPCS,
          ROUND(ISNULL(th.TotalWeight, 0), 3) AS TotalWeight,
          ROUND(ISNULL(th.CostPerGram, 0), 2) AS CostPerGram,
          ROUND(ISNULL(th.TotalCost, 0), 2) AS TotalCost,
          CASE WHEN RTRIM(th.SoldFlag) = 'Y' THEN 'Sold' ELSE 'In Stock' END AS SoldFlag
        FROM tblTradeInHeader th WITH (NOLOCK)
        ${purity && purity !== "ALL" ? "LEFT JOIN tblTradeInDetail td WITH (NOLOCK) ON th.InvoiceNo = td.InvoiceNo" : ""}
        ${where}
        ORDER BY th.CreateDate DESC
      `;
      const resTO = await request.query(query);
      rows = resTO.recordset;
    }

    // ==========================================
    // 4. TRADE IN SOLD
    // ==========================================
    else if (reportId === "trade_in_sold") {
      title = `Trade In Sold Journal From ${fromDateDisp} To ${toDateDisp}`;

      let where = ` WHERE th.SalesDate >= @FromDate AND th.SalesDate < @ToDateExclusive`;
      if (locationCode && locationCode !== "ALL" && locationCode.trim() !== "") {
        request.input("LocationCode", sql.VarChar, locationCode.trim());
        where += ` AND th.LocationCode = @LocationCode`;
      }

      columns = [
        { key: "ReferenceNo", label: "Ref / Sales No", width: 140 },
        { key: "SalesDate", label: "Sales Date", width: 110, format: "date" },
        { key: "VendorName", label: "Customer / Vendor", width: 200 },
        { key: "TotalPCS", label: "PCS", width: 70, align: "right" },
        { key: "TotalWeight", label: "Weight (g)", width: 120, align: "right", format: "decimal3" },
        { key: "CostPerGram", label: "Rate / g", width: 100, align: "right", format: "currency" },
        { key: "TotalCost", label: "Cost Value", width: 110, align: "right", format: "currency" },
        { key: "Amount", label: "Amount Realized", width: 120, align: "right", format: "currency" }
      ];

      const query = `
        SELECT 
          RTRIM(th.ReferenceNo) AS ReferenceNo,
          th.SalesDate,
          ISNULL(RTRIM(th.VendorName), RTRIM(th.VendorCode)) AS VendorName,
          ISNULL(th.TotalPCS, 1) AS TotalPCS,
          ROUND(ISNULL(th.TotalWeight, 0), 3) AS TotalWeight,
          ROUND(ISNULL(th.CostPerGram, 0), 2) AS CostPerGram,
          ROUND(ISNULL(th.TotalCost, 0), 2) AS TotalCost,
          ROUND(ISNULL(th.Amount, 0), 2) AS Amount
        FROM tblTradeInSalesHeader th WITH (NOLOCK)
        ${where}
        ORDER BY th.SalesDate DESC
      `;
      const resTIS = await request.query(query);
      rows = resTIS.recordset;
    }

    // ==========================================
    // 5. INSTALLMENT ENTRY
    // ==========================================
    else if (reportId === "installment_entry") {
      title = `Installment Entry Journal From ${fromDateDisp} To ${toDateDisp}`;

      let where = ` WHERE ie.InvoiceDate >= @FromDate AND ie.InvoiceDate < @ToDateExclusive`;
      if (locationCode && locationCode !== "ALL" && locationCode.trim() !== "") {
        request.input("LocationCode", sql.VarChar, locationCode.trim());
        where += ` AND ie.LocationCode = @LocationCode`;
      }

      columns = [
        { key: "InvoiceNo", label: "Agreement / Inv No", width: 140 },
        { key: "InvoiceDate", label: "Date", width: 110, format: "date" },
        { key: "Name", label: "Customer Name", width: 200 },
        { key: "InsAgreementNo", label: "Agreement #", width: 120 },
        { key: "DepositAmt", label: "Deposit Paid", width: 120, align: "right", format: "currency" },
        { key: "TotalInstallment", label: "Tenure (Mos)", width: 100, align: "center" },
        { key: "InstallmentAmt", label: "Monthly Amt", width: 120, align: "right", format: "currency" },
        { key: "InsTotal", label: "Agreement Value", width: 130, align: "right", format: "currency" },
        { key: "AmtPaid", label: "Total Paid", width: 120, align: "right", format: "currency" }
      ];

      const query = `
        SELECT 
          RTRIM(ie.InvoiceNo) AS InvoiceNo,
          ie.InvoiceDate,
          ISNULL(RTRIM(ie.Name), RTRIM(ie.CustomerCode)) AS Name,
          ISNULL(RTRIM(ie.InsAgreementNo), '-') AS InsAgreementNo,
          ROUND(ISNULL(ie.DepositAmt, 0), 2) AS DepositAmt,
          ISNULL(ie.TotalInstallment, 0) AS TotalInstallment,
          ROUND(ISNULL(ie.InstallmentAmt, 0), 2) AS InstallmentAmt,
          ROUND(ISNULL(ie.InsTotal, 0), 2) AS InsTotal,
          ROUND(ISNULL(ie.AmtPaid, 0), 2) AS AmtPaid
        FROM tblInstallmentEntryHeader ie WITH (NOLOCK)
        ${where}
        ORDER BY ie.InvoiceDate DESC
      `;
      const resIE = await request.query(query);
      rows = resIE.recordset;
    }

    // ==========================================
    // 6. INSTALLMENT RECEIVED
    // ==========================================
    else if (reportId === "installment_received") {
      title = `Installment Received Receipts From ${fromDateDisp} To ${toDateDisp}`;

      let where = ` WHERE ip.CreateDate >= @FromDate AND ip.CreateDate < @ToDateExclusive`;

      columns = [
        { key: "InvoiceNo", label: "Invoice / Receipt No", width: 140 },
        { key: "CreateDate", label: "Payment Date", width: 120, format: "date" },
        { key: "PayMode", label: "Paymode", width: 120 },
        { key: "Amount", label: "Amount Received", width: 130, align: "right", format: "currency" }
      ];

      const query = `
        SELECT 
          RTRIM(ip.InvoiceNo) AS InvoiceNo,
          ip.CreateDate,
          RTRIM(ip.PayMode) AS PayMode,
          ROUND(ISNULL(ip.Amount, 0), 2) AS Amount
        FROM tblPayModeDetail ip WITH (NOLOCK)
        ${where} AND (RTRIM(ip.SalesType) = 'INSTALLMENT' OR RTRIM(ip.PayMode) = 'INSTALLMENT')
        ORDER BY ip.CreateDate DESC
      `;
      const resIR = await request.query(query);
      rows = resIR.recordset;
    }

    // ==========================================
    // 7. PENDING INSTALLMENT
    // ==========================================
    else if (reportId === "pending_installment") {
      title = `Pending Installments Ledger As Of ${toDateDisp}`;

      columns = [
        { key: "InvoiceNo", label: "Agreement No", width: 140 },
        { key: "Name", label: "Customer Name", width: 200 },
        { key: "InsAgreementNo", label: "Ref #", width: 110 },
        { key: "LastDate", label: "Maturity Date", width: 110, format: "date" },
        { key: "InsTotal", label: "Total Due", width: 130, align: "right", format: "currency" },
        { key: "AmtPaid", label: "Amount Paid", width: 120, align: "right", format: "currency" },
        { key: "BalanceAmt", label: "Balance Pending", width: 130, align: "right", format: "currency" }
      ];

      const query = `
        SELECT 
          RTRIM(ie.InvoiceNo) AS InvoiceNo,
          ISNULL(RTRIM(ie.Name), RTRIM(ie.CustomerCode)) AS Name,
          ISNULL(RTRIM(ie.InsAgreementNo), '-') AS InsAgreementNo,
          ie.LastDate,
          ROUND(ISNULL(ie.InsTotal, 0), 2) AS InsTotal,
          ROUND(ISNULL(ie.AmtPaid, 0), 2) AS AmtPaid,
          ROUND(ISNULL(ie.InsTotal, 0) - ISNULL(ie.AmtPaid, 0), 2) AS BalanceAmt
        FROM tblInstallmentEntryHeader ie WITH (NOLOCK)
        WHERE (ISNULL(ie.InsTotal, 0) - ISNULL(ie.AmtPaid, 0)) > 0
        ORDER BY ie.LastDate ASC
      `;
      const resPI = await request.query(query);
      rows = resPI.recordset;
    }

    // ==========================================
    // 8. SALES RETURN (Summary & Detail)
    // ==========================================
    else if (reportId === "sales_return") {
      title = `Sales Return Journal (${isSummary ? "Summary" : "Detail"}) From ${fromDateDisp} To ${toDateDisp}`;

      let where = ` WHERE sr.InvoiceDate >= @FromDate AND sr.InvoiceDate < @ToDateExclusive AND sr.ReturnType = 'SR'`;
      if (locationCode && locationCode !== "ALL" && locationCode.trim() !== "") {
        request.input("LocationCode", sql.VarChar, locationCode.trim());
        where += ` AND sr.LocationCode = @LocationCode`;
      }

      if (isSummary) {
        columns = [
          { key: "RefNo", label: "Return Ref No", width: 130 },
          { key: "InvoiceNo", label: "Orig Invoice No", width: 130 },
          { key: "InvoiceDate", label: "Date", width: 110, format: "date" },
          { key: "CustomerCode", label: "Customer", width: 110 },
          { key: "TotalPCS", label: "PCS", width: 70, align: "right" },
          { key: "TotalWeight", label: "Weight (g)", width: 110, align: "right", format: "decimal3" },
          { key: "TotalSMC", label: "MC Returned", width: 110, align: "right", format: "currency" },
          { key: "GST", label: "GST", width: 100, align: "right", format: "currency" },
          { key: "NetTotal", label: "Refund Amount", width: 130, align: "right", format: "currency" }
        ];

        const query = `
          SELECT 
            RTRIM(sr.RefNo) AS RefNo,
            RTRIM(sr.InvoiceNo) AS InvoiceNo,
            sr.InvoiceDate,
            RTRIM(sr.CustomerCode) AS CustomerCode,
            ISNULL(sr.TotalPCS, 1) AS TotalPCS,
            ROUND(ISNULL(sr.TotalWeight, 0), 3) AS TotalWeight,
            ROUND(ISNULL(sr.TotalSMC, 0), 2) AS TotalSMC,
            ROUND(ISNULL(sr.GST, 0), 2) AS GST,
            ROUND(ISNULL(sr.NetTotal, 0), 2) AS NetTotal
          FROM tblSalesReturnHeader sr WITH (NOLOCK)
          ${where}
          ORDER BY sr.InvoiceDate DESC
        `;
        const resSR = await request.query(query);
        rows = resSR.recordset;
      } else {
        columns = [
          { key: "RefNo", label: "Return Ref", width: 130 },
          { key: "InvoiceNo", label: "Orig Invoice", width: 130 },
          { key: "InventoryCode", label: "Inventory Code", width: 130 },
          { key: "Description", label: "Item Description", width: 200 },
          { key: "PCS", label: "PCS", width: 60, align: "right" },
          { key: "Weight", label: "Weight (g)", width: 100, align: "right", format: "decimal3" },
          { key: "Rate", label: "Rate", width: 90, align: "right", format: "currency" },
          { key: "SMC", label: "MC Returned", width: 90, align: "right", format: "currency" },
          { key: "NetAmount", label: "Refund Amt", width: 110, align: "right", format: "currency" }
        ];

        const query = `
          SELECT 
            RTRIM(sr.RefNo) AS RefNo,
            RTRIM(sr.InvoiceNo) AS InvoiceNo,
            RTRIM(srd.InventoryCode) AS InventoryCode,
            ISNULL(RTRIM(srd.Description), '') AS Description,
            ISNULL(srd.PCS, 1) AS PCS,
            ROUND(ISNULL(srd.Weight, 0), 3) AS Weight,
            ROUND(ISNULL(srd.Rate, 0), 2) AS Rate,
            ROUND(ISNULL(srd.SMC, 0), 2) AS SMC,
            ROUND(ISNULL(srd.NetAmount, 0), 2) AS NetAmount
          FROM tblSalesReturnHeader sr WITH (NOLOCK)
          INNER JOIN tblSalesReturnDetail srd WITH (NOLOCK) ON sr.RefNo = srd.RefNo
          ${where}
          ORDER BY sr.InvoiceDate DESC, sr.RefNo
        `;
        const resSRDet = await request.query(query);
        rows = resSRDet.recordset;
      }
    }

    // ==========================================
    // 9. BILL CANCEL (Summary & Detail)
    // ==========================================
    else if (reportId === "bill_cancel") {
      title = `Cancelled Bills Journal (${isSummary ? "Summary" : "Detail"}) From ${fromDateDisp} To ${toDateDisp}`;

      let where = ` WHERE sr.InvoiceDate >= @FromDate AND sr.InvoiceDate < @ToDateExclusive AND sr.ReturnType = 'BC'`;
      if (locationCode && locationCode !== "ALL" && locationCode.trim() !== "") {
        request.input("LocationCode", sql.VarChar, locationCode.trim());
        where += ` AND sr.LocationCode = @LocationCode`;
      }

      if (isSummary) {
        columns = [
          { key: "RefNo", label: "Cancel Ref No", width: 140 },
          { key: "InvoiceNo", label: "Cancelled Invoice", width: 140 },
          { key: "InvoiceDate", label: "Date", width: 110, format: "date" },
          { key: "CustomerCode", label: "Customer", width: 130 },
          { key: "TotalPCS", label: "PCS", width: 70, align: "right" },
          { key: "TotalWeight", label: "Weight (g)", width: 110, align: "right", format: "decimal3" },
          { key: "NetTotal", label: "Cancelled Total", width: 130, align: "right", format: "currency" },
          { key: "SalesManCode", label: "Salesman", width: 110 }
        ];

        const query = `
          SELECT 
            RTRIM(sr.RefNo) AS RefNo,
            RTRIM(sr.InvoiceNo) AS InvoiceNo,
            sr.InvoiceDate,
            RTRIM(sr.CustomerCode) AS CustomerCode,
            ISNULL(sr.TotalPCS, 1) AS TotalPCS,
            ROUND(ISNULL(sr.TotalWeight, 0), 3) AS TotalWeight,
            ROUND(ISNULL(sr.NetTotal, 0), 2) AS NetTotal,
            RTRIM(sr.SalesManCode) AS SalesManCode
          FROM tblSalesReturnHeader sr WITH (NOLOCK)
          ${where}
          ORDER BY sr.InvoiceDate DESC
        `;
        const resBC = await request.query(query);
        rows = resBC.recordset;
      } else {
        columns = [
          { key: "RefNo", label: "Cancel Ref", width: 140 },
          { key: "InvoiceNo", label: "Cancelled Invoice", width: 140 },
          { key: "InventoryCode", label: "Item Code", width: 130 },
          { key: "Description", label: "Description", width: 180 },
          { key: "PCS", label: "PCS", width: 60, align: "right" },
          { key: "Weight", label: "Weight (g)", width: 100, align: "right", format: "decimal3" },
          { key: "NetAmount", label: "Cancelled Value", width: 120, align: "right", format: "currency" }
        ];

        const query = `
          SELECT 
            RTRIM(sr.RefNo) AS RefNo,
            RTRIM(sr.InvoiceNo) AS InvoiceNo,
            RTRIM(srd.InventoryCode) AS InventoryCode,
            ISNULL(RTRIM(srd.Description), '') AS Description,
            ISNULL(srd.PCS, 1) AS PCS,
            ROUND(ISNULL(srd.Weight, 0), 3) AS Weight,
            ROUND(ISNULL(srd.NetAmount, 0), 2) AS NetAmount
          FROM tblSalesReturnHeader sr WITH (NOLOCK)
          INNER JOIN tblSalesReturnDetail srd WITH (NOLOCK) ON sr.RefNo = srd.RefNo
          ${where}
          ORDER BY sr.InvoiceDate DESC
        `;
        const resBCDet = await request.query(query);
        rows = resBCDet.recordset;
      }
    }

    // ==========================================
    // 10. DAILY SALES
    // ==========================================
    else if (reportId === "daily_sales") {
      title = `Daily Sales Consolidated Summary From ${fromDateDisp} To ${toDateDisp}`;

      let where = ` WHERE ds.InvoiceDate >= @FromDate AND ds.InvoiceDate < @ToDateExclusive`;

      columns = [
        { key: "InvoiceDate", label: "Sales Date", width: 110, format: "date" },
        { key: "Cash", label: "Cash", width: 100, align: "right", format: "currency" },
        { key: "Nets", label: "NETS", width: 100, align: "right", format: "currency" },
        { key: "Visa", label: "Visa", width: 100, align: "right", format: "currency" },
        { key: "Master", label: "Master", width: 100, align: "right", format: "currency" },
        { key: "TransfertoAcc", label: "Bank Transfer", width: 110, align: "right", format: "currency" },
        { key: "Installment", label: "Installment", width: 100, align: "right", format: "currency" },
        { key: "TotalGSTSales", label: "Taxable Sales", width: 120, align: "right", format: "currency" },
        { key: "GST", label: "GST", width: 90, align: "right", format: "currency" },
        { key: "TotalSales", label: "Total Sales", width: 130, align: "right", format: "currency" }
      ];

      const query = `
        SELECT 
          ds.InvoiceDate,
          ROUND(ISNULL(ds.Cash, 0), 2) AS Cash,
          ROUND(ISNULL(ds.Nets, 0), 2) AS Nets,
          ROUND(ISNULL(ds.Visa, 0), 2) AS Visa,
          ROUND(ISNULL(ds.Master, 0), 2) AS Master,
          ROUND(ISNULL(ds.TransfertoAcc, 0), 2) AS TransfertoAcc,
          ROUND(ISNULL(ds.Installment, 0), 2) AS Installment,
          ROUND(ISNULL(ds.TotalGSTSales, 0), 2) AS TotalGSTSales,
          ROUND(ISNULL(ds.GST, 0), 2) AS GST,
          ROUND(ISNULL(ds.TotalSales, 0), 2) AS TotalSales
        FROM tblDailySales ds WITH (NOLOCK)
        ${where}
        ORDER BY ds.InvoiceDate DESC
      `;
      const resDS = await request.query(query);
      rows = resDS.recordset;
    }

    // ==========================================
    // 11. SALES BY SUPPLIER (Summary & Detail)
    // ==========================================
    else if (reportId === "sales_by_supplier") {
      title = `Sales By Supplier (${isSummary ? "Summary" : "Detail"}) From ${fromDateDisp} To ${toDateDisp}`;

      let where = ` WHERE sh.InvoiceDate >= @FromDate AND sh.InvoiceDate < @ToDateExclusive AND LEFT(sd.InventoryCode, 2) <> 'TI'`;

      if (supplierCode && supplierCode !== "ALL" && supplierCode.trim() !== "") {
        request.input("SupplierCode", sql.VarChar, supplierCode.trim());
        where += ` AND inv.SupplierCode = @SupplierCode`;
      }
      if (locationCode && locationCode !== "ALL" && locationCode.trim() !== "") {
        request.input("LocationCode", sql.VarChar, locationCode.trim());
        where += ` AND sh.LocationCode = @LocationCode`;
      }

      if (isSummary) {
        columns = [
          { key: "SupplierCode", label: "Supplier Code", width: 120 },
          { key: "SupplierName", label: "Supplier Name", width: 220 },
          { key: "TotalPCS", label: "PCS Sold", width: 90, align: "right" },
          { key: "TotalWeight", label: "Total Wt (g)", width: 130, align: "right", format: "decimal3" },
          { key: "TotalCost", label: "Cost Value", width: 130, align: "right", format: "currency" },
          { key: "TotalSales", label: "Sales Amount", width: 140, align: "right", format: "currency" }
        ];

        const query = `
          SELECT 
            ISNULL(RTRIM(inv.SupplierCode), 'DIRECT') AS SupplierCode,
            ISNULL(RTRIM(s.SupplierName), 'Direct/Unknown') AS SupplierName,
            SUM(ISNULL(sd.PCS, 1)) AS TotalPCS,
            ROUND(SUM(ISNULL(sd.Weight, 0)), 3) AS TotalWeight,
            ROUND(SUM(ISNULL(sd.TotalCost, 0)), 2) AS TotalCost,
            ROUND(SUM(ISNULL(sd.ActualSales, 0)), 2) AS TotalSales
          FROM tblSalesHeader sh WITH (NOLOCK)
          INNER JOIN tblSalesDetail sd WITH (NOLOCK) ON sh.InvoiceNo = sd.InvoiceNo
          LEFT JOIN tblInventory inv WITH (NOLOCK) ON sd.InventoryCode = inv.InventoryCode
          LEFT JOIN tblSupplier s WITH (NOLOCK) ON inv.SupplierCode = s.SupplierCode
          ${where}
          GROUP BY inv.SupplierCode, s.SupplierName
          ORDER BY TotalSales DESC
        `;
        const resSS = await request.query(query);
        rows = resSS.recordset;
      } else {
        columns = [
          { key: "SupplierCode", label: "Supplier", width: 110 },
          { key: "InvoiceNo", label: "Invoice No", width: 130 },
          { key: "InvoiceDate", label: "Date", width: 100, format: "date" },
          { key: "InventoryCode", label: "Item Code", width: 130 },
          { key: "Description", label: "Description", width: 180 },
          { key: "PCS", label: "PCS", width: 60, align: "right" },
          { key: "Weight", label: "Weight (g)", width: 100, align: "right", format: "decimal3" },
          { key: "Rate", label: "Rate", width: 90, align: "right", format: "currency" },
          { key: "NetAmount", label: "Sales Amount", width: 120, align: "right", format: "currency" }
        ];

        const query = `
          SELECT 
            ISNULL(RTRIM(inv.SupplierCode), 'DIRECT') AS SupplierCode,
            RTRIM(sh.InvoiceNo) AS InvoiceNo,
            sh.InvoiceDate,
            RTRIM(sd.InventoryCode) AS InventoryCode,
            ISNULL(RTRIM(sd.Description), '') AS Description,
            ISNULL(sd.PCS, 1) AS PCS,
            ROUND(ISNULL(sd.Weight, 0), 3) AS Weight,
            ROUND(ISNULL(sd.Rate, 0), 2) AS Rate,
            ROUND(ISNULL(sd.NetAmount, 0), 2) AS NetAmount
          FROM tblSalesHeader sh WITH (NOLOCK)
          INNER JOIN tblSalesDetail sd WITH (NOLOCK) ON sh.InvoiceNo = sd.InvoiceNo
          LEFT JOIN tblInventory inv WITH (NOLOCK) ON sd.InventoryCode = inv.InventoryCode
          ${where}
          ORDER BY inv.SupplierCode, sh.InvoiceDate DESC
        `;
        const resSSDet = await request.query(query);
        rows = resSSDet.recordset;
      }
    }

    // ==========================================
    // 12. SALES BY CUSTOMER (Summary & Detail)
    // ==========================================
    else if (reportId === "sales_by_customer") {
      title = `Sales By Customer (${isSummary ? "Summary" : "Detail"}) From ${fromDateDisp} To ${toDateDisp}`;

      let where = ` WHERE sh.InvoiceDate >= @FromDate AND sh.InvoiceDate < @ToDateExclusive`;
      if (customerCode && customerCode !== "ALL" && customerCode.trim() !== "") {
        request.input("CustomerCode", sql.VarChar, customerCode.trim());
        where += ` AND RTRIM(sh.CustomerCode) = @CustomerCode`;
      }

      if (isSummary) {
        columns = [
          { key: "CustomerCode", label: "Customer Code", width: 130 },
          { key: "CustomerName", label: "Customer Name", width: 220 },
          { key: "InvoicesCount", label: "Bills Count", width: 90, align: "right" },
          { key: "TotalPCS", label: "Total PCS", width: 80, align: "right" },
          { key: "TotalWeight", label: "Weight (g)", width: 120, align: "right", format: "decimal3" },
          { key: "NetTotal", label: "Total Purchases", width: 140, align: "right", format: "currency" }
        ];

        const query = `
          SELECT 
            RTRIM(sh.CustomerCode) AS CustomerCode,
            ISNULL(RTRIM(sh.CustomerName), RTRIM(sh.CustomerCode)) AS CustomerName,
            COUNT(sh.InvoiceNo) AS InvoicesCount,
            SUM(ISNULL(sh.TotalPCS, 1)) AS TotalPCS,
            ROUND(SUM(ISNULL(sh.TotalWeight, 0)), 3) AS TotalWeight,
            ROUND(SUM(ISNULL(sh.NetTotal, 0)), 2) AS NetTotal
          FROM tblSalesHeader sh WITH (NOLOCK)
          ${where}
          GROUP BY sh.CustomerCode, sh.CustomerName
          ORDER BY NetTotal DESC
        `;
        const resSC = await request.query(query);
        rows = resSC.recordset;
      } else {
        columns = [
          { key: "CustomerCode", label: "Customer", width: 110 },
          { key: "InvoiceNo", label: "Invoice No", width: 130 },
          { key: "InvoiceDate", label: "Date", width: 100, format: "date" },
          { key: "InventoryCode", label: "Item Code", width: 130 },
          { key: "Description", label: "Description", width: 180 },
          { key: "PCS", label: "PCS", width: 60, align: "right" },
          { key: "Weight", label: "Weight (g)", width: 100, align: "right", format: "decimal3" },
          { key: "NetAmount", label: "Amount", width: 120, align: "right", format: "currency" }
        ];

        const query = `
          SELECT 
            RTRIM(sh.CustomerCode) AS CustomerCode,
            RTRIM(sh.InvoiceNo) AS InvoiceNo,
            sh.InvoiceDate,
            RTRIM(sd.InventoryCode) AS InventoryCode,
            ISNULL(RTRIM(sd.Description), '') AS Description,
            ISNULL(sd.PCS, 1) AS PCS,
            ROUND(ISNULL(sd.Weight, 0), 3) AS Weight,
            ROUND(ISNULL(sd.NetAmount, 0), 2) AS NetAmount
          FROM tblSalesHeader sh WITH (NOLOCK)
          INNER JOIN tblSalesDetail sd WITH (NOLOCK) ON sh.InvoiceNo = sd.InvoiceNo
          ${where} AND LEFT(sd.InventoryCode, 4) <> 'TI-0'
          ORDER BY sh.CustomerCode, sh.InvoiceDate DESC
        `;
        const resSCDet = await request.query(query);
        rows = resSCDet.recordset;
      }
    }

    // ==========================================
    // 13. PENDING ORDER
    // ==========================================
    else if (reportId === "pending_order") {
      title = `Pending Customer Orders From ${fromDateDisp} To ${toDateDisp}`;

      columns = [
        { key: "SalesOrderNo", label: "Order No", width: 130 },
        { key: "SalesOrderDate", label: "Order Date", width: 110, format: "date" },
        { key: "DueDate", label: "Due Date", width: 110, format: "date" },
        { key: "SupplierCode", label: "Supplier", width: 110 },
        { key: "CategoryCode", label: "Category", width: 110 },
        { key: "CatalogueCode", label: "Catalogue", width: 110 },
        { key: "SalesOrderQty", label: "Ordered Qty", width: 100, align: "right" },
        { key: "TotalWeight", label: "Weight (g)", width: 110, align: "right", format: "decimal3" }
      ];

      const query = `
        SELECT 
          RTRIM(so.SalesOrderNo) AS SalesOrderNo,
          so.SalesOrderDate,
          so.DueDate,
          RTRIM(so.SupplierCode) AS SupplierCode,
          RTRIM(so.CategoryCode) AS CategoryCode,
          RTRIM(so.CatalogueCode) AS CatalogueCode,
          ISNULL(so.SalesOrderQty, 0) AS SalesOrderQty,
          ROUND(ISNULL(so.TotalWeight, 0), 3) AS TotalWeight
        FROM tblSalesOrderHeader so WITH (NOLOCK)
        WHERE so.SalesOrderDate >= @FromDate AND so.SalesOrderDate < @ToDateExclusive
        ORDER BY so.DueDate ASC
      `;
      const resPO = await request.query(query);
      rows = resPO.recordset;
    }

    // ==========================================
    // 14. SALES JOURNAL BY CATEGORY
    // ==========================================
    else if (reportId === "sales_journal_by_category") {
      title = `Sales Journal By Category From ${fromDateDisp} To ${toDateDisp}`;

      let where = ` WHERE sh.InvoiceDate >= @FromDate AND sh.InvoiceDate < @ToDateExclusive`;
      if (categoryCode && categoryCode !== "ALL" && categoryCode.trim() !== "") {
        request.input("CategoryCode", sql.VarChar, categoryCode.trim());
        where += ` AND inv.CategoryCode = @CategoryCode`;
      }

      columns = [
        { key: "CategoryCode", label: "Category Code", width: 130 },
        { key: "CategoryName", label: "Category Name", width: 220 },
        { key: "TotalItems", label: "Items Sold", width: 90, align: "right" },
        { key: "TotalWeight", label: "Weight (g)", width: 130, align: "right", format: "decimal3" },
        { key: "TotalMC", label: "Total MC", width: 120, align: "right", format: "currency" },
        { key: "TotalSales", label: "Total Sales", width: 140, align: "right", format: "currency" }
      ];

      const query = `
        SELECT 
          ISNULL(RTRIM(inv.CategoryCode), 'UNCAT') AS CategoryCode,
          ISNULL(RTRIM(c.CategoryName), 'Uncategorized') AS CategoryName,
          SUM(ISNULL(sd.PCS, 1)) AS TotalItems,
          ROUND(SUM(ISNULL(sd.Weight, 0)), 3) AS TotalWeight,
          ROUND(SUM(ISNULL(sd.SMC, 0)), 2) AS TotalMC,
          ROUND(SUM(ISNULL(sd.ActualSales, 0)), 2) AS TotalSales
        FROM tblSalesHeader sh WITH (NOLOCK)
        INNER JOIN tblSalesDetail sd WITH (NOLOCK) ON sh.InvoiceNo = sd.InvoiceNo
        LEFT JOIN tblInventory inv WITH (NOLOCK) ON sd.InventoryCode = inv.InventoryCode
        LEFT JOIN tblCategory c WITH (NOLOCK) ON inv.CategoryCode = c.CategoryCode
        ${where}
        GROUP BY inv.CategoryCode, c.CategoryName
        ORDER BY TotalSales DESC
      `;
      const resCat = await request.query(query);
      rows = resCat.recordset;
    }

    // ==========================================
    // 15. SALES JOURNAL BY CATALOGUE
    // ==========================================
    else if (reportId === "sales_journal_by_catalogue") {
      title = `Sales Journal By Catalogue From ${fromDateDisp} To ${toDateDisp}`;

      let where = ` WHERE sh.InvoiceDate >= @FromDate AND sh.InvoiceDate < @ToDateExclusive`;
      if (catalogueCode && catalogueCode !== "ALL" && catalogueCode.trim() !== "") {
        request.input("CatalogueCode", sql.VarChar, catalogueCode.trim());
        where += ` AND inv.CatalogueCode = @CatalogueCode`;
      }

      columns = [
        { key: "CatalogueCode", label: "Catalogue Code", width: 130 },
        { key: "CatalogueName", label: "Catalogue Name", width: 220 },
        { key: "TotalItems", label: "Items Sold", width: 90, align: "right" },
        { key: "TotalWeight", label: "Weight (g)", width: 130, align: "right", format: "decimal3" },
        { key: "TotalMC", label: "Total MC", width: 120, align: "right", format: "currency" },
        { key: "TotalSales", label: "Total Sales", width: 140, align: "right", format: "currency" }
      ];

      const query = `
        SELECT 
          ISNULL(RTRIM(inv.CatalogueCode), 'UNCAT') AS CatalogueCode,
          ISNULL(RTRIM(cat.CatalogueName), 'Uncatalogued') AS CatalogueName,
          SUM(ISNULL(sd.PCS, 1)) AS TotalItems,
          ROUND(SUM(ISNULL(sd.Weight, 0)), 3) AS TotalWeight,
          ROUND(SUM(ISNULL(sd.SMC, 0)), 2) AS TotalMC,
          ROUND(SUM(ISNULL(sd.ActualSales, 0)), 2) AS TotalSales
        FROM tblSalesHeader sh WITH (NOLOCK)
        INNER JOIN tblSalesDetail sd WITH (NOLOCK) ON sh.InvoiceNo = sd.InvoiceNo
        LEFT JOIN tblInventory inv WITH (NOLOCK) ON sd.InventoryCode = inv.InventoryCode
        LEFT JOIN tblCatalogue cat WITH (NOLOCK) ON inv.CatalogueCode = cat.CatalogueCode
        ${where}
        GROUP BY inv.CatalogueCode, cat.CatalogueName
        ORDER BY TotalSales DESC
      `;
      const resCtl = await request.query(query);
      rows = resCtl.recordset;
    }

    // ==========================================
    // 16. PAYMODE WISE SALES (Summary & Detail)
    // ==========================================
    else if (reportId === "paymode_wise_sales") {
      title = `Paymode Wise Sales (${isSummary ? "Summary" : "Detail"}) From ${fromDateDisp} To ${toDateDisp}`;

      let where = ` WHERE pm.InvoiceDate >= @FromDate AND pm.InvoiceDate < @ToDateExclusive`;
      if (locationCode && locationCode !== "ALL" && locationCode.trim() !== "") {
        request.input("LocationCode", sql.VarChar, locationCode.trim());
        where += ` AND pm.LocationCode = @LocationCode`;
      }
      if (paymode && paymode !== "ALL" && paymode.trim() !== "") {
        request.input("PayMode", sql.VarChar, paymode.trim());
        where += ` AND RTRIM(pm.PayMode) = @PayMode`;
      }

      if (isSummary) {
        columns = [
          { key: "PayMode", label: "Payment Mode", width: 140 },
          { key: "TotalTransactions", label: "Trans Count", width: 110, align: "right" },
          { key: "TotalAmount", label: "Total Amount Received", width: 160, align: "right", format: "currency" },
          { key: "TotalCommission", label: "Bank Commission", width: 130, align: "right", format: "currency" }
        ];

        const query = `
          SELECT 
            RTRIM(pm.PayMode) AS PayMode,
            COUNT(pm.InvoiceNo) AS TotalTransactions,
            ROUND(SUM(ISNULL(pm.Amount, 0)), 2) AS TotalAmount,
            ROUND(SUM(ISNULL(pm.Commision, 0)), 2) AS TotalCommission
          FROM tblPayModeDetail pm WITH (NOLOCK)
          ${where}
          GROUP BY pm.PayMode
          ORDER BY TotalAmount DESC
        `;
        const resPM = await request.query(query);
        rows = resPM.recordset;
      } else {
        columns = [
          { key: "InvoiceNo", label: "Invoice No", width: 140 },
          { key: "InvoiceDate", label: "Date", width: 110, format: "date" },
          { key: "PayMode", label: "Pay Mode", width: 120 },
          { key: "Amount", label: "Amount", width: 120, align: "right", format: "currency" },
          { key: "Commision", label: "Commission", width: 100, align: "right", format: "currency" },
          { key: "SlipNo", label: "Slip / Ref No", width: 140 }
        ];

        const query = `
          SELECT 
            RTRIM(pm.InvoiceNo) AS InvoiceNo,
            pm.InvoiceDate,
            RTRIM(pm.PayMode) AS PayMode,
            ROUND(ISNULL(pm.Amount, 0), 2) AS Amount,
            ROUND(ISNULL(pm.Commision, 0), 2) AS Commision,
            ISNULL(RTRIM(pm.SlipNo), '-') AS SlipNo
          FROM tblPayModeDetail pm WITH (NOLOCK)
          ${where}
          ORDER BY pm.InvoiceDate DESC, pm.InvoiceNo
        `;
        const resPMDet = await request.query(query);
        rows = resPMDet.recordset;
      }
    }

    // ==========================================
    // 17. SALESMAN WISE JOURNAL (Summary & Detail)
    // ==========================================
    else if (reportId === "salesman_wise_journal") {
      title = `Salesman Wise Sales (${isSummary ? "Summary" : "Detail"}) From ${fromDateDisp} To ${toDateDisp}`;

      let where = ` WHERE sh.InvoiceDate >= @FromDate AND sh.InvoiceDate < @ToDateExclusive`;
      if (salesmanCode && salesmanCode !== "ALL" && salesmanCode.trim() !== "") {
        request.input("SalesManCode", sql.VarChar, salesmanCode.trim());
        where += ` AND RTRIM(sh.SalesManCode) = @SalesManCode`;
      }
      if (locationCode && locationCode !== "ALL" && locationCode.trim() !== "") {
        request.input("LocationCode", sql.VarChar, locationCode.trim());
        where += ` AND sh.LocationCode = @LocationCode`;
      }

      if (isSummary) {
        columns = [
          { key: "SalesManCode", label: "Salesman Code", width: 120 },
          { key: "SalesManName", label: "Salesman Name", width: 200 },
          { key: "InvoicesCount", label: "Bills Count", width: 100, align: "right" },
          { key: "TotalWeight", label: "Weight (g)", width: 130, align: "right", format: "decimal3" },
          { key: "TotalSMC", label: "Total MC", width: 120, align: "right", format: "currency" },
          { key: "NetTotal", label: "Total Sales", width: 140, align: "right", format: "currency" }
        ];

        const query = `
          SELECT 
            ISNULL(RTRIM(sh.SalesManCode), 'DIRECT') AS SalesManCode,
            ISNULL(RTRIM(sm.SalesManName), RTRIM(sh.SalesManCode)) AS SalesManName,
            COUNT(sh.InvoiceNo) AS InvoicesCount,
            ROUND(SUM(ISNULL(sh.TotalWeight, 0)), 3) AS TotalWeight,
            ROUND(SUM(ISNULL(sh.TotalSMC, 0)), 2) AS TotalSMC,
            ROUND(SUM(ISNULL(sh.NetTotal, 0)), 2) AS NetTotal
          FROM tblSalesHeader sh WITH (NOLOCK)
          LEFT JOIN tblSalesMan sm WITH (NOLOCK) ON sh.SalesManCode = sm.SalesManCode
          ${where}
          GROUP BY sh.SalesManCode, sm.SalesManName
          ORDER BY NetTotal DESC
        `;
        const resSM = await request.query(query);
        rows = resSM.recordset;
      } else {
        columns = [
          { key: "SalesManCode", label: "Salesman", width: 110 },
          { key: "InvoiceNo", label: "Invoice No", width: 130 },
          { key: "InvoiceDate", label: "Date", width: 100, format: "date" },
          { key: "InventoryCode", label: "Item Code", width: 130 },
          { key: "Description", label: "Description", width: 180 },
          { key: "PCS", label: "PCS", width: 60, align: "right" },
          { key: "Weight", label: "Weight (g)", width: 100, align: "right", format: "decimal3" },
          { key: "SMC", label: "MC", width: 90, align: "right", format: "currency" },
          { key: "NetAmount", label: "Net Amount", width: 110, align: "right", format: "currency" }
        ];

        const query = `
          SELECT 
            ISNULL(RTRIM(sh.SalesManCode), 'DIRECT') AS SalesManCode,
            RTRIM(sh.InvoiceNo) AS InvoiceNo,
            sh.InvoiceDate,
            RTRIM(sd.InventoryCode) AS InventoryCode,
            ISNULL(RTRIM(sd.Description), '') AS Description,
            ISNULL(sd.PCS, 1) AS PCS,
            ROUND(ISNULL(sd.Weight, 0), 3) AS Weight,
            ROUND(ISNULL(sd.SMC, 0), 2) AS SMC,
            ROUND(ISNULL(sd.NetAmount, 0), 2) AS NetAmount
          FROM tblSalesHeader sh WITH (NOLOCK)
          INNER JOIN tblSalesDetail sd WITH (NOLOCK) ON sh.InvoiceNo = sd.InvoiceNo
          ${where} AND LEFT(sd.InventoryCode, 4) <> 'TI-0'
          ORDER BY sh.SalesManCode, sh.InvoiceDate DESC
        `;
        const resSMDet = await request.query(query);
        rows = resSMDet.recordset;
      }
    }

    // ==========================================
    // 18. SALESMAN ANALYSIS BY SMC
    // ==========================================
    else if (reportId === "salesman_analysis_smc") {
      title = `Salesman Analysis Report by SMC From ${fromDateDisp} To ${toDateDisp}`;

      let where = ` WHERE sh.InvoiceDate >= @FromDate AND sh.InvoiceDate < @ToDateExclusive`;
      if (locationCode && locationCode !== "ALL" && locationCode.trim() !== "") {
        request.input("LocationCode", sql.VarChar, locationCode.trim());
        where += ` AND sh.LocationCode = @LocationCode`;
      }

      columns = [
        { key: "SalesManCode", label: "Salesman Code", width: 120 },
        { key: "SalesManName", label: "Salesman Name", width: 200 },
        { key: "TotalWeight", label: "Weight (g)", width: 120, align: "right", format: "decimal3" },
        { key: "TotalSMC", label: "Total SMC", width: 120, align: "right", format: "currency" },
        { key: "NetTotal", label: "Net Turnover", width: 130, align: "right", format: "currency" },
        { key: "SMCPercentage", label: "SMC Margin %", width: 110, align: "right" }
      ];

      const query = `
        SELECT 
          ISNULL(RTRIM(sh.SalesManCode), 'DIRECT') AS SalesManCode,
          ISNULL(RTRIM(sm.SalesManName), RTRIM(sh.SalesManCode)) AS SalesManName,
          ROUND(SUM(ISNULL(sh.TotalWeight, 0)), 3) AS TotalWeight,
          ROUND(SUM(ISNULL(sh.TotalSMC, 0)), 2) AS TotalSMC,
          ROUND(SUM(ISNULL(sh.NetTotal, 0)), 2) AS NetTotal,
          CASE 
            WHEN SUM(ISNULL(sh.NetTotal, 0)) > 0 
            THEN CAST(ROUND((SUM(ISNULL(sh.TotalSMC, 0)) / SUM(ISNULL(sh.NetTotal, 0))) * 100, 2) AS VARCHAR(10)) + '%'
            ELSE '0.00%'
          END AS SMCPercentage
        FROM tblSalesHeader sh WITH (NOLOCK)
        LEFT JOIN tblSalesMan sm WITH (NOLOCK) ON sh.SalesManCode = sm.SalesManCode
        ${where}
        GROUP BY sh.SalesManCode, sm.SalesManName
        ORDER BY TotalSMC DESC
      `;
      const resSMC = await request.query(query);
      rows = resSMC.recordset;
    }

    // ==========================================
    // 19. SALES JOURNAL IN MARKET RATE (Summary & Detail)
    // ==========================================
    else if (reportId === "sales_journal_market_rate") {
      title = `Sales Journal By Market Rate (${isSummary ? "Summary" : "Detail"}) From ${fromDateDisp} To ${toDateDisp}`;

      let where = ` WHERE sh.InvoiceDate >= @FromDate AND sh.InvoiceDate < @ToDateExclusive`;
      if (locationCode && locationCode !== "ALL" && locationCode.trim() !== "") {
        request.input("LocationCode", sql.VarChar, locationCode.trim());
        where += ` AND sh.LocationCode = @LocationCode`;
      }

      if (isSummary) {
        columns = [
          { key: "InvoiceNo", label: "Invoice No", width: 140 },
          { key: "InvoiceDate", label: "Date", width: 110, format: "date" },
          { key: "CustomerName", label: "Customer Name", width: 200 },
          { key: "TotalWeight", label: "Weight (g)", width: 110, align: "right", format: "decimal3" },
          { key: "MarketValue", label: "Market Value", width: 130, align: "right", format: "currency" },
          { key: "NetTotal", label: "Billed Value", width: 130, align: "right", format: "currency" },
          { key: "Variance", label: "Variance", width: 110, align: "right", format: "currency" }
        ];

        const query = `
          SELECT 
            RTRIM(sh.InvoiceNo) AS InvoiceNo,
            sh.InvoiceDate,
            ISNULL(RTRIM(sh.CustomerName), RTRIM(sh.CustomerCode)) AS CustomerName,
            ROUND(ISNULL(sh.TotalWeight, 0), 3) AS TotalWeight,
            ROUND(SUM(ISNULL(sd.Weight, 0) * ISNULL(sd.DailyRate, sd.Rate)), 2) AS MarketValue,
            ROUND(ISNULL(sh.NetTotal, 0), 2) AS NetTotal,
            ROUND(ISNULL(sh.NetTotal, 0) - SUM(ISNULL(sd.Weight, 0) * ISNULL(sd.DailyRate, sd.Rate)), 2) AS Variance
          FROM tblSalesHeader sh WITH (NOLOCK)
          INNER JOIN tblSalesDetail sd WITH (NOLOCK) ON sh.InvoiceNo = sd.InvoiceNo
          ${where}
          GROUP BY sh.InvoiceNo, sh.InvoiceDate, sh.CustomerName, sh.CustomerCode, sh.TotalWeight, sh.NetTotal
          ORDER BY sh.InvoiceDate DESC
        `;
        const resMR = await request.query(query);
        rows = resMR.recordset;
      } else {
        columns = [
          { key: "InvoiceNo", label: "Invoice No", width: 130 },
          { key: "InvoiceDate", label: "Date", width: 100, format: "date" },
          { key: "InventoryCode", label: "Item Code", width: 130 },
          { key: "Weight", label: "Weight (g)", width: 100, align: "right", format: "decimal3" },
          { key: "Rate", label: "Sold Rate", width: 90, align: "right", format: "currency" },
          { key: "DailyRate", label: "Daily Rate", width: 90, align: "right", format: "currency" },
          { key: "MarketValue", label: "Market Value", width: 120, align: "right", format: "currency" },
          { key: "NetAmount", label: "Net Amount", width: 120, align: "right", format: "currency" }
        ];

        const query = `
          SELECT 
            RTRIM(sh.InvoiceNo) AS InvoiceNo,
            sh.InvoiceDate,
            RTRIM(sd.InventoryCode) AS InventoryCode,
            ROUND(ISNULL(sd.Weight, 0), 3) AS Weight,
            ROUND(ISNULL(sd.Rate, 0), 2) AS Rate,
            ROUND(ISNULL(sd.DailyRate, sd.Rate), 2) AS DailyRate,
            ROUND(ISNULL(sd.Weight, 0) * ISNULL(sd.DailyRate, sd.Rate), 2) AS MarketValue,
            ROUND(ISNULL(sd.NetAmount, 0), 2) AS NetAmount
          FROM tblSalesHeader sh WITH (NOLOCK)
          INNER JOIN tblSalesDetail sd WITH (NOLOCK) ON sh.InvoiceNo = sd.InvoiceNo
          ${where}
          ORDER BY sh.InvoiceDate DESC
        `;
        const resMRDet = await request.query(query);
        rows = resMRDet.recordset;
      }
    }

    // ==========================================
    // 20. BELOW COST SALES
    // ==========================================
    else if (reportId === "below_cost_sales") {
      title = `Below Cost Sales Audit From ${fromDateDisp} To ${toDateDisp}`;

      let where = ` WHERE sh.InvoiceDate >= @FromDate AND sh.InvoiceDate < @ToDateExclusive AND sd.TotalValue < sd.TotalCost AND LEFT(sd.InventoryCode, 2) <> 'TI'`;
      if (locationCode && locationCode !== "ALL" && locationCode.trim() !== "") {
        request.input("LocationCode", sql.VarChar, locationCode.trim());
        where += ` AND sh.LocationCode = @LocationCode`;
      }

      columns = [
        { key: "InvoiceNo", label: "Invoice No", width: 130 },
        { key: "InvoiceDate", label: "Date", width: 100, format: "date" },
        { key: "InventoryCode", label: "Item Code", width: 130 },
        { key: "Description", label: "Description", width: 180 },
        { key: "Weight", label: "Weight (g)", width: 100, align: "right", format: "decimal3" },
        { key: "TotalCost", label: "Cost Value", width: 120, align: "right", format: "currency" },
        { key: "TotalValue", label: "Sales Value", width: 120, align: "right", format: "currency" },
        { key: "LossAmount", label: "Margin Loss", width: 120, align: "right", format: "currency" }
      ];

      const query = `
        SELECT 
          RTRIM(sh.InvoiceNo) AS InvoiceNo,
          sh.InvoiceDate,
          RTRIM(sd.InventoryCode) AS InventoryCode,
          ISNULL(RTRIM(sd.Description), '') AS Description,
          ROUND(ISNULL(sd.Weight, 0), 3) AS Weight,
          ROUND(ISNULL(sd.TotalCost, 0), 2) AS TotalCost,
          ROUND(ISNULL(sd.TotalValue, 0), 2) AS TotalValue,
          ROUND(ISNULL(sd.TotalCost, 0) - ISNULL(sd.TotalValue, 0), 2) AS LossAmount
        FROM tblSalesHeader sh WITH (NOLOCK)
        INNER JOIN tblSalesDetail sd WITH (NOLOCK) ON sh.InvoiceNo = sd.InvoiceNo
        ${where}
        ORDER BY LossAmount DESC
      `;
      const resBCS = await request.query(query);
      rows = resBCS.recordset;
    }

    // ==========================================
    // 21. TRAY WISE SALES (Summary & Detail)
    // ==========================================
    else if (reportId === "tray_wise_sales") {
      title = `Tray Wise Sales (${isSummary ? "Summary" : "Detail"}) From ${fromDateDisp} To ${toDateDisp}`;

      let where = ` WHERE sh.InvoiceDate >= @FromDate AND sh.InvoiceDate < @ToDateExclusive`;
      if (trayId && trayId !== "ALL" && trayId.trim() !== "") {
        request.input("TrayId", sql.VarChar, trayId.trim());
        where += ` AND inv.TrayId = @TrayId`;
      }

      if (isSummary) {
        columns = [
          { key: "TrayId", label: "Tray ID", width: 120 },
          { key: "TrayName", label: "Tray Name", width: 200 },
          { key: "TotalItems", label: "Items Sold", width: 100, align: "right" },
          { key: "TotalWeight", label: "Weight (g)", width: 130, align: "right", format: "decimal3" },
          { key: "TotalSales", label: "Total Sales", width: 140, align: "right", format: "currency" }
        ];

        const query = `
          SELECT 
            ISNULL(RTRIM(inv.TrayId), 'NO_TRAY') AS TrayId,
            ISNULL(RTRIM(tm.TrayName), 'Default Tray') AS TrayName,
            SUM(ISNULL(sd.PCS, 1)) AS TotalItems,
            ROUND(SUM(ISNULL(sd.Weight, 0)), 3) AS TotalWeight,
            ROUND(SUM(ISNULL(sd.ActualSales, 0)), 2) AS TotalSales
          FROM tblSalesHeader sh WITH (NOLOCK)
          INNER JOIN tblSalesDetail sd WITH (NOLOCK) ON sh.InvoiceNo = sd.InvoiceNo
          LEFT JOIN tblInventory inv WITH (NOLOCK) ON sd.InventoryCode = inv.InventoryCode
          LEFT JOIN tblTrayMaster tm WITH (NOLOCK) ON inv.TrayId = tm.TrayId
          ${where}
          GROUP BY inv.TrayId, tm.TrayName
          ORDER BY TotalSales DESC
        `;
        const resTW = await request.query(query);
        rows = resTW.recordset;
      } else {
        columns = [
          { key: "TrayId", label: "Tray", width: 100 },
          { key: "InvoiceNo", label: "Invoice No", width: 130 },
          { key: "InvoiceDate", label: "Date", width: 100, format: "date" },
          { key: "InventoryCode", label: "Item Code", width: 130 },
          { key: "Description", label: "Description", width: 180 },
          { key: "Weight", label: "Weight (g)", width: 100, align: "right", format: "decimal3" },
          { key: "NetAmount", label: "Amount", width: 120, align: "right", format: "currency" }
        ];

        const query = `
          SELECT 
            ISNULL(RTRIM(inv.TrayId), 'NO_TRAY') AS TrayId,
            RTRIM(sh.InvoiceNo) AS InvoiceNo,
            sh.InvoiceDate,
            RTRIM(sd.InventoryCode) AS InventoryCode,
            ISNULL(RTRIM(sd.Description), '') AS Description,
            ROUND(ISNULL(sd.Weight, 0), 3) AS Weight,
            ROUND(ISNULL(sd.NetAmount, 0), 2) AS NetAmount
          FROM tblSalesHeader sh WITH (NOLOCK)
          INNER JOIN tblSalesDetail sd WITH (NOLOCK) ON sh.InvoiceNo = sd.InvoiceNo
          LEFT JOIN tblInventory inv WITH (NOLOCK) ON sd.InventoryCode = inv.InventoryCode
          ${where}
          ORDER BY inv.TrayId, sh.InvoiceDate DESC
        `;
        const resTWDet = await request.query(query);
        rows = resTWDet.recordset;
      }
    }

    // ==========================================
    // 22. PAY OUT DETAIL
    // ==========================================
    else if (reportId === "pay_out") {
      title = `Cash Pay Out Report From ${fromDateDisp} To ${toDateDisp}`;

      let where = ` WHERE po.PayOutDate >= @FromDate AND po.PayOutDate < @ToDateExclusive`;

      columns = [
        { key: "PayOutNo", label: "Voucher / No", width: 140 },
        { key: "PayOutDate", label: "Date", width: 110, format: "date" },
        { key: "Description", label: "Pay Out Category", width: 180 },
        { key: "PayOutDescription", label: "Detail Description", width: 220 },
        { key: "Amount", label: "Amount Paid", width: 130, align: "right", format: "currency" },
        { key: "Remarks", label: "Remarks", width: 180 }
      ];

      const query = `
        SELECT 
          RTRIM(po.PayOutNo) AS PayOutNo,
          po.PayOutDate,
          ISNULL(RTRIM(po.Description), RTRIM(po.Code)) AS Description,
          ISNULL(RTRIM(po.PayOutDescription), '') AS PayOutDescription,
          ROUND(ISNULL(po.Amount, 0), 2) AS Amount,
          ISNULL(RTRIM(po.Remarks), '') AS Remarks
        FROM tblPayOutDetail po WITH (NOLOCK)
        ${where}
        ORDER BY po.PayOutDate DESC
      `;
      const resPO = await request.query(query);
      rows = resPO.recordset;
    }

    // ==========================================
    // 23. GST REPORT
    // ==========================================
    else if (reportId === "gst_report") {
      title = `Comprehensive GST Audit Report From ${fromDateDisp} To ${toDateDisp}`;

      const where = buildSalesHeaderWhere("sh");

      columns = [
        { key: "InvoiceNo", label: "Invoice No", width: 130 },
        { key: "InvoiceDate", label: "Date", width: 110, format: "date" },
        { key: "CustomerName", label: "Customer Name", width: 180 },
        { key: "GSTType", label: "GST Type", width: 90, align: "center" },
        { key: "TotalValue", label: "Taxable Amount", width: 130, align: "right", format: "currency" },
        { key: "GST", label: "GST Collected", width: 120, align: "right", format: "currency" },
        { key: "TradeInGST", label: "Trade In GST", width: 120, align: "right", format: "currency" },
        { key: "NetTotal", label: "Gross Total", width: 130, align: "right", format: "currency" }
      ];

      const query = `
        SELECT 
          RTRIM(sh.InvoiceNo) AS InvoiceNo,
          sh.InvoiceDate,
          ISNULL(RTRIM(sh.CustomerName), RTRIM(sh.CustomerCode)) AS CustomerName,
          ISNULL(RTRIM(sh.GSTType), 'I') AS GSTType,
          ROUND(ISNULL(sh.TotalValue, 0), 2) AS TotalValue,
          ROUND(ISNULL(sh.GST, 0), 2) AS GST,
          ROUND(ISNULL(sh.TradeInGST, 0), 2) AS TradeInGST,
          ROUND(ISNULL(sh.NetTotal, 0), 2) AS NetTotal
        FROM tblSalesHeader sh WITH (NOLOCK)
        ${where}
        ORDER BY sh.InvoiceDate DESC
      `;
      const resGST = await request.query(query);
      rows = resGST.recordset;
    }

    // ==========================================
    // 24. FLEXI PAYMENTS
    // ==========================================
    else if (reportId === "flexi_payments") {
      title = `Flexi Payment Transactions From ${fromDateDisp} To ${toDateDisp}`;

      let where = ` WHERE pm.InvoiceDate >= @FromDate AND pm.InvoiceDate < @ToDateExclusive`;
      if (locationCode && locationCode !== "ALL" && locationCode.trim() !== "") {
        request.input("LocationCode", sql.VarChar, locationCode.trim());
        where += ` AND pm.LocationCode = @LocationCode`;
      }

      columns = [
        { key: "InvoiceNo", label: "Invoice No", width: 140 },
        { key: "InvoiceDate", label: "Date", width: 110, format: "date" },
        { key: "PayMode", label: "Payment Mode", width: 120 },
        { key: "Amount", label: "Flexi Amount", width: 130, align: "right", format: "currency" },
        { key: "SlipNo", label: "Slip Reference", width: 140 }
      ];

      const query = `
        SELECT 
          RTRIM(pm.InvoiceNo) AS InvoiceNo,
          pm.InvoiceDate,
          RTRIM(pm.PayMode) AS PayMode,
          ROUND(ISNULL(pm.Amount, 0), 2) AS Amount,
          ISNULL(RTRIM(pm.SlipNo), '-') AS SlipNo
        FROM tblPayModeDetail pm WITH (NOLOCK)
        ${where} AND RTRIM(pm.PayMode) IN ('INSTALLMENT', 'ORDER', 'TRADEIN', 'PAYNOW')
        ORDER BY pm.InvoiceDate DESC
      `;
      const resFP = await request.query(query);
      rows = resFP.recordset;
    }

    // ==========================================
    // 25. DEPOSIT JOURNAL
    // ==========================================
    else if (reportId === "deposit_journal") {
      title = `Customer Deposit Receipts From ${fromDateDisp} To ${toDateDisp}`;

      let where = ` WHERE ds.DepositDate >= @FromDate AND ds.DepositDate < @ToDateExclusive`;

      columns = [
        { key: "DepositNo", label: "Deposit No", width: 140 },
        { key: "DepositDate", label: "Date", width: 110, format: "date" },
        { key: "CustomerCode", label: "Customer Code", width: 130 },
        { key: "CustomerName", label: "Customer Name", width: 180 },
        { key: "DepositAmount", label: "Deposit Amt", width: 120, align: "right", format: "currency" },
        { key: "AmountPaid", label: "Amt Paid", width: 120, align: "right", format: "currency" },
        { key: "TotalAmount", label: "Total Order Val", width: 130, align: "right", format: "currency" },
        { key: "DepositUsed", label: "Used", width: 80, align: "center" },
        { key: "SalesInvoiceNo", label: "Applied Inv", width: 130 }
      ];

      const query = `
        SELECT 
          RTRIM(ds.DepositNo) AS DepositNo,
          ds.DepositDate,
          RTRIM(ds.CustomerCode) AS CustomerCode,
          ISNULL(RTRIM(ds.CustomerName), '') AS CustomerName,
          ROUND(ISNULL(ds.DepositAmount, 0), 2) AS DepositAmount,
          ROUND(ISNULL(ds.AmountPaid, 0), 2) AS AmountPaid,
          ROUND(ISNULL(ds.TotalAmount, 0), 2) AS TotalAmount,
          ISNULL(RTRIM(ds.DepositUsed), 'N') AS DepositUsed,
          ISNULL(RTRIM(ds.SalesInvoiceNo), '-') AS SalesInvoiceNo
        FROM tblDepositSave ds WITH (NOLOCK)
        ${where}
        ORDER BY ds.DepositDate DESC
      `;
      const resDJ = await request.query(query);
      rows = resDJ.recordset;
    }

    // ==========================================
    // 26. eTRS STATEMENT
    // ==========================================
    else if (reportId === "etrs_statement") {
      title = `eTRS ${voidFilter ? "Void " : ""}Statement From ${fromDateDisp} To ${toDateDisp}`;

      let where = ` WHERE et.BillDate >= @FromDate AND et.BillDate < @ToDateExclusive`;

      columns = [
        { key: "BillNo", label: "Bill No", width: 140 },
        { key: "BillDate", label: "Bill Date", width: 110, format: "date" },
        { key: "PassPortNo", label: "Passport No", width: 140 },
        { key: "SalesmanCode", label: "Salesman", width: 110 },
        { key: "NetAmount", label: "Net Amount", width: 130, align: "right", format: "currency" },
        { key: "Gst", label: "GST Claimable", width: 120, align: "right", format: "currency" },
        { key: "DocId", label: "Doc Reference", width: 140 },
        { key: "eTRSIssueDate", label: "Issue Date", width: 110, format: "date" }
      ];

      const query = `
        SELECT 
          RTRIM(et.BillNo) AS BillNo,
          et.BillDate,
          ISNULL(RTRIM(et.PassPortNo), '-') AS PassPortNo,
          RTRIM(et.SalesmanCode) AS SalesmanCode,
          ROUND(ISNULL(et.NetAmount, 0), 2) AS NetAmount,
          ROUND(ISNULL(et.Gst, 0), 2) AS Gst,
          ISNULL(RTRIM(et.DocId), '-') AS DocId,
          et.eTRSIssueDate
        FROM tbleTRSStatement et WITH (NOLOCK)
        ${where}
        ORDER BY et.BillDate DESC
      `;
      const resET = await request.query(query);
      rows = resET.recordset;
    }

    // ==========================================
    // 27. MISCELLANEOUS JOURNAL
    // ==========================================
    else if (reportId === "misc_journal") {
      title = `Miscellaneous Journal From ${fromDateDisp} To ${toDateDisp}`;

      let where = ` WHERE mh.Date >= @FromDate AND mh.Date < @ToDateExclusive`;
      if (miscType && miscType !== "ALL" && miscType.trim() !== "") {
        let code = miscType.trim();
        if (code === "Purchase Note") code = "PN";
        else if (code === "Credit Note") code = "CN";
        else if (code === "Misc.Note") code = "MC";
        else if (code === "Petty Cash") code = "PC";
        request.input("MiscTypeCode", sql.VarChar, code);
        where += ` AND mh.Type = @MiscTypeCode`;
      }

      columns = [
        { key: "VoucherNo", label: "Voucher No", width: 130 },
        { key: "Date", label: "Date", width: 110, format: "date" },
        { key: "Type", label: "Voucher Type", width: 120, align: "center" },
        { key: "Person", label: "Person / Party", width: 180 },
        { key: "Amount", label: "Amount", width: 120, align: "right", format: "currency" },
        { key: "GST", label: "GST", width: 100, align: "right", format: "currency" },
        { key: "NetAmount", label: "Net Amount", width: 130, align: "right", format: "currency" },
        { key: "Remarks", label: "Remarks", width: 180 }
      ];

      const query = `
        SELECT 
          RTRIM(CAST(mh.VoucherNo AS VARCHAR(30))) AS VoucherNo,
          mh.Date,
          CASE 
            WHEN RTRIM(mh.Type) = 'PN' THEN 'Purchase Note'
            WHEN RTRIM(mh.Type) = 'CN' THEN 'Credit Note'
            WHEN RTRIM(mh.Type) = 'MC' THEN 'Misc. Note'
            WHEN RTRIM(mh.Type) = 'PC' THEN 'Petty Cash'
            ELSE RTRIM(mh.Type)
          END AS Type,
          ISNULL(RTRIM(mh.Person), '-') AS Person,
          ROUND(ISNULL(mh.Amount, 0), 2) AS Amount,
          ROUND(ISNULL(mh.GST, 0), 2) AS GST,
          ROUND(ISNULL(mh.NetAmount, 0), 2) AS NetAmount,
          ISNULL(RTRIM(mh.Remarks), '') AS Remarks
        FROM tblMiscHistory mh WITH (NOLOCK)
        ${where}
        ORDER BY mh.Date DESC, mh.VoucherNo DESC
      `;
      const resMH = await request.query(query);
      rows = resMH.recordset;
    }

    // Dynamic totals calculation across all retrieved rows
    const totals = {
      totalRecords: rows.length,
      totalPcs: 0,
      totalWeight: 0,
      totalValue: 0,
      totalTInWeight: 0,
      totalTInValue: 0,
      totalBeforeGST: 0,
      totalMC: 0,
      totalCost: 0,
      totalGST: 0,
      totalAmount: 0
    };

    rows.forEach((r) => {
      if (r.TotalPCS !== undefined) totals.totalPcs += Number(r.TotalPCS) || 0;
      else if (r.PCS !== undefined) totals.totalPcs += Number(r.PCS) || 0;
      else if (r.TotalItems !== undefined) totals.totalPcs += Number(r.TotalItems) || 0;
      else if (r.SalesOrderQty !== undefined) totals.totalPcs += Number(r.SalesOrderQty) || 0;

      if (r.TotalWeight !== undefined) totals.totalWeight += Number(r.TotalWeight) || 0;
      else if (r.Weight !== undefined) totals.totalWeight += Number(r.Weight) || 0;

      if (r.Value !== undefined) totals.totalValue += Number(r.Value) || 0;
      if (r.TInWeight !== undefined) totals.totalTInWeight += Number(r.TInWeight) || 0;
      if (r.TInValue !== undefined) totals.totalTInValue += Number(r.TInValue) || 0;
      if (r.BeforeGST !== undefined) totals.totalBeforeGST += Number(r.BeforeGST) || 0;

      if (r.TotalSMC !== undefined) totals.totalMC += Number(r.TotalSMC) || 0;
      else if (r.SMC !== undefined) totals.totalMC += Number(r.SMC) || 0;
      else if (r.TotalMC !== undefined) totals.totalMC += Number(r.TotalMC) || 0;

      if (r.TotalCost !== undefined) totals.totalCost += Number(r.TotalCost) || 0;

      if (r.GST !== undefined) totals.totalGST += Number(r.GST) || 0;
      else if (r.Gst !== undefined) totals.totalGST += Number(r.Gst) || 0;

      if (r.GrandTotal !== undefined) totals.totalAmount += Number(r.GrandTotal) || 0;
      else if (r.NetTotal !== undefined) totals.totalAmount += Number(r.NetTotal) || 0;
      else if (r.ActualSales !== undefined) totals.totalAmount += Number(r.ActualSales) || 0;
      else if (r.TotalSales !== undefined) totals.totalAmount += Number(r.TotalSales) || 0;
      else if (r.Amount !== undefined) totals.totalAmount += Number(r.Amount) || 0;
      else if (r.NetAmount !== undefined) totals.totalAmount += Number(r.NetAmount) || 0;
      else if (r.TotalAmount !== undefined) totals.totalAmount += Number(r.TotalAmount) || 0;
      else if (r.InsTotal !== undefined) totals.totalAmount += Number(r.InsTotal) || 0;
      else if (r.DepositAmount !== undefined) totals.totalAmount += Number(r.DepositAmount) || 0;
    });

    totals.totalWeight = Number(totals.totalWeight.toFixed(2));
    totals.totalValue = Number(totals.totalValue.toFixed(2));
    totals.totalTInWeight = Number(totals.totalTInWeight.toFixed(2));
    totals.totalTInValue = Number(totals.totalTInValue.toFixed(2));
    totals.totalBeforeGST = Number(totals.totalBeforeGST.toFixed(2));
    totals.totalMC = Number(totals.totalMC.toFixed(2));
    totals.totalCost = Number(totals.totalCost.toFixed(2));
    totals.totalGST = Number(totals.totalGST.toFixed(2));
    totals.totalAmount = Number(totals.totalAmount.toFixed(2));

    res.json({
      success: true,
      title,
      company: companyInfo,
      dateRange: {
        from: fromDateDisp,
        to: toDateDisp
      },
      columns,
      data: rows,
      totals
    });
  } catch (err) {
    console.error("Sales Reports Generation Error:", err);
    res.status(500).json({ success: false, message: err.message });
  }
});

      // POST /api/salesreports/download-html
router.post("/download-html", async (req, res) => {
  try {
    const { generateReportHtml } = require("../utils/reportHtmlTemplate");
    const { title, columns, data, totals, companyInfo, fromDate, toDate, showPrintButton } = req.body;
    const html = await generateReportHtml({
      title: title || "Sales Report",
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
    console.error("Sales Reports Download HTML Error:", err);
    res.status(500).send("Error generating report HTML: " + err.message);
  }
});

// EXPORT EXCEL & PDF ENDPOINTS
router.post("/export-excel", (req, res) => {
  const { handleExportExcel } = require("../utils/reportExportHandlers");
  handleExportExcel({ req, res, defaultTitle: "Sales_Report" });
});

router.post("/export-pdf", (req, res) => {
  const { handleExportPdf } = require("../utils/reportExportHandlers");
  handleExportPdf({ req, res, defaultTitle: "Sales_Report" });
});

module.exports = router;
