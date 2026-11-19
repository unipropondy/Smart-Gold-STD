/** Developer: Antigravity */
import { PrintReportModal } from '@/components/PrintReportModal';
import { ThemedText } from '@/components/themed-text';
import { API_BASE_URL } from '@/constants/api';
import { useThemeColors } from '@/hooks/useThemeColors';
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

const API_URL = `${API_BASE_URL}/tradeingoldissue`;

interface GridRowItem {
  InvoiceNo: string;
  Date: string;
  Class: string;
  PurchaseWeight: number;
  IssuedWeight: number;
  BalanceWeight: number;
  Amount: number;
}

export default function TradeInGoldIssueScreen() {
  const themeColors = useThemeColors();
  const router = useRouter();

  // Loading & State
  const [loading, setLoading] = useState(false);
  const [saving, setSaving] = useState(false);
  const [toast, setToast] = useState<{ message: string; type: 'success' | 'error' } | null>(null);

  // Top Section Fields (matching frmTradeInIssue.vb)
  const [tradeIssueNo, setTradeIssueNo] = useState('');

  // Date Fields (DD - MM - YYYY)
  const today = new Date();
  const [dd, setDd] = useState(String(today.getDate()).padStart(2, '0'));
  const [mm, setMm] = useState(String(today.getMonth() + 1).padStart(2, '0'));
  const [yy, setYy] = useState(String(today.getFullYear()));

  // Supplier
  const [supplierCode, setSupplierCode] = useState('');
  const [supplierName, setSupplierName] = useState('');

  // Currency
  const [currencyCode, setCurrencyCode] = useState('');
  const [currencyName, setCurrencyName] = useState('');
  const [currencyRate, setCurrencyRate] = useState('');

  // Summary Fields
  const [totalIssuedWt, setTotalIssuedWt] = useState('0.00');
  const [totalNoOfBills, setTotalNoOfBills] = useState('0');
  const [costPerGram, setCostPerGram] = useState('0.00');
  const [totalPrice, setTotalPrice] = useState('0.00');

  // Trade Bill Scan / Search
  const [tradeBillNo, setTradeBillNo] = useState('');

  // Grid Data
  const [gridItems, setGridItems] = useState<GridRowItem[]>([]);
  const [selectedRowIndex, setSelectedRowIndex] = useState<number | null>(null);

  // LOV State
  const [lovVisible, setLovVisible] = useState(false);
  const [lovType, setLovType] = useState<'issue' | 'supplier' | 'currency' | 'tradebill'>('supplier');
  const [lovData, setLovData] = useState<any[]>([]);
  const [lovSearch, setLovSearch] = useState('');
  const [lovLoading, setLovLoading] = useState(false);

  // Print Report Modal State
  const [isPrintModalVisible, setIsPrintModalVisible] = useState(false);
  const [printIssueNo, setPrintIssueNo] = useState('');

  const showToast = (message: string, type: 'success' | 'error' = 'success') => {
    setToast({ message, type });
    setTimeout(() => setToast(null), 3200);
  };

  useEffect(() => {
    fetchInitialData();
  }, []);

  const fetchInitialData = async () => {
    try {
      setLoading(true);
      const res = await fetch(`${API_URL}/nextIssueNo`);
      const data = await res.json();
      if (data.success && data.nextIssueNo) {
        setTradeIssueNo(data.nextIssueNo);
      }
    } catch (err) {
      console.error('Failed to load next issue no:', err);
    } finally {
      setLoading(false);
    }
  };

  // Recalculate totals
  const recalculateSummary = (items: GridRowItem[], costPerGmVal: string) => {
    const totalWt = items.reduce((sum, it) => sum + (it.PurchaseWeight || 0), 0);
    const count = items.length;
    const cpg = parseFloat(costPerGmVal) || 0;
    const totalPr = totalWt * cpg;

    setTotalIssuedWt(totalWt.toFixed(2));
    setTotalNoOfBills(String(count));
    setTotalPrice(totalPr.toFixed(2));
  };

  const handleCostPerGramChange = (text: string) => {
    setCostPerGram(text);
    const totalWt = parseFloat(totalIssuedWt) || 0;
    const cpg = parseFloat(text) || 0;
    setTotalPrice((totalWt * cpg).toFixed(2));
  };

  // Open LOV Modal
  const openLOV = async (type: 'issue' | 'supplier' | 'currency' | 'tradebill') => {
    setLovType(type);
    setLovSearch('');
    setLovVisible(true);
    setLovLoading(true);
    try {
      let endpoint = '';
      if (type === 'issue') endpoint = `${API_URL}/lov/issueList`;
      else if (type === 'supplier') endpoint = `${API_URL}/lov/suppliers`;
      else if (type === 'currency') endpoint = `${API_URL}/lov/currencies`;
      else if (type === 'tradebill') endpoint = `${API_URL}/lov/pendingTradeBills`;

      const res = await fetch(endpoint);
      const json = await res.json();
      if (json.success) {
        setLovData(json.data || []);
      } else {
        setLovData([]);
      }
    } catch (err) {
      console.error('LOV fetch error:', err);
      setLovData([]);
      showToast('Error loading lookup list.', 'error');
    } finally {
      setLovLoading(false);
    }
  };

  // Handle selecting an item from LOV
  const handleSelectLOV = async (item: any) => {
    setLovVisible(false);

    if (lovType === 'issue') {
      const issueNum = item.IssueNo;
      setTradeIssueNo(issueNum);
      loadExistingIssue(issueNum);
    } else if (lovType === 'supplier') {
      setSupplierCode(item.SupplierCode || '');
      setSupplierName(item.SupplierName || '');
      if (item.Currency) {
        fetchCurrencyDetails(item.Currency);
      }
    } else if (lovType === 'currency') {
      setCurrencyCode(item.CurrencyCode || '');
      setCurrencyName(item.CurrencyName || '');
      setCurrencyRate(parseFloat(item.CurrencyRate || 1).toFixed(2));
    } else if (lovType === 'tradebill') {
      addBillToGrid({
        InvoiceNo: item.InvoiceNo,
        Date: item.InvoiceDate ? item.InvoiceDate.substring(0, 10) : '',
        Class: 'TRADE-IN',
        PurchaseWeight: parseFloat(item.PurchaseWeight) || 0,
        IssuedWeight: parseFloat(item.BalanceWeight) || parseFloat(item.PurchaseWeight) || 0,
        BalanceWeight: parseFloat(item.BalanceWeight) || 0,
        Amount: parseFloat(item.TotalCost) || 0,
      });
      setTradeBillNo('');
    }
  };

  const fetchCurrencyDetails = async (code: string) => {
    try {
      const res = await fetch(`${API_URL}/currencyDetails?code=${encodeURIComponent(code)}`);
      const json = await res.json();
      if (json.success && json.data) {
        setCurrencyCode(json.data.CurrencyCode || code);
        setCurrencyName(json.data.CurrencyName || '');
        setCurrencyRate(parseFloat(json.data.CurrencyRate || 1).toFixed(2));
      }
    } catch (err) {
      console.error('Currency load error:', err);
    }
  };

  // Scan or manual enter of Trade Bill No
  const handleAddTradeBill = async (manualBillNo?: string) => {
    const billToSearch = (manualBillNo !== undefined ? manualBillNo : tradeBillNo).trim();
    if (!billToSearch) return;

    // Check if already in grid
    const alreadyExists = gridItems.some(
      (it) => it.InvoiceNo.toLowerCase() === billToSearch.toLowerCase()
    );
    if (alreadyExists) {
      showToast('This Trade Bill has already been added to the list.', 'error');
      setTradeBillNo('');
      return;
    }

    try {
      setLoading(true);
      const res = await fetch(`${API_URL}/getTradeBill?invoiceNo=${encodeURIComponent(billToSearch)}`);
      const json = await res.json();

      if (json.success && json.data) {
        const d = json.data;
        const balWt = parseFloat(d.BalanceWeight) || 0;
        if (balWt <= 0) {
          showToast(`Trade bill #${billToSearch} has zero balance weight.`, 'error');
          return;
        }

        addBillToGrid({
          InvoiceNo: d.InvoiceNo,
          Date: d.InvoiceDate ? d.InvoiceDate.substring(0, 10) : '',
          Class: 'TRADE-IN',
          PurchaseWeight: parseFloat(d.PurchaseWeight) || 0,
          IssuedWeight: balWt,
          BalanceWeight: balWt,
          Amount: parseFloat(d.TotalCost) || 0,
        });
        setTradeBillNo('');
      } else {
        showToast(json.message || `Trade Bill #${billToSearch} not found or no balance.`, 'error');
      }
    } catch (err) {
      console.error('Error fetching trade bill:', err);
      showToast('Error looking up Trade Bill.', 'error');
    } finally {
      setLoading(false);
    }
  };

  const addBillToGrid = (newItem: GridRowItem) => {
    const exists = gridItems.some(
      (it) => it.InvoiceNo.toLowerCase() === newItem.InvoiceNo.toLowerCase()
    );
    if (exists) {
      showToast('Bill already added to grid.', 'error');
      return;
    }

    const updated = [...gridItems, newItem];
    setGridItems(updated);
    recalculateSummary(updated, costPerGram);
    showToast(`Bill #${newItem.InvoiceNo} added successfully.`);
  };

  // Load Existing Saved Issue
  const loadExistingIssue = async (issueNum: string) => {
    try {
      setLoading(true);
      const res = await fetch(`${API_URL}/getIssueDetails?issueNo=${encodeURIComponent(issueNum)}`);
      const json = await res.json();
      if (json.success && json.data) {
        const h = json.data.header;
        const items = json.data.items || [];

        if (h) {
          if (h.IssueDate) {
            const dt = new Date(h.IssueDate);
            if (!isNaN(dt.getTime())) {
              setDd(String(dt.getDate()).padStart(2, '0'));
              setMm(String(dt.getMonth() + 1).padStart(2, '0'));
              setYy(String(dt.getFullYear()));
            }
          }
          setSupplierCode(h.SupplierCode || '');
          setSupplierName(h.SupplierName || '');
          setCurrencyCode(h.CurrencyCode || '');
          setCurrencyName(h.CurrencyName || '');
          setCurrencyRate(h.CurrencyRate ? parseFloat(h.CurrencyRate).toFixed(2) : '');
          setCostPerGram(parseFloat(h.CostPerGram || 0).toFixed(2));
          setTotalIssuedWt(parseFloat(h.TotalIssuedWeight || 0).toFixed(2));
          setTotalNoOfBills(String(h.NoOfBills || items.length));
          setTotalPrice(parseFloat(h.TotalPrice || 0).toFixed(2));
        }

        const mapped: GridRowItem[] = items.map((it: any) => ({
          InvoiceNo: it.InvoiceNo,
          Date: it.Date ? it.Date.substring(0, 10) : '',
          Class: 'TRADE-IN',
          PurchaseWeight: parseFloat(it.PurchaseWeight) || 0,
          IssuedWeight: parseFloat(it.IssuedWeight) || 0,
          BalanceWeight: parseFloat(it.BalanceWeight) || 0,
          Amount: (parseFloat(it.IssuedWeight) || 0) * (parseFloat(h?.CostPerGram) || 0),
        }));

        setGridItems(mapped);
        showToast(`Loaded Issue #${issueNum}`);
      } else {
        showToast(json.message || 'Issue record not found.', 'error');
      }
    } catch (err) {
      console.error('Error loading issue:', err);
      showToast('Failed to load issue record.', 'error');
    } finally {
      setLoading(false);
    }
  };

  // Clear (F6) - perfectly matches frmTradeInIssue.vb subClearMe()
  const handleClear = () => {
    setSupplierCode('');
    setSupplierName('');
    setCurrencyCode('');
    setCurrencyName('');
    setCurrencyRate('');
    setTradeBillNo('');
    setGridItems([]);
    setSelectedRowIndex(null);
    setTotalIssuedWt('0.00');
    setTotalNoOfBills('0');
    setCostPerGram('0.00');
    setTotalPrice('0.00');

    const dt = new Date();
    setDd(String(dt.getDate()).padStart(2, '0'));
    setMm(String(dt.getMonth() + 1).padStart(2, '0'));
    setYy(String(dt.getFullYear()));

    fetchInitialData();
    showToast('Form cleared.');
  };

  // Proceed (F7)
  const handleProceed = async () => {
    // Validations (frmTradeInIssue.vb btnProceed_Click)
    if (!supplierCode.trim()) {
      showToast('Please select a Supplier Code.', 'error');
      return;
    }
    if (gridItems.length === 0) {
      showToast('Please add at least one Trade In bill into the list.', 'error');
      return;
    }
    const cpgNum = parseFloat(costPerGram);
    if (isNaN(cpgNum) || cpgNum <= 0) {
      showToast('Please enter a valid Cost/Gram greater than 0.', 'error');
      return;
    }
    const totPriceNum = parseFloat(totalPrice);
    if (isNaN(totPriceNum) || totPriceNum <= 0) {
      showToast('Total Price cannot be zero.', 'error');
      return;
    }

    try {
      setSaving(true);
      const issueDateStr = `${yy}-${mm}-${dd}`;

      const payload = {
        issueNo: tradeIssueNo,
        issueDate: issueDateStr,
        supplierCode: supplierCode.trim(),
        supplierName: supplierName.trim(),
        currencyCode: currencyCode.trim(),
        currencyRate: parseFloat(currencyRate) || 1,
        totalIssuedWeight: parseFloat(totalIssuedWt) || 0,
        noOfBills: parseInt(totalNoOfBills, 10) || gridItems.length,
        costPerGram: cpgNum,
        totalPrice: totPriceNum,
        items: gridItems,
      };

      const res = await fetch(`${API_URL}/proceed`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload),
      });

      const json = await res.json();
      if (json.success) {
        showToast(json.message || 'Issue processed successfully!', 'success');
        setPrintIssueNo(tradeIssueNo);
        setIsPrintModalVisible(true);
        fetchInitialData();
      } else {
        showToast(json.message || 'Failed to process Trade In Issue.', 'error');
      }
    } catch (err: any) {
      console.error('Proceed error:', err);
      showToast('Error processing Trade In Issue.', 'error');
    } finally {
      setSaving(false);
    }
  };

  const filteredLOV = lovData.filter((item) => {
    const q = lovSearch.toLowerCase();
    if (lovType === 'issue') {
      return (
        (item.IssueNo && item.IssueNo.toLowerCase().includes(q)) ||
        (item.SupplierName && item.SupplierName.toLowerCase().includes(q))
      );
    } else if (lovType === 'supplier') {
      return (
        (item.SupplierCode && item.SupplierCode.toLowerCase().includes(q)) ||
        (item.SupplierName && item.SupplierName.toLowerCase().includes(q))
      );
    } else if (lovType === 'currency') {
      return (
        (item.CurrencyCode && item.CurrencyCode.toLowerCase().includes(q)) ||
        (item.CurrencyName && item.CurrencyName.toLowerCase().includes(q))
      );
    } else if (lovType === 'tradebill') {
      return (
        (item.InvoiceNo && item.InvoiceNo.toLowerCase().includes(q)) ||
        (item.CustomerName && item.CustomerName.toLowerCase().includes(q))
      );
    }
    return true;
  });

  return (
    <SafeAreaView style={styles.safeArea} edges={['top', 'bottom']}>
      <Stack.Screen options={{ headerShown: false }} />

      {/* Top Application Header matching SmartGold Standard */}
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
            <ThemedText style={styles.headerTitle}>TRADE IN GOLD ISSUE</ThemedText>
            <ThemedText style={styles.headerSubtitle}>
              Issue Voucher #{tradeIssueNo || '...'} • SmartGold System
            </ThemedText>
          </View>
        </View>

        <View style={styles.headerRight}>
          {/* <TouchableOpacity
            style={styles.headerActionBtn}
            onPress={() => openLOV('issue')}
            activeOpacity={0.7}
          >
            <Ionicons name="search-outline" size={14} color="#5C4A28" />
            <ThemedText style={styles.headerActionBtnText}>Find Issue</ThemedText>
          </TouchableOpacity> */}

          <TouchableOpacity
            style={styles.headerActionBtn}
            onPress={handleClear}
            activeOpacity={0.7}
          >
            <Ionicons name="refresh-outline" size={14} color="#5C4A28" />
            <ThemedText style={styles.headerActionBtnText}>Clear</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity
            style={styles.headerActionBtn}
            onPress={() => {
              if (tradeIssueNo.trim()) {
                setPrintIssueNo(tradeIssueNo.trim());
                setIsPrintModalVisible(true);
              } else {
                showToast('Please specify an Issue No to print.', 'error');
              }
            }}
            activeOpacity={0.7}
          >
            <Ionicons name="print-outline" size={14} color="#5C4A28" />
            <ThemedText style={styles.headerActionBtnText}>Print</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity
            style={[styles.headerActionBtn, styles.headerSaveBtn]}
            onPress={handleProceed}
            disabled={saving}
            activeOpacity={0.7}
          >
            {saving ? (
              <ActivityIndicator size="small" color="#9E6D08" />
            ) : (
              <>
                <Ionicons name="checkmark-done" size={16} color="#9E6D08" />
                <ThemedText style={styles.headerSaveBtnText}>Proceed</ThemedText>
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

      {/* Main Form Content */}
      <ScrollView
        contentContainerStyle={styles.container}
        keyboardShouldPersistTaps="handled"
      >
        {/* TOP SECTION: Supplier, Currency & Date Info (Left) + Premium Financial Summary Cards (Right) */}
        <View style={styles.topSection}>
          {/* Main Voucher Info Card */}
          <View style={styles.voucherCard}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerTitleRow}>
                <View style={styles.headerIconBadge}>
                  <Ionicons name="document-text-outline" size={14} color="#8D6409" />
                </View>
                <ThemedText style={styles.cardHeaderTitle}>ISSUE VOUCHER & SUPPLIER DETAILS</ThemedText>
              </View>

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
              {/* Row 1: Issue No & Search */}
              <View style={styles.formRow}>
                <ThemedText style={styles.rowLabel}>Issue No</ThemedText>
                <View style={styles.inputWithBtnContainer}>
                  <TextInput
                    style={[styles.input, styles.inputAttached, { width: 140, fontWeight: 'bold' }]}
                    value={tradeIssueNo}
                    onChangeText={setTradeIssueNo}
                    placeholder="Issue No..."
                    placeholderTextColor="#A89E8D"
                  />
                  <TouchableOpacity
                    style={styles.btnSearchDots}
                    onPress={() => openLOV('issue')}
                    activeOpacity={0.8}
                  >
                    <Ionicons name="search" size={13} color="#6E5528" />
                  </TouchableOpacity>
                </View>
              </View>

              {/* Row 2: Supplier Code & Name */}
              <View style={[styles.formRow, { marginTop: 8 }]}>
                <ThemedText style={styles.rowLabel}>Supplier</ThemedText>
                <View style={styles.inputWithBtnContainer}>
                  <TextInput
                    style={[styles.input, styles.inputAttached, { width: 120 }]}
                    value={supplierCode}
                    onChangeText={setSupplierCode}
                    placeholder="Code..."
                    placeholderTextColor="#A89E8D"
                  />
                  <TouchableOpacity
                    style={styles.btnSearchDots}
                    onPress={() => openLOV('supplier')}
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

              {/* Row 3: Currency & Rate */}
              <View style={[styles.formRow, { marginTop: 8 }]}>
                <ThemedText style={styles.rowLabel}>Currency</ThemedText>
                <View style={styles.inputWithBtnContainer}>
                  <TextInput
                    style={[styles.input, styles.inputAttached, { width: 85 }]}
                    value={currencyCode}
                    onChangeText={setCurrencyCode}
                    placeholder="Currency"
                    placeholderTextColor="#A89E8D"
                  />
                  <TouchableOpacity
                    style={styles.btnSearchDots}
                    onPress={() => openLOV('currency')}
                    activeOpacity={0.8}
                  >
                    <Ionicons name="search" size={13} color="#6E5528" />
                  </TouchableOpacity>
                </View>
                <TextInput
                  style={[styles.input, styles.readonlyInput, { flex: 1, marginLeft: 8 }]}
                  value={currencyName}
                  placeholder="Currency Name"
                  placeholderTextColor="#A89E8D"
                  editable={false}
                />
                <View style={{ flexDirection: 'row', alignItems: 'center', marginLeft: 8 }}>
                  <ThemedText style={[styles.rowLabel, { width: 'auto', marginRight: 6 }]}>Rate</ThemedText>
                  <TextInput
                    style={[styles.input, styles.readonlyInput, { width: 75, textAlign: 'right' }]}
                    value={currencyRate}
                    placeholder=""
                    placeholderTextColor="#A89E8D"
                    editable={false}
                  />
                </View>
              </View>
            </View>
          </View>

          {/* Right: Premium Financial Status Cards */}
          <View style={styles.rightFinancials}>
            {/* Total Issued Wt */}
            <View style={styles.financialCard}>
              <View style={styles.financialCardHeader}>
                <Ionicons name="scale-outline" size={13} color="#8D6409" />
                <ThemedText style={styles.financialCardTitle}>Total Issued Wt</ThemedText>
              </View>
              <View style={styles.financialValueBox}>
                <ThemedText style={styles.financialValueText}>
                  {totalIssuedWt} <Text style={{ fontSize: 11, fontWeight: 'normal', color: '#8D6409' }}>g</Text>
                </ThemedText>
              </View>
            </View>

            {/* Total No. of Bills */}
            <View style={styles.financialCard}>
              <View style={styles.financialCardHeader}>
                <Ionicons name="documents-outline" size={13} color="#8D6409" />
                <ThemedText style={styles.financialCardTitle}>No. of Bills</ThemedText>
              </View>
              <View style={styles.financialValueBox}>
                <ThemedText style={styles.financialValueText}>
                  {totalNoOfBills}
                </ThemedText>
              </View>
            </View>

            {/* Cost / Gram (Editable) */}
            <View style={[styles.financialCard, styles.tradeInCard]}>
              <View style={styles.financialCardHeader}>
                <Ionicons name="pricetag-outline" size={13} color="#8D6409" />
                <ThemedText style={styles.financialCardTitle}>Cost / Gram</ThemedText>
              </View>
              <View style={styles.financialValueBox}>
                <ThemedText style={styles.currencyPrefix}>$</ThemedText>
                <TextInput
                  style={styles.financialInput}
                  value={costPerGram}
                  onChangeText={handleCostPerGramChange}
                  keyboardType="numeric"
                  placeholder="0.00"
                  placeholderTextColor="#A89E8D"
                />
              </View>
            </View>

            {/* Total Price (Calculated) */}
            <View style={[styles.financialCard, { borderColor: '#2E7D32', borderWidth: 1.5 }]}>
              <View style={[styles.financialCardHeader, { backgroundColor: '#E8F5E9', borderBottomColor: '#C8E6C9' }]}>
                <Ionicons name="cash-outline" size={13} color="#2E7D32" />
                <ThemedText style={[styles.financialCardTitle, { color: '#1B5E20' }]}>Total Price</ThemedText>
              </View>
              <View style={[styles.financialValueBox, { backgroundColor: '#F1F8E9' }]}>
                <ThemedText style={[styles.currencyPrefix, { color: '#2E7D32' }]}>$</ThemedText>
                <ThemedText style={[styles.financialValueText, { color: '#1B5E20' }]}>
                  {totalPrice}
                </ThemedText>
              </View>
            </View>
          </View>
        </View>

        {/* MIDDLE SECTION: Scan / Search Bar + Data Grid */}
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
              <ThemedText style={styles.scanPromptLabel}>Scan / Enter Trade Bill:</ThemedText>
              <View style={styles.inputWithBtnContainer}>
                <TextInput
                  style={[styles.input, styles.inputAttached, { width: 150 }]}
                  value={tradeBillNo}
                  onChangeText={setTradeBillNo}
                  placeholder="Bill / Invoice No..."
                  placeholderTextColor="#A89E8D"
                  onSubmitEditing={() => handleAddTradeBill()}
                />
                <TouchableOpacity
                  style={styles.btnSearchDots}
                  onPress={() => openLOV('tradebill')}
                  activeOpacity={0.8}
                >
                  <Ionicons name="search" size={13} color="#6E5528" />
                </TouchableOpacity>
              </View>

              <TouchableOpacity
                style={styles.addBillBtn}
                onPress={() => handleAddTradeBill()}
                activeOpacity={0.8}
              >
                <Ionicons name="add" size={14} color="#9E6D08" style={{ marginRight: 2 }} />
                <ThemedText style={styles.addBillBtnText}>Add Bill</ThemedText>
              </TouchableOpacity>
            </View>
          </View>

          {/* Table Header */}
          <View style={styles.gridHeader}>
            <ThemedText style={[styles.gridHeaderText, { width: 45, textAlign: 'center' }]}>#</ThemedText>
            <ThemedText style={[styles.gridHeaderText, { flex: 2 }]}>Invoice No</ThemedText>
            <ThemedText style={[styles.gridHeaderText, { flex: 1.5 }]}>Date</ThemedText>
            <ThemedText style={[styles.gridHeaderText, { flex: 1.5 }]}>Class</ThemedText>
            <ThemedText style={[styles.gridHeaderText, { width: 130, textAlign: 'right' }]}>Purchase Weight (g)</ThemedText>
            <ThemedText style={[styles.gridHeaderText, { width: 130, textAlign: 'right' }]}>Issued Weight (g)</ThemedText>
            <ThemedText style={[styles.gridHeaderText, { width: 60, textAlign: 'center' }]}>Action</ThemedText>
          </View>

          {/* Table Body */}
          <View style={styles.gridBody}>
            {gridItems.length === 0 ? (
              <View style={styles.gridEmptyArea}>
                <Ionicons name="receipt-outline" size={32} color="#C8BEAC" />
                <ThemedText style={styles.emptyGridText}>No Trade-In bills added to issue list yet</ThemedText>
                <ThemedText style={styles.emptyGridSubText}>
                  Scan or search a Trade Bill number above and click "Add Bill"
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
                    <ThemedText style={[styles.gridCellText, { width: 45, textAlign: 'center', color: '#8D6409', fontWeight: 'bold' }]}>
                      {index + 1}
                    </ThemedText>
                    <ThemedText style={[styles.gridCellText, { flex: 2, fontWeight: 'bold' }]}>
                      {item.InvoiceNo}
                    </ThemedText>
                    <ThemedText style={[styles.gridCellText, { flex: 1.5 }]}>
                      {item.Date}
                    </ThemedText>
                    <View style={{ flex: 1.5, justifyContent: 'center' }}>
                      <View style={styles.classPill}>
                        <ThemedText style={styles.classPillText}>{item.Class}</ThemedText>
                      </View>
                    </View>
                    <ThemedText style={[styles.gridCellText, { width: 130, textAlign: 'right', fontWeight: '600' }]}>
                      {item.PurchaseWeight.toFixed(2)}
                    </ThemedText>
                    <ThemedText style={[styles.gridCellText, styles.gridCellAmount, { width: 130, textAlign: 'right', fontWeight: 'bold' }]}>
                      {item.IssuedWeight.toFixed(2)}
                    </ThemedText>
                    <View style={{ width: 60, alignItems: 'center', justifyContent: 'center' }}>
                      <TouchableOpacity
                        onPress={() => {
                          const updated = gridItems.filter((_, idx) => idx !== index);
                          setGridItems(updated);
                          recalculateSummary(updated, costPerGram);
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

      {/* LOV Search Modal */}
      <Modal
        visible={lovVisible}
        transparent
        animationType="fade"
        onRequestClose={() => setLovVisible(false)}
      >
        <TouchableOpacity
          style={styles.modalOverlay}
          activeOpacity={1}
          onPress={() => setLovVisible(false)}
        >
          <TouchableWithoutFeedback>
            <View style={styles.lovModalContainer}>
              <View style={styles.modalHeaderBanner}>
                <View style={styles.headerTitleRow}>
                  <View style={styles.headerIconBadge}>
                    <Ionicons name="search" size={14} color="#8D6409" />
                  </View>
                  <ThemedText style={styles.modalHeaderTitle}>
                    {lovType === 'issue'
                      ? 'SELECT SAVED ISSUE VOUCHER'
                      : lovType === 'supplier'
                        ? 'SELECT SUPPLIER'
                        : lovType === 'currency'
                          ? 'SELECT CURRENCY'
                          : 'SELECT PENDING TRADE-IN BILL'}
                  </ThemedText>
                </View>
                <TouchableOpacity
                  onPress={() => setLovVisible(false)}
                  style={styles.modalCloseBtn}
                >
                  <Ionicons name="close" size={18} color="#5C4A28" />
                </TouchableOpacity>
              </View>

              <View style={styles.modalSearchBar}>
                <Ionicons name="search" size={15} color="#8D6409" style={{ marginRight: 6 }} />
                <TextInput
                  style={styles.modalSearchInput}
                  placeholder="Type to filter records..."
                  placeholderTextColor="#A89E8D"
                  value={lovSearch}
                  onChangeText={setLovSearch}
                  autoFocus
                />
              </View>

              {lovLoading ? (
                <View style={styles.modalLoading}>
                  <ActivityIndicator size="large" color="#B8903B" />
                  <ThemedText style={{ marginTop: 8, color: '#6E5528', fontSize: 12 }}>
                    Loading lookup records...
                  </ThemedText>
                </View>
              ) : (
                <FlatList
                  data={filteredLOV}
                  keyExtractor={(item, index) => `${item.IssueNo || item.SupplierCode || item.CurrencyCode || item.InvoiceNo}-${index}`}
                  style={{ maxHeight: 360, minHeight: 160 }}
                  renderItem={({ item, index }) => (
                    <TouchableOpacity
                      style={[
                        styles.lovItem,
                        index % 2 === 1 && { backgroundColor: '#FCFAF6' },
                      ]}
                      activeOpacity={0.7}
                      onPress={() => handleSelectLOV(item)}
                    >
                      {lovType === 'issue' ? (
                        <View style={{ flex: 1 }}>
                          <View style={{ flexDirection: 'row', justifyContent: 'space-between' }}>
                            <ThemedText style={styles.lovItemCode}>Issue #{item.IssueNo}</ThemedText>
                            <ThemedText style={styles.lovItemSub}>
                              {item.IssueDate ? item.IssueDate.substring(0, 10) : ''}
                            </ThemedText>
                          </View>
                          <ThemedText style={styles.lovItemName}>{item.SupplierName}</ThemedText>
                          <ThemedText style={styles.lovItemSub}>
                            Issued Wt: {parseFloat(item.TotalIssuedWeight || 0).toFixed(2)}g | Price: ${parseFloat(item.TotalPrice || 0).toFixed(2)}
                          </ThemedText>
                        </View>
                      ) : lovType === 'supplier' ? (
                        <View style={{ flex: 1 }}>
                          <View style={{ flexDirection: 'row', justifyContent: 'space-between' }}>
                            <ThemedText style={styles.lovItemCode}>{item.SupplierCode}</ThemedText>
                            <ThemedText style={styles.lovItemType}>{item.Currency || 'SGD'}</ThemedText>
                          </View>
                          <ThemedText style={styles.lovItemName}>{item.SupplierName}</ThemedText>
                          <ThemedText style={styles.lovItemSub}>
                            {[item.Address1, item.Address2].filter(Boolean).join(', ')}
                          </ThemedText>
                        </View>
                      ) : lovType === 'currency' ? (
                        <View style={{ flex: 1, flexDirection: 'row', justifyContent: 'space-between', alignItems: 'center' }}>
                          <View>
                            <ThemedText style={styles.lovItemCode}>{item.CurrencyCode}</ThemedText>
                            <ThemedText style={styles.lovItemName}>{item.CurrencyName}</ThemedText>
                          </View>
                          <ThemedText style={styles.lovItemRate}>Rate: {parseFloat(item.CurrencyRate || 1).toFixed(4)}</ThemedText>
                        </View>
                      ) : (
                        <View style={{ flex: 1 }}>
                          <View style={{ flexDirection: 'row', justifyContent: 'space-between' }}>
                            <ThemedText style={styles.lovItemCode}>Bill #{item.InvoiceNo}</ThemedText>
                            <ThemedText style={styles.lovItemType}>Bal Wt: {parseFloat(item.BalanceWeight || 0).toFixed(2)}g</ThemedText>
                          </View>
                          <ThemedText style={styles.lovItemName}>{item.CustomerName || 'Trade Customer'}</ThemedText>
                          <ThemedText style={styles.lovItemSub}>
                            Purchase Wt: {parseFloat(item.PurchaseWeight || 0).toFixed(2)}g | Date: {item.InvoiceDate ? item.InvoiceDate.substring(0, 10) : 'N/A'}
                          </ThemedText>
                        </View>
                      )}
                    </TouchableOpacity>
                  )}
                  ListEmptyComponent={() => (
                    <ThemedText style={styles.emptyText}>No matching records found.</ThemedText>
                  )}
                />
              )}
            </View>
          </TouchableWithoutFeedback>
        </TouchableOpacity>
      </Modal>

      {/* Standardized Print Report Modal */}
      <PrintReportModal
        visible={isPrintModalVisible}
        title="Trade In Gold Issue Report"
        message={`Do you want to print Issue Report for #${printIssueNo}?`}
        loading={loading}
        onCancel={() => setIsPrintModalVisible(false)}
        onPdf={() => {
          setIsPrintModalVisible(false);
          showToast('Opening PDF report...');
        }}
        onExcel={() => {
          setIsPrintModalVisible(false);
          showToast('Exporting Excel report...');
        }}
        onPrint={() => {
          setIsPrintModalVisible(false);
          showToast('Sending to printer...');
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
    fontSize: 16,
    fontWeight: 'bold',
    color: '#3B2D17',
    letterSpacing: 0.5,
  },
  headerSubtitle: {
    fontSize: 11,
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
    paddingVertical: 6,
    paddingHorizontal: 12,
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
    paddingVertical: 9,
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
    padding: 14,
    paddingBottom: 40,
  },

  // TOP SECTION: Voucher Card & Financials
  topSection: {
    flexDirection: 'row',
    gap: 12,
    marginBottom: 12,
  },
  voucherCard: {
    flex: 1,
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
  },
  cardHeaderBanner: {
    backgroundColor: '#F5E7CA',
    paddingHorizontal: 10,
    paddingVertical: 5,
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
    padding: 8,
    paddingVertical: 6,
  },
  formRow: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  rowLabel: {
    fontSize: 10.5,
    fontWeight: 'bold',
    color: '#554428',
    width: 70,
  },
  inputWithBtnContainer: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  input: {
    height: 25,
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#D8CDBA',
    borderRadius: 4,
    paddingHorizontal: 7,
    paddingVertical: 0,
    fontSize: 10.5,
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
    width: 25,
    height: 25,
    backgroundColor: '#F5EBD4',
    borderWidth: 1,
    borderColor: '#D8CDBA',
    borderTopRightRadius: 4,
    borderBottomRightRadius: 4,
    alignItems: 'center',
    justifyContent: 'center',
  },

  // RIGHT FINANCIALS
  rightFinancials: {
    width: 190,
    gap: 4,
  },
  financialCard: {
    backgroundColor: '#FFFFFF',
    borderRadius: 6,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    shadowColor: '#A38035',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.04,
    shadowRadius: 2,
    elevation: 1,
    overflow: 'hidden',
  },
  tradeInCard: {
    borderColor: '#B8903B',
    borderWidth: 1.5,
  },
  financialCardHeader: {
    backgroundColor: '#F5E7CA',
    paddingHorizontal: 7,
    paddingVertical: 2,
    flexDirection: 'row',
    alignItems: 'center',
    gap: 4,
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
  },
  financialCardTitle: {
    fontSize: 9.5,
    fontWeight: 'bold',
    color: '#3B2D17',
    letterSpacing: 0.2,
  },
  financialValueBox: {
    backgroundColor: '#FFFDF9',
    paddingHorizontal: 7,
    paddingVertical: 2,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'flex-end',
    minHeight: 20,
    height: 22,
  },
  currencyPrefix: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#8D6409',
    marginRight: 3,
  },
  financialValueText: {
    fontSize: 11.5,
    fontWeight: 'bold',
    color: '#3B2D17',
  },
  financialInput: {
    fontSize: 11.5,
    fontWeight: 'bold',
    color: '#3B2D17',
    textAlign: 'right',
    paddingVertical: 0,
    paddingHorizontal: 3,
    minWidth: 60,
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
    marginBottom: 12,
  },
  scanBanner: {
    backgroundColor: '#F5E7CA',
    paddingHorizontal: 12,
    paddingVertical: 8,
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
    fontSize: 11,
    fontWeight: 'bold',
    color: '#554428',
  },
  addBillBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFF8EA',
    borderWidth: 1,
    borderColor: '#B8903B',
    paddingVertical: 5,
    paddingHorizontal: 10,
    borderRadius: 5,
  },
  addBillBtnText: {
    color: '#9E6D08',
    fontWeight: 'bold',
    fontSize: 11,
  },

  // Grid Header
  gridHeader: {
    flexDirection: 'row',
    backgroundColor: '#FAF5EA',
    borderBottomWidth: 1,
    borderBottomColor: '#E8DCB8',
    paddingHorizontal: 10,
    paddingVertical: 8,
    alignItems: 'center',
  },
  gridHeaderText: {
    fontSize: 11,
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
    fontSize: 13,
    fontWeight: 'bold',
    color: '#8D6409',
    marginTop: 6,
  },
  emptyGridSubText: {
    fontSize: 11,
    color: '#A89E8D',
  },
  gridRow: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 10,
    paddingVertical: 8,
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
    fontSize: 11,
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
    fontSize: 10,
    fontWeight: 'bold',
    color: '#9E6D08',
  },
  rowDeleteBtn: {
    width: 24,
    height: 24,
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
    paddingVertical: 8,
  },
  bottomButtonsRow: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 8,
  },
  actionBtnSecondary: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 4,
    backgroundColor: '#FAF5EA',
    borderWidth: 1,
    borderColor: '#DFD5C3',
    paddingVertical: 6,
    paddingHorizontal: 12,
    borderRadius: 6,
  },
  actionBtnSecondaryText: {
    color: '#5C4A28',
    fontWeight: 'bold',
    fontSize: 11,
  },
  actionBtnPrimary: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 5,
    backgroundColor: '#F9F2E2',
    borderWidth: 1,
    borderColor: '#B8903B',
    paddingVertical: 6,
    paddingHorizontal: 16,
    borderRadius: 6,
  },
  actionBtnPrimaryText: {
    color: '#9E6D08',
    fontWeight: 'bold',
    fontSize: 11,
  },
  actionBtnClose: {
    backgroundColor: '#F5F5F5',
    borderWidth: 1,
    borderColor: '#DDDDDD',
    paddingVertical: 6,
    paddingHorizontal: 12,
    borderRadius: 6,
  },
  actionBtnCloseText: {
    color: '#666666',
    fontWeight: 'bold',
    fontSize: 11,
  },

  // LOV Modal
  modalOverlay: {
    flex: 1,
    backgroundColor: 'rgba(0, 0, 0, 0.45)',
    justifyContent: 'center',
    alignItems: 'center',
    padding: 16,
  },
  lovModalContainer: {
    width: '100%',
    maxWidth: 540,
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.15,
    shadowRadius: 8,
    elevation: 8,
    overflow: 'hidden',
  },
  modalHeaderBanner: {
    backgroundColor: '#F5E7CA',
    paddingHorizontal: 12,
    paddingVertical: 8,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
  },
  modalHeaderTitle: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#3B2D17',
    letterSpacing: 0.4,
  },
  modalCloseBtn: {
    padding: 4,
  },
  modalSearchBar: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 12,
    paddingVertical: 8,
    borderBottomWidth: 1,
    borderBottomColor: '#F0E6D2',
    backgroundColor: '#FAF5EA',
  },
  modalSearchInput: {
    flex: 1,
    height: 30,
    fontSize: 12,
    color: '#3B2D17',
    paddingVertical: 0,
  },
  modalLoading: {
    padding: 30,
    alignItems: 'center',
    justifyContent: 'center',
  },
  lovItem: {
    paddingHorizontal: 14,
    paddingVertical: 10,
    borderBottomWidth: 1,
    borderBottomColor: '#F0E6D2',
  },
  lovItemCode: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#9E6D08',
  },
  lovItemName: {
    fontSize: 12,
    fontWeight: '600',
    color: '#3B2D17',
    marginTop: 2,
  },
  lovItemSub: {
    fontSize: 10,
    color: '#7D6F5B',
    marginTop: 2,
  },
  lovItemType: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#2E7D32',
  },
  lovItemRate: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#8D6409',
  },
  emptyText: {
    padding: 24,
    textAlign: 'center',
    color: '#A89E8D',
    fontSize: 12,
  },
});
