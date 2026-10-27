const express = require("express");
const router = express.Router();
const { sql } = require("../db");

// Helper to safely parse dates for SQL
const parseDateForSQL = (dateStr) => {
  if (!dateStr || typeof dateStr !== 'string') return null;
  const trimmed = dateStr.trim();
  if (!trimmed) return null;

  // If format is DD-MM-YYYY or DD/MM/YYYY
  const dmyMatch = trimmed.match(/^(\d{1,2})[-/](\d{1,2})[-/](\d{4})$/);
  if (dmyMatch) {
    const d = parseInt(dmyMatch[1], 10);
    const m = parseInt(dmyMatch[2], 10) - 1;
    const y = parseInt(dmyMatch[3], 10);
    const dt = new Date(y, m, d);
    return isNaN(dt.getTime()) ? null : dt;
  }

  // If format is YYYY-MM-DD or YYYY/MM/DD
  const ymdMatch = trimmed.match(/^(\d{4})[-/](\d{1,2})[-/](\d{1,2})$/);
  if (ymdMatch) {
    const y = parseInt(ymdMatch[1], 10);
    const m = parseInt(ymdMatch[2], 10) - 1;
    const d = parseInt(ymdMatch[3], 10);
    const dt = new Date(y, m, d);
    return isNaN(dt.getTime()) ? null : dt;
  }

  const dt = new Date(trimmed);
  return isNaN(dt.getTime()) ? null : dt;
};


// GET Next Sales Order No
router.get("/nextNo", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query("SELECT NextSalesOrderNo FROM tblCompany");
    let nextNo = 1;
    if (result.recordset.length > 0 && result.recordset[0].NextSalesOrderNo) {
      nextNo = parseInt(result.recordset[0].NextSalesOrderNo, 10);
    }
    const formattedNo = `SO${String(nextNo).padStart(6, '0')}`;
    res.json({ success: true, nextNo: formattedNo });
  } catch (err) {
    console.error("Get next Sales Order number error:", err);
    res.status(500).json({ success: false, message: "Database Error", error: err.message });
  }
});



// GET list of all Sales Orders for lookup/search
router.get("/list", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query(`
      SELECT DISTINCT 
        h.SalesOrderNo, 
        h.SalesOrderDate, 
        h.LocationCode, 
        h.SupplierCode, 
        s.SupplierName
      FROM tblSalesOrderHeader h
      LEFT JOIN tblSupplier s ON h.SupplierCode = s.SupplierCode
      ORDER BY h.SalesOrderNo DESC
    `);
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Get Sales Order list error:", err);
    res.status(500).json({ success: false, message: "Database Error", error: err.message });
  }
});

// GET Fetch Reorder items matching screen queries
router.get("/fetch-reorder", async (req, res) => {
  try {
    const pool = req.pool;
    const { LocationCode, CategoryCode, CatalogueCode } = req.query;

    if (!LocationCode) {
      return res.status(400).json({ success: false, message: "Location Code is required" });
    }

    let query = `
      SELECT 
        c.LocationCode, 
        b.CategoryCode, 
        b.CatalogueCode, 
        a.weight AS Weight, 
        a.toweight AS ToWeight, 
        a.SizeCode, 
        a.SizeType, 
        a.ToSizeCode, 
        a.ToSizeType,
        a.ReOrderLevel, 
        SUM(c.Qtyonhand) AS Qty
      FROM tblReOrderlevelMaster a WITH(nolock)
      INNER JOIN tblInventory b WITH(nolock) ON a.CategoryCode = b.CategoryCode AND a.CatalogueCode = b.CatalogueCode
      INNER JOIN tblInventoryStock c WITH(nolock) ON b.InventoryCode = c.InventoryCode
      WHERE c.locationcode = @LocationCode
        AND a.locationcode = @LocationCode
    `;

    const request = pool.request().input("LocationCode", sql.VarChar, LocationCode);

    if (CategoryCode && CategoryCode.trim() !== "") {
      query += " AND b.CategoryCode = @CategoryCode";
      request.input("CategoryCode", sql.VarChar, CategoryCode.trim());
    }
    if (CatalogueCode && CatalogueCode.trim() !== "") {
      query += " AND b.CatalogueCode = @CatalogueCode";
      request.input("CatalogueCode", sql.VarChar, CatalogueCode.trim());
    }

    query += `
      AND b.weight BETWEEN a.weight AND a.toweight
      AND b.sizecode BETWEEN a.SizeCode AND a.ToSizeCode
      GROUP BY 
        c.LocationCode, b.CategoryCode, b.CatalogueCode, 
        a.SizeCode, a.SizeType, a.ToSizeCode, a.ToSizeType, 
        a.ReOrderLevel, a.weight, a.toweight
      HAVING (ISNULL(a.ReOrderLevel, 0) > SUM(c.Qtyonhand))
    `;

    const result = await request.query(query);
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch reorder logic error:", err);
    res.status(500).json({ success: false, message: "Database Error", error: err.message });
  }
});

// GET retrieve Sales Order details by number
router.get("/:salesOrderNo", async (req, res) => {
  try {
    const { salesOrderNo } = req.params;
    const pool = req.pool;

    const headerRes = await pool.request()
      .input("SalesOrderNo", sql.VarChar, salesOrderNo)
      .query(`
        SELECT h.*, s.SupplierName
        FROM tblSalesOrderHeader h
        LEFT JOIN tblSupplier s ON h.SupplierCode = s.SupplierCode
        WHERE h.SalesOrderNo = @SalesOrderNo
      `);

    if (headerRes.recordset.length === 0) {
      return res.status(404).json({ success: false, message: "Sales Order not found" });
    }

    const detailRes = await pool.request()
      .input("SalesOrderNo", sql.VarChar, salesOrderNo)
      .query(`
        SELECT * FROM tblSalesOrderDetail WHERE SalesOrderNo = @SalesOrderNo
      `);

    res.json({
      success: true,
      header: headerRes.recordset[0],
      details: detailRes.recordset
    });
  } catch (err) {
    console.error("Fetch Sales Order error:", err);
    res.status(500).json({ success: false, message: "Database Error", error: err.message });
  }
});

// POST Create or Update Sales Order
router.post("/", async (req, res) => {
  const pool = req.pool;
  const transaction = new sql.Transaction(pool);

  try {
    const { header, details, userId } = req.body;
    if (!header || !header.SalesOrderNo) {
      return res.status(400).json({ success: false, message: "SalesOrderNo is required" });
    }

    const sysUser = userId || "SYSTEM";
    const curDate = new Date();
    const parsedDueDate = parseDateForSQL(header.DueDate);
    const parsedSODate = parseDateForSQL(header.SalesOrderDate) || curDate;

    await transaction.begin();

    const checkRequest = new sql.Request(transaction);
    const checkRes = await checkRequest
      .input("SalesOrderNoCheck", sql.VarChar, header.SalesOrderNo)
      .query("SELECT 1 FROM tblSalesOrderHeader WHERE SalesOrderNo = @SalesOrderNoCheck");

    // Calculate TotalWeight and SalesOrderQty from details or header
    let totalWeight = 0;
    let totalQty = 0;
    let firstCategory = header.CategoryCode || "";
    let firstCatalogue = header.CatalogueCode || "";

    if (details && details.length > 0) {
      for (const d of details) {
        totalWeight += parseFloat(d.Weight) || 0;
        totalQty += parseFloat(d.SalesOrderQty) || 0;
      }
      if (!firstCategory && details[0].CategoryCode) firstCategory = details[0].CategoryCode;
      if (!firstCatalogue && details[0].CatalogueCode) firstCatalogue = details[0].CatalogueCode;
    }

    const headerRequest = new sql.Request(transaction)
      .input("SalesOrderNo", sql.VarChar, header.SalesOrderNo)
      .input("SalesOrderDate", sql.SmallDateTime, parsedSODate)
      .input("DueDate", sql.SmallDateTime, parsedDueDate)
      .input("LocationCode", sql.VarChar, header.LocationCode || "")
      .input("SupplierCode", sql.VarChar, header.SupplierCode || "")
      .input("CategoryCode", sql.VarChar, firstCategory)
      .input("CatalogueCode", sql.VarChar, firstCatalogue)
      .input("TotalWeight", sql.Decimal(18, 3), totalWeight)
      .input("SalesOrderQty", sql.Decimal(18, 2), totalQty)
      .input("User", sql.VarChar, sysUser)
      .input("Date", sql.SmallDateTime, curDate);

    if (checkRes.recordset.length > 0) {
      // Update header
      await headerRequest.query(`
        UPDATE tblSalesOrderHeader SET
          SalesOrderDate = @SalesOrderDate,
          DueDate = @DueDate,
          LocationCode = @LocationCode,
          SupplierCode = @SupplierCode,
          CategoryCode = @CategoryCode,
          CatalogueCode = @CatalogueCode,
          TotalWeight = @TotalWeight,
          SalesOrderQty = @SalesOrderQty,
          ModifyUser = @User,
          ModifyDate = @Date
        WHERE SalesOrderNo = @SalesOrderNo
      `);

      // Delete existing details to overwrite
      await new sql.Request(transaction)
        .input("SalesOrderNo", sql.VarChar, header.SalesOrderNo)
        .query("DELETE FROM tblSalesOrderDetail WHERE SalesOrderNo = @SalesOrderNo");
    } else {
      // Insert header
      await headerRequest.query(`
        INSERT INTO tblSalesOrderHeader (
          SalesOrderNo, SalesOrderDate, DueDate, LocationCode, SupplierCode, CategoryCode, CatalogueCode, TotalWeight, SalesOrderQty, CreateUser, CreateDate, ModifyUser, ModifyDate
        ) VALUES (
          @SalesOrderNo, @SalesOrderDate, @DueDate, @LocationCode, @SupplierCode, @CategoryCode, @CatalogueCode, @TotalWeight, @SalesOrderQty, @User, @Date, @User, @Date
        )
      `);

      // Auto-increment the Sales Order No in tblCompany
      await new sql.Request(transaction).query(`
        UPDATE tblCompany SET NextSalesOrderNo = ISNULL(NextSalesOrderNo, 0) + 1
      `);
    }

    // Insert details
    if (details && details.length > 0) {
      for (const item of details) {
        await new sql.Request(transaction)
          .input("SalesOrderNo", sql.VarChar, header.SalesOrderNo)
          .input("SalesOrderDate", sql.SmallDateTime, parsedSODate)
          .input("DueDate", sql.SmallDateTime, parsedDueDate)
          .input("LocationCode", sql.VarChar, header.LocationCode || "")
          .input("SupplierCode", sql.VarChar, header.SupplierCode || "")
          .input("CategoryCode", sql.VarChar, item.CategoryCode || "")
          .input("CatalogueCode", sql.VarChar, item.CatalogueCode || "")
          .input("Weight", sql.Decimal(18, 3), parseFloat(item.Weight) || 0)
          .input("ToWeight", sql.Decimal(18, 3), parseFloat(item.ToWeight) || 0)
          .input("SizeCode", sql.Decimal(18, 2), parseFloat(item.SizeCode) || 0)
          .input("SizeType", sql.VarChar, item.SizeType || "")
          .input("ReOrderLevel", sql.Decimal(18, 2), parseFloat(item.ReOrderLevel) || 0)
          .input("SalesOrderQty", sql.Decimal(18, 2), parseFloat(item.SalesOrderQty) || 0)
          .input("Remarks", sql.VarChar, header.Remarks || "")
          .input("User", sql.VarChar, sysUser)
          .input("Date", sql.SmallDateTime, curDate)
          .query(`
            INSERT INTO tblSalesOrderDetail (
              SalesOrderNo, SalesOrderDate, DueDate, LocationCode, SupplierCode, CategoryCode, CatalogueCode, 
              Weight, ToWeight, SizeCode, SizeType, ReOrderLevel, SalesOrderQty, Remarks, CreateUser, CreateDate, ModifyUser, ModifyDate
            ) VALUES (
              @SalesOrderNo, @SalesOrderDate, @DueDate, @LocationCode, @SupplierCode, @CategoryCode, @CatalogueCode, 
              @Weight, @ToWeight, @SizeCode, @SizeType, @ReOrderLevel, @SalesOrderQty, @Remarks, @User, @Date, @User, @Date
            )
          `);
      }
    }

    await transaction.commit();
    res.json({
      success: true,
      message: "Sales Order saved successfully",
      salesOrderNo: header.SalesOrderNo
    });
  } catch (err) {
    console.error("Save Sales Order error:", err);
    try {
      await transaction.rollback();
    } catch (rbErr) {}
    res.status(500).json({ 
      success: false, 
      message: err.message || "Database update failed", 
      error: err.message,
      detail: err.stack 
    });
  }
});

// DELETE Sales Order
router.delete("/:salesOrderNo", async (req, res) => {
  const pool = req.pool;
  const { salesOrderNo } = req.params;

  try {
    // Check if Jobsheet uses this SalesOrderNo
    const checkJS = await pool.request()
      .input("SalesOrderNo", sql.VarChar, salesOrderNo)
      .query("SELECT 1 FROM tblJobSheetHeader WHERE SalesOrderNo = @SalesOrderNo");

    if (checkJS.recordset.length > 0) {
      return res.status(400).json({
        success: false,
        message: "Can Not Delete SalesOrder , Beacuse This SalesOrder Exists in JobSheet"
      });
    }

    const transaction = new sql.Transaction(pool);
    await transaction.begin();

    await new sql.Request(transaction)
      .input("SalesOrderNo", sql.VarChar, salesOrderNo)
      .query("DELETE FROM tblSalesOrderDetail WHERE SalesOrderNo = @SalesOrderNo");

    await new sql.Request(transaction)
      .input("SalesOrderNo", sql.VarChar, salesOrderNo)
      .query("DELETE FROM tblSalesOrderHeader WHERE SalesOrderNo = @SalesOrderNo");

    await transaction.commit();
    res.json({ success: true, message: "Sales Order deleted successfully" });
  } catch (err) {
    console.error("Delete Sales Order error:", err);
    res.status(500).json({ success: false, message: "Database delete failed", error: err.message });
  }
});

module.exports = router;
