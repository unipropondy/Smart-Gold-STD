const express = require("express");
const { sql } = require("../db");
const router = express.Router();


// ================= GET ALL WHOLESALE INVOICES (LOV) =================
router.get("/", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query(`
      SELECT DISTINCT 
        InvoiceNo, 
        CustomerCode, 
        CustomerName,
        SalesManCode,
        CreateDate, 
        NetTotal,
        TotalPCS,
        TotalValue,
        GST,
        GSTType
      FROM tblSalesHeader
      WHERE SalesType = 'Wholesale'
      ORDER BY CreateDate DESC
    `);
    return res.status(200).json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("🔥 GET ALL WHOLESALE POS ERROR:", err);
    return res.status(500).json({ success: false, message: "Server Error", error: err.message });
  }
});

// ================= GET NEXT WHOLESALE INVOICE NUMBER =================
router.get("/next-invoice", async (req, res) => {
  try {
    const pool = req.pool;
    // Check if next invoice format exists or generate standard prefix WS-YYMMDD-XXX
    const now = new Date();
    const datePrefix = now.getFullYear().toString().slice(-2) +
      String(now.getMonth() + 1).padStart(2, '0') +
      String(now.getDate()).padStart(2, '0');

    const result = await pool.request()
      .input("Prefix", sql.VarChar, `WS-${datePrefix}%`)
      .query(`
        SELECT TOP 1 InvoiceNo 
        FROM tblSalesHeader 
        WHERE InvoiceNo LIKE @Prefix
        ORDER BY InvoiceNo DESC
      `);

    let nextNo = `WS-${datePrefix}-001`;
    if (result.recordset.length > 0) {
      const lastNo = result.recordset[0].InvoiceNo;
      const parts = lastNo.split('-');
      if (parts.length >= 3) {
        const lastSeq = parseInt(parts[2], 10);
        if (!isNaN(lastSeq)) {
          nextNo = `WS-${datePrefix}-${String(lastSeq + 1).padStart(3, '0')}`;
        }
      }
    }

    return res.status(200).json({ success: true, nextInvoiceNo: nextNo });
  } catch (err) {
    console.error("🔥 NEXT WHOLESALE INVOICE ERROR:", err);
    return res.status(500).json({ success: false, message: "Server Error", error: err.message });
  }
});

// ================= GET SALESMEN LOV FOR WHOLESALE =================
router.get("/lov/salesmen", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query(`
      SELECT SalesManCode, SalesManName 
      FROM tblSalesman
      ORDER BY SalesManCode
    `);
    return res.status(200).json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("🔥 WHOLESALE SALESMEN LOV ERROR:", err);
    return res.status(500).json({ success: false, message: "Server Error", error: err.message });
  }
});

// ================= GET CUSTOMERS LOV FOR WHOLESALE =================
router.get("/lov/customers", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query(`
      SELECT 
        CustomerCode, 
        CustomerName, 
        ISNULL(Address1, '') AS Address1,
        ISNULL(Address2, '') AS Address2,
        ISNULL(Address3, '') AS Address3
      FROM tblCustomer
      ORDER BY CustomerCode
    `);
    return res.status(200).json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("🔥 WHOLESALE CUSTOMERS LOV ERROR:", err);
    return res.status(500).json({ success: false, message: "Server Error", error: err.message });
  }
});

// ================= GET INVENTORY LOV FOR WHOLESALE =================
router.get("/lov/inventory", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query(`
      SELECT 
        a.InventoryCode,
        ISNULL(a.Description, '') AS Description,
        ISNULL(a.ClassCode, '') AS Class,
        ISNULL(a.Weight, 0) AS Weight,
        ISNULL(a.GoldLoss, 0) AS GoldLoss,
        ISNULL(a.CostPerGram, 0) AS CostPerGram,
        ISNULL(a.SMC, 0) AS SMC,
        ISNULL(a.PMC, 0) AS PMC,
        ISNULL(a.SalesPCS, 1) AS PCS,
        ISNULL(a.TotalCost, 0) AS TotalCost,
        ISNULL(a.StoneWeight, 0) AS StoneWeight,
        ISNULL(c.Rate, a.CostPerGram) AS ClassRate,
        ISNULL(b.QtyOnHand, ISNULL(a.SalesPCS, 1)) AS QtyOnHand
      FROM tblInventory a
      LEFT JOIN tblInventoryStock b ON a.InventoryCode = b.InventoryCode
      LEFT JOIN tblClass c ON a.ClassCode = c.ClassCode
      WHERE (a.SalesBillNo IS NULL OR a.SalesBillNo = '' OR LTRIM(RTRIM(a.SalesBillNo)) = '')
      ORDER BY a.InventoryCode
    `);
    return res.status(200).json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("🔥 WHOLESALE INVENTORY LOV ERROR:", err);
    return res.status(500).json({ success: false, message: "Server Error: " + err.message, error: err.message });
  }
});

// ================= GET SINGLE INVENTORY DETAILS BY CODE =================
router.get("/inventory/:code", async (req, res) => {
  try {
    const { code } = req.params;
    const pool = req.pool;

    const result = await pool.request()
      .input("InventoryCode", sql.VarChar, code)
      .query(`
        SELECT 
          a.InventoryCode,
          ISNULL(a.Description, '') AS Description,
          ISNULL(a.ClassCode, '') AS Class,
          ISNULL(a.Weight, 0) AS Weight,
          ISNULL(a.GoldLoss, 0) AS GoldLoss,
          ISNULL(a.CostPerGram, 0) AS CostPerGram,
          ISNULL(a.SMC, 0) AS SMC,
          ISNULL(a.PMC, 0) AS PMC,
          ISNULL(a.SalesPCS, 1) AS PCS,
          ISNULL(a.TotalCost, 0) AS TotalCost,
          ISNULL(a.StoneWeight, 0) AS StoneWeight,
          ISNULL(a.SalesBillNo, '') AS SalesBillNo,
          a.SalesDate,
          ISNULL(b.QtyOnHand, ISNULL(a.SalesPCS, 1)) AS QtyOnHand,
          ISNULL(c.SalesType, '') AS SalesType,
          ISNULL(c.Rate, a.CostPerGram) AS ClassRate
        FROM tblInventory a
        LEFT JOIN tblInventoryStock b ON a.InventoryCode = b.InventoryCode AND a.LocationCode = b.LocationCode
        LEFT JOIN tblClass c ON a.ClassCode = c.ClassCode
        WHERE a.InventoryCode = @InventoryCode
      `);

    if (result.recordset.length === 0) {
      return res.status(404).json({ success: false, message: "Inventory item not found." });
    }

    const item = result.recordset[0];
    if (item.SalesBillNo && item.SalesBillNo.trim() !== '') {
      return res.status(400).json({
        success: false,
        message: `Item already sold on Bill No: ${item.SalesBillNo}`,
        data: item
      });
    }

    return res.status(200).json({ success: true, data: item });
  } catch (err) {
    console.error("🔥 WHOLESALE GET INVENTORY ERROR:", err);
    return res.status(500).json({ success: false, message: "Server Error", error: err.message });
  }
});

// ================= GET INVOICE BY NO =================
router.get("/:invoiceNo", async (req, res) => {
  try {
    const { invoiceNo } = req.params;
    const pool = req.pool;

    const headerResult = await pool.request()
      .input("InvoiceNo", sql.VarChar, invoiceNo)
      .query(`
        SELECT p.*, s.CustomerName AS CustomerFullName
        FROM tblSalesHeader p
        LEFT JOIN tblCustomer s ON p.CustomerCode = s.CustomerCode
        WHERE p.InvoiceNo = @InvoiceNo
      `);

    if (headerResult.recordset.length === 0) {
      return res.status(404).json({ success: false, message: "Wholesale invoice not found." });
    }

    const detailsResult = await pool.request()
      .input("InvoiceNo", sql.VarChar, invoiceNo)
      .query(`
        SELECT * 
        FROM tblSalesDetail 
        WHERE InvoiceNo = @InvoiceNo
        ORDER BY ItemNo
      `);

    return res.status(200).json({
      success: true,
      header: headerResult.recordset[0],
      details: detailsResult.recordset
    });
  } catch (err) {
    console.error("🔥 GET WHOLESALE INVOICE BY NO ERROR:", err);
    return res.status(500).json({ success: false, message: "Server Error", error: err.message });
  }
});

// ================= SAVE/UPDATE WHOLESALE INVOICE =================
router.post("/", async (req, res) => {
  const pool = req.pool;
  const transaction = new sql.Transaction(pool);

  try {
    const { header, details, userId } = req.body;

    if (!header || !header.InvoiceNo) {
      return res.status(400).json({ success: false, message: "InvoiceNo is required in header" });
    }
    if (!header.SalesManCode) {
      return res.status(400).json({ success: false, message: "SalesMan Code is required" });
    }
    if (!header.CustomerCode) {
      return res.status(400).json({ success: false, message: "Customer Code is required" });
    }
    if (!details || details.length === 0) {
      return res.status(400).json({ success: false, message: "At least one item is required" });
    }

    const sysUser = userId || "SYSTEM";
    const curDate = new Date();

    await transaction.begin();

    // 1. Release previously linked inventory for this Invoice if editing
    const revertInvReq = new sql.Request(transaction);
    await revertInvReq
      .input("InvoiceNo", sql.VarChar, header.InvoiceNo)
      .query(`
        UPDATE tblInventory 
        SET SalesBillNo = '', SalesDate = NULL, SalesCostPerGram = 0, SalesMC = 0, SalesAmount = 0
        WHERE SalesBillNo = @InvoiceNo
      `);

    // 2. Delete existing header and details for this InvoiceNo
    const deleteReq = new sql.Request(transaction);
    await deleteReq
      .input("InvoiceNo", sql.VarChar, header.InvoiceNo)
      .query(`
        DELETE FROM tblSalesHeader WHERE InvoiceNo = @InvoiceNo;
        DELETE FROM tblSalesDetail WHERE InvoiceNo = @InvoiceNo;
      `);

    // 3. Compute aggregate totals
    let totalWeight = 0;
    let totalStoneWeight = 0;
    let totalGoldLoss = 0;
    let totalSMC = 0;
    let totalPCS = 0;
    let totalValue = 0;
    let totalCost = 0;

    for (const d of details) {
      totalWeight += parseFloat(d.Weight) || 0;
      totalStoneWeight += parseFloat(d.DSWt || d.StoneWeight) || 0;
      totalGoldLoss += parseFloat(d.GoldLoss) || 0;
      totalSMC += parseFloat(d.SMC) || 0;
      totalPCS += parseFloat(d.PCS) || 1;
      totalValue += parseFloat(d.TotalValue) || 0;
      totalCost += parseFloat(d.TotalCost) || 0;
    }

    // Map GST Type to 1 char (tblSalesHeader.GSTType is char(1))
    let gstTypeChar = 'Z';
    const rawGst = (header.GSTType || header.TaxMode || '').toUpperCase();
    if (rawGst.includes('EXCL') || rawGst === 'E') {
      gstTypeChar = 'E';
    } else if (rawGst.includes('INCL') || rawGst === 'I') {
      gstTypeChar = 'I';
    } else {
      gstTypeChar = 'Z';
    }

    const customerCode = (header.CustomerCode || '').substring(0, 20);
    const customerName = (header.CustomerName || '').substring(0, 50);
    const address1 = (header.Address1 || '').substring(0, 50);
    const address2 = (header.Address2 || '').substring(0, 50);
    const address3 = (header.Address3 || '').substring(0, 50);
    const salesManCode = (header.SalesManCode || '').substring(0, 20);
    const remarks = (header.Remarks || '').substring(0, 50);
    const invNo = (header.InvoiceNo || '').substring(0, 20);

    // Insert into tblSalesHeader
    const insHeaderReq = new sql.Request(transaction);
    await insHeaderReq
      .input("InvoiceNo", sql.VarChar(20), invNo)
      .input("InvoiceDate", sql.DateTime, header.InvoiceDate || curDate)
      .input("CompanyCode", sql.VarChar(20), (header.CompanyCode || "1").substring(0, 20))
      .input("LocationCode", sql.VarChar(20), (header.LocationCode || "HQ").substring(0, 20))
      .input("TerminalCode", sql.VarChar(20), (header.TerminalCode || "T1").substring(0, 20))
      .input("CustomerCode", sql.VarChar(20), customerCode)
      .input("CustomerName", sql.VarChar(50), customerName)
      .input("Address1", sql.VarChar(50), address1)
      .input("Address2", sql.VarChar(50), address2)
      .input("Address3", sql.VarChar(50), address3)
      .input("SalesManCode", sql.VarChar(20), salesManCode)
      .input("SalesType", sql.VarChar(20), "Wholesale")
      .input("TotalWeight", sql.Decimal(18, 4), totalWeight)
      .input("TotalStoneWeight", sql.Decimal(18, 4), totalStoneWeight)
      .input("TotalGoldLoss", sql.Decimal(18, 4), totalGoldLoss)
      .input("TotalSMC", sql.Decimal(18, 4), totalSMC)
      .input("TotalPCS", sql.Decimal(18, 4), totalPCS)
      .input("GST", sql.Decimal(18, 4), parseFloat(header.GST) || 0)
      .input("GSTType", sql.Char(1), gstTypeChar)
      .input("NetTotal", sql.Decimal(18, 4), parseFloat(header.NetTotal) || totalValue)
      .input("TotalValue", sql.Decimal(18, 4), totalValue)
      .input("TotalCost", sql.Decimal(18, 4), totalCost)
      .input("Remarks", sql.VarChar(50), remarks)
      .input("CreateUser", sql.VarChar(20), sysUser.substring(0, 20))
      .input("CreateDate", sql.DateTime, curDate)
      .query(`
        INSERT INTO tblSalesHeader (
          InvoiceNo, InvoiceDate, CompanyCode, LocationCode, TerminalCode,
          CustomerCode, CustomerName, Address1, Address2, Address3,
          SalesManCode, SalesType, TotalWeight, TotalStoneWeight, TotalGoldLoss,
          TotalSMC, TotalPCS, GST, GSTType, NetTotal, TotalValue, TotalCost, Remarks,
          CreateUser, CreateDate
        ) VALUES (
          @InvoiceNo, @InvoiceDate, @CompanyCode, @LocationCode, @TerminalCode,
          @CustomerCode, @CustomerName, @Address1, @Address2, @Address3,
          @SalesManCode, @SalesType, @TotalWeight, @TotalStoneWeight, @TotalGoldLoss,
          @TotalSMC, @TotalPCS, @GST, @GSTType, @NetTotal, @TotalValue, @TotalCost, @Remarks,
          @CreateUser, @CreateDate
        )
      `);

    // 4. Insert tblSalesDetail & Update tblInventory
    const totalSalesAmt = totalValue || 1;
    const headerNetDiscount = parseFloat(header.NetDiscount) || 0;
    let runningNetDiscount = 0;

    for (let index = 0; index < details.length; index++) {
      const row = details[index];
      const itemVal = parseFloat(row.TotalValue) || 0;
      let lineNetDiscount = 0;
      if (totalSalesAmt > 0 && headerNetDiscount > 0) {
        if (index === details.length - 1) {
          lineNetDiscount = Number((headerNetDiscount - runningNetDiscount).toFixed(2));
        } else {
          lineNetDiscount = Number(((itemVal * headerNetDiscount) / totalSalesAmt).toFixed(2));
        }
        runningNetDiscount += lineNetDiscount;
      }

      const lineGst = parseFloat(row.GST) || 0;
      let netAmount = 0;
      let actualSales = 0;
      if (gstTypeChar === 'E') {
        netAmount = itemVal - lineNetDiscount + lineGst;
        actualSales = itemVal - lineNetDiscount;
      } else if (gstTypeChar === 'I') {
        netAmount = itemVal - lineNetDiscount;
        actualSales = itemVal - lineNetDiscount - lineGst;
      } else {
        netAmount = itemVal - lineNetDiscount;
        actualSales = itemVal - lineNetDiscount;
      }

      const insDetReq = new sql.Request(transaction);
      await insDetReq
        .input("InvoiceNo", sql.VarChar(20), invNo)
        .input("InventoryCode", sql.VarChar(20), (row.InventoryCode || "").trim().substring(0, 20))
        .input("Description", sql.VarChar(50), (row.Description || "").substring(0, 50))
        .input("Weight", sql.Decimal(18, 4), parseFloat(row.Weight) || 0)
        .input("StoneWeight", sql.Decimal(18, 4), parseFloat(row.DSWt || row.StoneWeight) || 0)
        .input("GoldLoss", sql.Decimal(18, 4), parseFloat(row.GoldLoss) || 0)
        .input("Rate", sql.Decimal(18, 4), parseFloat(row.CostPerGram) || 0)
        .input("SMC", sql.Decimal(18, 4), parseFloat(row.SMC) || 0)
        .input("PCS", sql.Decimal(18, 4), parseFloat(row.PCS) || 1)
        .input("GST", sql.Decimal(18, 4), lineGst)
        .input("DetailDiscount", sql.Decimal(18, 4), 0)
        .input("NetDiscount", sql.Decimal(18, 4), lineNetDiscount)
        .input("NetAmount", sql.Decimal(18, 4), netAmount)
        .input("ActualSales", sql.Decimal(18, 4), actualSales)
        .input("TotalValue", sql.Decimal(18, 4), itemVal)
        .input("TotalCost", sql.Decimal(18, 4), parseFloat(row.TotalCost) || 0)
        .input("FixedCost", sql.Decimal(18, 4), 0)
        .input("Commission", sql.Decimal(18, 4), 0)
        .input("Remarks", sql.VarChar(50), remarks)
        .input("DailyRate", sql.Decimal(18, 4), parseFloat(row.CostPerGram) || 0)
        .input("SortCode", sql.Int, index + 1)
        .input("CreateUser", sql.VarChar(20), sysUser.substring(0, 20))
        .query(`
          INSERT INTO tblSalesDetail (
            InvoiceNo, InventoryCode, Description, Weight, StoneWeight, GoldLoss,
            Rate, SMC, PCS, GST, DetailDiscount, NetDiscount, NetAmount, ActualSales,
            TotalValue, TotalCost, FixedCost, Commission, Remarks, DailyRate, SortCode,
            CreateUser, CreateDate, ModifyUser, ModifyDate
          ) VALUES (
            @InvoiceNo, @InventoryCode, @Description, @Weight, @StoneWeight, @GoldLoss,
            @Rate, @SMC, @PCS, @GST, @DetailDiscount, @NetDiscount, @NetAmount, @ActualSales,
            @TotalValue, @TotalCost, @FixedCost, @Commission, @Remarks, @DailyRate, @SortCode,
            @CreateUser, GETDATE(), @CreateUser, GETDATE()
          )
        `);

      // Update inventory item SalesBillNo to mark as sold
      const cleanCode = (row.InventoryCode || "").trim();
      if (cleanCode) {
        const updateInvReq = new sql.Request(transaction);
        await updateInvReq
          .input("InventoryCode", sql.VarChar(20), cleanCode)
          .input("SalesBillNo", sql.VarChar(20), invNo)
          .input("SalesDate", sql.DateTime, curDate)
          .input("SalesCostPerGram", sql.Decimal(18, 4), parseFloat(row.CostPerGram) || 0)
          .input("SalesMC", sql.Decimal(18, 4), parseFloat(row.SMC) || 0)
          .input("SalesAmount", sql.Decimal(18, 4), itemVal)
          .query(`
            UPDATE tblInventory 
            SET SalesBillNo = @SalesBillNo, SalesDate = @SalesDate, SalesCostPerGram = @SalesCostPerGram,
                SalesMC = @SalesMC, SalesAmount = @SalesAmount
            WHERE InventoryCode = @InventoryCode
          `);
      }
    }

    await transaction.commit();
    return res.status(200).json({
      success: true,
      message: "Wholesale invoice saved successfully!",
      invoiceNo: header.InvoiceNo
    });
  } catch (err) {
    console.error("🔥 WHOLESALE POS SAVE ERROR:", err);
    try {
      await transaction.rollback();
    } catch (rbErr) {
      console.error("Rollback failed:", rbErr);
    }
    return res.status(500).json({ success: false, message: "Save failed: " + err.message });
  }
});

// ================= DELETE / CANCEL WHOLESALE INVOICE =================
router.delete("/:id", async (req, res) => {
  const pool = req.pool;
  const transaction = new sql.Transaction(pool);

  try {
    const { id } = req.params;

    // Check if invoice exists and verify type
    const checkReq = await pool.request()
      .input("InvoiceNo", sql.VarChar, id)
      .query(`SELECT SalesType FROM tblSalesHeader WHERE InvoiceNo = @InvoiceNo`);

    if (checkReq.recordset.length === 0) {
      return res.status(404).json({ success: false, message: "Invoice not found" });
    }

    await transaction.begin();

    // Revert inventory items
    const revInvReq = new sql.Request(transaction);
    await revInvReq
      .input("InvoiceNo", sql.VarChar, id)
      .query(`
        UPDATE tblInventory 
        SET SalesBillNo = '', SalesDate = NULL, SalesCostPerGram = 0, SalesMC = 0, SalesAmount = 0
        WHERE SalesBillNo = @InvoiceNo
      `);

    // Delete header and details
    const delReq = new sql.Request(transaction);
    await delReq
      .input("InvoiceNo", sql.VarChar, id)
      .query(`
        DELETE FROM tblSalesHeader WHERE InvoiceNo = @InvoiceNo;
        DELETE FROM tblSalesDetail WHERE InvoiceNo = @InvoiceNo;
      `);

    await transaction.commit();
    return res.status(200).json({ success: true, message: `Wholesale Invoice ${id} cancelled successfully!` });
  } catch (err) {
    console.error("🔥 WHOLESALE DELETE ERROR:", err);
    try {
      await transaction.rollback();
    } catch (rbErr) {
      console.error("Rollback failed:", rbErr);
    }
    return res.status(500).json({ success: false, message: "Server Error", error: err.message });
  }
});

module.exports = router;
