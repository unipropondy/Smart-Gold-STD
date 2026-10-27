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
  Platform,
  ScrollView,
  StyleSheet,
  TextInput,
  TouchableOpacity,
  View,
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';

const API_URL = `${API_BASE_URL}/inventorymovement`;

export default function InventoryMovementScreen() {
  const router = useRouter();

  const [inventoryCode, setInventoryCode] = useState('');
  const [data, setData] = useState<any[]>([]);
  const [loading, setLoading] = useState(false);
  const [toastMsg, setToastMsg] = useState({ title: '', message: '', visible: false });

  const showToast = (title: string, message: string) => {
    setToastMsg({ title, message, visible: true });
    setTimeout(() => {
      setToastMsg(prev => ({ ...prev, visible: false }));
    }, 3000);
  };

  const showAlert = (title: string, msg: string) => {
    if (Platform.OS === 'web') {
      showToast(title, msg);
    } else {
      Alert.alert(title, msg);
    }
  };

  const fetchMovementDetails = async () => {
    if (!inventoryCode.trim()) {
      showAlert('Validation Error', 'Please enter the Inventory Code');
      return;
    }

    setLoading(true);
    try {
      const res = await fetch(`${API_URL}/details/${encodeURIComponent(inventoryCode.trim())}`);
      const json = await res.json();
      if (json.success) {
        if (json.data && json.data.length > 0) {
          setData(json.data);
          showToast('Success', `Loaded ${json.data.length} movement records.`);
        } else {
          setData([]);
          showAlert('Notice', 'No movement records found for this inventory code.');
        }
      } else {
        const msg = json.message || 'Error fetching movement records';
        showAlert('Error', msg);
      }
    } catch (err) {
      console.error(err);
      showAlert('Error', 'Failed to fetch inventory movement details');
    } finally {
      setLoading(false);
    }
  };

  const handleClear = () => {
    setInventoryCode('');
    setData([]);
  };

  // Helper formatting for dates
  const formatDate = (dateStr: any) => {
    if (!dateStr) return '';
    try {
      const d = new Date(dateStr);
      if (isNaN(d.getTime())) return String(dateStr);
      return d.toLocaleDateString('en-GB'); // DD/MM/YYYY
    } catch {
      return String(dateStr);
    }
  };

  const formatNumber = (val: any, decimals: number = 2) => {
    if (val === null || val === undefined || val === '') return '0.00';
    const num = Number(val);
    return isNaN(num) ? String(val) : num.toFixed(decimals);
  };

  const renderHeader = () => (
    <View style={styles.tableHeaderRow}>
      <ThemedText style={[styles.headerCell, { width: 140 }]}>Inventory Code</ThemedText>
      <ThemedText style={[styles.headerCell, { width: 220 }]}>Description</ThemedText>
      <ThemedText style={[styles.headerCell, { width: 90, textAlign: 'right' }]}>Weight</ThemedText>
      <ThemedText style={[styles.headerCell, { width: 80, textAlign: 'center' }]}>Size</ThemedText>
      <ThemedText style={[styles.headerCell, { width: 110, textAlign: 'right' }]}>Cost / Gram</ThemedText>
      <ThemedText style={[styles.headerCell, { width: 115, textAlign: 'right' }]}>Total Cost</ThemedText>
      <ThemedText style={[styles.headerCell, { width: 120, textAlign: 'center' }]}>Location</ThemedText>
      <ThemedText style={[styles.headerCell, { width: 110, textAlign: 'center' }]}>Status</ThemedText>
      <ThemedText style={[styles.headerCell, { width: 120 }]}>Ref Number</ThemedText>
      <ThemedText style={[styles.headerCell, { width: 120, borderRightWidth: 0, textAlign: 'center' }]}>Ref Date</ThemedText>
    </View>
  );

  const renderRow = ({ item, index }: { item: any; index: number }) => {
    return (
      <View style={[styles.tableRow, index % 2 === 1 && styles.tableRowAlt]}>
        <ThemedText style={[styles.cell, { width: 140, fontWeight: '600' }]} numberOfLines={1}>
          {item.InventoryCode}
        </ThemedText>
        <ThemedText style={[styles.cell, { width: 220 }]} numberOfLines={1}>
          {item.Description || '-'}
        </ThemedText>
        <ThemedText style={[styles.cell, { width: 90, textAlign: 'right' }]} numberOfLines={1}>
          {formatNumber(item.Weight, 3)}
        </ThemedText>
        <ThemedText style={[styles.cell, { width: 80, textAlign: 'center' }]} numberOfLines={1}>
          {item.Size || '-'}
        </ThemedText>
        <ThemedText style={[styles.cell, { width: 110, textAlign: 'right' }]} numberOfLines={1}>
          {formatNumber(item.CostPerGram, 2)}
        </ThemedText>
        <ThemedText style={[styles.cell, { width: 115, textAlign: 'right', fontWeight: '600', color: '#8A5D07' }]} numberOfLines={1}>
          {formatNumber(item.TotalCost, 2)}
        </ThemedText>
        <ThemedText style={[styles.cell, { width: 120, textAlign: 'center' }]} numberOfLines={1}>
          <View style={styles.badgeLocation}>
            <ThemedText style={styles.badgeLocationText}>{item.LocationCode || '-'}</ThemedText>
          </View>
        </ThemedText>
        <ThemedText style={[styles.cell, { width: 110, textAlign: 'center' }]} numberOfLines={1}>
          <View style={[
            styles.badgeStatus,
            item.Status === 'I' || item.Status === 'IN' ? styles.badgeStatusIn : styles.badgeStatusOut
          ]}>
            <ThemedText style={[
              styles.badgeStatusText,
              item.Status === 'I' || item.Status === 'IN' ? styles.badgeStatusInText : styles.badgeStatusOutText
            ]}>
              {item.Status || '-'}
            </ThemedText>
          </View>
        </ThemedText>
        <ThemedText style={[styles.cell, { width: 120 }]} numberOfLines={1}>
          {item.RefNumber || '-'}
        </ThemedText>
        <ThemedText style={[styles.cell, { width: 120, borderRightWidth: 0, textAlign: 'center', color: '#6A583E' }]} numberOfLines={1}>
          {formatDate(item.RefDate)}
        </ThemedText>
      </View>
    );
  };

  return (
    <SafeAreaView style={styles.safeArea} edges={['top']}>
      <Stack.Screen options={{ headerShown: false }} />

      {/* Top Header Bar */}
      <View style={styles.header}>
        <View style={{ flexDirection: 'row', alignItems: 'center' }}>
          <TouchableOpacity style={styles.backButton} onPress={() => router.back()}>
            <Ionicons name="arrow-back" size={22} color="#3B2D17" />
          </TouchableOpacity>
          <ThemedText style={styles.title}>Inventory Movement</ThemedText>
        </View>

        <View style={{ flexDirection: 'row', gap: 8, alignItems: 'center' }}>
          <TouchableOpacity style={styles.clearBtn} onPress={handleClear}>
            <Ionicons name="refresh-outline" size={14} color="#5C4A28" style={{ marginRight: 4 }} />
            <ThemedText style={styles.clearBtnText}>Clear</ThemedText>
          </TouchableOpacity>
          <TouchableOpacity style={styles.fetchBtn} onPress={fetchMovementDetails}>
            <Ionicons name="search" size={14} color="#9E6D08" style={{ marginRight: 4 }} />
            <ThemedText style={styles.fetchBtnText}>Fetch</ThemedText>
          </TouchableOpacity>
        </View>
      </View>

      <View style={styles.container}>
        {/* Search Filter Card */}
        <View style={styles.searchCard}>
          <View style={styles.cardHeaderBanner}>
            <View style={styles.headerIconBadge}>
              <Ionicons name="swap-horizontal" size={13} color="#B48218" />
            </View>
            <ThemedText style={styles.cardHeaderTitle}>Track Inventory Code Movement</ThemedText>
          </View>

          <View style={styles.searchFilterBody}>
            <View style={styles.searchRow}>
              <View style={styles.fieldGroup}>
                <ThemedText style={styles.label}>Inventory Code</ThemedText>
                <TextInput
                  style={styles.input}
                  value={inventoryCode}
                  onChangeText={setInventoryCode}
                  placeholder="e.g. INV001"
                  placeholderTextColor="#A89E8D"
                  autoCapitalize="characters"
                  onSubmitEditing={fetchMovementDetails}
                  returnKeyType="search"
                />
              </View>

              <TouchableOpacity style={styles.actionPillBtn} onPress={fetchMovementDetails}>
                <Ionicons name="arrow-forward-circle-outline" size={16} color="#9E6D08" style={{ marginRight: 4 }} />
                <ThemedText style={styles.actionPillBtnText}>View Ledger</ThemedText>
              </TouchableOpacity>

              {data.length > 0 && (
                <View style={styles.summaryBadge}>
                  <ThemedText style={styles.summaryBadgeLabel}>Total Movements:</ThemedText>
                  <ThemedText style={styles.summaryBadgeValue}>{data.length}</ThemedText>
                </View>
              )}

              {loading && (
                <View style={styles.loadingInline}>
                  <ActivityIndicator size="small" color="#B48218" />
                  <ThemedText style={styles.loadingText}>Fetching movements...</ThemedText>
                </View>
              )}
            </View>
          </View>
        </View>

        {/* Movement Table Card */}
        <View style={styles.tableCard}>
          <View style={styles.cardHeaderBanner}>
            <View style={styles.headerIconBadge}>
              <Ionicons name="file-tray-full-outline" size={13} color="#B48218" />
            </View>
            <ThemedText style={styles.cardHeaderTitle}>Movement History Audit Trail</ThemedText>
            {data.length > 0 && (
              <ThemedText style={styles.headerSubCount}>({data.length} records)</ThemedText>
            )}
          </View>

          <ScrollView horizontal showsHorizontalScrollIndicator={true} contentContainerStyle={{ minWidth: '100%' }}>
            <View style={{ flex: 1 }}>
              {renderHeader()}
              <FlatList
                data={data}
                keyExtractor={(item, index) => `${item.InventoryCode || index}-${index}`}
                renderItem={renderRow}
                contentContainerStyle={{ flexGrow: 1 }}
                ListEmptyComponent={
                  !loading ? (
                    <View style={styles.emptyContainer}>
                      <Ionicons name="swap-vertical-outline" size={32} color="#C4B79D" />
                      <ThemedText style={styles.emptyTitle}>No Movement History</ThemedText>
                      <ThemedText style={styles.emptySubtitle}>
                        Enter an Inventory Code above and tap Fetch to view its transaction history.
                      </ThemedText>
                    </View>
                  ) : null
                }
              />
            </View>
          </ScrollView>
        </View>
      </View>

      {/* Toast Notification */}
      {toastMsg.visible && (
        <View style={styles.toastBox}>
          <ThemedText style={styles.toastTitle}>{toastMsg.title}</ThemedText>
          <ThemedText style={styles.toastMessage}>{toastMsg.message}</ThemedText>
        </View>
      )}
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
  fetchBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#F9F2E2',
    borderWidth: 1,
    borderColor: '#B8903B',
    paddingVertical: 5,
    paddingHorizontal: 14,
    borderRadius: 6,
  },
  fetchBtnText: {
    color: '#9E6D08',
    fontWeight: 'bold',
    fontSize: 12,
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
  headerSubCount: {
    marginLeft: 6,
    fontSize: 11,
    fontWeight: '500',
    color: '#735F3C',
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
    gap: 8,
  },
  label: {
    fontWeight: 'bold',
    color: '#4C3B24',
    fontSize: 12,
  },
  input: {
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderRadius: 6,
    paddingHorizontal: 10,
    height: 32,
    minWidth: 180,
    fontSize: 12,
    backgroundColor: '#FFFFFF',
    color: '#2C2519',
  },
  actionPillBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#F9F2E2',
    borderWidth: 1,
    borderColor: '#CBB27A',
    paddingVertical: 6,
    paddingHorizontal: 12,
    borderRadius: 6,
  },
  actionPillBtnText: {
    color: '#8A5D07',
    fontWeight: 'bold',
    fontSize: 11,
  },
  summaryBadge: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#F8F4EA',
    borderWidth: 1,
    borderColor: '#E5DAC0',
    borderRadius: 6,
    paddingHorizontal: 10,
    paddingVertical: 5,
    gap: 6,
  },
  summaryBadgeLabel: {
    fontSize: 11,
    color: '#6E5D3E',
    fontWeight: '500',
  },
  summaryBadgeValue: {
    fontSize: 12,
    color: '#9E6D08',
    fontWeight: 'bold',
  },
  loadingInline: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 6,
  },
  loadingText: {
    fontSize: 11,
    color: '#8A5D07',
    fontWeight: '500',
  },

  // Table Card
  tableCard: {
    flex: 1,
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
  tableHeaderRow: {
    flexDirection: 'row',
    backgroundColor: '#EFE3CA',
    borderBottomWidth: 1,
    borderBottomColor: '#DECFB1',
  },
  headerCell: {
    fontWeight: 'bold',
    color: '#3B2D17',
    paddingVertical: 8,
    paddingHorizontal: 8,
    borderRightWidth: 1,
    borderRightColor: '#DECFB1',
    fontSize: 11,
  },
  tableRow: {
    flexDirection: 'row',
    borderBottomWidth: 1,
    borderBottomColor: '#F0E8D9',
    backgroundColor: '#FFFFFF',
    alignItems: 'center',
  },
  tableRowAlt: {
    backgroundColor: '#FDFBF7',
  },
  cell: {
    color: '#2C2519',
    paddingVertical: 8,
    paddingHorizontal: 8,
    borderRightWidth: 1,
    borderRightColor: '#F0E8D9',
    fontSize: 11,
  },

  badgeLocation: {
    backgroundColor: '#F5EEDD',
    borderWidth: 1,
    borderColor: '#DFD2B7',
    borderRadius: 4,
    paddingHorizontal: 6,
    paddingVertical: 2,
    alignSelf: 'center',
  },
  badgeLocationText: {
    fontSize: 10,
    fontWeight: '600',
    color: '#5C4824',
  },

  badgeStatus: {
    borderRadius: 4,
    paddingHorizontal: 6,
    paddingVertical: 2,
    alignSelf: 'center',
    borderWidth: 1,
  },
  badgeStatusIn: {
    backgroundColor: '#EEF7EE',
    borderColor: '#C3E6C3',
  },
  badgeStatusText: {
    fontSize: 10,
    fontWeight: '700',
  },
  badgeStatusInText: {
    color: '#1E6B20',
  },
  badgeStatusOut: {
    backgroundColor: '#FFF1F1',
    borderColor: '#F8CECE',
  },
  badgeStatusOutText: {
    color: '#A82B2B',
    fontSize: 10,
    fontWeight: '700',
  },

  // Empty State
  emptyContainer: {
    flex: 1,
    padding: 36,
    alignItems: 'center',
    justifyContent: 'center',
  },
  emptyTitle: {
    fontSize: 13,
    fontWeight: 'bold',
    color: '#5C4A28',
    marginTop: 8,
    marginBottom: 4,
  },
  emptySubtitle: {
    fontSize: 11,
    color: '#8A7B64',
    textAlign: 'center',
    maxWidth: 280,
  },

  // Toast
  toastBox: {
    position: 'absolute',
    bottom: 24,
    alignSelf: 'center',
    width: '80%',
    maxWidth: 380,
    backgroundColor: '#2E2413',
    padding: 12,
    borderRadius: 8,
    zIndex: 1000,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.25,
    shadowRadius: 4,
    elevation: 5,
    borderWidth: 1,
    borderColor: '#B8903B',
  },
  toastTitle: {
    color: '#FFBD00',
    fontWeight: 'bold',
    fontSize: 12,
    marginBottom: 3,
  },
  toastMessage: {
    color: '#FFFFFF',
    fontSize: 11,
  },
});
