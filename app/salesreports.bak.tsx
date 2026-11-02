import React, { useState, useEffect, useMemo } from 'react';
import {
  View,
  Text,
  StyleSheet,
  ScrollView,
  TouchableOpacity,
  TextInput,
  ActivityIndicator,
  Modal,
  Platform,
  useWindowDimensions,
  Alert,
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';
import { Ionicons } from '@expo/vector-icons';
import { Stack, useRouter } from 'expo-router';
import { API_BASE_URL } from '@/constants/api';

// ==========================================
// SALES REPORT OPTIONS (All 20 Reports)
// ==========================================
interface ReportOption {
  id: string;
  label: string;
  columnGroup?: 'left' | 'right';
  description: string;
  supportsDetail: boolean;
  supportsGstOptions?: boolean;
  supportsWeightPcs?: boolean;
  supportsPaymodeFilter?: boolean;
  supportsSupplierFilter?: boolean;
  supportsSalesmanFilter?: boolean;
  supportsTradeInOptions?: boolean;
  supportsClassFilter?: boolean;
  supportsCategoryFilter?: boolean;
  supportsCatalogueFilter?: boolean;
  supportsSizeFilter?: boolean;
  supportsAkshaya?: boolean;
}

const REPORT_OPTIONS: ReportOption[] = [
  {
    id: 'sales_journal',
    label: 'Sales Journal',
    description: 'Chronological sales invoices with tax, discount and net total breakdown',
    supportsDetail: true,
    supportsGstOptions: true,
    supportsClassFilter: true,
    supportsCategoryFilter: true,
    supportsCatalogueFilter: true,
    supportsSizeFilter: true,
  },
  {
    id: 'sales_by_weight_pcs',
    label: 'Sales by Weight and PCS',
    description: 'Item sales filtered and grouped by weight scales and piece count',
    supportsDetail: false,
    supportsWeightPcs: true,
  },
  {
    id: 'trade_in_sales',
    label: 'Trade In Sales',
    description: 'Gold trade-in intake transactions applied towards customer sales',
    supportsDetail: false,
    supportsTradeInOptions: true,
  },
  {
    id: 'installment_entry',
    label: 'Installment Entry',
    description: 'Active installment agreement records, tenures, and initial deposits',
    supportsDetail: false,
  },
  {
    id: 'installment_received',
    label: 'Installment Received',
    description: 'Payments received towards ongoing customer gold installment schemes',
    supportsDetail: false,
  },
  {
    id: 'sales_return',
    label: 'Sales Return',
    description: 'Customer credit notes, sales returns and returned inventory pieces',
    supportsDetail: false,
  },
  {
    id: 'bill_cancel',
    label: 'Bill Cancel',
    description: 'Voided and cancelled invoices audit tracking report',
    supportsDetail: false,
  },
  {
    id: 'daily_sales',
    label: 'Daily Sales',
    description: 'Consolidated end-of-day sales register across all payment tenders',
    supportsDetail: false,
  },
  {
    id: 'sales_by_supplier',
    label: 'Sales By Supplier',
    description: 'Sales performance, volume and margins categorized by vendor/supplier',
    supportsDetail: false,
    supportsSupplierFilter: true,
  },
  {
    id: 'pending_order',
    label: 'Pending Order',
    description: 'Customer custom orders and sales orders pending fulfillment',
    supportsDetail: false,
  },
  {
    id: 'sales_journal_by_category',
    label: 'Sales Journal By Category',
    description: 'Summary of sales turnover and margins grouped by item category',
    supportsDetail: false,
    supportsCategoryFilter: true,
  },
  {
    id: 'sales_journal_by_catalogue',
    label: 'Sales Journal By Catalogue',
    description: 'Sales breakdown by catalogue model design numbers',
    supportsDetail: false,
    supportsCatalogueFilter: true,
  },
  {
    id: 'paymode_wise_sales',
    label: 'Paymode Wise Sales',
    description: 'Tender collection totals across Cash, NETS, Visa, Master, Bank Transfer',
    supportsDetail: false,
    supportsPaymodeFilter: true,
    supportsAkshaya: true,
  },
  {
    id: 'salesman_wise_journal',
    label: 'Salesman Wise Journal',
    description: 'Individual sales representative performance, turnover, and volume',
    supportsDetail: false,
    supportsSalesmanFilter: true,
  },
  {
    id: 'pay_out',
    label: 'Pay Out',
    description: 'Cash pay-out vouchers and store expense disbursements',
    supportsDetail: false,
  },
  {
    id: 'pending_installment',
    label: 'Pending Installment',
    description: 'Customer installment balances remaining unpaid as of selected period',
    supportsDetail: false,
  },
  {
    id: 'gst_report',
    label: 'GST Report',
    description: 'Tax authority audit report with standard rated and zero rated breakdown',
    supportsDetail: false,
    supportsGstOptions: true,
  },
  {
    id: 'flexi_payments',
    label: 'Flexi Payments',
    description: 'Flexible split payments and installment payment settlement vouchers',
    supportsDetail: false,
  },
  {
    id: 'trade_in_only',
    label: 'Trade In Only',
    description: 'Scrap and trade-in gold purchases register without invoice netting',
    supportsDetail: false,
  },
  {
    id: 'deposit_journal',
    label: 'Deposit Journal',
    description: 'Customer advance deposit receipts, balances, and invoice redemptions',
    supportsDetail: false,
  },
];

const MONTH_NAMES = [
  'January', 'February', 'March', 'April', 'May', 'June',
  'July', 'August', 'September', 'October', 'November', 'December'
];

export default function SalesReportsScreen() {
  const router = useRouter();
  const { width } = useWindowDimensions();
  const isDesktop = Platform.OS === 'web' ? (typeof window !== 'undefined' ? window.innerWidth >= 1024 : true) : width >= 1024;

  // Selected Report
  const [selectedReport, setSelectedReport] = useState<string>('');
  const [isReportDropdownOpen, setIsReportDropdownOpen] = useState<boolean>(false);
  const [reportType, setReportType] = useState<'Summary' | 'Detail'>('Summary');

  // Collapsible Filter Options Panel
  const [isFilterExpanded, setIsFilterExpanded] = useState<boolean>(false);

  // Conditional Report Settings
  const [gstOption, setGstOption] = useState<'All' | 'With GST' | 'Zero Rated'>('All');
  const [weightPcsChoice, setWeightPcsChoice] = useState<'2' | '4' | '6' | '8'>('2');
  const [akshayaTritiya, setAkshayaTritiya] = useState<boolean>(false);
  const [etrsStatus, setEtrsStatus] = useState<'All' | 'eTRS Ticket Issued' | 'Not for eTRS' | 'eTRS Eligible'>('All');
  const [voidFilter, setVoidFilter] = useState<boolean>(false);

  // Filter Values
  const [locationCode, setLocationCode] = useState<string>('HQ');
  const [supplierCode, setSupplierCode] = useState<string>('ALL');
  const [classCode, setClassCode] = useState<string>('');
  const [categoryCode, setCategoryCode] = useState<string>('');
  const [catalogueCode, setCatalogueCode] = useState<string>('');
  const [sizeCode, setSizeCode] = useState<string>('');
  const [paymode, setPaymode] = useState<string>('ALL');
  const [salesmanCode, setSalesmanCode] = useState<string>('ALL');
  const [tradeInOption, setTradeInOption] = useState<string>('ALL');

  // Dates
  const formatIsoDate = (d: Date) => {
    const y = d.getFullYear();
    const m = String(d.getMonth() + 1).padStart(2, '0');
    const day = String(d.getDate()).padStart(2, '0');
    return `${y}-${m}-${day}`;
  };

  const today = new Date();
  const defaultFrom = '2025-01-01';
  const defaultTo = formatIsoDate(today);

  const [fromDate, setFromDate] = useState<string>(defaultFrom);
  const [toDate, setToDate] = useState<string>(defaultTo);

  // Master Data Lists (loaded from backend)
  const [locations, setLocations] = useState<any[]>([]);
  const [suppliers, setSuppliers] = useState<any[]>([]);
  const [classes, setClasses] = useState<any[]>([]);
  const [categories, setCategories] = useState<any[]>([]);
  const [catalogues, setCatalogues] = useState<any[]>([]);
  const [sizes, setSizes] = useState<any[]>([]);
  const [paymodes, setPaymodes] = useState<any[]>([]);
  const [salesmen, setSalesmen] = useState<any[]>([]);
  const [tradeInList, setTradeInList] = useState<any[]>([]);

  // Search Dialog LOV Modal State
  const [lovModal, setLovModal] = useState<{
    visible: boolean;
    type: 'class' | 'category' | 'catalogue' | 'size' | 'supplier' | 'location' | 'paymode' | 'salesman' | 'tradein' | null;
    title: string;
    searchQuery: string;
  }>({
    visible: false,
    type: null,
    title: '',
    searchQuery: ''
  });

  // Calendar Modal State
  const [datePickerTarget, setDatePickerTarget] = useState<'from' | 'to' | null>(null);
  const [pickerYear, setPickerYear] = useState<number>(today.getFullYear());
  const [pickerMonth, setPickerMonth] = useState<number>(today.getMonth());
  const [tempDateStr, setTempDateStr] = useState<string>('');

  // Report Execution & Results State
  const [isGenerating, setIsGenerating] = useState<boolean>(false);
  const [isExporting, setIsExporting] = useState<boolean>(false);
  const [reportResult, setReportResult] = useState<{
    title: string;
    company?: any;
    dateRange?: { from: string; to: string };
    columns: Array<{ key: string; label: string; width: number; align?: 'left' | 'center' | 'right'; format?: string }>;
    data: any[];
    totals: any;
  } | null>(null);

  const [errorMessage, setErrorMessage] = useState<string | null>(null);
  const [currentPage, setCurrentPage] = useState<number>(1);
  const pageSize = 50;

  // Active Report Object
  const selectedReportObj = useMemo(() => {
    return REPORT_OPTIONS.find((r) => r.id === selectedReport) || null;
  }, [selectedReport]);

  // Load Initial Master Lookups
  useEffect(() => {
    loadMasterLookups();
  }, []);

  const loadMasterLookups = async () => {
    try {
      const [locRes, suppRes, clsRes, catRes, ctlRes, szRes, pmRes, smRes, tiRes] = await Promise.all([
        fetch(`${API_BASE_URL}/salesreports/locations`).then((r) => r.json()).catch(() => ({ data: [] })),
        fetch(`${API_BASE_URL}/salesreports/suppliers`).then((r) => r.json()).catch(() => ({ data: [] })),
        fetch(`${API_BASE_URL}/salesreports/classes`).then((r) => r.json()).catch(() => ({ data: [] })),
        fetch(`${API_BASE_URL}/salesreports/categories`).then((r) => r.json()).catch(() => ({ data: [] })),
        fetch(`${API_BASE_URL}/salesreports/catalogues`).then((r) => r.json()).catch(() => ({ data: [] })),
        fetch(`${API_BASE_URL}/salesreports/sizes`).then((r) => r.json()).catch(() => ({ data: [] })),
        fetch(`${API_BASE_URL}/salesreports/paymodes`).then((r) => r.json()).catch(() => ({ data: [] })),
        fetch(`${API_BASE_URL}/salesreports/salesmen`).then((r) => r.json()).catch(() => ({ data: [] })),
        fetch(`${API_BASE_URL}/salesreports/trade-in-options`).then((r) => r.json()).catch(() => ({ data: [] })),
      ]);

      if (locRes.data) setLocations(locRes.data);
      if (suppRes.data) setSuppliers(suppRes.data);
      if (clsRes.data) setClasses(clsRes.data);
      if (catRes.data) setCategories(catRes.data);
      if (ctlRes.data) setCatalogues(ctlRes.data);
      if (szRes.data) setSizes(szRes.data);
      if (pmRes.data) setPaymodes(pmRes.data);
      if (smRes.data) setSalesmen(smRes.data);
      if (tiRes.data) setTradeInList(tiRes.data);
    } catch (err) {
      console.error('Failed to load master lookups:', err);
    }
  };

  // Keyboard Shortcuts (F7/F9: Get Report, Esc: Close)
  useEffect(() => {
    if (Platform.OS === 'web' && typeof window !== 'undefined') {
      const handleKeyDown = (e: KeyboardEvent) => {
        const target = e.target as HTMLElement;
        if (target && (target.tagName === 'INPUT' || target.tagName === 'TEXTAREA')) {
          if (e.key === 'Escape') target.blur();
          return;
        }

        if (e.key === 'F7' || e.key === 'F9') {
          e.preventDefault();
          handleGetReport();
        } else if (e.key === 'Escape') {
          if (isReportDropdownOpen) {
            setIsReportDropdownOpen(false);
          } else if (lovModal.visible) {
            setLovModal({ visible: false, type: null, title: '', searchQuery: '' });
          } else if (datePickerTarget) {
            setDatePickerTarget(null);
          } else {
            router.back();
          }
        }
      };
      window.addEventListener('keydown', handleKeyDown);
      return () => window.removeEventListener('keydown', handleKeyDown);
    }
  }, [
    isReportDropdownOpen,
    lovModal.visible,
    datePickerTarget,
    selectedReport,
    reportType,
    locationCode,
    supplierCode,
    classCode,
    categoryCode,
    catalogueCode,
    sizeCode,
    paymode,
    salesmanCode,
    tradeInOption,
    fromDate,
    toDate,
    gstOption,
    weightPcsChoice,
    akshayaTritiya,
    etrsStatus,
    voidFilter,
  ]);

  // Handle Get Report
  const handleGetReport = async () => {
    if (isGenerating) return;
    setErrorMessage(null);

    if (!selectedReport) {
      setErrorMessage('Please select a Sales Report.');
      Alert.alert('Validation Error', 'Please select a Sales Report.');
      return;
    }

    const dFrom = new Date(fromDate);
    const dTo = new Date(toDate);
    if (isNaN(dFrom.getTime())) {
      setErrorMessage('Enter Valid From Date..');
      Alert.alert('Validation Error', 'Enter Valid From Date..');
      return;
    }
    if (isNaN(dTo.getTime())) {
      setErrorMessage('Enter Valid To Date..');
      Alert.alert('Validation Error', 'Enter Valid To Date..');
      return;
    }
    if (dTo < dFrom) {
      setErrorMessage('To Date is lesser then From Date');
      Alert.alert('Validation Error', 'To Date is lesser then From Date');
      return;
    }

    setIsGenerating(true);
    setCurrentPage(1);

    try {
      const response = await fetch(`${API_BASE_URL}/salesreports/generate`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          reportId: selectedReport,
          reportType: selectedReportObj?.supportsDetail ? reportType : 'Summary',
          filters: {
            locationCode,
            supplierCode,
            classCode,
            categoryCode,
            catalogueCode,
            sizeCode,
            paymode,
            salesmanCode,
            tradeInOption,
            fromDate,
            toDate,
            gstOption,
            weightPcsChoice,
            akshayaTritiya,
            voidFilter,
            etrsStatus,
          },
        }),
      });

      const json = await response.json();
      if (!response.ok || !json.success) {
        throw new Error(json.message || 'Failed to generate sales report');
      }

      setReportResult({
        title: json.title,
        company: json.company,
        dateRange: json.dateRange,
        columns: json.columns || [],
        data: json.data || [],
        totals: json.totals || {},
      });

      // Automatically collapse filter options panel once report data arrives
      setIsFilterExpanded(false);
    } catch (err: any) {
      console.error('Sales report generation error:', err);
      setErrorMessage(err.message || 'An error occurred while generating the report.');
      setReportResult(null);
    } finally {
      setIsGenerating(false);
    }
  };

  // Handle Export to Professional A4 Report
  const handleExportExcel = async () => {
    if (!reportResult || !reportResult.data || reportResult.data.length === 0) {
      Alert.alert('No Data', 'Please generate a report with results before exporting.');
      return;
    }

    setIsExporting(true);
    try {
      const res = await fetch(`${API_BASE_URL}/salesreports/download-html`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          title: reportResult.title,
          columns: reportResult.columns,
          data: reportResult.data,
          totals: reportResult.totals,
          fromDate,
          toDate,
        }),
      });

      if (!res.ok) {
        throw new Error('Failed to generate report template');
      }

      const htmlContent = await res.text();

      if (Platform.OS === 'web' && typeof window !== 'undefined') {
        const blob = new Blob([htmlContent], { type: 'text/html;charset=utf-8;' });
        const url = URL.createObjectURL(blob);
        const win = window.open(url, '_blank');
        if (!win) {
          const link = document.createElement('a');
          link.href = url;
          const cleanTitle = (reportResult.title || 'Sales_Report').replace(/[^a-zA-Z0-9_\-]/g, '_');
          link.setAttribute('download', `${cleanTitle}.html`);
          document.body.appendChild(link);
          link.click();
          document.body.removeChild(link);
        }
      } else {
        Alert.alert('Report Ready', 'Professional A4 report generated successfully.');
      }
    } catch (err: any) {
      console.error('Export Error:', err);
      Alert.alert('Export Error', err.message || 'Failed to export report');
    } finally {
      setIsExporting(false);
    }
  };

  // Reset Filters
  const handleResetFilters = () => {
    setSelectedReport('');
    setIsReportDropdownOpen(false);
    setIsFilterExpanded(false);
    setLocationCode('HQ');
    setSupplierCode('ALL');
    setClassCode('');
    setCategoryCode('');
    setCatalogueCode('');
    setSizeCode('');
    setPaymode('ALL');
    setSalesmanCode('ALL');
    setTradeInOption('ALL');
    setGstOption('All');
    setWeightPcsChoice('2');
    setAkshayaTritiya(false);
    setVoidFilter(false);
    setEtrsStatus('All');
    setFromDate(defaultFrom);
    setToDate(defaultTo);
    setReportResult(null);
    setErrorMessage(null);
  };

  // Pagination Slice
  const paginatedRows = useMemo(() => {
    if (!reportResult || !reportResult.data) return [];
    const startIndex = (currentPage - 1) * pageSize;
    return reportResult.data.slice(startIndex, startIndex + pageSize);
  }, [reportResult, currentPage]);

  const totalPages = useMemo(() => {
    if (!reportResult || !reportResult.data || reportResult.data.length === 0) return 1;
    return Math.ceil(reportResult.data.length / pageSize);
  }, [reportResult]);

  // LOV Filtered List
  const lovFilteredList = useMemo(() => {
    if (!lovModal.type) return [];
    const q = (lovModal.searchQuery || '').toLowerCase().trim();

    let list: Array<{ code: string; label: string; sub?: string }> = [];

    if (lovModal.type === 'location') {
      list = locations.map((l) => ({
        code: l.LocationCode,
        label: `${l.LocationCode} - ${l.LocationName || l.LocationCode}`,
      }));
    } else if (lovModal.type === 'supplier') {
      list = suppliers.map((s) => ({
        code: s.SupplierCode,
        label: `${s.SupplierCode} - ${s.SupplierName}`,
      }));
    } else if (lovModal.type === 'class') {
      list = classes.map((c) => ({
        code: c.ClassCode,
        label: `${c.ClassCode} - ${c.ClassName}`,
      }));
    } else if (lovModal.type === 'category') {
      list = categories.map((c) => ({
        code: c.CategoryCode,
        label: `${c.CategoryCode} - ${c.CategoryName}`,
      }));
    } else if (lovModal.type === 'catalogue') {
      list = catalogues.map((c) => ({
        code: c.CatalogueCode,
        label: `${c.CatalogueCode} - ${c.CatalogueName}`,
      }));
    } else if (lovModal.type === 'size') {
      list = sizes.map((s) => ({
        code: s.SizeCode,
        label: `${s.SizeCode} - ${s.SizeType}`,
      }));
    } else if (lovModal.type === 'paymode') {
      list = paymodes.map((p) => ({
        code: p.PayMode,
        label: p.PayModeName || p.PayMode,
      }));
    } else if (lovModal.type === 'salesman') {
      list = salesmen.map((sm) => ({
        code: sm.SalesManCode,
        label: `${sm.SalesManCode} - ${sm.SalesManName}`,
      }));
    } else if (lovModal.type === 'tradein') {
      list = tradeInList.map((ti) => ({
        code: ti.Code,
        label: `${ti.Code} - ${ti.Name}`,
      }));
    }

    if (!q) return list;
    return list.filter((item) => item.label.toLowerCase().includes(q) || item.code.toLowerCase().includes(q));
  }, [lovModal, locations, suppliers, classes, categories, catalogues, sizes, paymodes, salesmen, tradeInList]);

  const handleSelectLovItem = (code: string) => {
    if (lovModal.type === 'location') setLocationCode(code);
    else if (lovModal.type === 'supplier') setSupplierCode(code);
    else if (lovModal.type === 'class') setClassCode(code === 'ALL' ? '' : code);
    else if (lovModal.type === 'category') setCategoryCode(code === 'ALL' ? '' : code);
    else if (lovModal.type === 'catalogue') setCatalogueCode(code === 'ALL' ? '' : code);
    else if (lovModal.type === 'size') setSizeCode(code === 'ALL' ? '' : code);
    else if (lovModal.type === 'paymode') setPaymode(code);
    else if (lovModal.type === 'salesman') setSalesmanCode(code);
    else if (lovModal.type === 'tradein') setTradeInOption(code);

    setLovModal({ visible: false, type: null, title: '', searchQuery: '' });
  };

  // Calendar Helpers
  const openDatePicker = (target: 'from' | 'to') => {
    const val = target === 'from' ? fromDate : toDate;
    setTempDateStr(val);
    if (val && val.includes('-')) {
      const parts = val.split('-');
      if (parts.length === 3) {
        const y = parseInt(parts[0], 10);
        const m = parseInt(parts[1], 10) - 1;
        if (!isNaN(y)) setPickerYear(y);
        if (!isNaN(m) && m >= 0 && m <= 11) setPickerMonth(m);
      }
    }
    setDatePickerTarget(target);
  };

  const calendarDays = useMemo(() => {
    const firstDayIndex = new Date(pickerYear, pickerMonth, 1).getDay();
    const daysInCurrentMonth = new Date(pickerYear, pickerMonth + 1, 0).getDate();
    const daysInPrevMonth = new Date(pickerYear, pickerMonth, 0).getDate();

    const days: { day: number; isCurrentMonth: boolean }[] = [];
    for (let i = firstDayIndex - 1; i >= 0; i--) {
      days.push({ day: daysInPrevMonth - i, isCurrentMonth: false });
    }
    for (let i = 1; i <= daysInCurrentMonth; i++) {
      days.push({ day: i, isCurrentMonth: true });
    }
    const remaining = 42 - days.length;
    for (let i = 1; i <= remaining; i++) {
      days.push({ day: i, isCurrentMonth: false });
    }
    return days;
  }, [pickerYear, pickerMonth]);

  const handleSelectDay = (day: number) => {
    const mStr = String(pickerMonth + 1).padStart(2, '0');
    const dStr = String(day).padStart(2, '0');
    const iso = `${pickerYear}-${mStr}-${dStr}`;
    if (datePickerTarget === 'from') setFromDate(iso);
    if (datePickerTarget === 'to') setToDate(iso);
    setDatePickerTarget(null);
  };

  const formatDisp = (iso: string) => {
    if (!iso) return '';
    const parts = iso.split('-');
    if (parts.length === 3) return `${parts[2]} - ${parts[1]} - ${parts[0]}`;
    return iso;
  };

  const hasSummary = useMemo(() => {
    if (!reportResult || !reportResult.data || reportResult.data.length === 0) return false;
    const t = reportResult.totals || {};
    return t.totalPcs > 0 || t.totalWeight > 0 || t.totalAmount > 0 || t.totalGoldAmount > 0 || t.totalProfit > 0;
  }, [reportResult]);

  return (
    <SafeAreaView style={styles.safeArea} edges={['top', 'bottom', 'left', 'right']}>
      <Stack.Screen options={{ headerShown: false }} />

      {/* TOP HEADER - Gold Scheme Transaction theme */}
      <View style={styles.topHeader}>
        <View style={styles.headerLeft}>
          <TouchableOpacity
            style={styles.backButton}
            onPress={() => router.back()}
            activeOpacity={0.7}
          >
            <Ionicons name="arrow-back" size={18} color="#9A7B38" />
          </TouchableOpacity>
          <Text style={styles.headerTitle}>Sales Reports</Text>
        </View>
      </View>

      <ScrollView contentContainerStyle={styles.scrollContainer} showsVerticalScrollIndicator={true}>
        
        {/* ========================================================= */}
        {/* TOP FILTER BAR: Dropdown, Dates, Find & Clear Buttons */}
        {/* ========================================================= */}
        <View style={[styles.topFilterBarCard, isReportDropdownOpen ? styles.topFilterBarCardActive : null]}>
          <View style={[styles.topControlsRow, isDesktop ? styles.topControlsRowDesktop : styles.topControlsRowMobile]}>
            
            {/* 1. Sales Report Dropdown */}
            <View style={[styles.controlItem, styles.reportDropdownWrapper]}>
              <Text style={styles.controlLabel}>Sales Report</Text>
              <TouchableOpacity
                style={[
                  styles.dropdownSelectBox,
                  selectedReport ? styles.dropdownSelectBoxHasValue : null,
                  isReportDropdownOpen ? styles.dropdownSelectBoxActive : null
                ]}
                onPress={() => setIsReportDropdownOpen(!isReportDropdownOpen)}
                activeOpacity={0.8}
              >
                <Text
                  style={[
                    styles.dropdownSelectText,
                    !selectedReportObj && styles.dropdownSelectPlaceholderText
                  ]}
                  numberOfLines={1}
                >
                  {selectedReportObj ? selectedReportObj.label : '-- Select --'}
                </Text>
                <Ionicons
                  name={isReportDropdownOpen ? 'chevron-up' : 'chevron-down'}
                  size={16}
                  color="#D97706"
                />
              </TouchableOpacity>

              {/* Dropdown Options Popup */}
              {isReportDropdownOpen && (
                <View style={styles.reportDropdownMenu}>
                  <ScrollView style={{ maxHeight: 280 }} nestedScrollEnabled>
                    {REPORT_OPTIONS.map((item) => {
                      const isSelected = selectedReport === item.id;
                      return (
                        <TouchableOpacity
                          key={item.id}
                          style={[styles.reportMenuItem, isSelected && styles.reportMenuItemSelected]}
                          onPress={() => {
                            setSelectedReport(item.id);
                            setIsReportDropdownOpen(false);
                            setReportResult(null);
                            setIsFilterExpanded(true);
                          }}
                        >
                          <Text style={[styles.reportMenuItemText, isSelected && styles.reportMenuItemTextSelected]}>
                            {item.label}
                          </Text>
                          {isSelected && (
                            <Ionicons name="checkmark" size={16} color="#D97706" />
                          )}
                        </TouchableOpacity>
                      );
                    })}
                  </ScrollView>
                </View>
              )}
            </View>

            {/* 2. From Date */}
            <View style={[styles.controlItem, styles.dateControlWrapper]}>
              <Text style={styles.controlLabel}>From Date</Text>
              <TouchableOpacity
                style={[styles.datePickerInput, fromDate ? styles.datePickerInputHasValue : null]}
                onPress={() => openDatePicker('from')}
                activeOpacity={0.8}
              >
                <Text style={styles.dateInputText}>{formatDisp(fromDate)}</Text>
                <Ionicons name="calendar-outline" size={16} color="#D97706" />
              </TouchableOpacity>
            </View>

            {/* 3. To Date */}
            <View style={[styles.controlItem, styles.dateControlWrapper]}>
              <Text style={styles.controlLabel}>To Date</Text>
              <TouchableOpacity
                style={[styles.datePickerInput, toDate ? styles.datePickerInputHasValue : null]}
                onPress={() => openDatePicker('to')}
                activeOpacity={0.8}
              >
                <Text style={styles.dateInputText}>{formatDisp(toDate)}</Text>
                <Ionicons name="calendar-outline" size={16} color="#D97706" />
              </TouchableOpacity>
            </View>

            {/* 4. Action Buttons (Find & Clear) */}
            <View style={styles.topButtonsWrapper}>
              <TouchableOpacity
                style={[styles.btnFind, isGenerating && { opacity: 0.7 }]}
                onPress={handleGetReport}
                disabled={isGenerating}
                activeOpacity={0.8}
              >
                {isGenerating ? (
                  <ActivityIndicator size="small" color="#FFFFFF" style={{ marginRight: 6 }} />
                ) : null}
                <Text style={styles.btnFindText}>Find</Text>
              </TouchableOpacity>

              <TouchableOpacity
                style={styles.btnClear}
                onPress={handleResetFilters}
                activeOpacity={0.8}
              >
                <Text style={styles.btnClearText}>Clear</Text>
              </TouchableOpacity>
            </View>

          </View>
        </View>

        {/* ========================================================= */}
        {/* COLLAPSIBLE FILTER OPTIONS ACCORDION PANEL */}
        {/* ========================================================= */}
        {selectedReport && selectedReportObj ? (
          <View style={styles.collapsiblePanelCard}>
            <TouchableOpacity
              style={styles.collapsibleHeader}
              onPress={() => setIsFilterExpanded(!isFilterExpanded)}
              activeOpacity={0.8}
            >
              <View style={styles.collapsibleTitleRow}>
                <Text style={styles.collapsibleTitleText}>Filter Options</Text>
              </View>
              <Ionicons
                name={isFilterExpanded ? 'chevron-up' : 'chevron-down'}
                size={18}
                color="#ff7f27"
              />
            </TouchableOpacity>

            {isFilterExpanded && (
              <View style={styles.collapsibleBody}>
                <View style={[styles.filterInputsRow, isDesktop ? styles.filterInputsRowDesktop : styles.filterInputsRowMobile, { flexWrap: 'wrap' }]}>
                  
                  {/* Location Code */}
                  <View style={[styles.filterFieldCol, isDesktop && { minWidth: 160, flex: 1 }]}>
                    <Text style={styles.filterFieldLabel}>Location</Text>
                    <TouchableOpacity
                      style={[styles.filterDropdownSelector, locationCode ? styles.filterDropdownSelectorHasValue : null]}
                      onPress={() => setLovModal({ visible: true, type: 'location', title: 'Select Location', searchQuery: '' })}
                      activeOpacity={0.8}
                    >
                      <Text style={styles.filterDropdownValue} numberOfLines={1}>
                        {locationCode ? `${locationCode} ${locations.find(l => l.LocationCode === locationCode)?.LocationName ? `- ${locations.find(l => l.LocationCode === locationCode)?.LocationName}` : ''}` : 'Select Location'}
                      </Text>
                      <Ionicons name="chevron-down" size={15} color="#888" />
                    </TouchableOpacity>
                  </View>

                  {/* Supplier Code */}
                  {(selectedReportObj.supportsSupplierFilter || selectedReport === 'sales_journal') && (
                    <View style={[styles.filterFieldCol, isDesktop && { minWidth: 160, flex: 1 }]}>
                      <Text style={styles.filterFieldLabel}>Supplier Code</Text>
                      <TouchableOpacity
                        style={[styles.filterDropdownSelector, supplierCode && supplierCode !== 'ALL' ? styles.filterDropdownSelectorHasValue : null]}
                        onPress={() => setLovModal({ visible: true, type: 'supplier', title: 'Select Supplier', searchQuery: '' })}
                        activeOpacity={0.8}
                      >
                        <Text style={styles.filterDropdownValue} numberOfLines={1}>
                          {supplierCode ? `${supplierCode} ${suppliers.find(s => s.SupplierCode === supplierCode)?.SupplierName ? `- ${suppliers.find(s => s.SupplierCode === supplierCode)?.SupplierName}` : ''}` : 'Select Supplier'}
                        </Text>
                        <Ionicons name="chevron-down" size={15} color="#888" />
                      </TouchableOpacity>
                    </View>
                  )}

                  {/* Salesman Code */}
                  {(selectedReportObj.supportsSalesmanFilter || selectedReport === 'sales_journal') && (
                    <View style={[styles.filterFieldCol, isDesktop && { minWidth: 160, flex: 1 }]}>
                      <Text style={styles.filterFieldLabel}>Salesman</Text>
                      <TouchableOpacity
                        style={[styles.filterDropdownSelector, salesmanCode && salesmanCode !== 'ALL' ? styles.filterDropdownSelectorHasValue : null]}
                        onPress={() => setLovModal({ visible: true, type: 'salesman', title: 'Select Salesman', searchQuery: '' })}
                        activeOpacity={0.8}
                      >
                        <Text style={styles.filterDropdownValue} numberOfLines={1}>
                          {salesmanCode ? `${salesmanCode} ${salesmen.find(sm => sm.SalesManCode === salesmanCode)?.SalesManName ? `- ${salesmen.find(sm => sm.SalesManCode === salesmanCode)?.SalesManName}` : ''}` : 'Select Salesman'}
                        </Text>
                        <Ionicons name="chevron-down" size={15} color="#888" />
                      </TouchableOpacity>
                    </View>
                  )}

                  {/* Class */}
                  {selectedReportObj.supportsClassFilter && (
                    <View style={[styles.filterFieldCol, isDesktop && { minWidth: 160, flex: 1 }]}>
                      <Text style={styles.filterFieldLabel}>Class</Text>
                      <TouchableOpacity
                        style={[styles.filterDropdownSelector, classCode ? styles.filterDropdownSelectorHasValue : null]}
                        onPress={() => setLovModal({ visible: true, type: 'class', title: 'Select Class', searchQuery: '' })}
                        activeOpacity={0.8}
                      >
                        <Text style={styles.filterDropdownValue} numberOfLines={1}>
                          {classCode || 'Select Class'}
                        </Text>
                        <Ionicons name="chevron-down" size={15} color="#888" />
                      </TouchableOpacity>
                    </View>
                  )}

                  {/* Category */}
                  {selectedReportObj.supportsCategoryFilter && (
                    <View style={[styles.filterFieldCol, isDesktop && { minWidth: 160, flex: 1 }]}>
                      <Text style={styles.filterFieldLabel}>Category</Text>
                      <TouchableOpacity
                        style={[styles.filterDropdownSelector, categoryCode ? styles.filterDropdownSelectorHasValue : null]}
                        onPress={() => setLovModal({ visible: true, type: 'category', title: 'Select Category', searchQuery: '' })}
                        activeOpacity={0.8}
                      >
                        <Text style={styles.filterDropdownValue} numberOfLines={1}>
                          {categoryCode || 'Select Category'}
                        </Text>
                        <Ionicons name="chevron-down" size={15} color="#888" />
                      </TouchableOpacity>
                    </View>
                  )}

                  {/* Catalogue */}
                  {selectedReportObj.supportsCatalogueFilter && (
                    <View style={[styles.filterFieldCol, isDesktop && { minWidth: 160, flex: 1 }]}>
                      <Text style={styles.filterFieldLabel}>Catalogue</Text>
                      <TouchableOpacity
                        style={[styles.filterDropdownSelector, catalogueCode ? styles.filterDropdownSelectorHasValue : null]}
                        onPress={() => setLovModal({ visible: true, type: 'catalogue', title: 'Select Catalogue', searchQuery: '' })}
                        activeOpacity={0.8}
                      >
                        <Text style={styles.filterDropdownValue} numberOfLines={1}>
                          {catalogueCode || 'Select Catalogue'}
                        </Text>
                        <Ionicons name="chevron-down" size={15} color="#888" />
                      </TouchableOpacity>
                    </View>
                  )}

                  {/* Size */}
                  {selectedReportObj.supportsSizeFilter && (
                    <View style={[styles.filterFieldCol, isDesktop && { minWidth: 160, flex: 1 }]}>
                      <Text style={styles.filterFieldLabel}>Size</Text>
                      <TouchableOpacity
                        style={[styles.filterDropdownSelector, sizeCode ? styles.filterDropdownSelectorHasValue : null]}
                        onPress={() => setLovModal({ visible: true, type: 'size', title: 'Select Size', searchQuery: '' })}
                        activeOpacity={0.8}
                      >
                        <Text style={styles.filterDropdownValue} numberOfLines={1}>
                          {sizeCode || 'Select Size'}
                        </Text>
                        <Ionicons name="chevron-down" size={15} color="#888" />
                      </TouchableOpacity>
                    </View>
                  )}

                  {/* Paymode */}
                  {selectedReportObj.supportsPaymodeFilter && (
                    <View style={[styles.filterFieldCol, isDesktop && { minWidth: 160, flex: 1 }]}>
                      <Text style={styles.filterFieldLabel}>Paymode</Text>
                      <TouchableOpacity
                        style={[styles.filterDropdownSelector, paymode && paymode !== 'ALL' ? styles.filterDropdownSelectorHasValue : null]}
                        onPress={() => setLovModal({ visible: true, type: 'paymode', title: 'Select Paymode', searchQuery: '' })}
                        activeOpacity={0.8}
                      >
                        <Text style={styles.filterDropdownValue} numberOfLines={1}>
                          {paymode || 'ALL'}
                        </Text>
                        <Ionicons name="chevron-down" size={15} color="#888" />
                      </TouchableOpacity>
                    </View>
                  )}

                  {/* Trade In Option */}
                  {selectedReportObj.supportsTradeInOptions && (
                    <View style={[styles.filterFieldCol, isDesktop && { minWidth: 160, flex: 1 }]}>
                      <Text style={styles.filterFieldLabel}>Trade In Option</Text>
                      <TouchableOpacity
                        style={[styles.filterDropdownSelector, tradeInOption && tradeInOption !== 'ALL' ? styles.filterDropdownSelectorHasValue : null]}
                        onPress={() => setLovModal({ visible: true, type: 'tradein', title: 'Select Trade-In Option', searchQuery: '' })}
                        activeOpacity={0.8}
                      >
                        <Text style={styles.filterDropdownValue} numberOfLines={1}>
                          {tradeInOption || 'ALL'}
                        </Text>
                        <Ionicons name="chevron-down" size={15} color="#888" />
                      </TouchableOpacity>
                    </View>
                  )}

                  {/* Report View (Summary / Detail) */}
                  {selectedReportObj.supportsDetail && (
                    <View style={[styles.filterFieldCol, isDesktop && { minWidth: 160, flex: 1 }]}>
                      <Text style={styles.filterFieldLabel}>Report View</Text>
                      <View style={styles.radioInlineGroup}>
                        <TouchableOpacity
                          style={[styles.radioItem, reportType === 'Summary' && styles.radioItemActive]}
                          onPress={() => setReportType('Summary')}
                        >
                          <Text style={[styles.radioItemText, reportType === 'Summary' && styles.radioItemTextActive]}>Summary</Text>
                        </TouchableOpacity>
                        <TouchableOpacity
                          style={[styles.radioItem, reportType === 'Detail' && styles.radioItemActive]}
                          onPress={() => setReportType('Detail')}
                        >
                          <Text style={[styles.radioItemText, reportType === 'Detail' && styles.radioItemTextActive]}>Detail</Text>
                        </TouchableOpacity>
                      </View>
                    </View>
                  )}

                  {/* GST Options */}
                  {selectedReportObj.supportsGstOptions && (
                    <View style={[styles.filterFieldCol, isDesktop && { minWidth: 220, flex: 1.2 }]}>
                      <Text style={styles.filterFieldLabel}>GST Mode</Text>
                      <View style={styles.radioInlineGroup}>
                        {(['All', 'With GST', 'Zero Rated'] as const).map((opt) => (
                          <TouchableOpacity
                            key={opt}
                            style={[styles.radioItem, gstOption === opt && styles.radioItemActive]}
                            onPress={() => setGstOption(opt)}
                          >
                            <Text style={[styles.radioItemText, gstOption === opt && styles.radioItemTextActive]}>{opt}</Text>
                          </TouchableOpacity>
                        ))}
                      </View>
                    </View>
                  )}

                  {/* Weight & PCS Scale Choice */}
                  {selectedReportObj.supportsWeightPcs && (
                    <View style={[styles.filterFieldCol, isDesktop && { minWidth: 200, flex: 1 }]}>
                      <Text style={styles.filterFieldLabel}>Scale Breakpoint</Text>
                      <View style={styles.radioInlineGroup}>
                        {(['2', '4', '6', '8'] as const).map((scale) => (
                          <TouchableOpacity
                            key={scale}
                            style={[styles.radioItem, weightPcsChoice === scale && styles.radioItemActive]}
                            onPress={() => setWeightPcsChoice(scale)}
                          >
                            <Text style={[styles.radioItemText, weightPcsChoice === scale && styles.radioItemTextActive]}>{scale}g</Text>
                          </TouchableOpacity>
                        ))}
                      </View>
                    </View>
                  )}

                  {/* Akshaya Tritiya checkbox */}
                  {selectedReportObj.supportsAkshaya && (
                    <View style={[styles.filterFieldCol, isDesktop && { minWidth: 160, flex: 1 }]}>
                      <Text style={styles.filterFieldLabel}>Special Option</Text>
                      <TouchableOpacity style={styles.checkboxOption} onPress={() => setAkshayaTritiya(!akshayaTritiya)}>
                        <View style={[styles.checkboxBox, akshayaTritiya && styles.checkboxBoxActive]}>
                          {akshayaTritiya && <Ionicons name="checkmark" size={13} color="#FFFFFF" />}
                        </View>
                        <Text style={styles.checkboxLabelText}>Akshaya Tritiya</Text>
                      </TouchableOpacity>
                    </View>
                  )}

                </View>
              </View>
            )}
          </View>
        ) : null}

        {/* ========================================================= */}
        {/* SUMMARY CARDS */}
        {/* ========================================================= */}
        {hasSummary && reportResult && (
          <View style={[styles.summaryCardsGrid, isDesktop ? styles.summaryCardsRow : styles.summaryCardsCol]}>
            <View style={styles.summaryCard}>
              <Text style={styles.summaryCardLabel}>TOTAL RECORDS</Text>
              <Text style={styles.summaryCardValue}>
                {reportResult.totals?.totalRecords || reportResult.data.length}
              </Text>
            </View>

            {reportResult.totals?.totalWeight > 0 && (
              <View style={styles.summaryCard}>
                <Text style={styles.summaryCardLabel}>TOTAL WEIGHT (G)</Text>
                <Text style={styles.summaryCardValue}>
                  {Number(reportResult.totals.totalWeight).toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 3 })}
                </Text>
              </View>
            )}

            {(reportResult.totals?.totalAmount > 0 || reportResult.totals?.totalSales > 0) && (
              <View style={styles.summaryCard}>
                <Text style={styles.summaryCardLabel}>TOTAL SALES</Text>
                <Text style={[styles.summaryCardValue, { color: '#E27D00' }]}>
                  ${Number(reportResult.totals?.totalAmount || reportResult.totals?.totalSales || 0).toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 })}
                </Text>
              </View>
            )}
          </View>
        )}

        {/* ========================================================= */}
        {/* REPORT RESULTS TABLE CARD WITH DOWNLOAD BUTTON */}
        {/* ========================================================= */}
        {(isGenerating || reportResult || errorMessage) ? (
          <View style={styles.resultsCard}>
            <View style={styles.resultsHeaderRow}>
              <View style={styles.resultsHeaderLeft}>
                <Text style={styles.resultsHeaderTitle}>
                  {reportResult?.title || (selectedReportObj ? selectedReportObj.label : 'Report Results')}
                </Text>
                {reportResult && (
                  <View style={styles.recordsCountBadge}>
                    <Text style={styles.recordsCountText}>{reportResult.data.length} Records</Text>
                  </View>
                )}
              </View>

              {/* Orange Download Icon Button */}
              <TouchableOpacity
                style={[
                  styles.btnDownloadSquare,
                  (!reportResult?.data?.length || isExporting) && { opacity: 0.5 }
                ]}
                onPress={handleExportExcel}
                disabled={!reportResult?.data?.length || isExporting}
                activeOpacity={0.8}
              >
                {isExporting ? (
                  <ActivityIndicator size="small" color="#FFFFFF" />
                ) : (
                  <Ionicons name="download-outline" size={20} color="#FFFFFF" />
                )}
              </TouchableOpacity>
            </View>

            {/* ERROR ALERT BANNER */}
            {errorMessage && (
              <View style={styles.errorBanner}>
                <Ionicons name="alert-circle" size={18} color="#DC2626" style={{ marginRight: 8 }} />
                <Text style={styles.errorText}>{errorMessage}</Text>
              </View>
            )}

            {/* LOADING STATE */}
            {isGenerating ? (
              <View style={styles.loadingContainer}>
                <ActivityIndicator size="large" color="#E27D00" />
                <Text style={styles.loadingText}>Loading report from database...</Text>
              </View>
            ) : reportResult && reportResult.data && reportResult.data.length > 0 ? (
              <View>
                <ScrollView horizontal showsHorizontalScrollIndicator={true}>
                  <View>
                    {/* Table Header Row */}
                    <View style={styles.tableHeaderRow}>
                      <View style={[styles.tableHeaderCell, { width: 50, justifyContent: 'center' }]}>
                        <Text style={styles.tableHeaderText}>#</Text>
                      </View>
                      {reportResult.columns.map((col: any) => (
                        <View
                          key={col.key}
                          style={[
                            styles.tableHeaderCell,
                            { width: col.width || 130 },
                            col.align === 'right' && { alignItems: 'flex-end' }
                          ]}
                        >
                          <Text style={styles.tableHeaderText}>{col.label}</Text>
                        </View>
                      ))}
                    </View>

                    {/* Table Body Rows */}
                    {paginatedRows.map((row: any, idx: number) => {
                      const rowNumber = (currentPage - 1) * pageSize + idx + 1;
                      const isEven = idx % 2 === 0;
                      return (
                        <View
                          key={`row_${idx}`}
                          style={[styles.tableBodyRow, isEven ? styles.tableRowEven : styles.tableRowOdd]}
                        >
                          <View style={[styles.tableBodyCell, { width: 50, justifyContent: 'center' }]}>
                            <Text style={styles.tableCellIndexText}>{rowNumber}</Text>
                          </View>
                          {reportResult.columns.map((col: any) => {
                            let val = row[col.key];
                            let formattedVal = val !== null && val !== undefined ? String(val) : '-';

                            if (col.format === 'date' && val) {
                              formattedVal = formatDisp(formatIsoDate(new Date(val)));
                            } else if (col.format === 'currency' && val !== null && val !== undefined) {
                              formattedVal = `$ ${Number(val).toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 })}`;
                            } else if (col.format === 'decimal3' && val !== null && val !== undefined) {
                              formattedVal = Number(val).toFixed(3);
                            }

                            return (
                              <View
                                key={col.key}
                                style={[
                                  styles.tableBodyCell,
                                  { width: col.width || 130 },
                                  col.align === 'right' && { alignItems: 'flex-end' }
                                ]}
                              >
                                <Text style={styles.tableCellText} numberOfLines={1}>
                                  {formattedVal}
                                </Text>
                              </View>
                            );
                          })}
                        </View>
                      );
                    })}
                  </View>
                </ScrollView>

                {/* PAGINATION CONTROLS */}
                {totalPages > 1 && (
                  <View style={styles.paginationRow}>
                    <Text style={styles.paginationInfoText}>
                      Showing {((currentPage - 1) * pageSize) + 1} to {Math.min(currentPage * pageSize, reportResult.data.length)} of {reportResult.data.length} entries
                    </Text>
                    <View style={styles.paginationButtons}>
                      <TouchableOpacity
                        style={[styles.pageBtn, currentPage === 1 && styles.pageBtnDisabled]}
                        onPress={() => setCurrentPage(prev => Math.max(prev - 1, 1))}
                        disabled={currentPage === 1}
                      >
                        <Ionicons name="chevron-back" size={16} color={currentPage === 1 ? '#94A3B8' : '#334155'} />
                      </TouchableOpacity>

                      <Text style={styles.pageNumberText}>
                        Page {currentPage} of {totalPages}
                      </Text>

                      <TouchableOpacity
                        style={[styles.pageBtn, currentPage === totalPages && styles.pageBtnDisabled]}
                        onPress={() => setCurrentPage(prev => Math.min(prev + 1, totalPages))}
                        disabled={currentPage === totalPages}
                      >
                        <Ionicons name="chevron-forward" size={16} color={currentPage === totalPages ? '#94A3B8' : '#334155'} />
                      </TouchableOpacity>
                    </View>
                  </View>
                )}
              </View>
            ) : (
              <View style={styles.emptyContainer}>
                <Ionicons name="file-tray-outline" size={44} color="#CBD5E1" />
                <Text style={styles.emptyTitle}>No Data Available</Text>
                <Text style={styles.emptySubtitle}>
                  No records found matching the selected report and filter criteria.
                </Text>
              </View>
            )}
          </View>
        ) : null}

      </ScrollView>

      {/* ========================================================= */}
      {/* SEARCH / LOV SELECTION MODAL */}
      {/* ========================================================= */}
      <Modal
        visible={lovModal.visible}
        transparent={true}
        animationType="fade"
        onRequestClose={() => setLovModal(prev => ({ ...prev, visible: false }))}
      >
        <TouchableOpacity
          style={styles.modalOverlay}
          activeOpacity={1}
          onPress={() => setLovModal(prev => ({ ...prev, visible: false }))}
        >
          <TouchableOpacity activeOpacity={1} style={styles.lovModalCard}>
            <View style={styles.lovModalHeader}>
              <Text style={styles.lovModalTitle}>{lovModal.title}</Text>
              <TouchableOpacity onPress={() => setLovModal(prev => ({ ...prev, visible: false }))}>
                <Ionicons name="close" size={20} color="#64748B" />
              </TouchableOpacity>
            </View>

            <View style={styles.lovSearchContainer}>
              <Ionicons name="search" size={18} color="#94A3B8" style={{ marginRight: 8 }} />
              <TextInput
                style={styles.lovSearchInput}
                placeholder="Type to filter..."
                placeholderTextColor="#94A3B8"
                value={lovModal.searchQuery}
                onChangeText={(text) => setLovModal(prev => ({ ...prev, searchQuery: text }))}
                autoFocus={true}
              />
              {lovModal.searchQuery ? (
                <TouchableOpacity onPress={() => setLovModal(prev => ({ ...prev, searchQuery: '' }))}>
                  <Ionicons name="close-circle" size={16} color="#94A3B8" />
                </TouchableOpacity>
              ) : null}
            </View>

            <ScrollView style={styles.lovScrollView} keyboardShouldPersistTaps="handled">
              {lovFilteredList.length === 0 ? (
                <View style={{ padding: 24, alignItems: 'center' }}>
                  <Text style={{ color: '#94A3B8', fontSize: 13 }}>No matches found.</Text>
                </View>
              ) : (
                lovFilteredList.map((item, i) => (
                  <TouchableOpacity
                    key={`lov_${i}`}
                    style={styles.lovItemRow}
                    onPress={() => handleSelectLovItem(item.code)}
                  >
                    <View style={{ flex: 1 }}>
                      <Text style={styles.lovItemPrimaryText}>{item.label}</Text>
                      {item.sub ? <Text style={styles.lovItemSecondaryText}>{item.sub}</Text> : null}
                    </View>
                    <Ionicons name="chevron-forward" size={16} color="#CBD5E1" />
                  </TouchableOpacity>
                ))
              )}
            </ScrollView>

            <View style={styles.lovModalFooter}>
              <TouchableOpacity
                style={styles.lovModalCloseBtn}
                onPress={() => setLovModal(prev => ({ ...prev, visible: false }))}
              >
                <Text style={styles.lovModalCloseBtnText}>Close</Text>
              </TouchableOpacity>
            </View>
          </TouchableOpacity>
        </TouchableOpacity>
      </Modal>

      {/* ========================================================= */}
      {/* CALENDAR DATE PICKER MODAL */}
      {/* ========================================================= */}
      <Modal
        visible={datePickerTarget !== null}
        transparent={true}
        animationType="fade"
        onRequestClose={() => setDatePickerTarget(null)}
      >
        <TouchableOpacity
          style={styles.modalOverlay}
          activeOpacity={1}
          onPress={() => setDatePickerTarget(null)}
        >
          <TouchableOpacity activeOpacity={1} style={styles.calendarModalCard}>
            <View style={styles.calendarHeaderRow}>
              <TouchableOpacity
                style={styles.monthNavBtn}
                onPress={() => {
                  if (pickerMonth === 0) {
                    setPickerMonth(11);
                    setPickerYear(prev => prev - 1);
                  } else {
                    setPickerMonth(prev => prev - 1);
                  }
                }}
              >
                <Ionicons name="chevron-back" size={16} color="#1E293B" />
              </TouchableOpacity>

              <Text style={styles.monthYearTitle}>
                {MONTH_NAMES[pickerMonth]} {pickerYear}
              </Text>

              <TouchableOpacity
                style={styles.monthNavBtn}
                onPress={() => {
                  if (pickerMonth === 11) {
                    setPickerMonth(0);
                    setPickerYear(prev => prev + 1);
                  } else {
                    setPickerMonth(prev => prev + 1);
                  }
                }}
              >
                <Ionicons name="chevron-forward" size={16} color="#1E293B" />
              </TouchableOpacity>
            </View>

            <View style={styles.weekDaysRow}>
              {['Su', 'Mo', 'Tu', 'We', 'Th', 'Fr', 'Sa'].map(w => (
                <Text key={w} style={styles.weekDayText}>{w}</Text>
              ))}
            </View>

            <View style={styles.calendarGrid}>
              {calendarDays.map((dObj, idx) => {
                const isSelected = dObj.isCurrentMonth && (
                  (datePickerTarget === 'from' && fromDate === `${pickerYear}-${String(pickerMonth + 1).padStart(2, '0')}-${String(dObj.day).padStart(2, '0')}`) ||
                  (datePickerTarget === 'to' && toDate === `${pickerYear}-${String(pickerMonth + 1).padStart(2, '0')}-${String(dObj.day).padStart(2, '0')}`)
                );

                return (
                  <TouchableOpacity
                    key={`day_${idx}`}
                    style={[
                      styles.calendarDayCell,
                      isSelected && styles.calendarDayCellActive,
                      !dObj.isCurrentMonth && { opacity: 0.25 }
                    ]}
                    onPress={() => dObj.isCurrentMonth && handleSelectDay(dObj.day)}
                    disabled={!dObj.isCurrentMonth}
                  >
                    <Text style={[styles.calendarDayText, isSelected && styles.calendarDayTextActive]}>
                      {dObj.day}
                    </Text>
                  </TouchableOpacity>
                );
              })}
            </View>

            <View style={styles.datePresetsRow}>
              <TouchableOpacity
                style={styles.presetBadge}
                onPress={() => {
                  const todayIso = formatIsoDate(today);
                  if (datePickerTarget === 'from') setFromDate(todayIso);
                  if (datePickerTarget === 'to') setToDate(todayIso);
                  setDatePickerTarget(null);
                }}
              >
                <Text style={styles.presetBadgeText}>Today</Text>
              </TouchableOpacity>
            </View>
          </TouchableOpacity>
        </TouchableOpacity>
      </Modal>

    </SafeAreaView>
  );
}

// ==========================================
// STYLES - Gold Scheme Transaction Theme
// ==========================================
const styles = StyleSheet.create({
  safeArea: {
    flex: 1,
    backgroundColor: '#F8FAFC',
  },
  topHeader: {
    height: 44,
    borderBottomWidth: 1,
    borderBottomColor: '#E2E8F0',
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingHorizontal: 16,
    backgroundColor: '#FAF8F5',
  },
  headerLeft: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  backButton: {
    width: 28,
    height: 28,
    borderRadius: 6,
    borderWidth: 1,
    borderColor: '#E6DBBE',
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: '#FFFDF9',
    marginRight: 10,
  },
  headerTitle: {
    fontSize: 17,
    fontWeight: '700',
    color: '#0F172A',
    letterSpacing: -0.2,
  },

  scrollContainer: {
    padding: 12,
    gap: 10,
  },

  // TOP FILTER BAR CARD
  topFilterBarCard: {
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E2E8F0',
    padding: 12,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.03,
    shadowRadius: 2,
    elevation: 1,
    position: 'relative',
    zIndex: 10,
  },
  topFilterBarCardActive: {
    zIndex: 9999,
    elevation: 9999,
  },
  topControlsRow: {
    gap: 12,
    position: 'relative',
    zIndex: 10,
    flexDirection: Platform.OS === 'web' ? 'row' : undefined,
    alignItems: Platform.OS === 'web' ? 'flex-end' : undefined,
    flexWrap: Platform.OS === 'web' ? 'wrap' : undefined,
  },
  topControlsRowDesktop: {
    flexDirection: 'row',
    alignItems: 'flex-end',
  },
  topControlsRowMobile: {
    flexDirection: 'column',
  },
  controlItem: {
    flex: 1,
  },
  reportDropdownWrapper: {
    flex: 2,
    position: 'relative',
    zIndex: 9999,
  },
  dateControlWrapper: {
    flex: 1.2,
  },
  controlLabel: {
    fontSize: 12,
    fontWeight: '600',
    color: '#1E293B',
    marginBottom: 5,
  },
  dropdownSelectBox: {
    height: 32,
    borderWidth: 1,
    borderColor: '#CBD5E1',
    borderRadius: 6,
    backgroundColor: '#FFFFFF',
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingHorizontal: 10,
  },
  dropdownSelectBoxHasValue: {
    borderColor: '#D97706',
    borderWidth: 1.5,
    backgroundColor: '#FFFFFF',
  },
  dropdownSelectBoxActive: {
    borderColor: '#D97706',
    borderWidth: 1.5,
    backgroundColor: '#FFFFFF',
  },
  dropdownSelectText: {
    fontSize: 12.5,
    fontWeight: '600',
    color: '#0F172A',
    flex: 1,
    marginRight: 6,
  },
  dropdownSelectPlaceholderText: {
    color: '#94A3B8',
    fontWeight: '400',
  },
  reportDropdownMenu: {
    position: 'absolute',
    top: 58,
    left: 0,
    right: 0,
    backgroundColor: '#FFFFFF',
    borderRadius: 6,
    borderWidth: 1,
    borderColor: '#CBD5E1',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.12,
    shadowRadius: 8,
    elevation: 9999,
    zIndex: 99999,
  },
  reportMenuItem: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingVertical: 9,
    paddingHorizontal: 12,
    borderBottomWidth: 1,
    borderBottomColor: '#F1F5F9',
  },
  reportMenuItemSelected: {
    backgroundColor: '#FEF7E6',
  },
  reportMenuItemText: {
    fontSize: 12.5,
    color: '#334155',
    fontWeight: '500',
  },
  reportMenuItemTextSelected: {
    color: '#D97706',
    fontWeight: '700',
  },

  datePickerInput: {
    height: 32,
    borderWidth: 1,
    borderColor: '#CBD5E1',
    borderRadius: 6,
    backgroundColor: '#FFFFFF',
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingHorizontal: 10,
  },
  datePickerInputHasValue: {
    borderColor: '#D97706',
    borderWidth: 1.5,
    backgroundColor: '#FFFFFF',
  },
  dateInputText: {
    fontSize: 12.5,
    fontWeight: '600',
    color: '#0F172A',
  },

  topButtonsWrapper: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 8,
    paddingBottom: 1,
  },
  btnFind: {
    height: 32,
    paddingHorizontal: 18,
    backgroundColor: '#D97706',
    borderRadius: 6,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    shadowColor: '#D97706',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.2,
    shadowRadius: 2,
    elevation: 1,
  },
  btnFindText: {
    color: '#FFFFFF',
    fontWeight: '600',
    fontSize: 12.5,
  },
  btnClear: {
    height: 32,
    paddingHorizontal: 16,
    backgroundColor: '#FFFFFF',
    borderRadius: 6,
    borderWidth: 1,
    borderColor: '#CBD5E1',
    alignItems: 'center',
    justifyContent: 'center',
  },
  btnClearText: {
    color: '#334155',
    fontWeight: '600',
    fontSize: 12.5,
  },

  // COLLAPSIBLE FILTER ACCORDION
  collapsiblePanelCard: {
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E2E8F0',
    overflow: 'hidden',
    position: 'relative',
    zIndex: 1,
  },
  collapsibleHeader: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingHorizontal: 12,
    paddingVertical: 7,
    backgroundColor: '#FEF7E6',
    borderBottomWidth: 1,
    borderBottomColor: '#F3E8D0',
  },
  collapsibleTitleRow: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  collapsibleTitleText: {
    fontSize: 13,
    fontWeight: '700',
    color: '#0F172A',
    textTransform: 'uppercase',
    letterSpacing: 0.3,
  },
  collapsibleBody: {
    padding: 12,
    backgroundColor: '#FFFFFF',
  },
  filterInputsRow: {
    gap: 10,
  },
  filterInputsRowDesktop: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  filterInputsRowMobile: {
    flexDirection: 'column',
  },
  filterFieldCol: {
    flex: 1,
  },
  filterFieldLabel: {
    fontSize: 11.5,
    fontWeight: '600',
    color: '#1E293B',
    marginBottom: 5,
  },
  filterDropdownSelector: {
    height: 30,
    borderWidth: 1,
    borderColor: '#CBD5E1',
    borderRadius: 6,
    backgroundColor: '#FFFFFF',
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingHorizontal: 10,
  },
  filterDropdownSelectorHasValue: {
    borderColor: '#D97706',
    borderWidth: 1.5,
    backgroundColor: '#FFFFFF',
  },
  filterDropdownValue: {
    fontSize: 12,
    color: '#334155',
    fontWeight: '500',
    flex: 1,
    marginRight: 6,
  },
  radioInlineGroup: {
    flexDirection: 'row',
    height: 30,
    borderRadius: 6,
    borderWidth: 1,
    borderColor: '#CBD5E1',
    overflow: 'hidden',
  },
  radioItem: {
    flex: 1,
    alignItems: 'center',
    justifyContent: 'center',
    backgroundColor: '#F8FAFC',
  },
  radioItemActive: {
    backgroundColor: '#D97706',
  },
  radioItemText: {
    fontSize: 12,
    fontWeight: '600',
    color: '#475569',
  },
  radioItemTextActive: {
    color: '#FFFFFF',
  },
  checkboxOption: {
    flexDirection: 'row',
    alignItems: 'center',
    height: 30,
    gap: 6,
  },
  checkboxBox: {
    width: 16,
    height: 16,
    borderRadius: 4,
    borderWidth: 1,
    borderColor: '#CBD5E1',
    alignItems: 'center',
    justifyContent: 'center',
    backgroundColor: '#FFFFFF',
  },
  checkboxBoxActive: {
    borderColor: '#D97706',
    backgroundColor: '#D97706',
  },
  checkboxLabelText: {
    fontSize: 12,
    fontWeight: '500',
    color: '#334155',
  },

  // SUMMARY CARDS
  summaryCardsGrid: {
    gap: 10,
  },
  summaryCardsRow: {
    flexDirection: 'row',
  },
  summaryCardsCol: {
    flexDirection: 'column',
  },
  summaryCard: {
    flex: 1,
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E2E8F0',
    paddingVertical: 12,
    paddingHorizontal: 14,
    minHeight: 74,
    alignItems: 'center',
    justifyContent: 'center',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.03,
    shadowRadius: 2,
    elevation: 1,
  },
  summaryCardLabel: {
    fontSize: 10.5,
    fontWeight: '600',
    color: '#64748B',
    textTransform: 'uppercase',
    letterSpacing: 0.5,
    marginBottom: 4,
    textAlign: 'center',
  },
  summaryCardValue: {
    fontSize: 18,
    fontWeight: '700',
    color: '#D97706',
    textAlign: 'center',
  },

  // RESULTS TABLE CARD
  resultsCard: {
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E2E8F0',
    overflow: 'hidden',
  },
  resultsHeaderRow: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingHorizontal: 12,
    paddingVertical: 8,
    backgroundColor: '#FEF7E6',
    borderBottomWidth: 1,
    borderBottomColor: '#F3E8D0',
  },
  resultsHeaderLeft: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 8,
  },
  resultsHeaderTitle: {
    fontSize: 13,
    fontWeight: '700',
    color: '#0F172A',
  },
  recordsCountBadge: {
    backgroundColor: '#FFFDF9',
    borderWidth: 1,
    borderColor: '#E6DBBE',
    paddingHorizontal: 8,
    paddingVertical: 2,
    borderRadius: 6,
  },
  recordsCountText: {
    fontSize: 11,
    fontWeight: '600',
    color: '#9A7B38',
  },
  btnDownloadSquare: {
    width: 30,
    height: 30,
    backgroundColor: '#D97706',
    borderRadius: 6,
    alignItems: 'center',
    justifyContent: 'center',
    shadowColor: '#D97706',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.2,
    shadowRadius: 2,
    elevation: 1,
  },

  errorBanner: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FEF2F2',
    borderBottomWidth: 1,
    borderColor: '#FCA5A5',
    padding: 10,
  },
  errorText: {
    color: '#DC2626',
    fontSize: 12,
    fontWeight: '500',
    flex: 1,
  },

  loadingContainer: {
    padding: 30,
    alignItems: 'center',
    justifyContent: 'center',
  },
  loadingText: {
    marginTop: 8,
    fontSize: 12,
    color: '#64748B',
  },

  emptyContainer: {
    padding: 30,
    alignItems: 'center',
    justifyContent: 'center',
  },
  emptyTitle: {
    fontSize: 13,
    fontWeight: '700',
    color: '#0F172A',
    marginTop: 8,
  },
  emptySubtitle: {
    fontSize: 12,
    color: '#94A3B8',
    textAlign: 'center',
    marginTop: 4,
    maxWidth: 320,
  },

  // TABLE STYLES
  tableHeaderRow: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FEF7E6',
    borderBottomWidth: 1,
    borderBottomColor: '#F3E8D0',
    paddingVertical: 7,
    paddingHorizontal: 10,
  },
  tableHeaderCell: {
    paddingHorizontal: 6,
    justifyContent: 'center',
  },
  tableHeaderText: {
    fontSize: 11,
    fontWeight: '700',
    color: '#0F172A',
    textTransform: 'uppercase',
  },
  tableBodyRow: {
    flexDirection: 'row',
    borderBottomWidth: 1,
    borderBottomColor: '#F1F5F9',
    paddingVertical: 7,
    paddingHorizontal: 10,
    alignItems: 'center',
  },
  tableRowEven: {
    backgroundColor: '#FFFFFF',
  },
  tableRowOdd: {
    backgroundColor: '#FCFBF9',
  },
  tableBodyCell: {
    paddingHorizontal: 6,
    justifyContent: 'center',
  },
  tableCellIndexText: {
    fontSize: 11,
    color: '#94A3B8',
    textAlign: 'center',
  },
  tableCellText: {
    fontSize: 12,
    color: '#334155',
  },

  // PAGINATION
  paginationRow: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingHorizontal: 12,
    paddingVertical: 8,
    backgroundColor: '#FAF8F5',
    borderTopWidth: 1,
    borderTopColor: '#E2E8F0',
  },
  paginationInfoText: {
    fontSize: 11.5,
    color: '#64748B',
  },
  paginationButtons: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 6,
  },
  pageBtn: {
    width: 28,
    height: 28,
    borderRadius: 6,
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#CBD5E1',
    alignItems: 'center',
    justifyContent: 'center',
  },
  pageBtnDisabled: {
    opacity: 0.4,
  },
  pageNumberText: {
    fontSize: 12,
    fontWeight: '600',
    color: '#334155',
  },

  // LOV MODAL STYLES
  modalOverlay: {
    flex: 1,
    backgroundColor: 'rgba(15, 23, 42, 0.45)',
    justifyContent: 'center',
    alignItems: 'center',
    padding: 16,
  },
  lovModalCard: {
    width: '100%',
    maxWidth: 420,
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    overflow: 'hidden',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.15,
    shadowRadius: 10,
    elevation: 8,
  },
  lovModalHeader: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingHorizontal: 14,
    paddingVertical: 10,
    backgroundColor: '#FEF7E6',
    borderBottomWidth: 1,
    borderBottomColor: '#F3E8D0',
  },
  lovModalTitle: {
    fontSize: 13,
    fontWeight: '700',
    color: '#0F172A',
  },
  lovSearchContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 12,
    paddingVertical: 6,
    borderBottomWidth: 1,
    borderBottomColor: '#E2E8F0',
    backgroundColor: '#FFFFFF',
  },
  lovSearchInput: {
    flex: 1,
    height: 32,
    fontSize: 12.5,
    color: '#0F172A',
  },
  lovScrollView: {
    maxHeight: 300,
  },
  lovItemRow: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingHorizontal: 14,
    paddingVertical: 9,
    borderBottomWidth: 1,
    borderBottomColor: '#F1F5F9',
  },
  lovItemPrimaryText: {
    fontSize: 12.5,
    fontWeight: '600',
    color: '#0F172A',
  },
  lovItemSecondaryText: {
    fontSize: 11,
    color: '#64748B',
    marginTop: 2,
  },
  lovModalFooter: {
    padding: 8,
    borderTopWidth: 1,
    borderTopColor: '#E2E8F0',
    backgroundColor: '#F8FAFC',
    alignItems: 'flex-end',
  },
  lovModalCloseBtn: {
    paddingHorizontal: 14,
    paddingVertical: 6,
    borderRadius: 6,
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#CBD5E1',
  },
  lovModalCloseBtnText: {
    fontSize: 12,
    fontWeight: '600',
    color: '#334155',
  },

  // CALENDAR MODAL
  calendarModalCard: {
    width: '100%',
    maxWidth: 300,
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    padding: 12,
    borderWidth: 1,
    borderColor: '#E2E8F0',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.15,
    shadowRadius: 10,
    elevation: 8,
  },
  calendarHeaderRow: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    marginBottom: 10,
  },
  monthNavBtn: {
    padding: 5,
    borderRadius: 6,
    backgroundColor: '#F1F5F9',
  },
  monthYearTitle: {
    fontSize: 13,
    fontWeight: '700',
    color: '#0F172A',
  },
  weekDaysRow: {
    flexDirection: 'row',
    marginBottom: 6,
    borderBottomWidth: 1,
    borderBottomColor: '#E2E8F0',
    paddingBottom: 4,
  },
  weekDayText: {
    width: `${100 / 7}%`,
    textAlign: 'center',
    fontSize: 11,
    fontWeight: '700',
    color: '#64748B',
  },
  calendarGrid: {
    flexDirection: 'row',
    flexWrap: 'wrap',
  },
  calendarDayCell: {
    width: `${100 / 7}%`,
    height: 30,
    alignItems: 'center',
    justifyContent: 'center',
    borderRadius: 6,
    marginVertical: 1,
  },
  calendarDayCellActive: {
    backgroundColor: '#D97706',
  },
  calendarDayText: {
    fontSize: 11.5,
    color: '#0F172A',
  },
  calendarDayTextActive: {
    color: '#FFFFFF',
    fontWeight: '700',
  },
  datePresetsRow: {
    marginTop: 10,
    paddingTop: 8,
    borderTopWidth: 1,
    borderTopColor: '#E2E8F0',
    alignItems: 'flex-end',
  },
  presetBadge: {
    paddingHorizontal: 10,
    paddingVertical: 4,
    borderRadius: 6,
    backgroundColor: '#FEF7E6',
    borderWidth: 1,
    borderColor: '#E6DBBE',
  },
  presetBadgeText: {
    fontSize: 11,
    fontWeight: '600',
    color: '#9A7B38',
  },
});
