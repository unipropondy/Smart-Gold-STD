import { Ionicons, MaterialCommunityIcons } from '@expo/vector-icons';
import { Stack, useRouter } from 'expo-router';
import React, { useEffect, useState } from 'react';
import {
  ActivityIndicator,
  Alert,
  FlatList,
  Modal,
  Platform,
  ScrollView,
  StyleSheet,
  Text,
  TextInput,
  TouchableOpacity,
  View,
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';
import { API_BASE_URL } from '@/constants/api';

interface SchemeMasterItem {
  Code: string;
  Description: string;
  Remarks: string;
}

interface TransactionRow {
  InvoiceNo: string;
  InvoiceDate: string;
  SchemeAmount: number;
  DailyRate: number;
  WeightAccured: number;
}

export default function GoldSchemeTransactionScreen() {
  const router = useRouter();

  // Salesman State
  const [salesmanPassword, setSalesmanPassword] = useState('');
  const [salesmanName, setSalesmanName] = useState('');
  const [salesmanCode, setSalesmanCode] = useState('');
  const [validatingSalesman, setValidatingSalesman] = useState(false);

  // Invoice Date
  const today = new Date();
  const [invoiceDay, setInvoiceDay] = useState(String(today.getDate()).padStart(2, '0'));
  const [invoiceMonth, setInvoiceMonth] = useState(String(today.getMonth() + 1).padStart(2, '0'));
  const [invoiceYear, setInvoiceYear] = useState(String(today.getFullYear()));

  // Scheme No
  const [schemeNo, setSchemeNo] = useState('');
  const [fetchingSchemeNo, setFetchingSchemeNo] = useState(false);

  // Customer Details
  const [customerCode, setCustomerCode] = useState('');
  const [customerName, setCustomerName] = useState('');
  const [customerType, setCustomerType] = useState('');
  const [address1, setAddress1] = useState('');
  const [address2, setAddress2] = useState('');
  const [address3, setAddress3] = useState('');
  const [searchingCustomer, setSearchingCustomer] = useState(false);

  // Scheme Details
  const [schemeCode, setSchemeCode] = useState('');
  const [schemeValue, setSchemeValue] = useState('');
  const [mode, setMode] = useState<'VALUE' | 'GRAM'>('VALUE');
  const [schemeModalVisible, setSchemeModalVisible] = useState(false);
  const [schemeList, setSchemeList] = useState<SchemeMasterItem[]>([]);
  const [loadingSchemes, setLoadingSchemes] = useState(false);

  // Transaction Table Data
  const [transactions, setTransactions] = useState<TransactionRow[]>([]);
  const [loadingTransactions, setLoadingTransactions] = useState(false);

  // General Action State
  const [saving, setSaving] = useState(false);

  // Modal Visibility States
  const [customerModalVisible, setCustomerModalVisible] = useState(false);
  const [customerDetailsVisible, setCustomerDetailsVisible] = useState(false);

  // Customer Help Search Modal States (Image 1)
  const [custSearchField, setCustSearchField] = useState<'CustomerName' | 'CustomerCode' | 'NricNo' | 'Address1'>('CustomerName');
  const [showCustFieldDropdown, setShowCustFieldDropdown] = useState(false);
  const [custSearchText, setCustSearchText] = useState('');
  const [custSearchMode, setCustSearchMode] = useState<'Start' | 'End' | 'Any'>('Start');
  const [customerSearchResults, setCustomerSearchResults] = useState<any[]>([]);
  const [custTotalRecords, setCustTotalRecords] = useState(0);
  const [loadingCustomerSearch, setLoadingCustomerSearch] = useState(false);
  const [selectedCustIndex, setSelectedCustIndex] = useState<number | null>(null);

  // Scheme Help Search Modal States (Image 3)
  const [schemeSearchField, setSchemeSearchField] = useState<'Description' | 'Code' | 'Remarks' | 'CreateUser'>('Description');
  const [showSchemeFieldDropdown, setShowSchemeFieldDropdown] = useState(false);
  const [schemeSearchText, setSchemeSearchText] = useState('');
  const [schemeSearchMode, setSchemeSearchMode] = useState<'Start' | 'End' | 'Any'>('Start');
  const [schemeTotalRecords, setSchemeTotalRecords] = useState(0);
  const [selectedSchemeIndex, setSelectedSchemeIndex] = useState<number | null>(null);

  // New / Edit Customer Details Screen Form States (Image 2)
  const [newCustCode, setNewCustCode] = useState('');
  const [newCustType, setNewCustType] = useState('DIAMOND');
  const [newCustName, setNewCustName] = useState('');
  const [newCustAddress, setNewCustAddress] = useState('');
  const [newCustCountry, setNewCustCountry] = useState('');
  const [newCustPostalCode, setNewCustPostalCode] = useState('');
  const [newCustNationality, setNewCustNationality] = useState('');
  const [newCustGender, setNewCustGender] = useState<'Male' | 'Female'>('Male');
  const [newCustNric, setNewCustNric] = useState('');
  const [newCustRemarks, setNewCustRemarks] = useState('');
  const [newCustPassport, setNewCustPassport] = useState('');
  const [newCustPhone1, setNewCustPhone1] = useState('');
  const [newCustPhone2, setNewCustPhone2] = useState('');
  const [newCustMobile, setNewCustMobile] = useState('');
  const [newCustFax, setNewCustFax] = useState('');
  const [newCustEmail, setNewCustEmail] = useState('');
  const [newCustReligion, setNewCustReligion] = useState('');
  const [newCustDobDay, setNewCustDobDay] = useState('');
  const [newCustDobMonth, setNewCustDobMonth] = useState('');
  const [newCustDobYear, setNewCustDobYear] = useState('');
  const [newCustDoaDay, setNewCustDoaDay] = useState('');
  const [newCustDoaMonth, setNewCustDoaMonth] = useState('');
  const [newCustDoaYear, setNewCustDoaYear] = useState('');
  const [newCustActive, setNewCustActive] = useState(true);
  const [newCustContact, setNewCustContact] = useState<'SMS' | 'Phone' | 'None'>('None');
  const [addKyc, setAddKyc] = useState(false);
  const [showKyc, setShowKyc] = useState(false);
  const [savingCustomer, setSavingCustomer] = useState(false);

  // Open Customer Help Search Modal (Image 1)
  const handleOpenCustomerModal = async () => {
    setCustomerModalVisible(true);
    fetchCustomerSearchResults('CustomerName', custSearchText, custSearchMode);
  };

  // Fetch Customer Search API
  const fetchCustomerSearchResults = async (field = custSearchField, text = custSearchText, mode = custSearchMode) => {
    setLoadingCustomerSearch(true);
    try {
      const url = `${API_BASE_URL}/scheme/customers?searchField=${field}&searchText=${encodeURIComponent(text)}&searchMode=${mode}`;
      const res = await fetch(url);
      const json = await res.json();
      if (json.success && Array.isArray(json.data)) {
        setCustomerSearchResults(json.data);
        setCustTotalRecords(json.total != null ? json.total : json.data.length);
      } else {
        setCustomerSearchResults([]);
        setCustTotalRecords(0);
      }
    } catch (err) {
      console.error('Error in customer search:', err);
      setCustomerSearchResults([]);
      setCustTotalRecords(0);
    } finally {
      setLoadingCustomerSearch(false);
    }
  };

  // Customer Row Selected from Search Modal
  const handleSelectCustomerRow = (custRow: any) => {
    const code = custRow.CustomerCode;
    setCustomerModalVisible(false);
    setCustomerCode(code);
    fetchCustomerDetailsByCode(code);
  };

  // Fetch Customer Details By Code
  const fetchCustomerDetailsByCode = async (code: string) => {
    if (!code || !code.trim()) return;
    setSearchingCustomer(true);
    try {
      const res = await fetch(`${API_BASE_URL}/scheme/customer-detail/${encodeURIComponent(code.trim())}`);
      const json = await res.json();
      if (json.success && json.customer) {
        const c = json.customer;
        setCustomerCode(c.customerCode || '');
        setCustomerName(c.customerName || '');
        setCustomerType(c.customerType || '');
        setAddress1(c.address1 || '');
        setAddress2(c.address2 || '');
        setAddress3(c.address3 || '');

        // Fetch transaction history for this customer
        fetchTransactionHistory(c.customerCode);
      }
    } catch (err) {
      console.error('Error fetching customer details:', err);
    } finally {
      setSearchingCustomer(false);
    }
  };

  // Open Scheme Lookup Modal (Image 3)
  const handleOpenSchemeModal = async () => {
    setSchemeModalVisible(true);
    fetchSchemeSearchResults('Description', schemeSearchText, schemeSearchMode);
  };

  // Fetch Scheme Search API
  const fetchSchemeSearchResults = async (field = schemeSearchField, text = schemeSearchText, mode = schemeSearchMode) => {
    setLoadingSchemes(true);
    try {
      const url = `${API_BASE_URL}/scheme/master?searchField=${field}&searchText=${encodeURIComponent(text)}&searchMode=${mode}`;
      const res = await fetch(url);
      const json = await res.json();
      if (json.success && Array.isArray(json.data)) {
        setSchemeList(json.data);
        setSchemeTotalRecords(json.total != null ? json.total : json.data.length);
      } else {
        setSchemeList([]);
        setSchemeTotalRecords(0);
      }
    } catch (err) {
      console.error('Error fetching scheme master search:', err);
      setSchemeList([]);
      setSchemeTotalRecords(0);
    } finally {
      setLoadingSchemes(false);
    }
  };

  // Select Scheme Row
  const handleSelectSchemeRow = (scheme: any) => {
    setSchemeCode(scheme.Code ? scheme.Code.trim() : '');
    setSchemeValue(scheme.Value ? scheme.Value.trim() : '0');
    setSchemeModalVisible(false);
  };

  // Open New Customer Creation Screen / Modal (Image 2)
  const handleOpenNewCustomer = () => {
    setNewCustCode('');
    setNewCustType('DIAMOND');
    setNewCustName('');
    setNewCustAddress('');
    setNewCustCountry('');
    setNewCustPostalCode('');
    setNewCustNationality('');
    setNewCustGender('Male');
    setNewCustNric('');
    setNewCustRemarks('');
    setNewCustPassport('');
    setNewCustPhone1('');
    setNewCustPhone2('');
    setNewCustMobile('');
    setNewCustFax('');
    setNewCustEmail('');
    setNewCustReligion('');
    setNewCustDobDay('');
    setNewCustDobMonth('');
    setNewCustDobYear('');
    setNewCustDoaDay('');
    setNewCustDoaMonth('');
    setNewCustDoaYear('');
    setNewCustActive(true);
    setNewCustContact('None');
    setCustomerDetailsVisible(true);
  };

  // Save New Customer to SQL Database
  const handleSaveNewCustomer = async () => {
    if (!newCustCode.trim()) {
      const msg = 'Please enter Customer Code.';
      Platform.OS === 'web' ? window.alert(msg) : Alert.alert('Validation Error', msg);
      return;
    }
    if (!newCustName.trim()) {
      const msg = 'Please enter Customer Name.';
      Platform.OS === 'web' ? window.alert(msg) : Alert.alert('Validation Error', msg);
      return;
    }

    setSavingCustomer(true);
    try {
      let dobStr = null;
      if (newCustDobYear && newCustDobMonth && newCustDobDay) {
        dobStr = `${newCustDobYear}-${newCustDobMonth.padStart(2, '0')}-${newCustDobDay.padStart(2, '0')}`;
      }
      let doaStr = null;
      if (newCustDoaYear && newCustDoaMonth && newCustDoaDay) {
        doaStr = `${newCustDoaYear}-${newCustDoaMonth.padStart(2, '0')}-${newCustDoaDay.padStart(2, '0')}`;
      }

      const payload = {
        CustomerCode: newCustCode.trim(),
        CustomerName: newCustName.trim(),
        CustomerType: newCustType,
        Address1: newCustAddress.trim(),
        Address2: newCustCountry.trim(),
        Address3: newCustPostalCode.trim(),
        Address4: newCustNationality.trim(),
        Phone1: newCustPhone1.trim(),
        Phone2: newCustPhone2.trim(),
        Mobile: newCustMobile.trim(),
        Fax: newCustFax.trim(),
        eMail: newCustEmail.trim(),
        AccountType: newCustContact === 'SMS' ? 'S' : newCustContact === 'Phone' ? 'P' : 'N',
        GLAccount1: newCustActive ? 'Y' : 'N',
        Remarks: newCustRemarks.trim(),
        NRICNo: newCustNric.trim(),
        Gender: newCustGender === 'Male' ? 'M' : 'F',
        PassportNo: newCustPassport.trim(),
        DOB: dobStr,
        DOA: doaStr,
        DateType: newCustReligion.trim()
      };

      const res = await fetch(`${API_BASE_URL}/customer`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload),
      });
      const json = await res.json();

      if (json.success) {
        const msg = 'Customer created successfully!';
        Platform.OS === 'web' ? window.alert(msg) : Alert.alert('Success', msg);
        setCustomerDetailsVisible(false);

        // Auto populate created customer in scheme transaction screen
        const createdCode = newCustCode.trim();
        setCustomerCode(createdCode);
        fetchCustomerDetailsByCode(createdCode);
      } else {
        const msg = json.message || 'Failed to create customer.';
        Platform.OS === 'web' ? window.alert(msg) : Alert.alert('Error', msg);
      }
    } catch (err) {
      console.error('Error saving customer:', err);
      const msg = 'Unable to connect to server.';
      Platform.OS === 'web' ? window.alert(msg) : Alert.alert('Error', msg);
    } finally {
      setSavingCustomer(false);
    }
  };

  // Initial Load: Fetch Next Scheme Number
  useEffect(() => {
    fetchNextSchemeNo();
  }, []);

  // Keyboard Shortcuts Listener for Web (F6, F7, F9, F10, Esc)
  useEffect(() => {
    if (Platform.OS === 'web') {
      const handleKeyDown = (e: KeyboardEvent) => {
        if (e.key === 'F6') {
          e.preventDefault();
          handleClear();
        } else if (e.key === 'F7') {
          e.preventDefault();
          handleSave();
        } else if (e.key === 'F9') {
          e.preventDefault();
          handleReprint();
        } else if (e.key === 'F10') {
          e.preventDefault();
          handleCancel();
        } else if (e.key === 'Escape') {
          e.preventDefault();
          router.back();
        }
      };
      window.addEventListener('keydown', handleKeyDown);
      return () => window.removeEventListener('keydown', handleKeyDown);
    }
  }, [salesmanCode, customerCode, schemeCode, schemeValue, mode, schemeNo]);

  // Fetch Next Scheme Number
  const fetchNextSchemeNo = async () => {
    setFetchingSchemeNo(true);
    try {
      const res = await fetch(`${API_BASE_URL}/scheme/next-number`);
      const json = await res.json();
      if (json.success && json.schemeNo) {
        setSchemeNo(json.schemeNo);
      }
    } catch (err) {
      console.error('Error fetching next scheme no:', err);
    } finally {
      setFetchingSchemeNo(false);
    }
  };

  // Validate Salesman Password
  const handleValidateSalesman = async () => {
    if (!salesmanPassword.trim()) {
      const msg = 'Please enter Sales Man password.';
      Platform.OS === 'web' ? window.alert(msg) : Alert.alert('Validation Error', msg);
      return;
    }

    setValidatingSalesman(true);
    try {
      const res = await fetch(`${API_BASE_URL}/scheme/salesman/validate`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ password: salesmanPassword.trim() }),
      });
      const json = await res.json();

      if (json.success) {
        setSalesmanName(json.salesManName);
        setSalesmanCode(json.salesmanCode);
      } else {
        setSalesmanName('');
        setSalesmanCode('');
        const msg = json.message || 'Invalid Password.';
        Platform.OS === 'web' ? window.alert(msg) : Alert.alert('Validation Error', msg);
      }
    } catch (err) {
      console.error('Error validating salesman:', err);
      const msg = 'Unable to connect to server.';
      Platform.OS === 'web' ? window.alert(msg) : Alert.alert('Error', msg);
    } finally {
      setValidatingSalesman(false);
    }
  };

  // Customer Lookup
  const handleSearchCustomer = async () => {
    if (!customerCode.trim()) {
      const msg = 'Please enter Customer Code.';
      Platform.OS === 'web' ? window.alert(msg) : Alert.alert('Validation', msg);
      return;
    }

    setSearchingCustomer(true);
    try {
      const res = await fetch(`${API_BASE_URL}/scheme/customer/${encodeURIComponent(customerCode.trim())}`);
      const json = await res.json();

      if (json.success && json.customer) {
        const c = json.customer;
        setCustomerCode(c.customerCode);
        setCustomerName(c.customerName);
        setCustomerType(c.customerType);
        setAddress1(c.address1);
        setAddress2(c.address2);
        setAddress3(c.address3);

        // Fetch Customer Transaction History
        fetchTransactionHistory(c.customerCode);
      }
    } catch (err) {
      console.error('Error searching customer:', err);
      const msg = 'Unable to connect to server.';
      Platform.OS === 'web' ? window.alert(msg) : Alert.alert('Error', msg);
    } finally {
      setSearchingCustomer(false);
    }
  };

  // Fetch Transaction History for Customer
  const fetchTransactionHistory = async (code: string) => {
    if (!code) return;
    setLoadingTransactions(true);
    try {
      const res = await fetch(`${API_BASE_URL}/scheme/transactions/${encodeURIComponent(code)}`);
      const json = await res.json();
      if (json.success && Array.isArray(json.data)) {
        setTransactions(json.data);
      } else {
        setTransactions([]);
      }
    } catch (err) {
      console.error('Error fetching transaction history:', err);
    } finally {
      setLoadingTransactions(false);
    }
  };


  // Select Scheme from Modal
  const handleSelectScheme = (scheme: SchemeMasterItem) => {
    setSchemeCode(scheme.Code);
    setSchemeValue(scheme.Remarks || '0');
    setSchemeModalVisible(false);
  };

  // Clear Form (F6)
  const handleClear = () => {
    setSalesmanPassword('');
    setSalesmanName('');
    setSalesmanCode('');
    setCustomerCode('');
    setCustomerName('');
    setCustomerType('');
    setAddress1('');
    setAddress2('');
    setAddress3('');
    setSchemeCode('');
    setSchemeValue('');
    setMode('VALUE');
    setTransactions([]);
    fetchNextSchemeNo();
  };

  // Save Transaction (F7)
  const handleSave = async () => {
    if (!salesmanCode) {
      const msg = 'Please validate Sales Man password first.';
      Platform.OS === 'web' ? window.alert(msg) : Alert.alert('Validation Error', msg);
      return;
    }
    if (!customerCode.trim()) {
      const msg = 'Customer Code is required.';
      Platform.OS === 'web' ? window.alert(msg) : Alert.alert('Validation Error', msg);
      return;
    }
    if (!schemeCode.trim()) {
      const msg = 'Scheme Code is required.';
      Platform.OS === 'web' ? window.alert(msg) : Alert.alert('Validation Error', msg);
      return;
    }

    setSaving(true);
    try {
      const payload = {
        customerCode: customerCode.trim(),
        customerName,
        customerType,
        salesmanCode,
        schemeCode: schemeCode.trim(),
        schemeValue: schemeValue.trim(),
        mode,
        paymentMode: 'CASH',
        paymentAmount: parseFloat(schemeValue) || 0,
      };

      const res = await fetch(`${API_BASE_URL}/scheme/transaction`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload),
      });
      const json = await res.json();

      if (json.success) {
        const msg = json.message || 'Scheme Transaction saved successfully!';
        Platform.OS === 'web' ? window.alert(msg) : Alert.alert('Success', msg);
        fetchTransactionHistory(customerCode);
        fetchNextSchemeNo();
      } else {
        const msg = json.message || 'Failed to save Scheme Transaction.';
        Platform.OS === 'web' ? window.alert(msg) : Alert.alert('Error', msg);
      }
    } catch (err) {
      console.error('Error saving scheme transaction:', err);
      const msg = 'Unable to connect to server.';
      Platform.OS === 'web' ? window.alert(msg) : Alert.alert('Error', msg);
    } finally {
      setSaving(false);
    }
  };

  // Cancel Transaction (F10)
  const handleCancel = async () => {
    if (!schemeNo) {
      const msg = 'No active Scheme No to cancel.';
      Platform.OS === 'web' ? window.alert(msg) : Alert.alert('Validation Error', msg);
      return;
    }
    try {
      const res = await fetch(`${API_BASE_URL}/scheme/cancel`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ schemeNo }),
      });
      const json = await res.json();
      if (json.success) {
        const msg = 'Scheme Transaction cancelled.';
        Platform.OS === 'web' ? window.alert(msg) : Alert.alert('Success', msg);
        handleClear();
      } else {
        const msg = json.message || 'Failed to cancel scheme.';
        Platform.OS === 'web' ? window.alert(msg) : Alert.alert('Error', msg);
      }
    } catch (err) {
      console.error('Error cancelling scheme:', err);
    }
  };

  const [selectedTxIndex, setSelectedTxIndex] = useState<number | null>(null);

  // Reprint (F9)
  const handleReprint = async () => {
    let targetInvoiceNo = '';
    if (selectedTxIndex !== null && transactions[selectedTxIndex]?.InvoiceNo) {
      targetInvoiceNo = transactions[selectedTxIndex].InvoiceNo;
    } else if (transactions.length > 0 && transactions[0]?.InvoiceNo) {
      targetInvoiceNo = transactions[0].InvoiceNo;
    } else if (schemeNo && schemeNo.trim()) {
      targetInvoiceNo = schemeNo;
    }

    console.log("Reprint InvoiceNo requested:", targetInvoiceNo);

    if (!targetInvoiceNo || !targetInvoiceNo.trim()) {
      const msg = 'Please select a transaction from the table to reprint.';
      Platform.OS === 'web' ? window.alert(msg) : Alert.alert('Validation Error', msg);
      return;
    }
    try {
      const res = await fetch(`${API_BASE_URL}/scheme/reprint/${encodeURIComponent(targetInvoiceNo.trim())}`);
      const json = await res.json();
      if (json.success && json.data) {
        const inv = json.data.InvoiceNo || targetInvoiceNo;
        const amt = json.data.InvoiceAmount != null ? Number(json.data.InvoiceAmount).toFixed(2) : '0.00';
        const msg = `Reprint Receipt Ready!\nInvoice No: ${inv}\nAmount: $${amt}\nDate: ${json.data.InvoiceDate || ''}`;
        Platform.OS === 'web' ? window.alert(msg) : Alert.alert('Reprint Receipt', msg);
      } else {
        const msg = json.message || 'Transaction record not found for reprint.';
        Platform.OS === 'web' ? window.alert(msg) : Alert.alert('Not Found', msg);
      }
    } catch (err) {
      console.error('Error during reprint:', err);
      const msg = 'Unable to connect to server for reprint.';
      Platform.OS === 'web' ? window.alert(msg) : Alert.alert('Error', msg);
    }
  };

  return (
    <SafeAreaView style={styles.safeArea} edges={['top', 'left', 'right']}>
      <Stack.Screen options={{ headerShown: false }} />

      {/* TOP HEADER BAR */}
      <View style={styles.topHeader}>
        <View style={styles.headerLeft}>
          <TouchableOpacity style={styles.backButton} onPress={() => router.back()}>
            <Ionicons name="arrow-back" size={20} color="#3B2D17" />
          </TouchableOpacity>
          <Text style={styles.headerTitle}>Gold Scheme Transaction Detail</Text>
        </View>
      </View>

      <ScrollView contentContainerStyle={styles.scrollContainer} keyboardShouldPersistTaps="handled">
        <View style={styles.mainGrid}>

          {/* CARD 1: Customer & Transaction Details */}
          <View style={styles.cardSection}>
            <View style={styles.sectionHeader}>
              <View style={styles.headerIconBadge}>
                <MaterialCommunityIcons name="card-account-details-outline" size={16} color="#9E6D08" />
              </View>
              <Text style={styles.sectionTitle}>Customer & Transaction Details</Text>
            </View>

            <View style={styles.cardBody}>

              {/* 1. Sales Man */}
              <View style={styles.formRow}>
                <Text style={styles.label}>
                  Sales Man <Text style={styles.requiredStar}>*</Text>
                </Text>
                <View style={styles.flexRow}>
                  <View style={styles.inputWrapperWithButton}>
                    <TextInput
                      style={styles.inputFlex}
                      placeholder="Enter Sales Man Password"
                      placeholderTextColor="#94A3B8"
                      secureTextEntry
                      value={salesmanPassword}
                      onChangeText={setSalesmanPassword}
                      onSubmitEditing={handleValidateSalesman}
                    />
                    <TouchableOpacity style={styles.inlineSearchBtn} onPress={handleValidateSalesman} disabled={validatingSalesman}>
                      {validatingSalesman ? (
                        <ActivityIndicator size="small" color="#FFFFFF" />
                      ) : (
                        <Ionicons name="checkmark" size={18} color="#FFFFFF" />
                      )}
                    </TouchableOpacity>
                  </View>
                  <View style={[styles.readOnlyBox, { flex: 1.2, marginLeft: 10 }]}>
                    <Text style={styles.readOnlyText} numberOfLines={1}>
                      {salesmanName || '--'}
                    </Text>
                  </View>
                </View>
              </View>

              {/* 2. Invoice Date & Scheme No */}
              <View style={styles.formRow}>
                <Text style={styles.label}>
                  Invoice Date <Text style={styles.requiredStar}>*</Text>
                </Text>
                <View style={styles.flexRow}>
                  <View style={styles.dateTripleRow}>
                    <View style={styles.dateTripleBox}>
                      <Text style={styles.dateTripleText}>{invoiceDay}</Text>
                      <Ionicons name="caret-down" size={12} color="#8F7E66" />
                    </View>
                    <Text style={styles.dateSeparator}>/</Text>
                    <View style={styles.dateTripleBox}>
                      <Text style={styles.dateTripleText}>{invoiceMonth}</Text>
                      <Ionicons name="caret-down" size={12} color="#8F7E66" />
                    </View>
                    <Text style={styles.dateSeparator}>/</Text>
                    <View style={styles.dateTripleBox}>
                      <Text style={styles.dateTripleText}>{invoiceYear}</Text>
                      <Ionicons name="caret-down" size={12} color="#8F7E66" />
                    </View>
                    <View style={styles.calendarIconBox}>
                      <Ionicons name="calendar-outline" size={18} color="#8F7E66" />
                    </View>
                  </View>

                  <Text style={[styles.inlineLabel, { marginLeft: 20, marginRight: 8 }]}>Scheme No</Text>
                  <View style={[styles.readOnlyBox, { flex: 1 }]}>
                    {fetchingSchemeNo ? (
                      <ActivityIndicator size="small" color="#9E6D08" />
                    ) : (
                      <Text style={styles.readOnlyText}>{schemeNo || '--'}</Text>
                    )}
                  </View>

                  <Text style={[styles.inlineLabel, { marginLeft: 16, marginRight: 8 }]}>Customer Type</Text>
                  <View style={[styles.readOnlyBox, { flex: 0.8 }]}>
                    <Text style={styles.readOnlyText}>{customerType || '--'}</Text>
                  </View>
                </View>
              </View>

              {/* 3. Customer * */}
              <View style={styles.formRow}>
                <Text style={styles.label}>
                  Customer <Text style={styles.requiredStar}>*</Text>
                </Text>
                <View style={styles.inputWrapperWithButton}>
                  <TextInput
                    style={styles.inputFlex}
                    placeholder="Enter Customer Code"
                    placeholderTextColor="#94A3B8"
                    value={customerCode}
                    onChangeText={setCustomerCode}
                    onSubmitEditing={() => fetchCustomerDetailsByCode(customerCode)}
                  />
                  <TouchableOpacity style={styles.inlineSearchBtn} onPress={handleOpenCustomerModal} disabled={searchingCustomer}>
                    {searchingCustomer ? (
                      <ActivityIndicator size="small" color="#FFFFFF" />
                    ) : (
                      <Ionicons name="search" size={18} color="#FFFFFF" />
                    )}
                  </TouchableOpacity>
                </View>
              </View>

              {/* 4. Customer Name */}
              <View style={styles.formRow}>
                <Text style={styles.label}>Customer Name</Text>
                <View style={styles.readOnlyBox}>
                  <Text style={styles.readOnlyText}>{customerName || ''}</Text>
                </View>
              </View>

              {/* 5. Address */}
              <View style={styles.formRowTopAlign}>
                <Text style={[styles.label, { marginTop: 8 }]}>Address</Text>
                <TextInput
                  style={styles.addressTextArea}
                  placeholder="Enter Address"
                  placeholderTextColor="#94A3B8"
                  multiline
                  numberOfLines={3}
                  value={[address1, address2, address3].filter(Boolean).join('\n')}
                  onChangeText={(text) => {
                    const lines = text.split('\n');
                    setAddress1(lines[0] || '');
                    setAddress2(lines[1] || '');
                    setAddress3(lines.slice(2).join(' ') || '');
                  }}
                />
              </View>

              {/* New Customer Button */}
              <View style={{ flexDirection: 'row', marginTop: 4 }}>
                <TouchableOpacity style={styles.newCustomerBtn} onPress={handleOpenNewCustomer}>
                  <Ionicons name="add" size={18} color="#FFFFFF" style={{ marginRight: 6 }} />
                  <Text style={styles.newCustomerBtnText}>New Customer</Text>
                </TouchableOpacity>
              </View>

            </View>
          </View>

          {/* CARD 2: Scheme Details */}
          <View style={styles.cardSection}>
            <View style={styles.sectionHeader}>
              <View style={styles.headerIconBadge}>
                <MaterialCommunityIcons name="layers-outline" size={16} color="#9E6D08" />
              </View>
              <Text style={styles.sectionTitle}>Scheme Details</Text>
            </View>

            <View style={styles.cardBody}>
              <View style={styles.schemeRowLayout}>
                
                {/* Scheme Code */}
                <View style={{ flexDirection: 'row', alignItems: 'center', marginRight: 24 }}>
                  <Text style={styles.schemeLabel}>
                    Scheme Code <Text style={styles.requiredStar}>*</Text>
                  </Text>
                  <View style={styles.schemeInputGroup}>
                    <TextInput
                      style={styles.inputFlex}
                      placeholder="Enter Scheme Code"
                      placeholderTextColor="#94A3B8"
                      value={schemeCode}
                      onChangeText={setSchemeCode}
                    />
                    <TouchableOpacity style={styles.inlineSearchBtn} onPress={handleOpenSchemeModal}>
                      <Ionicons name="search" size={18} color="#FFFFFF" />
                    </TouchableOpacity>
                  </View>
                </View>

                {/* Scheme Value */}
                <View style={{ flexDirection: 'row', alignItems: 'center', marginRight: 24 }}>
                  <Text style={styles.schemeLabel}>Scheme Value</Text>
                  <TextInput
                    style={styles.schemeValueInput}
                    placeholder="Enter Scheme Value"
                    placeholderTextColor="#94A3B8"
                    keyboardType="numeric"
                    value={schemeValue}
                    onChangeText={setSchemeValue}
                  />
                </View>

                {/* Mode: By Value / By Gram */}
                <View style={styles.radioContainerBox}>
                  <TouchableOpacity style={styles.radioButton} onPress={() => setMode('VALUE')}>
                    <View style={[styles.radioOuter, mode === 'VALUE' && styles.radioOuterSelected]}>
                      {mode === 'VALUE' && <View style={styles.radioInner} />}
                    </View>
                    <Text style={styles.radioText}>By Value</Text>
                  </TouchableOpacity>

                  <TouchableOpacity style={styles.radioButton} onPress={() => setMode('GRAM')}>
                    <View style={[styles.radioOuter, mode === 'GRAM' && styles.radioOuterSelected]}>
                      {mode === 'GRAM' && <View style={styles.radioInner} />}
                    </View>
                    <Text style={styles.radioText}>By Gram</Text>
                  </TouchableOpacity>
                </View>

              </View>
            </View>
          </View>

          {/* CARD 3: Transaction Details Table */}
          <View style={styles.cardSection}>
            <View style={[styles.sectionHeader, { justifyContent: 'space-between' }]}>
              <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                <View style={styles.headerIconBadge}>
                  <MaterialCommunityIcons name="swap-horizontal" size={16} color="#9E6D08" />
                </View>
                <Text style={styles.sectionTitle}>Transaction Details</Text>
              </View>
              <TouchableOpacity
                style={styles.refreshBtn}
                onPress={() => fetchTransactionHistory(customerCode)}
                disabled={loadingTransactions}
              >
                <Ionicons name="refresh" size={14} color="#9E6D08" style={{ marginRight: 4 }} />
                <Text style={styles.refreshBtnText}>Refresh</Text>
              </TouchableOpacity>
            </View>

            <View style={{ padding: 0 }}>
              {/* Table Header */}
              <View style={styles.tableHeaderRow}>
                <Text style={[styles.thText, { width: 40 }]}>#</Text>
                <Text style={[styles.thText, { flex: 1.2 }]}>Invoice No</Text>
                <Text style={[styles.thText, { flex: 1.2 }]}>Invoice Date</Text>
                <Text style={[styles.thText, { flex: 1, textAlign: 'right' }]}>Scheme Amount</Text>
                <Text style={[styles.thText, { flex: 1, textAlign: 'right' }]}>Daily Rate</Text>
                <Text style={[styles.thText, { flex: 1, textAlign: 'right' }]}>Weight Accured</Text>
              </View>

              {/* Table Rows */}
              {loadingTransactions ? (
                <View style={styles.tableEmptyState}>
                  <ActivityIndicator size="small" color="#9E6D08" />
                  <Text style={styles.tableEmptyText}>Loading transactions...</Text>
                </View>
              ) : transactions.length > 0 ? (
                transactions.map((item, idx) => {
                  const isSelected = selectedTxIndex === idx;
                  return (
                    <TouchableOpacity
                      key={idx}
                      style={[styles.tableDataRow, isSelected && { backgroundColor: '#FEF3C7', borderColor: '#D97706', borderWidth: 1 }]}
                      onPress={() => setSelectedTxIndex(idx)}
                    >
                      <Text style={[styles.tdText, { width: 40 }]}>{idx + 1}</Text>
                      <Text style={[styles.tdText, { flex: 1.2, fontWeight: '600', color: isSelected ? '#92400E' : '#D97706' }]}>{item.InvoiceNo}</Text>
                      <Text style={[styles.tdText, { flex: 1.2 }]}>{item.InvoiceDate}</Text>
                      <Text style={[styles.tdText, { flex: 1, textAlign: 'right' }]}>
                        {item.SchemeAmount != null ? Number(item.SchemeAmount).toFixed(2) : '0.00'}
                      </Text>
                      <Text style={[styles.tdText, { flex: 1, textAlign: 'right' }]}>
                        {item.DailyRate != null ? Number(item.DailyRate).toFixed(2) : '0.00'}
                      </Text>
                      <Text style={[styles.tdText, { flex: 1, textAlign: 'right' }]}>
                        {item.WeightAccured != null ? Number(item.WeightAccured).toFixed(2) : '0.00'}
                      </Text>
                    </TouchableOpacity>
                  );
                })
              ) : (
                <View style={styles.tableEmptyState}>
                  <MaterialCommunityIcons name="cube-outline" size={38} color="#D8C7AA" />
                  <Text style={styles.tableEmptyText}>No data available</Text>
                </View>
              )}
            </View>
          </View>

        </View>
      </ScrollView>

      {/* ========================================================= */}
      {/* 1. CUSTOMER SEARCH HELP SEARCH MODAL (EXACT MATCH IMAGE 1) */}
      {/* ========================================================= */}
      <Modal visible={customerModalVisible} transparent animationType="fade" onRequestClose={() => setCustomerModalVisible(false)}>
        <View style={styles.legacyModalOverlay}>
          <View style={styles.legacyModalContainer}>
            
            {/* Modal Title & Close */}
            <View style={styles.legacyModalHeader}>
              <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                <MaterialCommunityIcons name="card-search-outline" size={18} color="#000" style={{ marginRight: 6 }} />
                <Text style={styles.legacyModalTitle}>Help Search</Text>
              </View>
              <TouchableOpacity onPress={() => setCustomerModalVisible(false)} style={styles.legacyModalCloseBtn}>
                <Ionicons name="close" size={16} color="#333" />
              </TouchableOpacity>
            </View>

            {/* SubHeader Search Controls */}
            <View style={styles.legacySearchControlArea}>
              
              <View style={styles.legacyTopMetaRow}>
                <Text style={styles.legacySearchByText}>Search By...</Text>
                <Text style={styles.legacyRecordCountText}>
                  <Text style={{ color: 'red', fontWeight: 'bold' }}>{custTotalRecords}</Text> Records Found
                </Text>
              </View>

              {/* Search Field Dropdown & Radio Options */}
              <View style={[styles.legacyOptionsRow, { zIndex: 10 }]}>
                <View style={{ flexDirection: 'row', alignItems: 'center', zIndex: 10 }}>
                  <Text style={styles.legacyLabelText}>Search Field </Text>
                  <TouchableOpacity 
                    style={styles.legacyDropdownSelect} 
                    onPress={() => setShowCustFieldDropdown(!showCustFieldDropdown)}
                  >
                    <Text style={styles.legacyDropdownText}>{custSearchField}</Text>
                    <Ionicons name="caret-down" size={12} color="#000" style={{ marginLeft: 4 }} />
                  </TouchableOpacity>

                  {/* Dropdown Options Menu */}
                  {showCustFieldDropdown && (
                    <View style={styles.dropdownMenuOverlay}>
                      {(['CustomerName', 'CustomerCode', 'NricNo', 'Address1'] as const).map((field) => (
                        <TouchableOpacity
                          key={field}
                          style={[styles.dropdownMenuItem, custSearchField === field && styles.dropdownMenuItemActive]}
                          onPress={() => {
                            setCustSearchField(field);
                            setShowCustFieldDropdown(false);
                            fetchCustomerSearchResults(field, custSearchText, custSearchMode);
                          }}
                        >
                          <Text style={[styles.dropdownMenuItemText, custSearchField === field && styles.dropdownMenuItemTextActive]}>
                            {field}
                          </Text>
                        </TouchableOpacity>
                      ))}
                    </View>
                  )}
                </View>

                {/* Radio Modes: Start, End, Any */}
                <View style={{ flexDirection: 'row', alignItems: 'center', marginLeft: 16 }}>
                  <TouchableOpacity style={styles.legacyRadioOption} onPress={() => { setCustSearchMode('Start'); fetchCustomerSearchResults(custSearchField, custSearchText, 'Start'); }}>
                    <View style={[styles.legacyRadioCircle, custSearchMode === 'Start' && styles.legacyRadioSelected]} />
                    <Text style={styles.legacyRadioLabel}>Start</Text>
                  </TouchableOpacity>

                  <TouchableOpacity style={styles.legacyRadioOption} onPress={() => { setCustSearchMode('End'); fetchCustomerSearchResults(custSearchField, custSearchText, 'End'); }}>
                    <View style={[styles.legacyRadioCircle, custSearchMode === 'End' && styles.legacyRadioSelected]} />
                    <Text style={styles.legacyRadioLabel}>End</Text>
                  </TouchableOpacity>

                  <TouchableOpacity style={styles.legacyRadioOption} onPress={() => { setCustSearchMode('Any'); fetchCustomerSearchResults(custSearchField, custSearchText, 'Any'); }}>
                    <View style={[styles.legacyRadioCircle, custSearchMode === 'Any' && styles.legacyRadioSelected]} />
                    <Text style={styles.legacyRadioLabel}>Any</Text>
                  </TouchableOpacity>
                </View>
              </View>

              {/* Search Field Title & Search Text Box */}
              <View style={styles.legacyInputSection}>
                <Text style={styles.legacySearchFieldColumnTitle}>{custSearchField}</Text>
                <View style={styles.legacySearchBoxRow}>
                  <TextInput
                    style={styles.legacySearchTextInput}
                    placeholder="Enter search text..."
                    value={custSearchText}
                    onChangeText={(text) => {
                      setCustSearchText(text);
                      fetchCustomerSearchResults(custSearchField, text, custSearchMode);
                    }}
                  />
                  <TouchableOpacity style={styles.legacySearchExecuteBtn} onPress={() => fetchCustomerSearchResults(custSearchField, custSearchText, custSearchMode)}>
                    <Text style={styles.legacySearchExecuteBtnText}>Search</Text>
                  </TouchableOpacity>
                </View>
              </View>
            </View>

            {/* Customer Results Grid / Table */}
            <View style={styles.legacyGridWrapper}>
              <View style={styles.legacyGridHeaderRow}>
                <Text style={[styles.legacyGridTh, { width: 140 }]}>CustomerCode</Text>
                <Text style={[styles.legacyGridTh, { width: 220 }]}>CustomerName</Text>
                <Text style={[styles.legacyGridTh, { width: 100 }]}>NricNo</Text>
                <Text style={[styles.legacyGridTh, { flex: 1, minWidth: 160 }]}>Address1</Text>
              </View>

              {loadingCustomerSearch ? (
                <View style={{ padding: 20, alignItems: 'center' }}>
                  <ActivityIndicator size="small" color="#0000FF" />
                </View>
              ) : (
                <ScrollView style={{ maxHeight: 320 }}>
                  {customerSearchResults.map((row, index) => {
                    const isSelected = selectedCustIndex === index;
                    return (
                      <TouchableOpacity
                        key={index}
                        style={[styles.legacyGridDataRow, isSelected && styles.legacyRowHighlight]}
                        onPress={() => {
                          setSelectedCustIndex(index);
                          handleSelectCustomerRow(row);
                        }}
                      >
                        <Text style={[styles.legacyGridTd, { width: 140, fontWeight: 'bold' }]}>{row.CustomerCode}</Text>
                        <Text style={[styles.legacyGridTd, { width: 220 }]}>{row.CustomerName}</Text>
                        <Text style={[styles.legacyGridTd, { width: 100 }]}>{row.NricNo || ''}</Text>
                        <Text style={[styles.legacyGridTd, { flex: 1, minWidth: 160 }]}>{row.Address1 || ''}</Text>
                      </TouchableOpacity>
                    );
                  })}
                </ScrollView>
              )}
            </View>

          </View>
        </View>
      </Modal>

      {/* ========================================================= */}
      {/* 2. CUSTOMER DETAILS / NEW CUSTOMER SCREEN (EXACT MATCH IMAGE 2) */}
      {/* ========================================================= */}
      <Modal visible={customerDetailsVisible} transparent animationType="slide" onRequestClose={() => setCustomerDetailsVisible(false)}>
        <View style={styles.custDetailsOverlay}>
          <View style={styles.custDetailsWindow}>
            
            {/* Logo & Header Info */}
            <View style={styles.custDetailsTopHeader}>
              <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                <View style={styles.goldDiamondLogo}>
                  <Text style={styles.goldDiamondLogoText}>g</Text>
                </View>
                <View style={{ marginLeft: 8 }}>
                  <Text style={styles.smartGoldBrandText}>smart</Text>
                  <Text style={styles.goldBrandText}>GOLD</Text>
                </View>
              </View>

              <View style={styles.custDetailsPointsSummary}>
                <View style={styles.pointSummaryRow}><Text style={styles.pointLabel}>Total Points</Text><Text style={styles.pointValRed}>0</Text></View>
                <View style={styles.pointSummaryRow}><Text style={styles.pointLabel}>Redeemed Points</Text><Text style={styles.pointValRed}>0</Text></View>
                <View style={styles.pointSummaryRow}><Text style={styles.pointLabel}>Balance Points</Text><Text style={styles.pointValRed}>0</Text></View>
              </View>
            </View>

            <ScrollView style={{ flex: 1, padding: 10 }}>
              
              {/* Customer Details Form Frame */}
              <View style={styles.custDetailsFrame}>
                <Text style={styles.custDetailsFrameTitle}>Customer Details</Text>
                
                <View style={styles.custDetailsTwoColumnGrid}>
                  
                  {/* Left Column Fields */}
                  <View style={styles.custDetailsCol}>
                    
                    {/* Customer Code */}
                    <View style={styles.custDetailsFormRow}>
                      <Text style={styles.custFieldLabel}>Customer Code</Text>
                      <TextInput
                        style={styles.custFieldInput}
                        value={newCustCode}
                        onChangeText={setNewCustCode}
                        placeholder="Enter Code"
                      />
                    </View>

                    {/* Customer Type */}
                    <View style={styles.custDetailsFormRow}>
                      <Text style={styles.custFieldLabel}>Customer Type</Text>
                      <View style={styles.custFieldSelect}>
                        <Text style={styles.custFieldSelectText}>{newCustType}</Text>
                        <Ionicons name="caret-down" size={14} color="#000" />
                      </View>
                    </View>

                    {/* Customer Name */}
                    <View style={styles.custDetailsFormRow}>
                      <Text style={styles.custFieldLabel}>Customer Name</Text>
                      <TextInput
                        style={styles.custFieldInput}
                        value={newCustName}
                        onChangeText={setNewCustName}
                      />
                    </View>

                    {/* Address */}
                    <View style={styles.custDetailsFormRow}>
                      <Text style={styles.custFieldLabel}>Address</Text>
                      <TextInput
                        style={styles.custFieldInput}
                        value={newCustAddress}
                        onChangeText={setNewCustAddress}
                      />
                    </View>

                    {/* Country */}
                    <View style={styles.custDetailsFormRow}>
                      <Text style={styles.custFieldLabel}>Country</Text>
                      <TextInput
                        style={styles.custFieldInput}
                        value={newCustCountry}
                        onChangeText={setNewCustCountry}
                      />
                    </View>

                    {/* Postal Code */}
                    <View style={styles.custDetailsFormRow}>
                      <Text style={styles.custFieldLabel}>Postal Code</Text>
                      <TextInput
                        style={styles.custFieldInput}
                        value={newCustPostalCode}
                        onChangeText={setNewCustPostalCode}
                      />
                    </View>

                    {/* Nationality */}
                    <View style={styles.custDetailsFormRow}>
                      <Text style={styles.custFieldLabel}>Nationality</Text>
                      <TextInput
                        style={styles.custFieldInput}
                        value={newCustNationality}
                        onChangeText={setNewCustNationality}
                      />
                    </View>

                    {/* Gender */}
                    <View style={styles.custDetailsFormRow}>
                      <Text style={styles.custFieldLabel}>Gender</Text>
                      <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                        <TouchableOpacity style={{ flexDirection: 'row', alignItems: 'center', marginRight: 15 }} onPress={() => setNewCustGender('Male')}>
                          <View style={[styles.legacyRadioCircle, newCustGender === 'Male' && styles.legacyRadioSelected]} />
                          <Text style={styles.legacyRadioLabel}>Male</Text>
                        </TouchableOpacity>
                        <TouchableOpacity style={{ flexDirection: 'row', alignItems: 'center' }} onPress={() => setNewCustGender('Female')}>
                          <View style={[styles.legacyRadioCircle, newCustGender === 'Female' && styles.legacyRadioSelected]} />
                          <Text style={styles.legacyRadioLabel}>Female</Text>
                        </TouchableOpacity>
                      </View>
                    </View>

                    {/* Nric Number */}
                    <View style={styles.custDetailsFormRow}>
                      <Text style={styles.custFieldLabel}>Nric Number</Text>
                      <TextInput
                        style={styles.custFieldInput}
                        value={newCustNric}
                        onChangeText={setNewCustNric}
                      />
                    </View>

                    {/* Remarks */}
                    <View style={styles.custDetailsFormRow}>
                      <Text style={styles.custFieldLabel}>Remarks</Text>
                      <TextInput
                        style={styles.custFieldInput}
                        value={newCustRemarks}
                        onChangeText={setNewCustRemarks}
                      />
                    </View>

                  </View>

                  {/* Right Column Fields */}
                  <View style={styles.custDetailsCol}>
                    
                    {/* Passport Number */}
                    <View style={styles.custDetailsFormRow}>
                      <Text style={styles.custFieldLabel}>Passport Number</Text>
                      <TextInput style={styles.custFieldInput} value={newCustPassport} onChangeText={setNewCustPassport} />
                    </View>

                    {/* Phone1 */}
                    <View style={styles.custDetailsFormRow}>
                      <Text style={styles.custFieldLabel}>Phone1</Text>
                      <TextInput style={styles.custFieldInput} value={newCustPhone1} onChangeText={setNewCustPhone1} />
                    </View>

                    {/* Phone2 */}
                    <View style={styles.custDetailsFormRow}>
                      <Text style={styles.custFieldLabel}>Phone2</Text>
                      <TextInput style={styles.custFieldInput} value={newCustPhone2} onChangeText={setNewCustPhone2} />
                    </View>

                    {/* Mobile */}
                    <View style={styles.custDetailsFormRow}>
                      <Text style={styles.custFieldLabel}>Mobile</Text>
                      <TextInput style={styles.custFieldInput} value={newCustMobile} onChangeText={setNewCustMobile} />
                    </View>

                    {/* Fax */}
                    <View style={styles.custDetailsFormRow}>
                      <Text style={styles.custFieldLabel}>Fax</Text>
                      <TextInput style={styles.custFieldInput} value={newCustFax} onChangeText={setNewCustFax} />
                    </View>

                    {/* Email */}
                    <View style={styles.custDetailsFormRow}>
                      <Text style={styles.custFieldLabel}>Email</Text>
                      <TextInput style={styles.custFieldInput} value={newCustEmail} onChangeText={setNewCustEmail} />
                    </View>

                    {/* Religion */}
                    <View style={styles.custDetailsFormRow}>
                      <Text style={styles.custFieldLabel}>Religion</Text>
                      <TextInput style={styles.custFieldInput} value={newCustReligion} onChangeText={setNewCustReligion} />
                    </View>

                    {/* Date Of Birth */}
                    <View style={styles.custDetailsFormRow}>
                      <Text style={styles.custFieldLabel}>Date Of Birth</Text>
                      <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                        <TextInput style={styles.dateMiniInput} value={newCustDobDay} onChangeText={setNewCustDobDay} placeholder="DD" maxLength={2} />
                        <Text style={{ marginHorizontal: 2 }}>/</Text>
                        <TextInput style={styles.dateMiniInput} value={newCustDobMonth} onChangeText={setNewCustDobMonth} placeholder="MM" maxLength={2} />
                        <Text style={{ marginHorizontal: 2 }}>/</Text>
                        <TextInput style={[styles.dateMiniInput, { width: 45 }]} value={newCustDobYear} onChangeText={setNewCustDobYear} placeholder="YYYY" maxLength={4} />
                      </View>
                    </View>

                    {/* Date Of Anniversary */}
                    <View style={styles.custDetailsFormRow}>
                      <Text style={styles.custFieldLabel}>Date Of Anniversary</Text>
                      <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                        <TextInput style={styles.dateMiniInput} value={newCustDoaDay} onChangeText={setNewCustDoaDay} placeholder="DD" maxLength={2} />
                        <Text style={{ marginHorizontal: 2 }}>/</Text>
                        <TextInput style={styles.dateMiniInput} value={newCustDoaMonth} onChangeText={setNewCustDoaMonth} placeholder="MM" maxLength={2} />
                        <Text style={{ marginHorizontal: 2 }}>/</Text>
                        <TextInput style={[styles.dateMiniInput, { width: 45 }]} value={newCustDoaYear} onChangeText={setNewCustDoaYear} placeholder="YYYY" maxLength={4} />
                        
                        <TouchableOpacity style={{ flexDirection: 'row', alignItems: 'center', marginLeft: 10 }} onPress={() => setNewCustActive(!newCustActive)}>
                          <Ionicons name={newCustActive ? "checkbox" : "square-outline"} size={16} color="#000" />
                          <Text style={{ fontSize: 12, fontWeight: 'bold', marginLeft: 4 }}>Active</Text>
                        </TouchableOpacity>
                      </View>
                    </View>

                    {/* Contact Radio Options */}
                    <View style={styles.custDetailsFormRow}>
                      <Text style={styles.custFieldLabel}>Contact</Text>
                      <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                        <TouchableOpacity style={{ flexDirection: 'row', alignItems: 'center', marginRight: 10 }} onPress={() => setNewCustContact('SMS')}>
                          <View style={[styles.legacyRadioCircle, newCustContact === 'SMS' && styles.legacyRadioSelected]} />
                          <Text style={styles.legacyRadioLabel}>SMS</Text>
                        </TouchableOpacity>
                        <TouchableOpacity style={{ flexDirection: 'row', alignItems: 'center', marginRight: 10 }} onPress={() => setNewCustContact('Phone')}>
                          <View style={[styles.legacyRadioCircle, newCustContact === 'Phone' && styles.legacyRadioSelected]} />
                          <Text style={styles.legacyRadioLabel}>Phone</Text>
                        </TouchableOpacity>
                        <TouchableOpacity style={{ flexDirection: 'row', alignItems: 'center' }} onPress={() => setNewCustContact('None')}>
                          <View style={[styles.legacyRadioCircle, newCustContact === 'None' && styles.legacyRadioSelected]} />
                          <Text style={styles.legacyRadioLabel}>None</Text>
                        </TouchableOpacity>
                      </View>
                    </View>

                  </View>

                </View>
              </View>

              {/* Bottom Customer Tab Header */}
              <View style={styles.custTabHeader}>
                <View style={[styles.custTabItem, styles.custTabItemActive]}><Text style={styles.custTabTextActive}>Sales Summary</Text></View>
                <View style={styles.custTabItem}><Text style={styles.custTabText}>Sales Detail</Text></View>
                <View style={styles.custTabItem}><Text style={styles.custTabText}>Customer Points</Text></View>
              </View>

              {/* Empty Sales Table Frame */}
              <View style={styles.custSalesTableFrame}>
                <View style={styles.custSalesTableHeader}>
                  <Text style={[styles.custSalesTh, { width: 120 }]}>InvoiceNo</Text>
                  <Text style={[styles.custSalesTh, { width: 100 }]}>InvoiceDate</Text>
                  <Text style={[styles.custSalesTh, { width: 80 }]}>Weight</Text>
                  <Text style={[styles.custSalesTh, { width: 80 }]}>PCS</Text>
                  <Text style={[styles.custSalesTh, { width: 100 }]}>Price</Text>
                </View>
                <View style={{ height: 80 }} />
              </View>

              {/* Action Buttons Bar matching Image 2 */}
              <View style={styles.custModalActionBar}>
                
                <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                  <TouchableOpacity style={{ flexDirection: 'row', alignItems: 'center', marginRight: 15 }} onPress={() => setAddKyc(!addKyc)}>
                    <Ionicons name={addKyc ? "checkbox" : "square-outline"} size={16} color="#000" />
                    <Text style={{ fontSize: 12, marginLeft: 4 }}>Add KYC</Text>
                  </TouchableOpacity>

                  <TouchableOpacity style={{ flexDirection: 'row', alignItems: 'center' }} onPress={() => setShowKyc(!showKyc)}>
                    <Ionicons name={showKyc ? "checkbox" : "square-outline"} size={16} color="#000" />
                    <Text style={{ fontSize: 12, marginLeft: 4 }}>Show KYC</Text>
                  </TouchableOpacity>
                </View>

                <View style={{ flexDirection: 'row', alignItems: 'center', gap: 6 }}>
                  <TouchableOpacity style={styles.orangeActionBtn} onPress={handleOpenNewCustomer}>
                    <Text style={styles.orangeActionBtnText}>New</Text>
                  </TouchableOpacity>
                  <TouchableOpacity style={styles.orangeActionBtn} onPress={handleSaveNewCustomer} disabled={savingCustomer}>
                    <Text style={styles.orangeActionBtnText}>{savingCustomer ? 'Saving...' : 'Save'}</Text>
                  </TouchableOpacity>
                  <TouchableOpacity style={styles.orangeActionBtn} onPress={() => setCustomerDetailsVisible(false)}>
                    <Text style={styles.orangeActionBtnText}>Delete</Text>
                  </TouchableOpacity>
                  <TouchableOpacity style={styles.orangeActionBtn} onPress={() => setCustomerDetailsVisible(false)}>
                    <Text style={styles.orangeActionBtnText}>Close</Text>
                  </TouchableOpacity>
                </View>

              </View>

            </ScrollView>
          </View>
        </View>
      </Modal>

      {/* ========================================================= */}
      {/* 3. SCHEME CODE SEARCH HELP SEARCH MODAL (EXACT MATCH IMAGE 3) */}
      {/* ========================================================= */}
      <Modal visible={schemeModalVisible} transparent animationType="fade" onRequestClose={() => setSchemeModalVisible(false)}>
        <View style={styles.legacyModalOverlay}>
          <View style={styles.legacyModalContainer}>
            
            {/* Modal Title & Close */}
            <View style={styles.legacyModalHeader}>
              <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                <MaterialCommunityIcons name="layers-search-outline" size={18} color="#000" style={{ marginRight: 6 }} />
                <Text style={styles.legacyModalTitle}>Help Search</Text>
              </View>
              <TouchableOpacity onPress={() => setSchemeModalVisible(false)} style={styles.legacyModalCloseBtn}>
                <Ionicons name="close" size={16} color="#333" />
              </TouchableOpacity>
            </View>

            {/* SubHeader Search Controls */}
            <View style={styles.legacySearchControlArea}>
              
              <View style={styles.legacyTopMetaRow}>
                <Text style={styles.legacySearchByText}>Search By...</Text>
                <Text style={styles.legacyRecordCountText}>
                  <Text style={{ color: 'red', fontWeight: 'bold' }}>{schemeTotalRecords}</Text> Records Found
                </Text>
              </View>

              {/* Search Field Dropdown & Radio Options */}
              <View style={[styles.legacyOptionsRow, { zIndex: 10 }]}>
                <View style={{ flexDirection: 'row', alignItems: 'center', zIndex: 10 }}>
                  <Text style={styles.legacyLabelText}>Search Field </Text>
                  <TouchableOpacity 
                    style={styles.legacyDropdownSelect} 
                    onPress={() => setShowSchemeFieldDropdown(!showSchemeFieldDropdown)}
                  >
                    <Text style={styles.legacyDropdownText}>{schemeSearchField}</Text>
                    <Ionicons name="caret-down" size={12} color="#000" style={{ marginLeft: 4 }} />
                  </TouchableOpacity>

                  {/* Dropdown Options Menu */}
                  {showSchemeFieldDropdown && (
                    <View style={styles.dropdownMenuOverlay}>
                      {(['Description', 'Code', 'Remarks', 'CreateUser'] as const).map((field) => (
                        <TouchableOpacity
                          key={field}
                          style={[styles.dropdownMenuItem, schemeSearchField === field && styles.dropdownMenuItemActive]}
                          onPress={() => {
                            setSchemeSearchField(field);
                            setShowSchemeFieldDropdown(false);
                            fetchSchemeSearchResults(field, schemeSearchText, schemeSearchMode);
                          }}
                        >
                          <Text style={[styles.dropdownMenuItemText, schemeSearchField === field && styles.dropdownMenuItemTextActive]}>
                            {field}
                          </Text>
                        </TouchableOpacity>
                      ))}
                    </View>
                  )}
                </View>

                {/* Radio Modes: Start, End, Any */}
                <View style={{ flexDirection: 'row', alignItems: 'center', marginLeft: 16 }}>
                  <TouchableOpacity style={styles.legacyRadioOption} onPress={() => { setSchemeSearchMode('Start'); fetchSchemeSearchResults(schemeSearchField, schemeSearchText, 'Start'); }}>
                    <View style={[styles.legacyRadioCircle, schemeSearchMode === 'Start' && styles.legacyRadioSelected]} />
                    <Text style={styles.legacyRadioLabel}>Start</Text>
                  </TouchableOpacity>

                  <TouchableOpacity style={styles.legacyRadioOption} onPress={() => { setSchemeSearchMode('End'); fetchSchemeSearchResults(schemeSearchField, schemeSearchText, 'End'); }}>
                    <View style={[styles.legacyRadioCircle, schemeSearchMode === 'End' && styles.legacyRadioSelected]} />
                    <Text style={styles.legacyRadioLabel}>End</Text>
                  </TouchableOpacity>

                  <TouchableOpacity style={styles.legacyRadioOption} onPress={() => { setSchemeSearchMode('Any'); fetchSchemeSearchResults(schemeSearchField, schemeSearchText, 'Any'); }}>
                    <View style={[styles.legacyRadioCircle, schemeSearchMode === 'Any' && styles.legacyRadioSelected]} />
                    <Text style={styles.legacyRadioLabel}>Any</Text>
                  </TouchableOpacity>
                </View>
              </View>

              {/* Search Field Title & Search Text Box */}
              <View style={styles.legacyInputSection}>
                <Text style={styles.legacySearchFieldColumnTitle}>{schemeSearchField}</Text>
                <View style={styles.legacySearchBoxRow}>
                  <TextInput
                    style={styles.legacySearchTextInput}
                    placeholder="Enter search text..."
                    value={schemeSearchText}
                    onChangeText={(text) => {
                      setSchemeSearchText(text);
                      fetchSchemeSearchResults(schemeSearchField, text, schemeSearchMode);
                    }}
                  />
                  <TouchableOpacity style={styles.legacySearchExecuteBtn} onPress={() => fetchSchemeSearchResults(schemeSearchField, schemeSearchText, schemeSearchMode)}>
                    <Text style={styles.legacySearchExecuteBtnText}>Search</Text>
                  </TouchableOpacity>
                </View>
              </View>
            </View>

            {/* Scheme Master Grid / Table matching Image 3 */}
            <View style={styles.legacyGridWrapper}>
              <View style={styles.legacyGridHeaderRow}>
                <Text style={[styles.legacyGridTh, { width: 120 }]}>Code</Text>
                <Text style={[styles.legacyGridTh, { width: 180 }]}>Description</Text>
                <Text style={[styles.legacyGridTh, { width: 100 }]}>Value</Text>
                <Text style={[styles.legacyGridTh, { flex: 1, minWidth: 100 }]}>CreateUser</Text>
              </View>

              {loadingSchemes ? (
                <View style={{ padding: 20, alignItems: 'center' }}>
                  <ActivityIndicator size="small" color="#0000FF" />
                </View>
              ) : (
                <ScrollView style={{ maxHeight: 320 }}>
                  {schemeList.map((row: any, index: number) => {
                    const isSelected = selectedSchemeIndex === index;
                    return (
                      <TouchableOpacity
                        key={index}
                        style={[styles.legacyGridDataRow, isSelected && styles.legacyRowHighlight]}
                        onPress={() => {
                          setSelectedSchemeIndex(index);
                          handleSelectSchemeRow(row);
                        }}
                      >
                        <Text style={[styles.legacyGridTd, { width: 120, fontWeight: 'bold' }]}>{row.Code}</Text>
                        <Text style={[styles.legacyGridTd, { width: 180 }]}>{row.Description}</Text>
                        <Text style={[styles.legacyGridTd, { width: 100 }]}>{row.Value || ''}</Text>
                        <Text style={[styles.legacyGridTd, { flex: 1, minWidth: 100 }]}>{row.CreateUser || ''}</Text>
                      </TouchableOpacity>
                    );
                  })}
                </ScrollView>
              )}
            </View>
          </View>
        </View>
      </Modal>


      {/* BOTTOM ACTION BAR */}
      <View style={styles.bottomActionBar}>
        <TouchableOpacity style={styles.secondaryBtn} onPress={handleReprint}>
          <Text style={styles.secondaryBtnText}>RePrint</Text>
        </TouchableOpacity>

        <TouchableOpacity style={styles.secondaryBtn} onPress={handleCancel}>
          <Text style={styles.secondaryBtnText}>Cancel</Text>
        </TouchableOpacity>

        <TouchableOpacity style={styles.secondaryBtn} onPress={handleClear}>
          <Text style={styles.secondaryBtnText}>Clear</Text>
        </TouchableOpacity>

        <TouchableOpacity style={styles.primaryBtn} onPress={handleSave} disabled={saving}>
          {saving ? (
            <ActivityIndicator size="small" color="#FFFFFF" />
          ) : (
            <Text style={styles.primaryBtnText}>Save</Text>
          )}
        </TouchableOpacity>

        <TouchableOpacity style={styles.secondaryBtn} onPress={() => router.back()}>
          <Text style={styles.secondaryBtnText}>Close</Text>
        </TouchableOpacity>
      </View>
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  safeArea: {
    flex: 1,
    backgroundColor: '#FBF9F5',
  },
  topHeader: {
    height: 52,
    borderBottomWidth: 1,
    borderBottomColor: '#E8DCB8',
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingHorizontal: 16,
    backgroundColor: '#FFFFFF',
  },
  headerLeft: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  backButton: {
    width: 32,
    height: 32,
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#DFD5C3',
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: '#FFFFFF',
    marginRight: 12,
  },
  headerTitle: {
    fontSize: 18,
    fontWeight: '700',
    color: '#3B2D17',
    fontFamily: Platform.OS === 'web' ? 'Cambria, Georgia, serif' : undefined,
    letterSpacing: -0.2,
  },
  headerRightBadge: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 10,
    paddingVertical: 4,
    borderRadius: 6,
    borderWidth: 1,
    borderColor: '#E2D3B3',
    backgroundColor: '#FFF8EA',
  },
  badgeText: {
    fontSize: 12,
    fontWeight: '600',
    color: '#9E6D08',
    marginLeft: 6,
  },

  scrollContainer: {
    padding: 12,
    paddingBottom: 16,
  },
  mainGrid: {
    gap: 12,
  },
  cardSection: {
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    overflow: 'hidden',
    shadowColor: '#3B2D17',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.04,
    shadowRadius: 3,
    elevation: 1,
  },
  sectionHeader: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 12,
    paddingVertical: 8,
    backgroundColor: '#F5E7CA',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
  },
  headerIconBadge: {
    width: 24,
    height: 24,
    borderRadius: 5,
    backgroundColor: '#FFF8EA',
    borderWidth: 1,
    borderColor: '#E2D2AF',
    justifyContent: 'center',
    alignItems: 'center',
    marginRight: 8,
  },
  sectionTitle: {
    fontSize: 13,
    fontWeight: '700',
    color: '#3B2D17',
    fontFamily: Platform.OS === 'web' ? 'Cambria, Georgia, serif' : undefined,
  },
  cardBody: {
    padding: 14,
    gap: 8,
  },

  formRow: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  formRowTopAlign: {
    flexDirection: 'row',
    alignItems: 'flex-start',
  },
  flexRow: {
    flex: 1,
    flexDirection: 'row',
    alignItems: 'center',
  },
  label: {
    fontSize: 12,
    fontWeight: '600',
    color: '#3B2D17',
    width: 120,
  },
  inlineLabel: {
    fontSize: 12,
    fontWeight: '600',
    color: '#3B2D17',
  },
  requiredStar: {
    color: '#DC2626',
  },

  inputWrapperWithButton: {
    flex: 1,
    flexDirection: 'row',
    alignItems: 'center',
  },
  inputFlex: {
    flex: 1,
    height: 32,
    borderWidth: 1,
    borderColor: '#DFD5C3',
    borderTopLeftRadius: 6,
    borderBottomLeftRadius: 6,
    paddingHorizontal: 10,
    fontSize: 12,
    color: '#3B2D17',
    backgroundColor: '#FFFFFF',
  },
  inlineSearchBtn: {
    width: 36,
    height: 32,
    backgroundColor: '#9E6D08',
    borderTopRightRadius: 6,
    borderBottomRightRadius: 6,
    justifyContent: 'center',
    alignItems: 'center',
  },

  readOnlyBox: {
    flex: 1,
    height: 32,
    borderWidth: 1,
    borderColor: '#EADBCA',
    borderRadius: 6,
    backgroundColor: '#FAF7F0',
    justifyContent: 'center',
    paddingHorizontal: 10,
  },
  addressTextArea: {
    flex: 1,
    height: 52,
    borderWidth: 1,
    borderColor: '#DFD5C3',
    borderRadius: 6,
    backgroundColor: '#FFFFFF',
    paddingHorizontal: 10,
    paddingVertical: 6,
    fontSize: 12,
    color: '#3B2D17',
    textAlignVertical: 'top',
  },
  readOnlyText: {
    fontSize: 12,
    color: '#6B5738',
  },
  readOnlyPlaceholderText: {
    fontSize: 10,
    color: '#A69680',
    position: 'absolute',
    left: 10,
    top: 2,
  },

  dateTripleRow: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 4,
  },
  dateTripleBox: {
    width: 46,
    height: 32,
    borderWidth: 1,
    borderColor: '#DFD5C3',
    borderRadius: 6,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingHorizontal: 6,
    backgroundColor: '#FFFFFF',
  },
  dateTripleText: {
    fontSize: 12,
    color: '#3B2D17',
    fontWeight: '500',
  },
  dateSeparator: {
    fontSize: 13,
    color: '#8F7E66',
    fontWeight: '600',
  },
  calendarIconBox: {
    width: 32,
    height: 32,
    borderWidth: 1,
    borderColor: '#DFD5C3',
    borderRadius: 6,
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: '#FAF7F0',
    marginLeft: 4,
  },

  newCustomerBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#9E6D08',
    paddingHorizontal: 14,
    paddingVertical: 6,
    borderRadius: 6,
  },
  newCustomerBtnText: {
    color: '#FFFFFF',
    fontSize: 12,
    fontWeight: '600',
  },

  schemeRowLayout: {
    flexDirection: 'row',
    alignItems: 'center',
    flexWrap: 'wrap',
  },
  schemeLabel: {
    fontSize: 12,
    fontWeight: '600',
    color: '#3B2D17',
  },
  schemeInputGroup: {
    width: 170,
    flexDirection: 'row',
    alignItems: 'center',
    marginLeft: 8,
  },
  schemeValueInput: {
    width: 130,
    height: 32,
    borderWidth: 1,
    borderColor: '#DFD5C3',
    borderRadius: 6,
    paddingHorizontal: 10,
    fontSize: 12,
    color: '#3B2D17',
    backgroundColor: '#FFFFFF',
    marginLeft: 8,
  },

  radioContainerBox: {
    flexDirection: 'row',
    alignItems: 'center',
    borderWidth: 1,
    borderColor: '#E8DCB8',
    borderRadius: 6,
    paddingHorizontal: 12,
    height: 32,
    gap: 16,
    backgroundColor: '#FFFFFF',
  },
  radioButton: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  radioOuter: {
    width: 15,
    height: 15,
    borderRadius: 7.5,
    borderWidth: 1.5,
    borderColor: '#C4B59D',
    justifyContent: 'center',
    alignItems: 'center',
    marginRight: 6,
  },
  radioOuterSelected: {
    borderColor: '#9E6D08',
  },
  radioInner: {
    width: 7,
    height: 7,
    borderRadius: 3.5,
    backgroundColor: '#9E6D08',
  },
  radioText: {
    fontSize: 12,
    fontWeight: '600',
    color: '#3B2D17',
  },

  refreshBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    borderWidth: 1,
    borderColor: '#DFD5C3',
    backgroundColor: '#FAF7F0',
    paddingHorizontal: 10,
    paddingVertical: 4,
    borderRadius: 6,
  },
  refreshBtnText: {
    fontSize: 11,
    fontWeight: '600',
    color: '#9E6D08',
  },

  tableHeaderRow: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#F5E7CA',
    paddingHorizontal: 12,
    paddingVertical: 8,
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
  },
  thText: {
    fontSize: 11,
    fontWeight: '700',
    color: '#3B2D17',
  },
  tableDataRow: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 12,
    paddingVertical: 8,
    borderBottomWidth: 1,
    borderBottomColor: '#F0E6D2',
    backgroundColor: '#FFFFFF',
  },
  tdText: {
    fontSize: 12,
    color: '#332717',
  },
  tableEmptyState: {
    alignItems: 'center',
    justifyContent: 'center',
    paddingVertical: 20,
  },
  tableEmptyText: {
    fontSize: 12,
    color: '#8F7E66',
    marginTop: 6,
  },

  bottomActionBar: {
    height: 48,
    backgroundColor: '#FFFFFF',
    borderTopWidth: 1,
    borderTopColor: '#E8DCB8',
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'flex-end',
    paddingHorizontal: 16,
    gap: 8,
  },
  secondaryBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 16,
    paddingVertical: 7,
    borderRadius: 6,
    borderWidth: 1,
    borderColor: '#DFD5C3',
    backgroundColor: '#FFFFFF',
  },
  secondaryBtnText: {
    fontSize: 12,
    fontWeight: '600',
    color: '#3B2D17',
  },
  primaryBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 20,
    paddingVertical: 7,
    borderRadius: 6,
    backgroundColor: '#9E6D08',
  },
  primaryBtnText: {
    fontSize: 12,
    fontWeight: '600',
    color: '#FFFFFF',
  },

  // ==========================================
  // MODERN MODAL & POPUP STYLES
  // ==========================================
  legacyModalOverlay: {
    flex: 1,
    backgroundColor: 'rgba(59, 45, 23, 0.45)',
    justifyContent: 'center',
    alignItems: 'center',
  },
  legacyModalContainer: {
    width: Platform.OS === 'web' ? 660 : '95%',
    backgroundColor: '#FFFFFF',
    borderRadius: 12,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    overflow: 'hidden',
    elevation: 8,
    shadowColor: '#3B2D17',
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.15,
    shadowRadius: 10,
  },
  legacyModalHeader: {
    height: 44,
    backgroundColor: '#F5E7CA',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingHorizontal: 14,
  },
  legacyModalTitle: {
    fontSize: 14,
    color: '#3B2D17',
    fontWeight: '700',
    fontFamily: Platform.OS === 'web' ? 'Cambria, Georgia, serif' : undefined,
  },
  legacyModalCloseBtn: {
    width: 26,
    height: 26,
    borderRadius: 13,
    backgroundColor: 'rgba(59, 45, 23, 0.08)',
    justifyContent: 'center',
    alignItems: 'center',
  },
  legacySearchControlArea: {
    padding: 12,
    backgroundColor: '#FFFFFF',
    gap: 8,
  },
  legacyTopMetaRow: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
  },
  legacySearchByText: {
    fontSize: 12,
    fontWeight: '700',
    color: '#3B2D17',
  },
  legacyRecordCountText: {
    fontSize: 12,
    fontWeight: '700',
    color: '#9E6D08',
  },
  legacyOptionsRow: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
  },
  legacyLabelText: {
    fontSize: 12,
    color: '#6B5738',
    fontWeight: '600',
  },
  legacyDropdownSelect: {
    flexDirection: 'row',
    alignItems: 'center',
    borderWidth: 1,
    borderColor: '#DFD5C3',
    borderRadius: 6,
    backgroundColor: '#FAF7F0',
    paddingHorizontal: 10,
    height: 30,
    minWidth: 140,
    justifyContent: 'space-between',
  },
  legacyDropdownText: {
    fontSize: 12,
    color: '#3B2D17',
    fontWeight: '600',
  },
  dropdownMenuOverlay: {
    position: 'absolute',
    top: 34,
    left: 80,
    minWidth: 150,
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#DFD5C3',
    borderRadius: 6,
    elevation: 10,
    shadowColor: '#3B2D17',
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.15,
    shadowRadius: 8,
    zIndex: 999,
  },
  dropdownMenuItem: {
    paddingHorizontal: 12,
    paddingVertical: 8,
    borderBottomWidth: 1,
    borderBottomColor: '#F0E6D2',
  },
  dropdownMenuItemActive: {
    backgroundColor: '#FAF7F0',
  },
  dropdownMenuItemText: {
    fontSize: 12,
    color: '#3B2D17',
  },
  dropdownMenuItemTextActive: {
    fontWeight: '700',
    color: '#9E6D08',
  },
  legacyRadioOption: {
    flexDirection: 'row',
    alignItems: 'center',
    marginRight: 12,
  },
  legacyRadioCircle: {
    width: 14,
    height: 14,
    borderRadius: 7,
    borderWidth: 1.5,
    borderColor: '#C4B59D',
    marginRight: 5,
    backgroundColor: '#FFF',
  },
  legacyRadioSelected: {
    backgroundColor: '#9E6D08',
    borderColor: '#9E6D08',
  },
  legacyRadioLabel: {
    fontSize: 12,
    color: '#3B2D17',
    fontWeight: '600',
  },
  legacyInputSection: {
    marginTop: 2,
  },
  legacySearchFieldColumnTitle: {
    fontSize: 12,
    fontWeight: '700',
    color: '#9E6D08',
    marginBottom: 4,
  },
  legacySearchBoxRow: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  legacySearchTextInput: {
    flex: 1,
    height: 32,
    borderWidth: 1,
    borderColor: '#DFD5C3',
    borderRadius: 6,
    paddingHorizontal: 10,
    fontSize: 12,
    color: '#3B2D17',
    backgroundColor: '#FFFFFF',
  },
  legacySearchExecuteBtn: {
    height: 32,
    paddingHorizontal: 16,
    backgroundColor: '#9E6D08',
    borderRadius: 6,
    justifyContent: 'center',
    alignItems: 'center',
    marginLeft: 6,
  },
  legacySearchExecuteBtnText: {
    fontSize: 12,
    color: '#FFFFFF',
    fontWeight: '600',
  },
  legacyGridWrapper: {
    borderTopWidth: 1,
    borderTopColor: '#E8DCB8',
    backgroundColor: '#FFFFFF',
  },
  legacyGridHeaderRow: {
    flexDirection: 'row',
    backgroundColor: '#F5E7CA',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    paddingVertical: 8,
    paddingHorizontal: 12,
  },
  legacyGridTh: {
    fontSize: 11,
    fontWeight: '700',
    color: '#3B2D17',
  },
  legacyGridDataRow: {
    flexDirection: 'row',
    paddingVertical: 8,
    paddingHorizontal: 12,
    borderBottomWidth: 1,
    borderBottomColor: '#F0E6D2',
  },
  legacyRowHighlight: {
    backgroundColor: '#FAF7F0',
  },
  legacyGridTd: {
    fontSize: 12,
    color: '#332717',
  },

  // ==========================================
  // CUSTOMER DETAILS SCREEN STYLES (IMAGE 2)
  // ==========================================
  custDetailsOverlay: {
    flex: 1,
    backgroundColor: 'rgba(59, 45, 23, 0.45)',
    justifyContent: 'center',
    alignItems: 'center',
    padding: 12,
  },
  custDetailsWindow: {
    width: Platform.OS === 'web' ? 840 : '98%',
    maxHeight: '94%',
    backgroundColor: '#FFFFFF',
    borderRadius: 12,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    overflow: 'hidden',
    elevation: 10,
    shadowColor: '#3B2D17',
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.15,
    shadowRadius: 10,
  },
  custDetailsTopHeader: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    paddingHorizontal: 16,
    paddingVertical: 10,
    backgroundColor: '#F5E7CA',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
  },
  goldDiamondLogo: {
    width: 28,
    height: 28,
    backgroundColor: '#9E6D08',
    borderRadius: 6,
    justifyContent: 'center',
    alignItems: 'center',
    marginRight: 8,
  },
  goldDiamondLogoText: {
    color: '#FFFFFF',
    fontWeight: 'bold',
    fontSize: 16,
  },
  smartGoldBrandText: {
    fontSize: 16,
    fontWeight: '800',
    color: '#3B2D17',
    lineHeight: 18,
  },
  goldBrandText: {
    fontSize: 16,
    fontWeight: '800',
    color: '#9E6D08',
    lineHeight: 18,
  },
  custDetailsPointsSummary: {
    gap: 3,
  },
  pointSummaryRow: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    width: 170,
  },
  pointLabel: {
    fontSize: 11,
    fontWeight: '600',
    color: '#6B5738',
  },
  pointValRed: {
    fontSize: 11,
    fontWeight: '700',
    color: '#DC2626',
  },
  custDetailsFrame: {
    borderWidth: 1,
    borderColor: '#E8DCB8',
    padding: 12,
    marginTop: 6,
    borderRadius: 8,
    backgroundColor: '#FAF7F0',
  },
  custDetailsFrameTitle: {
    position: 'absolute',
    top: -9,
    left: 12,
    backgroundColor: '#FFFFFF',
    paddingHorizontal: 6,
    fontSize: 11,
    fontWeight: '700',
    color: '#9E6D08',
  },
  custDetailsTwoColumnGrid: {
    flexDirection: Platform.OS === 'web' ? 'row' : 'column',
    gap: 16,
    marginTop: 4,
  },
  custDetailsCol: {
    flex: 1,
    gap: 6,
  },
  custDetailsFormRow: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  custFieldLabel: {
    width: 110,
    fontSize: 11,
    fontWeight: '600',
    color: '#3B2D17',
  },
  custFieldInput: {
    flex: 1,
    height: 28,
    borderWidth: 1,
    borderColor: '#DFD5C3',
    borderRadius: 6,
    paddingHorizontal: 8,
    fontSize: 11,
    color: '#3B2D17',
    backgroundColor: '#FFFFFF',
  },
  custFieldSelect: {
    flex: 1,
    height: 28,
    borderWidth: 1,
    borderColor: '#DFD5C3',
    borderRadius: 6,
    paddingHorizontal: 8,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    backgroundColor: '#FFFFFF',
  },
  custFieldSelectText: {
    fontSize: 11,
    color: '#3B2D17',
  },
  dateMiniInput: {
    width: 32,
    height: 28,
    borderWidth: 1,
    borderColor: '#DFD5C3',
    borderRadius: 6,
    textAlign: 'center',
    fontSize: 11,
    color: '#3B2D17',
    backgroundColor: '#FFFFFF',
  },
  custTabHeader: {
    flexDirection: 'row',
    marginTop: 10,
    borderBottomWidth: 1,
    borderBottomColor: '#E8DCB8',
  },
  custTabItem: {
    paddingHorizontal: 14,
    paddingVertical: 6,
    borderRadius: 6,
    marginRight: 4,
    backgroundColor: '#FAF7F0',
  },
  custTabItemActive: {
    backgroundColor: '#F5E7CA',
    borderWidth: 1,
    borderColor: '#E2D3B3',
  },
  custTabText: {
    fontSize: 11,
    color: '#8F7E66',
    fontWeight: '600',
  },
  custTabTextActive: {
    fontSize: 11,
    fontWeight: '700',
    color: '#9E6D08',
  },
  custSalesTableFrame: {
    borderWidth: 1,
    borderColor: '#E8DCB8',
    borderRadius: 6,
    overflow: 'hidden',
    backgroundColor: '#FFFFFF',
    marginTop: 4,
  },
  custSalesTableHeader: {
    flexDirection: 'row',
    backgroundColor: '#F5E7CA',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    paddingVertical: 6,
    paddingHorizontal: 8,
  },
  custSalesTh: {
    fontSize: 11,
    fontWeight: '700',
    color: '#3B2D17',
  },
  custModalActionBar: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    marginTop: 10,
    paddingTop: 8,
    borderTopWidth: 1,
    borderTopColor: '#E8DCB8',
  },
  orangeActionBtn: {
    backgroundColor: '#9E6D08',
    paddingHorizontal: 14,
    paddingVertical: 6,
    borderRadius: 6,
  },
  orangeActionBtnText: {
    color: '#FFFFFF',
    fontSize: 11,
    fontWeight: '600',
  },
});
