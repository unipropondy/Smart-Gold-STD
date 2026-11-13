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

    // Validations matching frmgpos.vb fncValidate
    if (!header.SalesManCode || !String(header.SalesManCode).trim()) {
      return res.status(400).json({ success: false, message: "Validation Error: Please select or authorize Salesman" });
    }
    if (!header.CustomerCode || !String(header.CustomerCode).trim()) {
      return res.status(400).json({ success: false, message: "Validation Error: Please enter Customer!" });
    }
    if (!details || !Array.isArray(details) || details.length === 0) {
      return res.status(400).json({ success: false, message: "Validation Error: Enter items for sales!" });
    }
    const totalSalesNum = parseFloat(header.TotalSalesAmt !== undefined ? header.TotalSalesAmt : header.GrandTotal) || 0;
    if (totalSalesNum < 0) {
      return res.status(400).json({ success: false, message: "Validation Error: Can not do negative sales." });
    }

    // Line items validation
    for (let i = 0; i < details.length; i++) {
      const itm = details[i];
      const invCode = (itm.InventoryCode || "").trim();
      if (!invCode) {
        return res.status(400).json({ success: false, message: `Validation Error: Item at line ${i + 1} has no Inventory Code!` });
      }
      const pcsVal = parseInt(itm.PCS, 10) || 0;
      if (pcsVal < 1) {
        return res.status(400).json({ success: false, message: `Validation Error: PCS should be greater than or equal to one for item ${invCode}!` });
      }
      const isPcsSales = itm.ClassSalesType === 'P' || (itm.Class && String(itm.Class).toUpperCase() === 'P');
      if (!isPcsSales) {
        const wt = parseFloat(itm.Weight) || 0;
        if (wt <= 0) {
          return res.status(400).json({ success: false, message: `Validation Error: Weight must be greater than 0 for item ${invCode}!` });
        }
      }
    }

    const sysUser = userId || "SYSTEM";
    const curDate = new Date();

    await transaction.begin();

    // Format and validate InvoiceNo
    let invNo = header.InvoiceNo ? String(header.InvoiceNo).trim() : '';
    let locCode = 'HQ';
    try {
      const compLoc = await new sql.Request(transaction).query("SELECT TOP 1 LocationCode FROM tblCompany");
      if (compLoc.recordset.length > 0 && compLoc.recordset[0].LocationCode) {
        locCode = String(compLoc.recordset[0].LocationCode).trim();
      }
    } catch (_) {}

    if (!invNo || invNo.startsWith('INV-')) {
      // 1. Try tblCompany (e.g. NextInvoice, NextInvoiceNo or NextSalesNo)
      let foundNextNo = null;
      try {
        const compRes = await new sql.Request(transaction).query(`
          SELECT TOP 1 LocationCode, NextInvoice, NextInvoiceNo, NextSalesNo, NextSalesBillNo FROM tblCompany
        `);
        if (compRes.recordset.length > 0) {
          const row = compRes.recordset[0];
          if (row.LocationCode) locCode = String(row.LocationCode).trim();
          foundNextNo = row.NextInvoice || row.NextInvoiceNo || row.NextSalesNo || row.NextSalesBillNo;
          if (foundNextNo) {
            const numPart = parseInt(String(foundNextNo).replace(/\D/g, ''), 10);
            if (!isNaN(numPart)) {
              await new sql.Request(transaction).query(`
                UPDATE tblCompany 
                SET NextInvoice = ${numPart + 1},
                    NextInvoiceNo = ${numPart + 1}
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
            SELECT TOP 1 LocationCode, NextInvoice FROM tblInvoiceNo WHERE Terminal = 'T1' OR LocationCode = '${locCode}'
          `);
          if (invNumRes.recordset.length > 0 && invNumRes.recordset[0].NextInvoice) {
            if (invNumRes.recordset[0].LocationCode) locCode = String(invNumRes.recordset[0].LocationCode).trim();
            foundNextNo = invNumRes.recordset[0].NextInvoice;
            const numPart = parseInt(String(foundNextNo).replace(/\D/g, ''), 10);
            if (!isNaN(numPart)) {
              await new sql.Request(transaction).query(`
                UPDATE tblInvoiceNo SET NextInvoice = '${numPart + 1}' WHERE Terminal = 'T1' OR LocationCode = '${locCode}'
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
            SELECT TOP 1 InvoiceNo FROM tblSalesHeader ORDER BY CreateDate DESC
          `);
          if (maxRes.recordset.length > 0 && maxRes.recordset[0].InvoiceNo) {
            const maxVal = parseInt(String(maxRes.recordset[0].InvoiceNo).replace(/\D/g, ''), 10);
            foundNextNo = String(maxVal + 1);
          }
        } catch (mErr) {}
      }

      if (foundNextNo) {
        const strVal = String(foundNextNo).trim();
        if (strVal.startsWith(locCode)) {
          const numPart = strVal.slice(locCode.length).replace(/\D/g, '');
          invNo = `${locCode}${numPart.padStart(8, '0')}`;
        } else {
          const numPart = strVal.replace(/\D/g, '');
          invNo = `${locCode}${(numPart || '1').padStart(8, '0')}`;
        }
      } else {
        invNo = `${locCode}00000001`;
      }
    } else {
      // User passed an InvoiceNo; ensure it is properly formatted with locationCode prefix and 8 digits
      if (!invNo.startsWith(locCode)) {
        const numPart = invNo.replace(/\D/g, '');
        invNo = `${locCode}${(numPart || '1').padStart(8, '0')}`;
      }
      // Increment NextInvoice in tblCompany so the next bill does not collide
      try {
        const currentNum = parseInt(invNo.slice(locCode.length).replace(/\D/g, ''), 10);
        if (!isNaN(currentNum)) {
          await new sql.Request(transaction).query(`
            UPDATE tblCompany 
            SET NextInvoice = ${currentNum + 1},
                NextInvoiceNo = ${currentNum + 1}
          `);
        }
      } catch (_) {}
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

    const reqPaidAmt = parseFloat(req.body.paidAmount !== undefined ? req.body.paidAmount : header.GrandTotal) || 0;
    const reqBalanceAmt = parseFloat(req.body.balance !== undefined ? req.body.balance : header.BalanceAmt) || 0;
    const reqCommissionAmt = parseFloat(req.body.commission !== undefined ? req.body.commission : header.Commission) || 0;
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
          .input("PaidAmt", sql.Numeric(18, 2), reqPaidAmt)
          .input("BalanceAmt", sql.Numeric(18, 2), reqBalanceAmt)
          .input("TotalValue", sql.Numeric(18, 2), parseFloat(header.TotalSalesAmt) || 0)
          .input("TotalCost", sql.Numeric(18, 2), totalCost || parseFloat(header.TotalCost) || 0)
          .input("TradeInAmt", sql.Numeric(18, 2), tradeInAmt || parseFloat(header.TradeInAmt) || 0)
          .input("DepositAmt", sql.Numeric(18, 2), parseFloat(header.DepositAmt) || 0)
          .input("InsAgreementNo", sql.VarChar, header.InsAgreementNo || "")
          .input("Commission", sql.Numeric(18, 2), reqCommissionAmt)
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
      addField("PaidAmt", sql.Numeric(18, 2), reqPaidAmt);
      addField("BalanceAmt", sql.Numeric(18, 2), reqBalanceAmt);
      addField("Commission", sql.Numeric(18, 2), reqCommissionAmt);
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
      addField("CreatedBy", sql.VarChar, sysUser);
      addField("ModifyUser", sql.VarChar, sysUser);
      if (hasCol("ModifyDate")) {
        cols.push("ModifyDate");
        vals.push("GETDATE()");
      }

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
            .input("LocationCode", sql.VarChar, header.LocationCode || locCode)
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
            .input("DailyClassRate", sql.Numeric(18, 2), parseFloat(d.DailyClassRate || d.DailyRate || d.CostPerGram) || 0)
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
        addDetField("LocationCode", sql.VarChar, header.LocationCode || locCode);
        addDetField("Weight", sql.Numeric(18, 3), parseFloat(d.Weight) || 0);
        addDetField("StoneWeight", sql.Numeric(18, 3), parseFloat(d.StoneWeight) || 0);
        addDetField("GoldLoss", sql.Numeric(18, 3), parseFloat(d.GoldLoss) || 0);
        addDetField("Rate", sql.Numeric(18, 2), parseFloat(d.CostPerGram) || 0);
        addDetField("DailyClassRate", sql.Numeric(18, 2), parseFloat(d.DailyClassRate || d.DailyRate || d.CostPerGram) || 0);
        addDetField("DailyRate", sql.Numeric(18, 2), parseFloat(d.DailyRate || d.DailyClassRate || d.CostPerGram) || 0);
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

        addDetField("CreateUser", sql.VarChar, sysUser);
        addDetField("CreatedBy", sql.VarChar, sysUser);
        addDetField("ModifyUser", sql.VarChar, sysUser);
        if (hasDetCol("CreateDate")) {
          dCols.push("CreateDate");
          dVals.push("GETDATE()");
        }
        if (hasDetCol("ModifyDate")) {
          dCols.push("ModifyDate");
          dVals.push("GETDATE()");
        }

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

      // 1. Mandatory stock deduction in tblInventoryStock - Must succeed or throw
      const stockReq = new sql.Request(transaction);
      const stockUpdateResult = await stockReq
        .input("InventoryCode", sql.VarChar, d.InventoryCode)
        .input("PCS", sql.Numeric(18, 0), pcsVal)
        .input("Weight", sql.Numeric(18, 3), parseFloat(d.Weight) || 0)
        .query(`
          UPDATE tblInventoryStock 
          SET QtyOnHand = CASE WHEN ISNULL(QtyOnHand, 0) - @PCS < 0 THEN 0 ELSE ISNULL(QtyOnHand, 0) - @PCS END,
              QtySold = ISNULL(QtySold, 0) + @PCS,
              Weight = CASE WHEN ISNULL(Weight, 0) - @Weight < 0 THEN 0 ELSE ISNULL(Weight, 0) - @Weight END,
              ModifyDate = GETDATE()
          WHERE UPPER(LTRIM(RTRIM(InventoryCode))) = UPPER(LTRIM(RTRIM(@InventoryCode)))
        `);

      if (stockUpdateResult.rowsAffected && stockUpdateResult.rowsAffected[0] === 0) {
        // If row didn't exist in tblInventoryStock, insert it to keep ledger consistent
        try {
          const insertStockReq = new sql.Request(transaction);
          await insertStockReq
            .input("InventoryCode", sql.VarChar, d.InventoryCode)
            .input("PCS", sql.Numeric(18, 0), pcsVal)
            .input("Weight", sql.Numeric(18, 3), parseFloat(d.Weight) || 0)
            .query(`
              INSERT INTO tblInventoryStock (InventoryCode, QtyOnHand, QtySold, Weight, ModifyDate)
              VALUES (@InventoryCode, 0, @PCS, 0, GETDATE())
            `);
        } catch (isErr) {
          console.warn("tblInventoryStock initial record create:", isErr.message);
        }
      }

      // 2. Mandatory inventory movement record matching InventoryMovementSave (Status: 'SA')
      const mvReq = new sql.Request(transaction);
      await mvReq
        .input("LocationCode", sql.VarChar, header.LocationCode || "HQ")
        .input("InventoryCode", sql.VarChar, d.InventoryCode)
        .input("Description", sql.VarChar, d.Description || "")
        .input("RefNumber", sql.VarChar, invNo)
        .input("RefDate", sql.SmallDateTime, header.InvoiceDate || curDate)
        .input("PCS", sql.Numeric(18, 0), pcsVal)
        .input("Weight", sql.Numeric(18, 3), parseFloat(d.Weight) || 0)
        .input("PMC", sql.Numeric(18, 2), parseFloat(d.SMC) || 0)
        .input("CostPerGram", sql.Numeric(18, 2), parseFloat(d.CostPerGram) || 0)
        .input("TotalCost", sql.Numeric(18, 2), itemVal)
        .input("CustomerCode", sql.VarChar, header.CustomerCode || "CASH")
        .input("CreateUser", sql.VarChar, sysUser)
        .query(`
          INSERT INTO tblInventoryMovement (
            LocationCode, InventoryCode, Description, Status, RefNumber, RefDate,
            Qty, Weight, PMC, CostPerGram, TotalCost, SupplierCode, CreateUser, CreateDate
          ) VALUES (
            @LocationCode, @InventoryCode, @Description, 'SA', @RefNumber, @RefDate,
            @PCS, @Weight, @PMC, @CostPerGram, @TotalCost, @CustomerCode, @CreateUser, GETDATE()
          )
        `);

      // 3. RFID item status update (matching RFIDItemStatusUpdate: ToDisplayInStockTake = '0', Status = 'SALES')
      try {
        await new sql.Request(transaction)
          .input("ItemCode", sql.VarChar, d.InventoryCode)
          .query(`
            UPDATE Item 
            SET ToDisplayInStockTake = '0', Status = 'SALES' 
            WHERE ItemCode = @ItemCode
          `);
      } catch (rfidErr) {
        // RFID database or Item table is optional / secondary in certain configurations
      }
    }

    // Insert Payment Modes
    const rawPayModes = req.body.payModes || req.body.paymodeDetails || req.body.payments || [{ PayMode: 'CASH', Amount: header.GrandTotal }];
    const payModes = Array.isArray(rawPayModes) ? rawPayModes : [];
    for (let i = 0; i < payModes.length; i++) {
        const pm = payModes[i];
        const pmMode = pm.PayMode || pm.paymode || 'CASH';
        const pmAmt = parseFloat(pm.Amount !== undefined ? pm.Amount : pm.amount) || 0;
        const pmComm = parseFloat(pm.Commission !== undefined ? pm.Commission : pm.commission) || 0;
        const pmSlip = pm.SlipNo || pm.slipNo || pm.remarks || "";

        if (pmAmt > 0) {
            let pmSuccess = false;
            if (!useDirectInsert) {
              try {
                const pmReq = new sql.Request(transaction);
                await pmReq
                    .input("InvoiceNo", sql.VarChar, invNo)
                    .input("Date", sql.SmallDateTime, header.InvoiceDate || curDate)
                    .input("Paymode", sql.VarChar, pmMode)
                    .input("Amount", sql.Numeric(18, 2), pmAmt)
                    .input("Commission", sql.Numeric(18, 2), pmComm)
                    .input("SlipNo", sql.VarChar, pmSlip)
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
                addPmField("PayMode", sql.VarChar, pmMode);
              }

              addPmField("Amount", sql.Numeric(18, 2), pmAmt);
              addPmField("Commission", sql.Numeric(18, 2), pmComm);
              addPmField("SlipNo", sql.VarChar, pmSlip);
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

    // ================= ADVANCE DEPOSIT REDEMPTION (Matching objSales.fncUpdateAdvDeposit) =================
    if (header.Advance && String(header.Advance).trim() !== '' && parseFloat(header.Advance) > 0) {
      try {
        const advDepNo = (header.AdvDepositNo || header.DepositNo || '').trim();
        const advReq = new sql.Request(transaction);
        advReq.input("InvoiceNo", sql.VarChar, invNo);
        advReq.input("SalesInvoiceDate", sql.SmallDateTime, header.InvoiceDate || curDate);
        advReq.input("UserID", sql.VarChar, sysUser);

        if (advDepNo) {
          advReq.input("DepositNo", sql.VarChar, advDepNo);
          await advReq.query(`
            UPDATE tblCustomerDeposit 
            SET DepositUsed = 'Y', InvoiceNo = @InvoiceNo, InvoiceDate = @SalesInvoiceDate,
                ModifiedBy = @UserID, ModifiedDate = GETDATE()
            WHERE DepositNo = @DepositNo
          `);
        } else if (header.CustomerCode) {
          advReq.input("CustomerCode", sql.VarChar, header.CustomerCode.trim());
          await advReq.query(`
            UPDATE TOP (1) tblCustomerDeposit 
            SET DepositUsed = 'Y', InvoiceNo = @InvoiceNo, InvoiceDate = @SalesInvoiceDate,
                ModifiedBy = @UserID, ModifiedDate = GETDATE()
            WHERE CustomerCode = @CustomerCode AND DepositUsed = 'N'
          `);
        }
      } catch (advErr) {
        console.warn("Advance deposit update warning:", advErr.message);
      }
    }

    // ================= FLEXI SUBSCRIPTION REDEMPTION (Matching objSales.fncUpdatePayModeFlexiClosure) =================
    if ((header.FlexiAmount && parseFloat(header.FlexiAmount) > 0) || (header.FlexiBonus && parseFloat(header.FlexiBonus) > 0) || header.SubscriptionID) {
      try {
        const subId = String(header.SubscriptionID || '').trim();
        if (subId) {
          const flexiReq = new sql.Request(transaction);
          flexiReq.input("SubscriptionID", sql.VarChar, subId);
          flexiReq.input("LocationCode", sql.VarChar, header.LocationCode || "HQ");
          await flexiReq.query(`
            UPDATE tblPayModeDetailForFlexiClosure 
            SET IsProcessed = 'Y', ModifyDate = GETDATE()
            WHERE SubscriptionID = @SubscriptionID AND LocationCode = @LocationCode
          `);
        }
      } catch (flexiErr) {
        console.warn("Flexi subscription closure update warning:", flexiErr.message);
      }
    }

    // ================= MEMBER TRANSACTION SAVE (Matching ClsSales.MemberTransactionSave) =================
    if (header.CustomerType === 'M' || (String(header.CustomerCode).trim().toUpperCase() === 'CASH' && header.MemberCode && String(header.MemberCode).trim() !== '')) {
      try {
        const memberCode = (header.MemberCode || '').trim();
        if (memberCode) {
          const discountAmt = parseFloat(header.Discount) || 0;
          const netDiscountAmt = parseFloat(header.NetDiscount) || 0;
          const netTot = parseFloat(header.GrandTotal) || 0;
          const memReq = new sql.Request(transaction);
          await memReq
            .input("MemberCode", sql.VarChar, memberCode)
            .input("InvoiceNo", sql.VarChar, invNo)
            .input("Date", sql.SmallDateTime, header.InvoiceDate || curDate)
            .input("DiscountSalesAmount", sql.Numeric(18, 2), discountAmt)
            .input("MemberDiscount", sql.Numeric(18, 2), netDiscountAmt)
            .input("NetTotal", sql.Numeric(18, 2), netTot)
            .input("LocationCode", sql.VarChar, header.LocationCode || "HQ")
            .input("UserID", sql.VarChar, sysUser)
            .execute("sp_MemberTransaction");
        }
      } catch (memErr) {
        console.warn("MemberTransactionSave sp_MemberTransaction warning:", memErr.message);
      }
    }

    // ================= TRADE-IN SALES UPDATE (Matching fncUpdateTradeInRefNo) =================
    if (header.TradeInRefNo || salesTypeStr === 'TRADEIN') {
      try {
        const tradeInRefNo = (header.TradeInRefNo || '').trim();
        if (tradeInRefNo) {
          const tiInvNo = `TI-${invNo}`;
          const tiOldInvNo = `TI-${tradeInRefNo}`;
          await new sql.Request(transaction)
            .input("NewInvNo", sql.VarChar, invNo)
            .input("NewTIInvNo", sql.VarChar, tiInvNo)
            .input("TradeInRefNo", sql.VarChar, tradeInRefNo)
            .input("OldTIInvNo", sql.VarChar, tiOldInvNo)
            .query(`
              UPDATE tblTradeInHeader 
              SET InventoryCode = @NewTIInvNo, InvoiceNo = @NewInvNo 
              WHERE InvoiceNo = @TradeInRefNo;

              UPDATE tblTradeInDetail 
              SET InventoryCode = @NewTIInvNo 
              WHERE InventoryCode = @OldTIInvNo;
            `);
        }
      } catch (tiErr) {
        console.warn("TradeInRefNo update warning:", tiErr.message);
      }
    }

    // ================= INSTALLMENT REF UPDATE (Matching fncUpdateInstallmentRefNo) =================
    if (header.InsAgreementNo && String(header.InsAgreementNo).trim() !== '') {
      try {
        const insAgrNo = String(header.InsAgreementNo).trim();
        await new sql.Request(transaction)
          .input("InvoiceNo", sql.VarChar, invNo)
          .input("InsAgreementNo", sql.VarChar, insAgrNo)
          .query(`
            UPDATE tblInstallmentHeader 
            SET InvoiceNo = @InvoiceNo 
            WHERE InsAgreementNo = @InsAgreementNo
          `);
      } catch (insErr) {
        console.warn("InstallmentHeader update warning:", insErr.message);
      }
    }

    // ================= COST CHANGE LOG SAVE (Matching ClsSales.fncUpdateCostChangeLogSave) =================
    if (header.CostChangeSalesman && String(header.CostChangeSalesman).trim() !== '') {
      try {
        const costChangeReq = new sql.Request(transaction);
        await costChangeReq
          .input("InvoiceNo", sql.VarChar, invNo)
          .input("InvoiceDate", sql.SmallDateTime, header.InvoiceDate || curDate)
          .input("SalesmanName", sql.VarChar, header.SalesManName || header.SalesManCode || "")
          .input("CostChangeSalesmanName", sql.VarChar, String(header.CostChangeSalesman).trim())
          .input("UserID", sql.VarChar, sysUser)
          .execute("sp_CostChangeLogSave");
      } catch (cclErr) {
        console.warn("sp_CostChangeLogSave warning:", cclErr.message);
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

// ================= DELETE INVOICE (Matching objSalesReturn.BillCancel, sp_SalesBillCancel, sp_SalesBillCancelCustomerUpdate) =================
router.delete("/:invoiceNo", async (req, res) => {
  const pool = req.pool;
  const transaction = new sql.Transaction(pool);

  try {
    const { invoiceNo } = req.params;
    const sysUser = req.query.userId || req.body?.userId || "SYSTEM";
    const locCode = req.query.locationCode || req.body?.locationCode || "HQ";

    await transaction.begin();

    // 1. Fetch line items of this invoice to restore stock and record reversal movements
    const getItemsReq = new sql.Request(transaction);
    const itemsRes = await getItemsReq
      .input("InvoiceNo", sql.VarChar, invoiceNo)
      .query(`
        SELECT InventoryCode, Description, PCS, Weight, SMC, Rate, CostPerGram, TotalValue 
        FROM tblSalesDetail 
        WHERE InvoiceNo = @InvoiceNo
      `);

    const invoiceItems = itemsRes.recordset;

    // 2. Execute original stored procedures if available: sp_SalesBillCancelCustomerUpdate & sp_SalesBillCancel
    try {
      const spCustReq = new sql.Request(transaction);
      await spCustReq
        .input("InvoiceNo", sql.VarChar, invoiceNo)
        .execute("sp_SalesBillCancelCustomerUpdate");

      const spCancelReq = new sql.Request(transaction);
      await spCancelReq
        .input("InvoiceNo", sql.VarChar, invoiceNo)
        .input("LocationCode", sql.VarChar, locCode)
        .input("UserID", sql.VarChar, sysUser)
        .execute("sp_SalesBillCancel");
    } catch (spErr) {
      console.warn("⚠️ Stored procedures sp_SalesBillCancel / CustomerUpdate fallback to standard cancellation:", spErr.message);
    }

    // 3. Restore Stock in tblInventoryStock and write 'SR' (Sales Return / Cancel) Movement record for each item
    for (const item of invoiceItems) {
      const invCode = (item.InventoryCode || '').trim();
      if (invCode) {
        const pcsVal = parseFloat(item.PCS) || 1;
        const wtVal = parseFloat(item.Weight) || 0;

        // Restore QtyOnHand and decrement QtySold in tblInventoryStock
        const restoreStockReq = new sql.Request(transaction);
        await restoreStockReq
          .input("InventoryCode", sql.VarChar, invCode)
          .input("PCS", sql.Numeric(18, 0), pcsVal)
          .input("Weight", sql.Numeric(18, 3), wtVal)
          .query(`
            UPDATE tblInventoryStock 
            SET QtyOnHand = ISNULL(QtyOnHand, 0) + @PCS,
                QtySold = CASE WHEN ISNULL(QtySold, 0) - @PCS < 0 THEN 0 ELSE ISNULL(QtySold, 0) - @PCS END,
                Weight = ISNULL(Weight, 0) + @Weight,
                ModifyDate = GETDATE()
            WHERE UPPER(LTRIM(RTRIM(InventoryCode))) = UPPER(LTRIM(RTRIM(@InventoryCode)))
          `);

        // Record reversal movement in tblInventoryMovement (Status: 'SR')
        try {
          const mvReq = new sql.Request(transaction);
          await mvReq
            .input("LocationCode", sql.VarChar, locCode)
            .input("InventoryCode", sql.VarChar, invCode)
            .input("Description", sql.VarChar, item.Description || "")
            .input("RefNumber", sql.VarChar, invoiceNo)
            .input("PCS", sql.Numeric(18, 0), pcsVal)
            .input("Weight", sql.Numeric(18, 3), wtVal)
            .input("PMC", sql.Numeric(18, 2), parseFloat(item.SMC) || 0)
            .input("CostPerGram", sql.Numeric(18, 2), parseFloat(item.CostPerGram || item.Rate) || 0)
            .input("TotalCost", sql.Numeric(18, 2), parseFloat(item.TotalValue) || 0)
            .input("CreateUser", sql.VarChar, sysUser)
            .query(`
              INSERT INTO tblInventoryMovement (
                LocationCode, InventoryCode, Description, Status, RefNumber, RefDate,
                Qty, Weight, PMC, CostPerGram, TotalCost, CreateUser, CreateDate
              ) VALUES (
                @LocationCode, @InventoryCode, @Description, 'SR', @RefNumber, GETDATE(),
                @PCS, @Weight, @PMC, @CostPerGram, @TotalCost, @CreateUser, GETDATE()
              )
            `);
        } catch (_) {}
      }
    }

    // 4. Mark tblInventory unsold
    const clearInvReq = new sql.Request(transaction);
    await clearInvReq
      .input("InvoiceNo", sql.VarChar, invoiceNo)
      .query(`
        UPDATE tblInventory 
        SET SalesBillNo = '', SalesDate = NULL, SalesCostPerGram = 0, SalesMC = 0, SalesAmount = 0 
        WHERE SalesBillNo = @InvoiceNo
      `);

    // 5. Restore redeemed deposits or flexi closures used on this invoice
    try {
      const restDepReq = new sql.Request(transaction);
      await restDepReq
        .input("InvoiceNo", sql.VarChar, invoiceNo)
        .query(`
          UPDATE tblCustomerDeposit 
          SET DepositUsed = 'N', InvoiceNo = '', InvoiceDate = NULL 
          WHERE InvoiceNo = @InvoiceNo
        `);
    } catch (_) {}

    // 6. Delete bill records
    const delSalesReq = new sql.Request(transaction);
    await delSalesReq
      .input("InvoiceNo", sql.VarChar, invoiceNo)
      .query(`
        DELETE FROM tblSalesHeader WHERE InvoiceNo = @InvoiceNo;
        DELETE FROM tblSalesDetail WHERE InvoiceNo = @InvoiceNo;
        DELETE FROM tblPaymodeDetail WHERE InvoiceNo = @InvoiceNo;
      `);

    await transaction.commit();
    return res.status(200).json({ success: true, message: `Invoice ${invoiceNo} cancelled and stock successfully restored!` });
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
// Matches VB.NET frmGPOS.vb: subShowClassMarketRate (SELECT * FROM tblClass where DisplayCost = 'Y')
// and tblClassRate for current date if available
router.get("/lov/market-rates", async (req, res) => {
    try {
        const pool = req.pool;
        const curDate = new Date().toISOString().slice(0, 10);
        
        // As per subShowClassMarketRate in frmGPOS.vb:
        // Reads from tblClass (where DisplayCost = 'Y' or all active classes)
        // and checks if tblClassRate has a rate for today; otherwise uses tblClass.Rate
        const result = await pool.request()
            .input("SalesDate", sql.Date, curDate)
            .query(`
                SELECT 
                    c.ClassCode,
                    ISNULL(cr.Rate, ISNULL(c.Rate, 0.00)) AS Rate
                FROM tblClass c
                LEFT JOIN tblClassRate cr ON c.ClassCode = cr.ClassCode AND @SalesDate BETWEEN cr.FromDate AND cr.ToDate
                WHERE ISNULL(c.DisplayCost, 'Y') = 'Y'
                ORDER BY c.ClassCode ASC
            `);

        if (result.recordset.length === 0) {
            // Fallback to all from tblClass
            const fallback = await pool.request().query(`
                SELECT ClassCode, ISNULL(Rate, 0.00) AS Rate
                FROM tblClass
                ORDER BY ClassCode ASC
            `);
            return res.status(200).json({ success: true, data: fallback.recordset });
        }
            
        return res.status(200).json({ success: true, data: result.recordset });
    } catch (err) {
        console.error("🔥 All Market Rates ERROR:", err);
        try {
            // Fallback direct query on tblClass
            const pool = req.pool;
            const fallback = await pool.request().query(`
                SELECT ClassCode, ISNULL(Rate, 0.00) AS Rate
                FROM tblClass
                ORDER BY ClassCode ASC
            `);
            return res.status(200).json({ success: true, data: fallback.recordset });
        } catch (fbErr) {
            return res.status(500).json({ success: false, message: "Server Error", error: err.message });
        }
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

// ================= GET CUSTOMER BY CODE =================
router.get("/lov/customerByCode", async (req, res) => {
    try {
        const { code } = req.query;
        if (!code || !code.trim()) {
            return res.status(400).json({ success: false, message: "Customer code required" });
        }
        const pool = req.pool;
        const result = await pool.request()
            .input("CustomerCode", sql.VarChar, code.trim())
            .query(`
                SELECT TOP 1
                    CustomerCode, 
                    CustomerName, 
                    ISNULL(Address1, '') AS Address1, 
                    ISNULL(Address2, '') AS Address2, 
                    ISNULL(Address3, '') AS Address3, 
                    ISNULL(NricNo, '') AS NricNo, 
                    ISNULL(CustomerType, 'N') AS CustomerType
                FROM tblCustomer
                WHERE CustomerCode = @CustomerCode
            `);
        if (result.recordset.length > 0) {
            return res.json({ success: true, data: result.recordset[0] });
        }
        return res.json({ success: false, message: "Customer Not Found" });
    } catch (err) {
        return res.status(500).json({ success: false, message: "Server Error", error: err.message });
    }
});

// ================= GET INVENTORY DETAIL FOR CODE (Matches txtInventoryCode_LostFocus & fncGetInventory in frmgpos.vb) =================
router.get("/inventoryDetail", async (req, res) => {
  try {
    const pool = req.pool;
    const { InventoryCode, CustomerCode, LocationCode } = req.query;

    if (!InventoryCode || !InventoryCode.trim()) {
      return res.status(400).json({ success: false, message: "InventoryCode is required." });
    }

    const trimmedCode = InventoryCode.trim();

    const result = await pool.request()
      .input("InventoryCode", sql.VarChar, trimmedCode)
      .query(`
        SELECT TOP 1
          a.InventoryCode,
          ISNULL(a.Description, '') AS Description,
          ISNULL(a.ClassCode, '') AS ClassCode,
          ISNULL(a.Weight, 0) AS Weight,
          ISNULL(a.GoldLoss, 0) AS GoldLoss,
          ISNULL(a.CostPerGram, 0) AS CostPerGram,
          ISNULL(a.SMC, 0) AS SMC,
          ISNULL(a.PMC, 0) AS PMC,
          ISNULL(b.PCs, ISNULL(b.QtyOnHand, 1)) AS PCS,
          ISNULL(a.TotalCost, 0) AS TotalCost,
          ISNULL(a.StoneWeight, 0) AS StoneWeight,
          ISNULL(a.LocationCode, 'HQ') AS LocationCode,
          ISNULL(a.SalesBillNo, '') AS SalesBillNo,
          a.SalesDate,
          ISNULL(a.OrderNo, '') AS OrderNo,
          ISNULL(b.QtyOnHand, 1) AS QtyOnHand,
          ISNULL(b.QtySold, 0) AS QtySold,
          ISNULL(c.SalesType, 'W') AS ClassSalesType,
          ISNULL(c.Rate, a.CostPerGram) AS ClassRate,
          ISNULL(c.MCDiscount, 0) AS MCDiscount
        FROM tblInventory a
        LEFT JOIN tblInventoryStock b ON a.InventoryCode = b.InventoryCode
        LEFT JOIN tblClass c ON a.ClassCode = c.ClassCode
        WHERE UPPER(LTRIM(RTRIM(a.InventoryCode))) = UPPER(LTRIM(RTRIM(@InventoryCode)))
      `);

    if (result.recordset.length === 0) {
      return res.status(200).json({ success: false, message: "Inventory Not Found ..." });
    }

    const item = result.recordset[0];

    // 1. Check stock on hand (Matching frmGPOS.vb: Val(dsInv.Tables(0).Rows(0)("QtyOnHand")) < 1)
    const qtyOnHand = item.QtyOnHand !== null && item.QtyOnHand !== undefined ? parseFloat(item.QtyOnHand) : null;
    if (qtyOnHand !== null && qtyOnHand < 1) {
      return res.status(200).json({ success: false, message: "No Stock for this Inventory!" });
    }

    // 2. Check if already sold
    const isSold = (item.SalesBillNo && item.SalesBillNo.trim() !== '' && item.SalesBillNo.trim() !== '0');
    if (isSold) {
      let dateStr = '';
      if (item.SalesDate) {
        try {
          const d = new Date(item.SalesDate);
          dateStr = ` on ${String(d.getDate()).padStart(2, '0')}/${String(d.getMonth() + 1).padStart(2, '0')}/${d.getFullYear()}`;
        } catch (_) {}
      }
      return res.status(200).json({
        success: false,
        message: `Item Already Sold for Invoice ${item.SalesBillNo || ''}${dateStr}`.trim()
      });
    }

    // 3. Location check
    if (LocationCode && item.LocationCode && item.LocationCode.trim().toUpperCase() !== LocationCode.trim().toUpperCase()) {
      return res.status(200).json({
        success: false,
        message: `The selected Inventory belongs to the Location - ${item.LocationCode}!`
      });
    }

    // 4. Installment status check (checks tblInstallmentEntryDetail matching frmgpos.vb)
    try {
      const insRes = await pool.request()
        .input("InventoryCode", sql.VarChar, trimmedCode)
        .query(`SELECT TOP 1 InsAgreementNo FROM tblInstallmentEntryDetail WHERE UPPER(LTRIM(RTRIM(InventoryCode))) = UPPER(LTRIM(RTRIM(@InventoryCode)))`);
      if (insRes.recordset.length > 0) {
        return res.status(200).json({
          success: false,
          message: `This Inventory is in Installment Status! ${insRes.recordset[0].InsAgreementNo || ''}`.trim()
        });
      }
    } catch (_) {
      // If table doesn't exist or query fails, skip gracefully
    }

    // 5. Check if ordered for some other customer
    if (item.OrderNo && item.OrderNo.trim() !== '') {
      try {
        const orderRes = await pool.request()
          .input("OrderNo", sql.VarChar, item.OrderNo.trim())
          .query(`
            SELECT TOP 1 CustomerCode, RateFixed, CostPerGram 
            FROM tblInventoryOrderHeader 
            WHERE OrderNo = @OrderNo
          `);
        if (orderRes.recordset.length > 0) {
          const ord = orderRes.recordset[0];
          const orderCust = (ord.CustomerCode || '').trim().toUpperCase();
          const currentCust = (CustomerCode || '').trim().toUpperCase();
          if (currentCust && currentCust !== orderCust) {
            return res.status(200).json({
              success: false,
              message: "This inventory is ordered for some other customer. you Cant sale this now."
            });
          }
          if (ord.RateFixed === 'Y' && parseFloat(ord.CostPerGram) > 0) {
            item.CostPerGram = parseFloat(ord.CostPerGram);
          }
        }
      } catch (_) {
        // Table may differ or not exist, gracefully continue
      }
    }

    // 6. Check today's market rate for class
    let dailyRate = item.ClassRate;
    if (item.ClassCode) {
      try {
        const curDate = new Date().toISOString().slice(0, 10);
        const rateRes = await pool.request()
          .input("ClassCode", sql.VarChar, item.ClassCode)
          .input("SalesDate", sql.Date, curDate)
          .query(`
            SELECT TOP 1 Rate 
            FROM tblClassRate 
            WHERE ClassCode = @ClassCode 
            AND @SalesDate BETWEEN FromDate AND ToDate
            ORDER BY FromDate DESC
          `);
        if (rateRes.recordset.length > 0 && parseFloat(rateRes.recordset[0].Rate) > 0) {
          dailyRate = parseFloat(rateRes.recordset[0].Rate);
        }
      } catch (_) {}
    }

    item.DailyClassRate = dailyRate;
    item.ClassRate = dailyRate;

    return res.status(200).json({ success: true, data: item });
  } catch (err) {
    console.error("🔥 Inventory Detail Error:", err);
    return res.status(200).json({ success: false, message: err.message || "Inventory Not Found ...", error: err.message });
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

        let locationCode = '';
        let terminalCode = '';
        let rawNextInvoiceNo = '';

        // 1. Fetch TerminalCode and LocationCode from tblTerminal
        try {
            const termRes = await pool.request().query(`
                SELECT TOP 1 TerminalCode, LocationCode FROM tblTerminal ORDER BY TerminalCode ASC
            `);
            if (termRes.recordset.length > 0) {
                const term = termRes.recordset[0];
                if (term.TerminalCode) terminalCode = String(term.TerminalCode).trim();
                if (term.LocationCode) locationCode = String(term.LocationCode).trim();
            }
        } catch (te) {}

        // 2. Fetch LocationCode and NextInvoice / NextInvoiceNo from tblCompany
        try {
            const compRes = await pool.request().query(`
                SELECT TOP 1 LocationCode, NextInvoice, NextInvoiceNo, NextSalesNo FROM tblCompany
            `);
            if (compRes.recordset.length > 0) {
                const comp = compRes.recordset[0];
                if (!locationCode && comp.LocationCode) locationCode = String(comp.LocationCode).trim();
                rawNextInvoiceNo = comp.NextInvoice || comp.NextInvoiceNo || comp.NextSalesNo || '';
            }
        } catch (ce) {}

        // Fallback default for LocationCode and TerminalCode if empty in DB
        if (!locationCode) locationCode = 'HQ';
        if (!terminalCode) terminalCode = 'T1';

        // 3. If not found in tblCompany, check tblInvoiceNo
        if (!rawNextInvoiceNo) {
            try {
                const nextInvResult = await pool.request().query(`
                    SELECT TOP 1 LocationCode, Terminal, NextInvoice FROM tblInvoiceNo 
                    WHERE Terminal = '${terminalCode}' OR LocationCode = '${locationCode}'
                    ORDER BY NextInvoice DESC
                `);
                if (nextInvResult.recordset.length > 0 && nextInvResult.recordset[0].NextInvoice) {
                    if (nextInvResult.recordset[0].LocationCode) {
                        locationCode = String(nextInvResult.recordset[0].LocationCode).trim();
                    }
                    if (nextInvResult.recordset[0].Terminal) {
                        terminalCode = String(nextInvResult.recordset[0].Terminal).trim();
                    }
                    rawNextInvoiceNo = nextInvResult.recordset[0].NextInvoice;
                }
            } catch (e) {}
        }

        // Query the last created bill from tblSalesHeader (as per frmgpos.vb / clsSalesMan.GetLastBillDetail)
        let lastBill = {};
        try {
            const lastBillResult = await pool.request().query(`
                SELECT TOP 1 A.InvoiceNo, A.NetTotal, A.SalesManCode, B.SalesManName 
                FROM tblSalesHeader A 
                LEFT JOIN tblSalesMan B ON A.SalesManCode = B.SalesManCode 
                ORDER BY A.CreateDate DESC
            `);
            if (lastBillResult.recordset.length > 0) {
                lastBill = lastBillResult.recordset[0];
            }
        } catch (lbe) {
            try {
                const fallbackRes = await pool.request().query(`
                    SELECT TOP 1 InvoiceNo, SalesManCode, NetTotal 
                    FROM tblSalesHeader 
                    ORDER BY CreateDate DESC
                `);
                if (fallbackRes.recordset.length > 0) {
                    lastBill = fallbackRes.recordset[0];
                }
            } catch (fe) {}
        }

        // Format LastBillNo with LocationCode (e.g., HQ01000143)
        let formattedLastBillNo = '';
        if (lastBill && lastBill.InvoiceNo) {
            const rawBill = String(lastBill.InvoiceNo).trim();
            if (rawBill.startsWith(locationCode)) {
                const numPart = rawBill.slice(locationCode.length).replace(/\D/g, '');
                formattedLastBillNo = `${locationCode}${numPart.padStart(8, '0')}`;
            } else {
                const numPart = rawBill.replace(/\D/g, '');
                formattedLastBillNo = `${locationCode}${(numPart || '0').padStart(8, '0')}`;
            }
        }

        // 3. Next invoice number logic:
        // In legacy system: NextInvoice = LastBillNo + 1
        let formattedNextInvoiceNo = '';
        if (rawNextInvoiceNo) {
            const strVal = String(rawNextInvoiceNo).trim();
            if (strVal.startsWith(locationCode)) {
                const numPart = strVal.slice(locationCode.length).replace(/\D/g, '');
                formattedNextInvoiceNo = `${locationCode}${numPart.padStart(8, '0')}`;
            } else {
                const numPart = strVal.replace(/\D/g, '');
                formattedNextInvoiceNo = `${locationCode}${(numPart || '1').padStart(8, '0')}`;
            }
        }

        // If rawNextInvoiceNo equals the last bill or is missing, compute it as lastBill + 1
        if (formattedLastBillNo) {
            if (!formattedNextInvoiceNo || formattedNextInvoiceNo === formattedLastBillNo) {
                const lastNum = parseInt(formattedLastBillNo.slice(locationCode.length).replace(/\D/g, ''), 10) || 0;
                formattedNextInvoiceNo = `${locationCode}${String(lastNum + 1).padStart(8, '0')}`;
            }
        } else if (!formattedNextInvoiceNo) {
            formattedNextInvoiceNo = `${locationCode}00000001`;
        }

        return res.status(200).json({
            success: true,
            data: {
                LocationCode: locationCode,
                TerminalCode: terminalCode,
                NextInvoiceNo: formattedNextInvoiceNo,
                LastSalesMan: lastBill.SalesManName || lastBill.SalesManCode || '0',
                LastBillNo: formattedLastBillNo || lastBill.InvoiceNo || '0',
                LastBillAmount: lastBill.NetTotal !== undefined && lastBill.NetTotal !== null ? Number(lastBill.NetTotal).toFixed(2) : '0.00'
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

// ================= GET PRINT DATA =================
// Crystal Reports migrated:
// Reports/rptSalesInvoice_MJ.rpt, rptSalesInvoiceStandard_MJ.rpt, rptSalesInvoiceNoGST_MJ.rpt,
// rptSalesInvoice_FH.rpt, rptSalesInvoice_SSJ.rpt, rptSalesInvoice_RJ.rpt, rptSalesInvoice_ESP.rpt,
// rptSalesInvoice_AS.rpt, rptSalesInvoice_AJ.rpt, rptSalesInvoice_MD.rpt, rptSalesInvoice_ML.rpt,
// rptSalesInvoice_GMT.rpt, rptSalesInvoice_HN.rpt, rptInstallmentSalesInvoice_*.rpt, rptTradeInSalesInvoice_*.rpt
// Record Selection: {tblSalesHeader.InvoiceNo} = sInvoiceNo
// Formula Fields: CompAddress1, CompAddress2, CompAddress3, Customer1, Customer2, Customer3, Rate1, Rate2, Rate3, Rate4
router.get("/print-data", async (req, res) => {
  try {
    const rawInvoiceNo = (req.query.invoiceNo || req.query.billNo || "").trim();
    if (!rawInvoiceNo) {
      return res.status(400).json({ success: false, message: "Invoice No / Bill No is required for printing." });
    }

    const pool = req.pool;

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

    // 2. Fetch Sales Header joined with tblCustomer & tblSalesMan
    const headerRes = await pool.request()
      .input("InvoiceNo", sql.VarChar, rawInvoiceNo)
      .query(`
        SELECT TOP 1 
          h.*,
          c.CustomerName AS CustMasterName,
          c.Address1 AS CustAddress1,
          c.Address2 AS CustAddress2,
          c.Address3 AS CustAddress3,
          c.CustomerType AS CustType,
          c.NricNo AS CustNricNo,
          sm.SalesManName
        FROM tblSalesHeader h
        LEFT JOIN tblCustomer c ON RTRIM(h.CustomerCode) = RTRIM(c.CustomerCode)
        LEFT JOIN tblSalesMan sm ON RTRIM(h.SalesManCode) = RTRIM(sm.SalesManCode)
        WHERE RTRIM(h.InvoiceNo) = RTRIM(@InvoiceNo)
      `);

    if (headerRes.recordset.length === 0) {
      return res.status(404).json({ success: false, message: `Invoice No "${rawInvoiceNo}" not found.` });
    }

    const header = headerRes.recordset[0];

    // Customer Name resolution
    let finalCustName = "";
    if (header.CustMasterName) {
      finalCustName = Array.isArray(header.CustMasterName) ? header.CustMasterName[0] : header.CustMasterName;
    } else if (header.CustomerName) {
      finalCustName = Array.isArray(header.CustomerName) ? header.CustomerName[0] : header.CustomerName;
    } else {
      finalCustName = header.CustomerCode || "CASH CUSTOMER";
    }

    // 3. Fetch Sales Details
    const itemsRes = await pool.request()
      .input("InvoiceNo", sql.VarChar, rawInvoiceNo)
      .query(`
        SELECT d.*
        FROM tblSalesDetail d
        WHERE RTRIM(d.InvoiceNo) = RTRIM(@InvoiceNo)
        ORDER BY d.SortCode ASC, d.InventoryCode ASC
      `);

    // 4. Fetch Payment Modes
    const payRes = await pool.request()
      .input("InvoiceNo", sql.VarChar, rawInvoiceNo)
      .query(`
        SELECT *
        FROM tblPayModeDetail
        WHERE RTRIM(InvoiceNo) = RTRIM(@InvoiceNo)
        ORDER BY PayMode ASC
      `);

    // 5. Fetch Daily Class Rates for Rate1, Rate2, Rate3, Rate4
    let rate999 = "-";
    let rate916 = "-";
    let rate835 = "-";
    let rate24k999 = "-";
    let rate20k850 = "-";
    let rate22ksov = "-";

    try {
      const salesDate = header.InvoiceDate ? new Date(header.InvoiceDate) : new Date();
      const rateRes = await pool.request()
        .input("SalesDate", sql.DateTime, salesDate)
        .query(`
          SELECT ClassCode, ISNULL(Rate, 0.00) AS Rate
          FROM tblClassRate
          WHERE @SalesDate BETWEEN FromDate AND ToDate
          ORDER BY ClassCode
        `);

      for (const row of rateRes.recordset) {
        const code = (row.ClassCode || "").trim().toUpperCase();
        const rVal = parseFloat(row.Rate) || 0;
        const formatted = rVal.toFixed(2);
        if (code === "22K" || code === "916" || code === "22K-916") rate916 = formatted;
        if (code === "999" || code === "24K" || code === "24K-999") {
          rate999 = formatted;
          rate24k999 = formatted;
        }
        if (code === "SILVER" || code === "835") rate835 = formatted;
        if (code === "20K-850" || code === "20K") rate20k850 = formatted;
        if (code === "22K-SOV" || code === "SOV") rate22ksov = formatted;
      }
    } catch (rateErr) {
      console.warn("⚠️ Rate lookup warning:", rateErr.message);
    }

    return res.status(200).json({
      success: true,
      data: {
        company,
        rates: {
          rate999,
          rate916,
          rate835,
          rate24k999,
          rate20k850,
          rate22ksov
        },
        header: {
          ...header,
          CustomerName: String(finalCustName).trim(),
          CustomerAddress1: (header.CustAddress1 || header.Address1 || "").trim(),
          CustomerAddress2: (header.CustAddress2 || header.Address2 || "").trim(),
          CustomerAddress3: (header.CustAddress3 || header.Address3 || "").trim(),
          CustomerNricNo: (header.CustNricNo || "").trim(),
          SalesManName: (header.SalesManName || header.SalesManCode || "").trim()
        },
        items: itemsRes.recordset,
        payments: payRes.recordset
      }
    });
  } catch (err) {
    console.error("🔥 GET POS PRINT-DATA ERROR:", err);
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
