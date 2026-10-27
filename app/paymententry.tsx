import { ThemedText } from '@/components/themed-text';
import { API_BASE_URL } from '@/constants/api';
import { Ionicons } from '@expo/vector-icons';
import { Stack, useFocusEffect, useRouter } from 'expo-router';
import React, { useCallback, useEffect, useRef, useState } from 'react';
import {
  ActivityIndicator,
  Alert,
  FlatList,
  Modal,
  Platform,
  StyleSheet,
  TextInput,
  TouchableOpacity,
  View,
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';

const API_URL = API_BASE_URL;

// Toast Component
type ToastProps = {
  message: string;
  type: 'success' | 'error' | 'info';
  visible: boolean;
  onHide: () => void;
};

const Toast: React.FC<ToastProps> = ({ message, type, visible, onHide }) => {
  useEffect(() => {
    if (visible) {
      const timer = setTimeout(() => {
        onHide();
      }, 3500);
      return () => clearTimeout(timer);
    }
  }, [visible, onHide]);

  if (!visible) return null;

  return (
    <View
      style={[
        styles.toastContainer,
        type === 'error'
          ? styles.toastError
          : type === 'info'
            ? styles.toastInfo
            : styles.toastSuccess,
      ]}
    >
      <Ionicons
        name={
          type === 'error'
            ? 'alert-circle'
            : type === 'info'
              ? 'information-circle'
              : 'checkmark-circle'
        }
        size={18}
        color="#FFFFFF"
        style={{ marginRight: 8 }}
      />
      <ThemedText style={styles.toastText}>{message}</ThemedText>
    </View>
  );
};

interface PayModeItem {
  Paymode: string;
  Amount: string;
  SlipNo: string;
  NewAmount: string;
  NewSlipNo: string;
  isNew?: boolean;
}

export default function PaymentEntry() {
  const router = useRouter();
  const searchInputRef = useRef<TextInput>(null);

  // Loading & Toast State
  const [isLoading, setIsLoading] = useState(false);
  const [toast, setToast] = useState<{
    visible: boolean;
    message: string;
    type: 'success' | 'error' | 'info';
  }>({ visible: false, message: '', type: 'success' });

  // Mode Selection: 'Invoice' | 'Flexi' | 'eTRS'
  const [mode, setMode] = useState<'Invoice' | 'Flexi' | 'eTRS'>('Invoice');
  const [searchInvoiceNo, setSearchInvoiceNo] = useState('');

  // Grid Data
  const [invoices, setInvoices] = useState<any[]>([]);
  const [selectedInvoice, setSelectedInvoice] = useState<any | null>(null);
  const [paymodes, setPaymodes] = useState<PayModeItem[]>([]);

  // Calculation & Summary States matching reference
  const [netAmount, setNetAmount] = useState(0);
  const [paidAmount, setPaidAmount] = useState(0);
  const [balanceAmount, setBalanceAmount] = useState(0);
  const [gstAmount, setGstAmount] = useState(0);

  // LOV Modal for selecting/adding Paymode
  const [isLovVisible, setIsLovVisible] = useState(false);
  const [lovList, setLovList] = useState<any[]>([]);
  const [lovSearch, setLovSearch] = useState('');
  const [isLovLoading, setIsLovLoading] = useState(false);

  // New paymode row quick add state
  const [newPaymodeName, setNewPaymodeName] = useState('');
  const [newPaymodeAmount, setNewPaymodeAmount] = useState('');
  const [newPaymodeSlipNo, setNewPaymodeSlipNo] = useState('');

  const showToast = (message: string, type: 'success' | 'error' | 'info' = 'success') => {
    setToast({ visible: true, message, type });
  };

  // 1. Fetch Invoices list or search
  const fetchInvoices = async (fetchMode = mode, search = '') => {
    setIsLoading(true);
    try {
      let endpoint = '';
      const trimmedSearch = search.trim();
      if (trimmedSearch) {
        endpoint = `/paymententry/invoice/${encodeURIComponent(trimmedSearch)}`;
      } else if (fetchMode === 'Invoice') {
        endpoint = '/paymententry/invoices';
      } else if (fetchMode === 'Flexi') {
        endpoint = '/paymententry/flexi';
      } else if (fetchMode === 'eTRS') {
        endpoint = '/paymententry/etrs';
      }

      const response = await fetch(`${API_URL}${endpoint}`);
      const data = await response.json();
      if (data.success && Array.isArray(data.data)) {
        setInvoices(data.data);
        if (trimmedSearch && data.data.length > 0) {
          // If searched and results found, auto-select first matching bill
          handleSelectInvoice(data.data[0]);
        }
      } else {
        setInvoices([]);
        showToast(data.message || 'No records returned', 'info');
      }
    } catch (error) {
      console.error('Error fetching invoices:', error);
      showToast('Network error fetching invoices. Check connection.', 'error');
    } finally {
      setIsLoading(false);
    }
  };

  // Initial load and on mode toggle
  useEffect(() => {
    setSearchInvoiceNo('');
    clearPaymodeGrid();
    fetchInvoices(mode);
  }, [mode]);

  // Recalculate totals according to reference VB formula:
  // For each row: lblPaid += Val(cNewAmount), lblBalance = Val(lblTotal) - (Val(lblPaid) + Val(lblGST))
  // actualPaidAmount = Val(lblPaid)
  // displayed paidAmount = Val(lblPaid) + Val(lblGST)
  const recalculateTotals = (currentPaymodes: PayModeItem[], currentNet: number, currentGst: number) => {
    let sumNewAmt = 0;
    currentPaymodes.forEach((pm) => {
      const val = parseFloat(pm.NewAmount) || 0;
      sumNewAmt += val;
    });

    const totalPaidWithGst = sumNewAmt + currentGst;
    const balance = currentNet - totalPaidWithGst;

    setPaidAmount(totalPaidWithGst);
    setBalanceAmount(balance);
  };

  // 2. Fetch Paymodes when an invoice is clicked/selected
  const fetchPaymodes = async (invoice: any) => {
    if (!invoice) return;
    setIsLoading(true);
    try {
      const invNo = invoice.InvoiceNo ? String(invoice.InvoiceNo).trim() : '';
      const response = await fetch(`${API_URL}/paymententry/paymode/${encodeURIComponent(invNo)}`);
      const data = await response.json();

      const net = parseFloat(String(invoice.NetTotal || '0'));
      const gst = parseFloat(String(invoice.GST || invoice.Gst || '0'));
      setNetAmount(net);
      setGstAmount(gst);

      if (data.success && Array.isArray(data.data)) {
        // Prepare paymode items with NewAmount and NewSlipNo
        const mappedPaymodes: PayModeItem[] = data.data.map((pm: any) => {
          const originalAmt = parseFloat(String(pm.Amount || '0')).toFixed(2);
          const originalSlip = pm.SlipNo ? String(pm.SlipNo).trim() : '';
          return {
            Paymode: pm.Paymode || pm.PayMode || '',
            Amount: originalAmt,
            SlipNo: originalSlip,
            NewAmount: originalAmt, // default initialized to existing amount for editing
            NewSlipNo: originalSlip,
            isNew: false,
          };
        });

        setPaymodes(mappedPaymodes);
        recalculateTotals(mappedPaymodes, net, gst);
      } else {
        setPaymodes([]);
        setPaidAmount(gst);
        setBalanceAmount(net - gst);
      }
    } catch (error) {
      console.error('Error fetching paymodes:', error);
      showToast('Error loading payment mode details', 'error');
    } finally {
      setIsLoading(false);
    }
  };

  const handleSelectInvoice = (item: any) => {
    setSelectedInvoice(item);
    fetchPaymodes(item);
  };

  // Paymode inline change handler
  const handlePaymodeChange = (index: number, field: 'NewAmount' | 'NewSlipNo', value: string) => {
    const updated = [...paymodes];
    updated[index] = { ...updated[index], [field]: value };
    setPaymodes(updated);
    if (field === 'NewAmount') {
      recalculateTotals(updated, netAmount, gstAmount);
    }
  };

  // Add new paymode row
  const handleAddNewPaymode = () => {
    if (!selectedInvoice) {
      Alert.alert('Validation', 'Please select an invoice first.');
      return;
    }
    const modeName = newPaymodeName.trim().toUpperCase();
    if (!modeName) {
      Alert.alert('Validation', 'Please specify a Paymode.');
      return;
    }
    const amtNum = parseFloat(newPaymodeAmount);
    if (isNaN(amtNum) || amtNum <= 0) {
      Alert.alert('Validation', 'Please key in a valid Amount.');
      return;
    }

    const newRow: PayModeItem = {
      Paymode: modeName,
      Amount: '0.00',
      SlipNo: '',
      NewAmount: amtNum.toFixed(2),
      NewSlipNo: newPaymodeSlipNo.trim(),
      isNew: true,
    };

    const updated = [...paymodes, newRow];
    setPaymodes(updated);
    recalculateTotals(updated, netAmount, gstAmount);

    setNewPaymodeName('');
    setNewPaymodeAmount('');
    setNewPaymodeSlipNo('');
  };

  const handleRemovePaymode = (index: number) => {
    const updated = [...paymodes];
    updated.splice(index, 1);
    setPaymodes(updated);
    recalculateTotals(updated, netAmount, gstAmount);
  };

  // Fetch LOV options
  const handleOpenPaymodeLOV = async () => {
    setIsLovVisible(true);
    setIsLovLoading(true);
    setLovSearch('');
    try {
      const response = await fetch(`${API_URL}/paymententry/paymodes-lov`);
      const data = await response.json();
      if (data.success && Array.isArray(data.data)) {
        setLovList(data.data);
      } else {
        setLovList([
          { Paymode: 'CASH' },
          { Paymode: 'NETS' },
          { Paymode: 'VISA' },
          { Paymode: 'MASTER' },
          { Paymode: 'AMEX' },
          { Paymode: 'CHEQUE' },
          { Paymode: 'TRANSFER' },
        ]);
      }
    } catch {
      setLovList([
        { Paymode: 'CASH' },
        { Paymode: 'NETS' },
        { Paymode: 'VISA' },
        { Paymode: 'MASTER' },
        { Paymode: 'AMEX' },
        { Paymode: 'CHEQUE' },
      ]);
    } finally {
      setIsLovLoading(false);
    }
  };

  const handleSelectFromLov = (selectedMode: string) => {
    setNewPaymodeName(selectedMode);
    setIsLovVisible(false);
  };

  // Validation function matching VB fncValiDate()
  const validateBeforeSave = (): boolean => {
    if (!selectedInvoice) {
      showToast('Please select an invoice first.', 'error');
      if (Platform.OS !== 'web') {
        Alert.alert('Validation Error', 'Please select an invoice first.');
      }
      return false;
    }

    // Check Amount tally: Net Amount must equal Paid Amount
    if (Math.abs(netAmount - paidAmount) > 0.01) {
      showToast('Amount Not Tally. Please Key in Exact Amount.', 'error');
      if (Platform.OS !== 'web') {
        Alert.alert('Validation Error', 'Amount Not Tally. Please Key in Exact Amount.');
      }
      return false;
    }

    return true;
  };

  // 3. Save Payment
  const handleSave = async () => {
    if (!validateBeforeSave()) return;

    setIsLoading(true);
    try {
      // actual paid amount excluding GST calculation if any
      const actualPaid = paidAmount - gstAmount;

      const payload = {
        type: mode,
        invoiceNo: selectedInvoice.InvoiceNo,
        paymodes: paymodes,
        actualPaidAmount: actualPaid,
        gst: gstAmount,
        userId: 'ADMIN',
      };

      const response = await fetch(`${API_URL}/paymententry/save`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload),
      });

      const data = await response.json();
      if (data.success) {
        showToast('Payment saved successfully!', 'success');
        clearPaymodeGrid();
        fetchInvoices(mode);
      } else {
        const errorMsg = data.message || 'Unknown server error';
        console.error('Save failed:', errorMsg);
        showToast(`Failed to save: ${errorMsg}`, 'error');
        if (Platform.OS !== 'web') {
          Alert.alert('Save Failed', errorMsg);
        }
      }
    } catch (error: any) {
      console.error('Error saving payment:', error);
      showToast(`Network error: ${error?.message || 'Check backend connection.'}`, 'error');
      if (Platform.OS !== 'web') {
        Alert.alert('Error', `Network error saving payment: ${error?.message || ''}`);
      }
    } finally {
      setIsLoading(false);
    }
  };

  // 4. Open Cash Drawer
  const handleOpenCashDrawer = async () => {
    try {
      const response = await fetch(`${API_URL}/paymententry/cashdrawer`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          terminalId: '1',
          terminalCode: 'HQ',
          userId: 'ADMIN',
        }),
      });
      const data = await response.json();
      if (data.success) {
        showToast('Cash drawer command executed.', 'success');
      } else {
        showToast('Cash drawer trigger failed.', 'error');
      }
    } catch (error) {
      showToast('Failed to open cash drawer.', 'error');
    }
  };

  // Clear Form
  const clearPaymodeGrid = () => {
    setSelectedInvoice(null);
    setPaymodes([]);
    setNetAmount(0);
    setPaidAmount(0);
    setBalanceAmount(0);
    setGstAmount(0);
    setNewPaymodeName('');
    setNewPaymodeAmount('');
    setNewPaymodeSlipNo('');
  };

  const handleClear = () => {
    setSearchInvoiceNo('');
    clearPaymodeGrid();
    fetchInvoices(mode);
  };

  // Search invoice
  const handleSearchInvoice = () => {
    if (searchInvoiceNo.trim()) {
      fetchInvoices(mode, searchInvoiceNo.trim());
    } else {
      fetchInvoices(mode);
    }
  };

  // Keyboard Shortcuts (F4: Save, F6: Clear, F8: Cash Drawer)
  useFocusEffect(
    useCallback(() => {
      if (Platform.OS === 'web') {
        const handleKeyDown = (e: KeyboardEvent) => {
          if (e.key === 'F4') {
            e.preventDefault();
            handleSave();
          } else if (e.key === 'F6') {
            e.preventDefault();
            handleClear();
          } else if (e.key === 'F8') {
            e.preventDefault();
            handleOpenCashDrawer();
          } else if (e.key === 'Escape') {
            e.preventDefault();
            router.back();
          }
        };
        window.addEventListener('keydown', handleKeyDown);
        return () => window.removeEventListener('keydown', handleKeyDown);
      }
    }, [selectedInvoice, paymodes, netAmount, paidAmount, gstAmount, mode])
  );

  return (
    <SafeAreaView edges={['top']} style={styles.safeArea}>
      <Stack.Screen options={{ headerShown: false }} />

      {/* TOP HEADER BAR */}
      <View style={styles.header}>
        <View style={styles.headerLeft}>
          <TouchableOpacity onPress={() => router.back()} style={styles.backButton}>
            <Ionicons name="arrow-back" size={20} color="#3B2D17" />
          </TouchableOpacity>
          <ThemedText style={styles.headerTitle}>Payment Entry</ThemedText>
        </View>

        <View style={styles.headerActions}>
          <TouchableOpacity
            style={styles.headerBtnSecondary}
            onPress={handleOpenCashDrawer}
            activeOpacity={0.7}
          >
            <Ionicons name="archive-outline" size={14} color="#554428" style={{ marginRight: 4 }} />
            <ThemedText style={styles.headerBtnSecondaryText}>Cash Drawer</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity
            style={styles.headerBtnSecondary}
            onPress={handleClear}
            activeOpacity={0.7}
          >
            <Ionicons name="refresh-outline" size={14} color="#554428" style={{ marginRight: 4 }} />
            <ThemedText style={styles.headerBtnSecondaryText}>Clear</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity
            style={[styles.headerBtnPrimary, isLoading && { opacity: 0.7 }]}
            onPress={handleSave}
            disabled={isLoading}
            activeOpacity={0.7}
          >
            {isLoading ? (
              <ActivityIndicator size="small" color="#FFFFFF" style={{ marginRight: 4 }} />
            ) : (
              <Ionicons name="save-outline" size={14} color="#FFFFFF" style={{ marginRight: 4 }} />
            )}
            <ThemedText style={styles.headerBtnPrimaryText}>Save</ThemedText>
          </TouchableOpacity>
        </View>
      </View>

      {/* Loading Overlay */}
      {isLoading && (
        <View style={styles.loadingOverlay}>
          <ActivityIndicator size="large" color="#B48218" />
        </View>
      )}

      {/* Toast Notification */}
      <Toast
        visible={toast.visible}
        message={toast.message}
        type={toast.type}
        onHide={() => setToast({ ...toast, visible: false })}
      />

      <View style={styles.contentContainer}>
        {/* TOP SEARCH & FILTER BAR (SINGLE ROW) */}
        <View style={styles.goldCard}>
          <View style={styles.singleRowCard}>
            {/* Title / Badge */}
            <View style={{ flexDirection: 'row', alignItems: 'center', marginRight: 16 }}>
              <View style={styles.headerIconBadge}>
                <Ionicons name="search" size={14} color="#B48218" />
              </View>
              <ThemedText style={styles.cardHeaderTitle}>Search & Mode</ThemedText>
            </View>

            {/* Invoice No Search Input & Button */}
            <View style={{ flexDirection: 'row', alignItems: 'center', flex: 1 }}>
              <ThemedText style={styles.searchLabel}>Invoice No</ThemedText>
              <TextInput
                ref={searchInputRef}
                style={[styles.input, { width: 180, maxWidth: 220 }]}
                placeholder="Key in invoice number..."
                placeholderTextColor="#9E8D77"
                value={searchInvoiceNo}
                onChangeText={setSearchInvoiceNo}
                onSubmitEditing={handleSearchInvoice}
              />
              <TouchableOpacity
                style={styles.searchBtn}
                onPress={handleSearchInvoice}
                activeOpacity={0.7}
              >
                <Ionicons name="search-outline" size={14} color="#7D5E19" style={{ marginRight: 4 }} />
                <ThemedText style={styles.searchBtnText}>Search</ThemedText>
              </TouchableOpacity>

              {searchInvoiceNo.length > 0 && (
                <TouchableOpacity
                  style={[styles.searchBtn, { backgroundColor: '#F8EBE8', borderColor: '#E8C4BE', marginLeft: 4 }]}
                  onPress={() => {
                    setSearchInvoiceNo('');
                    fetchInvoices(mode, '');
                  }}
                  activeOpacity={0.7}
                >
                  <Ionicons name="close-circle-outline" size={14} color="#C62828" />
                </TouchableOpacity>
              )}
            </View>

            {/* Mode Radios: Invoice / Flexi / eTRS */}
            <View style={styles.pillBox}>
              {(['Invoice', 'Flexi', 'eTRS'] as const).map((option) => (
                <TouchableOpacity
                  key={option}
                  style={styles.radioContainer}
                  onPress={() => setMode(option)}
                  activeOpacity={0.7}
                >
                  <View style={[styles.radioCircle, mode === option && styles.radioCircleSelected]}>
                    {mode === option && <View style={styles.radioInner} />}
                  </View>
                  <ThemedText style={styles.radioLabel}>{option}</ThemedText>
                </TouchableOpacity>
              ))}
            </View>
          </View>
        </View>

        {/* MIDDLE SECTION: INVOICES GRID */}
        <View style={[styles.goldCard, { flex: 1.1, marginTop: 8 }]}>
          <View style={styles.cardHeaderBanner}>
            <View style={{ flexDirection: 'row', alignItems: 'center' }}>
              <View style={styles.headerIconBadge}>
                <Ionicons name="receipt-outline" size={14} color="#B48218" />
              </View>
              <ThemedText style={styles.cardHeaderTitle}>
                {mode === 'Invoice' ? 'Bills (Invoices)' : mode === 'Flexi' ? 'Flexi Details' : 'eTRS Statement Invoices'}
              </ThemedText>
              <View style={styles.counterBadge}>
                <ThemedText style={styles.counterBadgeText}>{invoices.length} Records</ThemedText>
              </View>
            </View>
          </View>

          <View style={{ flex: 1, width: '100%' }}>
            {/* Table Header */}
            <View style={styles.tableHeader}>
              <ThemedText style={[styles.tableHeaderText, { flex: 1.5, paddingHorizontal: 4 }]}>Invoice No</ThemedText>
              <ThemedText style={[styles.tableHeaderText, { flex: 1.2, textAlign: 'center', paddingHorizontal: 4 }]}>Date</ThemedText>
              <ThemedText style={[styles.tableHeaderText, { flex: 1.2, paddingHorizontal: 4 }]}>
                {mode === 'Flexi' ? 'NRIC No' : 'Cust Code'}
              </ThemedText>
              {mode === 'Invoice' && (
                <>
                  <ThemedText style={[styles.tableHeaderText, { flex: 1, textAlign: 'center', paddingHorizontal: 4 }]}>Term</ThemedText>
                  <ThemedText style={[styles.tableHeaderText, { flex: 1.2, paddingHorizontal: 4 }]}>Salesman</ThemedText>
                  <ThemedText style={[styles.tableHeaderText, { flex: 1.5, paddingHorizontal: 4 }]}>Paymode</ThemedText>
                </>
              )}
              {mode === 'Flexi' && (
                <>
                  <ThemedText style={[styles.tableHeaderText, { flex: 1.2, paddingHorizontal: 4 }]}>Flexi Type</ThemedText>
                  <ThemedText style={[styles.tableHeaderText, { flex: 1.2, paddingHorizontal: 4 }]}>Paymode</ThemedText>
                </>
              )}
              {mode === 'eTRS' && (
                <ThemedText style={[styles.tableHeaderText, { flex: 2, paddingHorizontal: 4 }]}>Remarks</ThemedText>
              )}
              <ThemedText style={[styles.tableHeaderText, { flex: 1.2, textAlign: 'right', paddingHorizontal: 4 }]}>Net Amount</ThemedText>
              <ThemedText style={[styles.tableHeaderText, { flex: 1.1, textAlign: 'right', paddingHorizontal: 4 }]}>Paid Amt</ThemedText>
              {mode === 'Invoice' && (
                <ThemedText style={[styles.tableHeaderText, { flex: 1, textAlign: 'right', paddingHorizontal: 4 }]}>GST</ThemedText>
              )}
            </View>

            {/* Invoices List */}
            <FlatList
              data={invoices}
              keyExtractor={(item, idx) => item.InvoiceNo?.toString() || idx.toString()}
              ListEmptyComponent={
                <View style={styles.emptyTableState}>
                  <Ionicons name="document-text-outline" size={32} color="#D0C2A8" />
                  <ThemedText style={styles.emptyTableTitle}>No records found</ThemedText>
                  <ThemedText style={styles.emptyTableSubtitle}>
                    Select a mode or enter an invoice number to search
                  </ThemedText>
                </View>
              }
              renderItem={({ item, index }) => {
                const isSelected = selectedInvoice?.InvoiceNo === item.InvoiceNo;
                return (
                  <TouchableOpacity
                    style={[
                      styles.tableRow,
                      index % 2 === 1 && { backgroundColor: '#FCFAF6' },
                      isSelected && styles.tableRowSelected,
                    ]}
                    activeOpacity={0.7}
                    onPress={() => handleSelectInvoice(item)}
                  >
                    <ThemedText style={[styles.tableCellText, { flex: 1.5, fontWeight: 'bold', color: '#8D6409' }]}>
                      {item.InvoiceNo}
                    </ThemedText>
                    <ThemedText style={[styles.tableCellText, { flex: 1.2, textAlign: 'center' }]}>
                      {item.InvoiceDate ? String(item.InvoiceDate).split('T')[0] : '-'}
                    </ThemedText>
                    <ThemedText style={[styles.tableCellText, { flex: 1.2 }]} numberOfLines={1}>
                      {item.CustomerCode || item.NricNo || '-'}
                    </ThemedText>

                    {mode === 'Invoice' && (
                      <>
                        <ThemedText style={[styles.tableCellText, { flex: 1, textAlign: 'center' }]}>
                          {item.TerminalCode || '-'}
                        </ThemedText>
                        <ThemedText style={[styles.tableCellText, { flex: 1.2 }]} numberOfLines={1}>
                          {item.SalesManName || '-'}
                        </ThemedText>
                        <ThemedText style={[styles.tableCellText, { flex: 1.5, color: '#685022' }]} numberOfLines={1}>
                          {item.InvoicePaymode || '-'}
                        </ThemedText>
                      </>
                    )}

                    {mode === 'Flexi' && (
                      <>
                        <ThemedText style={[styles.tableCellText, { flex: 1.2 }]} numberOfLines={1}>
                          {item.FlexiType || '-'}
                        </ThemedText>
                        <ThemedText style={[styles.tableCellText, { flex: 1.2 }]} numberOfLines={1}>
                          {item.PayMode || '-'}
                        </ThemedText>
                      </>
                    )}

                    {mode === 'eTRS' && (
                      <ThemedText style={[styles.tableCellText, { flex: 2 }]} numberOfLines={1}>
                        {item.Remarks || '-'}
                      </ThemedText>
                    )}

                    <ThemedText style={[styles.tableCellText, { flex: 1.2, textAlign: 'right', fontWeight: 'bold', color: '#9E6D08' }]}>
                      {parseFloat(String(item.NetTotal || item.Amount || '0')).toFixed(2)}
                    </ThemedText>

                    <ThemedText style={[styles.tableCellText, { flex: 1.1, textAlign: 'right', color: '#2E7D32' }]}>
                      {parseFloat(String(item.PaidAmt || item.Amount || '0')).toFixed(2)}
                    </ThemedText>

                    {mode === 'Invoice' && (
                      <ThemedText style={[styles.tableCellText, { flex: 1, textAlign: 'right', color: '#554736' }]}>
                        {parseFloat(String(item.GST || '0')).toFixed(2)}
                      </ThemedText>
                    )}
                  </TouchableOpacity>
                );
              }}
            />
          </View>
        </View>

        {/* BOTTOM SECTION: PAYMODES BREAKDOWN & SETTLEMENT SUMMARY */}
        <View style={styles.bottomSection}>
          {/* Left: Paymodes Grid with editable NewAmount & NewSlipNo */}
          <View style={[styles.goldCard, styles.paymodeContainer]}>
            <View style={styles.cardHeaderBanner}>
              <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                <View style={styles.headerIconBadge}>
                  <Ionicons name="card-outline" size={14} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitle}>Payment Modes Breakdown</ThemedText>
                <View style={styles.counterBadge}>
                  <ThemedText style={styles.counterBadgeText}>{paymodes.length} Items</ThemedText>
                </View>
              </View>

              {/* <TouchableOpacity
                style={styles.lovTriggerBtn}
                onPress={handleOpenPaymodeLOV}
                activeOpacity={0.7}
              >
                <Ionicons name="list" size={13} color="#7D5E19" style={{ marginRight: 3 }} />
                <ThemedText style={styles.lovTriggerBtnText}>Pick Paymode</ThemedText>
              </TouchableOpacity> */}
            </View>

            <View style={{ flex: 1 }}>
              {/* Paymodes Table Header matching dgvPaymode */}
              <View style={styles.tableHeader}>
                <ThemedText style={[styles.tableHeaderText, { flex: 1.8, paddingHorizontal: 4 }]}>Paymode</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { flex: 1.4, textAlign: 'right', paddingHorizontal: 4 }]}>Amount</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { flex: 1.5, paddingHorizontal: 6 }]}>Slip No</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { flex: 1.6, textAlign: 'right', paddingHorizontal: 4 }]}>New Amount</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { flex: 1.6, paddingHorizontal: 6 }]}>New Slip No</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { flex: 0.6, textAlign: 'center', paddingHorizontal: 2 }]}>Act</ThemedText>
              </View>

              {/* Paymodes List */}
              <FlatList
                data={paymodes}
                keyExtractor={(item, idx) => idx.toString()}
                ListEmptyComponent={
                  <View style={styles.emptyTableStateSmall}>
                    <ThemedText style={styles.emptyTableSubtitle}>
                      {selectedInvoice
                        ? 'No payment modes recorded. Key in below to add.'
                        : 'Select an invoice above to view and amend payment modes.'}
                    </ThemedText>
                  </View>
                }
                renderItem={({ item, index }) => {
                  const isCash = (item.Paymode || '').toUpperCase().trim() === 'CASH';
                  return (
                    <View
                      style={[
                        styles.tableRow,
                        index % 2 === 1 && { backgroundColor: '#FCFAF6' },
                      ]}
                    >
                      <ThemedText style={[styles.tableCellText, { flex: 1.8, fontWeight: 'bold', paddingHorizontal: 4 }]}>
                        {item.Paymode}
                      </ThemedText>
                      <ThemedText style={[styles.tableCellText, { flex: 1.4, textAlign: 'right', color: '#554736', paddingHorizontal: 4 }]}>
                        {parseFloat(String(item.Amount || '0')).toFixed(2)}
                      </ThemedText>
                      <ThemedText style={[styles.tableCellText, { flex: 1.5, color: '#776652', paddingHorizontal: 6 }]} numberOfLines={1}>
                        {item.SlipNo || '-'}
                      </ThemedText>

                      {/* Editable New Amount */}
                      <View style={{ flex: 1.6, paddingHorizontal: 3 }}>
                        <TextInput
                          style={[styles.input, styles.editableGridInput, { textAlign: 'right' }]}
                          keyboardType="numeric"
                          value={item.NewAmount}
                          onChangeText={(val) => handlePaymodeChange(index, 'NewAmount', val)}
                          placeholder="0.00"
                        />
                      </View>

                      {/* Editable New Slip No (Disabled for Cash as per VB code) */}
                      <View style={{ flex: 1.6, paddingHorizontal: 3 }}>
                        <TextInput
                          style={[
                            styles.input,
                            styles.editableGridInput,
                            isCash && styles.readonlyInput,
                          ]}
                          editable={!isCash}
                          value={item.NewSlipNo}
                          onChangeText={(val) => handlePaymodeChange(index, 'NewSlipNo', val)}
                          placeholder={isCash ? 'N/A' : 'Slip #'}
                        />
                      </View>

                      <View style={{ flex: 0.6, alignItems: 'center', justifyContent: 'center' }}>
                        {item.isNew ? (
                          <TouchableOpacity
                            onPress={() => handleRemovePaymode(index)}
                            activeOpacity={0.7}
                            style={{ padding: 4 }}
                          >
                            <Ionicons name="trash-outline" size={15} color="#D93829" />
                          </TouchableOpacity>
                        ) : (
                          <Ionicons name="checkmark" size={14} color="#8D6409" />
                        )}
                      </View>
                    </View>
                  );
                }}
              />

              {/* Bottom Quick Row to Add New Paymode */}
              <View style={styles.paymodeEntryBar}>
                <View style={{ flex: 1.8, marginRight: 6 }}>
                  <ThemedText style={styles.entryLabel}>Paymode</ThemedText>
                  <View style={{ flexDirection: 'row', alignItems: 'center', gap: 4 }}>
                    <TextInput
                      style={[styles.input, { flex: 1 }]}
                      placeholder="e.g. CASH"
                      placeholderTextColor="#9E8D77"
                      value={newPaymodeName}
                      onChangeText={setNewPaymodeName}
                    />
                    <TouchableOpacity
                      style={styles.entryPickBtn}
                      onPress={handleOpenPaymodeLOV}
                      activeOpacity={0.7}
                      accessibilityLabel="Pick Paymode"
                    >
                      <Ionicons name="list" size={14} color="#7D5E19" />
                    </TouchableOpacity>
                  </View>
                </View>

                <View style={{ flex: 1.5, marginRight: 6 }}>
                  <ThemedText style={styles.entryLabel}>Amount</ThemedText>
                  <TextInput
                    style={[styles.input, { textAlign: 'right' }]}
                    placeholder="0.00"
                    placeholderTextColor="#9E8D77"
                    keyboardType="numeric"
                    value={newPaymodeAmount}
                    onChangeText={setNewPaymodeAmount}
                  />
                </View>

                <View style={{ flex: 1.5, marginRight: 6 }}>
                  <ThemedText style={styles.entryLabel}>Slip No</ThemedText>
                  <TextInput
                    style={styles.input}
                    placeholder="Slip / Ref #"
                    placeholderTextColor="#9E8D77"
                    value={newPaymodeSlipNo}
                    onChangeText={setNewPaymodeSlipNo}
                  />
                </View>

                <View style={{ justifyContent: 'flex-end', height: 48 }}>
                  <TouchableOpacity
                    style={styles.addPaymodeBtn}
                    onPress={handleAddNewPaymode}
                    activeOpacity={0.8}
                  >
                    <Ionicons name="add" size={15} color="#7D5E19" style={{ marginRight: 2 }} />
                    <ThemedText style={styles.addPaymodeBtnText}>Add</ThemedText>
                  </TouchableOpacity>
                </View>
              </View>
            </View>
          </View>

          {/* Right: Summary Card matching VB Labels (lblInvoice, lblTotal, lblPaid, lblBalance, lblGST) */}
          <View style={[styles.goldCard, styles.summaryContainer]}>
            <View style={styles.cardHeaderBanner}>
              <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                <View style={styles.headerIconBadge}>
                  <Ionicons name="calculator-outline" size={14} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitle}>Payment Summary</ThemedText>
              </View>
            </View>

            <View style={styles.summaryBody}>
              <View style={styles.summaryRow}>
                <ThemedText style={styles.summaryLabel}>Invoice No</ThemedText>
                <TextInput
                  style={[styles.input, styles.readonlyInput, { width: 140, fontWeight: 'bold' }]}
                  value={selectedInvoice?.InvoiceNo || ''}
                  editable={false}
                  placeholder="None"
                />
              </View>

              <View style={styles.summaryRow}>
                <ThemedText style={styles.summaryLabel}>Net Amount</ThemedText>
                <TextInput
                  style={[styles.input, styles.readonlyInput, { width: 140, textAlign: 'right', fontWeight: 'bold' }]}
                  value={netAmount.toFixed(2)}
                  editable={false}
                />
              </View>

              <View style={styles.summaryRow}>
                <ThemedText style={styles.summaryLabel}>Paid</ThemedText>
                <TextInput
                  style={[styles.input, styles.readonlyInput, { width: 140, textAlign: 'right', fontWeight: 'bold', color: '#2E7D32' }]}
                  value={paidAmount.toFixed(2)}
                  editable={false}
                />
              </View>

              <View style={styles.summaryRow}>
                <ThemedText style={styles.summaryLabel}>Balance</ThemedText>
                <TextInput
                  style={[
                    styles.input,
                    styles.readonlyInput,
                    {
                      width: 140,
                      textAlign: 'right',
                      fontWeight: 'bold',
                      color: Math.abs(balanceAmount) < 0.01 ? '#2E7D32' : '#C62828',
                    },
                  ]}
                  value={balanceAmount.toFixed(2)}
                  editable={false}
                />
              </View>

              <View style={styles.summaryRow}>
                <ThemedText style={styles.summaryLabel}>GST / NDP Offer</ThemedText>
                <TextInput
                  style={[styles.input, styles.readonlyInput, { width: 140, textAlign: 'right', fontWeight: 'bold', color: '#66533A' }]}
                  value={gstAmount.toFixed(2)}
                  editable={false}
                />
              </View>

              {/* Status Banner */}
              <View
                style={[
                  styles.statusBanner,
                  Math.abs(balanceAmount) < 0.01 && netAmount > 0
                    ? styles.statusBannerSuccess
                    : styles.statusBannerPending,
                ]}
              >
                <Ionicons
                  name={Math.abs(balanceAmount) < 0.01 && netAmount > 0 ? 'checkmark-circle' : 'information-circle'}
                  size={16}
                  color={Math.abs(balanceAmount) < 0.01 && netAmount > 0 ? '#2E7D32' : '#9E6D08'}
                  style={{ marginRight: 6 }}
                />
                <ThemedText
                  style={[
                    styles.statusBannerText,
                    { color: Math.abs(balanceAmount) < 0.01 && netAmount > 0 ? '#2E7D32' : '#9E6D08' },
                  ]}
                >
                  {Math.abs(balanceAmount) < 0.01 && netAmount > 0
                    ? 'Amount Tallied. Ready to Save (F4).'
                    : selectedInvoice
                      ? 'Amount Not Tally. Please Key in Exact Amount.'
                      : 'Select an invoice to begin.'}
                </ThemedText>
              </View>

              {/* Action Buttons inside Summary card matching VB layout (cmdSave, cmdClear) */}
              <View style={{ marginTop: 12, gap: 8 }}>
                <TouchableOpacity
                  style={[
                    styles.summarySaveBtn,
                    (isLoading || !selectedInvoice || Math.abs(balanceAmount) >= 0.01) && styles.summarySaveBtnDisabled,
                  ]}
                  onPress={handleSave}
                  disabled={isLoading}
                  activeOpacity={0.8}
                >
                  {isLoading ? (
                    <ActivityIndicator size="small" color="#FFFFFF" style={{ marginRight: 6 }} />
                  ) : (
                    <Ionicons name="save" size={16} color="#FFFFFF" style={{ marginRight: 6 }} />
                  )}
                  <ThemedText style={styles.summarySaveBtnText}>Save (F4)</ThemedText>
                </TouchableOpacity>

                <TouchableOpacity
                  style={styles.summaryClearBtn}
                  onPress={handleClear}
                  activeOpacity={0.8}
                >
                  <Ionicons name="refresh-outline" size={15} color="#554428" style={{ marginRight: 4 }} />
                  <ThemedText style={styles.summaryClearBtnText}>Clear (F6)</ThemedText>
                </TouchableOpacity>
              </View>
            </View>
          </View>
        </View>
      </View>

      {/* PAYMODE LOV MODAL */}
      <Modal visible={isLovVisible} transparent animationType="fade" onRequestClose={() => setIsLovVisible(false)}>
        <TouchableOpacity style={styles.modalOverlay} activeOpacity={1} onPress={() => setIsLovVisible(false)}>
          <View style={styles.modalCard} onStartShouldSetResponder={() => true}>
            <View style={styles.cardHeaderBanner}>
              <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                <Ionicons name="list-outline" size={15} color="#B48218" style={{ marginRight: 6 }} />
                <ThemedText style={styles.cardHeaderTitle}>Select Payment Mode</ThemedText>
              </View>
              <TouchableOpacity onPress={() => setIsLovVisible(false)} hitSlop={{ top: 8, bottom: 8, left: 8, right: 8 }}>
                <Ionicons name="close" size={20} color="#3B2D17" />
              </TouchableOpacity>
            </View>

            <View style={{ padding: 10 }}>
              <TextInput
                style={styles.input}
                placeholder="Search paymode..."
                placeholderTextColor="#9E8D77"
                value={lovSearch}
                onChangeText={setLovSearch}
              />
            </View>

            {isLovLoading ? (
              <ActivityIndicator size="small" color="#B48218" style={{ padding: 20 }} />
            ) : (
              <FlatList
                data={lovList.filter((m) =>
                  lovSearch
                    ? (m.Paymode || m.Description || '').toLowerCase().includes(lovSearch.toLowerCase())
                    : true
                )}
                keyExtractor={(item, index) => item.Paymode || index.toString()}
                renderItem={({ item }) => (
                  <TouchableOpacity
                    style={styles.lovItem}
                    onPress={() => handleSelectFromLov(item.Paymode)}
                  >
                    <ThemedText style={styles.lovItemText}>{item.Paymode}</ThemedText>
                    {item.Description ? (
                      <ThemedText style={styles.lovItemDesc}>{item.Description}</ThemedText>
                    ) : null}
                  </TouchableOpacity>
                )}
                style={{ maxHeight: 280 }}
              />
            )}
          </View>
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
    paddingHorizontal: 14,
    paddingVertical: 6,
    shadowColor: '#B48218',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.2,
    shadowRadius: 2,
    elevation: 2,
  },
  headerBtnPrimaryText: {
    fontSize: 12,
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
  headerIconBadge: {
    width: 22,
    height: 22,
    borderRadius: 5,
    backgroundColor: '#FFF8EA',
    borderWidth: 1,
    borderColor: '#E2D2AF',
    alignItems: 'center',
    justifyContent: 'center',
    marginRight: 6,
  },
  cardHeaderTitle: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#3B2D17',
    letterSpacing: 0.3,
  },
  singleRowCard: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingHorizontal: 12,
    paddingVertical: 7,
    backgroundColor: '#FAF5EA',
  },
  cardBody: {
    padding: 8,
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

  // ===== SEARCH ROW & CONTROLS =====
  searchRow: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  searchLabel: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#4A3B2C',
    marginRight: 10,
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
  editableGridInput: {
    height: 26,
    backgroundColor: '#FFFDF9',
    borderColor: '#E5D5BC',
    fontSize: 11,
    fontWeight: '600',
  },
  readonlyInput: {
    backgroundColor: '#F8F5EE',
    borderColor: '#EADBCA',
    color: '#554736',
  },
  searchBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FAF5EA',
    borderWidth: 1,
    borderColor: '#DFD3BC',
    borderRadius: 6,
    paddingHorizontal: 10,
    paddingVertical: 4,
    marginLeft: 6,
    height: 28,
  },
  searchBtnText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#7D5E19',
  },

  // ===== PILL BOX & RADIOS =====
  pillBox: {
    flexDirection: 'row',
    alignItems: 'center',
    borderWidth: 1,
    borderColor: '#DFD3BC',
    borderRadius: 14,
    paddingHorizontal: 6,
    paddingVertical: 2,
    backgroundColor: '#FBF8F2',
  },
  radioContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    marginHorizontal: 6,
  },
  radioCircle: {
    width: 14,
    height: 14,
    borderRadius: 7,
    borderWidth: 1.5,
    borderColor: '#B8903B',
    alignItems: 'center',
    justifyContent: 'center',
    marginRight: 4,
    backgroundColor: '#FFFFFF',
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
    color: '#362B1A',
    fontWeight: 'bold',
  },

  // ===== TABLES =====
  tableHeader: {
    flexDirection: 'row',
    backgroundColor: '#F5E7CA',
    paddingVertical: 6,
    paddingHorizontal: 8,
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
  },
  tableHeaderText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3B2D17',
    textTransform: 'uppercase',
    letterSpacing: 0.3,
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
  tableCellText: {
    fontSize: 11,
    color: '#3B2D17',
  },
  emptyTableState: {
    padding: 30,
    alignItems: 'center',
    justifyContent: 'center',
  },
  emptyTableStateSmall: {
    padding: 20,
    alignItems: 'center',
    justifyContent: 'center',
  },
  emptyTableTitle: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#3B2D17',
    marginTop: 4,
  },
  emptyTableSubtitle: {
    fontSize: 11,
    color: '#8F816E',
    marginTop: 2,
    textAlign: 'center',
  },

  // ===== BOTTOM SECTION =====
  bottomSection: {
    flex: 1,
    flexDirection: 'row',
    gap: 8,
    marginTop: 8,
  },
  paymodeContainer: {
    flex: 1.6,
  },
  lovTriggerBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#D4C3A3',
    borderRadius: 5,
    paddingHorizontal: 8,
    paddingVertical: 3,
  },
  lovTriggerBtnText: {
    fontSize: 10,
    fontWeight: 'bold',
    color: '#7D5E19',
  },
  paymodeEntryBar: {
    flexDirection: 'row',
    alignItems: 'center',
    padding: 8,
    backgroundColor: '#FAF5EA',
    borderTopWidth: 1,
    borderTopColor: '#EADBCA',
  },
  entryLabel: {
    fontSize: 10,
    fontWeight: 'bold',
    color: '#5C4A28',
    marginBottom: 2,
  },
  entryPickBtn: {
    alignItems: 'center',
    justifyContent: 'center',
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderRadius: 6,
    width: 28,
    height: 28,
  },
  addPaymodeBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#F5E7CA',
    borderWidth: 1,
    borderColor: '#D4C3A3',
    borderRadius: 6,
    paddingHorizontal: 12,
    height: 28,
    justifyContent: 'center',
  },
  addPaymodeBtnText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#7D5E19',
  },

  // ===== SUMMARY CONTAINER =====
  summaryContainer: {
    flex: 1,
  },
  summaryBody: {
    padding: 10,
    gap: 6,
  },
  summaryRow: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
  },
  summaryLabel: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#4A3B2C',
  },
  statusBanner: {
    flexDirection: 'row',
    alignItems: 'center',
    borderWidth: 1,
    borderRadius: 6,
    paddingHorizontal: 8,
    paddingVertical: 6,
    marginTop: 6,
  },
  statusBannerSuccess: {
    backgroundColor: '#E8F5E9',
    borderColor: '#C8E6C9',
  },
  statusBannerPending: {
    backgroundColor: '#FFF8E1',
    borderColor: '#FFE082',
  },
  statusBannerText: {
    fontSize: 11,
    fontWeight: 'bold',
    flex: 1,
  },
  summarySaveBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    backgroundColor: '#B48218',
    borderRadius: 6,
    paddingVertical: 10,
    shadowColor: '#B48218',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.25,
    shadowRadius: 3,
    elevation: 3,
  },
  summarySaveBtnDisabled: {
    backgroundColor: '#CBBFA8',
    shadowOpacity: 0,
    elevation: 0,
  },
  summarySaveBtnText: {
    fontSize: 13,
    fontWeight: 'bold',
    color: '#FFFFFF',
    letterSpacing: 0.4,
  },
  summaryClearBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#DFD3BC',
    borderRadius: 6,
    paddingVertical: 8,
  },
  summaryClearBtnText: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#554428',
  },

  // ===== LOV MODAL =====
  modalOverlay: {
    flex: 1,
    backgroundColor: 'rgba(0, 0, 0, 0.45)',
    justifyContent: 'center',
    alignItems: 'center',
    padding: 16,
  },
  modalCard: {
    width: '100%',
    maxWidth: 380,
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#D4C3A3',
    overflow: 'hidden',
  },
  lovItem: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    paddingVertical: 9,
    paddingHorizontal: 12,
    borderBottomWidth: 1,
    borderBottomColor: '#F3EBDD',
  },
  lovItemText: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#3B2D17',
  },
  lovItemDesc: {
    fontSize: 11,
    color: '#7A6B56',
  },

  // ===== LOADING & TOAST =====
  loadingOverlay: {
    ...StyleSheet.absoluteFillObject,
    backgroundColor: 'rgba(255, 255, 255, 0.6)',
    justifyContent: 'center',
    alignItems: 'center',
    zIndex: 1000,
  },
  toastContainer: {
    position: 'absolute',
    top: 50,
    alignSelf: 'center',
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 16,
    paddingVertical: 10,
    borderRadius: 8,
    zIndex: 9999,
    shadowColor: '#000',
    shadowOpacity: 0.15,
    shadowRadius: 8,
    elevation: 4,
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
