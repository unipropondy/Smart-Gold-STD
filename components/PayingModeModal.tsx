// Paying Mode Modal Component for Gold Scheme Transaction
// Styled to match the Gold Scheme Transaction Detail screen theme:
// Primary Gold: #A87300, Header Background: #F5E8C9, Main Background: #FFFFFF,
// Input Background: #FAF7F0, Border: #E8D6B5, Primary Text: #44351F, Secondary Text: #806B4D
import React, { useState, useEffect } from 'react';
import {
  View,
  Text,
  TextInput,
  TouchableOpacity,
  Modal,
  ScrollView,
  StyleSheet,
  ActivityIndicator,
  Platform,
  Alert
} from 'react-native';
import { Ionicons } from '@expo/vector-icons';
import { API_BASE_URL } from '@/constants/api';

export interface PayModeRow {
  PayMode: string;
  Commission: number;
  Amount: string;
  SlipNo: string;
  valueExist: boolean;
}

interface PayingModeModalProps {
  visible: boolean;
  totalAmount: number;
  customerCode: string;
  customerType: string;
  onClose: () => void;
  onSavePayment: (
    payments: Array<{ paymode: string; amount: number; slipNo: string; commission: number }>,
    summary: { total: number; paid: number; balance: number; commission: number; returnAmt: number }
  ) => Promise<void>;
  saving: boolean;
}

export const PayingModeModal: React.FC<PayingModeModalProps> = ({
  visible,
  totalAmount,
  customerCode,
  customerType,
  onClose,
  onSavePayment,
  saving
}) => {
  const [payModes, setPayModes] = useState<PayModeRow[]>([]);
  const [loading, setLoading] = useState<boolean>(false);
  const [customerData, setCustomerData] = useState<any>({});

  // Summary state
  const [paid, setPaid] = useState<number>(0);
  const [balance, setBalance] = useState<number>(0);
  const [commission, setCommission] = useState<number>(0);
  const [returnAmt, setReturnAmt] = useState<number>(0);

  // Recalculate totals according to VB.NET frmPayingmode.vb logic
  const calculateTotals = (modes: PayModeRow[]) => {
    let dPaid = 0;
    let dComm = 0;

    modes.forEach(m => {
      const amt = parseFloat(m.Amount) || 0;
      if (amt > 0) {
        dPaid += amt;
        const commRate = Number(m.Commission) || 0;
        if (commRate > 0) {
          const actualAmt = amt / (1 + commRate / 100);
          dComm += (amt - actualAmt);
        }
      }
    });

    const dTotal = totalAmount;
    let dBal = dTotal - dPaid;
    let dReturn = 0;

    if (dPaid >= dTotal) {
      dBal = 0;
      dReturn = dPaid - dTotal;
    }

    setPaid(dPaid);
    setCommission(dComm);
    setBalance(dBal);
    setReturnAmt(dReturn);
  };

  // Load payment modes and customer init data from backend
  const loadPaymentModes = async () => {
    setLoading(true);
    try {
      const res = await fetch(`${API_BASE_URL}/payingmode/initData?customerCode=${encodeURIComponent(customerCode)}&t=${Date.now()}`);
      const json = await res.json();

      let modes: PayModeRow[] = [];
      if (json.success && Array.isArray(json.payModes) && json.payModes.length > 0) {
        modes = json.payModes
          .filter((m: any) => m.Active !== false && m.Active !== 0)
          .map((m: any) => ({
            PayMode: (m.PayMode || '').trim(),
            Commission: parseFloat(m.Commission) || 0,
            Amount: '',
            SlipNo: '',
            valueExist: false
          }));
      } else {
        // Fallback default list from VB.NET
        const defaultList = [
          'CASH', 'NETS', 'VISA', 'TRADEIN', 'MASTER', 
          'AMEX', 'DINERS', 'DEPOSIT', 'INSTALLMENT', 
          'SALESRET', 'PAYNOW', 'YEAHPAY'
        ];
        modes = defaultList.map(name => ({
          PayMode: name,
          Commission: 0,
          Amount: '',
          SlipNo: '',
          valueExist: false
        }));
      }

      // Pre-fill defaults matching frmPayingmode.vb Form_Load:
      const cashIdx = modes.findIndex(m => m.PayMode.toUpperCase() === 'CASH');
      if (cashIdx !== -1 && totalAmount > 0) {
        modes[cashIdx].Amount = totalAmount.toFixed(2);
        modes[cashIdx].valueExist = true;
      }

      // Pre-fill DEPOSIT if available from customer
      if (json.customerData?.depositDetails) {
        const depIdx = modes.findIndex(m => m.PayMode.toUpperCase() === 'DEPOSIT');
        if (depIdx !== -1) {
          const depAmt = parseFloat(json.customerData.depositDetails.DepositAmount) || 0;
          modes[depIdx].Amount = depAmt.toFixed(2);
          modes[depIdx].SlipNo = (json.customerData.depositDetails.DepositNo || '').trim();
          modes[depIdx].valueExist = depAmt > 0;
        }
      }

      // Pre-fill TRADEIN if available
      if (json.customerData?.depositTradeInDetails) {
        const trdIdx = modes.findIndex(m => m.PayMode.toUpperCase() === 'TRADEIN');
        if (trdIdx !== -1) {
          const trdAmt = parseFloat(json.customerData.depositTradeInDetails.Amount) || 0;
          modes[trdIdx].Amount = trdAmt.toFixed(2);
          modes[trdIdx].SlipNo = (json.customerData.depositTradeInDetails.SalesReturnNo || '').trim();
          modes[trdIdx].valueExist = trdAmt > 0;
        }
      }

      setPayModes(modes);
      setCustomerData(json.customerData || {});
      calculateTotals(modes);
    } catch (err) {
      console.error('Error loading paying modes:', err);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    if (visible) {
      loadPaymentModes();
    }
  }, [visible, totalAmount, customerCode]);

  // Handle amount edit
  const handleAmountChange = (val: string, index: number) => {
    const updated = [...payModes];
    updated[index].Amount = val;
    updated[index].valueExist = (parseFloat(val) || 0) > 0;
    setPayModes(updated);
    calculateTotals(updated);
  };

  // Handle SlipNo edit
  const handleSlipNoChange = (val: string, index: number) => {
    const updated = [...payModes];
    updated[index].SlipNo = val;
    setPayModes(updated);
  };

  // Validate and submit
  const handleConfirmSave = async () => {
    if (saving) return;

    if (balance > 0) {
      const msg = 'Balance Amount Should Be Zero';
      Platform.OS === 'web' ? window.alert(msg) : Alert.alert('Payment Validation', msg);
      return;
    }

    if (paid < totalAmount) {
      const msg = `Paid amount ($${paid.toFixed(2)}) is less than total scheme amount ($${totalAmount.toFixed(2)}).`;
      Platform.OS === 'web' ? window.alert(msg) : Alert.alert('Payment Validation', msg);
      return;
    }

    const activePayments = payModes
      .filter(m => (parseFloat(m.Amount) || 0) > 0)
      .map(m => ({
        paymode: m.PayMode,
        amount: parseFloat(m.Amount) || 0,
        slipNo: m.SlipNo || '',
        commission: Number(m.Commission) || 0
      }));

    if (activePayments.length === 0) {
      const msg = 'Please enter at least one payment amount.';
      Platform.OS === 'web' ? window.alert(msg) : Alert.alert('Payment Validation', msg);
      return;
    }

    await onSavePayment(activePayments, {
      total: totalAmount,
      paid,
      balance,
      commission,
      returnAmt
    });
  };

  // Keyboard shortcut listener for F7 and Esc
  useEffect(() => {
    if (!visible) return;
    if (Platform.OS === 'web') {
      const handleModalKeyDown = (e: KeyboardEvent) => {
        if (e.key === 'F7') {
          e.preventDefault();
          e.stopPropagation();
          handleConfirmSave();
        } else if (e.key === 'Escape') {
          e.preventDefault();
          e.stopPropagation();
          onClose();
        }
      };
      window.addEventListener('keydown', handleModalKeyDown);
      return () => window.removeEventListener('keydown', handleModalKeyDown);
    }
  }, [visible, payModes, paid, balance, totalAmount, saving]);

  return (
    <Modal visible={visible} transparent animationType="fade" onRequestClose={onClose}>
      <View style={styles.modalOverlay}>
        <View style={styles.modalContainer}>
          {/* Header Bar matching Gold Scheme Transaction theme */}
          <View style={styles.modalHeader}>
            <Text style={styles.modalHeaderTitle}>Paying Mode</Text>
            <TouchableOpacity onPress={onClose} style={styles.closeBtn} activeOpacity={0.7}>
              <Ionicons name="close" size={18} color="#44351F" />
            </TouchableOpacity>
          </View>

          {/* Modal Body */}
          <View style={styles.modalBody}>
            {loading ? (
              <View style={{ padding: 40, alignItems: 'center' }}>
                <ActivityIndicator size="large" color="#A87300" />
                <Text style={{ marginTop: 10, color: '#44351F', fontWeight: '600' }}>Loading payment modes...</Text>
              </View>
            ) : (
              <View style={styles.contentRow}>
                {/* LEFT: PAYMENT DETAILS TABLE */}
                <View style={styles.leftTableContainer}>
                  {/* Table Header */}
                  <View style={styles.tableHeaderRow}>
                    <Text style={[styles.tableTh, { width: 140 }]}>PAYMODE</Text>
                    <Text style={[styles.tableTh, { width: 110, textAlign: 'right' }]}>AMOUNT</Text>
                    <Text style={[styles.tableTh, { width: 130 }]}>SLIP NO</Text>
                    <Text style={[styles.tableTh, { width: 70, textAlign: 'center' }]}>VALUE EX</Text>
                  </View>

                  {/* Table Rows Scrollable */}
                  <ScrollView style={styles.tableScrollView} showsVerticalScrollIndicator>
                    {payModes.map((item, index) => {
                      const isCash = item.PayMode.toUpperCase() === 'CASH';
                      const hasAmt = (parseFloat(item.Amount) || 0) > 0;

                      return (
                        <View key={item.PayMode || index} style={[styles.tableTr, index % 2 === 1 && styles.tableTrAlt]}>
                          {/* Paymode Column */}
                          <View style={[styles.tableTdWrap, { width: 140 }]}>
                            <Text style={styles.paymodeName}>{item.PayMode}</Text>
                          </View>

                          {/* Amount Input Column */}
                          <View style={[styles.tableTdWrap, { width: 110 }]}>
                            <TextInput
                              style={[
                                styles.amountInput,
                                isCash && hasAmt && styles.activeCashInput
                              ]}
                              value={item.Amount}
                              onChangeText={val => handleAmountChange(val, index)}
                              keyboardType="numeric"
                              placeholder="0.00"
                              placeholderTextColor="#A89A82"
                            />
                          </View>

                          {/* SlipNo Input Column */}
                          <View style={[styles.tableTdWrap, { width: 130 }]}>
                            <TextInput
                              style={styles.slipInput}
                              value={item.SlipNo}
                              onChangeText={val => handleSlipNoChange(val, index)}
                              editable={!isCash}
                              placeholder={isCash ? '--' : 'Slip/Ref'}
                              placeholderTextColor="#A89A82"
                            />
                          </View>

                          {/* Value Exist Column */}
                          <View style={[styles.tableTdWrap, { width: 70, alignItems: 'center' }]}>
                            {item.valueExist ? (
                              <Ionicons name="checkmark-sharp" size={15} color="#A87300" />
                            ) : (
                              <Text style={{ color: '#C8BEAF', fontSize: 11 }}>--</Text>
                            )}
                          </View>
                        </View>
                      );
                    })}
                  </ScrollView>
                </View>

                {/* RIGHT: PAYMENT SUMMARY BOX & ACTIONS */}
                <View style={styles.rightSummaryContainer}>
                  {/* Gold Scheme Summary Box */}
                  <View style={styles.summaryGrid}>
                    {/* Total Row */}
                    <View style={styles.summaryRow}>
                      <View style={styles.summaryLabelCell}>
                        <Text style={styles.summaryLabelText}>Total</Text>
                      </View>
                      <View style={styles.summaryValueCell}>
                        <Text style={styles.summaryValueText}>{totalAmount.toFixed(2)}</Text>
                      </View>
                    </View>

                    {/* Paid Row */}
                    <View style={styles.summaryRow}>
                      <View style={styles.summaryLabelCell}>
                        <Text style={styles.summaryLabelText}>Paid</Text>
                      </View>
                      <View style={styles.summaryValueCell}>
                        <Text style={styles.summaryValueText}>{paid.toFixed(2)}</Text>
                      </View>
                    </View>

                    {/* Balance Row */}
                    <View style={styles.summaryRow}>
                      <View style={styles.summaryLabelCell}>
                        <Text style={styles.summaryLabelText}>Balance</Text>
                      </View>
                      <View style={styles.summaryValueCell}>
                        <Text style={[styles.summaryValueText, balance > 0 && { color: '#B91C1C' }]}>
                          {balance.toFixed(2)}
                        </Text>
                      </View>
                    </View>

                    {/* Commission Row */}
                    <View style={styles.summaryRow}>
                      <View style={styles.summaryLabelCell}>
                        <Text style={styles.summaryLabelText}>Commission</Text>
                      </View>
                      <View style={styles.summaryValueCell}>
                        <Text style={styles.summaryValueText}>{commission.toFixed(2)}</Text>
                      </View>
                    </View>

                    {/* Return Row */}
                    <View style={[styles.summaryRow, { borderBottomWidth: 0 }]}>
                      <View style={styles.summaryLabelCell}>
                        <Text style={styles.summaryLabelText}>Return</Text>
                      </View>
                      <View style={styles.summaryValueCell}>
                        <Text style={styles.summaryValueText}>{returnAmt.toFixed(2)}</Text>
                      </View>
                    </View>
                  </View>

                  {/* Actions matching Gold Scheme Transaction buttons */}
                  <View style={styles.actionButtonsCol}>
                    <TouchableOpacity
                      style={[styles.saveBtn, saving && { opacity: 0.7 }]}
                      onPress={handleConfirmSave}
                      disabled={saving}
                      activeOpacity={0.8}
                    >
                      {saving ? (
                        <ActivityIndicator size="small" color="#FFFFFF" />
                      ) : (
                        <Text style={styles.saveBtnText}>Save</Text>
                      )}
                    </TouchableOpacity>

                    <TouchableOpacity
                      style={styles.cancelBtn}
                      onPress={onClose}
                      disabled={saving}
                      activeOpacity={0.8}
                    >
                      <Text style={styles.cancelBtnText}>Cancel</Text>
                    </TouchableOpacity>
                  </View>
                </View>
              </View>
            )}
          </View>
        </View>
      </View>
    </Modal>
  );
};

const styles = StyleSheet.create({
  modalOverlay: {
    flex: 1,
    backgroundColor: 'rgba(0,0,0,0.45)',
    justifyContent: 'center',
    alignItems: 'center',
    padding: 16,
  },
  modalContainer: {
    width: Platform.OS === 'web' ? 760 : '96%',
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1.5,
    borderColor: '#E8D6B5',
    overflow: 'hidden',
    shadowColor: '#44351F',
    shadowOffset: { width: 0, height: 6 },
    shadowOpacity: 0.15,
    shadowRadius: 12,
    elevation: 8,
  },
  modalHeader: {
    backgroundColor: '#F5E8C9', // Header background
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    paddingHorizontal: 16,
    paddingVertical: 10,
    borderBottomWidth: 1,
    borderBottomColor: '#E8D6B5',
  },
  modalHeaderTitle: {
    color: '#44351F',
    fontWeight: '700',
    fontSize: 14,
    letterSpacing: 0.2,
    fontFamily: Platform.OS === 'web' ? 'Cambria, Georgia, serif' : undefined,
  },
  closeBtn: {
    width: 24,
    height: 24,
    borderRadius: 4,
    alignItems: 'center',
    justifyContent: 'center',
    backgroundColor: '#EBD8B2',
  },
  modalBody: {
    padding: 14,
    backgroundColor: '#FFFFFF',
  },
  contentRow: {
    flexDirection: 'row',
    gap: 14,
  },
  leftTableContainer: {
    flex: 1,
    borderWidth: 1,
    borderColor: '#E8D6B5',
    borderRadius: 6,
    overflow: 'hidden',
    backgroundColor: '#FFFFFF',
  },
  tableHeaderRow: {
    flexDirection: 'row',
    backgroundColor: '#F5E8C9', // Gold Scheme table header
    borderBottomWidth: 1,
    borderBottomColor: '#E8D6B5',
    paddingVertical: 7,
    paddingHorizontal: 8,
  },
  tableTh: {
    color: '#44351F',
    fontWeight: '700',
    fontSize: 11,
    letterSpacing: 0.3,
  },
  tableScrollView: {
    maxHeight: 320,
    backgroundColor: '#FFFFFF',
  },
  tableTr: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingVertical: 5,
    paddingHorizontal: 8,
    borderBottomWidth: 1,
    borderBottomColor: '#F5EFE3',
  },
  tableTrAlt: {
    backgroundColor: '#FAF7F0', // Input / alt row background
  },
  tableTdWrap: {
    justifyContent: 'center',
  },
  paymodeName: {
    fontSize: 11,
    fontWeight: '700',
    color: '#44351F',
  },
  amountInput: {
    height: 28,
    borderWidth: 1,
    borderColor: '#E8D6B5',
    borderRadius: 4,
    paddingHorizontal: 8,
    fontSize: 11,
    textAlign: 'right',
    color: '#44351F',
    backgroundColor: '#FAF7F0',
  },
  activeCashInput: {
    backgroundColor: '#FAF7F0',
    color: '#A87300',
    fontWeight: '800',
    borderColor: '#A87300',
  },
  slipInput: {
    height: 28,
    borderWidth: 1,
    borderColor: '#E8D6B5',
    borderRadius: 4,
    paddingHorizontal: 8,
    fontSize: 11,
    color: '#44351F',
    backgroundColor: '#FAF7F0',
  },

  // Right Summary Styling matching Gold Scheme Transaction Detail
  rightSummaryContainer: {
    width: 250,
    justifyContent: 'space-between',
  },
  summaryGrid: {
    borderWidth: 1,
    borderColor: '#E8D6B5',
    borderRadius: 6,
    overflow: 'hidden',
    backgroundColor: '#FFFFFF',
  },
  summaryRow: {
    flexDirection: 'row',
    borderBottomWidth: 1,
    borderBottomColor: '#E8D6B5',
    height: 36,
  },
  summaryLabelCell: {
    width: 105,
    backgroundColor: '#F5E8C9', // Gold Theme Label
    justifyContent: 'center',
    paddingHorizontal: 12,
    borderRightWidth: 1,
    borderRightColor: '#E8D6B5',
  },
  summaryLabelText: {
    color: '#44351F',
    fontWeight: '700',
    fontSize: 12,
  },
  summaryValueCell: {
    flex: 1,
    backgroundColor: '#FAF7F0', // Light Gold/Cream Box
    justifyContent: 'center',
    alignItems: 'flex-end',
    paddingHorizontal: 12,
  },
  summaryValueText: {
    color: '#44351F',
    fontWeight: '700',
    fontSize: 13,
    fontFamily: Platform.OS === 'web' ? 'monospace' : undefined,
  },

  // Buttons matching Save & Cancel in Gold Scheme Transaction
  actionButtonsCol: {
    flexDirection: 'row',
    gap: 10,
    marginTop: 16,
  },
  saveBtn: {
    flex: 1,
    backgroundColor: '#A87300', // Primary Gold Save button
    paddingVertical: 10,
    borderRadius: 6,
    alignItems: 'center',
    justifyContent: 'center',
    borderWidth: 1,
    borderColor: '#8C5F00',
    shadowColor: '#44351F',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.1,
    shadowRadius: 2,
    elevation: 2,
  },
  saveBtnText: {
    color: '#FFFFFF',
    fontWeight: '700',
    fontSize: 12,
  },
  cancelBtn: {
    flex: 1,
    backgroundColor: '#FFFFFF', // Light Outline / Secondary Cancel
    paddingVertical: 10,
    borderRadius: 6,
    alignItems: 'center',
    justifyContent: 'center',
    borderWidth: 1,
    borderColor: '#E8D6B5',
    shadowColor: '#44351F',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.05,
    shadowRadius: 1,
    elevation: 1,
  },
  cancelBtnText: {
    color: '#44351F',
    fontWeight: '700',
    fontSize: 12,
  },
});
