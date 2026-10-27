import { ThemedText } from '@/components/themed-text';
import { API_BASE_URL } from '@/constants/api';
import { Ionicons } from '@expo/vector-icons';
import { Stack, useRouter } from 'expo-router';
import React, { useEffect, useState } from 'react';
import {
  ActivityIndicator,
  Alert,
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

const API_URL = `${API_BASE_URL}/dailysalesentry`;

export default function DailySalesEntryScreen() {
  const router = useRouter();

  // ===== STATE =====
  const [loading, setLoading] = useState(false);
  const [toast, setToast] = useState<{ message: string; type: 'success' | 'error' } | null>(null);

  // Form Fields State
  const [invoiceDate, setInvoiceDate] = useState('');
  const [cash, setCash] = useState('');
  const [nets, setNets] = useState('');
  const [visa, setVisa] = useState('');
  const [master, setMaster] = useState('');
  const [amex, setAmex] = useState('');
  const [diners, setDiners] = useState('');
  const [chq, setChq] = useState('');
  const [transfer, setTransfer] = useState('');
  const [installment, setInstallment] = useState('');
  const [nonGst24k, setNonGst24k] = useState('');
  const [tradeInGold, setTradeInGold] = useState('');
  const [advancePayment, setAdvancePayment] = useState('');
  const [pettyCash, setPettyCash] = useState('');
  const [advCreditUtilise, setAdvCreditUtilise] = useState('');

  // Date Picker Modal State
  const [isCalendarOpen, setIsCalendarOpen] = useState(false);
  const [calendarMonth, setCalendarMonth] = useState(new Date().getMonth());
  const [calendarYear, setCalendarYear] = useState(new Date().getFullYear());

  // Computed Read-Only Fields
  const [totalGstSales, setTotalGstSales] = useState(0);
  const [gst, setGst] = useState(0);
  const [totalSales, setTotalSales] = useState(0);

  const monthNames = [
    "January", "February", "March", "April", "May", "June",
    "July", "August", "September", "October", "November", "December"
  ];

  const showToast = (message: string, type: 'success' | 'error' = 'success') => {
    setToast({ message, type });
    setTimeout(() => {
      setToast(null);
    }, 3000);
  };

  const setTodayDate = () => {
    const d = new Date();
    const year = d.getFullYear();
    const month = String(d.getMonth() + 1).padStart(2, '0');
    const day = String(d.getDate()).padStart(2, '0');
    setInvoiceDate(`${year}-${month}-${day}`);
    setCalendarMonth(d.getMonth());
    setCalendarYear(d.getFullYear());
  };

  useEffect(() => {
    setTodayDate();
  }, []);

  // Compute live calculations
  useEffect(() => {
    const valCash = parseFloat(cash) || 0;
    const valNets = parseFloat(nets) || 0;
    const valVisa = parseFloat(visa) || 0;
    const valMaster = parseFloat(master) || 0;
    const valAmex = parseFloat(amex) || 0;
    const valDiners = parseFloat(diners) || 0;
    const valChq = parseFloat(chq) || 0;
    const valTransfer = parseFloat(transfer) || 0;
    const valInstallment = parseFloat(installment) || 0;
    const valPetty = parseFloat(pettyCash) || 0;
    const valUtilise = parseFloat(advCreditUtilise) || 0;
    const val24k = parseFloat(nonGst24k) || 0;
    const valTradeIn = parseFloat(tradeInGold) || 0;

    // Total GST Sales = Cash + Nets + Visa + Master + Amex + Diners + Chq + Transfer + Installment + PettyCash + AdvCreditUtilise - 24K
    const totalGstSalesComputed = valCash + valNets + valVisa + valMaster + valAmex + valDiners + valChq + valTransfer + valInstallment + valPetty + valUtilise - val24k;
    setTotalGstSales(parseFloat(totalGstSalesComputed.toFixed(4)));

    // GST = Total GST Sales * (9 / 109)
    const gstComputed = totalGstSalesComputed * (9 / 109);
    setGst(parseFloat(gstComputed.toFixed(2)));

    // Total Sales = Total GST Sales + TradeIn Gold + 24K
    const totalSalesComputed = totalGstSalesComputed + valTradeIn + val24k;
    setTotalSales(parseFloat(totalSalesComputed.toFixed(2)));
  }, [cash, nets, visa, master, amex, diners, chq, transfer, installment, nonGst24k, tradeInGold, pettyCash, advCreditUtilise]);

  // Fetch daily sales record by date
  const handleFetch = async () => {
    if (!invoiceDate.trim()) {
      Alert.alert("Validation Error", "Please enter a valid Invoice Date");
      return;
    }

    setLoading(true);
    try {
      const res = await fetch(`${API_URL}?date=${encodeURIComponent(invoiceDate.trim())}`);
      const json = await res.json();
      if (json.success && json.data) {
        const d = json.data;
        setCash(String(d.Cash || '0'));
        setNets(String(d.Nets || '0'));
        setVisa(String(d.Visa || '0'));
        setMaster(String(d.Master || '0'));
        setAmex(String(d.Amex || '0'));
        setDiners(String(d.Diners || '0'));
        setTransfer(String(d.TransfertoAcc || '0'));
        setChq(String(d.Chq || '0'));
        setInstallment(String(d.Installment || '0'));
        setNonGst24k(String(d.NonGST24K || '0'));
        setTradeInGold(String(d.TradeInGold || '0'));
        setAdvancePayment(String(d.AdvancePayment || '0'));
        setPettyCash(String(d.PettyCash || '0'));
        setAdvCreditUtilise(String(d.AdvanceUtil || '0'));
        showToast("Daily sales loaded for " + invoiceDate.trim(), "success");
      } else {
        showToast(json.message || "No record found for this date. Fields cleared.", "error");
        subClearMe();
      }
    } catch (e) {
      console.error(e);
      showToast("Network error fetching daily sales", "error");
    } finally {
      setLoading(false);
    }
  };

  // Clear Form Fields
  const subClearMe = () => {
    setCash('');
    setNets('');
    setVisa('');
    setMaster('');
    setAmex('');
    setDiners('');
    setTransfer('');
    setChq('');
    setInstallment('');
    setNonGst24k('');
    setTradeInGold('');
    setAdvancePayment('');
    setPettyCash('');
    setAdvCreditUtilise('');
  };

  // Save / Update record
  const handleSave = async () => {
    if (!invoiceDate.trim()) {
      showToast("InvoiceDate should not be blank.", "error");
      return;
    }

    const valCash = parseFloat(cash) || 0;
    const valNets = parseFloat(nets) || 0;
    const valVisa = parseFloat(visa) || 0;
    const valMaster = parseFloat(master) || 0;
    const valAmex = parseFloat(amex) || 0;
    const valDiners = parseFloat(diners) || 0;
    const valChq = parseFloat(chq) || 0;
    const valTransfer = parseFloat(transfer) || 0;
    const valInstallment = parseFloat(installment) || 0;
    const val24k = parseFloat(nonGst24k) || 0;
    const valTradeIn = parseFloat(tradeInGold) || 0;
    const valAdvPay = parseFloat(advancePayment) || 0;
    const valPetty = parseFloat(pettyCash) || 0;
    const valUtilise = parseFloat(advCreditUtilise) || 0;

    const hasAnyValue = (
      valCash !== 0 || valNets !== 0 || valVisa !== 0 || valMaster !== 0 ||
      valAmex !== 0 || valDiners !== 0 || valChq !== 0 || valTransfer !== 0 ||
      valInstallment !== 0 || val24k !== 0 || valTradeIn !== 0 || valAdvPay !== 0 ||
      valPetty !== 0 || valUtilise !== 0
    );

    if (!hasAnyValue) {
      showToast("Please enter at least one amount before saving!", "error");
      return;
    }

    setLoading(true);
    try {
      const payload = {
        InvoiceDate: invoiceDate.trim(),
        Cash: parseFloat(cash) || 0,
        Nets: parseFloat(nets) || 0,
        Visa: parseFloat(visa) || 0,
        Master: parseFloat(master) || 0,
        Amex: parseFloat(amex) || 0,
        Diners: parseFloat(diners) || 0,
        Chq: parseFloat(chq) || 0,
        TransfertoAcc: parseFloat(transfer) || 0,
        Installment: parseFloat(installment) || 0,
        NonGST24K: parseFloat(nonGst24k) || 0,
        TradeInGold: parseFloat(tradeInGold) || 0,
        AdvancePayment: parseFloat(advancePayment) || 0,
        PettyCash: parseFloat(pettyCash) || 0,
        AdvanceUtil: parseFloat(advCreditUtilise) || 0,
        TotalGSTSales: totalGstSales,
        GST: gst,
        TotalSales: totalSales,
        userId: "sys"
      };

      const res = await fetch(`${API_URL}/save`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(payload)
      });
      const json = await res.json();
      if (json.success) {
        showToast(json.message || "Daily sales saved successfully!", "success");
      } else {
        showToast(json.message || "Error in Save.", "error");
      }
    } catch (e) {
      console.error(e);
      showToast("Server connection error during save", "error");
    } finally {
      setLoading(false);
    }
  };

  // Calendar Helper Functions
  const handlePrevMonth = () => {
    if (calendarMonth === 0) {
      setCalendarMonth(11);
      setCalendarYear(calendarYear - 1);
    } else {
      setCalendarMonth(calendarMonth - 1);
    }
  };

  const handleNextMonth = () => {
    if (calendarMonth === 11) {
      setCalendarMonth(0);
      setCalendarYear(calendarYear + 1);
    } else {
      setCalendarMonth(calendarMonth + 1);
    }
  };

  const generateCalendarDays = () => {
    const numDays = new Date(calendarYear, calendarMonth + 1, 0).getDate();
    const firstDayIndex = new Date(calendarYear, calendarMonth, 1).getDay();
    const days = [];

    // Add empty slots before first day of month
    for (let i = 0; i < firstDayIndex; i++) {
      days.push(null);
    }

    // Add actual days
    for (let i = 1; i <= numDays; i++) {
      days.push({ day: i });
    }

    return days;
  };

  const handleSelectDate = (day: number) => {
    const selectedMonth = String(calendarMonth + 1).padStart(2, '0');
    const selectedDay = String(day).padStart(2, '0');
    const formatted = `${calendarYear}-${selectedMonth}-${selectedDay}`;
    setInvoiceDate(formatted);
    setIsCalendarOpen(false);
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
          <ThemedText style={styles.headerTitle}>Daily Sales Entry</ThemedText>
          {loading && <ActivityIndicator size="small" color="#B48218" style={{ marginLeft: 6 }} />}
        </View>

        <View style={styles.headerRight}>
          {/* Clear Button */}
          <TouchableOpacity
            style={[styles.actionBtn, styles.clearBtn]}
            onPress={subClearMe}
            activeOpacity={0.8}
            disabled={loading}
          >
            <Ionicons name="refresh-outline" size={14} color="#5C4A28" />
            <ThemedText style={styles.clearBtnText}>Clear</ThemedText>
          </TouchableOpacity>

          {/* Save Button */}
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
        <ScrollView contentContainerStyle={styles.scrollContent}>
          {/* COMPACT DATE SELECTOR CARD */}
          <View style={styles.goldCard}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="calendar-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitleSmall}>Posting Date</ThemedText>
              </View>
              {invoiceDate ? (
                <View style={styles.dateBadge}>
                  <ThemedText style={styles.dateBadgeText}>{invoiceDate}</ThemedText>
                </View>
              ) : null}
            </View>

            <View style={styles.dateSelectorBody}>
              <View style={styles.compactDateRow}>
                <ThemedText style={styles.compactDateLabel}>Posting Date *</ThemedText>
                <View style={styles.dateInputWithLookup}>
                  <TextInput
                    style={[styles.formInput, styles.inputAttached, styles.codeCell, { maxWidth: 140 }]}
                    value={invoiceDate}
                    onChangeText={setInvoiceDate}
                    placeholder="YYYY-MM-DD"
                    placeholderTextColor="#9E8D77"
                  />
                  <TouchableOpacity
                    style={styles.attachedLookupBtn}
                    onPress={() => setIsCalendarOpen(true)}
                    activeOpacity={0.7}
                  >
                    <Ionicons name="calendar-outline" size={14} color="#7D5E19" />
                  </TouchableOpacity>
                  <TouchableOpacity
                    style={styles.actionFetchBtn}
                    onPress={handleFetch}
                    activeOpacity={0.7}
                  >
                    <Ionicons name="search" size={13} color="#8D6409" style={{ marginRight: 4 }} />
                    <ThemedText style={styles.actionFetchBtnText}>Fetch</ThemedText>
                  </TouchableOpacity>
                </View>
              </View>
            </View>
          </View>

          {/* MAIN CARD: ALL TENDER FIELDS IN BALANCED MULTI-COLUMN ROWS */}
          <View style={[styles.goldCard, { marginTop: 8 }]}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="cash-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitleSmall}>Tender Breakdown & Payments</ThemedText>
              </View>
            </View>

            <View style={styles.cardBody}>
              {/* Row 1: CASH, NETS, VISA */}
              <View style={styles.fieldsGridRow}>
                <View style={styles.gridFieldItem}>
                  <ThemedText style={styles.gridFieldLabel}>CASH</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.amountInput]}
                    value={cash}
                    onChangeText={setCash}
                    keyboardType="numeric"
                    placeholder="0.00"
                    placeholderTextColor="#9E8D77"
                  />
                </View>
                <View style={styles.gridFieldItem}>
                  <ThemedText style={styles.gridFieldLabel}>NETS</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.amountInput]}
                    value={nets}
                    onChangeText={setNets}
                    keyboardType="numeric"
                    placeholder="0.00"
                    placeholderTextColor="#9E8D77"
                  />
                </View>
                <View style={styles.gridFieldItem}>
                  <ThemedText style={styles.gridFieldLabel}>VISA</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.amountInput]}
                    value={visa}
                    onChangeText={setVisa}
                    keyboardType="numeric"
                    placeholder="0.00"
                    placeholderTextColor="#9E8D77"
                  />
                </View>
              </View>

              {/* Row 2: MASTER, AMEX, DINERS */}
              <View style={styles.fieldsGridRow}>
                <View style={styles.gridFieldItem}>
                  <ThemedText style={styles.gridFieldLabel}>MASTER</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.amountInput]}
                    value={master}
                    onChangeText={setMaster}
                    keyboardType="numeric"
                    placeholder="0.00"
                    placeholderTextColor="#9E8D77"
                  />
                </View>
                <View style={styles.gridFieldItem}>
                  <ThemedText style={styles.gridFieldLabel}>AMEX</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.amountInput]}
                    value={amex}
                    onChangeText={setAmex}
                    keyboardType="numeric"
                    placeholder="0.00"
                    placeholderTextColor="#9E8D77"
                  />
                </View>
                <View style={styles.gridFieldItem}>
                  <ThemedText style={styles.gridFieldLabel}>DINERS</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.amountInput]}
                    value={diners}
                    onChangeText={setDiners}
                    keyboardType="numeric"
                    placeholder="0.00"
                    placeholderTextColor="#9E8D77"
                  />
                </View>
              </View>

              {/* Row 3: CHQ, TRANSFER A/C, INST */}
              <View style={styles.fieldsGridRow}>
                <View style={styles.gridFieldItem}>
                  <ThemedText style={styles.gridFieldLabel}>CHQ</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.amountInput]}
                    value={chq}
                    onChangeText={setChq}
                    keyboardType="numeric"
                    placeholder="0.00"
                    placeholderTextColor="#9E8D77"
                  />
                </View>
                <View style={styles.gridFieldItem}>
                  <ThemedText style={styles.gridFieldLabel}>TRANSFER A/C</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.amountInput]}
                    value={transfer}
                    onChangeText={setTransfer}
                    keyboardType="numeric"
                    placeholder="0.00"
                    placeholderTextColor="#9E8D77"
                  />
                </View>
                <View style={styles.gridFieldItem}>
                  <ThemedText style={styles.gridFieldLabel}>INST</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.amountInput]}
                    value={installment}
                    onChangeText={setInstallment}
                    keyboardType="numeric"
                    placeholder="0.00"
                    placeholderTextColor="#9E8D77"
                  />
                </View>
              </View>

              {/* Row 4: 24K (NO GST), TradeIn Gold, Advance Pay */}
              <View style={styles.fieldsGridRow}>
                <View style={styles.gridFieldItem}>
                  <ThemedText style={styles.gridFieldLabel}>24K (NO GST)</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.amountInput]}
                    value={nonGst24k}
                    onChangeText={setNonGst24k}
                    keyboardType="numeric"
                    placeholder="0.00"
                    placeholderTextColor="#9E8D77"
                  />
                </View>
                <View style={styles.gridFieldItem}>
                  <ThemedText style={styles.gridFieldLabel}>TradeIn Gold</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.amountInput]}
                    value={tradeInGold}
                    onChangeText={setTradeInGold}
                    keyboardType="numeric"
                    placeholder="0.00"
                    placeholderTextColor="#9E8D77"
                  />
                </View>
                <View style={styles.gridFieldItem}>
                  <ThemedText style={styles.gridFieldLabel}>Advance Pay</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.amountInput]}
                    value={advancePayment}
                    onChangeText={setAdvancePayment}
                    keyboardType="numeric"
                    placeholder="0.00"
                    placeholderTextColor="#9E8D77"
                  />
                </View>
              </View>

              {/* Row 5: Petty Cash & Adv Credit Util */}
              <View style={styles.fieldsGridRow}>
                <View style={styles.gridFieldItem}>
                  <ThemedText style={styles.gridFieldLabel}>Petty Cash</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.amountInput]}
                    value={pettyCash}
                    onChangeText={setPettyCash}
                    keyboardType="numeric"
                    placeholder="0.00"
                    placeholderTextColor="#9E8D77"
                  />
                </View>
                <View style={styles.gridFieldItem}>
                  <ThemedText style={styles.gridFieldLabel}>Adv Credit Util</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.amountInput]}
                    value={advCreditUtilise}
                    onChangeText={setAdvCreditUtilise}
                    keyboardType="numeric"
                    placeholder="0.00"
                    placeholderTextColor="#9E8D77"
                  />
                </View>
                <View style={[styles.gridFieldItem, { opacity: 0 }]} />
              </View>
            </View>
          </View>

          {/* FINANCIAL SUMMARY TOTALS CARD */}
          <View style={[styles.goldCard, { marginTop: 8 }]}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="calculator-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitleSmall}>Computed Sales Summary</ThemedText>
              </View>
            </View>

            <View style={styles.cardBody}>
              <View style={styles.summaryBar}>
                {/* Total GST Sales */}
                <View style={styles.summaryPill}>
                  <ThemedText style={styles.summaryPillLabel}>Total GST Sales</ThemedText>
                  <ThemedText style={styles.summaryPillValue}>${totalGstSales.toFixed(2)}</ThemedText>
                </View>

                {/* GST (9%) */}
                <View style={styles.summaryPill}>
                  <ThemedText style={styles.summaryPillLabel}>GST (9%)</ThemedText>
                  <ThemedText style={styles.summaryPillValue}>${gst.toFixed(2)}</ThemedText>
                </View>

                {/* Grand Total Value */}
                <View style={styles.grandTotalPill}>
                  <View>
                    <ThemedText style={styles.grandTotalPillLabel}>TOTAL SALES VALUE</ThemedText>
                    <ThemedText style={styles.grandTotalPillSub}>GST + Non-GST</ThemedText>
                  </View>
                  <ThemedText style={styles.grandTotalPillValue}>${totalSales.toFixed(2)}</ThemedText>
                </View>
              </View>
            </View>
          </View>
        </ScrollView>
      </KeyboardAvoidingView>

      {/* CALENDAR DATE PICKER MODAL */}
      <Modal visible={isCalendarOpen} transparent animationType="fade" onRequestClose={() => setIsCalendarOpen(false)}>
        <TouchableOpacity style={styles.modalOverlay} activeOpacity={1} onPress={() => setIsCalendarOpen(false)}>
          <TouchableWithoutFeedback>
            <View style={styles.calendarModalCard}>
              <View style={styles.modalHeader}>
                <View style={{ flexDirection: 'row', alignItems: 'center', gap: 6 }}>
                  <View style={styles.headerIconBadgeSmall}>
                    <Ionicons name="calendar" size={14} color="#B48218" />
                  </View>
                  <ThemedText style={styles.modalTitle}>Select Date</ThemedText>
                </View>
                <TouchableOpacity onPress={() => setIsCalendarOpen(false)} style={styles.modalCloseBtn}>
                  <Ionicons name="close" size={20} color="#3B2D17" />
                </TouchableOpacity>
              </View>

              <View style={{ padding: 14 }}>
                <View style={styles.calendarNavRow}>
                  <TouchableOpacity style={styles.navArrow} onPress={handlePrevMonth}>
                    <Ionicons name="chevron-back" size={18} color="#3B2D17" />
                  </TouchableOpacity>
                  <ThemedText style={{ fontSize: 13, fontWeight: 'bold', color: '#3B2D17' }}>
                    {monthNames[calendarMonth]} {calendarYear}
                  </ThemedText>
                  <TouchableOpacity style={styles.navArrow} onPress={handleNextMonth}>
                    <Ionicons name="chevron-forward" size={18} color="#3B2D17" />
                  </TouchableOpacity>
                </View>

                <View style={styles.weekdaysRow}>
                  {['S', 'M', 'T', 'W', 'T', 'F', 'S'].map((day, idx) => (
                    <ThemedText key={idx} style={styles.weekdayCell}>{day}</ThemedText>
                  ))}
                </View>

                <View style={styles.daysGrid}>
                  {generateCalendarDays().map((dayInfo, idx) => {
                    const yearPart = invoiceDate ? parseInt(invoiceDate.split('-')[0]) : null;
                    const monthPart = invoiceDate ? parseInt(invoiceDate.split('-')[1]) - 1 : null;
                    const dayPart = invoiceDate ? parseInt(invoiceDate.split('-')[2]) : null;

                    const isSelected = dayInfo &&
                      dayInfo.day === dayPart &&
                      calendarMonth === monthPart &&
                      calendarYear === yearPart;

                    return (
                      <TouchableOpacity
                        key={idx}
                        disabled={!dayInfo}
                        style={[
                          styles.dayCell,
                          !dayInfo && styles.emptyCell,
                          isSelected && styles.dayCellSelected
                        ]}
                        onPress={() => dayInfo && handleSelectDate(dayInfo.day)}
                      >
                        <ThemedText style={[styles.dayCellText, isSelected && styles.dayCellTextSelected]}>
                          {dayInfo ? dayInfo.day : ''}
                        </ThemedText>
                      </TouchableOpacity>
                    );
                  })}
                </View>
              </View>
            </View>
          </TouchableWithoutFeedback>
        </TouchableOpacity>
      </Modal>

      {/* TOAST NOTIFICATION */}
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
  scrollContent: {
    padding: 8,
    paddingBottom: 24,
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
    paddingHorizontal: 10,
    paddingVertical: 6,
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
    fontSize: 12,
    fontWeight: 'bold',
    color: '#3B2D17',
    letterSpacing: 0.3,
  },
  cardBody: {
    padding: 10,
  },
  dateSelectorBody: {
    padding: 10,
  },

  // Date Badge
  dateBadge: {
    backgroundColor: '#FFF8EA',
    borderWidth: 1,
    borderColor: '#D8C59A',
    borderRadius: 10,
    paddingHorizontal: 8,
    paddingVertical: 1,
  },
  dateBadgeText: {
    fontSize: 10,
    fontWeight: 'bold',
    color: '#8D6409',
  },

  // ===== TWO COLUMN GRID =====
  twoColumnGrid: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    gap: 12,
  },

  // ===== DATE SELECTOR =====
  compactDateRow: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 8,
  },
  compactDateLabel: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#4A3B2C',
    width: 85,
  },

  // ===== MULTI-COLUMN GRID (SINGLE ROW STYLE) =====
  fieldsGridRow: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingVertical: 3,
    borderBottomWidth: 1,
    borderBottomColor: '#F7F2E9',
    gap: 12,
  },
  gridFieldItem: {
    flex: 1,
    flexDirection: 'row',
    alignItems: 'center',
    gap: 6,
    minWidth: 160,
  },
  gridFieldLabel: {
    fontSize: 11,
    fontWeight: '600',
    color: '#4A3B2C',
    width: 82,
    flexShrink: 0,
  },

  // ===== INLINE FIELD ROWS =====
  inlineFieldRow: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingVertical: 3,
    borderBottomWidth: 1,
    borderBottomColor: '#F7F2E9',
    gap: 8,
  },
  inlineFieldLabel: {
    fontSize: 11,
    fontWeight: '600',
    color: '#4A3B2C',
    width: 100,
    flexShrink: 0,
  },
  formInput: {
    height: 26,
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderRadius: 5,
    paddingHorizontal: 8,
    paddingVertical: 0,
    fontSize: 11,
    color: '#2B2118',
    flex: 1,
  },
  amountInput: {
    textAlign: 'right',
    fontWeight: '600',
  },
  inputAttached: {
    borderTopRightRadius: 0,
    borderBottomRightRadius: 0,
  },
  dateInputWithLookup: {
    flexDirection: 'row',
    alignItems: 'center',
    minWidth: 0,
  },
  attachedLookupBtn: {
    width: 28,
    height: 26,
    flexShrink: 0,
    backgroundColor: '#FAF5EA',
    borderWidth: 1,
    borderColor: '#D4C3A3',
    borderRadius: 5,
    marginLeft: 4,
    alignItems: 'center',
    justifyContent: 'center',
  },
  actionFetchBtn: {
    height: 26,
    flexDirection: 'row',
    backgroundColor: '#FAF5EA',
    borderWidth: 1,
    borderColor: '#D4C3A3',
    borderRadius: 5,
    paddingHorizontal: 10,
    marginLeft: 4,
    alignItems: 'center',
    justifyContent: 'center',
  },
  actionFetchBtnText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#8D6409',
  },
  codeCell: {
    fontWeight: 'bold',
    color: '#8D6409',
  },

  // ===== SUMMARY SECTION =====
  summaryBar: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    alignItems: 'center',
    gap: 10,
  },
  summaryPill: {
    flex: 1,
    minWidth: 150,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    backgroundColor: '#FCFAF6',
    borderWidth: 1,
    borderColor: '#E8DCB8',
    borderRadius: 6,
    paddingHorizontal: 10,
    paddingVertical: 6,
  },
  summaryPillLabel: {
    fontSize: 11,
    fontWeight: '600',
    color: '#5C4A28',
  },
  summaryPillValue: {
    fontSize: 13,
    fontWeight: 'bold',
    color: '#3B2D17',
  },
  grandTotalPill: {
    flex: 1.4,
    minWidth: 200,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    backgroundColor: '#FFF8EA',
    borderWidth: 1,
    borderColor: '#B8903B',
    borderRadius: 6,
    paddingHorizontal: 12,
    paddingVertical: 6,
  },
  grandTotalPillLabel: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#8D6409',
    letterSpacing: 0.3,
  },
  grandTotalPillSub: {
    fontSize: 8,
    color: '#7D6F5B',
  },
  grandTotalPillValue: {
    fontSize: 16,
    fontWeight: 'bold',
    color: '#9E6D08',
  },

  // ===== MODAL (CALENDAR) =====
  modalOverlay: {
    flex: 1,
    backgroundColor: 'rgba(30, 20, 10, 0.45)',
    justifyContent: 'center',
    alignItems: 'center',
    padding: 16,
  },
  calendarModalCard: {
    width: 320,
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
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
  calendarNavRow: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    marginBottom: 10,
  },
  navArrow: {
    padding: 4,
  },
  weekdaysRow: {
    flexDirection: 'row',
    marginBottom: 6,
  },
  weekdayCell: {
    flex: 1,
    textAlign: 'center',
    fontSize: 11,
    fontWeight: 'bold',
    color: '#8D6409',
  },
  daysGrid: {
    flexDirection: 'row',
    flexWrap: 'wrap',
  },
  dayCell: {
    width: '14.28%',
    height: 30,
    justifyContent: 'center',
    alignItems: 'center',
    borderRadius: 4,
  },
  emptyCell: {
    backgroundColor: 'transparent',
  },
  dayCellSelected: {
    backgroundColor: '#B8903B',
  },
  dayCellText: {
    fontSize: 12,
    color: '#3B2D17',
  },
  dayCellTextSelected: {
    color: '#FFFFFF',
    fontWeight: 'bold',
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
