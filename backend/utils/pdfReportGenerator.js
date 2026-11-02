/**
 * Pure JavaScript PDF Generator for Table Reports
 * Produces valid standard PDF 1.4 documents with no external dependencies
 */

function generateTablePdf({ title = 'Customer Report', columns = [], data = [], totals = null, fromDate = null, toDate = null }) {
  // Page setup: A4 Landscape (841.89 x 595.28 pt)
  const pageWidth = 841.89;
  const pageHeight = 595.28;
  const marginLeft = 35;
  const marginRight = 35;
  const marginTop = 35;
  const marginBottom = 35;
  const contentWidth = pageWidth - marginLeft - marginRight;

  // Font setup (Standard Helvetica & Helvetica-Bold in standard 14 fonts)
  // Calculate column widths
  const numCols = Math.max(columns.length, 1);
  const colWidth = contentWidth / numCols;

  const escapePdfText = (str) => {
    if (str === null || str === undefined) return '';
    return String(str)
      .replace(/\\/g, '\\\\')
      .replace(/\(/g, '\\(')
      .replace(/\)/g, '\\)');
  };

  const pages = [];
  const rowHeight = 18;
  const headerHeight = 22;

  // Chunk data into pages
  // Page 1 has title banner (approx 70 pt), so fewer rows
  const maxRowsPage1 = Math.floor((pageHeight - marginTop - marginBottom - 75 - headerHeight) / rowHeight);
  const maxRowsOther = Math.floor((pageHeight - marginTop - marginBottom - headerHeight) / rowHeight);

  let currentDataIndex = 0;
  let pageNum = 1;

  while (currentDataIndex < data.length || pageNum === 1) {
    const isFirstPage = pageNum === 1;
    const maxRows = isFirstPage ? maxRowsPage1 : maxRowsOther;
    const pageRows = data.slice(currentDataIndex, currentDataIndex + maxRows);
    currentDataIndex += maxRows;

    let stream = '';

    // Draw header on first page
    let currentY = pageHeight - marginTop;

    if (isFirstPage) {
      // Title
      stream += `BT /F2 16 Tf 0.62 0.43 0.03 rg ${marginLeft} ${currentY - 16} Td (${escapePdfText(title)}) Tj ET\n`;
      currentY -= 24;

      // Subtitle / Period
      const periodStr = fromDate && toDate ? `Period: ${fromDate} to ${toDate}   |   Exported: ${new Date().toLocaleDateString('en-GB')}` : `Exported: ${new Date().toLocaleDateString('en-GB')}`;
      stream += `BT /F1 9 Tf 0.4 0.4 0.4 rg ${marginLeft} ${currentY - 10} Td (${escapePdfText(periodStr)}) Tj ET\n`;
      currentY -= 24;

      // Divider line
      stream += `0.85 0.8 0.7 RG 1 w ${marginLeft} ${currentY} m ${pageWidth - marginRight} ${currentY} l S\n`;
      currentY -= 14;
    }

    // Table Header Row Background
    stream += `0.62 0.43 0.03 rg ${marginLeft} ${currentY - headerHeight} ${contentWidth} ${headerHeight} re f\n`;

    // Table Header Text
    columns.forEach((col, idx) => {
      const colX = marginLeft + idx * colWidth + 4;
      const text = escapePdfText(col.label || col.key);
      const truncated = text.length > 25 ? text.substring(0, 23) + '..' : text;
      stream += `BT /F2 9 Tf 1 1 1 rg ${colX} ${currentY - 15} Td (${truncated}) Tj ET\n`;
    });

    currentY -= headerHeight;

    // Data Rows
    pageRows.forEach((row, rowIdx) => {
      const isOdd = rowIdx % 2 === 1;
      if (isOdd) {
        // Light cream background for alternating rows
        stream += `0.98 0.97 0.95 rg ${marginLeft} ${currentY - rowHeight} ${contentWidth} ${rowHeight} re f\n`;
      }

      // Bottom border for each row
      stream += `0.91 0.86 0.72 RG 0.5 w ${marginLeft} ${currentY - rowHeight} m ${pageWidth - marginRight} ${currentY - rowHeight} l S\n`;

      // Text cells
      columns.forEach((col, idx) => {
        const colX = marginLeft + idx * colWidth + 4;
        const val = row[col.key];
        const text = escapePdfText(val !== null && val !== undefined ? String(val) : '');
        const maxLen = Math.floor(colWidth / 6.5);
        const truncated = text.length > maxLen ? text.substring(0, maxLen - 2) + '..' : text;
        stream += `BT /F1 8.5 Tf 0.23 0.18 0.09 rg ${colX} ${currentY - 13} Td (${truncated}) Tj ET\n`;
      });

      currentY -= rowHeight;
    });

    // Page Number Footer
    const footerText = `Page ${pageNum}`;
    stream += `BT /F1 8 Tf 0.5 0.5 0.5 rg ${pageWidth / 2 - 15} ${marginBottom - 15} Td (${footerText}) Tj ET\n`;

    pages.push(stream);
    pageNum++;

    if (currentDataIndex >= data.length) break;
  }

  // Construct PDF Objects
  const objects = [];
  const addObject = (content) => {
    objects.push(content);
    return objects.length;
  };

  // 1. Catalog
  // 2. Outlines
  // 3. Pages
  // 4. Font 1 (Helvetica)
  // 5. Font 2 (Helvetica-Bold)

  const catalogObjNum = 1;
  const outlinesObjNum = 2;
  const pagesObjNum = 3;
  const font1ObjNum = 4;
  const font2ObjNum = 5;

  addObject(`<< /Type /Catalog /Pages ${pagesObjNum} 0 R >>`);
  addObject(`<< /Type /Outlines /Count 0 >>`);

  // We will fill pages later
  const pageObjectRefs = [];
  const pageStartObjNum = 6;

  // Reserve slots for fonts and page structure
  addObject(''); // placeholder for Pages
  addObject(`<< /Type /Font /Subtype /Type1 /Name /F1 /BaseFont /Helvetica /Encoding /WinAnsiEncoding >>`);
  addObject(`<< /Type /Font /Subtype /Type1 /Name /F2 /BaseFont /Helvetica-Bold /Encoding /WinAnsiEncoding >>`);

  // Create Page and Content objects
  let currentObjNum = 6;
  pages.forEach((pageStream) => {
    const pageObjNum = currentObjNum++;
    const contentObjNum = currentObjNum++;
    pageObjectRefs.push(`${pageObjNum} 0 R`);

    const pageObj = `<< /Type /Page /Parent ${pagesObjNum} 0 R /MediaBox [0 0 ${pageWidth} ${pageHeight}] /Contents ${contentObjNum} 0 R /Resources << /Font << /F1 ${font1ObjNum} 0 R /F2 ${font2ObjNum} 0 R >> >> >>`;
    const contentObj = `<< /Length ${Buffer.byteLength(pageStream, 'utf8')} >>\nstream\n${pageStream}endstream`;

    addObject(pageObj);
    addObject(contentObj);
  });

  // Update Pages Object (index 2 in 0-based array)
  objects[2] = `<< /Type /Pages /Kids [${pageObjectRefs.join(' ')}] /Count ${pages.length} >>`;

  // Build binary buffer
  let pdf = '%PDF-1.4\n';
  const xrefOffsets = [];

  objects.forEach((objContent, index) => {
    xrefOffsets.push(Buffer.byteLength(pdf, 'binary'));
    pdf += `${index + 1} 0 obj\n${objContent}\nendobj\n`;
  });

  const startxref = Buffer.byteLength(pdf, 'binary');
  pdf += `xref\n0 ${objects.length + 1}\n0000000000 65535 f \n`;

  xrefOffsets.forEach((offset) => {
    const str = String(offset).padStart(10, '0');
    pdf += `${str} 00000 n \n`;
  });

  pdf += `trailer\n<< /Size ${objects.length + 1} /Root ${catalogObjNum} 0 R >>\nstartxref\n${startxref}\n%%EOF`;

  return Buffer.from(pdf, 'binary');
}

module.exports = { generateTablePdf };

