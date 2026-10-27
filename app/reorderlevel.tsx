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

  const handleSave = async () => {
    if (!locationCode || !locationCode.trim() || !categoryCode || !categoryCode.trim() || !catalogueCode || !catalogueCode.trim()) {
      showError('Location, Category, and Catalogue are required');
      return;
    }

    const payload = {
      LocationCode: locationCode.trim(),
      CategoryCode: categoryCode.trim(),
      CatalogueCode: catalogueCode.trim(),
      Weight: weight,
      ToWeight: toWeight,
      SizeCode: sizeCode.trim(),
      ToSizeCode: toSizeCode.trim(),
      ReOrderLevel: reOrderLevel,
      Remarks: remarks.trim(),
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
        showSuccess(`ReOrder Level ${isEditing ? 'updated' : 'added'} successfully`);
        setIsModalVisible(false);
        fetchData();
      } else {
        showError(json.message || 'Failed to save');
      }
    } catch (error) {
      console.error('Save error:', error);
      showError('Failed to save ReOrder Level');
    } finally {
      setSaving(false);
    }
  };

  const executeDelete = async (item: any) => {
    const id = `${item.LocationCode}-${item.CategoryCode}-${item.CatalogueCode}`;
    setDeletingId(id);
    try {
      const qs = `?LocationCode=${encodeURIComponent(item.LocationCode)}&CategoryCode=${encodeURIComponent(item.CategoryCode)}&CatalogueCode=${encodeURIComponent(item.CatalogueCode)}`;
      const response = await fetch(`${API_URL}${qs}`, { method: 'DELETE' });
      const json = await response.json();
      if (json.success) {
        showSuccess('Record deleted successfully');
        fetchData();
      } else {
        showError(json.message || 'Failed to delete');
      }
    } catch (error) {
      console.error('Delete error:', error);
      showError('Failed to delete record');
    } finally {
      setDeletingId(null);
    }
  };

  const handleDelete = (item: any) => {
    if (Platform.OS === 'web') {
      if (window.confirm('Are you sure you want to delete this record?')) {
        executeDelete(item);
      }
    } else {
      Alert.alert('Confirm Delete', 'Are you sure you want to delete this record?', [
        { text: 'Cancel', style: 'cancel' },
        { text: 'Delete', style: 'destructive', onPress: () => executeDelete(item) },
      ]);
    }
  };

  const openAddModal = () => {
    setIsEditing(false);
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
                      style={[styles.input, { color: textColor, borderColor, backgroundColor: isDark ? '#2A2A2A' : '#FFF' }, isEditing && { opacity: 0.7 }]}
                      value={locationCode}
                      onChangeText={setLocationCode}
                      editable={!isEditing}
                      placeholder="Location"
                      placeholderTextColor={subtitleColor}
                    />
                    {!isEditing && (
                      <TouchableOpacity style={styles.lookupButton} onPress={() => setLookupType('location')}>
                        <ThemedText style={{ color: textColor, fontWeight: 'bold' }}>...</ThemedText>
                      </TouchableOpacity>
                    )}
                  </View>
                </View>

                {/* Category Lookup */}
                <View style={styles.inputGroup}>
                  <ThemedText style={[styles.label, { color: subtitleColor }]}>Category Code *</ThemedText>
                  <View style={styles.lookupContainer}>
                    <TextInput
                      style={[styles.input, { color: textColor, borderColor, backgroundColor: isDark ? '#2A2A2A' : '#FFF' }, isEditing && { opacity: 0.7 }]}
                      value={categoryCode}
                      onChangeText={setCategoryCode}
                      editable={!isEditing}
                      placeholder="Category"
                      placeholderTextColor={subtitleColor}
                    />
                    {!isEditing && (
                      <TouchableOpacity style={styles.lookupButton} onPress={() => setLookupType('category')}>
                        <ThemedText style={{ color: textColor, fontWeight: 'bold' }}>...</ThemedText>
                      </TouchableOpacity>
                    )}
                  </View>
                </View>

                {/* Catalogue Lookup */}
                <View style={styles.inputGroup}>
                  <ThemedText style={[styles.label, { color: subtitleColor }]}>Catalogue Code *</ThemedText>
                  <View style={styles.lookupContainer}>
                    <TextInput
                      style={[styles.input, { color: textColor, borderColor, backgroundColor: isDark ? '#2A2A2A' : '#FFF' }, isEditing && { opacity: 0.7 }]}
                      value={catalogueCode}
                      onChangeText={setCatalogueCode}
                      editable={!isEditing}
                      placeholder="Catalogue"
                      placeholderTextColor={subtitleColor}
                    />
                    {!isEditing && (
                      <TouchableOpacity style={styles.lookupButton} onPress={() => setLookupType('catalogue')}>
                        <ThemedText style={{ color: textColor, fontWeight: 'bold' }}>...</ThemedText>
                      </TouchableOpacity>
                    )}
                  </View>
                </View>

                {/* Weights */}
                <View style={styles.inputGroup}>
                  <ThemedText style={[styles.label, { color: subtitleColor }]}>From Weight</ThemedText>
                  <TextInput
                    style={[styles.input, { color: textColor, borderColor, backgroundColor: isDark ? '#2A2A2A' : '#FFF' }]}
                    value={weight}
                    onChangeText={setWeight}
                    keyboardType="numeric"
                    placeholder="0.000"
                    placeholderTextColor={subtitleColor}
                  />
                </View>
                <View style={styles.inputGroup}>
                  <ThemedText style={[styles.label, { color: subtitleColor }]}>To Weight</ThemedText>
                  <TextInput
                    style={[styles.input, { color: textColor, borderColor, backgroundColor: isDark ? '#2A2A2A' : '#FFF' }]}
                    value={toWeight}
                    onChangeText={setToWeight}
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
