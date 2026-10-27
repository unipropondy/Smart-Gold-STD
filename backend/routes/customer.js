const express = require("express");
const router = express.Router();
const { sql } = require("../db");

router.get("/debug/runsql", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query(req.query.sql || "SELECT 1 as test");
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    res.json({ success: false, error: err.message });
  }
});

router.get("/debug/sales_header", async (req, res) => {
  try {
    const pool = req.pool;
    const q = req.query.q || `
      SELECT TOP 5 
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
    `;
    const result = await pool.request().query(q);
    res.json({ success: true, count: result.recordset.length, data: result.recordset });
  } catch (err) {
    res.json({ success: false, error: err.message });
  }
});

router.get("/debug/points_tables", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query("SELECT table_name FROM information_schema.tables WHERE table_name LIKE '%Point%' OR table_name LIKE '%Reward%'");
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    res.json({ success: false, error: err.message });
  }
});

router.get("/debug/cols/:table", async (req, res) => {
  try {
    const pool = req.pool;
    const { table } = req.params;
    const result = await pool.request().query(`SELECT column_name FROM information_schema.columns WHERE table_name = '${table}'`);
    res.json({ success: true, data: result.recordset.map(r => r.column_name) });
  } catch (err) {
    res.json({ success: false, error: err.message });
  }
});

// GET all customers
router.get("/", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query(`
      SELECT c.*, 
             ISNULL(v.RewardPoints, 0) as VwTotalRewardPoints, 
             ISNULL(v.RedeemPoints, 0) as VwTotalRedeemedPoints
      FROM tblCustomer c
      LEFT JOIN VwMemberPoints v ON c.CustomerCode = v.CustomerCode
      ORDER BY c.CustomerCode ASC
    `);
    
    // Process results to overwrite any existing TotalRewardPoints from c.* with the updated View points
    const processedData = result.recordset.map(item => {
      const { VwTotalRewardPoints, VwTotalRedeemedPoints, ...rest } = item;
      return {
        ...rest,
        TotalRewardPoints: VwTotalRewardPoints,
        TotalRedeemedPoints: VwTotalRedeemedPoints
      };
    });

    res.json({ success: true, data: processedData });
  } catch (err) {
    console.error("Fetch Customer Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// POST insert new customer
router.post("/", async (req, res) => {
  try {
    const pool = req.pool;
    const {
      CustomerCode, CustomerName, CustomerType, Address1, Address2, Address3, Address4,
      Phone1, Phone2, Mobile, Fax, eMail, Url, AccountType,
      GLAccount1, Remarks, NRICNo, Gender, PassportNo, DOB, DOA, DateType
    } = req.body;

    if (!CustomerCode) {
      return res.status(400).json({ success: false, message: "Customer Code is required." });
    }

    // Check if exists
    const checkResult = await pool.request()
      .input('CustomerCode', sql.VarChar, CustomerCode)
      .query("SELECT CustomerCode FROM tblCustomer WHERE CustomerCode = @CustomerCode");

    if (checkResult.recordset.length > 0) {
      return res.status(400).json({ success: false, message: "CustomerCode already exists." });
    }

    const insertQuery = `
      INSERT INTO tblCustomer (
        CustomerCode, CustomerName, CustomerType, Address1, Address2, Address3, Address4, 
        Phone1, Phone2, Mobile, Fax, eMail, Url, AccountType, 
        GLAccount1, Remarks, NRICNo, Gender, PassportNo, DOB, DOA, DateType,
        CreateUser, CreateDate, ModifyUser, ModifyDate
      ) VALUES (
        @CustomerCode, @CustomerName, @CustomerType, @Address1, @Address2, @Address3, @Address4, 
        @Phone1, @Phone2, @Mobile, @Fax, @eMail, @Url, @AccountType, 
        @GLAccount1, @Remarks, @NRICNo, @Gender, @PassportNo, @DOB, @DOA, @DateType,
        'sys', GETDATE(), 'sys', GETDATE()
      )
    `;

    const request = pool.request()
      .input('CustomerCode', sql.VarChar, CustomerCode)
      .input('CustomerName', sql.VarChar, CustomerName || '')
      .input('CustomerType', sql.VarChar, CustomerType || '')
      .input('Address1', sql.VarChar, Address1 || '')
      .input('Address2', sql.VarChar, Address2 || '')
      .input('Address3', sql.VarChar, Address3 || '')
      .input('Address4', sql.VarChar, Address4 || '') // Nationality
      .input('Phone1', sql.VarChar, Phone1 || '')
      .input('Phone2', sql.VarChar, Phone2 || '')
      .input('Mobile', sql.VarChar, Mobile || '')
      .input('Fax', sql.VarChar, Fax || '')
      .input('eMail', sql.VarChar, eMail || '')
      .input('Url', sql.VarChar, Url || '')
      .input('AccountType', sql.VarChar, AccountType || 'N') // Contact
      .input('GLAccount1', sql.VarChar, GLAccount1 || 'Y') // Active
      .input('Remarks', sql.VarChar, Remarks || '')
      .input('NRICNo', sql.VarChar, NRICNo || '')
      .input('Gender', sql.VarChar, Gender || 'M')
      .input('DateType', sql.VarChar, DateType || '')
      .input('PassportNo', sql.VarChar, PassportNo || '');

    if (DOB) {
      request.input('DOB', sql.DateTime, new Date(DOB));
    } else {
      request.input('DOB', sql.DateTime, null);
    }

    if (DOA) {
      request.input('DOA', sql.DateTime, new Date(DOA));
    } else {
      request.input('DOA', sql.DateTime, null);
    }

    await request.query(insertQuery);

    res.json({ success: true, message: "Customer created successfully." });
  } catch (err) {
    console.error("Insert Customer Error:", err);
    res.status(500).json({ success: false, message: "Failed to create customer", error: err.message });
  }
});

// PUT update customer
router.put("/", async (req, res) => {
  try {
    const pool = req.pool;
    const {
      CustomerCode, CustomerName, CustomerType, Address1, Address2, Address3, Address4,
      Phone1, Phone2, Mobile, Fax, eMail, Url, AccountType,
      GLAccount1, Remarks, NRICNo, Gender, PassportNo, DOB, DOA, DateType
    } = req.body;

    if (!CustomerCode) {
      return res.status(400).json({ success: false, message: "Customer Code is required." });
    }

    const updateQuery = `
      UPDATE tblCustomer SET
        CustomerName = @CustomerName,
        CustomerType = @CustomerType,
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
        AccountType = @AccountType,
        GLAccount1 = @GLAccount1,
        Remarks = @Remarks,
        NRICNo = @NRICNo,
        Gender = @Gender,
        PassportNo = @PassportNo,
        DOB = @DOB,
        DOA = @DOA,
        DateType = @DateType,
        ModifyUser = 'sys',
        ModifyDate = GETDATE()
      WHERE CustomerCode = @CustomerCode
    `;

    const request = pool.request()
      .input('CustomerCode', sql.VarChar, CustomerCode)
      .input('CustomerName', sql.VarChar, CustomerName || '')
      .input('CustomerType', sql.VarChar, CustomerType || '')
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
      .input('AccountType', sql.VarChar, AccountType || 'N')
      .input('GLAccount1', sql.VarChar, GLAccount1 || 'Y')
      .input('Remarks', sql.VarChar, Remarks || '')
      .input('NRICNo', sql.VarChar, NRICNo || '')
      .input('Gender', sql.VarChar, Gender || 'M')
      .input('DateType', sql.VarChar, DateType || '')
      .input('PassportNo', sql.VarChar, PassportNo || '');

    if (DOB) {
      request.input('DOB', sql.DateTime, new Date(DOB));
    } else {
      request.input('DOB', sql.DateTime, null);
    }

    if (DOA) {
      request.input('DOA', sql.DateTime, new Date(DOA));
    } else {
      request.input('DOA', sql.DateTime, null);
    }

    const result = await request.query(updateQuery);

    if (result.rowsAffected[0] === 0) {
      return res.status(404).json({ success: false, message: "Customer not found." });
    }

    res.json({ success: true, message: "Customer updated successfully." });
  } catch (err) {
    console.error("Update Customer Error:", err);
    res.status(500).json({ success: false, message: "Failed to update customer", error: err.message });
  }
});

// DELETE customer
router.delete("/:id", async (req, res) => {
  try {
    const pool = req.pool;
    const { id } = req.params;

    if (!id) {
      return res.status(400).json({ success: false, message: "Customer Code is required." });
    }

    // Check if customer has transactions in tblSalesHeader
    const tranCheck = await pool.request()
      .input('CustomerCode', sql.VarChar, id)
      .query("SELECT TOP 1 * FROM tblSalesHeader WHERE CustomerCode = @CustomerCode");

    if (tranCheck.recordset.length > 0) {
      return res.status(400).json({ success: false, message: "Cannot Delete Customer, Because This Customer Is Having Some Transaction" });
    }

    const deleteQuery = "DELETE FROM tblCustomer WHERE CustomerCode = @CustomerCode";

    const result = await pool.request()
      .input('CustomerCode', sql.VarChar, id)
      .query(deleteQuery);

    if (result.rowsAffected[0] === 0) {
      return res.status(404).json({ success: false, message: "Customer not found." });
    }

    res.json({ success: true, message: "Customer deleted successfully." });
  } catch (err) {
    console.error("Delete Customer Error:", err);
    res.status(500).json({ success: false, message: "Failed to delete customer", error: err.message });
  }
});

router.get("/debug/sales_header", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query("SELECT TOP 5 * FROM tblSalesHeader");
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    res.json({ success: false, error: err.message });
  }
});

router.get("/debug/points_tables", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query("SELECT table_name FROM information_schema.tables WHERE table_name LIKE '%Point%' OR table_name LIKE '%Reward%'");
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    res.json({ success: false, error: err.message });
  }
});

// GET sales summary for customer
router.get("/:id/sales_summary", async (req, res) => {
  try {
    const pool = req.pool;
    const { id } = req.params;
    const result = await pool.request()
      .input('CustomerCode', sql.VarChar, id)
      .query("SELECT InvoiceNo, InvoiceDate, TotalWeight as Weight, TotalPCS as PCS, NetTotal as Price FROM tblSalesHeader WHERE CustomerCode = @CustomerCode ORDER BY InvoiceDate DESC");
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.warn("Could not fetch sales summary, returning empty", err.message);
    res.json({ success: true, data: [], error: err.message });
  }
});

// GET sales detail for customer
router.get("/:id/sales_detail", async (req, res) => {
  try {
    const pool = req.pool;
    const { id } = req.params;
    const result = await pool.request()
      .input('CustomerCode', sql.VarChar, id)
      .query(`
        SELECT d.InventoryCode, d.Description, d.Weight, d.PCS, d.NetAmount as Amount 
        FROM tblSalesDetail d 
        INNER JOIN tblSalesHeader h ON d.InvoiceNo = h.InvoiceNo 
        WHERE h.CustomerCode = @CustomerCode 
        ORDER BY h.InvoiceDate DESC
      `);
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.warn("Could not fetch sales details, returning empty", err.message);
    res.json({ success: true, data: [], error: err.message });
  }
});

// GET points for customer
router.get("/:id/points", async (req, res) => {
  try {
    const pool = req.pool;
    const { id } = req.params;
    const result = await pool.request()
      .input('CustomerCode', sql.VarChar, id)
      .query("SELECT InvoiceNo, InvoiceDate, InvoiceAmount as NetAmount, RewardPoints as EarnedPts, RedeemPoints as RedeemedPts, ExpiryDate, Remarks, Redeemed, InvoiceRedeemedPoints FROM tblCustomerPoints WHERE CustomerCode = @CustomerCode ORDER BY InvoiceDate DESC");
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.warn("Could not fetch points, returning empty", err.message);
    res.json({ success: true, data: [], error: err.message });
  }
});

module.exports = router;
