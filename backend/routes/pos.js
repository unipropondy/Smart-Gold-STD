const express = require("express");
const { sql } = require("../db");
const router = express.Router();

// ================= GET ALL INVOICES (LOV) =================
router.get("/", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query(`
      SELECT DISTINCT InvoiceNo, CustomerCode, CreateDate, NetTotal
      FROM tblSalesHeader
      ORDER BY CreateDate DESC
    `);
    return res.status(200).json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("🔥 GET ALL POS ERROR:", err);
    return res.status(500).json({ success: false, message: "Server Error", error: err.message });
  }
});

// ================= SAVE/UPDATE INVOICE =================
router.post("/", async (req, res) => {
  const pool = req.pool;
  const transaction = new sql.Transaction(pool);

  try {
    const { header, details, userId } = req.body;

    if (!header || !header.InvoiceNo) {
      return res.status(400).json({ success: false, message: "InvoiceNo is required in header" });
    }

    const sysUser = userId || "SYSTEM";
    const curDate = new Date();

    await transaction.begin();

    const request = new sql.Request(transaction);

    // Delete existing lines
    await request
      .input("InvoiceNo", sql.VarChar, header.InvoiceNo)
      .query(`
        DELETE FROM tblSalesHeader WHERE InvoiceNo = @InvoiceNo;
        DELETE FROM tblSalesDetail WHERE InvoiceNo = @InvoiceNo;
      `);

    // Insert into tblSales via Stored Procedure
    // Map GST Type
    let gstTypeChar = 'I';
    if (header.TaxMode === 'Exclusive' || header.GSTType === 'E') {
      gstTypeChar = 'E';
    } else if (header.TaxMode === 'Zero Rated' || header.GSTType === 'Z') {
      gstTypeChar = 'Z';
    }

    // Map eTRS Type
    let eTRSTypeChar = 'N';
    if (header.eTRS === 'For eTRS' || header.eTRSType === 'Y') {
      eTRSTypeChar = 'Y';
    } else if (header.eTRS === 'eTRS Eligible' || header.eTRSType === 'E') {
      eTRSTypeChar = 'E';
    }

    // Map Sales Type
    let salesTypeStr = 'CASH';
    if (header.PaymentType === 'Trade In' || header.TradeIn || header.SalesType === 'TRADE-IN' || header.SalesType === 'TRADEIN') {
      salesTypeStr = 'TRADEIN';
    } else if (header.PaymentType === 'Installment' || header.SalesType === 'INSTALLMENT') {
      salesTypeStr = 'INSTALLMENT';
    }

    // Calculate aggregated detail values
    let totalWeight = 0;
    let totalStoneWeight = 0;
    let totalGoldLoss = 0;
    let totalSMC = 0;
    let totalPCS = 0;
    let totalDetailDiscount = 0;
    let totalCost = 0;
    let tradeInAmt = 0;
    let tradeInWeight = 0;
    let tradeInGst = 0;

    for (const d of details) {
      const wt = parseFloat(d.Weight) || 0;
      const swt = parseFloat(d.StoneWeight) || 0;
      const gloss = parseFloat(d.GoldLoss) || 0;
      const smc = parseFloat(d.SMC) || 0;
      const pcs = parseFloat(d.PCS) || 1;
      const disc = parseFloat(d.DetailDiscount) || 0;
      const cost = parseFloat(d.TotalCost) || 0;
      const dVal = parseFloat(d.TotalValue) || 0;
      const dGst = parseFloat(d.GST) || 0;

      if ((d.SalesType && d.SalesType.toUpperCase() === 'TI') || header.TradeIn) {
        tradeInWeight += wt;
        tradeInAmt += (dVal - dGst);
        tradeInGst += dGst;
      } else {
        totalWeight += wt;
        totalStoneWeight += swt;
        totalGoldLoss += gloss;
        totalSMC += smc;
        totalPCS += pcs;
        totalDetailDiscount += disc;
        totalCost += cost;
      }
    }

    // Insert into tblSales via Stored Procedure
    const insHeaderReq = new sql.Request(transaction);
    await insHeaderReq
        .input("InvoiceNo", sql.VarChar, header.InvoiceNo)
        .input("Date", sql.SmallDateTime, header.InvoiceDate || curDate)
        .input("CompanyCode", sql.VarChar, header.CompanyCode || "1")
        .input("LocationCode", sql.VarChar, header.LocationCode || "HQ")
        .input("TerminalCode", sql.VarChar, header.TerminalCode || "T1")
        .input("CustomerCode", sql.VarChar, header.CustomerCode || "CASH")
        .input("Membercode", sql.VarChar, header.MemberCode || "")
        .input("CustomerName", sql.VarChar, header.CustomerName || "CASH CUSTOMER")
        .input("Address1", sql.VarChar, header.CustomerAddress || header.Address1 || "")
        .input("Address2", sql.VarChar, header.Address2 || "")
        .input("Address3", sql.VarChar, header.Address3 || "")
        .input("SalesManCode", sql.VarChar, header.SalesManCode || "")
        .input("SalesType", sql.VarChar, salesTypeStr)
        .input("TotalWeight", sql.Numeric(18, 3), totalWeight || parseFloat(header.TotalQty) || 0)
        .input("TotalStoneWeight", sql.Numeric(18, 3), totalStoneWeight || parseFloat(header.TotalStoneWeight) || 0)
        .input("TotalGoldLoss", sql.Numeric(18, 3), totalGoldLoss || parseFloat(header.TotalGoldLoss) || 0)
        .input("TotalSMC", sql.Numeric(18, 2), totalSMC || parseFloat(header.TotalSMC) || 0)
        .input("TotalPCS", sql.Numeric(18, 0), totalPCS || parseFloat(header.TotalQty) || 0)
        .input("GST", sql.Numeric(18, 2), parseFloat(header.Tax) || 0)
        .input("GSTType", sql.VarChar, gstTypeChar)
        .input("DetailDiscount", sql.Numeric(18, 2), totalDetailDiscount || parseFloat(header.DetailDiscount) || 0)
        .input("NetDiscount", sql.Numeric(18, 2), parseFloat(header.NetDiscount) || 0)
        .input("Discount", sql.Numeric(18, 2), parseFloat(header.Discount) || 0)
        .input("NetTotal", sql.Numeric(18, 2), parseFloat(header.GrandTotal) || 0)
        .input("PaidAmt", sql.Numeric(18, 2), parseFloat(header.GrandTotal) || 0)
        .input("BalanceAmt", sql.Numeric(18, 2), parseFloat(header.BalanceAmt) || 0)
        .input("TotalValue", sql.Numeric(18, 2), parseFloat(header.TotalSalesAmt) || 0)
        .input("TotalCost", sql.Numeric(18, 2), totalCost || parseFloat(header.TotalCost) || 0)
        .input("TradeInAmt", sql.Numeric(18, 2), tradeInAmt || parseFloat(header.TradeInAmt) || 0)
        .input("DepositAmt", sql.Numeric(18, 2), parseFloat(header.DepositAmt) || 0)
        .input("InsAgreementNo", sql.VarChar, header.InsAgreementNo || "")
        .input("Commission", sql.Numeric(18, 2), parseFloat(header.Commission) || 0)
        .input("Remarks", sql.VarChar, header.Remarks || "")
        .input("TradeInGst", sql.Numeric(18, 2), tradeInGst || parseFloat(header.TradeInGst) || 0)
        .input("eTRSType", sql.VarChar, eTRSTypeChar)
        .input("UserID", sql.VarChar, sysUser)
        .execute("sp_SalesHeader");

    // Insert into tblSalesDetail via Stored Procedure
    const totalSalesAmt = parseFloat(header.TotalSalesAmt) || 1;
    const headerNetDiscount = parseFloat(header.NetDiscount) || 0;
    const headerCommission = parseFloat(header.Commission) || 0;
    let runningNetDiscount = 0;
    let runningCommission = 0;

    for (let i = 0; i < details.length; i++) {
      const d = details[i];
      const itemVal = parseFloat(d.TotalValue) || 0;

      // Apportion NetDiscount and Commission across items matching VB pos.txt logic
      let lineNetDiscount = 0;
      let lineCommission = 0;
      if (totalSalesAmt > 0 && headerNetDiscount > 0) {
        if (i === details.length - 1) {
          lineNetDiscount = Number((headerNetDiscount - runningNetDiscount).toFixed(2));
        } else {
          lineNetDiscount = Number(((itemVal * headerNetDiscount) / totalSalesAmt).toFixed(2));
        }
        runningNetDiscount += lineNetDiscount;
      }

      if (totalSalesAmt > 0 && headerCommission > 0) {
        if (i === details.length - 1) {
          lineCommission = Number((headerCommission - runningCommission).toFixed(2));
        } else {
          lineCommission = Number(((itemVal * headerCommission) / totalSalesAmt).toFixed(2));
        }
        runningCommission += lineCommission;
      }

      const lineGst = parseFloat(d.GST) || 0;
      let netAmount = 0;
      let actualSales = 0;

      if (gstTypeChar === 'E') {
        netAmount = itemVal - lineNetDiscount + lineGst + lineCommission;
        actualSales = itemVal - lineNetDiscount + lineCommission;
      } else if (gstTypeChar === 'I') {
        netAmount = itemVal - lineNetDiscount + lineCommission;
        actualSales = itemVal - lineNetDiscount - lineGst + lineCommission;
      } else {
        netAmount = itemVal - lineNetDiscount + lineCommission;
        actualSales = itemVal - lineNetDiscount + lineCommission;
      }

      const insertReq = new sql.Request(transaction);
      await insertReq
        .input("InvoiceNo", sql.VarChar, header.InvoiceNo)
        .input("InventoryCode", sql.VarChar, d.InventoryCode || "")
        .input("Description", sql.VarChar, d.Description || "")
        .input("LocationCode", sql.VarChar, header.LocationCode || "HQ")
        .input("Weight", sql.Numeric(18, 3), parseFloat(d.Weight) || 0)
        .input("StoneWeight", sql.Numeric(18, 3), parseFloat(d.StoneWeight) || 0)
        .input("GoldLoss", sql.Numeric(18, 3), parseFloat(d.GoldLoss) || 0)
        .input("Rate", sql.Numeric(18, 2), parseFloat(d.CostPerGram) || 0) 
        .input("SMC", sql.Numeric(18, 2), parseFloat(d.SMC) || 0)
        .input("PCS", sql.Numeric(18, 0), parseFloat(d.PCS) || 1)
        .input("GST", sql.Numeric(18, 2), lineGst)
        .input("DetailDiscount", sql.Numeric(18, 2), parseFloat(d.DetailDiscount) || 0)
        .input("NetDiscount", sql.Numeric(18, 2), lineNetDiscount)
        .input("NetAmount", sql.Numeric(18, 2), Number(netAmount.toFixed(2)))
        .input("ActualSales", sql.Numeric(18, 2), Number(actualSales.toFixed(2)))
        .input("TotalValue", sql.Numeric(18, 2), itemVal)
        .input("TotalCost", sql.Numeric(18, 2), parseFloat(d.TotalCost) || 0)
        .input("Commission", sql.Numeric(18, 2), lineCommission)
        .input("Remarks", sql.VarChar, d.Remarks || header.Remarks || "")
        .input("DailyClassRate", sql.Numeric(18, 2), parseFloat(d.DailyClassRate) || 0)
        .input("SortCode", sql.Int, i + 1)
        .input("SalesType", sql.VarChar, d.SalesType || salesTypeStr)
        .input("TradeInGst", sql.Numeric(18, 2), parseFloat(d.TradeInGst) || 0)
        .input("UserID", sql.VarChar, sysUser)
        .execute("sp_SalesDetail");

      // Update tblInventory to mark as sold
      const invReq = new sql.Request(transaction);
      await invReq
        .input("InventoryCode", sql.VarChar, d.InventoryCode)
        .input("SalesBillNo", sql.VarChar, header.InvoiceNo)
        .input("SalesDate", sql.SmallDateTime, curDate)
        .input("SalesCostPerGram", sql.Numeric(18, 2), parseFloat(d.CostPerGram) || 0)
        .input("SalesMC", sql.Numeric(18, 2), parseFloat(d.SMC) || 0)
        .input("SalesAmount", sql.Numeric(18, 2), itemVal)
        .query(`
          UPDATE tblInventory 
          SET SalesBillNo = @SalesBillNo, SalesDate = @SalesDate, SalesCostPerGram = @SalesCostPerGram, 
              SalesMC = @SalesMC, SalesAmount = @SalesAmount
          WHERE InventoryCode = @InventoryCode
        `);
    }

    // Insert Payment Modes via Stored Procedure
    const payModes = req.body.payModes || [{ PayMode: 'CASH', Amount: header.GrandTotal }];
    for (let i = 0; i < payModes.length; i++) {
        const pm = payModes[i];
        if (parseFloat(pm.Amount) > 0) {
            const pmReq = new sql.Request(transaction);
            await pmReq
                .input("InvoiceNo", sql.VarChar, header.InvoiceNo)
                .input("Date", sql.SmallDateTime, header.InvoiceDate || curDate)
                .input("Paymode", sql.VarChar, pm.PayMode)
                .input("Amount", sql.Numeric(18, 2), parseFloat(pm.Amount))
                .input("Commission", sql.Numeric(18, 2), parseFloat(pm.Commission) || 0)
                .input("SlipNo", sql.VarChar, pm.SlipNo || "")
                .input("SalesType", sql.VarChar, salesTypeStr)
                .input("UserID", sql.VarChar, sysUser)
                .execute("sp_PayModeDetail");
        }
    }

    await transaction.commit();
    return res.status(200).json({ success: true, message: "Invoice saved successfully via Stored Procedures" });
  } catch (err) {
    await transaction.rollback();
    console.error("🔥 POST POS ERROR:", err);
    return res.status(500).json({ success: false, message: "Server Error", error: err.message });
  }
});

// ================= DELETE INVOICE =================
router.delete("/:invoiceNo", async (req, res) => {
  const pool = req.pool;
  const transaction = new sql.Transaction(pool);

  try {
    const { invoiceNo } = req.params;

    await transaction.begin();

    const request = new sql.Request(transaction);
    await request
      .input("InvoiceNo", sql.VarChar, invoiceNo)
      .query(`
        DELETE FROM tblSalesHeader WHERE InvoiceNo = @InvoiceNo;
        DELETE FROM tblSalesDetail WHERE InvoiceNo = @InvoiceNo;
        DELETE FROM tblPaymodeDetail WHERE InvoiceNo = @InvoiceNo;
        UPDATE tblInventory SET SalesBillNo = '', SalesDate = NULL WHERE SalesBillNo = @InvoiceNo;
      `);

    await transaction.commit();
    return res.status(200).json({ success: true, message: "Invoice voided successfully" });
  } catch (err) {
    await transaction.rollback();
    console.error("🔥 DELETE POS ERROR:", err);
    return res.status(500).json({ success: false, message: "Server Error", error: err.message });
  }
});

// ================= GET MARKET RATE LOV =================
router.get("/lov/market-rate/:classCode", async (req, res) => {
    try {
        const { classCode } = req.params;
        const pool = req.pool;
        const curDate = new Date().toISOString().slice(0, 10); // YYYY-MM-DD
        
        const result = await pool.request()
            .input("ClassCode", sql.VarChar, classCode)
            .input("SalesDate", sql.Date, curDate)
            .query(`
                SELECT Rate 
                FROM tblClassRate 
                WHERE ClassCode = @ClassCode 
                AND @SalesDate BETWEEN FromDate AND ToDate
            `);
        return res.status(200).json({ success: true, data: result.recordset });
    } catch (err) {
        console.error("🔥 LOV Market Rate ERROR:", err);
        return res.status(500).json({ success: false, message: "Server Error", error: err.message });
    }
});

// ================= GET ALL MARKET RATES =================
router.get("/lov/market-rates", async (req, res) => {
    try {
        const pool = req.pool;
        const curDate = new Date().toISOString().slice(0, 10);
        
        const result = await pool.request()
            .input("SalesDate", sql.Date, curDate)
            .query(`
                SELECT ClassCode, ISNULL(Rate, 0.00) as Rate
                FROM tblClassRate 
                WHERE @SalesDate BETWEEN FromDate AND ToDate
                ORDER BY ClassCode
            `);
            
        return res.status(200).json({ success: true, data: result.recordset });
    } catch (err) {
        console.error("🔥 All Market Rates ERROR:", err);
        return res.status(500).json({ success: false, message: "Server Error", error: err.message });
    }
});

// ================= GET MC DISCOUNT LOV =================
router.get("/lov/mc-discount/:classCode", async (req, res) => {
    try {
        const { classCode } = req.params;
        const pool = req.pool;
        
        const result = await pool.request()
            .input("ClassCode", sql.VarChar, classCode)
            .query(`
                SELECT ISNULL(MCDiscount, 0.00) AS MCDiscount 
                FROM tblClass 
                WHERE ClassCode = @ClassCode
            `);
            
        return res.status(200).json({ success: true, data: result.recordset });
    } catch (err) {
        console.error("🔥 LOV MC Discount ERROR:", err);
        return res.status(500).json({ success: false, message: "Server Error", error: err.message });
    }
});

// ================= GET CUSTOMERS LOV =================
router.get("/lov/customers", async (req, res) => {
    try {
        const pool = req.pool;
        const result = await pool.request().query(`
            SELECT CustomerCode, CustomerName 
            FROM tblCustomer
        `);
        return res.status(200).json({ success: true, data: result.recordset });
    } catch (err) {
        console.error("🔥 LOV Customers ERROR:", err);
        return res.status(500).json({ success: false, message: "Server Error", error: err.message });
    }
});

// ================= GET SALESMEN LOV =================
router.get("/lov/salesmen", async (req, res) => {
    try {
        const pool = req.pool;
        const result = await pool.request().query(`
            SELECT SalesManCode, SalesManName 
            FROM tblSalesman
        `);
        return res.status(200).json({ success: true, data: result.recordset });
    } catch (err) {
        console.error("🔥 LOV Salesmen ERROR:", err);
        return res.status(500).json({ success: false, message: "Server Error", error: err.message });
    }
});

// ================= GET INVOICE BY NO =================
router.get("/:invoiceNo", async (req, res) => {
  try {
    const { invoiceNo } = req.params;
    const pool = req.pool;

    const detailsResult = await pool.request()
      .input("InvoiceNo", sql.VarChar, invoiceNo)
      .query(`
        SELECT p.*, s.CustomerName 
        FROM tblSalesHeader p
        LEFT JOIN tblCustomer s ON p.CustomerCode = s.CustomerCode
        WHERE p.InvoiceNo = @InvoiceNo
      `);

    if (detailsResult.recordset.length === 0) {
      return res.status(404).json({ success: false, message: "Invoice not found" });
    }

    const header = detailsResult.recordset[0];

    const itemsResult = await pool.request()
      .input("InvoiceNo", sql.VarChar, invoiceNo)
      .query(`SELECT * FROM tblSalesDetail WHERE InvoiceNo = @InvoiceNo`);

    return res.status(200).json({
      success: true,
      header: header,
      details: itemsResult.recordset
    });
  } catch (err) {
    console.error("🔥 GET POS ERROR:", err);
    return res.status(500).json({ success: false, message: "Server Error", error: err.message });
  }
});

module.exports = router;
