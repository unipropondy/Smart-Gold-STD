import { formatColumnHeader } from '@/utils/formatColumnHeader';
import { API_BASE_URL } from '@/constants/api';
import { downloadExcelReport, downloadPdfReport, printHtmlReport } from '@/utils/reportExportClient';
import { ReportDatePicker } from '@/components/ReportDatePicker';
import { validateReportDateRange, getTodayDisp } from '@/utils/dateValidation';
import { Ionicons } from '@expo/vector-icons';
import { Stack, useRouter } from 'expo-router';
import React, { useEffect, useState } from 'react';
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
import { ThemedText } from '../components/themed-text';
import DateTimePicker from '@react-native-community/datetimepicker';

// Royal Gold Design Palette (matching Master Reports)
const GOLD_ACCENT = '#B48218';
const GOLD_DARK = '#9E6D08';
const GOLD_BORDER = '#E8DCB8';
const CARD_BG = '#FFFFFF';
const APP_BG = '#FBF9F5';
const TEXT_DARK = '#3B2D17';

// Web HTML Select Inline Style
const webSelectStyle: React.CSSProperties = {
  flex: 1,
  backgroundColor: 'transparent',
  border: 'none',
  color: TEXT_DARK,
  outline: 'none',
  paddingLeft: 8,
  fontSize: 12,
  appearance: 'none',
  WebkitAppearance: 'none',
  fontWeight: '500',
};

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

const CustomDropdown = ({
  value,
  options,
  onSelect,
  placeholder,
  style,
}: {
  value: string;
  options: string[];
  onSelect: (val: string) => void;
  placeholder: string;
  style?: any;
}) => {
  const [open, setOpen] = useState(false);
  return (
    <View style={[{ flex: 1, zIndex: 100 }, style]}>
      <TouchableOpacity
        style={styles.dropdownToggle}
        onPress={() => setOpen(!open)}
        activeOpacity={0.8}
      >
        <Text style={value ? styles.dropdownText : styles.dropdownPlaceholder}>
          {value || placeholder}
        </Text>
        <View style={styles.dropdownIconBox}>
          <Ionicons name={open ? 'chevron-up' : 'chevron-down'} size={14} color={TEXT_DARK} />
        </View>
      </TouchableOpacity>
      {open && (
        <View style={styles.dropdownMenu}>
          <ScrollView nestedScrollEnabled>
            {options.map((opt, i) => (
              <TouchableOpacity
                key={i}
                style={[styles.dropdownItem, i < options.length - 1 && styles.dropdownItemBorder]}
                onPress={() => {
                  onSelect(opt);
                  setOpen(false);
                }}
              >
                <Text style={styles.dropdownItemText}>{opt}</Text>
              </TouchableOpacity>
            ))}
          </ScrollView>
        </View>
      )}
    </View>
  );
};

const API_URL = `${API_BASE_URL}/transferreports`;
const LOCATION_API = `${API_BASE_URL}/location`;

const formatDateLocal = (d: any) => {
  if (!d || !(d instanceof Date) || isNaN(d.getTime())) return '';
  const day = String(d.getDate()).padStart(2, '0');
  const month = String(d.getMonth() + 1).padStart(2, '0');
  const year = d.getFullYear();
  return `${day}/${month}/${year}`;
};

const parseLocalDate = (dateStr: string) => {
  if (!dateStr) return new Date();
  if (dateStr.includes('/')) {
    const [d, m, y] = dateStr.split('/');
    return new Date(parseInt(y, 10), parseInt(m, 10) - 1, parseInt(d, 10));
  }
  if (dateStr.includes('-')) {
    const parts = dateStr.split('-');
    if (parts.length === 3) {
      if (parts[0].length === 4) {
        return new Date(parseInt(parts[0], 10), parseInt(parts[1], 10) - 1, parseInt(parts[2], 10));
      } else {
        return new Date(parseInt(parts[2], 10), parseInt(parts[1], 10) - 1, parseInt(parts[0], 10));
      }
    }
  }
  return new Date();
};

export default function TransferReportsScreen() {
  const router = useRouter();

  const [locations, setLocations] = useState<any[]>([]);
  const [toastMessage, setToastMessage] = useState('');
  const [toastType, setToastType] = useState<'success' | 'error'>('success');
  const [isLoading, setIsLoading] = useState(false);

  const [actionOption, setActionOption] = useState<ActionOption>('Preview');

  const [showDatePicker, setShowDatePicker] = useState<{ show: boolean; mode: 'from' | 'to' }>({
    show: false,
    mode: 'from',
  });

  const [formData, setFormData] = useState({
    SelectOption: 'Transfer Out',
    FromLocation: 'ALL',
    ToLocation: 'ALL',
    DateFrom: getTodayDisp('/'),
    DateTo: getTodayDisp('/'),
    ReportType: 'Summary',
    TransferStatus: 'ALL',
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

  const fetchLocations = async () => {
    try {
      const res = await fetch(LOCATION_API);
      const json = await res.json();
      if (json.success) {
        setLocations([{ LocationCode: 'ALL' }, ...json.data]);
      }
    } catch (error) {
      console.error('Failed to fetch locations', error);
      setLocations([{ LocationCode: 'ALL' }]);
    }
  };

  useEffect(() => {
    fetchLocations();
  }, []);

  const handleChange = (field: string, value: any) => {
    setFormData((prev) => ({ ...prev, [field]: value }));
  };

  const handleDateChange = (event: any, selectedDate?: Date) => {
    const mode = showDatePicker.mode;
    setShowDatePicker({ show: false, mode });
    if (selectedDate) {
      if (mode === 'from') {
        handleChange('DateFrom', formatDateLocal(selectedDate));
      } else {
        handleChange('DateTo', formatDateLocal(selectedDate));
      }
    }
  };

  const handleGetReport = async () => {
    const valRes = validateReportDateRange(formData.DateFrom, formData.DateTo);
    if (!valRes.isValid) {
      showError(valRes.errorMessage || 'Invalid date range.');
      return;
    }

    setIsLoading(true);

    try {
      const payload = {
        ...formData,
      };

      const response = await fetch(`${API_URL}/generate`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload),
      });

      const resJson = await response.json();

      if (!resJson.success || !resJson.data || resJson.data.length === 0) {
        showError(resJson.message || 'No records found for the selected criteria.');
        setIsLoading(false);
        return;
      }

      const reportTitle = resJson.title || `${formData.SelectOption} ${formData.ReportType}`;
      const rawCols = resJson.columns || Object.keys(resJson.data[0]).map((k) => ({ key: k, label: k }));
      const columns = rawCols.map((c: any) => ({ ...c, label: formatColumnHeader(c.label || c.key) }));
      const reportData = resJson.data;
      const reportTotals = resJson.totals || {};
      const subtitle = resJson.subtitle || '';

      if (actionOption === 'Preview') {
        await printHtmlReport(
          {
            endpoint: 'transferreports',
            title: reportTitle,
            subtitle,
            columns,
            data: reportData,
            totals: reportTotals,
            fromDate: formData.DateFrom,
            toDate: formData.DateTo,
          },
          false
        );
        showSuccess('Report preview opened.');
      } else if (actionOption === 'Print') {
        await printHtmlReport(
          {
            endpoint: 'transferreports',
            title: reportTitle,
            subtitle,
            columns,
            data: reportData,
            totals: reportTotals,
            fromDate: formData.DateFrom,
            toDate: formData.DateTo,
          },
          true
        );
        showSuccess('Print dialog opened.');
      } else if (actionOption === 'PDF') {
        const ok = await downloadPdfReport({
          endpoint: 'transferreports',
          title: reportTitle,
          subtitle,
          columns,
          data: reportData,
          totals: reportTotals,
          fromDate: formData.DateFrom,
          toDate: formData.DateTo,
        });
        if (ok) showSuccess('PDF downloaded successfully!');
      } else if (actionOption === 'Excel') {
        const ok = await downloadExcelReport({
          endpoint: 'transferreports',
          title: reportTitle,
          subtitle,
          columns,
          data: reportData,
          totals: reportTotals,
          fromDate: formData.DateFrom,
          toDate: formData.DateTo,
        });
        if (ok) showSuccess('Excel downloaded successfully!');
      }
    } catch (err: any) {
      console.error('Transfer Reports Get Error:', err);
      showError(err.message || 'Failed to generate report');
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <SafeAreaView style={styles.container} edges={['top']}>
      <Stack.Screen options={{ headerShown: false }} />

      {/* LUXURY ROYAL GOLD HEADER BAR (matching Master Reports) */}
      <View style={styles.header}>
        <View style={styles.headerLeft}>
          <TouchableOpacity onPress={() => router.back()} style={styles.backButton} activeOpacity={0.7}>
            <Ionicons name="arrow-back" size={20} color={TEXT_DARK} />
          </TouchableOpacity>
          <View>
            <ThemedText style={styles.title}>Transfer Reports</ThemedText>
          </View>
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

      {/* TOAST NOTIFICATION */}
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

      <ScrollView contentContainerStyle={styles.scrollContent} keyboardShouldPersistTaps="handled">
        <View style={styles.cardWrapper}>
          <View style={styles.goldCard}>
            <View style={styles.cardBody}>
              {/* Row 1: Select Option (Radio Buttons) */}
              <View style={styles.gridRow}>
                <View style={[styles.inputGroup, { flex: 1 }]}>
                  <Text style={styles.label}>Select Option</Text>
                  <View style={styles.radioRow}>
                    <RadioButton
                      label="Transfer Out"
                      selected={formData.SelectOption === 'Transfer Out'}
                      onPress={() => handleChange('SelectOption', 'Transfer Out')}
                    />
                    <RadioButton
                      label="Transfer In"
                      selected={formData.SelectOption === 'Transfer In'}
                      onPress={() => handleChange('SelectOption', 'Transfer In')}
                    />
                    <RadioButton
                      label="TradeIn Tr. Out"
                      selected={formData.SelectOption === 'TradeIn Transfer Out'}
                      onPress={() => handleChange('SelectOption', 'TradeIn Transfer Out')}
                    />
                  </View>
                </View>
              </View>

              {/* Row 2: From Loc & To Loc */}
              <View style={styles.gridRow}>
                <View style={[styles.inputGroup, { flex: 1, minWidth: 220 }]}>
                  <Text style={styles.label}>From Loc.</Text>
                  {Platform.OS === 'web' ? (
                    <View style={styles.selectWrapper}>
                      <select
                        value={formData.FromLocation}
                        onChange={(e) => handleChange('FromLocation', e.target.value)}
                        style={webSelectStyle}
                      >
                        {locations.map((loc, idx) => (
                          <option key={idx} value={loc.LocationCode} style={{ color: '#000' }}>
                            {loc.LocationCode}
                          </option>
                        ))}
                      </select>
                      <View style={styles.selectIconBox}>
                        <Ionicons name="chevron-down" size={14} color={TEXT_DARK} />
                      </View>
                    </View>
                  ) : (
                    <CustomDropdown
                      value={formData.FromLocation}
                      options={locations.map((l) => l.LocationCode)}
                      onSelect={(val) => handleChange('FromLocation', val)}
                      placeholder="Select From Loc"
                      style={{ flex: 1, maxWidth: 250 }}
                    />
                  )}
                </View>

                <View style={[styles.inputGroup, { flex: 1, minWidth: 220 }]}>
                  <Text style={[styles.label, { paddingLeft: 8 }]}>To Location</Text>
                  {Platform.OS === 'web' ? (
                    <View style={styles.selectWrapper}>
                      <select
                        value={formData.ToLocation}
                        onChange={(e) => handleChange('ToLocation', e.target.value)}
                        style={webSelectStyle}
                      >
                        {locations.map((loc, idx) => (
                          <option key={idx} value={loc.LocationCode} style={{ color: '#000' }}>
                            {loc.LocationCode}
                          </option>
                        ))}
                      </select>
                      <View style={styles.selectIconBox}>
                        <Ionicons name="chevron-down" size={14} color={TEXT_DARK} />
                      </View>
                    </View>
                  ) : (
                    <CustomDropdown
                      value={formData.ToLocation}
                      options={locations.map((l) => l.LocationCode)}
                      onSelect={(val) => handleChange('ToLocation', val)}
                      placeholder="Select To Loc"
                      style={{ flex: 1, maxWidth: 250 }}
                    />
                  )}
                </View>
              </View>

              {/* Row 3: Date From & Date To using ReportDatePicker (matching Master Reports calendar date picker) */}
              <View style={styles.gridRow}>
                <View style={[styles.inputGroup, { flex: 1, minWidth: 220 }]}>
                  <Text style={styles.label}>Date From</Text>
                  <View style={{ flex: 1, maxWidth: 250 }}>
                    <ReportDatePicker
                      label=""
                      value={formData.DateFrom}
                      onChange={(val) => handleChange('DateFrom', val)}
                      onOpenNative={() => setShowDatePicker({ show: true, mode: 'from' })}
                    />
                  </View>
                </View>

                <View style={[styles.inputGroup, { flex: 1, minWidth: 220 }]}>
                  <Text style={[styles.label, { paddingLeft: 8 }]}>Date To</Text>
                  <View style={{ flex: 1, maxWidth: 250 }}>
                    <ReportDatePicker
                      label=""
                      value={formData.DateTo}
                      onChange={(val) => handleChange('DateTo', val)}
                      onOpenNative={() => setShowDatePicker({ show: true, mode: 'to' })}
                    />
                  </View>
                </View>
              </View>

              {/* Row 4: Report Type & Status */}
              <View style={styles.gridRow}>
                <View style={[styles.inputGroup, { flex: 1, minWidth: 220 }]}>
                  <Text style={styles.label}>Report Type</Text>
                  <View style={styles.radioRow}>
                    <RadioButton
                      label="Summary"
                      selected={formData.ReportType === 'Summary'}
                      onPress={() => handleChange('ReportType', 'Summary')}
                    />
                    <RadioButton
                      label="Detail"
                      selected={formData.ReportType === 'Detail'}
                      onPress={() => handleChange('ReportType', 'Detail')}
                    />
                  </View>
                </View>

                <View style={[styles.inputGroup, { flex: 1, minWidth: 220 }]}>
                  <Text style={[styles.label, { paddingLeft: 8 }]}>Status</Text>
                  {Platform.OS === 'web' ? (
                    <View style={styles.selectWrapper}>
                      <select
                        value={formData.TransferStatus}
                        onChange={(e) => handleChange('TransferStatus', e.target.value)}
                        style={webSelectStyle}
                      >
                        <option value="ALL" style={{ color: '#000' }}>ALL</option>
                        <option value="PENDING" style={{ color: '#000' }}>PENDING</option>
                        <option value="ACCEPTED" style={{ color: '#000' }}>ACCEPTED</option>
                      </select>
                      <View style={styles.selectIconBox}>
                        <Ionicons name="chevron-down" size={14} color={TEXT_DARK} />
                      </View>
                    </View>
                  ) : (
                    <CustomDropdown
                      value={formData.TransferStatus}
                      options={['ALL', 'PENDING', 'ACCEPTED']}
                      onSelect={(val) => handleChange('TransferStatus', val)}
                      placeholder="Status"
                      style={{ flex: 1, maxWidth: 250 }}
                    />
                  )}
                </View>
              </View>

              {/* Row 5: Action Options matching Master Reports (Preview, Print, PDF, Excel) */}
              <View style={[styles.gridRow, { marginTop: 8, paddingTop: 10, borderTopWidth: 1, borderTopColor: '#F0E6CC' }]}>
                <View style={[styles.inputGroup, { flex: 1 }]}>
                  <Text style={styles.label}>Action</Text>
                  <View style={styles.radioRow}>
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

            </View>
          </View>
        </View>

        {showDatePicker.show && (
          <DateTimePicker
            value={parseLocalDate(showDatePicker.mode === 'from' ? formData.DateFrom : formData.DateTo)}
            mode="date"
            display="default"
            onChange={handleDateChange}
          />
        )}
      </ScrollView>
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
    fontSize: 16,
    fontWeight: 'bold',
    color: '#3B2D17',
    fontFamily: 'Cambria',
  },
  btnGetReport: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#A37416',
    paddingVertical: 7,
    paddingHorizontal: 16,
    borderRadius: 6,
    borderWidth: 1,
    borderColor: '#7A5408',
    elevation: 2,
    shadowColor: '#7A5408',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.2,
    shadowRadius: 4,
  },
  btnDisabled: {
    opacity: 0.6,
  },
  btnGetReportText: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#FFFFFF',
    letterSpacing: 0.3,
  },
  scrollContent: {
    padding: 16,
  },
  cardWrapper: {
    width: '100%',
    alignSelf: 'center',
  },
  goldCard: {
    backgroundColor: CARD_BG,
    borderRadius: 8,
    borderWidth: 1,
    borderColor: GOLD_BORDER,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.04,
    shadowRadius: 8,
    elevation: 2,
  },
  cardBody: {
    padding: 16,
    gap: 12,
  },
  gridRow: {
    flexDirection: 'row',
    gap: 12,
    flexWrap: 'wrap',
    alignItems: 'center',
  },
  inputGroup: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 6,
    minWidth: 0,
  },
  label: {
    fontSize: 11,
    fontWeight: 'bold',
    color: TEXT_DARK,
    width: 95,
  },

  // Radio Buttons with Royal Gold touch
  radioRow: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    flex: 1,
    gap: 8,
  },
  radioBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingVertical: 5,
    paddingHorizontal: 10,
    borderRadius: 5,
    borderWidth: 1,
    borderColor: '#E2D3B3',
    backgroundColor: '#FAF7F0',
  },
  radioBtnSelected: {
    backgroundColor: '#FFF4DA',
    borderColor: GOLD_ACCENT,
  },
  radioCircle: {
    height: 16,
    width: 16,
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
    height: 8,
    width: 8,
    borderRadius: 4,
    backgroundColor: GOLD_DARK,
  },
  radioLabel: {
    fontSize: 11,
    fontWeight: '500',
    color: '#5C4A28',
  },
  radioLabelSelected: {
    color: GOLD_DARK,
    fontWeight: 'bold',
  },

  // Dropdown / Select
  selectWrapper: {
    flex: 1,
    maxWidth: 250,
    borderWidth: 1,
    borderColor: '#D4C3A3',
    borderRadius: 4,
    height: 32,
    backgroundColor: '#FFFFFF',
    flexDirection: 'row',
    alignItems: 'center',
  },
  selectIconBox: {
    width: 26,
    height: '100%',
    borderLeftWidth: 1,
    borderLeftColor: '#E8DCB8',
    alignItems: 'center',
    justifyContent: 'center',
    pointerEvents: 'none',
    backgroundColor: '#FAF5EA',
  },
  dropdownToggle: {
    borderWidth: 1,
    borderColor: '#D4C3A3',
    borderRadius: 4,
    height: 32,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    backgroundColor: '#FFFFFF',
  },
  dropdownText: {
    color: TEXT_DARK,
    fontSize: 12,
    flex: 1,
    paddingLeft: 8,
  },
  dropdownPlaceholder: {
    color: '#9E8D77',
    fontSize: 12,
    flex: 1,
    paddingLeft: 8,
  },
  dropdownIconBox: {
    width: 26,
    height: '100%',
    borderLeftWidth: 1,
    borderLeftColor: '#E8DCB8',
    alignItems: 'center',
    justifyContent: 'center',
    backgroundColor: '#FAF5EA',
  },
  dropdownMenu: {
    position: 'absolute',
    top: 36,
    left: 0,
    right: 0,
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#D4C3A3',
    borderRadius: 6,
    zIndex: 1000,
    maxHeight: 150,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.1,
    shadowRadius: 8,
    elevation: 4,
  },
  dropdownItem: {
    padding: 8,
  },
  dropdownItemBorder: {
    borderBottomWidth: 1,
    borderBottomColor: '#F5EFE6',
  },
  dropdownItemText: {
    color: TEXT_DARK,
    fontSize: 12,
  },

  // Toast
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
  },
});
