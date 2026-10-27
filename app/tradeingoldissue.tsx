import { ThemedText } from '@/components/themed-text';
import { API_BASE_URL } from '@/constants/api';
import { useThemeColors } from '@/hooks/useThemeColors';
import { Ionicons } from '@expo/vector-icons';
import { Stack, useRouter } from 'expo-router';
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

const API_URL = `${API_BASE_URL}/tradeingoldissue`;

const CLASSES = ['22K', '24K', '18K', '835', '750', '916', '875', '20K', '21K'];

export default function TradeInGoldIssueScreen() {
  const themeColors = useThemeColors();
  const router = useRouter();

  // States
  const [loading, setLoading] = useState(false);
  const [saving, setSaving] = useState(false);
  const [toast, setToast] = useState<{ message: string; type: 'success' | 'error' } | null>(null);

  // Header Details
  const [invoiceNo, setInvoiceNo] = useState('');
  const [customerCode, setCustomerCode] = useState('');
  const [customerName, setCustomerName] = useState('');
  const [address1, setAddress1] = useState('');
  const [address2, setAddress2] = useState('');
  const [address3, setAddress3] = useState('');

  // Settings
  const [lossPerc, setLossPerc] = useState('0');
  const [costPerGram, setCostPerGram] = useState('0');
  const [remarks, setRemarks] = useState('');
  const [selectedClass, setSelectedClass] = useState('22K');

  // Input fields for grid
  const [description, setDescription] = useState('22K ITEMS');
  const [diamondDescription, setDiamondDescription] = useState('');
  const [weight, setWeight] = useState('');
  const [stoneWeight, setStoneWeight] = useState('');
  const [goldLoss, setGoldLoss] = useState('');
  const [totalCost, setTotalCost] = useState('');

  // Grid Data
  const [gridData, setGridData] = useState<any[]>([]);

  // LOV State
  const [lovVisible, setLovVisible] = useState(false);
  const [lovData, setLovData] = useState<any[]>([]);
  const [lovSearch, setLovSearch] = useState('');

  // Summary
  const salesAmount = 0.0;
  const tradeInAmount = gridData.reduce((sum, item) => sum + (parseFloat(item.totalCost) || 0), 0);
  const balanceAmount = salesAmount - tradeInAmount;

  const showToast = (message: string, type: 'success' | 'error' = 'success') => {
    setToast({ message, type });
    setTimeout(() => setToast(null), 3000);
  };

  useEffect(() => {
    fetchCostPerGram(selectedClass);
    setDescription(`${selectedClass} ITEMS`);
  }, [selectedClass]);

  const fetchCostPerGram = async (cCode: string) => {
    try {
      const response = await fetch(`${API_URL}/costpergram/${encodeURIComponent(cCode)}`);
      const data = await response.json();
      if (data.success && data.Rate !== undefined) {
        setCostPerGram(String(data.Rate));
      }
    } catch (err) {
      console.error(err);
    }
  };

  const fetchCustomers = async () => {
    try {
      setLoading(true);
      const response = await fetch(`${API_URL}/customers`);
      const data = await response.json();
      if (data.success) {
        setLovData(data.data || []);
        setLovSearch('');
        setLovVisible(true);
      } else {
        showToast('Error loading customer list', 'error');
      }
    } catch (err) {
      showToast('Error fetching customers', 'error');
    } finally {
      setLoading(false);
    }
  };

  const handleSelectCustomer = (item: any) => {
    setCustomerCode(item.CustomerCode || '');
    setCustomerName(item.CustomerName || '');
    setAddress1(item.Address1 || '');
    setAddress2(item.Address2 || '');
    setAddress3(item.Address3 || '');
    setLovVisible(false);
  };

  const lookupCustomerByCode = async (code: string) => {
    if (!code.trim()) return;
    try {
      const response = await fetch(`${API_URL}/customers`);
      const data = await response.json();
      if (data.success && Array.isArray(data.data)) {
        const found = data.data.find(
          (c: any) => (c.CustomerCode || '').trim().toLowerCase() === code.trim().toLowerCase()
        );
        if (found) {
          handleSelectCustomer(found);
        }
      }
    } catch (err) {
      console.error(err);
    }
  };

  const calculateTotalCost = () => {
    const wt = parseFloat(weight) || 0;
    const costPerGm = parseFloat(costPerGram) || 0;
    let calcCost = wt * costPerGm;
    if (selectedClass === '24K') {
      calcCost = calcCost * 1.05;
    }
    setTotalCost(calcCost.toFixed(2));
  };

  const handleAddToGrid = () => {
    if (!description.trim()) {
      showToast('Description is required', 'error');
      return;
    }
    if (!weight || parseFloat(weight) <= 0) {
      showToast('Weight is invalid', 'error');
      return;
    }
    if (parseFloat(goldLoss || '0') < 0) {
      showToast('Gold Loss is invalid', 'error');
      return;
    }
    if (parseFloat(costPerGram || '0') <= 0) {
      showToast('Cost per gram is invalid', 'error');
      return;
    }

    const calculatedCost =
      totalCost && parseFloat(totalCost) > 0
        ? parseFloat(totalCost).toFixed(2)
        : (
            (parseFloat(weight) || 0) * (parseFloat(costPerGram) || 0) * (selectedClass === '24K' ? 1.05 : 1)
          ).toFixed(2);

    const newItem = {
      description: description.trim(),
      diamondDescription: diamondDescription.trim(),
      weight: parseFloat(weight).toFixed(3),
      stoneWeight: parseFloat(stoneWeight || '0').toFixed(3),
      goldLoss: parseFloat(goldLoss || '0').toFixed(3),
      costPerGram: parseFloat(costPerGram).toFixed(2),
      totalCost: calculatedCost,
      class: selectedClass,
    };

    setGridData([...gridData, newItem]);

    // Clear inputs for next item
    setDescription(`${selectedClass} ITEMS`);
    setDiamondDescription('');
    setWeight('');
    setStoneWeight('');
    setGoldLoss('');
    setTotalCost('');
    showToast('Trade-in item added', 'success');
  };

  const handleDeleteRow = (index: number) => {
    const newData = [...gridData];
    newData.splice(index, 1);
    setGridData(newData);
  };

  const handleClear = () => {
    setCustomerCode('');
    setCustomerName('');
    setAddress1('');
    setAddress2('');
    setAddress3('');
    setRemarks('');
    setGridData([]);
    setWeight('');
    setStoneWeight('');
    setGoldLoss('');
    setTotalCost('');
    setSelectedClass('22K');
    setInvoiceNo('');
  };

  const handleSave = async () => {
    if (!customerCode.trim()) {
      showToast('Please select a customer first.', 'error');
      return;
    }
    if (gridData.length === 0) {
      showToast('Please add at least one trade-in item.', 'error');
      return;
    }

    try {
      setSaving(true);
      const payload = {
        customerCode,
        customerName,
        address1,
        address2,
        address3,
        lossPerc,
        costPerGram,
        remarks,
        items: gridData,
      };

      const response = await fetch(`${API_URL}/save`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload),
      });
      const data = await response.json();

      if (data.success) {
        setInvoiceNo(data.invoiceNo || 'TI-SAVED');
        showToast(data.message || 'Trade-in Saved Successfully', 'success');
        setTimeout(() => {
          handleClear();
        }, 1500);
      } else {
        showToast(data.message || 'Save failed', 'error');
      }
    } catch (err: any) {
      showToast('Network error while saving trade-in', 'error');
    } finally {
      setSaving(false);
    }
  };

  const combinedAddress = [address1, address2, address3].filter(Boolean).join(' ');

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
            <ThemedText style={styles.headerTitle}>Trade In Gold Issue</ThemedText>
            <ThemedText style={styles.headerSubtitle}>Old Gold Purchase, Assessment & Settlement</ThemedText>
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
            onPress={() => showToast('Printing trade-in voucher...', 'success')}
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
                <ThemedText style={styles.saveBtnText}>Save Trade-In</ThemedText>
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
          {/* CARD 1: CUSTOMER & INVOICE DETAILS */}
          <View style={styles.goldCard}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="person-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitleSmall}>Customer & Docket Identification</ThemedText>
              </View>

              {invoiceNo ? (
                <View style={styles.invoiceBadge}>
                  <ThemedText style={styles.invoiceBadgeLabel}>Voucher:</ThemedText>
                  <ThemedText style={styles.invoiceBadgeValue}>{invoiceNo}</ThemedText>
                </View>
              ) : null}
            </View>

            <View style={styles.cardBody}>
              {/* Row 1: Invoice No, Customer Code + Lookup, Customer Name */}
              <View style={styles.threeColumnGrid}>
                {/* Invoice No */}
                <View style={[styles.inlineFieldRow, { flex: 1, minWidth: 220 }]}>
                  <ThemedText style={styles.inlineFieldLabel}>Invoice No</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1 }]}
                    value={invoiceNo}
                    editable={false}
                    placeholder="Auto Generated"
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* Customer Code */}
                <View style={[styles.inlineFieldRow, { flex: 1.2, minWidth: 250 }]}>
                  <ThemedText style={styles.inlineFieldLabel}>Customer *</ThemedText>
                  <View style={styles.inputWithLookup}>
                    <TextInput
                      style={[styles.formInput, styles.inputAttached, { flex: 1 }]}
                      value={customerCode}
                      onChangeText={setCustomerCode}
                      onBlur={() => lookupCustomerByCode(customerCode)}
                      placeholder="Enter/Search Code..."
                      placeholderTextColor="#9E8D77"
                    />
                    <TouchableOpacity
                      style={styles.attachedLookupBtn}
                      onPress={fetchCustomers}
                      activeOpacity={0.7}
                    >
                      <Ionicons name="search" size={13} color="#7D5E19" />
                    </TouchableOpacity>
                  </View>
                </View>

                {/* Customer Name */}
                <View style={[styles.inlineFieldRow, { flex: 1.5, minWidth: 260 }]}>
                  <ThemedText style={styles.inlineFieldLabel}>Name</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1 }]}
                    value={customerName}
                    editable={false}
                    placeholder="Auto-populated"
                    placeholderTextColor="#9E8D77"
                  />
                </View>
              </View>

              {/* Row 2: Address & Remarks */}
              <View style={[styles.twoColumnGrid, { marginTop: 6 }]}>
                {/* Address */}
                <View style={[styles.inlineFieldRow, { flex: 1.8, minWidth: 280 }]}>
                  <ThemedText style={styles.inlineFieldLabel}>Address</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1 }]}
                    value={combinedAddress}
                    editable={false}
                    placeholder="Auto-populated address"
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* Remarks */}
                <View style={[styles.inlineFieldRow, { flex: 1.5, minWidth: 260 }]}>
                  <ThemedText style={styles.inlineFieldLabel}>Remarks</ThemedText>
                  <TextInput
                    style={[styles.formInput, { flex: 1 }]}
                    value={remarks}
                    onChangeText={setRemarks}
                    placeholder="Trade-in notes..."
                    placeholderTextColor="#9E8D77"
                  />
                </View>
              </View>
            </View>
          </View>

          {/* CARD 2: GOLD PURITY CLASS & VALUATION RATES */}
          <View style={[styles.goldCard, { marginTop: 10 }]}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="ribbon-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitleSmall}>Gold Purity Class & Live Valuation Rates</ThemedText>
              </View>
            </View>

            <View style={styles.cardBody}>
              <View style={styles.classAndRatesRow}>
                {/* Gold Class Pills */}
                <View style={styles.classPillsWrapper}>
                  <ThemedText style={[styles.inlineFieldLabel, { width: 55 }]}>Class</ThemedText>
                  <View style={styles.classPillsGroup}>
                    {CLASSES.map((cls) => {
                      const isSelected = selectedClass === cls;
                      return (
                        <TouchableOpacity
                          key={cls}
                          style={[styles.classPill, isSelected && styles.classPillSelected]}
                          onPress={() => setSelectedClass(cls)}
                          activeOpacity={0.7}
                        >
                          <Ionicons
                            name={isSelected ? 'radio-button-on' : 'radio-button-off'}
                            size={12}
                            color={isSelected ? '#9E6D08' : '#7A6855'}
                          />
                          <ThemedText style={[styles.classPillText, isSelected && styles.classPillTextSelected]}>
                            {cls}
                          </ThemedText>
                        </TouchableOpacity>
                      );
                    })}
                  </View>
                </View>

                {/* Rates & Loss Group */}
                <View style={styles.ratesInputGroup}>
                  {/* Loss % */}
                  <View style={[styles.inlineFieldRow, { width: 140, flex: 0 }]}>
                    <ThemedText style={[styles.inlineFieldLabel, { width: 55 }]}>Loss (%)</ThemedText>
                    <TextInput
                      style={[styles.formInput, { flex: 1, textAlign: 'right' }]}
                      value={lossPerc}
                      onChangeText={setLossPerc}
                      keyboardType="numeric"
                    />
                  </View>

                  {/* Cost Per Gram */}
                  <View style={[styles.inlineFieldRow, { width: 175, flex: 0 }]}>
                    <ThemedText style={[styles.inlineFieldLabel, { width: 75 }]}>Cost/Gram ($)</ThemedText>
                    <TextInput
                      style={[styles.formInput, styles.highlightTotal, { flex: 1, textAlign: 'right' }]}
                      value={costPerGram}
                      onChangeText={setCostPerGram}
                      keyboardType="numeric"
                    />
                  </View>
                </View>
              </View>
            </View>
          </View>

          {/* CARD 3: TRADE-IN ITEMS TABLE & ENTRY SUBFORM (100% FULL WIDTH) */}
          <View style={[styles.goldCard, { marginTop: 10 }]}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="sparkles-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitleSmall}>Trade-In Items Assessment</ThemedText>
                <View style={styles.counterBadge}>
                  <ThemedText style={styles.counterBadgeText}>{gridData.length} items</ThemedText>
                </View>
              </View>
            </View>

            {/* Table Header (100% Full Width) */}
            <View style={styles.tableHeadRow}>
              <ThemedText style={[styles.tableHeadCell, { width: 36, textAlign: 'center' }]}>#</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 2.2 }]}>Description</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { width: 70, textAlign: 'center' }]}>Class</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 1, textAlign: 'right' }]}>Weight (g)</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 1, textAlign: 'right' }]}>Stone (g)</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 1, textAlign: 'right' }]}>Loss (g)</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 1.1, textAlign: 'right' }]}>Rate ($)</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 1.3, textAlign: 'right' }]}>Total ($)</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { width: 60, textAlign: 'center' }]}>Action</ThemedText>
            </View>

            {/* Table Body */}
            {gridData.length === 0 ? (
              <View style={styles.emptyTableContainer}>
                <Ionicons name="cube-outline" size={28} color="#D4C3A3" />
                <ThemedText style={styles.emptyTableText}>
                  No trade-in items assessed. Use the form below to add gold items.
                </ThemedText>
              </View>
            ) : (
              gridData.map((item, index) => (
                <View key={index} style={styles.tableRow}>
                  <ThemedText style={[styles.tableCell, { width: 36, textAlign: 'center', color: '#8D6409' }]}>
                    {index + 1}
                  </ThemedText>
                  <View style={{ flex: 2.2, paddingHorizontal: 4 }}>
                    <ThemedText style={[styles.tableCell, { fontWeight: 'bold' }]} numberOfLines={1}>
                      {item.description}
                    </ThemedText>
                    {item.diamondDescription ? (
                      <ThemedText style={{ fontSize: 10, color: '#7A6855' }} numberOfLines={1}>
                        {item.diamondDescription}
                      </ThemedText>
                    ) : null}
                  </View>
                  <View style={{ width: 70, alignItems: 'center' }}>
                    <View style={styles.classTag}>
                      <ThemedText style={styles.classTagText}>{item.class}</ThemedText>
                    </View>
                  </View>
                  <ThemedText style={[styles.tableCell, { flex: 1, textAlign: 'right' }]}>
                    {item.weight}
                  </ThemedText>
                  <ThemedText style={[styles.tableCell, { flex: 1, textAlign: 'right', color: '#7A6855' }]}>
                    {item.stoneWeight}
                  </ThemedText>
                  <ThemedText style={[styles.tableCell, { flex: 1, textAlign: 'right', color: '#C62828' }]}>
                    {item.goldLoss}
                  </ThemedText>
                  <ThemedText style={[styles.tableCell, { flex: 1.1, textAlign: 'right' }]}>
                    {item.costPerGram}
                  </ThemedText>
                  <ThemedText style={[styles.tableCell, styles.amountCell, { flex: 1.3, textAlign: 'right' }]}>
                    {item.totalCost}
                  </ThemedText>
                  <View style={{ width: 60, alignItems: 'center' }}>
                    <TouchableOpacity
                      style={styles.deleteRowBtn}
                      onPress={() => handleDeleteRow(index)}
                      activeOpacity={0.7}
                    >
                      <Ionicons name="trash-outline" size={14} color="#C62828" />
                    </TouchableOpacity>
                  </View>
                </View>
              ))
            )}

            {/* SUBFORM: ITEM ENTRY BAR */}
            <View style={styles.itemEntryBar}>
              {/* Row 1: Description, Diamond Description */}
              <View style={styles.twoColumnGrid}>
                <View style={[styles.inlineFieldRow, { flex: 1.5, minWidth: 260 }]}>
                  <ThemedText style={styles.inlineFieldLabel}>Description *</ThemedText>
                  <TextInput
                    style={[styles.formInput, { flex: 1 }]}
                    value={description}
                    onChangeText={setDescription}
                    placeholder="e.g. 22K Bangle, Scrap Gold..."
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                <View style={[styles.inlineFieldRow, { flex: 1.5, minWidth: 260 }]}>
                  <ThemedText style={styles.inlineFieldLabel}>Diamond / Gem</ThemedText>
                  <TextInput
                    style={[styles.formInput, { flex: 1 }]}
                    value={diamondDescription}
                    onChangeText={setDiamondDescription}
                    placeholder="Diamond or gemstone details..."
                    placeholderTextColor="#9E8D77"
                  />
                </View>
              </View>

              {/* Row 2: Weight, Stone Weight, Gold Loss, Total Cost + Add Button */}
              <View style={[styles.fourColumnGrid, { marginTop: 6 }]}>
                {/* Gross Weight */}
                <View style={[styles.inlineFieldRow, { flex: 1, minWidth: 150 }]}>
                  <ThemedText style={[styles.inlineFieldLabel, { width: 70 }]}>Weight (g) *</ThemedText>
                  <TextInput
                    style={[styles.formInput, { flex: 1, textAlign: 'right' }]}
                    value={weight}
                    onChangeText={setWeight}
                    onBlur={calculateTotalCost}
                    keyboardType="numeric"
                    placeholder="0.000"
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* Stone Weight */}
                <View style={[styles.inlineFieldRow, { flex: 1, minWidth: 150 }]}>
                  <ThemedText style={[styles.inlineFieldLabel, { width: 70 }]}>Stone (g)</ThemedText>
                  <TextInput
                    style={[styles.formInput, { flex: 1, textAlign: 'right' }]}
                    value={stoneWeight}
                    onChangeText={setStoneWeight}
                    keyboardType="numeric"
                    placeholder="0.000"
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* Gold Loss */}
                <View style={[styles.inlineFieldRow, { flex: 1, minWidth: 150 }]}>
                  <ThemedText style={[styles.inlineFieldLabel, { width: 70 }]}>Loss (g)</ThemedText>
                  <TextInput
                    style={[styles.formInput, { flex: 1, textAlign: 'right' }]}
                    value={goldLoss}
                    onChangeText={setGoldLoss}
                    keyboardType="numeric"
                    placeholder="0.000"
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* Total Cost */}
                <View style={[styles.inlineFieldRow, { flex: 1.2, minWidth: 170 }]}>
                  <ThemedText style={[styles.inlineFieldLabel, { width: 75 }]}>Total Cost ($)</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.highlightTotal, { flex: 1, textAlign: 'right' }]}
                    value={totalCost}
                    onChangeText={setTotalCost}
                    keyboardType="numeric"
                    placeholder="0.00"
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* Add to Grid Button */}
                <TouchableOpacity
                  style={[styles.tableActionBtn, styles.addBtn, { height: 26, paddingHorizontal: 12 }]}
                  onPress={handleAddToGrid}
                  activeOpacity={0.8}
                >
                  <Ionicons name="add-circle-outline" size={14} color="#9E6D08" style={{ marginRight: 4 }} />
                  <ThemedText style={styles.addBtnText}>Add Item</ThemedText>
                </TouchableOpacity>
              </View>
            </View>
          </View>

          {/* CARD 4: TRADE-IN VALUATION SUMMARY (3 COLUMNS) */}
          <View style={[styles.goldCard, { marginTop: 10 }]}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="calculator-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitleSmall}>Financial Settlement Summary</ThemedText>
              </View>
            </View>

            <View style={styles.cardBody}>
              <View style={styles.threeColumnGrid}>
                {/* Sales Amount */}
                <View style={styles.inlineFieldRow}>
                  <ThemedText style={styles.inlineFieldLabel}>Sales Amount</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1, textAlign: 'right' }]}
                    value={salesAmount.toFixed(2)}
                    editable={false}
                  />
                </View>

                {/* Trade In Amount */}
                <View style={styles.inlineFieldRow}>
                  <ThemedText style={styles.inlineFieldLabel}>Trade-In Val ($)</ThemedText>
                  <TextInput
                    style={[
                      styles.formInput,
                      styles.readonlyInput,
                      styles.highlightTotal,
                      { flex: 1, textAlign: 'right', fontWeight: 'bold' },
                    ]}
                    value={tradeInAmount.toFixed(2)}
                    editable={false}
                  />
                </View>

                {/* Balance Amount */}
                <View style={styles.inlineFieldRow}>
                  <ThemedText style={styles.inlineFieldLabel}>Net Settlement ($)</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1, textAlign: 'right' }]}
                    value={balanceAmount.toFixed(2)}
                    editable={false}
                  />
                </View>
              </View>
            </View>
          </View>
        </ScrollView>
      </KeyboardAvoidingView>

      {/* CUSTOMER LOOKUP MODAL */}
      <Modal visible={lovVisible} transparent animationType="fade" onRequestClose={() => setLovVisible(false)}>
        <View style={styles.modalOverlay}>
          <View style={styles.lovModalCard}>
            <View style={styles.modalHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="person-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.modalHeaderTitle}>Search Customer Directory</ThemedText>
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
                value={lovSearch}
                onChangeText={setLovSearch}
                placeholder="Search by Code or Name..."
                placeholderTextColor="#9E8D77"
                autoFocus
              />
              {lovSearch.length > 0 && (
                <TouchableOpacity onPress={() => setLovSearch('')}>
                  <Ionicons name="close-circle" size={16} color="#9E8D77" />
                </TouchableOpacity>
              )}
            </View>

            <View style={styles.lovHeadRow}>
              <ThemedText style={[styles.lovHeadText, { flex: 1.4 }]}>Customer Code</ThemedText>
              <ThemedText style={[styles.lovHeadText, { flex: 2.2 }]}>Customer Name</ThemedText>
            </View>

            <FlatList
              data={lovData.filter(
                (item) =>
                  (item.CustomerName || '').toLowerCase().includes(lovSearch.toLowerCase()) ||
                  (item.CustomerCode || '').toLowerCase().includes(lovSearch.toLowerCase())
              )}
              keyExtractor={(item, index) => item.CustomerCode || index.toString()}
              initialNumToRender={50}
              maxToRenderPerBatch={50}
              ListEmptyComponent={
                <View style={styles.emptyTableContainer}>
                  <Ionicons name="search-outline" size={24} color="#D4C3A3" />
                  <ThemedText style={styles.emptyTableText}>No matching customers found.</ThemedText>
                </View>
              }
              renderItem={({ item }) => (
                <TouchableOpacity
                  style={styles.lovItemRow}
                  onPress={() => handleSelectCustomer(item)}
                  activeOpacity={0.7}
                >
                  <ThemedText style={[styles.lovItemText, styles.codeCell, { flex: 1.4 }]}>
                    {item.CustomerCode}
                  </ThemedText>
                  <ThemedText style={[styles.lovItemText, { flex: 2.2 }]} numberOfLines={1}>
                    {item.CustomerName}
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
  invoiceBadge: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFF4DA',
    paddingHorizontal: 8,
    paddingVertical: 2,
    borderRadius: 4,
    borderWidth: 1,
    borderColor: '#B8903B',
  },
  invoiceBadgeLabel: {
    fontSize: 10,
    color: '#7D5E19',
    marginRight: 4,
  },
  invoiceBadgeValue: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#8A5D00',
  },

  // ===== GRIDS & INLINE ROWS =====
  fourColumnGrid: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    gap: 8,
  },
  twoColumnGrid: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    gap: 8,
  },
  threeColumnGrid: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    gap: 8,
  },
  inlineFieldRow: {
    flex: 1,
    minWidth: 240,
    flexDirection: 'row',
    alignItems: 'center',
  },
  inlineFieldLabel: {
    width: 95,
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

  // Class and Rates Row
  classAndRatesRow: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    flexWrap: 'wrap',
    gap: 10,
  },
  classPillsWrapper: {
    flexDirection: 'row',
    alignItems: 'center',
    flexWrap: 'wrap',
    gap: 6,
    flex: 2,
  },
  classPillsGroup: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    gap: 5,
    flex: 1,
  },
  classPill: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FCFAF6',
    borderWidth: 1,
    borderColor: '#E2D3B3',
    borderRadius: 4,
    paddingVertical: 3,
    paddingHorizontal: 6,
    gap: 3,
  },
  classPillSelected: {
    backgroundColor: '#FFF4DA',
    borderColor: '#B8903B',
  },
  classPillText: {
    fontSize: 11,
    fontWeight: '500',
    color: '#5C4A28',
  },
  classPillTextSelected: {
    fontWeight: 'bold',
    color: '#9E6D08',
  },
  ratesInputGroup: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 8,
    flexWrap: 'wrap',
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
  classTag: {
    backgroundColor: '#FFF1D6',
    borderWidth: 1,
    borderColor: '#D4C3A3',
    borderRadius: 3,
    paddingHorizontal: 6,
    paddingVertical: 1,
  },
  classTagText: {
    fontSize: 10,
    fontWeight: 'bold',
    color: '#7D5E19',
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
