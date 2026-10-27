const express = require("express");
const router = express.Router();
const { sql } = require("../db");

// GET jobsheets for lookup
router.get("/jobsheets", async (req, res) => {
  try {
    const pool = req.pool;
    // The VB code filters by FixStatus='N'
    const result = await pool.request().query(`
      SELECT JobSheetNo, PurchaseBillNo, SupplierCode, CostPerGram 
      FROM tblJobSheetHeader 
      WHERE ISNULL(FixStatus, 'N') = 'N'
    `);
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch JobSheets Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// GET specific jobsheet details
router.get("/jobsheet/:id", async (req, res) => {
  try {
    const pool = req.pool;
    const { id } = req.params;

    const result = await pool.request()
      .input('JobSheetNo', sql.VarChar, id)
      .query(`
        SELECT * 
        FROM tblJobSheetHeader
        WHERE JobSheetNo = @JobSheetNo
      `);

    if (result.recordset.length > 0) {
      res.json({ success: true, data: result.recordset[0] });
    } else {
      res.json({ success: false, message: "JobSheet No not found." });
    }
  } catch (err) {
    console.error("Fetch JobSheet Details Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// PUT update purchase details
router.put("/jobsheet/:id", async (req, res) => {
  try {
    const pool = req.pool;
    const { id } = req.params;
    const { InvoiceNo, Supplier, TotalGSTSales, GST, TotalSales } = req.body;

    const updateQuery = `
      UPDATE tblJobSheetHeader SET 
        PurchaseBillNo = @InvoiceNo,
        SupplierCode = @Supplier,
        ModifyUser = 'sys',
        ModifyDate = GETDATE()
      WHERE JobSheetNo = @JobSheetNo
    `;

    // Attempt to also update totals if columns exist. If they don't, we just catch the error and do the simpler update.
    try {
      const complexUpdate = `
        UPDATE tblJobSheetHeader SET 
          PurchaseBillNo = @InvoiceNo,
          SupplierCode = @Supplier,
          TotalBeforeGST = @TotalGSTSales,
          GST = @GST,
          TotalSales = @TotalSales,
          ModifyUser = 'sys',
          ModifyDate = GETDATE()
        WHERE JobSheetNo = @JobSheetNo
      `;
      const req = pool.request()
        .input('JobSheetNo', sql.VarChar, id)
        .input('InvoiceNo', sql.VarChar, InvoiceNo || '')
        .input('Supplier', sql.VarChar, Supplier || '')
        .input('TotalGSTSales', sql.Numeric(18, 2), TotalGSTSales || 0)
        .input('GST', sql.Numeric(18, 2), GST || 0)
        .input('TotalSales', sql.Numeric(18, 2), TotalSales || 0);
      
      const result = await req.query(complexUpdate);
      if (result.rowsAffected[0] === 0) {
        return res.status(404).json({ success: false, message: "JobSheet not found." });
      }
      return res.json({ success: true, message: "Purchase Details updated successfully!" });
    } catch (err) {
      // Fallback to simpler update if TotalBeforeGST columns don't exist
      const req2 = pool.request()
        .input('JobSheetNo', sql.VarChar, id)
        .input('InvoiceNo', sql.VarChar, InvoiceNo || '')
        .input('Supplier', sql.VarChar, Supplier || '');
      
      const result2 = await req2.query(updateQuery);
      if (result2.rowsAffected[0] === 0) {
        return res.status(404).json({ success: false, message: "JobSheet not found." });
      }
      return res.json({ success: true, message: "Purchase Details updated (totals ignored)!" });
    }

  } catch (err) {
    console.error("Update Purchase Details Error:", err);
    res.status(500).json({ success: false, message: "Failed to update purchase details", error: err.message });
  }
});

// POST insert new purchase details/jobsheet
router.post("/jobsheet", async (req, res) => {
  try {
    const pool = req.pool;
    const { JobSheetNo, InvoiceNo, Supplier, TotalGSTSales, GST, TotalSales } = req.body;

    if (!JobSheetNo) {
      return res.status(400).json({ success: false, message: "JobSheet No is required." });
    }

    // Check if exists
    const checkResult = await pool.request()
      .input('JobSheetNo', sql.VarChar, JobSheetNo)
      .query("SELECT JobSheetNo FROM tblJobSheetHeader WHERE JobSheetNo = @JobSheetNo");

    if (checkResult.recordset.length > 0) {
      return res.status(400).json({ success: false, message: "JobSheet No already exists. Use Save/Update instead." });
    }

    const insertQuery = `
      INSERT INTO tblJobSheetHeader (
        JobSheetNo, PurchaseBillNo, SupplierCode, TotalBeforeGST, GST, TotalSales,
        CreateUser, CreateDate, ModifyUser, ModifyDate
      ) VALUES (
        @JobSheetNo, @InvoiceNo, @Supplier, @TotalGSTSales, @GST, @TotalSales,
        'sys', GETDATE(), 'sys', GETDATE()
      )
    `;

    await pool.request()
      .input('JobSheetNo', sql.VarChar, JobSheetNo)
      .input('InvoiceNo', sql.VarChar, InvoiceNo || '')
      .input('Supplier', sql.VarChar, Supplier || '')
      .input('TotalGSTSales', sql.Numeric(18, 2), TotalGSTSales || 0)
      .input('GST', sql.Numeric(18, 2), GST || 0)
      .input('TotalSales', sql.Numeric(18, 2), TotalSales || 0)
      .query(insertQuery);

    res.json({ success: true, message: "Purchase Entry inserted successfully!" });
  } catch (err) {
    console.error("Insert Purchase Details Error:", err);
    res.status(500).json({ success: false, message: "Failed to insert purchase entry", error: err.message });
  }
});

// DELETE purchase details (clears the invoice info from jobsheet)
router.delete("/jobsheet/:id", async (req, res) => {
  try {
    const pool = req.pool;
    const { id } = req.params;

    const clearQuery = `
      UPDATE tblJobSheetHeader SET 
        PurchaseBillNo = NULL,
        PurchaseBillDate = NULL,
        TotalBeforeGST = 0,
        GST = 0,
        TotalSales = 0,
        ModifyUser = 'sys',
        ModifyDate = GETDATE()
      WHERE JobSheetNo = @JobSheetNo
    `;

    const result = await pool.request()
      .input('JobSheetNo', sql.VarChar, id)
      .query(clearQuery);

    if (result.rowsAffected[0] === 0) {
      return res.status(404).json({ success: false, message: "JobSheet not found." });
    }

    res.json({ success: true, message: "Purchase Details cleared successfully!" });
  } catch (err) {
    console.error("Delete Purchase Details Error:", err);
    res.status(500).json({ success: false, message: "Failed to clear purchase details", error: err.message });
  }
});

module.exports = router;
