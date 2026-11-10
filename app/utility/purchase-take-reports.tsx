import { API_BASE_URL } from '@/constants/api';
import { downloadExcelReport, downloadPdfReport, printHtmlReport } from '@/utils/reportExportClient';
import { formatColumnHeader } from '@/utils/formatColumnHeader';
import { ReportDropdown } from '@/components/ReportDropdown';
import { ReportDatePicker } from '@/components/ReportDatePicker';
import { validateReportDateRange, getTodayDisp, getCurrentYearStartDisp } from '@/utils/dateValidation';
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

// Royal Gold Design Palette (matching Master Reports Screen)
const GOLD_ACCENT = '#B48218';
const GOLD_DARK = '#9E6D08';
const GOLD_BORDER = '#E8DCB8';
const CARD_BG = '#FFFFFF';
const APP_BG = '#FBF9F5';
const TEXT_DARK = '#3B2D17';

type PurchaseTakeMode = 'all' | 'scanned' | 'unscanned';
type ActionOption = 'Preview' | 'Print' | 'PDF' | 'Excel';

interface TrayItem {
  TrayID: string;
  TrayName: string;
}

interface JobSheetItem {
  jobSheetNo: string;
}

const RadioButton = ({
  label,
  selected,
  onPress,
}: {
  label: string;
  selected: boolean;
  onPress: () => void;
}) => (
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

export default function PurchaseTakeReportsScreen() {
  const router = useRouter();

  // Mode: All, Scanned, UnScanned
  const [mode, setMode] = useState<PurchaseTakeMode>('all');
  const [trayId, setTrayId] = useState<string>('');
  const [trayList, setTrayList] = useState<TrayItem[]>([]);
  const [isTrayDropdownOpen, setIsTrayDropdownOpen] = useState<boolean>(false);

  // JobSheet filter
  const [jobSheetNo, setJobSheetNo] = useState<string>('');
  const [jobSheetList, setJobSheetList] = useState<JobSheetItem[]>([]);
  const [isJobSheetDropdownOpen, setIsJobSheetDropdownOpen] = useState<boolean>(false);

  // Date filters (using standard DD/MM/YYYY matching Master Reports)
  const [fromDate, setFromDate] = useState<string>(getTodayDisp('/'));
  const [toDate, setToDate] = useState<string>(getTodayDisp());

  // Native Date Picker State
  const [showDatePicker, setShowDatePicker] = useState<{
    show: boolean;
    mode: 'from' | 'to';
  }>({ show: false, mode: 'from' });

  // Action State: Preview, Print, PDF, Excel
  const [actionOption, setActionOption] = useState<ActionOption>('Preview');

  // Loading & Toast Notifications
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [toastMessage, setToastMessage] = useState<string>('');
  const [toastType, setToastType] = useState<'success' | 'error'>('success');

  // Load available trays and jobsheets from backend
  useEffect(() => {
    let isMounted = true;
    async function loadInitialData() {
      try {
        const [trayRes, jobRes] = await Promise.all([
          fetch(`${API_BASE_URL}/purchasetake/trays`),
          fetch(`${API_BASE_URL}/purchasetake/jobsheets`),
        ]);
        const trayJson = await trayRes.json();
        const jobJson = await jobRes.json();

        if (isMounted) {
          if (trayJson.success && Array.isArray(trayJson.data)) {
            setTrayList(trayJson.data);
          }
          if (jobJson.success && Array.isArray(jobJson.jobsheets)) {
            setJobSheetList(jobJson.jobsheets);
          }
        }
      } catch (e) {
        console.warn('Failed to load initial data for purchase take reports:', e);
      }
    }
    loadInitialData();
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

    const d = String(selectedDate.getDate()).padStart(2, '0');
    const m = String(selectedDate.getMonth() + 1).padStart(2, '0');
    const y = selectedDate.getFullYear();
    const formatted = `${d}/${m}/${y}`;

    if (showDatePicker.mode === 'from') {
      setFromDate(formatted);
    } else {
      setToDate(formatted);
    }
  };

  const parseLocalDate = (dateStr: string) => {
    if (!dateStr) return new Date();
    if (dateStr.includes('/')) {
      const parts = dateStr.split('/');
      if (parts.length === 3) {
        return new Date(parseInt(parts[2], 10), parseInt(parts[1], 10) - 1, parseInt(parts[0], 10));
      }
    } else if (dateStr.includes('-')) {
      const parts = dateStr.split('-');
      if (parts.length === 3) {
        return new Date(parseInt(parts[0], 10), parseInt(parts[1], 10) - 1, parseInt(parts[2], 10));
      }
    }
    return new Date();
  };

  // Execute Purchase Take Report (Matches Master Reports Generate & Preview Flow)
  const handleGetReport = async (overrideAction?: ActionOption) => {
    const currentAction = overrideAction || actionOption;

    if (!fromDate || !toDate) {
      showError('Please enter valid data for Date!');
      return;
    }

    const valRes = validateReportDateRange(fromDate, toDate);
    if (!valRes.isValid) {
      showError(valRes.errorMessage || 'Invalid date range.');
      return;
    }

    setIsLoading(true);

    try {
      const response = await fetch(`${API_BASE_URL}/purchasetake/report`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          fromDate,
          toDate,
          mode,
          trayId: trayId.trim(),
          jobSheetNo: jobSheetNo.trim(),
        }),
      });

      const resJson = await response.json();

      if (!resJson.success || !resJson.data || resJson.data.length === 0) {
        showError(resJson.message || 'No records found for the selected criteria.');
        setIsLoading(false);
        return;
      }

      const reportTitle = 'PURCHASE TAKE REPORT';
      const rawCols = resJson.columns || Object.keys(resJson.data[0]).map((k) => ({ key: k, label: k }));
      const columns = rawCols.map((c: any) => ({
        ...c,
        label: formatColumnHeader(c.label || c.key),
      }));
      const reportData = resJson.data;
      const reportTotals = resJson.totals || {};

      const displayTray = trayId.trim() ? trayId.trim() : 'ALL TRAYS';
      const displayJob = jobSheetNo.trim() && jobSheetNo.trim().toUpperCase() !== 'ALL' ? ` | JOBSHEET : ${jobSheetNo.trim()}` : '';
      const subtitleText = `TRAY ID : ${displayTray}${displayJob}`;

      if (currentAction === 'Preview') {
        await printHtmlReport(
          {
            endpoint: 'purchasetake',
            title: reportTitle,
            subtitle: subtitleText,
            columns,
            data: reportData,
            totals: reportTotals,
            fromDate,
            toDate,
          },
          false
        );
        showSuccess('Report preview opened.');
      } else if (currentAction === 'Print') {
        await printHtmlReport(
          {
            endpoint: 'purchasetake',
            title: reportTitle,
            subtitle: subtitleText,
            columns,
            data: reportData,
            totals: reportTotals,
            fromDate,
            toDate,
          },
          true
        );
        showSuccess('Print dialog opened.');
      } else if (currentAction === 'PDF') {
        const ok = await downloadPdfReport({
          endpoint: 'purchasetake',
          title: reportTitle,
          columns,
          data: reportData,
          totals: reportTotals,
          fromDate,
          toDate,
        });
        if (ok) showSuccess('PDF downloaded successfully!');
      } else if (currentAction === 'Excel') {
        const ok = await downloadExcelReport({
          endpoint: 'purchasetake',
          title: reportTitle,
          columns,
          data: reportData,
          totals: reportTotals,
          fromDate,
          toDate,
        });
        if (ok) showSuccess('Excel downloaded successfully!');
      }
    } catch (err: any) {
      console.error('Purchase Take Report Error:', err);
      showError(err.message || 'Failed to generate report');
    } finally {
      setIsLoading(false);
    }
  };

  // Keyboard shortcut listener (Web only)
  useEffect(() => {
    if (Platform.OS === 'web' && typeof window !== 'undefined') {
      const handleKeyDown = (e: KeyboardEvent) => {
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
  }, [mode, trayId, jobSheetNo, fromDate, toDate, actionOption]);

  const trayOptions = useMemo(() => {
    return [
      { id: '', label: 'ALL TRAYS' },
      ...trayList.map((t) => ({ id: t.TrayID, label: `${t.TrayID} - ${t.TrayName}` })),
    ];
  }, [trayList]);

  const jobSheetOptions = useMemo(() => {
    return [
      { id: '', label: 'ALL JOBSHEETS' },
      ...jobSheetList.map((j) => ({ id: j.jobSheetNo, label: j.jobSheetNo })),
    ];
  }, [jobSheetList]);

  return (
    <SafeAreaView style={styles.safeArea} edges={['top']}>
      <Stack.Screen options={{ headerShown: false }} />

      {/* HEADER (MATCHING MASTER REPORTS SCREEN) */}
      <View style={styles.header}>
        <View style={styles.headerLeft}>
          <TouchableOpacity onPress={() => router.back()} style={styles.backButton} activeOpacity={0.7}>
            <Ionicons name="arrow-back" size={20} color={TEXT_DARK} />
          </TouchableOpacity>
          <Text style={styles.headerTitle}>Purchase Take Reports</Text>
        </View>

        <TouchableOpacity
          style={[styles.btnGetReport, isLoading && styles.btnDisabled]}
          onPress={() => handleGetReport()}
          disabled={isLoading}
          activeOpacity={0.8}
        >
          {isLoading ? (
            <ActivityIndicator size="small" color="#FFFFFF" />
          ) : (
            <>
              <Ionicons name="document-text-outline" size={15} color="#FFFFFF" style={{ marginRight: 6 }} />
              <Text style={styles.btnGetReportText}>Get Report</Text>
            </>
          )}
        </TouchableOpacity>
      </View>

      {/* TOAST MESSAGE */}
      {toastMessage ? (
        <View style={[styles.toastContainer, toastType === 'error' ? styles.toastError : styles.toastSuccess]}>
          <Ionicons
            name={toastType === 'error' ? 'alert-circle' : 'checkmark-circle'}
            size={18}
            color="#FFFFFF"
            style={{ marginRight: 6 }}
          />
          <Text style={styles.toastText}>{toastMessage}</Text>
        </View>
      ) : null}

      <ScrollView contentContainerStyle={styles.scrollContent} keyboardShouldPersistTaps="handled">
        <View style={styles.filterCard}>
          {/* FILTER ROW: SELECT OPTION, JOBSHEET, TRAY ID, FROM DATE, TO DATE */}
          <View style={styles.filterRow}>
            {/* SELECT OPTION (MODE) */}
            <View style={styles.selectOptionCol}>
              <Text style={styles.fieldLabel}>Select Option</Text>
              <View style={styles.radioGroupRow}>
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

            {/* JOBSHEET NO. (OPTIONAL) */}
            <View style={styles.trayCol}>
              <ReportDropdown
                label="JobSheet No. (Optional)"
                options={jobSheetOptions}
                selectedId={jobSheetNo}
                onSelect={(id) => {
                  setJobSheetNo(id);
                  setIsJobSheetDropdownOpen(false);
                }}
                isOpen={isJobSheetDropdownOpen}
                onToggle={() => {
                  setIsJobSheetDropdownOpen(!isJobSheetDropdownOpen);
                  setIsTrayDropdownOpen(false);
                }}
                maxHeight={260}
              />
            </View>

            {/* TRAY ID (OPTIONAL) */}
            <View style={styles.trayCol}>
              <ReportDropdown
                label="Tray ID (Optional)"
                options={trayOptions}
                selectedId={trayId}
                onSelect={(id) => {
                  setTrayId(id);
                  setIsTrayDropdownOpen(false);
                }}
                isOpen={isTrayDropdownOpen}
                onToggle={() => {
                  setIsTrayDropdownOpen(!isTrayDropdownOpen);
                  setIsJobSheetDropdownOpen(false);
                }}
                maxHeight={260}
              />
            </View>

            {/* FROM DATE */}
            <ReportDatePicker
              label="From Date"
              value={fromDate}
              onChange={setFromDate}
              onOpenNative={() => setShowDatePicker({ show: true, mode: 'from' })}
            />

            {/* TO DATE */}
            <ReportDatePicker
              label="To Date"
              value={toDate}
              onChange={setToDate}
              onOpenNative={() => setShowDatePicker({ show: true, mode: 'to' })}
            />
          </View>

          {/* SUBTLE HORIZONTAL DIVIDER */}
          <View style={styles.divider} />

          {/* ACTIONS ROW: PREVIEW, PRINT, PDF, EXCEL */}
          <View style={styles.radioGroupRow}>
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
      </ScrollView>

      {/* MOBILE DATE PICKER */}
      {showDatePicker.show && Platform.OS !== 'web' && (
        <DateTimePicker
          value={parseLocalDate(showDatePicker.mode === 'from' ? fromDate : toDate)}
          maximumDate={new Date()}
          mode="date"
          display="default"
          onChange={handleDateChange}
        />
      )}
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  safeArea: {
    flex: 1,
    backgroundColor: APP_BG,
  },
  header: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingHorizontal: 16,
    paddingVertical: 12,
    backgroundColor: CARD_BG,
    borderBottomWidth: 1,
    borderBottomColor: GOLD_BORDER,
  },
  headerLeft: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  backButton: {
    padding: 6,
    marginRight: 10,
    borderRadius: 6,
    backgroundColor: '#F5EFE6',
  },
  headerTitle: {
    fontSize: 18,
    fontWeight: '700',
    color: TEXT_DARK,
    fontFamily: 'Cambria, serif',
  },
  btnGetReport: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: GOLD_ACCENT,
    paddingHorizontal: 16,
    paddingVertical: 8,
    borderRadius: 6,
    shadowColor: GOLD_DARK,
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.2,
    shadowRadius: 3,
    elevation: 3,
  },
  btnDisabled: {
    opacity: 0.6,
  },
  btnGetReportText: {
    fontSize: 13,
    fontWeight: '700',
    color: '#FFFFFF',
    fontFamily: 'Cambria, serif',
  },
  toastContainer: {
    position: 'absolute',
    top: 60,
    alignSelf: 'center',
    flexDirection: 'row',
    alignItems: 'center',
    paddingVertical: 8,
    paddingHorizontal: 16,
    borderRadius: 6,
    zIndex: 9999,
    shadowColor: '#000000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.15,
    shadowRadius: 4,
    elevation: 5,
  },
  toastSuccess: {
    backgroundColor: '#2E7D32',
  },
  toastError: {
    backgroundColor: '#C62828',
  },
  toastText: {
    color: '#FFFFFF',
    fontSize: 13,
    fontWeight: '600',
    fontFamily: 'Cambria, serif',
  },
  scrollContent: {
    padding: 16,
  },
  filterCard: {
    backgroundColor: CARD_BG,
    borderRadius: 8,
    borderWidth: 1,
    borderColor: GOLD_BORDER,
    padding: 16,
    shadowColor: '#000000',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.05,
    shadowRadius: 3,
    elevation: 2,
    position: 'relative',
    zIndex: 1,
  },
  filterRow: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    gap: 12,
    marginBottom: 16,
    position: 'relative',
    zIndex: 100,
  },
  selectOptionCol: {
    flex: 1.8,
    minWidth: 240,
    zIndex: 10,
  },
  trayCol: {
    flex: 1.5,
    minWidth: 180,
    position: 'relative',
    zIndex: 100,
  },
  fieldLabel: {
    fontSize: 12,
    fontWeight: '700',
    color: TEXT_DARK,
    marginBottom: 6,
    fontFamily: 'Cambria, serif',
  },
  divider: {
    height: 1,
    backgroundColor: '#F0E6CC',
    marginVertical: 12,
  },
  radioGroupRow: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    gap: 8,
  },
  radioBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingVertical: 6,
    paddingHorizontal: 12,
    borderRadius: 6,
    borderWidth: 1,
    borderColor: GOLD_BORDER,
    backgroundColor: '#FFFFFF',
    height: 38,
  },
  radioBtnSelected: {
    borderColor: GOLD_ACCENT,
    backgroundColor: '#FDFBF7',
  },
  radioCircle: {
    width: 16,
    height: 16,
    borderRadius: 8,
    borderWidth: 1.5,
    borderColor: '#C4B59D',
    alignItems: 'center',
    justifyContent: 'center',
    marginRight: 6,
  },
  radioCircleSelected: {
    borderColor: GOLD_ACCENT,
  },
  radioDot: {
    width: 8,
    height: 8,
    borderRadius: 4,
    backgroundColor: GOLD_ACCENT,
  },
  radioLabel: {
    fontSize: 12,
    color: TEXT_DARK,
    fontFamily: 'Cambria, serif',
  },
  radioLabelSelected: {
    fontWeight: '700',
    color: GOLD_ACCENT,
  },
});
