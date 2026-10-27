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

const API_URL = `${API_BASE_URL}/category`;

export default function CategoryScreen() {
  const themeColors = useThemeColors();
  const { accentColor } = useThemeContext();
  const { background: backgroundColor, card: cardColor, text: textColor, border: borderColor, primaryGold: GOLD_COLOR, subtitle: subtitleColor } = themeColors;
  const router = useRouter();
  const colorScheme = useColorScheme();
  const isDark = colorScheme === 'dark';

  const [categories, setCategories] = useState<any[]>([]);
  const [loading, setLoading] = useState(true);
  const [saving, setSaving] = useState(false);
  const [deletingId, setDeletingId] = useState<string | null>(null);
  const [successMsg, setSuccessMsg] = useState('');
  const [errorMsg, setErrorMsg] = useState('');
  const [isModalVisible, setIsModalVisible] = useState(false);

  const showSuccess = (msg: string) => {
    setSuccessMsg(msg);
    setTimeout(() => setSuccessMsg(''), 3000);
  };

  const showError = (msg: string) => {
    setErrorMsg(msg);
    setTimeout(() => setErrorMsg(''), 3000);
  };
  const [isEditing, setIsEditing] = useState(false);

  // Form States
  const [CategoryCode, setCategoryCode] = useState('');
  const [CategoryName, setCategoryName] = useState('');
  const [ClassCode, setClassCode] = useState('');
  const [ClassName, setClassName] = useState('');
  const [SizeFactor, setSizeFactor] = useState(false);
  const [FromInventoryCode, setFromInventoryCode] = useState('');
  const [ToInventoryCode, setToInventoryCode] = useState('');
  const [NextInventoryCode, setNextInventoryCode] = useState('');
  const [InventoryPrefix, setInventoryPrefix] = useState('');
  const [Remarks, setRemarks] = useState('');
  const [ShowInReceipt, setShowInReceipt] = useState(false);

  const inputBg = isDark ? '#2A2A2A' : '#F9F9F9';

  useEffect(() => {
    fetchCategories();
  }, []);

  const fetchCategories = async () => {
    try {
      const response = await fetch(`${API_URL}?t=${new Date().getTime()}`, {
        headers: {
          'Cache-Control': 'no-cache',
          'Pragma': 'no-cache'
        }
      });
      const data = await response.json();
      if (data.success) {
        setCategories(data.data);
      }
    } catch (error) {
      console.error('Error fetching categories:', error);
      Alert.alert('Error', 'Failed to load categories');
    } finally {
      setLoading(false);
    }
  };

  const handleSave = async () => {
    if (!CategoryCode || !String(CategoryCode).trim()) {
      showError('Category Code is required.');
      return;
    }
    if (!CategoryName || !String(CategoryName).trim()) {
      showError('Category Name is required.');
      return;
    }

    setSaving(true);
    const payload = {
      CategoryCode,
      CategoryName,
      ClassCode,
      InventoryPrefix,
      FromInventoryCode,
      ToInventoryCode,
      NextInventoryCode,
      Remarks,
      SizeFactor,
      ShowInReceipt,
    };

    try {
      const url = API_URL;
      const method = isEditing ? 'PUT' : 'POST';

      const response = await fetch(url, {
        method,
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload),
      });

      const data = await response.json();

      if (data.success) {
        showSuccess(`Category ${isEditing ? 'updated' : 'added'} successfully`);
        setIsModalVisible(false);
        fetchCategories();
      } else {
        showError(data.message || 'Failed to save');
      }
    } catch (error) {
      console.error('Save error:', error);
      showError('Failed to save category');
    } finally {
      setSaving(false);
    }
  };

  const executeDelete = async (code: string) => {
    setDeletingId(code);
    try {
      const response = await fetch(`${API_URL}?id=${encodeURIComponent(code)}`, { method: 'DELETE' });
      const data = await response.json();
      if (data.success) {
        showSuccess('Category deleted successfully');
        fetchCategories();
      } else {
        showError(data.message || 'Failed to delete');
      }
    } catch (error) {
      console.error('Delete error:', error);
      showError('Failed to delete category');
    } finally {
      setDeletingId(null);
    }
  };

  const handleDelete = (code: string) => {
    if (Platform.OS === 'web') {
      if (window.confirm(`Are you sure you want to delete Category ${code}?`)) {
        executeDelete(code);
      }
    } else {
      Alert.alert('Confirm Delete', `Are you sure you want to delete Category ${code}?`, [
        { text: 'Cancel', style: 'cancel' },
        {
          text: 'Delete',
          style: 'destructive',
          onPress: () => executeDelete(code),
        },
      ]);
    }
  };

  const openAddModal = () => {
    setIsEditing(false);
    setCategoryCode('');
    setCategoryName('');
    setClassCode('');
    setClassName('');
    setSizeFactor(false);
    setFromInventoryCode('');
    setToInventoryCode('');
    setNextInventoryCode('');
    setInventoryPrefix('');
    setRemarks('');
    setShowInReceipt(false);
    setIsModalVisible(true);
  };

  const openEditModal = (item: any) => {
    setIsEditing(true);
    setCategoryCode(item.CategoryCode);
    setCategoryName(item.CategoryName);
    setClassCode(item.ClassCode || '');
    setClassName(''); // We don't save ClassName in category table
    setSizeFactor(!!item.SizeFactor);
    setFromInventoryCode(item.FromInventoryCode || '');
    setToInventoryCode(item.ToInventoryCode || '');
    setNextInventoryCode(item.NextInventoryCode || '');
    setInventoryPrefix(item.InventoryPrefix || '');
    setRemarks(item.Remarks || '');
    setShowInReceipt(!!item.ShowInReceipt);
    setIsModalVisible(true);
  };

  const CustomCheckbox = ({ label, value, onValueChange }: any) => (
    <TouchableOpacity 
      style={{ flexDirection: 'row', alignItems: 'center', marginRight: 24, paddingVertical: 8 }} 
      onPress={() => onValueChange(!value)}
    >
      <View style={{
        width: 20, height: 20, borderRadius: 4, borderWidth: 2, 
        borderColor: value ? GOLD_COLOR : borderColor,
        backgroundColor: value ? GOLD_COLOR : 'transparent',
        alignItems: 'center', justifyContent: 'center', marginRight: 8
      }}>
        {value && <Ionicons name="checkmark" size={14} color="#FFF" />}
      </View>
      <ThemedText style={{ color: textColor, fontSize: 14 }}>{label}</ThemedText>
    </TouchableOpacity>
  );

  const renderItem = ({ item, index }: { item: any; index: number }) => {
    return (
      <View style={[styles.row, { backgroundColor: cardColor, borderColor: borderColor }]}>
        <View style={[styles.cell, { flex: 1.5 }]}>
          <ThemedText type="tableText" style={{ color: textColor, fontWeight: '500', fontSize: 11 }}>{item.CategoryCode}</ThemedText>
        </View>
        <View style={[styles.cell, { flex: 2 }]}>
          <ThemedText type="tableText" style={{ color: textColor, fontSize: 11 }}>{item.CategoryName}</ThemedText>
        </View>
        <View style={[styles.cell, { flex: 1.5 }]}>
          <ThemedText type="tableText" style={{ color: textColor, fontSize: 11 }}>{item.FromInventoryCode}</ThemedText>
        </View>
        <View style={[styles.cell, { flex: 1.5 }]}>
          <ThemedText type="tableText" style={{ color: textColor, fontSize: 11 }}>{item.ToInventoryCode}</ThemedText>
        </View>
        
        <View style={styles.actionCell}>
          <TouchableOpacity style={styles.actionButton} onPress={() => openEditModal(item)} disabled={deletingId === item.CategoryCode}>
            <Ionicons name="pencil-outline" size={20} color={GOLD_COLOR} />
          </TouchableOpacity>
          <TouchableOpacity style={styles.actionButton} onPress={() => handleDelete(item.CategoryCode)} disabled={deletingId === item.CategoryCode}>
            {deletingId === item.CategoryCode ? (
              <ActivityIndicator size="small" color="#F44336" />
            ) : (
              <Ionicons name="trash-outline" size={20} color="#F44336" />
            )}
          </TouchableOpacity>
        </View>
      </View>
    );
  };

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
          <ThemedText style={[styles.title, { color: textColor }]}>Category Master</ThemedText>
        </View>
        <TouchableOpacity style={[styles.addButton, { backgroundColor: 'rgba(212, 175, 55, 0.15)', borderColor: GOLD_COLOR }]} onPress={openAddModal}>
          <Ionicons name="add" size={20} color={GOLD_COLOR} />
          <ThemedText style={[styles.addButtonText, { color: GOLD_COLOR }]}>New</ThemedText>
        </TouchableOpacity>
      </View>

      {/* Table Container */}
      <View style={{ flex: 1 }}>
        <View style={styles.tableContainer}>
          
          {/* Table Header */}
          <View style={[styles.tableHeader, { backgroundColor: 'rgba(0,0,0,0.02)' }]}>
            <View style={[styles.cell, { flex: 1.5 }]}><ThemedText style={[styles.headerText, { color: subtitleColor }]}>CODE</ThemedText></View>
            <View style={[styles.cell, { flex: 2 }]}><ThemedText style={[styles.headerText, { color: subtitleColor }]}>NAME</ThemedText></View>
            <View style={[styles.cell, { flex: 1.5 }]}><ThemedText style={[styles.headerText, { color: subtitleColor }]}>FROM INV</ThemedText></View>
            <View style={[styles.cell, { flex: 1.5 }]}><ThemedText style={[styles.headerText, { color: subtitleColor }]}>TO INV</ThemedText></View>
            <View style={[styles.actionCell, { width: 120 }]}><ThemedText style={[styles.headerText, { color: subtitleColor, width: 55, textAlign: 'center' }]}>EDIT</ThemedText><ThemedText style={[styles.headerText, { color: subtitleColor, width: 55, textAlign: 'center' }]}>DELETE</ThemedText></View>
          </View>

          {loading ? (
            <ActivityIndicator size="large" color={GOLD_COLOR} style={{ marginTop: 40 }} />
          ) : (
            <FlatList
              data={categories}
              keyExtractor={(item) => item.CategoryCode}
              renderItem={renderItem}
              contentContainerStyle={{ paddingBottom: 20 }}
            />
          )}
        </View>
      </View>

      {/* Add/Edit Modal */}
      <Modal visible={isModalVisible} animationType="fade" transparent={true}>
        <TouchableWithoutFeedback onPress={() => setIsModalVisible(false)}>
          <View style={styles.modalOverlay}>
            <TouchableWithoutFeedback>
              <View style={[styles.modalContent, { backgroundColor: cardColor, borderColor }]}>
                {renderToast()}
                {/* Modal Header */}
                <View style={[styles.modalHeader, { borderBottomColor: borderColor }]}>
                  <ThemedText style={[styles.modalTitle, { color: textColor }]}>
                    {isEditing ? 'Edit Category' : 'Create New Category'}
                  </ThemedText>
                  <TouchableOpacity onPress={() => setIsModalVisible(false)}>
                    <Ionicons name="close" size={24} color={subtitleColor} />
                  </TouchableOpacity>
                </View>

                {/* Grid Form Container */}
                <View style={{ padding: 24, gap: 16 }}>
                  
                  {/* Row 1 */}
                  <View style={{ flexDirection: 'row', gap: 24 }}>
                    <View style={{ flex: 1, flexDirection: 'row', alignItems: 'center' }}>
                      <ThemedText style={[styles.label, { color: subtitleColor, width: 120, marginBottom: 0 }]}>Category Code</ThemedText>
                      <TextInput
                        style={[styles.input, { backgroundColor: inputBg, color: textColor, borderColor, height: 40, flex: 1 }]}
                        value={CategoryCode}
                        onChangeText={setCategoryCode}
                        placeholder="Enter Code"
                        placeholderTextColor={subtitleColor}
                        editable={!isEditing}
                      />
                    </View>
                    <View style={{ flex: 1, flexDirection: 'row', alignItems: 'center' }}>
                      <ThemedText style={[styles.label, { color: subtitleColor, width: 120, marginBottom: 0 }]}>Category Name</ThemedText>
                      <TextInput
                        style={[styles.input, { backgroundColor: inputBg, color: textColor, borderColor, height: 40, flex: 1 }]}
                        value={CategoryName}
                        onChangeText={setCategoryName}
                        placeholder="Enter Name"
                        placeholderTextColor={subtitleColor}
                      />
                    </View>
                  </View>

                  {/* Row 2 */}
                  <View style={{ flexDirection: 'row', gap: 24 }}>
                    <View style={{ flex: 1, flexDirection: 'row', alignItems: 'center' }}>
                      <ThemedText style={[styles.label, { color: subtitleColor, width: 120, marginBottom: 0 }]}>Class Code</ThemedText>
                      <TextInput
                        style={[styles.input, { backgroundColor: inputBg, color: textColor, borderColor, height: 40, flex: 1 }]}
                        value={ClassCode}
                        onChangeText={setClassCode}
                        placeholder="Class Code"
                        placeholderTextColor={subtitleColor}
                      />
                    </View>
                    <View style={{ flex: 1, flexDirection: 'row', alignItems: 'center' }}>
                      <ThemedText style={[styles.label, { color: subtitleColor, width: 120, marginBottom: 0 }]}>Class Name</ThemedText>
                      <TextInput
                        style={[styles.input, { backgroundColor: inputBg, color: textColor, borderColor, height: 40, flex: 1 }]}
                        value={ClassName}
                        onChangeText={setClassName}
                        placeholder="Class Name"
                        placeholderTextColor={subtitleColor}
                      />
                    </View>
                  </View>

                  {/* Row 3 - Full width checkboxes */}
                  <View style={{ flexDirection: 'row', gap: 24, paddingLeft: 120 }}>
                     <CustomCheckbox label="Size Factor" value={SizeFactor} onValueChange={setSizeFactor} />
                     <CustomCheckbox label="Show in Receipt" value={ShowInReceipt} onValueChange={setShowInReceipt} />
                  </View>

                  {/* Row 4 */}
                  <View style={{ flexDirection: 'row', gap: 24 }}>
                    <View style={{ flex: 1, flexDirection: 'row', alignItems: 'center' }}>
                      <ThemedText style={[styles.label, { color: subtitleColor, width: 120, marginBottom: 0 }]}>From Inventory</ThemedText>
                      <TextInput
                        style={[styles.input, { backgroundColor: inputBg, color: textColor, borderColor, height: 40, flex: 1 }]}
                        value={FromInventoryCode}
                        onChangeText={setFromInventoryCode}
                        placeholder="From Code"
                        placeholderTextColor={subtitleColor}
                      />
                    </View>
                    <View style={{ flex: 1, flexDirection: 'row', alignItems: 'center' }}>
                      <ThemedText style={[styles.label, { color: subtitleColor, width: 120, marginBottom: 0 }]}>To Inventory</ThemedText>
                      <TextInput
                        style={[styles.input, { backgroundColor: inputBg, color: textColor, borderColor, height: 40, flex: 1 }]}
                        value={ToInventoryCode}
                        onChangeText={setToInventoryCode}
                        placeholder="To Code"
                        placeholderTextColor={subtitleColor}
                      />
                    </View>
                  </View>

                  {/* Row 5 */}
                  <View style={{ flexDirection: 'row', gap: 24 }}>
                    <View style={{ flex: 1, flexDirection: 'row', alignItems: 'center' }}>
                      <ThemedText style={[styles.label, { color: subtitleColor, width: 120, marginBottom: 0 }]}>Next Inventory</ThemedText>
                      <TextInput
                        style={[styles.input, { backgroundColor: inputBg, color: textColor, borderColor, height: 40, flex: 1 }]}
                        value={NextInventoryCode}
                        onChangeText={setNextInventoryCode}
                        placeholder="Next Code"
                        placeholderTextColor={subtitleColor}
                      />
                    </View>
                    <View style={{ flex: 1, flexDirection: 'row', alignItems: 'center' }}>
                      <ThemedText style={[styles.label, { color: subtitleColor, width: 120, marginBottom: 0 }]}>Inventory Prefix</ThemedText>
                      <TextInput
                        style={[styles.input, { backgroundColor: inputBg, color: textColor, borderColor, height: 40, flex: 1 }]}
                        value={InventoryPrefix}
                        onChangeText={setInventoryPrefix}
                        placeholder="Prefix"
                        placeholderTextColor={subtitleColor}
                      />
                    </View>
                  </View>

                  {/* Row 6 */}
                  <View style={{ flexDirection: 'row', gap: 24 }}>
                    <View style={{ flex: 1, flexDirection: 'row', alignItems: 'center' }}>
                      <ThemedText style={[styles.label, { color: subtitleColor, width: 120, marginBottom: 0 }]}>Remarks</ThemedText>
                      <TextInput
                        style={[styles.input, { backgroundColor: inputBg, color: textColor, borderColor, height: 40, flex: 1 }]}
                        value={Remarks}
                        onChangeText={setRemarks}
                        placeholder="Enter Remarks"
                        placeholderTextColor={subtitleColor}
                      />
                    </View>
                  </View>
                  
                </View>

                {/* Action Buttons */}
                <View style={[styles.modalActions, { borderTopColor: borderColor }]}>
                  <TouchableOpacity style={styles.cancelButton} onPress={() => setIsModalVisible(false)}>
                    <ThemedText style={styles.cancelButtonText}>Cancel</ThemedText>
                  </TouchableOpacity>
                  <TouchableOpacity style={styles.saveButton} onPress={handleSave} disabled={saving}>
                    {saving ? (
                      <ActivityIndicator size="small" color="#FFF" />
                    ) : (
                      <ThemedText style={styles.saveButtonText}>{isEditing ? 'Update' : 'Save'}</ThemedText>
                    )}
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

  modalOverlay: {
    flex: 1, backgroundColor: 'rgba(0,0,0,0.5)', justifyContent: 'center', alignItems: 'center',
  },
  modalContent: {
    width: '90%', maxWidth: 1000, borderWidth: 1, borderRadius: 12,
    shadowOffset: { width: 0, height: 4 }, shadowOpacity: 0.2, shadowRadius: 10, elevation: 10,
  },
  modalHeader: {
    flexDirection: 'row', justifyContent: 'space-between', alignItems: 'center',
    padding: 20, borderBottomWidth: 1,
  },
  modalTitle: { fontSize: 20, fontWeight: '700' },
  label: { fontSize: 14, fontWeight: '500' },
  input: {
    borderWidth: 1, borderRadius: 8, paddingHorizontal: 12, fontSize: 14,
  },
  modalActions: {
    flexDirection: 'row', justifyContent: 'flex-end', gap: 12, padding: 20, borderTopWidth: 1,
  },
  cancelButton: {
    paddingHorizontal: 20, paddingVertical: 12, borderRadius: 8, borderWidth: 1, borderColor: '#CCC',
  },
  cancelButtonText: { color: '#666', fontWeight: '600' },
  saveButton: {
    paddingHorizontal: 24, paddingVertical: 12, borderRadius: 8, backgroundColor: '#D4AF37',
  },
  saveButtonText: {  fontWeight: '600' },
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
