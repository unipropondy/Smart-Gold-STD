const express = require("express");
const router = express.Router();
const sql = require("mssql");

// GET /api/mergeinventory/detail
router.get("/detail", async (req, res) => {
  try {
    const pool = req.pool;
    const { code } = req.query;

    if (!code) {
      return res.status(400).json({ success: false, message: "Inventory code is required." });
    }

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
        b.LocationCode,
        b.PCS
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
      return res.status(400).json({ success: false, message: "This Item already sold. Can not merge!" });
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

// GET /api/mergeinventory/categories
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

// GET /api/mergeinventory/catalogues
router.get("/catalogues", async (req, res) => {
  try {
    const pool = req.pool;
    const { categoryCode } = req.query;

    let query = "SELECT CatalogueCode, CatalogueName, CategoryCode FROM tblCatalogue";
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

// GET /api/mergeinventory/sizes
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

// GET /api/mergeinventory/nextCode
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

// POST /api/mergeinventory/merge
router.post("/merge", async (req, res) => {
  const pool = req.pool;
  const transaction = new sql.Transaction(pool);

  try {
    let {
      item1Code,
      item2Code,
      isMergeWeightOnly,
      mergeWeight,
      newCategoryCode,
      newCatalogueCode,
      newInventoryCode,
      newDescription,
      newDiamondDescription,
      newSizeCode,
      newSizeType,
      newSMC,
      remarks,
      item1Details,
      item2Details,
      mergedCalculations,
      additionalDescription,
      userId
    } = req.body;

    item1Code = (item1Code || "").trim();
    item2Code = item2Code ? item2Code.trim() : null;
    newCategoryCode = (newCategoryCode || "").trim();
    newCatalogueCode = (newCatalogueCode || "").trim();
    newInventoryCode = (newInventoryCode || "").trim();
    newDescription = (newDescription || "").trim();
    newDiamondDescription = (newDiamondDescription || "").trim();
    newSizeType = (newSizeType || "").trim();
    remarks = (remarks || "").trim();

    if (!item1Code || !newInventoryCode || !newCategoryCode || !newCatalogueCode) {
      return res.status(400).json({ success: false, message: "Missing required fields for merge." });
    }

    const sysUser = userId || "sys";
    await transaction.begin();

    // 1. Fetch Company Config
    const compResult = await new sql.Request(transaction).query(`
      SELECT TOP 1 LocationCode, CompanyShortCode, PurchaseCostingGL FROM tblCompany
    `);
    const comp = compResult.recordset[0] || {};
    const loc = (comp.LocationCode || "HQ").trim();

    const refNo = "ME" + String(Date.now()).substring(4);

    // 2. Movement logs for retiring the original items
    // Retiring Item 1
    await new sql.Request(transaction)
      .input("LocationCode", sql.VarChar, loc)
      .input("InventoryCode", sql.VarChar, item1Code)
      .input("Description", sql.VarChar, item1Details.Description || "")
      .input("RefNumber", sql.VarChar, refNo)
      .input("Qty", sql.Numeric(18, 0), -1)
      .input("Weight", sql.Numeric(18, 3), -parseFloat(item1Details.Weight || 0))
      .input("PMC", sql.Numeric(18, 2), -parseFloat(item1Details.PMC || 0))
      .input("CostPerGram", sql.Numeric(18, 2), parseFloat(item1Details.CostperGram || 0))
      .input("TotalCost", sql.Numeric(18, 2), -parseFloat(item1Details.TotalCost || 0))
      .input("SupplierCode", sql.VarChar, item1Details.SupplierCode || "")
      .input("CreateUser", sql.VarChar, sysUser)
      .query(`
        INSERT INTO tblInventoryMovement (
          LocationCode, InventoryCode, Description, Status, RefNumber, RefDate,
          Qty, Weight, PMC, CostPerGram, TotalCost, SupplierCode, CreateUser, CreateDate
        ) VALUES (
          @LocationCode, @InventoryCode, @Description, 'ME', @RefNumber, GETDATE(),
          @Qty, @Weight, @PMC, @CostPerGram, @TotalCost, @SupplierCode, @CreateUser, GETDATE()
        )
      `);

    // Retiring Item 2 if applicable
    if (!isMergeWeightOnly && item2Code) {
      await new sql.Request(transaction)
        .input("LocationCode", sql.VarChar, loc)
        .input("InventoryCode", sql.VarChar, item2Code)
        .input("Description", sql.VarChar, item2Details.Description || "")
        .input("RefNumber", sql.VarChar, refNo)
        .input("Qty", sql.Numeric(18, 0), -1)
        .input("Weight", sql.Numeric(18, 3), -parseFloat(item2Details.Weight || 0))
        .input("PMC", sql.Numeric(18, 2), -parseFloat(item2Details.PMC || 0))
        .input("CostPerGram", sql.Numeric(18, 2), parseFloat(item2Details.CostperGram || 0))
        .input("TotalCost", sql.Numeric(18, 2), -parseFloat(item2Details.TotalCost || 0))
        .input("SupplierCode", sql.VarChar, item2Details.SupplierCode || "")
        .input("CreateUser", sql.VarChar, sysUser)
        .query(`
          INSERT INTO tblInventoryMovement (
            LocationCode, InventoryCode, Description, Status, RefNumber, RefDate,
            Qty, Weight, PMC, CostPerGram, TotalCost, SupplierCode, CreateUser, CreateDate
          ) VALUES (
            @LocationCode, @InventoryCode, @Description, 'ME', @RefNumber, GETDATE(),
            @Qty, @Weight, @PMC, @CostPerGram, @TotalCost, @SupplierCode, @CreateUser, GETDATE()
          )
        `);
    }

    // 3. Delete retired items from active inventory tables (same as MergedUptation)
    await new sql.Request(transaction)
      .input("item1", sql.VarChar, item1Code)
      .query(`
        DELETE FROM tblInventory WHERE InventoryCode = @item1;
        DELETE FROM tblInventoryStock WHERE InventoryCode = @item1;
      `);

    if (!isMergeWeightOnly && item2Code) {
      await new sql.Request(transaction)
        .input("item2", sql.VarChar, item2Code)
        .query(`
          DELETE FROM tblInventory WHERE InventoryCode = @item2;
          DELETE FROM tblInventoryStock WHERE InventoryCode = @item2;
        `);
    }

    // 4. Save new merged inventory item
    const mergedWeight = parseFloat(mergedCalculations.weight) || 0;
    const mergedStoneWeight = parseFloat(mergedCalculations.stoneWeight) || 0;
    const mergedCostPerGram = parseFloat(mergedCalculations.costPerGram) || 0;
    const mergedPMC = parseFloat(mergedCalculations.pmc) || 0;
    const mergedSMC = parseFloat(mergedCalculations.smc) || 0;
    const mergedPCS = parseFloat(mergedCalculations.pcs) || 1;
    const mergedGoldLoss = parseFloat(mergedCalculations.goldLoss) || 0;
    const mergedTotalCost = parseFloat(mergedCalculations.totalCost) || 0;

    const reqInsert = new sql.Request(transaction);
    await reqInsert
      .input("InventoryCode", sql.VarChar, newInventoryCode)
      .input("Description", sql.VarChar, newDescription || "")
      .input("DiamondDescription", sql.VarChar, newDiamondDescription || "")
      .input("Weight", sql.Numeric(18, 3), mergedWeight)
      .input("StoneWeight", sql.Numeric(18, 3), mergedStoneWeight)
      .input("CostPerGram", sql.Numeric(18, 2), mergedCostPerGram)
      .input("PMC", sql.Numeric(18, 2), mergedPMC)
      .input("SMC", sql.Numeric(18, 2), mergedSMC)
      .input("PCS", sql.Numeric(18, 0), mergedPCS)
      .input("GoldLoss", sql.Numeric(18, 3), mergedGoldLoss)
      .input("TotalCost", sql.Numeric(18, 2), mergedTotalCost)
      .input("JobSheetNo", sql.VarChar, item1Details.JobSheetNo || "")
      .input("PurchaseBillNo", sql.VarChar, item1Details.PurchaseBillNo || "")
      .input("PurchaseDate", sql.SmallDateTime, item1Details.PurchaseDate ? new Date(item1Details.PurchaseDate) : new Date())
      .input("SupplierCode", sql.VarChar, item1Details.SupplierCode || "")
      .input("CategoryCode", sql.VarChar, newCategoryCode)
      .input("ClassCode", sql.VarChar, item1Details.ClassCode)
      .input("CatalogueCode", sql.VarChar, newCatalogueCode)
      .input("TrayId", sql.VarChar, item1Details.TrayId || "")
      .input("PMCType", sql.VarChar, item1Details.PMCType || "W")
      .input("SMCType", sql.VarChar, item1Details.SMCType || "W")
      .input("Remarks", sql.VarChar, remarks || "")
      .input("LocationCode", sql.VarChar, loc)
      .input("SizeCode", sql.Numeric(18, 2), parseFloat(newSizeCode) || 0)
      .input("SizeType", sql.VarChar, newSizeType || "")
      .input("CreateUser", sql.VarChar, sysUser)
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
        );

        INSERT INTO tblInventoryStock (
          LocationCode, InventoryCode, Description, DiamondDescription, Weight, PCS, TotalCost,
          QtyOnHand, QtySold, OrderQty, ConsignmentQty, Status, CreateUser, CreateDate, ModifyUser, ModifyDate
        ) VALUES (
          @LocationCode, @InventoryCode, @Description, @DiamondDescription, @Weight, @PCS, @TotalCost,
          @PCS, 0, 0, 0, 'PU', @CreateUser, GETDATE(), @CreateUser, GETDATE()
        );
      `);

    // If not HQ, also save to HQ location as per legacy code
    if (loc !== "HQ") {
      await new sql.Request(transaction)
        .input("InventoryCode", sql.VarChar, newInventoryCode)
        .input("Description", sql.VarChar, newDescription || "")
        .input("DiamondDescription", sql.VarChar, newDiamondDescription || "")
        .input("Weight", sql.Numeric(18, 3), mergedWeight)
        .input("StoneWeight", sql.Numeric(18, 3), mergedStoneWeight)
        .input("CostPerGram", sql.Numeric(18, 2), mergedCostPerGram)
        .input("PMC", sql.Numeric(18, 2), mergedPMC)
        .input("SMC", sql.Numeric(18, 2), mergedSMC)
        .input("PCS", sql.Numeric(18, 0), mergedPCS)
        .input("GoldLoss", sql.Numeric(18, 3), mergedGoldLoss)
        .input("TotalCost", sql.Numeric(18, 2), mergedTotalCost)
        .input("JobSheetNo", sql.VarChar, item1Details.JobSheetNo || "")
        .input("PurchaseBillNo", sql.VarChar, item1Details.PurchaseBillNo || "")
        .input("PurchaseDate", sql.SmallDateTime, item1Details.PurchaseDate ? new Date(item1Details.PurchaseDate) : new Date())
        .input("SupplierCode", sql.VarChar, item1Details.SupplierCode || "")
        .input("CategoryCode", sql.VarChar, newCategoryCode)
        .input("ClassCode", sql.VarChar, item1Details.ClassCode)
        .input("CatalogueCode", sql.VarChar, newCatalogueCode)
        .input("TrayId", sql.VarChar, item1Details.TrayId || "")
        .input("PMCType", sql.VarChar, item1Details.PMCType || "W")
        .input("SMCType", sql.VarChar, item1Details.SMCType || "W")
        .input("Remarks", sql.VarChar, remarks || "")
        .input("SizeCode", sql.Numeric(18, 2), parseFloat(newSizeCode) || 0)
        .input("SizeType", sql.VarChar, newSizeType || "")
        .input("CreateUser", sql.VarChar, sysUser)
        .query(`
          INSERT INTO tblInventory_HQ (
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
            'HQ', @SizeCode, @SizeType, 0
          );
        `);
    }

    // 5. Save Merge History Log
    const w1 = parseFloat(item1Details.Weight) || 0;
    const pmc1 = parseFloat(item1Details.PMC) || 0;
    const smc1 = parseFloat(item1Details.sMc) || 0;
    const gl1 = parseFloat(item1Details.GoldLoss) || 0;
    const tc1 = parseFloat(item1Details.TotalCost) || 0;

    let w2 = 0, pmc2 = 0, smc2 = 0, gl2 = 0, tc2 = 0, code2 = "MERGEITEM";
    if (isMergeWeightOnly) {
      w2 = parseFloat(mergeWeight) || 0;
    } else if (item2Details) {
      code2 = item2Details.InventoryCode;
      w2 = parseFloat(item2Details.Weight) || 0;
      pmc2 = parseFloat(item2Details.PMC) || 0;
      smc2 = parseFloat(item2Details.sMc) || 0;
      gl2 = parseFloat(item2Details.GoldLoss) || 0;
      tc2 = parseFloat(item2Details.TotalCost) || 0;
    }

    await new sql.Request(transaction)
      .input("InventoryCode1", sql.VarChar, item1Code)
      .input("Weight1", sql.Numeric(18, 3), w1)
      .input("PMC1", sql.Numeric(18, 2), pmc1)
      .input("SMC1", sql.Numeric(18, 2), smc1)
      .input("GoldLoss1", sql.Numeric(18, 3), gl1)
      .input("TotalCost1", sql.Numeric(18, 2), tc1)
      .input("InventoryCode2", sql.VarChar, code2)
      .input("Weight2", sql.Numeric(18, 3), w2)
      .input("PMC2", sql.Numeric(18, 2), pmc2)
      .input("SMC2", sql.Numeric(18, 2), smc2)
      .input("GoldLoss2", sql.Numeric(18, 3), gl2)
      .input("TotalCost2", sql.Numeric(18, 2), tc2)
      .input("InventoryCodeN", sql.VarChar, newInventoryCode)
      .input("WeightN", sql.Numeric(18, 3), mergedWeight)
      .input("PMCN", sql.Numeric(18, 2), mergedPMC)
      .input("SMCN", sql.Numeric(18, 2), mergedSMC)
      .input("GoldLossN", sql.Numeric(18, 3), mergedGoldLoss)
      .input("TotalCostN", sql.Numeric(18, 2), mergedTotalCost)
      .input("DescriptionNew", sql.VarChar, newDescription || "")
      .input("JobSheetNo", sql.VarChar, item1Details.JobSheetNo || "")
      .input("ClassCode", sql.VarChar, item1Details.ClassCode || "")
      .input("UserID", sql.VarChar, sysUser)
      .input("LocationCode", sql.VarChar, loc)
      .query(`
        INSERT INTO tblMergeHistory (
          InventoryCode1, Weight1, PMC1, SMC1, GoldLoss1, TotalCost1,
          InventoryCode2, Weight2, PMC2, SMC2, GoldLoss2, TotalCost2,
          InventoryCodeN, WeightN, PMCN, SMCN, GoldLossN, TotalCostN, DescriptionNew,
          JobSheetNo, ClassCode, UserID, LocationCode, MergedDate
        ) VALUES (
          @InventoryCode1, @Weight1, @PMC1, @SMC1, @GoldLoss1, @TotalCost1,
          @InventoryCode2, @Weight2, @PMC2, @SMC2, @GoldLoss2, @TotalCost2,
          @InventoryCodeN, @WeightN, @PMCN, @SMCN, @GoldLossN, @TotalCostN, @DescriptionNew,
          @JobSheetNo, @ClassCode, @UserID, @LocationCode, GETDATE()
        )
      `);

    // 6. Movement Log for Merged New Item
    await new sql.Request(transaction)
      .input("LocationCode", sql.VarChar, loc)
      .input("InventoryCode", sql.VarChar, newInventoryCode)
      .input("Description", sql.VarChar, newDescription || "")
      .input("RefNumber", sql.VarChar, refNo)
      .input("Qty", sql.Numeric(18, 0), 1)
      .input("Weight", sql.Numeric(18, 3), mergedWeight)
      .input("PMC", sql.Numeric(18, 2), mergedPMC)
      .input("CostPerGram", sql.Numeric(18, 2), mergedCostPerGram)
      .input("TotalCost", sql.Numeric(18, 2), mergedTotalCost)
      .input("SupplierCode", sql.VarChar, item1Details.SupplierCode || "")
      .input("CreateUser", sql.VarChar, sysUser)
      .query(`
        INSERT INTO tblInventoryMovement (
          LocationCode, InventoryCode, Description, Status, RefNumber, RefDate,
          Qty, Weight, PMC, CostPerGram, TotalCost, SupplierCode, CreateUser, CreateDate
        ) VALUES (
          @LocationCode, @InventoryCode, @Description, 'TI', @RefNumber, GETDATE(),
          @Qty, @Weight, @PMC, @CostPerGram, @TotalCost, @SupplierCode, @CreateUser, GETDATE()
        )
      `);

    // If not HQ, log movement at HQ as well
    if (loc !== "HQ") {
      await new sql.Request(transaction)
        .input("InventoryCode", sql.VarChar, newInventoryCode)
        .input("Description", sql.VarChar, newDescription || "")
        .input("RefNumber", sql.VarChar, refNo)
        .input("Weight", sql.Numeric(18, 3), mergedWeight)
        .input("PMC", sql.Numeric(18, 2), mergedPMC)
        .input("TotalCost", sql.Numeric(18, 2), mergedTotalCost)
        .input("CreateUser", sql.VarChar, sysUser)
        .query(`
          INSERT INTO tblInventoryMovement (
            LocationCode, InventoryCode, Description, Status, RefNumber, RefDate,
            Qty, Weight, PMC, CostPerGram, TotalCost, CreateUser, CreateDate
          ) VALUES (
            'HQ', @InventoryCode, @Description, 'PU', @RefNumber, GETDATE(),
            1, @Weight, @PMC, 0, @TotalCost, @CreateUser, GETDATE()
          );

          INSERT INTO tblInventoryMovement (
            LocationCode, InventoryCode, Description, Status, RefNumber, RefDate,
            Qty, Weight, PMC, CostPerGram, TotalCost, CreateUser, CreateDate
          ) VALUES (
            'HQ', @InventoryCode, @Description, 'TO', @RefNumber, GETDATE(),
            1, @Weight, @PMC, 0, @TotalCost, @CreateUser, GETDATE()
          );
        `);
    }

    // 7. Additional Description Save
    if (additionalDescription) {
      await new sql.Request(transaction)
        .input("InventoryCode", sql.VarChar, newInventoryCode)
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
            InventoryCode, Description1, Description2, Description3, Description4,
            Cost1, Cost2, Cost3, Cost4, SellingPrice1, SellingPrice2, SellingPrice3, SellingPrice4,
            CreateUser, CreateDate, ModifyUser, ModifyDate
          ) VALUES (
            @InventoryCode, @Description1, @Description2, @Description3, @Description4,
            @Cost1, @Cost2, @Cost3, @Cost4, @SellingPrice1, @SellingPrice2, @SellingPrice3, @SellingPrice4,
            @CreateUser, GETDATE(), @CreateUser, GETDATE()
          )
        `);
    }

    // 8. Update Next Available Code in Category
    const numPart = parseInt(newInventoryCode.replace(/^\D+/g, "")) || 0;
    if (numPart > 0) {
      const nextNum = numPart + 1;
      const prefix = newInventoryCode.replace(/\d+$/, "");
      const paddedNum = String(nextNum).padStart(newInventoryCode.length - prefix.length, "0");
      const nextAvailableCode = `${prefix}${paddedNum}`;

      await new sql.Request(transaction)
        .input("nextCode", sql.VarChar, nextAvailableCode)
        .input("catCode", sql.VarChar, newCategoryCode)
        .query("UPDATE tblCategory SET NextInventoryCode = @nextCode WHERE CategoryCode = @catCode");
    }

    await transaction.commit();
    res.json({ success: true, message: "Inventory Merged Successfully!" });

  } catch (err) {
    console.error("Merge Inventory Transaction Error:", err);
    await transaction.rollback();
    res.status(500).json({ success: false, message: "Database transaction failed", error: err.message });
  }
});

module.exports = router;
