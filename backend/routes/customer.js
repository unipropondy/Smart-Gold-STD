const express = require("express");
const router = express.Router();
const { sql } = require("../db");

// ==========================================
// 1. GET Customer Types (from actual SQL Server data)
// ==========================================
router.get("/types", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query(`
      SELECT DISTINCT RTRIM(CustomerType) AS CustomerType 
      FROM tblCustomer 
      WHERE CustomerType IS NOT NULL AND LTRIM(RTRIM(CustomerType)) <> ''
      ORDER BY CustomerType
    `);
    
    let types = result.recordset.map(r => r.CustomerType).filter(Boolean);
    if (!types || types.length === 0) {
      types = ['DIAMOND', 'EMERALD', 'RUBY'];
    }
    res.json({ success: true, data: types });
  } catch (err) {
    console.error("Fetch Customer Types Error:", err);
    res.status(500).json({ success: false, message: "Error fetching customer types", error: err.message });
  }
});

// Alias for customer types
router.get("/customer-types", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query(`
      SELECT DISTINCT RTRIM(CustomerType) AS CustomerType 
      FROM tblCustomer 
      WHERE CustomerType IS NOT NULL AND LTRIM(RTRIM(CustomerType)) <> ''
      ORDER BY CustomerType
    `);
    let types = result.recordset.map(r => r.CustomerType).filter(Boolean);
    if (!types || types.length === 0) types = ['DIAMOND', 'EMERALD', 'RUBY'];
    res.json({ success: true, data: types });
  } catch (err) {
    res.status(500).json({ success: false, message: err.message });
  }
});

// ==========================================
// 2. GET Next Customer Code
// ==========================================
router.get("/next-code", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query(`
      SELECT MAX(TRY_CAST(RTRIM(CustomerCode) AS BIGINT)) AS maxNum
      FROM tblCustomer
      WHERE TRY_CAST(RTRIM(CustomerCode) AS BIGINT) IS NOT NULL
        AND LEN(RTRIM(CustomerCode)) <= 8
    `);
    const maxNum = result.recordset[0]?.maxNum;
    const nextCode = maxNum ? String(Number(maxNum) + 1) : "10001";
    res.json({ success: true, nextCode });
  } catch (err) {
    console.error("Fetch Next Customer Code Error:", err);
    res.status(500).json({ success: false, message: "Error generating customer code", error: err.message });
  }
});

// ==========================================
// 3. GET All Customers / Search
// ==========================================
router.get("/", async (req, res) => {
  try {
    const pool = req.pool;
    const { searchField = 'CustomerName', searchText = '', searchMode = 'Any' } = req.query;

    const allowedCols = {
      'CustomerName': 'CustomerName',
      'CustomerCode': 'CustomerCode',
      'NricNo': 'NricNo',
      'NRICNo': 'NricNo',
      'Mobile': 'Mobile',
      'Phone1': 'Phone1'
    };
    const col = allowedCols[searchField] || 'CustomerName';

    let query = `
      SELECT c.*, 
             ISNULL(v.RewardPoints, 0) as VwTotalRewardPoints, 
             ISNULL(v.RedeemPoints, 0) as VwTotalRedeemedPoints
      FROM tblCustomer c
      LEFT JOIN VwMemberPoints v ON RTRIM(c.CustomerCode) = RTRIM(v.CustomerCode)
    `;

    const request = pool.request();
    const cleanText = (searchText || '').trim();

    if (cleanText) {
      let pattern = `%${cleanText}%`;
      if (searchMode === 'Start') pattern = `${cleanText}%`;
      else if (searchMode === 'End') pattern = `%${cleanText}`;

      request.input('SearchText', sql.VarChar, pattern);
      query += ` WHERE RTRIM(c.${col}) LIKE @SearchText`;
    }

    query += ` ORDER BY c.CustomerCode ASC`;

    const result = await request.query(query);

    const processedData = result.recordset.map(item => {
      const { VwTotalRewardPoints, VwTotalRedeemedPoints, ...rest } = item;
      const totalReward = Number(VwTotalRewardPoints || item.TotalRewardPoints || 0);
      const totalRedeemed = Number(VwTotalRedeemedPoints || item.TotalRedeemedPoints || 0);
      return {
        ...rest,
        CustomerCode: (item.CustomerCode || '').trim(),
        CustomerName: (item.CustomerName || '').trim(),
        CustomerType: (item.CustomerType || '').trim(),
        Address1: (item.Address1 || '').trim(),
        Address2: (item.Address2 || '').trim(),
        Address3: (item.Address3 || '').trim(),
        Address4: (item.Address4 || '').trim(),
        NricNo: (item.NricNo || item.NRICNo || '').trim(),
        TotalRewardPoints: totalReward,
        TotalRedeemedPoints: totalRedeemed,
        BalancePoints: totalReward - totalRedeemed
      };
    });

    res.json({ success: true, count: processedData.length, data: processedData });
  } catch (err) {
    console.error("Fetch Customers Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// ==========================================
// 4. GET Customer by ID / CustomerCode
// ==========================================
router.get("/:id", async (req, res) => {
  try {
    const pool = req.pool;
    const { id } = req.params;
    const code = (id || '').trim();

    if (!code) {
      return res.status(400).json({ success: false, message: "Customer Code is required." });
    }

    const result = await pool.request()
      .input('CustomerCode', sql.VarChar, code)
      .query(`
        SELECT c.*, 
               ISNULL(v.RewardPoints, 0) as VwTotalRewardPoints, 
               ISNULL(v.RedeemPoints, 0) as VwTotalRedeemedPoints
        FROM tblCustomer c
        LEFT JOIN VwMemberPoints v ON RTRIM(c.CustomerCode) = RTRIM(v.CustomerCode)
        WHERE RTRIM(c.CustomerCode) = RTRIM(@CustomerCode)
      `);

    if (result.recordset.length > 0) {
      const row = result.recordset[0];
      const totalReward = Number(row.VwTotalRewardPoints || row.TotalRewardPoints || 0);
      const totalRedeemed = Number(row.VwTotalRedeemedPoints || row.TotalRedeemedPoints || 0);
      const balance = totalReward - totalRedeemed;

      res.json({
        success: true,
        data: {
          CustomerCode: (row.CustomerCode || '').trim(),
          CustomerType: (row.CustomerType || '').trim(),
          CustomerName: (row.CustomerName || '').trim(),
          Address1: (row.Address1 || '').trim(),
          Address2: (row.Address2 || '').trim(),
          Address3: (row.Address3 || '').trim(),
          Address4: (row.Address4 || '').trim(), // Nationality
          Phone1: (row.Phone1 || '').trim(),
          Phone2: (row.Phone2 || '').trim(),
          Mobile: (row.Mobile || '').trim(),
          Fax: (row.Fax || '').trim(),
          eMail: (row.eMail || '').trim(),
          Url: (row.Url || '').trim(),
          AccountType: (row.AccountType || 'N').trim(), // Contact: SMS=S, Phone=P, None=N
          GLAccount1: (row.GLAccount1 || 'Y').trim(), // Active: Y / N
          Remarks: (row.Remarks || '').trim(),
          NricNo: (row.NricNo || row.NRICNo || '').trim(),
          PassportNo: (row.PassPortNo || row.PassportNo || '').trim(),
          Gender: (row.Gender || 'M').trim(),
          DOB: row.DOB ? new Date(row.DOB).toISOString().split('T')[0] : '',
          DOA: row.DOA ? new Date(row.DOA).toISOString().split('T')[0] : '',
          DateType: (row.DateType || '').trim(), // Religion
          TotalRewardPoints: totalReward,
          TotalRedeemedPoints: totalRedeemed,
          BalancePoints: balance,
          CustomerImageName: (row.CustomerImageName || '').trim()
        }
      });
    } else {
      res.status(404).json({ success: false, message: "Customer not found." });
    }
  } catch (err) {
    console.error("Fetch Customer by ID Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// ==========================================
// 5. POST Create New Customer
// ==========================================
router.post("/", async (req, res) => {
  try {
    const pool = req.pool;
    const {
      CustomerCode, CustomerName, CustomerType, Address1, Address2, Address3, Address4,
      Phone1, Phone2, Mobile, Fax, eMail, Url, AccountType,
      GLAccount1, Remarks, NRICNo, NricNo, Gender, PassportNo, DOB, DOA, DateType,
      CustomerImageName
    } = req.body;

    const code = (CustomerCode || '').trim();
    if (!code) {
      return res.status(400).json({ success: false, message: "Customer Code is required." });
    }
    if (!CustomerName || !CustomerName.trim()) {
      return res.status(400).json({ success: false, message: "Customer Name is required." });
    }

    // Check if exists
    const checkResult = await pool.request()
      .input('CustomerCode', sql.VarChar, code)
      .query("SELECT CustomerCode FROM tblCustomer WHERE RTRIM(CustomerCode) = RTRIM(@CustomerCode)");

    const isUpdate = checkResult.recordset.length > 0;
    const finalNric = (NRICNo || NricNo || '').trim();

    if (isUpdate) {
      // Execute UPDATE
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
          NricNo = @NRICNo,
          Gender = @Gender,
          PassportNo = @PassportNo,
          DOB = @DOB,
          DOA = @DOA,
          DateType = @DateType,
          CustomerImageName = ISNULL(@CustomerImageName, CustomerImageName),
          ModifyUser = 'sys',
          ModifyDate = GETDATE()
        WHERE RTRIM(CustomerCode) = RTRIM(@CustomerCode)
      `;

      const request = pool.request()
        .input('CustomerCode', sql.VarChar, code)
        .input('CustomerName', sql.VarChar, (CustomerName || '').trim())
        .input('CustomerType', sql.VarChar, (CustomerType || 'DIAMOND').trim())
        .input('Address1', sql.VarChar, (Address1 || '').trim())
        .input('Address2', sql.VarChar, (Address2 || '').trim())
        .input('Address3', sql.VarChar, (Address3 || '').trim())
        .input('Address4', sql.VarChar, (Address4 || '').trim()) // Nationality
        .input('Phone1', sql.VarChar, (Phone1 || '').trim())
        .input('Phone2', sql.VarChar, (Phone2 || '').trim())
        .input('Mobile', sql.VarChar, (Mobile || '').trim())
        .input('Fax', sql.VarChar, (Fax || '').trim())
        .input('eMail', sql.VarChar, (eMail || '').trim())
        .input('Url', sql.VarChar, (Url || '').trim())
        .input('AccountType', sql.VarChar, (AccountType || 'N').trim())
        .input('GLAccount1', sql.VarChar, (GLAccount1 || 'Y').trim())
        .input('Remarks', sql.VarChar, (Remarks || '').trim())
        .input('NRICNo', sql.VarChar, finalNric)
        .input('Gender', sql.VarChar, (Gender || 'M').trim())
        .input('PassportNo', sql.VarChar, (PassportNo || '').trim())
        .input('DateType', sql.VarChar, (DateType || '').trim())
        .input('CustomerImageName', sql.VarChar, (CustomerImageName || null));

      request.input('DOB', sql.DateTime, DOB ? new Date(DOB) : null);
      request.input('DOA', sql.DateTime, DOA ? new Date(DOA) : null);

      await request.query(updateQuery);
      return res.json({ success: true, message: "Customer updated successfully.", customerCode: code });
    }

    // Execute INSERT
    const insertQuery = `
      INSERT INTO tblCustomer (
        CustomerCode, CustomerName, CustomerType, Address1, Address2, Address3, Address4, 
        Phone1, Phone2, Mobile, Fax, eMail, Url, AccountType, 
        GLAccount1, Remarks, NRICNo, Gender, PassportNo, DOB, DOA, DateType,
        CustomerImageName, CreateUser, CreateDate, ModifyUser, ModifyDate
      ) VALUES (
        @CustomerCode, @CustomerName, @CustomerType, @Address1, @Address2, @Address3, @Address4, 
        @Phone1, @Phone2, @Mobile, @Fax, @eMail, @Url, @AccountType, 
        @GLAccount1, @Remarks, @NRICNo, @Gender, @PassportNo, @DOB, @DOA, @DateType,
        @CustomerImageName, 'sys', GETDATE(), 'sys', GETDATE()
      )
    `;

    const request = pool.request()
      .input('CustomerCode', sql.VarChar, code)
      .input('CustomerName', sql.VarChar, (CustomerName || '').trim())
      .input('CustomerType', sql.VarChar, (CustomerType || 'DIAMOND').trim())
      .input('Address1', sql.VarChar, (Address1 || '').trim())
      .input('Address2', sql.VarChar, (Address2 || '').trim())
      .input('Address3', sql.VarChar, (Address3 || '').trim())
      .input('Address4', sql.VarChar, (Address4 || '').trim()) // Nationality
      .input('Phone1', sql.VarChar, (Phone1 || '').trim())
      .input('Phone2', sql.VarChar, (Phone2 || '').trim())
      .input('Mobile', sql.VarChar, (Mobile || '').trim())
      .input('Fax', sql.VarChar, (Fax || '').trim())
      .input('eMail', sql.VarChar, (eMail || '').trim())
      .input('Url', sql.VarChar, (Url || '').trim())
      .input('AccountType', sql.VarChar, (AccountType || 'N').trim())
      .input('GLAccount1', sql.VarChar, (GLAccount1 || 'Y').trim())
      .input('Remarks', sql.VarChar, (Remarks || '').trim())
      .input('NRICNo', sql.VarChar, finalNric)
      .input('Gender', sql.VarChar, (Gender || 'M').trim())
      .input('PassportNo', sql.VarChar, (PassportNo || '').trim())
      .input('DateType', sql.VarChar, (DateType || '').trim())
      .input('CustomerImageName', sql.VarChar, (CustomerImageName || null));

    request.input('DOB', sql.DateTime, DOB ? new Date(DOB) : null);
    request.input('DOA', sql.DateTime, DOA ? new Date(DOA) : null);

    await request.query(insertQuery);

    res.json({ success: true, message: "Customer created successfully.", customerCode: code });
  } catch (err) {
    console.error("Save Customer Error:", err);
    res.status(500).json({ success: false, message: "Failed to save customer", error: err.message });
  }
});

// ==========================================
// 6. PUT Update Customer
// ==========================================
const updateCustomerHandler = async (req, res) => {
  try {
    const pool = req.pool;
    const paramId = req.params.id;
    const {
      CustomerCode, CustomerName, CustomerType, Address1, Address2, Address3, Address4,
      Phone1, Phone2, Mobile, Fax, eMail, Url, AccountType,
      GLAccount1, Remarks, NRICNo, NricNo, Gender, PassportNo, DOB, DOA, DateType,
      CustomerImageName
    } = req.body;

    const code = (paramId || CustomerCode || '').trim();
    if (!code) {
      return res.status(400).json({ success: false, message: "Customer Code is required." });
    }

    const finalNric = (NRICNo || NricNo || '').trim();

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
        NricNo = @NRICNo,
        Gender = @Gender,
        PassportNo = @PassportNo,
        DOB = @DOB,
        DOA = @DOA,
        DateType = @DateType,
        CustomerImageName = ISNULL(@CustomerImageName, CustomerImageName),
        ModifyUser = 'sys',
        ModifyDate = GETDATE()
      WHERE RTRIM(CustomerCode) = RTRIM(@CustomerCode)
    `;

    const request = pool.request()
      .input('CustomerCode', sql.VarChar, code)
      .input('CustomerName', sql.VarChar, (CustomerName || '').trim())
      .input('CustomerType', sql.VarChar, (CustomerType || 'DIAMOND').trim())
      .input('Address1', sql.VarChar, (Address1 || '').trim())
      .input('Address2', sql.VarChar, (Address2 || '').trim())
      .input('Address3', sql.VarChar, (Address3 || '').trim())
      .input('Address4', sql.VarChar, (Address4 || '').trim())
      .input('Phone1', sql.VarChar, (Phone1 || '').trim())
      .input('Phone2', sql.VarChar, (Phone2 || '').trim())
      .input('Mobile', sql.VarChar, (Mobile || '').trim())
      .input('Fax', sql.VarChar, (Fax || '').trim())
      .input('eMail', sql.VarChar, (eMail || '').trim())
      .input('Url', sql.VarChar, (Url || '').trim())
      .input('AccountType', sql.VarChar, (AccountType || 'N').trim())
      .input('GLAccount1', sql.VarChar, (GLAccount1 || 'Y').trim())
      .input('Remarks', sql.VarChar, (Remarks || '').trim())
      .input('NRICNo', sql.VarChar, finalNric)
      .input('Gender', sql.VarChar, (Gender || 'M').trim())
      .input('PassportNo', sql.VarChar, (PassportNo || '').trim())
      .input('DateType', sql.VarChar, (DateType || '').trim())
      .input('CustomerImageName', sql.VarChar, (CustomerImageName || null));

    request.input('DOB', sql.DateTime, DOB ? new Date(DOB) : null);
    request.input('DOA', sql.DateTime, DOA ? new Date(DOA) : null);

    const result = await request.query(updateQuery);

    if (result.rowsAffected[0] === 0) {
      return res.status(404).json({ success: false, message: "Customer not found." });
    }

    res.json({ success: true, message: "Customer updated successfully.", customerCode: code });
  } catch (err) {
    console.error("Update Customer Error:", err);
    res.status(500).json({ success: false, message: "Failed to update customer", error: err.message });
  }
};

router.put("/:id", updateCustomerHandler);
router.put("/", updateCustomerHandler);

// ==========================================
// 7. DELETE Customer
// ==========================================
router.delete("/:id", async (req, res) => {
  try {
    const pool = req.pool;
    const { id } = req.params;
    const code = (id || '').trim();

    if (!code) {
      return res.status(400).json({ success: false, message: "Customer Code is required." });
    }

    // Check if customer has transactions in tblSalesHeader
    const tranCheck = await pool.request()
      .input('CustomerCode', sql.VarChar, code)
      .query("SELECT TOP 1 InvoiceNo FROM tblSalesHeader WHERE RTRIM(CustomerCode) = RTRIM(@CustomerCode)");

    if (tranCheck.recordset.length > 0) {
      return res.status(400).json({ 
        success: false, 
        message: "Cannot Delete Customer, Because This Customer Is Having Some Transaction" 
      });
    }

    // Check if customer has points transactions in tblMemberTransactionAJ
    const pointsCheck = await pool.request()
      .input('CustomerCode', sql.VarChar, code)
      .query("SELECT TOP 1 InvoiceNo FROM tblMemberTransactionAJ WHERE RTRIM(CustomerCode) = RTRIM(@CustomerCode)");

    if (pointsCheck.recordset.length > 0) {
      return res.status(400).json({ 
        success: false, 
        message: "Cannot Delete Customer, Because This Customer Is Having Some Transaction" 
      });
    }

    const deleteQuery = "DELETE FROM tblCustomer WHERE RTRIM(CustomerCode) = RTRIM(@CustomerCode)";
    const result = await pool.request()
      .input('CustomerCode', sql.VarChar, code)
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

// ==========================================
// 8. TAB 1: Sales Summary
// ==========================================
const salesSummaryHandler = async (req, res) => {
  try {
    const pool = req.pool;
    const { id } = req.params;
    const code = (id || '').trim();

    const result = await pool.request()
      .input('CustomerCode', sql.VarChar, code)
      .query(`
        SELECT 
          RTRIM(InvoiceNo) AS InvoiceNo, 
          CONVERT(varchar(10), InvoiceDate, 120) AS InvoiceDate, 
          ISNULL(TotalWeight, 0) AS Weight, 
          ISNULL(TotalPCS, 0) AS PCS, 
          ISNULL(NetTotal, 0) AS Price 
        FROM tblSalesHeader 
        WHERE RTRIM(CustomerCode) = RTRIM(@CustomerCode) 
        ORDER BY InvoiceDate DESC, InvoiceNo DESC
      `);
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Sales summary error:", err);
    res.json({ success: true, data: [], error: err.message });
  }
};

router.get("/:id/sales_summary", salesSummaryHandler);
router.get("/:id/sales-summary", salesSummaryHandler);

// ==========================================
// 9. TAB 2: Sales Detail
// ==========================================
const salesDetailHandler = async (req, res) => {
  try {
    const pool = req.pool;
    const { id } = req.params;
    const code = (id || '').trim();

    const result = await pool.request()
      .input('CustomerCode', sql.VarChar, code)
      .query(`
        SELECT 
          RTRIM(d.InvoiceNo) AS InvoiceNo, 
          CONVERT(varchar(10), h.InvoiceDate, 120) AS InvoiceDate, 
          RTRIM(d.InventoryCode) AS InventoryCode, 
          RTRIM(d.Description) AS Description, 
          ISNULL(d.Weight, 0) AS Weight, 
          ISNULL(d.PCS, 0) AS PCS, 
          ISNULL(d.Rate, 0) AS Rate, 
          ISNULL(d.NetAmount, 0) AS Amount 
        FROM tblSalesDetail d 
        INNER JOIN tblSalesHeader h ON RTRIM(d.InvoiceNo) = RTRIM(h.InvoiceNo) 
        WHERE RTRIM(h.CustomerCode) = RTRIM(@CustomerCode) 
        ORDER BY h.InvoiceDate DESC, d.InvoiceNo DESC
      `);
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Sales detail error:", err);
    res.json({ success: true, data: [], error: err.message });
  }
};

router.get("/:id/sales_detail", salesDetailHandler);
router.get("/:id/sales-detail", salesDetailHandler);

// ==========================================
// 10. TAB 3: Customer Points
// ==========================================
router.get("/:id/points", async (req, res) => {
  try {
    const pool = req.pool;
    const { id } = req.params;
    const code = (id || '').trim();

    const result = await pool.request()
      .input('CustomerCode', sql.VarChar, code)
      .query(`
        SELECT 
          RTRIM(InvoiceNo) AS InvoiceNo, 
          CONVERT(varchar(10), InvoiceDate, 120) AS InvoiceDate, 
          ISNULL(InvoiceAmount, 0) AS NetAmount, 
          ISNULL(RewardPoints, 0) AS EarnedPts, 
          ISNULL(RedeemPoints, 0) AS RedeemedPts, 
          CONVERT(varchar(10), ExpiryDate, 120) AS ExpiryDate, 
          ISNULL(RTRIM(Remarks), '') AS Remarks, 
          ISNULL(RTRIM(Redeemed), 'N') AS Redeemed, 
          ISNULL(InvoiceRedeemedPoints, 0) AS InvoiceRedeemedPoints 
        FROM tblMemberTransactionAJ 
        WHERE RTRIM(CustomerCode) = RTRIM(@CustomerCode) 
        ORDER BY InvoiceDate DESC, InvoiceNo DESC
      `);
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Points fetch error:", err);
    res.json({ success: true, data: [], error: err.message });
  }
});

// ==========================================
// 11. KYC Details (Add KYC / Show KYC)
// ==========================================
router.get("/:id/kyc", async (req, res) => {
  try {
    const pool = req.pool;
    const { id } = req.params;
    const code = (id || '').trim();

    const result = await pool.request()
      .input('CustomerCode', sql.VarChar, code)
      .query(`
        SELECT CustomerCode, CustomerName, NricNo, PassPortNo, Address1, Address2, Address3, Address4 AS Nationality,
               DOB, Remarks, CustomerImageName
        FROM tblCustomer
        WHERE RTRIM(CustomerCode) = RTRIM(@CustomerCode)
      `);

    if (result.recordset.length > 0) {
      const c = result.recordset[0];
      const nric = (c.NricNo || '').trim();
      const passport = (c.PassPortNo || '').trim();
      res.json({
        success: true,
        data: {
          CustomerCode: (c.CustomerCode || '').trim(),
          CustomerName: (c.CustomerName || '').trim(),
          NricNo: nric,
          PassportNo: passport,
          Address: [(c.Address1 || '').trim(), (c.Address2 || '').trim(), (c.Address3 || '').trim()].filter(Boolean).join(', '),
          Nationality: (c.Nationality || '').trim(),
          DOB: c.DOB ? new Date(c.DOB).toISOString().split('T')[0] : '',
          CustomerImageName: (c.CustomerImageName || '').trim(),
          KycStatus: (nric || passport) ? 'Verified' : 'Pending',
          Remarks: (c.Remarks || '').trim()
        }
      });
    } else {
      res.status(404).json({ success: false, message: "Customer not found" });
    }
  } catch (err) {
    console.error("KYC fetch error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

router.post("/:id/kyc", async (req, res) => {
  try {
    const pool = req.pool;
    const { id } = req.params;
    const code = (id || '').trim();
    const { NricNo, PassportNo, Nationality, Address, DOB, Remarks, CustomerImageName } = req.body;

    const request = pool.request()
      .input('CustomerCode', sql.VarChar, code)
      .input('NricNo', sql.VarChar, (NricNo || '').trim())
      .input('PassportNo', sql.VarChar, (PassportNo || '').trim())
      .input('Nationality', sql.VarChar, (Nationality || '').trim())
      .input('Address', sql.VarChar, (Address || '').trim())
      .input('Remarks', sql.VarChar, (Remarks || '').trim())
      .input('CustomerImageName', sql.VarChar, (CustomerImageName || null));

    request.input('DOB', sql.DateTime, DOB ? new Date(DOB) : null);

    await request.query(`
      UPDATE tblCustomer SET
        NricNo = CASE WHEN @NricNo <> '' THEN @NricNo ELSE NricNo END,
        PassPortNo = CASE WHEN @PassportNo <> '' THEN @PassportNo ELSE PassPortNo END,
        Address4 = CASE WHEN @Nationality <> '' THEN @Nationality ELSE Address4 END,
        Address1 = CASE WHEN @Address <> '' THEN @Address ELSE Address1 END,
        DOB = CASE WHEN @DOB IS NOT NULL THEN @DOB ELSE DOB END,
        Remarks = CASE WHEN @Remarks <> '' THEN @Remarks ELSE Remarks END,
        CustomerImageName = ISNULL(@CustomerImageName, CustomerImageName),
        ModifyUser = 'sys',
        ModifyDate = GETDATE()
      WHERE RTRIM(CustomerCode) = RTRIM(@CustomerCode)
    `);

    res.json({ success: true, message: "KYC details saved successfully." });
  } catch (err) {
    console.error("KYC save error:", err);
    res.status(500).json({ success: false, message: "Failed to save KYC", error: err.message });
  }
});

module.exports = router;
