import { ThemedText } from '@/components/themed-text';
import { API_BASE_URL } from '@/constants/api';
import { useThemeColors } from '@/hooks/useThemeColors';
import { Ionicons } from '@expo/vector-icons';
import { Stack, useRouter } from 'expo-router';
import React, { useEffect, useState } from 'react';
import {
  ActivityIndicator,
  FlatList,
  Modal,
  Platform,
  ScrollView,
  StyleSheet,
  TextInput,
  TouchableOpacity,
  View,
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';

const API_URL = `${API_BASE_URL}/transferstatus`;

export default function TransferStatusScreen() {
  const themeColors = useThemeColors();
  const router = useRouter();

  const [loading, setLoading] = useState(false);
  const [toast, setToast] = useState<{ message: string; type: 'success' | 'error' } | null>(null);

  // Form State
  const [locations, setLocations] = useState<any[]>([]);
  const [selectedLocation, setSelectedLocation] = useState('');
  const [locationModalVisible, setLocationModalVisible] = useState(false);
  const [locationSearchQuery, setLocationSearchQuery] = useState('');

  // Date State - defaulting to today
  const [transferDate, setTransferDate] = useState(() => {
    const d = new Date();
    const mm = String(d.getMonth() + 1).padStart(2, '0');
    const dd = String(d.getDate()).padStart(2, '0');
    const yyyy = d.getFullYear();
    return `${yyyy}-${mm}-${dd}`;
  });

  const [transferType, setTransferType] = useState<'in' | 'out'>('in');

  // Grid State
  const [gridData, setGridData] = useState<any[]>([]);
  const [selectedRowIndex, setSelectedRowIndex] = useState<number | null>(null);

  useEffect(() => {
    fetchLocations();
  }, []);

  const showToast = (message: string, type: 'success' | 'error' = 'success') => {
    setToast({ message, type });
    setTimeout(() => setToast(null), 3000);
  };

  const fetchLocations = async () => {
    try {
      const res = await fetch(`${API_URL}/locations`);
      const data = await res.json();
      if (data.success) {
        setLocations(data.data || []);
        if (data.data && data.data.length > 0) {
          setSelectedLocation(data.data[0].LocationCode);
        }
      }
    } catch (err) {
      console.error('Error fetching locations', err);
    }
  };

  const handleFetch = async (isPending = false) => {
    if (!selectedLocation) {
      showToast('Please select a branch location.', 'error');
      return;
    }

    try {
      setLoading(true);
      setGridData([]);
      setSelectedRowIndex(null);

      let url = `${API_URL}/fetch?location=${encodeURIComponent(
        selectedLocation
      )}&transferDate=${transferDate}&type=${transferType}`;

      if (isPending) {
        url = `${API_URL}/pending?location=${encodeURIComponent(selectedLocation)}&type=${transferType}`;
      }

      const res = await fetch(url);

      let data;
      const contentType = res.headers.get('content-type');
      if (contentType && contentType.indexOf('application/json') !== -1) {
        data = await res.json();
      } else {
        throw new Error(`Server returned a non-JSON response: ${res.status}`);
      }

      if (data.success) {
        if (!data.data || data.data.length === 0) {
          showToast(`No transfers found for ${selectedLocation}`, 'error');
        } else {
          setGridData(data.data);
          showToast(`Found ${data.data.length} transfer record(s)`, 'success');
        }
      } else {
        showToast(data.message || 'No transfer records found', 'error');
      }
    } catch (error: any) {
      showToast(error.message || 'An error occurred while fetching', 'error');
    } finally {
      setLoading(false);
    }
  };

  const handleClear = () => {
    setGridData([]);
    setSelectedRowIndex(null);
  };

  const handleView = () => {
    if (selectedRowIndex === null || !gridData[selectedRowIndex]) {
      showToast('Please select a transfer voucher to view details', 'error');
      return;
    }
    const transferNo = gridData[selectedRowIndex].TransferNo;

    if (transferType === 'in') {
      router.push(`/transferin?transferNo=${encodeURIComponent(transferNo)}` as any);
    } else {
      router.push(`/transferout?transferNo=${encodeURIComponent(transferNo)}` as any);
    }
  };

  const selectedLocationObj = locations.find((l) => l.LocationCode === selectedLocation);

  // Quick summary counts
  const totalRecords = gridData.length;
  const totalPcsSum = gridData.reduce((sum, it) => sum + (Number(it.TotalPCS) || 0), 0);
  const pendingCount = gridData.filter((it) => it.ToLocStatus == 0).length;
  const acceptedCount = gridData.filter((it) => it.ToLocStatus != 0).length;

  return (
    <SafeAreaView style={styles.safeArea} edges={['top']}>
      <Stack.Screen options={{ headerShown: false }} />

      {/* LUXURY ROYAL GOLD HEADER BAR */}
      <View style={styles.headerBar}>
        <View style={styles.headerLeft}>
          <TouchableOpacity style={styles.backBtn} onPress={() => router.back()} activeOpacity={0.7}>
            <Ionicons name="arrow-back" size={20} color="#3B2D17" />
          </TouchableOpacity>
          <View>
            <ThemedText style={styles.headerTitle}>Transfer Status</ThemedText>
          </View>
          {loading && <ActivityIndicator size="small" color="#B48218" style={{ marginLeft: 8 }} />}
        </View>

        <View style={styles.headerRight}>
          <TouchableOpacity
            style={[styles.actionBtn, styles.clearBtn]}
            onPress={handleClear}
            activeOpacity={0.8}
            disabled={loading}
          >
            <Ionicons name="refresh-outline" size={14} color="#5C4A28" />
            <ThemedText style={styles.clearBtnText}>Clear</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity
            style={[styles.actionBtn, styles.pendingBtn]}
            onPress={() => handleFetch(true)}
            activeOpacity={0.8}
            disabled={loading}
          >
            <Ionicons name="time-outline" size={14} color="#B48218" />
            <ThemedText style={styles.pendingBtnText}>Pending Only</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity
            style={[styles.actionBtn, styles.fetchBtn]}
            onPress={() => handleFetch(false)}
            activeOpacity={0.8}
            disabled={loading}
          >
            <Ionicons name="search-outline" size={14} color="#5C4A28" />
            <ThemedText style={styles.clearBtnText}>Query Date</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity
            style={[
              styles.actionBtn,
              styles.saveBtn,
              (selectedRowIndex === null || loading) && { opacity: 0.55 },
            ]}
            onPress={handleView}
            activeOpacity={0.8}
            disabled={selectedRowIndex === null || loading}
          >
            <Ionicons name="open-outline" size={15} color="#9E6D08" />
            <ThemedText style={styles.saveBtnText}>View Voucher</ThemedText>
          </TouchableOpacity>
        </View>
      </View>

      {/* TOAST ALERTS */}
      {toast && (
        <View style={[styles.toast, toast.type === 'error' ? styles.toastError : styles.toastSuccess]}>
          <Ionicons
            name={toast.type === 'error' ? 'alert-circle' : 'checkmark-circle'}
            size={16}
            color="#FFFFFF"
            style={{ marginRight: 6 }}
          />
          <ThemedText style={styles.toastText}>{toast.message}</ThemedText>
        </View>
      )}

      <ScrollView contentContainerStyle={styles.scrollContainer}>
        {/* CARD 1: FILTER CONTROLS & MOVEMENT DIRECTION */}
        <View style={styles.goldCard}>
          <View style={[styles.cardHeaderBanner, { justifyContent: 'flex-start' }]}>
            {/* Movement Direction Toggle Segment */}
            <View style={styles.segmentContainer}>
              <TouchableOpacity
                style={[styles.segmentBtn, transferType === 'in' && styles.segmentBtnActive]}
                onPress={() => setTransferType('in')}
                activeOpacity={0.8}
              >
                <Ionicons
                  name="arrow-down-circle-outline"
                  size={13}
                  color={transferType === 'in' ? '#FFFFFF' : '#7D5E19'}
                  style={{ marginRight: 4 }}
                />
                <ThemedText
                  style={[styles.segmentBtnText, transferType === 'in' && styles.segmentBtnTextActive]}
                >
                  Transfer In
                </ThemedText>
              </TouchableOpacity>

              <TouchableOpacity
                style={[styles.segmentBtn, transferType === 'out' && styles.segmentBtnActive]}
                onPress={() => setTransferType('out')}
                activeOpacity={0.8}
              >
                <Ionicons
                  name="arrow-up-circle-outline"
                  size={13}
                  color={transferType === 'out' ? '#FFFFFF' : '#7D5E19'}
                  style={{ marginRight: 4 }}
                />
                <ThemedText
                  style={[styles.segmentBtnText, transferType === 'out' && styles.segmentBtnTextActive]}
                >
                  Transfer Out
                </ThemedText>
              </TouchableOpacity>
            </View>
          </View>

          <View style={styles.cardBody}>
            <View style={styles.threeColumnGrid}>
              {/* Location Selector (with LOV lookup modal support) */}
              <View style={[styles.inlineFieldRow, { flex: 1.3, minWidth: 260 }]}>
                <ThemedText style={[styles.inlineFieldLabel, { width: 95 }]}>Branch Loc. *</ThemedText>
                <View style={styles.inputWithLookup}>
                  <TextInput
                    style={[styles.formInput, styles.inputAttached, { flex: 1 }]}
                    value={
                      selectedLocationObj
                        ? `${selectedLocationObj.LocationCode} - ${selectedLocationObj.LocationName}`
                        : selectedLocation
                    }
                    placeholder="Select branch..."
                    placeholderTextColor="#9E8D77"
                    editable={false}
                  />
                  <TouchableOpacity
                    style={styles.attachedLookupBtn}
                    onPress={() => {
                      setLocationSearchQuery('');
                      setLocationModalVisible(true);
                    }}
                    activeOpacity={0.7}
                  >
                    <Ionicons name="business" size={13} color="#7D5E19" />
                  </TouchableOpacity>
                </View>
              </View>

              {/* Transfer Date Input */}
              <View style={[styles.inlineFieldRow, { flex: 1, minWidth: 210 }]}>
                <ThemedText style={[styles.inlineFieldLabel, { width: 95 }]}>Transfer Date</ThemedText>
                <TextInput
                  style={[styles.formInput, { flex: 1, minWidth: 100 }]}
                  value={transferDate}
                  onChangeText={setTransferDate}
                  placeholder="YYYY-MM-DD"
                  placeholderTextColor="#9E8D77"
                />
              </View>

              {/* Action Buttons inside Card */}
              <View style={[styles.inlineFieldRow, { flex: 1, minWidth: 220, justifyContent: 'flex-start' }]}>
                <TouchableOpacity
                  style={[styles.tableActionBtn, styles.searchBtn]}
                  onPress={() => handleFetch(false)}
                  activeOpacity={0.8}
                >
                  <Ionicons name="search" size={13} color="#9E6D08" style={{ marginRight: 4 }} />
                  <ThemedText style={styles.searchBtnText}>Run Query</ThemedText>
                </TouchableOpacity>

                <TouchableOpacity
                  style={[styles.tableActionBtn, styles.pendingPillBtn, { marginLeft: 8 }]}
                  onPress={() => handleFetch(true)}
                  activeOpacity={0.8}
                >
                  <Ionicons name="alert-circle-outline" size={13} color="#B48218" style={{ marginRight: 4 }} />
                  <ThemedText style={styles.pendingPillText}>View Pending</ThemedText>
                </TouchableOpacity>
              </View>
            </View>
          </View>
        </View>

        {/* CARD 2: CONSOLIDATED AUDIT METRICS */}
        <View style={[styles.goldCard, { marginTop: 10 }]}>
          <View style={styles.cardHeaderBanner}>
            <View style={styles.headerTitleContainer}>
              <View style={styles.headerIconBadgeSmall}>
                <Ionicons name="stats-chart-outline" size={13} color="#B48218" />
              </View>
              <ThemedText style={styles.cardHeaderTitleSmall}>Transfer Status Overview</ThemedText>
            </View>
          </View>

          <View style={styles.cardBody}>
            <View style={styles.fourColumnGrid}>
              {/* Total Vouchers */}
              <View style={[styles.inlineFieldRow, { flex: 1, minWidth: 180 }]}>
                <ThemedText style={[styles.inlineFieldLabel, { width: 90 }]}>Vouchers</ThemedText>
                <TextInput
                  style={[styles.formInput, styles.readonlyInput, { flex: 1, minWidth: 70, textAlign: 'right', fontWeight: 'bold' }]}
                  value={String(totalRecords)}
                  editable={false}
                />
              </View>

              {/* Total PCS */}
              <View style={[styles.inlineFieldRow, { flex: 1, minWidth: 180 }]}>
                <ThemedText style={[styles.inlineFieldLabel, { width: 85 }]}>Total PCS</ThemedText>
                <TextInput
                  style={[styles.formInput, styles.readonlyInput, { flex: 1, minWidth: 70, textAlign: 'right' }]}
                  value={String(totalPcsSum)}
                  editable={false}
                />
              </View>

              {/* Pending Count */}
              <View style={[styles.inlineFieldRow, { flex: 1, minWidth: 190 }]}>
                <ThemedText style={[styles.inlineFieldLabel, { width: 90 }]}>Pending</ThemedText>
                <TextInput
                  style={[
                    styles.formInput,
                    styles.readonlyInput,
                    styles.highlightPending,
                    { flex: 1, minWidth: 70, textAlign: 'right', fontWeight: 'bold' },
                  ]}
                  value={String(pendingCount)}
                  editable={false}
                />
              </View>

              {/* Accepted Count */}
              <View style={[styles.inlineFieldRow, { flex: 1, minWidth: 195 }]}>
                <ThemedText style={[styles.inlineFieldLabel, { width: 95 }]}>Accepted / Done</ThemedText>
                <TextInput
                  style={[
                    styles.formInput,
                    styles.readonlyInput,
                    styles.highlightAccepted,
                    { flex: 1, minWidth: 70, textAlign: 'right', fontWeight: 'bold' },
                  ]}
                  value={String(acceptedCount)}
                  editable={false}
                />
              </View>
            </View>
          </View>
        </View>

        {/* CARD 3: AUDIT RESULTS DATA GRID */}
        <View style={[styles.goldCard, { marginTop: 10 }]}>
          <View style={styles.cardHeaderBanner}>
            <View style={styles.headerTitleContainer}>
              <View style={styles.headerIconBadgeSmall}>
                <Ionicons name="list-outline" size={13} color="#B48218" />
              </View>
              <ThemedText style={styles.cardHeaderTitleSmall}>Transit Vouchers Log</ThemedText>
              <View style={styles.counterBadge}>
                <ThemedText style={styles.counterBadgeText}>{gridData.length} records</ThemedText>
              </View>
            </View>

            <ThemedText style={styles.cardInstruction}>
              Tip: Tap a row to select, double tap or click "View Voucher" to open detail
            </ThemedText>
          </View>

          {/* Table Header */}
          <View style={styles.tableHeadRow}>
            <ThemedText style={[styles.tableHeadCell, { flex: 1.8 }]}>Transfer No</ThemedText>
            <ThemedText style={[styles.tableHeadCell, { flex: 1.4 }]}>Date</ThemedText>
            <ThemedText style={[styles.tableHeadCell, { flex: 1.6, textAlign: 'center' }]}>
              From Loc Status
            </ThemedText>
            <ThemedText style={[styles.tableHeadCell, { flex: 1.6, textAlign: 'center' }]}>
              To Loc Status
            </ThemedText>
            <ThemedText style={[styles.tableHeadCell, { flex: 1.4 }]}>Target Loc</ThemedText>
            <ThemedText style={[styles.tableHeadCell, { flex: 1, textAlign: 'right' }]}>PCS</ThemedText>
            <ThemedText style={[styles.tableHeadCell, { width: 70, textAlign: 'center' }]}>Action</ThemedText>
          </View>

          {/* Table Body */}
          {gridData.length === 0 ? (
            <View style={styles.emptyTableContainer}>
              <Ionicons name="file-tray-outline" size={32} color="#D4C3A3" />
              <ThemedText style={styles.emptyTableText}>
                No transfer records found. Select your filters and click "Run Query" or "View Pending".
              </ThemedText>
            </View>
          ) : (
            gridData.map((item, index) => {
              const isSelected = selectedRowIndex === index;
              const isPending = item.ToLocStatus == 0;
              return (
                <TouchableOpacity
                  key={item.TransferNo || index}
                  style={[styles.tableRow, isSelected && styles.tableRowSelected]}
                  onPress={() => setSelectedRowIndex(index)}
                  onLongPress={handleView}
                  activeOpacity={0.8}
                >
                  <ThemedText style={[styles.tableCell, styles.codeCell, { flex: 1.8 }]}>
                    {item.TransferNo}
                  </ThemedText>
                  <ThemedText style={[styles.tableCell, { flex: 1.4 }]}>
                    {item.TransferDate ? new Date(item.TransferDate).toLocaleDateString() : '-'}
                  </ThemedText>
                  <View style={{ flex: 1.6, alignItems: 'center' }}>
                    <View style={styles.statusBadgeDispatched}>
                      <ThemedText style={styles.statusBadgeTextDispatched}>
                        {item.FromLocStatus == 2 ? 'Dispatched' : 'Created'}
                      </ThemedText>
                    </View>
                  </View>
                  <View style={{ flex: 1.6, alignItems: 'center' }}>
                    {isPending ? (
                      <View style={styles.statusBadgePending}>
                        <Ionicons name="time-outline" size={11} color="#C62828" style={{ marginRight: 3 }} />
                        <ThemedText style={styles.statusBadgeTextPending}>Pending</ThemedText>
                      </View>
                    ) : (
                      <View style={styles.statusBadgeAccepted}>
                        <Ionicons name="checkmark-circle" size={11} color="#2E7D32" style={{ marginRight: 3 }} />
                        <ThemedText style={styles.statusBadgeTextAccepted}>Accepted</ThemedText>
                      </View>
                    )}
                  </View>
                  <ThemedText style={[styles.tableCell, { flex: 1.4 }]}>
                    {item.Location || '-'}
                  </ThemedText>
                  <ThemedText style={[styles.tableCell, { flex: 1, textAlign: 'right', fontWeight: 'bold' }]}>
                    {item.TotalPCS || 0}
                  </ThemedText>
                  <View style={{ width: 70, alignItems: 'center' }}>
                    <TouchableOpacity
                      style={styles.rowActionBtn}
                      onPress={() => {
                        setSelectedRowIndex(index);
                        const tNo = item.TransferNo;
                        if (transferType === 'in') {
                          router.push(`/transferin?transferNo=${encodeURIComponent(tNo)}` as any);
                        } else {
                          router.push(`/transferout?transferNo=${encodeURIComponent(tNo)}` as any);
                        }
                      }}
                      activeOpacity={0.7}
                    >
                      <Ionicons name="eye-outline" size={14} color="#7D5E19" />
                    </TouchableOpacity>
                  </View>
                </TouchableOpacity>
              );
            })
          )}
        </View>
      </ScrollView>

      {/* LOCATION LOOKUP MODAL */}
      <Modal
        visible={locationModalVisible}
        transparent
        animationType="fade"
        onRequestClose={() => setLocationModalVisible(false)}
      >
        <View style={styles.modalOverlay}>
          <View style={styles.lovModalCard}>
            <View style={styles.modalHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="business-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.modalHeaderTitle}>Select Branch Location</ThemedText>
              </View>
              <TouchableOpacity
                onPress={() => setLocationModalVisible(false)}
                activeOpacity={0.7}
                style={styles.closeModalBtn}
              >
                <Ionicons name="close" size={18} color="#3B2D17" />
              </TouchableOpacity>
            </View>

            <View style={styles.modalSearchBox}>
              <Ionicons name="search-outline" size={16} color="#8D6409" style={{ marginRight: 6 }} />
              <TextInput
                style={styles.modalSearchInput}
                value={locationSearchQuery}
                onChangeText={setLocationSearchQuery}
                placeholder="Search branches..."
                placeholderTextColor="#9E8D77"
                autoFocus
              />
              {locationSearchQuery.length > 0 && (
                <TouchableOpacity onPress={() => setLocationSearchQuery('')}>
                  <Ionicons name="close-circle" size={16} color="#9E8D77" />
                </TouchableOpacity>
              )}
            </View>

            <View style={styles.lovHeadRow}>
              <ThemedText style={[styles.lovHeadText, { flex: 1.4 }]}>Location Code</ThemedText>
              <ThemedText style={[styles.lovHeadText, { flex: 2.2 }]}>Branch Name</ThemedText>
            </View>

            <FlatList
              data={locations.filter(
                (l) =>
                  (l.LocationCode || '').toLowerCase().includes(locationSearchQuery.toLowerCase()) ||
                  (l.LocationName || '').toLowerCase().includes(locationSearchQuery.toLowerCase())
              )}
              keyExtractor={(item) => item.LocationCode}
              renderItem={({ item }) => (
                <TouchableOpacity
                  style={styles.lovItemRow}
                  onPress={() => {
                    setSelectedLocation(item.LocationCode);
                    setLocationModalVisible(false);
                  }}
                  activeOpacity={0.7}
                >
                  <ThemedText style={[styles.lovItemText, styles.codeCell, { flex: 1.4 }]}>
                    {item.LocationCode}
                  </ThemedText>
                  <ThemedText style={[styles.lovItemText, { flex: 2.2 }]}>
                    {item.LocationName}
                  </ThemedText>
                </TouchableOpacity>
              )}
            />
          </View>
        </View>
      </Modal>
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  safeArea: {
    flex: 1,
    backgroundColor: '#FBF9F5',
  },
  scrollContainer: {
    paddingHorizontal: 12,
    paddingTop: 8,
    paddingBottom: 24,
  },

  // ===== ROYAL GOLD HEADER BAR =====
  headerBar: {
    height: 48,
    backgroundColor: '#FFFFFF',
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingHorizontal: 14,
    borderBottomWidth: 1,
    borderBottomColor: '#E8DCB8',
    elevation: 2,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.05,
    shadowRadius: 2,
  },
  headerLeft: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  backBtn: {
    padding: 6,
    marginRight: 8,
    borderRadius: 6,
    backgroundColor: '#F9F6F0',
    borderWidth: 1,
    borderColor: '#E8DCB8',
  },
  headerTitle: {
    fontSize: 15,
    fontWeight: 'bold',
    color: '#3B2D17',
    letterSpacing: 0.3,
  },
  headerSubtitle: {
    fontSize: 10,
    color: '#8D6409',
    fontWeight: '500',
    marginTop: -1,
  },
  headerRight: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 8,
  },
  actionBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 12,
    paddingVertical: 5,
    borderRadius: 6,
    borderWidth: 1,
  },
  clearBtn: {
    backgroundColor: '#FFFFFF',
    borderColor: '#DFD5C3',
  },
  clearBtnText: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#5C4A28',
    marginLeft: 4,
  },
  pendingBtn: {
    backgroundColor: '#FFF9E6',
    borderColor: '#B8903B',
  },
  pendingBtnText: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#8D6409',
    marginLeft: 4,
  },
  fetchBtn: {
    backgroundColor: '#FFFFFF',
    borderColor: '#DFD5C3',
  },
  saveBtn: {
    backgroundColor: '#F9F2E2',
    borderColor: '#B8903B',
  },
  saveBtnText: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#9E6D08',
    marginLeft: 4,
  },

  // ===== CARDS & BANNERS =====
  goldCard: {
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    overflow: 'hidden',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.04,
    shadowRadius: 3,
    elevation: 2,
  },
  cardHeaderBanner: {
    backgroundColor: '#F5E7CA',
    paddingHorizontal: 12,
    paddingVertical: 7,
    borderBottomWidth: 1,
    borderBottomColor: '#E8DCB8',
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    flexWrap: 'wrap',
    gap: 6,
  },
  headerTitleContainer: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  headerIconBadgeSmall: {
    width: 22,
    height: 22,
    borderRadius: 4,
    backgroundColor: '#FFFFFF',
    justifyContent: 'center',
    alignItems: 'center',
    marginRight: 6,
    borderWidth: 1,
    borderColor: '#E8DCB8',
  },
  cardHeaderTitleSmall: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#3B2D17',
    letterSpacing: 0.3,
  },
  cardInstruction: {
    fontSize: 10,
    color: '#7D5E19',
    fontStyle: 'italic',
  },
  cardBody: {
    padding: 10,
  },

  // Movement Direction Segment
  segmentContainer: {
    flexDirection: 'row',
    backgroundColor: '#EBD8B0',
    borderRadius: 6,
    padding: 2,
    gap: 3,
  },
  segmentBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 10,
    paddingVertical: 3,
    borderRadius: 4,
  },
  segmentBtnActive: {
    backgroundColor: '#B48218',
  },
  segmentBtnText: {
    fontSize: 11,
    fontWeight: '600',
    color: '#5C4A28',
  },
  segmentBtnTextActive: {
    color: '#FFFFFF',
    fontWeight: 'bold',
  },

  // ===== GRIDS & INLINE ROWS =====
  threeColumnGrid: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    gap: 12,
    alignItems: 'center',
  },
  fourColumnGrid: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    gap: 12,
  },
  inlineFieldRow: {
    flex: 1,
    minWidth: 215,
    flexDirection: 'row',
    alignItems: 'center',
  },
  inlineFieldLabel: {
    width: 95,
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3B2D17',
    marginRight: 8,
    flexShrink: 0,
  },
  formInput: {
    height: 26,
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#D4C3A3',
    borderRadius: 4,
    paddingHorizontal: 6,
    paddingVertical: 2,
    fontSize: 12,
    color: '#2B2118',
  },
  readonlyInput: {
    backgroundColor: '#F5EFE6',
    color: '#5C4A28',
  },
  highlightPending: {
    backgroundColor: '#FFEBEE',
    color: '#C62828',
    borderColor: '#FFCDD2',
  },
  highlightAccepted: {
    backgroundColor: '#E8F5E9',
    color: '#2E7D32',
    borderColor: '#A5D6A7',
  },

  // Input Combo
  inputWithLookup: {
    flex: 1,
    flexDirection: 'row',
    alignItems: 'center',
  },
  inputAttached: {
    borderTopRightRadius: 0,
    borderBottomRightRadius: 0,
  },
  attachedLookupBtn: {
    height: 26,
    width: 26,
    backgroundColor: '#F5E7CA',
    borderWidth: 1,
    borderLeftWidth: 0,
    borderColor: '#D4C3A3',
    borderTopRightRadius: 4,
    borderBottomRightRadius: 4,
    justifyContent: 'center',
    alignItems: 'center',
    flexShrink: 0,
  },

  // Actions
  tableActionBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 12,
    paddingVertical: 5,
    borderRadius: 5,
    borderWidth: 1,
    height: 26,
  },
  searchBtn: {
    backgroundColor: '#F9F2E2',
    borderColor: '#B8903B',
  },
  searchBtnText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#9E6D08',
  },
  pendingPillBtn: {
    backgroundColor: '#FFF8E7',
    borderColor: '#D4C3A3',
  },
  pendingPillText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#7D5E19',
  },

  // ===== TABLE SECTION =====
  counterBadge: {
    backgroundColor: '#FFFFFF',
    paddingHorizontal: 6,
    paddingVertical: 2,
    borderRadius: 10,
    marginLeft: 6,
    borderWidth: 1,
    borderColor: '#E2D3B3',
  },
  counterBadgeText: {
    fontSize: 10,
    fontWeight: 'bold',
    color: '#8D6409',
  },
  tableHeadRow: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FAF5EA',
    borderBottomWidth: 1,
    borderBottomColor: '#E8DCB8',
    paddingHorizontal: 12,
    paddingVertical: 5,
  },
  tableHeadCell: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#5C4A28',
    textTransform: 'uppercase',
    letterSpacing: 0.3,
  },
  tableRow: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 12,
    paddingVertical: 6,
    borderBottomWidth: 1,
    borderBottomColor: '#F0E7D5',
    backgroundColor: '#FFFFFF',
  },
  tableRowSelected: {
    backgroundColor: '#FFF8E7',
  },
  tableCell: {
    fontSize: 11,
    color: '#3B2D17',
  },
  codeCell: {
    fontWeight: 'bold',
    color: '#8D6409',
  },
  statusBadgeDispatched: {
    backgroundColor: '#EFF6FF',
    paddingHorizontal: 6,
    paddingVertical: 2,
    borderRadius: 4,
    borderWidth: 1,
    borderColor: '#BFDBFE',
  },
  statusBadgeTextDispatched: {
    fontSize: 10,
    fontWeight: '600',
    color: '#1D4ED8',
  },
  statusBadgePending: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFEBEE',
    paddingHorizontal: 6,
    paddingVertical: 2,
    borderRadius: 4,
    borderWidth: 1,
    borderColor: '#FFCDD2',
  },
  statusBadgeTextPending: {
    fontSize: 10,
    fontWeight: 'bold',
    color: '#C62828',
  },
  statusBadgeAccepted: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#E8F5E9',
    paddingHorizontal: 6,
    paddingVertical: 2,
    borderRadius: 4,
    borderWidth: 1,
    borderColor: '#A5D6A7',
  },
  statusBadgeTextAccepted: {
    fontSize: 10,
    fontWeight: 'bold',
    color: '#2E7D32',
  },
  rowActionBtn: {
    paddingHorizontal: 8,
    paddingVertical: 4,
    backgroundColor: '#FAF5EA',
    borderRadius: 4,
    borderWidth: 1,
    borderColor: '#DFD5C3',
  },
  emptyTableContainer: {
    paddingVertical: 32,
    alignItems: 'center',
    justifyContent: 'center',
    width: '100%',
  },
  emptyTableText: {
    fontSize: 12,
    color: '#9E8D77',
    marginTop: 6,
    textAlign: 'center',
    maxWidth: 420,
  },

  // ===== LOV MODALS =====
  modalOverlay: {
    flex: 1,
    backgroundColor: 'rgba(0, 0, 0, 0.45)',
    justifyContent: 'center',
    alignItems: 'center',
    padding: 16,
  },
  lovModalCard: {
    width: '92%',
    maxWidth: 600,
    maxHeight: '80%',
    backgroundColor: '#FFFFFF',
    borderRadius: 10,
    overflow: 'hidden',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.15,
    shadowRadius: 10,
    elevation: 8,
  },
  modalHeaderBanner: {
    backgroundColor: '#F5E7CA',
    paddingHorizontal: 14,
    paddingVertical: 10,
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    borderBottomWidth: 1,
    borderBottomColor: '#E8DCB8',
  },
  modalHeaderTitle: {
    fontSize: 13,
    fontWeight: 'bold',
    color: '#3B2D17',
  },
  closeModalBtn: {
    padding: 4,
  },
  modalSearchBox: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FCFAF6',
    borderBottomWidth: 1,
    borderBottomColor: '#E8DCB8',
    paddingHorizontal: 12,
    paddingVertical: 6,
  },
  modalSearchInput: {
    flex: 1,
    height: 30,
    fontSize: 12,
    color: '#3B2D17',
  },
  lovHeadRow: {
    flexDirection: 'row',
    backgroundColor: '#FAF5EA',
    paddingHorizontal: 14,
    paddingVertical: 6,
    borderBottomWidth: 1,
    borderBottomColor: '#E8DCB8',
  },
  lovHeadText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#7D5E19',
    textTransform: 'uppercase',
  },
  lovItemRow: {
    flexDirection: 'row',
    paddingHorizontal: 14,
    paddingVertical: 8,
    borderBottomWidth: 1,
    borderBottomColor: '#F5EFE6',
  },
  lovItemText: {
    fontSize: 12,
    color: '#3B2D17',
  },

  // Toasts
  toast: {
    position: 'absolute',
    top: 54,
    alignSelf: 'center',
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 16,
    paddingVertical: 8,
    borderRadius: 6,
    elevation: 4,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.15,
    shadowRadius: 4,
    zIndex: 9999,
  },
  toastSuccess: {
    backgroundColor: '#2E7D32',
  },
  toastError: {
    backgroundColor: '#C62828',
  },
  toastText: {
    color: '#FFFFFF',
    fontSize: 12,
    fontWeight: '600',
  },
});
