const express = require("express");
const router = express.Router();
const { sql } = require("../db");

// Helper to pad voucher numbers
function padZero(num, size) {
  let s = num + "";
  while (s.length < size) s = "0" + s;
  return s;
}

// 1. GET next repair entry number
router.get("/next-no", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query(
      "SELECT TOP 1 LocationCode, NextRepairNo FROM tblCompany"
    );

    if (result.recordset.length > 0) {
      const loc = result.recordset[0].LocationCode || "HQ";
      const nextNo = result.recordset[0].NextRepairNo || 1;
      const formattedNo = `RP-${loc}${padZero(nextNo, 8)}`;
      res.json({ success: true, repairNo: formattedNo });
    } else {
      res.json({ success: true, repairNo: `RP-HQ00000001` });
    }
  } catch (err) {
    console.error("Fetch Next Repair No Error:", err);
    res.status(500).json({ success: false, message: "Failed to fetch next number", error: err.message });
  }
});

// 1.5. GET existing invoices for LOV
router.get("/invoices", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query(
      "SELECT RepairNo, RepairDate, CustomerCode FROM tblInventoryRepairHeader ORDER BY RepairDate DESC"
    );
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Invoices Error:", err);
    res.status(500).json({ success: false, message: "Failed to fetch invoices", error: err.message });
  }
});

// 2. GET Customers for LOV
router.get("/customers", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query(
      "SELECT CustomerCode, CustomerName, Address1, Address2, Address3, CustomerType FROM tblCustomer"
    );
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Customers Error:", err);
    res.status(500).json({ success: false, message: "Failed to fetch customers", error: err.message });
  }
});

// 3. GET Suppliers for LOV
router.get("/suppliers", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query(
      "SELECT SupplierCode, SupplierName FROM tblSupplier"
    );
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Suppliers Error:", err);
    res.status(500).json({ success: false, message: "Failed to fetch suppliers", error: err.message });
  }
});

// 4. Validate Salesman Password
router.post("/validate-salesman", async (req, res) => {
  try {
    const { password } = req.body;
    if (!password) {
      return res.status(400).json({ success: false, message: "Password is required" });
    }
    const pool = req.pool;
    const result = await pool.request()
      .input("Password", sql.VarChar, password)
      .query("SELECT SalesmanCode FROM tblSalesMan WHERE Password = @Password");

    if (result.recordset.length > 0) {
      res.json({ success: true, salesmanCode: result.recordset[0].SalesmanCode });
    } else {
      res.status(401).json({ success: false, message: "Invalid SalesMan Password" });
    }
  } catch (err) {
    console.error("Validate Salesman Error:", err);
    res.status(500).json({ success: false, message: "Error validating salesman", error: err.message });
  }
});

// 5. GET existing repair entry
router.get("/:repairNo", async (req, res) => {
  try {
    const { repairNo } = req.params;
    const pool = req.pool;

    const headerRes = await pool.request()
      .input("RepairNo", sql.VarChar, repairNo)
      .query("SELECT * FROM tblInventoryRepairHeader WHERE RepairNo = @RepairNo");

    if (headerRes.recordset.length === 0) {
      return res.status(404).json({ success: false, message: "Repair entry not found." });
    }

    const detailRes = await pool.request()
      .input("RepairNo", sql.VarChar, repairNo)
      .query("SELECT * FROM tblInventoryRepairDetail WHERE RepairNo = @RepairNo");

    res.json({ 
      success: true, 
      header: headerRes.recordset[0],
      details: detailRes.recordset
    });
  } catch (err) {
    console.error("Fetch Repair Error:", err);
    res.status(500).json({ success: false, message: "Failed to fetch repair details", error: err.message });
  }
});

// 6. POST to Save Repair Entry
router.post("/save", async (req, res) => {
  let transaction;
  try {
    const pool = req.pool;
    transaction = new sql.Transaction(pool);
    await transaction.begin();

    const {
      repairNo,
      repairDate,
      issueDate,
      dateOfDelivery,
      customerCode,
      supplierCode,
      remarks,
      advancePaid,
      totalCostEstimated,
      salesManCode,
      tradeInAmount,
      tradeInType,
      items // Array of { description, weight, cost }
    } = req.body;

    const reqHeader = new sql.Request(transaction);
    await reqHeader
      .input("RepairNo", sql.VarChar, repairNo)
      .input("RepairDate", sql.DateTime, repairDate ? new Date(repairDate) : new Date())
      .input("IssueDate", sql.DateTime, issueDate ? new Date(issueDate) : null)
      .input("DateOfDelivery", sql.DateTime, dateOfDelivery ? new Date(dateOfDelivery) : null)
      .input("CustomerCode", sql.VarChar, customerCode)
      .input("SupplierCode", sql.VarChar, supplierCode)
      .input("TradeInAmount", sql.Decimal(18, 2), tradeInAmount || 0)
      .input("TradeInType", sql.VarChar, tradeInType || 'C')
      .input("RateFixed", sql.VarChar, 'N')
      .input("TotalCostEstimated", sql.Decimal(18, 2), totalCostEstimated || 0)
      .input("AdvancePaid", sql.Decimal(18, 2), advancePaid || 0)
      .input("Status", sql.VarChar, 'R') // 'R' for Repair
      .input("Remarks", sql.VarChar, remarks || "")
      .input("SalesManCode", sql.VarChar, salesManCode || "")
      .query(`
        INSERT INTO tblInventoryRepairHeader 
        (RepairNo, RepairDate, IssueDate, DateOfDelivery, CustomerCode, SupplierCode, TradeInAmount, TradeInType, RateFixed, TotalCostEstimated, AdvancePaid, Status, Remarks, SalesManCode)
        VALUES 
        (@RepairNo, @RepairDate, @IssueDate, @DateOfDelivery, @CustomerCode, @SupplierCode, @TradeInAmount, @TradeInType, @RateFixed, @TotalCostEstimated, @AdvancePaid, @Status, @Remarks, @SalesManCode)
      `);

    // Delete existing details (just in case this was an update, though normally it's a new insert in VB code unless handled differently. The VB code executes a DELETE before insert)
    const reqDelDetails = new sql.Request(transaction);
    await reqDelDetails
      .input("RepairNo", sql.VarChar, repairNo)
      .query("DELETE FROM tblInventoryRepairDetail WHERE RepairNo = @RepairNo");

    // Insert Details
    if (items && items.length > 0) {
      for (const item of items) {
        const reqDetail = new sql.Request(transaction);
        await reqDetail
          .input("RepairNo", sql.VarChar, repairNo)
          .input("Description", sql.VarChar, item.description || "")
          .input("WeightEstimated", sql.Decimal(18, 2), item.weight || 0)
          .input("EstimatedCost", sql.Decimal(18, 2), item.cost || 0)
          .input("Status", sql.VarChar, 'R')
          .query(`
            INSERT INTO tblInventoryRepairDetail
            (RepairNo, Description, WeightEstimated, EstimatedCost, Status)
            VALUES
            (@RepairNo, @Description, @WeightEstimated, @EstimatedCost, @Status)
          `);
      }
    }

    // Increment NextRepairNo in tblCompany
    const reqComp = new sql.Request(transaction);
    await reqComp.query("UPDATE tblCompany SET NextRepairNo = NextRepairNo + 1");

    await transaction.commit();
    res.json({ success: true, message: "Repair Entry Saved Successfully", repairNo });
  } catch (err) {
    console.error("Save Repair Error:", err);
    if (transaction) {
      try {
        await transaction.rollback();
      } catch (rollbackErr) {
        console.error("Rollback Error:", rollbackErr);
      }
    }
    res.status(500).json({ success: false, message: "Failed to save repair entry", error: err.message });
  }
});

// 7. DELETE (Cancel) Repair Entry
router.delete("/:repairNo", async (req, res) => {
  let transaction;
  try {
    const { repairNo } = req.params;
    const pool = req.pool;
    transaction = new sql.Transaction(pool);
    await transaction.begin();

    // 1. Move Header to Cancel table
    const reqCopyHeader = new sql.Request(transaction);
    await reqCopyHeader
      .input("RepairNo", sql.VarChar, repairNo)
      .query(`
        INSERT INTO tblInventoryRepairHeaderCancel 
        SELECT * FROM tblInventoryRepairHeader WHERE RepairNo = @RepairNo
      `);

    // 2. Delete Header from original table
    const reqDelHeader = new sql.Request(transaction);
    await reqDelHeader
      .input("RepairNo", sql.VarChar, repairNo)
      .query("DELETE FROM tblInventoryRepairHeader WHERE RepairNo = @RepairNo");

    // 3. Move Details to Cancel table
    const reqCopyDetail = new sql.Request(transaction);
    await reqCopyDetail
      .input("RepairNo", sql.VarChar, repairNo)
      .query(`
        INSERT INTO tblInventoryRepairDetailCancel 
        SELECT * FROM tblInventoryRepairDetail WHERE RepairNo = @RepairNo
      `);

    // 4. Delete Details from original table
    const reqDelDetail = new sql.Request(transaction);
    await reqDelDetail
      .input("RepairNo", sql.VarChar, repairNo)
      .query("DELETE FROM tblInventoryRepairDetail WHERE RepairNo = @RepairNo");

    await transaction.commit();
    res.json({ success: true, message: "Repair Entry Cancelled Successfully" });
  } catch (err) {
    console.error("Cancel Repair Error:", err);
    if (transaction) {
      try {
        await transaction.rollback();
      } catch (rollbackErr) {
        console.error("Rollback Error:", rollbackErr);
      }
    }
    res.status(500).json({ success: false, message: "Failed to cancel repair entry", error: err.message });
  }
});

module.exports = router;
