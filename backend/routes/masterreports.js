const express = require("express");
const router = express.Router();
const { generateReportHtml } = require("../utils/reportHtmlTemplate");
const { handleExportExcel, handleExportPdf } = require("../utils/reportExportHandlers");
const { validateBackendDates } = require("../utils/validateReportDates");

// 1. POST /api/masterreports/generate
router.post("/generate", async (req, res) => {
  try {
    const { SelectOption, CustomerDateType, DateFrom, DateTo } = req.body;

    const dateVal = validateBackendDates(DateFrom, DateTo);
    if (!dateVal.isValid) {
      return res.status(400).json({ success: false, message: dateVal.message });
    }

    const pool = req.pool;

    let query = "";
    const safeDateFrom = DateFrom ? String(DateFrom).replace(/[-/]/g, '') : '';
    const safeDateTo = DateTo ? String(DateTo).replace(/[-/]/g, '') : '';

    if (SelectOption === 'Class') {
      query = `SELECT ClassCode, ClassName, SalesType, PurchaseType, GoldLoss, Rate, Remarks FROM tblClass ORDER BY ClassName`;
    } else if (SelectOption === 'Category') {
      query = `SELECT CategoryName, FromInventoryCode, ToInventoryCode, NextInventoryCode, ClassCode, InventoryPrefix FROM tblCategory ORDER BY ClassCode`;
    } else if (SelectOption === 'Catalogue') {
      query = `SELECT CatalogueName, CategoryCode FROM tblCatalogue ORDER BY CategoryCode`;
    } else if (SelectOption === 'Tray') {
      query = `SELECT TrayID, TrayName, SalesManID, SalesManName FROM tblTrayMaster`;
    } else if (SelectOption === 'Sales Man') {
      query = `SELECT SalesManCode, SalesManName, Remarks FROM tblSalesMan`;
    } else if (SelectOption === 'Suppliers') {
      query = `SELECT SupplierCode, SupplierName, Phone1, Fax, Mobile FROM tblSupplier`;
    } else if (SelectOption === 'Customers') {
      query = `SELECT RTRIM(CustomerCode) AS [Cust.Code], RTRIM(CustomerName) AS CustomerName, RTRIM(Address1) AS Address1, RTRIM(Address2) AS Address2, RTRIM(Address3) AS Address3, RTRIM(Address4) AS Address4, RTRIM(Phone1) AS Phone1, RTRIM(Phone2) AS Phone2, RTRIM(Mobile) AS Mobile, RTRIM(Fax) AS Fax, RTRIM(eMail) AS eMail, RTRIM(PassPortNo) AS PassPortNo FROM tblCustomer WHERE 1=1`;
      if (safeDateFrom && safeDateTo) {
        if (CustomerDateType === 'Date of Birth') {
          query += ` AND DOB >= '${safeDateFrom} 00:00:00' AND DOB <= '${safeDateTo} 23:59:59'`;
        } else {
          query += ` AND DOA >= '${safeDateFrom} 00:00:00' AND DOA <= '${safeDateTo} 23:59:59'`;
        }
      }
    } else if (SelectOption === "Customer's Mailing Address") {
      query = `SELECT CustomerName, Address1, Address2, Address3, Address4, CustomerCode, NricNo, TotalRewardPoints 
               FROM tblCustomer 
               WHERE CustomerCode NOT IN ('CASH', 'TRA', 'VALUE MAX PRECIOUS M') 
               AND TotalRewardPoints >= 145 
               ORDER BY TotalRewardPoints DESC`;
    } else {
      query = `SELECT ClassCode, ClassName, SalesType, PurchaseType, GoldLoss, Rate, Remarks FROM tblClass ORDER BY ClassName`;
    }

    const dbResult = await pool.request().query(query);
    let records = dbResult.recordset || [];

    if (SelectOption === 'Customers') {
      records = records.map((r) => {
        const addrLines = [r.Address1, r.Address2, r.Address3, r.Address4]
          .map((a) => (a || '').trim())
          .filter(Boolean);
        const p1 = (r.Phone1 || '').trim();
        const p2 = (r.Phone2 || '').trim();
        const hp = (r.Mobile || '').trim();
        const fax = (r.Fax || '').trim();
        const phoneParts = [];
        if (p1) phoneParts.push(`R1: ${p1}`);
        if (p2) phoneParts.push(`R2: ${p2}`);
        if (hp) phoneParts.push(`HP: ${hp}`);
        if (fax) phoneParts.push(`Fax: ${fax}`);

        const email = (r.eMail || '').trim();
        const ppNo = (r.PassPortNo || '').trim();
        const otherParts = [];
        if (email) otherParts.push(`eMail: ${email}`);
        if (ppNo) otherParts.push(`PP No: ${ppNo}`);

        return {
          'Cust.Code': r['Cust.Code'] || r.CustomerCode || '',
          'CustomerName': r.CustomerName || '',
          'Address': addrLines.join('\n'),
          'Phone (R1, R2, HP, Fax)': phoneParts.join('\n'),
          'Other Detail (eMail, PP No)': otherParts.join('\n'),
        };
      });
    }

    const columns = records.length > 0
      ? Object.keys(records[0]).map(k => ({ key: k, label: k }))
      : [{ key: "Result", label: "Result" }];

    res.json({
      success: true,
      title: `Master Report : ${SelectOption || 'Master'}`,
      data: records,
      columns,
      totals: {
        totalItems: records.length,
        totalRecords: records.length
      }
    });
  } catch (err) {
    console.error("Master Reports Generate Error:", err);
    res.status(500).json({ success: false, message: err.message });
  }
});

// Legacy /report endpoint maintained for backward compatibility
router.post("/report", async (req, res) => {
  try {
    const { SelectOption, PrintOption, CustomerDateType, DateFrom, DateTo } = req.body;
    const pool = req.pool;

    let query = "";
    const safeDateFrom = DateFrom ? String(DateFrom).replace(/[-/]/g, '') : '';
    const safeDateTo = DateTo ? String(DateTo).replace(/[-/]/g, '') : '';

    if (SelectOption === 'Class') {
      query = `SELECT ClassCode, ClassName, SalesType, PurchaseType, GoldLoss, Rate, Remarks FROM tblClass ORDER BY ClassName`;
    } else if (SelectOption === 'Category') {
      query = `SELECT CategoryName, FromInventoryCode, ToInventoryCode, NextInventoryCode, ClassCode, InventoryPrefix FROM tblCategory ORDER BY ClassCode`;
    } else if (SelectOption === 'Catalogue') {
      query = `SELECT CatalogueName, CategoryCode FROM tblCatalogue ORDER BY CategoryCode`;
    } else if (SelectOption === 'Tray') {
      query = `SELECT TrayID, TrayName, SalesManID, SalesManName FROM tblTrayMaster`;
    } else if (SelectOption === 'Sales Man') {
      query = `SELECT SalesManCode, SalesManName, Remarks FROM tblSalesMan`;
    } else if (SelectOption === 'Suppliers') {
      query = `SELECT SupplierCode, SupplierName, Phone1, Fax, Mobile FROM tblSupplier`;
    } else if (SelectOption === 'Customers') {
      query = `SELECT RTRIM(CustomerCode) AS [Cust.Code], RTRIM(CustomerName) AS CustomerName, RTRIM(Address1) AS Address1, RTRIM(Address2) AS Address2, RTRIM(Address3) AS Address3, RTRIM(Address4) AS Address4, RTRIM(Phone1) AS Phone1, RTRIM(Phone2) AS Phone2, RTRIM(Mobile) AS Mobile, RTRIM(Fax) AS Fax, RTRIM(eMail) AS eMail, RTRIM(PassPortNo) AS PassPortNo FROM tblCustomer WHERE 1=1`;
      if (safeDateFrom && safeDateTo) {
        if (CustomerDateType === 'Date of Birth') {
          query += ` AND DOB >= '${safeDateFrom} 00:00:00' AND DOB <= '${safeDateTo} 23:59:59'`;
        } else {
          query += ` AND DOA >= '${safeDateFrom} 00:00:00' AND DOA <= '${safeDateTo} 23:59:59'`;
        }
      }
    } else if (SelectOption === "Customer's Mailing Address") {
      query = `SELECT CustomerName, Address1, Address2, Address3, Address4, CustomerCode, NricNo, TotalRewardPoints 
               FROM tblCustomer 
               WHERE CustomerCode NOT IN ('CASH', 'TRA', 'VALUE MAX PRECIOUS M') 
               AND TotalRewardPoints >= 145 
               ORDER BY TotalRewardPoints DESC`;
    }

    const dbResult = await pool.request().query(query);
    let records = dbResult.recordset || [];

    if (SelectOption === 'Customers') {
      records = records.map((r) => {
        const addrLines = [r.Address1, r.Address2, r.Address3, r.Address4]
          .map((a) => (a || '').trim())
          .filter(Boolean);
        const p1 = (r.Phone1 || '').trim();
        const p2 = (r.Phone2 || '').trim();
        const hp = (r.Mobile || '').trim();
        const fax = (r.Fax || '').trim();
        const phoneParts = [];
        if (p1) phoneParts.push(`R1: ${p1}`);
        if (p2) phoneParts.push(`R2: ${p2}`);
        if (hp) phoneParts.push(`HP: ${hp}`);
        if (fax) phoneParts.push(`Fax: ${fax}`);

        const email = (r.eMail || '').trim();
        const ppNo = (r.PassPortNo || '').trim();
        const otherParts = [];
        if (email) otherParts.push(`eMail: ${email}`);
        if (ppNo) otherParts.push(`PP No: ${ppNo}`);

        return {
          'Cust.Code': r['Cust.Code'] || r.CustomerCode || '',
          'CustomerName': r.CustomerName || '',
          'Address': addrLines.join('\n'),
          'Phone (R1, R2, HP, Fax)': phoneParts.join('\n'),
          'Other Detail (eMail, PP No)': otherParts.join('\n'),
        };
      });
    }

    const reportTitle = `Master Report : ${SelectOption || 'Master'}`;
    const columns = records.length > 0
      ? Object.keys(records[0]).map(k => ({ key: k, label: k }))
      : [{ key: "Result", label: "Result" }];

    const html = await generateReportHtml({
      title: reportTitle,
      columns,
      data: records,
      totals: { totalItems: records.length },
      pool,
      showPrintButton: false,
    });

    res.setHeader("Content-Type", "text/html; charset=utf-8");
    res.send(html);
  } catch (err) {
    console.error("Master Report Legacy Error:", err);
    res.status(500).send("Server error generating report.");
  }
});

// 2. POST /api/masterreports/download-html
router.post("/download-html", async (req, res) => {
  try {
    const { title, columns, data, totals, companyInfo, fromDate, toDate, showPrintButton } = req.body;
    const html = await generateReportHtml({
      title: title || "Master Report",
      columns,
      data,
      totals,
      companyInfo,
      fromDate,
      toDate,
      pool: req.pool,
      showPrintButton: showPrintButton ?? false,
    });
    res.setHeader("Content-Type", "text/html; charset=utf-8");
    res.send(html);
  } catch (err) {
    console.error("Master Reports Download HTML Error:", err);
    res.status(500).send("Error generating report HTML: " + err.message);
  }
});

// 3. POST /api/masterreports/export-excel
router.post("/export-excel", (req, res) => {
  handleExportExcel({ req, res, defaultTitle: "Master_Report" });
});

// 4. POST /api/masterreports/export-pdf
router.post("/export-pdf", (req, res) => {
  handleExportPdf({ req, res, defaultTitle: "Master_Report" });
});

module.exports = router;
