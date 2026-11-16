import { MENU_STRUCTURE } from './menuConfig';

export interface MenuNode {
  nodeId: string;
  nodeName: string; // The exact Menu Page node title used as FormCode (e.g. 'Master', 'Sales', 'Purchase', 'Inventory')
  category: string;
}

// Extract all unique Menu Page nodes across parent sections and sub-cards
export const MENU_PAGE_NODES: MenuNode[] = (() => {
  const list: MenuNode[] = [];
  const seen = new Set<string>();

  // 1. Top-level Menu categories (Master, Purchase, Sales, Transaction, Transfer, Reports, Daily Procedure, Utility, Security)
  (MENU_STRUCTURE.home || []).forEach(item => {
    if (!seen.has(item.title)) {
      seen.add(item.title);
      list.push({ nodeId: item.id, nodeName: item.title, category: 'Main Menu' });
    }
  });

  // 2. Child nodes inside each section
  Object.keys(MENU_STRUCTURE).forEach(sectionKey => {
    if (sectionKey === 'home') return;
    const categoryName = sectionKey.replace('_submenu', '').replace(/^./, c => c.toUpperCase());
    (MENU_STRUCTURE[sectionKey] || []).forEach(item => {
      if (!seen.has(item.title)) {
        seen.add(item.title);
        list.push({ nodeId: item.id, nodeName: item.title, category: categoryName });
      }
    });
  });

  return list;
})();

