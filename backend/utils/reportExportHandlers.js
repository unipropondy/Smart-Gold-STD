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
async function handleExportPdf({ req, res, defaultTitle = "Report" }) {
  try {
    const { title, columns, data, totals, companyInfo, fromDate, toDate } = req.body;
    if (!columns || !Array.isArray(columns) || !data || !Array.isArray(data)) {
      return res.status(400).json({ success: false, message: "Columns and data arrays are required for export." });
    }

    const html = await generateReportHtml({
      title: title || defaultTitle,
      columns,
      data,
      totals,
      companyInfo,
      fromDate,
      toDate,
      pool: req.pool,
      showPrintButton: false,
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

