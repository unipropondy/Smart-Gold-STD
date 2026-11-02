const path = require("path");
const fs = require("fs");
const { poolPromise } = require("../db");

// Load Unipro logo base64 once
let cachedLogoBase64 = null;
const getLogoBase64 = () => {
  if (cachedLogoBase64 !== null) return cachedLogoBase64;
  try {
    const logoFilePath = path.join(__dirname, "../unipro_logo_base64.txt");
    if (fs.existsSync(logoFilePath)) {
      cachedLogoBase64 = fs.readFileSync(logoFilePath, "utf8").trim();
    } else {
      cachedLogoBase64 = "";
    }
  } catch (e) {
    cachedLogoBase64 = "";
  }
  return cachedLogoBase64;
};

// Date formatting helper DD/MM/YYYY
const formatDateDisp = (d) => {
  if (!d) return "";
  if (typeof d === "string" && d.includes("/") && d.length <= 10) return d;
  const dt = new Date(d);
  if (isNaN(dt.getTime())) return String(d);
  const day = String(dt.getDate()).padStart(2, "0");
  const month = String(dt.getMonth() + 1).padStart(2, "0");
  const year = dt.getFullYear();
  return `${day}/${month}/${year}`;
};

// Reusable professional column-header formatting: ALL CAPITAL LETTERS & SHORT ABBREVIATIONS ON ONE LINE
const formatColumnHeader = (colName) => {
  if (!colName) return "";
  const labelMap = {
    // Inventory & Items
    InventoryCode: "INV. CODE",
    Inventorycode: "INV. CODE",
    inventoryCode: "INV. CODE",
    "Inventory Code": "INV. CODE",
    ItemCode: "ITEM CODE",
    "Item Code": "ITEM CODE",
    ItemName: "ITEM NAME",
    "Item Name": "ITEM NAME",
    Description: "DESC.",
    description: "DESC.",
    Category: "CATEGORY",
    CategoryCode: "CAT. CODE",
    "Category Code": "CAT. CODE",
    CategoryName: "CAT. NAME",
    "Category Name": "CAT. NAME",
    ClassCode: "CLASS CODE",
    "Class Code": "CLASS CODE",
    ClassName: "CLASS NAME",
    "Class Name": "CLASS NAME",
    ClassType: "CLASS TYPE",
    "Class Type": "CLASS TYPE",
    CatalogueCode: "CATALOGUE CODE",
    "Catalogue Code": "CATALOGUE CODE",
    CatalogueName: "CATALOGUE NAME",
    "Catalogue Name": "CATALOGUE NAME",
    SizeCode: "SIZE CODE",
    "Size Code": "SIZE CODE",
    SizeName: "SIZE NAME",
    "Size Name": "SIZE NAME",
    TrayId: "TRAY ID",
    "Tray ID": "TRAY ID",
    TrayName: "TRAY NAME",
    "Tray Name": "TRAY NAME",

    // Weights, Quantities & Counts
    Weight: "WT.",
    GrossWeight: "GROSS WT.",
    "Gross Weight": "GROSS WT.",
    NetWeight: "NET WT.",
    "Net Weight": "NET WT.",
    TotalWeight: "TOTAL WT.",
    totalWeight: "TOTAL WT.",
    "Total Weight": "TOTAL WT.",
    StoneWt: "STONE WT.",
    "Stone Wt": "STONE WT.",
    Pcs: "PCS",
    Pieces: "PCS",
    TotalPcs: "TOTAL PCS",
    "Total Pcs": "TOTAL PCS",
    Quantity: "TOTAL QTY.",
    Qty: "TOTAL QTY.",
    TotalQuantity: "TOTAL QTY.",
    "Total Quantity": "TOTAL QTY.",
    TotalQty: "TOTAL QTY.",
    "Total Qty": "TOTAL QTY.",
    Items: "ITEMS",
    TotalItems: "TOTAL ITEMS",

    // Costs, Amounts, Sales & Profits
    Cost: "COST",
    TotalCost: "TOTAL COST",
    "Total Cost": "TOTAL COST",
    CostPerGram: "COST / G",
    "Cost / Gram": "COST / G",
    PurchaseCost: "PURCH. COST",
    purchaseCost: "PURCH. COST",
    "Purchase Cost": "PURCH. COST",
    PurchaseAmount: "PURCH. AMT.",
    purchaseAmount: "PURCH. AMT.",
    "Purchase Amount": "PURCH. AMT.",
    SellRate: "SELL RATE",
    SalesAmount: "SALES AMT.",
    salesAmount: "SALES AMT.",
    "Sales Amount": "SALES AMT.",
    TotalSales: "TOTAL SALES",
    "Total Sales": "TOTAL SALES",
    TotalAmount: "TOTAL AMT.",
    "Total Amount": "TOTAL AMT.",
    NetAmount: "NET AMT.",
    netAmount: "NET AMT.",
    "Net Amount": "NET AMT.",
    GrossAmount: "GROSS AMT.",
    "Gross Amount": "GROSS AMT.",
    Profit: "PROFIT",
    profit: "PROFIT",
    GrossProfit: "GROSS PROFIT",
    "Gross Profit": "GROSS PROFIT",
    ProfitAmount: "PROFIT AMT.",
    "Profit Amount": "PROFIT AMT.",
    ProfitMargin: "PROFIT %",
    "Profit Margin": "PROFIT %",
    LossAmount: "LOSS AMT.",
    "Loss Amount": "LOSS AMT.",
    StockValue: "STOCK VALUE",
    "Stock Value": "STOCK VALUE",

    // Balances
    ClosingBalance: "CLOSING BAL.",
    "Closing Balance": "CLOSING BAL.",
    OpeningBalance: "OPENING BAL.",
    "Opening Balance": "OPENING BAL.",
    BalanceAmount: "BAL. AMT.",
    "Balance Amount": "BAL. AMT.",
    PaidAmount: "PAID AMT.",
    "Paid Amount": "PAID AMT.",

    // Dates & Transactions
    InvoiceDate: "TRANS. DATE",
    TransactionDate: "TRANS. DATE",
    "Transaction Date": "TRANS. DATE",
    Date: "DATE",
    OrderDate: "ORDER DATE",
    "Order Date": "ORDER DATE",
    OrderDateTime: "ORDER DATE",
    CancelDate: "CANCEL DATE",
    "Cancel Date": "CANCEL DATE",
    Year: "YEAR",
    Month: "MONTH",
    InvoiceNo: "INV. NO.",
    "Invoice No": "INV. NO.",
    "Invoice Number": "INV. NO.",
    BillNo: "BILL NO.",
    "Bill No": "BILL NO.",
    BillNumber: "BILL NO.",
    "Bill Number": "BILL NO.",
    RefNo: "REF. NO.",
    "Ref No": "REF. NO.",
    DocNo: "DOC. NO.",
    "Doc No": "DOC. NO.",
    OrderNo: "ORDER NO.",
    "Order No": "ORDER NO.",
    OrderId: "ORDER ID",
    "Order ID": "ORDER ID",
    VoucherNo: "VOUCHER NO.",
    "Voucher No": "VOUCHER NO.",
    NoOfTransaction: "NO. OF TRANS.",
    NoOfTransactions: "NO. OF TRANS.",
    "No of Transactions": "NO. OF TRANS.",
    "Number of Transactions": "NO. OF TRANS.",
    BillCount: "BILL COUNT",
    "Bill Count": "BILL COUNT",

    // Customers, Suppliers & Representatives
    CustomerCode: "CUST. CODE",
    "Customer Code": "CUST. CODE",
    CustomerName: "CUST. NAME",
    "Customer Name": "CUST. NAME",
    CustomerType: "CUST. TYPE",
    "Customer Type": "CUST. TYPE",
    SupplierCode: "SUPP. CODE",
    "Supplier Code": "SUPP. CODE",
    SupplierName: "SUPP. NAME",
    "Supplier Name": "SUPP. NAME",
    SalesPersonCode: "SALESMAN",
    "Sales Person Code": "SALESMAN",
    SalesManCode: "SALESMAN",
    "Salesman Code": "SALESMAN",
    SalesPersonName: "SALESMAN NAME",
    "Sales Person Name": "SALESMAN NAME",
    SalesManName: "SALESMAN NAME",
    "Salesman Name": "SALESMAN NAME",
    LocationCode: "LOC. CODE",
    "Location Code": "LOC. CODE",
    LocationName: "LOC. NAME",
    "Location Name": "LOC. NAME",
    Location: "LOC.",
    Mobile: "MOBILE NO.",
    Phone: "PHONE NO.",
    TotalRewardPoints: "REWARD PTS.",
    TotalRedeemedPoints: "REDEEMED PTS.",
    BalancePoints: "BAL. PTS.",

    // Taxes, Discounts & Paymodes
    Tax: "GST",
    TotalTax: "GST",
    "Total Tax": "GST",
    GST: "GST",
    SMC: "SMC",
    PMC: "PMC",
    TotalSMC: "TOTAL SMC",
    TotalPMC: "TOTAL PMC",
    Discount: "DISC. AMT.",
    DiscountAmount: "DISC. AMT.",
    "Discount Amount": "DISC. AMT.",
    TotalDiscount: "TOTAL DISC.",
    "Total Discount": "TOTAL DISC.",
    ServiceCharge: "SVC. CHRG.",
    "Service Charge": "SVC. CHRG.",
    PaymentMode: "PAYMODE",
    "Payment Mode": "PAYMODE",
    PayMode: "PAYMODE",
    CreditCardAmount: "CC. AMT.",
    "Credit Card Amount": "CC. AMT.",
    Cash: "CASH",
    Nets: "NETS",
    Paynow: "PAYNOW",
    PayNow: "PAYNOW",
    UPI: "UPI",
    Credit: "CREDIT",
    Remarks: "REMARKS"
  };

  const clean = colName.trim();
  if (labelMap[clean]) return labelMap[clean];
  if (labelMap[colName]) return labelMap[colName];

  // Regex replacement for common word patterns to standardize abbreviations and force UPPERCASE
  let processed = clean
    .replace(/([a-z])([A-Z])/g, '$1 $2')
    .replace(/_/g, ' ')
    .toUpperCase();

  // Apply standard phrase replacements
  processed = processed
    .replace(/\bINVENTORY CODE\b/g, 'INV. CODE')
    .replace(/\bINVOICE NUMBER\b/g, 'INV. NO.')
    .replace(/\bINVOICE NO\b/g, 'INV. NO.')
    .replace(/\bCUSTOMER NAME\b/g, 'CUST. NAME')
    .replace(/\bCUSTOMER CODE\b/g, 'CUST. CODE')
    .replace(/\bSUPPLIER NAME\b/g, 'SUPP. NAME')
    .replace(/\bSUPPLIER CODE\b/g, 'SUPP. CODE')
    .replace(/\bDESCRIPTION\b/g, 'DESC.')
    .replace(/\bTOTAL QUANTITY\b/g, 'TOTAL QTY.')
    .replace(/\bTOTAL WEIGHT\b/g, 'TOTAL WT.')
    .replace(/\bTOTAL AMOUNT\b/g, 'TOTAL AMT.')
    .replace(/\bNET AMOUNT\b/g, 'NET AMT.')
    .replace(/\bPURCHASE AMOUNT\b/g, 'PURCH. AMT.')
    .replace(/\bPURCHASE COST\b/g, 'PURCH. COST')
    .replace(/\bSALES AMOUNT\b/g, 'SALES AMT.')
    .replace(/\bTRANSACTION DATE\b/g, 'TRANS. DATE')
    .replace(/\bNUMBER OF TRANSACTIONS\b/g, 'NO. OF TRANS.')
    .replace(/\bNO OF TRANSACTIONS\b/g, 'NO. OF TRANS.')
    .replace(/\bNO OF TRANSACTION\b/g, 'NO. OF TRANS.')
    .replace(/\bDISCOUNT AMOUNT\b/g, 'DISC. AMT.')
    .replace(/\bCLOSING BALANCE\b/g, 'CLOSING BAL.')
    .replace(/\bOPENING BALANCE\b/g, 'OPENING BAL.')
    .replace(/\bBALANCE AMOUNT\b/g, 'BAL. AMT.')
    .replace(/\bPAID AMOUNT\b/g, 'PAID AMT.');

  return processed;
};

// Alignment rules
const getColumnAlignment = (colName) => {
  if (!colName) return "left";
  const c = colName.toLowerCase().replace(/[\s_\-\.]/g, "");
  if (
    c === "date" || c === "invoicedate" || c === "orderdate" || c === "orderdatetime" ||
    c === "canceldate" || c === "year" || c === "month" || c === "hour" || c === "time" ||
    c === "canceltime" || c === "fromdate" || c === "todate" || c === "eoddate"
  ) {
    return "center";
  }
  if (
    c === "qty" || c === "quantity" || c === "qtysold" || c === "sold" ||
    c === "amount" || c === "amt" || c === "salesamount" || c === "nettotal" || c === "subtotal" ||
    c === "netamount" || c === "totalamount" || c === "lineamount" || c === "itemamount" ||
    c === "totalrevenue" || c === "voidqty" || c === "voidamount" || c === "vqty" || c === "vamt" ||
    c === "pax" || c === "totalpax" || c === "bills" || c === "noofbills" || c === "totalbills" ||
    c === "billcount" || c === "discount" || c === "servicecharge" || c === "totaltax" ||
    c === "tax" || c === "gst" || c === "roundoff" || c === "rounding" || c === "rndoff" ||
    c === "tips" || c === "foc" || c === "netsales" || c === "itemsales" || c === "itemdisc" ||
    c === "billdisc" || c === "contributionpct" || c === "salespct" || c === "contribution%" ||
    c === "cash" || c === "nets" || c === "paynow" || c === "upi" || c === "member" ||
    c === "credit" || c === "online" || c === "yeahpaypaynow" || c === "yeahpaycard" ||
    c === "others" || c === "avgbill" || c === "avgpax" || c === "avg/bill" || c === "avg/pax" ||
    c === "sales" || c === "rnd" || c === "digital" || c === "dishprice" || c === "rank" ||
    c === "cancelledamount" || c === "subtot" || c === "disc" || c === "svc" ||
    c === "taxableamount" || c === "totaldiscount" || c === "totalitemdisc" ||
    c === "totalbilldisc" || c === "itemdiscount" || c === "billdiscount" ||
    c.includes("items") || c.includes("pcs") || c.includes("wt") || c.includes("weight") ||
    c.includes("pmc") || c.includes("smc") || c.includes("cost") || c.includes("points")
  ) {
    return "right";
  }
  return "left";
};

const getIsTextCol = (c) => {
  if (!c) return false;
  const lower = c.toLowerCase().replace(/[\s_\-\.]/g, "");
  if (
    lower === "discountid" || lower === "orderid" || lower === "oderid" ||
    lower === "billno" || lower === "billnumber" || lower === "orderno" ||
    lower === "orderdatetime" || lower === "categorycode" || lower === "classcode" ||
    lower === "customercode" || lower === "suppliercode" || lower === "locationcode" ||
    lower === "mobile" || lower === "phone"
  ) {
    return true;
  }
  const numericKeywords = [
    "noofbills", "billcount", "totalbills", "qty", "amount", "total", "subtotal", "nettotal", "discount",
    "servicecharge", "tax", "bills", "pax", "sales", "foc", "tips", "rnd", "roundoff",
    "cash", "cards", "cheque", "ledger", "nektar", "voucher", "ent", "collection",
    "svc", "disc", "itemamount", "totalamount", "discountamount", "lineamount",
    "price", "rate", "percentage", "openingstock", "closingstock", "pqty", "prqty", "sqty", "srqty",
    "nets", "paynow", "upi", "member", "credit", "online", "yeahpay", "others", "master", "visa",
    "schrg", "chrg", "gst", "sold", "digital", "items", "pcs", "wt", "weight", "pmc", "smc", "cost", "points"
  ];
  return !numericKeywords.some((keyword) => lower.includes(keyword));
};

const getIsCountCol = (c) => {
  if (!c) return false;
  const lower = c.toLowerCase().replace(/[\s_\-]/g, "");
  if (
    lower.includes("discount") || lower.includes("disc") || lower.includes("wt") ||
    lower.includes("weight") || lower.includes("amount") || lower.includes("cost") ||
    lower.includes("pmc") || lower.includes("smc") || lower.includes("rate") || lower.includes("price")
  ) {
    return false;
  }
  return (
    lower.includes("bills") || lower.includes("qty") || lower.includes("pax") ||
    lower === "sold" || lower === "rank" || lower.includes("count") ||
    lower.includes("transactions") || lower.includes("items") || lower.includes("pcs") ||
    lower.includes("points")
  );
};

const formatNumber = (val, isCount = false, colKey = "") => {
  if (val === null || val === undefined || val === "") return "";
  const num = parseFloat(val);
  if (isNaN(num)) return String(val);
  const k = (colKey || "").toLowerCase();
  if (k.includes("wt") || k.includes("weight")) {
    return num.toFixed(3);
  }
  return isCount
    ? Math.round(num).toLocaleString("en-US")
    : num.toLocaleString("en-US", { minimumFractionDigits: 2, maximumFractionDigits: 2 });
};

const formatVal = (val, format, key) => {
  if (val === null || val === undefined || val === "") return "";
  if (format === "date") return formatDateDisp(val);
  if (format === "currency") return Number(val).toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 });
  if (format === "decimal2") return Number(val).toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 });
  if (format === "decimal3") return Number(val).toFixed(3);
  const isText = getIsTextCol(key);
  if (isText) {
    if (typeof val === "string" && val.includes("\n")) {
      return val.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;").replace(/\n/g, "<br/>");
    }
    return String(val);
  }
  const isCount = getIsCountCol(key);
  const k = (key || "").toLowerCase();
  if (k.includes("pmc") || k.includes("smc") || k.includes("cost") || k.includes("amount") || k.includes("price") || k.includes("sales") || k.includes("total")) {
    const num = parseFloat(val);
    return !isNaN(num) ? `$ ${num.toLocaleString("en-US", { minimumFractionDigits: 2, maximumFractionDigits: 2 })}` : String(val);
  }
  return formatNumber(val, isCount, key);
};

/**
 * Generate standard A4 Report HTML
 */
async function generateReportHtml({
  title = "REPORT",
  columns = [],
  data = [],
  totals = {},
  companyInfo = null,
  fromDate = null,
  toDate = null,
  pool = null,
  showPrintButton = false,
  showStatCards = false,
}) {
  // Query company dynamically from tblCompany if not supplied or incomplete
  let comp = companyInfo || {};
  if (!comp.CompanyName || comp.CompanyName === "SMART GOLD" || !comp.Phone) {
    try {
      const p = pool || (await poolPromise);
      const compRes = await p.request().query(
        "SELECT TOP 1 RTRIM(LTRIM(Name)) AS Name, RTRIM(LTRIM(Address1)) AS Address1, RTRIM(LTRIM(Address2)) AS Address2, RTRIM(LTRIM(City)) AS City, RTRIM(LTRIM(Country)) AS Country, RTRIM(LTRIM(Phone)) AS Phone FROM tblCompany WITH (NOLOCK)"
      );
      if (compRes.recordset && compRes.recordset.length > 0) {
        const r = compRes.recordset[0];
        comp = {
          CompanyName: r.Name || comp.CompanyName || "SMART",
          Address: [r.Address1, r.Address2, r.City, r.Country].filter(Boolean).join(", "),
          Phone: r.Phone ? r.Phone.trim() : "",
        };
      }
    } catch (e) {
      console.warn("Could not query tblCompany:", e.message);
    }
  }

  const compName = comp.CompanyName || "SMART";
  const compPhone = comp.Phone ? comp.Phone : "";
  const uniproLogoBase64 = getLogoBase64();

  // Singapore Date & Time
  const now = new Date();
  const sgDateStr = now.toLocaleDateString("en-GB", {
    timeZone: "Asia/Singapore",
    day: "2-digit",
    month: "2-digit",
    year: "numeric",
  });
  const sgTimeStr = now.toLocaleTimeString("en-US", {
    timeZone: "Asia/Singapore",
    hour: "2-digit",
    minute: "2-digit",
    hour12: true,
  });

  // Clean title by removing any embedded date range (e.g. From DD/MM/YYYY To DD/MM/YYYY)
  const cleanTitle = (title || "REPORT")
    .replace(/\s+(From\s+\d{2}[\/\-]\d{2}[\/\-]\d{4}\s+To\s+\d{2}[\/\-]\d{2}[\/\-]\d{4})/i, "")
    .replace(/\s+From\s+\d{2}[\/\-]\d{2}[\/\-]\d{4}/i, "")
    .trim();

  const datePeriodText =
    fromDate && toDate
      ? `Period: ${formatDateDisp(fromDate)} to ${formatDateDisp(toDate)} | Printed: ${sgDateStr}, ${sgTimeStr}`
      : `Printed: ${sgDateStr}, ${sgTimeStr}`;

  // Stat Card 1: Total Cost / Revenue / Sales / Amount
  const statTotalCost =
    totals?.totalSales !== undefined
      ? `$ ${Number(totals.totalSales).toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 })}`
      : totals?.totalAmount !== undefined
      ? `$ ${Number(totals.totalAmount).toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 })}`
      : totals?.totalCost !== undefined
      ? `$ ${Number(totals.totalCost).toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 })}`
      : totals?.netSales !== undefined
      ? `$ ${Number(totals.netSales).toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 })}`
      : "-";

  // Stat Card 2: Total Qty / Pcs / Bills
  const statTotalQty =
    totals?.totalQty !== undefined
      ? Number(totals.totalQty).toLocaleString()
      : totals?.totalPcs !== undefined
      ? Number(totals.totalPcs).toLocaleString()
      : totals?.totalBills !== undefined
      ? Number(totals.totalBills).toLocaleString()
      : "-";

  // Stat Card 3: Unique Records / Items / Customers
  const statUniqueItems =
    data && data.length
      ? data.length.toLocaleString()
      : totals?.totalItems !== undefined
      ? Number(totals.totalItems).toLocaleString()
      : totals?.customerCount !== undefined
      ? Number(totals.customerCount).toLocaleString()
      : "0";

  // Stat Card 4: Total Weight or Balance Points or Other
  const statTotalWeight =
    totals?.totalWeight !== undefined
      ? `${Number(totals.totalWeight).toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 3 })} g`
      : totals?.totalNetWeight !== undefined
      ? `${Number(totals.totalNetWeight).toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 3 })} g`
      : totals?.totalBalancePoints !== undefined
      ? `${Number(totals.totalBalancePoints).toLocaleString()} pts`
      : "-";

  return `<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8">
  <title>${title || "Report"}</title>
  <style>
    @page {
      size: A4 portrait;
      margin: 0;
    }
    * {
      box-sizing: border-box;
      -webkit-print-color-adjust: exact !important;
      print-color-adjust: exact !important;
    }
    html, body {
      margin: 0;
      padding: 0;
      background: #fbf9f5;
      font-family: Cambria, Georgia, "Times New Roman", serif;
      color: #3b2d17;
      font-size: 11px;
    }
    .page-sheet {
      width: 210mm;
      min-height: 297mm;
      margin: 15px auto;
      background: #ffffff;
      padding: 12mm 12mm 14mm 12mm;
      border: none;
      box-shadow: none;
      position: relative;
      display: flex;
      flex-direction: column;
    }
    /* HEADER SECTION */
    .header-row {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 6px;
      position: relative;
    }
    .header-left {
      width: 140px;
      display: flex;
      align-items: center;
    }
    .header-left img {
      max-height: 48px;
      max-width: 130px;
      object-fit: contain;
    }
    .header-center {
      flex: 1;
      text-align: center;
      padding: 0 10px;
    }
    .company-title {
      font-family: Cambria, Georgia, serif;
      font-size: 26px;
      font-weight: 700;
      color: #996515;
      letter-spacing: 2px;
      text-transform: uppercase;
      margin-bottom: 4px;
      line-height: 1.1;
    }
    .company-address {
      font-family: Cambria, Georgia, serif;
      font-size: 11px;
      color: #4a3b22;
      font-weight: 700;
      letter-spacing: 0.5px;
      text-transform: uppercase;
    }
    .header-right {
      width: 140px;
    }
    .header-ornament {
      text-align: center;
      margin: 8px 0 12px 0;
      color: #d4a742;
      font-size: 16px;
      position: relative;
    }
    .header-ornament::before,
    .header-ornament::after {
      content: "";
      position: absolute;
      top: 50%;
      width: calc(50% - 20px);
      height: 1.5px;
      background: #d4a742;
    }
    .header-ornament::before {
      left: 0;
    }
    .header-ornament::after {
      right: 0;
    }
    /* REPORT TITLE & PERIOD */
    .report-title-area {
      text-align: center;
      margin-bottom: 12px;
    }
    .report-main-title {
      font-family: Cambria, Georgia, serif;
      font-size: 18px;
      font-weight: 700;
      color: #3b2d17;
      letter-spacing: 1px;
      text-transform: uppercase;
      margin-bottom: 4px;
    }
    .report-period-line {
      font-family: Cambria, Georgia, serif;
      font-size: 11px;
      color: #705832;
      font-weight: 500;
    }
    /* 4 STAT CARDS ROW */
    .stat-cards-row {
      display: grid;
      grid-template-columns: repeat(4, 1fr);
      gap: 10px;
      margin-bottom: 14px;
    }
    .stat-card {
      border: 1.5px solid #d4a742;
      border-radius: 6px;
      padding: 8px 10px;
      text-align: center;
      background: #ffffff;
    }
    .stat-label {
      font-family: Cambria, Georgia, serif;
      font-size: 9.5px;
      font-weight: 700;
      color: #705832;
      letter-spacing: 0.5px;
      text-transform: uppercase;
      margin-bottom: 3px;
    }
    .stat-value {
      font-family: Cambria, Georgia, serif;
      font-size: 14px;
      font-weight: 700;
      color: #996515;
    }
    /* REPORT DATA TABLE */
    .table-container {
      width: 100%;
      flex: 1;
      overflow-x: auto;
      box-sizing: border-box;
    }
    table.report-table {
      width: 100% !important;
      border-collapse: collapse;
      font-family: Cambria, Georgia, serif;
      font-size: 11px;
      border: 0.75px solid #d4a742;
      box-sizing: border-box;
    }
    table.report-table thead th {
      background-color: #996515 !important;
      color: #ffffff !important;
      font-weight: 700 !important;
      font-family: Cambria, Georgia, serif;
      padding: 9px 10px;
      letter-spacing: 0.5px;
      border: 0.75px solid #d4a742;
      font-size: 10.5px;
      line-height: 1.2;
      text-align: center !important;
      box-sizing: border-box;
      white-space: nowrap !important;
      text-transform: uppercase !important;
    }
    table.report-table tbody td {
      padding: 6px 8px;
      color: #3b2d17;
      font-family: Cambria, Georgia, serif;
      border: 0.75px solid #f0d9a0;
      font-size: 11px;
      line-height: 1.25;
      text-align: center !important;
      box-sizing: border-box;
      white-space: nowrap;
    }
    /* ZEBRA STRIPING: Subtle light-cream alternating with Crisp White */
    table.report-table tbody tr:nth-child(even) td {
      background-color: #fffbf2 !important;
    }
    table.report-table tbody tr:nth-child(odd) td {
      background-color: #ffffff !important;
    }
    /* TOTALS ROW */
    table.report-table tfoot tr td,
    table.report-table tbody tr.totals-row td {
      background-color: #fff8e7 !important;
      font-weight: 700 !important;
      font-family: Cambria, Georgia, serif;
      color: #996515 !important;
      border: 0.75px solid #f0d9a0 !important;
      border-top: 0.75px solid #f0d9a0 !important;
      border-bottom: 0.75px solid #f0d9a0 !important;
      padding: 8px 8px;
      font-size: 12px;
      text-align: center !important;
    }
    /* FOOTER */
    .report-footer {
      margin-top: 24px;
      text-align: center;
      padding-top: 10px;
      position: relative;
    }
    .footer-divider {
      width: 100%;
      height: 1.5px;
      background: #d4a742;
      margin-bottom: 12px;
    }
    .footer-gen-text {
      font-family: Cambria, Georgia, serif;
      font-size: 10.5px;
      color: #4a3b22;
      font-weight: 600;
      margin-bottom: 3px;
    }
    .footer-powered {
      font-family: Cambria, Georgia, serif;
      font-size: 11px;
      color: #996515;
      font-weight: 700;
    }
    /* TOOLBAR */
    .print-toolbar {
      position: fixed;
      top: 16px;
      right: 20px;
      z-index: 1000;
      display: flex;
      gap: 10px;
    }
    .btn-action {
      background: #7a5408;
      color: #fff;
      border: none;
      padding: 9px 18px;
      font-family: Cambria, Georgia, serif;
      font-size: 12px;
      font-weight: 700;
      border-radius: 6px;
      cursor: pointer;
      box-shadow: 0 4px 12px rgba(122, 84, 8, 0.25);
    }
    .btn-action:hover {
      background: #946914;
    }
    @media print {
      @page {
        margin: 0;
        size: A4 portrait;
      }
      body {
        background: #fff !important;
        padding: 0 !important;
        margin: 0 !important;
      }
      .print-toolbar {
        display: none !important;
      }
      .page-sheet {
        width: 100% !important;
        min-height: 100vh !important;
        margin: 0 !important;
        padding: 10mm 10mm 10mm 10mm !important;
        border: none !important;
        box-shadow: none !important;
      }
      tr {
        page-break-inside: avoid;
      }
      thead {
        display: table-header-group;
      }
      tfoot {
        display: table-footer-group;
      }
    }
  </style>
</head>
<body>

  

  <div class="page-sheet">
    <!-- HEADER -->
    <div class="header-row">
      <div class="header-left">
        ${
          uniproLogoBase64
            ? `<img src="data:image/png;base64,${uniproLogoBase64}" alt="Unipro" />`
            : `
          <div style="display: flex; align-items: center; gap: 8px;">
            <div style="font-size: 26px; color: #b48218;">◆</div>
            <div>
              <div style="font-family: Cambria, Georgia, serif; font-size: 16px; font-weight: 700; color: #7a5408; line-height: 1;">UNIPRO</div>
              <div style="font-family: Cambria, Georgia, serif; font-size: 8.5px; color: #8c734b; letter-spacing: 0.5px;">SMARTGOLD</div>
            </div>
          </div>
        `
        }
      </div>

      <div class="header-center">
        <div class="company-title">SMART</div>
        <div class="company-address">${comp.Address || "45 KALLANG PUDDING ROAD, #08-02 ALPHA BUILDING, SINGAPORE"}</div>
      </div>

      <div class="header-right"></div>
    </div>

    <div class="header-ornament">◆</div>

    <!-- REPORT TITLE & PERIOD -->
    <div class="report-title-area">
      <div class="report-main-title">${cleanTitle || "REPORT"}</div>
      <div class="report-period-line">${datePeriodText}</div>
    </div>

    ${showStatCards ? `
    <!-- 4 STAT CARDS ROW (RETAINED ONLY WHEN SPECIFIED) -->
    <div class="stat-cards-row">
      <div class="stat-card">
        <div class="stat-label">TOTAL COST / REVENUE</div>
        <div class="stat-value">${statTotalCost}</div>
      </div>
      <div class="stat-card">
        <div class="stat-label">TOTAL QTY / PCS</div>
        <div class="stat-value">${statTotalQty}</div>
      </div>
      <div class="stat-card">
        <div class="stat-label">UNIQUE ITEMS / RECORDS</div>
        <div class="stat-value">${statUniqueItems}</div>
      </div>
      <div class="stat-card">
        <div class="stat-label">TOTAL WEIGHT</div>
        <div class="stat-value">${statTotalWeight}</div>
      </div>
    </div>` : ''}

    <!-- TABLE MATCHING REFERENCE IMAGE COLOR & STRUCTURE -->
    <div class="table-container">
      <table class="report-table">
        <colgroup>
          ${(() => {
            const cols = columns || [];
            const isCustList = cols.some(c => (c.key || c.label || '').toLowerCase().includes('cust.code') || (c.key || c.label || '').toLowerCase().includes('cust code'));
            if (isCustList && cols.length === 5) {
              return `
                <col style="width: 14%;" />
                <col style="width: 20%;" />
                <col style="width: 32%;" />
                <col style="width: 18%;" />
                <col style="width: 16%;" />
              `;
            }
            if (cols.length > 0) {
              const pct = (100 / cols.length).toFixed(2);
              return cols.map(() => `<col style="width: ${pct}%;" />`).join("");
            }
            return "";
          })()}
        </colgroup>
        <thead>
          <tr>
            ${(columns || [])
              .map(
                (c) => `
              <th>${formatColumnHeader(c.label || c.key || "")}</th>
            `
              )
              .join("")}
          </tr>
        </thead>
        <tbody>
          ${
            (() => {
              const isSalesJournalDetail = (cleanTitle || title || "").toLowerCase().includes("detail sales journal");
              if (!isSalesJournalDetail) {
                return (data || [])
                  .map(
                    (row) => `
                  <tr>
                    ${(columns || [])
                      .map(
                        (c) => `
                      <td>
                        ${formatVal(row[c.key], c.format, c.key)}
                      </td>
                    `
                      )
                      .join("")}
                  </tr>
                `
                  )
                  .join("");
              }

              // Group by Date -> InvoiceNo
              const dateGroups = [];
              let currentDateGroup = null;
              let currentInvoiceGroup = null;

              (data || []).forEach((row) => {
                const dateKey = formatDateDisp(row.InvoiceDate);
                const invKey = row.InvoiceNo ? String(row.InvoiceNo).trim() : "";

                if (!currentDateGroup || currentDateGroup.dateKey !== dateKey) {
                  currentDateGroup = { dateKey, invoices: [] };
                  dateGroups.push(currentDateGroup);
                  currentInvoiceGroup = null;
                }

                if (!currentInvoiceGroup || currentInvoiceGroup.invoiceNo !== invKey) {
                  const custCode = row.CustomerCode ? String(row.CustomerCode).trim() : "";
                  const custName = row.CustomerName ? String(row.CustomerName).trim() : "";
                  currentInvoiceGroup = {
                    invoiceNo: invKey,
                    customerLabel: custCode || custName,
                    items: [],
                  };
                  currentDateGroup.invoices.push(currentInvoiceGroup);
                }

                currentInvoiceGroup.items.push(row);
              });

              let htmlOut = "";
              const totalColSpan = (columns || []).length;

              dateGroups.forEach((dGroup) => {
                // Date Group Header Row
                htmlOut += `
                  <tr style="background-color: #f1f5f9; font-weight: 700;">
                    <td colspan="${totalColSpan}" style="padding: 6px 8px; font-size: 11px; color: #0f172a; border-top: 1px solid #cbd5e1; border-bottom: 1px solid #cbd5e1; text-align: left !important;">
                      ${dGroup.dateKey}
                    </td>
                  </tr>
                `;

                let dayWeight = 0;
                let dayPCS = 0;
                let dayTotalVal = 0;
                let dayNetDisc = 0;
                let daySMC = 0;
                let dayGST = 0;
                let dayNetAmt = 0;

                dGroup.invoices.forEach((inv) => {
                  // Invoice Header Row
                  htmlOut += `
                    <tr style="background-color: #ffffff; font-weight: 700;">
                      <td colspan="${totalColSpan}" style="padding: 5px 8px; font-size: 10px; color: #000000; text-align: left !important;">
                        <span style="display: inline-block; width: 100px;">${inv.invoiceNo}</span>
                        <span>${inv.customerLabel}</span>
                      </td>
                    </tr>
                  `;

                  let invWeight = 0;
                  let invPCS = 0;
                  let invTotalVal = 0;
                  let invNetDisc = 0;
                  let invSMC = 0;
                  let invGST = 0;
                  let invNetAmt = 0;

                  inv.items.forEach((item) => {
                    const wt = Number(item.Weight) || 0;
                    const pcs = Number(item.PCS) || 0;
                    const tv = Number(item.TotalValue) || 0;
                    const nd = Number(item.NetDisc) || 0;
                    const smc = Number(item.SMC) || 0;
                    const gst = Number(item.GST) || 0;
                    const na = Number(item.NetAmount) || 0;

                    invWeight += wt;
                    invPCS += pcs;
                    invTotalVal += tv;
                    invNetDisc += nd;
                    invSMC += smc;
                    invGST += gst;
                    invNetAmt += na;

                    htmlOut += `
                      <tr>
                        <td>${formatDateDisp(item.InvoiceDate)}</td>
                        <td>${item.InvoiceNo || ""}</td>
                        <td>${item.InventoryCode || ""}</td>
                        <td>${item.Description || ""}</td>
                        <td>${wt.toFixed(2)}</td>
                        <td>${pcs}</td>
                        <td>${tv.toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                        <td>${nd.toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                        <td>${smc.toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                        <td>${gst.toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                        <td>${na.toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                      </tr>
                    `;
                  });

                  dayWeight += invWeight;
                  dayPCS += invPCS;
                  dayTotalVal += invTotalVal;
                  dayNetDisc += invNetDisc;
                  daySMC += invSMC;
                  dayGST += invGST;
                  dayNetAmt += invNetAmt;

                  // Invoice Subtotal: Net Sales Amount
                  htmlOut += `
                    <tr style="font-weight: 700; font-style: italic; background-color: #fafbfc; border-bottom: 1px dashed #cbd5e1;">
                      <td colspan="4" style="text-align: right !important; padding-right: 10px; font-weight: 700; font-style: italic;">Net Sales Amount</td>
                      <td style="border-top: 1px solid #94a3b8; font-weight: 700;">${invWeight.toFixed(2)}</td>
                      <td style="border-top: 1px solid #94a3b8; font-weight: 700;">${invPCS}</td>
                      <td style="border-top: 1px solid #94a3b8; font-weight: 700;">${invTotalVal.toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                      <td style="border-top: 1px solid #94a3b8; font-weight: 700;">${invNetDisc.toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                      <td style="border-top: 1px solid #94a3b8; font-weight: 700;">${invSMC.toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                      <td style="border-top: 1px solid #94a3b8; font-weight: 700;">${invGST.toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                      <td style="border-top: 1px solid #94a3b8; font-weight: 700;">${invNetAmt.toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                    </tr>
                  `;
                });

                // Daily Total: Total Sales For the Day: [Date]
                htmlOut += `
                  <tr style="font-weight: 800; background-color: #ffffff; border-top: 2px solid #000000; border-bottom: 2px solid #000000;">
                    <td colspan="4" style="font-style: italic; font-weight: 700; padding: 6px 8px; text-align: left !important;">
                      Total Sales For the Day: ${dGroup.dateKey}
                    </td>
                    <td style="font-weight: 800;">${dayWeight.toFixed(2)}</td>
                    <td style="font-weight: 800;">${dayPCS}</td>
                    <td style="font-weight: 800;">${dayTotalVal.toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                    <td style="font-weight: 800;">${dayNetDisc.toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                    <td style="font-weight: 800;">${daySMC.toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                    <td style="font-weight: 800;">${dayGST.toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                    <td style="font-weight: 800;">${dayNetAmt.toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                  </tr>
                `;
              });

              return htmlOut;
            })()
          }

          ${
            totals && Object.keys(totals).length > 0
              ? `
            <tr class="totals-row">
              ${(columns || [])
                .map((c, cIdx) => {
                  if (cIdx === 0) return `<td>TOTAL:</td>`;
                  const k = (c.key || "").toLowerCase().replace(/[\s_\-\.]/g, "");
                  if (k === "pcs" || k === "totalpcs" || k === "qty" || k === "totalqty") {
                    return `<td>${totals.totalPcs !== undefined ? Number(totals.totalPcs).toLocaleString() : totals.totalQty !== undefined ? Number(totals.totalQty).toLocaleString() : ""}</td>`;
                  }
                  if (k.includes("weight")) {
                    return `<td>${totals.totalWeight !== undefined ? Number(totals.totalWeight).toFixed(3) : totals.totalNetWeight !== undefined ? Number(totals.totalNetWeight).toFixed(3) : ""}</td>`;
                  }
                  if (k === "cost" || k === "totalcost" || k === "totalsmc" || k === "smc") {
                    return `<td>$ ${totals.totalCost !== undefined ? Number(totals.totalCost).toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 }) : ""}</td>`;
                  }
                  if (k === "amount" || k === "totalamount" || k === "nettotal" || k === "totalsales") {
                    const amt = totals.totalAmount !== undefined ? totals.totalAmount : totals.totalSales !== undefined ? totals.totalSales : totals.netSales;
                    return `<td>${amt !== undefined ? "$ " + Number(amt).toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 }) : ""}</td>`;
                  }
                  if (k === "pmc" || k === "totalpmc") {
                    return `<td>${totals.totalPmc !== undefined ? "$ " + Number(totals.totalPmc).toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 }) : ""}</td>`;
                  }
                  if (totals[c.key] !== undefined) {
                    const num = Number(totals[c.key]);
                    return `<td>${!isNaN(num) ? num.toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 }) : totals[c.key]}</td>`;
                  }
                  return `<td></td>`;
                })
                .join("")}
            </tr>
          `
              : ""
          }
        </tbody>
      </table>
    </div>

    <!-- FOOTER -->
    <div class="report-footer">
      <div class="footer-divider"></div>
      <div class="footer-gen-text">*** System Generated Report ***</div>
      <div class="footer-powered">Powered by Unipro SG</div>
    </div>
  </div>

</body>
</html>`;
}

module.exports = {
  generateReportHtml,
  formatDateDisp,
  formatColumnHeader,
  getCompactHeaderLabel: formatColumnHeader,
  getColumnAlignment,
  formatVal,
};
