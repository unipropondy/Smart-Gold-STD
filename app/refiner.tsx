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
const API_URL = `${API_BASE_URL}/refiner`;

export default function RefinerScreen() {
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
  const [refinerCode, setRefinerCode] = useState('');
  const [refinerName, setRefinerName] = useState('');
  const [type, setType] = useState('');

  const headerBgColor = isDark ? '#2A2A2A' : '#F5F5F5';

  const fetchRefiners = async () => {
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
    fetchRefiners();
  }, []);

  const handleSave = async () => {
    if (!refinerCode || !refinerCode.trim()) {
      showError('Refiner Code is required');
      return;
    }

    const payload = {
      RefinerCode: refinerCode.trim(),
      RefinerName: refinerName.trim(),
      Type: type.trim(),
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
        showSuccess(`Refiner ${isEditing ? 'updated' : 'added'} successfully`);
        setIsModalVisible(false);
        fetchRefiners();
      } else {
        showError(json.message || 'Failed to save');
      }
    } catch (error) {
      console.error('Save error:', error);
      showError('Failed to save refiner');
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
        showSuccess('Refiner deleted successfully');
        fetchRefiners();
      } else {
        showError(json.message || 'Failed to delete');
      }
    } catch (error) {
      console.error('Delete error:', error);
      showError('Failed to delete refiner');
    } finally {
      setDeletingId(null);
    }
  };

  const handleDelete = (codeToDelete: string) => {
    if (Platform.OS === 'web') {
      if (window.confirm(`Are you sure you want to delete Refiner ${codeToDelete}?`)) {
        executeDelete(codeToDelete);
      }
    } else {
      Alert.alert('Confirm Delete', `Are you sure you want to delete Refiner ${codeToDelete}?`, [
        { text: 'Cancel', style: 'cancel' },
        { text: 'Delete', style: 'destructive', onPress: () => executeDelete(codeToDelete) },
      ]);
    }
  };

  const openAddModal = () => {
    setIsEditing(false);
    setRefinerCode('');
    setRefinerName('');
    setType('');
    setIsModalVisible(true);
  };

  const openEditModal = (item: any) => {
    setIsEditing(true);
    setRefinerCode(item.RefinerCode);
    setRefinerName(item.RefinerName || '');
    setType(item.Type || '');
    setIsModalVisible(true);
  };

  const renderItem = ({ item }: { item: any }) => (
    <View style={[styles.row, { backgroundColor: cardColor, borderColor: borderColor }]}>
      <View style={[styles.cell, { flex: 1.5 }]}>
        <ThemedText type="tableText" style={{ color: textColor, fontWeight: '500', fontSize: 11 }}>{item.RefinerCode}</ThemedText>
      </View>
      <View style={[styles.cell, { flex: 2 }]}>
        <ThemedText type="tableText" style={{ color: textColor, fontSize: 11 }}>{item.RefinerName}</ThemedText>
      </View>
      <View style={[styles.cell, { flex: 1.5 }]}>
        <ThemedText type="tableText" style={{ color: textColor, fontSize: 11 }}>{item.Type}</ThemedText>
      </View>
      <View style={[styles.actionCell, { alignItems: 'center', justifyContent: 'center' }]}>
        <TouchableOpacity style={styles.actionButton} onPress={() => openEditModal(item)} disabled={deletingId === item.RefinerCode}>
          <Ionicons name="pencil-outline" size={20} color={GOLD_COLOR} />
        </TouchableOpacity>
        <TouchableOpacity style={styles.actionButton} onPress={() => handleDelete(item.RefinerCode)} disabled={deletingId === item.RefinerCode}>
          {deletingId === item.RefinerCode ? (
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
          <ThemedText style={[styles.title, { color: textColor }]}>Refiner Master</ThemedText>
        </View>
        <TouchableOpacity style={[styles.addButton, { backgroundColor: 'rgba(212, 175, 55, 0.15)', borderColor: GOLD_COLOR }]} onPress={openAddModal}>
          <Ionicons name="add" size={20} color={GOLD_COLOR} />
          <ThemedText style={[styles.addButtonText, { color: GOLD_COLOR }]}>New</ThemedText>
        </TouchableOpacity>
      </View>

      <View style={styles.tableContainer}>
      {/* Table Header */}
      <View style={[styles.tableHeader, { backgroundColor: 'rgba(0,0,0,0.02)' }]}>
        <View style={[styles.cell, { flex: 1.5 }]}><ThemedText style={[styles.headerText, { color: subtitleColor }]}>REFINER CODE</ThemedText></View>
        <View style={[styles.cell, { flex: 2 }]}><ThemedText style={[styles.headerText, { color: subtitleColor }]}>REFINER NAME</ThemedText></View>
        <View style={[styles.cell, { flex: 1.5 }]}><ThemedText style={[styles.headerText, { color: subtitleColor }]}>TYPE</ThemedText></View>
        <View style={[styles.actionCell, { width: 120 }]}><ThemedText style={[styles.headerText, { color: subtitleColor, width: 55, textAlign: 'center' }]}>EDIT</ThemedText><ThemedText style={[styles.headerText, { color: subtitleColor, width: 55, textAlign: 'center' }]}>DELETE</ThemedText></View>
      </View>

      {loading ? (
        <View style={styles.centered}><ActivityIndicator size="large" color={GOLD_COLOR} /></View>
      ) : (
        <FlatList
          data={data}
          renderItem={renderItem}
          keyExtractor={(item, index) => item.RefinerCode || index.toString()}
          contentContainerStyle={{ paddingBottom: 20 }}
          ListEmptyComponent={
            <View style={styles.centered}>
              <ThemedText style={{ color: subtitleColor }}>No refiners found.</ThemedText>
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
                  {isEditing ? 'Edit Refiner' : 'Add New Refiner'}
                </ThemedText>
                <TouchableOpacity onPress={() => setIsModalVisible(false)}>
                  <Ionicons name="close" size={24} color={textColor} />
                </TouchableOpacity>
              </View>

              <View style={styles.modalBody}>
                <View style={styles.inputGroup}>
                  <ThemedText style={[styles.label, { color: subtitleColor }]}>Refiner Code *</ThemedText>
                  <TextInput
                    style={[styles.input, { color: textColor, borderColor, backgroundColor: isDark ? '#2A2A2A' : '#FFF' }, isEditing && { backgroundColor: isDark ? '#333' : '#F0F0F0', color: subtitleColor }]}
                    value={refinerCode}
                    onChangeText={setRefinerCode}
                    editable={!isEditing}
                    placeholder="Enter Refiner Code"
                    placeholderTextColor={subtitleColor}
                  />
                </View>

                <View style={styles.inputGroup}>
                  <ThemedText style={[styles.label, { color: subtitleColor }]}>Refiner Name</ThemedText>
                  <TextInput
                    style={[styles.input, { color: textColor, borderColor, backgroundColor: isDark ? '#2A2A2A' : '#FFF' }]}
                    value={refinerName}
                    onChangeText={setRefinerName}
                    placeholder="Enter Refiner Name"
                    placeholderTextColor={subtitleColor}
                  />
                </View>

                <View style={styles.inputGroup}>
                  <ThemedText style={[styles.label, { color: subtitleColor }]}>Type</ThemedText>
                  <TextInput
                    style={[styles.input, { color: textColor, borderColor, backgroundColor: isDark ? '#2A2A2A' : '#FFF' }]}
                    value={type}
                    onChangeText={setType}
                    placeholder="Enter Type (e.g. GOLD, COIN)"
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
