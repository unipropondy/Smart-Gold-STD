import { ThemedText } from '@/components/themed-text';
import { API_BASE_URL } from '@/constants/api';
import { useThemeColors } from '@/hooks/useThemeColors';
import { Ionicons } from '@expo/vector-icons';
import { Stack, useRouter } from 'expo-router';
import React, { useEffect, useRef, useState } from 'react';
import {
  ActivityIndicator,
  FlatList,
  Modal,
  ScrollView,
  StyleSheet,
  TextInput,
  TouchableOpacity,
  View
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';

const API_URL = `${API_BASE_URL}/transferstatus`;

interface TransferRecord {
  TransferNo: string;
  TransferDate: string;
  FromLocation: string;
  ToLocation: string;
  Location: string;
  FromLocStatus: number | string;
  ToLocStatus: number | string;
  TotalPCS: number;
  TotalWeight: number;
  TotalCost: number;
  Remarks: string;
}

interface DetailLineItem {
  InventoryCode: string;
  Description: string;
  ClassCode: string;
  CategoryCode: string;
  Weight: number;
  SizeCode: string;
  QtyOnHand: number;
  CostPerGram: number;
  PMC: number;
  SMC: number;
  TotalCost: number;
}

export default function TransferStatusScreen() {
  const themeColors = useThemeColors();
  const router = useRouter();

  const [loading, setLoading] = useState(false);
  const [toast, setToast] = useState<{ message: string; type: 'success' | 'error' | 'warning' } | null>(null);

  // Form State
  const [locations, setLocations] = useState<any[]>([]);
  const [selectedLocation, setSelectedLocation] = useState('HQ');
  const [locationModalVisible, setLocationModalVisible] = useState(false);
  const [locationSearchQuery, setLocationSearchQuery] = useState('');

  // Date State matching txtTransferDate, txtTransferMonth, txtTransferYear in frmTransferList.vb
  const today = new Date();
  const [transferDay, setTransferDay] = useState(String(today.getDate()).padStart(2, '0'));
  const [transferMonth, setTransferMonth] = useState(String(today.getMonth() + 1).padStart(2, '0'));
  const [transferYear, setTransferYear] = useState(String(today.getFullYear()));

  // Direction: rbtnTransferIn (checked by default in VB Designer) / rbtnTransferout
  const [transferType, setTransferType] = useState<'in' | 'out'>('in');

  // Grid State
  const [gridData, setGridData] = useState<TransferRecord[]>([]);
  const [selectedRowIndex, setSelectedRowIndex] = useState<number | null>(null);

  // Detail Modal for previewing lines
  const [previewModalVisible, setPreviewModalVisible] = useState(false);
  const [previewLines, setPreviewLines] = useState<DetailLineItem[]>([]);
  const [loadingLines, setLoadingLines] = useState(false);
  const [previewVoucher, setPreviewVoucher] = useState<TransferRecord | null>(null);

  const dayInputRef = useRef<TextInput>(null);
  const monthInputRef = useRef<TextInput>(null);
  const yearInputRef = useRef<TextInput>(null);

  useEffect(() => {
    fetchLocations();
  }, []);

  const showToast = (message: string, type: 'success' | 'error' | 'warning' = 'success') => {
    setToast({ message, type });
    setTimeout(() => setToast(null), 3500);
  };

  const fetchLocations = async () => {
    try {
      const res = await fetch(`${API_URL}/locations`);
      const data = await res.json();
      if (data.success && data.data && data.data.length > 0) {
        setLocations(data.data);
        // Default to HQ or first location
        const hasHQ = data.data.find((l: any) => l.LocationCode.trim().toUpperCase() === 'HQ');
        if (hasHQ) {
          setSelectedLocation('HQ');
        } else {
          setSelectedLocation(data.data[0].LocationCode);
        }
      }
    } catch (err) {
      console.error('Error fetching locations', err);
    }
  };

  // Keyboard Shortcuts (F6=Clear, Esc=Back)
  useEffect(() => {
    const handleKeyDown = (e: KeyboardEvent) => {
      if (e.key === 'F6') {
        e.preventDefault();
        handleClear();
      }
      if (e.key === 'Escape') {
        e.preventDefault();
        if (locationModalVisible) setLocationModalVisible(false);
        else if (previewModalVisible) setPreviewModalVisible(false);
        else router.back();
      }
    };
    if (typeof window !== 'undefined') {
      window.addEventListener('keydown', handleKeyDown);
      return () => window.removeEventListener('keydown', handleKeyDown);
    }
  }, [locationModalVisible, previewModalVisible]);

  // Validation function matching fncCheckData in frmTransferList.vb
  const fncCheckData = (isPending: boolean): boolean => {
    if (!selectedLocation || !selectedLocation.trim()) {
      showToast('Please select location.', 'error');
      return false;
    }
    if (!transferType) {
      showToast('Please select Transfer In / Transfer Out', 'error');
      return false;
    }
    if (!isPending) {
      if (!transferDay.trim() || !transferMonth.trim() || !transferYear.trim()) {
        showToast('Please enter date.', 'error');
        return false;
      }
      const d = parseInt(transferDay, 10);
      const m = parseInt(transferMonth, 10);
      const y = parseInt(transferYear, 10);
      if (isNaN(d) || isNaN(m) || isNaN(y) || d < 1 || d > 31 || m < 1 || m > 12 || y < 1900) {
        showToast('Please enter valid date.', 'error');
        return false;
      }
    }
    return true;
  };

  // Query Execution (Matching cmdFetch_Click & btnPending_Click in frmTransferList.vb)
  const handleFetch = async (isPending = false) => {
    if (!fncCheckData(isPending)) return;

    try {
      setLoading(true);
      setGridData([]);
      setSelectedRowIndex(null);

      const formattedDate = `${transferYear}-${transferMonth.padStart(2, '0')}-${transferDay.padStart(2, '0')}`;

      let url = `${API_URL}/fetch?location=${encodeURIComponent(
        selectedLocation.trim()
      )}&transferDate=${encodeURIComponent(formattedDate)}&type=${transferType}`;

      if (isPending) {
        url = `${API_URL}/pending?location=${encodeURIComponent(
          selectedLocation.trim()
        )}&type=${transferType}`;
      }

      const res = await fetch(url);
      const data = await res.json();

      if (data.success) {
        if (!data.data || data.data.length === 0) {
          showToast(`No transfer ${transferType === 'out' ? 'from' : 'to'} ${selectedLocation}`, 'warning');
        } else {
          setGridData(data.data);
          showToast(`Loaded ${data.data.length} transfer voucher(s)`, 'success');
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

  // Clear function matching cmdClear_Click in frmTransferList.vb
  const handleClear = () => {
    setTransferDay('');
    setTransferMonth('');
    setTransferYear('');
    setGridData([]);
    setSelectedRowIndex(null);
    showToast('Transfer list cleared', 'success');
  };

  // Reset to today
  const handleSetToday = () => {
    const cur = new Date();
    setTransferDay(String(cur.getDate()).padStart(2, '0'));
    setTransferMonth(String(cur.getMonth() + 1).padStart(2, '0'));
    setTransferYear(String(cur.getFullYear()));
  };

  // View Voucher (matching btnView_Click & subTransferDetails in frmTransferList.vb)
  const handleView = () => {
    if (selectedRowIndex === null || !gridData[selectedRowIndex]) {
      showToast('Please select a transfer voucher from the list', 'error');
      return;
    }
    const item = gridData[selectedRowIndex];
    const transferNo = item.TransferNo;

    if (transferType === 'in') {
      router.push(`/transferin?transferNo=${encodeURIComponent(transferNo)}` as any);
    } else {
      router.push(`/transferout?transferNo=${encodeURIComponent(transferNo)}` as any);
    }
  };

  // Quick inspect line items modal
  const handleInspectLines = async (voucher: TransferRecord) => {
    setPreviewVoucher(voucher);
    setPreviewModalVisible(true);
    setLoadingLines(true);
    try {
      const res = await fetch(`${API_URL}/details/${encodeURIComponent(voucher.TransferNo)}`);
      const data = await res.json();
      if (data.success) {
        setPreviewLines(data.details || []);
      } else {
        showToast(data.message || 'Could not load line items', 'error');
      }
    } catch (e: any) {
      showToast('Error loading line details', 'error');
    } finally {
      setLoadingLines(false);
    }
  };

  const selectedLocationObj = locations.find((l) => l.LocationCode.trim() === selectedLocation.trim());

  // Metrics
  const totalRecords = gridData.length;
  const totalPcsSum = gridData.reduce((sum, it) => sum + (Number(it.TotalPCS) || 0), 0);
  const totalWeightSum = gridData.reduce((sum, it) => sum + (Number(it.TotalWeight) || 0), 0);
  const pendingCount = gridData.filter(
    (it) => it.ToLocStatus == 0 || String(it.ToLocStatus).trim() === '0'
  ).length;
  const acceptedCount = gridData.filter(
    (it) => it.ToLocStatus != 0 && String(it.ToLocStatus).trim() !== '0'
  ).length;

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
            <ThemedText style={styles.headerTitle}>Branch Stock Movement Log</ThemedText>
            <ThemedText style={styles.headerSubtitle}>
              Transfer Status, Inter-Branch Tracking & Dispatch Audit
            </ThemedText>
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
        <View
          style={[
            styles.toast,
            toast.type === 'error'
              ? styles.toastError
              : toast.type === 'warning'
                ? styles.toastWarning
                : styles.toastSuccess,
          ]}
        >
          <Ionicons
            name={
              toast.type === 'error'
                ? 'alert-circle'
                : toast.type === 'warning'
                  ? 'warning-outline'
                  : 'checkmark-circle'
            }
            size={16}
            color="#FFFFFF"
            style={{ marginRight: 6 }}
          />
          <ThemedText style={styles.toastText}>{toast.message}</ThemedText>
        </View>
      )}

      <ScrollView contentContainerStyle={styles.scrollContainer} keyboardShouldPersistTaps="handled">
        {/* CARD 1: FILTER CONTROLS & MOVEMENT DIRECTION */}
        <View style={styles.goldCard}>
          <View style={styles.cardHeaderBanner}>
            <View style={styles.headerTitleContainer}>
              <View style={styles.headerIconBadgeSmall}>
                <Ionicons name="funnel-outline" size={13} color="#B48218" />
              </View>
              <ThemedText style={styles.cardHeaderTitleSmall}>Query Filters & Movement Direction</ThemedText>
            </View>

            {/* Movement Direction Toggle Segment (rbtnTransferIn vs rbtnTransferout) */}
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
                <ThemedText style={[styles.inlineFieldLabel, { width: 95 }]}>Location *</ThemedText>
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

              {/* Transfer Date Input (DD / MM / YYYY) */}
              <View style={[styles.inlineFieldRow, { flex: 1.2, minWidth: 240 }]}>
                <ThemedText style={[styles.inlineFieldLabel, { width: 95 }]}>Transfer Date</ThemedText>
                <View style={styles.dateTripleInputGroup}>
                  <TextInput
                    ref={dayInputRef}
                    style={[styles.formInput, styles.datePartInput]}
                    value={transferDay}
                    onChangeText={(txt) => {
                      setTransferDay(txt);
                      if (txt.length === 2) monthInputRef.current?.focus();
                    }}
                    maxLength={2}
                    keyboardType="number-pad"
                    placeholder="DD"
                    placeholderTextColor="#9E8D77"
                  />
                  <ThemedText style={styles.dateSlash}>/</ThemedText>
                  <TextInput
                    ref={monthInputRef}
                    style={[styles.formInput, styles.datePartInput]}
                    value={transferMonth}
                    onChangeText={(txt) => {
                      setTransferMonth(txt);
                      if (txt.length === 2) yearInputRef.current?.focus();
                    }}
                    maxLength={2}
                    keyboardType="number-pad"
                    placeholder="MM"
                    placeholderTextColor="#9E8D77"
                  />
                  <ThemedText style={styles.dateSlash}>/</ThemedText>
                  <TextInput
                    ref={yearInputRef}
                    style={[styles.formInput, styles.dateYearInput]}
                    value={transferYear}
                    onChangeText={setTransferYear}
                    maxLength={4}
                    keyboardType="number-pad"
                    placeholder="YYYY"
                    placeholderTextColor="#9E8D77"
                  />
                  <TouchableOpacity
                    style={styles.todaySmallBtn}
                    onPress={handleSetToday}
                    activeOpacity={0.7}
                    accessibilityLabel="Set Today"
                  >
                    <Ionicons name="calendar-outline" size={12} color="#7D5E19" />
                  </TouchableOpacity>
                </View>
              </View>

              {/* Action Buttons inside Card */}
              <View style={[styles.inlineFieldRow, { flex: 1, minWidth: 220, justifyContent: 'flex-start' }]}>
                <TouchableOpacity
                  style={[styles.tableActionBtn, styles.searchBtn]}
                  onPress={() => handleFetch(false)}
                  activeOpacity={0.8}
                >
                  <Ionicons name="search" size={13} color="#9E6D08" style={{ marginRight: 4 }} />
                  <ThemedText style={styles.searchBtnText}>Fetch Date</ThemedText>
                </TouchableOpacity>

                <TouchableOpacity
                  style={[styles.tableActionBtn, styles.pendingPillBtn, { marginLeft: 8 }]}
                  onPress={() => handleFetch(true)}
                  activeOpacity={0.8}
                >
                  <Ionicons name="time-outline" size={13} color="#B48218" style={{ marginRight: 4 }} />
                  <ThemedText style={styles.pendingPillText}>Pending</ThemedText>
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

              {/* Total Weight */}
              <View style={[styles.inlineFieldRow, { flex: 1, minWidth: 180 }]}>
                <ThemedText style={[styles.inlineFieldLabel, { width: 85 }]}>Total Wt (g)</ThemedText>
                <TextInput
                  style={[styles.formInput, styles.readonlyInput, { flex: 1, minWidth: 70, textAlign: 'right' }]}
                  value={totalWeightSum.toFixed(3)}
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
              Tip: Tap a row to select, double tap to open detail voucher
            </ThemedText>
          </View>

          {/* Table Header matching colTransferNo, colTransferDate, colFromLoc, colToLoc, colLocation, colNoofQty, ColNoofItems */}
          <View style={styles.tableHeadRow}>
            <ThemedText style={[styles.tableHeadCell, { flex: 1.8 }]}>Transfer No</ThemedText>
            <ThemedText style={[styles.tableHeadCell, { flex: 1.3 }]}>Date</ThemedText>
            <ThemedText style={[styles.tableHeadCell, { flex: 1.5, textAlign: 'center' }]}>
              From Loc. Status
            </ThemedText>
            <ThemedText style={[styles.tableHeadCell, { flex: 1.5, textAlign: 'center' }]}>
              To Loc. Status
            </ThemedText>
            <ThemedText style={[styles.tableHeadCell, { flex: 1.3 }]}>Location</ThemedText>
            <ThemedText style={[styles.tableHeadCell, { flex: 1, textAlign: 'right' }]}>No. of PCS</ThemedText>
            <ThemedText style={[styles.tableHeadCell, { flex: 1, textAlign: 'right' }]}>No of Items</ThemedText>
          </View>

          {/* Table Body */}
          {gridData.length === 0 ? (
            <View style={styles.emptyTableContainer}>
              <Ionicons name="file-tray-outline" size={32} color="#D4C3A3" />
              <ThemedText style={styles.emptyTableText}>
                No transfer records found. Select your filters and click "Fetch Date" or "Pending".
              </ThemedText>
            </View>
          ) : (
            gridData.map((item, index) => {
              const isSelected = selectedRowIndex === index;
              const isPending = item.ToLocStatus == 0 || String(item.ToLocStatus).trim() === '0';
              const isDispatched = item.FromLocStatus == 2 || String(item.FromLocStatus).trim() === '2';

              return (
                <TouchableOpacity
                  key={`${item.TransferNo}-${index}`}
                  style={[styles.tableRow, isSelected && styles.tableRowSelected]}
                  onPress={() => setSelectedRowIndex(index)}
                  onLongPress={handleView}
                  onPressIn={() => setSelectedRowIndex(index)}
                  activeOpacity={0.8}
                >
                  <ThemedText style={[styles.tableCell, styles.codeCell, { flex: 1.8 }]}>
                    {item.TransferNo}
                  </ThemedText>
                  <ThemedText style={[styles.tableCell, { flex: 1.3 }]}>
                    {item.TransferDate ? String(item.TransferDate).substring(0, 10) : '-'}
                  </ThemedText>
                  <View style={{ flex: 1.5, alignItems: 'center' }}>
                    <View style={styles.statusBadgeDispatched}>
                      <ThemedText style={styles.statusBadgeTextDispatched}>
                        {isDispatched ? 'Transferred' : 'Created'}
                      </ThemedText>
                    </View>
                  </View>
                  <View style={{ flex: 1.5, alignItems: 'center' }}>
                    {isPending ? (
                      <View style={styles.statusBadgePending}>
                        <Ionicons name="time-outline" size={11} color="#C62828" style={{ marginRight: 3 }} />
                        <ThemedText style={styles.statusBadgeTextPending}>Pending...</ThemedText>
                      </View>
                    ) : (
                      <View style={styles.statusBadgeAccepted}>
                        <Ionicons name="checkmark-circle" size={11} color="#2E7D32" style={{ marginRight: 3 }} />
                        <ThemedText style={styles.statusBadgeTextAccepted}>Accepted</ThemedText>
                      </View>
                    )}
                  </View>
                  <ThemedText style={[styles.tableCell, { flex: 1.3 }]}>
                    {item.Location || '-'}
                  </ThemedText>
                  <ThemedText style={[styles.tableCell, { flex: 1, textAlign: 'right', fontWeight: 'bold' }]}>
                    {item.TotalPCS || 0}
                  </ThemedText>
                  <ThemedText style={[styles.tableCell, { flex: 1, textAlign: 'right' }]}>
                    {item.TotalPCS || 0}
                  </ThemedText>
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

      {/* INSPECT LINE ITEMS MODAL */}
      <Modal
        visible={previewModalVisible}
        transparent
        animationType="fade"
        onRequestClose={() => setPreviewModalVisible(false)}
      >
        <View style={styles.modalOverlay}>
          <View style={[styles.lovModalCard, { maxWidth: 760, maxHeight: 520 }]}>
            <View style={styles.modalHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="cube-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.modalHeaderTitle}>
                  Voucher Items: #{previewVoucher?.TransferNo}
                </ThemedText>
              </View>
              <TouchableOpacity
                onPress={() => setPreviewModalVisible(false)}
                activeOpacity={0.7}
                style={styles.closeModalBtn}
              >
                <Ionicons name="close" size={18} color="#3B2D17" />
              </TouchableOpacity>
            </View>

            <View style={{ padding: 10, backgroundColor: '#FAF7F0', borderBottomWidth: 1, borderBottomColor: '#E8DCB8' }}>
              <ThemedText style={{ fontSize: 11, color: '#4A3B22' }}>
                From: <ThemedText style={{ fontWeight: 'bold' }}>{previewVoucher?.FromLocation}</ThemedText> → To: <ThemedText style={{ fontWeight: 'bold' }}>{previewVoucher?.ToLocation}</ThemedText> | Total PCS: <ThemedText style={{ fontWeight: 'bold' }}>{previewVoucher?.TotalPCS}</ThemedText>
              </ThemedText>
              {Boolean(previewVoucher?.Remarks) && (
                <ThemedText style={{ fontSize: 10, color: '#7D5E19', fontStyle: 'italic', marginTop: 2 }}>
                  Remarks: {previewVoucher?.Remarks}
                </ThemedText>
              )}
            </View>

            {/* Subtable Header */}
            <View style={styles.lovHeadRow}>
              <ThemedText style={[styles.lovHeadText, { flex: 1.8 }]}>Item Code</ThemedText>
              <ThemedText style={[styles.lovHeadText, { flex: 2.6 }]}>Description</ThemedText>
              <ThemedText style={[styles.lovHeadText, { flex: 1, textAlign: 'right' }]}>Weight</ThemedText>
              <ThemedText style={[styles.lovHeadText, { flex: 0.8, textAlign: 'center' }]}>Size</ThemedText>
              <ThemedText style={[styles.lovHeadText, { flex: 1.2, textAlign: 'right' }]}>Total Cost</ThemedText>
            </View>

            <ScrollView style={{ flex: 1 }}>
              {loadingLines ? (
                <View style={{ padding: 24, alignItems: 'center' }}>
                  <ActivityIndicator size="small" color="#B48218" />
                  <ThemedText style={{ color: '#7D5E19', fontSize: 12, marginTop: 8 }}>Loading items...</ThemedText>
                </View>
              ) : previewLines.length === 0 ? (
                <View style={{ padding: 24, alignItems: 'center' }}>
                  <ThemedText style={{ color: '#7D5E19', fontSize: 12 }}>No items found in detail</ThemedText>
                </View>
              ) : (
                previewLines.map((line, idx) => (
                  <View key={`${line.InventoryCode}-${idx}`} style={styles.lovItemRow}>
                    <ThemedText style={[styles.lovItemText, styles.codeCell, { flex: 1.8 }]}>
                      {line.InventoryCode}
                    </ThemedText>
                    <ThemedText style={[styles.lovItemText, { flex: 2.6 }]} numberOfLines={1}>
                      {line.Description || '-'}
                    </ThemedText>
                    <ThemedText style={[styles.lovItemText, { flex: 1, textAlign: 'right', fontWeight: 'bold' }]}>
                      {Number(line.Weight || 0).toFixed(3)}g
                    </ThemedText>
                    <ThemedText style={[styles.lovItemText, { flex: 0.8, textAlign: 'center' }]}>
                      {line.SizeCode || '-'}
                    </ThemedText>
                    <ThemedText style={[styles.lovItemText, { flex: 1.2, textAlign: 'right', color: '#2E7D32', fontWeight: 'bold' }]}>
                      ${Number(line.TotalCost || 0).toFixed(2)}
                    </ThemedText>
                  </View>
                ))
              )}
            </ScrollView>

            <View style={{ padding: 10, borderTopWidth: 1, borderTopColor: '#E8DCB8', flexDirection: 'row', justifyContent: 'flex-end', gap: 8 }}>
              <TouchableOpacity
                style={[styles.actionBtn, styles.clearBtn]}
                onPress={() => setPreviewModalVisible(false)}
              >
                <ThemedText style={styles.clearBtnText}>Close</ThemedText>
              </TouchableOpacity>
              <TouchableOpacity
                style={[styles.actionBtn, styles.saveBtn]}
                onPress={() => {
                  setPreviewModalVisible(false);
                  if (previewVoucher) {
                    const tNo = previewVoucher.TransferNo;
                    if (transferType === 'in') {
                      router.push(`/transferin?transferNo=${encodeURIComponent(tNo)}` as any);
                    } else {
                      router.push(`/transferout?transferNo=${encodeURIComponent(tNo)}` as any);
                    }
                  }
                }}
              >
                <Ionicons name="open-outline" size={14} color="#9E6D08" style={{ marginRight: 4 }} />
                <ThemedText style={styles.saveBtnText}>Open in Editor</ThemedText>
              </TouchableOpacity>
            </View>
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
    backgroundColor: '#FFF8E7',
    borderColor: '#E2CA8E',
  },
  pendingBtnText: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#9E6D08',
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
  counterBadge: {
    backgroundColor: '#FFFFFF',
    paddingHorizontal: 6,
    paddingVertical: 1,
    borderRadius: 10,
    marginLeft: 6,
    borderWidth: 1,
    borderColor: '#E8DCB8',
  },
  counterBadgeText: {
    fontSize: 10,
    fontWeight: 'bold',
    color: '#9E6D08',
  },
  cardBody: {
    padding: 10,
  },

  // ===== SEGMENTED CONTROL =====
  segmentContainer: {
    flexDirection: 'row',
    backgroundColor: '#EDE4D1',
    borderRadius: 6,
    padding: 2,
    borderWidth: 1,
    borderColor: '#D4C3A3',
  },
  segmentBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 10,
    paddingVertical: 4,
    borderRadius: 4,
  },
  segmentBtnActive: {
    backgroundColor: '#B48218',
  },
  segmentBtnText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#6A5636',
  },
  segmentBtnTextActive: {
    color: '#FFFFFF',
  },

  // ===== FORM GRID & INPUTS =====
  threeColumnGrid: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    gap: 8,
    alignItems: 'center',
  },
  fourColumnGrid: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    gap: 8,
  },
  inlineFieldRow: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  inlineFieldLabel: {
    fontSize: 11,
    fontWeight: '600',
    color: '#4A3B22',
  },
  formInput: {
    height: 28,
    borderWidth: 1,
    borderColor: '#D4C3A3',
    borderRadius: 4,
    paddingHorizontal: 8,
    fontSize: 11,
    color: '#2A1F10',
    backgroundColor: '#FFFFFF',
  },
  dateTripleInputGroup: {
    flexDirection: 'row',
    alignItems: 'center',
    flex: 1,
  },
  datePartInput: {
    width: 34,
    textAlign: 'center',
    paddingHorizontal: 2,
  },
  dateYearInput: {
    width: 50,
    textAlign: 'center',
    paddingHorizontal: 4,
  },
  dateSlash: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#7D5E19',
    marginHorizontal: 3,
  },
  todaySmallBtn: {
    padding: 5,
    marginLeft: 4,
    borderRadius: 4,
    backgroundColor: '#F5E7CA',
    borderWidth: 1,
    borderColor: '#D4C3A3',
  },
  inputWithLookup: {
    flexDirection: 'row',
    alignItems: 'center',
    flex: 1,
  },
  inputAttached: {
    borderTopRightRadius: 0,
    borderBottomRightRadius: 0,
    borderRightWidth: 0,
  },
  attachedLookupBtn: {
    width: 28,
    height: 28,
    backgroundColor: '#F5E7CA',
    borderWidth: 1,
    borderColor: '#D4C3A3',
    borderTopRightRadius: 4,
    borderBottomRightRadius: 4,
    justifyContent: 'center',
    alignItems: 'center',
  },
  readonlyInput: {
    backgroundColor: '#F7F3EB',
    color: '#5C4A28',
    borderColor: '#E2D5C0',
  },
  highlightPending: {
    backgroundColor: '#FFEBEE',
    borderColor: '#EF9A9A',
    color: '#C62828',
  },
  highlightAccepted: {
    backgroundColor: '#E8F5E9',
    borderColor: '#A5D6A7',
    color: '#2E7D32',
  },

  // ===== TABLE DESIGN =====
  tableHeadRow: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#F9F5EC',
    paddingVertical: 6,
    paddingHorizontal: 10,
    borderBottomWidth: 1,
    borderBottomColor: '#E8DCB8',
  },
  tableHeadCell: {
    fontSize: 10,
    fontWeight: 'bold',
    color: '#6A5636',
    textTransform: 'uppercase',
  },
  tableRow: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingVertical: 6,
    paddingHorizontal: 10,
    borderBottomWidth: 1,
    borderBottomColor: '#F0E8D8',
    backgroundColor: '#FFFFFF',
  },
  tableRowSelected: {
    backgroundColor: '#FFF8E6',
  },
  tableCell: {
    fontSize: 11,
    color: '#3B2D17',
  },
  codeCell: {
    fontWeight: 'bold',
    color: '#9E6D08',
  },
  statusBadgeDispatched: {
    backgroundColor: '#E3F2FD',
    borderColor: '#90CAF9',
    borderWidth: 1,
    paddingHorizontal: 6,
    paddingVertical: 1,
    borderRadius: 4,
  },
  statusBadgeTextDispatched: {
    fontSize: 9,
    fontWeight: 'bold',
    color: '#1565C0',
  },
  statusBadgePending: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFEBEE',
    borderColor: '#FFCDD2',
    borderWidth: 1,
    paddingHorizontal: 6,
    paddingVertical: 1,
    borderRadius: 4,
  },
  statusBadgeTextPending: {
    fontSize: 9,
    fontWeight: 'bold',
    color: '#C62828',
  },
  statusBadgeAccepted: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#E8F5E9',
    borderColor: '#A5D6A7',
    borderWidth: 1,
    paddingHorizontal: 6,
    paddingVertical: 1,
    borderRadius: 4,
  },
  statusBadgeTextAccepted: {
    fontSize: 9,
    fontWeight: 'bold',
    color: '#2E7D32',
  },
  emptyTableContainer: {
    padding: 24,
    alignItems: 'center',
    justifyContent: 'center',
  },
  emptyTableText: {
    fontSize: 11,
    color: '#9E8D77',
    textAlign: 'center',
    marginTop: 6,
  },
  rowActionBtn: {
    width: 24,
    height: 24,
    borderRadius: 4,
    backgroundColor: '#F5E7CA',
    justifyContent: 'center',
    alignItems: 'center',
    borderWidth: 1,
    borderColor: '#E8DCB8',
  },

  // ===== CARD ACTION BUTTONS =====
  tableActionBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    paddingHorizontal: 12,
    height: 28,
    borderRadius: 4,
    borderWidth: 1,
  },
  searchBtn: {
    backgroundColor: '#F5E7CA',
    borderColor: '#B8903B',
  },
  searchBtnText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#9E6D08',
  },
  pendingPillBtn: {
    backgroundColor: '#FFF8E7',
    borderColor: '#E2CA8E',
  },
  pendingPillText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#9E6D08',
  },

  // ===== TOAST NOTIFICATION =====
  toast: {
    position: 'absolute',
    top: 56,
    left: '25%',
    right: '25%',
    zIndex: 9999,
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 14,
    paddingVertical: 8,
    borderRadius: 6,
    elevation: 6,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.15,
    shadowRadius: 4,
  },
  toastSuccess: {
    backgroundColor: '#2E7D32',
  },
  toastError: {
    backgroundColor: '#D32F2F',
  },
  toastWarning: {
    backgroundColor: '#F57C00',
  },
  toastText: {
    color: '#FFFFFF',
    fontSize: 12,
    fontWeight: '600',
    flex: 1,
  },

  // ===== MODAL STYLING =====
  modalOverlay: {
    flex: 1,
    backgroundColor: 'rgba(0,0,0,0.5)',
    justifyContent: 'center',
    alignItems: 'center',
    padding: 16,
  },
  lovModalCard: {
    width: '100%',
    maxWidth: 480,
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    overflow: 'hidden',
    elevation: 8,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.15,
    shadowRadius: 8,
    maxHeight: 460,
  },
  modalHeaderBanner: {
    backgroundColor: '#F5E7CA',
    paddingHorizontal: 12,
    paddingVertical: 8,
    borderBottomWidth: 1,
    borderBottomColor: '#E8DCB8',
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
  },
  modalHeaderTitle: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#3B2D17',
  },
  closeModalBtn: {
    padding: 2,
  },
  modalSearchBox: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 10,
    paddingVertical: 6,
    borderBottomWidth: 1,
    borderBottomColor: '#E8DCB8',
    backgroundColor: '#FAF7F0',
  },
  modalSearchInput: {
    flex: 1,
    fontSize: 12,
    color: '#2A1F10',
    height: 26,
    padding: 0,
  },
  lovHeadRow: {
    flexDirection: 'row',
    backgroundColor: '#F0E8D8',
    paddingHorizontal: 12,
    paddingVertical: 6,
    borderBottomWidth: 1,
    borderBottomColor: '#E8DCB8',
  },
  lovHeadText: {
    fontSize: 10,
    fontWeight: 'bold',
    color: '#6A5636',
  },
  lovItemRow: {
    flexDirection: 'row',
    paddingHorizontal: 12,
    paddingVertical: 8,
    borderBottomWidth: 1,
    borderBottomColor: '#F5EFE0',
    alignItems: 'center',
  },
  lovItemText: {
    fontSize: 11,
    color: '#3B2D17',
  },
});
