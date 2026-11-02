/**
 * Server-side Date Validation Helper for Report Routes
 */

function parseServerDate(val) {
  if (!val) return null;
  const s = String(val).trim();
  if (!s) return null;

  if (s.includes("/")) {
    const parts = s.split("/");
    if (parts.length === 3) {
      const d = parseInt(parts[0], 10);
      const m = parseInt(parts[1], 10) - 1;
      const y = parseInt(parts[2], 10);
      const dt = new Date(y, m, d);
      return isNaN(dt.getTime()) ? null : dt;
    }
  }

  if (s.includes("-")) {
    const parts = s.split("-");
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

function validateBackendDates(fromDateVal, toDateVal) {
  const today = new Date();
  today.setHours(23, 59, 59, 999);

  const fromDate = parseServerDate(fromDateVal);
  const toDate = parseServerDate(toDateVal);

  if (fromDate && fromDate.getTime() > today.getTime()) {
    return {
      isValid: false,
      message: "From Date cannot be in the future. Please select a date on or before today.",
    };
  }

  if (toDate && toDate.getTime() > today.getTime()) {
    return {
      isValid: false,
      message: "To Date cannot be in the future. Please select a date on or before today.",
    };
  }

  if (fromDate && toDate && fromDate.getTime() > toDate.getTime()) {
    return {
      isValid: false,
      message: "From Date cannot be later than To Date.",
    };
  }

  return { isValid: true };
}

module.exports = {
  validateBackendDates,
};

