const express = require("express");
const router = express.Router();
const { sql } = require("../db");

// 1. GET Next Reference Number and Company info
router.get("/next-ref", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query(
      "SELECT TOP 1 LocationCode, CompanyShortCode, NextTradeInSalesNo, NextPVNo, TradeInLimit FROM tblCompany"
    );
    if (result.recordset.length === 0) {
      return res.json({ 
        success: true, 
        refNo: "00000001", 
        inventoryCode: "TI-00000001", 
        locationCode: "HQ", 
        companyShortCode: "",
        tradeInLimit: 0
      });
    }
    const company = result.recordset[0];
    const locationCode = (company.LocationCode || "HQ").trim();
    // Prioritize NextTradeInSalesNo, fallback to NextPVNo if present
    const nextSeq = parseInt(company.NextTradeInSalesNo || company.NextPVNo) || 1;
    const refNo = String(nextSeq).padStart(8, "0");
    const inventoryCode = "TI-" + refNo;
    res.json({
      success: true,
      refNo,
      inventoryCode,
      locationCode,
      companyShortCode: (company.CompanyShortCode || "").trim(),
      tradeInLimit: parseFloat(company.TradeInLimit || 0)
    });
  } catch (err) {
    console.error("Get Next Ref Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// 2. GET all trade-ins for LOV lookup
router.get("/list", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query(`
      SELECT 
        ReferenceNo, 
        LocationCode, 
        VendorCode, 
        VendorName, 
        TotalCost, 
        SalesDate, 
        Remarks 
      FROM tblTradeInSalesHeader 
      ORDER BY ReferenceNo DESC
    `);
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Trade-In List Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// 2b. GET all members for lookup
router.get("/members", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query(
      "SELECT MemberCode, MemberName, Address1, Address2, Address3, MemberType FROM tblMember ORDER BY MemberCode ASC"
    );
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    try {
      const pool = req.pool;
      const result = await pool.request().query(
        "SELECT CustomerCode AS MemberCode, CustomerName AS MemberName, Address1, Address2, Address3, CustomerType AS MemberType FROM tblCustomer ORDER BY CustomerCode ASC"
      );
      res.json({ success: true, data: result.recordset });
    } catch (innerErr) {
      res.status(500).json({ success: false, message: "Server error", error: err.message });
    }
  }
});

// 2c. GET single customer by Code (subGetCustomer)
router.get("/customer/:code", async (req, res) => {
  try {
    const { code } = req.params;
    const pool = req.pool;
    const result = await pool.request()
      .input("CustomerCode", sql.VarChar, (code || "").trim())
      .query(`
        SELECT TOP 1 CustomerCode, CustomerName, Address1, Address2, Address3, CustomerType 
        FROM tblCustomer 
        WHERE CustomerCode = @CustomerCode
      `);
    if (result.recordset.length > 0) {
      res.json({ success: true, data: result.recordset[0] });
    } else {
      res.json({ success: false, message: "Customer not found" });
    }
  } catch (err) {
    console.error("Fetch Customer Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// 2d. GET single member by Code (SubGetMemberCode)
router.get("/member/:code", async (req, res) => {
  try {
    const { code } = req.params;
    const pool = req.pool;
    let result = await pool.request()
      .input("MemberCode", sql.VarChar, (code || "").trim())
      .query(`
        SELECT TOP 1 MemberCode, MemberName, Address1, Address2, Address3, MemberType 
        FROM tblMember 
        WHERE MemberCode = @MemberCode
      `);
    if (result.recordset.length === 0) {
      result = await pool.request()
        .input("CustomerCode", sql.VarChar, (code || "").trim())
        .query(`
          SELECT TOP 1 CustomerCode AS MemberCode, CustomerName AS MemberName, Address1, Address2, Address3, CustomerType AS MemberType 
          FROM tblCustomer 
          WHERE CustomerCode = @CustomerCode
        `);
    }
    if (result.recordset.length > 0) {
      res.json({ success: true, data: result.recordset[0] });
    } else {
      res.json({ success: false, message: "Member not found" });
    }
  } catch (err) {
    console.error("Fetch Member Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// 3. GET single trade-in transaction by ReferenceNo (inventoryCode)
router.get("/transaction/:refNo", async (req, res) => {
  try {
    const { refNo } = req.params;
    const pool = req.pool;
    
    // Fetch header
    const headerResult = await pool.request()
      .input("ReferenceNo", sql.VarChar, refNo.trim())
      .query("SELECT * FROM tblTradeInSalesHeader WHERE ReferenceNo = @ReferenceNo");
      
    if (headerResult.recordset.length === 0) {
      return res.status(404).json({ success: false, message: "Transaction not found." });
    }
    
    // Fetch details
    const detailsResult = await pool.request()
      .input("ReferenceNo", sql.VarChar, refNo.trim())
      .query("SELECT * FROM tblTradeinSalesDetail WHERE ReferenceNo = @ReferenceNo ORDER BY InventoryCode ASC");
      
    res.json({
      success: true,
      header: headerResult.recordset[0],
      details: detailsResult.recordset
    });
  } catch (err) {
    console.error("Fetch Transaction Details Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// 4. GET gold rates from tblClass
router.get("/class-rates", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query("SELECT ClassCode, Rate FROM tblClass");
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Class Rates Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// 5. POST Save (Insert / Update) Trade-In Sales Transaction
router.post("/save", async (req, res) => {
  const pool = req.pool;
  const transaction = new sql.Transaction(pool);
  
  try {
    const {
      refNo, // e.g. "00000001"
      inventoryCode, // e.g. "TI-00000001"
      customerCode,
      customerName,
      address1,
      address2,
      address3,
      costPerGram,
      remarks,
      salesmanCode,
      salesAmount,
      tradeInAmount,
      balanceAmount,
      items, // array of items
      userId,
      isEdit // flag
    } = req.body;

    if (!inventoryCode || !customerCode || !items || items.length === 0) {
      return res.status(400).json({ success: false, message: "Required parameters missing." });
    }

    await transaction.begin();
    const reqDeleteHeader = new sql.Request(transaction);
    const reqDeleteDetail = new sql.Request(transaction);

    // Step A: Delete existing records if update / replacement
    await reqDeleteHeader
      .input("ReferenceNo", sql.VarChar, inventoryCode.trim())
      .query("DELETE FROM tblTradeInSalesHeader WHERE ReferenceNo = @ReferenceNo");
      
    await reqDeleteDetail
      .input("ReferenceNo", sql.VarChar, inventoryCode.trim())
      .query("DELETE FROM tblTradeinSalesDetail WHERE ReferenceNo = @ReferenceNo");

    // Also delete any previous PayMode detail records for this invoice reference
    const reqDeletePayMode = new sql.Request(transaction);
    await reqDeletePayMode
      .input("InvoiceNo", sql.VarChar, inventoryCode.trim())
      .query("DELETE FROM tblPayModeDetail WHERE InvoiceNo = @InvoiceNo");

    // Calculate aggregate sums from detail lines
    let totalWeight = 0;
    let totalStoneWeight = 0;
    let totalMC = 0;
    let totalPCS = 0;
    let totalGoldLoss = 0;
    let totalCostVal = 0;

    items.forEach(item => {
      totalWeight += parseFloat(item.Weight) || 0;
      totalStoneWeight += parseFloat(item.StoneWeight) || 0;
      totalMC += parseFloat(item.MC) || 0;
      totalPCS += parseInt(item.PCS) || 1;
      totalGoldLoss += parseFloat(item.GoldLoss) || 0;
      totalCostVal += parseFloat(item.TotalCost) || 0;
    });

    // Step B: Insert into tblTradeInSalesHeader
    const reqHeader = new sql.Request(transaction);
    await reqHeader
      .input("ReferenceNo", sql.VarChar, inventoryCode.trim())
      .input("LocationCode", sql.VarChar, "HQ")
      .input("VendorCode", sql.VarChar, customerCode.trim())
      .input("VendorName", sql.VarChar, (customerName || "").trim())
      .input("Address1", sql.VarChar, (address1 || "").trim())
      .input("Address2", sql.VarChar, (address2 || "").trim())
      .input("Address3", sql.VarChar, (address3 || "").trim())
      .input("CostPerGram", sql.Numeric(18, 4), parseFloat(costPerGram) || 0)
      .input("TotalWeight", sql.Numeric(18, 4), totalWeight)
      .input("TotalStoneWeight", sql.Numeric(18, 4), totalStoneWeight)
      .input("TotalMC", sql.Numeric(18, 4), totalMC)
      .input("TotalPCS", sql.Numeric(18, 4), totalPCS)
      .input("TotalGoldLoss", sql.Numeric(18, 4), totalGoldLoss)
      .input("TotalCost", sql.Numeric(18, 4), totalCostVal)
      .input("Amount", sql.Numeric(18, 4), parseFloat(salesAmount) || 0)
      .input("Remarks", sql.VarChar, (remarks || "").trim())
      .input("SalesmanCode", sql.VarChar, (salesmanCode || "").trim())
      .input("CreateUser", sql.VarChar, (userId || "sys").trim())
      .query(`
        INSERT INTO tblTradeInSalesHeader (
          ReferenceNo, LocationCode, VendorCode, VendorName, Address1, Address2, Address3,
          CostPerGram, TotalWeight, TotalStoneWeight, TotalMC, TotalPCS, TotalGoldLoss, TotalCost,
          Amount, Remarks, SalesmanCode, SalesDate, CreateUser, CreateDate, ModifyUser, ModifyDate
        ) VALUES (
          @ReferenceNo, @LocationCode, @VendorCode, @VendorName, @Address1, @Address2, @Address3,
          @CostPerGram, @TotalWeight, @TotalStoneWeight, @TotalMC, @TotalPCS, @TotalGoldLoss, @TotalCost,
          @Amount, @Remarks, @SalesmanCode, GETDATE(), @CreateUser, GETDATE(), @CreateUser, GETDATE()
        )
      `);

    // Step C: Insert into tblTradeinSalesDetail
    for (let i = 0; i < items.length; i++) {
      const item = items[i];
      const reqDetail = new sql.Request(transaction);
      
      await reqDetail
        .input("ReferenceNo", sql.VarChar, inventoryCode.trim())
        .input("InventoryCode", sql.VarChar, String(i + 1))
        .input("Description", sql.VarChar, (item.Description || "").trim())
        .input("DiamondDescription", sql.VarChar, (item.DiamondDescription || "").trim())
        .input("Weight", sql.Numeric(18, 4), parseFloat(item.Weight) || 0)
        .input("StoneWeight", sql.Numeric(18, 4), parseFloat(item.StoneWeight) || 0)
        .input("CostPerGram", sql.Numeric(18, 4), parseFloat(item.CostPerGram) || 0)
        .input("MC", sql.Numeric(18, 4), parseFloat(item.MC) || 0)
        .input("PCS", sql.Numeric(18, 4), parseInt(item.PCS) || 1)
        .input("GoldLoss", sql.Numeric(18, 4), parseFloat(item.GoldLoss) || 0)
        .input("TotalCost", sql.Numeric(18, 4), parseFloat(item.TotalCost) || 0)
        .input("CategoryCode", sql.VarChar, "TRADEIN")
        .input("ClassCode", sql.VarChar, (item.ClassCode || "").trim())
        .input("CatalogueCode", sql.VarChar, "TRADEIN")
        .input("Remarks", sql.VarChar, "TRADEIN")
        .input("CreateUser", sql.VarChar, (userId || "sys").trim())
        .query(`
          INSERT INTO tblTradeinSalesDetail (
            ReferenceNo, InventoryCode, Description, DiamondDescription, Weight, StoneWeight,
            CostPerGram, MC, PCS, GoldLoss, TotalCost, CategoryCode, ClassCode, CatalogueCode,
            Remarks, CreateUser, CreateDate, ModifyUser, ModifyDate
          ) VALUES (
            @ReferenceNo, @InventoryCode, @Description, @DiamondDescription, @Weight, @StoneWeight,
            @CostPerGram, @MC, @PCS, @GoldLoss, @TotalCost, @CategoryCode, @ClassCode, @CatalogueCode,
            @Remarks, @CreateUser, GETDATE(), @CreateUser, GETDATE()
          )
        `);
    }

    // Step D: Insert into tblPayModeDetail (CASH balance save as per VB fncPaymodeDetailSave)
    const balVal = parseFloat(balanceAmount) || 0;
    if (balVal !== 0) {
      try {
        const reqPayMode = new sql.Request(transaction);
        await reqPayMode
          .input("InvoiceNo", sql.VarChar, inventoryCode.trim())
          .input("InvoiceDate", sql.DateTime, new Date())
          .input("PayMode", sql.VarChar, "CASH")
          .input("Amount", sql.Numeric(18, 4), balVal)
          .input("Commision", sql.Numeric(18, 4), 0)
          .input("SlipNo", sql.VarChar, "")
          .input("CreateUser", sql.VarChar, (userId || "sys").trim())
          .input("LocationCode", sql.VarChar, "HQ")
          .input("SalesType", sql.VarChar, "TradeIn")
          .query(`
            INSERT INTO tblPayModeDetail (
              InvoiceNo, InvoiceDate, PayMode, Amount, Commision, SlipNo,
              CreateUser, CreateDate, ModifyUser, ModifyDate, LocationCode, SalesType
            ) VALUES (
              @InvoiceNo, @InvoiceDate, @PayMode, @Amount, @Commision, @SlipNo,
              @CreateUser, GETDATE(), @CreateUser, GETDATE(), @LocationCode, @SalesType
            )
          `);
      } catch (pmErr) {
        console.warn("PayMode detail insert warning (skipped if not applicable):", pmErr.message);
      }
    }

    // Step E: Update NextTradeInSalesNo and/or NextPVNo in tblCompany if it's a NEW insertion
    if (!isEdit) {
      const reqCompany = new sql.Request(transaction);
      await reqCompany.query(`
        UPDATE tblCompany 
        SET NextTradeInSalesNo = ISNULL(NextTradeInSalesNo, 0) + 1,
            NextPVNo = CASE WHEN NextPVNo IS NOT NULL THEN NextPVNo + 1 ELSE NextPVNo END
      `);
    }

    await transaction.commit();
    res.json({ success: true, message: "Trade In Sales transaction saved successfully.", invoiceNo: inventoryCode });
  } catch (err) {
    console.error("Save Trade-In Sales Error:", err);
    try {
      await transaction.rollback();
    } catch (rerr) {
      console.error("Rollback failed:", rerr);
    }
    res.status(500).json({ success: false, message: "Database Save Error", error: err.message });
  }
});

// 6. DELETE Trade-In Sales Transaction
router.delete("/delete/:refNo", async (req, res) => {
  const { refNo } = req.params;
  const pool = req.pool;
  const transaction = new sql.Transaction(pool);

  try {
    await transaction.begin();
    
    const reqDetail = new sql.Request(transaction);
    await reqDetail
      .input("ReferenceNo", sql.VarChar, refNo.trim())
      .query("DELETE FROM tblTradeinSalesDetail WHERE ReferenceNo = @ReferenceNo");

    const reqPayMode = new sql.Request(transaction);
    await reqPayMode
      .input("InvoiceNo", sql.VarChar, refNo.trim())
      .query("DELETE FROM tblPayModeDetail WHERE InvoiceNo = @InvoiceNo");

    const reqHeader = new sql.Request(transaction);
    await reqHeader
      .input("ReferenceNo", sql.VarChar, refNo.trim())
      .query("DELETE FROM tblTradeInSalesHeader WHERE ReferenceNo = @ReferenceNo");

    await transaction.commit();
    res.json({ success: true, message: "Trade In Sales transaction deleted successfully." });
  } catch (err) {
    console.error("Delete Transaction Error:", err);
    try {
      await transaction.rollback();
    } catch (rerr) {
      console.error("Rollback error:", rerr);
    }
    res.status(500).json({ success: false, message: "Database Delete Error", error: err.message });
  }
});

// 7. POST Verify SalesMan Password
// Checks tblSalesMan for Password and returns Salesman details (SalesManCode, SalesManName, TradeInNo, LastTradeInAmount)
router.post("/verifypassword", async (req, res) => {
  try {
    const { password } = req.body;
    if (!password) return res.status(400).json({ success: false, message: "Password required" });
    const pool = req.pool;

    let result;
    try {
      result = await pool.request()
        .input("Password", sql.VarChar, password.trim())
        .query(`
          SELECT TOP 1 
            SalesManCode, 
            SalesManName, 
            ISNULL(TradeInNo, '') AS TradeInNo, 
            ISNULL(LastTradeInAmount, 0) AS LastTradeInAmount,
            ISNULL(SalesManGroup, '') AS SalesManGroup
          FROM tblSalesMan 
          WHERE Password = @Password
        `);
    } catch (colErr) {
      // Fallback if TradeInNo columns do not exist on tblSalesman
      result = await pool.request()
        .input("Password", sql.VarChar, password.trim())
        .query(`
          SELECT TOP 1 
            SalesManCode, 
            SalesManName, 
            '' AS TradeInNo, 
            0 AS LastTradeInAmount,
            ISNULL(SalesManGroup, '') AS SalesManGroup
          FROM tblSalesMan 
          WHERE Password = @Password
        `);
    }

    if (result.recordset.length > 0) {
      res.json({ success: true, salesman: result.recordset[0] });
    } else {
      res.json({ success: false, message: "SalesMan Password is Incorrect!" });
    }
  } catch (err) {
    console.error("Verify Salesman Password Error:", err);
    res.status(500).json({ success: false, message: "Database Error", error: err.message });
  }
});

// 8. POST Check Admin Password (for changing cost over market rate)
router.post("/check-admin", async (req, res) => {
  try {
    const { password } = req.body;
    const pool = req.pool;
    
    const result = await pool.request()
      .input("Password", sql.VarChar, (password || "").trim())
      .query(`
        SELECT TOP 1 SalesManGroup 
        FROM tblSalesMan 
        WHERE Password = @Password AND UPPER(LTRIM(RTRIM(SalesManGroup))) = 'SUPER ADMIN'
      `);
      
    if (result.recordset.length > 0) {
      res.json({ success: true, message: "Authorized" });
    } else {
      res.json({ success: false, message: "SalesMan Has No Rights to change Cost!" });
    }
  } catch (err) {
    console.error("Check Admin PWD Error:", err);
    res.status(500).json({ success: false, message: "Database Error", error: err.message });
  }
});

module.exports = router;
