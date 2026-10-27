import { ThemedText } from '@/components/themed-text';
import { API_BASE_URL } from '@/constants/api';
import { useThemeColors } from '@/hooks/useThemeColors';
import { Ionicons } from '@expo/vector-icons';
import { Stack, useLocalSearchParams, useRouter } from 'expo-router';
import React, { useEffect, useState } from 'react';
import {
  ActivityIndicator,
  Alert,
  KeyboardAvoidingView,
  Platform,
  ScrollView,
  StyleSheet,
  TextInput,
  TouchableOpacity,
  View,
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';

const API_URL = `${API_BASE_URL}/payingmode`;

interface PayModeItem {
  PayMode: string;
  Commission: number;
  Amount: string;
  SlipNo: string;
  valueExist: boolean;
}

export default function PayingModeScreen() {
  const themeColors = useThemeColors();
  const router = useRouter();
  const params = useLocalSearchParams();

  // Params passed from POS or Installment Payment
  const initialTotalNet = parseFloat(params.totalNet as string) || 0;
  const customerCode = (params.customerCode as string) || '';

  const [loading, setLoading] = useState(true);
  const [saving, setSaving] = useState(false);
  const [payModes, setPayModes] = useState<PayModeItem[]>([]);
  const [customerData, setCustomerData] = useState<any>({});

  // Summary States
  const [total, setTotal] = useState(initialTotalNet);
  const [paid, setPaid] = useState(0);
  const [balance, setBalance] = useState(initialTotalNet);
  const [commission, setCommission] = useState(0);
  const [returnAmount, setReturnAmount] = useState(0);

  const [toast, setToast] = useState<{ message: string; type: 'success' | 'error' } | null>(null);

  const showToast = (message: string, type: 'success' | 'error' = 'success') => {
    setToast({ message, type });
    setTimeout(() => setToast(null), 3000);
  };

  const fetchInitData = async () => {
    try {
      const response = await fetch(`${API_URL}/initData?customerCode=${encodeURIComponent(customerCode)}&t=${Date.now()}`);
      const json = await response.json();
      if (json.success) {
        const modes = json.payModes.map((m: any) => ({
          PayMode: m.PayMode ? m.PayMode.trim() : '',
          Commission: parseFloat(m.Commission) || 0,
          Amount: '',
          SlipNo: '',
          valueExist: false,
        }));

        // Setup initial default logic
        if (modes.length > 0) {
          // Pre-fill Deposit
          if (json.customerData?.depositDetails) {
            const depIndex = modes.findIndex((m: any) => m.PayMode.toUpperCase() === 'DEPOSIT');
            if (depIndex !== -1) {
              const depAmt = parseFloat(json.customerData.depositDetails.DepositAmount) || 0;
              modes[depIndex].Amount = depAmt > 0 ? depAmt.toFixed(2) : '0.00';
            }
          } else {
            const depIndex = modes.findIndex((m: any) => m.PayMode.toUpperCase() === 'DEPOSIT');
            if (depIndex !== -1) modes[depIndex].Amount = '0.00';
          }

          // Pre-fill Trade-In
          if (json.customerData?.depositTradeInDetails) {
            const trdIndex = modes.findIndex((m: any) => m.PayMode.toUpperCase() === 'TRADEIN');
            if (trdIndex !== -1) {
              const trdAmt = parseFloat(json.customerData.depositTradeInDetails.Amount) || 0;
              modes[trdIndex].Amount = trdAmt > 0 ? trdAmt.toFixed(2) : '0.00';
            }
          } else {
            const trdIndex = modes.findIndex((m: any) => m.PayMode.toUpperCase() === 'TRADEIN');
            if (trdIndex !== -1) modes[trdIndex].Amount = '0.00';
          }

          // CASH takes remaining balance
          if (initialTotalNet > 0) {
            const cashIndex = modes.findIndex((m: any) => m.PayMode.toUpperCase() === 'CASH');
            if (cashIndex !== -1) {
              modes[cashIndex].Amount = initialTotalNet.toFixed(2);
              modes[cashIndex].valueExist = true;
            }
          }
        }

        setPayModes(modes);
        setCustomerData(json.customerData || {});
        recalculateTotals(modes, initialTotalNet);
      } else {
        showToast('Failed to load paymodes', 'error');
      }
    } catch (error) {
      console.error('InitData Error:', error);
      showToast('Could not connect to server', 'error');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchInitData();
  }, []);

  const recalculateTotals = (modes: PayModeItem[], currentTotal: number) => {
    let currentPaid = 0;
    let currentCommission = 0;

    modes.forEach((mode) => {
      const amt = parseFloat(mode.Amount) || 0;
      if (amt > 0) {
        currentPaid += amt;
        const actualAmt = amt / (1 + mode.Commission / 100);
        const commAmt = amt - actualAmt;
        currentCommission += commAmt;
      }
    });

    setPaid(currentPaid);
    setCommission(currentCommission);

    const currBalance = currentTotal - currentPaid;
    if (currBalance <= 0) {
      setBalance(0);
      setReturnAmount(Math.abs(currBalance));
    } else {
      setBalance(currBalance);
      setReturnAmount(0);
    }
  };

  const handleAmountChange = (text: string, index: number) => {
    const newModes = [...payModes];
    newModes[index].Amount = text;
    newModes[index].valueExist = parseFloat(text) > 0;
    setPayModes(newModes);
    recalculateTotals(newModes, total);
  };

  const handleSlipNoChange = (text: string, index: number) => {
    const newModes = [...payModes];
    newModes[index].SlipNo = text;
    setPayModes(newModes);
  };

  const handleQuickCash = () => {
    const newModes = payModes.map((m) => {
      if (m.PayMode.toUpperCase() === 'CASH') {
        return { ...m, Amount: total.toFixed(2), valueExist: true };
      }
      return { ...m, Amount: '', valueExist: false };
    });
    setPayModes(newModes);
    recalculateTotals(newModes, total);
  };

  const handleClearAll = () => {
    const newModes = payModes.map((m) => ({ ...m, Amount: '', SlipNo: '', valueExist: false }));
    setPayModes(newModes);
    recalculateTotals(newModes, total);
  };

  const handleSave = async () => {
    if (balance > 0) {
      showToast('Balance amount should be zero before confirming.', 'error');
      return;
    }

    const selectedModes = payModes.filter((m) => (parseFloat(m.Amount) || 0) > 0);
    const apiEndpoint = params.apiEndpoint as string;
    const payloadStr = params.payload as string;
    const returnPath = params.returnPath as string;

    if (apiEndpoint && payloadStr) {
      setSaving(true);
      try {
        const payloadObj = JSON.parse(payloadStr);
        payloadObj.paymodeDetails = selectedModes.map((m) => ({
          payMode: m.PayMode,
          amount: m.Amount,
          remarks: m.SlipNo,
        }));

        const res = await fetch(apiEndpoint, {
          method: 'POST',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify(payloadObj),
        });
        const json = await res.json();
        if (json.success) {
          showToast('Payment Saved Successfully!', 'success');
          setTimeout(() => {
            if (returnPath) router.push(returnPath as any);
            else router.back();
          }, 1000);
        } else {
          showToast(json.message || 'Failed to save payment', 'error');
        }
      } catch (err: any) {
        showToast(err.message || 'Network error occurred', 'error');
      } finally {
        setSaving(false);
      }
    } else {
      showToast('Payment processed successfully!', 'success');
      setTimeout(() => router.back(), 1000);
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
            <ThemedText style={styles.headerTitle}>Paying Mode Tender</ThemedText>
            <ThemedText style={styles.headerSubtitle}>
              {customerCode ? `Customer: ${customerCode}` : 'Settlement & Payment Allocation'}
            </ThemedText>
          </View>
          {(loading || saving) && <ActivityIndicator size="small" color="#B48218" style={{ marginLeft: 8 }} />}
        </View>

        <View style={styles.headerRight}>
          <TouchableOpacity
            style={[styles.actionBtn, styles.clearBtn]}
            onPress={handleClearAll}
            activeOpacity={0.8}
            disabled={saving || loading}
          >
            <Ionicons name="refresh-outline" size={14} color="#5C4A28" />
            <ThemedText style={styles.clearBtnText}>Reset</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity
            style={[styles.actionBtn, styles.quickCashBtn]}
            onPress={handleQuickCash}
            activeOpacity={0.8}
            disabled={saving || loading}
          >
            <Ionicons name="cash-outline" size={14} color="#7D5E19" />
            <ThemedText style={styles.quickCashBtnText}>Exact Cash</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity
            style={[styles.actionBtn, styles.saveBtn, balance > 0 && styles.saveBtnDisabled]}
            onPress={handleSave}
            activeOpacity={0.8}
            disabled={saving || loading || balance > 0}
          >
            {saving ? (
              <ActivityIndicator size="small" color="#9E6D08" />
            ) : (
              <>
                <Ionicons name="checkmark-done-outline" size={15} color={balance > 0 ? '#9E8D77' : '#9E6D08'} />
                <ThemedText style={[styles.saveBtnText, balance > 0 && styles.saveBtnTextDisabled]}>
                  Confirm Settlement
                </ThemedText>
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

      {loading ? (
        <View style={styles.loadingContainer}>
          <ActivityIndicator size="large" color="#B48218" />
          <ThemedText style={styles.loadingText}>Loading tender payment modes...</ThemedText>
        </View>
      ) : (
        <KeyboardAvoidingView
          style={{ flex: 1 }}
          behavior={Platform.OS === 'ios' ? 'padding' : undefined}
        >
          <ScrollView contentContainerStyle={styles.scrollContainer} keyboardShouldPersistTaps="handled">
            <View style={styles.layoutRow}>
              {/* LEFT CARD: TENDER ALLOCATION GRID (FLEX 1.6) */}
              <View style={[styles.goldCard, styles.leftCard]}>
                <View style={styles.cardHeaderBanner}>
                  <View style={styles.headerTitleContainer}>
                    <View style={styles.headerIconBadgeSmall}>
                      <Ionicons name="wallet-outline" size={13} color="#B48218" />
                    </View>
                    <ThemedText style={styles.cardHeaderTitleSmall}>Tender Paymodes</ThemedText>
                    <View style={styles.counterBadge}>
                      <ThemedText style={styles.counterBadgeText}>{payModes.length} modes</ThemedText>
                    </View>
                  </View>

                  {paid > 0 && (
                    <View style={styles.allocatedPill}>
                      <ThemedText style={styles.allocatedPillText}>Allocated: ${paid.toFixed(2)}</ThemedText>
                    </View>
                  )}
                </View>

                {/* Table Header Row (Full Width) */}
                <View style={styles.tableHeadRow}>
                  <ThemedText style={[styles.tableHeadCell, { flex: 1.5 }]}>Payment Mode</ThemedText>
                  <ThemedText style={[styles.tableHeadCell, { flex: 0.8, textAlign: 'right' }]}>Comm %</ThemedText>
                  <ThemedText style={[styles.tableHeadCell, { flex: 1.4, textAlign: 'right' }]}>Amount ($)</ThemedText>
                  <ThemedText style={[styles.tableHeadCell, { flex: 1.5 }]}>Slip / Cheque No</ThemedText>
                </View>

                {/* Table Body */}
                <View style={styles.tableBodyContainer}>
                  {payModes.map((item, index) => {
                    const isAllocated = parseFloat(item.Amount) > 0;
                    return (
                      <View
                        key={item.PayMode}
                        style={[styles.tableRow, isAllocated && styles.tableRowAllocated]}
                      >
                        {/* Mode Name */}
                        <View style={[styles.tableCellWrap, { flex: 1.5 }]}>
                          <ThemedText style={[styles.modeCellText, isAllocated && styles.modeCellTextAllocated]}>
                            {item.PayMode}
                          </ThemedText>
                        </View>

                        {/* Commission */}
                        <View style={[styles.tableCellWrap, { flex: 0.8, alignItems: 'flex-end' }]}>
                          <ThemedText style={styles.commCellText}>
                            {item.Commission > 0 ? `${item.Commission.toFixed(1)}%` : '-'}
                          </ThemedText>
                        </View>

                        {/* Amount Input */}
                        <View style={[styles.tableCellWrap, { flex: 1.4 }]}>
                          <TextInput
                            style={[
                              styles.formInput,
                              styles.amountInput,
                              isAllocated && styles.amountInputAllocated,
                            ]}
                            keyboardType="numeric"
                            value={item.Amount}
                            onChangeText={(txt) => handleAmountChange(txt, index)}
                            placeholder="0.00"
                            placeholderTextColor="#A89885"
                          />
                        </View>

                        {/* Slip / Ref No Input */}
                        <View style={[styles.tableCellWrap, { flex: 1.5 }]}>
                          <TextInput
                            style={[
                              styles.formInput,
                              item.PayMode.toUpperCase() === 'CASH' && styles.disabledInput,
                            ]}
                            value={item.SlipNo}
                            onChangeText={(txt) => handleSlipNoChange(txt, index)}
                            editable={item.PayMode.toUpperCase() !== 'CASH'}
                            placeholder={item.PayMode.toUpperCase() === 'CASH' ? 'N/A' : 'Slip/Ref No...'}
                            placeholderTextColor="#A89885"
                          />
                        </View>
                      </View>
                    );
                  })}
                </View>
              </View>

              {/* RIGHT CARD: FINANCIAL BREAKDOWN LEDGER (FLEX 1) */}
              <View style={[styles.goldCard, styles.rightCard]}>
                <View style={styles.cardHeaderBanner}>
                  <View style={styles.headerTitleContainer}>
                    <View style={styles.headerIconBadgeSmall}>
                      <Ionicons name="calculator-outline" size={13} color="#B48218" />
                    </View>
                    <ThemedText style={styles.cardHeaderTitleSmall}>Settlement Summary</ThemedText>
                  </View>
                </View>

                <View style={styles.cardBody}>
                  {/* Total Required */}
                  <View style={styles.summaryLedgerRow}>
                    <View style={styles.summaryLedgerLabelWrap}>
                      <Ionicons name="receipt-outline" size={14} color="#7D5E19" style={{ marginRight: 6 }} />
                      <ThemedText style={styles.summaryLedgerLabel}>Total Required</ThemedText>
                    </View>
                    <ThemedText style={[styles.summaryLedgerValue, styles.totalValText]}>
                      ${total.toFixed(2)}
                    </ThemedText>
                  </View>

                  {/* Total Paid / Tendered */}
                  <View style={styles.summaryLedgerRow}>
                    <View style={styles.summaryLedgerLabelWrap}>
                      <Ionicons name="cash-outline" size={14} color="#2E7D32" style={{ marginRight: 6 }} />
                      <ThemedText style={styles.summaryLedgerLabel}>Total Tendered</ThemedText>
                    </View>
                    <ThemedText style={[styles.summaryLedgerValue, styles.paidValText]}>
                      ${paid.toFixed(2)}
                    </ThemedText>
                  </View>

                  {/* Balance Left to Tender */}
                  <View style={[styles.summaryLedgerRow, styles.balanceRowHighlight]}>
                    <View style={styles.summaryLedgerLabelWrap}>
                      <Ionicons
                        name={balance === 0 ? 'checkmark-circle' : 'alert-circle-outline'}
                        size={15}
                        color={balance === 0 ? '#2E7D32' : '#C62828'}
                        style={{ marginRight: 6 }}
                      />
                      <ThemedText style={[styles.summaryLedgerLabel, { fontWeight: 'bold' }]}>
                        {balance === 0 ? 'Fully Tendered' : 'Balance Remaining'}
                      </ThemedText>
                    </View>
                    <ThemedText
                      style={[
                        styles.summaryLedgerValue,
                        balance === 0 ? styles.zeroBalText : styles.balanceValText,
                      ]}
                    >
                      ${balance.toFixed(2)}
                    </ThemedText>
                  </View>

                  {/* Change / Return Amount */}
                  <View style={styles.summaryLedgerRow}>
                    <View style={styles.summaryLedgerLabelWrap}>
                      <Ionicons name="return-down-back-outline" size={14} color="#7D5E19" style={{ marginRight: 6 }} />
                      <ThemedText style={styles.summaryLedgerLabel}>Change / Return</ThemedText>
                    </View>
                    <ThemedText style={[styles.summaryLedgerValue, styles.returnValText]}>
                      ${returnAmount.toFixed(2)}
                    </ThemedText>
                  </View>

                  {/* Merchant Commission */}
                  <View style={[styles.summaryLedgerRow, { borderBottomWidth: 0 }]}>
                    <View style={styles.summaryLedgerLabelWrap}>
                      <Ionicons name="pie-chart-outline" size={14} color="#8D6409" style={{ marginRight: 6 }} />
                      <ThemedText style={styles.summaryLedgerLabel}>Est. Commission</ThemedText>
                    </View>
                    <ThemedText style={[styles.summaryLedgerValue, styles.commValText]}>
                      ${commission.toFixed(2)}
                    </ThemedText>
                  </View>

                  {/* Status Indicator Card */}
                  <View
                    style={[
                      styles.statusBanner,
                      balance === 0 ? styles.statusBannerSuccess : styles.statusBannerPending,
                    ]}
                  >
                    <Ionicons
                      name={balance === 0 ? 'checkmark-circle-outline' : 'information-circle-outline'}
                      size={18}
                      color={balance === 0 ? '#2E7D32' : '#9E6D08'}
                      style={{ marginRight: 8 }}
                    />
                    <ThemedText
                      style={[
                        styles.statusBannerText,
                        balance === 0 ? styles.statusBannerTextSuccess : styles.statusBannerTextPending,
                      ]}
                    >
                      {balance === 0
                        ? 'Ready for settlement! Click Confirm Settlement.'
                        : `Please allocate $${balance.toFixed(2)} more to complete payment.`}
                    </ThemedText>
                  </View>

                  {/* Direct Actions in Card */}
                  <View style={styles.cardActionsRow}>
                    <TouchableOpacity
                      style={[styles.bottomBtn, styles.cancelBtn]}
                      onPress={() => router.back()}
                      activeOpacity={0.7}
                    >
                      <ThemedText style={styles.cancelBtnText}>Back</ThemedText>
                    </TouchableOpacity>

                    <TouchableOpacity
                      style={[
                        styles.bottomBtn,
                        styles.confirmBtn,
                        balance > 0 && styles.confirmBtnDisabled,
                      ]}
                      onPress={handleSave}
                      activeOpacity={0.8}
                      disabled={saving || loading || balance > 0}
                    >
                      {saving ? (
                        <ActivityIndicator size="small" color="#FFFFFF" />
                      ) : (
                        <ThemedText
                          style={[
                            styles.confirmBtnText,
                            balance > 0 && styles.confirmBtnTextDisabled,
                          ]}
                        >
                          Confirm
                        </ThemedText>
                      )}
                    </TouchableOpacity>
                  </View>
                </View>
              </View>
            </View>
          </ScrollView>
        </KeyboardAvoidingView>
      )}
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  safeArea: {
    flex: 1,
    backgroundColor: '#FBF9F5',
  },
  loadingContainer: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
  },
  loadingText: {
    marginTop: 10,
    fontSize: 13,
    color: '#8D6409',
    fontWeight: '500',
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
  quickCashBtn: {
    backgroundColor: '#FFF4DA',
    borderColor: '#D4C3A3',
  },
  quickCashBtnText: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#7D5E19',
    marginLeft: 4,
  },
  saveBtn: {
    backgroundColor: '#F9F2E2',
    borderColor: '#B8903B',
  },
  saveBtnDisabled: {
    backgroundColor: '#F0ECE4',
    borderColor: '#D8D0C0',
  },
  saveBtnText: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#9E6D08',
    marginLeft: 4,
  },
  saveBtnTextDisabled: {
    color: '#9E8D77',
  },

  // ===== LAYOUT GRID =====
  layoutRow: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    gap: 12,
  },
  leftCard: {
    flex: 1.6,
    minWidth: 360,
  },
  rightCard: {
    flex: 1,
    minWidth: 300,
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
  allocatedPill: {
    backgroundColor: '#FFF4DA',
    paddingHorizontal: 8,
    paddingVertical: 2,
    borderRadius: 4,
    borderWidth: 1,
    borderColor: '#B8903B',
  },
  allocatedPillText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#8A5D00',
  },
  cardBody: {
    padding: 12,
  },

  // ===== TABLE SECTION =====
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
  tableBodyContainer: {
    width: '100%',
  },
  tableRow: {
    flexDirection: 'row',
    alignItems: 'center',
    borderBottomWidth: 1,
    borderBottomColor: '#F3EBDD',
    paddingVertical: 4,
    paddingHorizontal: 8,
    backgroundColor: '#FFFFFF',
  },
  tableRowAllocated: {
    backgroundColor: '#FFFBF2',
  },
  tableCellWrap: {
    paddingHorizontal: 4,
    justifyContent: 'center',
  },
  modeCellText: {
    fontSize: 12,
    fontWeight: '600',
    color: '#3B2D17',
  },
  modeCellTextAllocated: {
    fontWeight: 'bold',
    color: '#8A5D00',
  },
  commCellText: {
    fontSize: 11,
    color: '#7D5E19',
    fontWeight: '500',
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
  amountInput: {
    textAlign: 'right',
  },
  amountInputAllocated: {
    backgroundColor: '#FFF4DA',
    borderColor: '#B8903B',
    fontWeight: 'bold',
    color: '#8A5D00',
  },
  disabledInput: {
    backgroundColor: '#F5EFE6',
    color: '#9E8D77',
  },

  // ===== SUMMARY LEDGER =====
  summaryLedgerRow: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    paddingVertical: 8,
    borderBottomWidth: 1,
    borderBottomColor: '#F3EBDD',
  },
  summaryLedgerLabelWrap: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  summaryLedgerLabel: {
    fontSize: 12,
    color: '#5C4A28',
    fontWeight: '500',
  },
  summaryLedgerValue: {
    fontSize: 13,
    fontWeight: 'bold',
  },
  totalValText: {
    color: '#3B2D17',
    fontSize: 14,
  },
  paidValText: {
    color: '#2E7D32',
    fontSize: 14,
  },
  balanceRowHighlight: {
    backgroundColor: '#FCFAF6',
    paddingHorizontal: 6,
    borderRadius: 6,
    marginVertical: 4,
  },
  balanceValText: {
    color: '#C62828',
    fontSize: 15,
    fontWeight: 'bold',
  },
  zeroBalText: {
    color: '#2E7D32',
    fontSize: 15,
    fontWeight: 'bold',
  },
  returnValText: {
    color: '#7D5E19',
  },
  commValText: {
    color: '#8D6409',
  },

  // Status Banner
  statusBanner: {
    flexDirection: 'row',
    alignItems: 'center',
    padding: 10,
    borderRadius: 6,
    marginTop: 10,
    marginBottom: 12,
    borderWidth: 1,
  },
  statusBannerSuccess: {
    backgroundColor: '#E8F5E9',
    borderColor: '#A5D6A7',
  },
  statusBannerPending: {
    backgroundColor: '#FFF8E1',
    borderColor: '#FFE082',
  },
  statusBannerText: {
    flex: 1,
    fontSize: 11,
    lineHeight: 16,
  },
  statusBannerTextSuccess: {
    color: '#1B5E20',
    fontWeight: '600',
  },
  statusBannerTextPending: {
    color: '#8D6409',
    fontWeight: '500',
  },

  // Card Actions
  cardActionsRow: {
    flexDirection: 'row',
    gap: 8,
  },
  bottomBtn: {
    flex: 1,
    height: 34,
    borderRadius: 6,
    justifyContent: 'center',
    alignItems: 'center',
  },
  cancelBtn: {
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#DFD5C3',
  },
  cancelBtnText: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#5C4A28',
  },
  confirmBtn: {
    backgroundColor: '#B8903B',
  },
  confirmBtnDisabled: {
    backgroundColor: '#DFD5C3',
  },
  confirmBtnText: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#FFFFFF',
  },
  confirmBtnTextDisabled: {
    color: '#9E8D77',
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
