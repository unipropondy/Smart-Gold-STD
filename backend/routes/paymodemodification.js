const express = require("express");
const router = express.Router();
const { sql } = require("../db");

// Helper to ensure change log table exists
async function ensureChangeLogTableExists(pool) {
  const checkTableQuery = `
    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='tblSalesPaymodeChangeLog' AND xtype='U')
    BEGIN
        CREATE TABLE tblSalesPaymodeChangeLog (
            ID INT IDENTITY(1,1) PRIMARY KEY,
            InvoiceNo VARCHAR(50),
            InvoiceDate DATETIME,
            PayMode VARCHAR(50),
            Amount DECIMAL(18,4),
            ChangeDate DATETIME DEFAULT GETDATE(),
            ChangeUser VARCHAR(50) DEFAULT 'sys'
        )
    END
  `;
  try {
    await pool.request().query(checkTableQuery);
  } catch (err) {
    console.error("Error ensuring tblSalesPaymodeChangeLog exists:", err);
  }
}

// GET all recent invoices for overview list (subLoadinvoices)
router.get("/invoices", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query(`
      SELECT TOP 100
        h.InvoiceNo,
        CONVERT(varchar, h.InvoiceDate, 103) as InvoiceDate,
        h.CustomerCode,
        h.TerminalCode,
        h.SalesManCode,
        h.SalesManCode as SalesManName,
        h.NetTotal,
        h.PaidAmt,
        h.BalanceAmt,
        h.Remarks,
        h.LocationCode,
        h.SalesType,
        (
          SELECT STUFF((
            SELECT ' , ' + p.Paymode
            FROM tblPayModeDetail p
            WHERE p.InvoiceNo = h.InvoiceNo
            FOR XML PATH(''), TYPE
          ).value('.', 'NVARCHAR(MAX)'), 1, 3, '')
        ) as InvoicePaymode
      FROM tblSalesHeader h
      ORDER BY h.InvoiceDate DESC
    `);

    res.json({ success: true, data: result.recordset });
  } catch (err) {
    try {
      // Fallback in case XML PATH or Paymode column differences exist
      const fallbackResult = await req.pool.request().query(`
        SELECT TOP 100
          h.InvoiceNo,
          CONVERT(varchar, h.InvoiceDate, 103) as InvoiceDate,
          h.CustomerCode,
          h.TerminalCode,
          h.SalesManCode,
          h.SalesManCode as SalesManName,
          h.NetTotal,
          h.PaidAmt,
          h.BalanceAmt,
          h.Remarks,
          h.LocationCode,
          h.SalesType,
          '' as InvoicePaymode
        FROM tblSalesHeader h
        ORDER BY h.InvoiceDate DESC
      `);
      res.json({ success: true, data: fallbackResult.recordset });
    } catch (fErr) {
      console.error("Fetch Invoices Error:", err);
      res.status(500).json({ success: false, message: "Error fetching invoices", error: err.message });
    }
  }
});

// GET invoice details by InvoiceNo (subLoadinvoicesbyInvoiceNo + subLoadPayMode)
router.get("/invoice/:invoiceNo", async (req, res) => {
  try {
    const { invoiceNo } = req.params;
    const pool = req.pool;

    // Fetch Header Details
    let headerRes;
    try {
      headerRes = await pool.request()
        .input("InvoiceNo", sql.VarChar, invoiceNo.trim())
        .query(`
          SELECT TOP 1
            h.InvoiceNo, 
            h.CustomerCode, 
            h.TerminalCode, 
            h.SalesManCode, 
            h.SalesManCode as SalesManName,
            h.NetTotal, 
            h.PaidAmt, 
            h.BalanceAmt, 
            h.Remarks,
            h.LocationCode, 
            h.SalesType, 
            CONVERT(varchar, h.InvoiceDate, 103) as InvoiceDateStr,
            h.InvoiceDate,
            (
              SELECT STUFF((
                SELECT ' , ' + p.Paymode
                FROM tblPayModeDetail p
                WHERE p.InvoiceNo = h.InvoiceNo
                FOR XML PATH(''), TYPE
              ).value('.', 'NVARCHAR(MAX)'), 1, 3, '')
            ) as InvoicePaymode
          FROM tblSalesHeader h
          WHERE h.InvoiceNo = @InvoiceNo OR h.InvoiceNo LIKE '%' + @InvoiceNo + '%'
          ORDER BY h.InvoiceDate DESC
        `);
    } catch (hErr) {
      // Fallback without XML PATH if needed
      headerRes = await pool.request()
        .input("InvoiceNo", sql.VarChar, invoiceNo.trim())
        .query(`
          SELECT TOP 1
            h.InvoiceNo, 
            h.CustomerCode, 
            h.TerminalCode, 
            h.SalesManCode, 
            h.SalesManCode as SalesManName,
            h.NetTotal, 
            h.PaidAmt, 
            h.BalanceAmt, 
            h.Remarks,
            h.LocationCode, 
            h.SalesType, 
            CONVERT(varchar, h.InvoiceDate, 103) as InvoiceDateStr,
            h.InvoiceDate,
            '' as InvoicePaymode
          FROM tblSalesHeader h
          WHERE h.InvoiceNo = @InvoiceNo OR h.InvoiceNo LIKE '%' + @InvoiceNo + '%'
          ORDER BY h.InvoiceDate DESC
        `);
    }

    if (headerRes.recordset.length === 0) {
      return res.status(404).json({ success: false, message: "Invoice No Not Found" });
    }

    const header = headerRes.recordset[0];
    const actualInvoiceNo = header.InvoiceNo;

    // Fetch existing payment details for this invoice
    const paymodeRes = await pool.request()
      .input("InvoiceNo", sql.VarChar, actualInvoiceNo)
      .query(`
        SELECT Paymode as PayMode, Amount, SlipNo 
        FROM tblPayModeDetail 
        WHERE InvoiceNo = @InvoiceNo 
        ORDER BY PayMode ASC
      `);

    // Fetch master paymodes list
    let masterPaymodes = [];
    try {
      const mpRes = await pool.request().query("SELECT PayMode FROM tblPaymode WHERE Active = 1 ORDER BY Position ASC");
      masterPaymodes = mpRes.recordset.map(r => (r.PayMode || '').trim());
    } catch (e1) {
      try {
        const mpRes2 = await pool.request().query("SELECT PayMode FROM tblPayModeMaster ORDER BY PayMode ASC");
        masterPaymodes = mpRes2.recordset.map(r => (r.PayMode || '').trim());
      } catch (e2) {}
    }

    // Default standard paymodes if tables are empty
    if (masterPaymodes.length === 0) {
      masterPaymodes = ["CASH", "NETS", "VISA", "TRADEIN", "MASTER", "AMEX", "DINERS", "DEPOSIT", "INSTALLMENT"];
    }

    // Build map of existing paymodes for this invoice
    const existingMap = new Map();
    let totalPaid = 0;
    paymodeRes.recordset.forEach(p => {
      const modeName = (p.PayMode || '').trim();
      const amt = parseFloat(p.Amount || 0);
      totalPaid += amt;
      existingMap.set(modeName.toUpperCase(), {
        PayMode: modeName,
        Amount: amt,
        SlipNo: (p.SlipNo || '').trim()
      });
    });

    // Merge master paymodes list so all standard paymodes are shown
    const finalPaymodes = [];
    const processedModes = new Set();

    masterPaymodes.forEach(modeName => {
      const upper = modeName.toUpperCase();
      processedModes.add(upper);
      if (existingMap.has(upper)) {
        const ex = existingMap.get(upper);
        finalPaymodes.push({
          PayMode: ex.PayMode,
          Amount: ex.Amount,
          SlipNo: ex.SlipNo,
          NewAmount: ex.Amount.toFixed(2),
          NewSlipNo: ex.SlipNo
        });
      } else {
        finalPaymodes.push({
          PayMode: modeName,
          Amount: 0,
          SlipNo: '',
          NewAmount: '0.00',
          NewSlipNo: ''
        });
      }
    });

    // Add any existing invoice paymode that was not in master list
    existingMap.forEach((ex, key) => {
      if (!processedModes.has(key)) {
        finalPaymodes.push({
          PayMode: ex.PayMode,
          Amount: ex.Amount,
          SlipNo: ex.SlipNo,
          NewAmount: ex.Amount.toFixed(2),
          NewSlipNo: ex.SlipNo
        });
      }
    });

    res.json({
      success: true,
      header: {
        ...header,
        PaidAmt: totalPaid,
        Change: (totalPaid - parseFloat(header.NetTotal || 0))
      },
      paymodes: finalPaymodes
    });
  } catch (err) {
    console.error("Fetch Invoice Error:", err);
    res.status(500).json({ success: false, message: "Database Error", error: err.message });
  }
});

// GET all active paymodes for LOV
router.get("/paymodes", async (req, res) => {
  try {
    const pool = req.pool;
    let result;
    try {
      result = await pool.request().query("SELECT PayMode FROM tblPaymode WHERE Active = 1 ORDER BY Position ASC");
    } catch (e1) {
      try {
        result = await pool.request().query("SELECT PayMode FROM tblPayModeMaster ORDER BY PayMode ASC");
      } catch (e2) {
        result = await pool.request().query("SELECT DISTINCT Paymode as PayMode FROM tblPayModeDetail WHERE Paymode IS NOT NULL AND Paymode <> '' ORDER BY Paymode ASC");
      }
    }

    if (result && result.recordset && result.recordset.length > 0) {
      return res.json({ success: true, data: result.recordset });
    }

    const defaultModes = [
      { PayMode: "CASH" },
      { PayMode: "NETS" },
      { PayMode: "VISA" },
      { PayMode: "TRADEIN" },
      { PayMode: "MASTER" },
      { PayMode: "AMEX" },
      { PayMode: "DINERS" },
      { PayMode: "DEPOSIT" },
      { PayMode: "INSTALLMENT" }
    ];
    res.json({ success: true, data: defaultModes });
  } catch (err) {
    console.error("Fetch Paymode LOV Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// POST verify salesman password (subGetPassword)
router.post("/verify-salesman", async (req, res) => {
  try {
    const pool = req.pool;
    const { password } = req.body;

    if (!password) {
      return res.status(400).json({ success: false, message: "SalesMan Password is Required!" });
    }

    // Try tblSalesMan first, then tblSalesman (case insensitive query)
    const result = await pool.request()
      .input("Password", sql.VarChar, password.trim())
      .query(`
        SELECT SalesManCode, SalesManName, AllowSalesReturn 
        FROM tblSalesMan 
        WHERE Password = @Password
      `);

    if (result.recordset.length > 0) {
      res.json({ success: true, data: result.recordset[0] });
    } else {
      res.json({ success: false, message: "SalesMan Password is Incorrect!" });
    }
  } catch (err) {
    console.error("Verify Salesman Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// POST Save modifications (fncPaymodeDetailSave + fncSalesPaymodeChangeLogSave)
router.post("/save", async (req, res) => {
  const pool = req.pool;
  const transaction = new sql.Transaction(pool);

  try {
    const { 
      invoiceNo, 
      invoiceDate, 
      locationCode, 
      salesType, 
      remarks, 
      paymodes, 
      userId, 
      salesmanCode 
    } = req.body;
    
    if (!invoiceNo) {
      return res.status(400).json({ success: false, message: "InvoiceNo is required" });
    }

    const sysUser = (userId || "sys").substring(0, 10);
    const curDate = new Date();

    // Check if invoice exists and get original header info
    const existingHeaderRes = await pool.request()
      .input("InvNo", sql.VarChar, invoiceNo.trim())
      .query(`SELECT TOP 1 InvoiceDate, LocationCode, SalesType, NetTotal FROM tblSalesHeader WHERE InvoiceNo = @InvNo`);

    let formattedInvDate = curDate;
    let finalLocationCode = locationCode || "HQ";
    let finalSalesType = salesType || "CASH";

    if (existingHeaderRes.recordset.length > 0) {
      const row = existingHeaderRes.recordset[0];
      if (row.InvoiceDate) formattedInvDate = row.InvoiceDate;
      if (row.LocationCode) finalLocationCode = row.LocationCode;
      if (row.SalesType) finalSalesType = row.SalesType;
    } else if (invoiceDate) {
      formattedInvDate = new Date(invoiceDate);
    }

    await ensureChangeLogTableExists(pool);

    await transaction.begin();

    // 1. Update Remarks on tblSalesHeader (fncUpdatePayModeDetailRemarks)
    await new sql.Request(transaction)
      .input("InvoiceNo", sql.VarChar, invoiceNo.trim())
      .input("Remarks", sql.VarChar, (remarks || "").substring(0, 255))
      .input("SalesManCode", sql.VarChar, (salesmanCode || "").substring(0, 50))
      .input("ModifyUser", sql.VarChar, sysUser)
      .input("ModifyDate", sql.SmallDateTime, curDate)
      .query(`
        UPDATE tblSalesHeader 
        SET Remarks = @Remarks, 
            ModifyUser = @ModifyUser, 
            ModifyDate = @ModifyDate 
        WHERE InvoiceNo = @InvoiceNo
      `);

    // 2. Delete existing paymodes for this invoice (fncUpdatePayModeDetails)
    await new sql.Request(transaction)
      .input("InvoiceNo", sql.VarChar, invoiceNo.trim())
      .query("DELETE FROM tblPayModeDetail WHERE InvoiceNo = @InvoiceNo");

    // 3. Insert new paymodes and add to change log (fncSalesPaymodeDetailSave & fncSalesPaymodeChangeLogSave)
    let totalSaved = 0;
    if (paymodes && paymodes.length > 0) {
      for (const p of paymodes) {
        const payAmount = parseFloat(p.NewAmount) || 0;
        
        if (payAmount !== 0) {
          totalSaved += payAmount;
          const payModeStr = (p.PayMode || "").trim().substring(0, 50);
          const slipNoStr = (p.NewSlipNo || "").trim().substring(0, 50);

          // Insert into tblPayModeDetail
          await new sql.Request(transaction)
            .input("InvoiceNo", sql.Char(20), invoiceNo.trim().substring(0, 20))
            .input("InvoiceDate", sql.SmallDateTime, formattedInvDate)
            .input("PayMode", sql.VarChar(50), payModeStr)
            .input("Amount", sql.Numeric(18, 4), payAmount)
            .input("SlipNo", sql.VarChar(50), slipNoStr)
            .input("LocationCode", sql.VarChar(20), finalLocationCode.substring(0, 20))
            .input("SalesType", sql.VarChar(20), finalSalesType.substring(0, 20))
            .input("User", sql.Char(10), sysUser)
            .input("Date", sql.SmallDateTime, curDate)
            .query(`
              INSERT INTO tblPayModeDetail (
                InvoiceNo, InvoiceDate, PayMode, Amount, Commision, SlipNo, 
                CreateUser, CreateDate, ModifyUser, ModifyDate, LocationCode, SalesType, IsPaid
              ) VALUES (
                @InvoiceNo, @InvoiceDate, @PayMode, @Amount, 0, @SlipNo, 
                @User, @Date, @User, @Date, @LocationCode, @SalesType, 1
              )
            `);

          // Insert into tblSalesPaymodeChangeLog
          await new sql.Request(transaction)
            .input("InvoiceNo", sql.VarChar(50), invoiceNo.trim().substring(0, 50))
            .input("InvoiceDate", sql.SmallDateTime, formattedInvDate)
            .input("PayMode", sql.VarChar(50), payModeStr)
            .input("Amount", sql.Numeric(18, 4), payAmount)
            .input("User", sql.VarChar(50), sysUser)
            .input("Date", sql.SmallDateTime, curDate)
            .query(`
              INSERT INTO tblSalesPaymodeChangeLog (
                InvoiceNo, InvoiceDate, PayMode, Amount, ChangeDate, ChangeUser
              ) VALUES (
                @InvoiceNo, @InvoiceDate, @PayMode, @Amount, @Date, @User
              )
            `);
        }
      }
    }

    // 4. Update PaidAmt & BalanceAmt on tblSalesHeader to keep it completely synchronized
    await new sql.Request(transaction)
      .input("InvoiceNo", sql.VarChar, invoiceNo.trim())
      .input("PaidAmt", sql.Numeric(18, 4), totalSaved)
      .query(`
        UPDATE tblSalesHeader 
        SET PaidAmt = @PaidAmt,
            BalanceAmt = NetTotal - @PaidAmt
        WHERE InvoiceNo = @InvoiceNo
      `);

    await transaction.commit();
    res.json({ success: true, message: `Paymode details updated successfully` });
  } catch (err) {
    console.error("Save Paymode Modification Error:", err);
    try {
      await transaction.rollback();
    } catch (rberr) {}
    res.status(500).json({ success: false, message: "Database Save failed", error: err.message });
  }
});

module.exports = router;

