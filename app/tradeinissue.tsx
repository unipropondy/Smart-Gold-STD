/** Developer: Antigravity */
import { PrintReportModal } from '@/components/PrintReportModal';
import { ThemedText } from '@/components/themed-text';
import { API_BASE_URL } from '@/constants/api';
import { Ionicons } from '@expo/vector-icons';
import { Stack, useRouter } from 'expo-router';
import React, { useEffect, useState } from 'react';
import {
  ActivityIndicator,
  FlatList,
  Modal,
  ScrollView,
  StyleSheet,
  Text,
  TextInput,
  TouchableOpacity,
  TouchableWithoutFeedback,
  View
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';

const API_URL = `${API_BASE_URL}/tradeinissue`;

interface GridRowItem {
  InvoiceNo: string;
  Date: string;
  InventoryCode: string;
  PurchaseWeight: number;
  IssuedWeight: number;
  BalanceWeight: number;
}

interface PendingTradeBillItem {
  InvoiceNo: string;
  InventoryCode: string;
  InvoiceDate: string;
  TotalWeight: number;
  IssuedWeight?: number;
  BalanceWeight: number;
  TotalPrice: number;
  AvgCost?: number;
  selected?: boolean;
}

export default function TradeInWholeSalesScreen() {
  const router = useRouter();

  // Loading & State
  const [loading, setLoading] = useState(false);
  const [saving, setSaving] = useState(false);
  const [toast, setToast] = useState<{ message: string; type: 'success' | 'error' } | null>(null);

  // Top Section Fields (matching frmTradeInWholeSales.vb)
  const [tradeIssueNo, setTradeIssueNo] = useState('');

  // Date Fields (DD - MM - YYYY)
  const today = new Date();
  const [dd, setDd] = useState(String(today.getDate()).padStart(2, '0'));
  const [mm, setMm] = useState(String(today.getMonth() + 1).padStart(2, '0'));
  const [yy, setYy] = useState(String(today.getFullYear()));

  // Supplier
  const [supplierCode, setSupplierCode] = useState('');
  const [supplierName, setSupplierName] = useState('');

  // Checkboxes
  const [isProduction, setIsProduction] = useState(false);
  const [isExport, setIsExport] = useState(false);

  // Financial & Weight Fields
  const [totalIssuedWt, setTotalIssuedWt] = useState('0.00');
  const [totalPrice, setTotalPrice] = useState('0.00');
  const [costPerGram, setCostPerGram] = useState('0.00');
  const [totalWt, setTotalWt] = useState('0.00');

  // Purity
  const [purityList, setPurityList] = useState<string[]>(['750', '835', '875', '916', '999', '999.9', '22K']);
  const [selectedPurity, setSelectedPurity] = useState('999.9');
  const [showPurityPicker, setShowPurityPicker] = useState(false);

  // Remarks
  const [remarks, setRemarks] = useState('');

  // Trade Bill scan / search
  const [tradeBillNo, setTradeBillNo] = useState('');

  // Grid (6 columns)
  const [gridItems, setGridItems] = useState<GridRowItem[]>([]);
  const [selectedRowIndex, setSelectedRowIndex] = useState<number | null>(null);

  // Print Report Modal
  const [printModalVisible, setPrintModalVisible] = useState(false);
  const [currentPrintIssueNo, setCurrentPrintIssueNo] = useState('');

  // Pending Trade Bills Modal
  const [billModalVisible, setBillModalVisible] = useState(false);
  const [pendingBills, setPendingBills] = useState<PendingTradeBillItem[]>([]);
  const [selectAllBills, setSelectAllBills] = useState(false);
  const [selectedWeightSum, setSelectedWeightSum] = useState('0.00');

  // View mode when loaded from history (VB: txtTradeBillNo.Enabled = False, btnProceed.Enabled = False)
  const [isHistoryViewMode, setIsHistoryViewMode] = useState(false);

  // Supplier LOV
  const [supplierModalVisible, setSupplierModalVisible] = useState(false);
  const [supplierList, setSupplierList] = useState<any[]>([]);
  const [supplierSearchQuery, setSupplierSearchQuery] = useState('');

  // Issue No History LOV
  const [issueModalVisible, setIssueModalVisible] = useState(false);
  const [issueList, setIssueList] = useState<any[]>([]);
  const [issueSearchQuery, setIssueSearchQuery] = useState('');

  // Dynamic GST & Company properties
  const [gstPercentage, setGstPercentage] = useState(9.0);
  const [companyShortCode, setCompanyShortCode] = useState('');

  const showToast = (message: string, type: 'success' | 'error' = 'success') => {
    setToast({ message, type });
    setTimeout(() => setToast(null), 3200);
  };

  useEffect(() => {
    fetchNextIssueNo();
    fetchPurities();
  }, []);

  const fetchNextIssueNo = async () => {
    try {
      setLoading(true);
      const res = await fetch(`${API_URL}/nextIssueNo`);
      const data = await res.json();
      if (data.success) {
        if (data.data || data.issueNo) {
          setTradeIssueNo(data.data || data.issueNo);
        }
        if (data.gstPercentage !== undefined) {
          setGstPercentage(parseFloat(data.gstPercentage) || 9.0);
        }
        if (data.companyShortCode) {
          setCompanyShortCode(String(data.companyShortCode).trim().toUpperCase());
        }
      }
    } catch (e: any) {
      console.error('Error fetching next issue no:', e);
    } finally {
      setLoading(false);
    }
  };

  const fetchPurities = async () => {
    try {
      const res = await fetch(`${API_URL}/purities`);
      const data = await res.json();
      if (data.success && Array.isArray(data.data) && data.data.length > 0) {
        setPurityList(data.data);
      }
    } catch (e) {
      // defaults
    }
  };

  const calculateTotalPrice = (costGramVal: string, tWtVal: string, exportChecked: boolean) => {
    const costNum = parseFloat(costGramVal) || 0;
    const wtNum = parseFloat(tWtVal) || 0;
    const baseTotal = wtNum * costNum;

    if (!exportChecked) {
      const totalWithGst = baseTotal + (baseTotal * gstPercentage) / 100;
      setTotalPrice(totalWithGst.toFixed(2));
    } else {
      setTotalPrice(baseTotal.toFixed(2));
    }
  };

  const handleCostPerGramChange = (text: string) => {
    setCostPerGram(text);
    calculateTotalPrice(text, totalWt, isExport);
  };

  const handleTotalPriceChange = (text: string) => {
    setTotalPrice(text);
    const priceNum = parseFloat(text) || 0;
    const wtNum = parseFloat(totalWt) || parseFloat(totalIssuedWt) || 0;
    if (wtNum > 0 && priceNum > 0) {
      if (!isExport) {
        // base = priceNum / (1 + gstPercentage/100)
        const base = priceNum / (1 + gstPercentage / 100);
        const derivedCost = base / wtNum;
        setCostPerGram(derivedCost.toFixed(2));
      } else {
        const derivedCost = priceNum / wtNum;
        setCostPerGram(derivedCost.toFixed(2));
      }
    }
  };

  const handleExportToggle = (checked: boolean) => {
    setIsExport(checked);
    calculateTotalPrice(costPerGram, totalWt, checked);
  };

  const handleProductionToggle = (checked: boolean) => {
    setIsProduction(checked);
    if (checked) {
      setSupplierCode('NGT');
      handleSupplierLookup('NGT');
    } else {
      setSupplierCode('');
      setSupplierName('');
    }
  };

  const handleSupplierLookup = async (code: string) => {
    if (!code || !code.trim()) return;
    try {
      const res = await fetch(`${API_URL}/supplier/${encodeURIComponent(code.trim())}`);
      const data = await res.json();
      if (data.success && data.data) {
        setSupplierName(data.data.SupplierName || '');
      } else {
        setSupplierName('');
      }
    } catch (e) {
      console.error('Error fetching supplier details:', e);
    }
  };

  const openSupplierLOV = async () => {
    if (isProduction) return;
    try {
      setLoading(true);
      const res = await fetch(`${API_URL}/suppliers`);
      const data = await res.json();
      if (data.success && data.data) {
        setSupplierList(data.data);
        setSupplierSearchQuery('');
        setSupplierModalVisible(true);
      }
    } catch (e: any) {
      showToast('Error loading suppliers: ' + e.message, 'error');
    } finally {
      setLoading(false);
    }
  };

  const selectSupplier = (s: any) => {
    setSupplierCode(s.SupplierCode || '');
    setSupplierName(s.SupplierName || '');
    setSupplierModalVisible(false);
  };

  const openIssueHistoryLOV = async () => {
    try {
      setLoading(true);
      setIssueModalVisible(true);
      const res = await fetch(`${API_URL}/history`);
      if (res.ok) {
        const data = await res.json();
        const records = (data && data.success && Array.isArray(data.data)) ? data.data : (Array.isArray(data) ? data : []);
        setIssueList(records);
      } else {
        setIssueList([]);
      }
      setIssueSearchQuery('');
    } catch (e: any) {
      console.warn('Error loading history:', e.message);
      setIssueList([]);
    } finally {
      setLoading(false);
    }
  };

  const selectHistoryIssue = async (issueItem: any) => {
    const selectedNo = issueItem.TradeIssueNo || issueItem.IssueNo;
    setIssueModalVisible(false);
    if (!selectedNo) return;

    try {
      setLoading(true);
      const res = await fetch(`${API_URL}/history/${encodeURIComponent(selectedNo)}`);
      const data = await res.json();
      if (data.success && (data.header || data.data)) {
        const header = data.header || (data.data && data.data.header) || {};
        const items = data.items || (data.data && data.data.items) || [];

        setTradeIssueNo(selectedNo);
        setSupplierCode(header.SupplierCode || issueItem.SupplierCode || '');
        setSupplierName(header.SupplierName || issueItem.SupplierName || '');
        if (header.CostPerGram) setCostPerGram(parseFloat(header.CostPerGram).toFixed(2));
        if (header.TotalIssuedWeight || header.IssuedWeight) {
          setTotalIssuedWt(parseFloat(header.TotalIssuedWeight || header.IssuedWeight).toFixed(2));
        }
        if (header.TotalWeight) {
          setTotalWt(parseFloat(header.TotalWeight).toFixed(2));
        }
        if (header.TotalPrice) {
          setTotalPrice(parseFloat(header.TotalPrice).toFixed(2));
        }
        if (header.Purity) setSelectedPurity(header.Purity);
        if (header.Remarks) setRemarks(header.Remarks);

        const loadedItems: GridRowItem[] = items.map((r: any) => ({
          InvoiceNo: r.InvoiceNo || '',
          Date: r.Date || r.InvoiceDate || '',
          InventoryCode: r.InventoryCode || r.InvoiceNo || '',
          PurchaseWeight: parseFloat(r.PurchaseWeight || r.TotalWeight || 0),
          IssuedWeight: parseFloat(r.IssuedWeight || 0),
          BalanceWeight: parseFloat(r.BalanceWeight || 0)
        }));
        setGridItems(loadedItems);
        setIsHistoryViewMode(true);
        showToast(`Loaded issue ${selectedNo}`, 'success');
      }
    } catch (e: any) {
      showToast('Error loading issue details: ' + e.message, 'error');
    } finally {
      setLoading(false);
    }
  };

  const openPendingTradeBillsModal = async () => {
    try {
      setLoading(true);
      // Query trade bills for the selected purity (and fallback/all open trade bills)
      const url = `${API_URL}/tradebills${selectedPurity ? `?purity=${encodeURIComponent(selectedPurity)}` : ''}`;
      const res = await fetch(url);
      if (!res.ok) {
        showToast('Server returned status ' + res.status, 'error');
        setPendingBills([]);
        setSelectedWeightSum('0.00');
        setBillModalVisible(true);
        return;
      }
      const data = await res.json();

      let items = data.success && Array.isArray(data.data) ? data.data : [];

      // If purity filtered endpoint returned empty or 1 item, also query without purity to ensure all available trade bills are listed
      if (items.length <= 1 && selectedPurity) {
        try {
          const fallbackRes = await fetch(`${API_URL}/tradebills`);
          if (fallbackRes.ok) {
            const fallbackData = await fallbackRes.json();
            if (fallbackData.success && Array.isArray(fallbackData.data) && fallbackData.data.length > items.length) {
              items = fallbackData.data;
            }
          }
        } catch (_) {}
      }

      const listWithSelection = items.map((item: any) => ({
        InvoiceNo: item.InvoiceNo || '',
        InventoryCode: item.InventoryCode || item.TradeinInventoryCode || item.InvoiceNo || '',
        InvoiceDate: item.InvoiceDate || '',
        TotalWeight: parseFloat(item.TotalWeight || 0),
        IssuedWeight: parseFloat(item.IssuedWeight || item.TotalWeight || 0),
        BalanceWeight: parseFloat(item.BalanceWeight || item.TotalWeight || 0),
        TotalPrice: parseFloat(item.TotalPrice || 0),
        AvgCost: parseFloat(item.AvgCost || 0),
        selected: false
      }));

      setPendingBills(listWithSelection);
      setSelectAllBills(false);
      setSelectedWeightSum('0.00');
      setBillModalVisible(true);

      if (listWithSelection.length === 0) {
        showToast(`No pending trade bills found`, 'error');
      }
    } catch (e: any) {
      showToast('Error fetching pending trade bills: ' + e.message, 'error');
    } finally {
      setLoading(false);
    }
  };

  const togglePendingBillSelection = (index: number) => {
    const updated = [...pendingBills];
    updated[index].selected = !updated[index].selected;
    setPendingBills(updated);

    const sumWt = updated
      .filter((b) => b.selected)
      .reduce((sum, b) => sum + (b.TotalWeight || b.BalanceWeight || 0), 0);
    setSelectedWeightSum(sumWt.toFixed(2));
  };

  const handleSelectAllBills = (val: boolean) => {
    setSelectAllBills(val);
    const updated = pendingBills.map((b) => ({ ...b, selected: val }));
    setPendingBills(updated);

    const sumWt = val
      ? updated.reduce((sum, b) => sum + (b.TotalWeight || b.BalanceWeight || 0), 0)
      : 0;
    setSelectedWeightSum(sumWt.toFixed(2));
  };

  const confirmSelectedBills = () => {
    const selected = pendingBills.filter((b) => b.selected);
    if (selected.length === 0) {
      showToast('Please select at least one trade bill', 'error');
      return;
    }

    let sumPurchaseWt = 0;
    let sumIssuedWt = 0;
    let sumTotalPrice = 0;

    const newGridRows: GridRowItem[] = [];

    selected.forEach((bill) => {
      const pWeight = bill.TotalWeight || bill.BalanceWeight || 0;
      const iWeight = bill.BalanceWeight || bill.TotalWeight || 0;
      const bWeight = Math.max(0, pWeight - iWeight);
      const avgCost = bill.AvgCost || (pWeight > 0 ? (bill.TotalPrice || 0) / pWeight : 0);

      sumPurchaseWt += pWeight;
      sumIssuedWt += iWeight;
      sumTotalPrice += avgCost * iWeight;

      newGridRows.push({
        InvoiceNo: bill.InvoiceNo,
        Date: bill.InvoiceDate || `${dd}/${mm}/${yy}`,
        InventoryCode: bill.InventoryCode || bill.InvoiceNo,
        PurchaseWeight: pWeight,
        IssuedWeight: iWeight,
        BalanceWeight: bWeight
      });
    });

    setGridItems(newGridRows);

    const formattedIssuedWt = sumIssuedWt.toFixed(2);
    const formattedTotalWt = (companyShortCode === 'MJ' ? sumIssuedWt : sumPurchaseWt).toFixed(2);
    setTotalIssuedWt(formattedIssuedWt);
    setTotalWt(formattedTotalWt);

    const avgCostPerGm = sumIssuedWt > 0 ? sumTotalPrice / sumIssuedWt : 0;
    if (avgCostPerGm > 0 && parseFloat(costPerGram) === 0) {
      setCostPerGram(avgCostPerGm.toFixed(2));
      calculateTotalPrice(avgCostPerGm.toFixed(2), formattedTotalWt, isExport);
    } else {
      calculateTotalPrice(costPerGram, formattedTotalWt, isExport);
    }

    setBillModalVisible(false);
    showToast(`Added ${newGridRows.length} item(s) to grid`, 'success');
  };

  const handleScanTradeBill = async () => {
    if (!tradeBillNo.trim()) return;
    try {
      setLoading(true);
      const res = await fetch(`${API_URL}/tradebill/${encodeURIComponent(tradeBillNo.trim())}`);
      const data = await res.json();
      if (data.success && data.data) {
        const bill = data.data;
        const exists = gridItems.some((r) => r.InvoiceNo === bill.InvoiceNo);
        if (exists) {
          showToast('Item already in grid', 'error');
          setTradeBillNo('');
          return;
        }

        const pWeight = parseFloat(bill.TotalWeight || bill.PurchaseWeight || 0);
        const iWeight = parseFloat(bill.BalanceWeight || pWeight);
        const bWeight = Math.max(0, pWeight - iWeight);

        const newRow: GridRowItem = {
          InvoiceNo: bill.InvoiceNo,
          Date: bill.InvoiceDate || `${dd}/${mm}/${yy}`,
          InventoryCode: bill.InventoryCode || bill.InvoiceNo,
          PurchaseWeight: pWeight,
          IssuedWeight: iWeight,
          BalanceWeight: bWeight
        };

        const updated = [...gridItems, newRow];
        setGridItems(updated);

        const sumIssued = updated.reduce((s, r) => s + r.IssuedWeight, 0);
        const sumTotal = updated.reduce((s, r) => s + r.PurchaseWeight, 0);
        setTotalIssuedWt(sumIssued.toFixed(2));
        setTotalWt((companyShortCode === 'MJ' ? sumIssued : sumTotal).toFixed(2));
        calculateTotalPrice(costPerGram, (companyShortCode === 'MJ' ? sumIssued : sumTotal).toFixed(2), isExport);

        setTradeBillNo('');
        showToast(`Bill ${bill.InvoiceNo} added`, 'success');
      } else {
        showToast(data.message || 'Bill not found or has zero balance', 'error');
      }
    } catch (e: any) {
      showToast('Error scanning trade bill: ' + e.message, 'error');
    } finally {
      setLoading(false);
    }
  };

  const handleClear = () => {
    setSupplierCode('');
    setSupplierName('');
    setIsProduction(false);
    setIsExport(false);
    setTotalIssuedWt('0.00');
    setTotalWt('0.00');
    setCostPerGram('0.00');
    setTotalPrice('0.00');
    setSelectedPurity('999.9');
    setRemarks('');
    setTradeBillNo('');
    setGridItems([]);
    setSelectedRowIndex(null);
    setIsHistoryViewMode(false);
    fetchNextIssueNo();
    showToast('Form cleared', 'success');
  };

  const handleProceed = async () => {
    // 1. Date validation (VB: fnDateValid)
    const dVal = parseInt(dd, 10);
    const mVal = parseInt(mm, 10);
    const yVal = parseInt(yy, 10);
    if (isNaN(dVal) || isNaN(mVal) || isNaN(yVal) || dVal < 1 || dVal > 31 || mVal < 1 || mVal > 12 || yVal < 1900 || yVal > 2100) {
      showToast('Pls Check the Date', 'error');
      return;
    }

    // 2. Supplier validation (VB: Len(Trim$(txtsupplierCode.Text)) = 0)
    if (!supplierCode.trim()) {
      showToast('Supplier cannot be blank!', 'error');
      return;
    }

    // 3. Gold Issued Actual Weight validation (VB: Val(txtIssued.Text) = 0)
    const issuedNum = parseFloat(totalIssuedWt) || 0;
    if (issuedNum <= 0) {
      showToast('Gold Issued Actual Weight Should Not Be 0 !!!', 'error');
      return;
    }

    // 4. Grid items validation
    if (gridItems.length === 0) {
      showToast('Please add Trade In Gold items to the grid!', 'error');
      return;
    }

    try {
      setSaving(true);
      const payload = {
        issueNo: tradeIssueNo,
        issueDate: `${dd}/${mm}/${yy}`,
        supplierCode: supplierCode.trim(),
        purity: selectedPurity,
        costPerGram: parseFloat(costPerGram) || 0,
        totalIssuedWt: parseFloat(totalIssuedWt) || 0,
        totalWt: parseFloat(totalWt) || 0,
        totalPrice: parseFloat(totalPrice) || 0,
        remarks: remarks.trim(),
        isProduction,
        isExport,
        userId: 'ADMIN',
        items: gridItems
      };

      const res = await fetch(`${API_URL}/proceed`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload)
      });
      const data = await res.json();

      if (data.success) {
        showToast('Tradein Gold Issued Sucessfully', 'success');
        const savedIssueNo = data.tradeIssueNo || tradeIssueNo;
        setCurrentPrintIssueNo(savedIssueNo);
        setPrintModalVisible(true);
        handleClear();
      } else {
        showToast(data.message || 'Error in save', 'error');
      }
    } catch (e: any) {
      showToast('Save error: ' + e.message, 'error');
    } finally {
      setSaving(false);
    }
  };

  const handlePrint = () => {
    const issueToPrint = tradeIssueNo.trim() || currentPrintIssueNo.trim();
    if (!issueToPrint) {
      showToast('No issue number to print', 'error');
      return;
    }
    setCurrentPrintIssueNo(issueToPrint);
    setPrintModalVisible(true);
  };

  return (
    <SafeAreaView style={styles.safeArea} edges={['top', 'bottom']}>
      <Stack.Screen options={{ headerShown: false }} />

      {/* Top Navigation Bar */}
      <View style={styles.topNavHeader}>
        <View style={styles.headerLeft}>
          <TouchableOpacity
            style={styles.backBtn}
            onPress={() => router.back()}
            activeOpacity={0.7}
          >
            <Ionicons name="arrow-back" size={18} color="#3B2D17" />
          </TouchableOpacity>
          <View>
            <ThemedText style={styles.headerTitle}>TRADE IN WHOLE SALES</ThemedText>
            <ThemedText style={styles.headerSubtitle}>
              Issue Voucher #{tradeIssueNo || '...'} • SmartGold System
            </ThemedText>
          </View>
        </View>

        <View style={styles.headerRight}>
          <TouchableOpacity
            style={styles.headerActionBtn}
            onPress={handleClear}
            activeOpacity={0.7}
          >
            <Ionicons name="refresh-outline" size={14} color="#5C4A28" />
            <ThemedText style={styles.headerActionBtnText}>Clear (F6)</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity
            style={styles.headerActionBtn}
            onPress={handlePrint}
            activeOpacity={0.7}
          >
            <Ionicons name="print-outline" size={14} color="#5C4A28" />
            <ThemedText style={styles.headerActionBtnText}>Print (F9)</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity
            style={[styles.headerActionBtn, styles.headerSaveBtn, isHistoryViewMode && { opacity: 0.5 }]}
            onPress={handleProceed}
            disabled={saving || isHistoryViewMode}
            activeOpacity={0.7}
          >
            {saving ? (
              <ActivityIndicator size="small" color="#9E6D08" />
            ) : (
              <>
                <Ionicons name="checkmark-done" size={16} color="#9E6D08" />
                <ThemedText style={styles.headerSaveBtnText}>Proceed (F7)</ThemedText>
              </>
            )}
          </TouchableOpacity>
        </View>
      </View>

      {/* Toast Alert */}
      {toast && (
        <View
          style={[
            styles.toastContainer,
            toast.type === 'error' ? styles.toastError : styles.toastSuccess,
          ]}
        >
          <Ionicons
            name={toast.type === 'error' ? 'alert-circle' : 'checkmark-circle'}
            size={16}
            color="#FFF"
            style={{ marginRight: 6 }}
          />
          <Text style={styles.toastText}>{toast.message}</Text>
        </View>
      )}

      {/* Main Content */}
      <ScrollView
        contentContainerStyle={styles.container}
        keyboardShouldPersistTaps="handled"
      >
        {/* PREMIUM GOLD VOUCHER CARD (Containing only the exact needed fields) */}
        <View style={styles.voucherCard}>
          <View style={styles.cardHeaderBanner}>
            <View style={styles.headerTitleRow}>
              <View style={styles.headerIconBadge}>
                <Ionicons name="document-text-outline" size={14} color="#8D6409" />
              </View>
              <ThemedText style={styles.cardHeaderTitle}>ISSUE VOUCHER & SUPPLIER DETAILS</ThemedText>
            </View>

            {/* Date Inputs in header */}
            <View style={styles.dateBadgeContainer}>
              <Ionicons name="calendar-outline" size={13} color="#8D6409" style={{ marginRight: 4 }} />
              <ThemedText style={styles.dateLabelText}>Date:</ThemedText>
              <View style={styles.dateInputsInline}>
                <TextInput
                  style={styles.dateInputPart}
                  value={dd}
                  onChangeText={setDd}
                  maxLength={2}
                  keyboardType="numeric"
                />
                <ThemedText style={styles.dateDash}>-</ThemedText>
                <TextInput
                  style={styles.dateInputPart}
                  value={mm}
                  onChangeText={setMm}
                  maxLength={2}
                  keyboardType="numeric"
                />
                <ThemedText style={styles.dateDash}>-</ThemedText>
                <TextInput
                  style={[styles.dateInputPart, { width: 44 }]}
                  value={yy}
                  onChangeText={setYy}
                  maxLength={4}
                  keyboardType="numeric"
                />
              </View>
            </View>
          </View>

          <View style={styles.cardBody}>
            {/* Row 1: Issue No & Supplier in ONE LINE */}
            <View style={styles.formRow}>
              {/* Issue No */}
              <ThemedText style={styles.rowLabel}>Issue No</ThemedText>
              <View style={styles.inputWithBtnContainer}>
                <TextInput
                  style={[styles.input, styles.inputAttached, { width: 140, fontWeight: 'bold' }]}
                  value={tradeIssueNo}
                  onChangeText={setTradeIssueNo}
                  placeholder="HQ65..."
                  placeholderTextColor="#A89E8D"
                />
                <TouchableOpacity
                  style={styles.btnSearchDots}
                  onPress={openIssueHistoryLOV}
                  activeOpacity={0.6}
                  hitSlop={{ top: 12, bottom: 12, left: 12, right: 12 }}
                >
                  <Ionicons name="search" size={14} color="#6E5528" />
                </TouchableOpacity>
              </View>

              {/* Supplier */}
              <ThemedText style={[styles.rowLabel, { marginLeft: 24, width: 60 }]}>Supplier</ThemedText>
              <View style={styles.inputWithBtnContainer}>
                <TextInput
                  style={[styles.input, styles.inputAttached, { width: 110 }]}
                  value={supplierCode}
                  onChangeText={setSupplierCode}
                  onBlur={() => handleSupplierLookup(supplierCode)}
                  editable={!isProduction}
                  placeholder="Code..."
                  placeholderTextColor="#A89E8D"
                />
                <TouchableOpacity
                  style={[styles.btnSearchDots, isProduction && { backgroundColor: '#ECE5D8' }]}
                  onPress={openSupplierLOV}
                  disabled={isProduction}
                  activeOpacity={0.8}
                >
                  <Ionicons name="search" size={13} color="#6E5528" />
                </TouchableOpacity>
              </View>
              <TextInput
                style={[styles.input, styles.readonlyInput, { flex: 1, marginLeft: 8 }]}
                value={supplierName}
                placeholder="Supplier Name..."
                placeholderTextColor="#A89E8D"
                editable={false}
              />
            </View>

            {/* Row 2: Options (Production & Export), Total Issued Wt, Total Price, Cost/Gram */}
            <View style={[styles.formRow, { marginTop: 8 }]}>
              {/* Checkboxes */}
              <View style={styles.optionsContainer}>
                <TouchableOpacity
                  style={styles.checkboxWrapper}
                  onPress={() => handleProductionToggle(!isProduction)}
                  activeOpacity={0.7}
                >
                  <View style={[styles.checkboxBox, isProduction && styles.checkboxChecked]}>
                    {isProduction && <Ionicons name="checkmark" size={11} color="#FFF" />}
                  </View>
                  <ThemedText style={styles.checkboxText}>Production</ThemedText>
                </TouchableOpacity>

                <TouchableOpacity
                  style={[styles.checkboxWrapper, { marginLeft: 16 }]}
                  onPress={() => handleExportToggle(!isExport)}
                  activeOpacity={0.7}
                >
                  <View style={[styles.checkboxBox, isExport && styles.checkboxChecked]}>
                    {isExport && <Ionicons name="checkmark" size={11} color="#FFF" />}
                  </View>
                  <ThemedText style={styles.checkboxText}>Export</ThemedText>
                </TouchableOpacity>
              </View>

              {/* Total Issued Wt */}
              <View style={[styles.inlineField, { marginLeft: 24 }]}>
                <ThemedText style={styles.inlineFieldLabel}>Total Issued Wt</ThemedText>
                <TextInput
                  style={[styles.input, styles.readonlyInput, { width: 95, textAlign: 'right' }]}
                  value={totalIssuedWt}
                  editable={false}
                />
              </View>

              {/* Total Price */}
              <View style={[styles.inlineField, { marginLeft: 16 }]}>
                <ThemedText style={styles.inlineFieldLabel}>Total Price</ThemedText>
                <TextInput
                  style={[styles.input, { width: 105, textAlign: 'right', fontWeight: 'bold' }]}
                  value={totalPrice}
                  onChangeText={handleTotalPriceChange}
                  keyboardType="numeric"
                />
              </View>

              {/* Cost/Gram */}
              <View style={[styles.inlineField, { marginLeft: 16 }]}>
                <ThemedText style={styles.inlineFieldLabel}>Cost/Gram</ThemedText>
                <TextInput
                  style={[styles.input, { width: 90, textAlign: 'right' }]}
                  value={costPerGram}
                  onChangeText={handleCostPerGramChange}
                  keyboardType="numeric"
                />
              </View>
            </View>

            {/* Row 3: Total Wt, Purity, and Remarks in ONE LINE */}
            <View style={[styles.formRow, { marginTop: 8 }]}>
              {/* Total Wt */}
              <View style={styles.inlineField}>
                <ThemedText style={styles.inlineFieldLabel}>Total Wt</ThemedText>
                <TextInput
                  style={[styles.input, styles.readonlyInput, { width: 90, textAlign: 'right' }]}
                  value={totalWt}
                  editable={false}
                />
              </View>

              {/* Purity */}
              <View style={[styles.inlineField, { marginLeft: 16 }]}>
                <ThemedText style={styles.inlineFieldLabel}>Purity</ThemedText>
                <TouchableOpacity
                  style={styles.purityDropdownBtn}
                  onPress={() => setShowPurityPicker(true)}
                  activeOpacity={0.8}
                >
                  <ThemedText style={styles.purityDropdownText}>{selectedPurity}</ThemedText>
                  <Ionicons name="chevron-down" size={13} color="#8D6409" />
                </TouchableOpacity>
              </View>

              {/* Remarks */}
              <ThemedText style={[styles.inlineFieldLabel, { marginLeft: 16 }]}>Remarks</ThemedText>
              <TextInput
                style={[styles.input, { flex: 1 }]}
                value={remarks}
                onChangeText={setRemarks}
                placeholder="Enter remarks or notes..."
                placeholderTextColor="#A89E8D"
              />
            </View>
          </View>
        </View>

        {/* MIDDLE SECTION: Scan / Search Bar + 6-Column Data Grid */}
        <View style={styles.gridSectionCard}>
          {/* Scan Bar Banner */}
          <View style={styles.scanBanner}>
            <View style={styles.headerTitleRow}>
              <View style={styles.headerIconBadge}>
                <Ionicons name="sparkles-outline" size={14} color="#8D6409" />
              </View>
              <ThemedText style={styles.cardHeaderTitle}>TRADE-IN GOLD BILLS TO ISSUE</ThemedText>
            </View>

            <View style={styles.scanInputGroup}>
              <ThemedText style={styles.scanPromptLabel}>Trade Bill:</ThemedText>
              <View style={styles.inputWithBtnContainer}>
                <TextInput
                  style={[styles.input, styles.inputAttached, { width: 140 }, isHistoryViewMode && styles.readonlyInput]}
                  value={tradeBillNo}
                  onChangeText={setTradeBillNo}
                  placeholder="Scan or enter..."
                  placeholderTextColor="#A89E8D"
                  onSubmitEditing={handleScanTradeBill}
                  editable={!isHistoryViewMode}
                />
                <TouchableOpacity
                  style={[styles.btnSearchDots, isHistoryViewMode && { opacity: 0.5 }]}
                  onPress={openPendingTradeBillsModal}
                  disabled={isHistoryViewMode}
                  activeOpacity={0.6}
                  hitSlop={{ top: 10, bottom: 10, left: 10, right: 10 }}
                >
                  <Ionicons name="search" size={14} color="#6E5528" />
                </TouchableOpacity>
              </View>

              <TouchableOpacity
                style={[styles.addBillBtn, isHistoryViewMode && { opacity: 0.5 }]}
                onPress={openPendingTradeBillsModal}
                disabled={isHistoryViewMode}
                activeOpacity={0.7}
              >
                <Ionicons name="list-outline" size={14} color="#9E6D08" style={{ marginRight: 4 }} />
                <ThemedText style={styles.addBillBtnText}>Browse Pending</ThemedText>
              </TouchableOpacity>
            </View>
          </View>

          {/* 6-Column Table Header */}
          <View style={styles.gridHeader}>
            <ThemedText style={[styles.gridHeaderText, { width: 40, textAlign: 'center' }]}>#</ThemedText>
            <ThemedText style={[styles.gridHeaderText, { flex: 2 }]}>Invoice No</ThemedText>
            <ThemedText style={[styles.gridHeaderText, { flex: 1.5 }]}>Date</ThemedText>
            <ThemedText style={[styles.gridHeaderText, { flex: 2 }]}>Inventory Code</ThemedText>
            <ThemedText style={[styles.gridHeaderText, { width: 130, textAlign: 'right' }]}>Purchase Weight (g)</ThemedText>
            <ThemedText style={[styles.gridHeaderText, { width: 125, textAlign: 'right' }]}>Issued Weight (g)</ThemedText>
            <ThemedText style={[styles.gridHeaderText, { width: 125, textAlign: 'right' }]}>Balance Weight (g)</ThemedText>
            <ThemedText style={[styles.gridHeaderText, { width: 55, textAlign: 'center' }]}>Action</ThemedText>
          </View>

          {/* Table Body */}
          <View style={styles.gridBody}>
            {gridItems.length === 0 ? (
              <View style={styles.gridEmptyArea}>
                <Ionicons name="receipt-outline" size={32} color="#C8BEAC" />
                <ThemedText style={styles.emptyGridText}>No Trade-In items in grid.</ThemedText>
                <ThemedText style={styles.emptyGridSubText}>
                  Scan or search a Trade Bill above and add to issue list
                </ThemedText>
              </View>
            ) : (
              gridItems.map((item, index) => {
                const isSelected = selectedRowIndex === index;
                return (
                  <TouchableOpacity
                    key={`${item.InvoiceNo}-${index}`}
                    style={[
                      styles.gridRow,
                      index % 2 === 1 && styles.gridRowAlt,
                      isSelected && styles.gridRowSelected,
                    ]}
                    activeOpacity={0.7}
                    onPress={() => setSelectedRowIndex(index)}
                  >
                    <ThemedText style={[styles.gridCellText, { width: 40, textAlign: 'center', color: '#8D6409', fontWeight: 'bold' }]}>
                      {index + 1}
                    </ThemedText>
                    <ThemedText style={[styles.gridCellText, { flex: 2, fontWeight: 'bold' }]}>
                      {item.InvoiceNo}
                    </ThemedText>
                    <ThemedText style={[styles.gridCellText, { flex: 1.5 }]}>
                      {item.Date}
                    </ThemedText>
                    <View style={{ flex: 2, justifyContent: 'center' }}>
                      <View style={styles.classPill}>
                        <ThemedText style={styles.classPillText}>{item.InventoryCode}</ThemedText>
                      </View>
                    </View>
                    <ThemedText style={[styles.gridCellText, { width: 130, textAlign: 'right', fontWeight: '600' }]}>
                      {item.PurchaseWeight.toFixed(2)}
                    </ThemedText>
                    <ThemedText style={[styles.gridCellText, styles.gridCellAmount, { width: 125, textAlign: 'right', fontWeight: 'bold' }]}>
                      {item.IssuedWeight.toFixed(2)}
                    </ThemedText>
                    <ThemedText style={[styles.gridCellText, { width: 125, textAlign: 'right', color: '#554428' }]}>
                      {item.BalanceWeight.toFixed(2)}
                    </ThemedText>
                    <View style={{ width: 55, alignItems: 'center', justifyContent: 'center' }}>
                      <TouchableOpacity
                        onPress={() => {
                          const updated = gridItems.filter((_, idx) => idx !== index);
                          setGridItems(updated);
                          const sumIssued = updated.reduce((s, r) => s + r.IssuedWeight, 0);
                          const sumTotal = updated.reduce((s, r) => s + r.PurchaseWeight, 0);
                          setTotalIssuedWt(sumIssued.toFixed(2));
                          const newTotWt = (companyShortCode === 'MJ' ? sumIssued : sumTotal).toFixed(2);
                          setTotalWt(newTotWt);
                          calculateTotalPrice(costPerGram, newTotWt, isExport);
                          showToast('Bill removed from list.');
                        }}
                        style={styles.rowDeleteBtn}
                        activeOpacity={0.7}
                      >
                        <Ionicons name="trash-outline" size={14} color="#C62828" />
                      </TouchableOpacity>
                    </View>
                  </TouchableOpacity>
                );
              })
            )}
          </View>
        </View>


      </ScrollView>

      {/* MODAL 1: PENDING TRADE BILLS (pnlInvoice - grdInvoice) */}
      <Modal
        visible={billModalVisible}
        transparent
        animationType="fade"
        onRequestClose={() => setBillModalVisible(false)}
      >
        <TouchableOpacity
          style={styles.modalOverlay}
          activeOpacity={1}
          onPress={() => setBillModalVisible(false)}
        >
          <TouchableWithoutFeedback>
            <View style={styles.vbPnlInvoiceContainer}>
              <View style={styles.vbPnlHeaderBanner}>
                <View style={{ flexDirection: 'row', alignItems: 'center', gap: 6 }}>
                  <Ionicons name="document-text-outline" size={14} color="#8D6409" />
                  <ThemedText style={styles.vbPnlHeaderBannerText}>Trade Bill Search</ThemedText>
                </View>
                <TouchableOpacity onPress={() => setBillModalVisible(false)} style={styles.modalCloseBtn}>
                  <Ionicons name="close" size={16} color="#6E5528" />
                </TouchableOpacity>
              </View>

              <ScrollView horizontal showsHorizontalScrollIndicator={true} style={{ maxHeight: 420 }}>
                <View>
                  {/* VB grdInvoice Table Header */}
                  <View style={styles.vbGrdHeaderRow}>
                    <View style={[styles.vbGrdHeaderCell, { width: 44, alignItems: 'center' }]}>
                      <ThemedText style={styles.vbGrdHeaderText}>Sel</ThemedText>
                    </View>
                    <View style={[styles.vbGrdHeaderCell, { width: 140 }]}>
                      <ThemedText style={styles.vbGrdHeaderText}>Inventory Code</ThemedText>
                    </View>
                    <View style={[styles.vbGrdHeaderCell, { width: 140 }]}>
                      <ThemedText style={styles.vbGrdHeaderText}>InvoiceDate</ThemedText>
                    </View>
                    <View style={[styles.vbGrdHeaderCell, { width: 110, alignItems: 'flex-end' }]}>
                      <ThemedText style={styles.vbGrdHeaderText}>Weight</ThemedText>
                    </View>
                    <View style={[styles.vbGrdHeaderCell, { width: 120, alignItems: 'flex-end' }]}>
                      <ThemedText style={styles.vbGrdHeaderText}>Issued Weight</ThemedText>
                    </View>
                    <View style={[styles.vbGrdHeaderCell, { width: 120, alignItems: 'flex-end' }]}>
                      <ThemedText style={styles.vbGrdHeaderText}>Total Price</ThemedText>
                    </View>
                    <View style={[styles.vbGrdHeaderCell, { width: 100, alignItems: 'flex-end' }]}>
                      <ThemedText style={styles.vbGrdHeaderText}>Average Cost</ThemedText>
                    </View>
                  </View>

                  {/* VB grdInvoice Table Body */}
                  <ScrollView style={{ maxHeight: 360, minHeight: 180 }}>
                    {pendingBills.length === 0 ? (
                      <View style={{ padding: 25, alignItems: 'center' }}>
                        <ThemedText style={{ color: '#555', fontStyle: 'italic', fontSize: 11 }}>
                          No pending trade bills found for purity {selectedPurity}.
                        </ThemedText>
                      </View>
                    ) : (
                      pendingBills.map((b, idx) => (
                        <TouchableOpacity
                          key={idx}
                          style={[
                            styles.vbGrdDataRow,
                            b.selected && styles.vbGrdDataRowSelected,
                            idx % 2 === 1 && !b.selected && styles.vbGrdDataRowAlt
                          ]}
                          onPress={() => togglePendingBillSelection(idx)}
                          activeOpacity={0.8}
                        >
                          <View style={[styles.vbGrdDataCell, { width: 44, alignItems: 'center' }]}>
                            <View style={[styles.vbCheckbox, b.selected && styles.vbCheckboxChecked]}>
                              {b.selected && <Ionicons name="checkmark" size={12} color="#FFF" />}
                            </View>
                          </View>
                          <View style={[styles.vbGrdDataCell, { width: 140 }]}>
                            <ThemedText style={styles.vbGrdDataText}>
                              {b.InventoryCode || b.InvoiceNo}
                            </ThemedText>
                          </View>
                          <View style={[styles.vbGrdDataCell, { width: 140 }]}>
                            <ThemedText style={styles.vbGrdDataText}>{b.InvoiceDate}</ThemedText>
                          </View>
                          <View style={[styles.vbGrdDataCell, { width: 110, alignItems: 'flex-end' }]}>
                            <ThemedText style={styles.vbGrdDataText}>{b.TotalWeight.toFixed(2)}</ThemedText>
                          </View>
                          <View style={[styles.vbGrdDataCell, { width: 120, alignItems: 'flex-end' }]}>
                            <ThemedText style={styles.vbGrdDataText}>{(b.IssuedWeight || b.TotalWeight || 0).toFixed(2)}</ThemedText>
                          </View>
                          <View style={[styles.vbGrdDataCell, { width: 120, alignItems: 'flex-end' }]}>
                            <ThemedText style={styles.vbGrdDataText}>{b.TotalPrice.toFixed(2)}</ThemedText>
                          </View>
                          <View style={[styles.vbGrdDataCell, { width: 100, alignItems: 'flex-end' }]}>
                            <ThemedText style={styles.vbGrdDataText}>{(b.AvgCost || 0).toFixed(2)}</ThemedText>
                          </View>
                        </TouchableOpacity>
                      ))
                    )}
                  </ScrollView>
                </View>
              </ScrollView>

              {/* VB pnlInvoice Bottom Bar (matching original layout and styling) */}
              <View style={styles.vbPnlFooterRow}>
                <View style={styles.vbPnlFooterLeft}>
                  <TouchableOpacity
                    style={styles.vbSelectAllWrapper}
                    onPress={() => handleSelectAllBills(!selectAllBills)}
                    activeOpacity={0.7}
                  >
                    <View style={[styles.vbCheckbox, selectAllBills && styles.vbCheckboxChecked]}>
                      {selectAllBills && <Ionicons name="checkmark" size={12} color="#FFF" />}
                    </View>
                    <ThemedText style={styles.vbFooterLabelBold}>Select All</ThemedText>
                  </TouchableOpacity>

                  <View style={styles.vbWeightDisplay}>
                    <ThemedText style={styles.vbFooterLabel}>Total Weight</ThemedText>
                    <ThemedText style={styles.vbFooterWeightValue}>{selectedWeightSum}</ThemedText>
                  </View>
                </View>

                <View style={styles.vbPnlFooterRight}>
                  <TouchableOpacity
                    style={styles.vbBtnOk}
                    onPress={confirmSelectedBills}
                    activeOpacity={0.8}
                  >
                    <ThemedText style={styles.vbBtnText}>OK</ThemedText>
                  </TouchableOpacity>
                  <TouchableOpacity
                    style={[styles.vbBtnClose, { marginLeft: 10 }]}
                    onPress={() => setBillModalVisible(false)}
                    activeOpacity={0.8}
                  >
                    <ThemedText style={styles.vbBtnText}>Close</ThemedText>
                  </TouchableOpacity>
                </View>
              </View>
            </View>
          </TouchableWithoutFeedback>
        </TouchableOpacity>
      </Modal>

      {/* MODAL 2: PURITY DROPDOWN */}
      <Modal
        visible={showPurityPicker}
        transparent
        animationType="fade"
        onRequestClose={() => setShowPurityPicker(false)}
      >
        <TouchableOpacity
          style={styles.modalOverlay}
          activeOpacity={1}
          onPress={() => setShowPurityPicker(false)}
        >
          <View style={styles.purityModalContainer}>
            <View style={styles.cardHeaderBanner}>
              <ThemedText style={styles.cardHeaderTitle}>Select Purity</ThemedText>
            </View>
            <View style={{ padding: 6 }}>
              {purityList.map((p, idx) => (
                <TouchableOpacity
                  key={idx}
                  style={[
                    styles.purityOptionItem,
                    selectedPurity === p && styles.purityOptionSelected
                  ]}
                  onPress={() => {
                    setSelectedPurity(p);
                    setShowPurityPicker(false);
                  }}
                >
                  <ThemedText
                    style={[
                      styles.purityOptionText,
                      selectedPurity === p && { fontWeight: 'bold', color: '#9E6D08' }
                    ]}
                  >
                    {p}
                  </ThemedText>
                  {selectedPurity === p && (
                    <Ionicons name="checkmark-circle" size={16} color="#9E6D08" />
                  )}
                </TouchableOpacity>
              ))}
            </View>
          </View>
        </TouchableOpacity>
      </Modal>

      {/* MODAL 3: SUPPLIER LOV */}
      <Modal
        visible={supplierModalVisible}
        transparent
        animationType="fade"
        onRequestClose={() => setSupplierModalVisible(false)}
      >
        <TouchableOpacity
          style={styles.modalOverlay}
          activeOpacity={1}
          onPress={() => setSupplierModalVisible(false)}
        >
          <TouchableWithoutFeedback>
            <View style={styles.lovModalCard}>
              <View style={styles.pnlModalHeader}>
                <View style={styles.headerTitleRow}>
                  <View style={styles.headerIconBadge}>
                    <Ionicons name="people-outline" size={14} color="#8D6409" />
                  </View>
                  <ThemedText style={styles.cardHeaderTitle}>SELECT SUPPLIER</ThemedText>
                </View>
                <TouchableOpacity onPress={() => setSupplierModalVisible(false)} style={styles.modalCloseBtn}>
                  <Ionicons name="close" size={18} color="#554428" />
                </TouchableOpacity>
              </View>

              <View style={styles.lovSearchContainer}>
                <Ionicons name="search" size={14} color="#8D6409" style={{ marginRight: 6 }} />
                <TextInput
                  style={styles.lovSearchInput}
                  placeholder="Search supplier code or name..."
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
                renderItem={({ item, index }) => (
                  <TouchableOpacity
                    style={[styles.lovItemRow, index % 2 === 1 && { backgroundColor: '#FCFAF6' }]}
                    onPress={() => selectSupplier(item)}
                  >
                    <ThemedText style={styles.lovItemCode}>{item.SupplierCode}</ThemedText>
                    <ThemedText style={styles.lovItemName}>{item.SupplierName}</ThemedText>
                  </TouchableOpacity>
                )}
                ListEmptyComponent={<ThemedText style={{ padding: 15, color: '#8D6409', textAlign: 'center' }}>No suppliers found</ThemedText>}
              />
            </View>
          </TouchableWithoutFeedback>
        </TouchableOpacity>
      </Modal>

      {/* MODAL 4: ISSUE HISTORY LOV */}
      <Modal
        visible={issueModalVisible}
        transparent
        animationType="fade"
        onRequestClose={() => setIssueModalVisible(false)}
      >
        <TouchableOpacity
          style={styles.modalOverlay}
          activeOpacity={1}
          onPress={() => setIssueModalVisible(false)}
        >
          <TouchableWithoutFeedback>
            <View style={styles.lovModalCard}>
              <View style={styles.pnlModalHeader}>
                <View style={styles.headerTitleRow}>
                  <View style={styles.headerIconBadge}>
                    <Ionicons name="time-outline" size={14} color="#8D6409" />
                  </View>
                  <ThemedText style={styles.cardHeaderTitle}>SELECT TRADE ISSUE HISTORY</ThemedText>
                </View>
                <TouchableOpacity onPress={() => setIssueModalVisible(false)} style={styles.modalCloseBtn}>
                  <Ionicons name="close" size={18} color="#554428" />
                </TouchableOpacity>
              </View>

              <View style={styles.lovSearchContainer}>
                <Ionicons name="search" size={14} color="#8D6409" style={{ marginRight: 6 }} />
                <TextInput
                  style={styles.lovSearchInput}
                  placeholder="Search by Issue No or Supplier..."
                  placeholderTextColor="#A89E8D"
                  value={issueSearchQuery}
                  onChangeText={setIssueSearchQuery}
                />
              </View>

              <FlatList
                data={issueList.filter(
                  (h) =>
                    (h.TradeIssueNo || h.IssueNo)?.toLowerCase().includes(issueSearchQuery.toLowerCase()) ||
                    h.SupplierCode?.toLowerCase().includes(issueSearchQuery.toLowerCase()) ||
                    h.SupplierName?.toLowerCase().includes(issueSearchQuery.toLowerCase())
                )}
                keyExtractor={(item, index) => item.TradeIssueNo || item.IssueNo || String(index)}
                renderItem={({ item, index }) => (
                  <TouchableOpacity
                    style={[styles.lovItemRow, index % 2 === 1 && { backgroundColor: '#FCFAF6' }]}
                    onPress={() => selectHistoryIssue(item)}
                  >
                    <ThemedText style={styles.lovItemCode}>{item.TradeIssueNo || item.IssueNo}</ThemedText>
                    <View style={{ flex: 1, paddingHorizontal: 8 }}>
                      <ThemedText style={styles.lovItemName}>{item.SupplierName || item.SupplierCode}</ThemedText>
                      <ThemedText style={{ fontSize: 10, color: '#8D6409' }}>Date: {item.IssueDate}</ThemedText>
                    </View>
                    <ThemedText style={styles.lovItemWeight}>
                      {parseFloat(item.IssuedWeight || 0).toFixed(2)} g
                    </ThemedText>
                  </TouchableOpacity>
                )}
                ListEmptyComponent={<ThemedText style={{ padding: 15, color: '#8D6409', textAlign: 'center' }}>No records found</ThemedText>}
              />
            </View>
          </TouchableWithoutFeedback>
        </TouchableOpacity>
      </Modal>

      {/* PRINT REPORT MODAL */}
      <PrintReportModal
        visible={printModalVisible}
        title="Print Trade In Whole Sales"
        message={`Print report for Issue No: ${currentPrintIssueNo}?`}
        onCancel={() => setPrintModalVisible(false)}
        onPdf={() => {
          setPrintModalVisible(false);
          showToast(`Exported PDF for ${currentPrintIssueNo}`, 'success');
        }}
        onExcel={() => {
          setPrintModalVisible(false);
          showToast(`Exported Excel for ${currentPrintIssueNo}`, 'success');
        }}
        onPrint={() => {
          setPrintModalVisible(false);
          showToast(`Printing Trade In Issue ${currentPrintIssueNo}...`, 'success');
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
  headerSaveBtnText: {
    color: '#9E6D08',
    fontWeight: 'bold',
    fontSize: 11,
  },

  // Toast
  toastContainer: {
    position: 'absolute',
    top: 55,
    right: 20,
    zIndex: 9999,
    flexDirection: 'row',
    alignItems: 'center',
    paddingVertical: 8,
    paddingHorizontal: 14,
    borderRadius: 6,
    shadowColor: '#000',
    shadowOpacity: 0.2,
    shadowRadius: 4,
    elevation: 6,
  },
  toastSuccess: {
    backgroundColor: '#2E7D32',
  },
  toastError: {
    backgroundColor: '#C62828',
  },
  toastText: {
    color: '#FFFFFF',
    fontSize: 12,
    fontWeight: '600',
  },

  // Main Container
  container: {
    padding: 12,
    paddingBottom: 35,
  },

  // VOUCHER CARD (Golden Warm Palette matching Trade In Gold Issue)
  voucherCard: {
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    shadowColor: '#A38035',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.06,
    shadowRadius: 4,
    elevation: 2,
    overflow: 'hidden',
    marginBottom: 12,
  },
  cardHeaderBanner: {
    backgroundColor: '#F5E7CA',
    paddingHorizontal: 12,
    paddingVertical: 6,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
  },
  headerTitleRow: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 5,
  },
  headerIconBadge: {
    width: 20,
    height: 20,
    borderRadius: 4,
    backgroundColor: '#FFF8EA',
    borderWidth: 1,
    borderColor: '#E2D2AF',
    alignItems: 'center',
    justifyContent: 'center',
  },
  cardHeaderTitle: {
    fontSize: 10.5,
    fontWeight: 'bold',
    color: '#3B2D17',
    letterSpacing: 0.3,
  },
  dateBadgeContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFF8EA',
    borderWidth: 1,
    borderColor: '#D8C59A',
    borderRadius: 5,
    paddingHorizontal: 6,
    paddingVertical: 2,
  },
  dateLabelText: {
    fontSize: 10.5,
    fontWeight: 'bold',
    color: '#8D6409',
    marginRight: 4,
  },
  dateInputsInline: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  dateInputPart: {
    width: 26,
    height: 20,
    borderWidth: 1,
    borderColor: '#E8DEC8',
    borderRadius: 3,
    textAlign: 'center',
    fontSize: 10.5,
    fontWeight: 'bold',
    color: '#3B2D17',
    backgroundColor: '#FFFFFF',
    paddingVertical: 0,
    paddingHorizontal: 0,
  },
  dateDash: {
    fontSize: 10.5,
    fontWeight: 'bold',
    color: '#8D6409',
    marginHorizontal: 2,
  },
  cardBody: {
    padding: 10,
    paddingVertical: 8,
  },
  formRow: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  rowLabel: {
    fontSize: 10.5,
    fontWeight: 'bold',
    color: '#554428',
    width: 80,
  },
  inputWithBtnContainer: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  input: {
    height: 26,
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#D8CDBA',
    borderRadius: 4,
    paddingHorizontal: 8,
    paddingVertical: 0,
    fontSize: 11,
    color: '#3B2D17',
    fontWeight: '500',
  },
  inputAttached: {
    borderTopRightRadius: 0,
    borderBottomRightRadius: 0,
    borderRightWidth: 0,
  },
  readonlyInput: {
    backgroundColor: '#FAF7F0',
    borderColor: '#E8DEC8',
    color: '#554428',
    fontWeight: 'bold',
  },
  btnSearchDots: {
    width: 26,
    height: 26,
    backgroundColor: '#F5EBD4',
    borderWidth: 1,
    borderColor: '#D8CDBA',
    borderTopRightRadius: 4,
    borderBottomRightRadius: 4,
    alignItems: 'center',
    justifyContent: 'center',
  },

  // Options row
  optionsContainer: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  checkboxWrapper: {
    flexDirection: 'row',
    alignItems: 'center',
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
    marginRight: 6,
  },
  checkboxChecked: {
    backgroundColor: '#B8903B',
  },
  checkboxText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#554428',
  },

  // Inline Fields
  inlineField: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  inlineFieldLabel: {
    fontSize: 10.5,
    fontWeight: 'bold',
    color: '#554428',
    marginRight: 6,
  },
  purityDropdownBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFF8EA',
    borderWidth: 1,
    borderColor: '#D8C59A',
    borderRadius: 4,
    paddingHorizontal: 10,
    height: 26,
    gap: 6,
  },
  purityDropdownText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#8D6409',
  },

  // MIDDLE SECTION: Grid Section Card
  gridSectionCard: {
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    shadowColor: '#A38035',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.06,
    shadowRadius: 4,
    elevation: 2,
    overflow: 'hidden',
    marginBottom: 10,
  },
  scanBanner: {
    backgroundColor: '#F5E7CA',
    paddingHorizontal: 12,
    paddingVertical: 6,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
  },
  scanInputGroup: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 8,
  },
  scanPromptLabel: {
    fontSize: 10.5,
    fontWeight: 'bold',
    color: '#554428',
  },
  addBillBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFF8EA',
    borderWidth: 1,
    borderColor: '#B8903B',
    paddingVertical: 4,
    paddingHorizontal: 9,
    borderRadius: 5,
  },
  addBillBtnText: {
    color: '#9E6D08',
    fontWeight: 'bold',
    fontSize: 10.5,
  },

  // Grid Header
  gridHeader: {
    flexDirection: 'row',
    backgroundColor: '#FAF5EA',
    borderBottomWidth: 1,
    borderBottomColor: '#E8DCB8',
    paddingHorizontal: 10,
    paddingVertical: 7,
    alignItems: 'center',
  },
  gridHeaderText: {
    fontSize: 10.5,
    fontWeight: 'bold',
    color: '#6E5528',
    letterSpacing: 0.2,
  },
  gridBody: {
    minHeight: 180,
  },
  gridEmptyArea: {
    height: 180,
    justifyContent: 'center',
    alignItems: 'center',
    gap: 4,
  },
  emptyGridText: {
    fontSize: 12.5,
    fontWeight: 'bold',
    color: '#8D6409',
    marginTop: 6,
  },
  emptyGridSubText: {
    fontSize: 10.5,
    color: '#A89E8D',
  },
  gridRow: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 10,
    paddingVertical: 7,
    borderBottomWidth: 1,
    borderBottomColor: '#F0E6D2',
    backgroundColor: '#FFFFFF',
  },
  gridRowAlt: {
    backgroundColor: '#FCFAF6',
  },
  gridRowSelected: {
    backgroundColor: '#FFF5DC',
    borderBottomColor: '#E2C785',
  },
  gridCellText: {
    fontSize: 10.5,
    color: '#3B2D17',
  },
  gridCellAmount: {
    color: '#2E7D32',
  },
  classPill: {
    alignSelf: 'flex-start',
    backgroundColor: '#FFF8EA',
    borderWidth: 1,
    borderColor: '#D8C59A',
    borderRadius: 4,
    paddingHorizontal: 6,
    paddingVertical: 2,
  },
  classPillText: {
    fontSize: 9.5,
    fontWeight: 'bold',
    color: '#9E6D08',
  },
  rowDeleteBtn: {
    width: 22,
    height: 22,
    borderRadius: 4,
    backgroundColor: '#FFEBEE',
    alignItems: 'center',
    justifyContent: 'center',
  },

  // BOTTOM ACTION BAR
  bottomActionBar: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    paddingHorizontal: 12,
    paddingVertical: 7,
  },
  bottomButtonsRow: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 8,
  },
  actionBtnSecondary: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 5,
    backgroundColor: '#FAF5EA',
    borderWidth: 1,
    borderColor: '#DFD5C3',
    paddingVertical: 6,
    paddingHorizontal: 11,
    borderRadius: 6,
  },
  actionBtnSecondaryText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#5C4A28',
  },
  actionBtnPrimary: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 5,
    backgroundColor: '#F9F2E2',
    borderWidth: 1,
    borderColor: '#B8903B',
    paddingVertical: 6,
    paddingHorizontal: 13,
    borderRadius: 6,
  },
  actionBtnPrimaryText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#9E6D08',
  },
  actionBtnClose: {
    paddingVertical: 6,
    paddingHorizontal: 11,
    borderRadius: 6,
    backgroundColor: '#F5F5F5',
    borderWidth: 1,
    borderColor: '#E0E0E0',
  },
  actionBtnCloseText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#616161',
  },

  // MODAL OVERLAY & VB PNLINVOICE MODAL
  modalOverlay: {
    flex: 1,
    backgroundColor: 'rgba(0,0,0,0.45)',
    justifyContent: 'center',
    alignItems: 'center',
    padding: 16,
  },
  vbPnlInvoiceContainer: {
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    width: '100%',
    maxWidth: 780,
    shadowColor: '#A38035',
    shadowOpacity: 0.12,
    shadowRadius: 8,
    elevation: 8,
    overflow: 'hidden',
  },
  vbPnlHeaderBanner: {
    backgroundColor: '#FAF5EA',
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    paddingHorizontal: 12,
    paddingVertical: 8,
    borderBottomWidth: 1,
    borderBottomColor: '#EBE2D2',
  },
  vbPnlHeaderBannerText: {
    color: '#3B2D17',
    fontWeight: 'bold',
    fontSize: 12,
    letterSpacing: 0.3,
  },
  vbGrdHeaderRow: {
    flexDirection: 'row',
    backgroundColor: '#F5E7CA',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    height: 36,
    alignItems: 'center',
  },
  vbGrdHeaderCell: {
    paddingHorizontal: 6,
    borderRightWidth: 1,
    borderRightColor: '#E8DCB8',
    justifyContent: 'center',
  },
  vbGrdHeaderText: {
    fontSize: 10.5,
    fontWeight: 'bold',
    color: '#6E5528',
    letterSpacing: 0.2,
  },
  vbGrdDataRow: {
    flexDirection: 'row',
    alignItems: 'center',
    borderBottomWidth: 1,
    borderBottomColor: '#F0E6D2',
    height: 30,
    backgroundColor: '#FFFFFF',
  },
  vbGrdDataRowAlt: {
    backgroundColor: '#FCFAF6',
  },
  vbGrdDataRowSelected: {
    backgroundColor: '#FFF5DC',
    borderBottomColor: '#E2C785',
  },
  vbGrdDataCell: {
    paddingHorizontal: 6,
    borderRightWidth: 1,
    borderRightColor: '#F0E6D2',
    justifyContent: 'center',
  },
  vbGrdDataText: {
    fontSize: 11,
    color: '#3B2D17',
  },
  vbCheckbox: {
    width: 15,
    height: 15,
    borderWidth: 1,
    borderColor: '#B8903B',
    borderRadius: 3,
    backgroundColor: '#FFFDF9',
    alignItems: 'center',
    justifyContent: 'center',
  },
  vbCheckboxChecked: {
    backgroundColor: '#B8903B',
    borderColor: '#8D6409',
  },
  vbPnlFooterRow: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    backgroundColor: '#FAF5EA',
    borderTopWidth: 1,
    borderTopColor: '#E8DCB8',
    paddingHorizontal: 12,
    paddingVertical: 8,
  },
  vbPnlFooterLeft: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 24,
  },
  vbSelectAllWrapper: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 6,
  },
  vbFooterLabelBold: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#554428',
  },
  vbWeightDisplay: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 6,
  },
  vbFooterLabel: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#554428',
  },
  vbFooterWeightValue: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#8D6409',
  },
  vbPnlFooterRight: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  vbBtnOk: {
    backgroundColor: '#F9F2E2',
    borderWidth: 1,
    borderColor: '#B8903B',
    width: 75,
    height: 28,
    borderRadius: 5,
    alignItems: 'center',
    justifyContent: 'center',
  },
  vbBtnClose: {
    backgroundColor: '#FAF5EA',
    borderWidth: 1,
    borderColor: '#DFD5C3',
    width: 75,
    height: 28,
    borderRadius: 5,
    alignItems: 'center',
    justifyContent: 'center',
  },
  vbBtnText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#5C4A28',
  },
  pnlInvoiceModal: {
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    width: '100%',
    maxWidth: 750,
    padding: 12,
    shadowColor: '#000',
    shadowOpacity: 0.15,
    shadowRadius: 8,
    elevation: 8,
  },
  pnlModalHeader: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    paddingBottom: 8,
    borderBottomWidth: 1,
    borderBottomColor: '#EBE2D2',
    marginBottom: 8,
  },
  modalCloseBtn: {
    padding: 4,
  },
  pnlHeaderRow: {
    flexDirection: 'row',
    backgroundColor: '#F5E7CA',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    paddingVertical: 6,
    paddingHorizontal: 8,
  },
  pnlHeaderCell: {
    justifyContent: 'center',
  },
  pnlHeaderText: {
    fontSize: 10.5,
    fontWeight: 'bold',
    color: '#3B2D17',
  },
  pnlDataRow: {
    flexDirection: 'row',
    alignItems: 'center',
    borderBottomWidth: 1,
    borderBottomColor: '#F0E6D2',
    paddingVertical: 6,
    paddingHorizontal: 8,
    backgroundColor: '#FFFFFF',
  },
  pnlDataCell: {
    justifyContent: 'center',
  },
  pnlDataText: {
    fontSize: 10.5,
    color: '#3B2D17',
  },
  pnlBottomRow: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    marginTop: 10,
    paddingTop: 8,
    borderTopWidth: 1,
    borderTopColor: '#EBE2D2',
  },
  pnlTotalWtDisplay: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  pnlTotalWtLabel: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#554428',
  },
  pnlTotalWtValue: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#C62828',
  },
  pnlBtnGroup: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  pnlActionBtnPrimary: {
    backgroundColor: '#F9F2E2',
    borderWidth: 1,
    borderColor: '#B8903B',
    paddingVertical: 5,
    paddingHorizontal: 12,
    borderRadius: 5,
  },
  pnlActionBtnPrimaryText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#9E6D08',
  },
  pnlActionBtnSecondary: {
    backgroundColor: '#FAF5EA',
    borderWidth: 1,
    borderColor: '#DFD5C3',
    paddingVertical: 5,
    paddingHorizontal: 12,
    borderRadius: 5,
  },
  pnlActionBtnSecondaryText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#5C4A28',
  },

  // Purity Modal
  purityModalContainer: {
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    width: 200,
    overflow: 'hidden',
  },
  purityOptionItem: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingVertical: 7,
    paddingHorizontal: 10,
    borderBottomWidth: 1,
    borderBottomColor: '#F5EBD4',
  },
  purityOptionSelected: {
    backgroundColor: '#FFF8EA',
  },
  purityOptionText: {
    fontSize: 11,
    color: '#3B2D17',
  },

  // LOV Card
  lovModalCard: {
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    width: '100%',
    maxWidth: 480,
    maxHeight: 460,
    padding: 12,
    shadowColor: '#000',
    shadowOpacity: 0.15,
    shadowRadius: 8,
    elevation: 8,
  },
  lovSearchContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFFDF9',
    borderWidth: 1,
    borderColor: '#D8CDBA',
    borderRadius: 5,
    paddingHorizontal: 8,
    marginBottom: 8,
  },
  lovSearchInput: {
    flex: 1,
    height: 28,
    fontSize: 11,
    color: '#3B2D17',
  },
  lovItemRow: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingVertical: 8,
    paddingHorizontal: 8,
    borderBottomWidth: 1,
    borderBottomColor: '#F0E6D2',
  },
  lovItemCode: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#9E6D08',
    width: 90,
  },
  lovItemName: {
    fontSize: 11,
    color: '#3B2D17',
    flex: 1,
  },
  lovItemWeight: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#2E7D32',
  },
});
