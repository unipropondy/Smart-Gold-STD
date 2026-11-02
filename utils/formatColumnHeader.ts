/**
 * Reusable column header formatter for React Native / Web Frontend
 * Enforces:
 * 1. ALL UPPERCASE
 * 2. Short, professional abbreviations
 * 3. Single-line presentation
 */
export const formatColumnHeader = (colName: string): string => {
  if (!colName) return "";
  const labelMap: Record<string, string> = {
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

