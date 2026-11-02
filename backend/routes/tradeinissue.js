const express = require("express");
const router = express.Router();
const { sql, poolPromise } = require("../db");

// Helper to acquire database connection pool reliably
const getPool = async (req) => {
  if (req && req.pool) return req.pool;
  if (poolPromise) return await poolPromise;
  throw new Error("Database pool is not available.");
};

// 1. GET /api/tradeinissue/suppliers
// Matching btnSupplierCodeSearch_Click in frmTradeInWholeSales.vb
// Queries tblSupplier: SupplierCode, SupplierName, Address1, Address2
router.get("/suppliers", async (req, res) => {
  try {
    const pool = await getPool(req);
    const query = `
      SELECT 
        RTRIM(LTRIM(SupplierCode)) AS SupplierCode, 
        RTRIM(LTRIM(ISNULL(SupplierName, ''))) AS SupplierName, 
        RTRIM(LTRIM(ISNULL(Address1, ''))) AS Address1, 
        RTRIM(LTRIM(ISNULL(Address2, ''))) AS Address2
      FROM tblSupplier
      ORDER BY SupplierName ASC, SupplierCode ASC
    `;
    const result = await pool.request().query(query);
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Suppliers Error:", err);
    res.status(500).json({ success: false, message: "Database error", error: err.message });
  }
});

// 2. GET /api/tradeinissue/tradebills
// Matching subGetTradeInInvoices / btnTradeBillNoSearch_Click in frmTradeInWholeSales.vb
// Queries eligible trade-in lots from tblTradeInHeader & tblTradeMergeInventoryheader
router.get("/tradebills", async (req, res) => {
  try {
    const pool = await getPool(req);
    
    // Check if tblTradeInHeader exists with unissued bills
    let bills = [];
    try {
      const qTradeIn = `
        SELECT 
          RTRIM(LTRIM(InvoiceNo)) AS InvoiceNo, 
          RTRIM(LTRIM(ISNULL(InventoryCode, InvoiceNo))) AS InventoryCode, 
          ISNULL(TotalWeight, 0) AS TotalWeight,
          ISNULL(IssuedWeight, 0) AS IssuedWeight,
          ISNULL(Balanceweight, TotalWeight) AS Balanceweight,
          ISNULL(Amount, 0) AS Amount,
          CreateDate AS ModifyDate,
          RTRIM(LTRIM(ISNULL(CustomerCode, ''))) AS CustomerCode 
        FROM tblTradeInHeader
        WHERE (IssueFlag IS NULL OR RTRIM(LTRIM(IssueFlag)) <> 'Y')
          AND (Balanceweight IS NULL OR Balanceweight > 0)
        ORDER BY InvoiceNo DESC
      `;
      const resTradeIn = await pool.request().query(qTradeIn);
      if (resTradeIn.recordset.length > 0) {
        bills = resTradeIn.recordset;
      }
    } catch (e1) {
      console.warn("tblTradeInHeader query fallback:", e1.message);
    }

    // Also check tblTradeMergeInventoryheader (used by TradeInMerge in standard flow)
    if (bills.length === 0) {
      try {
        const qMerge = `
          SELECT 
            RTRIM(LTRIM(TradeInInventoryCode)) AS InvoiceNo, 
            RTRIM(LTRIM(TradeInInventoryCode)) AS InventoryCode, 
            ISNULL(TotalWeight, 0) AS TotalWeight,
            0 AS IssuedWeight,
            ISNULL(TotalWeight, 0) AS Balanceweight,
            ISNULL(TotalPrice, 0) AS Amount,
            CreateDate AS ModifyDate,
            '' AS CustomerCode
          FROM tblTradeMergeInventoryheader
          WHERE ISNULL(QtyOnHand, 0) <> 0
          ORDER BY TradeInInventoryCode DESC
        `;
        const resMerge = await pool.request().query(qMerge);
        bills = resMerge.recordset;
      } catch (e2) {
        console.warn("tblTradeMergeInventoryheader query fallback:", e2.message);
      }
    }

    res.json({ success: true, data: bills });
  } catch (err) {
    console.error("Fetch Trade Bills Error:", err);
    res.status(500).json({ success: false, message: "Database error", error: err.message });
  }
});

// 3. GET /api/tradeinissue/history
// Matching txtTradeIssueNo_LostFocus / GetTradeIssueHistory in frmTradeInWholeSales.vb
router.get("/history", async (req, res) => {
  try {
    const pool = await getPool(req);
    
    // Check tblTradeIssueHistoryHeader first, then tblTradeIssueHistory
    let rows = [];
    try {
      const q = `
        SELECT DISTINCT
          RTRIM(LTRIM(h.TradeIssueNo)) AS TradeIssueNo, 
          RTRIM(LTRIM(h.SupplierCode)) AS SupplierCode,
          RTRIM(LTRIM(ISNULL(s.SupplierName, ''))) AS SupplierName,
          h.CreateDate AS IssueDate,
          ISNULL(h.NoOfBills, 0) AS NoOfBills,
          ISNULL(h.TotalWeight, 0) AS TotalWeight,
          ISNULL(h.TotalPrice, 0) AS TotalPrice
        FROM tblTradeIssueHistoryHeader h
        LEFT JOIN tblSupplier s ON h.SupplierCode = s.SupplierCode
        ORDER BY h.TradeIssueNo DESC
      `;
      const result = await pool.request().query(q);
      rows = result.recordset;
    } catch (eHdr) {
      try {
        const qFallback = `
          SELECT DISTINCT
            RTRIM(LTRIM(h.TradeIssueNo)) AS TradeIssueNo, 
            RTRIM(LTRIM(h.SupplierCode)) AS SupplierCode,
            RTRIM(LTRIM(ISNULL(s.SupplierName, ''))) AS SupplierName
          FROM tblTradeIssueHistory h
          LEFT JOIN tblSupplier s ON h.SupplierCode = s.SupplierCode
          ORDER BY h.TradeIssueNo DESC
        `;
        const resFallback = await pool.request().query(qFallback);
        rows = resFallback.recordset;
      } catch (eHist) {}
    }

    res.json({ success: true, data: rows });
  } catch (err) {
    console.error("Fetch Trade Issue History Error:", err);
    res.json({ success: true, data: [] });
  }
});

// 4. GET /api/tradeinissue/nextIssueNo
// Matching objTII.GetNextTradeIssueNo in frmTradeInWholeSales.vb
router.get("/nextIssueNo", async (req, res) => {
  try {
    const pool = await getPool(req);
    let nextSeq = 1;
    let loc = "HQ";

    const compRes = await pool.request().query(
      "SELECT TOP 1 LocationCode, NextTradeIssueNo FROM tblCompany"
    );
    if (compRes.recordset.length > 0) {
      const comp = compRes.recordset[0];
      nextSeq = parseInt(comp.NextTradeIssueNo, 10) || 1;
      loc = (comp.LocationCode || "HQ").trim();
    }

    const nextCode = `TI-${loc}${String(nextSeq).padStart(8, "0")}`;
    res.json({ success: true, data: nextCode, rawSeq: nextSeq });
  } catch (err) {
    console.error("Get Next Trade Issue No Error:", err);
    res.status(500).json({ success: false, message: "Database error", error: err.message });
  }
});

// 5. POST /api/tradeinissue/proceed
// Matching btnProceed_Click / UpdateTradeInHeader / UpdateTradeInIssueHistoryHeader in frmTradeInWholeSales.vb
router.post("/proceed", async (req, res) => {
  let pool;
  try {
    pool = await getPool(req);
  } catch (poolErr) {
    return res.status(500).json({ success: false, message: poolErr.message });
  }

  const transaction = new sql.Transaction(pool);

  try {
    const {
      tradeIssueNo,
      issueDate,
      supplierCode,
      noOfBills,
      totalIssuedWt,
      costPerGram,
      totalPrice,
      totalWt,
      purity,
      remarks,
      isProduction,
      isExport,
      userId,
      tradeBills
    } = req.body;

    const sIssueNo = (tradeIssueNo || "").trim();
    const sSupplier = (supplierCode || "").trim();
    const sRemarks = (remarks || "").trim();
    const sUser = (userId || "admin").trim();
    const sPurity = (purity || "999.9").trim();

    if (!sSupplier) {
      return res.status(400).json({ success: false, message: "Supplier cannot be blank!" });
    }
    const nIssuedWt = parseFloat(totalIssuedWt) || 0;
    if (nIssuedWt <= 0) {
      return res.status(400).json({ success: false, message: "Gold Issued Actual Weight Should Not Be 0 !!!" });
    }
    if (!tradeBills || !Array.isArray(tradeBills) || tradeBills.length === 0) {
      return res.status(400).json({ success: false, message: "Please add at least one Trade Bill in the grid!" });
    }

    const nPricePerGram = parseFloat(costPerGram) || 0;
    const nTotalPrice = parseFloat(totalPrice) || (nIssuedWt * nPricePerGram);
    const nTotalWt = parseFloat(totalWt) || nIssuedWt;
    const nBillsCount = parseInt(noOfBills, 10) || tradeBills.length;

    await transaction.begin();

    // 1. Process each selected trade bill
    let remainingToIssue = nIssuedWt;

    for (const bill of tradeBills) {
      const invNo = (bill.InvoiceNo || "").trim();
      const billBalWt = parseFloat(bill.BalanceWeight || bill.PurchaseWeight || 0);
      let issueWtForBill = 0;

      if (remainingToIssue >= billBalWt) {
        issueWtForBill = billBalWt;
        remainingToIssue -= billBalWt;
      } else {
        issueWtForBill = remainingToIssue;
        remainingToIssue = 0;
      }

      // Update tblTradeInHeader
      try {
        const reqUpdHdr = new sql.Request(transaction);
        reqUpdHdr.input("InvoiceNo", sql.VarChar(50), invNo);
        reqUpdHdr.input("IssueWt", sql.Decimal(18, 4), issueWtForBill);
        reqUpdHdr.input("TradeIssueNo", sql.VarChar(50), sIssueNo);
        reqUpdHdr.input("SupplierCode", sql.VarChar(50), sSupplier);
        reqUpdHdr.input("Remarks", sql.VarChar(200), sRemarks);

        await reqUpdHdr.query(`
          UPDATE tblTradeInHeader 
          SET 
            IssuedWeight = ISNULL(IssuedWeight, 0) + @IssueWt,
            Balanceweight = CASE 
              WHEN ISNULL(Balanceweight, TotalWeight) - @IssueWt < 0 THEN 0 
              ELSE ISNULL(Balanceweight, TotalWeight) - @IssueWt 
            END,
            IssueFlag = CASE 
              WHEN ISNULL(Balanceweight, TotalWeight) - @IssueWt <= 0 THEN 'Y' 
              ELSE 'P' 
            END,
            TradeIssueNo = @TradeIssueNo,
            ModifyDate = GETDATE()
          WHERE RTRIM(LTRIM(InvoiceNo)) = @InvoiceNo
        `);
      } catch (eHdr) {
        // If not in tblTradeInHeader, update tblTradeMergeInventoryheader QtyOnHand
        try {
          const reqUpdMerge = new sql.Request(transaction);
          reqUpdMerge.input("InvCode", sql.VarChar(50), invNo);
          await reqUpdMerge.query(`
            UPDATE tblTradeMergeInventoryheader
            SET QtyOnHand = 0, ModifyDate = GETDATE()
            WHERE RTRIM(LTRIM(TradeInInventoryCode)) = @InvCode
          `);
        } catch (eMerge) {}
      }

      if (remainingToIssue <= 0) break;
    }

    // 2. Insert into tblTradeIssueHistoryHeader if available
    try {
      const reqHistHdr = new sql.Request(transaction);
      reqHistHdr.input("TradeIssueNo", sql.VarChar(50), sIssueNo);
      reqHistHdr.input("SupplierCode", sql.VarChar(50), sSupplier);
      reqHistHdr.input("NoOfBills", sql.Int, nBillsCount);
      reqHistHdr.input("TotalWeight", sql.Decimal(18, 4), nTotalWt);
      reqHistHdr.input("PricePerGram", sql.Decimal(18, 4), nPricePerGram);
      reqHistHdr.input("TotalPrice", sql.Decimal(18, 2), nTotalPrice);
      reqHistHdr.input("Purity", sql.VarChar(20), sPurity);
      reqHistHdr.input("Remarks", sql.VarChar(200), sRemarks);
      reqHistHdr.input("CreateUser", sql.VarChar(20), sUser);

      await reqHistHdr.query(`
        INSERT INTO tblTradeIssueHistoryHeader (
          TradeIssueNo, IssueDate, SupplierCode, NoOfBills, TotalWeight, 
          PricePerGram, TotalPrice, Purity, Remarks, CreateUser, CreateDate
        ) VALUES (
          @TradeIssueNo, GETDATE(), @SupplierCode, @NoOfBills, @TotalWeight, 
          @PricePerGram, @TotalPrice, @Purity, @Remarks, @CreateUser, GETDATE()
        )
      `);
    } catch (eHistHdr) {
      // Fallback: Check if tblTradeIssueHistory exists
      try {
        const reqHist = new sql.Request(transaction);
        reqHist.input("TradeIssueNo", sql.VarChar(50), sIssueNo);
        reqHist.input("SupplierCode", sql.VarChar(50), sSupplier);
        reqHist.input("CreateUser", sql.VarChar(20), sUser);
        await reqHist.query(`
          INSERT INTO tblTradeIssueHistory (
            TradeIssueNo, SupplierCode, CreateDate, CreateUser
          ) VALUES (
            @TradeIssueNo, @SupplierCode, GETDATE(), @CreateUser
          )
        `);
      } catch (eHist) {}
    }

    // 3. Increment NextTradeIssueNo in tblCompany
    await new sql.Request(transaction).query(
      "UPDATE tblCompany SET NextTradeIssueNo = ISNULL(NextTradeIssueNo, 1) + 1"
    );

    await transaction.commit();
    res.json({ success: true, message: "Tradein Gold Issued Sucessfully" });

  } catch (err) {
    try {
      if (transaction) await transaction.rollback();
    } catch (rbErr) {}
    console.error("TradeIn Issue Save Error:", err);
    res.status(500).json({ success: false, message: "Error in save", error: err.message });
  }
});

module.exports = router;
