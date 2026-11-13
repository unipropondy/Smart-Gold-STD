/** Developer: durairaj */
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
router.get("/next-ref", async (req, res) => {
  try {
    const pool = await getPool(req);
    const result = await pool.request().query(
      "SELECT TOP 1 LocationCode, CompanyShortCode, NextTradeInSalesNo, NextPVNo, TradeInLimit FROM tblCompany"
    );
    if (!result.recordset || result.recordset.length === 0) {
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
    const pool = await getPool(req);
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
    res.json({ success: true, data: result.recordset || [] });
  } catch (err) {
    console.error("Fetch Trade-In List Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// 2b. GET all members for lookup
router.get("/members", async (req, res) => {
  try {
    const pool = await getPool(req);
    const result = await pool.request().query(
      "SELECT MemberCode, MemberName, Address1, Address2, Address3, MemberType FROM tblMember ORDER BY MemberCode ASC"
    );
    res.json({ success: true, data: result.recordset || [] });
  } catch (err) {
    try {
      const pool = await getPool(req);
      const result = await pool.request().query(
        "SELECT CustomerCode AS MemberCode, CustomerName AS MemberName, Address1, Address2, Address3, CustomerType AS MemberType FROM tblCustomer ORDER BY CustomerCode ASC"
      );
      res.json({ success: true, data: result.recordset || [] });
    } catch (innerErr) {
      res.status(500).json({ success: false, message: "Server error", error: err.message });
    }
  }
});

// 2c. GET single customer by Code (subGetCustomer)
router.get("/customer/:code", async (req, res) => {
  try {
    const { code } = req.params;
    const pool = await getPool(req);
    const result = await pool.request()
      .input("CustomerCode", sql.VarChar, (code || "").trim())
      .query(`
        SELECT TOP 1 CustomerCode, CustomerName, Address1, Address2, Address3, CustomerType 
        FROM tblCustomer 
        WHERE CustomerCode = @CustomerCode
      `);
    if (result.recordset && result.recordset.length > 0) {
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
    const pool = await getPool(req);
    let result = await pool.request()
      .input("MemberCode", sql.VarChar, (code || "").trim())
      .query(`
        SELECT TOP 1 MemberCode, MemberName, Address1, Address2, Address3, MemberType 
        FROM tblMember 
        WHERE MemberCode = @MemberCode
      `);
    if (!result.recordset || result.recordset.length === 0) {
      result = await pool.request()
        .input("CustomerCode", sql.VarChar, (code || "").trim())
        .query(`
          SELECT TOP 1 CustomerCode AS MemberCode, CustomerName AS MemberName, Address1, Address2, Address3, CustomerType AS MemberType 
          FROM tblCustomer 
          WHERE CustomerCode = @CustomerCode
        `);
    }
    if (result.recordset && result.recordset.length > 0) {
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
    const pool = await getPool(req);
    
    // Fetch header
    const headerResult = await pool.request()
      .input("ReferenceNo", sql.VarChar, (refNo || "").trim())
      .query("SELECT * FROM tblTradeInSalesHeader WHERE ReferenceNo = @ReferenceNo");
      
    if (!headerResult.recordset || headerResult.recordset.length === 0) {
      return res.status(404).json({ success: false, message: "Transaction not found." });
    }
    
    // Fetch details
    const detailsResult = await pool.request()
      .input("ReferenceNo", sql.VarChar, (refNo || "").trim())
      .query("SELECT * FROM tblTradeinSalesDetail WHERE ReferenceNo = @ReferenceNo ORDER BY InventoryCode ASC");
      
    res.json({
      success: true,
      header: headerResult.recordset[0],
      details: detailsResult.recordset || []
    });
  } catch (err) {
    console.error("Fetch Transaction Details Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// 4. GET gold rates from tblClass
router.get("/class-rates", async (req, res) => {
  try {
    const pool = await getPool(req);
    const result = await pool.request().query("SELECT ClassCode, Rate FROM tblClass");
    res.json({ success: true, data: result.recordset || [] });
  } catch (err) {
    console.error("Fetch Class Rates Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// 4b. GET /api/tradeinsales/print-data?billNo=...
// Crystal Report: Reports/rptTradeInGoldInvoice_MJ.rpt (or _ESP / _SSJ / _RJ / _GMT based on CompanyShortCode)
// Selection Formula: {tblTradeInHeader.InvoiceNo} = sInvoiceNo or {tblTradeInSalesHeader.ReferenceNo} = sInvoiceNo
// Formula Fields: CompAddress1, CompAddress2, CompAddress3, Customer1, Customer2, Customer3, Rate1, Rate2, Rate3
router.get("/print-data", async (req, res) => {
  try {
    const rawBillNo = (req.query.billNo || req.query.invoiceNo || req.query.refNo || "").trim();
    if (!rawBillNo) {
      return res.status(400).json({ success: false, message: "Bill No is required for print." });
    }

    const pool = await getPool(req);

    // 1. Fetch Company Information matching clsCompany.getCompanyName
    const compRes = await pool.request().query(`
      SELECT TOP 1 Name, Address1, Address2, City, State, Country, Phone, Fax, CompanyShortCode, CurrencySign
      FROM tblCompany
    `);
    const compRow = compRes.recordset[0] || {};
    const company = {
      name: (compRow.Name || "SMART").trim(),
      address1: (compRow.Address1 || "").trim(),
      address2: (compRow.Address2 || "").trim(),
      city: (compRow.City || "").trim(),
      state: (compRow.State || "").trim(),
      country: (compRow.Country || "").trim(),
      phone: (compRow.Phone || "").trim(),
      fax: (compRow.Fax || "").trim(),
      companyShortCode: (compRow.CompanyShortCode || "MJ").trim().toUpperCase(),
      currencySign: (compRow.CurrencySign || "SGD").trim()
    };

    // 2. Fetch Daily Class Rates matching subGetClassRate() in frmTradeInSales:
    // 24K -> s999, 22K -> s916, 20KJ/835 -> s835
    let rate999 = "-";
    let rate916 = "-";
    let rate835 = "-";
    try {
      const rateRes = await pool.request().query("SELECT ClassCode, Rate FROM tblClass");
      if (rateRes.recordset && rateRes.recordset.length > 0) {
        rateRes.recordset.forEach(r => {
          const code = (r.ClassCode || "").trim().toUpperCase();
          const val = parseFloat(r.Rate || 0).toFixed(2);
          if (code === "24K") rate999 = val;
          else if (code === "22K") rate916 = val;
          else if (code === "20KJ" || code === "835") rate835 = val;
        });
      }
    } catch (rErr) {
      console.warn("Class rates fetch warning:", rErr.message);
    }

    // 3. Query Trade-In Record: Check tblTradeInSalesHeader / tblTradeInHeader
    // Normalize variations: "00000012", "TI-00000012"
    let cleanRef = rawBillNo;
    let cleanInv = rawBillNo;
    if (rawBillNo.toUpperCase().startsWith("TI-")) {
      cleanRef = rawBillNo.substring(3).trim();
    } else {
      cleanInv = "TI-" + rawBillNo;
    }

    // Attempt A: Check tblTradeInSalesHeader & tblTradeinSalesDetail
    const qSalesHdr = await pool.request()
      .input("Ref1", sql.VarChar, rawBillNo)
      .input("Ref2", sql.VarChar, cleanRef)
      .input("Ref3", sql.VarChar, cleanInv)
      .query(`
        SELECT TOP 1 *
        FROM tblTradeInSalesHeader
        WHERE RTRIM(LTRIM(ReferenceNo)) IN (@Ref1, @Ref2, @Ref3)
      `);

    if (qSalesHdr.recordset && qSalesHdr.recordset.length > 0) {
      const header = qSalesHdr.recordset[0];
      const actualRef = (header.ReferenceNo || rawBillNo).trim();

      const qSalesDtl = await pool.request()
        .input("Ref", sql.VarChar, actualRef)
        .query(`
          SELECT *
          FROM tblTradeinSalesDetail
          WHERE RTRIM(LTRIM(ReferenceNo)) = @Ref
          ORDER BY InventoryCode ASC
        `);

      return res.json({
        success: true,
        data: {
          company,
          rates: { rate999, rate916, rate835 },
          header: {
            InvoiceNo: actualRef,
            ReferenceNo: actualRef,
            CustomerCode: (header.VendorCode || "").trim(),
            CustomerName: (header.VendorName || "").trim(),
            Address1: (header.Address1 || "").trim(),
            Address2: (header.Address2 || "").trim(),
            Address3: (header.Address3 || "").trim(),
            CostPerGram: header.CostPerGram || 0,
            TotalWeight: header.TotalWeight || 0,
            TotalStoneWeight: header.TotalStoneWeight || 0,
            TotalGoldLoss: header.TotalGoldLoss || 0,
            TotalCost: header.TotalCost || 0,
            TotalAmount: header.Amount || 0,
            Remarks: (header.Remarks || "").trim(),
            SalesmanCode: (header.SalesmanCode || "").trim(),
            Date: header.SalesDate || header.CreateDate,
            CreateUser: (header.CreateUser || "").trim()
          },
          items: qSalesDtl.recordset || []
        }
      });
    }

    // Attempt B: Check tblTradeInHeader & tblTradeInDetail (original VB.NET tblTradeInHeader)
    const qTradeHdr = await pool.request()
      .input("Ref1", sql.VarChar, rawBillNo)
      .input("Ref2", sql.VarChar, cleanRef)
      .input("Ref3", sql.VarChar, cleanInv)
      .query(`
        SELECT TOP 1 *
        FROM tblTradeInHeader
        WHERE RTRIM(LTRIM(InvoiceNo)) IN (@Ref1, @Ref2, @Ref3)
           OR RTRIM(LTRIM(InventoryCode)) IN (@Ref1, @Ref2, @Ref3)
      `);

    if (qTradeHdr.recordset && qTradeHdr.recordset.length > 0) {
      const header = qTradeHdr.recordset[0];
      const actualInvNo = (header.InvoiceNo || rawBillNo).trim();

      const qTradeDtl = await pool.request()
        .input("InvNo", sql.VarChar, actualInvNo)
        .query(`
          SELECT *
          FROM tblTradeInDetail
          WHERE RTRIM(LTRIM(InvoiceNo)) = @InvNo
          ORDER BY ItemNo ASC
        `);

      return res.json({
        success: true,
        data: {
          company,
          rates: { rate999, rate916, rate835 },
          header: {
            InvoiceNo: actualInvNo,
            ReferenceNo: (header.InventoryCode || actualInvNo).trim(),
            CustomerCode: (header.CustomerCode || "").trim(),
            CustomerName: (header.CustomerName || "").trim(),
            Address1: (header.Address1 || "").trim(),
            Address2: (header.Address2 || "").trim(),
            Address3: (header.Address3 || "").trim(),
            CostPerGram: header.CostPerGram || 0,
            TotalWeight: header.Weight || 0,
            TotalStoneWeight: header.StoneWeight || 0,
            TotalGoldLoss: header.GoldLoss || 0,
            TotalCost: header.TotalCost || 0,
            TotalAmount: header.Amount || 0,
            Remarks: (header.Remarks || "").trim(),
            SalesmanCode: (header.SalesManCode || "").trim(),
            Date: header.Date || header.Createdate,
            CreateUser: (header.UserId || "").trim()
          },
          items: (qTradeDtl.recordset || []).map(d => ({
            InventoryCode: d.InventoryCode || d.ItemNo,
            Description: d.Description,
            DiamondDescription: d.DiamondDescription,
            Weight: d.Weight,
            StoneWeight: d.StoneWeight,
            CostPerGram: d.CostPerGram,
            MC: d.MC,
            PCS: d.PCS,
            GoldLoss: d.GoldLoss,
            TotalCost: d.TotalCost,
            Remarks: d.Remarks
          }))
        }
      });
    }

    return res.status(404).json({
      success: false,
      message: `BillNo is not Correct!... Trade In bill '${rawBillNo}' not found.`
    });
  } catch (err) {
    console.error("Print Data Trade-In Sales Error:", err);
    res.status(500).json({ success: false, message: "Database query error", error: err.message });
  }
});

// 5. POST Save (Insert / Update) Trade-In Sales Transaction
router.post("/save", async (req, res) => {
  let pool;
  let transaction;
  
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

    pool = await getPool(req);
    transaction = new sql.Transaction(pool);

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
    if (transaction) {
      try {
        await transaction.rollback();
      } catch (rerr) {
        console.error("Rollback failed:", rerr);
      }
    }
    res.status(500).json({ success: false, message: "Database Save Error", error: err.message });
  }
});

// 6. DELETE Trade-In Sales Transaction
router.delete("/delete/:refNo", async (req, res) => {
  const { refNo } = req.params;
  let pool;
  let transaction;

  try {
    pool = await getPool(req);
    transaction = new sql.Transaction(pool);
    await transaction.begin();
    
    const reqDetail = new sql.Request(transaction);
    await reqDetail
      .input("ReferenceNo", sql.VarChar, (refNo || "").trim())
      .query("DELETE FROM tblTradeinSalesDetail WHERE ReferenceNo = @ReferenceNo");

    const reqPayMode = new sql.Request(transaction);
    await reqPayMode
      .input("InvoiceNo", sql.VarChar, (refNo || "").trim())
      .query("DELETE FROM tblPayModeDetail WHERE InvoiceNo = @InvoiceNo");

    const reqHeader = new sql.Request(transaction);
    await reqHeader
      .input("ReferenceNo", sql.VarChar, (refNo || "").trim())
      .query("DELETE FROM tblTradeInSalesHeader WHERE ReferenceNo = @ReferenceNo");

    await transaction.commit();
    res.json({ success: true, message: "Trade In Sales transaction deleted successfully." });
  } catch (err) {
    console.error("Delete Transaction Error:", err);
    if (transaction) {
      try {
        await transaction.rollback();
      } catch (rerr) {
        console.error("Rollback error:", rerr);
      }
    }
    res.status(500).json({ success: false, message: "Database Delete Error", error: err.message });
  }
});

// 7. POST Verify SalesMan Password
router.post("/verifypassword", async (req, res) => {
  try {
    const { password } = req.body;
    if (!password) return res.status(400).json({ success: false, message: "Password required" });
    const pool = await getPool(req);

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

    if (result.recordset && result.recordset.length > 0) {
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
    const pool = await getPool(req);
    
    const result = await pool.request()
      .input("Password", sql.VarChar, (password || "").trim())
      .query(`
        SELECT TOP 1 SalesManGroup 
        FROM tblSalesMan 
        WHERE Password = @Password AND UPPER(LTRIM(RTRIM(SalesManGroup))) = 'SUPER ADMIN'
      `);
      
    if (result.recordset && result.recordset.length > 0) {
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
