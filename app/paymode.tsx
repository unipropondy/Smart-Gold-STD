import { Ionicons } from '@expo/vector-icons';
import { Stack, useRouter } from 'expo-router';
import React, { useEffect, useState } from 'react';
import {
  ActivityIndicator,
  Alert,
  FlatList,
  Modal,
  StyleSheet,
  TextInput,
  TouchableOpacity,
  TouchableWithoutFeedback,
  useColorScheme,
  View,
  Platform,
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';
import { ThemedText } from '@/components/themed-text';
import { useThemeColors } from '@/hooks/useThemeColors';
import { useThemeContext } from '@/contexts/ThemeContext';
import { API_BASE_URL } from '@/constants/api';

const GOLD_COLOR = '#D4AF37';
const API_URL = `${API_BASE_URL}/paymode`;

export default function PaymodeScreen() {
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
  const [successMsg, setSuccessMsg] = useState('');
  const [errorMsg, setErrorMsg] = useState('');

  const showSuccess = (msg: string) => {
    setSuccessMsg(msg);
    setTimeout(() => setSuccessMsg(''), 3000);
  };

  const showError = (msg: string) => {
    setErrorMsg(msg);
    setTimeout(() => setErrorMsg(''), 3000);
  };

  // Modal State
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [isEditing, setIsEditing] = useState(false);

  // Form State
  const [payMode, setPayMode] = useState('');
  const [position, setPosition] = useState('1');
  const [description, setDescription] = useState('');
  const [commission, setCommission] = useState('0.00');
  const [active, setActive] = useState(true);

  const headerBgColor = isDark ? '#2A2A2A' : '#F5F5F5';

  const fetchPaymodes = async () => {
    try {
      const response = await fetch(`${API_URL}?t=${Date.now()}`, {
        headers: { 'Cache-Control': 'no-cache' }
      });
      const json = await response.json();
      if (json.success) {
        setData(json.data);
      } else {
        console.error('Failed to fetch:', json.message);
      }
    } catch (error) {
      console.error('Network Error:', error);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchPaymodes();
  }, []);

  const handleSave = async () => {
    if (!payMode || !payMode.trim()) {
      showError('Pay Mode is required');
      return;
    }

    const payload = {
      PayMode: payMode.trim(),
      Position: parseInt(position) || 0,
      Description: description.trim(),
      Commission: parseFloat(commission) || 0,
      Active: active,
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
        showSuccess(`Paymode ${isEditing ? 'updated' : 'added'} successfully`);
        setIsModalVisible(false);
        fetchPaymodes();
      } else {
        showError(json.message || 'Failed to save');
      }
    } catch (error) {
      console.error('Save error:', error);
      showError('Failed to save paymode');
    } finally {
      setSaving(false);
    }
  };

  const executeDelete = async (codeToDelete: string) => {
    setDeletingId(codeToDelete);
    try {
      const response = await fetch(`${API_URL}?id=${encodeURIComponent(codeToDelete)}`, { method: 'DELETE' });
      const json = await response.json();
      if (json.success) {
        showSuccess('Paymode deleted successfully');
        fetchPaymodes();
      } else {
        showError(json.message || 'Failed to delete');
      }
    } catch (error) {
      console.error('Delete error:', error);
      showError('Failed to delete paymode');
    } finally {
      setDeletingId(null);
    }
  };

  const handleDelete = (codeToDelete: string) => {
    if (Platform.OS === 'web') {
      if (window.confirm(`Are you sure you want to delete Paymode ${codeToDelete}?`)) {
        executeDelete(codeToDelete);
      }
    } else {
      Alert.alert('Confirm Delete', `Are you sure you want to delete Paymode ${codeToDelete}?`, [
        { text: 'Cancel', style: 'cancel' },
        { text: 'Delete', style: 'destructive', onPress: () => executeDelete(codeToDelete) },
      ]);
    }
  };

  const openAddModal = () => {
    setIsEditing(false);
    setPayMode('');
    setPosition('1');
    setDescription('');
    setCommission('0.00');
    setActive(true);
    setIsModalVisible(true);
  };

  const openEditModal = (item: any) => {
    setIsEditing(true);
    setPayMode(item.PayMode);
    setPosition(item.Position?.toString() || '1');
    setDescription(item.Description || '');
    setCommission(item.Commission?.toString() || '0.00');
    setActive(item.Active === 1 || item.Active === true);
    setIsModalVisible(true);
  };

  const renderItem = ({ item }: { item: any }) => (
    <View style={[styles.row, { backgroundColor: cardColor, borderColor: borderColor }]}>
      <View style={[styles.cell, { flex: 0.5 }]}>
        <ThemedText type="tableText" style={{ color: textColor, fontSize: 11 }}>{item.Position}</ThemedText>
      </View>
      <View style={[styles.cell, { flex: 1.5 }]}>
        <ThemedText type="tableText" style={{ color: textColor, fontWeight: '500', fontSize: 11 }}>{item.PayMode}</ThemedText>
      </View>
      <View style={[styles.cell, { flex: 2 }]}>
        <ThemedText type="tableText" style={{ color: textColor, fontSize: 11 }}>{item.Description}</ThemedText>
      </View>
      <View style={[styles.cell, { flex: 1 }]}>
        <ThemedText type="tableText" style={{ color: textColor, fontSize: 11 }}>{item.Commission}</ThemedText>
      </View>
      <View style={[styles.actionCell, { alignItems: 'center', justifyContent: 'center' }]}>
        <TouchableOpacity style={styles.actionButton} onPress={() => openEditModal(item)} disabled={deletingId === item.PayMode}>
          <Ionicons name="pencil-outline" size={20} color={GOLD_COLOR} />
        </TouchableOpacity>
        <TouchableOpacity style={styles.actionButton} onPress={() => handleDelete(item.PayMode)} disabled={deletingId === item.PayMode}>
          {deletingId === item.PayMode ? (
            <ActivityIndicator size="small" color="#F44336" />
          ) : (
            <Ionicons name="trash-outline" size={20} color="#F44336" />
          )}
        </TouchableOpacity>
      </View>
    </View>
  );

  const renderToast = () => {
    if (!successMsg && !errorMsg) return null;
    const isError = !!errorMsg;
    return (
      <View style={[styles.toast, { backgroundColor: isError ? '#F44336' : '#4CAF50' }]}>
        <ThemedText style={styles.toastText}>{isError ? errorMsg : successMsg}</ThemedText>
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
          <ThemedText style={[styles.title, { color: textColor }]}>Paymode Master</ThemedText>
        </View>
        <TouchableOpacity style={[styles.addButton, { backgroundColor: 'rgba(212, 175, 55, 0.15)', borderColor: GOLD_COLOR }]} onPress={openAddModal}>
          <Ionicons name="add" size={20} color={GOLD_COLOR} />
          <ThemedText style={[styles.addButtonText, { color: GOLD_COLOR }]}>New</ThemedText>
        </TouchableOpacity>
      </View>

      <View style={styles.tableContainer}>
      {/* Table Header */}
      <View style={[styles.tableHeader, { backgroundColor: 'rgba(0,0,0,0.02)' }]}>
        <View style={[styles.cell, { flex: 0.5 }]}><ThemedText style={[styles.headerText, { color: subtitleColor }]}>POS</ThemedText></View>
        <View style={[styles.cell, { flex: 1.5 }]}><ThemedText style={[styles.headerText, { color: subtitleColor }]}>PAY MODE</ThemedText></View>
        <View style={[styles.cell, { flex: 2 }]}><ThemedText style={[styles.headerText, { color: subtitleColor }]}>DESCRIPTION</ThemedText></View>
        <View style={[styles.cell, { flex: 1 }]}><ThemedText style={[styles.headerText, { color: subtitleColor }]}>COMMISSION</ThemedText></View>
        <View style={[styles.actionCell, { width: 120 }]}><ThemedText style={[styles.headerText, { color: subtitleColor, width: 55, textAlign: 'center' }]}>EDIT</ThemedText><ThemedText style={[styles.headerText, { color: subtitleColor, width: 55, textAlign: 'center' }]}>DELETE</ThemedText></View>
      </View>

      {loading ? (
        <View style={styles.centered}><ActivityIndicator size="large" color={GOLD_COLOR} /></View>
      ) : (
        <FlatList
          data={data}
          renderItem={renderItem}
          keyExtractor={(item, index) => item.PayMode || index.toString()}
          contentContainerStyle={{ paddingBottom: 20 }}
          ListEmptyComponent={
            <View style={styles.centered}>
              <ThemedText style={{ color: subtitleColor }}>No paymodes found.</ThemedText>
            </View>
          }
        />
      )}
      </View>

      {/* Modal */}
      <Modal visible={isModalVisible} transparent animationType="fade">
        <TouchableOpacity style={styles.modalOverlay} activeOpacity={1} onPress={() => setIsModalVisible(false)}>
          <TouchableWithoutFeedback>
            <View style={[styles.modalContent, { backgroundColor: cardColor, borderColor }]}>
              {renderToast()}
              <View style={[styles.modalHeader, { borderBottomColor: borderColor }]}>
                <ThemedText style={{ fontSize: 18, fontWeight: 'bold', color: textColor }}>
                  {isEditing ? 'Edit Paymode' : 'Add New Paymode'}
                </ThemedText>
                <TouchableOpacity onPress={() => setIsModalVisible(false)}>
                  <Ionicons name="close" size={24} color={textColor} />
                </TouchableOpacity>
              </View>

              <View style={styles.modalBody}>
                <View style={styles.inputGroup}>
                  <ThemedText style={[styles.label, { color: subtitleColor }]}>Pay Mode *</ThemedText>
                  <TextInput
                    style={[styles.input, { color: textColor, borderColor, backgroundColor: isDark ? '#2A2A2A' : '#FFF' }, isEditing && { backgroundColor: isDark ? '#333' : '#F0F0F0', color: subtitleColor }]}
                    value={payMode}
                    onChangeText={setPayMode}
                    editable={!isEditing}
                    placeholder="Enter Pay Mode (e.g. CASH)"
                    placeholderTextColor={subtitleColor}
                  />
                </View>

                <View style={styles.inputGroup}>
                  <ThemedText style={[styles.label, { color: subtitleColor }]}>Position</ThemedText>
                  <TextInput
                    style={[styles.input, { color: textColor, borderColor, backgroundColor: isDark ? '#2A2A2A' : '#FFF' }]}
                    value={position}
                    onChangeText={setPosition}
                    keyboardType="numeric"
                    placeholder="Position"
                    placeholderTextColor={subtitleColor}
                  />
                </View>

                <View style={styles.inputGroup}>
                  <ThemedText style={[styles.label, { color: subtitleColor }]}>Description</ThemedText>
                  <TextInput
                    style={[styles.input, { color: textColor, borderColor, backgroundColor: isDark ? '#2A2A2A' : '#FFF' }]}
                    value={description}
                    onChangeText={setDescription}
                    placeholder="Enter Description"
                    placeholderTextColor={subtitleColor}
                  />
                </View>

                <View style={styles.inputGroup}>
                  <ThemedText style={[styles.label, { color: subtitleColor }]}>Commission</ThemedText>
                  <TextInput
                    style={[styles.input, { color: textColor, borderColor, backgroundColor: isDark ? '#2A2A2A' : '#FFF' }]}
                    value={commission}
                    onChangeText={setCommission}
                    keyboardType="numeric"
                    placeholder="Commission Rate"
                    placeholderTextColor={subtitleColor}
                  />
                </View>

                {/* Active Checkbox */}
                <View style={styles.checkboxContainer}>
                  <ThemedText style={[styles.checkboxLabel, { color: subtitleColor }]}>Active</ThemedText>
                  <View style={styles.checkboxControl}>
                    <TouchableOpacity
                      style={{ flexDirection: 'row', alignItems: 'center' }}
                      onPress={() => setActive(!active)}
                    >
                      <View style={[styles.checkbox, { borderColor, backgroundColor: active ? GOLD_COLOR : 'transparent' }]}>
                        {active && <Ionicons name="checkmark" size={16} color="#FFF" />}
                      </View>
                    </TouchableOpacity>
                  </View>
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
  checkbox: {
    width: 20,
    height: 20,
    borderWidth: 1,
    borderRadius: 4,
    justifyContent: 'center',
    alignItems: 'center',
  },
  checkboxContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    marginBottom: 16,
  },
  checkboxLabel: {
    flex: 0.4,
    fontSize: 14,
    fontWeight: '600',
    marginRight: 12,
  },
  checkboxControl: {
    flex: 1,
    flexDirection: 'row',
    alignItems: 'center',
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
  btnSaveText: {  fontWeight: 'bold' },
  toast: {
    position: 'absolute',
    top: Platform.OS === 'web' ? 20 : 50,
    alignSelf: 'center',
    minWidth: 250,
    maxWidth: '90%',
    padding: 15,
    borderRadius: 8,
    zIndex: 9999,
    alignItems: 'center',
    justifyContent: 'center',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.25,
    shadowRadius: 3.84,
    elevation: 5,
  },
  toastText: {
    color: '#FFF',
    fontSize: 14,
    fontWeight: '600',
    textAlign: 'center',
  },
});
