import { formatColumnHeader } from '@/utils/formatColumnHeader';
import { API_BASE_URL } from '@/constants/api';
import { ReportDropdown } from '@/components/ReportDropdown';
import { ReportDatePicker } from '@/components/ReportDatePicker';
import { validateReportDateRange, getTodayIso, toDispDate } from '@/utils/dateValidation';
import { Ionicons } from '@expo/vector-icons';
import { Stack, useRouter } from 'expo-router';
import React, { useEffect, useState } from 'react';
import {
  ActivityIndicator,
  FlatList,
  Modal,
  Platform,
  ScrollView,
  StyleSheet,
  Text,
  TextInput,
  TouchableOpacity,
  TouchableWithoutFeedback,
  View,
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';
import DateTimePicker from '@react-native-community/datetimepicker';

// Royal Gold Design Palette
const GOLD_ACCENT = '#B48218';
const GOLD_DARK = '#9E6D08';
const GOLD_BORDER = '#E8DCB8';
const CARD_BG = '#FFFFFF';
const APP_BG = '#FBF9F5';
const TEXT_DARK = '#3B2D17';

// 12 Customer Reports List
interface ReportOption {
  id: string;
  label: string;
}

const REPORT_OPTIONS: ReportOption[] = [
  { id: 'customers_list', label: 'Customer Master Report' },
  { id: 'points_adjustment', label: 'Customer Points Adjustment' },
  { id: 'type_change', label: 'Customer Type Change' },
  { id: 'email_mobile', label: 'Email and Mobile Report' },
  { id: 'dob', label: 'Customer DOB Report' },
  { id: 'reward_points', label: 'Customer Reward Points' },
  { id: 'nationality_wise', label: 'Customer Nationality Wise' },
  { id: 'religion_wise', label: 'Customer Religion Wise' },
  { id: 'mailing_address', label: 'Customer Mailing Address Labels' },
  { id: 'customer_count', label: 'Customer Count' },
  { id: 'customer_volume', label: 'Customer Volume' },
  { id: 'points_expiry', label: 'Customer Points with Expiry Date' },
];

type ActionOption = 'Preview' | 'Print' | 'PDF' | 'Excel';

const RadioButton = ({
  label,
  selected,
  onPress,
}: {
  label: string;
  selected: boolean;
  onPress: () => void;
}) => {
  return (
    <TouchableOpacity
      style={[styles.radioBtn, selected && styles.radioBtnSelected]}
      onPress={onPress}
      activeOpacity={0.7}
    >
      <View style={[styles.radioCircle, selected && styles.radioCircleSelected]}>
        {selected && <View style={styles.radioDot} />}
      </View>
      <Text style={[styles.radioLabel, selected && styles.radioLabelSelected]}>
        {label}
      </Text>
    </TouchableOpacity>
  );
};

const formatDateLocal = (d: any) => {
  if (!d || !(d instanceof Date) || isNaN(d.getTime())) return '';
  const year = d.getFullYear();
  const month = String(d.getMonth() + 1).padStart(2, '0');
  const day = String(d.getDate()).padStart(2, '0');
  return `${year}-${month}-${day}`;
};

const parseLocalDate = (dateStr: string) => {
  if (!dateStr) return new Date();
  const parts = dateStr.split('-');
  if (parts.length !== 3) return new Date();
  const [y, m, d] = parts;
  return new Date(parseInt(y, 10), parseInt(m, 10) - 1, parseInt(d, 10));
};

export default function CustomerReportsScreen() {
  const router = useRouter();

  const [toastMessage, setToastMessage] = useState('');
  const [toastType, setToastType] = useState<'success' | 'error'>('success');

  const [selectedReport, setSelectedReport] = useState<string>('customers_list');
  const [customerDateType, setCustomerDateType] = useState<'Date of Birth' | 'Date of Anniversary'>('Date of Birth');
  const [customerType, setCustomerType] = useState<string>('ALL');
  const [nationality, setNationality] = useState<string>('ALL');
  const [religion, setReligion] = useState<string>('ALL');
  const [nricType, setNricType] = useState<'ALL' | 'SINGAPOREAN/SPR' | 'OTHERS'>('ALL');
  const [contactOption, setContactOption] = useState<'ALL' | 'SMS' | 'Phone' | 'None'>('ALL');
  const [customerCode, setCustomerCode] = useState<string>('');

  const [isReportDropdownOpen, setIsReportDropdownOpen] = useState(false);
  const [isCustTypeOpen, setIsCustTypeOpen] = useState(false);
  const [isNatOpen, setIsNatOpen] = useState(false);
  const [isRelOpen, setIsRelOpen] = useState(false);

  // Customer Lookup Modal State
  const [isCustomerModalVisible, setIsCustomerModalVisible] = useState(false);
  const [customerSearchQuery, setCustomerSearchQuery] = useState('');
  const [customerLookupList, setCustomerLookupList] = useState<any[]>([]);
  const [isLoadingLookup, setIsLoadingLookup] = useState(false);

  const [nationalitiesList, setNationalitiesList] = useState<{ id: string; label: string }[]>([
    { id: 'ALL', label: 'ALL NATIONALITIES' },
  ]);

  const [actionOption, setActionOption] = useState<ActionOption>('Preview');

  const [fromDate, setFromDate] = useState('2025-01-01');
  const [toDate, setToDate] = useState(formatDateLocal(new Date()));
  const [isLoading, setIsLoading] = useState(false);

  const [showDatePicker, setShowDatePicker] = useState<{ show: boolean; mode: 'from' | 'to' }>({
    show: false,
    mode: 'from',
  });

  // Fetch Nationalities from backend
  useEffect(() => {
    (async () => {
      try {
        const res = await fetch(`${API_BASE_URL}/customerreports/nationalities`);
        const json = await res.json();
        if (json.success && Array.isArray(json.data)) {
          const mapped = [
            { id: 'ALL', label: 'ALL NATIONALITIES' },
            ...json.data.map((n: any) => ({
              id: (n.Description || n.Code || '').trim(),
              label: (n.Description || n.Code || '').trim(),
            })).filter((x: any) => x.id),
          ];
          setNationalitiesList(mapped);
        }
      } catch (err) {
        console.warn('Failed to load nationalities:', err);
      }
    })();
  }, []);

  const closeAllDropdowns = () => {
    setIsReportDropdownOpen(false);
    setIsCustTypeOpen(false);
    setIsNatOpen(false);
    setIsRelOpen(false);
  };

  const showSuccess = (msg: string) => {
    setToastType('success');
    setToastMessage(msg);
    setTimeout(() => setToastMessage(''), 3000);
  };

  const showError = (msg: string) => {
    setToastType('error');
    setToastMessage(msg);
    setTimeout(() => setToastMessage(''), 3000);
  };

  const handleOpenCustomerLookup = async () => {
    setIsCustomerModalVisible(true);
    setIsLoadingLookup(true);
    try {
      const res = await fetch(`${API_BASE_URL}/customerreports/customers-search?search=${encodeURIComponent(customerSearchQuery)}&limit=100`);
      const json = await res.json();
      if (json.success && Array.isArray(json.data)) {
        setCustomerLookupList(json.data);
      }
    } catch (err) {
      console.error('Customer lookup error:', err);
    } finally {
      setIsLoadingLookup(false);
    }
  };

  const handleSearchCustomerInput = async (text: string) => {
    setCustomerSearchQuery(text);
    try {
      const res = await fetch(`${API_BASE_URL}/customerreports/customers-search?search=${encodeURIComponent(text)}&limit=100`);
      const json = await res.json();
      if (json.success && Array.isArray(json.data)) {
        setCustomerLookupList(json.data);
      }
    } catch (err) {
      console.error('Customer lookup search error:', err);
    }
  };

  const handleSelectCustomer = (c: any) => {
    setCustomerCode((c.CustomerCode || '').trim());
    setIsCustomerModalVisible(false);
  };

  const handleDateChange = (event: any, selectedDate?: Date) => {
    const mode = showDatePicker.mode;
    setShowDatePicker({ show: false, mode });
    if (selectedDate) {
      if (mode === 'from') {
        setFromDate(formatDateLocal(selectedDate));
      } else {
        setToDate(formatDateLocal(selectedDate));
      }
    }
  };

  const selectedReportObj = REPORT_OPTIONS.find((r) => r.id === selectedReport);

  // Check which filters apply to current report
  const needsDateRange = [
    'customers_list',
    'points_adjustment',
    'type_change',
    'email_mobile',
    'dob',
    'mailing_address',
    'customer_count',
    'customer_volume',
    'points_expiry',
  ].includes(selectedReport);

  const needsCustomerType = [
    'customers_list',
    'points_adjustment',
    'email_mobile',
    'dob',
    'reward_points',
    'nationality_wise',
    'religion_wise',
    'mailing_address',
    'customer_volume',
  ].includes(selectedReport);

  const needsNationality = ['customers_list', 'nationality_wise', 'mailing_address'].includes(selectedReport);
  const needsReligion = ['customers_list', 'religion_wise', 'mailing_address'].includes(selectedReport);
  const needsNricType = ['customers_list', 'email_mobile', 'reward_points', 'customer_count', 'customer_volume', 'mailing_address'].includes(selectedReport);
  const needsContactOption = ['customers_list', 'mailing_address'].includes(selectedReport);
  const needsCustomerCode = ['points_expiry'].includes(selectedReport);

  const handleGetReport = async (overrideAction?: ActionOption) => {
    const currentAction = overrideAction || actionOption;

    if (!selectedReport) {
      showError('Please select a Customer Report.');
      return;
    }

    if (needsDateRange && (!fromDate || !toDate)) {
      showError('Please select valid From Date and To Date.');
      return;
    }

    if (needsDateRange) {
      const valRes = validateReportDateRange(fromDate, toDate);
      if (!valRes.isValid) {
        showError(valRes.errorMessage || 'Invalid date range.');
        return;
      }
    }

    setIsLoading(true);

    let win: Window | null = null;
    if (
      Platform.OS === 'web' &&
      typeof window !== 'undefined' &&
      (currentAction === 'Preview' || currentAction === 'Print')
    ) {
      win = window.open('', '_blank');
    }

    try {
      const params = new URLSearchParams();
      params.append('reportId', selectedReport);
      if (fromDate) params.append('fromDate', fromDate);
      if (toDate) params.append('toDate', toDate);
      params.append('dobType', customerDateType === 'Date of Anniversary' ? 'DOA' : 'DOB');
      if (customerType && customerType !== 'ALL') params.append('customerType', customerType);
      if (nationality && nationality !== 'ALL') params.append('nationality', nationality);
      if (religion && religion !== 'ALL') params.append('religion', religion);
      if (nricType && nricType !== 'ALL') params.append('nricType', nricType);
      if (contactOption && contactOption !== 'ALL') params.append('contact', contactOption);
      if (customerCode && customerCode.trim()) params.append('customerCode', customerCode.trim());

      const res = await fetch(`${API_BASE_URL}/customerreports/report?${params.toString()}`);
      const json = await res.json();

      if (!json.success || !json.data || json.data.length === 0) {
        if (win) win.close();
        showError(json.message || 'No records found for the selected criteria.');
        setIsLoading(false);
        return;
      }

      const repTitle = selectedReportObj ? selectedReportObj.label : 'Customer Report';
      const keys = Object.keys(json.data[0]);
      const columns = keys.map((k) => ({ key: k, label: formatColumnHeader(k) }));
      const cleanName = repTitle.replace(/[^a-zA-Z0-9_-]/g, '_');

      // 1. PDF DOWNLOAD ACTION
      if (currentAction === 'PDF') {
        const pdfRes = await fetch(`${API_BASE_URL}/customerreports/export-pdf`, {
          method: 'POST',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify({
            title: repTitle,
            columns,
            data: json.data,
            totals: json.summary || {},
            fromDate,
            toDate,
          }),
        });

        if (!pdfRes.ok) {
          throw new Error('Failed to generate PDF document');
        }

        const pdfBlob = await pdfRes.blob();
        if (Platform.OS === 'web' && typeof window !== 'undefined') {
          const url = URL.createObjectURL(pdfBlob);
          const link = document.createElement('a');
          link.href = url;
          link.setAttribute('download', `${cleanName}.pdf`);
          document.body.appendChild(link);
          link.click();
          document.body.removeChild(link);
          setTimeout(() => URL.revokeObjectURL(url), 1000);
          showSuccess('PDF downloaded successfully!');
        } else {
          showSuccess('PDF report generated!');
        }
        setIsLoading(false);
        return;
      }

      // 2. EXCEL DOWNLOAD ACTION
      if (currentAction === 'Excel') {
        const excelRes = await fetch(`${API_BASE_URL}/customerreports/export-excel`, {
          method: 'POST',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify({
            title: repTitle,
            columns,
            data: json.data,
            totals: json.summary || {},
            fromDate,
            toDate,
          }),
        });

        if (!excelRes.ok) {
          throw new Error('Failed to generate Excel report');
        }

        const excelBlob = await excelRes.blob();
        if (Platform.OS === 'web' && typeof window !== 'undefined') {
          const url = URL.createObjectURL(excelBlob);
          const link = document.createElement('a');
          link.href = url;
          link.setAttribute('download', `${cleanName}.xlsx`);
          document.body.appendChild(link);
          link.click();
          document.body.removeChild(link);
          setTimeout(() => URL.revokeObjectURL(url), 1000);
          showSuccess('Excel file downloaded successfully!');
        } else {
          showSuccess('Excel report generated!');
        }
        setIsLoading(false);
        return;
      }

      // 3. PREVIEW & PRINT ACTIONS (HTML)
      const htmlRes = await fetch(`${API_BASE_URL}/customerreports/download-html`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          title: repTitle,
          columns,
          data: json.data,
          totals: json.summary || {},
          fromDate,
          toDate,
          showPrintButton: false,
        }),
      });

      if (!htmlRes.ok) {
        throw new Error('Failed to generate report HTML template');
      }

      let htmlContent = await htmlRes.text();

      // Trigger print for Print option
      if (currentAction === 'Print') {
        htmlContent = htmlContent.replace(
          '</body>',
          '<script>window.addEventListener("load", function(){ setTimeout(function(){ window.print(); }, 500); });</script></body>'
        );
      }

      if (Platform.OS === 'web' && typeof window !== 'undefined') {
        if (win) {
          win.document.open();
          win.document.write(htmlContent);
          win.document.close();
        } else {
          const blob = new Blob([htmlContent], { type: 'text/html;charset=utf-8;' });
          const url = URL.createObjectURL(blob);
          const newTab = window.open(url, '_blank');
          if (!newTab) {
            const link = document.createElement('a');
            link.href = url;
            link.setAttribute('download', `${repTitle}.html`);
            document.body.appendChild(link);
            link.click();
            document.body.removeChild(link);
          }
        }
        if (currentAction === 'Print') {
          showSuccess('Print dialog opened.');
        } else {
          showSuccess('Report preview opened.');
        }
      } else {
        showSuccess('Report generated! (Use web to view/print)');
      }
    } catch (err: any) {
      if (win) win.close();
      console.error(err);
      showError('Failed to generate report: ' + (err.message || 'Error'));
    } finally {
      setIsLoading(false);
    }
  };

  // Keyboard Shortcuts (F7/F9: Get Report, Esc: Close)
  useEffect(() => {
    if (Platform.OS === 'web' && typeof window !== 'undefined') {
      const handleKeyDown = (e: KeyboardEvent) => {
        const target = e.target as HTMLElement;
        if (target && (target.tagName === 'INPUT' || target.tagName === 'TEXTAREA')) {
          if (e.key === 'Escape') target.blur();
          return;
        }

        if (e.key === 'F7' || e.key === 'F9') {
          e.preventDefault();
          handleGetReport();
        } else if (e.key === 'Escape') {
          if (isReportDropdownOpen) {
            setIsReportDropdownOpen(false);
          } else {
            router.back();
          }
        }
      };
      window.addEventListener('keydown', handleKeyDown);
      return () => window.removeEventListener('keydown', handleKeyDown);
    }
  }, [selectedReport, fromDate, toDate, actionOption, isReportDropdownOpen, customerCode]);

  return (
    <SafeAreaView style={styles.container} edges={['top']}>
      <Stack.Screen options={{ headerShown: false }} />

      {/* HEADER */}
      <View style={styles.header}>
        <View style={styles.headerLeft}>
          <TouchableOpacity onPress={() => router.back()} style={styles.backButton} activeOpacity={0.7}>
            <Ionicons name="arrow-back" size={20} color={TEXT_DARK} />
          </TouchableOpacity>
          <Text style={styles.title}>Customer Reports</Text>
        </View>

        <TouchableOpacity
          style={styles.actionButton}
          onPress={() => handleGetReport()}
          disabled={isLoading}
          activeOpacity={0.8}
        >
          {isLoading ? (
            <ActivityIndicator size="small" color={GOLD_DARK} style={{ marginRight: 5 }} />
          ) : (
            <Ionicons name="document-text-outline" size={15} color={GOLD_DARK} style={{ marginRight: 5 }} />
          )}
          <Text style={styles.actionButtonText}>Get Report</Text>
        </TouchableOpacity>
      </View>

      <ScrollView contentContainerStyle={styles.scrollContent}>
        <View style={styles.cardWrapper}>
          <View style={styles.goldCard}>
            {/* Top row: Customer Report, From Date, To Date */}
            <View style={styles.inputRow}>
              {/* Customer Report */}
              <View style={styles.reportCol}>
                <ReportDropdown
                  label="Customer Report"
                  options={REPORT_OPTIONS}
                  selectedId={selectedReport}
                  onSelect={(id) => {
                    setSelectedReport(id);
                    setIsReportDropdownOpen(false);
                  }}
                  isOpen={isReportDropdownOpen}
                  onToggle={() => setIsReportDropdownOpen(!isReportDropdownOpen)}
                  maxHeight={320}
                />
              </View>

              {/* From Date (only if date filter applies) */}
              {needsDateRange && (
                <ReportDatePicker
                  label="From Date"
                  value={fromDate}
                  onChange={setFromDate}
                  isIsoOutput={true}
                  onOpenNative={() => setShowDatePicker({ show: true, mode: 'from' })}
                />
              )}

              {/* To Date (only if date filter applies) */}
              {needsDateRange && (
                <ReportDatePicker
                  label="To Date"
                  value={toDate}
                  onChange={setToDate}
                  isIsoOutput={true}
                  onOpenNative={() => setShowDatePicker({ show: true, mode: 'to' })}
                />
              )}

              {/* Customer Code Lookup for Report 12 */}
              {needsCustomerCode && (
                <View style={styles.customerCodeCol}>
                  <Text style={styles.fieldLabel}>Customer Code:</Text>
                  <View style={styles.customerCodeInputWrapper}>
                    <TextInput
                      style={styles.customerCodeInput}
                      value={customerCode}
                      onChangeText={setCustomerCode}
                      placeholder="Optional Code"
                      placeholderTextColor="#9E8D77"
                    />
                    <TouchableOpacity
                      style={styles.customerSearchBtn}
                      onPress={handleOpenCustomerLookup}
                      activeOpacity={0.7}
                    >
                      <Ionicons name="search" size={16} color="#FFFFFF" />
                    </TouchableOpacity>
                    {customerCode ? (
                      <TouchableOpacity
                        style={styles.customerClearBtn}
                        onPress={() => setCustomerCode('')}
                        activeOpacity={0.7}
                      >
                        <Ionicons name="close-circle" size={16} color="#9E8D77" />
                      </TouchableOpacity>
                    ) : null}
                  </View>
                </View>
              )}
            </View>

            {/* CONDITIONAL REPORT-SPECIFIC FILTERS: DOB vs DOA */}
            {selectedReport === 'dob' && (
              <View style={[styles.dateFilterRow, { borderBottomWidth: 1, borderBottomColor: '#F0E6CC', paddingBottom: 10, marginBottom: 12, marginTop: 12 }]}>
                <Text style={[styles.fieldLabel, { marginRight: 15, alignSelf: 'center', marginBottom: 0 }]}>Date Filter:</Text>
                <RadioButton
                  label="Date of Birth"
                  selected={customerDateType === 'Date of Birth'}
                  onPress={() => setCustomerDateType('Date of Birth')}
                />
                <RadioButton
                  label="Date of Anniversary"
                  selected={customerDateType === 'Date of Anniversary'}
                  onPress={() => setCustomerDateType('Date of Anniversary')}
                />
              </View>
            )}

            {/* CUSTOMER REPORT FILTER OPTIONS: Customer Type, Nationality, Religion, NRIC, Contact */}
            {(needsCustomerType || needsNationality || needsReligion || needsNricType || needsContactOption) && (
              <View style={styles.filterSection}>
                <View style={[styles.inputRow, { marginTop: 12, zIndex: 15 }]}>
                  {/* Customer Type */}
                  {needsCustomerType && (
                    <View style={styles.filterCol}>
                      <ReportDropdown
                        label="Customer Type"
                        options={[
                          { id: 'ALL', label: 'ALL TYPES' },
                          { id: 'DIAMOND', label: 'DIAMOND' },
                          { id: 'EMERALD', label: 'EMERALD' },
                          { id: 'RUBY', label: 'RUBY' },
                        ]}
                        selectedId={customerType}
                        onSelect={(id) => {
                          setCustomerType(id);
                          setIsCustTypeOpen(false);
                        }}
                        isOpen={isCustTypeOpen}
                        onToggle={() => {
                          closeAllDropdowns();
                          setIsCustTypeOpen(!isCustTypeOpen);
                        }}
                        maxHeight={200}
                      />
                    </View>
                  )}

                  {/* Nationality */}
                  {needsNationality && (
                    <View style={styles.filterCol}>
                      <ReportDropdown
                        label="Nationality"
                        options={nationalitiesList}
                        selectedId={nationality}
                        onSelect={(id) => {
                          setNationality(id);
                          setIsNatOpen(false);
                        }}
                        isOpen={isNatOpen}
                        onToggle={() => {
                          closeAllDropdowns();
                          setIsNatOpen(!isNatOpen);
                        }}
                        maxHeight={220}
                      />
                    </View>
                  )}

                  {/* Religion */}
                  {needsReligion && (
                    <View style={styles.filterCol}>
                      <ReportDropdown
                        label="Religion"
                        options={[
                          { id: 'ALL', label: 'ALL RELIGIONS' },
                          { id: 'HINDU', label: 'HINDU' },
                          { id: 'MUSLIM', label: 'MUSLIM' },
                          { id: 'CHRISTIAN', label: 'CHRISTIAN' },
                          { id: 'CHINESE', label: 'CHINESE' },
                        ]}
                        selectedId={religion}
                        onSelect={(id) => {
                          setReligion(id);
                          setIsRelOpen(false);
                        }}
                        isOpen={isRelOpen}
                        onToggle={() => {
                          closeAllDropdowns();
                          setIsRelOpen(!isRelOpen);
                        }}
                        maxHeight={200}
                      />
                    </View>
                  )}
                </View>

                {/* NRIC & Contact Radio Options */}
                {(needsNricType || needsContactOption) && (
                  <View style={[styles.filterRadioGroupRow, { marginTop: 14 }]}>
                    {/* NRIC Type */}
                    {needsNricType && (
                      <View style={styles.radioSubGroup}>
                        <Text style={[styles.fieldLabel, { marginBottom: 6 }]}>NRIC Type:</Text>
                        <View style={styles.radioSubGroupItems}>
                          <RadioButton
                            label="ALL"
                            selected={nricType === 'ALL'}
                            onPress={() => setNricType('ALL')}
                          />
                          <RadioButton
                            label="SINGAPOREAN / SPR"
                            selected={nricType === 'SINGAPOREAN/SPR'}
                            onPress={() => setNricType('SINGAPOREAN/SPR')}
                          />
                          <RadioButton
                            label="OTHERS"
                            selected={nricType === 'OTHERS'}
                            onPress={() => setNricType('OTHERS')}
                          />
                        </View>
                      </View>
                    )}

                    {/* Contact Options */}
                    {needsContactOption && (
                      <View style={styles.radioSubGroup}>
                        <Text style={[styles.fieldLabel, { marginBottom: 6 }]}>Contact Option:</Text>
                        <View style={styles.radioSubGroupItems}>
                          <RadioButton
                            label="ALL"
                            selected={contactOption === 'ALL'}
                            onPress={() => setContactOption('ALL')}
                          />
                          <RadioButton
                            label="SMS"
                            selected={contactOption === 'SMS'}
                            onPress={() => setContactOption('SMS')}
                          />
                          <RadioButton
                            label="Phone"
                            selected={contactOption === 'Phone'}
                            onPress={() => setContactOption('Phone')}
                          />
                          <RadioButton
                            label="None"
                            selected={contactOption === 'None'}
                            onPress={() => setContactOption('None')}
                          />
                        </View>
                      </View>
                    )}
                  </View>
                )}
              </View>
            )}

            {/* Divider */}
            <View style={styles.divider} />

            {/* Actions: Preview, Print, PDF, Excel */}
            <View style={styles.buttonRow}>
              <RadioButton
                label="Preview"
                selected={actionOption === 'Preview'}
                onPress={() => setActionOption('Preview')}
              />
              <RadioButton
                label="Print"
                selected={actionOption === 'Print'}
                onPress={() => setActionOption('Print')}
              />
              <RadioButton
                label="PDF"
                selected={actionOption === 'PDF'}
                onPress={() => setActionOption('PDF')}
              />
              <RadioButton
                label="Excel"
                selected={actionOption === 'Excel'}
                onPress={() => setActionOption('Excel')}
              />
            </View>
          </View>
        </View>

        {showDatePicker.show && (
          <DateTimePicker
            value={parseLocalDate(showDatePicker.mode === 'from' ? fromDate : toDate)}
            maximumDate={new Date()}
            mode="date"
            display="default"
            onChange={handleDateChange}
          />
        )}
      </ScrollView>

      {/* Customer Lookup Modal */}
      <Modal
        visible={isCustomerModalVisible}
        transparent
        animationType="fade"
        onRequestClose={() => setIsCustomerModalVisible(false)}
      >
        <TouchableOpacity
          style={styles.modalOverlay}
          activeOpacity={1}
          onPress={() => setIsCustomerModalVisible(false)}
        >
          <TouchableWithoutFeedback>
            <View style={styles.lovModalContainer}>
              <View style={styles.modalHeader}>
                <View style={{ flexDirection: 'row', alignItems: 'center', gap: 6 }}>
                  <View style={styles.headerIconBadgeSmall}>
                    <Ionicons name="person-circle-outline" size={16} color={GOLD_DARK} />
                  </View>
                  <Text style={styles.modalTitle}>Select Customer</Text>
                </View>
                <TouchableOpacity onPress={() => setIsCustomerModalVisible(false)} style={styles.modalCloseBtn}>
                  <Ionicons name="close" size={20} color={TEXT_DARK} />
                </TouchableOpacity>
              </View>

              <View style={{ padding: 12 }}>
                <View style={styles.modalSearchBox}>
                  <Ionicons name="search-outline" size={16} color="#8F816E" style={{ marginRight: 6 }} />
                  <TextInput
                    style={styles.modalSearchInput}
                    value={customerSearchQuery}
                    onChangeText={handleSearchCustomerInput}
                    placeholder="Search by Code, Name, or Mobile..."
                    placeholderTextColor="#8F816E"
                    autoFocus
                  />
                  {customerSearchQuery ? (
                    <TouchableOpacity onPress={() => handleSearchCustomerInput('')}>
                      <Ionicons name="close-circle" size={16} color="#8F816E" />
                    </TouchableOpacity>
                  ) : null}
                </View>

                <View style={styles.lovHeaderRow}>
                  <Text style={[styles.lovHeaderText, { flex: 1.5 }]}>Code</Text>
                  <Text style={[styles.lovHeaderText, { flex: 2.5 }]}>Name</Text>
                  <Text style={[styles.lovHeaderText, { flex: 1.2 }]}>Type</Text>
                  <Text style={[styles.lovHeaderText, { flex: 1.5 }]}>Mobile</Text>
                </View>

                {isLoadingLookup ? (
                  <View style={{ padding: 30, alignItems: 'center' }}>
                    <ActivityIndicator size="small" color={GOLD_DARK} />
                    <Text style={{ marginTop: 8, fontSize: 12, color: '#8F7E66' }}>Searching customers...</Text>
                  </View>
                ) : (
                  <FlatList
                    data={customerLookupList}
                    keyExtractor={(item, index) => item.CustomerCode || String(index)}
                    style={{ maxHeight: 320 }}
                    renderItem={({ item, index }) => (
                      <TouchableOpacity
                        style={[
                          styles.lovItemRow,
                          index % 2 === 1 && { backgroundColor: '#FCFAF6' }
                        ]}
                        activeOpacity={0.7}
                        onPress={() => handleSelectCustomer(item)}
                      >
                        <Text style={[styles.lovItemText, styles.codeCell, { flex: 1.5 }]}>
                          {item.CustomerCode}
                        </Text>
                        <Text style={[styles.lovItemText, { flex: 2.5 }]} numberOfLines={1}>
                          {item.CustomerName}
                        </Text>
                        <Text style={[styles.lovItemText, { flex: 1.2 }]}>
                          {item.CustomerType}
                        </Text>
                        <Text style={[styles.lovItemText, { flex: 1.5 }]}>
                          {item.Mobile || item.Phone || ''}
                        </Text>
                      </TouchableOpacity>
                    )}
                    ListEmptyComponent={
                      <View style={{ padding: 24, alignItems: 'center' }}>
                        <Text style={{ fontSize: 12, color: '#8F7E66' }}>No customers found</Text>
                      </View>
                    }
                  />
                )}
              </View>
            </View>
          </TouchableWithoutFeedback>
        </TouchableOpacity>
      </Modal>

      {/* Toast Notification */}
      {toastMessage ? (
        <View style={[styles.toast, toastType === 'success' ? styles.toastSuccess : styles.toastError]}>
          <Ionicons
            name={toastType === 'success' ? 'checkmark-circle' : 'alert-circle'}
            size={16}
            color="#FFFFFF"
            style={{ marginRight: 6 }}
          />
          <Text style={styles.toastText}>{toastMessage}</Text>
        </View>
      ) : null}
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: APP_BG,
  },
  header: {
    height: 48,
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    paddingHorizontal: 14,
    borderBottomWidth: 1,
    borderBottomColor: GOLD_BORDER,
    backgroundColor: CARD_BG,
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
  backButton: {
    padding: 6,
    marginRight: 8,
    borderRadius: 6,
    backgroundColor: '#F9F6F0',
    borderWidth: 1,
    borderColor: GOLD_BORDER,
  },
  title: {
    fontSize: 15,
    fontWeight: 'bold',
    color: TEXT_DARK,
    letterSpacing: 0.3,
    fontFamily: 'Cambria',
  },
  actionButton: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 12,
    paddingVertical: 5,
    borderRadius: 6,
    borderWidth: 1,
    borderColor: GOLD_ACCENT,
    backgroundColor: '#F9F2E2',
    height: 28,
  },
  actionButtonText: {
    color: GOLD_DARK,
    fontWeight: 'bold',
    fontSize: 12,
    fontFamily: 'Cambria',
  },
  scrollContent: {
    paddingHorizontal: 16,
    paddingTop: 16,
    paddingBottom: 40,
  },
  cardWrapper: {
    width: '100%',
  },
  goldCard: {
    backgroundColor: CARD_BG,
    borderRadius: 8,
    borderWidth: 1,
    borderColor: GOLD_BORDER,
    padding: 16,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.04,
    shadowRadius: 3,
    elevation: 2,
  },
  inputRow: {
    flexDirection: Platform.OS === 'web' ? 'row' : 'column',
    gap: 16,
    alignItems: Platform.OS === 'web' ? 'flex-start' : 'stretch',
    zIndex: 10,
  },
  reportCol: {
    flex: 1.5,
    position: 'relative',
    zIndex: 20,
  },
  customerCodeCol: {
    flex: 1,
    minWidth: 160,
  },
  customerCodeInputWrapper: {
    height: 36,
    borderWidth: 1,
    borderColor: '#D4C3A3',
    borderRadius: 6,
    backgroundColor: CARD_BG,
    flexDirection: 'row',
    alignItems: 'center',
    paddingLeft: 10,
    paddingRight: 4,
  },
  customerCodeInput: {
    flex: 1,
    fontSize: 12.5,
    color: TEXT_DARK,
    fontFamily: 'Cambria',
    height: '100%',
    padding: 0,
  },
  customerSearchBtn: {
    width: 26,
    height: 26,
    borderRadius: 4,
    backgroundColor: GOLD_DARK,
    justifyContent: 'center',
    alignItems: 'center',
    marginLeft: 4,
  },
  customerClearBtn: {
    padding: 4,
    marginLeft: 2,
  },
  fieldLabel: {
    fontSize: 12,
    fontWeight: 'bold',
    color: TEXT_DARK,
    marginBottom: 6,
    fontFamily: 'Cambria',
  },
  divider: {
    height: 1,
    backgroundColor: '#F5EFE6',
    marginVertical: 16,
  },
  buttonRow: {
    flexDirection: 'row',
    alignItems: 'center',
    flexWrap: 'wrap',
    gap: 12,
  },
  dateFilterRow: {
    flexDirection: 'row',
    alignItems: 'center',
    flexWrap: 'wrap',
    gap: 12,
  },
  radioBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingVertical: 6,
    paddingHorizontal: 16,
    borderRadius: 6,
    borderWidth: 1,
    borderColor: '#E2D3B3',
    backgroundColor: '#FAF7F0',
  },
  radioBtnSelected: {
    backgroundColor: '#FFF4DA',
    borderColor: GOLD_ACCENT,
  },
  radioCircle: {
    height: 15,
    width: 15,
    borderRadius: 8,
    borderWidth: 1.5,
    borderColor: '#C5B597',
    alignItems: 'center',
    justifyContent: 'center',
    marginRight: 6,
  },
  radioCircleSelected: {
    borderColor: GOLD_DARK,
  },
  radioDot: {
    height: 7,
    width: 7,
    borderRadius: 4,
    backgroundColor: GOLD_DARK,
  },
  radioLabel: {
    fontSize: 12,
    fontWeight: '500',
    color: '#5C4A28',
    fontFamily: 'Cambria',
  },
  radioLabelSelected: {
    color: GOLD_DARK,
    fontWeight: 'bold',
    fontFamily: 'Cambria',
  },
  filterSection: {
    borderTopWidth: 1,
    borderTopColor: '#F5EFE6',
    paddingTop: 12,
    marginTop: 10,
    marginBottom: 6,
  },
  filterCol: {
    flex: 1,
    minWidth: 160,
  },
  filterRadioGroupRow: {
    flexDirection: Platform.OS === 'web' ? 'row' : 'column',
    gap: 20,
    alignItems: Platform.OS === 'web' ? 'flex-start' : 'stretch',
  },
  radioSubGroup: {
    flex: 1,
  },
  radioSubGroupItems: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    gap: 8,
  },
  toast: {
    position: 'absolute',
    top: 54,
    alignSelf: 'center',
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 16,
    paddingVertical: 8,
    borderRadius: 6,
    elevation: 4,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.15,
    shadowRadius: 4,
    zIndex: 9999,
  },
  toastSuccess: {
    backgroundColor: '#2E7D32',
  },
  toastError: {
    backgroundColor: '#C62828',
  },
  toastText: {
    color: '#FFFFFF',
    fontSize: 12,
    fontWeight: '600',
    fontFamily: 'Cambria',
  },

  // Modal Styles
  modalOverlay: {
    flex: 1,
    backgroundColor: 'rgba(59, 45, 23, 0.45)',
    justifyContent: 'center',
    alignItems: 'center',
    padding: 16,
  },
  lovModalContainer: {
    width: Platform.OS === 'web' ? 620 : '95%',
    backgroundColor: CARD_BG,
    borderRadius: 10,
    borderWidth: 1,
    borderColor: GOLD_BORDER,
    overflow: 'hidden',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.15,
    shadowRadius: 10,
    elevation: 10,
  },
  modalHeader: {
    height: 44,
    backgroundColor: '#F5E7CA',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingHorizontal: 14,
  },
  headerIconBadgeSmall: {
    width: 24,
    height: 24,
    borderRadius: 5,
    backgroundColor: '#FFF8EA',
    borderWidth: 1,
    borderColor: '#E2D2AF',
    justifyContent: 'center',
    alignItems: 'center',
  },
  modalTitle: {
    fontSize: 14,
    fontWeight: '700',
    color: TEXT_DARK,
    fontFamily: 'Cambria',
  },
  modalCloseBtn: {
    width: 28,
    height: 28,
    borderRadius: 14,
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: 'rgba(59, 45, 23, 0.08)',
  },
  modalSearchBox: {
    height: 34,
    borderWidth: 1,
    borderColor: '#DFD5C3',
    borderRadius: 6,
    backgroundColor: '#FFFFFF',
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 10,
    marginBottom: 10,
  },
  modalSearchInput: {
    flex: 1,
    fontSize: 12,
    color: TEXT_DARK,
    fontFamily: 'Cambria',
    height: '100%',
    padding: 0,
  },
  lovHeaderRow: {
    flexDirection: 'row',
    backgroundColor: '#F5E7CA',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    paddingVertical: 7,
    paddingHorizontal: 10,
  },
  lovHeaderText: {
    fontSize: 11,
    fontWeight: '700',
    color: TEXT_DARK,
    fontFamily: 'Cambria',
  },
  lovItemRow: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingVertical: 8,
    paddingHorizontal: 10,
    borderBottomWidth: 1,
    borderBottomColor: '#F0E6D2',
    backgroundColor: '#FFFFFF',
  },
  lovItemText: {
    fontSize: 12,
    color: '#332717',
    fontFamily: 'Cambria',
  },
  codeCell: {
    fontWeight: '700',
    color: GOLD_DARK,
  },
});
