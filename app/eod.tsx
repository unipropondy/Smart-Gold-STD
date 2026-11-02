import { formatColumnHeader } from '@/utils/formatColumnHeader';
import { API_BASE_URL } from '@/constants/api';
import { downloadExcelReport, downloadPdfReport, printHtmlReport } from '@/utils/reportExportClient';
import { ReportDropdown } from '@/components/ReportDropdown';
import { ReportDatePicker } from '@/components/ReportDatePicker';
import { validateReportDateRange, getTodayIso, toDispDate } from '@/utils/dateValidation';
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

interface EODReportItem {
  id: string;
  label: string;
  key: string;
}

const EOD_REPORT_CONFIG: EODReportItem[] = [
  { id: '0', label: 'Sales Detail Journal', key: 'sales_detail_journal' },
  { id: '1', label: 'Salesman Wise Journal', key: 'salesman_wise_journal' },
  { id: '2', label: 'Terminal Wise Journal', key: 'terminal_wise_journal' },
  { id: '3', label: 'Refund Journal', key: 'refund_journal' },
  { id: '4', label: 'Trade In Journal', key: 'trade_in_journal' },
  { id: '5', label: 'Installment Journal', key: 'installment_journal' },
  { id: '6', label: 'Closing Stock Report (Pieces) by Category', key: 'closing_stock_pieces_category' },
  { id: '7', label: 'Closing Stock Report (Weight) by Category', key: 'closing_stock_weight_category' },
  { id: '8', label: 'Closing Stock Report (Pieces) by Tray', key: 'closing_stock_pieces_tray' },
  { id: '9', label: 'Below Cost Sales Report', key: 'below_cost_sales_report' },
  { id: '10', label: 'Closed Bills Listing', key: 'closed_bills_listing' },
  { id: '11', label: 'Purchase Return Journal', key: 'purchase_return_journal' },
  { id: '12', label: 'Gold Bar Purchase', key: 'gold_bar_purchase' },
  { id: '13', label: 'Miscellaneous Journal', key: 'miscellaneous_journal' },
  { id: '14', label: 'Normal Sales Profit', key: 'normal_sales_profit' },
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

export default function EODReportsScreen() {
  const router = useRouter();

  const [toastMessage, setToastMessage] = useState('');
  const [toastType, setToastType] = useState<'success' | 'error'>('success');

  const [selectedReport, setSelectedReport] = useState<string>('0');
  const [isReportDropdownOpen, setIsReportDropdownOpen] = useState(false);
  const [actionOption, setActionOption] = useState<ActionOption>('Preview');

  const [eodDate, setEODDate] = useState('2025-01-01');
  const [isLoading, setIsLoading] = useState(false);

  const [showDatePicker, setShowDatePicker] = useState(false);

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

  const loadInitialConfig = async () => {
    try {
      const res = await fetch(`${API_BASE_URL}/eod/config`);
      const json = await res.json();
      if (json.success && json.data?.lastEODDate) {
        setEODDate(json.data.lastEODDate);
      }
    } catch (e) {
      console.warn('Initial EOD config error:', e);
    }
  };

  useEffect(() => {
    loadInitialConfig();
  }, []);

  const handleDateChange = (event: any, selectedDate?: Date) => {
    setShowDatePicker(false);
    if (selectedDate) {
      setEODDate(formatDateLocal(selectedDate));
    }
  };

  const selectedReportObj = useMemo(() => {
    return EOD_REPORT_CONFIG.find((r) => r.id === selectedReport);
  }, [selectedReport]);

  const handleGetReport = async (overrideAction?: ActionOption) => {
    const currentAction = overrideAction || actionOption;

    if (!selectedReport) {
      showError('Please select a Dayend Report.');
      return;
    }

    if (!eodDate) {
      showError('Please select valid Dayend Date.');
      return;
    }

    const valRes = validateReportDateRange(eodDate, eodDate);
    if (!valRes.isValid) {
      showError('Dayend Date cannot be in the future. Please select a date on or before today.');
      return;
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
      const res = await fetch(`${API_BASE_URL}/eod/reports/generate`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          reportId: selectedReport,
          date: eodDate,
        }),
      });

      const json = await res.json();

      if (!json.success || !json.data || json.data.length === 0) {
        if (win) win.close();
        showError(json.message || 'No records found for the selected criteria.');
        setIsLoading(false);
        return;
      }

      const repTitle = json.title || (selectedReportObj ? selectedReportObj.label : 'Dayend Report');
      const rawCols = json.columns || Object.keys(json.data[0]).map((k) => ({ key: k, label: k }));
      const columns = rawCols.map((c: any) => ({ ...c, label: formatColumnHeader(c.label || c.key) }));
      const cleanName = repTitle.replace(/[^a-zA-Z0-9_-]/g, '_');

      // 1. PDF DOWNLOAD ACTION
      if (currentAction === 'PDF') {
        const pdfRes = await fetch(`${API_BASE_URL}/eod/export-pdf`, {
          method: 'POST',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify({
            title: repTitle,
            columns,
            data: json.data,
            totals: json.totals || {},
            fromDate: eodDate,
            toDate: eodDate,
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
        const excelRes = await fetch(`${API_BASE_URL}/eod/export-excel`, {
          method: 'POST',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify({
            title: repTitle,
            columns,
            data: json.data,
            totals: json.totals || {},
            fromDate: eodDate,
            toDate: eodDate,
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
      const htmlRes = await fetch(`${API_BASE_URL}/eod/download-html`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          title: repTitle,
          columns,
          data: json.data,
          totals: json.totals || {},
          fromDate: eodDate,
          toDate: eodDate,
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
  }, [selectedReport, eodDate, actionOption, isReportDropdownOpen]);

  return (
    <SafeAreaView style={styles.container} edges={['top']}>
      <Stack.Screen options={{ headerShown: false }} />

      {/* HEADER */}
      <View style={styles.header}>
        <View style={styles.headerLeft}>
          <TouchableOpacity onPress={() => router.back()} style={styles.backButton} activeOpacity={0.7}>
            <Ionicons name="arrow-back" size={20} color={TEXT_DARK} />
          </TouchableOpacity>
          <Text style={styles.title}>Dayend Report</Text>
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
            {/* Top row: Dayend Report, Dayend Date */}
            <View style={styles.inputRow}>
              {/* Dayend Report */}
              <View style={styles.reportCol}>
                <ReportDropdown
                  label="Dayend Report"
                  options={EOD_REPORT_CONFIG}
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

              {/* Dayend Date */}
              <ReportDatePicker
                label="Dayend Date"
                value={eodDate}
                onChange={setEODDate}
                isIsoOutput={true}
                onOpenNative={() => setShowDatePicker(true)}
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

        {showDatePicker && (
          <DateTimePicker
            value={parseLocalDate(eodDate)}
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
