/** Developer: Antigravity */
const express = require("express");
const { sql } = require("../db");
const router = express.Router();

// Helper to acquire database connection pool reliably
const getPool = async (req) => {
  if (req && req.pool) return req.pool;
  const dbModule = require("../db");
  if (typeof dbModule.getPool === "function") {
    return await dbModule.getPool(process.env.DB_NAME || "smartgold");
  }
  if (dbModule.poolPromise) {
    return await dbModule.poolPromise;
  }
  throw new Error("Database pool is not available.");
};

// 1. GET Next Gold Bar Issue Number
// VB: fncGoldBarIssueNo -> objGoldBarIssue.GetUpdateGoldBarIssue -> EXEC sp_GetUpdateGoldBarIssue / tblCompany
router.get("/nextIssueNo", async (req, res) => {
  try {
    const pool = await getPool(req);
    let nextIssueNo = "00000001";
    let companyShortCode = "";
    let companyName = "SMART GOLD";

    try {
      const spRes = await pool.request().query("EXEC sp_GetUpdateGoldBarIssue");
      if (spRes.recordset && spRes.recordset.length > 0) {
        nextIssueNo = String(spRes.recordset[0].NextGoldBarIssueNo || "").trim();
      }
    } catch (spErr) {
      // Fallback to tblCompany or tblGoldBarIssued
      const compRes = await pool.request().query(`
        SELECT TOP 1 
          ISNULL(NextGoldBarIssueNo, 1) AS NextGoldBarIssueNo,
          ISNULL(CompanyShortCode, '') AS CompanyShortCode,
          ISNULL(CompanyName, 'SMART GOLD') AS CompanyName
        FROM tblCompany
      `);
      if (compRes.recordset && compRes.recordset.length > 0) {
        const row = compRes.recordset[0];
        const num = parseInt(row.NextGoldBarIssueNo, 10) || 1;
        nextIssueNo = String(num).padStart(8, "0");
        companyShortCode = (row.CompanyShortCode || "").trim();
        companyName = (row.CompanyName || "SMART GOLD").trim();
      }
    }

    return res.status(200).json({
      success: true,
      data: nextIssueNo,
      issueNo: nextIssueNo,
      companyShortCode,
      companyName,
    });
  } catch (err) {
    console.error("🔥 GET Next Gold Bar Issue No Error:", err);
    return res.status(500).json({ success: false, message: "Error fetching next Issue No", error: err.message });
  }
});

// 2. GET Supplier List (LOV)
// VB: tblSupplier (SupplierCode, SupplierName, Address1, Address2)
router.get("/suppliers", async (req, res) => {
  try {
    const pool = await getPool(req);
    const result = await pool.request().query(`
      SELECT 
        RTRIM(SupplierCode) AS SupplierCode,
        RTRIM(SupplierName) AS SupplierName,
        RTRIM(ISNULL(Address1, '')) AS Address1,
        RTRIM(ISNULL(Address2, '')) AS Address2
      FROM tblSupplier
      ORDER BY SupplierName ASC, SupplierCode ASC
    `);
    return res.status(200).json({ success: true, data: result.recordset || [] });
  } catch (err) {
    console.error("🔥 GET Suppliers Error:", err);
    return res.status(500).json({ success: false, message: "Error fetching suppliers", error: err.message });
  }
});

// 3. GET Supplier By Code
router.get("/supplier/:code", async (req, res) => {
  try {
    const pool = await getPool(req);
    const code = (req.params.code || "").trim();
    const result = await pool.request()
      .input("SupplierCode", sql.VarChar, code)
      .query(`
        SELECT TOP 1 
          RTRIM(SupplierCode) AS SupplierCode,
          RTRIM(SupplierName) AS SupplierName,
          RTRIM(ISNULL(Address1, '')) AS Address1,
          RTRIM(ISNULL(Address2, '')) AS Address2
        FROM tblSupplier
        WHERE SupplierCode = @SupplierCode
      `);
    if (result.recordset && result.recordset.length > 0) {
      return res.status(200).json({ success: true, data: result.recordset[0] });
    }
    return res.status(404).json({ success: false, message: "Supplier not found" });
  } catch (err) {
    console.error("🔥 GET Supplier Error:", err);
    return res.status(500).json({ success: false, message: "Error fetching supplier", error: err.message });
  }
});

// 4. GET Unissued Invoices for Supplier (Trade-In / PurchaseFix Invoices)
// VB: objTIS.GetTradeInInvoiceWholeSale(txtSupplierCode1.Text.Trim, SInvoiceNo)
// Table: tblPurchaseFix where suppliercode = @SupplierCode and (BalanceWeight - PurchaseFix - issuedweight) > 0
router.get("/invoices", async (req, res) => {
  try {
    const pool = await getPool(req);
    const supplierCode = (req.query.supplierCode || "").trim();
    if (!supplierCode) {
      return res.status(400).json({ success: false, message: "Supplier Code is required" });
    }

    const qry = `
      SELECT 
        RTRIM(InvoiceNo) AS InvoiceNo,
        CONVERT(varchar, InvoiceDate, 103) AS InvoiceDate,
        RTRIM(SupplierCode) AS SupplierCode,
        ROUND((ISNULL(BalanceWeight, 0) - ISNULL(PurchaseFix, 0) - ISNULL(IssuedWeight, 0)), 3) AS PurchaseWeight,
        ROUND((ISNULL(BalanceWeight, 0) - ISNULL(PurchaseFix, 0) - ISNULL(IssuedWeight, 0)), 3) AS IssuedWeight
      FROM tblPurchaseFix 
      WHERE SupplierCode = @SupplierCode 
        AND (ISNULL(BalanceWeight, 0) - ISNULL(PurchaseFix, 0) - ISNULL(IssuedWeight, 0)) > 0
      ORDER BY InvoiceDate DESC, InvoiceNo DESC
    `;

    const result = await pool.request()
      .input("SupplierCode", sql.VarChar, supplierCode)
      .query(qry);

    return res.status(200).json({ success: true, data: result.recordset || [] });
  } catch (err) {
    console.error("🔥 GET Invoices Error:", err);
    return res.status(500).json({ success: false, message: "Error fetching invoices", error: err.message });
  }
});

// 5. GET Trade-in Balance details for selected invoices
// VB: objTIS.GetTradeInBalanceSales(sInvNo, sInvCode, txtSupplierCode1.Text)
router.post("/tradein-balance", async (req, res) => {
  try {
    const pool = await getPool(req);
    const { supplierCode, invoiceNos = [] } = req.body;
    if (!supplierCode || !invoiceNos.length) {
      return res.status(200).json({ success: true, data: [] });
    }

    const reqSql = pool.request();
    reqSql.input("SupplierCode", sql.VarChar, supplierCode.trim());

    // Clean invoice numbers for SQL IN query safely
    const sanitizedInvoices = invoiceNos.map((inv, idx) => {
      const pName = `inv_${idx}`;
      reqSql.input(pName, sql.VarChar, String(inv).trim());
      return `@${pName}`;
    }).join(", ");

    const qry = `
      SELECT 
        '' AS JobsheetNo,
        RTRIM(InvoiceNo) AS PurchaseBillNo,
        CONVERT(varchar, InvoiceDate, 103) AS PurchaseBillDate,
        RTRIM(SupplierCode) AS SupplierCode,
        ROUND((ISNULL(BalanceWeight, 0) - ISNULL(PurchaseFix, 0) - ISNULL(IssuedWeight, 0)), 3) AS TotalWeight24K,
        CONVERT(varchar, ModifyDate, 103) AS ModifyDate
      FROM tblPurchaseFix 
      WHERE SupplierCode = @SupplierCode 
        AND InvoiceNo IN (${sanitizedInvoices})
      ORDER BY InvoiceNo ASC
    `;

    const result = await reqSql.query(qry);
    return res.status(200).json({ success: true, data: result.recordset || [] });
  } catch (err) {
    console.error("🔥 GET TradeIn Balance Error:", err);
    return res.status(500).json({ success: false, message: "Error fetching balance", error: err.message });
  }
});

// 6. GET 24K Gold Bar Inventory Stock LOV
// VB: vwInventoryStock WHERE ClassCode = '999.9' and QtyOnHand > 0
router.get("/inventory-stock", async (req, res) => {
  try {
    const pool = await getPool(req);
    const result = await pool.request().query(`
      SELECT 
        RTRIM(InventoryCode) AS InventoryCode,
        RTRIM(Description) AS Description,
        ROUND(ISNULL(Weight, 0), 3) AS Weight,
        ROUND(ISNULL(CostPerGram, 0), 2) AS CostPerGram,
        ISNULL(QtyOnHand, 0) AS QtyOnHand,
        ISNULL(Pcs, 1) AS Pcs,
        ROUND(ISNULL(TotalCost, 0), 2) AS TotalCost,
        RTRIM(ISNULL(PurchaseBillNo, '')) AS PurchaseBillNo,
        RTRIM(ISNULL(ClassCode, '999.9')) AS ClassCode
      FROM vwInventoryStock 
      WHERE (ClassCode = '999.9' OR ClassCode = '24K' OR ClassCode = '999')
        AND ISNULL(QtyOnHand, 0) > 0
      ORDER BY InventoryCode ASC
    `);
    return res.status(200).json({ success: true, data: result.recordset || [] });
  } catch (err) {
    console.error("🔥 GET Inventory Stock Error:", err);
    return res.status(500).json({ success: false, message: "Error fetching inventory stock", error: err.message });
  }
});

// 7. GET Single Inventory Detail
// VB: clsGoldBarPurchase.GetInventoryDetail(sInventoryCode)
router.get("/inventory/:code", async (req, res) => {
  try {
    const pool = await getPool(req);
    const code = (req.params.code || "").trim();
    const result = await pool.request()
      .input("InventoryCode", sql.VarChar, code)
      .query(`
        SELECT TOP 1
          RTRIM(InventoryCode) AS InventoryCode,
          RTRIM(Description) AS Description,
          ROUND(ISNULL(Weight, 0), 3) AS Weight,
          ROUND(ISNULL(CostPerGram, 0), 2) AS CostPerGram,
          ISNULL(QtyOnHand, 0) AS QtyOnHand,
          ISNULL(Pcs, 1) AS Pcs,
          ROUND(ISNULL(TotalCost, 0), 2) AS TotalCost,
          RTRIM(ISNULL(ClassCode, '999.9')) AS ClassCode
        FROM vwInventoryStock 
        WHERE InventoryCode = @InventoryCode AND ISNULL(QtyOnHand, 0) > 0
      `);

    if (result.recordset && result.recordset.length > 0) {
      const inv = result.recordset[0];
      // Get Bar No from tblInventoryBarCoin
      const barRes = await pool.request()
        .input("InventoryCode", sql.VarChar, code)
        .query(`SELECT TOP 1 RTRIM(BarNo) AS BarNo FROM tblInventoryBarCoin WHERE InventoryCode = @InventoryCode ORDER BY BarNo ASC`);
      const barNo = (barRes.recordset && barRes.recordset.length > 0) ? barRes.recordset[0].BarNo : "";
      return res.status(200).json({ success: true, data: { ...inv, BarNo: barNo } });
    }
    return res.status(404).json({ success: false, message: "No Stock for this Inventory or not found!" });
  } catch (err) {
    console.error("🔥 GET Inventory Error:", err);
    return res.status(500).json({ success: false, message: "Error fetching inventory details", error: err.message });
  }
});

// 8. GET Gold Bar Issue History (LOV)
// VB: tblGoldBarIssued (IssueNo, PDate, SupplierCode, IssuedWeight)
router.get("/history", async (req, res) => {
  try {
    const pool = await getPool(req);
    const result = await pool.request().query(`
      SELECT 
        RTRIM(i.IssueNo) AS IssueNo,
        CONVERT(varchar, i.PDate, 103) AS PDate,
        RTRIM(i.SupplierCode) AS SupplierCode,
        RTRIM(s.SupplierName) AS SupplierName,
        ROUND(ISNULL(i.IssuedWeight, 0), 3) AS IssuedWeight,
        ROUND(ISNULL(i.PurchaseWeight, 0), 3) AS PurchaseWeight
      FROM tblGoldBarIssued i
      LEFT JOIN tblSupplier s ON i.SupplierCode = s.SupplierCode
      ORDER BY i.PDate DESC, i.IssueNo DESC
    `);
    return res.status(200).json({ success: true, data: result.recordset || [] });
  } catch (err) {
    console.error("🔥 GET Gold Bar Issue History Error:", err);
    return res.status(500).json({ success: false, message: "Error fetching history", error: err.message });
  }
});

// 9. GET Specific Gold Bar Issue by IssueNo (for loading / re-printing)
router.get("/history/:issueNo", async (req, res) => {
  try {
    const pool = await getPool(req);
    const issueNo = (req.params.issueNo || "").trim();

    // 1. Header
    const headRes = await pool.request()
      .input("IssueNo", sql.VarChar, issueNo)
      .query(`
        SELECT TOP 1
          RTRIM(i.IssueNo) AS IssueNo,
          CONVERT(varchar, i.PDate, 103) AS PDate,
          RTRIM(i.SupplierCode) AS SupplierCode,
          RTRIM(s.SupplierName) AS SupplierName,
          ROUND(ISNULL(i.IssuedWeight, 0), 3) AS IssuedWeight,
          ROUND(ISNULL(i.PurchaseWeight, 0), 3) AS PurchaseWeight,
          RTRIM(ISNULL(i.Remarks, '')) AS Remarks
        FROM tblGoldBarIssued i
        LEFT JOIN tblSupplier s ON i.SupplierCode = s.SupplierCode
        WHERE i.IssueNo = @IssueNo
      `);

    if (!headRes.recordset || headRes.recordset.length === 0) {
      return res.status(404).json({ success: false, message: "Gold Bar Issue not found" });
    }

    const header = headRes.recordset[0];

    // 2. Invoices (Top Grid)
    const invRes = await pool.request()
      .input("IssueNo", sql.VarChar, issueNo)
      .query(`
        SELECT 
          RTRIM(ISNULL(PBillNo, '')) AS PurchaseBillNo,
          CONVERT(varchar, PDate, 103) AS InvoiceDate,
          'TRADE-IN' AS TranType,
          ROUND(ISNULL(PurchaseWeight, 0), 3) AS TotalWeight24K,
          ROUND(ISNULL(IssuedWeight, 0), 3) AS IssuedWeight
        FROM tblGoldBarIssuedInvoice
        WHERE IssueNo = @IssueNo
        ORDER BY PBillNo ASC
      `);

    // 3. Purchase Items (Bottom Grid)
    const itemRes = await pool.request()
      .input("IssueNo", sql.VarChar, issueNo)
      .query(`
        SELECT 
          RTRIM(InventoryCode) AS InventoryCode,
          RTRIM(Description) AS Description,
          ROUND(ISNULL(Weight, 0), 3) AS Weight,
          ISNULL(Qty, 1) AS Qty,
          ROUND(ISNULL(CostPerGram, 0), 2) AS CostPerGram,
          ROUND(ISNULL(TotalCost, 0), 2) AS TotalCost,
          RTRIM(ISNULL(BarNo, '')) AS BarNo
        FROM tblGoldBarIssuedPurchase
        WHERE IssueNo = @IssueNo
        ORDER BY InventoryCode ASC
      `);

    return res.status(200).json({
      success: true,
      header,
      invoices: invRes.recordset || [],
      items: itemRes.recordset || [],
    });
  } catch (err) {
    console.error("🔥 GET Gold Bar Issue Details Error:", err);
    return res.status(500).json({ success: false, message: "Error fetching details", error: err.message });
  }
});

// 10. SAVE Gold Bar Issue Transaction (Insert / Proceed)
// VB: btnSave_Click in frmGoldBarIssue_AJ.vb
router.post("/save", async (req, res) => {
  let transaction;
  try {
    const pool = await getPool(req);
    const {
      issueNo,
      issueDate, // DD/MM/YYYY
      supplierCode,
      issuedWeight, // (Val(txtIssued2.Text))
      netIssuedWeight, // (Val(txtNetIssue1.Text))
      remarks = "",
      userId = "ADMIN",
      invoices = [],
      items = [],
    } = req.body;

    // Validations matching VB.NET
    if (!supplierCode || !supplierCode.trim()) {
      return res.status(400).json({ success: false, message: "Select the Supplier Code!" });
    }
    if (!invoices || invoices.length === 0) {
      return res.status(400).json({ success: false, message: "Please select at least one invoice!" });
    }
    if (!items || items.length === 0) {
      return res.status(400).json({ success: false, message: "Please add at least one Gold Bar item to issue!" });
    }

    const nIssued = parseFloat(issuedWeight) || 0;
    const nNet = parseFloat(netIssuedWeight) || 0;
    if (Math.abs(nIssued - nNet) > 0.001) {
      return res.status(400).json({ success: false, message: "Issued Weight and Invoice Weight Not Tally!" });
    }

    // Determine target issue number
    let targetIssueNo = (issueNo || "").trim();
    if (!targetIssueNo) {
      try {
        const spRes = await pool.request().query("EXEC sp_GetUpdateGoldBarIssue");
        if (spRes.recordset && spRes.recordset.length > 0) {
          targetIssueNo = String(spRes.recordset[0].NextGoldBarIssueNo || "").trim();
        }
      } catch (_) {
        const compRes = await pool.request().query("SELECT TOP 1 ISNULL(NextGoldBarIssueNo, 1) AS NextGoldBarIssueNo FROM tblCompany");
        const nextN = compRes.recordset && compRes.recordset.length > 0 ? compRes.recordset[0].NextGoldBarIssueNo : 1;
        targetIssueNo = String(nextN).padStart(8, "0");
      }
    }

    transaction = new sql.Transaction(pool);
    await transaction.begin();

    // Format SQL Date
    // issueDate format DD/MM/YYYY
    let pDateIso = new Date().toISOString();
    if (issueDate && typeof issueDate === "string") {
      const parts = issueDate.split("/");
      if (parts.length === 3) {
        pDateIso = new Date(`${parts[2]}-${parts[1]}-${parts[0]}`).toISOString();
      }
    }

    // 1. Execute sp_GoldBarIssued or Insert into tblGoldBarIssued
    try {
      const spReq = new sql.Request(transaction);
      await spReq
        .input("IssueNo", sql.VarChar(50), targetIssueNo)
        .input("PBillNo", sql.VarChar(50), "")
        .input("PDate", sql.DateTime, pDateIso)
        .input("SupplierCode", sql.VarChar(50), supplierCode.trim())
        .input("PurchaseWeight", sql.Decimal(18, 3), 0)
        .input("PurchaseCost", sql.Decimal(18, 2), 0)
        .input("TempPurchaseWeight", sql.Decimal(18, 3), nIssued)
        .input("IssuedWeight", sql.Decimal(18, 3), nNet)
        .input("InvoiceNo", sql.VarChar(50), "")
        .input("InvoiceDate", sql.DateTime, pDateIso)
        .input("ClassCode", sql.VarChar(50), "24K-999")
        .input("PaidCost", sql.Decimal(18, 2), 0)
        .input("TranType", sql.VarChar(50), "G")
        .input("UserId", sql.VarChar(50), userId)
        .query(`
          IF EXISTS (SELECT * FROM sysobjects WHERE name='sp_GoldBarIssued' AND xtype='P')
          BEGIN
            EXEC sp_GoldBarIssued @IssueNo, @PBillNo, @PDate, @SupplierCode, @PurchaseWeight, @PurchaseCost, @TempPurchaseWeight, @IssuedWeight, @InvoiceNo, @InvoiceDate, @ClassCode, @PaidCost, @TranType, @UserId
          END
          ELSE
          BEGIN
            INSERT INTO tblGoldBarIssued (IssueNo, PDate, SupplierCode, PurchaseWeight, PurchaseCost, IssuedWeight, ClassCode, Remarks, CreateUser, CreateDate)
            VALUES (@IssueNo, @PDate, @SupplierCode, @TempPurchaseWeight, @PurchaseCost, @IssuedWeight, @ClassCode, '${remarks.replace(/'/g, "''")}', @UserId, GETDATE())
          END
        `);
    } catch (spErr) {
      console.warn("Execute sp_GoldBarIssued fallback to table insert:", spErr.message);
    }

    // 2. Loop grdGoldIssue -> sp_GoldBarIssuedInvoice & Update tblPurchaseFix
    for (const inv of invoices) {
      const invNo = (inv.PurchaseBillNo || inv.InvoiceNo || "").trim();
      const pWeight = parseFloat(inv.TotalWeight24K || inv.PurchaseWeight || 0);
      const iWeight = parseFloat(inv.IssuedWeight || pWeight);

      const invReq = new sql.Request(transaction);
      await invReq
        .input("IssueNo", sql.VarChar(50), targetIssueNo)
        .input("PBillNo", sql.VarChar(50), invNo)
        .input("PDate", sql.DateTime, pDateIso)
        .input("SupplierCode", sql.VarChar(50), supplierCode.trim())
        .input("PurchaseWeight", sql.Decimal(18, 3), pWeight)
        .input("IssueWeight", sql.Decimal(18, 3), iWeight)
        .input("UserId", sql.VarChar(50), userId)
        .query(`
          IF EXISTS (SELECT * FROM sysobjects WHERE name='sp_GoldBarIssuedInvoice' AND xtype='P')
          BEGIN
            EXEC sp_GoldBarIssuedInvoice @IssueNo, @PBillNo, @PDate, @SupplierCode, @PurchaseWeight, @IssueWeight, @UserId
          END
          ELSE
          BEGIN
            INSERT INTO tblGoldBarIssuedInvoice (IssueNo, PBillNo, PDate, SupplierCode, PurchaseWeight, IssuedWeight, CreateUser, CreateDate)
            VALUES (@IssueNo, @PBillNo, @PDate, @SupplierCode, @PurchaseWeight, @IssueWeight, @UserId, GETDATE())
          END
        `);

      // VB: UpDate tblPurchaseFix Set Issuedweight = IssuedWeight + iWeight Where InvoiceNo = invNo
      const updFixReq = new sql.Request(transaction);
      await updFixReq
        .input("IssuedWeight", sql.Decimal(18, 3), iWeight)
        .input("InvoiceNo", sql.VarChar(50), invNo)
        .query(`
          UPDATE tblPurchaseFix 
          SET IssuedWeight = ISNULL(IssuedWeight, 0) + @IssuedWeight
          WHERE InvoiceNo = @InvoiceNo
        `);
    }

    // 3. Loop grdIssue1 -> sp_GoldBarIssuedPurchase & Update tblinventoryStock
    for (const itm of items) {
      const invCode = (itm.InventoryCode || "").trim();
      const desc = (itm.Description || "").trim();
      const wt = parseFloat(itm.Weight || 0);
      const qty = parseFloat(itm.Qty || itm.Pcs || 1);
      const cpg = parseFloat(itm.CostPerGram || 0);
      const tc = parseFloat(itm.TotalCost || (wt * cpg * qty));
      const bNo = (itm.BarNo || "").trim();

      const itmReq = new sql.Request(transaction);
      await itmReq
        .input("IssueNo", sql.VarChar(50), targetIssueNo)
        .input("InventoryCode", sql.VarChar(50), invCode)
        .input("Description", sql.VarChar(200), desc)
        .input("Weight", sql.Decimal(18, 3), wt)
        .input("Qty", sql.Decimal(18, 2), qty)
        .input("CostPerGram", sql.Decimal(18, 2), cpg)
        .input("TotalCost", sql.Decimal(18, 2), tc)
        .input("BarNo", sql.VarChar(50), bNo)
        .input("UserId", sql.VarChar(50), userId)
        .query(`
          IF EXISTS (SELECT * FROM sysobjects WHERE name='sp_GoldBarIssuedPurchase' AND xtype='P')
          BEGIN
            EXEC sp_GoldBarIssuedPurchase @IssueNo, @InventoryCode, @Description, @Weight, @Qty, @CostPerGram, @TotalCost, @BarNo, @UserId
          END
          ELSE
          BEGIN
            INSERT INTO tblGoldBarIssuedPurchase (IssueNo, InventoryCode, Description, Weight, Qty, CostPerGram, TotalCost, BarNo, CreateUser, CreateDate)
            VALUES (@IssueNo, @InventoryCode, @Description, @Weight, @Qty, @CostPerGram, @TotalCost, @BarNo, @UserId, GETDATE())
          END
        `);

      // VB: UpDate tblinventoryStock Set Pcs = Pcs - qty, QtyonHand = QtyonHand - qty Where Inventorycode = invCode
      const updStockReq = new sql.Request(transaction);
      await updStockReq
        .input("Qty", sql.Decimal(18, 2), qty)
        .input("InventoryCode", sql.VarChar(50), invCode)
        .query(`
          UPDATE tblinventoryStock 
          SET Pcs = ISNULL(Pcs, 0) - @Qty, 
              QtyOnHand = ISNULL(QtyOnHand, 0) - @Qty
          WHERE InventoryCode = @InventoryCode
        `);
    }

    // 4. Increment NextGoldBarIssueNo in tblCompany
    try {
      const compReq = new sql.Request(transaction);
      await compReq.query(`
        UPDATE tblCompany 
        SET NextGoldBarIssueNo = ISNULL(NextGoldBarIssueNo, 1) + 1
      `);
    } catch (_) {}

    await transaction.commit();

    return res.status(200).json({
      success: true,
      message: "Gold Bar Issued Successfully!",
      issueNo: targetIssueNo,
    });
  } catch (err) {
    if (transaction) {
      try { await transaction.rollback(); } catch (_) {}
    }
    console.error("🔥 SAVE Gold Bar Issue Error:", err);
    return res.status(500).json({ success: false, message: "Failed to save Gold Bar Issue", error: err.message });
  }
});

// 11. DELETE Gold Bar Issue Transaction
// Reverses tblPurchaseFix IssuedWeight and tblinventoryStock QtyOnHand
router.delete("/delete/:issueNo", async (req, res) => {
  let transaction;
  try {
    const pool = await getPool(req);
    const issueNo = (req.params.issueNo || "").trim();
    if (!issueNo) {
      return res.status(400).json({ success: false, message: "Issue No is required for deletion" });
    }

    transaction = new sql.Transaction(pool);
    await transaction.begin();

    // 1. Get associated invoices to revert IssuedWeight in tblPurchaseFix
    const invReq = new sql.Request(transaction);
    const invRes = await invReq
      .input("IssueNo", sql.VarChar, issueNo)
      .query(`SELECT PBillNo, ISNULL(IssuedWeight, 0) AS IssuedWeight FROM tblGoldBarIssuedInvoice WHERE IssueNo = @IssueNo`);

    if (invRes.recordset && invRes.recordset.length > 0) {
      for (const row of invRes.recordset) {
        const revReq = new sql.Request(transaction);
        await revReq
          .input("IssuedWeight", sql.Decimal(18, 3), row.IssuedWeight)
          .input("InvoiceNo", sql.VarChar, row.PBillNo)
          .query(`
            UPDATE tblPurchaseFix 
            SET IssuedWeight = CASE WHEN ISNULL(IssuedWeight, 0) >= @IssuedWeight THEN ISNULL(IssuedWeight, 0) - @IssuedWeight ELSE 0 END
            WHERE InvoiceNo = @InvoiceNo
          `);
      }
    }

    // 2. Get associated items to revert stock in tblinventoryStock
    const itmReq = new sql.Request(transaction);
    const itmRes = await itmReq
      .input("IssueNo", sql.VarChar, issueNo)
      .query(`SELECT InventoryCode, ISNULL(Qty, 1) AS Qty FROM tblGoldBarIssuedPurchase WHERE IssueNo = @IssueNo`);

    if (itmRes.recordset && itmRes.recordset.length > 0) {
      for (const row of itmRes.recordset) {
        const revStockReq = new sql.Request(transaction);
        await revStockReq
          .input("Qty", sql.Decimal(18, 2), row.Qty)
          .input("InventoryCode", sql.VarChar, row.InventoryCode)
          .query(`
            UPDATE tblinventoryStock 
            SET Pcs = ISNULL(Pcs, 0) + @Qty,
                QtyOnHand = ISNULL(QtyOnHand, 0) + @Qty
            WHERE InventoryCode = @InventoryCode
          `);
      }
    }

    // 3. Delete records
    const delReq = new sql.Request(transaction);
    await delReq
      .input("IssueNo", sql.VarChar, issueNo)
      .query(`
        DELETE FROM tblGoldBarIssuedPurchase WHERE IssueNo = @IssueNo;
        DELETE FROM tblGoldBarIssuedInvoice WHERE IssueNo = @IssueNo;
        DELETE FROM tblGoldBarIssued WHERE IssueNo = @IssueNo;
      `);

    await transaction.commit();
    return res.status(200).json({ success: true, message: `Issue ${issueNo} deleted and stock/invoices reversed successfully.` });
  } catch (err) {
    if (transaction) {
      try { await transaction.rollback(); } catch (_) {}
    }
    console.error("🔥 DELETE Gold Bar Issue Error:", err);
    return res.status(500).json({ success: false, message: "Failed to delete Gold Bar Issue", error: err.message });
  }
});

// 12. GET Print Data for Report
router.get("/print-data/:issueNo", async (req, res) => {
  try {
    const pool = await getPool(req);
    const issueNo = (req.params.issueNo || "").trim();

    const compRes = await pool.request().query(`
      SELECT TOP 1 
        ISNULL(CompanyName, 'SMART GOLD') AS Name,
        ISNULL(Address1, '') AS Address1,
        ISNULL(Address2, '') AS Address2,
        ISNULL(Country, 'Singapore') AS Country,
        ISNULL(Phone, '') AS Phone,
        ISNULL(Fax, '') AS Fax
      FROM tblCompany
    `);
    const company = (compRes.recordset && compRes.recordset.length > 0) ? compRes.recordset[0] : {};

    const headRes = await pool.request()
      .input("IssueNo", sql.VarChar, issueNo)
      .query(`
        SELECT TOP 1
          RTRIM(i.IssueNo) AS IssueNo,
          CONVERT(varchar, i.PDate, 103) AS PDate,
          RTRIM(i.SupplierCode) AS SupplierCode,
          RTRIM(s.SupplierName) AS SupplierName,
          ROUND(ISNULL(i.IssuedWeight, 0), 3) AS IssuedWeight,
          ROUND(ISNULL(i.PurchaseWeight, 0), 3) AS PurchaseWeight,
          RTRIM(ISNULL(i.Remarks, '')) AS Remarks
        FROM tblGoldBarIssued i
        LEFT JOIN tblSupplier s ON i.SupplierCode = s.SupplierCode
        WHERE i.IssueNo = @IssueNo
      `);

    const header = (headRes.recordset && headRes.recordset.length > 0) ? headRes.recordset[0] : null;

    const invRes = await pool.request()
      .input("IssueNo", sql.VarChar, issueNo)
      .query(`
        SELECT 
          RTRIM(ISNULL(PBillNo, '')) AS PurchaseBillNo,
          CONVERT(varchar, PDate, 103) AS InvoiceDate,
          ROUND(ISNULL(PurchaseWeight, 0), 3) AS TotalWeight24K,
          ROUND(ISNULL(IssuedWeight, 0), 3) AS IssuedWeight
        FROM tblGoldBarIssuedInvoice
        WHERE IssueNo = @IssueNo
        ORDER BY PBillNo ASC
      `);

    const itemRes = await pool.request()
      .input("IssueNo", sql.VarChar, issueNo)
      .query(`
        SELECT 
          RTRIM(InventoryCode) AS InventoryCode,
          RTRIM(Description) AS Description,
          ROUND(ISNULL(Weight, 0), 3) AS Weight,
          ISNULL(Qty, 1) AS Qty,
          ROUND(ISNULL(CostPerGram, 0), 2) AS CostPerGram,
          ROUND(ISNULL(TotalCost, 0), 2) AS TotalCost,
          RTRIM(ISNULL(BarNo, '')) AS BarNo
        FROM tblGoldBarIssuedPurchase
        WHERE IssueNo = @IssueNo
        ORDER BY InventoryCode ASC
      `);

    return res.status(200).json({
      success: true,
      data: {
        company,
        header,
        invoices: invRes.recordset || [],
        items: itemRes.recordset || [],
      }
    });
  } catch (err) {
    console.error("🔥 GET Print Data Error:", err);
    return res.status(500).json({ success: false, message: "Error fetching print data", error: err.message });
  }
});

module.exports = router;
