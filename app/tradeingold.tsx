/** Developer: Antigravity */
import { ThemedText } from '@/components/themed-text';
import { API_BASE_URL } from '@/constants/api';
import { useThemeColors } from '@/hooks/useThemeColors';
import { Ionicons } from '@expo/vector-icons';
import { Stack, useLocalSearchParams, useRouter } from 'expo-router';
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

const API_URL = `${API_BASE_URL}/tradeingold`;

interface TradeItem {
  id: string;
  inventoryCode: string;
  description: string;
  weight: string;
  goldLoss: string;
  costPerGram: string;
  totalCost: string;
  stoneDescription: string;
  stoneWeight: string;
  itemClass: string;
}

interface CustomerLOVItem {
  Code: string;
  Name: string;
  Address1: string;
  Address2: string;
  Address3: string;
  CustomerType: string;
  SourceType: string;
}

const CLASS_OPTIONS = ['21K', '20K', '22K', '24K', '18K'];

export default function TradeInGoldScreen() {
  const themeColors = useThemeColors();
  const router = useRouter();
  const searchParams = useLocalSearchParams();

  // State Management
  const [loading, setLoading] = useState(false);
  const [saving, setSaving] = useState(false);
  const [toast, setToast] = useState<{ message: string; type: 'success' | 'error' } | null>(null);

  // Header Details
  const [invoiceNo, setInvoiceNo] = useState('00000000');
  const [customerCode, setCustomerCode] = useState('');
  const [memberCode, setMemberCode] = useState('');
  const [customerName, setCustomerName] = useState('');
  const [address1, setAddress1] = useState('');
  const [address2, setAddress2] = useState('');
  const [address3, setAddress3] = useState('');
  const [customerType, setCustomerType] = useState('Customer Type');
  const [remarks, setRemarks] = useState('');

  // Financial Stats
  const [salesAmount, setSalesAmount] = useState('0.00');
  const [tradeInLimit, setTradeInLimit] = useState('0.00');

  // Input Row 1 (Grid Inputs)
  const [description, setDescription] = useState('21K ITEMS');
  const [weight, setWeight] = useState('');
  const [percentage, setPercentage] = useState('0');
  const [costPerGram, setCostPerGram] = useState('190');
  const [totalCost, setTotalCost] = useState('');

  // Input Row 2
  const [stoneDescription, setStoneDescription] = useState('');
  const [stoneWeight, setStoneWeight] = useState('');

  // Class Selection
  const [selectedClass, setSelectedClass] = useState('21K');
  const [marketBenchmarkRate, setMarketBenchmarkRate] = useState(190);

  // Grid Records
  const [items, setItems] = useState<TradeItem[]>([]);
  const [selectedIndex, setSelectedIndex] = useState<number | null>(null);

  // Customer LOV Modal
  const [lovVisible, setLovVisible] = useState(false);
  const [lovData, setLovData] = useState<CustomerLOVItem[]>([]);
  const [lovSearch, setLovSearch] = useState('');
  const [lovLoading, setLovLoading] = useState(false);

  // Transaction Search / History LOV
  const [historyVisible, setHistoryVisible] = useState(false);
  const [historyData, setHistoryData] = useState<any[]>([]);
  const [historySearch, setHistorySearch] = useState('');
  const [historyLoading, setHistoryLoading] = useState(false);

  // Super Admin Password Modal
  const [adminModalVisible, setAdminModalVisible] = useState(false);
  const [adminPassword, setAdminPassword] = useState('');
  const [adminPendingItem, setAdminPendingItem] = useState<TradeItem | null>(null);

  const showToast = (message: string, type: 'success' | 'error' = 'success') => {
    setToast({ message, type });
    setTimeout(() => setToast(null), 3200);
  };

  useEffect(() => {
    fetchInitialData();
  }, []);

  // Handle passed parameters from caller screens (e.g., installmentorderentry.tsx)
  useEffect(() => {
    if (searchParams) {
      const pInvNo = (searchParams.invoiceNo || searchParams.sInvoiceNo) as string;
      if (pInvNo && pInvNo.trim()) {
        setInvoiceNo(pInvNo.trim());
      }
      const pCustCode = (searchParams.customerCode || searchParams.txtCustomerCode) as string;
      if (pCustCode !== undefined && pCustCode !== null) {
        setCustomerCode(String(pCustCode));
      }
      const pCustName = (searchParams.customerName || searchParams.txtCustomerName) as string;
      if (pCustName !== undefined && pCustName !== null) {
        setCustomerName(String(pCustName));
      }
      const pCustType = (searchParams.customerType || searchParams.lblCustomerType) as string;
      if (pCustType && pCustType.trim()) {
        setCustomerType(String(pCustType).trim());
      }
      const pAddr1 = (searchParams.address1 || searchParams.txtAddress1) as string;
      if (pAddr1 !== undefined && pAddr1 !== null) {
        setAddress1(String(pAddr1));
      }
      const pAddr2 = (searchParams.address2 || searchParams.txtAddress2) as string;
      if (pAddr2 !== undefined && pAddr2 !== null) {
        setAddress2(String(pAddr2));
      }
      const pAddr3 = (searchParams.address3 || searchParams.txtAddress3) as string;
      if (pAddr3 !== undefined && pAddr3 !== null) {
        setAddress3(String(pAddr3));
      }
      const pLimit = (searchParams.tradeInLimit || searchParams.lblTradeInLimit) as string;
      if (pLimit && !isNaN(parseFloat(pLimit))) {
        setTradeInLimit(parseFloat(pLimit).toFixed(2));
      }
      const pSalesAmt = (searchParams.salesAmount || searchParams.sSalesAmount || searchParams.lblSalesAmount) as string;
      if (pSalesAmt && !isNaN(parseFloat(pSalesAmt))) {
        setSalesAmount(parseFloat(pSalesAmt).toFixed(2));
      }
      const pRemarks = searchParams.remarks as string;
      if (pRemarks !== undefined && pRemarks !== null) {
        setRemarks(String(pRemarks));
      }
    }
  }, [searchParams]);

  useEffect(() => {
    setDescription(`${selectedClass} ITEMS`);
    fetchClassRate(selectedClass);
  }, [selectedClass]);

  const fetchInitialData = async () => {
    try {
      setLoading(true);
      const res = await fetch(`${API_URL}/next-no`);
      const data = await res.json();
      if (data.success) {
        // Only set default invoiceNo from API if not passed from navigation
        const passedInv = (searchParams?.invoiceNo || searchParams?.sInvoiceNo) as string;
        if (!passedInv || !passedInv.trim()) {
          setInvoiceNo(data.invoiceNo || '00000001');
        }
        // Only set tradeInLimit from API if not already supplied
        const passedLimit = (searchParams?.tradeInLimit || searchParams?.lblTradeInLimit) as string;
        if (!passedLimit || isNaN(parseFloat(passedLimit))) {
          setTradeInLimit((parseFloat(data.tradeInLimit) || 0).toFixed(2));
        }
      }
      await fetchClassRate(selectedClass);
    } catch (err: any) {
      console.error('Initial data error:', err);
      showToast('Failed to load initial company info', 'error');
    } finally {
      setLoading(false);
    }
  };

  const fetchClassRate = async (classCode: string) => {
    try {
      const res = await fetch(`${API_URL}/costpergram/${encodeURIComponent(classCode)}`);
      const data = await res.json();
      if (data.success && data.Rate !== undefined) {
        const rate = parseFloat(data.Rate) || 0;
        setMarketBenchmarkRate(rate);
        setCostPerGram(String(rate));
        calculateTotalCost(weight, String(rate), classCode);
      }
    } catch (err) {
      console.error('Fetch rate error:', err);
    }
  };

  const calculateTotalCost = (wtStr: string, cpgStr: string, cls: string = selectedClass) => {
    const wt = parseFloat(wtStr) || 0;
    const cpg = parseFloat(cpgStr) || 0;
    if (wt <= 0 || cpg <= 0) {
      setTotalCost('');
      return;
    }
    const rawCost = wt * cpg;
    const finalCost = cls === '24K' ? rawCost * 1.05 : rawCost;
    setTotalCost(finalCost.toFixed(2));
  };

  const handleWeightChange = (val: string) => {
    setWeight(val);
    calculateTotalCost(val, costPerGram, selectedClass);
  };

  const handleCostPerGramChange = (val: string) => {
    setCostPerGram(val);
    calculateTotalCost(weight, val, selectedClass);
  };

  const handleTotalCostChange = (val: string) => {
    setTotalCost(val);
    const wt = parseFloat(weight) || 0;
    const tc = parseFloat(val) || 0;
    if (wt > 0 && tc > 0) {
      const computedCPG = tc / wt;
      setCostPerGram(computedCPG.toFixed(2));
    }
  };

  // Customer LOV handling
  const openCustomerLOV = async () => {
    try {
      setLovVisible(true);
      setLovLoading(true);
      const res = await fetch(`${API_URL}/customers`);
      const data = await res.json();
      if (data.success && Array.isArray(data.data)) {
        setLovData(data.data);
      } else {
        setLovData([]);
      }
    } catch (err) {
      console.error('Customer LOV error:', err);
      showToast('Error loading customer list', 'error');
    } finally {
      setLovLoading(false);
    }
  };

  const handleSelectCustomer = (c: CustomerLOVItem) => {
    if (c.SourceType === 'Member') {
      setMemberCode(c.Code);
      setCustomerCode('CASH');
      setCustomerType('Member');
    } else {
      setCustomerCode(c.Code);
      setMemberCode('');
      setCustomerType(c.CustomerType || 'Customer');
    }
    setCustomerName(c.Name || '');
    setAddress1(c.Address1 || '');
    setAddress2(c.Address2 || '');
    setAddress3(c.Address3 || '');
    setLovVisible(false);
  };

  // Search History LOV
  const openHistoryLOV = async () => {
    try {
      setHistoryVisible(true);
      setHistoryLoading(true);
      const res = await fetch(`${API_URL}/list`);
      const data = await res.json();
      if (data.success && Array.isArray(data.data)) {
        setHistoryData(data.data);
      } else {
        setHistoryData([]);
      }
    } catch (err) {
      console.error('History list error:', err);
      showToast('Error loading transaction history', 'error');
    } finally {
      setHistoryLoading(false);
    }
  };

  const handleSelectTransaction = async (item: any) => {
    try {
      setHistoryVisible(false);
      setLoading(true);
      const res = await fetch(`${API_URL}/transaction/${encodeURIComponent(item.InventoryCode || item.InvoiceNo)}`);
      const data = await res.json();
      if (data.success && data.header) {
        const h = data.header;
        setInvoiceNo(h.InvoiceNo || '00000001');
        setCustomerCode(h.CustomerCode || '');
        setMemberCode(h.MemberCode || '');
        setCustomerName(h.CustomerName || '');
        setAddress1(h.Address1 || '');
        setAddress2(h.Address2 || '');
        setAddress3(h.Address3 || '');
        setRemarks(h.Remarks || '');
        setCustomerType(h.MemberCode ? 'Member' : 'Customer');

        if (Array.isArray(data.details)) {
          const loadedItems: TradeItem[] = data.details.map((d: any, idx: number) => ({
            id: String(idx + 1),
            inventoryCode: String(idx + 1),
            description: d.Description || '',
            weight: (parseFloat(d.Weight) || 0).toFixed(3),
            goldLoss: (parseFloat(d.GoldLoss) || 0).toFixed(2),
            costPerGram: (parseFloat(d.CostPerGram) || 0).toFixed(2),
            totalCost: (parseFloat(d.TotalCost) || 0).toFixed(2),
            stoneDescription: d.DiamondDescription || '',
            stoneWeight: (parseFloat(d.StoneWeight) || 0).toFixed(3),
            itemClass: d.Class || '21K',
          }));
          setItems(loadedItems);
        }
        showToast(`Loaded Invoice #${h.InvoiceNo}`, 'success');
      }
    } catch (err) {
      console.error('Load transaction error:', err);
      showToast('Error loading transaction details', 'error');
    } finally {
      setLoading(false);
    }
  };

  // Add Item to Grid
  const handleAddItem = () => {
    if (!description.trim()) {
      showToast('Description cannot be blank.', 'error');
      return;
    }
    const wt = parseFloat(weight);
    if (isNaN(wt) || wt <= 0) {
      showToast('Weight is invalid or cannot be zero.', 'error');
      return;
    }
    const cpg = parseFloat(costPerGram);
    if (isNaN(cpg) || cpg <= 0) {
      showToast('Cost Per Gram is invalid.', 'error');
      return;
    }
    const tc = parseFloat(totalCost);
    if (isNaN(tc) || tc <= 0) {
      showToast('Total Cost is invalid.', 'error');
      return;
    }

    const newItem: TradeItem = {
      id: selectedIndex !== null ? items[selectedIndex].id : String(items.length + 1),
      inventoryCode: selectedIndex !== null ? items[selectedIndex].inventoryCode : String(items.length + 1),
      description: description.trim(),
      weight: wt.toFixed(3),
      goldLoss: (parseFloat(percentage) || 0).toFixed(2),
      costPerGram: cpg.toFixed(2),
      totalCost: tc.toFixed(2),
      stoneDescription: stoneDescription.trim(),
      stoneWeight: (parseFloat(stoneWeight) || 0).toFixed(3),
      itemClass: selectedClass,
    };

    if (cpg > marketBenchmarkRate && marketBenchmarkRate > 0) {
      setAdminPendingItem(newItem);
      setAdminModalVisible(true);
      return;
    }

    commitItem(newItem);
  };

  const commitItem = (itemToAdd: TradeItem) => {
    if (selectedIndex !== null) {
      const updated = [...items];
      updated[selectedIndex] = itemToAdd;
      setItems(updated);
      setSelectedIndex(null);
      showToast('Item updated in grid.');
    } else {
      setItems([...items, itemToAdd]);
      showToast('Item added to grid.');
    }
    handleClearInputs();
  };

  const handleVerifyAdminPassword = async () => {
    if (!adminPassword.trim()) {
      showToast('Please enter Super Admin password.', 'error');
      return;
    }
    try {
      const res = await fetch(`${API_URL}/verify-admin`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ password: adminPassword }),
      });
      const data = await res.json();
      if (data.success && adminPendingItem) {
        commitItem(adminPendingItem);
        setAdminModalVisible(false);
        setAdminPassword('');
        setAdminPendingItem(null);
      } else {
        showToast(data.message || 'Invalid Super Admin password', 'error');
      }
    } catch (err) {
      showToast('Error verifying password', 'error');
    }
  };

  const handleSelectGridRow = (index: number) => {
    const item = items[index];
    setSelectedIndex(index);
    setDescription(item.description);
    setWeight(item.weight);
    setPercentage(item.goldLoss);
    setCostPerGram(item.costPerGram);
    setTotalCost(item.totalCost);
    setStoneDescription(item.stoneDescription);
    setStoneWeight(item.stoneWeight);
    setSelectedClass(item.itemClass || '21K');
  };

  const handleDeleteItem = () => {
    if (selectedIndex === null) {
      showToast('Please select a row in the table to delete.', 'error');
      return;
    }
    const updated = items.filter((_, idx) => idx !== selectedIndex);
    const reindexed = updated.map((it, idx) => ({
      ...it,
      inventoryCode: String(idx + 1),
      id: String(idx + 1),
    }));
    setItems(reindexed);
    setSelectedIndex(null);
    handleClearInputs();
    showToast('Item deleted successfully.');
  };

  const handleClearInputs = () => {
    setSelectedIndex(null);
    setDescription(`${selectedClass} ITEMS`);
    setWeight('');
    setPercentage('0');
    setStoneDescription('');
    setStoneWeight('');
    fetchClassRate(selectedClass);
  };

  const handleClearAll = () => {
    handleClearInputs();
    setCustomerCode('');
    setMemberCode('');
    setCustomerName('');
    setAddress1('');
    setAddress2('');
    setAddress3('');
    setRemarks('');
    setItems([]);
    setCustomerType('Customer Type');
    fetchInitialData();
    showToast('Form reset to new transaction.');
  };

  // Totals
  const totalWeightSum = items.reduce((sum, item) => sum + (parseFloat(item.weight) || 0), 0);
  const totalCostSum = items.reduce((sum, item) => sum + (parseFloat(item.totalCost) || 0), 0);
  const tradeInAmount = totalCostSum;
  const balanceAmount = (parseFloat(salesAmount) || 0) - tradeInAmount;

  // Save / Update Operation
  const handleSaveTransaction = async () => {
    if (!customerName.trim() && !customerCode.trim() && !memberCode.trim()) {
      showToast('Please select or enter Customer / Member.', 'error');
      return;
    }
    if (items.length === 0) {
      showToast('Please add at least one trade-in item in the grid.', 'error');
      return;
    }

    try {
      setSaving(true);
      const payload = {
        invoiceNo,
        customerCode,
        memberCode,
        customerName,
        address1,
        address2,
        address3,
        remarks,
        salesAmount: parseFloat(salesAmount) || 0,
        items,
      };

      const res = await fetch(`${API_URL}/save`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload),
      });

      const data = await res.json();
      if (data.success) {
        showToast(data.message || 'Transaction saved successfully!', 'success');
        setInvoiceNo(data.invoiceNo || invoiceNo);

        // If called from Installment & Order Entry, Installment Payment, Order Payment, Repair Entry, or Repair Payment, return back with tradeInAmount
        const calledFrom = (searchParams?.calledFrom || searchParams?.sCalledFrom) as string;
        if (calledFrom === 'ORDERFORM') {
          setTimeout(() => {
            router.push({
              pathname: '/installmentorderentry',
              params: {
                tradeInAmount: tradeInAmount.toFixed(2),
                invoiceNo: invoiceNo,
              },
            });
          }, 800);
        } else if (calledFrom === 'INSTALLMENTPAYMENT') {
          setTimeout(() => {
            router.push({
              pathname: '/installmentpayment',
              params: {
                tradeInAmount: tradeInAmount.toFixed(2),
                agreementNo: invoiceNo,
                invoiceNo: invoiceNo,
              },
            });
          }, 800);
        } else if (calledFrom === 'ORDERPAYMENT') {
          setTimeout(() => {
            router.push({
              pathname: '/orderpayment',
              params: {
                tradeInAmount: tradeInAmount.toFixed(2),
                orderNo: invoiceNo,
                invoiceNo: invoiceNo,
              },
            });
          }, 800);
        } else if (calledFrom === 'REPAIRENTRY') {
          setTimeout(() => {
            router.push({
              pathname: '/repairentry',
              params: {
                tradeInAmount: tradeInAmount.toFixed(2),
                repairNo: invoiceNo,
                invoiceNo: invoiceNo,
              },
            });
          }, 800);
        } else if (calledFrom === 'REPAIRPAYMENT') {
          setTimeout(() => {
            router.push({
              pathname: '/repairpayment',
              params: {
                tradeInAmount: tradeInAmount.toFixed(2),
                repairNo: invoiceNo,
                invoiceNo: invoiceNo,
              },
            });
          }, 800);
        }
      } else {
        showToast(data.message || 'Failed to save transaction', 'error');
      }
    } catch (err: any) {
      console.error('Save error:', err);
      showToast('Failed to save transaction', 'error');
    } finally {
      setSaving(false);
    }
  };

  // Filtered LOV lists
  const filteredCustomers = lovData.filter((c) => {
    const q = lovSearch.toLowerCase();
    return (
      (c.Code && c.Code.toLowerCase().includes(q)) ||
      (c.Name && c.Name.toLowerCase().includes(q)) ||
      (c.Address1 && c.Address1.toLowerCase().includes(q))
    );
  });

  const filteredHistory = historyData.filter((h) => {
    const q = historySearch.toLowerCase();
    return (
      (h.InvoiceNo && h.InvoiceNo.toLowerCase().includes(q)) ||
      (h.CustomerName && h.CustomerName.toLowerCase().includes(q)) ||
      (h.InventoryCode && h.InventoryCode.toLowerCase().includes(q))
    );
  });

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
            <ThemedText style={styles.headerTitle}>TRADE IN GOLD</ThemedText>
            <ThemedText style={styles.headerSubtitle}>
              Invoice: #{invoiceNo} • SmartGold System
            </ThemedText>
          </View>
        </View>

        <View style={styles.headerRight}>
          <TouchableOpacity
            style={styles.headerActionBtn}
            onPress={openHistoryLOV}
            activeOpacity={0.7}
          >
            <Ionicons name="search-outline" size={14} color="#5C4A28" />
            <ThemedText style={styles.headerActionBtnText}>Search</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity
            style={styles.headerActionBtn}
            onPress={handleClearAll}
            activeOpacity={0.7}
          >
            <Ionicons name="refresh-outline" size={14} color="#5C4A28" />
            <ThemedText style={styles.headerActionBtnText}>New</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity
            style={[styles.headerActionBtn, styles.headerSaveBtn]}
            onPress={handleSaveTransaction}
            disabled={saving}
            activeOpacity={0.7}
          >
            {saving ? (
              <ActivityIndicator size="small" color="#9E6D08" />
            ) : (
              <>
                <Ionicons name="checkmark-done" size={16} color="#9E6D08" />
                <ThemedText style={styles.headerSaveBtnText}>Update (F7)</ThemedText>
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

      {/* Main Body */}
      <ScrollView
        contentContainerStyle={styles.container}
        keyboardShouldPersistTaps="handled"
      >
        {/* TOP SECTION: Invoice & Customer Card (Left) + Premium Financial Cards (Right) */}
        <View style={styles.topSection}>
          {/* Customer Card */}
          <View style={styles.customerCard}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerTitleRow}>
                <View style={styles.headerIconBadge}>
                  <Ionicons name="receipt-outline" size={14} color="#8D6409" />
                </View>
                <ThemedText style={styles.cardHeaderTitle}>INVOICE & CUSTOMER DETAILS</ThemedText>
              </View>

              <TouchableOpacity
                style={styles.customerTypeBadge}
                onPress={openCustomerLOV}
                activeOpacity={0.8}
              >
                <Ionicons name="people-outline" size={12} color="#9E6D08" style={{ marginRight: 4 }} />
                <ThemedText style={styles.customerTypeText}>{customerType}</ThemedText>
                <Ionicons name="search" size={11} color="#9E6D08" style={{ marginLeft: 4 }} />
              </TouchableOpacity>
            </View>

            <View style={styles.cardBody}>
              {/* Row 1: Invoice No, Cust Code, Member Code, Customer Name */}
              <View style={[styles.formRow, { gap: 10, flexWrap: 'wrap' }]}>
                <View style={styles.fieldGroup}>
                  <ThemedText style={styles.fieldLabel}>Invoice No.</ThemedText>
                  <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                    <TextInput
                      style={[styles.input, styles.readonlyInput, { width: 100 }]}
                      value={invoiceNo}
                      editable={false}
                    />
                    <TouchableOpacity
                      style={styles.btnSearchDots}
                      onPress={openHistoryLOV}
                      activeOpacity={0.7}
                    >
                      <Ionicons name="search" size={13} color="#554428" />
                    </TouchableOpacity>
                  </View>
                </View>

                <View style={styles.fieldGroup}>
                  <ThemedText style={styles.fieldLabel}>Customer</ThemedText>
                  <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                    <TextInput
                      style={[styles.input, { width: 95 }]}
                      value={customerCode}
                      onChangeText={setCustomerCode}
                      placeholder="Cust Code"
                      placeholderTextColor="#A89E8D"
                    />
                    <TouchableOpacity
                      style={styles.btnSearchDots}
                      onPress={openCustomerLOV}
                      activeOpacity={0.7}
                    >
                      <Ionicons name="search" size={13} color="#554428" />
                    </TouchableOpacity>
                  </View>
                </View>

                <View style={styles.fieldGroup}>
                  <ThemedText style={styles.fieldLabel}>Member Code</ThemedText>
                  <TextInput
                    style={[styles.input, { width: 110 }]}
                    value={memberCode}
                    onChangeText={setMemberCode}
                    placeholder="Member Code"
                    placeholderTextColor="#A89E8D"
                  />
                </View>

                <View style={[styles.fieldGroup, { flex: 1, minWidth: 160 }]}>
                  <ThemedText style={styles.fieldLabel}>Customer Name</ThemedText>
                  <TextInput
                    style={[styles.input, { width: '100%' }]}
                    value={customerName}
                    onChangeText={setCustomerName}
                    placeholder="Enter customer name..."
                    placeholderTextColor="#A89E8D"
                  />
                </View>
              </View>

              {/* Address Lines (Row 2 & 3) */}
              <View style={[styles.formRow, { marginTop: 8, gap: 8 }]}>
                <ThemedText style={styles.rowLabel}>Address</ThemedText>
                <TextInput
                  style={[styles.input, { flex: 1 }]}
                  value={address1}
                  onChangeText={setAddress1}
                  placeholder="Street / Unit (Line 1)"
                  placeholderTextColor="#A89E8D"
                />
                <TextInput
                  style={[styles.input, { flex: 1 }]}
                  value={address2}
                  onChangeText={setAddress2}
                  placeholder="Area / City (Line 2)"
                  placeholderTextColor="#A89E8D"
                />
              </View>

              <View style={[styles.formRow, { marginTop: 6, gap: 8 }]}>
                <ThemedText style={styles.rowLabel} />
                <TextInput
                  style={[styles.input, { flex: 1 }]}
                  value={address3}
                  onChangeText={setAddress3}
                  placeholder="Postal Code / Country (Line 3)"
                  placeholderTextColor="#A89E8D"
                />
              </View>

              {/* Remarks */}
              <View style={[styles.formRow, { marginTop: 6, gap: 8 }]}>
                <ThemedText style={styles.rowLabel}>Remarks</ThemedText>
                <TextInput
                  style={[styles.input, { flex: 1 }]}
                  value={remarks}
                  onChangeText={setRemarks}
                  placeholder="Transaction notes..."
                  placeholderTextColor="#A89E8D"
                />
              </View>
            </View>
          </View>

          {/* Right: Premium Financial Status Cards */}
          <View style={styles.rightFinancials}>
            {/* Sales Amount */}
            <View style={styles.financialCard}>
              <View style={styles.financialCardHeader}>
                <Ionicons name="cart-outline" size={13} color="#8D6409" />
                <ThemedText style={styles.financialCardTitle}>Sales Amount</ThemedText>
              </View>
              <View style={styles.financialValueBox}>
                <ThemedText style={styles.currencyPrefix}>$</ThemedText>
                <TextInput
                  style={styles.financialInput}
                  value={salesAmount}
                  keyboardType="numeric"
                  onChangeText={setSalesAmount}
                />
              </View>
            </View>

            {/* Trade In Amount */}
            <View style={[styles.financialCard, styles.tradeInCard]}>
              <View style={styles.financialCardHeader}>
                <Ionicons name="repeat-outline" size={13} color="#8D6409" />
                <ThemedText style={styles.financialCardTitle}>Trade In Amount</ThemedText>
              </View>
              <View style={styles.financialValueBox}>
                <ThemedText style={styles.currencyPrefix}>$</ThemedText>
                <ThemedText style={styles.financialValueText}>
                  {tradeInAmount.toFixed(2)}
                </ThemedText>
              </View>
            </View>

            {/* Balance Amount */}
            <View style={styles.financialCard}>
              <View style={styles.financialCardHeader}>
                <Ionicons name="wallet-outline" size={13} color="#8D6409" />
                <ThemedText style={styles.financialCardTitle}>Balance Amount</ThemedText>
              </View>
              <View style={styles.financialValueBox}>
                <ThemedText style={styles.currencyPrefix}>$</ThemedText>
                <ThemedText
                  style={[
                    styles.financialValueText,
                    balanceAmount < 0 ? { color: '#C62828' } : { color: '#2E7D32' },
                  ]}
                >
                  {balanceAmount.toFixed(2)}
                </ThemedText>
              </View>
            </View>

            {/* Trade In Limit */}
            <View style={styles.financialCard}>
              <View style={styles.financialCardHeader}>
                <Ionicons name="shield-outline" size={13} color="#8D6409" />
                <ThemedText style={styles.financialCardTitle}>Trade In Limit</ThemedText>
              </View>
              <View style={styles.financialValueBox}>
                <ThemedText style={styles.currencyPrefix}>$</ThemedText>
                <ThemedText style={[styles.financialValueText, { color: '#554428' }]}>
                  {tradeInLimit}
                </ThemedText>
              </View>
            </View>
          </View>
        </View>

        {/* MIDDLE SECTION: Elegant Modern Data Grid */}
        <View style={styles.gridContainer}>
          <View style={styles.gridHeader}>
            <ThemedText style={[styles.gridHeaderText, { width: 95 }]}>InventoryCode</ThemedText>
            <ThemedText style={[styles.gridHeaderText, { flex: 2.5 }]}>Description</ThemedText>
            <ThemedText style={[styles.gridHeaderText, { width: 80, textAlign: 'right' }]}>Weight (g)</ThemedText>
            <ThemedText style={[styles.gridHeaderText, { width: 80, textAlign: 'right' }]}>Cost / Gm</ThemedText>
            <ThemedText style={[styles.gridHeaderText, { width: 95, textAlign: 'right' }]}>Total Cost</ThemedText>
            <ThemedText style={[styles.gridHeaderText, { flex: 1.5 }]}>Stone Description</ThemedText>
            <ThemedText style={[styles.gridHeaderText, { width: 80, textAlign: 'right' }]}>Stone Wt</ThemedText>
            <ThemedText style={[styles.gridHeaderText, { width: 65, textAlign: 'center' }]}>Class</ThemedText>
          </View>

          {/* Grid Body */}
          <View style={styles.gridBody}>
            {items.length === 0 ? (
              <View style={styles.gridEmptyArea}>
                <Ionicons name="basket-outline" size={30} color="#C8BEAC" />
                <ThemedText style={styles.emptyGridText}>No trade-in items entered yet</ThemedText>
                <ThemedText style={styles.emptyGridSubText}>
                  Enter item details below and tap "Add (+)"
                </ThemedText>
              </View>
            ) : (
              items.map((item, index) => {
                const isSelected = selectedIndex === index;
                return (
                  <TouchableOpacity
                    key={item.id || index}
                    style={[
                      styles.gridRow,
                      index % 2 === 1 && styles.gridRowAlt,
                      isSelected && styles.gridRowSelected,
                    ]}
                    activeOpacity={0.7}
                    onPress={() => handleSelectGridRow(index)}
                  >
                    <View style={{ width: 95, flexDirection: 'row', alignItems: 'center', gap: 6 }}>
                      {isSelected && <View style={styles.selectedIndicatorDot} />}
                      <ThemedText style={[styles.gridCellText, { fontWeight: 'bold' }]}>
                        {item.inventoryCode}
                      </ThemedText>
                    </View>
                    <ThemedText style={[styles.gridCellText, { flex: 2.5 }]} numberOfLines={1}>
                      {item.description}
                    </ThemedText>
                    <ThemedText style={[styles.gridCellText, { width: 80, textAlign: 'right' }]}>
                      {item.weight}
                    </ThemedText>
                    <ThemedText style={[styles.gridCellText, { width: 80, textAlign: 'right' }]}>
                      {item.costPerGram}
                    </ThemedText>
                    <ThemedText
                      style={[styles.gridCellText, styles.gridCellAmount, { width: 95, textAlign: 'right' }]}
                    >
                      ${item.totalCost}
                    </ThemedText>
                    <ThemedText style={[styles.gridCellText, { flex: 1.5 }]} numberOfLines={1}>
                      {item.stoneDescription || '-'}
                    </ThemedText>
                    <ThemedText style={[styles.gridCellText, { width: 80, textAlign: 'right' }]}>
                      {item.stoneWeight || '0.000'}
                    </ThemedText>
                    <View style={{ width: 65, alignItems: 'center' }}>
                      <View style={styles.classPill}>
                        <ThemedText style={styles.classPillText}>{item.itemClass}</ThemedText>
                      </View>
                    </View>
                  </TouchableOpacity>
                );
              })
            )}
          </View>

          {/* Grid Total Footer */}
          {items.length > 0 && (
            <View style={styles.gridFooterRow}>
              <ThemedText style={[styles.gridFooterText, { width: 95, color: '#9E6D08' }]}>
                TOTAL
              </ThemedText>
              <ThemedText style={[styles.gridFooterText, { flex: 2.5 }]} />
              <ThemedText style={[styles.gridFooterText, { width: 80, textAlign: 'right', color: '#9E6D08' }]}>
                {totalWeightSum.toFixed(3)}g
              </ThemedText>
              <ThemedText style={[styles.gridFooterText, { width: 80 }]} />
              <ThemedText style={[styles.gridFooterText, { width: 95, textAlign: 'right', color: '#9E6D08', fontSize: 13 }]}>
                ${totalCostSum.toFixed(2)}
              </ThemedText>
              <ThemedText style={[styles.gridFooterText, { flex: 1.5 }]} />
              <ThemedText style={[styles.gridFooterText, { width: 80 }]} />
              <ThemedText style={[styles.gridFooterText, { width: 65 }]} />
            </View>
          )}
        </View>

        {/* LOWER SECTION: Entry Section & Input Boxes (Directly below Grid) */}
        <View style={styles.detailEntrySection}>
          <View style={styles.detailEntryBanner}>
            <View style={{ flexDirection: 'row', alignItems: 'center', gap: 6 }}>
              <Ionicons name="create-outline" size={14} color="#8D6409" />
              <ThemedText style={styles.detailEntryBannerTitle}>
                {selectedIndex !== null ? `EDIT ITEM #${selectedIndex + 1}` : 'ADD TRADE-IN ITEM'}
              </ThemedText>
            </View>
            {selectedIndex !== null && (
              <ThemedText style={styles.selectedItemPill}>Selected for Editing</ThemedText>
            )}
          </View>

          {/* Labels Row */}
          <View style={styles.entryHeaderLabelsRow}>
            <ThemedText style={[styles.entryColLabel, { flex: 3.5 }]}>Description</ThemedText>
            <ThemedText style={[styles.entryColLabel, { flex: 1.2, textAlign: 'right' }]}>Weight</ThemedText>
            <ThemedText style={[styles.entryColLabel, { flex: 1.2, textAlign: 'right' }]}>Percentage</ThemedText>
            <ThemedText style={[styles.entryColLabel, { flex: 1.2, textAlign: 'right' }]}>Cost / Gram</ThemedText>
            <ThemedText style={[styles.entryColLabel, { flex: 1.4, textAlign: 'right' }]}>Total Cost</ThemedText>
            <View style={{ width: 180 }} />
          </View>

          {/* Inputs Row 1: Description, Weight, Percentage, Cost/Gram, Total Cost + Action Buttons */}
          <View style={styles.inputsRow}>
            <TextInput
              style={[styles.inputBox, { flex: 3.5 }]}
              value={description}
              onChangeText={setDescription}
              placeholder="Description"
              placeholderTextColor="#A89E8D"
            />
            <TextInput
              style={[styles.inputBox, styles.numericInput, { flex: 1.2 }]}
              value={weight}
              keyboardType="numeric"
              placeholder="0.000"
              placeholderTextColor="#A89E8D"
              onChangeText={handleWeightChange}
            />
            <TextInput
              style={[styles.inputBox, styles.numericInput, { flex: 1.2 }]}
              value={percentage}
              keyboardType="numeric"
              placeholder="0"
              placeholderTextColor="#A89E8D"
              onChangeText={setPercentage}
            />
            <TextInput
              style={[styles.inputBox, styles.numericInput, { flex: 1.2 }]}
              value={costPerGram}
              keyboardType="numeric"
              placeholder="0.00"
              placeholderTextColor="#A89E8D"
              onChangeText={handleCostPerGramChange}
            />
            <TextInput
              style={[styles.inputBox, styles.numericInput, styles.totalCostInput, { flex: 1.4 }]}
              value={totalCost}
              keyboardType="numeric"
              placeholder="0.00"
              placeholderTextColor="#A89E8D"
              onChangeText={handleTotalCostChange}
            />

            {/* Add & Delete Buttons */}
            <View style={styles.rowActionsRight}>
              <TouchableOpacity
                style={[styles.itemActionBtn, styles.btnAddItem]}
                onPress={handleAddItem}
                activeOpacity={0.7}
              >
                <Ionicons
                  name={selectedIndex !== null ? 'save-outline' : 'add'}
                  size={14}
                  color="#9E6D08"
                />
                <ThemedText style={styles.btnAddItemText}>
                  {selectedIndex !== null ? 'Update (+)' : 'Add (+)'}
                </ThemedText>
              </TouchableOpacity>

              <TouchableOpacity
                style={[styles.itemActionBtn, styles.btnDeleteItem]}
                onPress={handleDeleteItem}
                activeOpacity={0.7}
              >
                <Ionicons name="trash-outline" size={13} color="#C62828" />
                <ThemedText style={styles.btnDeleteItemText}>Delete</ThemedText>
              </TouchableOpacity>
            </View>
          </View>

          {/* Inputs Row 2: Stone Description, Stone Weight + Clear Button */}
          <View style={[styles.inputsRow, { marginTop: 6, alignItems: 'center' }]}>
            <TextInput
              style={[styles.inputBox, { flex: 3.5 }]}
              value={stoneDescription}
              onChangeText={setStoneDescription}
              placeholder="Stone / Diamond Description"
              placeholderTextColor="#A89E8D"
            />
            <TextInput
              style={[styles.inputBox, styles.numericInput, { flex: 1.2 }]}
              value={stoneWeight}
              keyboardType="numeric"
              placeholder="Stone Wt"
              placeholderTextColor="#A89E8D"
              onChangeText={setStoneWeight}
            />
            <View style={{ flex: 3.8 }} />

            <View style={styles.rowActionsRight}>
              <TouchableOpacity
                style={[styles.itemActionBtn, styles.btnClearItem]}
                onPress={handleClearInputs}
                activeOpacity={0.7}
              >
                <Ionicons name="refresh-outline" size={13} color="#5C4A28" />
                <ThemedText style={styles.btnClearItemText}>Clear</ThemedText>
              </TouchableOpacity>
            </View>
          </View>
        </View>

        {/* BOTTOM SECTION: Class Radio Selection & Bottom Action Buttons */}
        <View style={styles.bottomSection}>
          {/* Class Radio Group */}
          <View style={styles.classCard}>
            <View style={styles.classCardHeader}>
              <Ionicons name="diamond-outline" size={13} color="#8D6409" />
              <ThemedText style={styles.classCardTitle}>PURITY / CLASS</ThemedText>
            </View>
            <View style={styles.radioRow}>
              {CLASS_OPTIONS.map((cls) => {
                const isChecked = selectedClass === cls;
                return (
                  <TouchableOpacity
                    key={cls}
                    style={styles.radioOption}
                    onPress={() => setSelectedClass(cls)}
                    activeOpacity={0.7}
                  >
                    <View style={[styles.radioCircle, isChecked && styles.radioCircleChecked]}>
                      {isChecked && <View style={styles.radioInnerDot} />}
                    </View>
                    <ThemedText style={[styles.radioLabel, isChecked && styles.radioLabelChecked]}>
                      {cls}
                    </ThemedText>
                  </TouchableOpacity>
                );
              })}
            </View>
          </View>

          {/* Main Action Buttons */}

        </View>
      </ScrollView>

      {/* Customer LOV Modal */}
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
            <View style={styles.lookupCard}>
              <View style={styles.modalHeaderBanner}>
                <View style={styles.headerTitleRow}>
                  <View style={styles.headerIconBadge}>
                    <Ionicons name="search-outline" size={14} color="#8D6409" />
                  </View>
                  <ThemedText style={styles.modalHeaderTitle}>SELECT CUSTOMER / MEMBER</ThemedText>
                </View>
                <TouchableOpacity onPress={() => setLovVisible(false)} activeOpacity={0.7}>
                  <Ionicons name="close" size={18} color="#554428" />
                </TouchableOpacity>
              </View>

              <View style={styles.lookupSearchRow}>
                <Ionicons name="search" size={14} color="#8D6409" style={{ marginRight: 6 }} />
                <TextInput
                  style={styles.lookupSearchInput}
                  placeholder="Filter by name, code or address..."
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
                    Loading customers...
                  </ThemedText>
                </View>
              ) : (
                <View style={{ maxHeight: 340, minHeight: 140 }}>
                  <FlatList
                    data={filteredCustomers}
                    keyExtractor={(item, index) => `${item.Code}-${index}`}
                    renderItem={({ item, index }) => (
                      <TouchableOpacity
                        style={[
                          styles.lovItem,
                          index % 2 === 1 && { backgroundColor: '#FCFAF6' },
                        ]}
                        activeOpacity={0.7}
                        onPress={() => handleSelectCustomer(item)}
                      >
                        <View style={{ flexDirection: 'row', alignItems: 'center', justifyContent: 'space-between' }}>
                          <ThemedText style={styles.lovItemCode}>{item.Code}</ThemedText>
                          <ThemedText style={styles.lovItemType}>{item.CustomerType}</ThemedText>
                        </View>
                        <ThemedText style={styles.lovItemName} numberOfLines={1}>
                          {item.Name}
                        </ThemedText>
                        {(item.Address1 || item.Address2) ? (
                          <ThemedText style={styles.lovItemSub} numberOfLines={1}>
                            {[item.Address1, item.Address2, item.Address3].filter(Boolean).join(', ')}
                          </ThemedText>
                        ) : null}
                      </TouchableOpacity>
                    )}
                    ListEmptyComponent={() => (
                      <ThemedText style={styles.emptyText}>No matching customers found.</ThemedText>
                    )}
                  />
                </View>
              )}
            </View>
          </TouchableWithoutFeedback>
        </TouchableOpacity>
      </Modal>

      {/* Transaction Search / History LOV Modal */}
      <Modal
        visible={historyVisible}
        transparent
        animationType="fade"
        onRequestClose={() => setHistoryVisible(false)}
      >
        <TouchableOpacity
          style={styles.modalOverlay}
          activeOpacity={1}
          onPress={() => setHistoryVisible(false)}
        >
          <TouchableWithoutFeedback>
            <View style={styles.lookupCard}>
              <View style={styles.modalHeaderBanner}>
                <View style={styles.headerTitleRow}>
                  <View style={styles.headerIconBadge}>
                    <Ionicons name="search-outline" size={14} color="#8D6409" />
                  </View>
                  <ThemedText style={styles.modalHeaderTitle}>SELECT TRADE-IN TRANSACTION</ThemedText>
                </View>
                <TouchableOpacity onPress={() => setHistoryVisible(false)} activeOpacity={0.7}>
                  <Ionicons name="close" size={18} color="#554428" />
                </TouchableOpacity>
              </View>

              <View style={styles.lookupSearchRow}>
                <Ionicons name="search" size={14} color="#8D6409" style={{ marginRight: 6 }} />
                <TextInput
                  style={styles.lookupSearchInput}
                  placeholder="Search invoice number, code or customer..."
                  placeholderTextColor="#A89E8D"
                  value={historySearch}
                  onChangeText={setHistorySearch}
                  autoFocus
                />
              </View>

              {historyLoading ? (
                <View style={styles.modalLoading}>
                  <ActivityIndicator size="large" color="#B8903B" />
                  <ThemedText style={{ marginTop: 8, color: '#6E5528', fontSize: 12 }}>
                    Loading transactions...
                  </ThemedText>
                </View>
              ) : (
                <View style={{ maxHeight: 340, minHeight: 140 }}>
                  <FlatList
                    data={filteredHistory}
                    keyExtractor={(item, index) => `${item.InventoryCode}-${index}`}
                    renderItem={({ item, index }) => (
                      <TouchableOpacity
                        style={[
                          styles.lovItem,
                          index % 2 === 1 && { backgroundColor: '#FCFAF6' },
                        ]}
                        activeOpacity={0.7}
                        onPress={() => handleSelectTransaction(item)}
                      >
                        <View style={{ flexDirection: 'row', alignItems: 'center', justifyContent: 'space-between' }}>
                          <ThemedText style={styles.lovItemCode}>
                            Invoice #{item.InvoiceNo || item.InventoryCode}
                          </ThemedText>
                          <ThemedText style={styles.lovItemType}>
                            ${(parseFloat(item.TotalCost) || 0).toFixed(2)}
                          </ThemedText>
                        </View>
                        <ThemedText style={styles.lovItemName} numberOfLines={1}>
                          {item.CustomerName || 'Walk-in Customer'}
                        </ThemedText>
                        <ThemedText style={styles.lovItemSub}>
                          Weight: {(parseFloat(item.Weight) || 0).toFixed(3)}g | Date: {item.Date ? new Date(item.Date).toLocaleDateString() : 'N/A'}
                        </ThemedText>
                      </TouchableOpacity>
                    )}
                    ListEmptyComponent={() => (
                      <ThemedText style={styles.emptyText}>No transactions found.</ThemedText>
                    )}
                  />
                </View>
              )}
            </View>
          </TouchableWithoutFeedback>
        </TouchableOpacity>
      </Modal>

      {/* Super Admin Authorization Modal */}
      <Modal
        visible={adminModalVisible}
        transparent
        animationType="fade"
        onRequestClose={() => setAdminModalVisible(false)}
      >
        <TouchableOpacity style={styles.modalOverlay} activeOpacity={1}>
          <TouchableWithoutFeedback>
            <View style={[styles.lookupCard, { maxWidth: 360 }]}>
              <View style={styles.modalHeaderBanner}>
                <View style={styles.headerTitleRow}>
                  <View style={styles.headerIconBadge}>
                    <Ionicons name="key-outline" size={14} color="#8D6409" />
                  </View>
                  <ThemedText style={styles.modalHeaderTitle}>SUPER ADMIN AUTHORIZATION</ThemedText>
                </View>
              </View>

              <View style={{ padding: 16 }}>
                <ThemedText style={{ fontSize: 11, color: '#554428', marginBottom: 12, lineHeight: 16 }}>
                  Cost Per Gram is higher than market benchmark ({marketBenchmarkRate}). Enter Super Admin password to authorize:
                </ThemedText>

                <TextInput
                  style={[styles.input, { width: '100%', marginBottom: 14, height: 34 }]}
                  placeholder="Enter admin password..."
                  placeholderTextColor="#A89E8D"
                  secureTextEntry
                  value={adminPassword}
                  onChangeText={setAdminPassword}
                  autoFocus
                  onSubmitEditing={handleVerifyAdminPassword}
                />

                <View style={{ flexDirection: 'row', gap: 10 }}>
                  <TouchableOpacity
                    style={[styles.modalActionBtn, styles.modalBtnCancel]}
                    onPress={() => {
                      setAdminModalVisible(false);
                      setAdminPassword('');
                      setAdminPendingItem(null);
                    }}
                    activeOpacity={0.7}
                  >
                    <ThemedText style={styles.modalBtnCancelText}>Cancel</ThemedText>
                  </TouchableOpacity>

                  <TouchableOpacity
                    style={[styles.modalActionBtn, styles.modalBtnPrimary]}
                    onPress={handleVerifyAdminPassword}
                    activeOpacity={0.7}
                  >
                    <ThemedText style={styles.modalBtnPrimaryText}>Authorize</ThemedText>
                  </TouchableOpacity>
                </View>
              </View>
            </View>
          </TouchableWithoutFeedback>
        </TouchableOpacity>
      </Modal>
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  safeArea: {
    flex: 1,
    backgroundColor: '#FBF9F5',
  },
  container: {
    padding: 10,
    paddingBottom: 30,
  },

  // ===== TOP NAV HEADER =====
  topNavHeader: {
    height: 48,
    backgroundColor: '#FFFFFF',
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingHorizontal: 12,
    borderBottomWidth: 1,
    borderBottomColor: '#EBE2D2',
    elevation: 2,
    shadowColor: '#A38035',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.08,
    shadowRadius: 3,
  },
  headerLeft: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 10,
  },
  backBtn: {
    padding: 6,
    borderRadius: 6,
    backgroundColor: '#FAF5EA',
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
    fontSize: 10,
    color: '#8D6409',
    fontWeight: '600',
  },
  headerRight: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 8,
  },
  headerActionBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 10,
    paddingVertical: 5,
    borderRadius: 6,
    backgroundColor: '#FAF5EA',
    borderWidth: 1,
    borderColor: '#DFD5C3',
    gap: 4,
  },
  headerActionBtnText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#5C4A28',
  },
  headerSaveBtn: {
    backgroundColor: '#F9F2E2',
    borderColor: '#B8903B',
  },
  headerSaveBtnText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#9E6D08',
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

  // ===== TOP SECTION (Customer Card + Financial Totals) =====
  topSection: {
    flexDirection: 'row',
    gap: 10,
    marginBottom: 10,
  },
  customerCard: {
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
    gap: 6,
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
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3B2D17',
    letterSpacing: 0.4,
  },
  customerTypeBadge: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFF8EA',
    borderWidth: 1,
    borderColor: '#D8C59A',
    borderRadius: 6,
    paddingHorizontal: 8,
    paddingVertical: 3,
  },
  customerTypeText: {
    fontSize: 10,
    fontWeight: 'bold',
    color: '#9E6D08',
  },
  cardBody: {
    padding: 10,
  },
  formRow: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  fieldGroup: {
    flexDirection: 'column',
  },
  fieldLabel: {
    fontSize: 10,
    fontWeight: '600',
    color: '#6E5528',
    marginBottom: 3,
  },
  rowLabel: {
    fontSize: 11,
    fontWeight: '600',
    color: '#554428',
    width: 65,
  },
  input: {
    height: 28,
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#D8CDBA',
    borderRadius: 5,
    paddingHorizontal: 8,
    paddingVertical: 0,
    fontSize: 11,
    color: '#3B2D17',
    fontWeight: '500',
  },
  readonlyInput: {
    backgroundColor: '#FAF7F0',
    borderColor: '#E8DEC8',
    color: '#554428',
    fontWeight: 'bold',
  },
  btnSearchDots: {
    width: 28,
    height: 28,
    backgroundColor: '#F5EBD4',
    borderWidth: 1,
    borderLeftWidth: 0,
    borderColor: '#D8CDBA',
    borderTopRightRadius: 5,
    borderBottomRightRadius: 5,
    alignItems: 'center',
    justifyContent: 'center',
  },

  // ===== RIGHT FINANCIALS =====
  rightFinancials: {
    width: 175,
    gap: 6,
  },
  financialCard: {
    backgroundColor: '#FFFFFF',
    borderRadius: 7,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    shadowColor: '#A38035',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.05,
    shadowRadius: 3,
    elevation: 2,
    overflow: 'hidden',
  },
  tradeInCard: {
    borderColor: '#B8903B',
    borderWidth: 1.5,
  },
  financialCardHeader: {
    backgroundColor: '#F5E7CA',
    paddingHorizontal: 8,
    paddingVertical: 4,
    flexDirection: 'row',
    alignItems: 'center',
    gap: 5,
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
  },
  financialCardTitle: {
    fontSize: 10,
    fontWeight: 'bold',
    color: '#3B2D17',
    letterSpacing: 0.3,
  },
  financialValueBox: {
    backgroundColor: '#FFFDF9',
    paddingHorizontal: 8,
    paddingVertical: 4,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'flex-end',
    minHeight: 28,
  },
  currencyPrefix: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#8D6409',
    marginRight: 4,
  },
  financialValueText: {
    fontSize: 14,
    fontWeight: 'bold',
    color: '#9E6D08',
  },
  financialInput: {
    fontSize: 14,
    fontWeight: 'bold',
    color: '#3B2D17',
    textAlign: 'right',
    padding: 0,
    minWidth: 70,
  },

  // ===== MIDDLE SECTION: DATA GRID =====
  gridContainer: {
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    overflow: 'hidden',
    shadowColor: '#A38035',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.06,
    shadowRadius: 3,
    elevation: 2,
    marginBottom: 10,
  },
  gridHeader: {
    backgroundColor: '#F5E7CA',
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 10,
    paddingVertical: 7,
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
  },
  gridHeaderText: {
    fontSize: 10,
    fontWeight: 'bold',
    color: '#554428',
    letterSpacing: 0.3,
  },
  gridBody: {
    minHeight: 120,
    backgroundColor: '#FFFFFF',
  },
  gridEmptyArea: {
    paddingVertical: 30,
    alignItems: 'center',
    justifyContent: 'center',
    gap: 4,
  },
  emptyGridText: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#8D6409',
  },
  emptyGridSubText: {
    fontSize: 10,
    color: '#A89E8D',
  },
  gridRow: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 10,
    paddingVertical: 6,
    borderBottomWidth: 1,
    borderBottomColor: '#F5EFE3',
  },
  gridRowAlt: {
    backgroundColor: '#FCFAF6',
  },
  gridRowSelected: {
    backgroundColor: '#F9F2E2',
    borderBottomColor: '#E8DCB8',
  },
  selectedIndicatorDot: {
    width: 6,
    height: 6,
    borderRadius: 3,
    backgroundColor: '#9E6D08',
  },
  gridCellText: {
    fontSize: 11,
    color: '#3B2D17',
  },
  gridCellAmount: {
    fontWeight: 'bold',
    color: '#9E6D08',
  },
  classPill: {
    backgroundColor: '#FAF5EA',
    borderWidth: 1,
    borderColor: '#E2D2AF',
    borderRadius: 4,
    paddingHorizontal: 6,
    paddingVertical: 1,
  },
  classPillText: {
    fontSize: 10,
    fontWeight: 'bold',
    color: '#8D6409',
  },
  gridFooterRow: {
    backgroundColor: '#FAF5EA',
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 10,
    paddingVertical: 6,
    borderTopWidth: 1,
    borderTopColor: '#E8DCB8',
  },
  gridFooterText: {
    fontSize: 11,
    fontWeight: 'bold',
  },

  // ===== LOWER ENTRY SECTION =====
  detailEntrySection: {
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    overflow: 'hidden',
    shadowColor: '#A38035',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.06,
    shadowRadius: 3,
    elevation: 2,
    padding: 10,
    marginBottom: 10,
  },
  detailEntryBanner: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingBottom: 6,
    borderBottomWidth: 1,
    borderBottomColor: '#F5EFE3',
    marginBottom: 6,
  },
  detailEntryBannerTitle: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3B2D17',
    letterSpacing: 0.4,
  },
  selectedItemPill: {
    fontSize: 9,
    fontWeight: 'bold',
    color: '#9E6D08',
    backgroundColor: '#FFF8EA',
    paddingHorizontal: 6,
    paddingVertical: 2,
    borderRadius: 4,
    borderWidth: 1,
    borderColor: '#E2D2AF',
  },
  entryHeaderLabelsRow: {
    flexDirection: 'row',
    alignItems: 'center',
    marginBottom: 3,
    paddingHorizontal: 2,
  },
  entryColLabel: {
    fontSize: 10,
    fontWeight: '600',
    color: '#6E5528',
    paddingHorizontal: 2,
  },
  inputsRow: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 6,
  },
  inputBox: {
    height: 28,
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#D8CDBA',
    borderRadius: 5,
    paddingHorizontal: 8,
    paddingVertical: 0,
    fontSize: 11,
    color: '#3B2D17',
  },
  numericInput: {
    textAlign: 'right',
  },
  totalCostInput: {
    fontWeight: 'bold',
    color: '#9E6D08',
    backgroundColor: '#FFFDF9',
  },
  rowActionsRight: {
    width: 180,
    flexDirection: 'row',
    justifyContent: 'flex-end',
    gap: 6,
  },
  itemActionBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    height: 28,
    borderRadius: 5,
    paddingHorizontal: 10,
    borderWidth: 1,
    gap: 4,
  },
  btnAddItem: {
    backgroundColor: '#F9F2E2',
    borderColor: '#B8903B',
    minWidth: 80,
  },
  btnAddItemText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#9E6D08',
  },
  btnDeleteItem: {
    backgroundColor: '#FFEBEE',
    borderColor: '#FFCDD2',
    minWidth: 68,
  },
  btnDeleteItemText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#C62828',
  },
  btnClearItem: {
    backgroundColor: '#FAF5EA',
    borderColor: '#DFD5C3',
    minWidth: 68,
  },
  btnClearItemText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#5C4A28',
  },

  // ===== BOTTOM SECTION (Class Radio + Action Buttons) =====
  bottomSection: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    gap: 10,
  },
  classCard: {
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    padding: 8,
    paddingHorizontal: 12,
    flexDirection: 'row',
    alignItems: 'center',
    gap: 14,
    shadowColor: '#A38035',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.05,
    shadowRadius: 3,
    elevation: 2,
  },
  classCardHeader: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 4,
    borderRightWidth: 1,
    borderRightColor: '#F0E6D2',
    paddingRight: 10,
  },
  classCardTitle: {
    fontSize: 10,
    fontWeight: 'bold',
    color: '#8D6409',
    letterSpacing: 0.4,
  },
  radioRow: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 14,
  },
  radioOption: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 5,
  },
  radioCircle: {
    width: 14,
    height: 14,
    borderRadius: 7,
    borderWidth: 1.5,
    borderColor: '#D8CDBA',
    justifyContent: 'center',
    alignItems: 'center',
  },
  radioCircleChecked: {
    borderColor: '#9E6D08',
  },
  radioInnerDot: {
    width: 7,
    height: 7,
    borderRadius: 3.5,
    backgroundColor: '#9E6D08',
  },
  radioLabel: {
    fontSize: 11,
    fontWeight: '600',
    color: '#6E5528',
  },
  radioLabelChecked: {
    fontWeight: 'bold',
    color: '#9E6D08',
  },

  // Bottom Main Buttons
  bottomActionButtons: {
    flexDirection: 'row',
    gap: 10,
  },
  mainActionBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    height: 36,
    borderRadius: 6,
    paddingHorizontal: 16,
    borderWidth: 1,
    gap: 6,
    minWidth: 115,
  },
  btnUpdatePrimary: {
    backgroundColor: '#F9F2E2',
    borderColor: '#B8903B',
    elevation: 2,
  },
  btnUpdateText: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#9E6D08',
  },
  btnCloseSecondary: {
    backgroundColor: '#FAF5EA',
    borderColor: '#DFD5C3',
  },
  btnCloseText: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#5C4A28',
  },

  // ===== MODAL STYLES =====
  modalOverlay: {
    flex: 1,
    backgroundColor: 'rgba(30, 24, 15, 0.45)',
    justifyContent: 'center',
    alignItems: 'center',
    padding: 16,
  },
  lookupCard: {
    width: '100%',
    maxWidth: 480,
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    overflow: 'hidden',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.15,
    shadowRadius: 10,
    elevation: 8,
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
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3B2D17',
    letterSpacing: 0.4,
  },
  lookupSearchRow: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 10,
    paddingVertical: 6,
    backgroundColor: '#FFFDF9',
    borderBottomWidth: 1,
    borderBottomColor: '#F5EFE3',
  },
  lookupSearchInput: {
    flex: 1,
    height: 30,
    fontSize: 11,
    color: '#3B2D17',
    paddingHorizontal: 4,
  },
  modalLoading: {
    padding: 24,
    alignItems: 'center',
    justifyContent: 'center',
  },
  lovItem: {
    paddingHorizontal: 12,
    paddingVertical: 8,
    borderBottomWidth: 1,
    borderBottomColor: '#F5EFE3',
  },
  lovItemCode: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#9E6D08',
  },
  lovItemType: {
    fontSize: 10,
    fontWeight: '600',
    color: '#8D6409',
    backgroundColor: '#FAF5EA',
    paddingHorizontal: 6,
    paddingVertical: 1,
    borderRadius: 4,
    borderWidth: 1,
    borderColor: '#E2D2AF',
  },
  lovItemName: {
    fontSize: 12,
    fontWeight: '500',
    color: '#3B2D17',
    marginTop: 2,
  },
  lovItemSub: {
    fontSize: 10,
    color: '#8A7A64',
    marginTop: 2,
  },
  emptyText: {
    textAlign: 'center',
    color: '#8A7A64',
    padding: 20,
    fontSize: 11,
  },
  modalActionBtn: {
    flex: 1,
    height: 34,
    borderRadius: 6,
    alignItems: 'center',
    justifyContent: 'center',
    borderWidth: 1,
  },
  modalBtnCancel: {
    backgroundColor: '#FAF5EA',
    borderColor: '#DFD5C3',
  },
  modalBtnCancelText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#5C4A28',
  },
  modalBtnPrimary: {
    backgroundColor: '#F9F2E2',
    borderColor: '#B8903B',
  },
  modalBtnPrimaryText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#9E6D08',
  },
});
