const express = require("express");
const router = express.Router();

router.post("/report", async (req, res) => {
  try {
    const { SelectOption, PrintOption, CustomerDateType, DateFrom, DateTo } = req.body;
    const pool = req.pool;

    let query = "";
    
    const safeDateFrom = DateFrom ? DateFrom.replace(/-/g, '') : '';
    const safeDateTo = DateTo ? DateTo.replace(/-/g, '') : '';

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
      query = `SELECT CustomerCode, CustomerName, Address1, Address2, Address3, Address4, Phone1, Phone2, Mobile, Fax, eMail FROM tblCustomer WHERE 1=1`;
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

    let records = [];
    try {
      const dbResult = await pool.request().query(query);
      records = dbResult.recordset;
    } catch (e) {
      console.error("Query Error:", e.message);
      return res.send(`<html><body><h2>SQL Execution Error</h2><p><strong>Error:</strong> ${e.message}</p><p><strong>Query:</strong> ${query}</p></body></html>`);
    }

    let companyName = "SMART GOLD";
    let companyAddress = "";

    try {
      const companyResult = await pool.request().query("SELECT TOP 1 * FROM tblcompany");
      if (companyResult.recordset.length > 0) {
        const comp = companyResult.recordset[0];
        companyName = comp.Name || companyName;
        const addrParts = [];
        if (comp.Address1) addrParts.push(comp.Address1);
        if (comp.Address2) addrParts.push(comp.Address2);
        if (comp.City) addrParts.push(comp.City);
        if (comp.State && comp.State !== comp.City) addrParts.push(comp.State);
        if (comp.Country && comp.Country !== comp.City && comp.Country !== comp.State) addrParts.push(comp.Country);
        companyAddress = addrParts.join(" ");
      }
    } catch (e) {
      console.error("Failed to fetch company info", e);
    }

    let reportTitle = `Master Report : ${SelectOption}`;

    // Generate table dynamically based on keys
    let reportBody = '';
    if (records.length === 0) {
      reportBody = `<div style="text-align: center; padding-top: 20px;">No records found.</div>`;
    } else {
      const columns = Object.keys(records[0]);
      reportBody += `<table><thead><tr>`;
      columns.forEach(col => {
        reportBody += `<th>${col}</th>`;
      });
      reportBody += `</tr></thead><tbody>`;
      records.forEach(row => {
        reportBody += `<tr>`;
        columns.forEach(col => {
          reportBody += `<td>${row[col] !== null ? row[col] : ''}</td>`;
        });
        reportBody += `</tr>`;
      });
      reportBody += `</tbody></table>`;
    }

    const html = `
    <html>
      <head>
        <title>${reportTitle}</title>
        <style>
          body { font-family: 'Courier New', Courier, monospace; font-size: 12px; padding: 20px; }
          .header { display: flex; justify-content: space-between; font-size: 12px; margin-bottom: 20px;}
          .title { font-weight: bold; font-size: 14px; margin-top: 20px; margin-bottom: 10px;}
          table { width: 100%; border-collapse: collapse; margin-top: 10px;}
          th { border-top: 2px solid black; border-bottom: 2px solid black; padding: 5px; text-align: left; font-size: 12px;}
          td { padding: 5px; text-align: left; font-size: 12px;}
          .right { text-align: right; }
          .bold { font-weight: bold; }
          @media print {
            @page { margin: 10mm; }
            body { padding: 0; }
          }
        </style>
      </head>
      <body>
        <div class="header">
          <div style="flex: 1;"></div>
          <div style="text-align: center; flex: 2;">
            <strong>${companyName}</strong><br/><br/>
            <strong>${companyAddress}</strong><br/>
            Phone: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Fax:
          </div>
          <div style="flex: 1; display: flex; justify-content: flex-end;">
            <table style="width: auto; border: none; margin: 0;">
              <tr>
                <td style="text-align: right; border: none; padding: 0 5px 2px 0;">Date</td>
                <td style="border: none; padding: 0 5px 2px 0;">:</td>
                <td style="text-align: right; border: none; padding: 0 0 2px 0;">${new Date().toLocaleDateString('en-GB')}</td>
              </tr>
              <tr>
                <td style="text-align: right; border: none; padding: 0 5px 2px 0;">Time</td>
                <td style="border: none; padding: 0 5px 2px 0;">:</td>
                <td style="text-align: right; border: none; padding: 0 0 2px 0;">${new Date().toLocaleTimeString('en-GB')}</td>
              </tr>
              <tr>
                <td style="text-align: right; border: none; padding: 0 5px 0 0;">Page</td>
                <td style="border: none; padding: 0 5px 0 0;">:</td>
                <td style="text-align: right; border: none; padding: 0;">1</td>
              </tr>
            </table>
          </div>
        </div>
        
        <div class="title">
          ${reportTitle}
        </div>
        
        ${reportBody}
        
        <script>
           ${PrintOption === 'Print' ? 'window.onload = function() { window.print(); }' : ''}
        </script>
      </body>
    </html>
    `;

    res.send(html);
  } catch (err) {
    console.error("Generate Report Error:", err);
    res.status(500).send("Server error generating report.");
  }
});

module.exports = router;
