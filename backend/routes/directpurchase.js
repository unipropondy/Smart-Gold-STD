const express = require("express");
const { sql } = require("../db");
const router = express.Router();

// ================= GET ALL DIRECT PURCHASES (LOV / SEARCH) =================
router.get("/", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query(`
      SELECT DISTINCT 
        p.PurchaseBillNo, 
        p.SupplierCode, 
        s.SupplierName,
        p.JobSheetNo, 
        p.CreateDate
      FROM tblPurchase p
      LEFT JOIN tblSupplier s ON p.SupplierCode = s.SupplierCode
      ORDER BY p.CreateDate DESC
    `);
    return res.status(200).json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("🔥 GET ALL DIRECT PURCHASES ERROR:", err);
    return res.status(500).json({ success: false, message: "Server Error", error: err.message });
  }
});

// ================= GET DIRECT PURCHASE BY BILL NO =================
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
      return res.status(404).json({ success: false, message: "Direct purchase not found" });
    }

    const firstRow = detailsResult.recordset[0];
    const header = {
      PurchaseBillNo: firstRow.PurchaseBillNo,
      SupplierCode: firstRow.SupplierCode,
      SupplierName: firstRow.SupplierName,
      JobSheetNo: firstRow.JobSheetNo,
      Remarks: firstRow.Remarks,
      LocationCode: firstRow.LocationCode,
      CurrencyCode: firstRow.CurrencyCode || '',
      CurrencyRate: firstRow.CurrencyRate != null ? firstRow.CurrencyRate : '',
      CreateDate: firstRow.CreateDate
    };

    return res.status(200).json({
      success: true,
      header: header,
      details: detailsResult.recordset
    });
  } catch (err) {
    console.error("🔥 GET DIRECT PURCHASE ERROR:", err);
    return res.status(500).json({ success: false, message: "Server Error", error: err.message });
  }
});

// ================= SAVE / UPDATE DIRECT PURCHASE =================
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

    // Remove previous records for this bill or items in tblPurchase and tblInventory to allow replace/update
    const inventoryCodes = details
      .map(d => (d.InventoryCode || '').trim())
      .filter(code => code.length > 0);

    const deleteReq = new sql.Request(transaction);
    deleteReq.input("PurchaseBillNo", sql.VarChar, header.PurchaseBillNo.trim());

    let deletePurchaseSql = `DELETE FROM tblPurchase WHERE PurchaseBillNo = @PurchaseBillNo`;
    let deleteInventorySql = `DELETE FROM tblInventory WHERE PurchaseBillNo = @PurchaseBillNo`;

    if (inventoryCodes.length > 0) {
      inventoryCodes.forEach((code, idx) => {
        deleteReq.input(`InvCode_${idx}`, sql.VarChar, code);
      });
      const codeParams = inventoryCodes.map((_, idx) => `@InvCode_${idx}`).join(", ");
      deletePurchaseSql += ` OR InventoryCode IN (${codeParams})`;
      deleteInventorySql += ` OR InventoryCode IN (${codeParams})`;
    }

    await deleteReq.query(`
      ${deletePurchaseSql};
      ${deleteInventorySql};
    `);

    // Insert line items
    for (let i = 0; i < details.length; i++) {
      const d = details[i];
      const insertReq = new sql.Request(transaction);
      const invCode = d.InventoryCode || `${header.PurchaseBillNo}-${i + 1}`;

      await insertReq
        .input("LocationCode", sql.VarChar, header.LocationCode || "")
        .input("JobSheetNo", sql.VarChar, header.JobSheetNo || "")
        .input("DirectPurchaseNo", sql.VarChar, d.DirectPurchaseNo || "")
        .input("PurchaseBillNo", sql.VarChar, header.PurchaseBillNo)
        .input("SupplierCode", sql.VarChar, header.SupplierCode || "")
        .input("InventoryCode", sql.VarChar, invCode)
        .input("Description", sql.VarChar, d.Description || d.InventoryDescription || "")
        .input("DiamondDescription", sql.VarChar, d.DiamondDescription || "")
        .input("StoneWeight", sql.Numeric(18, 3), parseFloat(d.StoneWeight) || 0)
        .input("CostPerGram", sql.Numeric(18, 2), parseFloat(d.CostPerGram) || 0)
        .input("Size", sql.Numeric(18, 2), parseFloat(d.Size) || 0)
        .input("Weight", sql.Numeric(18, 3), parseFloat(d.Weight) || 0)
        .input("MC", sql.Numeric(18, 2), parseFloat(d.MC || d.PMCCost || d.PMC) || 0)
        .input("PCS", sql.Numeric(18, 0), parseFloat(d.PCS) || 1)
        .input("GoldLoss", sql.Numeric(18, 3), parseFloat(d.GoldLoss) || 0)
        .input("TotalCost", sql.Numeric(18, 2), parseFloat(d.TotalCost) || 0)
        .input("CatalogueCode", sql.VarChar, d.CatalogueCode || "")
        .input("CategoryCode", sql.VarChar, d.CategoryCode || "")
        .input("ClassCode", sql.VarChar, d.ClassCode || "")
        .input("TrayId", sql.VarChar, d.TrayId || "")
        .input("PurchaseType", sql.VarChar, d.PurchaseType || (d.PurchaseMCType === 'PerPCS' || d.PMCType === 'P' ? 'P' : 'W'))
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

      // Insert matching item into tblInventory
      const invReq = new sql.Request(transaction);
      await invReq
        .input("InventoryCode", sql.VarChar, invCode)
        .input("Description", sql.VarChar, d.InventoryDescription || d.Description || "")
        .input("DiamondDescription", sql.VarChar, d.DiamondDescription || "")
        .input("Size", sql.Numeric(18, 2), parseFloat(d.Size) || 0)
        .input("Weight", sql.Numeric(18, 3), parseFloat(d.Weight) || 0)
        .input("StoneWeight", sql.Numeric(18, 3), parseFloat(d.StoneWeight) || 0)
        .input("CostPerGram", sql.Numeric(18, 2), parseFloat(d.CostPerGram) || 0)
        .input("PMC", sql.Numeric(18, 2), parseFloat(d.PMC || d.PMCCost || d.MC) || 0)
        .input("SMC", sql.Numeric(18, 2), parseFloat(d.SMC || d.SMCCost) || 0)
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
        .input("PMCType", sql.VarChar, d.PMCType || (d.PurchaseMCType === 'PerPCS' ? 'P' : 'W'))
        .input("SMCType", sql.VarChar, d.SMCType || (d.SalesMCType === 'PerPCS' ? 'P' : 'W'))
        .input("OrderNo", sql.VarChar, header.OrderNo || d.OrderNo || "")
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

      // Insert/Update matching stock in tblInventoryStock
      const stockReq = new sql.Request(transaction);
      const pcsVal = parseInt(d.PCS, 10) || 1;
      await stockReq
        .input("LocationCode", sql.VarChar, header.LocationCode || "")
        .input("InventoryCode", sql.VarChar, invCode)
        .input("Description", sql.VarChar, d.InventoryDescription || d.Description || "")
        .input("DiamondDescription", sql.VarChar, d.DiamondDescription || "")
        .input("Weight", sql.Numeric(18, 3), parseFloat(d.Weight) || 0)
        .input("PCS", sql.Numeric(18, 0), pcsVal)
        .input("TotalCost", sql.Numeric(18, 2), parseFloat(d.TotalCost) || 0)
        .input("QtyOnHand", sql.Numeric(18, 0), pcsVal)
        .input("CreateUser", sql.VarChar, sysUser)
        .query(`
          INSERT INTO tblInventoryStock (
            LocationCode, InventoryCode, Description, DiamondDescription, Weight,
            PCS, TotalCost, QtyOnHand, QtySold, Status, CreateUser, CreateDate, ModifyUser, ModifyDate
          ) VALUES (
            @LocationCode, @InventoryCode, @Description, @DiamondDescription, @Weight,
            @PCS, @TotalCost, @QtyOnHand, 0, 'IN', @CreateUser, GETDATE(), @CreateUser, GETDATE()
          )
        `);

      // Insert into tblInventoryAddlDescription if AddlDesc is active
      if (d.AddlDesc && (d.Desc1 || d.Desc2 || d.Desc3 || d.Desc4 || d.Cost1 || d.Cost2 || d.Cost3 || d.Cost4)) {
        const addlReq = new sql.Request(transaction);
        await addlReq
          .input("InventoryCode", sql.VarChar, invCode)
          .input("JobSheetNo", sql.VarChar, header.JobSheetNo || "")
          .input("Description1", sql.VarChar, d.Desc1 || "")
          .input("Description2", sql.VarChar, d.Desc2 || "")
          .input("Description3", sql.VarChar, d.Desc3 || "")
          .input("Description4", sql.VarChar, d.Desc4 || "")
          .input("Cost1", sql.Numeric(18, 2), parseFloat(d.Cost1) || 0)
          .input("Cost2", sql.Numeric(18, 2), parseFloat(d.Cost2) || 0)
          .input("Cost3", sql.Numeric(18, 2), parseFloat(d.Cost3) || 0)
          .input("Cost4", sql.Numeric(18, 2), parseFloat(d.Cost4) || 0)
          .input("SellingPrice1", sql.Numeric(18, 2), parseFloat(d.SellingPrice1) || 0)
          .input("SellingPrice2", sql.Numeric(18, 2), parseFloat(d.SellingPrice2) || 0)
          .input("SellingPrice3", sql.Numeric(18, 2), parseFloat(d.SellingPrice3) || 0)
          .input("SellingPrice4", sql.Numeric(18, 2), parseFloat(d.SellingPrice4) || 0)
          .input("CreateUser", sql.VarChar, sysUser)
          .query(`
            INSERT INTO tblInventoryAddlDescription (
              InventoryCode, JobSheetNo, Description1, Description2, Description3, Description4,
              Cost1, Cost2, Cost3, Cost4, SellingPrice1, SellingPrice2, SellingPrice3, SellingPrice4,
              CreateUser, CreateDate, ModifyUser, ModifyDate
            ) VALUES (
              @InventoryCode, @JobSheetNo, @Description1, @Description2, @Description3, @Description4,
              @Cost1, @Cost2, @Cost3, @Cost4, @SellingPrice1, @SellingPrice2, @SellingPrice3, @SellingPrice4,
              @CreateUser, GETDATE(), @CreateUser, GETDATE()
            )
          `);
      }

      // Update tblInventoryOrderDetail if Ordered is selected
      if (header.Ordered && (header.OrderNo || d.OrderNo)) {
        const orderNoVal = (header.OrderNo || d.OrderNo || "").trim();
        const orderInvCodeVal = (d.OrderInventoryCode || "").trim();
        const updOrderReq = new sql.Request(transaction);
        updOrderReq
          .input("InventoryCode", sql.VarChar, invCode)
          .input("OrderNo", sql.VarChar, orderNoVal)
          .input("OrderInventoryCode", sql.VarChar, orderInvCodeVal);

        if (orderInvCodeVal) {
          await updOrderReq.query(`
            UPDATE tblInventoryOrderDetail
            SET InventoryCode = @InventoryCode, ModifyDate = GETDATE()
            WHERE OrderNo = @OrderNo AND OrderInventoryCode = @OrderInventoryCode;
            UPDATE tblInventoryOrderHeader
            SET Status = 'R', ModifyDate = GETDATE()
            WHERE OrderNo = @OrderNo;
          `);
        } else {
          await updOrderReq.query(`
            UPDATE tblInventoryOrderHeader
            SET Status = 'R', ModifyDate = GETDATE()
            WHERE OrderNo = @OrderNo;
          `);
        }
      }

      // Update Trade-In if TradeIn inventory details provided
      if (d.TradeInInventoryCode && d.TradeInWeight) {
        const tiReq = new sql.Request(transaction);
        tiReq
          .input("TradeInCode", sql.VarChar, d.TradeInInventoryCode.trim())
          .input("WeightUsed", sql.Numeric(18, 3), parseFloat(d.TradeInWeight) || 0);

        await tiReq.query(`
          UPDATE tblTradeInDetail
          SET BalanceWeight = CASE 
                WHEN BalanceWeight - @WeightUsed < 0 THEN 0 
                ELSE BalanceWeight - @WeightUsed 
              END,
              ModifyDate = GETDATE()
          WHERE InventoryCode = @TradeInCode
        `);
      }
    }

    // Upsert tblJobSheetHeader matching clsDirectPurchase.JobSheetSave
    if (header.JobSheetNo && header.JobSheetNo.trim() !== '') {
      const jsReq = new sql.Request(transaction);
      const totalWeight = details.reduce((sum, item) => sum + (parseFloat(item.Weight) || 0), 0);
      const totalCost = details.reduce((sum, item) => sum + (parseFloat(item.TotalCost) || 0), 0);
      const totalMC = details.reduce((sum, item) => sum + (parseFloat(item.MC || item.PMCCost || item.PMC) || 0), 0);
      const totalPCS = details.reduce((sum, item) => sum + (parseInt(item.PCS, 10) || 1), 0);
      const totalGoldLoss = details.reduce((sum, item) => sum + (parseFloat(item.GoldLoss) || 0), 0);
      const gstAmount = parseFloat(header.GSTAmount) || 0;
      const gstPercent = parseFloat(header.GSTPercent) || 0;
      const fixStatus = header.FixUnFix === 'Fix' ? 'Y' : 'N';
      const taelGST = header.GSTActive ? 'Y' : 'N';
      const paymentType = header.Payable === 'Gold' ? 'G' : header.Payable === 'Both' ? 'B' : 'C';

      jsReq
        .input("JobSheetNo", sql.VarChar, header.JobSheetNo.trim())
        .input("PurchaseBillNo", sql.VarChar, header.PurchaseBillNo.trim())
        .input("PurchaseBillDate", sql.SmallDateTime, curDate)
        .input("SupplierCode", sql.VarChar, header.SupplierCode || "")
        .input("CurrencyCode", sql.VarChar, header.CurrencyCode || "")
        .input("CurrencyRate", sql.Numeric(18, 6), parseFloat(header.CurrencyRate) || 1)
        .input("TotalWeight24K", sql.Numeric(18, 3), totalWeight)
        .input("TotalMC", sql.Numeric(18, 2), totalMC)
        .input("TotalPCS", sql.Numeric(18, 0), totalPCS)
        .input("TotalGoldLoss", sql.Numeric(18, 3), totalGoldLoss)
        .input("CostPerGram", sql.Numeric(18, 2), parseFloat(header.CostPerGram) || 0)
        .input("GST", sql.Numeric(18, 2), gstAmount)
        .input("TotalCost", sql.Numeric(18, 2), totalCost)
        .input("PaymentType", sql.VarChar, paymentType)
        .input("Remarks", sql.VarChar, header.Remarks || "")
        .input("FixStatus", sql.VarChar, fixStatus)
        .input("TaelRate", sql.Numeric(18, 2), parseFloat(header.TEAL) || 0)
        .input("TaelPercent", sql.Numeric(18, 4), parseFloat(header.TEALPercent) || 0)
        .input("TaelGST", sql.VarChar, taelGST)
        .input("LocationCode", sql.VarChar, header.LocationCode || "")
        .input("User", sql.VarChar, sysUser)
        .input("Date", sql.SmallDateTime, curDate);

      const chkJs = await jsReq.query(`SELECT 1 FROM tblJobSheetHeader WHERE JobSheetNo = @JobSheetNo`);
      if (chkJs.recordset.length > 0) {
        await jsReq.query(`
          UPDATE tblJobSheetHeader SET
            PurchaseBillNo = @PurchaseBillNo,
            PurchaseBillDate = @PurchaseBillDate,
            SupplierCode = @SupplierCode,
            CurrencyCode = @CurrencyCode,
            CurrencyRate = @CurrencyRate,
            TotalWeight24K = @TotalWeight24K,
            TotalMC = @TotalMC,
            TotalPCS = @TotalPCS,
            TotalGoldLoss = @TotalGoldLoss,
            CostPerGram = @CostPerGram,
            GST = @GST,
            TotalCost = @TotalCost,
            PaymentType = @PaymentType,
            Remarks = @Remarks,
            FixStatus = @FixStatus,
            TaelRate = @TaelRate,
            TaelPercent = @TaelPercent,
            TaelGST = @TaelGST,
            TranType = 'DP',
            LocationCode = @LocationCode,
            ModifyUser = @User,
            ModifyDate = @Date
          WHERE JobSheetNo = @JobSheetNo
        `);
      } else {
        await jsReq.query(`
          INSERT INTO tblJobSheetHeader (
            JobSheetNo, JobSheetDate, PurchaseBillNo, PurchaseBillDate, SupplierCode,
            CurrencyCode, CurrencyRate, TotalWeight24K, TotalMC, TotalPCS, TotalGoldLoss,
            CostPerGram, GST, TotalCost, PaymentType, Remarks, FixStatus, TaelRate,
            TaelPercent, TaelGST, TranType, BillClose, LocationCode, CreateUser, CreateDate, ModifyUser, ModifyDate
          ) VALUES (
            @JobSheetNo, @Date, @PurchaseBillNo, @PurchaseBillDate, @SupplierCode,
            @CurrencyCode, @CurrencyRate, @TotalWeight24K, @TotalMC, @TotalPCS, @TotalGoldLoss,
            @CostPerGram, @GST, @TotalCost, @PaymentType, @Remarks, @FixStatus, @TaelRate,
            @TaelPercent, @TaelGST, 'DP', 'N', @LocationCode, @User, @Date, @User, @Date
          );
          UPDATE tblCompany SET NextJobSheetNo = ISNULL(NextJobSheetNo, 0) + 1;
        `);
      }
    }

    await transaction.commit();
    return res.status(200).json({ success: true, message: "Direct purchase saved successfully" });
  } catch (err) {
    if (transaction) {
      try { await transaction.rollback(); } catch (_) {}
    }
    console.error("🔥 POST DIRECT PURCHASE ERROR:", err);
    return res.status(500).json({ success: false, message: err.message || "Server Error", error: err.message });
  }
});

// ================= DELETE DIRECT PURCHASE =================
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
    return res.status(200).json({ success: true, message: "Direct purchase deleted successfully" });
  } catch (err) {
    await transaction.rollback();
    console.error("🔥 DELETE DIRECT PURCHASE ERROR:", err);
    return res.status(500).json({ success: false, message: "Server Error", error: err.message });
  }
});

// ================= DELETE SINGLE INVENTORY ITEM FROM DIRECT PURCHASE =================
router.delete("/:purchaseBillNo/item/:inventoryCode", async (req, res) => {
  const pool = req.pool;
  const transaction = new sql.Transaction(pool);

  try {
    const { purchaseBillNo, inventoryCode } = req.params;
    const invCode = inventoryCode.trim();

    // 1. Check if bill is closed or item exists
    const chkResult = await pool.request()
      .input("InventoryCode", sql.VarChar, invCode)
      .query(`
        SELECT 
          i.InventoryCode, 
          i.SalesBillNo, 
          i.CreateDate, 
          i.PCS, 
          i.LocationCode,
          i.JobSheetNo,
          st.QtyOnHand,
          jh.BillClose,
          jh.TranType,
          loc.EODDate
        FROM tblInventory i
        LEFT JOIN tblInventoryStock st ON i.InventoryCode = st.InventoryCode AND i.LocationCode = st.LocationCode
        LEFT JOIN tblJobSheetHeader jh ON i.JobSheetNo = jh.JobSheetNo
        LEFT JOIN tblLocation loc ON i.LocationCode = loc.LocationCode
        WHERE i.InventoryCode = @InventoryCode
      `);

    if (chkResult.recordset.length > 0) {
      const item = chkResult.recordset[0];
      if (item.BillClose === 'Y') {
        return res.status(400).json({ success: false, message: "This bill is closed. Cannot delete!" });
      }
      if (item.TranType === 'JP') {
        return res.status(400).json({ success: false, message: "This is not a direct purchase bill. Cannot delete!" });
      }
      if (item.SalesBillNo && item.SalesBillNo.trim() !== '') {
        return res.status(400).json({ success: false, message: "Item sold. Cannot delete!" });
      }
      if (item.QtyOnHand != null && parseFloat(item.QtyOnHand) !== parseFloat(item.PCS)) {
        return res.status(400).json({ success: false, message: "Qty on hand is not the same as no. of pieces purchased. Cannot delete!" });
      }
      if (item.EODDate && item.CreateDate) {
        const eod = new Date(item.EODDate);
        const crDate = new Date(item.CreateDate);
        if (eod >= crDate) {
          return res.status(400).json({ success: false, message: "You cannot delete the Inventory after EOD. Please use purchase return module." });
        }
      }
    }

    await transaction.begin();

    const delReq = new sql.Request(transaction);
    delReq.input("PurchaseBillNo", sql.VarChar, purchaseBillNo.trim());
    delReq.input("InventoryCode", sql.VarChar, invCode);

    await delReq.query(`
      DELETE FROM tblPurchase WHERE PurchaseBillNo = @PurchaseBillNo AND InventoryCode = @InventoryCode;
      DELETE FROM tblInventory WHERE InventoryCode = @InventoryCode;
      DELETE FROM tblInventoryStock WHERE InventoryCode = @InventoryCode;
    `);

    await transaction.commit();
    return res.status(200).json({ success: true, message: `Item ${invCode} deleted successfully` });
  } catch (err) {
    if (transaction) {
      try { await transaction.rollback(); } catch (_) {}
    }
    console.error("🔥 DELETE INVENTORY ITEM ERROR:", err);
    return res.status(500).json({ success: false, message: "Server Error", error: err.message });
  }
});

// ================= BILL CLOSE FOR DIRECT PURCHASE WITH WEIGHT TOLERANCE =================
router.post("/billclose", async (req, res) => {
  try {
    const { jobSheetNo } = req.body;
    if (!jobSheetNo) {
      return res.status(400).json({ success: false, message: "jobSheetNo is required" });
    }
    const pool = req.pool;

    // 1. Fetch JobSheet details and PurWtVarPerc
    const jsResult = await pool.request()
      .input("JobSheetNo", sql.VarChar, jobSheetNo.trim())
      .query(`
        SELECT 
          jh.JobSheetNo, 
          jh.TotalWeight24K, 
          jh.BillClose, 
          jh.TranType,
          c.PurWtVarPerc
        FROM tblJobSheetHeader jh
        CROSS JOIN tblCompany c
        WHERE jh.JobSheetNo = @JobSheetNo
      `);

    if (jsResult.recordset.length === 0) {
      return res.status(404).json({ success: false, message: "Job sheet not found!" });
    }

    const js = jsResult.recordset[0];
    if (js.BillClose === 'Y') {
      return res.status(400).json({ success: false, message: "This Job sheet bill is already closed!" });
    }
    if (js.TranType === 'JP') {
      return res.status(400).json({ success: false, message: "This is not a direct purchase bill. Use job sheet purchase screen to close this bill!" });
    }

    const dTotJobSheetWt = parseFloat(js.TotalWeight24K) || 0;
    const dPurWtVarPerc = parseFloat(js.PurWtVarPerc) || 5.0; // Default 5% if not set

    // 2. Fetch total purchase weight
    const purResult = await pool.request()
      .input("JobSheetNo", sql.VarChar, jobSheetNo.trim())
      .query(`SELECT SUM(Weight) as TotPurchaseWt FROM tblPurchase WHERE JobSheetNo = @JobSheetNo`);

    const dTotPurchaseWt = parseFloat(purResult.recordset[0]?.TotPurchaseWt) || 0;
    const dWtDiff = Math.abs(dTotJobSheetWt - dTotPurchaseWt);
    const maxAllowedDiff = (dPurWtVarPerc * dTotJobSheetWt) / 100;

    if (dTotJobSheetWt > 0 && dWtDiff > maxAllowedDiff) {
      return res.status(400).json({
        success: false,
        message: `Cannot close bill. The Inventory weight differs by more than ${dPurWtVarPerc.toFixed(2)}% of the job sheet weight!`
      });
    }

    // 3. Close the bill
    await pool.request()
      .input("JobSheetNo", sql.VarChar, jobSheetNo.trim())
      .query(`
        UPDATE tblJobSheetHeader
        SET BillClose = 'Y', ModifyDate = GETDATE()
        WHERE JobSheetNo = @JobSheetNo
      `);

    return res.status(200).json({ success: true, message: "Purchase bill closed!" });
  } catch (err) {
    console.error("🔥 DIRECT PURCHASE BILL CLOSE ERROR:", err);
    return res.status(500).json({ success: false, message: "Server Error", error: err.message });
  }
});

module.exports = router;
