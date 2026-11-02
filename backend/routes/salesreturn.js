/** Developer: durairaj */
const express = require("express");
const router = express.Router();
const { sql, poolPromise } = require("../db");

// Helper to acquire database connection pool reliably
const getPool = async (req) => {
  if (req && req.pool) return req.pool;
  if (poolPromise) return await poolPromise;
  throw new Error("Database pool is not available.");
};

// POST Verify Salesman Password
router.post("/verifypassword", async (req, res) => {
  try {
    const { password } = req.body;
    if (!password) return res.status(400).json({ success: false, message: "Password required" });
    const pool = await getPool(req);
    const result = await pool.request()
      .input("Password", sql.VarChar, password)
      .query("SELECT SalesManCode, SalesManName, AllowSalesReturn FROM tblSalesman WHERE Password = @Password");
    if (result.recordset && result.recordset.length > 0) {
      res.json({ success: true, salesman: result.recordset[0] });
    } else {
      res.json({ success: false, message: "Invalid password" });
    }
  } catch (err) {
    res.status(500).json({ success: false, message: err.message });
  }
});

// GET Inventory Search List of Values matching VB6 frmHelpSearch("tblInventory", "SalesAmount <> 0")
router.get("/inventory-lov", async (req, res) => {
  try {
    const pool = await getPool(req);
    const query = `
      SELECT a.InventoryCode,
             a.Description,
             ISNULL(a.JobSheetNo, '') AS JobSheetNo,
             ISNULL(a.PurchaseBillNo, '') AS PurchaseBillNo,
             CASE a.Weight WHEN 0 THEN a.StoneWeight ELSE a.Weight END AS Weight,
             a.SizeCode,
             a.SizeType,
             ISNULL(a.SalesAmount, 0) AS SalesAmount
      FROM tblInventory a
      WHERE ISNULL(a.SalesAmount, 0) <> 0
      ORDER BY a.InventoryCode ASC
    `;
    const result = await pool.request().query(query);
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Inventory LOV Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// GET Sales Header by Invoice No
router.get("/salesheader/:billNo", async (req, res) => {
  try {
    const { billNo } = req.params;
    const pool = await getPool(req);

    // Check if bill already returned (only if tblSalesReturnHeader table exists)
    try {
      const checkRet = await pool.request()
        .input("InvoiceNo", sql.VarChar, billNo.trim())
        .query(`SELECT TOP 1 ReturnNo, RefNo FROM tblSalesReturnHeader WHERE InvoiceNo = @InvoiceNo`);
      if (checkRet.recordset && checkRet.recordset.length > 0) {
        return res.status(200).json({
          success: false,
          alreadyReturned: true,
          message: `${billNo.trim().toUpperCase()} Bill Already Returned ...`
        });
      }
    } catch (tblRetErr) {
      // If table doesn't have InvoiceNo or other schema deviation, log and continue safely
      console.warn("Check return header warning:", tblRetErr.message);
    }

    // Fetch Header
    const headerResult = await pool.request()
      .input("InvoiceNo", sql.VarChar, billNo.trim())
      .query(`SELECT * FROM tblSalesHeader WHERE InvoiceNo = @InvoiceNo`);

    if (!headerResult.recordset || headerResult.recordset.length === 0) {
      return res.status(200).json({ success: false, message: "Invoice not found." });
    }

    const header = headerResult.recordset[0];

    // Fetch Customer
    let customerInfo = {};
    if (header.CustomerCode) {
      try {
        const custResult = await pool.request()
          .input("CustomerCode", sql.VarChar, header.CustomerCode.trim())
          .query(`SELECT CustomerName, Address1, Address2, Address3, CustomerType FROM tblCustomer WHERE CustomerCode = @CustomerCode`);
        if (custResult.recordset && custResult.recordset.length > 0) {
          customerInfo = custResult.recordset[0];
        }
      } catch (cErr) {
        console.warn("Customer fetch warning:", cErr.message);
      }
    }

    // Fetch Member
    let memberInfo = {};
    if (header.MemberCode) {
      try {
        const memResult = await pool.request()
          .input("MemberCode", sql.VarChar, header.MemberCode.trim())
          .query(`SELECT MemberName, Address1, Address2, Address3, MemberType FROM tblMember WHERE MemberCode = @MemberCode`);
        if (memResult.recordset && memResult.recordset.length > 0) {
          memberInfo = memResult.recordset[0];
        }
      } catch (mErr) {
        console.warn("Member fetch warning:", mErr.message);
      }
    }

    // Fetch Global GST from tblCompany
    let globalGstPerc = 0;
    let companyShortCode = "";
    try {
      const companyResult = await pool.request().query(`SELECT TOP 1 GstPerc, CompanyShortCode FROM tblCompany`);
      if (companyResult.recordset && companyResult.recordset.length > 0) {
        globalGstPerc = parseFloat(companyResult.recordset[0].GstPerc || 0);
        companyShortCode = (companyResult.recordset[0].CompanyShortCode || "").trim();
      }
    } catch (e) {
      console.error("Failed to fetch global GST:", e);
    }

    res.json({
      success: true,
      header: header,
      customer: Object.keys(customerInfo).length > 0 ? customerInfo : memberInfo,
      globalGst: globalGstPerc,
      companyShortCode: companyShortCode
    });
  } catch (err) {
    console.error("Fetch Sales Header Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// GET Sales Detail by Invoice No
router.get("/salesdetail/:billNo", async (req, res) => {
  try {
    const { billNo } = req.params;
    const pool = await getPool(req);

    const detailsResult = await pool.request()
      .input("InvoiceNo", sql.VarChar, billNo.trim())
      .query(`SELECT * FROM tblSalesDetail WHERE InvoiceNo = @InvoiceNo`);

    res.json({
      success: true,
      data: detailsResult.recordset
    });
  } catch (err) {
    console.error("Fetch Sales Detail Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// GET Inventory Search
router.get("/inventory/:inventoryCode", async (req, res) => {
  try {
    const { inventoryCode } = req.params;
    const pool = await getPool(req);

    // Primary query on tblInventory with robust fallback on column availability
    let invRecord = null;
    try {
      const invResult = await pool.request()
        .input("InventoryCode", sql.VarChar, inventoryCode.trim())
        .query(`
          SELECT a.InventoryCode, a.Description, 
                 ISNULL(a.CostPerGram, 0) AS CostPerGram, 
                 ISNULL(a.TotalCost, 0) AS TotalCost, 
                 ISNULL(a.Weight, 0) AS Weight, 
                 ISNULL(a.StoneWeight, 0) AS StoneWeight,
                 1 AS PCS
          FROM tblInventory a
          WHERE a.InventoryCode = @InventoryCode
        `);
      if (invResult.recordset && invResult.recordset.length > 0) {
        invRecord = invResult.recordset[0];
      }
    } catch (invErr) {
      console.warn("Inventory query specific column fallback:", invErr.message);
      const invFallback = await pool.request()
        .input("InventoryCode", sql.VarChar, inventoryCode.trim())
        .query(`SELECT * FROM tblInventory WHERE InventoryCode = @InventoryCode`);
      if (invFallback.recordset && invFallback.recordset.length > 0) {
        const raw = invFallback.recordset[0];
        invRecord = {
          InventoryCode: raw.InventoryCode,
          Description: raw.Description || '',
          CostPerGram: raw.CostPerGram || 0,
          TotalCost: raw.TotalCost || 0,
          Weight: raw.Weight || raw.StoneWeight || 0,
          StoneWeight: raw.StoneWeight || 0,
          PCS: raw.QtyOnHand || 1
        };
      }
    }

    if (!invRecord) {
      return res.status(200).json({ success: false, message: "Inventory not found." });
    }

    // Matching VB frmSalesReturn.vb fncGetCustomerPrice:
    // Select InvoiceNo from tblSalesDetail where InventoryCode = @InventoryCode order by InvoiceDate desc / InvoiceNo desc
    let salesRecord = null;
    try {
      const salesResult = await pool.request()
        .input("InventoryCode", sql.VarChar, inventoryCode.trim())
        .query(`
          SELECT TOP 1 d.InvoiceNo, d.TotalValue, d.NetAmount, d.ActualSales, d.Gst, d.DetailDiscount, d.NetDiscount, d.TotalCost
          FROM tblSalesDetail d
          INNER JOIN tblSalesHeader h ON d.InvoiceNo = h.InvoiceNo
          WHERE d.InventoryCode = @InventoryCode
          ORDER BY d.InvoiceNo DESC
        `);
      if (salesResult.recordset && salesResult.recordset.length > 0) {
        salesRecord = salesResult.recordset[0];
      }
    } catch (sErr) {
      // Fallback query directly on tblSalesDetail
      try {
        const fallbackResult = await pool.request()
          .input("InventoryCode", sql.VarChar, inventoryCode.trim())
          .query(`
            SELECT TOP 1 d.InvoiceNo, d.TotalValue, d.NetAmount, d.ActualSales, d.Gst, d.DetailDiscount, d.NetDiscount, d.TotalCost
            FROM tblSalesDetail d
            WHERE d.InventoryCode = @InventoryCode
            ORDER BY d.InvoiceNo DESC
          `);
        if (fallbackResult.recordset && fallbackResult.recordset.length > 0) {
          salesRecord = fallbackResult.recordset[0];
        }
      } catch (fErr) {
        console.warn("Sales detail fetch warning:", fErr.message);
      }
    }

    res.json({
      success: true,
      inventory: invRecord,
      inv: invRecord,
      salesData: salesRecord,
      sales: salesRecord
    });
  } catch (err) {
    console.error("Fetch Inventory Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// GET Fetch Sales Return By Return No for Reprint
router.get("/fetch-return/:returnNo", async (req, res) => {
  try {
    const { returnNo } = req.params;
    const pool = await getPool(req);

    const result = await pool.request()
      .input("ReturnNo", sql.VarChar, returnNo.trim())
      .query(`SELECT * FROM tblSalesReturnHeader WHERE ReturnNo = @ReturnNo OR RefNo = @ReturnNo`);

    if (result.recordset && result.recordset.length > 0) {
      res.json({ success: true, data: result.recordset[0] });
    } else {
      res.status(200).json({ success: false, message: "This Return Number Not Found... Please Give Correct Return No" });
    }
  } catch (err) {
    console.error("Fetch Return Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// POST Save Sales Return
router.post("/save", async (req, res) => {
  const pool = await getPool(req);
  const transaction = new sql.Transaction(pool);

  try {
    const {
      billNo,
      totalWeight,
      returnQty,
      returnTotal,
      totalCost,
      gstAmount,
      netAmount,
      salesmanCode,
      customerCode,
      memberCode,
      customerName,
      userId,
      items,
      locationCode
    } = req.body;

    await transaction.begin();

    // 1. EXEC sp_SalesReturnHeader
    const reqHeader = new sql.Request(transaction);
    const headerRes = await reqHeader
      .input("LocationCode", sql.VarChar, locationCode || "HQ")
      .input("InvoiceNo", sql.VarChar, billNo || "")
      .input("TotalWeight", sql.Numeric(18, 4), totalWeight || 0)
      .input("ReturnQty", sql.Numeric(18, 4), returnQty || 0)
      .input("ReturnTotal", sql.Numeric(18, 4), returnTotal || 0)
      .input("TotalCost", sql.Numeric(18, 4), totalCost || 0)
      .input("GstAmount", sql.Numeric(18, 4), gstAmount || 0)
      .input("NetAmount", sql.Numeric(18, 4), netAmount || 0)
      .input("SalesmanCode", sql.VarChar, salesmanCode || "")
      .input("CustomerCode", sql.VarChar, customerCode || "")
      .input("MemberCode", sql.VarChar, memberCode || "")
      .input("CustomerName", sql.VarChar, customerName || "")
      .input("UserId", sql.VarChar, userId || "SYS")
      .query(`
        EXEC sp_SalesReturnHeader 
          @LocationCode, @InvoiceNo, @TotalWeight, @ReturnQty, @ReturnTotal, 
          @TotalCost, @GstAmount, @NetAmount, @SalesmanCode, @CustomerCode, 
          @MemberCode, @CustomerName, @UserId
      `);

    // Extract return number
    let sNextRetNo = "";
    if (headerRes.recordsets && headerRes.recordsets.length > 0) {
      for (const rs of headerRes.recordsets) {
        if (rs && rs.length > 0 && (rs[0].ReturnNo || rs[0].RefNo)) {
          sNextRetNo = rs[0].ReturnNo || rs[0].RefNo;
          break;
        }
      }
    }

    if (!sNextRetNo && headerRes.recordset && headerRes.recordset.length > 0 && (headerRes.recordset[0].ReturnNo || headerRes.recordset[0].RefNo)) {
      sNextRetNo = headerRes.recordset[0].ReturnNo || headerRes.recordset[0].RefNo;
    }

    if (!sNextRetNo) {
      throw new Error("Failed to generate Return No from sp_SalesReturnHeader");
    }

    // 2. EXEC sp_SalesReturnDetail for each item
    for (const item of items) {
      const reqDetail = new sql.Request(transaction);
      await reqDetail
        .input("ReturnNo", sql.VarChar, sNextRetNo)
        .input("InventoryCode", sql.VarChar, item.InventoryCode)
        .input("Description", sql.VarChar, item.Description || "")
        .input("Weight", sql.Numeric(18, 4), item.Weight || 0)
        .input("PCS", sql.Numeric(18, 4), item.PCS || 0)
        .input("TotalCost", sql.Numeric(18, 4), item.TotalCost || 0)
        .input("ReturnAmount", sql.Numeric(18, 4), item.ReturnAmount || 0)
        .input("DetailDiscount", sql.Numeric(18, 4), item.DetailDiscount || 0)
        .input("NetDiscount", sql.Numeric(18, 4), item.NetDiscount || 0)
        .input("NetAmount", sql.Numeric(18, 4), item.NetAmount || 0)
        .input("ActualSales", sql.Numeric(18, 4), item.ActualSales || 0)
        .input("Gst", sql.Numeric(18, 4), item.GST || 0)
        .input("SalesmanCode", sql.VarChar, salesmanCode || "")
        .input("InvoiceNo", sql.VarChar, billNo || "")
        .input("LocationCode", sql.VarChar, locationCode || "HQ")
        .input("UserId", sql.VarChar, userId || "SYS")
        .query(`
          EXEC sp_SalesReturnDetail 
            @ReturnNo, @InventoryCode, @Description, @Weight, @PCS, @TotalCost,
            @ReturnAmount, @DetailDiscount, @NetDiscount, @NetAmount, @ActualSales,
            @Gst, @SalesmanCode, @InvoiceNo, @LocationCode, @UserId
        `);
    }

    // 3. EXEC sp_SalesReturnDeposit
    const reqDeposit = new sql.Request(transaction);
    await reqDeposit
      .input("ReturnNo", sql.VarChar, sNextRetNo)
      .input("CustomerCode", sql.VarChar, customerCode || "")
      .input("Amount", sql.Numeric(18, 4), netAmount || 0)
      .input("Mode", sql.VarChar, "DEPOSIT")
      .input("UserId", sql.VarChar, userId || "SYS")
      .query(`
        EXEC sp_SalesReturnDeposit 
          @ReturnNo, @CustomerCode, @Amount, @Mode, @UserId
      `);

    // 4. EXEC sp_SalesBillCancelCustomerUpdate if billNo provided
    if (billNo && billNo.trim()) {
      try {
        const reqCustomerUpdate = new sql.Request(transaction);
        await reqCustomerUpdate
          .input("InvoiceNo", sql.VarChar, billNo.trim())
          .query(`EXEC sp_SalesBillCancelCustomerUpdate @InvoiceNo`);
      } catch (custUpdErr) {
        console.warn("sp_SalesBillCancelCustomerUpdate warning:", custUpdErr.message);
      }
    }

    await transaction.commit();
    res.json({ success: true, message: `Please take note the sales return no ${sNextRetNo}`, returnNo: sNextRetNo });
  } catch (err) {
    console.error("Save Sales Return Error:", err);
    try {
      await transaction.rollback();
    } catch (rerr) {
      console.error("Rollback failed:", rerr);
    }
    res.status(500).json({ success: false, message: "Database Save Error", error: err.message });
  }
});

module.exports = router;


