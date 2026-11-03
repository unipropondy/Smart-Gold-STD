import { Ionicons, MaterialCommunityIcons } from '@expo/vector-icons';
import { Stack, useRouter, useLocalSearchParams } from 'expo-router';
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
  const params = useLocalSearchParams();

  // Sync state from URL parameters when returning from Customer screen or Paying Mode Tender screen
  useEffect(() => {
    // If returning from Customer Details screen with selected or newly created customer
    if (params.selectedCustomerCode) {
      const code = String(params.selectedCustomerCode).trim();
      setCustomerCode(code);
      if (params.selectedCustomerName) setCustomerName(String(params.selectedCustomerName));
      if (params.selectedAddress) setAddress1(String(params.selectedAddress));
      fetchCustomerDetailsByCode(code);
    }

    // Preserve existing Scheme form state if passed back in params
    if (params.prevSchemeCode) setSchemeCode(String(params.prevSchemeCode));
    if (params.prevSchemeValue) setSchemeValue(String(params.prevSchemeValue));
    if (params.prevMode) setMode(String(params.prevMode) as 'VALUE' | 'GRAM');
    if (params.prevSalesmanCode) setSalesmanCode(String(params.prevSalesmanCode));
    if (params.prevSalesmanName) setSalesmanName(String(params.prevSalesmanName));
    if (params.prevSalesmanPassword) setSalesmanPassword(String(params.prevSalesmanPassword));

    // If returning from Paying Mode Tender screen after successful payment
    if (params.paymentSuccess === 'true') {
      const savedInv = String(params.savedInvoiceNo || '');
      const custC = String(params.savedCustomerCode || customerCode || '');
      if (custC) {
        fetchTransactionHistory(custC);
      }
      fetchNextSchemeNo();
      // Reset scheme fields after successful transaction
      setSchemeCode('');
      setSchemeValue('');
      const msg = savedInv 
        ? `Scheme Transaction ${savedInv} saved successfully with payments!`
        : 'Scheme Transaction and payments saved successfully!';
      Platform.OS === 'web' ? window.alert(msg) : Alert.alert('Success', msg);
    }
  }, [params.selectedCustomerCode, params.paymentSuccess, params.savedInvoiceNo]);

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
  const [dailyRate, setDailyRate] = useState<number>(0);
  const [fetchingDailyRate, setFetchingDailyRate] = useState<boolean>(false);
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
  const [payingModalVisible, setPayingModalVisible] = useState(false);
  const [payingModalTotal, setPayingModalTotal] = useState(0);

  // Customer Help Search Modal States (Image 1)
  const [custSearchField, setCustSearchField] = useState<'CustomerName' | 'CustomerCode' | 'NricNo'>('CustomerName');
  const [showCustFieldDropdown, setShowCustFieldDropdown] = useState(false);
  const [custSearchText, setCustSearchText] = useState('');
  const [custSearchMode, setCustSearchMode] = useState<'Start' | 'End' | 'Any'>('Any');
  const [customerSearchResults, setCustomerSearchResults] = useState<any[]>([]);
  const [custTotalRecords, setCustTotalRecords] = useState(0);
  const [loadingCustomerSearch, setLoadingCustomerSearch] = useState(false);
  const [selectedCustIndex, setSelectedCustIndex] = useState<number | null>(null);

  // Scheme Help Search Modal States (Image 3)
  const [schemeSearchField, setSchemeSearchField] = useState<'Code' | 'Description' | 'Remarks' | 'CreateUser'>('Code');
  const [showSchemeFieldDropdown, setShowSchemeFieldDropdown] = useState(false);
  const [schemeSearchText, setSchemeSearchText] = useState('');
  const [schemeSearchMode, setSchemeSearchMode] = useState<'Start' | 'End' | 'Any'>('Any');
  const [schemeTotalRecords, setSchemeTotalRecords] = useState(0);
  const [selectedSchemeIndex, setSelectedSchemeIndex] = useState<number | null>(null);

  // New / Edit Customer Details Screen Form States (Image 2)
  const [newCustCode, setNewCustCode] = useState('');
  const [newCustType, setNewCustType] = useState('DIAMOND');
  const [showCustTypeDropdown, setShowCustTypeDropdown] = useState(false);
  const [customerTypes, setCustomerTypes] = useState<string[]>(['DIAMOND', 'EMERALD', 'RUBY']);
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
  const [savingCustomer, setSavingCustomer] = useState(false);
  const [deletingCustomer, setDeletingCustomer] = useState(false);

  // Customer caller tracker ('main' screen vs 'details' modal)
  const [custSearchCaller, setCustSearchCaller] = useState<'main' | 'details'>('main');

  // Customer Tabs States (Image 2: Sales Summary, Sales Detail, Customer Points)
  const [custActiveTab, setCustActiveTab] = useState<'summary' | 'detail' | 'points'>('summary');
  const [salesSummaryData, setSalesSummaryData] = useState<any[]>([]);
  const [salesDetailData, setSalesDetailData] = useState<any[]>([]);
  const [custPointsData, setCustPointsData] = useState<any[]>([]);
  const [loadingCustTabs, setLoadingCustTabs] = useState(false);
  const [custPointsSummary, setCustPointsSummary] = useState({ totalReward: 0, totalRedeemed: 0, balance: 0 });

  // KYC States & Modals
  const [addKycModalVisible, setAddKycModalVisible] = useState(false);
  const [showKycModalVisible, setShowKycModalVisible] = useState(false);
  const [kycData, setKycData] = useState<any>({
    CustomerCode: '',
    CustomerName: '',
    NricNo: '',
    PassportNo: '',
    Nationality: '',
    Address: '',
    DOB: '',
    CustomerImageName: '',
    KycStatus: 'Pending',
    Remarks: ''
  });
  const [savingKyc, setSavingKyc] = useState(false);

  // Fetch Current Daily Rate from SQL Server backend
  const fetchDailyRate = async () => {
    setFetchingDailyRate(true);
    try {
      const res = await fetch(`${API_BASE_URL}/scheme/daily-rate`);
      const json = await res.json();
      if (json.success && json.dailyRate) {
        setDailyRate(Number(json.dailyRate));
      }
    } catch (err) {
      console.error('Error fetching daily rate:', err);
    } finally {
      setFetchingDailyRate(false);
    }
  };

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

  // Initial Load: Fetch Next Scheme Number, Daily Rate, and Customer Types
  useEffect(() => {
    fetchNextSchemeNo();
    fetchDailyRate();
    fetchCustomerTypes();
  }, []);

  // Open Customer Help Search Modal (Image 1)
  const handleOpenCustomerModal = async () => {
    setCustSearchCaller('main');
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
    const code = (custRow.CustomerCode || '').trim();
    setCustomerModalVisible(false);
    if (custSearchCaller === 'details') {
      loadCustomerIntoDetailsForm(code);
    } else {
      setCustomerCode(code);
      fetchCustomerDetailsByCode(code);
    }
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
    fetchSchemeSearchResults(schemeSearchField, schemeSearchText, schemeSearchMode);
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

  // Fetch actual Customer Types from SQL Server
  const fetchCustomerTypes = async () => {
    try {
      const res = await fetch(`${API_BASE_URL}/customer/types`);
      const json = await res.json();
      if (json.success && Array.isArray(json.data) && json.data.length > 0) {
        setCustomerTypes(json.data);
      }
    } catch (err) {
      console.warn('Error fetching customer types:', err);
    }
  };

  // Fetch next Customer Code from SQL Server
  const fetchNextCustomerCode = async () => {
    try {
      const res = await fetch(`${API_BASE_URL}/customer/next-code`);
      const json = await res.json();
      if (json.success && json.nextCode) {
        return json.nextCode;
      }
    } catch (err) {
      console.warn('Error fetching next customer code:', err);
    }
    return '';
  };

  // Fetch Sales Summary, Sales Detail, and Points for Customer Details tabs
  const fetchCustTabsData = async (code: string) => {
    if (!code) {
      setSalesSummaryData([]);
      setSalesDetailData([]);
      setCustPointsData([]);
      return;
    }
    setLoadingCustTabs(true);
    try {
      const enc = encodeURIComponent(code.trim());
      const [sumRes, detRes, ptsRes] = await Promise.all([
        fetch(`${API_BASE_URL}/customer/${enc}/sales-summary`),
        fetch(`${API_BASE_URL}/customer/${enc}/sales-detail`),
        fetch(`${API_BASE_URL}/customer/${enc}/points`)
      ]);
      const sumJson = await sumRes.json();
      const detJson = await detRes.json();
      const ptsJson = await ptsRes.json();

      setSalesSummaryData(sumJson.success && Array.isArray(sumJson.data) ? sumJson.data : []);
      setSalesDetailData(detJson.success && Array.isArray(detJson.data) ? detJson.data : []);
      setCustPointsData(ptsJson.success && Array.isArray(ptsJson.data) ? ptsJson.data : []);
    } catch (err) {
      console.warn('Error fetching tab data:', err);
      setSalesSummaryData([]);
      setSalesDetailData([]);
      setCustPointsData([]);
    } finally {
      setLoadingCustTabs(false);
    }
  };

  // Load customer into Customer Details modal form
  const loadCustomerIntoDetailsForm = async (codeOrCust: any) => {
    const code = typeof codeOrCust === 'string' ? codeOrCust.trim() : (codeOrCust?.CustomerCode || '').trim();
    if (!code) return;

    try {
      const res = await fetch(`${API_BASE_URL}/customer/${encodeURIComponent(code)}`);
      const json = await res.json();
      if (json.success && json.data) {
        const d = json.data;
        setNewCustCode(d.CustomerCode || '');
        setNewCustType(d.CustomerType || 'DIAMOND');
        setNewCustName(d.CustomerName || '');
        setNewCustAddress(d.Address1 || '');
        setNewCustCountry(d.Address2 || '');
        setNewCustPostalCode(d.Address3 || '');
        setNewCustNationality(d.Address4 || '');
        setNewCustGender(d.Gender === 'F' ? 'Female' : 'Male');
        setNewCustNric(d.NricNo || '');
        setNewCustRemarks(d.Remarks || '');
        setNewCustPassport(d.PassportNo || '');
        setNewCustPhone1(d.Phone1 || '');
        setNewCustPhone2(d.Phone2 || '');
        setNewCustMobile(d.Mobile || '');
        setNewCustFax(d.Fax || '');
        setNewCustEmail(d.eMail || '');
        setNewCustReligion(d.DateType || '');
        
        // DOB
        if (d.DOB) {
          const parts = d.DOB.split('-');
          setNewCustDobYear(parts[0] || '');
          setNewCustDobMonth(parts[1] || '');
          setNewCustDobDay(parts[2] || '');
        } else {
          setNewCustDobYear('');
          setNewCustDobMonth('');
          setNewCustDobDay('');
        }

        // DOA
        if (d.DOA) {
          const parts = d.DOA.split('-');
          setNewCustDoaYear(parts[0] || '');
          setNewCustDoaMonth(parts[1] || '');
          setNewCustDoaDay(parts[2] || '');
        } else {
          setNewCustDoaYear('');
          setNewCustDoaMonth('');
          setNewCustDoaDay('');
        }

        setNewCustActive(d.GLAccount1 !== 'N');
        setNewCustContact(d.AccountType === 'S' ? 'SMS' : d.AccountType === 'P' ? 'Phone' : 'None');

        setCustPointsSummary({
          totalReward: d.TotalRewardPoints || 0,
          totalRedeemed: d.TotalRedeemedPoints || 0,
          balance: d.BalancePoints || 0
        });

        // Fetch tab data for this customer
        fetchCustTabsData(d.CustomerCode);
      }
    } catch (err) {
      console.error('Error loading customer into details form:', err);
    }
  };

  // Navigate to existing Customer Details Full Screen (/customer)
  const handleOpenNewCustomer = () => {
    router.push({
      pathname: '/customer' as any,
      params: {
        returnPath: '/utility/gold-scheme-transaction',
        prevSchemeCode: schemeCode,
        prevSchemeValue: schemeValue,
        prevMode: mode,
        prevSalesmanCode: salesmanCode,
        prevSalesmanName: salesmanName,
        prevSalesmanPassword: salesmanPassword,
      }
    });
  };

  // Save Customer to SQL Database (Insert or Update)
  const handleSaveCustomer = async () => {
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
    if (!newCustType.trim()) {
      const msg = 'Please select Customer Type.';
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
        CustomerType: newCustType.trim(),
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
        const msg = json.message || 'Customer saved successfully!';
        Platform.OS === 'web' ? window.alert(msg) : Alert.alert('Success', msg);

        // Fetch saved customer from SQL Server to display exact saved details
        const savedCode = newCustCode.trim();
        await loadCustomerIntoDetailsForm(savedCode);

        // Automatically close Customer Details modal and populate main Gold Scheme Transaction screen
        setCustomerDetailsVisible(false);
        setCustomerCode(savedCode);
        fetchCustomerDetailsByCode(savedCode);
      } else {
        const msg = json.message || 'Failed to save customer.';
        Platform.OS === 'web' ? window.alert(msg) : Alert.alert('Error', msg);
      }
    } catch (err) {
      console.error('Error saving customer:', err);
      const msg = 'Unable to connect to server.';
      Platform.OS === 'web' ? window.alert(msg) : Alert.alert('Network Error', msg);
    } finally {
      setSavingCustomer(false);
    }
  };

  // Delete Customer from SQL Server
  const handleDeleteCustomer = () => {
    if (!newCustCode.trim()) {
      const msg = 'No customer selected to delete.';
      Platform.OS === 'web' ? window.alert(msg) : Alert.alert('Warning', msg);
      return;
    }

    const doDelete = async () => {
      setDeletingCustomer(true);
      try {
        const res = await fetch(`${API_BASE_URL}/customer/${encodeURIComponent(newCustCode.trim())}`, {
          method: 'DELETE'
        });
        const json = await res.json();
        if (json.success) {
          const msg = 'Customer deleted successfully.';
          Platform.OS === 'web' ? window.alert(msg) : Alert.alert('Success', msg);
          handleOpenNewCustomer();
        } else {
          const msg = json.message || 'Failed to delete customer.';
          Platform.OS === 'web' ? window.alert(msg) : Alert.alert('Cannot Delete', msg);
        }
      } catch (err) {
        console.error('Delete customer error:', err);
        const msg = 'Error connecting to server to delete customer.';
        Platform.OS === 'web' ? window.alert(msg) : Alert.alert('Error', msg);
      } finally {
        setDeletingCustomer(false);
      }
    };

    if (Platform.OS === 'web') {
      if (window.confirm(`Are you sure you want to delete customer ${newCustCode}?`)) {
        doDelete();
      }
    } else {
      Alert.alert(
        'Confirm Delete',
        `Are you sure you want to delete customer ${newCustCode}?`,
        [
          { text: 'Cancel', style: 'cancel' },
          { text: 'Delete', style: 'destructive', onPress: doDelete }
        ]
      );
    }
  };

  // KYC Actions
  const handleOpenAddKycModal = () => {
    if (!newCustCode.trim()) {
      const msg = 'Please select or save a customer first.';
      Platform.OS === 'web' ? window.alert(msg) : Alert.alert('Notice', msg);
      return;
    }
    setKycData({
      CustomerCode: newCustCode,
      CustomerName: newCustName,
      NricNo: newCustNric,
      PassportNo: newCustPassport,
      Nationality: newCustNationality,
      Address: [newCustAddress, newCustCountry, newCustPostalCode].filter(Boolean).join(', '),
      DOB: (newCustDobYear && newCustDobMonth && newCustDobDay) ? `${newCustDobYear}-${newCustDobMonth}-${newCustDobDay}` : '',
      CustomerImageName: '',
      KycStatus: (newCustNric || newCustPassport) ? 'Verified' : 'Pending',
      Remarks: newCustRemarks
    });
    setAddKycModalVisible(true);
  };

  const handleSaveKycDetails = async () => {
    setSavingKyc(true);
    try {
      const res = await fetch(`${API_BASE_URL}/customer/${encodeURIComponent(newCustCode.trim())}/kyc`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(kycData)
      });
      const json = await res.json();
      if (json.success) {
        const msg = 'KYC details saved successfully.';
        Platform.OS === 'web' ? window.alert(msg) : Alert.alert('Success', msg);
        setAddKycModalVisible(false);
        // Refresh customer form
        loadCustomerIntoDetailsForm(newCustCode);
      } else {
        const msg = json.message || 'Failed to save KYC.';
        Platform.OS === 'web' ? window.alert(msg) : Alert.alert('Error', msg);
      }
    } catch (err) {
      console.error('Error saving KYC:', err);
      const msg = 'Error connecting to server.';
      Platform.OS === 'web' ? window.alert(msg) : Alert.alert('Error', msg);
    } finally {
      setSavingKyc(false);
    }
  };

  const handleOpenShowKycModal = async () => {
    if (!newCustCode.trim()) {
      const msg = 'Please select or save a customer first.';
      Platform.OS === 'web' ? window.alert(msg) : Alert.alert('Notice', msg);
      return;
    }
    try {
      const res = await fetch(`${API_BASE_URL}/customer/${encodeURIComponent(newCustCode.trim())}/kyc`);
      const json = await res.json();
      if (json.success && json.data) {
        setKycData(json.data);
        setShowKycModalVisible(true);
      } else {
        const msg = json.message || 'Customer KYC record not found.';
        Platform.OS === 'web' ? window.alert(msg) : Alert.alert('Notice', msg);
      }
    } catch (err) {
      console.error('Error loading KYC:', err);
    }
  };

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
    fetchDailyRate();
  };

  // Save Transaction (F7) -> Navigates to existing Paying Mode Tender Full Screen (/payingmode)
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

    const valNum = parseFloat(schemeValue) || 0;
    if (valNum <= 0) {
      const msg = 'Scheme Value must be greater than zero.';
      Platform.OS === 'web' ? window.alert(msg) : Alert.alert('Validation Error', msg);
      return;
    }

    // Calculate total net payment required
    let totalNet = valNum;
    if (mode === 'GRAM' && dailyRate > 0) {
      totalNet = parseFloat((valNum * dailyRate).toFixed(2));
    }

    // Construct transaction payload for /api/scheme/transaction
    const schemePayload = {
      customerCode: customerCode.trim(),
      customerName,
      customerType,
      salesmanCode,
      schemeCode: schemeCode.trim(),
      schemeValue: schemeValue.trim(),
      mode,
      dailyRate: Number(dailyRate) || 0,
      totalNet,
      schemeNo
    };

    router.push({
      pathname: '/payingmode' as any,
      params: {
        totalNet: String(totalNet),
        customerCode: customerCode.trim(),
        customerType: customerType || '',
        apiEndpoint: `${API_BASE_URL}/scheme/transaction`,
        payload: JSON.stringify(schemePayload),
        returnPath: '/utility/gold-scheme-transaction',
        prevSchemeCode: schemeCode,
        prevSchemeValue: schemeValue,
        prevMode: mode,
        prevSalesmanCode: salesmanCode,
        prevSalesmanName: salesmanName,
        prevSalesmanPassword: salesmanPassword,
      }
    });
  };

  // Called when user clicks Save(F7) in Paying Mode modal
  const handleConfirmPayingModeSave = async (
    payments: Array<{ paymode: string; amount: number; slipNo: string; commission: number }>,
    summary: { total: number; paid: number; balance: number; commission: number; returnAmt: number }
  ) => {
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
        dailyRate: Number(dailyRate) || 0,
        payments: payments.map(p => ({
          paymode: p.paymode,
          amount: p.amount,
          slipNo: p.slipNo,
          commission: p.commission
        })),
        totalNet: summary.total,
        paidAmount: summary.paid,
        returnAmount: summary.returnAmt,
        commission: summary.commission
      };

      const res = await fetch(`${API_BASE_URL}/scheme/transaction`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload),
      });
      const json = await res.json();

      if (json.success) {
        setPayingModalVisible(false);
        const msg = json.message || 'Scheme Transaction saved successfully!';
        Platform.OS === 'web' ? window.alert(msg) : Alert.alert('Success', msg);
        fetchTransactionHistory(customerCode);
        fetchNextSchemeNo();
        // Reset scheme input fields after successful transaction
        setSchemeCode('');
        setSchemeValue('');
      } else {
        const msg = json.message || 'Failed to save Scheme Transaction.';
        Platform.OS === 'web' ? window.alert(msg) : Alert.alert('Error', msg);
      }
    } catch (err) {
      console.error('Error saving scheme transaction with paying modes:', err);
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
                <View style={[styles.radioContainerBox, { marginRight: 16 }]}>
                  <TouchableOpacity style={styles.radioButton} onPress={() => { setMode('VALUE'); fetchDailyRate(); }}>
                    <View style={[styles.radioOuter, mode === 'VALUE' && styles.radioOuterSelected]}>
                      {mode === 'VALUE' && <View style={styles.radioInner} />}
                    </View>
                    <Text style={styles.radioText}>By Value</Text>
                  </TouchableOpacity>

                  <TouchableOpacity style={styles.radioButton} onPress={() => { setMode('GRAM'); fetchDailyRate(); }}>
                    <View style={[styles.radioOuter, mode === 'GRAM' && styles.radioOuterSelected]}>
                      {mode === 'GRAM' && <View style={styles.radioInner} />}
                    </View>
                    <Text style={styles.radioText}>By Gram</Text>
                  </TouchableOpacity>
                </View>

                {/* Daily Rate Display Box (Applicable for both By Value & By Gram) */}
                <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                  <Text style={styles.schemeLabel}>Daily Rate</Text>
                  <View style={[styles.readOnlyBox, { width: 110, height: 32, marginLeft: 8, justifyContent: 'center' }]}>
                    {fetchingDailyRate ? (
                      <ActivityIndicator size="small" color="#9E6D08" />
                    ) : (
                      <Text style={[styles.readOnlyText, { fontWeight: '700', color: '#9E6D08' }]}>
                        {dailyRate > 0 ? Number(dailyRate).toFixed(2) : '0.00'}
                      </Text>
                    )}
                  </View>
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
      {/* 1. CUSTOMER SEARCH HELP SEARCH MODAL */}
      {/* ========================================================= */}
      <Modal visible={customerModalVisible} transparent animationType="fade" onRequestClose={() => setCustomerModalVisible(false)}>
        <View style={styles.legacyModalOverlay}>
          <View style={[styles.legacyModalContainer, { width: Platform.OS === 'web' ? 760 : '96%' }]}>
            
            {/* Header: Icon & Close Only (Heading text removed) */}
            <View style={[styles.legacyModalHeader, { height: 40 }]}>
              <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                <MaterialCommunityIcons name="card-search-outline" size={20} color="#9E6D08" />
              </View>
              <Text style={styles.legacyRecordCountText}>
                <Text style={{ color: '#DC2626', fontWeight: 'bold' }}>{custTotalRecords}</Text> Records Found
              </Text>
              <TouchableOpacity onPress={() => setCustomerModalVisible(false)} style={styles.legacyModalCloseBtn}>
                <Ionicons name="close" size={16} color="#333" />
              </TouchableOpacity>
            </View>

            {/* Clean compact search bar with Dropdown, Input, and Search Button */}
            <View style={[styles.legacySearchControlArea, { paddingVertical: 10 }]}>
              <View style={[styles.legacySearchBoxRow, { zIndex: 50 }]}>
                {/* Search Field Dropdown (CustomerName, CustomerCode, NricNo) */}
                <View style={{ position: 'relative', zIndex: 60, marginRight: 8 }}>
                  <TouchableOpacity 
                    style={[styles.legacyDropdownSelect, { height: 34, minWidth: 130 }]} 
                    onPress={() => setShowCustFieldDropdown(!showCustFieldDropdown)}
                  >
                    <Text style={styles.legacyDropdownText}>{custSearchField}</Text>
                    <Ionicons name="caret-down" size={12} color="#000" style={{ marginLeft: 4 }} />
                  </TouchableOpacity>

                  {/* Dropdown Options Menu: exactly CustomerName, CustomerCode, NricNo */}
                  {showCustFieldDropdown && (
                    <View style={[styles.dropdownMenuOverlay, { top: 38, left: 0, minWidth: 130, zIndex: 100 }]}>
                      {(['CustomerName', 'CustomerCode', 'NricNo'] as const).map((field) => (
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

                {/* Search Input */}
                <TextInput
                  style={[styles.legacySearchTextInput, { height: 34 }]}
                  placeholder={`Search ${custSearchField}...`}
                  placeholderTextColor="#94A3B8"
                  value={custSearchText}
                  onChangeText={(text) => {
                    setCustSearchText(text);
                    fetchCustomerSearchResults(custSearchField, text, custSearchMode);
                  }}
                  onSubmitEditing={() => fetchCustomerSearchResults(custSearchField, custSearchText, custSearchMode)}
                />

                {/* Search Button */}
                <TouchableOpacity 
                  style={[styles.legacySearchExecuteBtn, { height: 34 }]} 
                  onPress={() => fetchCustomerSearchResults(custSearchField, custSearchText, custSearchMode)}
                >
                  <Ionicons name="search" size={14} color="#FFFFFF" style={{ marginRight: 4 }} />
                  <Text style={styles.legacySearchExecuteBtnText}>Search</Text>
                </TouchableOpacity>
              </View>
            </View>

            {/* Customer Results Grid / Table with Fixed Headers and Exactly Matching Widths */}
            <View style={styles.legacyGridWrapper}>
              {/* Fixed Header Row */}
              <View style={styles.legacyGridHeaderRow}>
                <Text style={[styles.legacyGridTh, { width: 140 }]}>CustomerCode</Text>
                <Text style={[styles.legacyGridTh, { width: 220 }]}>CustomerName</Text>
                <Text style={[styles.legacyGridTh, { width: 120 }]}>NricNo</Text>
                <Text style={[styles.legacyGridTh, { flex: 1, minWidth: 180 }]}>Address1</Text>
              </View>

              {loadingCustomerSearch ? (
                <View style={{ padding: 24, alignItems: 'center' }}>
                  <ActivityIndicator size="small" color="#9E6D08" />
                  <Text style={{ fontSize: 12, color: '#8F7E66', marginTop: 8 }}>Loading customers...</Text>
                </View>
              ) : (
                <ScrollView style={{ maxHeight: 340 }}>
                  {customerSearchResults.length > 0 ? (
                    customerSearchResults.map((row, index) => {
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
                          <Text style={[styles.legacyGridTd, { width: 140, fontWeight: '700', color: '#9E6D08' }]}>
                            {row.CustomerCode}
                          </Text>
                          <Text style={[styles.legacyGridTd, { width: 220, flexWrap: 'wrap' }]}>
                            {row.CustomerName}
                          </Text>
                          <Text style={[styles.legacyGridTd, { width: 120, flexWrap: 'wrap' }]}>
                            {row.NricNo || ''}
                          </Text>
                          <Text style={[styles.legacyGridTd, { flex: 1, minWidth: 180, flexWrap: 'wrap' }]}>
                            {row.Address1 || ''}
                          </Text>
                        </TouchableOpacity>
                      );
                    })
                  ) : (
                    <View style={{ padding: 24, alignItems: 'center' }}>
                      <Text style={{ fontSize: 12, color: '#8F7E66' }}>No matching customer records found</Text>
                    </View>
                  )}
                </ScrollView>
              )}
            </View>

          </View>
        </View>
      </Modal>

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
            <View style={[styles.legacySearchControlArea, { paddingVertical: 10 }]}>
              
              {/* Row 1: Search Field Dropdown on Left, Records Found on Right */}
              <View style={[styles.legacyTopMetaRow, { zIndex: 50, marginBottom: 4 }]}>
                <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                  <Text style={styles.legacyLabelText}>Search Field </Text>
                  <View style={{ position: 'relative', zIndex: 60 }}>
                    <TouchableOpacity 
                      style={styles.legacyDropdownSelect} 
                      onPress={() => setShowSchemeFieldDropdown(!showSchemeFieldDropdown)}
                    >
                      <Text style={styles.legacyDropdownText}>{schemeSearchField}</Text>
                      <Ionicons name="caret-down" size={12} color="#000" style={{ marginLeft: 4 }} />
                    </TouchableOpacity>

                    {/* Dropdown Options Menu */}
                    {showSchemeFieldDropdown && (
                      <View style={[styles.dropdownMenuOverlay, { top: 34, left: 0, minWidth: 140, zIndex: 100 }]}>
                        {(['Code', 'Description', 'Remarks', 'CreateUser'] as const).map((field) => (
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
                </View>

                <Text style={styles.legacyRecordCountText}>
                  <Text style={{ color: '#DC2626', fontWeight: 'bold' }}>{schemeTotalRecords}</Text> Records Found
                </Text>
              </View>

              {/* Row 2: Selected Field Title & Search Text Box */}
              <View style={styles.legacyInputSection}>
                <Text style={styles.legacySearchFieldColumnTitle}>{schemeSearchField}</Text>
                <View style={styles.legacySearchBoxRow}>
                  <TextInput
                    style={styles.legacySearchTextInput}
                    placeholder="Enter search text..."
                    placeholderTextColor="#94A3B8"
                    value={schemeSearchText}
                    onChangeText={(text) => {
                      setSchemeSearchText(text);
                      fetchSchemeSearchResults(schemeSearchField, text, schemeSearchMode);
                    }}
                    onSubmitEditing={() => fetchSchemeSearchResults(schemeSearchField, schemeSearchText, schemeSearchMode)}
                  />
                  <TouchableOpacity 
                    style={styles.legacySearchExecuteBtn} 
                    onPress={() => fetchSchemeSearchResults(schemeSearchField, schemeSearchText, schemeSearchMode)}
                  >
                    <Ionicons name="search" size={13} color="#FFFFFF" style={{ marginRight: 4 }} />
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
  
  custDetailsModalHeaderBar: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    paddingHorizontal: 16,
    paddingVertical: 12,
    backgroundColor: '#F5E7CA',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
  },
  custDetailsModalHeaderTitle: {
    fontSize: 14,
    fontWeight: '800',
    color: '#3B2D17',
  },
  custCodeSearchBtn: {
    height: 28,
    width: 28,
    backgroundColor: '#9E6D08',
    borderRadius: 6,
    justifyContent: 'center',
    alignItems: 'center',
    marginLeft: 6,
  },
  custTypeDropdownMenu: {
    position: 'absolute',
    top: 32,
    left: 0,
    right: 0,
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#DFD5C3',
    borderRadius: 6,
    zIndex: 999,
    elevation: 10,
    shadowColor: '#3B2D17',
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.15,
    shadowRadius: 8,
  },
  custTypeDropdownItem: {
    paddingHorizontal: 10,
    paddingVertical: 6,
    borderBottomWidth: 1,
    borderBottomColor: '#F0E6D2',
  },
  custTypeDropdownItemActive: {
    backgroundColor: '#FAF7F0',
  },
  custTypeDropdownText: {
    fontSize: 11,
    color: '#3B2D17',
  },
  custTypeDropdownTextActive: {
    fontWeight: '700',
    color: '#9E6D08',
  },
  custTableRow: {
    flexDirection: 'row',
    paddingVertical: 6,
    paddingHorizontal: 8,
    borderBottomWidth: 1,
    borderBottomColor: '#F0E6D2',
    alignItems: 'center',
  },
  custTableTd: {
    fontSize: 11,
    color: '#332717',
  },
  pointsSummaryBanner: {
    flexDirection: 'row',
    justifyContent: 'space-around',
    paddingVertical: 8,
    backgroundColor: '#FAF7F0',
    borderTopWidth: 1,
    borderTopColor: '#E8DCB8',
  },
  pointsSummaryText: {
    fontSize: 11,
    color: '#6B5738',
    fontWeight: '600',
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
