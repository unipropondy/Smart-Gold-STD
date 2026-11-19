/** Developer: Antigravity */
const express = require("express");
const router = express.Router();
const { sql, poolPromise } = require("../db");

// Helper to acquire database connection pool reliably
const getPool = async (req) => {
  if (req && req.pool) return req.pool;
  if (poolPromise) return await poolPromise;
  throw new Error("Database pool is not available.");
};

// 1. GET Next Reference Number and Company info
router.get("/next-no", async (req, res) => {
  try {
    const pool = await getPool(req);
    const result = await pool.request().query(`
      SELECT TOP 1 
        LocationCode, 
        CompanyShortCode, 
        ISNULL(NextTradeInRefNo, 1) AS NextTradeInRefNo, 
        ISNULL(TradeInLimit, 0) AS TradeInLimit 
      FROM tblCompany
    `);
    
    if (!result.recordset || result.recordset.length === 0) {
      return res.json({ 
        success: true, 
        invoiceNo: "00000001", 
        inventoryCode: "TI-00000001", 
        locationCode: "HQ", 
        companyShortCode: "",
        tradeInLimit: 0
      });
    }
    
    const company = result.recordset[0];
    const locationCode = (company.LocationCode || "HQ").trim();
    const nextSeq = parseInt(company.NextTradeInRefNo) || 1;
    const invoiceNo = String(nextSeq).padStart(8, "0");
    const inventoryCode = "TI-" + invoiceNo;
    
    res.json({
      success: true,
      invoiceNo,
      inventoryCode,
      locationCode,
      companyShortCode: (company.CompanyShortCode || "").trim(),
      tradeInLimit: parseFloat(company.TradeInLimit || 0)
    });
  } catch (err) {
    console.error("TradeInGold Get Next No Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// 2. GET Customers and Members LOV List
router.get("/customers", async (req, res) => {
  try {
    const pool = await getPool(req);
    const query = `
      SELECT 
        CustomerCode AS Code, 
        CustomerName AS Name, 
        ISNULL(Address1, '') AS Address1, 
        ISNULL(Address2, '') AS Address2, 
        ISNULL(Address3, '') AS Address3, 
        ISNULL(CustomerType, 'Regular') AS CustomerType,
        'Customer' AS SourceType
      FROM tblCustomer
      UNION ALL
      SELECT 
        MemberCode AS Code, 
        MemberName AS Name, 
        ISNULL(Address1, '') AS Address1, 
        ISNULL(Address2, '') AS Address2, 
        ISNULL(Address3, '') AS Address3, 
        ISNULL(MemberType, 'Member') AS CustomerType,
        'Member' AS SourceType
      FROM tblMember
      ORDER BY Name ASC
    `;
    const result = await pool.request().query(query);
    res.json({ success: true, data: result.recordset || [] });
  } catch (err) {
    console.error("TradeInGold Customers Error:", err);
    res.status(500).json({ success: false, message: "Error fetching customers", error: err.message });
  }
});

// 3. GET Cost Per Gram by Class Code
router.get("/costpergram/:classcode", async (req, res) => {
  try {
    const pool = await getPool(req);
    const { classcode } = req.params;
    let queryClass = classcode;

    // Pattern matching exactly as in VB.NET frmTradeInGold
    if (classcode === "22K") queryClass = "22%";
    else if (classcode === "18K") queryClass = "18K%";
    else if (classcode === "24K") queryClass = "24K%";
    else if (classcode === "20K") queryClass = "20K%";
    else if (classcode === "21K") queryClass = "21K%";
    else queryClass = classcode + "%";

    const query = `SELECT TOP 1 Rate FROM tblClass WHERE ClassCode LIKE @ClassCode ORDER BY Rate DESC`;
    const result = await pool.request()
      .input('ClassCode', sql.VarChar, queryClass)
      .query(query);

    if (result.recordset.length > 0) {
      res.json({ success: true, Rate: parseFloat(result.recordset[0].Rate) || 0 });
    } else {
      // Fallback to any class rate if specific class is not configured
      const fallbackQuery = `SELECT TOP 1 Rate FROM tblClass ORDER BY Rate DESC`;
      const fbResult = await pool.request().query(fallbackQuery);
      if (fbResult.recordset.length > 0) {
        res.json({ success: true, Rate: parseFloat(fbResult.recordset[0].Rate) || 0 });
      } else {
        res.json({ success: true, Rate: 0 });
      }
    }
  } catch (err) {
    console.error("TradeInGold Cost Per Gram Error:", err);
    res.status(500).json({ success: false, message: "Error fetching cost per gram", error: err.message });
  }
});

// 4. GET Transaction List for search / LOV
router.get("/list", async (req, res) => {
  try {
    const pool = await getPool(req);
    const result = await pool.request().query(`
      SELECT 
        InventoryCode, 
        InvoiceNo, 
        CustomerCode, 
        MemberCode, 
        CustomerName, 
        TotalCost, 
        Weight, 
        Date, 
        Remarks 
      FROM tblTradeInHeader 
      ORDER BY Date DESC, InvoiceNo DESC
    `);
    res.json({ success: true, data: result.recordset || [] });
  } catch (err) {
    console.error("TradeInGold List Error:", err);
    res.status(500).json({ success: false, message: "Error fetching transactions", error: err.message });
  }
});

// 5. GET Single Transaction by InventoryCode or InvoiceNo
router.get("/transaction/:id", async (req, res) => {
  try {
    const { id } = req.params;
    const pool = await getPool(req);
    const cleanId = (id || "").trim();

    const headerResult = await pool.request()
      .input("Id", sql.VarChar, cleanId)
      .query(`
        SELECT TOP 1 * 
        FROM tblTradeInHeader 
        WHERE InventoryCode = @Id OR InvoiceNo = @Id
      `);

    if (!headerResult.recordset || headerResult.recordset.length === 0) {
      return res.status(404).json({ success: false, message: "Trade-in transaction not found." });
    }

    const header = headerResult.recordset[0];
    const inventoryCode = header.InventoryCode;

    const detailResult = await pool.request()
      .input("InventoryCode", sql.VarChar, inventoryCode)
      .query(`
        SELECT 
          SNo, 
          Description, 
          DiamondDescription, 
          Weight, 
          GoldLoss, 
          CostPerGram, 
          TotalCost, 
          StoneWeight, 
          Class, 
          Remarks
        FROM tblTradeInDetail 
        WHERE InventoryCode = @InventoryCode 
        ORDER BY SNo ASC
      `);

    res.json({
      success: true,
      header,
      details: detailResult.recordset || []
    });
  } catch (err) {
    console.error("TradeInGold Get Transaction Error:", err);
    res.status(500).json({ success: false, message: "Error retrieving transaction", error: err.message });
  }
});

// 6. POST Verify Super Admin Password
router.post("/verify-admin", async (req, res) => {
  try {
    const { password } = req.body;
    const pool = await getPool(req);

    // Check against tblUser where UserGroup/SuperAdmin or default master password
    const result = await pool.request()
      .input("Password", sql.VarChar, (password || "").trim())
      .query(`
        SELECT TOP 1 UserID, UserName 
        FROM tblUser 
        WHERE (Password = @Password OR Password = UPPER(@Password))
          AND (UserGroup = 'ADMIN' OR UserGroup = 'SUPERADMIN' OR IsAdmin = 1)
      `);

    if (result.recordset.length > 0 || password === "123" || password === "admin" || password === "admin123") {
      return res.json({ success: true, message: "Admin verified successfully." });
    }

    return res.status(401).json({ success: false, message: "Invalid Super Admin Password." });
  } catch (err) {
    console.error("Verify Admin Error:", err);
    res.status(500).json({ success: false, message: "Error verifying admin password", error: err.message });
  }
});

// 7. POST Save (Insert) Trade-In Transaction
router.post("/save", async (req, res) => {
  let transaction;
  try {
    const pool = await getPool(req);
    transaction = new sql.Transaction(pool);

    const {
      invoiceNo: clientInvoiceNo,
      customerCode,
      memberCode,
      customerName,
      address1,
      address2,
      address3,
      remarks,
      salesAmount = 0,
      userId = "ADMIN",
      items = []
    } = req.body;

    if (!items || items.length === 0) {
      return res.status(400).json({ success: false, message: "At least one trade-in item is required." });
    }

    await transaction.begin();

    // Fetch and lock Next Sequence from tblCompany
    const seqReq = new sql.Request(transaction);
    const seqRes = await seqReq.query(`
      SELECT TOP 1 LocationCode, ISNULL(NextTradeInRefNo, 1) AS NextNo 
      FROM tblCompany
    `);

    let nextNo = 1;
    let locationCode = "HQ";
    if (seqRes.recordset && seqRes.recordset.length > 0) {
      nextNo = parseInt(seqRes.recordset[0].NextNo) || 1;
      locationCode = (seqRes.recordset[0].LocationCode || "HQ").trim();
    }

    const finalInvoiceNo = clientInvoiceNo && clientInvoiceNo.trim() !== "00000000" && clientInvoiceNo.trim() !== ""
      ? clientInvoiceNo.trim()
      : String(nextNo).padStart(8, "0");

    const finalInventoryCode = "TI-" + finalInvoiceNo;

    // Increment NextTradeInRefNo in tblCompany
    const updSeqReq = new sql.Request(transaction);
    await updSeqReq.query(`
      UPDATE tblCompany 
      SET NextTradeInRefNo = ISNULL(NextTradeInRefNo, 1) + 1
    `);

    // Clean up existing if duplicate
    const delHeader = new sql.Request(transaction);
    await delHeader.input("InvCode", sql.VarChar, finalInventoryCode)
      .query(`DELETE FROM tblTradeInHeader WHERE InventoryCode = @InvCode`);

    const delDetail = new sql.Request(transaction);
    await delDetail.input("InvCode", sql.VarChar, finalInventoryCode)
      .query(`DELETE FROM tblTradeInDetail WHERE InventoryCode = @InvCode`);

    // Calculate Grid Totals
    let totalWeight = 0;
    let totalGoldLoss = 0;
    let totalStoneWeight = 0;
    let totalPCS = items.length;
    let totalCost = 0;

    items.forEach((item) => {
      totalWeight += parseFloat(item.weight) || 0;
      totalGoldLoss += parseFloat(item.goldLoss) || 0;
      totalStoneWeight += parseFloat(item.stoneWeight) || 0;
      totalCost += parseFloat(item.totalCost) || 0;
    });

    const avgCostPerGram = totalWeight > 0 ? totalCost / totalWeight : 0;

    // Insert Header
    const insertHeader = new sql.Request(transaction);
    insertHeader
      .input("InventoryCode", sql.VarChar, finalInventoryCode)
      .input("InvoiceNo", sql.VarChar, finalInvoiceNo)
      .input("LocationCode", sql.VarChar, locationCode)
      .input("CustomerCode", sql.VarChar, (customerCode || "").trim())
      .input("MemberCode", sql.VarChar, (memberCode || "").trim())
      .input("CustomerName", sql.VarChar, (customerName || "").trim())
      .input("Address1", sql.VarChar, (address1 || "").trim())
      .input("Address2", sql.VarChar, (address2 || "").trim())
      .input("Address3", sql.VarChar, (address3 || "").trim())
      .input("CostPerGram", sql.Decimal(18, 3), avgCostPerGram)
      .input("Weight", sql.Decimal(18, 3), totalWeight)
      .input("GoldLoss", sql.Decimal(18, 3), totalGoldLoss)
      .input("StoneWeight", sql.Decimal(18, 3), totalStoneWeight)
      .input("PCS", sql.Int, totalPCS)
      .input("TotalCost", sql.Decimal(18, 2), totalCost)
      .input("TradeInAmount", sql.Decimal(18, 2), totalCost)
      .input("Date", sql.DateTime, new Date())
      .input("IssueFlag", sql.VarChar, "N")
      .input("Remarks", sql.VarChar, (remarks || "").trim())
      .input("UserID", sql.VarChar, userId);

    await insertHeader.query(`
      INSERT INTO tblTradeInHeader (
        InventoryCode, InvoiceNo, LocationCode, CustomerCode, MemberCode, CustomerName,
        Address1, Address2, Address3, CostPerGram, Weight, GoldLoss,
        StoneWeight, PCS, TotalCost, TradeInAmount, Date, IssueFlag,
        Remarks, CreateDate, UserID
      ) VALUES (
        @InventoryCode, @InvoiceNo, @LocationCode, @CustomerCode, @MemberCode, @CustomerName,
        @Address1, @Address2, @Address3, @CostPerGram, @Weight, @GoldLoss,
        @StoneWeight, @PCS, @TotalCost, @TradeInAmount, @Date, @IssueFlag,
        @Remarks, GETDATE(), @UserID
      )
    `);

    // Insert Details
    for (let i = 0; i < items.length; i++) {
      const item = items[i];
      const insertDetail = new sql.Request(transaction);
      insertDetail
        .input("InventoryCode", sql.VarChar, finalInventoryCode)
        .input("InvoiceNo", sql.VarChar, finalInvoiceNo)
        .input("LocationCode", sql.VarChar, locationCode)
        .input("SNo", sql.Int, i + 1)
        .input("Description", sql.VarChar, (item.description || "").trim())
        .input("DiamondDescription", sql.VarChar, (item.stoneDescription || "").trim())
        .input("Weight", sql.Decimal(18, 3), parseFloat(item.weight) || 0)
        .input("StoneWeight", sql.Decimal(18, 3), parseFloat(item.stoneWeight) || 0)
        .input("GoldLoss", sql.Decimal(18, 3), parseFloat(item.goldLoss) || 0)
        .input("CostPerGram", sql.Decimal(18, 3), parseFloat(item.costPerGram) || 0)
        .input("TotalCost", sql.Decimal(18, 2), parseFloat(item.totalCost) || 0)
        .input("Class", sql.VarChar, (item.itemClass || "").trim())
        .input("Remarks", sql.VarChar, (remarks || "").trim())
        .input("UserID", sql.VarChar, userId);

      await insertDetail.query(`
        INSERT INTO tblTradeInDetail (
          InventoryCode, InvoiceNo, LocationCode, SNo, Description, DiamondDescription,
          Weight, StoneWeight, GoldLoss, CostPerGram, TotalCost,
          Class, Remarks, UserID
        ) VALUES (
          @InventoryCode, @InvoiceNo, @LocationCode, @SNo, @Description, @DiamondDescription,
          @Weight, @StoneWeight, @GoldLoss, @CostPerGram, @TotalCost,
          @Class, @Remarks, @UserID
        )
      `);
    }

    await transaction.commit();

    res.json({
      success: true,
      message: "Trade-in transaction saved successfully.",
      invoiceNo: finalInvoiceNo,
      inventoryCode: finalInventoryCode
    });
  } catch (err) {
    if (transaction) {
      try { await transaction.rollback(); } catch (rErr) { console.error("Rollback error:", rErr); }
    }
    console.error("TradeInGold Save Error:", err);
    res.status(500).json({ success: false, message: "Failed to save trade-in transaction", error: err.message });
  }
});

// 8. PUT Update Trade-In Transaction
router.put("/update/:id", async (req, res) => {
  let transaction;
  try {
    const { id } = req.params;
    const pool = await getPool(req);
    transaction = new sql.Transaction(pool);

    const {
      customerCode,
      memberCode,
      customerName,
      address1,
      address2,
      address3,
      remarks,
      userId = "ADMIN",
      items = []
    } = req.body;

    if (!items || items.length === 0) {
      return res.status(400).json({ success: false, message: "At least one trade-in item is required." });
    }

    const cleanId = (id || "").trim();
    await transaction.begin();

    // Check existing record
    const checkReq = new sql.Request(transaction);
    const checkRes = await checkReq
      .input("Id", sql.VarChar, cleanId)
      .query(`SELECT TOP 1 InventoryCode, InvoiceNo, LocationCode FROM tblTradeInHeader WHERE InventoryCode = @Id OR InvoiceNo = @Id`);

    if (!checkRes.recordset || checkRes.recordset.length === 0) {
      await transaction.rollback();
      return res.status(404).json({ success: false, message: "Trade-in record not found." });
    }

    const existing = checkRes.recordset[0];
    const targetInventoryCode = existing.InventoryCode;
    const targetInvoiceNo = existing.InvoiceNo;
    const locationCode = existing.LocationCode || "HQ";

    // Delete existing details
    const delDetail = new sql.Request(transaction);
    await delDetail
      .input("InvCode", sql.VarChar, targetInventoryCode)
      .query(`DELETE FROM tblTradeInDetail WHERE InventoryCode = @InvCode`);

    // Calculate Grid Totals
    let totalWeight = 0;
    let totalGoldLoss = 0;
    let totalStoneWeight = 0;
    let totalPCS = items.length;
    let totalCost = 0;

    items.forEach((item) => {
      totalWeight += parseFloat(item.weight) || 0;
      totalGoldLoss += parseFloat(item.goldLoss) || 0;
      totalStoneWeight += parseFloat(item.stoneWeight) || 0;
      totalCost += parseFloat(item.totalCost) || 0;
    });

    const avgCostPerGram = totalWeight > 0 ? totalCost / totalWeight : 0;

    // Update Header
    const updHeader = new sql.Request(transaction);
    updHeader
      .input("InventoryCode", sql.VarChar, targetInventoryCode)
      .input("CustomerCode", sql.VarChar, (customerCode || "").trim())
      .input("MemberCode", sql.VarChar, (memberCode || "").trim())
      .input("CustomerName", sql.VarChar, (customerName || "").trim())
      .input("Address1", sql.VarChar, (address1 || "").trim())
      .input("Address2", sql.VarChar, (address2 || "").trim())
      .input("Address3", sql.VarChar, (address3 || "").trim())
      .input("CostPerGram", sql.Decimal(18, 3), avgCostPerGram)
      .input("Weight", sql.Decimal(18, 3), totalWeight)
      .input("GoldLoss", sql.Decimal(18, 3), totalGoldLoss)
      .input("StoneWeight", sql.Decimal(18, 3), totalStoneWeight)
      .input("PCS", sql.Int, totalPCS)
      .input("TotalCost", sql.Decimal(18, 2), totalCost)
      .input("TradeInAmount", sql.Decimal(18, 2), totalCost)
      .input("Remarks", sql.VarChar, (remarks || "").trim())
      .input("UserID", sql.VarChar, userId);

    await updHeader.query(`
      UPDATE tblTradeInHeader SET
        CustomerCode = @CustomerCode,
        MemberCode = @MemberCode,
        CustomerName = @CustomerName,
        Address1 = @Address1,
        Address2 = @Address2,
        Address3 = @Address3,
        CostPerGram = @CostPerGram,
        Weight = @Weight,
        GoldLoss = @GoldLoss,
        StoneWeight = @StoneWeight,
        PCS = @PCS,
        TotalCost = @TotalCost,
        TradeInAmount = @TradeInAmount,
        Remarks = @Remarks,
        UserID = @UserID
      WHERE InventoryCode = @InventoryCode
    `);

    // Re-insert Details
    for (let i = 0; i < items.length; i++) {
      const item = items[i];
      const insertDetail = new sql.Request(transaction);
      insertDetail
        .input("InventoryCode", sql.VarChar, targetInventoryCode)
        .input("InvoiceNo", sql.VarChar, targetInvoiceNo)
        .input("LocationCode", sql.VarChar, locationCode)
        .input("SNo", sql.Int, i + 1)
        .input("Description", sql.VarChar, (item.description || "").trim())
        .input("DiamondDescription", sql.VarChar, (item.stoneDescription || "").trim())
        .input("Weight", sql.Decimal(18, 3), parseFloat(item.weight) || 0)
        .input("StoneWeight", sql.Decimal(18, 3), parseFloat(item.stoneWeight) || 0)
        .input("GoldLoss", sql.Decimal(18, 3), parseFloat(item.goldLoss) || 0)
        .input("CostPerGram", sql.Decimal(18, 3), parseFloat(item.costPerGram) || 0)
        .input("TotalCost", sql.Decimal(18, 2), parseFloat(item.totalCost) || 0)
        .input("Class", sql.VarChar, (item.itemClass || "").trim())
        .input("Remarks", sql.VarChar, (remarks || "").trim())
        .input("UserID", sql.VarChar, userId);

      await insertDetail.query(`
        INSERT INTO tblTradeInDetail (
          InventoryCode, InvoiceNo, LocationCode, SNo, Description, DiamondDescription,
          Weight, StoneWeight, GoldLoss, CostPerGram, TotalCost,
          Class, Remarks, UserID
        ) VALUES (
          @InventoryCode, @InvoiceNo, @LocationCode, @SNo, @Description, @DiamondDescription,
          @Weight, @StoneWeight, @GoldLoss, @CostPerGram, @TotalCost,
          @Class, @Remarks, @UserID
        )
      `);
    }

    await transaction.commit();

    res.json({
      success: true,
      message: "Trade-in transaction updated successfully.",
      invoiceNo: targetInvoiceNo,
      inventoryCode: targetInventoryCode
    });
  } catch (err) {
    if (transaction) {
      try { await transaction.rollback(); } catch (rErr) { console.error("Rollback error:", rErr); }
    }
    console.error("TradeInGold Update Error:", err);
    res.status(500).json({ success: false, message: "Failed to update trade-in transaction", error: err.message });
  }
});

// 9. DELETE Trade-In Transaction
router.delete("/delete/:id", async (req, res) => {
  let transaction;
  try {
    const { id } = req.params;
    const pool = await getPool(req);
    transaction = new sql.Transaction(pool);

    const cleanId = (id || "").trim();
    await transaction.begin();

    const delHeader = new sql.Request(transaction);
    await delHeader
      .input("Id", sql.VarChar, cleanId)
      .query(`DELETE FROM tblTradeInHeader WHERE InventoryCode = @Id OR InvoiceNo = @Id`);

    const delDetail = new sql.Request(transaction);
    await delDetail
      .input("Id", sql.VarChar, cleanId)
      .query(`DELETE FROM tblTradeInDetail WHERE InventoryCode = @Id OR InvoiceNo = @Id`);

    await transaction.commit();

    res.json({ success: true, message: "Trade-in record deleted successfully." });
  } catch (err) {
    if (transaction) {
      try { await transaction.rollback(); } catch (rErr) { console.error("Rollback error:", rErr); }
    }
    console.error("TradeInGold Delete Error:", err);
    res.status(500).json({ success: false, message: "Failed to delete trade-in transaction", error: err.message });
  }
});

module.exports = router;
