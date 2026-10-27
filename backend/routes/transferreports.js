const express = require("express");
const router = express.Router();

let memoryReports = [];
let nextReportId = 1;

// GET all reports
router.get("/", async (req, res) => {
  try {
    res.json({ success: true, data: memoryReports.slice().reverse() });
  } catch (err) {
    console.error("Fetch Transfer Reports Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// POST insert new report
router.post("/", async (req, res) => {
  try {
    const body = req.body;

    const newReport = {
      ReportID: nextReportId++,
      ReportName: body.ReportName || '',
      SelectOption: body.SelectOption || '',
      FromLocation: body.FromLocation || '',
      ToLocation: body.ToLocation || '',
      DateFrom: body.DateFrom || '',
      DateTo: body.DateTo || '',
      ReportType: body.ReportType || '',
      TransferStatus: body.TransferStatus || ''
    };

    memoryReports.push(newReport);
    res.json({ success: true, message: "Transfer Report saved successfully" });
  } catch (err) {
    console.error("Create Transfer Report Error:", err);
    res.status(500).json({ success: false, message: err.message, error: err.message });
  }
});

// PUT update report
router.put("/:id", async (req, res) => {
  try {
    const id = parseInt(req.params.id);
    const body = req.body;

    const index = memoryReports.findIndex(r => r.ReportID === id);
    if (index === -1) {
      return res.status(404).json({ success: false, message: "Report not found" });
    }

    memoryReports[index] = {
      ...memoryReports[index],
      ...body,
      ReportID: id
    };

    res.json({ success: true, message: "Transfer Report updated successfully" });
  } catch (err) {
    console.error("Update Transfer Report Error:", err);
    res.status(500).json({ success: false, message: err.message, error: err.message });
  }
});

// DELETE report
router.delete("/:id", async (req, res) => {
  try {
    const id = parseInt(req.params.id);
    const index = memoryReports.findIndex(r => r.ReportID === id);

    if (index !== -1) {
      memoryReports.splice(index, 1);
      res.json({ success: true, message: "Transfer Report deleted successfully" });
    } else {
      res.status(404).json({ success: false, message: "Report not found" });
    }
  } catch (err) {
    console.error("Delete Transfer Report Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// Temporary route to drop the table if it was created
router.get("/drop_table", async (req, res) => {
  try {
    const pool = req.pool;
    await pool.request().query(`
      IF EXISTS (SELECT * FROM sysobjects WHERE name='tblTransferReports' and xtype='U')
      BEGIN
          DROP TABLE tblTransferReports
      END
    `);
    res.json({ success: true, message: "Table tblTransferReports successfully dropped from database." });
  } catch (err) {
    console.error("Drop Table Error:", err);
    res.status(500).json({ error: err.message });
  }
});

// Generate HTML Report
router.get("/test_schema", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query("SELECT TOP 1 * FROM tblInventoryTransferHeader");
    res.json(result.recordset);
  } catch (e) {
    res.json({ error: e.message });
  }
});

router.post("/report", async (req, res) => {
  try {
    const { SelectOption, FromLocation, ToLocation, DateFrom, DateTo, ReportType, TransferStatus } = req.body;

    const dFrom = DateFrom ? new Date(DateFrom).toLocaleDateString('en-GB') : '';
    const dTo = DateTo ? new Date(DateTo).toLocaleDateString('en-GB') : '';

    const pool = req.pool;

    // Convert YYYY-MM-DD to YYYYMMDD for safe MSSQL parsing regardless of region settings
    const safeDateFrom = DateFrom ? DateFrom.replace(/-/g, '') : '';
    const safeDateTo = DateTo ? DateTo.replace(/-/g, '') : '';

    let condition = "1=1";
    if (safeDateFrom && safeDateTo) {
      condition += ` AND h.TransferDate >= '${safeDateFrom} 00:00:00' AND h.TransferDate <= '${safeDateTo} 23:59:59'`;
    }
    if (FromLocation && FromLocation !== 'ALL') {
      condition += ` AND h.FromLocation = '${FromLocation}'`;
    }
    if (ToLocation && ToLocation !== 'ALL') {
      condition += ` AND h.ToLocation = '${ToLocation}'`;
    }
    if (SelectOption !== 'Transfer In') {
      if (TransferStatus === 'PENDING') {
        condition += ` AND h.ToLocStatus = '0'`;
      } else if (TransferStatus === 'ACCEPTED') {
        condition += ` AND h.ToLocStatus = '2'`;
      }
    }

    let records = [];
    let query = "";
    const isSummary = ReportType === 'Summary';

    if (SelectOption === 'Transfer Out') {
      if (isSummary) {
        query = `
          SELECT h.TransferNo, h.FromLocation, h.ToLocation, h.FromLocStatus, h.ToLocStatus, h.TotalWeight, h.TotalPcs, h.TotalCost
          FROM tblInventoryTransferHeader h
          WHERE ${condition}
          ORDER BY h.FromLocation, h.ToLocation, h.TransferNo
        `;
      } else {
        query = `
          SELECT d.TotalCost, d.PCS, d.Weight, d.Description, d.TransferNo, i.DiamondDescription, i.SupplierCode, d.InventoryCode, i.SizeCode, i.SizeType, h.ToLocation, h.FromLocation, i.Weight as InvWeight, i.StoneWeight, h.TransferDate, h.Remarks
          FROM tblInventoryTransferDetail d 
          INNER JOIN tblInventoryTransferHeader h ON d.TransferNo=h.TransferNo 
          LEFT JOIN tblInventory i ON d.InventoryCode=i.InventoryCode AND h.FromLocation=i.LocationCode
          WHERE ${condition}
          ORDER BY d.TransferNo, d.InventoryCode
        `;
      }
    } else if (SelectOption === 'TradeIn Transfer Out') {
      if (isSummary) {
        query = `
          SELECT h.TransferNo, h.FromLocation, h.ToLocation, h.FromLocStatus, h.ToLocStatus, h.TotalWeight, h.TotalPcs, h.TotalCost
          FROM tblInventoryTradeInTransferHeader h
          WHERE ${condition}
          ORDER BY h.FromLocation, h.ToLocation, h.TransferNo
        `;
      } else {
        query = `
          SELECT d.TotalCost, d.PCS, d.Weight, d.Description, d.TransferNo, i.DiamondDescription, i.SupplierCode, d.InventoryCode, i.SizeCode, i.SizeType, h.ToLocation, h.FromLocation, i.Weight as InvWeight, i.StoneWeight, h.TransferDate, h.Remarks
          FROM tblInventoryTradeInTransferDetail d 
          INNER JOIN tblInventoryTradeInTransferHeader h ON d.TransferNo=h.TransferNo 
          LEFT JOIN tblInventory i ON d.InventoryCode=i.InventoryCode AND h.FromLocation=i.LocationCode
          WHERE ${condition}
          ORDER BY d.TransferNo, d.InventoryCode
        `;
      }
    } else if (SelectOption === 'Transfer In') {
      query = `
        SELECT d.TotalCost, d.PCS, d.Weight, d.Description, d.TransferNo, d.InventoryCode, d.QtyOnHand, h.FromLocation, h.ToLocation
        FROM localinventorytransferdetail d
        INNER JOIN tblInventoryTransferHeader h ON d.TransferNo = h.TransferNo
        WHERE ${condition}
        ORDER BY d.TransferNo, d.InventoryCode
      `;
    }

    try {
      const dbResult = await pool.request().query(query);
      records = dbResult.recordset;
    } catch (e) {
      console.error("Query Error:", e.message);
      return res.send(`<html><body><h2>SQL Execution Error</h2><p><strong>Error:</strong> ${e.message}</p><p><strong>Query:</strong> ${query}</p></body></html>`);
    }

    let reportBody = '';

    // 1. TRANSFER OUT SUMMARY & TRADEIN TRANSFER OUT SUMMARY
    if ((SelectOption === 'Transfer Out' || SelectOption === 'TradeIn Transfer Out') && isSummary) {
      const groups = {};
      records.forEach(r => {
        const key = `FromLocation : ${r.FromLocation || 'HQ'} ToLocation : ${r.ToLocation || 'HQ'}`;
        if (!groups[key]) groups[key] = { records: [], totalW: 0, totalP: 0, totalC: 0 };
        const w = parseFloat(r.TotalWeight) || 0;
        const p = parseInt(r.TotalPcs) || 0;
        const c = parseFloat(r.TotalCost) || 0;
        groups[key].records.push(r);
        groups[key].totalW += w;
        groups[key].totalP += p;
        groups[key].totalC += c;
      });

      for (const [groupName, groupData] of Object.entries(groups)) {
        reportBody += `<div class="bold" style="margin-top:15px; margin-bottom: 5px;">${groupName}</div>`;
        reportBody += `
           <table>
             <thead>
               <tr>
                 <th style="width: 15%">Location</th>
                 <th style="width: 20%">TransferNo</th>
                 <th style="width: 20%">From Loc Status To Loc Status</th>
                 <th class="right" style="width: 15%">Weight</th>
                 <th class="right" style="width: 15%">PCS</th>
                 <th class="right" style="width: 15%">TotalCost</th>
               </tr>
             </thead>
             <tbody>
         `;
        groupData.records.forEach(r => {
          const fromStat = r.FromLocStatus === '2' ? 'ACCEPTED' : (r.FromLocStatus === '0' ? 'PENDING' : r.FromLocStatus || 'ACCEPTED');
          const toStat = r.ToLocStatus === '2' ? 'ACCEPTED' : (r.ToLocStatus === '0' ? 'PENDING' : r.ToLocStatus || 'PENDING');
          const weight = (parseFloat(r.TotalWeight) || 0).toFixed(2);
          const pcs = parseInt(r.TotalPcs) || 0;
          const cost = (parseFloat(r.TotalCost) || 0).toFixed(2);
          reportBody += `
              <tr>
                <td class="bold">${r.FromLocation || ''}</td>
                <td class="bold">${r.TransferNo || ''}</td>
                <td>${fromStat} &nbsp;&nbsp;&nbsp;&nbsp; ${toStat}</td>
                <td class="right">${weight}</td>
                <td class="right">${pcs}</td>
                <td class="right">${cost}</td>
              </tr>
            `;
        });
        reportBody += `
             </tbody>
           </table>
           <div style="border-top: 1px solid black; margin-top: 15px;"></div>
           <table style="margin-top: 0;">
             <tr>
               <td class="bold" style="width: 55%;">Total:</td>
               <td class="right bold" style="width: 15%;">${groupData.totalW.toFixed(2)}</td>
               <td class="right bold" style="width: 15%;">${groupData.totalP}</td>
               <td class="right bold" style="width: 15%;">${groupData.totalC.toFixed(2)}</td>
             </tr>
           </table>
           <div style="border-top: 1px solid black;"></div>
         `;
      }
    }

    // 2. TRANSFER IN SUMMARY
    else if (SelectOption === 'Transfer In' && isSummary) {
      const groups = {};
      let grandTotalWeight = 0;
      let grandTotalPcs = 0;
      records.forEach(r => {
        const key = `FromLocation : ${r.FromLocation || 'HQ'} ToLocation : ${r.ToLocation || 'HQ'}`;
        if (!groups[key]) groups[key] = { records: [], totalW: 0, totalP: 0 };
        const w = parseFloat(r.Weight) || 0;
        const p = parseInt(r.PCS) || 0;
        groups[key].records.push(r);
        groups[key].totalW += w;
        groups[key].totalP += p;
        grandTotalWeight += w;
        grandTotalPcs += p;
      });

      for (const [groupName, groupData] of Object.entries(groups)) {
        reportBody += `<div class="bold" style="margin-top:15px; margin-bottom: 5px;">${groupName}</div>`;
        reportBody += `
           <table>
             <thead>
               <tr>
                 <th style="width: 20%">Inventory</th>
                 <th style="width: 50%">Description</th>
                 <th class="right" style="width: 15%">Weight</th>
                 <th class="right" style="width: 15%">PCS</th>
               </tr>
             </thead>
             <tbody>
         `;
        groupData.records.forEach(r => {
          const weight = (parseFloat(r.Weight) || 0).toFixed(2);
          const pcs = parseInt(r.PCS) || 0;
          reportBody += `
              <tr>
                <td>${r.InventoryCode || ''}</td>
                <td>${r.Description || ''}</td>
                <td class="right">${weight}</td>
                <td class="right">${pcs}</td>
              </tr>
            `;
        });
        reportBody += `
             </tbody>
           </table>
         `;
      }
      reportBody += `
        <div style="border-top: 1px solid black; margin-top: 15px;"></div>
        <div class="bold" style="padding-top: 10px; padding-bottom: 10px;">
          Grand Total : 
          <span style="float:right; margin-right: 15%;"></span>
        </div>
        <div style="border-top: 1px solid black;"></div>
      `;
    }

    // 3. TRANSFER OUT DETAIL & TRADEIN TRANSFER OUT DETAIL
    else if ((SelectOption === 'Transfer Out' || SelectOption === 'TradeIn Transfer Out') && !isSummary) {
      const groups = {};
      records.forEach(r => {
        const fromLoc = r.FromLocation || 'HQ';
        const toLoc = r.ToLocation || 'HQ';
        const key = `${fromLoc}|${toLoc}`;
        if (!groups[key]) groups[key] = { transfers: {}, totalW: 0, totalNW: 0, totalP: 0, totalC: 0 };
        
        const transKey = `${r.TransferNo}`;
        const remarks = r.Remarks || '';
        const tDate = r.TransferDate ? new Date(r.TransferDate).toLocaleDateString('en-GB') : '';
        const tKey = `${transKey}|${tDate}|${remarks}`;

        if (!groups[key].transfers[tKey]) {
          groups[key].transfers[tKey] = { records: [], totalW: 0, totalNW: 0, totalP: 0, totalC: 0 };
        }

        const grossW = parseFloat(r.Weight) || 0;
        const netW = parseFloat(r.InvWeight) || parseFloat(r.Weight) || 0;
        const p = parseInt(r.PCS) || 0;
        const c = parseFloat(r.TotalCost) || 0;

        groups[key].transfers[tKey].records.push(r);
        groups[key].transfers[tKey].totalW += grossW;
        groups[key].transfers[tKey].totalNW += netW;
        groups[key].transfers[tKey].totalP += p;
        groups[key].transfers[tKey].totalC += c;

        groups[key].totalW += grossW;
        groups[key].totalNW += netW;
        groups[key].totalP += p;
        groups[key].totalC += c;
      });

      for (const [groupKey, groupData] of Object.entries(groups)) {
        const parts = groupKey.split('|');
        const fromLoc = parts[0];
        const toLoc = parts[1];

        reportBody += `
           <div style="display: flex; justify-content: space-between; font-weight: bold; font-size: 14px; margin-top: 15px; margin-bottom: 20px;">
             <div>${SelectOption} Detail From &nbsp;&nbsp;&nbsp; ${fromLoc} &nbsp;&nbsp;&nbsp; To &nbsp;&nbsp;&nbsp; ${toLoc}</div>
           </div>
           
           <table>
             <thead>
               <tr>
                 <th style="width: 15%">Inventory</th>
                 <th style="width: 25%">Description</th>
                 <th style="width: 10%">Size</th>
                 <th style="width: 10%">Supplier Code</th>
                 <th class="right" style="width: 10%">Gross<br/>Weight</th>
                 <th class="right" style="width: 10%">NetWeight</th>
                 <th class="right" style="width: 10%">PCS</th>
                 <th class="right" style="width: 10%">TotalCost</th>
               </tr>
             </thead>
             <tbody>
         `;
         
        let allRemarks = [];

        for (const [tKey, tData] of Object.entries(groupData.transfers)) {
           const tParts = tKey.split('|');
           const transKey = tParts[0];
           const tDate = tParts[1];
           const remarks = tParts[2];
           if (remarks) allRemarks.push(`${transKey}: ${remarks}`);

           reportBody += `
              <tr>
                <td colspan="8" style="font-weight: bold; padding-top: 15px; padding-bottom: 10px; border: none;">
                  <span style="text-decoration: underline;">TransferNo</span> &nbsp;&nbsp;&nbsp;&nbsp; ${transKey} &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ${tDate}
                </td>
              </tr>
           `;

           tData.records.forEach(r => {
             const grossW = (parseFloat(r.Weight) || 0).toFixed(2);
             const netW = (parseFloat(r.InvWeight) || parseFloat(r.Weight) || 0).toFixed(2);
             const pcs = parseInt(r.PCS) || 0;
             const pcsStr = pcs > 0 ? pcs : '';
             const cost = (parseFloat(r.TotalCost) || 0).toFixed(2);
             reportBody += `
                <tr>
                  <td>${r.InventoryCode || ''}</td>
                  <td>${r.Description || r.DiamondDescription || ''}</td>
                  <td>${r.SizeCode || r.SizeType || '0.00'}</td>
                  <td>${r.SupplierCode || ''}</td>
                  <td class="right">${grossW}</td>
                  <td class="right">${netW}</td>
                  <td class="right">${pcsStr}</td>
                  <td class="right">${cost}</td>
                </tr>
                <tr>
                  <td colspan="4" style="border: none;"></td>
                  <td colspan="4" style="border: none; padding: 0;">
                    <table style="width: 100%; border-top: 1px solid black; border-bottom: 1px solid black; margin: 0;">
                      <tr>
                        <td class="bold" style="width: 25%; text-align: center; border: none; padding: 2px;">Total:</td>
                        <td class="right bold" style="width: 25%; border: none; padding: 2px;">${grossW}</td>
                        <td class="right bold" style="width: 25%; border: none; padding: 2px;">${netW}</td>
                        <td class="right bold" style="width: 12.5%; border: none; padding: 2px;">${pcsStr}</td>
                        <td class="right bold" style="width: 12.5%; border: none; padding: 2px;">${cost}</td>
                      </tr>
                    </table>
                  </td>
                </tr>
              `;
           });
        }

        reportBody += `
             </tbody>
           </table>
           
           <table style="margin-top: 5px;">
             <tr>
               <td class="bold" style="width: 60%; border-bottom: 2px solid black; border-top: none;">Grand Total :</td>
               <td class="right bold" style="width: 10%; border-bottom: 2px solid black; border-top: none;">${groupData.totalW.toFixed(2)}</td>
               <td class="right bold" style="width: 10%; border-bottom: 2px solid black; border-top: none;">${groupData.totalNW.toFixed(2)}</td>
               <td class="right bold" style="width: 10%; border-bottom: 2px solid black; border-top: none;">${groupData.totalP > 0 ? groupData.totalP : ''}</td>
               <td class="right bold" style="width: 10%; border-bottom: 2px solid black; border-top: none;">${groupData.totalC.toFixed(2)}</td>
             </tr>
           </table>
           
           <div class="bold" style="margin-top: 25px;">Remarks &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;: ${allRemarks.join(', ')}</div>
           
           <table style="width: 100%; margin-top: 40px; font-weight: bold; border: none;">
             <tr>
               <td style="width: 33%; border: none;">Send Stock From ${fromLoc}</td>
               <td style="width: 33%; border: none;">Received Stock at ${toLoc}</td>
               <td style="width: 34%; border: none;">Cross Verification :</td>
             </tr>
             <tr>
               <td style="border: none; padding-top: 25px;">No. of Items:.............................................</td>
               <td style="border: none; padding-top: 25px;">No. of Items:.............................................</td>
               <td style="border: none; padding-top: 25px;">Weight Cross Checked By : ...........................</td>
             </tr>
             <tr>
               <td style="border: none; padding-top: 25px;">Name:.......................................................</td>
               <td style="border: none; padding-top: 25px;">Name:.......................................................</td>
               <td style="border: none; padding-top: 25px;">No. of PCS Checked :.......................................</td>
             </tr>
             <tr>
               <td style="border: none; padding-top: 25px;">Sign:.......................................................</td>
               <td style="border: none; padding-top: 25px;">Sign:.......................................................</td>
               <td style="border: none; padding-top: 25px;"></td>
             </tr>
             <tr>
               <td style="border: none; padding-top: 25px;">Date:.......................................................</td>
               <td style="border: none; padding-top: 25px;">Date:.......................................................</td>
               <td style="border: none; padding-top: 25px;"></td>
             </tr>
           </table>
           <div style="margin-bottom: 40px;"></div>
         `;
      }
    }

    // 4. TRANSFER IN DETAIL
    else if (SelectOption === 'Transfer In' && !isSummary) {
      const groups = {};
      records.forEach(r => {
        const key = `FromLocation : ${r.FromLocation || 'HQ'} ToLocation : ${r.ToLocation || 'HQ'}`;
        if (!groups[key]) groups[key] = { records: [], totalW: 0, totalP: 0, totalC: 0 };
        const w = parseFloat(r.Weight) || 0;
        const p = parseInt(r.PCS) || 0;
        const c = parseFloat(r.TotalCost) || 0;
        groups[key].records.push(r);
        groups[key].totalW += w;
        groups[key].totalP += p;
        groups[key].totalC += c;
      });

      for (const [groupName, groupData] of Object.entries(groups)) {
        reportBody += `<div class="bold" style="margin-top:15px; margin-bottom: 5px;">${groupName}</div>`;
        reportBody += `
           <table>
             <thead>
               <tr>
                 <th style="width: 15%">TransferNo</th>
                 <th style="width: 15%">Inventory</th>
                 <th style="width: 25%">Description</th>
                 <th class="right" style="width: 10%">Weight</th>
                 <th class="right" style="width: 10%">PCS</th>
                 <th class="right" style="width: 10%">QtyOnHand</th>
                 <th class="right" style="width: 15%">TotalCost</th>
               </tr>
             </thead>
             <tbody>
         `;
        groupData.records.forEach(r => {
          const weight = (parseFloat(r.Weight) || 0).toFixed(2);
          const pcs = parseInt(r.PCS) || 0;
          const cost = (parseFloat(r.TotalCost) || 0).toFixed(2);
          const qty = r.QtyOnHand || '0';
          reportBody += `
              <tr>
                <td>${r.TransferNo || ''}</td>
                <td>${r.InventoryCode || ''}</td>
                <td>${r.Description || ''}</td>
                <td class="right">${weight}</td>
                <td class="right">${pcs}</td>
                <td class="right">${qty}</td>
                <td class="right">${cost}</td>
              </tr>
            `;
        });
        reportBody += `
             </tbody>
           </table>
           <div style="border-top: 1px solid black; margin-top: 10px;"></div>
           <table style="margin-top: 0;">
             <tr>
               <td style="width: 55%;"></td>
               <td class="bold" style="width: 10%;">Total:</td>
               <td class="right bold" style="width: 10%;">${groupData.totalW.toFixed(2)}</td>
               <td class="right bold" style="width: 10%;">${groupData.totalP}</td>
               <td style="width: 10%;"></td>
               <td class="right bold" style="width: 15%;">${groupData.totalC.toFixed(2)}</td>
             </tr>
           </table>
         `;
      }
    }

    if (records.length === 0) {
      reportBody = `<div style="text-align: center; padding-top: 20px;">No records found for the selected criteria.</div>`;
    }

    // let companyName = "SMART";
    // let companyAddress = "45 KALLANG PUDDING ROAD #08-02 ALPHA BUILDING SINGAPORE";

    try {
      const companyResult = await pool.request().query("SELECT TOP 1 * FROM tblcompany");
      if (companyResult.recordset.length > 0) {
        const comp = companyResult.recordset[0];
        companyName = comp.Name || "";
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

    let titleSuffix = '';
    if (isSummary) {
      titleSuffix = `Summary From ${dFrom} To ${dTo}`;
    } else {
      if (SelectOption === 'Transfer Out' || SelectOption === 'TradeIn Transfer Out') {
        titleSuffix = `Detail I From &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; To`;
      } else {
        titleSuffix = `Detail From ${dFrom} To ${dTo}`;
      }
    }

    const html = `
    <html>
      <head>
        <title>Report - ${SelectOption}</title>
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
        ${((SelectOption === 'Transfer Out' || SelectOption === 'TradeIn Transfer Out') && !isSummary) ? '' : `
        <div class="title">
          ${SelectOption} ${titleSuffix}
        </div>
        `}
        
        ${reportBody}
        
        <script>
           window.onload = function() { window.print(); }
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
