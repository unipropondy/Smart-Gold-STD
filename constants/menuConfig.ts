import { Ionicons } from '@expo/vector-icons';

export interface MenuItem {
  id: string;
  title: string;
  icon: keyof typeof Ionicons.glyphMap;
  color: string;
  route?: string; // If it's a leaf node that navigates to a real page
  hasSubmenu?: boolean; // If it should navigate to /submenu/[id]
}

export type MenuStructure = Record<string, MenuItem[]>;

export const MENU_STRUCTURE: MenuStructure = {
  home: [
    { id: 'master', title: 'Master', icon: 'person-outline', color: '#2563EB', hasSubmenu: true },
    { id: 'purchase', title: 'Purchase', icon: 'cart-outline', color: '#3B82F6', hasSubmenu: true },
    { id: 'sales', title: 'Sales', icon: 'cash-outline', color: '#F59E0B', hasSubmenu: true },
    { id: 'transaction', title: 'Transaction', icon: 'card-outline', color: '#8B5CF6', hasSubmenu: true },
    { id: 'transfer', title: 'Transfer', icon: 'swap-horizontal-outline', color: '#EC4899', hasSubmenu: true },
    { id: 'reports', title: 'Reports', icon: 'bar-chart-outline', color: '#10B981', hasSubmenu: true },
    { id: 'dailyprocedure', title: 'Daily Procedure', icon: 'calendar-outline', color: '#F97316', hasSubmenu: true },
    { id: 'security', title: 'Security', icon: 'shield-checkmark-outline', color: '#64748B', hasSubmenu: true },
    // { id: 'customer', title: 'Customer', icon: 'people-outline', color: '#8B5CF6', route: '/customers' },
  ],
  master: [
    { id: 'company_submenu', title: 'Company', icon: 'business-outline', color: '#6366F1', hasSubmenu: true },
    { id: 'inventory_submenu', title: 'Inventory', icon: 'cube-outline', color: '#10B981', hasSubmenu: true },
    { id: 'procurement_submenu', title: 'Procurement', icon: 'cart-outline', color: '#10B981', hasSubmenu: true },
    { id: 'sales_submenu', title: 'Sales', icon: 'trending-up-outline', color: '#F59E0B', hasSubmenu: true },
    { id: 'ipm_submenu', title: 'IPM', icon: 'globe-outline', color: '#8B5CF6', hasSubmenu: true },
    { id: 'currency', title: 'Currency', icon: 'wallet-outline', color: '#14B8A6', route: '/currency' },
    { id: 'payoutmaster', title: 'Payout Master', icon: 'card-outline', color: '#6366F1', route: '/payoutmaster' },
    { id: 'reorderlevel', title: 'Reorder Level', icon: 'bar-chart-outline', color: '#EC4899', route: '/reorderlevel' },
    { id: 'schememaster', title: 'Scheme Master', icon: 'gift-outline', color: '#F43F5E', route: '/schememaster' },
  ],
  company_submenu: [
    { id: 'company', title: 'Company', icon: 'business-outline', color: '#6366F1', route: '/company' },
    { id: 'location', title: 'Location', icon: 'location-outline', color: '#EF4444', route: '/location' },
    { id: 'terminal', title: 'Terminal', icon: 'desktop-outline', color: '#8B5CF6', route: '/terminal' },
  ],
  inventory_submenu: [
    { id: 'class', title: 'Class', icon: 'list-outline', color: '#3B82F6', route: '/class' },
    { id: 'category', title: 'Category', icon: 'pricetag-outline', color: '#F43F5E', route: '/category' },
    { id: 'catalogue', title: 'Catalogue', icon: 'book-outline', color: '#8B5CF6', route: '/catalogue' },
    { id: 'dailyclassrate', title: 'Daily Class Rate', icon: 'trending-up-outline', color: '#F59E0B', route: '/dailyclassrate' },
    { id: 'traymaster', title: 'Tray Master', icon: 'apps-outline', color: '#06B6D4', route: '/traymaster' },
    { id: 'size', title: 'Size Master', icon: 'resize-outline', color: '#EC4899', route: '/size' },
  ],
  sales_submenu: [
    { id: 'salesman', title: 'Salesman', icon: 'person-outline', color: '#F59E0B', route: '/salesman' },
    { id: 'paymode', title: 'Paymode', icon: 'card-outline', color: '#3B82F6', route: '/paymode' },
    { id: 'customers', title: 'Customers', icon: 'people-outline', color: '#8B5CF6', route: '/customer' },
    { id: 'customeroutlets', title: 'Customer Outlets', icon: 'storefront-outline', color: '#10B981', route: '/outletcustomer' },
    { id: 'customerpointsadjustment', title: 'Customer Points Adjustment', icon: 'star-outline', color: '#EC4899', route: '/customerpointsadjustment' },
    { id: 'customertypechange', title: 'Customer Type Change', icon: 'swap-horizontal-outline', color: '#6366F1', route: '/customertypechange' },
  ],
  ipm_submenu: [
    { id: 'refiner', title: 'Refiner', icon: 'business-outline', color: '#F43F5E', route: '/refiner' },
    { id: 'country', title: 'Country', icon: 'globe-outline', color: '#3B82F6', route: '/country' },
  ],
  procurement_submenu: [
    { id: 'supplier', title: 'Supplier', icon: 'cube-outline', color: '#10B981', route: '/supplier' },
  ],
  security: [
    { id: 'usergroup', title: 'User Group', icon: 'people-outline', color: '#3B82F6', route: '/usergroup' },
    { id: 'usermaster', title: 'User Master', icon: 'person-outline', color: '#10B981', route: '/usermaster' },
    { id: 'userpermission', title: 'User Permission', icon: 'key-outline', color: '#F59E0B', route: '/userpermission' },
  ],
  purchase: [
    { id: 'jobsheet', title: 'JobSheet', icon: 'document-text-outline', color: '#3B82F6', route: '/jobsheet' },
    { id: 'purchase_page', title: 'Purchase', icon: 'cart-outline', color: '#10B981', route: '/purchase' },
    { id: 'directpurchase', title: 'Direct Purchase', icon: 'bag-outline', color: '#F59E0B', route: '/directpurchase' },
    { id: 'jobsheetcosting', title: 'Job Sheet Costing', icon: 'calculator-outline', color: '#8B5CF6', route: '/jobsheetcosting' },
    { id: 'barcode', title: 'Barcode', icon: 'barcode-outline', color: '#EC4899', route: '/barcode' },
    { id: 'inventorysearch', title: 'Inventory Search', icon: 'search-outline', color: '#6366F1', route: '/inventorysearch' },
    { id: 'inventorymaintenance', title: 'Inventory Maintenance', icon: 'construct-outline', color: '#F43F5E', route: '/inventorymaintenance' },
    { id: 'inventorymovement', title: 'Inventory Movement', icon: 'swap-horizontal-outline', color: '#14B8A6', route: '/inventorymovement' },
    { id: 'cataloguesearch', title: 'Catalogue Search', icon: 'search-circle-outline', color: '#8B5CF6', route: '/cataloguesearch' },
    { id: 'splitinventory', title: 'Split Inventory', icon: 'git-branch-outline', color: '#10B981', route: '/splitinventory' },
    { id: 'mergeinventory', title: 'Merge Inventory', icon: 'git-merge-outline', color: '#3B82F6', route: '/mergeinventory' },
    { id: 'purchasereturn', title: 'Puchase Return', icon: 'return-up-back-outline', color: '#F59E0B', route: '/purchasereturn' },
    { id: 'salesorder', title: 'Sales Order', icon: 'receipt-outline', color: '#EC4899', route: '/salesorder' },
    { id: 'goldbarfix', title: 'GoldBar Fix', icon: 'color-wand-outline', color: '#F59E0B', route: '/goldbarfix' },
    { id: 'supplierpayment', title: 'Supplier Payment', icon: 'card-outline', color: '#6366F1', route: '/supplierpayment' },
    { id: 'supplierpaymentamendment', title: 'Supplier Payment Amendment', icon: 'create-outline', color: '#14B8A6', route: '/supplierpaymentamendment' },
    { id: 'purchaseentry', title: 'Purchase Entry', icon: 'log-in-outline', color: '#F43F5E', route: '/purchaseentry' },
  ],
  dailyprocedure: [
    { id: 'settlement', title: 'Settlement', icon: 'cash-outline', color: '#F59E0B', route: '/settlement' },
    { id: 'dayend', title: 'Day End', icon: 'moon-outline', color: '#8B5CF6', route: '/dayend' },
  ],
  sales: [
    { id: 'pos', title: 'POS', icon: 'calculator-outline', color: '#10B981', route: '/pos' },
    { id: 'paymententry', title: 'Payment Entry', icon: 'cash-outline', color: '#F59E0B', route: '/paymententry' },
    { id: 'paymodemodification', title: 'Paymode Modification', icon: 'card-outline', color: '#3B82F6', route: '/paymodemodification' },
    { id: 'wholesalepos', title: 'WholeSale POS', icon: 'business-outline', color: '#8B5CF6', route: '/wholesalepos' },
    { id: 'tradeinsales', title: 'Trade In Sales', icon: 'swap-horizontal-outline', color: '#F43F5E', route: '/tradeinsales' },
    { id: 'misc', title: 'Misc', icon: 'grid-outline', color: '#6366F1', route: '/misc' },
    { id: 'salesreturn', title: 'Sales Return', icon: 'return-down-back-outline', color: '#EC4899', route: '/salesreturn' },
    { id: 'payout', title: 'Pay Out', icon: 'wallet-outline', color: '#14B8A6', route: '/payout' },
    { id: 'saleshistory', title: 'Sales History', icon: 'time-outline', color: '#F59E0B', route: '/saleshistory' },
    { id: 'creditsalespayment', title: 'Credit Sales Payment', icon: 'card-outline', color: '#3B82F6', route: '/creditsalespayment' },
    { id: 'deposit', title: 'Deposit', icon: 'archive-outline', color: '#10B981', route: '/deposit' },
    { id: 'dailysalesentry', title: 'Daily Sales Entry', icon: 'calendar-outline', color: '#8B5CF6', route: '/dailysalesentry' },
  ],
  transaction: [
    { id: 'installmentorderentry', title: 'Installment/Order Entry', icon: 'document-text-outline', color: '#3B82F6', route: '/installmentorderentry' },
    { id: 'installmentpayment', title: 'Installment Payment', icon: 'cash-outline', color: '#10B981', route: '/installmentpayment' },
    { id: 'orderinventory', title: 'Order Inventory', icon: 'cube-outline', color: '#F59E0B', route: '/orderinventory' },
    { id: 'orderpayment', title: 'Order Payment', icon: 'card-outline', color: '#8B5CF6', route: '/orderpayment' },
    { id: 'repairentry', title: 'Repair Entry', icon: 'build-outline', color: '#EC4899', route: '/repairentry' },
    { id: 'repairpayment', title: 'Repair Payment', icon: 'wallet-outline', color: '#6366F1', route: '/repairpayment' },
    { id: 'traytransfer', title: 'Tray Transfer', icon: 'swap-horizontal-outline', color: '#F43F5E', route: '/traytransfer' },
    { id: 'tradeingoldissue', title: 'Trade In Gold Issue', icon: 'sparkles-outline', color: '#F59E0B', route: '/tradeingoldissue' },
    { id: 'tradeinsplit', title: 'Trade In Split', icon: 'git-branch-outline', color: '#14B8A6', route: '/tradeinsplit' },
    { id: 'tradeinissue', title: 'Trade In Issue', icon: 'arrow-redo-outline', color: '#3B82F6', route: '/tradeinissue' },
  ],
  transfer: [
    { id: 'transfertransaction', title: 'Transaction', icon: 'card-outline', color: '#3B82F6', hasSubmenu: true },
    { id: 'transferreports', title: 'Reports', icon: 'bar-chart-outline', color: '#10B981', route: '/transferreports' },
  ],
  transfertransaction: [
    { id: 'transferout', title: 'Transfer Out', icon: 'arrow-up-circle-outline', color: '#EF4444', route: '/transferout' },
    { id: 'transferin', title: 'Transfer In', icon: 'arrow-down-circle-outline', color: '#10B981', route: '/transferin' },
    { id: 'transferstatus', title: 'Transfer Status', icon: 'information-circle-outline', color: '#3B82F6', route: '/transferstatus' },
    { id: 'onlinetransfer', title: 'Online Transfer', icon: 'globe-outline', color: '#8B5CF6', route: '/onlinetransfer' },
    { id: 'stocktradeintransfer', title: 'Stock Trade-In Transfer', icon: 'swap-horizontal-outline', color: '#F59E0B', route: '/stocktradeintransfer' },
  ],
  reports: [
    { id: 'masterreports', title: 'Master Reports', icon: 'document-text-outline', color: '#3B82F6', route: '/masterreports' },
    { id: 'customerreports', title: 'Customer Reports', icon: 'people-outline', color: '#10B981', route: '/customerreports' },
    { id: 'inventoryreports', title: 'Inventory Reports', icon: 'cube-outline', color: '#F59E0B', route: '/inventoryreports' },
    { id: 'purchasereports', title: 'Purchase Reports', icon: 'cart-outline', color: '#8B5CF6', route: '/purchasereports' },
    { id: 'salesreports', title: 'Sales Reports', icon: 'trending-up-outline', color: '#EC4899', route: '/salesreports' },
    { id: 'transactionreports', title: 'Transaction Reports', icon: 'card-outline', color: '#6366F1', route: '/transactionreports' },
    { id: 'eodhistoryreports', title: 'EOD History Reports', icon: 'time-outline', color: '#14B8A6', route: '/eodhistoryreports' },
    { id: 'managementreports', title: 'Management Reports', icon: 'briefcase-outline', color: '#F43F5E', route: '/managementreports' },
    { id: 'tradeinreports', title: 'Trade-In Reports', icon: 'swap-horizontal-outline', color: '#F59E0B', route: '/tradeinreports' },
  ]
};
