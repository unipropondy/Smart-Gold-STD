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

    if (!header.SupplierCode || !header.SupplierCode.trim()) {
      return res.status(400).json({ success: false, message: "Enter supplier code." });
    }

    // Verify SupplierCode exists in tblSupplier
    const supCheck = await pool.request()
      .input('SupplierCode', sql.VarChar, header.SupplierCode.trim())
      .query("SELECT 1 FROM tblSupplier WHERE SupplierCode = @SupplierCode");
    if (supCheck.recordset.length === 0) {
      return res.status(400).json({ success: false, message: "Invalid supplier code!" });
    }

    const currencyCode = (header.Currency || header.CurrencyCode || '').trim();
    if (!currencyCode) {
      return res.status(400).json({ success: false, message: "Enter currency code." });
    }

    // Verify CurrencyCode exists in tblCurrency
    const currCheck = await pool.request()
      .input('CurrencyCode', sql.VarChar, currencyCode)
      .query("SELECT 1 FROM tblCurrency WHERE CurrencyCode = @CurrencyCode");
    if (currCheck.recordset.length === 0) {
      return res.status(400).json({ success: false, message: "Set Default Currency in Currency Master" });
    }

    const currRate = parseFloat(header.Rate || header.CurrencyRate) || 0;
    if (currRate <= 0) {
      return res.status(400).json({ success: false, message: "Enter currency rate." });
    }

    const payable = header.PaymentType || (header.PayableGold ? 'G' : header.PayableCash ? 'C' : '');
    if (!payable) {
      return res.status(400).json({ success: false, message: "Select payment type." });
    }

    const sysUser = userId || "SYSTEM";
    const curDate = new Date();
    const purchaseBillNo = header.PurchaseBillNo.trim();

    await transaction.begin();

    // Clean up any existing records for this PurchaseBillNo AND for the items being saved
    const invCodesToDelete = details
      .map(d => (d.InventoryCode || d.ItemCode || '').trim())
      .filter(c => c.length > 0);

    const deleteReq = new sql.Request(transaction);
    deleteReq.input("PurchaseBillNo", sql.VarChar, purchaseBillNo);

    if (invCodesToDelete.length > 0) {
      invCodesToDelete.forEach((code, idx) => {
        deleteReq.input(`c_${idx}`, sql.VarChar, code);
      });
      const codeParamList = invCodesToDelete.map((_, idx) => `@c_${idx}`).join(', ');
      await deleteReq.query(`
        DELETE FROM tblInventoryAddlDescription 
        WHERE InventoryCode IN (SELECT InventoryCode FROM tblPurchase WHERE PurchaseBillNo = @PurchaseBillNo)
           OR InventoryCode IN (${codeParamList});

        DELETE FROM tblInventoryStock 
        WHERE InventoryCode IN (SELECT InventoryCode FROM tblPurchase WHERE PurchaseBillNo = @PurchaseBillNo)
           OR InventoryCode IN (${codeParamList});

        DELETE FROM tblInventory 
        WHERE PurchaseBillNo = @PurchaseBillNo
           OR InventoryCode IN (${codeParamList});

        DELETE FROM tblPurchase 
        WHERE PurchaseBillNo = @PurchaseBillNo
           OR InventoryCode IN (${codeParamList});
      `);
    } else {
      await deleteReq.query(`
        DELETE FROM tblInventoryAddlDescription WHERE InventoryCode IN (SELECT InventoryCode FROM tblPurchase WHERE PurchaseBillNo = @PurchaseBillNo);
        DELETE FROM tblInventoryStock WHERE InventoryCode IN (SELECT InventoryCode FROM tblPurchase WHERE PurchaseBillNo = @PurchaseBillNo);
        DELETE FROM tblInventory WHERE PurchaseBillNo = @PurchaseBillNo;
        DELETE FROM tblPurchase WHERE PurchaseBillNo = @PurchaseBillNo;
      `);
    }

    // Fetch existing columns for tblPurchase, tblInventory, tblInventoryStock
    let purchaseCols = [];
    let inventoryCols = [];
    let stockCols = [];
    let addlCols = [];
    try {
      const pRes = await new sql.Request(transaction).query(`SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tblPurchase'`);
      purchaseCols = pRes.recordset.map(c => c.COLUMN_NAME.toLowerCase());
      const iRes = await new sql.Request(transaction).query(`SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tblInventory'`);
      inventoryCols = iRes.recordset.map(c => c.COLUMN_NAME.toLowerCase());
      const sRes = await new sql.Request(transaction).query(`SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tblInventoryStock'`);
      stockCols = sRes.recordset.map(c => c.COLUMN_NAME.toLowerCase());
      const aRes = await new sql.Request(transaction).query(`SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tblInventoryAddlDescription'`);
      addlCols = aRes.recordset.map(c => c.COLUMN_NAME.toLowerCase());
    } catch (e) {}

    const hasPCol = (c) => purchaseCols.length === 0 || purchaseCols.includes(c.toLowerCase());
    const hasICol = (c) => inventoryCols.length === 0 || inventoryCols.includes(c.toLowerCase());
    const hasSCol = (c) => stockCols.length === 0 || stockCols.includes(c.toLowerCase());
    const hasACol = (c) => addlCols.length === 0 || addlCols.includes(c.toLowerCase());

    // Insert into tblPurchase, tblInventory, tblInventoryStock, and tblInventoryAddlDescription for each detail line
    for (let i = 0; i < details.length; i++) {
      const d = details[i];
      let invCode = d.InventoryCode ? d.InventoryCode.trim() : (d.ItemCode ? d.ItemCode.trim() : `${purchaseBillNo}-${i + 1}`);
      const pcsVal = parseInt(d.PCS, 10) || 1;

      // 1. Dynamic Insert into tblPurchase
      const insertReq = new sql.Request(transaction);
      const pC = [];
      const pV = [];
      const addP = (col, type, val) => {
        if (hasPCol(col)) {
          pC.push(col);
          pV.push(`@p_${col}`);
          insertReq.input(`p_${col}`, type, val);
        }
      };

      addP("LocationCode", sql.VarChar, header.LocationCode || "HQ");
      addP("JobSheetNo", sql.VarChar, header.JobSheetNo || "");
      addP("DirectPurchaseNo", sql.VarChar, d.DirectPurchaseNo || "");
      addP("PurchaseBillNo", sql.VarChar, purchaseBillNo);
      addP("SupplierCode", sql.VarChar, header.SupplierCode || "");
      addP("InventoryCode", sql.VarChar, invCode);
      addP("Description", sql.VarChar, d.InventoryDescription || d.Description || "");
      addP("DiamondDescription", sql.VarChar, d.DiamondDescription || "");
      addP("StoneWeight", sql.Numeric(18, 3), parseFloat(d.StoneWeight) || 0);
      addP("CostPerGram", sql.Numeric(18, 2), parseFloat(d.CostPerGram) || 0);
      addP("Size", sql.Numeric(18, 2), parseFloat(d.Size) || 0);
      addP("Weight", sql.Numeric(18, 3), parseFloat(d.Weight) || 0);
      addP("MC", sql.Numeric(18, 2), parseFloat(d.PMCCost || d.PMC || d.MC) || 0);
      addP("PCS", sql.Numeric(18, 0), pcsVal);
      addP("GoldLoss", sql.Numeric(18, 3), parseFloat(d.GoldLoss) || 0);
      addP("TotalCost", sql.Numeric(18, 2), parseFloat(d.TotalCost) || 0);
      addP("CatalogueCode", sql.VarChar, d.CatalogueCode || "");
      addP("CategoryCode", sql.VarChar, d.CategoryCode || "");
      addP("ClassCode", sql.VarChar, d.ClassCode || "");
      addP("TrayId", sql.VarChar, d.TrayId || "");
      addP("PurchaseType", sql.VarChar, d.PurchaseType || "JP");
      addP("Remarks", sql.VarChar, header.Remarks || "");
      addP("JournalNo", sql.VarChar, d.JournalNo || "");
      addP("CreateUser", sql.VarChar, sysUser);
      addP("CreateDate", sql.SmallDateTime, curDate);
      addP("ModifyUser", sql.VarChar, sysUser);
      addP("ModifyDate", sql.SmallDateTime, curDate);

      await insertReq.query(`
        INSERT INTO tblPurchase (${pC.join(', ')})
        VALUES (${pV.join(', ')})
      `);

      // 2. Dynamic Insert into tblInventory
      const invReq = new sql.Request(transaction);
      const iC = [];
      const iV = [];
      const addI = (col, type, val) => {
        if (hasICol(col)) {
          iC.push(col);
          iV.push(`@inv_${col}`);
          invReq.input(`inv_${col}`, type, val);
        }
      };

      addI("InventoryCode", sql.VarChar, invCode);
      addI("Description", sql.VarChar, d.InventoryDescription || d.Description || "");
      addI("DiamondDescription", sql.VarChar, d.DiamondDescription || "");
      addI("Size", sql.Numeric(18, 2), parseFloat(d.Size) || 0);
      addI("Weight", sql.Numeric(18, 3), parseFloat(d.Weight) || 0);
      addI("StoneWeight", sql.Numeric(18, 3), parseFloat(d.StoneWeight) || 0);
      addI("CostPerGram", sql.Numeric(18, 2), parseFloat(d.CostPerGram) || 0);
      addI("PMC", sql.Numeric(18, 2), parseFloat(d.PMCCost || d.PMC) || 0);
      addI("SMC", sql.Numeric(18, 2), parseFloat(d.SMCCost || d.SMC) || 0);
      addI("GoldLoss", sql.Numeric(18, 3), parseFloat(d.GoldLoss) || 0);
      addI("TotalCost", sql.Numeric(18, 2), parseFloat(d.TotalCost) || 0);
      addI("JobSheetNo", sql.VarChar, header.JobSheetNo || "");
      addI("PurchaseBillNo", sql.VarChar, purchaseBillNo);
      addI("PurchaseDate", sql.SmallDateTime, curDate);
      addI("SupplierCode", sql.VarChar, header.SupplierCode || "");
      addI("SalesBillNo", sql.VarChar, "");
      addI("SalesDate", sql.SmallDateTime, null);
      addI("SalesCostPerGram", sql.Numeric(18, 2), 0);
      addI("SalesMC", sql.Numeric(18, 2), 0);
      addI("SalesAmount", sql.Numeric(18, 2), 0);
      addI("CategoryCode", sql.VarChar, d.CategoryCode || "");
      addI("ClassCode", sql.VarChar, d.ClassCode || "");
      addI("CatalogueCode", sql.VarChar, d.CatalogueCode || "");
      addI("TrayId", sql.VarChar, d.TrayId || "");
      addI("PMCType", sql.VarChar, d.PMCType || (d.PurchaseMCType === 'PerPCS' ? 'P' : 'W'));
      addI("SMCType", sql.VarChar, d.SMCType || (d.SalesMCType === 'PerPCS' ? 'P' : 'W'));
      addI("OrderNo", sql.VarChar, d.OrderNo || "");
      addI("Remarks", sql.VarChar, header.Remarks || "");
      addI("CreateUser", sql.VarChar, sysUser);
      addI("CreateDate", sql.SmallDateTime, curDate);
      addI("ModifyUser", sql.VarChar, sysUser);
      addI("ModifyDate", sql.SmallDateTime, curDate);
      addI("LocationCode", sql.VarChar, header.LocationCode || "HQ");
      addI("SizeCode", sql.Numeric(18, 2), parseFloat(d.Size) || 0);
      addI("SizeType", sql.VarChar, d.SizeType || "");
      addI("SalesPCS", sql.Bit, d.SalesPCS ? 1 : 0);

      await invReq.query(`
        INSERT INTO tblInventory (${iC.join(', ')})
        VALUES (${iV.join(', ')})
      `);

      // 3. Dynamic Insert into tblInventoryStock
      const stockReq = new sql.Request(transaction);
      const sC = [];
      const sV = [];
      const addS = (col, type, val) => {
        if (hasSCol(col)) {
          sC.push(col);
          sV.push(`@stk_${col}`);
          stockReq.input(`stk_${col}`, type, val);
        }
      };

      addS("LocationCode", sql.VarChar, header.LocationCode || "HQ");
      addS("InventoryCode", sql.VarChar, invCode);
      addS("Description", sql.VarChar, d.InventoryDescription || d.Description || "");
      addS("DiamondDescription", sql.VarChar, d.DiamondDescription || "");
      addS("Weight", sql.Numeric(18, 3), parseFloat(d.Weight) || 0);
      addS("PCS", sql.Numeric(18, 0), pcsVal);
      addS("TotalCost", sql.Numeric(18, 2), parseFloat(d.TotalCost) || 0);
      addS("QtyOnHand", sql.Numeric(18, 0), pcsVal);
      addS("QtySold", sql.Numeric(18, 0), 0);
      addS("Status", sql.VarChar, 'IN');
      addS("CreateUser", sql.VarChar, sysUser);
      if (hasSCol("CreateDate")) {
        sC.push("CreateDate");
        sV.push("GETDATE()");
      }
      addS("ModifyUser", sql.VarChar, sysUser);
      if (hasSCol("ModifyDate")) {
        sC.push("ModifyDate");
        sV.push("GETDATE()");
      }

      await stockReq.query(`
        INSERT INTO tblInventoryStock (${sC.join(', ')})
        VALUES (${sV.join(', ')})
      `);

      // 4. Dynamic Insert into tblInventoryAddlDescription if AddlDesc is active
      if (d.AddlDesc && (d.Desc1 || d.Desc2 || d.Desc3 || d.Desc4 || d.Cost1 || d.Cost2 || d.Cost3 || d.Cost4)) {
        const addlReq = new sql.Request(transaction);
        const aC = [];
        const aV = [];
        const addA = (col, type, val) => {
          if (hasACol(col)) {
            aC.push(col);
            aV.push(`@add_${col}`);
            addlReq.input(`add_${col}`, type, val);
          }
        };

        addA("InventoryCode", sql.VarChar, invCode);
        addA("JobSheetNo", sql.VarChar, header.JobSheetNo || "");
        addA("Description1", sql.VarChar, d.Desc1 || "");
        addA("Description2", sql.VarChar, d.Desc2 || "");
        addA("Description3", sql.VarChar, d.Desc3 || "");
        addA("Description4", sql.VarChar, d.Desc4 || "");
        addA("Cost1", sql.Numeric(18, 2), parseFloat(d.Cost1) || 0);
        addA("Cost2", sql.Numeric(18, 2), parseFloat(d.Cost2) || 0);
        addA("Cost3", sql.Numeric(18, 2), parseFloat(d.Cost3) || 0);
        addA("Cost4", sql.Numeric(18, 2), parseFloat(d.Cost4) || 0);
        addA("SellingPrice1", sql.Numeric(18, 2), parseFloat(d.SellingPrice1) || 0);
        addA("SellingPrice2", sql.Numeric(18, 2), parseFloat(d.SellingPrice2) || 0);
        addA("SellingPrice3", sql.Numeric(18, 2), parseFloat(d.SellingPrice3) || 0);
        addA("SellingPrice4", sql.Numeric(18, 2), parseFloat(d.SellingPrice4) || 0);
        addA("CreateUser", sql.VarChar, sysUser);
        if (hasACol("CreateDate")) {
          aC.push("CreateDate");
          aV.push("GETDATE()");
        }
        addA("ModifyUser", sql.VarChar, sysUser);
        if (hasACol("ModifyDate")) {
          aC.push("ModifyDate");
          aV.push("GETDATE()");
        }

        await addlReq.query(`
          INSERT INTO tblInventoryAddlDescription (${aC.join(', ')})
          VALUES (${aV.join(', ')})
        `);
      }
    }

    await transaction.commit();
    return res.status(200).json({ success: true, message: "Purchase saved successfully" });
  } catch (err) {
    if (transaction) {
      try { await transaction.rollback(); } catch (_) {}
    }
    console.error("🔥 POST PURCHASE ERROR:", err);
    return res.status(500).json({ 
      success: false, 
      message: err.message || "Server Error", 
      error: err.message 
    });
  }
});

// ================= DELETE ENTIRE PURCHASE BILL =================
router.delete("/:purchaseBillNo", async (req, res) => {
  const pool = req.pool;
  const transaction = new sql.Transaction(pool);

  try {
    const { purchaseBillNo } = req.params;
    const billNo = purchaseBillNo.trim();

    // Validate that no items in this bill are already sold or past EOD
    const checkReq = await pool.request()
      .input("PurchaseBillNo", sql.VarChar, billNo)
      .query(`
        SELECT 
          i.InventoryCode,
          i.SalesBillNo,
          i.CreateDate,
          i.PCS,
          st.QtyOnHand,
          jh.BillClose,
          jh.TranType,
          loc.EODDate
        FROM tblInventory i
        LEFT JOIN tblInventoryStock st ON i.InventoryCode = st.InventoryCode AND i.LocationCode = st.LocationCode
        LEFT JOIN tblJobSheetHeader jh ON i.JobSheetNo = jh.JobSheetNo
        LEFT JOIN tblLocation loc ON i.LocationCode = loc.LocationCode
        WHERE i.PurchaseBillNo = @PurchaseBillNo
      `);

    if (checkReq.recordset.length > 0) {
      for (const item of checkReq.recordset) {
        if (item.BillClose === 'Y') {
          return res.status(400).json({ success: false, message: "This bill is closed. Cannot delete!" });
        }
        if (item.TranType === 'DP') {
          return res.status(400).json({ success: false, message: "This is a direct purchase bill. Cannot delete!" });
        }
        if (item.SalesBillNo && item.SalesBillNo.trim() !== '') {
          return res.status(400).json({ success: false, message: `Item ${item.InventoryCode} is sold. Cannot delete!` });
        }
        if (item.QtyOnHand != null && parseFloat(item.QtyOnHand) !== parseFloat(item.PCS)) {
          return res.status(400).json({ success: false, message: `Item ${item.InventoryCode}: Qty on hand is not the same as no. of pieces purchased. Cannot delete!` });
        }
        if (item.EODDate && item.CreateDate) {
          const eod = new Date(item.EODDate);
          const crDate = new Date(item.CreateDate);
          if (eod >= crDate) {
            return res.status(400).json({ success: false, message: "You cannot delete the Inventory after EOD!" });
          }
        }
      }
    }

    await transaction.begin();

    const request = new sql.Request(transaction);
    await request
      .input("PurchaseBillNo", sql.VarChar, billNo)
      .query(`
        DELETE FROM tblInventoryAddlDescription WHERE InventoryCode IN (SELECT InventoryCode FROM tblPurchase WHERE PurchaseBillNo = @PurchaseBillNo);
        DELETE FROM tblInventoryStock WHERE InventoryCode IN (SELECT InventoryCode FROM tblPurchase WHERE PurchaseBillNo = @PurchaseBillNo);
        DELETE FROM tblPurchase WHERE PurchaseBillNo = @PurchaseBillNo;
        DELETE FROM tblInventory WHERE PurchaseBillNo = @PurchaseBillNo;
      `);

    await transaction.commit();
    return res.status(200).json({ success: true, message: "Purchase deleted successfully" });
  } catch (err) {
    if (transaction) {
      try { await transaction.rollback(); } catch (_) {}
    }
    console.error("🔥 DELETE PURCHASE ERROR:", err);
    return res.status(500).json({ success: false, message: "Server Error", error: err.message });
  }
});

// ================= DELETE SINGLE INVENTORY ITEM FROM PURCHASE (btnDelete in frmPurchase.vb) =================
router.delete("/:purchaseBillNo/item/:inventoryCode", async (req, res) => {
  const pool = req.pool;
  let transaction;

  try {
    const { purchaseBillNo, inventoryCode } = req.params;
    const invCode = inventoryCode.trim();
    const billNo = (purchaseBillNo || "").trim();
    const supplierCode = (req.query.supplierCode || "").trim();
    const userId = (req.query.userId || req.headers['x-user-id'] || "SYSTEM").trim();

    // Check business rules from frmPurchase.vb btnDelete_Click
    const chkResult = await pool.request()
      .input("InventoryCode", sql.VarChar, invCode)
      .query(`
        SELECT 
          i.InventoryCode, 
          i.SalesBillNo, 
          i.CreateDate, 
          i.LocationCode,
          i.JobSheetNo,
          st.QtyOnHand,
          st.PCS,
          jh.BillClose,
          jh.TranType,
          loc.EODDate
        FROM tblInventory i WITH (NOLOCK)
        LEFT JOIN tblInventoryStock st WITH (NOLOCK) ON i.InventoryCode = st.InventoryCode AND i.LocationCode = st.LocationCode
        LEFT JOIN tblJobSheetHeader jh WITH (NOLOCK) ON i.JobSheetNo = jh.JobSheetNo
        LEFT JOIN tblLocation loc WITH (NOLOCK) ON i.LocationCode = loc.LocationCode
        WHERE i.InventoryCode = @InventoryCode
      `);

    if (chkResult.recordset.length > 0) {
      const item = chkResult.recordset[0];
      if (item.BillClose === 'Y') {
        return res.status(400).json({ success: false, message: "This bill is closed. Cannot delete!" });
      }
      if (item.TranType === 'DP') {
        return res.status(400).json({ success: false, message: "This is a direct purchase bill. Cannot delete!" });
      }
      if (item.SalesBillNo && item.SalesBillNo.trim() !== '') {
        return res.status(400).json({ success: false, message: "Item sold. Cannot delete!" });
      }
      if (item.QtyOnHand != null && item.PCS != null && parseFloat(item.QtyOnHand) !== parseFloat(item.PCS)) {
        return res.status(400).json({ success: false, message: "Qty on hand is not the same as no. of pieces purchased. Cannot delete!" });
      }
      if (item.EODDate && item.CreateDate) {
        const eod = new Date(item.EODDate);
        const crDate = new Date(item.CreateDate);
        if (eod >= crDate) {
          return res.status(400).json({ success: false, message: "You cannot delete the Inventory after EOD!" });
        }
      }
    }

    transaction = new sql.Transaction(pool);
    await transaction.begin();

    // 1. Try sp_JpInventorydelete first (matching clsPurchase.vb PurchaseDelete)
    let spDeleteExecuted = false;
    try {
      const spDelReq = new sql.Request(transaction);
      spDelReq
        .input("sinventorycode", sql.VarChar, invCode)
        .input("sInvoiceNo", sql.VarChar, billNo)
        .input("sSupplierCode", sql.VarChar, supplierCode)
        .input("sUserID", sql.VarChar, userId);

      await spDelReq.execute("sp_JpInventorydelete");
      spDeleteExecuted = true;
    } catch (spErr) {
      // If SP does not exist or parameter mismatch, proceed with table deletion
      console.warn("sp_JpInventorydelete failed or not found, falling back to direct SQL deletion:", spErr.message);
    }

    // 2. Direct SQL deletion fallback if SP did not execute
    if (!spDeleteExecuted) {
      const delReq = new sql.Request(transaction);
      delReq.input("PurchaseBillNo", sql.VarChar, billNo);
      delReq.input("InventoryCode", sql.VarChar, invCode);

      await delReq.query(`
        DELETE FROM tblInventoryMovement WHERE InventoryCode = @InventoryCode;
        DELETE FROM tblInventoryBarCoin WHERE InventoryCode = @InventoryCode;
        DELETE FROM tblInventoryAddlDescription WHERE InventoryCode = @InventoryCode;
        DELETE FROM tblInventoryStock WHERE InventoryCode = @InventoryCode;
        DELETE FROM tblPurchase WHERE InventoryCode = @InventoryCode;
        DELETE FROM tblInventory WHERE InventoryCode = @InventoryCode;
      `);
    }

    await transaction.commit();
    return res.status(200).json({ success: true, message: `Item ${invCode} deleted successfully` });
  } catch (err) {
    if (transaction) {
      try { await transaction.rollback(); } catch (_) {}
    }
    console.error("🔥 DELETE INVENTORY ITEM ERROR:", err);
    return res.status(500).json({ success: false, message: err.message || "Server Error", error: err.message });
  }
});

// ================= GET GOLD LOSS BY WEIGHT (fncGetGoldLossByWeight) =================
router.get("/goldloss/:weight", async (req, res) => {
  try {
    const weight = parseFloat(req.params.weight) || 0;
    const pool = req.pool;
    const result = await pool.request()
      .input("Weight", sql.Decimal(18, 4), weight)
      .query(`SELECT GoldLoss FROM tblGoldLoss WHERE @Weight BETWEEN FromWeight AND ToWeight`);

    if (result.recordset.length > 0) {
      return res.json({ success: true, goldLoss: result.recordset[0].GoldLoss });
    }
    return res.json({ success: true, goldLoss: 0 });
  } catch (err) {
    console.error("🔥 GET GOLD LOSS ERROR:", err);
    return res.status(500).json({ success: false, message: "Server Error", error: err.message });
  }
});

// ================= BILL CLOSE WITH WEIGHT TOLERANCE (btnBillClose in frmPurchase.vb) =================
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
    if (js.TranType === 'DP') {
      return res.status(400).json({ success: false, message: "This is a direct purchase bill. Use direct purchase screen to close this bill!" });
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

    return res.status(200).json({ success: true, message: "Job sheet bill closed successfully" });
  } catch (err) {
    console.error("🔥 BILL CLOSE ERROR:", err);
    return res.status(500).json({ success: false, message: "Server Error", error: err.message });
  }
});

module.exports = router;
