import { ThemedText } from '@/components/themed-text';
import { API_BASE_URL } from '@/constants/api';
import { Ionicons } from '@expo/vector-icons';
import { Stack, useRouter } from 'expo-router';
import React, { useEffect, useState } from 'react';
import {
  ActivityIndicator,
  Alert,
  FlatList,
  KeyboardAvoidingView,
  Modal,
  Platform,
  ScrollView,
  StyleSheet,
  TextInput,
  TouchableOpacity,
  TouchableWithoutFeedback,
  View,
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';

const API_URL = `${API_BASE_URL}/creditsalespayment`;
const CUSTOMER_API_URL = `${API_BASE_URL}/customer`;

export default function CreditSalesPaymentScreen() {
  const router = useRouter();

  const [loading, setLoading] = useState(false);
  const [toast, setToast] = useState<{ message: string; type: 'success' | 'error' } | null>(null);

  // Top Filter States
  const [paymentNo, setPaymentNo] = useState('');
  const [paymentDate, setPaymentDate] = useState('');
  const [customerCode, setCustomerCode] = useState('');
  const [customerName, setCustomerName] = useState('');
  const [fromDate, setFromDate] = useState('');
  const [toDate, setToDate] = useState('');
  const [statusFilter, setStatusFilter] = useState<'UNPAID' | 'PAID' | 'ALL'>('UNPAID');

  // Invoices List State
  const [invoiceList, setInvoiceList] = useState<any[]>([]);

  // Bottom Selected Invoice Detail & Payment Form
  const [selectedInvoiceNo, setSelectedInvoiceNo] = useState('');
  const [selectedInvoiceDate, setSelectedInvoiceDate] = useState('');
  const [selectedCustCode, setSelectedCustCode] = useState('');
  const [totalAmount, setTotalAmount] = useState('');
  const [paidAmount, setPaidAmount] = useState('');
  const [balanceAmount, setBalanceAmount] = useState('');

  const [paymode, setPaymode] = useState('CASH');
  const [amountToPay, setAmountToPay] = useState('');
  const [remarks, setRemarks] = useState('');

  // Lookup Modal State
  const [isCustomerModalVisible, setIsCustomerModalVisible] = useState(false);
  const [customerList, setCustomerList] = useState<any[]>([]);
  const [customerSearchQuery, setCustomerSearchQuery] = useState('');
  const [isPaymodePickerVisible, setIsPaymodePickerVisible] = useState(false);

  // Calendar State
  const [activeDatePicker, setActiveDatePicker] = useState<'payment' | 'from' | 'to' | null>(null);
  const [isCalendarOpen, setIsCalendarOpen] = useState(false);
  const [calendarMonth, setCalendarMonth] = useState(new Date().getMonth());
  const [calendarYear, setCalendarYear] = useState(new Date().getFullYear());

  const monthNames = [
    "January", "February", "March", "April", "May", "June",
    "July", "August", "September", "October", "November", "December"
  ];

  const showToast = (message: string, type: 'success' | 'error' = 'success') => {
    setToast({ message, type });
    setTimeout(() => setToast(null), 3000);
  };

  const showAlert = (title: string, message: string) => {
    if (Platform.OS === 'web') {
      window.alert(`${title}: ${message}`);
    } else {
      Alert.alert(title, message);
    }
  };

  // Initial Load
  useEffect(() => {
    const today = new Date();
    const dd = String(today.getDate()).padStart(2, '0');
    const mm = String(today.getMonth() + 1).padStart(2, '0');
    const yyyy = today.getFullYear();
    const formattedToday = `${dd}/${mm}/${yyyy}`;

    setPaymentDate(formattedToday);
    setFromDate(formattedToday);
    setToDate(formattedToday);

    fetchNextPaymentNo();
  }, []);

  // Fetch running payment voucher number
  const fetchNextPaymentNo = async () => {
    try {
      const res = await fetch(`${API_URL}/next-number`);
      const json = await res.json();
      if (json.success) {
        setPaymentNo(json.paymentNo);
      }
    } catch (err) {
      console.error("Failed to load next payment number:", err);
    }
  };

  // Open Customer Lookup Modal
  const handleOpenCustomerLookup = async () => {
    setLoading(true);
    try {
      const res = await fetch(CUSTOMER_API_URL);
      const json = await res.json();
      if (json.success) {
        setCustomerList(json.data);
        setIsCustomerModalVisible(true);
      } else {
        showToast('Failed to load customer list', 'error');
      }
    } catch (err) {
      showToast('Error querying customer directory', 'error');
    } finally {
      setLoading(false);
    }
  };

  // Select Customer from Modal
  const handleSelectCustomer = (cust: any) => {
    setCustomerCode(cust.CustomerCode ? cust.CustomerCode.trim() : '');
    setCustomerName(cust.CustomerName ? cust.CustomerName.trim() : '');
    setIsCustomerModalVisible(false);
  };

  // Fetch Credit Sales Invoices based on filters
  const handleFetchInvoices = async () => {
    setLoading(true);
    try {
      const formatToYYYYMMDD = (dStr: string) => {
        const p = dStr.split('/');
        return `${p[2]}${p[1]}${p[0]}`;
      };

      const fDate = formatToYYYYMMDD(fromDate);
      const tDate = formatToYYYYMMDD(toDate);

      let url = `${API_URL}/invoices?fromDate=${fDate}&toDate=${tDate}&status=${statusFilter}`;
      if (customerCode.trim()) {
        url += `&customerCode=${encodeURIComponent(customerCode.trim())}`;
      }

      const res = await fetch(url);
      const json = await res.json();
      if (json.success) {
        setInvoiceList(json.data);
        showToast(`Loaded ${json.data.length} invoices`);
      } else {
        showToast(json.message || 'Failed to fetch invoices', 'error');
      }
    } catch (err) {
      showToast('Error fetching invoices', 'error');
    } finally {
      setLoading(false);
    }
  };

  // Tap Row to select invoice for payment
  const handleSelectInvoice = (item: any) => {
    setSelectedInvoiceNo(item.InvoiceNo || '');

    if (item.InvoiceDate) {
      const d = new Date(item.InvoiceDate);
      const dd = String(d.getDate()).padStart(2, '0');
      const mm = String(d.getMonth() + 1).padStart(2, '0');
      const yyyy = d.getFullYear();
      setSelectedInvoiceDate(`${dd}/${mm}/${yyyy}`);
    } else {
      setSelectedInvoiceDate('');
    }

    setSelectedCustCode(item.CustomerCode || '');
    setTotalAmount(item.NetTotal ? String(item.NetTotal) : '0.00');
    setPaidAmount(item.PaidAmt ? String(item.PaidAmt) : '0.00');
    setBalanceAmount(item.BalanceAmt ? String(item.BalanceAmt) : '0.00');

    // Auto-fill payment amount to balance
    setAmountToPay(item.BalanceAmt ? String(item.BalanceAmt) : '0.00');
  };

  // UPDATE / SAVE payment details
  const handleUpdatePayment = async () => {
    if (!selectedInvoiceNo.trim()) {
      showToast('Please select an Invoice details from the list to update', 'error');
      return;
    }

    if (!paymode.trim()) {
      showToast('Please select a Paymode', 'error');
      return;
    }

    const payVal = parseFloat(amountToPay) || 0;
    if (payVal <= 0) {
      showToast('Please Enter a valid Amount greater than zero', 'error');
      return;
    }

    const currentPaid = parseFloat(paidAmount) || 0;
    const totalVal = parseFloat(totalAmount) || 0;
    if (payVal + currentPaid > totalVal) {
      showToast('Please Check the Amount: Total Paid exceeds Invoice Total Amount', 'error');
      return;
    }

    if (paymode === 'CHEQUE' && !remarks.trim()) {
      showToast('Please Enter the Remarks (Cheque Details)', 'error');
      return;
    }

    const pParts = paymentDate.split('/');
    const formattedPaymentDate = `${pParts[2]}${pParts[1]}${pParts[0]}`;

    const iParts = selectedInvoiceDate.split('/');
    const formattedInvoiceDate = new Date(parseInt(iParts[2]), parseInt(iParts[1]) - 1, parseInt(iParts[0])).toISOString();

    const payload = {
      PaymentNo: paymentNo,
      PaymentDate: formattedPaymentDate,
      Amount: payVal,
      Remarks: remarks,
      InvoiceNo: selectedInvoiceNo,
      InvoiceDate: formattedInvoiceDate,
      CustCode: selectedCustCode,
      Paymode: paymode,
      CreateUser: 'pos'
    };

    setLoading(true);
    try {
      const res = await fetch(`${API_URL}/save`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload)
      });
      const json = await res.json();
      if (json.success) {
        showToast(json.message || 'Payment updated successfully', 'success');
        handleClearAll();
      } else {
        showToast(json.message || 'Failed to update payment', 'error');
      }
    } catch (err) {
      showToast('An unexpected error occurred while saving the payment', 'error');
    } finally {
      setLoading(false);
    }
  };

  // Clear everything
  const handleClearAll = () => {
    setCustomerCode('');
    setCustomerName('');
    setSelectedInvoiceNo('');
    setSelectedInvoiceDate('');
    setSelectedCustCode('');
    setTotalAmount('');
    setPaidAmount('');
    setBalanceAmount('');
    setAmountToPay('');
    setRemarks('');
    setPaymode('CASH');
    setInvoiceList([]);
    fetchNextPaymentNo();
  };

  // Calendar helpers
  const handlePrevMonth = () => {
    if (calendarMonth === 0) {
      setCalendarMonth(11);
      setCalendarYear(calendarYear - 1);
    } else {
      setCalendarMonth(calendarMonth - 1);
    }
  };

  const handleNextMonth = () => {
    if (calendarMonth === 11) {
      setCalendarMonth(0);
      setCalendarYear(calendarYear + 1);
    } else {
      setCalendarMonth(calendarMonth + 1);
    }
  };

  const generateCalendarDays = () => {
    const firstDay = new Date(calendarYear, calendarMonth, 1).getDay();
    const daysInMonth = new Date(calendarYear, calendarMonth + 1, 0).getDate();
    const days = [];

    for (let i = 0; i < firstDay; i++) {
      days.push(null);
    }
    for (let i = 1; i <= daysInMonth; i++) {
      days.push({ day: i });
    }
    return days;
  };

  const handleSelectDate = (day: number) => {
    const dd = String(day).padStart(2, '0');
    const mm = String(calendarMonth + 1).padStart(2, '0');
    const yyyy = calendarYear;
    const formattedDate = `${dd}/${mm}/${yyyy}`;

    if (activeDatePicker === 'payment') {
      setPaymentDate(formattedDate);
    } else if (activeDatePicker === 'from') {
      setFromDate(formattedDate);
    } else if (activeDatePicker === 'to') {
      setToDate(formattedDate);
    }
    setIsCalendarOpen(false);
  };

  const openCalendar = (picker: 'payment' | 'from' | 'to') => {
    setActiveDatePicker(picker);
    let currentDateVal = '';
    if (picker === 'payment') currentDateVal = paymentDate;
    else if (picker === 'from') currentDateVal = fromDate;
    else if (picker === 'to') currentDateVal = toDate;

    if (currentDateVal) {
      const parts = currentDateVal.split('/');
      if (parts.length === 3) {
        setCalendarMonth(parseInt(parts[1]) - 1);
        setCalendarYear(parseInt(parts[2]));
      }
    }
    setIsCalendarOpen(true);
  };

  return (
    <SafeAreaView style={styles.safeArea} edges={['top']}>
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
          <ThemedText style={styles.headerTitle}>Credit Sales Payment</ThemedText>
          {loading && <ActivityIndicator size="small" color="#B48218" style={{ marginLeft: 6 }} />}
        </View>

        <View style={styles.headerRight}>
          <TouchableOpacity
            style={[styles.actionBtn, styles.clearBtn]}
            onPress={handleClearAll}
            activeOpacity={0.8}
            disabled={loading}
          >
            <Ionicons name="refresh-outline" size={14} color="#5C4A28" />
            <ThemedText style={styles.clearBtnText}>Clear</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity
            style={[styles.actionBtn, styles.saveBtn]}
            onPress={handleUpdatePayment}
            activeOpacity={0.8}
            disabled={loading}
          >
            {loading ? (
              <ActivityIndicator size="small" color="#9E6D08" />
            ) : (
              <>
                <Ionicons name="checkmark-done-outline" size={14} color="#9E6D08" />
                <ThemedText style={styles.saveBtnText}>Update</ThemedText>
              </>
            )}
          </TouchableOpacity>
        </View>
      </View>

      <KeyboardAvoidingView behavior={Platform.OS === 'ios' ? 'padding' : undefined} style={{ flex: 1 }}>
        <ScrollView contentContainerStyle={styles.scrollContainer}>

          {/* CARD 1: FILTERS & SELECTION */}
          <View style={styles.goldCard}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="filter-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitleSmall}>Payment & Invoice Search</ThemedText>
              </View>

              {/* Payment Voucher Badge */}
              <View style={styles.voucherBadge}>
                <ThemedText style={styles.voucherBadgeLabel}>Payment No</ThemedText>
                <ThemedText style={styles.voucherBadgeValue}>{paymentNo || '...'}</ThemedText>
              </View>
            </View>

            <View style={styles.cardBody}>
              {/* Row 1: Customer Code, Customer Name, Payment Date in One Single Row */}
              <View style={[styles.formGridRow, { borderBottomWidth: 0, gap: 12, marginBottom: 8 }]}>
                {/* Customer Code */}
                <View style={{ flex: 1.1, flexDirection: 'row', alignItems: 'center' }}>
                  <ThemedText style={styles.formLabel}>Customer Code</ThemedText>
                  <View style={styles.inputWithLookup}>
                    <TextInput
                      style={[styles.formInput, styles.inputAttached]}
                      value={customerCode}
                      onChangeText={setCustomerCode}
                      placeholder="Select Customer..."
                      placeholderTextColor="#9E8D77"
                    />
                    <TouchableOpacity
                      style={styles.attachedLookupBtn}
                      onPress={handleOpenCustomerLookup}
                      activeOpacity={0.7}
                    >
                      <Ionicons name="search" size={13} color="#7D5E19" />
                    </TouchableOpacity>
                  </View>
                </View>

                {/* Customer Name */}
                <View style={{ flex: 1.5, flexDirection: 'row', alignItems: 'center' }}>
                  <ThemedText style={styles.formLabel}>Customer Name</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput]}
                    value={customerName}
                    editable={false}
                    placeholder="Customer Name"
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* Payment Date */}
                <View style={{ flex: 1.1, flexDirection: 'row', alignItems: 'center' }}>
                  <ThemedText style={styles.formLabel}>Payment Date</ThemedText>
                  <View style={styles.inputWithLookup}>
                    <TextInput
                      style={[styles.formInput, styles.inputAttached]}
                      value={paymentDate}
                      onChangeText={setPaymentDate}
                      placeholder="dd/MM/yyyy"
                      placeholderTextColor="#9E8D77"
                    />
                    <TouchableOpacity
                      style={styles.attachedLookupBtn}
                      onPress={() => openCalendar('payment')}
                      activeOpacity={0.7}
                    >
                      <Ionicons name="calendar-outline" size={14} color="#7D5E19" />
                    </TouchableOpacity>
                  </View>
                </View>
              </View>

              {/* Row 2: Two Column Grid for Invoice Date Range & Payment Status */}
              <View style={[styles.twoColumnGrid, { alignItems: 'stretch' }]}>
                {/* Left Column: Date Range Subframe */}
                <View style={styles.columnHalf}>
                  <View style={[styles.subFrameCard, { height: 74, justifyContent: 'space-between', paddingVertical: 8 }]}>
                    <ThemedText style={styles.subFrameTitle}>INVOICE DATE RANGE</ThemedText>

                    <View style={styles.subFrameContent}>
                      <View style={[styles.formGridRow, { borderBottomWidth: 0, gap: 12, paddingVertical: 0 }]}>
                        {/* From Date */}
                        <View style={{ flex: 1, flexDirection: 'row', alignItems: 'center' }}>
                          <ThemedText style={styles.formSubLabel}>From Date</ThemedText>
                          <View style={styles.inputWithLookup}>
                            <TextInput
                              style={[styles.formInput, styles.inputAttached]}
                              value={fromDate}
                              onChangeText={setFromDate}
                              placeholder="dd/MM/yyyy"
                              placeholderTextColor="#9E8D77"
                            />
                            <TouchableOpacity
                              style={styles.attachedLookupBtn}
                              onPress={() => openCalendar('from')}
                              activeOpacity={0.7}
                            >
                              <Ionicons name="calendar-outline" size={14} color="#7D5E19" />
                            </TouchableOpacity>
                          </View>
                        </View>

                        {/* To Date */}
                        <View style={{ flex: 1, flexDirection: 'row', alignItems: 'center' }}>
                          <ThemedText style={styles.formSubLabel}>To Date</ThemedText>
                          <View style={styles.inputWithLookup}>
                            <TextInput
                              style={[styles.formInput, styles.inputAttached]}
                              value={toDate}
                              onChangeText={setToDate}
                              placeholder="dd/MM/yyyy"
                              placeholderTextColor="#9E8D77"
                            />
                            <TouchableOpacity
                              style={styles.attachedLookupBtn}
                              onPress={() => openCalendar('to')}
                              activeOpacity={0.7}
                            >
                              <Ionicons name="calendar-outline" size={14} color="#7D5E19" />
                            </TouchableOpacity>
                          </View>
                        </View>
                      </View>
                    </View>
                  </View>
                </View>

                {/* Right Column: Status Filter & Fetch Action Row */}
                <View style={styles.columnHalf}>
                  <View style={{ flexDirection: 'row', alignItems: 'center', gap: 10 }}>
                    <View style={[styles.subFrameCard, { flex: 1, marginTop: 0, height: 74, justifyContent: 'space-between', paddingVertical: 8 }]}>
                      <ThemedText style={styles.subFrameTitle}>PAYMENT STATUS</ThemedText>

                      <View style={[styles.radioGroupRow, { paddingVertical: 0, height: 32, alignItems: 'center' }]}>
                        <TouchableOpacity
                          style={styles.radioItem}
                          onPress={() => setStatusFilter('UNPAID')}
                          activeOpacity={0.7}
                        >
                          <View style={[styles.radioCircle, statusFilter === 'UNPAID' && styles.radioCircleSelected]}>
                            {statusFilter === 'UNPAID' && <View style={styles.radioInner} />}
                          </View>
                          <ThemedText style={styles.radioLabel}>UnPaid</ThemedText>
                        </TouchableOpacity>

                        <TouchableOpacity
                          style={styles.radioItem}
                          onPress={() => setStatusFilter('PAID')}
                          activeOpacity={0.7}
                        >
                          <View style={[styles.radioCircle, statusFilter === 'PAID' && styles.radioCircleSelected]}>
                            {statusFilter === 'PAID' && <View style={styles.radioInner} />}
                          </View>
                          <ThemedText style={styles.radioLabel}>Paid</ThemedText>
                        </TouchableOpacity>

                        <TouchableOpacity
                          style={styles.radioItem}
                          onPress={() => setStatusFilter('ALL')}
                          activeOpacity={0.7}
                        >
                          <View style={[styles.radioCircle, statusFilter === 'ALL' && styles.radioCircleSelected]}>
                            {statusFilter === 'ALL' && <View style={styles.radioInner} />}
                          </View>
                          <ThemedText style={styles.radioLabel}>All</ThemedText>
                        </TouchableOpacity>
                      </View>
                    </View>

                    {/* Fetch Action Button */}
                    <TouchableOpacity
                      style={[styles.fetchGoldBtn, { minWidth: 120, height: 34, marginTop: 0, paddingHorizontal: 12 }]}
                      onPress={handleFetchInvoices}
                      activeOpacity={0.8}
                    >
                      <Ionicons name="search" size={15} color="#9E6D08" style={{ marginRight: 6 }} />
                      <ThemedText style={styles.fetchGoldBtnText}>Fetch Invoices</ThemedText>
                    </TouchableOpacity>
                  </View>
                </View>
              </View>
            </View>
          </View>

          {/* CARD 2: INVOICES LIST DATA TABLE */}
          <View style={[styles.goldCard, { marginTop: 8 }]}>
            {/* Table Header */}
            <View style={styles.tableHeaderRow}>
              <ThemedText style={[styles.tableHeadCell, { flex: 1.5 }]}>Invoice No</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 1.3 }]}>Date</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 1.2 }]}>Cust Code</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 2.2 }]}>Cust Name</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 1.2, textAlign: 'right' }]}>Total ($)</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 1.2, textAlign: 'right' }]}>Paid ($)</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 1.2, textAlign: 'right' }]}>Balance ($)</ThemedText>
            </View>

            {invoiceList.length === 0 ? (
              <View style={styles.emptyTableContainer}>
                <Ionicons name="receipt-outline" size={30} color="#D4C3A3" />
                <ThemedText style={styles.emptyTableText}>No invoices loaded. Set filters and click Fetch.</ThemedText>
              </View>
            ) : (
              invoiceList.map((item) => {
                const isSelected = selectedInvoiceNo === item.InvoiceNo;
                return (
                  <TouchableOpacity
                    key={item.InvoiceNo}
                    style={[styles.tableRow, isSelected && styles.tableRowSelected]}
                    onPress={() => handleSelectInvoice(item)}
                    activeOpacity={0.7}
                  >
                    <ThemedText style={[styles.tableRowText, styles.codeCell, { flex: 1.5 }]}>
                      {item.InvoiceNo}
                    </ThemedText>
                    <ThemedText style={[styles.tableRowText, { flex: 1.3 }]}>
                      {item.InvoiceDate ? new Date(item.InvoiceDate).toLocaleDateString() : ''}
                    </ThemedText>
                    <ThemedText style={[styles.tableRowText, { flex: 1.2 }]}>
                      {item.CustomerCode}
                    </ThemedText>
                    <ThemedText style={[styles.tableRowText, { flex: 2.2 }]} numberOfLines={1}>
                      {item.CustomerName}
                    </ThemedText>
                    <ThemedText style={[styles.tableRowText, { flex: 1.2, textAlign: 'right' }]}>
                      {item.NetTotal}
                    </ThemedText>
                    <ThemedText style={[styles.tableRowText, { flex: 1.2, textAlign: 'right' }]}>
                      {item.PaidAmt}
                    </ThemedText>
                    <ThemedText style={[styles.tableRowText, styles.amountCell, { flex: 1.2, textAlign: 'right' }]}>
                      {item.BalanceAmt}
                    </ThemedText>
                  </TouchableOpacity>
                );
              })
            )}
          </View>

          {/* CARD 3: PAYMENT ENTRY FORM */}
          <View style={[styles.goldCard, { marginTop: 8 }]}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="card-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitleSmall}>Payment Voucher Entry</ThemedText>
              </View>
              {selectedInvoiceNo ? (
                <View style={styles.selectedInvoiceBadge}>
                  <ThemedText style={styles.selectedInvoiceBadgeText}>Selected: {selectedInvoiceNo}</ThemedText>
                </View>
              ) : null}
            </View>

            <View style={styles.cardBody}>
              {/* Single Combined Row: Invoice Descriptors & Financial Balances */}
              <View style={{ flexDirection: 'row', alignItems: 'center', gap: 10, flexWrap: 'nowrap' }}>
                <View style={{ flex: 1.1, minWidth: 100 }}>
                  <ThemedText style={styles.formItemLabel} numberOfLines={1}>Invoice No</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput]}
                    value={selectedInvoiceNo}
                    editable={false}
                    placeholder="None"
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                <View style={{ flex: 1, minWidth: 85 }}>
                  <ThemedText style={styles.formItemLabel} numberOfLines={1}>Invoice Date</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput]}
                    value={selectedInvoiceDate}
                    editable={false}
                    placeholder="-"
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                <View style={{ flex: 1, minWidth: 85 }}>
                  <ThemedText style={styles.formItemLabel} numberOfLines={1}>Cust Code</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput]}
                    value={selectedCustCode}
                    editable={false}
                    placeholder="-"
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                <View style={{ flex: 1.1, minWidth: 95 }}>
                  <ThemedText style={styles.formItemLabel} numberOfLines={1}>Total Amount</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { textAlign: 'right' }]}
                    value={totalAmount}
                    editable={false}
                    placeholder="0.00"
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                <View style={{ flex: 1.1, minWidth: 95 }}>
                  <ThemedText style={styles.formItemLabel} numberOfLines={1}>Paid Amount</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { textAlign: 'right' }]}
                    value={paidAmount}
                    editable={false}
                    placeholder="0.00"
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                <View style={{ flex: 1.1, minWidth: 95 }}>
                  <ThemedText style={[styles.formItemLabel, { color: '#9E6D08', fontWeight: 'bold' }]} numberOfLines={1}>Balance</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, styles.amountCell, { textAlign: 'right' }]}
                    value={balanceAmount}
                    editable={false}
                    placeholder="0.00"
                    placeholderTextColor="#9E8D77"
                  />
                </View>
              </View>

              {/* Row 3: Payment Inputs (Paymode, Amount, Remarks) */}
              <View style={[styles.paymentInputStrip, { marginTop: 10 }]}>
                <View style={[styles.inlineFieldRow, { flex: 1.1, minWidth: 150 }]}>
                  <ThemedText style={styles.inlineFieldLabel}>Paymode *</ThemedText>
                  <TouchableOpacity
                    style={[styles.paymodeDropdown, { flex: 1 }]}
                    onPress={() => setIsPaymodePickerVisible(true)}
                    activeOpacity={0.8}
                  >
                    <ThemedText style={styles.paymodeDropdownText}>{paymode}</ThemedText>
                    <Ionicons name="chevron-down" size={14} color="#7D6F5B" />
                  </TouchableOpacity>
                </View>

                <View style={[styles.inlineFieldRow, { flex: 1.3, minWidth: 160 }]}>
                  <ThemedText style={styles.inlineFieldLabel}>Amount *</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.amountCell, { textAlign: 'right', flex: 1 }]}
                    value={amountToPay}
                    onChangeText={setAmountToPay}
                    keyboardType="numeric"
                    placeholder="0.00"
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                <View style={[styles.inlineFieldRow, { flex: 2, minWidth: 220 }]}>
                  <ThemedText style={styles.inlineFieldLabel}>Remarks</ThemedText>
                  <TextInput
                    style={[styles.formInput, { flex: 1 }]}
                    value={remarks}
                    onChangeText={setRemarks}
                    placeholder="Cheque / reference details..."
                    placeholderTextColor="#9E8D77"
                  />
                </View>
              </View>
            </View>
          </View>

        </ScrollView>
      </KeyboardAvoidingView>

      {/* CUSTOMER SEARCH MODAL */}
      <Modal visible={isCustomerModalVisible} transparent animationType="fade" onRequestClose={() => setIsCustomerModalVisible(false)}>
        <TouchableOpacity style={styles.modalOverlay} activeOpacity={1} onPress={() => setIsCustomerModalVisible(false)}>
          <TouchableWithoutFeedback>
            <View style={styles.lovModalContainer}>
              <View style={styles.modalHeader}>
                <View style={{ flexDirection: 'row', alignItems: 'center', gap: 6 }}>
                  <View style={styles.headerIconBadgeSmall}>
                    <Ionicons name="person-circle-outline" size={14} color="#B48218" />
                  </View>
                  <ThemedText style={styles.modalTitle}>Customer Lookup</ThemedText>
                </View>
                <TouchableOpacity onPress={() => setIsCustomerModalVisible(false)} style={styles.modalCloseBtn}>
                  <Ionicons name="close" size={20} color="#3B2D17" />
                </TouchableOpacity>
              </View>

              <View style={{ padding: 12 }}>
                <View style={styles.modalSearchBox}>
                  <Ionicons name="search-outline" size={15} color="#8F816E" style={{ marginRight: 6 }} />
                  <TextInput
                    style={styles.modalSearchInput}
                    value={customerSearchQuery}
                    onChangeText={setCustomerSearchQuery}
                    placeholder="Search by code or name..."
                    placeholderTextColor="#8F816E"
                    autoFocus
                  />
                </View>

                <View style={styles.lovHeaderRow}>
                  <ThemedText style={[styles.lovHeaderText, { flex: 1.5 }]}>Code</ThemedText>
                  <ThemedText style={[styles.lovHeaderText, { flex: 2.5 }]}>Name</ThemedText>
                  <ThemedText style={[styles.lovHeaderText, { flex: 3 }]}>Address</ThemedText>
                </View>

                <FlatList
                  data={customerList.filter(c =>
                    (c.CustomerCode || '').toLowerCase().includes(customerSearchQuery.toLowerCase()) ||
                    (c.CustomerName || '').toLowerCase().includes(customerSearchQuery.toLowerCase())
                  )}
                  keyExtractor={(item) => item.CustomerCode}
                  style={{ maxHeight: 320 }}
                  renderItem={({ item, index }) => (
                    <TouchableOpacity
                      style={[
                        styles.lovItemRow,
                        index % 2 === 1 && { backgroundColor: '#FCFAF6' }
                      ]}
                      activeOpacity={0.7}
                      onPress={() => handleSelectCustomer(item)}
                    >
                      <ThemedText style={[styles.lovItemText, styles.codeCell, { flex: 1.5 }]}>
                        {item.CustomerCode}
                      </ThemedText>
                      <ThemedText style={[styles.lovItemText, { flex: 2.5 }]}>
                        {item.CustomerName}
                      </ThemedText>
                      <ThemedText style={[styles.lovItemText, { flex: 3 }]} numberOfLines={1}>
                        {item.Address1 || ''} {item.Address2 || ''}
                      </ThemedText>
                    </TouchableOpacity>
                  )}
                />
              </View>
            </View>
          </TouchableWithoutFeedback>
        </TouchableOpacity>
      </Modal>

      {/* DATE CALENDAR PICKER MODAL */}
      <Modal visible={isCalendarOpen} transparent animationType="fade" onRequestClose={() => setIsCalendarOpen(false)}>
        <TouchableOpacity style={styles.modalOverlay} activeOpacity={1} onPress={() => setIsCalendarOpen(false)}>
          <TouchableWithoutFeedback>
            <View style={styles.calendarModalCard}>
              <View style={styles.modalHeader}>
                <View style={{ flexDirection: 'row', alignItems: 'center', gap: 6 }}>
                  <View style={styles.headerIconBadgeSmall}>
                    <Ionicons name="calendar" size={14} color="#B48218" />
                  </View>
                  <ThemedText style={styles.modalTitle}>Select Date</ThemedText>
                </View>
                <TouchableOpacity onPress={() => setIsCalendarOpen(false)} style={styles.modalCloseBtn}>
                  <Ionicons name="close" size={20} color="#3B2D17" />
                </TouchableOpacity>
              </View>

              <View style={{ padding: 14 }}>
                <View style={styles.calendarNavRow}>
                  <TouchableOpacity style={styles.navArrow} onPress={handlePrevMonth}>
                    <Ionicons name="chevron-back" size={18} color="#3B2D17" />
                  </TouchableOpacity>
                  <ThemedText style={{ fontSize: 13, fontWeight: 'bold', color: '#3B2D17' }}>
                    {monthNames[calendarMonth]} {calendarYear}
                  </ThemedText>
                  <TouchableOpacity style={styles.navArrow} onPress={handleNextMonth}>
                    <Ionicons name="chevron-forward" size={18} color="#3B2D17" />
                  </TouchableOpacity>
                </View>

                <View style={styles.weekdaysRow}>
                  {['S', 'M', 'T', 'W', 'T', 'F', 'S'].map((day, idx) => (
                    <ThemedText key={idx} style={styles.weekdayCell}>{day}</ThemedText>
                  ))}
                </View>

                <View style={styles.daysGrid}>
                  {generateCalendarDays().map((dayInfo, idx) => {
                    let isSelected = false;
                    let currentDateVal = '';
                    if (activeDatePicker === 'payment') currentDateVal = paymentDate;
                    else if (activeDatePicker === 'from') currentDateVal = fromDate;
                    else if (activeDatePicker === 'to') currentDateVal = toDate;

                    if (dayInfo && currentDateVal) {
                      const dateParts = currentDateVal.split('/');
                      if (dateParts.length === 3) {
                        const dayPart = parseInt(dateParts[0]);
                        const monthPart = parseInt(dateParts[1]) - 1;
                        const yearPart = parseInt(dateParts[2]);
                        isSelected = dayInfo.day === dayPart && calendarMonth === monthPart && calendarYear === yearPart;
                      }
                    }

                    return (
                      <TouchableOpacity
                        key={idx}
                        disabled={!dayInfo}
                        style={[
                          styles.dayCell,
                          !dayInfo && styles.emptyCell,
                          isSelected && styles.dayCellSelected
                        ]}
                        onPress={() => dayInfo && handleSelectDate(dayInfo.day)}
                      >
                        <ThemedText style={[styles.dayCellText, isSelected && styles.dayCellTextSelected]}>
                          {dayInfo ? dayInfo.day : ''}
                        </ThemedText>
                      </TouchableOpacity>
                    );
                  })}
                </View>
              </View>
            </View>
          </TouchableWithoutFeedback>
        </TouchableOpacity>
      </Modal>

      {/* PAYMODE PICKER MODAL */}
      <Modal visible={isPaymodePickerVisible} transparent animationType="fade" onRequestClose={() => setIsPaymodePickerVisible(false)}>
        <TouchableOpacity style={styles.modalOverlay} activeOpacity={1} onPress={() => setIsPaymodePickerVisible(false)}>
          <TouchableWithoutFeedback>
            <View style={styles.paymodeModalCard}>
              <View style={styles.modalHeader}>
                <View style={{ flexDirection: 'row', alignItems: 'center', gap: 6 }}>
                  <View style={styles.headerIconBadgeSmall}>
                    <Ionicons name="card" size={14} color="#B48218" />
                  </View>
                  <ThemedText style={styles.modalTitle}>Select Paymode</ThemedText>
                </View>
                <TouchableOpacity onPress={() => setIsPaymodePickerVisible(false)} style={styles.modalCloseBtn}>
                  <Ionicons name="close" size={20} color="#3B2D17" />
                </TouchableOpacity>
              </View>

              <View style={{ padding: 10 }}>
                {['CASH', 'CHEQUE', 'CARD'].map((m) => {
                  const isSelected = paymode === m;
                  return (
                    <TouchableOpacity
                      key={m}
                      style={[styles.paymodeItem, isSelected && styles.paymodeItemSelected]}
                      onPress={() => {
                        setPaymode(m);
                        setIsPaymodePickerVisible(false);
                      }}
                      activeOpacity={0.7}
                    >
                      <ThemedText style={[styles.paymodeItemText, isSelected && styles.paymodeItemTextSelected]}>
                        {m}
                      </ThemedText>
                      {isSelected && <Ionicons name="checkmark-circle" size={18} color="#B48218" />}
                    </TouchableOpacity>
                  );
                })}
              </View>
            </View>
          </TouchableWithoutFeedback>
        </TouchableOpacity>
      </Modal>

      {/* TOAST ALERT */}
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
  safeArea: {
    flex: 1,
    backgroundColor: '#FBF9F5',
  },
  scrollContainer: {
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
  saveBtn: {
    backgroundColor: '#F9F2E2',
    borderColor: '#B8903B',
  },
  saveBtnText: {
    color: '#9E6D08',
    fontSize: 12,
    fontWeight: 'bold',
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
  voucherBadge: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFF8EA',
    borderWidth: 1,
    borderColor: '#D8C59A',
    borderRadius: 12,
    paddingHorizontal: 8,
    paddingVertical: 1,
    gap: 5,
  },
  voucherBadgeLabel: {
    fontSize: 10,
    fontWeight: '600',
    color: '#7D6F5B',
    textTransform: 'uppercase',
  },
  voucherBadgeValue: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#8D6409',
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
  selectedInvoiceBadge: {
    backgroundColor: '#FFF8EA',
    borderWidth: 1,
    borderColor: '#D8C59A',
    borderRadius: 10,
    paddingHorizontal: 8,
    paddingVertical: 1,
  },
  selectedInvoiceBadgeText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#8D6409',
  },
  cardBody: {
    padding: 8,
  },

  // ===== TWO COLUMN & THREE COLUMN GRIDS =====
  twoColumnGrid: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    gap: 10,
  },
  columnHalf: {
    flex: 1,
    minWidth: 320,
    gap: 6,
  },
  threeColumnGrid: {
    flexDirection: 'row',
    gap: 8,
  },
  inlineFieldRow: {
    flex: 1,
    flexDirection: 'row',
    alignItems: 'center',
    gap: 6,
  },
  inlineFieldLabel: {
    fontSize: 11,
    fontWeight: '600',
    color: '#4A3B2C',
    flexShrink: 0,
  },
  gridItem: {
    flex: 1,
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
  formSubLabel: {
    fontSize: 10,
    fontWeight: '600',
    color: '#5C4A28',
    width: 75,
    flexShrink: 0,
  },
  formItemLabel: {
    fontSize: 10,
    fontWeight: '600',
    color: '#5C4A28',
    marginBottom: 2,
  },
  itemLabel: {
    fontSize: 10,
    fontWeight: '600',
    color: '#5C4A28',
    marginBottom: 2,
  },
  inputWithLookup: {
    flexDirection: 'row',
    flex: 1,
    alignItems: 'center',
    minWidth: 0,
  },
  formInput: {
    height: 32,
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderRadius: 5,
    paddingHorizontal: 8,
    paddingVertical: 0,
    fontSize: 12,
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
    width: 32,
    height: 32,
    flexShrink: 0,
    backgroundColor: '#FAF5EA',
    borderWidth: 1,
    borderColor: '#D4C3A3',
    borderRadius: 5,
    marginLeft: 4,
    alignItems: 'center',
    justifyContent: 'center',
  },

  // Subframe Card
  subFrameCard: {
    borderWidth: 1,
    borderColor: '#E8DCB8',
    borderRadius: 6,
    padding: 8,
    backgroundColor: '#FCFAF6',
    marginTop: 4,
  },
  subFrameTitle: {
    fontSize: 10,
    fontWeight: 'bold',
    color: '#8D6409',
    marginBottom: 4,
    letterSpacing: 0.5,
  },
  subFrameContent: {
    gap: 4,
  },

  // Radio selection
  radioGroupRow: {
    flexDirection: 'row',
    gap: 16,
    alignItems: 'center',
    paddingVertical: 4,
  },
  radioItem: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 5,
  },
  radioCircle: {
    width: 14,
    height: 14,
    borderRadius: 7,
    borderWidth: 1.5,
    borderColor: '#C4B69C',
    alignItems: 'center',
    justifyContent: 'center',
  },
  radioCircleSelected: {
    borderColor: '#B8903B',
  },
  radioInner: {
    width: 6,
    height: 6,
    borderRadius: 3,
    backgroundColor: '#B8903B',
  },
  radioLabel: {
    fontSize: 11,
    color: '#3B2D17',
    fontWeight: '500',
  },

  // Fetch Button
  fetchGoldBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    height: 30,
    backgroundColor: '#FAF5EA',
    borderWidth: 1,
    borderColor: '#D4C3A3',
    borderRadius: 6,
    marginTop: 8,
  },
  fetchGoldBtnText: {
    color: '#8D6409',
    fontWeight: 'bold',
    fontSize: 12,
  },

  // Payment Input Strip
  paymentInputStrip: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 8,
    padding: 8,
    backgroundColor: '#FAF6EE',
    borderRadius: 6,
    borderWidth: 1,
    borderColor: '#EADBCA',
  },
  paymodeDropdown: {
    height: 32,
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderRadius: 5,
    paddingHorizontal: 8,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
  },
  paymodeDropdownText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3B2D17',
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
  tableRowSelected: {
    backgroundColor: '#FFF6DF',
    borderLeftWidth: 3,
    borderLeftColor: '#B8903B',
  },
  tableRowText: {
    fontSize: 11,
    color: '#3B2D17',
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
  calendarModalCard: {
    width: 320,
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    overflow: 'hidden',
    shadowColor: '#000',
    shadowOpacity: 0.15,
    shadowRadius: 10,
    elevation: 6,
  },
  paymodeModalCard: {
    width: 280,
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
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
  paymodeItem: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingVertical: 10,
    paddingHorizontal: 12,
    borderRadius: 6,
    marginBottom: 4,
    borderWidth: 1,
    borderColor: '#F0E8D8',
    backgroundColor: '#FCFAF6',
  },
  paymodeItemSelected: {
    backgroundColor: '#FFF8E7',
    borderColor: '#D4C3A3',
  },
  paymodeItemText: {
    fontSize: 13,
    fontWeight: '500',
    color: '#3B2D17',
  },
  paymodeItemTextSelected: {
    fontWeight: 'bold',
    color: '#8A5D00',
  },

  // Calendar
  calendarNavRow: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    marginBottom: 10,
  },
  navArrow: {
    padding: 4,
  },
  weekdaysRow: {
    flexDirection: 'row',
    marginBottom: 6,
  },
  weekdayCell: {
    flex: 1,
    textAlign: 'center',
    fontSize: 11,
    fontWeight: 'bold',
    color: '#8D6409',
  },
  daysGrid: {
    flexDirection: 'row',
    flexWrap: 'wrap',
  },
  dayCell: {
    width: '14.28%',
    height: 30,
    justifyContent: 'center',
    alignItems: 'center',
    borderRadius: 4,
  },
  emptyCell: {
    backgroundColor: 'transparent',
  },
  dayCellSelected: {
    backgroundColor: '#B8903B',
  },
  dayCellText: {
    fontSize: 12,
    color: '#3B2D17',
  },
  dayCellTextSelected: {
    color: '#FFFFFF',
    fontWeight: 'bold',
  },

  // ===== TOAST =====
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
});
