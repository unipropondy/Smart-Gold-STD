const express = require("express");
const router = express.Router();
const { sql } = require("../db");

// GET /api/splitinventory/detail
router.get("/detail", async (req, res) => {
  try {
    const pool = req.pool;
    const { code } = req.query;

    if (!code) {
      return res.status(400).json({ success: false, message: "InventoryCode (code) is required." });
    }

    // Query main inventory item with stock check
    const query = `
      SELECT 
        a.InventoryCode, 
        a.Description, 
        a.Weight, 
        a.StoneWeight, 
        a.PMC, 
        a.SMC as sMc, 
        a.TotalCost, 
        a.CostperGram, 
        a.GoldLoss, 
        a.JobSheetNo, 
        a.PurchaseBillNo, 
        a.PurchaseDate, 
        a.CreateDate, 
        a.SupplierCode, 
        a.SalesBillNo, 
        a.DiamondDescription, 
        a.CategoryCode, 
        a.ClassCode, 
        a.CatalogueCode, 
        a.TrayId, 
        a.PMCType, 
        a.SMCType, 
        a.Remarks,
        a.SizeCode,
        a.SizeType,
        b.QtyOnHand,
        b.LocationCode
      FROM tblInventory a
      LEFT JOIN tblInventoryStock b ON a.InventoryCode = b.InventoryCode AND a.LocationCode = b.LocationCode
      WHERE a.InventoryCode = @code
    `;

    const mainResult = await pool.request()
      .input("code", sql.VarChar, code.trim())
      .query(query);

    if (mainResult.recordset.length === 0) {
      return res.status(404).json({ success: false, message: "Inventory item not found." });
    }

    const item = mainResult.recordset[0];

    const salesBillNoTrim = (item.SalesBillNo || "").trim();
    if ((item.QtyOnHand !== null && item.QtyOnHand !== undefined && item.QtyOnHand <= 0) || salesBillNoTrim !== "") {
      return res.status(400).json({ success: false, message: "This item has already been sold or is out of stock. Cannot split." });
    }

    // Fetch additional inventory details if they exist
    const addlResult = await pool.request()
      .input("code", sql.VarChar, code.trim())
      .query("SELECT * FROM tblInventoryAddlDescription WHERE InventoryCode = @code");

    res.json({
      success: true,
      data: item,
      additionalDescription: addlResult.recordset.length > 0 ? addlResult.recordset[0] : null
    });

  } catch (err) {
    console.error("Get Inventory Detail Error:", err);
    res.status(500).json({ success: false, message: "Database error", error: err.message });
  }
});

// GET /api/splitinventory/categories
router.get("/categories", async (req, res) => {
  try {
    const pool = req.pool;
    const { classCode } = req.query;

    let query = "SELECT CategoryCode, CategoryName, ClassCode, NextInventoryCode FROM tblCategory";
    const request = pool.request();

    if (classCode) {
      query += " WHERE ClassCode = @classCode";
      request.input("classCode", sql.VarChar, classCode.trim());
    }
    query += " ORDER BY CategoryCode";

    const result = await request.query(query);
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Categories Error:", err);
    res.status(500).json({ success: false, message: "Database error", error: err.message });
  }
});

// GET /api/splitinventory/catalogues
router.get("/catalogues", async (req, res) => {
  try {
    const pool = req.pool;
    const { categoryCode } = req.query;

    let query = "SELECT CatalogueCode, CatalogueName, CategoryCode, GoldLoss FROM tblCatalogue";
    const request = pool.request();

    if (categoryCode) {
      query += " WHERE CategoryCode = @categoryCode";
      request.input("categoryCode", sql.VarChar, categoryCode.trim());
    }
    query += " ORDER BY CatalogueCode";

    const result = await request.query(query);
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Catalogues Error:", err);
    res.status(500).json({ success: false, message: "Database error", error: err.message });
  }
});

// GET /api/splitinventory/sizes
router.get("/sizes", async (req, res) => {
  try {
    const pool = req.pool;
    const { categoryCode } = req.query;

    let query = "SELECT SizeCode, SizeType, CategoryCode, Remarks FROM tblSize";
    const request = pool.request();

    if (categoryCode) {
      query += " WHERE CategoryCode = @categoryCode";
      request.input("categoryCode", sql.VarChar, categoryCode.trim());
    }
    query += " ORDER BY SizeCode";

    const result = await request.query(query);
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Sizes Error:", err);
    res.status(500).json({ success: false, message: "Database error", error: err.message });
  }
});

// GET /api/splitinventory/nextCode
router.get("/nextCode", async (req, res) => {
  try {
    const pool = req.pool;
    const { categoryCode } = req.query;

    if (!categoryCode) {
      return res.status(400).json({ success: false, message: "categoryCode is required." });
    }

    const result = await pool.request()
      .input("categoryCode", sql.VarChar, categoryCode.trim())
      .query("SELECT NextInventoryCode FROM tblCategory WHERE CategoryCode = @categoryCode");

    if (result.recordset.length === 0) {
      return res.status(404).json({ success: false, message: "Category not found." });
    }

    res.json({ success: true, data: result.recordset[0].NextInventoryCode });
  } catch (err) {
    console.error("Get Next Code Error:", err);
    res.status(500).json({ success: false, message: "Database error", error: err.message });
  }
});

// GET /api/splitinventory/nextScrapCode
router.get("/nextScrapCode", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query("SELECT LocationCode, NextScrapInventoryNo FROM tblCompany");
    
    let scrapNo = 1;
    let loc = "HQ";
    if (result.recordset.length > 0) {
      scrapNo = parseInt(result.recordset[0].NextScrapInventoryNo) || 1;
      loc = (result.recordset[0].LocationCode || "HQ").trim();
    }
    const nextScrapCode = `SC-${loc}${String(scrapNo).padStart(8, "0")}`;
    res.json({ success: true, data: nextScrapCode });
  } catch (err) {
    console.error("Get Next Scrap Code Error:", err);
    res.status(500).json({ success: false, message: "Database error", error: err.message });
  }
});

// POST /api/splitinventory/split
router.post("/split", async (req, res) => {
  const pool = req.pool;
  const transaction = new sql.Transaction(pool);

  try {
    const {
      selectedInventoryCode,
      originalInvCode,
      splitInvCode,
      originalItem,
      splitItem,
      originalDetails,
      additionalDescription,
      userId
    } = req.body;

    if (!selectedInventoryCode || !originalInvCode || !splitInvCode || !originalItem || !splitItem) {
      return res.status(400).json({ success: false, message: "All item details and codes are required for splitting." });
    }

    const sysUser = userId || "sys";
    await transaction.begin();

    // 1. Fetch Company Config
    const compResult = await new sql.Request(transaction).query(`
      SELECT TOP 1 LocationCode, CompanyShortCode, PurchaseCostingGL, RFIDModule, NextScrapInventoryNo FROM tblCompany
    `);
    const comp = compResult.recordset[0] || {};
    const loc = (comp.LocationCode || "HQ").trim();
    const purchaseCostingGL = comp.PurchaseCostingGL || "N";
    const rfidModule = comp.RFIDModule || "0";

    const costPerGram = parseFloat(originalDetails.CostperGram) || 0;
    const originalPMC = parseFloat(originalDetails.PMC) || 0;
    const splitPMC = parseFloat(splitItem.pmc) || 0;
    const goldLoss = parseFloat(originalDetails.GoldLoss) || 0;

    // Calculate Costs
    let originalTotalCost = 0;
    if (purchaseCostingGL === "Y") {
      originalTotalCost = (parseFloat(originalItem.weight) + goldLoss) * costPerGram + originalPMC;
    } else {
      originalTotalCost = (parseFloat(originalItem.weight) * costPerGram) + originalPMC;
    }

    let splitTotalCost = 0;
    if (purchaseCostingGL === "Y") {
      splitTotalCost = (parseFloat(splitItem.weight) + goldLoss) * costPerGram + splitPMC;
    } else {
      splitTotalCost = (parseFloat(splitItem.weight) * costPerGram) + splitPMC;
    }

    // Reference number for movement
    const refNo = "SP" + String(Date.now()).substring(4);

    // 2. Add Split movement log for old selected item (decrements weight/PCS)
    const reqMvOld = new sql.Request(transaction);
    await reqMvOld
      .input("LocationCode", sql.VarChar, loc)
      .input("InventoryCode", sql.VarChar, selectedInventoryCode)
      .input("Description", sql.VarChar, originalDetails.Description || "")
      .input("RefNumber", sql.VarChar, refNo)
      .input("Qty", sql.Numeric(18, 0), -1)
      .input("Weight", sql.Numeric(18, 3), -parseFloat(originalDetails.Weight))
      .input("PMC", sql.Numeric(18, 2), -originalPMC)
      .input("CostPerGram", sql.Numeric(18, 2), costPerGram)
      .input("TotalCost", sql.Numeric(18, 2), -parseFloat(originalDetails.TotalCost))
      .input("SupplierCode", sql.VarChar, originalDetails.SupplierCode)
      .input("CreateUser", sql.VarChar, sysUser)
      .query(`
        INSERT INTO tblInventoryMovement (
          LocationCode, InventoryCode, Description, Status, RefNumber, RefDate,
          Qty, Weight, PMC, CostPerGram, TotalCost, SupplierCode, CreateUser, CreateDate
        ) VALUES (
          @LocationCode, @InventoryCode, @Description, 'SP', @RefNumber, GETDATE(),
          @Qty, @Weight, @PMC, @CostPerGram, @TotalCost, @SupplierCode, @CreateUser, GETDATE()
        )
      `);

    // 3. Handle Original Item
    if (originalInvCode.trim() !== selectedInventoryCode.trim()) {
      // Different category/code, so delete the old code
      await new sql.Request(transaction)
        .input("code", sql.VarChar, selectedInventoryCode)
        .query(`
          DELETE FROM tblInventory WHERE InventoryCode = @code;
          DELETE FROM tblInventoryStock WHERE InventoryCode = @code;
        `);

      // Insert new original item under originalInvCode
      const reqInsertOrg = new sql.Request(transaction);
      await reqInsertOrg
        .input("InventoryCode", sql.VarChar, originalInvCode)
        .input("Description", sql.VarChar, originalItem.description || "")
        .input("DiamondDescription", sql.VarChar, originalDetails.DiamondDescription || "")
        .input("Weight", sql.Numeric(18, 3), parseFloat(originalItem.weight) || 0)
        .input("StoneWeight", sql.Numeric(18, 3), parseFloat(originalItem.stoneWeight) || 0)
        .input("CostPerGram", sql.Numeric(18, 2), costPerGram)
        .input("PMC", sql.Numeric(18, 2), originalPMC)
        .input("SMC", sql.Numeric(18, 2), parseFloat(originalItem.smc) || 0)
        .input("GoldLoss", sql.Numeric(18, 3), goldLoss)
        .input("TotalCost", sql.Numeric(18, 2), originalTotalCost)
        .input("JobSheetNo", sql.VarChar, originalDetails.JobSheetNo || "")
        .input("PurchaseBillNo", sql.VarChar, originalDetails.PurchaseBillNo || "")
        .input("PurchaseDate", sql.SmallDateTime, originalDetails.PurchaseDate ? new Date(originalDetails.PurchaseDate) : new Date())
        .input("SupplierCode", sql.VarChar, originalDetails.SupplierCode || "")
        .input("CategoryCode", sql.VarChar, originalItem.categoryCode)
        .input("ClassCode", sql.VarChar, originalDetails.ClassCode)
        .input("CatalogueCode", sql.VarChar, originalItem.catalogueCode)
        .input("TrayId", sql.VarChar, originalDetails.TrayId || "")
        .input("PMCType", sql.VarChar, originalDetails.PMCType || "")
        .input("SMCType", sql.VarChar, originalDetails.SMCType || "")
        .input("Remarks", sql.VarChar, originalDetails.Remarks || "")
        .input("CreateUser", sql.VarChar, sysUser)
        .input("LocationCode", sql.VarChar, loc)
        .input("SizeCode", sql.Numeric(18, 2), parseFloat(originalItem.sizeCode) || 0)
        .input("SizeType", sql.VarChar, originalItem.sizeType || "")
        .query(`
          INSERT INTO tblInventory (
            InventoryCode, Description, DiamondDescription, Weight, StoneWeight,
            CostPerGram, PMC, SMC, GoldLoss, TotalCost, JobSheetNo, PurchaseBillNo,
            PurchaseDate, SupplierCode, CategoryCode, ClassCode, CatalogueCode, TrayId,
            PMCType, SMCType, Remarks, CreateUser, CreateDate, ModifyUser, ModifyDate,
            LocationCode, SizeCode, SizeType, SalesPCS
          ) VALUES (
            @InventoryCode, @Description, @DiamondDescription, @Weight, @StoneWeight,
            @CostPerGram, @PMC, @SMC, @GoldLoss, @TotalCost, @JobSheetNo, @PurchaseBillNo,
            @PurchaseDate, @SupplierCode, @CategoryCode, @ClassCode, @CatalogueCode, @TrayId,
            @PMCType, @SMCType, @Remarks, @CreateUser, GETDATE(), @CreateUser, GETDATE(),
            @LocationCode, @SizeCode, @SizeType, 0
          )
        `);

      // Insert stock for new original item
      const reqStockOrg = new sql.Request(transaction);
      await reqStockOrg
        .input("InventoryCode", sql.VarChar, originalInvCode)
        .input("LocationCode", sql.VarChar, loc)
        .input("Description", sql.VarChar, originalItem.description || "")
        .input("DiamondDescription", sql.VarChar, originalDetails.DiamondDescription || "")
        .input("Weight", sql.Numeric(18, 3), parseFloat(originalItem.weight) || 0)
        .input("TotalCost", sql.Numeric(18, 2), originalTotalCost)
        .input("CreateUser", sql.VarChar, sysUser)
        .query(`
          INSERT INTO tblInventoryStock (
            LocationCode, InventoryCode, Description, DiamondDescription, Weight,
            PCS, TotalCost, QtyOnHand, QtySold, Status, CreateUser, CreateDate, ModifyUser, ModifyDate
          ) VALUES (
            @LocationCode, @InventoryCode, @Description, @DiamondDescription, @Weight,
            1, @TotalCost, 1, 0, 'IN', @CreateUser, GETDATE(), @CreateUser, GETDATE()
          )
        `);
    } else {
      // Same category, just update the weight and details
      const reqUpdateOrg = new sql.Request(transaction);
      await reqUpdateOrg
        .input("InventoryCode", sql.VarChar, selectedInventoryCode)
        .input("Description", sql.VarChar, originalItem.description || "")
        .input("Weight", sql.Numeric(18, 3), parseFloat(originalItem.weight) || 0)
        .input("StoneWeight", sql.Numeric(18, 3), parseFloat(originalItem.stoneWeight) || 0)
        .input("SMC", sql.Numeric(18, 2), parseFloat(originalItem.smc) || 0)
        .input("TotalCost", sql.Numeric(18, 2), originalTotalCost)
        .input("CategoryCode", sql.VarChar, originalItem.categoryCode)
        .input("CatalogueCode", sql.VarChar, originalItem.catalogueCode)
        .input("SizeCode", sql.Numeric(18, 2), parseFloat(originalItem.sizeCode) || 0)
        .input("SizeType", sql.VarChar, originalItem.sizeType || "")
        .input("ModifyUser", sql.VarChar, sysUser)
        .query(`
          UPDATE tblInventory SET
            Description = @Description,
            Weight = @Weight,
            StoneWeight = @StoneWeight,
            SMC = @SMC,
            TotalCost = @TotalCost,
            CategoryCode = @CategoryCode,
            CatalogueCode = @CatalogueCode,
            SizeCode = @SizeCode,
            SizeType = @SizeType,
            ModifyUser = @ModifyUser,
            ModifyDate = GETDATE()
          WHERE InventoryCode = @InventoryCode
        `);

      const reqStockUpdateOrg = new sql.Request(transaction);
      await reqStockUpdateOrg
        .input("InventoryCode", sql.VarChar, selectedInventoryCode)
        .input("LocationCode", sql.VarChar, loc)
        .input("Description", sql.VarChar, originalItem.description || "")
        .input("Weight", sql.Numeric(18, 3), parseFloat(originalItem.weight) || 0)
        .input("TotalCost", sql.Numeric(18, 2), originalTotalCost)
        .input("ModifyUser", sql.VarChar, sysUser)
        .query(`
          UPDATE tblInventoryStock SET
            Description = @Description,
            Weight = @Weight,
            TotalCost = @TotalCost,
            QtyOnHand = 1,
            ModifyUser = @ModifyUser,
            ModifyDate = GETDATE()
          WHERE InventoryCode = @InventoryCode AND LocationCode = @LocationCode
        `);
    }

    // Add Purchase movement log for original item
    const reqMvNewOrg = new sql.Request(transaction);
    await reqMvNewOrg
      .input("LocationCode", sql.VarChar, loc)
      .input("InventoryCode", sql.VarChar, originalInvCode)
      .input("Description", sql.VarChar, originalItem.description || "")
      .input("RefNumber", sql.VarChar, refNo)
      .input("Qty", sql.Numeric(18, 0), 1)
      .input("Weight", sql.Numeric(18, 3), parseFloat(originalItem.weight))
      .input("PMC", sql.Numeric(18, 2), originalPMC)
      .input("CostPerGram", sql.Numeric(18, 2), costPerGram)
      .input("TotalCost", sql.Numeric(18, 2), originalTotalCost)
      .input("SupplierCode", sql.VarChar, originalDetails.SupplierCode)
      .input("CreateUser", sql.VarChar, sysUser)
      .query(`
        INSERT INTO tblInventoryMovement (
          LocationCode, InventoryCode, Description, Status, RefNumber, RefDate,
          Qty, Weight, PMC, CostPerGram, TotalCost, SupplierCode, CreateUser, CreateDate
        ) VALUES (
          @LocationCode, @InventoryCode, @Description, 'PU', @RefNumber, GETDATE(),
          @Qty, @Weight, @PMC, @CostPerGram, @TotalCost, @SupplierCode, @CreateUser, GETDATE()
        )
      `);

    // 4. Handle Split Item
    const reqInsertSplit = new sql.Request(transaction);
    await reqInsertSplit
      .input("InventoryCode", sql.VarChar, splitInvCode)
      .input("Description", sql.VarChar, splitItem.description || "")
      .input("DiamondDescription", sql.VarChar, originalDetails.DiamondDescription || "")
      .input("Weight", sql.Numeric(18, 3), parseFloat(splitItem.weight) || 0)
      .input("StoneWeight", sql.Numeric(18, 3), parseFloat(splitItem.stoneWeight) || 0)
      .input("CostPerGram", sql.Numeric(18, 2), costPerGram)
      .input("PMC", sql.Numeric(18, 2), splitPMC)
      .input("SMC", sql.Numeric(18, 2), parseFloat(splitItem.smc) || 0)
      .input("GoldLoss", sql.Numeric(18, 3), goldLoss)
      .input("TotalCost", sql.Numeric(18, 2), splitTotalCost)
      .input("JobSheetNo", sql.VarChar, originalDetails.JobSheetNo || "")
      .input("PurchaseBillNo", sql.VarChar, originalDetails.PurchaseBillNo || "")
      .input("PurchaseDate", sql.SmallDateTime, originalDetails.PurchaseDate ? new Date(originalDetails.PurchaseDate) : new Date())
      .input("SupplierCode", sql.VarChar, originalDetails.SupplierCode || "")
      .input("CategoryCode", sql.VarChar, splitItem.categoryCode || "SCRAP")
      .input("ClassCode", sql.VarChar, splitItem.isScrap ? "SCRAP" : originalDetails.ClassCode)
      .input("CatalogueCode", sql.VarChar, splitItem.catalogueCode || "SCRAP")
      .input("TrayId", sql.VarChar, originalDetails.TrayId || "")
      .input("PMCType", sql.VarChar, originalDetails.PMCType || "")
      .input("SMCType", sql.VarChar, originalDetails.SMCType || "")
      .input("Remarks", sql.VarChar, originalDetails.Remarks || "")
      .input("CreateUser", sql.VarChar, sysUser)
      .input("LocationCode", sql.VarChar, loc)
      .input("SizeCode", sql.Numeric(18, 2), parseFloat(splitItem.sizeCode) || 0)
      .input("SizeType", sql.VarChar, splitItem.sizeType || "")
      .query(`
        INSERT INTO tblInventory (
          InventoryCode, Description, DiamondDescription, Weight, StoneWeight,
          CostPerGram, PMC, SMC, GoldLoss, TotalCost, JobSheetNo, PurchaseBillNo,
          PurchaseDate, SupplierCode, CategoryCode, ClassCode, CatalogueCode, TrayId,
          PMCType, SMCType, Remarks, CreateUser, CreateDate, ModifyUser, ModifyDate,
          LocationCode, SizeCode, SizeType, SalesPCS
        ) VALUES (
          @InventoryCode, @Description, @DiamondDescription, @Weight, @StoneWeight,
          @CostPerGram, @PMC, @SMC, @GoldLoss, @TotalCost, @JobSheetNo, @PurchaseBillNo,
          @PurchaseDate, @SupplierCode, @CategoryCode, @ClassCode, @CatalogueCode, @TrayId,
          @PMCType, @SMCType, @Remarks, @CreateUser, GETDATE(), @CreateUser, GETDATE(),
          @LocationCode, @SizeCode, @SizeType, 0
        )
      `);

    // Insert stock for split item
    const reqStockSplit = new sql.Request(transaction);
    await reqStockSplit
      .input("InventoryCode", sql.VarChar, splitInvCode)
      .input("LocationCode", sql.VarChar, loc)
      .input("Description", sql.VarChar, splitItem.description || "")
      .input("DiamondDescription", sql.VarChar, originalDetails.DiamondDescription || "")
      .input("Weight", sql.Numeric(18, 3), parseFloat(splitItem.weight) || 0)
      .input("TotalCost", sql.Numeric(18, 2), splitTotalCost)
      .input("CreateUser", sql.VarChar, sysUser)
      .query(`
        INSERT INTO tblInventoryStock (
          LocationCode, InventoryCode, Description, DiamondDescription, Weight,
          PCS, TotalCost, QtyOnHand, QtySold, Status, CreateUser, CreateDate, ModifyUser, ModifyDate
        ) VALUES (
          @LocationCode, @InventoryCode, @Description, @DiamondDescription, @Weight,
          1, @TotalCost, 1, 0, 'IN', @CreateUser, GETDATE(), @CreateUser, GETDATE()
        )
      `);

    // Add Purchase movement log for split item
    const reqMvNewSplit = new sql.Request(transaction);
    await reqMvNewSplit
      .input("LocationCode", sql.VarChar, loc)
      .input("InventoryCode", sql.VarChar, splitInvCode)
      .input("Description", sql.VarChar, splitItem.description || "")
      .input("RefNumber", sql.VarChar, refNo)
      .input("Qty", sql.Numeric(18, 0), 1)
      .input("Weight", sql.Numeric(18, 3), parseFloat(splitItem.weight))
      .input("PMC", sql.Numeric(18, 2), splitPMC)
      .input("CostPerGram", sql.Numeric(18, 2), costPerGram)
      .input("TotalCost", sql.Numeric(18, 2), splitTotalCost)
      .input("SupplierCode", sql.VarChar, originalDetails.SupplierCode)
      .input("CreateUser", sql.VarChar, sysUser)
      .query(`
        INSERT INTO tblInventoryMovement (
          LocationCode, InventoryCode, Description, Status, RefNumber, RefDate,
          Qty, Weight, PMC, CostPerGram, TotalCost, SupplierCode, CreateUser, CreateDate
        ) VALUES (
          @LocationCode, @InventoryCode, @Description, 'PU', @RefNumber, GETDATE(),
          @Qty, @Weight, @PMC, @CostPerGram, @TotalCost, @SupplierCode, @CreateUser, GETDATE()
        )
      `);

    // 5. Save Split History
    const reqHist = new sql.Request(transaction);
    await reqHist
      .input("InventoryCode", sql.VarChar, selectedInventoryCode)
      .input("Description", sql.VarChar, originalDetails.Description || "")
      .input("OriginalWeight", sql.Numeric(18, 3), parseFloat(originalDetails.Weight) || 0)
      .input("OriginalPMC", sql.Numeric(18, 2), originalPMC)
      .input("OriginalSMC", sql.Numeric(18, 2), parseFloat(originalDetails.SMC) || 0)
      .input("OriginalGoldLoss", sql.Numeric(18, 3), goldLoss)
      .input("OriginalTotalCost", sql.Numeric(18, 2), parseFloat(originalDetails.TotalCost) || 0)
      .input("Weight1", sql.Numeric(18, 3), parseFloat(originalItem.weight) || 0)
      .input("PMC1", sql.Numeric(18, 2), originalPMC)
      .input("SMC1", sql.Numeric(18, 2), parseFloat(originalItem.smc) || 0)
      .input("GoldLoss1", sql.Numeric(18, 3), goldLoss)
      .input("TotalCost1", sql.Numeric(18, 2), originalTotalCost)
      .input("DestInventoryCode", sql.VarChar, splitInvCode)
      .input("DestDescription", sql.VarChar, splitItem.description || "")
      .input("Weight2", sql.Numeric(18, 3), parseFloat(splitItem.weight) || 0)
      .input("PMC2", sql.Numeric(18, 2), splitPMC)
      .input("SMC2", sql.Numeric(18, 2), parseFloat(splitItem.smc) || 0)
      .input("GoldLoss2", sql.Numeric(18, 3), goldLoss)
      .input("TotalCost2", sql.Numeric(18, 2), splitTotalCost)
      .input("UserID", sql.VarChar, sysUser)
      .input("Locationcode", sql.VarChar, loc)
      .input("InventoryCode1", sql.VarChar, originalInvCode)
      .input("OriginalStoneWeight", sql.Numeric(18, 3), parseFloat(originalDetails.StoneWeight) || 0)
      .input("StoneWeight1", sql.Numeric(18, 3), parseFloat(originalItem.stoneWeight) || 0)
      .input("StoneWeight2", sql.Numeric(18, 3), parseFloat(splitItem.stoneWeight) || 0)
      .query(`
        INSERT INTO tblSplitHistory (
          InventoryCode, Description, OriginalWeight, OriginalPMC, OriginalSMC, OriginalGoldLoss, OriginalTotalCost,
          Weight1, PMC1, SMC1, GoldLoss1, TotalCost1,
          DestInventoryCode, DestDescription, Weight2, PMC2, SMC2, GoldLoss2, TotalCost2,
          UserID, SplitDate, Locationcode, InventoryCode1,
          OriginalStoneWeight, StoneWeight1, StoneWeight2
        ) VALUES (
          @InventoryCode, @Description, @OriginalWeight, @OriginalPMC, @OriginalSMC, @OriginalGoldLoss, @OriginalTotalCost,
          @Weight1, @PMC1, @SMC1, @GoldLoss1, @TotalCost1,
          @DestInventoryCode, @DestDescription, @Weight2, @PMC2, @SMC2, @GoldLoss2, @TotalCost2,
          @UserID, GETDATE(), @Locationcode, @InventoryCode1,
          @OriginalStoneWeight, @StoneWeight1, @StoneWeight2
        )
      `);

    // 6. Handle Additional Description if present
    if (additionalDescription) {
      await new sql.Request(transaction)
        .input("code", sql.VarChar, originalInvCode)
        .query("DELETE FROM tblInventoryAddlDescription WHERE InventoryCode = @code");

      const reqAddl = new sql.Request(transaction);
      await reqAddl
        .input("InventoryCode", sql.VarChar, originalInvCode)
        .input("JobSheetNo", sql.VarChar, originalDetails.JobSheetNo || "")
        .input("Description1", sql.VarChar, additionalDescription.desc1 || "")
        .input("Description2", sql.VarChar, additionalDescription.desc2 || "")
        .input("Description3", sql.VarChar, additionalDescription.desc3 || "")
        .input("Description4", sql.VarChar, additionalDescription.desc4 || "")
        .input("Cost1", sql.Numeric(18, 2), parseFloat(additionalDescription.cost1) || 0)
        .input("Cost2", sql.Numeric(18, 2), parseFloat(additionalDescription.cost2) || 0)
        .input("Cost3", sql.Numeric(18, 2), parseFloat(additionalDescription.cost3) || 0)
        .input("Cost4", sql.Numeric(18, 2), parseFloat(additionalDescription.cost4) || 0)
        .input("SellingPrice1", sql.Numeric(18, 2), parseFloat(additionalDescription.sp1) || 0)
        .input("SellingPrice2", sql.Numeric(18, 2), parseFloat(additionalDescription.sp2) || 0)
        .input("SellingPrice3", sql.Numeric(18, 2), parseFloat(additionalDescription.sp3) || 0)
        .input("SellingPrice4", sql.Numeric(18, 2), parseFloat(additionalDescription.sp4) || 0)
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

    // 7. Update Category NextInventoryCode if code was incremented
    if (originalInvCode.trim() !== selectedInventoryCode.trim()) {
      await new sql.Request(transaction)
        .input("categoryCode", sql.VarChar, originalItem.categoryCode)
        .query(`
          UPDATE tblCategory 
          SET NextInventoryCode = ISNULL(NextInventoryCode, 1) + 1 
          WHERE CategoryCode = @categoryCode
        `);
    }

    if (!splitItem.isScrap) {
      await new sql.Request(transaction)
        .input("categoryCode", sql.VarChar, splitItem.categoryCode)
        .query(`
          UPDATE tblCategory 
          SET NextInventoryCode = ISNULL(NextInventoryCode, 1) + 1 
          WHERE CategoryCode = @categoryCode
        `);
    } else {
      // Increment next scrap number
      await new sql.Request(transaction).query(`
        UPDATE tblCompany SET NextScrapInventoryNo = ISNULL(NextScrapInventoryNo, 1) + 1
      `);
    }

    await transaction.commit();
    res.json({ success: true, message: "Inventory split successfully.", refNo });

  } catch (err) {
    console.error("Split Inventory Transaction Error:", err);
    try {
      await transaction.rollback();
    } catch (rollErr) {
      console.error("Rollback error:", rollErr);
    }
    res.status(500).json({ success: false, message: "Transaction failed. Cannot split inventory.", error: err.message });
  }
});

module.exports = router;
