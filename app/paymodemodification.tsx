import { ThemedText } from '@/components/themed-text';
import { API_BASE_URL } from '@/constants/api';
import { Ionicons } from '@expo/vector-icons';
import { Stack, useRouter } from 'expo-router';
import React, { useEffect, useRef, useState } from 'react';
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
  TouchableWithoutFeedback,
  View,
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';

const API_URL = `${API_BASE_URL}/paymodemodification`;

interface InvoiceHeader {
  InvoiceNo: string;
  CustomerCode?: string;
  TerminalCode?: string;
  SalesManCode?: string;
  SalesManName?: string;
  InvoicePaymode?: string;
  NetTotal: number | string;
  PaidAmt: number | string;
  BalanceAmt?: number | string;
  Change?: number | string;
  Remarks?: string;
  LocationCode?: string;
  SalesType?: string;
  InvoiceDate?: string;
  InvoiceDateStr?: string;
}

interface PayModeItem {
  PayMode: string;
  Amount: number | string;
  SlipNo: string;
  NewAmount: string;
  NewSlipNo: string;
}

export default function PaymodeModificationScreen() {
  const router = useRouter();

  // Invoices list (dgvBills)
  const [invoicesList, setInvoicesList] = useState<InvoiceHeader[]>([]);
  const [loadingInvoices, setLoadingInvoices] = useState(false);
  const [invoiceNoInput, setInvoiceNoInput] = useState('');

  // Selected Invoice & Remarks
  const [invoiceData, setInvoiceData] = useState<InvoiceHeader | null>(null);
  const [remarks, setRemarks] = useState('');

  // Paymodes Grid (dgvPaymode)
  const [paymodes, setPaymodes] = useState<PayModeItem[]>([]);

  // Action loaders
  const [loading, setLoading] = useState(false);
  const [saving, setSaving] = useState(false);

  // Summary Totals matching VB subCalcTotal:
  // lblInvoice: string, lblTotal: NetTotal or PaidAmt of bill, lblPaid: sum of NewAmount, lblBalance: lblTotal - lblPaid
  const [netAmount, setNetAmount] = useState(0);
  const [totalPaid, setTotalPaid] = useState(0);
  const [balance, setBalance] = useState(0);

  // Salesman Password Modal (PnlSalesManPwd)
  const [pwdVisible, setPwdVisible] = useState(false);
  const [salesmanPwd, setSalesmanPwd] = useState('');
  const [verifyingPwd, setVerifyingPwd] = useState(false);

  // Custom Toast notification
  const [toast, setToast] = useState<{
    visible: boolean;
    message: string;
    type: 'success' | 'error' | 'info';
  }>({ visible: false, message: '', type: 'info' });

  const toastTimerRef = useRef<any>(null);
  const invoiceInputRef = useRef<TextInput>(null);
  const pwdInputRef = useRef<TextInput>(null);

  const showToast = (message: string, type: 'success' | 'error' | 'info' = 'info') => {
    if (toastTimerRef.current) clearTimeout(toastTimerRef.current);
    setToast({ visible: true, message, type });
    toastTimerRef.current = setTimeout(() => {
      setToast(prev => ({ ...prev, visible: false }));
    }, 3200);
  };

  // 1. Initial Load: subLoadinvoices() matching VB Form_Load
  useEffect(() => {
    fetchInvoicesList(true);
  }, []);

  const fetchInvoicesList = async (autoSelectFirst = false) => {
    setLoadingInvoices(true);
    try {
      const response = await fetch(`${API_URL}/invoices`);
      const json = await response.json();
      if (json.success && Array.isArray(json.data)) {
        setInvoicesList(json.data);
        // Automatically load first invoice if available (as shown in reference screenshot)
        if (autoSelectFirst && json.data.length > 0) {
          handleSelectInvoice(json.data[0]);
        }
      }
    } catch (err: any) {
      console.error('Error fetching invoices list:', err);
    } finally {
      setLoadingInvoices(false);
    }
  };

  // 2. Fetch single invoice details & paymodes (subLoadinvoicesbyInvoiceNo + subLoadPayMode)
  const fetchInvoice = async (invNo: string) => {
    const trimmed = invNo.trim();
    if (!trimmed) {
      showToast('Please enter an Invoice No to search', 'info');
      return;
    }
    setLoading(true);
    try {
      const response = await fetch(`${API_URL}/invoice/${encodeURIComponent(trimmed)}`);
      const json = await response.json();
      if (json.success && json.header) {
        setInvoiceData(json.header);
        setInvoiceNoInput(json.header.InvoiceNo);
        setRemarks(json.header.Remarks || '');

        const currentPaymodes: PayModeItem[] = json.paymodes || [];
        setPaymodes(currentPaymodes);

        // In VB subLoadPayMode: lblTotal.Text = Val(fnGetdgvText(dgvBills, dgvBills.CurrentRow.Index, cPaidAmt.Index))
        // Or NetTotal if PaidAmt is 0
        const headerPaid = parseFloat(json.header.PaidAmt || 0);
        const headerNet = parseFloat(json.header.NetTotal || 0);
        const targetTotal = headerPaid !== 0 ? headerPaid : headerNet;

        setNetAmount(targetTotal);
        calculateTotals(currentPaymodes, targetTotal);
        showToast(`Invoice ${json.header.InvoiceNo} loaded`, 'success');
      } else {
        showToast(json.message || 'Invoice No Not Found', 'error');
        clearData();
      }
    } catch (error: any) {
      console.error('Fetch error:', error);
      showToast('Failed to fetch invoice details', 'error');
    } finally {
      setLoading(false);
    }
  };

  // Select an invoice from the overview table (dgvBills_Click in VB)
  const handleSelectInvoice = (item: InvoiceHeader) => {
    setInvoiceNoInput(item.InvoiceNo);
    fetchInvoice(item.InvoiceNo);
  };

  // 3. Calculation matching subCalcTotal in VB:
  // For iRow = 0 To dgvPaymode.RowCount - 1
  //   lblPaid.Text = Val(lblPaid.Text) + Val(fnGetdgvText(dgvPaymode, iRow, cNewAmount.Index))
  //   lblBalance.Text = Val(lblTotal.Text) - Val(lblPaid.Text)
  // Next
  const calculateTotals = (currentPaymodes: PayModeItem[], targetTotal = netAmount) => {
    let paid = 0;
    currentPaymodes.forEach(p => {
      paid += parseFloat(p.NewAmount) || 0;
    });
    setTotalPaid(paid);
    setBalance(targetTotal - paid);
  };

  // Keep totals updated dynamically when paymodes change
  useEffect(() => {
    if (invoiceData) {
      calculateTotals(paymodes, netAmount);
    }
  }, [paymodes, netAmount]);

  const updatePaymodeField = (index: number, field: keyof PayModeItem, value: string) => {
    const updated = [...paymodes];
    updated[index] = { ...updated[index], [field]: value };
    setPaymodes(updated);
  };

  const handleAmountBlur = (index: number) => {
    const updated = [...paymodes];
    const rawVal = parseFloat(updated[index].NewAmount) || 0;
    updated[index].NewAmount = rawVal.toFixed(2);
    setPaymodes(updated);
  };

  // 4. Clear data matching subClear in VB
  const clearData = () => {
    setInvoiceData(null);
    setPaymodes([]);
    setRemarks('');
    setNetAmount(0);
    setTotalPaid(0);
    setBalance(0);
  };

  const handleClear = () => {
    setInvoiceNoInput('');
    clearData();
    invoiceInputRef.current?.focus();
    showToast('Screen cleared', 'info');
  };

  // 5. Validation before save matching cmdSave_Click in VB:
  // If Trim(gthisLocationType) = "HQ" Then
  //    If Val(lblTotal.Text) = (lblPaid.Text) Then fncPaymodeDetailSave()
  //    Else MsgBox("Amount Not Tally. Please Key in Excact Amount.")
  // Else
  //    PnlSalesManPwd.Visible = True
  const handleSaveInit = () => {
    if (!invoiceData) {
      showToast('Please select or load an invoice first', 'error');
      return;
    }

    // VB condition: Val(lblTotal.Text) = (lblPaid.Text)
    if (Math.abs(balance) > 0.01) {
      showToast('Amount Not Tally. Please Key in Exact Amount.', 'error');
      return;
    }

    // Prompt for SalesMan Password (PnlSalesManPwd)
    setPwdVisible(true);
    setSalesmanPwd('');
    setTimeout(() => {
      pwdInputRef.current?.focus();
    }, 200);
  };

  // 6. Save execution (fncPaymodeDetailSave + fncSalesPaymodeChangeLogSave)
  const executeSave = async (salesmanCode: string = '') => {
    setPwdVisible(false);
    setSaving(true);
    try {
      const payload = {
        invoiceNo: invoiceData?.InvoiceNo,
        invoiceDate: invoiceData?.InvoiceDate,
        locationCode: invoiceData?.LocationCode || invoiceData?.TerminalCode || 'HQ',
        salesType: invoiceData?.SalesType || 'CASH',
        remarks: remarks,
        paymodes: paymodes,
        userId: 'sys',
        salesmanCode: salesmanCode,
      };

      const response = await fetch(`${API_URL}/save`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload),
      });

      const json = await response.json();
      if (json.success) {
        showToast('Paymode details updated successfully', 'success');
        fetchInvoicesList(false);
        if (invoiceData?.InvoiceNo) {
          fetchInvoice(invoiceData.InvoiceNo);
        }
      } else {
        showToast(json.message || 'Failed to save modifications', 'error');
      }
    } catch (error: any) {
      console.error('Save error:', error);
      showToast('Network error while saving modifications', 'error');
    } finally {
      setSaving(false);
    }
  };

  // 7. Verify salesman password matching subGetPassword in VB
  const verifySalesman = async () => {
    const trimmed = salesmanPwd.trim();
    if (!trimmed) {
      showToast('Please enter SalesMan Password', 'error');
      return;
    }

    setVerifyingPwd(true);
    try {
      const response = await fetch(`${API_URL}/verify-salesman`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ password: trimmed }),
      });
      const json = await response.json();

      if (json.success && json.data) {
        showToast(`Authorized by ${json.data.SalesManName || json.data.SalesManCode}`, 'success');
        executeSave(json.data.SalesManCode);
      } else {
        showToast(json.message || 'SalesMan Password is Incorrect!', 'error');
        setSalesmanPwd('');
        pwdInputRef.current?.focus();
      }
    } catch (error: any) {
      showToast('Verification failed. Check network connection.', 'error');
    } finally {
      setVerifyingPwd(false);
    }
  };

  return (
    <SafeAreaView style={styles.safeArea} edges={['top']}>
      <Stack.Screen options={{ headerShown: false }} />

      {/* Floating Toast Notification */}
      {toast.visible && (
        <View
          style={[
            styles.toastContainer,
            toast.type === 'error'
              ? styles.toastError
              : toast.type === 'success'
                ? styles.toastSuccess
                : styles.toastInfo,
          ]}
        >
          <Ionicons
            name={
              toast.type === 'error'
                ? 'alert-circle'
                : toast.type === 'success'
                  ? 'checkmark-circle'
                  : 'information-circle'
            }
            size={18}
            color="#FFFFFF"
            style={{ marginRight: 8 }}
          />
          <ThemedText style={styles.toastText}>{toast.message}</ThemedText>
        </View>
      )}

      {/* Top Header Bar */}
      <View style={styles.header}>
        <View style={styles.headerLeft}>
          <TouchableOpacity style={styles.backButton} onPress={() => router.back()}>
            <Ionicons name="arrow-back" size={20} color="#3B2D17" />
          </TouchableOpacity>
          <ThemedText style={styles.headerTitle}>Paymode Modification</ThemedText>
        </View>

        <View style={styles.headerActions}>
          <TouchableOpacity
            style={styles.headerBtnSecondary}
            onPress={handleClear}
            activeOpacity={0.7}
          >
            <Ionicons name="refresh-outline" size={14} color="#554428" style={{ marginRight: 4 }} />
            <ThemedText style={styles.headerBtnSecondaryText}>Clear</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity
            style={[styles.headerBtnPrimary, (saving || !invoiceData) && { opacity: 0.6 }]}
            onPress={handleSaveInit}
            disabled={saving || !invoiceData}
            activeOpacity={0.7}
          >
            {saving ? (
              <ActivityIndicator size="small" color="#FFFFFF" />
            ) : (
              <>
                <Ionicons name="save-outline" size={14} color="#FFFFFF" style={{ marginRight: 4 }} />
                <ThemedText style={styles.headerBtnPrimaryText}>Save</ThemedText>
              </>
            )}
          </TouchableOpacity>
        </View>
      </View>

      <KeyboardAvoidingView style={{ flex: 1 }} behavior={Platform.OS === 'ios' ? 'padding' : undefined}>
        <View style={styles.contentContainer}>

          {/* 1. TOP BAR: Invoice No & Remarks Input (SAME SINGLE ROW) */}
          <View style={styles.goldCard}>
            <View style={styles.topInputsRow}>
              {/* Invoice No inline */}
              <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                <ThemedText style={styles.inlineLabel}>Invoice No</ThemedText>
                <TextInput
                  ref={invoiceInputRef}
                  style={[styles.input, { width: 170 }]}
                  value={invoiceNoInput}
                  onChangeText={setInvoiceNoInput}
                  onSubmitEditing={() => fetchInvoice(invoiceNoInput)}
                  placeholder="Key in invoice..."
                  placeholderTextColor="#9E8D77"
                />
                <TouchableOpacity
                  style={styles.searchButton}
                  onPress={() => fetchInvoice(invoiceNoInput)}
                  disabled={loading}
                  activeOpacity={0.8}
                >
                  {loading ? (
                    <ActivityIndicator size="small" color="#7D5E19" />
                  ) : (
                    <>
                      <Ionicons name="search" size={14} color="#7D5E19" style={{ marginRight: 4 }} />
                      <ThemedText style={styles.searchBtnText}>Search</ThemedText>
                    </>
                  )}
                </TouchableOpacity>
              </View>

              {/* Remarks inline */}
              <View style={{ flexDirection: 'row', alignItems: 'center', flex: 1, marginLeft: 20 }}>
                <ThemedText style={styles.inlineLabel}>Remarks</ThemedText>
                <TextInput
                  style={[styles.input, { flex: 1 }]}
                  value={remarks}
                  onChangeText={setRemarks}
                  placeholder="Enter remarks..."
                  placeholderTextColor="#9E8D77"
                  maxLength={100}
                />
              </View>
            </View>
          </View>

          {/* 2. MIDDLE SECTION: Bills Grid (dgvBills in reference) */}
          <View style={[styles.goldCard, styles.billsCard]}>
            {/* Invoices Table Header */}
            <View style={styles.tableHeaderGold}>
              <ThemedText style={[styles.tableHeaderText, { flex: 1.4, paddingHorizontal: 4 }]}>Invoice No.</ThemedText>
              <ThemedText style={[styles.tableHeaderText, { flex: 1.2, paddingHorizontal: 4 }]}>Cust Code</ThemedText>
              <ThemedText style={[styles.tableHeaderText, { flex: 1.1, textAlign: 'center', paddingHorizontal: 4 }]}>Terminal Code</ThemedText>
              <ThemedText style={[styles.tableHeaderText, { flex: 1.2, paddingHorizontal: 4 }]}>SalesMan Code</ThemedText>
              <ThemedText style={[styles.tableHeaderText, { flex: 1.3, paddingHorizontal: 4 }]}>Paymode</ThemedText>
              <ThemedText style={[styles.tableHeaderText, { flex: 1.2, textAlign: 'right', paddingHorizontal: 4 }]}>Net Amount</ThemedText>
              <ThemedText style={[styles.tableHeaderText, { flex: 1.2, textAlign: 'right', paddingHorizontal: 4 }]}>Paid Amt</ThemedText>
              <ThemedText style={[styles.tableHeaderText, { flex: 1, textAlign: 'right', paddingHorizontal: 4 }]}>Change</ThemedText>
            </View>

            {/* Invoices Table Rows */}
            <FlatList
              data={invoicesList}
              keyExtractor={(item) => item.InvoiceNo}
              style={{ flex: 1 }}
              ListEmptyComponent={
                <View style={styles.emptyTableState}>
                  <ThemedText style={styles.emptyTableSubtitle}>
                    {loadingInvoices ? 'Loading bills...' : 'No bills found.'}
                  </ThemedText>
                </View>
              }
              renderItem={({ item, index }) => {
                const isSelected = invoiceData?.InvoiceNo === item.InvoiceNo;
                return (
                  <TouchableOpacity
                    key={item.InvoiceNo}
                    style={[
                      styles.tableRow,
                      index % 2 === 1 && { backgroundColor: '#FCFAF6' },
                      isSelected && styles.tableRowSelectedBlue,
                    ]}
                    onPress={() => handleSelectInvoice(item)}
                    activeOpacity={0.7}
                  >
                    <ThemedText
                      style={[
                        styles.tableCellText,
                        { flex: 1.4, fontWeight: 'bold', paddingHorizontal: 4 },
                        isSelected && styles.tableCellSelectedText,
                      ]}
                    >
                      {item.InvoiceNo}
                    </ThemedText>
                    <ThemedText style={[styles.tableCellText, { flex: 1.2, paddingHorizontal: 4 }, isSelected && styles.tableCellSelectedText]}>
                      {item.CustomerCode || '-'}
                    </ThemedText>
                    <ThemedText style={[styles.tableCellText, { flex: 1.1, textAlign: 'center', paddingHorizontal: 4 }, isSelected && styles.tableCellSelectedText]}>
                      {item.TerminalCode || '-'}
                    </ThemedText>
                    <ThemedText style={[styles.tableCellText, { flex: 1.2, paddingHorizontal: 4 }, isSelected && styles.tableCellSelectedText]}>
                      {item.SalesManName || item.SalesManCode || '-'}
                    </ThemedText>
                    <ThemedText style={[styles.tableCellText, { flex: 1.3, fontWeight: 'bold', paddingHorizontal: 4 }, isSelected && styles.tableCellSelectedText]}>
                      {item.InvoicePaymode || '-'}
                    </ThemedText>
                    <ThemedText
                      style={[
                        styles.tableCellText,
                        { flex: 1.2, textAlign: 'right', fontWeight: 'bold', paddingHorizontal: 4 },
                        isSelected ? styles.tableCellSelectedText : { color: '#9E6D08' },
                      ]}
                    >
                      {parseFloat(item.NetTotal as any || 0).toFixed(2)}
                    </ThemedText>
                    <ThemedText
                      style={[
                        styles.tableCellText,
                        { flex: 1.2, textAlign: 'right', fontWeight: 'bold', paddingHorizontal: 4 },
                        isSelected ? styles.tableCellSelectedText : { color: '#2E7D32' },
                      ]}
                    >
                      {parseFloat(item.PaidAmt as any || 0).toFixed(2)}
                    </ThemedText>
                    <ThemedText style={[styles.tableCellText, { flex: 1, textAlign: 'right', paddingHorizontal: 4 }, isSelected && styles.tableCellSelectedText]}>
                      {parseFloat(item.BalanceAmt as any || 0).toFixed(2)}
                    </ThemedText>
                  </TouchableOpacity>
                );
              }}
            />
          </View>

          {/* 3. BOTTOM SECTION: Paymode Modification Table (Left) & Summary (Right) */}
          <View style={styles.bottomSection}>
            {/* Left: Paymode Modification Grid (dgvPaymode in reference) */}
            <View style={[styles.goldCard, styles.paymodeTableContainer]}>
              {/* Header with columns matching reference image: Paymode | Amount | SlipNo | Amount | SlipNo */}
              <View style={styles.tableHeaderGold}>
                <ThemedText style={[styles.tableHeaderText, { flex: 1.5, paddingHorizontal: 4 }]}>Paymode</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { flex: 1.2, textAlign: 'right', paddingHorizontal: 4 }]}>Amount</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { flex: 1.2, paddingHorizontal: 4 }]}>SlipNo</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { flex: 1.4, textAlign: 'right', paddingHorizontal: 4 }]}>Amount</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { flex: 1.4, paddingHorizontal: 4 }]}>SlipNo</ThemedText>
              </View>

              {/* Paymodes List */}
              <ScrollView style={{ flex: 1 }}>
                {paymodes.length === 0 ? (
                  <View style={styles.emptyTableStateSmall}>
                    <ThemedText style={styles.emptyTableSubtitle}>
                      {invoiceData
                        ? 'Loading paymodes...'
                        : 'Select an invoice above to view payment lines.'}
                    </ThemedText>
                  </View>
                ) : (
                  paymodes.map((item, index) => {
                    const isCash = (item.PayMode || '').toUpperCase() === 'CASH';
                    const hasAmount = parseFloat(item.Amount as any || 0) > 0;
                    return (
                      <View
                        key={item.PayMode}
                        style={[
                          styles.tableRow,
                          index % 2 === 1 && { backgroundColor: '#FCFAF6' },
                          hasAmount && { backgroundColor: '#FDF7E7' },
                        ]}
                      >
                        {/* Paymode Name */}
                        <View style={{ flex: 1.5, paddingHorizontal: 4 }}>
                          <View style={[styles.paymodeNameTag, hasAmount && styles.paymodeNameTagActive]}>
                            <ThemedText
                              style={[
                                styles.tableCellText,
                                { fontWeight: 'bold' },
                                hasAmount && { color: '#8D6409' },
                              ]}
                            >
                              {item.PayMode}
                            </ThemedText>
                          </View>
                        </View>

                        {/* Orig Amount */}
                        <ThemedText style={[styles.tableCellText, { flex: 1.2, textAlign: 'right', paddingHorizontal: 4, color: '#4A3B2C' }]}>
                          {parseFloat(item.Amount as any || 0).toFixed(2)}
                        </ThemedText>

                        {/* Orig Slip No */}
                        <ThemedText style={[styles.tableCellText, { flex: 1.2, paddingHorizontal: 4, color: '#4A3B2C' }]}>
                          {item.SlipNo || ''}
                        </ThemedText>

                        {/* New Amount Input */}
                        <View style={{ flex: 1.4, paddingHorizontal: 4 }}>
                          <TextInput
                            style={[
                              styles.gridInput,
                              { textAlign: 'right', fontWeight: 'bold', borderColor: '#B8903B' },
                            ]}
                            value={String(item.NewAmount)}
                            onChangeText={(val) => updatePaymodeField(index, 'NewAmount', val)}
                            onBlur={() => handleAmountBlur(index)}
                            keyboardType="numeric"
                            selectTextOnFocus
                          />
                        </View>

                        {/* New SlipNo Input (Readonly for Cash matching VB line 622) */}
                        <View style={{ flex: 1.4, paddingHorizontal: 4 }}>
                          <TextInput
                            style={[
                              styles.gridInput,
                              isCash && { backgroundColor: '#F0ECE1', color: '#9E8D77' },
                            ]}
                            value={item.NewSlipNo}
                            onChangeText={(val) => updatePaymodeField(index, 'NewSlipNo', val)}
                            placeholder={isCash ? 'N/A' : ''}
                            placeholderTextColor="#9E8D77"
                            editable={!isCash}
                          />
                        </View>
                      </View>
                    );
                  })
                )}
              </ScrollView>
            </View>

            {/* Right: Summary Panel (matching VB lblInvoice, lblTotal, lblPaid, lblBalance) */}
            <View style={[styles.goldCard, styles.totalsPanel]}>
              <View style={styles.summaryBody}>
                {/* Invoice No */}
                <View style={styles.summaryRow}>
                  <ThemedText style={styles.summaryLabelLarge}>Invoice No</ThemedText>
                  <View style={styles.goldDisplayBox}>
                    <ThemedText style={styles.goldDisplayText}>
                      {invoiceData ? invoiceData.InvoiceNo : ''}
                    </ThemedText>
                  </View>
                </View>

                {/* Net Amount / Total */}
                <View style={styles.summaryRow}>
                  <ThemedText style={styles.summaryLabelLarge}>Net Amount</ThemedText>
                  <View style={styles.goldDisplayBox}>
                    <ThemedText style={styles.goldDisplayText}>
                      {netAmount.toFixed(2)}
                    </ThemedText>
                  </View>
                </View>

                {/* Total Paid */}
                <View style={styles.summaryRow}>
                  <ThemedText style={styles.summaryLabelLarge}>Paid</ThemedText>
                  <View style={styles.goldDisplayBox}>
                    <ThemedText style={styles.goldDisplayText}>
                      {totalPaid.toFixed(2)}
                    </ThemedText>
                  </View>
                </View>

                {/* Balance */}
                <View style={styles.summaryRow}>
                  <ThemedText style={styles.summaryLabelLarge}>Balance</ThemedText>
                  <View style={styles.goldDisplayBox}>
                    <ThemedText style={[styles.goldDisplayText, Math.abs(balance) > 0.01 && { color: '#C62828' }]}>
                      {balance.toFixed(2)}
                    </ThemedText>
                  </View>
                </View>
              </View>
            </View>
          </View>

        </View>
      </KeyboardAvoidingView>

      {/* SalesMan Password Authorization Modal (PnlSalesManPwd) */}
      <Modal visible={pwdVisible} transparent animationType="fade">
        <TouchableOpacity style={styles.modalOverlay} activeOpacity={1} onPress={() => setPwdVisible(false)}>
          <TouchableWithoutFeedback>
            <View style={[styles.modalCard, { maxWidth: 330 }]}>
              <View style={styles.modalHeader}>
                <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                  <View style={styles.headerIconBadge}>
                    <Ionicons name="lock-closed" size={14} color="#B48218" />
                  </View>
                  <ThemedText style={styles.modalTitle}>SalesMan Authorization</ThemedText>
                </View>
                <TouchableOpacity onPress={() => setPwdVisible(false)} style={{ padding: 4 }}>
                  <Ionicons name="close" size={20} color="#554428" />
                </TouchableOpacity>
              </View>

              <View style={{ padding: 16, alignItems: 'center' }}>
                <ThemedText style={{ fontSize: 12, color: '#554428', marginBottom: 12, textAlign: 'center' }}>
                  Enter SalesMan Password to authorize paymode modification
                </ThemedText>
                <TextInput
                  ref={pwdInputRef}
                  style={[
                    styles.input,
                    {
                      width: '100%',
                      textAlign: 'center',
                      fontSize: 14,
                      fontWeight: 'bold',
                      letterSpacing: 2,
                      borderColor: '#B8903B',
                      marginBottom: 16,
                    },
                  ]}
                  value={salesmanPwd}
                  onChangeText={setSalesmanPwd}
                  secureTextEntry
                  placeholder="SalesMan Password"
                  placeholderTextColor="#9E8D77"
                  onSubmitEditing={verifySalesman}
                />
                <View style={{ flexDirection: 'row', gap: 10, width: '100%' }}>
                  <TouchableOpacity
                    style={[styles.headerBtnSecondary, { flex: 1, justifyContent: 'center' }]}
                    onPress={() => setPwdVisible(false)}
                  >
                    <ThemedText style={styles.headerBtnSecondaryText}>Cancel</ThemedText>
                  </TouchableOpacity>
                  <TouchableOpacity
                    style={[styles.headerBtnPrimary, { flex: 1, justifyContent: 'center' }]}
                    onPress={verifySalesman}
                    disabled={verifyingPwd}
                  >
                    {verifyingPwd ? (
                      <ActivityIndicator size="small" color="#FFFFFF" />
                    ) : (
                      <ThemedText style={styles.headerBtnPrimaryText}>Verify & Save</ThemedText>
                    )}
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
  contentContainer: {
    flex: 1,
    padding: 8,
    gap: 8,
  },

  // ===== TOP HEADER BAR =====
  header: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    backgroundColor: '#FFFFFF',
    borderBottomWidth: 1,
    borderBottomColor: '#EBE2D2',
    paddingHorizontal: 12,
    paddingVertical: 8,
  },
  headerLeft: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  backButton: {
    padding: 4,
    marginRight: 8,
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
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#DFD3BC',
    borderRadius: 6,
    paddingHorizontal: 10,
    paddingVertical: 5,
  },
  headerBtnSecondaryText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#554428',
  },
  headerBtnPrimary: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#B48218',
    borderRadius: 6,
    paddingHorizontal: 12,
    paddingVertical: 5,
  },
  headerBtnPrimaryText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#FFFFFF',
  },

  // ===== LUXURY GOLD CARD =====
  goldCard: {
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    shadowColor: '#A38035',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.06,
    shadowRadius: 3,
    elevation: 2,
    overflow: 'hidden',
  },
  topInputsRow: {
    flexDirection: 'row',
    alignItems: 'center',
    padding: 8,
  },
  inputGroup: {
    flexDirection: 'column',
  },
  billsCard: {
    flex: 1,
    minHeight: 150,
  },
  cardHeaderBanner: {
    backgroundColor: '#F5E7CA',
    paddingHorizontal: 10,
    paddingVertical: 5,
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
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
    marginRight: 6,
  },
  cardHeaderTitle: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3B2D17',
    letterSpacing: 0.3,
  },
  tableRefreshBtn: {
    padding: 3,
  },
  counterBadge: {
    backgroundColor: '#FAF5EA',
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderRadius: 10,
    paddingHorizontal: 7,
    paddingVertical: 1,
    marginLeft: 8,
  },
  counterBadgeText: {
    fontSize: 10,
    fontWeight: 'bold',
    color: '#7D5E19',
  },

  // ===== INPUTS =====
  inlineLabel: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#4A3B2C',
    marginRight: 10,
  },
  inputLabel: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#4A3B2C',
    marginBottom: 2,
  },
  input: {
    height: 28,
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderRadius: 6,
    paddingHorizontal: 8,
    paddingVertical: 0,
    fontSize: 12,
    color: '#2B2118',
  },
  searchButton: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FAF5EA',
    borderWidth: 1,
    borderColor: '#DFD3BC',
    borderRadius: 6,
    paddingHorizontal: 10,
    marginLeft: 6,
    height: 28,
  },
  searchBtnText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#7D5E19',
  },

  // ===== TABLES =====
  tableHeaderGold: {
    flexDirection: 'row',
    backgroundColor: '#F5E7CA', // Luxury gold header matching project design system
    paddingVertical: 7,
    paddingHorizontal: 8,
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    alignItems: 'center',
  },
  tableHeaderText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3B2D17',
    letterSpacing: 0.3,
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
  tableRowSelectedBlue: {
    backgroundColor: '#FFF6DF', // Elegant warm gold selection
    borderLeftWidth: 3,
    borderLeftColor: '#B8903B',
  },
  tableCellSelectedText: {
    color: '#8D6409',
    fontWeight: 'bold',
  },
  tableCellText: {
    fontSize: 11,
    color: '#3B2D17',
  },
  paymodeNameTag: {
    paddingVertical: 2,
    paddingHorizontal: 4,
    borderRadius: 4,
  },
  paymodeNameTagActive: {
    backgroundColor: '#FAF5EA',
  },
  emptyTableState: {
    padding: 20,
    alignItems: 'center',
    justifyContent: 'center',
  },
  emptyTableStateSmall: {
    padding: 16,
    alignItems: 'center',
    justifyContent: 'center',
  },
  emptyTableSubtitle: {
    fontSize: 11,
    color: '#8F816E',
    marginTop: 4,
    textAlign: 'center',
  },

  // ===== BOTTOM SECTION =====
  bottomSection: {
    flex: 1.2,
    flexDirection: 'row',
    gap: 8,
  },
  paymodeTableContainer: {
    flex: 1.8,
  },
  gridInput: {
    height: 25,
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderRadius: 5,
    paddingHorizontal: 6,
    paddingVertical: 0,
    fontSize: 11,
    color: '#2B2118',
  },

  // ===== SUMMARY PANEL & BUTTONS =====
  totalsPanel: {
    flex: 1.1,
    backgroundColor: '#FFFFFF',
  },
  summaryBody: {
    padding: 12,
    gap: 8,
  },
  summaryRow: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
  },
  summaryLabelLarge: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#4A3B2C',
  },
  goldDisplayBox: {
    width: 140,
    height: 30,
    backgroundColor: '#F8F5EE', // Sleek ivory luxury card input
    borderRadius: 6,
    borderWidth: 1,
    borderColor: '#EADBCA',
    justifyContent: 'center',
    alignItems: 'flex-end',
    paddingHorizontal: 10,
  },
  goldDisplayText: {
    fontSize: 13,
    fontWeight: 'bold',
    color: '#2B2118',
  },
  bottomButtonsStack: {
    marginTop: 10,
    gap: 6,
  },
  bigActionBtnPrimary: {
    backgroundColor: '#B48218',
    borderRadius: 6,
    paddingVertical: 8,
    alignItems: 'center',
    justifyContent: 'center',
    shadowColor: '#B48218',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.2,
    shadowRadius: 2,
    elevation: 2,
  },
  bigActionBtnPrimaryText: {
    color: '#FFFFFF',
    fontWeight: 'bold',
    fontSize: 12,
  },
  bigActionBtnSecondary: {
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#DFD3BC',
    borderRadius: 6,
    paddingVertical: 7,
    alignItems: 'center',
    justifyContent: 'center',
  },
  bigActionBtnSecondaryText: {
    color: '#554428',
    fontWeight: 'bold',
    fontSize: 12,
  },

  // ===== MODAL STYLING =====
  modalOverlay: {
    flex: 1,
    backgroundColor: 'rgba(0,0,0,0.4)',
    justifyContent: 'center',
    alignItems: 'center',
    padding: 20,
  },
  modalCard: {
    width: '100%',
    maxWidth: 400,
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    overflow: 'hidden',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.15,
    shadowRadius: 10,
    elevation: 6,
  },
  modalHeader: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    paddingHorizontal: 12,
    paddingVertical: 8,
    backgroundColor: '#F5E7CA',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
  },
  modalTitle: {
    fontSize: 13,
    fontWeight: 'bold',
    color: '#3B2D17',
  },

  // ===== TOAST NOTIFICATION =====
  toastContainer: {
    position: 'absolute',
    top: 50,
    alignSelf: 'center',
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 16,
    paddingVertical: 9,
    borderRadius: 8,
    zIndex: 9999,
    shadowColor: '#000',
    shadowOpacity: 0.15,
    shadowRadius: 8,
    elevation: 6,
  },
  toastSuccess: {
    backgroundColor: '#2E7D32',
  },
  toastError: {
    backgroundColor: '#C62828',
  },
  toastInfo: {
    backgroundColor: '#B48218',
  },
  toastText: {
    color: '#FFFFFF',
    fontWeight: 'bold',
    fontSize: 12,
  },
});
