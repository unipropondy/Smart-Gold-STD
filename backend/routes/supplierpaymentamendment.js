const express = require("express");
const router = express.Router();
const { sql } = require("../db");

// Fetch payment details by SupplierCode & InvoiceNo
router.get("/fetch", async (req, res) => {
  try {
    const pool = req.pool;
    const { SupplierCode, InvoiceNo } = req.query;

    if (!SupplierCode || !InvoiceNo) {
      return res.status(400).json({ success: false, message: "SupplierCode and InvoiceNo are required." });
    }

    const query = `
      SELECT PaymentNo, PaymentDate, Paymode, PaymentAmount, Remarks 
      FROM tbljobsheetpaymentdetail
      WHERE VendorCode = @SupplierCode AND InvoiceNo = @InvoiceNo
    `;

    const result = await pool.request()
      .input("SupplierCode", sql.VarChar, SupplierCode)
      .input("InvoiceNo", sql.VarChar, InvoiceNo)
      .query(query);

    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Supplier Payment Amendment Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// Update payment details (Remarks / details)
router.put("/update", async (req, res) => {
  try {
    const pool = req.pool;
    const { PaymentNo, Remarks } = req.body;

    if (!PaymentNo) {
      return res.status(400).json({ success: false, message: "PaymentNo is required." });
    }
    if (!Remarks || !Remarks.trim()) {
      return res.status(400).json({ success: false, message: "Please Enter the Details (Remarks)." });
    }

    const query = `
      UPDATE tbljobsheetpaymentdetail 
      SET Remarks = @Remarks
      WHERE PaymentNo = @PaymentNo
    `;

    await pool.request()
      .input("PaymentNo", sql.VarChar, PaymentNo)
      .input("Remarks", sql.VarChar, Remarks)
      .query(query);

    res.json({ success: true, message: "Payment Details Updated." });
  } catch (err) {
    console.error("Update Supplier Payment Amendment Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

module.exports = router;
