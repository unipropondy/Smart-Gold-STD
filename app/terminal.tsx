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
  useColorScheme,
  View,
  Platform,
  ScrollView,
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';
import { ThemedText } from '@/components/themed-text';
import { useThemeColors } from '@/hooks/useThemeColors';
import { useThemeContext } from '@/contexts/ThemeContext';
import { API_BASE_URL } from '@/constants/api';

const GOLD_COLOR = '#D4AF37';
const API_URL = `${API_BASE_URL}/terminal`;

const EMPTY_FORM = {
  TerminalCode: '',
  TerminalName: '',
  LocationCode: 'HQ',
  ComputerName: '',
  TillAmount: '0',
  CustDisplayAttached: '0',
  CusDisplayPortNo: '',
  BarCodeAttached: '0',
  BarcodePortNo: '',
  PrinterAttached: '0',
  PrinterType: '1',
  CashDrawerAttached: '0',
  DrawerPortNo: '',
  Remarks: '',
  VoiceEnabled: '0',
  PrinterRequired: '0'
};

export default function TerminalScreen() {
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
  const [formData, setFormData] = useState<any>({ ...EMPTY_FORM });

  const headerBgColor = isDark ? '#2A2A2A' : '#F5F5F5';

  const fetchTerminals = async () => {
    try {
      const response = await fetch(`${API_URL}?t=${Date.now()}`, {
        headers: { 'Cache-Control': 'no-cache', 'x-db-name': 'smartgold' }
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
    fetchTerminals();
  }, []);

  const handleChange = (field: string, value: string) => {
    setFormData((prev: any) => ({ ...prev, [field]: value }));
  };

  const handleSave = async () => {
    if (!formData.TerminalCode || !String(formData.TerminalCode).trim()) {
      showError('Terminal Code is required');
      return;
    }
    if (!formData.TerminalName || !String(formData.TerminalName).trim()) {
      showError('Terminal Name is required');
      return;
    }

    setSaving(true);
    try {
      const method = isEditing ? 'PUT' : 'POST';
      const url = isEditing ? `${API_URL}/${formData.TerminalCode}` : API_URL;

      const response = await fetch(url, {
        method,
        headers: { 
          'Content-Type': 'application/json',
          'x-db-name': 'smartgold'
        },
        body: JSON.stringify(formData),
      });

      const json = await response.json();
      if (json.success) {
        showSuccess(`Terminal ${isEditing ? 'updated' : 'added'} successfully`);
        
        setIsModalVisible(false);
        fetchTerminals();
      } else {
        showError(json.message || 'Failed to save');
      }
    } catch (error) {
      console.error('Save error:', error);
      showError('Failed to save terminal');
    } finally {
      setSaving(false);
    }
  };

  const executeDelete = async (id: string) => {
    setDeletingId(id);
    try {
      const response = await fetch(`${API_URL}/${encodeURIComponent(id)}`, { 
        method: 'DELETE',
        headers: { 'x-db-name': 'smartgold' }
      });
      const json = await response.json();
      if (json.success) {
        showSuccess('Terminal deleted successfully');
        fetchTerminals();
      } else {
        showError(json.message || 'Failed to delete');
      }
    } catch (error) {
      console.error('Delete error:', error);
      showError('Failed to delete terminal');
    } finally {
      setDeletingId(null);
    }
  };

  const handleDelete = (id: string) => {
    if (Platform.OS === 'web') {
      if (window.confirm(`Are you sure you want to delete Terminal ${id}?`)) {
        executeDelete(id);
      }
    } else {
      Alert.alert('Confirm Delete', `Are you sure you want to delete Terminal ${id}?`, [
        { text: 'Cancel', style: 'cancel' },
        { text: 'Delete', style: 'destructive', onPress: () => executeDelete(id) },
      ]);
    }
  };

  const openAddModal = () => {
    setIsEditing(false);
    setFormData({ ...EMPTY_FORM });
    setIsModalVisible(true);
  };

  const openEditModal = (item: any) => {
    setIsEditing(true);
    const cleanData = Object.keys(item).reduce((acc: any, key) => {
      acc[key] = item[key] === null ? '' : String(item[key]);
      return acc;
    }, {});
    setFormData({ ...EMPTY_FORM, ...cleanData });
    setIsModalVisible(true);
  };

  const renderHorizontalInput = (label: string, field: string, maxLength?: number, short?: boolean) => {
    return (
      <View style={{ flexDirection: 'row', alignItems: 'center', marginBottom: 12, marginRight: short ? 30 : 0 }}>
        <View style={{ width: 140 }}>
          <ThemedText style={{ fontSize: 13, fontWeight: '600', color: textColor }}>
            {label}
          </ThemedText>
        </View>
        <TextInput
          style={[{ borderWidth: 1, borderRadius: 4, padding: 8, fontSize: 13, color: textColor, borderColor, backgroundColor: isDark ? '#2A2A2A' : '#FFF' }, short ? { width: 120 } : { width: 320 }, (isEditing && field === 'TerminalCode') && { backgroundColor: isDark ? '#333' : '#F0F0F0', color: subtitleColor }]}
          value={formData[field]}
          onChangeText={(val) => handleChange(field, val)}
          editable={!(isEditing && field === 'TerminalCode')}
          maxLength={maxLength}
        />
      </View>
    );
  };

  const renderHorizontalCheckbox = (label: string, field: string) => {
    const isChecked = formData[field] === '1' || formData[field] === 1;
    return (
      <View style={{ flexDirection: 'row', alignItems: 'center', marginBottom: 12 }}>
        <View style={{ width: 150 }}>
          <ThemedText style={{ fontSize: 13, fontWeight: '600', color: textColor }}>
            {label}
          </ThemedText>
        </View>
        <TouchableOpacity onPress={() => handleChange(field, isChecked ? '0' : '1')}>
          <Ionicons
            name={isChecked ? "checkbox" : "square-outline"}
            size={22}
            color={isChecked ? GOLD_COLOR : subtitleColor}
          />
        </TouchableOpacity>
      </View>
    );
  };

  const renderItem = ({ item }: { item: any }) => (
    <View style={[styles.row, { backgroundColor: cardColor, borderColor: borderColor }]}>
      <View style={[styles.cell, { flex: 1.5 }]}>
        <ThemedText type="tableText" style={{ color: textColor, fontWeight: '500', fontSize: 11 }}>{item.TerminalCode}</ThemedText>
      </View>
      <View style={[styles.cell, { flex: 2 }]}>
        <ThemedText type="tableText" style={{ color: textColor, fontSize: 11 }}>{item.TerminalName || '-'}</ThemedText>
      </View>
      <View style={[styles.cell, { flex: 1.5 }]}>
        <ThemedText type="tableText" style={{ color: textColor, fontSize: 11 }}>{item.LocationCode || '-'}</ThemedText>
      </View>
      <View style={[styles.cell, { flex: 1.5 }]}>
        <ThemedText type="tableText" style={{ color: textColor, fontSize: 11 }}>{item.ComputerName || '-'}</ThemedText>
      </View>
      <View style={styles.actionCell}>
        <TouchableOpacity style={styles.actionButton} onPress={() => openEditModal(item)} disabled={deletingId === item.TerminalCode}>
          <Ionicons name="pencil-outline" size={20} color={GOLD_COLOR} />
        </TouchableOpacity>
        <TouchableOpacity style={styles.actionButton} onPress={() => handleDelete(item.TerminalCode)} disabled={deletingId === item.TerminalCode}>
          {deletingId === item.TerminalCode ? (
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
          <ThemedText style={[styles.title, { color: textColor }]}>Terminal Master</ThemedText>
        </View>
        <TouchableOpacity style={[styles.addButton, { backgroundColor: 'rgba(212, 175, 55, 0.15)', borderColor: GOLD_COLOR }]} onPress={openAddModal}>
          <Ionicons name="add" size={20} color={GOLD_COLOR} />
          <ThemedText style={[styles.addButtonText, { color: GOLD_COLOR }]}>New</ThemedText>
        </TouchableOpacity>
      </View>

      <View style={styles.tableContainer}>
      {/* Table Header */}
      <View style={[styles.tableHeader, { backgroundColor: 'rgba(0,0,0,0.02)' }]}>
        <View style={[styles.cell, { flex: 1.5 }]}><ThemedText style={[styles.headerText, { color: subtitleColor }]}>CODE</ThemedText></View>
        <View style={[styles.cell, { flex: 2 }]}><ThemedText style={[styles.headerText, { color: subtitleColor }]}>NAME</ThemedText></View>
        <View style={[styles.cell, { flex: 1.5 }]}><ThemedText style={[styles.headerText, { color: subtitleColor }]}>LOCATION</ThemedText></View>
        <View style={[styles.cell, { flex: 1.5 }]}><ThemedText style={[styles.headerText, { color: subtitleColor }]}>COMPUTER</ThemedText></View>
        <View style={[styles.actionCell, { width: 120 }]}>
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
          keyExtractor={(item, index) => item.TerminalCode ? String(item.TerminalCode) : index.toString()}
          contentContainerStyle={{ paddingBottom: 20 }}
          ListEmptyComponent={
            <View style={styles.centered}>
              <ThemedText style={{ color: subtitleColor }}>No terminals found.</ThemedText>
            </View>
          }
        />
      )}
      </View>

      {/* Modal */}
      <Modal visible={isModalVisible} transparent={true} animationType="fade">
        <View style={{ flex: 1, backgroundColor: 'rgba(0,0,0,0.5)', justifyContent: 'center', alignItems: 'center' }}>
          {renderToast()}
          <View style={{ width: '100%', maxWidth: 550, backgroundColor: cardColor, borderRadius: 12, overflow: 'hidden', maxHeight: '90%', shadowColor: textColor, shadowOffset: { width: 0, height: 4 }, shadowOpacity: 0.3, shadowRadius: 8, elevation: 10 }}>
            
            <View style={[styles.modalHeader, { borderBottomColor: borderColor, paddingVertical: 12, paddingHorizontal: 20 }]}>
              <ThemedText style={{ fontSize: 16, fontWeight: 'bold', color: textColor }}>
                {isEditing ? 'Edit Terminal' : 'Add New Terminal'}
              </ThemedText>
              <TouchableOpacity onPress={() => setIsModalVisible(false)}>
                <Ionicons name="close" size={24} color={textColor} />
              </TouchableOpacity>
            </View>

            <ScrollView style={{ padding: 20 }} contentContainerStyle={{ paddingBottom: 20 }}>
              <View style={{ flexDirection: 'column' }}>
                  
                  {renderHorizontalInput('Terminal Code', 'TerminalCode', 50)}
                  {renderHorizontalInput('Terminal Name', 'TerminalName', 100)}
                  {renderHorizontalInput('Computer Name', 'ComputerName', 100)}
                  {renderHorizontalInput('Location Code', 'LocationCode', 50)}

                  <View style={{ flexDirection: 'row' }}>
                    {renderHorizontalInput('Cust DisplayPort No', 'CusDisplayPortNo', 50, true)}
                    {renderHorizontalCheckbox('Cust Display Attached', 'CustDisplayAttached')}
                  </View>

                  <View style={{ flexDirection: 'row' }}>
                    {renderHorizontalInput('BarCode Port No', 'BarcodePortNo', 50, true)}
                    {renderHorizontalCheckbox('BarCode Attached', 'BarCodeAttached')}
                  </View>

                  <View style={{ flexDirection: 'row' }}>
                    {renderHorizontalInput('Cash Drawer Port No', 'DrawerPortNo', 50, true)}
                    {renderHorizontalCheckbox('Cash Drawer Attached', 'CashDrawerAttached')}
                  </View>

                  <View style={{ flexDirection: 'row' }}>
                    {renderHorizontalInput('Remarks', 'Remarks', 255, true)}
                    {renderHorizontalCheckbox('Voice Enabled', 'VoiceEnabled')}
                  </View>

                  {renderHorizontalCheckbox('Printer Attached', 'PrinterAttached')}
                  
              </View>
            </ScrollView>

            <View style={[styles.modalFooter, { borderTopColor: borderColor, backgroundColor: headerBgColor, padding: 15 }]}>
              <TouchableOpacity style={[styles.button, styles.cancelButton, { borderColor, paddingVertical: 10, paddingHorizontal: 20 }]} onPress={() => setIsModalVisible(false)}>
                <ThemedText style={{ color: subtitleColor, fontWeight: '600' }}>Cancel</ThemedText>
              </TouchableOpacity>
              <TouchableOpacity style={[styles.button, styles.saveButton, { paddingVertical: 10, paddingHorizontal: 20 }]} onPress={handleSave} disabled={saving}>
                {saving ? (
                  <ActivityIndicator color="#000" size="small" />
                ) : (
                  <ThemedText style={{ color: textColor, fontWeight: 'bold' }}>Save Details</ThemedText>
                )}
              </TouchableOpacity>
            </View>

          </View>
        </View>
      </Modal>
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  safeArea: { flex: 1 },
  header: {
    flexDirection: 'row', alignItems: 'center', justifyContent: 'space-between',
    paddingHorizontal: 15, paddingVertical: 10, borderBottomWidth: 1,
  },
  backButton: { marginRight: 15 },
  title: { fontSize: 16, fontWeight: 'bold' },
  addButton: {
    flexDirection: 'row', alignItems: 'center', paddingHorizontal: 10,
    paddingVertical: 6, borderRadius: 8, borderWidth: 1,
  },
  addButtonText: { marginLeft: 5, fontWeight: '600', fontSize: 12 },

  tableContainer: { flex: 1, margin: 15, maxWidth: 1100, width: '100%', alignSelf: 'center', padding: 10 },
  tableHeader: { flexDirection: 'row', paddingVertical: 10, paddingHorizontal: 20, marginBottom: 8, borderRadius: 12 },
  headerText: { fontSize: 10, fontWeight: 'bold', letterSpacing: 1 },
  row: { flexDirection: 'row', paddingVertical: 10, paddingHorizontal: 20, marginBottom: 8, borderRadius: 12, alignItems: 'center', shadowColor: '#000', shadowOffset: { width: 0, height: 2 }, shadowOpacity: 0.05, shadowRadius: 6, elevation: 2, borderWidth: 1 },
  cell: { justifyContent: 'center' },
  actionCell: { flexDirection: 'row', justifyContent: 'center', width: 120 },
  actionButton: { padding: 6, marginHorizontal: 4, borderRadius: 8, backgroundColor: 'rgba(0,0,0,0.03)', alignItems: 'center', justifyContent: 'center' },
  centered: { flex: 1, justifyContent: 'center', alignItems: 'center', padding: 20 },
  
  modalHeader: { flexDirection: 'row', justifyContent: 'space-between', alignItems: 'center', paddingHorizontal: 16, paddingVertical: 12, borderBottomWidth: 1 },
  modalBody: { padding: 16 },
  gridContainer: { width: '100%', maxWidth: 1600, alignSelf: 'center' },
  sectionBlock: { marginBottom: 16, borderWidth: 1, borderRadius: 12, padding: 15, backgroundColor: 'rgba(0,0,0,0.01)' },
  sectionTitle: { fontSize: 14, fontWeight: '700', marginBottom: 12 },
  grid: { flexDirection: 'row', flexWrap: 'wrap', marginHorizontal: -10 },
  inputGroup: { width: Platform.OS === 'web' ? '50%' : '100%', paddingHorizontal: 8, marginBottom: 12, minWidth: 200 },
  label: { fontSize: 11, fontWeight: '600', marginBottom: 4 },
  input: { borderWidth: 1, borderRadius: 8, padding: 8, fontSize: 12 },
  modalFooter: { flexDirection: 'row', justifyContent: 'flex-end', padding: 16, borderTopWidth: 1, gap: 12 },
  button: { paddingVertical: 10, paddingHorizontal: 20, borderRadius: 8, alignItems: 'center', justifyContent: 'center' },
  cancelButton: { borderWidth: 1, backgroundColor: 'transparent' },
  saveButton: { backgroundColor: '#D4AF37' },
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
