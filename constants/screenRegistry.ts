// Central registry of all actual screen names mapped to their routes
// This matches MENU_STRUCTURE and each screen's FormCode
export interface ScreenDefinition {
  formCode: string;   // Actual screen name used in tblUserpermission (e.g. 'Class', 'POS', 'Customer')
  title: string;      // Human-readable title
  route?: string;     // Route path
  category: string;   // Module group
}

export const APPLICATION_SCREENS: ScreenDefinition[] = [
  // Master -> Inventory
  { formCode: 'Class', title: 'Class', route: '/class', category: 'Master - Inventory' },
  { formCode: 'Category', title: 'Category', route: '/category', category: 'Master - Inventory' },
  { formCode: 'Catalogue', title: 'Catalogue', route: '/catalogue', category: 'Master - Inventory' },
  { formCode: 'DailyClassRate', title: 'Daily Class Rate', route: '/dailyclassrate', category: 'Master - Inventory' },
  { formCode: 'TrayMaster', title: 'Tray Master', route: '/traymaster', category: 'Master - Inventory' },
  { formCode: 'SizeMaster', title: 'Size Master', route: '/size', category: 'Master - Inventory' },

  // Master -> Company
  { formCode: 'Company', title: 'Company', route: '/company', category: 'Master - Company' },
  { formCode: 'Location', title: 'Location', route: '/location', category: 'Master - Company' },
  { formCode: 'Terminal', title: 'Terminal', route: '/terminal', category: 'Master - Company' },

  // Master -> Sales
  { formCode: 'Salesman', title: 'Salesman', route: '/salesman', category: 'Master - Sales' },
  { formCode: 'Paymode', title: 'Paymode', route: '/paymode', category: 'Master - Sales' },
  { formCode: 'Customer', title: 'Customers', route: '/customer', category: 'Master - Sales' },
  { formCode: 'CustomerOutlets', title: 'Customer Outlets', route: '/outletcustomer', category: 'Master - Sales' },
  { formCode: 'PointsAdjustment', title: 'Customer Points Adjustment', route: '/customerpointsadjustment', category: 'Master - Sales' },
  { formCode: 'TypeChange', title: 'Customer Type Change', route: '/customertypechange', category: 'Master - Sales' },

  // Master -> Procurement & IPM
  { formCode: 'Supplier', title: 'Supplier', route: '/supplier', category: 'Master - Procurement' },
  { formCode: 'Refiner', title: 'Refiner', route: '/refiner', category: 'Master - IPM' },
  { formCode: 'Country', title: 'Country', route: '/country', category: 'Master - IPM' },

  // Master Others
  { formCode: 'Currency', title: 'Currency', route: '/currency', category: 'Master - General' },
  { formCode: 'PayoutMaster', title: 'Payout Master', route: '/payoutmaster', category: 'Master - General' },
  { formCode: 'ReorderLevel', title: 'Reorder Level', route: '/reorderlevel', category: 'Master - General' },
  { formCode: 'SchemeMaster', title: 'Scheme Master', route: '/schememaster', category: 'Master - General' },

  // Security
  { formCode: 'UserGroup', title: 'User Group', route: '/usergroup', category: 'Security' },
  { formCode: 'UserMaster', title: 'User Master', route: '/usermaster', category: 'Security' },
  { formCode: 'UserPermission', title: 'User Permission', route: '/userpermission', category: 'Security' },

  // Sales
  { formCode: 'POS', title: 'POS', route: '/pos', category: 'Sales' },
  { formCode: 'PaymentEntry', title: 'Payment Entry', route: '/paymententry', category: 'Sales' },
  { formCode: 'PaymodeMod', title: 'Paymode Modification', route: '/paymodemodification', category: 'Sales' },
  { formCode: 'WholesalePOS', title: 'WholeSale POS', route: '/wholesalepos', category: 'Sales' },
  { formCode: 'TradeInSales', title: 'Trade In Sales', route: '/tradeinsales', category: 'Sales' },
  { formCode: 'MiscSales', title: 'Misc', route: '/misc', category: 'Sales' },
  { formCode: 'SalesReturn', title: 'Sales Return', route: '/salesreturn', category: 'Sales' },
  { formCode: 'PayOut', title: 'Pay Out', route: '/payout', category: 'Sales' },
  { formCode: 'SalesHistory', title: 'Sales History', route: '/saleshistory', category: 'Sales' },
  { formCode: 'CreditPayment', title: 'Credit Sales Payment', route: '/creditsalespayment', category: 'Sales' },
  { formCode: 'Deposit', title: 'Deposit', route: '/deposit', category: 'Sales' },
  { formCode: 'DailySales', title: 'Daily Sales Entry', route: '/dailysalesentry', category: 'Sales' },

  // Purchase
  { formCode: 'JobSheet', title: 'JobSheet', route: '/jobsheet', category: 'Purchase' },
  { formCode: 'Purchase', title: 'Purchase', route: '/purchase', category: 'Purchase' },
  { formCode: 'DirectPurchase', title: 'Direct Purchase', route: '/directpurchase', category: 'Purchase' },
  { formCode: 'JobCosting', title: 'Job Sheet Costing', route: '/jobsheetcosting', category: 'Purchase' },
  { formCode: 'Barcode', title: 'Barcode', route: '/barcode', category: 'Purchase' },
  { formCode: 'InvSearch', title: 'Inventory Search', route: '/inventorysearch', category: 'Purchase' },
  { formCode: 'InvMaintenance', title: 'Inventory Maintenance', route: '/inventorymaintenance', category: 'Purchase' },
  { formCode: 'InvMovement', title: 'Inventory Movement', route: '/inventorymovement', category: 'Purchase' },
  { formCode: 'CatalogueSearch', title: 'Catalogue Search', route: '/cataloguesearch', category: 'Purchase' },
  { formCode: 'SplitInv', title: 'Split Inventory', route: '/splitinventory', category: 'Purchase' },
  { formCode: 'MergeInv', title: 'Merge Inventory', route: '/mergeinventory', category: 'Purchase' },
  { formCode: 'PurchaseReturn', title: 'Purchase Return', route: '/purchasereturn', category: 'Purchase' },
  { formCode: 'SalesOrder', title: 'Sales Order', route: '/salesorder', category: 'Purchase' },
  { formCode: 'GoldBarFix', title: 'GoldBar Fix', route: '/goldbarfix', category: 'Purchase' },
  { formCode: 'SupplierPay', title: 'Supplier Payment', route: '/supplierpayment', category: 'Purchase' },
  { formCode: 'SupplierPayAmend', title: 'Supplier Payment Amendment', route: '/supplierpaymentamendment', category: 'Purchase' },
  { formCode: 'PurchaseEntry', title: 'Purchase Entry', route: '/purchaseentry', category: 'Purchase' },

  // Daily Procedure
  { formCode: 'DayendReport', title: 'Dayend Report', route: '/eod', category: 'Daily Procedure' },
  { formCode: 'SettlementReport', title: 'Settlement Report', route: '/settlementreports', category: 'Daily Procedure' },

  // Transaction
  { formCode: 'InstallmentEntry', title: 'Installment/Order Entry', route: '/installmentorderentry', category: 'Transaction' },
  { formCode: 'InstallmentPay', title: 'Installment Payment', route: '/installmentpayment', category: 'Transaction' },
  { formCode: 'OrderInv', title: 'Order Inventory', route: '/orderinventory', category: 'Transaction' },
  { formCode: 'OrderPayment', title: 'Order Payment', route: '/orderpayment', category: 'Transaction' },
  { formCode: 'RepairEntry', title: 'Repair Entry', route: '/repairentry', category: 'Transaction' },
  { formCode: 'RepairPayment', title: 'Repair Payment', route: '/repairpayment', category: 'Transaction' },
  { formCode: 'TrayTransfer', title: 'Tray Transfer', route: '/traytransfer', category: 'Transaction' },
  { formCode: 'TradeInGoldIssue', title: 'Trade In Gold Issue', route: '/tradeingoldissue', category: 'Transaction' },
  { formCode: 'TradeInSplit', title: 'Trade In Split', route: '/tradeinsplit', category: 'Transaction' },
  { formCode: 'TradeInIssue', title: 'Trade In Issue', route: '/tradeinissue', category: 'Transaction' },

  // Transfer
  { formCode: 'TransferOut', title: 'Transfer Out', route: '/transferout', category: 'Transfer' },
  { formCode: 'TransferIn', title: 'Transfer In', route: '/transferin', category: 'Transfer' },
  { formCode: 'TransferStatus', title: 'Transfer Status', route: '/transferstatus', category: 'Transfer' },
  { formCode: 'OnlineTransfer', title: 'Online Transfer', route: '/onlinetransfer', category: 'Transfer' },
  { formCode: 'StockTradeTransfer', title: 'Stock Trade-In Transfer', route: '/stocktradeintransfer', category: 'Transfer' },
  { formCode: 'TransferReports', title: 'Transfer Reports', route: '/transferreports', category: 'Transfer' },

  // Utility
  { formCode: 'StockTake', title: 'Stock Take', route: '/utility/stock-take', category: 'Utility' },
  { formCode: 'StockTakeReports', title: 'Stock Take Reports', route: '/utility/stock-take-reports', category: 'Utility' },
  { formCode: 'ChangePrinter', title: 'Change Printer', route: '/utility/change-printer', category: 'Utility' },
  { formCode: 'eTRSTicket', title: 'eTRS Ticket', route: '/utility/etrs-ticket', category: 'Utility' },
  { formCode: 'eTRSTicketVoid', title: 'eTRS Ticket Void', route: '/utility/etrs-ticket-void', category: 'Utility' },
  { formCode: 'PurchaseTake', title: 'Purchase Take', route: '/utility/purchase-take', category: 'Utility' },
  { formCode: 'PurchaseTakeReports', title: 'Purchase Take Reports', route: '/utility/purchase-take-reports', category: 'Utility' },
  { formCode: 'GoldSchemeTxn', title: 'Gold Scheme Transaction', route: '/utility/gold-scheme-transaction', category: 'Utility' },

  // Reports
  { formCode: 'MasterReports', title: 'Master Reports', route: '/masterreports', category: 'Reports' },
  { formCode: 'CustomerReports', title: 'Customer Reports', route: '/customerreports', category: 'Reports' },
  { formCode: 'InventoryReports', title: 'Inventory Reports', route: '/inventoryreports', category: 'Reports' },
  { formCode: 'PurchaseReports', title: 'Purchase Reports', route: '/purchasereports', category: 'Reports' },
  { formCode: 'SalesReports', title: 'Sales Reports', route: '/salesreports', category: 'Reports' },
  { formCode: 'TransactionReports', title: 'Transaction Reports', route: '/transactionreports', category: 'Reports' },
  { formCode: 'ManagementReports', title: 'Management Reports', route: '/managementreports', category: 'Reports' },
  { formCode: 'TradeInReports', title: 'Trade-In Reports', route: '/tradeinreports', category: 'Reports' },
];

/**
 * Maps a MenuItem (from menuConfig) to its screen FormCode in tblUserpermission
 */
export function getFormCodeForMenuItem(menuItem: { id: string; route?: string; title?: string }): string | undefined {
  if (menuItem.route) {
    const cleanRoute = menuItem.route.toLowerCase();
    const match = APPLICATION_SCREENS.find(s => s.route && s.route.toLowerCase() === cleanRoute);
    if (match) return match.formCode;
  }
  // Try matching by id or title
  const idMatch = APPLICATION_SCREENS.find(s => s.formCode.toLowerCase() === menuItem.id.toLowerCase());
  if (idMatch) return idMatch.formCode;

  const titleMatch = APPLICATION_SCREENS.find(s => s.title.toLowerCase() === (menuItem.title || '').toLowerCase());
  if (titleMatch) return titleMatch.formCode;

  return undefined;
}

