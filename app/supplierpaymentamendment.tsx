import { ThemedText } from '@/components/themed-text';
import { API_BASE_URL } from '@/constants/api';
import { Ionicons } from '@expo/vector-icons';
import { Stack, useRouter } from 'expo-router';
import React, { useState } from 'react';
import {
  ActivityIndicator,
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

const API_URL = `${API_BASE_URL}`;

export default function SupplierPaymentAmendmentScreen() {
  const router = useRouter();

  // ===== STATE =====
  const [supplierCode, setSupplierCode] = useState('');
  const [supplierName, setSupplierName] = useState('');
  const [invoiceNo, setInvoiceNo] = useState('');
  const [customerCopy, setCustomerCopy] = useState(false);

  const [payments, setPayments] = useState<any[]>([]);
  const [selectedIndex, setSelectedIndex] = useState<number | null>(null);

  // Selected Detail State
  const [detailPaymentNo, setDetailPaymentNo] = useState('');
  const [detailPaymentDate, setDetailPaymentDate] = useState('');
  const [detailPaymode, setDetailPaymode] = useState('');
  const [detailAmount, setDetailAmount] = useState('');
  const [detailRemarks, setDetailRemarks] = useState('');

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
        `${API_URL}/supplierpaymentamendment/fetch?SupplierCode=${supplierCode}&InvoiceNo=${invoiceNo}`
      );
      const json = await res.json();
      if (json.success) {
        setPayments(json.data);
        if (json.data.length === 0) {
          showToast('No payments found for this criteria.', 'error');
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

  const handleSelectPayment = (item: any, idx: number) => {
    setSelectedIndex(idx);
    setDetailPaymentNo(item.PaymentNo || '');
    setDetailPaymentDate(item.PaymentDate ? item.PaymentDate.split('T')[0] : '');
    setDetailPaymode(item.Paymode || '');
    setDetailAmount(item.PaymentAmount ? item.PaymentAmount.toString() : '');
    setDetailRemarks(item.Remarks || '');
  };

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
    showToast('Form Cleared', 'success');
  };

  const handleUpdate = async () => {
    if (!detailPaymentNo.trim()) {
      showToast('Please select a payment record to update', 'error');
      return;
    }
    if (!detailRemarks.trim()) {
      showToast('Please Enter the Details (Remarks)', 'error');
      return;
    }

    setSaving(true);
    try {
      const res = await fetch(`${API_URL}/supplierpaymentamendment/update`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          PaymentNo: detailPaymentNo,
          Remarks: detailRemarks
        })
      });
      const json = await res.json();
      if (json.success) {
        showToast('Payment Details Updated', 'success');
        handleFetch();
      } else {
        showToast(json.message || 'Failed to update record', 'error');
      }
    } catch (e) {
      showToast('Server connection failed', 'error');
    } finally {
      setSaving(false);
    }
  };

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
          <ThemedText style={styles.headerTitle}>Supplier Payment Amendment</ThemedText>
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
              <ThemedText style={styles.headerBtnPrimaryText}>Update</ThemedText>
            )}
          </TouchableOpacity>
        </View>
      </View>

      <ScrollView style={styles.container} contentContainerStyle={styles.contentContainer}>
        {/* TOP SECTION: Amendment Search Filter Card */}
        <View style={styles.goldCard}>
          <View style={styles.cardHeaderBanner}>
            <View style={{ flexDirection: 'row', alignItems: 'center' }}>
              <View style={styles.headerIconBadge}>
                <Ionicons name="create-outline" size={15} color="#B48218" />
              </View>
              <ThemedText style={styles.cardHeaderTitle}>Search Criteria</ThemedText>
            </View>

            <TouchableOpacity style={styles.fetchBtn} onPress={handleFetch} disabled={loading}>
              <Ionicons name="search" size={13} color="#9E6D08" style={{ marginRight: 4 }} />
              <ThemedText style={styles.fetchBtnText}>{loading ? 'Fetching...' : 'Fetch Payments'}</ThemedText>
            </TouchableOpacity>
          </View>

          <View style={styles.cardBody}>
            {/* Single Row: Supplier Code, Supplier Name, Invoice No, Customer Copy */}
            <View style={styles.row}>
              <ThemedText style={styles.label}>Supplier Code</ThemedText>
              <View style={{ width: 140, flexDirection: 'row' }}>
                <TextInput
                  style={[styles.input, { width: 108, borderTopRightRadius: 0, borderBottomRightRadius: 0, borderRightWidth: 0 }]}
                  value={supplierCode}
                  onChangeText={setSupplierCode}
                  placeholder="Code"
                />
                <TouchableOpacity style={styles.lookupBtn} onPress={fetchSuppliers}>
                  <ThemedText style={styles.lookupBtnText}>...</ThemedText>
                </TouchableOpacity>
              </View>

              <TextInput
                style={[styles.input, styles.readonlyInput, { flex: 1, minWidth: 160, marginLeft: 8 }]}
                value={supplierName}
                editable={false}
                placeholder="Supplier Name"
              />

              <ThemedText style={[styles.label, { width: 'auto', marginLeft: 16, marginRight: 6 }]}>Invoice No</ThemedText>
              <TextInput
                style={[styles.input, { width: 140 }]}
                value={invoiceNo}
                onChangeText={setInvoiceNo}
                placeholder="Invoice No"
              />

              <View style={{ marginLeft: 16 }}>
                <Checkbox label="Customer Copy" selected={customerCopy} onPress={() => setCustomerCopy(!customerCopy)} />
              </View>
            </View>
          </View>
        </View>

        {/* MIDDLE SECTION: Payments List Grid (Full Width Table) */}
        <View style={styles.goldCard}>

          <View style={{ width: '100%' }}>
            {/* Table Header with proportional flex widths */}
            <View style={styles.tableHeader}>
              <ThemedText style={[styles.tableHeaderText, { flex: 1.3 }]}>Payment No</ThemedText>
              <ThemedText style={[styles.tableHeaderText, { flex: 1.2, textAlign: 'center' }]}>Payment Date</ThemedText>
              <ThemedText style={[styles.tableHeaderText, { flex: 1.1 }]}>Paymode</ThemedText>
              <ThemedText style={[styles.tableHeaderText, { flex: 1.3, textAlign: 'right' }]}>Amount</ThemedText>
              <ThemedText style={[styles.tableHeaderText, { flex: 2.5 }]}>Remarks</ThemedText>
            </View>

            {/* Table Body */}
            <ScrollView nestedScrollEnabled={true} style={{ maxHeight: 250, minHeight: payments.length === 0 ? 100 : 0 }}>
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
                    Enter Supplier Code and Invoice No, then click 'Fetch Payments'
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
                        {item.PaymentDate ? item.PaymentDate.split('T')[0] : ''}
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

        {/* BOTTOM SECTION: Selected Payment Amendment Entry Card */}
        <View style={styles.goldCard}>
          <View style={styles.cardHeaderBanner}>
            <View style={{ flexDirection: 'row', alignItems: 'center' }}>
              <View style={styles.headerIconBadge}>
                <Ionicons name="clipboard-outline" size={15} color="#B48218" />
              </View>
              <ThemedText style={styles.cardHeaderTitle}>Amend Payment Details</ThemedText>
            </View>
          </View>

          <View style={styles.cardBody}>
            {/* Read-only reference fields of selected payment */}
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

            {/* Editable Remarks input on single row */}
            <View style={{ marginTop: 10, paddingTop: 8, borderTopWidth: 1, borderTopColor: '#F2ECE0', flexDirection: 'row', alignItems: 'center' }}>
              <ThemedText style={[styles.label, { width: 'auto', marginRight: 10, marginBottom: 0, color: '#9E6D08' }]}>
                Amendment Remarks
              </ThemedText>
              <TextInput
                style={[styles.input, { flex: 1, height: 32, borderColor: '#B8903B' }]}
                value={detailRemarks}
                onChangeText={setDetailRemarks}
                placeholder="Enter new payment remarks..."
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
          <ThemedText style={{ color: '#FFFFFF', fontWeight: 'bold', marginTop: 10 }}>Updating Payment...</ThemedText>
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
    width: 95,
    marginRight: 6,
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
  radioContainer: {
    flexDirection: 'row',
    alignItems: 'center',
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
  radioLabel: {
    fontSize: 11,
    color: '#362B1A',
    fontWeight: 'bold',
  },
  // Table
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
    color: '#3B2D17',
    paddingHorizontal: 4,
  },
  tableRow: {
    flexDirection: 'row',
    paddingVertical: 6,
    paddingHorizontal: 8,
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
