const express = require("express");
const router = express.Router();

router.post("/report", async (req, res) => {
  try {
    const {
      ReportOption, // 'Order entry' | 'Repair Details' | 'Order Inventory' | 'Order Payment' | 'Installment Payment Delete' | 'Order Payment Delete' | 'Repair Entry Delete'
      ReportType,   // 'Summary' | 'Detail'
      PrintOption,  // 'Preview' | 'Print'
      DateFrom,     // YYYY-MM-DD
      DateTo,       // YYYY-MM-DD
      Status,       // 'ALL' | 'PENDING' | 'PROCESSING' | 'READY' | 'DELIVERED'
      IsPdfDownload
    } = req.body;

    const pool = req.pool;

    // Dates formatted for display (DD/MM/YYYY)
    const formatDisplayDate = (dStr) => {
      if (!dStr) return '';
      const parts = dStr.split('-');
      if (parts.length === 3) return `${parts[2]}/${parts[1]}/${parts[0]}`;
      return dStr;
    };

    const sPrintFromDate = formatDisplayDate(DateFrom);
    const sPrintToDate = formatDisplayDate(DateTo);

    const safeDateFrom = DateFrom ? DateFrom.replace(/-/g, '') : '';
    const safeDateTo = DateTo ? DateTo.replace(/-/g, '') : '';

    // Fetch company info
    let companyName = "SMART GOLD";
    let companyAddress = "";
    let companyPhone = "";
    let companyFax = "";

    try {
      const companyResult = await pool.request().query("SELECT TOP 1 * FROM tblCompany");
      if (companyResult.recordset.length > 0) {
        const comp = companyResult.recordset[0];
        companyName = (comp.Name || companyName).trim();
        const addrParts = [];
        if (comp.Address1) addrParts.push(comp.Address1.trim());
        if (comp.Address2) addrParts.push(comp.Address2.trim());
        if (comp.City) addrParts.push(comp.City.trim());
        if (comp.State && comp.State.trim() !== comp.City?.trim()) addrParts.push(comp.State.trim());
        if (comp.Country && comp.Country.trim() !== comp.City?.trim() && comp.Country.trim() !== comp.State?.trim()) addrParts.push(comp.Country.trim());
        companyAddress = addrParts.join(" ");
        companyPhone = (comp.Phone || "").trim();
        companyFax = (comp.Fax || "").trim();
      }
    } catch (e) {
      console.error("Failed to fetch company info:", e);
    }

    const isSummary = ReportType === "Summary";
    let reportTitle = "";
    let query = "";

    // Status mapping: PENDING -> 'D', PROCESSING -> 'P', READY -> 'R', DELIVERED -> 'L'
    let sStatus = "";
    if (Status === "PENDING") sStatus = "D";
    else if (Status === "PROCESSING") sStatus = "P";
    else if (Status === "READY") sStatus = "R";
    else if (Status === "DELIVERED") sStatus = "L";

    // 1. Order entry
    if (ReportOption === "Order entry") {
      let whereClause = "WHERE 1=1";
      if (safeDateFrom && safeDateTo) {
        whereClause += ` AND h.OrderDate >= '${safeDateFrom} 00:00:00' AND h.OrderDate <= '${safeDateTo} 23:59:59'`;
      } else if (safeDateFrom) {
        whereClause += ` AND h.OrderDate >= '${safeDateFrom} 00:00:00'`;
      }
      if (sStatus) {
        whereClause += ` AND h.Status = '${sStatus}'`;
      }

      if (isSummary) {
        reportTitle = `Order Entry Report - Summary From ${sPrintFromDate} To ${sPrintToDate}`;
        query = `
          SELECT 
            h.OrderNo, 
            h.OrderDate, 
            h.DeliveryDate, 
            h.SupplierCode, 
            h.CustomerCode, 
            c.CustomerName,
            h.AdvancePaid, 
            h.TotalCostEstimated, 
            h.Status
          FROM tblInventoryOrderHeader h
          LEFT JOIN tblCustomer c ON h.CustomerCode = c.CustomerCode
          ${whereClause}
          ORDER BY h.OrderNo
        `;
      } else {
        reportTitle = `Order Entry Report - Detail From ${sPrintFromDate} To ${sPrintToDate}`;
        query = `
          SELECT 
            h.OrderNo, 
            h.OrderDate, 
            h.DeliveryDate, 
            h.SupplierCode, 
            h.CustomerCode, 
            c.CustomerName,
            h.AdvancePaid, 
            h.TotalCostEstimated, 
            h.Status,
            d.OrderInventoryCode,
            d.InventoryCode,
            d.Description,
            d.StoneDescription,
            d.WeightEstimated,
            d.EstimatedCost,
            d.MC
          FROM tblInventoryOrderHeader h
          LEFT JOIN tblCustomer c ON h.CustomerCode = c.CustomerCode
          LEFT JOIN tblInventoryOrderDetail d ON h.OrderNo = d.OrderNo
          ${whereClause}
          ORDER BY h.OrderNo, d.OrderInventoryCode
        `;
      }
    } 
    // 2. Repair Details
    else if (ReportOption === "Repair Details") {
      let whereClause = "WHERE 1=1";
      if (safeDateFrom && safeDateTo) {
        whereClause += ` AND h.RepairDate >= '${safeDateFrom} 00:00:00' AND h.RepairDate <= '${safeDateTo} 23:59:59'`;
      } else if (safeDateFrom) {
        whereClause += ` AND h.RepairDate >= '${safeDateFrom} 00:00:00'`;
      }

      if (isSummary) {
        reportTitle = `Repair Report - Summary From ${sPrintFromDate} To ${sPrintToDate}`;
        query = `
          SELECT 
            h.RepairNo, 
            h.RepairDate, 
            h.DateOfDelivery AS DeliveryDate, 
            h.SupplierCode, 
            h.CustomerCode, 
            c.CustomerName,
            h.AdvancePaid, 
            h.TotalCostEstimated, 
            h.Status
          FROM tblInventoryRepairHeader h
          LEFT JOIN tblCustomer c ON h.CustomerCode = c.CustomerCode
          ${whereClause}
          ORDER BY h.RepairNo
        `;
      } else {
        reportTitle = `Repair Report - Detail From ${sPrintFromDate} To ${sPrintToDate}`;
        query = `
          SELECT 
            h.RepairNo, 
            h.RepairDate, 
            h.DateOfDelivery AS DeliveryDate, 
            h.SupplierCode, 
            h.CustomerCode, 
            c.CustomerName,
            h.AdvancePaid, 
            h.TotalCostEstimated, 
            h.Status,
            d.Description,
            d.WeightEstimated,
            d.EstimatedCost
          FROM tblInventoryRepairHeader h
          LEFT JOIN tblCustomer c ON h.CustomerCode = c.CustomerCode
          LEFT JOIN tblInventoryRepairDetail d ON h.RepairNo = d.RepairNo
          ${whereClause}
          ORDER BY h.RepairNo
        `;
      }
    }
    // 3. Order Inventory
    else if (ReportOption === "Order Inventory") {
      let whereClause = "WHERE (d.InventoryCode IS NOT NULL AND d.InventoryCode <> '')";
      if (safeDateFrom && safeDateTo) {
        whereClause += ` AND h.OrderDate >= '${safeDateFrom} 00:00:00' AND h.OrderDate <= '${safeDateTo} 23:59:59'`;
      } else if (safeDateFrom) {
        whereClause += ` AND h.OrderDate >= '${safeDateFrom} 00:00:00'`;
      }
      if (sStatus) {
        whereClause += ` AND h.Status = '${sStatus}'`;
      }

      reportTitle = `Order Inventory Report - ${isSummary ? 'Summary' : 'Detail'} From ${sPrintFromDate} To ${sPrintToDate}`;
      query = `
        SELECT 
          h.OrderNo, 
          h.OrderDate, 
          h.DeliveryDate, 
          h.CustomerCode, 
          c.CustomerName,
          d.InventoryCode, 
          d.Description, 
          d.StoneDescription, 
          d.WeightEstimated, 
          d.MC, 
          d.EstimatedCost
        FROM tblInventoryOrderHeader h
        INNER JOIN tblInventoryOrderDetail d ON h.OrderNo = d.OrderNo
        LEFT JOIN tblCustomer c ON h.CustomerCode = c.CustomerCode
        ${whereClause}
        ORDER BY h.OrderNo, d.InventoryCode
      `;
    }
    // 4. Order Payment
    else if (ReportOption === "Order Payment") {
      let whereClause = "WHERE 1=1";
      if (safeDateFrom && safeDateTo) {
        whereClause += ` AND d.CreateDate >= '${safeDateFrom} 00:00:00' AND d.CreateDate <= '${safeDateTo} 23:59:59'`;
      } else if (safeDateFrom) {
        whereClause += ` AND d.CreateDate >= '${safeDateFrom} 00:00:00'`;
      }

      if (isSummary) {
        reportTitle = `Order Payment Report - Summary From ${sPrintFromDate} To ${sPrintToDate}`;
        query = `
          SELECT 
            d.OrderPaymentNo, 
            d.OrderNo, 
            d.CreateDate, 
            d.AmountPaid, 
            c.CustomerName,
            c.CustomerCode
          FROM tblOrderPaymentDetail d
          INNER JOIN tblInventoryOrderHeader h ON d.OrderNo = h.OrderNo
          LEFT JOIN tblCustomer c ON h.CustomerCode = c.CustomerCode
          ${whereClause}
          ORDER BY d.CreateDate DESC, d.OrderPaymentNo
        `;
      } else {
        reportTitle = `Order Payment Report - Detail From ${sPrintFromDate} To ${sPrintToDate}`;
        query = `
          SELECT 
            d.OrderPaymentNo, 
            d.OrderNo, 
            d.CreateDate, 
            d.AmountPaid, 
            d.TradeinAmt,
            d.TradeinGST,
            c.CustomerName,
            c.CustomerCode
          FROM tblOrderPaymentDetail d
          INNER JOIN tblInventoryOrderHeader h ON d.OrderNo = h.OrderNo
          LEFT JOIN tblCustomer c ON h.CustomerCode = c.CustomerCode
          ${whereClause}
          ORDER BY d.OrderNo, d.OrderPaymentNo
        `;
      }
    }
    // 5. Installment Payment Delete
    else if (ReportOption === "Installment Payment Delete") {
      let whereClause = "WHERE 1=1";
      if (safeDateFrom && safeDateTo) {
        whereClause += ` AND d.CreateDate >= '${safeDateFrom} 00:00:00' AND d.CreateDate <= '${safeDateTo} 23:59:59'`;
      } else if (safeDateFrom) {
        whereClause += ` AND d.CreateDate >= '${safeDateFrom} 00:00:00'`;
      }

      reportTitle = `Installment Payment Delete Report From ${sPrintFromDate} To ${sPrintToDate}`;
      query = `
        SELECT 
          d.InstallmentNo, 
          d.AmtPaid, 
          h.Name, 
          h.CustomerCode,
          d.CreateDate, 
          d.InsAgreementNo
        FROM tblInstallmentEntryHeader h
        INNER JOIN tblInstallmentDetailDelete d ON h.InsAgreementNo = d.InsAgreementNo
        ${whereClause}
        ORDER BY d.InstallmentNo
      `;
    }
    // 6. Order Payment Delete
    else if (ReportOption === "Order Payment Delete") {
      let whereClause = "WHERE 1=1";
      if (safeDateFrom && safeDateTo) {
        whereClause += ` AND d.CreateDate >= '${safeDateFrom} 00:00:00' AND d.CreateDate <= '${safeDateTo} 23:59:59'`;
      } else if (safeDateFrom) {
        whereClause += ` AND d.CreateDate >= '${safeDateFrom} 00:00:00'`;
      }

      reportTitle = `Order Payment Delete Report From ${sPrintFromDate} To ${sPrintToDate}`;
      query = `
        SELECT 
          d.OrderNo, 
          d.OrderPaymentNo, 
          d.CreateDate, 
          d.AmountPaid, 
          c.CustomerName, 
          c.CustomerCode
        FROM tblOrderPaymentDetailDelete d
        INNER JOIN tblInventoryOrderHeader h ON d.OrderNo = h.OrderNo
        LEFT JOIN tblCustomer c ON h.CustomerCode = c.CustomerCode
        ${whereClause}
        ORDER BY h.OrderNo, d.OrderPaymentNo
      `;
    }
    // 7. Repair Entry Delete
    else if (ReportOption === "Repair Entry Delete") {
      let whereClause = "WHERE 1=1";
      if (safeDateFrom && safeDateTo) {
        whereClause += ` AND h.RepairDate >= '${safeDateFrom} 00:00:00' AND h.RepairDate <= '${safeDateTo} 23:59:59'`;
      } else if (safeDateFrom) {
        whereClause += ` AND h.RepairDate >= '${safeDateFrom} 00:00:00'`;
      }

      reportTitle = `Repair Payment Delete Report From ${sPrintFromDate} To ${sPrintToDate}`;
      query = `
        SELECT 
          h.RepairNo, 
          h.RepairDate, 
          h.DateOfDelivery AS DeliveryDate, 
          h.SupplierCode, 
          h.CustomerCode, 
          c.CustomerName,
          h.AdvancePaid, 
          h.TotalCostEstimated, 
          h.Status
        FROM tblInventoryRepairHeaderCancel h
        LEFT JOIN tblCustomer c ON h.CustomerCode = c.CustomerCode
        ${whereClause}
        ORDER BY h.RepairNo
      `;
    }

    let records = [];
    try {
      const dbResult = await pool.request().query(query);
      records = dbResult.recordset;
    } catch (e) {
      console.error("TransactionReports Query Error:", e.message);
      // Fallback if Cancel/Delete tables are not named exactly
      if (ReportOption === "Repair Entry Delete" && e.message.includes("tblInventoryRepairHeaderCancel")) {
        try {
          const fallbackQuery = `
            SELECT 
              h.RepairNo, 
              h.RepairDate, 
              h.CustomerCode, 
              c.CustomerName,
              h.AdvancePaid, 
              h.TotalCostEstimated, 
              h.Status
            FROM tblInventoryRepairHeader h
            LEFT JOIN tblCustomer c ON h.CustomerCode = c.CustomerCode
            WHERE h.Status = 'C'
            ORDER BY h.RepairNo
          `;
          const fbRes = await pool.request().query(fallbackQuery);
          records = fbRes.recordset;
        } catch (fbErr) {
          return res.send(`<html><body><h2>SQL Execution Error</h2><p><strong>Error:</strong> ${e.message}</p><p><strong>Query:</strong> ${query}</p></body></html>`);
        }
      } else if (ReportOption === "Installment Payment Delete" && e.message.includes("tblInstallmentDetailDelete")) {
        try {
          const fallbackQuery = `
            SELECT 
              d.InstallmentNo, 
              d.AmtPaid, 
              h.Name, 
              d.CreateDate, 
              d.InsAgreementNo
            FROM tblInstallmentEntryHeader h
            INNER JOIN tblInstallmentDetail d ON h.InsAgreementNo = d.InsAgreementNo
            WHERE d.AmtPaid < 0 OR d.InstallmentNo LIKE '%DEL%'
            ORDER BY d.InstallmentNo
          `;
          const fbRes = await pool.request().query(fallbackQuery);
          records = fbRes.recordset;
        } catch (fbErr) {
          return res.send(`<html><body><h2>SQL Execution Error</h2><p><strong>Error:</strong> ${e.message}</p><p><strong>Query:</strong> ${query}</p></body></html>`);
        }
      } else {
        return res.send(`<html><body><h2>SQL Execution Error</h2><p><strong>Error:</strong> ${e.message}</p><p><strong>Query:</strong> ${query}</p></body></html>`);
      }
    }

    // Format Report Body
    let reportBody = "";

    const formatDt = (dt) => {
      if (!dt) return "";
      const d = new Date(dt);
      if (isNaN(d.getTime())) return String(dt);
      return d.toLocaleDateString('en-GB');
    };

    const formatNum = (n, dec = 2) => {
      const v = parseFloat(n);
      return isNaN(v) ? "0.00" : v.toFixed(dec);
    };

    if (records.length === 0) {
      reportBody = `<div style="text-align: center; padding-top: 30px; font-size: 13px; color: #555;">No records found for the selected criteria.</div>`;
    } 
    // 1. Order Entry Report
    else if (ReportOption === "Order entry") {
      if (isSummary) {
        let totalAdvance = 0;
        let totalCost = 0;

        reportBody += `
          <table>
            <thead>
              <tr>
                <th style="width: 14%;">Order No</th>
                <th style="width: 12%;">Order Date</th>
                <th style="width: 12%;">Delivery Date</th>
                <th style="width: 12%;">Customer</th>
                <th style="width: 16%;">Customer Name</th>
                <th class="right" style="width: 14%;">Advance Paid</th>
                <th class="right" style="width: 14%;">Est. Total Cost</th>
                <th style="width: 6%; text-align: center;">Status</th>
              </tr>
            </thead>
            <tbody>
        `;
        records.forEach(r => {
          const adv = parseFloat(r.AdvancePaid) || 0;
          const cost = parseFloat(r.TotalCostEstimated) || 0;
          totalAdvance += adv;
          totalCost += cost;

          reportBody += `
            <tr>
              <td>${r.OrderNo || ''}</td>
              <td>${formatDt(r.OrderDate)}</td>
              <td>${formatDt(r.DeliveryDate)}</td>
              <td>${r.CustomerCode || ''}</td>
              <td>${r.CustomerName || ''}</td>
              <td class="right">${formatNum(adv)}</td>
              <td class="right">${formatNum(cost)}</td>
              <td style="text-align: center;">${r.Status || ''}</td>
            </tr>
          `;
        });
        reportBody += `
            </tbody>
            <tfoot>
              <tr style="border-top: 2px solid black; font-weight: bold;">
                <td colspan="5">Grand Total:</td>
                <td class="right">${formatNum(totalAdvance)}</td>
                <td class="right">${formatNum(totalCost)}</td>
                <td></td>
              </tr>
            </tfoot>
          </table>
        `;
      } else {
        // Detailed Order Entry Grouped by Order No
        const groups = {};
        records.forEach(r => {
          const orderNo = r.OrderNo || 'N/A';
          if (!groups[orderNo]) {
            groups[orderNo] = {
              header: r,
              items: []
            };
          }
          if (r.Description || r.InventoryCode || r.EstimatedCost) {
            groups[orderNo].items.push(r);
          }
        });

        let totalAdvance = 0;
        let totalCost = 0;
        let grandWeight = 0;

        for (const [orderNo, grp] of Object.entries(groups)) {
          const h = grp.header;
          const adv = parseFloat(h.AdvancePaid) || 0;
          const cost = parseFloat(h.TotalCostEstimated) || 0;
          totalAdvance += adv;
          totalCost += cost;

          reportBody += `
            <div style="margin-top: 15px; margin-bottom: 6px; padding: 6px; background: #f8f8f8; border: 1px solid #ddd; font-size: 12px;">
              <span class="bold">Order No:</span> ${orderNo} &nbsp;&nbsp;|&nbsp;&nbsp;
              <span class="bold">Date:</span> ${formatDt(h.OrderDate)} &nbsp;&nbsp;|&nbsp;&nbsp;
              <span class="bold">Customer:</span> ${h.CustomerCode || ''} - ${h.CustomerName || ''} &nbsp;&nbsp;|&nbsp;&nbsp;
              <span class="bold">Status:</span> ${h.Status || ''} &nbsp;&nbsp;|&nbsp;&nbsp;
              <span class="bold">Adv:</span> ${formatNum(adv)} &nbsp;&nbsp;|&nbsp;&nbsp;
              <span class="bold">Est Cost:</span> ${formatNum(cost)}
            </div>
            <table>
              <thead>
                <tr>
                  <th style="width: 18%;">Inventory Code</th>
                  <th style="width: 32%;">Description</th>
                  <th style="width: 20%;">Stone Description</th>
                  <th class="right" style="width: 15%;">Weight (g)</th>
                  <th class="right" style="width: 15%;">Est. Cost</th>
                </tr>
              </thead>
              <tbody>
          `;

          grp.items.forEach(item => {
            const wt = parseFloat(item.WeightEstimated) || 0;
            const est = parseFloat(item.EstimatedCost) || 0;
            grandWeight += wt;

            reportBody += `
              <tr>
                <td>${item.InventoryCode || '-'}</td>
                <td>${item.Description || '-'}</td>
                <td>${item.StoneDescription || '-'}</td>
                <td class="right">${formatNum(wt, 3)}</td>
                <td class="right">${formatNum(est)}</td>
              </tr>
            `;
          });

          reportBody += `
              </tbody>
            </table>
          `;
        }

        reportBody += `
          <div style="border-top: 2px solid black; margin-top: 15px; padding-top: 8px;">
            <table style="width: 100%; border: none;">
              <tr style="font-weight: bold; font-size: 13px;">
                <td style="width: 50%; border: none;">Total Orders: ${Object.keys(groups).length}</td>
                <td class="right" style="width: 25%; border: none;">Total Advance: ${formatNum(totalAdvance)}</td>
                <td class="right" style="width: 25%; border: none;">Total Est. Cost: ${formatNum(totalCost)}</td>
              </tr>
            </table>
          </div>
        `;
      }
    }
    // 2. Repair Details Report
    else if (ReportOption === "Repair Details") {
      if (isSummary) {
        let totalAdvance = 0;
        let totalCost = 0;

        reportBody += `
          <table>
            <thead>
              <tr>
                <th style="width: 15%;">Repair No</th>
                <th style="width: 12%;">Repair Date</th>
                <th style="width: 12%;">Delivery Date</th>
                <th style="width: 12%;">Customer</th>
                <th style="width: 17%;">Customer Name</th>
                <th class="right" style="width: 16%;">Advance Paid</th>
                <th class="right" style="width: 16%;">Total Cost Est.</th>
              </tr>
            </thead>
            <tbody>
        `;
        records.forEach(r => {
          const adv = parseFloat(r.AdvancePaid) || 0;
          const cost = parseFloat(r.TotalCostEstimated) || 0;
          totalAdvance += adv;
          totalCost += cost;

          reportBody += `
            <tr>
              <td>${r.RepairNo || ''}</td>
              <td>${formatDt(r.RepairDate)}</td>
              <td>${formatDt(r.DeliveryDate)}</td>
              <td>${r.CustomerCode || ''}</td>
              <td>${r.CustomerName || ''}</td>
              <td class="right">${formatNum(adv)}</td>
              <td class="right">${formatNum(cost)}</td>
            </tr>
          `;
        });
        reportBody += `
            </tbody>
            <tfoot>
              <tr style="border-top: 2px solid black; font-weight: bold;">
                <td colspan="5">Grand Total:</td>
                <td class="right">${formatNum(totalAdvance)}</td>
                <td class="right">${formatNum(totalCost)}</td>
              </tr>
            </tfoot>
          </table>
        `;
      } else {
        // Detailed Repair Report
        let totalAdvance = 0;
        let totalCost = 0;

        reportBody += `
          <table>
            <thead>
              <tr>
                <th style="width: 14%;">Repair No</th>
                <th style="width: 12%;">Date</th>
                <th style="width: 16%;">Customer</th>
                <th style="width: 26%;">Description</th>
                <th class="right" style="width: 14%;">Weight (g)</th>
                <th class="right" style="width: 18%;">Estimated Cost</th>
              </tr>
            </thead>
            <tbody>
        `;
        records.forEach(r => {
          const wt = parseFloat(r.WeightEstimated) || 0;
          const cost = parseFloat(r.EstimatedCost || r.TotalCostEstimated) || 0;
          totalCost += cost;

          reportBody += `
            <tr>
              <td>${r.RepairNo || ''}</td>
              <td>${formatDt(r.RepairDate)}</td>
              <td>${r.CustomerCode || ''} - ${r.CustomerName || ''}</td>
              <td>${r.Description || '-'}</td>
              <td class="right">${formatNum(wt, 3)}</td>
              <td class="right">${formatNum(cost)}</td>
            </tr>
          `;
        });
        reportBody += `
            </tbody>
            <tfoot>
              <tr style="border-top: 2px solid black; font-weight: bold;">
                <td colspan="5">Total Estimated Cost:</td>
                <td class="right">${formatNum(totalCost)}</td>
              </tr>
            </tfoot>
          </table>
        `;
      }
    }
    // 3. Order Inventory Report
    else if (ReportOption === "Order Inventory") {
      let grandWeight = 0;
      let grandCost = 0;

      reportBody += `
        <table>
          <thead>
            <tr>
              <th style="width: 14%;">Order No</th>
              <th style="width: 12%;">Date</th>
              <th style="width: 14%;">Customer</th>
              <th style="width: 16%;">Inventory Code</th>
              <th style="width: 20%;">Description</th>
              <th class="right" style="width: 12%;">Weight (g)</th>
              <th class="right" style="width: 12%;">Cost</th>
            </tr>
          </thead>
          <tbody>
      `;
      records.forEach(r => {
        const wt = parseFloat(r.WeightEstimated) || 0;
        const cost = parseFloat(r.EstimatedCost) || 0;
        grandWeight += wt;
        grandCost += cost;

        reportBody += `
          <tr>
            <td>${r.OrderNo || ''}</td>
            <td>${formatDt(r.OrderDate)}</td>
            <td>${r.CustomerCode || ''}</td>
            <td>${r.InventoryCode || ''}</td>
            <td>${r.Description || ''}</td>
            <td class="right">${formatNum(wt, 3)}</td>
            <td class="right">${formatNum(cost)}</td>
          </tr>
        `;
      });
      reportBody += `
          </tbody>
          <tfoot>
            <tr style="border-top: 2px solid black; font-weight: bold;">
              <td colspan="5">Grand Total:</td>
              <td class="right">${formatNum(grandWeight, 3)}</td>
              <td class="right">${formatNum(grandCost)}</td>
            </tr>
          </tfoot>
        </table>
      `;
    }
    // 4. Order Payment Report
    else if (ReportOption === "Order Payment") {
      let grandPaid = 0;

      reportBody += `
        <table>
          <thead>
            <tr>
              <th style="width: 18%;">Payment No</th>
              <th style="width: 16%;">Order No</th>
              <th style="width: 14%;">Date</th>
              <th style="width: 14%;">Customer</th>
              <th style="width: 20%;">Customer Name</th>
              <th class="right" style="width: 18%;">Amount Paid</th>
            </tr>
          </thead>
          <tbody>
      `;
      records.forEach(r => {
        const amt = parseFloat(r.AmountPaid) || 0;
        grandPaid += amt;

        reportBody += `
          <tr>
            <td>${r.OrderPaymentNo || ''}</td>
            <td>${r.OrderNo || ''}</td>
            <td>${formatDt(r.CreateDate)}</td>
            <td>${r.CustomerCode || ''}</td>
            <td>${r.CustomerName || ''}</td>
            <td class="right">${formatNum(amt)}</td>
          </tr>
        `;
      });
      reportBody += `
          </tbody>
          <tfoot>
            <tr style="border-top: 2px solid black; font-weight: bold;">
              <td colspan="5">Grand Total Paid:</td>
              <td class="right">${formatNum(grandPaid)}</td>
            </tr>
          </tfoot>
        </table>
      `;
    }
    // 5. Installment Payment Delete Report
    else if (ReportOption === "Installment Payment Delete") {
      let grandAmt = 0;

      reportBody += `
        <table>
          <thead>
            <tr>
              <th style="width: 20%;">Installment No</th>
              <th style="width: 22%;">Agreement No</th>
              <th style="width: 16%;">Date</th>
              <th style="width: 24%;">Customer Name</th>
              <th class="right" style="width: 18%;">Amount Paid</th>
            </tr>
          </thead>
          <tbody>
      `;
      records.forEach(r => {
        const amt = parseFloat(r.AmtPaid) || 0;
        grandAmt += amt;

        reportBody += `
          <tr>
            <td>${r.InstallmentNo || ''}</td>
            <td>${r.InsAgreementNo || ''}</td>
            <td>${formatDt(r.CreateDate)}</td>
            <td>${r.Name || ''}</td>
            <td class="right">${formatNum(amt)}</td>
          </tr>
        `;
      });
      reportBody += `
          </tbody>
          <tfoot>
            <tr style="border-top: 2px solid black; font-weight: bold;">
              <td colspan="4">Grand Total Deleted Amount:</td>
              <td class="right">${formatNum(grandAmt)}</td>
            </tr>
          </tfoot>
        </table>
      `;
    }
    // 6. Order Payment Delete Report
    else if (ReportOption === "Order Payment Delete") {
      let grandPaid = 0;

      reportBody += `
        <table>
          <thead>
            <tr>
              <th style="width: 18%;">Payment No</th>
              <th style="width: 16%;">Order No</th>
              <th style="width: 14%;">Date</th>
              <th style="width: 14%;">Customer</th>
              <th style="width: 20%;">Customer Name</th>
              <th class="right" style="width: 18%;">Amount Paid</th>
            </tr>
          </thead>
          <tbody>
      `;
      records.forEach(r => {
        const amt = parseFloat(r.AmountPaid) || 0;
        grandPaid += amt;

        reportBody += `
          <tr>
            <td>${r.OrderPaymentNo || ''}</td>
            <td>${r.OrderNo || ''}</td>
            <td>${formatDt(r.CreateDate)}</td>
            <td>${r.CustomerCode || ''}</td>
            <td>${r.CustomerName || ''}</td>
            <td class="right">${formatNum(amt)}</td>
          </tr>
        `;
      });
      reportBody += `
          </tbody>
          <tfoot>
            <tr style="border-top: 2px solid black; font-weight: bold;">
              <td colspan="5">Grand Total Deleted Payment:</td>
              <td class="right">${formatNum(grandPaid)}</td>
            </tr>
          </tfoot>
        </table>
      `;
    }
    // 7. Repair Entry Delete Report
    else if (ReportOption === "Repair Entry Delete") {
      let totalAdvance = 0;
      let totalCost = 0;

      reportBody += `
        <table>
          <thead>
            <tr>
              <th style="width: 16%;">Repair No</th>
              <th style="width: 14%;">Repair Date</th>
              <th style="width: 14%;">Customer Code</th>
              <th style="width: 20%;">Customer Name</th>
              <th class="right" style="width: 18%;">Advance Paid</th>
              <th class="right" style="width: 18%;">Estimated Cost</th>
            </tr>
          </thead>
          <tbody>
      `;
      records.forEach(r => {
        const adv = parseFloat(r.AdvancePaid) || 0;
        const cost = parseFloat(r.TotalCostEstimated) || 0;
        totalAdvance += adv;
        totalCost += cost;

        reportBody += `
          <tr>
            <td>${r.RepairNo || ''}</td>
            <td>${formatDt(r.RepairDate)}</td>
            <td>${r.CustomerCode || ''}</td>
            <td>${r.CustomerName || ''}</td>
            <td class="right">${formatNum(adv)}</td>
            <td class="right">${formatNum(cost)}</td>
          </tr>
        `;
      });
      reportBody += `
          </tbody>
          <tfoot>
            <tr style="border-top: 2px solid black; font-weight: bold;">
              <td colspan="4">Total:</td>
              <td class="right">${formatNum(totalAdvance)}</td>
              <td class="right">${formatNum(totalCost)}</td>
            </tr>
          </tfoot>
        </table>
      `;
    }

    const html = `
    <!DOCTYPE html>
    <html>
      <head>
        <meta charset="utf-8" />
        <title>${reportTitle}</title>
        <style>
          @page {
            size: A4 portrait;
            margin: 12mm 10mm;
          }
          body { 
            font-family: 'Courier New', Courier, monospace; 
            font-size: 12px; 
            color: #111;
            padding: 20px; 
            margin: 0;
            background: #fff;
          }
          .header { 
            display: flex; 
            justify-content: space-between; 
            font-size: 12px; 
            margin-bottom: 15px;
            border-bottom: 1px solid #ccc;
            padding-bottom: 10px;
          }
          .title { 
            font-weight: bold; 
            font-size: 14px; 
            margin-top: 10px; 
            margin-bottom: 12px;
            text-align: center;
            text-transform: uppercase;
            letter-spacing: 0.5px;
          }
          table { 
            width: 100%; 
            border-collapse: collapse; 
            margin-top: 6px;
          }
          th { 
            border-top: 2px solid #000; 
            border-bottom: 2px solid #000; 
            padding: 6px 4px; 
            text-align: left; 
            font-size: 12px;
            font-weight: bold;
          }
          td { 
            padding: 5px 4px; 
            text-align: left; 
            font-size: 12px;
          }
          .right { text-align: right; }
          .bold { font-weight: bold; }
          @media print {
            body { padding: 0; }
          }
        </style>
      </head>
      <body>
        <div class="header">
          <div style="flex: 1;"></div>
          <div style="text-align: center; flex: 2;">
            <strong style="font-size: 15px;">${companyName}</strong><br/>
            <span>${companyAddress}</span><br/>
            <span>Phone: ${companyPhone || ' - '} &nbsp;&nbsp;&nbsp;&nbsp; Fax: ${companyFax || ' - '}</span>
          </div>
          <div style="flex: 1; display: flex; justify-content: flex-end;">
            <table style="width: auto; border: none; margin: 0;">
              <tr>
                <td style="text-align: right; border: none; padding: 0 4px 2px 0;">Date</td>
                <td style="border: none; padding: 0 4px 2px 0;">:</td>
                <td style="text-align: right; border: none; padding: 0 0 2px 0;">${new Date().toLocaleDateString('en-GB')}</td>
              </tr>
              <tr>
                <td style="text-align: right; border: none; padding: 0 4px 2px 0;">Time</td>
                <td style="border: none; padding: 0 4px 2px 0;">:</td>
                <td style="text-align: right; border: none; padding: 0 0 2px 0;">${new Date().toLocaleTimeString('en-GB')}</td>
              </tr>
              <tr>
                <td style="text-align: right; border: none; padding: 0 4px 0 0;">Page</td>
                <td style="border: none; padding: 0 4px 0 0;">:</td>
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
          ${PrintOption === 'Print' || IsPdfDownload ? 'window.onload = function() { window.print(); };' : ''}
        </script>
      </body>
    </html>
    `;

    res.send(html);
  } catch (err) {
    console.error("Generate Transaction Report Error:", err);
    res.status(500).send("Server error generating Transaction report.");
  }
});

module.exports = router;
