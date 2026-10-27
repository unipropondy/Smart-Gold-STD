import { ThemedText } from '@/components/themed-text';
import { API_BASE_URL } from '@/constants/api';
import { Ionicons } from '@expo/vector-icons';
import { Stack, useRouter } from 'expo-router';
import React, { useEffect, useState } from 'react';
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
  View
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';

const API_URL = `${API_BASE_URL}`;

export default function PayOutScreen() {
  const router = useRouter();

  // ===== STATE =====
  const [loading, setLoading] = useState(false);
  const [toast, setToast] = useState<{ message: string; type: 'success' | 'error' } | null>(null);

  // Field values
  const [payoutNo, setPayoutNo] = useState('');
  const [payoutCode, setPayoutCode] = useState('');
  const [codeDescription, setCodeDescription] = useState('');
  const [payoutDescription, setPayoutDescription] = useState('');
  const [amount, setAmount] = useState('');
  const [remarks, setRemarks] = useState('');

  // LOV State
  const [lovVisible, setLovVisible] = useState(false);
  const [lovData, setLovData] = useState<any[]>([]);
  const [filteredLovData, setFilteredLovData] = useState<any[]>([]);
  const [lovSearch, setLovSearch] = useState('');

  const showToast = (message: string, type: 'success' | 'error' = 'success') => {
    setToast({ message, type });
    setTimeout(() => {
      setToast(null);
    }, 3000);
  };

  // Fetch next payout sequence number
  const fetchNextCode = async () => {
    setLoading(true);
    try {
      const res = await fetch(`${API_URL}/payout/nextCode`);
      const json = await res.json();
      if (json.success) {
        setPayoutNo(json.data);
      }
    } catch (e) {
      showToast("Failed to fetch next payout number.", "error");
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchNextCode();
  }, []);

  // Fetch payout master data for LOV selection
  const handleOpenMasterLov = async () => {
    setLoading(true);
    try {
      const res = await fetch(`${API_URL}/payoutmaster`);
      const json = await res.json();
      if (json.success) {
        setLovData(json.data);
        setFilteredLovData(json.data);
        setLovSearch('');
        setLovVisible(true);
      } else {
        showToast("Failed to load payout master entries.", "error");
      }
    } catch (e) {
      showToast("Server connection error.", "error");
    } finally {
      setLoading(false);
    }
  };

  const handleLovSearch = (text: string) => {
    setLovSearch(text);
    if (!text) {
      setFilteredLovData(lovData);
      return;
    }
    const filtered = lovData.filter(item =>
      (item.Code && item.Code.toLowerCase().includes(text.toLowerCase())) ||
      (item.Description && item.Description.toLowerCase().includes(text.toLowerCase())) ||
      (item.Remarks && item.Remarks.toLowerCase().includes(text.toLowerCase()))
    );
    setFilteredLovData(filtered);
  };

  const handleSelectMaster = (item: any) => {
    setPayoutCode(item.Code ? item.Code.trim() : '');
    setCodeDescription(item.Description ? item.Description.trim() : '');
    setLovVisible(false);
  };

  const handleClear = () => {
    setPayoutCode('');
    setCodeDescription('');
    setPayoutDescription('');
    setAmount('');
    setRemarks('');
    fetchNextCode();
  };

  const handleSave = async () => {
    if (!payoutCode.trim()) {
      showToast("PayOut Code should not be blank.", "error");
      return;
    }
    if (!payoutDescription.trim()) {
      showToast("PayOut Description should not be blank.", "error");
      return;
    }
    const parsedAmount = parseFloat(amount);
    if (isNaN(parsedAmount) || parsedAmount <= 0) {
      showToast("Amount must be a numeric value greater than zero.", "error");
      return;
    }

    setLoading(true);
    try {
      const res = await fetch(`${API_URL}/payout/save`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          Code: payoutCode,
          sDescription: codeDescription,
          PayOutDescription: payoutDescription,
          Amount: amount,
          Remarks: remarks,
          userId: "sys"
        })
      });
      const json = await res.json();
      if (json.success) {
        showToast("PayOut detail saved successfully.", "success");
        handleClear();
      } else {
        showToast(json.message || "Failed to save payout detail.", "error");
      }
    } catch (e) {
      showToast("Server connection failed.", "error");
    } finally {
      setLoading(false);
    }
  };

  const handlePrint = () => {
    if (!payoutNo) {
      showToast("No active payout number to print.", "error");
      return;
    }
    showToast(`Sending Payout Bill ${payoutNo} to printer...`, "success");
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
          <ThemedText style={styles.headerTitle}>Pay Out</ThemedText>
          {loading && <ActivityIndicator size="small" color="#B48218" style={{ marginLeft: 6 }} />}
        </View>

        <View style={styles.headerRight}>
          {/* Print */}
          <TouchableOpacity
            style={[styles.actionBtn, styles.clearBtn]}
            onPress={handlePrint}
            activeOpacity={0.8}
            disabled={loading}
          >
            <Ionicons name="print-outline" size={14} color="#5C4A28" />
            <ThemedText style={styles.clearBtnText}>Print</ThemedText>
          </TouchableOpacity>

          {/* New / Clear */}
          <TouchableOpacity
            style={[styles.actionBtn, styles.clearBtn]}
            onPress={handleClear}
            activeOpacity={0.8}
            disabled={loading}
          >
            <Ionicons name="add-circle-outline" size={14} color="#5C4A28" />
            <ThemedText style={styles.clearBtnText}>New</ThemedText>
          </TouchableOpacity>

          {/* Save */}
          <TouchableOpacity
            style={[styles.actionBtn, styles.saveBtn]}
            onPress={handleSave}
            activeOpacity={0.8}
            disabled={loading}
          >
            {loading ? (
              <ActivityIndicator size="small" color="#9E6D08" />
            ) : (
              <>
                <Ionicons name="checkmark-done-outline" size={14} color="#9E6D08" />
                <ThemedText style={styles.saveBtnText}>Save</ThemedText>
              </>
            )}
          </TouchableOpacity>
        </View>
      </View>

      <KeyboardAvoidingView behavior={Platform.OS === 'ios' ? 'padding' : undefined} style={{ flex: 1 }}>
        <ScrollView contentContainerStyle={styles.contentContainer}>
          <View style={styles.formCenterWrapper}>
            {/* LUXURY CARD */}
            <View style={styles.goldCard}>
              <View style={styles.cardHeaderBanner}>
                <View style={styles.headerTitleContainer}>
                  <View style={styles.headerIconBadgeSmall}>
                    <Ionicons name="cash-outline" size={13} color="#B48218" />
                  </View>
                  <ThemedText style={styles.cardHeaderTitleSmall}>Payout Voucher Entry</ThemedText>
                </View>

                {/* Payout Entry No Badge */}
                <View style={styles.payoutBadge}>
                  <ThemedText style={styles.payoutBadgeLabel}>Voucher #</ThemedText>
                  <ThemedText style={styles.payoutBadgeValue}>{payoutNo || '...'}</ThemedText>
                </View>
              </View>

              <View style={styles.cardBody}>
                {/* Code with LOV */}
                <View style={styles.formGridRow}>
                  <ThemedText style={styles.formLabel}>Code *</ThemedText>
                  <View style={styles.inputWithLookup}>
                    <TextInput
                      style={[styles.formInput, styles.inputAttached]}
                      value={payoutCode}
                      onChangeText={setPayoutCode}
                      placeholder="Select Code..."
                      placeholderTextColor="#9E8D77"
                    />
                    <TouchableOpacity
                      style={styles.attachedLookupBtn}
                      onPress={handleOpenMasterLov}
                      activeOpacity={0.7}
                    >
                      <ThemedText style={styles.attachedLookupText}>...</ThemedText>
                    </TouchableOpacity>
                  </View>
                </View>

                {/* Description */}
                <View style={styles.formGridRow}>
                  <ThemedText style={styles.formLabel}>Description</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput]}
                    value={codeDescription}
                    editable={false}
                    placeholder="Auto-populated from code"
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* Specific Payout Description */}
                <View style={[styles.formGridRow, { alignItems: 'flex-start', paddingTop: 8 }]}>
                  <ThemedText style={[styles.formLabel, { marginTop: 4 }]}>Payout Desc *</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.multilineInput]}
                    value={payoutDescription}
                    onChangeText={setPayoutDescription}
                    multiline={true}
                    numberOfLines={3}
                    placeholder="Enter specific payout details or expense reason..."
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* Amount */}
                <View style={styles.formGridRow}>
                  <ThemedText style={styles.formLabel}>Amount *</ThemedText>
                  <View style={styles.amountInputContainer}>
                    <ThemedText style={styles.currencyPrefix}>$</ThemedText>
                    <TextInput
                      style={[styles.formInput, styles.amountInput]}
                      value={amount}
                      onChangeText={setAmount}
                      keyboardType="numeric"
                      placeholder="0.00"
                      placeholderTextColor="#9E8D77"
                    />
                  </View>
                </View>

                {/* Remarks */}
                <View style={[styles.formGridRow, { borderBottomWidth: 0 }]}>
                  <ThemedText style={styles.formLabel}>Remarks</ThemedText>
                  <TextInput
                    style={styles.formInput}
                    value={remarks}
                    onChangeText={setRemarks}
                    placeholder="Optional notes..."
                    placeholderTextColor="#9E8D77"
                  />
                </View>
              </View>
            </View>
          </View>
        </ScrollView>
      </KeyboardAvoidingView>

      {/* LOV Selection Modal */}
      <Modal visible={lovVisible} transparent={true} animationType="fade" onRequestClose={() => setLovVisible(false)}>
        <TouchableOpacity style={styles.modalOverlay} activeOpacity={1} onPress={() => setLovVisible(false)}>
          <TouchableWithoutFeedback>
            <View style={styles.lovModalContainer}>
              <View style={styles.modalHeader}>
                <View style={{ flexDirection: 'row', alignItems: 'center', gap: 6 }}>
                  <View style={styles.headerIconBadgeSmall}>
                    <Ionicons name="list" size={14} color="#B48218" />
                  </View>
                  <ThemedText style={styles.modalTitle}>Select Payout Code</ThemedText>
                </View>
                <TouchableOpacity onPress={() => setLovVisible(false)} style={styles.modalCloseBtn}>
                  <Ionicons name="close" size={20} color="#3B2D17" />
                </TouchableOpacity>
              </View>

              <View style={{ padding: 12 }}>
                <View style={styles.modalSearchBox}>
                  <Ionicons name="search-outline" size={15} color="#8F816E" style={{ marginRight: 6 }} />
                  <TextInput
                    style={styles.modalSearchInput}
                    placeholder="Search by code, description or remarks..."
                    placeholderTextColor="#8F816E"
                    value={lovSearch}
                    onChangeText={handleLovSearch}
                    autoFocus
                  />
                </View>

                <View style={styles.lovHeaderRow}>
                  <ThemedText style={[styles.lovHeaderText, { flex: 1.2 }]}>Code</ThemedText>
                  <ThemedText style={[styles.lovHeaderText, { flex: 2.5 }]}>Description</ThemedText>
                  <ThemedText style={[styles.lovHeaderText, { flex: 2.3 }]}>Remarks</ThemedText>
                </View>

                {loading ? (
                  <View style={{ padding: 30, alignItems: 'center' }}>
                    <ActivityIndicator size="large" color="#B48218" />
                  </View>
                ) : (
                  <FlatList
                    data={filteredLovData}
                    keyExtractor={(_, index) => index.toString()}
                    style={{ maxHeight: 320 }}
                    renderItem={({ item, index }) => (
                      <TouchableOpacity
                        style={[
                          styles.lovItemRow,
                          index % 2 === 1 && { backgroundColor: '#FCFAF6' }
                        ]}
                        activeOpacity={0.7}
                        onPress={() => handleSelectMaster(item)}
                      >
                        <ThemedText style={[styles.lovItemText, styles.codeCell, { flex: 1.2 }]}>
                          {String(item.Code || '')}
                        </ThemedText>
                        <ThemedText style={[styles.lovItemText, { flex: 2.5 }]}>
                          {String(item.Description || '')}
                        </ThemedText>
                        <ThemedText style={[styles.lovItemText, { flex: 2.3, color: '#7D6F5B' }]}>
                          {String(item.Remarks || '')}
                        </ThemedText>
                      </TouchableOpacity>
                    )}
                  />
                )}
              </View>
            </View>
          </TouchableWithoutFeedback>
        </TouchableOpacity>
      </Modal>

      {/* TOAST ALERT */}
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
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#FBF9F5',
  },
  contentContainer: {
    padding: 12,
    paddingVertical: 16,
  },
  formCenterWrapper: {
    maxWidth: 680,
    width: '100%',
    alignSelf: 'center',
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
    gap: 8,
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
    color: '#3B2D17',
    letterSpacing: 0.5,
  },
  headerRight: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 6,
  },

  // ===== ACTION BUTTONS =====
  actionBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    paddingHorizontal: 12,
    paddingVertical: 5,
    borderRadius: 6,
    borderWidth: 1,
    gap: 4,
    minHeight: 28,
  },
  saveBtn: {
    backgroundColor: '#F9F2E2',
    borderColor: '#B8903B',
  },
  saveBtnText: {
    color: '#9E6D08',
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
    gap: 6,
  },
  headerIconBadgeSmall: {
    width: 22,
    height: 22,
    borderRadius: 5,
    backgroundColor: '#FFF8EA',
    borderWidth: 1,
    borderColor: '#E2D2AF',
    alignItems: 'center',
    justifyContent: 'center',
  },
  cardHeaderTitleSmall: {
    fontSize: 13,
    fontWeight: 'bold',
    color: '#3B2D17',
    letterSpacing: 0.3,
  },
  payoutBadge: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFF8EA',
    borderWidth: 1,
    borderColor: '#D8C59A',
    borderRadius: 12,
    paddingHorizontal: 10,
    paddingVertical: 2,
    gap: 6,
  },
  payoutBadgeLabel: {
    fontSize: 10,
    fontWeight: '600',
    color: '#7D6F5B',
    textTransform: 'uppercase',
  },
  payoutBadgeValue: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#8D6409',
  },
  cardBody: {
    padding: 12,
  },

  // ===== FORM ROWS & INPUTS =====
  formGridRow: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingVertical: 8,
    borderBottomWidth: 1,
    borderBottomColor: '#F7F2E9',
    gap: 12,
  },
  formLabel: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#4A3B2C',
    width: 110,
    flexShrink: 0,
  },
  inputWithLookup: {
    flexDirection: 'row',
    flex: 1,
    alignItems: 'center',
    minWidth: 0,
  },
  formInput: {
    height: 32,
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderRadius: 6,
    paddingHorizontal: 10,
    paddingVertical: 0,
    fontSize: 12,
    color: '#2B2118',
    flex: 1,
  },
  readonlyInput: {
    backgroundColor: '#F8F5EE',
    borderColor: '#EADBCA',
    color: '#554736',
  },
  inputAttached: {
    borderTopRightRadius: 0,
    borderBottomRightRadius: 0,
  },
  attachedLookupBtn: {
    width: 32,
    height: 32,
    flexShrink: 0,
    backgroundColor: '#FAF5EA',
    borderWidth: 1,
    borderColor: '#D4C3A3',
    borderRadius: 6,
    marginLeft: 4,
    alignItems: 'center',
    justifyContent: 'center',
  },
  attachedLookupText: {
    fontSize: 14,
    fontWeight: 'bold',
    color: '#6E5528',
    letterSpacing: 1,
    marginTop: -2,
  },
  multilineInput: {
    height: 72,
    paddingTop: 8,
    paddingBottom: 8,
    textAlignVertical: 'top',
  },
  amountInputContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    flex: 1,
    maxWidth: 200,
  },
  currencyPrefix: {
    fontSize: 14,
    fontWeight: 'bold',
    color: '#8D6409',
    marginRight: 6,
  },
  amountInput: {
    fontWeight: 'bold',
    color: '#9E6D08',
    fontSize: 14,
  },

  // ===== MODALS & LOV =====
  modalOverlay: {
    flex: 1,
    backgroundColor: 'rgba(30, 20, 10, 0.45)',
    justifyContent: 'center',
    alignItems: 'center',
    padding: 16,
  },
  lovModalContainer: {
    width: '100%',
    maxWidth: 640,
    backgroundColor: '#FFFFFF',
    borderRadius: 10,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    overflow: 'hidden',
    shadowColor: '#000',
    shadowOpacity: 0.15,
    shadowRadius: 10,
    elevation: 6,
  },
  modalHeader: {
    backgroundColor: '#F5E7CA',
    paddingHorizontal: 14,
    paddingVertical: 10,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
  },
  modalTitle: {
    fontSize: 13,
    fontWeight: 'bold',
    color: '#3B2D17',
  },
  modalCloseBtn: {
    padding: 4,
  },
  modalSearchBox: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderRadius: 6,
    paddingHorizontal: 10,
    height: 32,
    marginBottom: 8,
  },
  modalSearchInput: {
    flex: 1,
    fontSize: 12,
    color: '#2B2118',
    paddingVertical: 0,
  },
  lovHeaderRow: {
    flexDirection: 'row',
    backgroundColor: '#F5E7CA',
    paddingVertical: 6,
    paddingHorizontal: 8,
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
  },
  lovHeaderText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3B2D17',
  },
  lovItemRow: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingVertical: 8,
    paddingHorizontal: 8,
    borderBottomWidth: 1,
    borderBottomColor: '#F3EBDD',
  },
  lovItemText: {
    fontSize: 12,
    color: '#3B2D17',
  },
  codeCell: {
    fontWeight: 'bold',
    color: '#8D6409',
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
