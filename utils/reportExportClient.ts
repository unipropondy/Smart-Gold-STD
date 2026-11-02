import { Platform, Alert } from 'react-native';
import { API_BASE_URL } from '@/constants/api';
import { formatColumnHeader } from './formatColumnHeader';

export interface ExportReportParams {
  endpoint: string; // e.g. 'inventoryreports', 'salesreports'
  title: string;
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
  const { endpoint, title, columns, data, totals, fromDate, toDate } = params;

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
 * Download a PDF matching the exact print preview layout
 */
export async function downloadPdfReport(params: ExportReportParams): Promise<boolean> {
  const { endpoint, title, columns, data, totals, fromDate, toDate } = params;

  if (!data || data.length === 0) {
    Alert.alert('No Data', 'No records available to export.');
    return false;
  }

  const formattedColumns = standardizeColumns(columns);

  try {
    const res = await fetch(`${API_BASE_URL}/${endpoint}/export-pdf`, {
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
      throw new Error('Failed to generate PDF document');
    }

    const blob = await res.blob();
    const safeTitle = (title || 'Report').replace(/[^a-zA-Z0-9_-]/g, '_');

    if (Platform.OS === 'web' && typeof window !== 'undefined') {
      const url = URL.createObjectURL(blob);
      const link = document.createElement('a');
      link.href = url;
      link.setAttribute('download', `${safeTitle}.pdf`);
      document.body.appendChild(link);
      link.click();
      document.body.removeChild(link);
      setTimeout(() => URL.revokeObjectURL(url), 1000);
      return true;
    } else {
      Alert.alert('PDF Export', 'PDF generated successfully.');
      return true;
    }
  } catch (err: any) {
    console.error('PDF Export Error:', err);
    Alert.alert('Export Error', err.message || 'Failed to export PDF.');
    return false;
  }
}

/**
 * Open Print dialog or preview tab
 */
export async function printHtmlReport(params: ExportReportParams, autoPrint: boolean = true): Promise<boolean> {
  const { endpoint, title, columns, data, totals, fromDate, toDate } = params;

  if (!data || data.length === 0) {
    Alert.alert('No Data', 'No records available to print.');
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

    let htmlContent = await res.text();
    if (autoPrint) {
      htmlContent = htmlContent.replace(
        '</body>',
        '<script>window.addEventListener("load", function(){ setTimeout(function(){ window.print(); }, 500); });</script></body>'
      );
    }

    if (Platform.OS === 'web' && typeof window !== 'undefined') {
      if (win) {
        win.document.open();
        win.document.write(htmlContent);
        win.document.close();
      } else {
        const blob = new Blob([htmlContent], { type: 'text/html;charset=utf-8;' });
        const url = URL.createObjectURL(blob);
        const newTab = window.open(url, '_blank');
        if (!newTab) {
          const link = document.createElement('a');
          link.href = url;
          link.setAttribute('download', `${(title || 'Report').replace(/[^a-zA-Z0-9_-]/g, '_')}.html`);
          document.body.appendChild(link);
          link.click();
          document.body.removeChild(link);
        }
      }
      return true;
    } else {
      Alert.alert('Print Report', 'Report layout generated.');
      return true;
    }
  } catch (err: any) {
    if (win) win.close();
    console.error('Print Report Error:', err);
    Alert.alert('Print Error', err.message || 'Failed to print report.');
    return false;
  }
}

