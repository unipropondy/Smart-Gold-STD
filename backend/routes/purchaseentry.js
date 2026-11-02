/**
 * Developer: durairaj
 */

const express = require("express");
const router = express.Router();
const { sql, poolPromise } = require("../db");

// 1. GET jobsheets for lookup (LOV)
// Matches frmHelpSearch("tblJobSheetHeader", "L", 4, "JobSheetNo", "FixStatus='N'", "JobSheetNo", ...)
router.get("/jobsheets", async (req, res) => {
  try {
    const pool = req.pool || (await poolPromise);
    const result = await pool.request().query(`
      SELECT 
        RTRIM(JobSheetNo) AS JobSheetNo, 
        ISNULL(RTRIM(PurchaseBillNo), '') AS PurchaseBillNo, 
        ISNULL(RTRIM(SupplierCode), '') AS SupplierCode, 
        ISNULL(CostPerGram, 0) AS CostPerGram 
      FROM tblJobSheetHeader 
      WHERE ISNULL(FixStatus, 'N') = 'N'
      ORDER BY JobSheetNo DESC
    `);
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch JobSheets LOV Error:", err);
    res.status(500).json({ success: false, message: "Database Error", error: err.message });
  }
});

// 2. GET specific jobsheet details
// Matches objClsJobsheet.GetJobSheetDetail and GetPurchaseDetail in frmPurchaseEntry.vb
router.get("/jobsheet/:id", async (req, res) => {
  try {
    const pool = req.pool || (await poolPromise);
    const { id } = req.params;

    if (!id || !id.trim()) {
      return res.status(400).json({ success: false, message: "JobSheet No is required." });
    }

    // Inspect columns of tblJobSheetHeader dynamically using sys.columns
    let existingCols = new Set();
    try {
      const colRes = await pool.request().query(`
        SELECT name FROM sys.columns WHERE object_id = OBJECT_ID('tblJobSheetHeader')
      `);
      colRes.recordset.forEach(c => {
        if (c.name) existingCols.add(c.name.toLowerCase());
      });
    } catch (e) {
      console.warn("sys.columns query error:", e.message);
    }

    // Fallback if sys.columns didn't return
    if (existingCols.size === 0) {
      try {
        const colRes2 = await pool.request().query(`
          SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tblJobSheetHeader'
        `);
        colRes2.recordset.forEach(c => {
          if (c.COLUMN_NAME) existingCols.add(c.COLUMN_NAME.toLowerCase());
        });
      } catch (e) {}
    }

    const hasCol = (name) => existingCols.has(name.toLowerCase());

    const selectCols = [
      "RTRIM(JobSheetNo) AS JobSheetNo",
      "ISNULL(RTRIM(PurchaseBillNo), '') AS PurchaseBillNo",
      "PurchaseBillDate",
      "ISNULL(RTRIM(SupplierCode), '') AS SupplierCode"
    ];

    if (hasCol('CostPerGram')) {
      selectCols.push("ISNULL(CostPerGram, 0) AS CostPerGram");
    } else {
      selectCols.push("0 AS CostPerGram");
    }

    if (hasCol('TotalBeforeGST')) {
      selectCols.push("ISNULL(TotalBeforeGST, 0) AS TotalBeforeGST");
    } else if (hasCol('TotalCost')) {
      selectCols.push("ISNULL(TotalCost, 0) AS TotalBeforeGST");
    } else {
      selectCols.push("0 AS TotalBeforeGST");
    }

    if (hasCol('GST')) {
      selectCols.push("ISNULL(GST, 0) AS GST");
    } else {
      selectCols.push("0 AS GST");
    }

    if (hasCol('TotalSales')) {
      selectCols.push("ISNULL(TotalSales, 0) AS TotalSales");
    } else if (hasCol('TotalCost') && hasCol('GST')) {
      selectCols.push("(ISNULL(TotalCost, 0) + ISNULL(GST, 0)) AS TotalSales");
    } else {
      selectCols.push("0 AS TotalSales");
    }

    const query = `
      SELECT TOP 1
        ${selectCols.join(', ')}
      FROM tblJobSheetHeader
      WHERE RTRIM(JobSheetNo) = @JobSheetNo
    `;

    const result = await pool.request()
      .input('JobSheetNo', sql.VarChar, id.trim())
      .query(query);

    if (result.recordset.length > 0) {
      res.json({ success: true, data: result.recordset[0] });
    } else {
      res.json({ success: false, message: "Jobsheet No not found." });
    }
  } catch (err) {
    console.error("Fetch JobSheet Details Error:", err);
    res.status(500).json({ success: false, message: err.message || "Database Error", error: err.message });
  }
});

// 3. PUT update purchase details
// Matches objClsJobsheet.UpdatePurchaseDetails(txtJobSheetNo, txtInvoiceNo, txtSupplier, Val(txtTotalGSTSales), Val(txtGST), Val(txtTotalSales))
router.put("/jobsheet/:id", async (req, res) => {
  try {
    const pool = req.pool || (await poolPromise);
    const { id } = req.params;
    const { InvoiceNo, Supplier, TotalGSTSales, GST, TotalSales, UserID } = req.body;
    const sysUser = UserID || "sys";

    if (!id || !id.trim()) {
      return res.status(400).json({ success: false, message: "JobsheeNo should not be blank." });
    }
    if (!InvoiceNo || !InvoiceNo.trim()) {
      return res.status(400).json({ success: false, message: "Invoice No should not be blank." });
    }
    if (!Supplier || !Supplier.trim()) {
      return res.status(400).json({ success: false, message: "Supplier Code should not be blank." });
    }

    let existingCols = new Set();
    try {
      const colRes = await pool.request().query(`
        SELECT name FROM sys.columns WHERE object_id = OBJECT_ID('tblJobSheetHeader')
      `);
      colRes.recordset.forEach(c => {
        if (c.name) existingCols.add(c.name.toLowerCase());
      });
    } catch (e) {}

    const hasCol = (name) => existingCols.has(name.toLowerCase());

    const setClauses = [];
    const request = pool.request().input('JobSheetNo', sql.VarChar, id.trim());

    setClauses.push("PurchaseBillNo = @InvoiceNo");
    request.input('InvoiceNo', sql.VarChar, InvoiceNo.trim());

    setClauses.push("SupplierCode = @SupplierCode");
    request.input('SupplierCode', sql.VarChar, Supplier.trim());

    if (hasCol('TotalBeforeGST')) {
      setClauses.push("TotalBeforeGST = @TotalBeforeGST");
      request.input('TotalBeforeGST', sql.Numeric(18, 2), parseFloat(TotalGSTSales) || 0);
    } else if (hasCol('TotalCost')) {
      setClauses.push("TotalCost = @TotalBeforeGST");
      request.input('TotalBeforeGST', sql.Numeric(18, 2), parseFloat(TotalGSTSales) || 0);
    }

    if (hasCol('GST')) {
      setClauses.push("GST = @GST");
      request.input('GST', sql.Numeric(18, 2), parseFloat(GST) || 0);
    }

    if (hasCol('TotalSales')) {
      setClauses.push("TotalSales = @TotalSales");
      request.input('TotalSales', sql.Numeric(18, 2), parseFloat(TotalSales) || 0);
    }

    if (hasCol('ModifyUser')) {
      setClauses.push("ModifyUser = @ModifyUser");
      request.input('ModifyUser', sql.VarChar, sysUser);
    }
    if (hasCol('ModifyDate')) {
      setClauses.push("ModifyDate = GETDATE()");
    }

    const updateQuery = `
      UPDATE tblJobSheetHeader 
      SET ${setClauses.join(', ')}
      WHERE RTRIM(JobSheetNo) = @JobSheetNo
    `;

    const result = await request.query(updateQuery);
    if (result.rowsAffected[0] === 0) {
      return res.status(404).json({ success: false, message: "Jobsheet No not found." });
    }

    res.json({ success: true, message: "Purchase Details updated successfully!" });
  } catch (err) {
    console.error("Update Purchase Details Error:", err);
    res.status(500).json({ success: false, message: err.message || "Error in Save.", error: err.message });
  }
});

// 4. POST /jobsheet
router.post("/jobsheet", async (req, res) => {
  try {
    const pool = req.pool || (await poolPromise);
    const { JobSheetNo, InvoiceNo, Supplier, TotalGSTSales, GST, TotalSales, UserID } = req.body;
    const sysUser = UserID || "sys";

    if (!JobSheetNo || !JobSheetNo.trim()) {
      return res.status(400).json({ success: false, message: "JobsheeNo should not be blank." });
    }
    if (!InvoiceNo || !InvoiceNo.trim()) {
      return res.status(400).json({ success: false, message: "Invoice No should not be blank." });
    }
    if (!Supplier || !Supplier.trim()) {
      return res.status(400).json({ success: false, message: "Supplier Code should not be blank." });
    }

    let existingCols = new Set();
    try {
      const colRes = await pool.request().query(`
        SELECT name FROM sys.columns WHERE object_id = OBJECT_ID('tblJobSheetHeader')
      `);
      colRes.recordset.forEach(c => {
        if (c.name) existingCols.add(c.name.toLowerCase());
      });
    } catch (e) {}

    const hasCol = (name) => existingCols.has(name.toLowerCase());

    const setClauses = [];
    const request = pool.request().input('JobSheetNo', sql.VarChar, JobSheetNo.trim());

    setClauses.push("PurchaseBillNo = @InvoiceNo");
    request.input('InvoiceNo', sql.VarChar, InvoiceNo.trim());

    setClauses.push("SupplierCode = @SupplierCode");
    request.input('SupplierCode', sql.VarChar, Supplier.trim());

    if (hasCol('TotalBeforeGST')) {
      setClauses.push("TotalBeforeGST = @TotalBeforeGST");
      request.input('TotalBeforeGST', sql.Numeric(18, 2), parseFloat(TotalGSTSales) || 0);
    } else if (hasCol('TotalCost')) {
      setClauses.push("TotalCost = @TotalBeforeGST");
      request.input('TotalBeforeGST', sql.Numeric(18, 2), parseFloat(TotalGSTSales) || 0);
    }

    if (hasCol('GST')) {
      setClauses.push("GST = @GST");
      request.input('GST', sql.Numeric(18, 2), parseFloat(GST) || 0);
    }

    if (hasCol('TotalSales')) {
      setClauses.push("TotalSales = @TotalSales");
      request.input('TotalSales', sql.Numeric(18, 2), parseFloat(TotalSales) || 0);
    }

    if (hasCol('ModifyUser')) {
      setClauses.push("ModifyUser = @ModifyUser");
      request.input('ModifyUser', sql.VarChar, sysUser);
    }
    if (hasCol('ModifyDate')) {
      setClauses.push("ModifyDate = GETDATE()");
    }

    const updateQuery = `
      UPDATE tblJobSheetHeader 
      SET ${setClauses.join(', ')}
      WHERE RTRIM(JobSheetNo) = @JobSheetNo
    `;

    const result = await request.query(updateQuery);
    if (result.rowsAffected[0] === 0) {
      return res.status(404).json({ success: false, message: "Jobsheet No not found." });
    }

    res.json({ success: true, message: "Purchase Details updated successfully!" });
  } catch (err) {
    console.error("Save Purchase Details Error:", err);
    res.status(500).json({ success: false, message: err.message || "Error in Save.", error: err.message });
  }
});

module.exports = router;
