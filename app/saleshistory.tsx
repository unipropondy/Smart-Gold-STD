import { ThemedText } from '@/components/themed-text';
import { API_BASE_URL } from '@/constants/api';
import { Ionicons } from '@expo/vector-icons';
import { Stack, useRouter } from 'expo-router';
import React, { useEffect, useState } from 'react';
import {
  ActivityIndicator,
  Alert,
  FlatList,
  Modal,
  ScrollView,
  StyleSheet,
  TextInput,
  TouchableOpacity,
  TouchableWithoutFeedback,
  View
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';

const API_URL = `${API_BASE_URL}/saleshistory`;

interface InventoryItem {
  InventoryCode: string;
  Description: string;
  Weight: number;
  StoneWeight: number;
  GST: number;
  NetAmount: number;
  TotalCost: number;
}

interface PaymentItem {
  PayMode: string;
  Amount: number;
  SlipNo: string;
}

export default function SalesHistoryScreen() {
  const router = useRouter();

  // ===== STATE =====
  const [loading, setLoading] = useState(false);
  const [toast, setToast] = useState<{ message: string; type: 'success' | 'error' } | null>(null);
  const [isEditing, setIsEditing] = useState(false);

  // Form Header fields
  const [invoiceNo, setInvoiceNo] = useState('');
  const [invoiceDate, setInvoiceDate] = useState('');
  const [locationCode, setLocationCode] = useState('HQ');
  const [salesType, setSalesType] = useState('CASH');
  const [customerCode, setCustomerCode] = useState('');
  const [customerName, setCustomerName] = useState('');
  const [salesManCode, setSalesManCode] = useState('');
  const [insAgreementNo, setInsAgreementNo] = useState('');
  const [remarks, setRemarks] = useState('');
  const [gst, setGst] = useState('0.00');
  const [tradeInAmt, setTradeInAmt] = useState('0.00');

  // Computed fields (summed up or readonly)
  const [totalWeight, setTotalWeight] = useState(0);
  const [totalStoneWeight, setTotalStoneWeight] = useState(0);
  const [totalPCS, setTotalPCS] = useState(0);
  const [netTotal, setNetTotal] = useState(0);

  // Grids details lists
  const [details, setDetails] = useState<InventoryItem[]>([]);
  const [payments, setPayments] = useState<PaymentItem[]>([]);

  // LOV / Search Modals
  const [lovType, setLovType] = useState<'invoice' | 'location' | 'customer' | 'salesman' | 'inventory' | 'paymode' | null>(null);
  const [lovSearch, setLovSearch] = useState('');
  const [lovData, setLovData] = useState<any[]>([]);
  const [filteredLovData, setFilteredLovData] = useState<any[]>([]);
  const [selectedPaymodeIndex, setSelectedPaymodeIndex] = useState<number | null>(null);

  // Default values
  const paymodesOptions = ['CASH', 'CREDIT', 'CARD', 'NET BANKING', 'CHEQUE', 'UNIONPAY', 'ALIPAY', 'WECHAT'];

  const showToast = (message: string, type: 'success' | 'error' = 'success') => {
    setToast({ message, type });
    setTimeout(() => {
      setToast(null);
    }, 3000);
  };

  // Set today's date in local YYYY-MM-DD
  const setTodayDate = () => {
    const d = new Date();
    const year = d.getFullYear();
    const month = String(d.getMonth() + 1).padStart(2, '0');
    const day = String(d.getDate()).padStart(2, '0');
    setInvoiceDate(`${year}-${month}-${day}`);
  };

  useEffect(() => {
    setTodayDate();
    fetchNextInvoiceNo();
  }, []);

  // recalculate details totals
  useEffect(() => {
    let weightSum = 0;
    let stoneWeightSum = 0;
    let netSum = 0;
    details.forEach(item => {
      weightSum += Number(item.Weight) || 0;
      stoneWeightSum += Number(item.StoneWeight) || 0;
      netSum += Number(item.NetAmount) || 0;
    });

    setTotalWeight(parseFloat(weightSum.toFixed(4)));
    setTotalStoneWeight(parseFloat(stoneWeightSum.toFixed(4)));
    setTotalPCS(details.length);
    setNetTotal(parseFloat(netSum.toFixed(2)));
  }, [details]);

  // Fetch Next Invoice number from backend
  const fetchNextInvoiceNo = async () => {
    try {
      const res = await fetch(`${API_URL}/nextNo?t=${Date.now()}`);
      const json = await res.json();
      if (json.success) {
        setInvoiceNo(json.nextNo);
        setIsEditing(false);
      }
    } catch (e) {
      console.error(e);
    }
  };

  // Load a transaction by Invoice No
  const loadInvoice = async (invNo: string) => {
    if (!invNo.trim()) return;
    setLoading(true);
    try {
      const res = await fetch(`${API_URL}/${encodeURIComponent(invNo)}`);
      const json = await res.json();
      if (json.success) {
        const { header, details: invDetails, payments: invPayments } = json;
        setInvoiceNo(header.InvoiceNo.trim());
        setInvoiceDate(header.InvoiceDate ? header.InvoiceDate.substring(0, 10) : '');
        setLocationCode(header.LocationCode ? header.LocationCode.trim() : 'HQ');
        setSalesType(header.SalesType ? header.SalesType.trim() : 'CASH');
        setCustomerCode(header.CustomerCode ? header.CustomerCode.trim() : '');
        setCustomerName(header.CustomerName ? header.CustomerName.trim() : '');
        setSalesManCode(header.SalesManCode ? header.SalesManCode.trim() : '');
        setInsAgreementNo(header.InsAgreementNo ? header.InsAgreementNo.trim() : '');
        setRemarks(header.Remarks ? header.Remarks.trim() : '');
        setGst(String(header.GST || 0));
        setTradeInAmt(String(header.TradeInAmt || 0));

        setDetails(invDetails || []);
        setPayments(invPayments || []);
        setIsEditing(true);
        showToast("Invoice loaded successfully");
      } else {
        showToast(json.message || "Failed to load invoice details.", "error");
      }
    } catch (e) {
      console.error(e);
      showToast("Network error loading invoice", "error");
    } finally {
      setLoading(false);
    }
  };

  const handleInvoiceNoLostFocus = () => {
    if (invoiceNo.trim()) {
      loadInvoice(invoiceNo.trim());
    }
  };

  // Fetch LOV datasets
  const openLOV = async (type: 'invoice' | 'location' | 'customer' | 'salesman' | 'inventory' | 'paymode', paymodeIndex?: number) => {
    setLovType(type);
    setLovSearch('');
    setLoading(true);
    if (paymodeIndex !== undefined) {
      setSelectedPaymodeIndex(paymodeIndex);
    }

    try {
      let res;
      if (type === 'invoice') {
        res = await fetch(`${API_URL}/list?t=${Date.now()}`);
      } else if (type === 'location') {
        res = await fetch(`${API_BASE_URL}/location?t=${Date.now()}`);
      } else if (type === 'customer') {
        res = await fetch(`${API_BASE_URL}/customer?t=${Date.now()}`);
      } else if (type === 'salesman') {
        res = await fetch(`${API_BASE_URL}/salesman?t=${Date.now()}`);
      } else if (type === 'inventory') {
        res = await fetch(`${API_BASE_URL}/inventorysearch?t=${Date.now()}`);
      }

      if (res && res.ok) {
        const json = await res.json();
        if (json.success || json.data) {
          const list = json.data || [];
          setLovData(list);
          setFilteredLovData(list);
        } else {
          showToast("Failed to fetch lookups", "error");
        }
      } else if (type === 'paymode') {
        const list = paymodesOptions.map(p => ({ PayMode: p }));
        setLovData(list);
        setFilteredLovData(list);
      }
    } catch (e) {
      console.error(e);
      showToast("Server connection error during lookup", "error");
    } finally {
      setLoading(false);
    }
  };

  const handleLovSearch = (text: string) => {
    setLovSearch(text);
    const term = text.toLowerCase();
    if (!term) {
      setFilteredLovData(lovData);
      return;
    }

    let filtered = [];
    if (lovType === 'invoice') {
      filtered = lovData.filter(i =>
        i.InvoiceNo.toLowerCase().includes(term) ||
        (i.CustomerName || '').toLowerCase().includes(term)
      );
    } else if (lovType === 'location') {
      filtered = lovData.filter(l =>
        l.LocationCode.toLowerCase().includes(term) ||
        (l.LocationName || '').toLowerCase().includes(term)
      );
    } else if (lovType === 'customer') {
      filtered = lovData.filter(c =>
        c.CustomerCode.toLowerCase().includes(term) ||
        (c.CustomerName || '').toLowerCase().includes(term)
      );
    } else if (lovType === 'salesman') {
      filtered = lovData.filter(s =>
        s.SalesManCode.toLowerCase().includes(term) ||
        (s.SalesManName || '').toLowerCase().includes(term)
      );
    } else if (lovType === 'inventory') {
      filtered = lovData.filter(item =>
        item.InventoryCode.toLowerCase().includes(term) ||
        (item.Description || '').toLowerCase().includes(term)
      );
    } else {
      filtered = lovData.filter(p => p.PayMode.toLowerCase().includes(term));
    }
    setFilteredLovData(filtered);
  };

  const handleSelectLovItem = async (item: any) => {
    if (lovType === 'invoice') {
      setInvoiceNo(item.InvoiceNo.trim());
      loadInvoice(item.InvoiceNo.trim());
    } else if (lovType === 'location') {
      setLocationCode(item.LocationCode.trim());
    } else if (lovType === 'customer') {
      setCustomerCode(item.CustomerCode.trim());
      setCustomerName(item.CustomerName ? item.CustomerName.trim() : '');
    } else if (lovType === 'salesman') {
      setSalesManCode(item.SalesManCode.trim());
    } else if (lovType === 'inventory') {
      setLoading(true);
      try {
        const res = await fetch(`${API_BASE_URL}/inventorysearch/${encodeURIComponent(item.InventoryCode.trim())}`);
        const json = await res.json();
        if (json.Inventory) {
          const inv = json.Inventory;
          const newItem: InventoryItem = {
            InventoryCode: inv.InventoryCode.trim(),
            Description: inv.Description ? inv.Description.trim() : '',
            Weight: Number(inv.Weight) || 0,
            StoneWeight: Number(inv.StoneWeight) || 0,
            GST: Number(inv.GST) || 0,
            NetAmount: Number(inv.Price) || Number(inv.NetTotal) || 0,
            TotalCost: Number(inv.TotalCost) || 0
          };
          setDetails([...details, newItem]);
        } else {
          showToast("Failed to retrieve inventory details", "error");
        }
      } catch (e) {
        showToast("Error retrieving inventory details", "error");
      } finally {
        setLoading(false);
      }
    } else if (lovType === 'paymode' && selectedPaymodeIndex !== null) {
      const updated = [...payments];
      updated[selectedPaymodeIndex].PayMode = item.PayMode;
      setPayments(updated);
    }
    setLovType(null);
  };

  const handleDetailChange = (index: number, field: keyof InventoryItem, val: string) => {
    const updated = [...details];
    const item = updated[index];
    if (field === 'NetAmount' || field === 'Weight' || field === 'StoneWeight' || field === 'GST' || field === 'TotalCost') {
      (item as any)[field] = parseFloat(val) || 0;
    } else {
      (item as any)[field] = val;
    }
    setDetails(updated);
  };

  const handlePaymentChange = (index: number, field: keyof PaymentItem, val: string) => {
    const updated = [...payments];
    const p = updated[index];
    if (field === 'Amount') {
      p.Amount = parseFloat(val) || 0;
    } else {
      p[field] = val;
    }
    setPayments(updated);
  };

  // Clear / New
  const handleClear = () => {
    setInvoiceNo('');
    setTodayDate();
    setLocationCode('HQ');
    setSalesType('CASH');
    setCustomerCode('');
    setCustomerName('');
    setSalesManCode('');
    setInsAgreementNo('');
    setRemarks('');
    setGst('0.00');
    setTradeInAmt('0.00');
    setDetails([]);
    setPayments([]);
    fetchNextInvoiceNo();
  };

  return (
    <SafeAreaView style={styles.container} edges={['top']}>
      <Stack.Screen options={{ headerShown: false }} />

      {/* LUXURY GOLD HEADER BAR */}
      <View style={styles.headerBar}>
        <View style={styles.headerLeft}>
          <TouchableOpacity
            style={styles.backBtn}
            onPress={() => router.back()}
            activeOpacity={0.7}
          >
            <Ionicons name="arrow-back" size={20} color="#3B2D17" />
          </TouchableOpacity>
          <ThemedText style={styles.headerTitle}>Sales History Detail</ThemedText>
          {loading && <ActivityIndicator size="small" color="#B48218" style={{ marginLeft: 6 }} />}
        </View>

        <View style={styles.headerRight}>
          <TouchableOpacity
            style={[styles.actionBtn, styles.clearBtn]}
            onPress={handleClear}
            activeOpacity={0.8}
            disabled={loading}
          >
            <Ionicons name="refresh-outline" size={14} color="#5C4A28" />
            <ThemedText style={styles.clearBtnText}>Clear</ThemedText>
          </TouchableOpacity>
        </View>
      </View>

      <ScrollView contentContainerStyle={styles.contentContainer}>
        {/* TOP SPLIT: INVOICE INFO & SUMMARY METRICS */}
        <View style={styles.topSplitRow}>
          {/* Card 1: Header / Invoice Information */}
          <View style={[styles.goldCard, { flex: 1.2 }]}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="receipt-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitleSmall}>Invoice Information</ThemedText>
              </View>
              {isEditing && (
                <View style={styles.statusBadge}>
                  <ThemedText style={styles.statusBadgeText}>History Mode</ThemedText>
                </View>
              )}
            </View>

            <View style={styles.cardBody}>
              {/* Invoice No */}
              <View style={styles.formGridRow}>
                <ThemedText style={styles.formLabel}>Invoice No</ThemedText>
                <View style={styles.inputWithLookup}>
                  <TextInput
                    style={[styles.formInput, styles.inputAttached]}
                    value={invoiceNo}
                    onChangeText={setInvoiceNo}
                    onBlur={handleInvoiceNoLostFocus}
                    placeholder="Enter Invoice No"
                    placeholderTextColor="#9E8D77"
                  />
                  <TouchableOpacity
                    style={styles.attachedLookupBtn}
                    onPress={() => openLOV('invoice')}
                    activeOpacity={0.7}
                  >
                    <Ionicons name="search" size={13} color="#7D5E19" />
                  </TouchableOpacity>
                </View>
              </View>

              {/* Location Code */}
              <View style={styles.formGridRow}>
                <ThemedText style={styles.formLabel}>Location Code</ThemedText>
                <View style={styles.inputWithLookup}>
                  <TextInput
                    style={[styles.formInput, styles.inputAttached]}
                    value={locationCode}
                    onChangeText={setLocationCode}
                    placeholder="Location"
                    placeholderTextColor="#9E8D77"
                  />
                  <TouchableOpacity
                    style={styles.attachedLookupBtn}
                    onPress={() => openLOV('location')}
                    activeOpacity={0.7}
                  >
                    <Ionicons name="search" size={13} color="#7D5E19" />
                  </TouchableOpacity>
                </View>
              </View>

              {/* Customer Code */}
              <View style={styles.formGridRow}>
                <ThemedText style={styles.formLabel}>Customer Code</ThemedText>
                <View style={styles.inputWithLookup}>
                  <TextInput
                    style={[styles.formInput, styles.inputAttached]}
                    value={customerCode}
                    onChangeText={setCustomerCode}
                    placeholder="Customer Code"
                    placeholderTextColor="#9E8D77"
                  />
                  <TouchableOpacity
                    style={styles.attachedLookupBtn}
                    onPress={() => openLOV('customer')}
                    activeOpacity={0.7}
                  >
                    <Ionicons name="search" size={13} color="#7D5E19" />
                  </TouchableOpacity>
                </View>
              </View>

              {/* Customer Name */}
              <View style={styles.formGridRow}>
                <ThemedText style={styles.formLabel}>Customer Name</ThemedText>
                <TextInput
                  style={[styles.formInput, styles.readonlyInput]}
                  value={customerName}
                  editable={false}
                  placeholder="Auto-loaded Name"
                  placeholderTextColor="#9E8D77"
                />
              </View>

              {/* SalesMan */}
              <View style={styles.formGridRow}>
                <ThemedText style={styles.formLabel}>SalesMan</ThemedText>
                <View style={styles.inputWithLookup}>
                  <TextInput
                    style={[styles.formInput, styles.inputAttached]}
                    value={salesManCode}
                    onChangeText={setSalesManCode}
                    placeholder="Salesman"
                    placeholderTextColor="#9E8D77"
                  />
                  <TouchableOpacity
                    style={styles.attachedLookupBtn}
                    onPress={() => openLOV('salesman')}
                    activeOpacity={0.7}
                  >
                    <Ionicons name="search" size={13} color="#7D5E19" />
                  </TouchableOpacity>
                </View>
              </View>

              {/* Order/Ins No */}
              <View style={styles.formGridRow}>
                <ThemedText style={styles.formLabel}>Order/Ins No</ThemedText>
                <TextInput
                  style={styles.formInput}
                  value={insAgreementNo}
                  onChangeText={setInsAgreementNo}
                  placeholder="Agreement or Order No"
                  placeholderTextColor="#9E8D77"
                />
              </View>

              {/* Remarks */}
              <View style={[styles.formGridRow, { borderBottomWidth: 0 }]}>
                <ThemedText style={styles.formLabel}>Remarks</ThemedText>
                <TextInput
                  style={styles.formInput}
                  value={remarks}
                  onChangeText={setRemarks}
                  placeholder="Remarks..."
                  placeholderTextColor="#9E8D77"
                />
              </View>
            </View>
          </View>

          {/* Card 2: Date, Sales Type & Financial Summary */}
          <View style={[styles.goldCard, { flex: 1 }]}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="calculator-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitleSmall}>Values & Totals</ThemedText>
              </View>
            </View>

            <View style={styles.cardBody}>
              {/* Invoice Date */}
              <View style={styles.formGridRow}>
                <ThemedText style={styles.formLabel}>Invoice Date</ThemedText>
                <TextInput
                  style={styles.formInput}
                  value={invoiceDate}
                  onChangeText={setInvoiceDate}
                  placeholder="YYYY-MM-DD"
                  placeholderTextColor="#9E8D77"
                />
              </View>

              {/* Sales Type */}
              <View style={styles.formGridRow}>
                <ThemedText style={styles.formLabel}>Sales Type</ThemedText>
                <TextInput
                  style={styles.formInput}
                  value={salesType}
                  onChangeText={setSalesType}
                  placeholder="CASH / CREDIT"
                  placeholderTextColor="#9E8D77"
                />
              </View>

              {/* Total Weight */}
              <View style={styles.formGridRow}>
                <ThemedText style={styles.formLabel}>Total Weight (g)</ThemedText>
                <TextInput
                  style={[styles.formInput, styles.readonlyInput, { textAlign: 'right' }]}
                  value={totalWeight.toFixed(4)}
                  editable={false}
                />
              </View>

              {/* Total StoneWt */}
              <View style={styles.formGridRow}>
                <ThemedText style={styles.formLabel}>Total Stone Wt</ThemedText>
                <TextInput
                  style={[styles.formInput, styles.readonlyInput, { textAlign: 'right' }]}
                  value={totalStoneWeight.toFixed(4)}
                  editable={false}
                />
              </View>

              {/* Total PCS */}
              <View style={styles.formGridRow}>
                <ThemedText style={styles.formLabel}>Total PCS</ThemedText>
                <TextInput
                  style={[styles.formInput, styles.readonlyInput, { textAlign: 'right' }]}
                  value={String(totalPCS)}
                  editable={false}
                />
              </View>

              {/* GST */}
              <View style={styles.formGridRow}>
                <ThemedText style={styles.formLabel}>GST</ThemedText>
                <TextInput
                  style={[styles.formInput, { textAlign: 'right' }]}
                  value={gst}
                  onChangeText={setGst}
                  keyboardType="numeric"
                />
              </View>

              {/* TradeIn Amt */}
              <View style={styles.formGridRow}>
                <ThemedText style={styles.formLabel}>TradeIn Amt</ThemedText>
                <TextInput
                  style={[styles.formInput, { textAlign: 'right' }]}
                  value={tradeInAmt}
                  onChangeText={setTradeInAmt}
                  keyboardType="numeric"
                />
              </View>

              {/* Net Total Highlight */}
              <View style={styles.netTotalHighlight}>
                <ThemedText style={styles.netTotalHighlightLabel}>Net Total</ThemedText>
                <ThemedText style={styles.netTotalHighlightValue}>${netTotal.toFixed(2)}</ThemedText>
              </View>
            </View>
          </View>
        </View>

        {/* SECTION 2: INVENTORY DETAILS DATA TABLE */}
        <View style={[styles.goldCard, { marginTop: 8 }]}>
          <View style={styles.cardHeaderBanner}>
            <View style={styles.headerTitleContainer}>
              <View style={styles.headerIconBadgeSmall}>
                <Ionicons name="cube-outline" size={13} color="#B48218" />
              </View>
              <ThemedText style={styles.cardHeaderTitleSmall}>Inventory Details</ThemedText>
              <View style={styles.counterBadge}>
                <ThemedText style={styles.counterBadgeText}>{details.length} items</ThemedText>
              </View>
            </View>
          </View>

          {/* Table Header */}
          <View style={styles.tableHeaderRow}>
            <ThemedText style={[styles.tableHeadCell, { flex: 2 }]}>Inventory Code</ThemedText>
            <ThemedText style={[styles.tableHeadCell, { flex: 3.5 }]}>Description</ThemedText>
            <ThemedText style={[styles.tableHeadCell, { flex: 1.5, textAlign: 'right' }]}>Weight (g)</ThemedText>
            <ThemedText style={[styles.tableHeadCell, { flex: 1.5, textAlign: 'right' }]}>St. Wt</ThemedText>
            <ThemedText style={[styles.tableHeadCell, { flex: 1.2, textAlign: 'right' }]}>GST</ThemedText>
            <ThemedText style={[styles.tableHeadCell, { flex: 2, textAlign: 'right' }]}>Net Total</ThemedText>
          </View>

          {details.length === 0 ? (
            <View style={styles.emptyTableContainer}>
              <Ionicons name="basket-outline" size={30} color="#D4C3A3" />
              <ThemedText style={styles.emptyTableText}>No inventory items recorded</ThemedText>
            </View>
          ) : (
            details.map((item, idx) => (
              <View key={idx} style={styles.tableRow}>
                <ThemedText style={[styles.tableRowText, styles.codeCell, { flex: 2 }]}>
                  {item.InventoryCode}
                </ThemedText>
                <ThemedText style={[styles.tableRowText, { flex: 3.5 }]} numberOfLines={1}>
                  {item.Description}
                </ThemedText>
                <TextInput
                  style={[styles.tableInput, { flex: 1.5, textAlign: 'right' }]}
                  value={String(item.Weight)}
                  keyboardType="numeric"
                  onChangeText={(val) => handleDetailChange(idx, 'Weight', val)}
                />
                <TextInput
                  style={[styles.tableInput, { flex: 1.5, textAlign: 'right' }]}
                  value={String(item.StoneWeight)}
                  keyboardType="numeric"
                  onChangeText={(val) => handleDetailChange(idx, 'StoneWeight', val)}
                />
                <TextInput
                  style={[styles.tableInput, { flex: 1.2, textAlign: 'right' }]}
                  value={String(item.GST)}
                  keyboardType="numeric"
                  onChangeText={(val) => handleDetailChange(idx, 'GST', val)}
                />
                <TextInput
                  style={[styles.tableInput, styles.amountCell, { flex: 2, textAlign: 'right' }]}
                  value={String(item.NetAmount)}
                  keyboardType="numeric"
                  onChangeText={(val) => handleDetailChange(idx, 'NetAmount', val)}
                />
              </View>
            ))
          )}
        </View>

        {/* SECTION 3: PAYMENT DETAILS TABLE */}
        <View style={[styles.goldCard, { marginTop: 8 }]}>
          <View style={styles.cardHeaderBanner}>
            <View style={styles.headerTitleContainer}>
              <View style={styles.headerIconBadgeSmall}>
                <Ionicons name="card-outline" size={13} color="#B48218" />
              </View>
              <ThemedText style={styles.cardHeaderTitleSmall}>Payment Details</ThemedText>
              <View style={styles.counterBadge}>
                <ThemedText style={styles.counterBadgeText}>{payments.length} entries</ThemedText>
              </View>
            </View>
          </View>

          {/* Table Header */}
          <View style={styles.tableHeaderRow}>
            <ThemedText style={[styles.tableHeadCell, { flex: 3 }]}>Paymode</ThemedText>
            <ThemedText style={[styles.tableHeadCell, { flex: 3, textAlign: 'right' }]}>Amount ($)</ThemedText>
            <ThemedText style={[styles.tableHeadCell, { flex: 4 }]}>Remarks / Slip No</ThemedText>
          </View>

          {payments.length === 0 ? (
            <View style={styles.emptyTableContainer}>
              <Ionicons name="wallet-outline" size={30} color="#D4C3A3" />
              <ThemedText style={styles.emptyTableText}>No payments recorded</ThemedText>
            </View>
          ) : (
            payments.map((p, idx) => (
              <View key={idx} style={styles.tableRow}>
                <TouchableOpacity
                  style={[styles.tableInput, styles.paymodeSelector, { flex: 3 }]}
                  onPress={() => openLOV('paymode', idx)}
                  activeOpacity={0.7}
                >
                  <ThemedText style={[styles.tableRowText, { fontWeight: 'bold', color: '#8D6409' }]}>
                    {p.PayMode}
                  </ThemedText>
                  <Ionicons name="chevron-down" size={12} color="#7D6F5B" />
                </TouchableOpacity>

                <TextInput
                  style={[styles.tableInput, styles.amountCell, { flex: 3, textAlign: 'right' }]}
                  value={String(p.Amount)}
                  keyboardType="numeric"
                  onChangeText={(val) => handlePaymentChange(idx, 'Amount', val)}
                />

                <TextInput
                  style={[styles.tableInput, { flex: 4 }]}
                  value={p.SlipNo}
                  onChangeText={(val) => handlePaymentChange(idx, 'SlipNo', val)}
                  placeholder="Slip No / Remarks"
                  placeholderTextColor="#9E8D77"
                />
              </View>
            ))
          )}
        </View>
      </ScrollView>

      {/* LOV Lookup Modal */}
      <Modal visible={lovType !== null} transparent={true} animationType="fade" onRequestClose={() => setLovType(null)}>
        <TouchableOpacity style={styles.modalOverlay} activeOpacity={1} onPress={() => setLovType(null)}>
          <TouchableWithoutFeedback>
            <View style={styles.lovModalContainer}>
              <View style={styles.modalHeader}>
                <View style={{ flexDirection: 'row', alignItems: 'center', gap: 6 }}>
                  <View style={styles.headerIconBadgeSmall}>
                    <Ionicons name="list" size={14} color="#B48218" />
                  </View>
                  <ThemedText style={styles.modalTitle}>
                    Select {lovType ? lovType.toUpperCase() : ''}
                  </ThemedText>
                </View>
                <TouchableOpacity onPress={() => setLovType(null)} style={styles.modalCloseBtn}>
                  <Ionicons name="close" size={20} color="#3B2D17" />
                </TouchableOpacity>
              </View>

              <View style={{ padding: 12 }}>
                <View style={styles.modalSearchBox}>
                  <Ionicons name="search-outline" size={15} color="#8F816E" style={{ marginRight: 6 }} />
                  <TextInput
                    style={styles.modalSearchInput}
                    placeholder="Search options..."
                    placeholderTextColor="#8F816E"
                    value={lovSearch}
                    onChangeText={handleLovSearch}
                    autoFocus
                  />
                </View>

                {/* Headers based on LOV type */}
                <View style={styles.lovHeaderRow}>
                  {lovType === 'invoice' && (
                    <>
                      <ThemedText style={[styles.lovHeaderText, { flex: 1.5 }]}>Invoice No</ThemedText>
                      <ThemedText style={[styles.lovHeaderText, { flex: 2 }]}>Customer</ThemedText>
                      <ThemedText style={[styles.lovHeaderText, { flex: 1.5, textAlign: 'right' }]}>Net Total</ThemedText>
                    </>
                  )}
                  {lovType === 'customer' && (
                    <>
                      <ThemedText style={[styles.lovHeaderText, { flex: 2 }]}>Customer Code</ThemedText>
                      <ThemedText style={[styles.lovHeaderText, { flex: 3 }]}>Customer Name</ThemedText>
                    </>
                  )}
                  {lovType === 'location' && (
                    <>
                      <ThemedText style={[styles.lovHeaderText, { flex: 2 }]}>Location Code</ThemedText>
                      <ThemedText style={[styles.lovHeaderText, { flex: 3 }]}>Location Name</ThemedText>
                    </>
                  )}
                  {lovType === 'salesman' && (
                    <>
                      <ThemedText style={[styles.lovHeaderText, { flex: 2 }]}>Code</ThemedText>
                      <ThemedText style={[styles.lovHeaderText, { flex: 3 }]}>Salesman Name</ThemedText>
                    </>
                  )}
                  {lovType === 'inventory' && (
                    <>
                      <ThemedText style={[styles.lovHeaderText, { flex: 2 }]}>Item Code</ThemedText>
                      <ThemedText style={[styles.lovHeaderText, { flex: 3 }]}>Description</ThemedText>
                    </>
                  )}
                  {lovType === 'paymode' && (
                    <ThemedText style={[styles.lovHeaderText, { flex: 1 }]}>Paymode Options</ThemedText>
                  )}
                </View>

                {loading ? (
                  <View style={{ padding: 30, alignItems: 'center' }}>
                    <ActivityIndicator size="large" color="#B48218" />
                  </View>
                ) : (
                  <FlatList
                    data={filteredLovData}
                    keyExtractor={(_, index) => index.toString()}
                    style={{ maxHeight: 320 }}
                    renderItem={({ item, index }) => (
                      <TouchableOpacity
                        style={[
                          styles.lovItemRow,
                          index % 2 === 1 && { backgroundColor: '#FCFAF6' }
                        ]}
                        activeOpacity={0.7}
                        onPress={() => handleSelectLovItem(item)}
                      >
                        {lovType === 'invoice' && (
                          <>
                            <ThemedText style={[styles.lovItemText, styles.codeCell, { flex: 1.5 }]}>{item.InvoiceNo}</ThemedText>
                            <ThemedText style={[styles.lovItemText, { flex: 2 }]}>{item.CustomerName || ''}</ThemedText>
                            <ThemedText style={[styles.lovItemText, styles.amountCell, { flex: 1.5, textAlign: 'right' }]}>${(item.NetTotal || 0).toFixed(2)}</ThemedText>
                          </>
                        )}
                        {lovType === 'customer' && (
                          <>
                            <ThemedText style={[styles.lovItemText, styles.codeCell, { flex: 2 }]}>{item.CustomerCode}</ThemedText>
                            <ThemedText style={[styles.lovItemText, { flex: 3 }]}>{item.CustomerName || ''}</ThemedText>
                          </>
                        )}
                        {lovType === 'location' && (
                          <>
                            <ThemedText style={[styles.lovItemText, styles.codeCell, { flex: 2 }]}>{item.LocationCode}</ThemedText>
                            <ThemedText style={[styles.lovItemText, { flex: 3 }]}>{item.LocationName || ''}</ThemedText>
                          </>
                        )}
                        {lovType === 'salesman' && (
                          <>
                            <ThemedText style={[styles.lovItemText, styles.codeCell, { flex: 2 }]}>{item.SalesManCode}</ThemedText>
                            <ThemedText style={[styles.lovItemText, { flex: 3 }]}>{item.SalesManName || ''}</ThemedText>
                          </>
                        )}
                        {lovType === 'inventory' && (
                          <>
                            <ThemedText style={[styles.lovItemText, styles.codeCell, { flex: 2 }]}>{item.InventoryCode}</ThemedText>
                            <ThemedText style={[styles.lovItemText, { flex: 3 }]}>{item.Description || ''}</ThemedText>
                          </>
                        )}
                        {lovType === 'paymode' && (
                          <ThemedText style={[styles.lovItemText, { flex: 1, fontWeight: 'bold', color: '#8D6409' }]}>{item.PayMode}</ThemedText>
                        )}
                      </TouchableOpacity>
                    )}
                  />
                )}
              </View>
            </View>
          </TouchableWithoutFeedback>
        </TouchableOpacity>
      </Modal>

      {/* Loading Overlay */}
      {loading && (
        <View style={styles.loaderOverlay}>
          <ActivityIndicator size="large" color="#B48218" />
        </View>
      )}

      {/* Toast Alert */}
      {toast && (
        <View style={[styles.toast, toast.type === 'error' ? styles.toastError : styles.toastSuccess]}>
          <Ionicons
            name={toast.type === 'error' ? 'alert-circle' : 'checkmark-circle'}
            size={16}
            color="#FFFFFF"
            style={{ marginRight: 6 }}
          />
          <ThemedText style={styles.toastText}>{toast.message}</ThemedText>
        </View>
      )}
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#FBF9F5',
  },
  contentContainer: {
    padding: 8,
    paddingBottom: 24,
  },

  // ===== HEADER BAR =====
  headerBar: {
    height: 48,
    backgroundColor: '#FFFFFF',
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingHorizontal: 12,
    borderBottomWidth: 1,
    borderBottomColor: '#EBE2D2',
    elevation: 2,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.05,
    shadowRadius: 2,
  },
  headerLeft: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 8,
  },
  backBtn: {
    padding: 6,
    borderRadius: 6,
    backgroundColor: '#FAF5EA',
    borderWidth: 1,
    borderColor: '#E8DCB8',
  },
  headerTitle: {
    fontSize: 18,
    fontWeight: 'bold',
    color: '#3B2D17',
    letterSpacing: 0.5,
  },
  headerRight: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 6,
  },

  // ===== ACTION BUTTONS =====
  actionBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    paddingHorizontal: 12,
    paddingVertical: 5,
    borderRadius: 6,
    borderWidth: 1,
    gap: 4,
    minHeight: 28,
  },
  clearBtn: {
    backgroundColor: '#FFFFFF',
    borderColor: '#DFD5C3',
  },
  clearBtnText: {
    color: '#5C4A28',
    fontSize: 12,
    fontWeight: 'bold',
  },

  // ===== TOP SPLIT =====
  topSplitRow: {
    flexDirection: 'row',
    gap: 8,
  },

  // ===== LUXURY GOLD CARD =====
  goldCard: {
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    shadowColor: '#A38035',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.08,
    shadowRadius: 4,
    elevation: 2,
    overflow: 'hidden',
  },
  cardHeaderBanner: {
    backgroundColor: '#F5E7CA',
    paddingHorizontal: 10,
    paddingVertical: 6,
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
  },
  headerTitleContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 6,
  },
  headerIconBadgeSmall: {
    width: 22,
    height: 22,
    borderRadius: 5,
    backgroundColor: '#FFF8EA',
    borderWidth: 1,
    borderColor: '#E2D2AF',
    alignItems: 'center',
    justifyContent: 'center',
  },
  cardHeaderTitleSmall: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#3B2D17',
    letterSpacing: 0.3,
  },
  counterBadge: {
    backgroundColor: '#FAF5EA',
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderRadius: 10,
    paddingHorizontal: 7,
    paddingVertical: 1,
    marginLeft: 6,
  },
  counterBadgeText: {
    fontSize: 10,
    fontWeight: 'bold',
    color: '#7D5E19',
  },
  statusBadge: {
    backgroundColor: '#FFF8EA',
    borderWidth: 1,
    borderColor: '#D8C59A',
    borderRadius: 10,
    paddingHorizontal: 8,
    paddingVertical: 1,
  },
  statusBadgeText: {
    fontSize: 10,
    fontWeight: 'bold',
    color: '#8D6409',
  },
  cardBody: {
    padding: 8,
  },

  // ===== FORM ROWS & INPUTS =====
  formGridRow: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingVertical: 4,
    borderBottomWidth: 1,
    borderBottomColor: '#F7F2E9',
    gap: 8,
  },
  formLabel: {
    fontSize: 11,
    fontWeight: '600',
    color: '#4A3B2C',
    width: 95,
    flexShrink: 0,
  },
  inputWithLookup: {
    flexDirection: 'row',
    flex: 1,
    alignItems: 'center',
    minWidth: 0,
  },
  formInput: {
    height: 26,
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderRadius: 5,
    paddingHorizontal: 8,
    paddingVertical: 0,
    fontSize: 11,
    color: '#2B2118',
    flex: 1,
  },
  readonlyInput: {
    backgroundColor: '#F8F5EE',
    borderColor: '#EADBCA',
    color: '#554736',
  },
  inputAttached: {
    borderTopRightRadius: 0,
    borderBottomRightRadius: 0,
  },
  attachedLookupBtn: {
    width: 28,
    height: 26,
    flexShrink: 0,
    backgroundColor: '#FAF5EA',
    borderWidth: 1,
    borderColor: '#D4C3A3',
    borderRadius: 5,
    marginLeft: 4,
    alignItems: 'center',
    justifyContent: 'center',
  },
  netTotalHighlight: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    backgroundColor: '#FFF8EA',
    borderWidth: 1,
    borderColor: '#B8903B',
    borderRadius: 6,
    paddingHorizontal: 10,
    paddingVertical: 6,
    marginTop: 6,
  },
  netTotalHighlightLabel: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#8D6409',
    textTransform: 'uppercase',
  },
  netTotalHighlightValue: {
    fontSize: 16,
    fontWeight: 'bold',
    color: '#9E6D08',
  },

  // ===== DATA TABLE =====
  tableHeaderRow: {
    flexDirection: 'row',
    backgroundColor: '#F5E7CA',
    paddingVertical: 6,
    paddingHorizontal: 8,
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
  },
  tableHeadCell: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3B2D17',
    textTransform: 'uppercase',
  },
  tableRow: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingVertical: 6,
    paddingHorizontal: 8,
    borderBottomWidth: 1,
    borderBottomColor: '#F3EBDD',
  },
  tableRowText: {
    fontSize: 11,
    color: '#3B2D17',
  },
  tableInput: {
    height: 24,
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderRadius: 4,
    paddingHorizontal: 6,
    paddingVertical: 0,
    fontSize: 11,
    color: '#2B2118',
    marginHorizontal: 2,
  },
  paymodeSelector: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
  },
  codeCell: {
    fontWeight: 'bold',
    color: '#8D6409',
  },
  amountCell: {
    fontWeight: 'bold',
    color: '#9E6D08',
  },
  emptyTableContainer: {
    padding: 24,
    alignItems: 'center',
    justifyContent: 'center',
    gap: 6,
  },
  emptyTableText: {
    fontSize: 11,
    color: '#8F816E',
  },

  // ===== MODALS & LOV =====
  modalOverlay: {
    flex: 1,
    backgroundColor: 'rgba(30, 20, 10, 0.45)',
    justifyContent: 'center',
    alignItems: 'center',
    padding: 16,
  },
  lovModalContainer: {
    width: '100%',
    maxWidth: 640,
    backgroundColor: '#FFFFFF',
    borderRadius: 10,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    overflow: 'hidden',
    shadowColor: '#000',
    shadowOpacity: 0.15,
    shadowRadius: 10,
    elevation: 6,
  },
  modalHeader: {
    backgroundColor: '#F5E7CA',
    paddingHorizontal: 14,
    paddingVertical: 10,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
  },
  modalTitle: {
    fontSize: 13,
    fontWeight: 'bold',
    color: '#3B2D17',
  },
  modalCloseBtn: {
    padding: 4,
  },
  modalSearchBox: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderRadius: 6,
    paddingHorizontal: 10,
    height: 32,
    marginBottom: 8,
  },
  modalSearchInput: {
    flex: 1,
    fontSize: 12,
    color: '#2B2118',
    paddingVertical: 0,
  },
  lovHeaderRow: {
    flexDirection: 'row',
    backgroundColor: '#F5E7CA',
    paddingVertical: 6,
    paddingHorizontal: 8,
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
  },
  lovHeaderText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3B2D17',
  },
  lovItemRow: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingVertical: 8,
    paddingHorizontal: 8,
    borderBottomWidth: 1,
    borderBottomColor: '#F3EBDD',
  },
  lovItemText: {
    fontSize: 12,
    color: '#3B2D17',
  },

  // ===== TOAST & LOADER =====
  toast: {
    position: 'absolute',
    top: 56,
    alignSelf: 'center',
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 16,
    paddingVertical: 8,
    borderRadius: 20,
    zIndex: 9999,
    elevation: 6,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.15,
    shadowRadius: 4,
  },
  toastSuccess: {
    backgroundColor: '#2E7D32',
  },
  toastError: {
    backgroundColor: '#D32F2F',
  },
  toastText: {
    color: '#FFFFFF',
    fontWeight: 'bold',
    fontSize: 12,
  },
  loaderOverlay: {
    ...StyleSheet.absoluteFillObject,
    backgroundColor: 'rgba(30, 20, 10, 0.35)',
    justifyContent: 'center',
    alignItems: 'center',
    zIndex: 99999,
  },
});
