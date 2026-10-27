const express = require("express");
const router = express.Router();
const { sql } = require("../db");

// GET all suppliers
router.get("/", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query("SELECT * FROM tblSupplier ORDER BY SupplierCode ASC");
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Supplier Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// POST insert new supplier
router.post("/", async (req, res) => {
  try {
    const pool = req.pool;
    const { 
        SupplierCode, SupplierName, Address1, Address2, Address3, Address4, 
        Phone1, Phone2, Mobile, Fax, eMail, Url, Terms, AccountType, 
        GLAccount1, GLAccount2, Remarks, GSTType, NRICNo, Gender, PassportNo, DOB 
    } = req.body;

    if (!SupplierCode) {
      return res.status(400).json({ success: false, message: "Supplier Code is required." });
    }

    // Check if exists
    const checkResult = await pool.request()
      .input('SupplierCode', sql.VarChar, SupplierCode)
      .query("SELECT SupplierCode FROM tblSupplier WHERE SupplierCode = @SupplierCode");
    
    if (checkResult.recordset.length > 0) {
      return res.status(400).json({ success: false, message: "SupplierCode already exists." });
    }

    const insertQuery = `
      INSERT INTO tblSupplier (
        SupplierCode, SupplierName, Address1, Address2, Address3, Address4, 
        Phone1, Phone2, Mobile, Fax, eMail, Url, Terms, AccountType, 
        GLAccount1, GLAccount2, Remarks, GSTType, NRICNo, Gender, PassportNo, DOB,
        CreateUser, CreateDate, ModifyUser, ModifyDate
      ) VALUES (
        @SupplierCode, @SupplierName, @Address1, @Address2, @Address3, @Address4, 
        @Phone1, @Phone2, @Mobile, @Fax, @eMail, @Url, @Terms, @AccountType, 
        @GLAccount1, @GLAccount2, @Remarks, @GSTType, @NRICNo, @Gender, @PassportNo, @DOB,
        'sys', GETDATE(), 'sys', GETDATE()
      )
    `;

    const request = pool.request()
      .input('SupplierCode', sql.VarChar, SupplierCode)
      .input('SupplierName', sql.VarChar, SupplierName || '')
      .input('Address1', sql.VarChar, Address1 || '')
      .input('Address2', sql.VarChar, Address2 || '')
      .input('Address3', sql.VarChar, Address3 || '')
      .input('Address4', sql.VarChar, Address4 || '')
      .input('Phone1', sql.VarChar, Phone1 || '')
      .input('Phone2', sql.VarChar, Phone2 || '')
      .input('Mobile', sql.VarChar, Mobile || '')
      .input('Fax', sql.VarChar, Fax || '')
      .input('eMail', sql.VarChar, eMail || '')
      .input('Url', sql.VarChar, Url || '')
      .input('Terms', sql.VarChar, Terms || '')
      .input('AccountType', sql.VarChar, AccountType || '')
      .input('GLAccount1', sql.VarChar, GLAccount1 || '')
      .input('GLAccount2', sql.VarChar, GLAccount2 || '')
      .input('Remarks', sql.VarChar, Remarks || '')
      .input('GSTType', sql.VarChar, GSTType || '')
      .input('NRICNo', sql.VarChar, NRICNo || '')
      .input('Gender', sql.VarChar, Gender || '')
      .input('PassportNo', sql.VarChar, PassportNo || '');

    if (DOB) {
        request.input('DOB', sql.DateTime, new Date(DOB));
    } else {
        request.input('DOB', sql.DateTime, null);
    }

    await request.query(insertQuery);

    res.json({ success: true, message: "Supplier created successfully." });
  } catch (err) {
    console.error("Insert Supplier Error:", err);
    res.status(500).json({ success: false, message: "Failed to create supplier", error: err.message });
  }
});

// PUT update supplier
router.put("/", async (req, res) => {
  try {
    const pool = req.pool;
    const { 
        SupplierCode, SupplierName, Address1, Address2, Address3, Address4, 
        Phone1, Phone2, Mobile, Fax, eMail, Url, Terms, AccountType, 
        GLAccount1, GLAccount2, Remarks, GSTType, NRICNo, Gender, PassportNo, DOB 
    } = req.body;
    
    if (!SupplierCode) return res.status(400).json({ success: false, message: "Supplier Code is required" });

    const updateQuery = `
      UPDATE tblSupplier SET 
        SupplierName = @SupplierName,
        Address1 = @Address1,
        Address2 = @Address2,
        Address3 = @Address3,
        Address4 = @Address4,
        Phone1 = @Phone1,
        Phone2 = @Phone2,
        Mobile = @Mobile,
        Fax = @Fax,
        eMail = @eMail,
        Url = @Url,
        Terms = @Terms,
        AccountType = @AccountType,
        GLAccount1 = @GLAccount1,
        GLAccount2 = @GLAccount2,
        Remarks = @Remarks,
        GSTType = @GSTType,
        NRICNo = @NRICNo,
        Gender = @Gender,
        PassportNo = @PassportNo,
        DOB = @DOB,
        ModifyUser = 'sys',
        ModifyDate = GETDATE()
      WHERE SupplierCode = @SupplierCode
    `;

    const request = pool.request()
      .input('SupplierCode', sql.VarChar, SupplierCode)
      .input('SupplierName', sql.VarChar, SupplierName || '')
      .input('Address1', sql.VarChar, Address1 || '')
      .input('Address2', sql.VarChar, Address2 || '')
      .input('Address3', sql.VarChar, Address3 || '')
      .input('Address4', sql.VarChar, Address4 || '')
      .input('Phone1', sql.VarChar, Phone1 || '')
      .input('Phone2', sql.VarChar, Phone2 || '')
      .input('Mobile', sql.VarChar, Mobile || '')
      .input('Fax', sql.VarChar, Fax || '')
      .input('eMail', sql.VarChar, eMail || '')
      .input('Url', sql.VarChar, Url || '')
      .input('Terms', sql.VarChar, Terms || '')
      .input('AccountType', sql.VarChar, AccountType || '')
      .input('GLAccount1', sql.VarChar, GLAccount1 || '')
      .input('GLAccount2', sql.VarChar, GLAccount2 || '')
      .input('Remarks', sql.VarChar, Remarks || '')
      .input('GSTType', sql.VarChar, GSTType || '')
      .input('NRICNo', sql.VarChar, NRICNo || '')
      .input('Gender', sql.VarChar, Gender || '')
      .input('PassportNo', sql.VarChar, PassportNo || '');
      
    if (DOB) {
        request.input('DOB', sql.DateTime, new Date(DOB));
    } else {
        request.input('DOB', sql.DateTime, null);
    }

    const result = await request.query(updateQuery);

    if (result.rowsAffected[0] === 0) {
      return res.status(404).json({ success: false, message: "Supplier not found." });
    }

    res.json({ success: true, message: "Supplier updated successfully." });
  } catch (err) {
    console.error("Update Supplier Error:", err);
    res.status(500).json({ success: false, message: "Failed to update supplier", error: err.message });
  }
});

// DELETE supplier
router.delete("/", async (req, res) => {
  try {
    const pool = req.pool;
    const { id } = req.query;

    if (!id) return res.status(400).json({ success: false, message: "Supplier Code required" });

    const deleteQuery = "DELETE FROM tblSupplier WHERE SupplierCode = @SupplierCode";
    
    const result = await pool.request()
      .input('SupplierCode', sql.VarChar, id)
      .query(deleteQuery);

    if (result.rowsAffected[0] === 0) {
      return res.status(404).json({ success: false, message: "Supplier not found" });
    }

    res.json({ success: true, message: "Supplier deleted successfully." });
  } catch (err) {
    console.error("Delete Supplier Error:", err);
    res.status(500).json({ success: false, message: "Failed to delete supplier", error: err.message });
  }
});

// GET supplier purchases summary
router.get("/:supplierCode/purchases", async (req, res) => {
  try {
    const pool = req.pool;
    const { supplierCode } = req.params;
    const result = await pool.request()
      .input('SupplierCode', sql.VarChar, supplierCode)
      .query(`
        SELECT JobSheetNo, PurchaseBillNo, PurchaseBillDate, TotalWeight24K, TotalPCS, TotalLocalCost
        FROM tblJobSheetHeader
        WHERE SupplierCode = @SupplierCode
        ORDER BY JobSheetDate DESC
      `);
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Supplier Purchases Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// GET supplier purchase details by JobSheetNo
router.get("/:supplierCode/purchases/:jobSheetNo", async (req, res) => {
  try {
    const pool = req.pool;
    const { jobSheetNo } = req.params;
    let result = await pool.request()
      .input('JobSheetNo', sql.VarChar, jobSheetNo)
      .query(`
        SELECT CatalogueCode as Inventorycode, Description, Weight, PCS as Pcs, TotalCost
        FROM tblJobSheetDetail
        WHERE JobSheetNo = @JobSheetNo
      `);

    if (result.recordset.length === 0) {
      result = await pool.request()
        .input('JobSheetNo', sql.VarChar, jobSheetNo)
        .query(`
          SELECT InventoryCode as Inventorycode, Description, Weight, PCS as Pcs, TotalCost
          FROM tblPurchase
          WHERE JobSheetNo = @JobSheetNo
        `);
    }
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Supplier Purchase Details Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

module.exports = router;
