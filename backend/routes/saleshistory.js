const express = require("express");
const router = express.Router();
const { sql } = require("../db");

// Helper to format date for SQL
const formatDateToSQL = (dateStr) => {
  if (!dateStr) return null;
  const parts = dateStr.split('-');
  if (parts.length === 3) {
    if (parts[2].length === 4) {
      return `${parts[2]}-${parts[1]}-${parts[0]}`; // DD-MM-YYYY to YYYY-MM-DD
    }
  }
  return dateStr;
};

// GET list of invoices for the Invoice No LOV
router.get("/list", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query(`
      SELECT DISTINCT 
        InvoiceNo, 
        InvoiceDate, 
        LocationCode, 
        CustomerCode, 
        CustomerName, 
        NetTotal 
      FROM tblSalesHeader 
      ORDER BY InvoiceNo DESC
    `);
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Get Sales History List Error:", err);
    res.status(500).json({ success: false, message: "Database Error", error: err.message });
  }
});

// GET next Invoice sequence No
router.get("/nextNo", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query("SELECT TOP 1 LocationCode, NextInvoice FROM tblCompany");
    let nextNo = 1;
    let loc = "HQ";
    if (result.recordset.length > 0) {
      nextNo = parseInt(result.recordset[0].NextInvoice) || 1;
      loc = (result.recordset[0].LocationCode || "HQ").trim();
    }
    const formattedNo = `INV-${loc}${String(nextNo).padStart(8, '0')}`;
    res.json({ success: true, nextNo: formattedNo });
  } catch (err) {
    console.error("Get next Invoice Number error:", err);
    res.status(500).json({ success: false, message: "Database Error", error: err.message });
  }
});

// GET fetch detail columns by InvoiceNo
router.get("/:invoiceNo", async (req, res) => {
  try {
    const { invoiceNo } = req.params;
    const pool = req.pool;

    // 1. Fetch Header Details
    const headerRes = await pool.request()
      .input("InvoiceNo", sql.VarChar, invoiceNo)
      .query("SELECT * FROM tblSalesHeader WHERE InvoiceNo = @InvoiceNo");

    if (headerRes.recordset.length === 0) {
      return res.status(404).json({ success: false, message: "Invoice not found" });
    }

    // 2. Fetch Detail items joined with tblInventory to get TotalCost
    const detailRes = await pool.request()
      .input("InvoiceNo", sql.VarChar, invoiceNo)
      .query(`
        SELECT 
          a.InventoryCode, 
          a.Description, 
          a.Weight, 
          a.StoneWeight, 
          a.GST, 
          a.NetAmount AS NetAmount, 
          ISNULL(b.TotalCost, a.TotalCost) AS TotalCost
        FROM tblSalesDetail a
        LEFT JOIN tblInventory b ON a.InventoryCode = b.InventoryCode
        WHERE a.InvoiceNo = @InvoiceNo
        ORDER BY a.ModifyDate DESC
      `);

    // 3. Fetch Payment details
    const paymodeRes = await pool.request()
      .input("InvoiceNo", sql.VarChar, invoiceNo)
      .query("SELECT PayMode, Amount, SlipNo FROM tblPayModeDetail WHERE InvoiceNo = @InvoiceNo ORDER BY PayMode DESC");

    res.json({
      success: true,
      header: headerRes.recordset[0],
      details: detailRes.recordset,
      payments: paymodeRes.recordset
    });
  } catch (err) {
    console.error("Fetch Invoice details error:", err);
    res.status(500).json({ success: false, message: "Database Error", error: err.message });
  }
});

// POST Create or Update Sales History Transaction
router.post("/", async (req, res) => {
  const pool = req.pool;
  const transaction = new sql.Transaction(pool);

  try {
    const { header, details, payments, userId } = req.body;
    if (!header || !header.InvoiceNo) {
      return res.status(400).json({ success: false, message: "InvoiceNo is required" });
    }

    const sysUser = (userId || "sys").substring(0, 10);
    const curDate = new Date();
    const formattedInvDate = header.InvoiceDate ? new Date(header.InvoiceDate) : curDate;

    await transaction.begin();

    const checkRequest = new sql.Request(transaction);
    const checkRes = await checkRequest
      .input("InvoiceNoCheck", sql.VarChar, header.InvoiceNo)
      .query("SELECT 1 FROM tblSalesHeader WHERE InvoiceNo = @InvoiceNoCheck");

    const isExisting = checkRes.recordset.length > 0;

    const request = new sql.Request(transaction)
      .input("InvoiceNo", sql.Char, header.InvoiceNo.substring(0, 20))
      .input("InvoiceDate", sql.SmallDateTime, formattedInvDate)
      .input("LocationCode", sql.Char, (header.LocationCode || "HQ").substring(0, 10))
      .input("CustomerCode", sql.Char, (header.CustomerCode || "").substring(0, 20))
      .input("CustomerName", sql.VarChar, (header.CustomerName || "").substring(0, 100))
      .input("SalesManCode", sql.Char, (header.SalesManCode || "").substring(0, 10))
      .input("SalesType", sql.Char, (header.SalesType || "CASH").substring(0, 10))
      .input("TotalWeight", sql.Numeric(18, 4), parseFloat(header.TotalWeight) || 0)
      .input("TotalStoneWeight", sql.Numeric(18, 4), parseFloat(header.TotalStoneWeight) || 0)
      .input("TotalPCS", sql.Numeric(18, 4), parseFloat(header.TotalPCS) || 0)
      .input("GST", sql.Numeric(18, 4), parseFloat(header.GST) || 0)
      .input("NetTotal", sql.Numeric(18, 4), parseFloat(header.NetTotal) || 0)
      .input("InsAgreementNo", sql.Char, (header.InsAgreementNo || "").substring(0, 50))
      .input("TradeInAmt", sql.Numeric(18, 4), parseFloat(header.TradeInAmt) || 0)
      .input("Remarks", sql.Char, (header.Remarks || "").substring(0, 255))
      .input("User", sql.Char, sysUser)
      .input("Date", sql.SmallDateTime, curDate);

    if (isExisting) {
      // 1. Update existing header
      await request.query(`
        UPDATE tblSalesHeader SET
          InvoiceDate = @InvoiceDate,
          LocationCode = @LocationCode,
          CustomerCode = @CustomerCode,
          CustomerName = @CustomerName,
          SalesManCode = @SalesManCode,
          SalesType = @SalesType,
          TotalWeight = @TotalWeight,
          TotalStoneWeight = @TotalStoneWeight,
          TotalPCS = @TotalPCS,
          GST = @GST,
          NetTotal = @NetTotal,
          InsAgreementNo = @InsAgreementNo,
          TradeInAmt = @TradeInAmt,
          Remarks = @Remarks,
          ModifyUser = @User,
          ModifyDate = @Date
        WHERE InvoiceNo = @InvoiceNo
      `);

      // Delete existing details and payments
      await new sql.Request(transaction)
        .input("InvoiceNo", sql.VarChar, header.InvoiceNo)
        .query("DELETE FROM tblSalesDetail WHERE InvoiceNo = @InvoiceNo");

      await new sql.Request(transaction)
        .input("InvoiceNo", sql.VarChar, header.InvoiceNo)
        .query("DELETE FROM tblPayModeDetail WHERE InvoiceNo = @InvoiceNo");
    } else {
      // 2. Insert new header
      await request.query(`
        INSERT INTO tblSalesHeader (
          InvoiceNo, InvoiceDate, LocationCode, CustomerCode, CustomerName, SalesManCode, SalesType, 
          TotalWeight, TotalStoneWeight, TotalPCS, GST, NetTotal, InsAgreementNo, TradeInAmt, Remarks, 
          CreateUser, CreateDate, ModifyUser, ModifyDate
        ) VALUES (
          @InvoiceNo, @InvoiceDate, @LocationCode, @CustomerCode, @CustomerName, @SalesManCode, @SalesType, 
          @TotalWeight, @TotalStoneWeight, @TotalPCS, @GST, @NetTotal, @InsAgreementNo, @TradeInAmt, @Remarks, 
          @User, @Date, @User, @Date
        )
      `);

      // 3. Increment NextInvoice in tblCompany
      await new sql.Request(transaction).query("UPDATE tblCompany SET NextInvoice = NextInvoice + 1");
    }

    // 4. Insert details list
    if (details && details.length > 0) {
      for (const item of details) {
        await new sql.Request(transaction)
          .input("InvoiceNo", sql.Char, header.InvoiceNo.substring(0, 20))
          .input("InventoryCode", sql.Char, (item.InventoryCode || "").substring(0, 20))
          .input("Description", sql.Char, (item.Description || "").substring(0, 100))
          .input("Weight", sql.Numeric(18, 4), parseFloat(item.Weight) || 0)
          .input("StoneWeight", sql.Numeric(18, 4), parseFloat(item.StoneWeight) || 0)
          .input("GST", sql.Numeric(18, 4), parseFloat(item.GST) || 0)
          .input("NetAmount", sql.Numeric(18, 4), parseFloat(item.NetAmount) || parseFloat(item.NetTotal) || 0)
          .input("TotalCost", sql.Numeric(18, 4), parseFloat(item.TotalCost) || 0)
          .input("User", sql.Char, sysUser)
          .input("Date", sql.SmallDateTime, curDate)
          .query(`
            INSERT INTO tblSalesDetail (
              InvoiceNo, InventoryCode, Description, Weight, StoneWeight, GST, NetAmount, TotalCost,
              CreateUser, CreateDate, ModifyUser, ModifyDate
            ) VALUES (
              @InvoiceNo, @InventoryCode, @Description, @Weight, @StoneWeight, @GST, @NetAmount, @TotalCost,
              @User, @Date, @User, @Date
            )
          `);
      }
    }

    // 5. Insert payments list
    if (payments && payments.length > 0) {
      for (const p of payments) {
        await new sql.Request(transaction)
          .input("InvoiceNo", sql.Char, header.InvoiceNo.substring(0, 20))
          .input("InvoiceDate", sql.SmallDateTime, formattedInvDate)
          .input("PayMode", sql.VarChar, (p.PayMode || "").substring(0, 50))
          .input("Amount", sql.Numeric(18, 4), parseFloat(p.Amount) || 0)
          .input("SlipNo", sql.VarChar, (p.SlipNo || "").substring(0, 50))
          .input("LocationCode", sql.VarChar, (header.LocationCode || "HQ").substring(0, 20))
          .input("SalesType", sql.VarChar, (header.SalesType || "CASH").substring(0, 20))
          .input("User", sql.Char, sysUser)
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
      }
    }

    await transaction.commit();
    res.json({ success: true, message: `Invoice saved successfully` });
  } catch (err) {
    console.error("Save Invoice Error:", err);
    try {
      await transaction.rollback();
    } catch (rberr) {}
    res.status(500).json({ success: false, message: "Database Save failed", error: err.message });
  }
});

// DELETE Invoice
router.delete("/:invoiceNo", async (req, res) => {
  const pool = req.pool;
  const { invoiceNo } = req.params;
  const transaction = new sql.Transaction(pool);

  try {
    await transaction.begin();

    await new sql.Request(transaction)
      .input("InvoiceNo", sql.VarChar, invoiceNo)
      .query("DELETE FROM tblSalesDetail WHERE InvoiceNo = @InvoiceNo");

    await new sql.Request(transaction)
      .input("InvoiceNo", sql.VarChar, invoiceNo)
      .query("DELETE FROM tblPayModeDetail WHERE InvoiceNo = @InvoiceNo");

    const deleteRes = await new sql.Request(transaction)
      .input("InvoiceNo", sql.VarChar, invoiceNo)
      .query("DELETE FROM tblSalesHeader WHERE InvoiceNo = @InvoiceNo");

    if (deleteRes.rowsAffected[0] === 0) {
      await transaction.rollback();
      return res.status(404).json({ success: false, message: "Invoice not found or already deleted" });
    }

    await transaction.commit();
    res.json({ success: true, message: "Invoice deleted successfully" });
  } catch (err) {
    console.error("Delete Invoice Error:", err);
    try {
      await transaction.rollback();
    } catch (rberr) {}
    res.status(500).json({ success: false, message: "Database Delete failed", error: err.message });
  }
});

module.exports = router;
