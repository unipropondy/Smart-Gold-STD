/**
 * Developer: durairaj
 */

const express = require("express");
const router = express.Router();
const { sql, poolPromise } = require("../db");

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
    const pool = req.pool || (await poolPromise);
    let nextNo = 1;
    let loc = "HQ";
    try {
      const result = await pool.request().query("SELECT LocationCode, NextSalesOrderNo FROM tblCompany");
      if (result.recordset.length > 0) {
        if (result.recordset[0].NextSalesOrderNo) {
          nextNo = parseInt(result.recordset[0].NextSalesOrderNo, 10) || 1;
        }
        if (result.recordset[0].LocationCode) {
          loc = result.recordset[0].LocationCode.trim();
        }
      }
    } catch (compErr) {
      console.warn("Could not query NextSalesOrderNo from tblCompany:", compErr.message);
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
    const pool = req.pool || (await poolPromise);
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

// GET Fetch Reorder items matching screen queries in frmSalesOrder.vb
router.get("/fetch-reorder", async (req, res) => {
  try {
    const pool = req.pool || (await poolPromise);
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

    const request = pool.request().input("LocationCode", sql.VarChar, LocationCode.trim());

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
    const pool = req.pool || (await poolPromise);

    const headerRes = await pool.request()
      .input("SalesOrderNo", sql.VarChar, salesOrderNo.trim())
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
      .input("SalesOrderNo", sql.VarChar, salesOrderNo.trim())
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
  const pool = req.pool || (await poolPromise);
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

    // Inspect columns of tblSalesOrderHeader dynamically
    let headerCols = [];
    try {
      const hRes = await new sql.Request(transaction).query(`
        SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tblSalesOrderHeader'
      `);
      headerCols = hRes.recordset.map(c => c.COLUMN_NAME.toLowerCase());
    } catch (e) {}

    const hasHeaderCol = (name) => headerCols.length === 0 || headerCols.includes(name.toLowerCase());

    const checkRequest = new sql.Request(transaction);
    const checkRes = await checkRequest
      .input("SalesOrderNoCheck", sql.VarChar, header.SalesOrderNo.trim())
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
      .input("SalesOrderNo", sql.VarChar, header.SalesOrderNo.trim())
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
      // Build dynamic update clauses
      const updateFields = [
        "SalesOrderDate = @SalesOrderDate",
        "DueDate = @DueDate",
        "LocationCode = @LocationCode",
        "SupplierCode = @SupplierCode",
        "CategoryCode = @CategoryCode",
        "CatalogueCode = @CatalogueCode",
        "TotalWeight = @TotalWeight",
        "SalesOrderQty = @SalesOrderQty"
      ];
      if (hasHeaderCol("ModifyUser")) updateFields.push("ModifyUser = @User");
      if (hasHeaderCol("ModifyDate")) updateFields.push("ModifyDate = @Date");

      await headerRequest.query(`
        UPDATE tblSalesOrderHeader SET
          ${updateFields.join(", ")}
        WHERE SalesOrderNo = @SalesOrderNo
      `);

      // Delete existing details to overwrite
      await new sql.Request(transaction)
        .input("SalesOrderNo", sql.VarChar, header.SalesOrderNo.trim())
        .query("DELETE FROM tblSalesOrderDetail WHERE SalesOrderNo = @SalesOrderNo");
    } else {
      // Build dynamic insert clauses
      const insCols = ["SalesOrderNo", "SalesOrderDate", "DueDate", "LocationCode", "SupplierCode", "CategoryCode", "CatalogueCode", "TotalWeight", "SalesOrderQty"];
      const insVals = ["@SalesOrderNo", "@SalesOrderDate", "@DueDate", "@LocationCode", "@SupplierCode", "@CategoryCode", "@CatalogueCode", "@TotalWeight", "@SalesOrderQty"];

      if (hasHeaderCol("CreateUser")) {
        insCols.push("CreateUser");
        insVals.push("@User");
      }
      if (hasHeaderCol("CreateDate")) {
        insCols.push("CreateDate");
        insVals.push("@Date");
      }
      if (hasHeaderCol("ModifyUser")) {
        insCols.push("ModifyUser");
        insVals.push("@User");
      }
      if (hasHeaderCol("ModifyDate")) {
        insCols.push("ModifyDate");
        insVals.push("@Date");
      }

      await headerRequest.query(`
        INSERT INTO tblSalesOrderHeader (${insCols.join(", ")})
        VALUES (${insVals.join(", ")})
      `);

      // Auto-increment NextSalesOrderNo in tblCompany
      try {
        await new sql.Request(transaction).query(`
          UPDATE tblCompany SET NextSalesOrderNo = ISNULL(NextSalesOrderNo, 0) + 1
        `);
      } catch (incErr) {}
    }

    // Inspect columns of tblSalesOrderDetail dynamically
    let detailCols = [];
    try {
      const dRes = await new sql.Request(transaction).query(`
        SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tblSalesOrderDetail'
      `);
      detailCols = dRes.recordset.map(c => c.COLUMN_NAME.toLowerCase());
    } catch (e) {}

    const hasDetailCol = (name) => detailCols.length === 0 || detailCols.includes(name.toLowerCase());

    // Insert details
    if (details && details.length > 0) {
      for (const item of details) {
        const dReq = new sql.Request(transaction)
          .input("SalesOrderNo", sql.VarChar, header.SalesOrderNo.trim())
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
          .input("User", sql.VarChar, sysUser)
          .input("Date", sql.SmallDateTime, curDate);

        const dInsCols = ["SalesOrderNo", "SalesOrderDate", "DueDate", "LocationCode", "SupplierCode", "CategoryCode", "CatalogueCode", "Weight", "ToWeight", "SizeCode", "SizeType", "ReOrderLevel", "SalesOrderQty"];
        const dInsVals = ["@SalesOrderNo", "@SalesOrderDate", "@DueDate", "@LocationCode", "@SupplierCode", "@CategoryCode", "@CatalogueCode", "@Weight", "@ToWeight", "@SizeCode", "@SizeType", "@ReOrderLevel", "@SalesOrderQty"];

        if (hasDetailCol("Remarks")) {
          dInsCols.push("Remarks");
          dInsVals.push("@Remarks");
          dReq.input("Remarks", sql.VarChar, header.Remarks || "");
        }
        if (hasDetailCol("CreateUser")) {
          dInsCols.push("CreateUser");
          dInsVals.push("@User");
        }
        if (hasDetailCol("CreateDate")) {
          dInsCols.push("CreateDate");
          dInsVals.push("@Date");
        }
        if (hasDetailCol("ModifyUser")) {
          dInsCols.push("ModifyUser");
          dInsVals.push("@User");
        }
        if (hasDetailCol("ModifyDate")) {
          dInsCols.push("ModifyDate");
          dInsVals.push("@Date");
        }

        await dReq.query(`
          INSERT INTO tblSalesOrderDetail (${dInsCols.join(", ")})
          VALUES (${dInsVals.join(", ")})
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

// GET Sales Order Print Data matching subPrintSalesOrder(sTransferNo) in frmSalesOrder.vb
router.get("/print-data/:salesOrderNo", async (req, res) => {
  try {
    const { salesOrderNo } = req.params;
    const cleanNo = (salesOrderNo || "").trim();

    if (!cleanNo) {
      return res.status(400).json({ success: false, message: "SalesOrder Number Should not be Empty" });
    }

    const pool = req.pool || (await poolPromise);

    // 1. Fetch Company details matching clsCompany1.getCompanyName
    const compRes = await pool.request().query("SELECT TOP 1 * FROM tblCompany");
    const rawCompany = compRes.recordset[0] || {};
    const company = {
      name: String(rawCompany.Name || rawCompany.CompanyName || "").trim(),
      address1: String(rawCompany.Address1 || "").trim(),
      address2: String(rawCompany.Address2 || "").trim(),
      city: String(rawCompany.City || "").trim(),
      state: String(rawCompany.State || "").trim(),
      country: String(rawCompany.Country || "").trim(),
      phone: String(rawCompany.Phone || "").trim(),
      fax: String(rawCompany.Fax || "").trim(),
      companyShortCode: String(rawCompany.CompanyShortCode || "").trim(),
      currencySign: String(rawCompany.CurrencySign || "SGD").trim()
    };

    // 2. Fetch Header Information
    const headerRes = await pool.request()
      .input("SalesOrderNo", sql.VarChar, cleanNo)
      .query(`
        SELECT h.*, s.SupplierName
        FROM tblSalesOrderHeader h
        LEFT JOIN tblSupplier s ON h.SupplierCode = s.SupplierCode
        WHERE h.SalesOrderNo = @SalesOrderNo
      `);

    const header = headerRes.recordset.length > 0 ? headerRes.recordset[0] : null;

    // 3. Fetch Detail Information using the exact selection formula:
    // {tblSalesOrderDetail.SalesOrderNo} = sTransferNo
    const detailRes = await pool.request()
      .input("SalesOrderNo", sql.VarChar, cleanNo)
      .query(`
        SELECT 
          d.*,
          c.CategoryName,
          ct.CatalogueName
        FROM tblSalesOrderDetail d
        LEFT JOIN tblCategory c ON d.CategoryCode = c.CategoryCode
        LEFT JOIN tblCatalogue ct ON d.CatalogueCode = ct.CatalogueCode
        WHERE d.SalesOrderNo = @SalesOrderNo
        ORDER BY d.CategoryCode ASC, d.CatalogueCode ASC
      `);

    const details = detailRes.recordset;

    if (!header && details.length === 0) {
      return res.status(404).json({
        success: false,
        message: `No Sales Order records found for Sales Order No: ${cleanNo}`
      });
    }

    // Formulas matching VB.NET Crystal Reports:
    // CompAddress1: sCompanyname
    // CompAddress2: Address1 & " " & Address2 & " " & Country
    // CompAddress3: sPhone
    // CompAddress4: sFax
    // ReportTitle: "Transfer Note"
    // RPTSELECTION: sSelectFld
    const compAddress1 = company.name;
    const compAddress2 = [company.address1, company.address2, company.country].filter(Boolean).join(" ");
    const compAddress3 = company.phone;
    const compAddress4 = company.fax;
    const reportTitle = "Transfer Note";
    const rptSelection = "";

    res.json({
      success: true,
      data: {
        company,
        formulas: {
          CompAddress1: compAddress1,
          CompAddress2: compAddress2,
          CompAddress3: compAddress3,
          CompAddress4: compAddress4,
          ReportTitle: reportTitle,
          RPTSELECTION: rptSelection
        },
        header: header || { SalesOrderNo: cleanNo },
        details: details || []
      }
    });
  } catch (err) {
    console.error("Fetch Sales Order print data error:", err);
    res.status(500).json({ success: false, message: "Database Error", error: err.message });
  }
});

// DELETE Sales Order
router.delete("/:salesOrderNo", async (req, res) => {
  const pool = req.pool || (await poolPromise);
  const { salesOrderNo } = req.params;

  try {
    // Check if Jobsheet uses this SalesOrderNo
    const checkJS = await pool.request()
      .input("SalesOrderNo", sql.VarChar, salesOrderNo.trim())
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
      .input("SalesOrderNo", sql.VarChar, salesOrderNo.trim())
      .query("DELETE FROM tblSalesOrderDetail WHERE SalesOrderNo = @SalesOrderNo");

    await new sql.Request(transaction)
      .input("SalesOrderNo", sql.VarChar, salesOrderNo.trim())
      .query("DELETE FROM tblSalesOrderHeader WHERE SalesOrderNo = @SalesOrderNo");

    await transaction.commit();
    res.json({ success: true, message: "Sales Order deleted successfully" });
  } catch (err) {
    console.error("Delete Sales Order error:", err);
    res.status(500).json({ success: false, message: "Database delete failed", error: err.message });
  }
});

module.exports = router;
