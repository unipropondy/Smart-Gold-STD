import { ThemedText } from '@/components/themed-text';
import { API_BASE_URL } from '@/constants/api';
import { useThemeColors } from '@/hooks/useThemeColors';
import { Ionicons } from '@expo/vector-icons';
import { Stack, useLocalSearchParams, useRouter } from 'expo-router';
import React, { useEffect, useState } from 'react';
import {
  ActivityIndicator,
  FlatList,
  KeyboardAvoidingView,
  Modal,
  Platform,
  ScrollView,
  StyleSheet,
  TextInput,
  TouchableOpacity,
  View,
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';

const API_URL = `${API_BASE_URL}/transferout`;

export default function TransferOutScreen() {
  const themeColors = useThemeColors();
  const router = useRouter();
  const params = useLocalSearchParams();

  const [loading, setLoading] = useState(false);
  const [saving, setSaving] = useState(false);
  const [toast, setToast] = useState<{ message: string; type: 'success' | 'error' } | null>(null);

  // Form State
  const [locations, setLocations] = useState<any[]>([]);
  const [toLocation, setToLocation] = useState('');
  const [fromLocation] = useState('HQ');
  const [transferNo, setTransferNo] = useState(params.transferNo ? String(params.transferNo) : '');
  const [remarks, setRemarks] = useState('');
  const [isLocked, setIsLocked] = useState(false);

  // Location LOV modal state
  const [locationModalVisible, setLocationModalVisible] = useState(false);
  const [locationSearchQuery, setLocationSearchQuery] = useState('');

  // Date State
  const [transferDate, setTransferDate] = useState(() => {
    const d = new Date();
    const mm = String(d.getMonth() + 1).padStart(2, '0');
    const dd = String(d.getDate()).padStart(2, '0');
    const yyyy = d.getFullYear();
    return `${yyyy}-${mm}-${dd}`;
  });

  // Scanner State
  const [inventoryCode, setInventoryCode] = useState('');
  const [scannedItem, setScannedItem] = useState<any>(null);

  // Grid State
  const [gridData, setGridData] = useState<any[]>([]);
  const [selectedRowIndex, setSelectedRowIndex] = useState<number | null>(null);

  // Totals State
  const [totals, setTotals] = useState({
    Weight: 0,
    PMC: 0,
    PCS: 0,
    SMC: 0,
    TotalCost: 0,
  });

  useEffect(() => {
    fetchLocations();
    if (params.transferNo) {
      fetchTransferDetails(String(params.transferNo));
    }
  }, []);

  useEffect(() => {
    calculateTotals();
  }, [gridData]);

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
      }
    } catch (err) {
      console.error('Error fetching locations', err);
    }
  };

  const fetchTransferDetails = async (tNo: string) => {
    if (!tNo.trim()) return;
    try {
      setLoading(true);
      const res = await fetch(`${API_URL}/fetch?transferNo=${encodeURIComponent(tNo.trim())}`);
      const data = await res.json();

      if (data.success) {
        setTransferNo(data.header.TransferNo);
        setToLocation(data.header.ToLocation);
        setRemarks(data.header.Remarks || '');
        if (data.header.TransferDate) {
          setTransferDate(new Date(data.header.TransferDate).toISOString().split('T')[0]);
        }
        setGridData(data.details || []);
        setIsLocked(true);
        showToast('Transfer details loaded', 'success');
      } else {
        showToast(data.message || 'Transfer not found', 'error');
        setTransferNo('');
      }
    } catch (err: any) {
      showToast('Error loading transfer details', 'error');
    } finally {
      setLoading(false);
    }
  };

  const handleScanInventory = async () => {
    if (!inventoryCode.trim()) return;

    if (gridData.some((item) => item.InventoryCode.trim().toLowerCase() === inventoryCode.trim().toLowerCase())) {
      showToast('Item already in transfer list', 'error');
      setInventoryCode('');
      setScannedItem(null);
      return;
    }

    try {
      setLoading(true);
      const res = await fetch(`${API_URL}/inventory/${encodeURIComponent(inventoryCode.trim())}`);
      const data = await res.json();

      if (data.success && data.data) {
        setScannedItem(data.data);
        showToast('Item scanned: ' + (data.data.Description || data.data.InventoryCode), 'success');
      } else {
        showToast(data.message || 'Inventory item not found or out of stock', 'error');
        setInventoryCode('');
        setScannedItem(null);
      }
    } catch (err: any) {
      showToast('Network error scanning inventory item', 'error');
    } finally {
      setLoading(false);
    }
  };

  const handleAddScannedItem = () => {
    if (!scannedItem) {
      if (inventoryCode.trim()) {
        handleScanInventory();
      }
      return;
    }

    setGridData([...gridData, scannedItem]);
    setInventoryCode('');
    setScannedItem(null);
    showToast('Item added to transfer queue', 'success');
  };

  const calculateTotals = () => {
    let weight = 0,
      pmc = 0,
      pcs = 0,
      smc = 0,
      cost = 0;
    gridData.forEach((item) => {
      weight += Number(item.Weight) || 0;
      pmc += Number(item.PMC) || 0;
      pcs += Number(item.Pcs || item.QtyonHand || item.Qty || 1) || 0;
      smc += Number(item.SMC) || 0;
      cost += Number(item.TotalCost) || 0;
    });
    setTotals({ Weight: weight, PMC: pmc, PCS: pcs, SMC: smc, TotalCost: cost });
  };

  const handleDeleteItem = (index: number) => {
    if (isLocked) return;
    const newData = [...gridData];
    newData.splice(index, 1);
    setGridData(newData);
    if (selectedRowIndex === index) setSelectedRowIndex(null);
  };

  const handleClear = () => {
    setTransferNo('');
    setToLocation('');
    setRemarks('');
    setGridData([]);
    setIsLocked(false);
    setInventoryCode('');
    setScannedItem(null);
    setSelectedRowIndex(null);
  };

  const handleTransfer = async () => {
    if (isLocked) {
      showToast('This transfer is already completed and locked.', 'error');
      return;
    }
    if (!toLocation.trim()) {
      showToast('Please select destination (To Location)', 'error');
      return;
    }
    if (gridData.length === 0) {
      showToast('No items staged for transfer', 'error');
      return;
    }

    try {
      setSaving(true);
      const payload = {
        transferDate,
        fromLocation,
        toLocation,
        remarks,
        totalWeight: totals.Weight,
        totalPcs: totals.PCS,
        totalCost: totals.TotalCost,
        totalPMC: totals.PMC,
        totalSMC: totals.SMC,
        items: gridData.map((i) => ({
          ...i,
          Qty: i.Pcs || i.QtyonHand || 1,
        })),
        userId: 'admin',
      };

      const res = await fetch(`${API_URL}/transfer`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload),
      });

      const data = await res.json();

      if (data.success) {
        showToast(data.message || 'Transfer completed successfully', 'success');
        setTransferNo(data.transferNo);
        setIsLocked(true);
      } else {
        showToast(data.message || 'Failed to complete transfer', 'error');
      }
    } catch (err: any) {
      showToast('Network error saving transfer', 'error');
    } finally {
      setSaving(false);
    }
  };

  const selectedLocationObj = locations.find((l) => l.LocationCode === toLocation);

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
            <ThemedText style={styles.headerTitle}>Branch Stock Transfer Out</ThemedText>
            <ThemedText style={styles.headerSubtitle}>Inter-Branch & Warehouse Stock Movement Voucher</ThemedText>
          </View>
          {(loading || saving) && <ActivityIndicator size="small" color="#B48218" style={{ marginLeft: 8 }} />}
        </View>

        <View style={styles.headerRight}>
          <TouchableOpacity
            style={[styles.actionBtn, styles.clearBtn]}
            onPress={handleClear}
            activeOpacity={0.8}
            disabled={saving || loading}
          >
            <Ionicons name="refresh-outline" size={14} color="#5C4A28" />
            <ThemedText style={styles.clearBtnText}>Clear</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity
            style={[styles.actionBtn, styles.reprintBtn]}
            onPress={() => showToast('Printing dispatch note...', 'success')}
            activeOpacity={0.8}
          >
            <Ionicons name="print-outline" size={14} color="#5C4A28" />
            <ThemedText style={styles.clearBtnText}>Print</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity
            style={[styles.actionBtn, styles.saveBtn, isLocked && { opacity: 0.6 }]}
            onPress={handleTransfer}
            activeOpacity={0.8}
            disabled={saving || loading || isLocked}
          >
            {saving ? (
              <ActivityIndicator size="small" color="#9E6D08" />
            ) : (
              <>
                <Ionicons name="airplane-outline" size={15} color="#9E6D08" />
                <ThemedText style={styles.saveBtnText}>
                  {isLocked ? 'Transfer Locked' : 'Execute Transfer'}
                </ThemedText>
              </>
            )}
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
          {/* CARD 1: TRANSFER DOCKET & LOCATION ROUTING */}
          <View style={styles.goldCard}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="swap-horizontal-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitleSmall}>Transfer Routing & Dispatch Header</ThemedText>
              </View>

              {transferNo ? (
                <View style={styles.transferBadge}>
                  <ThemedText style={styles.transferBadgeLabel}>Transfer No:</ThemedText>
                  <ThemedText style={styles.transferBadgeValue}>{transferNo}</ThemedText>
                </View>
              ) : null}
            </View>

            <View style={styles.cardBody}>
              {/* Row 1: Transfer No, Transfer Date, From Location, To Location */}
              <View style={styles.fourColumnGrid}>
                {/* Transfer No */}
                <View style={[styles.inlineFieldRow, { flex: 1.1, minWidth: 230 }]}>
                  <ThemedText style={styles.inlineFieldLabel}>Transfer No</ThemedText>
                  <TextInput
                    style={[styles.formInput, isLocked ? styles.readonlyInput : undefined, { flex: 1 }]}
                    value={transferNo}
                    onChangeText={setTransferNo}
                    onBlur={() => transferNo && !isLocked && fetchTransferDetails(transferNo)}
                    placeholder="Auto-generated"
                    placeholderTextColor="#9E8D77"
                    editable={!isLocked}
                  />
                </View>

                {/* Transfer Date */}
                <View style={[styles.inlineFieldRow, { flex: 0.9, minWidth: 200 }]}>
                  <ThemedText style={styles.inlineFieldLabel}>Transfer Date</ThemedText>
                  <TextInput
                    style={[styles.formInput, isLocked ? styles.readonlyInput : undefined, { flex: 1 }]}
                    value={transferDate}
                    onChangeText={setTransferDate}
                    editable={!isLocked}
                  />
                </View>

                {/* From Location */}
                <View style={[styles.inlineFieldRow, { flex: 0.9, minWidth: 180 }]}>
                  <ThemedText style={styles.inlineFieldLabel}>From Loc.</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1 }]}
                    value={fromLocation}
                    editable={false}
                  />
                </View>

                {/* To Location (With Lookup Modal & Select fallback) */}
                <View style={[styles.inlineFieldRow, { flex: 1.1, minWidth: 240 }]}>
                  <ThemedText style={styles.inlineFieldLabel}>To Location *</ThemedText>
                  <View style={styles.inputWithLookup}>
                    <TextInput
                      style={[styles.formInput, styles.inputAttached, { flex: 1 }]}
                      value={selectedLocationObj ? `${selectedLocationObj.LocationCode} - ${selectedLocationObj.LocationName}` : toLocation}
                      placeholder="Select Target Branch..."
                      placeholderTextColor="#9E8D77"
                      editable={false}
                    />
                    <TouchableOpacity
                      style={styles.attachedLookupBtn}
                      onPress={() => {
                        if (!isLocked) {
                          setLocationSearchQuery('');
                          setLocationModalVisible(true);
                        }
                      }}
                      activeOpacity={0.7}
                      disabled={isLocked}
                    >
                      <Ionicons name="search" size={13} color="#7D5E19" />
                    </TouchableOpacity>
                  </View>
                </View>
              </View>

              {/* Row 2: Remarks */}
              <View style={[styles.singleColumnRow, { marginTop: 6 }]}>
                <View style={styles.inlineFieldRow}>
                  <ThemedText style={styles.inlineFieldLabel}>Remarks</ThemedText>
                  <TextInput
                    style={[styles.formInput, isLocked ? styles.readonlyInput : undefined, { flex: 1 }]}
                    value={remarks}
                    onChangeText={setRemarks}
                    placeholder="Dispatch notes or transit instructions..."
                    placeholderTextColor="#9E8D77"
                    editable={!isLocked}
                  />
                </View>
              </View>
            </View>
          </View>

          {/* CARD 2: DISPATCH TOTALS & FINANCIAL SUMMARY */}
          <View style={[styles.goldCard, { marginTop: 10 }]}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="calculator-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitleSmall}>Consolidated Dispatch Totals</ThemedText>
              </View>
            </View>

            <View style={styles.cardBody}>
              <View style={styles.fiveColumnGrid}>
                {/* Total Weight */}
                <View style={[styles.inlineFieldRow, { flex: 1, minWidth: 200 }]}>
                  <ThemedText style={[styles.inlineFieldLabel, { width: 95 }]}>Total Wt (g)</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1, minWidth: 90, textAlign: 'right', fontWeight: 'bold' }]}
                    value={totals.Weight.toFixed(3)}
                    editable={false}
                  />
                </View>

                {/* Total Pieces */}
                <View style={[styles.inlineFieldRow, { flex: 1, minWidth: 180 }]}>
                  <ThemedText style={[styles.inlineFieldLabel, { width: 85 }]}>Total PCS</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1, minWidth: 70, textAlign: 'right' }]}
                    value={String(totals.PCS)}
                    editable={false}
                  />
                </View>

                {/* Total PMC */}
                <View style={[styles.inlineFieldRow, { flex: 1, minWidth: 195 }]}>
                  <ThemedText style={[styles.inlineFieldLabel, { width: 95 }]}>Total PMC ($)</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1, minWidth: 80, textAlign: 'right' }]}
                    value={totals.PMC.toFixed(2)}
                    editable={false}
                  />
                </View>

                {/* Total SMC */}
                <View style={[styles.inlineFieldRow, { flex: 1, minWidth: 195 }]}>
                  <ThemedText style={[styles.inlineFieldLabel, { width: 95 }]}>Total SMC ($)</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1, minWidth: 80, textAlign: 'right' }]}
                    value={totals.SMC.toFixed(2)}
                    editable={false}
                  />
                </View>

                {/* Total Cost */}
                <View style={[styles.inlineFieldRow, { flex: 1, minWidth: 205 }]}>
                  <ThemedText style={[styles.inlineFieldLabel, { width: 95 }]}>Total Cost ($)</ThemedText>
                  <TextInput
                    style={[
                      styles.formInput,
                      styles.readonlyInput,
                      styles.highlightTotal,
                      { flex: 1, minWidth: 90, textAlign: 'right', fontWeight: 'bold' },
                    ]}
                    value={totals.TotalCost.toFixed(2)}
                    editable={false}
                  />
                </View>
              </View>
            </View>
          </View>

          {/* CARD 3: STAGED INVENTORY ITEMS TABLE (100% FULL WIDTH) */}
          <View style={[styles.goldCard, { marginTop: 10 }]}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="cube-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitleSmall}>Staged Items for Transfer</ThemedText>
                <View style={styles.counterBadge}>
                  <ThemedText style={styles.counterBadgeText}>{gridData.length} items</ThemedText>
                </View>
              </View>
            </View>

            {/* Table Header (100% Full Width) */}
            <View style={styles.tableHeadRow}>
              <ThemedText style={[styles.tableHeadCell, { flex: 1.6 }]}>Inventory Code</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 2.4 }]}>Description</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 1, textAlign: 'center' }]}>Class</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 1, textAlign: 'center' }]}>Category</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 1, textAlign: 'right' }]}>Weight (g)</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 0.8, textAlign: 'center' }]}>Size</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 0.7, textAlign: 'right' }]}>Qty</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 1.1, textAlign: 'right' }]}>Cost/g ($)</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 1, textAlign: 'right' }]}>PMC ($)</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 1, textAlign: 'right' }]}>SMC ($)</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 1.2, textAlign: 'right' }]}>Total ($)</ThemedText>
              {!isLocked && (
                <ThemedText style={[styles.tableHeadCell, { width: 50, textAlign: 'center' }]}>Action</ThemedText>
              )}
            </View>

            {/* Table Body */}
            {gridData.length === 0 ? (
              <View style={styles.emptyTableContainer}>
                <Ionicons name="barcode-outline" size={28} color="#D4C3A3" />
                <ThemedText style={styles.emptyTableText}>
                  No items staged for transfer. Scan barcodes using the entry bar below.
                </ThemedText>
              </View>
            ) : (
              gridData.map((item, index) => {
                const isSelected = selectedRowIndex === index;
                return (
                  <TouchableOpacity
                    key={index}
                    style={[styles.tableRow, isSelected && styles.tableRowSelected]}
                    onPress={() => setSelectedRowIndex(index)}
                    activeOpacity={0.8}
                  >
                    <ThemedText style={[styles.tableCell, styles.codeCell, { flex: 1.6 }]}>
                      {item.InventoryCode}
                    </ThemedText>
                    <ThemedText style={[styles.tableCell, { flex: 2.4 }]} numberOfLines={1}>
                      {item.Description}
                    </ThemedText>
                    <ThemedText style={[styles.tableCell, { flex: 1, textAlign: 'center' }]}>
                      {item.ClassCode || '-'}
                    </ThemedText>
                    <ThemedText style={[styles.tableCell, { flex: 1, textAlign: 'center' }]}>
                      {item.CategoryCode || '-'}
                    </ThemedText>
                    <ThemedText style={[styles.tableCell, { flex: 1, textAlign: 'right', fontWeight: 'bold' }]}>
                      {Number(item.Weight || 0).toFixed(3)}
                    </ThemedText>
                    <ThemedText style={[styles.tableCell, { flex: 0.8, textAlign: 'center' }]}>
                      {item.SizeCode || '-'}
                    </ThemedText>
                    <ThemedText style={[styles.tableCell, { flex: 0.7, textAlign: 'right' }]}>
                      {item.Pcs || item.QtyonHand || item.Qty || 1}
                    </ThemedText>
                    <ThemedText style={[styles.tableCell, { flex: 1.1, textAlign: 'right' }]}>
                      {Number(item.CostPerGram || 0).toFixed(2)}
                    </ThemedText>
                    <ThemedText style={[styles.tableCell, { flex: 1, textAlign: 'right', color: '#7A6855' }]}>
                      {Number(item.PMC || 0).toFixed(2)}
                    </ThemedText>
                    <ThemedText style={[styles.tableCell, { flex: 1, textAlign: 'right', color: '#7A6855' }]}>
                      {Number(item.SMC || 0).toFixed(2)}
                    </ThemedText>
                    <ThemedText style={[styles.tableCell, styles.amountCell, { flex: 1.2, textAlign: 'right' }]}>
                      {Number(item.TotalCost || 0).toFixed(2)}
                    </ThemedText>
                    {!isLocked && (
                      <View style={{ width: 50, alignItems: 'center' }}>
                        <TouchableOpacity
                          style={styles.deleteRowBtn}
                          onPress={() => handleDeleteItem(index)}
                          activeOpacity={0.7}
                        >
                          <Ionicons name="trash-outline" size={14} color="#C62828" />
                        </TouchableOpacity>
                      </View>
                    )}
                  </TouchableOpacity>
                );
              })
            )}

            {/* SCANNER SUBFORM ENTRY BAR */}
            {!isLocked && (
              <View style={styles.itemEntryBar}>
                <View style={styles.itemEntryRow}>
                  {/* Scan Input */}
                  <View style={[styles.inlineFieldRow, { flex: 1.6, minWidth: 230 }]}>
                    <ThemedText style={[styles.inlineFieldLabel, { width: 100 }]}>Scan Barcode *</ThemedText>
                    <TextInput
                      style={[styles.formInput, { flex: 1, minWidth: 110 }]}
                      value={inventoryCode}
                      onChangeText={setInventoryCode}
                      onBlur={handleScanInventory}
                      onSubmitEditing={handleScanInventory}
                      placeholder="Scan or type code..."
                      placeholderTextColor="#9E8D77"
                    />
                  </View>

                  {/* Scanned Description */}
                  <View style={[styles.inlineFieldRow, { flex: 2.2, minWidth: 240 }]}>
                    <ThemedText style={[styles.inlineFieldLabel, { width: 85 }]}>Description</ThemedText>
                    <TextInput
                      style={[styles.formInput, styles.readonlyInput, { flex: 1, minWidth: 130 }]}
                      value={scannedItem?.Description || ''}
                      editable={false}
                      placeholder="Scanned description"
                      placeholderTextColor="#9E8D77"
                    />
                  </View>

                  {/* Scanned Weight */}
                  <View style={[styles.inlineFieldRow, { flex: 0.9, minWidth: 150 }]}>
                    <ThemedText style={[styles.inlineFieldLabel, { width: 65 }]}>Weight</ThemedText>
                    <TextInput
                      style={[styles.formInput, styles.readonlyInput, { flex: 1, minWidth: 70, textAlign: 'right' }]}
                      value={scannedItem?.Weight ? Number(scannedItem.Weight).toFixed(3) : ''}
                      editable={false}
                      placeholder="0.000"
                      placeholderTextColor="#9E8D77"
                    />
                  </View>

                  {/* Scanned Total Cost */}
                  <View style={[styles.inlineFieldRow, { flex: 1.1, minWidth: 175 }]}>
                    <ThemedText style={[styles.inlineFieldLabel, { width: 70 }]}>Total ($)</ThemedText>
                    <TextInput
                      style={[styles.formInput, styles.readonlyInput, styles.highlightTotal, { flex: 1, minWidth: 80, textAlign: 'right' }]}
                      value={scannedItem?.TotalCost ? Number(scannedItem.TotalCost).toFixed(2) : ''}
                      editable={false}
                      placeholder="0.00"
                      placeholderTextColor="#9E8D77"
                    />
                  </View>

                  {/* Add Button */}
                  <TouchableOpacity
                    style={[styles.tableActionBtn, styles.addBtn, { height: 26, paddingHorizontal: 12, marginLeft: 4 }]}
                    onPress={handleAddScannedItem}
                    activeOpacity={0.8}
                  >
                    <Ionicons name="add-circle-outline" size={14} color="#9E6D08" style={{ marginRight: 4 }} />
                    <ThemedText style={styles.addBtnText}>Add Item</ThemedText>
                  </TouchableOpacity>
                </View>
              </View>
            )}
          </View>
        </ScrollView>
      </KeyboardAvoidingView>

      {/* LOCATION LOOKUP MODAL */}
      <Modal visible={locationModalVisible} transparent animationType="fade" onRequestClose={() => setLocationModalVisible(false)}>
        <View style={styles.modalOverlay}>
          <View style={styles.lovModalCard}>
            <View style={styles.modalHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="business-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.modalHeaderTitle}>Select Target Branch Location</ThemedText>
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
                    setToLocation(item.LocationCode);
                    setLocationModalVisible(false);
                  }}
                  activeOpacity={0.7}
                >
                  <ThemedText style={[styles.lovItemText, styles.codeCell, { flex: 1.4 }]}>
                    {item.LocationCode}
                  </ThemedText>
                  <ThemedText style={[styles.lovItemText, { flex: 2.2 }]}>{item.LocationName}</ThemedText>
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
  reprintBtn: {
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
    paddingVertical: 6,
    paddingHorizontal: 10,
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
  },
  headerTitleContainer: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  headerIconBadgeSmall: {
    width: 20,
    height: 20,
    borderRadius: 4,
    backgroundColor: '#FFFFFF',
    justifyContent: 'center',
    alignItems: 'center',
    marginRight: 6,
    borderWidth: 1,
    borderColor: '#E2D3B3',
  },
  cardHeaderTitleSmall: {
    fontSize: 13,
    fontWeight: 'bold',
    color: '#3B2D17',
    letterSpacing: 0.2,
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

  // ===== GRIDS & INLINE ROWS =====
  fourColumnGrid: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    gap: 10,
  },
  fiveColumnGrid: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    gap: 12,
  },
  twoColumnGrid: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    gap: 10,
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
    backgroundColor: '#F5E7CA',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    paddingVertical: 6,
    paddingHorizontal: 8,
    width: '100%',
  },
  tableHeadCell: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3B2D17',
    paddingHorizontal: 4,
  },
  tableRow: {
    flexDirection: 'row',
    alignItems: 'center',
    borderBottomWidth: 1,
    borderBottomColor: '#F3EBDD',
    paddingVertical: 5,
    paddingHorizontal: 8,
    backgroundColor: '#FFFFFF',
    width: '100%',
  },
  tableRowSelected: {
    backgroundColor: '#FFF8E7',
  },
  tableCell: {
    fontSize: 11,
    color: '#3B2D17',
    paddingHorizontal: 4,
  },
  codeCell: {
    fontFamily: Platform.OS === 'ios' ? 'Courier' : 'monospace',
    fontWeight: 'bold',
    color: '#8A5D00',
  },
  amountCell: {
    fontWeight: 'bold',
    color: '#1B5E20',
  },
  deleteRowBtn: {
    padding: 3,
  },
  emptyTableContainer: {
    paddingVertical: 24,
    alignItems: 'center',
    justifyContent: 'center',
    width: '100%',
  },
  emptyTableText: {
    fontSize: 12,
    color: '#9E8D77',
    marginTop: 4,
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
  addBtn: {
    backgroundColor: '#F9F2E2',
    borderColor: '#B8903B',
  },
  addBtnText: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#9E6D08',
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
    borderWidth: 1,
    borderColor: '#E8DCB8',
    overflow: 'hidden',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.15,
    shadowRadius: 8,
    elevation: 8,
  },
  modalHeaderBanner: {
    backgroundColor: '#F5E7CA',
    paddingVertical: 8,
    paddingHorizontal: 12,
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
  },
  modalHeaderTitle: {
    fontSize: 13,
    fontWeight: 'bold',
    color: '#3B2D17',
  },
  closeModalBtn: {
    padding: 3,
  },
  modalSearchBox: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FCFAF6',
    margin: 8,
    paddingHorizontal: 8,
    height: 32,
    borderWidth: 1,
    borderColor: '#D4C3A3',
    borderRadius: 6,
  },
  modalSearchInput: {
    flex: 1,
    fontSize: 12,
    color: '#2B2118',
    paddingVertical: 0,
  },
  lovHeadRow: {
    flexDirection: 'row',
    backgroundColor: '#F8F2E4',
    paddingVertical: 5,
    paddingHorizontal: 10,
    borderBottomWidth: 1,
    borderBottomColor: '#E8DCB8',
  },
  lovHeadText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#8D6409',
  },
  lovItemRow: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingVertical: 8,
    paddingHorizontal: 10,
    borderBottomWidth: 1,
    borderBottomColor: '#F3EBDD',
  },
  lovItemText: {
    fontSize: 11,
    color: '#3B2D17',
  },

  // ===== TOAST =====
  toast: {
    position: 'absolute',
    top: 56,
    alignSelf: 'center',
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 16,
    paddingVertical: 8,
    borderRadius: 20,
    zIndex: 9999,
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
  toastText: {
    color: '#FFFFFF',
    fontWeight: 'bold',
    fontSize: 12,
  },
});
