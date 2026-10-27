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

export default function PosScreen() {
  const router = useRouter();

  // ===== CUSTOM CHECKBOX & RADIO =====
  const Checkbox = ({ label, selected, onPress }: { label: string; selected: boolean; onPress: () => void }) => (
    <TouchableOpacity style={styles.radioContainer} onPress={onPress} activeOpacity={0.7}>
      <View style={[styles.checkboxSquare, selected && styles.checkboxSquareSelected]}>
        {selected && <Ionicons name="checkmark" size={13} color="#FFFFFF" />}
      </View>
      <ThemedText style={styles.radioLabel}>{label}</ThemedText>
    </TouchableOpacity>
  );

  const RadioButton = ({ label, selected, onPress, disabled }: { label: string; selected: boolean; onPress: () => void; disabled?: boolean }) => (
    <TouchableOpacity
      style={[styles.radioContainer, disabled && { opacity: 0.5 }]}
      onPress={disabled ? undefined : onPress}
      activeOpacity={disabled ? 1 : 0.7}
      disabled={disabled}
    >
      <View style={[styles.radioCircle, selected && styles.radioCircleSelected]}>
        {selected && <View style={styles.radioInner} />}
      </View>
      <ThemedText style={styles.radioLabel}>{label}</ThemedText>
    </TouchableOpacity>
  );

  // ===== STATE =====
  const [headerState, setHeaderState] = useState({
    InvoiceNo: '',
    InvoiceDate: new Date().toISOString().split('T')[0],
    SalesManCode: '',
    CustomerCode: 'CASH',
    CustomerName: 'CASH CUSTOMER',
    CustomerAddress: '',
    PassportNo: '',
    CustomerType: 'N', // Normal
    MemberCode: '',
    TaxMode: 'Inclusive', // Inclusive, Exclusive, Zero Rated
    LocationCode: 'HQ',
    Remarks: '',
    Advance: '',
    FlexiAmount: '',
    FlexiBonus: '',
    eTRS: 'Not For eTRS',
    PrintMC: false,
    PrintImage: false,
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
    PaymentType: 'Cash Sales',
    TradeIn: false,
    TotalQty: '0',
    TotalSalesAmt: '0',
    NetDiscount: '0',
    NetValue: '0',
    Tax: '0',
    GrandTotal: '0',
    ReturnAmt: '0',
    PayMode: 'Cash',
  });

  const [loading, setLoading] = useState(false);
  const [toast, setToast] = useState<{ visible: boolean; message: string; type: 'success' | 'error' | 'info' }>({
    visible: false,
    message: '',
    type: 'success'
  });
  const [selectedIndex, setSelectedIndex] = useState<number | null>(null);
  const [lineDetailModalVisible, setLineDetailModalVisible] = useState(false);
  const [classRates, setClassRates] = useState<any[]>([]);

  // Password Verification State
  const [passwordModalVisible, setPasswordModalVisible] = useState(true);
  const [salesmanPassword, setSalesmanPassword] = useState('');
  const [verifyingPassword, setVerifyingPassword] = useState(false);

  const verifySalesmanPassword = async () => {
    if (!salesmanPassword) {
      showToast('Validation Error: Please enter a password', 'error');
      return;
    }
    setVerifyingPassword(true);
    try {
      const response = await fetch(`${API_URL}/salesreturn/verifypassword`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ password: salesmanPassword })
      });
      const data = await response.json();
      if (data.success) {
        updateHeader('SalesManCode', data.salesman.SalesManName || data.salesman.SalesManCode);
        setPasswordModalVisible(false);
      } else {
        showToast(data.message || 'Invalid password', 'error');
      }
    } catch (err: any) {
      showToast(err.message, 'error');
    } finally {
      setVerifyingPassword(false);
    }
  };

  // Fetch Class Rates on load
  useEffect(() => {
    fetch(`${API_URL}/pos/lov/market-rates`)
      .then(res => res.json())
      .then(json => {
        if (json.success) setClassRates(json.data);
      })
      .catch(e => console.log('Error fetching market rates', e));
  }, []);

  // Line Totals calculations matching pos.txt txtSMC_LostFocus / txtGoldLoss_LostFocus
  useEffect(() => {
    const weight = parseFloat(currentDetail.Weight) || 0;
    const goldLoss = parseFloat(currentDetail.GoldLoss) || 0;
    const costPerGram = parseFloat(currentDetail.CostPerGram) || 0;
    const smc = parseFloat(currentDetail.SMC) || 0;
    const pcs = parseFloat(currentDetail.PCS) || 1;

    let baseVal = 0;
    if (currentDetail.Class && currentDetail.Class.toUpperCase() === 'P') {
      baseVal = smc * pcs;
    } else {
      baseVal = (weight + goldLoss) * costPerGram + smc;
    }

    let gstAmt = 0;
    let finalTotalVal = baseVal;

    if (paymentState.TradeIn) {
      gstAmt = 0;
      finalTotalVal = baseVal;
    } else if (headerState.TaxMode === 'Exclusive') {
      gstAmt = Number((baseVal * 0.09).toFixed(2));
      finalTotalVal = Number((baseVal + gstAmt).toFixed(2));
    } else if (headerState.TaxMode === 'Inclusive') {
      gstAmt = Number(((baseVal * 9) / 100).toFixed(2));
      finalTotalVal = Number((baseVal + gstAmt).toFixed(2));
    } else {
      gstAmt = 0;
      finalTotalVal = baseVal;
    }

    const valStr = finalTotalVal.toFixed(2);
    const gstStr = gstAmt.toFixed(2);
    if (currentDetail.TotalValue !== valStr || currentDetail.GST !== gstStr) {
      setCurrentDetail((prev: any) => ({ ...prev, TotalValue: valStr, GST: gstStr }));
    }
  }, [currentDetail.Weight, currentDetail.GoldLoss, currentDetail.CostPerGram, currentDetail.SMC, currentDetail.PCS, currentDetail.Class, headerState.TaxMode, paymentState.TradeIn]);

  // Invoice Summary calculation matching subGetGSTAmount in pos.txt
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
    let grandTotal = netValue;

    if (paymentState.TradeIn) {
      finalTax = 0;
      grandTotal = netValue;
    } else if (headerState.TaxMode === 'Exclusive') {
      finalTax = Number((netValue * 0.09).toFixed(2));
      grandTotal = Number((netValue + finalTax).toFixed(2));
    } else if (headerState.TaxMode === 'Inclusive') {
      finalTax = Number(((netValue * 9) / 109).toFixed(2));
      grandTotal = netValue;
    } else {
      finalTax = 0;
      grandTotal = netValue;
    }

    setPaymentState(prev => ({
      ...prev,
      TotalQty: sumQty.toString(),
      TotalSalesAmt: sumVal.toFixed(2),
      NetValue: netValue.toFixed(2),
      Tax: finalTax.toFixed(2),
      GrandTotal: grandTotal.toFixed(2)
    }));
  }, [details, paymentState.NetDiscount, headerState.TaxMode, paymentState.TradeIn]);

  // LOV State
  const [lovVisible, setLovVisible] = useState(false);
  const [lovTitle, setLovTitle] = useState('');
  const [lovData, setLovData] = useState<any[]>([]);
  const [filteredLovData, setFilteredLovData] = useState<any[]>([]);
  const [lovSearch, setLovSearch] = useState('');
  const [lovColumns, setLovColumns] = useState<string[]>([]);
  const [onLovSelect, setOnLovSelect] = useState<((item: any) => void) | null>(null);

  // Date Picker State
  const [datePickerVisible, setDatePickerVisible] = useState(false);
  const [datePickerKey, setDatePickerKey] = useState('');
  const [currentMonth, setCurrentMonth] = useState(new Date());

  const openDatePicker = (key: string) => {
    setDatePickerKey(key);
    const currentVal = headerState[key as keyof typeof headerState];
    const parsed = new Date(currentVal as string);
    setCurrentMonth(isNaN(parsed.getTime()) ? new Date() : parsed);
    setDatePickerVisible(true);
  };

  const handleDateSelect = (day: number) => {
    const newDate = new Date(currentMonth.getFullYear(), currentMonth.getMonth(), day);
    const year = newDate.getFullYear();
    const month = String(newDate.getMonth() + 1).padStart(2, '0');
    const d = String(newDate.getDate()).padStart(2, '0');
    updateHeader(datePickerKey, `${year}-${month}-${d}`);
    setDatePickerVisible(false);
  };

  const renderCalendar = () => {
    const year = currentMonth.getFullYear();
    const month = currentMonth.getMonth();
    const firstDay = new Date(year, month, 1).getDay();
    const daysInMonth = new Date(year, month + 1, 0).getDate();

    const days = [];
    for (let i = 0; i < firstDay; i++) days.push(null);
    for (let i = 1; i <= daysInMonth; i++) days.push(i);

    return (
      <View style={styles.calendarContainer}>
        <View style={styles.calendarHeader}>
          <TouchableOpacity onPress={() => setCurrentMonth(new Date(year, month - 1, 1))}>
            <Ionicons name="chevron-back" size={20} color="#3B2D17" />
          </TouchableOpacity>
          <ThemedText style={styles.calendarTitle}>
            {currentMonth.toLocaleString('default', { month: 'long', year: 'numeric' })}
          </ThemedText>
          <TouchableOpacity onPress={() => setCurrentMonth(new Date(year, month + 1, 1))}>
            <Ionicons name="chevron-forward" size={20} color="#3B2D17" />
          </TouchableOpacity>
        </View>
        <View style={{ flexDirection: 'row', flexWrap: 'wrap' }}>
          {['Su', 'Mo', 'Tu', 'We', 'Th', 'Fr', 'Sa'].map(d => (
            <ThemedText key={d} style={styles.calendarDayHeader}>
              {d}
            </ThemedText>
          ))}
          {days.map((d, i) => {
            const isSelected =
              !!d &&
              headerState[datePickerKey as keyof typeof headerState] ===
              `${year}-${String(month + 1).padStart(2, '0')}-${String(d).padStart(2, '0')}`;
            return (
              <TouchableOpacity
                key={i}
                style={[styles.calendarDayCell, isSelected ? styles.calendarDayCellActive : null]}
                onPress={() => d && handleDateSelect(d)}
                disabled={!d}
              >
                {d && (
                  <ThemedText style={[styles.calendarDayText, isSelected ? styles.calendarDayTextActive : null]}>
                    {d}
                  </ThemedText>
                )}
              </TouchableOpacity>
            );
          })}
        </View>
        <TouchableOpacity style={{ marginTop: 12, alignItems: 'center' }} onPress={() => setDatePickerVisible(false)}>
          <ThemedText style={{ color: '#D93829', fontWeight: 'bold', fontSize: 13 }}>Cancel</ThemedText>
        </TouchableOpacity>
      </View>
    );
  };

  const showToast = (message: string, type: 'success' | 'error' | 'info' = 'success') => {
    setToast({ visible: true, message, type });
    setTimeout(() => setToast(prev => ({ ...prev, visible: false })), 3000);
  };

  const updateHeader = (key: string, value: any) => setHeaderState(prev => ({ ...prev, [key]: value }));
  const updateDetail = (key: string, value: any) => setCurrentDetail((prev: any) => ({ ...prev, [key]: value }));
  const updatePayment = (key: string, value: any) => setPaymentState(prev => ({ ...prev, [key]: value }));

  const fetchCustomers = async () => {
    try {
      const res = await fetch(`${API_URL}/pos/lov/customers`);
      const json = await res.json();
      if (json.success) {
        openLov('Select Customer', json.data, ['CustomerCode', 'CustomerName'], (item) => {
          updateHeader('CustomerCode', item.CustomerCode);
          updateHeader('CustomerName', item.CustomerName);
          updateHeader('CustomerAddress', item.Address1 || '');
        });
      } else {
        showToast("Failed to fetch customers", 'error');
      }
    } catch (e) {
      showToast("Error fetching customers", 'error');
    }
  };

  const fetchSalesman = async () => {
    try {
      const res = await fetch(`${API_URL}/pos/lov/salesmen`);
      const json = await res.json();
      if (json.success) {
        openLov('Select Salesman', json.data, ['SalesManCode', 'SalesManName'], (item) => {
          updateHeader('SalesManCode', item.SalesManCode);
        });
      } else {
        showToast("Failed to fetch salesman", 'error');
      }
    } catch (e) {
      showToast("Error fetching salesman", 'error');
    }
  };

  const fetchInvoices = async () => {
    try {
      const res = await fetch(`${API_URL}/pos`);
      const json = await res.json();
      if (json.success) {
        openLov('Select Invoice', json.data, ['InvoiceNo', 'CustomerCode', 'CreateDate', 'NetTotal'], async (item) => {
          try {
            const detailRes = await fetch(`${API_URL}/pos/${item.InvoiceNo}`);
            const detailJson = await detailRes.json();
            if (detailJson.success) {
              const hdr = detailJson.header;
              let loadedTaxMode = 'Inclusive';
              if (hdr.GSTType === 'E') loadedTaxMode = 'Exclusive';
              else if (hdr.GSTType === 'Z') loadedTaxMode = 'Zero Rated';

              let loadedETRS = 'Not For eTRS';
              if (hdr.eTRSType === 'Y') loadedETRS = 'For eTRS';
              else if (hdr.eTRSType === 'E') loadedETRS = 'eTRS Eligible';

              setHeaderState(prev => ({
                ...prev,
                InvoiceNo: hdr.InvoiceNo,
                InvoiceDate: hdr.Date ? hdr.Date.split('T')[0] : (hdr.InvoiceDate ? hdr.InvoiceDate.split('T')[0] : prev.InvoiceDate),
                SalesManCode: hdr.SalesManCode || '',
                CustomerCode: hdr.CustomerCode || 'CASH',
                CustomerName: hdr.CustomerName || 'CASH CUSTOMER',
                CustomerAddress: hdr.Address1 || '',
                PassportNo: hdr.PassportNo || '',
                TaxMode: loadedTaxMode,
                eTRS: loadedETRS,
                LocationCode: hdr.LocationCode || 'HQ',
                Remarks: hdr.Remarks || '',
              }));
              updatePayment('NetDiscount', hdr.NetDiscount?.toString() || hdr.Discount?.toString() || '0');
              setDetails(detailJson.details.map((d: any) => ({
                ...d,
                CostPerGram: d.Rate,
                PCS: d.PCS || 1
              })));
            }
          } catch (e) {
            showToast("Error fetching invoice details", 'error');
          }
        });
      }
    } catch (e) {
      showToast("Error fetching invoices", 'error');
    }
  };

  const fetchInventory = async () => {
    try {
      const res = await fetch(`${API_URL}/inventorysearch`);
      const json = await res.json();
      if (json.success) {
        openLov('Select Inventory', json.data, ['InventoryCode', 'Description', 'Weight', 'CostPerGram', 'SMC'], async (item) => {
          updateDetail('InventoryCode', item.InventoryCode);
          updateDetail('Description', item.Description);
          updateDetail('Weight', item.Weight?.toString() || '0');
          updateDetail('CostPerGram', item.CostPerGram?.toString() || '0');
          updateDetail('SMC', item.SMC?.toString() || '0');
          updateDetail('Class', item.ClassCode || '');

          if (item.ClassCode) {
            try {
              const mrRes = await fetch(`${API_URL}/pos/lov/market-rate/${item.ClassCode}`);
              const mrJson = await mrRes.json();
              if (mrJson.success && mrJson.data.length > 0) {
                updateDetail('DailyClassRate', mrJson.data[0].Rate?.toString() || '0');
              }
              const mcRes = await fetch(`${API_URL}/pos/lov/mc-discount/${item.ClassCode}`);
              const mcJson = await mcRes.json();
              if (mcJson.success && mcJson.data.length > 0) {
                updateDetail('DetailDiscount', mcJson.data[0].MCDiscount?.toString() || '0');
              }
            } catch (e) {
              console.log('LOV fetch error', e);
            }
          }
        });
      }
    } catch (e) {
      showToast("Error fetching inventory", 'error');
    }
  };

  const openLov = (title: string, data: any[], columns: string[], onSelect: (item: any) => void) => {
    setLovTitle(title);
    setLovData(data);
    setFilteredLovData(data);
    setLovColumns(columns);
    setOnLovSelect(() => (item: any) => {
      onSelect(item);
      setLovVisible(false);
      setLovSearch('');
    });
    setLovVisible(true);
  };

  const handleLovSearch = (text: string) => {
    setLovSearch(text);
    if (!text) {
      setFilteredLovData(lovData);
      return;
    }
    const lower = text.toLowerCase();
    const filtered = lovData.filter(item =>
      lovColumns.some(col => String(item[col] || '').toLowerCase().includes(lower))
    );
    setFilteredLovData(filtered);
  };

  const addItem = () => {
    if (!currentDetail.InventoryCode) {
      showToast("Validation Error: Please select an Item Code", 'error');
      return;
    }
    if (!currentDetail.Weight || parseFloat(currentDetail.Weight) <= 0) {
      showToast("Validation Error: Weight must be greater than 0", 'error');
      return;
    }
    if (!currentDetail.CostPerGram || parseFloat(currentDetail.CostPerGram) <= 0) {
      showToast("Validation Error: Cost per gram must be greater than 0", 'error');
      return;
    }
    if (!currentDetail.PCS || parseFloat(currentDetail.PCS) <= 0) {
      showToast("Validation Error: PCS must be greater than 0", 'error');
      return;
    }

    if (selectedIndex !== null) {
      const newDetails = [...details];
      newDetails[selectedIndex] = { ...currentDetail };
      setDetails(newDetails);
      setSelectedIndex(null);
      showToast("Line item updated successfully!", 'success');
    } else {
      setDetails([...details, { ...currentDetail }]);
      showToast("Line item added successfully!", 'success');
    }
    setCurrentDetail({
      InventoryCode: '', Description: '', Class: '', Weight: '', GoldLoss: '', CostPerGram: '', SMC: '', PCS: '1', TotalValue: '', GST: ''
    });
    setLineDetailModalVisible(false);
  };

  const deleteItem = () => {
    if (selectedIndex !== null) {
      const newDetails = details.filter((_, i) => i !== selectedIndex);
      setDetails(newDetails);
      setSelectedIndex(null);
      setCurrentDetail({
        InventoryCode: '', Description: '', Class: '', Weight: '', GoldLoss: '', CostPerGram: '', SMC: '', PCS: '1', TotalValue: '', GST: ''
      });
      showToast("Line item deleted", 'info');
      setLineDetailModalVisible(false);
    }
  };

  const clearForm = () => {
    setHeaderState({
      InvoiceNo: '',
      InvoiceDate: new Date().toISOString().split('T')[0],
      SalesManCode: headerState.SalesManCode || '',
      CustomerCode: 'CASH',
      CustomerName: 'CASH CUSTOMER',
      CustomerAddress: '',
      PassportNo: '',
      CustomerType: 'N',
      MemberCode: '',
      TaxMode: 'Inclusive',
      LocationCode: 'HQ',
      Remarks: '',
      Advance: '',
      FlexiAmount: '',
      FlexiBonus: '',
      eTRS: 'Not For eTRS',
      PrintMC: false,
      PrintImage: false,
    });
    setDetails([]);
    setCurrentDetail({
      InventoryCode: '', Description: '', Class: '', Weight: '', GoldLoss: '', CostPerGram: '', SMC: '', PCS: '1', TotalValue: '', GST: ''
    });
    setPaymentState({
      PaymentType: 'Cash Sales', TradeIn: false, TotalQty: '0', TotalSalesAmt: '0', NetDiscount: '0', NetValue: '0', Tax: '0', GrandTotal: '0', ReturnAmt: '0', PayMode: 'Cash'
    });
    setSelectedIndex(null);
  };

  const handleSave = async () => {
    // 1. SalesMan check
    if (!headerState.SalesManCode) {
      showToast("Validation Error: Please select or authorize Salesman", 'error');
      return;
    }
    // 2. Customer check (pos.txt fncValidate: If Trim(txtCustomerCode.Text) = "" Then MsgBox("Please enter Customer! "))
    if (!headerState.CustomerCode || !headerState.CustomerCode.trim()) {
      showToast("Validation Error: Please enter Customer!", 'error');
      return;
    }
    // 3. MemberCode check when Customer is CASH and CustomerType is Member
    if (headerState.CustomerType === 'M' && !headerState.MemberCode) {
      showToast("Validation Error: MemberCode is NOT entered!", 'error');
      return;
    }
    // 4. eTRS Passport check (pos.txt fncValidate: If Trim(txtRemarks.Text) = "" And opteTrsTicketIssued.Checked Then MsgBox("Please Enter PassportNo! "))
    if (headerState.eTRS === 'For eTRS' && (!headerState.PassportNo || !headerState.PassportNo.trim()) && (!headerState.Remarks || !headerState.Remarks.trim())) {
      showToast("Validation Error: Please Enter PassportNo / Remarks for eTRS!", 'error');
      return;
    }
    // 5. Items count check (pos.txt: If grdSales.RowCount <= 0 Then MsgBox("Enter items for sales! "))
    if (details.length === 0) {
      showToast("Validation Error: Enter items for sales!", 'error');
      return;
    }
    // 6. Total Sales Amount check (pos.txt: If Val(lblSalesAmount.Text) < 0 Then MsgBox("Can not do negative sales."))
    const totalSalesNum = parseFloat(paymentState.TotalSalesAmt) || 0;
    if (totalSalesNum < 0) {
      showToast("Validation Error: Cannot do negative sales.", 'error');
      return;
    }
    // 7. Date check
    if (!headerState.InvoiceDate || isNaN(Date.parse(headerState.InvoiceDate))) {
      showToast("Validation Error: INVOICE DATE is invalid!", 'error');
      return;
    }

    const invNo = headerState.InvoiceNo || `INV-${Date.now()}`;

    // GST Type mapping
    let gstType = 'I';
    if (headerState.TaxMode === 'Exclusive') gstType = 'E';
    else if (headerState.TaxMode === 'Zero Rated') gstType = 'Z';

    // eTRS Type mapping
    let eTRSType = 'N';
    if (headerState.eTRS === 'For eTRS') eTRSType = 'Y';
    else if (headerState.eTRS === 'eTRS Eligible') eTRSType = 'E';

    // Sales Type mapping
    let salesType = 'CASH';
    if (paymentState.TradeIn || paymentState.PaymentType === 'Trade In') salesType = 'TRADEIN';
    else if (paymentState.PaymentType === 'Installment') salesType = 'INSTALLMENT';

    // If eTRS Ticket Issued and remarks empty, set customer passport
    const remarks = headerState.Remarks || (headerState.eTRS === 'For eTRS' ? (headerState.PassportNo || headerState.CustomerCode) : '');

    setLoading(true);
    try {
      const res = await fetch(`${API_URL}/pos`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          header: {
            ...headerState,
            InvoiceNo: invNo,
            ...paymentState,
            Remarks: remarks,
            LocationCode: headerState.LocationCode || 'HQ',
            TerminalCode: 'T1',
            GSTType: gstType,
            eTRSType: eTRSType,
            SalesType: salesType
          },
          details,
          userId: 'ADMIN',
          payModes: [{
            PayMode: paymentState.PayMode || (salesType === 'TRADEIN' ? 'TRADE-IN' : salesType),
            Amount: paymentState.GrandTotal,
            Commission: 0,
            SlipNo: ''
          }]
        })
      });
      const json = await res.json();
      if (json.success) {
        showToast("Invoice saved successfully!", 'success');
        clearForm();
      } else {
        Alert.alert("Error", json.message || "Failed to save");
      }
    } catch (e: any) {
      Alert.alert("Error", e.message || "Network error");
    } finally {
      setLoading(false);
    }
  };

  const handleCancel = () => {
    if (!headerState.InvoiceNo) {
      showToast("Validation Error: Please select an existing invoice to cancel", 'error');
      return;
    }
    Alert.alert(
      "Confirm Cancellation",
      "Are you sure you want to cancel this bill? This cannot be undone.",
      [
        { text: "No", style: "cancel" },
        {
          text: "Yes, Cancel It",
          style: "destructive",
          onPress: async () => {
            setLoading(true);
            try {
              const res = await fetch(`${API_URL}/pos/${headerState.InvoiceNo}`, {
                method: 'DELETE'
              });
              const json = await res.json();
              if (json.success) {
                showToast("Invoice cancelled successfully!", 'success');
                clearForm();
              } else {
                showToast(`Error: ${json.message}`, 'error');
              }
            } catch (e) {
              showToast("Network error during cancellation", 'error');
            } finally {
              setLoading(false);
            }
          }
        }
      ]
    );
  };

  const handleRePrint = () => {
    if (!headerState.InvoiceNo) {
      showToast("Please select an Invoice No first", 'info');
      return;
    }
    showToast(`RePrinting Invoice: ${headerState.InvoiceNo}`, 'success');
  };

  const handleNetTotal = () => {
    showToast(`Net Total: $${paymentState.GrandTotal}`, 'info');
  };

  const renderInputRow = (label: string, key: string, val: string, updateFn: any, lookupFn?: () => void, isNumeric = false, labelWidth?: number) => (
    <View style={styles.formRow}>
      <ThemedText style={[styles.formLabel, labelWidth ? { width: labelWidth } : null]} numberOfLines={1}>
        {label}
      </ThemedText>
      <View style={styles.inputWithLookup}>
        <TextInput
          style={[styles.formInput, isNumeric && { textAlign: 'right' }]}
          value={val}
          onChangeText={(text) => updateFn(key, text)}
          keyboardType={isNumeric ? 'numeric' : 'default'}
          editable={!lookupFn}
        />
        {lookupFn && (
          <TouchableOpacity style={styles.attachedLookupBtn} onPress={lookupFn} activeOpacity={0.7}>
            <ThemedText style={styles.attachedLookupText}>...</ThemedText>
          </TouchableOpacity>
        )}
      </View>
    </View>
  );

  const renderItemInput = (label: string, key: string, val: string, isNumeric = false, flex = 1) => (
    <View style={{ flex }}>
      <ThemedText style={styles.itemLabel}>{label}</ThemedText>
      <TextInput
        style={[styles.itemInput, isNumeric && { textAlign: 'right' }]}
        value={val}
        onChangeText={(text) => updateDetail(key, text)}
        keyboardType={isNumeric ? 'numeric' : 'default'}
      />
    </View>
  );

  return (
    <SafeAreaView style={styles.container} edges={['top']}>
      <Stack.Screen options={{ headerShown: false }} />

      {/* SALESMAN PASSWORD VERIFICATION MODAL */}
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
                  Enter SalesMan Password to access Retail POS
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
          <ThemedText style={styles.headerTitle}>Point of Sale (POS)</ThemedText>
        </View>

        <View style={styles.headerRight}>
          {/* Clear */}
          <TouchableOpacity
            style={[styles.actionBtn, styles.clearBtn]}
            onPress={clearForm}
            activeOpacity={0.8}
            disabled={loading}
          >
            <Ionicons name="refresh-outline" size={14} color="#5C4A28" />
            <ThemedText style={styles.clearBtnText}>Clear</ThemedText>
          </TouchableOpacity>

          {/* Save */}
          <TouchableOpacity
            style={[styles.actionBtn, styles.saveBtn]}
            onPress={handleSave}
            activeOpacity={0.8}
            disabled={loading}
          >
            {loading ? (
              <ActivityIndicator size="small" color="#9E6D08" />
            ) : (
              <>
                <Ionicons name="checkmark-done-outline" size={14} color="#9E6D08" />
                <ThemedText style={styles.saveBtnText}>Save</ThemedText>
              </>
            )}
          </TouchableOpacity>

          {/* RePrint */}
          <TouchableOpacity
            style={[styles.actionBtn, styles.clearBtn]}
            onPress={handleRePrint}
            activeOpacity={0.8}
            disabled={loading}
          >
            <Ionicons name="print-outline" size={14} color="#5C4A28" />
            <ThemedText style={styles.clearBtnText}>RePrint</ThemedText>
          </TouchableOpacity>

          {/* Net Total */}
          <TouchableOpacity
            style={[styles.actionBtn, styles.secondaryGoldBtn]}
            onPress={handleNetTotal}
            activeOpacity={0.8}
          >
            <Ionicons name="calculator-outline" size={14} color="#9E6D08" />
            <ThemedText style={styles.secondaryGoldBtnText}>Net Total</ThemedText>
          </TouchableOpacity>

          {/* Bill Cancel */}
          <TouchableOpacity
            style={[styles.actionBtn, styles.destructiveBtn]}
            onPress={handleCancel}
            activeOpacity={0.8}
            disabled={loading}
          >
            <Ionicons name="trash-outline" size={14} color="#D93829" />
            <ThemedText style={styles.destructiveBtnText}>Bill Cancel</ThemedText>
          </TouchableOpacity>


        </View>
      </View>

      {/* TOAST NOTIFICATION */}
      {toast.visible && (
        <View
          style={[
            styles.toast,
            { backgroundColor: toast.type === 'error' ? '#D32F2F' : toast.type === 'info' ? '#B48218' : '#2E7D32' },
          ]}
        >
          <Ionicons
            name={toast.type === 'error' ? 'alert-circle' : toast.type === 'info' ? 'information-circle' : 'checkmark-circle'}
            size={16}
            color="#FFFFFF"
            style={{ marginRight: 6 }}
          />
          <ThemedText style={styles.toastText}>{toast.message}</ThemedText>
        </View>
      )}

      {/* MAIN CONTENT AREA */}
      <View style={styles.contentBody}>
        {/* LEFT PRIMARY PANE */}
        <View style={styles.leftPane}>
          {/* Top Panel (Split: Class Rates Table & Customer/Invoice Details) */}
          <View style={styles.topSplitRow}>
            {/* Left: Class Rates Card */}
            <View style={[styles.goldCard, styles.classRatesCard]}>
              <View style={styles.cardHeaderBanner}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="pricetag-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitleSmall}>Class Rates</ThemedText>
              </View>

              <View style={styles.classRatesTableHeader}>
                <ThemedText style={[styles.classRatesHeadCell, { flex: 1 }]}>Class</ThemedText>
                <ThemedText style={[styles.classRatesHeadCell, { flex: 1, textAlign: 'right' }]}>Rate/g</ThemedText>
              </View>

              <ScrollView style={{ flex: 1 }} nestedScrollEnabled>
                {classRates.map((cr, idx) => (
                  <TouchableOpacity
                    key={idx}
                    style={[
                      styles.classRateRow,
                      idx % 2 === 1 && { backgroundColor: '#FCFAF6' },
                      currentDetail.Class === cr.ClassCode && styles.classRateRowSelected
                    ]}
                    activeOpacity={0.7}
                  >
                    <ThemedText style={[styles.classRateText, { flex: 1, fontWeight: 'bold' }]}>
                      {cr.ClassCode}
                    </ThemedText>
                    <ThemedText style={[styles.classRateText, { flex: 1, textAlign: 'right', color: '#9E6D08' }]}>
                      {parseFloat(cr.Rate).toFixed(2)}
                    </ThemedText>
                  </TouchableOpacity>
                ))}
              </ScrollView>
            </View>

            {/* Right: Customer & Invoice Form Card */}
            <View style={[styles.goldCard, { flex: 1 }]}>
              <View style={styles.cardHeaderBanner}>
                <View style={styles.headerTitleContainer}>
                  <View style={styles.headerIconBadgeSmall}>
                    <Ionicons name="person-outline" size={13} color="#B48218" />
                  </View>
                  <ThemedText style={styles.cardHeaderTitleSmall}>Invoice & Customer Information</ThemedText>
                </View>

                {/* Tax Mode Radios - Locked when items exist in invoice matching pos.txt */}
                <View style={{ flexDirection: 'row', alignItems: 'center', gap: 4 }}>
                  <RadioButton
                    label="Zero Rated"
                    selected={headerState.TaxMode === 'Zero Rated'}
                    disabled={details.length > 0}
                    onPress={() => updateHeader('TaxMode', 'Zero Rated')}
                  />
                  <RadioButton
                    label="Inclusive"
                    selected={headerState.TaxMode === 'Inclusive'}
                    disabled={details.length > 0}
                    onPress={() => updateHeader('TaxMode', 'Inclusive')}
                  />
                  <RadioButton
                    label="Exclusive"
                    selected={headerState.TaxMode === 'Exclusive'}
                    disabled={details.length > 0}
                    onPress={() => updateHeader('TaxMode', 'Exclusive')}
                  />
                </View>
              </View>

              <View style={styles.cardBody}>
                {/* Row 1: SalesMan & Customer */}
                <View style={styles.formGridRow}>
                  <View style={{ flex: 1 }}>
                    {renderInputRow("Sales Man", "SalesManCode", headerState.SalesManCode, updateHeader, undefined, false, 65)}
                  </View>
                  <View style={{ flex: 1 }}>
                    {renderInputRow("Customer", "CustomerCode", headerState.CustomerCode, updateHeader, fetchCustomers, false, 60)}
                  </View>
                </View>

                {/* Row 2: Inv Date, Inv No, Member Code & Customer Type */}
                <View style={styles.formGridRow}>
                  <View style={{ flex: 1 }}>
                    {renderInputRow("Inv Date", "InvoiceDate", headerState.InvoiceDate, updateHeader, () => openDatePicker("InvoiceDate"), false, 65)}
                  </View>
                  <View style={{ flex: 1 }}>
                    {renderInputRow("Inv No", "InvoiceNo", headerState.InvoiceNo, updateHeader, fetchInvoices, false, 60)}
                  </View>
                  {headerState.CustomerType === 'M' && (
                    <View style={{ flex: 1 }}>
                      {renderInputRow("Member", "MemberCode", headerState.MemberCode, updateHeader, undefined, false, 55)}
                    </View>
                  )}
                  <TouchableOpacity
                    style={styles.customerTypeBtn}
                    onPress={() => {
                      const nextType = headerState.CustomerType === 'N' ? 'M' : 'N';
                      updateHeader('CustomerType', nextType);
                      showToast(`Customer Type: ${nextType === 'N' ? 'Normal' : 'Member'}`, 'info');
                    }}
                    activeOpacity={0.7}
                  >
                    <ThemedText style={styles.customerTypeBtnText}>
                      {headerState.CustomerType === 'M' ? 'Member' : 'Normal Type'}
                    </ThemedText>
                  </TouchableOpacity>
                </View>

                {/* Row 3: Customer Name & Advance */}
                <View style={styles.formGridRow}>
                  <View style={{ flex: 1.5 }}>
                    {renderInputRow("Name", "CustomerName", headerState.CustomerName, updateHeader, undefined, false, 65)}
                  </View>
                  <View style={{ flex: 1 }}>
                    {renderInputRow("Advance", "Advance", headerState.Advance, updateHeader, undefined, true, 55)}
                  </View>
                </View>

                {/* Row 4: Address & Passport */}
                <View style={styles.formGridRow}>
                  <View style={{ flex: 1.5 }}>
                    {renderInputRow("Address", "CustomerAddress", headerState.CustomerAddress, updateHeader, undefined, false, 65)}
                  </View>
                  <View style={{ flex: 1 }}>
                    {renderInputRow("Passport", "PassportNo", headerState.PassportNo, updateHeader, undefined, false, 55)}
                  </View>
                </View>

                {/* Combined Row: Remarks, Flexi Amt, and Flexi Bonus in one row */}
                <View style={styles.formGridRow}>
                  <View style={{ flex: 1.5 }}>
                    {renderInputRow("Remarks", "Remarks", headerState.Remarks, updateHeader, undefined, false, 65)}
                  </View>
                  <View style={{ flex: 1 }}>
                    {renderInputRow("Flexi Amt", "FlexiAmount", headerState.FlexiAmount, updateHeader, undefined, true, 60)}
                  </View>
                  <View style={{ flex: 1 }}>
                    {renderInputRow("Flexi Bonus", "FlexiBonus", headerState.FlexiBonus, updateHeader, undefined, true, 72)}
                  </View>
                </View>

                {/* Row 7: eTRS Radios & Checkboxes (Clean layout, no overlapping) */}
                <View style={[styles.formGridRow, { borderBottomWidth: 0, paddingBottom: 0, justifyContent: 'space-between', flexWrap: 'wrap' }]}>
                  <View style={{ flexDirection: 'row', alignItems: 'center', gap: 8 }}>
                    <RadioButton
                      label="For eTRS"
                      selected={headerState.eTRS === 'For eTRS'}
                      onPress={() => {
                        updateHeader('eTRS', 'For eTRS');
                        if (!headerState.Remarks && headerState.CustomerCode) {
                          updateHeader('Remarks', headerState.PassportNo || headerState.CustomerCode);
                        }
                      }}
                    />
                    <RadioButton
                      label="Not eTRS"
                      selected={headerState.eTRS === 'Not For eTRS'}
                      onPress={() => {
                        updateHeader('eTRS', 'Not For eTRS');
                        if (headerState.Remarks === headerState.PassportNo || headerState.Remarks === headerState.CustomerCode) {
                          updateHeader('Remarks', '');
                        }
                      }}
                    />
                    <RadioButton
                      label="Eligible"
                      selected={headerState.eTRS === 'eTRS Eligible'}
                      onPress={() => updateHeader('eTRS', 'eTRS Eligible')}
                    />
                  </View>

                  <View style={{ flexDirection: 'row', gap: 12, alignItems: 'center' }}>
                    <Checkbox label="Print MC" selected={headerState.PrintMC} onPress={() => updateHeader('PrintMC', !headerState.PrintMC)} />
                    <Checkbox label="Print Img" selected={headerState.PrintImage} onPress={() => updateHeader('PrintImage', !headerState.PrintImage)} />
                    <Checkbox label="Trade In" selected={paymentState.TradeIn} onPress={() => updatePayment('TradeIn', !paymentState.TradeIn)} />
                  </View>
                </View>
              </View>
            </View>
          </View>

          {/* Middle 100% Full-Width Items Table */}
          <View style={[styles.goldCard, { flex: 1, marginTop: 8 }]}>
            <View style={[styles.cardHeaderBanner, { justifyContent: 'space-between' }]}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="cart-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitleSmall}>
                  Invoice Line Items ({details.length})
                </ThemedText>
              </View>

              <View style={{ flexDirection: 'row', alignItems: 'center', gap: 6 }}>
                {selectedIndex !== null && (
                  <TouchableOpacity
                    style={[styles.actionBtn, styles.destructiveBtn, { paddingVertical: 3, paddingHorizontal: 8 }]}
                    onPress={deleteItem}
                  >
                    <Ionicons name="trash-outline" size={12} color="#D93829" />
                    <ThemedText style={[styles.destructiveBtnText, { fontSize: 11 }]}>Delete</ThemedText>
                  </TouchableOpacity>
                )}
                <TouchableOpacity
                  style={[styles.actionBtn, styles.saveBtn, { paddingVertical: 4, paddingHorizontal: 12, backgroundColor: '#9E6D08' }]}
                  onPress={() => {
                    setSelectedIndex(null);
                    setCurrentDetail({
                      InventoryCode: '', Description: '', Class: '', Weight: '', GoldLoss: '', CostPerGram: '', SMC: '', PCS: '1', TotalValue: '', GST: ''
                    });
                    setLineDetailModalVisible(true);
                  }}
                  activeOpacity={0.8}
                >
                  <Ionicons name="add" size={14} color="#FFFFFF" />
                  <ThemedText style={{ color: '#FFFFFF', fontSize: 11, fontWeight: 'bold' }}>Line Add</ThemedText>
                </TouchableOpacity>
              </View>
            </View>

            {/* Table Header */}
            <View style={styles.tableHeaderRow}>
              <ThemedText style={[styles.tableHeadCell, { flex: 1.1 }]}>Item Code</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 0.8 }]}>Class</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 2 }]}>Description</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 0.9, textAlign: 'right' }]}>Weight</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 0.9, textAlign: 'right' }]}>Loss</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 1, textAlign: 'right' }]}>Cost/g</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 0.8, textAlign: 'right' }]}>SMC</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 0.7, textAlign: 'center' }]}>PCS</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 1.2, textAlign: 'right' }]}>Total ($)</ThemedText>
            </View>

            <ScrollView style={{ flex: 1 }} nestedScrollEnabled>
              {details.length === 0 ? (
                <View style={styles.emptyTableContainer}>
                  <Ionicons name="cube-outline" size={32} color="#C4B69C" />
                  <ThemedText style={styles.emptyTableText}>No items added yet</ThemedText>
                </View>
              ) : (
                details.map((d, i) => (
                  <TouchableOpacity
                    key={i}
                    style={[
                      styles.tableRow,
                      i % 2 === 1 && { backgroundColor: '#FCFAF6' },
                      selectedIndex === i && styles.tableRowSelected,
                    ]}
                    activeOpacity={0.7}
                    onPress={() => {
                      setSelectedIndex(i);
                      setCurrentDetail(d);
                      setLineDetailModalVisible(true);
                    }}
                  >
                    <ThemedText style={[styles.tableRowText, styles.codeCell, { flex: 1.1 }]}>
                      {d.InventoryCode}
                    </ThemedText>
                    <ThemedText style={[styles.tableRowText, { flex: 0.8 }]}>{d.Class}</ThemedText>
                    <ThemedText style={[styles.tableRowText, { flex: 2 }]} numberOfLines={1}>
                      {d.Description}
                    </ThemedText>
                    <ThemedText style={[styles.tableRowText, { flex: 0.9, textAlign: 'right' }]}>
                      {Number(d.Weight || 0).toFixed(2)}
                    </ThemedText>
                    <ThemedText style={[styles.tableRowText, { flex: 0.9, textAlign: 'right' }]}>
                      {Number(d.GoldLoss || 0).toFixed(2)}
                    </ThemedText>
                    <ThemedText style={[styles.tableRowText, { flex: 1, textAlign: 'right' }]}>
                      {Number(d.CostPerGram || 0).toFixed(2)}
                    </ThemedText>
                    <ThemedText style={[styles.tableRowText, { flex: 0.8, textAlign: 'right' }]}>
                      {Number(d.SMC || 0).toFixed(2)}
                    </ThemedText>
                    <ThemedText style={[styles.tableRowText, { flex: 0.7, textAlign: 'center' }]}>
                      {d.PCS}
                    </ThemedText>
                    <ThemedText style={[styles.tableRowText, styles.amountCell, { flex: 1.2, textAlign: 'right' }]}>
                      {Number(d.TotalValue || 0).toFixed(2)}
                    </ThemedText>
                  </TouchableOpacity>
                ))
              )}
            </ScrollView>
          </View>

        </View>

        {/* RIGHT SUMMARY PANEL */}
        <View style={styles.rightPane}>
          {/* Sale Mode Badge */}
          <View style={styles.saleBadgeCard}>
            <ThemedText style={styles.saleBadgeText}>
              {headerState.TaxMode.toUpperCase()} SALES
            </ThemedText>
          </View>

          {/* Totals Breakdown Card */}
          <View style={[styles.goldCard, { marginTop: 6 }]}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerIconBadgeSmall}>
                <Ionicons name="calculator-outline" size={13} color="#B48218" />
              </View>
              <ThemedText style={styles.cardHeaderTitleSmall}>Invoice Summary</ThemedText>
            </View>

            <View style={styles.cardBody}>
              <View style={styles.totalRow}>
                <ThemedText style={styles.totalLbl}>Total Qty</ThemedText>
                <ThemedText style={styles.totalVal}>{paymentState.TotalQty}</ThemedText>
              </View>
              <View style={styles.totalRow}>
                <ThemedText style={styles.totalLbl}>Sales Amount</ThemedText>
                <ThemedText style={styles.totalVal}>$ {paymentState.TotalSalesAmt}</ThemedText>
              </View>
              <View style={styles.totalRow}>
                <ThemedText style={styles.totalLbl}>Net Discount</ThemedText>
                <TextInput
                  style={[styles.formInput, { width: 70, height: 24, textAlign: 'right', fontSize: 11, marginLeft: 12 }]}
                  value={paymentState.NetDiscount}
                  onChangeText={(val) => updatePayment('NetDiscount', val)}
                  keyboardType="numeric"
                />
              </View>
              <View style={styles.totalRow}>
                <ThemedText style={styles.totalLbl}>Net Value</ThemedText>
                <ThemedText style={styles.totalVal}>$ {paymentState.NetValue}</ThemedText>
              </View>
              <View style={[styles.totalRow, { borderBottomWidth: 0 }]}>
                <ThemedText style={styles.totalLbl}>Tax (GST 9%)</ThemedText>
                <ThemedText style={[styles.totalVal, { color: '#9E6D08' }]}>
                  $ {paymentState.Tax}
                </ThemedText>
              </View>
            </View>
          </View>

          {/* Grand Net Total Display */}
          <View style={styles.grandTotalCard}>
            <ThemedText style={styles.grandTotalLabel}>Net Total Payable</ThemedText>
            <ThemedText style={styles.grandTotalValue}>
              ${Number(paymentState.GrandTotal || 0).toFixed(2)}
            </ThemedText>
          </View>
        </View>
      </View>

      {/* FOOTER AUDIT STATUS BAR */}
      <View style={styles.footerStatusBar}>
        <View style={styles.footerCol}>
          <ThemedText style={styles.footerLabel}>Location</ThemedText>
          <ThemedText style={styles.footerVal}>HQ</ThemedText>
        </View>
        <View style={styles.footerCol}>
          <ThemedText style={styles.footerLabel}>Last SalesMan</ThemedText>
          <ThemedText style={styles.footerVal}>0</ThemedText>
        </View>
        <View style={styles.footerCol}>
          <ThemedText style={styles.footerLabel}>Last Bill No</ThemedText>
          <ThemedText style={styles.footerVal}>0</ThemedText>
        </View>
        <View style={styles.footerCol}>
          <ThemedText style={styles.footerLabel}>Next Inv No</ThemedText>
          <ThemedText style={styles.footerVal}>0</ThemedText>
        </View>
        <View style={styles.footerCol}>
          <ThemedText style={styles.footerLabel}>Total Sales Amt</ThemedText>
          <ThemedText style={[styles.footerVal, { color: '#9E6D08' }]}>
            ${paymentState.TotalSalesAmt || '0.00'}
          </ThemedText>
        </View>
        <View style={styles.footerCol}>
          <ThemedText style={styles.footerLabel}>Return Amount</ThemedText>
          <ThemedText style={styles.footerVal}>$0.00</ThemedText>
        </View>
      </View>

      {/* LINE DETAIL MODAL (INSERT / UPDATE) - Matches image layout */}
      <Modal visible={lineDetailModalVisible} transparent animationType="fade" onRequestClose={() => setLineDetailModalVisible(false)}>
        <TouchableOpacity
          style={styles.modalOverlay}
          activeOpacity={1}
          onPress={() => setLineDetailModalVisible(false)}
        >
          <TouchableWithoutFeedback>
            <View style={styles.imageMatchedModalCard}>
              {/* Header Banner - Exactly like image */}
              <View style={styles.imageMatchedHeaderBanner}>
                <View style={styles.imageMatchedHeaderLeft}>
                  <View style={styles.imageMatchedIconBox}>
                    <Ionicons name="create-outline" size={15} color="#B48218" />
                  </View>
                  <ThemedText style={styles.imageMatchedHeaderTitle}>
                    {selectedIndex !== null ? 'Edit Item in Invoice' : 'Add Item to Invoice'}
                  </ThemedText>
                </View>

                <View style={styles.imageMatchedHeaderActions}>
                  {selectedIndex !== null && (
                    <TouchableOpacity
                      style={[styles.actionBtn, styles.destructiveBtn, { paddingVertical: 5, paddingHorizontal: 12, marginRight: 4 }]}
                      onPress={deleteItem}
                      activeOpacity={0.8}
                    >
                      <Ionicons name="trash-outline" size={13} color="#D93829" />
                      <ThemedText style={styles.destructiveBtnText}>Delete</ThemedText>
                    </TouchableOpacity>
                  )}

                  <TouchableOpacity
                    style={styles.imageMatchedClearBtn}
                    onPress={() => {
                      setCurrentDetail({
                        InventoryCode: '', Description: '', Class: '', Weight: '', GoldLoss: '', CostPerGram: '', SMC: '', PCS: '1', TotalValue: '', GST: ''
                      });
                    }}
                    activeOpacity={0.8}
                  >
                    <ThemedText style={styles.imageMatchedClearBtnText}>Clear Item</ThemedText>
                  </TouchableOpacity>

                  <TouchableOpacity
                    style={styles.imageMatchedAddBtn}
                    onPress={addItem}
                    activeOpacity={0.8}
                  >
                    <ThemedText style={styles.imageMatchedAddBtnText}>
                      {selectedIndex !== null ? 'Update Item' : '+ Add Item'}
                    </ThemedText>
                  </TouchableOpacity>

                  <TouchableOpacity
                    onPress={() => setLineDetailModalVisible(false)}
                    style={styles.imageMatchedCloseBtn}
                    activeOpacity={0.7}
                  >
                    <Ionicons name="close" size={18} color="#5C4A28" />
                  </TouchableOpacity>
                </View>
              </View>

              {/* Body: Inputs matching the exact horizontal screenshot layout */}
              <ScrollView
                horizontal
                showsHorizontalScrollIndicator={false}
                contentContainerStyle={styles.imageMatchedBodyRow}
              >
                {/* 1. Inventory Code with inline ... lookup button */}
                <View style={[styles.imageMatchedFieldCol, { width: 175 }]}>
                  <ThemedText style={styles.imageMatchedFieldLabel}>Inventory Code</ThemedText>
                  <View style={{ flexDirection: 'row', alignItems: 'center', gap: 6 }}>
                    <TextInput
                      style={[styles.imageMatchedInput, { flex: 1 }]}
                      value={currentDetail.InventoryCode}
                      onChangeText={(text) => updateDetail('InventoryCode', text)}
                      placeholder="Select..."
                      placeholderTextColor="#9E8D77"
                    />
                    <TouchableOpacity
                      style={styles.imageMatchedStandaloneLookupBtn}
                      onPress={fetchInventory}
                      activeOpacity={0.7}
                    >
                      <ThemedText style={styles.imageMatchedLookupBtnText}>...</ThemedText>
                    </TouchableOpacity>
                  </View>
                </View>

                {/* 2. Description */}
                <View style={[styles.imageMatchedFieldCol, { width: 220 }]}>
                  <ThemedText style={styles.imageMatchedFieldLabel}>Description</ThemedText>
                  <TextInput
                    style={styles.imageMatchedInput}
                    value={currentDetail.Description}
                    onChangeText={(text) => updateDetail('Description', text)}
                    placeholder=""
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* 3. Weight */}
                <View style={[styles.imageMatchedFieldCol, { width: 90 }]}>
                  <ThemedText style={styles.imageMatchedFieldLabel}>Weight</ThemedText>
                  <TextInput
                    style={[styles.imageMatchedInput, { textAlign: 'right' }]}
                    value={currentDetail.Weight}
                    onChangeText={(text) => updateDetail('Weight', text)}
                    keyboardType="numeric"
                    placeholder=""
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* 4. Gold Loss */}
                <View style={[styles.imageMatchedFieldCol, { width: 90 }]}>
                  <ThemedText style={styles.imageMatchedFieldLabel}>Gold Loss</ThemedText>
                  <TextInput
                    style={[styles.imageMatchedInput, { textAlign: 'right' }]}
                    value={currentDetail.GoldLoss}
                    onChangeText={(text) => updateDetail('GoldLoss', text)}
                    keyboardType="numeric"
                    placeholder=""
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* 5. Cost/g */}
                <View style={[styles.imageMatchedFieldCol, { width: 90 }]}>
                  <ThemedText style={styles.imageMatchedFieldLabel}>Cost/g</ThemedText>
                  <TextInput
                    style={[styles.imageMatchedInput, { textAlign: 'right' }]}
                    value={currentDetail.CostPerGram}
                    onChangeText={(text) => updateDetail('CostPerGram', text)}
                    keyboardType="numeric"
                    placeholder=""
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* 6. PCS */}
                <View style={[styles.imageMatchedFieldCol, { width: 60 }]}>
                  <ThemedText style={[styles.imageMatchedFieldLabel, { textAlign: 'center' }]}>PCS</ThemedText>
                  <TextInput
                    style={[styles.imageMatchedInput, { textAlign: 'center' }]}
                    value={currentDetail.PCS}
                    onChangeText={(text) => updateDetail('PCS', text)}
                    keyboardType="numeric"
                    placeholder="1"
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* 7. SMC */}
                <View style={[styles.imageMatchedFieldCol, { width: 85 }]}>
                  <ThemedText style={styles.imageMatchedFieldLabel}>SMC</ThemedText>
                  <TextInput
                    style={[styles.imageMatchedInput, { textAlign: 'right' }]}
                    value={currentDetail.SMC}
                    onChangeText={(text) => updateDetail('SMC', text)}
                    keyboardType="numeric"
                    placeholder=""
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* 8. GST */}
                <View style={[styles.imageMatchedFieldCol, { width: 85 }]}>
                  <ThemedText style={styles.imageMatchedFieldLabel}>GST</ThemedText>
                  <TextInput
                    style={[styles.imageMatchedInput, styles.readonlyInputStyle, { textAlign: 'right' }]}
                    value={currentDetail.GST}
                    editable={false}
                    placeholder=""
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* 9. Total Value */}
                <View style={[styles.imageMatchedFieldCol, { width: 105 }]}>
                  <ThemedText style={styles.imageMatchedFieldLabel}>Total Value</ThemedText>
                  <TextInput
                    style={[styles.imageMatchedInput, styles.readonlyInputStyle, { textAlign: 'right', fontWeight: 'bold' }]}
                    value={currentDetail.TotalValue}
                    editable={false}
                    placeholder=""
                    placeholderTextColor="#9E8D77"
                  />
                </View>
              </ScrollView>
            </View>
          </TouchableWithoutFeedback>
        </TouchableOpacity>
      </Modal>

      {/* LOV SELECTION MODAL (Placed on top so it always layers in front of lineDetailModal) */}
      <Modal visible={lovVisible} animationType="fade" transparent onRequestClose={() => setLovVisible(false)}>
        <TouchableWithoutFeedback onPress={() => setLovVisible(false)}>
          <View style={[styles.modalOverlay, { zIndex: 99999 }]}>
            <TouchableWithoutFeedback>
              <View style={[styles.lovModalContainer, { zIndex: 100000 }]}>
                <View style={styles.modalHeader}>
                  <View style={styles.headerTitleContainer}>
                    <View style={styles.headerIconBadgeSmall}>
                      <Ionicons name="search" size={14} color="#B48218" />
                    </View>
                    <ThemedText style={styles.modalTitle}>{lovTitle}</ThemedText>
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
                      value={lovSearch}
                      onChangeText={handleLovSearch}
                      autoFocus
                    />
                  </View>

                  {lovColumns.length > 0 && (
                    <View style={styles.lovHeaderRow}>
                      {lovColumns.map((col, i) => (
                        <ThemedText key={i} style={[styles.lovHeaderText, { flex: 1 }]}>
                          {col}
                        </ThemedText>
                      ))}
                    </View>
                  )}

                  <FlatList
                    data={filteredLovData}
                    keyExtractor={(_, index) => index.toString()}
                    style={{ maxHeight: 320 }}
                    renderItem={({ item, index }) => (
                      <TouchableOpacity
                        style={[
                          styles.lovItemRow,
                          index % 2 === 1 && { backgroundColor: '#FCFAF6' }
                        ]}
                        activeOpacity={0.7}
                        onPress={() => onLovSelect?.(item)}
                      >
                        {lovColumns.map((col, i) => {
                          let val = String(item[col] || '');
                          if (val.includes('T') && val.endsWith('Z')) val = val.split('T')[0];
                          return (
                            <ThemedText
                              key={i}
                              style={[
                                styles.lovItemText,
                                { flex: 1 },
                                i === 0 && { fontWeight: 'bold', color: '#8D6409' }
                              ]}
                            >
                              {val}
                            </ThemedText>
                          );
                        })}
                      </TouchableOpacity>
                    )}
                  />
                </View>
              </View>
            </TouchableWithoutFeedback>
          </View>
        </TouchableWithoutFeedback>
      </Modal>

      {/* DATE PICKER MODAL */}
      <Modal visible={datePickerVisible} transparent={true} animationType="fade" onRequestClose={() => setDatePickerVisible(false)}>
        <TouchableWithoutFeedback onPress={() => setDatePickerVisible(false)}>
          <View style={[styles.modalOverlay, { zIndex: 99999 }]}>
            <TouchableWithoutFeedback>
              {renderCalendar()}
            </TouchableWithoutFeedback>
          </View>
        </TouchableWithoutFeedback>
      </Modal>
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#FBF9F5',
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
    fontSize: 20,
    fontWeight: '400',
    letterSpacing: 1,
    color: '#3B2D17',
  },
  headerRight: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 6,
  },
  actionBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    paddingHorizontal: 10,
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
  secondaryGoldBtn: {
    backgroundColor: '#FAF5EA',
    borderColor: '#D8C59A',
  },
  secondaryGoldBtnText: {
    color: '#9E6D08',
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

  // Customer Type Button
  customerTypeBtn: {
    backgroundColor: '#FFF6F6',
    borderWidth: 1,
    borderColor: '#F2A8A2',
    borderRadius: 5,
    paddingHorizontal: 8,
    paddingVertical: 4,
    justifyContent: 'center',
    alignItems: 'center',
  },
  customerTypeBtnText: {
    color: '#D93829',
    fontSize: 11,
    fontWeight: 'bold',
  },

  // ===== CONTENT BODY =====
  contentBody: {
    flex: 1,
    flexDirection: 'row',
    padding: 8,
    gap: 8,
  },
  leftPane: {
    flex: 1,
    flexDirection: 'column',
  },
  rightPane: {
    width: 230,
    flexDirection: 'column',
  },
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
  cardBody: {
    padding: 8,
  },

  // ===== CLASS RATES CARD =====
  classRatesCard: {
    width: 170,
  },
  classRatesTableHeader: {
    flexDirection: 'row',
    backgroundColor: '#FCFAF6',
    paddingHorizontal: 8,
    paddingVertical: 5,
    borderBottomWidth: 1,
    borderBottomColor: '#F0E6D4',
  },
  classRatesHeadCell: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#6E5528',
  },
  classRateRow: {
    flexDirection: 'row',
    paddingHorizontal: 8,
    paddingVertical: 5,
    borderBottomWidth: 1,
    borderBottomColor: '#F5EFE4',
  },
  classRateRowSelected: {
    backgroundColor: '#FFF6DF',
    borderLeftWidth: 3,
    borderLeftColor: '#B8903B',
  },
  classRateText: {
    fontSize: 11,
    color: '#3B2D17',
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
  formRow: {
    flexDirection: 'row',
    alignItems: 'center',
    minWidth: 0,
  },
  formLabel: {
    fontSize: 11,
    fontWeight: '600',
    color: '#4A3B2C',
    marginRight: 10,
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
    paddingHorizontal: 6,
    paddingVertical: 0,
    fontSize: 11,
    color: '#2B2118',
    flex: 1,
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

  // Custom Controls
  radioContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    marginRight: 6,
  },
  radioCircle: {
    width: 14,
    height: 14,
    borderRadius: 7,
    borderWidth: 1,
    borderColor: '#C4B69C',
    alignItems: 'center',
    justifyContent: 'center',
    marginRight: 4,
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
  checkboxSquare: {
    width: 14,
    height: 14,
    borderRadius: 3,
    borderWidth: 1,
    borderColor: '#C4B69C',
    alignItems: 'center',
    justifyContent: 'center',
    marginRight: 4,
  },
  checkboxSquareSelected: {
    backgroundColor: '#B8903B',
    borderColor: '#B8903B',
  },
  radioLabel: {
    fontSize: 11,
    color: '#3B2D17',
  },

  // ===== 100% FULL-WIDTH DATA TABLE =====
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

  // ===== ITEM INPUT ROW =====
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

  // ===== RIGHT SUMMARY CARD =====
  saleBadgeCard: {
    backgroundColor: '#F5E7CA',
    borderWidth: 1,
    borderColor: '#E2D3B3',
    borderRadius: 8,
    paddingVertical: 8,
    alignItems: 'center',
  },
  saleBadgeText: {
    fontSize: 13,
    fontWeight: 'bold',
    color: '#8D6409',
    letterSpacing: 0.5,
  },
  totalRow: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    paddingVertical: 5,
    borderBottomWidth: 1,
    borderBottomColor: '#F5EFE4',
  },
  totalLbl: {
    fontSize: 11,
    fontWeight: '600',
    color: '#5C4A28',
    marginRight: 10,
  },
  totalVal: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3B2D17',
  },
  grandTotalCard: {
    backgroundColor: '#FFF8EA',
    borderWidth: 1,
    borderColor: '#B8903B',
    borderRadius: 8,
    marginTop: 8,
    padding: 14,
    alignItems: 'center',
    shadowColor: '#B8903B',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.15,
    shadowRadius: 4,
    elevation: 3,
  },
  grandTotalLabel: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#8D6409',
    textTransform: 'uppercase',
    letterSpacing: 0.5,
  },
  grandTotalValue: {
    fontSize: 24,
    fontWeight: 'bold',
    color: '#9E6D08',
    marginTop: 4,
  },

  // ===== FOOTER AUDIT STATUS BAR =====
  footerStatusBar: {
    backgroundColor: '#FFFFFF',
    borderTopWidth: 1,
    borderTopColor: '#EBE2D2',
    flexDirection: 'row',
    paddingVertical: 6,
    paddingHorizontal: 8,
  },
  footerCol: {
    flex: 1,
    alignItems: 'center',
    borderRightWidth: 1,
    borderRightColor: '#F2E8D7',
  },
  footerLabel: {
    fontSize: 9,
    fontWeight: '600',
    color: '#8F816E',
    textTransform: 'uppercase',
  },
  footerVal: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3B2D17',
    marginTop: 1,
  },

  // ===== MODALS & CALENDAR =====
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
    maxWidth: 620,
    backgroundColor: '#FFFFFF',
    borderRadius: 10,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    overflow: 'hidden',
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

  // Calendar
  calendarContainer: {
    width: 290,
    backgroundColor: '#FFFFFF',
    borderRadius: 10,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    padding: 14,
  },
  calendarHeader: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    marginBottom: 12,
  },
  calendarTitle: {
    fontWeight: 'bold',
    fontSize: 13,
    color: '#3B2D17',
  },
  calendarDayHeader: {
    width: 37,
    textAlign: 'center',
    fontWeight: 'bold',
    fontSize: 11,
    color: '#8D6409',
    marginBottom: 6,
  },
  calendarDayCell: {
    width: 37,
    height: 32,
    justifyContent: 'center',
    alignItems: 'center',
    borderRadius: 6,
  },
  calendarDayCellActive: {
    backgroundColor: '#B8903B',
  },
  calendarDayText: {
    fontSize: 12,
    color: '#3B2D17',
  },
  calendarDayTextActive: {
    color: '#FFFFFF',
    fontWeight: 'bold',
  },

  // Line Detail Modal Styling matching user screenshot
  imageMatchedModalCard: {
    width: '98%',
    maxWidth: 1400,
    backgroundColor: '#FFFDF9',
    borderRadius: 8,
    borderWidth: 1.5,
    borderColor: '#E8DCB8',
    overflow: 'hidden',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.15,
    shadowRadius: 10,
    elevation: 8,
  },
  imageMatchedHeaderBanner: {
    backgroundColor: '#F7ECCF',
    paddingHorizontal: 12,
    paddingVertical: 8,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    borderBottomWidth: 1,
    borderBottomColor: '#E6D7B4',
  },
  imageMatchedHeaderLeft: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 8,
  },
  imageMatchedIconBox: {
    width: 26,
    height: 26,
    borderRadius: 5,
    borderWidth: 1,
    borderColor: '#D8C59A',
    backgroundColor: '#FFFDF9',
    justifyContent: 'center',
    alignItems: 'center',
  },
  imageMatchedHeaderTitle: {
    fontSize: 13,
    fontWeight: 'bold',
    color: '#3B2D17',
  },
  imageMatchedHeaderActions: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 8,
  },
  imageMatchedClearBtn: {
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#CBB27E',
    borderRadius: 6,
    paddingVertical: 5,
    paddingHorizontal: 14,
  },
  imageMatchedClearBtnText: {
    color: '#634917',
    fontSize: 12,
    fontWeight: 'bold',
  },
  imageMatchedAddBtn: {
    backgroundColor: '#FFF8E7',
    borderWidth: 1,
    borderColor: '#B8903B',
    borderRadius: 6,
    paddingVertical: 5,
    paddingHorizontal: 14,
  },
  imageMatchedAddBtnText: {
    color: '#8D6409',
    fontSize: 12,
    fontWeight: 'bold',
  },
  imageMatchedCloseBtn: {
    marginLeft: 6,
    padding: 4,
    borderRadius: 4,
    backgroundColor: '#EFE2C2',
  },
  imageMatchedBodyRow: {
    flexDirection: 'row',
    alignItems: 'center',
    flexGrow: 1,
    gap: 8,
    paddingHorizontal: 14,
    paddingVertical: 14,
    backgroundColor: '#FFFFFF',
  },
  imageMatchedFieldCol: {
    justifyContent: 'flex-start',
  },
  imageMatchedFieldLabel: {
    fontSize: 11,
    fontWeight: '600',
    color: '#554428',
    marginBottom: 5,
  },
  imageMatchedInputWithBtn: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  imageMatchedInput: {
    height: 32,
    borderWidth: 1,
    borderColor: '#D8CDBA',
    borderRadius: 5,
    paddingHorizontal: 8,
    paddingVertical: 0,
    fontSize: 11,
    color: '#3B2D17',
    backgroundColor: '#FFFFFF',
  },
  imageMatchedStandaloneLookupBtn: {
    width: 28,
    height: 32,
    backgroundColor: '#F7EDD5',
    borderWidth: 1,
    borderColor: '#D8CDBA',
    borderRadius: 5,
    justifyContent: 'center',
    alignItems: 'center',
  },
  imageMatchedLookupBtn: {
    width: 28,
    height: 32,
    backgroundColor: '#F5EBD4',
    borderWidth: 1,
    borderLeftWidth: 0,
    borderColor: '#D8CDBA',
    borderTopRightRadius: 5,
    borderBottomRightRadius: 5,
    justifyContent: 'center',
    alignItems: 'center',
  },
  imageMatchedLookupBtnText: {
    color: '#554428',
    fontWeight: 'bold',
    fontSize: 12,
  },
  readonlyInputStyle: {
    backgroundColor: '#FAF7F0',
    borderColor: '#E8DEC8',
    color: '#6E5528',
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
  },
  toastText: {
    color: '#FFFFFF',
    fontWeight: 'bold',
    fontSize: 12,
  },
});
