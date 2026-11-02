import { API_BASE_URL } from '@/constants/api';
import { downloadExcelReport, downloadPdfReport, printHtmlReport } from '@/utils/reportExportClient';
import { formatColumnHeader } from '@/utils/formatColumnHeader';
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
  TextInput,
  TouchableOpacity,
  View,
  Alert,
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';
import DateTimePicker from '@react-native-community/datetimepicker';

// Royal Gold Design Palette (matching Smart Gold Report Theme)
const GOLD_ACCENT = '#B48218';
const GOLD_DARK = '#9E6D08';
const GOLD_BORDER = '#E8DCB8';
const CARD_BG = '#FFFFFF';
const APP_BG = '#FBF9F5';
const TEXT_DARK = '#3B2D17';

type StockTakeMode = 'all' | 'scanned' | 'unscanned';
type ActionOption = 'Preview' | 'Print' | 'PDF' | 'Excel';

interface TrayItem {
  TrayID: string;
  TrayName: string;
}

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

export default function StockTakeReportsScreen() {
  const router = useRouter();

  // Mode: All (All items), Scanned (Trayid <> 'X'), Unscanned (Trayid = 'X')
  const [mode, setMode] = useState<StockTakeMode>('all');
  const [trayId, setTrayId] = useState<string>('');
  const [trayList, setTrayList] = useState<TrayItem[]>([]);
  const [isTrayDropdownOpen, setIsTrayDropdownOpen] = useState<boolean>(false);

  // Date filters
  const [fromDate, setFromDate] = useState<string>(getTodayIso());
  const [toDate, setToDate] = useState<string>(getTodayIso());

  // Native Date Picker State
  const [showDatePicker, setShowDatePicker] = useState<{
    show: boolean;
    mode: 'from' | 'to';
  }>({ show: false, mode: 'from' });

  // Action State: Preview, Print, PDF, Excel
  const [actionOption, setActionOption] = useState<ActionOption>('Preview');

  // Loading & Results
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [toastMessage, setToastMessage] = useState<string>('');
  const [toastType, setToastType] = useState<'success' | 'error'>('success');

  // Query Result for on-screen viewing / table summary
  const [reportResult, setReportResult] = useState<{
    title: string;
    columns: any[];
    data: any[];
    totals: any;
  } | null>(null);

  // Load available trays from backend
  useEffect(() => {
    let isMounted = true;
    async function loadTrays() {
      try {
        const res = await fetch(`${API_BASE_URL}/stocktake/trays`);
        const json = await res.json();
        if (isMounted && json.success && Array.isArray(json.data)) {
          setTrayList(json.data);
        }
      } catch (e) {
        console.warn('Failed to load tray list for stock take:', e);
      }
    }
    loadTrays();
    return () => {
      isMounted = false;
    };
  }, []);

  const showSuccess = (msg: string) => {
    setToastType('success');
    setToastMessage(msg);
    setTimeout(() => setToastMessage(''), 4000);
  };

  const showError = (msg: string) => {
    setToastType('error');
    setToastMessage(msg);
    setTimeout(() => setToastMessage(''), 5000);
  };

  const handleDateChange = (event: any, selectedDate?: Date) => {
    setShowDatePicker({ show: false, mode: 'from' });
    if (event.type === 'dismissed' || !selectedDate) return;

    const y = selectedDate.getFullYear();
    const m = String(selectedDate.getMonth() + 1).padStart(2, '0');
    const d = String(selectedDate.getDate()).padStart(2, '0');
    const iso = `${y}-${m}-${d}`;

    if (showDatePicker.mode === 'from') {
      setFromDate(iso);
    } else {
      setToDate(iso);
    }
  };

  const parseLocalDate = (dateStr: string) => {
    if (!dateStr) return new Date();
    const parts = dateStr.split('-');
    if (parts.length === 3) {
      return new Date(parseInt(parts[0], 10), parseInt(parts[1], 10) - 1, parseInt(parts[2], 10));
    }
    return new Date();
  };

  // Reset / Clear Filters
  const handleClear = () => {
    setMode('all');
    setTrayId('');
    setFromDate(getTodayIso());
    setToDate(getTodayIso());
    setActionOption('Preview');
    setReportResult(null);
    setToastMessage('');
    setIsTrayDropdownOpen(false);
  };

  // Execute Stock Take Report (Find / Get Report)
  const handleGetReport = async (overrideAction?: ActionOption) => {
    const currentAction = overrideAction || actionOption;

    if (!fromDate || !toDate) {
      showError('Please enter valid data for Date!');
      return;
    }

    const valRes = validateReportDateRange(fromDate, toDate);
    if (!valRes.isValid) {
      showError(valRes.errorMessage || 'Enter valid data for Date!');
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
      const res = await fetch(`${API_BASE_URL}/stocktake/report`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          fromDate,
          toDate,
          mode,
          trayId: trayId.trim(),
        }),
      });

      const json = await res.json();

      if (!json.success || !json.data || json.data.length === 0) {
        if (win) win.close();
        showError(json.message || 'No records found for the selected criteria.');
        setReportResult(null);
        setIsLoading(false);
        return;
      }

      const repTitle = json.title || 'Stock Take Report';
      const rawCols = json.columns || Object.keys(json.data[0]).map((k) => ({ key: k, label: k }));
      const columns = rawCols.map((c: any) => ({
        ...c,
        label: formatColumnHeader(c.label || c.key),
      }));
      const cleanName = repTitle.replace(/[^a-zA-Z0-9_-]/g, '_');

      // Update state for on-screen summary table
      setReportResult({
        title: repTitle,
        columns,
        data: json.data,
        totals: json.totals || {},
      });

      // 1. PDF DOWNLOAD ACTION
      if (currentAction === 'PDF') {
        const pdfRes = await fetch(`${API_BASE_URL}/stocktake/export-pdf`, {
          method: 'POST',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify({
            title: repTitle,
            columns,
            data: json.data,
            totals: json.totals || {},
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
        const excelRes = await fetch(`${API_BASE_URL}/stocktake/export-excel`, {
          method: 'POST',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify({
            title: repTitle,
            columns,
            data: json.data,
            totals: json.totals || {},
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
      const htmlRes = await fetch(`${API_BASE_URL}/stocktake/download-html`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          title: repTitle,
          columns,
          data: json.data,
          totals: json.totals || {},
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
            link.setAttribute('download', `${cleanName}.html`);
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

  // Keyboard Shortcuts (F7: Get Report, Esc: Close)
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
          router.back();
        }
      };
      window.addEventListener('keydown', handleKeyDown);
      return () => window.removeEventListener('keydown', handleKeyDown);
    }
  }, [mode, trayId, fromDate, toDate, actionOption]);

  const trayOptions = useMemo(() => {
    return [
      { id: '', label: 'ALL TRAYS' },
      ...trayList.map((t) => ({ id: t.TrayID, label: `${t.TrayID} - ${t.TrayName}` })),
    ];
  }, [trayList]);

  return (
    <SafeAreaView style={styles.container} edges={['top']}>
      <Stack.Screen options={{ headerShown: false }} />

      {/* HEADER */}
      <View style={styles.header}>
        <View style={styles.headerLeft}>
          <TouchableOpacity onPress={() => router.back()} style={styles.backButton} activeOpacity={0.7}>
            <Ionicons name="arrow-back" size={20} color={TEXT_DARK} />
          </TouchableOpacity>
          <Text style={styles.title}>Stock Take Report</Text>
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
            {/* Top row: Select Option (Report Mode), Tray ID, Date Range */}
            <View style={styles.inputRow}>
              {/* Report Mode (All, Scanned, UnScanned) */}
              <View style={[styles.reportCol, { flex: 1.4 }]}>
                <Text style={styles.fieldLabel}>Select Option</Text>
                <View style={{ flexDirection: 'row', gap: 8, marginTop: 4, flexWrap: 'wrap' }}>
                  <RadioButton
                    label="All"
                    selected={mode === 'all'}
                    onPress={() => setMode('all')}
                  />
                  <RadioButton
                    label="Scanned"
                    selected={mode === 'scanned'}
                    onPress={() => setMode('scanned')}
                  />
                  <RadioButton
                    label="UnScanned"
                    selected={mode === 'unscanned'}
                    onPress={() => setMode('unscanned')}
                  />
                </View>
              </View>

              {/* Tray ID Filter */}
              <View style={[styles.reportCol, { flex: 1 }]}>
                <ReportDropdown
                  label="Tray ID (Optional)"
                  options={trayOptions}
                  selectedId={trayId}
                  onSelect={(id) => {
                    setTrayId(id);
                    setIsTrayDropdownOpen(false);
                  }}
                  isOpen={isTrayDropdownOpen}
                  onToggle={() => setIsTrayDropdownOpen(!isTrayDropdownOpen)}
                  maxHeight={260}
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

        {/* SUMMARY CARDS IF RESULTS ARE RETURNED */}
        {reportResult && reportResult.totals && (
          <View style={styles.summaryContainer}>
            <View style={styles.summaryCard}>
              <Text style={styles.summaryCardLabel}>TOTAL RECORDS</Text>
              <Text style={styles.summaryCardVal}>{reportResult.totals.totalRecords || 0}</Text>
            </View>
            <View style={styles.summaryCard}>
              <Text style={styles.summaryCardLabel}>TOTAL QTY (PCS)</Text>
              <Text style={styles.summaryCardVal}>{reportResult.totals.totalPcs || 0}</Text>
            </View>
            <View style={styles.summaryCard}>
              <Text style={styles.summaryCardLabel}>TOTAL WT. (G)</Text>
              <Text style={styles.summaryCardVal}>{Number(reportResult.totals.totalWeight || 0).toFixed(3)}</Text>
            </View>
            <View style={styles.summaryCard}>
              <Text style={styles.summaryCardLabel}>TOTAL VALUATION</Text>
              <Text style={[styles.summaryCardVal, { color: GOLD_DARK }]}>
                ${Number(reportResult.totals.totalCost || 0).toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 })}
              </Text>
            </View>
          </View>
        )}

        {/* ON-SCREEN TABLE PREVIEW */}
        {reportResult && reportResult.data && reportResult.data.length > 0 && (
          <View style={styles.tableCard}>
            <View style={styles.tableCardHeader}>
              <Text style={styles.tableCardTitle}>{reportResult.title}</Text>
              <Text style={styles.tableCardSubtitle}>{reportResult.data.length} Records Found</Text>
            </View>

            <ScrollView horizontal showsHorizontalScrollIndicator={true}>
              <View>
                {/* TABLE HEADER */}
                <View style={styles.tableHeaderRow}>
                  <View style={[styles.tableHeaderCell, { width: 50 }]}>
                    <Text style={styles.tableHeaderText}>#</Text>
                  </View>
                  {reportResult.columns.map((col: any) => (
                    <View
                      key={col.key}
                      style={[
                        styles.tableHeaderCell,
                        { width: col.width || 120 },
                        col.align === 'right' && { alignItems: 'flex-end' },
                        col.align === 'center' && { alignItems: 'center' },
                      ]}
                    >
                      <Text style={styles.tableHeaderText}>{col.label}</Text>
                    </View>
                  ))}
                </View>

                {/* TABLE BODY */}
                {reportResult.data.map((row: any, idx: number) => {
                  const isEven = idx % 2 === 0;
                  return (
                    <View
                      key={`row_${idx}`}
                      style={[styles.tableBodyRow, isEven ? styles.tableRowEven : styles.tableRowOdd]}
                    >
                      <View style={[styles.tableBodyCell, { width: 50, alignItems: 'center' }]}>
                        <Text style={styles.tableIndexText}>{idx + 1}</Text>
                      </View>
                      {reportResult.columns.map((col: any) => {
                        let val = row[col.key];
                        if (col.format === 'currency') {
                          val = `$ ${Number(val || 0).toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 })}`;
                        } else if (col.format === 'decimal3') {
                          val = Number(val || 0).toFixed(3);
                        } else if (col.format === 'date') {
                          val = val ? new Date(val).toLocaleDateString('en-GB') : '';
                        }

                        return (
                          <View
                            key={col.key}
                            style={[
                              styles.tableBodyCell,
                              { width: col.width || 120 },
                              col.align === 'right' && { alignItems: 'flex-end' },
                              col.align === 'center' && { alignItems: 'center' },
                            ]}
                          >
                            <Text
                              style={[
                                styles.tableCellText,
                                col.key === 'Status' && (val === 'Scanned' ? styles.statusScanned : styles.statusUnscanned),
                              ]}
                            >
                              {val !== undefined && val !== null ? String(val) : ''}
                            </Text>
                          </View>
                        );
                      })}
                    </View>
                  );
                })}
              </View>
            </ScrollView>
          </View>
        )}

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
            color={toastType === 'success' ? '#15803D' : '#DC2626'}
            style={{ marginRight: 8 }}
          />
          <Text style={[styles.toastText, toastType === 'success' ? styles.toastTextSuccess : styles.toastTextError]}>
            {toastMessage}
          </Text>
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
    height: 52,
    backgroundColor: CARD_BG,
    borderBottomWidth: 1,
    borderBottomColor: GOLD_BORDER,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingHorizontal: 16,
    zIndex: 50,
  },
  headerLeft: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  backButton: {
    marginRight: 12,
    padding: 6,
    borderRadius: 6,
  },
  title: {
    fontSize: 16,
    fontWeight: '700',
    color: TEXT_DARK,
    fontFamily: 'Cambria',
    letterSpacing: 0.5,
  },
  actionButton: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFF4DA',
    paddingVertical: 6,
    paddingHorizontal: 12,
    borderRadius: 6,
    borderWidth: 1,
    borderColor: GOLD_ACCENT,
  },
  actionButtonText: {
    color: GOLD_DARK,
    fontSize: 12,
    fontWeight: '700',
    fontFamily: 'Cambria',
  },
  scrollContent: {
    padding: 16,
    paddingBottom: 40,
  },
  cardWrapper: {
    marginBottom: 16,
  },
  goldCard: {
    backgroundColor: CARD_BG,
    borderRadius: 8,
    borderWidth: 1,
    borderColor: GOLD_BORDER,
    padding: 16,
    shadowColor: '#3B2D17',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.05,
    shadowRadius: 6,
    elevation: 2,
  },
  inputRow: {
    flexDirection: 'row',
    gap: 12,
    flexWrap: 'wrap',
    zIndex: 20,
  },
  reportCol: {
    minWidth: 180,
  },
  fieldLabel: {
    fontSize: 11.5,
    fontWeight: '700',
    color: '#5C4A28',
    marginBottom: 4,
    fontFamily: 'Cambria',
    textTransform: 'uppercase',
  },
  divider: {
    height: 1,
    backgroundColor: '#F5EFE6',
    marginVertical: 14,
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
    paddingHorizontal: 14,
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
    fontWeight: '700',
  },
  summaryContainer: {
    flexDirection: 'row',
    gap: 12,
    marginBottom: 16,
    flexWrap: 'wrap',
  },
  summaryCard: {
    flex: 1,
    minWidth: 140,
    backgroundColor: CARD_BG,
    borderRadius: 8,
    borderWidth: 1,
    borderColor: GOLD_BORDER,
    padding: 12,
    alignItems: 'center',
  },
  summaryCardLabel: {
    fontSize: 10.5,
    fontWeight: '700',
    color: '#7C6745',
    marginBottom: 4,
    fontFamily: 'Cambria',
    textTransform: 'uppercase',
  },
  summaryCardVal: {
    fontSize: 16,
    fontWeight: '800',
    color: TEXT_DARK,
    fontFamily: 'Cambria',
  },
  tableCard: {
    backgroundColor: CARD_BG,
    borderRadius: 8,
    borderWidth: 1,
    borderColor: GOLD_BORDER,
    overflow: 'hidden',
    shadowColor: '#3B2D17',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.05,
    shadowRadius: 6,
    elevation: 2,
  },
  tableCardHeader: {
    padding: 12,
    borderBottomWidth: 1,
    borderBottomColor: '#F5EFE6',
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
  },
  tableCardTitle: {
    fontSize: 13,
    fontWeight: '700',
    color: TEXT_DARK,
    fontFamily: 'Cambria',
  },
  tableCardSubtitle: {
    fontSize: 11,
    color: '#7C6745',
    fontWeight: '600',
    fontFamily: 'Cambria',
  },
  tableHeaderRow: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#996515',
    borderBottomWidth: 1,
    borderBottomColor: '#d4a742',
    paddingVertical: 8,
    paddingHorizontal: 8,
  },
  tableHeaderCell: {
    paddingHorizontal: 6,
    justifyContent: 'center',
  },
  tableHeaderText: {
    fontSize: 10.5,
    fontWeight: '700',
    color: '#FFFFFF',
    fontFamily: 'Cambria',
    textTransform: 'uppercase',
  },
  tableBodyRow: {
    flexDirection: 'row',
    borderBottomWidth: 1,
    borderBottomColor: '#F0D9A0',
    paddingVertical: 7,
    paddingHorizontal: 8,
    alignItems: 'center',
  },
  tableRowEven: {
    backgroundColor: '#FFFBF2',
  },
  tableRowOdd: {
    backgroundColor: '#FFFFFF',
  },
  tableBodyCell: {
    paddingHorizontal: 6,
    justifyContent: 'center',
  },
  tableIndexText: {
    fontSize: 10.5,
    color: '#7C6745',
    fontFamily: 'Cambria',
  },
  tableCellText: {
    fontSize: 11,
    color: TEXT_DARK,
    fontFamily: 'Cambria',
  },
  statusScanned: {
    color: '#16A34A',
    fontWeight: '700',
  },
  statusUnscanned: {
    color: '#DC2626',
    fontWeight: '700',
  },
  toast: {
    position: 'absolute',
    bottom: 20,
    alignSelf: 'center',
    flexDirection: 'row',
    alignItems: 'center',
    paddingVertical: 10,
    paddingHorizontal: 16,
    borderRadius: 8,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.15,
    shadowRadius: 5,
    elevation: 4,
    zIndex: 999,
  },
  toastSuccess: {
    backgroundColor: '#F0FDF4',
    borderWidth: 1,
    borderColor: '#BBF7D0',
  },
  toastError: {
    backgroundColor: '#FEF2F2',
    borderWidth: 1,
    borderColor: '#FECACA',
  },
  toastText: {
    fontSize: 12,
    fontWeight: '600',
    fontFamily: 'Cambria',
  },
  toastTextSuccess: {
    color: '#15803D',
  },
  toastTextError: {
    color: '#DC2626',
  },
});
