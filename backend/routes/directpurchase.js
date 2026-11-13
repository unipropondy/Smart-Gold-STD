const express = require("express");
const { sql } = require("../db");
const router = express.Router();

// Schema cache to avoid heavy queries on remote DB
let cachedColsMap = null;
let cachedColsTime = 0;
async function getCachedSchemaCols(pool) {
  const now = Date.now();
  if (cachedColsMap && now - cachedColsTime < 30 * 60 * 1000) {
    return cachedColsMap;
  }
  try {
    const res = await pool.request().query(`
      SELECT t.name AS TABLE_NAME, c.name AS COLUMN_NAME 
      FROM sys.tables t WITH (NOLOCK)
      INNER JOIN sys.columns c WITH (NOLOCK) ON t.object_id = c.object_id
      WHERE t.name IN (
        'tblPurchase',
        'tblInventory',
        'tblInventoryStock',
        'tblInventoryMovement',
        'tblJobSheetHeader',
        'tblJobSheetDetail',
        'tblInventoryBarCoin',
        'tblInventoryAddlDescription',
        'tblInventoryOrderDetail',
        'tblInventoryOrderHeader',
        'tblTradeInDetail'
      )
    `);
    const map = {};
    for (const r of res.recordset) {
      const t = r.TABLE_NAME.toLowerCase();
      const c = r.COLUMN_NAME.toLowerCase();
      if (!map[t]) map[t] = new Set();
      map[t].add(c);
    }
    cachedColsMap = map;
    cachedColsTime = now;
    return cachedColsMap;
  } catch (e) {
    console.warn("Schema cache fetch warning:", e.message);
    return cachedColsMap || {};
  }
}

function hasTableCol(schemaMap, tableName, colName) {
  if (!schemaMap) return true;
  const t = tableName.toLowerCase();
  const c = colName.toLowerCase();
  if (!schemaMap[t] || schemaMap[t].size === 0) {
    return true;
  }
  return schemaMap[t].has(c);
}

// SP existence cache
let cachedSPs = null;
let cachedSPsTime = 0;
async function getCachedSPs(pool) {
  const now = Date.now();
  if (cachedSPs && now - cachedSPsTime < 30 * 60 * 1000) {
    return cachedSPs;
  }
  try {
    const spCheck = await pool.request().query(`
      SELECT name AS ROUTINE_NAME 
      FROM sys.procedures WITH (NOLOCK)
      WHERE name IN ('sp_DPInventory', 'sp_Purchase', 'Sp_UpdateNewInventoryMovement', 'sp_DPJobsheetSave', 'sp_UpdateInventoryOrderStatus', 'sp_InventoryBarCoinSave', 'sp_InventoryAddlDescSave', 'sp_UpdateTradeInDetails', 'sp_DPInventorydelete', 'sp_UpdateStatusOnDelete')
    `);
    cachedSPs = (spCheck.recordset || []).map(r => r.ROUTINE_NAME.toLowerCase());
    cachedSPsTime = now;
    return cachedSPs;
  } catch (e) {
    console.warn("SP cache fetch warning:", e.message);
    return cachedSPs || [];
  }
}

// ================= GET ALL DIRECT PURCHASES (LOV / SEARCH) =================
router.get("/", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query(`
      SELECT TOP 500
        p.PurchaseBillNo, 
        MAX(p.SupplierCode) AS SupplierCode, 
        MAX(s.SupplierName) AS SupplierName,
        MAX(p.JobSheetNo) AS JobSheetNo, 
        MAX(p.CreateDate) AS CreateDate
      FROM tblPurchase p WITH (NOLOCK)
      LEFT JOIN tblSupplier s WITH (NOLOCK) ON p.SupplierCode = s.SupplierCode
      WHERE p.PurchaseBillNo IS NOT NULL AND p.PurchaseBillNo <> ''
      GROUP BY p.PurchaseBillNo
      ORDER BY MAX(p.CreateDate) DESC
    `);
    return res.status(200).json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("🔥 GET ALL DIRECT PURCHASES ERROR:", err);
    return res.status(500).json({ success: false, message: "Server Error", error: err.message });
  }
});

// ================= PRINT DIRECT PURCHASE REPORT (SQL QUERY) =================
router.get("/print-report", async (req, res) => {
  try {
    const pool = req.pool;
    const { jobSheetNo, purchaseBillNo } = req.query;

    let whereConditions = [];
    const request = pool.request();

    if (jobSheetNo && jobSheetNo.trim() !== '') {
      request.input("JobSheetNo", sql.VarChar, jobSheetNo.trim());
      whereConditions.push("tblJobSheetHeader.JobSheetNo = @JobSheetNo");
    }

    if (purchaseBillNo && purchaseBillNo.trim() !== '') {
      request.input("PurchaseBillNo", sql.VarChar, purchaseBillNo.trim());
      whereConditions.push("(tblJobSheetHeader.PurchaseBillNo = @PurchaseBillNo OR tblPurchase.PurchaseBillNo = @PurchaseBillNo)");
    }

    const whereClause = whereConditions.length > 0 ? " WHERE " + whereConditions.join(" AND ") : "";

    // 1. Primary Query: exact INNER JOIN matching VB.NET rptPurchaseDetail
    let queryInner = `
      SELECT 
        tblJobSheetHeader.JobSheetNo, 
        tblJobSheetHeader.JobSheetDate, 
        tblPurchase.Description, 
        tblPurchase.Weight, 
        tblPurchase.GoldLoss, 
        tblPurchase.PCS, 
        tblPurchase.MC, 
        tblPurchase.TotalCost, 
        tblPurchase.InventoryCode, 
        tblPurchase.CreateDate, 
        tblPurchase.CreateUser, 
        tblJobSheetHeader.PurchaseBillNo, 
        tblJobSheetHeader.PurchaseBillDate, 
        tblJobSheetDetail.CatalogueCode, 
        tblJobSheetDetail.Weight AS DetailWeight, 
        tblJobSheetDetail.PCS AS DetailPCS, 
        tblJobSheetDetail.MC AS DetailMC, 
        tblJobSheetDetail.TotalCost AS DetailTotalCost, 
        tblPurchase.SupplierCode, 
        tblPurchase.DiamondDescription
      FROM tblJobSheetHeader tblJobSheetHeader WITH (NOLOCK)
      INNER JOIN tblPurchase tblPurchase WITH (NOLOCK)
        ON tblJobSheetHeader.JobSheetNo = tblPurchase.JobSheetNo
      INNER JOIN tblJobSheetDetail tblJobSheetDetail WITH (NOLOCK)
        ON tblJobSheetHeader.JobSheetNo = tblJobSheetDetail.JobSheetNo 
        AND tblPurchase.CatalogueCode = tblJobSheetDetail.CatalogueCode 
        AND tblPurchase.CategoryCode = tblJobSheetDetail.CategoryCode 
        AND tblPurchase.JobSheetNo = tblJobSheetDetail.JobSheetNo
      ${whereClause}
      ORDER BY tblPurchase.CreateDate DESC, tblPurchase.InventoryCode ASC
    `;

    let result = await request.query(queryInner);

    // 2. Fallback Query: If tblJobSheetDetail has no entries for this sheet, use LEFT JOIN
    // so that direct purchase lines are never omitted or empty
    if (!result.recordset || result.recordset.length === 0) {
      let queryLeft = `
        SELECT 
          tblJobSheetHeader.JobSheetNo, 
          tblJobSheetHeader.JobSheetDate, 
          tblPurchase.Description, 
          tblPurchase.Weight, 
          tblPurchase.GoldLoss, 
          tblPurchase.PCS, 
          tblPurchase.MC, 
          tblPurchase.TotalCost, 
          tblPurchase.InventoryCode, 
          tblPurchase.CreateDate, 
          tblPurchase.CreateUser, 
          tblJobSheetHeader.PurchaseBillNo, 
          tblJobSheetHeader.PurchaseBillDate, 
          tblJobSheetDetail.CatalogueCode, 
          tblJobSheetDetail.Weight AS DetailWeight, 
          tblJobSheetDetail.PCS AS DetailPCS, 
          tblJobSheetDetail.MC AS DetailMC, 
          tblJobSheetDetail.TotalCost AS DetailTotalCost, 
          tblPurchase.SupplierCode, 
          tblPurchase.DiamondDescription
        FROM tblJobSheetHeader tblJobSheetHeader WITH (NOLOCK)
        INNER JOIN tblPurchase tblPurchase WITH (NOLOCK)
          ON tblJobSheetHeader.JobSheetNo = tblPurchase.JobSheetNo
        LEFT JOIN tblJobSheetDetail tblJobSheetDetail WITH (NOLOCK)
          ON tblJobSheetHeader.JobSheetNo = tblJobSheetDetail.JobSheetNo 
          AND tblPurchase.CatalogueCode = tblJobSheetDetail.CatalogueCode 
          AND tblPurchase.CategoryCode = tblJobSheetDetail.CategoryCode 
          AND tblPurchase.JobSheetNo = tblJobSheetDetail.JobSheetNo
        ${whereClause}
        ORDER BY tblPurchase.CreateDate DESC, tblPurchase.InventoryCode ASC
      `;
      result = await request.query(queryLeft);
    }

    // 3. Fetch Company Information matching frmDirectPurchase.btnReport_Click
    let company = {
      name: "SMART GOLD",
      address1: "",
      address2: "",
      city: "",
      phone: "",
      fax: ""
    };

    try {
      const compRes = await pool.request().query("SELECT TOP 1 * FROM tblCompany");
      if (compRes.recordset && compRes.recordset.length > 0) {
        const c = compRes.recordset[0];
        company = {
          name: c.CompanyName || c.Name || "SMART GOLD",
          address1: c.Address1 || "",
          address2: c.Address2 || "",
          city: c.City || "",
          phone: c.Phone || c.Telephone || "",
          fax: c.Fax || "",
          gstNo: c.GSTNo || c.GstNo || ""
        };
      }
    } catch (_) {}

    const rows = (result.recordset || []).map(r => ({
      ...r,
      DetailWeight: r.DetailWeight != null ? r.DetailWeight : r.Weight,
      DetailPCS: r.DetailPCS != null ? r.DetailPCS : r.PCS,
      DetailMC: r.DetailMC != null ? r.DetailMC : r.MC,
      DetailTotalCost: r.DetailTotalCost != null ? r.DetailTotalCost : r.TotalCost,
    }));

    // 4. Compute Totals
    let totWeight = 0;
    let totGoldLoss = 0;
    let totPCS = 0;
    let totMC = 0;
    let totCost = 0;
    let totDetailWeight = 0;
    let totDetailPCS = 0;
    let totDetailMC = 0;
    let totDetailCost = 0;

    rows.forEach(r => {
      totWeight += Number(r.Weight) || 0;
      totGoldLoss += Number(r.GoldLoss) || 0;
      totPCS += Number(r.PCS) || 0;
      totMC += Number(r.MC) || 0;
      totCost += Number(r.TotalCost) || 0;
      totDetailWeight += Number(r.DetailWeight) || 0;
      totDetailPCS += Number(r.DetailPCS) || 0;
      totDetailMC += Number(r.DetailMC) || 0;
      totDetailCost += Number(r.DetailTotalCost) || 0;
    });

    return res.status(200).json({
      success: true,
      title: "Direct Purchase Detail Report",
      company,
      data: rows,
      totals: {
        totalWeight: Number(totWeight.toFixed(3)),
        totalGoldLoss: Number(totGoldLoss.toFixed(3)),
        totalPCS: totPCS,
        totalMC: Number(totMC.toFixed(2)),
        totalCost: Number(totCost.toFixed(2)),
        totalDetailWeight: Number(totDetailWeight.toFixed(3)),
        totalDetailPCS: totDetailPCS,
        totalDetailMC: Number(totDetailMC.toFixed(2)),
        totalDetailCost: Number(totDetailCost.toFixed(2))
      }
    });
  } catch (err) {
    console.error("🔥 PRINT DIRECT PURCHASE REPORT ERROR:", err);
    return res.status(500).json({ success: false, message: "Server Error", error: err.message });
  }
});

// ================= GET GOLD LOSS BY WEIGHT (matching clsPurchase.GetGoldLossByWeight) =================
router.get("/goldloss", async (req, res) => {
  try {
    const weight = parseFloat(req.query.weight) || 0;
    const pool = req.pool;
    const result = await pool.request()
      .input("Weight", sql.Numeric(18, 3), weight)
      .query(`
        SELECT TOP 1 GoldLoss 
        FROM tblGoldLoss WITH (NOLOCK) 
        WHERE @Weight BETWEEN FromWeight AND ToWeight
      `);
    if (result.recordset.length > 0) {
      return res.status(200).json({ success: true, goldLoss: result.recordset[0].GoldLoss });
    }
    return res.status(200).json({ success: true, goldLoss: 0 });
  } catch (err) {
    return res.status(500).json({ success: false, message: err.message, goldLoss: 0 });
  }
});

// ================= GET CATALOGUE SUMMARY (matching clsDirectPurchase.GetCatalogueSummary) =================
router.get("/catalogue-summary", async (req, res) => {
  try {
    const { catalogueCode, jobSheetNo } = req.query;
    if (!catalogueCode) {
      return res.status(200).json({ success: true, weight: 0, pcs: 0 });
    }
    const pool = req.pool;
    const result = await pool.request()
      .input("CatalogueCode", sql.VarChar, (catalogueCode || '').trim())
      .input("JobSheetNo", sql.VarChar, (jobSheetNo || '').trim())
      .query(`
        SELECT 
          ISNULL(SUM(Weight), 0) AS TotalWeight, 
          ISNULL(SUM(PCS), 0) AS TotalPCS 
        FROM tblPurchase WITH (NOLOCK)
        WHERE CatalogueCode = @CatalogueCode AND JobSheetNo = @JobSheetNo
      `);
    const r = result.recordset[0] || { TotalWeight: 0, TotalPCS: 0 };
    return res.status(200).json({
      success: true,
      weight: r.TotalWeight,
      pcs: r.TotalPCS
    });
  } catch (err) {
    return res.status(500).json({ success: false, message: err.message, weight: 0, pcs: 0 });
  }
});

// ================= GET CATEGORY DETAIL WITH SIZEFACTOR & COSTPERGRAM =================
router.get("/category-detail/:categoryCode", async (req, res) => {
  try {
    const { categoryCode } = req.params;
    const { jobSheetNo, catalogueCode } = req.query;
    const pool = req.pool;
    const catRes = await pool.request()
      .input("CategoryCode", sql.VarChar, categoryCode.trim())
      .query(`
        SELECT CategoryCode, CategoryName, ClassCode, NextInventoryCode, SizeFactor 
        FROM tblCategory WITH (NOLOCK) 
        WHERE CategoryCode = @CategoryCode
      `);
    let cat = catRes.recordset[0] || null;
    let costPerGram = null;
    if (jobSheetNo && catalogueCode) {
      const jdRes = await pool.request()
        .input("JobSheetNo", sql.VarChar, jobSheetNo.trim())
        .input("CatalogueCode", sql.VarChar, catalogueCode.trim())
        .query(`
          SELECT CostPerGram 
          FROM tblJobSheetDetail WITH (NOLOCK) 
          WHERE JobSheetNo = @JobSheetNo AND CatalogueCode = @CatalogueCode
        `);
      if (jdRes.recordset.length > 0) {
        costPerGram = jdRes.recordset[0].CostPerGram;
      }
    }
    return res.status(200).json({
      success: true,
      category: cat,
      costPerGram: costPerGram
    });
  } catch (err) {
    return res.status(500).json({ success: false, message: err.message });
  }
});

// ================= GET DIRECT PURCHASE BY BILL NO OR JOBSHEET NO =================
router.get("/:purchaseBillNo", async (req, res) => {
  try {
    const { purchaseBillNo } = req.params;
    const pool = req.pool;

    let detailsResult = await pool.request()
      .input("PurchaseBillNo", sql.VarChar, purchaseBillNo)
      .query(`
        SELECT p.*, s.SupplierName 
        FROM tblPurchase p WITH (NOLOCK)
        LEFT JOIN tblSupplier s WITH (NOLOCK) ON p.SupplierCode = s.SupplierCode
        WHERE p.PurchaseBillNo = @PurchaseBillNo
      `);

    // If not found by PurchaseBillNo, check by JobSheetNo (matching clsInventory.GetInventorybyJobSheet)
    if (detailsResult.recordset.length === 0) {
      detailsResult = await pool.request()
        .input("JobSheetNo", sql.VarChar, purchaseBillNo)
        .query(`
          SELECT a.*, b.PCS, b.TotalCost, s.SupplierName
          FROM tblInventory a WITH (NOLOCK)
          INNER JOIN tblPurchase b WITH (NOLOCK) ON a.JobSheetNo = b.JobSheetNo AND a.InventoryCode = b.InventoryCode
          LEFT JOIN tblSupplier s WITH (NOLOCK) ON a.SupplierCode = s.SupplierCode
          WHERE a.JobSheetNo = @JobSheetNo
          ORDER BY a.InventoryCode ASC
        `);
    }

    if (detailsResult.recordset.length === 0) {
      return res.status(404).json({ success: false, message: "Direct purchase not found" });
    }

    const firstRow = detailsResult.recordset[0];
    const jsNo = firstRow.JobSheetNo || purchaseBillNo;

    // Fetch tblJobSheetHeader to get full direct purchase summary and settings
    let jsHeader = null;
    if (jsNo) {
      const jsHRes = await pool.request()
        .input("JobSheetNo", sql.VarChar, jsNo)
        .query(`
          SELECT jh.*, s.SupplierName, c.CurrencyName 
          FROM tblJobSheetHeader jh WITH (NOLOCK)
          LEFT JOIN tblSupplier s WITH (NOLOCK) ON jh.SupplierCode = s.SupplierCode
          LEFT JOIN tblCurrency c WITH (NOLOCK) ON jh.CurrencyCode = c.CurrencyCode
          WHERE jh.JobSheetNo = @JobSheetNo
        `);
      if (jsHRes.recordset.length > 0) {
        jsHeader = jsHRes.recordset[0];
      }
    }

    const header = {
      PurchaseBillNo: firstRow.PurchaseBillNo,
      SupplierCode: jsHeader ? jsHeader.SupplierCode : firstRow.SupplierCode,
      SupplierName: jsHeader ? jsHeader.SupplierName : firstRow.SupplierName,
      JobSheetNo: firstRow.JobSheetNo,
      JobSheetDate: jsHeader?.JobSheetDate || firstRow.CreateDate,
      PurchaseBillDate: jsHeader?.PurchaseBillDate || firstRow.CreateDate,
      Remarks: jsHeader ? jsHeader.Remarks : firstRow.Remarks,
      LocationCode: jsHeader ? jsHeader.LocationCode : firstRow.LocationCode,
      CurrencyCode: jsHeader ? jsHeader.CurrencyCode : (firstRow.CurrencyCode || 'SGD'),
      CurrencyName: jsHeader ? jsHeader.CurrencyName : '',
      CurrencyRate: jsHeader?.CurrencyRate != null ? jsHeader.CurrencyRate : (firstRow.CurrencyRate != null ? firstRow.CurrencyRate : 1),
      TotalCost: jsHeader?.TotalCost != null ? jsHeader.TotalCost : (jsHeader?.ManualLocalCost != null ? jsHeader.ManualLocalCost : ''),
      TotalMC: jsHeader?.TotalMC != null ? jsHeader.TotalMC : (jsHeader?.ManualMC != null ? jsHeader.ManualMC : ''),
      TotalWeight24K: jsHeader?.TotalWeight24K != null ? jsHeader.TotalWeight24K : (jsHeader?.ManualWeight != null ? jsHeader.ManualWeight : ''),
      TotalGoldLoss: jsHeader?.TotalGoldLoss != null ? jsHeader.TotalGoldLoss : (jsHeader?.ManualGoldLoss != null ? jsHeader.ManualGoldLoss : ''),
      CostPerGram: jsHeader?.CostPerGram != null ? jsHeader.CostPerGram : '',
      GST: jsHeader?.GST != null ? jsHeader.GST : 0,
      TaelGST: jsHeader?.TaelGST || 'N',
      TaelRate: jsHeader?.TaelRate != null ? jsHeader.TaelRate : 0,
      TaelPercent: jsHeader?.TaelPercent != null ? jsHeader.TaelPercent : 0,
      PaymentType: jsHeader?.PaymentType || 'C',
      FixStatus: jsHeader?.FixStatus || 'N',
      BillClose: jsHeader?.BillClose || 'N',
      TranType: jsHeader?.TranType || 'DP',
      CreateDate: firstRow.CreateDate
    };

    return res.status(200).json({
      success: true,
      header: header,
      details: detailsResult.recordset
    });
  } catch (err) {
    console.error("🔥 GET DIRECT PURCHASE ERROR:", err);
    return res.status(500).json({ success: false, message: "Server Error", error: err.message });
  }
});

// ================= SAVE / UPDATE DIRECT PURCHASE =================
router.post("/", async (req, res) => {
  const pool = req.pool;
  let transaction;

  try {
    const { header, details, userId } = req.body;

    if (!header || !header.PurchaseBillNo || !header.PurchaseBillNo.trim()) {
      return res.status(400).json({ success: false, message: "Enter invoice no." });
    }

    if (!header.SupplierCode || !header.SupplierCode.trim()) {
      return res.status(400).json({ success: false, message: "Enter supplier code." });
    }

    // Verify SupplierCode exists in tblSupplier
    const supCheck = await pool.request()
      .input('SupplierCode', sql.VarChar, header.SupplierCode.trim())
      .query("SELECT 1 FROM tblSupplier WITH (NOLOCK) WHERE SupplierCode = @SupplierCode");
    if (supCheck.recordset.length === 0) {
      return res.status(400).json({ success: false, message: "Invalid supplier code!" });
    }

    if (!header.InvoiceDate || isNaN(Date.parse(header.InvoiceDate))) {
      return res.status(400).json({ success: false, message: "Enter valid invoice date." });
    }

    if (!header.CurrencyCode || !header.CurrencyCode.trim()) {
      return res.status(400).json({ success: false, message: "Enter currency code." });
    }

    // Verify CurrencyCode exists in tblCurrency
    const currCheck = await pool.request()
      .input('CurrencyCode', sql.VarChar, header.CurrencyCode.trim())
      .query("SELECT 1 FROM tblCurrency WITH (NOLOCK) WHERE CurrencyCode = @CurrencyCode");
    if (currCheck.recordset.length === 0) {
      return res.status(400).json({ success: false, message: "Set Default Currency in Currency Master" });
    }

    const currRate = parseFloat(header.CurrencyRate) || 0;
    if (currRate <= 0) {
      return res.status(400).json({ success: false, message: "Enter currency rate." });
    }

    if (!header.PaymentType && !header.Payable) {
      return res.status(400).json({ success: false, message: "Select payment type." });
    }

    const isPieceClass = header.PurchaseType === 'P' || header.PurchaseMCType === 'PerPCS';
    if (!isPieceClass) {
      if ((parseFloat(header.TEALPercent) || 0) <= 0) {
        return res.status(400).json({ success: false, message: "Enter TEAL Percent." });
      }
      if ((parseFloat(header.TEAL) || 0) <= 0) {
        return res.status(400).json({ success: false, message: "Enter teal." });
      }
      if ((parseFloat(header.CostPerGram) || 0) <= 0) {
        return res.status(400).json({ success: false, message: "Enter cost per gram." });
      }
    }

    if (!details || details.length === 0) {
      return res.status(400).json({ success: false, message: "Please add at least one line item to the grid" });
    }

    // Detail-level checks matching fncValidateInventory in frmDirectPurchase.vb
    for (const d of details) {
      if (!String(d.InventoryCode || '').trim()) {
        return res.status(400).json({ success: false, message: "Inventory Code Cannot be Blank ...." });
      }
      if (!String(d.CatalogueCode || '').trim()) {
        return res.status(400).json({ success: false, message: "Catalogue Code Cannot be Blank ...." });
      }
      if (!String(d.CategoryCode || '').trim()) {
        return res.status(400).json({ success: false, message: "Category Code Cannot be Blank ...." });
      }
      if (!String(d.TrayId || '').trim()) {
        return res.status(400).json({ success: false, message: "Tray Id Cannot be Blank ...." });
      }
      if (!String(d.InventoryDescription || d.Description || '').trim()) {
        return res.status(400).json({ success: false, message: "Description Cannot be Blank ...." });
      }
      const dWeight = parseFloat(d.Weight) || 0;
      const isDetPiece = d.PurchaseMCType === 'PerPCS' || d.PurchaseType === 'P' || d.PMCType === 'P';
      if (dWeight <= 0 && !isDetPiece) {
        return res.status(400).json({ success: false, message: "Invalid weight!" });
      }
      const dPcs = parseInt(d.PCS, 10) || 0;
      if (dPcs < 1) {
        return res.status(400).json({ success: false, message: "Invalid pcs!" });
      }
      const dCostPerGram = parseFloat(d.CostPerGram) || 0;
      if (dCostPerGram <= 0 && !isDetPiece) {
        return res.status(400).json({ success: false, message: "Invalid cost per gram!" });
      }

      if (d.IPM) {
        if (!String(d.IPMType || '').trim()) {
          return res.status(400).json({ success: false, message: "Type Cannot be Blank ...." });
        }
        if (!String(d.IPMBarNo || '').trim()) {
          return res.status(400).json({ success: false, message: "BarNo Cannot be Blank ...." });
        }
        if (!String(d.IPMPurity || '').trim()) {
          return res.status(400).json({ success: false, message: "Purity Cannot be Blank ...." });
        }
        if (!String(d.IPMGSTType || '').trim()) {
          return res.status(400).json({ success: false, message: "GST Type Cannot be Blank ...." });
        }
        if (!String(d.IPMRefiner || '').trim()) {
          return res.status(400).json({ success: false, message: "Refiner Cannot be Blank ...." });
        }
        if (!String(d.IPMCountry || '').trim()) {
          return res.status(400).json({ success: false, message: "Country Cannot be Blank ...." });
        }
      }

      if (header.Ordered && (header.OrderNo || d.OrderNo)) {
        if (!d.OrderInventoryCode && !d.TradeInInventoryCode) {
          return res.status(400).json({ success: false, message: "Please select the order inventory code." });
        }
      }
    }

    // Fetch cached column names and stored procedures
    const schema = await getCachedSchemaCols(pool);

    const hasPCol = (c) => hasTableCol(schema, 'tblPurchase', c);
    const hasICol = (c) => hasTableCol(schema, 'tblInventory', c);
    const hasSCol = (c) => hasTableCol(schema, 'tblInventoryStock', c);
    const hasMCol = (c) => hasTableCol(schema, 'tblInventoryMovement', c);
    const hasBCCol = (c) => hasTableCol(schema, 'tblInventoryBarCoin', c);
    const hasACol = (c) => hasTableCol(schema, 'tblInventoryAddlDescription', c);
    const hasODCol = (c) => hasTableCol(schema, 'tblInventoryOrderDetail', c);
    const hasOHCol = (c) => hasTableCol(schema, 'tblInventoryOrderHeader', c);
    const hasTICol = (c) => hasTableCol(schema, 'tblTradeInDetail', c);
    const hasJCol = (c) => hasTableCol(schema, 'tblJobSheetHeader', c);
    const hasJDCol = (c) => hasTableCol(schema, 'tblJobSheetDetail', c);

    const existingSPs = await getCachedSPs(pool);
    const hasSP = (name) => existingSPs.includes(name.toLowerCase());

    transaction = new sql.Transaction(pool);
    const sysUser = (
      userId ||
      header.userId ||
      header.CreateUser ||
      header.CreatedBy ||
      header.ModifyUser ||
      header.ModifiedBy ||
      req.headers['x-user-id'] ||
      "SYSTEM"
    ).toString().trim();
    const curDate = new Date();

    await transaction.begin();

    // Set lock timeout to 8 seconds inside transaction to fail fast if blocked
    await new sql.Request(transaction).query(`SET LOCK_TIMEOUT 8000;`);

    // Remove previous records for this bill or items in tblPurchase, tblInventory, tblInventoryStock to allow replace/update
    const inventoryCodes = details
      .map(d => (d.InventoryCode || '').trim())
      .filter(code => code.length > 0);

    const billNoVal = (header.PurchaseBillNo || header.InvoiceNo || "").trim();
    const jsNoVal = (header.JobSheetNo || "").trim();

    const deleteReq = new sql.Request(transaction);
    deleteReq.input("PurchaseBillNo", sql.VarChar, billNoVal);
    deleteReq.input("JobSheetNo", sql.VarChar, jsNoVal);

    let deleteSql = "";
    if (billNoVal !== "") {
      deleteSql += `
        DELETE FROM tblInventoryStock WHERE InventoryCode IN (SELECT InventoryCode FROM tblPurchase WHERE PurchaseBillNo = @PurchaseBillNo);
        DELETE FROM tblPurchase WHERE PurchaseBillNo = @PurchaseBillNo;
        DELETE FROM tblInventory WHERE PurchaseBillNo = @PurchaseBillNo;
      `;
    }

    if (inventoryCodes.length > 0) {
      inventoryCodes.forEach((code, idx) => {
        deleteReq.input(`InvCode_${idx}`, sql.VarChar, code);
      });
      const codeParams = inventoryCodes.map((_, idx) => `@InvCode_${idx}`).join(", ");
      deleteSql += `
        DELETE FROM tblInventoryStock WHERE InventoryCode IN (${codeParams});
        DELETE FROM tblPurchase WHERE InventoryCode IN (${codeParams});
        DELETE FROM tblInventory WHERE InventoryCode IN (${codeParams});
      `;
    }

    if (jsNoVal !== "") {
      deleteSql += `
        DELETE FROM tblJobSheetDetail WHERE JobSheetNo = @JobSheetNo;
      `;
    }

    if (deleteSql.trim() !== "") {
      await deleteReq.query(deleteSql);
    }

    const locCodeVal = (header.LocationCode || "HQ").trim();

    // Insert line items directly into tblPurchase, tblInventory, tblInventoryStock, tblInventoryMovement
    for (let i = 0; i < details.length; i++) {
      const d = details[i];
      const invCode = (d.InventoryCode || `${billNoVal}-${i + 1}`).trim();
      const desc = (d.Description || d.InventoryDescription || "").trim();
      const diamondDesc = (d.DiamondDescription || "").trim();
      const weight = parseFloat(d.Weight) || 0;
      const stoneWeight = parseFloat(d.StoneWeight) || 0;
      const costPerGram = parseFloat(d.CostPerGram) || 0;
      const pmcCost = parseFloat(d.MC || d.PMCCost || d.PMC) || 0;
      const smcCost = parseFloat(d.SMC || d.SMCCost) || 0;
      const pcs = parseInt(d.PCS, 10) || 1;
      const goldLoss = parseFloat(d.GoldLoss) || 0;
      const totalCost = parseFloat(d.TotalCost) || 0;
      const catCode = (d.CatalogueCode || "").trim();
      const categoryCode = (d.CategoryCode || "").trim();
      const classCode = (d.ClassCode || "").trim();
      const trayId = (d.TrayId || "").trim();
      const pmcType = d.PMCType || (d.PurchaseMCType === 'PerPCS' ? 'P' : 'W');
      const smcType = d.SMCType || (d.SalesMCType === 'PerPCS' ? 'P' : 'W');
      const sizeVal = parseFloat(d.Size || d.SizeCode) || 0;
      const sizeType = (d.SizeType || (d.TradeInInventoryCode ? d.TradeInInventoryCode.trim() : "")).trim();
      const purchaseType = d.PurchaseType || (d.PurchaseMCType === 'PerPCS' || d.PMCType === 'P' ? 'P' : 'W');
      const remarks = (header.Remarks || d.Remarks || "").trim();
      const salesPcs = d.SalesPCS ? 1 : 0;

      // 1. Direct insert into tblPurchase
      const insertReq = new sql.Request(transaction);
      const pC = [];
      const pV = [];
      const addP = (col, type, val) => {
        if (hasPCol(col)) {
          pC.push(col);
          pV.push(`@p_${col}`);
          insertReq.input(`p_${col}`, type, val);
        }
      };

      addP("LocationCode", sql.VarChar, locCodeVal);
      addP("JobSheetNo", sql.VarChar, header.JobSheetNo || "");
      addP("DirectPurchaseNo", sql.VarChar, d.DirectPurchaseNo || "");
      addP("PurchaseBillNo", sql.VarChar, billNoVal);
      addP("SupplierCode", sql.VarChar, header.SupplierCode || "");
      addP("InventoryCode", sql.VarChar, invCode);
      addP("Description", sql.VarChar, desc);
      addP("DiamondDescription", sql.VarChar, diamondDesc);
      addP("StoneWeight", sql.Numeric(18, 3), stoneWeight);
      addP("CostPerGram", sql.Numeric(18, 2), costPerGram);
      addP("Size", sql.Numeric(18, 2), sizeVal);
      addP("Weight", sql.Numeric(18, 3), weight);
      addP("MC", sql.Numeric(18, 2), pmcCost);
      addP("PCS", sql.Numeric(18, 0), pcs);
      addP("GoldLoss", sql.Numeric(18, 3), goldLoss);
      addP("TotalCost", sql.Numeric(18, 2), totalCost);
      addP("CatalogueCode", sql.VarChar, catCode);
      addP("CategoryCode", sql.VarChar, categoryCode);
      addP("ClassCode", sql.VarChar, classCode);
      addP("TrayId", sql.VarChar, trayId);
      addP("PurchaseType", sql.VarChar, purchaseType);
      addP("Remarks", sql.VarChar, remarks);
      addP("JournalNo", sql.VarChar, d.JournalNo || "");
      addP("CreateUser", sql.VarChar, sysUser);
      addP("CreatedBy", sql.VarChar, sysUser);
      addP("CreateDate", sql.SmallDateTime, curDate);
      addP("ModifyUser", sql.VarChar, sysUser);
      addP("ModifiedBy", sql.VarChar, sysUser);
      addP("ModifyDate", sql.SmallDateTime, curDate);

      await insertReq.query(`
        INSERT INTO tblPurchase (${pC.join(', ')})
        VALUES (${pV.join(', ')})
      `);

      // 2. Direct insert into tblInventory
      const invReq = new sql.Request(transaction);
      const iC = [];
      const iV = [];
      const addI = (col, type, val) => {
        if (hasICol(col)) {
          iC.push(col);
          iV.push(`@inv_${col}`);
          invReq.input(`inv_${col}`, type, val);
        }
      };

      addI("InventoryCode", sql.VarChar, invCode);
      addI("Description", sql.VarChar, desc);
      addI("DiamondDescription", sql.VarChar, diamondDesc);
      addI("Size", sql.Numeric(18, 2), sizeVal);
      addI("Weight", sql.Numeric(18, 3), weight);
      addI("StoneWeight", sql.Numeric(18, 3), stoneWeight);
      addI("CostPerGram", sql.Numeric(18, 2), costPerGram);
      addI("PMC", sql.Numeric(18, 2), pmcCost);
      addI("SMC", sql.Numeric(18, 2), smcCost);
      addI("GoldLoss", sql.Numeric(18, 3), goldLoss);
      addI("TotalCost", sql.Numeric(18, 2), totalCost);
      addI("JobSheetNo", sql.VarChar, header.JobSheetNo || "");
      addI("PurchaseBillNo", sql.VarChar, billNoVal);
      addI("PurchaseDate", sql.SmallDateTime, curDate);
      addI("SupplierCode", sql.VarChar, header.SupplierCode || "");
      addI("SalesBillNo", sql.VarChar, "");
      addI("SalesDate", sql.SmallDateTime, null);
      addI("SalesCostPerGram", sql.Numeric(18, 2), 0);
      addI("SalesMC", sql.Numeric(18, 2), 0);
      addI("SalesAmount", sql.Numeric(18, 2), 0);
      addI("CategoryCode", sql.VarChar, categoryCode);
      addI("ClassCode", sql.VarChar, classCode);
      addI("CatalogueCode", sql.VarChar, catCode);
      addI("TrayId", sql.VarChar, trayId);
      addI("PMCType", sql.VarChar, pmcType);
      addI("SMCType", sql.VarChar, smcType);
      addI("OrderNo", sql.VarChar, header.OrderNo || d.OrderNo || "");
      addI("Remarks", sql.VarChar, remarks);
      addI("CreateUser", sql.VarChar, sysUser);
      addI("CreatedBy", sql.VarChar, sysUser);
      addI("CreateDate", sql.SmallDateTime, curDate);
      addI("ModifyUser", sql.VarChar, sysUser);
      addI("ModifiedBy", sql.VarChar, sysUser);
      addI("ModifyDate", sql.SmallDateTime, curDate);
      addI("LocationCode", sql.VarChar, locCodeVal);
      addI("SizeCode", sql.Numeric(18, 2), sizeVal);
      addI("SizeType", sql.VarChar, sizeType);
      addI("SalesPCS", sql.Bit, salesPcs);
      addI("QtyOnHand", sql.Int, pcs);
      addI("QtySold", sql.Int, 0);

      await invReq.query(`
        INSERT INTO tblInventory (${iC.join(', ')})
        VALUES (${iV.join(', ')})
      `);

      // 3. Direct insert into tblInventoryStock
      const stockReq = new sql.Request(transaction);
      const sC = [];
      const sV = [];
      const addS = (col, type, val) => {
        if (hasSCol(col)) {
          sC.push(col);
          sV.push(`@stk_${col}`);
          stockReq.input(`stk_${col}`, type, val);
        }
      };

      addS("LocationCode", sql.VarChar, locCodeVal);
      addS("InventoryCode", sql.VarChar, invCode);
      addS("Description", sql.VarChar, desc);
      addS("DiamondDescription", sql.VarChar, diamondDesc);
      addS("Weight", sql.Numeric(18, 3), weight);
      addS("PCS", sql.Numeric(18, 0), pcs);
      addS("TotalCost", sql.Numeric(18, 2), totalCost);
      addS("QtyOnHand", sql.Numeric(18, 0), pcs);
      addS("QtySold", sql.Numeric(18, 0), 0);
      addS("Status", sql.VarChar, 'IN');
      addS("CreateUser", sql.VarChar, sysUser);
      addS("CreatedBy", sql.VarChar, sysUser);
      addS("ModifyUser", sql.VarChar, sysUser);
      addS("ModifiedBy", sql.VarChar, sysUser);

      stockReq.input("chk_InvCode", sql.VarChar, invCode);
      stockReq.input("chk_LocCode", sql.VarChar, locCodeVal);

      await stockReq.query(`
        IF EXISTS (SELECT 1 FROM tblInventoryStock WHERE InventoryCode = @chk_InvCode AND LocationCode = @chk_LocCode)
        BEGIN
          UPDATE tblInventoryStock SET
            Description = @stk_Description,
            DiamondDescription = @stk_DiamondDescription,
            Weight = @stk_Weight,
            PCS = @stk_PCS,
            TotalCost = @stk_TotalCost,
            QtyOnHand = @stk_QtyOnHand,
            Status = 'IN',
            ${hasSCol('ModifyUser') ? 'ModifyUser = @stk_ModifyUser,' : ''}
            ${hasSCol('ModifiedBy') ? 'ModifiedBy = @stk_ModifiedBy,' : ''}
            ModifyDate = GETDATE()
          WHERE InventoryCode = @chk_InvCode AND LocationCode = @chk_LocCode
        END
        ELSE
        BEGIN
          INSERT INTO tblInventoryStock (${sC.join(', ')}${hasSCol('CreateDate') ? ', CreateDate' : ''}${hasSCol('ModifyDate') ? ', ModifyDate' : ''})
          VALUES (${sV.join(', ')}${hasSCol('CreateDate') ? ', GETDATE()' : ''}${hasSCol('ModifyDate') ? ', GETDATE()' : ''})
        END
      `);

      // 4. Record Inventory Movement (Status: 'PU')
      try {
        const movReq = new sql.Request(transaction);
        const mC = [
          "LocationCode", "InventoryCode", "Description", "Status", "RefNumber", "RefDate",
          "Qty", "Weight", "Size", "PMC", "SMC", "CostPerGram", "TotalCost", "SupplierCode"
        ];
        const mV = [
          "@LocationCode", "@InventoryCode", "@Description", "@Status", "@RefNumber", "@RefDate",
          "@Qty", "@Weight", "@Size", "@PMC", "@SMC", "@CostPerGram", "@TotalCost", "@SupplierCode"
        ];
        movReq.input("LocationCode", sql.VarChar, locCodeVal);
        movReq.input("InventoryCode", sql.VarChar, invCode);
        movReq.input("Description", sql.VarChar, desc);
        movReq.input("Status", sql.VarChar, "PU");
        movReq.input("RefNumber", sql.VarChar, billNoVal);
        movReq.input("RefDate", sql.SmallDateTime, curDate);
        movReq.input("Qty", sql.Int, pcs);
        movReq.input("Weight", sql.Numeric(18, 3), weight);
        movReq.input("Size", sql.Numeric(18, 2), sizeVal);
        movReq.input("PMC", sql.Numeric(18, 2), pmcCost);
        movReq.input("SMC", sql.Numeric(18, 2), smcCost);
        movReq.input("CostPerGram", sql.Numeric(18, 2), costPerGram);
        movReq.input("TotalCost", sql.Numeric(18, 2), totalCost);
        movReq.input("SupplierCode", sql.VarChar, header.SupplierCode || "");
        movReq.input("SysUser", sql.VarChar, sysUser);

        if (hasMCol("CreateUser")) { mC.push("CreateUser"); mV.push("@SysUser"); }
        if (hasMCol("CreatedBy")) { mC.push("CreatedBy"); mV.push("@SysUser"); }
        if (hasMCol("ModifyUser")) { mC.push("ModifyUser"); mV.push("@SysUser"); }
        if (hasMCol("ModifiedBy")) { mC.push("ModifiedBy"); mV.push("@SysUser"); }
        if (hasMCol("CreateDate")) { mC.push("CreateDate"); mV.push("GETDATE()"); }
        if (hasMCol("ModifyDate")) { mC.push("ModifyDate"); mV.push("GETDATE()"); }

        await movReq.query(`
          IF OBJECT_ID('tblInventoryMovement', 'U') IS NOT NULL
          BEGIN
            INSERT INTO tblInventoryMovement (${mC.join(', ')})
            VALUES (${mV.join(', ')})
          END
        `);
      } catch (_) {}

      // 5. IPM Bar/Coin Save
      if (d.IPM) {
        try {
          if (hasSP('sp_InventoryBarCoinSave')) {
            const ipmSpReq = new sql.Request(transaction);
            await ipmSpReq
              .input("InventoryCode", sql.VarChar, invCode)
              .input("MetalType", sql.VarChar, d.IPMType || "")
              .input("GSTType", sql.VarChar, d.IPMGSTType || "")
              .input("Purity", sql.VarChar, d.IPMPurity || "")
              .input("Refiner", sql.VarChar, d.IPMRefiner || "")
              .input("Country", sql.VarChar, d.IPMCountry || "")
              .input("BarNo", sql.VarChar, d.IPMBarNo || "")
              .input("UserId", sql.VarChar, sysUser)
              .execute("sp_InventoryBarCoinSave");
          } else {
            const ipmReq = new sql.Request(transaction);
            const bcC = ["InventoryCode", "MetalType", "GSTType", "Purity", "Refiner", "Country", "BarNo"];
            const bcV = ["@InventoryCode", "@MetalType", "@GSTType", "@Purity", "@Refiner", "@Country", "@BarNo"];
            ipmReq.input("InventoryCode", sql.VarChar, invCode);
            ipmReq.input("MetalType", sql.VarChar, d.IPMType || "");
            ipmReq.input("GSTType", sql.VarChar, d.IPMGSTType || "");
            ipmReq.input("Purity", sql.VarChar, d.IPMPurity || "");
            ipmReq.input("Refiner", sql.VarChar, d.IPMRefiner || "");
            ipmReq.input("Country", sql.VarChar, d.IPMCountry || "");
            ipmReq.input("BarNo", sql.VarChar, d.IPMBarNo || "");
            ipmReq.input("SysUser", sql.VarChar, sysUser);

            if (hasBCCol("CreateUser")) { bcC.push("CreateUser"); bcV.push("@SysUser"); }
            if (hasBCCol("CreatedBy")) { bcC.push("CreatedBy"); bcV.push("@SysUser"); }
            if (hasBCCol("ModifyUser")) { bcC.push("ModifyUser"); bcV.push("@SysUser"); }
            if (hasBCCol("ModifiedBy")) { bcC.push("ModifiedBy"); bcV.push("@SysUser"); }
            if (hasBCCol("CreateDate")) { bcC.push("CreateDate"); bcV.push("GETDATE()"); }
            if (hasBCCol("ModifyDate")) { bcC.push("ModifyDate"); bcV.push("GETDATE()"); }

            await ipmReq.query(`
              INSERT INTO tblInventoryBarCoin (${bcC.join(', ')})
              VALUES (${bcV.join(', ')})
            `);
          }
        } catch (_) {}
      }

      // 6. Additional descriptions
      if (d.AddlDesc && (d.Desc1 || d.Desc2 || d.Desc3 || d.Desc4 || d.Cost1 || d.Cost2 || d.Cost3 || d.Cost4)) {
        try {
          if (hasSP('sp_InventoryAddlDescSave')) {
            const addlSpReq = new sql.Request(transaction);
            await addlSpReq
              .input("InventoryCode", sql.VarChar, invCode)
              .input("JobSheetNo", sql.VarChar, header.JobSheetNo || "")
              .input("Desc1", sql.VarChar, d.Desc1 || "")
              .input("Desc2", sql.VarChar, d.Desc2 || "")
              .input("Desc3", sql.VarChar, d.Desc3 || "")
              .input("Desc4", sql.VarChar, d.Desc4 || "")
              .input("Cost1", sql.Decimal(18, 2), parseFloat(d.Cost1) || 0)
              .input("Cost2", sql.Decimal(18, 2), parseFloat(d.Cost2) || 0)
              .input("Cost3", sql.Decimal(18, 2), parseFloat(d.Cost3) || 0)
              .input("Cost4", sql.Decimal(18, 2), parseFloat(d.Cost4) || 0)
              .input("SellingPrice1", sql.Decimal(18, 2), parseFloat(d.SellingPrice1) || 0)
              .input("SellingPrice2", sql.Decimal(18, 2), parseFloat(d.SellingPrice2) || 0)
              .input("SellingPrice3", sql.Decimal(18, 2), parseFloat(d.SellingPrice3) || 0)
              .input("SellingPrice4", sql.Decimal(18, 2), parseFloat(d.SellingPrice4) || 0)
              .input("UserId", sql.VarChar, sysUser)
              .execute("sp_InventoryAddlDescSave");
          } else {
            const addlReq = new sql.Request(transaction);
            const aC = [
              "InventoryCode", "JobSheetNo", "Description1", "Description2", "Description3", "Description4",
              "Cost1", "Cost2", "Cost3", "Cost4", "SellingPrice1", "SellingPrice2", "SellingPrice3", "SellingPrice4"
            ];
            const aV = [
              "@InventoryCode", "@JobSheetNo", "@Description1", "@Description2", "@Description3", "@Description4",
              "@Cost1", "@Cost2", "@Cost3", "@Cost4", "@SellingPrice1", "@SellingPrice2", "@SellingPrice3", "@SellingPrice4"
            ];
            addlReq.input("InventoryCode", sql.VarChar, invCode);
            addlReq.input("JobSheetNo", sql.VarChar, header.JobSheetNo || "");
            addlReq.input("Description1", sql.VarChar, d.Desc1 || "");
            addlReq.input("Description2", sql.VarChar, d.Desc2 || "");
            addlReq.input("Description3", sql.VarChar, d.Desc3 || "");
            addlReq.input("Description4", sql.VarChar, d.Desc4 || "");
            addlReq.input("Cost1", sql.Numeric(18, 2), parseFloat(d.Cost1) || 0);
            addlReq.input("Cost2", sql.Numeric(18, 2), parseFloat(d.Cost2) || 0);
            addlReq.input("Cost3", sql.Numeric(18, 2), parseFloat(d.Cost3) || 0);
            addlReq.input("Cost4", sql.Numeric(18, 2), parseFloat(d.Cost4) || 0);
            addlReq.input("SellingPrice1", sql.Numeric(18, 2), parseFloat(d.SellingPrice1) || 0);
            addlReq.input("SellingPrice2", sql.Numeric(18, 2), parseFloat(d.SellingPrice2) || 0);
            addlReq.input("SellingPrice3", sql.Numeric(18, 2), parseFloat(d.SellingPrice3) || 0);
            addlReq.input("SellingPrice4", sql.Numeric(18, 2), parseFloat(d.SellingPrice4) || 0);
            addlReq.input("SysUser", sql.VarChar, sysUser);

            if (hasACol("CreateUser")) { aC.push("CreateUser"); aV.push("@SysUser"); }
            if (hasACol("CreatedBy")) { aC.push("CreatedBy"); aV.push("@SysUser"); }
            if (hasACol("ModifyUser")) { aC.push("ModifyUser"); aV.push("@SysUser"); }
            if (hasACol("ModifiedBy")) { aC.push("ModifiedBy"); aV.push("@SysUser"); }
            if (hasACol("CreateDate")) { aC.push("CreateDate"); aV.push("GETDATE()"); }
            if (hasACol("ModifyDate")) { aC.push("ModifyDate"); aV.push("GETDATE()"); }

            await addlReq.query(`
              INSERT INTO tblInventoryAddlDescription (${aC.join(', ')})
              VALUES (${aV.join(', ')})
            `);
          }
        } catch (_) {}
      }

      // 7. Update Order if Ordered is checked
      if (header.Ordered && (header.OrderNo || d.OrderNo)) {
        const orderNoVal = (header.OrderNo || d.OrderNo || "").trim();
        const orderInvCodeVal = (d.OrderInventoryCode || "").trim();
        try {
          // If stored procedure sp_UpdateInventoryOrderStatus exists, invoke it matching clsInventory.UpdateInventoryAndOrder
          if (hasSP('sp_UpdateInventoryOrderStatus')) {
            const spOrdReq = new sql.Request(transaction);
            await spOrdReq
              .input("InventoryCode", sql.VarChar, invCode)
              .input("OrderNo", sql.VarChar, orderNoVal)
              .input("OrderInventoryCode", sql.VarChar, orderInvCodeVal)
              .input("UserID", sql.VarChar, sysUser)
              .execute("sp_UpdateInventoryOrderStatus");
          } else {
            const updOrderReq = new sql.Request(transaction);
            updOrderReq
              .input("InventoryCode", sql.VarChar, invCode)
              .input("OrderNo", sql.VarChar, orderNoVal)
              .input("OrderInventoryCode", sql.VarChar, orderInvCodeVal)
              .input("SysUser", sql.VarChar, sysUser);

            const modODUserSet = [];
            if (hasODCol("ModifyUser")) modODUserSet.push("ModifyUser = @SysUser");
            if (hasODCol("ModifiedBy")) modODUserSet.push("ModifiedBy = @SysUser");
            const modODUserSql = modODUserSet.length > 0 ? `, ${modODUserSet.join(', ')}` : '';

            const modOHUserSet = [];
            if (hasOHCol("ModifyUser")) modOHUserSet.push("ModifyUser = @SysUser");
            if (hasOHCol("ModifiedBy")) modOHUserSet.push("ModifiedBy = @SysUser");
            const modOHUserSql = modOHUserSet.length > 0 ? `, ${modOHUserSet.join(', ')}` : '';

            if (orderInvCodeVal) {
              await updOrderReq.query(`
                UPDATE tblInventoryOrderDetail
                SET InventoryCode = @InventoryCode, ModifyDate = GETDATE()${modODUserSql}
                WHERE OrderNo = @OrderNo AND OrderInventoryCode = @OrderInventoryCode;
                UPDATE tblInventoryOrderHeader
                SET Status = 'R', ModifyDate = GETDATE()${modOHUserSql}
                WHERE OrderNo = @OrderNo;
              `);
            } else {
              await updOrderReq.query(`
                UPDATE tblInventoryOrderHeader
                SET Status = 'R', ModifyDate = GETDATE()${modOHUserSql}
                WHERE OrderNo = @OrderNo;
              `);
            }
          }
        } catch (_) {}
      }

      // 8. Update Trade-In if TradeIn inventory details provided
      if (d.TradeInInventoryCode && d.TradeInWeight) {
        const tiCodeVal = d.TradeInInventoryCode.trim();
        const tiWeightVal = parseFloat(d.TradeInWeight) || 0;
        try {
          // If stored procedure sp_UpdateTradeInDetails exists, invoke it matching clsTradeInIssue.UpdateTradeInDetails
          if (hasSP('sp_UpdateTradeInDetails')) {
            const spTiReq = new sql.Request(transaction);
            await spTiReq
              .input("IssueNo", sql.VarChar, (d.TradeIssueNo || "").trim())
              .input("InvoiceNo", sql.VarChar, (d.TradeInInvoice || billNoVal).trim())
              .input("InventoryCode", sql.VarChar, tiCodeVal)
              .input("TempPurWeight", sql.Decimal(18, 3), tiWeightVal)
              .input("Supplier", sql.VarChar, header.SupplierCode || "")
              .input("UserId", sql.VarChar, sysUser)
              .execute("sp_UpdateTradeInDetails");
          } else {
            const tiReq = new sql.Request(transaction);
            tiReq
              .input("TradeInCode", sql.VarChar, tiCodeVal)
              .input("WeightUsed", sql.Numeric(18, 3), tiWeightVal)
              .input("SysUser", sql.VarChar, sysUser);

            const modTIUserSet = [];
            if (hasTICol("ModifyUser")) modTIUserSet.push("ModifyUser = @SysUser");
            if (hasTICol("ModifiedBy")) modTIUserSet.push("ModifiedBy = @SysUser");
            const modTIUserSql = modTIUserSet.length > 0 ? `, ${modTIUserSet.join(', ')}` : '';

            await tiReq.query(`
              UPDATE tblTradeInDetail
              SET BalanceWeight = CASE 
                    WHEN BalanceWeight - @WeightUsed < 0 THEN 0 
                    ELSE BalanceWeight - @WeightUsed 
                  END,
                  ModifyDate = GETDATE()${modTIUserSql}
              WHERE InventoryCode = @TradeInCode
            `);
          }
        } catch (_) {}
      }
    }

    // Upsert tblJobSheetHeader matching clsDirectPurchase.JobSheetSave (sp_DPJobsheetSave with table fallback)
    if (header.JobSheetNo && header.JobSheetNo.trim() !== '') {
      const jsNo = header.JobSheetNo.trim();
      const pBillDate = curDate;
      const payableType = header.Payable === 'Gold' ? 'G' : header.Payable === 'Both' ? 'B' : 'C';
      const suppCode = (header.SupplierCode || "").trim();
      const currCode = (header.CurrencyCode || "").trim();
      const currRate = parseFloat(header.CurrencyRate) || 1;
      const catCode = details[0]?.CatalogueCode || "";
      const categoryCode = details[0]?.CategoryCode || "";
      const description = details[0]?.Description || details[0]?.InventoryDescription || "";
      const remarks = (header.Remarks || "").trim();
      const taelGST = header.GSTActive ? 'Y' : 'N';
      const taelPercent = parseFloat(header.TEALPercent) || 0;
      const taelRate = parseFloat(header.TEAL) || 0;
      const fixType = header.FixUnFix === 'Fix' ? 'Y' : 'N';

      const totalWeight = details.reduce((sum, item) => sum + (parseFloat(item.Weight) || 0), 0);
      const totalCost = details.reduce((sum, item) => sum + (parseFloat(item.TotalCost) || 0), 0);
      const totalMC = details.reduce((sum, item) => sum + (parseFloat(item.MC || item.PMCCost || item.PMC) || 0), 0);
      const totalPCS = details.reduce((sum, item) => sum + (parseInt(item.PCS, 10) || 1), 0);
      const totalGoldLoss = details.reduce((sum, item) => sum + (parseFloat(item.GoldLoss) || 0), 0);
      const gstAmount = parseFloat(header.GSTAmount) || 0;

      const jsReq = new sql.Request(transaction);
      const jC = [];
      const jV = [];
      const jU = [];
      const addJ = (col, type, val) => {
        if (hasJCol(col)) {
          jC.push(col);
          jV.push(`@js_${col}`);
          jU.push(`${col} = @js_${col}`);
          jsReq.input(`js_${col}`, type, val);
        }
      };

      addJ("JobSheetNo", sql.VarChar, jsNo);
      addJ("JobSheetDate", sql.SmallDateTime, curDate);
      addJ("PurchaseBillNo", sql.VarChar, billNoVal);
      addJ("PurchaseBillDate", sql.SmallDateTime, pBillDate);
      addJ("SupplierCode", sql.VarChar, suppCode);
      addJ("CurrencyCode", sql.VarChar, currCode);
      addJ("CurrencyRate", sql.Numeric(18, 6), currRate);
      addJ("TotalWeight24K", sql.Numeric(18, 3), totalWeight);
      addJ("TotalMC", sql.Numeric(18, 2), totalMC);
      addJ("TotalPCS", sql.Numeric(18, 0), totalPCS);
      addJ("TotalGoldLoss", sql.Numeric(18, 3), totalGoldLoss);
      addJ("CostPerGram", sql.Numeric(18, 2), parseFloat(header.CostPerGram) || 0);
      addJ("GST", sql.Numeric(18, 2), gstAmount);
      addJ("TotalCost", sql.Numeric(18, 2), totalCost);
      addJ("PaymentType", sql.VarChar, payableType);
      addJ("Remarks", sql.VarChar, remarks);
      addJ("FixStatus", sql.VarChar, fixType);
      addJ("TaelRate", sql.Numeric(18, 2), taelRate);
      addJ("TaelPercent", sql.Numeric(18, 4), taelPercent);
      addJ("TaelGST", sql.VarChar, taelGST);
      addJ("TranType", sql.VarChar, "DP");
      addJ("BillClose", sql.VarChar, "N");
      addJ("LocationCode", sql.VarChar, locCodeVal);
      addJ("ModifyUser", sql.VarChar, sysUser);
      addJ("ModifiedBy", sql.VarChar, sysUser);
      addJ("ModifyDate", sql.SmallDateTime, curDate);

      // Extra standard columns if present in tblJobSheetHeader
      addJ("TotalLocalCost", sql.Numeric(18, 2), totalCost);
      addJ("TotalForeignCost", sql.Numeric(18, 2), totalCost);
      addJ("ManualLocalCost", sql.Numeric(18, 2), totalCost);
      addJ("ManualForeignCost", sql.Numeric(18, 2), totalCost);
      addJ("ManualMC", sql.Numeric(18, 2), totalMC);
      addJ("ManualWeight", sql.Numeric(18, 3), totalWeight);
      addJ("ManualGoldLoss", sql.Numeric(18, 3), totalGoldLoss);

      jsReq.input("chk_JobSheetNo", sql.VarChar, jsNo);

      let insertCols = [...jC];
      let insertVals = [...jV];
      if (hasJCol("CreateUser")) {
        insertCols.push("CreateUser");
        insertVals.push("@js_CreateUser");
        jsReq.input("js_CreateUser", sql.VarChar, sysUser);
      }
      if (hasJCol("CreatedBy")) {
        insertCols.push("CreatedBy");
        insertVals.push("@js_CreatedBy");
        jsReq.input("js_CreatedBy", sql.VarChar, sysUser);
      }
      if (hasJCol("CreateDate")) {
        insertCols.push("CreateDate");
        insertVals.push("GETDATE()");
      }

      await jsReq.query(`
        IF EXISTS (SELECT 1 FROM tblJobSheetHeader WHERE JobSheetNo = @chk_JobSheetNo)
        BEGIN
          UPDATE tblJobSheetHeader SET
            ${jU.join(',\n            ')}
          WHERE JobSheetNo = @chk_JobSheetNo;
        END
        ELSE
        BEGIN
          INSERT INTO tblJobSheetHeader (${insertCols.join(', ')})
          VALUES (${insertVals.join(', ')});
          IF OBJECT_ID('tblCompany', 'U') IS NOT NULL
          BEGIN
            UPDATE tblCompany SET NextJobSheetNo = ISNULL(NextJobSheetNo, 0) + 1;
          END
        END
      `);

      // -------------------------------------------------------------
      // Insert / Update tblJobSheetDetail (Catalog & Category lines)
      // Grouping details by CatalogueCode + CategoryCode as in frmJobSheet & clsJobSheet.JobSheetDetailSave
      // -------------------------------------------------------------
      const jsDetailMap = new Map();
      for (const item of details) {
        const catCode = (item.CatalogueCode || "").trim();
        const catgCode = (item.CategoryCode || "").trim();
        const key = `${catCode}_${catgCode}`.toUpperCase();

        const wt = parseFloat(item.Weight) || 0;
        const mc = parseFloat(item.MC || item.PMCCost || item.PMC) || 0;
        const pcs = parseInt(item.PCS, 10) || 1;
        const gl = parseFloat(item.GoldLoss) || 0;
        const tc = parseFloat(item.TotalCost) || 0;
        const costG = parseFloat(item.CostPerGram) || (parseFloat(header.CostPerGram) || 0);
        const desc = (item.Description || item.InventoryDescription || "").trim();

        if (jsDetailMap.has(key)) {
          const existing = jsDetailMap.get(key);
          existing.Weight += wt;
          existing.MC += mc;
          existing.PCS += pcs;
          existing.GoldLoss += gl;
          existing.TotalCost += tc;
        } else {
          jsDetailMap.set(key, {
            JobSheetNo: jsNo,
            CatalogueCode: catCode,
            CategoryCode: catgCode,
            Description: desc,
            CostPerGram: costG,
            Weight: wt,
            MC: mc,
            PCS: pcs,
            GoldLoss: gl,
            TotalCost: tc,
          });
        }
      }

      // Execute insert into tblJobSheetDetail
      for (const [_, jdItem] of jsDetailMap) {
        const jdReq = new sql.Request(transaction);
        const jdC = [];
        const jdV = [];
        const addJD = (col, type, val) => {
          if (hasJDCol(col)) {
            jdC.push(col);
            jdV.push(`@jd_${col}`);
            jdReq.input(`jd_${col}`, type, val);
          }
        };

        addJD("JobSheetNo", sql.VarChar, jsNo);
        addJD("CatalogueCode", sql.VarChar, jdItem.CatalogueCode);
        addJD("CategoryCode", sql.VarChar, jdItem.CategoryCode);
        addJD("Description", sql.VarChar, jdItem.Description);
        addJD("CostPerGram", sql.Numeric(18, 2), jdItem.CostPerGram);
        addJD("Weight", sql.Numeric(18, 3), jdItem.Weight);
        addJD("MC", sql.Numeric(18, 2), jdItem.MC);
        addJD("PCS", sql.Numeric(18, 0), jdItem.PCS);
        addJD("GoldLoss", sql.Numeric(18, 3), jdItem.GoldLoss);
        addJD("TotalCost", sql.Numeric(18, 2), jdItem.TotalCost);
        addJD("CreateUser", sql.VarChar, sysUser);
        addJD("CreatedBy", sql.VarChar, sysUser);
        addJD("ModifyUser", sql.VarChar, sysUser);
        addJD("ModifiedBy", sql.VarChar, sysUser);

        if (hasJDCol("CreateDate")) {
          jdC.push("CreateDate");
          jdV.push("GETDATE()");
        }
        if (hasJDCol("ModifyDate")) {
          jdC.push("ModifyDate");
          jdV.push("GETDATE()");
        }

        await jdReq.query(`
          INSERT INTO tblJobSheetDetail (${jdC.join(', ')})
          VALUES (${jdV.join(', ')})
        `);
      }
    }

    await transaction.commit();
    return res.status(200).json({ success: true, message: "Direct purchase saved successfully" });
  } catch (err) {
    if (transaction) {
      try { await transaction.rollback(); } catch (_) {}
    }
    console.error("🔥 POST DIRECT PURCHASE ERROR:", err);
    console.error("🔥 ERROR STACK:", err.stack);
    return res.status(500).json({
      success: false,
      message: err.message || "Server Error",
      error: err.message,
      detail: err.originalError ? err.originalError.message : undefined,
      code: err.code || err.number
    });
  }
});

// ================= DELETE DIRECT PURCHASE =================
router.delete("/:purchaseBillNo", async (req, res) => {
  const pool = req.pool;
  const transaction = new sql.Transaction(pool);

  try {
    const { purchaseBillNo } = req.params;

    await transaction.begin();

    const request = new sql.Request(transaction);
    await request
      .input("PurchaseBillNo", sql.VarChar, purchaseBillNo)
      .query(`
        DELETE FROM tblPurchase WHERE PurchaseBillNo = @PurchaseBillNo;
        DELETE FROM tblInventory WHERE PurchaseBillNo = @PurchaseBillNo;
      `);

    await transaction.commit();
    return res.status(200).json({ success: true, message: "Direct purchase deleted successfully" });
  } catch (err) {
    await transaction.rollback();
    console.error("🔥 DELETE DIRECT PURCHASE ERROR:", err);
    return res.status(500).json({ success: false, message: "Server Error", error: err.message });
  }
});

// ================= DELETE SINGLE INVENTORY ITEM FROM DIRECT PURCHASE =================
router.delete("/:purchaseBillNo/item/:inventoryCode", async (req, res) => {
  const pool = req.pool;
  const transaction = new sql.Transaction(pool);

  try {
    const { purchaseBillNo, inventoryCode } = req.params;
    const invCode = inventoryCode.trim();

    // 1. Check if bill is closed or item exists
    const chkResult = await pool.request()
      .input("InventoryCode", sql.VarChar, invCode)
      .query(`
        SELECT 
          i.InventoryCode, 
          i.SalesBillNo, 
          i.CreateDate, 
          i.LocationCode,
          i.JobSheetNo,
          st.QtyOnHand,
          st.PCS,
          jh.BillClose,
          jh.TranType,
          loc.EODDate
        FROM tblInventory i
        LEFT JOIN tblInventoryStock st ON i.InventoryCode = st.InventoryCode AND i.LocationCode = st.LocationCode
        LEFT JOIN tblJobSheetHeader jh ON i.JobSheetNo = jh.JobSheetNo
        LEFT JOIN tblLocation loc ON i.LocationCode = loc.LocationCode
        WHERE i.InventoryCode = @InventoryCode
      `);

    if (chkResult.recordset.length > 0) {
      const item = chkResult.recordset[0];
      if (item.BillClose === 'Y') {
        return res.status(400).json({ success: false, message: "This bill is closed. Cannot delete!" });
      }
      if (item.TranType === 'JP') {
        return res.status(400).json({ success: false, message: "This is not a direct purchase bill. Cannot delete!" });
      }
      if (item.SalesBillNo && item.SalesBillNo.trim() !== '') {
        return res.status(400).json({ success: false, message: "Item sold. Cannot delete!" });
      }
      if (item.QtyOnHand != null && item.PCS != null && parseFloat(item.QtyOnHand) !== parseFloat(item.PCS)) {
        return res.status(400).json({ success: false, message: "Qty on hand is not the same as no. of pieces purchased. Cannot delete!" });
      }
      if (item.EODDate && item.CreateDate) {
        const eod = new Date(item.EODDate);
        const crDate = new Date(item.CreateDate);
        if (eod >= crDate) {
          return res.status(400).json({ success: false, message: "You cannot delete the Inventory after EOD. Please use purchase return module." });
        }
      }
    }

    await transaction.begin();

    const delReq = new sql.Request(transaction);
    delReq.input("PurchaseBillNo", sql.VarChar, purchaseBillNo.trim());
    delReq.input("InventoryCode", sql.VarChar, invCode);

    // Try executing stored procedure sp_DPInventorydelete matching clsDirectPurchase.PurchaseDelete
    let spDeleteExecuted = false;
    try {
      const spDelReq = new sql.Request(transaction);
      spDelReq
        .input("JobsheetNo", sql.VarChar, (chkResult.recordset[0]?.JobSheetNo || "").trim())
        .input("sinventorycode", sql.VarChar, invCode)
        .input("sInvoiceNo", sql.VarChar, purchaseBillNo.trim())
        .input("sSupplierCode", sql.VarChar, (req.query.supplierCode || "").trim())
        .input("sUserID", sql.VarChar, (req.query.userId || req.headers['x-user-id'] || "SYSTEM").trim());

      await spDelReq.execute("sp_DPInventorydelete");
      spDeleteExecuted = true;
    } catch (_) {
      // Fallback to table deletion
    }

    if (!spDeleteExecuted) {
      await delReq.query(`
        DELETE FROM tblPurchase WHERE PurchaseBillNo = @PurchaseBillNo AND InventoryCode = @InventoryCode;
        DELETE FROM tblInventory WHERE InventoryCode = @InventoryCode;
        DELETE FROM tblInventoryStock WHERE InventoryCode = @InventoryCode;
      `);
    }

    // If item was ordered, revert/update status via sp_UpdateStatusOnDelete
    if (req.query.orderNo) {
      try {
        const spOrdReq = new sql.Request(transaction);
        spOrdReq
          .input("OrderNo", sql.VarChar, req.query.orderNo.trim())
          .input("sinventorycode", sql.VarChar, invCode);
        await spOrdReq.execute("sp_UpdateStatusOnDelete");
      } catch (_) {}
    }

    await transaction.commit();
    return res.status(200).json({ success: true, message: `Item ${invCode} deleted successfully` });
  } catch (err) {
    if (transaction) {
      try { await transaction.rollback(); } catch (_) {}
    }
    console.error("🔥 DELETE INVENTORY ITEM ERROR:", err);
    return res.status(500).json({ success: false, message: "Server Error", error: err.message });
  }
});

// ================= BILL CLOSE FOR DIRECT PURCHASE WITH WEIGHT TOLERANCE =================
router.post("/billclose", async (req, res) => {
  try {
    const { jobSheetNo, invoiceNo, userId } = req.body;
    const targetNo = (jobSheetNo || invoiceNo || "").trim();
    if (!targetNo) {
      return res.status(400).json({ success: false, message: "Please enter the invoice no.!" });
    }
    const pool = req.pool;

    // 1. Fetch JobSheet details safely without failing on CROSS JOIN
    const jsRequest = pool.request();
    jsRequest.input("TargetNo", sql.VarChar, targetNo);
    if (invoiceNo && invoiceNo.trim() !== targetNo) {
      jsRequest.input("InvoiceNo", sql.VarChar, invoiceNo.trim());
    } else {
      jsRequest.input("InvoiceNo", sql.VarChar, targetNo);
    }

    const jsResult = await jsRequest.query(`
      SELECT TOP 1
        jh.JobSheetNo, 
        jh.PurchaseBillNo,
        jh.TotalWeight24K, 
        jh.BillClose, 
        jh.TranType
      FROM tblJobSheetHeader jh WITH (NOLOCK)
      WHERE jh.JobSheetNo = @TargetNo OR jh.PurchaseBillNo = @TargetNo OR jh.PurchaseBillNo = @InvoiceNo
    `);

    if (jsResult.recordset.length === 0) {
      // Check if bill exists in tblPurchase
      const purCheck = await pool.request()
        .input("TargetNo", sql.VarChar, targetNo)
        .query(`
          SELECT TOP 1 JobSheetNo, PurchaseBillNo, TranType 
          FROM tblPurchase WITH (NOLOCK) 
          WHERE JobSheetNo = @TargetNo OR PurchaseBillNo = @TargetNo
        `);

      if (purCheck.recordset.length === 0) {
        return res.status(404).json({ success: false, message: "Job sheet not found! Please save the purchase bill first." });
      }

      const foundJsNo = purCheck.recordset[0].JobSheetNo || targetNo;
      const closeUser = (userId || req.body.userId || req.headers['x-user-id'] || "SYSTEM").toString().trim();
      const schema = await getCachedSchemaCols(pool);
      const hasJCol = (c) => hasTableCol(schema, 'tblJobSheetHeader', c);
      const updateFields = ["BillClose = 'Y'", "ModifyDate = GETDATE()"];
      if (hasJCol("ModifyUser")) updateFields.push("ModifyUser = @UserId");
      if (hasJCol("ModifiedBy")) updateFields.push("ModifiedBy = @UserId");

      // Close directly in tblJobSheetHeader if present or create closing entry
      await pool.request()
        .input("JobSheetNo", sql.VarChar, foundJsNo)
        .input("UserId", sql.VarChar, closeUser)
        .query(`
          IF EXISTS (SELECT 1 FROM tblJobSheetHeader WHERE JobSheetNo = @JobSheetNo)
          BEGIN
            UPDATE tblJobSheetHeader SET ${updateFields.join(', ')} WHERE JobSheetNo = @JobSheetNo;
          END
        `);
      return res.status(200).json({ success: true, message: "Purchase bill closed!" });
    }

    const js = jsResult.recordset[0];
    const actualJsNo = (js.JobSheetNo || targetNo).trim();

    if ((js.BillClose || '').toUpperCase() === 'Y') {
      return res.status(400).json({ success: false, message: "This Job sheet bill is already closed!" });
    }
    if ((js.TranType || '').toUpperCase() === 'JP') {
      return res.status(400).json({ success: false, message: "This is not a direct purchase bill. Use job sheet purchase screen to close this bill!" });
    }

    // Safely retrieve PurWtVarPerc from tblCompany
    let purWtVarPerc = 5.0;
    try {
      const compRes = await pool.request().query("SELECT TOP 1 * FROM tblCompany WITH (NOLOCK)");
      if (compRes.recordset && compRes.recordset.length > 0) {
        const c = compRes.recordset[0];
        purWtVarPerc = parseFloat(c.PurWtVarPerc || c.PurWeightVarPerc || 5.0) || 5.0;
      }
    } catch (_) {}

    const dTotJobSheetWt = parseFloat(js.TotalWeight24K) || 0;

    // 2. Fetch total purchase weight
    const purResult = await pool.request()
      .input("JobSheetNo", sql.VarChar, actualJsNo)
      .query(`SELECT ISNULL(SUM(Weight), 0) as TotPurchaseWt FROM tblPurchase WITH (NOLOCK) WHERE JobSheetNo = @JobSheetNo`);

    const dTotPurchaseWt = parseFloat(purResult.recordset[0]?.TotPurchaseWt) || 0;
    const dWtDiff = Math.abs(dTotJobSheetWt - dTotPurchaseWt);
    const maxAllowedDiff = (purWtVarPerc * dTotJobSheetWt) / 100;

    if (dTotJobSheetWt > 0 && dTotPurchaseWt > 0 && dWtDiff > maxAllowedDiff) {
      return res.status(400).json({
        success: false,
        message: `Cannot close bill. The Inventory weight differs by more than ${purWtVarPerc.toFixed(2)}% of the job sheet weight!`
      });
    }

    // 3. Close the bill in tblJobSheetHeader
    const schema = await getCachedSchemaCols(pool);
    const hasJCol = (c) => hasTableCol(schema, 'tblJobSheetHeader', c);
    const closeUser = (userId || req.body.userId || req.headers['x-user-id'] || "SYSTEM").toString().trim();
    const updateFields = ["BillClose = 'Y'", "ModifyDate = GETDATE()"];
    if (hasJCol("ModifyUser")) updateFields.push("ModifyUser = @UserId");
    if (hasJCol("ModifiedBy")) updateFields.push("ModifiedBy = @UserId");

    await pool.request()
      .input("JobSheetNo", sql.VarChar, actualJsNo)
      .input("UserId", sql.VarChar, closeUser)
      .query(`
        UPDATE tblJobSheetHeader
        SET ${updateFields.join(', ')}
        WHERE JobSheetNo = @JobSheetNo;
      `);

    return res.status(200).json({ success: true, message: "Purchase bill closed!" });
  } catch (err) {
    console.error("🔥 DIRECT PURCHASE BILL CLOSE ERROR:", err);
    return res.status(500).json({ success: false, message: "Server Error", error: err.message });
  }
});

// ================= REPORT EXPORT ENDPOINTS =================
// POST /api/directpurchase/download-html
router.post("/download-html", async (req, res) => {
  try {
    const { generateReportHtml } = require("../utils/reportHtmlTemplate");
    const { title, columns, data, totals, companyInfo, fromDate, toDate, showPrintButton } = req.body;
    const html = await generateReportHtml({
      title: title || "Direct Purchase Report",
      columns,
      data,
      totals,
      companyInfo,
      fromDate,
      toDate,
      showPrintButton: showPrintButton ?? false,
      pool: req.pool,
    });
    res.setHeader("Content-Type", "text/html; charset=utf-8");
    res.send(html);
  } catch (err) {
    console.error("🔥 Direct Purchase Download HTML Error:", err);
    res.status(500).send("Error generating report HTML: " + err.message);
  }
});

// POST /api/directpurchase/export-excel
router.post("/export-excel", (req, res) => {
  const { handleExportExcel } = require("../utils/reportExportHandlers");
  handleExportExcel({ req, res, defaultTitle: "Direct_Purchase_Report" });
});

// POST /api/directpurchase/export-pdf
router.post("/export-pdf", (req, res) => {
  const { handleExportPdf } = require("../utils/reportExportHandlers");
  handleExportPdf({ req, res, defaultTitle: "Direct_Purchase_Report" });
});

module.exports = router;
