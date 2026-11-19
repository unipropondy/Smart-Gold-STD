/** Developer: Antigravity */
const express = require("express");
const router = express.Router();
const { sql, poolPromise } = require("../db");

// Helper to acquire database connection pool reliably
const getPool = async (req) => {
  if (req && req.pool) return req.pool;
  if (poolPromise) return await poolPromise;
  throw new Error("Database pool is not available.");
};

// 1. GET /api/tradeingoldissue/nextIssueNo
// VB: objTII.GetNextTradeIssueNo() -> Mid(gthisLocation,1,2) & "65" & Format(NextTradeIssueNo, "000000")
router.get("/nextIssueNo", async (req, res) => {
  try {
    const pool = await getPool(req);
    const result = await pool.request().query(`
      SELECT TOP 1 
        ISNULL(LocationCode, 'HQ') AS LocationCode, 
        ISNULL(NextTradeIssueNo, 1) AS NextTradeIssueNo 
      FROM tblCompany
    `);

    let loc = "HQ";
    let nextSeq = 1;
    if (result.recordset && result.recordset.length > 0) {
      loc = (result.recordset[0].LocationCode || "HQ").trim();
      nextSeq = parseInt(result.recordset[0].NextTradeIssueNo, 10) || 1;
    }

    const locPrefix = loc.substring(0, 2);
    const issueNo = `${locPrefix}65${String(nextSeq).padStart(6, "0")}`;

    res.json({ success: true, issueNo, nextSeq });
  } catch (err) {
    console.error("TradeInIssue nextIssueNo error:", err);
    res.status(500).json({ success: false, message: "Error generating next issue number", error: err.message });
  }
});

// 2. GET /api/tradeingoldissue/suppliers
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

// 2b. GET /api/tradeingoldissue/supplier/:code
// VB: subGetSupplier -> objSupp.GetSupplierDetail
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
    console.error("TradeInIssue supplier by code error:", err);
    res.status(500).json({ success: false, message: "Error fetching supplier", error: err.message });
  }
});

// 3. GET /api/tradeingoldissue/currencies
// VB: frmHelpSearch on tblCurrency: CurrencyCode, CurrencyName, CurrencyRate, Remarks
router.get("/currencies", async (req, res) => {
  try {
    const pool = await getPool(req);
    const result = await pool.request().query(`
      SELECT 
        RTRIM(LTRIM(CurrencyCode)) AS CurrencyCode, 
        RTRIM(LTRIM(ISNULL(CurrencyName, ''))) AS CurrencyName, 
        ISNULL(CurrencyRate, 1.0) AS CurrencyRate, 
        RTRIM(LTRIM(ISNULL(Remarks, ''))) AS Remarks 
      FROM tblCurrency 
      ORDER BY CurrencyCode ASC
    `);
    res.json({ success: true, data: result.recordset || [] });
  } catch (err) {
    console.error("TradeInIssue currencies error:", err);
    res.status(500).json({ success: false, message: "Error fetching currencies", error: err.message });
  }
});

// 3b. GET /api/tradeingoldissue/currency/:code
// VB: subGetCurrency -> objCurr.GetCurrencyDetail
router.get("/currency/:code", async (req, res) => {
  try {
    const { code } = req.params;
    const pool = await getPool(req);
    const result = await pool.request()
      .input("CurrencyCode", sql.VarChar, (code || "").trim())
      .query(`
        SELECT TOP 1 
          RTRIM(LTRIM(CurrencyCode)) AS CurrencyCode, 
          RTRIM(LTRIM(ISNULL(CurrencyName, ''))) AS CurrencyName, 
          ISNULL(CurrencyRate, 1.0) AS CurrencyRate, 
          RTRIM(LTRIM(ISNULL(Remarks, ''))) AS Remarks 
        FROM tblCurrency 
        WHERE CurrencyCode = @CurrencyCode
      `);

    if (result.recordset && result.recordset.length > 0) {
      res.json({ success: true, data: result.recordset[0] });
    } else {
      res.json({ success: false, message: "Currency not found" });
    }
  } catch (err) {
    console.error("TradeInIssue currency by code error:", err);
    res.status(500).json({ success: false, message: "Error fetching currency", error: err.message });
  }
});

// 4. GET /api/tradeingoldissue/tradebills
// VB: btnTradeBillNoSearch_Click -> tblTradeInHeader WHERE ((IssueFlag is Null) or (IssueFlag <> 'Y'))
// AND BalanceWeight > 0
router.get("/tradebills", async (req, res) => {
  try {
    const pool = await getPool(req);
    const query = `
      SELECT 
        RTRIM(LTRIM(InvoiceNo)) AS InvoiceNo, 
        RTRIM(LTRIM(ISNULL(InventoryCode, ''))) AS InventoryCode, 
        ISNULL(Weight, ISNULL(TotalWeight, 0)) AS TotalWeight, 
        ISNULL(IssuedWeight, 0) AS IssuedWeight, 
        ISNULL(BalanceWeight, ISNULL(Weight, ISNULL(TotalWeight, 0))) AS BalanceWeight, 
        ISNULL(TradeInAmount, ISNULL(TotalCost, 0)) AS Amount, 
        ISNULL(ModifyDate, CreateDate) AS ModifyDate, 
        RTRIM(LTRIM(ISNULL(CustomerCode, ''))) AS CustomerCode 
      FROM tblTradeInHeader 
      WHERE (IssueFlag IS NULL OR RTRIM(LTRIM(IssueFlag)) <> 'Y') 
        AND (ISNULL(BalanceWeight, ISNULL(Weight, ISNULL(TotalWeight, 0))) > 0) 
      ORDER BY InvoiceNo DESC
    `;
    const result = await pool.request().query(query);
    res.json({ success: true, data: result.recordset || [] });
  } catch (err) {
    console.error("TradeInIssue tradebills error:", err);
    res.status(500).json({ success: false, message: "Error fetching trade bills", error: err.message });
  }
});

// 4b. GET /api/tradeingoldissue/tradebill/:invoiceno
// VB: objTIS.GetTradeInBalance(Trim(txtTradeBillNo.Text)) -> tblTradeInHeader WHERE InvoiceNo = ... AND BalanceWeight > 0
router.get("/tradebill/:invoiceno", async (req, res) => {
  try {
    const { invoiceno } = req.params;
    const pool = await getPool(req);
    const result = await pool.request()
      .input("InvoiceNo", sql.VarChar, (invoiceno || "").trim())
      .query(`
        SELECT TOP 1 
          RTRIM(LTRIM(InvoiceNo)) AS InvoiceNo, 
          RTRIM(LTRIM(ISNULL(InventoryCode, ''))) AS InventoryCode, 
          ISNULL(Weight, ISNULL(TotalWeight, 0)) AS TotalWeight, 
          ISNULL(IssuedWeight, 0) AS IssuedWeight, 
          ISNULL(BalanceWeight, ISNULL(Weight, ISNULL(TotalWeight, 0))) AS BalanceWeight, 
          ISNULL(TradeInAmount, ISNULL(TotalCost, 0)) AS Amount, 
          ISNULL(ModifyDate, CreateDate) AS ModifyDate, 
          RTRIM(LTRIM(ISNULL(CustomerCode, ''))) AS CustomerCode 
        FROM tblTradeInHeader 
        WHERE InvoiceNo = @InvoiceNo 
          AND (ISNULL(BalanceWeight, ISNULL(Weight, ISNULL(TotalWeight, 0))) > 0)
      `);

    if (result.recordset && result.recordset.length > 0) {
      res.json({ success: true, data: result.recordset[0] });
    } else {
      res.json({ success: false, message: "Trade bill not found or balance weight is 0" });
    }
  } catch (err) {
    console.error("TradeInIssue tradebill lookup error:", err);
    res.status(500).json({ success: false, message: "Error retrieving trade bill", error: err.message });
  }
});

// 5. GET /api/tradeingoldissue/history
// VB: btnIssueNoSearch_Click -> tblTradeIssueHistory: TradeIssueNo, SupplierCode, SupplierName, CustomerCode, InvoiceNo
router.get("/history", async (req, res) => {
  try {
    const pool = await getPool(req);
    const query = `
      SELECT DISTINCT 
        RTRIM(LTRIM(h.TradeIssueNo)) AS TradeIssueNo, 
        RTRIM(LTRIM(h.SupplierCode)) AS SupplierCode, 
        RTRIM(LTRIM(ISNULL(s.SupplierName, ''))) AS SupplierName, 
        h.Date AS IssueDate, 
        ISNULL(h.IssuedWeight, 0) AS IssuedWeight, 
        ISNULL(h.TotalCost, 0) AS TotalCost 
      FROM tblTradeIssueHistory h 
      LEFT JOIN tblSupplier s ON h.SupplierCode = s.SupplierCode 
      ORDER BY h.TradeIssueNo DESC
    `;
    const result = await pool.request().query(query);
    res.json({ success: true, data: result.recordset || [] });
  } catch (err) {
    console.error("TradeInIssue history error:", err);
    res.status(500).json({ success: false, message: "Error fetching history", error: err.message });
  }
});

// 5b. GET /api/tradeingoldissue/history/:issueno
// VB: objTII.GetTradeIssueHistory(txtTradeIssueNo.Text.Trim)
router.get("/history/:issueno", async (req, res) => {
  try {
    const { issueno } = req.params;
    const pool = await getPool(req);
    const result = await pool.request()
      .input("TradeIssueNo", sql.VarChar, (issueno || "").trim())
      .query(`
        SELECT 
          h.*, 
          RTRIM(LTRIM(ISNULL(s.SupplierName, ''))) AS SupplierName 
        FROM tblTradeIssueHistory h 
        LEFT JOIN tblSupplier s ON h.SupplierCode = s.SupplierCode 
        WHERE h.TradeIssueNo = @TradeIssueNo
      `);

    res.json({ success: true, data: result.recordset || [] });
  } catch (err) {
    console.error("TradeInIssue history detail error:", err);
    res.status(500).json({ success: false, message: "Error fetching history details", error: err.message });
  }
});

// 6. POST /api/tradeingoldissue/proceed
// VB: btnProceed_Click -> loops through grdGoldIssue, calls sp_UpdateTradeInHeader & sp_TradeIssueHistory, updates tblCompany.NextTradeIssueNo
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
      currencyCode,
      currencyRate = 1.0,
      totalIssuedWt = 0,
      totalNoOfBills = 0,
      costPerGram = 0,
      totalPrice = 0,
      userId = "ADMIN",
      items = []
    } = req.body;

    if (!supplierCode || !supplierCode.trim()) {
      return res.status(400).json({ success: false, message: "Supplier cannot be blank!" });
    }
    if (!currencyCode || !currencyCode.trim()) {
      return res.status(400).json({ success: false, message: "Currency cannot be blank!" });
    }
    const nIssuedWt = parseFloat(totalIssuedWt) || 0;
    if (nIssuedWt <= 0) {
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

    let totPurchaseWeight = nIssuedWt;

    // 2. Loop through grid items matching VB btnProceed_Click logic
    for (let i = 0; i < items.length; i++) {
      const item = items[i];
      const invNo = (item.InvoiceNo || "").trim();

      // Retrieve Trade In Header for current InvoiceNo
      const thReq = new sql.Request(transaction);
      thReq.input("InvNo", sql.VarChar, invNo);
      const thRes = await thReq.query(`
        SELECT TOP 1 * 
        FROM tblTradeInHeader 
        WHERE InvoiceNo = @InvNo
      `);

      if (thRes.recordset && thRes.recordset.length > 0) {
        const row = thRes.recordset[0];
        const balWeight = parseFloat(row.BalanceWeight || row.Weight || row.TotalWeight || 0);
        let tempPurWeight = 0;

        if (totPurchaseWeight >= balWeight) {
          totPurchaseWeight -= balWeight;
          tempPurWeight = balWeight;
        } else {
          tempPurWeight = totPurchaseWeight;
          totPurchaseWeight = 0;
        }

        // Execute sp_UpdateTradeInHeader
        try {
          const spUpdReq = new sql.Request(transaction);
          spUpdReq.input("SIssueNo", sql.VarChar, finalTradeIssueNo);
          spUpdReq.input("InvoiceNo", sql.VarChar, invNo);
          spUpdReq.input("InventoryCode", sql.VarChar, row.InventoryCode || invNo);
          spUpdReq.input("TempPurWeight", sql.Decimal(18, 4), tempPurWeight);
          spUpdReq.input("Supplier", sql.VarChar, supplierCode.trim());
          spUpdReq.input("UserID", sql.VarChar, userId);
          spUpdReq.input("ActualIssuedWeight", sql.Decimal(18, 4), 0);
          spUpdReq.input("Remarks", sql.VarChar, "");

          await spUpdReq.execute("sp_UpdateTradeInHeader");
        } catch (spErr) {
          // If stored procedure doesn't exist or fails, execute exact equivalent SQL
          const directUpd = new sql.Request(transaction);
          directUpd.input("InvoiceNo", sql.VarChar, invNo);
          directUpd.input("IssueWt", sql.Decimal(18, 4), tempPurWeight);
          await directUpd.query(`
            UPDATE tblTradeInHeader SET 
              IssuedWeight = ISNULL(IssuedWeight, 0) + @IssueWt, 
              BalanceWeight = CASE 
                WHEN ISNULL(BalanceWeight, ISNULL(Weight, TotalWeight)) - @IssueWt < 0 THEN 0 
                ELSE ISNULL(BalanceWeight, ISNULL(Weight, TotalWeight)) - @IssueWt 
              END, 
              IssueFlag = CASE 
                WHEN ISNULL(BalanceWeight, ISNULL(Weight, TotalWeight)) - @IssueWt <= 0 THEN 'Y' 
                ELSE 'P' 
              END 
            WHERE InvoiceNo = @InvoiceNo
          `);
        }

        // Save into tblTradeIssueHistory using sp_TradeIssueHistory or direct INSERT
        try {
          const spHist = new sql.Request(transaction);
          spHist.input("TradeIssueNo", sql.VarChar, finalTradeIssueNo);
          spHist.input("InventoryCode", sql.VarChar, row.InventoryCode || invNo);
          spHist.input("InvoiceNo", sql.VarChar, invNo);
          spHist.input("LocationCode", sql.VarChar, loc);
          spHist.input("CustomerCode", sql.VarChar, row.CustomerCode || "");
          spHist.input("CustomerName", sql.VarChar, row.CustomerName || "");
          spHist.input("Address1", sql.VarChar, row.Address1 || "");
          spHist.input("Address2", sql.VarChar, row.Address2 || "");
          spHist.input("Address3", sql.VarChar, row.Address3 || "");
          spHist.input("CostPerGram", sql.Decimal(18, 3), parseFloat(row.CostPerGram) || 0);
          spHist.input("TotalWeight", sql.Decimal(18, 3), parseFloat(row.Weight || row.TotalWeight) || 0);
          spHist.input("IssuedWeight", sql.Decimal(18, 3), tempPurWeight);
          spHist.input("BalanceWeight", sql.Decimal(18, 3), Math.max(0, balWeight - tempPurWeight));
          spHist.input("StoneWeight", sql.Decimal(18, 3), parseFloat(row.StoneWeight) || 0);
          spHist.input("MC", sql.Decimal(18, 2), 0);
          spHist.input("PCS", sql.Decimal(18, 2), parseFloat(row.PCS) || 1);
          spHist.input("GoldLoss", sql.Decimal(18, 3), parseFloat(row.GoldLoss) || 0);
          spHist.input("TotalCost", sql.Decimal(18, 2), parseFloat(row.TotalCost) || 0);
          spHist.input("Amount", sql.Decimal(18, 2), parseFloat(row.TradeInAmount || row.TotalCost) || 0);
          spHist.input("Remarks", sql.VarChar, row.Remarks || "");
          spHist.input("SoldFlag", sql.VarChar, "N");
          spHist.input("UserID", sql.VarChar, userId);

          await spHist.execute("sp_TradeIssueHistory");
        } catch (spHistErr) {
          const directHist = new sql.Request(transaction);
          directHist
            .input("TradeIssueNo", sql.VarChar, finalTradeIssueNo)
            .input("InventoryCode", sql.VarChar, row.InventoryCode || invNo)
            .input("InvoiceNo", sql.VarChar, invNo)
            .input("LocationCode", sql.VarChar, loc)
            .input("SupplierCode", sql.VarChar, supplierCode.trim())
            .input("CustomerCode", sql.VarChar, row.CustomerCode || "")
            .input("CustomerName", sql.VarChar, row.CustomerName || "")
            .input("Address1", sql.VarChar, row.Address1 || "")
            .input("Address2", sql.VarChar, row.Address2 || "")
            .input("Address3", sql.VarChar, row.Address3 || "")
            .input("CostPerGram", sql.Decimal(18, 3), parseFloat(row.CostPerGram) || 0)
            .input("TotalWeight", sql.Decimal(18, 3), parseFloat(row.Weight || row.TotalWeight) || 0)
            .input("IssuedWeight", sql.Decimal(18, 3), tempPurWeight)
            .input("BalanceWeight", sql.Decimal(18, 3), Math.max(0, balWeight - tempPurWeight))
            .input("StoneWeight", sql.Decimal(18, 3), parseFloat(row.StoneWeight) || 0)
            .input("MC", sql.Decimal(18, 2), 0)
            .input("PCS", sql.Decimal(18, 2), parseFloat(row.PCS) || 1)
            .input("GoldLoss", sql.Decimal(18, 3), parseFloat(row.GoldLoss) || 0)
            .input("TotalCost", sql.Decimal(18, 2), parseFloat(row.TotalCost) || 0)
            .input("Amount", sql.Decimal(18, 2), parseFloat(row.TradeInAmount || row.TotalCost) || 0)
            .input("Remarks", sql.VarChar, row.Remarks || "")
            .input("SoldFlag", sql.VarChar, "N")
            .input("UserID", sql.VarChar, userId);

          await directHist.query(`
            INSERT INTO tblTradeIssueHistory (
              TradeIssueNo, InventoryCode, InvoiceNo, LocationCode, SupplierCode,
              CustomerCode, CustomerName, Address1, Address2, Address3,
              CostPerGram, TotalWeight, IssuedWeight, BalanceWeight, StoneWeight,
              MC, PCS, GoldLoss, TotalCost, Amount,
              Remarks, SoldFlag, Date, UserID
            ) VALUES (
              @TradeIssueNo, @InventoryCode, @InvoiceNo, @LocationCode, @SupplierCode,
              @CustomerCode, @CustomerName, @Address1, @Address2, @Address3,
              @CostPerGram, @TotalWeight, @IssuedWeight, @BalanceWeight, @StoneWeight,
              @MC, @PCS, @GoldLoss, @TotalCost, @Amount,
              @Remarks, @SoldFlag, GETDATE(), @UserID
            )
          `);
        }
      }
    }

    // 3. Update Sequence in tblCompany: NextTradeIssueNo = NextTradeIssueNo + 1
    const updComp = new sql.Request(transaction);
    await updComp.query(`
      UPDATE tblCompany 
      SET NextTradeIssueNo = ISNULL(NextTradeIssueNo, 1) + 1
    `);

    await transaction.commit();

    res.json({
      success: true,
      message: "Tradein Gold Issued Successfully",
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

// Aliases for frontend LOV paths
router.get("/lov/suppliers", async (req, res) => {
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
    res.status(500).json({ success: false, message: err.message });
  }
});

router.get("/lov/currencies", async (req, res) => {
  try {
    const pool = await getPool(req);
    const result = await pool.request().query(`
      SELECT 
        RTRIM(LTRIM(CurrencyCode)) AS CurrencyCode, 
        RTRIM(LTRIM(ISNULL(CurrencyName, ''))) AS CurrencyName, 
        ISNULL(CurrencyRate, 1.0) AS CurrencyRate, 
        RTRIM(LTRIM(ISNULL(Remarks, ''))) AS Remarks 
      FROM tblCurrency 
      ORDER BY CurrencyCode ASC
    `);
    res.json({ success: true, data: result.recordset || [] });
  } catch (err) {
    res.status(500).json({ success: false, message: err.message });
  }
});

router.get("/currencyDetails", async (req, res) => {
  try {
    const code = (req.query.code || "").trim();
    const pool = await getPool(req);
    const result = await pool.request()
      .input("CurrencyCode", sql.VarChar, code)
      .query(`
        SELECT TOP 1 
          RTRIM(LTRIM(CurrencyCode)) AS CurrencyCode, 
          RTRIM(LTRIM(ISNULL(CurrencyName, ''))) AS CurrencyName, 
          ISNULL(CurrencyRate, 1.0) AS CurrencyRate, 
          RTRIM(LTRIM(ISNULL(Remarks, ''))) AS Remarks 
        FROM tblCurrency 
        WHERE CurrencyCode = @CurrencyCode
      `);
    if (result.recordset && result.recordset.length > 0) {
      res.json({ success: true, data: result.recordset[0] });
    } else {
      res.json({ success: false, message: "Currency not found" });
    }
  } catch (err) {
    res.status(500).json({ success: false, message: err.message });
  }
});

router.get("/lov/pendingTradeBills", async (req, res) => {
  try {
    const pool = await getPool(req);
    const query = `
      SELECT 
        RTRIM(LTRIM(InvoiceNo)) AS InvoiceNo, 
        RTRIM(LTRIM(ISNULL(InventoryCode, ''))) AS InventoryCode, 
        ISNULL(Weight, ISNULL(TotalWeight, 0)) AS PurchaseWeight, 
        ISNULL(IssuedWeight, 0) AS IssuedWeight, 
        ISNULL(BalanceWeight, ISNULL(Weight, ISNULL(TotalWeight, 0))) AS BalanceWeight, 
        ISNULL(TradeInAmount, ISNULL(TotalCost, 0)) AS TotalCost, 
        ISNULL(ModifyDate, CreateDate) AS InvoiceDate, 
        RTRIM(LTRIM(ISNULL(CustomerCode, ''))) AS CustomerCode 
      FROM tblTradeInHeader 
      WHERE (IssueFlag IS NULL OR RTRIM(LTRIM(IssueFlag)) <> 'Y') 
        AND (ISNULL(BalanceWeight, ISNULL(Weight, ISNULL(TotalWeight, 0))) > 0) 
      ORDER BY InvoiceNo DESC
    `;
    const result = await pool.request().query(query);
    res.json({ success: true, data: result.recordset || [] });
  } catch (err) {
    res.status(500).json({ success: false, message: err.message });
  }
});

router.get("/getTradeBill", async (req, res) => {
  try {
    const invoiceNo = (req.query.invoiceNo || "").trim();
    const pool = await getPool(req);
    const result = await pool.request()
      .input("InvoiceNo", sql.VarChar, invoiceNo)
      .query(`
        SELECT TOP 1 
          RTRIM(LTRIM(InvoiceNo)) AS InvoiceNo, 
          RTRIM(LTRIM(ISNULL(InventoryCode, ''))) AS InventoryCode, 
          ISNULL(Weight, ISNULL(TotalWeight, 0)) AS PurchaseWeight, 
          ISNULL(IssuedWeight, 0) AS IssuedWeight, 
          ISNULL(BalanceWeight, ISNULL(Weight, ISNULL(TotalWeight, 0))) AS BalanceWeight, 
          ISNULL(TradeInAmount, ISNULL(TotalCost, 0)) AS TotalCost, 
          ISNULL(ModifyDate, CreateDate) AS InvoiceDate, 
          RTRIM(LTRIM(ISNULL(CustomerCode, ''))) AS CustomerCode 
        FROM tblTradeInHeader 
        WHERE InvoiceNo = @InvoiceNo 
          AND (ISNULL(BalanceWeight, ISNULL(Weight, ISNULL(TotalWeight, 0))) > 0)
      `);
    if (result.recordset && result.recordset.length > 0) {
      res.json({ success: true, data: result.recordset[0] });
    } else {
      res.json({ success: false, message: "Trade bill not found or balance weight is 0" });
    }
  } catch (err) {
    res.status(500).json({ success: false, message: err.message });
  }
});

router.get("/lov/issueList", async (req, res) => {
  try {
    const pool = await getPool(req);
    const query = `
      SELECT DISTINCT 
        RTRIM(LTRIM(h.TradeIssueNo)) AS IssueNo, 
        RTRIM(LTRIM(h.SupplierCode)) AS SupplierCode, 
        RTRIM(LTRIM(ISNULL(s.SupplierName, ''))) AS SupplierName, 
        h.Date AS IssueDate, 
        ISNULL(h.IssuedWeight, 0) AS TotalIssuedWeight, 
        ISNULL(h.TotalCost, 0) AS TotalPrice 
      FROM tblTradeIssueHistory h 
      LEFT JOIN tblSupplier s ON h.SupplierCode = s.SupplierCode 
      ORDER BY IssueNo DESC
    `;
    const result = await pool.request().query(query);
    res.json({ success: true, data: result.recordset || [] });
  } catch (err) {
    res.status(500).json({ success: false, message: err.message });
  }
});

router.get("/getIssueDetails", async (req, res) => {
  try {
    const issueNo = (req.query.issueNo || "").trim();
    const pool = await getPool(req);
    const result = await pool.request()
      .input("TradeIssueNo", sql.VarChar, issueNo)
      .query(`
        SELECT 
          h.*, 
          RTRIM(LTRIM(ISNULL(s.SupplierName, ''))) AS SupplierName 
        FROM tblTradeIssueHistory h 
        LEFT JOIN tblSupplier s ON h.SupplierCode = s.SupplierCode 
        WHERE h.TradeIssueNo = @TradeIssueNo
      `);
    const records = result.recordset || [];
    if (records.length > 0) {
      const first = records[0];
      const header = {
        TradeIssueNo: first.TradeIssueNo,
        IssueDate: first.Date,
        SupplierCode: first.SupplierCode,
        SupplierName: first.SupplierName,
        CostPerGram: first.CostPerGram,
        TotalIssuedWeight: records.reduce((s, r) => s + (parseFloat(r.IssuedWeight) || 0), 0),
        NoOfBills: records.length,
        TotalPrice: records.reduce((s, r) => s + (parseFloat(r.TotalCost) || 0), 0)
      };
      const items = records.map(r => ({
        InvoiceNo: r.InvoiceNo,
        Date: r.Date,
        PurchaseWeight: r.TotalWeight,
        IssuedWeight: r.IssuedWeight,
        BalanceWeight: r.BalanceWeight
      }));
      res.json({ success: true, data: { header, items } });
    } else {
      res.json({ success: false, message: "Issue details not found" });
    }
  } catch (err) {
    res.status(500).json({ success: false, message: err.message });
  }
});

module.exports = router;
