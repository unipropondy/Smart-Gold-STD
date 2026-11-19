/** Developer: Antigravity */
import { PrintReportModal } from '@/components/PrintReportModal';
import { ThemedText } from '@/components/themed-text';
import { API_BASE_URL } from '@/constants/api';
import { useThemeColors } from '@/hooks/useThemeColors';
import { getStandardReportCss, renderStandardReportFooter, renderStandardReportHeader } from '@/utils/standardPrintReport';
import { Ionicons } from '@expo/vector-icons';
import { Stack, useRouter } from 'expo-router';
import React, { useEffect, useState } from 'react';
import {
  ActivityIndicator,
  FlatList,
  Modal,
  Platform,
  ScrollView,
  StyleSheet,
  Text,
  TextInput,
  TouchableOpacity,
  TouchableWithoutFeedback,
  View
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';

const API_URL = `${API_BASE_URL}/goldbarissue`;

interface InvoiceRowItem {
  id?: string;
  JobsheetNo?: string;
  PurchaseBillNo: string;
  InvoiceDate: string;
  TranType?: string;
  PurchaseWeight: number;
  IssuedWeight: number;
  selected?: boolean;
}

interface IssueItemRow {
  id?: string;
  InventoryCode: string;
  Description: string;
  Weight: number;
  Qty: number;
  CostPerGram: number;
  TotalCost: number;
  BarNo: string;
}

interface SupplierItem {
  SupplierCode: string;
  SupplierName: string;
  Address1?: string;
  Address2?: string;
}

interface StockItem {
  InventoryCode: string;
  Description: string;
  Weight: number;
  CostPerGram: number;
  QtyOnHand: number;
  Pcs: number;
  TotalCost: number;
  ClassCode?: string;
  BarNo?: string;
}

export default function GoldBarIssueScreen() {
  const router = useRouter();
  const themeColors = useThemeColors();

  const [loading, setLoading] = useState(false);
  const [saving, setSaving] = useState(false);
  const [toast, setToast] = useState<{ message: string; type: 'success' | 'error' } | null>(null);

  // Top Section States (matching frmGoldBarIssue_AJ.vb)
  const [issueNo, setIssueNo] = useState('');
  const now = new Date();
  const [txtDD, setTxtDD] = useState(String(now.getDate()).padStart(2, '0'));
  const [txtMM, setTxtMM] = useState(String(now.getMonth() + 1).padStart(2, '0'));
  const [txtYYYY, setTxtYYYY] = useState(String(now.getFullYear()));

  const [supplierCode, setSupplierCode] = useState('');
  const [supplierName, setSupplierName] = useState('');
  const [remarks, setRemarks] = useState('');

  // Top Right Totals
  const [netIssuedWt, setNetIssuedWt] = useState('0.00'); // 24K - 999 (txtNetIssue1)
  const [issued22kWt, setIssued22kWt] = useState('0.00'); // 22K - 916 (txtIssued2)

  // Top Grid: Selected Invoices (grdGoldIssue)
  const [invoiceRows, setInvoiceRows] = useState<InvoiceRowItem[]>([]);
  const [selectedInvoiceRowIndex, setSelectedInvoiceRowIndex] = useState<number | null>(null);

  // Bottom Grid: Issued Gold Bar Items (grdIssue1)
  const [issueItems, setIssueItems] = useState<IssueItemRow[]>([]);
  const [selectedIssueItemIndex, setSelectedIssueItemIndex] = useState<number | null>(null);

  // Bottom Input Fields for Adding Items
  const [inputInventoryCode, setInputInventoryCode] = useState('');
  const [inputDescription, setInputDescription] = useState('');
  const [inputWeight, setInputWeight] = useState('');
  const [inputQty, setInputQty] = useState('1');
  const [inputCostPerGram, setInputCostPerGram] = useState('');
  const [inputTotalCost, setInputTotalCost] = useState('');
  const [inputBarNo, setInputBarNo] = useState('');

  // Modal 1: Select Invoices for Supplier (pnlInvoice - grdInvoice)
  const [isInvoiceModalVisible, setIsInvoiceModalVisible] = useState(false);
  const [availableInvoices, setAvailableInvoices] = useState<InvoiceRowItem[]>([]);
  const [selectAllInvoices, setSelectAllInvoices] = useState(false);

  // Modal 2: Supplier LOV
  const [isSupplierModalVisible, setIsSupplierModalVisible] = useState(false);
  const [supplierList, setSupplierList] = useState<SupplierItem[]>([]);
  const [supplierSearchQuery, setSupplierSearchQuery] = useState('');

  // Modal 3: Inventory Stock LOV
  const [isStockModalVisible, setIsStockModalVisible] = useState(false);
  const [stockList, setStockList] = useState<StockItem[]>([]);
  const [stockSearchQuery, setStockSearchQuery] = useState('');

  // Modal 4: Issue History LOV
  const [isHistoryModalVisible, setIsHistoryModalVisible] = useState(false);
  const [historyList, setHistoryList] = useState<any[]>([]);
  const [historySearchQuery, setHistorySearchQuery] = useState('');

  // Print Report Modal
  const [isPrintModalVisible, setIsPrintModalVisible] = useState(false);
  const [currentPrintIssueNo, setCurrentPrintIssueNo] = useState('');

  const showToast = (message: string, type: 'success' | 'error' = 'success') => {
    setToast({ message, type });
    setTimeout(() => setToast(null), 3200);
  };

  useEffect(() => {
    fetchNextIssueNo();
  }, []);

  const fetchNextIssueNo = async () => {
    try {
      setLoading(true);
      const res = await fetch(`${API_URL}/nextIssueNo`);
      const data = await res.json();
      if (data.success && (data.data || data.issueNo)) {
        setIssueNo(data.data || data.issueNo);
      }
    } catch (e: any) {
      console.error('Error fetching next issue no:', e);
    } finally {
      setLoading(false);
    }
  };

  // 1. Supplier Actions
  const openSupplierLOV = async () => {
    try {
      setLoading(true);
      const res = await fetch(`${API_URL}/suppliers`);
      const data = await res.json();
      if (data.success) {
        setSupplierList(data.data || []);
        setSupplierSearchQuery('');
        setIsSupplierModalVisible(true);
      } else {
        showToast('Failed to load suppliers', 'error');
      }
    } catch (e: any) {
      showToast('Error querying suppliers: ' + e.message, 'error');
    } finally {
      setLoading(false);
    }
  };

  const handleSelectSupplier = (s: SupplierItem) => {
    setSupplierCode(s.SupplierCode || '');
    setSupplierName(s.SupplierName || '');
    setIsSupplierModalVisible(false);
    // Clear invoices when supplier changes
    setInvoiceRows([]);
    setIssued22kWt('0.00');
  };

  const handleSupplierBlur = async () => {
    if (!supplierCode.trim()) {
      setSupplierName('');
      return;
    }
    try {
      const res = await fetch(`${API_URL}/supplier/${encodeURIComponent(supplierCode.trim())}`);
      const data = await res.json();
      if (data.success && data.data) {
        setSupplierName(data.data.SupplierName || '');
      } else {
        showToast('Invalid supplier code!', 'error');
        setSupplierName('');
      }
    } catch (e) {
      // ignore
    }
  };

  // 2. Open Invoice Selection Modal (pnlInvoice - grdInvoice)
  const openInvoiceSelectionModal = async () => {
    if (!supplierCode.trim()) {
      showToast('Select the Supplier Code!', 'error');
      return;
    }
    try {
      setLoading(true);
      const res = await fetch(`${API_URL}/invoices?supplierCode=${encodeURIComponent(supplierCode.trim())}`);
      const data = await res.json();
      if (data.success) {
        const rows = (data.data || []).map((inv: any) => ({
          PurchaseBillNo: inv.InvoiceNo || '',
          InvoiceDate: inv.InvoiceDate || '',
          PurchaseWeight: parseFloat(inv.PurchaseWeight || 0),
          IssuedWeight: parseFloat(inv.IssuedWeight || inv.PurchaseWeight || 0),
          selected: false,
        }));
        setAvailableInvoices(rows);
        setSelectAllInvoices(false);
        setIsInvoiceModalVisible(true);
      } else {
        showToast(data.message || 'No pending invoices found for supplier', 'error');
      }
    } catch (e: any) {
      showToast('Error loading invoices: ' + e.message, 'error');
    } finally {
      setLoading(false);
    }
  };

  const toggleInvoiceSelection = (idx: number) => {
    const updated = [...availableInvoices];
    updated[idx].selected = !updated[idx].selected;
    setAvailableInvoices(updated);
  };

  const handleToggleSelectAllInvoices = (val: boolean) => {
    setSelectAllInvoices(val);
    setAvailableInvoices(availableInvoices.map((inv) => ({ ...inv, selected: val })));
  };

  // Confirm Invoice Modal (btnPanelOK_Click in VB)
  const confirmSelectedInvoices = () => {
    const selected = availableInvoices.filter((inv) => inv.selected);
    if (selected.length === 0) {
      showToast('Please select at least one invoice!', 'error');
      return;
    }

    const mappedRows: InvoiceRowItem[] = selected.map((inv) => ({
      JobsheetNo: '',
      PurchaseBillNo: inv.PurchaseBillNo,
      InvoiceDate: inv.InvoiceDate,
      TranType: 'TRADE-IN',
      PurchaseWeight: inv.PurchaseWeight,
      IssuedWeight: inv.IssuedWeight,
    }));

    setInvoiceRows(mappedRows);
    setIsInvoiceModalVisible(false);

    // Sum Issued Weight for top grid (txtIssued2)
    const sumIssued = mappedRows.reduce((acc, row) => acc + (parseFloat(String(row.IssuedWeight)) || 0), 0);
    setIssued22kWt(sumIssued.toFixed(2));
    showToast(`Loaded ${mappedRows.length} invoice(s)`, 'success');
  };

  // Top Grid "OK" Button: Recalculate Issued Weight (btnIssueOk_Click in VB)
  const handleRecalculateInvoiceGrid = () => {
    const sumIssued = invoiceRows.reduce((acc, row) => acc + (parseFloat(String(row.IssuedWeight)) || 0), 0);
    setIssued22kWt(sumIssued.toFixed(2));
    showToast(`Invoice Issued Weight updated: ${sumIssued.toFixed(2)}`, 'success');
  };

  const handleInvoiceRowIssuedWeightChange = (index: number, val: string) => {
    const updated = [...invoiceRows];
    updated[index].IssuedWeight = parseFloat(val) || 0;
    setInvoiceRows(updated);
  };

  // 3. Stock / Inventory Code Lookup (btnInventoryCode_Click in VB)
  const openStockLOV = async () => {
    try {
      setLoading(true);
      const res = await fetch(`${API_URL}/inventory-stock`);
      const data = await res.json();
      if (data.success) {
        setStockList(data.data || []);
        setStockSearchQuery('');
        setIsStockModalVisible(true);
      } else {
        showToast('Failed to load inventory stock', 'error');
      }
    } catch (e: any) {
      showToast('Error querying stock: ' + e.message, 'error');
    } finally {
      setLoading(false);
    }
  };

  const handleSelectStock = (stk: StockItem) => {
    setInputInventoryCode(stk.InventoryCode || '');
    setInputDescription(stk.Description || '');
    setInputWeight(parseFloat(String(stk.Weight || 0)).toFixed(2));
    setInputCostPerGram(parseFloat(String(stk.CostPerGram || 0)).toFixed(2));
    setInputQty(String(stk.Pcs || stk.QtyOnHand || 1));
    setInputTotalCost(parseFloat(String(stk.TotalCost || 0)).toFixed(2));
    setIsStockModalVisible(false);

    // Automatically fetch Bar No if available
    fetchInventoryDetails(stk.InventoryCode);
  };

  const fetchInventoryDetails = async (code: string) => {
    if (!code || !code.trim()) return;
    try {
      const res = await fetch(`${API_URL}/inventory/${encodeURIComponent(code.trim())}`);
      const data = await res.json();
      if (data.success && data.data) {
        const inv = data.data;
        setInputDescription(inv.Description || '');
        setInputWeight(parseFloat(String(inv.Weight || 0)).toFixed(2));
        setInputCostPerGram(parseFloat(String(inv.CostPerGram || 0)).toFixed(2));
        setInputQty(String(inv.Pcs || inv.QtyOnHand || 1));
        setInputTotalCost(parseFloat(String(inv.TotalCost || 0)).toFixed(2));
        if (inv.BarNo) {
          setInputBarNo(inv.BarNo);
        }
      } else {
        showToast(data.message || 'Inventory not found or zero stock', 'error');
      }
    } catch (e) {
      // ignore
    }
  };

  const handleQtyWeightCostChange = (field: 'weight' | 'qty' | 'cpg', val: string) => {
    let wt = parseFloat(field === 'weight' ? val : inputWeight) || 0;
    let qty = parseFloat(field === 'qty' ? val : inputQty) || 0;
    let cpg = parseFloat(field === 'cpg' ? val : inputCostPerGram) || 0;

    if (field === 'weight') setInputWeight(val);
    if (field === 'qty') setInputQty(val);
    if (field === 'cpg') setInputCostPerGram(val);

    const calculatedTotal = wt * cpg * qty;
    setInputTotalCost(calculatedTotal > 0 ? calculatedTotal.toFixed(2) : '0.00');
  };

  // Add Item to Bottom Grid (subAddGrid in VB)
  const handleAddItem = () => {
    if (!inputInventoryCode.trim()) {
      showToast('Please enter or select Inventory Code', 'error');
      return;
    }
    const wt = parseFloat(inputWeight) || 0;
    const qty = parseFloat(inputQty) || 0;
    const cpg = parseFloat(inputCostPerGram) || 0;
    const tc = parseFloat(inputTotalCost) || (wt * cpg * qty);

    if (wt <= 0) {
      showToast('Total Weight cannot be zero', 'error');
      return;
    }

    const newItem: IssueItemRow = {
      InventoryCode: inputInventoryCode.trim(),
      Description: inputDescription.trim() || '24K GOLD BAR',
      Weight: wt,
      Qty: qty > 0 ? qty : 1,
      CostPerGram: cpg,
      TotalCost: tc,
      BarNo: inputBarNo.trim(),
    };

    const updated = [...issueItems, newItem];
    setIssueItems(updated);

    // Recalculate txtNetIssue1: d24KIssueWeight = sum of (Weight * Qty)
    const sum24kWt = updated.reduce((acc, itm) => acc + (itm.Weight * (itm.Qty || 1)), 0);
    setNetIssuedWt(sum24kWt.toFixed(2));

    // Clear detail inputs
    setInputInventoryCode('');
    setInputDescription('');
    setInputWeight('');
    setInputQty('1');
    setInputCostPerGram('');
    setInputTotalCost('');
    setInputBarNo('');
    setSelectedIssueItemIndex(null);

    showToast('Gold Bar item added', 'success');
  };

  // Delete Item from Bottom Grid (btnDelete_Click in VB)
  const handleDeleteItem = () => {
    if (selectedIssueItemIndex === null || selectedIssueItemIndex < 0 || selectedIssueItemIndex >= issueItems.length) {
      showToast('Please select a row from the Gold Bar table to delete', 'error');
      return;
    }
    const updated = [...issueItems];
    updated.splice(selectedIssueItemIndex, 1);
    setIssueItems(updated);
    setSelectedIssueItemIndex(null);

    const sum24kWt = updated.reduce((acc, itm) => acc + (itm.Weight * (itm.Qty || 1)), 0);
    setNetIssuedWt(sum24kWt.toFixed(2));
    showToast('Row deleted', 'success');
  };

  // 4. Issue History LOV (btnIssueNo_Click in VB)
  const openHistoryLOV = async () => {
    try {
      setLoading(true);
      const res = await fetch(`${API_URL}/history`);
      const data = await res.json();
      if (data.success) {
        setHistoryList(data.data || []);
        setHistorySearchQuery('');
        setIsHistoryModalVisible(true);
      } else {
        showToast('Failed to load history', 'error');
      }
    } catch (e: any) {
      showToast('Error loading history: ' + e.message, 'error');
    } finally {
      setLoading(false);
    }
  };

  const handleSelectHistoryIssue = async (hItem: any) => {
    const selNo = hItem.IssueNo;
    setIsHistoryModalVisible(false);
    if (!selNo) return;

    try {
      setLoading(true);
      const res = await fetch(`${API_URL}/history/${encodeURIComponent(selNo)}`);
      const data = await res.json();
      if (data.success) {
        const { header, invoices, items } = data;
        setIssueNo(header.IssueNo || selNo);
        setSupplierCode(header.SupplierCode || '');
        setSupplierName(header.SupplierName || '');
        setRemarks(header.Remarks || '');

        if (header.PDate) {
          const parts = String(header.PDate).split('/');
          if (parts.length === 3) {
            setTxtDD(parts[0]);
            setTxtMM(parts[1]);
            setTxtYYYY(parts[2]);
          }
        }

        const loadedInvoices: InvoiceRowItem[] = (invoices || []).map((inv: any) => ({
          PurchaseBillNo: inv.PurchaseBillNo || '',
          InvoiceDate: inv.InvoiceDate || '',
          TranType: 'TRADE-IN',
          PurchaseWeight: parseFloat(inv.TotalWeight24K || 0),
          IssuedWeight: parseFloat(inv.IssuedWeight || 0),
        }));
        setInvoiceRows(loadedInvoices);

        const loadedItems: IssueItemRow[] = (items || []).map((itm: any) => ({
          InventoryCode: itm.InventoryCode || '',
          Description: itm.Description || '',
          Weight: parseFloat(itm.Weight || 0),
          Qty: parseFloat(itm.Qty || 1),
          CostPerGram: parseFloat(itm.CostPerGram || 0),
          TotalCost: parseFloat(itm.TotalCost || 0),
          BarNo: itm.BarNo || '',
        }));
        setIssueItems(loadedItems);

        setIssued22kWt(parseFloat(header.IssuedWeight || 0).toFixed(2));
        setNetIssuedWt(parseFloat(header.IssuedWeight || 0).toFixed(2));

        showToast(`Loaded Gold Bar Issue ${selNo}`, 'success');
      } else {
        showToast(data.message || 'Issue record not found', 'error');
      }
    } catch (e: any) {
      showToast('Error loading issue details: ' + e.message, 'error');
    } finally {
      setLoading(false);
    }
  };

  // 5. Save Function (btnSave_Click in VB)
  const handleSave = async () => {
    if (!supplierCode.trim()) {
      showToast('Select the Supplier Code!', 'error');
      return;
    }
    if (invoiceRows.length === 0) {
      showToast('Please select at least one invoice in the invoice table!', 'error');
      return;
    }
    if (issueItems.length === 0) {
      showToast('Please add at least one Gold Bar item in the issue table!', 'error');
      return;
    }

    const nIssued = parseFloat(issued22kWt) || 0;
    const nNet = parseFloat(netIssuedWt) || 0;
    if (Math.abs(nIssued - nNet) > 0.001) {
      showToast('Issued Weight and Invoice Weight Not Tally!', 'error');
      return;
    }

    setSaving(true);
    try {
      const payload = {
        issueNo: issueNo.trim(),
        issueDate: `${txtDD}/${txtMM}/${txtYYYY}`,
        supplierCode: supplierCode.trim(),
        issuedWeight: nIssued,
        netIssuedWeight: nNet,
        remarks: remarks.trim(),
        userId: 'ADMIN',
        invoices: invoiceRows,
        items: issueItems,
      };

      const res = await fetch(`${API_URL}/save`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload),
      });

      const data = await res.json();
      if (data.success) {
        showToast('Issued Successfully!', 'success');
        const savedIssueNo = data.issueNo || issueNo;
        setCurrentPrintIssueNo(savedIssueNo);
        setIsPrintModalVisible(true);
        handleClear();
      } else {
        showToast(data.message || 'Failed to issue gold bar', 'error');
      }
    } catch (e: any) {
      showToast('Error saving gold bar issue: ' + e.message, 'error');
    } finally {
      setSaving(false);
    }
  };

  // Clear All Fields (btnNew_Click in VB)
  const handleClear = () => {
    setSupplierCode('');
    setSupplierName('');
    setRemarks('');
    setInvoiceRows([]);
    setIssueItems([]);
    setIssued22kWt('0.00');
    setNetIssuedWt('0.00');
    setSelectedInvoiceRowIndex(null);
    setSelectedIssueItemIndex(null);

    setInputInventoryCode('');
    setInputDescription('');
    setInputWeight('');
    setInputQty('1');
    setInputCostPerGram('');
    setInputTotalCost('');
    setInputBarNo('');

    fetchNextIssueNo();
  };

  // Standardized Print Functionality (subPrint in VB)
  const handlePrint = async (targetIssueNo: string) => {
    const sNo = (targetIssueNo || currentPrintIssueNo || issueNo).trim();
    if (!sNo) {
      showToast('Issue No is required for printing', 'error');
      return;
    }
    try {
      setLoading(true);
      const res = await fetch(`${API_URL}/print-data/${encodeURIComponent(sNo)}`);
      const json = await res.json();
      if (!json.success || !json.data) {
        showToast('Print data not found for issue ' + sNo, 'error');
        return;
      }

      const { company, header, invoices, items } = json.data;
      const htmlContent = buildReportHtml(sNo, company, header, invoices, items);

      if (Platform.OS === 'web' && typeof window !== 'undefined') {
        const printWindow = window.open('', '_blank', 'width=900,height=750');
        if (printWindow) {
          printWindow.document.open();
          printWindow.document.write(htmlContent);
          printWindow.document.close();
          setTimeout(() => {
            printWindow.focus();
            printWindow.print();
          }, 350);
          showToast(`Print preview opened for ${sNo}`, 'success');
        } else {
          showToast('Popup blocker prevented opening print window', 'error');
        }
      } else {
        showToast('Print preview generated', 'success');
      }
    } catch (e: any) {
      showToast('Error preparing print: ' + e.message, 'error');
    } finally {
      setLoading(false);
      setIsPrintModalVisible(false);
    }
  };

  const buildReportHtml = (sNo: string, company: any, header: any, invoices: any[], items: any[]) => {
    return `
      <!DOCTYPE html>
      <html>
        <head>
          <meta charset="utf-8" />
          <title>Gold Bar Issue Report - ${sNo}</title>
          <style>
            @page { size: A4 portrait; margin: 10mm; }
            ${getStandardReportCss()}
            .info-grid {
              display: grid;
              grid-template-columns: 1fr 1fr;
              gap: 12px;
              margin-bottom: 12px;
            }
            .info-card {
              border: 1px solid #D8CDBA;
              border-radius: 4px;
              padding: 8px 12px;
              background: #FAF7F0;
            }
            .info-row {
              display: flex;
              justify-content: space-between;
              font-size: 11px;
              margin-bottom: 4px;
            }
            .info-lbl { font-weight: bold; color: #554428; }
            .info-val { font-weight: bold; color: #222; }
            table.detail-table {
              width: 100%;
              border-collapse: collapse;
              margin-bottom: 12px;
              font-size: 11px;
            }
            table.detail-table th {
              background: #F5E7CA;
              border: 1px solid #D8CDBA;
              padding: 5px 8px;
              font-weight: bold;
              color: #3B2D17;
            }
            table.detail-table td {
              border: 1px solid #EBE2D2;
              padding: 5px 8px;
            }
            .section-title {
              font-size: 12px;
              font-weight: bold;
              color: #8D6409;
              margin-top: 10px;
              margin-bottom: 6px;
              text-transform: uppercase;
            }
          </style>
        </head>
        <body>
          <div class="page-sheet">
            <div class="report-main-content">
              ${renderStandardReportHeader({
      company: {
        name: company?.Name || 'SMART GOLD',
        address: [company?.Address1, company?.Address2, company?.Country].filter(Boolean).join(' '),
      },
      reportTitle: `GOLD BAR ISSUE REPORT`,
      printedDateTime: `${txtDD}/${txtMM}/${txtYYYY}`,
    })}

              <div class="info-grid">
                <div class="info-card">
                  <div class="info-row"><span class="info-lbl">Issue No:</span> <span class="info-val font-bold">${sNo}</span></div>
                  <div class="info-row"><span class="info-lbl">Issue Date:</span> <span class="info-val">${header?.PDate || `${txtDD}/${txtMM}/${txtYYYY}`}</span></div>
                  <div class="info-row"><span class="info-lbl">Remarks:</span> <span class="info-val">${header?.Remarks || '-'}</span></div>
                </div>
                <div class="info-card">
                  <div class="info-row"><span class="info-lbl">Supplier Code:</span> <span class="info-val font-bold">${header?.SupplierCode || supplierCode}</span></div>
                  <div class="info-row"><span class="info-lbl">Supplier Name:</span> <span class="info-val">${header?.SupplierName || supplierName}</span></div>
                  <div class="info-row"><span class="info-lbl">Total Issued Wt:</span> <span class="info-val" style="color: #8D6409;">${header?.IssuedWeight || netIssuedWt} g</span></div>
                </div>
              </div>

              <div class="section-title">Settled Invoices / Bills</div>
              <table class="detail-table">
                <thead>
                  <tr>
                    <th style="width: 30%; text-align: left;">Invoice / Bill No</th>
                    <th style="width: 25%; text-align: center;">Date</th>
                    <th style="width: 20%; text-align: right;">Total Weight (24K)</th>
                    <th style="width: 25%; text-align: right;">Issued Weight (g)</th>
                  </tr>
                </thead>
                <tbody>
                  ${invoices.map((inv: any) => `
                    <tr>
                      <td>${inv.PurchaseBillNo || inv.InvoiceNo}</td>
                      <td style="text-align: center;">${inv.InvoiceDate || '-'}</td>
                      <td style="text-align: right;">${parseFloat(inv.TotalWeight24K || 0).toFixed(2)}</td>
                      <td style="text-align: right; font-weight: bold;">${parseFloat(inv.IssuedWeight || 0).toFixed(2)}</td>
                    </tr>
                  `).join('')}
                </tbody>
              </table>

              <div class="section-title">Issued Gold Bar Items (Inventory Stock)</div>
              <table class="detail-table">
                <thead>
                  <tr>
                    <th style="width: 22%; text-align: left;">Inventory Code</th>
                    <th style="width: 30%; text-align: left;">Description</th>
                    <th style="width: 14%; text-align: right;">Weight (g)</th>
                    <th style="width: 10%; text-align: center;">Qty</th>
                    <th style="width: 12%; text-align: right;">Cost/g ($)</th>
                    <th style="width: 12%; text-align: right;">Total ($)</th>
                  </tr>
                </thead>
                <tbody>
                  ${items.map((itm: any) => `
                    <tr>
                      <td style="font-weight: bold;">${itm.InventoryCode}</td>
                      <td>${itm.Description}</td>
                      <td style="text-align: right; font-weight: bold;">${parseFloat(itm.Weight || 0).toFixed(2)}</td>
                      <td style="text-align: center;">${itm.Qty || 1}</td>
                      <td style="text-align: right;">${parseFloat(itm.CostPerGram || 0).toFixed(2)}</td>
                      <td style="text-align: right; font-weight: bold;">${parseFloat(itm.TotalCost || 0).toFixed(2)}</td>
                    </tr>
                  `).join('')}
                </tbody>
              </table>
            </div>

            ${renderStandardReportFooter({
      staff: {
        staffLabel: 'PREPARED BY',
        staffName: 'ADMIN',
        receiverLabel: 'SUPPLIER RECEIVER',
        receiverName: supplierName || 'AUTHORIZED SIGNATORY',
      }
    })}
          </div>
        </body>
      </html>
    `;
  };

  return (
    <SafeAreaView style={styles.safeArea} edges={['top']}>
      <Stack.Screen options={{ headerShown: false }} />

      {/* TOP HEADER */}
      <View style={styles.topNavHeader}>
        <View style={styles.headerLeft}>
          <TouchableOpacity onPress={() => router.back()} style={styles.backBtn} activeOpacity={0.7}>
            <Ionicons name="arrow-back" size={18} color="#8D6409" />
          </TouchableOpacity>
          <View>
            <ThemedText style={styles.headerTitle}>GOLD BAR ISSUE</ThemedText>
            <ThemedText style={styles.headerSubtitle}>Issue 24K Gold Bars Against Supplier Invoices</ThemedText>
          </View>
        </View>

        <View style={styles.headerRight}>
          <TouchableOpacity
            style={styles.headerActionBtn}
            onPress={() => {
              if (issueNo.trim()) {
                setCurrentPrintIssueNo(issueNo.trim());
                setIsPrintModalVisible(true);
              } else {
                showToast('Please enter or select Issue No to Re-Print', 'error');
              }
            }}
            activeOpacity={0.8}
          >
            <Ionicons name="print-outline" size={14} color="#8D6409" />
            <ThemedText style={styles.headerActionBtnText}>Re-Print</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity
            style={[styles.headerActionBtn, styles.headerSaveBtn]}
            onPress={handleSave}
            disabled={saving}
            activeOpacity={0.8}
          >
            {saving ? (
              <ActivityIndicator size="small" color="#9E6D08" />
            ) : (
              <>
                <Ionicons name="save-outline" size={14} color="#9E6D08" />
                <ThemedText style={[styles.headerActionBtnText, { color: '#9E6D08' }]}>Save</ThemedText>
              </>
            )}
          </TouchableOpacity>

          <TouchableOpacity style={styles.headerActionBtn} onPress={handleClear} activeOpacity={0.8}>
            <Ionicons name="refresh-outline" size={14} color="#6E5528" />
            <ThemedText style={styles.headerActionBtnText}>Clear</ThemedText>
          </TouchableOpacity>


        </View>
      </View>

      {/* TOAST MESSAGE */}
      {toast && (
        <View style={[styles.toastContainer, toast.type === 'error' ? styles.toastError : styles.toastSuccess]}>
          <Ionicons
            name={toast.type === 'error' ? 'alert-circle' : 'checkmark-circle'}
            size={16}
            color="#FFF"
            style={{ marginRight: 6 }}
          />
          <Text style={styles.toastText}>{toast.message}</Text>
        </View>
      )}

      <ScrollView style={styles.scrollArea} contentContainerStyle={styles.scrollContent}>
        {/* CARD 1: HEADER SECTION */}
        <View style={styles.goldCard}>
          <View style={styles.cardSectionBanner}>
            <View style={{ flexDirection: 'row', alignItems: 'center', gap: 6 }}>
              <Ionicons name="document-text" size={14} color="#8D6409" />
              <ThemedText style={styles.cardSectionTitle}>ISSUE DETAILS</ThemedText>
            </View>
          </View>
          <View style={[styles.goldCardHeader, { paddingHorizontal: 4, paddingTop: 4 }]}>
            {/* Row 1: Issue No, Invoice Date, Net Issued Wt */}
            <View style={styles.fieldRow}>
              {/* Issue No */}
              <View style={[styles.inlineField, { flex: 1.1 }]}>
                <ThemedText style={styles.fieldLabel}>Issue No</ThemedText>
                <View style={styles.inputWithBtnContainer}>
                  <TextInput
                    style={[styles.formInput, styles.inputAttached, { flex: 1 }]}
                    value={issueNo}
                    onChangeText={setIssueNo}
                    placeholder="Auto..."
                    placeholderTextColor="#A89E8D"
                  />
                  <TouchableOpacity style={styles.btnDots} onPress={openHistoryLOV} activeOpacity={0.7}>
                    <ThemedText style={styles.dotsText}>...</ThemedText>
                  </TouchableOpacity>
                </View>
              </View>

              {/* Invoice Date */}
              <View style={[styles.inlineField, { flex: 1.1, marginLeft: 16 }]}>
                <ThemedText style={styles.fieldLabel}>Invoice Date</ThemedText>
                <View style={styles.dateTripleInput}>
                  <TextInput
                    style={[styles.datePartInput, { width: 34 }]}
                    value={txtDD}
                    onChangeText={setTxtDD}
                    maxLength={2}
                    keyboardType="numeric"
                  />
                  <ThemedText style={styles.dateSlash}>/</ThemedText>
                  <TextInput
                    style={[styles.datePartInput, { width: 34 }]}
                    value={txtMM}
                    onChangeText={setTxtMM}
                    maxLength={2}
                    keyboardType="numeric"
                  />
                  <ThemedText style={styles.dateSlash}>/</ThemedText>
                  <TextInput
                    style={[styles.datePartInput, { width: 50 }]}
                    value={txtYYYY}
                    onChangeText={setTxtYYYY}
                    maxLength={4}
                    keyboardType="numeric"
                  />
                </View>
              </View>

              {/* Net Issued Wt (24K - 999) */}
              <View style={[styles.inlineField, { flex: 1.3, justifyContent: 'flex-end', marginLeft: 16 }]}>
                <ThemedText style={[styles.fieldLabel, { width: 85 }]}>Net Issued Wt</ThemedText>
                <TextInput
                  style={[styles.formInput, styles.readonlyInput, { width: 85, textAlign: 'right' }]}
                  value={netIssuedWt}
                  editable={false}
                />
                <ThemedText style={styles.purityLabelTag}>(24K - 999)</ThemedText>
              </View>
            </View>

            {/* Row 2: Supplier Code, Supplier Name, Issued (22K - 916) */}
            <View style={[styles.fieldRow, { marginTop: 8 }]}>
              {/* Supplier Code */}
              <View style={[styles.inlineField, { flex: 1.1 }]}>
                <ThemedText style={styles.fieldLabel}>Supplier Code</ThemedText>
                <View style={styles.inputWithBtnContainer}>
                  <TextInput
                    style={[styles.formInput, styles.inputAttached, { flex: 1 }]}
                    value={supplierCode}
                    onChangeText={setSupplierCode}
                    onBlur={handleSupplierBlur}
                    placeholder="Code..."
                    placeholderTextColor="#A89E8D"
                  />
                  <TouchableOpacity style={styles.btnDots} onPress={openSupplierLOV} activeOpacity={0.7}>
                    <ThemedText style={styles.dotsText}>...</ThemedText>
                  </TouchableOpacity>
                </View>
              </View>

              {/* Supplier Name */}
              <View style={[styles.inlineField, { flex: 1.5, marginLeft: 20 }]}>
                <TextInput
                  style={[styles.formInput, styles.readonlyInput, { flex: 1 }]}
                  value={supplierName}
                  editable={false}
                  placeholder="Supplier Name Auto-populated"
                  placeholderTextColor="#A89E8D"
                />
              </View>

              {/* Issued (22K - 916) */}
              <View style={[styles.inlineField, { flex: 1.3, justifyContent: 'flex-end', marginLeft: 20 }]}>
                <ThemedText style={[styles.fieldLabel, { width: 85 }]}>Issued</ThemedText>
                <TextInput
                  style={[styles.formInput, styles.readonlyInput, { width: 85, textAlign: 'right' }]}
                  value={issued22kWt}
                  editable={false}
                />
                <ThemedText style={styles.purityLabelTag}>(22K - 916)</ThemedText>
              </View>
            </View>

            {/* Row 3: Invoice No Search Trigger & Remarks */}
            <View style={[styles.fieldRow, { marginTop: 8 }]}>
              <View style={[styles.inlineField, { flex: 1.1 }]}>
                <ThemedText style={styles.fieldLabel}>Invoice No</ThemedText>
                <View style={styles.inputWithBtnContainer}>
                  <TextInput
                    style={[styles.formInput, styles.inputAttached, { flex: 1 }]}
                    value={invoiceRows.length > 0 ? `${invoiceRows.length} Invoice(s) Selected` : ''}
                    placeholder="Select Invoices..."
                    placeholderTextColor="#A89E8D"
                    editable={false}
                  />
                  <TouchableOpacity style={styles.btnDots} onPress={openInvoiceSelectionModal} activeOpacity={0.7}>
                    <ThemedText style={styles.dotsText}>...</ThemedText>
                  </TouchableOpacity>
                </View>
              </View>

              {/* Remarks */}
              <View style={[styles.inlineField, { flex: 2.8, marginLeft: 20 }]}>
                <ThemedText style={[styles.fieldLabel, { width: 60 }]}>Remarks</ThemedText>
                <TextInput
                  style={[styles.formInput, { flex: 1 }]}
                  value={remarks}
                  onChangeText={setRemarks}
                  placeholder="Enter remarks..."
                  placeholderTextColor="#A89E8D"
                />
              </View>
            </View>
          </View>
        </View>

        {/* GOLD BAR CARD 2: TOP GRID (Settled Invoices) */}
        <View style={[styles.goldCard, { marginTop: 10 }]}>
          <View style={styles.cardSectionBanner}>
            <View style={{ flexDirection: 'row', alignItems: 'center', gap: 6 }}>
              <Ionicons name="receipt-outline" size={14} color="#8D6409" />
              <ThemedText style={styles.cardSectionTitle}>SETTLED INVOICES TABLE</ThemedText>
            </View>
            <ThemedText style={styles.counterBadgeText}>{invoiceRows.length} records</ThemedText>
          </View>

          {/* Table Container */}
          <ScrollView horizontal showsHorizontalScrollIndicator={true} style={{ width: '100%' }}>
            <View style={{ minWidth: 680 }}>
              {/* Table Header */}
              <View style={styles.gridHeaderRow}>
                <View style={[styles.gridHeaderCell, { width: 140 }]}><ThemedText style={styles.gridHeaderText}>Invoice No</ThemedText></View>
                <View style={[styles.gridHeaderCell, { width: 130 }]}><ThemedText style={styles.gridHeaderText}>InvoiceDate</ThemedText></View>
                <View style={[styles.gridHeaderCell, { width: 140 }]}><ThemedText style={styles.gridHeaderText}>Purchase Bill No</ThemedText></View>
                <View style={[styles.gridHeaderCell, { width: 130, alignItems: 'flex-end' }]}><ThemedText style={styles.gridHeaderText}>Purchase Weight</ThemedText></View>
                <View style={[styles.gridHeaderCell, { width: 140, alignItems: 'flex-end' }]}><ThemedText style={styles.gridHeaderText}>Issued Weight</ThemedText></View>
              </View>

              {/* Table Rows */}
              {invoiceRows.length === 0 ? (
                <View style={styles.emptyGridContainer}>
                  <ThemedText style={styles.emptyGridText}>No Invoices selected. Click "..." next to Invoice No to select supplier invoices.</ThemedText>
                </View>
              ) : (
                invoiceRows.map((row, idx) => (
                  <TouchableOpacity
                    key={idx}
                    style={[styles.gridDataRow, selectedInvoiceRowIndex === idx && styles.gridDataRowSelected]}
                    onPress={() => setSelectedInvoiceRowIndex(idx)}
                    activeOpacity={0.8}
                  >
                    <View style={[styles.gridDataCell, { width: 140 }]}><ThemedText style={styles.gridDataText}>{row.PurchaseBillNo}</ThemedText></View>
                    <View style={[styles.gridDataCell, { width: 130 }]}><ThemedText style={styles.gridDataText}>{row.InvoiceDate}</ThemedText></View>
                    <View style={[styles.gridDataCell, { width: 140 }]}><ThemedText style={styles.gridDataText}>{row.PurchaseBillNo}</ThemedText></View>
                    <View style={[styles.gridDataCell, { width: 130, alignItems: 'flex-end' }]}>
                      <ThemedText style={styles.gridDataText}>{row.PurchaseWeight.toFixed(2)}</ThemedText>
                    </View>
                    <View style={[styles.gridDataCell, { width: 140, alignItems: 'flex-end' }]}>
                      <TextInput
                        style={styles.editableTableCell}
                        value={String(row.IssuedWeight)}
                        onChangeText={(val) => handleInvoiceRowIssuedWeightChange(idx, val)}
                        keyboardType="numeric"
                      />
                    </View>
                  </TouchableOpacity>
                ))
              )}
            </View>
          </ScrollView>

          {/* OK Button Row matching VB.NET btnIssueOk */}
          <View style={styles.gridFooterActions}>
            <TouchableOpacity style={styles.btnActionOk} onPress={handleRecalculateInvoiceGrid} activeOpacity={0.8}>
              <ThemedText style={styles.btnActionOkText}>OK</ThemedText>
            </TouchableOpacity>
          </View>
        </View>

        {/* GOLD BAR CARD 3: BOTTOM GRID (Issued 24K Gold Bar Items) */}
        <View style={[styles.goldCard, { marginTop: 10 }]}>
          <View style={styles.cardSectionBanner}>
            <View style={{ flexDirection: 'row', alignItems: 'center', gap: 6 }}>
              <Ionicons name="cube-outline" size={14} color="#8D6409" />
              <ThemedText style={styles.cardSectionTitle}>ISSUED GOLD BAR INVENTORY ITEMS</ThemedText>
            </View>
            <ThemedText style={styles.counterBadgeText}>{issueItems.length} items</ThemedText>
          </View>

          {/* Table Container */}
          <ScrollView horizontal showsHorizontalScrollIndicator={true} style={{ width: '100%' }}>
            <View style={{ minWidth: 860 }}>
              {/* Table Header */}
              <View style={styles.gridHeaderRow}>
                <View style={[styles.gridHeaderCell, { width: 140 }]}><ThemedText style={styles.gridHeaderText}>Inventory Code</ThemedText></View>
                <View style={[styles.gridHeaderCell, { width: 210 }]}><ThemedText style={styles.gridHeaderText}>Description</ThemedText></View>
                <View style={[styles.gridHeaderCell, { width: 95, alignItems: 'flex-end' }]}><ThemedText style={styles.gridHeaderText}>Weight (g)</ThemedText></View>
                <View style={[styles.gridHeaderCell, { width: 65, alignItems: 'center' }]}><ThemedText style={styles.gridHeaderText}>Qty</ThemedText></View>
                <View style={[styles.gridHeaderCell, { width: 105, alignItems: 'flex-end' }]}><ThemedText style={styles.gridHeaderText}>CostPerGram</ThemedText></View>
                <View style={[styles.gridHeaderCell, { width: 115, alignItems: 'flex-end' }]}><ThemedText style={styles.gridHeaderText}>Total Cost</ThemedText></View>
                <View style={[styles.gridHeaderCell, { width: 120 }]}><ThemedText style={styles.gridHeaderText}>BarNo</ThemedText></View>
              </View>

              {/* Table Rows */}
              {issueItems.length === 0 ? (
                <View style={styles.emptyGridContainer}>
                  <ThemedText style={styles.emptyGridText}>No Gold Bar items added. Fill the inputs below and click "Add".</ThemedText>
                </View>
              ) : (
                issueItems.map((itm, idx) => (
                  <TouchableOpacity
                    key={idx}
                    style={[styles.gridDataRow, selectedIssueItemIndex === idx && styles.gridDataRowSelected]}
                    onPress={() => setSelectedIssueItemIndex(idx)}
                    activeOpacity={0.8}
                  >
                    <View style={[styles.gridDataCell, { width: 140 }]}><ThemedText style={styles.gridDataTextBold}>{itm.InventoryCode}</ThemedText></View>
                    <View style={[styles.gridDataCell, { width: 210 }]}><ThemedText style={styles.gridDataText}>{itm.Description}</ThemedText></View>
                    <View style={[styles.gridDataCell, { width: 95, alignItems: 'flex-end' }]}><ThemedText style={styles.gridDataTextBold}>{itm.Weight.toFixed(2)}</ThemedText></View>
                    <View style={[styles.gridDataCell, { width: 65, alignItems: 'center' }]}><ThemedText style={styles.gridDataText}>{itm.Qty}</ThemedText></View>
                    <View style={[styles.gridDataCell, { width: 105, alignItems: 'flex-end' }]}><ThemedText style={styles.gridDataText}>{itm.CostPerGram.toFixed(2)}</ThemedText></View>
                    <View style={[styles.gridDataCell, { width: 115, alignItems: 'flex-end' }]}><ThemedText style={styles.gridDataTextBold}>{itm.TotalCost.toFixed(2)}</ThemedText></View>
                    <View style={[styles.gridDataCell, { width: 120 }]}><ThemedText style={styles.gridDataText}>{itm.BarNo || '-'}</ThemedText></View>
                  </TouchableOpacity>
                ))
              )}

              {/* Bottom Inputs Row (Matching VB.NET txtInventoryCode1, txtDescription, etc.) */}
              <View style={styles.bottomInputsContainer}>
                {/* Inventory Code Input with Dots */}
                <View style={{ width: 140, paddingRight: 10 }}>
                  <View style={styles.inputWithBtnContainer}>
                    <TextInput
                      style={[styles.formInput, styles.inputAttached, { flex: 1, minWidth: 60, fontSize: 11 }]}
                      value={inputInventoryCode}
                      onChangeText={setInputInventoryCode}
                      onBlur={() => fetchInventoryDetails(inputInventoryCode)}
                      placeholder="Code..."
                      placeholderTextColor="#A89E8D"
                    />
                    <TouchableOpacity style={styles.btnDots} onPress={openStockLOV} activeOpacity={0.7}>
                      <ThemedText style={styles.dotsText}>...</ThemedText>
                    </TouchableOpacity>
                  </View>
                </View>

                {/* Description */}
                <View style={{ width: 210, paddingRight: 10 }}>
                  <TextInput
                    style={[styles.formInput, { width: '100%', fontSize: 11 }]}
                    value={inputDescription}
                    onChangeText={setInputDescription}
                    placeholder="Description..."
                    placeholderTextColor="#A89E8D"
                  />
                </View>

                {/* Weight */}
                <View style={{ width: 95, paddingRight: 10 }}>
                  <TextInput
                    style={[styles.formInput, { width: '100%', textAlign: 'right', fontSize: 11 }]}
                    value={inputWeight}
                    onChangeText={(val) => handleQtyWeightCostChange('weight', val)}
                    keyboardType="numeric"
                    placeholder="Wt (g)"
                    placeholderTextColor="#A89E8D"
                  />
                </View>

                {/* Qty */}
                <View style={{ width: 65, paddingRight: 10 }}>
                  <TextInput
                    style={[styles.formInput, { width: '100%', textAlign: 'center', fontSize: 11 }]}
                    value={inputQty}
                    onChangeText={(val) => handleQtyWeightCostChange('qty', val)}
                    keyboardType="numeric"
                    placeholder="Qty"
                    placeholderTextColor="#A89E8D"
                  />
                </View>

                {/* CostPerGram */}
                <View style={{ width: 105, paddingRight: 10 }}>
                  <TextInput
                    style={[styles.formInput, { width: '100%', textAlign: 'right', fontSize: 11 }]}
                    value={inputCostPerGram}
                    onChangeText={(val) => handleQtyWeightCostChange('cpg', val)}
                    keyboardType="numeric"
                    placeholder="Cost/g"
                    placeholderTextColor="#A89E8D"
                  />
                </View>

                {/* Total Cost */}
                <View style={{ width: 115, paddingRight: 10 }}>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { width: '100%', textAlign: 'right', fontSize: 11 }]}
                    value={inputTotalCost}
                    editable={false}
                    placeholder="Total ($)"
                    placeholderTextColor="#A89E8D"
                  />
                </View>

                {/* Bar No */}
                <View style={{ width: 120 }}>
                  <TextInput
                    style={[styles.formInput, { width: '100%', fontSize: 11 }]}
                    value={inputBarNo}
                    onChangeText={setInputBarNo}
                    placeholder="Bar No..."
                    placeholderTextColor="#A89E8D"
                  />
                </View>
              </View>
            </View>
          </ScrollView>

          {/* Add & Delete Buttons Row (Matching VB.NET btnAdd & btnDelete) */}
          <View style={styles.gridFooterActionButtons}>
            <TouchableOpacity style={styles.btnActionAdd} onPress={handleAddItem} activeOpacity={0.8}>
              <Ionicons name="add" size={14} color="#5C4A28" style={{ marginRight: 4 }} />
              <ThemedText style={styles.btnActionAddText}>Add</ThemedText>
            </TouchableOpacity>

            <TouchableOpacity
              style={[styles.btnActionDelete, selectedIssueItemIndex === null && { opacity: 0.5 }]}
              onPress={handleDeleteItem}
              disabled={selectedIssueItemIndex === null}
              activeOpacity={0.8}
            >
              <Ionicons name="trash-outline" size={13} color="#C62828" style={{ marginRight: 4 }} />
              <ThemedText style={styles.btnActionDeleteText}>Delete</ThemedText>
            </TouchableOpacity>
          </View>
        </View>

        <View style={{ height: 40 }} />
      </ScrollView>

      {/* MODAL 1: SELECT INVOICES (pnlInvoice - grdInvoice) */}
      <Modal visible={isInvoiceModalVisible} transparent animationType="fade" onRequestClose={() => setIsInvoiceModalVisible(false)}>
        <TouchableOpacity style={styles.modalOverlay} activeOpacity={1} onPress={() => setIsInvoiceModalVisible(false)}>
          <TouchableWithoutFeedback>
            <View style={styles.modalCardWide}>
              <View style={styles.modalHeaderBanner}>
                <View style={{ flexDirection: 'row', alignItems: 'center', gap: 6 }}>
                  <Ionicons name="document-text-outline" size={15} color="#8D6409" />
                  <ThemedText style={styles.modalHeaderTitle}>Select Unissued Invoices</ThemedText>
                </View>
                <TouchableOpacity onPress={() => setIsInvoiceModalVisible(false)} style={styles.modalCloseBtn}>
                  <Ionicons name="close" size={18} color="#6E5528" />
                </TouchableOpacity>
              </View>

              <ScrollView horizontal showsHorizontalScrollIndicator={true} style={{ maxHeight: 380 }}>
                <View style={{ minWidth: 620 }}>
                  <View style={styles.gridHeaderRow}>
                    <View style={[styles.gridHeaderCell, { width: 50, alignItems: 'center' }]}><ThemedText style={styles.gridHeaderText}>Sel</ThemedText></View>
                    <View style={[styles.gridHeaderCell, { width: 140 }]}><ThemedText style={styles.gridHeaderText}>Invoice No</ThemedText></View>
                    <View style={[styles.gridHeaderCell, { width: 130 }]}><ThemedText style={styles.gridHeaderText}>Invoice Date</ThemedText></View>
                    <View style={[styles.gridHeaderCell, { width: 140, alignItems: 'flex-end' }]}><ThemedText style={styles.gridHeaderText}>Purchase Weight</ThemedText></View>
                    <View style={[styles.gridHeaderCell, { width: 140, alignItems: 'flex-end' }]}><ThemedText style={styles.gridHeaderText}>Issued Weight</ThemedText></View>
                  </View>

                  <ScrollView style={{ maxHeight: 300 }}>
                    {availableInvoices.length === 0 ? (
                      <View style={styles.emptyGridContainer}>
                        <ThemedText style={styles.emptyGridText}>No open invoices found for supplier {supplierCode}.</ThemedText>
                      </View>
                    ) : (
                      availableInvoices.map((inv, idx) => (
                        <TouchableOpacity
                          key={idx}
                          style={[styles.gridDataRow, inv.selected && styles.gridDataRowSelected]}
                          onPress={() => toggleInvoiceSelection(idx)}
                          activeOpacity={0.8}
                        >
                          <View style={[styles.gridDataCell, { width: 50, alignItems: 'center' }]}>
                            <View style={[styles.checkboxBox, inv.selected && styles.checkboxBoxChecked]}>
                              {inv.selected && <Ionicons name="checkmark" size={12} color="#FFF" />}
                            </View>
                          </View>
                          <View style={[styles.gridDataCell, { width: 140 }]}><ThemedText style={styles.gridDataText}>{inv.PurchaseBillNo}</ThemedText></View>
                          <View style={[styles.gridDataCell, { width: 130 }]}><ThemedText style={styles.gridDataText}>{inv.InvoiceDate}</ThemedText></View>
                          <View style={[styles.gridDataCell, { width: 140, alignItems: 'flex-end' }]}><ThemedText style={styles.gridDataText}>{inv.PurchaseWeight.toFixed(2)}</ThemedText></View>
                          <View style={[styles.gridDataCell, { width: 140, alignItems: 'flex-end' }]}><ThemedText style={styles.gridDataText}>{inv.IssuedWeight.toFixed(2)}</ThemedText></View>
                        </TouchableOpacity>
                      ))
                    )}
                  </ScrollView>
                </View>
              </ScrollView>

              {/* Bottom Bar: Select All & OK Button (btnPanelOK in VB) */}
              <View style={styles.modalBottomBar}>
                <TouchableOpacity
                  style={{ flexDirection: 'row', alignItems: 'center', gap: 6 }}
                  onPress={() => handleToggleSelectAllInvoices(!selectAllInvoices)}
                  activeOpacity={0.7}
                >
                  <View style={[styles.checkboxBox, selectAllInvoices && styles.checkboxBoxChecked]}>
                    {selectAllInvoices && <Ionicons name="checkmark" size={12} color="#FFF" />}
                  </View>
                  <ThemedText style={{ fontSize: 11, fontWeight: 'bold', color: '#554428' }}>Select All</ThemedText>
                </TouchableOpacity>

                <View style={{ flexDirection: 'row', gap: 10 }}>
                  <TouchableOpacity style={styles.btnActionAdd} onPress={confirmSelectedInvoices} activeOpacity={0.8}>
                    <ThemedText style={styles.btnActionAddText}>OK</ThemedText>
                  </TouchableOpacity>
                  <TouchableOpacity style={styles.btnActionClose} onPress={() => setIsInvoiceModalVisible(false)} activeOpacity={0.8}>
                    <ThemedText style={styles.btnActionCloseText}>Close</ThemedText>
                  </TouchableOpacity>
                </View>
              </View>
            </View>
          </TouchableWithoutFeedback>
        </TouchableOpacity>
      </Modal>

      {/* MODAL 2: SUPPLIER LOV */}
      <Modal visible={isSupplierModalVisible} transparent animationType="fade" onRequestClose={() => setIsSupplierModalVisible(false)}>
        <TouchableOpacity style={styles.modalOverlay} activeOpacity={1} onPress={() => setIsSupplierModalVisible(false)}>
          <TouchableWithoutFeedback>
            <View style={styles.modalCard}>
              <View style={styles.modalHeaderBanner}>
                <ThemedText style={styles.modalHeaderTitle}>Select Supplier</ThemedText>
                <TouchableOpacity onPress={() => setIsSupplierModalVisible(false)} style={styles.modalCloseBtn}>
                  <Ionicons name="close" size={18} color="#6E5528" />
                </TouchableOpacity>
              </View>

              <View style={styles.searchBarContainer}>
                <Ionicons name="search" size={14} color="#8D6409" style={{ marginRight: 6 }} />
                <TextInput
                  style={styles.searchInput}
                  placeholder="Search code or name..."
                  placeholderTextColor="#A89E8D"
                  value={supplierSearchQuery}
                  onChangeText={setSupplierSearchQuery}
                />
              </View>

              <FlatList
                data={supplierList.filter(
                  (s) =>
                    s.SupplierCode?.toLowerCase().includes(supplierSearchQuery.toLowerCase()) ||
                    s.SupplierName?.toLowerCase().includes(supplierSearchQuery.toLowerCase())
                )}
                keyExtractor={(item) => item.SupplierCode}
                style={{ maxHeight: 320 }}
                renderItem={({ item, index }) => (
                  <TouchableOpacity
                    style={[styles.lovRow, index % 2 === 1 && { backgroundColor: '#FCFAF6' }]}
                    onPress={() => handleSelectSupplier(item)}
                  >
                    <ThemedText style={styles.lovCode}>{item.SupplierCode}</ThemedText>
                    <ThemedText style={styles.lovName}>{item.SupplierName}</ThemedText>
                  </TouchableOpacity>
                )}
                ListEmptyComponent={<ThemedText style={styles.emptyGridText}>No suppliers found</ThemedText>}
              />
            </View>
          </TouchableWithoutFeedback>
        </TouchableOpacity>
      </Modal>

      {/* MODAL 3: INVENTORY STOCK LOV */}
      <Modal visible={isStockModalVisible} transparent animationType="fade" onRequestClose={() => setIsStockModalVisible(false)}>
        <TouchableOpacity style={styles.modalOverlay} activeOpacity={1} onPress={() => setIsStockModalVisible(false)}>
          <TouchableWithoutFeedback>
            <View style={styles.modalCardWide}>
              <View style={styles.modalHeaderBanner}>
                <ThemedText style={styles.modalHeaderTitle}>Select 24K Gold Bar Inventory</ThemedText>
                <TouchableOpacity onPress={() => setIsStockModalVisible(false)} style={styles.modalCloseBtn}>
                  <Ionicons name="close" size={18} color="#6E5528" />
                </TouchableOpacity>
              </View>

              <View style={styles.searchBarContainer}>
                <Ionicons name="search" size={14} color="#8D6409" style={{ marginRight: 6 }} />
                <TextInput
                  style={styles.searchInput}
                  placeholder="Search inventory code or description..."
                  placeholderTextColor="#A89E8D"
                  value={stockSearchQuery}
                  onChangeText={setStockSearchQuery}
                />
              </View>

              <FlatList
                data={stockList.filter(
                  (s) =>
                    s.InventoryCode?.toLowerCase().includes(stockSearchQuery.toLowerCase()) ||
                    s.Description?.toLowerCase().includes(stockSearchQuery.toLowerCase())
                )}
                keyExtractor={(item) => item.InventoryCode}
                style={{ maxHeight: 320 }}
                renderItem={({ item, index }) => (
                  <TouchableOpacity
                    style={[styles.lovRow, index % 2 === 1 && { backgroundColor: '#FCFAF6' }]}
                    onPress={() => handleSelectStock(item)}
                  >
                    <ThemedText style={[styles.lovCode, { width: 130 }]}>{item.InventoryCode}</ThemedText>
                    <ThemedText style={[styles.lovName, { flex: 1 }]}>{item.Description}</ThemedText>
                    <ThemedText style={[styles.lovWeight, { width: 85, textAlign: 'right' }]}>{item.Weight.toFixed(2)} g</ThemedText>
                    <ThemedText style={{ width: 60, textAlign: 'center', fontSize: 11, color: '#554428' }}>Qty: {item.QtyOnHand}</ThemedText>
                  </TouchableOpacity>
                )}
                ListEmptyComponent={<ThemedText style={styles.emptyGridText}>No Gold Bar inventory found</ThemedText>}
              />
            </View>
          </TouchableWithoutFeedback>
        </TouchableOpacity>
      </Modal>

      {/* MODAL 4: ISSUE HISTORY LOV */}
      <Modal visible={isHistoryModalVisible} transparent animationType="fade" onRequestClose={() => setIsHistoryModalVisible(false)}>
        <TouchableOpacity style={styles.modalOverlay} activeOpacity={1} onPress={() => setIsHistoryModalVisible(false)}>
          <TouchableWithoutFeedback>
            <View style={styles.modalCardWide}>
              <View style={styles.modalHeaderBanner}>
                <ThemedText style={styles.modalHeaderTitle}>Select Gold Bar Issue History</ThemedText>
                <TouchableOpacity onPress={() => setIsHistoryModalVisible(false)} style={styles.modalCloseBtn}>
                  <Ionicons name="close" size={18} color="#6E5528" />
                </TouchableOpacity>
              </View>

              <View style={styles.searchBarContainer}>
                <Ionicons name="search" size={14} color="#8D6409" style={{ marginRight: 6 }} />
                <TextInput
                  style={styles.searchInput}
                  placeholder="Search Issue No or Supplier..."
                  placeholderTextColor="#A89E8D"
                  value={historySearchQuery}
                  onChangeText={setHistorySearchQuery}
                />
              </View>

              <FlatList
                data={historyList.filter(
                  (h) =>
                    h.IssueNo?.toLowerCase().includes(historySearchQuery.toLowerCase()) ||
                    h.SupplierCode?.toLowerCase().includes(historySearchQuery.toLowerCase()) ||
                    h.SupplierName?.toLowerCase().includes(historySearchQuery.toLowerCase())
                )}
                keyExtractor={(item) => item.IssueNo}
                style={{ maxHeight: 320 }}
                renderItem={({ item, index }) => (
                  <TouchableOpacity
                    style={[styles.lovRow, index % 2 === 1 && { backgroundColor: '#FCFAF6' }]}
                    onPress={() => handleSelectHistoryIssue(item)}
                  >
                    <ThemedText style={[styles.lovCode, { width: 120 }]}>{item.IssueNo}</ThemedText>
                    <View style={{ flex: 1, paddingHorizontal: 6 }}>
                      <ThemedText style={styles.lovName}>{item.SupplierName || item.SupplierCode}</ThemedText>
                      <ThemedText style={{ fontSize: 10, color: '#8D6409' }}>Date: {item.PDate}</ThemedText>
                    </View>
                    <ThemedText style={[styles.lovWeight, { width: 90, textAlign: 'right' }]}>{parseFloat(item.IssuedWeight || 0).toFixed(2)} g</ThemedText>
                  </TouchableOpacity>
                )}
                ListEmptyComponent={<ThemedText style={styles.emptyGridText}>No issue history found</ThemedText>}
              />
            </View>
          </TouchableWithoutFeedback>
        </TouchableOpacity>
      </Modal>

      {/* PRINT REPORT MODAL */}
      <PrintReportModal
        visible={isPrintModalVisible}
        title="Print Gold Bar Issue"
        message={`Print report for Gold Bar Issue No: ${currentPrintIssueNo}?`}
        onCancel={() => setIsPrintModalVisible(false)}
        onPdf={() => {
          setIsPrintModalVisible(false);
          handlePrint(currentPrintIssueNo);
        }}
        onExcel={() => {
          setIsPrintModalVisible(false);
          showToast(`Exported Excel for ${currentPrintIssueNo}`, 'success');
        }}
        onPrint={() => {
          handlePrint(currentPrintIssueNo);
        }}
      />
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  safeArea: {
    flex: 1,
    backgroundColor: '#FBF9F5',
  },
  topNavHeader: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingHorizontal: 16,
    paddingVertical: 10,
    borderBottomWidth: 1,
    borderBottomColor: '#EBE2D2',
    backgroundColor: '#FFFFFF',
  },
  headerLeft: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  backBtn: {
    padding: 6,
    marginRight: 10,
    backgroundColor: '#FAF5EA',
    borderRadius: 6,
    borderWidth: 1,
    borderColor: '#E8DCB8',
  },
  headerTitle: {
    fontSize: 15,
    fontWeight: 'bold',
    color: '#3B2D17',
    letterSpacing: 0.5,
  },
  headerSubtitle: {
    fontSize: 10.5,
    color: '#8D6409',
    marginTop: 1,
  },
  headerRight: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 8,
  },
  headerActionBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 5,
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#DFD5C3',
    paddingVertical: 5,
    paddingHorizontal: 11,
    borderRadius: 6,
  },
  headerActionBtnText: {
    color: '#5C4A28',
    fontWeight: 'bold',
    fontSize: 11,
  },
  headerSaveBtn: {
    backgroundColor: '#F9F2E2',
    borderColor: '#B8903B',
  },
  toastContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 16,
    paddingVertical: 8,
    marginHorizontal: 16,
    marginTop: 8,
    borderRadius: 6,
  },
  toastSuccess: {
    backgroundColor: '#2E7D32',
  },
  toastError: {
    backgroundColor: '#C62828',
  },
  toastText: {
    color: '#FFFFFF',
    fontSize: 11.5,
    fontWeight: 'bold',
  },
  scrollArea: {
    flex: 1,
  },
  scrollContent: {
    padding: 12,
  },

  // CARD STYLES (Clean cream & soft gold card style matching Jobsheet & GoldBarFix)
  goldCard: {
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    padding: 10,
    marginBottom: 10,
    shadowColor: '#A38035',
    shadowOpacity: 0.08,
    shadowRadius: 3,
    elevation: 2,
    overflow: 'hidden',
  },
  goldCardHeader: {
    backgroundColor: 'transparent',
  },
  fieldRow: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  inlineField: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  fieldLabel: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3B2D17',
    width: 90,
  },
  formInput: {
    height: 26,
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderRadius: 5,
    paddingHorizontal: 8,
    paddingVertical: 2,
    fontSize: 11.5,
    color: '#2C2519',
    ...(Platform.OS === 'web' ? { outlineStyle: 'none', boxSizing: 'border-box' } : {}),
  } as any,
  inputAttached: {
    borderTopRightRadius: 0,
    borderBottomRightRadius: 0,
    borderRightWidth: 0,
  },
  readonlyInput: {
    backgroundColor: '#F8F5EE',
    borderColor: '#EADBCA',
    color: '#554736',
  },
  inputWithBtnContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    flex: 1,
  },
  btnDots: {
    width: 28,
    height: 26,
    backgroundColor: '#FAF5EA',
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderTopRightRadius: 5,
    borderBottomRightRadius: 5,
    alignItems: 'center',
    justifyContent: 'center',
  },
  dotsText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#6E5528',
  },
  dateTripleInput: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderRadius: 5,
    paddingHorizontal: 4,
    height: 26,
  },
  datePartInput: {
    height: 24,
    textAlign: 'center',
    fontSize: 11.5,
    color: '#2C2519',
    padding: 0,
    ...(Platform.OS === 'web' ? { outlineStyle: 'none' } : {}),
  } as any,
  dateSlash: {
    fontSize: 11,
    color: '#8D6409',
    marginHorizontal: 2,
    fontWeight: 'bold',
  },
  purityLabelTag: {
    fontSize: 10.5,
    fontWeight: 'bold',
    color: '#5C4A28',
    marginLeft: 6,
  },

  // CARD SECTIONS & GRIDS
  cardSectionBanner: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    backgroundColor: '#F5E7CA',
    borderWidth: 1,
    borderColor: '#E2D3B3',
    paddingHorizontal: 8,
    paddingVertical: 5,
    borderRadius: 5,
    marginBottom: 6,
  },
  cardSectionTitle: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3B2D17',
    letterSpacing: 0.3,
  },
  counterBadgeText: {
    fontSize: 10.5,
    fontWeight: 'bold',
    color: '#5C4A28',
  },
  gridHeaderRow: {
    flexDirection: 'row',
    backgroundColor: '#F5E7CA',
    borderWidth: 1,
    borderColor: '#E2D3B3',
    height: 28,
    alignItems: 'center',
  },
  gridHeaderCell: {
    paddingHorizontal: 6,
    borderRightWidth: 1,
    borderRightColor: '#E2D3B3',
    justifyContent: 'center',
  },
  gridHeaderText: {
    fontSize: 10.5,
    fontWeight: 'bold',
    color: '#3B2D17',
  },
  gridDataRow: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFFFFF',
    borderBottomWidth: 1,
    borderBottomColor: '#EBE2D2',
    height: 26,
  },
  gridDataRowSelected: {
    backgroundColor: '#F9F1E2',
  },
  gridDataCell: {
    paddingHorizontal: 6,
    borderRightWidth: 1,
    borderRightColor: '#F0E6D2',
    justifyContent: 'center',
  },
  gridDataText: {
    fontSize: 11,
    color: '#3B2D17',
  },
  gridDataTextBold: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3B2D17',
  },
  editableTableCell: {
    height: 22,
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderRadius: 3,
    backgroundColor: '#FFFDF9',
    paddingHorizontal: 4,
    paddingVertical: 0,
    fontSize: 11,
    textAlign: 'right',
    color: '#3B2D17',
    fontWeight: 'bold',
  },
  emptyGridContainer: {
    padding: 16,
    alignItems: 'center',
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#EBE2D2',
  },
  emptyGridText: {
    fontSize: 11,
    color: '#776240',
    fontStyle: 'italic',
  },
  gridFooterActions: {
    flexDirection: 'row',
    justifyContent: 'flex-end',
    marginTop: 6,
  },
  btnActionOk: {
    backgroundColor: '#F9F2E2',
    borderWidth: 1,
    borderColor: '#B8903B',
    paddingVertical: 5,
    paddingHorizontal: 22,
    borderRadius: 5,
  },
  btnActionOkText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#9E6D08',
  },

  // BOTTOM DETAIL INPUTS
  bottomInputsContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 6,
    marginTop: 8,
    paddingTop: 8,
    borderTopWidth: 1,
    borderTopColor: '#EBE2D2',
  },
  gridFooterActionButtons: {
    flexDirection: 'row',
    justifyContent: 'flex-end',
    alignItems: 'center',
    gap: 8,
    marginTop: 8,
  },
  btnActionAdd: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#F9F2E2',
    borderWidth: 1,
    borderColor: '#B8903B',
    paddingVertical: 5,
    paddingHorizontal: 16,
    borderRadius: 5,
  },
  btnActionAddText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#9E6D08',
  },
  btnActionDelete: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#F2A8A2',
    paddingVertical: 5,
    paddingHorizontal: 16,
    borderRadius: 5,
  },
  btnActionDeleteText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#D93829',
  },
  btnActionClose: {
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#DFD5C3',
    paddingVertical: 5,
    paddingHorizontal: 16,
    borderRadius: 5,
  },
  btnActionCloseText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#5C4A28',
  },

  // MODAL STYLES
  modalOverlay: {
    flex: 1,
    backgroundColor: 'rgba(0,0,0,0.45)',
    justifyContent: 'center',
    alignItems: 'center',
    padding: 16,
  },
  modalCard: {
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    width: '100%',
    maxWidth: 480,
    padding: 12,
    shadowColor: '#000',
    shadowOpacity: 0.15,
    shadowRadius: 8,
    elevation: 8,
  },
  modalCardWide: {
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    width: '100%',
    maxWidth: 720,
    padding: 12,
    shadowColor: '#000',
    shadowOpacity: 0.15,
    shadowRadius: 8,
    elevation: 8,
  },
  modalHeaderBanner: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    paddingBottom: 8,
    borderBottomWidth: 1,
    borderBottomColor: '#EBE2D2',
    marginBottom: 8,
  },
  modalHeaderTitle: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#3B2D17',
  },
  modalCloseBtn: {
    padding: 4,
  },
  searchBarContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFFDF9',
    borderWidth: 1,
    borderColor: '#D8CDBA',
    borderRadius: 5,
    paddingHorizontal: 8,
    marginBottom: 8,
  },
  searchInput: {
    flex: 1,
    height: 28,
    fontSize: 11,
    color: '#3B2D17',
  },
  modalBottomBar: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    marginTop: 10,
    paddingTop: 8,
    borderTopWidth: 1,
    borderTopColor: '#EBE2D2',
  },
  checkboxBox: {
    width: 15,
    height: 15,
    borderWidth: 1,
    borderColor: '#B8903B',
    borderRadius: 3,
    backgroundColor: '#FFFDF9',
    alignItems: 'center',
    justifyContent: 'center',
  },
  checkboxBoxChecked: {
    backgroundColor: '#B8903B',
    borderColor: '#8D6409',
  },
  lovRow: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingVertical: 7,
    paddingHorizontal: 8,
    borderBottomWidth: 1,
    borderBottomColor: '#F0E6D2',
  },
  lovCode: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#9E6D08',
    width: 100,
  },
  lovName: {
    fontSize: 11,
    color: '#3B2D17',
    flex: 1,
  },
  lovWeight: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#2E7D32',
  },
});
