const express = require("express");
const { sql } = require("../db");
const router = express.Router();

// ================= GET TAEL PERCENT (LOV) =================
router.get("/lov/taelpercent", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query(`
      SELECT 
        TaelPercent,
        TaelPercent1,
        TaelPercent2,
        TaelPercent3
      FROM TblTaelPercent
    `);
    return res.status(200).json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("🔥 GET TAEL PERCENT ERROR:", err);
    return res.status(500).json({ success: false, message: "Server Error", error: err.message });
  }
});

// ================= GET NEXT JOBSHEET NO =================
router.get(["/nextNo", "/next-number"], async (req, res) => {
  try {
    const pool = req.pool;

    const result = await pool.request().query(`
      SELECT LocationCode, NextJobSheetNo
      FROM tblCompany
    `);

    if (result.recordset.length > 0) {
      const company = result.recordset[0];

      const locationCode = company.LocationCode || "";
      const nextJobSheetNo = String(company.NextJobSheetNo || 0).padStart(8, "0");

      const formattedNo = `${locationCode}${nextJobSheetNo}`;

      res.json({
        success: true,
        nextNo: formattedNo
      });
    } else {
      res.json({
        success: false,
        message: "Company details not found"
      });
    }
  } catch (err) {
    console.error("Get next JobSheet number error:", err);

    res.status(500).json({
      success: false,
      message: "Database Error",
      error: err.message
    });
  }
});
// router.get("/nextNo", async (req, res) => {
//   try {
//     const pool = req.pool;
//     const result = await pool.request().query("SELECT NextJobSheetNo FROM tblCompany");
//     let nextNo = 1;
//     if (result.recordset.length > 0 && result.recordset[0].NextJobSheetNo) {
//       nextNo = parseInt(result.recordset[0].NextJobSheetNo, 10);
//     }
//     const formattedNo = `JS${String(nextNo).padStart(6, '0')}`;
//     res.json({ success: true, nextNo: formattedNo });
//   } catch (err) {
//     console.error("Get next JobSheet number error:", err);
//     res.status(500).json({ success: false, message: "Database Error", error: err.message });
//   }
// });

// ================= GET JOBSHEET (LOV) =================
router.get("/lov/jobsheet", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query(`
      SELECT 
        JobSheetNo,
        SupplierCode,
        PurchaseBillNo,
        TotalWeight24k
      FROM tblJobSheetHeader
    `);
    return res.status(200).json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("🔥 GET JOBSHEET LOV ERROR:", err);
    return res.status(500).json({ success: false, message: "Server Error", error: err.message });
  }
});
router.get("/:jobSheetNo", async (req, res) => {
  try {
    const { jobSheetNo } = req.params;
    const pool = req.pool;

    const headerResult = await pool.request()
      .input("JobSheetNo", sql.VarChar, jobSheetNo)
      .query(`
        SELECT h.*, s.SupplierName, c.CurrencyName
        FROM tblJobSheetHeader h
        LEFT JOIN tblSupplier s ON h.SupplierCode = s.SupplierCode
        LEFT JOIN tblCurrency c ON h.CurrencyCode = c.CurrencyCode
        WHERE h.JobSheetNo = @JobSheetNo
      `);

    if (headerResult.recordset.length === 0) {
      return res.status(404).json({ success: false, message: "JobSheet not found" });
    }

    console.log("👉 ACTUAL DB COLUMNS FOR HEADER:", Object.keys(headerResult.recordset[0]));

    const detailsResult = await pool.request()
      .input("JobSheetNo", sql.VarChar, jobSheetNo)
      .query(`
        SELECT d.*, c.NextInventoryCode 
        FROM tblJobSheetDetail d
        LEFT JOIN tblCategory c ON d.CategoryCode = c.CategoryCode
        WHERE d.JobSheetNo = @JobSheetNo
      `);

    // Process auto-increment for duplicate category codes
    let categoryCounters = {};
    let processedDetails = detailsResult.recordset.map(d => {
      let currentCode = d.NextInventoryCode;

      if (currentCode) {
        if (!categoryCounters[d.CategoryCode]) {
          categoryCounters[d.CategoryCode] = currentCode;
        } else {
          currentCode = categoryCounters[d.CategoryCode];
        }

        // Increment for next item in same category
        const match = currentCode.match(/^([A-Za-z]+)(\d+)$/);
        if (match) {
          const prefix = match[1];
          const numStr = match[2];
          const num = parseInt(numStr, 10);
          const nextNum = num + 1;
          const nextStr = nextNum.toString().padStart(numStr.length, '0');
          categoryCounters[d.CategoryCode] = prefix + nextStr;
        }
      }

      return {
        ...d,
        NextInventoryCode: currentCode
      };
    });

    const inventoryResult = await pool.request()
      .input("JobSheetNo", sql.VarChar, jobSheetNo)
      .query(`
        SELECT * FROM tblInventory WHERE JobSheetNo = @JobSheetNo
      `);

    return res.status(200).json({
      success: true,
      header: headerResult.recordset[0],
      details: processedDetails,
      inventory: inventoryResult.recordset
    });
  } catch (err) {
    console.error("🔥 GET JOBSHEET ERROR:", err);
    return res.status(500).json({ success: false, message: "Server Error", error: err.message });
  }
});

// ================= GET ALL JOBSHEETS (LOV) =================
router.get("/", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query(`
      SELECT JobSheetNo, SupplierCode, PurchaseBillNo, TotalWeight24K
      FROM tblJobSheetHeader
      ORDER BY JobSheetNo DESC
    `);
    return res.status(200).json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("🔥 GET ALL JOBSHEETS ERROR:", err);
    return res.status(500).json({ success: false, message: "Server Error", error: err.message });
  }
});

// ================= SAVE/UPDATE JOBSHEET =================
router.post("/", async (req, res) => {
  const pool = req.pool;
  const transaction = new sql.Transaction(pool);

  try {
    const { header, details, userId } = req.body;

    if (!header || !header.JobSheetNo) {
      return res.status(400).json({ success: false, message: "JobSheetNo is required in header" });
    }

    const { JobSheetNo } = header;
    const sysUser = userId || "SYSTEM";
    const curDate = new Date();

    await transaction.begin();

    const request = new sql.Request(transaction);
    const checkHeader = await request
      .input("JobSheetNoCheck", sql.VarChar, JobSheetNo)
      .query(`SELECT 1 FROM tblJobSheetHeader WHERE JobSheetNo = @JobSheetNoCheck`);

    const headerReq = new sql.Request(transaction)
      .input("JobSheetNo", sql.VarChar, JobSheetNo)
      .input("JobSheetDate", sql.SmallDateTime, header.JobSheetDate ? new Date(header.JobSheetDate) : curDate)
      .input("PurchaseBillNo", sql.VarChar, header.PurchaseBillNo || "")
      .input("PurchaseBillDate", sql.SmallDateTime, header.PurchaseBillDate ? new Date(header.PurchaseBillDate) : curDate)
      .input("SupplierCode", sql.VarChar, header.SupplierCode || "")
      .input("TotalWeight24K", sql.Numeric(18, 2), parseFloat(header.TotalWeight24K) || 0)
      .input("TotalMC", sql.Numeric(18, 2), parseFloat(header.TotalMC) || 0)
      .input("TotalPCS", sql.Numeric(18, 2), parseFloat(header.TotalPCS) || 0)
      .input("TotalGoldLoss", sql.Numeric(18, 2), parseFloat(header.TotalGoldLoss) || 0)
      .input("CostPerGram", sql.Numeric(18, 2), parseFloat(header.CostPerGram) || 0)
      .input("CurrencyCode", sql.VarChar, header.CurrencyCode || "SGD")
      .input("CurrencyRate", sql.Numeric(9, 4), parseFloat(header.CurrencyRate) || 1.0)
      .input("GST", sql.Numeric(18, 2), parseFloat(header.GST) || 0)
      .input("TotalCost", sql.Numeric(18, 2), parseFloat(header.TotalCost) || 0)
      .input("PaymentType", sql.Char(1), header.PaymentType ? header.PaymentType.substring(0, 1) : "C")
      .input("BillClose", sql.Char(1), header.BillClose ? header.BillClose.substring(0, 1) : "N")
      .input("Remarks", sql.VarChar, header.Remarks || "")
      .input("TranType", sql.VarChar, header.TranType || "")
      .input("TotalLocalCost", sql.Numeric(18, 2), parseFloat(header.TotalLocalCost) || 0)
      .input("TotalForeignCost", sql.Numeric(18, 2), parseFloat(header.TotalForeignCost) || 0)
      .input("ManualLocalCost", sql.Numeric(18, 2), parseFloat(header.ManualLocalCost) || 0)
      .input("ManualForeignCost", sql.Numeric(18, 2), parseFloat(header.ManualForeignCost) || 0)
      .input("ManualMC", sql.Numeric(18, 2), parseFloat(header.ManualMC) || 0)
      .input("ManualWeight", sql.Numeric(18, 2), parseFloat(header.ManualWeight) || 0)
      .input("ManualGoldLoss", sql.Numeric(18, 2), parseFloat(header.ManualGoldLoss) || 0)
      .input("LocationCode", sql.VarChar, header.LocationCode || "")
      .input("TaelGST", sql.Char(1), header.TaelGST ? header.TaelGST.substring(0, 1) : "N")
      .input("ManualAdjustment", sql.Numeric(18, 2), parseFloat(header.ManualAdjustment) || 0)
      .input("FixStatus", sql.Char(1), header.FixStatus ? header.FixStatus.substring(0, 1) : "U")
      .input("SplCategory", sql.Char(1), header.SplCategory ? "Y" : "N")
      .input("Accessories", sql.Char(1), header.Accessories ? "Y" : "N")
      .input("MatchJobSheetNo", sql.VarChar, header.MatchJobSheetNo || "")
      .input("MatchWeight", sql.Numeric(18, 2), parseFloat(header.MatchWeight) || 0)
      .input("MatchPCS", sql.Numeric(18, 2), parseFloat(header.MatchPCS) || 0)
      .input("SalesOrderNo", sql.VarChar, header.SalesOrderNo || "")
      .input("TaelPercent", sql.Numeric(5, 2), parseFloat(header.TaelPercent) || 0)
      .input("TaelRate", sql.Numeric(18, 2), parseFloat(header.TaelRate) || 0)
      .input("ClassType", sql.VarChar, header.ClassType || "")
      .input("PaidAmount", sql.Numeric(18, 2), parseFloat(header.PaidAmount) || 0)
      .input("BalanceAmount", sql.Numeric(18, 2), parseFloat(header.BalanceAmount) || 0)
      .input("DueDate", sql.SmallDateTime, header.DueDate ? new Date(header.DueDate) : null)
      .input("User", sql.VarChar, sysUser)
      .input("Date", sql.SmallDateTime, curDate);

    if (checkHeader.recordset.length > 0) {
      await headerReq.query(`
        UPDATE tblJobSheetHeader SET
          JobSheetDate = @JobSheetDate, PurchaseBillNo = @PurchaseBillNo, PurchaseBillDate = @PurchaseBillDate,
          SupplierCode = @SupplierCode, TotalWeight24K = @TotalWeight24K, TotalMC = @TotalMC, TotalPCS = @TotalPCS,
          TotalGoldLoss = @TotalGoldLoss, CostPerGram = @CostPerGram, CurrencyCode = @CurrencyCode, CurrencyRate = @CurrencyRate,
          GST = @GST, TotalCost = @TotalCost, PaymentType = @PaymentType, BillClose = @BillClose, Remarks = @Remarks,
          TranType = @TranType, TotalLocalCost = @TotalLocalCost, TotalForeignCost = @TotalForeignCost,
          ManualLocalCost = @ManualLocalCost, ManualForeignCost = @ManualForeignCost, ManualMC = @ManualMC,
          ManualWeight = @ManualWeight, ManualGoldLoss = @ManualGoldLoss, LocationCode = @LocationCode,
          TaelGST = @TaelGST, ModifyUser = @User, ModifyDate = @Date, ManualAdjustment = @ManualAdjustment,
          FixStatus = @FixStatus, SplCategory = @SplCategory, Accessories = @Accessories, MatchJobSheetNo = @MatchJobSheetNo,
          MatchWeight = @MatchWeight, MatchPCS = @MatchPCS, SalesOrderNo = @SalesOrderNo, TaelPercent = @TaelPercent,
          TaelRate = @TaelRate, ClassType = @ClassType, PaidAmount = @PaidAmount, BalanceAmount = @BalanceAmount, DueDate = @DueDate
        WHERE JobSheetNo = @JobSheetNo
      `);
    } else {
      await headerReq.query(`
        INSERT INTO tblJobSheetHeader (
          JobSheetNo, JobSheetDate, PurchaseBillNo, PurchaseBillDate, SupplierCode, TotalWeight24K, TotalMC, TotalPCS,
          TotalGoldLoss, CostPerGram, CurrencyCode, CurrencyRate, GST, TotalCost, PaymentType, BillClose, Remarks,
          TranType, TotalLocalCost, TotalForeignCost, ManualLocalCost, ManualForeignCost, ManualMC, ManualWeight,
          ManualGoldLoss, LocationCode, TaelGST, CreateUser, CreateDate, ModifyUser, ModifyDate, ManualAdjustment,
          FixStatus, SplCategory, Accessories, MatchJobSheetNo, MatchWeight, MatchPCS, SalesOrderNo, TaelPercent,
          TaelRate, ClassType, PaidAmount, BalanceAmount, DueDate
        ) VALUES (
          @JobSheetNo, @JobSheetDate, @PurchaseBillNo, @PurchaseBillDate, @SupplierCode, @TotalWeight24K, @TotalMC, @TotalPCS,
          @TotalGoldLoss, @CostPerGram, @CurrencyCode, @CurrencyRate, @GST, @TotalCost, @PaymentType, @BillClose, @Remarks,
          @TranType, @TotalLocalCost, @TotalForeignCost, @ManualLocalCost, @ManualForeignCost, @ManualMC, @ManualWeight,
          @ManualGoldLoss, @LocationCode, @TaelGST, @User, @Date, @User, @Date, @ManualAdjustment,
          @FixStatus, @SplCategory, @Accessories, @MatchJobSheetNo, @MatchWeight, @MatchPCS, @SalesOrderNo, @TaelPercent,
          @TaelRate, @ClassType, @PaidAmount, @BalanceAmount, @DueDate
        );
        UPDATE tblCompany SET NextJobSheetNo = ISNULL(NextJobSheetNo, 0) + 1;
      `);
    }

    const deleteReq = new sql.Request(transaction)
      .input("JobSheetNo", sql.VarChar, JobSheetNo);
    await deleteReq.query(`DELETE FROM tblJobSheetDetail WHERE JobSheetNo = @JobSheetNo`);

    if (details && details.length > 0) {
      for (const item of details) {
        const detailReq = new sql.Request(transaction)
          .input("JobSheetNo", sql.VarChar, JobSheetNo)
          .input("CatalogueCode", sql.VarChar, item.CatalogueCode || "")
          .input("CategoryCode", sql.VarChar, item.CategoryCode || "")
          .input("Description", sql.VarChar, item.Description || "")
          .input("CostPerGram", sql.Numeric(18, 2), parseFloat(item.CostPerGram) || 0)
          .input("Weight", sql.Numeric(18, 2), parseFloat(item.Weight) || 0)
          .input("MC", sql.Numeric(18, 2), parseFloat(item.MC) || 0)
          .input("PCS", sql.Numeric(18, 2), parseFloat(item.PCS) || 0)
          .input("GoldLoss", sql.Numeric(18, 2), parseFloat(item.GoldLoss) || 0)
          .input("TotalCost", sql.Numeric(18, 2), parseFloat(item.TotalCost) || 0)
          .input("User", sql.VarChar, sysUser)
          .input("Date", sql.SmallDateTime, curDate);

        await detailReq.query(`
          INSERT INTO tblJobSheetDetail (
            JobSheetNo, CatalogueCode, CategoryCode, Description, CostPerGram, Weight, MC, PCS, GoldLoss, TotalCost,
            CreateUser, CreateDate, ModifyUser, ModifyDate
          ) VALUES (
            @JobSheetNo, @CatalogueCode, @CategoryCode, @Description, @CostPerGram, @Weight, @MC, @PCS, @GoldLoss, @TotalCost,
            @User, @Date, @User, @Date
          )
        `);
      }
    }

    await transaction.commit();
    return res.status(200).json({ success: true, message: "JobSheet saved successfully" });
  } catch (err) {
    console.error("🔥 SAVE JOBSHEET ERROR:", err);
    try {
      await transaction.rollback();
    } catch (rbErr) { }
    return res.status(500).json({ success: false, message: "Database update failed", error: err.message });
  }
});

// ================= DELETE JOBSHEET =================
router.delete("/:jobSheetNo", async (req, res) => {
  const pool = req.pool;
  const transaction = new sql.Transaction(pool);

  try {
    const { jobSheetNo } = req.params;
    const pool = req.pool;

    // 1. Check if jobsheet exists and if items exist in tblPurchase or tblInventory
    const chk = await pool.request()
      .input("JobSheetNo", sql.VarChar, jobSheetNo.trim())
      .query(`
        SELECT 
          jh.JobSheetNo,
          jh.BillClose,
          jh.TranType,
          (SELECT COUNT(1) FROM tblPurchase WHERE JobSheetNo = @JobSheetNo) as PurchaseItemCount,
          (SELECT COUNT(1) FROM tblInventory WHERE JobSheetNo = @JobSheetNo) as InventoryItemCount
        FROM tblJobSheetHeader jh
        WHERE jh.JobSheetNo = @JobSheetNo
      `);

    if (chk.recordset.length === 0) {
      return res.status(404).json({ success: false, message: "JobSheet not found" });
    }

    const jsInfo = chk.recordset[0];
    if (jsInfo.BillClose === 'Y') {
      return res.status(400).json({ success: false, message: "This bill is closed. Cannot delete!" });
    }
    if (jsInfo.TranType === 'DP') {
      return res.status(400).json({ success: false, message: "This is a direct purchase bill. Cannot delete from JobSheet!" });
    }
    if ((parseInt(jsInfo.PurchaseItemCount, 10) || 0) > 0 || (parseInt(jsInfo.InventoryItemCount, 10) || 0) > 0) {
      return res.status(400).json({ success: false, message: "This job sheet has items. Cannot delete!" });
    }

    await transaction.begin();

    const request = new sql.Request(transaction)
      .input("JobSheetNo", sql.VarChar, jobSheetNo.trim());

    await request.query(`DELETE FROM tblJobSheetDetail WHERE JobSheetNo = @JobSheetNo`);
    if (jsInfo.Accessories === 'Y') {
      await request.query(`DELETE FROM tblAccessoriesDetail WHERE JobSheetNo = @JobSheetNo`);
    }
    const headerRes = await request.query(`DELETE FROM tblJobSheetHeader WHERE JobSheetNo = @JobSheetNo`);

    if (headerRes.rowsAffected[0] === 0) {
      await transaction.rollback();
      return res.status(404).json({ success: false, message: "JobSheet not found" });
    }

    await transaction.commit();
    return res.status(200).json({ success: true, message: "Jobsheet deleted successfully!" });
  } catch (err) {
    console.error("🔥 DELETE JOBSHEET ERROR:", err);
    try {
      await transaction.rollback();
    } catch (rbErr) { }
    return res.status(500).json({ success: false, message: "Database delete failed", error: err.message });
  }
});

// ================= DELETE SINGLE CATALOGUE FROM JOBSHEET =================
// VB Reference: btnCatDelete_Click -> objCatalogue.fncCheckCatalogueDelete1
router.delete("/:jobSheetNo/catalogue/:catalogueCode", async (req, res) => {
  const pool = req.pool;
  const transaction = new sql.Transaction(pool);

  try {
    const { jobSheetNo, catalogueCode } = req.params;
    const jsNo = jobSheetNo.trim();
    const catCode = catalogueCode.trim();

    // Check if items exist for this catalogue in this jobsheet
    const chk = await pool.request()
      .input("JobSheetNo", sql.VarChar, jsNo)
      .input("CatalogueCode", sql.VarChar, catCode)
      .query(`
        SELECT 
          (SELECT COUNT(1) FROM tblPurchase WHERE JobSheetNo = @JobSheetNo AND CatalogueCode = @CatalogueCode) as PurCount,
          (SELECT COUNT(1) FROM tblInventory WHERE JobSheetNo = @JobSheetNo AND CatalogueCode = @CatalogueCode) as InvCount
      `);

    const purCount = parseInt(chk.recordset[0]?.PurCount, 10) || 0;
    const invCount = parseInt(chk.recordset[0]?.InvCount, 10) || 0;

    if (purCount > 0 || invCount > 0) {
      return res.status(400).json({ success: false, message: "This catalogue has items. Cannot delete!" });
    }

    await transaction.begin();

    // Delete catalogue from tblJobSheetDetail
    const delReq = new sql.Request(transaction);
    delReq.input("JobSheetNo", sql.VarChar, jsNo);
    delReq.input("CatalogueCode", sql.VarChar, catCode);

    await delReq.query(`
      DELETE FROM tblJobSheetDetail 
      WHERE JobSheetNo = @JobSheetNo AND CatalogueCode = @CatalogueCode;
      DELETE FROM tblAccessoriesDetail 
      WHERE JobSheetNo = @JobSheetNo AND CatalogueCode = @CatalogueCode;
    `);

    // Recalculate remaining header totals
    const sumReq = new sql.Request(transaction);
    sumReq.input("JobSheetNo", sql.VarChar, jsNo);
    const sumRes = await sumReq.query(`
      SELECT 
        ISNULL(SUM(Weight), 0) as TotalWeight,
        ISNULL(SUM(MC), 0) as TotalMC,
        ISNULL(SUM(PCS), 0) as TotalPCS,
        ISNULL(SUM(GoldLoss), 0) as TotalGoldLoss,
        ISNULL(SUM(TotalCost), 0) as TotalCost
      FROM tblJobSheetDetail
      WHERE JobSheetNo = @JobSheetNo
    `);

    const tot = sumRes.recordset[0];
    const updReq = new sql.Request(transaction);
    updReq
      .input("JobSheetNo", sql.VarChar, jsNo)
      .input("TotalWeight24K", sql.Numeric(18, 3), tot.TotalWeight)
      .input("TotalMC", sql.Numeric(18, 2), tot.TotalMC)
      .input("TotalPCS", sql.Numeric(18, 0), tot.TotalPCS)
      .input("TotalGoldLoss", sql.Numeric(18, 3), tot.TotalGoldLoss)
      .input("TotalLocalCost", sql.Numeric(18, 2), tot.TotalCost)
      .input("TotalForeignCost", sql.Numeric(18, 2), tot.TotalCost)
      .input("TotalCost", sql.Numeric(18, 2), tot.TotalCost)
      .input("ManualWeight", sql.Numeric(18, 3), tot.TotalWeight)
      .input("ManualMC", sql.Numeric(18, 2), tot.TotalMC)
      .input("ManualGoldLoss", sql.Numeric(18, 3), tot.TotalGoldLoss)
      .input("ManualLocalCost", sql.Numeric(18, 2), tot.TotalCost);

    await updReq.query(`
      UPDATE tblJobSheetHeader SET
        TotalWeight24K = @TotalWeight24K,
        TotalMC = @TotalMC,
        TotalPCS = @TotalPCS,
        TotalGoldLoss = @TotalGoldLoss,
        TotalLocalCost = @TotalLocalCost,
        TotalForeignCost = @TotalForeignCost,
        TotalCost = @TotalCost,
        ManualWeight = @ManualWeight,
        ManualMC = @ManualMC,
        ManualGoldLoss = @ManualGoldLoss,
        ManualLocalCost = @ManualLocalCost,
        ModifyDate = GETDATE()
      WHERE JobSheetNo = @JobSheetNo
    `);

    await transaction.commit();
    return res.status(200).json({ success: true, message: "Catalogue deleted successfully!" });
  } catch (err) {
    if (transaction) {
      try { await transaction.rollback(); } catch (_) {}
    }
    console.error("🔥 DELETE CATALOGUE ERROR:", err);
    return res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// ================= CHECK DUPLICATE INVOICE NO =================
// VB Reference: subCheckDuplicateBill
router.get("/check/duplicate", async (req, res) => {
  try {
    const { supplierCode, invoiceNo, jobSheetNo } = req.query;
    if (!supplierCode || !invoiceNo) {
      return res.status(400).json({ success: false, message: "supplierCode and invoiceNo are required" });
    }

    const pool = req.pool;
    const result = await pool.request()
      .input("SupplierCode", sql.VarChar, supplierCode.trim())
      .input("PurchaseBillNo", sql.VarChar, invoiceNo.trim())
      .query(`
        SELECT JobSheetNo 
        FROM tblJobSheetHeader 
        WHERE SupplierCode = @SupplierCode AND PurchaseBillNo = @PurchaseBillNo
      `);

    if (result.recordset.length > 0) {
      const existingNo = result.recordset[0].JobSheetNo;
      if (!jobSheetNo || existingNo.trim() !== jobSheetNo.trim()) {
        return res.json({
          success: true,
          isDuplicate: true,
          existingJobSheetNo: existingNo,
          message: `Duplicate Invoice No. This invoice is already entered in JobsheetNo ${existingNo}.`
        });
      }
    }

    return res.json({ success: true, isDuplicate: false });
  } catch (err) {
    console.error("🔥 CHECK DUPLICATE INVOICE ERROR:", err);
    return res.status(500).json({ success: false, message: "Server Error", error: err.message });
  }
});

// ================= GET JOBSHEET BY SUPPLIER & INVOICE =================
// VB Reference: fncGetJobSheet(sSupplier, sInvoiceNo)
router.get("/by-invoice/:supplierCode/:invoiceNo", async (req, res) => {
  try {
    const { supplierCode, invoiceNo } = req.params;
    const pool = req.pool;
    const result = await pool.request()
      .input("SupplierCode", sql.VarChar, supplierCode.trim())
      .input("PurchaseBillNo", sql.VarChar, invoiceNo.trim())
      .query(`
        SELECT JobSheetNo 
        FROM tblJobSheetHeader 
        WHERE SupplierCode = @SupplierCode AND PurchaseBillNo = @PurchaseBillNo
      `);

    if (result.recordset.length > 0) {
      return res.json({ success: true, jobSheetNo: result.recordset[0].JobSheetNo });
    }
    return res.json({ success: false, message: "No jobsheet found for invoice" });
  } catch (err) {
    console.error("🔥 GET BY INVOICE ERROR:", err);
    return res.status(500).json({ success: false, message: "Server Error", error: err.message });
  }
});

// ================= GET CATALOGUE INFO (WITH PURCHASE TYPE) =================
// VB Reference: subGetCatalogue & subSetPurchaseType
router.get("/lookup/catalogue/:code", async (req, res) => {
  try {
    const { code } = req.params;
    const pool = req.pool;
    const result = await pool.request()
      .input("CatalogueCode", sql.VarChar, code.trim())
      .query(`
        SELECT 
          a.CatalogueCode,
          a.CatalogueName,
          a.CategoryCode,
          b.ClassCode,
          c.PurchaseType
        FROM tblCatalogue a
        INNER JOIN tblCategory b ON a.CategoryCode = b.CategoryCode
        LEFT JOIN tblClass c ON b.ClassCode = c.ClassCode
        WHERE a.CatalogueCode = @CatalogueCode
      `);

    if (result.recordset.length > 0) {
      return res.json({ success: true, data: result.recordset[0] });
    }
    return res.status(404).json({ success: false, message: "Invalid CatalogueCode" });
  } catch (err) {
    console.error("🔥 GET CATALOGUE LOOKUP ERROR:", err);
    return res.status(500).json({ success: false, message: "Server Error", error: err.message });
  }
});

// ================= GET CATEGORY INFO (WITH PURCHASE TYPE) =================
// VB Reference: subGetCategory & subSetPurchaseType
router.get("/lookup/category/:code", async (req, res) => {
  try {
    const { code } = req.params;
    const pool = req.pool;
    const result = await pool.request()
      .input("CategoryCode", sql.VarChar, code.trim())
      .query(`
        SELECT 
          b.CategoryCode,
          b.CategoryName,
          b.ClassCode,
          c.PurchaseType
        FROM tblCategory b
        LEFT JOIN tblClass c ON b.ClassCode = c.ClassCode
        WHERE b.CategoryCode = @CategoryCode
      `);

    if (result.recordset.length > 0) {
      return res.json({ success: true, data: result.recordset[0] });
    }
    return res.status(404).json({ success: false, message: "Invalid CategoryCode" });
  } catch (err) {
    console.error("🔥 GET CATEGORY LOOKUP ERROR:", err);
    return res.status(500).json({ success: false, message: "Server Error", error: err.message });
  }
});

module.exports = router;
