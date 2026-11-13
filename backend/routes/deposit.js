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

// GET all deposits
router.get("/", async (req, res) => {
  try {
    const pool = await getPool(req);
    const result = await pool.request().query("SELECT * FROM tblDepositSave ORDER BY CreateDate DESC");
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch All Deposits Error:", err);
    res.status(500).json({ success: false, message: "Failed to fetch deposits", error: err.message });
  }
});

// 1. GET next numbers (Deposit No and Payment No)
router.get("/next-numbers", async (req, res) => {
  try {
    const pool = await getPool(req);
    const result = await pool.request().query(
      "SELECT TOP 1 LocationCode, NextDepositNo, NextCustomerDepositNumber FROM tblCompany"
    );

    if (result.recordset.length > 0) {
      const loc = (result.recordset[0].LocationCode || "HQ").trim();
      const nextDep = result.recordset[0].NextDepositNo || 1;
      const nextPay = result.recordset[0].NextCustomerDepositNumber || 1;

      const nextDepositNo = `${loc}09${padZero(nextDep, 6)}`;
      const nextDepositPaymentNo = `${loc}04${padZero(nextPay, 6)}`;

      res.json({
        success: true,
        nextDepositNo,
        nextDepositPaymentNo
      });
    } else {
      res.json({
        success: true,
        nextDepositNo: "HQ09000001",
        nextDepositPaymentNo: "HQ04000001"
      });
    }
  } catch (err) {
    console.error("Fetch Next Deposit Numbers Error:", err);
    res.status(500).json({ success: false, message: "Failed to generate next deposit numbers", error: err.message });
  }
});

// 2. GET specific Deposit and its linked payment details
router.get("/fetch", async (req, res) => {
  try {
    const pool = await getPool(req);
    const { depositNo } = req.query;

    if (!depositNo) {
      return res.status(400).json({ success: false, message: "DepositNo is required" });
    }

    // Fetch main deposit header
    const mainResult = await pool.request()
      .input("DepositNo", sql.VarChar, depositNo.trim())
      .query("SELECT * FROM tblDepositSave WHERE DepositNo = @DepositNo");

    if (mainResult.recordset.length === 0) {
      return res.status(200).json({ success: false, message: "Deposit not found" });
    }

    const depositData = mainResult.recordset[0];

    // Fetch payment details
    const detailsResult = await pool.request()
      .input("DepositNo", sql.VarChar, depositNo.trim())
      .query("SELECT * FROM tblDepositDetailSave WHERE DepositNo = @DepositNo ORDER BY CreateDate ASC");

    // Fetch customer details if they exist to be absolutely sure we have latest name/address
    let customerData = null;
    if (depositData.CustomerCode) {
      const custResult = await pool.request()
        .input("CustomerCode", sql.VarChar, depositData.CustomerCode.trim())
        .query("SELECT * FROM tblCustomer WHERE CustomerCode = @CustomerCode");
      if (custResult.recordset.length > 0) {
        customerData = custResult.recordset[0];
      }
    }

    res.json({
      success: true,
      data: depositData,
      details: detailsResult.recordset,
      customer: customerData
    });

  } catch (err) {
    console.error("Fetch Deposit Error:", err);
    res.status(500).json({ success: false, message: "Failed to fetch deposit details", error: err.message });
  }
});

// 3. POST - Insert new Deposit or Add Payment to existing Deposit
router.post(["/", "/save"], async (req, res) => {
  const pool = await getPool(req);
  const transaction = new sql.Transaction(pool);

  try {
    const {
      Mode, // "I" for Insert new deposit, "U" for Update (Adding a payment)
      DepositNo,
      DepositDate,
      CustomerCode,
      CustomerName,
      Address1,
      Address2,
      Address3,
      SalesmanCode,
      TotalAmount,
      DepositAmount, // Amount for initial deposit or cumulative paid
      AmountPaid, // Amount paid in the current transaction (in Update mode)
      Remarks,
      InventoryCode,
      InventoryCode1,
      InventoryCode2,
      PayMode, // "CASH", "GOLD", etc.
      CreateUser
    } = req.body;

    const user = CreateUser || "sys";
    await transaction.begin();

    // Query Company Config
    const compResult = await new sql.Request(transaction).query(
      "SELECT TOP 1 LocationCode, NextDepositNo, NextCustomerDepositNumber FROM tblCompany"
    );
    if (compResult.recordset.length === 0) {
      throw new Error("Company configuration not found.");
    }
    const loc = (compResult.recordset[0].LocationCode || "HQ").trim();
    
    let targetDepositNo = DepositNo;
    let targetPaymentNo = "";

    if (Mode === "I") {
      // 1. Generate Next Deposit Agreement No
      const nextDep = compResult.recordset[0].NextDepositNo || 1;
      targetDepositNo = `${loc}09${padZero(nextDep, 6)}`;

      // 2. Insert main header row into tblDepositSave
      const insertHeaderQuery = `
        INSERT INTO tblDepositSave (
          DepositNo, DepositDate, CustomerCode, CustomerName, SalesmanCode, TotalAmount, DepositAmount,
          AmountPaid, Remarks, DepositUsed, InventoryCode, InventoryCode1, InventoryCode2,
          CreateUser, CreateDate, ModifyUser, ModifyDate
        ) VALUES (
          @DepositNo, @DepositDate, @CustomerCode, @CustomerName, @SalesmanCode, @TotalAmount, @DepositAmount,
          0, @Remarks, 'N', @InventoryCode, @InventoryCode1, @InventoryCode2,
          @CreateUser, GETDATE(), @CreateUser, GETDATE()
        )
      `;

      const formattedDepDate = DepositDate ? new Date(DepositDate) : new Date();

      await new sql.Request(transaction)
        .input("DepositNo", sql.VarChar, targetDepositNo)
        .input("DepositDate", sql.DateTime, formattedDepDate)
        .input("CustomerCode", sql.VarChar, CustomerCode ? CustomerCode.trim() : "")
        .input("CustomerName", sql.VarChar, CustomerName ? CustomerName.trim() : "")
        .input("SalesmanCode", sql.VarChar, SalesmanCode ? SalesmanCode.trim() : "")
        .input("TotalAmount", sql.Decimal(18, 4), parseFloat(TotalAmount || 0))
        .input("DepositAmount", sql.Decimal(18, 4), parseFloat(DepositAmount || 0))
        .input("Remarks", sql.NVarChar, Remarks ? Remarks.trim() : "")
        .input("InventoryCode", sql.VarChar, InventoryCode ? InventoryCode.trim() : "")
        .input("InventoryCode1", sql.VarChar, InventoryCode1 ? InventoryCode1.trim() : "")
        .input("InventoryCode2", sql.VarChar, InventoryCode2 ? InventoryCode2.trim() : "")
        .input("CreateUser", sql.VarChar, user)
        .query(insertHeaderQuery);

      // 3. Increment NextDepositNo counter
      await new sql.Request(transaction).query(
        "UPDATE tblCompany SET NextDepositNo = NextDepositNo + 1"
      );

      // 4. Save PayMode details for initial DepositAmount if greater than zero
      const initialDepAmt = parseFloat(DepositAmount || 0);
      if (initialDepAmt > 0) {
        const paymodeQuery = `
          INSERT INTO tblPayModeDetail (
            InvoiceNo, InvoiceDate, PayMode, Amount, Commision, SlipNo,
            CreateUser, CreateDate, ModifyUser, ModifyDate, LocationCode, SalesType, IsPaid
          ) VALUES (
            @InvoiceNo, @InvoiceDate, @PayMode, @Amount, 0, '',
            @CreateUser, GETDATE(), @CreateUser, GETDATE(), @LocationCode, 'DEPOSIT', 1
          )
        `;
        await new sql.Request(transaction)
          .input("InvoiceNo", sql.VarChar, targetDepositNo)
          .input("InvoiceDate", sql.DateTime, formattedDepDate)
          .input("PayMode", sql.VarChar, PayMode || "CASH")
          .input("Amount", sql.Decimal(18, 4), initialDepAmt)
          .input("CreateUser", sql.VarChar, user)
          .input("LocationCode", sql.VarChar, loc)
          .query(paymodeQuery);
      }

    } else {
      // Mode === "U" -> Adding a new payment to an existing deposit
      if (!targetDepositNo) {
        throw new Error("DepositNo is required for adding a payment.");
      }

      // 1. Generate Next Payment Voucher No
      const nextPay = compResult.recordset[0].NextCustomerDepositNumber || 1;
      targetPaymentNo = `${loc}04${padZero(nextPay, 6)}`;
      const amtPaid = parseFloat(AmountPaid || 0);

      // 2. Insert into tblDepositDetailSave
      const insertDetailQuery = `
        INSERT INTO tblDepositDetailSave (
          DepositNo, DepositPaymentNO, AmtPaid, Remarks, DepositPaymentDate,
          CreateUser, CreateDate, ModifyUser, ModifyDate
        ) VALUES (
          @DepositNo, @DepositPaymentNO, @AmtPaid, @Remarks, GETDATE(),
          @CreateUser, GETDATE(), @CreateUser, GETDATE()
        )
      `;

      await new sql.Request(transaction)
        .input("DepositNo", sql.VarChar, targetDepositNo)
        .input("DepositPaymentNO", sql.VarChar, targetPaymentNo)
        .input("AmtPaid", sql.Decimal(18, 4), amtPaid)
        .input("Remarks", sql.VarChar, Remarks ? Remarks.trim() : "")
        .input("CreateUser", sql.VarChar, user)
        .query(insertDetailQuery);

      // 3. Update main tblDepositSave with cumulative amount
      const updateHeaderQuery = `
        UPDATE tblDepositSave SET
          DepositAmount = DepositAmount + @AmtPaid,
          AmountPaid = @AmtPaid,
          ModifyUser = @ModifyUser,
          ModifyDate = GETDATE()
        WHERE DepositNo = @DepositNo
      `;

      await new sql.Request(transaction)
        .input("DepositNo", sql.VarChar, targetDepositNo)
        .input("AmtPaid", sql.Decimal(18, 4), amtPaid)
        .input("ModifyUser", sql.VarChar, user)
        .query(updateHeaderQuery);

      // 4. Increment NextCustomerDepositNumber counter
      await new sql.Request(transaction).query(
        "UPDATE tblCompany SET NextCustomerDepositNumber = NextCustomerDepositNumber + 1"
      );

      // 5. Save PayMode details for current AmtPaid
      if (amtPaid > 0) {
        const paymodeQuery = `
          INSERT INTO tblPayModeDetail (
            InvoiceNo, InvoiceDate, PayMode, Amount, Commision, SlipNo,
            CreateUser, CreateDate, ModifyUser, ModifyDate, LocationCode, SalesType, IsPaid
          ) VALUES (
            @InvoiceNo, GETDATE(), @PayMode, @Amount, 0, '',
            @CreateUser, GETDATE(), @CreateUser, GETDATE(), @LocationCode, 'DEPOSIT', 1
          )
        `;
        await new sql.Request(transaction)
          .input("InvoiceNo", sql.VarChar, targetPaymentNo)
          .input("PayMode", sql.VarChar, PayMode || "CASH")
          .input("Amount", sql.Decimal(18, 4), amtPaid)
          .input("CreateUser", sql.VarChar, user)
          .input("LocationCode", sql.VarChar, loc)
          .query(paymodeQuery);
      }
    }

    // 5. Upsert Customer details (Address1, Address2, Address3)
    if (CustomerCode) {
      const custCheck = await new sql.Request(transaction)
        .input("CustomerCode", sql.VarChar, CustomerCode.trim())
        .query("SELECT CustomerCode FROM tblCustomer WHERE CustomerCode = @CustomerCode");

      if (custCheck.recordset.length > 0) {
        // Update
        const updateCustQuery = `
          UPDATE tblCustomer SET
            CustomerName = @CustomerName,
            Address1 = @Address1,
            Address2 = @Address2,
            Address3 = @Address3,
            ModifyUser = @ModifyUser,
            ModifyDate = GETDATE()
          WHERE CustomerCode = @CustomerCode
        `;
        await new sql.Request(transaction)
          .input("CustomerCode", sql.VarChar, CustomerCode.trim())
          .input("CustomerName", sql.VarChar, CustomerName ? CustomerName.trim() : "")
          .input("Address1", sql.VarChar, Address1 ? Address1.trim() : "")
          .input("Address2", sql.VarChar, Address2 ? Address2.trim() : "")
          .input("Address3", sql.VarChar, Address3 ? Address3.trim() : "")
          .input("ModifyUser", sql.VarChar, user)
          .query(updateCustQuery);
      } else {
        // Insert
        const insertCustQuery = `
          INSERT INTO tblCustomer (
            CustomerCode, CustomerName, Address1, Address2, Address3,
            CreateUser, CreateDate, ModifyUser, ModifyDate
          ) VALUES (
            @CustomerCode, @CustomerName, @Address1, @Address2, @Address3,
            @CreateUser, GETDATE(), @CreateUser, GETDATE()
          )
        `;
        await new sql.Request(transaction)
          .input("CustomerCode", sql.VarChar, CustomerCode.trim())
          .input("CustomerName", sql.VarChar, CustomerName ? CustomerName.trim() : "")
          .input("Address1", sql.VarChar, Address1 ? Address1.trim() : "")
          .input("Address2", sql.VarChar, Address2 ? Address2.trim() : "")
          .input("Address3", sql.VarChar, Address3 ? Address3.trim() : "")
          .input("CreateUser", sql.VarChar, user)
          .query(insertCustQuery);
      }
    }

    await transaction.commit();
    res.json({
      success: true,
      message: `Deposit details saved successfully`,
      depositNo: targetDepositNo,
      depositPaymentNo: targetPaymentNo
    });

  } catch (err) {
    console.error("Save Deposit Transaction Error:", err);
    await transaction.rollback();
    res.status(500).json({ success: false, message: "Database transaction failed", error: err.message });
  }
});

// 4. DELETE - Delete a deposit record and all its associated details
router.delete("/", async (req, res) => {
  const pool = await getPool(req);
  const transaction = new sql.Transaction(pool);

  try {
    const { depositNo } = req.query;

    if (!depositNo) {
      return res.status(400).json({ success: false, message: "DepositNo is required" });
    }

    await transaction.begin();

    // 1. Delete linked PayModes
    const deletePaymodesQuery = `
      DELETE FROM tblPayModeDetail 
      WHERE InvoiceNo = @DepositNo 
         OR InvoiceNo IN (SELECT TRIM(DepositPaymentNO) FROM tblDepositDetailSave WHERE DepositNo = @DepositNo)
    `;
    await new sql.Request(transaction)
      .input("DepositNo", sql.VarChar, depositNo.trim())
      .query(deletePaymodesQuery);

    // 2. Delete linked details
    await new sql.Request(transaction)
      .input("DepositNo", sql.VarChar, depositNo.trim())
      .query("DELETE FROM tblDepositDetailSave WHERE DepositNo = @DepositNo");

    // 3. Delete main deposit header
    const deleteHeaderResult = await new sql.Request(transaction)
      .input("DepositNo", sql.VarChar, depositNo.trim())
      .query("DELETE FROM tblDepositSave WHERE DepositNo = @DepositNo");

    if (deleteHeaderResult.rowsAffected[0] === 0) {
      await transaction.rollback();
      return res.status(200).json({ success: false, message: "Deposit not found to delete" });
    }

    await transaction.commit();
    res.json({ success: true, message: "Deposit and associated records deleted successfully" });

  } catch (err) {
    console.error("Delete Deposit Error:", err);
    await transaction.rollback();
    res.status(500).json({ success: false, message: "Failed to delete deposit", error: err.message });
  }
});

// 5. GET Print Data matching frmDeposit.vb subPrintInstallment
// Crystal Report: Reports/rptDeposit_HN.rpt
// Selection: {tblDepositSave.DepositNo} = @DepositNo
// PaymodeDetail: sPayModeDetail from tblPaymodeDetail (objSales.GetPayModeDetail)
// Formula Fields: CompAddress1, CompAddress2, CompAddress3, Customer1, Customer2, Customer3, PaymodeDetail
router.get("/print-data", async (req, res) => {
  const depositNo = (req.query.depositNo || req.query.invoiceNo || "").trim();
  if (!depositNo) {
    return res.status(400).json({ success: false, message: "Deposit Number is required for printing." });
  }

  try {
    const pool = await getPool(req);

    // 1. Fetch Company Information
    const compResult = await pool.request().query(`
      SELECT TOP 1 Name, Address1, Address2, City, State, Country, Phone, Fax, CompanyShortCode, CurrencySign
      FROM tblCompany
    `);
    const compRow = compResult.recordset[0] || {};
    const company = {
      name: (compRow.Name || "SMART").trim(),
      address1: (compRow.Address1 || "").trim(),
      address2: (compRow.Address2 || "").trim(),
      city: (compRow.City || "").trim(),
      state: (compRow.State || "").trim(),
      country: (compRow.Country || "").trim(),
      phone: (compRow.Phone || "").trim(),
      fax: (compRow.Fax || "").trim(),
      companyShortCode: (compRow.CompanyShortCode || "HN").trim(),
      currencySign: (compRow.CurrencySign || "SGD").trim(),
    };

    // 2. Fetch Deposit Record (tblDepositSave joined with tblCustomer)
    const depReq = pool.request();
    depReq.input("DepositNo", sql.VarChar(50), depositNo);
    const depResult = await depReq.query(`
      SELECT 
        d.DepositNo, d.DepositDate, d.CustomerCode, d.CustomerName as DepCustomerName,
        d.SalesmanCode, d.TotalAmount, d.DepositAmount, d.AmountPaid, d.Remarks,
        d.DepositUsed, d.InventoryCode, d.InventoryCode1, d.InventoryCode2,
        d.CreateUser, d.CreateDate,
        c.CustomerName as CustMasterName, c.Address1 as CustAddress1, c.Address2 as CustAddress2,
        c.Address3 as CustAddress3, c.Phone1, c.Mobile, c.CustomerType
      FROM tblDepositSave d
      LEFT JOIN tblCustomer c ON RTRIM(LTRIM(c.CustomerCode)) = RTRIM(LTRIM(d.CustomerCode))
      WHERE RTRIM(LTRIM(d.DepositNo)) = @DepositNo
    `);

    if (!depResult.recordset.length) {
      return res.status(404).json({
        success: false,
        message: `Deposit record "${depositNo}" not found in database.`,
      });
    }

    const row = depResult.recordset[0];

    // 3. Fetch Linked Deposit Payment Details (tblDepositDetailSave)
    const detailReq = pool.request();
    detailReq.input("DepositNo", sql.VarChar(50), depositNo);
    const detailResult = await detailReq.query(`
      SELECT DepositNo, DepositPaymentNO, AmtPaid, Remarks, CreateUser, CreateDate, DepositPaymentDate
      FROM tblDepositDetailSave
      WHERE RTRIM(LTRIM(DepositNo)) = @DepositNo
      ORDER BY CreateDate ASC
    `);
    const history = detailResult.recordset.map((h) => ({
      DepositPaymentNo: (h.DepositPaymentNO || "").trim(),
      AmtPaid: parseFloat(h.AmtPaid) || 0,
      Remarks: (h.Remarks || "").trim(),
      CreateUser: (h.CreateUser || "").trim(),
      CreateDate: h.CreateDate,
      DepositPaymentDate: h.DepositPaymentDate,
    }));

    // 4. Fetch PayMode Details using original objSales.GetPayModeDetail logic:
    // SELECT PayMode, Amount FROM tblPaymodeDetail WHERE InvoiceNo = @DepositNo (and linked payment vouchers)
    const pmReq = pool.request();
    pmReq.input("DepositNo", sql.VarChar(50), depositNo);
    const pmResult = await pmReq.query(`
      SELECT PayMode, Amount, SlipNo, InvoiceNo
      FROM tblPayModeDetail
      WHERE RTRIM(LTRIM(InvoiceNo)) = @DepositNo
         OR RTRIM(LTRIM(InvoiceNo)) IN (
              SELECT RTRIM(LTRIM(DepositPaymentNO)) FROM tblDepositDetailSave WHERE RTRIM(LTRIM(DepositNo)) = @DepositNo
         )
      ORDER BY CreateDate ASC
    `);

    const payModes = pmResult.recordset.map((p) => ({
      payMode: (p.PayMode || "").trim(),
      amount: parseFloat(p.Amount) || 0,
      slipNo: (p.SlipNo || "").trim(),
      invoiceNo: (p.InvoiceNo || "").trim(),
    }));

    // Construct original VB.NET sPayModeDetail string:
    // sPayModeDetail = sPayModeDetail & " " & Trim(dsClass.Tables(0).Rows(i)("PayMode") & "") & "             " & Trim(dsClass.Tables(0).Rows(i)("Amount") & "")
    let sPayModeDetail = "";
    if (payModes.length > 0) {
      for (const pm of payModes) {
        sPayModeDetail += (sPayModeDetail ? "   " : "") + `${pm.payMode}             ${pm.amount.toFixed(2)}`;
      }
    } else {
      const depAmt = parseFloat(row.DepositAmount) || 0;
      sPayModeDetail = `CASH             ${depAmt.toFixed(2)}`;
    }

    // 5. Fetch Gold Rates (Rate1, Rate2, Rate3 from tblClass)
    const ratesResult = await pool.request().query(`
      SELECT ClassCode, Rate FROM tblClass WHERE ClassCode IN ('24K', '22K', '835', '999', '916', '18K', 'SIL')
    `);
    const ratesMap = {};
    ratesResult.recordset.forEach((r) => {
      ratesMap[(r.ClassCode || "").trim().toUpperCase()] = r.Rate;
    });

    const rates = {
      rate999: ratesMap["24K"] !== undefined ? ratesMap["24K"] : (ratesMap["999"] || 190),
      rate916: ratesMap["22K"] !== undefined ? ratesMap["22K"] : (ratesMap["916"] || 176),
      rate835: ratesMap["835"] !== undefined ? ratesMap["835"] : (ratesMap["18K"] || 0),
    };

    // 6. Linked Inventory Descriptions if any
    const invCodes = [
      (row.InventoryCode || "").trim(),
      (row.InventoryCode1 || "").trim(),
      (row.InventoryCode2 || "").trim(),
    ].filter(Boolean);

    let inventoryItems = [];
    if (invCodes.length > 0) {
      const invReq = pool.request();
      const codeList = invCodes.map((c) => `'${c.replace(/'/g, "''")}'`).join(",");
      const invRes = await invReq.query(`
        SELECT InventoryCode, Description, Weight, TotalCost
        FROM tblInventory
        WHERE RTRIM(LTRIM(InventoryCode)) IN (${codeList})
      `);
      inventoryItems = invRes.recordset.map((item) => ({
        inventoryCode: (item.InventoryCode || "").trim(),
        description: (item.Description || "").trim(),
        totalWeight: parseFloat(item.Weight) || 0,
        totalCost: parseFloat(item.TotalCost) || 0,
      }));
    }

    const deposit = {
      DepositNo: (row.DepositNo || "").trim(),
      DepositDate: row.DepositDate,
      CustomerCode: (row.CustomerCode || "").trim(),
      CustomerName: (row.CustMasterName || row.DepCustomerName || "").trim(),
      Address1: (row.CustAddress1 || "").trim(),
      Address2: (row.CustAddress2 || "").trim(),
      Address3: (row.CustAddress3 || "").trim(),
      Phone: (row.Mobile || row.Phone1 || "").trim(),
      CustomerType: (row.CustomerType || "").trim(),
      SalesmanCode: (row.SalesmanCode || "").trim(),
      TotalAmount: parseFloat(row.TotalAmount) || 0,
      DepositAmount: parseFloat(row.DepositAmount) || 0,
      AmountPaid: parseFloat(row.AmountPaid) || 0,
      Remarks: (row.Remarks || "").trim(),
      DepositUsed: (row.DepositUsed || "N").trim(),
      InventoryCode: (row.InventoryCode || "").trim(),
      InventoryCode1: (row.InventoryCode1 || "").trim(),
      InventoryCode2: (row.InventoryCode2 || "").trim(),
      CreateUser: (row.CreateUser || "").trim(),
      CreateDate: row.CreateDate,
    };

    res.json({
      success: true,
      data: {
        company,
        deposit,
        history,
        payModes,
        sPayModeDetail,
        rates,
        inventoryItems,
      },
    });

  } catch (err) {
    console.error("Deposit Print Data Error:", err);
    res.status(500).json({
      success: false,
      message: "Failed to retrieve deposit print data.",
      error: err.message,
    });
  }
});

module.exports = router;
