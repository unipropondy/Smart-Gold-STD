const express = require("express");
const router = express.Router();
const sql = require("mssql");

// GET /api/payout/nextCode
router.get("/nextCode", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query("SELECT TOP 1 LocationCode, NextPayOutNo FROM tblCompany");
    
    let payoutNo = 1;
    let loc = "HQ";
    if (result.recordset.length > 0) {
      payoutNo = parseInt(result.recordset[0].NextPayOutNo) || 1;
      loc = (result.recordset[0].LocationCode || "HQ").trim();
    }
    const nextCode = `PO-${loc}${String(payoutNo).padStart(8, "0")}`;
    res.json({ success: true, data: nextCode });
  } catch (err) {
    console.error("Get Next Payout Code Error:", err);
    res.status(500).json({ success: false, message: "Database error", error: err.message });
  }
});

// POST /api/payout/save
router.post("/save", async (req, res) => {
  const pool = req.pool;
  const transaction = new sql.Transaction(pool);

  try {
    let { Code, sDescription, PayOutDescription, Amount, Remarks, userId } = req.body;

    Code = (Code || "").trim();
    sDescription = (sDescription || "").trim();
    PayOutDescription = (PayOutDescription || "").trim();
    Remarks = (Remarks || "").trim();
    const payAmount = parseFloat(Amount) || 0;
    const sysUser = (userId || "sys").substring(0, 10);

    if (!Code) {
      return res.status(400).json({ success: false, message: "Payout Code is required." });
    }
    if (!PayOutDescription) {
      return res.status(400).json({ success: false, message: "Payout Description is required." });
    }
    if (payAmount <= 0) {
      return res.status(400).json({ success: false, message: "Amount must be greater than zero." });
    }

    await transaction.begin();

    // 1. Fetch Company Config and current sequence
    const compResult = await new sql.Request(transaction).query(`
      SELECT TOP 1 LocationCode, NextPayOutNo FROM tblCompany
    `);
    const comp = compResult.recordset[0] || {};
    const loc = (comp.LocationCode || "HQ").trim();
    const nextSeq = parseInt(comp.NextPayOutNo) || 1;
    
    const payOutNo = `PO-${loc}${String(nextSeq).padStart(8, "0")}`;

    // 2. Insert Payout details
    await new sql.Request(transaction)
      .input("PayOutNo", sql.VarChar, payOutNo)
      .input("Code", sql.Char, Code.substring(0, 10))
      .input("Description", sql.VarChar, sDescription.substring(0, 500))
      .input("PayOutDescription", sql.VarChar, PayOutDescription.substring(0, 500))
      .input("Amount", sql.Numeric(18, 4), payAmount)
      .input("Remarks", sql.VarChar, Remarks.substring(0, 50))
      .input("CreateUser", sql.VarChar, sysUser)
      .query(`
        INSERT INTO tblPayOutDetail (
          PayOutNo, PayOutDate, Code, Description, PayOutDescription, Amount, Remarks,
          CreateUser, CreateDate, ModifyUser, ModifyDate
        ) VALUES (
          @PayOutNo, GETDATE(), @Code, @Description, @PayOutDescription, @Amount, @Remarks,
          @CreateUser, GETDATE(), @CreateUser, GETDATE()
        )
      `);

    // 3. Insert Paymode details
    await new sql.Request(transaction)
      .input("InvoiceNo", sql.Char, payOutNo.substring(0, 20))
      .input("PayMode", sql.VarChar, "CASH")
      .input("Amount", sql.Numeric(18, 4), -payAmount)
      .input("CreateUser", sql.Char, sysUser)
      .input("LocationCode", sql.VarChar, loc)
      .query(`
        INSERT INTO tblPayModeDetail (
          InvoiceNo, InvoiceDate, PayMode, Amount, Commision, SlipNo,
          CreateUser, CreateDate, ModifyUser, ModifyDate, LocationCode, SalesType, IsPaid
        ) VALUES (
          @InvoiceNo, GETDATE(), @PayMode, @Amount, 0, '',
          @CreateUser, GETDATE(), @CreateUser, GETDATE(), @LocationCode, 'PAYOUT', 1
        )
      `);

    // 4. Increment NextPayOutNo sequence
    await new sql.Request(transaction).query(`
      UPDATE tblCompany SET NextPayOutNo = NextPayOutNo + 1
    `);

    await transaction.commit();
    res.json({ success: true, message: "PayOut detail saved successfully.", data: payOutNo });

  } catch (err) {
    console.error("Save Payout Transaction Error:", err);
    await transaction.rollback();
    res.status(500).json({ success: false, message: "Database transaction failed", error: err.message });
  }
});

module.exports = router;
