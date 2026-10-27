import { ThemedText } from '@/components/themed-text';
import { API_BASE_URL } from '@/constants/api';
import { useThemeColors } from '@/hooks/useThemeColors';
import { Ionicons } from '@expo/vector-icons';
import DateTimePicker from '@react-native-community/datetimepicker';
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

export default function InstallmentOrderEntryScreen() {
  const themeColors = useThemeColors();
  const router = useRouter();

  const [loading, setLoading] = useState(false);
  const [saving, setSaving] = useState(false);

  // Salesman Password State
  const [isPasswordVisible, setIsPasswordVisible] = useState(true);
  const [salesmanPassword, setSalesmanPassword] = useState('');

  // Global Config
  const [globalGst, setGlobalGst] = useState(0);

  // Modes
  const [mode, setMode] = useState<'Installment' | 'Order'>('Installment');
  const [paymentMode, setPaymentMode] = useState<'Gold' | 'Cash' | 'Both'>('Cash');

  // Header State
  const [invoiceNo, setInvoiceNo] = useState('');
  const [customerCode, setCustomerCode] = useState('');
  const [customerType, setCustomerType] = useState('Customer Type');
  const [flexiId, setFlexiId] = useState('');
  const [customerName, setCustomerName] = useState('');
  const [address1, setAddress1] = useState('');
  const [address2, setAddress2] = useState('');
  const [address3, setAddress3] = useState('');
  const [remarks, setRemarks] = useState('');

  // Amounts
  const [tradeIn, setTradeIn] = useState('0.00');
  const [totalAmount, setTotalAmount] = useState('0.00');
  const [gstType, setGstType] = useState<'Exclusive' | 'Inclusive' | 'ZeroRated'>('Exclusive');
  const [salesman, setSalesman] = useState('');
  const [depositAmount, setDepositAmount] = useState('0.00');
  const [amountPaid, setAmountPaid] = useState('0.00');
  const [installmentAmt, setInstallmentAmt] = useState('0.00');

  const formatDate = (date: Date) => {
    const d = String(date.getDate()).padStart(2, '0');
    const m = String(date.getMonth() + 1).padStart(2, '0');
    const y = date.getFullYear();
    return `${d}-${m}-${y}`;
  };

  const [dueDate, setDueDate] = useState(formatDate(new Date()));
  const [orderDate, setOrderDate] = useState(formatDate(new Date()));
  const [issueDate, setIssueDate] = useState(formatDate(new Date()));

  const [showPicker, setShowPicker] = useState<{ visible: boolean; mode: 'dueDate' | 'orderDate' | 'issueDate' }>({
    visible: false,
    mode: 'dueDate',
  });
  const [estimatedWeight, setEstimatedWeight] = useState('');
  const [costPerGram, setCostPerGram] = useState('');
  const [estimatedGst, setEstimatedGst] = useState('');
  const [supplierCode, setSupplierCode] = useState('');
  const [rateFixed, setRateFixed] = useState<'Y' | 'N'>('Y');

  // Entry Item
  const [entryItem, setEntryItem] = useState({
    InventoryCode: '',
    Description: '',
    Weight: '0.00',
    GoldLoss: '0.00',
    Cost: '0.00',
    PCS: '1',
    SMC: '0.00',
    GST: '0.00',
    TotalValue: '0.00',
    ClassCode: '',
    CategoryCode: '',
    CatalogueCode: '',
    StoneWeight: '0.00',
    SizeCode: '',
    SalesType: '',
    StoneDescription: '',
    StoneCost: '0.00',
  });

  const clearEntry = () => {
    setEntryItem({
      InventoryCode: '',
      Description: '',
      Weight: '0.00',
      GoldLoss: '0.00',
      Cost: '0.00',
      PCS: '1',
      SMC: '0.00',
      GST: '0.00',
      TotalValue: '0.00',
      ClassCode: '',
      CategoryCode: '',
      CatalogueCode: '',
      StoneWeight: '0.00',
      SizeCode: '',
      SalesType: '',
      StoneDescription: '',
      StoneCost: '0.00',
    });
  };

  // Toast
  const [successMsg, setSuccessMsg] = useState('');
  const [errorMsg, setErrorMsg] = useState('');

  const showToast = (title: string, message: string) => {
    if (title === 'Success') {
      setSuccessMsg(message);
      setTimeout(() => setSuccessMsg(''), 3000);
    } else {
      setErrorMsg(message);
      setTimeout(() => setErrorMsg(''), 3000);
    }
  };

  const verifyPassword = async () => {
    if (!salesmanPassword.trim()) {
      showToast('Error', 'Please enter password');
      return;
    }
    setLoading(true);
    try {
      const res = await fetch(`${API_BASE_URL}/installmentorderentry/validate-salesman`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ password: salesmanPassword }),
      });
      const json = await res.json();
      if (json.success) {
        setSalesman(json.salesmanCode || '');
        setIsPasswordVisible(false);
      } else {
        showToast('Error', 'Invalid Salesman Password');
      }
    } catch (err) {
      showToast('Error', 'Network error validating password');
    } finally {
      setLoading(false);
    }
  };

  // Grid Data
  const [gridItems, setGridItems] = useState<any[]>([]);
  const [selectedRowIndex, setSelectedRowIndex] = useState<number | null>(null);

  // LOV State
  const [lovVisible, setLovVisible] = useState(false);
  const [lovType, setLovType] = useState<
    'customer' | 'inventory' | 'invoice' | 'class' | 'category' | 'catalogue' | 'supplier' | 'size' | 'salesman'
  >('customer');
  const [lovData, setLovData] = useState<any[]>([]);
  const [lovSearchQuery, setLovSearchQuery] = useState('');

  useEffect(() => {
    if (lovVisible) {
      fetchLovData();
    }
  }, [lovVisible, lovType, lovSearchQuery]);

  const fetchLovData = async () => {
    try {
      const url = `${API_BASE_URL}/installmentorderentry/lov?type=${lovType}&mode=${mode}&search=${encodeURIComponent(
        lovSearchQuery
      )}`;
      const res = await fetch(url);
      const data = await res.json();
      if (data.success && data.data) {
        setLovData(data.data);
      } else {
        setLovData([]);
      }
    } catch (e) {
      setLovData([]);
    }
  };

  const handleLovSelect = (item: any) => {
    if (lovType === 'customer') {
      setCustomerCode(item.Code || '');
      setCustomerName(item.Name || '');
      setAddress1(item.Address1 || '');
      setAddress2(item.Address2 || '');
      setAddress3(item.Address3 || '');
    } else if (lovType === 'invoice') {
      setInvoiceNo(item.Code);
      setTimeout(() => fetchInvoiceInfo(item.Code), 100);
    } else if (lovType === 'inventory') {
      setEntryItem({ ...entryItem, InventoryCode: item.Code });
      setTimeout(() => fetchInventoryDetails(item.Code), 100);
    } else if (lovType === 'class') {
      setEntryItem({ ...entryItem, ClassCode: item.Code });
    } else if (lovType === 'category') {
      setEntryItem({ ...entryItem, CategoryCode: item.Code });
    } else if (lovType === 'catalogue') {
      setEntryItem({ ...entryItem, CatalogueCode: item.Code });
    } else if (lovType === 'supplier') {
      setSupplierCode(item.Code);
    } else if (lovType === 'size') {
      setEntryItem({ ...entryItem, SizeCode: item.Code });
    } else if (lovType === 'salesman') {
      setSalesman(item.Code);
    }
    setLovVisible(false);
  };

  useEffect(() => {
    fetchCompanyConfig();
  }, []);

  const fetchCompanyConfig = async () => {
    try {
      const res = await fetch(`${API_BASE_URL}/installmentorderentry/company`);
      const data = await res.json();
      if (data.success) {
        setGlobalGst(data.company.GstPerc || data.company.GSTPerc || 0);
      }
    } catch (e) {
      console.log('Error fetching company config', e);
    }
  };

  // Recalculate totals whenever grid changes
  useEffect(() => {
    let tot = 0;
    let totWeight = 0;
    let totCost = 0;
    let totGst = 0;

    gridItems.forEach((item) => {
      tot += parseFloat(item.TotalValue) || 0;
      totWeight += parseFloat(item.Weight) || 0;
      totCost += parseFloat(item.Cost) || 0;
      totGst += parseFloat(item.GST) || 0;
    });

    const calculatedTotal = tot.toFixed(2);
    setTotalAmount(calculatedTotal);
    setEstimatedWeight(totWeight.toFixed(2));
    setCostPerGram(totCost.toFixed(2));
    setEstimatedGst(totGst.toFixed(2));

    const dep = parseFloat(depositAmount) || 0;
    const tiAmt = parseFloat(tradeIn) || 0;
    let instAmt = 0;

    if (mode === 'Installment') {
      if (paymentMode === 'Cash' || paymentMode === 'Gold') {
        instAmt = tot - dep;
      } else if (paymentMode === 'Both') {
        instAmt = tot - tiAmt - dep;
      }
    } else {
      instAmt = tot - dep;
    }
    setInstallmentAmt(instAmt > 0 ? instAmt.toFixed(2) : '0.00');
  }, [gridItems, depositAmount, amountPaid, paymentMode, tradeIn, mode]);

  const clearForm = () => {
    setInvoiceNo('');
    setCustomerCode('');
    setCustomerType('Customer Type');
    setFlexiId('');
    setCustomerName('');
    setAddress1('');
    setAddress2('');
    setAddress3('');
    setRemarks('');
    setTradeIn('0.00');
    setTotalAmount('0.00');
    setDepositAmount('0.00');
    setAmountPaid('0.00');
    setInstallmentAmt('0.00');
    setEstimatedWeight('0.00');
    setCostPerGram('0.00');
    setEstimatedGst('0.00');
    setSupplierCode('');
    setGridItems([]);
    setSelectedRowIndex(null);
    clearEntry();
  };

  const calculateEntryTotals = (item: any, currentGstType = gstType) => {
    const cost = parseFloat(item.Cost) || 0;
    const weight = parseFloat(item.Weight) || 0;
    const goldLoss = parseFloat(item.GoldLoss) || 0;
    const pcs = parseFloat(item.PCS) || 1;
    const smc = parseFloat(item.SMC) || 0;

    let baseValue = 0;
    const salesType = item.SalesType || '';

    if (salesType === 'P' || salesType === 'B') {
      baseValue = (weight + goldLoss) * cost + smc;
    } else if (salesType) {
      baseValue = smc * pcs;
    } else {
      baseValue = cost * weight * pcs + smc;
    }

    let gstAmt = 0;
    let total = baseValue;

    if (currentGstType === 'Exclusive' || currentGstType === 'Inclusive') {
      gstAmt = (baseValue * globalGst) / 100;
      total = baseValue + gstAmt;
    } else if (currentGstType === 'ZeroRated') {
      gstAmt = 0;
      total = baseValue;
    }

    return {
      ...item,
      GST: gstAmt.toFixed(2),
      TotalValue: total.toFixed(2),
    };
  };

  const handleEntryChange = (field: string, value: string) => {
    const updated = { ...entryItem, [field]: value };
    setEntryItem(calculateEntryTotals(updated));
  };

  const fetchInvoiceInfo = async (overrideCode?: string) => {
    const codeToUse = overrideCode || invoiceNo.trim();
    if (!codeToUse) return;
    setLoading(true);
    try {
      const res = await fetch(
        `${API_BASE_URL}/installmentorderentry/header/${encodeURIComponent(codeToUse)}?type=${mode}`
      );
      const data = await res.json();
      if (data.success) {
        const h = data.header;
        if (mode === 'Installment') {
          setCustomerCode(h.CustomerCode || '');
          setCustomerName(h.Name || h.CustomerName || '');
          setAddress1(h.Address1 || '');
          setAddress2(h.Address2 || '');
          setAddress3(h.Address3 || '');
          setCustomerType(h.CustomerType || 'Customer Type');
          setRemarks(h.Remarks || '');
          setDepositAmount(parseFloat(h.DepositAmt || 0).toFixed(2));
          setAmountPaid(parseFloat(h.AmtPaid || 0).toFixed(2));
          setInstallmentAmt(parseFloat(h.InstallmentAmt || 0).toFixed(2));
          setTradeIn(parseFloat(h.TradeINAmount || 0).toFixed(2));
          setTotalAmount(parseFloat(h.TotalCost || 0).toFixed(2));
          if (h.Invdate) setIssueDate(formatDate(new Date(h.Invdate)));
          if (h.DueDate) setDueDate(formatDate(new Date(h.DueDate)));
          setGridItems(
            data.details.map((d: any) => ({
              ...d,
              TotalValue: d.TotalValue || d.Cost || '0.00',
              PCS: d.PCS || d.Pcs || '1',
            }))
          );
        } else {
          setCustomerCode(h.CustomerCode || '');
          setCustomerName(h.Name || h.CustomerName || '');
          setAddress1(h.Address1 || '');
          setAddress2(h.Address2 || '');
          setAddress3(h.Address3 || '');
          setCustomerType(h.CustomerType || 'Customer Type');
          setRemarks(h.Remarks || '');
          setDepositAmount(parseFloat(h.AdvancePaid || 0).toFixed(2));
          setTotalAmount(parseFloat(h.TotalCostEstimated || 0).toFixed(2));
          setTradeIn(parseFloat(h.TradeINAmount || 0).toFixed(2));
          if (h.OrderDate) setOrderDate(formatDate(new Date(h.OrderDate)));
          if (h.DeliveryDate) setDueDate(formatDate(new Date(h.DeliveryDate)));
          setGridItems(
            data.details.map((d: any) => ({
              InventoryCode: d.OrderInventoryCode || '',
              Description: d.Description || '',
              Weight: d.WeightEstimated || '0.00',
              GoldLoss: d.GoldLoss || '0.00',
              Cost: d.CostPerGram || '0.00',
              SMC: d.MC || '0.00',
              TotalValue: d.EstimatedCost || '0.00',
              GST: d.EstimatedGST || d.EstimatedGst || '0.00',
              ClassCode: d.ClassCode || '',
              CategoryCode: d.CategoryCode || '',
              CatalogueCode: d.CatalogueCode || '',
              StoneWeight: d.StoneWeight || '0.00',
              StoneDescription: d.StoneDescription || '',
              StoneCost: d.StoneCost || '0.00',
              SizeCode: d.SizeCode || '',
              PCS: '1',
            }))
          );
        }
        showToast('Success', 'Invoice record loaded');
      } else {
        showToast('Error', data.message || 'Invoice not found');
      }
    } catch (err) {
      showToast('Error', 'Failed to fetch invoice');
    } finally {
      setLoading(false);
    }
  };

  const fetchInventoryDetails = async (code: string) => {
    if (!code.trim()) return;
    setLoading(true);
    try {
      const res = await fetch(`${API_BASE_URL}/installmentorderentry/inventory/${encodeURIComponent(code.trim())}`);
      const data = await res.json();
      if (data.success) {
        const itm = data.item;
        const updated = {
          ...entryItem,
          InventoryCode: itm.InventoryCode,
          Description: itm.Description,
          Weight: parseFloat(itm.Weight || 0).toFixed(3),
          GoldLoss: parseFloat(itm.GoldLoss || 0).toFixed(3),
          Cost: parseFloat(itm.AverageCost || itm.SalesPrice || 0).toFixed(2),
          ClassCode: itm.ClassCode || '',
          CategoryCode: itm.CategoryCode || '',
          CatalogueCode: itm.CatalogueCode || '',
        };
        setEntryItem(calculateEntryTotals(updated));
      } else {
        showToast('Error', data.message);
      }
    } catch (e) {
      showToast('Error', 'Failed to fetch inventory');
    } finally {
      setLoading(false);
    }
  };

  const addToGrid = () => {
    if (mode === 'Installment') {
      if (!entryItem.InventoryCode) {
        showToast('Error', 'Please enter an Inventory Code');
        return;
      }
    } else {
      if (!entryItem.Description && !entryItem.ClassCode) {
        showToast('Error', 'Please specify item details');
        return;
      }
    }
    const val = parseFloat(entryItem.TotalValue);
    if (isNaN(val) || val <= 0) {
      showToast('Error', 'Total Value cannot be zero');
      return;
    }
    setGridItems([...gridItems, entryItem]);
    clearEntry();
  };

  const removeFromGrid = () => {
    if (selectedRowIndex === null) {
      showToast('Error', 'Please select a row to delete');
      return;
    }
    const newItems = [...gridItems];
    newItems.splice(selectedRowIndex, 1);
    setGridItems(newItems);
    setSelectedRowIndex(null);
  };

  const validateMe = () => {
    if (!invoiceNo || invoiceNo.trim() === '') {
      showToast('Error', mode === 'Installment' ? 'Invoice No should not be empty!' : 'Order No should not be empty!');
      return false;
    }
    if (!customerCode) {
      showToast('Error', 'Customer code should not be empty!');
      return false;
    }
    if (gridItems.length === 0) {
      showToast('Error', 'No Item to save.');
      return false;
    }

    const dep = parseFloat(depositAmount);
    const tot = parseFloat(totalAmount);

    if (mode === 'Installment') {
      if (isNaN(dep) || dep <= 0) {
        showToast('Error', 'Deposit Amount should be greater than zero!');
        return false;
      }
      if (dep > tot) {
        showToast('Error', 'Deposit Amount should be lower than the Sales Amount!');
        return false;
      }
      const instAmt = parseFloat(installmentAmt);
      if (isNaN(instAmt) || instAmt <= 0) {
        showToast('Error', 'Installment Amount should be Greater than Zero!');
        return false;
      }
    } else {
      if (isNaN(dep) || dep <= 0) {
        showToast('Error', 'Please enter the Advance amount!');
        return false;
      }
      const bal = tot - dep;
      if (bal <= 0) {
        showToast('Error', 'Balance Amount Should not be Zero.!');
        return false;
      }
    }

    return true;
  };

  const saveEntry = async () => {
    if (!validateMe()) return;
    setSaving(true);
    try {
      const parseDate = (dateStr: string) => {
        const parts = dateStr.split('-');
        if (parts.length === 3) return new Date(`${parts[2]}-${parts[1]}-${parts[0]}`);
        return new Date();
      };

      const headerPayload = {
        invoiceNo,
        InsAgreementNo: invoiceNo,
        insAgreementNo: invoiceNo,
        customerCode,
        customerName,
        customerType,
        address1,
        address2,
        address3,
        remarks,
        salesManCode: salesman,
        depositAmt: depositAmount,
        amtPaid: amountPaid,
        installmentAmt: installmentAmt,
        totalCost: totalAmount,
        tradeInAmount: tradeIn,
        date: new Date().toISOString(),
        lastDate: parseDate(dueDate).toISOString(),
        orderDate: parseDate(orderDate).toISOString(),
        issueDate: parseDate(issueDate).toISOString(),
        rateFixed: mode === 'Order' ? rateFixed : undefined,
      };

      const payload = {
        mode,
        header: headerPayload,
        details: gridItems,
      };

      const res = await fetch(`${API_BASE_URL}/installmentorderentry/save`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload),
      });
      const data = await res.json();
      if (data.success) {
        showToast('Success', 'Transaction Saved Successfully');
        clearForm();
      } else {
        showToast('Error', data.message || 'Failed to save transaction');
      }
    } catch (e) {
      showToast('Error', 'Failed to save');
    } finally {
      setSaving(false);
    }
  };

  // Date picker rendering
  const renderDatePickerInput = (
    value: string,
    onChangeText: (val: string) => void,
    dateMode: 'dueDate' | 'orderDate' | 'issueDate'
  ) => {
    if (Platform.OS === 'web') {
      const parts = value.split('-');
      let webVal = '';
      if (parts.length === 3) webVal = `${parts[2]}-${parts[1]}-${parts[0]}`;
      return (
        <View style={styles.datePickerInputWrap}>
          {React.createElement('input', {
            type: 'date',
            value: webVal,
            onChange: (e: any) => {
              const v = e.target.value;
              if (v) {
                const p = v.split('-');
                onChangeText(`${p[2]}-${p[1]}-${p[0]}`);
              }
            },
            style: {
              width: '100%',
              border: 'none',
              outline: 'none',
              background: 'transparent',
              fontSize: '12px',
              fontWeight: '600',
              color: '#3B2D17',
              paddingLeft: '4px',
              paddingRight: '4px',
              margin: 0,
              boxSizing: 'border-box',
            },
          })}
        </View>
      );
    }

    return (
      <View style={styles.datePickerInputWrap}>
        <TextInput
          style={[styles.formInput, { flex: 1, paddingVertical: 2, height: 26, fontSize: 12 }]}
          value={value}
          onChangeText={onChangeText}
          placeholder="DD-MM-YYYY"
          placeholderTextColor="#9E8D77"
        />
        <TouchableOpacity
          onPress={() => setShowPicker({ visible: true, mode: dateMode })}
          style={styles.calendarIconBtn}
        >
          <Ionicons name="calendar-outline" size={14} color="#8D6409" />
        </TouchableOpacity>
      </View>
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
            <ThemedText style={styles.headerTitle}>Installment & Order Entry</ThemedText>
            <ThemedText style={styles.headerSubtitle}>
              {mode === 'Installment' ? 'Customer Installment Scheme' : 'Special Customer Order'}
            </ThemedText>
          </View>
          {loading && <ActivityIndicator size="small" color="#B48218" style={{ marginLeft: 8 }} />}
        </View>

        <View style={styles.headerRight}>
          <TouchableOpacity
            style={[styles.actionBtn, styles.clearBtn]}
            onPress={clearForm}
            activeOpacity={0.8}
            disabled={saving || loading}
          >
            <Ionicons name="refresh-outline" size={14} color="#5C4A28" />
            <ThemedText style={styles.clearBtnText}>Clear</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity
            style={[styles.actionBtn, styles.printBtn]}
            onPress={() => showToast('Success', 'Printing document...')}
            activeOpacity={0.8}
          >
            <Ionicons name="print-outline" size={14} color="#5C4A28" />
            <ThemedText style={styles.clearBtnText}>Print</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity
            style={[styles.actionBtn, styles.saveBtn]}
            onPress={saveEntry}
            activeOpacity={0.8}
            disabled={saving || loading}
          >
            {saving ? (
              <ActivityIndicator size="small" color="#9E6D08" />
            ) : (
              <>
                <Ionicons name="checkmark-done-outline" size={15} color="#9E6D08" />
                <ThemedText style={styles.saveBtnText}>Save</ThemedText>
              </>
            )}
          </TouchableOpacity>
        </View>
      </View>

      {/* TOAST ALERTS */}
      {(successMsg || errorMsg) && (
        <View style={[styles.toast, errorMsg ? styles.toastError : styles.toastSuccess]}>
          <Ionicons
            name={errorMsg ? 'alert-circle' : 'checkmark-circle'}
            size={16}
            color="#FFFFFF"
            style={{ marginRight: 6 }}
          />
          <ThemedText style={styles.toastText}>{errorMsg || successMsg}</ThemedText>
        </View>
      )}

      {/* SALESMAN PASSWORD MODAL */}
      <Modal visible={isPasswordVisible} transparent animationType="fade">
        <View style={styles.modalOverlay}>
          <View style={styles.passwordModalCard}>
            <View style={styles.passwordHeader}>
              <View style={styles.lockIconBadge}>
                <Ionicons name="lock-closed" size={24} color="#9E6D08" />
              </View>
              <ThemedText style={styles.passwordTitle}>Salesman Authorization</ThemedText>
              <ThemedText style={styles.passwordSubtitle}>Please enter your password to proceed</ThemedText>
            </View>

            <TextInput
              style={styles.passwordInput}
              secureTextEntry
              placeholder="Enter Salesman Password..."
              placeholderTextColor="#9E8D77"
              value={salesmanPassword}
              onChangeText={setSalesmanPassword}
              autoFocus
              onSubmitEditing={verifyPassword}
            />

            <View style={styles.passwordBtnRow}>
              <TouchableOpacity
                style={[styles.passwordBtn, styles.passwordCancelBtn]}
                onPress={() => router.back()}
                activeOpacity={0.7}
              >
                <ThemedText style={styles.passwordCancelText}>Cancel</ThemedText>
              </TouchableOpacity>
              <TouchableOpacity
                style={[styles.passwordBtn, styles.passwordVerifyBtn]}
                onPress={verifyPassword}
                activeOpacity={0.8}
                disabled={loading}
              >
                {loading ? (
                  <ActivityIndicator size="small" color="#FFFFFF" />
                ) : (
                  <ThemedText style={styles.passwordVerifyText}>Verify Access</ThemedText>
                )}
              </TouchableOpacity>
            </View>
          </View>
        </View>
      </Modal>

      {/* DATE PICKER COMPONENT (NATIVE) */}
      {showPicker.visible && (
        <DateTimePicker
          value={
            showPicker.mode === 'dueDate'
              ? new Date(dueDate.split('-').reverse().join('-')) || new Date()
              : showPicker.mode === 'orderDate'
                ? new Date(orderDate.split('-').reverse().join('-')) || new Date()
                : new Date(issueDate.split('-').reverse().join('-')) || new Date()
          }
          mode="date"
          display="default"
          onChange={(event, selectedDate) => {
            if (Platform.OS === 'android' || event.type === 'dismissed') {
              setShowPicker({ ...showPicker, visible: false });
            }
            if (selectedDate) {
              if (showPicker.mode === 'dueDate') setDueDate(formatDate(selectedDate));
              if (showPicker.mode === 'orderDate') setOrderDate(formatDate(selectedDate));
              if (showPicker.mode === 'issueDate') setIssueDate(formatDate(selectedDate));
            }
          }}
        />
      )}

      {!isPasswordVisible && (
        <ScrollView contentContainerStyle={styles.scrollContainer} keyboardShouldPersistTaps="handled">
          {/* TOP CARD: HEADER INFORMATION & FINANCIAL TOTALS */}
          <View style={styles.goldCard}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="document-text-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitleSmall}>Header Details</ThemedText>
              </View>

              {/* Mode Toggle Pills */}
              <View style={styles.modeToggleGroup}>
                <TouchableOpacity
                  style={[styles.modePill, mode === 'Installment' && styles.modePillActive]}
                  onPress={() => {
                    if (mode !== 'Installment') {
                      setMode('Installment');
                      clearForm();
                    }
                  }}
                  activeOpacity={0.8}
                >
                  <Ionicons
                    name="calendar-outline"
                    size={13}
                    color={mode === 'Installment' ? '#9E6D08' : '#6A5636'}
                    style={{ marginRight: 4 }}
                  />
                  <ThemedText style={[styles.modePillText, mode === 'Installment' && styles.modePillTextActive]}>
                    Installment
                  </ThemedText>
                </TouchableOpacity>

                <TouchableOpacity
                  style={[styles.modePill, mode === 'Order' && styles.modePillActive]}
                  onPress={() => {
                    if (mode !== 'Order') {
                      setMode('Order');
                      clearForm();
                    }
                  }}
                  activeOpacity={0.8}
                >
                  <Ionicons
                    name="basket-outline"
                    size={13}
                    color={mode === 'Order' ? '#9E6D08' : '#6A5636'}
                    style={{ marginRight: 4 }}
                  />
                  <ThemedText style={[styles.modePillText, mode === 'Order' && styles.modePillTextActive]}>
                    Order
                  </ThemedText>
                </TouchableOpacity>
              </View>
            </View>

            <View style={styles.cardBody}>
              <View style={styles.twoColumnGrid}>
                {/* Left Column: Customer & Transaction Identifiers */}
                <View style={styles.columnLeft}>
                  {/* Row 1: Invoice / Order No */}
                  <View style={styles.inlineFieldRow}>
                    <ThemedText style={styles.inlineFieldLabel}>
                      {mode === 'Installment' ? 'Invoice No *' : 'Order No *'}
                    </ThemedText>
                    <View style={styles.inputWithLookup}>
                      <TextInput
                        style={[styles.formInput, styles.inputAttached, { flex: 1 }]}
                        value={invoiceNo}
                        onChangeText={setInvoiceNo}
                        onBlur={() => fetchInvoiceInfo()}
                        placeholder={mode === 'Installment' ? 'Enter/Select Inv...' : 'Enter/Select Order...'}
                        placeholderTextColor="#9E8D77"
                      />
                      <TouchableOpacity
                        style={styles.attachedLookupBtn}
                        onPress={() => {
                          setLovType('invoice');
                          setLovVisible(true);
                        }}
                        activeOpacity={0.7}
                      >
                        <Ionicons name="search" size={13} color="#7D5E19" />
                      </TouchableOpacity>
                    </View>
                  </View>

                  {/* Row 2: Customer Code + New Customer + Diamond Tag */}
                  <View style={styles.inlineFieldRow}>
                    <ThemedText style={styles.inlineFieldLabel}>Customer Code *</ThemedText>
                    <View style={styles.customerInputCombo}>
                      <View style={[styles.inputWithLookup, { flex: 1 }]}>
                        <TextInput
                          style={[styles.formInput, styles.inputAttached, { flex: 1 }]}
                          value={customerCode}
                          onChangeText={setCustomerCode}
                          placeholder="Cust Code..."
                          placeholderTextColor="#9E8D77"
                        />
                        <TouchableOpacity
                          style={styles.attachedLookupBtn}
                          onPress={() => {
                            setLovType('customer');
                            setLovVisible(true);
                          }}
                          activeOpacity={0.7}
                        >
                          <Ionicons name="search" size={13} color="#7D5E19" />
                        </TouchableOpacity>
                      </View>

                      <TouchableOpacity
                        style={styles.newCustBtn}
                        onPress={() => router.push('/customer')}
                        activeOpacity={0.8}
                      >
                        <Ionicons name="person-add-outline" size={12} color="#7D5E19" style={{ marginRight: 3 }} />
                        <ThemedText style={styles.newCustBtnText}>New</ThemedText>
                      </TouchableOpacity>

                      <View style={styles.customerTypeBadge}>
                        <ThemedText style={styles.customerTypeBadgeText}>
                          {customerType === 'Customer Type' ? 'DIAMOND' : customerType}
                        </ThemedText>
                      </View>
                    </View>
                  </View>

                  {/* Row 3: Flexi ID */}
                  <View style={styles.inlineFieldRow}>
                    <ThemedText style={styles.inlineFieldLabel}>Flexi ID</ThemedText>
                    <TextInput
                      style={[styles.formInput, { flex: 1 }]}
                      value={flexiId}
                      onChangeText={setFlexiId}
                      placeholder="Optional Flexi ID..."
                      placeholderTextColor="#9E8D77"
                    />
                  </View>

                  {/* Row 4: Customer Name */}
                  <View style={styles.inlineFieldRow}>
                    <ThemedText style={styles.inlineFieldLabel}>Customer Name</ThemedText>
                    <TextInput
                      style={[styles.formInput, { flex: 1 }]}
                      value={customerName}
                      onChangeText={setCustomerName}
                      placeholder="Customer Name..."
                      placeholderTextColor="#9E8D77"
                    />
                  </View>

                  {/* Row 5: Address Lines */}
                  <View style={styles.inlineFieldRow}>
                    <ThemedText style={styles.inlineFieldLabel}>Address Line 1</ThemedText>
                    <TextInput
                      style={[styles.formInput, { flex: 1 }]}
                      value={address1}
                      onChangeText={setAddress1}
                      placeholder="Street / Unit No..."
                      placeholderTextColor="#9E8D77"
                    />
                  </View>
                  <View style={styles.inlineFieldRow}>
                    <ThemedText style={styles.inlineFieldLabel}>Address Line 2/3</ThemedText>
                    <View style={{ flex: 1, flexDirection: 'row', gap: 6 }}>
                      <TextInput
                        style={[styles.formInput, { flex: 1 }]}
                        value={address2}
                        onChangeText={setAddress2}
                        placeholder="Address 2..."
                        placeholderTextColor="#9E8D77"
                      />
                      <TextInput
                        style={[styles.formInput, { flex: 1 }]}
                        value={address3}
                        onChangeText={setAddress3}
                        placeholder="Address 3..."
                        placeholderTextColor="#9E8D77"
                      />
                    </View>
                  </View>

                  {/* Row 6: Remarks */}
                  <View style={styles.inlineFieldRow}>
                    <ThemedText style={styles.inlineFieldLabel}>Remarks</ThemedText>
                    <TextInput
                      style={[styles.formInput, { flex: 1 }]}
                      value={remarks}
                      onChangeText={setRemarks}
                      placeholder="Transaction remarks..."
                      placeholderTextColor="#9E8D77"
                    />
                  </View>

                  {/* Row 7: Payment Mode Pills & Trade IN in one row */}
                  <View style={[styles.inlineFieldRow, { flexWrap: 'wrap', gap: 12 }]}>
                    <View style={{ flexDirection: 'row', alignItems: 'center', gap: 6 }}>
                      <ThemedText style={styles.inlineFieldLabel}>Payment Mode</ThemedText>
                      <View style={styles.radioPillsGroup}>
                        {['Gold', 'Cash', 'Both'].map((pm) => (
                          <TouchableOpacity
                            key={pm}
                            style={[styles.pillOption, paymentMode === pm && styles.pillOptionSelected]}
                            onPress={() => setPaymentMode(pm as any)}
                            activeOpacity={0.7}
                          >
                            <Ionicons
                              name={paymentMode === pm ? 'radio-button-on' : 'radio-button-off'}
                              size={13}
                              color={paymentMode === pm ? '#9E6D08' : '#7A6855'}
                            />
                            <ThemedText
                              style={[styles.pillOptionText, paymentMode === pm && styles.pillOptionTextSelected]}
                            >
                              {pm}
                            </ThemedText>
                          </TouchableOpacity>
                        ))}
                      </View>
                    </View>

                    {/* Trade IN Amount */}
                    <View style={{ flexDirection: 'row', alignItems: 'center', gap: 6 }}>
                      <ThemedText style={[styles.inlineFieldLabel, { width: 'auto' }]}>Trade IN ($)</ThemedText>
                      <TextInput
                        style={[styles.formInput, { width: 100, textAlign: 'right' }]}
                        value={tradeIn}
                        onChangeText={setTradeIn}
                        keyboardType="numeric"
                        placeholder="0.00"
                        placeholderTextColor="#9E8D77"
                      />
                    </View>
                  </View>

                  {/* Order Mode Specific: GST Radios */}
                  {mode === 'Order' && (
                    <View style={styles.inlineFieldRow}>
                      <ThemedText style={styles.inlineFieldLabel}>GST Type</ThemedText>
                      <View style={styles.radioPillsGroup}>
                        {['Exclusive', 'Inclusive', 'ZeroRated'].map((gt) => (
                          <TouchableOpacity
                            key={gt}
                            style={[styles.pillOption, gstType === gt && styles.pillOptionSelected]}
                            onPress={() => {
                              setGstType(gt as any);
                              setEntryItem(calculateEntryTotals(entryItem, gt as any));
                            }}
                            activeOpacity={0.7}
                          >
                            <Ionicons
                              name={gstType === gt ? 'radio-button-on' : 'radio-button-off'}
                              size={13}
                              color={gstType === gt ? '#9E6D08' : '#7A6855'}
                            />
                            <ThemedText style={[styles.pillOptionText, gstType === gt && styles.pillOptionTextSelected]}>
                              {gt}
                            </ThemedText>
                          </TouchableOpacity>
                        ))}
                      </View>
                    </View>
                  )}
                </View>

                {/* Right Column: Financial Totals & Target Dates */}
                <View style={styles.columnRight}>
                  {mode === 'Installment' ? (
                    <>
                      <View style={styles.inlineFieldRow}>
                        <ThemedText style={styles.inlineFieldLabel}>Total Amount</ThemedText>
                        <TextInput
                          style={[styles.formInput, styles.readonlyInput, { flex: 1, textAlign: 'right', fontWeight: 'bold' }]}
                          value={totalAmount}
                          editable={false}
                        />
                      </View>

                      <View style={styles.inlineFieldRow}>
                        <ThemedText style={styles.inlineFieldLabel}>Deposit Amount *</ThemedText>
                        <TextInput
                          style={[styles.formInput, { flex: 1, textAlign: 'right' }]}
                          value={depositAmount}
                          onChangeText={setDepositAmount}
                          keyboardType="numeric"
                          placeholder="0.00"
                          placeholderTextColor="#9E8D77"
                        />
                      </View>

                      <View style={styles.inlineFieldRow}>
                        <ThemedText style={styles.inlineFieldLabel}>Amount Paid</ThemedText>
                        <TextInput
                          style={[styles.formInput, { flex: 1, textAlign: 'right' }]}
                          value={amountPaid}
                          onChangeText={setAmountPaid}
                          keyboardType="numeric"
                          placeholder="0.00"
                          placeholderTextColor="#9E8D77"
                        />
                      </View>

                      <View style={styles.inlineFieldRow}>
                        <ThemedText style={styles.inlineFieldLabel}>Installment Amt</ThemedText>
                        <TextInput
                          style={[
                            styles.formInput,
                            styles.readonlyInput,
                            styles.highlightTotal,
                            { flex: 1, textAlign: 'right' },
                          ]}
                          value={installmentAmt}
                          editable={false}
                        />
                      </View>

                      <View style={[styles.inlineFieldRow, { marginTop: 8 }]}>
                        <ThemedText style={styles.inlineFieldLabel}>Due Date</ThemedText>
                        {renderDatePickerInput(dueDate, setDueDate, 'dueDate')}
                      </View>
                    </>
                  ) : (
                    <>
                      <View style={styles.inlineFieldRow}>
                        <ThemedText style={styles.inlineFieldLabel}>Date of Delivery</ThemedText>
                        {renderDatePickerInput(dueDate, setDueDate, 'dueDate')}
                      </View>

                      <View style={styles.inlineFieldRow}>
                        <ThemedText style={styles.inlineFieldLabel}>Order Date</ThemedText>
                        {renderDatePickerInput(orderDate, setOrderDate, 'orderDate')}
                      </View>

                      {/* Today Rate vs DeliveryDate Rate */}
                      <View style={[styles.inlineFieldRow, { paddingVertical: 2 }]}>
                        <ThemedText style={styles.inlineFieldLabel}>Rate Type</ThemedText>
                        <View style={[styles.rateTypeContainer, { paddingVertical: 0, marginBottom: 0 }]}>
                          <TouchableOpacity
                            style={[styles.pillOption, rateFixed === 'Y' && styles.pillOptionSelected]}
                            onPress={() => setRateFixed('Y')}
                            activeOpacity={0.7}
                          >
                            <Ionicons
                              name={rateFixed === 'Y' ? 'radio-button-on' : 'radio-button-off'}
                              size={13}
                              color={rateFixed === 'Y' ? '#9E6D08' : '#7A6855'}
                            />
                            <ThemedText
                              style={[styles.pillOptionText, rateFixed === 'Y' && styles.pillOptionTextSelected]}
                            >
                              Today Rate
                            </ThemedText>
                          </TouchableOpacity>

                          <TouchableOpacity
                            style={[styles.pillOption, rateFixed === 'N' && styles.pillOptionSelected]}
                            onPress={() => setRateFixed('N')}
                            activeOpacity={0.7}
                          >
                            <Ionicons
                              name={rateFixed === 'N' ? 'radio-button-on' : 'radio-button-off'}
                              size={13}
                              color={rateFixed === 'N' ? '#9E6D08' : '#7A6855'}
                            />
                            <ThemedText
                              style={[styles.pillOptionText, rateFixed === 'N' && styles.pillOptionTextSelected]}
                            >
                              DeliveryDate Rate
                            </ThemedText>
                          </TouchableOpacity>
                        </View>
                      </View>

                      <View style={styles.inlineFieldRow}>
                        <ThemedText style={styles.inlineFieldLabel}>Estimated Weight</ThemedText>
                        <TextInput
                          style={[styles.formInput, { flex: 1, textAlign: 'right' }]}
                          value={estimatedWeight}
                          onChangeText={setEstimatedWeight}
                          keyboardType="numeric"
                          placeholder="0.00"
                          placeholderTextColor="#9E8D77"
                        />
                      </View>

                      <View style={styles.inlineFieldRow}>
                        <ThemedText style={styles.inlineFieldLabel}>Cost Per Gram</ThemedText>
                        <TextInput
                          style={[styles.formInput, { flex: 1, textAlign: 'right' }]}
                          value={costPerGram}
                          onChangeText={setCostPerGram}
                          keyboardType="numeric"
                          placeholder="0.00"
                          placeholderTextColor="#9E8D77"
                        />
                      </View>

                      <View style={styles.inlineFieldRow}>
                        <ThemedText style={styles.inlineFieldLabel}>Estimated GST</ThemedText>
                        <TextInput
                          style={[styles.formInput, { flex: 1, textAlign: 'right' }]}
                          value={estimatedGst}
                          onChangeText={setEstimatedGst}
                          keyboardType="numeric"
                          placeholder="0.00"
                          placeholderTextColor="#9E8D77"
                        />
                      </View>

                      <View style={styles.inlineFieldRow}>
                        <ThemedText style={styles.inlineFieldLabel}>Estimated Cost</ThemedText>
                        <TextInput
                          style={[styles.formInput, { flex: 1, textAlign: 'right' }]}
                          value={totalAmount}
                          onChangeText={setTotalAmount}
                          keyboardType="numeric"
                          placeholder="0.00"
                          placeholderTextColor="#9E8D77"
                        />
                      </View>

                      <View style={styles.inlineFieldRow}>
                        <ThemedText style={styles.inlineFieldLabel}>Advance Paid *</ThemedText>
                        <TextInput
                          style={[styles.formInput, { flex: 1, textAlign: 'right' }]}
                          value={depositAmount}
                          onChangeText={setDepositAmount}
                          keyboardType="numeric"
                          placeholder="0.00"
                          placeholderTextColor="#9E8D77"
                        />
                      </View>

                      <View style={styles.inlineFieldRow}>
                        <ThemedText style={styles.inlineFieldLabel}>Balance Amount</ThemedText>
                        <TextInput
                          style={[
                            styles.formInput,
                            styles.readonlyInput,
                            styles.highlightTotal,
                            { flex: 1, textAlign: 'right' },
                          ]}
                          value={installmentAmt}
                          editable={false}
                        />
                      </View>

                      <View style={styles.inlineFieldRow}>
                        <ThemedText style={styles.inlineFieldLabel}>Supplier Code</ThemedText>
                        <View style={[styles.inputWithLookup, { flex: 1 }]}>
                          <TextInput
                            style={[styles.formInput, styles.inputAttached, { flex: 1 }]}
                            value={supplierCode}
                            onChangeText={setSupplierCode}
                            placeholder="Supplier..."
                            placeholderTextColor="#9E8D77"
                          />
                          <TouchableOpacity
                            style={styles.attachedLookupBtn}
                            onPress={() => {
                              setLovType('supplier');
                              setLovVisible(true);
                            }}
                            activeOpacity={0.7}
                          >
                            <Ionicons name="search" size={13} color="#7D5E19" />
                          </TouchableOpacity>
                        </View>
                      </View>

                      <View style={[styles.inlineFieldRow, { marginTop: 4 }]}>
                        <ThemedText style={styles.inlineFieldLabel}>Issue Date</ThemedText>
                        {renderDatePickerInput(issueDate, setIssueDate, 'issueDate')}
                      </View>
                    </>
                  )}
                </View>
              </View>
            </View>
          </View>

          {/* MIDDLE CARD: LINE ITEMS DATA TABLE */}
          <View style={[styles.goldCard, { marginTop: 10 }]}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="list-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitleSmall}>Line Items</ThemedText>
                <View style={styles.counterBadge}>
                  <ThemedText style={styles.counterBadgeText}>{gridItems.length} items</ThemedText>
                </View>
              </View>

              {selectedRowIndex !== null && (
                <View style={styles.selectedRowIndicator}>
                  <ThemedText style={styles.selectedRowIndicatorText}>
                    Selected Row #{selectedRowIndex + 1}
                  </ThemedText>
                </View>
              )}
            </View>

            <ScrollView horizontal={false} style={{ width: '100%' }}>
              <View style={{ width: '100%' }}>
                {/* Table Header */}
                <View style={styles.tableHeadRow}>
                  {mode === 'Installment' ? (
                    <>
                      <ThemedText style={[styles.tableHeadCell, { flex: 1.5 }]}>Inventory Code</ThemedText>
                      <ThemedText style={[styles.tableHeadCell, { flex: 2.8 }]}>Description</ThemedText>
                      <ThemedText style={[styles.tableHeadCell, { flex: 1, textAlign: 'right' }]}>Weight</ThemedText>
                      <ThemedText style={[styles.tableHeadCell, { flex: 1, textAlign: 'right' }]}>GoldLoss</ThemedText>
                      <ThemedText style={[styles.tableHeadCell, { flex: 1.2, textAlign: 'right' }]}>Cost</ThemedText>
                      <ThemedText style={[styles.tableHeadCell, { flex: 0.7, textAlign: 'right' }]}>PCS</ThemedText>
                      <ThemedText style={[styles.tableHeadCell, { flex: 1, textAlign: 'right' }]}>SMC</ThemedText>
                      <ThemedText style={[styles.tableHeadCell, { flex: 1, textAlign: 'right' }]}>GST</ThemedText>
                      <ThemedText style={[styles.tableHeadCell, { flex: 1.5, textAlign: 'right' }]}>
                        Total Value ($)
                      </ThemedText>
                    </>
                  ) : (
                    <>
                      <ThemedText style={[styles.tableHeadCell, { flex: 0.8 }]}>Class</ThemedText>
                      <ThemedText style={[styles.tableHeadCell, { flex: 0.9 }]}>Category</ThemedText>
                      <ThemedText style={[styles.tableHeadCell, { flex: 0.9 }]}>Catalogue</ThemedText>
                      <ThemedText style={[styles.tableHeadCell, { flex: 1.8 }]}>Description</ThemedText>
                      <ThemedText style={[styles.tableHeadCell, { flex: 1.4 }]}>Stone Desc</ThemedText>
                      <ThemedText style={[styles.tableHeadCell, { flex: 0.8, textAlign: 'right' }]}>Weight</ThemedText>
                      <ThemedText style={[styles.tableHeadCell, { flex: 0.8, textAlign: 'right' }]}>St.Wt</ThemedText>
                      <ThemedText style={[styles.tableHeadCell, { flex: 0.8, textAlign: 'right' }]}>GoldLoss</ThemedText>
                      <ThemedText style={[styles.tableHeadCell, { flex: 0.9, textAlign: 'right' }]}>Cost/Gm</ThemedText>
                      <ThemedText style={[styles.tableHeadCell, { flex: 0.7, textAlign: 'right' }]}>MC</ThemedText>
                      <ThemedText style={[styles.tableHeadCell, { flex: 1.0, textAlign: 'right' }]}>Total Cost</ThemedText>
                      <ThemedText style={[styles.tableHeadCell, { flex: 0.7, textAlign: 'right' }]}>GST</ThemedText>
                      <ThemedText style={[styles.tableHeadCell, { flex: 0.9, textAlign: 'right' }]}>Stone Cost</ThemedText>
                      <ThemedText style={[styles.tableHeadCell, { flex: 0.7 }]}>Size</ThemedText>
                    </>
                  )}
                </View>

                {/* Table Body */}
                {gridItems.length === 0 ? (
                  <View style={styles.emptyTableContainer}>
                    <Ionicons name="cube-outline" size={28} color="#D4C3A3" />
                    <ThemedText style={styles.emptyTableText}>
                      No items entered. Add line items using the form below.
                    </ThemedText>
                  </View>
                ) : (
                  gridItems.map((item, index) => {
                    const isSelected = selectedRowIndex === index;
                    return (
                      <TouchableOpacity
                        key={index}
                        style={[styles.tableRow, isSelected && styles.tableRowSelected]}
                        onPress={() => setSelectedRowIndex(isSelected ? null : index)}
                        activeOpacity={0.7}
                      >
                        {mode === 'Installment' ? (
                          <>
                            <ThemedText style={[styles.tableCell, styles.codeCell, { flex: 1.5 }]}>
                              {item.InventoryCode}
                            </ThemedText>
                            <ThemedText style={[styles.tableCell, { flex: 2.8 }]} numberOfLines={1}>
                              {item.Description}
                            </ThemedText>
                            <ThemedText style={[styles.tableCell, { flex: 1, textAlign: 'right' }]}>
                              {item.Weight}
                            </ThemedText>
                            <ThemedText style={[styles.tableCell, { flex: 1, textAlign: 'right' }]}>
                              {item.GoldLoss}
                            </ThemedText>
                            <ThemedText style={[styles.tableCell, { flex: 1.2, textAlign: 'right' }]}>
                              {item.Cost}
                            </ThemedText>
                            <ThemedText style={[styles.tableCell, { flex: 0.7, textAlign: 'right' }]}>
                              {item.PCS}
                            </ThemedText>
                            <ThemedText style={[styles.tableCell, { flex: 1, textAlign: 'right' }]}>
                              {item.SMC}
                            </ThemedText>
                            <ThemedText style={[styles.tableCell, { flex: 1, textAlign: 'right' }]}>
                              {item.GST}
                            </ThemedText>
                            <ThemedText
                              style={[styles.tableCell, styles.amountCell, { flex: 1.5, textAlign: 'right' }]}
                            >
                              {item.TotalValue}
                            </ThemedText>
                          </>
                        ) : (
                          <>
                            <ThemedText style={[styles.tableCell, styles.codeCell, { flex: 0.8 }]}>
                              {item.ClassCode}
                            </ThemedText>
                            <ThemedText style={[styles.tableCell, { flex: 0.9 }]}>{item.CategoryCode}</ThemedText>
                            <ThemedText style={[styles.tableCell, { flex: 0.9 }]}>{item.CatalogueCode}</ThemedText>
                            <ThemedText style={[styles.tableCell, { flex: 1.8 }]} numberOfLines={1}>
                              {item.Description}
                            </ThemedText>
                            <ThemedText style={[styles.tableCell, { flex: 1.4 }]} numberOfLines={1}>
                              {item.StoneDescription}
                            </ThemedText>
                            <ThemedText style={[styles.tableCell, { flex: 0.8, textAlign: 'right' }]}>
                              {item.Weight}
                            </ThemedText>
                            <ThemedText style={[styles.tableCell, { flex: 0.8, textAlign: 'right' }]}>
                              {item.StoneWeight}
                            </ThemedText>
                            <ThemedText style={[styles.tableCell, { flex: 0.8, textAlign: 'right' }]}>
                              {item.GoldLoss}
                            </ThemedText>
                            <ThemedText style={[styles.tableCell, { flex: 0.9, textAlign: 'right' }]}>
                              {item.Cost}
                            </ThemedText>
                            <ThemedText style={[styles.tableCell, { flex: 0.7, textAlign: 'right' }]}>
                              {item.SMC}
                            </ThemedText>
                            <ThemedText
                              style={[styles.tableCell, styles.amountCell, { flex: 1.0, textAlign: 'right' }]}
                            >
                              {item.TotalValue}
                            </ThemedText>
                            <ThemedText style={[styles.tableCell, { flex: 0.7, textAlign: 'right' }]}>
                              {item.GST}
                            </ThemedText>
                            <ThemedText style={[styles.tableCell, { flex: 0.9, textAlign: 'right' }]}>
                              {item.StoneCost}
                            </ThemedText>
                            <ThemedText style={[styles.tableCell, { flex: 0.7 }]}>{item.SizeCode}</ThemedText>
                          </>
                        )}
                      </TouchableOpacity>
                    );
                  })
                )}
              </View>
            </ScrollView>

            {/* BOTTOM ENTRY INPUT FIELDS */}
            <View style={styles.entrySection}>
              <View style={styles.entrySectionHeader}>
                <ThemedText style={styles.entrySectionTitle}>Add / Edit Item Details</ThemedText>
              </View>

              {mode === 'Installment' ? (
                <>
                  <View style={styles.entryGridTable}>
                    {/* Installment Row 1 Header */}
                    <View style={styles.entryGridHeaderRow}>
                      <ThemedText style={[styles.entryGridHeadCell, { flex: 1.2 }]}>Inv Code *</ThemedText>
                      <ThemedText style={[styles.entryGridHeadCell, { flex: 2.8 }]}>Description</ThemedText>
                      <ThemedText style={[styles.entryGridHeadCell, { flex: 1 }]}>Weight</ThemedText>
                      <ThemedText style={[styles.entryGridHeadCell, { flex: 1 }]}>Gold Loss</ThemedText>
                    </View>

                    {/* Installment Row 1 Inputs */}
                    <View style={styles.entryGridInputRow}>
                      {/* Inv Code */}
                      <View style={[styles.entryGridInputCell, { flex: 1.2 }]}>
                        <View style={styles.inputWithLookup}>
                          <TextInput
                            style={[styles.entryGridInput, styles.inputAttached, { flex: 1 }]}
                            value={entryItem.InventoryCode}
                            onChangeText={(v) => handleEntryChange('InventoryCode', v)}
                            onBlur={() => fetchInventoryDetails(entryItem.InventoryCode)}
                            placeholder="Code..."
                            placeholderTextColor="#9E8D77"
                          />
                          <TouchableOpacity
                            style={styles.entryGridLookupBtn}
                            onPress={() => {
                              setLovType('inventory');
                              setLovVisible(true);
                            }}
                            activeOpacity={0.7}
                          >
                            <Ionicons name="search" size={13} color="#7D5E19" />
                          </TouchableOpacity>
                        </View>
                      </View>

                      {/* Description */}
                      <View style={[styles.entryGridInputCell, { flex: 2.8 }]}>
                        <TextInput
                          style={styles.entryGridInput}
                          value={entryItem.Description}
                          onChangeText={(v) => handleEntryChange('Description', v)}
                          placeholder="Description..."
                          placeholderTextColor="#9E8D77"
                        />
                      </View>

                      {/* Weight */}
                      <View style={[styles.entryGridInputCell, { flex: 1 }]}>
                        <TextInput
                          style={[styles.entryGridInput, { textAlign: 'right' }]}
                          value={entryItem.Weight}
                          onChangeText={(v) => handleEntryChange('Weight', v)}
                          keyboardType="numeric"
                          placeholder="0.00"
                          placeholderTextColor="#9E8D77"
                        />
                      </View>

                      {/* Gold Loss */}
                      <View style={[styles.entryGridInputCell, { flex: 1 }]}>
                        <TextInput
                          style={[styles.entryGridInput, { textAlign: 'right' }]}
                          value={entryItem.GoldLoss}
                          onChangeText={(v) => handleEntryChange('GoldLoss', v)}
                          keyboardType="numeric"
                          placeholder="0.00"
                          placeholderTextColor="#9E8D77"
                        />
                      </View>
                    </View>

                    {/* Installment Row 2 Header */}
                    <View style={[styles.entryGridHeaderRow, { borderTopWidth: 1, borderTopColor: '#D4C3A3' }]}>
                      <ThemedText style={[styles.entryGridHeadCell, { flex: 1 }]}>Cost ($)</ThemedText>
                      <ThemedText style={[styles.entryGridHeadCell, { flex: 0.8 }]}>PCS</ThemedText>
                      <ThemedText style={[styles.entryGridHeadCell, { flex: 1 }]}>SMC</ThemedText>
                      <ThemedText style={[styles.entryGridHeadCell, { flex: 1 }]}>GST</ThemedText>
                      <ThemedText style={[styles.entryGridHeadCell, { flex: 1.2 }]}>Total Value</ThemedText>
                    </View>

                    {/* Installment Row 2 Inputs */}
                    <View style={styles.entryGridInputRow}>
                      {/* Cost */}
                      <View style={[styles.entryGridInputCell, { flex: 1 }]}>
                        <TextInput
                          style={[styles.entryGridInput, { textAlign: 'right' }]}
                          value={entryItem.Cost}
                          onChangeText={(v) => handleEntryChange('Cost', v)}
                          keyboardType="numeric"
                          placeholder="0.00"
                          placeholderTextColor="#9E8D77"
                        />
                      </View>

                      {/* PCS */}
                      <View style={[styles.entryGridInputCell, { flex: 0.8 }]}>
                        <TextInput
                          style={[styles.entryGridInput, { textAlign: 'right' }]}
                          value={entryItem.PCS}
                          onChangeText={(v) => handleEntryChange('PCS', v)}
                          keyboardType="numeric"
                          placeholder="1"
                          placeholderTextColor="#9E8D77"
                        />
                      </View>

                      {/* SMC */}
                      <View style={[styles.entryGridInputCell, { flex: 1 }]}>
                        <TextInput
                          style={[styles.entryGridInput, { textAlign: 'right' }]}
                          value={entryItem.SMC}
                          onChangeText={(v) => handleEntryChange('SMC', v)}
                          keyboardType="numeric"
                          placeholder="0.00"
                          placeholderTextColor="#9E8D77"
                        />
                      </View>

                      {/* GST */}
                      <View style={[styles.entryGridInputCell, { flex: 1 }]}>
                        <TextInput
                          style={[styles.entryGridInput, styles.readonlyInput, { textAlign: 'right' }]}
                          value={entryItem.GST}
                          editable={false}
                        />
                      </View>

                      {/* Total Value */}
                      <View style={[styles.entryGridInputCell, { flex: 1.2 }]}>
                        <TextInput
                          style={[
                            styles.entryGridInput,
                            styles.readonlyInput,
                            styles.highlightTotal,
                            { textAlign: 'right' },
                          ]}
                          value={entryItem.TotalValue}
                          editable={false}
                        />
                      </View>
                    </View>
                  </View>
                </>
              ) : (
                <>
                  {/* Order Mode Structured Grid (Row 1 & Row 2 table style) */}
                  <View style={styles.entryGridTable}>
                    {/* Row 1 Header */}
                    <View style={styles.entryGridHeaderRow}>
                      <ThemedText style={[styles.entryGridHeadCell, { flex: 1.1 }]}>Class Code</ThemedText>
                      <ThemedText style={[styles.entryGridHeadCell, { flex: 1.1 }]}>Category Code</ThemedText>
                      <ThemedText style={[styles.entryGridHeadCell, { flex: 1.1 }]}>Catalogue Code</ThemedText>
                      <ThemedText style={[styles.entryGridHeadCell, { flex: 2.2 }]}>Description</ThemedText>
                      <ThemedText style={[styles.entryGridHeadCell, { flex: 1.0 }]}>Weight</ThemedText>
                      <ThemedText style={[styles.entryGridHeadCell, { flex: 1.1 }]}>Cost/Gm</ThemedText>
                      <ThemedText style={[styles.entryGridHeadCell, { flex: 1.0 }]}>MC</ThemedText>
                      <ThemedText style={[styles.entryGridHeadCell, { flex: 1.0 }]}>GoldLoss</ThemedText>
                      <ThemedText style={[styles.entryGridHeadCell, { flex: 1.0, borderRightWidth: 0 }]}>GST</ThemedText>
                    </View>

                    {/* Row 1 Inputs */}
                    <View style={styles.entryGridInputRow}>
                      {/* Class Code */}
                      <View style={[styles.entryGridInputCell, { flex: 1.1 }]}>
                        <View style={styles.inputWithLookup}>
                          <TextInput
                            style={[styles.entryGridInput, styles.inputAttached, { flex: 1 }]}
                            value={entryItem.ClassCode}
                            onChangeText={(v) => handleEntryChange('ClassCode', v)}
                            placeholder="Class..."
                            placeholderTextColor="#9E8D77"
                          />
                          <TouchableOpacity
                            style={styles.entryGridLookupBtn}
                            onPress={() => {
                              setLovType('class');
                              setLovVisible(true);
                            }}
                            activeOpacity={0.7}
                          >
                            <Ionicons name="search" size={13} color="#7D5E19" />
                          </TouchableOpacity>
                        </View>
                      </View>

                      {/* Category Code */}
                      <View style={[styles.entryGridInputCell, { flex: 1.1 }]}>
                        <View style={styles.inputWithLookup}>
                          <TextInput
                            style={[styles.entryGridInput, styles.inputAttached, { flex: 1 }]}
                            value={entryItem.CategoryCode}
                            onChangeText={(v) => handleEntryChange('CategoryCode', v)}
                            placeholder="Cat..."
                            placeholderTextColor="#9E8D77"
                          />
                          <TouchableOpacity
                            style={styles.entryGridLookupBtn}
                            onPress={() => {
                              setLovType('category');
                              setLovVisible(true);
                            }}
                            activeOpacity={0.7}
                          >
                            <Ionicons name="search" size={13} color="#7D5E19" />
                          </TouchableOpacity>
                        </View>
                      </View>

                      {/* Catalogue Code */}
                      <View style={[styles.entryGridInputCell, { flex: 1.1 }]}>
                        <View style={styles.inputWithLookup}>
                          <TextInput
                            style={[styles.entryGridInput, styles.inputAttached, { flex: 1 }]}
                            value={entryItem.CatalogueCode}
                            onChangeText={(v) => handleEntryChange('CatalogueCode', v)}
                            placeholder="Catlog..."
                            placeholderTextColor="#9E8D77"
                          />
                          <TouchableOpacity
                            style={styles.entryGridLookupBtn}
                            onPress={() => {
                              setLovType('catalogue');
                              setLovVisible(true);
                            }}
                            activeOpacity={0.7}
                          >
                            <Ionicons name="search" size={13} color="#7D5E19" />
                          </TouchableOpacity>
                        </View>
                      </View>

                      {/* Description */}
                      <View style={[styles.entryGridInputCell, { flex: 2.2 }]}>
                        <TextInput
                          style={styles.entryGridInput}
                          value={entryItem.Description}
                          onChangeText={(v) => handleEntryChange('Description', v)}
                          placeholder="Description..."
                          placeholderTextColor="#9E8D77"
                        />
                      </View>

                      {/* Weight */}
                      <View style={[styles.entryGridInputCell, { flex: 1.0 }]}>
                        <TextInput
                          style={[styles.entryGridInput, { textAlign: 'right' }]}
                          value={entryItem.Weight}
                          onChangeText={(v) => handleEntryChange('Weight', v)}
                          keyboardType="numeric"
                          placeholder="0.00"
                          placeholderTextColor="#9E8D77"
                        />
                      </View>

                      {/* Cost/Gm */}
                      <View style={[styles.entryGridInputCell, { flex: 1.1 }]}>
                        <TextInput
                          style={[styles.entryGridInput, { textAlign: 'right' }]}
                          value={entryItem.Cost}
                          onChangeText={(v) => handleEntryChange('Cost', v)}
                          keyboardType="numeric"
                          placeholder="0.00"
                          placeholderTextColor="#9E8D77"
                        />
                      </View>

                      {/* MC */}
                      <View style={[styles.entryGridInputCell, { flex: 1.0 }]}>
                        <TextInput
                          style={[styles.entryGridInput, { textAlign: 'right' }]}
                          value={entryItem.SMC}
                          onChangeText={(v) => handleEntryChange('SMC', v)}
                          keyboardType="numeric"
                          placeholder="0.00"
                          placeholderTextColor="#9E8D77"
                        />
                      </View>

                      {/* Gold Loss */}
                      <View style={[styles.entryGridInputCell, { flex: 1.0 }]}>
                        <TextInput
                          style={[styles.entryGridInput, { textAlign: 'right' }]}
                          value={entryItem.GoldLoss}
                          onChangeText={(v) => handleEntryChange('GoldLoss', v)}
                          keyboardType="numeric"
                          placeholder="0.00"
                          placeholderTextColor="#9E8D77"
                        />
                      </View>

                      {/* GST */}
                      <View style={[styles.entryGridInputCell, { flex: 1.0, borderRightWidth: 0 }]}>
                        <TextInput
                          style={[styles.entryGridInput, styles.readonlyInput, { textAlign: 'right' }]}
                          value={entryItem.GST}
                          editable={false}
                        />
                      </View>
                    </View>

                    {/* Row 2: Stone Description, Size, Stone Wt, Stone Cost, Total Value */}
                    <View style={[styles.entryGridCombinedRow, { borderTopWidth: 1, borderTopColor: '#D4C3A3' }]}>
                      {/* Stone Description */}
                      <View style={[styles.entryGridHeaderCellWrap, { flex: 1.1 }]}>
                        <ThemedText style={styles.entryGridRow2Label}>Stone Description</ThemedText>
                      </View>
                      <View style={[styles.entryGridInputCell, { flex: 1.6 }]}>
                        <TextInput
                          style={styles.entryGridInput}
                          value={entryItem.StoneDescription}
                          onChangeText={(v) => handleEntryChange('StoneDescription', v)}
                          placeholder="Stone details..."
                          placeholderTextColor="#9E8D77"
                        />
                      </View>

                      {/* Size */}
                      <View style={[styles.entryGridHeaderCellWrap, { flex: 0.6 }]}>
                        <ThemedText style={styles.entryGridRow2Label}>Size</ThemedText>
                      </View>
                      <View style={[styles.entryGridInputCell, { flex: 1.2 }]}>
                        <View style={styles.inputWithLookup}>
                          <TextInput
                            style={[styles.entryGridInput, styles.inputAttached, { flex: 1 }]}
                            value={entryItem.SizeCode}
                            onChangeText={(v) => handleEntryChange('SizeCode', v)}
                            placeholder="Size..."
                            placeholderTextColor="#9E8D77"
                          />
                          <TouchableOpacity
                            style={styles.entryGridLookupBtn}
                            onPress={() => {
                              setLovType('size');
                              setLovVisible(true);
                            }}
                            activeOpacity={0.7}
                          >
                            <Ionicons name="search" size={13} color="#7D5E19" />
                          </TouchableOpacity>
                        </View>
                      </View>

                      {/* Stone Wt */}
                      <View style={[styles.entryGridHeaderCellWrap, { flex: 0.9 }]}>
                        <ThemedText style={styles.entryGridRow2Label}>Stone Wt</ThemedText>
                      </View>
                      <View style={[styles.entryGridInputCell, { flex: 1.1 }]}>
                        <TextInput
                          style={[styles.entryGridInput, { textAlign: 'right' }]}
                          value={entryItem.StoneWeight}
                          onChangeText={(v) => handleEntryChange('StoneWeight', v)}
                          keyboardType="numeric"
                          placeholder="0.00"
                          placeholderTextColor="#9E8D77"
                        />
                      </View>

                      {/* Stone Cost */}
                      <View style={[styles.entryGridHeaderCellWrap, { flex: 0.9 }]}>
                        <ThemedText style={styles.entryGridRow2Label}>Stone Cost</ThemedText>
                      </View>
                      <View style={[styles.entryGridInputCell, { flex: 1.1 }]}>
                        <TextInput
                          style={[styles.entryGridInput, { textAlign: 'right' }]}
                          value={entryItem.StoneCost}
                          onChangeText={(v) => handleEntryChange('StoneCost', v)}
                          keyboardType="numeric"
                          placeholder="0.00"
                          placeholderTextColor="#9E8D77"
                        />
                      </View>

                      {/* Total */}
                      <View style={[styles.entryGridHeaderCellWrap, { flex: 0.6 }]}>
                        <ThemedText style={[styles.entryGridRow2Label, { fontWeight: 'bold' }]}>Total</ThemedText>
                      </View>
                      <View style={[styles.entryGridInputCell, { flex: 1.0, borderRightWidth: 0 }]}>
                        <TextInput
                          style={[
                            styles.entryGridInput,
                            styles.readonlyInput,
                            styles.highlightTotal,
                            { textAlign: 'right' },
                          ]}
                          value={entryItem.TotalValue}
                          editable={false}
                        />
                      </View>
                    </View>
                  </View>
                </>
              )}

              {/* ACTION BUTTONS (ADD, DELETE, CLEAR) */}
              <View style={styles.gridBtnRow}>
                <TouchableOpacity style={[styles.tableActionBtn, styles.addBtn]} onPress={addToGrid} activeOpacity={0.8}>
                  <Ionicons name="add-circle-outline" size={14} color="#9E6D08" style={{ marginRight: 4 }} />
                  <ThemedText style={styles.addBtnText}>Add Item</ThemedText>
                </TouchableOpacity>

                <TouchableOpacity
                  style={[styles.tableActionBtn, styles.deleteBtn]}
                  onPress={removeFromGrid}
                  activeOpacity={0.8}
                >
                  <Ionicons name="trash-outline" size={14} color="#C62828" style={{ marginRight: 4 }} />
                  <ThemedText style={styles.deleteBtnText}>Delete Selected</ThemedText>
                </TouchableOpacity>

                <TouchableOpacity
                  style={[styles.tableActionBtn, styles.clearItemBtn]}
                  onPress={clearEntry}
                  activeOpacity={0.8}
                >
                  <Ionicons name="close-circle-outline" size={14} color="#5C4A28" style={{ marginRight: 4 }} />
                  <ThemedText style={styles.clearItemBtnText}>Clear Input</ThemedText>
                </TouchableOpacity>
              </View>
            </View>
          </View>
        </ScrollView>
      )}

      {/* LOV LOOKUP MODAL */}
      <Modal visible={lovVisible} transparent animationType="fade">
        <View style={styles.modalOverlay}>
          <View style={styles.lovModalCard}>
            <View style={styles.modalHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="search" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.modalHeaderTitle}>Select {lovType.toUpperCase()}</ThemedText>
              </View>
              <TouchableOpacity onPress={() => setLovVisible(false)} activeOpacity={0.7} style={styles.closeModalBtn}>
                <Ionicons name="close" size={18} color="#3B2D17" />
              </TouchableOpacity>
            </View>

            <View style={styles.modalSearchBox}>
              <Ionicons name="search-outline" size={16} color="#8D6409" style={{ marginRight: 6 }} />
              <TextInput
                style={styles.modalSearchInput}
                placeholder="Search records..."
                placeholderTextColor="#9E8D77"
                value={lovSearchQuery}
                onChangeText={setLovSearchQuery}
                autoFocus
              />
              {lovSearchQuery.length > 0 && (
                <TouchableOpacity onPress={() => setLovSearchQuery('')}>
                  <Ionicons name="close-circle" size={16} color="#9E8D77" />
                </TouchableOpacity>
              )}
            </View>

            {/* LOV Table Column Headers */}
            {lovType === 'invoice' && mode === 'Installment' && (
              <View style={styles.lovHeadRow}>
                <ThemedText style={[styles.lovHeadText, { flex: 2 }]}>Agreement No</ThemedText>
                <ThemedText style={[styles.lovHeadText, { flex: 1.2 }]}>Location</ThemedText>
                <ThemedText style={[styles.lovHeadText, { flex: 1.5 }]}>Cust Code</ThemedText>
                <ThemedText style={[styles.lovHeadText, { flex: 2 }]}>Cust Name</ThemedText>
              </View>
            )}
            {lovType === 'invoice' && mode === 'Order' && (
              <View style={styles.lovHeadRow}>
                <ThemedText style={[styles.lovHeadText, { flex: 2 }]}>Order No</ThemedText>
                <ThemedText style={[styles.lovHeadText, { flex: 1.5 }]}>Order Date</ThemedText>
                <ThemedText style={[styles.lovHeadText, { flex: 1.5 }]}>Cust Code</ThemedText>
                <ThemedText style={[styles.lovHeadText, { flex: 1.5 }]}>Supplier</ThemedText>
              </View>
            )}
            {lovType === 'customer' && (
              <View style={styles.lovHeadRow}>
                <ThemedText style={[styles.lovHeadText, { flex: 1.5 }]}>Customer Code</ThemedText>
                <ThemedText style={[styles.lovHeadText, { flex: 2 }]}>Customer Name</ThemedText>
                <ThemedText style={[styles.lovHeadText, { flex: 1.5 }]}>NRIC No</ThemedText>
                <ThemedText style={[styles.lovHeadText, { flex: 2 }]}>Address</ThemedText>
              </View>
            )}

            <FlatList
              data={lovData}
              keyExtractor={(_, index) => index.toString()}
              initialNumToRender={50}
              maxToRenderPerBatch={50}
              ListEmptyComponent={
                <View style={styles.emptyTableContainer}>
                  <Ionicons name="search-outline" size={24} color="#D4C3A3" />
                  <ThemedText style={styles.emptyTableText}>No matching records found.</ThemedText>
                </View>
              }
              renderItem={({ item }) => (
                <TouchableOpacity
                  style={styles.lovItemRow}
                  onPress={() => handleLovSelect(item)}
                  activeOpacity={0.7}
                >
                  {lovType === 'invoice' ? (
                    mode === 'Installment' ? (
                      <View style={{ flexDirection: 'row', width: '100%', alignItems: 'center' }}>
                        <ThemedText style={[styles.lovItemText, styles.codeCell, { flex: 2 }]}>
                          {item.Code}
                        </ThemedText>
                        <ThemedText style={[styles.lovItemText, { flex: 1.2 }]}>{item.LocationC}</ThemedText>
                        <ThemedText style={[styles.lovItemText, { flex: 1.5 }]}>{item.CustomerCode}</ThemedText>
                        <ThemedText style={[styles.lovItemText, { flex: 2 }]} numberOfLines={1}>
                          {item.Name}
                        </ThemedText>
                      </View>
                    ) : (
                      <View style={{ flexDirection: 'row', width: '100%', alignItems: 'center' }}>
                        <ThemedText style={[styles.lovItemText, styles.codeCell, { flex: 2 }]}>
                          {item.Code}
                        </ThemedText>
                        <ThemedText style={[styles.lovItemText, { flex: 1.5 }]}>{item.OrderDate}</ThemedText>
                        <ThemedText style={[styles.lovItemText, { flex: 1.5 }]}>{item.CustomerCode}</ThemedText>
                        <ThemedText style={[styles.lovItemText, { flex: 1.5 }]}>{item.SupplierCode}</ThemedText>
                      </View>
                    )
                  ) : lovType === 'customer' ? (
                    <View style={{ flexDirection: 'row', width: '100%', alignItems: 'center' }}>
                      <ThemedText style={[styles.lovItemText, styles.codeCell, { flex: 1.5 }]}>
                        {item.Code}
                      </ThemedText>
                      <ThemedText style={[styles.lovItemText, { flex: 2 }]} numberOfLines={1}>
                        {item.Name}
                      </ThemedText>
                      <ThemedText style={[styles.lovItemText, { flex: 1.5 }]}>{item.NricNo}</ThemedText>
                      <ThemedText style={[styles.lovItemText, { flex: 2 }]} numberOfLines={1}>
                        {item.Address1}
                      </ThemedText>
                    </View>
                  ) : (
                    <View style={{ flexDirection: 'row', width: '100%', alignItems: 'center' }}>
                      <ThemedText style={[styles.lovItemText, styles.codeCell, { width: 100 }]}>
                        {item.Code}
                      </ThemedText>
                      <ThemedText style={[styles.lovItemText, { flex: 1 }]} numberOfLines={1}>
                        {item.Name}
                      </ThemedText>
                    </View>
                  )}
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
  printBtn: {
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

  // ===== MODE TOGGLE PILLS =====
  modeToggleGroup: {
    flexDirection: 'row',
    backgroundColor: '#FFFFFF',
    padding: 2,
    borderRadius: 6,
    borderWidth: 1,
    borderColor: '#D4C3A3',
    gap: 2,
  },
  modePill: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingVertical: 3,
    paddingHorizontal: 8,
    borderRadius: 4,
  },
  modePillActive: {
    backgroundColor: '#FFF4DA',
    borderWidth: 1,
    borderColor: '#B8903B',
  },
  modePillText: {
    fontSize: 11,
    fontWeight: '600',
    color: '#6A5636',
  },
  modePillTextActive: {
    color: '#9E6D08',
    fontWeight: 'bold',
  },

  // ===== TWO COLUMN GRID =====
  twoColumnGrid: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    gap: 16,
  },
  columnLeft: {
    flex: 1.2,
    minWidth: 320,
  },
  columnRight: {
    flex: 1,
    minWidth: 300,
  },

  // ===== INLINE FIELD ROW =====
  inlineFieldRow: {
    flexDirection: 'row',
    alignItems: 'center',
    marginBottom: 5,
  },
  inlineFieldLabel: {
    width: 125,
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3B2D17',
    marginRight: 6,
    textAlign: 'left',
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

  // Customer combo elements
  customerInputCombo: {
    flex: 1,
    flexDirection: 'row',
    alignItems: 'center',
    gap: 4,
  },
  newCustBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    height: 26,
    backgroundColor: '#FFF1D6',
    borderWidth: 1,
    borderColor: '#D4C3A3',
    borderRadius: 4,
    paddingHorizontal: 6,
    justifyContent: 'center',
  },
  newCustBtnText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#7D5E19',
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

  // Pill Options / Radios
  radioPillsGroup: {
    flex: 1,
    flexDirection: 'row',
    alignItems: 'center',
    gap: 6,
    flexWrap: 'wrap',
  },
  pillOption: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FCFAF6',
    borderWidth: 1,
    borderColor: '#E2D3B3',
    borderRadius: 4,
    paddingVertical: 3,
    paddingHorizontal: 6,
    gap: 4,
  },
  pillOptionSelected: {
    backgroundColor: '#FFF4DA',
    borderColor: '#B8903B',
  },
  pillOptionText: {
    fontSize: 11,
    fontWeight: '500',
    color: '#5C4A28',
  },
  pillOptionTextSelected: {
    color: '#9E6D08',
    fontWeight: 'bold',
  },

  rateTypeContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 6,
    paddingVertical: 4,
    marginBottom: 5,
  },

  // Date picker wrapper
  datePickerInputWrap: {
    flex: 1,
    flexDirection: 'row',
    alignItems: 'center',
    height: 26,
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#D4C3A3',
    borderRadius: 4,
    paddingHorizontal: 4,
  },
  calendarIconBtn: {
    padding: 3,
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
  selectedRowIndicator: {
    backgroundColor: '#FFF3D4',
    paddingHorizontal: 8,
    paddingVertical: 2,
    borderRadius: 4,
    borderWidth: 1,
    borderColor: '#B8903B',
  },
  selectedRowIndicatorText: {
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
    paddingHorizontal: 6,
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
    paddingHorizontal: 6,
    backgroundColor: '#FFFFFF',
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
    fontFamily: Platform.OS === 'ios' ? 'Courier' : 'monospace',
    fontWeight: 'bold',
    color: '#8A5D00',
  },
  amountCell: {
    fontWeight: 'bold',
    color: '#1B5E20',
  },
  emptyTableContainer: {
    paddingVertical: 20,
    alignItems: 'center',
    justifyContent: 'center',
  },
  emptyTableText: {
    fontSize: 12,
    color: '#9E8D77',
    marginTop: 4,
  },

  // ===== BOTTOM ENTRY SECTION =====
  entrySection: {
    borderTopWidth: 1,
    borderTopColor: '#E8DCB8',
    backgroundColor: '#FCFAF6',
    padding: 10,
  },
  entrySectionHeader: {
    marginBottom: 6,
  },
  entrySectionTitle: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#8D6409',
    textTransform: 'uppercase',
    letterSpacing: 0.4,
  },
  entryInputsRow: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    gap: 6,
    alignItems: 'center',
  },
  inlineEntryField: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 4,
  },
  entryLabel: {
    fontSize: 10,
    fontWeight: 'bold',
    color: '#5C4A28',
    flexShrink: 0,
  },

  // Structured Entry Grid Table
  entryGridTable: {
    borderWidth: 1,
    borderColor: '#D4C3A3',
    backgroundColor: '#FFFFFF',
    borderRadius: 4,
    overflow: 'hidden',
  },
  entryGridHeaderRow: {
    flexDirection: 'row',
    backgroundColor: '#F5E7CA',
    borderBottomWidth: 1,
    borderBottomColor: '#D4C3A3',
    minHeight: 28,
    alignItems: 'center',
  },
  entryGridHeadCell: {
    paddingVertical: 4,
    paddingHorizontal: 6,
    fontSize: 11,
    fontWeight: 'bold',
    color: '#0A2540',
    borderRightWidth: 1,
    borderRightColor: '#D4C3A3',
    textAlign: 'center',
    textAlignVertical: 'center',
  },
  entryGridInputRow: {
    flexDirection: 'row',
    backgroundColor: '#FFFFFF',
    height: 32,
    alignItems: 'stretch',
  },
  entryGridInputCell: {
    borderRightWidth: 1,
    borderRightColor: '#D4C3A3',
    justifyContent: 'center',
  },
  entryGridInput: {
    height: '100%',
    backgroundColor: '#FFFFFF',
    paddingHorizontal: 8,
    paddingVertical: 0,
    fontSize: 12,
    color: '#2B2118',
    width: '100%',
    borderWidth: 0,
    textAlignVertical: 'center',
  },
  entryGridLookupBtn: {
    height: '100%',
    width: 26,
    backgroundColor: '#F5E7CA',
    justifyContent: 'center',
    alignItems: 'center',
    borderLeftWidth: 1,
    borderLeftColor: '#D4C3A3',
  },
  entryGridCombinedRow: {
    flexDirection: 'row',
    alignItems: 'stretch',
    height: 32,
    backgroundColor: '#FFFFFF',
  },
  entryGridHeaderCellWrap: {
    backgroundColor: '#F5E7CA',
    justifyContent: 'center',
    alignItems: 'center',
    borderRightWidth: 1,
    borderRightColor: '#D4C3A3',
    paddingHorizontal: 6,
  },
  entryGridRow2Label: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#0A2540',
    textAlign: 'center',
  },

  // Grid Action Buttons
  gridBtnRow: {
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
  passwordModalCard: {
    width: 320,
    backgroundColor: '#FFFFFF',
    borderRadius: 10,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    padding: 20,
    alignItems: 'center',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.15,
    shadowRadius: 8,
    elevation: 8,
  },
  passwordHeader: {
    alignItems: 'center',
    marginBottom: 16,
  },
  lockIconBadge: {
    width: 44,
    height: 44,
    borderRadius: 22,
    backgroundColor: '#F5E7CA',
    justifyContent: 'center',
    alignItems: 'center',
    marginBottom: 8,
    borderWidth: 1,
    borderColor: '#E2D3B3',
  },
  passwordTitle: {
    fontSize: 15,
    fontWeight: 'bold',
    color: '#3B2D17',
  },
  passwordSubtitle: {
    fontSize: 11,
    color: '#8D6409',
    marginTop: 2,
  },
  passwordInput: {
    width: '100%',
    height: 38,
    backgroundColor: '#FCFAF6',
    borderWidth: 1,
    borderColor: '#D4C3A3',
    borderRadius: 6,
    paddingHorizontal: 12,
    fontSize: 14,
    color: '#2B2118',
    marginBottom: 16,
  },
  passwordBtnRow: {
    flexDirection: 'row',
    width: '100%',
    gap: 8,
  },
  passwordBtn: {
    flex: 1,
    height: 34,
    borderRadius: 6,
    justifyContent: 'center',
    alignItems: 'center',
  },
  passwordCancelBtn: {
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#DFD5C3',
  },
  passwordCancelText: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#5C4A28',
  },
  passwordVerifyBtn: {
    backgroundColor: '#B8903B',
  },
  passwordVerifyText: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#FFFFFF',
  },

  // LOV Modal Card
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
    paddingVertical: 8,
    paddingHorizontal: 10,
    borderBottomWidth: 1,
    borderBottomColor: '#F3EBDD',
  },
  lovItemText: {
    fontSize: 11,
    color: '#3B2D17',
  },

  // Toast
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
