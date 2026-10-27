import { ThemedText } from '@/components/themed-text';
import { API_BASE_URL } from '@/constants/api';
import { Ionicons } from '@expo/vector-icons';
import { Stack, useRouter } from 'expo-router';
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
  TouchableWithoutFeedback,
  View,
  Modal,
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';

const API_URL = `${API_BASE_URL}/misc`;

type OptionType = {
  label: string;
  code: string;
};

export default function MiscScreen() {
  const router = useRouter();

  // Form Fields State
  const [selectedType, setSelectedType] = useState<OptionType | null>(null);
  const [voucherNo, setVoucherNo] = useState('');
  const [date, setDate] = useState('');
  const [netAmount, setNetAmount] = useState('');
  const [gstPerc, setGstPerc] = useState('0');
  const [isZeroRated, setIsZeroRated] = useState(false);
  const [amount, setAmount] = useState('0.00');
  const [gst, setGst] = useState('0.00');
  const [weight, setWeight] = useState('');
  const [person, setPerson] = useState('');
  const [remarks, setRemarks] = useState('');
  const [userId, setUserId] = useState('sys');

  // Configuration and Control State
  const [defaultGstPerc, setDefaultGstPerc] = useState('0');
  const [companyShortCode, setCompanyShortCode] = useState('');
  const [typeOptions, setTypeOptions] = useState<OptionType[]>([]);
  const [isTypeDropdownOpen, setIsTypeDropdownOpen] = useState(false);

  // Date Picker Modal State
  const [isCalendarOpen, setIsCalendarOpen] = useState(false);
  const [calendarMonth, setCalendarMonth] = useState(new Date().getMonth());
  const [calendarYear, setCalendarYear] = useState(new Date().getFullYear());

  const monthNames = [
    "January", "February", "March", "April", "May", "June",
    "July", "August", "September", "October", "November", "December"
  ];

  // Status State
  const [loading, setLoading] = useState(false);
  const [isEditing, setIsEditing] = useState(false);
  const [toast, setToast] = useState<{ message: string; type: 'success' | 'error' } | null>(null);

  const showToast = (message: string, type: 'success' | 'error' = 'success') => {
    setToast({ message, type });
    setTimeout(() => {
      setToast(null);
    }, 3000);
  };

  const showAlert = (title: string, message: string) => {
    if (Platform.OS === 'web') {
      window.alert(`${title}: ${message}`);
    } else {
      Alert.alert(title, message);
    }
  };

  // Fetch initial configuration on mount
  useEffect(() => {
    const fetchCompanyConfig = async () => {
      setLoading(true);
      try {
        const res = await fetch(`${API_URL}/company-info`);
        const json = await res.json();
        if (json.success) {
          const gstVal = String(json.GSTPerc);
          setGstPerc(gstVal);
          setDefaultGstPerc(gstVal);
          setCompanyShortCode(json.CompanyShortCode);

          // Configure type options based on company short code
          const shortCodeUpper = (json.CompanyShortCode || '').toUpperCase();
          if (shortCodeUpper === 'SSJ') {
            setTypeOptions([{ label: 'Services', code: 'SR' }]);
          } else {
            setTypeOptions([
              { label: 'Purchase Note', code: 'PN' },
              { label: 'Credit Note', code: 'CN' },
              { label: 'Misc.Note', code: 'MC' },
              { label: 'Petty Cash', code: 'PC' },
            ]);
          }
        }
      } catch (err) {
        showToast('Failed to load company config.', 'error');
      } finally {
        setLoading(false);
      }
    };

    // Set today's date format (dd/MM/yyyy)
    const today = new Date();
    const dd = String(today.getDate()).padStart(2, '0');
    const mm = String(today.getMonth() + 1).padStart(2, '0');
    const yyyy = today.getFullYear();
    setDate(`${dd}/${mm}/${yyyy}`);

    fetchCompanyConfig();
  }, []);

  // Fetch next voucher number when Type selection changes
  useEffect(() => {
    if (!selectedType || isEditing) return;

    const fetchNextVoucher = async () => {
      setLoading(true);
      try {
        const res = await fetch(`${API_URL}/next-voucher?type=${selectedType.code}`);
        const json = await res.json();
        if (json.success) {
          setVoucherNo(String(json.VoucherNo));
        }
      } catch (err) {
        showToast('Failed to fetch next voucher number', 'error');
      } finally {
        setLoading(false);
      }
    };

    fetchNextVoucher();
  }, [selectedType, isEditing]);

  // Handle calculation when NetAmount, GST % changes
  const handleCalculate = (netAmtValue: string, currentGstPerc: string) => {
    const netAmt = parseFloat(netAmtValue) || 0;
    const gstP = parseFloat(currentGstPerc) || 0;

    if (netAmt > 0) {
      const gstVal = netAmt - netAmt / (1 + gstP / 100);
      const amtVal = netAmt - gstVal;
      setGst(gstVal.toFixed(2));
      setAmount(amtVal.toFixed(2));
    } else {
      setGst('0.00');
      setAmount('0.00');
    }
  };

  const handleNetAmountChange = (text: string) => {
    setNetAmount(text);
    handleCalculate(text, gstPerc);
  };

  const handleZeroRatedChange = () => {
    const nextZeroState = !isZeroRated;
    setIsZeroRated(nextZeroState);

    const nextGstPerc = nextZeroState ? '0' : defaultGstPerc;
    setGstPerc(nextGstPerc);
    handleCalculate(netAmount, nextGstPerc);
  };

  // FETCH function
  const handleFetch = async () => {
    if (!selectedType) {
      showToast('Please select a Type first', 'error');
      return;
    }
    if (!voucherNo.trim()) {
      showToast('Please enter a Voucher No', 'error');
      return;
    }

    setLoading(true);
    try {
      const res = await fetch(`${API_URL}/fetch?type=${selectedType.code}&voucherNo=${voucherNo.trim()}`);
      const json = await res.json();
      if (json.success && json.data) {
        const data = json.data;

        // Map Date
        if (data.Date) {
          const d = new Date(data.Date);
          const dd = String(d.getDate()).padStart(2, '0');
          const mm = String(d.getMonth() + 1).padStart(2, '0');
          const yyyy = d.getFullYear();
          setDate(`${dd}/${mm}/${yyyy}`);
        }

        setNetAmount(data.NetAmount !== null ? String(data.NetAmount) : '');
        setGstPerc(data.GSTPerc !== null ? String(data.GSTPerc) : '0');
        setGst(data.GST !== null ? Number(data.GST).toFixed(2) : '0.00');
        setAmount(data.Amount !== null ? Number(data.Amount).toFixed(2) : '0.00');
        setWeight(data.Weight !== null ? String(data.Weight) : '');
        setPerson(data.Person || '');
        setRemarks(data.Remarks || '');
        setUserId(data.CreateUser || 'sys');
        setIsZeroRated(parseFloat(data.GSTPerc || 0) === 0);
        setIsEditing(true);

        showToast('Voucher details fetched successfully');
      } else {
        showToast(json.message || 'Voucher not found', 'error');
      }
    } catch (err) {
      showToast('Error fetching voucher details', 'error');
    } finally {
      setLoading(false);
    }
  };

  const handleSave = async () => {
    if (!selectedType) {
      showAlert('Information', 'Select Type');
      return;
    }
    if (!voucherNo || !voucherNo.trim()) {
      showAlert('Information', 'Enter VoucherNo');
      return;
    }
    if (isNaN(parseInt(voucherNo))) {
      showAlert('Information', 'VoucherNo must be a valid number');
      return;
    }
    if (!date || !date.trim()) {
      showAlert('Information', 'Select or enter Date');
      return;
    }
    const dateParts = date.split('/');
    if (dateParts.length !== 3) {
      showAlert('Information', 'Enter Date in dd/MM/yyyy format');
      return;
    }
    const parsedDay = parseInt(dateParts[0]);
    const parsedMonth = parseInt(dateParts[1]);
    const parsedYear = parseInt(dateParts[2]);
    if (isNaN(parsedDay) || isNaN(parsedMonth) || isNaN(parsedYear) || parsedMonth < 1 || parsedMonth > 12 || parsedDay < 1 || parsedDay > 31) {
      showAlert('Information', 'Enter a valid Date');
      return;
    }
    if (!netAmount || !netAmount.trim() || parseFloat(netAmount) === 0) {
      showAlert('Information', 'Enter Net Amount');
      return;
    }
    if (isNaN(parseFloat(netAmount))) {
      showAlert('Information', 'Net Amount must be a valid number');
      return;
    }
    if (parseFloat(amount || '0') === 0) {
      showAlert('Information', 'Enter Amount');
      return;
    }

    let parsedDate = new Date();
    if (date) {
      const parts = date.split('/');
      if (parts.length === 3) {
        parsedDate = new Date(parseInt(parts[2]), parseInt(parts[1]) - 1, parseInt(parts[0]));
      }
    }

    const payload = {
      Type: selectedType.code,
      VoucherNo: parseInt(voucherNo),
      Date: parsedDate.toISOString(),
      Person: person.trim(),
      Amount: parseFloat(amount),
      GSTPerc: parseFloat(gstPerc),
      GST: parseFloat(gst),
      NetAmount: parseFloat(netAmount),
      Weight: parseFloat(weight) || 0,
      Remarks: remarks.trim(),
      CreateUser: userId.trim() || 'sys',
      ModifyUser: userId.trim() || 'sys',
    };

    setLoading(true);
    try {
      const response = await fetch(API_URL, {
        method: isEditing ? 'PUT' : 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload),
      });

      const json = await response.json();
      if (json.success) {
        showToast(json.message || 'Miscellaneous details saved successfully');
        handleCancel();
      } else {
        showToast(json.message || 'Failed to save', 'error');
      }
    } catch (err) {
      showToast('Network error saving details', 'error');
    } finally {
      setLoading(false);
    }
  };

  // DELETE function
  const handleDelete = async () => {
    if (!selectedType) {
      showToast('Please select a Type first', 'error');
      return;
    }
    if (!voucherNo.trim()) {
      showToast('Please enter a Voucher No', 'error');
      return;
    }

    const executeDelete = async () => {
      setLoading(true);
      try {
        const response = await fetch(`${API_URL}?type=${selectedType.code}&voucherNo=${voucherNo.trim()}`, {
          method: 'DELETE',
        });
        const json = await response.json();
        if (json.success) {
          showToast('Miscellaneous details deleted');
          handleCancel();
        } else {
          showToast(json.message || 'Failed to delete', 'error');
        }
      } catch (err) {
        showToast('Error deleting entry', 'error');
      } finally {
        setLoading(false);
      }
    };

    if (Platform.OS === 'web') {
      if (window.confirm(`Do you want to delete ${selectedType.label} Type?`)) {
        executeDelete();
      }
    } else {
      Alert.alert(
        'Confirm Delete',
        `Do you want to delete ${selectedType.label} Type?`,
        [
          { text: 'No', style: 'cancel' },
          { text: 'Yes', style: 'destructive', onPress: executeDelete },
        ]
      );
    }
  };

  // REPRINT logic simulation
  const handleReprint = () => {
    if (!selectedType || !voucherNo.trim()) {
      showToast('Select Type and enter Voucher No to reprint', 'error');
      return;
    }
    showToast(`Reprinting Voucher ${selectedType.code}-${voucherNo}...`);
  };

  // CANCEL / CLEAR function
  const handleCancel = () => {
    setSelectedType(null);
    setVoucherNo('');
    const today = new Date();
    const dd = String(today.getDate()).padStart(2, '0');
    const mm = String(today.getMonth() + 1).padStart(2, '0');
    const yyyy = today.getFullYear();
    setDate(`${dd}/${mm}/${yyyy}`);

    setNetAmount('');
    setGstPerc(defaultGstPerc);
    setAmount('0.00');
    setGst('0.00');
    setWeight('');
    setPerson('');
    setRemarks('');
    setUserId('sys');
    setIsZeroRated(false);
    setIsEditing(false);
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

    for (let i = 0; i < firstDayIndex; i++) {
      days.push(null);
    }

    for (let i = 1; i <= numDays; i++) {
      days.push({ day: i });
    }

    return days;
  };

  const handleSelectDate = (day: number) => {
    const selectedMonth = String(calendarMonth + 1).padStart(2, '0');
    const selectedDay = String(day).padStart(2, '0');
    setDate(`${selectedDay}/${selectedMonth}/${calendarYear}`);
    setIsCalendarOpen(false);
  };

  return (
    <SafeAreaView style={styles.safeArea} edges={['top']}>
      <Stack.Screen options={{ headerShown: false }} />

      {/* Top Header Bar matching jobsheet & pos */}
      <View style={styles.header}>
        <View style={styles.headerLeft}>
          <TouchableOpacity style={styles.backButton} onPress={() => router.back()}>
            <Ionicons name="arrow-back" size={20} color="#3B2D17" />
          </TouchableOpacity>
          <ThemedText style={styles.headerTitle}>Miscellaneous Entry</ThemedText>
        </View>

        <View style={styles.headerActions}>
          <TouchableOpacity style={styles.headerBtnSecondary} onPress={handleReprint}>
            <Ionicons name="print-outline" size={14} color="#554428" style={{ marginRight: 4 }} />
            <ThemedText style={styles.headerBtnSecondaryText}>Reprint</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity style={styles.headerBtnSecondary} onPress={handleCancel}>
            <Ionicons name="refresh-outline" size={14} color="#554428" style={{ marginRight: 4 }} />
            <ThemedText style={styles.headerBtnSecondaryText}>Cancel</ThemedText>
          </TouchableOpacity>

          {isEditing && (
            <TouchableOpacity style={styles.headerBtnDanger} onPress={handleDelete}>
              <Ionicons name="trash-outline" size={14} color="#D93829" style={{ marginRight: 4 }} />
              <ThemedText style={styles.headerBtnDangerText}>Delete</ThemedText>
            </TouchableOpacity>
          )}

          <TouchableOpacity
            style={[styles.headerBtnPrimary, loading && { opacity: 0.7 }]}
            onPress={handleSave}
            disabled={loading}
          >
            <Ionicons name="save-outline" size={14} color="#9E6D08" style={{ marginRight: 4 }} />
            <ThemedText style={styles.headerBtnPrimaryText}>{isEditing ? 'Update' : 'Add'}</ThemedText>
          </TouchableOpacity>
        </View>
      </View>

      <KeyboardAvoidingView behavior={Platform.OS === 'ios' ? 'padding' : 'height'} style={{ flex: 1 }}>
        <ScrollView contentContainerStyle={styles.contentContainer}>
          {/* Main Card Form using 100% full width layout */}
          <View style={styles.topSplitRow}>
            {/* Left Primary Card: Voucher Header & Transaction Data */}
            <View style={[styles.goldCard, { flex: 2 }]}>
              <View style={styles.cardHeaderBanner}>
                <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                  <View style={styles.headerIconBadge}>
                    <Ionicons name="document-text-outline" size={14} color="#B48218" />
                  </View>
                  <ThemedText style={styles.cardHeaderTitle}>Voucher Details</ThemedText>
                </View>

                {isEditing && (
                  <View style={styles.counterBadge}>
                    <ThemedText style={styles.counterBadgeText}>Editing Mode</ThemedText>
                  </View>
                )}
              </View>

              <View style={styles.cardBody}>
                {/* Row 1: Type Selection (Pill Box) & Voucher No */}
                <View style={styles.formRow}>
                  {/* Type Selector */}
                  <View style={{ flex: 1.2, marginRight: 8 }}>
                    <ThemedText style={styles.inputLabel}>Type *</ThemedText>
                    <TouchableOpacity
                      style={styles.dropdownSelector}
                      onPress={() => setIsTypeDropdownOpen(true)}
                      activeOpacity={0.8}
                    >
                      <ThemedText style={[styles.dropdownSelectorText, !selectedType && { color: '#9E8D77' }]}>
                        {selectedType ? selectedType.label : 'Select Type...'}
                      </ThemedText>
                      <Ionicons name="chevron-down" size={14} color="#7D6F5B" />
                    </TouchableOpacity>
                  </View>

                  {/* Voucher No & Fetch Button */}
                  <View style={{ flex: 1 }}>
                    <ThemedText style={styles.inputLabel}>Voucher No *</ThemedText>
                    <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                      <TextInput
                        style={[styles.input, { flex: 1 }]}
                        value={voucherNo}
                        onChangeText={setVoucherNo}
                        keyboardType="numeric"
                        placeholder="Auto or number"
                        placeholderTextColor="#9E8D77"
                      />
                      <TouchableOpacity style={styles.searchBtn} onPress={handleFetch} activeOpacity={0.8}>
                        <Ionicons name="search" size={13} color="#7D5E19" style={{ marginRight: 3 }} />
                        <ThemedText style={styles.searchBtnText}>Fetch</ThemedText>
                      </TouchableOpacity>
                    </View>
                  </View>
                </View>

                {/* Row 2: Date & Person */}
                <View style={styles.formRow}>
                  {/* Date Input */}
                  <View style={{ flex: 1.2, marginRight: 8 }}>
                    <ThemedText style={styles.inputLabel}>Date *</ThemedText>
                    <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                      <TextInput
                        style={[styles.input, { flex: 1 }]}
                        value={date}
                        onChangeText={setDate}
                        placeholder="dd/MM/yyyy"
                        placeholderTextColor="#9E8D77"
                      />
                      <TouchableOpacity style={styles.lookupBtn} onPress={() => setIsCalendarOpen(true)} activeOpacity={0.8}>
                        <Ionicons name="calendar-outline" size={15} color="#6E5528" />
                      </TouchableOpacity>
                    </View>
                  </View>

                  {/* Person Input */}
                  <View style={{ flex: 1 }}>
                    <ThemedText style={styles.inputLabel}>Person</ThemedText>
                    <TextInput
                      style={styles.input}
                      value={person}
                      onChangeText={setPerson}
                      placeholder="Name of Person"
                      placeholderTextColor="#9E8D77"
                    />
                  </View>
                </View>

                {/* Row 3: Weight & Remarks */}
                <View style={styles.formRow}>
                  <View style={{ flex: 0.8, marginRight: 8 }}>
                    <ThemedText style={styles.inputLabel}>Weight (g)</ThemedText>
                    <TextInput
                      style={[styles.input, { textAlign: 'right' }]}
                      value={weight}
                      onChangeText={setWeight}
                      keyboardType="numeric"
                      placeholder="0.00"
                      placeholderTextColor="#9E8D77"
                    />
                  </View>

                  <View style={{ flex: 1.4 }}>
                    <ThemedText style={styles.inputLabel}>Remarks</ThemedText>
                    <TextInput
                      style={styles.input}
                      value={remarks}
                      onChangeText={setRemarks}
                      placeholder="Voucher remarks"
                      placeholderTextColor="#9E8D77"
                    />
                  </View>
                </View>

                {/* Row 4: User Id */}
                <View style={styles.formRow}>
                  <View style={{ flex: 0.8 }}>
                    <ThemedText style={styles.inputLabel}>User Id</ThemedText>
                    <TextInput
                      style={[styles.input, styles.readonlyInput]}
                      value={userId}
                      onChangeText={setUserId}
                      editable={false}
                    />
                  </View>
                </View>
              </View>
            </View>

            {/* Right Card: Financials & Tax Calculation */}
            <View style={[styles.goldCard, { flex: 1 }]}>
              <View style={styles.cardHeaderBanner}>
                <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                  <View style={styles.headerIconBadge}>
                    <Ionicons name="calculator-outline" size={14} color="#B48218" />
                  </View>
                  <ThemedText style={styles.cardHeaderTitle}>Financial Summary</ThemedText>
                </View>

                {/* Zero Rated Checkbox */}
                <TouchableOpacity
                  style={styles.zeroRatedToggle}
                  onPress={handleZeroRatedChange}
                  activeOpacity={0.8}
                >
                  <View style={[styles.checkboxSquare, isZeroRated && styles.checkboxSquareActive]}>
                    {isZeroRated && <Ionicons name="checkmark" size={11} color="#FFFFFF" />}
                  </View>
                  <ThemedText style={styles.zeroRatedLabel}>Zero Rated</ThemedText>
                </TouchableOpacity>
              </View>

              <View style={styles.summaryBody}>
                {/* Net Amount */}
                <View style={styles.summaryRow}>
                  <ThemedText style={styles.summaryLabel}>Net Amount *</ThemedText>
                  <TextInput
                    style={[styles.input, { width: 130, textAlign: 'right', fontWeight: 'bold', borderColor: '#B8903B' }]}
                    value={netAmount}
                    onChangeText={handleNetAmountChange}
                    keyboardType="numeric"
                    placeholder="0.00"
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* GST Rate */}
                <View style={styles.summaryRow}>
                  <ThemedText style={styles.summaryLabel}>GST Rate (%)</ThemedText>
                  <TextInput
                    style={[styles.input, styles.readonlyInput, { width: 130, textAlign: 'right' }]}
                    value={`${gstPerc}%`}
                    editable={false}
                  />
                </View>

                {/* Amount (Excl. GST) */}
                <View style={styles.summaryRow}>
                  <ThemedText style={styles.summaryLabel}>Amount (Excl.)</ThemedText>
                  <TextInput
                    style={[styles.input, styles.readonlyInput, { width: 130, textAlign: 'right' }]}
                    value={amount}
                    editable={false}
                  />
                </View>

                {/* GST Amount */}
                <View style={styles.summaryRow}>
                  <ThemedText style={styles.summaryLabel}>GST Amount</ThemedText>
                  <TextInput
                    style={[styles.input, styles.readonlyInput, { width: 130, textAlign: 'right' }]}
                    value={gst}
                    editable={false}
                  />
                </View>

                {/* Total Net Banner */}
                <View style={[styles.summaryRow, styles.summaryTotalRow]}>
                  <ThemedText style={styles.summaryTotalLabel}>Total Payable</ThemedText>
                  <ThemedText style={styles.summaryTotalValue}>${parseFloat(netAmount || '0').toFixed(2)}</ThemedText>
                </View>
              </View>
            </View>
          </View>
        </ScrollView>
      </KeyboardAvoidingView>

      {/* Calendar Date Picker Modal */}
      <Modal visible={isCalendarOpen} transparent={true} animationType="fade">
        <TouchableOpacity style={styles.modalOverlay} activeOpacity={1} onPress={() => setIsCalendarOpen(false)}>
          <TouchableWithoutFeedback>
            <View style={styles.calendarModalCard}>
              <View style={styles.modalHeader}>
                <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                  <View style={styles.headerIconBadge}>
                    <Ionicons name="calendar" size={14} color="#B48218" />
                  </View>
                  <ThemedText style={styles.modalTitle}>Select Voucher Date</ThemedText>
                </View>
                <TouchableOpacity onPress={() => setIsCalendarOpen(false)} style={{ padding: 4 }}>
                  <Ionicons name="close" size={20} color="#554428" />
                </TouchableOpacity>
              </View>

              <View style={{ padding: 14 }}>
                {/* Navigation Row */}
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

                {/* Weekdays */}
                <View style={styles.weekdaysRow}>
                  {['S', 'M', 'T', 'W', 'T', 'F', 'S'].map((day, idx) => (
                    <ThemedText key={idx} style={styles.weekdayCell}>{day}</ThemedText>
                  ))}
                </View>

                {/* Days Grid */}
                <View style={styles.daysGrid}>
                  {generateCalendarDays().map((dayInfo, idx) => {
                    let isSelected = false;
                    if (dayInfo && date) {
                      const dateParts = date.split('/');
                      if (dateParts.length === 3) {
                        const dayPart = parseInt(dateParts[0]);
                        const monthPart = parseInt(dateParts[1]) - 1;
                        const yearPart = parseInt(dateParts[2]);
                        isSelected = dayInfo.day === dayPart && calendarMonth === monthPart && calendarYear === yearPart;
                      }
                    }

                    return (
                      <TouchableOpacity
                        key={idx}
                        disabled={!dayInfo}
                        style={[
                          styles.dayCell,
                          !dayInfo && styles.emptyCell,
                          isSelected && styles.dayCellSelected,
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

      {/* Type Selection Modal */}
      <Modal visible={isTypeDropdownOpen} transparent={true} animationType="fade">
        <TouchableOpacity style={styles.modalOverlay} activeOpacity={1} onPress={() => setIsTypeDropdownOpen(false)}>
          <TouchableWithoutFeedback>
            <View style={styles.typeModalCard}>
              <View style={styles.modalHeader}>
                <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                  <View style={styles.headerIconBadge}>
                    <Ionicons name="list" size={14} color="#B48218" />
                  </View>
                  <ThemedText style={styles.modalTitle}>Select Voucher Type</ThemedText>
                </View>
                <TouchableOpacity onPress={() => setIsTypeDropdownOpen(false)} style={{ padding: 4 }}>
                  <Ionicons name="close" size={20} color="#554428" />
                </TouchableOpacity>
              </View>

              <View style={{ padding: 8 }}>
                {typeOptions.map((opt) => {
                  const isSelected = selectedType?.code === opt.code;
                  return (
                    <TouchableOpacity
                      key={opt.code}
                      style={[
                        styles.typeModalItem,
                        isSelected && styles.typeModalItemSelected,
                      ]}
                      onPress={() => {
                        setSelectedType(opt);
                        setIsTypeDropdownOpen(false);
                      }}
                      activeOpacity={0.7}
                    >
                      <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                        <View style={[styles.typeIconCircle, isSelected && styles.typeIconCircleSelected]}>
                          <Ionicons
                            name={
                              opt.code === 'PN' ? 'cart-outline' :
                              opt.code === 'CN' ? 'card-outline' :
                              opt.code === 'PC' ? 'cash-outline' : 'document-text-outline'
                            }
                            size={14}
                            color={isSelected ? '#FFFFFF' : '#8A703F'}
                          />
                        </View>
                        <ThemedText style={[styles.typeModalItemText, isSelected && styles.typeModalItemTextSelected]}>
                          {opt.label}
                        </ThemedText>
                      </View>
                      {isSelected && (
                        <Ionicons name="checkmark-circle" size={18} color="#B48218" />
                      )}
                    </TouchableOpacity>
                  );
                })}
              </View>
            </View>
          </TouchableWithoutFeedback>
        </TouchableOpacity>
      </Modal>

      {/* Toast Alert */}
      {toast && (
        <View style={[styles.toastContainer, toast.type === 'error' ? styles.toastError : styles.toastSuccess]}>
          <Ionicons
            name={toast.type === 'error' ? 'alert-circle' : 'checkmark-circle'}
            size={16}
            color="#FFFFFF"
            style={{ marginRight: 6 }}
          />
          <ThemedText style={styles.toastText}>{toast.message}</ThemedText>
        </View>
      )}

      {loading && (
        <View style={styles.loadingOverlay}>
          <ActivityIndicator size="large" color="#B48218" />
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
  contentContainer: {
    padding: 8,
  },

  // ===== TOP HEADER BAR =====
  header: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    backgroundColor: '#FFFFFF',
    borderBottomWidth: 1,
    borderBottomColor: '#EBE2D2',
    paddingHorizontal: 12,
    paddingVertical: 8,
  },
  headerLeft: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  backButton: {
    padding: 4,
    marginRight: 8,
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
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#DFD3BC',
    borderRadius: 6,
    paddingHorizontal: 10,
    paddingVertical: 5,
  },
  headerBtnSecondaryText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#554428',
  },
  headerBtnDanger: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFF6F6',
    borderWidth: 1,
    borderColor: '#F2A8A2',
    borderRadius: 6,
    paddingHorizontal: 10,
    paddingVertical: 5,
  },
  headerBtnDangerText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#D93829',
  },
  headerBtnPrimary: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#F5E7CA',
    borderWidth: 1,
    borderColor: '#D4C3A3',
    borderRadius: 6,
    paddingHorizontal: 12,
    paddingVertical: 5,
  },
  headerBtnPrimaryText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#9E6D08',
  },

  // ===== TOP SPLIT ROW =====
  topSplitRow: {
    flexDirection: 'row',
    gap: 8,
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
    overflow: 'visible',
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
    borderTopLeftRadius: 7,
    borderTopRightRadius: 7,
  },
  headerIconBadge: {
    width: 22,
    height: 22,
    borderRadius: 5,
    backgroundColor: '#FFF8EA',
    borderWidth: 1,
    borderColor: '#E2D2AF',
    alignItems: 'center',
    justifyContent: 'center',
    marginRight: 6,
  },
  cardHeaderTitle: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#3B2D17',
    letterSpacing: 0.3,
  },
  cardBody: {
    padding: 8,
  },
  counterBadge: {
    backgroundColor: '#FAF5EA',
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderRadius: 10,
    paddingHorizontal: 7,
    paddingVertical: 1,
    marginLeft: 8,
  },
  counterBadgeText: {
    fontSize: 10,
    fontWeight: 'bold',
    color: '#7D5E19',
  },

  // ===== FORM ROWS & INPUTS =====
  formRow: {
    flexDirection: 'row',
    alignItems: 'center',
    marginBottom: 8,
  },
  inputLabel: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#4A3B2C',
    marginBottom: 2,
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
  },
  readonlyInput: {
    backgroundColor: '#F8F5EE',
    borderColor: '#EADBCA',
    color: '#554736',
  },
  searchBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FAF5EA',
    borderWidth: 1,
    borderColor: '#DFD3BC',
    borderRadius: 6,
    paddingHorizontal: 8,
    marginLeft: 5,
    height: 28,
  },
  searchBtnText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#7D5E19',
  },
  lookupBtn: {
    width: 28,
    height: 28,
    backgroundColor: '#FAF5EA',
    borderWidth: 1,
    borderColor: '#D4C3A3',
    borderRadius: 5,
    marginLeft: 4,
    alignItems: 'center',
    justifyContent: 'center',
    flexShrink: 0,
  },

  // ===== DROPDOWN SELECTOR =====
  dropdownSelector: {
    height: 28,
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderRadius: 6,
    paddingHorizontal: 8,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
  },
  dropdownSelectorText: {
    fontSize: 12,
    color: '#2B2118',
    fontWeight: '500',
  },
  typeModalCard: {
    width: 320,
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    overflow: 'hidden',
    shadowColor: '#000',
    shadowOpacity: 0.15,
    shadowRadius: 10,
    elevation: 8,
  },
  typeModalItem: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingVertical: 10,
    paddingHorizontal: 12,
    borderRadius: 6,
    marginBottom: 4,
    borderWidth: 1,
    borderColor: '#F0E8D8',
    backgroundColor: '#FCFAF6',
  },
  typeModalItemSelected: {
    backgroundColor: '#FFF8E7',
    borderColor: '#D4C3A3',
  },
  typeIconCircle: {
    width: 26,
    height: 26,
    borderRadius: 13,
    backgroundColor: '#F3ECDA',
    alignItems: 'center',
    justifyContent: 'center',
    marginRight: 10,
  },
  typeIconCircleSelected: {
    backgroundColor: '#B48218',
  },
  typeModalItemText: {
    fontSize: 13,
    fontWeight: '500',
    color: '#3B2D17',
  },
  typeModalItemTextSelected: {
    fontWeight: 'bold',
    color: '#8A5D00',
  },

  // ===== ZERO RATED CHECKBOX =====
  zeroRatedToggle: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 6,
    paddingVertical: 2,
    borderRadius: 4,
  },
  checkboxSquare: {
    width: 14,
    height: 14,
    borderWidth: 1.5,
    borderColor: '#B8903B',
    borderRadius: 3,
    backgroundColor: '#FFFFFF',
    alignItems: 'center',
    justifyContent: 'center',
    marginRight: 4,
  },
  checkboxSquareActive: {
    backgroundColor: '#B8903B',
  },
  zeroRatedLabel: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#554428',
  },

  // ===== SUMMARY PANEL =====
  summaryBody: {
    padding: 10,
    gap: 6,
  },
  summaryRow: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
  },
  summaryLabel: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#4A3B2C',
  },
  summaryTotalRow: {
    borderTopWidth: 1,
    borderTopColor: '#ECD8AB',
    backgroundColor: '#FFF4DC',
    borderRadius: 5,
    paddingHorizontal: 8,
    paddingVertical: 6,
    marginTop: 4,
  },
  summaryTotalLabel: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#9E6D08',
  },
  summaryTotalValue: {
    fontSize: 15,
    fontWeight: 'bold',
    color: '#9E6D08',
  },

  // ===== CALENDAR MODAL =====
  modalOverlay: {
    flex: 1,
    backgroundColor: 'rgba(0,0,0,0.4)',
    justifyContent: 'center',
    alignItems: 'center',
    padding: 20,
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
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    paddingHorizontal: 12,
    paddingVertical: 8,
    backgroundColor: '#F5E7CA',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
  },
  modalTitle: {
    fontSize: 13,
    fontWeight: 'bold',
    color: '#3B2D17',
  },
  calendarNavRow: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    marginBottom: 12,
  },
  navArrow: {
    padding: 4,
  },
  weekdaysRow: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    marginBottom: 6,
  },
  weekdayCell: {
    width: 36,
    textAlign: 'center',
    fontSize: 11,
    fontWeight: 'bold',
    color: '#7D6F5B',
  },
  daysGrid: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    justifyContent: 'space-between',
  },
  dayCell: {
    width: 36,
    height: 32,
    justifyContent: 'center',
    alignItems: 'center',
    borderRadius: 4,
    marginVertical: 2,
  },
  dayCellSelected: {
    backgroundColor: '#B8903B',
  },
  dayCellText: {
    fontSize: 11,
    color: '#2B2118',
    fontWeight: '500',
  },
  dayCellTextSelected: {
    color: '#FFFFFF',
    fontWeight: 'bold',
  },
  emptyCell: {
    opacity: 0,
  },

  // ===== TOAST & OVERLAYS =====
  loadingOverlay: {
    ...StyleSheet.absoluteFillObject,
    backgroundColor: 'rgba(255, 255, 255, 0.6)',
    justifyContent: 'center',
    alignItems: 'center',
    zIndex: 1000,
  },
  toastContainer: {
    position: 'absolute',
    bottom: 30,
    alignSelf: 'center',
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 16,
    paddingVertical: 10,
    borderRadius: 8,
    zIndex: 9999,
    shadowColor: '#000',
    shadowOpacity: 0.15,
    shadowRadius: 8,
    elevation: 4,
  },
  toastSuccess: {
    backgroundColor: '#2E7D32',
  },
  toastError: {
    backgroundColor: '#C62828',
  },
  toastText: {
    color: '#FFFFFF',
    fontWeight: 'bold',
    fontSize: 12,
  },
});
