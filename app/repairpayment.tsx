import { ThemedText } from '@/components/themed-text';
import { API_BASE_URL } from '@/constants/api';
import { useThemeColors } from '@/hooks/useThemeColors';
import { Ionicons } from '@expo/vector-icons';
import { Stack, useRouter } from 'expo-router';
import React, { useEffect, useRef, useState } from 'react';
import {
  ActivityIndicator,
  Alert,
  FlatList,
  KeyboardAvoidingView,
  Modal,
  Platform,
  ScrollView,
  StyleSheet,
  TextInput,
  TouchableOpacity,
  View,
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';

const API_URL = `${API_BASE_URL}/repairpayment`;

export default function RepairPaymentScreen() {
  const themeColors = useThemeColors();
  const router = useRouter();

  const [loading, setLoading] = useState(false);
  const [saving, setSaving] = useState(false);
  const [toast, setToast] = useState<{ message: string; type: 'success' | 'error' } | null>(null);

  // Form State
  const [repairNo, setRepairNo] = useState('');
  const [customerCode, setCustomerCode] = useState('');
  const [customerName, setCustomerName] = useState('');
  const [address, setAddress] = useState('');
  const [customerType, setCustomerType] = useState('');
  const [remarks, setRemarks] = useState('');

  // Payment State
  const [amountPaid, setAmountPaid] = useState('0.00');

  // Footer Totals
  const [estimatedCost, setEstimatedCost] = useState('0.00');
  const [advancePaid, setAdvancePaid] = useState('0.00');
  const [balanceAmount, setBalanceAmount] = useState('0.00');

  // Trade-in
  const [tradeInOption, setTradeInOption] = useState<'Gold' | 'Cash' | 'Both'>('Gold');

  // History
  const [historyList, setHistoryList] = useState<any[]>([]);

  // LOV State
  const [isRepairModalVisible, setIsRepairModalVisible] = useState(false);
  const [repairList, setRepairList] = useState<any[]>([]);
  const [repairSearchQuery, setRepairSearchQuery] = useState('');

  const showToast = (message: string, type: 'success' | 'error' = 'success') => {
    setToast({ message, type });
    setTimeout(() => setToast(null), 3000);
  };

  const showAlert = (title: string, message: string) => {
    if (Platform.OS === 'web') {
      window.alert(`${title}: ${message}`);
    } else {
      Alert.alert(title, message);
    }
  };

  const fetchBalance = async (repNo: string) => {
    if (!repNo) return;
    try {
      setLoading(true);
      const res = await fetch(`${API_URL}/balance/${encodeURIComponent(repNo.trim())}`);
      const json = await res.json();
      if (json.success && json.data) {
        const { repair, balance, history } = json.data;
        setEstimatedCost(balance.estimatedCost ? balance.estimatedCost.toFixed(2) : '0.00');
        setAdvancePaid(balance.advancePaid ? balance.advancePaid.toFixed(2) : '0.00');
        setBalanceAmount(balance.balanceAmount > 0 ? balance.balanceAmount.toFixed(2) : '0.00');
        setHistoryList(history || []);

        if (repair) {
          setCustomerCode(repair.CustomerCode ? repair.CustomerCode.trim() : '');
          setCustomerName(repair.CustomerName ? repair.CustomerName.trim() : '');
          setAddress(`${repair.Address1 || ''} ${repair.Address2 || ''} ${repair.Address3 || ''}`.trim());
          setCustomerType(repair.CustomerType ? repair.CustomerType.trim() : '');
        }
        showToast('Repair balance & history loaded', 'success');
      } else {
        showToast(json.message || 'Repair not found', 'error');
        handleClearCustomerData();
      }
    } catch (err) {
      console.error(err);
      showToast('Error fetching repair balance', 'error');
    } finally {
      setLoading(false);
    }
  };

  const handleClearCustomerData = () => {
    setCustomerCode('');
    setCustomerName('');
    setAddress('');
    setCustomerType('');
    setEstimatedCost('0.00');
    setAdvancePaid('0.00');
    setBalanceAmount('0.00');
    setHistoryList([]);
  };

  const handleOpenRepairLookup = async () => {
    setLoading(true);
    try {
      const res = await fetch(`${API_URL}/repairs`);
      const json = await res.json();
      if (json.success) {
        setRepairList(json.data || []);
        setIsRepairModalVisible(true);
      } else {
        showToast('Failed to load repairs', 'error');
      }
    } catch (err) {
      showToast('Error querying repairs', 'error');
    } finally {
      setLoading(false);
    }
  };

  const handleSelectRepair = (rep: any) => {
    setRepairNo(rep.RepairNo);
    setIsRepairModalVisible(false);
    fetchBalance(rep.RepairNo);
  };

  const handleClear = () => {
    setRepairNo('');
    setRemarks('');
    setAmountPaid('0.00');
    handleClearCustomerData();
  };

  const handleCancelPayment = async (paymentNo: string) => {
    if (!repairNo) return;
    Alert.alert('Confirm Cancel', `Are you sure you want to cancel payment ${paymentNo}?`, [
      { text: 'No', style: 'cancel' },
      {
        text: 'Yes',
        onPress: async () => {
          setLoading(true);
          try {
            const res = await fetch(`${API_URL}/pay/${encodeURIComponent(repairNo)}/${encodeURIComponent(paymentNo)}`, {
              method: 'DELETE',
            });
            const json = await res.json();
            if (json.success) {
              showToast('Payment Cancelled Successfully', 'success');
              fetchBalance(repairNo);
            } else {
              showToast(json.message || 'Failed to cancel', 'error');
            }
          } catch (err) {
            showToast('Network error while cancelling payment', 'error');
          } finally {
            setLoading(false);
          }
        },
      },
    ]);
  };

  const latestState = useRef({ repairNo, amountPaid });
  useEffect(() => {
    latestState.current = { repairNo, amountPaid };
  }, [repairNo, amountPaid]);

  const handleSave = async () => {
    const currentState = latestState.current;
    if (!currentState.repairNo.trim()) {
      showToast('Please select or enter a Repair No.', 'error');
      return;
    }
    const tot = parseFloat(currentState.amountPaid) || 0;
    if (tot <= 0) {
      showToast('Pay amount must be greater than zero.', 'error');
      return;
    }

    setSaving(true);
    try {
      // 1. Fetch next payment no
      const noRes = await fetch(`${API_URL}/next-no`);
      const noJson = await noRes.json();
      const repairPaymentNo = noJson.success ? noJson.paymentNo : `RP-${Date.now()}`;

      // 2. Save payment
      const payload = {
        repairNo: currentState.repairNo.trim(),
        repairPaymentNo,
        amountPaid: currentState.amountPaid,
        remarks: remarks.trim() || 'REPAIR PAYMENT',
        paymodeDetails: [{ payMode: 'CASH', amount: currentState.amountPaid }],
      };

      const res = await fetch(`${API_URL}/pay`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload),
      });
      const json = await res.json();
      if (json.success) {
        showToast('Repair Payment Saved Successfully', 'success');
        setAmountPaid('0.00');
        setRemarks('');
        fetchBalance(currentState.repairNo);
      } else {
        showToast(json.message || 'Failed to save payment', 'error');
      }
    } catch (err) {
      showToast('Network error while saving payment', 'error');
    } finally {
      setSaving(false);
    }
  };

  return (
    <SafeAreaView style={styles.safeArea} edges={['top']}>
      <Stack.Screen options={{ headerShown: false }} />

      {/* LUXURY ROYAL GOLD HEADER BAR */}
      <View style={styles.headerBar}>
        <View style={styles.headerLeft}>
          <TouchableOpacity style={styles.backBtn} onPress={() => router.back()} activeOpacity={0.7}>
            <Ionicons name="arrow-back" size={20} color="#3B2D17" />
          </TouchableOpacity>
          <View>
            <ThemedText style={styles.headerTitle}>Jewellery Repair Payment</ThemedText>
            <ThemedText style={styles.headerSubtitle}>Repair Advance & Settlement Processing</ThemedText>
          </View>
          {(loading || saving) && <ActivityIndicator size="small" color="#B48218" style={{ marginLeft: 8 }} />}
        </View>

        <View style={styles.headerRight}>
          <TouchableOpacity
            style={[styles.actionBtn, styles.clearBtn]}
            onPress={handleClear}
            activeOpacity={0.8}
            disabled={saving || loading}
          >
            <Ionicons name="refresh-outline" size={14} color="#5C4A28" />
            <ThemedText style={styles.clearBtnText}>Clear</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity
            style={[styles.actionBtn, styles.reprintBtn]}
            onPress={() => showToast('Printing repair receipt...', 'success')}
            activeOpacity={0.8}
          >
            <Ionicons name="print-outline" size={14} color="#5C4A28" />
            <ThemedText style={styles.clearBtnText}>Print</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity
            style={[styles.actionBtn, styles.saveBtn]}
            onPress={handleSave}
            activeOpacity={0.8}
            disabled={saving || loading}
          >
            {saving ? (
              <ActivityIndicator size="small" color="#9E6D08" />
            ) : (
              <>
                <Ionicons name="checkmark-done-outline" size={15} color="#9E6D08" />
                <ThemedText style={styles.saveBtnText}>Save</ThemedText>
              </>
            )}
          </TouchableOpacity>
        </View>
      </View>

      {/* TOAST ALERTS */}
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

      <KeyboardAvoidingView behavior={Platform.OS === 'ios' ? 'padding' : undefined} style={{ flex: 1 }}>
        <ScrollView contentContainerStyle={styles.scrollContainer} keyboardShouldPersistTaps="handled">
          {/* CARD 1: REPAIR IDENTIFICATION & CUSTOMER DETAILS */}
          <View style={styles.goldCard}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="construct-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitleSmall}>Repair Identification</ThemedText>
              </View>

              {repairNo ? (
                <View style={styles.repairBadge}>
                  <ThemedText style={styles.repairBadgeLabel}>Repair:</ThemedText>
                  <ThemedText style={styles.repairBadgeValue}>{repairNo}</ThemedText>
                </View>
              ) : null}
            </View>

            <View style={styles.cardBody}>
              {/* Row 1: Repair No, Customer Code, Customer Type, Customer Name */}
              <View style={styles.fourColumnGrid}>
                {/* Repair No */}
                <View style={[styles.inlineFieldRow, { flex: 1.2, minWidth: 260 }]}>
                  <ThemedText style={styles.inlineFieldLabel}>Repair No *</ThemedText>
                  <View style={styles.inputWithLookup}>
                    <TextInput
                      style={[styles.formInput, styles.inputAttached, { flex: 1 }]}
                      value={repairNo}
                      onChangeText={setRepairNo}
                      onBlur={() => repairNo && fetchBalance(repairNo)}
                      placeholder="Search/Enter Repair No..."
                      placeholderTextColor="#9E8D77"
                    />
                    <TouchableOpacity
                      style={styles.attachedLookupBtn}
                      onPress={handleOpenRepairLookup}
                      activeOpacity={0.7}
                    >
                      <Ionicons name="search" size={13} color="#7D5E19" />
                    </TouchableOpacity>
                  </View>
                </View>

                {/* Customer Code */}
                <View style={styles.inlineFieldRow}>
                  <ThemedText style={styles.inlineFieldLabel}>Cust Code</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1 }]}
                    value={customerCode}
                    editable={false}
                    placeholder="Auto-populated"
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* Customer Type */}
                <View style={styles.inlineFieldRow}>
                  <ThemedText style={styles.inlineFieldLabel}>Cust Type</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1 }]}
                    value={customerType}
                    editable={false}
                    placeholder="Auto-populated"
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* Customer Name */}
                <View style={styles.inlineFieldRow}>
                  <ThemedText style={styles.inlineFieldLabel}>Cust Name</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1 }]}
                    value={customerName}
                    editable={false}
                    placeholder="Auto-populated"
                    placeholderTextColor="#9E8D77"
                  />
                </View>
              </View>

              {/* Row 2: Address, Trade-In */}
              <View style={[styles.threeColumnGrid, { marginTop: 6 }]}>
                {/* Address */}
                <View style={[styles.inlineFieldRow, { flex: 2, minWidth: 280 }]}>
                  <ThemedText style={styles.inlineFieldLabel}>Address</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1 }]}
                    value={address}
                    editable={false}
                    placeholder="Auto-populated address"
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* Trade-In Options */}
                <View style={[styles.inlineFieldRow, { flex: 1.5, minWidth: 260 }]}>
                  <ThemedText style={styles.inlineFieldLabel}>Trade-In</ThemedText>
                  <View style={styles.tradeInOptionsGroup}>
                    {['Gold', 'Cash', 'Both'].map((opt) => (
                      <TouchableOpacity
                        key={opt}
                        style={[styles.pillOption, tradeInOption === opt && styles.pillOptionSelected]}
                        onPress={() => setTradeInOption(opt as any)}
                        activeOpacity={0.7}
                      >
                        <Ionicons
                          name={tradeInOption === opt ? 'radio-button-on' : 'radio-button-off'}
                          size={13}
                          color={tradeInOption === opt ? '#9E6D08' : '#7A6855'}
                        />
                        <ThemedText
                          style={[styles.pillOptionText, tradeInOption === opt && styles.pillOptionTextSelected]}
                        >
                          {opt}
                        </ThemedText>
                      </TouchableOpacity>
                    ))}

                    {tradeInOption !== 'Cash' && (
                      <TouchableOpacity
                        style={styles.tradeInGoBtn}
                        onPress={() => router.push('/tradeinsales')}
                        activeOpacity={0.8}
                      >
                        <ThemedText style={styles.tradeInGoBtnText}>Go</ThemedText>
                        <Ionicons name="arrow-forward" size={11} color="#9E6D08" />
                      </TouchableOpacity>
                    )}
                  </View>
                </View>
              </View>

              {/* Row 3: Pay Amount, Remarks */}
              <View style={[styles.twoColumnGrid, { marginTop: 6 }]}>
                {/* Pay Amount */}
                <View style={[styles.inlineFieldRow, { flex: 1, minWidth: 260 }]}>
                  <ThemedText style={styles.inlineFieldLabel}>Pay Amount ($) *</ThemedText>
                  <TextInput
                    style={[
                      styles.formInput,
                      styles.highlightTotal,
                      { flex: 1, textAlign: 'right' },
                    ]}
                    value={amountPaid}
                    onChangeText={setAmountPaid}
                    keyboardType="numeric"
                    placeholder="0.00"
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* Remarks */}
                <View style={[styles.inlineFieldRow, { flex: 2, minWidth: 280 }]}>
                  <ThemedText style={styles.inlineFieldLabel}>Remarks</ThemedText>
                  <TextInput
                    style={[styles.formInput, { flex: 1 }]}
                    value={remarks}
                    onChangeText={setRemarks}
                    placeholder="Repair payment notes..."
                    placeholderTextColor="#9E8D77"
                  />
                </View>
              </View>
            </View>
          </View>

          {/* CARD 2: REPAIR PAYMENT HISTORY TABLE (100% FULL WIDTH) */}
          <View style={[styles.goldCard, { marginTop: 10 }]}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="time-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitleSmall}>Repair Payment History</ThemedText>
                <View style={styles.counterBadge}>
                  <ThemedText style={styles.counterBadgeText}>{historyList.length} records</ThemedText>
                </View>
              </View>
            </View>

            {/* Table Header (100% Full Width) */}
            <View style={styles.tableHeadRow}>
              <ThemedText style={[styles.tableHeadCell, { flex: 1.5 }]}>Repair No</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 1.8 }]}>Payment No</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 2.2 }]}>Remarks</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 1.4, textAlign: 'right' }]}>
                Amt Paid ($)
              </ThemedText>
              <ThemedText style={[styles.tableHeadCell, { width: 70, textAlign: 'center' }]}>Action</ThemedText>
            </View>

            {/* Table Body */}
            {historyList.length === 0 ? (
              <View style={styles.emptyTableContainer}>
                <Ionicons name="receipt-outline" size={28} color="#D4C3A3" />
                <ThemedText style={styles.emptyTableText}>
                  No payment history recorded for this repair docket.
                </ThemedText>
              </View>
            ) : (
              historyList.map((item, index) => (
                <View key={index} style={styles.tableRow}>
                  <ThemedText style={[styles.tableCell, styles.codeCell, { flex: 1.5 }]}>
                    {item.RepairNo}
                  </ThemedText>
                  <ThemedText style={[styles.tableCell, styles.codeCell, { flex: 1.8 }]}>
                    {item.RepairPaymentNo}
                  </ThemedText>
                  <ThemedText style={[styles.tableCell, { flex: 2.2 }]} numberOfLines={1}>
                    {item.Remarks || '-'}
                  </ThemedText>
                  <ThemedText style={[styles.tableCell, styles.amountCell, { flex: 1.4, textAlign: 'right' }]}>
                    {parseFloat(item.AmountPaid || 0).toFixed(2)}
                  </ThemedText>
                  <View style={{ width: 70, alignItems: 'center' }}>
                    <TouchableOpacity
                      style={styles.cancelRowBtn}
                      onPress={() => handleCancelPayment(item.RepairPaymentNo)}
                      activeOpacity={0.7}
                    >
                      <ThemedText style={styles.cancelRowBtnText}>Cancel</ThemedText>
                    </TouchableOpacity>
                  </View>
                </View>
              ))
            )}
          </View>

          {/* CARD 3: REPAIR FINANCIAL SUMMARY */}
          <View style={[styles.goldCard, { marginTop: 10 }]}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="calculator-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitleSmall}>Repair Financial Summary</ThemedText>
              </View>
            </View>

            <View style={styles.cardBody}>
              <View style={styles.threeColumnGrid}>
                {/* Estimated Cost */}
                <View style={styles.inlineFieldRow}>
                  <ThemedText style={styles.inlineFieldLabel}>Estimated Cost</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1, textAlign: 'right' }]}
                    value={estimatedCost}
                    editable={false}
                  />
                </View>

                {/* Advance Paid */}
                <View style={styles.inlineFieldRow}>
                  <ThemedText style={styles.inlineFieldLabel}>Advance Paid</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1, textAlign: 'right' }]}
                    value={advancePaid}
                    editable={false}
                  />
                </View>

                {/* Balance Amount */}
                <View style={styles.inlineFieldRow}>
                  <ThemedText style={styles.inlineFieldLabel}>Balance Amount</ThemedText>
                  <TextInput
                    style={[
                      styles.formInput,
                      styles.readonlyInput,
                      styles.highlightTotal,
                      { flex: 1, textAlign: 'right' },
                    ]}
                    value={balanceAmount}
                    editable={false}
                  />
                </View>
              </View>
            </View>
          </View>
        </ScrollView>
      </KeyboardAvoidingView>

      {/* REPAIR LOOKUP MODAL */}
      <Modal visible={isRepairModalVisible} transparent animationType="fade">
        <View style={styles.modalOverlay}>
          <View style={styles.lovModalCard}>
            <View style={styles.modalHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="search" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.modalHeaderTitle}>Search Repair Order</ThemedText>
              </View>
              <TouchableOpacity
                onPress={() => setIsRepairModalVisible(false)}
                activeOpacity={0.7}
                style={styles.closeModalBtn}
              >
                <Ionicons name="close" size={18} color="#3B2D17" />
              </TouchableOpacity>
            </View>

            <View style={styles.modalSearchBox}>
              <Ionicons name="search-outline" size={16} color="#8D6409" style={{ marginRight: 6 }} />
              <TextInput
                style={styles.modalSearchInput}
                value={repairSearchQuery}
                onChangeText={setRepairSearchQuery}
                placeholder="Search by Repair No or Customer Code..."
                placeholderTextColor="#9E8D77"
                autoFocus
              />
              {repairSearchQuery.length > 0 && (
                <TouchableOpacity onPress={() => setRepairSearchQuery('')}>
                  <Ionicons name="close-circle" size={16} color="#9E8D77" />
                </TouchableOpacity>
              )}
            </View>

            <View style={styles.lovHeadRow}>
              <ThemedText style={[styles.lovHeadText, { flex: 1.6 }]}>Repair No</ThemedText>
              <ThemedText style={[styles.lovHeadText, { flex: 1.4 }]}>Customer Code</ThemedText>
              <ThemedText style={[styles.lovHeadText, { flex: 1.2 }]}>Repair Date</ThemedText>
              <ThemedText style={[styles.lovHeadText, { flex: 1.4, textAlign: 'right' }]}>Est Cost</ThemedText>
            </View>

            <FlatList
              data={repairList.filter(
                (o) =>
                  (o.RepairNo || '').toLowerCase().includes(repairSearchQuery.toLowerCase()) ||
                  (o.CustomerCode || '').toLowerCase().includes(repairSearchQuery.toLowerCase())
              )}
              keyExtractor={(item, index) => item.RepairNo || index.toString()}
              initialNumToRender={50}
              maxToRenderPerBatch={50}
              ListEmptyComponent={
                <View style={styles.emptyTableContainer}>
                  <Ionicons name="search-outline" size={24} color="#D4C3A3" />
                  <ThemedText style={styles.emptyTableText}>No matching repair orders found.</ThemedText>
                </View>
              }
              renderItem={({ item }) => (
                <TouchableOpacity
                  style={styles.lovItemRow}
                  onPress={() => handleSelectRepair(item)}
                  activeOpacity={0.7}
                >
                  <ThemedText style={[styles.lovItemText, styles.codeCell, { flex: 1.6 }]}>
                    {item.RepairNo}
                  </ThemedText>
                  <ThemedText style={[styles.lovItemText, { flex: 1.4 }]}>{item.CustomerCode}</ThemedText>
                  <ThemedText style={[styles.lovItemText, { flex: 1.2 }]}>
                    {item.RepairDate ? new Date(item.RepairDate).toLocaleDateString() : ''}
                  </ThemedText>
                  <ThemedText style={[styles.lovItemText, styles.amountCell, { flex: 1.4, textAlign: 'right' }]}>
                    {parseFloat(item.TotalCostEstimated || 0).toFixed(2)}
                  </ThemedText>
                </TouchableOpacity>
              )}
            />
          </View>
        </View>
      </Modal>
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  safeArea: {
    flex: 1,
    backgroundColor: '#FBF9F5',
  },
  scrollContainer: {
    paddingHorizontal: 12,
    paddingTop: 8,
    paddingBottom: 24,
  },

  // ===== ROYAL GOLD HEADER BAR =====
  headerBar: {
    height: 48,
    backgroundColor: '#FFFFFF',
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingHorizontal: 14,
    borderBottomWidth: 1,
    borderBottomColor: '#E8DCB8',
    elevation: 2,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.05,
    shadowRadius: 2,
  },
  headerLeft: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  backBtn: {
    padding: 6,
    marginRight: 8,
    borderRadius: 6,
    backgroundColor: '#F9F6F0',
    borderWidth: 1,
    borderColor: '#E8DCB8',
  },
  headerTitle: {
    fontSize: 15,
    fontWeight: 'bold',
    color: '#3B2D17',
    letterSpacing: 0.3,
  },
  headerSubtitle: {
    fontSize: 10,
    color: '#8D6409',
    fontWeight: '500',
    marginTop: -1,
  },
  headerRight: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 8,
  },
  actionBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 12,
    paddingVertical: 5,
    borderRadius: 6,
    borderWidth: 1,
  },
  clearBtn: {
    backgroundColor: '#FFFFFF',
    borderColor: '#DFD5C3',
  },
  clearBtnText: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#5C4A28',
    marginLeft: 4,
  },
  reprintBtn: {
    backgroundColor: '#FFFFFF',
    borderColor: '#DFD5C3',
  },
  saveBtn: {
    backgroundColor: '#F9F2E2',
    borderColor: '#B8903B',
  },
  saveBtnText: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#9E6D08',
    marginLeft: 4,
  },

  // ===== CARDS & BANNERS =====
  goldCard: {
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    overflow: 'hidden',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.04,
    shadowRadius: 3,
    elevation: 2,
  },
  cardHeaderBanner: {
    backgroundColor: '#F5E7CA',
    paddingVertical: 6,
    paddingHorizontal: 10,
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
  },
  headerTitleContainer: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  headerIconBadgeSmall: {
    width: 20,
    height: 20,
    borderRadius: 4,
    backgroundColor: '#FFFFFF',
    justifyContent: 'center',
    alignItems: 'center',
    marginRight: 6,
    borderWidth: 1,
    borderColor: '#E2D3B3',
  },
  cardHeaderTitleSmall: {
    fontSize: 13,
    fontWeight: 'bold',
    color: '#3B2D17',
    letterSpacing: 0.2,
  },
  cardBody: {
    padding: 10,
  },
  repairBadge: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFF4DA',
    paddingHorizontal: 8,
    paddingVertical: 2,
    borderRadius: 4,
    borderWidth: 1,
    borderColor: '#B8903B',
  },
  repairBadgeLabel: {
    fontSize: 10,
    color: '#7D5E19',
    marginRight: 4,
  },
  repairBadgeValue: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#8A5D00',
  },

  // ===== GRIDS & INLINE ROWS =====
  fourColumnGrid: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    gap: 8,
  },
  twoColumnGrid: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    gap: 8,
  },
  threeColumnGrid: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    gap: 8,
  },
  inlineFieldRow: {
    flex: 1,
    minWidth: 240,
    flexDirection: 'row',
    alignItems: 'center',
  },
  inlineFieldLabel: {
    width: 95,
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3B2D17',
    marginRight: 4,
    flexShrink: 0,
  },
  formInput: {
    height: 26,
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#D4C3A3',
    borderRadius: 4,
    paddingHorizontal: 6,
    paddingVertical: 2,
    fontSize: 12,
    color: '#2B2118',
  },
  readonlyInput: {
    backgroundColor: '#F5EFE6',
    color: '#5C4A28',
  },
  highlightTotal: {
    backgroundColor: '#FFF4DA',
    color: '#8A5D00',
    fontWeight: 'bold',
    borderColor: '#D4C3A3',
  },

  // Attached lookup
  inputWithLookup: {
    flex: 1,
    flexDirection: 'row',
    alignItems: 'center',
  },
  inputAttached: {
    borderTopRightRadius: 0,
    borderBottomRightRadius: 0,
  },
  attachedLookupBtn: {
    height: 26,
    width: 26,
    backgroundColor: '#F5E7CA',
    borderWidth: 1,
    borderLeftWidth: 0,
    borderColor: '#D4C3A3',
    borderTopRightRadius: 4,
    borderBottomRightRadius: 4,
    justifyContent: 'center',
    alignItems: 'center',
    flexShrink: 0,
  },

  // Trade In
  tradeInOptionsGroup: {
    flex: 1,
    flexDirection: 'row',
    alignItems: 'center',
    gap: 4,
    flexWrap: 'wrap',
  },
  pillOption: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FCFAF6',
    borderWidth: 1,
    borderColor: '#E2D3B3',
    borderRadius: 4,
    paddingVertical: 3,
    paddingHorizontal: 6,
    gap: 3,
  },
  pillOptionSelected: {
    backgroundColor: '#FFF4DA',
    borderColor: '#B8903B',
  },
  pillOptionText: {
    fontSize: 11,
    fontWeight: '500',
    color: '#5C4A28',
  },
  pillOptionTextSelected: {
    color: '#9E6D08',
    fontWeight: 'bold',
  },
  tradeInGoBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFF1D6',
    borderWidth: 1,
    borderColor: '#B8903B',
    borderRadius: 4,
    paddingHorizontal: 6,
    height: 22,
    gap: 2,
  },
  tradeInGoBtnText: {
    fontSize: 10,
    fontWeight: 'bold',
    color: '#9E6D08',
  },

  // ===== TABLE SECTION =====
  counterBadge: {
    backgroundColor: '#FFFFFF',
    paddingHorizontal: 6,
    paddingVertical: 2,
    borderRadius: 10,
    marginLeft: 6,
    borderWidth: 1,
    borderColor: '#E2D3B3',
  },
  counterBadgeText: {
    fontSize: 10,
    fontWeight: 'bold',
    color: '#8D6409',
  },
  tableHeadRow: {
    flexDirection: 'row',
    backgroundColor: '#F5E7CA',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    paddingVertical: 6,
    paddingHorizontal: 8,
    width: '100%',
  },
  tableHeadCell: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3B2D17',
    paddingHorizontal: 4,
  },
  tableRow: {
    flexDirection: 'row',
    alignItems: 'center',
    borderBottomWidth: 1,
    borderBottomColor: '#F3EBDD',
    paddingVertical: 5,
    paddingHorizontal: 8,
    backgroundColor: '#FFFFFF',
    width: '100%',
  },
  tableCell: {
    fontSize: 11,
    color: '#3B2D17',
    paddingHorizontal: 4,
  },
  codeCell: {
    fontFamily: Platform.OS === 'ios' ? 'Courier' : 'monospace',
    fontWeight: 'bold',
    color: '#8A5D00',
  },
  amountCell: {
    fontWeight: 'bold',
    color: '#1B5E20',
  },
  cancelRowBtn: {
    backgroundColor: '#FFEBEE',
    borderWidth: 1,
    borderColor: '#EF9A9A',
    borderRadius: 4,
    paddingHorizontal: 8,
    paddingVertical: 2,
  },
  cancelRowBtnText: {
    fontSize: 10,
    fontWeight: 'bold',
    color: '#C62828',
  },
  emptyTableContainer: {
    paddingVertical: 24,
    alignItems: 'center',
    justifyContent: 'center',
    width: '100%',
  },
  emptyTableText: {
    fontSize: 12,
    color: '#9E8D77',
    marginTop: 4,
  },

  // ===== LOV MODALS =====
  modalOverlay: {
    flex: 1,
    backgroundColor: 'rgba(0, 0, 0, 0.45)',
    justifyContent: 'center',
    alignItems: 'center',
    padding: 16,
  },
  lovModalCard: {
    width: '92%',
    maxWidth: 600,
    maxHeight: '80%',
    backgroundColor: '#FFFFFF',
    borderRadius: 10,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    overflow: 'hidden',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.15,
    shadowRadius: 8,
    elevation: 8,
  },
  modalHeaderBanner: {
    backgroundColor: '#F5E7CA',
    paddingVertical: 8,
    paddingHorizontal: 12,
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
  },
  modalHeaderTitle: {
    fontSize: 13,
    fontWeight: 'bold',
    color: '#3B2D17',
  },
  closeModalBtn: {
    padding: 3,
  },
  modalSearchBox: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FCFAF6',
    margin: 8,
    paddingHorizontal: 8,
    height: 32,
    borderWidth: 1,
    borderColor: '#D4C3A3',
    borderRadius: 6,
  },
  modalSearchInput: {
    flex: 1,
    fontSize: 12,
    color: '#2B2118',
    paddingVertical: 0,
  },
  lovHeadRow: {
    flexDirection: 'row',
    backgroundColor: '#F8F2E4',
    paddingVertical: 5,
    paddingHorizontal: 10,
    borderBottomWidth: 1,
    borderBottomColor: '#E8DCB8',
  },
  lovHeadText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#8D6409',
  },
  lovItemRow: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingVertical: 8,
    paddingHorizontal: 10,
    borderBottomWidth: 1,
    borderBottomColor: '#F3EBDD',
  },
  lovItemText: {
    fontSize: 11,
    color: '#3B2D17',
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
