/**
 * Date validation and formatting utilities for SmartGold reports
 */

// Format a Date or date string to YYYY-MM-DD
export function getTodayIso(): string {
  const now = new Date();
  const year = now.getFullYear();
  const month = String(now.getMonth() + 1).padStart(2, '0');
  const day = String(now.getDate()).padStart(2, '0');
  return `${year}-${month}-${day}`;
}

// Format a Date or date string to DD/MM/YYYY
export function getTodayDisp(): string {
  const now = new Date();
  const day = String(now.getDate()).padStart(2, '0');
  const month = String(now.getMonth() + 1).padStart(2, '0');
  const year = now.getFullYear();
  return `${day}/${month}/${year}`;
}

// Parse string (either YYYY-MM-DD or DD/MM/YYYY) to Date
export function parseDateInput(val: string | null | undefined): Date | null {
  if (!val) return null;
  const s = String(val).trim();
  if (!s) return null;

  if (s.includes('/')) {
    const parts = s.split('/');
    if (parts.length === 3) {
      const d = parseInt(parts[0], 10);
      const m = parseInt(parts[1], 10) - 1;
      const y = parseInt(parts[2], 10);
      const dt = new Date(y, m, d);
      return isNaN(dt.getTime()) ? null : dt;
    }
  }

  if (s.includes('-')) {
    const parts = s.split('-');
    if (parts.length === 3) {
      const y = parseInt(parts[0], 10);
      const m = parseInt(parts[1], 10) - 1;
      const d = parseInt(parts[2], 10);
      const dt = new Date(y, m, d);
      return isNaN(dt.getTime()) ? null : dt;
    }
  }

  const dt = new Date(s);
  return isNaN(dt.getTime()) ? null : dt;
}

// Convert DD/MM/YYYY or YYYY-MM-DD to YYYY-MM-DD for <input type="date">
export function toIsoDate(val: string | null | undefined): string {
  if (!val) return '';
  const s = String(val).trim();
  if (s.includes('/')) {
    const parts = s.split('/');
    if (parts.length === 3) {
      return `${parts[2]}-${parts[1].padStart(2, '0')}-${parts[0].padStart(2, '0')}`;
    }
  }
  return s;
}

// Convert YYYY-MM-DD to DD/MM/YYYY
export function toDispDate(val: string | null | undefined): string {
  if (!val) return '';
  const s = String(val).trim();
  if (s.includes('-')) {
    const parts = s.split('-');
    if (parts.length === 3) {
      return `${parts[2].padStart(2, '0')}/${parts[1].padStart(2, '0')}/${parts[0]}`;
    }
  }
  return s;
}

export interface DateRangeValidationResult {
  isValid: boolean;
  errorMessage?: string;
}

/**
 * Validate that:
 * 1. From Date is not in the future (after today)
 * 2. To Date is not in the future (after today)
 * 3. From Date is not later than To Date
 */
export function validateReportDateRange(
  fromDateStr: string | null | undefined,
  toDateStr: string | null | undefined
): DateRangeValidationResult {
  const today = new Date();
  today.setHours(23, 59, 59, 999); // end of today

  const fromDate = parseDateInput(fromDateStr);
  const toDate = parseDateInput(toDateStr);

  if (fromDate && fromDate.getTime() > today.getTime()) {
    return {
      isValid: false,
      errorMessage: 'From Date cannot be in the future. Please select a date on or before today.',
    };
  }

  if (toDate && toDate.getTime() > today.getTime()) {
    return {
      isValid: false,
      errorMessage: 'To Date cannot be in the future. Please select a date on or before today.',
    };
  }

  if (fromDate && toDate && fromDate.getTime() > toDate.getTime()) {
    return {
      isValid: false,
      errorMessage: 'From Date cannot be later than To Date.',
    };
  }

  return { isValid: true };
}

