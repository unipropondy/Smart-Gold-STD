const express = require("express");
const router = express.Router();
const { sql } = require("../db");

// Helper to format date YYYY-MM-DD
const formatDate = (d) => {
  if (!d) return null;
  const dateObj = new Date(d);
  if (isNaN(dateObj.getTime())) return null;
  return dateObj.toISOString().split("T")[0];
};

// GET Fetch daily sales by InvoiceDate
router.get("/", async (req, res) => {
  try {
    const { date } = req.query;
    if (!date) {
      return res.status(400).json({ success: false, message: "Date parameter is required" });
    }

    const formattedDate = formatDate(date);
    if (!formattedDate) {
      return res.status(400).json({ success: false, message: "Invalid date format" });
    }

    const pool = req.pool;
    const result = await pool.request()
      .input("InvoiceDate", sql.SmallDateTime, formattedDate)
      .query("SELECT * FROM tblDailySales WHERE InvoiceDate = @InvoiceDate");

    if (result.recordset.length > 0) {
      res.json({ success: true, data: result.recordset[0] });
    } else {
      res.json({ success: true, data: null, message: "No record found for this date" });
    }
  } catch (err) {
    console.error("Get Daily Sales Error:", err);
    res.status(500).json({ success: false, message: "Database error", error: err.message });
  }
});

// POST Save/Upsert daily sales record
router.post("/save", async (req, res) => {
  try {
    const pool = req.pool;
    const {
      InvoiceDate, Cash, Nets, Visa, Master, Amex, Diners, Chq, TransfertoAcc, Installment,
      NonGST24K, TradeInGold, AdvancePayment, PettyCash, AdvanceUtil, TotalGSTSales, GST, TotalSales,
      userId
    } = req.body;

    if (!InvoiceDate) {
      return res.status(400).json({ success: false, message: "InvoiceDate is required" });
    }

    const formattedDate = formatDate(InvoiceDate);
    if (!formattedDate) {
      return res.status(400).json({ success: false, message: "Invalid date format" });
    }

    const nCash = parseFloat(Cash) || 0;
    const nNets = parseFloat(Nets) || 0;
    const nVisa = parseFloat(Visa) || 0;
    const nMaster = parseFloat(Master) || 0;
    const nAmex = parseFloat(Amex) || 0;
    const nDiners = parseFloat(Diners) || 0;
    const nChq = parseFloat(Chq) || 0;
    const nTransfer = parseFloat(TransfertoAcc) || 0;
    const nInstallment = parseFloat(Installment) || 0;
    const nNonGST24K = parseFloat(NonGST24K) || 0;
    const nTradeInGold = parseFloat(TradeInGold) || 0;
    const nAdvancePayment = parseFloat(AdvancePayment) || 0;
    const nPettyCash = parseFloat(PettyCash) || 0;
    const nAdvanceUtil = parseFloat(AdvanceUtil) || 0;

    const hasAnyValue = (
      nCash !== 0 || nNets !== 0 || nVisa !== 0 || nMaster !== 0 ||
      nAmex !== 0 || nDiners !== 0 || nChq !== 0 || nTransfer !== 0 ||
      nInstallment !== 0 || nNonGST24K !== 0 || nTradeInGold !== 0 ||
      nAdvancePayment !== 0 || nPettyCash !== 0 || nAdvanceUtil !== 0
    );

    if (!hasAnyValue) {
      return res.status(400).json({ success: false, message: "Please enter at least one amount before saving!" });
    }

    const sysUser = (userId || "sys").substring(0, 10);

    // Check if record exists
    const checkRes = await pool.request()
      .input("InvoiceDate", sql.SmallDateTime, formattedDate)
      .query("SELECT 1 FROM tblDailySales WHERE InvoiceDate = @InvoiceDate");

    const exists = checkRes.recordset.length > 0;

    const request = pool.request()
      .input("InvoiceDate", sql.SmallDateTime, formattedDate)
      .input("Cash", sql.Numeric(18, 4), parseFloat(Cash) || 0)
      .input("Nets", sql.Numeric(18, 4), parseFloat(Nets) || 0)
      .input("Visa", sql.Numeric(18, 4), parseFloat(Visa) || 0)
      .input("Master", sql.Numeric(18, 4), parseFloat(Master) || 0)
      .input("Amex", sql.Numeric(18, 4), parseFloat(Amex) || 0)
      .input("Diners", sql.Numeric(18, 4), parseFloat(Diners) || 0)
      .input("Chq", sql.Numeric(18, 4), parseFloat(Chq) || 0)
      .input("TransfertoAcc", sql.Numeric(18, 4), parseFloat(TransfertoAcc) || 0)
      .input("Installment", sql.Numeric(18, 4), parseFloat(Installment) || 0)
      .input("NonGST24K", sql.Numeric(18, 4), parseFloat(NonGST24K) || 0)
      .input("TradeInGold", sql.Numeric(18, 4), parseFloat(TradeInGold) || 0)
      .input("AdvancePayment", sql.Numeric(18, 4), parseFloat(AdvancePayment) || 0)
      .input("PettyCash", sql.Numeric(18, 4), parseFloat(PettyCash) || 0)
      .input("AdvanceUtil", sql.Numeric(18, 4), parseFloat(AdvanceUtil) || 0)
      .input("TotalGSTSales", sql.Numeric(18, 4), parseFloat(TotalGSTSales) || 0)
      .input("GST", sql.Numeric(18, 4), parseFloat(GST) || 0)
      .input("TotalSales", sql.Numeric(18, 4), parseFloat(TotalSales) || 0)
      .input("User", sql.VarChar, sysUser);

    if (exists) {
      await request.query(`
        UPDATE tblDailySales SET
          Cash = @Cash,
          Nets = @Nets,
          Visa = @Visa,
          Master = @Master,
          Amex = @Amex,
          Diners = @Diners,
          Chq = @Chq,
          TransfertoAcc = @TransfertoAcc,
          Installment = @Installment,
          NonGST24K = @NonGST24K,
          TradeInGold = @TradeInGold,
          AdvancePayment = @AdvancePayment,
          PettyCash = @PettyCash,
          AdvanceUtil = @AdvanceUtil,
          TotalGSTSales = @TotalGSTSales,
          GST = @GST,
          TotalSales = @TotalSales,
          ModifyUser = @User,
          ModifyDate = GETDATE()
        WHERE InvoiceDate = @InvoiceDate
      `);
      res.json({ success: true, message: "DailySales Updated Successfully!" });
    } else {
      await request.query(`
        INSERT INTO tblDailySales (
          InvoiceDate, Cash, Nets, Visa, Master, Amex, Diners, Chq, TransfertoAcc, Installment, NonGST24K,
          TradeInGold, AdvancePayment, PettyCash, AdvanceUtil, TotalGSTSales, GST, TotalSales,
          CreateUser, CreateDate, ModifyUser, ModifyDate
        ) VALUES (
          @InvoiceDate, @Cash, @Nets, @Visa, @Master, @Amex, @Diners, @Chq, @TransfertoAcc, @Installment, @NonGST24K,
          @TradeInGold, @AdvancePayment, @PettyCash, @AdvanceUtil, @TotalGSTSales, @GST, @TotalSales,
          @User, GETDATE(), @User, GETDATE()
        )
      `);
      res.json({ success: true, message: "DailySales Created Successfully!" });
    }
  } catch (err) {
    console.error("Save Daily Sales Error:", err);
    res.status(500).json({ success: false, message: "Failed to save record", error: err.message });
  }
});

// DELETE daily sales by date
router.delete("/:date", async (req, res) => {
  try {
    const { date } = req.params;
    const formattedDate = formatDate(date);
    if (!formattedDate) {
      return res.status(400).json({ success: false, message: "Invalid date format" });
    }

    const pool = req.pool;
    const result = await pool.request()
      .input("InvoiceDate", sql.SmallDateTime, formattedDate)
      .query("DELETE FROM tblDailySales WHERE InvoiceDate = @InvoiceDate");

    if (result.rowsAffected[0] === 0) {
      return res.status(404).json({ success: false, message: "Record not found" });
    }

    res.json({ success: true, message: "DailySales deleted successfully" });
  } catch (err) {
    console.error("Delete Daily Sales Error:", err);
    res.status(500).json({ success: false, message: "Failed to delete record", error: err.message });
  }
});

module.exports = router;
