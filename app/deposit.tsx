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
  View
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';

const API_URL = `${API_BASE_URL}/deposit`;
const CUSTOMER_API_URL = `${API_BASE_URL}/customer`;

export default function DepositScreen() {
  const router = useRouter();

  // Mode: "I" (Insert/New), "U" (Update/Add Payment)
  const [mode, setMode] = useState<'I' | 'U'>('I');
  const [loading, setLoading] = useState(false);
  const [toast, setToast] = useState<{ message: string; type: 'success' | 'error' } | null>(null);

  // Form State
  const [depositNo, setDepositNo] = useState('');
  const [customerCode, setCustomerCode] = useState('');
  const [customerName, setCustomerName] = useState('');
  const [address1, setAddress1] = useState('');
  const [address2, setAddress2] = useState('');
  const [address3, setAddress3] = useState('');
  const [depositDate, setDepositDate] = useState('');
  const [paymentDate, setPaymentDate] = useState('');

  const [totalAmount, setTotalAmount] = useState('');
  const [depositAmount, setDepositAmount] = useState('');
  const [amountPaid, setAmountPaid] = useState('');
  const [balanceAmount, setBalanceAmount] = useState('0.00');

  const [inventoryCode, setInventoryCode] = useState('');
  const [inventoryCode1, setInventoryCode1] = useState('');
  const [inventoryCode2, setInventoryCode2] = useState('');

  const [remarks, setRemarks] = useState('');
  const [payMode, setPayMode] = useState('CASH');
  const [salesmanCode, setSalesmanCode] = useState('sys');

  // Next running numbers (for reference)
  const [nextDepositNo, setNextDepositNo] = useState('');
  const [nextDepositPaymentNo, setNextDepositPaymentNo] = useState('');

  // Lookup Modals State
  const [isCustomerModalVisible, setIsCustomerModalVisible] = useState(false);
  const [customerList, setCustomerList] = useState<any[]>([]);
  const [customerSearchQuery, setCustomerSearchQuery] = useState('');

  const [isDepositModalVisible, setIsDepositModalVisible] = useState(false);
  const [depositList, setDepositList] = useState<any[]>([]);
  const [depositSearchQuery, setDepositSearchQuery] = useState('');

  // Calendar State (Dynamic Picker)
  const [activeDatePicker, setActiveDatePicker] = useState<'deposit' | 'payment' | null>(null);
  const [isCalendarOpen, setIsCalendarOpen] = useState(false);
  const [calendarMonth, setCalendarMonth] = useState(new Date().getMonth());
  const [calendarYear, setCalendarYear] = useState(new Date().getFullYear());

  const monthNames = [
    "January", "February", "March", "April", "May", "June",
    "July", "August", "September", "October", "November", "December"
  ];

  const showToast = (message: string, type: 'success' | 'error' = 'success') => {
    setToast({ message, type });
    setTimeout(() => {
      setToast(null);
    }, 3000);
  };

  const showAlert = (title: string, message: string) => {
    if (Platform.OS === 'web') {
      window.alert(`${title}: ${message}`);
    } else {
      Alert.alert(title, message);
    }
  };

  const handleNewCustomer = () => {
    router.push('/customer');
  };

  // 1. Initial Load of configuration
  useEffect(() => {
    const today = new Date();
    const dd = String(today.getDate()).padStart(2, '0');
    const mm = String(today.getMonth() + 1).padStart(2, '0');
    const yyyy = today.getFullYear();
    const formattedToday = `${dd}/${mm}/${yyyy}`;
    setDepositDate(formattedToday);
    setPaymentDate(formattedToday);

    fetchNextNumbers();
  }, []);

  // Compute Balance Amount dynamically when values change
  useEffect(() => {
    const total = parseFloat(totalAmount) || 0;
    const dep = parseFloat(depositAmount) || 0;
    const paid = parseFloat(amountPaid) || 0;
    const balance = total - dep - paid;
    setBalanceAmount(balance.toFixed(2));
  }, [totalAmount, depositAmount, amountPaid]);

  // Fetch next numbers from backend
  const fetchNextNumbers = async () => {
    try {
      const res = await fetch(`${API_URL}/next-numbers`);
      const json = await res.json();
      if (json.success) {
        setNextDepositNo(json.nextDepositNo);
        setNextDepositPaymentNo(json.nextDepositPaymentNo);
        if (mode === 'I') {
          setDepositNo(json.nextDepositNo);
        }
      }
    } catch (err) {
      console.error("Failed to load next numbers:", err);
    }
  };

  // Fetch specific deposit by depositNo
  const handleFetchDeposit = async (selectedNo: string) => {
    if (!selectedNo.trim()) return;
    setLoading(true);
    try {
      const res = await fetch(`${API_URL}/fetch?depositNo=${encodeURIComponent(selectedNo.trim())}`);
      const json = await res.json();
      if (json.success && json.data) {
        const header = json.data;

        setDepositNo(header.DepositNo || '');
        setCustomerCode(header.CustomerCode || '');
        setCustomerName(header.CustomerName || '');
        setInventoryCode(header.InventoryCode || '');
        setInventoryCode1(header.InventoryCode1 || '');
        setInventoryCode2(header.InventoryCode2 || '');
        setRemarks(header.Remarks || '');
        setTotalAmount(header.TotalAmount ? String(header.TotalAmount) : '0.00');

        const prevPayments = parseFloat(header.DepositAmount || 0) + parseFloat(header.AmountPaid || 0);
        setDepositAmount(String(prevPayments));
        setAmountPaid('0.00');

        if (header.DepositDate) {
          const d = new Date(header.DepositDate);
          const dd = String(d.getDate()).padStart(2, '0');
          const mm = String(d.getMonth() + 1).padStart(2, '0');
          const yyyy = d.getFullYear();
          setDepositDate(`${dd}/${mm}/${yyyy}`);
        }

        if (json.customer) {
          setAddress1(json.customer.Address1 || '');
          setAddress2(json.customer.Address2 || '');
          setAddress3(json.customer.Address3 || '');
        }

        setMode('U');
        showToast('Deposit details loaded successfully');
      } else {
        showToast(json.message || 'Deposit not found', 'error');
      }
    } catch (err) {
      showToast('Error fetching deposit details', 'error');
    } finally {
      setLoading(false);
    }
  };

  // Open LOV modal for Customers
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

  // Open LOV modal for Deposits
  const handleOpenDepositLookup = async () => {
    setLoading(true);
    try {
      const res = await fetch(API_URL);
      const json = await res.json();
      if (json.success) {
        setDepositList(json.data);
        setIsDepositModalVisible(true);
      } else {
        showToast('Failed to load deposit list', 'error');
      }
    } catch (err) {
      showToast('Error querying deposit database', 'error');
    } finally {
      setLoading(false);
    }
  };

  // Select Customer from LOV
  const handleSelectCustomer = (cust: any) => {
    setCustomerCode(cust.CustomerCode ? cust.CustomerCode.trim() : '');
    setCustomerName(cust.CustomerName ? cust.CustomerName.trim() : '');
    setAddress1(cust.Address1 ? cust.Address1.trim() : '');
    setAddress2(cust.Address2 ? cust.Address2.trim() : '');
    setAddress3(cust.Address3 ? cust.Address3.trim() : '');
    setIsCustomerModalVisible(false);
  };

  // Form Validation
  const validateForm = () => {
    if (!customerCode.trim()) {
      showAlert('Information', 'Please enter or select the Customer Code');
      return false;
    }
    if (!customerName.trim()) {
      showAlert('Information', 'Please enter the Customer Name');
      return false;
    }
    if (!depositDate.trim()) {
      showAlert('Information', 'Please select the Deposit Date');
      return false;
    }
    const tot = parseFloat(totalAmount) || 0;
    if (tot <= 0) {
      showAlert('Information', 'Please enter a valid Total Amount greater than zero');
      return false;
    }

    if (mode === 'I') {
      const dep = parseFloat(depositAmount) || 0;
      if (dep <= 0) {
        showAlert('Information', 'Please enter the initial Deposit Amount');
        return false;
      }
      if (dep > tot) {
        showAlert('Information', 'Deposit Amount cannot exceed Total Amount');
        return false;
      }
    } else {
      const paid = parseFloat(amountPaid) || 0;
      const dep = parseFloat(depositAmount) || 0;
      if (paid <= 0) {
        showAlert('Information', 'Please enter the Amount Paid');
        return false;
      }
      if (dep + paid > tot) {
        showAlert('Information', 'Total payments exceed total agreement value');
        return false;
      }
    }
    return true;
  };

  // SAVE or UPDATE Operation
  const handleSave = async () => {
    if (!validateForm()) return;

    setLoading(true);
    try {
      const depParts = depositDate.split('/');
      const formattedDepositDate = `${depParts[2]}${depParts[1]}${depParts[0]}`;

      const payParts = paymentDate.split('/');
      const formattedPaymentDate = `${payParts[2]}${payParts[1]}${payParts[0]}`;

      const payload = {
        Mode: mode,
        DepositNo: depositNo.trim(),
        CustomerCode: customerCode.trim(),
        CustomerName: customerName.trim(),
        Address1: address1.trim(),
        Address2: address2.trim(),
        Address3: address3.trim(),
        DepositDate: formattedDepositDate,
        TotalAmount: parseFloat(totalAmount) || 0,
        DepositAmount: parseFloat(depositAmount) || 0,
        AmountPaid: parseFloat(amountPaid) || 0,
        PaymentDate: formattedPaymentDate,
        InventoryCode: inventoryCode.trim(),
        InventoryCode1: inventoryCode1.trim(),
        InventoryCode2: inventoryCode2.trim(),
        Remarks: remarks.trim(),
        PayMode: payMode,
        SalesmanCode: salesmanCode,
        CreateUser: 'pos',
      };

      const res = await fetch(`${API_URL}/save`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload)
      });
      const json = await res.json();
      if (json.success) {
        showToast(json.message || 'Deposit agreement saved successfully');
        handleClear();
      } else {
        showToast(json.message || 'Failed to save deposit', 'error');
      }
    } catch (err) {
      showToast('Network error saving deposit details', 'error');
    } finally {
      setLoading(false);
    }
  };

  // DELETE operation
  const handleDelete = async () => {
    if (!depositNo.trim()) {
      showToast('Please select or enter a Deposit No to delete', 'error');
      return;
    }

    const executeDelete = async () => {
      setLoading(true);
      try {
        const response = await fetch(`${API_URL}?depositNo=${encodeURIComponent(depositNo.trim())}`, {
          method: 'DELETE'
        });
        const json = await response.json();
        if (json.success) {
          showToast('Deposit deleted successfully');
          handleClear();
        } else {
          showToast(json.message || 'Failed to delete deposit', 'error');
        }
      } catch (err) {
        showToast('Network error deleting deposit', 'error');
      } finally {
        setLoading(false);
      }
    };

    if (Platform.OS === 'web') {
      if (window.confirm(`Are you sure you want to delete Deposit Agreement ${depositNo}?`)) {
        executeDelete();
      }
    } else {
      Alert.alert(
        'Confirm Delete',
        `Are you sure you want to delete Deposit Agreement ${depositNo}?`,
        [
          { text: 'Cancel', style: 'cancel' },
          { text: 'Delete', style: 'destructive', onPress: executeDelete }
        ]
      );
    }
  };

  // Clear Form fields
  const handleClear = () => {
    setCustomerCode('');
    setCustomerName('');
    setAddress1('');
    setAddress2('');
    setAddress3('');
    setTotalAmount('');
    setDepositAmount('');
    setAmountPaid('');
    setBalanceAmount('0.00');
    setInventoryCode('');
    setInventoryCode1('');
    setInventoryCode2('');
    setRemarks('');
    setPayMode('CASH');

    const today = new Date();
    const dd = String(today.getDate()).padStart(2, '0');
    const mm = String(today.getMonth() + 1).padStart(2, '0');
    const yyyy = today.getFullYear();
    const formattedToday = `${dd}/${mm}/${yyyy}`;
    setDepositDate(formattedToday);
    setPaymentDate(formattedToday);

    setMode('I');
    fetchNextNumbers();
  };

  const handlePrint = () => {
    if (!depositNo.trim()) {
      showToast('Please load a voucher before printing', 'error');
      return;
    }
    showToast(`Printing Deposit Voucher ${depositNo}...`);
  };

  // Calendar Helpers
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
    const fullDate = `${dd}/${mm}/${yyyy}`;
    if (activeDatePicker === 'deposit') {
      setDepositDate(fullDate);
    } else {
      setPaymentDate(fullDate);
    }
    setIsCalendarOpen(false);
  };

  const openCalendar = (picker: 'deposit' | 'payment') => {
    setActiveDatePicker(picker);
    const currentDateVal = picker === 'deposit' ? depositDate : paymentDate;
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
          <ThemedText style={styles.headerTitle}>Deposit Master</ThemedText>
          {loading && <ActivityIndicator size="small" color="#B48218" style={{ marginLeft: 6 }} />}
        </View>

        <View style={styles.headerRight}>
          <TouchableOpacity
            style={[styles.actionBtn, styles.clearBtn]}
            onPress={handlePrint}
            activeOpacity={0.8}
            disabled={loading}
          >
            <Ionicons name="print-outline" size={14} color="#5C4A28" />
            <ThemedText style={styles.clearBtnText}>Print</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity
            style={[styles.actionBtn, styles.destructiveBtn]}
            onPress={handleDelete}
            activeOpacity={0.8}
            disabled={loading}
          >
            <Ionicons name="trash-outline" size={14} color="#D93829" />
            <ThemedText style={styles.destructiveBtnText}>Delete</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity
            style={[styles.actionBtn, styles.clearBtn]}
            onPress={handleClear}
            activeOpacity={0.8}
            disabled={loading}
          >
            <Ionicons name="refresh-outline" size={14} color="#5C4A28" />
            <ThemedText style={styles.clearBtnText}>Clear</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity
            style={[styles.actionBtn, styles.saveBtn]}
            onPress={handleSave}
            activeOpacity={0.8}
            disabled={loading}
          >
            {loading ? (
              <ActivityIndicator size="small" color="#9E6D08" />
            ) : (
              <>
                <Ionicons name="checkmark-done-outline" size={14} color="#9E6D08" />
                <ThemedText style={styles.saveBtnText}>{mode === 'I' ? 'Save' : 'Update'}</ThemedText>
              </>
            )}
          </TouchableOpacity>
        </View>
      </View>

      <KeyboardAvoidingView behavior={Platform.OS === 'ios' ? 'padding' : undefined} style={{ flex: 1 }}>
        <ScrollView contentContainerStyle={styles.scrollContainer}>
          {/* TOP CARD: DEPOSIT & CUSTOMER DETAILS */}
          <View style={styles.goldCard}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="wallet-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitleSmall}>Deposit Agreement & Customer Information</ThemedText>
              </View>

              <View style={styles.modeBadge}>
                <ThemedText style={styles.modeBadgeText}>
                  {mode === 'I' ? 'New Agreement' : 'Update Payment'}
                </ThemedText>
              </View>
            </View>

            <View style={styles.cardBody}>
              <View style={styles.twoColumnGrid}>
                {/* Column 1: Voucher & Customer Details */}
                <View style={styles.columnHalf}>
                  {/* Deposit No */}
                  <View style={styles.inlineFieldRow}>
                    <ThemedText style={styles.inlineFieldLabel}>Deposit No</ThemedText>
                    <View style={styles.inputWithLookup}>
                      <TextInput
                        style={[styles.formInput, styles.inputAttached, styles.codeCell]}
                        value={depositNo}
                        onChangeText={setDepositNo}
                        placeholder="Voucher No"
                        placeholderTextColor="#9E8D77"
                      />
                      <TouchableOpacity
                        style={styles.attachedLookupBtn}
                        onPress={handleOpenDepositLookup}
                        activeOpacity={0.7}
                      >
                        <Ionicons name="search" size={13} color="#7D5E19" />
                      </TouchableOpacity>
                      <TouchableOpacity
                        style={styles.actionFetchBtn}
                        onPress={() => handleFetchDeposit(depositNo)}
                        activeOpacity={0.7}
                      >
                        <ThemedText style={styles.actionFetchBtnText}>Fetch</ThemedText>
                      </TouchableOpacity>
                    </View>
                  </View>

                  {/* Customer Code */}
                  <View style={styles.inlineFieldRow}>
                    <ThemedText style={styles.inlineFieldLabel}>Cust Code</ThemedText>
                    <View style={styles.inputWithLookup}>
                      <TextInput
                        style={[styles.formInput, styles.inputAttached, styles.codeCell]}
                        value={customerCode}
                        onChangeText={setCustomerCode}
                        placeholder="Code"
                        placeholderTextColor="#9E8D77"
                      />
                      <TouchableOpacity
                        style={styles.attachedLookupBtn}
                        onPress={handleOpenCustomerLookup}
                        activeOpacity={0.7}
                      >
                        <Ionicons name="search" size={13} color="#7D5E19" />
                      </TouchableOpacity>
                      <TouchableOpacity
                        style={styles.actionFetchBtn}
                        onPress={handleNewCustomer}
                        activeOpacity={0.7}
                      >
                        <ThemedText style={styles.actionFetchBtnText}>New</ThemedText>
                      </TouchableOpacity>
                    </View>
                  </View>

                  {/* Customer Name */}
                  <View style={styles.inlineFieldRow}>
                    <ThemedText style={styles.inlineFieldLabel}>Cust Name</ThemedText>
                    <TextInput
                      style={styles.formInput}
                      value={customerName}
                      onChangeText={setCustomerName}
                      placeholder="Customer Name"
                      placeholderTextColor="#9E8D77"
                    />
                  </View>

                  {/* Customer Address */}
                  <View style={styles.inlineFieldRow}>
                    <ThemedText style={styles.inlineFieldLabel}>Address</ThemedText>
                    <TextInput
                      style={[styles.formInput, styles.readonlyInput]}
                      value={[address1, address2, address3].filter(Boolean).join(', ')}
                      editable={false}
                      placeholder="Auto-populated address"
                      placeholderTextColor="#9E8D77"
                      numberOfLines={1}
                    />
                  </View>
                </View>

                {/* Column 2: Dates & PayMode */}
                <View style={styles.columnHalf}>
                  {/* Deposit Date */}
                  <View style={styles.inlineFieldRow}>
                    <ThemedText style={styles.inlineFieldLabel}>Deposit Date</ThemedText>
                    <View style={styles.inputWithLookup}>
                      <TextInput
                        style={[styles.formInput, styles.inputAttached]}
                        value={depositDate}
                        onChangeText={setDepositDate}
                        placeholder="dd/MM/yyyy"
                        placeholderTextColor="#9E8D77"
                      />
                      <TouchableOpacity
                        style={styles.attachedLookupBtn}
                        onPress={() => openCalendar('deposit')}
                        activeOpacity={0.7}
                      >
                        <Ionicons name="calendar-outline" size={14} color="#7D5E19" />
                      </TouchableOpacity>
                    </View>
                  </View>

                  {/* Payment Date */}
                  <View style={styles.inlineFieldRow}>
                    <ThemedText style={styles.inlineFieldLabel}>Pay Date</ThemedText>
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

                  {/* Pay Mode */}
                  <View style={styles.inlineFieldRow}>
                    <ThemedText style={styles.inlineFieldLabel}>Pay Mode</ThemedText>
                    <View style={styles.payModeSelector}>
                      {['CASH', 'NETS', 'VISA', 'AMEX'].map((pm) => (
                        <TouchableOpacity
                          key={pm}
                          style={[
                            styles.payModePill,
                            payMode === pm && styles.payModePillActive
                          ]}
                          onPress={() => setPayMode(pm)}
                          activeOpacity={0.7}
                        >
                          <ThemedText
                            style={[
                              styles.payModePillText,
                              payMode === pm && styles.payModePillTextActive
                            ]}
                          >
                            {pm}
                          </ThemedText>
                        </TouchableOpacity>
                      ))}
                    </View>
                  </View>

                  {/* Salesman Code */}
                  <View style={styles.inlineFieldRow}>
                    <ThemedText style={styles.inlineFieldLabel}>Salesman</ThemedText>
                    <TextInput
                      style={[styles.formInput, styles.readonlyInput]}
                      value={salesmanCode}
                      editable={false}
                      placeholder="Salesman"
                      placeholderTextColor="#9E8D77"
                    />
                  </View>
                </View>
              </View>
            </View>
          </View>

          {/* LOWER SECTION: FINANCIALS & INVENTORY ITEMS */}
          <View style={[styles.twoColumnGrid, { marginTop: 8 }]}>
            {/* Card 2: Financial Details */}
            <View style={[styles.goldCard, { flex: 1, minWidth: 320 }]}>
              <View style={styles.cardHeaderBanner}>
                <View style={styles.headerTitleContainer}>
                  <View style={styles.headerIconBadgeSmall}>
                    <Ionicons name="cash-outline" size={13} color="#B48218" />
                  </View>
                  <ThemedText style={styles.cardHeaderTitleSmall}>Financial Breakdown</ThemedText>
                </View>
              </View>

              <View style={styles.cardBody}>
                {/* Total Amount */}
                <View style={styles.inlineFieldRow}>
                  <ThemedText style={styles.inlineFieldLabel}>Total Amount</ThemedText>
                  <TextInput
                    style={[styles.formInput, { textAlign: 'right' }]}
                    value={totalAmount}
                    onChangeText={setTotalAmount}
                    keyboardType="numeric"
                    placeholder="0.00"
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* Deposit Amount (Initial / Paid) */}
                <View style={styles.inlineFieldRow}>
                  <ThemedText style={styles.inlineFieldLabel}>Deposit Amt</ThemedText>
                  <TextInput
                    style={[
                      styles.formInput,
                      mode === 'U' && styles.readonlyInput,
                      { textAlign: 'right' }
                    ]}
                    value={depositAmount}
                    onChangeText={setDepositAmount}
                    editable={mode === 'I'}
                    keyboardType="numeric"
                    placeholder="0.00"
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* Amount Paid (in Update Mode) */}
                <View style={styles.inlineFieldRow}>
                  <ThemedText style={styles.inlineFieldLabel}>Amount Paid</ThemedText>
                  <TextInput
                    style={[
                      styles.formInput,
                      mode === 'I' && styles.readonlyInput,
                      styles.amountCell,
                      { textAlign: 'right' }
                    ]}
                    value={amountPaid}
                    onChangeText={setAmountPaid}
                    editable={mode === 'U'}
                    keyboardType="numeric"
                    placeholder="0.00"
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* Balance Amount Highlight Banner */}
                <View style={styles.balanceHighlightRow}>
                  <View style={{ flexDirection: 'row', alignItems: 'center', gap: 6 }}>
                    <Ionicons name="calculator-outline" size={15} color="#8D6409" />
                    <ThemedText style={styles.balanceHighlightLabel}>Balance Amount</ThemedText>
                  </View>
                  <ThemedText style={styles.balanceHighlightValue}>${balanceAmount}</ThemedText>
                </View>
              </View>
            </View>

            {/* Card 3: Inventory Items & Remarks */}
            <View style={[styles.goldCard, { flex: 1, minWidth: 320 }]}>
              <View style={styles.cardHeaderBanner}>
                <View style={styles.headerTitleContainer}>
                  <View style={styles.headerIconBadgeSmall}>
                    <Ionicons name="pricetag-outline" size={13} color="#B48218" />
                  </View>
                  <ThemedText style={styles.cardHeaderTitleSmall}>Linked Inventory & Remarks</ThemedText>
                </View>
              </View>

              <View style={styles.cardBody}>
                {/* Inventory Code 1 */}
                <View style={styles.inlineFieldRow}>
                  <ThemedText style={styles.inlineFieldLabel}>Item Code 1</ThemedText>
                  <TextInput
                    style={styles.formInput}
                    value={inventoryCode}
                    onChangeText={setInventoryCode}
                    placeholder="Inventory Code 1"
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* Inventory Code 2 */}
                <View style={styles.inlineFieldRow}>
                  <ThemedText style={styles.inlineFieldLabel}>Item Code 2</ThemedText>
                  <TextInput
                    style={styles.formInput}
                    value={inventoryCode1}
                    onChangeText={setInventoryCode1}
                    placeholder="Inventory Code 2"
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* Inventory Code 3 */}
                <View style={styles.inlineFieldRow}>
                  <ThemedText style={styles.inlineFieldLabel}>Item Code 3</ThemedText>
                  <TextInput
                    style={styles.formInput}
                    value={inventoryCode2}
                    onChangeText={setInventoryCode2}
                    placeholder="Inventory Code 3"
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* Remarks */}
                <View style={[styles.inlineFieldRow, { alignItems: 'flex-start' }]}>
                  <ThemedText style={[styles.inlineFieldLabel, { marginTop: 4 }]}>Remarks</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.multilineInput]}
                    value={remarks}
                    onChangeText={setRemarks}
                    placeholder="Agreement remarks / notes..."
                    placeholderTextColor="#9E8D77"
                    multiline
                    numberOfLines={2}
                  />
                </View>
              </View>
            </View>
          </View>
        </ScrollView>
      </KeyboardAvoidingView>

      {/* 1. Customer Lookup Modal (LOV) */}
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
                    placeholder="Search by Code or Name..."
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

      {/* 2. Deposit Lookup Modal (LOV) */}
      <Modal visible={isDepositModalVisible} transparent animationType="fade" onRequestClose={() => setIsDepositModalVisible(false)}>
        <TouchableOpacity style={styles.modalOverlay} activeOpacity={1} onPress={() => setIsDepositModalVisible(false)}>
          <TouchableWithoutFeedback>
            <View style={styles.lovModalContainer}>
              <View style={styles.modalHeader}>
                <View style={{ flexDirection: 'row', alignItems: 'center', gap: 6 }}>
                  <View style={styles.headerIconBadgeSmall}>
                    <Ionicons name="document-text-outline" size={14} color="#B48218" />
                  </View>
                  <ThemedText style={styles.modalTitle}>Deposit Agreement Lookup</ThemedText>
                </View>
                <TouchableOpacity onPress={() => setIsDepositModalVisible(false)} style={styles.modalCloseBtn}>
                  <Ionicons name="close" size={20} color="#3B2D17" />
                </TouchableOpacity>
              </View>

              <View style={{ padding: 12 }}>
                <View style={styles.modalSearchBox}>
                  <Ionicons name="search-outline" size={15} color="#8F816E" style={{ marginRight: 6 }} />
                  <TextInput
                    style={styles.modalSearchInput}
                    value={depositSearchQuery}
                    onChangeText={setDepositSearchQuery}
                    placeholder="Search by Deposit No or Customer Name..."
                    placeholderTextColor="#8F816E"
                    autoFocus
                  />
                </View>

                <View style={styles.lovHeaderRow}>
                  <ThemedText style={[styles.lovHeaderText, { flex: 2 }]}>Deposit No</ThemedText>
                  <ThemedText style={[styles.lovHeaderText, { flex: 2.5 }]}>Customer Name</ThemedText>
                  <ThemedText style={[styles.lovHeaderText, { flex: 1.5, textAlign: 'right' }]}>Paid Amt</ThemedText>
                  <ThemedText style={[styles.lovHeaderText, { flex: 1.5, textAlign: 'right' }]}>Total Amt</ThemedText>
                </View>

                <FlatList
                  data={depositList.filter(d =>
                    (d.DepositNo || '').toLowerCase().includes(depositSearchQuery.toLowerCase()) ||
                    (d.CustomerName || '').toLowerCase().includes(depositSearchQuery.toLowerCase())
                  )}
                  keyExtractor={(item) => item.DepositNo}
                  style={{ maxHeight: 320 }}
                  renderItem={({ item, index }) => (
                    <TouchableOpacity
                      style={[
                        styles.lovItemRow,
                        index % 2 === 1 && { backgroundColor: '#FCFAF6' }
                      ]}
                      activeOpacity={0.7}
                      onPress={() => {
                        handleFetchDeposit(item.DepositNo);
                        setIsDepositModalVisible(false);
                      }}
                    >
                      <ThemedText style={[styles.lovItemText, styles.codeCell, { flex: 2 }]}>
                        {item.DepositNo}
                      </ThemedText>
                      <ThemedText style={[styles.lovItemText, { flex: 2.5 }]}>
                        {item.CustomerName}
                      </ThemedText>
                      <ThemedText style={[styles.lovItemText, styles.amountCell, { flex: 1.5, textAlign: 'right' }]}>
                        ${Number(item.DepositAmount || 0).toFixed(2)}
                      </ThemedText>
                      <ThemedText style={[styles.lovItemText, { flex: 1.5, textAlign: 'right' }]}>
                        ${Number(item.TotalAmount || 0).toFixed(2)}
                      </ThemedText>
                    </TouchableOpacity>
                  )}
                />
              </View>
            </View>
          </TouchableWithoutFeedback>
        </TouchableOpacity>
      </Modal>

      {/* 3. Calendar Picker Modal */}
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
                    const currentDateVal = activeDatePicker === 'deposit' ? depositDate : paymentDate;
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
    paddingHorizontal: 10,
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
  destructiveBtn: {
    backgroundColor: '#FFFFFF',
    borderColor: '#F2A8A2',
  },
  destructiveBtnText: {
    color: '#D93829',
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
  modeBadge: {
    backgroundColor: '#FFF8EA',
    borderWidth: 1,
    borderColor: '#D8C59A',
    borderRadius: 10,
    paddingHorizontal: 8,
    paddingVertical: 1,
  },
  modeBadgeText: {
    fontSize: 10,
    fontWeight: 'bold',
    color: '#8D6409',
  },
  cardBody: {
    padding: 10,
  },

  // ===== TWO COLUMN LAYOUT =====
  twoColumnGrid: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    gap: 16,
  },
  columnHalf: {
    flex: 1,
    minWidth: 320,
    gap: 6,
  },

  // ===== FORM ROWS & INPUTS =====
  inlineFieldRow: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingVertical: 3,
    borderBottomWidth: 1,
    borderBottomColor: '#F7F2E9',
    gap: 8,
  },
  inlineFieldLabel: {
    fontSize: 11,
    fontWeight: '600',
    color: '#4A3B2C',
    width: 90,
    flexShrink: 0,
  },
  payModeSelector: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 4,
    flex: 1,
  },
  payModePill: {
    paddingHorizontal: 8,
    paddingVertical: 3,
    borderRadius: 4,
    borderWidth: 1,
    borderColor: '#DFD5C3',
    backgroundColor: '#FFFFFF',
  },
  payModePillActive: {
    backgroundColor: '#F9F2E2',
    borderColor: '#B8903B',
  },
  payModePillText: {
    fontSize: 10,
    fontWeight: '600',
    color: '#7D6F5B',
  },
  payModePillTextActive: {
    color: '#9E6D08',
    fontWeight: 'bold',
  },
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
    width: 100,
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
  actionFetchBtn: {
    height: 26,
    backgroundColor: '#FAF5EA',
    borderWidth: 1,
    borderColor: '#D4C3A3',
    borderRadius: 5,
    paddingHorizontal: 8,
    marginLeft: 4,
    alignItems: 'center',
    justifyContent: 'center',
  },
  actionFetchBtnText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#8D6409',
  },
  multilineInput: {
    height: 64,
    paddingTop: 4,
    paddingBottom: 4,
    textAlignVertical: 'top',
  },

  // Balance Highlight
  balanceHighlightRow: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    backgroundColor: '#FFF8EA',
    borderWidth: 1,
    borderColor: '#B8903B',
    borderRadius: 6,
    paddingHorizontal: 12,
    paddingVertical: 8,
    marginTop: 6,
  },
  balanceHighlightLabel: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#8D6409',
    textTransform: 'uppercase',
  },
  balanceHighlightValue: {
    fontSize: 16,
    fontWeight: 'bold',
    color: '#9E6D08',
  },

  amountCell: {
    fontWeight: 'bold',
    color: '#9E6D08',
  },
  codeCell: {
    fontWeight: 'bold',
    color: '#8D6409',
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
