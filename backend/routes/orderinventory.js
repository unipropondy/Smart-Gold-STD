const express = require("express");
const router = express.Router();
const { sql } = require("../db");

// 1. GET all orders for lookup (Order No search)
router.get("/orders", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query(`
      SELECT DISTINCT 
        OrderNo, 
        OrderDate, 
        CustomerCode, 
        SupplierCode, 
        TotalCostEstimated, 
        AdvancePaid, 
        Status, 
        Description, 
        WeightEstimated 
      FROM vwInventoryOrder 
      WHERE Status IN ('D', 'P', 'R') AND AdvancePaid < TotalCostEstimated
      ORDER BY OrderNo
    `);
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Orders LOV Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// 2. GET unassigned order details (Description search)
router.get("/order-details/:orderNo", async (req, res) => {
  try {
    const { orderNo } = req.params;
    const pool = req.pool;
    const result = await pool.request()
      .input("OrderNo", sql.VarChar, orderNo)
      .query(`
        SELECT 
          OrderInventoryCode, 
          Description, 
          EstimatedCost, 
          WeightEstimated 
        FROM tblInventoryOrderDetail 
        WHERE OrderNo = @OrderNo AND (InventoryCode = '' OR InventoryCode IS NULL)
      `);
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Order Details LOV Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// 3. GET active inventory stock (Inventory Code search)
router.get("/inventory-stock", async (req, res) => {
  try {
    const pool = req.pool;
    const locationCode = req.query.locationCode || "HQ";
    const result = await pool.request()
      .input("LocationCode", sql.VarChar, locationCode)
      .query(`
        SELECT 
          InventoryCode, 
          Description, 
          SupplierCode, 
          Weight 
        FROM vwInventoryStock 
        WHERE QtyOnHand > 0 AND LocationCode = @LocationCode
        ORDER BY InventoryCode
      `);
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Inventory Stock LOV Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// 4. GET customer details by customer code
router.get("/customer/:customerCode", async (req, res) => {
  try {
    const { customerCode } = req.params;
    const pool = req.pool;
    const result = await pool.request()
      .input("CustomerCode", sql.VarChar, customerCode)
      .query(`
        SELECT 
          CustomerCode, 
          CustomerName, 
          Address1, 
          Address2, 
          Address3, 
          CustomerType 
        FROM tblCustomer 
        WHERE CustomerCode = @CustomerCode
      `);
    if (result.recordset.length === 0) {
      return res.status(404).json({ success: false, message: "Customer not found" });
    }
    res.json({ success: true, data: result.recordset[0] });
  } catch (err) {
    console.error("Fetch Customer Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// 5. GET inventory details by inventory code
router.get("/inventory/:inventoryCode", async (req, res) => {
  try {
    const { inventoryCode } = req.params;
    console.log("[DEBUG] Fetch inventoryCode:", JSON.stringify(inventoryCode));
    const pool = req.pool;
    const result = await pool.request()
      .input("InventoryCode", sql.VarChar, inventoryCode)
      .query(`
        SELECT 
          InventoryCode, 
          Description, 
          Weight, 
          StoneWeight 
        FROM tblInventory 
        WHERE InventoryCode = @InventoryCode
      `);
    console.log("[DEBUG] Result rows count:", result.recordset.length);
    if (result.recordset.length === 0) {
      return res.status(404).json({ success: false, message: "InventoryCode Not Found" });
    }
    res.json({ success: true, data: result.recordset[0] });
  } catch (err) {
    console.error("Fetch Inventory Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// 6. GET order header and its customer details
router.get("/order-header/:orderNo", async (req, res) => {
  try {
    const { orderNo } = req.params;
    const pool = req.pool;
    const result = await pool.request()
      .input("OrderNo", sql.VarChar, orderNo)
      .query(`
        SELECT 
          h.OrderNo, 
          h.CustomerCode, 
          c.CustomerName, 
          c.Address1, 
          c.Address2, 
          c.Address3, 
          c.CustomerType 
        FROM tblInventoryOrderHeader h
        LEFT JOIN tblCustomer c ON h.CustomerCode = c.CustomerCode
        WHERE h.OrderNo = @OrderNo
      `);
    if (result.recordset.length === 0) {
      return res.status(404).json({ success: false, message: "Could not find the OrderNo." });
    }
    res.json({ success: true, data: result.recordset[0] });
  } catch (err) {
    console.error("Fetch Order Header Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// 7. GET mapped items for an order
router.get("/order-details-mapped/:orderNo", async (req, res) => {
  try {
    const { orderNo } = req.params;
    const pool = req.pool;
    const result = await pool.request()
      .input("OrderNo", sql.VarChar, orderNo)
      .query(`
        SELECT 
          d.OrderInventoryCode, 
          d.Description AS OrderDescription, 
          d.WeightEstimated AS OrderWeight,
          d.InventoryCode, 
          i.Description AS Description, 
          i.Weight AS InvWeight
        FROM tblInventoryOrderDetail d
        LEFT JOIN tblInventory i ON d.InventoryCode = i.InventoryCode
        WHERE d.OrderNo = @OrderNo AND d.InventoryCode <> '' AND d.InventoryCode IS NOT NULL
      `);
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Mapped Order Details Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// 8. POST save (transaction block to map items)
router.post("/save", async (req, res) => {
  const pool = req.pool;
  const transaction = new sql.Transaction(pool);
  
  try {
    const { orderNo, items, userId } = req.body;
    
    if (!orderNo) {
      return res.status(400).json({ success: false, message: "OrderNo is required" });
    }

    const sysUser = userId || "sys";
    await transaction.begin();

    // 1. First, clear old mappings if any, but wait, VB code doesn't do a full delete/reinsert,
    // it simply loops through grdOrder and updates tblInventoryOrderDetail and tblInventory.
    // However, if we removed some mappings, how do we clear them?
    // In VB: when we load, the grid is populated with existing mappings. If we delete a row in the grid and then hit save,
    // we would want to unmap it. But VB6 doesn't seem to track deleted items, or does it?
    // Actually, in the VB6 code:
    // It loops through grdOrder rows and calls fncUpdateOrderInventory for each.
    // Let's implement this:
    // First, let's find all currently mapped InventoryCodes for this OrderNo in tblInventoryOrderDetail,
    // and clear their OrderNo in tblInventory, and clear their InventoryCode in tblInventoryOrderDetail.
    // That way, we do a clean "sync" to match the frontend grid state! This is much more reliable and prevents orphaned mappings.
    
    // Clear existing mapped inventory items for this order:
    // a. Get existing mapped inventory codes
    const existingMappedRequest = new sql.Request(transaction);
    const existingMappedRes = await existingMappedRequest
      .input("OrderNo", sql.VarChar, orderNo)
      .query(`
        SELECT InventoryCode 
        FROM tblInventoryOrderDetail 
        WHERE OrderNo = @OrderNo AND InventoryCode <> '' AND InventoryCode IS NOT NULL
      `);
    
    const existingCodes = existingMappedRes.recordset.map(r => r.InventoryCode.trim());
    if (existingCodes.length > 0) {
      // Unlink them in tblInventory
      const unlinkRequest = new sql.Request(transaction);
      unlinkRequest.input("OrderNo", sql.VarChar, orderNo);
      await unlinkRequest.query(`
        UPDATE tblInventory 
        SET OrderNo = NULL, 
            ModifyUser = @UserId, 
            ModifyDate = GETDATE() 
        WHERE OrderNo = @OrderNo
      `);
      
      // Clear them in tblInventoryOrderDetail
      const clearRequest = new sql.Request(transaction);
      clearRequest.input("OrderNo", sql.VarChar, orderNo);
      await clearRequest.query(`
        UPDATE tblInventoryOrderDetail 
        SET InventoryCode = '', 
            Status = ' ', 
            ModifiedUser = @UserId, 
            ModifiedDate = GETDATE() 
        WHERE OrderNo = @OrderNo
      `);
    }

    // 2. Insert/Update new mappings from req.body.items
    if (items && items.length > 0) {
      for (const item of items) {
        const { OrderInventoryCode, InventoryCode } = item;
        
        if (!OrderInventoryCode || !InventoryCode) continue;

        // Update tblInventoryOrderDetail
        const updateDetailReq = new sql.Request(transaction);
        await updateDetailReq
          .input("OrderNo", sql.VarChar, orderNo)
          .input("OrderInventoryCode", sql.VarChar, OrderInventoryCode)
          .input("InventoryCode", sql.VarChar, InventoryCode)
          .input("UserId", sql.VarChar, sysUser)
          .query(`
            UPDATE tblInventoryOrderDetail 
            SET InventoryCode = @InventoryCode, 
                Status = 'S', 
                ModifiedUser = @UserId, 
                ModifiedDate = GETDATE() 
            WHERE OrderNo = @OrderNo AND OrderInventoryCode = @OrderInventoryCode
          `);

        // Update tblInventory
        const updateInvReq = new sql.Request(transaction);
        await updateInvReq
          .input("OrderNo", sql.VarChar, orderNo)
          .input("InventoryCode", sql.VarChar, InventoryCode)
          .input("UserId", sql.VarChar, sysUser)
          .query(`
            UPDATE tblInventory 
            SET OrderNo = @OrderNo, 
                ModifyUser = @UserId, 
                ModifyDate = GETDATE() 
            WHERE InventoryCode = @InventoryCode
          `);
      }
    }

    // 3. Update Order Header (fncUpdateOrderInventoryHeader)
    // Check if there are any unassigned details remaining
    const checkRemainingReq = new sql.Request(transaction);
    const checkRemainingRes = await checkRemainingReq
      .input("OrderNo", sql.VarChar, orderNo)
      .query(`
        SELECT COUNT(*) as remainingCount 
        FROM tblInventoryOrderDetail 
        WHERE OrderNo = @OrderNo AND (InventoryCode = '' OR InventoryCode IS NULL)
      `);
    
    const remainingCount = checkRemainingRes.recordset[0].remainingCount;
    // Set Status: if 0 remaining, Status = 'R' (Ready/Fully Processed), else 'P' (Processing/Partially Processed)
    const newStatus = remainingCount === 0 ? "R" : "P";

    const updateHeaderReq = new sql.Request(transaction);
    await updateHeaderReq
      .input("OrderNo", sql.VarChar, orderNo)
      .input("Status", sql.VarChar, newStatus)
      .input("UserId", sql.VarChar, sysUser)
      .query(`
        UPDATE tblInventoryOrderHeader 
        SET Status = @Status, 
            ModifiedUser = @UserId, 
            ModifiedDate = CONVERT(varchar(10), GETDATE(), 120) 
        WHERE OrderNo = @OrderNo
      `);

    await transaction.commit();
    res.json({ success: true, message: "Order inventory mapping saved successfully" });

  } catch (err) {
    console.error("Save Order Inventory Error:", err);
    try {
      await transaction.rollback();
    } catch (rollbackErr) {
      console.error("Rollback failed:", rollbackErr);
    }
    res.status(500).json({ success: false, message: "Database Save Error", error: err.message });
  }
});

module.exports = router;
