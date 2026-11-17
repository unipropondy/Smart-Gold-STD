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
            SELECT TOP 1 CustomerName, Address1, Address2, Address3, CustomerType, ISNULL(Phone1, ISNULL(Mobile, '')) AS Phone
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

// 6. GET print / reprint repair receipt details (matching subPrintRepair in frmRepairPayment.vb)
router.get("/print-data", async (req, res) => {
  try {
    const repairNo = (req.query.repairNo || req.query.orderNo || '').trim();
    if (!repairNo) {
      return res.status(400).json({ success: false, message: "Repair No is required for print." });
    }

    const pool = await getPool(req);

    // 1. Fetch Company details matching objClsCompany
    const compRes = await pool.request().query("SELECT TOP 1 * FROM tblCompany");
    const rawCompany = compRes.recordset[0] || {};
    const company = {
      name: String(rawCompany.Name || rawCompany.CompanyName || '').trim(),
      address1: String(rawCompany.Address1 || '').trim(),
      address2: String(rawCompany.Address2 || '').trim(),
      city: String(rawCompany.City || '').trim(),
      state: String(rawCompany.State || '').trim(),
      country: String(rawCompany.Country || '').trim(),
      phone: String(rawCompany.Phone || '').trim(),
      fax: String(rawCompany.Fax || '').trim(),
      companyShortCode: String(rawCompany.CompanyShortCode || '').trim().toUpperCase(),
    };

    // 2. Fetch Repair Header + Customer Details
    const headerRes = await pool.request()
      .input("RepairNo", sql.VarChar, repairNo)
      .query(`
        SELECT 
          h.*,
          RTRIM(LTRIM(ISNULL(c.CustomerName, ''))) AS CustomerName,
          RTRIM(LTRIM(ISNULL(c.Address1, ''))) AS CustomerAddress1,
          RTRIM(LTRIM(ISNULL(c.Address2, ''))) AS CustomerAddress2,
          RTRIM(LTRIM(ISNULL(c.Address3, ''))) AS CustomerAddress3,
          RTRIM(LTRIM(ISNULL(c.Phone1, ISNULL(c.Mobile, '')))) AS CustomerPhone,
          RTRIM(LTRIM(ISNULL(c.CustomerType, ''))) AS CustomerType
        FROM tblInventoryRepairHeader h
        LEFT JOIN tblCustomer c ON RTRIM(LTRIM(c.CustomerCode)) = RTRIM(LTRIM(h.CustomerCode))
        WHERE RTRIM(LTRIM(h.RepairNo)) = @RepairNo
      `);

    if (headerRes.recordset.length === 0) {
      return res.status(404).json({ success: false, message: `Repair docket ${repairNo} not found.` });
    }

    const header = headerRes.recordset[0];

    // 3. Fetch Repair Details (line items)
    let details = [];
    try {
      const detailRes = await pool.request()
        .input("RepairNo", sql.VarChar, repairNo)
        .query(`
          SELECT * 
          FROM tblInventoryRepairDetail 
          WHERE RTRIM(LTRIM(RepairNo)) = @RepairNo 
          ORDER BY LineNo ASC
        `);
      details = detailRes.recordset || [];
    } catch (eDet) {
      console.warn("Could not query tblInventoryRepairDetail for print:", eDet.message);
    }

    // 4. Fetch Payment History (from tblRepairPaymentDetail, fallback to tblOrderPaymentDetail)
    let payments = [];
    try {
      const payRes = await pool.request()
        .input("RepairNo", sql.VarChar, repairNo)
        .query(`
          SELECT RepairNo, RepairPaymentNo, AmountPaid, Remarks, CreateDate, CreateUser
          FROM tblRepairPaymentDetail
          WHERE RTRIM(LTRIM(RepairNo)) = @RepairNo
          ORDER BY CreateDate ASC
        `);
      payments = payRes.recordset || [];
    } catch (ePay) {
      console.warn("Could not query tblRepairPaymentDetail for print:", ePay.message);
    }

    if (payments.length === 0) {
      try {
        const fallbackRes = await pool.request()
          .input("OrderNo", sql.VarChar, repairNo)
          .query(`
            SELECT OrderNo AS RepairNo, OrderPaymentNo AS RepairPaymentNo, AmountPaid, Remarks, CreateDate, CreateUser
            FROM tblOrderPaymentDetail
            WHERE RTRIM(LTRIM(OrderNo)) = @OrderNo
            ORDER BY CreateDate ASC
          `);
        payments = fallbackRes.recordset || [];
      } catch (eFallback) {}
    }

    // 5. Fetch Class Gold Rates (subGetClassRate in VB.NET)
    let rates = { rate999: '', rate916: '', rate835: '', rate24k999: '', rate20k850: '', rate22ksov: '' };
    try {
      const classRes = await pool.request().query("SELECT ClassCode, Rate FROM tblClass");
      if (classRes.recordset && classRes.recordset.length > 0) {
        for (const r of classRes.recordset) {
          const cCode = String(r.ClassCode || '').trim().toUpperCase();
          const rVal = parseFloat(r.Rate) || 0;
          const rFormatted = rVal.toFixed(2);
          if (cCode === '916' || cCode === '22K-916') rates.rate916 = rFormatted;
          else if (cCode === '999' || cCode === '24K') rates.rate999 = rFormatted;
          else if (cCode === '20KJ' || cCode === '835') rates.rate835 = rFormatted;
          else if (cCode === '24K-999') rates.rate24k999 = rFormatted;
          else if (cCode === '20K-850') rates.rate20k850 = rFormatted;
          else if (cCode === '22K-SOV') rates.rate22ksov = rFormatted;
        }
      }
    } catch (eRates) {
      console.warn("Class rate fetch warning for print:", eRates.message);
    }

    res.json({
      success: true,
      data: {
        company,
        header,
        details,
        payments,
        rates
      }
    });
  } catch (err) {
    console.error("Print Data Fetch Error:", err);
    res.status(500).json({ success: false, message: "Failed to fetch repair print data", error: err.message });
  }
});

// 7. GET reprint repair receipt details (matching subPrintRepair in frmRepairPayment.vb)
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
          ISNULL(c.Phone1, ISNULL(c.Mobile, '')) AS Phone,
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

// ================= EXPORT PDF ENDPOINT =================
// POST /api/repairpayment/export-pdf
router.post("/export-pdf", async (req, res) => {
  try {
    const { html, filename } = req.body;
    if (!html) {
      return res.status(400).json({ success: false, message: "HTML content is required for PDF export." });
    }

    const { convertHtmlToPdf } = require("../utils/htmlToPdf");
    const pdfBuffer = await convertHtmlToPdf(html);
    const downloadName = (filename || "Repair_Payment_Receipt.pdf").replace(/[^a-zA-Z0-9_\-\.]/g, "_");

    res.setHeader("Content-Type", "application/pdf");
    res.setHeader("Content-Disposition", `attachment; filename="${downloadName}"`);
    res.setHeader("Content-Length", pdfBuffer.length);
    return res.send(pdfBuffer);
  } catch (err) {
    console.error("Repair Payment Export PDF Error:", err);
    return res.status(500).json({ success: false, message: "Failed to generate PDF: " + err.message });
  }
});

// ================= EXPORT EXCEL ENDPOINT =================
// POST /api/repairpayment/export-excel
router.post("/export-excel", async (req, res) => {
  try {
    const { repairNo, isReprint } = req.body;
    const rawNo = (repairNo || "").trim();
    if (!rawNo) {
      return res.status(400).json({ success: false, message: "Repair Number is required." });
    }

    const pool = await getPool(req);

    // 1. Company
    const compResult = await pool.request().query("SELECT TOP 1 * FROM tblCompany");
    const compRow = compResult.recordset[0] || {};
    const companyName = (compRow.Name || "SMART").trim();
    const companyAddress = [compRow.Address1, compRow.Address2, compRow.City, compRow.Country].filter(Boolean).map(s => String(s).trim()).join(" ");

    // 2. Repair Header & Customer
    const headerRes = await pool.request()
      .input("RepairNo", sql.VarChar, rawNo)
      .query(`
        SELECT 
          h.*,
          RTRIM(LTRIM(ISNULL(c.CustomerName, ''))) AS CustomerName,
          RTRIM(LTRIM(ISNULL(c.Address1, ''))) AS CustomerAddress1,
          RTRIM(LTRIM(ISNULL(c.Address2, ''))) AS CustomerAddress2,
          RTRIM(LTRIM(ISNULL(c.Address3, ''))) AS CustomerAddress3,
          RTRIM(LTRIM(ISNULL(c.Phone1, ISNULL(c.Mobile, '')))) AS CustomerPhone,
          RTRIM(LTRIM(ISNULL(c.CustomerType, ''))) AS CustomerType
        FROM tblInventoryRepairHeader h
        LEFT JOIN tblCustomer c ON RTRIM(LTRIM(c.CustomerCode)) = RTRIM(LTRIM(h.CustomerCode))
        WHERE RTRIM(LTRIM(h.RepairNo)) = @RepairNo
      `);

    if (headerRes.recordset.length === 0) {
      return res.status(404).json({ success: false, message: `Repair record "${rawNo}" not found.` });
    }

    const header = headerRes.recordset[0];

    // 3. Payment history
    let payments = [];
    const payRes = await pool.request()
      .input("RepairNo", sql.VarChar, rawNo)
      .query(`
        SELECT RepairNo, RepairPaymentNo, AmountPaid, Remarks, CreateDate, CreateUser
        FROM tblRepairPaymentDetail
        WHERE RTRIM(LTRIM(RepairNo)) = @RepairNo
        ORDER BY CreateDate ASC
      `);
    payments = payRes.recordset || [];

    if (payments.length === 0) {
      const fallbackRes = await pool.request()
        .input("OrderNo", sql.VarChar, rawNo)
        .query(`
          SELECT OrderNo AS RepairNo, OrderPaymentNo AS RepairPaymentNo, AmountPaid, Remarks, CreateDate, CreateUser
          FROM tblOrderPaymentDetail
          WHERE RTRIM(LTRIM(OrderNo)) = @OrderNo
          ORDER BY CreateDate ASC
        `);
      payments = fallbackRes.recordset || [];
    }

    // 4. Gold Rates
    const ratesResult = await pool.request().query("SELECT ClassCode, Rate FROM tblClass WHERE ClassCode IN ('24K', '22K', '835', '999', '916', '18K', 'SIL')");
    const ratesMap = {};
    ratesResult.recordset.forEach((r) => {
      ratesMap[(r.ClassCode || "").trim().toUpperCase()] = r.Rate;
    });
    const s999 = ratesMap["24K"] !== undefined ? parseFloat(ratesMap["24K"]).toFixed(2) : (ratesMap["999"] ? parseFloat(ratesMap["999"]).toFixed(2) : "-");
    const s916 = ratesMap["22K"] !== undefined ? parseFloat(ratesMap["22K"]).toFixed(2) : (ratesMap["916"] ? parseFloat(ratesMap["916"]).toFixed(2) : "-");
    const s835 = ratesMap["835"] !== undefined ? parseFloat(ratesMap["835"]).toFixed(2) : (ratesMap["18K"] ? parseFloat(ratesMap["18K"]).toFixed(2) : "-");

    // Dates
    const rawDate = header.RepairDate || header.CreateDate;
    let repairDateStr = "-";
    if (rawDate) {
      const d = new Date(rawDate);
      if (!isNaN(d.getTime())) {
        repairDateStr = `${String(d.getDate()).padStart(2, '0')}/${String(d.getMonth() + 1).padStart(2, '0')}/${d.getFullYear()}`;
      }
    }
    const printTimeStr = new Date().toLocaleTimeString('en-US', { hour: '2-digit', minute: '2-digit', hour12: true });

    const reportTitle = "REPAIR PAYMENT";

    const XLSX = require("xlsx");
    const wb = XLSX.utils.book_new();
    const rows = [];

    rows.push([companyName]);
    rows.push([companyAddress]);
    rows.push([]);
    rows.push([reportTitle]);
    rows.push([`Printed: ${repairDateStr} ${printTimeStr}`]);
    rows.push([]);

    // Two panels
    const custName = (header.CustomerName || "").trim();
    const custAddress = [header.CustomerAddress1, header.CustomerAddress2, header.CustomerAddress3].filter(Boolean).map(s => String(s).trim()).join(" ");

    rows.push(["CUSTOMER INFORMATION", "", "", "REPAIR DOCKET DETAILS"]);
    rows.push(["Customer Code:", (header.CustomerCode || "-").trim(), "", "Repair Docket No:", rawNo]);
    rows.push(["Customer Name:", custName || "CASH CUSTOMER", "", "Repair Date:", repairDateStr]);
    rows.push(["Address:", custAddress || "-", "", "Salesman:", (header.CreateUser || "Staff").trim()]);
    rows.push(["Contact:", (header.CustomerPhone || "-").trim(), "", "Status:", (header.Status || "COMPLETED").trim()]);
    rows.push([]);

    const totalEstCost = parseFloat(header.TotalCostEstimated) || 0;
    const initialAdvance = parseFloat(header.AdvancePaid) || 0;
    const totalPaymentsReceived = payments.reduce((acc, p) => acc + (parseFloat(p.AmountPaid) || 0), 0);
    const totalPaidToDate = initialAdvance + totalPaymentsReceived;
    const balanceDue = Math.max(0, totalEstCost - totalPaidToDate);

    rows.push(["TOTAL ESTIMATED REPAIR COST:", `$${totalEstCost.toFixed(2)}`, "", `REMAINING BALANCE: $${balanceDue.toFixed(2)}`]);
    rows.push([]);

    // Payments Table
    rows.push(["#", "PAYMENT VOUCHER NO", "PAYMENT DATE", "REMARKS", "AMOUNT PAID ($)"]);
    if (payments.length > 0) {
      payments.forEach((p, idx) => {
        let pDate = "-";
        if (p.CreateDate) {
          const pd = new Date(p.CreateDate);
          if (!isNaN(pd.getTime())) {
            pDate = `${String(pd.getDate()).padStart(2, '0')}/${String(pd.getMonth() + 1).padStart(2, '0')}/${pd.getFullYear()}`;
          }
        }
        rows.push([
          idx + 1,
          (p.RepairPaymentNo || rawNo).trim(),
          pDate,
          (p.Remarks || "-").trim(),
          parseFloat(p.AmountPaid || 0)
        ]);
      });
    } else {
      rows.push([1, rawNo, repairDateStr, "Initial Advance", initialAdvance]);
    }
    rows.push([]);

    // Financial totals
    rows.push(["", "", "", "Total Repair Cost:", totalEstCost]);
    rows.push(["", "", "", "Initial Advance Paid:", initialAdvance]);
    rows.push(["", "", "", "Settlement Payments:", totalPaymentsReceived]);
    rows.push(["", "", "", "Total Received To Date:", totalPaidToDate]);
    rows.push(["", "", "", "REMAINING BALANCE DUE:", balanceDue]);
    rows.push([]);

    // Signatures
    rows.push(["PREPARED BY (STAFF)", "", "", "SUPPLIER / AUTHORIZED RECEIVER"]);
    rows.push(["Staff Name:", (header.CreateUser || "Staff").trim(), "", "Receiver Name:", custName || "CASH CUSTOMER"]);
    rows.push(["Signature:", "______________________", "", "Signature:", "______________________"]);
    rows.push(["Date:", "______________________", "", "Date:", "______________________"]);
    rows.push([]);
    rows.push(["*** System Generated Report ***"]);
    rows.push(["Powered by Unipro SG"]);

    const ws = XLSX.utils.aoa_to_sheet(rows);
    ws["!cols"] = [{ wch: 6 }, { wch: 24 }, { wch: 16 }, { wch: 30 }, { wch: 18 }];
    XLSX.utils.book_append_sheet(wb, ws, "Repair Payment");

    const excelBuffer = XLSX.write(wb, { type: "buffer", bookType: "xlsx" });
    const downloadName = `Repair_Payment_${rawNo.replace(/[^a-zA-Z0-9_\-]/g, "_")}.xlsx`;

    res.setHeader("Content-Type", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
    res.setHeader("Content-Disposition", `attachment; filename="${downloadName}"`);
    res.setHeader("Content-Length", excelBuffer.length);
    return res.send(excelBuffer);
  } catch (err) {
    console.error("Repair Payment Export Excel Error:", err);
    return res.status(500).json({ success: false, message: "Failed to generate Excel: " + err.message });
  }
});

module.exports = router;
