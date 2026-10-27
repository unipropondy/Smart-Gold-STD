const express = require("express");
const router = express.Router();
const { sql } = require("../db");

// POST Verify Salesman Password
router.post("/verifypassword", async (req, res) => {
  try {
    const { password } = req.body;
    if (!password) return res.status(400).json({ success: false, message: "Password required" });
    const pool = req.pool;
    const result = await pool.request()
      .input("Password", sql.VarChar, password)
      .query("SELECT SalesManCode, SalesManName FROM tblSalesman WHERE Password = @Password");
    if (result.recordset.length > 0) {
      res.json({ success: true, salesman: result.recordset[0] });
    } else {
      res.json({ success: false, message: "Invalid password" });
    }
  } catch (err) {
    res.status(500).json({ success: false, message: err.message });
  }
});
// GET Sales Header by Invoice No
router.get("/salesheader/:billNo", async (req, res) => {
  try {
    const { billNo } = req.params;
    const pool = req.pool;
    
    // Fetch Header
    const headerResult = await pool.request()
      .input("InvoiceNo", sql.VarChar, billNo.trim())
      .query(`SELECT * FROM tblSalesHeader WHERE InvoiceNo = @InvoiceNo`);
      
    if (headerResult.recordset.length === 0) {
      return res.status(404).json({ success: false, message: "Invoice not found." });
    }
    
    const header = headerResult.recordset[0];

    // Fetch Customer
    let customerInfo = {};
    if (header.CustomerCode) {
      const custResult = await pool.request()
        .input("CustomerCode", sql.VarChar, header.CustomerCode.trim())
        .query(`SELECT CustomerName, Address1, Address2, Address3, CustomerType FROM tblCustomer WHERE CustomerCode = @CustomerCode`);
      if (custResult.recordset.length > 0) {
        customerInfo = custResult.recordset[0];
      }
    }

    // Fetch Member
    let memberInfo = {};
    if (header.MemberCode) {
      const memResult = await pool.request()
        .input("MemberCode", sql.VarChar, header.MemberCode.trim())
        .query(`SELECT MemberName, Address1, Address2, Address3, MemberType FROM tblMember WHERE MemberCode = @MemberCode`);
      if (memResult.recordset.length > 0) {
        memberInfo = memResult.recordset[0];
      }
    }

    // Fetch Global GST from tblCompany
    let globalGstPerc = 0;
    try {
      const companyResult = await pool.request().query(`SELECT TOP 1 GstPerc FROM tblCompany`);
      if (companyResult.recordset.length > 0) {
        globalGstPerc = parseFloat(companyResult.recordset[0].GstPerc || 0);
      }
    } catch (e) {
      console.error("Failed to fetch global GST:", e);
    }

    res.json({
      success: true,
      header: header,
      customer: Object.keys(customerInfo).length > 0 ? customerInfo : memberInfo,
      globalGst: globalGstPerc
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
    const pool = req.pool;
    
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
    const pool = req.pool;
    
    const invResult = await pool.request()
      .input("InventoryCode", sql.VarChar, inventoryCode.trim())
      .query(`SELECT a.InventoryCode, a.Description, a.CostPerGram, a.TotalCost, a.Weight, a.StoneWeight, ISNULL(b.QtyOnHand, 1) AS PCS FROM tblInventory a LEFT JOIN tblInventoryStock b ON a.InventoryCode = b.InventoryCode AND a.LocationCode = b.LocationCode WHERE a.InventoryCode = @InventoryCode`);
      
    if (invResult.recordset.length === 0) {
      return res.status(404).json({ success: false, message: "Inventory not found." });
    }

    // Also get the customer price / associated sales detail
    const salesResult = await pool.request()
      .input("InventoryCode", sql.VarChar, inventoryCode.trim())
      .query(`SELECT InvoiceNo, TotalValue, NetAmount, ActualSales, Gst, DetailDiscount, NetDiscount, TotalCost FROM tblSalesDetail WHERE InventoryCode = @InventoryCode ORDER BY InvoiceNo DESC`);

    const invRecord = invResult.recordset[0];
    const salesRecord = salesResult.recordset.length > 0 ? salesResult.recordset[0] : null;

    res.json({
      success: true,
      inventory: invRecord,
      inv: invRecord, // compatibility for both inv and inventory
      salesData: salesRecord,
      sales: salesRecord // compatibility for both sales and salesData
    });
  } catch (err) {
    console.error("Fetch Inventory Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// POST Save Sales Return
router.post("/save", async (req, res) => {
  const pool = req.pool;
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
      .input("InvoiceNo", sql.VarChar, billNo)
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

    // The stored procedure should return the ReturnNo (sNextRetNo in VB)
    let sNextRetNo = "";
    if (headerRes.recordsets && headerRes.recordsets.length > 0) {
      for (const rs of headerRes.recordsets) {
        if (rs && rs.length > 0 && rs[0].ReturnNo) {
          sNextRetNo = rs[0].ReturnNo;
          break;
        }
      }
    }
    
    // Fallback if it comes as recordset instead of recordsets
    if (!sNextRetNo && headerRes.recordset && headerRes.recordset.length > 0 && headerRes.recordset[0].ReturnNo) {
      sNextRetNo = headerRes.recordset[0].ReturnNo;
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
        .input("Description", sql.VarChar, item.Description)
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
        .input("InvoiceNo", sql.VarChar, billNo)
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

    // 4. EXEC sp_SalesBillCancelCustomerUpdate
    const reqCustomerUpdate = new sql.Request(transaction);
    await reqCustomerUpdate
      .input("InvoiceNo", sql.VarChar, billNo)
      .query(`EXEC sp_SalesBillCancelCustomerUpdate @InvoiceNo`);

    await transaction.commit();
    res.json({ success: true, message: "Sales Return saved successfully.", returnNo: sNextRetNo });
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
