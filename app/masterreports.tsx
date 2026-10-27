import { API_BASE_URL } from '@/constants/api';
import { Ionicons } from '@expo/vector-icons';
import { Stack, useRouter } from 'expo-router';
import React, { useState } from 'react';
import { Platform, ScrollView, StyleSheet, TouchableOpacity, View } from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';
import { ThemedText } from '../components/themed-text';

import DateTimePicker from '@react-native-community/datetimepicker';

// Royal Gold Design Palette
const GOLD_ACCENT = '#B48218';
const GOLD_DARK = '#9E6D08';
const GOLD_BORDER = '#E8DCB8';
const CARD_BG = '#FFFFFF';
const APP_BG = '#FBF9F5';
const TEXT_DARK = '#3B2D17';

// Web HTML Date Input Inline Style
const webDateInputStyle: React.CSSProperties = {
  flex: 1,
  height: '100%',
  border: 'none',
  backgroundColor: 'transparent',
  outline: 'none',
  color: TEXT_DARK,
  paddingLeft: 10,
  paddingRight: 6,
  fontSize: 13,
  cursor: 'pointer',
};

const RadioButton = ({ label, selected, onPress }: { label: string; selected: boolean; onPress: () => void }) => {
  return (
    <TouchableOpacity
      style={[styles.radioBtn, selected && styles.radioBtnSelected]}
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

const API_URL = `${API_BASE_URL}/masterreports`;

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

export default function MasterReportsScreen() {
  const router = useRouter();

  const [toastMessage, setToastMessage] = useState('');
  const [toastType, setToastType] = useState<'success' | 'error'>('success');

  const [showDatePicker, setShowDatePicker] = useState<{ show: boolean; mode: 'from' | 'to' }>({
    show: false,
    mode: 'from',
  });

  const [formData, setFormData] = useState({
    SelectOption: 'Class',
    PrintOption: 'Preview',
    CustomerDateType: 'Date of Birth',
    DateFrom: formatDateLocal(new Date()),
    DateTo: formatDateLocal(new Date()),
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
    if (formData.SelectOption === 'Customers' && (!formData.DateFrom || !formData.DateTo)) {
      showError('Date From and Date To are required for Customers report');
      return;
    }

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

      {/* LUXURY ROYAL GOLD HEADER BAR - CLEAN TITLE ONLY */}
      <View style={styles.header}>
        <View style={styles.headerLeft}>
          <TouchableOpacity onPress={() => router.back()} style={styles.backButton} activeOpacity={0.7}>
            <Ionicons name="arrow-back" size={20} color={TEXT_DARK} />
          </TouchableOpacity>
          <ThemedText style={styles.title}>Master Reports</ThemedText>
        </View>

        <TouchableOpacity style={styles.actionButton} onPressOut={handleGetReport} activeOpacity={0.8}>
          <Ionicons name="document-text-outline" size={15} color={GOLD_DARK} style={{ marginRight: 5 }} />
          <ThemedText style={styles.actionButtonText}>Get Report</ThemedText>
        </TouchableOpacity>
      </View>

      <ScrollView contentContainerStyle={styles.scrollContent}>
        <View style={styles.cardWrapper}>
          <View style={styles.goldCard}>
            <View style={styles.cardBody}>
              {/* Report Selection (Direct Pills, No Banner/Topic Clutter) */}
              <View style={styles.pillSection}>
                <View style={styles.radioRow}>
                  <RadioButton
                    label="Class"
                    selected={formData.SelectOption === 'Class'}
                    onPress={() => handleChange('SelectOption', 'Class')}
                  />
                  <RadioButton
                    label="Category"
                    selected={formData.SelectOption === 'Category'}
                    onPress={() => handleChange('SelectOption', 'Category')}
                  />
                  <RadioButton
                    label="Catalogue"
                    selected={formData.SelectOption === 'Catalogue'}
                    onPress={() => handleChange('SelectOption', 'Catalogue')}
                  />
                  <RadioButton
                    label="Tray"
                    selected={formData.SelectOption === 'Tray'}
                    onPress={() => handleChange('SelectOption', 'Tray')}
                  />
                  <RadioButton
                    label="Sales Man"
                    selected={formData.SelectOption === 'Sales Man'}
                    onPress={() => handleChange('SelectOption', 'Sales Man')}
                  />
                  <RadioButton
                    label="Suppliers"
                    selected={formData.SelectOption === 'Suppliers'}
                    onPress={() => handleChange('SelectOption', 'Suppliers')}
                  />
                  <RadioButton
                    label="Customers"
                    selected={formData.SelectOption === 'Customers'}
                    onPress={() => handleChange('SelectOption', 'Customers')}
                  />
                  <RadioButton
                    label="Customer's Mailing Address"
                    selected={formData.SelectOption === "Customer's Mailing Address"}
                    onPress={() => handleChange('SelectOption', "Customer's Mailing Address")}
                  />
                </View>
              </View>

              {/* Print / Preview Mode */}
              <View style={[styles.pillSection, { borderTopWidth: 1, borderTopColor: '#F5EFE6', paddingTop: 10 }]}>
                <View style={styles.radioRow}>
                  <RadioButton
                    label="Preview"
                    selected={formData.PrintOption === 'Preview'}
                    onPress={() => handleChange('PrintOption', 'Preview')}
                  />
                  <RadioButton
                    label="Print"
                    selected={formData.PrintOption === 'Print'}
                    onPress={() => handleChange('PrintOption', 'Print')}
                  />
                </View>
              </View>

              {/* Conditional Customer Filter Dates */}
              {formData.SelectOption === 'Customers' && (
                <View style={[styles.pillSection, { borderTopWidth: 1, borderTopColor: '#F5EFE6', paddingTop: 10 }]}>
                  <View style={[styles.radioRow, { marginBottom: 10 }]}>
                    <RadioButton
                      label="Date of Birth"
                      selected={formData.CustomerDateType === 'Date of Birth'}
                      onPress={() => handleChange('CustomerDateType', 'Date of Birth')}
                    />
                    <RadioButton
                      label="Date of Anniversary"
                      selected={formData.CustomerDateType === 'Date of Anniversary'}
                      onPress={() => handleChange('CustomerDateType', 'Date of Anniversary')}
                    />
                  </View>

                  <View style={styles.dateRow}>
                    <View style={styles.dateCol}>
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
                            {formData.DateFrom
                              ? parseLocalDate(formData.DateFrom).toLocaleDateString('en-GB')
                              : 'From Date'}
                          </ThemedText>
                          <View style={styles.dateIconBox}>
                            <Ionicons name="calendar-outline" size={15} color={GOLD_DARK} />
                          </View>
                        </TouchableOpacity>
                      )}
                    </View>

                    <View style={styles.dateCol}>
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
                            {formData.DateTo
                              ? parseLocalDate(formData.DateTo).toLocaleDateString('en-GB')
                              : 'To Date'}
                          </ThemedText>
                          <View style={styles.dateIconBox}>
                            <Ionicons name="calendar-outline" size={15} color={GOLD_DARK} />
                          </View>
                        </TouchableOpacity>
                      )}
                    </View>
                  </View>
                </View>
              )}
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
  cardBody: {
    padding: 14,
    gap: 12,
  },
  pillSection: {
    width: '100%',
  },
  radioRow: {
    flexDirection: 'row',
    flexWrap: 'wrap',
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
    fontSize: 11,
    fontWeight: '500',
    color: '#5C4A28',
  },
  radioLabelSelected: {
    color: GOLD_DARK,
    fontWeight: 'bold',
  },

  // Date controls
  dateRow: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    gap: 14,
    alignItems: 'center',
  },
  dateCol: {
    width: 200,
    maxWidth: 220,
  },
  dateWrapper: {
    width: 200,
    maxWidth: 220,
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
    width: 200,
    maxWidth: 220,
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
