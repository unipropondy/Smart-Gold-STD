const XLSX = require("xlsx");
const { generateReportHtml, formatColumnHeader } = require("./reportHtmlTemplate");
const { convertHtmlToPdf } = require("./htmlToPdf");

/**
 * Handle exporting a report to a genuine, structured .xlsx Excel file
 */
function handleExportExcel({ req, res, defaultTitle = "Report" }) {
  try {
    const { title, columns, data, totals, fromDate, toDate } = req.body;
    if (!columns || !Array.isArray(columns) || !data || !Array.isArray(data)) {
      return res.status(400).json({ success: false, message: "Columns and data arrays are required for export." });
    }

    const formattedData = data.map((row) => {
      const formattedRow = {};
      columns.forEach((col) => {
        const header = formatColumnHeader(col.label || col.key);
        let val = row[col.key];
        formattedRow[header] = val !== null && val !== undefined ? val : "";
      });
      return formattedRow;
    });

    // Determine whether to append a TOTAL row to the Excel sheet
    // Same rule: ONLY for additive numeric columns, NO total under names, codes, dates, types
    const isNonSummableCol = (colKey) => {
      if (!colKey) return true;
      const k = colKey.toLowerCase().replace(/[\s_\-\.]/g, "");
      return (
        k.includes("cust") || k.includes("name") || k.includes("code") ||
        k.includes("desc") || k.includes("type") || k.includes("user") ||
        k.includes("date") || k.includes("time") || k.includes("invno") ||
        k.includes("billno") || k.includes("docno") || k.includes("orderno") ||
        k.includes("voucher") || k.includes("category") || k.includes("class") ||
        k.includes("address") || k.includes("phone") || k.includes("mobile") ||
        k.includes("email") || k.includes("nric") || k.includes("passport") ||
        k.includes("status") || k.includes("remark") || k.includes("tray") ||
        k.includes("location") || k.includes("religion") || k.includes("nationality") ||
        k.includes("serial") || k.includes("item") || k.includes("id") ||
        k.includes("rate") || k.includes("loss") || k.includes("goldloss") ||
        k.includes("percentage") || k.includes("pct") || k.includes("%") ||
        k.includes("ratio") || k.includes("margin") || k.includes("markup") ||
        k.includes("prefix") || k.includes("salestype") || k.includes("purchasetype")
      );
    };

    const isAdditiveMetric = (colKey) => {
      if (!colKey || isNonSummableCol(colKey)) return false;
      const k = colKey.toLowerCase().replace(/[\s_\-\.]/g, "");
      if (k.includes("rate") || k.includes("loss") || k.includes("pct") || k.includes("%")) return false;
      return (
        k.includes("point") || k.includes("pts") || k.includes("qty") ||
        k.includes("pcs") || k.includes("wt") || k.includes("weight") ||
        k.includes("cost") || k.includes("amount") || k.includes("total") ||
        k.includes("sales") || k.includes("smc") ||
        k.includes("pmc") || k.includes("gst") || k.includes("tax") ||
        k.includes("discount") || k.includes("disc") || k.includes("sold") ||
        k.includes("balance") || k.includes("stock")
      );
    };

    const getColTotalVal = (c) => {
      const colKey = c.key || "";
      if (isNonSummableCol(colKey) || !isAdditiveMetric(colKey)) return "";
      const k = colKey.toLowerCase().replace(/[\s_\-\.]/g, "");

      if (k === "adjustmentpoints" || k === "adjpts" || k === "adjpoints") {
        const pts = totals?.totalAdjustmentPoints !== undefined ? totals.totalAdjustmentPoints : totals?.adjustmentPoints;
        if (pts !== undefined) return Number(pts);
      }
      if (k === "previouspoints" || k === "prevpts" || k === "prevpoints") {
        const pts = totals?.totalPreviousPoints !== undefined ? totals.totalPreviousPoints : totals?.previousPoints;
        if (pts !== undefined) return Number(pts);
      }
      if (k === "totalpoints") {
        const pts = totals?.totalPoints !== undefined ? totals.totalPoints : totals?.TotalPoints;
        if (pts !== undefined) return Number(pts);
      }
      if (k === "newtotalpoints") {
        const pts = totals?.newTotalPoints !== undefined ? totals.newTotalPoints : totals?.NewTotalPoints;
        if (pts !== undefined) return Number(pts);
      }
      if (k === "rewardpoints" || k === "totalrewardpoints") {
        const pts = totals?.totalRewardPoints !== undefined ? totals.totalRewardPoints : totals?.rewardPoints;
        if (pts !== undefined) return Number(pts);
      }
      if (k === "redeemedpoints" || k === "totalredeemedpoints" || k === "redeempoints") {
        const pts = totals?.totalRedeemedPoints !== undefined ? totals.totalRedeemedPoints : totals?.redeemedPoints;
        if (pts !== undefined) return Number(pts);
      }
      if (k === "balancepoints") {
        const pts = totals?.totalBalancePoints !== undefined ? totals.totalBalancePoints : totals?.balancePoints;
        if (pts !== undefined) return Number(pts);
      }
      if (k === "pcs" || k === "totalpcs" || k === "qty" || k === "totalqty" || k === "sold" || k === "qtysold") {
        const q = totals?.totalPcs !== undefined ? totals.totalPcs : totals?.totalQty !== undefined ? totals.totalQty : totals?.qty !== undefined ? totals.qty : totals?.pcs;
        if (q !== undefined) return Number(q);
      }
      if (k.includes("weight") || k.includes("wt")) {
        const wt = totals?.totalWeight !== undefined ? totals.totalWeight : totals?.totalNetWeight !== undefined ? totals.totalNetWeight : totals?.weight;
        if (wt !== undefined) return Number(Number(wt).toFixed(3));
      }
      if (k === "cost" || k === "totalcost") {
        if (totals?.totalCost !== undefined) return Number(totals.totalCost);
      }
      if (k === "amount" || k === "totalamount" || k === "nettotal" || k === "totalsales" || k === "netamount" || k === "salesamount" || k === "netsales") {
        const amt = totals?.totalAmount !== undefined ? totals.totalAmount : totals?.totalSales !== undefined ? totals.totalSales : totals?.netSales !== undefined ? totals.netSales : totals?.salesAmount;
        if (amt !== undefined) return Number(amt);
      }
      if (totals && totals[colKey] !== undefined) {
        const num = Number(totals[colKey]);
        if (!isNaN(num)) return num;
      }
      if (Array.isArray(data) && data.length > 0) {
        const sum = data.reduce((acc, row) => acc + (Number(row[colKey]) || 0), 0);
        if (!isNaN(sum) && sum !== 0) return sum;
      }
      return "";
    };

    const numericTotals = (columns || []).map(c => getColTotalVal(c));
    const hasValidTotal = numericTotals.some(v => v !== "" && v !== null && v !== undefined);

    if (hasValidTotal) {
      const totalRow = {};
      columns.forEach((col, idx) => {
        const header = formatColumnHeader(col.label || col.key);
        if (idx === 0) {
          totalRow[header] = "TOTAL:";
        } else {
          totalRow[header] = numericTotals[idx] !== "" ? numericTotals[idx] : "";
        }
      });
      formattedData.push(totalRow);
    }

    const wb = XLSX.utils.book_new();
    const ws = XLSX.utils.json_to_sheet(formattedData);

    const colWidths = columns.map((col) => {
      const header = formatColumnHeader(col.label || col.key);
      const headerLen = header.length;
      let maxValLen = 0;
      data.slice(0, 50).forEach((row) => {
        const valStr = String(row[col.key] || "");
        if (valStr.length > maxValLen) maxValLen = valStr.length;
      });
      return { wch: Math.max(headerLen + 2, maxValLen + 2, 12) };
    });
    ws["!cols"] = colWidths;

    XLSX.utils.book_append_sheet(wb, ws, "Report");

    const excelBuffer = XLSX.write(wb, { type: "buffer", bookType: "xlsx" });
    const safeTitle = (title || defaultTitle).replace(/[^a-zA-Z0-9_-]/g, "_");

    res.setHeader("Content-Type", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
    res.setHeader("Content-Disposition", `attachment; filename="${safeTitle}.xlsx"`);
    res.setHeader("Content-Length", excelBuffer.length);
    res.send(excelBuffer);
  } catch (err) {
    console.error("Export Excel Error:", err);
    res.status(500).json({ success: false, message: err.message });
  }
}

/**
 * Handle exporting a report to a PDF matching the exact print preview layout
 */
async function handleExportPdf({ req, res, defaultTitle = "Report", orientation = null }) {
  try {
    const { title, subtitle, columns, data, totals, companyInfo, fromDate, toDate, orientation: reqOrientation } = req.body;
    if (!columns || !Array.isArray(columns) || !data || !Array.isArray(data)) {
      return res.status(400).json({ success: false, message: "Columns and data arrays are required for export." });
    }

    const html = await generateReportHtml({
      title: title || defaultTitle,
      subtitle: subtitle || "",
      columns,
      data,
      totals,
      companyInfo,
      fromDate,
      toDate,
      pool: req.pool,
      showPrintButton: false,
      orientation: reqOrientation || orientation || "portrait",
    });

    const pdfBuffer = await convertHtmlToPdf(html);
    const safeTitle = (title || defaultTitle).replace(/[^a-zA-Z0-9_-]/g, "_");

    res.setHeader("Content-Type", "application/pdf");
    res.setHeader("Content-Disposition", `attachment; filename="${safeTitle}.pdf"`);
    res.setHeader("Content-Length", pdfBuffer.length);
    res.send(pdfBuffer);
  } catch (err) {
    console.error("Export PDF Error:", err);
    res.status(500).json({ success: false, message: err.message });
  }
}

module.exports = {
  handleExportExcel,
  handleExportPdf,
};

