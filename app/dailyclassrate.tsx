import { ThemedText } from '@/components/themed-text';
import { API_BASE_URL } from '@/constants/api';
import { useThemeContext } from '@/contexts/ThemeContext';
import { useThemeColors } from '@/hooks/useThemeColors';
import { Ionicons } from '@expo/vector-icons';
import { Stack, useRouter } from 'expo-router';
import React, { useEffect, useState } from 'react';
import {
  ActivityIndicator,
  FlatList,
  Modal,
  Platform,
  StyleSheet,
  TextInput,
  TouchableOpacity,
  TouchableWithoutFeedback,
  useColorScheme,
  View,
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';

const GOLD_COLOR = '#D4AF37';
const API_URL = `${API_BASE_URL}/dailyclassrate`;

// helper for simple YYYY-MM-DD format
function formatDate(isoString: string) {
  if (!isoString) return '';
  const d = new Date(isoString);
  if (isNaN(d.getTime())) return isoString;
  return d.toISOString().split('T')[0];
}

export default function DailyClassRateScreen() {
  const themeColors = useThemeColors();
  const { accentColor } = useThemeContext();
  const { background: backgroundColor, card: cardColor, text: textColor, border: borderColor, primaryGold: GOLD_COLOR, subtitle: subtitleColor } = themeColors;
  const colorScheme = useColorScheme();
  const isDark = colorScheme === 'dark';
  const router = useRouter();

  const [data, setData] = useState<any[]>([]);
  const [loading, setLoading] = useState(true);
  const [saving, setSaving] = useState(false);
  const [deletingId, setDeletingId] = useState<string | null>(null);
  const [toast, setToast] = useState<{ message: string; type: 'success' | 'error' } | null>(null);
  const [deleteConfirmItem, setDeleteConfirmItem] = useState<{ code: string; date: string } | null>(null);

  const showToast = (message: string, type: 'success' | 'error' = 'success') => {
    setToast({ message, type });
    setTimeout(() => setToast(null), 3500);
  };

  const showSuccess = (msg: string) => {
    showToast(msg, 'success');
  };

  const showError = (msg: string) => {
    showToast(msg, 'error');
  };

  // Modal State
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [isEditing, setIsEditing] = useState(false);
  const [originalFromDate, setOriginalFromDate] = useState('');

  // Form State
  const [classCode, setClassCode] = useState('');
  const [className, setClassName] = useState('');
  const [fromDate, setFromDate] = useState('');
  const [toDate, setToDate] = useState('');
  const [rate, setRate] = useState('');

  // LOV State
  const [classes, setClasses] = useState<any[]>([]);
  const [isClassLookupVisible, setIsClassLookupVisible] = useState(false);
  const [classSearch, setClassSearch] = useState('');

  const headerBgColor = isDark ? '#2A2A2A' : '#F5F5F5';

  const fetchRates = async () => {
    try {
      const response = await fetch(`${API_URL}?t=${Date.now()}`);
      const json = await response.json();
      if (json.success) setData(json.data);
    } catch (error) {
      console.error('Network Error:', error);
    } finally {
      setLoading(false);
    }
  };

  const fetchClassesForLookup = async () => {
    try {
      const response = await fetch(`${API_BASE_URL}/class?t=${Date.now()}`);
      const json = await response.json();
      if (json.success) setClasses(json.data);
    } catch (error) {
      console.error('Failed to fetch classes:', error);
    }
  };

  useEffect(() => {
    fetchRates();
    fetchClassesForLookup();
  }, []);

  // 1. txtClassCode blur validation & auto-lookup matching txtClassCode_LostFocus in frmClassRate.vb
  const handleClassCodeBlur = async () => {
    const trimmed = (classCode || '').trim();
    if (!trimmed) {
      showError('ClassCode should not be blank.');
      setClassName('');
      return;
    }

    try {
      const res = await fetch(`${API_URL}/validate-class/${encodeURIComponent(trimmed)}`);
      const json = await res.json();
      if (json.success && json.data) {
        setClassName(json.data.ClassName || '');
      } else {
        showError('Please enter valid ClassCode.');
        setClassCode('');
        setClassName('');
      }
    } catch (e) {
      const matched = classes.find((c: any) => (c.ClassCode || '').trim().toUpperCase() === trimmed.toUpperCase());
      if (matched) {
        setClassName(matched.ClassName || '');
      } else {
        showError('Please enter valid ClassCode.');
        setClassCode('');
        setClassName('');
      }
    }
  };

  // 2. txtRate blur validation matching txtRate_Validating in frmClassRate.vb
  const handleRateBlur = () => {
    const trimmed = (rate || '').trim();
    if (!trimmed) {
      showError('Rate Should not be empty.');
      return;
    }
    if (isNaN(Number(trimmed))) {
      showError('Rate Should be Numeric.');
      setRate('');
    }
  };

  const handleSave = async () => {
    // Form validation matching fncValidateMe in frmClassRate.vb (lines 551-586)
    if (!classCode || !classCode.trim()) {
      showError('ClassCode should not be blank.');
      return;
    }
    if (!fromDate || !fromDate.trim() || isNaN(new Date(fromDate.trim()).getTime())) {
      showError('Please enter valid from date.');
      return;
    }
    if (!toDate || !toDate.trim() || isNaN(new Date(toDate.trim()).getTime())) {
      showError('Please enter valid to date.');
      return;
    }

    const fDate = new Date(fromDate.trim());
    const tDate = new Date(toDate.trim());
    if (tDate.getTime() < fDate.getTime()) {
      showError('To date should be greater than From date.');
      return;
    }

    if (!rate || !rate.trim()) {
      showError('Rate Should not be empty.');
      return;
    }

    const numRate = parseFloat(rate.trim());
    if (isNaN(numRate)) {
      showError('Rate Should be Numeric.');
      return;
    }

    const payload = {
      ClassCode: classCode.trim().toUpperCase(),
      ClassName: className.trim(),
      FromDate: fromDate.trim(),
      ToDate: toDate.trim(),
      Rate: numRate,
      OriginalFromDate: isEditing ? originalFromDate : undefined
    };

    setSaving(true);
    try {
      const method = isEditing ? 'PUT' : 'POST';
      const response = await fetch(API_URL, {
        method,
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload),
      });

      const json = await response.json();
      if (json.success) {
        showSuccess(json.message || 'Class Rate detail saved sucssfully.');
        setIsModalVisible(false);
        fetchRates();
      } else {
        showError(json.message || 'Error in Save');
      }
    } catch (error) {
      console.error('Save error:', error);
      showError('Failed to save class rate');
    } finally {
      setSaving(false);
    }
  };

  const executeDelete = async (code: string, date: string) => {
    setDeletingId(`${code}_${date}`);
    try {
      const response = await fetch(`${API_URL}?code=${encodeURIComponent(code)}&date=${encodeURIComponent(date)}`, { method: 'DELETE' });
      const json = await response.json();
      if (json.success) {
        showSuccess(json.message || 'Class Rate Deleted.');
        fetchRates();
      } else {
        showError(json.message || 'Error in Delete');
      }
    } catch (error) {
      console.error('Delete error:', error);
      showError('Failed to delete class rate');
    } finally {
      setDeletingId(null);
    }
  };

  const handleDelete = (code: string, date: string) => {
    if (!code || !date) {
      showError('Please select the Class Rate to Delete?');
      return;
    }
    setDeleteConfirmItem({ code, date });
  };

  const openAddModal = () => {
    const today = new Date().toISOString().split('T')[0];
    setIsEditing(false);
    setClassCode('');
    setClassName('');
    setFromDate(today);
    setToDate(today);
    setRate('');
    setOriginalFromDate('');
    setIsModalVisible(true);
  };

  const openEditModal = (item: any) => {
    setIsEditing(true);
    setClassCode(item.ClassCode);

    const matchedClass = classes.find((c: any) => c.ClassCode === item.ClassCode);
    setClassName(matchedClass ? matchedClass.ClassName : (item.ClassName || ''));

    setFromDate(formatDate(item.FromDate));
    setToDate(item.ToDate ? formatDate(item.ToDate) : '');
    setRate(item.Rate !== null ? item.Rate.toString() : '');
    setOriginalFromDate(item.FromDate);
    setIsModalVisible(true);
  };

  const renderItem = ({ item }: { item: any }) => (
    <View style={[styles.row, { backgroundColor: cardColor, borderColor: borderColor }]}>
      <View style={[styles.cell, { flex: 1.5 }]}>
        <ThemedText type="tableText" style={{ color: textColor, fontWeight: '500', fontSize: 11 }}>{item.ClassCode}</ThemedText>
      </View>
      <View style={[styles.cell, { flex: 2 }]}>
        <ThemedText type="tableText" style={{ color: textColor, fontSize: 11 }}>{formatDate(item.FromDate)}</ThemedText>
      </View>
      <View style={[styles.cell, { flex: 2 }]}>
        <ThemedText type="tableText" style={{ color: textColor, fontSize: 11 }}>{item.ToDate ? formatDate(item.ToDate) : '-'}</ThemedText>
      </View>
      <View style={[styles.cell, { flex: 1.5, alignItems: 'center' }]}>
        <ThemedText type="tableText" style={{ color: GOLD_COLOR, fontWeight: 'bold', fontSize: 11 }}>{item.Rate}</ThemedText>
      </View>
      <View style={[styles.actionCell, { alignItems: 'center', justifyContent: 'center' }]}>
        <TouchableOpacity style={styles.actionButton} onPress={() => openEditModal(item)} disabled={deletingId === `${item.ClassCode}_${item.FromDate}`}>
          <Ionicons name="pencil-outline" size={20} color={GOLD_COLOR} />
        </TouchableOpacity>
        <TouchableOpacity style={styles.actionButton} onPress={() => handleDelete(item.ClassCode, item.FromDate)} disabled={deletingId === `${item.ClassCode}_${item.FromDate}`}>
          {deletingId === `${item.ClassCode}_${item.FromDate}` ? (
            <ActivityIndicator size="small" color="#F44336" />
          ) : (
            <Ionicons name="trash-outline" size={20} color="#F44336" />
          )}
        </TouchableOpacity>
      </View>
    </View>
  );

  const renderToast = () => {
    if (!toast) return null;
    const isError = toast.type === 'error';
    return (
      <View style={[styles.toast, isError ? styles.toastError : styles.toastSuccess]}>
        <Ionicons
          name={isError ? 'alert-circle' : 'checkmark-circle'}
          size={16}
          color="#FFFFFF"
          style={{ marginRight: 6 }}
        />
        <ThemedText style={styles.toastText}>{toast.message}</ThemedText>
      </View>
    );
  };

  return (
    <SafeAreaView style={[styles.safeArea, { backgroundColor }]} edges={['top']}>
      <Stack.Screen options={{ headerShown: false }} />
      {!isModalVisible && renderToast()}
      {/* Header */}
      <View style={[styles.header, { borderBottomColor: borderColor }]}>
        <View style={{ flexDirection: 'row', alignItems: 'center' }}>
          <TouchableOpacity style={styles.backButton} onPress={() => router.back()}>
            <Ionicons name="arrow-back" size={24} color={textColor} />
          </TouchableOpacity>
          <ThemedText style={[styles.title, { color: textColor }]}>Daily Class Rate</ThemedText>
        </View>
        <TouchableOpacity style={[styles.addButton, { backgroundColor: 'rgba(212, 175, 55, 0.15)', borderColor: GOLD_COLOR }]} onPress={openAddModal}>
          <Ionicons name="add" size={20} color={GOLD_COLOR} />
          <ThemedText style={[styles.addButtonText, { color: GOLD_COLOR }]}>New</ThemedText>
        </TouchableOpacity>
      </View>

      <View style={styles.tableContainer}>
        {/* Table Header */}
        <View style={[styles.tableHeader, { backgroundColor: 'rgba(0,0,0,0.02)' }]}>
          <View style={[styles.cell, { flex: 1.5 }]}><ThemedText style={[styles.headerText, { color: subtitleColor }]}>CLASS CODE</ThemedText></View>
          <View style={[styles.cell, { flex: 2 }]}><ThemedText style={[styles.headerText, { color: subtitleColor }]}>FROM DATE</ThemedText></View>
          <View style={[styles.cell, { flex: 2 }]}><ThemedText style={[styles.headerText, { color: subtitleColor }]}>TO DATE</ThemedText></View>
          <View style={[styles.cell, { flex: 1.5, alignItems: 'center' }]}><ThemedText style={[styles.headerText, { color: subtitleColor }]}>RATE</ThemedText></View>
          <View style={[styles.actionCell, { alignItems: 'center', justifyContent: 'center' }]}>
            <ThemedText style={[styles.headerText, { color: subtitleColor, width: 55, textAlign: 'center' }]}>EDIT</ThemedText>
            <ThemedText style={[styles.headerText, { color: subtitleColor, width: 55, textAlign: 'center' }]}>DELETE</ThemedText>
          </View>
        </View>

        {loading ? (
          <View style={styles.centered}><ActivityIndicator size="large" color={GOLD_COLOR} /></View>
        ) : (
          <FlatList
            data={data}
            renderItem={renderItem}
            keyExtractor={(item, index) => `${item.ClassCode}_${item.FromDate}_${index}`}
            contentContainerStyle={{ paddingBottom: 20 }}
            ListEmptyComponent={
              <View style={styles.centered}>
                <ThemedText style={{ color: subtitleColor }}>No class rates found.</ThemedText>
              </View>
            }
          />
        )}
      </View>

      {/* Modal */}
      <Modal visible={isModalVisible} transparent animationType="fade">
        <TouchableOpacity style={styles.modalOverlay} activeOpacity={1} onPress={() => setIsModalVisible(false)}>
          {renderToast()}
          <TouchableWithoutFeedback>
            <View style={[styles.modalContent, { backgroundColor: cardColor, borderColor }]}>
              <View style={[styles.modalHeader, { borderBottomColor: borderColor }]}>
                <ThemedText style={{ fontSize: 18, fontWeight: 'bold', color: textColor }}>
                  {isEditing ? 'Edit Class Rate' : 'Add New Class Rate'}
                </ThemedText>
                <TouchableOpacity onPress={() => setIsModalVisible(false)}>
                  <Ionicons name="close" size={24} color={textColor} />
                </TouchableOpacity>
              </View>

              <View style={styles.modalBody}>

                {/* Class Lookup Row */}
                <View style={styles.inputGroup}>
                  <ThemedText style={[styles.label, { color: subtitleColor }]}>Class Code *</ThemedText>
                  <View style={styles.lookupContainer}>
                    <TextInput
                      style={[styles.input, { color: textColor, borderColor, backgroundColor: isDark ? '#2A2A2A' : '#FFF' }, isEditing && { backgroundColor: isDark ? '#333' : '#F0F0F0', color: subtitleColor }]}
                      value={classCode}
                      onChangeText={(t) => setClassCode(t.toUpperCase())}
                      onBlur={handleClassCodeBlur}
                      editable={!isEditing}
                      placeholder="Enter Class Code"
                      placeholderTextColor={subtitleColor}
                      autoCapitalize="characters"
                    />
                    {!isEditing && (
                      <TouchableOpacity style={styles.lookupButton} onPress={() => setIsClassLookupVisible(true)}>
                        <ThemedText style={{ color: textColor, fontWeight: 'bold' }}>...</ThemedText>
                      </TouchableOpacity>
                    )}
                  </View>
                </View>

                <View style={styles.inputGroup}>
                  <ThemedText style={[styles.label, { color: subtitleColor }]}>Class Name</ThemedText>
                  <TextInput
                    style={[styles.input, { color: subtitleColor, borderColor, backgroundColor: isDark ? '#333' : '#F0F0F0' }]}
                    value={className}
                    editable={false}
                    placeholder="Class Name"
                    placeholderTextColor={subtitleColor}
                  />
                </View>

                <View style={styles.inputGroup}>
                  <ThemedText style={[styles.label, { color: subtitleColor }]}>From Date *</ThemedText>
                  {Platform.OS === 'web' ? (
                    <input
                      type="date"
                      value={fromDate}
                      onChange={(e) => setFromDate(e.target.value)}
                      style={{
                        flex: 1,
                        borderWidth: '1px',
                        borderStyle: 'solid',
                        borderRadius: '6px',
                        padding: '10px 12px',
                        fontSize: '14px',
                        borderColor: borderColor as string,
                        backgroundColor: (isDark ? '#2A2A2A' : '#FFF'),
                        color: (textColor as string),
                        fontFamily: 'Cambria'
                      }}
                    />
                  ) : (
                    <TextInput
                      style={[styles.input, { color: textColor, borderColor, backgroundColor: isDark ? '#2A2A2A' : '#FFF' }]}
                      value={fromDate}
                      onChangeText={setFromDate}
                      placeholder="YYYY-MM-DD"
                      placeholderTextColor={subtitleColor}
                    />
                  )}
                </View>

                <View style={styles.inputGroup}>
                  <ThemedText style={[styles.label, { color: subtitleColor }]}>To Date *</ThemedText>
                  {Platform.OS === 'web' ? (
                    <input
                      type="date"
                      value={toDate}
                      onChange={(e) => setToDate(e.target.value)}
                      style={{
                        flex: 1,
                        borderWidth: '1px',
                        borderStyle: 'solid',
                        borderRadius: '6px',
                        padding: '10px 12px',
                        fontSize: '14px',
                        borderColor: borderColor as string,
                        backgroundColor: (isDark ? '#2A2A2A' : '#FFF'),
                        color: (textColor as string),
                        fontFamily: 'Cambria'
                      }}
                    />
                  ) : (
                    <TextInput
                      style={[styles.input, { color: textColor, borderColor, backgroundColor: isDark ? '#2A2A2A' : '#FFF' }]}
                      value={toDate}
                      onChangeText={setToDate}
                      placeholder="YYYY-MM-DD"
                      placeholderTextColor={subtitleColor}
                    />
                  )}
                </View>

                <View style={styles.inputGroup}>
                  <ThemedText style={[styles.label, { color: subtitleColor }]}>Rate *</ThemedText>
                  <TextInput
                    style={[styles.input, { color: textColor, borderColor, backgroundColor: isDark ? '#2A2A2A' : '#FFF' }]}
                    value={rate}
                    onChangeText={setRate}
                    onBlur={handleRateBlur}
                    keyboardType="numeric"
                    placeholder="Enter Rate (e.g. 150.00)"
                    placeholderTextColor={subtitleColor}
                  />
                </View>
              </View>

              <View style={[styles.modalFooter, { borderTopColor: borderColor }]}>
                <TouchableOpacity style={[styles.btn, styles.btnCancel]} onPress={() => setIsModalVisible(false)}>
                  <ThemedText style={{ color: '#666', fontWeight: '600' }}>Cancel</ThemedText>
                </TouchableOpacity>
                <TouchableOpacity style={[styles.btn, styles.btnSave, saving && { opacity: 0.7 }]} onPress={handleSave} disabled={saving}>
                  {saving ? <ActivityIndicator size="small" color="#FFF" /> : <ThemedText style={{ color: textColor, fontWeight: '600' }}>Save</ThemedText>}
                </TouchableOpacity>
              </View>
            </View>
          </TouchableWithoutFeedback>
        </TouchableOpacity>
      </Modal>

      {/* Class Lookup Modal */}
      <Modal visible={isClassLookupVisible} transparent animationType="slide">
        <View style={styles.modalOverlay}>
          <View style={[styles.modalContent, { backgroundColor: cardColor, borderColor, height: '80%' }]}>
            <View style={[styles.modalHeader, { borderBottomColor: borderColor }]}>
              <ThemedText style={{ fontSize: 18, fontWeight: 'bold', color: textColor }}>
                Select Class
              </ThemedText>
              <TouchableOpacity onPress={() => setIsClassLookupVisible(false)}>
                <Ionicons name="close" size={24} color={textColor} />
              </TouchableOpacity>
            </View>
            <View style={{ padding: 16, borderBottomWidth: 1, borderBottomColor: borderColor }}>
              <TextInput
                style={[styles.input, { color: textColor, borderColor, backgroundColor: isDark ? '#2A2A2A' : '#FFF' }]}
                placeholder="Search Classes..."
                placeholderTextColor={subtitleColor}
                value={classSearch}
                onChangeText={setClassSearch}
              />
            </View>
            <FlatList
              data={classes.filter(c =>
                (c.ClassCode || '').toLowerCase().includes(classSearch.toLowerCase()) ||
                (c.ClassName || '').toLowerCase().includes(classSearch.toLowerCase())
              )}
              keyExtractor={(item) => item.ClassCode}
              renderItem={({ item }) => (
                <TouchableOpacity
                  style={{ padding: 16, borderBottomWidth: 1, borderBottomColor: borderColor, flexDirection: 'row' }}
                  onPress={() => {
                    setClassCode(item.ClassCode);
                    setClassName(item.ClassName);
                    setIsClassLookupVisible(false);
                    setClassSearch('');
                  }}
                >
                  <ThemedText style={{ color: textColor, flex: 1, fontWeight: 'bold' }}>{item.ClassCode}</ThemedText>
                  <ThemedText style={{ color: textColor, flex: 2 }}>{item.ClassName}</ThemedText>
                </TouchableOpacity>
              )}
              ListEmptyComponent={
                <View style={[styles.centered, { marginTop: 20 }]}>
                  <ThemedText style={{ color: subtitleColor }}>No classes found.</ThemedText>
                </View>
              }
            />
          </View>
        </View>
      </Modal>

      {/* Delete Confirmation Modal */}
      <Modal visible={!!deleteConfirmItem} animationType="fade" transparent={true}>
        <TouchableWithoutFeedback onPress={() => setDeleteConfirmItem(null)}>
          <View style={styles.modalOverlay}>
            <TouchableWithoutFeedback>
              <View style={[styles.modalContent, { backgroundColor: cardColor, borderColor, maxWidth: 420, padding: 24 }]}>
                <View style={{ flexDirection: 'row', alignItems: 'center', marginBottom: 16 }}>
                  <Ionicons name="trash-outline" size={24} color="#C62828" style={{ marginRight: 10 }} />
                  <ThemedText style={{ fontSize: 18, fontWeight: 'bold', color: textColor }}>Delete Class Rate</ThemedText>
                </View>
                <ThemedText style={{ color: textColor, fontSize: 14, marginBottom: 24, lineHeight: 22 }}>
                  {`Do you want to delete rate for `}
                  <ThemedText style={{ fontWeight: 'bold' }}>
                    {deleteConfirmItem?.code ? deleteConfirmItem.code.trim() : ''}
                  </ThemedText>
                  {` on `}
                  <ThemedText style={{ fontWeight: '600' }}>
                    {deleteConfirmItem ? formatDate(deleteConfirmItem.date) : ''}
                  </ThemedText>
                  {`?`}
                </ThemedText>
                <View style={{ flexDirection: 'row', justifyContent: 'flex-end', gap: 12 }}>
                  <TouchableOpacity
                    style={[styles.btn, styles.btnCancel]}
                    onPress={() => setDeleteConfirmItem(null)}
                  >
                    <ThemedText style={{ color: '#666', fontWeight: '600' }}>Cancel</ThemedText>
                  </TouchableOpacity>
                  <TouchableOpacity
                    style={[styles.btn, { backgroundColor: '#C62828' }]}
                    onPress={() => {
                      const item = deleteConfirmItem;
                      setDeleteConfirmItem(null);
                      if (item) executeDelete(item.code, item.date);
                    }}
                  >
                    <ThemedText style={{ color: '#FFF', fontWeight: '600' }}>Delete</ThemedText>
                  </TouchableOpacity>
                </View>
              </View>
            </TouchableWithoutFeedback>
          </View>
        </TouchableWithoutFeedback>
      </Modal>

    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  safeArea: { flex: 1 },
  header: { flexDirection: 'row', alignItems: 'center', justifyContent: 'space-between', paddingHorizontal: 16, paddingVertical: 8, borderBottomWidth: 1 },
  backButton: { padding: 4, marginRight: 12 },
  title: { fontSize: 20, fontWeight: '300', letterSpacing: 1.5 },
  addButton: { flexDirection: 'row', alignItems: 'center', paddingHorizontal: 12, paddingVertical: 6, borderRadius: 8, borderWidth: 1 },
  addButtonText: { marginLeft: 4, fontWeight: '600', fontSize: 14 },
  tableContainer: { flex: 1, margin: 15, maxWidth: 1100, width: '100%', alignSelf: 'center', padding: 10 },
  tableHeader: { flexDirection: 'row', paddingVertical: 10, paddingHorizontal: 20, marginBottom: 8, borderRadius: 12 },
  headerText: { fontSize: 10, fontWeight: 'bold', letterSpacing: 1 },
  row: { flexDirection: 'row', paddingVertical: 10, paddingHorizontal: 20, marginBottom: 8, borderRadius: 12, alignItems: 'center', shadowColor: '#000', shadowOffset: { width: 0, height: 2 }, shadowOpacity: 0.05, shadowRadius: 6, elevation: 2, borderWidth: 1 },
  cell: { justifyContent: 'center' },
  actionCell: { flexDirection: 'row', justifyContent: 'center', width: 120 },
  actionButton: { padding: 6, marginHorizontal: 4, borderRadius: 8, backgroundColor: 'rgba(0,0,0,0.03)', alignItems: 'center', justifyContent: 'center' },
  centered: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    marginTop: 40,
  },
  modalOverlay: {
    flex: 1,
    backgroundColor: 'rgba(0,0,0,0.5)',
    justifyContent: 'center',
    alignItems: 'center',
    padding: 20,
  },
  modalContent: {
    width: '100%',
    maxWidth: 500,
    borderWidth: 1,
    borderRadius: 12,
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.3,
    shadowRadius: 8,
    elevation: 10,
  },
  modalHeader: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    padding: 20,
    borderBottomWidth: 1,
  },
  modalBody: { padding: 20 },
  inputGroup: {
    flexDirection: 'row',
    alignItems: 'center',
    marginBottom: 16
  },
  label: {
    flex: 0.4,
    fontSize: 14,
    fontWeight: '600',
    marginRight: 12,
  },
  input: {
    flex: 1,
    borderWidth: 1,
    borderRadius: 6,
    paddingHorizontal: 12,
    paddingVertical: 10,
    fontSize: 14,
  },
  lookupContainer: {
    flex: 1,
    flexDirection: 'row',
    gap: 8,
  },
  lookupButton: {
    backgroundColor: '#F5B041',
    paddingHorizontal: 16,
    justifyContent: 'center',
    alignItems: 'center',
    borderRadius: 6,
  },
  modalFooter: {
    flexDirection: 'row',
    justifyContent: 'flex-end',
    gap: 12,
    padding: 20,
    borderTopWidth: 1,
  },
  btn: {
    paddingHorizontal: 20,
    paddingVertical: 10,
    borderRadius: 6,
    minWidth: 100,
    alignItems: 'center',
  },
  btnCancel: {
    backgroundColor: 'transparent',
    borderWidth: 1,
    borderColor: '#CCC',
  },
  btnSave: { backgroundColor: '#D4AF37' },
  btnSaveText: { fontWeight: 'bold' },
  toast: {
    position: 'absolute',
    top: Platform.OS === 'web' ? 20 : 50,
    alignSelf: 'center',
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 16,
    paddingVertical: 10,
    borderRadius: 8,
    zIndex: 999999,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.25,
    shadowRadius: 3.84,
    elevation: 20,
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
    textAlign: 'center',
  },
});
