/**
 * Developer: durairaj
 */

import { ThemedText } from '@/components/themed-text';
import { API_BASE_URL } from '@/constants/api';
import { Ionicons } from '@expo/vector-icons';
import { Stack, useRouter } from 'expo-router';
import React, { useState } from 'react';
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

export default function SupplierPaymentAmendmentScreen() {
  const router = useRouter();

  // ===== SEARCH CRITERIA STATE =====
  const [supplierCode, setSupplierCode] = useState('');
  const [supplierName, setSupplierName] = useState('');
  const [invoiceNo, setInvoiceNo] = useState('');
  const [customerCopy, setCustomerCopy] = useState(false);

  // ===== GRID STATE =====
  const [payments, setPayments] = useState<any[]>([]);
  const [selectedIndex, setSelectedIndex] = useState<number | null>(null);

  // ===== SELECTED PAYMENT AMENDMENT ENTRY STATE =====
  const [detailPaymentNo, setDetailPaymentNo] = useState('');
  const [detailPaymentDate, setDetailPaymentDate] = useState('');
  const [detailPaymode, setDetailPaymode] = useState('');
  const [detailAmount, setDetailAmount] = useState('');
  const [detailRemarks, setDetailRemarks] = useState('');

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

  // Supplier Search LOV matching btnSupplierCodeSearch_Click in frmSupplierPaymentAmendment.vb
  const fetchSuppliers = async () => {
    setLoading(true);
    try {
      const res = await fetch(`${API_URL}/supplierpaymentamendment/lov/supplier`);
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

  // Supplier Blur Validation
  const handleSupplierBlur = async () => {
    if (!supplierCode.trim()) {
      setSupplierName('');
      return;
    }
    try {
      const res = await fetch(`${API_URL}/supplierpaymentamendment/supplier/${encodeURIComponent(supplierCode.trim())}`);
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

  // Invoice Lookup LOV
  const fetchInvoices = async () => {
    setLoading(true);
    try {
      const queryParam = supplierCode.trim() ? `?SupplierCode=${encodeURIComponent(supplierCode.trim())}` : '';
      const res = await fetch(`${API_URL}/supplierpaymentamendment/lov/invoices${queryParam}`);
      const json = await res.json();
      if (json.success && json.data) {
        if (json.data.length === 0) {
          showToast('No invoices found for this supplier', 'info');
          return;
        }
        openLov(
          'Select Invoice',
          json.data,
          [
            { key: 'InvoiceNo', label: 'Invoice No', flex: 1.5 },
            { key: 'InvoiceDate', label: 'Date', flex: 1.2 },
            { key: 'JobSheetNo', label: 'Job Sheet', flex: 1.5 }
          ],
          item => {
            setInvoiceNo(item.InvoiceNo);
          }
        );
      } else {
        showToast(json.message || 'Failed to fetch invoices', 'error');
      }
    } catch (e) {
      showToast('Error connecting to server', 'error');
    } finally {
      setLoading(false);
    }
  };

  // Fetch Payments matching cmdFetch_Click in frmSupplierPaymentAmendment.vb
  const handleFetch = async () => {
    if (!supplierCode.trim()) {
      showToast('Supplier Code is required', 'error');
      return;
    }
    if (!invoiceNo.trim()) {
      showToast('Invoice No is required', 'error');
      return;
    }

    setLoading(true);
    try {
      const res = await fetch(
        `${API_URL}/supplierpaymentamendment/fetch?SupplierCode=${encodeURIComponent(supplierCode.trim())}&InvoiceNo=${encodeURIComponent(invoiceNo.trim())}`
      );
      const json = await res.json();
      if (json.success) {
        setPayments(json.data || []);
        if (!json.data || json.data.length === 0) {
          showToast('No payment records found', 'info');
        } else {
          showToast(`Loaded ${json.data.length} payment records`, 'success');
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

  // Select Payment Row matching dgvSupplier_Click in frmSupplierPaymentAmendment.vb
  const handleSelectPayment = (item: any, idx: number) => {
    setSelectedIndex(idx);
    setDetailPaymentNo(item.PaymentNo || '');
    setDetailPaymentDate(item.PaymentDate || '');
    setDetailPaymode(item.Paymode || '');
    setDetailAmount(item.PaymentAmount ? Number(item.PaymentAmount).toFixed(2) : '0.00');
    setDetailRemarks(item.Remarks || '');
  };

  // Clear Form matching SubClearall() in frmSupplierPaymentAmendment.vb
  const handleClearAll = () => {
    setSupplierCode('');
    setSupplierName('');
    setInvoiceNo('');
    setCustomerCopy(false);
    setPayments([]);
    setSelectedIndex(null);
    setDetailPaymentNo('');
    setDetailPaymentDate('');
    setDetailPaymode('');
    setDetailAmount('');
    setDetailRemarks('');
    showToast('Cleared All', 'info');
  };

  // Update Payment Details matching cmdUpdate_Click in frmSupplierPaymentAmendment.vb
  const handleUpdate = async () => {
    if (!detailPaymentNo.trim()) {
      showToast('Please select a payment record to update', 'error');
      return;
    }
    // Validation matching VB: If Trim(txtRemarks.Text) = "" Then MsgBox("Please Enter the Details", MsgBoxStyle.Information)
    if (!detailRemarks.trim()) {
      showToast('Please Enter the Details', 'error');
      return;
    }

    setSaving(true);
    try {
      const res = await fetch(`${API_URL}/supplierpaymentamendment/update`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          PaymentNo: detailPaymentNo.trim(),
          Remarks: detailRemarks.trim()
        })
      });

      const json = await res.json();
      if (json.success) {
        // Matches MsgBox("Payment Details Update", MsgBoxStyle.Information) in VB
        showToast('Payment Details Update', 'success');

        // Reload payments matching cmdFetch_Click in VB
        await handleFetch();

        // Clear all fields matching SubClearall() in VB
        handleClearAll();
      } else {
        const errDetail = json.error ? `${json.message || 'Error'}: ${json.error}` : (json.message || 'Update failed');
        showToast(errDetail, 'error');
      }
    } catch (e) {
      showToast('Server connection failed', 'error');
    } finally {
      setSaving(false);
    }
  };

  // RePrint matching subPrint(txtHeadInvoiceNo.Text.Trim) in frmSupplierPaymentAmendment.vb
  const handleReprint = async () => {
    if (!invoiceNo.trim()) {
      showToast('Please enter an Invoice No to reprint', 'error');
      return;
    }

    setVoucherLoading(true);
    setVoucherModalVisible(true);
    try {
      const supplierParam = supplierCode.trim() ? `?SupplierCode=${encodeURIComponent(supplierCode.trim())}` : '';
      const res = await fetch(
        `${API_URL}/supplierpaymentamendment/voucher/${encodeURIComponent(invoiceNo.trim())}${supplierParam}`
      );
      const json = await res.json();
      if (json.success && json.data) {
        setVoucherData(json.data);
      } else {
        setVoucherData([]);
        showToast('No voucher history found for this invoice', 'info');
      }
    } catch (e) {
      showToast('Error loading voucher data', 'error');
    } finally {
      setVoucherLoading(false);
    }
  };

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
          <ThemedText style={styles.headerTitle}>Supplier Payment Amendment</ThemedText>
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
        {/* TOP SECTION: Amendment Search Filter Card (GroupBox1) */}
        <View style={styles.goldCard}>
          <View style={styles.cardHeaderBanner}>
            <View style={{ flexDirection: 'row', alignItems: 'center' }}>
              <View style={styles.headerIconBadge}>
                <Ionicons name="search" size={14} color="#B48218" />
              </View>
              <ThemedText style={styles.cardHeaderTitle}>Search Criteria</ThemedText>
            </View>

            <TouchableOpacity style={styles.fetchBtn} onPress={handleFetch} disabled={loading}>
              <Ionicons name="search" size={13} color="#9E6D08" style={{ marginRight: 4 }} />
              <ThemedText style={styles.fetchBtnText}>{loading ? 'Fetching...' : 'Fetch'}</ThemedText>
            </TouchableOpacity>
          </View>

          <View style={styles.cardBody}>
            {/* Single Row: Supplier Code, Search, Supplier Name, Invoice No, Search, Customer Copy */}
            <View style={[styles.row, { flexWrap: 'nowrap', alignItems: 'center' }]}>
              {/* Supplier Code */}
              <ThemedText style={[styles.label, { width: 'auto', marginRight: 8 }]}>Supplier Code</ThemedText>
              <View style={{ flexDirection: 'row', alignItems: 'center', marginRight: 8 }}>
                <TextInput
                  style={[
                    styles.input,
                    {
                      width: 80,
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
                style={[styles.input, styles.readonlyInput, { flex: 1.2, minWidth: 140, marginRight: 16 }]}
                value={supplierName}
                editable={false}
                placeholder="Supplier Name"
              />

              {/* Invoice No */}
              <ThemedText style={[styles.label, { width: 'auto', marginRight: 8 }]}>Invoice No</ThemedText>
              <View style={{ flexDirection: 'row', alignItems: 'center', marginRight: 16 }}>
                <TextInput
                  style={[
                    styles.input,
                    {
                      width: 110,
                      minWidth: 0,
                      borderTopRightRadius: 0,
                      borderBottomRightRadius: 0,
                      borderRightWidth: 0,
                    },
                  ]}
                  value={invoiceNo}
                  onChangeText={setInvoiceNo}
                  placeholder="Invoice No"
                />
                <TouchableOpacity style={styles.lookupBtn} onPress={fetchInvoices}>
                  <ThemedText style={styles.lookupBtnText}>...</ThemedText>
                </TouchableOpacity>
              </View>

              {/* Customer Copy Checkbox */}
              <View style={{ justifyContent: 'center', marginLeft: 8 }}>
                <Checkbox label="Customer Copy" selected={customerCopy} onPress={() => setCustomerCopy(!customerCopy)} />
              </View>
            </View>
          </View>
        </View>

        {/* MIDDLE SECTION: Payments List Grid (dgvSupplier matching frmSupplierPaymentAmendment.vb) */}
        <View style={styles.goldCard}>
          <View style={styles.cardHeaderBanner}>
            <View style={{ flexDirection: 'row', alignItems: 'center' }}>
              <View style={styles.headerIconBadge}>
                <Ionicons name="list" size={14} color="#B48218" />
              </View>
              <ThemedText style={styles.cardHeaderTitle}>Payment Records</ThemedText>
              <View style={styles.counterBadge}>
                <ThemedText style={styles.counterBadgeText}>{payments.length} Records</ThemedText>
              </View>
            </View>
          </View>

          <View style={{ width: '100%' }}>
            {/* Table Header with proportional flex widths */}
            <View style={styles.tableHeader}>
              <ThemedText style={[styles.tableHeaderText, { flex: 1.3 }]}>Payment No</ThemedText>
              <ThemedText style={[styles.tableHeaderText, { flex: 1.2, textAlign: 'center' }]}>Payment Date</ThemedText>
              <ThemedText style={[styles.tableHeaderText, { flex: 1.1 }]}>Paymode</ThemedText>
              <ThemedText style={[styles.tableHeaderText, { flex: 1.3, textAlign: 'right' }]}>Payment Amount</ThemedText>
              <ThemedText style={[styles.tableHeaderText, { flex: 2.5 }]}>Remarks</ThemedText>
            </View>

            {/* Table Body */}
            <ScrollView nestedScrollEnabled={true} style={{ maxHeight: 250, minHeight: payments.length === 0 ? 110 : 0 }}>
              {loading ? (
                <View style={styles.emptyTableState}>
                  <ActivityIndicator size="large" color="#B48218" />
                  <ThemedText style={{ color: '#7D6F5B', fontSize: 12, marginTop: 8 }}>Loading payments...</ThemedText>
                </View>
              ) : payments.length === 0 ? (
                <View style={styles.emptyTableState}>
                  <Ionicons name="document-text-outline" size={32} color="#D0C2A8" />
                  <ThemedText style={{ color: '#3B2D17', fontSize: 13, fontWeight: 'bold', marginTop: 4 }}>
                    No payment records found
                  </ThemedText>
                  <ThemedText style={{ color: '#7D6F5B', fontSize: 11, marginTop: 2 }}>
                    Enter Supplier Code and Invoice No, then click 'Fetch'
                  </ThemedText>
                </View>
              ) : (
                payments.map((item, idx) => {
                  const isSelected = selectedIndex === idx;
                  return (
                    <TouchableOpacity
                      key={idx}
                      activeOpacity={0.7}
                      style={[
                        styles.tableRow,
                        idx % 2 === 1 && { backgroundColor: '#FCFAF6' },
                        isSelected && styles.tableRowSelected,
                      ]}
                      onPress={() => handleSelectPayment(item, idx)}
                    >
                      <ThemedText style={[styles.tableCellText, { flex: 1.3, fontWeight: 'bold' }]}>
                        {item.PaymentNo}
                      </ThemedText>
                      <ThemedText style={[styles.tableCellText, { flex: 1.2, textAlign: 'center' }]}>
                        {item.PaymentDate || ''}
                      </ThemedText>
                      <ThemedText style={[styles.tableCellText, { flex: 1.1 }]}>
                        {item.Paymode}
                      </ThemedText>
                      <ThemedText style={[styles.tableCellText, { flex: 1.3, textAlign: 'right', fontWeight: 'bold', color: '#9E6D08' }]}>
                        {item.PaymentAmount ? Number(item.PaymentAmount).toFixed(2) : '0.00'}
                      </ThemedText>
                      <ThemedText style={[styles.tableCellText, { flex: 2.5 }]} numberOfLines={1}>
                        {item.Remarks || ''}
                      </ThemedText>
                    </TouchableOpacity>
                  );
                })
              )}
            </ScrollView>
          </View>
        </View>

        {/* BOTTOM SECTION: Selected Payment Amendment Entry Card (GroupBox4) */}
        <View style={styles.goldCard}>
          <View style={styles.cardHeaderBanner}>
            <View style={{ flexDirection: 'row', alignItems: 'center' }}>
              <View style={styles.headerIconBadge}>
                <Ionicons name="create-outline" size={14} color="#B48218" />
              </View>
              <ThemedText style={styles.cardHeaderTitle}>Amend Payment Details</ThemedText>
            </View>
          </View>

          <View style={styles.cardBody}>
            {/* Read-only reference fields: Payment No, Payment Date, Paymode, Amount */}
            <View style={styles.row}>
              <View style={{ flex: 1.2, minWidth: 110, marginRight: 8 }}>
                <ThemedText style={styles.inputSubLabel}>Payment No</ThemedText>
                <TextInput style={[styles.input, styles.readonlyInput]} value={detailPaymentNo} editable={false} placeholder="Select above" />
              </View>

              <View style={{ flex: 1.2, minWidth: 110, marginRight: 8 }}>
                <ThemedText style={styles.inputSubLabel}>Payment Date</ThemedText>
                <TextInput style={[styles.input, styles.readonlyInput]} value={detailPaymentDate} editable={false} />
              </View>

              <View style={{ flex: 1.2, minWidth: 110, marginRight: 8 }}>
                <ThemedText style={styles.inputSubLabel}>Paymode</ThemedText>
                <TextInput style={[styles.input, styles.readonlyInput]} value={detailPaymode} editable={false} />
              </View>

              <View style={{ flex: 1.2, minWidth: 110 }}>
                <ThemedText style={styles.inputSubLabel}>Amount</ThemedText>
                <TextInput style={[styles.input, styles.readonlyInput, { textAlign: 'right', fontWeight: 'bold' }]} value={detailAmount} editable={false} />
              </View>
            </View>

            {/* Editable Remarks input matching txtRemarks in frmSupplierPaymentAmendment.vb */}
            <View style={{ marginTop: 10, paddingTop: 10, borderTopWidth: 1, borderTopColor: '#F2ECE0' }}>
              <ThemedText style={[styles.inputSubLabel, { color: '#9E6D08' }]}>
                Remarks (Max 50 Characters)
              </ThemedText>
              <TextInput
                style={[styles.input, { borderColor: '#B8903B', height: 32 }]}
                value={detailRemarks}
                onChangeText={setDetailRemarks}
                maxLength={50}
                placeholder="Enter remarks..."
              />
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
                      <ThemedText style={{ marginTop: 10, color: '#7D6F5B' }}>Loading vouchers...</ThemedText>
                    </View>
                  ) : voucherData.length === 0 ? (
                    <View style={{ padding: 30, alignItems: 'center' }}>
                      <Ionicons name="document-text-outline" size={32} color="#D0C2A8" />
                      <ThemedText style={{ color: '#3B2D17', fontSize: 13, fontWeight: 'bold', marginTop: 6 }}>
                        No Payment Voucher History
                      </ThemedText>
                      <ThemedText style={{ color: '#7D6F5B', fontSize: 11, marginTop: 2 }}>
                        No records found for Invoice {invoiceNo}
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
    marginRight: 10,
    width: 95,
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
  radioLabel: {
    fontSize: 11,
    color: '#3B2D17',
    fontWeight: '600',
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
