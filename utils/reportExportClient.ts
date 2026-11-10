import { Platform, Alert } from 'react-native';
import { API_BASE_URL } from '@/constants/api';
import { formatColumnHeader } from './formatColumnHeader';

export interface ExportReportParams {
  endpoint: string; // e.g. 'inventoryreports', 'salesreports'
  title: string;
  subtitle?: string;
  columns: Array<{ key: string; label: string; [k: string]: any }>;
  data: any[];
  totals?: any;
  fromDate?: string;
  toDate?: string;
}

/**
 * Standardize columns to ensure UPPERCASE and abbreviated single-line headers
 */
function standardizeColumns(columns: Array<{ key: string; label: string; [k: string]: any }>) {
  if (!Array.isArray(columns)) return columns;
  return columns.map((col) => ({
    ...col,
    label: formatColumnHeader(col.label || col.key || ''),
  }));
}

/**
 * Download a genuine .xlsx Excel workbook
 */
export async function downloadExcelReport(params: ExportReportParams): Promise<boolean> {
  const { endpoint, title, subtitle, columns, data, totals, fromDate, toDate } = params;

  if (!data || data.length === 0) {
    Alert.alert('No Data', 'No records available to export.');
    return false;
  }

  const formattedColumns = standardizeColumns(columns);

  try {
    const res = await fetch(`${API_BASE_URL}/${endpoint}/export-excel`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({
        title,
        columns: formattedColumns,
        data,
        totals: totals || {},
        fromDate,
        toDate,
      }),
    });

    if (!res.ok) {
      throw new Error('Failed to generate Excel document');
    }

    const blob = await res.blob();
    const safeTitle = (title || 'Report').replace(/[^a-zA-Z0-9_-]/g, '_');

    if (Platform.OS === 'web' && typeof window !== 'undefined') {
      const url = URL.createObjectURL(blob);
      const link = document.createElement('a');
      link.href = url;
      link.setAttribute('download', `${safeTitle}.xlsx`);
      document.body.appendChild(link);
      link.click();
      document.body.removeChild(link);
      setTimeout(() => URL.revokeObjectURL(url), 1000);
      return true;
    } else {
      Alert.alert('Excel Export', 'Excel file generated successfully.');
      return true;
    }
  } catch (err: any) {
    console.error('Excel Export Error:', err);
    Alert.alert('Export Error', err.message || 'Failed to export Excel.');
    return false;
  }
}

/**
 * Helper to generate client-side PDF directly using html2pdf without opening the Print dialog
 */
function downloadClientSidePdf(htmlContent: string, filename: string): Promise<boolean> {
  return new Promise((resolve) => {
    if (typeof window === 'undefined' || typeof document === 'undefined') {
      return resolve(false);
    }

    const executeHtml2Pdf = () => {
      try {
        // Parse the full HTML document and extract styles and report container
        const parser = new DOMParser();
        const doc = parser.parseFromString(htmlContent, 'text/html');

        // Create an in-DOM container positioned properly so html2canvas renders all text and styles sharply
        const container = document.createElement('div');
        container.style.position = 'fixed';
        container.style.top = '0';
        container.style.left = '0';
        container.style.width = '210mm';
        container.style.background = '#ffffff';
        container.style.zIndex = '999999';
        container.style.overflow = 'visible';

        // Append all style tags
        const styles = doc.querySelectorAll('style, link[rel="stylesheet"]');
        styles.forEach((s) => container.appendChild(s.cloneNode(true)));

        // Extract and append report body content (.page-sheet or body children)
        const sheet = doc.querySelector('.page-sheet');
        if (sheet) {
          container.appendChild(sheet.cloneNode(true));
        } else {
          Array.from(doc.body.childNodes).forEach((node) => container.appendChild(node.cloneNode(true)));
        }

        document.body.appendChild(container);

        const opt = {
          margin: [0, 0, 0, 0],
          filename,
          image: { type: 'jpeg', quality: 0.98 },
          html2canvas: {
            scale: 2,
            useCORS: true,
            logging: false,
            windowWidth: 794,
            scrollX: 0,
            scrollY: 0,
          },
          jsPDF: { unit: 'mm', format: 'a4', orientation: 'portrait' },
          pagebreak: { mode: ['avoid-all', 'css', 'legacy'] },
        };

        // Render PDF directly and trigger download
        setTimeout(() => {
          (window as any)
            .html2pdf()
            .set(opt)
            .from(container)
            .save()
            .then(() => {
              try {
                document.body.removeChild(container);
              } catch (e) {}
              resolve(true);
            })
            .catch((err: any) => {
              console.error('html2pdf conversion error:', err);
              try {
                document.body.removeChild(container);
              } catch (e) {}
              resolve(false);
            });
        }, 300);
      } catch (e) {
        console.error('executeHtml2Pdf error:', e);
        resolve(false);
      }
    };

    if ((window as any).html2pdf) {
      executeHtml2Pdf();
    } else {
      const script = document.createElement('script');
      script.src = 'https://cdnjs.cloudflare.com/ajax/libs/html2pdf.js/0.10.1/html2pdf.bundle.min.js';
      script.onload = () => executeHtml2Pdf();
      script.onerror = () => resolve(false);
      document.head.appendChild(script);
    }
  });
}

/**
 * Download a PDF matching the exact print preview layout
 */
export async function downloadPdfReport(params: ExportReportParams): Promise<boolean> {
  const { endpoint, title, subtitle, columns, data, totals, fromDate, toDate } = params;

  if (!data || data.length === 0) {
    if (typeof window !== 'undefined') {
      window.alert('No records available to export.');
    } else {
      Alert.alert('No Data', 'No records available to export.');
    }
    return false;
  }

  const formattedColumns = standardizeColumns(columns);
  const safeTitle = (title || 'Report').replace(/[^a-zA-Z0-9_-]/g, '_');

  const exportPayload = {
    title,
    subtitle: subtitle || '',
    columns: formattedColumns,
    data,
    totals: totals || {},
    fromDate,
    toDate,
  };

  try {
    // 1. Try local server first if on web (local machine has desktop Chrome/Edge for exact PDF vector rendering)
    let res: Response | null = null;
    if (Platform.OS === 'web') {
      try {
        const localRes = await fetch(`http://localhost:3000/api/${endpoint}/export-pdf`, {
          method: 'POST',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify(exportPayload),
        });
        if (localRes.ok) {
          res = localRes;
        }
      } catch (e) {
        // Local server not available, fall through
      }
    }

    // 2. If local server not available or failed, try configured API_BASE_URL
    if (!res || !res.ok) {
      try {
        const primaryRes = await fetch(`${API_BASE_URL}/${endpoint}/export-pdf`, {
          method: 'POST',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify(exportPayload),
        });
        if (primaryRes.ok) {
          res = primaryRes;
        }
      } catch (e) {
        console.warn('Primary export-pdf fetch failed:', e);
      }
    }

    // If server provided the binary PDF buffer
    if (res && res.ok) {
      const blob = await res.blob();
      if (Platform.OS === 'web' && typeof window !== 'undefined') {
        const url = URL.createObjectURL(blob);
        const link = document.createElement('a');
        link.href = url;
        link.setAttribute('download', `${safeTitle}.pdf`);
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
        setTimeout(() => URL.revokeObjectURL(url), 2000);
        return true;
      } else {
        Alert.alert('PDF Export', 'PDF generated successfully.');
        return true;
      }
    }

    // 3. Direct client-side PDF download fallback: fetch exact print HTML and download PDF directly
    if (Platform.OS === 'web' && typeof window !== 'undefined') {
      let htmlRes: Response | null = null;
      try {
        htmlRes = await fetch(`${API_BASE_URL}/${endpoint}/download-html`, {
          method: 'POST',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify({
            ...exportPayload,
            showPrintButton: false,
          }),
        });
      } catch (e) {}

      if (!htmlRes || !htmlRes.ok) {
        try {
          htmlRes = await fetch(`http://localhost:3000/api/${endpoint}/download-html`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
              ...exportPayload,
              showPrintButton: false,
            }),
          });
        } catch (e) {}
      }

      if (htmlRes && htmlRes.ok) {
        const htmlContent = await htmlRes.text();
        const success = await downloadClientSidePdf(htmlContent, `${safeTitle}.pdf`);
        if (success) return true;
      }
    }

    throw new Error('Could not generate PDF document.');
  } catch (err: any) {
    console.error('PDF Export Error:', err);
    if (Platform.OS === 'web' && typeof window !== 'undefined') {
      window.alert(err.message || 'Failed to export PDF.');
    } else {
      Alert.alert('Export Error', err.message || 'Failed to export PDF.');
    }
    return false;
  }
}

/**
 * Sanitize and prepare report HTML for printing:
 * - Completely removes 'about:blank' and browser default headers/footers (URL, date, title, page numbers)
 * - Forces @page { margin: 0 !important; } so browser print headers/footers are disabled
 * - Ensures empty <title></title> so no document title appears in the header
 * - Injects fast, deduplicated print trigger
 */
export function sanitizeReportHtmlForPrint(htmlContent: string, autoPrint: boolean = true): string {
  let sanitized = htmlContent;

  // 1. Remove/empty title tag to prevent title from rendering in print header
  sanitized = sanitized.replace(/<title>[\s\S]*?<\/title>/gi, '<title></title>');

  // 2. Inject global zero-margin @page rule to suppress about:blank, URL, date, and page numbers
  const suppressBrowserHeaderFooter = `
<style id="suppress-browser-header-footer">
  @page {
    margin: 0 !important;
  }
  @media print {
    @page {
      margin: 0 !important;
    }
    html, body {
      margin: 0 !important;
      padding: 0 !important;
      background: #fff !important;
      -webkit-print-color-adjust: exact !important;
      print-color-adjust: exact !important;
    }
    /* Ensure page-sheet provides nice content margins without triggering browser header/footer */
    .page-sheet {
      padding: 8mm 10mm !important;
      margin: 0 !important;
      box-sizing: border-box !important;
    }
  }
</style>
`;

  if (sanitized.includes('</head>')) {
    sanitized = sanitized.replace('</head>', `${suppressBrowserHeaderFooter}</head>`);
  } else {
    sanitized = `${suppressBrowserHeaderFooter}${sanitized}`;
  }

  // 3. Inject reliable, deduplicated auto-print script
  if (autoPrint) {
    const printScript = `
<script>
  (function() {
    var printed = false;
    function doPrint() {
      if (printed) return;
      printed = true;
      try {
        document.title = "";
        window.focus();
        window.print();
      } catch(e) {
        console.error('Print error:', e);
      }
    }
    if (document.readyState === 'complete' || document.readyState === 'interactive') {
      setTimeout(doPrint, 50);
    } else {
      window.addEventListener('DOMContentLoaded', function() { setTimeout(doPrint, 50); });
      window.addEventListener('load', function() { setTimeout(doPrint, 50); });
    }
  })();
</script></body>`;
    sanitized = sanitized.replace('</body>', printScript);
  }

  return sanitized;
}

/**
 * Common helper to open clean print window or iframe without about:blank or browser headers/footers
 */
export function openPrintWindow(htmlContent: string, autoPrint: boolean = true, existingWin?: Window | null): void {
  if (typeof window === 'undefined') return;

  const sanitized = sanitizeReportHtmlForPrint(htmlContent, autoPrint);

  let win = existingWin;
  if (!win) {
    win = window.open('', '_blank');
  }

  if (win) {
    win.document.open();
    win.document.write(sanitized);
    try {
      win.document.title = '';
    } catch (e) {}
    win.document.close();
  } else {
    const blob = new Blob([sanitized], { type: 'text/html;charset=utf-8;' });
    const url = URL.createObjectURL(blob);
    window.open(url, '_blank');
  }
}

/**
 * Open Print dialog or preview tab
 */
export async function printHtmlReport(params: ExportReportParams, autoPrint: boolean = true): Promise<boolean> {
  const { endpoint, title, subtitle, columns, data, totals, fromDate, toDate } = params;

  if (!data || data.length === 0) {
    if (typeof window !== 'undefined') {
      window.alert('No records available to print.');
    } else {
      Alert.alert('No Data', 'No records available to print.');
    }
    return false;
  }

  const formattedColumns = standardizeColumns(columns);

  let win: Window | null = null;
  if (Platform.OS === 'web' && typeof window !== 'undefined') {
    win = window.open('', '_blank');
  }

  try {
    const res = await fetch(`${API_BASE_URL}/${endpoint}/download-html`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({
        title,
        subtitle: subtitle || '',
        columns: formattedColumns,
        data,
        totals: totals || {},
        fromDate,
        toDate,
        showPrintButton: false,
      }),
    });

    if (!res.ok) {
      throw new Error('Failed to generate print layout');
    }

    const htmlContent = await res.text();

    if (Platform.OS === 'web' && typeof window !== 'undefined') {
      openPrintWindow(htmlContent, autoPrint, win);
      return true;
    } else {
      Alert.alert('Print Report', 'Report layout generated.');
      return true;
    }
  } catch (err: any) {
    if (win) win.close();
    console.error('Print Report Error:', err);
    if (typeof window !== 'undefined') {
      window.alert(err.message || 'Failed to print report.');
    } else {
      Alert.alert('Print Error', err.message || 'Failed to print report.');
    }
    return false;
  }
}
