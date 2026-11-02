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

    if (!header) {
      return res.status(400).json({ success: false, message: "Invoice header is required" });
    }

    const sysUser = userId || "SYSTEM";
    const curDate = new Date();

    await transaction.begin();

    // Generate or fetch InvoiceNo
    let invNo = header.InvoiceNo;
    if (!invNo || !invNo.trim() || invNo.startsWith('INV-')) {
      // 1. Try tblCompany (e.g. NextInvoiceNo or NextSalesOrderNo)
      let foundNextNo = null;
      try {
        const compRes = await new sql.Request(transaction).query(`
          SELECT TOP 1 NextInvoiceNo, NextSalesNo, NextSalesBillNo FROM tblCompany
        `);
        if (compRes.recordset.length > 0) {
          const row = compRes.recordset[0];
          foundNextNo = row.NextInvoiceNo || row.NextSalesNo || row.NextSalesBillNo;
          if (foundNextNo) {
            const nextVal = parseInt(foundNextNo, 10);
            if (!isNaN(nextVal)) {
              await new sql.Request(transaction).query(`
                UPDATE tblCompany 
                SET NextInvoiceNo = ${nextVal + 1}
              `);
            }
          }
        }
      } catch (cErr) {
        // tblCompany column might differ
      }

      // 2. Try tblInvoiceNo if not found in tblCompany
      if (!foundNextNo) {
        try {
          const invNumRes = await new sql.Request(transaction).query(`
            SELECT TOP 1 NextInvoice FROM tblInvoiceNo WHERE Terminal = 'T1' OR LocationCode = 'HQ'
          `);
          if (invNumRes.recordset.length > 0 && invNumRes.recordset[0].NextInvoice) {
            foundNextNo = invNumRes.recordset[0].NextInvoice;
            const nextVal = parseInt(foundNextNo, 10);
            if (!isNaN(nextVal)) {
              await new sql.Request(transaction).query(`
                UPDATE tblInvoiceNo SET NextInvoice = '${nextVal + 1}' WHERE Terminal = 'T1' OR LocationCode = 'HQ'
              `);
            }
          }
        } catch (tErr) {
          // tblInvoiceNo does not exist in schema
        }
      }

      // 3. Fallback: Query MAX InvoiceNo from tblSalesHeader or generate sequential ID
      if (!foundNextNo) {
        try {
          const maxRes = await new sql.Request(transaction).query(`
            SELECT TOP 1 InvoiceNo FROM tblSalesHeader WHERE ISNUMERIC(InvoiceNo) = 1 ORDER BY CAST(InvoiceNo AS BIGINT) DESC
          `);
          if (maxRes.recordset.length > 0 && maxRes.recordset[0].InvoiceNo) {
            const maxVal = parseInt(maxRes.recordset[0].InvoiceNo, 10);
            foundNextNo = String(maxVal + 1);
          }
        } catch (mErr) {
          // Fallback if InvoiceNo not numeric
        }
      }

      invNo = foundNextNo ? String(foundNextNo).trim() : `100${Date.now().toString().slice(-5)}`;
    }

    const request = new sql.Request(transaction);

    // Delete existing lines if updating
    await request
      .input("InvoiceNo", sql.VarChar, invNo)
      .query(`
        DELETE FROM tblSalesHeader WHERE InvoiceNo = @InvoiceNo;
        DELETE FROM tblSalesDetail WHERE InvoiceNo = @InvoiceNo;
        DELETE FROM tblPayModeDetail WHERE InvoiceNo = @InvoiceNo;
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

    // Insert into tblSales Header
    let useDirectInsert = false;
    try {
      const insHeaderReq = new sql.Request(transaction);
      await insHeaderReq
          .input("InvoiceNo", sql.VarChar, invNo)
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
    } catch (spErr) {
      console.warn("⚠️ sp_SalesHeader execution failed, falling back to direct INSERT:", spErr.message);
      useDirectInsert = true;
      const directHdrReq = new sql.Request(transaction);
      // Determine valid columns dynamically or use exact frmGPOS tblSalesHeader column names
      let salesHdrCols = [];
      try {
        const colRes = await new sql.Request(transaction).query(`
          SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tblSalesHeader'
        `);
        salesHdrCols = colRes.recordset.map(c => c.COLUMN_NAME);
      } catch (colErr) {}

      const hasCol = (name) => salesHdrCols.length === 0 || salesHdrCols.some(c => c.toLowerCase() === name.toLowerCase());

      const cols = [];
      const vals = [];

      const addField = (colName, sqlType, value) => {
        if (hasCol(colName)) {
          cols.push(colName);
          vals.push(`@${colName}`);
          directHdrReq.input(colName, sqlType, value);
        }
      };

      addField("InvoiceNo", sql.VarChar, invNo);
      addField("InvoiceDate", sql.SmallDateTime, header.InvoiceDate || curDate);
      addField("Date", sql.SmallDateTime, header.InvoiceDate || curDate);
      addField("CompanyCode", sql.VarChar, header.CompanyCode || "1");
      addField("LocationCode", sql.VarChar, header.LocationCode || "HQ");
      addField("TerminalCode", sql.VarChar, header.TerminalCode || "T1");
      addField("CustomerCode", sql.VarChar, header.CustomerCode || "CASH");
      addField("CustomerName", sql.VarChar, header.CustomerName || "CASH CUSTOMER");
      addField("Address1", sql.VarChar, header.CustomerAddress || header.Address1 || "");
      addField("SalesManCode", sql.VarChar, header.SalesManCode || "");
      addField("SalesType", sql.VarChar, salesTypeStr);
      addField("TotalWeight", sql.Numeric(18, 3), totalWeight || parseFloat(header.TotalQty) || 0);
      addField("TotalQty", sql.Numeric(18, 3), totalWeight || parseFloat(header.TotalQty) || 0);
      addField("TotalStoneWeight", sql.Numeric(18, 3), totalStoneWeight || 0);
      addField("TotalGoldLoss", sql.Numeric(18, 3), totalGoldLoss || 0);
      addField("TotalSMC", sql.Numeric(18, 2), totalSMC || 0);
      addField("TotalPCS", sql.Numeric(18, 0), totalPCS || 0);
      addField("GST", sql.Numeric(18, 2), parseFloat(header.Tax) || 0);
      addField("GSTType", sql.VarChar, gstTypeChar);
      addField("DetailDiscount", sql.Numeric(18, 2), totalDetailDiscount || 0);
      addField("NetDiscount", sql.Numeric(18, 2), parseFloat(header.NetDiscount) || 0);
      addField("Discount", sql.Numeric(18, 2), parseFloat(header.Discount) || 0);
      addField("NetTotal", sql.Numeric(18, 2), parseFloat(header.GrandTotal) || 0);
      addField("PaidAmt", sql.Numeric(18, 2), parseFloat(header.GrandTotal) || 0);
      addField("BalanceAmt", sql.Numeric(18, 2), parseFloat(header.BalanceAmt) || 0);
      addField("TotalValue", sql.Numeric(18, 2), parseFloat(header.TotalSalesAmt) || 0);
      addField("TotalCost", sql.Numeric(18, 2), totalCost || 0);
      addField("TradeInAmt", sql.Numeric(18, 2), tradeInAmt || 0);
      addField("DepositAmt", sql.Numeric(18, 2), parseFloat(header.DepositAmt) || 0);
      addField("Remarks", sql.VarChar, header.Remarks || "");
      if (hasCol("CreateDate")) {
        cols.push("CreateDate");
        vals.push("GETDATE()");
      }
      addField("CreateUser", sql.VarChar, sysUser);

      await directHdrReq.query(`
        INSERT INTO tblSalesHeader (${cols.join(', ')})
        VALUES (${vals.join(', ')})
      `);
    }

    // Insert into tblSalesDetail
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

      const pcsVal = (typeof d.PCS === 'number' ? d.PCS : parseInt(d.PCS, 10)) || 1;

      if (!useDirectInsert) {
        try {
          const insertReq = new sql.Request(transaction);
          await insertReq
            .input("InvoiceNo", sql.VarChar, invNo)
            .input("InventoryCode", sql.VarChar, d.InventoryCode || "")
            .input("Description", sql.VarChar, d.Description || "")
            .input("LocationCode", sql.VarChar, header.LocationCode || "HQ")
            .input("Weight", sql.Numeric(18, 3), parseFloat(d.Weight) || 0)
            .input("StoneWeight", sql.Numeric(18, 3), parseFloat(d.StoneWeight) || 0)
            .input("GoldLoss", sql.Numeric(18, 3), parseFloat(d.GoldLoss) || 0)
            .input("Rate", sql.Numeric(18, 2), parseFloat(d.CostPerGram) || 0) 
            .input("SMC", sql.Numeric(18, 2), parseFloat(d.SMC) || 0)
            .input("PCS", sql.Numeric(18, 0), pcsVal)
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
        } catch (spDetErr) {
          console.warn("⚠️ sp_SalesDetail failed, falling back to direct INSERT:", spDetErr.message);
          useDirectInsert = true;
        }
      }

      if (useDirectInsert) {
        const directDetReq = new sql.Request(transaction);

        let salesDetCols = [];
        try {
          const colRes = await new sql.Request(transaction).query(`
            SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tblSalesDetail'
          `);
          salesDetCols = colRes.recordset.map(c => c.COLUMN_NAME);
        } catch (colErr) {}

        const hasDetCol = (name) => salesDetCols.length === 0 || salesDetCols.some(c => c.toLowerCase() === name.toLowerCase());

        const dCols = [];
        const dVals = [];

        const addDetField = (colName, sqlType, value) => {
          if (hasDetCol(colName)) {
            dCols.push(colName);
            dVals.push(`@${colName}`);
            directDetReq.input(colName, sqlType, value);
          }
        };

        addDetField("InvoiceNo", sql.VarChar, invNo);
        addDetField("InventoryCode", sql.VarChar, d.InventoryCode || "");
        addDetField("Description", sql.VarChar, d.Description || "");
        addDetField("LocationCode", sql.VarChar, header.LocationCode || "HQ");
        addDetField("Weight", sql.Numeric(18, 3), parseFloat(d.Weight) || 0);
        addDetField("StoneWeight", sql.Numeric(18, 3), parseFloat(d.StoneWeight) || 0);
        addDetField("GoldLoss", sql.Numeric(18, 3), parseFloat(d.GoldLoss) || 0);
        addDetField("Rate", sql.Numeric(18, 2), parseFloat(d.CostPerGram) || 0);
        addDetField("SMC", sql.Numeric(18, 2), parseFloat(d.SMC) || 0);
        addDetField("PCS", sql.Numeric(18, 0), pcsVal);
        addDetField("GST", sql.Numeric(18, 2), lineGst);
        addDetField("DetailDiscount", sql.Numeric(18, 2), parseFloat(d.DetailDiscount) || 0);
        addDetField("NetDiscount", sql.Numeric(18, 2), lineNetDiscount);
        addDetField("NetAmount", sql.Numeric(18, 2), Number(netAmount.toFixed(2)));
        addDetField("ActualSales", sql.Numeric(18, 2), Number(actualSales.toFixed(2)));
        addDetField("TotalValue", sql.Numeric(18, 2), itemVal);
        addDetField("TotalCost", sql.Numeric(18, 2), parseFloat(d.TotalCost) || 0);
        addDetField("Remarks", sql.VarChar, d.Remarks || header.Remarks || "");
        addDetField("SortCode", sql.Int, i + 1);
        addDetField("SalesType", sql.VarChar, d.SalesType || salesTypeStr);

        await directDetReq.query(`
          INSERT INTO tblSalesDetail (${dCols.join(', ')})
          VALUES (${dVals.join(', ')})
        `);
      }

      // Update tblInventory to mark as sold
      const invReq = new sql.Request(transaction);
      await invReq
        .input("InventoryCode", sql.VarChar, d.InventoryCode)
        .input("SalesBillNo", sql.VarChar, invNo)
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

    // Insert Payment Modes
    const payModes = req.body.payModes || [{ PayMode: 'CASH', Amount: header.GrandTotal }];
    for (let i = 0; i < payModes.length; i++) {
        const pm = payModes[i];
        if (parseFloat(pm.Amount) > 0) {
            let pmSuccess = false;
            if (!useDirectInsert) {
              try {
                const pmReq = new sql.Request(transaction);
                await pmReq
                    .input("InvoiceNo", sql.VarChar, invNo)
                    .input("Date", sql.SmallDateTime, header.InvoiceDate || curDate)
                    .input("Paymode", sql.VarChar, pm.PayMode)
                    .input("Amount", sql.Numeric(18, 2), parseFloat(pm.Amount))
                    .input("Commission", sql.Numeric(18, 2), parseFloat(pm.Commission) || 0)
                    .input("SlipNo", sql.VarChar, pm.SlipNo || "")
                    .input("SalesType", sql.VarChar, salesTypeStr)
                    .input("UserID", sql.VarChar, sysUser)
                    .execute("sp_PayModeDetail");
                pmSuccess = true;
              } catch (pmErr) {
                console.warn("⚠️ sp_PayModeDetail execution failed, falling back to direct INSERT:", pmErr.message);
              }
            }

            if (!pmSuccess) {
              const directPmReq = new sql.Request(transaction);

              let payModeCols = [];
              try {
                const colRes = await new sql.Request(transaction).query(`
                  SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tblPayModeDetail'
                `);
                payModeCols = colRes.recordset.map(c => c.COLUMN_NAME.toLowerCase());
              } catch (colErr) {}

              const pCols = [];
              const pVals = [];

              const addPmField = (colName, sqlType, value) => {
                const lower = colName.toLowerCase();
                if (payModeCols.length === 0 || payModeCols.includes(lower)) {
                  pCols.push(colName);
                  pVals.push(`@pm_${colName}`);
                  directPmReq.input(`pm_${colName}`, sqlType, value);
                }
              };

              // Map either InvoiceNo, Date/InvoiceDate, PayMode, Amount, Commission, SlipNo
              addPmField("InvoiceNo", sql.VarChar, invNo);
              if (payModeCols.includes("invoicedate")) {
                addPmField("InvoiceDate", sql.SmallDateTime, header.InvoiceDate || curDate);
              } else if (payModeCols.includes("date")) {
                addPmField("Date", sql.SmallDateTime, header.InvoiceDate || curDate);
              } else {
                addPmField("InvoiceDate", sql.SmallDateTime, header.InvoiceDate || curDate);
              }

              if (payModeCols.includes("paymode")) {
                addPmField("PayMode", sql.VarChar, pm.PayMode);
              }

              addPmField("Amount", sql.Numeric(18, 2), parseFloat(pm.Amount) || 0);
              addPmField("Commission", sql.Numeric(18, 2), parseFloat(pm.Commission) || 0);
              addPmField("SlipNo", sql.VarChar, pm.SlipNo || "");
              addPmField("SalesType", sql.VarChar, salesTypeStr);

              if (payModeCols.includes("createdate")) {
                pCols.push("CreateDate");
                pVals.push("GETDATE()");
              }
              if (payModeCols.includes("createuser")) {
                addPmField("CreateUser", sql.VarChar, sysUser);
              } else if (payModeCols.includes("userid")) {
                addPmField("UserID", sql.VarChar, sysUser);
              }

              await directPmReq.query(`
                INSERT INTO tblPayModeDetail (${pCols.join(', ')})
                VALUES (${pVals.join(', ')})
              `);
            }
        }
    }

    await transaction.commit();
    return res.status(200).json({ 
      success: true, 
      message: "Invoice saved successfully!",
      invoiceNo: invNo 
    });
  } catch (err) {
    try {
      await transaction.rollback();
    } catch (rbErr) {}
    console.error("🔥 POST POS ERROR:", err);
    return res.status(500).json({ 
      success: false, 
      message: err.message || "Server Error", 
      error: err.message 
    });
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
            SELECT 
                CustomerCode, 
                CustomerName, 
                ISNULL(Address1, '') AS Address1, 
                ISNULL(Address2, '') AS Address2, 
                ISNULL(Address3, '') AS Address3, 
                ISNULL(NricNo, '') AS NricNo, 
                ISNULL(CustomerType, 'N') AS CustomerType
            FROM tblCustomer
            ORDER BY CustomerCode ASC
        `);
        return res.status(200).json({ success: true, data: result.recordset });
    } catch (err) {
        console.error("🔥 LOV Customers ERROR:", err);
        return res.status(500).json({ success: false, message: "Server Error", error: err.message });
    }
});

// ================= GET INVENTORY FOR POS =================
router.get("/lov/inventory", async (req, res) => {
    try {
        const pool = req.pool;
        const result = await pool.request().query(`
            SELECT TOP 300
                a.InventoryCode,
                ISNULL(a.Description, '') AS Description,
                ISNULL(a.ClassCode, '') AS ClassCode,
                ISNULL(a.Weight, 0) AS Weight,
                ISNULL(a.GoldLoss, 0) AS GoldLoss,
                ISNULL(a.CostPerGram, 0) AS CostPerGram,
                ISNULL(a.SMC, 0) AS SMC,
                ISNULL(a.PMC, 0) AS PMC,
                CAST(1 AS INT) AS PCS,
                ISNULL(a.TotalCost, 0) AS TotalCost,
                ISNULL(a.StoneWeight, 0) AS StoneWeight,
                ISNULL(c.Rate, a.CostPerGram) AS ClassRate,
                ISNULL(b.QtyOnHand, 1) AS QtyOnHand
            FROM tblInventory a
            LEFT JOIN tblInventoryStock b ON a.InventoryCode = b.InventoryCode
            LEFT JOIN tblClass c ON a.ClassCode = c.ClassCode
            WHERE (a.SalesBillNo IS NULL OR a.SalesBillNo = '' OR LTRIM(RTRIM(a.SalesBillNo)) = '')
            ORDER BY a.InventoryCode ASC
        `);
        return res.status(200).json({ success: true, data: result.recordset });
    } catch (err) {
        console.error("🔥 LOV Inventory ERROR:", err);
        return res.status(500).json({ success: false, message: "Server Error", error: err.message });
    }
});

// ================= GET PAYMODES LOV =================
router.get("/lov/paymodes", async (req, res) => {
    try {
        const pool = req.pool;
        const result = await pool.request().query(`
            SELECT PayMode, Description, ISNULL(Commission, 0) AS Commission 
            FROM tblPaymode 
            WHERE Active = 1 
            ORDER BY Position ASC
        `);
        return res.status(200).json({ success: true, data: result.recordset });
    } catch (err) {
        console.error("🔥 LOV Paymodes ERROR:", err);
        return res.status(500).json({ success: false, message: "Server Error", error: err.message });
    }
});

// ================= GET ADVANCE DEPOSITS LOV =================
router.get("/lov/adv-deposits", async (req, res) => {
    try {
        const pool = req.pool;
        const result = await pool.request().query(`
            SELECT CustomerCode, CustomerName, DepositNo, Amount
            FROM tblCustomerDeposit
            WHERE DepositUsed = 'N'
        `);
        return res.status(200).json({ success: true, data: result.recordset });
    } catch (err) {
        console.error("🔥 LOV Adv Deposits ERROR:", err);
        return res.status(500).json({ success: false, message: "Server Error", error: err.message });
    }
});

// ================= GET FLEXI SUBSCRIPTIONS LOV =================
router.get("/lov/flexi-subscriptions", async (req, res) => {
    try {
        const pool = req.pool;
        const result = await pool.request().query(`
            SELECT SubscriptionId, CustomerName, TotalAmount, TotalBonus
            FROM vwFlexiFinalClosure
            WHERE isProcessed = 'N'
        `);
        return res.status(200).json({ success: true, data: result.recordset });
    } catch (err) {
        // Fallback gracefully if view does not exist in schema
        return res.status(200).json({ success: true, data: [] });
    }
});

// ================= GET MEMBER DETAILS BY CODE =================
router.get("/member/:memberCode", async (req, res) => {
    try {
        const { memberCode } = req.params;
        const pool = req.pool;
        const result = await pool.request()
            .input("MemberCode", sql.VarChar, memberCode)
            .query(`
                SELECT MemberCode, MemberName, MemberType, Address1, Address2, Address3 
                FROM tblMember 
                WHERE MemberCode = @MemberCode
            `);
        if (result.recordset.length > 0) {
            return res.status(200).json({ success: true, data: result.recordset[0] });
        }
        return res.status(404).json({ success: false, message: "Member not found" });
    } catch (err) {
        return res.status(500).json({ success: false, message: err.message });
    }
});

// ================= GET AUDIT FOOTER STATS =================
router.get("/audit/stats", async (req, res) => {
    try {
        const pool = req.pool;

        let nextInvoiceNo = '';
        try {
            const nextInvResult = await pool.request().query(`
                SELECT TOP 1 NextInvoice FROM tblInvoiceNo WHERE Terminal = 'T1' OR LocationCode = 'HQ'
            `);
            if (nextInvResult.recordset.length > 0 && nextInvResult.recordset[0].NextInvoice) {
                nextInvoiceNo = nextInvResult.recordset[0].NextInvoice;
            }
        } catch (e) {
            // tblInvoiceNo may not exist; try tblCompany or MAX tblSalesHeader
            try {
                const compRes = await pool.request().query(`
                    SELECT TOP 1 NextInvoiceNo, NextSalesNo FROM tblCompany
                `);
                if (compRes.recordset.length > 0) {
                    nextInvoiceNo = compRes.recordset[0].NextInvoiceNo || compRes.recordset[0].NextSalesNo || '';
                }
            } catch (e2) {}
            
            if (!nextInvoiceNo) {
                try {
                    const maxRes = await pool.request().query(`
                        SELECT TOP 1 InvoiceNo FROM tblSalesHeader WHERE ISNUMERIC(InvoiceNo) = 1 ORDER BY CAST(InvoiceNo AS BIGINT) DESC
                    `);
                    if (maxRes.recordset.length > 0 && maxRes.recordset[0].InvoiceNo) {
                        nextInvoiceNo = String(parseInt(maxRes.recordset[0].InvoiceNo, 10) + 1);
                    }
                } catch (e3) {}
            }
        }

        const lastBillResult = await pool.request().query(`
            SELECT TOP 1 InvoiceNo, SalesManCode, NetTotal 
            FROM tblSalesHeader 
            ORDER BY CreateDate DESC
        `);
        const lastBill = lastBillResult.recordset.length > 0 ? lastBillResult.recordset[0] : {};

        return res.status(200).json({
            success: true,
            data: {
                LocationCode: 'HQ',
                TerminalCode: 'T1',
                NextInvoiceNo: nextInvoiceNo || '',
                LastSalesMan: lastBill.SalesManCode || '0',
                LastBillNo: lastBill.InvoiceNo || '0',
                LastBillAmount: lastBill.NetTotal ? Number(lastBill.NetTotal).toFixed(2) : '0.00'
            }
        });
    } catch (err) {
        return res.status(500).json({ success: false, message: err.message });
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
router.get("/debug-schema", async (req, res) => {
  try {
    const pool = req.pool;
    const headerCols = await pool.request().query("SELECT column_name, data_type FROM information_schema.columns WHERE table_name = 'tblSalesHeader'");
    const detailCols = await pool.request().query("SELECT column_name, data_type FROM information_schema.columns WHERE table_name = 'tblSalesDetail'");
    const payCols = await pool.request().query("SELECT column_name, data_type FROM information_schema.columns WHERE table_name = 'tblPayModeDetail'");
    const spHeader = await pool.request().query("SELECT parameter_name, data_type FROM information_schema.parameters WHERE specific_name = 'sp_SalesHeader'");
    const spDetail = await pool.request().query("SELECT parameter_name, data_type FROM information_schema.parameters WHERE specific_name = 'sp_SalesDetail'");
    const spPay = await pool.request().query("SELECT parameter_name, data_type FROM information_schema.parameters WHERE specific_name = 'sp_PayModeDetail'");
    return res.json({
      headerCols: headerCols.recordset,
      detailCols: detailCols.recordset,
      payCols: payCols.recordset,
      spHeader: spHeader.recordset,
      spDetail: spDetail.recordset,
      spPay: spPay.recordset
    });
  } catch (err) {
    return res.status(500).json({ error: err.message });
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
