/**
 * Developer: durairaj
 */

const express = require("express");
const { sql, poolPromise } = require("../db");
const router = express.Router();

// GET JobSheet LOV for GoldBar Fix (matching frmHelpSearch("tblJobSheetHeader", "L", 4, "JobSheetNo", "SupplierCode='STM'"))
router.get("/lov/jobsheets", async (req, res) => {
  try {
    const pool = req.pool || (await poolPromise);
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

// GET JobSheet Info for GoldBar Fix matching subGetJobSheet in frmGoldBarFix.vb
router.get("/:jobSheetNo", async (req, res) => {
  try {
    const { jobSheetNo } = req.params;
    const pool = req.pool || (await poolPromise);

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

    // Attempt to fetch PurchaseFix from tblPurchase matching GetPurchaseFix in frmGoldBarFix.vb
    let purchaseFix = 0;
    try {
      const fixResult = await pool.request()
        .input("PurchaseBillNo", sql.VarChar, (row.PurchaseBillNo || "").trim())
        .query(`
          SELECT TOP 1 PurchaseFix 
          FROM tblPurchase 
          WHERE RTRIM(PurchaseBillNo) = @PurchaseBillNo
        `);
      if (fixResult.recordset.length > 0 && fixResult.recordset[0].PurchaseFix !== undefined) {
        purchaseFix = fixResult.recordset[0].PurchaseFix || 0;
      }
    } catch (ignoreErr) {}

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

// POST to save Purchase Fix matching fncInventory() & btnSave_Click in frmGoldBarFix.vb
router.post("/save", async (req, res) => {
  const pool = req.pool || (await poolPromise);

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
    const userVal = UserId || "sys";

    // Inspect columns of tblJobSheetHeader dynamically to avoid errors on schema variations
    let jsCols = [];
    try {
      const colRes = await pool.request().query(`
        SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tblJobSheetHeader'
      `);
      jsCols = colRes.recordset.map(c => c.COLUMN_NAME.toLowerCase());
    } catch (e) {}

    const hasCol = (col) => jsCols.length === 0 || jsCols.includes(col.toLowerCase());

    const updateFields = [];
    const jsReq = pool.request().input("JobSheetNo", sql.VarChar, trimmedJS);

    if (hasCol("TaelPercent")) {
      updateFields.push("TaelPercent = @TaelPercent");
      jsReq.input("TaelPercent", sql.Decimal(18, 4), tpVal);
    }
    if (hasCol("TotalWeight24k")) {
      updateFields.push("TotalWeight24k = @TotalWeight24k");
      jsReq.input("TotalWeight24k", sql.Decimal(18, 4), wt24Val);
    }
    if (hasCol("ManualWeight")) {
      updateFields.push("ManualWeight = @ManualWeight");
      jsReq.input("ManualWeight", sql.Decimal(18, 4), wtVal);
    }
    if (hasCol("ManualLocalCOST")) {
      updateFields.push("ManualLocalCOST = @ManualLocalCOST");
      jsReq.input("ManualLocalCOST", sql.Decimal(18, 4), costVal);
    }
    if (hasCol("ManualMc")) {
      updateFields.push("ManualMc = @ManualMc");
      jsReq.input("ManualMc", sql.Decimal(18, 4), mcVal);
    }
    if (hasCol("ManualGoldLoss")) {
      updateFields.push("ManualGoldLoss = @ManualGoldLoss");
      jsReq.input("ManualGoldLoss", sql.Decimal(18, 4), glVal);
    }
    if (hasCol("Gst")) {
      updateFields.push("Gst = @Gst");
      jsReq.input("Gst", sql.Decimal(18, 4), gstAmtVal);
    }
    if (hasCol("FixStatus")) {
      updateFields.push("FixStatus = @FixStatus");
      jsReq.input("FixStatus", sql.Char(1), "F");
    }
    if (hasCol("ModifyUser")) {
      updateFields.push("ModifyUser = @ModifyUser");
      jsReq.input("ModifyUser", sql.VarChar, userVal);
    }
    if (hasCol("ModifyDate")) {
      updateFields.push("ModifyDate = GETDATE()");
    }

    if (updateFields.length > 0) {
      const result = await jsReq.query(`
        UPDATE tblJobSheetHeader
        SET ${updateFields.join(", ")}
        WHERE RTRIM(JobSheetNo) = @JobSheetNo
      `);

      if (result.rowsAffected && result.rowsAffected[0] === 0) {
        return res.status(404).json({ success: false, message: "JobSheet No. not found!" });
      }
    }

    // 2. Update tblPurchase if PurchaseBillNo is present
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

    return res.json({ success: true, message: "Transaction Saved Successfully" });
  } catch (err) {
    console.error("Save GoldBar Fix Error:", err);
    return res.status(500).json({ success: false, message: "Transaction not saved: " + err.message });
  }
});

module.exports = router;

module.exports = router;
