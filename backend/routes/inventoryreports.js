const express = require("express");
const router = express.Router();
const path = require("path");
const fs = require("fs");
const { sql, poolPromise } = require("../db");
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

// GET /api/inventoryreports/initial-dates
router.get("/initial-dates", async (req, res) => {
  try {
    const pool = req.pool;
    // Get server date and max purchase/create date from inventory
    const result = await pool.request().query(`
      SELECT 
        CONVERT(VARCHAR(10), GETDATE(), 120) AS CurrentDate,
        CONVERT(VARCHAR(10), ISNULL(MAX(PurchaseDate), GETDATE()), 120) AS MaxPurchaseDate,
        CONVERT(VARCHAR(10), ISNULL(MIN(PurchaseDate), '2025-01-01'), 120) AS MinPurchaseDate
      FROM tblInventory WITH (NOLOCK)
    `);

    const row = result.recordset[0] || {};
    const todayStr = row.CurrentDate || new Date().toISOString().split("T")[0];
    const workingDate = row.MaxPurchaseDate || todayStr;

    res.json({
      success: true,
      data: {
        fromDate: workingDate,
        toDate: workingDate,
        currentDate: todayStr,
        minPurchaseDate: row.MinPurchaseDate || "2025-01-01",
        maxPurchaseDate: workingDate
      }
    });
  } catch (err) {
    console.error("Error fetching initial dates:", err);
    res.status(500).json({ success: false, message: err.message });
  }
});

// GET /api/inventoryreports/locations
router.get("/locations", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query(`
      SELECT 'ALL' AS LocationCode, 'ALL - All Locations' AS LocationName, 0 AS SortCode
      UNION ALL
      SELECT RTRIM(LocationCode) AS LocationCode, RTRIM(LocationName) AS LocationName, ISNULL(SortCode, 999) AS SortCode
      FROM tblLocation WITH (NOLOCK)
      ORDER BY SortCode, LocationCode
    `);

    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Error fetching inventory locations:", err);
    res.status(500).json({ success: false, message: err.message });
  }
});

// GET /api/inventoryreports/suppliers
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
    console.error("Error fetching suppliers:", err);
    res.status(500).json({ success: false, message: err.message });
  }
});

// GET /api/inventoryreports/classes
router.get("/classes", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query(`
      SELECT RTRIM(ClassCode) AS ClassCode, RTRIM(ClassName) AS ClassName
      FROM tblClass WITH (NOLOCK)
      ORDER BY ClassCode
    `);
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Error fetching classes:", err);
    res.status(500).json({ success: false, message: err.message });
  }
});

// GET /api/inventoryreports/categories
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
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Error fetching categories:", err);
    res.status(500).json({ success: false, message: err.message });
  }
});

// GET /api/inventoryreports/catalogues
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
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Error fetching catalogues:", err);
    res.status(500).json({ success: false, message: err.message });
  }
});

// GET /api/inventoryreports/sizes
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
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Error fetching sizes:", err);
    res.status(500).json({ success: false, message: err.message });
  }
});

// GET /api/inventoryreports/class-types
router.get("/class-types", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query(`
      SELECT DISTINCT RTRIM(ClassType) AS ClassType 
      FROM tblJobSheetHeader WITH (NOLOCK) 
      WHERE ClassType IS NOT NULL AND RTRIM(ClassType) <> ''
      UNION
      SELECT DISTINCT RTRIM(ClassCode) AS ClassType 
      FROM tblClass WITH (NOLOCK)
      ORDER BY ClassType
    `);
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Error fetching class types:", err);
    res.status(500).json({ success: false, message: err.message });
  }
});

// ==========================================
// 2. REPORT GENERATION ENGINE
// ==========================================

const SUPPORTED_REPORTS = [
  "stock_by_category",
  "stock_by_catalogue",
  "stock_by_category_datewise",
  "stock_by_catalogue_datewise",
  "movement_by_size",
  "stock_in_excel",
  "stock_in_excel_datewise",
  "diamond_stock",
  "deleted_inventory",
  "online_stock",
  "reorder_level",
  "accessories_stock",
  "scrap_stock",
  "sold_inventory",
  "purchase_return",
  "split_inventory",
  "merged_inventory",
  "inventory_maintenance"
];

router.post("/generate", async (req, res) => {
  try {
    const pool = req.pool;
    const {
      reportId = "stock_by_category",
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
      locationCode,
      supplierCode,
      classCode,
      categoryCode,
      catalogueCode,
      sizeCode,
      classType,
      withDate = false,
      fromDate,
      toDate,
      includeImage = false
    } = filters;

    if (fromDate || toDate) {
      const dateVal = validateBackendDates(fromDate, toDate);
      if (!dateVal.isValid) {
        return res.status(400).json({ success: false, message: dateVal.message });
      }
    }

    // Date validation
    let fDate = null;
    let tDateEx = null;
    let fromDateDisp = "";
    let toDateDisp = "";

    const isDateWiseReport = [
      "stock_by_category_datewise",
      "stock_by_catalogue_datewise",
      "stock_in_excel_datewise",
      "movement_by_size",
      "purchase_return"
    ].includes(reportId);
    // For general inventory on-hand stock summary reports, date is informational (as seen in Crystal Reports) unless explicitly date-wise
    const isInventoryStockSummary = (reportId === "stock_by_catalogue" || reportId === "stock_by_category") && (reportType || "Summary").toLowerCase() === "summary";
    const applyDateFilter = !isInventoryStockSummary && (withDate || isDateWiseReport);

    if (applyDateFilter) {
      if (!fromDate) {
        return res.status(400).json({
          success: false,
          message: "From Date is required when date filtering is enabled."
        });
      }
      const range = getInclusiveRange(fromDate, toDate);
      if (range.fromDate > range.toDate) {
        return res.status(400).json({
          success: false,
          message: "From Date must be less than or equal to To Date."
        });
      }
      fDate = range.fromDate;
      tDateEx = range.toDateExclusive;
      fromDateDisp = formatDateDisp(fDate);
      toDateDisp = formatDateDisp(range.toDate);
    }

    const request = pool.request();
    const isSummary = (reportType || "Summary").toLowerCase() === "summary";

    // Reusable filter clause builder for tblInventory queries
    const buildInventoryFilters = (invAlias = "a", stockAlias = "b") => {
      let clause = "";

      if (locationCode && locationCode !== "ALL") {
        request.input("LocationCode", sql.VarChar, locationCode.trim());
        clause += ` AND ${invAlias}.LocationCode = @LocationCode`;
      }

      if (supplierCode && supplierCode !== "ALL") {
        request.input("SupplierCode", sql.VarChar, supplierCode.trim());
        clause += ` AND ${invAlias}.SupplierCode = @SupplierCode`;
      }

      if (classCode && classCode !== "ALL" && classCode.trim() !== "") {
        request.input("ClassCode", sql.VarChar, classCode.trim());
        clause += ` AND RTRIM(${invAlias}.ClassCode) = @ClassCode`;
      }

      if (categoryCode && categoryCode !== "ALL" && categoryCode.trim() !== "") {
        request.input("CategoryCode", sql.VarChar, categoryCode.trim());
        clause += ` AND RTRIM(${invAlias}.CategoryCode) = @CategoryCode`;
      }

      if (catalogueCode && catalogueCode !== "ALL" && catalogueCode.trim() !== "") {
        request.input("CatalogueCode", sql.VarChar, catalogueCode.trim());
        clause += ` AND RTRIM(${invAlias}.CatalogueCode) = @CatalogueCode`;
      }

      if (sizeCode && sizeCode !== "ALL" && sizeCode.trim() !== "") {
        request.input("SizeCode", sql.VarChar, sizeCode.trim());
        clause += ` AND (RTRIM(CAST(${invAlias}.SizeCode AS VARCHAR(20))) = @SizeCode OR RTRIM(CAST(${invAlias}.Size AS VARCHAR(20))) = @SizeCode)`;
      }

      if (classType && classType !== "ALL" && classType.trim() !== "") {
        request.input("ClassType", sql.VarChar, classType.trim());
        clause += ` AND RTRIM(${invAlias}.ClassCode) = @ClassType`;
      }

      if (applyDateFilter && fDate && tDateEx) {
        request.input("FromDate", sql.DateTime, fDate);
        request.input("ToDateExclusive", sql.DateTime, tDateEx);
        clause += ` AND ${invAlias}.PurchaseDate >= @FromDate AND ${invAlias}.PurchaseDate < @ToDateExclusive`;
      }

      return clause;
    };

    let title = "";
    let columns = [];
    let rows = [];

    // ==========================================
    // 1 & 13. INVENTORY STOCK BY CATEGORY / DATEWISE
    // ==========================================
    if (reportId === "stock_by_category" || reportId === "stock_by_category_datewise") {
      const dateTitlePart = applyDateFilter ? ` From ${fromDateDisp} To ${toDateDisp}` : "";
      title = `${isSummary ? "Summary" : "Detail"} Inventory Stock Report By Category${dateTitlePart}`;

      if (isSummary) {
        columns = [
          { key: "CategoryCode", label: "Category Code", width: 140 },
          { key: "CategoryName", label: "Category Name", width: 220 },
          { key: "ClassCode", label: "Class Code", width: 120 },
          { key: "TotalItems", label: "Items", width: 90, align: "right" },
          { key: "TotalPcs", label: "Total Pcs", width: 100, align: "right" },
          { key: "TotalWeight", label: "Total Wt (g)", width: 130, align: "right", format: "decimal3" },
          { key: "TotalStoneWeight", label: "Stone Wt (g)", width: 130, align: "right", format: "decimal3" },
          { key: "TotalPMC", label: "Total PMC", width: 120, align: "right", format: "currency" },
          { key: "TotalSMC", label: "Total SMC", width: 120, align: "right", format: "currency" },
          { key: "TotalCost", label: "Total Cost", width: 140, align: "right", format: "currency" }
        ];

        let query = `
          SELECT 
            RTRIM(a.CategoryCode) AS CategoryCode,
            ISNULL(RTRIM(cat.CategoryName), RTRIM(a.CategoryCode)) AS CategoryName,
            RTRIM(a.ClassCode) AS ClassCode,
            COUNT(a.InventoryCode) AS TotalItems,
            SUM(ISNULL(b.PCS, 1)) AS TotalPcs,
            ROUND(SUM(ISNULL(a.Weight, 0)), 3) AS TotalWeight,
            ROUND(SUM(ISNULL(a.StoneWeight, 0)), 3) AS TotalStoneWeight,
            ROUND(SUM(ISNULL(a.PMC, 0)), 2) AS TotalPMC,
            ROUND(SUM(ISNULL(a.SMC, 0)), 2) AS TotalSMC,
            ROUND(SUM(ISNULL(a.TotalCost, 0)), 2) AS TotalCost
          FROM tblInventory a WITH (NOLOCK)
          INNER JOIN tblInventoryStock b WITH (NOLOCK) 
            ON a.InventoryCode = b.InventoryCode AND a.LocationCode = b.LocationCode
          LEFT JOIN tblCategory cat WITH (NOLOCK) 
            ON a.CategoryCode = cat.CategoryCode
          WHERE b.QtyOnHand > 0
        `;

        query += buildInventoryFilters("a", "b");
        query += `
          GROUP BY a.CategoryCode, cat.CategoryName, a.ClassCode
          ORDER BY a.CategoryCode, a.ClassCode
        `;

        const resQ = await request.query(query);
        rows = resQ.recordset;

      } else {
        // Detail Mode
        columns = [
          { key: "InventoryCode", label: "Inventory Code", width: 150 },
          { key: "Description", label: "Description", width: 220 },
          { key: "CategoryCode", label: "Category", width: 120 },
          { key: "CatalogueCode", label: "Catalogue", width: 120 },
          { key: "ClassCode", label: "Class", width: 100 },
          { key: "PCS", label: "Pcs", width: 80, align: "right" },
          { key: "Weight", label: "Weight (g)", width: 110, align: "right", format: "decimal3" },
          { key: "StoneWeight", label: "Stone Wt", width: 100, align: "right", format: "decimal3" },
          { key: "CostPerGram", label: "Cost/g", width: 100, align: "right", format: "currency" },
          { key: "PMC", label: "PMC", width: 90, align: "right", format: "currency" },
          { key: "SMC", label: "SMC", width: 90, align: "right", format: "currency" },
          { key: "TotalCost", label: "Total Cost", width: 120, align: "right", format: "currency" },
          { key: "PurchaseDate", label: "Purchase Date", width: 120, format: "date" },
          { key: "PurchaseBillNo", label: "Bill No", width: 130 },
          { key: "SupplierCode", label: "Supplier", width: 100 },
          { key: "LocationCode", label: "Location", width: 90 }
        ];

        let query = `
          SELECT 
            RTRIM(a.InventoryCode) AS InventoryCode,
            RTRIM(a.Description) AS Description,
            RTRIM(a.CategoryCode) AS CategoryCode,
            RTRIM(a.CatalogueCode) AS CatalogueCode,
            RTRIM(a.ClassCode) AS ClassCode,
            ISNULL(b.PCS, 1) AS PCS,
            ROUND(ISNULL(a.Weight, 0), 3) AS Weight,
            ROUND(ISNULL(a.StoneWeight, 0), 3) AS StoneWeight,
            ROUND(ISNULL(a.CostPerGram, 0), 2) AS CostPerGram,
            ROUND(ISNULL(a.PMC, 0), 2) AS PMC,
            ROUND(ISNULL(a.SMC, 0), 2) AS SMC,
            ROUND(ISNULL(a.GoldLoss, 0), 2) AS GoldLoss,
            ROUND(ISNULL(a.TotalCost, 0), 2) AS TotalCost,
            a.PurchaseDate,
            RTRIM(a.PurchaseBillNo) AS PurchaseBillNo,
            RTRIM(a.SupplierCode) AS SupplierCode,
            RTRIM(a.LocationCode) AS LocationCode,
            RTRIM(a.TrayId) AS TrayId
          FROM tblInventory a WITH (NOLOCK)
          INNER JOIN tblInventoryStock b WITH (NOLOCK) 
            ON a.InventoryCode = b.InventoryCode AND a.LocationCode = b.LocationCode
          WHERE b.QtyOnHand > 0
        `;

        query += buildInventoryFilters("a", "b");
        query += ` ORDER BY a.CategoryCode, a.InventoryCode`;

        const resQ = await request.query(query);
        rows = resQ.recordset;
      }

    // ==========================================
        // ==========================================
    // 2 & 2B. INVENTORY STOCK BY CATALOGUE / DATEWISE
    // ==========================================
    } else if (reportId === "stock_by_catalogue" || reportId === "stock_by_catalogue_datewise") {
      const dateTitlePart = applyDateFilter ? ` From ${fromDateDisp} To ${toDateDisp}` : "";
      title = `${isSummary ? "Summary" : "Detail"} Inventory Stock Report By Catalogue${dateTitlePart}`;

      if (isSummary) {
        columns = [
          { key: "CatalogueCode", label: "Catalogue Code", width: 180 },
          { key: "TotalWeight", label: "Weight", width: 140, align: "right", format: "decimal2" },
          { key: "TotalSMC", label: "MC", width: 140, align: "right", format: "currency" },
          { key: "TotalPcs", label: "Qty", width: 120, align: "right" },
          { key: "TotalCost", label: "TotalCost", width: 160, align: "right", format: "currency" }
        ];

        let query = `
          SELECT 
            RTRIM(a.CatalogueCode) AS CatalogueCode,
            ISNULL(RTRIM(c.CatalogueName), RTRIM(a.CatalogueCode)) AS CatalogueName,
            COUNT(a.InventoryCode) AS TotalItems,
            SUM(ISNULL(b.PCS, 1)) AS TotalPcs,
            ROUND(SUM(ISNULL(a.Weight, 0)), 2) AS TotalWeight,
            ROUND(SUM(ISNULL(a.PMC, ISNULL(a.SMC, 0))), 2) AS TotalSMC,
            ROUND(SUM(ISNULL(a.TotalCost, 0)), 2) AS TotalCost
          FROM tblInventory a WITH (NOLOCK)
          INNER JOIN tblInventoryStock b WITH (NOLOCK) 
            ON a.InventoryCode = b.InventoryCode AND a.LocationCode = b.LocationCode
          LEFT JOIN tblCatalogue c WITH (NOLOCK) 
            ON a.CatalogueCode = c.CatalogueCode
          WHERE b.QtyOnHand > 0
        `;

        query += buildInventoryFilters("a", "b");
        query += `
          GROUP BY a.CatalogueCode, c.CatalogueName
          ORDER BY a.CatalogueCode
        `;

        const resQ = await request.query(query);
        rows = resQ.recordset;

      } else {
        // Detail Mode
        columns = [
          { key: "CatalogueCode", label: "Catalogue Code", width: 140 },
          { key: "CatalogueName", label: "Catalogue Name", width: 180 },
          { key: "InventoryCode", label: "Inventory Code", width: 150 },
          { key: "Description", label: "Description", width: 220 },
          { key: "CategoryCode", label: "Category", width: 110 },
          { key: "ClassCode", label: "Class", width: 100 },
          { key: "PCS", label: "Pcs", width: 80, align: "right" },
          { key: "Weight", label: "Weight (g)", width: 110, align: "right", format: "decimal3" },
          { key: "CostPerGram", label: "Cost/g", width: 100, align: "right", format: "currency" },
          { key: "PMC", label: "PMC", width: 90, align: "right", format: "currency" },
          { key: "SMC", label: "SMC", width: 90, align: "right", format: "currency" },
          { key: "TotalCost", label: "Total Cost", width: 120, align: "right", format: "currency" },
          { key: "PurchaseDate", label: "Purchase Date", width: 120, format: "date" },
          { key: "SupplierCode", label: "Supplier", width: 100 },
          { key: "LocationCode", label: "Location", width: 90 }
        ];

        if (includeImage) {
          columns.push({ key: "InventoryImageName", label: "Image Name", width: 150 });
        }

        let query = `
          SELECT 
            RTRIM(a.CatalogueCode) AS CatalogueCode,
            ISNULL(RTRIM(c.CatalogueName), RTRIM(a.CatalogueCode)) AS CatalogueName,
            RTRIM(a.InventoryCode) AS InventoryCode,
            RTRIM(a.Description) AS Description,
            RTRIM(a.CategoryCode) AS CategoryCode,
            RTRIM(a.ClassCode) AS ClassCode,
            ISNULL(b.PCS, 1) AS PCS,
            ROUND(ISNULL(a.Weight, 0), 3) AS Weight,
            ROUND(ISNULL(a.StoneWeight, 0), 3) AS StoneWeight,
            ROUND(ISNULL(a.CostPerGram, 0), 2) AS CostPerGram,
            ROUND(ISNULL(a.PMC, 0), 2) AS PMC,
            ROUND(ISNULL(a.SMC, 0), 2) AS SMC,
            ROUND(ISNULL(a.TotalCost, 0), 2) AS TotalCost,
            a.PurchaseDate,
            RTRIM(a.PurchaseBillNo) AS PurchaseBillNo,
            RTRIM(a.SupplierCode) AS SupplierCode,
            RTRIM(a.LocationCode) AS LocationCode,
            RTRIM(a.InventoryImageName) AS InventoryImageName
          FROM tblInventory a WITH (NOLOCK)
          INNER JOIN tblInventoryStock b WITH (NOLOCK) 
            ON a.InventoryCode = b.InventoryCode AND a.LocationCode = b.LocationCode
          LEFT JOIN tblCatalogue c WITH (NOLOCK) 
            ON a.CatalogueCode = c.CatalogueCode
          WHERE b.QtyOnHand > 0
        `;

        query += buildInventoryFilters("a", "b");
        query += ` ORDER BY a.CatalogueCode, a.InventoryCode`;

        const resQ = await request.query(query);
        rows = resQ.recordset;
      }

    // ==========================================
    // 2C. MOVEMENT BY SIZE (rptInvStockBySizeSummary / rptInvStockBySizeDetail)
    // ==========================================
    } else if (reportId === "movement_by_size") {
      const dateTitlePart = applyDateFilter ? ` From ${fromDateDisp} To ${toDateDisp}` : "";
      title = `${isSummary ? "Summary" : "Detail"} Inventory Movement By Size Report${dateTitlePart}`;

      if (isSummary) {
        columns = [
          { key: "SizeCode", label: "Size Code", width: 140 },
          { key: "ClassCode", label: "Class Code", width: 120 },
          { key: "CategoryCode", label: "Category", width: 130 },
          { key: "TotalItems", label: "Items", width: 90, align: "right" },
          { key: "TotalPcs", label: "Total Pcs", width: 100, align: "right" },
          { key: "TotalWeight", label: "Total Wt (g)", width: 130, align: "right", format: "decimal3" },
          { key: "TotalCost", label: "Total Cost", width: 140, align: "right", format: "currency" }
        ];

        let query = `
          SELECT 
            ISNULL(NULLIF(RTRIM(a.SizeCode), ''), ISNULL(RTRIM(a.Size), 'UNKNOWN')) AS SizeCode,
            RTRIM(a.ClassCode) AS ClassCode,
            RTRIM(a.CategoryCode) AS CategoryCode,
            COUNT(a.InventoryCode) AS TotalItems,
            SUM(ISNULL(b.PCS, 1)) AS TotalPcs,
            ROUND(SUM(ISNULL(a.Weight, 0)), 3) AS TotalWeight,
            ROUND(SUM(ISNULL(a.TotalCost, 0)), 2) AS TotalCost
          FROM tblInventory a WITH (NOLOCK)
          INNER JOIN tblInventoryStock b WITH (NOLOCK) 
            ON a.InventoryCode = b.InventoryCode AND a.LocationCode = b.LocationCode
          WHERE b.QtyOnHand > 0
        `;

        query += buildInventoryFilters("a", "b");
        query += `
          GROUP BY ISNULL(NULLIF(RTRIM(a.SizeCode), ''), ISNULL(RTRIM(a.Size), 'UNKNOWN')), a.ClassCode, a.CategoryCode
          ORDER BY SizeCode, a.ClassCode
        `;

        const resQ = await request.query(query);
        rows = resQ.recordset;

      } else {
        // Detail Mode
        columns = [
          { key: "SizeCode", label: "Size", width: 100 },
          { key: "InventoryCode", label: "Inventory Code", width: 150 },
          { key: "Description", label: "Description", width: 220 },
          { key: "CategoryCode", label: "Category", width: 120 },
          { key: "CatalogueCode", label: "Catalogue", width: 120 },
          { key: "ClassCode", label: "Class", width: 100 },
          { key: "PCS", label: "Pcs", width: 80, align: "right" },
          { key: "Weight", label: "Weight (g)", width: 110, align: "right", format: "decimal3" },
          { key: "CostPerGram", label: "Cost/g", width: 100, align: "right", format: "currency" },
          { key: "TotalCost", label: "Total Cost", width: 120, align: "right", format: "currency" },
          { key: "PurchaseDate", label: "Purchase Date", width: 120, format: "date" },
          { key: "SupplierCode", label: "Supplier", width: 100 },
          { key: "LocationCode", label: "Location", width: 90 }
        ];

        let query = `
          SELECT 
            ISNULL(NULLIF(RTRIM(a.SizeCode), ''), ISNULL(RTRIM(a.Size), '')) AS SizeCode,
            RTRIM(a.InventoryCode) AS InventoryCode,
            RTRIM(a.Description) AS Description,
            RTRIM(a.CategoryCode) AS CategoryCode,
            RTRIM(a.CatalogueCode) AS CatalogueCode,
            RTRIM(a.ClassCode) AS ClassCode,
            ISNULL(b.PCS, 1) AS PCS,
            ROUND(ISNULL(a.Weight, 0), 3) AS Weight,
            ROUND(ISNULL(a.CostPerGram, 0), 2) AS CostPerGram,
            ROUND(ISNULL(a.TotalCost, 0), 2) AS TotalCost,
            a.PurchaseDate,
            RTRIM(a.SupplierCode) AS SupplierCode,
            RTRIM(a.LocationCode) AS LocationCode
          FROM tblInventory a WITH (NOLOCK)
          INNER JOIN tblInventoryStock b WITH (NOLOCK) 
            ON a.InventoryCode = b.InventoryCode AND a.LocationCode = b.LocationCode
          WHERE b.QtyOnHand > 0
        `;

        query += buildInventoryFilters("a", "b");
        query += ` ORDER BY a.SizeCode, a.InventoryCode`;

        const resQ = await request.query(query);
        rows = resQ.recordset;
      }

    // ==========================================
    // 3. DELETED INVENTORY
    // ==========================================
    } else if (reportId === "deleted_inventory") {
      const dateTitlePart = applyDateFilter ? ` From ${fromDateDisp} To ${toDateDisp}` : "";
      title = `Deleted Inventory Report${dateTitlePart}`;

      columns = [
        { key: "InventoryCode", label: "Inventory Code", width: 150 },
        { key: "Description", label: "Description", width: 220 },
        { key: "CategoryCode", label: "Category", width: 120 },
        { key: "CatalogueCode", label: "Catalogue", width: 120 },
        { key: "ClassCode", label: "Class", width: 100 },
        { key: "Weight", label: "Weight (g)", width: 110, align: "right", format: "decimal3" },
        { key: "CostPerGram", label: "Cost/g", width: 100, align: "right", format: "currency" },
        { key: "TotalCost", label: "Total Cost", width: 120, align: "right", format: "currency" },
        { key: "DeletedBy", label: "Deleted By", width: 120 },
        { key: "DeletedDate", label: "Deleted Date", width: 130, format: "date" },
        { key: "PurchaseBillNo", label: "Purchase Bill", width: 130 },
        { key: "SupplierCode", label: "Supplier", width: 100 },
        { key: "LocationCode", label: "Location", width: 90 },
        { key: "Remarks", label: "Remarks", width: 150 }
      ];

      let query = `
        SELECT 
          RTRIM(a.InventoryCode) AS InventoryCode,
          RTRIM(a.Description) AS Description,
          RTRIM(a.CategoryCode) AS CategoryCode,
          RTRIM(a.CatalogueCode) AS CatalogueCode,
          RTRIM(a.ClassCode) AS ClassCode,
          ROUND(ISNULL(a.Weight, 0), 3) AS Weight,
          ROUND(ISNULL(a.StoneWeight, 0), 3) AS StoneWeight,
          ROUND(ISNULL(a.CostPerGram, 0), 2) AS CostPerGram,
          ROUND(ISNULL(a.TotalCost, 0), 2) AS TotalCost,
          RTRIM(a.ModifyUser) AS DeletedBy,
          a.ModifyDate AS DeletedDate,
          RTRIM(a.PurchaseBillNo) AS PurchaseBillNo,
          a.PurchaseDate,
          RTRIM(a.SupplierCode) AS SupplierCode,
          RTRIM(a.LocationCode) AS LocationCode,
          RTRIM(a.Remarks) AS Remarks
        FROM tblInventoryDelete a WITH (NOLOCK)
        WHERE 1=1
      `;

      if (locationCode && locationCode !== "ALL") {
        request.input("LocationCode", sql.VarChar, locationCode.trim());
        query += ` AND a.LocationCode = @LocationCode`;
      }
      if (supplierCode && supplierCode !== "ALL") {
        request.input("SupplierCode", sql.VarChar, supplierCode.trim());
        query += ` AND a.SupplierCode = @SupplierCode`;
      }
      if (classCode && classCode !== "ALL" && classCode.trim() !== "") {
        request.input("ClassCode", sql.VarChar, classCode.trim());
        query += ` AND RTRIM(a.ClassCode) = @ClassCode`;
      }
      if (categoryCode && categoryCode !== "ALL" && categoryCode.trim() !== "") {
        request.input("CategoryCode", sql.VarChar, categoryCode.trim());
        query += ` AND RTRIM(a.CategoryCode) = @CategoryCode`;
      }
      if (catalogueCode && catalogueCode !== "ALL" && catalogueCode.trim() !== "") {
        request.input("CatalogueCode", sql.VarChar, catalogueCode.trim());
        query += ` AND RTRIM(a.CatalogueCode) = @CatalogueCode`;
      }
      if (applyDateFilter && fDate && tDateEx) {
        request.input("FromDate", sql.DateTime, fDate);
        request.input("ToDateExclusive", sql.DateTime, tDateEx);
        query += ` AND a.ModifyDate >= @FromDate AND a.ModifyDate < @ToDateExclusive`;
      }

      query += ` ORDER BY a.ModifyDate DESC, a.InventoryCode`;
      const resQ = await request.query(query);
      rows = resQ.recordset;

    // ==========================================
    // 4. MERGED INVENTORY
    // ==========================================
    } else if (reportId === "merged_inventory") {
      const dateTitlePart = applyDateFilter ? ` From ${fromDateDisp} To ${toDateDisp}` : "";
      title = `Merged Inventory Report${dateTitlePart}`;

      columns = [
        { key: "MergedDate", label: "Merged Date", width: 130, format: "date" },
        { key: "LocationCode", label: "Location", width: 90 },
        { key: "ClassCode", label: "Class", width: 90 },
        { key: "JobSheetNo", label: "JobSheet No", width: 130 },
        { key: "InventoryCode1", label: "Src Item 1", width: 140 },
        { key: "Weight1", label: "Wt 1 (g)", width: 90, align: "right", format: "decimal3" },
        { key: "TotalCost1", label: "Cost 1", width: 100, align: "right", format: "currency" },
        { key: "InventoryCode2", label: "Src Item 2", width: 140 },
        { key: "Weight2", label: "Wt 2 (g)", width: 90, align: "right", format: "decimal3" },
        { key: "TotalCost2", label: "Cost 2", width: 100, align: "right", format: "currency" },
        { key: "NewInventoryCode", label: "New Item Code", width: 150 },
        { key: "NewDescription", label: "New Description", width: 180 },
        { key: "NewWeight", label: "New Wt (g)", width: 110, align: "right", format: "decimal3" },
        { key: "NewTotalCost", label: "New Cost", width: 120, align: "right", format: "currency" },
        { key: "UserID", label: "User", width: 90 }
      ];

      let query = `
        SELECT 
          m.MergedDate,
          RTRIM(m.LocationCode) AS LocationCode,
          RTRIM(m.ClassCode) AS ClassCode,
          RTRIM(m.JobSheetNo) AS JobSheetNo,
          RTRIM(m.InventoryCode1) AS InventoryCode1,
          ROUND(ISNULL(m.Weight1, 0), 3) AS Weight1,
          ROUND(ISNULL(m.TotalCost1, 0), 2) AS TotalCost1,
          RTRIM(m.InventoryCode2) AS InventoryCode2,
          ROUND(ISNULL(m.Weight2, 0), 3) AS Weight2,
          ROUND(ISNULL(m.TotalCost2, 0), 2) AS TotalCost2,
          RTRIM(m.InventoryCodeN) AS NewInventoryCode,
          RTRIM(m.DescriptionNew) AS NewDescription,
          ROUND(ISNULL(m.WeightN, 0), 3) AS NewWeight,
          ROUND(ISNULL(m.PMCN, 0), 2) AS NewPMC,
          ROUND(ISNULL(m.SMCN, 0), 2) AS NewSMC,
          ROUND(ISNULL(m.TotalCostN, 0), 2) AS NewTotalCost,
          RTRIM(m.UserID) AS UserID
        FROM tblMergeHistory m WITH (NOLOCK)
        WHERE 1=1
      `;

      if (locationCode && locationCode !== "ALL") {
        request.input("LocationCode", sql.VarChar, locationCode.trim());
        query += ` AND m.LocationCode = @LocationCode`;
      }
      if (classCode && classCode !== "ALL" && classCode.trim() !== "") {
        request.input("ClassCode", sql.VarChar, classCode.trim());
        query += ` AND RTRIM(m.ClassCode) = @ClassCode`;
      }
      if (applyDateFilter && fDate && tDateEx) {
        request.input("FromDate", sql.DateTime, fDate);
        request.input("ToDateExclusive", sql.DateTime, tDateEx);
        query += ` AND m.MergedDate >= @FromDate AND m.MergedDate < @ToDateExclusive`;
      }

      query += ` ORDER BY m.MergedDate DESC`;
      const resQ = await request.query(query);
      rows = resQ.recordset;

    // ==========================================
    // 5. SPLIT INVENTORY
    // ==========================================
    } else if (reportId === "split_inventory") {
      const dateTitlePart = applyDateFilter ? ` From ${fromDateDisp} To ${toDateDisp}` : "";
      title = `Split Inventory Report${dateTitlePart}`;

      columns = [
        { key: "SplitDate", label: "Split Date", width: 130, format: "date" },
        { key: "LocationCode", label: "Location", width: 90 },
        { key: "OriginalCode", label: "Original Code", width: 140 },
        { key: "OriginalDescription", label: "Original Description", width: 180 },
        { key: "OriginalWeight", label: "Orig Wt (g)", width: 100, align: "right", format: "decimal3" },
        { key: "OriginalTotalCost", label: "Orig Cost", width: 110, align: "right", format: "currency" },
        { key: "Item1Code", label: "Split Item 1", width: 140 },
        { key: "Weight1", label: "Wt 1 (g)", width: 90, align: "right", format: "decimal3" },
        { key: "TotalCost1", label: "Cost 1", width: 100, align: "right", format: "currency" },
        { key: "Item2Code", label: "Split Item 2", width: 140 },
        { key: "Item2Description", label: "Item 2 Desc", width: 180 },
        { key: "Weight2", label: "Wt 2 (g)", width: 90, align: "right", format: "decimal3" },
        { key: "TotalCost2", label: "Cost 2", width: 100, align: "right", format: "currency" },
        { key: "UserID", label: "User", width: 90 }
      ];

      let query = `
        SELECT 
          s.SplitDate,
          RTRIM(s.Locationcode) AS LocationCode,
          RTRIM(s.InventoryCode) AS OriginalCode,
          RTRIM(s.Description) AS OriginalDescription,
          ROUND(ISNULL(s.OriginalWeight, 0), 3) AS OriginalWeight,
          ROUND(ISNULL(s.OriginalTotalCost, 0), 2) AS OriginalTotalCost,
          RTRIM(s.InventoryCode1) AS Item1Code,
          ROUND(ISNULL(s.Weight1, 0), 3) AS Weight1,
          ROUND(ISNULL(s.TotalCost1, 0), 2) AS TotalCost1,
          RTRIM(s.DestInventoryCode) AS Item2Code,
          RTRIM(s.DestDescription) AS Item2Description,
          ROUND(ISNULL(s.Weight2, 0), 3) AS Weight2,
          ROUND(ISNULL(s.TotalCost2, 0), 2) AS TotalCost2,
          RTRIM(s.UserID) AS UserID
        FROM tblSplitHistory s WITH (NOLOCK)
        WHERE 1=1
      `;

      if (locationCode && locationCode !== "ALL") {
        request.input("LocationCode", sql.VarChar, locationCode.trim());
        query += ` AND s.Locationcode = @LocationCode`;
      }
      if (applyDateFilter && fDate && tDateEx) {
        request.input("FromDate", sql.DateTime, fDate);
        request.input("ToDateExclusive", sql.DateTime, tDateEx);
        query += ` AND s.SplitDate >= @FromDate AND s.SplitDate < @ToDateExclusive`;
      }

      query += ` ORDER BY s.SplitDate DESC`;
      const resQ = await request.query(query);
      rows = resQ.recordset;

    // ==========================================
    // 6. INVENTORY MAINTENANCE
    // ==========================================
    } else if (reportId === "inventory_maintenance") {
      const dateTitlePart = applyDateFilter ? ` From ${fromDateDisp} To ${toDateDisp}` : "";
      title = `Inventory Maintenance Audit Report${dateTitlePart}`;

      columns = [
        { key: "MaintenanceDate", label: "Maintenance Date", width: 140, format: "date" },
        { key: "InventoryCode", label: "Inventory Code", width: 150 },
        { key: "OldDescription", label: "Old Description", width: 180 },
        { key: "NewDescription", label: "New Description", width: 180 },
        { key: "OldTotalCost", label: "Old Cost", width: 110, align: "right", format: "currency" },
        { key: "NewTotalCost", label: "New Cost", width: 110, align: "right", format: "currency" },
        { key: "OldPMC", label: "Old PMC", width: 90, align: "right", format: "currency" },
        { key: "NewPMC", label: "New PMC", width: 90, align: "right", format: "currency" },
        { key: "OldSMC", label: "Old SMC", width: 90, align: "right", format: "currency" },
        { key: "NewSMC", label: "New SMC", width: 90, align: "right", format: "currency" },
        { key: "UserID", label: "Modified By", width: 110 }
      ];

      let query = `
        SELECT 
          m.Date AS MaintenanceDate,
          RTRIM(m.InventoryCode) AS InventoryCode,
          RTRIM(m.OldDescription) AS OldDescription,
          RTRIM(m.NewDescription) AS NewDescription,
          ROUND(ISNULL(m.OldTotalCost, 0), 2) AS OldTotalCost,
          ROUND(ISNULL(m.NewTotalCost, 0), 2) AS NewTotalCost,
          ROUND(ISNULL(m.OldPMC, 0), 2) AS OldPMC,
          ROUND(ISNULL(m.NewPMC, 0), 2) AS NewPMC,
          ROUND(ISNULL(m.OldSMC, 0), 2) AS OldSMC,
          ROUND(ISNULL(m.NewSMC, 0), 2) AS NewSMC,
          RTRIM(m.UserID) AS UserID
        FROM tblInventoryMaintenance m WITH (NOLOCK)
        WHERE 1=1
      `;

      if (applyDateFilter && fDate && tDateEx) {
        request.input("FromDate", sql.DateTime, fDate);
        request.input("ToDateExclusive", sql.DateTime, tDateEx);
        query += ` AND m.Date >= @FromDate AND m.Date < @ToDateExclusive`;
      }

      query += ` ORDER BY m.Date DESC`;
      const resQ = await request.query(query);
      rows = resQ.recordset;

    // ==========================================
    // 7. SCRAP INVENTORY STOCK
    // ==========================================
    } else if (reportId === "scrap_stock") {
      const dateTitlePart = applyDateFilter ? ` From ${fromDateDisp} To ${toDateDisp}` : "";
      title = `Scrap Inventory Stock Report${dateTitlePart}`;

      columns = [
        { key: "InventoryCode", label: "Inventory Code", width: 150 },
        { key: "Description", label: "Description", width: 220 },
        { key: "CategoryCode", label: "Category", width: 120 },
        { key: "ClassCode", label: "Class", width: 100 },
        { key: "Weight", label: "Weight (g)", width: 110, align: "right", format: "decimal3" },
        { key: "CostPerGram", label: "Cost/g", width: 100, align: "right", format: "currency" },
        { key: "TotalCost", label: "Total Cost", width: 120, align: "right", format: "currency" },
        { key: "Status", label: "Status", width: 80 },
        { key: "LocationCode", label: "Location", width: 90 },
        { key: "Remarks", label: "Remarks", width: 150 }
      ];

      let query = `
        SELECT 
          RTRIM(a.InventoryCode) AS InventoryCode,
          RTRIM(a.Description) AS Description,
          RTRIM(a.CategoryCode) AS CategoryCode,
          RTRIM(a.ClassCode) AS ClassCode,
          ROUND(ISNULL(a.Weight, 0), 3) AS Weight,
          ROUND(ISNULL(a.CostPerGram, 0), 2) AS CostPerGram,
          ROUND(ISNULL(a.TotalCost, 0), 2) AS TotalCost,
          RTRIM(b.Status) AS Status,
          RTRIM(a.LocationCode) AS LocationCode,
          RTRIM(a.Remarks) AS Remarks
        FROM tblInventory a WITH (NOLOCK)
        INNER JOIN tblInventoryStock b WITH (NOLOCK) 
          ON a.InventoryCode = b.InventoryCode AND a.LocationCode = b.LocationCode
        WHERE b.QtyOnHand > 0 
          AND (b.Status = 'SC' OR a.CategoryCode LIKE '%SCRAP%' OR a.Description LIKE '%SCRAP%' OR a.Remarks LIKE '%SCRAP%')
      `;

      query += buildInventoryFilters("a", "b");
      query += ` ORDER BY a.InventoryCode`;

      const resQ = await request.query(query);
      rows = resQ.recordset;

    // ==========================================
    // 8. ACCESSORIES STOCK
    // ==========================================
    } else if (reportId === "accessories_stock") {
      const dateTitlePart = applyDateFilter ? ` From ${fromDateDisp} To ${toDateDisp}` : "";
      title = `Accessories Stock Report${dateTitlePart}`;

      columns = [
        { key: "InventoryCode", label: "Inventory Code", width: 150 },
        { key: "Description", label: "Description", width: 220 },
        { key: "CategoryCode", label: "Category", width: 120 },
        { key: "ClassCode", label: "Class", width: 110 },
        { key: "PCS", label: "Pcs", width: 80, align: "right" },
        { key: "Weight", label: "Weight (g)", width: 110, align: "right", format: "decimal3" },
        { key: "CostPerGram", label: "Cost/g", width: 100, align: "right", format: "currency" },
        { key: "TotalCost", label: "Total Cost", width: 120, align: "right", format: "currency" },
        { key: "SupplierCode", label: "Supplier", width: 100 },
        { key: "LocationCode", label: "Location", width: 90 },
        { key: "PurchaseDate", label: "Purchase Date", width: 120, format: "date" }
      ];

      let query = `
        SELECT 
          RTRIM(a.InventoryCode) AS InventoryCode,
          RTRIM(a.Description) AS Description,
          RTRIM(a.CategoryCode) AS CategoryCode,
          RTRIM(a.ClassCode) AS ClassCode,
          ISNULL(b.PCS, 1) AS PCS,
          ROUND(ISNULL(a.Weight, 0), 3) AS Weight,
          ROUND(ISNULL(a.CostPerGram, 0), 2) AS CostPerGram,
          ROUND(ISNULL(a.TotalCost, 0), 2) AS TotalCost,
          RTRIM(a.SupplierCode) AS SupplierCode,
          RTRIM(a.LocationCode) AS LocationCode,
          a.PurchaseDate
        FROM tblInventory a WITH (NOLOCK)
        INNER JOIN tblInventoryStock b WITH (NOLOCK) 
          ON a.InventoryCode = b.InventoryCode AND a.LocationCode = b.LocationCode
        WHERE b.QtyOnHand > 0 
          AND (a.ClassCode LIKE '%ACC%' OR a.CategoryCode LIKE '%ACC%' OR a.Description LIKE '%ACC%')
      `;

      query += buildInventoryFilters("a", "b");
      query += ` ORDER BY a.InventoryCode`;

      const resQ = await request.query(query);
      rows = resQ.recordset;

    // ==========================================
    // 9. REORDER LEVEL REPORT
    // ==========================================
    } else if (reportId === "reorder_level") {
      title = `Inventory ReOrder Level Report`;

      columns = [
        { key: "CategoryCode", label: "Category Code", width: 130 },
        { key: "CatalogueCode", label: "Catalogue Code", width: 140 },
        { key: "LocationCode", label: "Location", width: 90 },
        { key: "ReOrderLevel", label: "ReOrder Level", width: 120, align: "right" },
        { key: "CurrentQtyOnHand", label: "Current Stock", width: 120, align: "right" },
        { key: "CurrentWeight", label: "Current Wt (g)", width: 130, align: "right", format: "decimal3" },
        { key: "StockStatus", label: "Status", width: 120 },
        { key: "Remarks", label: "Remarks", width: 150 }
      ];

      let query = `
        SELECT 
          RTRIM(r.CategoryCode) AS CategoryCode,
          RTRIM(r.CatalogueCode) AS CatalogueCode,
          RTRIM(r.LocationCode) AS LocationCode,
          ISNULL(r.ReOrderLevel, 0) AS ReOrderLevel,
          ISNULL(s.CurrentQtyOnHand, 0) AS CurrentQtyOnHand,
          ROUND(ISNULL(s.CurrentWeight, 0), 3) AS CurrentWeight,
          CASE 
            WHEN ISNULL(s.CurrentQtyOnHand, 0) <= ISNULL(r.ReOrderLevel, 0) THEN 'BELOW LEVEL'
            ELSE 'OK'
          END AS StockStatus,
          RTRIM(r.Remarks) AS Remarks
        FROM tblReOrderLevelMaster r WITH (NOLOCK)
        LEFT JOIN (
          SELECT 
            RTRIM(a.CategoryCode) AS CategoryCode, 
            RTRIM(a.CatalogueCode) AS CatalogueCode, 
            RTRIM(a.LocationCode) AS LocationCode, 
            SUM(ISNULL(b.QtyOnHand, 0)) AS CurrentQtyOnHand, 
            SUM(ISNULL(a.Weight, 0)) AS CurrentWeight
          FROM tblInventory a WITH (NOLOCK)
          INNER JOIN tblInventoryStock b WITH (NOLOCK) 
            ON a.InventoryCode = b.InventoryCode AND a.LocationCode = b.LocationCode
          WHERE b.QtyOnHand > 0
          GROUP BY a.CategoryCode, a.CatalogueCode, a.LocationCode
        ) s ON (r.CategoryCode = s.CategoryCode OR r.CategoryCode IS NULL)
           AND (r.CatalogueCode = s.CatalogueCode OR r.CatalogueCode IS NULL)
        WHERE 1=1
      `;

      if (locationCode && locationCode !== "ALL") {
        request.input("LocationCode", sql.VarChar, locationCode.trim());
        query += ` AND (r.LocationCode = @LocationCode OR r.LocationCode IS NULL)`;
      }
      if (categoryCode && categoryCode !== "ALL" && categoryCode.trim() !== "") {
        request.input("CategoryCode", sql.VarChar, categoryCode.trim());
        query += ` AND RTRIM(r.CategoryCode) = @CategoryCode`;
      }
      if (catalogueCode && catalogueCode !== "ALL" && catalogueCode.trim() !== "") {
        request.input("CatalogueCode", sql.VarChar, catalogueCode.trim());
        query += ` AND RTRIM(r.CatalogueCode) = @CatalogueCode`;
      }

      query += ` ORDER BY r.CategoryCode, r.CatalogueCode`;
      const resQ = await request.query(query);
      rows = resQ.recordset;

    // ==========================================
    // 10. ONLINE STOCK
    // ==========================================
    } else if (reportId === "online_stock") {
      const dateTitlePart = applyDateFilter ? ` From ${fromDateDisp} To ${toDateDisp}` : "";
      title = `Online Transfer Stock Report${dateTitlePart}`;

      columns = [
        { key: "TransferNo", label: "Transfer No", width: 130 },
        { key: "TransferDate", label: "Transfer Date", width: 130, format: "date" },
        { key: "FromLocation", label: "From Loc", width: 100 },
        { key: "ToLocation", label: "To Loc", width: 100 },
        { key: "InventoryCode", label: "Inventory Code", width: 150 },
        { key: "Description", label: "Description", width: 200 },
        { key: "CategoryCode", label: "Category", width: 110 },
        { key: "ClassCode", label: "Class", width: 90 },
        { key: "QtyOnHand", label: "Qty", width: 80, align: "right" },
        { key: "Weight", label: "Weight (g)", width: 110, align: "right", format: "decimal3" },
        { key: "TotalCost", label: "Total Cost", width: 120, align: "right", format: "currency" }
      ];

      let query = `
        SELECT 
          RTRIM(h.TransferNo) AS TransferNo,
          h.TransferDate,
          RTRIM(h.FromLocation) AS FromLocation,
          RTRIM(h.ToLocation) AS ToLocation,
          RTRIM(d.InventoryCode) AS InventoryCode,
          RTRIM(d.Description) AS Description,
          RTRIM(d.CategoryCode) AS CategoryCode,
          RTRIM(d.ClassCode) AS ClassCode,
          ISNULL(d.QtyOnHand, 1) AS QtyOnHand,
          ROUND(ISNULL(d.Weight, 0), 3) AS Weight,
          ROUND(ISNULL(d.TotalCost, 0), 2) AS TotalCost
        FROM tblInventoryOnlineTransferHeader h WITH (NOLOCK)
        INNER JOIN tblInventoryOnlineTransferDetail d WITH (NOLOCK) 
          ON h.TransferNo = d.TransferNo
        WHERE 1=1
      `;

      if (locationCode && locationCode !== "ALL") {
        request.input("LocationCode", sql.VarChar, locationCode.trim());
        query += ` AND (h.FromLocation = @LocationCode OR h.ToLocation = @LocationCode)`;
      }
      if (applyDateFilter && fDate && tDateEx) {
        request.input("FromDate", sql.DateTime, fDate);
        request.input("ToDateExclusive", sql.DateTime, tDateEx);
        query += ` AND h.TransferDate >= @FromDate AND h.TransferDate < @ToDateExclusive`;
      }

      query += ` ORDER BY h.TransferDate DESC, h.TransferNo`;
      const resQ = await request.query(query);
      rows = resQ.recordset;

    // ==========================================
    // 11. INVENTORY STOCK IN EXCEL / DATEWISE
    // ==========================================
    } else if (reportId === "stock_in_excel" || reportId === "stock_in_excel_datewise") {
      const dateTitlePart = applyDateFilter ? ` From ${fromDateDisp} To ${toDateDisp}` : "";
      title = `Complete Inventory Stock Report (Excel Format)${dateTitlePart}`;

      columns = [
        { key: "InventoryCode", label: "Inventory Code", width: 150 },
        { key: "Description", label: "Description", width: 220 },
        { key: "CategoryCode", label: "Category", width: 110 },
        { key: "CatalogueCode", label: "Catalogue", width: 110 },
        { key: "ClassCode", label: "Class", width: 90 },
        { key: "PCS", label: "Pcs", width: 80, align: "right" },
        { key: "Weight", label: "Gross Wt", width: 100, align: "right", format: "decimal3" },
        { key: "StoneWeight", label: "Stone Wt", width: 100, align: "right", format: "decimal3" },
        { key: "CostPerGram", label: "Cost/g", width: 100, align: "right", format: "currency" },
        { key: "PMC", label: "PMC", width: 90, align: "right", format: "currency" },
        { key: "SMC", label: "SMC", width: 90, align: "right", format: "currency" },
        { key: "GoldLoss", label: "Gold Loss", width: 90, align: "right", format: "currency" },
        { key: "TotalCost", label: "Total Cost", width: 120, align: "right", format: "currency" },
        { key: "PurchaseBillNo", label: "Bill No", width: 120 },
        { key: "PurchaseDate", label: "Purchase Date", width: 120, format: "date" },
        { key: "SupplierCode", label: "Supplier", width: 100 },
        { key: "LocationCode", label: "Location", width: 90 },
        { key: "TrayId", label: "Tray ID", width: 100 },
        { key: "QtyOnHand", label: "Stock Qty", width: 90, align: "right" }
      ];

      let query = `
        SELECT 
          RTRIM(a.InventoryCode) AS InventoryCode,
          RTRIM(a.Description) AS Description,
          RTRIM(a.CategoryCode) AS CategoryCode,
          RTRIM(a.CatalogueCode) AS CatalogueCode,
          RTRIM(a.ClassCode) AS ClassCode,
          ISNULL(b.PCS, 1) AS PCS,
          ROUND(ISNULL(a.Weight, 0), 3) AS Weight,
          ROUND(ISNULL(a.StoneWeight, 0), 3) AS StoneWeight,
          ROUND(ISNULL(a.CostPerGram, 0), 2) AS CostPerGram,
          ROUND(ISNULL(a.PMC, 0), 2) AS PMC,
          ROUND(ISNULL(a.SMC, 0), 2) AS SMC,
          ROUND(ISNULL(a.GoldLoss, 0), 2) AS GoldLoss,
          ROUND(ISNULL(a.TotalCost, 0), 2) AS TotalCost,
          RTRIM(a.PurchaseBillNo) AS PurchaseBillNo,
          a.PurchaseDate,
          RTRIM(a.SupplierCode) AS SupplierCode,
          RTRIM(a.LocationCode) AS LocationCode,
          RTRIM(a.TrayId) AS TrayId,
          ISNULL(b.QtyOnHand, 0) AS QtyOnHand
        FROM tblInventory a WITH (NOLOCK)
        INNER JOIN tblInventoryStock b WITH (NOLOCK) 
          ON a.InventoryCode = b.InventoryCode AND a.LocationCode = b.LocationCode
        WHERE b.QtyOnHand > 0
      `;

      query += buildInventoryFilters("a", "b");
      query += ` ORDER BY a.CategoryCode, a.InventoryCode`;

      const resQ = await request.query(query);
      rows = resQ.recordset;

    // ==========================================
    // 12. DIAMOND STOCK
    // ==========================================
    } else if (reportId === "diamond_stock") {
      const dateTitlePart = applyDateFilter ? ` From ${fromDateDisp} To ${toDateDisp}` : "";
      title = `Diamond & Stone Inventory Stock Report${dateTitlePart}`;

      columns = [
        { key: "InventoryCode", label: "Inventory Code", width: 150 },
        { key: "Description", label: "Description", width: 220 },
        { key: "DiamondDescription", label: "Diamond Details", width: 180 },
        { key: "StoneWeight", label: "Stone Wt (ct/g)", width: 130, align: "right", format: "decimal3" },
        { key: "Weight", label: "Gross Wt (g)", width: 120, align: "right", format: "decimal3" },
        { key: "PCS", label: "Pcs", width: 80, align: "right" },
        { key: "CostPerGram", label: "Cost/g", width: 100, align: "right", format: "currency" },
        { key: "TotalCost", label: "Total Cost", width: 120, align: "right", format: "currency" },
        { key: "CategoryCode", label: "Category", width: 110 },
        { key: "CatalogueCode", label: "Catalogue", width: 110 },
        { key: "ClassCode", label: "Class", width: 90 },
        { key: "SupplierCode", label: "Supplier", width: 100 },
        { key: "LocationCode", label: "Location", width: 90 },
        { key: "PurchaseDate", label: "Purchase Date", width: 120, format: "date" }
      ];

      let query = `
        SELECT 
          RTRIM(a.InventoryCode) AS InventoryCode,
          RTRIM(a.Description) AS Description,
          RTRIM(a.DiamondDescription) AS DiamondDescription,
          ROUND(ISNULL(a.StoneWeight, 0), 3) AS StoneWeight,
          ROUND(ISNULL(a.Weight, 0), 3) AS Weight,
          ISNULL(b.PCS, 1) AS PCS,
          ROUND(ISNULL(a.CostPerGram, 0), 2) AS CostPerGram,
          ROUND(ISNULL(a.TotalCost, 0), 2) AS TotalCost,
          RTRIM(a.CategoryCode) AS CategoryCode,
          RTRIM(a.CatalogueCode) AS CatalogueCode,
          RTRIM(a.ClassCode) AS ClassCode,
          RTRIM(a.SupplierCode) AS SupplierCode,
          RTRIM(a.LocationCode) AS LocationCode,
          a.PurchaseDate
        FROM tblInventory a WITH (NOLOCK)
        INNER JOIN tblInventoryStock b WITH (NOLOCK) 
          ON a.InventoryCode = b.InventoryCode AND a.LocationCode = b.LocationCode
        WHERE b.QtyOnHand > 0 
          AND (ISNULL(a.StoneWeight, 0) > 0 OR (a.DiamondDescription IS NOT NULL AND RTRIM(a.DiamondDescription) <> '') OR a.ClassCode LIKE '%DIA%' OR a.CategoryCode LIKE '%DIA%')
      `;

      query += buildInventoryFilters("a", "b");
      query += ` ORDER BY a.InventoryCode`;

      const resQ = await request.query(query);
      rows = resQ.recordset;

    // ==========================================
    // 14. SOLD INVENTORY
    // ==========================================
    } else if (reportId === "sold_inventory") {
      const dateTitlePart = applyDateFilter ? ` From ${fromDateDisp} To ${toDateDisp}` : "";
      title = `Sold Inventory Report${dateTitlePart}`;

      columns = [
        { key: "InventoryCode", label: "Inventory Code", width: 150 },
        { key: "Description", label: "Description", width: 220 },
        { key: "SalesBillNo", label: "Invoice No", width: 130 },
        { key: "SalesDate", label: "Sales Date", width: 130, format: "date" },
        { key: "Weight", label: "Weight (g)", width: 110, align: "right", format: "decimal3" },
        { key: "SalesAmount", label: "Sales Amount", width: 120, align: "right", format: "currency" },
        { key: "CostPerGram", label: "Cost/g", width: 100, align: "right", format: "currency" },
        { key: "SalesCostPerGram", label: "Sales Cost/g", width: 110, align: "right", format: "currency" },
        { key: "CategoryCode", label: "Category", width: 110 },
        { key: "CatalogueCode", label: "Catalogue", width: 110 },
        { key: "ClassCode", label: "Class", width: 90 },
        { key: "SupplierCode", label: "Supplier", width: 100 },
        { key: "LocationCode", label: "Location", width: 90 }
      ];

      let query = `
        SELECT 
          RTRIM(a.InventoryCode) AS InventoryCode,
          RTRIM(a.Description) AS Description,
          RTRIM(a.SalesBillNo) AS SalesBillNo,
          a.SalesDate,
          ROUND(ISNULL(a.Weight, 0), 3) AS Weight,
          ROUND(ISNULL(a.SalesAmount, 0), 2) AS SalesAmount,
          ROUND(ISNULL(a.CostPerGram, 0), 2) AS CostPerGram,
          ROUND(ISNULL(a.SalesCostPerGram, 0), 2) AS SalesCostPerGram,
          RTRIM(a.CategoryCode) AS CategoryCode,
          RTRIM(a.CatalogueCode) AS CatalogueCode,
          RTRIM(a.ClassCode) AS ClassCode,
          RTRIM(a.SupplierCode) AS SupplierCode,
          RTRIM(a.LocationCode) AS LocationCode
        FROM tblInventory a WITH (NOLOCK)
        WHERE (a.SalesBillNo IS NOT NULL AND RTRIM(a.SalesBillNo) <> '')
      `;

      if (locationCode && locationCode !== "ALL") {
        request.input("LocationCode", sql.VarChar, locationCode.trim());
        query += ` AND a.LocationCode = @LocationCode`;
      }
      if (supplierCode && supplierCode !== "ALL") {
        request.input("SupplierCode", sql.VarChar, supplierCode.trim());
        query += ` AND a.SupplierCode = @SupplierCode`;
      }
      if (classCode && classCode !== "ALL" && classCode.trim() !== "") {
        request.input("ClassCode", sql.VarChar, classCode.trim());
        query += ` AND RTRIM(a.ClassCode) = @ClassCode`;
      }
      if (categoryCode && categoryCode !== "ALL" && categoryCode.trim() !== "") {
        request.input("CategoryCode", sql.VarChar, categoryCode.trim());
        query += ` AND RTRIM(a.CategoryCode) = @CategoryCode`;
      }
      if (applyDateFilter && fDate && tDateEx) {
        request.input("FromDate", sql.DateTime, fDate);
        request.input("ToDateExclusive", sql.DateTime, tDateEx);
        query += ` AND a.SalesDate >= @FromDate AND a.SalesDate < @ToDateExclusive`;
      }

      query += ` ORDER BY a.SalesDate DESC, a.InventoryCode`;
      const resQ = await request.query(query);
      rows = resQ.recordset;

    // ==========================================
    // 15. PURCHASE RETURN (rptPurchaseReturnDetail.rpt)
    // ==========================================
    } else if (reportId === "purchase_return") {
      const dateTitlePart = applyDateFilter ? ` From ${fromDateDisp} To ${toDateDisp}` : "";
      title = `Purchase Return Inventory Report${dateTitlePart}`;

      columns = [
        { key: "ReturnNo", label: "Return No", width: 130 },
        { key: "ReturnDate", label: "Return Date", width: 130, format: "date" },
        { key: "SupplierCode", label: "Supplier", width: 110 },
        { key: "LocationCode", label: "Location", width: 90 },
        { key: "InventoryCode", label: "Inventory Code", width: 150 },
        { key: "Description", label: "Description", width: 220 },
        { key: "PCS", label: "Pcs", width: 80, align: "right" },
        { key: "Weight", label: "Weight (g)", width: 110, align: "right", format: "decimal3" },
        { key: "GoldLoss", label: "Gold Loss", width: 100, align: "right", format: "currency" },
        { key: "MC", label: "MC", width: 90, align: "right", format: "currency" },
        { key: "CostPerGram", label: "Cost/g", width: 100, align: "right", format: "currency" },
        { key: "TotalValue", label: "Total Value", width: 120, align: "right", format: "currency" },
        { key: "Remarks", label: "Remarks", width: 150 }
      ];

      let query = `
        SELECT 
          RTRIM(h.ReturnNo) AS ReturnNo,
          h.ReturnDate,
          RTRIM(h.SupplierCode) AS SupplierCode,
          RTRIM(h.LocationCode) AS LocationCode,
          RTRIM(d.InventoryCode) AS InventoryCode,
          RTRIM(d.Description) AS Description,
          ISNULL(d.PCS, 1) AS PCS,
          ROUND(ISNULL(d.Weight, 0), 3) AS Weight,
          ROUND(ISNULL(d.GoldLoss, 0), 2) AS GoldLoss,
          ROUND(ISNULL(d.MC, 0), 2) AS MC,
          ROUND(ISNULL(d.CostPerGram, 0), 2) AS CostPerGram,
          ROUND(ISNULL(d.TotalValue, 0), 2) AS TotalValue,
          RTRIM(h.Remarks) AS Remarks
        FROM tblPurchaseReturnHeader h WITH (NOLOCK)
        INNER JOIN tblPurchaseReturnDetail d WITH (NOLOCK)
          ON h.ReturnNo = d.ReturnNo
        WHERE 1=1
      `;

      if (locationCode && locationCode !== "ALL") {
        request.input("LocationCode", sql.VarChar, locationCode.trim());
        query += ` AND h.LocationCode = @LocationCode`;
      }
      if (supplierCode && supplierCode !== "ALL") {
        request.input("SupplierCode", sql.VarChar, supplierCode.trim());
        query += ` AND h.SupplierCode = @SupplierCode`;
      }
      if (applyDateFilter && fDate && tDateEx) {
        request.input("FromDate", sql.DateTime, fDate);
        request.input("ToDateExclusive", sql.DateTime, tDateEx);
        query += ` AND h.ReturnDate >= @FromDate AND h.ReturnDate < @ToDateExclusive`;
      }

      query += ` ORDER BY h.ReturnDate DESC, h.ReturnNo, d.InventoryCode`;
      const resQ = await request.query(query);
      rows = resQ.recordset;
    }

    // ==========================================
    // SUMMARY TOTALS CALCULATION
    // ==========================================
    let totalItems = rows.length;
    let totalPcs = 0;
    let totalWeight = 0;
    let totalCost = 0;
    let totalAmount = 0;

    rows.forEach((r) => {
      if (r.TotalPcs !== undefined) totalPcs += Number(r.TotalPcs) || 0;
      else if (r.PCS !== undefined) totalPcs += Number(r.PCS) || 0;
      else if (r.QtyOnHand !== undefined) totalPcs += Number(r.QtyOnHand) || 0;

      if (r.TotalWeight !== undefined) totalWeight += Number(r.TotalWeight) || 0;
      else if (r.Weight !== undefined) totalWeight += Number(r.Weight) || 0;
      else if (r.NewWeight !== undefined) totalWeight += Number(r.NewWeight) || 0;
      else if (r.OriginalWeight !== undefined) totalWeight += Number(r.OriginalWeight) || 0;

      if (r.TotalCost !== undefined) totalCost += Number(r.TotalCost) || 0;
      else if (r.NewTotalCost !== undefined) totalCost += Number(r.NewTotalCost) || 0;
      else if (r.OriginalTotalCost !== undefined) totalCost += Number(r.OriginalTotalCost) || 0;

      if (r.SalesAmount !== undefined) totalAmount += Number(r.SalesAmount) || 0;
    });

    res.json({
      success: true,
      reportId,
      reportType,
      title,
      columns,
      data: rows,
      totals: {
        totalItems,
        totalPcs: Math.round(totalPcs),
        totalWeight: Number(totalWeight.toFixed(3)),
        totalCost: Number(totalCost.toFixed(2)),
        totalAmount: Number(totalAmount.toFixed(2))
      }
    });

  } catch (err) {
    console.error("Error generating inventory report:", err);
    res.status(500).json({
      success: false,
      message: err.message || "Failed to generate inventory report."
    });
  }
});


router.post("/export-pdf", (req, res) => {
  const { handleExportPdf } = require("../utils/reportExportHandlers");
  handleExportPdf({ req, res, defaultTitle: "Inventory_Report" });
});

// EXPORT EXCEL & PDF ENDPOINTS
router.post("/export-excel", (req, res) => {
  const { handleExportExcel } = require("../utils/reportExportHandlers");
  handleExportExcel({ req, res, defaultTitle: "Inventory_Report" });
});

router.post("/export-pdf", (req, res) => {
  const { handleExportPdf } = require("../utils/reportExportHandlers");
  handleExportPdf({ req, res, defaultTitle: "Inventory_Report" });
});


router.post('/download-html', async (req, res) => {
  try {
    const { generateReportHtml } = require('../utils/reportHtmlTemplate');
    const { title, columns, data, totals, companyInfo, fromDate, toDate, showPrintButton } = req.body;
    const html = await generateReportHtml({
      title,
      columns,
      data,
      totals,
      companyInfo,
      fromDate,
      toDate,
      showPrintButton: showPrintButton ?? false,
    });
    res.setHeader('Content-Type', 'text/html; charset=utf-8');
    return res.send(html);
  } catch (err) {
    console.error('download-html error:', err);
    return res.status(500).send('Error generating HTML report');
  }
});

module.exports = router;


