import { ThemedText } from '@/components/themed-text';
import { API_BASE_URL } from '@/constants/api';
import { useThemeColors } from '@/hooks/useThemeColors';
import { useThemeContext } from '@/contexts/ThemeContext';
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

const API_URL = `${API_BASE_URL}/catalogue`;

export default function CatalogueSearchScreen() {
  const themeColors = useThemeColors();
  const { accentColor } = useThemeContext();
  const { background: backgroundColor, card: cardColor, text: textColor, border: borderColor, primaryGold: GOLD_COLOR, subtitle: subtitleColor } = themeColors;
  const router = useRouter();

  const [category, setCategory] = useState('');
  const [catalogue, setCatalogue] = useState('');
  const [fromWeight, setFromWeight] = useState('');
  const [toWeight, setToWeight] = useState('');
  const [inventoryCode, setInventoryCode] = useState('');

  const [data, setData] = useState<any[]>([]);
  const [loading, setLoading] = useState(false);
  const [toast, setToast] = useState<{ message: string; type: 'success' | 'error' | 'info' } | null>(null);

  const showToast = (message: string, type: 'success' | 'error' | 'info' = 'info') => {
    setToast({ message, type });
    setTimeout(() => {
      setToast(null);
    }, 3000);
  };

  // LOV State
  const [isLOVVisible, setIsLOVVisible] = useState(false);
  const [lovType, setLovType] = useState<'category' | 'catalogue' | null>(null);
  const [lovData, setLovData] = useState<any[]>([]);
  const [lovLoading, setLovLoading] = useState(false);

  const fetchMovementDetails = async () => {
    setLoading(true);
    try {
      let queryUrl = `${API_URL}/search?`;
      const params = new URLSearchParams();
      if (category.trim()) params.append('category', category.trim());
      if (catalogue.trim()) params.append('catalogue', catalogue.trim());
      if (fromWeight.trim()) params.append('fromWeight', fromWeight.trim());
      if (toWeight.trim()) params.append('toWeight', toWeight.trim());
      if (inventoryCode.trim()) params.append('inventoryCode', inventoryCode.trim());

      const res = await fetch(queryUrl + params.toString());
      const json = await res.json();
      if (json.success) {
        if (json.data && json.data.length > 0) {
          setData(json.data);
          showToast(`Found ${json.data.length} item(s)`, 'success');
        } else {
          setData([]);
          showToast('Stock Not Available', 'info');
        }
      } else {
        const msg = json.message || 'Error fetching data';
        showToast(msg, 'error');
      }
    } catch (err) {
      console.error(err);
      showToast('Failed to fetch catalogue details', 'error');
    } finally {
      setLoading(false);
    }
  };

  const handleClear = () => {
    setCategory('');
    setCatalogue('');
    setFromWeight('');
    setToWeight('');
    setInventoryCode('');
    setData([]);
  };

  const fetchLOV = async (type: 'category' | 'catalogue') => {
    setLovType(type);
    setLovLoading(true);
    setIsLOVVisible(true);
    setLovData([]);
    try {
      let url = `${API_URL}/lov/${type}`;
      if (type === 'catalogue' && category) {
        url += `?category=${encodeURIComponent(category.trim())}`;
      }
      const res = await fetch(url);
      const json = await res.json();
      if (json.success) {
        setLovData(json.data || []);
      } else {
        showToast(json.message || `Failed to fetch ${type}`, 'error');
      }
    } catch (err) {
      console.error('LOV Error:', err);
      showToast(`Error fetching ${type} list`, 'error');
    } finally {
      setLovLoading(false);
    }
  };

  const totalWeight = data.reduce((sum, item) => sum + (parseFloat(item.Weight) || 0), 0);

  const renderLOVItem = ({ item }: { item: any }) => {
    if (lovType === 'category') {
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
            setCatalogue(item.CatalogueCode);
            setIsLOVVisible(false);
          }}
        >
          <ThemedText style={styles.lovItemTitle}>{item.CatalogueCode}</ThemedText>
          <ThemedText style={styles.lovItemSub}>{item.CatalogueName}</ThemedText>
        </TouchableOpacity>
      );
    }
  };

  return (
    <SafeAreaView style={styles.safeArea} edges={['top']}>
      <Stack.Screen options={{ headerShown: false }} />

      {/* Toast Notification */}
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

      {/* Top Header Bar */}
      <View style={styles.header}>
        <View style={{ flexDirection: 'row', alignItems: 'center' }}>
          <TouchableOpacity style={styles.backButton} onPress={() => router.back()}>
            <Ionicons name="arrow-back" size={22} color="#3B2D17" />
          </TouchableOpacity>
          <ThemedText style={styles.title}>Catalogue Search</ThemedText>
        </View>
        <View style={{ flexDirection: 'row', alignItems: 'center' }}>
          <TouchableOpacity style={styles.clearBtn} onPress={handleClear}>
            <Ionicons name="refresh-outline" size={14} color="#5C4A28" style={{ marginRight: 4 }} />
            <ThemedText style={styles.clearBtnText}>Clear</ThemedText>
          </TouchableOpacity>
          <TouchableOpacity style={styles.searchBtn} onPress={fetchMovementDetails}>
            {loading ? (
              <ActivityIndicator size="small" color="#9E6D08" style={{ marginRight: 6 }} />
            ) : (
              <Ionicons name="search" size={14} color="#9E6D08" style={{ marginRight: 4 }} />
            )}
            <ThemedText style={styles.searchBtnText}>Search</ThemedText>
          </TouchableOpacity>
        </View>
      </View>

      <View style={styles.container}>
        {/* Search Filter Card */}
        <View style={styles.goldCard}>
          <View style={styles.cardHeaderBanner}>
            <View style={styles.headerIconBadge}>
              <Ionicons name="filter" size={14} color="#B48218" />
            </View>
            <ThemedText style={styles.cardHeaderTitle}>Search Filters</ThemedText>
          </View>

          <View style={styles.cardBody}>
            <View style={styles.filterRow}>
              {/* Category */}
              <View style={styles.fieldContainer}>
                <ThemedText style={styles.label}>Category</ThemedText>
                <View style={styles.lookupContainer}>
                  <TextInput
                    style={[styles.input, { width: 110, borderTopRightRadius: 0, borderBottomRightRadius: 0 }]}
                    value={category}
                    onChangeText={setCategory}
                    autoCapitalize="characters"
                    placeholder="All"
                    placeholderTextColor="#A89D8B"
                  />
                  <TouchableOpacity style={styles.lookupBtn} onPress={() => fetchLOV('category')}>
                    <ThemedText style={styles.lookupBtnText}>...</ThemedText>
                  </TouchableOpacity>
                </View>
              </View>

              {/* Catalogue */}
              <View style={styles.fieldContainer}>
                <ThemedText style={styles.label}>Catalogue</ThemedText>
                <View style={styles.lookupContainer}>
                  <TextInput
                    style={[styles.input, { width: 120, borderTopRightRadius: 0, borderBottomRightRadius: 0 }]}
                    value={catalogue}
                    onChangeText={setCatalogue}
                    autoCapitalize="characters"
                    placeholder="All"
                    placeholderTextColor="#A89D8B"
                  />
                  <TouchableOpacity style={styles.lookupBtn} onPress={() => fetchLOV('catalogue')}>
                    <ThemedText style={styles.lookupBtnText}>...</ThemedText>
                  </TouchableOpacity>
                </View>
              </View>

              {/* From Weight */}
              <View style={styles.fieldContainer}>
                <ThemedText style={styles.label}>From Weight</ThemedText>
                <TextInput
                  style={[styles.input, { width: 95, textAlign: 'right' }]}
                  value={fromWeight}
                  onChangeText={setFromWeight}
                  keyboardType="numeric"
                  placeholder="0.000"
                  placeholderTextColor="#A89D8B"
                />
              </View>

              {/* To Weight */}
              <View style={styles.fieldContainer}>
                <ThemedText style={styles.label}>To Weight</ThemedText>
                <TextInput
                  style={[styles.input, { width: 95, textAlign: 'right' }]}
                  value={toWeight}
                  onChangeText={setToWeight}
                  keyboardType="numeric"
                  placeholder="0.000"
                  placeholderTextColor="#A89D8B"
                />
              </View>

              {/* Inventory Code */}
              <View style={styles.fieldContainer}>
                <ThemedText style={styles.label}>Inventory Code</ThemedText>
                <TextInput
                  style={[styles.input, { width: 140 }]}
                  value={inventoryCode}
                  onChangeText={setInventoryCode}
                  autoCapitalize="characters"
                  placeholder="Scan / Type"
                  placeholderTextColor="#A89D8B"
                />
              </View>
            </View>
          </View>
        </View>

        {/* Results Table Section */}
        <View style={[styles.goldCard, { flex: 1, marginTop: 12 }]}>
          <View style={styles.cardHeaderBanner}>
            <View style={styles.headerIconBadge}>
              <Ionicons name="grid-outline" size={14} color="#B48218" />
            </View>
            <ThemedText style={styles.cardHeaderTitle}>Catalogue Stock Items</ThemedText>
            {data.length > 0 && (
              <View style={styles.badgeCount}>
                <ThemedText style={styles.badgeCountText}>{data.length} items</ThemedText>
              </View>
            )}
          </View>

          <View style={{ flex: 1, width: '100%' }}>
            {/* Table Header */}
            <View style={styles.tableHeader}>
              <ThemedText style={[styles.tableHeaderText, { flex: 1.2 }]}>Location</ThemedText>
              <ThemedText style={[styles.tableHeaderText, { flex: 1.8 }]}>Inventory Code</ThemedText>
              <ThemedText style={[styles.tableHeaderText, { flex: 3.5 }]}>Description</ThemedText>
              <ThemedText style={[styles.tableHeaderText, { flex: 1.5, textAlign: 'right' }]}>Weight (g)</ThemedText>
              <ThemedText style={[styles.tableHeaderText, { flex: 1.2, textAlign: 'center' }]}>Size Code</ThemedText>
              <ThemedText style={[styles.tableHeaderText, { flex: 1.2, textAlign: 'center' }]}>Size Type</ThemedText>
              <ThemedText style={[styles.tableHeaderText, { flex: 1.2, textAlign: 'center' }]}>Status</ThemedText>
            </View>

            {/* Table Body */}
            <FlatList
              data={data}
              keyExtractor={(item, index) => `${item.InventoryCode}-${index}`}
              renderItem={({ item, index }) => (
                <View style={[styles.tableRow, index % 2 === 1 && styles.tableRowAlt]}>
                  <ThemedText style={[styles.tableCellText, { flex: 1.2 }]} numberOfLines={1}>
                    {item.Locationcode || '-'}
                  </ThemedText>
                  <ThemedText style={[styles.tableCellText, { flex: 1.8, fontWeight: '600' }]} numberOfLines={1}>
                    {item.InventoryCode || '-'}
                  </ThemedText>
                  <ThemedText style={[styles.tableCellText, { flex: 3.5 }]} numberOfLines={1}>
                    {item.Description || '-'}
                  </ThemedText>
                  <ThemedText style={[styles.tableCellText, { flex: 1.5, textAlign: 'right', fontWeight: 'bold' }]} numberOfLines={1}>
                    {item.Weight !== undefined && item.Weight !== null ? Number(item.Weight).toFixed(3) : '0.000'}
                  </ThemedText>
                  <ThemedText style={[styles.tableCellText, { flex: 1.2, textAlign: 'center' }]} numberOfLines={1}>
                    {item.SizeCode || '-'}
                  </ThemedText>
                  <ThemedText style={[styles.tableCellText, { flex: 1.2, textAlign: 'center' }]} numberOfLines={1}>
                    {item.SizeType || '-'}
                  </ThemedText>
                  <View style={{ flex: 1.2, alignItems: 'center', justifyContent: 'center' }}>
                    <View style={[styles.statusBadge, item.Status === 'IN' ? styles.statusBadgeIn : styles.statusBadgeOther]}>
                      <ThemedText style={[styles.statusBadgeText, item.Status === 'IN' ? styles.statusBadgeTextIn : styles.statusBadgeTextOther]}>
                        {item.Status || 'N/A'}
                      </ThemedText>
                    </View>
                  </View>
                </View>
              )}
              contentContainerStyle={{ flexGrow: 1 }}
              ListEmptyComponent={() => (
                <View style={styles.emptyTableState}>
                  <Ionicons name="cube-outline" size={38} color="#C4B79D" style={{ marginBottom: 8 }} />
                  <ThemedText style={styles.emptyStateTitle}>
                    {loading ? 'Searching Catalogue...' : 'No Catalogue Stock Records Found'}
                  </ThemedText>
                  <ThemedText style={styles.emptyStateSub}>
                    {loading ? 'Please wait a moment' : 'Select filters and tap "Search" to view inventory items'}
                  </ThemedText>
                </View>
              )}
            />

            {/* Table Total Summary Row */}
            {data.length > 0 && (
              <View style={styles.tableTotalRow}>
                <ThemedText style={[styles.tableTotalCellText, { flex: 1.2 }]}>Total Items:</ThemedText>
                <ThemedText style={[styles.tableTotalCellText, { flex: 1.8 }]}>{data.length}</ThemedText>
                <ThemedText style={[styles.tableTotalCellText, { flex: 3.5, textAlign: 'right', paddingRight: 12 }]}>
                  Total Weight:
                </ThemedText>
                <ThemedText style={[styles.tableTotalCellText, { flex: 1.5, textAlign: 'right', color: '#9E6D08' }]}>
                  {totalWeight.toFixed(3)}g
                </ThemedText>
                <ThemedText style={[styles.tableTotalCellText, { flex: 3.6 }]}></ThemedText>
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
              <View style={styles.modalContainer}>
                <View style={styles.modalHeader}>
                  <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                    <View style={styles.headerIconBadge}>
                      <Ionicons name="list" size={14} color="#B48218" />
                    </View>
                    <ThemedText style={styles.modalTitle}>
                      Select {lovType === 'category' ? 'Category' : 'Catalogue'}
                    </ThemedText>
                  </View>
                  <TouchableOpacity onPress={() => setIsLOVVisible(false)} style={{ padding: 4 }}>
                    <Ionicons name="close-circle-outline" size={22} color="#7A684C" />
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
                    style={{ maxHeight: 380 }}
                    ListEmptyComponent={() => (
                      <View style={{ padding: 24, alignItems: 'center' }}>
                        <ThemedText style={{ color: '#7A684C', fontSize: 13 }}>No options found</ThemedText>
                      </View>
                    )}
                  />
                )}

                <View style={styles.modalFooter}>
                  <TouchableOpacity style={styles.modalCloseBtn} onPress={() => setIsLOVVisible(false)}>
                    <ThemedText style={styles.modalCloseBtnText}>Close</ThemedText>
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
  safeArea: {
    flex: 1,
    backgroundColor: '#FBF9F5',
  },
  header: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingHorizontal: 16,
    paddingVertical: 10,
    borderBottomWidth: 1,
    borderBottomColor: '#EBE2D2',
    backgroundColor: '#FFFFFF',
  },
  backButton: {
    padding: 4,
    marginRight: 12,
  },
  title: {
    fontSize: 20,
    fontWeight: '400',
    letterSpacing: 1,
    color: '#3B2D17',
  },
  clearBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#DFD5C3',
    paddingVertical: 6,
    paddingHorizontal: 12,
    borderRadius: 5,
    marginRight: 8,
  },
  clearBtnText: {
    color: '#5C4A28',
    fontWeight: 'bold',
    fontSize: 12,
  },
  searchBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#F9F2E2',
    borderWidth: 1,
    borderColor: '#B8903B',
    paddingVertical: 6,
    paddingHorizontal: 14,
    borderRadius: 5,
  },
  searchBtnText: {
    color: '#9E6D08',
    fontWeight: 'bold',
    fontSize: 12,
  },
  container: {
    flex: 1,
    paddingVertical: 12,
    paddingHorizontal: 16,
    backgroundColor: '#FBF9F5',
  },

  // Luxury Gold Card Styling (jobsheet style)
  goldCard: {
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
  },
  cardHeaderBanner: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#F5E7CA',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    paddingVertical: 8,
    paddingHorizontal: 12,
  },
  headerIconBadge: {
    width: 24,
    height: 24,
    borderRadius: 5,
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
  badgeCount: {
    marginLeft: 'auto',
    backgroundColor: '#FFF8EA',
    borderWidth: 1,
    borderColor: '#ECD8AB',
    borderRadius: 12,
    paddingHorizontal: 8,
    paddingVertical: 2,
  },
  badgeCountText: {
    fontSize: 11,
    color: '#8B6413',
    fontWeight: 'bold',
  },
  cardBody: {
    padding: 12,
  },
  filterRow: {
    flexDirection: 'row',
    alignItems: 'center',
    flexWrap: 'wrap',
    gap: 12,
  },
  fieldContainer: {
    marginRight: 8,
  },
  label: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3A2E19',
    marginBottom: 4,
  },
  input: {
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderRadius: 6,
    paddingHorizontal: 8,
    paddingVertical: 3,
    fontSize: 12,
    height: 30,
    backgroundColor: '#FFFFFF',
    color: '#2C2519',
  },
  lookupContainer: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  lookupBtn: {
    width: 32,
    height: 30,
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderLeftWidth: 0,
    justifyContent: 'center',
    alignItems: 'center',
    borderTopRightRadius: 6,
    borderBottomRightRadius: 6,
    backgroundColor: '#FAF5EA',
  },
  lookupBtnText: {
    color: '#6E5528',
    fontWeight: 'bold',
    fontSize: 12,
  },

  // Table Styling (jobsheet style)
  tableHeader: {
    flexDirection: 'row',
    backgroundColor: '#F5E7CA',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    paddingVertical: 8,
    paddingHorizontal: 8,
    alignItems: 'center',
  },
  tableHeaderText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3B2D17',
    paddingHorizontal: 6,
  },
  tableRow: {
    flexDirection: 'row',
    paddingVertical: 7,
    paddingHorizontal: 8,
    borderBottomWidth: 1,
    borderBottomColor: '#F0E6D2',
    alignItems: 'center',
    backgroundColor: '#FFFFFF',
  },
  tableRowAlt: {
    backgroundColor: '#FCFAF6',
  },
  tableCellText: {
    fontSize: 11,
    color: '#332717',
    paddingHorizontal: 6,
  },
  statusBadge: {
    paddingHorizontal: 8,
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
  emptyTableState: {
    paddingVertical: 50,
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: '#FFFFFF',
  },
  emptyStateTitle: {
    fontSize: 14,
    fontWeight: 'bold',
    color: '#5C4A28',
    marginBottom: 4,
  },
  emptyStateSub: {
    fontSize: 12,
    color: '#8C7A5C',
  },
  tableTotalRow: {
    flexDirection: 'row',
    backgroundColor: '#F9F1DF',
    borderTopWidth: 1,
    borderTopColor: '#E5D6B6',
    paddingVertical: 8,
    paddingHorizontal: 8,
    alignItems: 'center',
  },
  tableTotalCellText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#32230D',
    paddingHorizontal: 6,
  },

  // Modal Styling
  modalOverlay: {
    flex: 1,
    backgroundColor: 'rgba(0,0,0,0.45)',
    justifyContent: 'center',
    alignItems: 'center',
  },
  modalContainer: {
    width: '85%',
    maxWidth: 500,
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#E8DCB8',
    borderRadius: 8,
    overflow: 'hidden',
    shadowColor: '#A38035',
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.15,
    shadowRadius: 10,
    elevation: 5,
  },
  modalHeader: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    backgroundColor: '#F5E7CA',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    paddingVertical: 9,
    paddingHorizontal: 12,
  },
  modalTitle: {
    fontSize: 14,
    fontWeight: 'bold',
    color: '#3B2D17',
  },
  lovItem: {
    paddingVertical: 10,
    paddingHorizontal: 14,
    borderBottomWidth: 1,
    borderBottomColor: '#F0E6D2',
    backgroundColor: '#FFFFFF',
  },
  lovItemTitle: {
    fontWeight: 'bold',
    fontSize: 13,
    color: '#362B1A',
  },
  lovItemSub: {
    fontSize: 11,
    color: '#705D3D',
    marginTop: 2,
  },
  modalFooter: {
    padding: 10,
    borderTopWidth: 1,
    borderTopColor: '#EADBCA',
    alignItems: 'flex-end',
    backgroundColor: '#FAF7F0',
  },
  modalCloseBtn: {
    paddingVertical: 6,
    paddingHorizontal: 14,
    borderRadius: 5,
    borderWidth: 1,
    borderColor: '#DFD5C3',
    backgroundColor: '#FFFFFF',
  },
  modalCloseBtnText: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#5C4A28',
  },

  // Toast
  toast: {
    position: 'absolute',
    top: 50,
    alignSelf: 'center',
    paddingHorizontal: 20,
    paddingVertical: 10,
    borderRadius: 6,
    elevation: 6,
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.25,
    shadowRadius: 3.84,
    zIndex: 9999,
  },
  toastText: {
    fontSize: 13,
    fontWeight: 'bold',
    color: '#FFFFFF',
  },
});



