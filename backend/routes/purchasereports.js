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

  // If toDateStr is omitted or empty, legacy Smart Gold uses fromDate as toDate
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

// GET /api/purchasereports/suppliers
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
    console.error("Error fetching purchase suppliers:", err);
    res.status(500).json({ success: false, message: err.message });
  }
});

// GET /api/purchasereports/classes
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
    console.error("Error fetching purchase classes:", err);
    res.status(500).json({ success: false, message: err.message });
  }
});

// GET /api/purchasereports/categories
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
    console.error("Error fetching purchase categories:", err);
    res.status(500).json({ success: false, message: err.message });
  }
});

// GET /api/purchasereports/catalogues
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
    console.error("Error fetching purchase catalogues:", err);
    res.status(500).json({ success: false, message: err.message });
  }
});

// GET /api/purchasereports/sizes
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
    console.error("Error fetching purchase sizes:", err);
    res.status(500).json({ success: false, message: err.message });
  }
});

// GET /api/purchasereports/server-date
router.get("/server-date", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query("SELECT GETDATE() AS ServerDate");
    const serverDate = result.recordset[0]?.ServerDate || new Date();
    res.json({ success: true, serverDate });
  } catch (err) {
    console.error("Error fetching server date:", err);
    res.status(500).json({ success: false, message: err.message, serverDate: new Date() });
  }
});

// GET /api/purchasereports/company-info
router.get("/company-info", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query(`
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

    res.json({ success: true, data: result.recordset[0] || {} });
  } catch (err) {
    console.error("Error fetching company info:", err);
    res.status(500).json({ success: false, message: err.message });
  }
});

// ==========================================
// 2. REPORT GENERATION ENGINE
// ==========================================

const SUPPORTED_REPORTS = [
  "job_sheet",
  "purchase",
  "purchase_fix",
  "sales_order",
  "sales_order_delete",
  "sales_order_pending",
  "purchase_return",
  "purchase_tally",
  "supplier_payment",
  "supplier_mc",
  "supplier_analysis",
  "trade_in_journal",
  "trade_in_gold_issued",
  "vendor_weight_balance",
  "vendor_mc_balance",
  "purchase_details"
];

router.post("/generate", async (req, res) => {
  try {
    const pool = req.pool;
    const {
      reportId = "job_sheet",
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
      supplierCode,
      invoiceNo,
      jobSheetNo,
      classCode,
      categoryCode,
      catalogueCode,
      sizeCode,
      fromDate,
      toDate,
      fixType = "All", // "All", "Fix", "Un Fix"
      statusPending = false,
      statusClosed = false,
      statusUnclosed = false
    } = filters;

    if (fromDate || toDate) {
      const dateVal = validateBackendDates(fromDate, toDate);
      if (!dateVal.isValid) {
        return res.status(400).json({ success: false, message: dateVal.message });
      }
    }

    // Date range parsing & validation
    if (!fromDate) {
      return res.status(400).json({
        success: false,
        message: "From Date is required."
      });
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

    // Retrieve company info for report header
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
          RTRIM(Fax) AS Fax
        FROM tblCompany WITH (NOLOCK)
      `);
      if (compRes.recordset.length > 0) companyInfo = compRes.recordset[0];
    } catch (e) {
      console.warn("Could not fetch company info:", e.message);
    }

    let title = "";
    let columns = [];
    let rows = [];

    // ==========================================
    // 1. JOB SHEET REPORT
    // ==========================================
    if (reportId === "job_sheet") {
      title = `${isSummary ? "Summary" : "Detail"} Job Sheet Report From ${fromDateDisp} To ${toDateDisp}`;

      let whereClause = ` WHERE j.JobSheetDate >= @FromDate AND j.JobSheetDate < @ToDateExclusive`;

      if (supplierCode && supplierCode !== "ALL" && supplierCode.trim() !== "") {
        request.input("SupplierCode", sql.VarChar, supplierCode.trim());
        whereClause += ` AND RTRIM(j.SupplierCode) = @SupplierCode`;
      }

      if (invoiceNo && invoiceNo.trim() !== "") {
        request.input("PurchaseBillNo", sql.VarChar, invoiceNo.trim());
        whereClause += ` AND RTRIM(j.PurchaseBillNo) = @PurchaseBillNo`;
      }

      if (jobSheetNo && jobSheetNo.trim() !== "") {
        request.input("JobSheetNo", sql.VarChar, jobSheetNo.trim());
        whereClause += ` AND RTRIM(j.JobSheetNo) = @JobSheetNo`;
      }

      // Type of Fix
      if (fixType === "Fix") {
        whereClause += ` AND RTRIM(j.FixStatus) = 'Y'`;
      } else if (fixType === "Un Fix") {
        whereClause += ` AND (RTRIM(j.FixStatus) = 'N' OR RTRIM(j.FixStatus) = 'U' OR j.FixStatus IS NULL)`;
      }

      // Status filters (Pending, Closed, Unclosed)
      const statusConditions = [];
      if (statusClosed) {
        statusConditions.push(`RTRIM(j.BillClose) = 'Y'`);
      }
      if (statusUnclosed) {
        statusConditions.push(`(RTRIM(j.BillClose) = 'N' OR j.BillClose IS NULL)`);
      }
      if (statusPending) {
        statusConditions.push(`(RTRIM(j.BillClose) = 'N' OR j.BillClose IS NULL)`);
      }
      if (statusConditions.length > 0) {
        whereClause += ` AND (${statusConditions.join(" OR ")})`;
      }

      if (isSummary) {
        columns = [
          { key: "JobSheetNo", label: "Job Sheet No", width: 130 },
          { key: "JobSheetDate", label: "Job Sheet Date", width: 120, format: "date" },
          { key: "PurchaseBillNo", label: "Invoice / Bill No", width: 140 },
          { key: "SupplierCode", label: "Supplier Code", width: 120 },
          { key: "PCS", label: "PCS", width: 90, align: "right" },
          { key: "Weight", label: "Weight (g)", width: 130, align: "right", format: "decimal3" },
          { key: "MC", label: "MC", width: 120, align: "right", format: "currency" },
          { key: "GST", label: "GST", width: 100, align: "right", format: "currency" },
          { key: "TotalCost", label: "Total Cost", width: 130, align: "right", format: "currency" }
        ];

        let summaryWhere = whereClause;
        if (categoryCode && categoryCode !== "ALL" && categoryCode.trim() !== "") {
          request.input("CategoryCode", sql.VarChar, categoryCode.trim());
          summaryWhere += ` AND RTRIM(jd.CategoryCode) = @CategoryCode`;
        }
        if (catalogueCode && catalogueCode !== "ALL" && catalogueCode.trim() !== "") {
          request.input("CatalogueCode", sql.VarChar, catalogueCode.trim());
          summaryWhere += ` AND RTRIM(jd.CatalogueCode) = @CatalogueCode`;
        }

        const query = `
          SELECT 
            j.JobSheetDate,
            ROUND(ISNULL(jd.Weight, 0), 3) AS Weight,
            ISNULL(jd.PCS, 1) AS PCS,
            ROUND(ISNULL(jd.TotalCost, 0), 2) AS TotalCost,
            ROUND(ISNULL(jd.MC, 0), 2) AS MC,
            ROUND(ISNULL(j.GST, 0), 2) AS GST,
            RTRIM(jd.JobSheetNo) AS JobSheetNo,
            RTRIM(j.PurchaseBillNo) AS PurchaseBillNo,
            RTRIM(j.SupplierCode) AS SupplierCode
          FROM tblJobSheetHeader j WITH (NOLOCK)
          INNER JOIN tblJobSheetDetail jd WITH (NOLOCK) ON j.JobSheetNo = jd.JobSheetNo
          ${summaryWhere}
          ORDER BY j.JobSheetDate DESC, jd.JobSheetNo DESC
        `;
        const resJob = await request.query(query);
        rows = resJob.recordset;
      } else {
        // Detail Job Sheet using tblJobSheetDetail
        columns = [
          { key: "JobSheetNo", label: "Job Sheet No", width: 130 },
          { key: "JobSheetDate", label: "Job Sheet Date", width: 120, format: "date" },
          { key: "Description", label: "Description", width: 220 },
          { key: "PCS", label: "PCS", width: 80, align: "right" },
          { key: "Weight", label: "Weight (g)", width: 120, align: "right", format: "decimal3" },
          { key: "MC", label: "MC", width: 110, align: "right", format: "currency" },
          { key: "TotalCost", label: "Total Cost", width: 130, align: "right", format: "currency" }
        ];

        let detailWhere = whereClause;
        if (categoryCode && categoryCode !== "ALL" && categoryCode.trim() !== "") {
          request.input("CategoryCode", sql.VarChar, categoryCode.trim());
          detailWhere += ` AND RTRIM(jd.CategoryCode) = @CategoryCode`;
        }
        if (catalogueCode && catalogueCode !== "ALL" && catalogueCode.trim() !== "") {
          request.input("CatalogueCode", sql.VarChar, catalogueCode.trim());
          detailWhere += ` AND RTRIM(jd.CatalogueCode) = @CatalogueCode`;
        }

        const query = `
          SELECT 
            j.JobSheetDate,
            RTRIM(jd.Description) AS Description,
            ROUND(ISNULL(jd.Weight, 0), 3) AS Weight,
            ISNULL(jd.PCS, 1) AS PCS,
            ROUND(ISNULL(jd.TotalCost, 0), 2) AS TotalCost,
            ROUND(ISNULL(jd.MC, 0), 2) AS MC,
            RTRIM(jd.JobSheetNo) AS JobSheetNo
          FROM tblJobSheetHeader j WITH (NOLOCK)
          INNER JOIN tblJobSheetDetail jd WITH (NOLOCK) ON j.JobSheetNo = jd.JobSheetNo
          ${detailWhere}
          ORDER BY j.JobSheetDate DESC, jd.JobSheetNo DESC
        `;
        const resDetail = await request.query(query);
        rows = resDetail.recordset;
      }
    }

    // ==========================================
    // 2. PURCHASE REPORT
    // ==========================================
    else if (reportId === "purchase") {
      title = `${isSummary ? "Summary" : "Detail"} Purchase Report From ${fromDateDisp} To ${toDateDisp}`;

      let whereClause = ` WHERE p.CreateDate >= @FromDate AND p.CreateDate < @ToDateExclusive`;

      if (supplierCode && supplierCode !== "ALL" && supplierCode.trim() !== "") {
        request.input("SupplierCode", sql.VarChar, supplierCode.trim());
        whereClause += ` AND RTRIM(p.SupplierCode) = @SupplierCode`;
      }

      if (invoiceNo && invoiceNo.trim() !== "") {
        request.input("PurchaseBillNo", sql.VarChar, invoiceNo.trim());
        whereClause += ` AND RTRIM(p.PurchaseBillNo) = @PurchaseBillNo`;
      }

      if (jobSheetNo && jobSheetNo.trim() !== "") {
        request.input("JobSheetNo", sql.VarChar, jobSheetNo.trim());
        whereClause += ` AND RTRIM(p.JobSheetNo) = @JobSheetNo`;
      }

      if (classCode && classCode !== "ALL" && classCode.trim() !== "") {
        request.input("ClassCode", sql.VarChar, classCode.trim());
        whereClause += ` AND RTRIM(p.ClassCode) = @ClassCode`;
      }

      if (categoryCode && categoryCode !== "ALL" && categoryCode.trim() !== "") {
        request.input("CategoryCode", sql.VarChar, categoryCode.trim());
        whereClause += ` AND RTRIM(p.CategoryCode) = @CategoryCode`;
      }

      if (catalogueCode && catalogueCode !== "ALL" && catalogueCode.trim() !== "") {
        request.input("CatalogueCode", sql.VarChar, catalogueCode.trim());
        whereClause += ` AND RTRIM(p.CatalogueCode) = @CatalogueCode`;
      }

      if (isSummary) {
        columns = [
          { key: "PurchaseBillNo", label: "Invoice / Bill No", width: 140 },
          { key: "JobSheetNo", label: "Job Sheet No", width: 130 },
          { key: "SupplierCode", label: "Supplier Code", width: 120 },
          { key: "SupplierName", label: "Supplier Name", width: 200 },
          { key: "TotalItems", label: "Total Items", width: 100, align: "right" },
          { key: "TotalPcs", label: "Total PCS", width: 90, align: "right" },
          { key: "TotalWeight", label: "Total Weight (g)", width: 130, align: "right", format: "decimal3" },
          { key: "TotalMC", label: "Total MC", width: 120, align: "right", format: "currency" },
          { key: "TotalCost", label: "Total Cost", width: 130, align: "right", format: "currency" }
        ];

        const query = `
          SELECT 
            RTRIM(p.PurchaseBillNo) AS PurchaseBillNo,
            RTRIM(p.JobSheetNo) AS JobSheetNo,
            RTRIM(p.SupplierCode) AS SupplierCode,
            ISNULL(RTRIM(s.SupplierName), RTRIM(p.SupplierCode)) AS SupplierName,
            COUNT(p.InventoryCode) AS TotalItems,
            SUM(ISNULL(p.PCS, 1)) AS TotalPcs,
            ROUND(SUM(ISNULL(p.Weight, 0)), 3) AS TotalWeight,
            ROUND(SUM(ISNULL(p.MC, 0)), 2) AS TotalMC,
            ROUND(SUM(ISNULL(p.TotalCost, 0)), 2) AS TotalCost
          FROM tblPurchase p WITH (NOLOCK)
          LEFT JOIN tblSupplier s WITH (NOLOCK) ON p.SupplierCode = s.SupplierCode
          ${whereClause}
          GROUP BY p.PurchaseBillNo, p.JobSheetNo, p.SupplierCode, s.SupplierName
          ORDER BY p.PurchaseBillNo DESC
        `;
        const resPur = await request.query(query);
        rows = resPur.recordset;
      } else {
        columns = [
          { key: "PurchaseBillNo", label: "Bill No", width: 120 },
          { key: "JobSheetNo", label: "Job Sheet No", width: 120 },
          { key: "SupplierCode", label: "Supplier", width: 100 },
          { key: "InventoryCode", label: "Inventory Code", width: 140 },
          { key: "Description", label: "Description", width: 200 },
          { key: "ClassCode", label: "Class", width: 80 },
          { key: "CategoryCode", label: "Category", width: 90 },
          { key: "CatalogueCode", label: "Catalogue", width: 100 },
          { key: "PCS", label: "PCS", width: 70, align: "right" },
          { key: "Weight", label: "Weight (g)", width: 110, align: "right", format: "decimal3" },
          { key: "CostPerGram", label: "Cost / g", width: 100, align: "right", format: "currency" },
          { key: "MC", label: "MC", width: 90, align: "right", format: "currency" },
          { key: "TotalCost", label: "Total Cost", width: 120, align: "right", format: "currency" }
        ];

        const query = `
          SELECT 
            RTRIM(p.PurchaseBillNo) AS PurchaseBillNo,
            RTRIM(p.JobSheetNo) AS JobSheetNo,
            RTRIM(p.SupplierCode) AS SupplierCode,
            RTRIM(p.InventoryCode) AS InventoryCode,
            RTRIM(p.Description) AS Description,
            RTRIM(p.ClassCode) AS ClassCode,
            RTRIM(p.CategoryCode) AS CategoryCode,
            RTRIM(p.CatalogueCode) AS CatalogueCode,
            ISNULL(p.PCS, 1) AS PCS,
            ROUND(ISNULL(p.Weight, 0), 3) AS Weight,
            ROUND(ISNULL(p.CostPerGram, 0), 2) AS CostPerGram,
            ROUND(ISNULL(p.MC, 0), 2) AS MC,
            ROUND(ISNULL(p.TotalCost, 0), 2) AS TotalCost
          FROM tblPurchase p WITH (NOLOCK)
          ${whereClause}
          ORDER BY p.CreateDate DESC, p.PurchaseBillNo, p.InventoryCode
        `;
        const resPurDet = await request.query(query);
        rows = resPurDet.recordset;
      }
    }

    // ==========================================
    // 3. PURCHASE RETURN REPORT
    // ==========================================
    else if (reportId === "purchase_return") {
      title = `${isSummary ? "Summary" : "Detail"} Purchase Return Report From ${fromDateDisp} To ${toDateDisp}`;

      let whereClause = ` WHERE r.ReturnDate >= @FromDate AND r.ReturnDate < @ToDateExclusive`;

      if (supplierCode && supplierCode !== "ALL" && supplierCode.trim() !== "") {
        request.input("SupplierCode", sql.VarChar, supplierCode.trim());
        whereClause += ` AND RTRIM(r.SupplierCode) = @SupplierCode`;
      }

      if (invoiceNo && invoiceNo.trim() !== "") {
        request.input("InvoiceNo", sql.VarChar, invoiceNo.trim());
        whereClause += ` AND RTRIM(r.ReturnNo) = @InvoiceNo`;
      }

      if (isSummary) {
        columns = [
          { key: "ReturnNo", label: "Return No", width: 130 },
          { key: "ReturnDate", label: "Return Date", width: 110, format: "date" },
          { key: "SupplierName", label: "Supplier Name", width: 220 },
          { key: "InventoryCode", label: "Inventory Code", width: 140 },
          { key: "Description", label: "Description", width: 200 },
          { key: "PCS", label: "PCS", width: 70, align: "right" },
          { key: "Weight", label: "Weight (g)", width: 110, align: "right", format: "decimal3" },
          { key: "GoldLoss", label: "Gold Loss", width: 100, align: "right", format: "decimal3" },
          { key: "MC", label: "MC", width: 90, align: "right", format: "currency" },
          { key: "TotalValue", label: "Item Total", width: 120, align: "right", format: "currency" },
          { key: "TotalGst", label: "Total GST", width: 100, align: "right", format: "currency" },
          { key: "NetAmount", label: "Net Amount", width: 120, align: "right", format: "currency" },
          { key: "Remarks", label: "Remarks", width: 180 }
        ];

        const query = `
          SELECT 
            r.ReturnDate,
            RTRIM(r.ReturnNo) AS ReturnNo,
            RTRIM(rd.ReturnNo) AS DetailReturnNo,
            ROUND(ISNULL(r.TotalGst, 0), 2) AS TotalGst,
            ROUND(ISNULL(r.NetAmount, 0), 2) AS NetAmount,
            ISNULL(RTRIM(r.Remarks), '') AS Remarks,
            RTRIM(rd.InventoryCode) AS InventoryCode,
            RTRIM(rd.Description) AS Description,
            ROUND(ISNULL(rd.Weight, 0), 3) AS Weight,
            ROUND(ISNULL(rd.GoldLoss, 0), 3) AS GoldLoss,
            ROUND(ISNULL(rd.MC, 0), 2) AS MC,
            ISNULL(rd.PCS, 1) AS PCS,
            ROUND(ISNULL(rd.TotalValue, 0), 2) AS TotalValue,
            ROUND(ISNULL(r.TotalValue, 0), 2) AS HeaderTotalValue,
            ISNULL(RTRIM(s.SupplierName), '') AS SupplierName,
            ISNULL(RTRIM(s.Address1), '') AS Address1,
            ISNULL(RTRIM(s.Address2), '') AS Address2,
            ROUND(ISNULL(r.TotalWeight, 0), 3) AS TotalWeight,
            ISNULL(r.TotalPCS, 0) AS TotalPCS
          FROM tblPurchaseReturnHeader r WITH (NOLOCK)
          INNER JOIN tblPurchaseReturnDetail rd WITH (NOLOCK) ON r.ReturnNo = rd.ReturnNo
          LEFT OUTER JOIN tblSupplier s WITH (NOLOCK) ON r.SupplierCode = s.SupplierCode
          ${whereClause}
          ORDER BY rd.ReturnNo
        `;
        const resRet = await request.query(query);
        rows = resRet.recordset;
      } else {
        // Detail with line items from tblPurchaseReturnDetail & tblInventory
        columns = [
          { key: "ReturnNo", label: "Return No", width: 130 },
          { key: "ReturnDate", label: "Return Date", width: 110, format: "date" },
          { key: "SupplierCode", label: "Supplier", width: 110 },
          { key: "InventoryCode", label: "Inventory Code", width: 140 },
          { key: "Description", label: "Description", width: 200 },
          { key: "PCS", label: "PCS", width: 70, align: "right" },
          { key: "Weight", label: "Weight (g)", width: 110, align: "right", format: "decimal3" },
          { key: "GoldLoss", label: "Gold Loss", width: 100, align: "right", format: "decimal3" },
          { key: "MC", label: "MC", width: 90, align: "right", format: "currency" },
          { key: "TotalValue", label: "Total Value", width: 120, align: "right", format: "currency" },
          { key: "QtyOnHand", label: "Qty On Hand", width: 100, align: "right" }
        ];

        const query = `
          SELECT 
            RTRIM(rd.InventoryCode) AS InventoryCode,
            RTRIM(rd.Description) AS Description,
            ROUND(ISNULL(rd.Weight, 0), 3) AS Weight,
            ROUND(ISNULL(rd.GoldLoss, 0), 3) AS GoldLoss,
            ROUND(ISNULL(rd.MC, 0), 2) AS MC,
            ISNULL(rd.PCS, 1) AS PCS,
            ROUND(ISNULL(rd.TotalValue, 0), 2) AS TotalValue,
            RTRIM(r.ReturnNo) AS ReturnNo,
            r.ReturnDate,
            RTRIM(r.SupplierCode) AS SupplierCode,
            ISNULL(stk.QtyOnHand, 0) AS QtyOnHand,
            ISNULL(RTRIM(inv.InventoryCode), RTRIM(rd.InventoryCode)) AS InvInventoryCode
          FROM tblPurchaseReturnHeader r WITH (NOLOCK)
          INNER JOIN tblPurchaseReturnDetail rd WITH (NOLOCK) ON r.ReturnNo = rd.ReturnNo
          LEFT JOIN tblInventory inv WITH (NOLOCK) ON rd.InventoryCode = inv.InventoryCode
          LEFT JOIN tblStock stk WITH (NOLOCK) ON rd.InventoryCode = stk.InventoryCode
          ${whereClause}
          ORDER BY r.ReturnNo, rd.InventoryCode
        `;
        const resRetDet = await request.query(query);
        rows = resRetDet.recordset;
      }
    }

    // ==========================================
    // 4. SALES ORDER REPORT
    // ==========================================
    else if (reportId === "sales_order") {
      title = `${isSummary ? "Summary" : "Detail"} Sales Order Report From ${fromDateDisp} To ${toDateDisp}`;

      let whereClause = ` WHERE so.ModifyDate >= @FromDate AND so.ModifyDate < @ToDateExclusive`;

      if (supplierCode && supplierCode !== "ALL" && supplierCode.trim() !== "") {
        request.input("SupplierCode", sql.VarChar, supplierCode.trim());
        whereClause += ` AND RTRIM(so.SupplierCode) = @SupplierCode`;
      }

      if (classCode && classCode !== "ALL" && classCode.trim() !== "") {
        request.input("ClassCode", sql.VarChar, classCode.trim());
        whereClause += ` AND RTRIM(cat.ClassCode) = @ClassCode`;
      }

      if (categoryCode && categoryCode !== "ALL" && categoryCode.trim() !== "") {
        request.input("CategoryCode", sql.VarChar, categoryCode.trim());
        whereClause += ` AND RTRIM(so.CategoryCode) = @CategoryCode`;
      }

      if (catalogueCode && catalogueCode !== "ALL" && catalogueCode.trim() !== "") {
        request.input("CatalogueCode", sql.VarChar, catalogueCode.trim());
        whereClause += ` AND RTRIM(so.CatalogueCode) = @CatalogueCode`;
      }

      columns = [
        { key: "SalesOrderNo", label: "Sales Order No", width: 140 },
        { key: "SalesOrderDate", label: "Order Date", width: 120, format: "date" },
        { key: "DueDate", label: "Due Date", width: 120, format: "date" },
        { key: "SupplierCode", label: "Supplier", width: 110 },
        { key: "CategoryCode", label: "Category", width: 110 },
        { key: "CatalogueCode", label: "Catalogue", width: 120 },
        { key: "SalesOrderQty", label: "Order Qty", width: 100, align: "right" },
        { key: "Weight", label: "Weight (g)", width: 120, align: "right", format: "decimal3" },
        { key: "Remarks", label: "Remarks", width: 200 }
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
          ROUND(ISNULL(so.Weight, 0), 3) AS Weight,
          ISNULL(RTRIM(so.Remarks), '') AS Remarks
        FROM tblSalesOrderDetail so WITH (NOLOCK)
        LEFT JOIN tblCategory cat WITH (NOLOCK) ON so.CategoryCode = cat.CategoryCode
        ${whereClause}
        ORDER BY so.SalesOrderDate DESC, so.SalesOrderNo DESC
      `;
      const resSO = await request.query(query);
      rows = resSO.recordset;
    }

    // ==========================================
    // 4B. SALES ORDER DELETE REPORT (rptSalesOrderDetailDelete_AJ.rpt)
    // ==========================================
    else if (reportId === "sales_order_delete") {
      title = `Sales Order Delete Report From ${fromDateDisp} To ${toDateDisp}`;

      let whereClause = ` WHERE so.ModifyDate >= @FromDate AND so.ModifyDate < @ToDateExclusive`;

      if (supplierCode && supplierCode !== "ALL" && supplierCode.trim() !== "") {
        request.input("SupplierCode", sql.VarChar, supplierCode.trim());
        whereClause += ` AND RTRIM(so.SupplierCode) = @SupplierCode`;
      }

      columns = [
        { key: "SalesOrderNo", label: "Sales Order No", width: 140 },
        { key: "SalesOrderDate", label: "Order Date", width: 120, format: "date" },
        { key: "SupplierCode", label: "Supplier", width: 110 },
        { key: "CategoryCode", label: "Category", width: 110 },
        { key: "CatalogueCode", label: "Catalogue", width: 120 },
        { key: "SalesOrderQty", label: "Qty", width: 90, align: "right" },
        { key: "Weight", label: "Weight (g)", width: 120, align: "right", format: "decimal3" },
        { key: "Remarks", label: "Remarks", width: 200 }
      ];

      const query = `
        SELECT 
          RTRIM(so.SalesOrderNo) AS SalesOrderNo,
          so.SalesOrderDate,
          RTRIM(so.SupplierCode) AS SupplierCode,
          RTRIM(so.CategoryCode) AS CategoryCode,
          RTRIM(so.CatalogueCode) AS CatalogueCode,
          ISNULL(so.SalesOrderQty, 0) AS SalesOrderQty,
          ROUND(ISNULL(so.Weight, 0), 3) AS Weight,
          ISNULL(RTRIM(so.Remarks), '') AS Remarks
        FROM tblSalesOrderDetailDelete so WITH (NOLOCK)
        ${whereClause}
        ORDER BY so.SalesOrderDate DESC, so.SalesOrderNo DESC
      `;
      const resSOD = await request.query(query);
      rows = resSOD.recordset;
    }

    // ==========================================
    // 5. SALES ORDER PENDING REPORT
    // ==========================================
    else if (reportId === "sales_order_pending") {
      title = `Sales Order Pending Report From ${fromDateDisp} To ${toDateDisp}`;

      // In legacy code, SalesOrderQty > ReceivedQty or pending status
      let whereClause = ` WHERE (so.ModifyDate >= @FromDate AND so.ModifyDate < @ToDateExclusive) AND (ISNULL(h.SalesOrderQty, so.SalesOrderQty) > ISNULL(h.ReceivedQty, 0))`;

      if (supplierCode && supplierCode !== "ALL" && supplierCode.trim() !== "") {
        request.input("SupplierCode", sql.VarChar, supplierCode.trim());
        whereClause += ` AND RTRIM(so.SupplierCode) = @SupplierCode`;
      }

      if (categoryCode && categoryCode !== "ALL" && categoryCode.trim() !== "") {
        request.input("CategoryCode", sql.VarChar, categoryCode.trim());
        whereClause += ` AND RTRIM(so.CategoryCode) = @CategoryCode`;
      }

      if (catalogueCode && catalogueCode !== "ALL" && catalogueCode.trim() !== "") {
        request.input("CatalogueCode", sql.VarChar, catalogueCode.trim());
        whereClause += ` AND RTRIM(so.CatalogueCode) = @CatalogueCode`;
      }

      columns = [
        { key: "SalesOrderNo", label: "Sales Order No", width: 140 },
        { key: "SalesOrderDate", label: "Order Date", width: 120, format: "date" },
        { key: "DueDate", label: "Due Date", width: 120, format: "date" },
        { key: "SupplierCode", label: "Supplier", width: 110 },
        { key: "CategoryCode", label: "Category", width: 110 },
        { key: "CatalogueCode", label: "Catalogue", width: 120 },
        { key: "SalesOrderQty", label: "Order Qty", width: 100, align: "right" },
        { key: "Weight", label: "Pending Wt (g)", width: 130, align: "right", format: "decimal3" },
        { key: "Remarks", label: "Remarks", width: 200 }
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
          ROUND(ISNULL(so.Weight, 0), 3) AS Weight,
          ISNULL(RTRIM(so.Remarks), '') AS Remarks
        FROM tblSalesOrderDetail so WITH (NOLOCK)
        LEFT JOIN tblSalesOrderHeader h WITH (NOLOCK) ON so.SalesOrderNo = h.SalesOrderNo
        ${whereClause}
        ORDER BY so.DueDate ASC, so.SalesOrderNo ASC
      `;
      const resSOP = await request.query(query);
      rows = resSOP.recordset;
    }

    // ==========================================
    // 6. PURCHASE FIX REPORT
    // ==========================================
    else if (reportId === "purchase_fix") {
      title = `${isSummary ? "Summary" : "Detail"} Purchase Fix Report From ${fromDateDisp} To ${toDateDisp}`;

      let whereClause = ` WHERE pf.ModifyDate >= @FromDate AND pf.ModifyDate < @ToDateExclusive`;

      if (supplierCode && supplierCode !== "ALL" && supplierCode.trim() !== "") {
        request.input("SupplierCode", sql.VarChar, supplierCode.trim());
        whereClause += ` AND RTRIM(pf.SupplierCode) = @SupplierCode`;
      }

      if (invoiceNo && invoiceNo.trim() !== "") {
        request.input("InvoiceNo", sql.VarChar, invoiceNo.trim());
        whereClause += ` AND RTRIM(pf.InvoiceNo) = @InvoiceNo`;
      }

      columns = [
        { key: "InvoiceNo", label: "Invoice No", width: 130 },
        { key: "InvoiceDate", label: "Invoice Date", width: 120, format: "date" },
        { key: "SupplierCode", label: "Supplier Code", width: 120 },
        { key: "SupplierName", label: "Supplier Name", width: 200 },
        { key: "PaymentType", label: "Payment Type", width: 120 },
        { key: "PurchaseWeight", label: "Purchase Wt (g)", width: 130, align: "right", format: "decimal3" },
        { key: "IssuedWeight", label: "Issued Wt (g)", width: 130, align: "right", format: "decimal3" },
        { key: "BalanceWeight", label: "Bal Wt (g)", width: 120, align: "right", format: "decimal3" },
        { key: "PurchaseAmount", label: "Purchase Amount", width: 130, align: "right", format: "currency" },
        { key: "PaidAmount", label: "Paid Amount", width: 120, align: "right", format: "currency" }
      ];

      const query = `
        SELECT 
          RTRIM(pf.InvoiceNo) AS InvoiceNo,
          pf.InvoiceDate,
          RTRIM(pf.SupplierCode) AS SupplierCode,
          ISNULL(RTRIM(s.SupplierName), RTRIM(pf.SupplierCode)) AS SupplierName,
          RTRIM(pf.PaymentType) AS PaymentType,
          ROUND(ISNULL(pf.PurchaseWeight, 0), 3) AS PurchaseWeight,
          ROUND(ISNULL(pf.IssuedWeight, 0), 3) AS IssuedWeight,
          ROUND(ISNULL(pf.BalanceWeight, 0), 3) AS BalanceWeight,
          ROUND(ISNULL(pf.PurchaseAmount, 0), 2) AS PurchaseAmount,
          ROUND(ISNULL(pf.PaidAmount, 0), 2) AS PaidAmount
        FROM tblPurchaseFix pf WITH (NOLOCK)
        LEFT JOIN tblSupplier s WITH (NOLOCK) ON pf.SupplierCode = s.SupplierCode
        ${whereClause}
        ORDER BY pf.InvoiceDate DESC, pf.InvoiceNo DESC
      `;
      const resPF = await request.query(query);
      rows = resPF.recordset;
    }

    // ==========================================
    // 7. SUPPLIER MC REPORT
    // ==========================================
    else if (reportId === "supplier_mc") {
      title = `Supplier Making Charge (MC) Report`;

      let whereClause = ` WHERE 1=1`;

      if (supplierCode && supplierCode !== "ALL" && supplierCode.trim() !== "") {
        request.input("SupplierCode", sql.VarChar, supplierCode.trim());
        whereClause += ` AND RTRIM(smc.SupplierCode) = @SupplierCode`;
      }

      if (catalogueCode && catalogueCode !== "ALL" && catalogueCode.trim() !== "") {
        request.input("CatalogueCode", sql.VarChar, catalogueCode.trim());
        whereClause += ` AND RTRIM(smc.CatalogueCode) = @CatalogueCode`;
      }

      columns = [
        { key: "SupplierCode", label: "Supplier Code", width: 130 },
        { key: "SupplierName", label: "Supplier Name", width: 220 },
        { key: "CatalogueCode", label: "Catalogue Code", width: 130 },
        { key: "Type", label: "Type", width: 100 },
        { key: "FromWeight", label: "From Wt (g)", width: 120, align: "right", format: "decimal3" },
        { key: "ToWeight", label: "To Wt (g)", width: 120, align: "right", format: "decimal3" },
        { key: "McPercentage", label: "MC %", width: 100, align: "right", format: "currency" }
      ];

      const query = `
        SELECT 
          RTRIM(smc.SupplierCode) AS SupplierCode,
          ISNULL(RTRIM(s.SupplierName), RTRIM(smc.SupplierCode)) AS SupplierName,
          RTRIM(smc.CatalogueCode) AS CatalogueCode,
          RTRIM(smc.Type) AS Type,
          ROUND(ISNULL(smc.FromWeight, 0), 3) AS FromWeight,
          ROUND(ISNULL(smc.ToWeight, 0), 3) AS ToWeight,
          ROUND(ISNULL(smc.McPercentage, 0), 2) AS McPercentage
        FROM tblSupplierMCPerc smc WITH (NOLOCK)
        LEFT JOIN tblSupplier s WITH (NOLOCK) ON smc.SupplierCode = s.SupplierCode
        ${whereClause}
        ORDER BY smc.SupplierCode, smc.CatalogueCode
      `;
      const resMC = await request.query(query);
      rows = resMC.recordset;
    }

    // ==========================================
    // 8. PURCHASE TALLY REPORT
    // ==========================================
    else if (reportId === "purchase_tally") {
      title = `Purchase Tally Report From ${fromDateDisp} To ${toDateDisp}`;

      let whereClause = ` WHERE pt.ModifyDate >= @FromDate AND pt.ModifyDate < @ToDateExclusive`;

      if (supplierCode && supplierCode !== "ALL" && supplierCode.trim() !== "") {
        request.input("SupplierCode", sql.VarChar, supplierCode.trim());
        whereClause += ` AND RTRIM(pt.SupplierCode) = @SupplierCode`;
      }

      if (invoiceNo && invoiceNo.trim() !== "") {
        request.input("InvoiceNo", sql.VarChar, invoiceNo.trim());
        whereClause += ` AND RTRIM(pt.InvoiceNo) = @InvoiceNo`;
      }

      if (jobSheetNo && jobSheetNo.trim() !== "") {
        request.input("JobSheetNo", sql.VarChar, jobSheetNo.trim());
        whereClause += ` AND RTRIM(pt.JobSheetNo) = @JobSheetNo`;
      }

      columns = [
        { key: "SupplierCode", label: "Supplier", width: 110 },
        { key: "JobSheetNo", label: "Job Sheet No", width: 130 },
        { key: "InvoiceNo", label: "Invoice No", width: 130 },
        { key: "InvoiceDate", label: "Invoice Date", width: 120, format: "date" },
        { key: "PurchaseWeight", label: "Pur Wt (g)", width: 120, align: "right", format: "decimal3" },
        { key: "BillWeight", label: "Bill Wt (g)", width: 120, align: "right", format: "decimal3" },
        { key: "PurchaseAmount", label: "Pur Amount", width: 120, align: "right", format: "currency" },
        { key: "BillAmount", label: "Bill Amount", width: 120, align: "right", format: "currency" },
        { key: "PurchaseMC", label: "Pur MC", width: 110, align: "right", format: "currency" },
        { key: "BillMC", label: "Bill MC", width: 110, align: "right", format: "currency" }
      ];

      const query = `
        SELECT 
          RTRIM(pt.SupplierCode) AS SupplierCode,
          RTRIM(pt.JobSheetNo) AS JobSheetNo,
          RTRIM(pt.InvoiceNo) AS InvoiceNo,
          pt.InvoiceDate,
          ROUND(ISNULL(pt.PurchaseWeight, 0), 3) AS PurchaseWeight,
          ROUND(ISNULL(pt.BillWeight, 0), 3) AS BillWeight,
          ROUND(ISNULL(pt.PurchaseAmount, 0), 2) AS PurchaseAmount,
          ROUND(ISNULL(pt.BillAmount, 0), 2) AS BillAmount,
          ROUND(ISNULL(pt.PurchaseMC, 0), 2) AS PurchaseMC,
          ROUND(ISNULL(pt.BillMC, 0), 2) AS BillMC
        FROM tblPurchaseTally pt WITH (NOLOCK)
        ${whereClause}
        ORDER BY pt.InvoiceDate DESC, pt.JobSheetNo DESC
      `;
      const resPT = await request.query(query);
      rows = resPT.recordset;
    }

    // ==========================================
    // 9. SUPPLIER ANALYSIS REPORT
    // ==========================================
    else if (reportId === "supplier_analysis") {
      title = `Supplier Analysis Report From ${fromDateDisp} To ${toDateDisp}`;

      let whereClause = ` WHERE j.PurchaseBillDate >= @FromDate AND j.PurchaseBillDate < @ToDateExclusive`;

      if (supplierCode && supplierCode !== "ALL" && supplierCode.trim() !== "") {
        request.input("SupplierCode", sql.VarChar, supplierCode.trim());
        whereClause += ` AND RTRIM(j.SupplierCode) = @SupplierCode`;
      }

      columns = [
        { key: "SupplierCode", label: "Supplier Code", width: 130 },
        { key: "SupplierName", label: "Supplier Name", width: 240 },
        { key: "TotalJobSheets", label: "Total Job Sheets", width: 130, align: "right" },
        { key: "TotalPCS", label: "Total PCS", width: 110, align: "right" },
        { key: "TotalWeight", label: "Total Wt 24K (g)", width: 140, align: "right", format: "decimal3" },
        { key: "TotalMC", label: "Total MC", width: 130, align: "right", format: "currency" },
        { key: "TotalCost", label: "Total Cost", width: 140, align: "right", format: "currency" }
      ];

      const query = `
        SELECT 
          RTRIM(j.SupplierCode) AS SupplierCode,
          ISNULL(RTRIM(s.SupplierName), RTRIM(j.SupplierCode)) AS SupplierName,
          COUNT(j.JobSheetNo) AS TotalJobSheets,
          SUM(ISNULL(j.TotalPCS, 0)) AS TotalPCS,
          ROUND(SUM(ISNULL(j.TotalWeight24K, 0)), 3) AS TotalWeight,
          ROUND(SUM(ISNULL(j.TotalMC, 0)), 2) AS TotalMC,
          ROUND(SUM(ISNULL(j.TotalCost, 0)), 2) AS TotalCost
        FROM tblJobSheetHeader j WITH (NOLOCK)
        LEFT JOIN tblSupplier s WITH (NOLOCK) ON j.SupplierCode = s.SupplierCode
        ${whereClause}
        GROUP BY j.SupplierCode, s.SupplierName
        ORDER BY TotalCost DESC
      `;
      const resSA = await request.query(query);
      rows = resSA.recordset;
    }

    // ==========================================
    // 10. SUPPLIER PAYMENT REPORT
    // ==========================================
    else if (reportId === "supplier_payment") {
      title = `Supplier Payment Report From ${fromDateDisp} To ${toDateDisp}`;

      let whereClause = ` WHERE pd.PaymentDate >= @FromDate AND pd.PaymentDate < @ToDateExclusive`;

      if (supplierCode && supplierCode !== "ALL" && supplierCode.trim() !== "") {
        request.input("SupplierCode", sql.VarChar, supplierCode.trim());
        whereClause += ` AND RTRIM(pd.VendorCode) = @SupplierCode`;
      }

      if (invoiceNo && invoiceNo.trim() !== "") {
        request.input("InvoiceNo", sql.VarChar, invoiceNo.trim());
        whereClause += ` AND RTRIM(pd.Invoiceno) = @InvoiceNo`;
      }

      columns = [
        { key: "PaymentNo", label: "Payment No", width: 130 },
        { key: "PaymentDate", label: "Payment Date", width: 120, format: "date" },
        { key: "SupplierCode", label: "Supplier Code", width: 120 },
        { key: "SupplierName", label: "Supplier Name", width: 200 },
        { key: "InvoiceNo", label: "Invoice No", width: 130 },
        { key: "InvoiceDate", label: "Invoice Date", width: 120, format: "date" },
        { key: "Paymode", label: "Pay Mode", width: 110 },
        { key: "PaymentAmount", label: "Amount Paid", width: 130, align: "right", format: "currency" },
        { key: "Remarks", label: "Remarks", width: 200 }
      ];

      const query = `
        SELECT 
          RTRIM(pd.PaymentNo) AS PaymentNo,
          pd.PaymentDate,
          RTRIM(pd.VendorCode) AS SupplierCode,
          ISNULL(RTRIM(s.SupplierName), RTRIM(pd.VendorCode)) AS SupplierName,
          RTRIM(pd.Invoiceno) AS InvoiceNo,
          pd.InvoiceDate,
          RTRIM(pd.Paymode) AS Paymode,
          ROUND(ISNULL(pd.PaymentAmount, 0), 2) AS PaymentAmount,
          ISNULL(RTRIM(pd.Remarks), '') AS Remarks
        FROM tblJobSheetPaymentDetail pd WITH (NOLOCK)
        LEFT JOIN tblSupplier s WITH (NOLOCK) ON pd.VendorCode = s.SupplierCode
        ${whereClause}
        ORDER BY pd.PaymentDate DESC, pd.PaymentNo DESC
      `;
      const resSP = await request.query(query);
      rows = resSP.recordset;
    }

    // ==========================================
    // 11. PURCHASE DETAILS REPORT
    // ==========================================
    else if (reportId === "purchase_details") {
      title = `Purchase Details Report From ${fromDateDisp} To ${toDateDisp}`;

      let whereClause = ` WHERE pld.CreateDate >= @FromDate AND pld.CreateDate < @ToDateExclusive`;

      if (supplierCode && supplierCode !== "ALL" && supplierCode.trim() !== "") {
        request.input("SupplierCode", sql.VarChar, supplierCode.trim());
        whereClause += ` AND RTRIM(pld.SupplierCode) = @SupplierCode`;
      }

      if (invoiceNo && invoiceNo.trim() !== "") {
        request.input("InvoiceNo", sql.VarChar, invoiceNo.trim());
        whereClause += ` AND RTRIM(pld.InvoiceNo) = @InvoiceNo`;
      }

      if (jobSheetNo && jobSheetNo.trim() !== "") {
        request.input("JobSheetNo", sql.VarChar, jobSheetNo.trim());
        whereClause += ` AND RTRIM(pld.JobsheetNo) = @JobSheetNo`;
      }

      columns = [
        { key: "JobSheetNo", label: "Job Sheet No", width: 130 },
        { key: "InvoiceNo", label: "Invoice No", width: 130 },
        { key: "SupplierCode", label: "Supplier Code", width: 120 },
        { key: "SupplierName", label: "Supplier Name", width: 220 },
        { key: "TotalBeforeGST", label: "Total Excl GST", width: 140, align: "right", format: "currency" },
        { key: "GST", label: "GST", width: 110, align: "right", format: "currency" },
        { key: "TotalSales", label: "Total Amount", width: 140, align: "right", format: "currency" },
        { key: "CreateDate", label: "Entry Date", width: 120, format: "date" }
      ];

      const query = `
        SELECT 
          RTRIM(pld.JobsheetNo) AS JobSheetNo,
          RTRIM(pld.InvoiceNo) AS InvoiceNo,
          RTRIM(pld.SupplierCode) AS SupplierCode,
          ISNULL(RTRIM(s.SupplierName), RTRIM(pld.SupplierCode)) AS SupplierName,
          ROUND(ISNULL(pld.TotalBeforeGST, 0), 2) AS TotalBeforeGST,
          ROUND(ISNULL(pld.GST, 0), 2) AS GST,
          ROUND(ISNULL(pld.TotalSales, 0), 2) AS TotalSales,
          pld.CreateDate
        FROM tblPurchaseDetails pld WITH (NOLOCK)
        LEFT JOIN tblSupplier s WITH (NOLOCK) ON pld.SupplierCode = s.SupplierCode
        ${whereClause}
        ORDER BY pld.CreateDate DESC
      `;
      const resPD = await request.query(query);
      rows = resPD.recordset;
    }

    // ==========================================
    // 12. TRADE IN GOLD ISSUED REPORT
    // ==========================================
    else if (reportId === "trade_in_gold_issued" || reportId === "trade_in_journal") {
      title = `Trade In History Report From ${fromDateDisp} To ${toDateDisp}`;

      let whereClause = ` WHERE th.CreateDate >= @FromDate AND th.CreateDate < @ToDateExclusive`;

      if (supplierCode && supplierCode !== "ALL" && supplierCode.trim() !== "") {
        request.input("SupplierCode", sql.VarChar, supplierCode.trim());
        whereClause += ` AND RTRIM(th.SupplierCode) = @SupplierCode`;
      }

      columns = [
        { key: "TradeIssueNo", label: "Issue No", width: 130 },
        { key: "SupplierCode", label: "Supplier", width: 110 },
        { key: "InventoryCode", label: "Inventory Code", width: 140 },
        { key: "TotalWeight", label: "Total Wt (g)", width: 120, align: "right", format: "decimal3" },
        { key: "IssuedWeight", label: "Issued Wt (g)", width: 120, align: "right", format: "decimal3" },
        { key: "BalanceWeight", label: "Bal Wt (g)", width: 120, align: "right", format: "decimal3" },
        { key: "CostPerGram", label: "Cost / g", width: 100, align: "right", format: "currency" },
        { key: "TotalCost", label: "Total Cost", width: 130, align: "right", format: "currency" },
        { key: "CreateDate", label: "Date", width: 120, format: "date" }
      ];

      const query = `
        SELECT 
          RTRIM(th.TradeIssueNo) AS TradeIssueNo,
          RTRIM(th.SupplierCode) AS SupplierCode,
          RTRIM(th.InventoryCode) AS InventoryCode,
          ROUND(ISNULL(th.TotalWeight, 0), 3) AS TotalWeight,
          ROUND(ISNULL(th.IssuedWeight, 0), 3) AS IssuedWeight,
          ROUND(ISNULL(th.BalanceWeight, 0), 3) AS BalanceWeight,
          ROUND(ISNULL(th.CostPerGram, 0), 2) AS CostPerGram,
          ROUND(ISNULL(th.TotalCost, 0), 2) AS TotalCost,
          th.CreateDate
        FROM tblTradeIssueHistory th WITH (NOLOCK)
        ${whereClause}
        ORDER BY th.CreateDate DESC
      `;
      const resTH = await request.query(query);
      rows = resTH.recordset;
    }

    // ==========================================
    // 13. VENDOR WEIGHT BALANCE REPORT (rptSupplierbalance.rpt)
    // ==========================================
    else if (reportId === "vendor_weight_balance") {
      title = `Vendor Weight Balance Report`;

      let whereClause = ` WHERE 1=1`;

      if (supplierCode && supplierCode !== "ALL" && supplierCode.trim() !== "") {
        request.input("SupplierCode", sql.VarChar, supplierCode.trim());
        whereClause += ` AND RTRIM(h.SupplierCode) = @SupplierCode`;
      }

      columns = [
        { key: "SupplierCode", label: "Vendor Code", width: 120 },
        { key: "SupplierName", label: "Vendor Name", width: 220 },
        { key: "TotalPurchasedWeight", label: "Purchased Wt (g)", width: 140, align: "right", format: "decimal3" },
        { key: "TotalPaidWeight", label: "Paid Wt (g)", width: 130, align: "right", format: "decimal3" },
        { key: "BalanceWeight", label: "Balance Wt (g)", width: 140, align: "right", format: "decimal3" },
        { key: "LastTransactionDate", label: "Last Date", width: 120, format: "date" }
      ];

      const query = `
        SELECT 
          RTRIM(h.SupplierCode) AS SupplierCode,
          ISNULL(RTRIM(s.SupplierName), RTRIM(h.SupplierCode)) AS SupplierName,
          ROUND(SUM(ISNULL(h.TotalWeight24K, 0)), 3) AS TotalPurchasedWeight,
          ROUND(SUM(ISNULL(h.ManualWeight, 0)), 3) AS TotalPaidWeight,
          ROUND(SUM(ISNULL(h.TotalWeight24K, 0) - ISNULL(h.ManualWeight, 0)), 3) AS BalanceWeight,
          MAX(h.JobSheetDate) AS LastTransactionDate
        FROM tblJobSheetHeader h WITH (NOLOCK)
        LEFT JOIN tblSupplier s WITH (NOLOCK) ON RTRIM(h.SupplierCode) = RTRIM(s.SupplierCode)
        ${whereClause}
        GROUP BY h.SupplierCode, s.SupplierName
        ORDER BY h.SupplierCode ASC
      `;
      const resVWB = await request.query(query);
      rows = resVWB.recordset;
    }

    // ==========================================
    // 14. VENDOR MC BALANCE REPORT (rptSupplierpayment.rpt)
    // ==========================================
    else if (reportId === "vendor_mc_balance") {
      title = `Vendor MC Balance Report`;

      let whereClause = ` WHERE 1=1`;

      if (supplierCode && supplierCode !== "ALL" && supplierCode.trim() !== "") {
        request.input("SupplierCode", sql.VarChar, supplierCode.trim());
        whereClause += ` AND RTRIM(h.SupplierCode) = @SupplierCode`;
      }

      columns = [
        { key: "SupplierCode", label: "Vendor Code", width: 120 },
        { key: "SupplierName", label: "Vendor Name", width: 220 },
        { key: "TotalMC", label: "Total MC", width: 130, align: "right", format: "currency" },
        { key: "PaidMC", label: "Paid MC", width: 130, align: "right", format: "currency" },
        { key: "BalanceMC", label: "Balance MC", width: 130, align: "right", format: "currency" },
        { key: "LastTransactionDate", label: "Last Date", width: 120, format: "date" }
      ];

      const query = `
        SELECT 
          RTRIM(h.SupplierCode) AS SupplierCode,
          ISNULL(RTRIM(s.SupplierName), RTRIM(h.SupplierCode)) AS SupplierName,
          ROUND(SUM(ISNULL(h.TotalMC, 0)), 2) AS TotalMC,
          ROUND(SUM(ISNULL(h.ManualMC, 0)), 2) AS PaidMC,
          ROUND(SUM(ISNULL(h.TotalMC, 0) - ISNULL(h.ManualMC, 0)), 2) AS BalanceMC,
          MAX(h.JobSheetDate) AS LastTransactionDate
        FROM tblJobSheetHeader h WITH (NOLOCK)
        LEFT JOIN tblSupplier s WITH (NOLOCK) ON RTRIM(h.SupplierCode) = RTRIM(s.SupplierCode)
        ${whereClause}
        GROUP BY h.SupplierCode, s.SupplierName
        ORDER BY h.SupplierCode ASC
      `;
      const resVMC = await request.query(query);
      rows = resVMC.recordset;
    }

    // Compute dynamic totals across retrieved rows
    const totals = {
      totalRecords: rows.length,
      totalPcs: 0,
      totalWeight: 0,
      totalCost: 0,
      totalAmount: 0,
      totalMC: 0
    };

    rows.forEach((r) => {
      if (r.TotalPCS !== undefined) totals.totalPcs += Number(r.TotalPCS) || 0;
      else if (r.PCS !== undefined) totals.totalPcs += Number(r.PCS) || 0;
      else if (r.TotalPcs !== undefined) totals.totalPcs += Number(r.TotalPcs) || 0;

      if (r.TotalWeight24K !== undefined) totals.totalWeight += Number(r.TotalWeight24K) || 0;
      else if (r.TotalWeight !== undefined) totals.totalWeight += Number(r.TotalWeight) || 0;
      else if (r.Weight !== undefined) totals.totalWeight += Number(r.Weight) || 0;
      else if (r.PurchaseWeight !== undefined) totals.totalWeight += Number(r.PurchaseWeight) || 0;

      if (r.TotalCost !== undefined) totals.totalCost += Number(r.TotalCost) || 0;
      if (r.TotalMC !== undefined) totals.totalMC += Number(r.TotalMC) || 0;
      else if (r.MC !== undefined) totals.totalMC += Number(r.MC) || 0;

      if (r.TotalSales !== undefined) totals.totalAmount += Number(r.TotalSales) || 0;
      else if (r.NetAmount !== undefined) totals.totalAmount += Number(r.NetAmount) || 0;
      else if (r.TotalValue !== undefined) totals.totalAmount += Number(r.TotalValue) || 0;
      else if (r.PaymentAmount !== undefined) totals.totalAmount += Number(r.PaymentAmount) || 0;
      else if (r.PurchaseAmount !== undefined) totals.totalAmount += Number(r.PurchaseAmount) || 0;
    });

    totals.totalWeight = Number(totals.totalWeight.toFixed(3));
    totals.totalCost = Number(totals.totalCost.toFixed(2));
    totals.totalMC = Number(totals.totalMC.toFixed(2));
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
    console.error("Purchase Reports Generation Error:", err);
    res.status(500).json({ success: false, message: err.message });
  }
});

      // POST /api/purchasereports/download-html
router.post("/download-html", async (req, res) => {
  try {
    const { generateReportHtml } = require("../utils/reportHtmlTemplate");
    const { title, columns, data, totals, companyInfo, fromDate, toDate, showPrintButton } = req.body;
    const html = await generateReportHtml({
      title: title || "Purchase Report",
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
    console.error("Purchase Reports Download HTML Error:", err);
    res.status(500).send("Error generating report HTML: " + err.message);
  }
});

// EXPORT EXCEL & PDF ENDPOINTS
router.post("/export-excel", (req, res) => {
  const { handleExportExcel } = require("../utils/reportExportHandlers");
  handleExportExcel({ req, res, defaultTitle: "Purchase_Report" });
});

router.post("/export-pdf", (req, res) => {
  const { handleExportPdf } = require("../utils/reportExportHandlers");
  handleExportPdf({ req, res, defaultTitle: "Purchase_Report" });
});

module.exports = router;

