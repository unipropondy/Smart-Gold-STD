import React from 'react';
import { View, Text, TouchableOpacity, StyleSheet, Platform } from 'react-native';
import { Ionicons } from '@expo/vector-icons';
import { toIsoDate, toDispDate, getTodayIso } from '@/utils/dateValidation';

interface ReportDatePickerProps {
  label: string;
  value: string; // Accepts either DD/MM/YYYY or YYYY-MM-DD
  onChange: (val: string) => void; // Emits DD/MM/YYYY or YYYY-MM-DD depending on format
  onOpenNative?: () => void;
  maxDate?: string; // YYYY-MM-DD (defaults to today)
  isIsoOutput?: boolean; // If true, emits YYYY-MM-DD, otherwise DD/MM/YYYY
}

export const ReportDatePicker: React.FC<ReportDatePickerProps> = ({
  label,
  value,
  onChange,
  onOpenNative,
  maxDate = getTodayIso(),
  isIsoOutput = false,
}) => {
  const isoVal = toIsoDate(value);
  const dispVal = toDispDate(value) || value;

  return (
    <View style={styles.container}>
      <Text style={styles.label}>{label}</Text>
      <View style={styles.inputContainer}>
        {Platform.OS === 'web' ? (
          <div style={styles.webWrapper as any}>
            <input
              type="date"
              max={maxDate}
              value={isoVal || ''}
              onChange={(e: any) => {
                const rawVal = e.target.value;
                if (!rawVal) {
                  onChange('');
                  return;
                }
                // Prevent future dates directly if typed or selected
                if (maxDate && rawVal > maxDate) {
                  onChange(isIsoOutput ? maxDate : toDispDate(maxDate));
                  return;
                }
                onChange(isIsoOutput ? rawVal : toDispDate(rawVal));
              }}
              style={styles.webInput as any}
            />
          </div>
        ) : (
          <TouchableOpacity
            style={styles.nativeBtn}
            onPress={onOpenNative}
            activeOpacity={0.8}
          >
            <Text style={styles.nativeText}>{dispVal || 'DD/MM/YYYY'}</Text>
            <Ionicons name="calendar-outline" size={16} color="#B48218" />
          </TouchableOpacity>
        )}
      </View>
    </View>
  );
};

const styles: any = StyleSheet.create({
  container: {
    flex: 1,
    minWidth: 140,
  },
  label: {
    fontSize: 12,
    fontWeight: '700',
    color: '#3B2D17',
    marginBottom: 6,
    fontFamily: 'Cambria, serif',
  },
  inputContainer: {
    height: 38,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    borderRadius: 6,
    backgroundColor: '#FFFFFF',
    justifyContent: 'center',
    overflow: 'hidden',
  },
  webWrapper: {
    display: 'flex',
    flex: 1,
    height: '100%',
    width: '100%',
  } as any,
  webInput: {
    flex: 1,
    width: '100%',
    height: '100%',
    border: 'none',
    backgroundColor: 'transparent',
    outline: 'none',
    color: '#3B2D17',
    paddingLeft: 10,
    paddingRight: 6,
    fontSize: 13,
    cursor: 'pointer',
    fontFamily: 'Cambria, serif',
    boxSizing: 'border-box',
  } as any,
  nativeBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingHorizontal: 10,
    height: '100%',
  },
  nativeText: {
    fontSize: 13,
    color: '#3B2D17',
    fontFamily: 'Cambria, serif',
  },
});

