import { formatColumnHeader } from '@/utils/formatColumnHeader';
import { API_BASE_URL } from '@/constants/api';
import { downloadExcelReport, downloadPdfReport, printHtmlReport } from '@/utils/reportExportClient';
import { ReportDropdown } from '@/components/ReportDropdown';
import { ReportDatePicker } from '@/components/ReportDatePicker';
import { validateReportDateRange, getTodayIso, toDispDate, getCurrentYearStartIso } from '@/utils/dateValidation';
import { Ionicons } from '@expo/vector-icons';
import { Stack, useRouter } from 'expo-router';
import React, { useEffect, useState, useMemo } from 'react';
import {
  ActivityIndicator,
  Platform,
  ScrollView,
  StyleSheet,
  Text,
  TouchableOpacity,
  View,
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';
import DateTimePicker from '@react-native-community/datetimepicker';

// Royal Gold Design Palette (matching Customer Reports)
const GOLD_ACCENT = '#B48218';
const GOLD_DARK = '#9E6D08';
const GOLD_BORDER = '#E8DCB8';
const CARD_BG = '#FFFFFF';
const APP_BG = '#FBF9F5';
const TEXT_DARK = '#3B2D17';

interface ManagementReportOption {
  id: string;
  category: string;
  label: string;
  apiKey: string;
  description: string;
  supportsSalesman?: boolean;
  supportsSupplier?: boolean;
}

const REPORT_OPTIONS: ManagementReportOption[] = [
  { id: 'sales_profit_date', category: 'Sales', label: 'Sales Profit Report By Date', apiKey: 'sales-profit-by-date', description: 'Daily sales revenue, purchase costs, and gross margins' },
  { id: 'sales_profit_salesman', category: 'Sales', label: 'Sales Profit Report By Sales Man', apiKey: 'sales-profit-by-salesman', description: 'Sales performance and gross profit by representative', supportsSalesman: true },
  { id: 'gst_input_output', category: 'Taxation', label: 'GST Input and Output with Profit', apiKey: 'gst-input-output', description: 'GST input tax, output tax, and overall net margins' },
  { id: 'day_sales_history', category: 'Sales', label: 'Day Sales History', apiKey: 'day-sales-history', description: 'Historical daily sales breakdown and invoice summaries' },
  { id: 'standard_rated_suppliers', category: 'Supplier', label: 'Standard Rated Suppliers', apiKey: 'standard-rated-suppliers', description: 'Standard rated GST vendor purchases and invoice totals', supportsSupplier: true },
  { id: 'zero_rated_suppliers', category: 'Supplier', label: 'Zero Rated Suppliers', apiKey: 'zero-rated-suppliers', description: 'Zero rated tax exempt vendor transactions and details', supportsSupplier: true },
  { id: 'normal_sales_profit', category: 'Sales', label: 'Normal Sales Profit Report', apiKey: 'normal-sales-profit', description: 'Standard sales ledger with net margin calculation' },
  { id: 'tradein_issue', category: 'Trade-In', label: 'Trade In Issue', apiKey: 'tradein-issue', description: 'Inventory movement and issues of traded-in gold' },
  { id: 'tradein_sales_profit', category: 'Trade-In', label: 'TradeIn Sales Profit Report', apiKey: 'tradein-sales-profit', description: 'Margin analysis on customer trade-in transactions' },
  { id: 'monthly_sales_salesman', category: 'Sales', label: 'Monthly Sales by Salesman', apiKey: 'monthly-sales-by-salesman', description: 'Monthly turnover and achievement by sales representatives', supportsSalesman: true },
  { id: 'monthly_stock_report', category: 'Stock', label: 'Monthly Stock Report', apiKey: 'monthly-stock-report', description: 'Beginning, movement, and ending monthly stock totals' },
  { id: 'monthly_stock_turnover', category: 'Stock', label: 'Monthly Stock Turn Over', apiKey: 'monthly-stock-turnover', description: 'Inventory turnover ratio and monthly sales stock velocity' },
  { id: 'daily_stock_value', category: 'Stock', label: 'Stock Value', apiKey: 'daily-stock-value', description: 'Current valuation and total cost of inventory stock' },
  { id: 'tradein_stock_value', category: 'Trade-In', label: 'Trade In Stock Value', apiKey: 'tradein-stock-value', description: 'Valuation and inventory balances of traded gold' },
  { id: 'sales_tradein_profit', category: 'Profit', label: 'Sales and TradeIn Profit Report', apiKey: 'sales-tradein-profit', description: 'Combined sales and trade-in gross margins' },
  { id: 'user_log_reports', category: 'Audit', label: 'User Log Reports', apiKey: 'user-log-reports', description: 'System user access history, login sessions, and audit trail' },
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

const formatDisp = (iso: string) => {
  if (!iso) return '';
  const parts = iso.split('-');
  if (parts.length === 3) return `${parts[2]} - ${parts[1]} - ${parts[0]}`;
  return iso;
};

const parseLocalDate = (dateStr: string) => {
  if (!dateStr) return new Date();
  const parts = dateStr.split('-');
  if (parts.length !== 3) return new Date();
  const [y, m, d] = parts;
  return new Date(parseInt(y, 10), parseInt(m, 10) - 1, parseInt(d, 10));
};

export default function ManagementReportsScreen() {
  const router = useRouter();

  const [toastMessage, setToastMessage] = useState('');
  const [toastType, setToastType] = useState<'success' | 'error'>('success');

  const [selectedReport, setSelectedReport] = useState<string>('sales_profit_date');
  const [isReportDropdownOpen, setIsReportDropdownOpen] = useState(false);
  const [actionOption, setActionOption] = useState<ActionOption>('Preview');
  const [reportType, setReportType] = useState<'Summary' | 'Detail'>('Summary');

  // Filter values
  const [locationCode, setLocationCode] = useState<string>('ALL');
  const [isLocOpen, setIsLocOpen] = useState(false);
  const [locations, setLocations] = useState<{ id: string; label: string }[]>([{ id: 'ALL', label: 'ALL - All Locations' }]);

  const closeAllDropdowns = () => {
    setIsReportDropdownOpen(false);
    setIsLocOpen(false);
  };

  useEffect(() => {
    (async () => {
      try {
        const locRes = await fetch(`${API_BASE_URL}/salesreports/locations`).then(r => r.json()).catch(() => ({ data: [] }));
        if (locRes?.data) setLocations(locRes.data.map((x: any) => ({ id: x.LocationCode, label: x.LocationName || x.LocationCode })));
      } catch (e) {
        console.warn('Failed loading locations:', e);
      }
    })();
  }, []);

  const [fromDate, setFromDate] = useState(formatDateLocal(new Date()));
  const [toDate, setToDate] = useState(formatDateLocal(new Date()));
  const [isLoading, setIsLoading] = useState(false);

  const [showDatePicker, setShowDatePicker] = useState<{ show: boolean; mode: 'from' | 'to' }>({
    show: false,
    mode: 'from',
  });

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

  const selectedReportObj = useMemo(() => {
    return REPORT_OPTIONS.find((r) => r.id === selectedReport) || REPORT_OPTIONS[0];
  }, [selectedReport]);

  const handleGetReport = async (overrideAction?: ActionOption) => {
    const currentAction = overrideAction || actionOption;

    if (!selectedReport) {
      showError('Please select a Management Report.');
      return;
    }

    if (!fromDate || !toDate) {
      showError('Please select valid From Date and To Date.');
      return;
    }

    const valRes = validateReportDateRange(fromDate, toDate);
    if (!valRes.isValid) {
      showError(valRes.errorMessage || 'Invalid date range.');
      return;
    }

    setIsLoading(true);

    try {
      const repObj = selectedReportObj;
      const params = new URLSearchParams({
        fromDate,
        toDate,
        locationCode: locationCode || 'ALL',
        reportType,
      });

      const res = await fetch(`${API_BASE_URL}/managementreports/${repObj.apiKey}?${params.toString()}`);
      const json = await res.json();

      if (!json.success || !json.data || json.data.length === 0) {
        showError(json.message || 'No records found for the selected criteria.');
        setIsLoading(false);
        return;
      }

      const repTitle = json.title || json.reportTitle || repObj.label;
      const rawCols = json.columns || Object.keys(json.data[0]).map((k) => ({ key: k, label: k }));
      const columns = rawCols.map((c: any) => ({ ...c, label: formatColumnHeader(c.label || c.key) }));
      const cleanName = repTitle.replace(/[^a-zA-Z0-9_-]/g, '_');

            const repTotals = json.summary || {};

      if (currentAction === 'Preview') {
        await printHtmlReport(
          {
            endpoint: 'managementreports',
            title: repTitle,
            columns,
            data: json.data,
            totals: repTotals,
            fromDate,
            toDate,
          },
          false
        );
        showSuccess('Report preview opened.');
      } else if (currentAction === 'Print') {
        await printHtmlReport(
          {
            endpoint: 'managementreports',
            title: repTitle,
            columns,
            data: json.data,
            totals: repTotals,
            fromDate,
            toDate,
          },
          true
        );
        showSuccess('Print dialog opened.');
      } else if (currentAction === 'PDF') {
        const ok = await downloadPdfReport({
          endpoint: 'managementreports',
          title: repTitle,
          columns,
          data: json.data,
          totals: repTotals,
          fromDate,
          toDate,
        });
        if (ok) showSuccess('PDF downloaded successfully!');
      } else if (currentAction === 'Excel') {
        const ok = await downloadExcelReport({
          endpoint: 'managementreports',
          title: repTitle,
          columns,
          data: json.data,
          totals: repTotals,
          fromDate,
          toDate,
        });
        if (ok) showSuccess('Excel file downloaded successfully!');
      }
} catch (err: any) {
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
  }, [selectedReport, fromDate, toDate, actionOption, isReportDropdownOpen, reportType]);

  return (
    <SafeAreaView style={styles.container} edges={['top']}>
      <Stack.Screen options={{ headerShown: false }} />

      {/* HEADER */}
      <View style={styles.header}>
        <View style={styles.headerLeft}>
          <TouchableOpacity onPress={() => router.back()} style={styles.backButton} activeOpacity={0.7}>
            <Ionicons name="arrow-back" size={20} color={TEXT_DARK} />
          </TouchableOpacity>
          <Text style={styles.title}>Management Reports</Text>
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
            {/* Top row: Management Report, From Date, To Date */}
            <View style={styles.inputRow}>
              {/* Management Report */}
              <View style={styles.reportCol}>
                <ReportDropdown
                  label="Management Report"
                  options={REPORT_OPTIONS}
                  selectedId={selectedReport}
                  onSelect={(id) => {
                    setSelectedReport(id);
                    setIsReportDropdownOpen(false);
                  }}
                  isOpen={isReportDropdownOpen}
                  onToggle={() => setIsReportDropdownOpen(!isReportDropdownOpen)}
                  maxHeight={280}
                />
              </View>

              {/* From Date */}
              <ReportDatePicker
                label="From Date"
                value={fromDate}
                onChange={setFromDate}
                isIsoOutput={true}
                onOpenNative={() => setShowDatePicker({ show: true, mode: 'from' })}
              />

              {/* To Date */}
              <ReportDatePicker
                label="To Date"
                value={toDate}
                onChange={setToDate}
                isIsoOutput={true}
                onOpenNative={() => setShowDatePicker({ show: true, mode: 'to' })}
              />
            </View>

            {/* RESTORED MANAGEMENT FILTER OPTIONS MATCHING CUSTOMER REPORTS */}
            <View style={styles.filterSection}>
              <View style={[styles.inputRow, { marginTop: 12, zIndex: 15 }]}>
                {/* Location Filter */}
                <View style={styles.filterCol}>
                  <ReportDropdown
                    label="Location"
                    options={locations}
                    selectedId={locationCode}
                    onSelect={(id) => {
                      setLocationCode(id);
                      setIsLocOpen(false);
                    }}
                    isOpen={isLocOpen}
                    onToggle={() => {
                      closeAllDropdowns();
                      setIsLocOpen(!isLocOpen);
                    }}
                    maxHeight={200}
                  />
                </View>
                {/* Spacer column to match Salesman/Supplier dropdown width */}
                <View style={styles.filterCol} />
              </View>
            </View>

            {/* Optional Summary / Detail RadioButtons */}
            <View style={{ flexDirection: 'row', gap: 12, marginTop: 14 }}>
              <RadioButton
                label="Summary"
                selected={reportType === 'Summary'}
                onPress={() => setReportType('Summary')}
              />
              <RadioButton
                label="Detail"
                selected={reportType === 'Detail'}
                onPress={() => setReportType('Detail')}
              />
            </View>

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
  dateCol: {
    flex: 1,
  },
  fieldLabel: {
    fontSize: 12,
    fontWeight: 'bold',
    color: TEXT_DARK,
    marginBottom: 6,
    fontFamily: 'Cambria',
  },
  inputBox: {
    height: 36,
    borderWidth: 1,
    borderColor: '#D4C3A3',
    borderRadius: 6,
    backgroundColor: CARD_BG,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingHorizontal: 12,
  },
  inputBoxActive: {
    borderColor: GOLD_ACCENT,
  },
  dropdownText: {
    fontSize: 12.5,
    fontWeight: '500',
    color: TEXT_DARK,
    flex: 1,
    marginRight: 6,
    fontFamily: 'Cambria',
  },
  dropdownPlaceholder: {
    color: '#9E8D77',
  },
  dropdownMenu: {
    position: 'absolute',
    top: 64,
    left: 0,
    right: 0,
    backgroundColor: CARD_BG,
    borderRadius: 6,
    borderWidth: 1,
    borderColor: '#D4C3A3',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.15,
    shadowRadius: 8,
    elevation: 9999,
    zIndex: 99999,
  },
  menuItem: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingVertical: 9,
    paddingHorizontal: 12,
    borderBottomWidth: 1,
    borderBottomColor: '#F5EFE6',
  },
  menuItemSelected: {
    backgroundColor: '#FFF4DA',
  },
  menuItemText: {
    fontSize: 12.5,
    color: TEXT_DARK,
    fontFamily: 'Cambria',
  },
  menuItemTextSelected: {
    color: GOLD_DARK,
    fontWeight: 'bold',
    fontFamily: 'Cambria',
  },
  webDateWrapper: {
    height: 36,
    borderWidth: 1,
    borderColor: '#D4C3A3',
    borderRadius: 6,
    backgroundColor: CARD_BG,
    flexDirection: 'row',
    alignItems: 'center',
    overflow: 'hidden',
  },
  webDateInput: {
    flex: 1,
    height: '100%',
    border: 'none',
    backgroundColor: 'transparent',
    outline: 'none',
    color: TEXT_DARK,
    paddingLeft: 12,
    paddingRight: 8,
    fontSize: 12.5,
    fontFamily: 'Cambria',
    cursor: 'pointer',
  } as any,
  dateText: {
    fontSize: 12.5,
    color: TEXT_DARK,
    fontFamily: 'Cambria',
  },
  dateIconBox: {
    paddingRight: 10,
    justifyContent: 'center',
    alignItems: 'center',
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
});
