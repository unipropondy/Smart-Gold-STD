const express = require("express");
const router = express.Router();
const { sql } = require("../db");

// Helper to pad voucher numbers
function padZero(num, size) {
  let s = num + "";
  while (s.length < size) s = "0" + s;
  return s;
}

// 1. GET next repair payment number
router.get("/next-no", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query(
      "SELECT TOP 1 NextOrderPaymentNo FROM tblCompany"
    );

    if (result.recordset.length > 0) {
      const nextNo = result.recordset[0].NextOrderPaymentNo || 1;
      const formattedNo = `RP-${nextNo}`;
      res.json({ success: true, paymentNo: formattedNo });
    } else {
      res.json({ success: true, paymentNo: "RP-1" });
    }
  } catch (err) {
    console.error("Fetch Next Repair Payment Number Error:", err);
    res.status(500).json({ success: false, message: "Failed to generate next repair payment number", error: err.message });
  }
});

// 2. GET repairs for LOV lookup
router.get("/repairs", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query(`
      SELECT RepairNo, CustomerCode, RepairDate, TotalCostEstimated, AdvancePaid
      FROM tblInventoryRepairHeader
      WHERE ISNULL(AdvancePaid, 0) < ISNULL(TotalCostEstimated, 0)
      AND (Status = 'R' OR Status = 'P')
      ORDER BY RepairDate DESC
    `);
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Repairs Error:", err);
    res.status(500).json({ success: false, message: "Failed to fetch repairs", error: err.message });
  }
});

// 3. GET balance and payment history
router.get("/balance/:repairNo", async (req, res) => {
  const { repairNo } = req.params;
  try {
    const pool = req.pool;
    const request = pool.request();
    request.input("RepairNo", sql.VarChar, repairNo);

    // Get repair details
    const repairResult = await request.query(`
      SELECT a.RepairNo, a.CustomerCode, a.TotalCostEstimated, a.AdvancePaid, 
             c.CustomerName, c.Address1, c.Address2, c.Address3, c.CustomerType
      FROM tblInventoryRepairHeader a
      LEFT JOIN tblCustomer c ON a.CustomerCode = c.CustomerCode
      WHERE a.RepairNo = @RepairNo
    `);

    if (repairResult.recordset.length === 0) {
      return res.status(404).json({ success: false, message: "Repair order not found" });
    }

    const repairData = repairResult.recordset[0];
    const estimatedCost = repairData.TotalCostEstimated || 0;
    let advancePaid = repairData.AdvancePaid || 0;

    // Get payment history
    const historyResult = await request.query(`
      SELECT RepairNo, RepairPaymentNo, AmountPaid, Remarks
      FROM tblRepairPaymentDetail
      WHERE RepairNo = @RepairNo
    `);

    let totalAmountPaid = 0;
    historyResult.recordset.forEach(row => {
      totalAmountPaid += (row.AmountPaid || 0);
    });
    
    const balanceAmount = estimatedCost - (advancePaid + totalAmountPaid);

    res.json({ 
      success: true, 
      data: {
        repair: repairData,
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

// 4. POST pay repair
router.post("/pay", async (req, res) => {
  const {
    repairNo,
    repairPaymentNo,
    amountPaid,
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
    insertDetailReq.input("RepairNo", sql.VarChar, repairNo);
    insertDetailReq.input("RepairPaymentNo", sql.VarChar, repairPaymentNo);
    insertDetailReq.input("AmountPaid", sql.Decimal(18, 2), parseFloat(amountPaid));
    insertDetailReq.input("Remarks", sql.VarChar, remarks || 'REPAIR PAYMENT');

    await insertDetailReq.query(`
      INSERT INTO tblRepairPaymentDetail (
        RepairNo, RepairPaymentNo, AmountPaid, Remarks
      ) VALUES (
        @RepairNo, @RepairPaymentNo, @AmountPaid, @Remarks
      )
    `);

    // Handle Paymode Details
    if (paymodeDetails && paymodeDetails.length > 0) {
      for (const paymode of paymodeDetails) {
        const pmReq = new sql.Request(transaction);
        pmReq.input("RefNo", sql.VarChar, repairNo);
        pmReq.input("PayDate", sql.DateTime, new Date());
        pmReq.input("PayMode", sql.VarChar, paymode.payMode);
        pmReq.input("Amount", sql.Decimal(18, 2), parseFloat(paymode.amount));
        pmReq.input("ForeignAmount", sql.Decimal(18, 2), parseFloat(paymode.foreignAmount || 0));
        pmReq.input("Remarks", sql.VarChar, paymode.remarks || 'REPAIR PAYMENT');
        pmReq.input("TransType", sql.VarChar, 'REPAIR');
        
        await pmReq.query(`
          INSERT INTO tblPayModeDetail (
            RefNo, PayDate, PayMode, Amount, ForeignAmount, Remarks, TransType
          ) VALUES (
            @RefNo, @PayDate, @PayMode, @Amount, @ForeignAmount, @Remarks, @TransType
          )
        `);
      }
    }

    // Update Company Counter
    const counterReq = new sql.Request(transaction);
    await counterReq.query("UPDATE tblCompany SET NextOrderPaymentNo = NextOrderPaymentNo + 1");

    await transaction.commit();
    res.json({ success: true, message: "Repair payment saved successfully" });
  } catch (err) {
    await transaction.rollback();
    console.error("Repair Payment Save Error:", err);
    res.status(500).json({ success: false, message: "Failed to save repair payment", error: err.message });
  }
});

// 5. DELETE cancel payment
router.delete("/pay/:repairNo/:paymentNo", async (req, res) => {
  const { repairNo, paymentNo } = req.params;

  const pool = req.pool;
  const transaction = new sql.Transaction(pool);

  try {
    await transaction.begin();

    // Select Amount Paid for Cancellation
    const amountReq = new sql.Request(transaction);
    amountReq.input("RepairNo", sql.VarChar, repairNo);
    amountReq.input("RepairPaymentNo", sql.VarChar, paymentNo);
    const amountRes = await amountReq.query(`
      SELECT AmountPaid FROM tblRepairPaymentDetail
      WHERE RepairNo = @RepairNo AND RepairPaymentNo = @RepairPaymentNo
    `);

    if (amountRes.recordset.length === 0) {
      await transaction.rollback();
      return res.status(404).json({ success: false, message: "Payment not found" });
    }

    const cancelAmount = amountRes.recordset[0].AmountPaid;

    // Delete Detail
    const deleteDetailReq = new sql.Request(transaction);
    deleteDetailReq.input("RepairNo", sql.VarChar, repairNo);
    deleteDetailReq.input("RepairPaymentNo", sql.VarChar, paymentNo);

    await deleteDetailReq.query(`
      DELETE FROM tblRepairPaymentDetail
      WHERE RepairNo = @RepairNo AND RepairPaymentNo = @RepairPaymentNo
    `);

    // Insert cancellation in PayMode
    const pmReq = new sql.Request(transaction);
    pmReq.input("RefNo", sql.VarChar, repairNo);
    pmReq.input("PayDate", sql.DateTime, new Date());
    pmReq.input("PayMode", sql.VarChar, "CASH"); // VB default
    pmReq.input("Amount", sql.Decimal(18, 2), -parseFloat(cancelAmount));
    pmReq.input("Remarks", sql.VarChar, "RP CANCEL");
    pmReq.input("TransType", sql.VarChar, 'REPAIR');
    
    await pmReq.query(`
      INSERT INTO tblPayModeDetail (
        RefNo, PayDate, PayMode, Amount, Remarks, TransType
      ) VALUES (
        @RefNo, @PayDate, @PayMode, @Amount, @Remarks, @TransType
      )
    `);

    await transaction.commit();
    res.json({ success: true, message: "Repair payment cancelled successfully" });
  } catch (err) {
    await transaction.rollback();
    console.error("Cancel Repair Payment Error:", err);
    res.status(500).json({ success: false, message: "Failed to cancel repair payment", error: err.message });
  }
});

module.exports = router;
