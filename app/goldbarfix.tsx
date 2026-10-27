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

export default function GoldBarFixScreen() {
  const router = useRouter();

  // State
  const [jobSheetNo, setJobSheetNo] = useState('');
  const [invoiceNo, setInvoiceNo] = useState('');
  const [invoiceDate, setInvoiceDate] = useState({ DD: '', MM: '', YYYY: '' });
  const [supplierCode, setSupplierCode] = useState('');
  const [taelPercent, setTaelPercent] = useState('');

  const [totalCost, setTotalCost] = useState('0.00');
  const [totalMC, setTotalMC] = useState('0.00');
  const [gstAmount, setGstAmount] = useState('0.00');
  const [hasGST, setHasGST] = useState(false);
  const [weight, setWeight] = useState('0.00');
  const [goldLoss, setGoldLoss] = useState('0.00');
  const [weight24K, setWeight24K] = useState('0.00');
  const [fix, setFix] = useState('0.00');

  const [loading, setLoading] = useState(false);
  const [saving, setSaving] = useState(false);

  // Toast Notification State
  const [toast, setToast] = useState<{ message: string; type: 'success' | 'error' } | null>(null);

  const showToast = (message: string, type: 'success' | 'error' = 'success') => {
    setToast({ message, type });
    setTimeout(() => {
      setToast(null);
    }, 3000);
  };

  // LOV State
  const [isLOVVisible, setIsLOVVisible] = useState(false);
  const [lovData, setLovData] = useState<any[]>([]);
  const [filteredLovData, setFilteredLovData] = useState<any[]>([]);
  const [lovSearch, setLovSearch] = useState('');
  const [lovLoading, setLovLoading] = useState(false);
  const [lovType, setLovType] = useState<'jobsheet' | 'tael'>('jobsheet');

  const fetchLOV = async (type: 'jobsheet' | 'tael') => {
    setLovType(type);
    setLovLoading(true);
    setLovSearch('');
    setIsLOVVisible(true);
    setLovData([]);
    setFilteredLovData([]);
    try {
      const url =
        type === 'jobsheet'
          ? `${API_BASE_URL}/goldbarfix/lov/jobsheets`
          : `${API_BASE_URL}/taelpercent`;

      const res = await fetch(url);
      const json = await res.json();
      const list = json.success || json.data ? (json.data || json) : [];
      setLovData(list);
      setFilteredLovData(list);
    } catch (err) {
      console.error('LOV Error:', err);
      const fallback =
        type === 'jobsheet'
          ? [{ JobSheetNo: 'JS001', SupplierCode: 'STM', PurchaseBillNo: '', TotalWeight24k: 0 }]
          : [{ TaelPercent: '0.999', TaelPercent1: 0, TaelPercent2: 0, TaelPercent3: 0 }];
      setLovData(fallback);
      setFilteredLovData(fallback);
    } finally {
      setLovLoading(false);
    }
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

  const handleSelectLOV = (item: any) => {
    if (lovType === 'jobsheet') {
      const jsVal = String(item.JobSheetNo || '').trim();
      setJobSheetNo(jsVal);
      fetchJobSheetDetails(jsVal);
    } else {
      const tpVal = String(item.TaelPercent !== undefined && item.TaelPercent !== null ? item.TaelPercent : '').trim();
      setTaelPercent(tpVal);
      calculate24KWeight(weight, tpVal);
    }
    setIsLOVVisible(false);
  };

  const fetchJobSheetDetails = async (jsNo: string) => {
    const trimmed = (jsNo || '').trim();
    if (!trimmed) return;
    setLoading(true);
    try {
      const res = await fetch(`${API_BASE_URL}/goldbarfix/${encodeURIComponent(trimmed)}`);
      const json = await res.json();
      if (json.success && json.data) {
        const d = json.data;
        setJobSheetNo(String(d.JobSheetNo || trimmed).trim());
        setInvoiceNo(String(d.PurchaseBillNo || '').trim());

        const pDateStr = String(d.Pdate || '').trim();
        if (pDateStr.length >= 8) {
          setInvoiceDate({
            YYYY: pDateStr.substring(0, 4),
            MM: pDateStr.substring(4, 6),
            DD: pDateStr.substring(6, 8),
          });
        } else if (d.PurchaseBillDate) {
          const dt = new Date(d.PurchaseBillDate);
          if (!isNaN(dt.getTime())) {
            setInvoiceDate({
              YYYY: dt.getFullYear().toString(),
              MM: String(dt.getMonth() + 1).padStart(2, '0'),
              DD: String(dt.getDate()).padStart(2, '0'),
            });
          }
        }

        setSupplierCode(String(d.SupplierCode || '').trim());
        const tpVal = d.TaelPercent !== undefined && d.TaelPercent !== null ? String(d.TaelPercent).trim() : '0';
        setTaelPercent(tpVal);

        const costVal = parseFloat(d.ManualLocalCOST) || 0;
        const mcVal = parseFloat(d.ManualMc) || 0;
        const wtVal = parseFloat(d.ManualWeight) || 0;
        const glVal = parseFloat(d.ManualGoldLoss) || 0;
        const gstVal = parseFloat(d.Gst) || 0;
        const fixVal = parseFloat(d.PurchaseFix) || 0;

        setTotalCost(costVal.toFixed(2));
        setTotalMC(mcVal.toFixed(2));
        setWeight(wtVal.toFixed(2));
        setGoldLoss(glVal.toFixed(2));

        setHasGST(gstVal > 0);
        setGstAmount(gstVal.toFixed(2));

        setFix(fixVal.toFixed(2));

        calculate24KWeight(wtVal, tpVal);
        showToast('JobSheet details loaded');
      } else {
        showToast(json.message || 'Job sheet no. is invalid!', 'error');
      }
    } catch (err) {
      console.error(err);
      showToast('Error loading JobSheet details', 'error');
    } finally {
      setLoading(false);
    }
  };

  const calculate24KWeight = (wt: any, tp: any) => {
    const w = parseFloat(wt) || 0;
    const t = parseFloat(tp) || 0;
    const calc = (w * t).toFixed(3);
    setWeight24K(calc);
  };

  const handleWeightChange = (val: string) => {
    setWeight(val);
    calculate24KWeight(val, taelPercent);
  };

  const handleTaelPercentChange = (val: string) => {
    setTaelPercent(val);
    calculate24KWeight(weight, val);
  };

  const handleSave = async () => {
    const trimmedJS = (jobSheetNo || '').trim();
    if (!trimmedJS) {
      showToast('Please enter or select a JobSheet No.', 'error');
      return;
    }

    setSaving(true);
    try {
      const res = await fetch(`${API_BASE_URL}/goldbarfix/save`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          JobSheetNo: trimmedJS,
          PurchaseBillNo: invoiceNo.trim(),
          SupplierCode: supplierCode.trim(),
          InvoiceDate: `${invoiceDate.YYYY}${invoiceDate.MM}${invoiceDate.DD}`,
          Weight: parseFloat(weight) || 0,
          TotalCost: parseFloat(totalCost) || 0,
          TotalMC: parseFloat(totalMC) || 0,
          GoldLoss: parseFloat(goldLoss) || 0,
          TaelPercent: parseFloat(taelPercent) || 0,
          PurchaseFix: parseFloat(fix) || 0,
          TotalWeight24k: parseFloat(weight24K) || 0,
          GstAmount: parseFloat(gstAmount) || 0,
          HasGST: hasGST,
          UserId: 'sys',
        }),
      });
      const json = await res.json();
      if (json.success) {
        showToast('Transaction Saved Successfully');
      } else {
        showToast('Transaction not saved: ' + (json.message || ''), 'error');
      }
    } catch (err) {
      console.error(err);
      showToast('Error saving data', 'error');
    } finally {
      setSaving(false);
    }
  };

  const handleClear = () => {
    setJobSheetNo('');
    setInvoiceNo('');
    setInvoiceDate({ DD: '', MM: '', YYYY: '' });
    setSupplierCode('');
    setTaelPercent('');
    setTotalCost('0.00');
    setTotalMC('0.00');
    setGstAmount('0.00');
    setHasGST(false);
    setWeight('0.00');
    setGoldLoss('0.00');
    setWeight24K('0.00');
    setFix('0.00');
    showToast('Form Cleared');
  };

  return (
    <SafeAreaView edges={['top']} style={styles.safeArea}>
      <Stack.Screen options={{ headerShown: false }} />

      {/* Top Header Bar matching jobsheet.tsx */}
      <View style={styles.header}>
        <View style={styles.headerLeft}>
          <TouchableOpacity onPress={() => router.back()} style={styles.backButton}>
            <Ionicons name="arrow-back" size={20} color="#3B2D17" />
          </TouchableOpacity>
          <ThemedText style={styles.headerTitle}>GoldBar Fix</ThemedText>
        </View>

        <View style={styles.headerActions}>
          <TouchableOpacity style={styles.headerBtnSecondary} onPress={handleClear}>
            <ThemedText style={styles.headerBtnSecondaryText}>Clear</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity
            style={[styles.headerBtnPrimary, saving && { opacity: 0.7 }]}
            onPress={handleSave}
            disabled={saving}
          >
            {saving ? (
              <ActivityIndicator size="small" color="#9E6D08" />
            ) : (
              <ThemedText style={styles.headerBtnPrimaryText}>Save</ThemedText>
            )}
          </TouchableOpacity>
        </View>
      </View>

      <ScrollView style={styles.container} contentContainerStyle={styles.contentContainer}>
        <View style={styles.mainWrapper}>
          {/* Card 1: JobSheet Reference Information */}
          <View style={styles.goldCard}>
            <View style={styles.cardHeaderBanner}>
              <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                <View style={styles.headerIconBadge}>
                  <Ionicons name="document-text" size={15} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitle}>Reference JobSheet</ThemedText>
              </View>
            </View>

            <View style={styles.cardBody}>
              {/* JobSheet No */}
              <View style={styles.formRow}>
                <ThemedText style={styles.label}>JobSheet No.</ThemedText>
                <View style={{ width: 160, flexDirection: 'row' }}>
                  <TextInput
                    style={[styles.input, { flex: 1, borderTopRightRadius: 0, borderBottomRightRadius: 0, borderRightWidth: 0 }]}
                    value={jobSheetNo}
                    onChangeText={setJobSheetNo}
                    onBlur={() => {
                      if (jobSheetNo.trim()) {
                        fetchJobSheetDetails(jobSheetNo);
                      }
                    }}
                    placeholder="Enter / select"
                  />
                  <TouchableOpacity style={styles.lookupBtn} onPress={() => fetchLOV('jobsheet')}>
                    <ThemedText style={styles.lookupBtnText}>...</ThemedText>
                  </TouchableOpacity>
                </View>
              </View>

              {/* Invoice No */}
              <View style={styles.formRow}>
                <ThemedText style={styles.label}>Invoice No</ThemedText>
                <TextInput
                  style={[styles.input, { width: 160 }]}
                  value={invoiceNo}
                  onChangeText={setInvoiceNo}
                  placeholder="Invoice No"
                />
              </View>

              {/* Invoice Date */}
              <View style={styles.formRow}>
                <ThemedText style={styles.label}>Invoice Date</ThemedText>
                <View style={styles.dateContainer}>
                  <TextInput
                    style={styles.dateInputShort}
                    value={invoiceDate.DD}
                    onChangeText={(val) => setInvoiceDate(prev => ({ ...prev, DD: val }))}
                    placeholder="DD"
                    maxLength={2}
                    keyboardType="numeric"
                  />
                  <ThemedText style={{ color: '#7D6F5B' }}>-</ThemedText>
                  <TextInput
                    style={styles.dateInputShort}
                    value={invoiceDate.MM}
                    onChangeText={(val) => setInvoiceDate(prev => ({ ...prev, MM: val }))}
                    placeholder="MM"
                    maxLength={2}
                    keyboardType="numeric"
                  />
                  <ThemedText style={{ color: '#7D6F5B' }}>-</ThemedText>
                  <TextInput
                    style={styles.dateInputLong}
                    value={invoiceDate.YYYY}
                    onChangeText={(val) => setInvoiceDate(prev => ({ ...prev, YYYY: val }))}
                    placeholder="YYYY"
                    maxLength={4}
                    keyboardType="numeric"
                  />
                </View>
              </View>

              {/* Supplier Code */}
              <View style={styles.formRow}>
                <ThemedText style={styles.label}>Supplier Code</ThemedText>
                <TextInput
                  style={[styles.input, { width: 160 }]}
                  value={supplierCode}
                  onChangeText={setSupplierCode}
                  placeholder="Supplier Code"
                />
              </View>

              {/* TEAL Percent */}
              <View style={styles.formRow}>
                <ThemedText style={styles.label}>TEAL Percent</ThemedText>
                <View style={{ width: 160, flexDirection: 'row' }}>
                  <TextInput
                    style={[styles.input, { flex: 1, borderTopRightRadius: 0, borderBottomRightRadius: 0, borderRightWidth: 0 }]}
                    value={taelPercent}
                    onChangeText={handleTaelPercentChange}
                    keyboardType="numeric"
                    placeholder="0.00"
                  />
                  <TouchableOpacity style={styles.lookupBtn} onPress={() => fetchLOV('tael')}>
                    <ThemedText style={styles.lookupBtnText}>...</ThemedText>
                  </TouchableOpacity>
                </View>
              </View>
            </View>
          </View>

          {/* Card 2: Cost & Weight Fix Specifications */}
          <View style={styles.goldCard}>
            <View style={styles.cardHeaderBanner}>
              <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                <View style={styles.headerIconBadge}>
                  <Ionicons name="calculator" size={15} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitle}>Fix & Weight Calculation</ThemedText>
              </View>
            </View>

            <View style={styles.cardBody}>
              {/* Total Cost */}
              <View style={styles.formRow}>
                <ThemedText style={styles.label}>Total Cost</ThemedText>
                <TextInput
                  style={[styles.input, { width: 160, textAlign: 'right' }]}
                  value={totalCost}
                  onChangeText={setTotalCost}
                  keyboardType="numeric"
                  placeholder="0.00"
                />
              </View>

              {/* Total MC */}
              <View style={styles.formRow}>
                <ThemedText style={styles.label}>Total MC</ThemedText>
                <TextInput
                  style={[styles.input, { width: 160, textAlign: 'right' }]}
                  value={totalMC}
                  onChangeText={setTotalMC}
                  keyboardType="numeric"
                  placeholder="0.00"
                />
              </View>

              {/* GST Checkbox & Amount */}
              <View style={styles.formRow}>
                <TouchableOpacity
                  activeOpacity={0.7}
                  onPress={() => {
                    const newGST = !hasGST;
                    setHasGST(newGST);
                    if (!newGST) {
                      setGstAmount('0.00');
                    }
                  }}
                  style={{ width: 110, flexDirection: 'row', alignItems: 'center' }}
                >
                  <ThemedText style={styles.labelSmall}>GST</ThemedText>
                  <View style={[styles.checkboxSquare, hasGST && styles.checkboxSquareActive]}>
                    {hasGST && <Ionicons name="checkmark" size={10} color="#FFFFFF" />}
                  </View>
                </TouchableOpacity>
                <TextInput
                  style={[styles.input, { width: 160, textAlign: 'right' }]}
                  value={gstAmount}
                  onChangeText={setGstAmount}
                  keyboardType="numeric"
                  placeholder="0.00"
                />
              </View>

              {/* Weight */}
              <View style={styles.formRow}>
                <ThemedText style={styles.label}>Weight</ThemedText>
                <TextInput
                  style={[styles.input, { width: 160, textAlign: 'right' }]}
                  value={weight}
                  onChangeText={handleWeightChange}
                  keyboardType="numeric"
                  placeholder="0.00"
                />
              </View>

              {/* Gold Loss */}
              <View style={styles.formRow}>
                <ThemedText style={styles.label}>Gold Loss</ThemedText>
                <TextInput
                  style={[styles.input, { width: 160, textAlign: 'right' }]}
                  value={goldLoss}
                  onChangeText={setGoldLoss}
                  keyboardType="numeric"
                  placeholder="0.00"
                />
              </View>

              {/* 24K Weight */}
              <View style={styles.formRow}>
                <ThemedText style={styles.label}>24K Weight</ThemedText>
                <TextInput
                  style={[styles.input, { width: 160, textAlign: 'right', fontWeight: 'bold' }]}
                  value={weight24K}
                  onChangeText={setWeight24K}
                  keyboardType="numeric"
                  placeholder="0.000"
                />
              </View>

              {/* Fix */}
              <View style={[styles.formRow, { marginTop: 6, paddingTop: 8, borderTopWidth: 1, borderTopColor: '#F0E6D2' }]}>
                <ThemedText style={[styles.label, { color: '#9E6D08' }]}>Fix</ThemedText>
                <TextInput
                  style={[styles.input, { width: 160, textAlign: 'right', fontWeight: 'bold', borderColor: '#B8903B' }]}
                  value={fix}
                  onChangeText={setFix}
                  keyboardType="numeric"
                  placeholder="0.00"
                />
              </View>
            </View>
          </View>
        </View>
      </ScrollView>

      {/* LOV Search Modal */}
      <Modal visible={isLOVVisible} transparent animationType="fade">
        <TouchableWithoutFeedback onPress={() => setIsLOVVisible(false)}>
          <View style={styles.modalOverlay}>
            <TouchableWithoutFeedback>
              <View style={styles.modalContainer}>
                <View style={styles.modalHeader}>
                  <ThemedText style={styles.modalTitle}>
                    {lovType === 'jobsheet' ? 'Select JobSheet (Supplier: STM)' : 'Select TEAL Percent'}
                  </ThemedText>
                  <TouchableOpacity onPress={() => setIsLOVVisible(false)} style={styles.modalCloseIconBtn}>
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

                  {/* Header Row */}
                  {lovType === 'jobsheet' ? (
                    <View style={styles.lovHeaderRow}>
                      <ThemedText style={[styles.lovHeaderText, { flex: 1.5 }]}>JobSheetNo</ThemedText>
                      <ThemedText style={[styles.lovHeaderText, { flex: 1.2 }]}>Supplier</ThemedText>
                      <ThemedText style={[styles.lovHeaderText, { flex: 1.3 }]}>Bill No</ThemedText>
                      <ThemedText style={[styles.lovHeaderText, { flex: 1.2, textAlign: 'right' }]}>24k Wt</ThemedText>
                    </View>
                  ) : (
                    <View style={styles.lovHeaderRow}>
                      <ThemedText style={[styles.lovHeaderText, { flex: 1.5 }]}>Tael Percent</ThemedText>
                      <ThemedText style={[styles.lovHeaderText, { flex: 1, textAlign: 'right' }]}>Percent 1</ThemedText>
                      <ThemedText style={[styles.lovHeaderText, { flex: 1, textAlign: 'right' }]}>Percent 2</ThemedText>
                      <ThemedText style={[styles.lovHeaderText, { flex: 1, textAlign: 'right' }]}>Percent 3</ThemedText>
                    </View>
                  )}

                  {lovLoading ? (
                    <View style={{ padding: 24, alignItems: 'center' }}>
                      <ActivityIndicator size="large" color="#B48218" />
                    </View>
                  ) : (
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
                          onPress={() => handleSelectLOV(item)}
                        >
                          {lovType === 'jobsheet' ? (
                            <View style={{ flexDirection: 'row', flex: 1, alignItems: 'center' }}>
                              <ThemedText style={[styles.lovItemText, { flex: 1.5, fontWeight: 'bold' }]}>
                                {item.JobSheetNo || ''}
                              </ThemedText>
                              <ThemedText style={[styles.lovItemText, { flex: 1.2 }]}>
                                {item.SupplierCode || ''}
                              </ThemedText>
                              <ThemedText style={[styles.lovItemText, { flex: 1.3 }]}>
                                {item.PurchaseBillNo || ''}
                              </ThemedText>
                              <ThemedText style={[styles.lovItemText, { flex: 1.2, textAlign: 'right' }]}>
                                {item.TotalWeight24k !== undefined && item.TotalWeight24k !== null
                                  ? Number(item.TotalWeight24k).toFixed(3)
                                  : ''}
                              </ThemedText>
                            </View>
                          ) : (
                            <View style={{ flexDirection: 'row', flex: 1, alignItems: 'center' }}>
                              <ThemedText style={[styles.lovItemText, { flex: 1.5, fontWeight: 'bold' }]}>
                                {item.TaelPercent !== undefined && item.TaelPercent !== null
                                  ? String(item.TaelPercent)
                                  : ''}
                              </ThemedText>
                              <ThemedText style={[styles.lovItemText, { flex: 1, textAlign: 'right' }]}>
                                {item.TaelPercent1 !== undefined && item.TaelPercent1 !== null ? String(item.TaelPercent1) : ''}
                              </ThemedText>
                              <ThemedText style={[styles.lovItemText, { flex: 1, textAlign: 'right' }]}>
                                {item.TaelPercent2 !== undefined && item.TaelPercent2 !== null ? String(item.TaelPercent2) : ''}
                              </ThemedText>
                              <ThemedText style={[styles.lovItemText, { flex: 1, textAlign: 'right' }]}>
                                {item.TaelPercent3 !== undefined && item.TaelPercent3 !== null ? String(item.TaelPercent3) : ''}
                              </ThemedText>
                            </View>
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
                  )}
                </View>

                <View style={styles.modalFooter}>
                  <TouchableOpacity style={styles.modalCloseBtn} onPress={() => setIsLOVVisible(false)}>
                    <ThemedText style={styles.modalCloseBtnText}>Close(Esc)</ThemedText>
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
      {loading && (
        <View style={styles.loadingOverlay}>
          <ActivityIndicator size="large" color="#B48218" />
          <ThemedText style={{ color: '#FFFFFF', fontWeight: 'bold', marginTop: 10 }}>Loading details...</ThemedText>
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
    paddingHorizontal: 12,
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
    padding: 16,
    paddingBottom: 28,
  },
  mainWrapper: {
    width: '100%',
    maxWidth: 620,
    alignSelf: 'center',
  },
  goldCard: {
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#E8DCB8',
    borderRadius: 8,
    marginBottom: 14,
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
    paddingHorizontal: 12,
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
  cardBody: {
    padding: 12,
    gap: 8,
  },
  formRow: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  label: {
    width: 110,
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3A2E19',
  },
  labelSmall: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3A2E19',
    marginRight: 6,
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
  dateContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 4,
  },
  dateInputShort: {
    width: 40,
    height: 28,
    borderWidth: 1,
    borderColor: '#EADBCA',
    borderRadius: 6,
    textAlign: 'center',
    fontSize: 12,
  },
  dateInputLong: {
    width: 60,
    height: 28,
    borderWidth: 1,
    borderColor: '#EADBCA',
    borderRadius: 6,
    textAlign: 'center',
    fontSize: 12,
  },
  checkboxSquare: {
    width: 16,
    height: 16,
    borderWidth: 1.5,
    borderColor: '#B8903B',
    borderRadius: 3,
    backgroundColor: '#FFFFFF',
    justifyContent: 'center',
    alignItems: 'center',
  },
  checkboxSquareActive: {
    backgroundColor: '#B8903B',
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
