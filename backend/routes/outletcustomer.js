const express = require("express");
const router = express.Router();
const { sql } = require("../db");

// GET all customers for lookup
router.get("/", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query("SELECT * FROM tblCustomer ORDER BY CustomerCode ASC");
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Outlet Customer Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// GET customer by ID
router.get("/:id", async (req, res) => {
  try {
    const pool = req.pool;
    const { id } = req.params;
    const result = await pool.request()
      .input('CustomerCode', sql.VarChar, id)
      .query("SELECT * FROM tblCustomer WHERE CustomerCode = @CustomerCode");
    
    if (result.recordset.length > 0) {
      res.json({ success: true, data: result.recordset[0] });
    } else {
      res.json({ success: false, message: "Customer not found" });
    }
  } catch (err) {
    console.error("Fetch Customer by ID Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// POST insert or update customer
router.post("/", async (req, res) => {
  try {
    const pool = req.pool;
    const { 
        CustomerCode, CustomerName, CustomerType, Mobile, eMail, 
        NRICNo, Remarks, DateType
    } = req.body;

    if (!CustomerCode) {
      return res.status(400).json({ success: false, message: "Customer Code is required." });
    }

    // Check if exists
    const checkResult = await pool.request()
      .input('CustomerCode', sql.VarChar, CustomerCode)
      .query("SELECT CustomerCode FROM tblCustomer WHERE CustomerCode = @CustomerCode");
    
    const exists = checkResult.recordset.length > 0;

    const request = pool.request()
      .input('CustomerCode', sql.VarChar, CustomerCode)
      .input('CustomerName', sql.VarChar, CustomerName || '')
      .input('CustomerType', sql.VarChar, CustomerType || '')
      .input('Mobile', sql.VarChar, Mobile || '')
      .input('eMail', sql.VarChar, eMail || '')
      .input('NRICNo', sql.VarChar, NRICNo || '')
      .input('Remarks', sql.VarChar, Remarks || '')
      .input('DateType', sql.VarChar, DateType || '');

    if (exists) {
      // UPDATE existing customer
      const updateQuery = `
        UPDATE tblCustomer 
        SET 
          CustomerName = @CustomerName,
          CustomerType = @CustomerType,
          Mobile = @Mobile,
          eMail = @eMail,
          NRICNo = @NRICNo,
          Remarks = @Remarks,
          DateType = @DateType,
          ModifyDate = GETDATE()
        WHERE CustomerCode = @CustomerCode
      `;
      await request.query(updateQuery);
      return res.json({ success: true, message: "Customer updated successfully!" });
    } else {
      // INSERT new customer
      const insertQuery = `
        INSERT INTO tblCustomer (
          CustomerCode, CustomerName, CustomerType, Mobile, eMail, 
          NRICNo, Remarks, DateType, CreateUser, CreateDate, ModifyUser, ModifyDate
        ) VALUES (
          @CustomerCode, @CustomerName, @CustomerType, @Mobile, @eMail,
          @NRICNo, @Remarks, @DateType, 'sys', GETDATE(), 'sys', GETDATE()
        )
      `;
      await request.query(insertQuery);
      return res.json({ success: true, message: "Customer saved successfully!" });
    }
  } catch (err) {
    console.error("Save Outlet Customer Error:", err);
    res.status(500).json({ success: false, message: "Failed to save customer", error: err.message });
  }
});

module.exports = router;
