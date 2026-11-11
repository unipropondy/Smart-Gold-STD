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
import { API_BASE_URL } from '@/constants/api';
import { useThemeColors } from '@/hooks/useThemeColors';
import { useThemeContext } from '@/contexts/ThemeContext';
const API_URL = `${API_BASE_URL}/catalogue`;

export default function CatalogueScreen() {
  const themeColors = useThemeColors();
  const { accentColor } = useThemeContext();
  const { background: backgroundColor, card: cardColor, text: textColor, border: borderColor, subtitle: subtitleColor, primaryGold: GOLD_COLOR, primaryGold } = themeColors;

  
  
  

  const colorScheme = useColorScheme();
  const isDark = colorScheme === 'dark';
  const router = useRouter();

  const [data, setData] = useState<any[]>([]);
  const [loading, setLoading] = useState(true);
  const [saving, setSaving] = useState(false);
  const [deletingId, setDeletingId] = useState<string | null>(null);
  const [toast, setToast] = useState<{ message: string; type: 'success' | 'error' } | null>(null);
  const [deleteConfirmCode, setDeleteConfirmCode] = useState<string | null>(null);

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

  // Form State
  const [catalogueCode, setCatalogueCode] = useState('');
  const [catalogueName, setCatalogueName] = useState('');
  const [categoryCode, setCategoryCode] = useState('');
  const [categoryName, setCategoryName] = useState('');
  const [remarks, setRemarks] = useState('');
  const [isActive, setIsActive] = useState(true);

  const [categories, setCategories] = useState<any[]>([]);
  const [isCategoryLookupVisible, setIsCategoryLookupVisible] = useState(false);
  const [categorySearch, setCategorySearch] = useState('');

  
  const headerBgColor = isDark ? '#2A2A2A' : '#F5F5F5';

  const fetchCatalogues = async () => {
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

  const fetchCategoriesForLookup = async () => {
    try {
      const response = await fetch(`${API_BASE_URL}/category?t=${Date.now()}`);
      const json = await response.json();
      if (json.success) {
        setCategories(json.data);
      }
    } catch (error) {
      console.error('Failed to fetch categories:', error);
    }
  };

  useEffect(() => {
    fetchCatalogues();
    fetchCategoriesForLookup();
  }, []);

  // 1. txtCatalogueCode blur validation
  const handleCatalogueCodeBlur = () => {
    if (!catalogueCode || !catalogueCode.trim()) {
      showError('CatalogueCode should not be blank.');
    }
  };

  // 2. txtCatalogueName blur validation
  const handleCatalogueNameBlur = () => {
    if (!catalogueName || !catalogueName.trim()) {
      showError('CatalogueName should not be blank.');
    }
  };

  // 3. txtCategoryCode blur validation & auto-fetch category name matching frmCatalogue.vb
  const handleCategoryCodeBlur = async () => {
    const trimmed = (categoryCode || '').trim();
    if (!trimmed) {
      showError('CategoryCode should not be blank.');
      setCategoryName('');
      return;
    }

    try {
      const res = await fetch(`${API_URL}/validate-category/${encodeURIComponent(trimmed)}`);
      const json = await res.json();
      if (json.success && json.data) {
        setCategoryName(json.data.CategoryName || '');
      } else {
        showError('Pleae enter valid CatagoryCode.');
        setCategoryCode('');
        setCategoryName('');
      }
    } catch (e) {
      // Local fallback lookup
      const matched = categories.find((c: any) => (c.CategoryCode || '').trim().toUpperCase() === trimmed.toUpperCase());
      if (matched) {
        setCategoryName(matched.CategoryName || '');
      } else {
        showError('Pleae enter valid CatagoryCode.');
        setCategoryCode('');
        setCategoryName('');
      }
    }
  };

  const handleSave = async () => {
    // Exact validations from subValidateMe in frmCatalogue.vb (lines 808-834)
    if (!catalogueCode || !catalogueCode.trim()) {
      showError('CatalogueCode should not be blank.');
      return;
    }
    if (!catalogueName || !catalogueName.trim()) {
      showError('CatalogueName should not be blank.');
      return;
    }
    if (!categoryCode || !categoryCode.trim()) {
      showError('CategoryCode should not be blank.');
      return;
    }
    if (!categoryName || !categoryName.trim()) {
      showError('Pleae enter valid CatagoryCode.');
      return;
    }

    const payload = {
      CatalogueCode: catalogueCode.trim().toUpperCase(),
      CatalogueName: catalogueName.trim(),
      CategoryCode: categoryCode.trim().toUpperCase(),
      Remarks: remarks.trim(),
      IsActive: isActive,
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
        showSuccess(json.message || 'Catalogue detail saved sucssfully.');
        setIsModalVisible(false);
        fetchCatalogues();
      } else {
        showError(json.message || 'Error in Save');
      }
    } catch (error) {
      console.error('Save error:', error);
      showError('Failed to save catalogue');
    } finally {
      setSaving(false);
    }
  };

  const executeDelete = async (code: string) => {
    setDeletingId(code);
    try {
      const response = await fetch(`${API_URL}?id=${encodeURIComponent(code)}`, { method: 'DELETE' });
      const json = await response.json();
      if (json.success) {
        showSuccess(json.message || 'Catalogue detail deleted.');
        fetchCatalogues();
      } else {
        showError(json.message || 'Error in deleting Catalogue detail.');
      }
    } catch (error) {
      console.error('Delete error:', error);
      showError('Failed to delete catalogue');
    } finally {
      setDeletingId(null);
    }
  };

  const handleDelete = (code: string) => {
    setDeleteConfirmCode(code);
  };

  const openAddModal = () => {
    setIsEditing(false);
    setCatalogueCode('');
    setCatalogueName('');
    setCategoryCode('');
    setCategoryName('');
    setRemarks('');
    setIsActive(true);
    setIsModalVisible(true);
  };

  const openEditModal = (item: any) => {
    setIsEditing(true);
    setCatalogueCode(item.CatalogueCode);
    setCatalogueName(item.CatalogueName || '');
    setCategoryCode(item.CategoryCode || '');
    
    // Find category name from our lookup data
    const matchedCategory = categories.find((c: any) => c.CategoryCode === item.CategoryCode);
    setCategoryName(matchedCategory ? matchedCategory.CategoryName : '');
    
    setRemarks(item.Remarks || '');
    setIsActive(item.IsActive !== false && item.IsActive !== 0);
    setIsModalVisible(true);
  };

  const renderItem = ({ item }: { item: any }) => (
    <View style={[styles.row, { backgroundColor: cardColor, borderColor: borderColor }]}>
      <View style={[styles.cell, { flex: 1.5 }]}>
        <ThemedText type="tableText" style={{ color: textColor, fontWeight: '500', fontSize: 11 }}>{item.CatalogueCode}</ThemedText>
      </View>
      <View style={[styles.cell, { flex: 2 }]}>
        <ThemedText type="tableText" style={{ color: textColor, fontSize: 11 }}>{item.CatalogueName}</ThemedText>
      </View>
      <View style={[styles.cell, { flex: 1.5 }]}>
        <ThemedText type="tableText" style={{ color: textColor, fontSize: 11 }}>{item.CategoryCode}</ThemedText>
      </View>
      <View style={[styles.cell, { flex: 1, alignItems: 'center' }]}>
        <ThemedText type="tableText" style={{ color: textColor, fontSize: 11 }}>{item.ReOrderLevel ?? 0}</ThemedText>
      </View>
      <View style={styles.actionCell}>
        <TouchableOpacity style={styles.actionButton} onPress={() => openEditModal(item)} disabled={deletingId === item.CatalogueCode}>
          <Ionicons name="pencil-outline" size={20} color={GOLD_COLOR} />
        </TouchableOpacity>
        <TouchableOpacity style={styles.actionButton} onPress={() => handleDelete(item.CatalogueCode)} disabled={deletingId === item.CatalogueCode}>
          {deletingId === item.CatalogueCode ? (
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
          <ThemedText style={[styles.title, { color: textColor }]}>Catalogue Master</ThemedText>
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
        <View style={[styles.cell, { flex: 1.5 }]}><ThemedText style={[styles.headerText, { color: subtitleColor }]}>CATEGORY</ThemedText></View>
        <View style={[styles.cell, { flex: 1, alignItems: 'center' }]}><ThemedText style={[styles.headerText, { color: subtitleColor }]}>REORDER</ThemedText></View>
        <View style={[styles.actionCell, { width: 120 }]}><ThemedText style={[styles.headerText, { color: subtitleColor, width: 55, textAlign: 'center' }]}>EDIT</ThemedText><ThemedText style={[styles.headerText, { color: subtitleColor, width: 55, textAlign: 'center' }]}>DELETE</ThemedText></View>
      </View>

      {loading ? (
        <View style={styles.centered}><ActivityIndicator size="large" color={GOLD_COLOR} /></View>
      ) : (
        <FlatList
          data={data}
          renderItem={renderItem}
          keyExtractor={(item, index) => item.CatalogueCode || index.toString()}
          contentContainerStyle={{ paddingBottom: 20 }}
          ListEmptyComponent={
            <View style={styles.centered}>
              <ThemedText style={{ color: subtitleColor }}>No catalogues found.</ThemedText>
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
                  {isEditing ? 'Edit Catalogue' : 'Add New Catalogue'}
                </ThemedText>
                <TouchableOpacity onPress={() => setIsModalVisible(false)}>
                  <Ionicons name="close" size={24} color={textColor} />
                </TouchableOpacity>
              </View>

              <View style={styles.modalBody}>
                <View style={styles.inputGroup}>
                  <ThemedText style={[styles.label, { color: subtitleColor }]}>Catalogue Code *</ThemedText>
                  <TextInput
                    style={[styles.input, { color: textColor, borderColor, backgroundColor: isDark ? '#2A2A2A' : '#FFF' }, isEditing && { backgroundColor: isDark ? '#333' : '#F0F0F0', color: subtitleColor }]}
                    value={catalogueCode}
                    onChangeText={(t) => setCatalogueCode(t.toUpperCase())}
                    onBlur={handleCatalogueCodeBlur}
                    editable={!isEditing}
                    placeholder="Enter Catalogue Code"
                    placeholderTextColor={subtitleColor}
                    autoCapitalize="characters"
                  />
                </View>

                <View style={styles.inputGroup}>
                  <ThemedText style={[styles.label, { color: subtitleColor }]}>Catalogue Name *</ThemedText>
                  <TextInput
                    style={[styles.input, { color: textColor, borderColor, backgroundColor: isDark ? '#2A2A2A' : '#FFF' }]}
                    value={catalogueName}
                    onChangeText={setCatalogueName}
                    onBlur={handleCatalogueNameBlur}
                    placeholder="Enter Catalogue Name"
                    placeholderTextColor={subtitleColor}
                  />
                </View>

                {/* Category Lookup Row */}
                <View style={styles.inputGroup}>
                  <ThemedText style={[styles.label, { color: subtitleColor }]}>Category Code *</ThemedText>
                  <View style={styles.lookupContainer}>
                    <TextInput
                      style={[styles.input, { color: textColor, borderColor, backgroundColor: isDark ? '#2A2A2A' : '#FFF' }]}
                      value={categoryCode}
                      onChangeText={(t) => setCategoryCode(t.toUpperCase())}
                      onBlur={handleCategoryCodeBlur}
                      placeholder="Enter Category Code"
                      placeholderTextColor={subtitleColor}
                      autoCapitalize="characters"
                    />
                    <TouchableOpacity style={styles.lookupButton} onPress={() => setIsCategoryLookupVisible(true)}>
                      <ThemedText style={{ color: textColor, fontWeight: 'bold' }}>...</ThemedText>
                    </TouchableOpacity>
                  </View>
                </View>

                <View style={styles.inputGroup}>
                  <ThemedText style={[styles.label, { color: subtitleColor }]}>Category Name</ThemedText>
                  <TextInput
                    style={[styles.input, { color: subtitleColor, borderColor, backgroundColor: isDark ? '#333' : '#F0F0F0' }]}
                    value={categoryName}
                    editable={false}
                    placeholder="Category Name"
                    placeholderTextColor={subtitleColor}
                  />
                </View>

                {/* Active Checkbox */}
                <View style={styles.checkboxContainer}>
                  <ThemedText style={[styles.checkboxLabel, { color: subtitleColor }]}></ThemedText>
                  <View style={styles.checkboxControl}>
                    <TouchableOpacity
                      style={{ flexDirection: 'row', alignItems: 'center' }}
                      onPress={() => setIsActive(!isActive)}
                    >
                      <View style={[styles.checkbox, { borderColor, backgroundColor: isActive ? GOLD_COLOR : 'transparent' }]}>
                        {isActive && <Ionicons name="checkmark" size={16} color="#FFF" />}
                      </View>
                      <ThemedText style={{ color: textColor, marginLeft: 8 }}>Active</ThemedText>
                    </TouchableOpacity>
                  </View>
                </View>

                <View style={styles.inputGroup}>
                  <ThemedText style={[styles.label, { color: subtitleColor }]}>Remarks</ThemedText>
                  <TextInput
                    style={[styles.input, { color: textColor, borderColor, backgroundColor: isDark ? '#2A2A2A' : '#FFF' }]}
                    value={remarks}
                    onChangeText={setRemarks}
                    placeholder="Enter Remarks"
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

      {/* Category Lookup Modal */}
      <Modal visible={isCategoryLookupVisible} transparent animationType="slide">
        <View style={styles.modalOverlay}>
          <View style={[styles.modalContent, { backgroundColor: cardColor, borderColor, height: '80%' }]}>
            <View style={[styles.modalHeader, { borderBottomColor: borderColor }]}>
              <ThemedText style={{ fontSize: 18, fontWeight: 'bold', color: textColor }}>
                Select Category
              </ThemedText>
              <TouchableOpacity onPress={() => setIsCategoryLookupVisible(false)}>
                <Ionicons name="close" size={24} color={textColor} />
              </TouchableOpacity>
            </View>
            <View style={{ padding: 16, borderBottomWidth: 1, borderBottomColor: borderColor }}>
              <TextInput
                style={[styles.input, { color: textColor, borderColor, backgroundColor: isDark ? '#2A2A2A' : '#FFF' }]}
                placeholder="Search Categories..."
                placeholderTextColor={subtitleColor}
                value={categorySearch}
                onChangeText={setCategorySearch}
              />
            </View>
            <FlatList
              data={categories.filter(c => 
                (c.CategoryCode || '').toLowerCase().includes(categorySearch.toLowerCase()) || 
                (c.CategoryName || '').toLowerCase().includes(categorySearch.toLowerCase())
              )}
              keyExtractor={(item) => item.CategoryCode}
              renderItem={({ item }) => (
                <TouchableOpacity 
                  style={{ padding: 16, borderBottomWidth: 1, borderBottomColor: borderColor, flexDirection: 'row' }}
                  onPress={() => {
                    setCategoryCode(item.CategoryCode);
                    setCategoryName(item.CategoryName);
                    setIsCategoryLookupVisible(false);
                    setCategorySearch('');
                  }}
                >
                  <ThemedText style={{ color: textColor, flex: 1, fontWeight: 'bold' }}>{item.CategoryCode}</ThemedText>
                  <ThemedText style={{ color: textColor, flex: 2 }}>{item.CategoryName}</ThemedText>
                </TouchableOpacity>
              )}
              ListEmptyComponent={
                <View style={[styles.centered, { marginTop: 20 }]}>
                  <ThemedText style={{ color: subtitleColor }}>No categories found.</ThemedText>
                </View>
              }
            />
          </View>
        </View>
      </Modal>

      {/* Delete Confirmation Modal */}
      <Modal visible={!!deleteConfirmCode} animationType="fade" transparent={true}>
        <TouchableWithoutFeedback onPress={() => setDeleteConfirmCode(null)}>
          <View style={styles.modalOverlay}>
            <TouchableWithoutFeedback>
              <View style={[styles.modalContent, { backgroundColor: cardColor, borderColor, maxWidth: 420, padding: 24 }]}>
                <View style={{ flexDirection: 'row', alignItems: 'center', marginBottom: 16 }}>
                  <Ionicons name="trash-outline" size={24} color="#C62828" style={{ marginRight: 10 }} />
                  <ThemedText style={{ fontSize: 18, fontWeight: 'bold', color: textColor }}>Delete Catalogue</ThemedText>
                </View>
                <ThemedText style={{ color: textColor, fontSize: 14, marginBottom: 24, lineHeight: 20 }}>
                  Do you want to delete <ThemedText style={{ fontWeight: 'bold' }}>{deleteConfirmCode}</ThemedText> Catalogue?
                </ThemedText>
                <View style={{ flexDirection: 'row', justifyContent: 'flex-end', gap: 12 }}>
                  <TouchableOpacity
                    style={[styles.btn, styles.btnCancel]}
                    onPress={() => setDeleteConfirmCode(null)}
                  >
                    <ThemedText style={{ color: '#666', fontWeight: '600' }}>Cancel</ThemedText>
                  </TouchableOpacity>
                  <TouchableOpacity
                    style={[styles.btn, { backgroundColor: '#C62828' }]}
                    onPress={() => {
                      const code = deleteConfirmCode;
                      setDeleteConfirmCode(null);
                      if (code) executeDelete(code);
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
