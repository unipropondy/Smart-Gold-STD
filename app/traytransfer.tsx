import { ThemedText } from '@/components/themed-text';
import { API_BASE_URL } from '@/constants/api';
import { useThemeColors } from '@/hooks/useThemeColors';
import { Ionicons } from '@expo/vector-icons';
import { Stack, useRouter } from 'expo-router';
import React, { useState } from 'react';
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

const API_URL = `${API_BASE_URL}/traytransfer`;

export default function TrayTransferScreen() {
  const themeColors = useThemeColors();
  const router = useRouter();

  const [loading, setLoading] = useState(false);
  const [saving, setSaving] = useState(false);
  const [toast, setToast] = useState<{ message: string; type: 'success' | 'error' } | null>(null);

  // Form State
  const [oldTray, setOldTray] = useState('');
  const [inventoryCode, setInventoryCode] = useState('');
  const [itemDescription, setItemDescription] = useState('');
  const [trayId, setTrayId] = useState('');
  const [trayDescription, setTrayDescription] = useState('');
  const [remarks, setRemarks] = useState('');
  const [isCustomerTray, setIsCustomerTray] = useState(false);

  // Grid / Multi-select state
  const [selectedInventoryCodes, setSelectedInventoryCodes] = useState<string[]>([]);
  const [inventoryModalVisible, setInventoryModalVisible] = useState(false);
  const [gridData, setGridData] = useState<any[]>([]);
  const [barcodeInput, setBarcodeInput] = useState('');
  const [inventorySearchQuery, setInventorySearchQuery] = useState('');

  // LOV State
  const [lovVisible, setLovVisible] = useState(false);
  const [lovData, setLovData] = useState<any[]>([]);
  const [lovType, setLovType] = useState<'oldTray' | 'newTray'>('oldTray');
  const [traySearchQuery, setTraySearchQuery] = useState('');

  const showToast = (message: string, type: 'success' | 'error' = 'success') => {
    setToast({ message, type });
    setTimeout(() => setToast(null), 3000);
  };

  const handleClear = () => {
    setOldTray('');
    setInventoryCode('');
    setItemDescription('');
    setTrayId('');
    setTrayDescription('');
    setRemarks('');
    setIsCustomerTray(false);
    setSelectedInventoryCodes([]);
    setGridData([]);
  };

  // --- API Calls ---

  const fetchTrays = async (type: 'oldTray' | 'newTray') => {
    try {
      setLoading(true);
      const res = await fetch(`${API_URL}/trays`);
      const data = await res.json();
      if (data.success) {
        setLovData(data.data || []);
        setLovType(type);
        setTraySearchQuery('');
        setLovVisible(true);
      } else {
        showToast(data.message || 'Failed to load trays', 'error');
      }
    } catch (err: any) {
      showToast('Error querying trays', 'error');
    } finally {
      setLoading(false);
    }
  };

  const handleSelectTray = (item: any) => {
    setLovVisible(false);
    if (lovType === 'oldTray') {
      setOldTray(item.TrayId);
    } else {
      setTrayId(item.TrayId);
      setTrayDescription(item.TrayName || '');
      setIsCustomerTray(item.IsCustomerTray === 'Y' || item.IsCustomerTray === true);
    }
  };

  const fetchSingleInventory = async (code: string) => {
    if (!code || !code.trim()) return;
    try {
      setLoading(true);
      const res = await fetch(`${API_URL}/inventory?code=${encodeURIComponent(code.trim())}`);
      const data = await res.json();
      if (data.success && data.data) {
        setItemDescription(data.data.Description || '');
        setOldTray(data.data.TrayId || '');
        setSelectedInventoryCodes([data.data.InventoryCode]);
        showToast('Item loaded', 'success');
      } else {
        showToast(data.message || 'Inventory code not found or already sold', 'error');
        setItemDescription('');
        setSelectedInventoryCodes([]);
      }
    } catch (err: any) {
      showToast('Network error loading inventory code', 'error');
    } finally {
      setLoading(false);
    }
  };

  const fetchInventoryByOldTray = async () => {
    if (!oldTray || !oldTray.trim()) {
      showToast('Please select Old Tray first', 'error');
      return;
    }
    try {
      setLoading(true);
      const res = await fetch(`${API_URL}/inventory_by_tray?trayId=${encodeURIComponent(oldTray.trim())}`);
      const data = await res.json();
      if (data.success && data.data) {
        const gridItems = data.data.map((item: any) => ({
          ...item,
          selected: selectedInventoryCodes.includes(item.InventoryCode),
        }));
        setGridData(gridItems);
        setInventorySearchQuery('');
        setBarcodeInput('');
        setInventoryModalVisible(true);
      } else {
        showToast(data.message || 'No items found in this tray', 'error');
      }
    } catch (err: any) {
      showToast('Network error loading tray items', 'error');
    } finally {
      setLoading(false);
    }
  };

  const handleSave = async () => {
    let codesToSave = selectedInventoryCodes;
    if (codesToSave.length === 0 && inventoryCode.trim()) {
      codesToSave = [inventoryCode.trim()];
    }

    if (codesToSave.length === 0) {
      showToast('Please enter or select Inventory Code to transfer.', 'error');
      return;
    }
    if (!trayId.trim()) {
      showToast('Please select New Tray to transfer to.', 'error');
      return;
    }

    try {
      setSaving(true);
      const payload = {
        newTrayId: trayId.trim(),
        oldTrayId: oldTray.trim(),
        remarks: remarks.trim(),
        inventoryCodes: codesToSave,
        userId: 'admin',
      };
      const res = await fetch(`${API_URL}/save`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload),
      });
      const data = await res.json();
      if (data.success) {
        showToast('Tray Transferred Successfully', 'success');
        handleClear();
      } else {
        showToast(data.message || 'Failed to transfer tray', 'error');
      }
    } catch (err: any) {
      showToast('Network error while saving transfer', 'error');
    } finally {
      setSaving(false);
    }
  };

  // --- Modal Logic ---
  const handleModalSelectAll = (value: boolean) => {
    setGridData((prev) => prev.map((item) => ({ ...item, selected: value })));
  };

  const handleModalToggleItem = (index: number) => {
    const newData = [...gridData];
    newData[index].selected = !newData[index].selected;
    setGridData(newData);
  };

  const handleModalBarcodeScan = () => {
    if (!barcodeInput.trim()) return;
    const newData = [...gridData];
    let found = false;
    for (let i = 0; i < newData.length; i++) {
      if (newData[i].InventoryCode.trim().toLowerCase() === barcodeInput.trim().toLowerCase()) {
        newData[i].selected = true;
        found = true;
        break;
      }
    }
    if (!found) {
      showToast(`Item code "${barcodeInput}" not found in this tray.`, 'error');
    } else {
      showToast(`Selected "${barcodeInput}"`, 'success');
    }
    setGridData(newData);
    setBarcodeInput('');
  };

  const handleModalOk = () => {
    const selected = gridData.filter((i) => i.selected).map((i) => i.InventoryCode);
    setSelectedInventoryCodes(selected);
    if (selected.length > 0) {
      setInventoryCode(selected.length > 1 ? `[${selected.length} items selected]` : selected[0]);
    }
    setInventoryModalVisible(false);
  };

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
            <ThemedText style={styles.headerTitle}>Tray Stock Transfer</ThemedText>
            <ThemedText style={styles.headerSubtitle}>Stock Movement & Showcase Tray Allocation</ThemedText>
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
            onPress={() => showToast('Printing transfer docket...', 'success')}
            activeOpacity={0.8}
          >
            <Ionicons name="print-outline" size={14} color="#5C4A28" />
            <ThemedText style={styles.clearBtnText}>Print</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity
            style={[styles.actionBtn, styles.saveBtn]}
            onPress={handleSave}
            activeOpacity={0.8}
            disabled={saving || loading}
          >
            {saving ? (
              <ActivityIndicator size="small" color="#9E6D08" />
            ) : (
              <>
                <Ionicons name="checkmark-done-outline" size={15} color="#9E6D08" />
                <ThemedText style={styles.saveBtnText}>Save Transfer</ThemedText>
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
          {/* CARD 1: SOURCE TRAY & INVENTORY SELECTION */}
          <View style={styles.goldCard}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="cube-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitleSmall}>Source Stock (Old Tray)</ThemedText>
              </View>

              {oldTray ? (
                <View style={styles.trayBadge}>
                  <ThemedText style={styles.trayBadgeLabel}>Source:</ThemedText>
                  <ThemedText style={styles.trayBadgeValue}>{oldTray}</ThemedText>
                </View>
              ) : null}
            </View>

            <View style={styles.cardBody}>
              {/* Row 1: Old Tray + Inventory Code + Batch Multi-Select Button */}
              <View style={styles.twoColumnGrid}>
                {/* Old Tray */}
                <View style={[styles.inlineFieldRow, { flex: 1, minWidth: 260 }]}>
                  <ThemedText style={styles.inlineFieldLabel}>Old Tray</ThemedText>
                  <View style={styles.inputWithCombo}>
                    <TextInput
                      style={[styles.formInput, styles.inputAttached, styles.readonlyInput, { flex: 1 }]}
                      value={oldTray}
                      editable={false}
                      placeholder="Select Old Tray..."
                      placeholderTextColor="#9E8D77"
                    />
                    <TouchableOpacity
                      style={styles.attachedLookupBtn}
                      onPress={() => fetchTrays('oldTray')}
                      activeOpacity={0.7}
                    >
                      <Ionicons name="search" size={13} color="#7D5E19" />
                    </TouchableOpacity>
                    <TouchableOpacity
                      style={styles.batchItemsBtn}
                      onPress={fetchInventoryByOldTray}
                      activeOpacity={0.8}
                    >
                      <Ionicons name="list" size={13} color="#7D5E19" />
                      <ThemedText style={styles.batchItemsBtnText}>Tray Items</ThemedText>
                    </TouchableOpacity>
                  </View>
                </View>

                {/* Inventory Code */}
                <View style={[styles.inlineFieldRow, { flex: 1, minWidth: 260 }]}>
                  <ThemedText style={styles.inlineFieldLabel}>Inventory Code *</ThemedText>
                  <TextInput
                    style={[styles.formInput, { flex: 1 }]}
                    value={inventoryCode}
                    onChangeText={setInventoryCode}
                    onBlur={() => fetchSingleInventory(inventoryCode)}
                    placeholder="Scan or enter item code..."
                    placeholderTextColor="#9E8D77"
                  />
                </View>
              </View>

              {/* Row 2: Item Description */}
              <View style={[styles.singleColumnRow, { marginTop: 6 }]}>
                <View style={styles.inlineFieldRow}>
                  <ThemedText style={styles.inlineFieldLabel}>Item Description</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1 }]}
                    value={itemDescription}
                    editable={false}
                    placeholder="Auto-populated item description"
                    placeholderTextColor="#9E8D77"
                  />
                </View>
              </View>

              {selectedInventoryCodes.length > 1 && (
                <View style={styles.multiSelectIndicatorBanner}>
                  <Ionicons name="checkmark-circle" size={15} color="#1B5E20" style={{ marginRight: 6 }} />
                  <ThemedText style={styles.multiSelectIndicatorText}>
                    {selectedInventoryCodes.length} inventory items selected for bulk transfer.
                  </ThemedText>
                </View>
              )}
            </View>
          </View>

          {/* CARD 2: DESTINATION TRAY & DETAILS */}
          <View style={[styles.goldCard, { marginTop: 10 }]}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="arrow-forward-circle-outline" size={14} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitleSmall}>Destination (New Tray)</ThemedText>
              </View>

              {trayId ? (
                <View style={styles.destTrayBadge}>
                  <ThemedText style={styles.destTrayBadgeLabel}>Target:</ThemedText>
                  <ThemedText style={styles.destTrayBadgeValue}>{trayId}</ThemedText>
                </View>
              ) : null}
            </View>

            <View style={styles.cardBody}>
              {/* Row 1: New Tray ID + Tray Description */}
              <View style={styles.twoColumnGrid}>
                {/* New Tray ID */}
                <View style={[styles.inlineFieldRow, { flex: 1, minWidth: 260 }]}>
                  <ThemedText style={styles.inlineFieldLabel}>New Tray ID *</ThemedText>
                  <View style={styles.inputWithLookup}>
                    <TextInput
                      style={[styles.formInput, styles.inputAttached, { flex: 1 }]}
                      value={trayId}
                      onChangeText={setTrayId}
                      placeholder="Select/Enter Target Tray..."
                      placeholderTextColor="#9E8D77"
                    />
                    <TouchableOpacity
                      style={styles.attachedLookupBtn}
                      onPress={() => fetchTrays('newTray')}
                      activeOpacity={0.7}
                    >
                      <Ionicons name="search" size={13} color="#7D5E19" />
                    </TouchableOpacity>
                  </View>
                </View>

                {/* Tray Description */}
                <View style={[styles.inlineFieldRow, { flex: 1, minWidth: 260 }]}>
                  <ThemedText style={styles.inlineFieldLabel}>Tray Name</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1 }]}
                    value={trayDescription}
                    editable={false}
                    placeholder="Auto-populated tray description"
                    placeholderTextColor="#9E8D77"
                  />
                </View>
              </View>

              {/* Row 2: Customer Tray Flag & Remarks */}
              <View style={[styles.twoColumnGrid, { marginTop: 6 }]}>
                {/* Remarks */}
                <View style={[styles.inlineFieldRow, { flex: 2, minWidth: 280 }]}>
                  <ThemedText style={styles.inlineFieldLabel}>Remarks</ThemedText>
                  <TextInput
                    style={[styles.formInput, { flex: 1 }]}
                    value={remarks}
                    onChangeText={setRemarks}
                    placeholder="Stock transfer notes or justification..."
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* Customer Tray Indicator */}
                <View style={[styles.inlineFieldRow, { flex: 1, minWidth: 180 }]}>
                  <View style={[styles.customerTrayPill, isCustomerTray && styles.customerTrayPillActive]}>
                    <Ionicons
                      name={isCustomerTray ? 'people' : 'pricetag-outline'}
                      size={14}
                      color={isCustomerTray ? '#9E6D08' : '#7A6855'}
                      style={{ marginRight: 6 }}
                    />
                    <ThemedText style={[styles.customerTrayText, isCustomerTray && styles.customerTrayTextActive]}>
                      {isCustomerTray ? 'Customer Consignment Tray' : 'Standard Store Tray'}
                    </ThemedText>
                  </View>
                </View>
              </View>
            </View>
          </View>
        </ScrollView>
      </KeyboardAvoidingView>

      {/* MULTI-SELECT INVENTORY MODAL (100% FULL WIDTH TABLE) */}
      <Modal visible={inventoryModalVisible} transparent animationType="fade">
        <View style={styles.modalOverlay}>
          <View style={styles.lovModalCardLarge}>
            <View style={styles.modalHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="layers-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.modalHeaderTitle}>
                  Select Inventory Items from Tray [{oldTray}]
                </ThemedText>
                <View style={styles.counterBadge}>
                  <ThemedText style={styles.counterBadgeText}>
                    {gridData.filter((i) => i.selected).length} / {gridData.length} selected
                  </ThemedText>
                </View>
              </View>
              <TouchableOpacity
                onPress={() => setInventoryModalVisible(false)}
                activeOpacity={0.7}
                style={styles.closeModalBtn}
              >
                <Ionicons name="close" size={18} color="#3B2D17" />
              </TouchableOpacity>
            </View>

            {/* Modal Quick Filter & Barcode Scanner */}
            <View style={styles.modalBarcodeRow}>
              {/* Select All Toggle */}
              <TouchableOpacity
                style={styles.modalSelectAllBtn}
                onPress={() => handleModalSelectAll(!(gridData.length > 0 && gridData.every((i) => i.selected)))}
                activeOpacity={0.7}
              >
                <Ionicons
                  name={
                    gridData.length > 0 && gridData.every((i) => i.selected) ? 'checkbox' : 'square-outline'
                  }
                  size={16}
                  color="#9E6D08"
                />
                <ThemedText style={styles.modalSelectAllText}>Select All</ThemedText>
              </TouchableOpacity>

              {/* Barcode / Item Scanner Box */}
              <View style={styles.modalBarcodeScanner}>
                <Ionicons name="barcode-outline" size={16} color="#8D6409" style={{ marginRight: 6 }} />
                <TextInput
                  style={styles.modalBarcodeInput}
                  placeholder="Scan or type barcode to select..."
                  placeholderTextColor="#9E8D77"
                  value={barcodeInput}
                  onChangeText={setBarcodeInput}
                  onSubmitEditing={handleModalBarcodeScan}
                />
                <TouchableOpacity
                  style={styles.barcodeGoBtn}
                  onPress={handleModalBarcodeScan}
                  activeOpacity={0.8}
                >
                  <ThemedText style={styles.barcodeGoBtnText}>Check</ThemedText>
                </TouchableOpacity>
              </View>
            </View>

            {/* Table Header (100% Full Width) */}
            <View style={styles.tableHeadRow}>
              <View style={{ width: 44, alignItems: 'center' }}>
                <ThemedText style={styles.tableHeadCell}>Pick</ThemedText>
              </View>
              <ThemedText style={[styles.tableHeadCell, { flex: 1.6 }]}>Inventory Code</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 2.4 }]}>Description</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 1.2, textAlign: 'right' }]}>Weight (g)</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 1, textAlign: 'right' }]}>Qty</ThemedText>
            </View>

            {/* Table Body */}
            <FlatList
              data={gridData}
              keyExtractor={(item, index) => item.InventoryCode || index.toString()}
              initialNumToRender={50}
              maxToRenderPerBatch={50}
              ListEmptyComponent={
                <View style={styles.emptyTableContainer}>
                  <Ionicons name="cube-outline" size={28} color="#D4C3A3" />
                  <ThemedText style={styles.emptyTableText}>No inventory found for this tray.</ThemedText>
                </View>
              }
              renderItem={({ item, index }) => (
                <TouchableOpacity
                  style={[styles.tableRow, item.selected && styles.tableRowSelected]}
                  onPress={() => handleModalToggleItem(index)}
                  activeOpacity={0.7}
                >
                  <View style={{ width: 44, alignItems: 'center' }}>
                    <Ionicons
                      name={item.selected ? 'checkbox' : 'square-outline'}
                      size={17}
                      color={item.selected ? '#9E6D08' : '#8D6409'}
                    />
                  </View>
                  <ThemedText style={[styles.tableCell, styles.codeCell, { flex: 1.6 }]}>
                    {item.InventoryCode}
                  </ThemedText>
                  <ThemedText style={[styles.tableCell, { flex: 2.4 }]} numberOfLines={1}>
                    {item.Description}
                  </ThemedText>
                  <ThemedText style={[styles.tableCell, { flex: 1.2, textAlign: 'right', fontWeight: 'bold' }]}>
                    {parseFloat(item.Weight || 0).toFixed(2)}
                  </ThemedText>
                  <ThemedText style={[styles.tableCell, { flex: 1, textAlign: 'right' }]}>
                    {item.QtyOnHand}
                  </ThemedText>
                </TouchableOpacity>
              )}
            />

            {/* Modal Bottom Actions */}
            <View style={styles.modalBottomBar}>
              <TouchableOpacity
                style={[styles.actionBtn, styles.clearBtn]}
                onPress={() => setInventoryModalVisible(false)}
                activeOpacity={0.8}
              >
                <ThemedText style={styles.clearBtnText}>Cancel</ThemedText>
              </TouchableOpacity>

              <TouchableOpacity
                style={[styles.actionBtn, styles.saveBtn, { minWidth: 100 }]}
                onPress={handleModalOk}
                activeOpacity={0.8}
              >
                <Ionicons name="checkmark-done" size={14} color="#9E6D08" style={{ marginRight: 4 }} />
                <ThemedText style={styles.saveBtnText}>
                  Confirm ({gridData.filter((i) => i.selected).length})
                </ThemedText>
              </TouchableOpacity>
            </View>
          </View>
        </View>
      </Modal>

      {/* TRAY LOOKUP MODAL */}
      <Modal visible={lovVisible} transparent animationType="fade">
        <View style={styles.modalOverlay}>
          <View style={styles.lovModalCard}>
            <View style={styles.modalHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="grid-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.modalHeaderTitle}>
                  Select {lovType === 'oldTray' ? 'Source (Old Tray)' : 'Destination (New Tray)'}
                </ThemedText>
              </View>
              <TouchableOpacity
                onPress={() => setLovVisible(false)}
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
                value={traySearchQuery}
                onChangeText={setTraySearchQuery}
                placeholder="Search by Tray ID or Name..."
                placeholderTextColor="#9E8D77"
                autoFocus
              />
              {traySearchQuery.length > 0 && (
                <TouchableOpacity onPress={() => setTraySearchQuery('')}>
                  <Ionicons name="close-circle" size={16} color="#9E8D77" />
                </TouchableOpacity>
              )}
            </View>

            <View style={styles.lovHeadRow}>
              <ThemedText style={[styles.lovHeadText, { flex: 1.4 }]}>Tray ID</ThemedText>
              <ThemedText style={[styles.lovHeadText, { flex: 2.2 }]}>Tray Name</ThemedText>
              <ThemedText style={[styles.lovHeadText, { flex: 1.4 }]}>Salesman</ThemedText>
            </View>

            <FlatList
              data={lovData.filter(
                (item) =>
                  (item.TrayId || '').toLowerCase().includes(traySearchQuery.toLowerCase()) ||
                  (item.TrayName || '').toLowerCase().includes(traySearchQuery.toLowerCase()) ||
                  (item.SalesManName || '').toLowerCase().includes(traySearchQuery.toLowerCase())
              )}
              keyExtractor={(item, index) => item.TrayId || index.toString()}
              initialNumToRender={50}
              maxToRenderPerBatch={50}
              ListEmptyComponent={
                <View style={styles.emptyTableContainer}>
                  <Ionicons name="search-outline" size={24} color="#D4C3A3" />
                  <ThemedText style={styles.emptyTableText}>No matching trays found.</ThemedText>
                </View>
              }
              renderItem={({ item }) => (
                <TouchableOpacity
                  style={styles.lovItemRow}
                  onPress={() => handleSelectTray(item)}
                  activeOpacity={0.7}
                >
                  <ThemedText style={[styles.lovItemText, styles.codeCell, { flex: 1.4 }]}>
                    {item.TrayId}
                  </ThemedText>
                  <ThemedText style={[styles.lovItemText, { flex: 2.2 }]} numberOfLines={1}>
                    {item.TrayName}
                  </ThemedText>
                  <ThemedText style={[styles.lovItemText, { flex: 1.4 }]} numberOfLines={1}>
                    {item.SalesManName || '-'}
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
  trayBadge: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFF4DA',
    paddingHorizontal: 8,
    paddingVertical: 2,
    borderRadius: 4,
    borderWidth: 1,
    borderColor: '#B8903B',
  },
  trayBadgeLabel: {
    fontSize: 10,
    color: '#7D5E19',
    marginRight: 4,
  },
  trayBadgeValue: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#8A5D00',
  },
  destTrayBadge: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#E8F5E9',
    paddingHorizontal: 8,
    paddingVertical: 2,
    borderRadius: 4,
    borderWidth: 1,
    borderColor: '#81C784',
  },
  destTrayBadgeLabel: {
    fontSize: 10,
    color: '#2E7D32',
    marginRight: 4,
  },
  destTrayBadgeValue: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#1B5E20',
  },

  // ===== GRIDS & INLINE ROWS =====
  twoColumnGrid: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    gap: 8,
  },
  singleColumnRow: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  inlineFieldRow: {
    flex: 1,
    minWidth: 260,
    flexDirection: 'row',
    alignItems: 'center',
  },
  inlineFieldLabel: {
    width: 105,
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3B2D17',
    marginRight: 4,
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

  // Input Combo
  inputWithCombo: {
    flex: 1,
    flexDirection: 'row',
    alignItems: 'center',
    flexShrink: 0,
  },
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
    justifyContent: 'center',
    alignItems: 'center',
    flexShrink: 0,
  },
  batchItemsBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    height: 26,
    backgroundColor: '#FFF1D6',
    borderWidth: 1,
    borderLeftWidth: 0,
    borderColor: '#D4C3A3',
    borderTopRightRadius: 4,
    borderBottomRightRadius: 4,
    paddingHorizontal: 7,
    flexShrink: 0,
  },
  batchItemsBtnText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#7D5E19',
    marginLeft: 3,
  },

  // Multi-select indicator
  multiSelectIndicatorBanner: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#E8F5E9',
    borderRadius: 4,
    borderWidth: 1,
    borderColor: '#A5D6A7',
    paddingHorizontal: 10,
    paddingVertical: 4,
    marginTop: 8,
  },
  multiSelectIndicatorText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#1B5E20',
  },

  // Customer Consignment Pill
  customerTrayPill: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FCFAF6',
    borderWidth: 1,
    borderColor: '#E2D3B3',
    borderRadius: 4,
    paddingHorizontal: 8,
    paddingVertical: 4,
  },
  customerTrayPillActive: {
    backgroundColor: '#FFF4DA',
    borderColor: '#B8903B',
  },
  customerTrayText: {
    fontSize: 11,
    fontWeight: '500',
    color: '#5C4A28',
  },
  customerTrayTextActive: {
    fontWeight: 'bold',
    color: '#9E6D08',
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
    paddingVertical: 6,
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

  // ===== MODALS =====
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
  lovModalCardLarge: {
    width: '95%',
    maxWidth: 720,
    maxHeight: '85%',
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
  modalBarcodeRow: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FCFAF6',
    padding: 8,
    borderBottomWidth: 1,
    borderBottomColor: '#E8DCB8',
    gap: 8,
  },
  modalSelectAllBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#D4C3A3',
    borderRadius: 4,
    paddingHorizontal: 8,
    height: 30,
    gap: 4,
  },
  modalSelectAllText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3B2D17',
  },
  modalBarcodeScanner: {
    flex: 1,
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#D4C3A3',
    borderRadius: 4,
    paddingHorizontal: 6,
    height: 30,
  },
  modalBarcodeInput: {
    flex: 1,
    fontSize: 12,
    color: '#2B2118',
    paddingVertical: 0,
  },
  barcodeGoBtn: {
    backgroundColor: '#F5E7CA',
    borderWidth: 1,
    borderColor: '#D4C3A3',
    borderRadius: 3,
    paddingHorizontal: 8,
    height: 22,
    justifyContent: 'center',
    alignItems: 'center',
  },
  barcodeGoBtnText: {
    fontSize: 10,
    fontWeight: 'bold',
    color: '#7D5E19',
  },
  modalBottomBar: {
    flexDirection: 'row',
    justifyContent: 'flex-end',
    alignItems: 'center',
    padding: 10,
    backgroundColor: '#FAF7F0',
    borderTopWidth: 1,
    borderTopColor: '#E8DCB8',
    gap: 8,
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
