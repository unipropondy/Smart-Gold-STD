import { formatColumnHeader } from '@/utils/formatColumnHeader';
import { API_BASE_URL } from '@/constants/api';
import { downloadExcelReport, downloadPdfReport, printHtmlReport } from '@/utils/reportExportClient';
import { ReportDropdown } from '@/components/ReportDropdown';
import { ReportDatePicker } from '@/components/ReportDatePicker';
import { validateReportDateRange, getTodayIso, toDispDate } from '@/utils/dateValidation';
import { Ionicons } from '@expo/vector-icons';
import { Stack, useRouter } from 'expo-router';
import React, { useEffect, useState, useMemo } from 'react';
import {
  ActivityIndicator,
  FlatList,
  Modal,
  Platform,
  ScrollView,
  StyleSheet,
  Text,
  TextInput,
  TouchableOpacity,
  TouchableWithoutFeedback,
  View,
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';
import DateTimePicker from '@react-native-community/datetimepicker';

// Royal Gold Design Palette (matching Customer & Purchase Reports)
const GOLD_ACCENT = '#B48218';
const GOLD_DARK = '#9E6D08';
const GOLD_BORDER = '#E8DCB8';
const CARD_BG = '#FFFFFF';
const APP_BG = '#FBF9F5';
const TEXT_DARK = '#3B2D17';

interface ReportOption {
  id: string;
  label: string;
  supportsDetail?: boolean;
}

const REPORT_OPTIONS: ReportOption[] = [
  { id: 'sales_journal', label: 'Sales Journal', supportsDetail: true },
  { id: 'sales_by_weight_pcs', label: 'Sales by Weight and PCS' },
  { id: 'trade_in_sales', label: 'Trade In Sales' },
  { id: 'trade_in_only', label: 'Trade In Only' },
  { id: 'trade_in_sold', label: 'Trade In Sold' },
  { id: 'installment_entry', label: 'Installment Entry' },
  { id: 'installment_received', label: 'Installment Received' },
  { id: 'pending_installment', label: 'Pending Installment' },
  { id: 'sales_return', label: 'Sales Return' },
  { id: 'bill_cancel', label: 'Bill Cancel' },
  { id: 'daily_sales', label: 'Daily Sales' },
  { id: 'sales_by_supplier', label: 'Sales By Supplier' },
  { id: 'sales_by_customer', label: 'Sales By Customer' },
  { id: 'pending_order', label: 'Pending Order' },
  { id: 'sales_journal_by_category', label: 'Sales Journal By Category' },
  { id: 'sales_journal_by_catalogue', label: 'Sales Journal By Catalogue' },
  { id: 'paymode_wise_sales', label: 'Paymode Wise Sales' },
  { id: 'salesman_wise_journal', label: 'Salesman Wise Journal' },
  { id: 'salesman_analysis_smc', label: 'Salesman Analysis (SMC)' },
  { id: 'sales_journal_market_rate', label: 'Sales Journal (Market Rate)', supportsDetail: true },
  { id: 'below_cost_sales', label: 'Below Cost Sales' },
  { id: 'tray_wise_sales', label: 'Tray Wise Sales' },
  { id: 'pay_out', label: 'Pay Out' },
  { id: 'gst_report', label: 'GST Report' },
  { id: 'flexi_payments', label: 'Flexi Payments' },
  { id: 'deposit_journal', label: 'Deposit Journal' },
  { id: 'etrs_statement', label: 'eTRS Statement' },
  { id: 'misc_journal', label: 'Misc Journal' },
];

type ActionOption = 'Preview' | 'Print' | 'PDF' | 'Excel';
type LovType = 'catalogue' | 'size';

const RadioButton = ({
  label,
  selected,
  onPress,
}: {
  label: string;
  selected: boolean;
  onPress: () => void;
}) => {
  return (
    <TouchableOpacity
      style={[styles.radioBtn, selected && styles.radioBtnSelected]}
      onPress={onPress}
      activeOpacity={0.7}
    >
      <View style={[styles.radioCircle, selected && styles.radioCircleSelected]}>
        {selected && <View style={styles.radioDot} />}
      </View>
      <Text style={[styles.radioLabel, selected && styles.radioLabelSelected]}>
        {label}
      </Text>
    </TouchableOpacity>
  );
};

const formatDateLocal = (d: any) => {
  if (!d || !(d instanceof Date) || isNaN(d.getTime())) return '';
  const year = d.getFullYear();
  const month = String(d.getMonth() + 1).padStart(2, '0');
  const day = String(d.getDate()).padStart(2, '0');
  return `${year}-${month}-${day}`;
};

const parseLocalDate = (dateStr: string) => {
  if (!dateStr) return new Date();
  const parts = dateStr.split('-');
  if (parts.length !== 3) return new Date();
  const [y, m, d] = parts;
  return new Date(parseInt(y, 10), parseInt(m, 10) - 1, parseInt(d, 10));
};

export default function SalesReportsScreen() {
  const router = useRouter();

  const [toastMessage, setToastMessage] = useState('');
  const [toastType, setToastType] = useState<'success' | 'error'>('success');

  // Row 1: Selected Report & Dates
  const [selectedReport, setSelectedReport] = useState<string>('sales_journal');
  const [isReportDropdownOpen, setIsReportDropdownOpen] = useState(false);
  const [fromDate, setFromDate] = useState(formatDateLocal(new Date()));
  const [toDate, setToDate] = useState(formatDateLocal(new Date()));
  const [isLoading, setIsLoading] = useState(false);

  // Row 2: Summary / Detail & eTRS Status
  const [reportType, setReportType] = useState<'Summary' | 'Detail'>('Summary');
  // Strictly only 3 options: 'eTRS Ticket Issued' | 'eTRS Eligible' | 'Not for eTRS'
  const [etrsStatus, setEtrsStatus] = useState<'eTRS Ticket Issued' | 'eTRS Eligible' | 'Not for eTRS' | ''>('');

  // Row 3: Sales Journal Options | Sales by Weight and PCS | Checkboxes
  const [gstOption, setGstOption] = useState<'All' | 'With GST' | 'Zero Rated'>('All');
  const [weightPcsChoice, setWeightPcsChoice] = useState<'2' | '4' | '6' | '8'>('2');
  const [akshayaTritiya, setAkshayaTritiya] = useState<boolean>(false);
  const [gstFilter, setGstFilter] = useState<boolean>(false);
  const [voidFilter, setVoidFilter] = useState<boolean>(false);

  // Row 4: Dropdowns - Location Code | Supplier Code | Salesman
  const [locationCode, setLocationCode] = useState<string>('ALL');
  const [supplierCode, setSupplierCode] = useState<string>('ALL');
  const [salesmanCode, setSalesmanCode] = useState<string>('ALL');
  const [isLocOpen, setIsLocOpen] = useState(false);
  const [isSupOpen, setIsSupOpen] = useState(false);
  const [isSmOpen, setIsSmOpen] = useState(false);

  // Row 5: Dropdowns - Class | Category | Catalogue text input with '...'
  const [classCode, setClassCode] = useState<string>('ALL');
  const [categoryCode, setCategoryCode] = useState<string>('ALL');
  const [catalogueCode, setCatalogueCode] = useState<string>('');
  const [isClassOpen, setIsClassOpen] = useState(false);
  const [isCatOpen, setIsCatOpen] = useState(false);

  // Row 6: Size text input with '...' | Job Sheet No text input
  const [sizeCode, setSizeCode] = useState<string>('');
  const [jobSheetNo, setJobSheetNo] = useState<string>('');

  // Bottom Row: Actions
  const [actionOption, setActionOption] = useState<ActionOption>('Preview');

  // Filter option lists
  const [locations, setLocations] = useState<{ id: string; label: string }[]>([{ id: 'ALL', label: 'ALL - All Locations' }]);
  const [suppliers, setSuppliers] = useState<{ id: string; label: string }[]>([{ id: 'ALL', label: 'ALL - All Suppliers' }]);
  const [salesmen, setSalesmen] = useState<{ id: string; label: string }[]>([{ id: 'ALL', label: 'ALL - All Salesmen' }]);
  const [classes, setClasses] = useState<{ id: string; label: string }[]>([{ id: 'ALL', label: 'ALL - All Classes' }]);
  const [categories, setCategories] = useState<{ id: string; label: string }[]>([{ id: 'ALL', label: 'ALL - All Categories' }]);

  // LOV Lists for Catalogue and Size
  const [cataloguesList, setCataloguesList] = useState<{ code: string; name: string }[]>([]);
  const [sizesList, setSizesList] = useState<{ code: string; name: string }[]>([]);
  const [isLovModalVisible, setIsLovModalVisible] = useState(false);
  const [lovType, setLovType] = useState<LovType>('catalogue');
  const [lovSearchQuery, setLovSearchQuery] = useState('');

  const [showDatePicker, setShowDatePicker] = useState<{ show: boolean; mode: 'from' | 'to' }>({
    show: false,
    mode: 'from',
  });

  const closeAllDropdowns = () => {
    setIsReportDropdownOpen(false);
    setIsLocOpen(false);
    setIsSupOpen(false);
    setIsSmOpen(false);
    setIsClassOpen(false);
    setIsCatOpen(false);
  };

  useEffect(() => {
    (async () => {
      try {
        const [locRes, supRes, smRes, clsRes, catRes, catlgRes, szRes] = await Promise.all([
          fetch(`${API_BASE_URL}/salesreports/locations`).then(r => r.json()).catch(() => ({ data: [] })),
          fetch(`${API_BASE_URL}/salesreports/suppliers`).then(r => r.json()).catch(() => ({ data: [] })),
          fetch(`${API_BASE_URL}/salesreports/salesmen`).then(r => r.json()).catch(() => ({ data: [] })),
          fetch(`${API_BASE_URL}/salesreports/classes`).then(r => r.json()).catch(() => ({ data: [] })),
          fetch(`${API_BASE_URL}/salesreports/categories`).then(r => r.json()).catch(() => ({ data: [] })),
          fetch(`${API_BASE_URL}/purchasereports/catalogues`).then(r => r.json()).catch(() => ({ data: [] })),
          fetch(`${API_BASE_URL}/purchasereports/sizes`).then(r => r.json()).catch(() => ({ data: [] })),
        ]);

        if (locRes?.data) setLocations(locRes.data.map((x: any) => ({ id: x.LocationCode, label: x.LocationName || x.LocationCode })));
        if (supRes?.data) setSuppliers(supRes.data.map((x: any) => ({ id: x.SupplierCode, label: x.SupplierName || x.SupplierCode })));
        if (smRes?.data) setSalesmen(smRes.data.map((x: any) => ({ id: x.SalesManCode, label: x.SalesManName || x.SalesManCode })));
        if (clsRes?.data) setClasses(clsRes.data.map((x: any) => ({ id: x.ClassCode, label: x.ClassName || x.ClassCode })));
        if (catRes?.data) setCategories(catRes.data.map((x: any) => ({ id: x.CategoryCode, label: x.CategoryName || x.CategoryCode })));

        if (catlgRes?.data) {
          const list = catlgRes.data.filter((x: any) => x.CatalogueCode !== 'ALL').map((x: any) => ({
            code: x.CatalogueCode,
            name: x.CatalogueName || x.CatalogueCode,
          }));
          setCataloguesList(list);
        }
        if (szRes?.data) {
          const list = szRes.data.filter((x: any) => x.SizeCode !== 'ALL').map((x: any) => ({
            code: x.SizeCode,
            name: x.SizeType || x.SizeCode,
          }));
          setSizesList(list);
        }
      } catch (e) {
        console.warn('Failed loading sales filters:', e);
      }
    })();
  }, []);

  const showSuccess = (msg: string) => {
    setToastType('success');
    setToastMessage(msg);
    setTimeout(() => setToastMessage(''), 3000);
  };

  const showError = (msg: string) => {
    setToastType('error');
    setToastMessage(msg);
    setTimeout(() => setToastMessage(''), 3000);
  };

  const handleDateChange = (event: any, selectedDate?: Date) => {
    const mode = showDatePicker.mode;
    setShowDatePicker({ show: false, mode });
    if (selectedDate) {
      if (mode === 'from') {
        setFromDate(formatDateLocal(selectedDate));
      } else {
        setToDate(formatDateLocal(selectedDate));
      }
    }
  };

  const selectedReportObj = useMemo(() => {
    return REPORT_OPTIONS.find((r) => r.id === selectedReport);
  }, [selectedReport]);

  const openLovModal = (type: LovType) => {
    setLovType(type);
    setLovSearchQuery('');
    setIsLovModalVisible(true);
  };

  const lovModalConfig = useMemo(() => {
    switch (lovType) {
      case 'catalogue':
        return {
          title: 'Select Catalogue',
          codeLabel: 'Catalogue Code',
          nameLabel: 'Catalogue Name',
          data: cataloguesList,
        };
      case 'size':
        return {
          title: 'Select Size',
          codeLabel: 'Size Code',
          nameLabel: 'Size Type',
          data: sizesList,
        };
    }
  }, [lovType, cataloguesList, sizesList]);

  const filteredLovData = useMemo(() => {
    if (!lovModalConfig?.data) return [];
    if (!lovSearchQuery.trim()) return lovModalConfig.data;
    const q = lovSearchQuery.toLowerCase().trim();
    return lovModalConfig.data.filter(
      (item) => item.code.toLowerCase().includes(q) || item.name.toLowerCase().includes(q)
    );
  }, [lovModalConfig, lovSearchQuery]);

  const handleSelectLovItem = (code: string) => {
    if (lovType === 'catalogue') setCatalogueCode(code);
    else if (lovType === 'size') setSizeCode(code);
    setIsLovModalVisible(false);
  };

  const handleGetReport = async (overrideAction?: ActionOption) => {
    const currentAction = overrideAction || actionOption;

    if (!selectedReport) {
      showError('Please select a Sales Report.');
      return;
    }

    if (!fromDate || !toDate) {
      showError('Please select valid From Date and To Date.');
      return;
    }

    const valRes = validateReportDateRange(fromDate, toDate);
    if (!valRes.isValid) {
      showError(valRes.errorMessage || 'Invalid date range.');
      return;
    }

    setIsLoading(true);

    try {
      const res = await fetch(`${API_BASE_URL}/salesreports/generate`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          reportId: selectedReport,
          reportType: selectedReportObj?.supportsDetail ? reportType : 'Summary',
          filters: {
            fromDate,
            toDate,
            locationCode,
            supplierCode,
            salesmanCode,
            classCode,
            categoryCode,
            catalogueCode: catalogueCode ? catalogueCode.trim() : undefined,
            sizeCode: sizeCode ? sizeCode.trim() : undefined,
            gstOption,
            weightPcsChoice,
            akshayaTritiya,
            gstFilter,
            voidFilter,
            etrsStatus: etrsStatus || 'All',
            jobSheetNo: jobSheetNo ? jobSheetNo.trim() : undefined,
          },
        }),
      });

      const json = await res.json();

      if (!json.success || !json.data || json.data.length === 0) {
        showError(json.message || 'No records found for the selected criteria.');
        setIsLoading(false);
        return;
      }

      const repTitle = json.title || (selectedReportObj ? selectedReportObj.label : 'Sales Report');
      const rawCols = json.columns || Object.keys(json.data[0]).map((k) => ({ key: k, label: k }));
      const columns = rawCols.map((c: any) => ({ ...c, label: formatColumnHeader(c.label || c.key) }));

      const repTotals = json.summary || {};

      if (currentAction === 'Preview') {
        await printHtmlReport(
          {
            endpoint: 'salesreports',
            title: repTitle,
            columns,
            data: json.data,
            totals: repTotals,
            fromDate,
            toDate,
          },
          false
        );
        showSuccess('Report preview opened.');
      } else if (currentAction === 'Print') {
        await printHtmlReport(
          {
            endpoint: 'salesreports',
            title: repTitle,
            columns,
            data: json.data,
            totals: repTotals,
            fromDate,
            toDate,
          },
          true
        );
        showSuccess('Print dialog opened.');
      } else if (currentAction === 'PDF') {
        const ok = await downloadPdfReport({
          endpoint: 'salesreports',
          title: repTitle,
          columns,
          data: json.data,
          totals: repTotals,
          fromDate,
          toDate,
        });
        if (ok) showSuccess('PDF downloaded successfully!');
      } else if (currentAction === 'Excel') {
        const ok = await downloadExcelReport({
          endpoint: 'salesreports',
          title: repTitle,
          columns,
          data: json.data,
          totals: repTotals,
          fromDate,
          toDate,
        });
        if (ok) showSuccess('Excel file downloaded successfully!');
      }
    } catch (err: any) {
      console.error(err);
      showError('Failed to generate report: ' + (err.message || 'Error'));
    } finally {
      setIsLoading(false);
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
          if (isLovModalVisible) {
            setIsLovModalVisible(false);
          } else if (isReportDropdownOpen || isLocOpen || isSupOpen || isSmOpen || isClassOpen || isCatOpen) {
            closeAllDropdowns();
          } else {
            router.back();
          }
        }
      };
      window.addEventListener('keydown', handleKeyDown);
      return () => window.removeEventListener('keydown', handleKeyDown);
    }
  }, [selectedReport, fromDate, toDate, actionOption, isReportDropdownOpen, isLocOpen, isSupOpen, isSmOpen, isClassOpen, isCatOpen, reportType, isLovModalVisible]);

  return (
    <SafeAreaView style={styles.container} edges={['top']}>
      <Stack.Screen options={{ headerShown: false }} />

      {/* HEADER */}
      <View style={styles.header}>
        <View style={styles.headerLeft}>
          <TouchableOpacity onPress={() => router.back()} style={styles.backButton} activeOpacity={0.7}>
            <Ionicons name="arrow-back" size={20} color={TEXT_DARK} />
          </TouchableOpacity>
          <Text style={styles.title}>Sales Reports</Text>
        </View>

        <TouchableOpacity
          style={styles.actionButton}
          onPress={() => handleGetReport()}
          disabled={isLoading}
          activeOpacity={0.8}
        >
          {isLoading ? (
            <ActivityIndicator size="small" color={GOLD_DARK} style={{ marginRight: 5 }} />
          ) : (
            <Ionicons name="document-text-outline" size={15} color={GOLD_DARK} style={{ marginRight: 5 }} />
          )}
          <Text style={styles.actionButtonText}>Get Report</Text>
        </TouchableOpacity>
      </View>

      <ScrollView contentContainerStyle={styles.scrollContent}>
        <View style={styles.cardWrapper}>
          <View style={styles.goldCard}>
            {/* ROW 1: Sales Report | From Date | To Date */}
            <View style={[styles.filterGridRow, { zIndex: 100 }]}>
              {/* Col 1: Sales Report */}
              <View style={styles.gridCol}>
                <ReportDropdown
                  label="Sales Report"
                  options={REPORT_OPTIONS}
                  selectedId={selectedReport}
                  onSelect={(id) => {
                    setSelectedReport(id);
                    setIsReportDropdownOpen(false);
                  }}
                  isOpen={isReportDropdownOpen}
                  onToggle={() => {
                    closeAllDropdowns();
                    setIsReportDropdownOpen(!isReportDropdownOpen);
                  }}
                  maxHeight={280}
                />
              </View>

              {/* Col 2: From Date */}
              <View style={styles.gridCol}>
                <ReportDatePicker
                  label="From Date"
                  value={fromDate}
                  onChange={setFromDate}
                  isIsoOutput={true}
                  onOpenNative={() => setShowDatePicker({ show: true, mode: 'from' })}
                />
              </View>

              {/* Col 3: To Date */}
              <View style={styles.gridCol}>
                <ReportDatePicker
                  label="To Date"
                  value={toDate}
                  onChange={setToDate}
                  isIsoOutput={true}
                  onOpenNative={() => setShowDatePicker({ show: true, mode: 'to' })}
                />
              </View>
            </View>

            {/* ROW 2: Summary / Detail | Vertical Divider | eTRS Filter Section (Strictly 3 options: eTRS Ticket Issued, eTRS Eligible, Not for eTRS) */}
            <View style={styles.subFilterRow}>
              {/* Left: Summary / Detail Radio Buttons */}
              <View style={styles.reportTypeRow}>
                <RadioButton
                  label="Summary"
                  selected={reportType === 'Summary'}
                  onPress={() => setReportType('Summary')}
                />
                <RadioButton
                  label="Detail"
                  selected={reportType === 'Detail'}
                  onPress={() => setReportType('Detail')}
                />
              </View>

              {/* Vertical Divider */}
              <View style={styles.verticalDivider} />

              {/* Right: eTRS Radio Buttons (strictly 3 options, evenly spaced) */}
              <View style={styles.etrsRadioGroup}>
                <RadioButton
                  label="eTRS Ticket Issued"
                  selected={etrsStatus === 'eTRS Ticket Issued'}
                  onPress={() => setEtrsStatus(etrsStatus === 'eTRS Ticket Issued' ? '' : 'eTRS Ticket Issued')}
                />
                <RadioButton
                  label="eTRS Eligible"
                  selected={etrsStatus === 'eTRS Eligible'}
                  onPress={() => setEtrsStatus(etrsStatus === 'eTRS Eligible' ? '' : 'eTRS Eligible')}
                />
                <RadioButton
                  label="Not for eTRS"
                  selected={etrsStatus === 'Not for eTRS'}
                  onPress={() => setEtrsStatus(etrsStatus === 'Not for eTRS' ? '' : 'Not for eTRS')}
                />
              </View>
            </View>

            {/* ROW 3: Sales Journal (All, With GST, Zero Rated) | Divider | Sales by Weight and PCS (2, 4, 6, 8) | Divider | Checkboxes (Akshaya, GST, Void) */}
            <View style={styles.inlineFilterSectionRow}>
              {/* Left: Sales Journal Radio Group */}
              <View style={styles.inlineFilterGroup}>
                <Text style={styles.inlineGroupLabel}>Sales Journal</Text>
                <View style={styles.inlineRadioList}>
                  <RadioButton
                    label="All"
                    selected={gstOption === 'All'}
                    onPress={() => setGstOption('All')}
                  />
                  <RadioButton
                    label="With GST"
                    selected={gstOption === 'With GST'}
                    onPress={() => setGstOption('With GST')}
                  />
                  <RadioButton
                    label="Zero Rated"
                    selected={gstOption === 'Zero Rated'}
                    onPress={() => setGstOption('Zero Rated')}
                  />
                </View>
              </View>

              {/* Vertical Divider */}
              <View style={styles.verticalDivider} />

              {/* Middle: Sales by Weight and PCS */}
              <View style={styles.inlineFilterGroup}>
                <Text style={styles.inlineGroupLabel}>Sales by Weight and PCS</Text>
                <View style={styles.inlineRadioList}>
                  {(['2', '4', '6', '8'] as const).map((choice) => (
                    <RadioButton
                      key={choice}
                      label={choice}
                      selected={weightPcsChoice === choice}
                      onPress={() => setWeightPcsChoice(choice)}
                    />
                  ))}
                </View>
              </View>

              {/* Vertical Divider */}
              <View style={styles.verticalDivider} />

              {/* Right: Checkboxes: Akshaya, GST, Void */}
              <View style={styles.statusCheckboxGroup}>
                <TouchableOpacity
                  style={styles.checkboxItem}
                  onPress={() => setAkshayaTritiya(!akshayaTritiya)}
                  activeOpacity={0.7}
                >
                  <View style={[styles.checkboxBox, akshayaTritiya && styles.checkboxBoxChecked]}>
                    {akshayaTritiya && <Ionicons name="checkmark" size={13} color="#FFFFFF" />}
                  </View>
                  <Text style={styles.checkboxLabel}>Akshaya</Text>
                </TouchableOpacity>

                <TouchableOpacity
                  style={styles.checkboxItem}
                  onPress={() => setGstFilter(!gstFilter)}
                  activeOpacity={0.7}
                >
                  <View style={[styles.checkboxBox, gstFilter && styles.checkboxBoxChecked]}>
                    {gstFilter && <Ionicons name="checkmark" size={13} color="#FFFFFF" />}
                  </View>
                  <Text style={styles.checkboxLabel}>GST</Text>
                </TouchableOpacity>

                <TouchableOpacity
                  style={styles.checkboxItem}
                  onPress={() => setVoidFilter(!voidFilter)}
                  activeOpacity={0.7}
                >
                  <View style={[styles.checkboxBox, voidFilter && styles.checkboxBoxChecked]}>
                    {voidFilter && <Ionicons name="checkmark" size={13} color="#FFFFFF" />}
                  </View>
                  <Text style={styles.checkboxLabel}>Void</Text>
                </TouchableOpacity>
              </View>
            </View>

            {/* ROW 4 (Filters Grid Row 1): Location Code | Supplier Code | Salesman */}
            <View style={[styles.filterGridRow, { zIndex: 90 }]}>
              {/* Col 1: Location Code */}
              <View style={styles.gridCol}>
                <ReportDropdown
                  label="Location Code"
                  options={locations}
                  selectedId={locationCode}
                  onSelect={(id) => {
                    setLocationCode(id);
                    setIsLocOpen(false);
                  }}
                  isOpen={isLocOpen}
                  onToggle={() => {
                    closeAllDropdowns();
                    setIsLocOpen(!isLocOpen);
                  }}
                  maxHeight={200}
                />
              </View>

              {/* Col 2: Supplier Code */}
              <View style={styles.gridCol}>
                <ReportDropdown
                  label="Supplier Code"
                  options={suppliers}
                  selectedId={supplierCode}
                  onSelect={(id) => {
                    setSupplierCode(id);
                    setIsSupOpen(false);
                  }}
                  isOpen={isSupOpen}
                  onToggle={() => {
                    closeAllDropdowns();
                    setIsSupOpen(!isSupOpen);
                  }}
                  maxHeight={200}
                />
              </View>

              {/* Col 3: Salesman */}
              <View style={styles.gridCol}>
                <ReportDropdown
                  label="Salesman"
                  options={salesmen}
                  selectedId={salesmanCode}
                  onSelect={(id) => {
                    setSalesmanCode(id);
                    setIsSmOpen(false);
                  }}
                  isOpen={isSmOpen}
                  onToggle={() => {
                    closeAllDropdowns();
                    setIsSmOpen(!isSmOpen);
                  }}
                  maxHeight={200}
                />
              </View>
            </View>

            {/* ROW 5 (Filters Grid Row 2): Class | Category | Catalogue [...] */}
            <View style={[styles.filterGridRow, { zIndex: 80 }]}>
              {/* Col 1: Class */}
              <View style={styles.gridCol}>
                <ReportDropdown
                  label="Class"
                  options={classes}
                  selectedId={classCode}
                  onSelect={(id) => {
                    setClassCode(id);
                    setIsClassOpen(false);
                  }}
                  isOpen={isClassOpen}
                  onToggle={() => {
                    closeAllDropdowns();
                    setIsClassOpen(!isClassOpen);
                  }}
                  maxHeight={200}
                />
              </View>

              {/* Col 2: Category */}
              <View style={styles.gridCol}>
                <ReportDropdown
                  label="Category"
                  options={categories}
                  selectedId={categoryCode}
                  onSelect={(id) => {
                    setCategoryCode(id);
                    setIsCatOpen(false);
                  }}
                  isOpen={isCatOpen}
                  onToggle={() => {
                    closeAllDropdowns();
                    setIsCatOpen(!isCatOpen);
                  }}
                  maxHeight={200}
                />
              </View>

              {/* Col 3: Catalogue */}
              <View style={styles.gridCol}>
                <Text style={styles.fieldLabel}>Catalogue</Text>
                <View style={styles.lookupInputWrapper}>
                  <TextInput
                    style={styles.lookupTextInput}
                    value={catalogueCode}
                    onChangeText={setCatalogueCode}
                    placeholder=""
                  />
                  <TouchableOpacity
                    style={styles.lookupBtn}
                    onPress={() => openLovModal('catalogue')}
                    activeOpacity={0.8}
                  >
                    <Text style={styles.lookupBtnText}>...</Text>
                  </TouchableOpacity>
                </View>
              </View>
            </View>

            {/* ROW 6 (Filters Grid Row 3): Size [...] | Job Sheet No | Empty Col */}
            <View style={[styles.filterGridRow, { zIndex: 70 }]}>
              {/* Col 1: Size */}
              <View style={styles.gridCol}>
                <Text style={styles.fieldLabel}>Size</Text>
                <View style={styles.lookupInputWrapper}>
                  <TextInput
                    style={styles.lookupTextInput}
                    value={sizeCode}
                    onChangeText={setSizeCode}
                    placeholder=""
                  />
                  <TouchableOpacity
                    style={styles.lookupBtn}
                    onPress={() => openLovModal('size')}
                    activeOpacity={0.8}
                  >
                    <Text style={styles.lookupBtnText}>...</Text>
                  </TouchableOpacity>
                </View>
              </View>

              {/* Col 2: Job Sheet No */}
              <View style={styles.gridCol}>
                <Text style={styles.fieldLabel}>Job Sheet No</Text>
                <View style={styles.textInputWrapper}>
                  <TextInput
                    style={styles.simpleTextInput}
                    value={jobSheetNo}
                    onChangeText={setJobSheetNo}
                    placeholder=""
                  />
                </View>
              </View>

              {/* Col 3: Empty Placeholder to balance 3 columns */}
              <View style={styles.gridCol} />
            </View>

            {/* Divider */}
            <View style={styles.divider} />

            {/* ACTIONS ROW: Preview, Print, PDF, Excel */}
            <View style={styles.buttonRow}>
              <RadioButton
                label="Preview"
                selected={actionOption === 'Preview'}
                onPress={() => setActionOption('Preview')}
              />
              <RadioButton
                label="Print"
                selected={actionOption === 'Print'}
                onPress={() => setActionOption('Print')}
              />
              <RadioButton
                label="PDF"
                selected={actionOption === 'PDF'}
                onPress={() => setActionOption('PDF')}
              />
              <RadioButton
                label="Excel"
                selected={actionOption === 'Excel'}
                onPress={() => setActionOption('Excel')}
              />
            </View>
          </View>
        </View>

        {showDatePicker.show && (
          <DateTimePicker
            value={parseLocalDate(showDatePicker.mode === 'from' ? fromDate : toDate)}
            maximumDate={new Date()}
            mode="date"
            display="default"
            onChange={handleDateChange}
          />
        )}
      </ScrollView>

      {/* LOV Selection Modal for Catalogue and Size */}
      <Modal
        visible={isLovModalVisible}
        transparent={true}
        animationType="fade"
        onRequestClose={() => setIsLovModalVisible(false)}
      >
        <TouchableWithoutFeedback onPress={() => setIsLovModalVisible(false)}>
          <View style={styles.modalOverlay}>
            <TouchableWithoutFeedback onPress={(e) => e.stopPropagation()}>
              <View style={styles.modalContent}>
                {/* Modal Header */}
                <View style={styles.modalHeader}>
                  <Text style={styles.modalTitle}>{lovModalConfig?.title}</Text>
                  <TouchableOpacity
                    onPress={() => setIsLovModalVisible(false)}
                    style={styles.modalCloseBtn}
                  >
                    <Ionicons name="close" size={20} color={TEXT_DARK} />
                  </TouchableOpacity>
                </View>

                {/* Search Box */}
                <View style={styles.modalSearchBox}>
                  <Ionicons name="search" size={16} color="#8A7B66" style={{ marginRight: 8 }} />
                  <TextInput
                    style={styles.modalSearchInput}
                    placeholder="Search by code or description..."
                    value={lovSearchQuery}
                    onChangeText={setLovSearchQuery}
                    autoFocus={true}
                  />
                  {lovSearchQuery ? (
                    <TouchableOpacity onPress={() => setLovSearchQuery('')}>
                      <Ionicons name="close-circle" size={16} color="#8A7B66" />
                    </TouchableOpacity>
                  ) : null}
                </View>

                {/* Table Header */}
                <View style={styles.modalTableHeader}>
                  <Text style={[styles.modalColHeader, { flex: 1.2 }]}>{lovModalConfig?.codeLabel}</Text>
                  <Text style={[styles.modalColHeader, { flex: 2 }]}>{lovModalConfig?.nameLabel}</Text>
                </View>

                {/* Table List */}
                <FlatList
                  data={filteredLovData}
                  keyExtractor={(item) => item.code}
                  style={styles.modalList}
                  renderItem={({ item }) => (
                    <TouchableOpacity
                      style={styles.modalRow}
                      onPress={() => handleSelectLovItem(item.code)}
                      activeOpacity={0.7}
                    >
                      <Text style={[styles.modalCellCode, { flex: 1.2 }]} numberOfLines={1}>
                        {item.code}
                      </Text>
                      <Text style={[styles.modalCellName, { flex: 2 }]} numberOfLines={1}>
                        {item.name}
                      </Text>
                    </TouchableOpacity>
                  )}
                  ListEmptyComponent={
                    <View style={styles.modalEmpty}>
                      <Text style={styles.modalEmptyText}>No records found</Text>
                    </View>
                  }
                />
              </View>
            </TouchableWithoutFeedback>
          </View>
        </TouchableWithoutFeedback>
      </Modal>

      {/* Toast Notification */}
      {toastMessage ? (
        <View style={[styles.toast, toastType === 'success' ? styles.toastSuccess : styles.toastError]}>
          <Ionicons
            name={toastType === 'success' ? 'checkmark-circle' : 'alert-circle'}
            size={16}
            color="#FFFFFF"
            style={{ marginRight: 6 }}
          />
          <Text style={styles.toastText}>{toastMessage}</Text>
        </View>
      ) : null}
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: APP_BG,
  },
  header: {
    height: 48,
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    paddingHorizontal: 14,
    borderBottomWidth: 1,
    borderBottomColor: GOLD_BORDER,
    backgroundColor: CARD_BG,
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
  backButton: {
    padding: 6,
    marginRight: 8,
    borderRadius: 6,
    backgroundColor: '#F9F6F0',
    borderWidth: 1,
    borderColor: GOLD_BORDER,
  },
  title: {
    fontSize: 15,
    fontWeight: 'bold',
    color: TEXT_DARK,
    letterSpacing: 0.3,
    fontFamily: 'Cambria',
  },
  actionButton: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 12,
    paddingVertical: 5,
    borderRadius: 6,
    borderWidth: 1,
    borderColor: GOLD_ACCENT,
    backgroundColor: '#FAF7F0',
  },
  actionButtonText: {
    color: GOLD_DARK,
    fontWeight: 'bold',
    fontSize: 12,
    fontFamily: 'Cambria',
  },
  scrollContent: {
    paddingHorizontal: 16,
    paddingTop: 16,
    paddingBottom: 40,
  },
  cardWrapper: {
    width: '100%',
  },
  goldCard: {
    backgroundColor: CARD_BG,
    borderRadius: 8,
    borderWidth: 1,
    borderColor: GOLD_BORDER,
    padding: 18,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.04,
    shadowRadius: 3,
    elevation: 2,
  },
  filterGridRow: {
    flexDirection: Platform.OS === 'web' ? 'row' : 'column',
    gap: 16,
    alignItems: 'flex-start',
    marginBottom: 14,
  },
  gridCol: {
    flex: 1,
    width: Platform.OS === 'web' ? undefined : '100%',
  },
  fieldLabel: {
    fontSize: 12,
    fontWeight: 'bold',
    color: TEXT_DARK,
    marginBottom: 6,
    fontFamily: 'Cambria',
  },
  subFilterRow: {
    flexDirection: 'row',
    alignItems: 'center',
    flexWrap: 'wrap',
    gap: 16,
    marginTop: 2,
    marginBottom: 14,
  },
  reportTypeRow: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 12,
  },
  verticalDivider: {
    width: 1,
    height: 24,
    backgroundColor: '#E8DCB8',
    marginHorizontal: 8,
  },
  etrsRadioGroup: {
    flexDirection: 'row',
    alignItems: 'center',
    flexWrap: 'wrap',
    gap: 16,
  },
  inlineFilterSectionRow: {
    flexDirection: 'row',
    alignItems: 'center',
    flexWrap: 'wrap',
    gap: 16,
    marginBottom: 14,
  },
  inlineFilterGroup: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 12,
  },
  inlineGroupLabel: {
    fontSize: 12,
    fontWeight: 'bold',
    color: TEXT_DARK,
    fontFamily: 'Cambria',
  },
  inlineRadioList: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 10,
  },
  statusCheckboxGroup: {
    flexDirection: 'row',
    alignItems: 'center',
    flexWrap: 'wrap',
    gap: 16,
  },
  checkboxItem: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  checkboxBox: {
    width: 18,
    height: 18,
    borderRadius: 4,
    borderWidth: 1.5,
    borderColor: '#C5B597',
    backgroundColor: CARD_BG,
    alignItems: 'center',
    justifyContent: 'center',
    marginRight: 8,
  },
  checkboxBoxChecked: {
    backgroundColor: GOLD_DARK,
    borderColor: GOLD_DARK,
  },
  checkboxLabel: {
    fontSize: 12.5,
    fontWeight: 'bold',
    color: TEXT_DARK,
    fontFamily: 'Cambria',
  },
  textInputWrapper: {
    height: 36,
    borderWidth: 1,
    borderColor: '#D4C3A3',
    borderRadius: 6,
    backgroundColor: CARD_BG,
    overflow: 'hidden',
  },
  simpleTextInput: {
    flex: 1,
    height: '100%',
    paddingHorizontal: 10,
    fontSize: 12.5,
    color: TEXT_DARK,
    fontFamily: 'Cambria',
    outlineStyle: 'none',
  } as any,
  lookupInputWrapper: {
    flexDirection: 'row',
    alignItems: 'center',
    height: 36,
    borderWidth: 1,
    borderColor: '#D4C3A3',
    borderRadius: 6,
    backgroundColor: CARD_BG,
    overflow: 'hidden',
  },
  lookupTextInput: {
    flex: 1,
    height: '100%',
    paddingHorizontal: 10,
    fontSize: 12.5,
    color: TEXT_DARK,
    fontFamily: 'Cambria',
    outlineStyle: 'none',
  } as any,
  lookupBtn: {
    width: 36,
    height: '100%',
    backgroundColor: '#ECC975',
    borderLeftWidth: 1,
    borderLeftColor: '#D4A742',
    alignItems: 'center',
    justifyContent: 'center',
  },
  lookupBtnText: {
    fontSize: 14,
    fontWeight: 'bold',
    color: TEXT_DARK,
    marginTop: -4,
  },
  divider: {
    height: 1,
    backgroundColor: '#F0E5D0',
    marginTop: 6,
    marginBottom: 16,
  },
  buttonRow: {
    flexDirection: 'row',
    alignItems: 'center',
    flexWrap: 'wrap',
    gap: 12,
  },
  radioBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingVertical: 6,
    paddingHorizontal: 14,
    borderRadius: 6,
    borderWidth: 1,
    borderColor: '#E2D3B3',
    backgroundColor: '#FAF7F0',
  },
  radioBtnSelected: {
    backgroundColor: '#FFF4DA',
    borderColor: GOLD_ACCENT,
  },
  radioCircle: {
    height: 15,
    width: 15,
    borderRadius: 8,
    borderWidth: 1.5,
    borderColor: '#C5B597',
    alignItems: 'center',
    justifyContent: 'center',
    marginRight: 6,
  },
  radioCircleSelected: {
    borderColor: GOLD_DARK,
  },
  radioDot: {
    height: 7,
    width: 7,
    borderRadius: 4,
    backgroundColor: GOLD_DARK,
  },
  radioLabel: {
    fontSize: 12,
    fontWeight: '500',
    color: '#5C4A28',
    fontFamily: 'Cambria',
  },
  radioLabelSelected: {
    color: GOLD_DARK,
    fontWeight: 'bold',
    fontFamily: 'Cambria',
  },
  toast: {
    position: 'absolute',
    top: 54,
    alignSelf: 'center',
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 16,
    paddingVertical: 8,
    borderRadius: 6,
    elevation: 4,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.15,
    shadowRadius: 4,
    zIndex: 9999,
  },
  toastSuccess: {
    backgroundColor: '#2E7D32',
  },
  toastError: {
    backgroundColor: '#C62828',
  },
  toastText: {
    color: '#FFFFFF',
    fontSize: 12,
    fontWeight: '600',
    fontFamily: 'Cambria',
  },
  // Modal styles for LOV
  modalOverlay: {
    flex: 1,
    backgroundColor: 'rgba(0, 0, 0, 0.4)',
    justifyContent: 'center',
    alignItems: 'center',
    padding: 20,
  },
  modalContent: {
    width: '100%',
    maxWidth: 520,
    maxHeight: '80%',
    backgroundColor: CARD_BG,
    borderRadius: 10,
    borderWidth: 1,
    borderColor: GOLD_BORDER,
    overflow: 'hidden',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.15,
    shadowRadius: 10,
    elevation: 5,
  },
  modalHeader: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    paddingHorizontal: 16,
    paddingVertical: 12,
    backgroundColor: '#FAF7F0',
    borderBottomWidth: 1,
    borderBottomColor: GOLD_BORDER,
  },
  modalTitle: {
    fontSize: 14,
    fontWeight: 'bold',
    color: TEXT_DARK,
    fontFamily: 'Cambria',
  },
  modalCloseBtn: {
    padding: 4,
  },
  modalSearchBox: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 12,
    paddingVertical: 8,
    borderBottomWidth: 1,
    borderBottomColor: '#F0E5D0',
    backgroundColor: '#FFFFFF',
  },
  modalSearchInput: {
    flex: 1,
    fontSize: 12.5,
    color: TEXT_DARK,
    fontFamily: 'Cambria',
    outlineStyle: 'none',
  } as any,
  modalTableHeader: {
    flexDirection: 'row',
    backgroundColor: '#F5EFE6',
    paddingHorizontal: 16,
    paddingVertical: 8,
    borderBottomWidth: 1,
    borderBottomColor: '#E8DCB8',
  },
  modalColHeader: {
    fontSize: 11.5,
    fontWeight: 'bold',
    color: '#6E5528',
    fontFamily: 'Cambria',
  },
  modalList: {
    maxHeight: 320,
  },
  modalRow: {
    flexDirection: 'row',
    paddingHorizontal: 16,
    paddingVertical: 10,
    borderBottomWidth: 1,
    borderBottomColor: '#F5EFE6',
    alignItems: 'center',
  },
  modalCellCode: {
    fontSize: 12.5,
    fontWeight: 'bold',
    color: GOLD_DARK,
    fontFamily: 'Cambria',
  },
  modalCellName: {
    fontSize: 12.5,
    color: TEXT_DARK,
    fontFamily: 'Cambria',
  },
  modalEmpty: {
    padding: 24,
    alignItems: 'center',
    justifyContent: 'center',
  },
  modalEmptyText: {
    fontSize: 12,
    color: '#8A7B66',
    fontFamily: 'Cambria',
  },
});
