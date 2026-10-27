const express = require("express");
const router = express.Router();
const { sql } = require("../db");

// Get next Supplier Payment Number
router.get("/nextNo", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query(`
      SELECT LocationCode, NextSupplierPayment
      FROM tblCompany
    `);

    if (result.recordset.length > 0) {
      const company = result.recordset[0];
      const loc = company.LocationCode || "HQ";
      const nextNo = String(company.NextSupplierPayment || 1).padStart(8, "0");
      res.json({ success: true, nextNo: `${loc}${nextNo}` });
    } else {
      res.json({ success: true, nextNo: "HQ00000001" });
    }
  } catch (err) {
    console.error("Get next Supplier Payment No Error:", err);
    res.status(500).json({ success: false, message: "Database Error", error: err.message });
  }
});

// Fetch jobsheets list matching filter criteria
router.get("/fetch", async (req, res) => {
  try {
    const pool = req.pool;
    const { SupplierCode, FromDate, ToDate, PaidStatus } = req.query;

    if (!SupplierCode) {
      return res.status(400).json({ success: false, message: "SupplierCode is required." });
    }

    let query = `
      SELECT 
        JobSheetNo,
        PurchaseBillNo,
        PurchaseBillDate,
        (ISNULL(TotalCost, 0) + ISNULL(GST, 0) + ISNULL(ManualAdjustment, 0)) AS TotalCost,
        ISNULL(PaidAmount, 0) AS PaidAmount,
        ISNULL(BalanceAmount, 0) AS BalanceAmount,
        Remarks 
      FROM tblJobSheetHeader
      WHERE SupplierCode = @SupplierCode
    `;

    const request = pool.request().input("SupplierCode", sql.VarChar, SupplierCode);

    if (FromDate) {
      query += " AND CONVERT(CHAR(10), JobSheetDate, 112) >= @FromDate";
      request.input("FromDate", sql.VarChar, FromDate);
    }
    if (ToDate) {
      query += " AND CONVERT(CHAR(10), JobSheetDate, 112) <= @ToDate";
      request.input("ToDate", sql.VarChar, ToDate);
    }

    if (PaidStatus === "Paid") {
      query += " AND ISNULL(BalanceAmount, 0) = 0";
    } else if (PaidStatus === "UnPaid") {
      query += " AND ISNULL(BalanceAmount, 0) > 0";
    }

    const result = await request.query(query);
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Jobsheets error for supplier payment:", err);
    res.status(500).json({ success: false, message: "Database Error", error: err.message });
  }
});

// Add payment transaction
router.post("/", async (req, res) => {
  const pool = req.pool;
  const transaction = new sql.Transaction(pool);
  try {
    const {
      PaymentNo, PaymentDate, PaymentAmount, Remarks, InvoiceNo,
      InvoiceDate, SupplierCode, Paymode, UserID
    } = req.body;

    if (!PaymentNo || !InvoiceNo || !SupplierCode || !Paymode || !PaymentAmount) {
      return res.status(400).json({ success: false, message: "Required fields are missing." });
    }

    await transaction.begin();
    const req1 = new sql.Request(transaction);

    // 1. Insert into tbljobsheetpaymentdetail
    await req1
      .input("PaymentNo", sql.VarChar, PaymentNo)
      .input("PaymentDate", sql.VarChar, PaymentDate)
      .input("PaymentAmount", sql.Numeric(18, 2), parseFloat(PaymentAmount))
      .input("Remarks", sql.VarChar, Remarks || "")
      .input("InvoiceNo", sql.VarChar, InvoiceNo)
      .input("InvoiceDate", sql.SmallDateTime, InvoiceDate ? new Date(InvoiceDate) : new Date())
      .input("SupplierCode", sql.VarChar, SupplierCode)
      .input("Paymode", sql.VarChar, Paymode)
      .input("UserID", sql.VarChar, UserID || "sys")
      .query(`
        INSERT INTO tbljobsheetpaymentdetail (
          PaymentNo, PaymentDate, PaymentAmount, Remarks, InvoiceNo,
          InvoiceDate, VendorCode, Paymode, CreateUser, CreateDate, ModifyUser, ModifyDate
        ) VALUES (
          @PaymentNo, @PaymentDate, @PaymentAmount, @Remarks, @InvoiceNo,
          @InvoiceDate, @SupplierCode, @Paymode, @UserID, GETDATE(), @UserID, GETDATE()
        )
      `);

    // 2. Update tblJobSheetHeader PaidAmount and BalanceAmount
    const req2 = new sql.Request(transaction);
    await req2
      .input("InvoiceNo", sql.VarChar, InvoiceNo)
      .input("SupplierCode", sql.VarChar, SupplierCode)
      .input("PaymentAmount", sql.Numeric(18, 2), parseFloat(PaymentAmount))
      .query(`
        UPDATE tblJobSheetHeader 
        SET PaidAmount = ISNULL(PaidAmount, 0) + @PaymentAmount,
            BalanceAmount = ISNULL(BalanceAmount, 0) - @PaymentAmount
        WHERE PurchaseBillNo = @InvoiceNo AND SupplierCode = @SupplierCode
      `);

    // 3. Increment NextSupplierPayment in tblCompany
    const req3 = new sql.Request(transaction);
    await req3.query(`
      UPDATE tblCompany 
      SET NextSupplierPayment = ISNULL(NextSupplierPayment, 1) + 1
    `);

    await transaction.commit();
    res.json({ success: true, message: "Payment Successful" });
  } catch (err) {
    console.error("Save Supplier Payment Error:", err);
    try {
      await transaction.rollback();
    } catch (ex) {}
    res.status(500).json({ success: false, message: "Transaction failed", error: err.message });
  }
});

// Help query details for goldbar issue or trade-in issue
router.get("/issueDetail", async (req, res) => {
  try {
    const pool = req.pool;
    const { Type, IssueNo } = req.query;

    if (!Type || !IssueNo) {
      return res.status(400).json({ success: false, message: "Type and IssueNo are required." });
    }

    if (Type === "GOLD BAR") {
      const result = await pool.request()
        .input("IssueNo", sql.VarChar, IssueNo)
        .query("SELECT IssuedWeight FROM tblGoldBarIssued WHERE IssueNo = @IssueNo");
      
      if (result.recordset.length > 0) {
        return res.json({ success: true, data: { Remarks: result.recordset[0].IssuedWeight } });
      }
    } else if (Type === "TRADE-IN") {
      const result = await pool.request()
        .input("IssueNo", sql.VarChar, IssueNo)
        .query("SELECT TotalWeight, Purity, TotalPrice FROM tblTradeissuehistoryHeader WHERE TradeIssueNo = @IssueNo");
      
      if (result.recordset.length > 0) {
        const row = result.recordset[0];
        return res.json({ 
          success: true, 
          data: { 
            Remarks: `${row.TotalWeight || 0} Purity ${row.Purity || ''}`,
            Amount: row.TotalPrice || 0
          } 
        });
      }
    }
    res.json({ success: false, message: "Issue details not found." });
  } catch (err) {
    console.error("Fetch Issue details error:", err);
    res.status(500).json({ success: false, message: "Database Error", error: err.message });
  }
});

module.exports = router;
