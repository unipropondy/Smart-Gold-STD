import { ThemedText } from '@/components/themed-text';
import { API_BASE_URL } from '@/constants/api';
import { useThemeColors } from '@/hooks/useThemeColors';
import { Ionicons } from '@expo/vector-icons';
import { Stack, useRouter } from 'expo-router';
import React, { useEffect, useRef, useState } from 'react';
import {
  ActivityIndicator,
  KeyboardAvoidingView,
  Platform,
  ScrollView,
  StyleSheet,
  TextInput,
  TouchableOpacity,
  View,
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';

const API_URL = `${API_BASE_URL}/transferin`;

export default function TransferInScreen() {
  const themeColors = useThemeColors();
  const router = useRouter();

  const [loading, setLoading] = useState(false);
  const [toast, setToast] = useState<{ message: string; type: 'success' | 'error' } | null>(null);

  // Header state
  const [transferNo, setTransferNo] = useState('');
  const [transferDate, setTransferDate] = useState('');
  const [fromLocation, setFromLocation] = useState('');
  const [remarks, setRemarks] = useState('');

  // Grid state
  const [gridData, setGridData] = useState<any[]>([]);
  const [inventoryCodeInput, setInventoryCodeInput] = useState('');
  const [selectedRowIndex, setSelectedRowIndex] = useState<number | null>(null);

  const barcodeInputRef = useRef<TextInput>(null);

  const showToast = (message: string, type: 'success' | 'error' = 'success') => {
    setToast({ message, type });
    setTimeout(() => setToast(null), 3000);
  };

  const handleClear = () => {
    setTransferNo('');
    setTransferDate('');
    setFromLocation('');
    setRemarks('');
    setGridData([]);
    setInventoryCodeInput('');
    setSelectedRowIndex(null);
  };

  // --- API Calls ---
  const handleFetch = async () => {
    if (!transferNo.trim()) {
      showToast('Please enter the transfer no.', 'error');
      return;
    }

    try {
      setLoading(true);
      const res = await fetch(`${API_URL}/fetch?transferNo=${encodeURIComponent(transferNo.trim())}&location=L1`);

      let data;
      const contentType = res.headers.get('content-type');
      if (contentType && contentType.indexOf('application/json') !== -1) {
        data = await res.json();
      } else {
        throw new Error(`Server returned a non-JSON response: ${res.status} ${res.statusText}`);
      }

      if (data.success) {
        setFromLocation(data.data.header?.FromLocation || '');

        if (data.data.header?.TransferDate) {
          const dateObj = new Date(data.data.header.TransferDate);
          const formatted = `${String(dateObj.getDate()).padStart(2, '0')}/${String(
            dateObj.getMonth() + 1
          ).padStart(2, '0')}/${dateObj.getFullYear()}`;
          setTransferDate(formatted);
        }

        setRemarks(data.data.header?.Remarks || '');
        setGridData(data.data.details || []);

        showToast('Transfer dispatch docket retrieved', 'success');

        setTimeout(() => {
          barcodeInputRef.current?.focus();
        }, 150);
      } else {
        showToast(data.message || 'Transfer docket not found', 'error');
        handleClear();
      }
    } catch (err: any) {
      showToast(err.message || 'Network error fetching transfer', 'error');
    } finally {
      setLoading(false);
    }
  };

  const handleAccept = async () => {
    if (!transferNo.trim()) {
      showToast('Enter Transfer Number', 'error');
      return;
    }

    if (selectedCount !== totalCount) {
      showToast('Please scan and verify all items before accepting', 'error');
      return;
    }

    try {
      setLoading(true);
      const res = await fetch(`${API_URL}/accept`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          transferNo,
          location: 'L1',
          userId: 'admin',
          items: gridData,
        }),
      });

      let data;
      const contentType = res.headers.get('content-type');
      if (contentType && contentType.indexOf('application/json') !== -1) {
        data = await res.json();
      } else {
        throw new Error(`Server returned a non-JSON response: ${res.status} ${res.statusText}`);
      }

      if (data.success) {
        showToast('Inventory Accepted & Received Successfully', 'success');
        handleClear();
      } else {
        showToast(data.message || 'Inventory acceptance failed', 'error');
      }
    } catch (err: any) {
      showToast(err.message || 'Error executing inventory accept', 'error');
    } finally {
      setLoading(false);
    }
  };

  const handleScanInventory = () => {
    if (!inventoryCodeInput.trim()) return;

    const code = inventoryCodeInput.trim();
    const index = gridData.findIndex(
      (item) => String(item.InventoryCode).toLowerCase() === code.toLowerCase()
    );

    if (index === -1) {
      showToast(`Item ${code} not found in this transfer voucher`, 'error');
    } else {
      if (gridData[index].TransferTag) {
        showToast(`Item ${code} already scanned & verified`, 'error');
      } else {
        const newData = [...gridData];
        newData[index].TransferTag = true;
        setGridData(newData);
        setSelectedRowIndex(index);
        showToast(`Verified: ${code}`, 'success');
      }
    }

    setInventoryCodeInput('');
    setTimeout(() => {
      barcodeInputRef.current?.focus();
    }, 50);
  };

  const toggleRowSelection = (index: number) => {
    const newData = [...gridData];
    newData[index].TransferTag = !newData[index].TransferTag;
    setGridData(newData);
    setSelectedRowIndex(index);
  };

  // Keyboard Shortcuts (F6=Clear, F7=Accept, Esc=Back)
  useEffect(() => {
    const handleKeyDown = (e: KeyboardEvent) => {
      if (e.key === 'F6') {
        e.preventDefault();
        handleClear();
      }
      if (e.key === 'F7' && selectedCount === totalCount && totalCount > 0) {
        e.preventDefault();
        handleAccept();
      }
      if (e.key === 'Escape') {
        e.preventDefault();
        router.back();
      }
    };
    if (typeof window !== 'undefined') {
      window.addEventListener('keydown', handleKeyDown);
      return () => window.removeEventListener('keydown', handleKeyDown);
    }
  }, [gridData, transferNo]);

  const totalCount = gridData.length;
  const selectedCount = gridData.filter((item) => item.TransferTag).length;
  const canAccept = totalCount > 0 && totalCount === selectedCount;

  // Totals calculations
  const totalWeight = gridData.reduce((sum, item) => sum + (Number(item.Weight) || 0), 0);
  const verifiedWeight = gridData
    .filter((item) => item.TransferTag)
    .reduce((sum, item) => sum + (Number(item.Weight) || 0), 0);
  const totalCostSum = gridData.reduce((sum, item) => sum + (Number(item.TotalCost) || 0), 0);

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
            <ThemedText style={styles.headerTitle}>Branch Stock Transfer In</ThemedText>
            <ThemedText style={styles.headerSubtitle}>
              Receive, Barcode Scan & Accept Inter-Branch Inventory
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
            <ThemedText style={styles.clearBtnText}>Clear (F6)</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity
            style={[styles.actionBtn, styles.fetchBtn]}
            onPress={handleFetch}
            activeOpacity={0.8}
            disabled={loading}
          >
            <Ionicons name="cloud-download-outline" size={14} color="#5C4A28" />
            <ThemedText style={styles.clearBtnText}>Fetch</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity
            style={[styles.actionBtn, styles.saveBtn, (!canAccept || loading) && { opacity: 0.55 }]}
            onPress={handleAccept}
            activeOpacity={0.8}
            disabled={!canAccept || loading}
          >
            <Ionicons name="checkmark-done-circle-outline" size={15} color="#9E6D08" />
            <ThemedText style={styles.saveBtnText}>
              {canAccept ? 'Accept Transfer (F7)' : `Verify All (${selectedCount}/${totalCount})`}
            </ThemedText>
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

      <KeyboardAvoidingView behavior={Platform.OS === 'ios' ? 'padding' : undefined} style={{ flex: 1 }}>
        <ScrollView contentContainerStyle={styles.scrollContainer} keyboardShouldPersistTaps="handled">
          {/* CARD 1: INBOUND TRANSFER DOCKET HEADER */}
          <View style={styles.goldCard}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="document-text-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitleSmall}>Inbound Dispatch Voucher Details</ThemedText>
              </View>

              {totalCount > 0 && (
                <View style={[styles.transferBadge, canAccept && styles.transferBadgeSuccess]}>
                  <ThemedText style={styles.transferBadgeLabel}>Verification Status:</ThemedText>
                  <ThemedText style={styles.transferBadgeValue}>
                    {selectedCount} / {totalCount} Items Scanned
                  </ThemedText>
                </View>
              )}
            </View>

            <View style={styles.cardBody}>
              {/* Row 1: Transfer No + Fetch, Transfer Date, From Location, Status */}
              <View style={styles.fourColumnGrid}>
                {/* Transfer No */}
                <View style={[styles.inlineFieldRow, { flex: 1.2, minWidth: 260 }]}>
                  <ThemedText style={[styles.inlineFieldLabel, { width: 95 }]}>Transfer No *</ThemedText>
                  <View style={styles.inputWithLookup}>
                    <TextInput
                      style={[styles.formInput, styles.inputAttached, { flex: 1 }]}
                      value={transferNo}
                      onChangeText={setTransferNo}
                      onBlur={() => transferNo && handleFetch()}
                      onSubmitEditing={handleFetch}
                      placeholder="Enter Transfer No..."
                      placeholderTextColor="#9E8D77"
                    />
                    <TouchableOpacity
                      style={styles.attachedLookupBtn}
                      onPress={handleFetch}
                      activeOpacity={0.7}
                      disabled={loading}
                    >
                      <Ionicons name="search" size={13} color="#7D5E19" />
                    </TouchableOpacity>
                  </View>
                </View>

                {/* Transfer Date */}
                <View style={[styles.inlineFieldRow, { flex: 0.9, minWidth: 200 }]}>
                  <ThemedText style={[styles.inlineFieldLabel, { width: 90 }]}>Transfer Date</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1, minWidth: 90 }]}
                    value={transferDate}
                    editable={false}
                    placeholder="DD/MM/YYYY"
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* From Location */}
                <View style={[styles.inlineFieldRow, { flex: 0.9, minWidth: 190 }]}>
                  <ThemedText style={[styles.inlineFieldLabel, { width: 90 }]}>From Location</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1, minWidth: 80 }]}
                    value={fromLocation}
                    editable={false}
                    placeholder="Source Location"
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* Verification Progress Badge */}
                <View style={[styles.inlineFieldRow, { flex: 1, minWidth: 190 }]}>
                  <ThemedText style={[styles.inlineFieldLabel, { width: 85 }]}>Progress</ThemedText>
                  <View
                    style={[
                      styles.statusPill,
                      canAccept ? styles.statusPillReady : styles.statusPillPending,
                    ]}
                  >
                    <Ionicons
                      name={canAccept ? 'shield-checkmark' : 'time-outline'}
                      size={12}
                      color={canAccept ? '#2E7D32' : '#C62828'}
                      style={{ marginRight: 4 }}
                    />
                    <ThemedText
                      style={[
                        styles.statusPillText,
                        canAccept ? styles.statusPillTextReady : styles.statusPillTextPending,
                      ]}
                    >
                      {totalCount === 0
                        ? 'Awaiting Fetch'
                        : canAccept
                        ? 'Ready to Accept'
                        : `${totalCount - selectedCount} Remaining`}
                    </ThemedText>
                  </View>
                </View>
              </View>

              {/* Row 2: Remarks */}
              <View style={[styles.singleColumnRow, { marginTop: 8 }]}>
                <View style={styles.inlineFieldRow}>
                  <ThemedText style={[styles.inlineFieldLabel, { width: 95 }]}>Remarks</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1 }]}
                    value={remarks}
                    editable={false}
                    placeholder="Dispatch notes or transit instructions..."
                    placeholderTextColor="#9E8D77"
                  />
                </View>
              </View>
            </View>
          </View>

          {/* CARD 2: CONSOLIDATED INBOUND TOTALS */}
          <View style={[styles.goldCard, { marginTop: 10 }]}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="calculator-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitleSmall}>Inbound Verification Summary</ThemedText>
              </View>
            </View>

            <View style={styles.cardBody}>
              <View style={styles.fourColumnGrid}>
                {/* Total Manifest Wt */}
                <View style={[styles.inlineFieldRow, { flex: 1, minWidth: 200 }]}>
                  <ThemedText style={[styles.inlineFieldLabel, { width: 95 }]}>Manifest Wt (g)</ThemedText>
                  <TextInput
                    style={[
                      styles.formInput,
                      styles.readonlyInput,
                      { flex: 1, minWidth: 90, textAlign: 'right', fontWeight: 'bold' },
                    ]}
                    value={totalWeight.toFixed(3)}
                    editable={false}
                  />
                </View>

                {/* Verified Wt */}
                <View style={[styles.inlineFieldRow, { flex: 1, minWidth: 200 }]}>
                  <ThemedText style={[styles.inlineFieldLabel, { width: 95 }]}>Verified Wt (g)</ThemedText>
                  <TextInput
                    style={[
                      styles.formInput,
                      styles.readonlyInput,
                      styles.highlightTotal,
                      { flex: 1, minWidth: 90, textAlign: 'right', fontWeight: 'bold' },
                    ]}
                    value={verifiedWeight.toFixed(3)}
                    editable={false}
                  />
                </View>

                {/* Total Pieces */}
                <View style={[styles.inlineFieldRow, { flex: 1, minWidth: 180 }]}>
                  <ThemedText style={[styles.inlineFieldLabel, { width: 85 }]}>Total Items</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1, minWidth: 70, textAlign: 'right' }]}
                    value={`${selectedCount} / ${totalCount}`}
                    editable={false}
                  />
                </View>

                {/* Total Cost Value */}
                <View style={[styles.inlineFieldRow, { flex: 1, minWidth: 205 }]}>
                  <ThemedText style={[styles.inlineFieldLabel, { width: 95 }]}>Total Value ($)</ThemedText>
                  <TextInput
                    style={[
                      styles.formInput,
                      styles.readonlyInput,
                      styles.highlightTotal,
                      { flex: 1, minWidth: 90, textAlign: 'right', fontWeight: 'bold' },
                    ]}
                    value={totalCostSum.toFixed(2)}
                    editable={false}
                  />
                </View>
              </View>
            </View>
          </View>

          {/* CARD 3: INVENTORY ITEMS TO RECEIVE (100% FULL WIDTH) */}
          <View style={[styles.goldCard, { marginTop: 10 }]}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="cube-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitleSmall}>Incoming Items for Intake</ThemedText>
                <View style={styles.counterBadge}>
                  <ThemedText style={styles.counterBadgeText}>{gridData.length} items</ThemedText>
                </View>
              </View>

              <ThemedText style={styles.cardInstruction}>
                Tip: Click row or scan barcode to toggle verified status
              </ThemedText>
            </View>

            {/* Table Header */}
            <View style={styles.tableHeadRow}>
              <ThemedText style={[styles.tableHeadCell, { flex: 1.8 }]}>Inventory Code</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 2.6 }]}>Description</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 1, textAlign: 'right' }]}>Weight (g)</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 0.9, textAlign: 'center' }]}>Size</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 1.2, textAlign: 'right' }]}>Total Cost ($)</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { width: 90, textAlign: 'center' }]}>Verified</ThemedText>
            </View>

            {/* Table Body */}
            {gridData.length === 0 ? (
              <View style={styles.emptyTableContainer}>
                <Ionicons name="archive-outline" size={32} color="#D4C3A3" />
                <ThemedText style={styles.emptyTableText}>
                  No items loaded. Enter a Transfer No above and click "Fetch" to load incoming stock.
                </ThemedText>
              </View>
            ) : (
              gridData.map((item, index) => {
                const isSelected = selectedRowIndex === index;
                const isVerified = Boolean(item.TransferTag);
                return (
                  <TouchableOpacity
                    key={item.InventoryCode || index}
                    style={[
                      styles.tableRow,
                      isSelected && styles.tableRowSelected,
                      isVerified && styles.tableRowVerified,
                    ]}
                    onPress={() => toggleRowSelection(index)}
                    activeOpacity={0.8}
                  >
                    <ThemedText style={[styles.tableCell, styles.codeCell, { flex: 1.8 }]}>
                      {item.InventoryCode}
                    </ThemedText>
                    <ThemedText style={[styles.tableCell, { flex: 2.6 }]} numberOfLines={1}>
                      {item.Description || '-'}
                    </ThemedText>
                    <ThemedText style={[styles.tableCell, { flex: 1, textAlign: 'right', fontWeight: 'bold' }]}>
                      {Number(item.Weight || 0).toFixed(3)}
                    </ThemedText>
                    <ThemedText style={[styles.tableCell, { flex: 0.9, textAlign: 'center' }]}>
                      {item.SizeCode || '-'}
                    </ThemedText>
                    <ThemedText style={[styles.tableCell, styles.amountCell, { flex: 1.2, textAlign: 'right' }]}>
                      {Number(item.TotalCost || 0).toFixed(2)}
                    </ThemedText>
                    <View style={{ width: 90, alignItems: 'center', justifyContent: 'center' }}>
                      {isVerified ? (
                        <View style={styles.verifiedBadge}>
                          <Ionicons name="checkmark-circle" size={13} color="#2E7D32" style={{ marginRight: 3 }} />
                          <ThemedText style={styles.verifiedText}>Scanned</ThemedText>
                        </View>
                      ) : (
                        <View style={styles.pendingBadge}>
                          <Ionicons name="ellipse-outline" size={11} color="#C62828" style={{ marginRight: 3 }} />
                          <ThemedText style={styles.pendingText}>Pending</ThemedText>
                        </View>
                      )}
                    </View>
                  </TouchableOpacity>
                );
              })
            )}

            {/* SCANNER SUBFORM ENTRY BAR */}
            <View style={styles.itemEntryBar}>
              <View style={styles.itemEntryRow}>
                <View style={[styles.inlineFieldRow, { flex: 1.6, minWidth: 260 }]}>
                  <ThemedText style={[styles.inlineFieldLabel, { width: 110 }]}>Scan Barcode *</ThemedText>
                  <TextInput
                    ref={barcodeInputRef}
                    style={[styles.formInput, { flex: 1, minWidth: 120 }]}
                    value={inventoryCodeInput}
                    onChangeText={setInventoryCodeInput}
                    onSubmitEditing={handleScanInventory}
                    onBlur={handleScanInventory}
                    placeholder="Scan or enter inventory code to verify..."
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                <TouchableOpacity
                  style={[styles.tableActionBtn, styles.verifyBtn, { height: 26, paddingHorizontal: 14 }]}
                  onPress={handleScanInventory}
                  activeOpacity={0.8}
                >
                  <Ionicons name="barcode-outline" size={14} color="#9E6D08" style={{ marginRight: 4 }} />
                  <ThemedText style={styles.verifyBtnText}>Verify Item</ThemedText>
                </TouchableOpacity>

                <TouchableOpacity
                  style={[
                    styles.tableActionBtn,
                    styles.verifyAllBtn,
                    { height: 26, paddingHorizontal: 12, marginLeft: 4 },
                  ]}
                  onPress={() => {
                    const allVerified = gridData.map((it) => ({ ...it, TransferTag: true }));
                    setGridData(allVerified);
                    showToast('Marked all items as verified', 'success');
                  }}
                  activeOpacity={0.8}
                  disabled={gridData.length === 0}
                >
                  <Ionicons name="checkmark-done-outline" size={14} color="#7D5E19" style={{ marginRight: 4 }} />
                  <ThemedText style={styles.verifyAllBtnText}>Verify All</ThemedText>
                </TouchableOpacity>
              </View>
            </View>
          </View>
        </ScrollView>
      </KeyboardAvoidingView>
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
  transferBadge: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFF4DA',
    paddingHorizontal: 8,
    paddingVertical: 2,
    borderRadius: 4,
    borderWidth: 1,
    borderColor: '#B8903B',
  },
  transferBadgeSuccess: {
    backgroundColor: '#EDF7ED',
    borderColor: '#81C784',
  },
  transferBadgeLabel: {
    fontSize: 10,
    color: '#7D5E19',
    marginRight: 4,
  },
  transferBadgeValue: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#8A5D00',
  },

  // Status pills
  statusPill: {
    flex: 1,
    height: 26,
    borderRadius: 4,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    paddingHorizontal: 8,
    borderWidth: 1,
  },
  statusPillReady: {
    backgroundColor: '#EDF7ED',
    borderColor: '#A5D6A7',
  },
  statusPillPending: {
    backgroundColor: '#FFEBEE',
    borderColor: '#FFCDD2',
  },
  statusPillText: {
    fontSize: 11,
    fontWeight: 'bold',
  },
  statusPillTextReady: {
    color: '#2E7D32',
  },
  statusPillTextPending: {
    color: '#C62828',
  },

  // ===== GRIDS & INLINE ROWS =====
  fourColumnGrid: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    gap: 12,
  },
  singleColumnRow: {
    flexDirection: 'row',
    alignItems: 'center',
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
  highlightTotal: {
    backgroundColor: '#FFF4DA',
    color: '#8A5D00',
    fontWeight: 'bold',
    borderColor: '#D4C3A3',
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
  tableRowVerified: {
    backgroundColor: '#F4FAF4',
  },
  tableCell: {
    fontSize: 11,
    color: '#3B2D17',
  },
  codeCell: {
    fontWeight: 'bold',
    color: '#8D6409',
  },
  amountCell: {
    fontWeight: 'bold',
    color: '#8A5D00',
  },
  verifiedBadge: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#E8F5E9',
    paddingHorizontal: 6,
    paddingVertical: 2,
    borderRadius: 4,
    borderWidth: 1,
    borderColor: '#A5D6A7',
  },
  verifiedText: {
    fontSize: 10,
    fontWeight: 'bold',
    color: '#2E7D32',
  },
  pendingBadge: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFEBEE',
    paddingHorizontal: 6,
    paddingVertical: 2,
    borderRadius: 4,
    borderWidth: 1,
    borderColor: '#FFCDD2',
  },
  pendingText: {
    fontSize: 10,
    fontWeight: 'bold',
    color: '#C62828',
  },
  emptyTableContainer: {
    paddingVertical: 28,
    alignItems: 'center',
    justifyContent: 'center',
    width: '100%',
  },
  emptyTableText: {
    fontSize: 12,
    color: '#9E8D77',
    marginTop: 6,
    textAlign: 'center',
    maxWidth: 400,
  },

  // Item Entry Bar
  itemEntryBar: {
    borderTopWidth: 1,
    borderTopColor: '#E8DCB8',
    backgroundColor: '#FCFAF6',
    padding: 10,
  },
  itemEntryRow: {
    flexDirection: 'row',
    alignItems: 'center',
    flexWrap: 'wrap',
    gap: 12,
  },
  tableActionBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 12,
    paddingVertical: 5,
    borderRadius: 5,
    borderWidth: 1,
  },
  verifyBtn: {
    backgroundColor: '#F9F2E2',
    borderColor: '#B8903B',
  },
  verifyBtnText: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#9E6D08',
  },
  verifyAllBtn: {
    backgroundColor: '#FFFFFF',
    borderColor: '#D4C3A3',
  },
  verifyAllBtnText: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#7D5E19',
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
