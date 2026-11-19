import { formatColumnHeader } from '@/utils/formatColumnHeader';
import { API_BASE_URL } from '@/constants/api';
import { downloadExcelReport, downloadPdfReport, printHtmlReport } from '@/utils/reportExportClient';
import { ReportDropdown } from '@/components/ReportDropdown';
import { Ionicons } from '@expo/vector-icons';
import { Stack, useRouter } from 'expo-router';
import React, { useMemo, useState } from 'react';
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

// Royal Gold Design Palette (matching Customer Reports)
const GOLD_ACCENT = '#B48218';
const GOLD_DARK = '#9E6D08';
const GOLD_BORDER = '#E8DCB8';
const CARD_BG = '#FFFFFF';
const APP_BG = '#FBF9F5';
const TEXT_DARK = '#3B2D17';

interface ReportOption {
  id: string;
  label: string;
}

const REPORT_OPTIONS: ReportOption[] = [
  { id: 'Class', label: 'Class' },
  { id: 'Category', label: 'Category' },
  { id: 'Catalogue', label: 'Catalogue' },
  { id: 'Tray', label: 'Tray' },
  { id: 'Sales Man', label: 'Sales Man' },
  { id: 'Suppliers', label: 'Suppliers' },
  { id: 'Customers', label: 'Customers' },
  { id: "Customer's Mailing Address", label: "Customer's Mailing Address" },
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

export default function MasterReportsScreen() {
  const router = useRouter();

  const [selectedReport, setSelectedReport] = useState<string>('Class');
  const [actionOption, setActionOption] = useState<ActionOption>('Preview');
  const [isDropdownOpen, setIsDropdownOpen] = useState(false);
  const [isLoading, setIsLoading] = useState(false);

  const [toastMessage, setToastMessage] = useState('');
  const [toastType, setToastType] = useState<'success' | 'error'>('success');

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

  const selectedReportObj = useMemo(
    () => REPORT_OPTIONS.find((r) => r.id === selectedReport),
    [selectedReport]
  );

  const handleGetReport = async () => {
    if (!selectedReport) {
      showError('Please select a report.');
      return;
    }

    setIsLoading(true);

    try {
      const response = await fetch(`${API_BASE_URL}/masterreports/generate`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          SelectOption: selectedReport,
        }),
      });

      const resJson = await response.json();

      if (!resJson.success || !resJson.data || resJson.data.length === 0) {
        showError(resJson.message || 'No records found for the selected criteria.');
        setIsLoading(false);
        return;
      }

      const reportTitle = resJson.title || selectedReportObj?.label || 'Master Report';
      const rawCols = resJson.columns || Object.keys(resJson.data[0]).map((k) => ({ key: k, label: k }));
      const columns = rawCols.map((c: any) => ({ ...c, label: formatColumnHeader(c.label || c.key) }));
      const reportData = resJson.data;
      const reportTotals = resJson.totals || {};

      if (actionOption === 'Preview') {
        await printHtmlReport(
          {
            endpoint: 'masterreports',
            title: reportTitle,
            columns,
            data: reportData,
            totals: reportTotals,
          },
          false
        );
        showSuccess('Report preview opened.');
      } else if (actionOption === 'Print') {
        await printHtmlReport(
          {
            endpoint: 'masterreports',
            title: reportTitle,
            columns,
            data: reportData,
            totals: reportTotals,
          },
          true
        );
        showSuccess('Print dialog opened.');
      } else if (actionOption === 'PDF') {
        const ok = await downloadPdfReport({
          endpoint: 'masterreports',
          title: reportTitle,
          columns,
          data: reportData,
          totals: reportTotals,
        });
        if (ok) showSuccess('PDF downloaded successfully!');
      } else if (actionOption === 'Excel') {
        const ok = await downloadExcelReport({
          endpoint: 'masterreports',
          title: reportTitle,
          columns,
          data: reportData,
          totals: reportTotals,
        });
        if (ok) showSuccess('Excel downloaded successfully!');
      }
    } catch (err: any) {
      console.error('Master Reports Get Error:', err);
      showError(err.message || 'Failed to generate report');
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <SafeAreaView style={styles.safeArea} edges={['top']}>
      <Stack.Screen options={{ headerShown: false }} />

      {/* HEADER */}
      <View style={styles.header}>
        <View style={styles.headerLeft}>
          <TouchableOpacity onPress={() => router.back()} style={styles.backButton} activeOpacity={0.7}>
            <Ionicons name="arrow-back" size={20} color={TEXT_DARK} />
          </TouchableOpacity>
          <Text style={styles.headerTitle}>Master Reports</Text>
        </View>

        <TouchableOpacity
          style={[styles.btnGetReport, isLoading && styles.btnDisabled]}
          onPress={handleGetReport}
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
          <View style={styles.singleRow}>
            {/* REPORT DROPDOWN */}
            <View style={styles.dropdownCol}>
              <ReportDropdown
                label="Master Report"
                options={REPORT_OPTIONS}
                selectedId={selectedReport}
                onSelect={(id) => {
                  setSelectedReport(id);
                  setIsDropdownOpen(false);
                }}
                isOpen={isDropdownOpen}
                onToggle={() => setIsDropdownOpen(!isDropdownOpen)}
                maxHeight={260}
              />
            </View>

            {/* ACTION BUTTONS (4 RADIO BUTTONS) */}
            <View style={styles.radioGroupCol}>
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
      </ScrollView>
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
    color: '#FFFFFF',
    fontSize: 13,
    fontWeight: '700',
    fontFamily: 'Cambria, serif',
  },
  toastContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 16,
    paddingVertical: 8,
  },
  toastSuccess: {
    backgroundColor: '#10B981',
  },
  toastError: {
    backgroundColor: '#EF4444',
  },
  toastText: {
    color: '#FFFFFF',
    fontSize: 13,
    fontWeight: '600',
  },
  scrollContent: {
    padding: 16,
  },
  filterCard: {
    backgroundColor: CARD_BG,
    borderRadius: 8,
    padding: 16,
    borderWidth: 1,
    borderColor: GOLD_BORDER,
    shadowColor: '#000000',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.05,
    shadowRadius: 3,
    elevation: 2,
    position: 'relative',
    zIndex: 1,
  },
  singleRow: {
    flexDirection: 'row',
    alignItems: 'flex-end',
    gap: 16,
    flexWrap: 'wrap',
    position: 'relative',
    zIndex: 100,
  },
  dropdownCol: {
    width: 280,
    minWidth: 220,
    position: 'relative',
    zIndex: 100,
  },
  radioGroupCol: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 10,
    flexWrap: 'wrap',
  },
  fieldLabel: {
    fontSize: 12,
    fontWeight: '700',
    color: TEXT_DARK,
    marginBottom: 6,
    fontFamily: 'Cambria, serif',
  },
  dropdownSelector: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    height: 38,
    paddingHorizontal: 12,
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: GOLD_BORDER,
    borderRadius: 6,
  },
  dropdownSelectorText: {
    fontSize: 13,
    color: TEXT_DARK,
    fontFamily: 'Cambria, serif',
  },
  dropdownMenu: {
    position: 'absolute',
    top: 66,
    left: 0,
    right: 0,
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: GOLD_BORDER,
    borderRadius: 6,
    shadowColor: '#000000',
    shadowOffset: { width: 0, height: 6 },
    shadowOpacity: 0.2,
    shadowRadius: 10,
    elevation: 9999,
    zIndex: 999999,
    ...(Platform.OS === 'web'
      ? ({
          boxShadow: '0 8px 24px rgba(0, 0, 0, 0.18)',
        } as any)
      : {}),
  },
  dropdownItem: {
    paddingVertical: 10,
    paddingHorizontal: 12,
    borderBottomWidth: 1,
    borderBottomColor: '#F5EFE6',
  },
  dropdownItemSelected: {
    backgroundColor: '#FDFBF7',
  },
  dropdownItemText: {
    fontSize: 13,
    color: TEXT_DARK,
    fontFamily: 'Cambria, serif',
  },
  dropdownItemTextSelected: {
    fontWeight: '700',
    color: GOLD_ACCENT,
  },
  radioBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    height: 38,
    paddingHorizontal: 14,
    borderRadius: 6,
    borderWidth: 1,
    borderColor: GOLD_BORDER,
    backgroundColor: '#FFFFFF',
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
