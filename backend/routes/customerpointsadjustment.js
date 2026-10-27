const express = require("express");
const router = express.Router();
const { sql } = require("../db");

// GET all customers for lookup
router.get("/customers", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query(`
      SELECT CustomerCode, CustomerName, Address1, Address2, Mobile, CustomerType 
      FROM tblCustomer 
      ORDER BY CustomerCode ASC
    `);
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Customers Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// GET specific customer details and their sales history
router.get("/customer/:code", async (req, res) => {
  try {
    const pool = req.pool;
    const { code } = req.params;

    // Fetch customer details
    const custResult = await pool.request()
      .input('CustomerCode', sql.VarChar, code)
      .query(`
        SELECT CustomerCode, CustomerName, CustomerType, TotalRewardPoints 
        FROM tblCustomer 
        WHERE CustomerCode = @CustomerCode
      `);

    if (custResult.recordset.length === 0) {
      return res.status(404).json({ success: false, message: "Customer not found" });
    }

    const customer = custResult.recordset[0];

    // Fetch Sales History for this customer
    const salesResult = await pool.request()
      .input('CustomerCode', sql.VarChar, code)
      .query(`
        SELECT InvoiceNo, InvoiceDate, TotalWeight, TotalPCS, NetTotal 
        FROM tblSalesHeader 
        WHERE CustomerCode = @CustomerCode
        ORDER BY InvoiceDate DESC
      `);

    res.json({ 
      success: true, 
      customer: customer,
      sales: salesResult.recordset 
    });
  } catch (err) {
    console.error("Fetch Customer Details Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// POST to save point adjustment
router.post("/", async (req, res) => {
  try {
    const pool = req.pool;
    const { 
      CustomerCode, CustomerName, CustomerType, PreviousPoints, 
      AdjustmentPoints, InvoiceNo 
    } = req.body;

    if (!CustomerCode || !AdjustmentPoints || !InvoiceNo) {
      return res.status(400).json({ success: false, message: "Required fields missing." });
    }

    // Check if InvoiceNo already exists in adjustment table (fncCheckInvoiceNo logic)
    const checkInvoice = await pool.request()
      .input('InvoiceNo', sql.VarChar, InvoiceNo)
      .query("SELECT InvoiceNo FROM tblCustomersPointAdjustment WHERE InvoiceNo = @InvoiceNo");

    if (checkInvoice.recordset.length > 0) {
      return res.status(400).json({ success: false, message: "Invoice Number Already Entered For another customer. Please Check." });
    }

    // Insert into tblCustomersPointAdjustment
    const insertQuery = `
      INSERT INTO tblCustomersPointAdjustment (
        CustomerCode, CustomerName, CustomerType, PreviousPoints, 
        AdjustmentPoints, CreateUser, CreateDate, ModifyUser, ModifyDate, InvoiceNo
      ) VALUES (
        @CustomerCode, @CustomerName, @CustomerType, @PreviousPoints, 
        @AdjustmentPoints, 'sys', GETDATE(), 'sys', GETDATE(), @InvoiceNo
      )
    `;

    await pool.request()
      .input('CustomerCode', sql.VarChar, CustomerCode)
      .input('CustomerName', sql.VarChar, CustomerName || '')
      .input('CustomerType', sql.VarChar, CustomerType || '')
      .input('PreviousPoints', sql.Decimal(18,2), PreviousPoints || 0)
      .input('AdjustmentPoints', sql.Decimal(18,2), AdjustmentPoints)
      .input('InvoiceNo', sql.VarChar, InvoiceNo)
      .query(insertQuery);

    res.json({ success: true, message: "Customer detail saved successfully." });
  } catch (err) {
    console.error("Save Adjustment Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

module.exports = router;
