import { ThemedText } from '@/components/themed-text';
import { API_BASE_URL } from '@/constants/api';
import { useThemeContext } from '@/contexts/ThemeContext';
import { useThemeColors } from '@/hooks/useThemeColors';
import { Ionicons } from '@expo/vector-icons';
import { Stack, useRouter } from 'expo-router';
import React, { useEffect, useRef, useState } from 'react';
import {
  ActivityIndicator,
  FlatList,
  Modal,
  Platform,
  ScrollView,
  StyleSheet,
  TextInput,
  TouchableOpacity,
  TouchableWithoutFeedback,
  View,
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';

const API_URL = `${API_BASE_URL}/barcode`;

export default function BarcodeScreen() {
  const themeColors = useThemeColors();
  const { accentColor } = useThemeContext();
  const { background: backgroundColor, card: cardColor, text: textColor, border: borderColor, primaryGold: GOLD_COLOR, subtitle: subtitleColor } = themeColors;
  const router = useRouter();

  // Field states matching frmBarcode.vb
  const [company, setCompany] = useState('MILLION');
  const [defaultCompany, setDefaultCompany] = useState('MILLION');
  const [barCode, setBarCode] = useState('');
  const [inventoryCode, setInventoryCode] = useState('');
  const [description, setDescription] = useState('');
  const [weight, setWeight] = useState('');
  const [category, setCategory] = useState('');
  const [initialCategory, setInitialCategory] = useState('');
  const [goldLoss, setGoldLoss] = useState('');
  const [smc, setSmc] = useState('');
  const [classCode, setClassCode] = useState('');
  const [trayId, setTrayId] = useState('');
  const [newTrayId, setNewTrayId] = useState('');
  const [noOfLabels, setNoOfLabels] = useState('1');
  const [loading, setLoading] = useState(false);
  const [printing, setPrinting] = useState(false);

  // Focus ref
  const inventoryCodeRef = useRef<TextInput>(null);
  const noOfLabelsRef = useRef<TextInput>(null);

  // Fetch company details on mount matching frmBarcode.vb Load
  useEffect(() => {
    const fetchCompany = async () => {
      try {
        const res = await fetch(`${API_URL}/company`);
        const json = await res.json();
        if (json.success && json.company) {
          setCompany(json.company);
          setDefaultCompany(json.company);
        }
      } catch (err) {
        console.warn('Failed to fetch company:', err);
      }
    };
    fetchCompany();
  }, []);

  // In-App Toast Notification
  const [toast, setToast] = useState<{ visible: boolean; message: string; type: 'success' | 'error' | 'info' }>({
    visible: false,
    message: '',
    type: 'success'
  });

  const showToast = (message: string, type: 'success' | 'error' | 'info' = 'success') => {
    setToast({ visible: true, message, type });
    setTimeout(() => {
      setToast(prev => ({ ...prev, visible: false }));
    }, 3500);
  };

  // LOV State
  const [lovType, setLovType] = useState<'inventory' | 'category' | 'tray' | null>(null);
  const [isLOVVisible, setIsLOVVisible] = useState(false);
  const [lovData, setLovData] = useState<any[]>([]);
  const [lovSearch, setLovSearch] = useState('');
  const [lovLoading, setLovLoading] = useState(false);

  // Fetch LOVs
  const fetchLOV = async (type: 'inventory' | 'category' | 'tray') => {
    setLovType(type);
    setLovLoading(true);
    setLovSearch('');
    setIsLOVVisible(true);
    setLovData([]);
    try {
      const res = await fetch(`${API_URL}/lov/${type}`);
      const json = await res.json();
      if (json.success) {
        setLovData(json.data || []);
      } else {
        showToast(json.message || `Failed to fetch ${type}`, 'error');
      }
    } catch (err) {
      console.error('LOV Error:', err);
      showToast('Network error loading LOV', 'error');
    } finally {
      setLovLoading(false);
    }
  };

  const filteredLovData = React.useMemo(() => {
    if (!lovSearch.trim()) return lovData;
    const lower = lovSearch.toLowerCase();
    return lovData.filter((item) =>
      Object.values(item).some((val) => String(val || '').toLowerCase().includes(lower))
    );
  }, [lovData, lovSearch]);

  // subGetInventory logic
  const fetchInventoryDetails = async (code: string) => {
    if (!code || !code.trim()) return;
    setLoading(true);
    try {
      const res = await fetch(`${API_URL}/inventory/${encodeURIComponent(code.trim())}`);
      const json = await res.json();
      if (json.success && json.data) {
        const item = json.data;
        setBarCode(item.InventoryCode || code.trim());
        setDescription(item.Description || '');
        setWeight(item.Weight != null ? Number(item.Weight).toFixed(2) : '');
        setGoldLoss(item.GoldLoss != null ? Number(item.GoldLoss).toFixed(2) : '0.00');
        setSmc(item.CalculatedSMC || item.SMC?.toString() || '');
        setClassCode(item.ClassCode || '');
        setTrayId(item.TrayId || '');
        setNewTrayId('');
        const cat = item.CategoryCode || '';
        setCategory(cat);
        setInitialCategory(cat);
        setTimeout(() => noOfLabelsRef.current?.focus(), 100);
      } else {
        const msg = json.message || 'InventoryCode Not Found';
        showToast(msg, 'error');
        handleClear(false);
        setTimeout(() => inventoryCodeRef.current?.focus(), 150);
      }
    } catch (err) {
      console.error(err);
      showToast('Failed to fetch inventory details', 'error');
    } finally {
      setLoading(false);
    }
  };

  // btnNew_Click
  const handleClear = (fullClear = true) => {
    if (fullClear) {
      setCompany(defaultCompany || 'MILLION');
      setInventoryCode('');
    }
    setBarCode('');
    setDescription('');
    setWeight('');
    setCategory('');
    setInitialCategory('');
    setGoldLoss('');
    setSmc('');
    setClassCode('');
    setTrayId('');
    setNewTrayId('');
    setNoOfLabels('1');
    if (fullClear) {
      setTimeout(() => inventoryCodeRef.current?.focus(), 100);
    }
  };

  // btnPrint_Click
  const handlePrint = async () => {
    // Exact validations from frmBarcode.vb
    if (!inventoryCode || !inventoryCode.trim()) {
      showToast('Please Enter InventoryCode', 'error');
      inventoryCodeRef.current?.focus();
      return;
    }
    if (!smc || smc.trim() === '') {
      showToast('Please Enter SMC', 'error');
      return;
    }
    if (!trayId && !newTrayId) {
      showToast('Please Enter Tray ID', 'error');
      return;
    }
    if (!classCode || !classCode.trim()) {
      showToast('Please Enter Class', 'error');
      return;
    }
    if (!description || !description.trim()) {
      showToast('Please Enter Description', 'error');
      return;
    }
    if (!weight || weight.trim() === '') {
      showToast('Please Enter Weight', 'error');
      return;
    }
    if (!company || !company.trim()) {
      showToast('Please Enter Company Name', 'error');
      return;
    }
    if (!barCode || !barCode.trim()) {
      showToast('Please Enter BarCode', 'error');
      return;
    }

    setPrinting(true);
    try {
      const response = await fetch(`${API_URL}/print`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          inventoryCode: inventoryCode.trim(),
          newTrayId: newTrayId.trim(),
          oldTrayId: trayId.trim(),
          categoryCode: category.trim(),
          oldCategoryCode: initialCategory.trim(),
          noOfLabels: parseInt(noOfLabels, 10) || 1,
          company: company.trim(),
          description: description.trim(),
          weight: weight.trim(),
          goldLoss: goldLoss.trim(),
          smc: smc.trim(),
          classCode: classCode.trim()
        }),
      });

      const json = await response.json();
      if (json.success) {
        showToast(json.message || 'Barcode printed successfully!', 'success');
        handleClear(true);
      } else {
        showToast(json.message || 'Error in updation.', 'error');
      }
    } catch (err: any) {
      console.error('Print Error:', err);
      showToast('Error in updation.', 'error');
    } finally {
      setPrinting(false);
    }
  };

  // Keyboard Shortcuts (F6: Clear, F9: Print, Esc: Close)
  useEffect(() => {
    if (Platform.OS === 'web') {
      const handleKeyDown = (e: KeyboardEvent) => {
        if (e.key === 'F6') {
          e.preventDefault();
          handleClear(true);
        } else if (e.key === 'F9') {
          e.preventDefault();
          handlePrint();
        } else if (e.key === 'Escape') {
          if (isLOVVisible) {
            setIsLOVVisible(false);
          } else {
            router.back();
          }
        }
      };
      window.addEventListener('keydown', handleKeyDown);
      return () => window.removeEventListener('keydown', handleKeyDown);
    }
  }, [inventoryCode, smc, trayId, newTrayId, classCode, description, weight, company, barCode, category, initialCategory, noOfLabels, isLOVVisible]);

  const renderLOVItem = ({ item }: { item: any }) => {
    if (lovType === 'inventory') {
      return (
        <TouchableOpacity
          style={styles.lovRow}
          onPress={() => {
            setInventoryCode(item.InventoryCode);
            setIsLOVVisible(false);
            fetchInventoryDetails(item.InventoryCode);
          }}
        >
          <ThemedText style={[styles.tableCellText, { width: 110, fontWeight: 'bold' }]}>
            {item.InventoryCode}
          </ThemedText>
          <ThemedText style={[styles.tableCellText, { flex: 2 }]} numberOfLines={1}>
            {item.Description || '-'}
          </ThemedText>
          <ThemedText style={[styles.tableCellText, { width: 85, textAlign: 'center' }]}>
            {item.SupplierCode || '-'}
          </ThemedText>
          <ThemedText style={[styles.tableCellText, { width: 75, textAlign: 'right', fontWeight: 'bold', color: '#8D6409' }]}>
            {item.Weight != null ? Number(item.Weight).toFixed(2) : '-'}
          </ThemedText>
        </TouchableOpacity>
      );
    } else if (lovType === 'category') {
      return (
        <TouchableOpacity
          style={styles.lovItem}
          onPress={() => {
            setCategory(item.CategoryCode);
            setIsLOVVisible(false);
          }}
        >
          <ThemedText style={styles.lovItemTitle}>{item.CategoryCode}</ThemedText>
          <ThemedText style={styles.lovItemSub}>{item.CategoryName}</ThemedText>
        </TouchableOpacity>
      );
    } else {
      return (
        <TouchableOpacity
          style={styles.lovItem}
          onPress={() => {
            setNewTrayId(item.TrayId);
            setIsLOVVisible(false);
          }}
        >
          <ThemedText style={styles.lovItemTitle}>{item.TrayId}</ThemedText>
          <ThemedText style={styles.lovItemSub}>{item.TrayName}</ThemedText>
        </TouchableOpacity>
      );
    }
  };

  return (
    <SafeAreaView style={[styles.container, { backgroundColor: '#FBF9F5' }]} edges={['top', 'bottom', 'left', 'right']}>
      <Stack.Screen options={{ headerShown: false }} />

      {/* Floating In-App Toast Banner */}
      {toast.visible && (
        <View style={[
          styles.toastBanner,
          toast.type === 'error' ? styles.toastError : styles.toastSuccess
        ]}>
          <Ionicons
            name={toast.type === 'error' ? 'alert-circle' : 'checkmark-circle'}
            size={18}
            color="#FFFFFF"
            style={{ marginRight: 8 }}
          />
          <ThemedText style={styles.toastText}>{toast.message}</ThemedText>
        </View>
      )}

      {/* Custom Header */}
      <View style={styles.header}>
        <View style={{ flexDirection: 'row', alignItems: 'center' }}>
          <TouchableOpacity style={styles.backButton} onPress={() => router.back()}>
            <Ionicons name="arrow-back" size={22} color="#3B2D17" />
          </TouchableOpacity>
          <ThemedText style={styles.title}>Barcode</ThemedText>
        </View>
        <View style={styles.toolbarBtns}>
          <TouchableOpacity style={styles.toolbarBtn} onPress={() => handleClear(true)}>
            <ThemedText style={styles.toolbarBtnText}>Clear</ThemedText>
          </TouchableOpacity>
          <TouchableOpacity
            style={[styles.toolbarBtn, { backgroundColor: '#F9F2E2', borderColor: '#B8903B' }]}
            onPress={handlePrint}
            disabled={printing}
          >
            <ThemedText style={[styles.toolbarBtnText, { color: '#9E6D08' }]}>
              {printing ? "Printing..." : "Print"}
            </ThemedText>
          </TouchableOpacity>
        </View>
      </View>

      <ScrollView contentContainerStyle={styles.scrollContent}>
        {loading && <ActivityIndicator size="large" color="#9E6D08" style={styles.loader} />}

        {/* Luxury Gold Card */}
        <View style={styles.goldCard}>
          {/* Card Header Banner */}
          <View style={styles.cardHeaderBanner}>
            <View style={styles.headerIconBadge}>
              <Ionicons name="barcode-outline" size={15} color="#B48218" />
            </View>
            <ThemedText style={styles.cardHeaderTitle}>Barcode Generation Details</ThemedText>
          </View>

          <View style={styles.cardBody}>
            {/* Company */}
            <View style={styles.formRow}>
              <ThemedText style={styles.label}>Company</ThemedText>
              <TextInput
                style={styles.inputDisabled}
                value={company}
                editable={false}
              />
            </View>

            {/* BarCode */}
            <View style={styles.formRow}>
              <ThemedText style={styles.label}>BarCode</ThemedText>
              <TextInput
                style={styles.inputDisabled}
                value={barCode}
                editable={false}
              />
            </View>

            {/* Inventory Code */}
            <View style={styles.formRow}>
              <ThemedText style={styles.label}>Inventory Code</ThemedText>
              <View style={styles.rowInputGroup}>
                <TextInput
                  ref={inventoryCodeRef}
                  style={[styles.input, { borderTopRightRadius: 0, borderBottomRightRadius: 0, borderRightWidth: 0 }]}
                  value={inventoryCode}
                  onChangeText={setInventoryCode}
                  onBlur={() => fetchInventoryDetails(inventoryCode)}
                  onSubmitEditing={() => fetchInventoryDetails(inventoryCode)}
                  autoCapitalize="characters"
                  placeholder="Enter or search Inventory Code"
                />
                <TouchableOpacity style={styles.searchBtn} onPress={() => fetchLOV('inventory')}>
                  <ThemedText style={styles.searchBtnText}>...</ThemedText>
                </TouchableOpacity>
              </View>
            </View>

            {/* Description */}
            <View style={styles.formRow}>
              <ThemedText style={styles.label}>Description</ThemedText>
              <TextInput
                style={styles.inputDisabled}
                value={description}
                editable={false}
              />
            </View>

            {/* Weight & Category Row */}
            <View style={styles.formRowMulti}>
              <View style={styles.flexRow}>
                <ThemedText style={styles.label}>Weight</ThemedText>
                <TextInput
                  style={[styles.inputDisabled, { textAlign: 'right' }]}
                  value={weight}
                  editable={false}
                />
              </View>
              <View style={styles.flexRow}>
                <ThemedText style={styles.labelMulti}>Category</ThemedText>
                <View style={styles.rowInputGroup}>
                  <TextInput
                    style={[styles.input, { borderTopRightRadius: 0, borderBottomRightRadius: 0, borderRightWidth: 0 }]}
                    value={category}
                    onChangeText={setCategory}
                    autoCapitalize="characters"
                  />
                  <TouchableOpacity style={styles.searchBtn} onPress={() => fetchLOV('category')}>
                    <ThemedText style={styles.searchBtnText}>...</ThemedText>
                  </TouchableOpacity>
                </View>
              </View>
            </View>

            {/* Gold Loss */}
            <View style={styles.formRow}>
              <ThemedText style={styles.label}>Gold Loss</ThemedText>
              <TextInput
                style={[styles.inputDisabled, { textAlign: 'right' }]}
                value={goldLoss}
                editable={false}
              />
            </View>

            {/* SMC */}
            <View style={styles.formRow}>
              <ThemedText style={styles.label}>SMC</ThemedText>
              <TextInput
                style={[styles.inputDisabled, { textAlign: 'right' }]}
                value={smc}
                editable={false}
              />
            </View>

            {/* Class */}
            <View style={styles.formRow}>
              <ThemedText style={styles.label}>Class</ThemedText>
              <TextInput
                style={styles.inputDisabled}
                value={classCode}
                editable={false}
              />
            </View>

            {/* Tray ID */}
            <View style={styles.formRow}>
              <ThemedText style={styles.label}>Tray ID</ThemedText>
              <View style={styles.rowInputGroup}>
                <TextInput
                  style={[styles.inputDisabled, { flex: 1, marginRight: 8 }]}
                  value={trayId}
                  editable={false}
                />
                <TextInput
                  style={[styles.input, { flex: 1, borderTopRightRadius: 0, borderBottomRightRadius: 0, borderRightWidth: 0 }]}
                  value={newTrayId}
                  onChangeText={setNewTrayId}
                  placeholder="New Tray"
                  placeholderTextColor="#998973"
                />
                <TouchableOpacity style={styles.searchBtn} onPress={() => fetchLOV('tray')}>
                  <ThemedText style={styles.searchBtnText}>...</ThemedText>
                </TouchableOpacity>
              </View>
            </View>

            {/* No of Labels */}
            <View style={styles.formRow}>
              <ThemedText style={styles.label}>No of Labels</ThemedText>
              <TextInput
                ref={noOfLabelsRef}
                style={[styles.input, { maxWidth: 120, flex: 0, width: 120 }]}
                value={noOfLabels}
                onChangeText={setNoOfLabels}
                keyboardType="numeric"
              />
            </View>
          </View>
        </View>
      </ScrollView>

      {/* LOV Modal */}
      <Modal visible={isLOVVisible} animationType="slide" transparent={true}>
        <TouchableWithoutFeedback onPress={() => setIsLOVVisible(false)}>
          <View style={styles.modalOverlay}>
            <TouchableWithoutFeedback>
              <View style={[styles.modalContainer, { backgroundColor: '#FFFFFF', borderColor: '#E8DCB8' }]}>
                <View style={styles.modalHeader}>
                  <ThemedText style={styles.modalTitle}>Select {lovType}</ThemedText>
                  <TouchableOpacity onPress={() => setIsLOVVisible(false)}>
                    <Ionicons name="close" size={20} color="#3B2D17" />
                  </TouchableOpacity>
                </View>

                {/* Filter Search Input */}
                <View style={styles.modalSearchBox}>
                  <Ionicons name="search" size={15} color="#8D6409" />
                  <TextInput
                    style={styles.modalSearchInput}
                    value={lovSearch}
                    onChangeText={setLovSearch}
                    placeholder={`Search ${lovType || 'items'}...`}
                    placeholderTextColor="#A99C89"
                    autoFocus
                  />
                  {lovSearch ? (
                    <TouchableOpacity onPress={() => setLovSearch('')}>
                      <Ionicons name="close-circle" size={16} color="#999" />
                    </TouchableOpacity>
                  ) : null}
                </View>

                {/* Inventory Table Header */}
                {lovType === 'inventory' && (
                  <View style={styles.tableHeadRow}>
                    <ThemedText style={[styles.tableHeadText, { width: 110 }]}>InventoryCode</ThemedText>
                    <ThemedText style={[styles.tableHeadText, { flex: 2 }]}>Description</ThemedText>
                    <ThemedText style={[styles.tableHeadText, { width: 85, textAlign: 'center' }]}>SupplierCode</ThemedText>
                    <ThemedText style={[styles.tableHeadText, { width: 75, textAlign: 'right' }]}>Weight</ThemedText>
                  </View>
                )}

                {lovLoading ? (
                  <ActivityIndicator size="large" color="#9E6D08" style={{ padding: 20 }} />
                ) : filteredLovData.length === 0 ? (
                  <ThemedText style={{ padding: 20, textAlign: 'center', color: '#8F7E66' }}>No data found</ThemedText>
                ) : (
                  <FlatList
                    data={filteredLovData}
                    keyExtractor={(item, index) => index.toString()}
                    renderItem={renderLOVItem}
                    style={styles.lovList}
                    keyboardShouldPersistTaps="handled"
                  />
                )}
              </View>
            </TouchableWithoutFeedback>
          </View>
        </TouchableWithoutFeedback>
      </Modal>
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  container: { flex: 1, backgroundColor: '#FBF9F5' },
  header: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingHorizontal: 16,
    paddingVertical: 10,
    backgroundColor: '#FFFFFF',
    borderBottomWidth: 1,
    borderBottomColor: '#EBE2D2',
  },
  backButton: {
    padding: 4,
    marginRight: 12,
  },
  title: {
    fontSize: 20,
    fontWeight: '500',
    color: '#3B2D17',
  },
  toolbarBtns: {
    flexDirection: 'row',
    gap: 8,
  },
  toolbarBtn: {
    paddingVertical: 6,
    paddingHorizontal: 14,
    borderRadius: 5,
    borderWidth: 1,
    borderColor: '#DFD5C3',
    backgroundColor: '#FFFFFF',
  },
  toolbarBtnText: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#5C4A28',
  },
  scrollContent: {
    padding: 16,
    alignItems: 'center',
    flexGrow: 1,
    paddingBottom: 60,
  },
  loader: { marginVertical: 10 },

  // Toast Banner
  toastBanner: {
    position: 'absolute',
    top: 60,
    alignSelf: 'center',
    zIndex: 9999,
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 18,
    paddingVertical: 10,
    borderRadius: 24,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.25,
    shadowRadius: 8,
    elevation: 8,
  },
  toastError: {
    backgroundColor: '#C53030',
  },
  toastSuccess: {
    backgroundColor: '#276749',
  },
  toastText: {
    color: '#FFFFFF',
    fontSize: 13,
    fontWeight: 'bold',
  },

  // Luxury Gold Card
  goldCard: {
    width: '100%',
    maxWidth: 620,
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    overflow: 'hidden',
    shadowColor: '#A38035',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.08,
    shadowRadius: 6,
    elevation: 2,
    marginTop: 8,
  },
  cardHeaderBanner: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#F5E7CA',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    paddingVertical: 10,
    paddingHorizontal: 14,
  },
  headerIconBadge: {
    width: 24,
    height: 24,
    borderRadius: 12,
    backgroundColor: '#FFF8EA',
    borderWidth: 1,
    borderColor: '#E2D2AF',
    justifyContent: 'center',
    alignItems: 'center',
    marginRight: 8,
  },
  cardHeaderTitle: {
    fontSize: 13,
    fontWeight: 'bold',
    color: '#3B2D17',
    letterSpacing: 0.3,
  },
  cardBody: {
    padding: 16,
    gap: 10,
  },
  formRow: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  formRowMulti: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 16,
  },
  flexRow: {
    flexDirection: 'row',
    alignItems: 'center',
    flex: 1,
  },
  label: {
    width: 125,
    fontSize: 12,
    fontWeight: 'bold',
    color: '#3A2E19',
    flexShrink: 0,
  },
  labelMulti: {
    width: 70,
    fontSize: 12,
    fontWeight: 'bold',
    color: '#3A2E19',
    flexShrink: 0,
  },
  input: {
    flex: 1,
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderRadius: 6,
    paddingHorizontal: 10,
    paddingVertical: 4,
    fontSize: 12,
    height: 32,
    backgroundColor: '#FFFFFF',
    color: '#2C2519',
  },
  inputDisabled: {
    flex: 1,
    borderWidth: 1,
    borderColor: '#EADBCA',
    borderRadius: 6,
    paddingHorizontal: 10,
    paddingVertical: 4,
    fontSize: 12,
    height: 32,
    backgroundColor: '#F8F5EE',
    color: '#554736',
  },
  rowInputGroup: {
    flex: 1,
    flexDirection: 'row',
    alignItems: 'center',
  },
  searchBtn: {
    width: 32,
    height: 32,
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderLeftWidth: 1,
    justifyContent: 'center',
    alignItems: 'center',
    borderTopRightRadius: 6,
    borderBottomRightRadius: 6,
    backgroundColor: '#FAF5EA',
  },
  searchBtnText: {
    color: '#6E5528',
    fontWeight: 'bold',
    fontSize: 12,
  },

  // Modal
  modalOverlay: {
    flex: 1,
    backgroundColor: 'rgba(0,0,0,0.5)',
    justifyContent: 'center',
    alignItems: 'center',
    padding: 16,
  },
  modalContainer: {
    width: '100%',
    maxWidth: 580,
    maxHeight: '80%',
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#E8DCB8',
    borderRadius: 8,
    overflow: 'hidden',
  },
  modalHeader: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    paddingHorizontal: 14,
    paddingVertical: 10,
    backgroundColor: '#F5E7CA',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
  },
  modalTitle: {
    fontSize: 15,
    fontWeight: 'bold',
    color: '#3B2D17',
    textTransform: 'capitalize',
  },
  modalSearchBox: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 10,
    paddingVertical: 6,
    backgroundColor: '#FAF7F0',
    borderBottomWidth: 1,
    borderBottomColor: '#EADBCA',
    gap: 6,
  },
  modalSearchInput: {
    flex: 1,
    fontSize: 12,
    color: '#2C2519',
    height: 28,
    paddingHorizontal: 8,
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderRadius: 5,
  },
  tableHeadRow: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#F8F3E8',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    paddingVertical: 6,
    paddingHorizontal: 10,
  },
  tableHeadText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#5C4A28',
  },
  lovList: {
    maxHeight: 320,
  },
  lovRow: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingVertical: 8,
    paddingHorizontal: 10,
    borderBottomWidth: 1,
    borderBottomColor: '#F0E6D2',
  },
  tableCellText: {
    fontSize: 12,
    color: '#2C2519',
  },
  lovItem: {
    paddingVertical: 10,
    paddingHorizontal: 14,
    borderBottomWidth: 1,
    borderBottomColor: '#F0E6D2',
  },
  lovItemTitle: {
    fontSize: 13,
    fontWeight: 'bold',
    color: '#2C2519',
  },
  lovItemSub: {
    fontSize: 12,
    color: '#6E5D46',
    marginTop: 2,
  },
});
