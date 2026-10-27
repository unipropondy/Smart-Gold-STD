const express = require("express");
const router = express.Router();
const { sql } = require("../db");

// Helper to pad voucher numbers
function padZero(num, size) {
  let s = num + "";
  while (s.length < size) s = "0" + s;
  return s;
}

// GET all deposits
router.get("/", async (req, res) => {
  try {
    const pool = req.pool;
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
    const pool = req.pool;
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
    const pool = req.pool;
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
  const pool = req.pool;
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
  const pool = req.pool;
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

module.exports = router;
