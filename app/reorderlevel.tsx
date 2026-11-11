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
  ScrollView,
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';
import { ThemedText } from '@/components/themed-text';
import { API_BASE_URL } from '@/constants/api';
import { useThemeColors } from '@/hooks/useThemeColors';
import { useThemeContext } from '@/contexts/ThemeContext';
const API_URL = `${API_BASE_URL}/reorderlevel`;

export default function ReOrderLevelScreen() {
  const themeColors = useThemeColors();
  const { accentColor } = useThemeContext();
  const { background: backgroundColor, card: cardColor, text: textColor, border: borderColor, subtitle: subtitleColor, primaryGold: GOLD_COLOR } = themeColors;

  
  
  

  const colorScheme = useColorScheme();
  const isDark = colorScheme === 'dark';
  const router = useRouter();

  const [data, setData] = useState<any[]>([]);
  const [loading, setLoading] = useState(true);
  const [saving, setSaving] = useState(false);
  const [deletingId, setDeletingId] = useState<string | null>(null);
  const [toast, setToast] = useState<{ message: string; type: 'success' | 'error' } | null>(null);
  const [deleteConfirmItem, setDeleteConfirmItem] = useState<any | null>(null);

  const showToast = (message: string, type: 'success' | 'error' = 'success') => {
    setToast({ message, type });
    setTimeout(() => setToast(null), 3000);
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
  const [originalKeys, setOriginalKeys] = useState<{ locationCode: string; categoryCode: string; catalogueCode: string } | null>(null);

  // Form State
  const [locationCode, setLocationCode] = useState('');
  const [categoryCode, setCategoryCode] = useState('');
  const [catalogueCode, setCatalogueCode] = useState('');
  const [weight, setWeight] = useState('');
  const [toWeight, setToWeight] = useState('');
  const [sizeCode, setSizeCode] = useState('');
  const [toSizeCode, setToSizeCode] = useState('');
  const [reOrderLevel, setReOrderLevel] = useState('');
  const [remarks, setRemarks] = useState('');

  // Lookups
  const [lookupType, setLookupType] = useState<'location'|'category'|'catalogue'|'size'|'toSize'|null>(null);
  const [lookupSearch, setLookupSearch] = useState('');
  
  const [locations, setLocations] = useState<any[]>([]);
  const [categories, setCategories] = useState<any[]>([]);
  const [catalogues, setCatalogues] = useState<any[]>([]);
  const [sizes, setSizes] = useState<any[]>([]);

  

  const fetchData = async () => {
    try {
      const response = await fetch(`${API_URL}?t=${Date.now()}`, { headers: { 'Cache-Control': 'no-cache' }});
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

  const fetchLookups = async () => {
    try {
      const locRes = await fetch(`${API_BASE_URL}/location?t=${Date.now()}`);
      if (locRes.ok) {
        const locJson = await locRes.json();
        if (locJson.success) setLocations(locJson.data);
      }

      const catRes = await fetch(`${API_BASE_URL}/category?t=${Date.now()}`);
      if (catRes.ok) {
        const catJson = await catRes.json();
        if (catJson.success) setCategories(catJson.data);
      }

      const ctgRes = await fetch(`${API_BASE_URL}/catalogue?t=${Date.now()}`);
      if (ctgRes.ok) {
        const ctgJson = await ctgRes.json();
        if (ctgJson.success) setCatalogues(ctgJson.data);
      }

      const sizeRes = await fetch(`${API_BASE_URL}/size?t=${Date.now()}`);
      if (sizeRes.ok) {
        const sizeJson = await sizeRes.json();
        if (sizeJson.success) setSizes(sizeJson.data);
      }
    } catch (error) {
      console.error('Failed to fetch lookups:', error);
    }
  };

  useEffect(() => {
    fetchData();
    fetchLookups();
  }, []);

  // Field-level blur handlers matching subValidateMe in frmReOrderLevelMaster.vb
  const handleCatalogueCodeBlur = () => {
    const trimmed = (catalogueCode || '').trim();
    if (!trimmed) {
      showError('CatalogueCode should not be blank.');
    }
  };

  const handleFromWeightBlur = () => {
    const trimmed = (weight || '').trim();
    if (!trimmed) {
      showError('Weight should not be blank.');
    }
  };

  const handleToWeightBlur = () => {
    const trimmed = (toWeight || '').trim();
    if (!trimmed) {
      showError('Weight should not be blank.');
    }
  };

  const handleCategoryCodeBlur = () => {
    const trimmed = (categoryCode || '').trim();
    if (!trimmed) {
      showError('CategoryCode should not be blank.');
    }
  };

  const handleSizeCodeBlur = () => {
    const trimmed = (sizeCode || '').trim();
    if (trimmed && sizes.length > 0) {
      const match = sizes.find((s: any) => (s.SizeCode || '').trim().toUpperCase() === trimmed.toUpperCase());
      if (!match) {
        showError('Invalid Size Code!');
        setSizeCode('');
      }
    }
  };

  const handleToSizeCodeBlur = () => {
    const trimmed = (toSizeCode || '').trim();
    if (trimmed && sizes.length > 0) {
      const match = sizes.find((s: any) => (s.SizeCode || '').trim().toUpperCase() === trimmed.toUpperCase());
      if (!match) {
        showError('Invalid Size Code!');
        setToSizeCode('');
      }
    }
  };

  const handleSave = async () => {
    const trimmedCatalogue = (catalogueCode || '').trim();
    const trimmedFromWeight = (weight || '').trim();
    const trimmedToWeight = (toWeight || '').trim();
    const trimmedCategory = (categoryCode || '').trim();

    if (!trimmedCatalogue) {
      showError('CatalogueCode should not be blank.');
      return;
    }

    if (!trimmedFromWeight) {
      showError('Weight should not be blank.');
      return;
    }

    if (!trimmedToWeight) {
      showError('Weight should not be blank.');
      return;
    }

    if (!trimmedCategory) {
      showError('CategoryCode should not be blank.');
      return;
    }

    const payload = {
      OriginalLocationCode: isEditing && originalKeys ? originalKeys.locationCode : undefined,
      OriginalCategoryCode: isEditing && originalKeys ? originalKeys.categoryCode : undefined,
      OriginalCatalogueCode: isEditing && originalKeys ? originalKeys.catalogueCode : undefined,
      LocationCode: (locationCode || '').trim(),
      CategoryCode: trimmedCategory,
      CatalogueCode: trimmedCatalogue,
      Weight: trimmedFromWeight,
      ToWeight: trimmedToWeight,
      SizeCode: (sizeCode || '').trim(),
      ToSizeCode: (toSizeCode || '').trim(),
      ReOrderLevel: (reOrderLevel || '').trim(),
      Remarks: (remarks || '').trim(),
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
        showSuccess(json.message || 'ReOrder detail saved sucssfully.');
        setIsModalVisible(false);
        fetchData();
      } else {
        showError(json.message || 'Error in Save');
      }
    } catch (error) {
      console.error('Save error:', error);
      showError('Error in Save');
    } finally {
      setSaving(false);
    }
  };

  const executeDelete = async (item: any) => {
    const id = `${item.LocationCode}-${item.CategoryCode}-${item.CatalogueCode}`;
    setDeletingId(id);
    try {
      const qs = `?LocationCode=${encodeURIComponent(item.LocationCode || '')}&CategoryCode=${encodeURIComponent(item.CategoryCode || '')}&CatalogueCode=${encodeURIComponent(item.CatalogueCode || '')}`;
      const response = await fetch(`${API_URL}${qs}`, { method: 'DELETE' });
      const json = await response.json();
      if (json.success) {
        showSuccess(json.message || 'Catalogue detail deleted.');
        fetchData();
      } else {
        showError(json.message || 'Error in deleting Catalogue detail.');
      }
    } catch (error) {
      console.error('Delete error:', error);
      showError('Error in deleting Catalogue detail.');
    } finally {
      setDeletingId(null);
    }
  };

  const handleDelete = (item: any) => {
    setDeleteConfirmItem(item);
  };

  const openAddModal = () => {
    setIsEditing(false);
    setOriginalKeys(null);
    setLocationCode('');
    setCategoryCode('');
    setCatalogueCode('');
    setWeight('');
    setToWeight('');
    setSizeCode('');
    setToSizeCode('');
    setReOrderLevel('');
    setRemarks('');
    setIsModalVisible(true);
  };

  const openEditModal = (item: any) => {
    setIsEditing(true);
    setOriginalKeys({
      locationCode: (item.LocationCode || '').trim(),
      categoryCode: (item.CategoryCode || '').trim(),
      catalogueCode: (item.CatalogueCode || '').trim(),
    });
    setLocationCode(item.LocationCode || '');
    setCategoryCode(item.CategoryCode || '');
    setCatalogueCode(item.CatalogueCode || '');
    setWeight(item.Weight !== null ? String(item.Weight) : '');
    setToWeight(item.ToWeight !== null ? String(item.ToWeight) : '');
    setSizeCode(item.SizeCode || '');
    setToSizeCode(item.ToSizeCode || '');
    setReOrderLevel(item.ReOrderLevel !== null ? String(item.ReOrderLevel) : '');
    setRemarks(item.Remarks || '');
    setIsModalVisible(true);
  };

  const renderItem = ({ item }: { item: any }) => (
    <View style={[styles.row, { backgroundColor: cardColor, borderColor: borderColor }]}>
      <View style={[styles.cell, { flex: 1.5 }]}>
        <ThemedText type="tableText" style={{ color: textColor, fontWeight: '500', fontSize: 11 }}>{item.LocationCode}</ThemedText>
      </View>
      <View style={[styles.cell, { flex: 1.5 }]}>
        <ThemedText type="tableText" style={{ color: textColor, fontSize: 11 }}>{item.CategoryCode}</ThemedText>
      </View>
      <View style={[styles.cell, { flex: 1.5 }]}>
        <ThemedText type="tableText" style={{ color: textColor, fontSize: 11 }}>{item.CatalogueCode}</ThemedText>
      </View>
      <View style={[styles.cell, { flex: 1, alignItems: 'center' }]}>
        <ThemedText type="tableText" style={{ color: textColor, fontSize: 11 }}>{item.ReOrderLevel ?? 0}</ThemedText>
      </View>
      <View style={[styles.actionCell, { alignItems: 'center', justifyContent: 'center' }]}>
        <TouchableOpacity style={styles.actionButton} onPress={() => openEditModal(item)} disabled={deletingId === `${item.LocationCode}-${item.CategoryCode}-${item.CatalogueCode}`}>
          <Ionicons name="pencil-outline" size={20} color={GOLD_COLOR} />
        </TouchableOpacity>
        <TouchableOpacity style={styles.actionButton} onPress={() => handleDelete(item)} disabled={deletingId === `${item.LocationCode}-${item.CategoryCode}-${item.CatalogueCode}`}>
          {deletingId === `${item.LocationCode}-${item.CategoryCode}-${item.CatalogueCode}` ? (
            <ActivityIndicator size="small" color="#F44336" />
          ) : (
            <Ionicons name="trash-outline" size={20} color="#F44336" />
          )}
        </TouchableOpacity>
      </View>
    </View>
  );

  const getLookupData = () => {
    switch (lookupType) {
      case 'location': return locations;
      case 'category': return categories;
      case 'catalogue': return catalogues;
      case 'size': 
      case 'toSize': return sizes;
      default: return [];
    }
  };

  const renderLookupItem = ({ item }: { item: any }) => {
    let code = '';
    let name = '';
    if (lookupType === 'location') { code = item.LocationCode; name = item.LocationName; }
    else if (lookupType === 'category') { code = item.CategoryCode; name = item.CategoryName; }
    else if (lookupType === 'catalogue') { code = item.CatalogueCode; name = item.CatalogueName; }
    else if (lookupType === 'size' || lookupType === 'toSize') { code = item.SizeCode; name = item.SizeName; }

    return (
      <TouchableOpacity 
        style={{ padding: 16, borderBottomWidth: 1, borderBottomColor: borderColor, flexDirection: 'row' }}
        onPress={() => {
          if (lookupType === 'location') setLocationCode(code);
          else if (lookupType === 'category') setCategoryCode(code);
          else if (lookupType === 'catalogue') setCatalogueCode(code);
          else if (lookupType === 'size') setSizeCode(code);
          else if (lookupType === 'toSize') setToSizeCode(code);
          setLookupType(null);
          setLookupSearch('');
        }}
      >
        <ThemedText style={{ color: textColor, flex: 1, fontWeight: 'bold' }}>{code}</ThemedText>
        <ThemedText style={{ color: textColor, flex: 2 }}>{name}</ThemedText>
      </TouchableOpacity>
    );
  };

  const lookupData = getLookupData().filter(i => {
    const term = lookupSearch.toLowerCase();
    let code = '';
    let name = '';
    if (lookupType === 'location') { code = i.LocationCode; name = i.LocationName; }
    else if (lookupType === 'category') { code = i.CategoryCode; name = i.CategoryName; }
    else if (lookupType === 'catalogue') { code = i.CatalogueCode; name = i.CatalogueName; }
    else if (lookupType === 'size' || lookupType === 'toSize') { code = i.SizeCode; name = i.SizeName; }
    
    return (code || '').toLowerCase().includes(term) || (name || '').toLowerCase().includes(term);
  });

  const renderToast = () => {
    if (!toast) return null;
    const isError = toast.type === 'error';
    return (
      <View style={[styles.toast, { backgroundColor: isError ? '#F44336' : '#4CAF50' }]}>
        <Ionicons
          name={isError ? 'alert-circle' : 'checkmark-circle'}
          size={16}
          color="#FFF"
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
          <ThemedText style={[styles.title, { color: textColor }]}>ReOrder Level Master</ThemedText>
        </View>
        <TouchableOpacity style={[styles.addButton, { backgroundColor: 'rgba(212, 175, 55, 0.15)', borderColor: GOLD_COLOR }]} onPress={openAddModal}>
          <Ionicons name="add" size={20} color={GOLD_COLOR} />
          <ThemedText style={[styles.addButtonText, { color: GOLD_COLOR }]}>New</ThemedText>
        </TouchableOpacity>
      </View>

      <View style={styles.tableContainer}>
      {/* Table Header */}
      <View style={[styles.tableHeader, { backgroundColor: 'rgba(0,0,0,0.02)' }]}>
        <View style={[styles.cell, { flex: 1.5 }]}><ThemedText style={[styles.headerText, { color: subtitleColor }]}>LOCATION</ThemedText></View>
        <View style={[styles.cell, { flex: 1.5 }]}><ThemedText style={[styles.headerText, { color: subtitleColor }]}>CATEGORY</ThemedText></View>
        <View style={[styles.cell, { flex: 1.5 }]}><ThemedText style={[styles.headerText, { color: subtitleColor }]}>CATALOGUE</ThemedText></View>
        <View style={[styles.cell, { flex: 1, alignItems: 'center' }]}><ThemedText style={[styles.headerText, { color: subtitleColor }]}>LEVEL</ThemedText></View>
        <View style={[styles.actionCell, { width: 120 }]}><ThemedText style={[styles.headerText, { color: subtitleColor, width: 55, textAlign: 'center' }]}>EDIT</ThemedText><ThemedText style={[styles.headerText, { color: subtitleColor, width: 55, textAlign: 'center' }]}>DELETE</ThemedText></View>
      </View>

      {loading ? (
        <View style={styles.centered}><ActivityIndicator size="large" color={GOLD_COLOR} /></View>
      ) : (
        <FlatList
          data={data}
          renderItem={renderItem}
          keyExtractor={(item, index) => `${item.LocationCode}-${item.CategoryCode}-${item.CatalogueCode}` || index.toString()}
          contentContainerStyle={{ paddingBottom: 20 }}
          ListEmptyComponent={
            <View style={styles.centered}>
              <ThemedText style={{ color: subtitleColor }}>No records found.</ThemedText>
            </View>
          }
        />
      )}
      </View>

      {/* Main Modal */}
      <Modal visible={isModalVisible} transparent animationType="fade">
        <TouchableOpacity style={styles.modalOverlay} activeOpacity={1} onPress={() => setIsModalVisible(false)}>
          <TouchableWithoutFeedback>
            <View style={[styles.modalContent, { backgroundColor: cardColor, borderColor }]}>
              {renderToast()}
              <View style={[styles.modalHeader, { borderBottomColor: borderColor }]}>
                <ThemedText style={{ fontSize: 18, fontWeight: 'bold', color: textColor }}>
                  {isEditing ? 'Edit ReOrder Level' : 'Add ReOrder Level'}
                </ThemedText>
                <TouchableOpacity onPress={() => setIsModalVisible(false)}>
                  <Ionicons name="close" size={24} color={textColor} />
                </TouchableOpacity>
              </View>

              <ScrollView style={styles.modalBody}>
                {/* Location Lookup */}
                <View style={styles.inputGroup}>
                  <ThemedText style={[styles.label, { color: subtitleColor }]}>Location Code *</ThemedText>
                  <View style={styles.lookupContainer}>
                    <TextInput
                      style={[styles.input, { color: textColor, borderColor, backgroundColor: isDark ? '#2A2A2A' : '#FFF' }]}
                      value={locationCode}
                      onChangeText={setLocationCode}
                      placeholder="Location"
                      placeholderTextColor={subtitleColor}
                    />
                    <TouchableOpacity style={styles.lookupButton} onPress={() => setLookupType('location')}>
                      <ThemedText style={{ color: textColor, fontWeight: 'bold' }}>...</ThemedText>
                    </TouchableOpacity>
                  </View>
                </View>

                {/* Category Lookup */}
                <View style={styles.inputGroup}>
                  <ThemedText style={[styles.label, { color: subtitleColor }]}>Category Code *</ThemedText>
                  <View style={styles.lookupContainer}>
                    <TextInput
                      style={[styles.input, { color: textColor, borderColor, backgroundColor: isDark ? '#2A2A2A' : '#FFF' }]}
                      value={categoryCode}
                      onChangeText={setCategoryCode}
                      onBlur={handleCategoryCodeBlur}
                      placeholder="Category"
                      placeholderTextColor={subtitleColor}
                    />
                    <TouchableOpacity style={styles.lookupButton} onPress={() => setLookupType('category')}>
                      <ThemedText style={{ color: textColor, fontWeight: 'bold' }}>...</ThemedText>
                    </TouchableOpacity>
                  </View>
                </View>

                {/* Catalogue Lookup */}
                <View style={styles.inputGroup}>
                  <ThemedText style={[styles.label, { color: subtitleColor }]}>Catalogue Code *</ThemedText>
                  <View style={styles.lookupContainer}>
                    <TextInput
                      style={[styles.input, { color: textColor, borderColor, backgroundColor: isDark ? '#2A2A2A' : '#FFF' }]}
                      value={catalogueCode}
                      onChangeText={setCatalogueCode}
                      onBlur={handleCatalogueCodeBlur}
                      placeholder="Catalogue"
                      placeholderTextColor={subtitleColor}
                    />
                    <TouchableOpacity style={styles.lookupButton} onPress={() => setLookupType('catalogue')}>
                      <ThemedText style={{ color: textColor, fontWeight: 'bold' }}>...</ThemedText>
                    </TouchableOpacity>
                  </View>
                </View>

                {/* Weights */}
                <View style={styles.inputGroup}>
                  <ThemedText style={[styles.label, { color: subtitleColor }]}>From Weight *</ThemedText>
                  <TextInput
                    style={[styles.input, { color: textColor, borderColor, backgroundColor: isDark ? '#2A2A2A' : '#FFF' }]}
                    value={weight}
                    onChangeText={setWeight}
                    onBlur={handleFromWeightBlur}
                    keyboardType="numeric"
                    placeholder="0.000"
                    placeholderTextColor={subtitleColor}
                  />
                </View>
                <View style={styles.inputGroup}>
                  <ThemedText style={[styles.label, { color: subtitleColor }]}>To Weight *</ThemedText>
                  <TextInput
                    style={[styles.input, { color: textColor, borderColor, backgroundColor: isDark ? '#2A2A2A' : '#FFF' }]}
                    value={toWeight}
                    onChangeText={setToWeight}
                    onBlur={handleToWeightBlur}
                    keyboardType="numeric"
                    placeholder="0.000"
                    placeholderTextColor={subtitleColor}
                  />
                </View>

                {/* Sizes Lookup */}
                <View style={styles.inputGroup}>
                  <ThemedText style={[styles.label, { color: subtitleColor }]}>From Size</ThemedText>
                  <View style={styles.lookupContainer}>
                    <TextInput
                      style={[styles.input, { color: textColor, borderColor, backgroundColor: isDark ? '#2A2A2A' : '#FFF' }]}
                      value={sizeCode}
                      onChangeText={setSizeCode}
                      onBlur={handleSizeCodeBlur}
                      placeholder="Size"
                      placeholderTextColor={subtitleColor}
                    />
                    <TouchableOpacity style={styles.lookupButton} onPress={() => setLookupType('size')}>
                      <ThemedText style={{ color: textColor, fontWeight: 'bold' }}>...</ThemedText>
                    </TouchableOpacity>
                  </View>
                </View>

                <View style={styles.inputGroup}>
                  <ThemedText style={[styles.label, { color: subtitleColor }]}>To Size</ThemedText>
                  <View style={styles.lookupContainer}>
                    <TextInput
                      style={[styles.input, { color: textColor, borderColor, backgroundColor: isDark ? '#2A2A2A' : '#FFF' }]}
                      value={toSizeCode}
                      onChangeText={setToSizeCode}
                      onBlur={handleToSizeCodeBlur}
                      placeholder="To Size"
                      placeholderTextColor={subtitleColor}
                    />
                    <TouchableOpacity style={styles.lookupButton} onPress={() => setLookupType('toSize')}>
                      <ThemedText style={{ color: textColor, fontWeight: 'bold' }}>...</ThemedText>
                    </TouchableOpacity>
                  </View>
                </View>

                <View style={styles.inputGroup}>
                  <ThemedText style={[styles.label, { color: subtitleColor }]}>ReOrder level</ThemedText>
                  <TextInput
                    style={[styles.input, { color: textColor, borderColor, backgroundColor: isDark ? '#2A2A2A' : '#FFF' }]}
                    value={reOrderLevel}
                    onChangeText={setReOrderLevel}
                    keyboardType="numeric"
                    placeholder="0"
                    placeholderTextColor={subtitleColor}
                  />
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

              </ScrollView>

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

      {/* Dynamic Lookup Modal */}
      <Modal visible={!!lookupType} transparent animationType="slide">
        <View style={styles.modalOverlay}>
          <View style={[styles.modalContent, { backgroundColor: cardColor, borderColor, height: '80%' }]}>
            <View style={[styles.modalHeader, { borderBottomColor: borderColor }]}>
              <ThemedText style={{ fontSize: 18, fontWeight: 'bold', color: textColor, textTransform: 'capitalize' }}>
                Select {lookupType?.replace(/([A-Z])/g, ' $1').trim()}
              </ThemedText>
              <TouchableOpacity onPress={() => setLookupType(null)}>
                <Ionicons name="close" size={24} color={textColor} />
              </TouchableOpacity>
            </View>
            <View style={{ padding: 16, borderBottomWidth: 1, borderBottomColor: borderColor }}>
              <TextInput
                style={[styles.input, { color: textColor, borderColor, backgroundColor: isDark ? '#2A2A2A' : '#FFF' }]}
                placeholder="Search..."
                placeholderTextColor={subtitleColor}
                value={lookupSearch}
                onChangeText={setLookupSearch}
              />
            </View>
            <FlatList
              data={lookupData}
              keyExtractor={(item, index) => index.toString()}
              renderItem={renderLookupItem}
              ListEmptyComponent={
                <View style={[styles.centered, { marginTop: 20 }]}>
                  <ThemedText style={{ color: subtitleColor }}>No records found.</ThemedText>
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
                  <ThemedText style={{ fontSize: 18, fontWeight: 'bold', color: textColor }}>Delete ReOrder Level</ThemedText>
                </View>
                <ThemedText style={{ color: textColor, fontSize: 14, marginBottom: 24, lineHeight: 22 }}>
                  Do you want to delete{' '}
                  <ThemedText style={{ fontWeight: 'bold' }}>
                    {deleteConfirmItem?.CategoryCode ? deleteConfirmItem.CategoryCode.trim() : ''}
                  </ThemedText>
                  {' Catalogue?'}
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
                      if (item) executeDelete(item);
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
    maxHeight: '90%',
    borderWidth: 1,
    borderRadius: 12,
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.3,
    shadowRadius: 8,
    elevation: 10,
    overflow: 'hidden',
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
