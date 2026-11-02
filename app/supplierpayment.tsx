/**
 * Developer: durairaj
 */

import { ThemedText } from '@/components/themed-text';
import { API_BASE_URL } from '@/constants/api';
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
  TextInput,
  TouchableOpacity,
  TouchableWithoutFeedback,
  View
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';

const API_URL = `${API_BASE_URL}`;

const BANK_OPTIONS = ['OCBC', 'IB', 'DBS', 'UOB', 'STD'];
const PAYMODE_OPTIONS = ['CASH', 'GOLD BAR', 'CHEQUE', 'TRADE-IN', 'BANK TRANSFER', 'TT'];

export default function SupplierPaymentScreen() {
  const router = useRouter();

  // ===== HEADER & FILTER STATE =====
  const [paymentNo, setPaymentNo] = useState('');
  const [paymentDate, setPaymentDate] = useState(new Date().toISOString().split('T')[0]);

  const [supplierCode, setSupplierCode] = useState('');
  const [supplierName, setSupplierName] = useState('');
  const [headInvoiceNo, setHeadInvoiceNo] = useState('');

  // Date selection states (Defaulting to today, matching frmSupplierPayment_Load)
  const [fromDate, setFromDate] = useState(new Date().toISOString().split('T')[0]);
  const [toDate, setToDate] = useState(new Date().toISOString().split('T')[0]);

  const [paidStatus, setPaidStatus] = useState<'UnPaid' | 'Paid' | 'All'>('UnPaid');
  const [customerCopy, setCustomerCopy] = useState(false);

  // ===== GRID STATE =====
  const [payments, setPayments] = useState<any[]>([]);
  const [selectedIndex, setSelectedIndex] = useState<number | null>(null);

  // ===== SELECTED JOBSHEET DETAIL STATE =====
  const [jobsheetNo, setJobsheetNo] = useState('');
  const [selectedInvoiceNo, setSelectedInvoiceNo] = useState('');
  const [invoiceDate, setInvoiceDate] = useState('');
  const [totalAmount, setTotalAmount] = useState('');
  const [paidAmount, setPaidAmount] = useState('');
  const [balanceAmount, setBalanceAmount] = useState('');

  // ===== PAYMENT SETTLEMENT ENTRY STATE =====
  const [paymode, setPaymode] = useState('CASH');
  const [bank, setBank] = useState('OCBC');
  const [amount, setAmount] = useState('');
  const [remarks, setRemarks] = useState('');
  const [issueNo, setIssueNo] = useState('');

  // Loading & Action States
  const [saving, setSaving] = useState(false);
  const [loading, setLoading] = useState(false);
  const [toast, setToast] = useState<{ message: string; type: 'success' | 'error' | 'info' } | null>(null);

  // LOV State
  const [lovVisible, setLovVisible] = useState(false);
  const [lovTitle, setLovTitle] = useState('');
  const [lovData, setLovData] = useState<any[]>([]);
  const [filteredLovData, setFilteredLovData] = useState<any[]>([]);
  const [lovSearch, setLovSearch] = useState('');
  const [lovColumns, setLovColumns] = useState<{ key: string; label: string; flex?: number }[]>([]);
  const [onLovSelect, setOnLovSelect] = useState<((item: any) => void) | null>(null);

  // Voucher / RePrint Modal State
  const [voucherModalVisible, setVoucherModalVisible] = useState(false);
  const [voucherData, setVoucherData] = useState<any[]>([]);
  const [voucherLoading, setVoucherLoading] = useState(false);

  const showToast = (message: string, type: 'success' | 'error' | 'info' = 'success') => {
    setToast({ message, type });
    setTimeout(() => {
      setToast(null);
    }, 3200);
  };

  useEffect(() => {
    fetchNextPaymentNo();
  }, []);

  const fetchNextPaymentNo = async () => {
    try {
      const res = await fetch(`${API_URL}/supplierpayment/nextNo`);
      const json = await res.json();
      if (json.success && json.nextNo) {
        setPaymentNo(json.nextNo);
      }
    } catch (e) {
      console.error('Failed to load payment no', e);
    }
  };

  const openLov = (
    title: string,
    data: any[],
    columns: { key: string; label: string; flex?: number }[],
    onSelect: (item: any) => void
  ) => {
    setLovTitle(title);
    setLovData(data);
    setFilteredLovData(data);
    setLovColumns(columns);
    setLovSearch('');
    setOnLovSelect(() => onSelect);
    setLovVisible(true);
  };

  const handleLovSearch = (text: string) => {
    setLovSearch(text);
    if (!text.trim()) {
      setFilteredLovData(lovData);
      return;
    }
    const lower = text.toLowerCase();
    const filtered = lovData.filter(item =>
      Object.values(item).some(val => String(val).toLowerCase().includes(lower))
    );
    setFilteredLovData(filtered);
  };

  const handleLovSelect = (item: any) => {
    if (onLovSelect) onLovSelect(item);
    setLovVisible(false);
  };

  // Supplier Search LOV matching btnSupplierCodeSearch_Click
  const fetchSuppliers = async () => {
    setLoading(true);
    try {
      const res = await fetch(`${API_URL}/supplierpayment/lov/supplier`);
      const json = await res.json();
      if (json.success && json.data) {
        openLov(
          'Select Supplier',
          json.data,
          [
            { key: 'SupplierCode', label: 'Supplier Code', flex: 1.5 },
            { key: 'SupplierName', label: 'Supplier Name', flex: 2.5 },
            { key: 'Address1', label: 'Address', flex: 2 }
          ],
          item => {
            setSupplierCode(item.SupplierCode);
            setSupplierName(item.SupplierName);
          }
        );
      } else {
        showToast(json.message || 'Failed to fetch suppliers', 'error');
      }
    } catch (e) {
      showToast('Error connecting to server', 'error');
    } finally {
      setLoading(false);
    }
  };

  // Supplier Code validation matching subGetSupplier on LostFocus
  const handleSupplierBlur = async () => {
    if (!supplierCode.trim()) {
      setSupplierName('');
      return;
    }
    try {
      const res = await fetch(`${API_URL}/supplierpayment/supplier/${encodeURIComponent(supplierCode.trim())}`);
      const json = await res.json();
      if (json.success && json.data) {
        setSupplierCode(json.data.SupplierCode);
        setSupplierName(json.data.SupplierName);
      } else {
        showToast('Invalid supplier code!', 'error');
        setSupplierCode('');
        setSupplierName('');
      }
    } catch (e) {
      console.warn('Supplier check error:', e);
    }
  };

  // Issue No Search matching btnIssueNo_Click in frmSupplierPayment.vb
  const fetchIssueNo = async () => {
    setLoading(true);
    try {
      if (paymode === 'GOLD BAR') {
        const res = await fetch(`${API_URL}/supplierpayment/lov/goldbarissue?SupplierCode=${encodeURIComponent(supplierCode.trim())}`);
        const json = await res.json();
        const data = json.success ? json.data : [];
        if (data.length === 0) {
          showToast('No GoldBar Issue records found', 'info');
          return;
        }
        openLov(
          'Select GoldBar Issue No',
          data,
          [
            { key: 'IssueNo', label: 'Issue No', flex: 1.5 },
            { key: 'PDate', label: 'Date', flex: 1.2 },
            { key: 'SupplierCode', label: 'Supplier', flex: 1.2 },
            { key: 'IssuedWeight', label: 'Issued Wt', flex: 1.2 }
          ],
          item => {
            setIssueNo(item.IssueNo);
            loadIssueDetail('GOLD BAR', item.IssueNo);
          }
        );
      } else if (paymode === 'TRADE-IN') {
        const res = await fetch(`${API_URL}/supplierpayment/lov/tradeinissue?SupplierCode=${encodeURIComponent(supplierCode.trim())}`);
        const json = await res.json();
        const data = json.success ? json.data : [];
        if (data.length === 0) {
          showToast('No Trade-In Issue records found', 'info');
          return;
        }
        openLov(
          'Select Trade-In Issue No',
          data,
          [
            { key: 'TradeIssueNo', label: 'Trade Issue No', flex: 1.5 },
            { key: 'TradeIssueDate', label: 'Date', flex: 1.2 },
            { key: 'SupplierCode', label: 'Supplier', flex: 1.2 },
            { key: 'TotalWeight', label: 'Total Wt', flex: 1.2 }
          ],
          item => {
            setIssueNo(item.TradeIssueNo);
            loadIssueDetail('TRADE-IN', item.TradeIssueNo);
          }
        );
      }
    } catch (e) {
      showToast('Error fetching issue records', 'error');
    } finally {
      setLoading(false);
    }
  };

  // Load Issue Details matching subGetIssueDetail & subGetTradeInIssueDetail
  const loadIssueDetail = async (type: 'GOLD BAR' | 'TRADE-IN', no: string) => {
    try {
      const res = await fetch(`${API_URL}/supplierpayment/issueDetail?Type=${encodeURIComponent(type)}&IssueNo=${encodeURIComponent(no)}`);
      const json = await res.json();
      if (json.success && json.data) {
        if (json.data.Remarks !== undefined) setRemarks(String(json.data.Remarks));
        if (json.data.Amount !== undefined) setAmount(Number(json.data.Amount).toFixed(2));
      }
    } catch (e) {
      console.error('Failed to load issue detail:', e);
    }
  };

  // Fetch Jobsheets matching cmdFetch_Click / subLoadJobSheet()
  const handleFetch = async () => {
    if (!supplierCode.trim()) {
      showToast('Supplier Code is required', 'error');
      return;
    }

    setLoading(true);
    try {
      const formattedFrom = fromDate.replace(/-/g, '');
      const formattedTo = toDate.replace(/-/g, '');
      const invFilter = headInvoiceNo.trim() ? `&InvoiceNo=${encodeURIComponent(headInvoiceNo.trim())}` : '';

      const res = await fetch(
        `${API_URL}/supplierpayment/fetch?SupplierCode=${encodeURIComponent(supplierCode.trim())}&FromDate=${formattedFrom}&ToDate=${formattedTo}&PaidStatus=${paidStatus}${invFilter}`
      );
      const json = await res.json();
      if (json.success) {
        setPayments(json.data);
        if (json.data.length === 0) {
          showToast('No jobsheets found for selected criteria', 'info');
        } else {
          showToast(`Loaded ${json.data.length} records`, 'success');
        }
      } else {
        showToast(json.message || 'Failed to fetch details', 'error');
      }
    } catch (e) {
      showToast('Server connection failed', 'error');
    } finally {
      setLoading(false);
    }
  };

  // Select Row matching dgvSupplier_Click / subSetJobsheet
  const handleSelectJobsheet = (item: any, idx: number) => {
    setSelectedIndex(idx);
    setJobsheetNo(item.JobSheetNo || '');
    setSelectedInvoiceNo(item.PurchaseBillNo || '');
    setHeadInvoiceNo(item.PurchaseBillNo || '');
    setInvoiceDate(item.PurchaseBillDate || '');
    setTotalAmount(item.TotalCost !== undefined && item.TotalCost !== null ? Number(item.TotalCost).toFixed(2) : '0.00');
    setPaidAmount(item.PaidAmount !== undefined && item.PaidAmount !== null ? Number(item.PaidAmount).toFixed(2) : '0.00');
    setBalanceAmount(item.BalanceAmount !== undefined && item.BalanceAmount !== null ? Number(item.BalanceAmount).toFixed(2) : '0.00');

    // Auto-fill amount with remaining balance if available
    const bal = parseFloat(item.BalanceAmount) || 0;
    if (bal > 0 && !amount) {
      setAmount(bal.toFixed(2));
    }
  };

  // Clear All matching SubClearall()
  const handleClearAll = () => {
    setJobsheetNo('');
    setSelectedInvoiceNo('');
    setHeadInvoiceNo('');
    setInvoiceDate('');
    setTotalAmount('');
    setPaidAmount('');
    setBalanceAmount('');
    setPayments([]);
    setSelectedIndex(null);
    setAmount('');
    setRemarks('');
    setSupplierCode('');
    setSupplierName('');
    setIssueNo('');
    setFromDate(new Date().toISOString().split('T')[0]);
    setToDate(new Date().toISOString().split('T')[0]);
    setPaidStatus('UnPaid');
    setCustomerCopy(false);
    fetchNextPaymentNo();
    showToast('Cleared All', 'info');
  };

  // Partial Clear after Save matching SubClear()
  const handlePartialClear = () => {
    setJobsheetNo('');
    setSelectedInvoiceNo('');
    setInvoiceDate('');
    setTotalAmount('');
    setPaidAmount('');
    setBalanceAmount('');
    setAmount('');
    setRemarks('');
    setHeadInvoiceNo('');
    setIssueNo('');
    setPaidStatus('UnPaid');
    fetchNextPaymentNo();
  };

  // Update Payment matching cmdUpdate_Click in frmSupplierPayment.vb
  const handleUpdate = async () => {
    // 1. Validation Rules matching VB.NET
    if (paymode === 'CHEQUE' && !remarks.trim()) {
      showToast('Please Enter the Cheque No', 'error');
      return;
    }
    if (paymode === 'GOLD BAR' && !remarks.trim()) {
      showToast('Please Enter the Bar Weight', 'error');
      return;
    }
    if (paymode === 'TRADE-IN' && !remarks.trim()) {
      showToast('Please Enter the TradeIn Weight', 'error');
      return;
    }
    if (!paymode.trim()) {
      showToast('Please Enter the Paymode', 'error');
      return;
    }
    const valAmt = parseFloat(amount) || 0;
    if (valAmt <= 0) {
      showToast('Please Enter the Amount', 'error');
      return;
    }
    if (!selectedInvoiceNo.trim()) {
      showToast('Please Select The Invoice details to update', 'error');
      return;
    }

    setSaving(true);
    try {
      const res = await fetch(`${API_URL}/supplierpayment`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          PaymentNo: paymentNo,
          PaymentDate: paymentDate.replace(/-/g, ''),
          PaymentAmount: valAmt,
          Remarks: remarks.trim(),
          InvoiceNo: selectedInvoiceNo.trim(),
          InvoiceDate: invoiceDate,
          SupplierCode: supplierCode.trim(),
          Paymode: paymode,
          JobSheetNo: jobsheetNo.trim(),
          Bank: paymode === 'CHEQUE' ? bank : ''
        })
      });

      const json = await res.json();
      if (json.success) {
        showToast('Payment Successful', 'success');
        // Refresh grid matching cmdFetch_Click in VB
        await handleFetch();
        // Clear transaction form matching SubClear() in VB
        handlePartialClear();
    } else {
      const serverErr = json.error ? `: ${json.error}` : '';
      const errMsg = `${json.message || 'Transaction failed'}${serverErr}`;
      showToast(errMsg, 'error');
    }
    } catch (e) {
      showToast('Server connection failed', 'error');
    } finally {
      setSaving(false);
    }
  };

  // RePrint matching btnReprint_Click in frmSupplierPayment.vb
  const handleReprint = async () => {
    const targetInvoice = headInvoiceNo.trim() || selectedInvoiceNo.trim();
    if (!targetInvoice) {
      showToast('Please select or enter an Invoice No to reprint', 'error');
      return;
    }

    setVoucherLoading(true);
    setVoucherModalVisible(true);
    try {
      const res = await fetch(
        `${API_URL}/supplierpayment/voucher/${encodeURIComponent(targetInvoice)}?SupplierCode=${encodeURIComponent(supplierCode.trim())}`
      );
      const json = await res.json();
      if (json.success && json.data) {
        setVoucherData(json.data);
      } else {
        setVoucherData([]);
        showToast('No payment voucher records found', 'info');
      }
    } catch (e) {
      showToast('Error loading voucher data', 'error');
    } finally {
      setVoucherLoading(false);
    }
  };

  const RadioButton = ({ label, selected, onPress }: { label: string; selected: boolean; onPress: () => void }) => (
    <TouchableOpacity style={styles.radioContainer} onPress={onPress} activeOpacity={0.7}>
      <View style={styles.radioCircle}>
        {selected && <View style={styles.radioInner} />}
      </View>
      <ThemedText style={styles.radioLabel}>{label}</ThemedText>
    </TouchableOpacity>
  );

  const Checkbox = ({ label, selected, onPress }: { label: string; selected: boolean; onPress: () => void }) => (
    <TouchableOpacity style={styles.checkboxContainer} onPress={onPress} activeOpacity={0.7}>
      <View style={[styles.checkboxSquare, selected && styles.checkboxSquareActive]}>
        {selected && <Ionicons name="checkmark" size={11} color="#FFFFFF" />}
      </View>
      {label ? <ThemedText style={styles.radioLabel}>{label}</ThemedText> : null}
    </TouchableOpacity>
  );

  return (
    <SafeAreaView edges={['top']} style={styles.safeArea}>
      <Stack.Screen options={{ headerShown: false }} />

      {/* Top Header Bar */}
      <View style={styles.header}>
        <View style={styles.headerLeft}>
          <TouchableOpacity onPress={() => router.back()} style={styles.backButton}>
            <Ionicons name="arrow-back" size={20} color="#3B2D17" />
          </TouchableOpacity>
          <ThemedText style={styles.headerTitle}>Supplier Payment</ThemedText>
        </View>

        <View style={styles.headerActions}>
          <TouchableOpacity style={styles.headerBtnSecondary} onPress={handleReprint}>
            <Ionicons name="print-outline" size={14} color="#7D6F5B" style={{ marginRight: 4 }} />
            <ThemedText style={styles.headerBtnSecondaryText}>RePrint</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity style={styles.headerBtnSecondary} onPress={handleClearAll}>
            <Ionicons name="refresh-outline" size={14} color="#7D6F5B" style={{ marginRight: 4 }} />
            <ThemedText style={styles.headerBtnSecondaryText}>Clear</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity
            style={[styles.headerBtnPrimary, saving && { opacity: 0.7 }]}
            onPress={handleUpdate}
            disabled={saving}
          >
            {saving ? (
              <ActivityIndicator size="small" color="#9E6D08" />
            ) : (
              <>
                <Ionicons name="checkmark-circle-outline" size={14} color="#7D5800" style={{ marginRight: 4 }} />
                <ThemedText style={styles.headerBtnPrimaryText}>Update</ThemedText>
              </>
            )}
          </TouchableOpacity>


        </View>
      </View>

      <ScrollView style={styles.container} contentContainerStyle={styles.contentContainer}>
        {/* TOP SECTION: Payment & Filter Details Card */}
        <View style={styles.goldCard}>
          <View style={styles.cardHeaderBanner}>
            <View style={{ flexDirection: 'row', alignItems: 'center' }}>
              <View style={styles.headerIconBadge}>
                <Ionicons name="card" size={14} color="#B48218" />
              </View>
              <ThemedText style={styles.cardHeaderTitle}>Payment Header & Filter</ThemedText>
            </View>

            <TouchableOpacity style={styles.fetchBtn} onPress={handleFetch} disabled={loading}>
              <Ionicons name="search" size={13} color="#9E6D08" style={{ marginRight: 4 }} />
              <ThemedText style={styles.fetchBtnText}>{loading ? 'Fetching...' : 'Fetch'}</ThemedText>
            </TouchableOpacity>
          </View>

          <View style={styles.cardBody}>
            {/* Row 1: Payment No, Payment Date, Supplier Code & Name */}
            <View style={styles.row}>
              {/* Payment No */}
              <View style={{ flexDirection: 'row', alignItems: 'center', width: 230, marginRight: 16 }}>
                <ThemedText style={styles.label}>Payment No</ThemedText>
                <TextInput
                  style={[styles.input, styles.readonlyInput, { flex: 1 }]}
                  value={paymentNo}
                  editable={false}
                />
              </View>

              {/* Payment Date */}
              <View style={{ flexDirection: 'row', alignItems: 'center', width: 240, marginRight: 16 }}>
                <ThemedText style={styles.label}>Payment Date</ThemedText>
                <View style={{ flex: 1 }}>
                  {Platform.OS === 'web' ? (
                    <input
                      type="date"
                      style={{
                        width: '100%',
                        border: '1px solid #E2D7C5',
                        borderRadius: '6px',
                        padding: '4px 8px',
                        fontSize: '12px',
                        height: '28px',
                        color: '#2C2519',
                        backgroundColor: '#FFFFFF',
                        outline: 'none',
                        boxSizing: 'border-box',
                      }}
                      value={paymentDate}
                      onChange={(e: any) => setPaymentDate(e.target.value || '')}
                    />
                  ) : (
                    <TextInput
                      style={styles.input}
                      value={paymentDate}
                      onChangeText={setPaymentDate}
                      placeholder="YYYY-MM-DD"
                    />
                  )}
                </View>
              </View>

              {/* Supplier Code */}
              <View style={{ flexDirection: 'row', alignItems: 'center', flex: 1, minWidth: 260 }}>
                <ThemedText style={styles.label}>Supplier</ThemedText>
                <View style={{ flexDirection: 'row', alignItems: 'center', marginRight: 12 }}>
                  <TextInput
                    style={[
                      styles.input,
                      {
                        width: 95,
                        minWidth: 0,
                        borderTopRightRadius: 0,
                        borderBottomRightRadius: 0,
                        borderRightWidth: 0,
                      },
                    ]}
                    value={supplierCode}
                    onChangeText={setSupplierCode}
                    onBlur={handleSupplierBlur}
                    placeholder="Code"
                    autoCapitalize="characters"
                  />
                  <TouchableOpacity style={styles.lookupBtn} onPress={fetchSuppliers}>
                    <ThemedText style={styles.lookupBtnText}>...</ThemedText>
                  </TouchableOpacity>
                </View>

                {/* Supplier Name */}
                <TextInput
                  style={[styles.input, styles.readonlyInput, { flex: 1, minWidth: 150 }]}
                  value={supplierName}
                  editable={false}
                  placeholder="Supplier Name"
                />
              </View>
            </View>

            {/* Row 2: Date Selection (From - To), Invoice No Filter, Paid Status Filter, Customer Copy */}
            <View style={[styles.row, { marginTop: 10 }]}>
              {/* Date Selection */}
              <View style={{ flexDirection: 'row', alignItems: 'center', marginRight: 16 }}>
                <ThemedText style={styles.label}>From Date</ThemedText>
                {Platform.OS === 'web' ? (
                  <input
                    type="date"
                    style={{
                      width: 120,
                      border: '1px solid #E2D7C5',
                      borderRadius: '6px',
                      padding: '4px 8px',
                      fontSize: '12px',
                      height: '28px',
                      color: '#2C2519',
                      backgroundColor: '#FFFFFF',
                      outline: 'none',
                      boxSizing: 'border-box',
                    }}
                    value={fromDate}
                    onChange={(e: any) => setFromDate(e.target.value || '')}
                  />
                ) : (
                  <TextInput
                    style={[styles.input, { width: 105 }]}
                    value={fromDate}
                    onChangeText={setFromDate}
                    placeholder="YYYY-MM-DD"
                  />
                )}

                <ThemedText style={{ fontSize: 11, color: '#7D6F5B', marginHorizontal: 8 }}>To</ThemedText>
                {Platform.OS === 'web' ? (
                  <input
                    type="date"
                    style={{
                      width: 120,
                      border: '1px solid #E2D7C5',
                      borderRadius: '6px',
                      padding: '4px 8px',
                      fontSize: '12px',
                      height: '28px',
                      color: '#2C2519',
                      backgroundColor: '#FFFFFF',
                      outline: 'none',
                      boxSizing: 'border-box',
                    }}
                    value={toDate}
                    onChange={(e: any) => setToDate(e.target.value || '')}
                  />
                ) : (
                  <TextInput
                    style={[styles.input, { width: 105 }]}
                    value={toDate}
                    onChangeText={setToDate}
                    placeholder="YYYY-MM-DD"
                  />
                )}
              </View>

              {/* Invoice No Filter matching txtHeadInvoiceNo */}
              <View style={{ flexDirection: 'row', alignItems: 'center', width: 200, marginRight: 16 }}>
                <ThemedText style={styles.label}>Invoice No</ThemedText>
                <TextInput
                  style={[styles.input, { flex: 1 }]}
                  value={headInvoiceNo}
                  onChangeText={setHeadInvoiceNo}
                  placeholder="Optional filter"
                />
              </View>

              {/* Paid Status Filter */}
              <View style={{ flexDirection: 'row', alignItems: 'center', marginRight: 16 }}>
                <ThemedText style={[styles.label, { width: 50 }]}>Status</ThemedText>
                <View style={styles.pillBox}>
                  <RadioButton label="UnPaid" selected={paidStatus === 'UnPaid'} onPress={() => setPaidStatus('UnPaid')} />
                  <RadioButton label="Paid" selected={paidStatus === 'Paid'} onPress={() => setPaidStatus('Paid')} />
                  <RadioButton label="All" selected={paidStatus === 'All'} onPress={() => setPaidStatus('All')} />
                </View>
              </View>

              {/* Customer Copy */}
              <View style={{ justifyContent: 'center' }}>
                <Checkbox label="Customer Copy" selected={customerCopy} onPress={() => setCustomerCopy(!customerCopy)} />
              </View>
            </View>
          </View>
        </View>

        {/* MIDDLE SECTION: JobSheet List Grid (dgvSupplier matching frmSupplierPayment.vb) */}
        <View style={styles.goldCard}>
          <View style={styles.cardHeaderBanner}>
            <View style={{ flexDirection: 'row', alignItems: 'center' }}>
              <View style={styles.headerIconBadge}>
                <Ionicons name="list" size={14} color="#B48218" />
              </View>
              <ThemedText style={styles.cardHeaderTitle}>Supplier JobSheets To Pay</ThemedText>
              <View style={styles.counterBadge}>
                <ThemedText style={styles.counterBadgeText}>{payments.length} Records</ThemedText>
              </View>
            </View>
          </View>

          <View style={{ width: '100%' }}>
            {/* Table Header */}
            <View style={styles.tableHeader}>
              <ThemedText style={[styles.tableHeaderText, { flex: 1.4 }]}>Job Sheet No</ThemedText>
              <ThemedText style={[styles.tableHeaderText, { flex: 1.2 }]}>Invoice No</ThemedText>
              <ThemedText style={[styles.tableHeaderText, { flex: 1.1, textAlign: 'center' }]}>Invoice Date</ThemedText>
              <ThemedText style={[styles.tableHeaderText, { flex: 1.2, textAlign: 'right' }]}>Total Cost</ThemedText>
              <ThemedText style={[styles.tableHeaderText, { flex: 1.2, textAlign: 'right' }]}>Paid Amount</ThemedText>
              <ThemedText style={[styles.tableHeaderText, { flex: 1.2, textAlign: 'right' }]}>Balance Amount</ThemedText>
              <ThemedText style={[styles.tableHeaderText, { flex: 1, textAlign: 'center' }]}>Status</ThemedText>
              <ThemedText style={[styles.tableHeaderText, { flex: 2 }]}>Remarks</ThemedText>
            </View>

            {/* Table Body */}
            <ScrollView nestedScrollEnabled={true} style={{ maxHeight: 250, minHeight: payments.length === 0 ? 110 : 0 }}>
              {loading ? (
                <View style={styles.emptyTableState}>
                  <ActivityIndicator size="large" color="#B48218" />
                  <ThemedText style={{ color: '#7D6F5B', fontSize: 12, marginTop: 8 }}>Loading jobsheets...</ThemedText>
                </View>
              ) : payments.length === 0 ? (
                <View style={styles.emptyTableState}>
                  <Ionicons name="document-text-outline" size={32} color="#D0C2A8" />
                  <ThemedText style={{ color: '#3B2D17', fontSize: 13, fontWeight: 'bold', marginTop: 4 }}>
                    No jobsheets found
                  </ThemedText>
                  <ThemedText style={{ color: '#7D6F5B', fontSize: 11, marginTop: 2 }}>
                    Select a Supplier and click 'Fetch'
                  </ThemedText>
                </View>
              ) : (
                payments.map((item, idx) => {
                  const isSelected = selectedIndex === idx;
                  const statusStr =
                    parseFloat(item.TotalCost) === parseFloat(item.PaidAmount)
                      ? 'PAID'
                      : parseFloat(item.PaidAmount) > 0
                        ? 'PARTIAL'
                        : 'UNPAID';

                  return (
                    <TouchableOpacity
                      key={idx}
                      activeOpacity={0.7}
                      style={[
                        styles.tableRow,
                        idx % 2 === 1 && { backgroundColor: '#FCFAF6' },
                        isSelected && styles.tableRowSelected,
                      ]}
                      onPress={() => handleSelectJobsheet(item, idx)}
                    >
                      <ThemedText style={[styles.tableCellText, { flex: 1.4, fontWeight: 'bold' }]}>
                        {item.JobSheetNo}
                      </ThemedText>
                      <ThemedText style={[styles.tableCellText, { flex: 1.2 }]}>
                        {item.PurchaseBillNo}
                      </ThemedText>
                      <ThemedText style={[styles.tableCellText, { flex: 1.1, textAlign: 'center' }]}>
                        {item.PurchaseBillDate || ''}
                      </ThemedText>
                      <ThemedText style={[styles.tableCellText, { flex: 1.2, textAlign: 'right' }]}>
                        {item.TotalCost ? Number(item.TotalCost).toFixed(2) : '0.00'}
                      </ThemedText>
                      <ThemedText style={[styles.tableCellText, { flex: 1.2, textAlign: 'right' }]}>
                        {item.PaidAmount ? Number(item.PaidAmount).toFixed(2) : '0.00'}
                      </ThemedText>
                      <ThemedText style={[styles.tableCellText, { flex: 1.2, textAlign: 'right', fontWeight: 'bold', color: '#9E6D08' }]}>
                        {item.BalanceAmount ? Number(item.BalanceAmount).toFixed(2) : '0.00'}
                      </ThemedText>
                      <ThemedText
                        style={[
                          styles.tableCellText,
                          {
                            flex: 1,
                            textAlign: 'center',
                            fontWeight: 'bold',
                            color: statusStr === 'PAID' ? '#2E7D32' : statusStr === 'PARTIAL' ? '#D97706' : '#C62828',
                          },
                        ]}
                      >
                        {statusStr}
                      </ThemedText>
                      <ThemedText style={[styles.tableCellText, { flex: 2 }]} numberOfLines={1}>
                        {item.Remarks || ''}
                      </ThemedText>
                    </TouchableOpacity>
                  );
                })
              )}
            </ScrollView>
          </View>
        </View>

        {/* BOTTOM SECTION: Payment Settlement Entry Card (GroupBox4 matching frmSupplierPayment.vb) */}
        <View style={styles.goldCard}>
          <View style={styles.cardHeaderBanner}>
            <View style={{ flexDirection: 'row', alignItems: 'center' }}>
              <View style={styles.headerIconBadge}>
                <Ionicons name="cash-outline" size={14} color="#B48218" />
              </View>
              <ThemedText style={styles.cardHeaderTitle}>Payment Settlement Entry</ThemedText>
            </View>
          </View>

          <View style={styles.cardBody}>
            {/* Selected Jobsheet Summary Info Row (JobSheetNo, InvoiceNo, InvoiceDate, TotalAmount, PaidAmount, BalanceAmount) */}
            <View style={styles.row}>
              <View style={{ flex: 1.2, minWidth: 110, marginRight: 8 }}>
                <ThemedText style={styles.inputSubLabel}>Job Sheet No</ThemedText>
                <TextInput style={[styles.input, styles.readonlyInput]} value={jobsheetNo} editable={false} placeholder="Select above" />
              </View>

              <View style={{ flex: 1.2, minWidth: 110, marginRight: 8 }}>
                <ThemedText style={styles.inputSubLabel}>Invoice No</ThemedText>
                <TextInput style={[styles.input, styles.readonlyInput]} value={selectedInvoiceNo} editable={false} />
              </View>

              <View style={{ flex: 1.1, minWidth: 95, marginRight: 8 }}>
                <ThemedText style={styles.inputSubLabel}>Invoice Date</ThemedText>
                <TextInput style={[styles.input, styles.readonlyInput]} value={invoiceDate} editable={false} />
              </View>

              <View style={{ flex: 1.1, minWidth: 95, marginRight: 8 }}>
                <ThemedText style={styles.inputSubLabel}>Total Amount</ThemedText>
                <TextInput style={[styles.input, styles.readonlyInput, { textAlign: 'right' }]} value={totalAmount} editable={false} />
              </View>

              <View style={{ flex: 1.1, minWidth: 95, marginRight: 8 }}>
                <ThemedText style={styles.inputSubLabel}>Total Paid</ThemedText>
                <TextInput style={[styles.input, styles.readonlyInput, { textAlign: 'right' }]} value={paidAmount} editable={false} />
              </View>

              <View style={{ flex: 1.2, minWidth: 100 }}>
                <ThemedText style={styles.inputSubLabel}>Balance Due</ThemedText>
                <TextInput
                  style={[styles.input, styles.readonlyInput, { textAlign: 'right', fontWeight: 'bold', color: '#9E6D08' }]}
                  value={balanceAmount}
                  editable={false}
                />
              </View>
            </View>

            {/* Payment Input Details */}
            <View style={[styles.row, { marginTop: 10, paddingTop: 10, borderTopWidth: 1, borderTopColor: '#F2ECE0' }]}>
              {/* Paymode Selector */}
              <View style={{ width: 145, marginRight: 10 }}>
                <ThemedText style={styles.inputSubLabel}>Paymode</ThemedText>
                <TouchableOpacity
                  style={[styles.input, { justifyContent: 'center' }]}
                  onPress={() =>
                    openLov(
                      'Select Paymode',
                      PAYMODE_OPTIONS.map(m => ({ Paymode: m })),
                      [{ key: 'Paymode', label: 'Payment Mode', flex: 1 }],
                      item => {
                        setPaymode(item.Paymode);
                        setRemarks('');
                        setIssueNo('');
                      }
                    )
                  }
                >
                  <View style={{ flexDirection: 'row', justifyContent: 'space-between', alignItems: 'center' }}>
                    <ThemedText style={{ fontSize: 12, fontWeight: 'bold', color: '#2C2519' }}>{paymode}</ThemedText>
                    <Ionicons name="chevron-down" size={14} color="#7D6F5B" />
                  </View>
                </TouchableOpacity>
              </View>

              {/* Conditional Bank Dropdown (when CHEQUE) */}
              {paymode === 'CHEQUE' && (
                <View style={{ width: 120, marginRight: 10 }}>
                  <ThemedText style={styles.inputSubLabel}>Bank</ThemedText>
                  <TouchableOpacity
                    style={[styles.input, { justifyContent: 'center' }]}
                    onPress={() =>
                      openLov(
                        'Select Bank',
                        BANK_OPTIONS.map(b => ({ Bank: b })),
                        [{ key: 'Bank', label: 'Bank Name', flex: 1 }],
                        item => setBank(item.Bank)
                      )
                    }
                  >
                    <View style={{ flexDirection: 'row', justifyContent: 'space-between', alignItems: 'center' }}>
                      <ThemedText style={{ fontSize: 12, fontWeight: 'bold', color: '#2C2519' }}>{bank}</ThemedText>
                      <Ionicons name="chevron-down" size={14} color="#7D6F5B" />
                    </View>
                  </TouchableOpacity>
                </View>
              )}

              {/* Conditional Issue Lookup for Gold Bar / Trade-In */}
              {(paymode === 'GOLD BAR' || paymode === 'TRADE-IN') && (
                <View style={{ width: 170, marginRight: 10 }}>
                  <ThemedText style={styles.inputSubLabel}>
                    {paymode === 'GOLD BAR' ? 'GoldBar Issue No' : 'Trade-In Issue No'}
                  </ThemedText>
                  <View style={{ flexDirection: 'row' }}>
                    <TextInput
                      style={[styles.input, { flex: 1, borderTopRightRadius: 0, borderBottomRightRadius: 0, borderRightWidth: 0 }]}
                      value={issueNo}
                      onChangeText={setIssueNo}
                      placeholder="Issue No"
                    />
                    <TouchableOpacity style={styles.lookupBtn} onPress={fetchIssueNo}>
                      <ThemedText style={styles.lookupBtnText}>...</ThemedText>
                    </TouchableOpacity>
                  </View>
                </View>
              )}

              {/* Payment Amount */}
              <View style={{ width: 130, marginRight: 10 }}>
                <ThemedText style={styles.inputSubLabel}>Pay Amount</ThemedText>
                <TextInput
                  style={[styles.input, { textAlign: 'right', fontWeight: 'bold', borderColor: '#B8903B' }]}
                  value={amount}
                  onChangeText={setAmount}
                  keyboardType="numeric"
                  placeholder="0.00"
                />
              </View>

              {/* Remarks Label matches lblRemarks in VB */}
              <View style={{ flex: 1, minWidth: 180 }}>
                <ThemedText style={styles.inputSubLabel}>
                  {paymode === 'CHEQUE' ? 'Cheque No' : paymode === 'GOLD BAR' || paymode === 'TRADE-IN' ? 'Gram' : 'Remarks'}
                </ThemedText>
                <TextInput
                  style={styles.input}
                  value={remarks}
                  onChangeText={setRemarks}
                  placeholder={
                    paymode === 'CHEQUE'
                      ? 'Enter Cheque Number'
                      : paymode === 'GOLD BAR' || paymode === 'TRADE-IN'
                        ? 'Enter Weight / Gram'
                        : 'Enter Remarks'
                  }
                />
              </View>
            </View>
          </View>
        </View>
      </ScrollView>

      {/* LOV Selection Modal */}
      <Modal visible={lovVisible} transparent={true} animationType="fade">
        <TouchableWithoutFeedback onPress={() => setLovVisible(false)}>
          <View style={styles.modalOverlay}>
            <TouchableWithoutFeedback>
              <View style={styles.modalContainer}>
                {/* Header */}
                <View style={styles.modalHeader}>
                  <ThemedText style={styles.modalTitle}>{lovTitle}</ThemedText>
                  <TouchableOpacity onPress={() => setLovVisible(false)} style={styles.modalCloseIconBtn}>
                    <Ionicons name="close" size={18} color="#3B2D17" />
                  </TouchableOpacity>
                </View>

                {/* Search */}
                <View style={{ padding: 10 }}>
                  <TextInput
                    style={styles.modalSearchInput}
                    placeholder="Search..."
                    value={lovSearch}
                    onChangeText={handleLovSearch}
                    autoFocus={true}
                  />

                  {/* Header Row */}
                  <View style={styles.lovHeaderRow}>
                    {lovColumns.map((col, idx) => (
                      <ThemedText key={idx} style={[styles.lovHeaderText, { flex: col.flex || 1 }]}>
                        {col.label}
                      </ThemedText>
                    ))}
                  </View>

                  {/* List */}
                  <FlatList
                    data={filteredLovData}
                    keyExtractor={(_, index) => index.toString()}
                    style={{ maxHeight: 300 }}
                    renderItem={({ item, index }) => (
                      <TouchableOpacity
                        style={[styles.lovItem, index % 2 === 1 && { backgroundColor: '#FCFAF6' }]}
                        onPress={() => handleLovSelect(item)}
                      >
                        {lovColumns.map((col, cIdx) => (
                          <ThemedText key={cIdx} style={[styles.lovItemText, { flex: col.flex || 1 }]}>
                            {item[col.key] !== undefined ? String(item[col.key]) : ''}
                          </ThemedText>
                        ))}
                      </TouchableOpacity>
                    )}
                    ListEmptyComponent={
                      <View style={{ padding: 20, alignItems: 'center' }}>
                        <ThemedText style={{ color: '#7D6F5B', fontSize: 12 }}>No matching records</ThemedText>
                      </View>
                    }
                  />
                </View>

                {/* Footer */}
                <View style={styles.modalFooter}>
                  <TouchableOpacity style={styles.modalCloseBtn} onPress={() => setLovVisible(false)}>
                    <ThemedText style={styles.modalCloseBtnText}>Cancel</ThemedText>
                  </TouchableOpacity>
                </View>
              </View>
            </TouchableWithoutFeedback>
          </View>
        </TouchableWithoutFeedback>
      </Modal>

      {/* Voucher RePrint Modal */}
      <Modal visible={voucherModalVisible} transparent={true} animationType="fade">
        <TouchableWithoutFeedback onPress={() => setVoucherModalVisible(false)}>
          <View style={styles.modalOverlay}>
            <TouchableWithoutFeedback>
              <View style={[styles.modalContainer, { maxWidth: 640 }]}>
                {/* Header */}
                <View style={styles.modalHeader}>
                  <ThemedText style={styles.modalTitle}>Supplier Payment Voucher</ThemedText>
                  <TouchableOpacity onPress={() => setVoucherModalVisible(false)} style={styles.modalCloseIconBtn}>
                    <Ionicons name="close" size={18} color="#3B2D17" />
                  </TouchableOpacity>
                </View>

                <View style={{ padding: 14 }}>
                  {voucherLoading ? (
                    <View style={{ padding: 30, alignItems: 'center' }}>
                      <ActivityIndicator size="large" color="#B48218" />
                      <ThemedText style={{ marginTop: 10, color: '#7D6F5B' }}>Loading payment vouchers...</ThemedText>
                    </View>
                  ) : voucherData.length === 0 ? (
                    <View style={{ padding: 30, alignItems: 'center' }}>
                      <Ionicons name="document-text-outline" size={32} color="#D0C2A8" />
                      <ThemedText style={{ color: '#3B2D17', fontSize: 13, fontWeight: 'bold', marginTop: 6 }}>
                        No Payment Voucher History
                      </ThemedText>
                      <ThemedText style={{ color: '#7D6F5B', fontSize: 11, marginTop: 2 }}>
                        No previous payments recorded for Invoice {headInvoiceNo || selectedInvoiceNo}
                      </ThemedText>
                    </View>
                  ) : (
                    <ScrollView style={{ maxHeight: 320 }}>
                      <View style={styles.tableHeader}>
                        <ThemedText style={[styles.tableHeaderText, { flex: 1.3 }]}>Payment No</ThemedText>
                        <ThemedText style={[styles.tableHeaderText, { flex: 1.1 }]}>Date</ThemedText>
                        <ThemedText style={[styles.tableHeaderText, { flex: 1 }]}>Paymode</ThemedText>
                        <ThemedText style={[styles.tableHeaderText, { flex: 1.2, textAlign: 'right' }]}>Amount</ThemedText>
                        <ThemedText style={[styles.tableHeaderText, { flex: 2 }]}>Remarks</ThemedText>
                      </View>
                      {voucherData.map((v, i) => (
                        <View key={i} style={[styles.tableRow, i % 2 === 1 && { backgroundColor: '#FCFAF6' }]}>
                          <ThemedText style={[styles.tableCellText, { flex: 1.3, fontWeight: 'bold' }]}>{v.PaymentNo}</ThemedText>
                          <ThemedText style={[styles.tableCellText, { flex: 1.1 }]}>{v.PaymentDate}</ThemedText>
                          <ThemedText style={[styles.tableCellText, { flex: 1 }]}>{v.Paymode}</ThemedText>
                          <ThemedText style={[styles.tableCellText, { flex: 1.2, textAlign: 'right', fontWeight: 'bold', color: '#9E6D08' }]}>
                            {Number(v.PaymentAmount).toFixed(2)}
                          </ThemedText>
                          <ThemedText style={[styles.tableCellText, { flex: 2 }]}>{v.Remarks}</ThemedText>
                        </View>
                      ))}
                    </ScrollView>
                  )}
                </View>

                <View style={styles.modalFooter}>
                  {Platform.OS === 'web' && (
                    <TouchableOpacity
                      style={[styles.modalCloseBtn, { backgroundColor: '#FDF6E2', borderColor: '#C8A24A', marginRight: 8 }]}
                      onPress={() => {
                        if (typeof window !== 'undefined') {
                          window.print();
                        }
                      }}
                    >
                      <ThemedText style={[styles.modalCloseBtnText, { color: '#9E6D08' }]}>Print Voucher</ThemedText>
                    </TouchableOpacity>
                  )}
                  <TouchableOpacity style={styles.modalCloseBtn} onPress={() => setVoucherModalVisible(false)}>
                    <ThemedText style={styles.modalCloseBtnText}>Close</ThemedText>
                  </TouchableOpacity>
                </View>
              </View>
            </TouchableWithoutFeedback>
          </View>
        </TouchableWithoutFeedback>
      </Modal>

      {/* Non-blocking Toast Banner */}
      {toast && (
        <View
          style={[
            styles.toast,
            {
              backgroundColor:
                toast.type === 'error'
                  ? '#D32F2F'
                  : toast.type === 'info'
                    ? '#0288D1'
                    : '#2E7D32',
            },
          ]}
        >
          <Ionicons
            name={
              toast.type === 'error'
                ? 'alert-circle'
                : toast.type === 'info'
                  ? 'information-circle'
                  : 'checkmark-circle'
            }
            size={18}
            color="#FFFFFF"
            style={{ marginRight: 8 }}
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
    backgroundColor: '#FAF7EE',
  },
  header: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingHorizontal: 16,
    paddingVertical: 10,
    backgroundColor: '#FFFFFF',
    borderBottomWidth: 1,
    borderBottomColor: '#E8DCB8',
  },
  headerLeft: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  backButton: {
    marginRight: 10,
    padding: 4,
  },
  headerTitle: {
    fontSize: 16,
    fontWeight: 'bold',
    color: '#3B2D17',
    letterSpacing: 0.3,
  },
  headerActions: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 8,
  },
  headerBtnSecondary: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingVertical: 6,
    paddingHorizontal: 12,
    borderRadius: 6,
    borderWidth: 1,
    borderColor: '#DFD5C3',
    backgroundColor: '#FFFFFF',
  },
  headerBtnSecondaryText: {
    fontSize: 12,
    fontWeight: '600',
    color: '#5C4A28',
  },
  headerBtnPrimary: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingVertical: 6,
    paddingHorizontal: 14,
    borderRadius: 6,
    backgroundColor: '#F5C842',
    borderWidth: 1,
    borderColor: '#D4A41B',
  },
  headerBtnPrimaryText: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#3D2C04',
  },
  headerBtnClose: {
    paddingVertical: 6,
    paddingHorizontal: 10,
    borderRadius: 6,
    backgroundColor: '#EAE3D2',
  },
  headerBtnCloseText: {
    fontSize: 12,
    fontWeight: '600',
    color: '#4A3B22',
  },
  container: {
    flex: 1,
  },
  contentContainer: {
    padding: 12,
    gap: 10,
  },
  goldCard: {
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    overflow: 'hidden',
  },
  cardHeaderBanner: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    backgroundColor: '#FDF6E2',
    paddingHorizontal: 12,
    paddingVertical: 8,
    borderBottomWidth: 1,
    borderBottomColor: '#F0E6D2',
  },
  headerIconBadge: {
    width: 22,
    height: 22,
    borderRadius: 11,
    backgroundColor: '#FFF1C2',
    justifyContent: 'center',
    alignItems: 'center',
    marginRight: 8,
  },
  cardHeaderTitle: {
    fontSize: 13,
    fontWeight: 'bold',
    color: '#3B2D17',
  },
  counterBadge: {
    backgroundColor: '#F3E8C4',
    paddingHorizontal: 8,
    paddingVertical: 2,
    borderRadius: 10,
    marginLeft: 8,
  },
  counterBadgeText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#7D5800',
  },
  fetchBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFF6D6',
    borderWidth: 1,
    borderColor: '#C8A24A',
    paddingVertical: 4,
    paddingHorizontal: 12,
    borderRadius: 5,
  },
  fetchBtnText: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#9E6D08',
  },
  cardBody: {
    padding: 12,
  },
  row: {
    flexDirection: 'row',
    alignItems: 'center',
    flexWrap: 'wrap',
  },
  label: {
    fontSize: 12,
    fontWeight: '600',
    color: '#4A3B22',
    marginRight: 8,
    width: 85,
  },
  inputSubLabel: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#6E5C3D',
    marginBottom: 4,
  },
  input: {
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderRadius: 6,
    paddingHorizontal: 8,
    paddingVertical: 4,
    fontSize: 12,
    height: 28,
    backgroundColor: '#FFFFFF',
    color: '#2C2519',
    minWidth: 0,
    ...(Platform.OS === 'web' ? { outlineStyle: 'none', boxSizing: 'border-box' } : {}),
  } as any,
  readonlyInput: {
    backgroundColor: '#F7F4EC',
    color: '#554734',
  },
  lookupBtn: {
    width: 32,
    height: 28,
    backgroundColor: '#FAF5EA',
    borderWidth: 1,
    borderColor: '#DFD5C3',
    borderTopRightRadius: 6,
    borderBottomRightRadius: 6,
    justifyContent: 'center',
    alignItems: 'center',
    flexShrink: 0,
    ...(Platform.OS === 'web' ? { boxSizing: 'border-box' } : {}),
  } as any,
  lookupBtnText: {
    fontSize: 13,
    fontWeight: 'bold',
    color: '#6E5528',
  },
  pillBox: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 10,
    backgroundColor: '#FAF5EA',
    borderWidth: 1,
    borderColor: '#E8DCB8',
    borderRadius: 6,
    paddingHorizontal: 8,
    paddingVertical: 3,
  },
  radioContainer: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  radioCircle: {
    width: 14,
    height: 14,
    borderRadius: 7,
    borderWidth: 1.5,
    borderColor: '#B8903B',
    justifyContent: 'center',
    alignItems: 'center',
    marginRight: 4,
  },
  radioInner: {
    width: 7,
    height: 7,
    borderRadius: 3.5,
    backgroundColor: '#B8903B',
  },
  radioLabel: {
    fontSize: 11,
    color: '#3B2D17',
    fontWeight: '600',
  },
  checkboxContainer: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  checkboxSquare: {
    width: 15,
    height: 15,
    borderRadius: 3,
    borderWidth: 1.5,
    borderColor: '#B8903B',
    justifyContent: 'center',
    alignItems: 'center',
    marginRight: 6,
    backgroundColor: '#FFFFFF',
  },
  checkboxSquareActive: {
    backgroundColor: '#B8903B',
  },
  tableHeader: {
    flexDirection: 'row',
    backgroundColor: '#F5E7CA',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    paddingVertical: 7,
    paddingHorizontal: 8,
  },
  tableHeaderText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#4A3B22',
  },
  tableRow: {
    flexDirection: 'row',
    alignItems: 'center',
    borderBottomWidth: 1,
    borderBottomColor: '#F0E6D2',
    paddingVertical: 7,
    paddingHorizontal: 8,
  },
  tableRowSelected: {
    backgroundColor: '#FFF2C6',
  },
  tableCellText: {
    fontSize: 11,
    color: '#2C2519',
  },
  emptyTableState: {
    padding: 30,
    alignItems: 'center',
    justifyContent: 'center',
  },
  modalOverlay: {
    flex: 1,
    backgroundColor: 'rgba(0,0,0,0.5)',
    justifyContent: 'center',
    alignItems: 'center',
    padding: 16,
  },
  modalContainer: {
    width: '90%',
    maxWidth: 580,
    maxHeight: '80%',
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#E8DCB8',
    borderRadius: 8,
    overflow: 'hidden',
  },
  modalHeader: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    backgroundColor: '#FDF6E2',
    borderBottomWidth: 1,
    borderBottomColor: '#E8DCB8',
    paddingHorizontal: 14,
    paddingVertical: 10,
  },
  modalTitle: {
    fontSize: 14,
    fontWeight: 'bold',
    color: '#3B2D17',
  },
  modalCloseIconBtn: {
    padding: 4,
  },
  modalSearchInput: {
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderRadius: 6,
    paddingHorizontal: 10,
    paddingVertical: 5,
    fontSize: 12,
    height: 32,
    backgroundColor: '#FFFFFF',
    color: '#2C2519',
    marginBottom: 8,
  },
  lovHeaderRow: {
    flexDirection: 'row',
    backgroundColor: '#F5E7CA',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    paddingVertical: 6,
    paddingHorizontal: 8,
    borderRadius: 4,
    marginBottom: 4,
  },
  lovHeaderText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3B2D17',
  },
  lovItem: {
    flexDirection: 'row',
    paddingVertical: 8,
    paddingHorizontal: 8,
    borderBottomWidth: 1,
    borderBottomColor: '#F0E6D2',
    backgroundColor: '#FFFFFF',
    alignItems: 'center',
  },
  lovItemText: {
    fontSize: 12,
    color: '#332717',
  },
  modalFooter: {
    flexDirection: 'row',
    justifyContent: 'flex-end',
    paddingHorizontal: 14,
    paddingVertical: 9,
    borderTopWidth: 1,
    borderTopColor: '#F0E6D2',
    backgroundColor: '#FCFAF6',
  },
  modalCloseBtn: {
    paddingVertical: 5,
    paddingHorizontal: 14,
    borderRadius: 5,
    borderWidth: 1,
    borderColor: '#DFD5C3',
    backgroundColor: '#FFFFFF',
  },
  modalCloseBtnText: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#5C4A28',
  },
  toast: {
    position: 'absolute',
    top: 40,
    alignSelf: 'center',
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 16,
    paddingVertical: 9,
    borderRadius: 20,
    elevation: 6,
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.25,
    shadowRadius: 3.84,
    zIndex: 9999,
  },
  toastText: {
    fontSize: 13,
    fontWeight: 'bold',
    color: '#FFFFFF',
  },
});
