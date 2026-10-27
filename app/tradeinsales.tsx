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

const API_URL = `${API_BASE_URL}/tradeinsales`;

interface GridItem {
  InventoryCode: string;
  Description: string;
  Weight: number;
  GoldLoss: number;
  CostPerGram: number;
  TotalCost: number;
  DiamondDescription: string;
  StoneWeight: number;
  ClassCode: string;
}

export default function TradeInSalesScreen() {
  const router = useRouter();

  // State Variables - Header Panel
  const [loading, setLoading] = useState(false);
  const [isEditMode, setIsEditMode] = useState(false);
  const [refNo, setRefNo] = useState('');
  const [invoiceNo, setInvoiceNo] = useState('');
  const [costPerGram, setCostPerGram] = useState('0');
  const [lossPerc, setLossPerc] = useState('0');
  const [customerCode, setCustomerCode] = useState('');
  const [customerName, setCustomerName] = useState('');
  const [customerType, setCustomerType] = useState('Customer Type');
  const [address1, setAddress1] = useState('');
  const [address2, setAddress2] = useState('');
  const [address3, setAddress3] = useState('');
  const [remarks, setRemarks] = useState('');
  const [memberCode, setMemberCode] = useState('');
  const [companyShortCode, setCompanyShortCode] = useState('');

  // Top-Right Status Display
  const [salesAmount, setSalesAmount] = useState('0.00');

  // Detail Inputs (Below Grid)
  const [detailDescription, setDetailDescription] = useState('');
  const [detailWeight, setDetailWeight] = useState('');
  const [detailGoldLoss, setDetailGoldLoss] = useState('');
  const [detailCostPerGram, setDetailCostPerGram] = useState('0');
  const [detailTotalCost, setDetailTotalCost] = useState('0.00');
  const [detailDiamondDesc, setDetailDiamondDesc] = useState('');
  const [detailStoneWeight, setDetailStoneWeight] = useState('');

  // Grid Data
  const [gridItems, setGridItems] = useState<GridItem[]>([]);
  const [selectedGridIndex, setSelectedGridIndex] = useState<number | null>(null);

  // Lookups and Modals
  const [lookupType, setLookupType] = useState<'customer' | 'member' | null>(null);
  const [lookupSearch, setLookupSearch] = useState('');
  const [customersList, setCustomersList] = useState<any[]>([]);
  const [membersList, setMembersList] = useState<any[]>([]);
  const [classRates, setClassRates] = useState<Record<string, number>>({});

  // Customer Code direct input modal / prompt
  const [isCustInputModalVisible, setIsCustInputModalVisible] = useState(false);
  const [custInputTemp, setCustInputTemp] = useState('');

  // Member Code modal (pnlMember)
  const [isCashMemberModalVisible, setIsCashMemberModalVisible] = useState(false);
  const [cashMemberCodeInput, setCashMemberCodeInput] = useState('');

  // Reprint Bill Modal (PnlReprint)
  const [isReprintModalVisible, setIsReprintModalVisible] = useState(false);
  const [reprintBillNo, setReprintBillNo] = useState('');

  // Cost Admin Password verification modal (for Cost > Market Rate)
  const [isAdminPwdVisible, setIsAdminPwdVisible] = useState(false);
  const [adminPassword, setAdminPassword] = useState('');
  const [pendingGridItem, setPendingGridItem] = useState<GridItem | null>(null);

  // Salesman Password Modal (PnlSalesManPwd)
  const [salesmanPasswordModalVisible, setSalesmanPasswordModalVisible] = useState(true);
  const [salesmanPassword, setSalesmanPassword] = useState('');
  const [salesmanCode, setSalesmanCode] = useState('');
  const [verifyingSalesmanPassword, setVerifyingSalesmanPassword] = useState(false);

  const [toast, setToast] = useState<{ message: string, type: 'success' | 'error' } | null>(null);

  const showToast = (message: string, type: 'success' | 'error' = 'success') => {
    setToast({ message, type });
    setTimeout(() => {
      setToast(null);
    }, 3000);
  };

  useEffect(() => {
    fetchInitialData();
  }, []);

  const fetchInitialData = async () => {
    try {
      setLoading(true);
      const refRes = await fetch(`${API_URL}/next-ref?t=${Date.now()}`);
      const refJson = await refRes.json();
      let compShort = '';
      if (refJson.success) {
        setRefNo(refJson.refNo);
        setInvoiceNo(refJson.refNo);
        compShort = (refJson.companyShortCode || '').trim();
        setCompanyShortCode(compShort);
      }

      // Class Rates from tblClass
      const ratesRes = await fetch(`${API_URL}/class-rates?t=${Date.now()}`);
      const ratesJson = await ratesRes.json();
      if (ratesJson.success) {
        const ratesMap: Record<string, number> = {};
        ratesJson.data.forEach((r: any) => {
          const code = String(r.ClassCode || r.classcode || '').trim();
          const rateVal = parseFloat(r.Rate || r.rate) || 0;
          ratesMap[code] = rateVal;
        });
        setClassRates(ratesMap);

        // subGetCostPerGram logic from VB:
        let defaultLoss = 0;
        if (compShort.toUpperCase() === 'ES') {
          defaultLoss = 18;
          setLossPerc('18');
        }

        let baseRate = 0;
        const key22 = Object.keys(ratesMap).find(k => k.toUpperCase().startsWith('22'));
        if (key22 && ratesMap[key22] > 0) {
          baseRate = ratesMap[key22];
        } else {
          const key18 = Object.keys(ratesMap).find(k => k.toUpperCase().startsWith('18K'));
          if (key18 && ratesMap[key18] > 0) {
            baseRate = ratesMap[key18];
          } else {
            const firstKey = Object.keys(ratesMap)[0];
            if (firstKey) baseRate = ratesMap[firstKey];
          }
        }

        if (baseRate > 0) {
          setCostPerGram(String(baseRate));
          const finalCost = baseRate - (baseRate * defaultLoss) / 100;
          setDetailCostPerGram(String(finalCost));
        }
      }
    } catch (err) {
      console.error(err);
      Alert.alert("Network Error", "Failed to connect to the backend server.");
    } finally {
      setLoading(false);
    }
  };

  // Re-calculate item total cost: (Val(txtWeight) + Val(txtGoldLoss)) * Val(txtCostPerGram1)
  const handleDetailCalculations = (weightStr: string, goldLossStr: string, costStr: string) => {
    const wt = parseFloat(weightStr) || 0;
    const loss = parseFloat(goldLossStr) || 0;
    const cost = parseFloat(costStr) || 0;
    const rawTotal = (wt + loss) * cost;
    setDetailTotalCost(rawTotal.toFixed(2));
  };

  // When TotalCost is typed directly in detail row
  const handleDetailTotalCostChange = (val: string) => {
    setDetailTotalCost(val);
    const total = parseFloat(val) || 0;
    const wt = parseFloat(detailWeight) || 0;
    const loss = parseFloat(detailGoldLoss) || 0;
    const denom = wt + loss;
    if (denom > 0 && total > 0) {
      const calculatedCostPerGram = total / denom;
      setDetailCostPerGram(calculatedCostPerGram.toFixed(2));
    }
  };

  // Fetch customer details by code
  const fetchCustomerByCode = async (code: string) => {
    const cleanCode = code.trim().toUpperCase();
    if (!cleanCode) return;

    if (cleanCode === 'CASH') {
      setCustomerCode('CASH');
      setCustomerName('CASH CUSTOMER');
      setCustomerType('CASH');
      setAddress1('');
      setAddress2('');
      setAddress3('');
      if (companyShortCode.toUpperCase() !== 'GMT') {
        setCashMemberCodeInput('');
        setIsCashMemberModalVisible(true);
      }
      return;
    }

    try {
      setLoading(true);
      const res = await fetch(`${API_URL}/customer/${encodeURIComponent(cleanCode)}`);
      const json = await res.json();
      if (json.success && json.data) {
        const c = json.data;
        setCustomerCode(String(c.CustomerCode || '').trim());
        setCustomerName(String(c.CustomerName || '').trim());
        setCustomerType(String(c.CustomerType || 'Customer Type').trim());
        setAddress1(String(c.Address1 || '').trim());
        setAddress2(String(c.Address2 || '').trim());
        setAddress3(String(c.Address3 || '').trim());
        setMemberCode('');
        showToast(`Customer: ${c.CustomerName || c.CustomerCode}`, 'success');
      } else {
        showToast(`Customer code ${cleanCode} does not exist.`, 'error');
      }
    } catch (err) {
      console.error(err);
      showToast("Failed to retrieve customer details.", 'error');
    } finally {
      setLoading(false);
    }
  };

  // Fetch member details by code
  const fetchMemberByCode = async (code: string) => {
    const cleanCode = code.trim();
    if (!cleanCode) {
      setIsCashMemberModalVisible(false);
      return;
    }

    try {
      setLoading(true);
      const res = await fetch(`${API_URL}/member/${encodeURIComponent(cleanCode)}`);
      const json = await res.json();
      if (json.success && json.data) {
        const m = json.data;
        setMemberCode(String(m.MemberCode || '').trim());
        setCustomerName(String(m.MemberName || '').trim());
        setCustomerType(String(m.MemberType || 'Member').trim());
        setAddress1(String(m.Address1 || '').trim());
        setAddress2(String(m.Address2 || '').trim());
        setAddress3(String(m.Address3 || '').trim());
        setIsCashMemberModalVisible(false);
        showToast(`Member: ${m.MemberName || m.MemberCode}`, 'success');
      } else {
        showToast("No such Member found!", 'error');
      }
    } catch (err) {
      console.error(err);
      showToast("Failed to retrieve member details.", 'error');
    } finally {
      setLoading(false);
    }
  };

  // Add Item to Grid with exact VB rounding & validation
  const handleAddGridItem = () => {
    if (!detailDescription.trim()) {
      showToast("Description cannot be blank!", 'error');
      return;
    }
    const wt = parseFloat(detailWeight) || 0;
    if (wt <= 0) {
      showToast("Weight must be greater than 0!", 'error');
      return;
    }
    const goldLossVal = parseFloat(detailGoldLoss) || 0;
    if (goldLossVal < 0) {
      showToast("Gold Loss is invalid!", 'error');
      return;
    }
    const cost = parseFloat(detailCostPerGram) || 0;
    if (cost <= 0) {
      showToast("Cost Per Gram is invalid!", 'error');
      return;
    }
    if (detailStoneWeight.trim() !== '') {
      const stoneWtVal = parseFloat(detailStoneWeight);
      if (isNaN(stoneWtVal) || stoneWtVal < 0) {
        showToast("Stone Weight is invalid!", 'error');
        return;
      }
    }

    // VB TotalCost rounding rule:
    const enteredCost = parseFloat(detailTotalCost) || ((wt + goldLossVal) * cost);
    const sCostFormatted = enteredCost.toFixed(2);
    const decimalPart = parseFloat(sCostFormatted.split('.')[1] || '0') / 100;
    let roundedTotalCost = enteredCost;
    if (decimalPart > 0 && decimalPart < 0.50) {
      roundedTotalCost = Math.round(enteredCost) - 1;
    } else if (decimalPart >= 0.50 && decimalPart <= 0.99) {
      roundedTotalCost = Math.round(enteredCost);
    }

    const newItem: GridItem = {
      InventoryCode: selectedGridIndex !== null ? gridItems[selectedGridIndex].InventoryCode : String(gridItems.length + 1),
      Description: detailDescription.toUpperCase(),
      Weight: wt,
      GoldLoss: goldLossVal,
      CostPerGram: cost,
      TotalCost: roundedTotalCost,
      DiamondDescription: detailDiamondDesc.toUpperCase(),
      StoneWeight: parseFloat(detailStoneWeight) || 0,
      ClassCode: "22K"
    };

    // Check market rate for admin authorization
    const marketRate = classRates["22K"] || 0;
    if (cost > marketRate && marketRate > 0) {
      setPendingGridItem(newItem);
      setAdminPassword('');
      setIsAdminPwdVisible(true);
      return;
    }

    commitGridItem(newItem);
    showToast(selectedGridIndex !== null ? "Item updated successfully" : "Item added to trade-in grid", 'success');
  };

  const commitGridItem = (item: GridItem) => {
    if (selectedGridIndex !== null) {
      const updated = [...gridItems];
      updated[selectedGridIndex] = item;
      setGridItems(updated);
      setSelectedGridIndex(null);
    } else {
      setGridItems([...gridItems, item]);
    }
    clearDetailSubform();
  };

  const handleVerifyAdminPwd = async () => {
    if (!adminPassword.trim()) {
      showToast("Please enter the admin password.", 'error');
      return;
    }
    try {
      setLoading(true);
      const res = await fetch(`${API_URL}/check-admin`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ password: adminPassword })
      });
      const json = await res.json();
      if (json.success) {
        setIsAdminPwdVisible(false);
        if (pendingGridItem) {
          commitGridItem(pendingGridItem);
          setPendingGridItem(null);
          showToast("Authorized by Admin", 'success');
        }
      } else {
        showToast(json.message || "SalesMan Has No Rights to change Cost!", 'error');
      }
    } catch (err) {
      console.error(err);
      showToast("Could not verify password with database.", 'error');
    } finally {
      setLoading(false);
    }
  };

  const handleSelectGridRow = (index: number) => {
    const item = gridItems[index];
    setSelectedGridIndex(index);
    setDetailDescription(item.Description);
    setDetailWeight(item.Weight.toString());
    setDetailGoldLoss(item.GoldLoss > 0 ? item.GoldLoss.toString() : '');
    setDetailCostPerGram(item.CostPerGram.toString());
    setDetailTotalCost(item.TotalCost.toFixed(2));
    setDetailDiamondDesc(item.DiamondDescription);
    setDetailStoneWeight(item.StoneWeight > 0 ? item.StoneWeight.toString() : '');
  };

  const handleClearGridRowSelection = () => {
    setSelectedGridIndex(null);
    clearDetailSubform();
  };

  const handleDeleteGridItem = () => {
    if (selectedGridIndex === null) {
      showToast("Select an item to delete.", 'error');
      return;
    }
    const updated = gridItems.filter((_, i) => i !== selectedGridIndex);
    const reindexed = updated.map((item, i) => ({
      ...item,
      InventoryCode: String(i + 1)
    }));
    setGridItems(reindexed);
    handleClearGridRowSelection();
    showToast("Item removed from list", 'success');
  };

  const clearDetailSubform = () => {
    setSelectedGridIndex(null);
    setDetailDescription('');
    setDetailWeight('');
    setDetailGoldLoss('');
    setDetailCostPerGram(costPerGram);
    setDetailTotalCost('0.00');
    setDetailDiamondDesc('');
    setDetailStoneWeight('');
  };

  // Open LOV modal
  const openLookupModal = async (type: 'customer' | 'member') => {
    setLookupSearch('');
    setLookupType(type);
    try {
      setLoading(true);
      if (type === 'customer') {
        const res = await fetch(`${API_BASE_URL}/customer?t=${Date.now()}`);
        const json = await res.json();
        if (json.success) setCustomersList(json.data);
      } else if (type === 'member') {
        const res = await fetch(`${API_URL}/members?t=${Date.now()}`);
        const json = await res.json();
        if (json.success) setMembersList(json.data);
      }
    } catch (err) {
      console.error(err);
      showToast(`Failed to load lookup items for ${type}`, 'error');
    } finally {
      setLoading(false);
    }
  };

  // Save Trade In Transaction
  const handleSaveTransaction = async () => {
    const custUpper = customerCode.trim().toUpperCase();
    if (!custUpper) {
      showToast("Please choose a Customer!", 'error');
      return;
    }

    if (custUpper === 'CASH' && companyShortCode.toUpperCase() !== 'GMT') {
      if (!memberCode.trim()) {
        showToast("Member Code is required for CASH customer!", 'error');
        setIsCashMemberModalVisible(true);
        return;
      }
    }

    if (gridItems.length === 0) {
      showToast("Please enter at least one trade-in item!", 'error');
      return;
    }

    if (totalTradeInAmount <= 0) {
      showToast("Trade-In Amount must have a value!", 'error');
      return;
    }

    try {
      setLoading(true);
      const payload = {
        refNo,
        inventoryCode: "TI-" + invoiceNo,
        customerCode,
        customerName,
        address1,
        address2,
        address3,
        costPerGram: parseFloat(costPerGram) || 0,
        remarks,
        salesmanCode,
        salesAmount: parseFloat(salesAmount) || 0,
        tradeInAmount: totalTradeInAmount,
        balanceAmount: parseFloat(salesAmount) - totalTradeInAmount,
        items: gridItems,
        userId: salesmanCode || "sys",
        isEdit: isEditMode
      };

      const res = await fetch(`${API_URL}/save`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload)
      });
      const json = await res.json();
      if (json.success) {
        showToast(`Transaction saved! Sales No: ${invoiceNo}`, 'success');
        handleClearAll();
      } else {
        showToast(json.message || "Transaction not saved", 'error');
      }
    } catch (err) {
      console.error(err);
      showToast("Network error: Failed to save transaction.", 'error');
    } finally {
      setLoading(false);
    }
  };

  const handleClearAll = () => {
    setIsEditMode(false);
    setCustomerCode('');
    setCustomerName('');
    setCustomerType('Customer Type');
    setAddress1('');
    setAddress2('');
    setAddress3('');
    setRemarks('');
    setMemberCode('');
    setGridItems([]);
    clearDetailSubform();
    fetchInitialData();
  };

  // Reprint handling
  const handleReprintSubmit = () => {
    const bill = reprintBillNo.trim();
    if (!bill) {
      showToast("Bill No is required for reprint!", 'error');
      return;
    }
    setIsReprintModalVisible(false);
    showToast(`Printing bill ${bill}...`, 'success');
  };

  // Verify Salesman Password
  const verifySalesmanPassword = async () => {
    if (!salesmanPassword.trim()) {
      showToast('SalesMan Password is Incorrect!', 'error');
      return;
    }
    setVerifyingSalesmanPassword(true);
    try {
      const response = await fetch(`${API_URL}/verifypassword`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ password: salesmanPassword.trim() })
      });
      const data = await response.json();
      if (data.success && data.salesman) {
        setSalesmanCode(data.salesman.SalesManCode || '');
        setSalesmanPasswordModalVisible(false);
        showToast(`Authorized: ${data.salesman.SalesManName || data.salesman.SalesManCode}`, 'success');
      } else {
        showToast(data.message || 'SalesMan Password is Incorrect!', 'error');
      }
    } catch (err: any) {
      showToast('Connection error', 'error');
    } finally {
      setVerifyingSalesmanPassword(false);
    }
  };

  const totalTradeInAmount = gridItems.reduce((acc, item) => acc + item.TotalCost, 0);

  const getFilteredCustomers = () => {
    const term = lookupSearch.toLowerCase();
    return customersList.filter(c =>
      (c.CustomerCode && c.CustomerCode.toLowerCase().includes(term)) ||
      (c.CustomerName && c.CustomerName.toLowerCase().includes(term))
    );
  };

  const getFilteredMembers = () => {
    const term = lookupSearch.toLowerCase();
    return membersList.filter(m =>
      (m.MemberCode && m.MemberCode.toLowerCase().includes(term)) ||
      (m.MemberName && m.MemberName.toLowerCase().includes(term))
    );
  };

  return (
    <SafeAreaView style={styles.safeArea} edges={['top']}>
      <Stack.Screen options={{ headerShown: false }} />

      {/* TOP LUXURY APP HEADER */}
      <View style={styles.topNavHeader}>
        <View style={styles.headerLeft}>
          <TouchableOpacity style={styles.backBtn} onPress={() => router.back()} activeOpacity={0.7}>
            <Ionicons name="arrow-back" size={18} color="#3B2D17" />
          </TouchableOpacity>
          <View>
            <ThemedText style={styles.headerTitle}>TRADE IN SALES</ThemedText>
            <ThemedText style={styles.headerSubtitle}>
              {companyShortCode ? `Ref: ${refNo} • ${companyShortCode}` : `Ref: ${refNo}`}
            </ThemedText>
          </View>
        </View>

        <View style={styles.headerRight}>
          <TouchableOpacity
            style={styles.headerActionBtn}
            onPress={() => setIsReprintModalVisible(true)}
            activeOpacity={0.7}
          >
            <Ionicons name="print-outline" size={15} color="#5C4A28" />
            <ThemedText style={styles.headerActionBtnText}>Reprint (F6)</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity
            style={[styles.headerActionBtn, styles.headerSaveBtn]}
            onPress={handleSaveTransaction}
            disabled={loading}
            activeOpacity={0.7}
          >
            <Ionicons name="checkmark-done" size={16} color="#9E6D08" />
            <ThemedText style={styles.headerSaveBtnText}>Update (F7)</ThemedText>
          </TouchableOpacity>
        </View>
      </View>

      <ScrollView contentContainerStyle={styles.container} keyboardShouldPersistTaps="handled">
        {/* TOP SECTION: Customer / Invoice Info Card & Financial Totals */}
        <View style={styles.topSection}>
          {/* Left: Customer & Invoice Card */}
          <View style={styles.customerCard}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerTitleRow}>
                <View style={styles.headerIconBadge}>
                  <Ionicons name="receipt-outline" size={14} color="#8D6409" />
                </View>
                <ThemedText style={styles.cardHeaderTitle}>INVOICE & CUSTOMER DETAILS</ThemedText>
              </View>
              {salesmanCode ? (
                <View style={styles.salesmanBadge}>
                  <Ionicons name="person" size={11} color="#8D6409" />
                  <ThemedText style={styles.salesmanBadgeText}>{salesmanCode}</ThemedText>
                </View>
              ) : null}
            </View>

            <View style={styles.cardBody}>
              {/* Single Combined Row: Invoice No, Cost/Gram, Loss(%), Customer, Name, Customer Type */}
              <View style={[styles.formRow, { alignItems: 'flex-end', flexWrap: 'wrap', gap: 10 }]}>
                <View style={styles.fieldGroup}>
                  <ThemedText style={styles.fieldLabel}>Invoice No.</ThemedText>
                  <TextInput
                    style={[styles.input, styles.readonlyInput, { width: 95 }]}
                    value={invoiceNo}
                    editable={false}
                  />
                </View>

                <View style={styles.fieldGroup}>
                  <ThemedText style={styles.fieldLabel}>Cost / Gram</ThemedText>
                  <TextInput
                    style={[styles.input, styles.numericInput, { width: 75 }]}
                    value={costPerGram}
                    keyboardType="numeric"
                    onChangeText={(val) => {
                      setCostPerGram(val);
                      setDetailCostPerGram(val);
                      handleDetailCalculations(detailWeight, detailGoldLoss, val);
                    }}
                  />
                </View>

                <View style={styles.fieldGroup}>
                  <ThemedText style={styles.fieldLabel}>Loss (%)</ThemedText>
                  <TextInput
                    style={[styles.input, styles.numericInput, { width: 65 }]}
                    value={lossPerc}
                    keyboardType="numeric"
                    onChangeText={(val) => {
                      setLossPerc(val);
                      const base = parseFloat(costPerGram) || 0;
                      const finalCost = base - (base * (parseFloat(val) || 0)) / 100;
                      setDetailCostPerGram(String(finalCost));
                      handleDetailCalculations(detailWeight, detailGoldLoss, String(finalCost));
                    }}
                  />
                </View>

                <View style={styles.fieldGroup}>
                  <ThemedText style={styles.fieldLabel}>Customer</ThemedText>
                  <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                    <TouchableOpacity
                      style={[styles.input, { minWidth: 110, justifyContent: 'center' }]}
                      onPress={() => {
                        setCustInputTemp(customerCode);
                        setIsCustInputModalVisible(true);
                      }}
                      activeOpacity={0.7}
                    >
                      <ThemedText style={styles.customerCodeText} numberOfLines={1}>
                        {customerCode || 'Select...'}
                      </ThemedText>
                    </TouchableOpacity>

                    <TouchableOpacity
                      style={styles.btnSearchDots}
                      onPress={() => openLookupModal('customer')}
                      activeOpacity={0.7}
                    >
                      <Ionicons name="search" size={13} color="#554428" />
                    </TouchableOpacity>
                  </View>
                </View>

                <View style={[styles.fieldGroup, { flex: 1, minWidth: 150 }]}>
                  <ThemedText style={styles.fieldLabel}>Name</ThemedText>
                  <TextInput
                    style={[styles.input, styles.readonlyInput, { width: '100%' }]}
                    value={customerName}
                    editable={false}
                    placeholder="Customer Name"
                    placeholderTextColor="#A89E8D"
                  />
                </View>

                <View style={{ alignItems: 'flex-end', justifyContent: 'flex-end' }}>
                  <View style={styles.customerTypeBadge}>
                    <Ionicons name="shield-checkmark-outline" size={12} color="#9E6D08" style={{ marginRight: 4 }} />
                    <ThemedText style={styles.customerTypeText}>{customerType}</ThemedText>
                  </View>
                </View>
              </View>

              {/* Address Row 1: Address Line 1 & Line 2 */}
              <View style={[styles.formRow, { marginTop: 6, gap: 8 }]}>
                <ThemedText style={styles.rowLabel}>Address</ThemedText>
                <TextInput
                  style={[styles.input, styles.readonlyInput, { flex: 1 }]}
                  value={address1}
                  editable={false}
                  placeholder="Street / Unit (Line 1)"
                  placeholderTextColor="#A89E8D"
                />
                <TextInput
                  style={[styles.input, styles.readonlyInput, { flex: 1 }]}
                  value={address2}
                  editable={false}
                  placeholder="Area / City (Line 2)"
                  placeholderTextColor="#A89E8D"
                />
              </View>

              {/* Address Row 2: New Button & Address Line 3 / Postal Code */}
              <View style={[styles.formRow, { marginTop: 6, gap: 8 }]}>
                <View style={{ width: 75 }}>
                  <TouchableOpacity
                    style={styles.btnNewCustomer}
                    onPress={() => router.push('/customer')}
                    activeOpacity={0.7}
                  >
                    <Ionicons name="person-add-outline" size={12} color="#8D6409" style={{ marginRight: 3 }} />
                    <ThemedText style={styles.btnNewCustomerText}>New</ThemedText>
                  </TouchableOpacity>
                </View>
                <TextInput
                  style={[styles.input, styles.readonlyInput, { flex: 1 }]}
                  value={address3}
                  editable={false}
                  placeholder="Postal Code / Country (Line 3)"
                  placeholderTextColor="#A89E8D"
                />
              </View>

              {/* Row 7: Remarks */}
              <View style={[styles.formRow, { marginTop: 6 }]}>
                <ThemedText style={styles.rowLabel}>Remarks</ThemedText>
                <TextInput
                  style={[styles.input, { flex: 1 }]}
                  value={remarks}
                  onChangeText={setRemarks}
                  placeholder="Additional notes..."
                  placeholderTextColor="#A89E8D"
                />
              </View>
            </View>
          </View>

          {/* Right: Premium Financials Summary Cards */}
          <View style={styles.rightFinancials}>
            {/* Sales Amount Card */}
            <View style={styles.financialCard}>
              <View style={styles.financialCardHeader}>
                <Ionicons name="cart-outline" size={13} color="#8D6409" />
                <ThemedText style={styles.financialCardTitle}>Sales Amount</ThemedText>
              </View>
              <View style={styles.financialValueBox}>
                <ThemedText style={styles.currencyPrefix}>$</ThemedText>
                <TextInput
                  style={styles.financialInput}
                  value={salesAmount}
                  keyboardType="numeric"
                  onChangeText={setSalesAmount}
                />
              </View>
            </View>

            {/* Trade In Amount Card */}
            <View style={[styles.financialCard, styles.tradeInCard]}>
              <View style={styles.financialCardHeader}>
                <Ionicons name="repeat-outline" size={13} color="#8D6409" />
                <ThemedText style={styles.financialCardTitle}>Trade In Amount</ThemedText>
              </View>
              <View style={styles.financialValueBox}>
                <ThemedText style={styles.currencyPrefix}>$</ThemedText>
                <ThemedText style={styles.financialValueText}>
                  {totalTradeInAmount.toFixed(2)}
                </ThemedText>
              </View>
            </View>

            {/* Quick Summary Pill */}
            <View style={styles.itemsSummaryBadge}>
              <ThemedText style={styles.itemsSummaryText}>
                Items: {gridItems.length}
              </ThemedText>
              <ThemedText style={styles.itemsSummarySubText}>
                Tot Wt: {gridItems.reduce((acc, it) => acc + (it.Weight || 0), 0).toFixed(2)}g
              </ThemedText>
            </View>
          </View>
        </View>

        {/* MIDDLE SECTION: Data Grid (grdTrade) */}
        <View style={styles.gridContainer}>
          <View style={styles.gridHeader}>
            <ThemedText style={[styles.gridHeaderText, { flex: 3.5 }]}>Description</ThemedText>
            <ThemedText style={[styles.gridHeaderText, { flex: 1.2, textAlign: 'right' }]}>Weight (g)</ThemedText>
            <ThemedText style={[styles.gridHeaderText, { flex: 1.2, textAlign: 'right' }]}>Gold Loss</ThemedText>
            <ThemedText style={[styles.gridHeaderText, { flex: 1.2, textAlign: 'right' }]}>Cost / Gm</ThemedText>
            <ThemedText style={[styles.gridHeaderText, { flex: 1.4, textAlign: 'right' }]}>Total Cost</ThemedText>
          </View>

          {/* Grid Body */}
          <View style={styles.gridBody}>
            {gridItems.length === 0 ? (
              <View style={styles.gridEmptyArea}>
                <Ionicons name="basket-outline" size={28} color="#C8BEAC" />
                <ThemedText style={styles.emptyGridText}>No trade-in items added yet</ThemedText>
                <ThemedText style={styles.emptyGridSubText}>Enter item details below and tap "Add"</ThemedText>
              </View>
            ) : (
              gridItems.map((item, index) => {
                const isSelected = selectedGridIndex === index;
                return (
                  <TouchableOpacity
                    key={index}
                    style={[
                      styles.gridRow,
                      index % 2 === 1 && styles.gridRowAlt,
                      isSelected && styles.gridRowSelected
                    ]}
                    activeOpacity={0.7}
                    onPress={() => handleSelectGridRow(index)}
                  >
                    <View style={{ flex: 3.5, flexDirection: 'row', alignItems: 'center', gap: 6 }}>
                      {isSelected && <View style={styles.selectedIndicatorDot} />}
                      <ThemedText style={[styles.gridCellText, { fontWeight: isSelected ? 'bold' : '600' }]} numberOfLines={1}>
                        {item.Description}
                      </ThemedText>
                    </View>
                    <ThemedText style={[styles.gridCellText, { flex: 1.2, textAlign: 'right' }]}>
                      {item.Weight.toFixed(2)}
                    </ThemedText>
                    <ThemedText style={[styles.gridCellText, { flex: 1.2, textAlign: 'right', color: '#7D5E19' }]}>
                      {item.GoldLoss.toFixed(2)}
                    </ThemedText>
                    <ThemedText style={[styles.gridCellText, { flex: 1.2, textAlign: 'right' }]}>
                      {item.CostPerGram.toFixed(2)}
                    </ThemedText>
                    <ThemedText style={[styles.gridCellText, styles.gridCellAmount, { flex: 1.4, textAlign: 'right' }]}>
                      ${item.TotalCost.toFixed(2)}
                    </ThemedText>
                  </TouchableOpacity>
                );
              })
            )}
          </View>
        </View>

        {/* LOWER SECTION: Entry Section & Input Boxes (Directly below Grid) */}
        <View style={styles.detailEntrySection}>
          <View style={styles.detailEntryBanner}>
            <View style={{ flexDirection: 'row', alignItems: 'center', gap: 6 }}>
              <Ionicons name="create-outline" size={14} color="#8D6409" />
              <ThemedText style={styles.detailEntryBannerTitle}>
                {selectedGridIndex !== null ? `EDIT ITEM #${selectedGridIndex + 1}` : 'ADD TRADE-IN ITEM'}
              </ThemedText>
            </View>
            {selectedGridIndex !== null && (
              <ThemedText style={styles.selectedItemPill}>Selected</ThemedText>
            )}
          </View>

          {/* Labels Row */}
          <View style={styles.entryHeaderLabelsRow}>
            <ThemedText style={[styles.entryColLabel, { flex: 3.5 }]}>Description</ThemedText>
            <ThemedText style={[styles.entryColLabel, { flex: 1.2, textAlign: 'right' }]}>Weight</ThemedText>
            <ThemedText style={[styles.entryColLabel, { flex: 1.2, textAlign: 'right' }]}>Gold Loss</ThemedText>
            <ThemedText style={[styles.entryColLabel, { flex: 1.2, textAlign: 'right' }]}>Cost / Gm</ThemedText>
            <ThemedText style={[styles.entryColLabel, { flex: 1.4, textAlign: 'right' }]}>Total Cost</ThemedText>
          </View>

          {/* Inputs Row 1: txtDescription, txtWeight, txtGoldLoss, txtCostPerGram1, txtTotalCost */}
          <View style={styles.inputsRow}>
            <TextInput
              style={[styles.inputBox, { flex: 3.5 }]}
              value={detailDescription}
              onChangeText={setDetailDescription}
              placeholder="Gold item description..."
              placeholderTextColor="#A89E8D"
            />
            <TextInput
              style={[styles.inputBox, styles.numericInput, { flex: 1.2 }]}
              value={detailWeight}
              keyboardType="numeric"
              placeholder="0.00"
              placeholderTextColor="#A89E8D"
              onChangeText={(val) => {
                setDetailWeight(val);
                handleDetailCalculations(val, detailGoldLoss, detailCostPerGram);
              }}
            />
            <TextInput
              style={[styles.inputBox, styles.numericInput, { flex: 1.2 }]}
              value={detailGoldLoss}
              keyboardType="numeric"
              placeholder="0.00"
              placeholderTextColor="#A89E8D"
              onChangeText={(val) => {
                setDetailGoldLoss(val);
                handleDetailCalculations(detailWeight, val, detailCostPerGram);
              }}
            />
            <TextInput
              style={[styles.inputBox, styles.numericInput, { flex: 1.2 }]}
              value={detailCostPerGram}
              keyboardType="numeric"
              placeholder="0.00"
              placeholderTextColor="#A89E8D"
              onChangeText={(val) => {
                setDetailCostPerGram(val);
                handleDetailCalculations(detailWeight, detailGoldLoss, val);
              }}
            />
            <TextInput
              style={[styles.inputBox, styles.numericInput, styles.totalCostInput, { flex: 1.4 }]}
              value={detailTotalCost}
              keyboardType="numeric"
              placeholder="0.00"
              placeholderTextColor="#A89E8D"
              onChangeText={handleDetailTotalCostChange}
            />
          </View>

          {/* Inputs Row 2: txtDiamondDescription, txtStoneWeight & Item Action Buttons */}
          <View style={[styles.inputsRow, { marginTop: 6, alignItems: 'center' }]}>
            <TextInput
              style={[styles.inputBox, { flex: 3.5 }]}
              value={detailDiamondDesc}
              onChangeText={setDetailDiamondDesc}
              placeholder="Diamond / stone notes (optional)"
              placeholderTextColor="#A89E8D"
            />
            <TextInput
              style={[styles.inputBox, styles.numericInput, { flex: 1.2 }]}
              value={detailStoneWeight}
              keyboardType="numeric"
              placeholder="Stone Wt"
              placeholderTextColor="#A89E8D"
              onChangeText={setDetailStoneWeight}
            />

            {/* Action Buttons: Add, Delete, Clear */}
            <View style={{ flex: 3.8, flexDirection: 'row', justifyContent: 'flex-end', gap: 6 }}>
              <TouchableOpacity
                style={[styles.itemActionBtn, styles.btnAddItem]}
                onPress={handleAddGridItem}
                activeOpacity={0.7}
              >
                <Ionicons name={selectedGridIndex !== null ? "save-outline" : "add"} size={14} color="#9E6D08" />
                <ThemedText style={styles.btnAddItemText}>
                  {selectedGridIndex !== null ? 'Update' : 'Add'}
                </ThemedText>
              </TouchableOpacity>

              <TouchableOpacity
                style={[styles.itemActionBtn, styles.btnDeleteItem]}
                onPress={handleDeleteGridItem}
                activeOpacity={0.7}
              >
                <Ionicons name="trash-outline" size={13} color="#C62828" />
                <ThemedText style={styles.btnDeleteItemText}>Delete</ThemedText>
              </TouchableOpacity>

              <TouchableOpacity
                style={[styles.itemActionBtn, styles.btnClearItem]}
                onPress={handleClearGridRowSelection}
                activeOpacity={0.7}
              >
                <Ionicons name="refresh-outline" size={13} color="#5C4A28" />
                <ThemedText style={styles.btnClearItemText}>Clear</ThemedText>
              </TouchableOpacity>
            </View>
          </View>
        </View>


      </ScrollView>

      {/* SALESMAN PASSWORD MODAL (Elegant Gold Card) */}
      <Modal visible={salesmanPasswordModalVisible} transparent animationType="fade">
        <View style={styles.modalOverlay}>
          <View style={styles.salesmanPwdCard}>
            <View style={styles.modalHeaderBanner}>
              <View style={styles.headerTitleRow}>
                <View style={styles.headerIconBadge}>
                  <Ionicons name="key-outline" size={14} color="#8D6409" />
                </View>
                <ThemedText style={styles.modalHeaderTitle}>SALESMAN AUTHENTICATION</ThemedText>
              </View>
            </View>
            <View style={{ padding: 16, alignItems: 'center' }}>
              <ThemedText style={styles.salesmanPwdSubtitle}>
                Please enter your Salesman Password to access Trade-In Sales
              </ThemedText>
              <TextInput
                style={styles.salesmanPwdInput}
                value={salesmanPassword}
                onChangeText={setSalesmanPassword}
                secureTextEntry={true}
                autoFocus={true}
                placeholder="Enter password..."
                placeholderTextColor="#A89E8D"
                onSubmitEditing={verifySalesmanPassword}
              />
              <View style={{ flexDirection: 'row', gap: 10, marginTop: 14, width: '100%' }}>
                <TouchableOpacity
                  style={[styles.modalActionBtn, styles.modalBtnCancel]}
                  onPress={() => router.back()}
                  activeOpacity={0.7}
                >
                  <ThemedText style={styles.modalBtnCancelText}>Cancel</ThemedText>
                </TouchableOpacity>
                <TouchableOpacity
                  style={[styles.modalActionBtn, styles.modalBtnPrimary]}
                  onPress={verifySalesmanPassword}
                  disabled={verifyingSalesmanPassword}
                  activeOpacity={0.7}
                >
                  {verifyingSalesmanPassword ? (
                    <ActivityIndicator size="small" color="#9E6D08" />
                  ) : (
                    <ThemedText style={styles.modalBtnPrimaryText}>Authorize</ThemedText>
                  )}
                </TouchableOpacity>
              </View>
            </View>
          </View>
        </View>
      </Modal>

      {/* Customer Code Direct Input Modal */}
      <Modal visible={isCustInputModalVisible} transparent animationType="fade">
        <TouchableOpacity style={styles.modalOverlay} activeOpacity={1}>
          <TouchableWithoutFeedback>
            <View style={[styles.lookupCard, { maxWidth: 340 }]}>
              <View style={styles.modalHeaderBanner}>
                <View style={styles.headerTitleRow}>
                  <View style={styles.headerIconBadge}>
                    <Ionicons name="person-outline" size={14} color="#8D6409" />
                  </View>
                  <ThemedText style={styles.modalHeaderTitle}>ENTER CUSTOMER CODE</ThemedText>
                </View>
              </View>
              <View style={{ padding: 16 }}>
                <TextInput
                  style={[styles.input, { width: '100%', marginBottom: 14, height: 34 }]}
                  value={custInputTemp}
                  onChangeText={setCustInputTemp}
                  autoCapitalize="characters"
                  placeholder="e.g. CASH or Member ID"
                  placeholderTextColor="#A89E8D"
                  autoFocus
                  onSubmitEditing={() => {
                    setIsCustInputModalVisible(false);
                    fetchCustomerByCode(custInputTemp);
                  }}
                />
                <View style={{ flexDirection: 'row', gap: 10 }}>
                  <TouchableOpacity
                    style={[styles.modalActionBtn, styles.modalBtnCancel]}
                    onPress={() => setIsCustInputModalVisible(false)}
                    activeOpacity={0.7}
                  >
                    <ThemedText style={styles.modalBtnCancelText}>Cancel</ThemedText>
                  </TouchableOpacity>
                  <TouchableOpacity
                    style={[styles.modalActionBtn, styles.modalBtnPrimary]}
                    onPress={() => {
                      setIsCustInputModalVisible(false);
                      fetchCustomerByCode(custInputTemp);
                    }}
                    activeOpacity={0.7}
                  >
                    <ThemedText style={styles.modalBtnPrimaryText}>Apply</ThemedText>
                  </TouchableOpacity>
                </View>
              </View>
            </View>
          </TouchableWithoutFeedback>
        </TouchableOpacity>
      </Modal>

      {/* Cash Member Modal (pnlMember) */}
      <Modal visible={isCashMemberModalVisible} transparent animationType="fade">
        <TouchableOpacity style={styles.modalOverlay} activeOpacity={1}>
          <TouchableWithoutFeedback>
            <View style={[styles.lookupCard, { maxWidth: 350 }]}>
              <View style={styles.modalHeaderBanner}>
                <View style={styles.headerTitleRow}>
                  <View style={styles.headerIconBadge}>
                    <Ionicons name="card-outline" size={14} color="#8D6409" />
                  </View>
                  <ThemedText style={styles.modalHeaderTitle}>MEMBER CODE (CASH CUSTOMER)</ThemedText>
                </View>
              </View>
              <View style={{ padding: 16 }}>
                <View style={{ flexDirection: 'row', alignItems: 'center', marginBottom: 14 }}>
                  <TextInput
                    style={[styles.input, { flex: 1, height: 34 }]}
                    value={cashMemberCodeInput}
                    onChangeText={setCashMemberCodeInput}
                    placeholder="Enter member code..."
                    placeholderTextColor="#A89E8D"
                    autoFocus
                    onSubmitEditing={() => fetchMemberByCode(cashMemberCodeInput)}
                  />
                  <TouchableOpacity
                    style={[styles.btnSearchDots, { height: 34, width: 34 }]}
                    onPress={() => {
                      setIsCashMemberModalVisible(false);
                      openLookupModal('member');
                    }}
                    activeOpacity={0.7}
                  >
                    <Ionicons name="search" size={14} color="#554428" />
                  </TouchableOpacity>
                </View>
                <View style={{ flexDirection: 'row', gap: 10 }}>
                  <TouchableOpacity
                    style={[styles.modalActionBtn, styles.modalBtnCancel]}
                    onPress={() => setIsCashMemberModalVisible(false)}
                    activeOpacity={0.7}
                  >
                    <ThemedText style={styles.modalBtnCancelText}>Cancel</ThemedText>
                  </TouchableOpacity>
                  <TouchableOpacity
                    style={[styles.modalActionBtn, styles.modalBtnPrimary]}
                    onPress={() => fetchMemberByCode(cashMemberCodeInput)}
                    activeOpacity={0.7}
                  >
                    <ThemedText style={styles.modalBtnPrimaryText}>Confirm</ThemedText>
                  </TouchableOpacity>
                </View>
              </View>
            </View>
          </TouchableWithoutFeedback>
        </TouchableOpacity>
      </Modal>

      {/* Reprint Bill Modal (PnlReprint) */}
      <Modal visible={isReprintModalVisible} transparent animationType="fade">
        <TouchableOpacity style={styles.modalOverlay} activeOpacity={1}>
          <TouchableWithoutFeedback>
            <View style={[styles.lookupCard, { maxWidth: 330 }]}>
              <View style={styles.modalHeaderBanner}>
                <View style={styles.headerTitleRow}>
                  <View style={styles.headerIconBadge}>
                    <Ionicons name="print-outline" size={14} color="#8D6409" />
                  </View>
                  <ThemedText style={styles.modalHeaderTitle}>REPRINT TRADE-IN BILL</ThemedText>
                </View>
              </View>
              <View style={{ padding: 16 }}>
                <ThemedText style={styles.fieldLabel}>Bill Number</ThemedText>
                <TextInput
                  style={[styles.input, { width: '100%', marginBottom: 14, height: 34 }]}
                  value={reprintBillNo}
                  onChangeText={setReprintBillNo}
                  placeholder="e.g. 00000001"
                  placeholderTextColor="#A89E8D"
                  autoFocus
                  onSubmitEditing={handleReprintSubmit}
                />
                <View style={{ flexDirection: 'row', gap: 10 }}>
                  <TouchableOpacity
                    style={[styles.modalActionBtn, styles.modalBtnCancel]}
                    onPress={() => {
                      setIsReprintModalVisible(false);
                      setReprintBillNo('');
                    }}
                    activeOpacity={0.7}
                  >
                    <ThemedText style={styles.modalBtnCancelText}>Cancel</ThemedText>
                  </TouchableOpacity>
                  <TouchableOpacity
                    style={[styles.modalActionBtn, styles.modalBtnPrimary]}
                    onPress={handleReprintSubmit}
                    activeOpacity={0.7}
                  >
                    <ThemedText style={styles.modalBtnPrimaryText}>Load Bill</ThemedText>
                  </TouchableOpacity>
                </View>
              </View>
            </View>
          </TouchableWithoutFeedback>
        </TouchableOpacity>
      </Modal>

      {/* Admin Cost Password Authorization Modal */}
      <Modal visible={isAdminPwdVisible} transparent animationType="fade">
        <TouchableOpacity style={styles.modalOverlay} activeOpacity={1}>
          <TouchableWithoutFeedback>
            <View style={[styles.lookupCard, { maxWidth: 350 }]}>
              <View style={styles.modalHeaderBanner}>
                <View style={styles.headerTitleRow}>
                  <View style={styles.headerIconBadge}>
                    <Ionicons name="shield-outline" size={14} color="#8D6409" />
                  </View>
                  <ThemedText style={styles.modalHeaderTitle}>SUPER ADMIN OVERRIDE</ThemedText>
                </View>
              </View>
              <View style={{ padding: 16, alignItems: 'center' }}>
                <ThemedText style={styles.salesmanPwdSubtitle}>
                  Cost exceeds market rate. Super Admin authorization required:
                </ThemedText>
                <TextInput
                  style={[styles.input, { width: '100%', textAlign: 'center', marginBottom: 14, height: 34 }]}
                  secureTextEntry={true}
                  value={adminPassword}
                  onChangeText={setAdminPassword}
                  placeholder="Admin Password"
                  placeholderTextColor="#A89E8D"
                  autoFocus
                  onSubmitEditing={handleVerifyAdminPwd}
                />
                <View style={{ flexDirection: 'row', gap: 10, width: '100%' }}>
                  <TouchableOpacity
                    style={[styles.modalActionBtn, styles.modalBtnCancel]}
                    onPress={() => {
                      setIsAdminPwdVisible(false);
                      setPendingGridItem(null);
                    }}
                    activeOpacity={0.7}
                  >
                    <ThemedText style={styles.modalBtnCancelText}>Cancel</ThemedText>
                  </TouchableOpacity>
                  <TouchableOpacity
                    style={[styles.modalActionBtn, styles.modalBtnPrimary]}
                    onPress={handleVerifyAdminPwd}
                    activeOpacity={0.7}
                  >
                    <ThemedText style={styles.modalBtnPrimaryText}>Authorize</ThemedText>
                  </TouchableOpacity>
                </View>
              </View>
            </View>
          </TouchableWithoutFeedback>
        </TouchableOpacity>
      </Modal>

      {/* Lookup Selection Modal (Customer / Member) */}
      <Modal visible={lookupType !== null} transparent animationType="fade">
        <TouchableOpacity style={styles.modalOverlay} activeOpacity={1} onPress={() => setLookupType(null)}>
          <TouchableWithoutFeedback>
            <View style={styles.lookupCard}>
              <View style={styles.modalHeaderBanner}>
                <View style={styles.headerTitleRow}>
                  <View style={styles.headerIconBadge}>
                    <Ionicons name="search-outline" size={14} color="#8D6409" />
                  </View>
                  <ThemedText style={styles.modalHeaderTitle}>
                    {lookupType === 'customer' ? 'SELECT CUSTOMER' : 'SELECT MEMBER'}
                  </ThemedText>
                </View>
                <TouchableOpacity onPress={() => setLookupType(null)} activeOpacity={0.7}>
                  <Ionicons name="close" size={18} color="#554428" />
                </TouchableOpacity>
              </View>

              <View style={styles.lookupSearchRow}>
                <Ionicons name="search" size={14} color="#8D6409" style={{ marginRight: 6 }} />
                <TextInput
                  style={styles.lookupSearchInput}
                  placeholder="Filter by name or code..."
                  placeholderTextColor="#A89E8D"
                  value={lookupSearch}
                  onChangeText={setLookupSearch}
                  autoFocus
                />
              </View>

              <View style={{ maxHeight: 320, minHeight: 140 }}>
                <FlatList
                  data={lookupType === 'customer' ? getFilteredCustomers() : getFilteredMembers()}
                  keyExtractor={(_, index) => index.toString()}
                  renderItem={({ item, index }) => (
                    <TouchableOpacity
                      style={[
                        styles.lovItem,
                        index % 2 === 1 && { backgroundColor: '#FCFAF6' },
                      ]}
                      activeOpacity={0.7}
                      onPress={() => {
                        if (lookupType === 'customer') {
                          const code = String(item.CustomerCode || '').trim();
                          setCustomerCode(code);
                          setCustomerName(String(item.CustomerName || '').trim());
                          setCustomerType(String(item.CustomerType || 'Customer Type').trim());
                          setAddress1(String(item.Address1 || '').trim());
                          setAddress2(String(item.Address2 || '').trim());
                          setAddress3(String(item.Address3 || '').trim());
                          setLookupType(null);
                          if (code.toUpperCase() === 'CASH' && companyShortCode.toUpperCase() !== 'GMT') {
                            setCashMemberCodeInput('');
                            setIsCashMemberModalVisible(true);
                          }
                        } else if (lookupType === 'member') {
                          setMemberCode(String(item.MemberCode || '').trim());
                          setCustomerName(String(item.MemberName || '').trim());
                          setCustomerType(String(item.MemberType || 'Member').trim());
                          setAddress1(String(item.Address1 || '').trim());
                          setAddress2(String(item.Address2 || '').trim());
                          setAddress3(String(item.Address3 || '').trim());
                          setLookupType(null);
                        }
                      }}
                    >
                      <View style={{ flexDirection: 'row', alignItems: 'center', justifyContent: 'space-between' }}>
                        <ThemedText style={styles.lovItemCode}>
                          {lookupType === 'customer' ? item.CustomerCode : item.MemberCode}
                        </ThemedText>
                        <ThemedText style={styles.lovItemType}>
                          {lookupType === 'customer' ? (item.CustomerType || 'Customer') : (item.MemberType || 'Member')}
                        </ThemedText>
                      </View>
                      <ThemedText style={styles.lovItemName} numberOfLines={1}>
                        {lookupType === 'customer' ? item.CustomerName : item.MemberName}
                      </ThemedText>
                      {(item.Address1 || item.Address2) ? (
                        <ThemedText style={styles.lovItemSub} numberOfLines={1}>
                          {[item.Address1, item.Address2].filter(Boolean).join(', ')}
                        </ThemedText>
                      ) : null}
                    </TouchableOpacity>
                  )}
                />
              </View>
            </View>
          </TouchableWithoutFeedback>
        </TouchableOpacity>
      </Modal>

      {/* Loading Indicator */}
      {loading && (
        <View style={styles.loadingOverlay}>
          <ActivityIndicator size="large" color="#B8903B" />
        </View>
      )}

      {/* Toast Notification */}
      {toast && (
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
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  safeArea: {
    flex: 1,
    backgroundColor: '#FBF9F5',
  },
  container: {
    padding: 10,
    paddingBottom: 24,
  },

  // ===== TOP NAV HEADER =====
  topNavHeader: {
    height: 48,
    backgroundColor: '#FFFFFF',
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingHorizontal: 12,
    borderBottomWidth: 1,
    borderBottomColor: '#EBE2D2',
    elevation: 2,
    shadowColor: '#A38035',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.08,
    shadowRadius: 3,
  },
  headerLeft: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 10,
  },
  backBtn: {
    padding: 6,
    borderRadius: 6,
    backgroundColor: '#FAF5EA',
    borderWidth: 1,
    borderColor: '#E8DCB8',
  },
  headerTitle: {
    fontSize: 15,
    fontWeight: 'bold',
    color: '#3B2D17',
    letterSpacing: 0.5,
  },
  headerSubtitle: {
    fontSize: 10,
    color: '#8D6409',
    fontWeight: '600',
  },
  headerRight: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 8,
  },
  headerActionBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 10,
    paddingVertical: 5,
    borderRadius: 6,
    backgroundColor: '#FAF5EA',
    borderWidth: 1,
    borderColor: '#DFD5C3',
    gap: 4,
  },
  headerActionBtnText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#5C4A28',
  },
  headerSaveBtn: {
    backgroundColor: '#F9F2E2',
    borderColor: '#B8903B',
  },
  headerSaveBtnText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#9E6D08',
  },

  // ===== TOP SECTION (Customer Card + Financial Totals) =====
  topSection: {
    flexDirection: 'row',
    gap: 10,
  },
  customerCard: {
    flex: 1,
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    shadowColor: '#A38035',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.06,
    shadowRadius: 4,
    elevation: 2,
    overflow: 'hidden',
  },
  cardHeaderBanner: {
    backgroundColor: '#F5E7CA',
    paddingHorizontal: 10,
    paddingVertical: 6,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
  },
  headerTitleRow: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 6,
  },
  headerIconBadge: {
    width: 20,
    height: 20,
    borderRadius: 4,
    backgroundColor: '#FFF8EA',
    borderWidth: 1,
    borderColor: '#E2D2AF',
    alignItems: 'center',
    justifyContent: 'center',
  },
  cardHeaderTitle: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3B2D17',
    letterSpacing: 0.4,
  },
  salesmanBadge: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFF8EA',
    paddingHorizontal: 6,
    paddingVertical: 2,
    borderRadius: 4,
    borderWidth: 1,
    borderColor: '#E2D2AF',
    gap: 3,
  },
  salesmanBadgeText: {
    fontSize: 10,
    fontWeight: 'bold',
    color: '#8D6409',
  },
  cardBody: {
    padding: 10,
  },
  formRow: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  fieldGroup: {
    flexDirection: 'column',
  },
  fieldLabel: {
    fontSize: 10,
    fontWeight: '600',
    color: '#6E5528',
    marginBottom: 3,
  },
  rowLabel: {
    fontSize: 11,
    fontWeight: '600',
    color: '#554428',
    width: 75,
  },
  input: {
    height: 28,
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#D8CDBA',
    borderRadius: 5,
    paddingHorizontal: 8,
    paddingVertical: 0,
    fontSize: 11,
    color: '#3B2D17',
    fontWeight: '500',
  },
  readonlyInput: {
    backgroundColor: '#FAF7F0',
    borderColor: '#E8DEC8',
    color: '#554428',
  },
  numericInput: {
    textAlign: 'right',
  },
  customerCodeText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#8D6409',
  },
  btnSearchDots: {
    width: 28,
    height: 28,
    backgroundColor: '#F5EBD4',
    borderWidth: 1,
    borderLeftWidth: 0,
    borderColor: '#D8CDBA',
    borderTopRightRadius: 5,
    borderBottomRightRadius: 5,
    alignItems: 'center',
    justifyContent: 'center',
  },
  customerTypeBadge: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFF8EA',
    borderWidth: 1,
    borderColor: '#D8C59A',
    borderRadius: 6,
    paddingHorizontal: 8,
    paddingVertical: 4,
  },
  customerTypeText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#9E6D08',
  },
  btnNewCustomer: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    width: 66,
    height: 28,
    backgroundColor: '#FAF5EA',
    borderWidth: 1,
    borderColor: '#D8C59A',
    borderRadius: 5,
  },
  btnNewCustomerText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#8D6409',
  },

  // ===== RIGHT FINANCIALS =====
  rightFinancials: {
    width: 175,
    gap: 8,
  },
  financialCard: {
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    shadowColor: '#A38035',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.05,
    shadowRadius: 3,
    elevation: 2,
    overflow: 'hidden',
  },
  tradeInCard: {
    borderColor: '#B8903B',
    borderWidth: 1.5,
  },
  financialCardHeader: {
    backgroundColor: '#F5E7CA',
    paddingHorizontal: 8,
    paddingVertical: 5,
    flexDirection: 'row',
    alignItems: 'center',
    gap: 5,
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
  },
  financialCardTitle: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3B2D17',
    letterSpacing: 0.3,
  },
  financialValueBox: {
    backgroundColor: '#FFFDF9',
    paddingHorizontal: 10,
    paddingVertical: 6,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'flex-end',
    minHeight: 36,
  },
  currencyPrefix: {
    fontSize: 13,
    fontWeight: 'bold',
    color: '#8D6409',
    marginRight: 4,
  },
  financialValueText: {
    fontSize: 16,
    fontWeight: 'bold',
    color: '#9E6D08',
  },
  financialInput: {
    fontSize: 15,
    fontWeight: 'bold',
    color: '#3B2D17',
    textAlign: 'right',
    padding: 0,
    minWidth: 70,
  },
  itemsSummaryBadge: {
    backgroundColor: '#FAF5EA',
    borderRadius: 6,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    paddingHorizontal: 8,
    paddingVertical: 5,
    alignItems: 'center',
  },
  itemsSummaryText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#5C4A28',
  },
  itemsSummarySubText: {
    fontSize: 10,
    color: '#8D6409',
    marginTop: 1,
  },

  // ===== MIDDLE SECTION: DATA GRID (grdTrade) =====
  gridContainer: {
    marginTop: 10,
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    shadowColor: '#A38035',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.05,
    shadowRadius: 3,
    elevation: 2,
    overflow: 'hidden',
  },
  gridHeader: {
    flexDirection: 'row',
    backgroundColor: '#F5E7CA',
    paddingVertical: 8,
    paddingHorizontal: 10,
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
  },
  gridHeaderText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3B2D17',
    letterSpacing: 0.3,
  },
  gridBody: {
    minHeight: 160,
    backgroundColor: '#FFFFFF',
  },
  gridEmptyArea: {
    minHeight: 160,
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: '#FCFAF6',
    padding: 20,
    gap: 4,
  },
  emptyGridText: {
    fontSize: 12,
    fontWeight: '600',
    color: '#8D6409',
    marginTop: 4,
  },
  emptyGridSubText: {
    fontSize: 10,
    color: '#A89E8D',
  },
  gridRow: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingVertical: 8,
    paddingHorizontal: 10,
    borderBottomWidth: 1,
    borderBottomColor: '#F3EBDD',
    backgroundColor: '#FFFFFF',
  },
  gridRowAlt: {
    backgroundColor: '#FDFCFA',
  },
  gridRowSelected: {
    backgroundColor: '#FFF6DF',
    borderLeftWidth: 3,
    borderLeftColor: '#B8903B',
  },
  selectedIndicatorDot: {
    width: 6,
    height: 6,
    borderRadius: 3,
    backgroundColor: '#B8903B',
  },
  gridCellText: {
    fontSize: 11,
    color: '#3B2D17',
  },
  gridCellAmount: {
    fontWeight: 'bold',
    color: '#9E6D08',
  },

  // ===== LOWER SECTION: DETAIL ENTRY SECTION =====
  detailEntrySection: {
    marginTop: 10,
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    shadowColor: '#A38035',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.05,
    shadowRadius: 3,
    elevation: 2,
    overflow: 'hidden',
    paddingBottom: 8,
  },
  detailEntryBanner: {
    backgroundColor: '#F7ECCF',
    paddingHorizontal: 10,
    paddingVertical: 6,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    borderBottomWidth: 1,
    borderBottomColor: '#E6D7B4',
  },
  detailEntryBannerTitle: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3B2D17',
    letterSpacing: 0.3,
  },
  selectedItemPill: {
    fontSize: 10,
    fontWeight: 'bold',
    color: '#9E6D08',
    backgroundColor: '#FFF8EA',
    paddingHorizontal: 6,
    paddingVertical: 2,
    borderRadius: 4,
    borderWidth: 1,
    borderColor: '#E2D2AF',
  },
  entryHeaderLabelsRow: {
    flexDirection: 'row',
    paddingHorizontal: 8,
    paddingTop: 6,
    paddingBottom: 2,
  },
  entryColLabel: {
    fontSize: 10,
    fontWeight: '600',
    color: '#6E5528',
  },
  inputsRow: {
    flexDirection: 'row',
    paddingHorizontal: 8,
    gap: 4,
  },
  inputBox: {
    height: 28,
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#D8CDBA',
    borderRadius: 5,
    paddingHorizontal: 8,
    paddingVertical: 0,
    fontSize: 11,
    color: '#3B2D17',
    fontWeight: '500',
  },
  totalCostInput: {
    fontWeight: 'bold',
    color: '#9E6D08',
    backgroundColor: '#FFFDF7',
    borderColor: '#B8903B',
  },
  itemActionBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    height: 28,
    paddingHorizontal: 10,
    borderRadius: 5,
    borderWidth: 1,
    gap: 4,
  },
  btnAddItem: {
    backgroundColor: '#FFF8E7',
    borderColor: '#B8903B',
  },
  btnAddItemText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#8D6409',
  },
  btnDeleteItem: {
    backgroundColor: '#FFF5F5',
    borderColor: '#F2A8A2',
  },
  btnDeleteItemText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#C62828',
  },
  btnClearItem: {
    backgroundColor: '#FFFFFF',
    borderColor: '#DFD5C3',
  },
  btnClearItemText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#5C4A28',
  },

  // ===== BOTTOM ACTION BUTTONS =====
  bottomBar: {
    flexDirection: 'row',
    justifyContent: 'flex-end',
    gap: 8,
    marginTop: 14,
    paddingBottom: 10,
  },
  bottomActionBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    minWidth: 105,
    height: 32,
    borderRadius: 6,
    borderWidth: 1,
    paddingHorizontal: 12,
    gap: 5,
  },
  bottomBtnPrimary: {
    backgroundColor: '#F9F2E2',
    borderColor: '#B8903B',
  },
  bottomBtnPrimaryText: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#9E6D08',
  },
  bottomBtnSecondary: {
    backgroundColor: '#FFFFFF',
    borderColor: '#DFD5C3',
  },
  bottomBtnSecondaryText: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#5C4A28',
  },
  bottomBtnClose: {
    backgroundColor: '#FAF5EA',
    borderColor: '#D8CDBA',
  },
  bottomBtnCloseText: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#554428',
  },

  // ===== MODALS & OVERLAYS =====
  modalOverlay: {
    flex: 1,
    backgroundColor: 'rgba(30, 20, 10, 0.45)',
    justifyContent: 'center',
    alignItems: 'center',
    padding: 16,
  },
  salesmanPwdCard: {
    width: 290,
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    overflow: 'hidden',
    shadowColor: '#A38035',
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.15,
    shadowRadius: 8,
    elevation: 8,
  },
  modalHeaderBanner: {
    backgroundColor: '#F5E7CA',
    paddingHorizontal: 12,
    paddingVertical: 9,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
  },
  modalHeaderTitle: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3B2D17',
    letterSpacing: 0.4,
  },
  salesmanPwdSubtitle: {
    fontSize: 11,
    color: '#6E5528',
    textAlign: 'center',
    marginBottom: 12,
    lineHeight: 16,
  },
  salesmanPwdInput: {
    width: '100%',
    height: 34,
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#D8CDBA',
    borderRadius: 5,
    textAlign: 'center',
    fontSize: 13,
    fontWeight: 'bold',
    color: '#3B2D17',
    paddingHorizontal: 10,
  },
  modalActionBtn: {
    flex: 1,
    height: 32,
    borderRadius: 5,
    alignItems: 'center',
    justifyContent: 'center',
    borderWidth: 1,
  },
  modalBtnPrimary: {
    backgroundColor: '#F9F2E2',
    borderColor: '#B8903B',
  },
  modalBtnPrimaryText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#9E6D08',
  },
  modalBtnCancel: {
    backgroundColor: '#FFFFFF',
    borderColor: '#DFD5C3',
  },
  modalBtnCancelText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#5C4A28',
  },
  lookupCard: {
    width: '100%',
    maxWidth: 420,
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    overflow: 'hidden',
    shadowColor: '#A38035',
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.15,
    shadowRadius: 8,
    elevation: 8,
  },
  lookupSearchRow: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 10,
    paddingVertical: 6,
    borderBottomWidth: 1,
    borderBottomColor: '#F0E6D4',
    backgroundColor: '#FCFAF6',
  },
  lookupSearchInput: {
    flex: 1,
    height: 28,
    fontSize: 11,
    color: '#3B2D17',
    padding: 0,
  },
  lovItem: {
    paddingVertical: 8,
    paddingHorizontal: 12,
    borderBottomWidth: 1,
    borderBottomColor: '#F5EFE4',
  },
  lovItemCode: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#8D6409',
  },
  lovItemType: {
    fontSize: 10,
    fontWeight: 'bold',
    color: '#9E6D08',
  },
  lovItemName: {
    fontSize: 12,
    fontWeight: '600',
    color: '#3B2D17',
    marginTop: 2,
  },
  lovItemSub: {
    fontSize: 10,
    color: '#8F816E',
    marginTop: 1,
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
    shadowOpacity: 0.2,
    shadowRadius: 4,
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
    fontSize: 11,
  },
});
