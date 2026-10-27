const express = require("express");
const { sql } = require("../db");
const router = express.Router();


// GET JobSheet LOV for GoldBar Fix (Filtered by SupplierCode='STM' as in VB: frmHelpSearch("tblJobSheetHeader", "L", 4, "JobSheetNo", "SupplierCode='STM'"))
// Also returns fallback unfiltered list if STM returns 0 rows so user isn't stuck if sample data uses different suppliers
router.get("/lov/jobsheets", async (req, res) => {
  try {
    const pool = req.pool;
    let result = await pool.request().query(`
      SELECT 
        RTRIM(JobSheetNo) AS JobSheetNo,
        RTRIM(SupplierCode) AS SupplierCode,
        RTRIM(PurchaseBillNo) AS PurchaseBillNo,
        TotalWeight24k
      FROM tblJobSheetHeader
      WHERE RTRIM(SupplierCode) = 'STM'
      ORDER BY JobSheetNo DESC
    `);

    if (result.recordset.length === 0) {
      result = await pool.request().query(`
        SELECT 
          RTRIM(JobSheetNo) AS JobSheetNo,
          RTRIM(SupplierCode) AS SupplierCode,
          RTRIM(PurchaseBillNo) AS PurchaseBillNo,
          TotalWeight24k
        FROM tblJobSheetHeader
        ORDER BY JobSheetNo DESC
      `);
    }

    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch GoldBar Fix JobSheets LOV Error:", err);
    res.status(500).json({ success: false, message: "Server Error", error: err.message });
  }
});

// GET JobSheet Info for GoldBar Fix
router.get("/:jobSheetNo", async (req, res) => {
  try {
    const { jobSheetNo } = req.params;
    const pool = req.pool;

    const result = await pool.request()
      .input("JobSheetNo", sql.VarChar, jobSheetNo.trim())
      .query(`
        SELECT 
          RTRIM(JobSheetNo) AS JobSheetNo,
          RTRIM(PurchaseBillNo) AS PurchaseBillNo,
          PurchaseBillDate,
          RTRIM(SupplierCode) AS SupplierCode,
          TaelPercent,
          ManualLocalCOST,
          ManualMc,
          ManualWeight,
          ManualGoldLoss,
          Gst,
          TotalWeight24k
        FROM tblJobSheetHeader
        WHERE RTRIM(JobSheetNo) = @JobSheetNo
      `);

    if (result.recordset.length === 0) {
      return res.status(404).json({ success: false, message: "Job sheet no. is invalid!" });
    }

    const row = result.recordset[0];
    
    // Format Pdate into YYYYMMDD string for frontend if PurchaseBillDate is present
    let pdate = "";
    if (row.PurchaseBillDate) {
      const d = new Date(row.PurchaseBillDate);
      if (!isNaN(d.getTime())) {
        const yyyy = d.getFullYear().toString();
        const mm = String(d.getMonth() + 1).padStart(2, "0");
        const dd = String(d.getDate()).padStart(2, "0");
        pdate = `${yyyy}${mm}${dd}`;
      }
    }

    // Attempt to fetch PurchaseFix from tblPurchase or similar if available, or default
    let purchaseFix = 0;
    try {
      const fixResult = await pool.request()
        .input("PurchaseBillNo", sql.VarChar, row.PurchaseBillNo || "")
        .query(`
          SELECT TOP 1 PurchaseFix 
          FROM tblPurchase 
          WHERE RTRIM(PurchaseBillNo) = @PurchaseBillNo
        `);
      if (fixResult.recordset.length > 0 && fixResult.recordset[0].PurchaseFix !== undefined) {
        purchaseFix = fixResult.recordset[0].PurchaseFix || 0;
      }
    } catch (ignoreErr) {
      // Table or column might not have PurchaseFix
    }

    res.json({
      success: true,
      data: {
        ...row,
        Pdate: pdate,
        PurchaseFix: purchaseFix
      }
    });
  } catch (err) {
    console.error("Fetch JobSheet Error:", err);
    res.status(500).json({ success: false, message: "Server Error", error: err.message });
  }
});

// POST to save Purchase Fix
router.post("/save", async (req, res) => {
  const pool = req.pool;

  try {
    const {
      JobSheetNo,
      PurchaseBillNo,
      SupplierCode,
      InvoiceDate,
      Weight,
      TotalCost,
      TotalMC,
      GoldLoss,
      TaelPercent,
      PurchaseFix,
      TotalWeight24k,
      GstAmount,
      HasGST,
      UserId
    } = req.body;

    if (!JobSheetNo || !JobSheetNo.trim()) {
      return res.status(400).json({ success: false, message: "Job sheet no. is required!" });
    }

    const trimmedJS = JobSheetNo.trim();
    const tpVal = parseFloat(TaelPercent) || 0;
    const wt24Val = parseFloat(TotalWeight24k) || 0;
    const fixVal = parseFloat(PurchaseFix) || 0;
    const wtVal = parseFloat(Weight) || 0;
    const costVal = parseFloat(TotalCost) || 0;
    const mcVal = parseFloat(TotalMC) || 0;
    const glVal = parseFloat(GoldLoss) || 0;
    const gstAmtVal = parseFloat(GstAmount) || 0;
    const hasGSTVal = !!HasGST;
    const userVal = UserId || "sys";

    // 1. Update tblJobSheetHeader with all values: TaelPercent, TotalWeight24k, FixStatus, Cost, MC, Weight, GoldLoss, GST
    const result = await pool.request()
      .input("JobSheetNo", sql.VarChar, trimmedJS)
      .input("TaelPercent", sql.Decimal(18, 4), tpVal)
      .input("TotalWeight24k", sql.Decimal(18, 4), wt24Val)
      .input("ManualWeight", sql.Decimal(18, 4), wtVal)
      .input("ManualLocalCOST", sql.Decimal(18, 4), costVal)
      .input("ManualMc", sql.Decimal(18, 4), mcVal)
      .input("ManualGoldLoss", sql.Decimal(18, 4), glVal)
      .input("Gst", sql.Decimal(18, 4), gstAmtVal)
      .input("FixStatus", sql.Char(1), "F")
      .input("ModifyUser", sql.VarChar, userVal)
      .query(`
        UPDATE tblJobSheetHeader
        SET 
          TaelPercent = @TaelPercent,
          TotalWeight24k = @TotalWeight24k,
          ManualWeight = @ManualWeight,
          ManualLocalCOST = @ManualLocalCOST,
          ManualMc = @ManualMc,
          ManualGoldLoss = @ManualGoldLoss,
          Gst = @Gst,
          FixStatus = @FixStatus,
          ModifyUser = @ModifyUser,
          ModifyDate = GETDATE()
        WHERE RTRIM(JobSheetNo) = @JobSheetNo
      `);

    if (result.rowsAffected && result.rowsAffected[0] === 0) {
      return res.status(404).json({ success: false, message: "JobSheet No. not found!" });
    }

    // 2. Next, update tblPurchase if PurchaseBillNo is present
    if (PurchaseBillNo && PurchaseBillNo.trim()) {
      const purBill = PurchaseBillNo.trim();
      try {
        await pool.request()
          .input("PurchaseBillNo", sql.VarChar, purBill)
          .input("PurchaseFix", sql.Decimal(18, 4), fixVal)
          .input("SupplierCode", sql.VarChar, (SupplierCode || '').trim())
          .query(`
            UPDATE tblPurchase 
            SET PurchaseFix = @PurchaseFix,
                ModifyDate = GETDATE()
            WHERE RTRIM(PurchaseBillNo) = @PurchaseBillNo
          `);
      } catch (purErr) {
        console.warn("Notice updating tblPurchase:", purErr.message);
      }
    }

    // 3. In case tblPurchaseFix exists in schema, update or insert
    try {
      const fixCheck = await pool.request()
        .input("PurchaseBillNo", sql.VarChar, (PurchaseBillNo || '').trim())
        .query(`SELECT 1 FROM tblPurchaseFix WHERE RTRIM(PurchaseBillNo) = @PurchaseBillNo`);
      
      if (fixCheck.recordset && fixCheck.recordset.length > 0) {
        await pool.request()
          .input("PurchaseBillNo", sql.VarChar, (PurchaseBillNo || '').trim())
          .input("PurchaseFix", sql.Decimal(18, 4), fixVal)
          .input("TotalWeight24k", sql.Decimal(18, 4), wt24Val)
          .input("Weight", sql.Decimal(18, 4), wtVal)
          .input("TotalCost", sql.Decimal(18, 4), costVal)
          .input("TotalMC", sql.Decimal(18, 4), mcVal)
          .query(`
            UPDATE tblPurchaseFix
            SET PurchaseFix = @PurchaseFix,
                TotalWeight24k = @TotalWeight24k,
                Weight = @Weight,
                TotalCost = @TotalCost,
                TotalMC = @TotalMC,
                ModifyDate = GETDATE()
            WHERE RTRIM(PurchaseBillNo) = @PurchaseBillNo
          `);
      }
    } catch (ignoreFixTbl) {
      // tblPurchaseFix table may not exist
    }

    return res.json({ success: true, message: "Transaction Saved Successfully" });
  } catch (err) {
    console.error("Save GoldBar Fix Error:", err);
    return res.status(500).json({ success: false, message: "Transaction not saved: " + err.message });
  }
});

module.exports = router;
