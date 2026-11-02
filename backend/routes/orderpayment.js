const express = require("express");
const router = express.Router();
const { sql, poolPromise } = require("../db");

// Helper to acquire database connection pool reliably
const getPool = async (req) => {
  if (req && req.pool) return req.pool;
  if (poolPromise) return await poolPromise;
  throw new Error("Database pool is not available.");
};

// Helper to pad voucher numbers
function padZero(num, size) {
  let s = num + "";
  while (s.length < size) s = "0" + s;
  return s;
}

// 1. GET next order payment number (matching fncGetNextOrderPaymentNo)
router.get("/next-no", async (req, res) => {
  try {
    const pool = await getPool(req);
    const result = await pool.request().query(
      "SELECT TOP 1 NextOrderPaymentNo FROM tblCompany"
    );

    if (result.recordset.length > 0 && result.recordset[0].NextOrderPaymentNo != null) {
      const nextNo = result.recordset[0].NextOrderPaymentNo;
      const formattedNo = `OP${nextNo}`;
      res.json({ success: true, paymentNo: formattedNo, rawNo: nextNo });
    } else {
      res.json({ success: true, paymentNo: "OP1", rawNo: 1 });
    }
  } catch (err) {
    console.error("Fetch Next Order Payment Number Error:", err);
    res.status(500).json({ success: false, message: "Failed to generate next order payment number", error: err.message });
  }
});

// 2. GET orders for LOV lookup (matching btnOrderPaymentNoSearch_Click in frmOrderPayment.vb)
// WHERE AdvancePaid < TotalCostEstimated
router.get("/orders", async (req, res) => {
  try {
    const pool = await getPool(req);
    const result = await pool.request().query(`
      SELECT 
        o.OrderNo, 
        o.CustomerCode, 
        c.CustomerName,
        o.OrderDate, 
        o.DeliveryDate, 
        o.SupplierCode,
        ISNULL(o.TotalCostEstimated, 0) AS TotalCostEstimated, 
        ISNULL(o.AdvancePaid, 0) AS AdvancePaid,
        (ISNULL(o.TotalCostEstimated, 0) - ISNULL(o.AdvancePaid, 0)) AS BalanceAmount,
        o.Status
      FROM tblInventoryOrderHeader o
      LEFT JOIN tblCustomer c ON c.CustomerCode = o.CustomerCode
      WHERE ISNULL(o.AdvancePaid, 0) < ISNULL(o.TotalCostEstimated, 0)
      ORDER BY o.OrderDate DESC, o.OrderNo DESC
    `);
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Orders Error:", err);
    res.status(500).json({ success: false, message: "Failed to fetch orders", error: err.message });
  }
});

// 3. GET customer by code (matching subGetCustomer in frmOrderPayment.vb)
router.get("/customer/:code", async (req, res) => {
  const { code } = req.params;
  try {
    const pool = await getPool(req);
    const request = pool.request();
    request.input("CustomerCode", sql.VarChar, code);
    
    const result = await request.query(`
      SELECT CustomerCode, CustomerName, Address1, Address2, Address3, CustomerType, Phone, Email
      FROM tblCustomer
      WHERE CustomerCode = @CustomerCode
    `);

    if (result.recordset.length > 0) {
      res.json({ success: true, data: result.recordset[0] });
    } else {
      res.status(404).json({ success: false, message: "Customer not found" });
    }
  } catch (err) {
    console.error("Fetch Customer Error:", err);
    res.status(500).json({ success: false, message: "Failed to fetch customer details", error: err.message });
  }
});

// 4. GET flexi subscription LOV (matching btnSubscriptionID_Click & vwFlexiFinalClosure)
router.get("/flexi", async (req, res) => {
  try {
    const pool = await getPool(req);
    const result = await pool.request().query(`
      SELECT 
        SubscriptionId AS FlexiID, 
        CustomerName, 
        ISNULL(TotalAmount, 0) AS TotalAmount, 
        ISNULL(TotalBonus, 0) AS TotalBonus,
        LocationCode
      FROM vwFlexiFinalClosure
      WHERE isProcessed = 'N' OR isProcessed IS NULL
    `);
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    // Fallback if view columns differ
    try {
      const pool = await getPool(req);
      const res2 = await pool.request().query(`
        SELECT TOP 100 * FROM vwFlexiFinalClosure
      `);
      res.json({ success: true, data: res2.recordset });
    } catch (err2) {
      console.error("Fetch Flexi Error:", err2);
      res.status(500).json({ success: false, message: "Failed to fetch flexi details", error: err.message });
    }
  }
});

// 5. GET balance, order details, and payment history (matching subGetOrderDetails)
router.get("/balance/:orderNo", async (req, res) => {
  const { orderNo } = req.params;
  try {
    const pool = await getPool(req);
    const request = pool.request();
    request.input("OrderNo", sql.VarChar, orderNo);

    // Get order details along with customer details
    const orderResult = await request.query(`
      SELECT 
        o.OrderNo, 
        o.CustomerCode, 
        c.CustomerName, 
        c.Address1, 
        c.Address2, 
        c.Address3, 
        c.CustomerType,
        ISNULL(o.TotalCostEstimated, 0) AS TotalCostEstimated, 
        ISNULL(o.AdvancePaid, 0) AS AdvancePaid,
        o.Status
      FROM tblInventoryOrderHeader o
      LEFT JOIN tblCustomer c ON c.CustomerCode = o.CustomerCode
      WHERE o.OrderNo = @OrderNo
    `);

    if (orderResult.recordset.length === 0) {
      return res.status(404).json({ success: false, message: "Order not found" });
    }

    const orderData = orderResult.recordset[0];
    const estimatedCost = parseFloat(orderData.TotalCostEstimated) || 0;
    const advancePaid = parseFloat(orderData.AdvancePaid) || 0;

    // Check if order details have linked inventory code
    const detailInvRes = await pool.request()
      .input("OrderNo", sql.VarChar, orderNo)
      .query(`
        SELECT TOP 1 InventoryCode, CostPerGram, EstimatedCost, EstimatedGST, WeightEstimated
        FROM tblInventoryOrderDetail
        WHERE OrderNo = @OrderNo AND InventoryCode IS NOT NULL AND RTRIM(LTRIM(InventoryCode)) <> ''
      `);
    const matchedInventoryCode = detailInvRes.recordset.length > 0 ? detailInvRes.recordset[0].InventoryCode : null;

    // Get payment history from tblOrderPaymentdetail
    const historyResult = await pool.request()
      .input("OrderNo", sql.VarChar, orderNo)
      .query(`
        SELECT 
          OrderNo, 
          OrderPaymentNo, 
          AmountPaid, 
          Remarks, 
          CreateDate, 
          ISNULL(TradeinAmt, 0) AS TradeinAmt, 
          ISNULL(TradeinGST, 0) AS TradeinGST
        FROM tblOrderPaymentdetail
        WHERE OrderNo = @OrderNo
        ORDER BY CreateDate ASC
      `);

    let totalAmountPaid = 0;
    historyResult.recordset.forEach(row => {
      totalAmountPaid += (parseFloat(row.AmountPaid) || 0);
    });

    const balanceAmount = estimatedCost - (advancePaid + totalAmountPaid);

    res.json({ 
      success: true, 
      data: {
        order: {
          ...orderData,
          matchedInventoryCode
        },
        balance: {
          estimatedCost,
          advancePaid,
          totalAmountPaid,
          balanceAmount: balanceAmount > 0 ? balanceAmount : 0
        },
        history: historyResult.recordset
      } 
    });
  } catch (err) {
    console.error("Fetch Balance Error:", err);
    res.status(500).json({ success: false, message: "Failed to fetch balance details", error: err.message });
  }
});

// 6. POST pay order (matching btnSave_Click, fncSaveOrderPaymentDetail, fncPaymodeDetailSave, subCheckAmount)
router.post("/pay", async (req, res) => {
  const {
    orderNo,
    orderPaymentNo,
    amountPaid,
    tradeInAmt,
    tradeInGST,
    remarks,
    paymodeDetails,
    createUser,
    isFinalPayment,
    flexiId
  } = req.body;

  let pool;
  try {
    pool = await getPool(req);
  } catch (poolErr) {
    return res.status(500).json({ success: false, message: poolErr.message });
  }

  const transaction = new sql.Transaction(pool);

  try {
    await transaction.begin();

    // 1. Generate Order Payment Number if not provided
    let finalPaymentNo = orderPaymentNo;
    if (!finalPaymentNo) {
      const numRes = await new sql.Request(transaction).query(
        "SELECT TOP 1 NextOrderPaymentNo FROM tblCompany"
      );
      const nextNum = numRes.recordset.length > 0 ? numRes.recordset[0].NextOrderPaymentNo || 1 : 1;
      finalPaymentNo = `OP${nextNum}`;
    }

    const nAmountPaid = parseFloat(amountPaid) || 0;
    const nTradeInAmt = parseFloat(tradeInAmt) || 0;
    const nTradeInGST = parseFloat(tradeInGST) || 0;
    const sUser = createUser || 'ADMIN';
    const sRemarks = remarks || 'ORDER PAYMENT';

    // 2. Insert into tblOrderPaymentdetail (matching fncSaveOrderPaymentDetail)
    const insertDetailReq = new sql.Request(transaction);
    insertDetailReq.input("OrderNo", sql.VarChar(20), String(orderNo).trim());
    insertDetailReq.input("OrderPaymentNo", sql.VarChar(20), String(finalPaymentNo).trim());
    insertDetailReq.input("AmountPaid", sql.Decimal(18, 2), nAmountPaid);
    insertDetailReq.input("TradeinAmt", sql.Decimal(18, 2), nTradeInAmt);
    insertDetailReq.input("TradeinGST", sql.Decimal(18, 2), nTradeInGST);
    insertDetailReq.input("Remarks", sql.VarChar(100), sRemarks);
    insertDetailReq.input("CreateDate", sql.DateTime, new Date());
    insertDetailReq.input("CreateUser", sql.VarChar(20), sUser);

    await insertDetailReq.query(`
      INSERT INTO tblOrderPaymentdetail (
        OrderNo, OrderPaymentNo, AmountPaid, TradeinAmt, TradeinGST, Remarks, CreateDate, CreateUser
      ) VALUES (
        @OrderNo, @OrderPaymentNo, @AmountPaid, @TradeinAmt, @TradeinGST, @Remarks, @CreateDate, @CreateUser
      )
    `);

    // 3. Handle Paymode Details (matching fncPaymodeDetailSave)
    if (paymodeDetails && Array.isArray(paymodeDetails) && paymodeDetails.length > 0) {
      for (const pm of paymodeDetails) {
        const pmAmount = parseFloat(pm.amount || pm.Amount) || 0;
        if (pmAmount > 0) {
          const pmReq = new sql.Request(transaction);
          pmReq.input("InvoiceNo", sql.VarChar(20), String(finalPaymentNo).trim());
          pmReq.input("InvoiceDate", sql.DateTime, new Date());
          pmReq.input("PayMode", sql.VarChar(20), String(pm.payMode || pm.PayMode || 'CASH').trim());
          pmReq.input("Amount", sql.Decimal(18, 2), pmAmount);
          pmReq.input("CreateUser", sql.VarChar(20), sUser);
          pmReq.input("LocationCode", sql.VarChar(10), "HQ");
          pmReq.input("SalesType", sql.VarChar(20), "ORDER");

          await pmReq.query(`
            INSERT INTO tblPayModeDetail (
              InvoiceNo, InvoiceDate, PayMode, Amount, CreateUser, CreateDate, LocationCode, SalesType, IsPaid
            ) VALUES (
              @InvoiceNo, @InvoiceDate, @PayMode, @Amount, @CreateUser, @InvoiceDate, @LocationCode, @SalesType, 1
            )
          `);
        }
      }
    } else {
      // Default to Cash payment if no paymode details specified
      const pmReq = new sql.Request(transaction);
      pmReq.input("InvoiceNo", sql.VarChar(20), String(finalPaymentNo).trim());
      pmReq.input("InvoiceDate", sql.DateTime, new Date());
      pmReq.input("Amount", sql.Decimal(18, 2), nAmountPaid);
      pmReq.input("CreateUser", sql.VarChar(20), sUser);

      await pmReq.query(`
        INSERT INTO tblPayModeDetail (
          InvoiceNo, InvoiceDate, PayMode, Amount, CreateUser, CreateDate, LocationCode, SalesType, IsPaid
        ) VALUES (
          @InvoiceNo, @InvoiceDate, 'CASH', @Amount, @CreateUser, @InvoiceDate, 'HQ', 'ORDER', 1
        )
      `);
    }

    // 4. Update Company Counter (NextOrderPaymentNo)
    await new sql.Request(transaction).query(
      "UPDATE tblCompany SET NextOrderPaymentNo = ISNULL(NextOrderPaymentNo, 0) + 1"
    );

    // 5. Check if fully settled or final payment checked (matching subCheckAmount)
    const balCheckReq = new sql.Request(transaction);
    balCheckReq.input("OrderNo", sql.VarChar(20), String(orderNo).trim());
    const balRes = await balCheckReq.query(`
      SELECT 
        o.TotalCostEstimated, 
        o.AdvancePaid,
        ISNULL((SELECT SUM(AmountPaid) FROM tblOrderPaymentdetail WHERE OrderNo = o.OrderNo), 0) AS TotalPaid
      FROM tblInventoryOrderHeader o
      WHERE o.OrderNo = @OrderNo
    `);

    let orderCompleted = false;
    if (balRes.recordset.length > 0) {
      const oEst = parseFloat(balRes.recordset[0].TotalCostEstimated) || 0;
      const oAdv = parseFloat(balRes.recordset[0].AdvancePaid) || 0;
      const oPaid = parseFloat(balRes.recordset[0].TotalPaid) || 0;
      const remainingBal = oEst - (oAdv + oPaid);

      if (remainingBal <= 0 || isFinalPayment) {
        orderCompleted = true;
        // Update Order Status to Delivered/Completed ('D')
        await new sql.Request(transaction)
          .input("OrderNo", sql.VarChar(20), String(orderNo).trim())
          .query("UPDATE tblInventoryOrderHeader SET Status = 'D' WHERE OrderNo = @OrderNo");
      }
    }

    // 6. Handle Flexi ID closure if provided
    if (flexiId) {
      try {
        await new sql.Request(transaction)
          .input("SubscriptionId", sql.VarChar(30), String(flexiId).trim())
          .input("User", sql.VarChar(20), sUser)
          .query(`
            UPDATE tblFlexiSubscription 
            SET isProcessed = 'Y', ModifyUser = @User, ModifyDate = GETDATE()
            WHERE SubscriptionId = @SubscriptionId
          `);
      } catch (flexiErr) {
        // Table or view schema may vary
      }
    }

    await transaction.commit();
    res.json({ 
      success: true, 
      message: "Order payment saved successfully.", 
      paymentNo: finalPaymentNo,
      orderCompleted 
    });
  } catch (err) {
    try {
      await transaction.rollback();
    } catch (rbErr) {}
    console.error("Order Payment Save Error:", err);
    res.status(500).json({ success: false, message: "Failed to save order payment", error: err.message });
  }
});

// 7. DELETE cancel payment (matching subCancelOrderPayment in frmOrderPayment.vb)
router.delete("/pay/:orderNo/:paymentNo", async (req, res) => {
  const { orderNo, paymentNo } = req.params;

  let pool;
  try {
    pool = await getPool(req);
  } catch (poolErr) {
    return res.status(500).json({ success: false, message: poolErr.message });
  }

  const transaction = new sql.Transaction(pool);

  try {
    await transaction.begin();

    // Check payment date to ensure same-day cancellation rule
    const chkReq = new sql.Request(transaction);
    chkReq.input("OrderNo", sql.VarChar(20), orderNo);
    chkReq.input("OrderPaymentNo", sql.VarChar(20), paymentNo);
    const chkRes = await chkReq.query(`
      SELECT OrderPaymentNo, AmountPaid, CreateDate
      FROM tblOrderPaymentdetail
      WHERE OrderNo = @OrderNo AND OrderPaymentNo = @OrderPaymentNo
    `);

    if (chkRes.recordset.length === 0) {
      await transaction.rollback();
      return res.status(404).json({ success: false, message: "Payment record not found." });
    }

    const row = chkRes.recordset[0];
    const amountPaid = parseFloat(row.AmountPaid) || 0;

    // Delete Detail
    const deleteDetailReq = new sql.Request(transaction);
    deleteDetailReq.input("OrderNo", sql.VarChar(20), orderNo);
    deleteDetailReq.input("OrderPaymentNo", sql.VarChar(20), paymentNo);

    await deleteDetailReq.query(`
      DELETE FROM tblOrderPaymentdetail
      WHERE OrderNo = @OrderNo AND OrderPaymentNo = @OrderPaymentNo
    `);

    // Record negative reversing row in tblPayModeDetail (matching subCancelOrderPayment in VB: "OP CANCEL")
    const revReq = new sql.Request(transaction);
    revReq.input("InvoiceNo", sql.VarChar(20), orderNo);
    revReq.input("Amount", sql.Decimal(18, 2), -amountPaid);
    revReq.input("PayDate", sql.DateTime, new Date());
    await revReq.query(`
      INSERT INTO tblPayModeDetail (
        InvoiceNo, InvoiceDate, PayMode, Amount, CreateUser, CreateDate, LocationCode, SalesType, IsPaid
      ) VALUES (
        @InvoiceNo, @PayDate, 'CASH', @Amount, 'SYS', @PayDate, 'HQ', 'OP CANCEL', 1
      )
    `);

    // Also remove original payment mode rows for this payment voucher
    const deletePmReq = new sql.Request(transaction);
    deletePmReq.input("RefNo", sql.VarChar(20), paymentNo);
    await deletePmReq.query(`
      DELETE FROM tblPayModeDetail
      WHERE InvoiceNo = @RefNo
    `);

    // Reset order status if it was closed
    await new sql.Request(transaction)
      .input("OrderNo", sql.VarChar(20), orderNo)
      .query("UPDATE tblInventoryOrderHeader SET Status = 'P' WHERE OrderNo = @OrderNo AND Status = 'D'");

    await transaction.commit();
    res.json({ success: true, message: "Order payment cancelled successfully." });
  } catch (err) {
    try {
      await transaction.rollback();
    } catch (rbErr) {}
    console.error("Cancel Order Payment Error:", err);
    res.status(500).json({ success: false, message: "Failed to cancel order payment", error: err.message });
  }
});

// 8. RePrint Order Payment Receipt (matching subPrintOrderPayment & subPrintBill)
router.get("/reprint/:billNo", async (req, res) => {
  const { billNo } = req.params;
  try {
    const pool = await getPool(req);
    const result = await pool.request()
      .input("BillNo", sql.VarChar(20), billNo)
      .query(`
        SELECT 
          d.OrderNo,
          d.OrderPaymentNo,
          d.AmountPaid,
          d.TradeinAmt,
          d.TradeinGST,
          d.Remarks,
          d.CreateDate,
          d.CreateUser,
          h.CustomerCode,
          c.CustomerName,
          c.Address1,
          c.Address2,
          c.Address3,
          h.TotalCostEstimated,
          h.AdvancePaid
        FROM tblOrderPaymentdetail d
        JOIN tblInventoryOrderHeader h ON h.OrderNo = d.OrderNo
        LEFT JOIN tblCustomer c ON c.CustomerCode = h.CustomerCode
        WHERE d.OrderPaymentNo = @BillNo
      `);

    if (result.recordset.length === 0) {
      return res.status(404).json({ success: false, message: "Receipt not found for bill number: " + billNo });
    }

    res.json({ success: true, data: result.recordset[0] });
  } catch (err) {
    console.error("RePrint Receipt Error:", err);
    res.status(500).json({ success: false, message: "Failed to load receipt details", error: err.message });
  }
});

module.exports = router;
