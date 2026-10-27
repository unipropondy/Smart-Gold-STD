import { ThemedText } from '@/components/themed-text';
import { API_BASE_URL } from '@/constants/api';
import { useThemeColors } from '@/hooks/useThemeColors';
import { Ionicons } from '@expo/vector-icons';
import { Stack, useRouter } from 'expo-router';
import React, { useEffect, useState, useRef } from 'react';
import {
  ActivityIndicator,
  FlatList,
  KeyboardAvoidingView,
  Modal,
  Platform,
  ScrollView,
  StyleSheet,
  TextInput,
  TouchableOpacity,
  View,
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';

const API_URL = `${API_BASE_URL}/installmentpayment`;
const CUSTOMER_API_URL = `${API_BASE_URL}/customer`;
const FLEXI_API_URL = `${API_BASE_URL}/paymententry/flexi`;

export default function InstallmentPaymentScreen() {
  const themeColors = useThemeColors();
  const router = useRouter();

  const [loading, setLoading] = useState(false);
  const [toast, setToast] = useState<{ message: string; type: 'success' | 'error' } | null>(null);

  // Form State
  const [agreementNo, setAgreementNo] = useState('');
  const [customerCode, setCustomerCode] = useState('');
  const [flexiId, setFlexiId] = useState('');
  const [customerName, setCustomerName] = useState('');
  const [address, setAddress] = useState('');
  const [remarks, setRemarks] = useState('');

  // Payment Calculation State
  const [noOfInstallment, setNoOfInstallment] = useState('1');
  const [installmentAmount, setInstallmentAmount] = useState('0.00');
  const [totalAmount, setTotalAmount] = useState('0.00');

  // Footer Totals
  const [salesAmount, setSalesAmount] = useState('0.00');
  const [depositAmount, setDepositAmount] = useState('0.00');
  const [balanceAmount, setBalanceAmount] = useState('0.00');

  // Trade-in
  const [tradeInOption, setTradeInOption] = useState<'Gold' | 'Cash' | 'Both'>('Gold');

  // History
  const [historyList, setHistoryList] = useState<any[]>([]);

  // LOV State
  const [isAgreementModalVisible, setIsAgreementModalVisible] = useState(false);
  const [agreementList, setAgreementList] = useState<any[]>([]);
  const [agreementSearchQuery, setAgreementSearchQuery] = useState('');

  const [isCustomerModalVisible, setIsCustomerModalVisible] = useState(false);
  const [customerList, setCustomerList] = useState<any[]>([]);
  const [customerSearchQuery, setCustomerSearchQuery] = useState('');

  const [isFlexiModalVisible, setIsFlexiModalVisible] = useState(false);
  const [flexiList, setFlexiList] = useState<any[]>([]);
  const [flexiSearchQuery, setFlexiSearchQuery] = useState('');

  const showToast = (message: string, type: 'success' | 'error' = 'success') => {
    setToast({ message, type });
    setTimeout(() => setToast(null), 3000);
  };

  const fetchAgreement = async () => {
    if (!agreementNo) return;
    setLoading(true);
    try {
      const res = await fetch(`${API_URL}/agreement/${encodeURIComponent(agreementNo)}`);
      const json = await res.json();
      if (json.success && json.data) {
        const data = json.data;
        const totalCost = data.TotalCost || 0;
        const depAmt = data.DepositAmt || 0;
        const insAmt = data.InstallmentAmt || 0;
        const amtPaid = data.AmtPaid || 0;

        if (amtPaid >= totalCost && totalCost > 0) {
          showToast('Installment Amount was already fully paid!', 'error');
          handleClear();
          return;
        }

        setCustomerCode(data.CustomerCode || '');
        setCustomerName(data.Name || '');
        setAddress(`${data.Address1 || ''} ${data.Address2 || ''}`.trim());

        setSalesAmount(totalCost.toFixed(2));
        setDepositAmount(depAmt.toFixed(2));
        setInstallmentAmount(insAmt.toFixed(2));

        // Fetch Balance
        fetchBalance(agreementNo, totalCost, depAmt);
        fetchHistory(agreementNo);
        showToast('Agreement details loaded', 'success');
      } else {
        showToast('Agreement not found', 'error');
      }
    } catch (err) {
      showToast('Error fetching agreement', 'error');
    } finally {
      setLoading(false);
    }
  };

  const fetchBalance = async (agrNo: string, totalCost: number, depositAmt: number) => {
    try {
      const res = await fetch(`${API_URL}/balance/${encodeURIComponent(agrNo)}`);
      const json = await res.json();
      if (json.success) {
        const amountPaid = json.data.AmountPaid || 0;
        const bal = totalCost - depositAmt - amountPaid;
        setBalanceAmount(bal > 0 ? bal.toFixed(2) : '0.00');
      }
    } catch (err) {
      console.error(err);
    }
  };

  const fetchHistory = async (agrNo: string) => {
    try {
      const res = await fetch(`${API_URL}/installment-details/${encodeURIComponent(agrNo)}`);
      const json = await res.json();
      if (json.success) {
        setHistoryList(json.data || []);
      }
    } catch (err) {
      console.error(err);
    }
  };

  const handleOpenAgreementLookup = async () => {
    setLoading(true);
    try {
      const res = await fetch(`${API_URL}/agreements`);
      const json = await res.json();
      if (json.success) {
        setAgreementList(json.data || []);
        setIsAgreementModalVisible(true);
      } else {
        showToast('Failed to load agreements', 'error');
      }
    } catch (err) {
      showToast('Error querying agreements', 'error');
    } finally {
      setLoading(false);
    }
  };

  const handleSelectAgreement = (agr: any) => {
    setAgreementNo(agr.InsAgreementNo);
    setIsAgreementModalVisible(false);
  };

  const handleOpenCustomerLookup = async () => {
    setLoading(true);
    try {
      const res = await fetch(CUSTOMER_API_URL);
      const json = await res.json();
      if (json.success) {
        setCustomerList(json.data || []);
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

  const handleSelectCustomer = (cust: any) => {
    setCustomerCode(cust.CustomerCode ? cust.CustomerCode.trim() : '');
    setCustomerName(cust.CustomerName ? cust.CustomerName.trim() : '');
    setAddress(`${cust.Address1 || ''} ${cust.Address2 || ''}`.trim());
    setIsCustomerModalVisible(false);
  };

  const handleOpenFlexiLookup = async () => {
    setLoading(true);
    try {
      const res = await fetch(FLEXI_API_URL);
      const json = await res.json();
      if (json.success) {
        setFlexiList(json.data || []);
        setIsFlexiModalVisible(true);
      } else {
        showToast('Failed to load Flexi list', 'error');
      }
    } catch (err) {
      showToast('Error querying Flexi directory', 'error');
    } finally {
      setLoading(false);
    }
  };

  const handleSelectFlexi = (flexi: any) => {
    setFlexiId(flexi.SubscriptionID ? String(flexi.SubscriptionID).trim() : '');
    setIsFlexiModalVisible(false);
  };

  // Trigger fetch when agreementNo is selected from LOV
  useEffect(() => {
    if (agreementNo) {
      fetchAgreement();
    }
  }, [agreementNo]);

  // Recalculate total amount when inputs change
  useEffect(() => {
    const qty = parseInt(noOfInstallment) || 0;
    const amt = parseFloat(installmentAmount) || 0;
    setTotalAmount((qty * amt).toFixed(2));
  }, [noOfInstallment, installmentAmount]);

  const handleClear = () => {
    setAgreementNo('');
    setCustomerCode('');
    setFlexiId('');
    setCustomerName('');
    setAddress('');
    setRemarks('');
    setNoOfInstallment('1');
    setInstallmentAmount('0.00');
    setTotalAmount('0.00');
    setSalesAmount('0.00');
    setDepositAmount('0.00');
    setBalanceAmount('0.00');
    setHistoryList([]);
  };

  const latestState = useRef({ agreementNo, totalAmount, noOfInstallment, installmentAmount, customerCode });
  useEffect(() => {
    latestState.current = { agreementNo, totalAmount, noOfInstallment, installmentAmount, customerCode };
  }, [agreementNo, totalAmount, noOfInstallment, installmentAmount, customerCode]);

  const handleSave = async () => {
    const currentState = latestState.current;

    if (!currentState.agreementNo || currentState.agreementNo.trim() === '') {
      showToast('Please enter the Installment Agreement No!', 'error');
      return;
    }

    const qty = parseFloat(currentState.noOfInstallment);
    if (isNaN(qty) || qty <= 0) {
      showToast('No. of Installment should be greater than zero!', 'error');
      return;
    }

    const amt = parseFloat(currentState.installmentAmount);
    if (isNaN(amt) || amt <= 0) {
      showToast('Installment Amount should be greater than zero!', 'error');
      return;
    }

    const tot = parseFloat(currentState.totalAmount) || 0;
    if (tot <= 0) {
      showToast('Total amount must be greater than zero!', 'error');
      return;
    }

    const balanceLeft = parseFloat(balanceAmount) || 0;
    if (balanceLeft < 0) {
      showToast('Total Installment Amount exceeds the balance Amount!', 'error');
      return;
    }

    setLoading(true);
    try {
      const payload = {
        agreementNo: currentState.agreementNo,
        installmentNo: `IN-${Date.now()}`,
        noOfInstallment: currentState.noOfInstallment,
        installmentAmount: currentState.installmentAmount,
        totalAmount: currentState.totalAmount,
        customerCode: currentState.customerCode,
      };

      router.push({
        pathname: '/payingmode',
        params: {
          totalNet: currentState.totalAmount,
          customerCode: currentState.customerCode,
          apiEndpoint: `${API_URL}/pay`,
          payload: JSON.stringify(payload),
          returnPath: '/installmentpayment',
        },
      });

      handleClear();
    } catch (err: any) {
      showToast(err.message || 'Error navigating to payment', 'error');
    } finally {
      setLoading(false);
    }
  };

  return (
    <SafeAreaView style={styles.safeArea} edges={['top']}>
      <Stack.Screen options={{ headerShown: false }} />

      {/* LUXURY ROYAL GOLD HEADER BAR */}
      <View style={styles.headerBar}>
        <View style={styles.headerLeft}>
          <TouchableOpacity style={styles.backBtn} onPress={() => router.back()} activeOpacity={0.7}>
            <Ionicons name="arrow-back" size={20} color="#3B2D17" />
          </TouchableOpacity>
          <View>
            <ThemedText style={styles.headerTitle}>Installment Payment</ThemedText>
            <ThemedText style={styles.headerSubtitle}>Customer Payment Receipt & Ledger</ThemedText>
          </View>
          {loading && <ActivityIndicator size="small" color="#B48218" style={{ marginLeft: 8 }} />}
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

          <TouchableOpacity
            style={[styles.actionBtn, styles.reprintBtn]}
            onPress={() => showToast('RePrinting receipt...', 'success')}
            activeOpacity={0.8}
          >
            <Ionicons name="print-outline" size={14} color="#5C4A28" />
            <ThemedText style={styles.clearBtnText}>RePrint</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity
            style={[styles.actionBtn, styles.saveBtn]}
            onPress={handleSave}
            activeOpacity={0.8}
            disabled={loading}
          >
            <Ionicons name="checkmark-done-outline" size={15} color="#9E6D08" />
            <ThemedText style={styles.saveBtnText}>Pay & Save</ThemedText>
          </TouchableOpacity>
        </View>
      </View>

      {/* TOAST ALERTS */}
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

      <KeyboardAvoidingView behavior={Platform.OS === 'ios' ? 'padding' : undefined} style={{ flex: 1 }}>
        <ScrollView contentContainerStyle={styles.scrollContainer} keyboardShouldPersistTaps="handled">
          {/* CARD 1: CUSTOMER & AGREEMENT DETAILS */}
          <View style={styles.goldCard}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="card-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitleSmall}>Installment Agreement Search</ThemedText>
              </View>

              {agreementNo ? (
                <View style={styles.agreementBadge}>
                  <ThemedText style={styles.agreementBadgeLabel}>Agreement:</ThemedText>
                  <ThemedText style={styles.agreementBadgeValue}>{agreementNo}</ThemedText>
                </View>
              ) : null}
            </View>

            <View style={styles.cardBody}>
              {/* Row 1: Agreement No, Cust Code, Flexi ID */}
              <View style={styles.threeColumnGrid}>
                {/* Agreement No */}
                <View style={styles.inlineFieldRow}>
                  <ThemedText style={styles.inlineFieldLabel}>Agreement No *</ThemedText>
                  <View style={styles.inputWithLookup}>
                    <TextInput
                      style={[styles.formInput, styles.inputAttached, { flex: 1 }]}
                      value={agreementNo}
                      onChangeText={setAgreementNo}
                      onBlur={fetchAgreement}
                      placeholder="Search Agreement..."
                      placeholderTextColor="#9E8D77"
                    />
                    <TouchableOpacity
                      style={styles.attachedLookupBtn}
                      onPress={handleOpenAgreementLookup}
                      activeOpacity={0.7}
                    >
                      <Ionicons name="search" size={13} color="#7D5E19" />
                    </TouchableOpacity>
                  </View>
                </View>

                {/* Cust Code */}
                <View style={styles.inlineFieldRow}>
                  <ThemedText style={styles.inlineFieldLabel}>Cust Code</ThemedText>
                  <View style={styles.inputWithLookup}>
                    <TextInput
                      style={[styles.formInput, styles.inputAttached, { flex: 1 }]}
                      value={customerCode}
                      onChangeText={setCustomerCode}
                      placeholder="Customer Code..."
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

                {/* Flexi ID */}
                <View style={styles.inlineFieldRow}>
                  <ThemedText style={styles.inlineFieldLabel}>Flexi ID</ThemedText>
                  <View style={styles.inputWithLookup}>
                    <TextInput
                      style={[styles.formInput, styles.inputAttached, { flex: 1 }]}
                      value={flexiId}
                      onChangeText={setFlexiId}
                      placeholder="Flexi ID..."
                      placeholderTextColor="#9E8D77"
                    />
                    <TouchableOpacity
                      style={styles.attachedLookupBtn}
                      onPress={handleOpenFlexiLookup}
                      activeOpacity={0.7}
                    >
                      <Ionicons name="search" size={13} color="#7D5E19" />
                    </TouchableOpacity>
                  </View>
                </View>
              </View>

              {/* Row 2: Customer Name, Address, Trade-In */}
              <View style={[styles.threeColumnGrid, { marginTop: 6 }]}>
                {/* Customer Name */}
                <View style={styles.inlineFieldRow}>
                  <ThemedText style={styles.inlineFieldLabel}>Customer Name</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1 }]}
                    value={customerName}
                    editable={false}
                    placeholder="Auto-populated"
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* Address */}
                <View style={styles.inlineFieldRow}>
                  <ThemedText style={styles.inlineFieldLabel}>Address</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1 }]}
                    value={address}
                    editable={false}
                    placeholder="Auto-populated"
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* Trade-In Options */}
                <View style={styles.inlineFieldRow}>
                  <ThemedText style={styles.inlineFieldLabel}>Trade-In</ThemedText>
                  <View style={styles.tradeInOptionsGroup}>
                    {['Gold', 'Cash', 'Both'].map((opt) => (
                      <TouchableOpacity
                        key={opt}
                        style={[styles.pillOption, tradeInOption === opt && styles.pillOptionSelected]}
                        onPress={() => setTradeInOption(opt as any)}
                        activeOpacity={0.7}
                      >
                        <Ionicons
                          name={tradeInOption === opt ? 'radio-button-on' : 'radio-button-off'}
                          size={13}
                          color={tradeInOption === opt ? '#9E6D08' : '#7A6855'}
                        />
                        <ThemedText
                          style={[styles.pillOptionText, tradeInOption === opt && styles.pillOptionTextSelected]}
                        >
                          {opt}
                        </ThemedText>
                      </TouchableOpacity>
                    ))}

                    {tradeInOption !== 'Cash' && (
                      <TouchableOpacity
                        style={styles.tradeInGoBtn}
                        onPress={() => router.push('/tradeinsales')}
                        activeOpacity={0.8}
                      >
                        <ThemedText style={styles.tradeInGoBtnText}>Go</ThemedText>
                        <Ionicons name="arrow-forward" size={11} color="#9E6D08" />
                      </TouchableOpacity>
                    )}
                  </View>
                </View>
              </View>

              {/* Row 3: No of Inst, Inst Amount, Total Amount */}
              <View style={[styles.threeColumnGrid, { marginTop: 6 }]}>
                {/* No of Installment */}
                <View style={styles.inlineFieldRow}>
                  <ThemedText style={styles.inlineFieldLabel}>No of Inst *</ThemedText>
                  <TextInput
                    style={[styles.formInput, { flex: 1, textAlign: 'right' }]}
                    value={noOfInstallment}
                    onChangeText={setNoOfInstallment}
                    keyboardType="numeric"
                    placeholder="1"
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* Installment Amount */}
                <View style={styles.inlineFieldRow}>
                  <ThemedText style={styles.inlineFieldLabel}>Inst Amount *</ThemedText>
                  <TextInput
                    style={[styles.formInput, { flex: 1, textAlign: 'right' }]}
                    value={installmentAmount}
                    onChangeText={setInstallmentAmount}
                    keyboardType="numeric"
                    placeholder="0.00"
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* Total Amount to Pay */}
                <View style={styles.inlineFieldRow}>
                  <ThemedText style={styles.inlineFieldLabel}>Total Amount ($)</ThemedText>
                  <TextInput
                    style={[
                      styles.formInput,
                      styles.readonlyInput,
                      styles.highlightTotal,
                      { flex: 1, textAlign: 'right' },
                    ]}
                    value={totalAmount}
                    editable={false}
                  />
                </View>
              </View>
            </View>
          </View>

          {/* CARD 2: INSTALLMENT HISTORY DATA TABLE (FULL WIDTH) */}
          <View style={[styles.goldCard, { marginTop: 10 }]}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="time-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitleSmall}>Installment Payment History</ThemedText>
                <View style={styles.counterBadge}>
                  <ThemedText style={styles.counterBadgeText}>{historyList.length} records</ThemedText>
                </View>
              </View>
            </View>

            {/* Table Header (100% Full Width) */}
            <View style={styles.tableHeadRow}>
              <ThemedText style={[styles.tableHeadCell, { flex: 1.8 }]}>Installment No</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 1.4 }]}>Date</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 1.2, textAlign: 'right' }]}>No Of Ins</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 1.3, textAlign: 'right' }]}>
                Installment Amt ($)
              </ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 1.3, textAlign: 'right' }]}>
                Amt Paid ($)
              </ThemedText>
            </View>

            {/* Table Body */}
            {historyList.length === 0 ? (
              <View style={styles.emptyTableContainer}>
                <Ionicons name="receipt-outline" size={28} color="#D4C3A3" />
                <ThemedText style={styles.emptyTableText}>
                  No installment history found for this agreement.
                </ThemedText>
              </View>
            ) : (
              historyList.map((item, index) => (
                <View key={index} style={styles.tableRow}>
                  <ThemedText style={[styles.tableCell, styles.codeCell, { flex: 1.8 }]}>
                    {item.InstallmentNo}
                  </ThemedText>
                  <ThemedText style={[styles.tableCell, { flex: 1.4 }]}>
                    {item.CreateDate ? new Date(item.CreateDate).toLocaleDateString() : ''}
                  </ThemedText>
                  <ThemedText style={[styles.tableCell, { flex: 1.2, textAlign: 'right' }]}>
                    {item.InsPaid}
                  </ThemedText>
                  <ThemedText style={[styles.tableCell, { flex: 1.3, textAlign: 'right' }]}>
                    {item.InstallmentAmt}
                  </ThemedText>
                  <ThemedText style={[styles.tableCell, styles.amountCell, { flex: 1.3, textAlign: 'right' }]}>
                    {item.AmtPaid}
                  </ThemedText>
                </View>
              ))
            )}
          </View>

          {/* CARD 3: FINANCIAL LEDGER TOTALS */}
          <View style={[styles.goldCard, { marginTop: 10 }]}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="calculator-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitleSmall}>Financial Ledger Balance</ThemedText>
              </View>
            </View>

            <View style={styles.cardBody}>
              <View style={styles.threeColumnGrid}>
                <View style={styles.inlineFieldRow}>
                  <ThemedText style={styles.inlineFieldLabel}>Sales Amount</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1, textAlign: 'right' }]}
                    value={salesAmount}
                    editable={false}
                  />
                </View>

                <View style={styles.inlineFieldRow}>
                  <ThemedText style={styles.inlineFieldLabel}>Deposit Amount</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1, textAlign: 'right' }]}
                    value={depositAmount}
                    editable={false}
                  />
                </View>

                <View style={styles.inlineFieldRow}>
                  <ThemedText style={styles.inlineFieldLabel}>Balance Amount</ThemedText>
                  <TextInput
                    style={[
                      styles.formInput,
                      styles.readonlyInput,
                      styles.highlightTotal,
                      { flex: 1, textAlign: 'right' },
                    ]}
                    value={balanceAmount}
                    editable={false}
                  />
                </View>
              </View>
            </View>
          </View>
        </ScrollView>
      </KeyboardAvoidingView>

      {/* AGREEMENT LOOKUP MODAL */}
      <Modal visible={isAgreementModalVisible} transparent animationType="fade">
        <View style={styles.modalOverlay}>
          <View style={styles.lovModalCard}>
            <View style={styles.modalHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="search" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.modalHeaderTitle}>Search Agreement</ThemedText>
              </View>
              <TouchableOpacity
                onPress={() => setIsAgreementModalVisible(false)}
                activeOpacity={0.7}
                style={styles.closeModalBtn}
              >
                <Ionicons name="close" size={18} color="#3B2D17" />
              </TouchableOpacity>
            </View>

            <View style={styles.modalSearchBox}>
              <Ionicons name="search-outline" size={16} color="#8D6409" style={{ marginRight: 6 }} />
              <TextInput
                style={styles.modalSearchInput}
                value={agreementSearchQuery}
                onChangeText={setAgreementSearchQuery}
                placeholder="Search by Agreement No or Customer..."
                placeholderTextColor="#9E8D77"
                autoFocus
              />
              {agreementSearchQuery.length > 0 && (
                <TouchableOpacity onPress={() => setAgreementSearchQuery('')}>
                  <Ionicons name="close-circle" size={16} color="#9E8D77" />
                </TouchableOpacity>
              )}
            </View>

            <View style={styles.lovHeadRow}>
              <ThemedText style={[styles.lovHeadText, { flex: 1.8 }]}>Agreement No</ThemedText>
              <ThemedText style={[styles.lovHeadText, { flex: 1.3 }]}>Cust Code</ThemedText>
              <ThemedText style={[styles.lovHeadText, { flex: 2 }]}>Customer Name</ThemedText>
            </View>

            <FlatList
              data={agreementList.filter(
                (a) =>
                  (a.InsAgreementNo || '').toLowerCase().includes(agreementSearchQuery.toLowerCase()) ||
                  (a.Name || '').toLowerCase().includes(agreementSearchQuery.toLowerCase()) ||
                  (a.CustomerCode || '').toLowerCase().includes(agreementSearchQuery.toLowerCase())
              )}
              keyExtractor={(item, index) => item.InsAgreementNo || index.toString()}
              initialNumToRender={50}
              maxToRenderPerBatch={50}
              ListEmptyComponent={
                <View style={styles.emptyTableContainer}>
                  <Ionicons name="search-outline" size={24} color="#D4C3A3" />
                  <ThemedText style={styles.emptyTableText}>No matching agreements found.</ThemedText>
                </View>
              }
              renderItem={({ item }) => (
                <TouchableOpacity
                  style={styles.lovItemRow}
                  onPress={() => handleSelectAgreement(item)}
                  activeOpacity={0.7}
                >
                  <ThemedText style={[styles.lovItemText, styles.codeCell, { flex: 1.8 }]}>
                    {item.InsAgreementNo}
                  </ThemedText>
                  <ThemedText style={[styles.lovItemText, { flex: 1.3 }]}>{item.CustomerCode}</ThemedText>
                  <ThemedText style={[styles.lovItemText, { flex: 2 }]} numberOfLines={1}>
                    {item.Name || ''}
                  </ThemedText>
                </TouchableOpacity>
              )}
            />
          </View>
        </View>
      </Modal>

      {/* CUSTOMER LOOKUP MODAL */}
      <Modal visible={isCustomerModalVisible} transparent animationType="fade">
        <View style={styles.modalOverlay}>
          <View style={styles.lovModalCard}>
            <View style={styles.modalHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="person-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.modalHeaderTitle}>Search Customer</ThemedText>
              </View>
              <TouchableOpacity
                onPress={() => setIsCustomerModalVisible(false)}
                activeOpacity={0.7}
                style={styles.closeModalBtn}
              >
                <Ionicons name="close" size={18} color="#3B2D17" />
              </TouchableOpacity>
            </View>

            <View style={styles.modalSearchBox}>
              <Ionicons name="search-outline" size={16} color="#8D6409" style={{ marginRight: 6 }} />
              <TextInput
                style={styles.modalSearchInput}
                value={customerSearchQuery}
                onChangeText={setCustomerSearchQuery}
                placeholder="Search by Code, Name, or Address..."
                placeholderTextColor="#9E8D77"
                autoFocus
              />
              {customerSearchQuery.length > 0 && (
                <TouchableOpacity onPress={() => setCustomerSearchQuery('')}>
                  <Ionicons name="close-circle" size={16} color="#9E8D77" />
                </TouchableOpacity>
              )}
            </View>

            <View style={styles.lovHeadRow}>
              <ThemedText style={[styles.lovHeadText, { flex: 1.4 }]}>Code</ThemedText>
              <ThemedText style={[styles.lovHeadText, { flex: 2 }]}>Name</ThemedText>
              <ThemedText style={[styles.lovHeadText, { flex: 2.4 }]}>Address</ThemedText>
            </View>

            <FlatList
              data={customerList.filter(
                (c) =>
                  (c.CustomerCode || '').toLowerCase().includes(customerSearchQuery.toLowerCase()) ||
                  (c.CustomerName || '').toLowerCase().includes(customerSearchQuery.toLowerCase())
              )}
              keyExtractor={(item, index) => item.CustomerCode || index.toString()}
              initialNumToRender={50}
              maxToRenderPerBatch={50}
              ListEmptyComponent={
                <View style={styles.emptyTableContainer}>
                  <Ionicons name="search-outline" size={24} color="#D4C3A3" />
                  <ThemedText style={styles.emptyTableText}>No matching customers found.</ThemedText>
                </View>
              }
              renderItem={({ item }) => (
                <TouchableOpacity
                  style={styles.lovItemRow}
                  onPress={() => handleSelectCustomer(item)}
                  activeOpacity={0.7}
                >
                  <ThemedText style={[styles.lovItemText, styles.codeCell, { flex: 1.4 }]}>
                    {item.CustomerCode}
                  </ThemedText>
                  <ThemedText style={[styles.lovItemText, { flex: 2 }]} numberOfLines={1}>
                    {item.CustomerName}
                  </ThemedText>
                  <ThemedText style={[styles.lovItemText, { flex: 2.4 }]} numberOfLines={1}>
                    {item.Address1 || ''} {item.Address2 || ''}
                  </ThemedText>
                </TouchableOpacity>
              )}
            />
          </View>
        </View>
      </Modal>

      {/* FLEXI LOOKUP MODAL */}
      <Modal visible={isFlexiModalVisible} transparent animationType="fade">
        <View style={styles.modalOverlay}>
          <View style={styles.lovModalCard}>
            <View style={styles.modalHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="finger-print-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.modalHeaderTitle}>Search Flexi ID</ThemedText>
              </View>
              <TouchableOpacity
                onPress={() => setIsFlexiModalVisible(false)}
                activeOpacity={0.7}
                style={styles.closeModalBtn}
              >
                <Ionicons name="close" size={18} color="#3B2D17" />
              </TouchableOpacity>
            </View>

            <View style={styles.modalSearchBox}>
              <Ionicons name="search-outline" size={16} color="#8D6409" style={{ marginRight: 6 }} />
              <TextInput
                style={styles.modalSearchInput}
                value={flexiSearchQuery}
                onChangeText={setFlexiSearchQuery}
                placeholder="Search by Subscription ID or NRIC..."
                placeholderTextColor="#9E8D77"
                autoFocus
              />
              {flexiSearchQuery.length > 0 && (
                <TouchableOpacity onPress={() => setFlexiSearchQuery('')}>
                  <Ionicons name="close-circle" size={16} color="#9E8D77" />
                </TouchableOpacity>
              )}
            </View>

            <View style={styles.lovHeadRow}>
              <ThemedText style={[styles.lovHeadText, { flex: 1.5 }]}>Sub ID</ThemedText>
              <ThemedText style={[styles.lovHeadText, { flex: 2 }]}>NRIC</ThemedText>
              <ThemedText style={[styles.lovHeadText, { flex: 1.5 }]}>Type</ThemedText>
            </View>

            <FlatList
              data={flexiList.filter(
                (f) =>
                  (f.SubscriptionID || '').toString().toLowerCase().includes(flexiSearchQuery.toLowerCase()) ||
                  (f.NricNo || '').toLowerCase().includes(flexiSearchQuery.toLowerCase())
              )}
              keyExtractor={(item, index) =>
                item.SubscriptionID ? item.SubscriptionID.toString() : index.toString()
              }
              initialNumToRender={50}
              maxToRenderPerBatch={50}
              ListEmptyComponent={
                <View style={styles.emptyTableContainer}>
                  <Ionicons name="search-outline" size={24} color="#D4C3A3" />
                  <ThemedText style={styles.emptyTableText}>No matching Flexi records found.</ThemedText>
                </View>
              }
              renderItem={({ item }) => (
                <TouchableOpacity
                  style={styles.lovItemRow}
                  onPress={() => handleSelectFlexi(item)}
                  activeOpacity={0.7}
                >
                  <ThemedText style={[styles.lovItemText, styles.codeCell, { flex: 1.5 }]}>
                    {item.SubscriptionID}
                  </ThemedText>
                  <ThemedText style={[styles.lovItemText, { flex: 2 }]}>{item.NricNo}</ThemedText>
                  <ThemedText style={[styles.lovItemText, { flex: 1.5 }]}>{item.FlexiType}</ThemedText>
                </TouchableOpacity>
              )}
            />
          </View>
        </View>
      </Modal>
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  safeArea: {
    flex: 1,
    backgroundColor: '#FBF9F5',
  },
  scrollContainer: {
    paddingHorizontal: 12,
    paddingTop: 8,
    paddingBottom: 24,
  },

  // ===== ROYAL GOLD HEADER BAR =====
  headerBar: {
    height: 48,
    backgroundColor: '#FFFFFF',
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingHorizontal: 14,
    borderBottomWidth: 1,
    borderBottomColor: '#E8DCB8',
    elevation: 2,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.05,
    shadowRadius: 2,
  },
  headerLeft: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  backBtn: {
    padding: 6,
    marginRight: 8,
    borderRadius: 6,
    backgroundColor: '#F9F6F0',
    borderWidth: 1,
    borderColor: '#E8DCB8',
  },
  headerTitle: {
    fontSize: 15,
    fontWeight: 'bold',
    color: '#3B2D17',
    letterSpacing: 0.3,
  },
  headerSubtitle: {
    fontSize: 10,
    color: '#8D6409',
    fontWeight: '500',
    marginTop: -1,
  },
  headerRight: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 8,
  },
  actionBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 12,
    paddingVertical: 5,
    borderRadius: 6,
    borderWidth: 1,
  },
  clearBtn: {
    backgroundColor: '#FFFFFF',
    borderColor: '#DFD5C3',
  },
  clearBtnText: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#5C4A28',
    marginLeft: 4,
  },
  reprintBtn: {
    backgroundColor: '#FFFFFF',
    borderColor: '#DFD5C3',
  },
  saveBtn: {
    backgroundColor: '#F9F2E2',
    borderColor: '#B8903B',
  },
  saveBtnText: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#9E6D08',
    marginLeft: 4,
  },

  // ===== CARDS & BANNERS =====
  goldCard: {
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    overflow: 'hidden',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.04,
    shadowRadius: 3,
    elevation: 2,
  },
  cardHeaderBanner: {
    backgroundColor: '#F5E7CA',
    paddingVertical: 6,
    paddingHorizontal: 10,
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
  },
  headerTitleContainer: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  headerIconBadgeSmall: {
    width: 20,
    height: 20,
    borderRadius: 4,
    backgroundColor: '#FFFFFF',
    justifyContent: 'center',
    alignItems: 'center',
    marginRight: 6,
    borderWidth: 1,
    borderColor: '#E2D3B3',
  },
  cardHeaderTitleSmall: {
    fontSize: 13,
    fontWeight: 'bold',
    color: '#3B2D17',
    letterSpacing: 0.2,
  },
  cardBody: {
    padding: 10,
  },
  agreementBadge: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFF4DA',
    paddingHorizontal: 8,
    paddingVertical: 2,
    borderRadius: 4,
    borderWidth: 1,
    borderColor: '#B8903B',
  },
  agreementBadgeLabel: {
    fontSize: 10,
    color: '#7D5E19',
    marginRight: 4,
  },
  agreementBadgeValue: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#8A5D00',
  },

  // ===== THREE COLUMN GRID =====
  threeColumnGrid: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    gap: 10,
  },
  inlineFieldRow: {
    flex: 1,
    minWidth: 260,
    flexDirection: 'row',
    alignItems: 'center',
  },
  inlineFieldLabel: {
    width: 105,
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3B2D17',
    marginRight: 4,
  },
  formInput: {
    height: 26,
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#D4C3A3',
    borderRadius: 4,
    paddingHorizontal: 6,
    paddingVertical: 2,
    fontSize: 12,
    color: '#2B2118',
  },
  readonlyInput: {
    backgroundColor: '#F5EFE6',
    color: '#5C4A28',
  },
  highlightTotal: {
    backgroundColor: '#FFF4DA',
    color: '#8A5D00',
    fontWeight: 'bold',
    borderColor: '#D4C3A3',
  },

  // Input with attached lookup button
  inputWithLookup: {
    flex: 1,
    flexDirection: 'row',
    alignItems: 'center',
  },
  inputAttached: {
    borderTopRightRadius: 0,
    borderBottomRightRadius: 0,
  },
  attachedLookupBtn: {
    height: 26,
    width: 26,
    backgroundColor: '#F5E7CA',
    borderWidth: 1,
    borderLeftWidth: 0,
    borderColor: '#D4C3A3',
    borderTopRightRadius: 4,
    borderBottomRightRadius: 4,
    justifyContent: 'center',
    alignItems: 'center',
  },

  // Trade In Options
  tradeInOptionsGroup: {
    flex: 1,
    flexDirection: 'row',
    alignItems: 'center',
    gap: 4,
  },
  pillOption: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FCFAF6',
    borderWidth: 1,
    borderColor: '#E2D3B3',
    borderRadius: 4,
    paddingVertical: 3,
    paddingHorizontal: 6,
    gap: 3,
  },
  pillOptionSelected: {
    backgroundColor: '#FFF4DA',
    borderColor: '#B8903B',
  },
  pillOptionText: {
    fontSize: 11,
    fontWeight: '500',
    color: '#5C4A28',
  },
  pillOptionTextSelected: {
    color: '#9E6D08',
    fontWeight: 'bold',
  },
  tradeInGoBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFF1D6',
    borderWidth: 1,
    borderColor: '#B8903B',
    borderRadius: 4,
    paddingHorizontal: 6,
    height: 22,
    gap: 2,
  },
  tradeInGoBtnText: {
    fontSize: 10,
    fontWeight: 'bold',
    color: '#9E6D08',
  },

  // ===== TABLE SECTION =====
  counterBadge: {
    backgroundColor: '#FFFFFF',
    paddingHorizontal: 6,
    paddingVertical: 2,
    borderRadius: 10,
    marginLeft: 6,
    borderWidth: 1,
    borderColor: '#E2D3B3',
  },
  counterBadgeText: {
    fontSize: 10,
    fontWeight: 'bold',
    color: '#8D6409',
  },
  tableHeadRow: {
    flexDirection: 'row',
    backgroundColor: '#F5E7CA',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    paddingVertical: 6,
    paddingHorizontal: 8,
    width: '100%',
  },
  tableHeadCell: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3B2D17',
    paddingHorizontal: 4,
  },
  tableRow: {
    flexDirection: 'row',
    alignItems: 'center',
    borderBottomWidth: 1,
    borderBottomColor: '#F3EBDD',
    paddingVertical: 6,
    paddingHorizontal: 8,
    backgroundColor: '#FFFFFF',
    width: '100%',
  },
  tableCell: {
    fontSize: 11,
    color: '#3B2D17',
    paddingHorizontal: 4,
  },
  codeCell: {
    fontFamily: Platform.OS === 'ios' ? 'Courier' : 'monospace',
    fontWeight: 'bold',
    color: '#8A5D00',
  },
  amountCell: {
    fontWeight: 'bold',
    color: '#1B5E20',
  },
  emptyTableContainer: {
    paddingVertical: 24,
    alignItems: 'center',
    justifyContent: 'center',
    width: '100%',
  },
  emptyTableText: {
    fontSize: 12,
    color: '#9E8D77',
    marginTop: 4,
  },

  // ===== MODALS =====
  modalOverlay: {
    flex: 1,
    backgroundColor: 'rgba(0, 0, 0, 0.45)',
    justifyContent: 'center',
    alignItems: 'center',
    padding: 16,
  },
  lovModalCard: {
    width: '92%',
    maxWidth: 600,
    maxHeight: '80%',
    backgroundColor: '#FFFFFF',
    borderRadius: 10,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    overflow: 'hidden',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.15,
    shadowRadius: 8,
    elevation: 8,
  },
  modalHeaderBanner: {
    backgroundColor: '#F5E7CA',
    paddingVertical: 8,
    paddingHorizontal: 12,
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
  },
  modalHeaderTitle: {
    fontSize: 13,
    fontWeight: 'bold',
    color: '#3B2D17',
  },
  closeModalBtn: {
    padding: 3,
  },
  modalSearchBox: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FCFAF6',
    margin: 8,
    paddingHorizontal: 8,
    height: 32,
    borderWidth: 1,
    borderColor: '#D4C3A3',
    borderRadius: 6,
  },
  modalSearchInput: {
    flex: 1,
    fontSize: 12,
    color: '#2B2118',
    paddingVertical: 0,
  },
  lovHeadRow: {
    flexDirection: 'row',
    backgroundColor: '#F8F2E4',
    paddingVertical: 5,
    paddingHorizontal: 10,
    borderBottomWidth: 1,
    borderBottomColor: '#E8DCB8',
  },
  lovHeadText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#8D6409',
  },
  lovItemRow: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingVertical: 8,
    paddingHorizontal: 10,
    borderBottomWidth: 1,
    borderBottomColor: '#F3EBDD',
  },
  lovItemText: {
    fontSize: 11,
    color: '#3B2D17',
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
