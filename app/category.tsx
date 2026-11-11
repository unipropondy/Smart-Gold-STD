import { Ionicons } from '@expo/vector-icons';
import { Stack, useRouter } from 'expo-router';
import React, { useEffect, useState } from 'react';
import {
  ActivityIndicator,
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

interface ClassItem {
  ClassCode: string;
  ClassName: string;
  Rate?: number;
  Remarks?: string;
}

export default function CategoryScreen() {
  const themeColors = useThemeColors();
  const { accentColor } = useThemeContext();
  const {
    background: backgroundColor,
    card: cardColor,
    text: textColor,
    border: borderColor,
    primaryGold: GOLD_COLOR,
    subtitle: subtitleColor,
  } = themeColors;
  const router = useRouter();
  const colorScheme = useColorScheme();
  const isDark = colorScheme === 'dark';

  const [categories, setCategories] = useState<any[]>([]);
  const [loading, setLoading] = useState(true);
  const [saving, setSaving] = useState(false);
  const [deletingId, setDeletingId] = useState<string | null>(null);
  const [toast, setToast] = useState<{ message: string; type: 'success' | 'error' } | null>(null);
  const [deleteConfirmCode, setDeleteConfirmCode] = useState<string | null>(null);
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [isEditing, setIsEditing] = useState(false);

  // Form States matching original design
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

  // Class LOV Modal State
  const [classList, setClassList] = useState<ClassItem[]>([]);
  const [classModalVisible, setClassModalVisible] = useState(false);
  const [classSearch, setClassSearch] = useState('');

  const inputBg = isDark ? '#2A2A2A' : '#F9F9F9';

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

  useEffect(() => {
    fetchCategories();
    fetchClasses();
  }, []);

  const fetchCategories = async () => {
    try {
      const response = await fetch(`${API_URL}?t=${new Date().getTime()}`, {
        headers: {
          'Cache-Control': 'no-cache',
          'Pragma': 'no-cache',
        },
      });
      const data = await response.json();
      if (data.success && Array.isArray(data.data)) {
        setCategories(data.data);
      }
    } catch (error) {
      console.error('Error fetching categories:', error);
      showError('Failed to load categories');
    } finally {
      setLoading(false);
    }
  };

  const fetchClasses = async () => {
    try {
      const res = await fetch(`${API_URL}/classes`);
      const data = await res.json();
      if (data.success && Array.isArray(data.data)) {
        setClassList(data.data);
      }
    } catch (err) {
      console.error('Error fetching classes:', err);
    }
  };

  // ========================================================
  // FIELD-LEVEL VALIDATIONS (Matching Master/frmCategory.vb)
  // ========================================================

  // 1. txtCategoryCode_Validating (VB lines 701-707)
  const handleCategoryCodeBlur = () => {
    if (!CategoryCode || !CategoryCode.trim()) {
      showError('CategoryCode should not be blank.');
    }
  };

  // 2. txtCategoryName_Validating (VB lines 710-716)
  const handleCategoryNameBlur = () => {
    if (!CategoryName || !CategoryName.trim()) {
      showError('CategoryName should not be blank.');
    }
  };

  // 3. txtClassCode_Validating & txtClassCode_LostFocus (VB lines 719-726 & 764-781)
  const handleClassCodeBlur = async () => {
    const trimmed = (ClassCode || '').trim();
    if (!trimmed) {
      showError('ClassCode should not be blank.');
      setClassName('');
      return;
    }
    try {
      const res = await fetch(`${API_URL}/validate-class/${encodeURIComponent(trimmed)}`);
      const data = await res.json();
      if (data.success && data.data) {
        setClassName(data.data.ClassName || '');
      } else {
        showError('Pleae enter valid ClassCode.');
        setClassCode('');
        setClassName('');
      }
    } catch (err) {
      // ignore
    }
  };

  // 4. txtFromInventoryCode_Validating (VB lines 728-738)
  const handleFromInventoryBlur = () => {
    const fromTrim = (FromInventoryCode || '').trim();
    if (!fromTrim) {
      showError('From Inventory code should not be blank.');
      return;
    }
    const fromNum = parseInt(fromTrim, 10);
    const toNum = parseInt((ToInventoryCode || '').trim(), 10);
    if (!isNaN(fromNum) && !isNaN(toNum) && toNum < fromNum) {
      showError('ToInventoryCode should be greater than FromInventoryCode.');
    }
  };

  // 5. txtToInventoryCode_Validating & comparison (VB lines 740-750 & 958)
  const handleToInventoryBlur = () => {
    const toTrim = (ToInventoryCode || '').trim();
    if (!toTrim) {
      showError('To Inventory code should not be blank.');
      return;
    }
    const fromNum = parseInt((FromInventoryCode || '').trim(), 10);
    const toNum = parseInt(toTrim, 10);
    if (!isNaN(fromNum) && !isNaN(toNum) && toNum < fromNum) {
      showError('ToInventoryCode should be greater than FromInventoryCode.');
    }
  };

  // 6. txtNextInventoryCode_Validating & range check (VB lines 752-762 & 976-980)
  const handleNextInventoryBlur = () => {
    const nextTrim = (NextInventoryCode || '').trim();
    if (!nextTrim) return;
    const nextNum = parseInt(nextTrim, 10);
    const fromNum = parseInt((FromInventoryCode || '').trim(), 10);
    const toNum = parseInt((ToInventoryCode || '').trim(), 10);
    if (!isNaN(nextNum) && !isNaN(fromNum) && !isNaN(toNum)) {
      if (nextNum < fromNum || nextNum > toNum) {
        showError('Next inventorycode should be greaterthan From InventoryCode and Less than To InventoryCode.');
      }
    }
  };

  // Form Submission Validation matching ValidateMe (VB lines 914-989)
  const handleSave = async () => {
    if (!CategoryCode || !String(CategoryCode).trim()) {
      showError('CategoryCode should not be blank.');
      return;
    }
    if (!CategoryName || !String(CategoryName).trim()) {
      showError('CategoryName should not be blank.');
      return;
    }
    if (!ClassCode || !String(ClassCode).trim()) {
      showError('ClassCode should not be blank.');
      return;
    }
    if (!ClassName || !String(ClassName).trim()) {
      showError('Pleae enter valid ClassCode.');
      return;
    }
    if (!FromInventoryCode || !String(FromInventoryCode).trim()) {
      showError('From Inventory code should not be blank.');
      return;
    }
    if (!ToInventoryCode || !String(ToInventoryCode).trim()) {
      showError('To Inventory code should not be blank.');
      return;
    }

    const fromNum = parseInt(String(FromInventoryCode).trim(), 10);
    const toNum = parseInt(String(ToInventoryCode).trim(), 10);
    if (!isNaN(fromNum) && !isNaN(toNum)) {
      if (toNum < fromNum) {
        showError('ToInventoryCode should be greater than FromInventoryCode.');
        return;
      }
    }

    if (NextInventoryCode && String(NextInventoryCode).trim() !== '') {
      const nextNum = parseInt(String(NextInventoryCode).trim(), 10);
      if (!isNaN(nextNum) && !isNaN(fromNum) && !isNaN(toNum)) {
        if (nextNum < fromNum || nextNum > toNum) {
          showError('Next inventorycode should be greaterthan From InventoryCode and Less than To InventoryCode.');
          return;
        }
      }
    }

    setSaving(true);
    const payload = {
      CategoryCode: String(CategoryCode).trim().toUpperCase(),
      CategoryName: String(CategoryName).trim(),
      ClassCode: String(ClassCode).trim(),
      InventoryPrefix: String(InventoryPrefix || '').trim(),
      FromInventoryCode: String(FromInventoryCode).trim(),
      ToInventoryCode: String(ToInventoryCode).trim(),
      NextInventoryCode: String(NextInventoryCode || FromInventoryCode).trim(),
      Remarks: String(Remarks || '').trim(),
      SizeFactor: SizeFactor ? 1 : 0,
      ShowInReceipt: ShowInReceipt ? 1 : 0,
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
        showSuccess(data.message || `Category ${isEditing ? 'updated' : 'added'} successfully`);
        setIsModalVisible(false);
        fetchCategories();
      } else {
        showError(data.message || 'Error in Save');
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
        showSuccess(data.message || 'Category deleted successfully');
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
    setDeleteConfirmCode(code);
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
    setCategoryCode(item.CategoryCode || '');
    setCategoryName(item.CategoryName || '');
    setClassCode(item.ClassCode || '');
    setClassName(item.ClassName || '');
    setSizeFactor(item.SizeFactor === 1 || item.SizeFactor === true || item.SizeFactor === '1');
    setFromInventoryCode(item.FromInventoryCode || '');
    setToInventoryCode(item.ToInventoryCode || '');
    setNextInventoryCode(item.NextInventoryCode || '');
    setInventoryPrefix(item.InventoryPrefix || '');
    setRemarks(item.Remarks || '');
    setShowInReceipt(item.ShowInReceipt === 1 || item.ShowInReceipt === true || item.ShowInReceipt === '1');
    setIsModalVisible(true);
  };

  const CustomCheckbox = ({ label, value, onValueChange, disabled }: any) => (
    <TouchableOpacity
      style={{ flexDirection: 'row', alignItems: 'center', marginRight: 24, paddingVertical: 8, opacity: disabled ? 0.6 : 1 }}
      onPress={() => !disabled && onValueChange(!value)}
      disabled={disabled}
    >
      <View
        style={{
          width: 20,
          height: 20,
          borderRadius: 4,
          borderWidth: 2,
          borderColor: value ? GOLD_COLOR : borderColor,
          backgroundColor: value ? GOLD_COLOR : 'transparent',
          alignItems: 'center',
          justifyContent: 'center',
          marginRight: 8,
        }}
      >
        {value && <Ionicons name="checkmark" size={14} color="#FFF" />}
      </View>
      <ThemedText style={{ color: textColor, fontSize: 14 }}>{label}</ThemedText>
      {disabled && <ThemedText style={{ color: '#888', fontSize: 11, marginLeft: 4 }}>(Locked)</ThemedText>}
    </TouchableOpacity>
  );

  const filteredClasses = classList.filter((c) => {
    const q = classSearch.toLowerCase().trim();
    if (!q) return true;
    return (
      (c.ClassCode || '').toLowerCase().includes(q) ||
      (c.ClassName || '').toLowerCase().includes(q)
    );
  });

  const renderItem = ({ item }: { item: any; index: number }) => {
    return (
      <View style={[styles.row, { backgroundColor: cardColor, borderColor: borderColor }]}>
        <View style={[styles.cell, { flex: 1.5 }]}>
          <ThemedText type="tableText" style={{ color: textColor, fontWeight: '500', fontSize: 11 }}>
            {item.CategoryCode}
          </ThemedText>
        </View>
        <View style={[styles.cell, { flex: 2 }]}>
          <ThemedText type="tableText" style={{ color: textColor, fontSize: 11 }}>
            {item.CategoryName}
          </ThemedText>
        </View>
        <View style={[styles.cell, { flex: 1.5 }]}>
          <ThemedText type="tableText" style={{ color: textColor, fontSize: 11 }}>
            {item.FromInventoryCode || '—'}
          </ThemedText>
        </View>
        <View style={[styles.cell, { flex: 1.5 }]}>
          <ThemedText type="tableText" style={{ color: textColor, fontSize: 11 }}>
            {item.ToInventoryCode || '—'}
          </ThemedText>
        </View>

        <View style={styles.actionCell}>
          <TouchableOpacity
            style={styles.actionButton}
            onPress={() => openEditModal(item)}
            disabled={deletingId === item.CategoryCode}
          >
            <Ionicons name="pencil-outline" size={20} color={GOLD_COLOR} />
          </TouchableOpacity>
          <TouchableOpacity
            style={styles.actionButton}
            onPress={() => handleDelete(item.CategoryCode)}
            disabled={deletingId === item.CategoryCode}
          >
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
    if (!toast) return null;
    const isError = toast.type === 'error';
    return (
      <View
        pointerEvents="none"
        style={[styles.toast, isError ? styles.toastError : styles.toastSuccess]}
      >
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
      {!isModalVisible && !classModalVisible && !deleteConfirmCode && renderToast()}

      {/* Header */}
      <View style={[styles.header, { borderBottomColor: borderColor }]}>
        <View style={{ flexDirection: 'row', alignItems: 'center' }}>
          <TouchableOpacity style={styles.backButton} onPress={() => router.back()}>
            <Ionicons name="arrow-back" size={24} color={textColor} />
          </TouchableOpacity>
          <ThemedText style={[styles.title, { color: textColor }]}>Category Master</ThemedText>
        </View>
        <TouchableOpacity
          style={[styles.addButton, { backgroundColor: 'rgba(212, 175, 55, 0.15)', borderColor: GOLD_COLOR }]}
          onPress={openAddModal}
        >
          <Ionicons name="add" size={20} color={GOLD_COLOR} />
          <ThemedText style={[styles.addButtonText, { color: GOLD_COLOR }]}>New</ThemedText>
        </TouchableOpacity>
      </View>

      {/* Table Container */}
      <View style={{ flex: 1 }}>
        <View style={styles.tableContainer}>
          {/* Table Header */}
          <View style={[styles.tableHeader, { backgroundColor: 'rgba(0,0,0,0.02)' }]}>
            <View style={[styles.cell, { flex: 1.5 }]}>
              <ThemedText style={[styles.headerText, { color: subtitleColor }]}>CODE</ThemedText>
            </View>
            <View style={[styles.cell, { flex: 2 }]}>
              <ThemedText style={[styles.headerText, { color: subtitleColor }]}>NAME</ThemedText>
            </View>
            <View style={[styles.cell, { flex: 1.5 }]}>
              <ThemedText style={[styles.headerText, { color: subtitleColor }]}>FROM INV</ThemedText>
            </View>
            <View style={[styles.cell, { flex: 1.5 }]}>
              <ThemedText style={[styles.headerText, { color: subtitleColor }]}>TO INV</ThemedText>
            </View>
            <View style={[styles.actionCell, { width: 120 }]}>
              <ThemedText style={[styles.headerText, { color: subtitleColor, width: 55, textAlign: 'center' }]}>
                EDIT
              </ThemedText>
              <ThemedText style={[styles.headerText, { color: subtitleColor, width: 55, textAlign: 'center' }]}>
                DELETE
              </ThemedText>
            </View>
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

      {/* Add/Edit Modal (Original Design) */}
      <Modal visible={isModalVisible} animationType="fade" transparent={true}>
        <TouchableWithoutFeedback onPress={() => setIsModalVisible(false)}>
          <View style={styles.modalOverlay}>
            <TouchableWithoutFeedback>
              <View style={[styles.modalContent, { backgroundColor: cardColor, borderColor }]}>
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
                      <ThemedText style={[styles.label, { color: subtitleColor, width: 120, marginBottom: 0 }]}>
                        Category Code
                      </ThemedText>
                      <TextInput
                        style={[styles.input, { backgroundColor: inputBg, color: textColor, borderColor, height: 40, flex: 1 }]}
                        value={CategoryCode}
                        onChangeText={(t) => setCategoryCode(t.toUpperCase())}
                        placeholder="Enter Code"
                        placeholderTextColor={subtitleColor}
                        editable={!isEditing}
                        autoCapitalize="characters"
                      />
                    </View>
                    <View style={{ flex: 1, flexDirection: 'row', alignItems: 'center' }}>
                      <ThemedText style={[styles.label, { color: subtitleColor, width: 120, marginBottom: 0 }]}>
                        Category Name
                      </ThemedText>
                      <TextInput
                        style={[styles.input, { backgroundColor: inputBg, color: textColor, borderColor, height: 40, flex: 1 }]}
                        value={CategoryName}
                        onChangeText={setCategoryName}
                        placeholder="Enter Name"
                        placeholderTextColor={subtitleColor}
                      />
                    </View>
                  </View>

                  {/* Row 2: Class Code with LOV search button & Class Name */}
                  <View style={{ flexDirection: 'row', gap: 24 }}>
                    <View style={{ flex: 1, flexDirection: 'row', alignItems: 'center' }}>
                      <ThemedText style={[styles.label, { color: subtitleColor, width: 120, marginBottom: 0 }]}>
                        Class Code
                      </ThemedText>
                      <View style={{ flex: 1, flexDirection: 'row', alignItems: 'center' }}>
                        <TextInput
                          style={[
                            styles.input,
                            {
                              backgroundColor: inputBg,
                              color: textColor,
                              borderColor,
                              height: 40,
                              flex: 1,
                              borderTopRightRadius: 0,
                              borderBottomRightRadius: 0,
                            },
                          ]}
                          value={ClassCode}
                          onChangeText={(t) => setClassCode(t.toUpperCase())}
                          onBlur={handleClassCodeBlur}
                          placeholder="Class Code"
                          placeholderTextColor={subtitleColor}
                          autoCapitalize="characters"
                        />
                        <TouchableOpacity
                          style={{
                            height: 40,
                            width: 38,
                            backgroundColor: 'rgba(212, 175, 55, 0.15)',
                            borderWidth: 1,
                            borderLeftWidth: 0,
                            borderColor,
                            borderTopRightRadius: 8,
                            borderBottomRightRadius: 8,
                            justifyContent: 'center',
                            alignItems: 'center',
                          }}
                          onPress={() => setClassModalVisible(true)}
                        >
                          <Ionicons name="search" size={16} color={GOLD_COLOR} />
                        </TouchableOpacity>
                      </View>
                    </View>
                    <View style={{ flex: 1, flexDirection: 'row', alignItems: 'center' }}>
                      <ThemedText style={[styles.label, { color: subtitleColor, width: 120, marginBottom: 0 }]}>
                        Class Name
                      </ThemedText>
                      <TextInput
                        style={[
                          styles.input,
                          { backgroundColor: isDark ? '#333' : '#EEE', color: textColor, borderColor, height: 40, flex: 1 },
                        ]}
                        value={ClassName}
                        placeholder="Auto Class Name"
                        placeholderTextColor={subtitleColor}
                        editable={false}
                      />
                    </View>
                  </View>

                  {/* Row 3 - Full width checkboxes */}
                  <View style={{ flexDirection: 'row', gap: 24, paddingLeft: 120 }}>
                    <CustomCheckbox
                      label="Size Factor"
                      value={SizeFactor}
                      onValueChange={setSizeFactor}
                      disabled={isEditing}
                    />
                    <CustomCheckbox
                      label="Show in Receipt"
                      value={ShowInReceipt}
                      onValueChange={setShowInReceipt}
                    />
                  </View>

                  {/* Row 4: From Inventory & To Inventory */}
                  <View style={{ flexDirection: 'row', gap: 24 }}>
                    <View style={{ flex: 1, flexDirection: 'row', alignItems: 'center' }}>
                      <ThemedText style={[styles.label, { color: subtitleColor, width: 120, marginBottom: 0 }]}>
                        From Inventory
                      </ThemedText>
                      <TextInput
                        style={[styles.input, { backgroundColor: inputBg, color: textColor, borderColor, height: 40, flex: 1 }]}
                        value={FromInventoryCode}
                        onChangeText={setFromInventoryCode}
                        placeholder="From Code"
                        placeholderTextColor={subtitleColor}
                        keyboardType="numeric"
                      />
                    </View>
                    <View style={{ flex: 1, flexDirection: 'row', alignItems: 'center' }}>
                      <ThemedText style={[styles.label, { color: subtitleColor, width: 120, marginBottom: 0 }]}>
                        To Inventory
                      </ThemedText>
                      <TextInput
                        style={[styles.input, { backgroundColor: inputBg, color: textColor, borderColor, height: 40, flex: 1 }]}
                        value={ToInventoryCode}
                        onChangeText={setToInventoryCode}
                        placeholder="To Code"
                        placeholderTextColor={subtitleColor}
                        keyboardType="numeric"
                      />
                    </View>
                  </View>

                  {/* Row 5: Next Inventory & Inventory Prefix */}
                  <View style={{ flexDirection: 'row', gap: 24 }}>
                    <View style={{ flex: 1, flexDirection: 'row', alignItems: 'center' }}>
                      <ThemedText style={[styles.label, { color: subtitleColor, width: 120, marginBottom: 0 }]}>
                        Next Inventory
                      </ThemedText>
                      <TextInput
                        style={[styles.input, { backgroundColor: inputBg, color: textColor, borderColor, height: 40, flex: 1 }]}
                        value={NextInventoryCode}
                        onChangeText={setNextInventoryCode}
                        placeholder={FromInventoryCode || 'Next Code'}
                        placeholderTextColor={subtitleColor}
                        keyboardType="numeric"
                      />
                    </View>
                    <View style={{ flex: 1, flexDirection: 'row', alignItems: 'center' }}>
                      <ThemedText style={[styles.label, { color: subtitleColor, width: 120, marginBottom: 0 }]}>
                        Inventory Prefix
                      </ThemedText>
                      <TextInput
                        style={[styles.input, { backgroundColor: inputBg, color: textColor, borderColor, height: 40, flex: 1 }]}
                        value={InventoryPrefix}
                        onChangeText={(t) => setInventoryPrefix(t.toUpperCase())}
                        placeholder="Prefix"
                        placeholderTextColor={subtitleColor}
                        autoCapitalize="characters"
                      />
                    </View>
                  </View>

                  {/* Row 6: Remarks */}
                  <View style={{ flexDirection: 'row', gap: 24 }}>
                    <View style={{ flex: 1, flexDirection: 'row', alignItems: 'center' }}>
                      <ThemedText style={[styles.label, { color: subtitleColor, width: 120, marginBottom: 0 }]}>
                        Remarks
                      </ThemedText>
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
        {renderToast()}
      </Modal>

      {/* Class LOV Modal */}
      <Modal visible={classModalVisible} animationType="fade" transparent={true}>
        <TouchableWithoutFeedback onPress={() => setClassModalVisible(false)}>
          <View style={styles.modalOverlay}>
            <TouchableWithoutFeedback>
              <View style={[styles.modalContent, { backgroundColor: cardColor, borderColor, maxWidth: 650 }]}>
                <View style={[styles.modalHeader, { borderBottomColor: borderColor }]}>
                  <ThemedText style={[styles.modalTitle, { color: textColor }]}>Select Class</ThemedText>
                  <TouchableOpacity onPress={() => setClassModalVisible(false)}>
                    <Ionicons name="close" size={24} color={subtitleColor} />
                  </TouchableOpacity>
                </View>

                <View style={{ padding: 16 }}>
                  <TextInput
                    style={[styles.input, { backgroundColor: inputBg, color: textColor, borderColor, height: 38, marginBottom: 12 }]}
                    placeholder="Search Class Code or Name..."
                    placeholderTextColor={subtitleColor}
                    value={classSearch}
                    onChangeText={setClassSearch}
                  />

                  <View style={{ flexDirection: 'row', backgroundColor: 'rgba(0,0,0,0.04)', paddingVertical: 8, paddingHorizontal: 12, borderRadius: 6, marginBottom: 6 }}>
                    <ThemedText style={{ width: 110, fontWeight: 'bold', fontSize: 11, color: subtitleColor }}>CODE</ThemedText>
                    <ThemedText style={{ flex: 1, fontWeight: 'bold', fontSize: 11, color: subtitleColor }}>NAME</ThemedText>
                    <ThemedText style={{ width: 80, fontWeight: 'bold', fontSize: 11, color: subtitleColor, textAlign: 'right' }}>RATE</ThemedText>
                  </View>

                  <FlatList
                    data={filteredClasses}
                    keyExtractor={(c) => c.ClassCode}
                    style={{ maxHeight: 280 }}
                    renderItem={({ item }) => (
                      <TouchableOpacity
                        style={{ flexDirection: 'row', paddingVertical: 10, paddingHorizontal: 12, borderBottomWidth: 1, borderBottomColor: borderColor }}
                        onPress={() => {
                          setClassCode(item.ClassCode);
                          setClassName(item.ClassName || '');
                          setClassModalVisible(false);
                        }}
                      >
                        <ThemedText style={{ width: 110, fontWeight: '600', fontSize: 12, color: GOLD_COLOR }}>{item.ClassCode}</ThemedText>
                        <ThemedText style={{ flex: 1, fontSize: 12, color: textColor }}>{item.ClassName}</ThemedText>
                        <ThemedText style={{ width: 80, fontSize: 12, color: textColor, textAlign: 'right' }}>{Number(item.Rate || 0).toFixed(2)}</ThemedText>
                      </TouchableOpacity>
                    )}
                  />
                </View>
              </View>
            </TouchableWithoutFeedback>
          </View>
        </TouchableWithoutFeedback>
        {renderToast()}
      </Modal>

      {/* Delete Confirmation Modal */}
      <Modal visible={!!deleteConfirmCode} animationType="fade" transparent={true}>
        <TouchableWithoutFeedback onPress={() => setDeleteConfirmCode(null)}>
          <View style={styles.modalOverlay}>
            <TouchableWithoutFeedback>
              <View style={[styles.modalContent, { backgroundColor: cardColor, borderColor, maxWidth: 420, padding: 24 }]}>
                <View style={{ flexDirection: 'row', alignItems: 'center', marginBottom: 16 }}>
                  <Ionicons name="trash-outline" size={24} color="#C62828" style={{ marginRight: 10 }} />
                  <ThemedText style={[styles.modalTitle, { color: textColor, fontSize: 18 }]}>Delete Category</ThemedText>
                </View>
                <ThemedText style={{ color: textColor, fontSize: 14, marginBottom: 24, lineHeight: 20 }}>
                  Do you want to delete <ThemedText style={{ fontWeight: 'bold' }}>{deleteConfirmCode}</ThemedText> Category?
                </ThemedText>
                <View style={{ flexDirection: 'row', justifyContent: 'flex-end', gap: 12 }}>
                  <TouchableOpacity
                    style={styles.cancelButton}
                    onPress={() => setDeleteConfirmCode(null)}
                  >
                    <ThemedText style={styles.cancelButtonText}>Cancel</ThemedText>
                  </TouchableOpacity>
                  <TouchableOpacity
                    style={[styles.saveButton, { backgroundColor: '#C62828' }]}
                    onPress={() => {
                      const code = deleteConfirmCode;
                      setDeleteConfirmCode(null);
                      if (code) executeDelete(code);
                    }}
                  >
                    <ThemedText style={[styles.saveButtonText, { color: '#FFF' }]}>Delete</ThemedText>
                  </TouchableOpacity>
                </View>
              </View>
            </TouchableWithoutFeedback>
          </View>
        </TouchableWithoutFeedback>
        {renderToast()}
      </Modal>
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  safeArea: { flex: 1 },
  header: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingHorizontal: 16,
    paddingVertical: 8,
    borderBottomWidth: 1,
  },
  backButton: { padding: 4, marginRight: 12 },
  title: { fontSize: 20, fontWeight: '300', letterSpacing: 1.5 },
  addButton: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 12,
    paddingVertical: 6,
    borderRadius: 8,
    borderWidth: 1,
  },
  addButtonText: { marginLeft: 4, fontWeight: '600', fontSize: 14 },

  tableContainer: { flex: 1, margin: 15, maxWidth: 1100, width: '100%', alignSelf: 'center', padding: 10 },
  tableHeader: { flexDirection: 'row', paddingVertical: 10, paddingHorizontal: 20, marginBottom: 8, borderRadius: 12 },
  headerText: { fontSize: 10, fontWeight: 'bold', letterSpacing: 1 },
  row: {
    flexDirection: 'row',
    paddingVertical: 10,
    paddingHorizontal: 20,
    marginBottom: 8,
    borderRadius: 12,
    alignItems: 'center',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.05,
    shadowRadius: 6,
    elevation: 2,
    borderWidth: 1,
  },
  cell: { justifyContent: 'center' },
  actionCell: { flexDirection: 'row', justifyContent: 'center', width: 120 },
  actionButton: {
    padding: 6,
    marginHorizontal: 4,
    borderRadius: 8,
    backgroundColor: 'rgba(0,0,0,0.03)',
    alignItems: 'center',
    justifyContent: 'center',
  },

  modalOverlay: {
    flex: 1,
    backgroundColor: 'rgba(0,0,0,0.5)',
    justifyContent: 'center',
    alignItems: 'center',
  },
  modalContent: {
    width: '90%',
    maxWidth: 1000,
    borderWidth: 1,
    borderRadius: 12,
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.2,
    shadowRadius: 10,
    elevation: 10,
  },
  modalHeader: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    padding: 20,
    borderBottomWidth: 1,
  },
  modalTitle: { fontSize: 20, fontWeight: '700' },
  label: { fontSize: 14, fontWeight: '500' },
  input: {
    borderWidth: 1,
    borderRadius: 8,
    paddingHorizontal: 12,
    fontSize: 14,
  },
  modalActions: {
    flexDirection: 'row',
    justifyContent: 'flex-end',
    gap: 12,
    padding: 20,
    borderTopWidth: 1,
  },
  cancelButton: {
    paddingHorizontal: 20,
    paddingVertical: 12,
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#CCC',
  },
  cancelButtonText: { color: '#666', fontWeight: '600' },
  saveButton: {
    paddingHorizontal: 24,
    paddingVertical: 12,
    borderRadius: 8,
    backgroundColor: '#D4AF37',
  },
  saveButtonText: { fontWeight: '600' },
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
