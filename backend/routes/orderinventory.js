const express = require("express");
const router = express.Router();
const { sql, poolPromise } = require("../db");

// Helper to acquire database connection pool reliably
const getPool = async (req) => {
  if (req && req.pool) return req.pool;
  if (poolPromise) return await poolPromise;
  throw new Error("Database pool is not available.");
};

// 1. GET all orders for lookup (Order No search matching frmOrderInventory btnOrderPaymentNoSearch_Click)
router.get("/orders", async (req, res) => {
  try {
    const pool = await getPool(req);
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

// 2. GET unassigned order details (Description search matching btnDescription_Click)
router.get("/order-details/:orderNo", async (req, res) => {
  try {
    const { orderNo } = req.params;
    const pool = await getPool(req);
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
        ORDER BY OrderInventoryCode
      `);
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Order Details LOV Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// 3. GET active inventory stock (Inventory Code search matching btnInventoryCodeSearch_Click)
router.get("/inventory-stock", async (req, res) => {
  try {
    const pool = await getPool(req);
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
    const pool = await getPool(req);
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

// 5. GET inventory details by inventory code (matching subGetInventory)
router.get("/inventory/:inventoryCode", async (req, res) => {
  try {
    const { inventoryCode } = req.params;
    const pool = await getPool(req);
    const result = await pool.request()
      .input("InventoryCode", sql.VarChar, inventoryCode)
      .query(`
        SELECT 
          InventoryCode, 
          Description, 
          Weight, 
          StoneWeight,
          QtyOnHand
        FROM tblInventory 
        WHERE InventoryCode = @InventoryCode
      `);
    if (result.recordset.length === 0) {
      return res.status(404).json({ success: false, message: "InventoryCode Not Found" });
    }
    res.json({ success: true, data: result.recordset[0] });
  } catch (err) {
    console.error("Fetch Inventory Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// 6. GET order header and its customer details (matching subGetOrderDetails)
router.get("/order-header/:orderNo", async (req, res) => {
  try {
    const { orderNo } = req.params;
    const pool = await getPool(req);
    const result = await pool.request()
      .input("OrderNo", sql.VarChar, orderNo)
      .query(`
        SELECT 
          h.OrderNo, 
          h.CustomerCode, 
          h.Remarks,
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

// 7. GET mapped items for an order (matching SubGetInventoryDetails)
router.get("/order-details-mapped/:orderNo", async (req, res) => {
  try {
    const { orderNo } = req.params;
    const pool = await getPool(req);
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

// 8. POST save (transaction block to map items matching btnSave_Click)
router.post("/save", async (req, res) => {
  const pool = await getPool(req);
  const transaction = new sql.Transaction(pool);
  
  try {
    const { orderNo, items, userId } = req.body;
    
    if (!orderNo) {
      return res.status(400).json({ success: false, message: "OrderNo is required" });
    }

    const sysUser = userId || "sys";
    await transaction.begin();

    // Dynamically check columns in tblInventoryOrderDetail, tblInventory, tblInventoryOrderHeader
    let detailCols = [];
    let invCols = [];
    let headerCols = [];
    try {
      const cRes = await new sql.Request(transaction).query(`
        SELECT TABLE_NAME, COLUMN_NAME 
        FROM INFORMATION_SCHEMA.COLUMNS 
        WHERE TABLE_NAME IN ('tblInventoryOrderDetail', 'tblInventory', 'tblInventoryOrderHeader')
      `);
      for (const row of cRes.recordset) {
        const t = row.TABLE_NAME.toLowerCase();
        const c = row.COLUMN_NAME.toLowerCase();
        if (t === 'tblinventoryorderdetail') detailCols.push(c);
        else if (t === 'tblinventory') invCols.push(c);
        else if (t === 'tblinventoryorderheader') headerCols.push(c);
      }
    } catch (e) {}

    const hasCol = (list, col) => list.length === 0 || list.includes(col.toLowerCase());

    // 1. Clear unmapped items that were previously linked to this order
    const existingMappedRequest = new sql.Request(transaction);
    const existingMappedRes = await existingMappedRequest
      .input("OrderNo", sql.VarChar, orderNo)
      .query(`
        SELECT InventoryCode 
        FROM tblInventoryOrderDetail 
        WHERE OrderNo = @OrderNo AND InventoryCode <> '' AND InventoryCode IS NOT NULL
      `);
    
    const existingCodes = existingMappedRes.recordset.map(r => (r.InventoryCode || '').trim());
    const incomingCodes = (items || []).map(i => (i.InventoryCode || '').trim());

    // Unlink only the ones removed from the grid
    const toUnlink = existingCodes.filter(c => c && !incomingCodes.includes(c));
    for (const code of toUnlink) {
      if (hasCol(invCols, "orderno")) {
        const unlinkInv = new sql.Request(transaction);
        unlinkInv.input("InventoryCode", sql.VarChar, code);
        let setClauses = ["OrderNo = NULL"];
        if (hasCol(invCols, "modifyuser")) setClauses.push("ModifyUser = @UserId");
        if (hasCol(invCols, "modifydate")) setClauses.push("ModifyDate = GETDATE()");
        unlinkInv.input("UserId", sql.VarChar, sysUser);
        await unlinkInv.query(`UPDATE tblInventory SET ${setClauses.join(", ")} WHERE InventoryCode = @InventoryCode`);
      }

      const clearDetail = new sql.Request(transaction);
      clearDetail.input("OrderNo", sql.VarChar, orderNo);
      clearDetail.input("InventoryCode", sql.VarChar, code);
      clearDetail.input("UserId", sql.VarChar, sysUser);
      let setD = ["InventoryCode = ''", "Status = ' '"];
      if (hasCol(detailCols, "modifieduser")) setD.push("ModifiedUser = @UserId");
      if (hasCol(detailCols, "modifieddate")) setD.push("ModifiedDate = GETDATE()");
      await clearDetail.query(`UPDATE tblInventoryOrderDetail SET ${setD.join(", ")} WHERE OrderNo = @OrderNo AND InventoryCode = @InventoryCode`);
    }

    // 2. Insert/Update new mappings from req.body.items (fncUpdateOrderInventory)
    if (items && items.length > 0) {
      for (const item of items) {
        const { OrderInventoryCode, InventoryCode, InvWeight } = item;
        if (!OrderInventoryCode || !InventoryCode) continue;

        // Update tblInventoryOrderDetail
        const updateDetailReq = new sql.Request(transaction);
        updateDetailReq
          .input("OrderNo", sql.VarChar, orderNo)
          .input("OrderInventoryCode", sql.VarChar, OrderInventoryCode)
          .input("InventoryCode", sql.VarChar, InventoryCode)
          .input("Weight", sql.Decimal(18, 3), parseFloat(InvWeight) || 0)
          .input("UserId", sql.VarChar, sysUser);

        let dSets = ["InventoryCode = @InventoryCode", "Status = 'S'"];
        if (hasCol(detailCols, "weight")) dSets.push("Weight = @Weight");
        if (hasCol(detailCols, "modifieduser")) dSets.push("ModifiedUser = @UserId");
        if (hasCol(detailCols, "modifieddate")) dSets.push("ModifiedDate = GETDATE()");

        await updateDetailReq.query(`
          UPDATE tblInventoryOrderDetail 
          SET ${dSets.join(", ")} 
          WHERE OrderNo = @OrderNo AND OrderInventoryCode = @OrderInventoryCode
        `);

        // Update tblInventory
        if (hasCol(invCols, "orderno")) {
          const updateInvReq = new sql.Request(transaction);
          updateInvReq
            .input("OrderNo", sql.VarChar, orderNo)
            .input("InventoryCode", sql.VarChar, InventoryCode)
            .input("UserId", sql.VarChar, sysUser);

          let iSets = ["OrderNo = @OrderNo"];
          if (hasCol(invCols, "modifyuser")) iSets.push("ModifyUser = @UserId");
          if (hasCol(invCols, "modifydate")) iSets.push("ModifyDate = GETDATE()");

          await updateInvReq.query(`
            UPDATE tblInventory 
            SET ${iSets.join(", ")} 
            WHERE InventoryCode = @InventoryCode
          `);
        }
      }
    }

    // 3. Update Order Header (fncUpdateOrderInventoryHeader)
    const checkRemainingReq = new sql.Request(transaction);
    const checkRemainingRes = await checkRemainingReq
      .input("OrderNo", sql.VarChar, orderNo)
      .query(`
        SELECT COUNT(*) as remainingCount 
        FROM tblInventoryOrderDetail 
        WHERE OrderNo = @OrderNo AND (InventoryCode = '' OR InventoryCode IS NULL)
      `);
    
    const remainingCount = checkRemainingRes.recordset[0].remainingCount;
    // Status: if 0 unassigned, Status = 'R' (Ready), else 'P' (Processing)
    const newStatus = remainingCount === 0 ? "R" : "P";

    const updateHeaderReq = new sql.Request(transaction);
    updateHeaderReq
      .input("OrderNo", sql.VarChar, orderNo)
      .input("Status", sql.VarChar, newStatus)
      .input("UserId", sql.VarChar, sysUser);

    let hSets = ["Status = @Status"];
    if (hasCol(headerCols, "modifieduser")) hSets.push("ModifiedUser = @UserId");
    if (hasCol(headerCols, "modifieddate")) hSets.push("ModifiedDate = GETDATE()");

    await updateHeaderReq.query(`
      UPDATE tblInventoryOrderHeader 
      SET ${hSets.join(", ")} 
      WHERE OrderNo = @OrderNo
    `);

    await transaction.commit();
    res.json({ success: true, message: "Order inventory mapping saved successfully" });

  } catch (err) {
    console.error("Save Order Inventory Error:", err);
    try {
      await transaction.rollback();
    } catch (rollbackErr) {}
    res.status(500).json({ success: false, message: "Database Save Error", error: err.message });
  }
});

module.exports = router;

module.exports = router;
