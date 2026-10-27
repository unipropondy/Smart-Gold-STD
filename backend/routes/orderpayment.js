const express = require("express");
const router = express.Router();
const { sql } = require("../db");

// Helper to pad voucher numbers
function padZero(num, size) {
  let s = num + "";
  while (s.length < size) s = "0" + s;
  return s;
}

// 1. GET next order payment number
router.get("/next-no", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query(
      "SELECT TOP 1 NextOrderPaymentNo FROM tblCompany"
    );

    if (result.recordset.length > 0) {
      const nextNo = result.recordset[0].NextOrderPaymentNo || 1;
      const formattedNo = `OP-${nextNo}`;
      res.json({ success: true, paymentNo: formattedNo });
    } else {
      res.json({ success: true, paymentNo: "OP-1" });
    }
  } catch (err) {
    console.error("Fetch Next Order Payment Number Error:", err);
    res.status(500).json({ success: false, message: "Failed to generate next order payment number", error: err.message });
  }
});

// 2. GET orders for LOV lookup
router.get("/orders", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query(`
      SELECT OrderNo, CustomerCode, OrderDate, DeliveryDate, TotalCostEstimated, AdvancePaid
      FROM tblInventoryOrderHeader
      WHERE ISNULL(AdvancePaid, 0) < ISNULL(TotalCostEstimated, 0)
      ORDER BY OrderDate DESC
    `);
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Orders Error:", err);
    res.status(500).json({ success: false, message: "Failed to fetch orders", error: err.message });
  }
});

// 3. GET customer by code
router.get("/customer/:code", async (req, res) => {
  const { code } = req.params;
  try {
    const pool = req.pool;
    const request = pool.request();
    request.input("CustomerCode", sql.VarChar, code);
    
    const result = await request.query(`
      SELECT CustomerCode, CustomerName, Address1, Address2, Address3, Phone, Email
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

// 4. GET flexi
router.get("/flexi", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query(`
      SELECT FlexiID, CustomerName, DepositBalance
      FROM vwFlexiFinalClosure
    `);
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Flexi Error:", err);
    res.status(500).json({ success: false, message: "Failed to fetch flexi details", error: err.message });
  }
});

// 5. GET balance and payment history
router.get("/balance/:orderNo", async (req, res) => {
  const { orderNo } = req.params;
  try {
    const pool = req.pool;
    const request = pool.request();
    request.input("OrderNo", sql.VarChar, orderNo);

    // Get order details
    const orderResult = await request.query(`
      SELECT OrderNo, CustomerCode, TotalCostEstimated, AdvancePaid
      FROM tblInventoryOrderHeader
      WHERE OrderNo = @OrderNo
    `);

    if (orderResult.recordset.length === 0) {
      return res.status(404).json({ success: false, message: "Order not found" });
    }

    const orderData = orderResult.recordset[0];
    const estimatedCost = orderData.TotalCostEstimated || 0;
    const advancePaid = orderData.AdvancePaid || 0;

    // Get payment history
    const historyResult = await request.query(`
      SELECT OrderNo, OrderPaymentNo, AmountPaid, Remarks, CreateDate, TradeinAmt, TradeinGST
      FROM tblOrderPaymentdetail
      WHERE OrderNo = @OrderNo
      ORDER BY CreateDate ASC
    `);

    let totalAmountPaid = 0;
    historyResult.recordset.forEach(row => {
      totalAmountPaid += (row.AmountPaid || 0);
    });

    const balanceAmount = estimatedCost - advancePaid - totalAmountPaid;

    res.json({ 
      success: true, 
      data: {
        order: orderData,
        balance: {
          estimatedCost,
          advancePaid,
          totalAmountPaid,
          balanceAmount
        },
        history: historyResult.recordset
      } 
    });
  } catch (err) {
    console.error("Fetch Balance Error:", err);
    res.status(500).json({ success: false, message: "Failed to fetch balance details", error: err.message });
  }
});

// 6. POST pay order
router.post("/pay", async (req, res) => {
  const {
    orderNo,
    orderPaymentNo,
    amountPaid,
    tradeInAmt,
    tradeInGST,
    remarks,
    paymodeDetails,
    createUser
  } = req.body;

  const pool = req.pool;
  const transaction = new sql.Transaction(pool);

  try {
    await transaction.begin();

    // Insert Detail
    const insertDetailReq = new sql.Request(transaction);
    insertDetailReq.input("OrderNo", sql.VarChar, orderNo);
    insertDetailReq.input("OrderPaymentNo", sql.VarChar, orderPaymentNo);
    insertDetailReq.input("AmountPaid", sql.Decimal(18, 2), parseFloat(amountPaid));
    insertDetailReq.input("TradeinAmt", sql.Decimal(18, 2), parseFloat(tradeInAmt || 0));
    insertDetailReq.input("TradeinGST", sql.Decimal(18, 2), parseFloat(tradeInGST || 0));
    insertDetailReq.input("Remarks", sql.VarChar, remarks || 'ORDER PAYMENT');
    insertDetailReq.input("CreateDate", sql.DateTime, new Date());
    insertDetailReq.input("CreateUser", sql.VarChar, createUser || 'ADMIN');

    await insertDetailReq.query(`
      INSERT INTO tblOrderPaymentdetail (
        OrderNo, OrderPaymentNo, AmountPaid, TradeinAmt, TradeinGST, Remarks, CreateDate, CreateUser
      ) VALUES (
        @OrderNo, @OrderPaymentNo, @AmountPaid, @TradeinAmt, @TradeinGST, @Remarks, @CreateDate, @CreateUser
      )
    `);

    // Handle Paymode Details
    if (paymodeDetails && paymodeDetails.length > 0) {
      for (const paymode of paymodeDetails) {
        const pmReq = new sql.Request(transaction);
        pmReq.input("RefNo", sql.VarChar, orderPaymentNo);
        pmReq.input("PayDate", sql.DateTime, new Date());
        pmReq.input("PayMode", sql.VarChar, paymode.payMode);
        pmReq.input("Amount", sql.Decimal(18, 2), parseFloat(paymode.amount));
        pmReq.input("Remarks", sql.VarChar, paymode.remarks || 'ORDER PAYMENT');
        
        await pmReq.query(`
          INSERT INTO tblPayModeDetail (
            InvoiceNo, InvoiceDate, PayMode, Amount, CreateUser, CreateDate, LocationCode, SalesType, IsPaid
          ) VALUES (
            @RefNo, @PayDate, @PayMode, @Amount, 'SYS', @PayDate, 'HQ', 'ORDER PAYMENT', 1
          )
        `);
      }
    }

    // Update Company Counter
    const counterReq = new sql.Request(transaction);
    await counterReq.query("UPDATE tblCompany SET NextOrderPaymentNo = NextOrderPaymentNo + 1");

    await transaction.commit();
    res.json({ success: true, message: "Order payment saved successfully" });
  } catch (err) {
    await transaction.rollback();
    console.error("Order Payment Save Error:", err);
    res.status(500).json({ success: false, message: "Failed to save order payment", error: err.message });
  }
});

// 7. DELETE cancel payment
router.delete("/pay/:orderNo/:paymentNo", async (req, res) => {
  const { orderNo, paymentNo } = req.params;

  const pool = req.pool;
  const transaction = new sql.Transaction(pool);

  try {
    await transaction.begin();

    // Delete Detail
    const deleteDetailReq = new sql.Request(transaction);
    deleteDetailReq.input("OrderNo", sql.VarChar, orderNo);
    deleteDetailReq.input("OrderPaymentNo", sql.VarChar, paymentNo);

    const result = await deleteDetailReq.query(`
      DELETE FROM tblOrderPaymentdetail
      WHERE OrderNo = @OrderNo AND OrderPaymentNo = @OrderPaymentNo
    `);

    if (result.rowsAffected[0] === 0) {
      await transaction.rollback();
      return res.status(404).json({ success: false, message: "Payment not found" });
    }

    // Delete Paymode Details
    const deletePmReq = new sql.Request(transaction);
    deletePmReq.input("RefNo", sql.VarChar, paymentNo);
    await deletePmReq.query(`
      DELETE FROM tblPayModeDetail
      WHERE RefNo = @RefNo AND TransType = 'ORDER PAYMENT'
    `);

    await transaction.commit();
    res.json({ success: true, message: "Order payment cancelled successfully" });
  } catch (err) {
    await transaction.rollback();
    console.error("Cancel Order Payment Error:", err);
    res.status(500).json({ success: false, message: "Failed to cancel order payment", error: err.message });
  }
});

module.exports = router;
