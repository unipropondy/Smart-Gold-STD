const express = require("express");
const router = express.Router();
const { sql } = require("../db");

// GET /api/tradeinissue/suppliers
router.get("/suppliers", async (req, res) => {
  try {
    const pool = req.pool;
    const query = `
      SELECT 
        SupplierCode, 
        SupplierName, 
        Address1, 
        Address2
      FROM tblSupplier
      ORDER BY SupplierName ASC
    `;
    const result = await pool.request().query(query);
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Suppliers Error:", err);
    res.status(500).json({ success: false, message: "Database error", error: err.message });
  }
});

// GET /api/tradeinissue/tradebills
router.get("/tradebills", async (req, res) => {
  try {
    const pool = req.pool;
    // VB Code: " ((IssueFlag is Null) or (IssueFlag<> 'Y'))"
    const query = `
      SELECT 
        InvoiceNo, 
        InventoryCode, 
        TotalWeight,
        IssuedWeight,
        Balanceweight,
        Amount,
        ModifyDate,
        CustomerCode 
      FROM tblTradeInHeader
      WHERE IssueFlag IS NULL OR IssueFlag <> 'Y'
      ORDER BY InvoiceNo DESC
    `;
    const result = await pool.request().query(query);
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Trade Bills Error:", err);
    res.status(500).json({ success: false, message: "Database error", error: err.message });
  }
});

// GET /api/tradeinissue/history
router.get("/history", async (req, res) => {
  try {
    const pool = req.pool;
    const query = `
      SELECT DISTINCT
        h.TradeIssueNo, 
        h.SupplierCode,
        s.SupplierName
      FROM tblTradeIssueHistory h
      LEFT JOIN tblSupplier s ON h.SupplierCode = s.SupplierCode
      ORDER BY h.TradeIssueNo DESC
    `;
    const result = await pool.request().query(query);
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Trade Issue History Error:", err);
    res.json({ success: true, data: [] }); // return empty on error so it doesn't break
  }
});

// GET /api/tradeinissue/nextIssueNo
router.get("/nextIssueNo", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query("SELECT TOP 1 LocationCode, NextTradeIssueNo FROM tblCompany");
    
    let nextSeq = 1;
    let loc = "HQ";
    if (result.recordset.length > 0) {
      nextSeq = parseInt(result.recordset[0].NextTradeIssueNo) || 1;
      loc = (result.recordset[0].LocationCode || "HQ").trim();
    }
    const nextCode = `TI-${loc}${String(nextSeq).padStart(8, "0")}`;
    res.json({ success: true, data: nextCode });
  } catch (err) {
    console.error("Get Next Trade Issue No Error:", err);
    res.status(500).json({ success: false, message: "Database error", error: err.message });
  }
});

// POST /api/tradeinissue/proceed
router.post("/proceed", async (req, res) => {
  const pool = req.pool;
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

    if (!supplierCode) {
      return res.status(400).json({ success: false, message: "Supplier is required." });
    }
    if (!tradeBills || tradeBills.length === 0) {
      return res.status(400).json({ success: false, message: "At least one trade bill is required." });
    }

    await transaction.begin();

    // The original VB code commented out the explicit insert into TradeIssueHistoryHeader.
    // It seems the TradeIssueNo on tblTradeInHeader is sufficient, or a trigger handles history.

    // Process each trade bill
    let remainingPurchaseWeight = parseFloat(totalIssuedWt);
    for (const bill of tradeBills) {
      const billBalanceWt = parseFloat(bill.BalanceWeight || 0);
      let issueWtForBill = 0;

      if (remainingPurchaseWeight >= billBalanceWt) {
        issueWtForBill = billBalanceWt;
        remainingPurchaseWeight -= billBalanceWt;
      } else {
        issueWtForBill = remainingPurchaseWeight;
        remainingPurchaseWeight = 0;
      }

      const reqUpdateBill = new sql.Request(transaction);
      await reqUpdateBill
        .input("InvoiceNo", sql.VarChar, bill.InvoiceNo)
        .input("IssueWt", sql.Numeric(18, 4), issueWtForBill)
        .input("TradeIssueNo", sql.VarChar, tradeIssueNo.trim())
        .input("SupplierCode", sql.VarChar, supplierCode.trim())
        .query(`
          UPDATE tblTradeInHeader 
          SET 
            IssuedWeight = ISNULL(IssuedWeight, 0) + @IssueWt,
            Balanceweight = ISNULL(Balanceweight, 0) - @IssueWt,
            IssueFlag = CASE WHEN ISNULL(Balanceweight, 0) - @IssueWt <= 0 THEN 'Y' ELSE IssueFlag END,
            TradeIssueNo = @TradeIssueNo,
            ModifyDate = GETDATE()
          WHERE InvoiceNo = @InvoiceNo
        `);
      
      if (remainingPurchaseWeight <= 0) break;
    }

    // Update Company Sequence
    const reqUpdateCompany = new sql.Request(transaction);
    await reqUpdateCompany.query(`
      UPDATE tblCompany SET NextTradeIssueNo = ISNULL(NextTradeIssueNo, 1) + 1
    `);

    await transaction.commit();
    res.json({ success: true, message: "Tradein Gold Issued Successfully" });

  } catch (err) {
    if (transaction) await transaction.rollback();
    console.error("TradeIn Issue Save Error:", err);
    res.status(500).json({ success: false, message: "Error in save", error: err.message });
  }
});

module.exports = router;
