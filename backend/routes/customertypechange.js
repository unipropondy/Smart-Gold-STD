const express = require("express");
const router = express.Router();
const { sql } = require("../db");

// GET /fetch - Retrieve customers based on criteria
router.get("/fetch", async (req, res) => {
  try {
    const pool = req.pool;
    const { customerType, customerCode, fromPoints, toPoints } = req.query;

    if (!customerType) {
      return res.status(400).json({ success: false, message: "Customer Type is required." });
    }

    let query = `
      SELECT CustomerCode, CustomerName, TotalRewardPoints 
      FROM tblCustomer 
      WHERE CustomerType = @CustomerType
    `;

    const request = pool.request();
    request.input('CustomerType', sql.VarChar, customerType);

    if (customerCode && customerCode.trim() !== '') {
      query += ` AND CustomerCode = @CustomerCode`;
      request.input('CustomerCode', sql.VarChar, customerCode.trim());
    }

    if (fromPoints && !isNaN(fromPoints)) {
      query += ` AND TotalRewardPoints >= @FromPoints`;
      request.input('FromPoints', sql.Decimal(18,2), parseFloat(fromPoints));
    }

    if (toPoints && !isNaN(toPoints)) {
      query += ` AND TotalRewardPoints <= @ToPoints`;
      request.input('ToPoints', sql.Decimal(18,2), parseFloat(toPoints));
    }

    const result = await request.query(query);

    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Customer Type Change Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// POST /save - Update customer types
router.post("/save", async (req, res) => {
  try {
    const pool = req.pool;
    const { customers, oldCustomerType, newCustomerType, fPoints, tPoints, userId } = req.body;

    if (!customers || !Array.isArray(customers) || customers.length === 0) {
      return res.status(400).json({ success: false, message: "No customers selected." });
    }

    if (!newCustomerType) {
      return res.status(400).json({ success: false, message: "New Customer Type is required." });
    }

    // We will run updates in a transaction if possible, or just sequential updates
    const transaction = new sql.Transaction(pool);
    await transaction.begin();

    try {
      for (const cust of customers) {
        const { CustomerCode, TotalPoints } = cust;

        // Basic update - typically you would also adjust points based on fPoints and tPoints
        // Example: New Points = (TotalPoints / fPoints) * tPoints
        // But for safety without knowing exact SQL, we just update the type.
        // If a stored proc is needed, it would be: await request.execute('CustomerTypeChange')
        
        let updateQuery = `
          UPDATE tblCustomer
          SET CustomerType = @NewCustomerType
          WHERE CustomerCode = @CustomerCode
        `;

        const request = new sql.Request(transaction);
        request.input('NewCustomerType', sql.VarChar, newCustomerType);
        request.input('CustomerCode', sql.VarChar, CustomerCode);

        await request.query(updateQuery);
      }

      await transaction.commit();
      res.json({ success: true, message: "Customer types updated successfully." });
    } catch (txErr) {
      await transaction.rollback();
      throw txErr;
    }

  } catch (err) {
    console.error("Save Customer Type Change Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

module.exports = router;
