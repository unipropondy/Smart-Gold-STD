import { Ionicons } from '@expo/vector-icons';
import { Stack, useRouter } from 'expo-router';
import React, { useState } from 'react';
import {
  ActivityIndicator,
  FlatList,
  Modal,
  ScrollView,
  StyleSheet,
  TextInput,
  TouchableOpacity,
  View,
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';

import { ThemedText } from '@/components/themed-text';
import { API_BASE_URL } from '@/constants/api';
import { useThemeColors } from '@/hooks/useThemeColors';

const API_URL = `${API_BASE_URL}/orderinventory`;

interface GridItem {
  OrderInventoryCode: string;
  OrderDescription: string;
  InventoryCode: string;
  Description: string;
  OrderWeight: number;
  InvWeight: number;
}

export default function OrderInventoryScreen() {
  const themeColors = useThemeColors();
  const router = useRouter();

  // Loading indicator states
  const [loading, setLoading] = useState(false);
  const [saving, setSaving] = useState(false);
  const [toast, setToast] = useState<{ message: string; type: 'success' | 'error' } | null>(null);

  // Header State
  const [orderNo, setOrderNo] = useState('');
  const [customerCode, setCustomerCode] = useState('');
  const [customerName, setCustomerName] = useState('');
  const [customerType, setCustomerType] = useState('Customer Type');
  const [address1, setAddress1] = useState('');
  const [address2, setAddress2] = useState('');
  const [address3, setAddress3] = useState('');
  const [remarks, setRemarks] = useState('');

  // Grid / Table Items state
  const [gridItems, setGridItems] = useState<GridItem[]>([]);
  const [selectedGridIndex, setSelectedGridIndex] = useState<number | null>(null);

  // Sub-Form Detail state (Left Side - Ordered Item)
  const [orderInventoryCode, setOrderInventoryCode] = useState('');
  const [orderDescription, setOrderDescription] = useState('');
  const [estimatedWeight, setEstimatedWeight] = useState('');

  // Sub-Form Detail state (Right Side - Physical Item)
  const [inventoryCode, setInventoryCode] = useState('');
  const [description, setDescription] = useState('');
  const [weight, setWeight] = useState('');

  // Search/Lookup Modals state
  const [lookupType, setLookupType] = useState<'order' | 'detail' | 'inventory' | null>(null);
  const [lookupSearch, setLookupSearch] = useState('');
  const [ordersList, setOrdersList] = useState<any[]>([]);
  const [orderDetailsList, setOrderDetailsList] = useState<any[]>([]);
  const [inventoryStockList, setInventoryStockList] = useState<any[]>([]);

  const showToast = (message: string, type: 'success' | 'error' = 'success') => {
    setToast({ message, type });
    setTimeout(() => setToast(null), 3000);
  };

  // Reset/Clear sub-form fields
  const handleClearDetails = () => {
    setOrderInventoryCode('');
    setOrderDescription('');
    setEstimatedWeight('');
    setInventoryCode('');
    setDescription('');
    setWeight('');
  };

  // Reset/Clear everything
  const handleClearAll = () => {
    setOrderNo('');
    setCustomerCode('');
    setCustomerName('');
    setCustomerType('Customer Type');
    setAddress1('');
    setAddress2('');
    setAddress3('');
    setRemarks('');
    setGridItems([]);
    setSelectedGridIndex(null);
    handleClearDetails();
  };

  // Load Order Details & Mapped Items
  const loadOrderDetails = async (selectedOrderNo: string) => {
    if (!selectedOrderNo) return;
    const trimmedOrderNo = selectedOrderNo.trim();
    if (!trimmedOrderNo) return;
    try {
      setLoading(true);
      const headerRes = await fetch(`${API_URL}/order-header/${encodeURIComponent(trimmedOrderNo)}`);
      const headerJson = await headerRes.json();

      if (headerJson.success && headerJson.data) {
        const header = headerJson.data;
        const rawCustCode = header.CustomerCode || header.customercode || header.CUSTOMERCODE || '';
        const rawCustName = header.CustomerName || header.customername || header.CUSTOMERNAME || '';
        const rawCustType = header.CustomerType || header.customertype || header.CUSTOMERTYPE || 'Customer Type';
        const rawAddr1 = header.Address1 || header.address1 || header.ADDRESS1 || '';
        const rawAddr2 = header.Address2 || header.address2 || header.ADDRESS2 || '';
        const rawAddr3 = header.Address3 || header.address3 || header.ADDRESS3 || '';

        setCustomerCode(rawCustCode ? String(rawCustCode).trim() : '');
        setCustomerName(rawCustName ? String(rawCustName).trim() : '');
        setCustomerType(rawCustType ? String(rawCustType).trim() : 'Customer Type');
        setAddress1(rawAddr1 ? String(rawAddr1).trim() : '');
        setAddress2(rawAddr2 ? String(rawAddr2).trim() : '');
        setAddress3(rawAddr3 ? String(rawAddr3).trim() : '');

        // Fetch Mapped Items for this order
        const mappedRes = await fetch(`${API_URL}/order-details-mapped/${encodeURIComponent(trimmedOrderNo)}`);
        const mappedJson = await mappedRes.json();
        if (mappedJson.success && mappedJson.data) {
          const items = mappedJson.data.map((item: any) => {
            const orderInvCode = item.OrderInventoryCode || item.orderinventorycode || item.ORDERINVENTORYCODE || '';
            const orderDesc = item.OrderDescription || item.orderdescription || item.ORDERDESCRIPTION || '';
            const invCode = item.InventoryCode || item.inventorycode || item.INVENTORYCODE || '';
            const desc = item.Description || item.description || item.DESCRIPTION || '';
            const orderWt = item.OrderWeight || item.orderweight || item.ORDERWEIGHT || 0;
            const invWt = item.InvWeight || item.invweight || item.INVWEIGHT || 0;

            return {
              OrderInventoryCode: orderInvCode ? String(orderInvCode).trim() : '',
              OrderDescription: orderDesc ? String(orderDesc).trim() : '',
              InventoryCode: invCode ? String(invCode).trim() : '',
              Description: desc ? String(desc).trim() : '',
              OrderWeight: parseFloat(orderWt) || 0,
              InvWeight: parseFloat(invWt) || 0,
            };
          });
          setGridItems(items);
        }
        showToast('Order details loaded', 'success');
      } else {
        showToast(headerJson.message || 'Could not find the OrderNo.', 'error');
      }
    } catch (err) {
      console.error(err);
      showToast('Failed to fetch order details', 'error');
    } finally {
      setLoading(false);
    }
  };

  // Lost Focus or search completion on Physical Inventory Code
  const loadPhysicalInventory = async (code: string) => {
    if (!code) return;
    const trimmedCode = code.trim();
    if (!trimmedCode) return;
    try {
      setLoading(true);
      const res = await fetch(`${API_URL}/inventory/${encodeURIComponent(trimmedCode)}`);
      const json = await res.json();
      if (json.success && json.data) {
        const inv = json.data;
        const desc = inv.Description || inv.description || inv.DESCRIPTION || '';
        const wt = inv.Weight || inv.weight || inv.WEIGHT || 0;
        const stoneWt = inv.StoneWeight || inv.stoneweight || inv.STONEWEIGHT || 0;

        setDescription(desc ? String(desc).trim() : '');
        const finalWeight = (parseFloat(wt) || 0) - (parseFloat(stoneWt) || 0);
        setWeight(finalWeight.toString());
      } else {
        showToast('InventoryCode Not Found', 'error');
        setInventoryCode('');
        setDescription('');
        setWeight('');
      }
    } catch (err) {
      console.error(err);
      showToast('Failed to fetch inventory details', 'error');
    } finally {
      setLoading(false);
    }
  };

  // Open LOV modals
  const openLookup = async (type: 'order' | 'detail' | 'inventory') => {
    setLookupSearch('');
    setLookupType(type);
    try {
      setLoading(true);
      if (type === 'order') {
        const res = await fetch(`${API_URL}/orders?t=${Date.now()}`);
        const json = await res.json();
        if (json.success) setOrdersList(json.data || []);
      } else if (type === 'detail') {
        if (!orderNo.trim()) {
          showToast('Please select or enter an Order No first.', 'error');
          setLookupType(null);
          return;
        }
        const res = await fetch(`${API_URL}/order-details/${encodeURIComponent(orderNo.trim())}?t=${Date.now()}`);
        const json = await res.json();
        if (json.success) setOrderDetailsList(json.data || []);
      } else if (type === 'inventory') {
        const res = await fetch(`${API_URL}/inventory-stock?locationCode=HQ&t=${Date.now()}`);
        const json = await res.json();
        if (json.success) setInventoryStockList(json.data || []);
      }
    } catch (err) {
      console.error(err);
      showToast('Failed to load search data.', 'error');
    } finally {
      setLoading(false);
    }
  };

  // Add Item to Grid
  const handleAddItem = () => {
    if (!orderInventoryCode.trim()) {
      showToast('Please select an Order Detail / Description first.', 'error');
      return;
    }
    if (!inventoryCode.trim()) {
      showToast('Please select a Physical Inventory Code.', 'error');
      return;
    }

    const orderInvDup = gridItems.some(
      (item, idx) => item.OrderInventoryCode === orderInventoryCode && idx !== selectedGridIndex
    );
    const invDup = gridItems.some(
      (item, idx) => item.InventoryCode === inventoryCode && idx !== selectedGridIndex
    );

    if (orderInvDup || invDup) {
      showToast('The Item is already added into the grid. Choose another item.', 'error');
      return;
    }

    const newItem: GridItem = {
      OrderInventoryCode: orderInventoryCode.trim(),
      OrderDescription: orderDescription.trim(),
      InventoryCode: inventoryCode.trim(),
      Description: description.trim(),
      OrderWeight: parseFloat(estimatedWeight) || 0,
      InvWeight: parseFloat(weight) || 0,
    };

    if (selectedGridIndex !== null) {
      const updated = [...gridItems];
      updated[selectedGridIndex] = newItem;
      setGridItems(updated);
      setSelectedGridIndex(null);
      showToast('Mapping updated', 'success');
    } else {
      setGridItems([...gridItems, newItem]);
      showToast('Item added to grid', 'success');
    }

    handleClearDetails();
  };

  // Delete Item from Grid
  const handleDeleteItem = () => {
    if (selectedGridIndex === null) {
      showToast('Please select a row in the grid to delete.', 'error');
      return;
    }
    const updated = [...gridItems];
    updated.splice(selectedGridIndex, 1);
    setGridItems(updated);
    setSelectedGridIndex(null);
    handleClearDetails();
    showToast('Item removed', 'success');
  };

  // Save mappings to DB
  const handleSave = async () => {
    if (!orderNo.trim()) {
      showToast('Order No is required to save.', 'error');
      return;
    }
    if (gridItems.length === 0) {
      showToast('No inventory mappings added to the grid.', 'error');
      return;
    }

    try {
      setSaving(true);
      const res = await fetch(`${API_URL}/save`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          orderNo: orderNo.trim(),
          items: gridItems.map((item) => ({
            OrderInventoryCode: item.OrderInventoryCode,
            InventoryCode: item.InventoryCode,
          })),
          userId: 'sys',
        }),
      });

      const json = await res.json();
      if (json.success) {
        showToast('Order Inventory mapping saved successfully.', 'success');
        handleClearAll();
      } else {
        showToast(json.message || 'Failed to save mapping.', 'error');
      }
    } catch (err) {
      console.error(err);
      showToast('Network error. Failed to save.', 'error');
    } finally {
      setSaving(false);
    }
  };

  // Select item from Grid to edit/view
  const handleSelectGridItem = (item: GridItem, index: number) => {
    if (selectedGridIndex === index) {
      setSelectedGridIndex(null);
      handleClearDetails();
      return;
    }
    setSelectedGridIndex(index);
    setOrderInventoryCode(item.OrderInventoryCode);
    setOrderDescription(item.OrderDescription);
    setEstimatedWeight(item.OrderWeight.toString());
    setInventoryCode(item.InventoryCode);
    setDescription(item.Description);
    setWeight(item.InvWeight.toString());
  };

  // Filtered lists for LOVs
  const getFilteredOrders = () => {
    const term = lookupSearch.toLowerCase();
    return ordersList.filter(
      (o) =>
        (o.OrderNo && o.OrderNo.toLowerCase().includes(term)) ||
        (o.CustomerCode && o.CustomerCode.toLowerCase().includes(term)) ||
        (o.Description && o.Description.toLowerCase().includes(term))
    );
  };

  const getFilteredDetails = () => {
    const term = lookupSearch.toLowerCase();
    return orderDetailsList.filter(
      (d) =>
        (d.OrderInventoryCode && d.OrderInventoryCode.toLowerCase().includes(term)) ||
        (d.Description && d.Description.toLowerCase().includes(term))
    );
  };

  const getFilteredInventory = () => {
    const term = lookupSearch.toLowerCase();
    return inventoryStockList.filter(
      (i) =>
        (i.InventoryCode && i.InventoryCode.toLowerCase().includes(term)) ||
        (i.Description && i.Description.toLowerCase().includes(term))
    );
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
            <ThemedText style={styles.headerTitle}>Order Inventory Mapping</ThemedText>
            <ThemedText style={styles.headerSubtitle}>Special Order to Physical Stock Allocation</ThemedText>
          </View>
          {(loading || saving) && <ActivityIndicator size="small" color="#B48218" style={{ marginLeft: 8 }} />}
        </View>

        <View style={styles.headerRight}>
          <TouchableOpacity
            style={[styles.actionBtn, styles.clearBtn]}
            onPress={handleClearAll}
            activeOpacity={0.8}
            disabled={saving || loading}
          >
            <Ionicons name="refresh-outline" size={14} color="#5C4A28" />
            <ThemedText style={styles.clearBtnText}>Clear</ThemedText>
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
                <ThemedText style={styles.saveBtnText}>Save Mapping</ThemedText>
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

      <ScrollView contentContainerStyle={styles.scrollContainer} keyboardShouldPersistTaps="handled">
        {/* CARD 1: ORDER HEADER DETAILS (SINGLE ROW ALIGNMENT) */}
        <View style={styles.goldCard}>
          <View style={styles.cardHeaderBanner}>
            <View style={styles.headerTitleContainer}>
              <View style={styles.headerIconBadgeSmall}>
                <Ionicons name="document-text-outline" size={13} color="#B48218" />
              </View>
              <ThemedText style={styles.cardHeaderTitleSmall}>Order Information</ThemedText>
            </View>

            {orderNo ? (
              <View style={styles.orderNoBadge}>
                <ThemedText style={styles.orderNoBadgeLabel}>Order:</ThemedText>
                <ThemedText style={styles.orderNoBadgeValue}>{orderNo}</ThemedText>
              </View>
            ) : null}
          </View>

          <View style={styles.cardBody}>
            {/* Row 1: Order No, Customer Code + Tag, Customer Name */}
            <View style={styles.threeColumnGrid}>
              {/* Order No */}
              <View style={styles.inlineFieldRow}>
                <ThemedText style={styles.inlineFieldLabel}>Order No *</ThemedText>
                <View style={styles.inputWithLookup}>
                  <TextInput
                    style={[styles.formInput, styles.inputAttached, { flex: 1 }]}
                    value={orderNo}
                    onChangeText={setOrderNo}
                    onBlur={() => loadOrderDetails(orderNo)}
                    placeholder="Enter/Search Order..."
                    placeholderTextColor="#9E8D77"
                  />
                  <TouchableOpacity
                    style={styles.attachedLookupBtn}
                    onPress={() => openLookup('order')}
                    activeOpacity={0.7}
                  >
                    <Ionicons name="search" size={13} color="#7D5E19" />
                  </TouchableOpacity>
                </View>
              </View>

              {/* Customer Code */}
              <View style={styles.inlineFieldRow}>
                <ThemedText style={styles.inlineFieldLabel}>Customer Code</ThemedText>
                <View style={{ flex: 1, flexDirection: 'row', alignItems: 'center', gap: 4 }}>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1 }]}
                    value={customerCode}
                    editable={false}
                    placeholder="Auto-filled"
                    placeholderTextColor="#9E8D77"
                  />
                  <View style={styles.customerTypeBadge}>
                    <ThemedText style={styles.customerTypeBadgeText}>
                      {customerType === 'Customer Type' ? 'DIAMOND' : customerType}
                    </ThemedText>
                  </View>
                </View>
              </View>

              {/* Customer Name */}
              <View style={styles.inlineFieldRow}>
                <ThemedText style={styles.inlineFieldLabel}>Customer Name</ThemedText>
                <TextInput
                  style={[styles.formInput, styles.readonlyInput, { flex: 1 }]}
                  value={customerName}
                  editable={false}
                  placeholder="Auto-filled"
                  placeholderTextColor="#9E8D77"
                />
              </View>
            </View>

            {/* Row 2: Addresses & Remarks */}
            <View style={[styles.threeColumnGrid, { marginTop: 6 }]}>
              {/* Address 1 */}
              <View style={styles.inlineFieldRow}>
                <ThemedText style={styles.inlineFieldLabel}>Address 1</ThemedText>
                <TextInput
                  style={[styles.formInput, styles.readonlyInput, { flex: 1 }]}
                  value={address1}
                  editable={false}
                  placeholder="Line 1"
                  placeholderTextColor="#9E8D77"
                />
              </View>

              {/* Address 2 & 3 */}
              <View style={styles.inlineFieldRow}>
                <ThemedText style={styles.inlineFieldLabel}>Address 2/3</ThemedText>
                <View style={{ flex: 1, flexDirection: 'row', gap: 4 }}>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1 }]}
                    value={address2}
                    editable={false}
                    placeholder="Line 2"
                    placeholderTextColor="#9E8D77"
                  />
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1 }]}
                    value={address3}
                    editable={false}
                    placeholder="Line 3"
                    placeholderTextColor="#9E8D77"
                  />
                </View>
              </View>

              {/* Remarks */}
              <View style={styles.inlineFieldRow}>
                <ThemedText style={styles.inlineFieldLabel}>Remarks</ThemedText>
                <TextInput
                  style={[styles.formInput, { flex: 1 }]}
                  value={remarks}
                  onChangeText={setRemarks}
                  maxLength={100}
                  placeholder="Mapping notes..."
                  placeholderTextColor="#9E8D77"
                />
              </View>
            </View>
          </View>
        </View>

        {/* CARD 2: MAPPED ITEMS DATA TABLE (100% FULL WIDTH) */}
        <View style={[styles.goldCard, { marginTop: 10 }]}>
          <View style={styles.cardHeaderBanner}>
            <View style={styles.headerTitleContainer}>
              <View style={styles.headerIconBadgeSmall}>
                <Ionicons name="git-network-outline" size={13} color="#B48218" />
              </View>
              <ThemedText style={styles.cardHeaderTitleSmall}>Mapped Inventory Allocations</ThemedText>
              <View style={styles.counterBadge}>
                <ThemedText style={styles.counterBadgeText}>{gridItems.length} mapped</ThemedText>
              </View>
            </View>

            {selectedGridIndex !== null && (
              <View style={styles.selectedRowPill}>
                <ThemedText style={styles.selectedRowPillText}>
                  Selected Row #{selectedGridIndex + 1}
                </ThemedText>
              </View>
            )}
          </View>

          {/* Table Header (100% Full Width) */}
          <View style={styles.tableHeadRow}>
            <ThemedText style={[styles.tableHeadCell, { flex: 1.4 }]}>Order Inv Code</ThemedText>
            <ThemedText style={[styles.tableHeadCell, { flex: 2 }]}>Order Description</ThemedText>
            <ThemedText style={[styles.tableHeadCell, { flex: 1.4 }]}>Physical Inv Code</ThemedText>
            <ThemedText style={[styles.tableHeadCell, { flex: 2 }]}>Physical Description</ThemedText>
            <ThemedText style={[styles.tableHeadCell, { flex: 0.9, textAlign: 'right' }]}>Order Wt</ThemedText>
            <ThemedText style={[styles.tableHeadCell, { flex: 0.9, textAlign: 'right' }]}>Inv Wt</ThemedText>
          </View>

          {/* Table Body */}
          {gridItems.length === 0 ? (
            <View style={styles.emptyTableContainer}>
              <Ionicons name="cube-outline" size={28} color="#D4C3A3" />
              <ThemedText style={styles.emptyTableText}>
                No mapped items. Select an order detail and link physical inventory below.
              </ThemedText>
            </View>
          ) : (
            gridItems.map((item, index) => {
              const isSelected = selectedGridIndex === index;
              return (
                <TouchableOpacity
                  key={index}
                  style={[styles.tableRow, isSelected && styles.tableRowSelected]}
                  onPress={() => handleSelectGridItem(item, index)}
                  activeOpacity={0.7}
                >
                  <ThemedText style={[styles.tableCell, styles.codeCell, { flex: 1.4 }]}>
                    {item.OrderInventoryCode}
                  </ThemedText>
                  <ThemedText style={[styles.tableCell, { flex: 2 }]} numberOfLines={1}>
                    {item.OrderDescription}
                  </ThemedText>
                  <ThemedText style={[styles.tableCell, styles.codeCell, { flex: 1.4 }]}>
                    {item.InventoryCode}
                  </ThemedText>
                  <ThemedText style={[styles.tableCell, { flex: 2 }]} numberOfLines={1}>
                    {item.Description}
                  </ThemedText>
                  <ThemedText style={[styles.tableCell, { flex: 0.9, textAlign: 'right' }]}>
                    {item.OrderWeight.toFixed(3)}
                  </ThemedText>
                  <ThemedText style={[styles.tableCell, styles.amountCell, { flex: 0.9, textAlign: 'right' }]}>
                    {item.InvWeight.toFixed(3)}
                  </ThemedText>
                </TouchableOpacity>
              );
            })
          )}

          {/* CARD SUBFORM: DETAIL MAPPING DUAL-COLUMN ENTRY */}
          <View style={styles.mappingSection}>
            <View style={styles.mappingSectionHeader}>
              <ThemedText style={styles.mappingSectionTitle}>
                {selectedGridIndex !== null ? `Edit Mapping Row #${selectedGridIndex + 1}` : 'Create Inventory Link'}
              </ThemedText>
            </View>

            <View style={styles.dualMappingContainer}>
              {/* Left Column: Order Item Details */}
              <View style={[styles.mappingSubCard, { marginRight: 6 }]}>
                <View style={styles.subCardHeader}>
                  <ThemedText style={styles.subCardTitle}>1. Order Specification</ThemedText>
                </View>

                {/* Description with LOV */}
                <View style={styles.inlineFieldRow}>
                  <ThemedText style={styles.inlineFieldLabel}>Description *</ThemedText>
                  <View style={styles.inputWithLookup}>
                    <TextInput
                      style={[styles.formInput, styles.inputAttached, { flex: 1 }]}
                      value={orderDescription}
                      editable={false}
                      placeholder="Select Order Detail..."
                      placeholderTextColor="#9E8D77"
                    />
                    <TouchableOpacity
                      style={styles.attachedLookupBtn}
                      onPress={() => openLookup('detail')}
                      activeOpacity={0.7}
                    >
                      <Ionicons name="search" size={13} color="#7D5E19" />
                    </TouchableOpacity>
                  </View>
                </View>

                {/* Order Inventory Code */}
                <View style={[styles.inlineFieldRow, { marginTop: 4 }]}>
                  <ThemedText style={styles.inlineFieldLabel}>Inv Code</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1 }]}
                    value={orderInventoryCode}
                    editable={false}
                    placeholder="Auto-populated"
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* Estimated Weight */}
                <View style={[styles.inlineFieldRow, { marginTop: 4 }]}>
                  <ThemedText style={styles.inlineFieldLabel}>Est. Weight</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1, textAlign: 'right' }]}
                    value={estimatedWeight}
                    editable={false}
                    placeholder="0.000"
                    placeholderTextColor="#9E8D77"
                  />
                </View>
              </View>

              {/* Right Column: Physical Inventory Assignment */}
              <View style={[styles.mappingSubCard, { marginLeft: 6 }]}>
                <View style={styles.subCardHeader}>
                  <ThemedText style={styles.subCardTitle}>2. Physical Stock Assignment</ThemedText>
                </View>

                {/* Inventory Code with LOV */}
                <View style={styles.inlineFieldRow}>
                  <ThemedText style={styles.inlineFieldLabel}>Stock Code *</ThemedText>
                  <View style={styles.inputWithLookup}>
                    <TextInput
                      style={[styles.formInput, styles.inputAttached, { flex: 1 }]}
                      value={inventoryCode}
                      onChangeText={setInventoryCode}
                      onBlur={() => loadPhysicalInventory(inventoryCode)}
                      placeholder="Enter/Select Stock..."
                      placeholderTextColor="#9E8D77"
                    />
                    <TouchableOpacity
                      style={styles.attachedLookupBtn}
                      onPress={() => openLookup('inventory')}
                      activeOpacity={0.7}
                    >
                      <Ionicons name="search" size={13} color="#7D5E19" />
                    </TouchableOpacity>
                  </View>
                </View>

                {/* Description */}
                <View style={[styles.inlineFieldRow, { marginTop: 4 }]}>
                  <ThemedText style={styles.inlineFieldLabel}>Description</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1 }]}
                    value={description}
                    editable={false}
                    placeholder="Auto-populated"
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* Physical Net Weight */}
                <View style={[styles.inlineFieldRow, { marginTop: 4 }]}>
                  <ThemedText style={styles.inlineFieldLabel}>Net Weight</ThemedText>
                  <TextInput
                    style={[
                      styles.formInput,
                      styles.readonlyInput,
                      styles.highlightTotal,
                      { flex: 1, textAlign: 'right' },
                    ]}
                    value={weight}
                    editable={false}
                    placeholder="0.000"
                    placeholderTextColor="#9E8D77"
                  />
                </View>
              </View>
            </View>

            {/* Action Buttons for Mapping */}
            <View style={styles.mappingActionsRow}>
              <TouchableOpacity
                style={[styles.tableActionBtn, styles.addBtn]}
                onPress={handleAddItem}
                activeOpacity={0.8}
              >
                <Ionicons
                  name={selectedGridIndex !== null ? 'create-outline' : 'add-circle-outline'}
                  size={14}
                  color="#9E6D08"
                  style={{ marginRight: 4 }}
                />
                <ThemedText style={styles.addBtnText}>
                  {selectedGridIndex !== null ? 'Update Row' : 'Link Item'}
                </ThemedText>
              </TouchableOpacity>

              <TouchableOpacity
                style={[styles.tableActionBtn, styles.deleteBtn]}
                onPress={handleDeleteItem}
                activeOpacity={0.8}
              >
                <Ionicons name="trash-outline" size={14} color="#C62828" style={{ marginRight: 4 }} />
                <ThemedText style={styles.deleteBtnText}>Remove Row</ThemedText>
              </TouchableOpacity>

              <TouchableOpacity
                style={[styles.tableActionBtn, styles.clearItemBtn]}
                onPress={handleClearDetails}
                activeOpacity={0.8}
              >
                <Ionicons name="close-circle-outline" size={14} color="#5C4A28" style={{ marginRight: 4 }} />
                <ThemedText style={styles.clearItemBtnText}>Clear Input</ThemedText>
              </TouchableOpacity>
            </View>
          </View>
        </View>
      </ScrollView>

      {/* UNIFIED LOV MODAL */}
      <Modal visible={lookupType !== null} transparent animationType="fade">
        <View style={styles.modalOverlay}>
          <View style={styles.lovModalCard}>
            <View style={styles.modalHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="search" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.modalHeaderTitle}>
                  {lookupType === 'order' && 'SELECT ORDER NO'}
                  {lookupType === 'detail' && 'SELECT ORDER DESCRIPTION'}
                  {lookupType === 'inventory' && 'SELECT PHYSICAL INVENTORY ITEM'}
                </ThemedText>
              </View>
              <TouchableOpacity
                onPress={() => setLookupType(null)}
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
                value={lookupSearch}
                onChangeText={setLookupSearch}
                placeholder="Search records..."
                placeholderTextColor="#9E8D77"
                autoFocus
              />
              {lookupSearch.length > 0 && (
                <TouchableOpacity onPress={() => setLookupSearch('')}>
                  <Ionicons name="close-circle" size={16} color="#9E8D77" />
                </TouchableOpacity>
              )}
            </View>

            {/* Column Headers for LOV */}
            {lookupType === 'order' && (
              <View style={styles.lovHeadRow}>
                <ThemedText style={[styles.lovHeadText, { flex: 1.5 }]}>Order No</ThemedText>
                <ThemedText style={[styles.lovHeadText, { flex: 1.5 }]}>Customer Code</ThemedText>
                <ThemedText style={[styles.lovHeadText, { flex: 2 }]}>Description</ThemedText>
                <ThemedText style={[styles.lovHeadText, { flex: 1, textAlign: 'right' }]}>Est Wt</ThemedText>
              </View>
            )}
            {lookupType === 'detail' && (
              <View style={styles.lovHeadRow}>
                <ThemedText style={[styles.lovHeadText, { flex: 1.5 }]}>Order Inv Code</ThemedText>
                <ThemedText style={[styles.lovHeadText, { flex: 2.5 }]}>Description</ThemedText>
                <ThemedText style={[styles.lovHeadText, { flex: 1, textAlign: 'right' }]}>Est Wt</ThemedText>
              </View>
            )}
            {lookupType === 'inventory' && (
              <View style={styles.lovHeadRow}>
                <ThemedText style={[styles.lovHeadText, { flex: 1.5 }]}>Inventory Code</ThemedText>
                <ThemedText style={[styles.lovHeadText, { flex: 2.5 }]}>Description</ThemedText>
                <ThemedText style={[styles.lovHeadText, { flex: 1, textAlign: 'right' }]}>Weight</ThemedText>
              </View>
            )}

            <FlatList
              data={
                lookupType === 'order'
                  ? getFilteredOrders()
                  : lookupType === 'detail'
                  ? getFilteredDetails()
                  : lookupType === 'inventory'
                  ? getFilteredInventory()
                  : []
              }
              keyExtractor={(_, index) => index.toString()}
              initialNumToRender={50}
              maxToRenderPerBatch={50}
              ListEmptyComponent={
                <View style={styles.emptyTableContainer}>
                  <Ionicons name="search-outline" size={24} color="#D4C3A3" />
                  <ThemedText style={styles.emptyTableText}>No matching records found.</ThemedText>
                </View>
              }
              renderItem={({ item }) => {
                if (lookupType === 'order') {
                  const orderNoVal = item.OrderNo || item.orderno || item.ORDERNO || '';
                  const custCodeVal = item.CustomerCode || item.customercode || item.CUSTOMERCODE || '';
                  const descVal = item.Description || item.description || item.DESCRIPTION || '';
                  const weightEstVal = item.WeightEstimated || item.weightestimated || item.WEIGHTESTIMATED || 0;

                  return (
                    <TouchableOpacity
                      style={styles.lovItemRow}
                      onPress={() => {
                        setOrderNo(orderNoVal ? String(orderNoVal).trim() : '');
                        loadOrderDetails(String(orderNoVal));
                        setLookupType(null);
                      }}
                      activeOpacity={0.7}
                    >
                      <ThemedText style={[styles.lovItemText, styles.codeCell, { flex: 1.5 }]}>
                        {String(orderNoVal).trim()}
                      </ThemedText>
                      <ThemedText style={[styles.lovItemText, { flex: 1.5 }]}>
                        {String(custCodeVal).trim()}
                      </ThemedText>
                      <ThemedText style={[styles.lovItemText, { flex: 2 }]} numberOfLines={1}>
                        {String(descVal).trim()}
                      </ThemedText>
                      <ThemedText style={[styles.lovItemText, { flex: 1, textAlign: 'right' }]}>
                        {parseFloat(weightEstVal).toFixed(3)}
                      </ThemedText>
                    </TouchableOpacity>
                  );
                } else if (lookupType === 'detail') {
                  const orderInvCodeVal = item.OrderInventoryCode || item.orderinventorycode || item.ORDERINVENTORYCODE || '';
                  const descVal = item.Description || item.description || item.DESCRIPTION || '';
                  const weightEstVal = item.WeightEstimated || item.weightestimated || item.WEIGHTESTIMATED || 0;

                  return (
                    <TouchableOpacity
                      style={styles.lovItemRow}
                      onPress={() => {
                        setOrderInventoryCode(orderInvCodeVal ? String(orderInvCodeVal).trim() : '');
                        setOrderDescription(descVal ? String(descVal).trim() : '');
                        setEstimatedWeight(weightEstVal.toString());
                        setLookupType(null);
                      }}
                      activeOpacity={0.7}
                    >
                      <ThemedText style={[styles.lovItemText, styles.codeCell, { flex: 1.5 }]}>
                        {String(orderInvCodeVal).trim()}
                      </ThemedText>
                      <ThemedText style={[styles.lovItemText, { flex: 2.5 }]} numberOfLines={1}>
                        {String(descVal).trim()}
                      </ThemedText>
                      <ThemedText style={[styles.lovItemText, { flex: 1, textAlign: 'right' }]}>
                        {parseFloat(weightEstVal).toFixed(3)}
                      </ThemedText>
                    </TouchableOpacity>
                  );
                } else {
                  const invCodeVal = item.InventoryCode || item.inventorycode || item.INVENTORYCODE || '';
                  const descVal = item.Description || item.description || item.DESCRIPTION || '';
                  const weightVal = item.Weight || item.weight || item.WEIGHT || 0;

                  return (
                    <TouchableOpacity
                      style={styles.lovItemRow}
                      onPress={() => {
                        setInventoryCode(invCodeVal ? String(invCodeVal).trim() : '');
                        loadPhysicalInventory(String(invCodeVal));
                        setLookupType(null);
                      }}
                      activeOpacity={0.7}
                    >
                      <ThemedText style={[styles.lovItemText, styles.codeCell, { flex: 1.5 }]}>
                        {String(invCodeVal).trim()}
                      </ThemedText>
                      <ThemedText style={[styles.lovItemText, { flex: 2.5 }]} numberOfLines={1}>
                        {String(descVal).trim()}
                      </ThemedText>
                      <ThemedText style={[styles.lovItemText, { flex: 1, textAlign: 'right' }]}>
                        {parseFloat(weightVal).toFixed(3)}
                      </ThemedText>
                    </TouchableOpacity>
                  );
                }
              }}
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
  orderNoBadge: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFF4DA',
    paddingHorizontal: 8,
    paddingVertical: 2,
    borderRadius: 4,
    borderWidth: 1,
    borderColor: '#B8903B',
  },
  orderNoBadgeLabel: {
    fontSize: 10,
    color: '#7D5E19',
    marginRight: 4,
  },
  orderNoBadgeValue: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#8A5D00',
  },
  cardBody: {
    padding: 10,
  },

  // ===== THREE COLUMN GRID =====
  threeColumnGrid: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    gap: 10,
  },
  inlineFieldRow: {
    flex: 1,
    minWidth: 260,
    flexDirection: 'row',
    alignItems: 'center',
  },
  inlineFieldLabel: {
    width: 100,
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3B2D17',
    marginRight: 4,
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

  // Input with attached lookup button
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
  },

  customerTypeBadge: {
    height: 26,
    paddingHorizontal: 8,
    backgroundColor: '#EFE7F6',
    borderWidth: 1,
    borderColor: '#CEBDE0',
    borderRadius: 4,
    justifyContent: 'center',
    alignItems: 'center',
  },
  customerTypeBadgeText: {
    fontSize: 10,
    fontWeight: 'bold',
    color: '#6A2A94',
    letterSpacing: 0.3,
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
  selectedRowPill: {
    backgroundColor: '#FFF3D4',
    paddingHorizontal: 8,
    paddingVertical: 2,
    borderRadius: 4,
    borderWidth: 1,
    borderColor: '#B8903B',
  },
  selectedRowPillText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#8A5D00',
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
    backgroundColor: '#FFF4DA',
    borderLeftWidth: 3,
    borderLeftColor: '#B8903B',
  },
  tableCell: {
    fontSize: 11,
    color: '#3B2D17',
    paddingHorizontal: 4,
  },
  codeCell: {
    fontFamily: 'monospace',
    fontWeight: 'bold',
    color: '#8A5D00',
  },
  amountCell: {
    fontWeight: 'bold',
    color: '#1B5E20',
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

  // ===== MAPPING SUBFORM =====
  mappingSection: {
    borderTopWidth: 1,
    borderTopColor: '#E8DCB8',
    backgroundColor: '#FCFAF6',
    padding: 10,
  },
  mappingSectionHeader: {
    marginBottom: 8,
  },
  mappingSectionTitle: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#8D6409',
    textTransform: 'uppercase',
    letterSpacing: 0.4,
  },
  dualMappingContainer: {
    flexDirection: 'row',
    flexWrap: 'wrap',
  },
  mappingSubCard: {
    flex: 1,
    minWidth: 300,
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#E8DCB8',
    borderRadius: 6,
    padding: 10,
  },
  subCardHeader: {
    marginBottom: 8,
    paddingBottom: 4,
    borderBottomWidth: 1,
    borderBottomColor: '#F0E8D8',
  },
  subCardTitle: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#7D5E19',
  },
  mappingActionsRow: {
    flexDirection: 'row',
    justifyContent: 'flex-end',
    alignItems: 'center',
    gap: 8,
    marginTop: 10,
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
  deleteBtn: {
    backgroundColor: '#FFEBEE',
    borderColor: '#EF9A9A',
  },
  deleteBtnText: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#C62828',
  },
  clearItemBtn: {
    backgroundColor: '#FFFFFF',
    borderColor: '#DFD5C3',
  },
  clearItemBtnText: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#5C4A28',
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
    maxWidth: 680,
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
