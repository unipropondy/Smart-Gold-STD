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

export default function SupplierPaymentScreen() {
  const router = useRouter();

  // ===== STATE =====
  const [paymentNo, setPaymentNo] = useState('');
  const [paymentDate, setPaymentDate] = useState(new Date().toISOString().split('T')[0]);

  const [supplierCode, setSupplierCode] = useState('');
  const [supplierName, setSupplierName] = useState('');
  const [invoiceNo, setInvoiceNo] = useState('');

  // Date selection states
  const [fromDate, setFromDate] = useState(
    new Date(Date.now() - 365 * 24 * 60 * 60 * 1000).toISOString().split('T')[0]
  );
  const [toDate, setToDate] = useState(new Date().toISOString().split('T')[0]);

  const [paidStatus, setPaidStatus] = useState<'All' | 'Paid' | 'UnPaid'>('UnPaid');
  const [customerCopy, setCustomerCopy] = useState(false);

  const [payments, setPayments] = useState<any[]>([]);
  const [selectedIndex, setSelectedIndex] = useState<number | null>(null);

  // Selected Jobsheet Detail Form
  const [jobsheetNo, setJobsheetNo] = useState('');
  const [selectedInvoiceNo, setSelectedInvoiceNo] = useState('');
  const [invoiceDate, setInvoiceDate] = useState('');
  const [totalAmount, setTotalAmount] = useState('');
  const [paidAmount, setPaidAmount] = useState('');
  const [balanceAmount, setBalanceAmount] = useState('');

  // Payment input fields
  const [paymode, setPaymode] = useState('CASH');
  const [amount, setAmount] = useState('');
  const [remarks, setRemarks] = useState('');
  const [issueNo, setIssueNo] = useState('');

  const [saving, setSaving] = useState(false);
  const [loading, setLoading] = useState(false);
  const [toast, setToast] = useState<{ message: string; type: 'success' | 'error' } | null>(null);

  // LOV State
  const [lovVisible, setLovVisible] = useState(false);
  const [lovTitle, setLovTitle] = useState('');
  const [lovData, setLovData] = useState<any[]>([]);
  const [filteredLovData, setFilteredLovData] = useState<any[]>([]);
  const [lovSearch, setLovSearch] = useState('');
  const [lovColumns, setLovColumns] = useState<string[]>([]);
  const [onLovSelect, setOnLovSelect] = useState<((item: any) => void) | null>(null);

  const showToast = (message: string, type: 'success' | 'error' = 'success') => {
    setToast({ message, type });
    setTimeout(() => {
      setToast(null);
    }, 3000);
  };

  useEffect(() => {
    fetchNextPaymentNo();
  }, []);

  const fetchNextPaymentNo = async () => {
    try {
      const res = await fetch(`${API_URL}/supplierpayment/nextNo`);
      const json = await res.json();
      if (json.success) {
        setPaymentNo(json.nextNo);
      }
    } catch (e) {
      console.error('Failed to load payment no', e);
    }
  };

  const openLov = (title: string, data: any[], columns: string[], onSelect: (item: any) => void) => {
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
    if (!text) {
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

  const fetchSuppliers = async () => {
    setLoading(true);
    try {
      const res = await fetch(`${API_URL}/supplier`);
      const json = await res.json();
      if (json.success) {
        openLov('Select Supplier', json.data, ['SupplierCode', 'SupplierName'], item => {
          setSupplierCode(item.SupplierCode);
          setSupplierName(item.SupplierName);
        });
      } else {
        showToast('Failed to fetch suppliers', 'error');
      }
    } catch (e) {
      showToast('Error connecting to server', 'error');
    } finally {
      setLoading(false);
    }
  };

  const fetchIssueNo = async () => {
    setLoading(true);
    try {
      if (paymode === 'GOLD BAR') {
        const res = await fetch(`${API_URL}/goldbarfix/lov`);
        const json = await res.json();
        const finalData = json.success ? json.data : [];
        openLov('Select GoldBar Issue No', finalData, ['IssueNo', 'SupplierCode', 'IssuedWeight'], item => {
          setIssueNo(item.IssueNo);
          loadIssueDetail('GOLD BAR', item.IssueNo);
        });
      } else if (paymode === 'TRADE-IN') {
        const res = await fetch(`${API_URL}/tradeinissue/lov`);
        const json = await res.json();
        const finalData = json.success ? json.data : [];
        openLov('Select TradeIn Issue No', finalData, ['TradeIssueNo', 'SupplierCode', 'TotalWeight'], item => {
          setIssueNo(item.TradeIssueNo);
          loadIssueDetail('TRADE-IN', item.TradeIssueNo);
        });
      }
    } catch (e) {
      showToast('Error fetching issue records', 'error');
    } finally {
      setLoading(false);
    }
  };

  const loadIssueDetail = async (type: 'GOLD BAR' | 'TRADE-IN', no: string) => {
    try {
      const res = await fetch(`${API_URL}/supplierpayment/issueDetail?Type=${type}&IssueNo=${no}`);
      const json = await res.json();
      if (json.success) {
        if (json.data.Remarks) setRemarks(json.data.Remarks);
        if (json.data.Amount) setAmount(json.data.Amount.toString());
      }
    } catch (e) {
      console.error(e);
    }
  };

  const handleFetch = async () => {
    if (!supplierCode.trim()) {
      showToast('Supplier Code is required', 'error');
      return;
    }

    setLoading(true);
    try {
      const formattedFrom = fromDate.replace(/-/g, '');
      const formattedTo = toDate.replace(/-/g, '');
      const res = await fetch(
        `${API_URL}/supplierpayment/fetch?SupplierCode=${supplierCode}&FromDate=${formattedFrom}&ToDate=${formattedTo}&PaidStatus=${paidStatus}`
      );
      const json = await res.json();
      if (json.success) {
        setPayments(json.data);
        if (json.data.length === 0) {
          showToast('No jobsheets found for selected criteria', 'error');
        } else {
          showToast(`Loaded ${json.data.length} records.`, 'success');
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

  const handleSelectJobsheet = (item: any, idx: number) => {
    setSelectedIndex(idx);
    setJobsheetNo(item.JobSheetNo || '');
    setSelectedInvoiceNo(item.PurchaseBillNo || '');
    setInvoiceDate(item.PurchaseBillDate ? item.PurchaseBillDate.split('T')[0] : '');
    setTotalAmount(item.TotalCost ? item.TotalCost.toString() : '');
    setPaidAmount(item.PaidAmount ? item.PaidAmount.toString() : '');
    setBalanceAmount(item.BalanceAmount ? item.BalanceAmount.toString() : '');
  };

  const handleClearAll = () => {
    setSupplierCode('');
    setSupplierName('');
    setInvoiceNo('');
    setCustomerCopy(false);
    setPayments([]);
    setSelectedIndex(null);
    setJobsheetNo('');
    setSelectedInvoiceNo('');
    setInvoiceDate('');
    setTotalAmount('');
    setPaidAmount('');
    setBalanceAmount('');
    setPaymode('CASH');
    setAmount('');
    setRemarks('');
    setIssueNo('');
    fetchNextPaymentNo();
    showToast('Cleared All', 'success');
  };

  const handleUpdate = async () => {
    if (paymode === 'CHEQUE' && !remarks.trim()) {
      showToast('Please Enter the Cheque No in Remarks', 'error');
      return;
    }
    if (paymode === 'GOLD BAR' && !remarks.trim()) {
      showToast('Please Enter the Bar Weight in Remarks', 'error');
      return;
    }
    if (paymode === 'TRADE-IN' && !remarks.trim()) {
      showToast('Please Enter the TradeIn Weight in Remarks', 'error');
      return;
    }
    if (!paymode.trim()) {
      showToast('Please select a Paymode', 'error');
      return;
    }
    const valAmt = parseFloat(amount) || 0;
    if (valAmt <= 0) {
      showToast('Please enter a valid Payment Amount', 'error');
      return;
    }
    if (!selectedInvoiceNo) {
      showToast('Please select invoice details to update', 'error');
      return;
    }

    setSaving(true);
    try {
      let finalRemarks = remarks;
      if (paymode === 'CHEQUE') {
        finalRemarks = `CHEQUE: ${remarks}`;
      } else if (paymode === 'GOLD BAR' || paymode === 'TRADE-IN') {
        finalRemarks = `ISSUE ${issueNo}: ${remarks}`;
      }

      const res = await fetch(`${API_URL}/supplierpayment`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          PaymentNo: paymentNo,
          PaymentDate: paymentDate.replace(/-/g, ''),
          PaymentAmount: valAmt,
          Remarks: finalRemarks,
          InvoiceNo: selectedInvoiceNo,
          InvoiceDate: invoiceDate,
          SupplierCode: supplierCode,
          Paymode: paymode
        })
      });
      const json = await res.json();
      if (json.success) {
        showToast('Payment Successful', 'success');
        handleFetch();
        setAmount('');
        setRemarks('');
        setIssueNo('');
        fetchNextPaymentNo();
      } else {
        showToast(json.message || 'Failed to submit payment', 'error');
      }
    } catch (e) {
      showToast('Server connection failed', 'error');
    } finally {
      setSaving(false);
    }
  };

  const RadioButton = ({ label, selected, onPress }: { label: string; selected: boolean; onPress: () => void }) => (
    <TouchableOpacity style={styles.radioContainer} onPress={onPress}>
      <View style={styles.radioCircle}>
        {selected && <View style={styles.radioInner} />}
      </View>
      <ThemedText style={styles.radioLabel}>{label}</ThemedText>
    </TouchableOpacity>
  );

  const Checkbox = ({ label, selected, onPress }: { label: string; selected: boolean; onPress: () => void }) => (
    <TouchableOpacity style={styles.radioContainer} onPress={onPress}>
      <View style={[styles.checkboxSquare, selected && styles.checkboxSquareActive]}>
        {selected && <Ionicons name="checkmark" size={10} color="#FFFFFF" />}
      </View>
      {label ? <ThemedText style={styles.radioLabel}>{label}</ThemedText> : null}
    </TouchableOpacity>
  );

  return (
    <SafeAreaView edges={['top']} style={styles.safeArea}>
      <Stack.Screen options={{ headerShown: false }} />

      {/* Top Header Bar matching jobsheet.tsx */}
      <View style={styles.header}>
        <View style={styles.headerLeft}>
          <TouchableOpacity onPress={() => router.back()} style={styles.backButton}>
            <Ionicons name="arrow-back" size={20} color="#3B2D17" />
          </TouchableOpacity>
          <ThemedText style={styles.headerTitle}>Supplier Payment</ThemedText>
        </View>

        <View style={styles.headerActions}>
          <TouchableOpacity style={styles.headerBtnSecondary} onPress={handleClearAll}>
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
              <ThemedText style={styles.headerBtnPrimaryText}>Update Payment</ThemedText>
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
                <Ionicons name="card" size={15} color="#B48218" />
              </View>
              <ThemedText style={styles.cardHeaderTitle}>Payment Header & Filter</ThemedText>
            </View>

            <TouchableOpacity style={styles.fetchBtn} onPress={handleFetch} disabled={loading}>
              <Ionicons name="search" size={13} color="#9E6D08" style={{ marginRight: 4 }} />
              <ThemedText style={styles.fetchBtnText}>{loading ? 'Fetching...' : 'Fetch JobSheets'}</ThemedText>
            </TouchableOpacity>
          </View>

          <View style={styles.cardBody}>
            {/* Row 1: Payment No, Payment Date, Supplier Code & Name */}
            <View style={styles.row}>
              {/* Payment No */}
              <ThemedText style={styles.label}>Payment No</ThemedText>
              <TextInput
                style={[styles.input, styles.readonlyInput, { width: 140 }]}
                value={paymentNo}
                editable={false}
              />

              {/* Payment Date */}
              <ThemedText style={[styles.labelRight, { width: 95 }]}>Payment Date</ThemedText>
              <View style={{ width: 140 }}>
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
                    style={[styles.input, { width: 140 }]}
                    value={paymentDate}
                    onChangeText={setPaymentDate}
                    placeholder="YYYY-MM-DD"
                  />
                )}
              </View>

              {/* Supplier Code */}
              <ThemedText style={[styles.labelRight, { width: 95 }]}>Supplier</ThemedText>
              <View style={{ width: 135, flexDirection: 'row' }}>
                <TextInput
                  style={[styles.input, { width: 103, borderTopRightRadius: 0, borderBottomRightRadius: 0, borderRightWidth: 0 }]}
                  value={supplierCode}
                  onChangeText={setSupplierCode}
                  placeholder="Code"
                />
                <TouchableOpacity style={styles.lookupBtn} onPress={fetchSuppliers}>
                  <ThemedText style={styles.lookupBtnText}>...</ThemedText>
                </TouchableOpacity>
              </View>

              {/* Supplier Name */}
              <TextInput
                style={[styles.input, styles.readonlyInput, { flex: 1, minWidth: 150, marginLeft: 8 }]}
                value={supplierName}
                editable={false}
                placeholder="Supplier Name"
              />
            </View>

            {/* Row 2: Date Selection (From - To), Paid Status Filter, Customer Copy */}
            <View style={styles.row}>
              {/* Date Selection */}
              <ThemedText style={styles.label}>Date Range</ThemedText>
              <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                <ThemedText style={{ fontSize: 11, color: '#7D6F5B', marginRight: 4 }}>From</ThemedText>
                {Platform.OS === 'web' ? (
                  <input
                    type="date"
                    style={{
                      width: 125,
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
                    style={[styles.input, { width: 110 }]}
                    value={fromDate}
                    onChangeText={setFromDate}
                    placeholder="YYYY-MM-DD"
                  />
                )}

                <ThemedText style={{ fontSize: 11, color: '#7D6F5B', marginHorizontal: 6 }}>To</ThemedText>
                {Platform.OS === 'web' ? (
                  <input
                    type="date"
                    style={{
                      width: 125,
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
                    style={[styles.input, { width: 110 }]}
                    value={toDate}
                    onChangeText={setToDate}
                    placeholder="YYYY-MM-DD"
                  />
                )}
              </View>

              {/* Paid Status Filter */}
              <ThemedText style={[styles.labelRight, { width: 90 }]}>Status</ThemedText>
              <View style={styles.pillBox}>
                <RadioButton label="UnPaid" selected={paidStatus === 'UnPaid'} onPress={() => setPaidStatus('UnPaid')} />
                <RadioButton label="Paid" selected={paidStatus === 'Paid'} onPress={() => setPaidStatus('Paid')} />
                <RadioButton label="All" selected={paidStatus === 'All'} onPress={() => setPaidStatus('All')} />
              </View>

              {/* Customer Copy */}
              <View style={{ marginLeft: 16 }}>
                <Checkbox label="Customer Copy" selected={customerCopy} onPress={() => setCustomerCopy(!customerCopy)} />
              </View>
            </View>
          </View>
        </View>

        {/* MIDDLE SECTION: JobSheet List Grid (Jobsheet-style Table) */}
        <View style={styles.goldCard}>
          <View style={styles.cardHeaderBanner}>
            <View style={{ flexDirection: 'row', alignItems: 'center' }}>
              <View style={styles.headerIconBadge}>
                <Ionicons name="list" size={15} color="#B48218" />
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
              <ThemedText style={[styles.tableHeaderText, { flex: 1.2, textAlign: 'center' }]}>Invoice Date</ThemedText>
              <ThemedText style={[styles.tableHeaderText, { flex: 1.3, textAlign: 'right' }]}>Total Amount</ThemedText>
              <ThemedText style={[styles.tableHeaderText, { flex: 1.3, textAlign: 'right' }]}>Paid Amount</ThemedText>
              <ThemedText style={[styles.tableHeaderText, { flex: 1.3, textAlign: 'right' }]}>Balance</ThemedText>
              <ThemedText style={[styles.tableHeaderText, { flex: 1, textAlign: 'center' }]}>Status</ThemedText>
              <ThemedText style={[styles.tableHeaderText, { flex: 2 }]}>Remarks</ThemedText>
            </View>

            {/* Table Body */}
            <ScrollView nestedScrollEnabled={true} style={{ maxHeight: 260, minHeight: payments.length === 0 ? 100 : 0 }}>
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
                    Select a Supplier Code and click 'Fetch JobSheets'
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
                      <ThemedText style={[styles.tableCellText, { flex: 1.2, textAlign: 'center' }]}>
                        {item.PurchaseBillDate ? item.PurchaseBillDate.split('T')[0] : ''}
                      </ThemedText>
                      <ThemedText style={[styles.tableCellText, { flex: 1.3, textAlign: 'right' }]}>
                        {item.TotalCost ? Number(item.TotalCost).toFixed(2) : '0.00'}
                      </ThemedText>
                      <ThemedText style={[styles.tableCellText, { flex: 1.3, textAlign: 'right' }]}>
                        {item.PaidAmount ? Number(item.PaidAmount).toFixed(2) : '0.00'}
                      </ThemedText>
                      <ThemedText style={[styles.tableCellText, { flex: 1.3, textAlign: 'right', fontWeight: 'bold', color: '#9E6D08' }]}>
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

        {/* BOTTOM SECTION: Payment Settlement Card (Jobsheet Gold Card) */}
        <View style={styles.goldCard}>
          <View style={styles.cardHeaderBanner}>
            <View style={{ flexDirection: 'row', alignItems: 'center' }}>
              <View style={styles.headerIconBadge}>
                <Ionicons name="cash-outline" size={15} color="#B48218" />
              </View>
              <ThemedText style={styles.cardHeaderTitle}>Payment Settlement Entry</ThemedText>
            </View>
          </View>

          <View style={styles.cardBody}>
            {/* Selected Jobsheet Summary Info Row */}
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
            <View style={[styles.row, { marginTop: 8, paddingTop: 8, borderTopWidth: 1, borderTopColor: '#F2ECE0' }]}>
              {/* Paymode */}
              <View style={{ width: 140, marginRight: 10 }}>
                <ThemedText style={styles.inputSubLabel}>Paymode</ThemedText>
                <TouchableOpacity
                  style={[styles.input, { justifyContent: 'center' }]}
                  onPress={() =>
                    openLov(
                      'Select Paymode',
                      [{ Mode: 'CASH' }, { Mode: 'CHEQUE' }, { Mode: 'GOLD BAR' }, { Mode: 'TRADE-IN' }],
                      ['Mode'],
                      item => setPaymode(item.Mode)
                    )
                  }
                >
                  <View style={{ flexDirection: 'row', justifyContent: 'space-between', alignItems: 'center' }}>
                    <ThemedText style={{ fontSize: 12, fontWeight: 'bold', color: '#2C2519' }}>{paymode}</ThemedText>
                    <Ionicons name="chevron-down" size={14} color="#7D6F5B" />
                  </View>
                </TouchableOpacity>
              </View>

              {/* Conditional Issue Lookup for Gold Bar / Trade-In */}
              {(paymode === 'GOLD BAR' || paymode === 'TRADE-IN') && (
                <View style={{ width: 160, marginRight: 10 }}>
                  <ThemedText style={styles.inputSubLabel}>
                    {paymode === 'GOLD BAR' ? 'GoldBar Issue No' : 'Trade-In Issue No'}
                  </ThemedText>
                  <View style={{ flexDirection: 'row' }}>
                    <TextInput
                      style={[styles.input, { flex: 1, borderTopRightRadius: 0, borderBottomRightRadius: 0, borderRightWidth: 0 }]}
                      value={issueNo}
                      onChangeText={setIssueNo}
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

              {/* Remarks */}
              <View style={{ flex: 1, minWidth: 180 }}>
                <ThemedText style={styles.inputSubLabel}>Remarks / Cheque No / Bar Weight</ThemedText>
                <TextInput
                  style={styles.input}
                  value={remarks}
                  onChangeText={setRemarks}
                  placeholder="Enter notes or cheque number"
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
                <View style={styles.modalHeader}>
                  <ThemedText style={styles.modalTitle}>{lovTitle}</ThemedText>
                  <TouchableOpacity onPress={() => setLovVisible(false)} style={styles.modalCloseIconBtn}>
                    <Ionicons name="close" size={20} color="#3B2D17" />
                  </TouchableOpacity>
                </View>

                <View style={{ padding: 12 }}>
                  <TextInput
                    style={styles.modalSearchInput}
                    placeholder="Search options..."
                    placeholderTextColor="#7D6F5B"
                    value={lovSearch}
                    onChangeText={handleLovSearch}
                    autoFocus
                  />

                  {lovColumns.length > 0 && (
                    <View style={styles.lovHeaderRow}>
                      {lovColumns.map((col, idx) => (
                        <ThemedText key={idx} style={[styles.lovHeaderText, { flex: 1 }]}>
                          {col}
                        </ThemedText>
                      ))}
                    </View>
                  )}

                  <FlatList
                    data={filteredLovData}
                    keyExtractor={(item, index) => index.toString()}
                    style={{ maxHeight: 320 }}
                    renderItem={({ item, index }) => (
                      <TouchableOpacity
                        activeOpacity={0.7}
                        style={[
                          styles.lovItem,
                          index % 2 === 1 && { backgroundColor: '#FCFAF6' },
                        ]}
                        onPress={() => handleLovSelect(item)}
                      >
                        {lovColumns.length > 0 ? (
                          lovColumns.map((col, idx) => (
                            <ThemedText
                              key={idx}
                              style={[
                                styles.lovItemText,
                                { flex: 1 },
                                idx === 0 && { fontWeight: 'bold' },
                              ]}
                            >
                              {String(item[col] ?? '')}
                            </ThemedText>
                          ))
                        ) : (
                          <ThemedText style={styles.lovItemText}>{Object.values(item).join(' - ')}</ThemedText>
                        )}
                      </TouchableOpacity>
                    )}
                    ListEmptyComponent={
                      <View style={{ padding: 24, alignItems: 'center' }}>
                        <ThemedText style={{ color: '#7D6F5B', fontSize: 13, fontStyle: 'italic' }}>
                          No records found
                        </ThemedText>
                      </View>
                    }
                  />
                </View>

                <View style={styles.modalFooter}>
                  <TouchableOpacity style={styles.modalCloseBtn} onPress={() => setLovVisible(false)}>
                    <ThemedText style={styles.modalCloseBtnText}>Close</ThemedText>
                  </TouchableOpacity>
                </View>
              </View>
            </TouchableWithoutFeedback>
          </View>
        </TouchableWithoutFeedback>
      </Modal>

      {/* Floating Toast Notification */}
      {toast && (
        <View style={[styles.toast, { backgroundColor: toast.type === 'success' ? '#2E7D32' : '#C62828' }]}>
          <Ionicons
            name={toast.type === 'success' ? 'checkmark-circle' : 'alert-circle'}
            size={16}
            color="#FFFFFF"
            style={{ marginRight: 6 }}
          />
          <ThemedText style={styles.toastText}>{toast.message}</ThemedText>
        </View>
      )}

      {/* Loading Overlay */}
      {saving && (
        <View style={styles.loadingOverlay}>
          <ActivityIndicator size="large" color="#B48218" />
          <ThemedText style={{ color: '#FFFFFF', fontWeight: 'bold', marginTop: 10 }}>Processing Payment...</ThemedText>
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
  // Top Header Bar
  header: {
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
  backButton: {
    padding: 4,
    marginRight: 10,
  },
  headerTitle: {
    fontSize: 20,
    fontWeight: '400',
    letterSpacing: 1,
    color: '#3B2D17',
  },
  headerActions: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 8,
  },
  headerBtnSecondary: {
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#DFD5C3',
    paddingVertical: 6,
    paddingHorizontal: 14,
    borderRadius: 5,
  },
  headerBtnSecondaryText: {
    color: '#5C4A28',
    fontWeight: 'bold',
    fontSize: 12,
  },
  headerBtnPrimary: {
    backgroundColor: '#F9F2E2',
    borderWidth: 1,
    borderColor: '#B8903B',
    paddingVertical: 6,
    paddingHorizontal: 16,
    borderRadius: 5,
  },
  headerBtnPrimaryText: {
    color: '#9E6D08',
    fontWeight: 'bold',
    fontSize: 12,
  },
  container: {
    flex: 1,
    backgroundColor: '#FBF9F5',
  },
  contentContainer: {
    padding: 12,
    paddingBottom: 28,
  },
  // Gold Cards
  goldCard: {
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#E8DCB8',
    borderRadius: 8,
    marginBottom: 12,
    overflow: 'hidden',
    shadowColor: '#A38035',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.08,
    shadowRadius: 3,
    elevation: 2,
  },
  cardHeaderBanner: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    backgroundColor: '#F5E7CA',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    paddingVertical: 7,
    paddingHorizontal: 10,
  },
  headerIconBadge: {
    width: 22,
    height: 22,
    borderRadius: 11,
    backgroundColor: '#FFF8EA',
    borderWidth: 1,
    borderColor: '#E2D2AF',
    justifyContent: 'center',
    alignItems: 'center',
    marginRight: 8,
  },
  cardHeaderTitle: {
    fontSize: 13,
    fontWeight: 'bold',
    color: '#3B2D17',
    letterSpacing: 0.3,
  },
  counterBadge: {
    backgroundColor: '#FFFFFF',
    paddingHorizontal: 8,
    paddingVertical: 2,
    borderRadius: 10,
    borderWidth: 1,
    borderColor: '#E2D3B3',
    marginLeft: 8,
  },
  counterBadgeText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#9E6D08',
  },
  fetchBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#F9F2E2',
    borderWidth: 1,
    borderColor: '#B8903B',
    paddingVertical: 4,
    paddingHorizontal: 10,
    borderRadius: 5,
  },
  fetchBtnText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#9E6D08',
  },
  cardBody: {
    padding: 10,
  },
  row: {
    flexDirection: 'row',
    alignItems: 'center',
    marginBottom: 7,
    flexWrap: 'wrap',
  },
  label: {
    fontSize: 11,
    fontWeight: 'bold',
    width: 85,
    marginRight: 6,
    color: '#3A2E19',
  },
  labelRight: {
    fontSize: 11,
    fontWeight: 'bold',
    textAlign: 'right',
    marginRight: 8,
    color: '#3A2E19',
  },
  inputSubLabel: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3A2E19',
    marginBottom: 3,
  },
  input: {
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderRadius: 6,
    paddingHorizontal: 8,
    paddingVertical: 3,
    fontSize: 12,
    height: 28,
    backgroundColor: '#FFFFFF',
    color: '#2C2519',
  },
  readonlyInput: {
    backgroundColor: '#F8F5EE',
    borderColor: '#EADBCA',
    color: '#554736',
  },
  lookupBtn: {
    width: 32,
    height: 28,
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderLeftWidth: 1,
    justifyContent: 'center',
    alignItems: 'center',
    borderTopRightRadius: 6,
    borderBottomRightRadius: 6,
    backgroundColor: '#FAF5EA',
  },
  lookupBtnText: {
    color: '#6E5528',
    fontWeight: 'bold',
    fontSize: 12,
  },
  pillBox: {
    flexDirection: 'row',
    alignItems: 'center',
    borderWidth: 1,
    borderColor: '#DFD3BC',
    borderRadius: 14,
    paddingHorizontal: 6,
    paddingVertical: 1,
    height: 26,
    backgroundColor: '#FBF8F2',
  },
  radioContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    marginVertical: 2,
    marginRight: 8,
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
    backgroundColor: '#FFFFFF',
  },
  radioInner: {
    width: 6,
    height: 6,
    borderRadius: 3,
    backgroundColor: '#B8903B',
  },
  radioLabel: {
    fontSize: 11,
    color: '#362B1A',
    fontWeight: 'bold',
  },
  checkboxSquare: {
    width: 16,
    height: 16,
    borderWidth: 1.5,
    borderColor: '#B8903B',
    justifyContent: 'center',
    alignItems: 'center',
    borderRadius: 3,
    backgroundColor: '#FFFFFF',
    marginRight: 4,
  },
  checkboxSquareActive: {
    backgroundColor: '#B8903B',
  },
  // Table
  tableHeader: {
    flexDirection: 'row',
    backgroundColor: '#F5E7CA',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    paddingVertical: 7,
    paddingHorizontal: 6,
  },
  tableHeaderText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3B2D17',
    paddingHorizontal: 4,
  },
  tableRow: {
    flexDirection: 'row',
    paddingVertical: 6,
    paddingHorizontal: 6,
    borderBottomWidth: 1,
    borderBottomColor: '#F0E6D2',
    alignItems: 'center',
    backgroundColor: '#FFFFFF',
  },
  tableRowSelected: {
    backgroundColor: '#FFF6DF',
    borderLeftWidth: 3,
    borderLeftColor: '#B8903B',
  },
  tableCellText: {
    fontSize: 11,
    color: '#332717',
    paddingHorizontal: 4,
  },
  emptyTableState: {
    paddingVertical: 32,
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: '#FFFFFF',
  },
  // Modal
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
    backgroundColor: '#F5E7CA',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    paddingHorizontal: 12,
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
    marginBottom: 10,
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
    paddingVertical: 9,
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
    paddingHorizontal: 12,
    paddingVertical: 8,
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
    paddingHorizontal: 18,
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
  loadingOverlay: {
    ...StyleSheet.absoluteFillObject,
    backgroundColor: 'rgba(0,0,0,0.45)',
    justifyContent: 'center',
    alignItems: 'center',
    zIndex: 10000,
  },
});
