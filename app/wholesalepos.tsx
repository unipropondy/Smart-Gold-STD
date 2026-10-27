import { ThemedText } from '@/components/themed-text';
import { API_BASE_URL } from '@/constants/api';
import { Ionicons } from '@expo/vector-icons';
import { Stack, useRouter } from 'expo-router';
import React, { useEffect, useState } from 'react';
import {
  ActivityIndicator,
  Alert,
  FlatList,
  Modal,
  ScrollView,
  StyleSheet,
  TextInput,
  TouchableOpacity,
  TouchableWithoutFeedback,
  View
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';

const API_URL = `${API_BASE_URL}`;

export default function WholeSalePOSScreen() {
  const router = useRouter();

  // ===== STATE =====
  const [headerState, setHeaderState] = useState({
    InvoiceNo: '',
    SalesManCode: '',
    CustomerCode: '',
    CustomerName: '',
    Address1: '',
    Remarks: '',
    TaxMode: 'Zero Rated', // Inclusive, Exclusive, Zero Rated
    ExportInvoice: false,
  });

  const [details, setDetails] = useState<any[]>([]);
  const [currentDetail, setCurrentDetail] = useState<any>({
    InventoryCode: '',
    Description: '',
    Class: '',
    Weight: '',
    GoldLoss: '',
    CostPerGram: '',
    SMC: '',
    PCS: '1',
    TotalValue: '',
    GST: ''
  });

  const [paymentState, setPaymentState] = useState({
    TotalQty: '0',
    TotalSalesAmt: '0',
    NetDiscount: '0',
    NetValue: '0',
    Tax: '0',
    NetTotal: '0',
  });

  const [loading, setLoading] = useState(false);
  const [toast, setToast] = useState({ visible: false, message: '', type: 'success' });
  const [selectedIndex, setSelectedIndex] = useState<number | null>(null);

  // Password Verification (Salesman Access)
  const [passwordModalVisible, setPasswordModalVisible] = useState(true);
  const [salesmanPassword, setSalesmanPassword] = useState('');
  const [verifyingPassword, setVerifyingPassword] = useState(false);

  // LOV State
  const [lovVisible, setLovVisible] = useState(false);
  const [lovTitle, setLovTitle] = useState('');
  const [lovData, setLovData] = useState<any[]>([]);
  const [filteredLovData, setFilteredLovData] = useState<any[]>([]);
  const [lovSearch, setLovSearch] = useState('');

  const showToast = (msg: string, type: 'success' | 'error' = 'success') => {
    setToast({ visible: true, message: msg, type });
    setTimeout(() => setToast({ visible: false, message: '', type: 'success' }), 3000);
  };

  const updateHeader = (field: string, value: string | boolean) => {
    setHeaderState(prev => ({ ...prev, [field]: value }));
  };

  const updateDetail = (field: string, value: string) => {
    setCurrentDetail((prev: any) => ({ ...prev, [field]: value }));
  };

  // Auto-fetch Next Invoice Number on init
  const fetchNextInvoiceNo = async () => {
    try {
      const res = await fetch(`${API_URL}/wholesalepos/next-invoice`);
      const data = await res.json();
      if (data.success && data.nextInvoiceNo) {
        setHeaderState(prev => ({ ...prev, InvoiceNo: data.nextInvoiceNo }));
      }
    } catch {
      // fallback auto-generated invoice if offline
      const fallbackNo = `WS-${Date.now().toString().slice(-6)}`;
      setHeaderState(prev => ({ ...prev, InvoiceNo: prev.InvoiceNo || fallbackNo }));
    }
  };

  // Recalculate line item TotalValue and GST matching reference formulas:
  // TotalValue = (Weight + GoldLoss) * CostPerGram + SMC * PCS
  // Exclusive GST: TotalValue * 9%
  // Inclusive GST: TotalValue - (TotalValue / 1.09)
  // Zero Rated GST: 0.00
  useEffect(() => {
    const weight = parseFloat(currentDetail.Weight) || 0;
    const goldLoss = parseFloat(currentDetail.GoldLoss) || 0;
    const costPerGram = parseFloat(currentDetail.CostPerGram) || 0;
    const smc = parseFloat(currentDetail.SMC) || 0;
    const pcs = parseFloat(currentDetail.PCS) || 1;

    const val = (weight + goldLoss) * costPerGram + smc * pcs;
    let gstAmt = 0;
    if (headerState.TaxMode === 'Exclusive') {
      gstAmt = val * 0.09;
    } else if (headerState.TaxMode === 'Inclusive') {
      gstAmt = val - (val / 1.09);
    }

    const valStr = val.toFixed(2);
    const gstStr = gstAmt.toFixed(2);
    if (currentDetail.TotalValue !== valStr || currentDetail.GST !== gstStr) {
      setCurrentDetail((prev: any) => ({ ...prev, TotalValue: valStr, GST: gstStr }));
    }
  }, [currentDetail.Weight, currentDetail.GoldLoss, currentDetail.CostPerGram, currentDetail.SMC, currentDetail.PCS, headerState.TaxMode]);

  // Overall Invoice Totals calculation
  useEffect(() => {
    let sumQty = 0;
    let sumVal = 0;
    let sumGst = 0;
    details.forEach(d => {
      sumQty += (parseFloat(d.PCS) || 1);
      sumVal += (parseFloat(d.TotalValue) || 0);
      sumGst += (parseFloat(d.GST) || 0);
    });

    const netDiscount = parseFloat(paymentState.NetDiscount) || 0;
    const netValue = Math.max(0, sumVal - netDiscount);
    let finalTax = 0;
    if (headerState.TaxMode === 'Exclusive') {
      finalTax = netValue * 0.09;
    } else if (headerState.TaxMode === 'Inclusive') {
      finalTax = netValue - (netValue / 1.09);
    }

    const netTotal = headerState.TaxMode === 'Exclusive' ? netValue + finalTax : netValue;

    setPaymentState(prev => ({
      ...prev,
      TotalQty: sumQty.toString(),
      TotalSalesAmt: sumVal.toFixed(2),
      NetValue: netValue.toFixed(2),
      Tax: finalTax.toFixed(2),
      NetTotal: netTotal.toFixed(2)
    }));
  }, [details, paymentState.NetDiscount, headerState.TaxMode]);

  // Lookup inventory item details on barcode enter / scan
  const fetchInventoryDetail = async (code: string) => {
    const trimmed = code.trim();
    if (!trimmed) return;
    setLoading(true);
    try {
      const res = await fetch(`${API_URL}/wholesalepos/inventory/${encodeURIComponent(trimmed)}`);
      const data = await res.json();
      if (data.success && data.data) {
        const item = data.data;
        setCurrentDetail({
          InventoryCode: item.InventoryCode || trimmed,
          Description: item.Description || '',
          Class: item.Class || '',
          Weight: (parseFloat(item.Weight) || 0).toFixed(2),
          GoldLoss: (parseFloat(item.GoldLoss) || 0).toFixed(2),
          CostPerGram: (parseFloat(item.CostPerGram || item.ClassRate) || 0).toFixed(2),
          SMC: (parseFloat(item.SMC) || 0).toFixed(2),
          PCS: (item.PCS || 1).toString(),
          TotalValue: '',
          GST: ''
        });
        showToast(`Loaded ${item.InventoryCode}: ${item.Description || ''}`, 'success');
      } else {
        showToast(data.message || 'Item not found in stock', 'error');
      }
    } catch {
      showToast('Error looking up inventory item', 'error');
    } finally {
      setLoading(false);
    }
  };

  const handleAddDetail = async () => {
    const code = (currentDetail.InventoryCode || '').trim();
    if (!code) {
      Alert.alert('Validation Error', 'Inventory Code is required.');
      return;
    }

    // If description or weight is not loaded yet, fetch now
    let itemToAdd = { ...currentDetail };
    if (!itemToAdd.Description && !itemToAdd.Weight) {
      setLoading(true);
      try {
        const res = await fetch(`${API_URL}/wholesalepos/inventory/${encodeURIComponent(code)}`);
        const data = await res.json();
        if (data.success && data.data) {
          const inv = data.data;
          itemToAdd = {
            ...itemToAdd,
            InventoryCode: inv.InventoryCode || code,
            Description: inv.Description || '',
            Class: inv.Class || '',
            Weight: (parseFloat(inv.Weight) || 0).toFixed(2),
            GoldLoss: (parseFloat(inv.GoldLoss) || 0).toFixed(2),
            CostPerGram: (parseFloat(inv.CostPerGram || inv.ClassRate) || 0).toFixed(2),
            SMC: (parseFloat(inv.SMC) || 0).toFixed(2),
            PCS: (inv.PCS || 1).toString(),
          };
          // Recalculate item total value
          const w = parseFloat(itemToAdd.Weight) || 0;
          const gl = parseFloat(itemToAdd.GoldLoss) || 0;
          const cpg = parseFloat(itemToAdd.CostPerGram) || 0;
          const smc = parseFloat(itemToAdd.SMC) || 0;
          const pcsVal = parseFloat(itemToAdd.PCS) || 1;
          const val = (w + gl) * cpg + smc * pcsVal;
          itemToAdd.TotalValue = val.toFixed(2);
        } else {
          Alert.alert('Item Error', data.message || 'Inventory item not found in stock.');
          setLoading(false);
          return;
        }
      } catch {
        Alert.alert('Network Error', 'Could not fetch item details.');
        setLoading(false);
        return;
      } finally {
        setLoading(false);
      }
    }

    const pcs = parseFloat(itemToAdd.PCS) || 0;
    if (pcs <= 0) {
      Alert.alert('Validation Error', 'Pieces must be at least 1.');
      return;
    }

    // Check duplicate if not currently editing
    if (selectedIndex === null) {
      const existing = details.find(
        d => d.InventoryCode.trim().toUpperCase() === code.toUpperCase()
      );
      if (existing) {
        Alert.alert('Validation Error', `Item ${code} is already added in this invoice.`);
        return;
      }
    }

    if (selectedIndex !== null) {
      const newDet = [...details];
      newDet[selectedIndex] = { ...itemToAdd };
      setDetails(newDet);
      setSelectedIndex(null);
      showToast('Line item updated', 'success');
    } else {
      setDetails([...details, { ...itemToAdd }]);
      showToast('Line item added', 'success');
    }

    // Reset current entry fields
    setCurrentDetail({
      InventoryCode: '',
      Description: '',
      Class: '',
      Weight: '',
      GoldLoss: '',
      CostPerGram: '',
      SMC: '',
      PCS: '1',
      TotalValue: '',
      GST: ''
    });
  };

  const editDetail = (index: number) => {
    setCurrentDetail({ ...details[index] });
    setSelectedIndex(index);
  };

  const removeDetail = (index: number) => {
    Alert.alert('Confirm Remove', `Remove item "${details[index]?.InventoryCode || 'this item'}"?`, [
      { text: 'Cancel', style: 'cancel' },
      {
        text: 'Remove',
        style: 'destructive',
        onPress: () => {
          const newDet = [...details];
          newDet.splice(index, 1);
          setDetails(newDet);
          if (selectedIndex === index) setSelectedIndex(null);
          showToast('Item removed', 'success');
        }
      }
    ]);
  };

  const clearForm = () => {
    setHeaderState({
      InvoiceNo: '',
      SalesManCode: headerState.SalesManCode, // preserve authorized salesman
      CustomerCode: '',
      CustomerName: '',
      Address1: '',
      Remarks: '',
      TaxMode: 'Zero Rated',
      ExportInvoice: false
    });
    setDetails([]);
    setSelectedIndex(null);
    setCurrentDetail({
      InventoryCode: '',
      Description: '',
      Class: '',
      Weight: '',
      GoldLoss: '',
      CostPerGram: '',
      SMC: '',
      PCS: '1',
      TotalValue: '',
      GST: ''
    });
    setPaymentState({
      TotalQty: '0',
      TotalSalesAmt: '0',
      NetDiscount: '0',
      NetValue: '0',
      Tax: '0',
      NetTotal: '0',
    });
    fetchNextInvoiceNo();
  };

  // Load existing wholesale invoice
  const loadInvoiceData = async (invNo: string) => {
    if (!invNo) return;
    setLoading(true);
    try {
      const res = await fetch(`${API_URL}/wholesalepos/${encodeURIComponent(invNo)}`);
      const data = await res.json();
      if (data.success && data.header) {
        const hdr = data.header;
        setHeaderState({
          InvoiceNo: hdr.InvoiceNo,
          SalesManCode: hdr.SalesManCode || '',
          CustomerCode: hdr.CustomerCode || '',
          CustomerName: hdr.CustomerName || hdr.CustomerFullName || '',
          Address1: hdr.Address1 || '',
          Remarks: hdr.Remarks || '',
          TaxMode: hdr.GSTType || 'Zero Rated',
          ExportInvoice: false
        });

        if (Array.isArray(data.details)) {
          setDetails(data.details.map((d: any) => ({
            InventoryCode: d.InventoryCode,
            Description: d.Description || '',
            Class: d.Class || '',
            Weight: (parseFloat(d.Weight) || 0).toFixed(2),
            GoldLoss: (parseFloat(d.GoldLoss) || 0).toFixed(2),
            CostPerGram: (parseFloat(d.CostPerGram) || 0).toFixed(2),
            SMC: (parseFloat(d.SMC) || 0).toFixed(2),
            PCS: (d.PCS || 1).toString(),
            TotalValue: (parseFloat(d.TotalValue) || 0).toFixed(2),
            GST: (parseFloat(d.GST) || 0).toFixed(2)
          })));
        }
        showToast(`Loaded Invoice ${hdr.InvoiceNo}`, 'success');
      } else {
        showToast(data.message || 'Invoice not found', 'error');
      }
    } catch {
      showToast('Error loading invoice', 'error');
    } finally {
      setLoading(false);
    }
  };

  const saveInvoice = async () => {
    if (!headerState.SalesManCode || !headerState.SalesManCode.trim()) {
      Alert.alert('Validation Error', 'Salesman Code is mandatory.');
      return;
    }
    if (!headerState.CustomerCode || !headerState.CustomerCode.trim()) {
      Alert.alert('Validation Error', 'Customer Code is mandatory.');
      return;
    }
    if (details.length === 0) {
      Alert.alert('Validation Error', 'Please add at least one line item.');
      return;
    }

    setLoading(true);
    const invoiceToSave = headerState.InvoiceNo.trim() || `WS-${Date.now().toString().slice(-6)}`;

    let mappedGstType = 'Z';
    if (headerState.TaxMode === 'Exclusive') mappedGstType = 'E';
    else if (headerState.TaxMode === 'Inclusive') mappedGstType = 'I';

    const payload = {
      header: {
        InvoiceNo: invoiceToSave,
        SalesManCode: headerState.SalesManCode.trim(),
        CustomerCode: headerState.CustomerCode.trim(),
        CustomerName: headerState.CustomerName.trim(),
        Address1: headerState.Address1.trim(),
        Remarks: headerState.Remarks.trim(),
        GSTType: mappedGstType,
        TotalPCS: parseFloat(paymentState.TotalQty) || 0,
        TotalValue: parseFloat(paymentState.TotalSalesAmt) || 0,
        NetDiscount: parseFloat(paymentState.NetDiscount) || 0,
        NetTotal: parseFloat(paymentState.NetTotal) || 0,
        GST: parseFloat(paymentState.Tax) || 0
      },
      details: details.map(d => ({
        InventoryCode: d.InventoryCode,
        Description: d.Description,
        Class: d.Class,
        Weight: parseFloat(d.Weight) || 0,
        GoldLoss: parseFloat(d.GoldLoss) || 0,
        CostPerGram: parseFloat(d.CostPerGram) || 0,
        SMC: parseFloat(d.SMC) || 0,
        PCS: parseFloat(d.PCS) || 1,
        TotalValue: parseFloat(d.TotalValue) || 0,
        GST: parseFloat(d.GST) || 0,
        SalesType: 'Wholesale'
      }))
    };

    try {
      const res = await fetch(`${API_URL}/wholesalepos`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload)
      });
      const data = await res.json();
      if (data.success) {
        showToast(data.message || 'Saved Successfully!', 'success');
        clearForm();
      } else {
        showToast(data.message || 'Error saving invoice', 'error');
      }
    } catch (err: any) {
      showToast(err.message || 'Network error saving invoice', 'error');
    } finally {
      setLoading(false);
    }
  };

  const deleteInvoice = async () => {
    if (!headerState.InvoiceNo || !headerState.InvoiceNo.trim()) {
      Alert.alert('Validation Error', 'Please select or enter an Invoice Number to cancel.');
      return;
    }

    Alert.alert(
      'Confirm Bill Cancel',
      `Are you sure you want to cancel Wholesale Invoice "${headerState.InvoiceNo}"? This will return all items to stock.`,
      [
        { text: 'No, Keep', style: 'cancel' },
        {
          text: 'Yes, Cancel Bill',
          style: 'destructive',
          onPress: async () => {
            setLoading(true);
            try {
              const res = await fetch(`${API_URL}/wholesalepos/${encodeURIComponent(headerState.InvoiceNo.trim())}`, {
                method: 'DELETE'
              });
              const data = await res.json();
              if (data.success) {
                showToast(data.message || 'Invoice cancelled successfully', 'success');
                clearForm();
              } else {
                showToast(data.message || 'Error cancelling invoice', 'error');
              }
            } catch (e: any) {
              showToast(e.message || 'Network error', 'error');
            } finally {
              setLoading(false);
            }
          }
        }
      ]
    );
  };

  // Generic LOV Handler
  const openLOV = async (type: string) => {
    setLoading(true);
    try {
      let endpoint = '';
      if (type === 'Salesman') endpoint = '/wholesalepos/lov/salesmen';
      else if (type === 'Customer') endpoint = '/wholesalepos/lov/customers';
      else if (type === 'Inventory') endpoint = '/wholesalepos/lov/inventory';
      else if (type === 'Invoice') endpoint = '/wholesalepos';

      const res = await fetch(`${API_URL}${endpoint}`);
      const data = await res.json();
      if (data.success && Array.isArray(data.data)) {
        setLovData(data.data);
        setFilteredLovData(data.data);
        setLovSearch('');
        setLovTitle(type);
        setLovVisible(true);
      } else {
        showToast(data.message || `No records found for ${type}`, 'error');
      }
    } catch {
      showToast(`Error fetching ${type} records`, 'error');
    } finally {
      setLoading(false);
    }
  };

  const handleSelectLovItem = (item: any) => {
    if (lovTitle === 'Salesman') {
      updateHeader('SalesManCode', item.SalesManCode || item.SalesmanCode || '');
    } else if (lovTitle === 'Customer') {
      updateHeader('CustomerCode', item.CustomerCode || '');
      updateHeader('CustomerName', item.CustomerName || '');
      updateHeader('Address1', item.Address1 || '');
    } else if (lovTitle === 'Inventory') {
      const weight = parseFloat(item.Weight) || 0;
      const goldLoss = parseFloat(item.GoldLoss) || 0;
      const costPerGram = parseFloat(item.CostPerGram || item.ClassRate) || 0;
      const smc = parseFloat(item.SMC) || 0;
      const pcs = parseFloat(item.PCS) || 1;
      const val = (weight + goldLoss) * costPerGram + smc * pcs;
      let gstAmt = 0;
      if (headerState.TaxMode === 'Exclusive') {
        gstAmt = val * 0.09;
      } else if (headerState.TaxMode === 'Inclusive') {
        gstAmt = val - (val / 1.09);
      }
      setCurrentDetail({
        InventoryCode: item.InventoryCode || '',
        Description: item.Description || '',
        Class: item.Class || '',
        Weight: weight.toFixed(2),
        GoldLoss: goldLoss.toFixed(2),
        CostPerGram: costPerGram.toFixed(2),
        SMC: smc.toFixed(2),
        PCS: pcs.toString(),
        TotalValue: val.toFixed(2),
        GST: gstAmt.toFixed(2)
      });
      showToast(`Selected ${item.InventoryCode}`, 'success');
    } else if (lovTitle === 'Invoice') {
      loadInvoiceData(item.InvoiceNo);
    }
    setLovVisible(false);
  };

  const verifySalesmanPassword = async () => {
    if (!salesmanPassword || !salesmanPassword.trim()) {
      showToast('Please enter SalesMan password', 'error');
      return;
    }
    setVerifyingPassword(true);
    try {
      const response = await fetch(`${API_BASE_URL}/salesreturn/verifypassword`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ password: salesmanPassword.trim() })
      });
      const data = await response.json();
      if (data.success && data.salesman) {
        updateHeader('SalesManCode', data.salesman.SalesManCode || data.salesman.SalesManName || '');
        setPasswordModalVisible(false);
        showToast('Authorized successfully', 'success');
        fetchNextInvoiceNo();
      } else {
        showToast(data.message || 'Invalid salesman password', 'error');
      }
    } catch (err: any) {
      showToast(err.message || 'Connection error', 'error');
    } finally {
      setVerifyingPassword(false);
    }
  };

  return (
    <SafeAreaView style={styles.safeArea} edges={['top']}>
      <Stack.Screen options={{ headerShown: false }} />

      {/* Top Header Toolbar */}
      <View style={styles.header}>
        <View style={styles.headerLeft}>
          <TouchableOpacity style={styles.backButton} onPress={() => router.back()}>
            <Ionicons name="arrow-back" size={20} color="#3B2D17" />
          </TouchableOpacity>
          <ThemedText style={styles.headerTitle}>WholeSale Jewel POS</ThemedText>
        </View>

        <View style={styles.headerActions}>
          <TouchableOpacity style={styles.headerBtnSecondary} onPress={clearForm}>
            <Ionicons name="refresh-outline" size={14} color="#554428" style={{ marginRight: 4 }} />
            <ThemedText style={styles.headerBtnSecondaryText}>Clear</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity style={styles.headerBtnSecondary} onPress={deleteInvoice}>
            <Ionicons name="close-circle-outline" size={14} color="#D93829" style={{ marginRight: 4 }} />
            <ThemedText style={[styles.headerBtnSecondaryText, { color: '#D93829' }]}>Bill Cancel</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity style={styles.headerBtnSecondary} onPress={() => openLOV('Invoice')}>
            <Ionicons name="print-outline" size={14} color="#554428" style={{ marginRight: 4 }} />
            <ThemedText style={styles.headerBtnSecondaryText}>Re Print / Find</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity
            testID="btn-net-total-save"
            style={[styles.headerBtnPrimary, loading && { opacity: 0.7 }]}
            onPress={saveInvoice}
            disabled={loading}
          >
            <Ionicons name="checkmark-done-outline" size={15} color="#9E6D08" style={{ marginRight: 4 }} />
            <ThemedText style={styles.headerBtnPrimaryText}>Net Total / Save</ThemedText>
          </TouchableOpacity>
        </View>
      </View>

      <ScrollView contentContainerStyle={styles.contentContainer}>
        {/* TOP SECTION: Header Info & Summary Cards Side-by-Side */}
        <View style={styles.topSplitRow}>
          {/* Left: Customer & Invoice Details Card */}
          <View style={[styles.goldCard, { flex: 2 }]}>
            <View style={styles.cardHeaderBanner}>
              <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                <View style={styles.headerIconBadge}>
                  <Ionicons name="person-outline" size={14} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitle}>Customer & Invoice Information</ThemedText>
              </View>

              {/* Tax Mode Radios */}
              <View style={styles.pillBox}>
                {['Zero Rated', 'Inclusive', 'Exclusive'].map(mode => (
                  <TouchableOpacity
                    key={mode}
                    style={styles.radioContainer}
                    onPress={() => updateHeader('TaxMode', mode)}
                  >
                    <View style={[styles.radioCircle, headerState.TaxMode === mode && styles.radioCircleSelected]}>
                      {headerState.TaxMode === mode && <View style={styles.radioInner} />}
                    </View>
                    <ThemedText style={styles.radioLabel}>{mode}</ThemedText>
                  </TouchableOpacity>
                ))}
              </View>
            </View>

            <View style={styles.cardBody}>
              {/* Row 1: Sales Man & Invoice No (same row) */}
              <View style={styles.formRow}>
                <View style={{ flex: 1, marginRight: 12, flexDirection: 'row', alignItems: 'center' }}>
                  <ThemedText style={styles.inlineFormLabel}>Sales Man</ThemedText>
                  <View style={{ flex: 1, flexDirection: 'row', alignItems: 'center' }}>
                    <TextInput
                      style={[styles.input, { flex: 1 }]}
                      value={headerState.SalesManCode}
                      onChangeText={(v) => updateHeader('SalesManCode', v)}
                      placeholder="Salesman code"
                      placeholderTextColor="#9E8D77"
                    />
                    <TouchableOpacity style={styles.lookupBtn} onPress={() => openLOV('Salesman')}>
                      <ThemedText style={styles.lookupBtnText}>...</ThemedText>
                    </TouchableOpacity>
                  </View>
                </View>

                <View style={{ flex: 1, flexDirection: 'row', alignItems: 'center' }}>
                  <ThemedText style={styles.inlineFormLabel}>Invoice No</ThemedText>
                  <View style={{ flex: 1, flexDirection: 'row', alignItems: 'center' }}>
                    <TextInput
                      style={[styles.input, { flex: 1 }]}
                      value={headerState.InvoiceNo}
                      onChangeText={(v) => updateHeader('InvoiceNo', v)}
                      onSubmitEditing={() => loadInvoiceData(headerState.InvoiceNo)}
                      placeholder="Auto / WS-..."
                      placeholderTextColor="#9E8D77"
                    />
                    <TouchableOpacity style={styles.lookupBtn} onPress={() => openLOV('Invoice')}>
                      <ThemedText style={styles.lookupBtnText}>...</ThemedText>
                    </TouchableOpacity>
                  </View>
                </View>
              </View>

              {/* Row 2: Customer Code & Name (same row) */}
              <View style={styles.formRow}>
                <View style={{ flex: 1, marginRight: 12, flexDirection: 'row', alignItems: 'center' }}>
                  <ThemedText style={styles.inlineFormLabel}>Customer Code</ThemedText>
                  <View style={{ flex: 1, flexDirection: 'row', alignItems: 'center' }}>
                    <TextInput
                      style={[styles.input, { flex: 1 }]}
                      value={headerState.CustomerCode}
                      onChangeText={(v) => updateHeader('CustomerCode', v)}
                      placeholder="Customer code"
                      placeholderTextColor="#9E8D77"
                    />
                    <TouchableOpacity style={styles.lookupBtn} onPress={() => openLOV('Customer')}>
                      <ThemedText style={styles.lookupBtnText}>...</ThemedText>
                    </TouchableOpacity>
                  </View>
                </View>

                <View style={{ flex: 1, flexDirection: 'row', alignItems: 'center' }}>
                  <ThemedText style={styles.inlineFormLabel}>Customer Name</ThemedText>
                  <View style={{ flex: 1, flexDirection: 'row', alignItems: 'center' }}>
                    <TextInput
                      style={[styles.input, { flex: 1 }]}
                      value={headerState.CustomerName}
                      onChangeText={(v) => updateHeader('CustomerName', v)}
                      placeholder="Customer name"
                      placeholderTextColor="#9E8D77"
                    />
                    <View style={{ width: 28, marginLeft: 4 }} />
                  </View>
                </View>
              </View>

              {/* Row 3: Address & Remarks (same row) */}
              <View style={styles.formRow}>
                <View style={{ flex: 1, marginRight: 12, flexDirection: 'row', alignItems: 'center' }}>
                  <ThemedText style={styles.inlineFormLabel}>Address</ThemedText>
                  <View style={{ flex: 1, flexDirection: 'row', alignItems: 'center' }}>
                    <TextInput
                      style={[styles.input, { flex: 1 }]}
                      value={headerState.Address1}
                      onChangeText={(v) => updateHeader('Address1', v)}
                      placeholder="Address details"
                      placeholderTextColor="#9E8D77"
                    />
                    <View style={{ width: 28, marginLeft: 4 }} />
                  </View>
                </View>
                <View style={{ flex: 1, flexDirection: 'row', alignItems: 'center' }}>
                  <ThemedText style={styles.inlineFormLabel}>Remarks</ThemedText>
                  <View style={{ flex: 1, flexDirection: 'row', alignItems: 'center' }}>
                    <TextInput
                      style={[styles.input, { flex: 1 }]}
                      value={headerState.Remarks}
                      onChangeText={(v) => updateHeader('Remarks', v)}
                      placeholder="Notes or terms"
                      placeholderTextColor="#9E8D77"
                    />
                    <View style={{ width: 28, marginLeft: 4 }} />
                  </View>
                </View>
              </View>
            </View>
          </View>

          {/* Right: Summary Breakdown Card */}
          <View style={[styles.goldCard, { flex: 1 }]}>
            <View style={styles.cardHeaderBanner}>
              <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                <View style={styles.headerIconBadge}>
                  <Ionicons name="calculator-outline" size={14} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitle}>Sales Summary</ThemedText>
              </View>
            </View>

            <View style={styles.summaryBody}>
              <View style={styles.summaryRow}>
                <ThemedText style={styles.summaryLabel}>Total Qty</ThemedText>
                <ThemedText style={styles.summaryValue}>{paymentState.TotalQty}</ThemedText>
              </View>
              <View style={styles.summaryRow}>
                <ThemedText style={styles.summaryLabel}>Sales Amount</ThemedText>
                <ThemedText style={styles.summaryValue}>{paymentState.TotalSalesAmt}</ThemedText>
              </View>
              <View style={styles.summaryRow}>
                <ThemedText style={styles.summaryLabel}>Net Discount</ThemedText>
                <TextInput
                  style={[styles.summaryInput]}
                  value={paymentState.NetDiscount}
                  onChangeText={(v) => setPaymentState(prev => ({ ...prev, NetDiscount: v }))}
                  keyboardType="numeric"
                  placeholder="0.00"
                  placeholderTextColor="#9E8D77"
                />
              </View>
              <View style={styles.summaryRow}>
                <ThemedText style={styles.summaryLabel}>Net Value</ThemedText>
                <ThemedText style={styles.summaryValue}>{paymentState.NetValue}</ThemedText>
              </View>
              <View style={styles.summaryRow}>
                <ThemedText style={styles.summaryLabel}>
                  GST ({headerState.TaxMode === 'Exclusive' ? '9%' : headerState.TaxMode === 'Inclusive' ? 'Inc 9%' : '0%'})
                </ThemedText>
                <ThemedText style={styles.summaryValue}>{paymentState.Tax}</ThemedText>
              </View>
              <View style={[styles.summaryRow, styles.summaryTotalRow]}>
                <ThemedText style={styles.summaryTotalLabel}>Net Amount</ThemedText>
                <ThemedText style={styles.summaryTotalValue}>{paymentState.NetTotal}</ThemedText>
              </View>
            </View>
          </View>
        </View>

        {/* MIDDLE SECTION: Item Entry Form Card */}
        <View style={[styles.goldCard, { marginTop: 8 }]}>
          <View style={styles.cardHeaderBanner}>
            <View style={{ flexDirection: 'row', alignItems: 'center' }}>
              <View style={styles.headerIconBadge}>
                <Ionicons name="create-outline" size={14} color="#B48218" />
              </View>
              <ThemedText style={styles.cardHeaderTitle}>
                {selectedIndex !== null ? 'Edit Line Item' : 'Add Item to Invoice'}
              </ThemedText>
            </View>

            <TouchableOpacity style={styles.addItemBtn} onPress={handleAddDetail} activeOpacity={0.8}>
              <Ionicons
                name={selectedIndex !== null ? "checkmark" : "add"}
                size={14}
                color="#7D5E19"
                style={{ marginRight: 4 }}
              />
              <ThemedText style={styles.addItemBtnText}>
                {selectedIndex !== null ? 'Update Item' : 'Add Item'}
              </ThemedText>
            </TouchableOpacity>
          </View>

          <View style={styles.cardBody}>
            <View style={{ flexDirection: 'row', alignItems: 'flex-end', gap: 6 }}>
              {/* Inventory Code */}
              <View style={{ flex: 1.5, minWidth: 150 }}>
                <ThemedText style={styles.entryLabel}>Inventory Code</ThemedText>
                <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                  <TextInput
                    style={[styles.input, { flex: 1, minWidth: 60 }]}
                    value={currentDetail.InventoryCode}
                    onChangeText={v => updateDetail('InventoryCode', v)}
                    onSubmitEditing={() => fetchInventoryDetail(currentDetail.InventoryCode)}
                    placeholder="Scan / Key in..."
                    placeholderTextColor="#9E8D77"
                  />
                  <TouchableOpacity style={styles.lookupBtn} onPress={() => openLOV('Inventory')}>
                    <ThemedText style={styles.lookupBtnText}>...</ThemedText>
                  </TouchableOpacity>
                </View>
              </View>

              {/* Description */}
              <View style={{ flex: 2.2, minWidth: 130 }}>
                <ThemedText style={styles.entryLabel}>Description</ThemedText>
                <TextInput
                  style={styles.input}
                  value={currentDetail.Description}
                  onChangeText={v => updateDetail('Description', v)}
                  placeholder="Item name"
                  placeholderTextColor="#9E8D77"
                />
              </View>

              {/* Weight */}
              <View style={{ flex: 0.9 }}>
                <ThemedText style={styles.entryLabel}>Weight (g)</ThemedText>
                <TextInput
                  style={[styles.input, { textAlign: 'right' }]}
                  value={currentDetail.Weight}
                  onChangeText={v => updateDetail('Weight', v)}
                  keyboardType="numeric"
                  placeholder="0.00"
                  placeholderTextColor="#9E8D77"
                />
              </View>

              {/* Gold Loss */}
              <View style={{ flex: 0.9 }}>
                <ThemedText style={styles.entryLabel}>Gold Loss</ThemedText>
                <TextInput
                  style={[styles.input, { textAlign: 'right' }]}
                  value={currentDetail.GoldLoss}
                  onChangeText={v => updateDetail('GoldLoss', v)}
                  keyboardType="numeric"
                  placeholder="0.00"
                  placeholderTextColor="#9E8D77"
                />
              </View>

              {/* Cost/Gram */}
              <View style={{ flex: 1 }}>
                <ThemedText style={styles.entryLabel}>Cost/g</ThemedText>
                <TextInput
                  style={[styles.input, { textAlign: 'right' }]}
                  value={currentDetail.CostPerGram}
                  onChangeText={v => updateDetail('CostPerGram', v)}
                  keyboardType="numeric"
                  placeholder="0.00"
                  placeholderTextColor="#9E8D77"
                />
              </View>

              {/* SMC */}
              <View style={{ flex: 0.9 }}>
                <ThemedText style={styles.entryLabel}>SMC</ThemedText>
                <TextInput
                  style={[styles.input, { textAlign: 'right' }]}
                  value={currentDetail.SMC}
                  onChangeText={v => updateDetail('SMC', v)}
                  keyboardType="numeric"
                  placeholder="0.00"
                  placeholderTextColor="#9E8D77"
                />
              </View>

              {/* PCS */}
              <View style={{ flex: 0.7 }}>
                <ThemedText style={styles.entryLabel}>PCS</ThemedText>
                <TextInput
                  style={[styles.input, { textAlign: 'right' }]}
                  value={currentDetail.PCS}
                  onChangeText={v => updateDetail('PCS', v)}
                  keyboardType="numeric"
                  placeholder="1"
                  placeholderTextColor="#9E8D77"
                />
              </View>

              {/* Total Value */}
              <View style={{ flex: 1.2 }}>
                <ThemedText style={styles.entryLabel}>Total Value</ThemedText>
                <TextInput
                  style={[styles.input, styles.readonlyInput, { textAlign: 'right', fontWeight: 'bold', color: '#9E6D08' }]}
                  value={currentDetail.TotalValue}
                  editable={false}
                />
              </View>
            </View>
          </View>
        </View>

        {/* BOTTOM SECTION: 100% Full-Width Items Table */}
        <View style={[styles.goldCard, { marginTop: 8, minHeight: 240 }]}>
          <View style={styles.cardHeaderBanner}>
            <View style={{ flexDirection: 'row', alignItems: 'center' }}>
              <View style={styles.headerIconBadge}>
                <Ionicons name="cart-outline" size={14} color="#B48218" />
              </View>
              <ThemedText style={styles.cardHeaderTitle}>Invoice Line Items</ThemedText>
              <View style={styles.counterBadge}>
                <ThemedText style={styles.counterBadgeText}>{details.length} Items</ThemedText>
              </View>
            </View>
          </View>

          <View style={{ width: '100%' }}>
            {/* Table Header */}
            <View style={styles.tableHeader}>
              <ThemedText style={[styles.tableHeaderText, { flex: 1.4 }]}>Inventory Code</ThemedText>
              <ThemedText style={[styles.tableHeaderText, { flex: 2.3 }]}>Description</ThemedText>
              <ThemedText style={[styles.tableHeaderText, { flex: 1, textAlign: 'right' }]}>Weight</ThemedText>
              <ThemedText style={[styles.tableHeaderText, { flex: 1, textAlign: 'right' }]}>Gold Loss</ThemedText>
              <ThemedText style={[styles.tableHeaderText, { flex: 1, textAlign: 'right' }]}>Cost/g</ThemedText>
              <ThemedText style={[styles.tableHeaderText, { flex: 1, textAlign: 'right' }]}>SMC</ThemedText>
              <ThemedText style={[styles.tableHeaderText, { flex: 0.8, textAlign: 'right' }]}>PCS</ThemedText>
              <ThemedText style={[styles.tableHeaderText, { flex: 1.3, textAlign: 'right' }]}>Total</ThemedText>
              <ThemedText style={[styles.tableHeaderText, { width: 60, textAlign: 'center' }]}>Act</ThemedText>
            </View>

            {/* Table Rows */}
            {details.length === 0 ? (
              <View style={styles.emptyTableState}>
                <Ionicons name="cart-outline" size={30} color="#D0C2A8" />
                <ThemedText style={styles.emptyTableTitle}>No items added yet</ThemedText>
                <ThemedText style={styles.emptyTableSubtitle}>
                  Fill in the item code and attributes above, then click '+ Add Item'
                </ThemedText>
              </View>
            ) : (
              details.map((item, index) => (
                <View
                  key={index.toString()}
                  style={[
                    styles.tableRow,
                    index % 2 === 1 && { backgroundColor: '#FCFAF6' },
                    selectedIndex === index && styles.tableRowSelected,
                  ]}
                >
                  <ThemedText style={[styles.tableCellText, { flex: 1.4, fontWeight: 'bold', color: '#8D6409' }]}>
                    {item.InventoryCode}
                  </ThemedText>
                  <ThemedText style={[styles.tableCellText, { flex: 2.3 }]} numberOfLines={1}>
                    {item.Description}
                  </ThemedText>
                  <ThemedText style={[styles.tableCellText, { flex: 1, textAlign: 'right' }]}>
                    {parseFloat(item.Weight || 0).toFixed(2)}
                  </ThemedText>
                  <ThemedText style={[styles.tableCellText, { flex: 1, textAlign: 'right' }]}>
                    {parseFloat(item.GoldLoss || 0).toFixed(2)}
                  </ThemedText>
                  <ThemedText style={[styles.tableCellText, { flex: 1, textAlign: 'right' }]}>
                    {parseFloat(item.CostPerGram || 0).toFixed(2)}
                  </ThemedText>
                  <ThemedText style={[styles.tableCellText, { flex: 1, textAlign: 'right' }]}>
                    {parseFloat(item.SMC || 0).toFixed(2)}
                  </ThemedText>
                  <ThemedText style={[styles.tableCellText, { flex: 0.8, textAlign: 'right', fontWeight: 'bold' }]}>
                    {item.PCS}
                  </ThemedText>
                  <ThemedText style={[styles.tableCellText, { flex: 1.3, textAlign: 'right', fontWeight: 'bold', color: '#9E6D08' }]}>
                    {parseFloat(item.TotalValue || 0).toFixed(2)}
                  </ThemedText>
                  <View style={{ width: 60, flexDirection: 'row', justifyContent: 'center', gap: 6 }}>
                    <TouchableOpacity
                      onPress={() => editDetail(index)}
                      activeOpacity={0.7}
                      style={{ padding: 4 }}
                    >
                      <Ionicons name="pencil" size={16} color="#B48218" />
                    </TouchableOpacity>
                    <TouchableOpacity
                      onPress={() => removeDetail(index)}
                      activeOpacity={0.7}
                      style={{ padding: 4 }}
                    >
                      <Ionicons name="trash-outline" size={16} color="#D93829" />
                    </TouchableOpacity>
                  </View>
                </View>
              ))
            )}
          </View>
        </View>
      </ScrollView>

      {/* LOV Modal */}
      <Modal visible={lovVisible} animationType="fade" transparent>
        <TouchableOpacity style={styles.modalOverlay} activeOpacity={1} onPress={() => setLovVisible(false)}>
          <TouchableWithoutFeedback>
            <View style={styles.modalCard}>
              <View style={styles.modalHeader}>
                <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                  <View style={styles.headerIconBadge}>
                    <Ionicons name="list" size={14} color="#B48218" />
                  </View>
                  <ThemedText style={styles.modalTitle}>Select {lovTitle}</ThemedText>
                </View>
                <TouchableOpacity onPress={() => setLovVisible(false)} style={{ padding: 4 }}>
                  <Ionicons name="close" size={20} color="#554428" />
                </TouchableOpacity>
              </View>

              <View style={{ padding: 10, borderBottomWidth: 1, borderBottomColor: '#EBE2D2' }}>
                <TextInput
                  style={styles.input}
                  placeholder="Filter records..."
                  placeholderTextColor="#9E8D77"
                  value={lovSearch}
                  onChangeText={(text) => {
                    setLovSearch(text);
                    const lower = text.toLowerCase();
                    setFilteredLovData(lovData.filter((item: any) =>
                      Object.values(item).some(val => String(val).toLowerCase().includes(lower))
                    ));
                  }}
                />
              </View>

              {/* Table Column Headers */}
              <View style={styles.lovHeadRow}>
                {lovTitle === 'Customer' ? (
                  <>
                    <ThemedText style={[styles.lovHeadText, { flex: 1.2 }]}>Code</ThemedText>
                    <ThemedText style={[styles.lovHeadText, { flex: 2 }]}>Customer Name</ThemedText>
                    <ThemedText style={[styles.lovHeadText, { flex: 2 }]}>Address</ThemedText>
                  </>
                ) : lovTitle === 'Salesman' ? (
                  <>
                    <ThemedText style={[styles.lovHeadText, { flex: 1.2 }]}>Code</ThemedText>
                    <ThemedText style={[styles.lovHeadText, { flex: 2.8 }]}>Salesman Name</ThemedText>
                  </>
                ) : lovTitle === 'Inventory' ? (
                  <>
                    <ThemedText style={[styles.lovHeadText, { flex: 1.4 }]}>Inventory Code</ThemedText>
                    <ThemedText style={[styles.lovHeadText, { flex: 2.2 }]}>Description</ThemedText>
                    <ThemedText style={[styles.lovHeadText, { flex: 1, textAlign: 'right' }]}>Weight(g)</ThemedText>
                  </>
                ) : lovTitle === 'Invoice' ? (
                  <>
                    <ThemedText style={[styles.lovHeadText, { flex: 1.5 }]}>Invoice No</ThemedText>
                    <ThemedText style={[styles.lovHeadText, { flex: 2.2 }]}>Customer</ThemedText>
                    <ThemedText style={[styles.lovHeadText, { flex: 1.2, textAlign: 'right' }]}>Net Total</ThemedText>
                  </>
                ) : (
                  <ThemedText style={[styles.lovHeadText, { flex: 1 }]}>Record</ThemedText>
                )}
                <View style={{ width: 18 }} />
              </View>

              <View style={{ maxHeight: 360, minHeight: 140 }}>
                {filteredLovData.length === 0 ? (
                  <View style={{ padding: 25, alignItems: 'center' }}>
                    <ThemedText style={styles.emptyTableSubtitle}>No records found</ThemedText>
                  </View>
                ) : (
                  <FlatList
                    data={filteredLovData}
                    keyExtractor={(_, i) => i.toString()}
                    renderItem={({ item, index }) => (
                      <TouchableOpacity
                        style={[
                          styles.lovItem,
                          index % 2 === 1 && { backgroundColor: '#FCFAF6' },
                        ]}
                        activeOpacity={0.7}
                        onPress={() => handleSelectLovItem(item)}
                      >
                        {lovTitle === 'Customer' ? (
                          <>
                            <ThemedText style={[styles.lovItemCode, { flex: 1.2 }]} numberOfLines={1}>
                              {item.CustomerCode}
                            </ThemedText>
                            <ThemedText style={[styles.lovItemText, { flex: 2 }]} numberOfLines={1}>
                              {item.CustomerName}
                            </ThemedText>
                            <ThemedText style={[styles.lovItemSubText, { flex: 2 }]} numberOfLines={1}>
                              {item.Address1 || '-'}
                            </ThemedText>
                          </>
                        ) : lovTitle === 'Salesman' ? (
                          <>
                            <ThemedText style={[styles.lovItemCode, { flex: 1.2 }]} numberOfLines={1}>
                              {item.SalesManCode}
                            </ThemedText>
                            <ThemedText style={[styles.lovItemText, { flex: 2.8 }]} numberOfLines={1}>
                              {item.SalesManName || '-'}
                            </ThemedText>
                          </>
                        ) : lovTitle === 'Inventory' ? (
                          <>
                            <ThemedText style={[styles.lovItemCode, { flex: 1.4 }]} numberOfLines={1}>
                              {item.InventoryCode}
                            </ThemedText>
                            <ThemedText style={[styles.lovItemText, { flex: 2.2 }]} numberOfLines={1}>
                              {item.Description || '-'}
                            </ThemedText>
                            <ThemedText style={[styles.lovItemText, { flex: 1, textAlign: 'right', fontWeight: 'bold' }]}>
                              {parseFloat(item.Weight || 0).toFixed(2)}
                            </ThemedText>
                          </>
                        ) : lovTitle === 'Invoice' ? (
                          <>
                            <ThemedText style={[styles.lovItemCode, { flex: 1.5 }]} numberOfLines={1}>
                              {item.InvoiceNo}
                            </ThemedText>
                            <ThemedText style={[styles.lovItemText, { flex: 2.2 }]} numberOfLines={1}>
                              {item.CustomerName || '-'}
                            </ThemedText>
                            <ThemedText style={[styles.lovItemText, { flex: 1.2, textAlign: 'right', fontWeight: 'bold', color: '#9E6D08' }]}>
                              {parseFloat(item.NetTotal || 0).toFixed(2)}
                            </ThemedText>
                          </>
                        ) : (
                          <ThemedText style={[styles.lovItemText, { flex: 1 }]}>
                            {Object.values(item).filter(v => typeof v === 'string' || typeof v === 'number').slice(0, 3).join(' - ')}
                          </ThemedText>
                        )}
                        <Ionicons name="chevron-forward" size={14} color="#B48218" style={{ marginLeft: 6 }} />
                      </TouchableOpacity>
                    )}
                  />
                )}
              </View>
            </View>
          </TouchableWithoutFeedback>
        </TouchableOpacity>
      </Modal>

      {/* Salesman Password Modal */}
      <Modal visible={passwordModalVisible} transparent animationType="fade">
        <TouchableOpacity style={styles.modalOverlay} activeOpacity={1}>
          <TouchableWithoutFeedback>
            <View style={[styles.modalCard, { maxWidth: 320 }]}>
              <View style={styles.modalHeader}>
                <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                  <View style={styles.headerIconBadge}>
                    <Ionicons name="lock-closed" size={14} color="#B48218" />
                  </View>
                  <ThemedText style={styles.modalTitle}>SalesMan Authorization</ThemedText>
                </View>
              </View>

              <View style={{ padding: 16, alignItems: 'center' }}>
                <ThemedText style={{ fontSize: 12, color: '#554428', marginBottom: 12, textAlign: 'center' }}>
                  Enter SalesMan Password to access Wholesale POS
                </ThemedText>
                <TextInput
                  style={[
                    styles.input,
                    {
                      width: '100%',
                      textAlign: 'center',
                      fontSize: 14,
                      fontWeight: 'bold',
                      letterSpacing: 2,
                      borderColor: '#B8903B',
                      marginBottom: 16
                    }
                  ]}
                  value={salesmanPassword}
                  onChangeText={setSalesmanPassword}
                  secureTextEntry
                  placeholder="Password"
                  placeholderTextColor="#9E8D77"
                  autoFocus
                  onSubmitEditing={verifySalesmanPassword}
                />
                <View style={{ flexDirection: 'row', gap: 10, width: '100%' }}>
                  <TouchableOpacity
                    style={[styles.headerBtnSecondary, { flex: 1, justifyContent: 'center' }]}
                    onPress={() => router.back()}
                  >
                    <ThemedText style={styles.headerBtnSecondaryText}>Cancel</ThemedText>
                  </TouchableOpacity>
                  <TouchableOpacity
                    style={[styles.headerBtnPrimary, { flex: 1, justifyContent: 'center' }]}
                    onPress={verifySalesmanPassword}
                    disabled={verifyingPassword}
                  >
                    {verifyingPassword ? (
                      <ActivityIndicator size="small" color="#9E6D08" />
                    ) : (
                      <ThemedText style={styles.headerBtnPrimaryText}>Verify</ThemedText>
                    )}
                  </TouchableOpacity>
                </View>
              </View>
            </View>
          </TouchableWithoutFeedback>
        </TouchableOpacity>
      </Modal>

      {/* Toast Alert */}
      {toast.visible && (
        <View style={[styles.toastContainer, toast.type === 'error' ? styles.toastError : styles.toastSuccess]}>
          <Ionicons
            name={toast.type === 'error' ? 'alert-circle' : 'checkmark-circle'}
            size={16}
            color="#FFFFFF"
            style={{ marginRight: 6 }}
          />
          <ThemedText style={styles.toastText}>{toast.message}</ThemedText>
        </View>
      )}

      {loading && (
        <View style={styles.loadingOverlay}>
          <ActivityIndicator size="large" color="#B48218" />
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
  contentContainer: {
    padding: 8,
  },

  // ===== TOP HEADER BAR =====
  header: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    backgroundColor: '#FFFFFF',
    borderBottomWidth: 1,
    borderBottomColor: '#EBE2D2',
    paddingHorizontal: 12,
    paddingVertical: 8,
  },
  headerLeft: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  backButton: {
    padding: 4,
    marginRight: 8,
  },
  headerTitle: {
    fontSize: 16,
    fontWeight: 'bold',
    color: '#3B2D17',
    letterSpacing: 0.3,
  },
  headerActions: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 8,
  },
  headerBtnSecondary: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#DFD3BC',
    borderRadius: 6,
    paddingHorizontal: 10,
    paddingVertical: 5,
  },
  headerBtnSecondaryText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#554428',
  },
  headerBtnPrimary: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#F5E7CA',
    borderWidth: 1,
    borderColor: '#D4C3A3',
    borderRadius: 6,
    paddingHorizontal: 12,
    paddingVertical: 5,
  },
  headerBtnPrimaryText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#9E6D08',
  },

  // ===== TOP SPLIT ROW =====
  topSplitRow: {
    flexDirection: 'row',
    gap: 8,
  },

  // ===== LUXURY GOLD CARD =====
  goldCard: {
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    shadowColor: '#A38035',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.08,
    shadowRadius: 4,
    elevation: 2,
    overflow: 'hidden',
  },
  cardHeaderBanner: {
    backgroundColor: '#F5E7CA',
    paddingHorizontal: 10,
    paddingVertical: 6,
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
  },
  headerIconBadge: {
    width: 22,
    height: 22,
    borderRadius: 5,
    backgroundColor: '#FFF8EA',
    borderWidth: 1,
    borderColor: '#E2D2AF',
    alignItems: 'center',
    justifyContent: 'center',
    marginRight: 6,
  },
  cardHeaderTitle: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#3B2D17',
    letterSpacing: 0.3,
  },
  cardBody: {
    padding: 8,
  },
  counterBadge: {
    backgroundColor: '#FAF5EA',
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderRadius: 10,
    paddingHorizontal: 7,
    paddingVertical: 1,
    marginLeft: 8,
  },
  counterBadgeText: {
    fontSize: 10,
    fontWeight: 'bold',
    color: '#7D5E19',
  },

  // ===== FORM ROWS & INPUTS =====
  formRow: {
    flexDirection: 'row',
    alignItems: 'center',
    marginBottom: 6,
  },
  inputLabel: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#4A3B2C',
    marginBottom: 2,
  },
  inlineFormLabel: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#4A3B2C',
    width: 95,
    marginRight: 6,
  },
  input: {
    height: 28,
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderRadius: 6,
    paddingHorizontal: 8,
    paddingVertical: 0,
    fontSize: 12,
    color: '#2B2118',
  },
  readonlyInput: {
    backgroundColor: '#F8F5EE',
    borderColor: '#EADBCA',
    color: '#554736',
  },
  lookupBtn: {
    width: 28,
    height: 28,
    backgroundColor: '#FAF5EA',
    borderWidth: 1,
    borderColor: '#D4C3A3',
    borderRadius: 5,
    marginLeft: 4,
    alignItems: 'center',
    justifyContent: 'center',
    flexShrink: 0,
  },
  lookupBtnText: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#6E5528',
    letterSpacing: 1,
    marginTop: -2,
  },

  // ===== PILL BOX & RADIOS =====
  pillBox: {
    flexDirection: 'row',
    alignItems: 'center',
    borderWidth: 1,
    borderColor: '#DFD3BC',
    borderRadius: 14,
    paddingHorizontal: 6,
    paddingVertical: 2,
    backgroundColor: '#FBF8F2',
  },
  radioContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    marginHorizontal: 5,
  },
  radioCircle: {
    width: 13,
    height: 13,
    borderRadius: 7,
    borderWidth: 1.5,
    borderColor: '#B8903B',
    alignItems: 'center',
    justifyContent: 'center',
    marginRight: 4,
    backgroundColor: '#FFFFFF',
  },
  radioCircleSelected: {
    borderColor: '#B8903B',
  },
  radioInner: {
    width: 5,
    height: 5,
    borderRadius: 2.5,
    backgroundColor: '#B8903B',
  },
  radioLabel: {
    fontSize: 10,
    color: '#362B1A',
    fontWeight: 'bold',
  },

  // ===== SUMMARY PANEL =====
  summaryBody: {
    padding: 8,
    gap: 4,
  },
  summaryRow: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingVertical: 2,
  },
  summaryLabel: {
    fontSize: 11,
    color: '#5A4A38',
    fontWeight: '500',
  },
  summaryValue: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#2B2118',
  },
  summaryInput: {
    height: 24,
    width: 80,
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderRadius: 4,
    paddingHorizontal: 6,
    paddingVertical: 0,
    fontSize: 11,
    fontWeight: 'bold',
    color: '#2B2118',
    textAlign: 'right',
  },
  summaryTotalRow: {
    borderTopWidth: 1,
    borderTopColor: '#EBE2D2',
    marginTop: 4,
    paddingTop: 6,
  },
  summaryTotalLabel: {
    fontSize: 13,
    fontWeight: 'bold',
    color: '#3B2D17',
  },
  summaryTotalValue: {
    fontSize: 14,
    fontWeight: 'bold',
    color: '#B48218',
  },

  // ===== ENTRY FORM LABELS & BUTTONS =====
  entryLabel: {
    fontSize: 10,
    fontWeight: 'bold',
    color: '#6E5528',
    marginBottom: 2,
  },
  addItemBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FAF2DE',
    borderWidth: 1,
    borderColor: '#DECFA9',
    borderRadius: 5,
    paddingHorizontal: 10,
    paddingVertical: 4,
  },
  addItemBtnText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#7D5E19',
  },

  // ===== 100% FULL-WIDTH DATA TABLE =====
  tableHeader: {
    flexDirection: 'row',
    backgroundColor: '#F5E7CA',
    paddingVertical: 6,
    paddingHorizontal: 8,
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    alignItems: 'center',
  },
  tableHeaderText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3B2D17',
    textTransform: 'uppercase',
  },
  tableRow: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingVertical: 6,
    paddingHorizontal: 8,
    borderBottomWidth: 1,
    borderBottomColor: '#F3EBDD',
  },
  tableRowSelected: {
    backgroundColor: '#FFF6DF',
    borderLeftWidth: 3,
    borderLeftColor: '#B8903B',
  },
  tableCellText: {
    fontSize: 11,
    color: '#3B2D17',
  },
  emptyTableState: {
    padding: 30,
    alignItems: 'center',
    justifyContent: 'center',
  },
  emptyTableTitle: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#3B2D17',
    marginTop: 4,
  },
  emptyTableSubtitle: {
    fontSize: 11,
    color: '#8F816E',
    marginTop: 2,
    textAlign: 'center',
  },

  // ===== MODAL STYLING =====
  modalOverlay: {
    flex: 1,
    backgroundColor: 'rgba(0,0,0,0.4)',
    justifyContent: 'center',
    alignItems: 'center',
    padding: 20,
  },
  modalCard: {
    width: '100%',
    maxWidth: 620,
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    overflow: 'hidden',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.15,
    shadowRadius: 10,
    elevation: 6,
  },
  modalHeader: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    paddingHorizontal: 12,
    paddingVertical: 8,
    backgroundColor: '#F5E7CA',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
  },
  modalTitle: {
    fontSize: 13,
    fontWeight: 'bold',
    color: '#3B2D17',
  },
  lovHeadRow: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#F7EED9',
    paddingVertical: 7,
    paddingHorizontal: 12,
    borderBottomWidth: 1,
    borderBottomColor: '#DFD2B5',
  },
  lovHeadText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#8D6409',
  },
  lovItem: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingVertical: 8,
    paddingHorizontal: 12,
    borderBottomWidth: 1,
    borderBottomColor: '#F3EBDD',
  },
  lovItemCode: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#8D6409',
  },
  lovItemText: {
    fontSize: 11,
    fontWeight: '500',
    color: '#3B2D17',
  },
  lovItemSubText: {
    fontSize: 11,
    color: '#7A6B56',
  },

  // ===== TOAST & OVERLAYS =====
  loadingOverlay: {
    ...StyleSheet.absoluteFillObject,
    backgroundColor: 'rgba(255, 255, 255, 0.6)',
    justifyContent: 'center',
    alignItems: 'center',
    zIndex: 1000,
  },
  toastContainer: {
    position: 'absolute',
    bottom: 30,
    alignSelf: 'center',
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 16,
    paddingVertical: 10,
    borderRadius: 8,
    zIndex: 9999,
    shadowColor: '#000',
    shadowOpacity: 0.15,
    shadowRadius: 8,
    elevation: 4,
  },
  toastSuccess: {
    backgroundColor: '#2E7D32',
  },
  toastError: {
    backgroundColor: '#C62828',
  },
  toastText: {
    color: '#FFFFFF',
    fontWeight: 'bold',
    fontSize: 12,
  },
});
