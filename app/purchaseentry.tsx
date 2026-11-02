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
  View,
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';

const API_URL = `${API_BASE_URL}/purchaseentry`;

export default function PurchaseEntryScreen() {
  const router = useRouter();

  const [saving, setSaving] = useState(false);
  const [loadingDetails, setLoadingDetails] = useState(false);
  const [loadingLookup, setLoadingLookup] = useState(false);

  // Form State matching frmPurchaseEntry.vb
  const [jobSheetNo, setJobSheetNo] = useState('');
  const [invoiceNo, setInvoiceNo] = useState('');

  // Date Fields (txtIDD, txtIMM, txtIYYYY)
  const [invoiceDateDD, setInvoiceDateDD] = useState('');
  const [invoiceDateMM, setInvoiceDateMM] = useState('');
  const [invoiceDateYYYY, setInvoiceDateYYYY] = useState('');

  const [supplier, setSupplier] = useState('');
  const [totalGSTSales, setTotalGSTSales] = useState('');
  const [gst, setGst] = useState('');
  const [totalSales, setTotalSales] = useState('');

  // Lookup Modal State
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [lookupData, setLookupData] = useState<any[]>([]);
  const [filteredLookupData, setFilteredLookupData] = useState<any[]>([]);
  const [searchQuery, setSearchQuery] = useState('');

  // Toast Notification State
  const [toast, setToast] = useState<{ message: string; type: 'success' | 'error' | 'info' } | null>(null);

  const showToast = (message: string, type: 'success' | 'error' | 'info' = 'success') => {
    setToast({ message, type });
    setTimeout(() => {
      setToast(null);
    }, 3200);
  };

  // Fetch LOV JobSheets matching btnJobSheetNoSearch_Click
  const fetchJobSheets = async () => {
    setLoadingLookup(true);
    try {
      const response = await fetch(`${API_URL}/jobsheets?t=${Date.now()}`);
      const json = await response.json();
      if (json.success && Array.isArray(json.data)) {
        setLookupData(json.data);
        setFilteredLookupData(json.data);
      } else {
        showToast(json.message || 'Failed to fetch job sheets', 'error');
      }
    } catch (error) {
      console.error('Fetch JobSheets Error:', error);
      showToast('Connection error', 'error');
    } finally {
      setLoadingLookup(false);
    }
  };

  const handleOpenLookup = () => {
    setSearchQuery('');
    setIsModalVisible(true);
    fetchJobSheets();
  };

  const handleSearch = (text: string) => {
    setSearchQuery(text);
    if (!text.trim()) {
      setFilteredLookupData(lookupData);
      return;
    }
    const q = text.toLowerCase();
    const filtered = lookupData.filter((item) =>
      (item.JobSheetNo && item.JobSheetNo.toString().toLowerCase().includes(q)) ||
      (item.PurchaseBillNo && item.PurchaseBillNo.toString().toLowerCase().includes(q)) ||
      (item.SupplierCode && item.SupplierCode.toString().toLowerCase().includes(q))
    );
    setFilteredLookupData(filtered);
  };

  const selectJobSheet = async (item: any) => {
    setIsModalVisible(false);
    const selectedNo = (item.JobSheetNo || '').trim();
    setJobSheetNo(selectedNo);
    if (item.PurchaseBillNo) setInvoiceNo(item.PurchaseBillNo.trim());
    if (item.SupplierCode) setSupplier(item.SupplierCode.trim());
    loadJobSheetDetails(selectedNo);
  };

  // Load JobSheet details matching subGetJobSheetDetail in frmPurchaseEntry.vb
  const loadJobSheetDetails = async (id: string) => {
    const trimmedId = id.trim();
    if (!trimmedId) return;
    setLoadingDetails(true);
    try {
      const response = await fetch(`${API_URL}/jobsheet/${encodeURIComponent(trimmedId)}?t=${Date.now()}`);
      const json = await response.json();
      if (json.success && json.data) {
        const data = json.data;
        if (data.PurchaseBillNo !== undefined) setInvoiceNo(data.PurchaseBillNo || '');
        if (data.SupplierCode !== undefined) setSupplier(data.SupplierCode || '');
        setTotalGSTSales(data.TotalBeforeGST !== undefined && data.TotalBeforeGST !== null ? Number(data.TotalBeforeGST).toFixed(2) : '');
        setGst(data.GST !== undefined && data.GST !== null ? Number(data.GST).toFixed(2) : '');
        setTotalSales(data.TotalSales !== undefined && data.TotalSales !== null ? Number(data.TotalSales).toFixed(2) : '');

        if (data.PurchaseBillDate) {
          const dateObj = new Date(data.PurchaseBillDate);
          if (!isNaN(dateObj.getTime())) {
            setInvoiceDateDD(String(dateObj.getDate()).padStart(2, '0'));
            setInvoiceDateMM(String(dateObj.getMonth() + 1).padStart(2, '0'));
            setInvoiceDateYYYY(String(dateObj.getFullYear()));
          } else {
            setInvoiceDateDD('');
            setInvoiceDateMM('');
            setInvoiceDateYYYY('');
          }
        } else {
          setInvoiceDateDD('');
          setInvoiceDateMM('');
          setInvoiceDateYYYY('');
        }
      } else {
        const msg = json.error ? `${json.message || 'Error'}: ${json.error}` : (json.message || 'Jobsheet No not found.');
        showToast(msg, 'error');
      }
    } catch (e: any) {
      console.error(e);
      showToast(e?.message || 'Jobsheet No not found.', 'error');
    } finally {
      setLoadingDetails(false);
    }
  };

  // Calculation matching txtTotalGSTSales_Leave in frmPurchaseEntry.vb:
  // If Val(txtTotalGSTSales.Text) <> 0 Then
  //   txtGST.Text = Format(Val(txtTotalGSTSales.Text) * 0.09, "0.00")
  //   txtTotalSales.Text = Format(Val(txtTotalGSTSales.Text) + Val(txtGST.Text), "0.00")
  // End If
  const handleTotalGSTSalesChange = (val: string) => {
    setTotalGSTSales(val);
  };

  const handleTotalGSTSalesBlur = () => {
    const valNum = parseFloat(totalGSTSales);
    if (!isNaN(valNum) && valNum !== 0) {
      const calculatedGst = Number((valNum * 0.09).toFixed(2));
      const calculatedTotal = Number((valNum + calculatedGst).toFixed(2));
      setGst(calculatedGst.toFixed(2));
      setTotalSales(calculatedTotal.toFixed(2));
    } else if (valNum === 0) {
      setGst('0.00');
      setTotalSales('0.00');
    }
  };

  // Validations matching fncValidateMe() in frmPurchaseEntry.vb:
  // If txtJobSheetNo.Text.Trim = "" Then MsgBox "JobsheeNo should not be blank."
  // If txtInvoiceNo.Text.Trim = "" Then MsgBox "Invoice No should not be blank."
  // ElseIf txtSupplier.Text.Trim = "" Then MsgBox "Supplier Code should not be blank."
  const handleSave = async () => {
    if (!jobSheetNo.trim()) {
      showToast('JobsheeNo should not be blank.', 'error');
      return;
    }
    if (!invoiceNo.trim()) {
      showToast('Invoice No should not be blank.', 'error');
      return;
    }
    if (!supplier.trim()) {
      showToast('Supplier Code should not be blank.', 'error');
      return;
    }

    const payload = {
      JobSheetNo: jobSheetNo.trim(),
      InvoiceNo: invoiceNo.trim(),
      Supplier: supplier.trim(),
      TotalGSTSales: parseFloat(totalGSTSales) || 0,
      GST: parseFloat(gst) || 0,
      TotalSales: parseFloat(totalSales) || 0,
    };

    setSaving(true);
    try {
      const response = await fetch(`${API_URL}/jobsheet/${encodeURIComponent(jobSheetNo.trim())}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload),
      });

      const json = await response.json();
      if (json.success) {
        showToast('Purchase Details updated successfully!', 'success');
        handleClear();
      } else {
        showToast(json.message || 'Error in Save.', 'error');
      }
    } catch (error) {
      console.error('Save error:', error);
      showToast('Error in Save.', 'error');
    } finally {
      setSaving(false);
    }
  };

  // Clear matching subClearMe() in frmPurchaseEntry.vb
  const handleClear = () => {
    setJobSheetNo('');
    setInvoiceNo('');
    setInvoiceDateDD('');
    setInvoiceDateMM('');
    setInvoiceDateYYYY('');
    setSupplier('');
    setTotalGSTSales('');
    setGst('');
    setTotalSales('');
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
          <ThemedText style={styles.headerTitle}>Purchase Entry</ThemedText>
        </View>

        <View style={styles.headerRight}>
          <TouchableOpacity
            style={[styles.actionBtn, styles.clearBtn]}
            onPress={handleClear}
            activeOpacity={0.8}
            disabled={saving}
          >
            <Ionicons name="refresh-outline" size={14} color="#5C4A28" />
            <ThemedText style={styles.clearBtnText}>Clear</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity
            style={[styles.actionBtn, styles.saveBtn]}
            onPress={handleSave}
            activeOpacity={0.8}
            disabled={saving}
          >
            {saving ? (
              <ActivityIndicator size="small" color="#9E6D08" />
            ) : (
              <>
                <Ionicons name="save-outline" size={14} color="#7D5800" />
                <ThemedText style={styles.saveBtnText}>Save</ThemedText>
              </>
            )}
          </TouchableOpacity>
        </View>
      </View>

      {/* TOAST NOTIFICATION */}
      {toast && (
        <View
          style={[
            styles.toast,
            {
              backgroundColor:
                toast.type === 'success'
                  ? '#2E7D32'
                  : toast.type === 'info'
                    ? '#0288D1'
                    : '#C62828',
            },
          ]}
        >
          <Ionicons
            name={
              toast.type === 'success'
                ? 'checkmark-circle'
                : toast.type === 'info'
                  ? 'information-circle'
                  : 'alert-circle'
            }
            size={16}
            color="#FFFFFF"
            style={{ marginRight: 6 }}
          />
          <ThemedText style={styles.toastText}>{toast.message}</ThemedText>
        </View>
      )}

      <ScrollView
        style={styles.scrollView}
        contentContainerStyle={styles.scrollContent}
        keyboardShouldPersistTaps="handled"
      >
        <View style={styles.mainWrapper}>
          {/* PRIMARY PURCHASE DETAILS CARD */}
          <View style={styles.goldCard}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadge}>
                  <Ionicons name="receipt-outline" size={15} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitle}>Purchase Invoice Details</ThemedText>
              </View>
            </View>

            <View style={styles.cardBody}>
              {/* JobSheet No with Lookup */}
              <View style={styles.formRow}>
                <ThemedText style={styles.fieldLabel}>JobSheet No</ThemedText>
                <View style={styles.fieldValueContainer}>
                  <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                    <TextInput
                      style={[
                        styles.input,
                        {
                          width: 140,
                          minWidth: 0,
                          borderTopRightRadius: 0,
                          borderBottomRightRadius: 0,
                          borderRightWidth: 0,
                        },
                      ]}
                      placeholder="JobSheet No"
                      placeholderTextColor="#9E8D77"
                      value={jobSheetNo}
                      onChangeText={setJobSheetNo}
                      onBlur={() => loadJobSheetDetails(jobSheetNo)}
                      autoCapitalize="characters"
                    />
                    <TouchableOpacity
                      style={styles.lookupBtn}
                      onPress={handleOpenLookup}
                      activeOpacity={0.7}
                    >
                      <ThemedText style={styles.lookupBtnText}>...</ThemedText>
                    </TouchableOpacity>
                  </View>
                </View>
              </View>

              {loadingDetails && (
                <View style={styles.inlineLoading}>
                  <ActivityIndicator size="small" color="#B8903B" />
                  <ThemedText style={styles.inlineLoadingText}>Loading JobSheet details...</ThemedText>
                </View>
              )}

              {/* Invoice No */}
              <View style={styles.formRow}>
                <ThemedText style={styles.fieldLabel}>Invoice No</ThemedText>
                <View style={styles.fieldValueContainer}>
                  <TextInput
                    style={[styles.input, { width: 172, minWidth: 0 }]}
                    placeholder="Invoice / Bill No"
                    placeholderTextColor="#9E8D77"
                    value={invoiceNo}
                    onChangeText={setInvoiceNo}
                  />
                </View>
              </View>

              {/* Invoice Date */}
              <View style={styles.formRow}>
                <ThemedText style={styles.fieldLabel}>Invoice Date</ThemedText>
                <View style={styles.fieldValueContainer}>
                  <View style={styles.dateRow}>
                    <TextInput
                      style={[styles.input, styles.datePartInput, { width: 44, minWidth: 0 }]}
                      placeholder="DD"
                      placeholderTextColor="#9E8D77"
                      value={invoiceDateDD}
                      onChangeText={setInvoiceDateDD}
                      keyboardType="numeric"
                      maxLength={2}
                    />
                    <ThemedText style={styles.dateSeparator}>/</ThemedText>
                    <TextInput
                      style={[styles.input, styles.datePartInput, { width: 44, minWidth: 0 }]}
                      placeholder="MM"
                      placeholderTextColor="#9E8D77"
                      value={invoiceDateMM}
                      onChangeText={setInvoiceDateMM}
                      keyboardType="numeric"
                      maxLength={2}
                    />
                    <ThemedText style={styles.dateSeparator}>/</ThemedText>
                    <TextInput
                      style={[styles.input, styles.datePartInput, { width: 68, minWidth: 0 }]}
                      placeholder="YYYY"
                      placeholderTextColor="#9E8D77"
                      value={invoiceDateYYYY}
                      onChangeText={setInvoiceDateYYYY}
                      keyboardType="numeric"
                      maxLength={4}
                    />
                  </View>
                </View>
              </View>

              {/* Supplier Code */}
              <View style={styles.formRow}>
                <ThemedText style={styles.fieldLabel}>Supplier</ThemedText>
                <View style={styles.fieldValueContainer}>
                  <TextInput
                    style={[styles.input, { width: 172, minWidth: 0 }]}
                    placeholder="Supplier Code"
                    placeholderTextColor="#9E8D77"
                    value={supplier}
                    onChangeText={setSupplier}
                    autoCapitalize="characters"
                  />
                </View>
              </View>
            </View>
          </View>

          {/* FINANCIALS & TAX COMPUTATION CARD */}
          <View style={styles.goldCard}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadge}>
                  <Ionicons name="calculator-outline" size={15} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitle}>Sales & Tax Calculation</ThemedText>
              </View>
            </View>

            <View style={styles.cardBody}>
              {/* Total GST Sales (Subtotal before GST) */}
              <View style={styles.formRow}>
                <ThemedText style={styles.fieldLabel}>Total GST Sales</ThemedText>
                <View style={styles.fieldValueContainer}>
                  <TextInput
                    style={[styles.input, { width: 172, minWidth: 0, textAlign: 'right', fontWeight: '600' }]}
                    placeholder="0.00"
                    placeholderTextColor="#9E8D77"
                    value={totalGSTSales}
                    onChangeText={handleTotalGSTSalesChange}
                    onBlur={handleTotalGSTSalesBlur}
                    keyboardType="numeric"
                  />
                </View>
              </View>

              {/* GST (9% Auto calculated) */}
              <View style={styles.formRow}>
                <View style={styles.labelWithSubContainer}>
                  <ThemedText style={styles.fieldLabel}>GST (9%)</ThemedText>
                </View>
                <View style={styles.fieldValueContainer}>
                  <TextInput
                    style={[styles.input, styles.readOnlyInput, { width: 172, minWidth: 0, textAlign: 'right', fontWeight: 'bold', color: '#9E6D08' }]}
                    value={gst || '0.00'}
                    editable={false}
                  />
                </View>
              </View>

              {/* Total Sales (Total + GST) */}
              <View style={[styles.formRow, { borderBottomWidth: 0, paddingBottom: 0 }]}>
                <View style={styles.labelWithSubContainer}>
                  <ThemedText style={[styles.fieldLabel, { fontSize: 12, color: '#3B2D17' }]}>Total Sales</ThemedText>
                </View>
                <View style={styles.fieldValueContainer}>
                  <TextInput
                    style={[
                      styles.input,
                      styles.readOnlyInput,
                      styles.totalHighlightInput,
                      { width: 172, minWidth: 0 }
                    ]}
                    value={totalSales || '0.00'}
                    editable={false}
                  />
                </View>
              </View>
            </View>
          </View>
        </View>
      </ScrollView>

      {/* JOBSHEET LOOKUP MODAL */}
      <Modal
        visible={isModalVisible}
        animationType="fade"
        transparent={true}
        onRequestClose={() => setIsModalVisible(false)}
      >
        <TouchableWithoutFeedback onPress={() => setIsModalVisible(false)}>
          <View style={styles.modalOverlay}>
            <TouchableWithoutFeedback>
              <View style={styles.modalContainer}>
                {/* Modal Header */}
                <View style={styles.modalHeader}>
                  <View style={styles.modalTitleContainer}>
                    <View style={styles.modalIconBadge}>
                      <Ionicons name="search" size={15} color="#B48218" />
                    </View>
                    <ThemedText style={styles.modalTitle}>Select JobSheet</ThemedText>
                  </View>
                  <TouchableOpacity
                    onPress={() => setIsModalVisible(false)}
                    style={styles.modalCloseBtn}
                  >
                    <Ionicons name="close" size={18} color="#5C4A28" />
                  </TouchableOpacity>
                </View>

                {/* Modal Search Input */}
                <View style={styles.modalSearchSection}>
                  <View style={styles.modalSearchBox}>
                    <Ionicons name="search-outline" size={15} color="#7D6F5B" style={{ marginRight: 8 }} />
                    <TextInput
                      style={styles.modalSearchInput}
                      placeholder="Search JobSheet No, Bill No, or Supplier..."
                      placeholderTextColor="#8F816E"
                      value={searchQuery}
                      onChangeText={handleSearch}
                      autoFocus
                    />
                    {searchQuery.length > 0 && (
                      <TouchableOpacity onPress={() => handleSearch('')}>
                        <Ionicons name="close-circle" size={16} color="#8F816E" />
                      </TouchableOpacity>
                    )}
                  </View>
                </View>

                {/* Table Header */}
                <View style={styles.tableHeaderRow}>
                  <ThemedText style={[styles.tableHeaderCell, { flex: 1.3 }]}>JobSheet No</ThemedText>
                  <ThemedText style={[styles.tableHeaderCell, { flex: 1.2 }]}>Purchase Bill No</ThemedText>
                  <ThemedText style={[styles.tableHeaderCell, { flex: 1 }]}>Supplier Code</ThemedText>
                </View>

                {/* Table Content */}
                {loadingLookup ? (
                  <View style={styles.modalLoading}>
                    <ActivityIndicator size="large" color="#B8903B" />
                    <ThemedText style={styles.modalLoadingText}>Fetching job sheets...</ThemedText>
                  </View>
                ) : filteredLookupData.length === 0 ? (
                  <View style={styles.modalEmpty}>
                    <Ionicons name="document-text-outline" size={32} color="#C4B69C" />
                    <ThemedText style={styles.modalEmptyText}>No job sheets found</ThemedText>
                  </View>
                ) : (
                  <FlatList
                    data={filteredLookupData}
                    keyExtractor={(item, index) => item.JobSheetNo || index.toString()}
                    style={{ maxHeight: 360 }}
                    renderItem={({ item, index }) => (
                      <TouchableOpacity
                        style={[
                          styles.tableRow,
                          index % 2 === 1 && { backgroundColor: '#FCFAF6' },
                        ]}
                        activeOpacity={0.7}
                        onPress={() => selectJobSheet(item)}
                      >
                        <ThemedText style={[styles.tableRowCell, styles.boldCell, { flex: 1.3 }]}>
                          {item.JobSheetNo}
                        </ThemedText>
                        <ThemedText style={[styles.tableRowCell, { flex: 1.2 }]}>
                          {item.PurchaseBillNo || '-'}
                        </ThemedText>
                        <ThemedText style={[styles.tableRowCell, { flex: 1 }]}>
                          {item.SupplierCode || '-'}
                        </ThemedText>
                      </TouchableOpacity>
                    )}
                  />
                )}
              </View>
            </TouchableWithoutFeedback>
          </View>
        </TouchableWithoutFeedback>
      </Modal>
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#FBF9F5',
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
    gap: 10,
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
    letterSpacing: 0.5,
    color: '#3B2D17',
  },
  headerRight: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 8,
  },
  actionBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    paddingHorizontal: 12,
    paddingVertical: 5,
    borderRadius: 6,
    borderWidth: 1,
    gap: 5,
    minHeight: 28,
  },
  saveBtn: {
    backgroundColor: '#FAF0D7',
    borderColor: '#D8B257',
  },
  saveBtnText: {
    color: '#7D5800',
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

  // ===== MAIN CONTENT =====
  scrollView: {
    flex: 1,
  },
  scrollContent: {
    padding: 12,
    alignItems: 'center',
  },
  mainWrapper: {
    width: '100%',
    maxWidth: 700,
    gap: 12,
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
    paddingHorizontal: 12,
    paddingVertical: 8,
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
  },
  headerTitleContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 8,
  },
  headerIconBadge: {
    width: 24,
    height: 24,
    borderRadius: 5,
    backgroundColor: '#FFF8EA',
    borderWidth: 1,
    borderColor: '#E2D2AF',
    alignItems: 'center',
    justifyContent: 'center',
  },
  cardHeaderTitle: {
    fontSize: 13,
    fontWeight: 'bold',
    color: '#3B2D17',
    letterSpacing: 0.3,
  },
  cardBody: {
    padding: 12,
  },

  // ===== FORM ROWS =====
  formRow: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingVertical: 6,
    borderBottomWidth: 1,
    borderBottomColor: '#F5EFE4',
  },
  fieldLabel: {
    width: 130,
    fontSize: 12,
    fontWeight: '600',
    color: '#4A3B2C',
  },
  labelWithSubContainer: {
    width: 130,
    justifyContent: 'center',
  },
  fieldValueContainer: {
    flex: 1,
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
    minWidth: 0,
    ...(Platform.OS === 'web' ? { outlineStyle: 'none', boxSizing: 'border-box' } : {}),
  } as any,
  readOnlyInput: {
    backgroundColor: '#F8F5EE',
    borderColor: '#EADBCA',
    color: '#554736',
  },
  totalHighlightInput: {
    textAlign: 'right',
    fontWeight: 'bold',
    color: '#9E6D08',
    fontSize: 12,
    backgroundColor: '#FFF9ED',
    borderColor: '#C29837',
  },

  // Date Row
  dateRow: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 4,
  },
  datePartInput: {
    textAlign: 'center',
  },
  dateSeparator: {
    color: '#8F816E',
    fontWeight: '600',
    fontSize: 13,
  },

  // Lookup Button
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

  inlineLoading: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 8,
    paddingVertical: 6,
    paddingHorizontal: 4,
  },
  inlineLoadingText: {
    fontSize: 11,
    color: '#8D6409',
    fontStyle: 'italic',
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
    shadowOffset: { width: 0, height: 3 },
    shadowOpacity: 0.2,
    shadowRadius: 4,
  },
  toastText: {
    color: '#FFFFFF',
    fontWeight: 'bold',
    fontSize: 12,
  },

  // ===== MODAL =====
  modalOverlay: {
    flex: 1,
    backgroundColor: 'rgba(30, 20, 10, 0.45)',
    justifyContent: 'center',
    alignItems: 'center',
    padding: 16,
  },
  modalContainer: {
    width: '100%',
    maxWidth: 600,
    backgroundColor: '#FFFFFF',
    borderRadius: 10,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    overflow: 'hidden',
    shadowColor: '#A38035',
    shadowOffset: { width: 0, height: 6 },
    shadowOpacity: 0.25,
    shadowRadius: 10,
    elevation: 8,
  },
  modalHeader: {
    backgroundColor: '#F5E7CA',
    paddingHorizontal: 14,
    paddingVertical: 9,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
  },
  modalTitleContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 8,
  },
  modalIconBadge: {
    width: 24,
    height: 24,
    borderRadius: 5,
    backgroundColor: '#FFF8EA',
    borderWidth: 1,
    borderColor: '#E2D2AF',
    alignItems: 'center',
    justifyContent: 'center',
  },
  modalTitle: {
    fontSize: 13,
    fontWeight: 'bold',
    color: '#3B2D17',
  },
  modalCloseBtn: {
    padding: 4,
    borderRadius: 4,
  },
  modalSearchSection: {
    padding: 10,
    backgroundColor: '#FBF9F5',
    borderBottomWidth: 1,
    borderBottomColor: '#EBE2D2',
  },
  modalSearchBox: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderRadius: 6,
    paddingHorizontal: 10,
    height: 30,
  },
  modalSearchInput: {
    flex: 1,
    fontSize: 12,
    color: '#2B2118',
    paddingVertical: 0,
    minWidth: 0,
    ...(Platform.OS === 'web' ? { outlineStyle: 'none' } : {}),
  } as any,
  tableHeaderRow: {
    flexDirection: 'row',
    backgroundColor: '#F5E7CA',
    paddingVertical: 7,
    paddingHorizontal: 12,
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
  },
  tableHeaderCell: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3B2D17',
    textTransform: 'uppercase',
  },
  tableRow: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingVertical: 8,
    paddingHorizontal: 12,
    borderBottomWidth: 1,
    borderBottomColor: '#F3EBDD',
  },
  tableRowCell: {
    fontSize: 12,
    color: '#3B2D17',
  },
  boldCell: {
    fontWeight: 'bold',
    color: '#8D6409',
  },
  modalLoading: {
    padding: 30,
    alignItems: 'center',
    gap: 8,
  },
  modalLoadingText: {
    fontSize: 12,
    color: '#8D6409',
  },
  modalEmpty: {
    padding: 36,
    alignItems: 'center',
    gap: 8,
  },
  modalEmptyText: {
    fontSize: 12,
    color: '#7D6F5B',
  },
});
