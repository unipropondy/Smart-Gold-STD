import { ThemedText } from '@/components/themed-text';
import { API_BASE_URL } from '@/constants/api';
import { Ionicons } from '@expo/vector-icons';
import { Stack, useRouter } from 'expo-router';
import React, { useMemo, useState } from 'react';
import {
  ActivityIndicator,
  FlatList,
  Modal,
  Platform,
  ScrollView,
  StyleSheet,
  TextInput,
  TouchableOpacity,
  TouchableWithoutFeedback,
  View
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';

export default function SalesReturnScreen() {
  const router = useRouter();

  const [loading, setLoading] = useState(false);
  const [saving, setSaving] = useState(false);

  // Bill & Header State
  const [billNo, setBillNo] = useState('');
  const [salesMan, setSalesMan] = useState('');
  const [billDate, setBillDate] = useState({ DD: '', MM: '', YYYY: '' });
  const [returnDate, setReturnDate] = useState({
    DD: String(new Date().getDate()).padStart(2, '0'),
    MM: String(new Date().getMonth() + 1).padStart(2, '0'),
    YYYY: String(new Date().getFullYear())
  });
  const [taxType, setTaxType] = useState<'Zero Rated' | 'Tax Inclusive' | 'Tax Exclusive'>('Zero Rated');
  const [gstRate, setGstRate] = useState(0);

  // Customer State
  const [customerCode, setCustomerCode] = useState('');
  const [memberCode, setMemberCode] = useState('');
  const [customerName, setCustomerName] = useState('');
  const [address1, setAddress1] = useState('');
  const [address2, setAddress2] = useState('');
  const [address3, setAddress3] = useState('');
  const [customerType, setCustomerType] = useState('Customer Type');
  const [remarks, setRemarks] = useState('');

  // Item Entry State
  const [entryItem, setEntryItem] = useState({
    InventoryCode: '', Description: '', Weight: '0.00', PCS: '0', ReturnAmount: '0.00',
    TotalCost: '0.00', NetAmount: '0.00', ActualSales: '0.00', GST: '0.00', DetailDiscount: '0.00', NetDiscount: '0.00',
    OriginalPCS: '0', OriginalWeight: '0.00', OriginalReturnAmount: '0.00', OriginalTotalCost: '0.00',
    OriginalNetAmount: '0.00', OriginalActualSales: '0.00', OriginalGST: '0.00', OriginalDetailDiscount: '0.00', OriginalNetDiscount: '0.00'
  });

  // Grid State
  const [returnItems, setReturnItems] = useState<any[]>([]);
  const [selectedRowIndex, setSelectedRowIndex] = useState<number | null>(null);

  // LOVs
  const [lovVisible, setLovVisible] = useState(false);
  const [lovType, setLovType] = useState<'customer' | 'inventory'>('customer');
  const [lovData, setLovData] = useState<any[]>([]);
  const [lovSearchQuery, setLovSearchQuery] = useState('');

  // Password Verification
  const [passwordModalVisible, setPasswordModalVisible] = useState(true);
  const [salesmanPassword, setSalesmanPassword] = useState('');
  const [verifyingPassword, setVerifyingPassword] = useState(false);

  // Toast State
  const [toast, setToast] = useState<{ message: string; type: 'success' | 'error' } | null>(null);

  const showToast = (title: string, message: string) => {
    setToast({ message, type: title === 'Success' ? 'success' : 'error' });
    setTimeout(() => setToast(null), 3500);
  };

  // Calculate Totals dynamically
  const totals = useMemo(() => {
    let totalQty = 0;
    let totalReturnAmt = 0;
    let totalCost = 0;

    returnItems.forEach(item => {
      totalQty += parseFloat(item.PCS) || 0;
      totalReturnAmt += parseFloat(item.ReturnAmount) || 0;
      totalCost += parseFloat(item.TotalCost) || 0;
    });

    let gstAmount = 0;
    let netAmount = totalReturnAmt;

    if (taxType === 'Tax Exclusive') {
      gstAmount = gstRate > 0 ? (totalReturnAmt * gstRate) / 100 : 0;
      netAmount = totalReturnAmt + gstAmount;
    } else if (taxType === 'Tax Inclusive') {
      gstAmount = gstRate > 0 ? (totalReturnAmt * gstRate) / (100 + gstRate) : 0;
      netAmount = totalReturnAmt;
    }

    return {
      qty: totalQty,
      amount: totalReturnAmt.toFixed(2),
      gst: gstAmount.toFixed(2),
      net: netAmount.toFixed(2),
      totalCost: totalCost.toFixed(2)
    };
  }, [returnItems, taxType, gstRate]);

  const clearForm = () => {
    setBillNo('');
    setSalesMan('');
    setBillDate({ DD: '', MM: '', YYYY: '' });
    setCustomerCode('');
    setMemberCode('');
    setCustomerName('');
    setAddress1('');
    setAddress2('');
    setAddress3('');
    setCustomerType('Customer Type');
    setRemarks('');
    setReturnItems([]);
    clearEntry();
  };

  const clearEntry = () => {
    setEntryItem({
      InventoryCode: '', Description: '', Weight: '0.00', PCS: '0', ReturnAmount: '0.00',
      TotalCost: '0.00', NetAmount: '0.00', ActualSales: '0.00', GST: '0.00', DetailDiscount: '0.00', NetDiscount: '0.00',
      OriginalPCS: '0', OriginalWeight: '0.00', OriginalReturnAmount: '0.00', OriginalTotalCost: '0.00',
      OriginalNetAmount: '0.00', OriginalActualSales: '0.00', OriginalGST: '0.00', OriginalDetailDiscount: '0.00', OriginalNetDiscount: '0.00'
    });
    setSelectedRowIndex(null);
  };

  // Fetch Bill Info
  const fetchBillInfo = async (overrideBillNo?: string) => {
    const targetBill = (overrideBillNo || billNo).trim();
    if (!targetBill) return;
    setLoading(true);
    setReturnItems([]);
    try {
      const res = await fetch(`${API_BASE_URL}/salesreturn/salesheader/${encodeURIComponent(targetBill)}`);
      const data = await res.json();
      if (data.success) {
        const { header, customer } = data;
        if (header.InvoiceDate) {
          const invoiceDate = new Date(header.InvoiceDate);
          const today = new Date();
          if (invoiceDate.toDateString() === today.toDateString()) {
            showToast('Error', "You cannot sales return the today's Bill. Please do the Bill Cancel!");
            setBillNo('');
            return;
          }
        }

        if (header.TradeInAmt && parseFloat(header.TradeInAmt) > 0) {
          if (Platform.OS === 'web') {
            window.alert("This Sales has TRADEIN Items");
          } else {
            showToast('Error', "This Sales has TRADEIN Items");
          }
        }

        setSalesMan(header.SalesManCode || '');
        if (header.GSTType === 'I') setTaxType('Tax Inclusive');
        else if (header.GSTType === 'E') setTaxType('Tax Exclusive');
        else setTaxType('Zero Rated');

        if (data.globalGst !== undefined && data.globalGst > 0) {
          setGstRate(data.globalGst);
        } else {
          const gstKey = Object.keys(header).find(k => k.toLowerCase() === 'gstper' || k.toLowerCase() === 'gstrate');
          setGstRate(parseFloat((gstKey ? header[gstKey] : '0') || '0'));
        }

        const bDateKey = Object.keys(header).find(k => k.toLowerCase() === 'bdate');
        const invoiceDateKey = Object.keys(header).find(k => k.toLowerCase() === 'invoicedate');
        const dateToParse = (bDateKey ? header[bDateKey] : '') || (invoiceDateKey ? header[invoiceDateKey] : '');
        
        if (dateToParse) {
          const d = new Date(dateToParse);
          if (!isNaN(d.getTime())) {
            setBillDate({
              DD: String(d.getDate()).padStart(2, '0'),
              MM: String(d.getMonth() + 1).padStart(2, '0'),
              YYYY: String(d.getFullYear())
            });
          } else {
            const strVal = String(dateToParse);
            const parts = strVal.match(/\d+/g);
            if (parts && parts.length >= 3) {
              let yyyy = parts[2], mm = parts[1], dd = parts[0];
              if (parts[0].length === 4) {
                yyyy = parts[0]; mm = parts[1]; dd = parts[2];
              } else if (parts[2].length === 2) {
                yyyy = "20" + parts[2];
              }
              setBillDate({ DD: dd.padStart(2, '0'), MM: mm.padStart(2, '0'), YYYY: yyyy });
            }
          }
        }

        setCustomerCode(header.CustomerCode || '');
        setMemberCode(header.MemberCode || '');
        if (customer) {
          setCustomerName(customer.CustomerName || customer.MemberName || '');
          setAddress1(customer.Address1 || '');
          setAddress2(customer.Address2 || '');
          setAddress3(customer.Address3 || '');
          setCustomerType(customer.CustomerType || customer.MemberType || 'Customer Type');
        }

        // Fetch Details
        const resDetail = await fetch(`${API_BASE_URL}/salesreturn/salesdetail/${encodeURIComponent(targetBill)}`);
        const dataDetail = await resDetail.json();
        if (dataDetail.success) {
          const validItems = dataDetail.data.filter((d: any) => {
            const returnAmt = parseFloat(d.TotalValue || '0') - parseFloat(d.NetDiscount || '0');
            const invCode = String(d.InventoryCode || d.Inventorycode || '').toUpperCase();
            return returnAmt >= 0 && !invCode.startsWith('TI-');
          });

          const items = validItems.map((d: any) => ({
            InventoryCode: d.InventoryCode || d.Inventorycode || '',
            Description: d.Description || '',
            Weight: d.Weight || '0',
            PCS: d.Pcs || d.PCS || '1',
            ReturnAmount: (parseFloat(d.TotalValue || '0') - parseFloat(d.NetDiscount || '0')).toFixed(2),
            TotalCost: d.TotalCost || '0',
            NetAmount: d.NetAmount || '0',
            ActualSales: d.ActualSales || '0',
            GST: d.Gst || '0',
            DetailDiscount: d.DetailDiscount || '0',
            NetDiscount: d.NetDiscount || '0',
            OriginalPCS: d.Pcs || d.PCS || '1',
            OriginalWeight: d.Weight || '0',
            OriginalReturnAmount: (parseFloat(d.TotalValue || '0') - parseFloat(d.NetDiscount || '0')).toFixed(2),
            OriginalTotalCost: d.TotalCost || '0',
            OriginalNetAmount: d.NetAmount || '0',
            OriginalActualSales: d.ActualSales || '0',
            OriginalGST: d.Gst || '0',
            OriginalDetailDiscount: d.DetailDiscount || '0',
            OriginalNetDiscount: d.NetDiscount || '0'
          }));

          setReturnItems(items);
        }
      } else {
        showToast('Error', data.message || 'Bill not found');
      }
    } catch (err) {
      console.error(err);
      showToast('Error', 'Failed to fetch bill details');
    } finally {
      setLoading(false);
    }
  };

  // Fetch Inventory by Code
  const fetchInventory = async (overrideCode?: string) => {
    const code = (overrideCode || entryItem.InventoryCode).trim();
    if (!code) return;
    setLoading(true);
    try {
      const res = await fetch(`${API_BASE_URL}/salesreturn/inventory/${encodeURIComponent(code)}`);
      const data = await res.json();
      if (data.success) {
        const inv = data.inv || data.inventory || {};
        const sales = data.sales || data.salesData || null;

        setEntryItem(prev => ({
          ...prev,
          InventoryCode: inv.InventoryCode || code,
          Description: inv.Description || '',
          Weight: inv.Weight?.toString() || '0',
          PCS: inv.PCS?.toString() || '1',
          ReturnAmount: sales ? (parseFloat(sales.TotalValue || '0') - parseFloat(sales.NetDiscount || '0')).toFixed(2) : '0.00',
          TotalCost: sales?.TotalCost?.toString() || inv.TotalCost?.toString() || '0',
          NetAmount: sales?.NetAmount?.toString() || '0',
          ActualSales: sales?.ActualSales?.toString() || '0',
          GST: sales?.Gst?.toString() || '0',
          DetailDiscount: sales?.DetailDiscount?.toString() || '0',
          NetDiscount: sales?.NetDiscount?.toString() || '0',
          OriginalPCS: inv.PCS?.toString() || '1',
          OriginalWeight: inv.Weight?.toString() || '0',
          OriginalReturnAmount: sales ? (parseFloat(sales.TotalValue || '0') - parseFloat(sales.NetDiscount || '0')).toFixed(2) : '0.00',
          OriginalTotalCost: sales?.TotalCost?.toString() || inv.TotalCost?.toString() || '0',
          OriginalNetAmount: sales?.NetAmount?.toString() || '0',
          OriginalActualSales: sales?.ActualSales?.toString() || '0',
          OriginalGST: sales?.Gst?.toString() || '0',
          OriginalDetailDiscount: sales?.DetailDiscount?.toString() || '0',
          OriginalNetDiscount: sales?.NetDiscount?.toString() || '0'
        }));

        if (sales && sales.InvoiceNo) {
          const trimmedInvoice = sales.InvoiceNo.trim();
          setBillNo(trimmedInvoice);
          fetchBillInfo(trimmedInvoice);
        } else {
          showToast('Error', "Please select the sold Inventory!");
        }
      } else {
        showToast('Info', 'Inventory not found');
        clearEntry();
      }
    } catch (err) {
      console.error(err);
      showToast('Error', 'Failed to fetch inventory');
    } finally {
      setLoading(false);
    }
  };

  const handleAdd = () => {
    if (!entryItem.InventoryCode || !entryItem.Description) {
      showToast('Validation', 'Please enter/search inventory details');
      return;
    }
    
    let updatedItem = { ...entryItem };
    if (parseFloat(entryItem.PCS) !== parseFloat(entryItem.OriginalPCS || '0') && parseFloat(entryItem.OriginalPCS || '0') > 0) {
      const ratio = parseFloat(entryItem.PCS) / parseFloat(entryItem.OriginalPCS);
      updatedItem.Weight = (parseFloat(entryItem.OriginalWeight || '0') * ratio).toFixed(3);
      updatedItem.ReturnAmount = (parseFloat(entryItem.OriginalReturnAmount || '0') * ratio).toFixed(2);
      updatedItem.TotalCost = (parseFloat(entryItem.OriginalTotalCost || '0') * ratio).toFixed(2);
      updatedItem.NetAmount = (parseFloat(entryItem.OriginalNetAmount || '0') * ratio).toFixed(2);
      updatedItem.ActualSales = (parseFloat(entryItem.OriginalActualSales || '0') * ratio).toFixed(2);
      updatedItem.GST = (parseFloat(entryItem.OriginalGST || '0') * ratio).toFixed(2);
      updatedItem.DetailDiscount = (parseFloat(entryItem.OriginalDetailDiscount || '0') * ratio).toFixed(2);
      updatedItem.NetDiscount = (parseFloat(entryItem.OriginalNetDiscount || '0') * ratio).toFixed(2);
    }

    const newItems = [...returnItems];
    if (selectedRowIndex !== null) {
      newItems[selectedRowIndex] = updatedItem;
    } else {
      newItems.push(updatedItem);
    }
    setReturnItems(newItems);
    clearEntry();
  };

  const handleDelete = () => {
    if (selectedRowIndex !== null) {
      const newItems = returnItems.filter((_, i) => i !== selectedRowIndex);
      setReturnItems(newItems);
      clearEntry();
    } else {
      showToast('Info', 'Select an item from the grid to delete');
    }
  };

  const handleSave = async () => {
    if (customerCode.toUpperCase() === 'CASH' && !memberCode) {
      showToast('Validation', 'MemberCode is NOT entered !');
      return;
    }
    if (!billNo || !billNo.trim()) {
      showToast('Validation', 'Bill No is missing');
      return;
    }
    if (returnItems.length === 0) {
      showToast('Validation', 'No items to return');
      return;
    }
    setSaving(true);
    try {
      const payload = {
        billNo,
        totalWeight: returnItems.reduce((acc, curr) => acc + parseFloat(curr.Weight), 0),
        returnQty: totals.qty,
        returnTotal: totals.amount,
        totalCost: totals.totalCost,
        gstAmount: totals.gst,
        netAmount: totals.net,
        salesmanCode: salesMan,
        customerCode,
        memberCode,
        customerName,
        userId: 'SYS',
        items: returnItems,
        locationCode: 'HQ'
      };

      const res = await fetch(`${API_BASE_URL}/salesreturn/save`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload)
      });
      const data = await res.json();
      if (data.success) {
        showToast('Success', `Sales Return generated: ${data.returnNo}`);
        clearForm();
      } else {
        showToast('Error', data.message || 'Save failed');
      }
    } catch (err) {
      console.error(err);
      showToast('Error', 'An error occurred while saving');
    } finally {
      setSaving(false);
    }
  };

  const openLov = async (type: 'customer' | 'inventory') => {
    setLovType(type);
    setLovData([]);
    setLovSearchQuery('');
    setLovVisible(true);
    setLoading(true);
    try {
      if (type === 'customer') {
        const res = await fetch(`${API_BASE_URL}/customer`);
        const data = await res.json();
        if (data.success) {
          setLovData(data.data);
        }
      } else if (type === 'inventory') {
        const res = await fetch(`${API_BASE_URL}/inventorysearch`);
        const data = await res.json();
        if (data.success) {
          setLovData(data.data);
        }
      }
    } catch (err) {
      console.error(err);
      showToast('Error', 'Failed to load LOV data');
    } finally {
      setLoading(false);
    }
  };

  const filteredLovData = useMemo(() => {
    if (!lovSearchQuery) return lovData;
    const q = lovSearchQuery.toLowerCase();
    return lovData.filter(item => {
      if (lovType === 'customer') {
        return (item.CustomerCode || '').toLowerCase().includes(q) ||
          (item.CustomerName || '').toLowerCase().includes(q) ||
          (item.NricNo || '').toLowerCase().includes(q) ||
          (item.Address1 || '').toLowerCase().includes(q);
      } else {
        return (item.InventoryCode || '').toLowerCase().includes(q) ||
          (item.Description || '').toLowerCase().includes(q) ||
          (item.JobSheetNo || '').toLowerCase().includes(q) ||
          (item.PurchaseBillNo || '').toLowerCase().includes(q);
      }
    });
  }, [lovData, lovSearchQuery, lovType]);

  const handleSelectLOV = (item: any) => {
    if (!item) {
      setLovVisible(false);
      return;
    }
    if (lovType === 'customer') {
      setCustomerCode(item.CustomerCode || '');
      setCustomerName(item.CustomerName || '');
      setAddress1(item.Address1 || '');
      setAddress2(item.Address2 || '');
      setAddress3(item.Address3 || '');
      setCustomerType(item.CustomerType || 'Customer Type');
    } else if (lovType === 'inventory') {
      const code = item.InventoryCode || item.inventoryCode || '';
      setEntryItem(prev => ({ ...(prev || {}), InventoryCode: code }));
      if (code) {
        fetchInventory(code);
      }
    }
    setLovVisible(false);
  };

  const verifySalesmanPassword = async () => {
    if (!salesmanPassword) {
      showToast('Error', 'Please enter a password');
      return;
    }
    setVerifyingPassword(true);
    try {
      const response = await fetch(`${API_BASE_URL}/salesreturn/verifypassword`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ password: salesmanPassword })
      });
      const data = await response.json();
      if (data.success) {
        setSalesMan(data.salesman.SalesManName);
        setPasswordModalVisible(false);
      } else {
        showToast('Error', data.message || 'Invalid password');
      }
    } catch (err: any) {
      showToast('Error', err.message);
    } finally {
      setVerifyingPassword(false);
    }
  };

  const renderDatePicker = (dateObj: { DD: string, MM: string, YYYY: string }, setDateObj: Function) => {
    if (Platform.OS === 'web') {
      const dateStr = dateObj.YYYY && dateObj.MM && dateObj.DD
        ? `${dateObj.YYYY}-${dateObj.MM.padStart(2, '0')}-${dateObj.DD.padStart(2, '0')}`
        : '';
      const handleChange = (e: any) => {
        const val = e.target.value;
        if (val) {
          const [y, m, d] = val.split('-');
          setDateObj({ DD: d, MM: m, YYYY: y });
        } else {
          setDateObj({ DD: '', MM: '', YYYY: '' });
        }
      };
      // @ts-ignore
      return <input type="date" value={dateStr} onChange={handleChange} style={{ width: '100%', padding: '3px 8px', borderColor: '#E2D7C5', borderWidth: '1px', borderStyle: 'solid', boxSizing: 'border-box', borderRadius: '5px', height: '26px', fontSize: '11px', backgroundColor: '#FFFFFF', color: '#2B2118', outline: 'none' }} />;
    } else {
      return (
        <View style={{ flexDirection: 'row', flex: 1, gap: 4 }}>
          <TextInput style={[styles.formInput, { flex: 1, textAlign: 'center' }]} value={dateObj.DD} onChangeText={(t) => setDateObj({ ...dateObj, DD: t })} placeholder="DD" placeholderTextColor="#9E8D77" />
          <TextInput style={[styles.formInput, { flex: 1, textAlign: 'center' }]} value={dateObj.MM} onChangeText={(t) => setDateObj({ ...dateObj, MM: t })} placeholder="MM" placeholderTextColor="#9E8D77" />
          <TextInput style={[styles.formInput, { flex: 2, textAlign: 'center' }]} value={dateObj.YYYY} onChangeText={(t) => setDateObj({ ...dateObj, YYYY: t })} placeholder="YYYY" placeholderTextColor="#9E8D77" />
        </View>
      );
    }
  };

  return (
    <SafeAreaView style={styles.container} edges={['top']}>
      <Stack.Screen options={{ headerShown: false }} />

      {/* SALESMAN PASSWORD MODAL */}
      <Modal visible={passwordModalVisible} transparent animationType="fade">
        <TouchableOpacity style={styles.modalOverlay} activeOpacity={1}>
          <TouchableWithoutFeedback>
            <View style={[styles.passwordModalContent, { maxWidth: 320, width: '100%' }]}>
              <View style={styles.passwordModalHeader}>
                <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                  <View style={styles.headerIconBadgeSmall}>
                    <Ionicons name="lock-closed" size={14} color="#B48218" />
                  </View>
                  <ThemedText style={styles.passwordModalTitle}>SalesMan Authorization</ThemedText>
                </View>
              </View>

              <View style={{ padding: 16, alignItems: 'center' }}>
                <ThemedText style={{ fontSize: 12, color: '#554428', marginBottom: 12, textAlign: 'center' }}>
                  Enter SalesMan Password to access Sales Return
                </ThemedText>
                <TextInput
                  style={[
                    styles.formInput,
                    {
                      width: '100%',
                      height: 38,
                      textAlign: 'center',
                      fontSize: 14,
                      fontWeight: 'bold',
                      letterSpacing: 2,
                      borderColor: '#B8903B',
                      marginBottom: 16,
                      borderRadius: 6,
                    },
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
                    style={[styles.actionBtn, styles.clearBtn, { flex: 1, justifyContent: 'center', paddingVertical: 8 }]}
                    onPress={() => router.back()}
                  >
                    <ThemedText style={styles.clearBtnText}>Cancel</ThemedText>
                  </TouchableOpacity>
                  <TouchableOpacity
                    style={[styles.actionBtn, styles.saveBtn, { flex: 1, justifyContent: 'center', paddingVertical: 8 }]}
                    onPress={verifySalesmanPassword}
                    disabled={verifyingPassword}
                  >
                    {verifyingPassword ? (
                      <ActivityIndicator size="small" color="#9E6D08" />
                    ) : (
                      <ThemedText style={styles.saveBtnText}>Verify</ThemedText>
                    )}
                  </TouchableOpacity>
                </View>
              </View>
            </View>
          </TouchableWithoutFeedback>
        </TouchableOpacity>
      </Modal>

      {/* LUXURY GOLD HEADER BAR */}
      <View style={styles.headerBar}>
        <View style={styles.headerLeft}>
          <TouchableOpacity
            style={styles.backBtn}
            onPress={() => router.back()}
            activeOpacity={0.7}
          >
            <Ionicons name="arrow-back" size={20} color="#3B2D17" />
          </TouchableOpacity>
          <ThemedText style={styles.headerTitle}>Sales Return</ThemedText>
          {loading && <ActivityIndicator size="small" color="#B48218" style={{ marginLeft: 6 }} />}
        </View>

        <View style={styles.headerRight}>
          <TouchableOpacity
            style={[styles.actionBtn, styles.clearBtn]}
            onPress={clearForm}
            activeOpacity={0.8}
            disabled={saving}
          >
            <Ionicons name="refresh-outline" size={14} color="#5C4A28" />
            <ThemedText style={styles.clearBtnText}>Clear</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity
            style={[styles.actionBtn, styles.saveBtn]}
            onPress={handleSave}
            activeOpacity={0.8}
            disabled={saving}
          >
            {saving ? (
              <ActivityIndicator size="small" color="#9E6D08" />
            ) : (
              <>
                <Ionicons name="checkmark-done-outline" size={14} color="#9E6D08" />
                <ThemedText style={styles.saveBtnText}>Save</ThemedText>
              </>
            )}
          </TouchableOpacity>
        </View>
      </View>

      <ScrollView contentContainerStyle={styles.contentContainer}>
        {/* TOP SECTION: BILL INFO & CUSTOMER INFO */}
        <View style={styles.topSplitRow}>
          {/* Bill Info Card */}
          <View style={[styles.goldCard, { flex: 1 }]}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="document-text-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitleSmall}>Bill Information</ThemedText>
              </View>
            </View>

            <View style={styles.cardBody}>
              {/* Bill No */}
              <View style={styles.formGridRow}>
                <ThemedText style={styles.formLabel}>Bill No</ThemedText>
                <View style={styles.inputWithLookup}>
                  <TextInput
                    style={styles.formInput}
                    value={billNo}
                    onChangeText={setBillNo}
                    onBlur={() => fetchBillInfo()}
                    placeholder="Enter or scan bill #"
                    placeholderTextColor="#9E8D77"
                  />
                  <TouchableOpacity
                    style={styles.attachedLookupBtn}
                    onPress={() => fetchBillInfo()}
                    activeOpacity={0.7}
                  >
                    <Ionicons name="search" size={13} color="#7D5E19" />
                  </TouchableOpacity>
                </View>
              </View>

              {/* Sales Man */}
              <View style={styles.formGridRow}>
                <ThemedText style={styles.formLabel}>Sales Man</ThemedText>
                <TextInput
                  style={[styles.formInput, styles.readonlyInput]}
                  value={salesMan}
                  onChangeText={setSalesMan}
                  placeholder="Salesman name"
                  placeholderTextColor="#9E8D77"
                />
              </View>

              {/* Bill Date */}
              <View style={styles.formGridRow}>
                <ThemedText style={styles.formLabel}>Bill Date</ThemedText>
                <View style={{ flex: 1 }}>
                  {renderDatePicker(billDate, setBillDate)}
                </View>
              </View>

              {/* Return Date */}
              <View style={styles.formGridRow}>
                <ThemedText style={styles.formLabel}>Return Date</ThemedText>
                <View style={{ flex: 1 }}>
                  {renderDatePicker(returnDate, setReturnDate)}
                </View>
              </View>

              {/* Tax Type Radios */}
              <View style={[styles.formGridRow, { borderBottomWidth: 0, paddingTop: 6, flexWrap: 'wrap' }]}>
                {['Zero Rated', 'Tax Inclusive', 'Tax Exclusive'].map((type) => (
                  <TouchableOpacity
                    key={type}
                    style={styles.radioContainer}
                    onPress={() => setTaxType(type as any)}
                    activeOpacity={0.7}
                  >
                    <View style={[styles.radioCircle, taxType === type && styles.radioCircleSelected]}>
                      {taxType === type && <View style={styles.radioInner} />}
                    </View>
                    <ThemedText style={styles.radioLabel}>{type}</ThemedText>
                  </TouchableOpacity>
                ))}
              </View>
            </View>
          </View>

          {/* Customer Info Card */}
          <View style={[styles.goldCard, { flex: 1.3 }]}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="person-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitleSmall}>Customer Information</ThemedText>
              </View>
              <View style={styles.customerTypeBadge}>
                <ThemedText style={styles.customerTypeBadgeText}>{customerType}</ThemedText>
              </View>
            </View>

            <View style={styles.cardBody}>
              {/* Customer Code with LOV */}
              <View style={styles.formGridRow}>
                <ThemedText style={styles.formLabel}>Customer Code</ThemedText>
                <View style={styles.inputWithLookup}>
                  <TextInput
                    style={[styles.formInput, styles.inputAttached]}
                    value={customerCode}
                    onChangeText={setCustomerCode}
                    placeholder="Select Customer..."
                    placeholderTextColor="#9E8D77"
                  />
                  <TouchableOpacity
                    style={styles.attachedLookupBtn}
                    onPress={() => openLov('customer')}
                    activeOpacity={0.7}
                  >
                    <ThemedText style={styles.attachedLookupText}>...</ThemedText>
                  </TouchableOpacity>
                </View>
              </View>

              {/* Name */}
              <View style={styles.formGridRow}>
                <ThemedText style={styles.formLabel}>Name</ThemedText>
                <TextInput
                  style={styles.formInput}
                  value={customerName}
                  onChangeText={setCustomerName}
                  placeholder="Customer Name"
                  placeholderTextColor="#9E8D77"
                />
              </View>

              {/* Address 1 */}
              <View style={styles.formGridRow}>
                <ThemedText style={styles.formLabel}>Address</ThemedText>
                <TextInput
                  style={styles.formInput}
                  value={address1}
                  onChangeText={setAddress1}
                  placeholder="Street / Unit address"
                  placeholderTextColor="#9E8D77"
                />
              </View>

              {/* Address 2 & 3 */}
              <View style={styles.formGridRow}>
                <ThemedText style={styles.formLabel}></ThemedText>
                <View style={{ flexDirection: 'row', flex: 1, gap: 6 }}>
                  <TextInput
                    style={[styles.formInput, { flex: 1 }]}
                    value={address2}
                    onChangeText={setAddress2}
                    placeholder="City / Area"
                    placeholderTextColor="#9E8D77"
                  />
                  <TextInput
                    style={[styles.formInput, { flex: 1 }]}
                    value={address3}
                    onChangeText={setAddress3}
                    placeholder="Postal Code / Country"
                    placeholderTextColor="#9E8D77"
                  />
                </View>
              </View>

              {/* Remarks */}
              <View style={[styles.formGridRow, { borderBottomWidth: 0 }]}>
                <ThemedText style={styles.formLabel}>Remarks</ThemedText>
                <TextInput
                  style={styles.formInput}
                  value={remarks}
                  onChangeText={setRemarks}
                  placeholder="Optional notes or return reason..."
                  placeholderTextColor="#9E8D77"
                />
              </View>
            </View>
          </View>
        </View>

        {/* RETURN ITEMS CARD & TABLE */}
        <View style={[styles.goldCard, { marginTop: 8 }]}>
          <View style={styles.cardHeaderBanner}>
            <View style={styles.headerTitleContainer}>
              <View style={styles.headerIconBadgeSmall}>
                <Ionicons name="cart-outline" size={13} color="#B48218" />
              </View>
              <ThemedText style={styles.cardHeaderTitleSmall}>Return Items</ThemedText>
              <View style={styles.counterBadge}>
                <ThemedText style={styles.counterBadgeText}>{returnItems.length} items</ThemedText>
              </View>
            </View>
          </View>

          {/* Table Header */}
          <View style={styles.tableHeaderRow}>
            <ThemedText style={[styles.tableHeadCell, { flex: 1.5 }]}>Inventory Code</ThemedText>
            <ThemedText style={[styles.tableHeadCell, { flex: 3 }]}>Description</ThemedText>
            <ThemedText style={[styles.tableHeadCell, { flex: 1.2, textAlign: 'right' }]}>Weight (g)</ThemedText>
            <ThemedText style={[styles.tableHeadCell, { flex: 0.8, textAlign: 'right' }]}>PCS</ThemedText>
            <ThemedText style={[styles.tableHeadCell, { flex: 1.5, textAlign: 'right' }]}>Return Amount</ThemedText>
          </View>

          {/* Table Rows */}
          {returnItems.length === 0 ? (
            <View style={styles.emptyTableContainer}>
              <Ionicons name="basket-outline" size={32} color="#D4C3A3" />
              <ThemedText style={styles.emptyTableText}>No return items added yet</ThemedText>
            </View>
          ) : (
            returnItems.map((item, idx) => {
              const isSelected = selectedRowIndex === idx;
              return (
                <TouchableOpacity
                  key={idx}
                  style={[styles.tableRow, isSelected && styles.tableRowSelected]}
                  onPress={() => {
                    setSelectedRowIndex(idx);
                    setEntryItem({ ...item });
                  }}
                  activeOpacity={0.7}
                >
                  <ThemedText style={[styles.tableRowText, styles.codeCell, { flex: 1.5 }]}>
                    {item.InventoryCode}
                  </ThemedText>
                  <ThemedText style={[styles.tableRowText, { flex: 3 }]}>
                    {item.Description}
                  </ThemedText>
                  <ThemedText style={[styles.tableRowText, { flex: 1.2, textAlign: 'right' }]}>
                    {Number(item.Weight || 0).toFixed(3)}
                  </ThemedText>
                  <ThemedText style={[styles.tableRowText, { flex: 0.8, textAlign: 'right' }]}>
                    {item.PCS}
                  </ThemedText>
                  <ThemedText style={[styles.tableRowText, styles.amountCell, { flex: 1.5, textAlign: 'right' }]}>
                    ${Number(item.ReturnAmount || 0).toFixed(2)}
                  </ThemedText>
                </TouchableOpacity>
              );
            })
          )}

          {/* Item Entry Row */}
          <View style={styles.entryRowContainer}>
            <View style={{ flexDirection: 'row', alignItems: 'flex-end', gap: 6, flexWrap: 'wrap' }}>
              {/* Inventory Code with LOV */}
              <View style={{ flex: 1.5, minWidth: 140 }}>
                <ThemedText style={styles.itemLabel}>Inventory Code</ThemedText>
                <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                  <TextInput
                    style={[styles.itemInput, styles.inputAttached, { flex: 1 }]}
                    value={entryItem.InventoryCode}
                    onChangeText={(t) => setEntryItem({ ...entryItem, InventoryCode: t })}
                    onBlur={() => fetchInventory()}
                    placeholder="Code..."
                    placeholderTextColor="#9E8D77"
                  />
                  <TouchableOpacity
                    style={styles.attachedLookupBtn}
                    onPress={() => openLov('inventory')}
                    activeOpacity={0.7}
                  >
                    <ThemedText style={styles.attachedLookupText}>...</ThemedText>
                  </TouchableOpacity>
                </View>
              </View>

              {/* Description */}
              <View style={{ flex: 3, minWidth: 180 }}>
                <ThemedText style={styles.itemLabel}>Description</ThemedText>
                <TextInput
                  style={styles.itemInput}
                  value={entryItem.Description}
                  onChangeText={(t) => setEntryItem({ ...entryItem, Description: t })}
                  placeholder="Item details..."
                  placeholderTextColor="#9E8D77"
                />
              </View>

              {/* Weight */}
              <View style={{ flex: 1.2, minWidth: 80 }}>
                <ThemedText style={styles.itemLabel}>Weight</ThemedText>
                <TextInput
                  style={[styles.itemInput, { textAlign: 'right' }]}
                  value={entryItem.Weight}
                  onChangeText={(t) => setEntryItem({ ...entryItem, Weight: t })}
                  keyboardType="numeric"
                  placeholder="0.00"
                  placeholderTextColor="#9E8D77"
                />
              </View>

              {/* PCS */}
              <View style={{ flex: 0.8, minWidth: 60 }}>
                <ThemedText style={styles.itemLabel}>PCS</ThemedText>
                <TextInput
                  style={[styles.itemInput, { textAlign: 'right' }]}
                  value={entryItem.PCS}
                  onChangeText={(t) => setEntryItem({ ...entryItem, PCS: t })}
                  keyboardType="numeric"
                  placeholder="0"
                  placeholderTextColor="#9E8D77"
                />
              </View>

              {/* Return Amount */}
              <View style={{ flex: 1.5, minWidth: 90 }}>
                <ThemedText style={styles.itemLabel}>Return Amount</ThemedText>
                <TextInput
                  style={[styles.itemInput, { textAlign: 'right' }]}
                  value={entryItem.ReturnAmount}
                  onChangeText={(t) => setEntryItem({ ...entryItem, ReturnAmount: t })}
                  keyboardType="numeric"
                  placeholder="0.00"
                  placeholderTextColor="#9E8D77"
                />
              </View>

              {/* Action Buttons */}
              <View style={{ flexDirection: 'row', gap: 6, alignItems: 'center', marginTop: 14 }}>
                <TouchableOpacity
                  style={[
                    styles.actionBtn,
                    styles.saveBtn,
                    (!entryItem.InventoryCode || !entryItem.Description || (selectedRowIndex === null && !!billNo)) && { opacity: 0.5 }
                  ]}
                  onPress={handleAdd}
                  disabled={!entryItem.InventoryCode || !entryItem.Description || (selectedRowIndex === null && !!billNo)}
                >
                  <ThemedText style={styles.saveBtnText}>
                    {selectedRowIndex !== null ? 'Update' : '+ Add'}
                  </ThemedText>
                </TouchableOpacity>

                {selectedRowIndex !== null && (
                  <TouchableOpacity
                    style={[styles.actionBtn, styles.destructiveBtn]}
                    onPress={handleDelete}
                  >
                    <ThemedText style={styles.destructiveBtnText}>Delete</ThemedText>
                  </TouchableOpacity>
                )}

                <TouchableOpacity
                  style={[styles.actionBtn, styles.clearBtn]}
                  onPress={clearEntry}
                >
                  <ThemedText style={styles.clearBtnText}>Clear</ThemedText>
                </TouchableOpacity>
              </View>
            </View>
          </View>
        </View>

        {/* BOTTOM TOTALS SUMMARY BAR */}
        <View style={styles.bottomSummaryBar}>
          <View style={styles.summaryItem}>
            <ThemedText style={styles.summaryLabel}>Total Qty</ThemedText>
            <ThemedText style={styles.summaryValue}>{totals.qty}</ThemedText>
          </View>

          <View style={styles.summaryDivider} />

          <View style={styles.summaryItem}>
            <ThemedText style={styles.summaryLabel}>Total Amount</ThemedText>
            <ThemedText style={styles.summaryValue}>${totals.amount}</ThemedText>
          </View>

          <View style={styles.summaryDivider} />

          <View style={styles.summaryItem}>
            <ThemedText style={styles.summaryLabel}>GST ({gstRate}%)</ThemedText>
            <ThemedText style={[styles.summaryValue, { color: '#8D6409' }]}>${totals.gst}</ThemedText>
          </View>

          <View style={styles.summaryDivider} />

          <View style={styles.summaryItemHighlight}>
            <ThemedText style={styles.summaryHighlightLabel}>Net Return Amount</ThemedText>
            <ThemedText style={styles.summaryHighlightValue}>${totals.net}</ThemedText>
          </View>
        </View>
      </ScrollView>

      {/* LOV MODAL */}
      <Modal visible={lovVisible} transparent animationType="fade" onRequestClose={() => setLovVisible(false)}>
        <TouchableOpacity style={styles.modalOverlay} activeOpacity={1} onPress={() => setLovVisible(false)}>
          <TouchableWithoutFeedback>
            <View style={styles.lovModalContainer}>
              <View style={styles.modalHeader}>
                <View style={{ flexDirection: 'row', alignItems: 'center', gap: 6 }}>
                  <View style={styles.headerIconBadgeSmall}>
                    <Ionicons name="list" size={14} color="#B48218" />
                  </View>
                  <ThemedText style={styles.modalTitle}>
                    Select {lovType === 'customer' ? 'Customer' : 'Inventory'}
                  </ThemedText>
                </View>
                <TouchableOpacity onPress={() => setLovVisible(false)} style={styles.modalCloseBtn}>
                  <Ionicons name="close" size={20} color="#3B2D17" />
                </TouchableOpacity>
              </View>

              <View style={{ padding: 12 }}>
                <View style={styles.modalSearchBox}>
                  <Ionicons name="search-outline" size={15} color="#8F816E" style={{ marginRight: 6 }} />
                  <TextInput
                    style={styles.modalSearchInput}
                    placeholder="Search options..."
                    placeholderTextColor="#8F816E"
                    value={lovSearchQuery}
                    onChangeText={setLovSearchQuery}
                    autoFocus
                  />
                </View>

                <View style={styles.lovHeaderRow}>
                  {lovType === 'customer' ? (
                    <>
                      <ThemedText style={[styles.lovHeaderText, { flex: 1.5 }]}>Customer Code</ThemedText>
                      <ThemedText style={[styles.lovHeaderText, { flex: 2 }]}>Customer Name</ThemedText>
                      <ThemedText style={[styles.lovHeaderText, { flex: 1 }]}>NRIC No</ThemedText>
                      <ThemedText style={[styles.lovHeaderText, { flex: 1.5 }]}>Address</ThemedText>
                    </>
                  ) : (
                    <>
                      <ThemedText style={[styles.lovHeaderText, { flex: 1.5 }]}>Inventory Code</ThemedText>
                      <ThemedText style={[styles.lovHeaderText, { flex: 2 }]}>Description</ThemedText>
                      <ThemedText style={[styles.lovHeaderText, { flex: 1 }]}>Job Sheet #</ThemedText>
                      <ThemedText style={[styles.lovHeaderText, { flex: 1.5 }]}>Purchase Bill #</ThemedText>
                    </>
                  )}
                </View>

                {loading ? (
                  <View style={{ padding: 30, alignItems: 'center' }}>
                    <ActivityIndicator size="large" color="#B48218" />
                  </View>
                ) : (
                  <FlatList
                    data={filteredLovData}
                    keyExtractor={(_, idx) => idx.toString()}
                    style={{ maxHeight: 320 }}
                    renderItem={({ item, index }) => (
                      <TouchableOpacity
                        style={[
                          styles.lovItemRow,
                          index % 2 === 1 && { backgroundColor: '#FCFAF6' }
                        ]}
                        activeOpacity={0.7}
                        onPress={() => handleSelectLOV(item)}
                      >
                        {lovType === 'customer' ? (
                          <>
                            <ThemedText style={[styles.lovItemText, styles.codeCell, { flex: 1.5 }]}>{item.CustomerCode}</ThemedText>
                            <ThemedText style={[styles.lovItemText, { flex: 2 }]}>{item.CustomerName}</ThemedText>
                            <ThemedText style={[styles.lovItemText, { flex: 1 }]}>{item.NricNo}</ThemedText>
                            <ThemedText style={[styles.lovItemText, { flex: 1.5 }]}>{item.Address1}</ThemedText>
                          </>
                        ) : (
                          <>
                            <ThemedText style={[styles.lovItemText, styles.codeCell, { flex: 1.5 }]}>{item.InventoryCode}</ThemedText>
                            <ThemedText style={[styles.lovItemText, { flex: 2 }]}>{item.Description}</ThemedText>
                            <ThemedText style={[styles.lovItemText, { flex: 1 }]}>{item.JobSheetNo}</ThemedText>
                            <ThemedText style={[styles.lovItemText, { flex: 1.5 }]}>{item.PurchaseBillNo}</ThemedText>
                          </>
                        )}
                      </TouchableOpacity>
                    )}
                  />
                )}
              </View>
            </View>
          </TouchableWithoutFeedback>
        </TouchableOpacity>
      </Modal>

      {/* TOAST ALERT */}
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
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#FBF9F5',
  },
  contentContainer: {
    padding: 8,
    paddingBottom: 24,
  },

  // ===== HEADER BAR =====
  headerBar: {
    height: 48,
    backgroundColor: '#FFFFFF',
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingHorizontal: 12,
    borderBottomWidth: 1,
    borderBottomColor: '#EBE2D2',
    elevation: 2,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.05,
    shadowRadius: 2,
  },
  headerLeft: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 8,
  },
  backBtn: {
    padding: 6,
    borderRadius: 6,
    backgroundColor: '#FAF5EA',
    borderWidth: 1,
    borderColor: '#E8DCB8',
  },
  headerIconBadge: {
    width: 28,
    height: 28,
    borderRadius: 6,
    backgroundColor: '#FAF5EA',
    borderWidth: 1,
    borderColor: '#E8DCB8',
    alignItems: 'center',
    justifyContent: 'center',
  },
  headerTitle: {
    fontSize: 18,
    fontWeight: 'bold',
    color: '#3B2D17',
    letterSpacing: 0.5,
  },
  headerRight: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 6,
  },

  // ===== ACTION BUTTONS =====
  actionBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    paddingHorizontal: 12,
    paddingVertical: 5,
    borderRadius: 6,
    borderWidth: 1,
    gap: 4,
    minHeight: 28,
  },
  saveBtn: {
    backgroundColor: '#F9F2E2',
    borderColor: '#B8903B',
  },
  saveBtnText: {
    color: '#9E6D08',
    fontSize: 12,
    fontWeight: 'bold',
  },
  clearBtn: {
    backgroundColor: '#FFFFFF',
    borderColor: '#DFD5C3',
  },
  clearBtnText: {
    color: '#5C4A28',
    fontSize: 12,
    fontWeight: 'bold',
  },
  destructiveBtn: {
    backgroundColor: '#FFFFFF',
    borderColor: '#F2A8A2',
  },
  destructiveBtnText: {
    color: '#D93829',
    fontSize: 12,
    fontWeight: 'bold',
  },

  // ===== LAYOUT PANELS =====
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
  headerTitleContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 6,
  },
  headerIconBadgeSmall: {
    width: 22,
    height: 22,
    borderRadius: 5,
    backgroundColor: '#FFF8EA',
    borderWidth: 1,
    borderColor: '#E2D2AF',
    alignItems: 'center',
    justifyContent: 'center',
  },
  cardHeaderTitleSmall: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#3B2D17',
    letterSpacing: 0.3,
  },
  counterBadge: {
    backgroundColor: '#FAF5EA',
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderRadius: 10,
    paddingHorizontal: 7,
    paddingVertical: 1,
    marginLeft: 6,
  },
  counterBadgeText: {
    fontSize: 10,
    fontWeight: 'bold',
    color: '#7D5E19',
  },
  customerTypeBadge: {
    backgroundColor: '#FFF8EA',
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderRadius: 12,
    paddingHorizontal: 8,
    paddingVertical: 2,
  },
  customerTypeBadgeText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#8D6409',
  },
  cardBody: {
    padding: 8,
  },

  // ===== FORM ROWS & INPUTS =====
  formGridRow: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingVertical: 4,
    borderBottomWidth: 1,
    borderBottomColor: '#F7F2E9',
    gap: 8,
  },
  formLabel: {
    fontSize: 11,
    fontWeight: '600',
    color: '#4A3B2C',
    width: 85,
    flexShrink: 0,
  },
  inputWithLookup: {
    flexDirection: 'row',
    flex: 1,
    alignItems: 'center',
    minWidth: 0,
  },
  formInput: {
    height: 26,
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderRadius: 5,
    paddingHorizontal: 8,
    paddingVertical: 0,
    fontSize: 11,
    color: '#2B2118',
    flex: 1,
  },
  readonlyInput: {
    backgroundColor: '#F8F5EE',
    borderColor: '#EADBCA',
    color: '#554736',
  },
  inputAttached: {
    borderTopRightRadius: 0,
    borderBottomRightRadius: 0,
  },
  attachedLookupBtn: {
    width: 28,
    height: 26,
    flexShrink: 0,
    backgroundColor: '#FAF5EA',
    borderWidth: 1,
    borderColor: '#D4C3A3',
    borderRadius: 5,
    marginLeft: 4,
    alignItems: 'center',
    justifyContent: 'center',
  },
  attachedLookupText: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#6E5528',
    letterSpacing: 1,
    marginTop: -2,
  },
  inputSubLabel: {
    fontSize: 11,
    fontWeight: '600',
    color: '#4A3B2C',
  },

  // Custom Radio
  radioContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    marginRight: 10,
    paddingVertical: 2,
  },
  radioCircle: {
    width: 14,
    height: 14,
    borderRadius: 7,
    borderWidth: 1.5,
    borderColor: '#C4B69C',
    alignItems: 'center',
    justifyContent: 'center',
    marginRight: 5,
  },
  radioCircleSelected: {
    borderColor: '#B8903B',
  },
  radioInner: {
    width: 6,
    height: 6,
    borderRadius: 3,
    backgroundColor: '#B8903B',
  },
  radioLabel: {
    fontSize: 11,
    color: '#3B2D17',
    fontWeight: '500',
  },

  // ===== DATA TABLE =====
  tableHeaderRow: {
    flexDirection: 'row',
    backgroundColor: '#F5E7CA',
    paddingVertical: 6,
    paddingHorizontal: 8,
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
  },
  tableHeadCell: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3B2D17',
    textTransform: 'uppercase',
  },
  tableRow: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingVertical: 7,
    paddingHorizontal: 8,
    borderBottomWidth: 1,
    borderBottomColor: '#F3EBDD',
  },
  tableRowSelected: {
    backgroundColor: '#FFF6DF',
    borderLeftWidth: 3,
    borderLeftColor: '#B8903B',
  },
  tableRowText: {
    fontSize: 11,
    color: '#3B2D17',
  },
  codeCell: {
    fontWeight: 'bold',
    color: '#8D6409',
  },
  amountCell: {
    fontWeight: 'bold',
    color: '#9E6D08',
  },
  emptyTableContainer: {
    padding: 30,
    alignItems: 'center',
    justifyContent: 'center',
    gap: 6,
  },
  emptyTableText: {
    fontSize: 11,
    color: '#8F816E',
  },

  // ===== ITEM ENTRY ROW =====
  entryRowContainer: {
    padding: 8,
    backgroundColor: '#FAF6EE',
    borderTopWidth: 1,
    borderTopColor: '#EADBCA',
  },
  itemLabel: {
    fontSize: 10,
    fontWeight: '600',
    color: '#5C4A28',
    marginBottom: 2,
  },
  itemInput: {
    height: 26,
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderRadius: 5,
    paddingHorizontal: 6,
    paddingVertical: 0,
    fontSize: 11,
    color: '#2B2118',
  },

  // ===== BOTTOM TOTALS SUMMARY BAR =====
  bottomSummaryBar: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    paddingVertical: 8,
    paddingHorizontal: 12,
    marginTop: 8,
    elevation: 2,
    shadowColor: '#A38035',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.08,
    shadowRadius: 3,
  },
  summaryItem: {
    flex: 1,
    alignItems: 'center',
  },
  summaryDivider: {
    width: 1,
    height: 28,
    backgroundColor: '#F0E6D4',
  },
  summaryLabel: {
    fontSize: 10,
    fontWeight: '600',
    color: '#7D6F5B',
    textTransform: 'uppercase',
  },
  summaryValue: {
    fontSize: 14,
    fontWeight: 'bold',
    color: '#3B2D17',
    marginTop: 2,
  },
  summaryItemHighlight: {
    flex: 1.4,
    alignItems: 'center',
    backgroundColor: '#FFF8EA',
    borderWidth: 1,
    borderColor: '#B8903B',
    borderRadius: 6,
    paddingVertical: 4,
    paddingHorizontal: 8,
  },
  summaryHighlightLabel: {
    fontSize: 10,
    fontWeight: 'bold',
    color: '#8D6409',
    textTransform: 'uppercase',
    letterSpacing: 0.5,
  },
  summaryHighlightValue: {
    fontSize: 16,
    fontWeight: 'bold',
    color: '#9E6D08',
    marginTop: 1,
  },

  // ===== MODALS & LOV =====
  modalOverlay: {
    flex: 1,
    backgroundColor: 'rgba(30, 20, 10, 0.45)',
    justifyContent: 'center',
    alignItems: 'center',
    padding: 16,
  },
  passwordModalContent: {
    width: 320,
    backgroundColor: '#FFFFFF',
    borderRadius: 10,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    overflow: 'hidden',
    shadowColor: '#000',
    shadowOpacity: 0.15,
    shadowRadius: 10,
    elevation: 6,
  },
  passwordModalHeader: {
    backgroundColor: '#F5E7CA',
    paddingHorizontal: 12,
    paddingVertical: 10,
    flexDirection: 'row',
    alignItems: 'center',
    gap: 8,
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
  },
  passwordModalTitle: {
    fontSize: 13,
    fontWeight: 'bold',
    color: '#3B2D17',
  },
  lovModalContainer: {
    width: '100%',
    maxWidth: 640,
    backgroundColor: '#FFFFFF',
    borderRadius: 10,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    overflow: 'hidden',
    shadowColor: '#000',
    shadowOpacity: 0.15,
    shadowRadius: 10,
    elevation: 6,
  },
  modalHeader: {
    backgroundColor: '#F5E7CA',
    paddingHorizontal: 14,
    paddingVertical: 10,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
  },
  modalTitle: {
    fontSize: 13,
    fontWeight: 'bold',
    color: '#3B2D17',
  },
  modalCloseBtn: {
    padding: 4,
  },
  modalSearchBox: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderRadius: 6,
    paddingHorizontal: 10,
    height: 32,
    marginBottom: 8,
  },
  modalSearchInput: {
    flex: 1,
    fontSize: 12,
    color: '#2B2118',
    paddingVertical: 0,
  },
  lovHeaderRow: {
    flexDirection: 'row',
    backgroundColor: '#F5E7CA',
    paddingVertical: 6,
    paddingHorizontal: 8,
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
  },
  lovHeaderText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3B2D17',
  },
  lovItemRow: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingVertical: 8,
    paddingHorizontal: 8,
    borderBottomWidth: 1,
    borderBottomColor: '#F3EBDD',
  },
  lovItemText: {
    fontSize: 12,
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
