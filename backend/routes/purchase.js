const express = require("express");
const { sql } = require("../db");
const router = express.Router();
// ================= GET ALL PURCHASES (LOV) =================
router.get("/", async (req, res) => {
  try {
    const pool = req.pool;
    // We get distinct PurchaseBillNo from tblPurchase
    const result = await pool.request().query(`
      SELECT DISTINCT PurchaseBillNo, SupplierCode, JobSheetNo, CreateDate
      FROM tblPurchase
      ORDER BY CreateDate DESC
    `);
    return res.status(200).json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("🔥 GET ALL PURCHASES ERROR:", err);
    return res.status(500).json({ success: false, message: "Server Error", error: err.message });
  }
});

// ================= GET PURCHASE BY BILL NO =================
router.get("/:purchaseBillNo", async (req, res) => {
  try {
    const { purchaseBillNo } = req.params;
    const pool = req.pool;

    const detailsResult = await pool.request()
      .input("PurchaseBillNo", sql.VarChar, purchaseBillNo)
      .query(`
        SELECT p.*, s.SupplierName 
        FROM tblPurchase p
        LEFT JOIN tblSupplier s ON p.SupplierCode = s.SupplierCode
        WHERE p.PurchaseBillNo = @PurchaseBillNo
      `);

    if (detailsResult.recordset.length === 0) {
      return res.status(404).json({ success: false, message: "Purchase not found" });
    }

    // Extract header-like info from the first row
    const header = {
      PurchaseBillNo: detailsResult.recordset[0].PurchaseBillNo,
      SupplierCode: detailsResult.recordset[0].SupplierCode,
      SupplierName: detailsResult.recordset[0].SupplierName,
      JobSheetNo: detailsResult.recordset[0].JobSheetNo,
      Remarks: detailsResult.recordset[0].Remarks,
      LocationCode: detailsResult.recordset[0].LocationCode,
      CurrencyCode: detailsResult.recordset[0].CurrencyCode || 'SGD',
      CurrencyRate: 1.0,
      CreateDate: detailsResult.recordset[0].CreateDate
    };

    return res.status(200).json({
      success: true,
      header: header,
      details: detailsResult.recordset
    });
  } catch (err) {
    console.error("🔥 GET PURCHASE ERROR:", err);
    return res.status(500).json({ success: false, message: "Server Error", error: err.message });
  }
});

// ================= SAVE/UPDATE PURCHASE =================
router.post("/", async (req, res) => {
  const pool = req.pool;
  const transaction = new sql.Transaction(pool);

  try {
    const { header, details, userId } = req.body;

    if (!header || !header.PurchaseBillNo) {
      return res.status(400).json({ success: false, message: "PurchaseBillNo is required in header" });
    }

    const sysUser = userId || "SYSTEM";
    const curDate = new Date();

    await transaction.begin();

    const request = new sql.Request(transaction);

    // Delete existing lines in tblPurchase and tblInventory for this PurchaseBillNo to replace them
    await request
      .input("PurchaseBillNo", sql.VarChar, header.PurchaseBillNo)
      .query(`
        DELETE FROM tblPurchase WHERE PurchaseBillNo = @PurchaseBillNo;
        DELETE FROM tblInventory WHERE PurchaseBillNo = @PurchaseBillNo;
      `);

    // Insert into tblPurchase and tblInventory for each detail line
    for (let i = 0; i < details.length; i++) {
      const d = details[i];
      const insertReq = new sql.Request(transaction);

      // We assume InventoryCode is unique or generated. If empty, generate one? 
      // For now we assume frontend sends a generated or empty string which will be updated.
      let invCode = d.InventoryCode || `${header.PurchaseBillNo}-${i}`;

      await insertReq
        .input("LocationCode", sql.VarChar, header.LocationCode || "")
        .input("JobSheetNo", sql.VarChar, header.JobSheetNo || "")
        .input("DirectPurchaseNo", sql.VarChar, d.DirectPurchaseNo || "")
        .input("PurchaseBillNo", sql.VarChar, header.PurchaseBillNo)
        .input("SupplierCode", sql.VarChar, header.SupplierCode || "")
        .input("InventoryCode", sql.VarChar, invCode)
        .input("Description", sql.VarChar, d.Description || "")
        .input("DiamondDescription", sql.VarChar, d.DiamondDescription || "")
        .input("StoneWeight", sql.Numeric(18, 3), parseFloat(d.StoneWeight) || 0)
        .input("CostPerGram", sql.Numeric(18, 2), parseFloat(d.CostPerGram) || 0)
        .input("Size", sql.Numeric(18, 2), parseFloat(d.Size) || 0)
        .input("Weight", sql.Numeric(18, 3), parseFloat(d.Weight) || 0)
        .input("MC", sql.Numeric(18, 2), parseFloat(d.MC) || 0)
        .input("PCS", sql.Numeric(18, 0), parseFloat(d.PCS) || 1)
        .input("GoldLoss", sql.Numeric(18, 3), parseFloat(d.GoldLoss) || 0)
        .input("TotalCost", sql.Numeric(18, 2), parseFloat(d.TotalCost) || 0)
        .input("CatalogueCode", sql.VarChar, d.CatalogueCode || "")
        .input("CategoryCode", sql.VarChar, d.CategoryCode || "")
        .input("ClassCode", sql.VarChar, d.ClassCode || "")
        .input("TrayId", sql.VarChar, d.TrayId || "")
        .input("PurchaseType", sql.VarChar, d.PurchaseType || "")
        .input("Remarks", sql.VarChar, header.Remarks || "")
        .input("JournalNo", sql.VarChar, d.JournalNo || "")
        .input("CreateUser", sql.VarChar, sysUser)
        .input("CreateDate", sql.SmallDateTime, curDate)
        .input("ModifyUser", sql.VarChar, sysUser)
        .input("ModifyDate", sql.SmallDateTime, curDate)
        .query(`
          INSERT INTO tblPurchase (
            LocationCode, JobSheetNo, DirectPurchaseNo, PurchaseBillNo, SupplierCode,
            InventoryCode, Description, DiamondDescription, StoneWeight, CostPerGram,
            Size, Weight, MC, PCS, GoldLoss, TotalCost, CatalogueCode, CategoryCode,
            ClassCode, TrayId, PurchaseType, Remarks, JournalNo, CreateUser, CreateDate, ModifyUser, ModifyDate
          ) VALUES (
            @LocationCode, @JobSheetNo, @DirectPurchaseNo, @PurchaseBillNo, @SupplierCode,
            @InventoryCode, @Description, @DiamondDescription, @StoneWeight, @CostPerGram,
            @Size, @Weight, @MC, @PCS, @GoldLoss, @TotalCost, @CatalogueCode, @CategoryCode,
            @ClassCode, @TrayId, @PurchaseType, @Remarks, @JournalNo, @CreateUser, @CreateDate, @ModifyUser, @ModifyDate
          )
        `);

      // Also insert into tblInventory
      const invReq = new sql.Request(transaction);
      await invReq
        .input("InventoryCode", sql.VarChar, invCode)
        .input("Description", sql.VarChar, d.Description || "")
        .input("DiamondDescription", sql.VarChar, d.DiamondDescription || "")
        .input("Size", sql.Numeric(18, 2), parseFloat(d.Size) || 0)
        .input("Weight", sql.Numeric(18, 3), parseFloat(d.Weight) || 0)
        .input("StoneWeight", sql.Numeric(18, 3), parseFloat(d.StoneWeight) || 0)
        .input("CostPerGram", sql.Numeric(18, 2), parseFloat(d.CostPerGram) || 0)
        .input("PMC", sql.Numeric(18, 2), parseFloat(d.PMC) || 0)
        .input("SMC", sql.Numeric(18, 2), parseFloat(d.SMC) || 0)
        .input("GoldLoss", sql.Numeric(18, 3), parseFloat(d.GoldLoss) || 0)
        .input("TotalCost", sql.Numeric(18, 2), parseFloat(d.TotalCost) || 0)
        .input("JobSheetNo", sql.VarChar, header.JobSheetNo || "")
        .input("PurchaseBillNo", sql.VarChar, header.PurchaseBillNo)
        .input("PurchaseDate", sql.SmallDateTime, curDate)
        .input("SupplierCode", sql.VarChar, header.SupplierCode || "")
        .input("SalesBillNo", sql.VarChar, "")
        .input("SalesDate", sql.SmallDateTime, null)
        .input("SalesCostPerGram", sql.Numeric(18, 2), 0)
        .input("SalesMC", sql.Numeric(18, 2), 0)
        .input("SalesAmount", sql.Numeric(18, 2), 0)
        .input("CategoryCode", sql.VarChar, d.CategoryCode || "")
        .input("ClassCode", sql.VarChar, d.ClassCode || "")
        .input("CatalogueCode", sql.VarChar, d.CatalogueCode || "")
        .input("TrayId", sql.VarChar, d.TrayId || "")
        .input("PMCType", sql.VarChar, d.PMCType || "")
        .input("SMCType", sql.VarChar, d.SMCType || "")
        .input("OrderNo", sql.VarChar, d.OrderNo || "")
        .input("Remarks", sql.VarChar, header.Remarks || "")
        .input("CreateUser", sql.VarChar, sysUser)
        .input("CreateDate", sql.SmallDateTime, curDate)
        .input("ModifyUser", sql.VarChar, sysUser)
        .input("ModifyDate", sql.SmallDateTime, curDate)
        .input("LocationCode", sql.VarChar, header.LocationCode || "")
        .input("SizeCode", sql.Numeric(18, 2), 0)
        .input("SizeType", sql.VarChar, "")
        .input("SalesPCS", sql.Bit, 0)
        .query(`
          INSERT INTO tblInventory (
            InventoryCode, Description, DiamondDescription, Size, Weight, StoneWeight,
            CostPerGram, PMC, SMC, GoldLoss, TotalCost, JobSheetNo, PurchaseBillNo,
            PurchaseDate, SupplierCode, SalesBillNo, SalesDate, SalesCostPerGram, SalesMC,
            SalesAmount, CategoryCode, ClassCode, CatalogueCode, TrayId, PMCType, SMCType,
            OrderNo, Remarks, CreateUser, CreateDate, ModifyUser, ModifyDate, LocationCode,
            SizeCode, SizeType, SalesPCS
          ) VALUES (
            @InventoryCode, @Description, @DiamondDescription, @Size, @Weight, @StoneWeight,
            @CostPerGram, @PMC, @SMC, @GoldLoss, @TotalCost, @JobSheetNo, @PurchaseBillNo,
            @PurchaseDate, @SupplierCode, @SalesBillNo, @SalesDate, @SalesCostPerGram, @SalesMC,
            @SalesAmount, @CategoryCode, @ClassCode, @CatalogueCode, @TrayId, @PMCType, @SMCType,
            @OrderNo, @Remarks, @CreateUser, @CreateDate, @ModifyUser, @ModifyDate, @LocationCode,
            @SizeCode, @SizeType, @SalesPCS
          )
        `);
    }

    await transaction.commit();
    return res.status(200).json({ success: true, message: "Purchase saved successfully" });
  } catch (err) {
    await transaction.rollback();
    console.error("🔥 POST PURCHASE ERROR:", err);
    return res.status(500).json({ success: false, message: "Server Error", error: err.message });
  }
});

router.delete("/:purchaseBillNo", async (req, res) => {
  const pool = req.pool;
  const transaction = new sql.Transaction(pool);

  try {
    const { purchaseBillNo } = req.params;

    await transaction.begin();

    const request = new sql.Request(transaction);
    await request
      .input("PurchaseBillNo", sql.VarChar, purchaseBillNo)
      .query(`
        DELETE FROM tblPurchase WHERE PurchaseBillNo = @PurchaseBillNo;
        DELETE FROM tblInventory WHERE PurchaseBillNo = @PurchaseBillNo;
      `);

    await transaction.commit();
    return res.status(200).json({ success: true, message: "Purchase deleted successfully" });
  } catch (err) {
    await transaction.rollback();
    console.error("🔥 DELETE PURCHASE ERROR:", err);
    return res.status(500).json({ success: false, message: "Server Error", error: err.message });
  }
});

// ================= BILL CLOSE =================
router.post("/billclose", async (req, res) => {
  try {
    const { jobSheetNo } = req.body;
    if (!jobSheetNo) {
      return res.status(400).json({ success: false, message: "jobSheetNo is required" });
    }
    const pool = req.pool;
    const result = await pool.request()
      .input("JobSheetNo", sql.VarChar, jobSheetNo)
      .query(`
        UPDATE tblJobSheetHeader
        SET BillClose = 'Y', ModifyDate = GETDATE()
        WHERE JobSheetNo = @JobSheetNo
      `);

    return res.status(200).json({ success: true, message: "Job sheet bill closed successfully" });
  } catch (err) {
    console.error("🔥 BILL CLOSE ERROR:", err);
    return res.status(500).json({ success: false, message: "Server Error", error: err.message });
  }
});

module.exports = router;
