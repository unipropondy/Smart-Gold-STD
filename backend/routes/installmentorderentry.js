const express = require("express");
const router = express.Router();
const { sql } = require("../db");

// GET Company Info (for GST and Next IDs)
router.get("/company", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query("SELECT TOP 1 * FROM tblCompany");
    if (result.recordset.length > 0) {
      res.json({ success: true, company: result.recordset[0] });
    } else {
      res.json({ success: false, message: "Company not found" });
    }
  } catch (err) {
    res.status(500).json({ success: false, message: err.message });
  }
});

// GET LOV Data
router.get("/lov", async (req, res) => {
  try {
    const { type, search, mode } = req.query;
    const pool = req.pool;
    let query = "";

    // Simple sanitization for search
    const safeSearch = search ? search.replace(/'/g, "''") : "";

    if (type === "customer") {
      query = `SELECT TOP 50 CustomerCode AS Code, CustomerName AS Name, NricNo, Address1, Address2, Address3 FROM tblCustomer ${safeSearch ? `WHERE CustomerCode LIKE '%${safeSearch}%' OR CustomerName LIKE '%${safeSearch}%'` : ""}`;
    } else if (type === "class") {
      query = `SELECT TOP 50 ClassCode AS Code, ClassName AS Name, Rate, Remarks FROM tblClass ${safeSearch ? `WHERE ClassCode LIKE '%${safeSearch}%' OR ClassName LIKE '%${safeSearch}%'` : ""}`;
    } else if (type === "category") {
      query = `SELECT TOP 50 CategoryCode AS Code, CategoryName AS Name, ClassCode, Remarks FROM tblCategory ${safeSearch ? `WHERE CategoryCode LIKE '%${safeSearch}%' OR CategoryName LIKE '%${safeSearch}%'` : ""}`;
    } else if (type === "catalogue") {
      query = `SELECT TOP 50 CatalogueCode AS Code, CatalogueName AS Name, CategoryCode, Remarks FROM tblCatalogue ${safeSearch ? `WHERE CatalogueCode LIKE '%${safeSearch}%' OR CatalogueName LIKE '%${safeSearch}%'` : ""}`;
    } else if (type === "supplier") {
      query = `SELECT TOP 50 SupplierCode AS Code, SupplierName AS Name, Address1, Address2 FROM tblsupplier ${safeSearch ? `WHERE SupplierCode LIKE '%${safeSearch}%' OR SupplierName LIKE '%${safeSearch}%'` : ""}`;
    } else if (type === "salesman") {
      query = `SELECT TOP 50 SalesManCode AS Code, SalesManName AS Name FROM tblSalesMan ${safeSearch ? `WHERE SalesManCode LIKE '%\$\{safeSearch\}%' OR SalesManName LIKE '%\$\{safeSearch\}%'` : ""}`;
    } else if (type === "size") {
      query = `SELECT TOP 50 SizeCode AS Code FROM tblSize ${safeSearch ? `WHERE SizeCode LIKE '%${safeSearch}%'` : ""}`;
    } else if (type === "invoice") {
      if (mode === "Installment") {
        query = `SELECT TOP 50 InsAgreementNo AS Code, LocationCode AS LocationC, CustomerCode, Name FROM tblInstallmentEntryHeader ${safeSearch ? `WHERE InsAgreementNo LIKE '%${safeSearch}%' OR Name LIKE '%${safeSearch}%'` : ""}`;
      } else {
        query = `SELECT TOP 50 OrderNo AS Code, OrderDate, CustomerCode, SupplierCode FROM tblInventoryOrderHeader ${safeSearch ? `WHERE OrderNo LIKE '%${safeSearch}%'` : ""}`;
      }
    } else if (type === "inventory") {
      query = `SELECT TOP 50 InventoryCode AS Code, Description AS Name FROM tblInventory ${safeSearch ? `WHERE InventoryCode LIKE '%${safeSearch}%' OR Description LIKE '%${safeSearch}%'` : ""}`;
    }

    if (!query) {
      return res.json({ success: false, message: "Invalid LOV type" });
    }

    const result = await pool.request().query(query);
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    res.status(500).json({ success: false, message: err.message });
  }
});

// Validate Salesman Password
router.post("/validate-salesman", async (req, res) => {
  try {
    const { password } = req.body;
    if (!password) {
      return res.status(400).json({ success: false, message: "Password is required" });
    }
    const pool = req.pool;
    const result = await pool.request()
      .input("Password", sql.VarChar, password)
      .query("SELECT SalesmanCode FROM tblSalesMan WHERE Password = @Password");

    if (result.recordset.length > 0) {
      res.json({ success: true, salesmanCode: result.recordset[0].SalesmanCode });
    } else {
      res.status(401).json({ success: false, message: "Invalid SalesMan Password" });
    }
  } catch (err) {
    console.error("Validate Salesman Error:", err);
    res.status(500).json({ success: false, message: "Error validating salesman", error: err.message });
  }
});

// GET Inventory Info
router.get("/inventory/:inventoryCode", async (req, res) => {
  try {
    const { inventoryCode } = req.params;
    const pool = req.pool;
    const result = await pool.request()
      .input("InventoryCode", sql.VarChar, inventoryCode.trim())
      .query(`
        SELECT InventoryCode, Description, Weight, GoldLoss, CostPerGram, TotalCost, 
               ClassCode, CategoryCode, CatalogueCode, SalesBillNo
        FROM tblInventory 
        WHERE InventoryCode = @InventoryCode
      `);

    if (result.recordset.length > 0) {
      const item = result.recordset[0];
      if (item.SalesBillNo && item.SalesBillNo.trim() !== '') {
        return res.json({ success: false, message: "Item is already Sold out." });
      }
      // Add a fallback so the frontend works smoothly without modifying it heavily
      item.AverageCost = item.CostPerGram || item.TotalCost;
      res.json({ success: true, item });
    } else {
      res.json({ success: false, message: "Inventory not found" });
    }
  } catch (err) {
    res.status(500).json({ success: false, message: err.message });
  }
});

// GET Existing Header
router.get("/header/:invoiceNo", async (req, res) => {
  try {
    const { invoiceNo } = req.params;
    const { type } = req.query; // 'Installment' or 'Order'
    const pool = req.pool;

    if (type === 'Installment') {
      const headerResult = await pool.request()
        .input("InvoiceNo", sql.VarChar, invoiceNo.trim())
        .query("SELECT * FROM tblInstallmentEntryHeader WHERE InsAgreementNo = @InvoiceNo");

      if (headerResult.recordset.length > 0) {
        const detailsResult = await pool.request()
          .input("InvoiceNo", sql.VarChar, invoiceNo.trim())
          .query("SELECT * FROM tblInstallmentEntryDetail WHERE InsAgreementNo = @InvoiceNo");

        return res.json({ success: true, header: headerResult.recordset[0], details: detailsResult.recordset });
      }
    } else {
      const headerResult = await pool.request()
        .input("InvoiceNo", sql.VarChar, invoiceNo.trim())
        .query(`
          SELECT h.*, 
                 c.CustomerName as Name, 
                 c.CustomerName,
                 c.Address1, 
                 c.Address2, 
                 c.Address3, 
                 c.CustomerType 
          FROM tblInventoryOrderHeader h
          LEFT JOIN tblCustomer c ON h.CustomerCode = c.CustomerCode
          WHERE h.OrderNo = @InvoiceNo
        `);

      if (headerResult.recordset.length > 0) {
        const detailsResult = await pool.request()
          .input("InvoiceNo", sql.VarChar, invoiceNo.trim())
          .query("SELECT * FROM tblInventoryOrderdetail WHERE OrderNo = @InvoiceNo");

        return res.json({ success: true, header: headerResult.recordset[0], details: detailsResult.recordset });
      }
    }

    res.json({ success: false, message: "Record not found" });
  } catch (err) {
    res.status(500).json({ success: false, message: err.message });
  }
});

// POST Save Installment or Order
router.post("/save", async (req, res) => {
  const transaction = new sql.Transaction(req.pool);
  try {
    const { mode, header, details } = req.body;
    await transaction.begin();

    if (mode === 'Installment') {
      // Handle Installment Save
      const request = transaction.request();

      // Basic insert into tblInstallmentEntryHeader
      await request
        .input("InsAgreementNo", sql.VarChar, header.invoiceNo || '')
        .input("Invdate", sql.DateTime, header.date || new Date())
        .input("GSTType", sql.VarChar, header.gstType || '')
        .input("LocationCode", sql.VarChar, header.locationCode || '01')
        .input("CustomerCode", sql.VarChar, header.customerCode || '')
        .input("CustomerType", sql.VarChar, header.customerType || '')
        .input("Name", sql.VarChar, header.customerName || '')
        .input("Address1", sql.VarChar, header.address1 || '')
        .input("Address2", sql.VarChar, header.address2 || '')
        .input("Address3", sql.VarChar, header.address3 || '')
        .input("SalesManCode", sql.VarChar, header.salesManCode || '')
        .input("DepositAmt", sql.Decimal(18, 2), header.depositAmt || 0)
        .input("TotalInstallment", sql.Int, header.totalInstallment || 0)
        .input("AmtPaid", sql.Decimal(18, 2), header.amountPaid || 0)
        .input("InstallmentAmt", sql.Decimal(18, 2), header.installmentAmt || 0)
        .input("ReturnAmount", sql.Decimal(18, 2), header.returnAmount || 0)
        .input("LastDate", sql.DateTime, header.lastDate || new Date())
        .input("Remarks", sql.VarChar, header.remarks || '')
        .input("TotalCost", sql.Decimal(18, 2), header.totalCost || 0)
        .input("TradeINAmount", sql.Decimal(18, 2), header.tradeInAmount || 0)
        .input("TradeInGST", sql.Decimal(18, 2), header.tradeInGst || 0)
        .input("CreateUser", sql.VarChar, header.salesManCode || '')
        .query(`
          IF EXISTS (SELECT 1 FROM tblInstallmentEntryHeader WHERE InsAgreementNo = @InsAgreementNo)
          BEGIN
            UPDATE tblInstallmentEntryHeader SET
              InvoiceDate = @Invdate, GSTType = @GSTType, LocationCode = @LocationCode, 
              CustomerCode = @CustomerCode, Name = @Name, Address1 = @Address1, 
              Address2 = @Address2, Address3 = @Address3, SalesMan = @SalesManCode, 
              DepositAmt = @DepositAmt, TotalInstallment = @TotalInstallment, AmtPaid = @AmtPaid, 
              InstallmentAmt = @InstallmentAmt, LastDate = @LastDate, Remarks = @Remarks, 
              TotalCost = @TotalCost, TradeINAmt = @TradeINAmount, TradeInGST = @TradeInGST,
              ModifyBy = @CreateUser, ModifyDate = GETDATE()
            WHERE InsAgreementNo = @InsAgreementNo
          END
          ELSE
          BEGIN
            INSERT INTO tblInstallmentEntryHeader (
              InsAgreementNo, InvoiceDate, GSTType, LocationCode, CustomerCode, Name, 
              Address1, Address2, Address3, SalesMan, DepositAmt, TotalInstallment, AmtPaid, 
              InstallmentAmt, LastDate, Remarks, TotalCost, TradeINAmt, TradeInGST,
              CreateBy, Createdate
            ) VALUES (
              @InsAgreementNo, @Invdate, @GSTType, @LocationCode, @CustomerCode, @Name, 
              @Address1, @Address2, @Address3, @SalesManCode, @DepositAmt, @TotalInstallment, @AmtPaid, 
              @InstallmentAmt, @LastDate, @Remarks, @TotalCost, @TradeINAmount, @TradeInGST,
              @CreateUser, GETDATE()
            )
          END
        `);

      // Delete existing details before re-inserting
      const delReq = transaction.request();
      await delReq
        .input("InsAgreementNo", sql.VarChar, header.invoiceNo || '')
        .query(`DELETE FROM tblInstallmentEntryDetail WHERE InsAgreementNo = @InsAgreementNo`);


      for (let item of details) {
        const itemReq = transaction.request();
        await itemReq
          .input("LocationCode", sql.VarChar, header.locationCode || '01')
          .input("InsAgreementNo", sql.VarChar, header.invoiceNo || '')
          .input("InventoryCode", sql.VarChar, item.InventoryCode || '')
          .input("Description", sql.VarChar, item.Description || '')
          .input("Weight", sql.Decimal(18, 3), item.Weight || 0)
          .input("GoldLoss", sql.Decimal(18, 3), item.GoldLoss || 0)
          .input("Cost", sql.Decimal(18, 2), item.Cost || 0)
          .input("PCS", sql.Int, item.PCS || 1)
          .input("SMC", sql.Decimal(18, 2), item.SMC || 0)
          .input("GST", sql.Decimal(18, 2), item.GST || 0)
          .input("TotalValue", sql.Decimal(18, 2), item.TotalValue || 0)
          .input("DetailDiscount", sql.Decimal(18, 2), item.DetailDiscount || 0)
          .input("CreateUser", sql.VarChar, header.salesManCode || '')
          .query(`
            INSERT INTO tblInstallmentEntryDetail (
              Locationcode, InsAgreementNo, InventoryCode, Description, Weight, Goldloss, Cost, 
              PCS, SMC, GST, TotalValue, DetailDiscount, CreateUser
            ) VALUES (
              @LocationCode, @InsAgreementNo, @InventoryCode, @Description, @Weight, @GoldLoss, @Cost, 
              @PCS, @SMC, @GST, @TotalValue, @DetailDiscount, @CreateUser
            )
          `);
      }
    } else {
      // Handle Order Save
      const request = transaction.request();
      await request
        .input("OrderNo", sql.VarChar, header.invoiceNo || '')
        .input("OrderDate", sql.DateTime, header.date || new Date())
        .input("CustomerCode", sql.VarChar, header.customerCode || '')
        .input("SupplierCode", sql.VarChar, header.supplierCode || '')
        .input("DeliveryDate", sql.DateTime, header.lastDate || new Date())
        .input("CostPerGram", sql.Decimal(18, 2), header.costPerGram || 0)
        .input("sTradeInType", sql.VarChar, header.tradeInType || '')
        .input("RateFixed", sql.VarChar, header.rateFixed || 'No')
        .input("TotalCostEstimated", sql.Decimal(18, 2), header.totalCost || 0)
        .input("AdvancePaid", sql.Decimal(18, 2), header.depositAmt || 0)
        .input("Status", sql.VarChar, header.status || 'Active')
        .input("Remarks", sql.VarChar, header.remarks || '')
        .input("InvType", sql.VarChar, header.invType || 'O')
        .input("SupplierCost", sql.Decimal(18, 2), header.supplierCost || 0)
        .input("EstimatedGST", sql.Decimal(18, 2), header.estimatedGst || 0)
        .input("IssueDate", sql.DateTime, header.issueDate || new Date())
        .input("SalesManCode", sql.VarChar, header.salesManCode || '')
        .input("TradeINAmount", sql.Decimal(18, 2), header.tradeInAmount || 0)
        .input("TradeInGST", sql.Decimal(18, 2), header.tradeInGst || 0)
        .input("CreateUser", sql.VarChar, header.salesManCode || '')
        .query(`
          IF EXISTS (SELECT 1 FROM tblInventoryOrderHeader WHERE OrderNo = @OrderNo)
          BEGIN
            UPDATE tblInventoryOrderHeader SET
              OrderDate = @OrderDate, CustomerCode = @CustomerCode, SupplierCode = @SupplierCode, 
              DeliveryDate = @DeliveryDate, CostPerGram = @CostPerGram, sTradeInType = @sTradeInType, 
              RateFixed = @RateFixed, TotalCostEstimated = @TotalCostEstimated, AdvancePaid = @AdvancePaid, 
              Status = @Status, Remarks = @Remarks, InvType = @InvType, SupplierCost = @SupplierCost, 
              EstimatedGST = @EstimatedGST, IssueDate = @IssueDate, SalesManCode = @SalesManCode, 
              TradeINAmount = @TradeINAmount, TradeInGST = @TradeInGST,
              ModifyBy = @CreateUser, ModifyDate = GETDATE()
            WHERE OrderNo = @OrderNo
          END
          ELSE
          BEGIN
            INSERT INTO tblInventoryOrderHeader (
              OrderNo, OrderDate, CustomerCode, SupplierCode, DeliveryDate, CostPerGram, sTradeInType, 
              RateFixed, TotalCostEstimated, AdvancePaid, Status, Remarks, InvType, SupplierCost, 
              EstimatedGST, IssueDate, SalesManCode, TradeINAmount, TradeInGST,
              CreateBy, Createdate
            ) VALUES (
              @OrderNo, @OrderDate, @CustomerCode, @SupplierCode, @DeliveryDate, @CostPerGram, @sTradeInType, 
              @RateFixed, @TotalCostEstimated, @AdvancePaid, @Status, @Remarks, @InvType, @SupplierCost, 
              @EstimatedGST, @IssueDate, @SalesManCode, @TradeINAmount, @TradeInGST,
              @CreateUser, GETDATE()
            )
          END
        `);

      // Delete existing details before re-inserting
      const delReq = transaction.request();
      await delReq
        .input("OrderNo", sql.VarChar, header.invoiceNo || '')
        .query(`DELETE FROM tblInventoryOrderDetail WHERE OrderNo = @OrderNo`);


      for (let item of details) {
        const itemReq = transaction.request();
        await itemReq
          .input("OrderNo", sql.VarChar, header.invoiceNo || '')
          .input("Description", sql.VarChar, item.Description || '')
          .input("OrderInventoryCode", sql.VarChar, item.InventoryCode || '')
          .input("StoneDescription", sql.VarChar, item.StoneDescription || '')
          .input("ClassCode", sql.VarChar, item.ClassCode || '')
          .input("CategoryCode", sql.VarChar, item.CategoryCode || '')
          .input("CatalogueCode", sql.VarChar, item.CatalogueCode || '')
          .input("WeightEstimated", sql.Decimal(18, 3), item.Weight || 0)
          .input("StoneWeight", sql.Decimal(18, 3), item.StoneWeight || 0)
          .input("GoldLoss", sql.Decimal(18, 3), item.GoldLoss || 0)
          .input("MC", sql.Decimal(18, 2), item.SMC || 0)
          .input("CostPerGram", sql.Decimal(18, 2), item.Cost || 0)
          .input("EstimatedCost", sql.Decimal(18, 2), item.TotalValue || 0)
          .input("EstimatedGst", sql.Decimal(18, 2), item.GST || 0)
          .input("StoneCost", sql.Decimal(18, 2), item.StoneCost || 0)
          .input("SizeCode", sql.VarChar, item.SizeCode || '')
          .input("SizeType", sql.VarChar, item.SizeType || '')
          .query(`
            INSERT INTO tblInventoryOrderdetail(
              OrderNo, Description, OrderInventoryCode, StoneDescription, ClassCode, CategoryCode, 
              CatalogueCode, WeightEstimated, StoneWeight, GoldLoss, MC, CostPerGram, EstimatedCost, 
              EstimatedGst, StoneCost, SizeCode, SizeType
            ) VALUES (
              @OrderNo, @Description, @OrderInventoryCode, @StoneDescription, @ClassCode, @CategoryCode, 
              @CatalogueCode, @WeightEstimated, @StoneWeight, @GoldLoss, @MC, @CostPerGram, @EstimatedCost, 
              @EstimatedGst, @StoneCost, @SizeCode, @SizeType
            )
          `);
      }
    }

    await transaction.commit();
    res.json({ success: true, message: "Saved successfully!" });
  } catch (err) {
    await transaction.rollback();
    res.status(500).json({ success: false, message: err.message });
  }
});

module.exports = router;
