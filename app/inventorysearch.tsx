import { ThemedText } from '@/components/themed-text';
import { API_BASE_URL } from '@/constants/api';
import { useThemeContext } from '@/contexts/ThemeContext';
import { useThemeColors } from '@/hooks/useThemeColors';
import { Ionicons } from '@expo/vector-icons';
import { Stack, useRouter } from 'expo-router';
import React, { useState } from 'react';
import {
  ActivityIndicator,
  Alert,
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

const API_SEARCH_URL = `${API_BASE_URL}/catalogue/search`;
const API_DETAIL_URL = `${API_BASE_URL}/inventorysearch`;

export default function InventorySearchScreen() {
  const themeColors = useThemeColors();
  const { accentColor } = useThemeContext();
  const router = useRouter();

  // Search filter fields matching VB6 frmInventorySearch
  const [category, setCategory] = useState('');
  const [catalogue, setCatalogue] = useState('');
  const [fromWeight, setFromWeight] = useState('');
  const [toWeight, setToWeight] = useState('');
  const [inventoryCode, setInventoryCode] = useState('');

  const [data, setData] = useState<any[]>([]);
  const [loading, setLoading] = useState(false);
  const [toast, setToast] = useState<{ message: string; type: 'success' | 'error' | 'info' } | null>(null);

  const [selectedInventory, setSelectedInventory] = useState<any | null>(null);
  const [detailLoading, setDetailLoading] = useState(false);

  // LOV State
  const [isLOVVisible, setIsLOVVisible] = useState(false);
  const [lovType, setLovType] = useState<'category' | 'catalogue' | null>(null);
  const [lovData, setLovData] = useState<any[]>([]);
  const [lovLoading, setLovLoading] = useState(false);

  // Option selection ('split' | 'merge' | 'addl' | null)
  const [splitMergeType, setSplitMergeType] = useState<'split' | 'merge'>('split');
  const [splitHistory, setSplitHistory] = useState<any | null>(null);
  const [mergeHistory, setMergeHistory] = useState<any | null>(null);
  const [addlDescData, setAddlDescData] = useState<any | null>(null);
  const [isSplitMergeModalVisible, setIsSplitMergeModalVisible] = useState(false);
  const [isAddlModalVisible, setIsAddlModalVisible] = useState(false);

  const showToast = (message: string, type: 'success' | 'error' | 'info' = 'info') => {
    setToast({ message, type });
    setTimeout(() => {
      setToast(null);
    }, 3000);
  };

  // Fetch Inventory Details according to VB6 FetchInventoryDetails()
  const fetchInventoryList = async () => {
    setLoading(true);
    setSelectedInventory(null);
    try {
      let queryUrl = `${API_SEARCH_URL}?`;
      const params = new URLSearchParams();

      if (category.trim()) params.append('category', category.trim());
      if (catalogue.trim()) params.append('catalogue', catalogue.trim());
      if (fromWeight.trim() && toWeight.trim()) {
        const fw = parseFloat(fromWeight.trim());
        const tw = parseFloat(toWeight.trim());
        if (!isNaN(fw) && !isNaN(tw) && fw > 0 && tw > 0) {
          params.append('fromWeight', fw.toString());
          params.append('toWeight', tw.toString());
        }
      }
      if (inventoryCode.trim()) params.append('inventoryCode', inventoryCode.trim());

      const res = await fetch(queryUrl + params.toString());
      const json = await res.json();
      if (json.success || json.data) {
        const records = json.data || [];
        if (records.length > 0) {
          setData(records);
          showToast(`Found ${records.length} item(s)`, 'success');
        } else {
          setData([]);
          const msg = 'Stock Not Available';
          Platform.OS === 'web' ? window.alert(msg) : Alert.alert('Notice', msg);
          showToast(msg, 'info');
        }
      } else {
        setData([]);
        const msg = json.error || json.message || 'Stock Not Available';
        Platform.OS === 'web' ? window.alert(msg) : Alert.alert('Error', msg);
        showToast(msg, 'error');
      }
    } catch (err) {
      console.error('Fetch Inventory Error:', err);
      const msg = 'Failed to fetch inventory details';
      Platform.OS === 'web' ? window.alert(msg) : Alert.alert('Error', msg);
      showToast(msg, 'error');
    } finally {
      setLoading(false);
    }
  };

  const fetchInventoryDetails = async (invCode: string) => {
    setDetailLoading(true);
    try {
      const res = await fetch(`${API_DETAIL_URL}/${encodeURIComponent(invCode.trim())}`);
      const json = await res.json();
      if (json.Inventory) {
        setSelectedInventory(json.Inventory);
        setSplitHistory(json.SplitHistory || null);
        setMergeHistory(json.MergeHistory || null);
        setAddlDescData(json.AdditionalDesc || null);
      } else {
        const msg = json.error || 'Inventory detail not found';
        Platform.OS === 'web' ? window.alert(msg) : Alert.alert('Notice', msg);
        showToast(msg, 'info');
      }
    } catch (err) {
      console.error(err);
      const msg = 'Failed to fetch inventory details';
      Platform.OS === 'web' ? window.alert(msg) : Alert.alert('Error', msg);
      showToast(msg, 'error');
    } finally {
      setDetailLoading(false);
    }
  };

  // Clear function matching VB6 btnClear_Click
  const handleClear = () => {
    setCategory('');
    setCatalogue('');
    setFromWeight('');
    setToWeight('');
    setInventoryCode('');
    setData([]);
    setSelectedInventory(null);
    setSplitHistory(null);
    setMergeHistory(null);
    setAddlDescData(null);
  };

  // Print function matching VB6 btnPrint_Click / subPrint
  const handlePrint = () => {
    if (data.length === 0) {
      const msg = 'No inventory data to print. Please fetch records first.';
      Platform.OS === 'web' ? window.alert(msg) : Alert.alert('Notice', msg);
      return;
    }
    if (Platform.OS === 'web' && typeof window !== 'undefined') {
      window.print();
    } else {
      Alert.alert('Print', `Printing ${data.length} inventory records...`);
    }
  };

  // LOV Search matching VB6 btnCatagorySearch_Click and btnCatalogueSearch_Click
  const fetchLOV = async (type: 'category' | 'catalogue') => {
    setLovType(type);
    setLovLoading(true);
    setIsLOVVisible(true);
    setLovData([]);
    try {
      let url = `${API_BASE_URL}/catalogue/lov/${type}`;
      if (type === 'catalogue' && category.trim()) {
        url += `?category=${encodeURIComponent(category.trim())}`;
      }
      const res = await fetch(url);
      const json = await res.json();
      if (json.success && json.data) {
        setLovData(json.data);
      } else if (json.data) {
        setLovData(json.data);
      } else {
        setLovData([]);
      }
    } catch (err) {
      console.error('LOV Error:', err);
      setLovData([]);
      showToast(`Error fetching ${type} list`, 'error');
    } finally {
      setLovLoading(false);
    }
  };

  const renderLOVItem = ({ item }: { item: any }) => {
    if (lovType === 'category') {
      return (
        <TouchableOpacity
          style={styles.lovItem}
          onPress={() => {
            setCategory(item.CategoryCode ? item.CategoryCode.trim() : '');
            setIsLOVVisible(false);
          }}
        >
          <ThemedText style={styles.lovItemTitle}>{item.CategoryCode}</ThemedText>
          <ThemedText style={styles.lovItemSub}>{item.CategoryName || ''}</ThemedText>
        </TouchableOpacity>
      );
    } else {
      return (
        <TouchableOpacity
          style={styles.lovItem}
          onPress={() => {
            setCatalogue(item.CatalogueCode ? item.CatalogueCode.trim() : '');
            setIsLOVVisible(false);
          }}
        >
          <ThemedText style={styles.lovItemTitle}>{item.CatalogueCode}</ThemedText>
          <ThemedText style={styles.lovItemSub}>{item.CatalogueName || ''}</ThemedText>
        </TouchableOpacity>
      );
    }
  };

  // Grid Header matching VB6 grdCatalogue columns: LocationCode, InventoryCode, Description, Weight, SizeCode, SizeType, Status
  const renderHeader = () => (
    <View style={styles.tableHeaderRow}>
      <ThemedText style={[styles.headerCell, { width: 90 }]}>LocationCode</ThemedText>
      <ThemedText style={[styles.headerCell, { width: 110 }]}>InventoryCode</ThemedText>
      <ThemedText style={[styles.headerCell, { flex: 1, minWidth: 150 }]}>Description</ThemedText>
      <ThemedText style={[styles.headerCell, { width: 80, textAlign: 'right' }]}>Weight</ThemedText>
      <ThemedText style={[styles.headerCell, { width: 75, textAlign: 'center' }]}>SizeCode</ThemedText>
      <ThemedText style={[styles.headerCell, { width: 75, textAlign: 'center' }]}>SizeType</ThemedText>
      <ThemedText style={[styles.headerCell, { width: 70, textAlign: 'center', borderRightWidth: 0 }]}>Status</ThemedText>
    </View>
  );

  const renderRow = ({ item, index }: { item: any; index: number }) => {
    const isSelected = selectedInventory && selectedInventory.InventoryCode === item.InventoryCode;
    const isStatusIn = item.Status === 'IN';

    return (
      <TouchableOpacity
        style={[
          styles.tableRow,
          index % 2 === 1 && styles.tableRowAlt,
          isSelected && styles.tableRowSelected,
        ]}
        onPress={() => fetchInventoryDetails(item.InventoryCode)}
      >
        <ThemedText style={[styles.cell, { width: 90 }, isSelected && styles.cellSelected]} numberOfLines={1}>
          {item.Locationcode || item.LocationCode || '-'}
        </ThemedText>
        <ThemedText style={[styles.cell, { width: 110, fontWeight: 'bold' }, isSelected && styles.cellSelected]} numberOfLines={1}>
          {item.InventoryCode || '-'}
        </ThemedText>
        <ThemedText style={[styles.cell, { flex: 1, minWidth: 150 }, isSelected && styles.cellSelected]} numberOfLines={1}>
          {item.Description || '-'}
        </ThemedText>
        <ThemedText style={[styles.cell, { width: 80, textAlign: 'right', fontWeight: 'bold' }, isSelected && styles.cellSelected]} numberOfLines={1}>
          {item.Weight !== undefined && item.Weight !== null ? Number(item.Weight).toFixed(3) : '0.000'}
        </ThemedText>
        <ThemedText style={[styles.cell, { width: 75, textAlign: 'center' }, isSelected && styles.cellSelected]} numberOfLines={1}>
          {item.SizeCode || '-'}
        </ThemedText>
        <ThemedText style={[styles.cell, { width: 75, textAlign: 'center' }, isSelected && styles.cellSelected]} numberOfLines={1}>
          {item.SizeType || '-'}
        </ThemedText>
        <View style={[{ width: 70, justifyContent: 'center', alignItems: 'center' }]}>
          <View style={[styles.statusBadge, isStatusIn ? styles.statusBadgeIn : styles.statusBadgeOther]}>
            <ThemedText style={[styles.statusBadgeText, isStatusIn ? styles.statusBadgeTextIn : styles.statusBadgeTextOther]}>
              {item.Status || 'OUT'}
            </ThemedText>
          </View>
        </View>
      </TouchableOpacity>
    );
  };

  const renderField = (label: string, value: string | number | undefined, width?: number, inline?: boolean) => (
    <View style={[styles.detailFieldContainer, inline && { flexDirection: 'row', alignItems: 'center', marginBottom: 6 }]}>
      <ThemedText style={[styles.detailLabel, inline && { width: 95, marginBottom: 0 }]}>{label}</ThemedText>
      <TextInput
        style={[styles.detailInput, width ? { width } : {}]}
        value={value !== undefined && value !== null ? String(value) : ''}
        editable={false}
      />
    </View>
  );

  return (
    <SafeAreaView style={styles.safeArea} edges={['top']}>
      <Stack.Screen options={{ headerShown: false }} />

      {/* Toast Alert */}
      {toast && (
        <View
          style={[
            styles.toast,
            {
              backgroundColor:
                toast.type === 'success' ? '#2E7D32' : toast.type === 'error' ? '#D32F2F' : '#8D6511',
            },
          ]}
        >
          <ThemedText style={styles.toastText}>{toast.message}</ThemedText>
        </View>
      )}

      {/* Top App Header Bar */}
      <View style={styles.header}>
        <View style={{ flexDirection: 'row', alignItems: 'center' }}>
          <TouchableOpacity style={styles.backButton} onPress={() => router.back()}>
            <Ionicons name="arrow-back" size={22} color="#3B2D17" />
          </TouchableOpacity>
          <ThemedText style={styles.title}>Inventory Search</ThemedText>
        </View>
        <View style={{ flexDirection: 'row', gap: 8, alignItems: 'center' }}>
          <TouchableOpacity style={styles.printBtn} onPress={handlePrint}>
            <Ionicons name="print-outline" size={14} color="#5C4A28" style={{ marginRight: 4 }} />
            <ThemedText style={styles.clearBtnText}>Print</ThemedText>
          </TouchableOpacity>
          <TouchableOpacity style={styles.clearBtn} onPress={handleClear}>
            <Ionicons name="refresh-outline" size={14} color="#5C4A28" style={{ marginRight: 4 }} />
            <ThemedText style={styles.clearBtnText}>Clear</ThemedText>
          </TouchableOpacity>
          <TouchableOpacity style={styles.searchBtn} onPress={fetchInventoryList}>
            {loading ? (
              <ActivityIndicator size="small" color="#9E6D08" style={{ marginRight: 6 }} />
            ) : (
              <Ionicons name="search" size={14} color="#9E6D08" style={{ marginRight: 4 }} />
            )}
            <ThemedText style={styles.searchBtnText}>Fetch</ThemedText>
          </TouchableOpacity>
        </View>
      </View>

      <View style={styles.container}>
        {/* Top Search Filter Card (matching VB6 grpSearch: Category, Catalogue, FromWeight, ToWeight, InventoryCode, Fetch) */}
        <View style={styles.searchCard}>
          <View style={styles.cardHeaderBanner}>
            <View style={styles.headerIconBadge}>
              <Ionicons name="filter" size={13} color="#B48218" />
            </View>
            <ThemedText style={styles.cardHeaderTitle}>Search Filters</ThemedText>
          </View>

          <View style={styles.searchFilterBody}>
            <View style={styles.searchRow}>
              {/* Category Filter (txtCategory, btnCatagorySearch) */}
              <View style={styles.fieldGroup}>
                <ThemedText style={styles.label}>Category</ThemedText>
                <View style={styles.inputWithBtn}>
                  <TextInput
                    style={[styles.input, { width: 100, borderTopRightRadius: 0, borderBottomRightRadius: 0 }]}
                    value={category}
                    onChangeText={setCategory}
                    autoCapitalize="characters"
                    placeholder="All"
                    placeholderTextColor="#A89E8D"
                  />
                  <TouchableOpacity style={styles.lovBtn} onPress={() => fetchLOV('category')}>
                    <ThemedText style={styles.lovBtnText}>...</ThemedText>
                  </TouchableOpacity>
                </View>
              </View>

              {/* Catalogue Filter (txtCatalogue, btnCatalogueSearch) */}
              <View style={styles.fieldGroup}>
                <ThemedText style={styles.label}>Catalogue</ThemedText>
                <View style={styles.inputWithBtn}>
                  <TextInput
                    style={[styles.input, { width: 115, borderTopRightRadius: 0, borderBottomRightRadius: 0 }]}
                    value={catalogue}
                    onChangeText={setCatalogue}
                    autoCapitalize="characters"
                    placeholder="All"
                    placeholderTextColor="#A89E8D"
                  />
                  <TouchableOpacity style={styles.lovBtn} onPress={() => fetchLOV('catalogue')}>
                    <ThemedText style={styles.lovBtnText}>...</ThemedText>
                  </TouchableOpacity>
                </View>
              </View>

              {/* From Weight Filter (txtFromWeight) */}
              <View style={styles.fieldGroup}>
                <ThemedText style={styles.label}>From Weight</ThemedText>
                <TextInput
                  style={[styles.input, { width: 75, textAlign: 'right' }]}
                  value={fromWeight}
                  onChangeText={setFromWeight}
                  keyboardType="numeric"
                  placeholder="0.00"
                  placeholderTextColor="#A89E8D"
                />
              </View>

              {/* To Weight Filter (txtToWeight) */}
              <View style={styles.fieldGroup}>
                <ThemedText style={styles.label}>To Weight</ThemedText>
                <TextInput
                  style={[styles.input, { width: 75, textAlign: 'right' }]}
                  value={toWeight}
                  onChangeText={setToWeight}
                  keyboardType="numeric"
                  placeholder="0.00"
                  placeholderTextColor="#A89E8D"
                />
              </View>

              {/* Inventory Code Filter (txtInventoryCode) */}
              <View style={styles.fieldGroup}>
                <ThemedText style={styles.label}>Inventory Code</ThemedText>
                <TextInput
                  style={[styles.input, { width: 125 }]}
                  value={inventoryCode}
                  onChangeText={setInventoryCode}
                  autoCapitalize="characters"
                  placeholder="Scan / Type"
                  placeholderTextColor="#A89E8D"
                />
              </View>
            </View>
          </View>
        </View>

        {/* Main Content Split View */}
        <View style={styles.mainContent}>
          {/* Left Pane: Search Results Grid Card (grdCatalogue) */}
          <View style={styles.leftCard}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerIconBadge}>
                <Ionicons name="list" size={13} color="#B48218" />
              </View>
              <ThemedText style={styles.cardHeaderTitle}>Catalogue Stock Items ({data.length})</ThemedText>
            </View>

            {loading && (
              <View style={styles.loadingContainer}>
                <ActivityIndicator size="small" color="#B48218" />
                <ThemedText style={styles.loadingText}>Searching inventory...</ThemedText>
              </View>
            )}

            <ScrollView horizontal showsHorizontalScrollIndicator={true} contentContainerStyle={{ minWidth: '100%' }}>
              <View style={{ flex: 1, minWidth: 680 }}>
                {renderHeader()}

                <FlatList
                  data={data}
                  keyExtractor={(item, index) => `${item.InventoryCode || index}-${index}`}
                  renderItem={renderRow}
                  contentContainerStyle={{ flexGrow: 1 }}
                  ListEmptyComponent={
                    !loading ? (
                      <View style={styles.emptyContainer}>
                        <Ionicons name="file-tray-outline" size={28} color="#C4B79D" />
                        <ThemedText style={styles.emptyText}>No inventory records to display</ThemedText>
                      </View>
                    ) : null
                  }
                />
              </View>
            </ScrollView>
          </View>

          {/* Right Pane: Detail View Card */}
          <View style={styles.rightCard}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerIconBadge}>
                <Ionicons name="information-circle" size={13} color="#B48218" />
              </View>
              <ThemedText style={styles.cardHeaderTitle}>
                {selectedInventory ? `Inventory Details: ${selectedInventory.InventoryCode || ''}` : 'Inventory Details'}
              </ThemedText>
            </View>

            {detailLoading && (
              <View style={styles.loadingContainer}>
                <ActivityIndicator size="large" color="#B48218" />
                <ThemedText style={styles.loadingText}>Loading details...</ThemedText>
              </View>
            )}

            {!detailLoading && selectedInventory && (
              <ScrollView contentContainerStyle={styles.detailsContainer}>
                {/* General Info Section */}
                <View style={styles.subCard}>
                  <View style={styles.subCardHeader}>
                    <Ionicons name="document-text-outline" size={14} color="#8D6511" style={{ marginRight: 6 }} />
                    <ThemedText style={styles.subCardTitle}>General Information</ThemedText>
                  </View>
                  <View style={styles.rowWrap}>
                    {renderField('Inventory Code', selectedInventory.InventoryCode, 130, true)}
                    {renderField('Description', selectedInventory.Description, 220, true)}
                    {renderField('Diamond Desc', selectedInventory.DiamondDescription, 220, true)}
                    {renderField('Category', selectedInventory.CategoryCode, 130, true)}
                    {renderField('Catalogue', selectedInventory.CatalogueCode, 130, true)}
                    {renderField('Class', selectedInventory.ClassCode, 130, true)}
                    {renderField('Location', selectedInventory.LocationCode || selectedInventory.Locationcode, 130, true)}
                    {renderField('Created By', selectedInventory.CreateUser, 130, true)}
                    {renderField('Remarks', selectedInventory.Remarks, 220, true)}
                  </View>
                </View>

                {/* Weights & Costs Section */}
                <View style={styles.subCard}>
                  <View style={styles.subCardHeader}>
                    <Ionicons name="scale-outline" size={14} color="#8D6511" style={{ marginRight: 6 }} />
                    <ThemedText style={styles.subCardTitle}>Weights & Costs</ThemedText>
                  </View>
                  <View style={styles.row}>
                    {renderField('Weight', selectedInventory.Weight !== undefined ? Number(selectedInventory.Weight).toFixed(3) : '', 90)}
                    {renderField('Stone Weight', selectedInventory.StoneWeight !== undefined ? Number(selectedInventory.StoneWeight).toFixed(3) : '', 90)}
                    {renderField('Net Wt', ((selectedInventory.Weight || 0) - (selectedInventory.StoneWeight || 0)).toFixed(3), 90)}
                    {renderField('Gold Loss', selectedInventory.GoldLoss, 90)}
                    {renderField('Size', selectedInventory.SizeCode, 70)}
                  </View>
                  <View style={[styles.row, { marginTop: 6 }]}>
                    {renderField('PMC', selectedInventory.PMC, 100)}
                    {renderField('SMC', selectedInventory.SMC, 100)}
                    {renderField('Qty On Hand', selectedInventory.QtyOnHand, 80)}
                    {renderField('Cost/Gram', selectedInventory.CostPerGram, 100)}
                    {renderField('Total Cost', selectedInventory.TotalCost, 110)}
                  </View>
                </View>

                {/* Sales & Purchase 2-column split */}
                <View style={styles.row}>
                  {/* Sales Details */}
                  <View style={[styles.subCard, { flex: 1, marginRight: 10 }]}>
                    <View style={styles.subCardHeader}>
                      <Ionicons name="pricetag-outline" size={14} color="#8D6511" style={{ marginRight: 6 }} />
                      <ThemedText style={styles.subCardTitle}>Sales</ThemedText>
                    </View>
                    <View style={styles.row}>
                      {renderField('Bill#', selectedInventory.SalesBillNo, 85)}
                      {renderField('Date', selectedInventory.SalesDate, 85)}
                      {renderField('SMC', selectedInventory.SalesMc, 80)}
                    </View>
                    <View style={[styles.row, { marginTop: 6 }]}>
                      {renderField('Cost/Gram', selectedInventory.SalesCostPerGram, 85)}
                      {renderField('PCS', selectedInventory.QtySold, 60)}
                      {renderField('Total Sales', selectedInventory.TotalSales, 85)}
                    </View>
                    <View style={[styles.row, { marginTop: 6 }]}>
                      {renderField('Sales Man', selectedInventory.SalesManName, 240)}
                    </View>
                  </View>

                  {/* Purchase Details */}
                  <View style={[styles.subCard, { flex: 1 }]}>
                    <View style={styles.subCardHeader}>
                      <Ionicons name="cart-outline" size={14} color="#8D6511" style={{ marginRight: 6 }} />
                      <ThemedText style={styles.subCardTitle}>Purchase</ThemedText>
                    </View>
                    <View style={styles.row}>
                      {renderField('Purchase#', selectedInventory.PurchaseBillNo, 85)}
                      {renderField('Date', selectedInventory.PurchaseDate, 85)}
                      {renderField('PMC', selectedInventory.PMC, 80)}
                    </View>
                    <View style={[styles.row, { marginTop: 6 }]}>
                      {renderField('Supplier', selectedInventory.SupplierCode, 85)}
                      {renderField('JobSheet#', selectedInventory.JobSheetNo, 85)}
                      {renderField('Date', selectedInventory.JobSheetDate, 80)}
                    </View>
                  </View>
                </View>

                {/* Sales Return & Purchase Return 2-column split */}
                <View style={styles.row}>
                  {/* Sales Return Details */}
                  <View style={[styles.subCard, { flex: 1, marginRight: 10 }]}>
                    <View style={styles.subCardHeader}>
                      <Ionicons name="arrow-undo-outline" size={14} color="#8D6511" style={{ marginRight: 6 }} />
                      <ThemedText style={styles.subCardTitle}>Sales Return</ThemedText>
                    </View>
                    <View style={styles.row}>
                      {renderField('Sales Ret Ref#', selectedInventory.SalesRetRefNo, 85)}
                      {renderField('Date', selectedInventory.SalesRetDate, 85)}
                      {renderField('SMC', selectedInventory.SalesRetSMC, 80)}
                    </View>
                    <View style={[styles.row, { marginTop: 6 }]}>
                      {renderField('Cost/Gram', selectedInventory.SalesRetCostPerGram, 85)}
                      {renderField('PCS', selectedInventory.SalesRetPCS, 60)}
                      {renderField('Total Sales', selectedInventory.SalesRetTotal, 85)}
                    </View>
                  </View>

                  {/* Purchase Return Details */}
                  <View style={[styles.subCard, { flex: 1 }]}>
                    <View style={styles.subCardHeader}>
                      <Ionicons name="return-up-back-outline" size={14} color="#8D6511" style={{ marginRight: 6 }} />
                      <ThemedText style={styles.subCardTitle}>Purchase Return</ThemedText>
                    </View>
                    <View style={styles.row}>
                      {renderField('Purchase Ret.#', selectedInventory.PurchaseRetNo, 85)}
                      {renderField('Date', selectedInventory.PurchaseRetDate, 85)}
                      {renderField('PMC', selectedInventory.PurchaseRetPMC, 80)}
                    </View>
                    <View style={[styles.row, { marginTop: 6 }]}>
                      {renderField('Cost/Gram', selectedInventory.PurchaseRetCostPerGram, 85)}
                      {renderField('PCS', selectedInventory.PurchaseRetPCS, 60)}
                      {renderField('Total', selectedInventory.PurchaseRetTotal, 85)}
                    </View>
                  </View>
                </View>

                {/* Split Merge & Addl Desc Checkboxes matching User Requirements */}
                <View style={[styles.row, { marginTop: 12, alignItems: 'center', flexWrap: 'wrap', gap: 20 }]}>
                  {/* Split Merge Checkbox / Button */}
                  <TouchableOpacity
                    style={styles.radioBtn}
                    onPress={() => {
                      if (!selectedInventory) {
                        const msg = 'Please select an inventory item first';
                        Platform.OS === 'web' ? window.alert(msg) : Alert.alert('Notice', msg);
                        return;
                      }
                      setIsSplitMergeModalVisible(true);
                    }}
                  >
                    <View style={[styles.checkBoxSquare, isSplitMergeModalVisible && styles.checkBoxSquareActive]}>
                      {isSplitMergeModalVisible && <Ionicons name="checkmark" size={12} color="#FFFFFF" />}
                    </View>
                    <ThemedText style={styles.radioLabel}>
                      Split Merge
                    </ThemedText>
                  </TouchableOpacity>

                  {/* Addl. Desc Checkbox / Button */}
                  <TouchableOpacity
                    style={styles.radioBtn}
                    onPress={() => {
                      if (!selectedInventory) {
                        const msg = 'Please select an inventory item first';
                        Platform.OS === 'web' ? window.alert(msg) : Alert.alert('Notice', msg);
                        return;
                      }
                      setIsAddlModalVisible(true);
                    }}
                  >
                    <View style={[styles.checkBoxSquare, isAddlModalVisible && styles.checkBoxSquareActive]}>
                      {isAddlModalVisible && <Ionicons name="checkmark" size={12} color="#FFFFFF" />}
                    </View>
                    <ThemedText style={styles.radioLabel}>
                      Addl. Desc
                    </ThemedText>
                  </TouchableOpacity>
                </View>

              </ScrollView>
            )}

            {!detailLoading && !selectedInventory && (
              <View style={styles.emptyDetail}>
                <Ionicons name="search-outline" size={36} color="#C4B79D" />
                <ThemedText style={styles.emptyDetailText}>Select an inventory item from the left table to inspect full details</ThemedText>
              </View>
            )}
          </View>
        </View>
      </View>

      {/* LOV Modal */}
      <Modal visible={isLOVVisible} transparent={true} animationType="fade">
        <TouchableWithoutFeedback onPress={() => setIsLOVVisible(false)}>
          <View style={styles.modalOverlay}>
            <TouchableWithoutFeedback>
              <View style={styles.modalContent}>
                <View style={styles.modalHeader}>
                  <ThemedText style={styles.modalTitle}>Select {lovType === 'category' ? 'Category' : 'Catalogue'}</ThemedText>
                  <TouchableOpacity onPress={() => setIsLOVVisible(false)}>
                    <Ionicons name="close-circle-outline" size={24} color="#7A684C" />
                  </TouchableOpacity>
                </View>
                {lovLoading ? (
                  <View style={{ padding: 30, alignItems: 'center' }}>
                    <ActivityIndicator size="large" color="#B48218" />
                  </View>
                ) : (
                  <FlatList
                    data={lovData}
                    keyExtractor={(item, index) => index.toString()}
                    renderItem={renderLOVItem}
                    style={{ marginTop: 8, maxHeight: 350 }}
                    ListEmptyComponent={() => (
                      <View style={{ padding: 20, alignItems: 'center' }}>
                        <ThemedText style={{ color: '#7A684C', fontSize: 13 }}>No options found</ThemedText>
                      </View>
                    )}
                  />
                )}
                <TouchableOpacity style={styles.modalCloseBtn} onPress={() => setIsLOVVisible(false)}>
                  <ThemedText style={styles.modalCloseBtnText}>Close</ThemedText>
                </TouchableOpacity>
              </View>
            </TouchableWithoutFeedback>
          </View>
        </TouchableWithoutFeedback>
      </Modal>

      {/* Split / Merge Modal (Matching Image 1: Split Box on Left, Merge Box on Right) */}
      <Modal visible={isSplitMergeModalVisible} transparent={true} animationType="fade">
        <TouchableWithoutFeedback onPress={() => setIsSplitMergeModalVisible(false)}>
          <View style={styles.modalOverlay}>
            <TouchableWithoutFeedback>
              <View style={[styles.modalContent, { maxWidth: 760, padding: 18 }]}>
                <View style={styles.modalHeader}>
                  <ThemedText style={styles.modalTitle}>Split Merge Details</ThemedText>
                  <TouchableOpacity onPress={() => setIsSplitMergeModalVisible(false)}>
                    <Ionicons name="close-circle-outline" size={24} color="#7A684C" />
                  </TouchableOpacity>
                </View>

                {/* Image 1: Split & Merge 2-Box Container */}
                <View style={styles.splitMergeBoxesRow}>
                  {/* Left Box: Split Box */}
                  <View style={styles.imageBox}>
                    <ThemedText style={styles.imageBoxHeader}>Split</ThemedText>
                    {/* Row 1: DestInventoryCode, Weight1, Weight2 */}
                    <View style={styles.imageBoxFieldsRow}>
                      <View style={{ flex: 1.4, marginRight: 8 }}>
                        <ThemedText style={styles.imageBoxLabel}>DestInventoryCode</ThemedText>
                        <TextInput
                          style={styles.imageBoxInput}
                          value={splitHistory?.DestInventoryCode || ''}
                          onChangeText={(v) => setSplitHistory((p: any) => ({ ...p, DestInventoryCode: v }))}
                          editable={true}
                        />
                      </View>
                      <View style={{ flex: 1, marginRight: 8 }}>
                        <ThemedText style={styles.imageBoxLabel}>Weight1</ThemedText>
                        <TextInput
                          style={[styles.imageBoxInput, { textAlign: 'right' }]}
                          value={splitHistory?.Weight1 !== undefined && splitHistory?.Weight1 !== null ? String(splitHistory.Weight1) : ''}
                          onChangeText={(v) => setSplitHistory((p: any) => ({ ...p, Weight1: v }))}
                          keyboardType="numeric"
                          editable={true}
                        />
                      </View>
                      <View style={{ flex: 1 }}>
                        <ThemedText style={styles.imageBoxLabel}>Weight2</ThemedText>
                        <TextInput
                          style={[styles.imageBoxInput, { textAlign: 'right' }]}
                          value={splitHistory?.Weight2 !== undefined && splitHistory?.Weight2 !== null ? String(splitHistory.Weight2) : ''}
                          onChangeText={(v) => setSplitHistory((p: any) => ({ ...p, Weight2: v }))}
                          keyboardType="numeric"
                          editable={true}
                        />
                      </View>
                    </View>

                    {/* Row 2: Cost/Gram */}
                    <View style={[styles.imageBoxFieldsRow, { marginTop: 10 }]}>
                      <View style={{ width: 140 }}>
                        <ThemedText style={styles.imageBoxLabel}>Cost/Gram</ThemedText>
                        <TextInput
                          style={[styles.imageBoxInput, { textAlign: 'right' }]}
                          value={selectedInventory?.CostPerGram !== undefined && selectedInventory?.CostPerGram !== null ? String(selectedInventory.CostPerGram) : ''}
                          onChangeText={(v) => setSelectedInventory((p: any) => ({ ...p, CostPerGram: v }))}
                          keyboardType="numeric"
                          editable={true}
                        />
                      </View>
                    </View>
                  </View>

                  {/* Right Box: Merge Box */}
                  <View style={styles.imageBox}>
                    <ThemedText style={styles.imageBoxHeader}>Merge</ThemedText>
                    {/* Row 1: InventoryCode1, Weight1, InventoryCode2 */}
                    <View style={styles.imageBoxFieldsRow}>
                      <View style={{ flex: 1.2, marginRight: 8 }}>
                        <ThemedText style={styles.imageBoxLabel}>InventoryCode1</ThemedText>
                        <TextInput
                          style={styles.imageBoxInput}
                          value={mergeHistory?.InventoryCode1 || ''}
                          onChangeText={(v) => setMergeHistory((p: any) => ({ ...p, InventoryCode1: v }))}
                          editable={true}
                        />
                      </View>
                      <View style={{ flex: 1, marginRight: 8 }}>
                        <ThemedText style={styles.imageBoxLabel}>Weight1</ThemedText>
                        <TextInput
                          style={[styles.imageBoxInput, { textAlign: 'right' }]}
                          value={mergeHistory?.Weight1 !== undefined && mergeHistory?.Weight1 !== null ? String(mergeHistory.Weight1) : ''}
                          onChangeText={(v) => setMergeHistory((p: any) => ({ ...p, Weight1: v }))}
                          keyboardType="numeric"
                          editable={true}
                        />
                      </View>
                      <View style={{ flex: 1.2 }}>
                        <ThemedText style={styles.imageBoxLabel}>InventoryCode2</ThemedText>
                        <TextInput
                          style={styles.imageBoxInput}
                          value={mergeHistory?.InventoryCode2 || ''}
                          onChangeText={(v) => setMergeHistory((p: any) => ({ ...p, InventoryCode2: v }))}
                          editable={true}
                        />
                      </View>
                    </View>

                    {/* Row 2: Weight2, InventoryCodeN, WeightN */}
                    <View style={[styles.imageBoxFieldsRow, { marginTop: 10 }]}>
                      <View style={{ flex: 1, marginRight: 8 }}>
                        <ThemedText style={styles.imageBoxLabel}>Weight2</ThemedText>
                        <TextInput
                          style={[styles.imageBoxInput, { textAlign: 'right' }]}
                          value={mergeHistory?.Weight2 !== undefined && mergeHistory?.Weight2 !== null ? String(mergeHistory.Weight2) : ''}
                          onChangeText={(v) => setMergeHistory((p: any) => ({ ...p, Weight2: v }))}
                          keyboardType="numeric"
                          editable={true}
                        />
                      </View>
                      <View style={{ flex: 1.2, marginRight: 8 }}>
                        <ThemedText style={styles.imageBoxLabel}>InventoryCodeN</ThemedText>
                        <TextInput
                          style={styles.imageBoxInput}
                          value={mergeHistory?.InventoryCodeN || ''}
                          onChangeText={(v) => setMergeHistory((p: any) => ({ ...p, InventoryCodeN: v }))}
                          editable={true}
                        />
                      </View>
                      <View style={{ flex: 1 }}>
                        <ThemedText style={styles.imageBoxLabel}>WeightN</ThemedText>
                        <TextInput
                          style={[styles.imageBoxInput, { textAlign: 'right' }]}
                          value={mergeHistory?.WeightN !== undefined && mergeHistory?.WeightN !== null ? String(mergeHistory.WeightN) : ''}
                          onChangeText={(v) => setMergeHistory((p: any) => ({ ...p, WeightN: v }))}
                          keyboardType="numeric"
                          editable={true}
                        />
                      </View>
                    </View>
                  </View>
                </View>

                {/* OK Button matching standard form */}
                <TouchableOpacity style={styles.yellowOkBtn} onPress={() => setIsSplitMergeModalVisible(false)}>
                  <ThemedText style={styles.yellowOkBtnText}>OK</ThemedText>
                </TouchableOpacity>
              </View>
            </TouchableWithoutFeedback>
          </View>
        </TouchableWithoutFeedback>
      </Modal>

      {/* Additional Description Modal (Matching Image 2: Description, Cost, Selling Price Grid with OK Button) */}
      <Modal visible={isAddlModalVisible} transparent={true} animationType="fade">
        <TouchableWithoutFeedback onPress={() => setIsAddlModalVisible(false)}>
          <View style={styles.modalOverlay}>
            <TouchableWithoutFeedback>
              <View style={[styles.modalContent, { maxWidth: 620, padding: 18 }]}>
                <View style={styles.modalHeader}>
                  <ThemedText style={styles.modalTitle}>Additional Description</ThemedText>
                  <TouchableOpacity onPress={() => setIsAddlModalVisible(false)}>
                    <Ionicons name="close-circle-outline" size={24} color="#7A684C" />
                  </TouchableOpacity>
                </View>

                {/* Image 2: Top Group and Bottom Group (or 8 rows of Description, Cost, Selling Price) */}
                <View style={{ marginTop: 10 }}>
                  {/* Table Column Labels */}
                  <View style={styles.image2GridHeader}>
                    <ThemedText style={[styles.image2HeaderLabel, { flex: 2 }]}>Description</ThemedText>
                    <ThemedText style={[styles.image2HeaderLabel, { flex: 1, textAlign: 'right', paddingRight: 6 }]}>Cost</ThemedText>
                    <ThemedText style={[styles.image2HeaderLabel, { flex: 1, textAlign: 'right', paddingRight: 6 }]}>Selling Price</ThemedText>
                  </View>

                  {/* Section 1: 4 Rows with outer border box */}
                  <View style={styles.image2OuterBox}>
                    {[1, 2, 3, 4].map(idx => {
                      const desc = addlDescData ? (addlDescData[`Description${idx}`] || '') : '';
                      const cost = addlDescData && addlDescData[`Cost${idx}`] !== undefined && addlDescData[`Cost${idx}`] !== null ? String(addlDescData[`Cost${idx}`]) : '';
                      const sp = addlDescData && addlDescData[`SellingPrice${idx}`] !== undefined && addlDescData[`SellingPrice${idx}`] !== null ? String(addlDescData[`SellingPrice${idx}`]) : '';
                      return (
                        <View key={`addl-row-${idx}`} style={styles.image2Row}>
                          <TextInput
                            style={[styles.image2Input, { flex: 2, marginRight: 8 }]}
                            value={desc}
                            onChangeText={(v) => setAddlDescData((p: any) => ({ ...(p || {}), [`Description${idx}`]: v }))}
                            placeholder=""
                            editable={true}
                          />
                          <TextInput
                            style={[styles.image2Input, { flex: 1, textAlign: 'right', marginRight: 8 }]}
                            value={cost}
                            onChangeText={(v) => setAddlDescData((p: any) => ({ ...(p || {}), [`Cost${idx}`]: v }))}
                            keyboardType="numeric"
                            placeholder=""
                            editable={true}
                          />
                          <TextInput
                            style={[styles.image2Input, { flex: 1, textAlign: 'right' }]}
                            value={sp}
                            onChangeText={(v) => setAddlDescData((p: any) => ({ ...(p || {}), [`SellingPrice${idx}`]: v }))}
                            keyboardType="numeric"
                            placeholder=""
                            editable={true}
                          />
                        </View>
                      );
                    })}
                  </View>

                  {/* Section 2: 4 Rows with outer border box */}
                  <View style={[styles.image2OuterBox, { marginTop: 14 }]}>
                    {[5, 6, 7, 8].map(idx => {
                      const desc = addlDescData ? (addlDescData[`Description${idx}`] || '') : '';
                      const cost = addlDescData && addlDescData[`Cost${idx}`] !== undefined && addlDescData[`Cost${idx}`] !== null ? String(addlDescData[`Cost${idx}`]) : '';
                      const sp = addlDescData && addlDescData[`SellingPrice${idx}`] !== undefined && addlDescData[`SellingPrice${idx}`] !== null ? String(addlDescData[`SellingPrice${idx}`]) : '';
                      return (
                        <View key={`addl-row-${idx}`} style={styles.image2Row}>
                          <TextInput
                            style={[styles.image2Input, { flex: 2, marginRight: 8 }]}
                            value={desc}
                            onChangeText={(v) => setAddlDescData((p: any) => ({ ...(p || {}), [`Description${idx}`]: v }))}
                            placeholder=""
                            editable={true}
                          />
                          <TextInput
                            style={[styles.image2Input, { flex: 1, textAlign: 'right', marginRight: 8 }]}
                            value={cost}
                            onChangeText={(v) => setAddlDescData((p: any) => ({ ...(p || {}), [`Cost${idx}`]: v }))}
                            keyboardType="numeric"
                            placeholder=""
                            editable={true}
                          />
                          <TextInput
                            style={[styles.image2Input, { flex: 1, textAlign: 'right' }]}
                            value={sp}
                            onChangeText={(v) => setAddlDescData((p: any) => ({ ...(p || {}), [`SellingPrice${idx}`]: v }))}
                            keyboardType="numeric"
                            placeholder=""
                            editable={true}
                          />
                        </View>
                      );
                    })}
                  </View>
                </View>

                {/* Bottom Yellow OK Button matching Image 2 */}
                <TouchableOpacity style={styles.yellowOkBtn} onPress={() => setIsAddlModalVisible(false)}>
                  <ThemedText style={styles.yellowOkBtnText}>OK</ThemedText>
                </TouchableOpacity>
              </View>
            </TouchableWithoutFeedback>
          </View>
        </TouchableWithoutFeedback>
      </Modal>
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  safeArea: {
    flex: 1,
    backgroundColor: '#FBF9F5',
  },
  header: {
    height: 48,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingHorizontal: 16,
    borderBottomWidth: 1,
    borderBottomColor: '#EBE2D2',
    backgroundColor: '#FFFFFF',
  },
  backButton: {
    marginRight: 10,
    padding: 4,
  },
  title: {
    fontSize: 16,
    fontWeight: 'bold',
    color: '#3B2D17',
  },
  clearBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#DFD5C3',
    paddingVertical: 5,
    paddingHorizontal: 12,
    borderRadius: 6,
  },
  clearBtnText: {
    color: '#5C4A28',
    fontWeight: 'bold',
    fontSize: 12,
  },
  printBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#DFD5C3',
    paddingVertical: 5,
    paddingHorizontal: 12,
    borderRadius: 6,
  },
  searchBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#F9F2E2',
    borderWidth: 1,
    borderColor: '#B8903B',
    paddingVertical: 5,
    paddingHorizontal: 14,
    borderRadius: 6,
  },
  searchBtnText: {
    color: '#9E6D08',
    fontWeight: 'bold',
    fontSize: 12,
  },
  toast: {
    position: 'absolute',
    top: 55,
    alignSelf: 'center',
    zIndex: 9999,
    paddingHorizontal: 18,
    paddingVertical: 10,
    borderRadius: 8,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.2,
    shadowRadius: 4,
    elevation: 6,
  },
  toastText: {
    color: '#FFFFFF',
    fontWeight: 'bold',
    fontSize: 13,
  },

  container: {
    flex: 1,
    padding: 12,
    backgroundColor: '#FBF9F5',
  },

  // Search Filter Card
  searchCard: {
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    marginBottom: 12,
    overflow: 'hidden',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.05,
    shadowRadius: 2,
    elevation: 2,
  },
  searchFilterBody: {
    padding: 12,
  },
  cardHeaderBanner: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#F5E7CA',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    paddingVertical: 7,
    paddingHorizontal: 12,
  },
  headerIconBadge: {
    width: 20,
    height: 20,
    borderRadius: 10,
    backgroundColor: '#FFF8EA',
    borderWidth: 1,
    borderColor: '#E2D2AF',
    justifyContent: 'center',
    alignItems: 'center',
    marginRight: 8,
  },
  cardHeaderTitle: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#3B2D17',
    letterSpacing: 0.3,
  },

  searchRow: {
    flexDirection: 'row',
    alignItems: 'center',
    flexWrap: 'wrap',
    gap: 12,
  },
  fieldGroup: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 6,
  },
  inputWithBtn: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  label: {
    fontWeight: 'bold',
    color: '#4C3B24',
    fontSize: 11,
  },
  input: {
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderRadius: 6,
    paddingHorizontal: 8,
    height: 28,
    fontSize: 12,
    backgroundColor: '#FFFFFF',
    color: '#2C2519',
  },
  lovBtn: {
    borderWidth: 1,
    borderLeftWidth: 0,
    borderColor: '#E2D7C5',
    backgroundColor: '#FAF5EA',
    paddingHorizontal: 8,
    height: 28,
    justifyContent: 'center',
    alignItems: 'center',
    borderTopRightRadius: 6,
    borderBottomRightRadius: 6,
  },
  lovBtnText: {
    fontWeight: 'bold',
    color: '#6E5528',
    fontSize: 12,
  },

  // Main Content Split
  mainContent: {
    flex: 1,
    flexDirection: 'row',
    gap: 12,
  },
  leftCard: {
    flex: 1.1,
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    overflow: 'hidden',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.05,
    shadowRadius: 2,
    elevation: 2,
  },
  rightCard: {
    flex: 2,
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    overflow: 'hidden',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.05,
    shadowRadius: 2,
    elevation: 2,
  },

  // Table styles
  tableHeaderRow: {
    flexDirection: 'row',
    backgroundColor: '#F5E7CA',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
  },
  headerCell: {
    fontWeight: 'bold',
    color: '#3B2D17',
    paddingVertical: 7,
    paddingHorizontal: 8,
    borderRightWidth: 1,
    borderRightColor: '#E2D3B3',
    fontSize: 11,
  },
  tableRow: {
    flexDirection: 'row',
    borderBottomWidth: 1,
    borderBottomColor: '#EFE7D8',
    backgroundColor: '#FFFFFF',
  },
  tableRowAlt: {
    backgroundColor: '#FAF7F0',
  },
  tableRowSelected: {
    backgroundColor: '#F9F1E2',
  },
  cell: {
    paddingVertical: 6,
    paddingHorizontal: 8,
    borderRightWidth: 1,
    borderRightColor: '#EFE7D8',
    fontSize: 11,
    color: '#33291B',
  },
  cellSelected: {
    fontWeight: '600',
    color: '#8A5D07',
  },
  statusBadge: {
    paddingHorizontal: 6,
    paddingVertical: 2,
    borderRadius: 4,
  },
  statusBadgeIn: {
    backgroundColor: '#E8F5E9',
    borderWidth: 1,
    borderColor: '#C8E6C9',
  },
  statusBadgeOther: {
    backgroundColor: '#FFF3E0',
    borderWidth: 1,
    borderColor: '#FFE0B2',
  },
  statusBadgeText: {
    fontSize: 10,
    fontWeight: 'bold',
  },
  statusBadgeTextIn: {
    color: '#2E7D32',
  },
  statusBadgeTextOther: {
    color: '#E65100',
  },

  // Details Container
  detailsContainer: {
    padding: 12,
    gap: 10,
  },
  subCard: {
    borderWidth: 1,
    borderColor: '#EFE7D8',
    borderRadius: 6,
    backgroundColor: '#FCFAF5',
    padding: 10,
  },
  subCardHeader: {
    flexDirection: 'row',
    alignItems: 'center',
    marginBottom: 8,
    borderBottomWidth: 1,
    borderBottomColor: '#EDE4D2',
    paddingBottom: 4,
  },
  subCardTitle: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#5C4318',
    textTransform: 'uppercase',
    letterSpacing: 0.4,
  },
  row: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 8,
  },
  rowWrap: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    gap: 8,
  },
  detailFieldContainer: {
    flexDirection: 'column',
    gap: 3,
  },
  detailLabel: {
    fontSize: 10,
    color: '#5E4F39',
    fontWeight: 'bold',
  },
  detailInput: {
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderRadius: 5,
    backgroundColor: '#F8F5EE',
    paddingHorizontal: 6,
    height: 24,
    fontSize: 11,
    color: '#382E1E',
    minWidth: 80,
  },

  // Empty states
  emptyContainer: {
    flex: 1,
    padding: 24,
    alignItems: 'center',
    justifyContent: 'center',
  },
  emptyText: {
    fontSize: 11,
    color: '#8A7A62',
    marginTop: 6,
  },
  emptyDetail: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    padding: 20,
  },
  emptyDetailText: {
    fontSize: 12,
    color: '#8A7A62',
    marginTop: 8,
    textAlign: 'center',
  },
  loadingContainer: {
    padding: 14,
    alignItems: 'center',
    justifyContent: 'center',
    flexDirection: 'row',
    gap: 8,
  },
  loadingText: {
    fontSize: 11,
    color: '#8A7A62',
  },

  // Modal
  modalOverlay: {
    flex: 1,
    backgroundColor: 'rgba(0,0,0,0.4)',
    justifyContent: 'center',
    alignItems: 'center',
  },
  modalContent: {
    width: '80%',
    maxWidth: 500,
    maxHeight: '80%',
    backgroundColor: '#FFFFFF',
    padding: 16,
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.15,
    shadowRadius: 8,
    elevation: 5,
  },
  modalHeader: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    borderBottomWidth: 1,
    borderBottomColor: '#E8DCB8',
    paddingBottom: 8,
    marginBottom: 8,
  },
  modalTitle: {
    fontSize: 14,
    fontWeight: 'bold',
    color: '#3B2D17',
  },
  lovItem: {
    paddingVertical: 8,
    paddingHorizontal: 8,
    borderBottomWidth: 1,
    borderBottomColor: '#EFE7D8',
    backgroundColor: '#FFFFFF',
  },
  lovItemTitle: {
    fontWeight: 'bold',
    fontSize: 12,
    color: '#2C2519',
  },
  lovItemSub: {
    fontSize: 11,
    color: '#7A684C',
    marginTop: 2,
  },
  modalCloseBtn: {
    alignSelf: 'flex-end',
    marginTop: 12,
    paddingVertical: 6,
    paddingHorizontal: 16,
    borderRadius: 6,
    backgroundColor: '#F9F2E2',
    borderWidth: 1,
    borderColor: '#B8903B',
  },
  modalCloseBtnText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#9E6D08',
  },

  // Radio Options
  radioBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingVertical: 5,
    paddingHorizontal: 8,
    borderRadius: 6,
    backgroundColor: '#FAF7EE',
    borderWidth: 1,
    borderColor: '#E8DDBE',
  },
  radioCircle: {
    width: 16,
    height: 16,
    borderRadius: 8,
    borderWidth: 2,
    borderColor: '#A89E8D',
    alignItems: 'center',
    justifyContent: 'center',
    marginRight: 6,
    backgroundColor: '#FFFFFF',
  },
  radioCircleActive: {
    borderColor: '#B48218',
  },
  radioDot: {
    width: 8,
    height: 8,
    borderRadius: 4,
    backgroundColor: '#B48218',
  },
  radioLabel: {
    fontSize: 12,
    fontWeight: '600',
    color: '#5E4F39',
  },
  radioLabelActive: {
    color: '#9E6D08',
    fontWeight: 'bold',
  },

  // History Table
  historyTableHeader: {
    flexDirection: 'row',
    backgroundColor: '#F3ECDB',
    paddingVertical: 6,
    paddingHorizontal: 8,
    borderBottomWidth: 1,
    borderBottomColor: '#D8C6A5',
    borderTopLeftRadius: 4,
    borderTopRightRadius: 4,
  },
  historyHeaderCell: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#4A3B22',
  },
  historyTableRow: {
    flexDirection: 'row',
    paddingVertical: 6,
    paddingHorizontal: 8,
    borderBottomWidth: 1,
    borderBottomColor: '#EDE4D2',
    backgroundColor: '#FFFFFF',
    alignItems: 'center',
  },
  historyCell: {
    fontSize: 11,
    color: '#3B2D17',
  },

  // Checkbox Square
  checkBoxSquare: {
    width: 16,
    height: 16,
    borderRadius: 3,
    borderWidth: 1.5,
    borderColor: '#7A684C',
    backgroundColor: '#FFFFFF',
    alignItems: 'center',
    justifyContent: 'center',
    marginRight: 8,
  },
  checkBoxSquareActive: {
    backgroundColor: '#B48218',
    borderColor: '#B48218',
  },

  // Image 1: Split & Merge 2 Boxes
  splitMergeBoxesRow: {
    flexDirection: 'row',
    gap: 14,
    marginTop: 10,
  },
  imageBox: {
    flex: 1,
    borderWidth: 1.5,
    borderColor: '#555555',
    backgroundColor: '#FFFFFF',
    padding: 10,
    borderRadius: 2,
  },
  imageBoxHeader: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#8D6511',
    marginBottom: 6,
    textTransform: 'uppercase',
  },
  imageBoxFieldsRow: {
    flexDirection: 'row',
    alignItems: 'flex-start',
  },
  imageBoxLabel: {
    fontSize: 11,
    color: '#000000',
    marginBottom: 3,
  },
  imageBoxInput: {
    borderWidth: 1,
    borderColor: '#555555',
    backgroundColor: '#FFFFFF',
    height: 24,
    paddingHorizontal: 6,
    fontSize: 11,
    color: '#000000',
    borderRadius: 1,
  },

  // Image 2: Additional Description Boxes
  image2GridHeader: {
    flexDirection: 'row',
    paddingHorizontal: 8,
    marginBottom: 4,
  },
  image2HeaderLabel: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#000000',
  },
  image2OuterBox: {
    borderWidth: 1.5,
    borderColor: '#555555',
    backgroundColor: '#FFFFFF',
    padding: 6,
    gap: 6,
  },
  image2Row: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  image2Input: {
    borderWidth: 1,
    borderColor: '#555555',
    backgroundColor: '#EAEAEA',
    height: 24,
    paddingHorizontal: 6,
    fontSize: 11,
    color: '#000000',
    borderRadius: 1,
  },

  // Yellow OK button matching Image 2
  yellowOkBtn: {
    alignSelf: 'flex-end',
    backgroundColor: '#FFBE00',
    paddingVertical: 5,
    paddingHorizontal: 28,
    borderWidth: 1,
    borderColor: '#C79300',
    marginTop: 14,
    borderRadius: 2,
  },
  yellowOkBtnText: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#000000',
  },
});
