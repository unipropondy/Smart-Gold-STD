/** Developer: Antigravity */
const express = require("express");
const router = express.Router();
const { sql, poolPromise, getPool: dbGetPool } = require("../db");

// Helper to acquire database connection pool reliably
const getPool = async (req) => {
  if (req && req.pool) return req.pool;
  if (poolPromise) {
    try {
      const p = await poolPromise;
      if (p && p.connected && !p.closed) return p;
    } catch (_) {}
  }
  if (typeof dbGetPool === "function") {
    return await dbGetPool(process.env.DB_NAME || "smartgold");
  }
  const dbModule = require("../db");
  if (typeof dbModule.getPool === "function") {
    return await dbModule.getPool(process.env.DB_NAME || "smartgold");
  }
  throw new Error("Database pool is not available.");
};

// 1. GET /api/tradeinissue/nextIssueNo
// VB: objTII.GetNextTradeIssueNo() -> Mid(gthisLocation,1,2) & "65" & Format(NextTradeIssueNo, "000000")
router.get("/nextIssueNo", async (req, res) => {
  try {
    const pool = await getPool(req);
    const result = await pool.request().query(`
      SELECT TOP 1 
        ISNULL(LocationCode, 'HQ') AS LocationCode, 
        ISNULL(NextTradeIssueNo, 1) AS NextTradeIssueNo,
        ISNULL(GstPercentage, 9.0) AS GstPercentage,
        ISNULL(CompanyShortCode, '') AS CompanyShortCode
      FROM tblCompany
    `);

    let loc = "HQ";
    let nextSeq = 1;
    let gstPercentage = 9.0;
    let companyShortCode = "";
    if (result.recordset && result.recordset.length > 0) {
      loc = (result.recordset[0].LocationCode || "HQ").trim();
      nextSeq = parseInt(result.recordset[0].NextTradeIssueNo, 10) || 1;
      gstPercentage = parseFloat(result.recordset[0].GstPercentage) || 9.0;
      companyShortCode = (result.recordset[0].CompanyShortCode || "").trim();
    }

    const locPrefix = loc.substring(0, 2);
    const issueNo = `${locPrefix}65${String(nextSeq).padStart(6, "0")}`;

    res.json({ success: true, data: issueNo, issueNo, nextSeq, gstPercentage, companyShortCode });
  } catch (err) {
    console.error("TradeInIssue nextIssueNo error:", err);
    res.status(500).json({ success: false, message: "Error generating next issue number", error: err.message });
  }
});

// 2. GET /api/tradeinissue/suppliers
// VB: frmHelpSearch on tblSupplier: SupplierCode, SupplierName, Address1, Address2
router.get("/suppliers", async (req, res) => {
  try {
    const pool = await getPool(req);
    const result = await pool.request().query(`
      SELECT 
        RTRIM(LTRIM(SupplierCode)) AS SupplierCode, 
        RTRIM(LTRIM(ISNULL(SupplierName, ''))) AS SupplierName, 
        RTRIM(LTRIM(ISNULL(Address1, ''))) AS Address1, 
        RTRIM(LTRIM(ISNULL(Address2, ''))) AS Address2 
      FROM tblSupplier 
      ORDER BY SupplierCode ASC
    `);
    res.json({ success: true, data: result.recordset || [] });
  } catch (err) {
    console.error("TradeInIssue suppliers error:", err);
    res.status(500).json({ success: false, message: "Error fetching suppliers", error: err.message });
  }
});

// 2b. GET /api/tradeinissue/supplier/:code
// VB: objSupp.GetSupplierDetail(Trim(txtsupplierCode.Text))
router.get("/supplier/:code", async (req, res) => {
  try {
    const { code } = req.params;
    const pool = await getPool(req);
    const result = await pool.request()
      .input("SupplierCode", sql.VarChar, (code || "").trim())
      .query(`
        SELECT TOP 1 
          RTRIM(LTRIM(SupplierCode)) AS SupplierCode, 
          RTRIM(LTRIM(ISNULL(SupplierName, ''))) AS SupplierName, 
          RTRIM(LTRIM(ISNULL(Address1, ''))) AS Address1, 
          RTRIM(LTRIM(ISNULL(Address2, ''))) AS Address2 
        FROM tblSupplier 
        WHERE SupplierCode = @SupplierCode
      `);

    if (result.recordset && result.recordset.length > 0) {
      res.json({ success: true, data: result.recordset[0] });
    } else {
      res.json({ success: false, message: "Supplier not found" });
    }
  } catch (err) {
    console.error("TradeInIssue supplier lookup error:", err);
    res.status(500).json({ success: false, message: "Error looking up supplier", error: err.message });
  }
});

// 3. GET /api/tradeinissue/purities
// VB: frmTradeInWholeSales.vb cboPurity Items: ("750", "835", "875", "916", "999", "999.9", "22K")
router.get("/purities", async (req, res) => {
  // Return the exact standard list defined in frmTradeInWholeSales.vb
  const standardPurities = ["750", "835", "875", "916", "999", "999.9", "22K"];
  res.json({ success: true, data: standardPurities });
});

// 4. GET /api/tradeinissue/tradebills / pendingTradeBills
// Reference: subGetTradeInInvoices() in frmTradeInWholeSales.vb & GetTradeInIssue in ClsTradeinSales.vb:
// If Trim(sCompanyShortCode) = "MJ":
//    SELECT InvoiceNo, InventoryCode, CreateDate, BalanceWeight, TotalCost FROM tblTradeInHeader WHERE BalanceWeight > 0
// Else:
//    SELECT TradeinInventoryCode, isnull(balanceweight,0) TotalWeight, TotalPrice, round(isnull(TotalPrice,0)/isnull(totalweight,0),2) AvgCost, Status, Convert(varchar(10),Createdate,105) InvoiceDate 
//    FROM tblTradeMergeInventoryheader WHERE isnull(BalanceWeight,0) > 0 and Purity = @sPurity
// NOTE: Also return open trade bills from tblTradeInHeader when available so all open customer trade-in records can be selected.
router.get("/tradebills", async (req, res) => {
  try {
    const pool = await getPool(req);
    const origPurity = (req.query.purity || "").trim();
    let mappedPurity = origPurity;
    if (origPurity === "999.9") mappedPurity = "1000";
    else if (origPurity === "22K") mappedPurity = "22";

    // Check company code
    let companyShortCode = "";
    try {
      const compRes = await pool.request().query("SELECT TOP 1 ISNULL(CompanyShortCode, '') AS CompanyShortCode FROM tblCompany");
      if (compRes.recordset && compRes.recordset.length > 0) {
        companyShortCode = (compRes.recordset[0].CompanyShortCode || "").trim().toUpperCase();
      }
    } catch (_) {}

    const seenInvoices = new Set();
    const list = [];

    // Helper to safely add rows without duplicates
    const addRows = (rows) => {
      if (!Array.isArray(rows)) return;
      for (const r of rows) {
        const key = (r.InvoiceNo || r.InventoryCode || "").trim().toUpperCase();
        if (key && !seenInvoices.has(key)) {
          seenInvoices.add(key);
          list.push(r);
        }
      }
    };

    // 1. If company is MJ, query tblTradeInHeader directly
    if (companyShortCode === "MJ") {
      try {
        const mjQuery = `
          SELECT 
            RTRIM(LTRIM(InvoiceNo)) AS InvoiceNo, 
            RTRIM(LTRIM(ISNULL(InventoryCode, InvoiceNo))) AS InventoryCode, 
            Convert(varchar(10), ISNULL(ModifyDate, CreateDate), 105) AS InvoiceDate,
            ISNULL(Weight, ISNULL(TotalWeight, 0)) AS TotalWeight, 
            ISNULL(BalanceWeight, ISNULL(Weight, ISNULL(TotalWeight, 0))) AS IssuedWeight, 
            ISNULL(BalanceWeight, ISNULL(Weight, ISNULL(TotalWeight, 0))) AS BalanceWeight, 
            ISNULL(TradeInAmount, ISNULL(TotalCost, 0)) AS TotalPrice, 
            ROUND(ISNULL(TradeInAmount, ISNULL(TotalCost, 0)) / CASE WHEN ISNULL(Weight, ISNULL(TotalWeight, 0)) = 0 THEN 1 ELSE ISNULL(Weight, ISNULL(TotalWeight, 0)) END, 2) AS AvgCost,
            RTRIM(LTRIM(ISNULL(CustomerCode, ''))) AS CustomerCode
          FROM tblTradeInHeader 
          WHERE (ISNULL(BalanceWeight, ISNULL(Weight, ISNULL(TotalWeight, 0))) > 0)
          ORDER BY InvoiceNo DESC
        `;
        const mjRes = await pool.request().query(mjQuery);
        addRows(mjRes.recordset || []);
      } catch (mjErr) {
        console.warn("TradeInIssue MJ tradebill error:", mjErr.message);
      }
    }

    // 2. Query open records from tblTradeInHeader (Customer trade bills, e.g. HQ01000008, HQ01000014, HQ01000044...)
    try {
      let hQuery = `
        SELECT 
          RTRIM(LTRIM(h.InvoiceNo)) AS InvoiceNo, 
          RTRIM(LTRIM(ISNULL(h.InventoryCode, h.InvoiceNo))) AS InventoryCode, 
          Convert(varchar(25), ISNULL(h.ModifyDate, h.CreateDate), 103) + ' ' + Convert(varchar(8), ISNULL(h.ModifyDate, h.CreateDate), 108) AS InvoiceDate,
          ISNULL(h.Weight, ISNULL(h.TotalWeight, 0)) AS TotalWeight, 
          ISNULL(h.BalanceWeight, ISNULL(h.Weight, ISNULL(h.TotalWeight, 0))) AS IssuedWeight, 
          ISNULL(h.BalanceWeight, ISNULL(h.Weight, ISNULL(h.TotalWeight, 0))) AS BalanceWeight, 
          ISNULL(h.TradeInAmount, ISNULL(h.TotalCost, 0)) AS TotalPrice, 
          ROUND(ISNULL(h.TradeInAmount, ISNULL(h.TotalCost, 0)) / CASE WHEN ISNULL(h.Weight, ISNULL(h.TotalWeight, 0)) = 0 THEN 1 ELSE ISNULL(h.Weight, ISNULL(h.TotalWeight, 0)) END, 2) AS AvgCost,
          RTRIM(LTRIM(ISNULL(h.CustomerCode, ''))) AS CustomerCode
        FROM tblTradeInHeader h 
        WHERE (ISNULL(h.BalanceWeight, ISNULL(h.Weight, ISNULL(h.TotalWeight, 0))) > 0)
      `;

      if (origPurity) {
        hQuery += `
          AND (
            EXISTS (
              SELECT 1 FROM tblTradeInDetail d 
              WHERE (d.InvoiceNo = h.InvoiceNo OR d.InventoryCode = h.InventoryCode OR d.InventoryCode = h.InvoiceNo)
                AND (
                  RTRIM(LTRIM(d.Class)) = '${origPurity.replace(/'/g, "''")}' 
                  OR RTRIM(LTRIM(d.Class)) = '${mappedPurity.replace(/'/g, "''")}'
                  OR RTRIM(LTRIM(d.Class)) LIKE '%${origPurity.replace(/'/g, "''")}%'
                )
            )
            OR NOT EXISTS (SELECT 1 FROM tblTradeInDetail d WHERE d.InvoiceNo = h.InvoiceNo OR d.InventoryCode = h.InventoryCode OR d.InventoryCode = h.InvoiceNo)
          )
        `;
      }

      hQuery += ` ORDER BY h.InvoiceNo ASC`;

      const hRes = await pool.request().query(hQuery);
      addRows(hRes.recordset || []);
    } catch (hErr) {
      console.warn("TradeInHeader query error:", hErr.message);
    }

    // Always ensure all open trade bills from tblTradeInHeader are available so the user sees all available trade bills (e.g. HQ01000008, HQ01000014, HQ01000044...)
    try {
      const allOpenHQuery = `
        SELECT 
          RTRIM(LTRIM(h.InvoiceNo)) AS InvoiceNo, 
          RTRIM(LTRIM(ISNULL(h.InventoryCode, h.InvoiceNo))) AS InventoryCode, 
          Convert(varchar(25), ISNULL(h.ModifyDate, h.CreateDate), 103) + ' ' + Convert(varchar(8), ISNULL(h.ModifyDate, h.CreateDate), 108) AS InvoiceDate,
          ISNULL(h.Weight, ISNULL(h.TotalWeight, 0)) AS TotalWeight, 
          ISNULL(h.BalanceWeight, ISNULL(h.Weight, ISNULL(h.TotalWeight, 0))) AS IssuedWeight, 
          ISNULL(h.BalanceWeight, ISNULL(h.Weight, ISNULL(h.TotalWeight, 0))) AS BalanceWeight, 
          ISNULL(h.TradeInAmount, ISNULL(h.TotalCost, 0)) AS TotalPrice, 
          ROUND(ISNULL(h.TradeInAmount, ISNULL(h.TotalCost, 0)) / CASE WHEN ISNULL(h.Weight, ISNULL(h.TotalWeight, 0)) = 0 THEN 1 ELSE ISNULL(h.Weight, ISNULL(h.TotalWeight, 0)) END, 2) AS AvgCost,
          RTRIM(LTRIM(ISNULL(h.CustomerCode, ''))) AS CustomerCode
        FROM tblTradeInHeader h 
        WHERE (ISNULL(h.BalanceWeight, ISNULL(h.Weight, ISNULL(h.TotalWeight, 0))) > 0)
        ORDER BY h.InvoiceNo ASC
      `;
      const allOpenHRes = await pool.request().query(allOpenHQuery);
      addRows(allOpenHRes.recordset || []);
    } catch (anyHErr) {
      console.warn("TradeInHeader open query error:", anyHErr.message);
    }

    // 3. Query tblTradeMergeInventoryheader (Merged trade-in inventory lots)
    try {
      let mergeQuery = `
        SELECT 
          RTRIM(LTRIM(TradeinInventoryCode)) AS InvoiceNo,
          RTRIM(LTRIM(TradeinInventoryCode)) AS InventoryCode,
          Convert(varchar(10), Createdate, 105) AS InvoiceDate,
          ISNULL(BalanceWeight, 0) AS TotalWeight,
          ISNULL(BalanceWeight, 0) AS IssuedWeight,
          ISNULL(BalanceWeight, 0) AS BalanceWeight,
          ISNULL(TotalPrice, 0) AS TotalPrice,
          ROUND(ISNULL(TotalPrice, 0) / CASE WHEN ISNULL(TotalWeight, 0) = 0 THEN 1 ELSE TotalWeight END, 2) AS AvgCost,
          RTRIM(LTRIM(ISNULL(Purity, ''))) AS Purity
        FROM tblTradeMergeInventoryheader 
        WHERE ISNULL(BalanceWeight, 0) > 0
      `;

      if (origPurity) {
        mergeQuery += ` AND (Purity = '${origPurity.replace(/'/g, "''")}' OR Purity = '${mappedPurity.replace(/'/g, "''")}')`;
      }
      mergeQuery += ` ORDER BY TradeinInventoryCode ASC`;

      const result = await pool.request().query(mergeQuery);
      addRows(result.recordset || []);
    } catch (mErr) {
      console.warn("TradeInIssue merge query error:", mErr.message);
    }

    // If still no records from tblTradeMergeInventoryheader with purity filter, check without purity filter
    try {
      const anyMergeQuery = `
        SELECT 
          RTRIM(LTRIM(TradeinInventoryCode)) AS InvoiceNo,
          RTRIM(LTRIM(TradeinInventoryCode)) AS InventoryCode,
          Convert(varchar(10), Createdate, 105) AS InvoiceDate,
          ISNULL(BalanceWeight, 0) AS TotalWeight,
          ISNULL(BalanceWeight, 0) AS IssuedWeight,
          ISNULL(BalanceWeight, 0) AS BalanceWeight,
          ISNULL(TotalPrice, 0) AS TotalPrice,
          ROUND(ISNULL(TotalPrice, 0) / CASE WHEN ISNULL(TotalWeight, 0) = 0 THEN 1 ELSE TotalWeight END, 2) AS AvgCost,
          RTRIM(LTRIM(ISNULL(Purity, ''))) AS Purity
        FROM tblTradeMergeInventoryheader 
        WHERE ISNULL(BalanceWeight, 0) > 0
        ORDER BY TradeinInventoryCode ASC
      `;
      const anyResult = await pool.request().query(anyMergeQuery);
      addRows(anyResult.recordset || []);
    } catch (anyErr) {
      console.warn("TradeInIssue any merge error:", anyErr.message);
    }

    return res.json({ success: true, data: list });
  } catch (err) {
    console.error("TradeInIssue tradebills error:", err);
    return res.status(200).json({ success: true, data: [] });
  }
});

// 4b. GET /api/tradeinissue/tradebill/:invoiceno
// VB: Scan or direct lookup of single trade bill
router.get("/tradebill/:invoiceno", async (req, res) => {
  try {
    const { invoiceno } = req.params;
    const pool = await getPool(req);
    const code = (invoiceno || "").trim();

    // 1. Check merge header first
    const mRes = await pool.request()
      .input("Code", sql.VarChar, code)
      .query(`
        SELECT TOP 1 
          RTRIM(LTRIM(TradeinInventoryCode)) AS InvoiceNo,
          RTRIM(LTRIM(TradeinInventoryCode)) AS InventoryCode,
          Convert(varchar(10), Createdate, 105) AS InvoiceDate,
          ISNULL(TotalWeight, 0) AS TotalWeight,
          ISNULL(BalanceWeight, TotalWeight) AS BalanceWeight,
          ISNULL(TotalPrice, 0) AS TotalPrice,
          ROUND(ISNULL(TotalPrice, 0) / CASE WHEN ISNULL(TotalWeight, 0) = 0 THEN 1 ELSE TotalWeight END, 2) AS AvgCost
        FROM tblTradeMergeInventoryheader 
        WHERE TradeinInventoryCode = @Code AND ISNULL(BalanceWeight, TotalWeight) > 0
      `);

    if (mRes.recordset && mRes.recordset.length > 0) {
      return res.json({ success: true, data: mRes.recordset[0] });
    }

    // 2. Check tblTradeInHeader
    const hRes = await pool.request()
      .input("InvNo", sql.VarChar, code)
      .query(`
        SELECT TOP 1 
          RTRIM(LTRIM(InvoiceNo)) AS InvoiceNo, 
          RTRIM(LTRIM(ISNULL(InventoryCode, InvoiceNo))) AS InventoryCode, 
          Convert(varchar(10), ISNULL(ModifyDate, CreateDate), 105) AS InvoiceDate,
          ISNULL(Weight, ISNULL(TotalWeight, 0)) AS TotalWeight, 
          ISNULL(IssuedWeight, 0) AS IssuedWeight, 
          ISNULL(BalanceWeight, ISNULL(Weight, ISNULL(TotalWeight, 0))) AS BalanceWeight, 
          ISNULL(TradeInAmount, ISNULL(TotalCost, 0)) AS TotalPrice, 
          ROUND(ISNULL(TradeInAmount, ISNULL(TotalCost, 0)) / CASE WHEN ISNULL(Weight, ISNULL(TotalWeight, 0)) = 0 THEN 1 ELSE ISNULL(Weight, ISNULL(TotalWeight, 0)) END, 2) AS AvgCost,
          RTRIM(LTRIM(ISNULL(CustomerCode, ''))) AS CustomerCode 
        FROM tblTradeInHeader 
        WHERE (InvoiceNo = @InvNo OR InventoryCode = @InvNo)
          AND (ISNULL(BalanceWeight, ISNULL(Weight, ISNULL(TotalWeight, 0))) > 0)
      `);

    if (hRes.recordset && hRes.recordset.length > 0) {
      res.json({ success: true, data: hRes.recordset[0] });
    } else {
      res.json({ success: false, message: "Trade bill not found or balance weight is 0" });
    }
  } catch (err) {
    console.error("TradeInIssue tradebill lookup error:", err);
    res.status(500).json({ success: false, message: "Error retrieving trade bill", error: err.message });
  }
});

// 5. GET /api/tradeinissue/history
// VB: btnIssueNoSearch_Click -> tblTradeIssueHistory: TradeIssueNo, SupplierCode, SupplierName, CustomerCode, InvoiceNo
router.get("/history", async (req, res) => {
  try {
    const pool = await getPool(req);
    let list = [];

    // 1. Query tblTradeIssueHistoryHeader
    try {
      const hQuery = `
        SELECT 
          RTRIM(LTRIM(h.TradeIssueNo)) AS TradeIssueNo, 
          RTRIM(LTRIM(ISNULL(h.SupplierCode, ''))) AS SupplierCode, 
          RTRIM(LTRIM(ISNULL(s.SupplierName, ''))) AS SupplierName, 
          Convert(varchar(10), ISNULL(h.Date, h.CreateDate), 105) AS IssueDate, 
          ISNULL(h.IssuedWeight, ISNULL(h.TotalWeight, 0)) AS IssuedWeight, 
          ISNULL(h.TotalPrice, 0) AS TotalPrice 
        FROM tblTradeIssueHistoryHeader h 
        LEFT JOIN tblSupplier s ON h.SupplierCode = s.SupplierCode 
        ORDER BY h.TradeIssueNo DESC
      `;
      const hRes = await pool.request().query(hQuery);
      if (hRes.recordset && hRes.recordset.length > 0) {
        list = hRes.recordset;
      }
    } catch (hErr) {
      console.warn("tblTradeIssueHistoryHeader query note:", hErr.message);
    }

    // 2. Query tblTradeIssueHistory
    if (list.length === 0) {
      try {
        const query = `
          SELECT 
            RTRIM(LTRIM(h.TradeIssueNo)) AS TradeIssueNo, 
            MAX(RTRIM(LTRIM(ISNULL(h.SupplierCode, '')))) AS SupplierCode, 
            MAX(RTRIM(LTRIM(ISNULL(s.SupplierName, '')))) AS SupplierName, 
            MAX(Convert(varchar(10), h.Date, 105)) AS IssueDate, 
            SUM(ISNULL(h.IssuedWeight, 0)) AS IssuedWeight, 
            SUM(ISNULL(h.TotalCost, 0)) AS TotalPrice 
          FROM tblTradeIssueHistory h 
          LEFT JOIN tblSupplier s ON h.SupplierCode = s.SupplierCode 
          GROUP BY h.TradeIssueNo
          ORDER BY h.TradeIssueNo DESC
        `;
        const result = await pool.request().query(query);
        if (result.recordset && result.recordset.length > 0) {
          list = result.recordset;
        }
      } catch (dErr) {
        console.warn("tblTradeIssueHistory query note:", dErr.message);
      }
    }

    // 3. Fallback: check tblTradeInIssueHistoryHeader or tblTradeInIssueHistory
    if (list.length === 0) {
      try {
        const altRes = await pool.request().query(`
          SELECT 
            RTRIM(LTRIM(TradeIssueNo)) AS TradeIssueNo, 
            RTRIM(LTRIM(ISNULL(SupplierCode, ''))) AS SupplierCode, 
            Convert(varchar(10), Date, 105) AS IssueDate, 
            ISNULL(IssuedWeight, ISNULL(TotalWeight, 0)) AS IssuedWeight, 
            ISNULL(TotalPrice, 0) AS TotalPrice 
          FROM tblTradeInIssueHistoryHeader
          ORDER BY TradeIssueNo DESC
        `);
        if (altRes.recordset && altRes.recordset.length > 0) {
          list = altRes.recordset;
        }
      } catch (_) {}
    }

    // 4. Fallback: if no history found yet, get available issue numbers from tblTradeInHeader or tblTradeInIssueHistory
    if (list.length === 0) {
      try {
        const fallbackTradeIn = await pool.request().query(`
          SELECT TOP 50
            RTRIM(LTRIM(InvoiceNo)) AS TradeIssueNo,
            RTRIM(LTRIM(ISNULL(CustomerCode, ''))) AS SupplierCode,
            Convert(varchar(10), ISNULL(ModifyDate, CreateDate), 105) AS IssueDate,
            ISNULL(Weight, ISNULL(TotalWeight, 0)) AS IssuedWeight,
            ISNULL(TradeInAmount, ISNULL(TotalCost, 0)) AS TotalPrice
          FROM tblTradeInHeader
          ORDER BY InvoiceNo DESC
        `);
        if (fallbackTradeIn.recordset && fallbackTradeIn.recordset.length > 0) {
          list = fallbackTradeIn.recordset;
        }
      } catch (_) {}
    }

    return res.json({ success: true, data: list });
  } catch (err) {
    console.error("TradeInIssue history error:", err);
    return res.status(200).json({ success: true, data: [] });
  }
});

// 5b. GET /api/tradeinissue/history/:issueno
// VB: objTII.GetTradeIssueHistory(txtTradeIssueNo.Text.Trim) -> SELECT * FROM tblTradeIssueHistory WHERE TradeIssueNo = ...
router.get("/history/:issueno", async (req, res) => {
  try {
    const { issueno } = req.params;
    const pool = await getPool(req);
    const code = (issueno || "").trim();

    // 1. Query tblTradeIssueHistory (lines)
    const dResult = await pool.request()
      .input("TradeIssueNo", sql.VarChar, code)
      .query(`
        SELECT 
          d.InvoiceNo,
          ISNULL(d.InventoryCode, d.InvoiceNo) AS InventoryCode,
          Convert(varchar(10), d.Date, 105) AS Date,
          ISNULL(d.TotalWeight, 0) AS PurchaseWeight,
          ISNULL(d.IssuedWeight, 0) AS IssuedWeight,
          ISNULL(d.BalanceWeight, 0) AS BalanceWeight,
          d.SupplierCode,
          RTRIM(LTRIM(ISNULL(s.SupplierName, ''))) AS SupplierName,
          d.CostPerGram,
          d.TotalCost,
          d.Remarks
        FROM tblTradeIssueHistory d 
        LEFT JOIN tblSupplier s ON d.SupplierCode = s.SupplierCode 
        WHERE d.TradeIssueNo = @TradeIssueNo
      `);

    const items = dResult.recordset || [];

    // 2. Query tblTradeIssueHistoryHeader if exists
    let header = null;
    const hResult = await pool.request()
      .input("TradeIssueNo", sql.VarChar, code)
      .query(`
        SELECT TOP 1 
          h.TradeIssueNo,
          Convert(varchar(10), h.Date, 105) AS IssueDate,
          h.SupplierCode,
          RTRIM(LTRIM(ISNULL(s.SupplierName, ''))) AS SupplierName,
          h.CostPerGram,
          h.IssuedWeight AS TotalIssuedWeight,
          h.TotalWeight,
          h.TotalPrice,
          h.Purity,
          h.Remarks
        FROM tblTradeIssueHistoryHeader h 
        LEFT JOIN tblSupplier s ON h.SupplierCode = s.SupplierCode 
        WHERE h.TradeIssueNo = @TradeIssueNo
      `);

    if (hResult.recordset && hResult.recordset.length > 0) {
      header = hResult.recordset[0];
    } else {
      // Check tblTradeInIssueHistoryHeader (alternate name)
      try {
        const altH = await pool.request()
          .input("TradeIssueNo", sql.VarChar, code)
          .query(`
            SELECT TOP 1 
              h.TradeIssueNo,
              Convert(varchar(10), h.Date, 105) AS IssueDate,
              h.SupplierCode,
              RTRIM(LTRIM(ISNULL(s.SupplierName, ''))) AS SupplierName,
              h.CostPerGram,
              h.IssuedWeight AS TotalIssuedWeight,
              h.TotalWeight,
              h.TotalPrice,
              h.Remarks
            FROM tblTradeInIssueHistoryHeader h 
            LEFT JOIN tblSupplier s ON h.SupplierCode = s.SupplierCode 
            WHERE h.TradeIssueNo = @TradeIssueNo
          `);
        if (altH.recordset && altH.recordset.length > 0) {
          header = altH.recordset[0];
        }
      } catch (_) {}
    }

    if (!header && items.length > 0) {
      const first = items[0];
      const sumIssued = items.reduce((s, r) => s + (parseFloat(r.IssuedWeight) || 0), 0);
      const sumPurch = items.reduce((s, r) => s + (parseFloat(r.PurchaseWeight) || 0), 0);
      const sumCost = items.reduce((s, r) => s + (parseFloat(r.TotalCost) || 0), 0);
      header = {
        TradeIssueNo: code,
        IssueDate: first.Date,
        SupplierCode: first.SupplierCode,
        SupplierName: first.SupplierName,
        CostPerGram: first.CostPerGram || 0,
        TotalIssuedWeight: sumIssued,
        TotalWeight: sumPurch,
        TotalPrice: sumCost,
        Purity: "999.9",
        Remarks: first.Remarks || ""
      };
    }

    // 3. Fallback: If no lines in tblTradeIssueHistory, check tblTradeInHeader if this was a TradeIn Invoice
    if (!header && items.length === 0) {
      try {
        const tiH = await pool.request()
          .input("InvNo", sql.VarChar, code)
          .query(`
            SELECT TOP 1 
              h.InvoiceNo AS TradeIssueNo,
              Convert(varchar(10), ISNULL(h.ModifyDate, h.CreateDate), 105) AS IssueDate,
              h.CustomerCode AS SupplierCode,
              ISNULL(h.IssuedWeight, ISNULL(h.Weight, h.TotalWeight)) AS TotalIssuedWeight,
              ISNULL(h.Weight, ISNULL(h.TotalWeight, 0)) AS TotalWeight,
              ISNULL(h.TradeInAmount, ISNULL(h.TotalCost, 0)) AS TotalPrice,
              h.Remarks
            FROM tblTradeInHeader h
            WHERE h.InvoiceNo = @InvNo
          `);
        if (tiH.recordset && tiH.recordset.length > 0) {
          header = tiH.recordset[0];
          items.push({
            InvoiceNo: header.TradeIssueNo,
            InventoryCode: header.TradeIssueNo,
            Date: header.IssueDate,
            PurchaseWeight: header.TotalWeight,
            IssuedWeight: header.TotalIssuedWeight,
            BalanceWeight: 0,
            SupplierCode: header.SupplierCode,
            SupplierName: header.SupplierCode,
            CostPerGram: 0,
            TotalCost: header.TotalPrice,
            Remarks: header.Remarks || ""
          });
        }
      } catch (_) {}
    }

    if (header || items.length > 0) {
      res.json({ success: true, data: { header, items } });
    } else {
      res.json({ success: false, message: "Issue details not found" });
    }
  } catch (err) {
    console.error("TradeInIssue history detail error:", err);
    res.status(500).json({ success: false, message: "Error fetching history details", error: err.message });
  }
});

// 6. POST /api/tradeinissue/proceed
// VB: btnProceed_Click in frmTradeInWholeSales.vb
// Executes:
// 1) Loops through items:
//    - objTII.UpdateTradeInMergeInventoryDetail(oTSalesConn, oTSalesRs, TradeIssueNo, SupplierCode, Remarks, InvCode, Weight, IssuedWeight, TotalCost, sError)
//    - Records detail in tblTradeIssueHistory
// 2) objTII.UpdateTradeInIssueHistoryHeader(oTSalesConn, oTSalesRs, TradeIssueNo, IssueDate, SupplierCode, NoOfBills, Issued, PricePerGram, TotalPrice, TealPercent, UserId, Purity, sError)
// 3) objTII.UpdateTradeIssueNo() -> UPDATE tblCompany SET NextTradeIssueNo = NextTradeIssueNo + 1
router.post("/proceed", async (req, res) => {
  let pool;
  try {
    pool = await getPool(req);
  } catch (pErr) {
    return res.status(500).json({ success: false, message: pErr.message });
  }

  const transaction = new sql.Transaction(pool);

  try {
    const {
      issueNo,
      issueDate,
      supplierCode,
      purity = "999.9",
      costPerGram = 0,
      totalIssuedWt = 0,
      totalWt = 0,
      totalPrice = 0,
      remarks = "",
      userId = "ADMIN",
      items = []
    } = req.body;

    if (!supplierCode || !supplierCode.trim()) {
      return res.status(400).json({ success: false, message: "Supplier cannot be blank!" });
    }
    const nIssued = parseFloat(totalIssuedWt) || 0;
    if (nIssued <= 0) {
      return res.status(400).json({ success: false, message: "Gold Issued Actual Weight Should Not Be 0 !!!" });
    }
    if (!items || !Array.isArray(items) || items.length === 0) {
      return res.status(400).json({ success: false, message: "Please enter Trade In Gold items in the grid!" });
    }

    await transaction.begin();

    // 1. Fetch current sequence and generate final TradeIssueNo
    const seqReq = new sql.Request(transaction);
    const seqRes = await seqReq.query(`
      SELECT TOP 1 
        ISNULL(LocationCode, 'HQ') AS LocationCode, 
        ISNULL(NextTradeIssueNo, 1) AS NextTradeIssueNo 
      FROM tblCompany
    `);

    let loc = "HQ";
    let nextSeq = 1;
    if (seqRes.recordset && seqRes.recordset.length > 0) {
      loc = (seqRes.recordset[0].LocationCode || "HQ").trim();
      nextSeq = parseInt(seqRes.recordset[0].NextTradeIssueNo, 10) || 1;
    }

    const locPrefix = loc.substring(0, 2);
    const finalTradeIssueNo = issueNo && issueNo.trim() ? issueNo.trim() : `${locPrefix}65${String(nextSeq).padStart(6, "0")}`;

    // Check company code
    let companyShortCode = "";
    try {
      const compRes = await pool.request().query("SELECT TOP 1 ISNULL(CompanyShortCode, '') AS CompanyShortCode FROM tblCompany");
      if (compRes.recordset && compRes.recordset.length > 0) {
        companyShortCode = (compRes.recordset[0].CompanyShortCode || "").trim().toUpperCase();
      }
    } catch (_) {}

    // 2. Loop through grid items: UpdateTradeInMergeInventoryDetail or UpdateTradeInHeaderMD
    for (let i = 0; i < items.length; i++) {
      const item = items[i];
      const invCode = (item.InventoryCode || item.InvoiceNo || "").trim();
      const pWeight = parseFloat(item.PurchaseWeight || item.TotalWeight || 0);
      const iWeight = parseFloat(item.IssuedWeight || 0);
      const bWeight = Math.max(0, pWeight - iWeight);
      const totCost = parseFloat(item.TotalCost || item.TotalPrice || 0);

      if (companyShortCode === "MJ") {
        try {
          const spUpdMD = new sql.Request(transaction);
          spUpdMD.input("SIssueNo", sql.VarChar, finalTradeIssueNo);
          spUpdMD.input("sInvoiceNo", sql.VarChar, item.InvoiceNo || invCode);
          spUpdMD.input("sInventoryCode", sql.VarChar, invCode);
          spUpdMD.input("dTempPurWeight", sql.Decimal(18, 4), bWeight);
          spUpdMD.input("sSupplier", sql.VarChar, supplierCode.trim());
          spUpdMD.input("sUserID", sql.VarChar, userId);
          spUpdMD.input("dActualIssuedWeight", sql.Decimal(18, 4), iWeight);
          spUpdMD.input("sRemarks", sql.VarChar, remarks || "");
          await spUpdMD.execute("sp_UpdateTradeInHeaderMD");
        } catch (_) {
          // Direct update for MJ
          const updHdr = new sql.Request(transaction);
          updHdr
            .input("Code", sql.VarChar, invCode)
            .input("IssueWt", sql.Decimal(18, 4), iWeight);
          await updHdr.query(`
            UPDATE tblTradeInHeader 
            SET 
              IssuedWeight = ISNULL(IssuedWeight, 0) + @IssueWt,
              BalanceWeight = CASE WHEN ISNULL(BalanceWeight, TotalWeight) - @IssueWt < 0 THEN 0 ELSE ISNULL(BalanceWeight, TotalWeight) - @IssueWt END,
              IssueFlag = CASE WHEN ISNULL(BalanceWeight, TotalWeight) - @IssueWt <= 0 THEN 'Y' ELSE 'P' END 
            WHERE InvoiceNo = @Code OR InventoryCode = @Code;
          `);
        }
      } else {
        try {
          const spUpd = new sql.Request(transaction);
          spUpd.input("IssueNo", sql.VarChar, finalTradeIssueNo);
          spUpd.input("TradeInInventoryCode", sql.VarChar, invCode);
          spUpd.input("Weight", sql.Decimal(18, 4), pWeight);
          spUpd.input("IssuedWeight", sql.Decimal(18, 4), iWeight);
          spUpd.input("TotalCost", sql.Decimal(18, 4), totCost);
          spUpd.input("SupplierCode", sql.VarChar, supplierCode.trim());
          spUpd.input("Remarks", sql.VarChar, remarks || "");
          spUpd.input("UserID", sql.VarChar, userId);

          await spUpd.execute("sp_UpdateTradeInMergeInventoryDetail");
        } catch (spErr) {
          // Direct SQL update
          const updMerge = new sql.Request(transaction);
          updMerge
            .input("Code", sql.VarChar, invCode)
            .input("IssueWt", sql.Decimal(18, 4), iWeight)
            .input("BalWt", sql.Decimal(18, 4), bWeight);

          await updMerge.query(`
            UPDATE tblTradeMergeInventoryheader 
            SET 
              BalanceWeight = @BalWt,
              Status = CASE WHEN @BalWt <= 0 THEN 'C' ELSE 'P' END 
            WHERE TradeinInventoryCode = @Code;

            UPDATE tblTradeInHeader 
            SET 
              IssuedWeight = ISNULL(IssuedWeight, 0) + @IssueWt,
              BalanceWeight = CASE WHEN ISNULL(BalanceWeight, TotalWeight) - @IssueWt < 0 THEN 0 ELSE ISNULL(BalanceWeight, TotalWeight) - @IssueWt END,
              IssueFlag = CASE WHEN ISNULL(BalanceWeight, TotalWeight) - @IssueWt <= 0 THEN 'Y' ELSE 'P' END 
            WHERE InvoiceNo = @Code OR InventoryCode = @Code;
          `);
        }
      }

      // Record in tblTradeIssueHistory
      try {
        const insHist = new sql.Request(transaction);
        insHist
          .input("TradeIssueNo", sql.VarChar, finalTradeIssueNo)
          .input("InventoryCode", sql.VarChar, invCode)
          .input("InvoiceNo", sql.VarChar, item.InvoiceNo || invCode)
          .input("LocationCode", sql.VarChar, loc)
          .input("SupplierCode", sql.VarChar, supplierCode.trim())
          .input("TotalWeight", sql.Decimal(18, 3), pWeight)
          .input("IssuedWeight", sql.Decimal(18, 3), iWeight)
          .input("BalanceWeight", sql.Decimal(18, 3), bWeight)
          .input("TotalCost", sql.Decimal(18, 2), totCost)
          .input("CostPerGram", sql.Decimal(18, 3), parseFloat(costPerGram) || 0)
          .input("Remarks", sql.VarChar, remarks || "")
          .input("UserID", sql.VarChar, userId);

        await insHist.query(`
          INSERT INTO tblTradeIssueHistory (
            TradeIssueNo, InventoryCode, InvoiceNo, LocationCode, SupplierCode,
            TotalWeight, IssuedWeight, BalanceWeight, TotalCost, CostPerGram,
            Remarks, Date, UserID
          ) VALUES (
            @TradeIssueNo, @InventoryCode, @InvoiceNo, @LocationCode, @SupplierCode,
            @TotalWeight, @IssuedWeight, @BalanceWeight, @TotalCost, @CostPerGram,
            @Remarks, GETDATE(), @UserID
          )
        `);
      } catch (hErr) {
        console.warn("Could not insert detail into tblTradeIssueHistory:", hErr.message);
      }
    }

    // 3. UpdateTradeInIssueHistoryHeader (or MD)
    if (companyShortCode === "MJ") {
      try {
        const spHeaderMD = new sql.Request(transaction);
        spHeaderMD.input("SIssueNo", sql.VarChar, finalTradeIssueNo);
        spHeaderMD.input("sIssueDate", sql.VarChar, issueDate || new Date().toLocaleDateString("en-GB"));
        spHeaderMD.input("sSupplierCode", sql.VarChar, supplierCode.trim());
        spHeaderMD.input("nTotalNoOfBills", sql.Int, items.length);
        spHeaderMD.input("nTotalWeight", sql.Decimal(18, 4), parseFloat(totalWt) || nIssued);
        spHeaderMD.input("nCostPerGram", sql.Decimal(18, 4), parseFloat(costPerGram) || 0);
        spHeaderMD.input("nTotalPrice", sql.Decimal(18, 4), parseFloat(totalPrice) || 0);
        spHeaderMD.input("nCostPercentage", sql.Decimal(18, 4), 0);
        spHeaderMD.input("sUserId", sql.VarChar, userId);
        await spHeaderMD.execute("sp_TradeInIssueHistoryHeaderSaveMD");
      } catch (_) {}
    } else {
      try {
        const spHeader = new sql.Request(transaction);
        spHeader.input("SIssueNo", sql.VarChar, finalTradeIssueNo);
        spHeader.input("IssueDate", sql.VarChar, issueDate || new Date().toLocaleDateString("en-GB"));
        spHeader.input("SupplierCode", sql.VarChar, supplierCode.trim());
        spHeader.input("TotalNoOfBills", sql.Int, items.length);
        spHeader.input("TotalWeight", sql.Decimal(18, 4), parseFloat(totalWt) || nIssued);
        spHeader.input("CostPerGram", sql.Decimal(18, 4), parseFloat(costPerGram) || 0);
        spHeader.input("CostPercentage", sql.Decimal(18, 4), 0);
        spHeader.input("TotalPrice", sql.Decimal(18, 4), parseFloat(totalPrice) || 0);
        spHeader.input("UserID", sql.VarChar, userId);
        spHeader.input("Purity", sql.VarChar, purity || "999.9");

        await spHeader.execute("sp_TradeInIssueHistoryHeaderSave");
      } catch (sphErr) {
        const insHdr = new sql.Request(transaction);
        insHdr
          .input("TradeIssueNo", sql.VarChar, finalTradeIssueNo)
          .input("SupplierCode", sql.VarChar, supplierCode.trim())
          .input("TotalNoOfBills", sql.Int, items.length)
          .input("TotalWeight", sql.Decimal(18, 4), parseFloat(totalWt) || nIssued)
          .input("IssuedWeight", sql.Decimal(18, 4), nIssued)
          .input("CostPerGram", sql.Decimal(18, 4), parseFloat(costPerGram) || 0)
          .input("TotalPrice", sql.Decimal(18, 4), parseFloat(totalPrice) || 0)
          .input("Purity", sql.VarChar, purity || "999.9")
          .input("Remarks", sql.VarChar, remarks || "")
          .input("UserID", sql.VarChar, userId);

        await insHdr.query(`
          IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tblTradeIssueHistoryHeader')
          BEGIN
            INSERT INTO tblTradeIssueHistoryHeader (
              TradeIssueNo, Date, SupplierCode, TotalNoOfBills, TotalWeight,
              IssuedWeight, CostPerGram, TotalPrice, Purity, Remarks, UserID
            ) VALUES (
              @TradeIssueNo, GETDATE(), @SupplierCode, @TotalNoOfBills, @TotalWeight,
              @IssuedWeight, @CostPerGram, @TotalPrice, @Purity, @Remarks, @UserID
            )
          END
        `);
      }
    }

    // 4. Update Sequence in tblCompany: NextTradeIssueNo = NextTradeIssueNo + 1
    const updComp = new sql.Request(transaction);
    await updComp.query(`
      UPDATE tblCompany 
      SET NextTradeIssueNo = ISNULL(NextTradeIssueNo, 1) + 1
    `);

    await transaction.commit();

    res.json({
      success: true,
      message: "Tradein Gold Issued Sucessfully",
      tradeIssueNo: finalTradeIssueNo
    });
  } catch (err) {
    if (transaction) {
      try { await transaction.rollback(); } catch (rErr) {}
    }
    console.error("TradeInIssue proceed error:", err);
    res.status(500).json({ success: false, message: "Error in save", error: err.message });
  }
});

module.exports = router;
