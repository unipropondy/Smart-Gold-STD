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

// Dynamic schema helper to safely inspect table columns
async function getTableColumns(pool, tableName) {
  try {
    const res = await pool.request()
      .input("tblName", sql.VarChar, tableName)
      .query(`
        SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE
        FROM INFORMATION_SCHEMA.COLUMNS
        WHERE LOWER(TABLE_NAME) = LOWER(@tblName)
      `);
    return res.recordset.map(r => ({
      name: r.COLUMN_NAME,
      lower: r.COLUMN_NAME.toLowerCase(),
      type: r.DATA_TYPE.toLowerCase(),
      nullable: r.IS_NULLABLE === 'YES'
    }));
  } catch (err) {
    console.warn(`Could not inspect schema for ${tableName}:`, err.message);
    return [];
  }
}

// 1. GET next repair payment number (matching VB fncGetNextOrderPaymentNo / tblCompany)
router.get("/next-no", async (req, res) => {
  try {
    const pool = await getPool(req);
    let loc = "HQ";
    let nextNo = 1;

    try {
      const compRes = await pool.request().query("SELECT TOP 1 * FROM tblCompany");
      if (compRes.recordset.length > 0) {
        const comp = compRes.recordset[0];
        loc = (comp.LocationCode || "HQ").trim();
        if (comp.NextOrderPaymentNo != null) {
          nextNo = parseInt(comp.NextOrderPaymentNo, 10) || 1;
        }
      }
    } catch (e1) {
      console.warn("tblCompany next-no warning:", e1.message);
    }

    // Standard VB prefix: RP followed by number or RP-HQ00000001
    const paymentNo = `RP${nextNo}`;
    res.json({ success: true, paymentNo, rawNo: nextNo, locationCode: loc });
  } catch (err) {
    console.error("Fetch Next Repair Payment Number Error:", err);
    res.status(500).json({ success: false, message: "Failed to generate next repair payment number", error: err.message });
  }
});

// 2. GET repairs for LOV lookup (matching btnOrderPaymentNoSearch_Click in frmRepairPayment.vb)
// WHERE (Status = 'R' or Status = 'P') and AdvancePaid < TotalCostEstimated
router.get("/repairs", async (req, res) => {
  try {
    const pool = await getPool(req);
    const result = await pool.request().query(`
      SELECT 
        h.RepairNo, 
        h.RepairDate, 
        h.CustomerCode,
        c.CustomerName,
        h.SupplierCode,
        ISNULL(h.TotalCostEstimated, 0) AS TotalCostEstimated,
        ISNULL(h.AdvancePaid, 0) AS AdvancePaid,
        (ISNULL(h.TotalCostEstimated, 0) - ISNULL(h.AdvancePaid, 0)) AS BalanceAmount,
        h.Status
      FROM tblInventoryRepairHeader h
      LEFT JOIN tblCustomer c ON c.CustomerCode = h.CustomerCode
      WHERE (h.Status = 'R' OR h.Status = 'P' OR h.Status IS NULL)
        AND ISNULL(h.AdvancePaid, 0) < ISNULL(h.TotalCostEstimated, 0)
      ORDER BY h.RepairDate DESC, h.RepairNo DESC
    `);
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Repairs Error:", err);
    res.status(500).json({ success: false, message: "Failed to fetch repairs", error: err.message });
  }
});

// 3. GET balance and payment history (matching subGetOrderDetails in frmRepairPayment.vb)
router.get("/balance/:repairNo", async (req, res) => {
  const { repairNo } = req.params;
  try {
    const pool = await getPool(req);
    const request = pool.request();
    request.input("RepairNo", sql.VarChar, (repairNo || '').trim());

    // 1. Get repair header details first
    const repairResult = await request.query(`
      SELECT 
        a.RepairNo, 
        a.RepairDate,
        a.CustomerCode, 
        a.SupplierCode,
        ISNULL(a.TotalCostEstimated, 0) AS TotalCostEstimated, 
        ISNULL(a.AdvancePaid, 0) AS AdvancePaid
      FROM tblInventoryRepairHeader a
      WHERE RTRIM(LTRIM(a.RepairNo)) = @RepairNo
    `);

    if (repairResult.recordset.length === 0) {
      return res.status(404).json({ success: false, message: "Repair docket not found" });
    }

    const repairData = repairResult.recordset[0];
    const estimatedCost = parseFloat(repairData.TotalCostEstimated) || 0;
    const advancePaid = parseFloat(repairData.AdvancePaid) || 0;
    const custCode = (repairData.CustomerCode || '').trim();

    // 2. Fetch customer info safely if CustomerCode is present
    let custInfo = {
      CustomerName: '',
      Address1: '',
      Address2: '',
      Address3: '',
      CustomerType: '',
      Phone: ''
    };

    if (custCode) {
      try {
        const custRes = await pool.request()
          .input("CustomerCode", sql.VarChar, custCode)
          .query(`
            SELECT TOP 1 CustomerName, Address1, Address2, Address3, CustomerType, Phone
            FROM tblCustomer
            WHERE RTRIM(LTRIM(CustomerCode)) = @CustomerCode
          `);
        if (custRes.recordset.length > 0) {
          custInfo = custRes.recordset[0];
        }
      } catch (eCust) {
        console.warn("Customer fetch warning:", eCust.message);
      }
    }

    // 3. Get payment history from tblRepairPaymentDetail
    let historyRows = [];
    try {
      const historyResult = await pool.request()
        .input("RepairNo", sql.VarChar, (repairNo || '').trim())
        .query(`
          SELECT RepairNo, RepairPaymentNo, AmountPaid, Remarks
          FROM tblRepairPaymentDetail
          WHERE RTRIM(LTRIM(RepairNo)) = @RepairNo
        `);
      historyRows = historyResult.recordset || [];
    } catch (eHistory) {
      console.warn("Could not query tblRepairPaymentDetail:", eHistory.message);
    }

    // Fallback: If no records in tblRepairPaymentDetail, also check tblOrderPaymentDetail
    if (historyRows.length === 0) {
      try {
        const fallbackRes = await pool.request()
          .input("OrderNo", sql.VarChar, (repairNo || '').trim())
          .query(`
            SELECT OrderNo AS RepairNo, OrderPaymentNo AS RepairPaymentNo, AmountPaid, Remarks
            FROM tblOrderPaymentDetail
            WHERE RTRIM(LTRIM(OrderNo)) = @OrderNo
          `);
        if (fallbackRes.recordset.length > 0) {
          historyRows = fallbackRes.recordset;
        }
      } catch (eFallback) {}
    }

    let totalAmountPaid = 0;
    historyRows.forEach(row => {
      totalAmountPaid += (parseFloat(row.AmountPaid) || 0);
    });
    
    const balanceAmount = estimatedCost - (advancePaid + totalAmountPaid);

    res.json({ 
      success: true, 
      data: {
        repair: {
          ...repairData,
          ...custInfo
        },
        balance: {
          estimatedCost,
          advancePaid,
          totalAmountPaid,
          balanceAmount: balanceAmount > 0 ? balanceAmount : 0
        },
        history: historyRows
      } 
    });
  } catch (err) {
    console.error("Fetch Balance Error:", err);
    res.status(500).json({ success: false, message: "Failed to fetch balance details", error: err.message });
  }
});

// 4. POST pay repair (matching btnSave_Click, fncPaymodeDetailSave, fncSaveOrderPaymentDetail)
router.post("/pay", async (req, res) => {
  const {
    repairNo,
    repairPaymentNo,
    amountPaid,
    remarks,
    paymodeDetails,
    createUser,
    locationCode
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

    // 1. Generate Repair Payment No if not provided
    let finalPaymentNo = repairPaymentNo;
    if (!finalPaymentNo) {
      const numRes = await new sql.Request(transaction).query(
        "SELECT TOP 1 NextOrderPaymentNo FROM tblCompany"
      );
      const nextNum = numRes.recordset.length > 0 ? numRes.recordset[0].NextOrderPaymentNo || 1 : 1;
      finalPaymentNo = `RP${nextNum}`;
    }

    const nAmountPaid = parseFloat(amountPaid) || 0;
    const sUser = (createUser || 'ADMIN').trim();
    const sRemarks = (remarks || 'REPAIR PAYMENT').trim();
    const sLocation = (locationCode || 'HQ').trim();
    const now = new Date();

    // Inspect tblRepairPaymentDetail schema
    const rpdCols = await getTableColumns(pool, 'tblRepairPaymentDetail');
    const rpdColNames = rpdCols.map(c => c.lower);

    // Build insert for tblRepairPaymentDetail
    const insertDetailReq = new sql.Request(transaction);
    const detailCols = ["RepairNo", "RepairPaymentNo", "AmountPaid", "Remarks"];
    const detailVals = ["@RepairNo", "@RepairPaymentNo", "@AmountPaid", "@Remarks"];

    insertDetailReq.input("RepairNo", sql.VarChar, String(repairNo).trim());
    insertDetailReq.input("RepairPaymentNo", sql.VarChar, String(finalPaymentNo).trim());
    insertDetailReq.input("AmountPaid", sql.Decimal(18, 2), nAmountPaid);
    insertDetailReq.input("Remarks", sql.VarChar, sRemarks);

    if (rpdColNames.includes("createdate")) {
      detailCols.push("CreateDate");
      detailVals.push("@CreateDate");
      insertDetailReq.input("CreateDate", sql.DateTime, now);
    }
    if (rpdColNames.includes("createuser")) {
      detailCols.push("CreateUser");
      detailVals.push("@CreateUser");
      insertDetailReq.input("CreateUser", sql.VarChar, sUser);
    }
    if (rpdColNames.includes("locationcode")) {
      detailCols.push("LocationCode");
      detailVals.push("@LocationCode");
      insertDetailReq.input("LocationCode", sql.VarChar, sLocation);
    }

    await insertDetailReq.query(`
      INSERT INTO tblRepairPaymentDetail (${detailCols.join(", ")})
      VALUES (${detailVals.join(", ")})
    `);

    // Inspect tblPayModeDetail schema
    const pmCols = await getTableColumns(pool, 'tblPayModeDetail');
    const pmColNames = pmCols.map(c => c.lower);

    // Handle Paymode Details (SalesType = 'REPAIR')
    const payModesToInsert = (paymodeDetails && Array.isArray(paymodeDetails) && paymodeDetails.length > 0)
      ? paymodeDetails
      : [{ payMode: 'CASH', amount: nAmountPaid, remarks: 'MANUAL' }];

    for (const pm of payModesToInsert) {
      const pmAmount = parseFloat(pm.amount || pm.Amount) || 0;
      if (pmAmount > 0) {
        const pmReq = new sql.Request(transaction);
        const pCols = [];
        const pVals = [];

        // Check if invoice column is InvoiceNo or RefNo
        if (pmColNames.includes("invoiceno")) {
          pCols.push("InvoiceNo");
          pVals.push("@InvoiceNo");
          pmReq.input("InvoiceNo", sql.VarChar, String(repairNo).trim());
        }
        if (pmColNames.includes("refno")) {
          pCols.push("RefNo");
          pVals.push("@RefNo");
          pmReq.input("RefNo", sql.VarChar, String(finalPaymentNo).trim());
        }
        if (pmColNames.includes("invoicedate")) {
          pCols.push("InvoiceDate");
          pVals.push("@InvoiceDate");
          pmReq.input("InvoiceDate", sql.DateTime, now);
        }
        if (pmColNames.includes("paydate")) {
          pCols.push("PayDate");
          pVals.push("@PayDate");
          pmReq.input("PayDate", sql.DateTime, now);
        }
        if (pmColNames.includes("paymode")) {
          pCols.push("PayMode");
          pVals.push("@PayMode");
          pmReq.input("PayMode", sql.VarChar, String(pm.payMode || pm.PayMode || 'CASH').trim());
        }
        if (pmColNames.includes("amount")) {
          pCols.push("Amount");
          pVals.push("@Amount");
          pmReq.input("Amount", sql.Decimal(18, 2), pmAmount);
        }
        if (pmColNames.includes("foreignamount")) {
          pCols.push("ForeignAmount");
          pVals.push("@ForeignAmount");
          pmReq.input("ForeignAmount", sql.Decimal(18, 2), parseFloat(pm.foreignAmount || 0));
        }
        if (pmColNames.includes("remarks")) {
          pCols.push("Remarks");
          pVals.push("@Remarks");
          pmReq.input("Remarks", sql.VarChar, String(pm.remarks || sRemarks || 'REPAIR PAYMENT').trim());
        }
        if (pmColNames.includes("salestype")) {
          pCols.push("SalesType");
          pVals.push("@SalesType");
          pmReq.input("SalesType", sql.VarChar, 'REPAIR');
        }
        if (pmColNames.includes("transtype")) {
          pCols.push("TransType");
          pVals.push("@TransType");
          pmReq.input("TransType", sql.VarChar, 'REPAIR');
        }
        if (pmColNames.includes("locationcode")) {
          pCols.push("LocationCode");
          pVals.push("@LocationCode");
          pmReq.input("LocationCode", sql.VarChar, sLocation);
        }
        if (pmColNames.includes("createuser")) {
          pCols.push("CreateUser");
          pVals.push("@CreateUser");
          pmReq.input("CreateUser", sql.VarChar, sUser);
        }
        if (pmColNames.includes("createdate")) {
          pCols.push("CreateDate");
          pVals.push("@CreateDate");
          pmReq.input("CreateDate", sql.DateTime, now);
        }
        if (pmColNames.includes("ispaid")) {
          pCols.push("IsPaid");
          pVals.push("1");
        }

        if (pCols.length > 0) {
          await pmReq.query(`
            INSERT INTO tblPayModeDetail (${pCols.join(", ")})
            VALUES (${pVals.join(", ")})
          `);
        }
      }
    }

    // Update Company Counter (NextOrderPaymentNo)
    await new sql.Request(transaction).query(
      "UPDATE tblCompany SET NextOrderPaymentNo = ISNULL(NextOrderPaymentNo, 0) + 1"
    );

    // Check if fully settled
    const balCheckReq = new sql.Request(transaction);
    balCheckReq.input("RepairNo", sql.VarChar, String(repairNo).trim());
    const balRes = await balCheckReq.query(`
      SELECT 
        h.TotalCostEstimated, 
        h.AdvancePaid,
        ISNULL((SELECT SUM(AmountPaid) FROM tblRepairPaymentDetail WHERE RepairNo = h.RepairNo), 0) AS TotalPaid
      FROM tblInventoryRepairHeader h
      WHERE h.RepairNo = @RepairNo
    `);

    let repairCompleted = false;
    if (balRes.recordset.length > 0) {
      const rEst = parseFloat(balRes.recordset[0].TotalCostEstimated) || 0;
      const rAdv = parseFloat(balRes.recordset[0].AdvancePaid) || 0;
      const rPaid = parseFloat(balRes.recordset[0].TotalPaid) || 0;
      const remainingBal = rEst - (rAdv + rPaid);

      if (remainingBal <= 0) {
        repairCompleted = true;
        // Optionally update Repair Status to Completed / Delivered ('D')
        await new sql.Request(transaction)
          .input("RepairNo", sql.VarChar, String(repairNo).trim())
          .query("UPDATE tblInventoryRepairHeader SET Status = 'D' WHERE RepairNo = @RepairNo");
      }
    }

    await transaction.commit();
    res.json({ 
      success: true, 
      message: "Repair payment saved successfully.", 
      paymentNo: finalPaymentNo,
      repairCompleted 
    });
  } catch (err) {
    try {
      await transaction.rollback();
    } catch (rbErr) {}
    console.error("Repair Payment Save Error:", err);
    res.status(500).json({ success: false, message: "Failed to save repair payment", error: err.message });
  }
});

// 5. DELETE cancel payment (matching subCancelRepairPayment in frmRepairPayment.vb)
router.delete("/pay/:repairNo/:paymentNo", async (req, res) => {
  const { repairNo, paymentNo } = req.params;

  let pool;
  try {
    pool = await getPool(req);
  } catch (poolErr) {
    return res.status(500).json({ success: false, message: poolErr.message });
  }

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
      return res.status(404).json({ success: false, message: "Repair payment record not found." });
    }

    const cancelAmount = parseFloat(amountRes.recordset[0].AmountPaid) || 0;

    // Delete Detail row from tblRepairPaymentDetail
    const deleteDetailReq = new sql.Request(transaction);
    deleteDetailReq.input("RepairNo", sql.VarChar, repairNo);
    deleteDetailReq.input("RepairPaymentNo", sql.VarChar, paymentNo);

    await deleteDetailReq.query(`
      DELETE FROM tblRepairPaymentDetail
      WHERE RepairNo = @RepairNo AND RepairPaymentNo = @RepairPaymentNo
    `);

    // Insert reversal in tblPayModeDetail with -cancelAmount and remarks 'RP CANCEL'
    const pmCols = await getTableColumns(pool, 'tblPayModeDetail');
    const pmColNames = pmCols.map(c => c.lower);

    const pmReq = new sql.Request(transaction);
    const pCols = [];
    const pVals = [];
    const now = new Date();

    if (pmColNames.includes("invoiceno")) {
      pCols.push("InvoiceNo");
      pVals.push("@InvoiceNo");
      pmReq.input("InvoiceNo", sql.VarChar, String(repairNo).trim());
    }
    if (pmColNames.includes("refno")) {
      pCols.push("RefNo");
      pVals.push("@RefNo");
      pmReq.input("RefNo", sql.VarChar, String(paymentNo).trim());
    }
    if (pmColNames.includes("invoicedate")) {
      pCols.push("InvoiceDate");
      pVals.push("@InvoiceDate");
      pmReq.input("InvoiceDate", sql.DateTime, now);
    }
    if (pmColNames.includes("paydate")) {
      pCols.push("PayDate");
      pVals.push("@PayDate");
      pmReq.input("PayDate", sql.DateTime, now);
    }
    if (pmColNames.includes("paymode")) {
      pCols.push("PayMode");
      pVals.push("'CASH'");
    }
    if (pmColNames.includes("amount")) {
      pCols.push("Amount");
      pVals.push("@Amount");
      pmReq.input("Amount", sql.Decimal(18, 2), -cancelAmount);
    }
    if (pmColNames.includes("remarks")) {
      pCols.push("Remarks");
      pVals.push("'RP CANCEL'");
    }
    if (pmColNames.includes("salestype")) {
      pCols.push("SalesType");
      pVals.push("'REPAIR'");
    }
    if (pmColNames.includes("transtype")) {
      pCols.push("TransType");
      pVals.push("'REPAIR'");
    }
    if (pmColNames.includes("locationcode")) {
      pCols.push("LocationCode");
      pVals.push("'HQ'");
    }
    if (pmColNames.includes("createuser")) {
      pCols.push("CreateUser");
      pVals.push("'SYS'");
    }
    if (pmColNames.includes("createdate")) {
      pCols.push("CreateDate");
      pVals.push("@CreateDate");
      pmReq.input("CreateDate", sql.DateTime, now);
    }
    if (pmColNames.includes("ispaid")) {
      pCols.push("IsPaid");
      pVals.push("1");
    }

    if (pCols.length > 0) {
      await pmReq.query(`
        INSERT INTO tblPayModeDetail (${pCols.join(", ")})
        VALUES (${pVals.join(", ")})
      `);
    }

    await transaction.commit();
    res.json({ success: true, message: "Repair payment cancelled successfully." });
  } catch (err) {
    try {
      await transaction.rollback();
    } catch (rbErr) {}
    console.error("Cancel Repair Payment Error:", err);
    res.status(500).json({ success: false, message: "Failed to cancel repair payment", error: err.message });
  }
});

// 6. GET reprint repair receipt details (matching subPrintRepair in frmRepairPayment.vb)
router.get("/reprint/:repairNo", async (req, res) => {
  const { repairNo } = req.params;
  try {
    const pool = await getPool(req);
    const result = await pool.request()
      .input("RepairNo", sql.VarChar, repairNo)
      .query(`
        SELECT 
          h.RepairNo,
          h.RepairDate,
          h.CustomerCode,
          c.CustomerName,
          c.Address1,
          c.Phone,
          ISNULL(h.TotalCostEstimated, 0) AS TotalCostEstimated,
          ISNULL(h.AdvancePaid, 0) AS AdvancePaid
        FROM tblInventoryRepairHeader h
        LEFT JOIN tblCustomer c ON c.CustomerCode = h.CustomerCode
        WHERE h.RepairNo = @RepairNo
      `);

    if (result.recordset.length === 0) {
      return res.status(404).json({ success: false, message: "Repair docket not found for reprinting" });
    }

    res.json({ success: true, data: result.recordset[0] });
  } catch (err) {
    console.error("Reprint Fetch Error:", err);
    res.status(500).json({ success: false, message: "Failed to fetch reprint details", error: err.message });
  }
});

module.exports = router;
