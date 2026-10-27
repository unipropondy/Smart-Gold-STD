import { API_BASE_URL } from '@/constants/api';
import { Ionicons } from '@expo/vector-icons';
import { Stack, useRouter } from 'expo-router';
import React, { useEffect, useState } from 'react';
import { Platform, ScrollView, StyleSheet, TouchableOpacity, View } from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';
import { ThemedText } from '../components/themed-text';
import { useThemeColors } from '../hooks/useThemeColors';

import DateTimePicker from '@react-native-community/datetimepicker';

// Royal Gold Design Palette
const GOLD_ACCENT = '#B48218';
const GOLD_DARK = '#9E6D08';
const GOLD_LIGHT_BG = '#F5E7CA';
const GOLD_BORDER = '#E8DCB8';
const CARD_BG = '#FFFFFF';
const APP_BG = '#FBF9F5';
const TEXT_DARK = '#3B2D17';
const TEXT_MUTED = '#7D5E19';

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

// Web HTML Date Input Inline Style
const webDateInputStyle: React.CSSProperties = {
  flex: 1,
  border: 'none',
  backgroundColor: 'transparent',
  outline: 'none',
  color: TEXT_DARK,
  paddingLeft: 8,
  fontSize: 12,
};

const RadioButton = ({ label, selected, onPress }: { label: string; selected: boolean; onPress: () => void }) => {
  return (
    <TouchableOpacity
      style={[
        styles.radioBtn,
        selected && styles.radioBtnSelected,
      ]}
      onPress={onPress}
      activeOpacity={0.7}
    >
      <View style={[styles.radioCircle, selected && styles.radioCircleSelected]}>
        {selected && <View style={styles.radioDot} />}
      </View>
      <ThemedText style={[styles.radioLabel, selected && styles.radioLabelSelected]}>
        {label}
      </ThemedText>
    </TouchableOpacity>
  );
};

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
        <ThemedText style={value ? styles.dropdownText : styles.dropdownPlaceholder}>
          {value || placeholder}
        </ThemedText>
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
                <ThemedText style={styles.dropdownItemText}>{opt}</ThemedText>
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

export default function TransferReportsScreen() {
  const router = useRouter();

  const [locations, setLocations] = useState<any[]>([]);
  const [toastMessage, setToastMessage] = useState('');
  const [toastType, setToastType] = useState<'success' | 'error'>('success');

  const [showDatePicker, setShowDatePicker] = useState<{ show: boolean; mode: 'from' | 'to' }>({
    show: false,
    mode: 'from',
  });

  const [formData, setFormData] = useState({
    SelectOption: 'Transfer Out',
    FromLocation: 'ALL',
    ToLocation: 'ALL',
    DateFrom: formatDateLocal(new Date()),
    DateTo: formatDateLocal(new Date()),
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

  const validateForm = () => {
    if (!formData.DateFrom) {
      showError('Date From is required');
      return false;
    }
    if (!formData.DateTo) {
      showError('Date To is required');
      return false;
    }
    return true;
  };

  const handleGetReport = async () => {
    if (!validateForm()) return;

    let win: Window | null = null;
    if (Platform.OS === 'web') {
      win = window.open('', '_blank');
    }

    try {
      const payload = {
        ...formData,
      };

      const response = await fetch(`${API_URL}/report`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload),
      });
      const html = await response.text();

      if (Platform.OS === 'web') {
        if (win) {
          win.document.write(html);
          win.document.close();
        }
      } else {
        showSuccess('Report generated! (Use web to view/print)');
      }
    } catch (err) {
      if (win) {
        win.close();
      }
      showError('Failed to generate report');
      console.error(err);
    }
  };

  return (
    <SafeAreaView style={styles.container} edges={['top']}>
      <Stack.Screen options={{ headerShown: false }} />

      {/* LUXURY ROYAL GOLD HEADER BAR */}
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
          style={styles.actionButton}
          onPressOut={handleGetReport}
          activeOpacity={0.8}
        >
          <Ionicons name="document-text-outline" size={15} color={GOLD_DARK} style={{ marginRight: 5 }} />
          <ThemedText style={styles.actionButtonText}>Get Report</ThemedText>
        </TouchableOpacity>
      </View>

      <ScrollView contentContainerStyle={styles.scrollContent}>
        <View style={styles.cardWrapper}>
          <View style={styles.goldCard}>
            <View style={styles.cardBody}>
              {/* Row 1: Select Option (Radio Buttons) */}
              <View style={styles.gridRow}>
                <View style={[styles.inputGroup, { flex: 1 }]}>
                  <ThemedText style={styles.label}>Select Option</ThemedText>
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
                  <ThemedText style={styles.label}>From Loc.</ThemedText>
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
                  <ThemedText style={[styles.label, { paddingLeft: 8 }]}>To Location</ThemedText>
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

              {/* Row 3: Date From & Date To */}
              <View style={styles.gridRow}>
                <View style={[styles.inputGroup, { flex: 1, minWidth: 220 }]}>
                  <ThemedText style={styles.label}>Date From</ThemedText>
                  {Platform.OS === 'web' ? (
                    <View style={styles.dateWrapper}>
                      <input
                        type="date"
                        value={formData.DateFrom || ''}
                        onChange={(e) => handleChange('DateFrom', e.target.value)}
                        style={webDateInputStyle}
                      />
                    </View>
                  ) : (
                    <TouchableOpacity
                      style={styles.mobileDateBtn}
                      onPress={() => setShowDatePicker({ show: true, mode: 'from' })}
                    >
                      <ThemedText style={styles.mobileDateText}>
                        {formData.DateFrom ? parseLocalDate(formData.DateFrom).toLocaleDateString('en-GB') : ''}
                      </ThemedText>
                      <View style={styles.dateIconBox}>
                        <Ionicons name="calendar-outline" size={14} color={GOLD_DARK} />
                      </View>
                    </TouchableOpacity>
                  )}
                </View>

                <View style={[styles.inputGroup, { flex: 1, minWidth: 220 }]}>
                  <ThemedText style={[styles.label, { paddingLeft: 8 }]}>Date To</ThemedText>
                  {Platform.OS === 'web' ? (
                    <View style={styles.dateWrapper}>
                      <input
                        type="date"
                        value={formData.DateTo || ''}
                        onChange={(e) => handleChange('DateTo', e.target.value)}
                        style={webDateInputStyle}
                      />
                    </View>
                  ) : (
                    <TouchableOpacity
                      style={styles.mobileDateBtn}
                      onPress={() => setShowDatePicker({ show: true, mode: 'to' })}
                    >
                      <ThemedText style={styles.mobileDateText}>
                        {formData.DateTo ? parseLocalDate(formData.DateTo).toLocaleDateString('en-GB') : ''}
                      </ThemedText>
                      <View style={styles.dateIconBox}>
                        <Ionicons name="calendar-outline" size={14} color={GOLD_DARK} />
                      </View>
                    </TouchableOpacity>
                  )}
                </View>
              </View>

              {/* Row 4: Report Type & Status */}
              <View style={styles.gridRow}>
                <View style={[styles.inputGroup, { flex: 1, minWidth: 220 }]}>
                  <ThemedText style={styles.label}>Report Type</ThemedText>
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
                  <ThemedText style={[styles.label, { paddingLeft: 8 }]}>Status</ThemedText>
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

      {/* Toast Notification */}
      {toastMessage ? (
        <View style={[styles.toast, toastType === 'success' ? styles.toastSuccess : styles.toastError]}>
          <Ionicons
            name={toastType === 'success' ? 'checkmark-circle' : 'alert-circle'}
            size={16}
            color="#FFFFFF"
            style={{ marginRight: 6 }}
          />
          <ThemedText style={styles.toastText}>{toastMessage}</ThemedText>
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
  },
  subtitle: {
    fontSize: 10,
    color: GOLD_DARK,
    fontWeight: '500',
    marginTop: -1,
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
  },
  scrollContent: {
    paddingHorizontal: 12,
    paddingTop: 10,
    paddingBottom: 40,
    alignItems: 'center',
  },
  cardWrapper: {
    width: '100%',
    maxWidth: 820,
  },
  goldCard: {
    backgroundColor: CARD_BG,
    borderRadius: 8,
    borderWidth: 1,
    borderColor: GOLD_BORDER,
    overflow: 'hidden',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.04,
    shadowRadius: 3,
    elevation: 2,
  },
  cardHeaderBanner: {
    backgroundColor: GOLD_LIGHT_BG,
    paddingHorizontal: 12,
    paddingVertical: 7,
    borderBottomWidth: 1,
    borderBottomColor: GOLD_BORDER,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
  },
  headerTitleContainer: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  headerIconBadgeSmall: {
    width: 22,
    height: 22,
    borderRadius: 4,
    backgroundColor: '#FFFFFF',
    justifyContent: 'center',
    alignItems: 'center',
    marginRight: 6,
    borderWidth: 1,
    borderColor: GOLD_BORDER,
  },
  cardHeaderTitleSmall: {
    fontSize: 12,
    fontWeight: 'bold',
    color: TEXT_DARK,
    letterSpacing: 0.3,
  },
  cardBody: {
    padding: 12,
    gap: 8,
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
    paddingVertical: 4,
    paddingHorizontal: 8,
    borderRadius: 4,
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
    height: 28,
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
    height: 28,
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
    top: 32,
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

  // Date Inputs
  dateWrapper: {
    flex: 1,
    maxWidth: 200,
    borderWidth: 1,
    borderColor: '#D4C3A3',
    borderRadius: 4,
    height: 38,
    backgroundColor: '#FFFFFF',
    flexDirection: 'row',
    alignItems: 'center',
    position: 'relative',
    overflow: 'hidden',
  },
  dateIconBox: {
    width: 34,
    height: '100%',
    borderLeftWidth: 1,
    borderLeftColor: '#E8DCB8',
    alignItems: 'center',
    justifyContent: 'center',
    backgroundColor: '#FAF5EA',
  },
  mobileDateBtn: {
    flex: 1,
    maxWidth: 200,
    borderWidth: 1,
    borderColor: '#D4C3A3',
    borderRadius: 4,
    height: 38,
    backgroundColor: '#FFFFFF',
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
  },
  mobileDateText: {
    color: TEXT_DARK,
    fontSize: 12,
    paddingLeft: 8,
    flex: 1,
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
