import { ThemedText } from '@/components/themed-text';
import { API_BASE_URL } from '@/constants/api';
import { useThemeColors } from '@/hooks/useThemeColors';
import { useThemeContext } from '@/contexts/ThemeContext';
import { Ionicons } from '@expo/vector-icons';
import { Stack, useRouter } from 'expo-router';
import React, { useEffect, useState, useRef } from 'react';
import {
  ActivityIndicator,
  Platform,
  ScrollView,
  StyleSheet,
  TextInput,
  TouchableOpacity,
  View,
  Modal,
  FlatList,
  TouchableWithoutFeedback,
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';

const API_URL = `${API_BASE_URL}/jobsheetcosting`;

export default function JobSheetCostingScreen() {
  const themeColors = useThemeColors();
  const { accentColor } = useThemeContext();
  const { background: backgroundColor, card: cardColor, text: textColor, border: borderColor, primaryGold: GOLD_COLOR, subtitle: subtitleColor } = themeColors;
  const router = useRouter();
  const isDark = backgroundColor !== '#FFFFFF' && backgroundColor !== '#F5F5F5';

  const [jobSheetNo, setJobSheetNo] = useState('');
  const [invoiceNo, setInvoiceNo] = useState('');
  const [invoiceDateDay, setInvoiceDateDay] = useState('');
  const [invoiceDateMonth, setInvoiceDateMonth] = useState('');
  const [invoiceDateYear, setInvoiceDateYear] = useState('');
  const [supplier, setSupplier] = useState('');
  const [classes, setClasses] = useState<string[]>(['All']);
  const [jobClass, setJobClass] = useState('All');
  const [oldCost, setOldCost] = useState('');
  const [newCost, setNewCost] = useState('');
  const [goldValue, setGoldValue] = useState('');
  const [cashValue, setCashValue] = useState('');
  const [loading, setLoading] = useState(false);
  const [saving, setSaving] = useState(false);

  // Toast Notification State
  const [toast, setToast] = useState<{ visible: boolean; message: string; type: 'success' | 'error' | 'info' }>({
    visible: false,
    message: '',
    type: 'success'
  });

  const showToast = (message: string, type: 'success' | 'error' | 'info' = 'success') => {
    setToast({ visible: true, message, type });
    setTimeout(() => {
      setToast(prev => ({ ...prev, visible: false }));
    }, 3500);
  };

  // LOV State
  const [isLOVVisible, setIsLOVVisible] = useState(false);
  const [lovData, setLovData] = useState<any[]>([]);
  const [lovLoading, setLovLoading] = useState(false);
  const [searchFilter, setSearchFilter] = useState('');

  // Refs for auto-focusing
  const newCostRef = useRef<TextInput>(null);
  const jobSheetNoRef = useRef<TextInput>(null);

  // Fetch LOV Data
  const fetchLOV = async () => {
    setLovLoading(true);
    setIsLOVVisible(true);
    setSearchFilter('');
    try {
      const res = await fetch(`${API_URL}/list`);
      const json = await res.json();
      if (json.success) {
        setLovData(json.data || []);
      } else {
        showToast(json.message || 'Failed to fetch jobsheets', 'error');
      }
    } catch (err: any) {
      console.error('LOV Error:', err);
      showToast('Network error while fetching jobsheets', 'error');
    } finally {
      setLovLoading(false);
    }
  };

  // subGetJobSheetDetail
  const fetchJobSheetDetails = async (sheetNo?: string | any) => {
    const targetNo = typeof sheetNo === 'string' ? sheetNo : jobSheetNo;
    if (!targetNo || !targetNo.trim()) return;
    setLoading(true);
    try {
      const res = await fetch(`${API_URL}/details?JobSheetNo=${encodeURIComponent(targetNo.trim())}`);
      const json = await res.json();
      if (json.success && json.data) {
        setInvoiceNo(json.data.PurchaseBillNo || '');
        if (json.data.PurchaseBillDate) {
          const date = new Date(json.data.PurchaseBillDate);
          if (!isNaN(date.getTime())) {
            setInvoiceDateDay(date.getDate().toString().padStart(2, '0'));
            setInvoiceDateMonth((date.getMonth() + 1).toString().padStart(2, '0'));
            setInvoiceDateYear(date.getFullYear().toString());
          }
        }
        setSupplier(json.data.SupplierCode || '');
        const loadedClasses = ['All', ...(json.data.classes || [])];
        setClasses(loadedClasses);
        setJobClass('All');
        fetchOldCost(targetNo.trim(), 'All');
      } else {
        showToast('Jobsheet No not found.', 'error');
        handleClear(true);
        setTimeout(() => jobSheetNoRef.current?.focus(), 150);
      }
    } catch (err) {
      console.error(err);
      showToast('Jobsheet No not found.', 'error');
    } finally {
      setLoading(false);
    }
  };

  // subGetOldCostPerGram
  const fetchOldCost = async (sheetNo: string, selectedClass: string) => {
    if (!sheetNo.trim()) return;
    try {
      const res = await fetch(`${API_URL}/oldcost?JobSheetNo=${encodeURIComponent(sheetNo.trim())}&ClassName=${encodeURIComponent(selectedClass)}`);
      const json = await res.json();
      if (json.success && json.data) {
        const costNum = parseFloat(json.data.CostPerGram) || 0;
        setOldCost(costNum.toFixed(2));
        setTimeout(() => newCostRef.current?.focus(), 100);
      } else {
        setOldCost('0.00');
      }
    } catch (err) {
      console.error(err);
      setOldCost('0.00');
    }
  };

  // On class combo selection changed
  const handleClassChange = (newSelectedClass: string) => {
    setJobClass(newSelectedClass);
    if (jobSheetNo.trim()) {
      fetchOldCost(jobSheetNo.trim(), newSelectedClass);
    }
  };

  // fncValidateMe & subSave
  const handleSave = async () => {
    // 1. JobsheeNo should not be blank. (Exact spelling in frmFixCostJobSheet.vb)
    if (!jobSheetNo || jobSheetNo.trim() === '') {
      showToast('JobsheeNo should not be blank.', 'error');
      jobSheetNoRef.current?.focus();
      return;
    }

    // 2. Class check
    if (!jobClass || jobClass.trim() === '') {
      showToast('Please select the classCode.', 'error');
      return;
    }

    // 3. New cost validations
    if (!newCost || newCost.trim() === '') {
      showToast('New cost should not be blank.', 'error');
      newCostRef.current?.focus();
      return;
    } else if (isNaN(Number(newCost))) {
      showToast('New cost should be numeric.', 'error');
      newCostRef.current?.focus();
      return;
    }

    setSaving(true);
    try {
      const payload = {
        JobSheetNo: jobSheetNo.trim(),
        ClassName: jobClass,
        OldCost: oldCost,
        NewCost: parseFloat(newCost).toFixed(2),
        GoldValue: goldValue ? parseFloat(goldValue) : 0,
        CashValue: cashValue ? parseFloat(cashValue) : 0
      };

      const response = await fetch(`${API_URL}/updatecost`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload),
      });

      const json = await response.json();
      if (json.success) {
        showToast('Cost updated successfully!', 'success');
        handleClear(true);
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

  // subClearMe
  const handleClear = (fullClear = true) => {
    if (fullClear) setJobSheetNo('');
    setInvoiceNo('');
    setInvoiceDateDay('');
    setInvoiceDateMonth('');
    setInvoiceDateYear('');
    setSupplier('');
    setClasses(['All']);
    setJobClass('All');
    setOldCost('');
    setNewCost('');
    setGoldValue('');
    setCashValue('');
  };

  // Keyboard shortcut handler (F6: Clear, F7: Save, Escape: Back)
  useEffect(() => {
    if (Platform.OS === 'web') {
      const handleKeyDown = (e: KeyboardEvent) => {
        if (e.key === 'F6') {
          e.preventDefault();
          handleClear(true);
        } else if (e.key === 'F7') {
          e.preventDefault();
          handleSave();
        } else if (e.key === 'Escape') {
          if (isLOVVisible) {
            setIsLOVVisible(false);
          } else {
            router.back();
          }
        }
      };
      window.addEventListener('keydown', handleKeyDown);
      return () => window.removeEventListener('keydown', handleKeyDown);
    }
  }, [jobSheetNo, jobClass, oldCost, newCost, goldValue, cashValue, isLOVVisible]);

  // Filtered LOV Data
  const filteredLovData = lovData.filter(item => {
    if (!searchFilter.trim()) return true;
    const q = searchFilter.toLowerCase();
    return (
      (item.JobSheetNo && item.JobSheetNo.toLowerCase().includes(q)) ||
      (item.SupplierCode && item.SupplierCode.toLowerCase().includes(q)) ||
      (item.PurchaseBillNo && item.PurchaseBillNo.toLowerCase().includes(q))
    );
  });

  return (
    <SafeAreaView style={[styles.safeArea, { backgroundColor: '#FBF9F5' }]} edges={['top']}>
      <Stack.Screen options={{ headerShown: false }} />

      {/* Floating In-App Toast Banner */}
      {toast.visible && (
        <View style={[
          styles.toastBanner,
          toast.type === 'error' ? styles.toastError : styles.toastSuccess
        ]}>
          <Ionicons
            name={toast.type === 'error' ? 'alert-circle' : 'checkmark-circle'}
            size={18}
            color="#FFFFFF"
            style={{ marginRight: 8 }}
          />
          <ThemedText style={styles.toastText}>{toast.message}</ThemedText>
        </View>
      )}

      {/* Header */}
      <View style={[styles.header, { borderBottomColor: '#EBE2D2', backgroundColor: '#FFFFFF' }]}>
        <View style={{ flexDirection: 'row', alignItems: 'center' }}>
          <TouchableOpacity style={styles.backButton} onPress={() => router.back()}>
            <Ionicons name="arrow-back" size={22} color="#3B2D17" />
          </TouchableOpacity>
          <ThemedText style={[styles.title, { color: '#3B2D17', fontWeight: '500' }]}>Job Sheet Costing</ThemedText>
        </View>
        <View style={{ flexDirection: 'row', gap: 8 }}>
          <TouchableOpacity style={styles.topActionBtn} onPress={() => handleClear(true)}>
            <ThemedText style={{ color: '#5C4A28', fontSize: 12, fontWeight: 'bold' }}>Clear (F6)</ThemedText>
          </TouchableOpacity>
          <TouchableOpacity
            style={[styles.topActionBtn, { backgroundColor: '#F9F2E2', borderColor: '#B8903B' }]}
            onPress={handleSave}
            disabled={saving}
          >
            <ThemedText style={{ color: '#9E6D08', fontSize: 12, fontWeight: 'bold' }}>
              {saving ? "Saving..." : "Save (F7)"}
            </ThemedText>
          </TouchableOpacity>
        </View>
      </View>

      <ScrollView contentContainerStyle={styles.scrollContainer}>
        {/* Luxury Gold Card */}
        <View style={styles.goldCard}>
          {/* Card Header Banner */}
          <View style={styles.cardHeaderBanner}>
            <View style={styles.headerIconBadge}>
              <Ionicons name="calculator-outline" size={15} color="#B48218" />
            </View>
            <ThemedText style={styles.cardHeaderTitle}>Costing Details</ThemedText>
          </View>

          <View style={styles.cardBody}>
            {/* JobSheet No */}
            <View style={styles.inputGroup}>
              <ThemedText style={styles.label}>JobSheet No</ThemedText>
              <View style={styles.rowInputs}>
                <TextInput
                  ref={jobSheetNoRef}
                  style={[styles.input, { flex: 1, borderTopRightRadius: 0, borderBottomRightRadius: 0, borderRightWidth: 0 }]}
                  value={jobSheetNo}
                  onChangeText={setJobSheetNo}
                  onBlur={() => fetchJobSheetDetails(jobSheetNo)}
                  onSubmitEditing={() => fetchJobSheetDetails(jobSheetNo)}
                  placeholder="Enter JobSheet No"
                />
                <TouchableOpacity
                  style={styles.lookupBtn}
                  onPress={fetchLOV}
                >
                  {lovLoading ? <ActivityIndicator size="small" color="#9E6D08" /> : <ThemedText style={styles.lookupBtnText}>...</ThemedText>}
                </TouchableOpacity>
              </View>
            </View>

            {/* Invoice No */}
            <View style={styles.inputGroup}>
              <ThemedText style={styles.label}>Invoice No</ThemedText>
              <TextInput
                style={[styles.input, styles.readonlyInput, { flex: 1 }]}
                value={invoiceNo}
                editable={false}
              />
            </View>

            {/* Invoice Date */}
            <View style={styles.inputGroup}>
              <ThemedText style={styles.label}>Invoice Date</ThemedText>
              <View style={[styles.rowInputs, { justifyContent: 'flex-start', gap: 8, flex: 1 }]}>
                <TextInput
                  style={[styles.input, styles.readonlyInput, { width: 52, textAlign: 'center' }]}
                  value={invoiceDateDay}
                  editable={false}
                  placeholder="DD"
                />
                <ThemedText style={{ color: '#A08E73', fontWeight: 'bold' }}>-</ThemedText>
                <TextInput
                  style={[styles.input, styles.readonlyInput, { width: 52, textAlign: 'center' }]}
                  value={invoiceDateMonth}
                  editable={false}
                  placeholder="MM"
                />
                <ThemedText style={{ color: '#A08E73', fontWeight: 'bold' }}>-</ThemedText>
                <TextInput
                  style={[styles.input, styles.readonlyInput, { width: 72, textAlign: 'center' }]}
                  value={invoiceDateYear}
                  editable={false}
                  placeholder="YYYY"
                />
              </View>
            </View>

            {/* Supplier */}
            <View style={styles.inputGroup}>
              <ThemedText style={styles.label}>Supplier</ThemedText>
              <TextInput
                style={[styles.input, styles.readonlyInput, { flex: 1 }]}
                value={supplier}
                editable={false}
              />
            </View>

            {/* Class */}
            <View style={styles.inputGroup}>
              <ThemedText style={styles.label}>Class</ThemedText>
              <View style={[styles.input, { padding: 0, overflow: 'hidden', flex: 1 }]}>
                {Platform.OS === 'web' ? (
                  <select
                    style={{
                      width: '100%',
                      padding: '4px 8px',
                      border: 'none',
                      backgroundColor: 'transparent',
                      color: '#2C2519',
                      outline: 'none',
                      fontSize: '12px',
                      fontWeight: 'bold',
                      height: '28px',
                    }}
                    value={jobClass}
                    onChange={(e) => handleClassChange(e.target.value)}
                  >
                    {classes.map(c => <option key={c} value={c}>{c}</option>)}
                  </select>
                ) : (
                  <TextInput
                    style={{ paddingHorizontal: 8, paddingVertical: 4, color: '#2C2519', fontWeight: 'bold', fontSize: 12 }}
                    value={jobClass}
                    onChangeText={handleClassChange}
                  />
                )}
              </View>
            </View>

            {/* Old Cost Per Gram */}
            <View style={styles.inputGroup}>
              <ThemedText style={styles.label}>Old Cost Per Gram</ThemedText>
              <TextInput
                style={[styles.input, styles.readonlyInput, { flex: 1, textAlign: 'right' }]}
                value={oldCost}
                editable={false}
                keyboardType="numeric"
              />
            </View>

            {/* New Cost Per Gram */}
            <View style={styles.inputGroup}>
              <ThemedText style={styles.label}>New Cost Per Gram</ThemedText>
              <TextInput
                ref={newCostRef}
                style={[styles.input, { flex: 1, textAlign: 'right' }]}
                value={newCost}
                onChangeText={setNewCost}
                keyboardType="numeric"
                placeholder="0.00"
              />
            </View>

            {/* Section Divider / Label */}
            <View style={styles.subSectionBanner}>
              <ThemedText style={styles.subSectionTitle}>Payable Breakdown</ThemedText>
            </View>

            {/* Gold */}
            <View style={styles.inputGroup}>
              <ThemedText style={styles.label}>Gold Value</ThemedText>
              <TextInput
                style={[styles.input, { flex: 1, textAlign: 'right' }]}
                value={goldValue}
                onChangeText={setGoldValue}
                keyboardType="numeric"
                placeholder="0.00"
              />
            </View>

            {/* Cash */}
            <View style={styles.inputGroup}>
              <ThemedText style={styles.label}>Cash Value</ThemedText>
              <TextInput
                style={[styles.input, { flex: 1, textAlign: 'right' }]}
                value={cashValue}
                onChangeText={setCashValue}
                keyboardType="numeric"
                placeholder="0.00"
              />
            </View>
          </View>
        </View>
      </ScrollView>

      {/* LOV Modal */}
      <Modal visible={isLOVVisible} transparent animationType="fade">
        <TouchableOpacity style={styles.modalOverlay} activeOpacity={1} onPress={() => setIsLOVVisible(false)}>
          <TouchableWithoutFeedback>
            <View style={[styles.modalContent, { backgroundColor: '#FFFFFF', borderColor: '#E8DCB8' }]}>
              <View style={[styles.modalHeader, { backgroundColor: '#F5E7CA', borderBottomColor: '#E2D3B3' }]}>
                <ThemedText style={{ fontSize: 15, fontWeight: 'bold', color: '#3B2D17' }}>Select JobSheet (FixStatus = 'N')</ThemedText>
                <TouchableOpacity onPress={() => setIsLOVVisible(false)}>
                  <Ionicons name="close" size={20} color="#3B2D17" />
                </TouchableOpacity>
              </View>

              {/* Search Bar in Modal */}
              <View style={styles.modalSearchBox}>
                <Ionicons name="search" size={16} color="#8F7E66" style={{ marginRight: 6 }} />
                <TextInput
                  style={styles.modalSearchInput}
                  placeholder="Search JobSheet, Supplier, Bill No..."
                  value={searchFilter}
                  onChangeText={setSearchFilter}
                  autoFocus
                />
              </View>

              {/* Table Header */}
              <View style={[styles.tableHeader, { backgroundColor: '#FAF3E3', borderBottomColor: '#E8DCB8' }]}>
                <View style={[styles.cell, { flex: 1.5 }]}><ThemedText style={[styles.headerText, { color: '#4A3B25' }]}>JobSheet No</ThemedText></View>
                <View style={[styles.cell, { flex: 1 }]}><ThemedText style={[styles.headerText, { color: '#4A3B25' }]}>Cost/g</ThemedText></View>
                <View style={[styles.cell, { flex: 1 }]}><ThemedText style={[styles.headerText, { color: '#4A3B25' }]}>Supplier</ThemedText></View>
                <View style={[styles.cell, { flex: 1 }]}><ThemedText style={[styles.headerText, { color: '#4A3B25' }]}>Bill No</ThemedText></View>
              </View>

              {lovLoading ? (
                <View style={styles.centered}><ActivityIndicator size="large" color="#9E6D08" /></View>
              ) : (
                <FlatList
                  data={filteredLovData}
                  style={{ maxHeight: 320 }}
                  keyExtractor={(item, index) => item.JobSheetNo || index.toString()}
                  renderItem={({ item, index }) => (
                    <TouchableOpacity
                      style={[styles.row, { borderBottomColor: '#F0E6D2', backgroundColor: index % 2 === 1 ? '#FAF7F0' : '#FFFFFF' }]}
                      onPress={() => {
                        setJobSheetNo(item.JobSheetNo);
                        setIsLOVVisible(false);
                        fetchJobSheetDetails(item.JobSheetNo);
                      }}
                    >
                      <View style={[styles.cell, { flex: 1.5 }]}><ThemedText style={{ color: '#2C2519', fontSize: 12, fontWeight: 'bold' }}>{item.JobSheetNo}</ThemedText></View>
                      <View style={[styles.cell, { flex: 1 }]}><ThemedText style={{ color: '#2C2519', fontSize: 12 }}>{item.CostPerGram != null ? parseFloat(item.CostPerGram).toFixed(2) : '0.00'}</ThemedText></View>
                      <View style={[styles.cell, { flex: 1 }]}><ThemedText style={{ color: '#2C2519', fontSize: 12 }}>{item.SupplierCode}</ThemedText></View>
                      <View style={[styles.cell, { flex: 1 }]}><ThemedText style={{ color: '#2C2519', fontSize: 12 }}>{item.PurchaseBillNo}</ThemedText></View>
                    </TouchableOpacity>
                  )}
                  ListEmptyComponent={
                    <View style={styles.centered}>
                      <ThemedText style={{ color: '#8F7E66', padding: 20 }}>No JobSheets pending costing found.</ThemedText>
                    </View>
                  }
                />
              )}
            </View>
          </TouchableWithoutFeedback>
        </TouchableOpacity>
      </Modal>

    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  safeArea: { flex: 1 },
  header: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingHorizontal: 16,
    paddingVertical: 10,
    borderBottomWidth: 1,
  },
  backButton: { padding: 4, marginRight: 12 },
  title: { fontSize: 20, fontWeight: '500' },
  topActionBtn: {
    paddingVertical: 6,
    paddingHorizontal: 14,
    borderRadius: 5,
    borderWidth: 1,
    borderColor: '#DFD5C3',
    backgroundColor: '#FFFFFF',
  },
  scrollContainer: {
    padding: 16,
    alignItems: 'center',
  },

  // Toast Banner
  toastBanner: {
    position: 'absolute',
    top: 60,
    alignSelf: 'center',
    zIndex: 9999,
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 18,
    paddingVertical: 10,
    borderRadius: 24,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.25,
    shadowRadius: 8,
    elevation: 8,
  },
  toastError: {
    backgroundColor: '#C53030',
  },
  toastSuccess: {
    backgroundColor: '#276749',
  },
  toastText: {
    color: '#FFFFFF',
    fontSize: 13,
    fontWeight: 'bold',
  },

  // Luxury Gold Card
  goldCard: {
    width: '100%',
    maxWidth: 500,
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    overflow: 'hidden',
    shadowColor: '#A38035',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.08,
    shadowRadius: 6,
    elevation: 2,
  },
  cardHeaderBanner: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#F5E7CA',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    paddingHorizontal: 14,
    paddingVertical: 10,
  },
  headerIconBadge: {
    width: 24,
    height: 24,
    borderRadius: 12,
    backgroundColor: '#FFFFFF',
    justifyContent: 'center',
    alignItems: 'center',
    marginRight: 8,
    borderWidth: 1,
    borderColor: '#DECFA9',
  },
  cardHeaderTitle: {
    fontSize: 14,
    fontWeight: 'bold',
    color: '#3B2D17',
  },
  cardBody: {
    padding: 16,
    gap: 10,
  },
  inputGroup: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
  },
  label: {
    width: 140,
    fontSize: 12,
    fontWeight: 'bold',
    color: '#3B2D17',
  },
  input: {
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderRadius: 6,
    paddingHorizontal: 10,
    paddingVertical: 4,
    fontSize: 12,
    height: 32,
    backgroundColor: '#FFFFFF',
    color: '#2C2519',
    justifyContent: 'center',
  },
  readonlyInput: {
    backgroundColor: '#F8F5EE',
    borderColor: '#EADBCA',
    color: '#554736',
  },
  rowInputs: {
    flex: 1,
    flexDirection: 'row',
    alignItems: 'center',
  },
  lookupBtn: {
    width: 34,
    height: 32,
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
  subSectionBanner: {
    backgroundColor: '#FAF3E3',
    borderWidth: 1,
    borderColor: '#ECD8AB',
    borderRadius: 5,
    paddingVertical: 5,
    paddingHorizontal: 10,
    marginVertical: 10,
  },
  subSectionTitle: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#5B421A',
  },

  // Modal
  modalOverlay: {
    flex: 1,
    backgroundColor: 'rgba(0,0,0,0.5)',
    justifyContent: 'center',
    alignItems: 'center',
    padding: 20,
  },
  modalContent: {
    width: '100%',
    maxWidth: 550,
    borderWidth: 1,
    borderRadius: 8,
    overflow: 'hidden',
  },
  modalHeader: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    paddingHorizontal: 14,
    paddingVertical: 10,
    borderBottomWidth: 1,
  },
  modalSearchBox: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 12,
    paddingVertical: 8,
    borderBottomWidth: 1,
    borderBottomColor: '#E8DCB8',
    backgroundColor: '#FAF7F0',
  },
  modalSearchInput: {
    flex: 1,
    fontSize: 12,
    color: '#2C2519',
    padding: 0,
  },
  tableHeader: {
    flexDirection: 'row',
    paddingHorizontal: 12,
    paddingVertical: 8,
    borderBottomWidth: 1,
  },
  headerText: {
    fontSize: 11,
    fontWeight: 'bold',
  },
  row: {
    flexDirection: 'row',
    paddingHorizontal: 12,
    paddingVertical: 8,
    borderBottomWidth: 1,
    alignItems: 'center',
  },
  cell: {
    justifyContent: 'center',
  },
  centered: {
    padding: 20,
    justifyContent: 'center',
    alignItems: 'center',
  },
});
