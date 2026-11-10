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

// Royal Gold Design Palette
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
  { id: 'purchase', label: 'Purchase', supportsDetail: true },
  { id: 'job_sheet', label: 'Job Sheet', supportsDetail: true },
  { id: 'purchase_fix', label: 'Purchase Fix', supportsDetail: true },
  { id: 'sales_order', label: 'Sales Order', supportsDetail: true },
  { id: 'sales_order_delete', label: 'Sales Order Delete' },
  { id: 'sales_order_pending', label: 'Sales Order Pending' },
  { id: 'purchase_return', label: 'Purchase Return', supportsDetail: true },
  { id: 'purchase_tally', label: 'Purchase Tally' },
  { id: 'supplier_payment', label: 'Supplier Payment' },
  { id: 'supplier_mc', label: 'Supplier MC' },
  { id: 'supplier_analysis', label: 'Supplier Analysis' },
  { id: 'trade_in_journal', label: 'Trade In Journal' },
  { id: 'trade_in_gold_issued', label: 'Trade In Gold Issued' },
  { id: 'vendor_weight_balance', label: 'Vendor Weight Balance' },
  { id: 'vendor_mc_balance', label: 'Vendor MC Balance' },
  { id: 'purchase_details', label: 'Purchase Details' },
];

type ActionOption = 'Preview' | 'Print' | 'PDF' | 'Excel';
type LovType = 'class' | 'category' | 'catalogue' | 'size';

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

export default function PurchaseReportsScreen() {
  const router = useRouter();

  const [toastMessage, setToastMessage] = useState('');
  const [toastType, setToastType] = useState<'success' | 'error'>('success');

  const [selectedReport, setSelectedReport] = useState<string>('purchase');
  const [isReportDropdownOpen, setIsReportDropdownOpen] = useState(false);
  const [reportType, setReportType] = useState<'Summary' | 'Detail'>('Summary');

  // Dates
  const [fromDate, setFromDate] = useState(formatDateLocal(new Date()));
  const [toDate, setToDate] = useState(formatDateLocal(new Date()));
  const [isLoading, setIsLoading] = useState(false);

  // Filter values
  const [supplierCode, setSupplierCode] = useState<string>('ALL');
  const [invoiceNo, setInvoiceNo] = useState<string>('');
  const [jobSheetNo, setJobSheetNo] = useState<string>('');

  // Status Filter Checkboxes (Pending, Closed, Unclosed)
  const [statusPending, setStatusPending] = useState<boolean>(false);
  const [statusClosed, setStatusClosed] = useState<boolean>(false);
  const [statusUnclosed, setStatusUnclosed] = useState<boolean>(false);

  // Type Of Fix Radio Button Group ('All' | 'Fix' | 'Un Fix')
  const [fixType, setFixType] = useState<'All' | 'Fix' | 'Un Fix'>('All');

  // Lookup filters
  const [classCode, setClassCode] = useState<string>('');
  const [categoryCode, setCategoryCode] = useState<string>('');
  const [catalogueCode, setCatalogueCode] = useState<string>('');
  const [sizeCode, setSizeCode] = useState<string>('');

  // Dropdown open states
  const [isSupOpen, setIsSupOpen] = useState(false);

  // Bottom action
  const [actionOption, setActionOption] = useState<ActionOption>('Preview');

  // Master data lists
  const [suppliers, setSuppliers] = useState<{ id: string; label: string }[]>([
    { id: 'ALL', label: 'ALL - All Suppliers' },
  ]);
  const [classesList, setClassesList] = useState<{ code: string; name: string }[]>([]);
  const [categoriesList, setCategoriesList] = useState<{ code: string; name: string }[]>([]);
  const [cataloguesList, setCataloguesList] = useState<{ code: string; name: string }[]>([]);
  const [sizesList, setSizesList] = useState<{ code: string; name: string }[]>([]);

  // Generic LOV Modal state
  const [isLovModalVisible, setIsLovModalVisible] = useState(false);
  const [lovType, setLovType] = useState<LovType>('class');
  const [lovSearchQuery, setLovSearchQuery] = useState('');

  const [showDatePicker, setShowDatePicker] = useState<{ show: boolean; mode: 'from' | 'to' }>({
    show: false,
    mode: 'from',
  });

  const closeAllDropdowns = () => {
    setIsReportDropdownOpen(false);
    setIsSupOpen(false);
  };

  useEffect(() => {
    (async () => {
      try {
        const [supRes, clsRes, catRes, catlgRes, szRes] = await Promise.all([
          fetch(`${API_BASE_URL}/purchasereports/suppliers`).then((r) => r.json()).catch(() => ({ data: [] })),
          fetch(`${API_BASE_URL}/purchasereports/classes`).then((r) => r.json()).catch(() => ({ data: [] })),
          fetch(`${API_BASE_URL}/purchasereports/categories`).then((r) => r.json()).catch(() => ({ data: [] })),
          fetch(`${API_BASE_URL}/purchasereports/catalogues`).then((r) => r.json()).catch(() => ({ data: [] })),
          fetch(`${API_BASE_URL}/purchasereports/sizes`).then((r) => r.json()).catch(() => ({ data: [] })),
        ]);

        if (supRes?.data) {
          setSuppliers(supRes.data.map((x: any) => ({
            id: x.SupplierCode,
            label: x.SupplierCode === 'ALL' ? 'ALL - All Suppliers' : `${x.SupplierCode} - ${x.SupplierName || x.SupplierCode}`,
          })));
        }
        if (clsRes?.data) {
          const list = clsRes.data.filter((x: any) => x.ClassCode !== 'ALL').map((x: any) => ({
            code: x.ClassCode,
            name: x.ClassName || x.ClassCode,
          }));
          setClassesList(list);
        }
        if (catRes?.data) {
          const list = catRes.data.filter((x: any) => x.CategoryCode !== 'ALL').map((x: any) => ({
            code: x.CategoryCode,
            name: x.CategoryName || x.CategoryCode,
          }));
          setCategoriesList(list);
        }
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
        console.warn('Failed loading purchase filters:', e);
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
      case 'class':
        return {
          title: 'Select Class',
          codeLabel: 'Class Code',
          nameLabel: 'Class Name',
          data: classesList,
        };
      case 'category':
        return {
          title: 'Select Category',
          codeLabel: 'Category Code',
          nameLabel: 'Category Name',
          data: categoriesList,
        };
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
          nameLabel: 'Size Type / Description',
          data: sizesList,
        };
    }
  }, [lovType, classesList, categoriesList, cataloguesList, sizesList]);

  const filteredLovData = useMemo(() => {
    const q = lovSearchQuery.trim().toLowerCase();
    if (!q) return lovModalConfig.data;
    return lovModalConfig.data.filter(
      (item) =>
        item.code.toLowerCase().includes(q) || (item.name && item.name.toLowerCase().includes(q))
    );
  }, [lovModalConfig, lovSearchQuery]);

  const handleSelectLovItem = (item: { code: string; name: string }) => {
    switch (lovType) {
      case 'class':
        setClassCode(item.code);
        break;
      case 'category':
        setCategoryCode(item.code);
        break;
      case 'catalogue':
        setCatalogueCode(item.code);
        break;
      case 'size':
        setSizeCode(item.code);
        break;
    }
    setIsLovModalVisible(false);
  };

  const handleGetReport = async (overrideAction?: ActionOption) => {
    const currentAction = overrideAction || actionOption;

    if (!selectedReport) {
      showError('Please select a Purchase Report.');
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
      const res = await fetch(`${API_BASE_URL}/purchasereports/generate`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          reportId: selectedReport,
          reportType: selectedReportObj?.supportsDetail ? reportType : 'Summary',
          filters: {
            fromDate,
            toDate,
            supplierCode: supplierCode || 'ALL',
            invoiceNo: invoiceNo.trim() || undefined,
            jobSheetNo: jobSheetNo.trim() || undefined,
            classCode: classCode.trim() || 'ALL',
            categoryCode: categoryCode.trim() || 'ALL',
            catalogueCode: catalogueCode.trim() || 'ALL',
            sizeCode: sizeCode.trim() || 'ALL',
            fixType,
            statusPending,
            statusClosed,
            statusUnclosed,
          },
        }),
      });

      const json = await res.json();

      if (!json.success || !json.data || json.data.length === 0) {
        showError(json.message || 'No records found for the selected criteria.');
        setIsLoading(false);
        return;
      }

      const repTitle = json.title || (selectedReportObj ? selectedReportObj.label : 'Purchase Report');
      const rawCols = json.columns || Object.keys(json.data[0]).map((k) => ({ key: k, label: k }));
      const columns = rawCols.map((c: any) => ({ ...c, label: formatColumnHeader(c.label || c.key) }));
      const repTotals = json.summary || {};

      if (currentAction === 'Preview') {
        await printHtmlReport(
          {
            endpoint: 'purchasereports',
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
            endpoint: 'purchasereports',
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
          endpoint: 'purchasereports',
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
          endpoint: 'purchasereports',
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
          } else if (isReportDropdownOpen || isSupOpen) {
            closeAllDropdowns();
          } else {
            router.back();
          }
        }
      };
      window.addEventListener('keydown', handleKeyDown);
      return () => window.removeEventListener('keydown', handleKeyDown);
    }
  }, [selectedReport, fromDate, toDate, actionOption, isReportDropdownOpen, isSupOpen, reportType, isLovModalVisible]);

  return (
    <SafeAreaView style={styles.container} edges={['top']}>
      <Stack.Screen options={{ headerShown: false }} />

      {/* HEADER */}
      <View style={styles.header}>
        <View style={styles.headerLeft}>
          <TouchableOpacity onPress={() => router.back()} style={styles.backButton} activeOpacity={0.7}>
            <Ionicons name="arrow-back" size={20} color={TEXT_DARK} />
          </TouchableOpacity>
          <Text style={styles.title}>Purchase Reports</Text>
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
            {/* ROW 1: Purchase Report | From Date | To Date */}
            <View style={[styles.filterGridRow, { zIndex: 100 }]}>
              {/* Col 1: Purchase Report */}
              <View style={styles.gridCol}>
                <ReportDropdown
                  label="Purchase Report"
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

            {/* ROW 2: Summary / Detail | Vertical Divider | Pending / Closed / Unclosed Checkboxes */}
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

              {/* Right: Status Checkboxes: Pending | Closed | Unclosed */}
              <View style={styles.statusCheckboxGroup}>
                <TouchableOpacity
                  style={styles.checkboxItem}
                  onPress={() => setStatusPending(!statusPending)}
                  activeOpacity={0.7}
                >
                  <View style={[styles.checkboxBox, statusPending && styles.checkboxBoxChecked]}>
                    {statusPending && <Ionicons name="checkmark" size={13} color="#FFFFFF" />}
                  </View>
                  <Text style={styles.checkboxLabel}>Pending</Text>
                </TouchableOpacity>

                <TouchableOpacity
                  style={styles.checkboxItem}
                  onPress={() => setStatusClosed(!statusClosed)}
                  activeOpacity={0.7}
                >
                  <View style={[styles.checkboxBox, statusClosed && styles.checkboxBoxChecked]}>
                    {statusClosed && <Ionicons name="checkmark" size={13} color="#FFFFFF" />}
                  </View>
                  <Text style={styles.checkboxLabel}>Closed</Text>
                </TouchableOpacity>

                <TouchableOpacity
                  style={styles.checkboxItem}
                  onPress={() => setStatusUnclosed(!statusUnclosed)}
                  activeOpacity={0.7}
                >
                  <View style={[styles.checkboxBox, statusUnclosed && styles.checkboxBoxChecked]}>
                    {statusUnclosed && <Ionicons name="checkmark" size={13} color="#FFFFFF" />}
                  </View>
                  <Text style={styles.checkboxLabel}>Unclosed</Text>
                </TouchableOpacity>
              </View>
            </View>

            {/* ROW 3 (Filters Grid Row 1): Supplier Code | Invoice No | Job Sheet No */}
            <View style={[styles.filterGridRow, { zIndex: 90 }]}>
              {/* Col 1: Supplier Code */}
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

              {/* Col 2: Invoice No */}
              <View style={styles.gridCol}>
                <Text style={styles.fieldLabel}>Invoice No</Text>
                <View style={styles.textInputWrapper}>
                  <TextInput
                    style={styles.simpleTextInput}
                    value={invoiceNo}
                    onChangeText={setInvoiceNo}
                    placeholder=""
                  />
                </View>
              </View>

              {/* Col 3: Job Sheet No */}
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
            </View>

            {/* ROW 4 (Filters Grid Row 2): Class [...] | Category [...] | Catalogue [...] */}
            <View style={[styles.filterGridRow, { zIndex: 80 }]}>
              {/* Col 1: Class */}
              <View style={styles.gridCol}>
                <Text style={styles.fieldLabel}>Class</Text>
                <View style={styles.lookupInputWrapper}>
                  <TextInput
                    style={styles.lookupTextInput}
                    value={classCode}
                    onChangeText={setClassCode}
                    placeholder=""
                  />
                  <TouchableOpacity
                    style={styles.lookupBtn}
                    onPress={() => openLovModal('class')}
                    activeOpacity={0.8}
                  >
                    <Text style={styles.lookupBtnText}>...</Text>
                  </TouchableOpacity>
                </View>
              </View>

              {/* Col 2: Category */}
              <View style={styles.gridCol}>
                <Text style={styles.fieldLabel}>Category</Text>
                <View style={styles.lookupInputWrapper}>
                  <TextInput
                    style={styles.lookupTextInput}
                    value={categoryCode}
                    onChangeText={setCategoryCode}
                    placeholder=""
                  />
                  <TouchableOpacity
                    style={styles.lookupBtn}
                    onPress={() => openLovModal('category')}
                    activeOpacity={0.8}
                  >
                    <Text style={styles.lookupBtnText}>...</Text>
                  </TouchableOpacity>
                </View>
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

            {/* ROW 5 (Filters Grid Row 3): Size [...] | Empty | Empty */}
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

              {/* Col 2: Empty placeholder */}
              <View style={styles.gridCol} />

              {/* Col 3: Empty placeholder */}
              <View style={styles.gridCol} />
            </View>

            {/* SIXTH ROW: Type Of Fix (Label on left, Radio buttons beside it) */}
            <View style={styles.fixTypeRowContainer}>
              <Text style={styles.fixTypeLabel}>Type Of Fix</Text>
              <View style={styles.fixTypeButtonsRow}>
                <RadioButton
                  label="All"
                  selected={fixType === 'All'}
                  onPress={() => setFixType('All')}
                />
                <RadioButton
                  label="Fix"
                  selected={fixType === 'Fix'}
                  onPress={() => setFixType('Fix')}
                />
                <RadioButton
                  label="Un Fix"
                  selected={fixType === 'Un Fix'}
                  onPress={() => setFixType('Un Fix')}
                />
              </View>
            </View>

            {/* DIVIDER */}
            <View style={styles.divider} />

            {/* BOTTOM ACTIONS: Preview | Print | PDF | Excel */}
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

      {/* GENERIC LOV MODAL */}
      <Modal
        visible={isLovModalVisible}
        transparent
        animationType="fade"
        onRequestClose={() => setIsLovModalVisible(false)}
      >
        <TouchableOpacity
          style={styles.modalOverlay}
          activeOpacity={1}
          onPress={() => setIsLovModalVisible(false)}
        >
          <TouchableWithoutFeedback>
            <View style={styles.lovModalContainer}>
              <View style={styles.modalHeader}>
                <View style={{ flexDirection: 'row', alignItems: 'center', gap: 6 }}>
                  <View style={styles.headerIconBadgeSmall}>
                    <Ionicons name="list-outline" size={16} color={GOLD_DARK} />
                  </View>
                  <Text style={styles.modalTitle}>{lovModalConfig.title}</Text>
                </View>
                <TouchableOpacity onPress={() => setIsLovModalVisible(false)} style={styles.modalCloseBtn}>
                  <Ionicons name="close" size={20} color={TEXT_DARK} />
                </TouchableOpacity>
              </View>

              <View style={{ padding: 12 }}>
                <View style={styles.modalSearchBox}>
                  <Ionicons name="search-outline" size={16} color="#8F816E" style={{ marginRight: 6 }} />
                  <TextInput
                    style={styles.modalSearchInput}
                    value={lovSearchQuery}
                    onChangeText={setLovSearchQuery}
                    placeholder={`Search ${lovModalConfig.codeLabel} or ${lovModalConfig.nameLabel}...`}
                    placeholderTextColor="#8F816E"
                    autoFocus
                  />
                  {lovSearchQuery ? (
                    <TouchableOpacity onPress={() => setLovSearchQuery('')}>
                      <Ionicons name="close-circle" size={16} color="#8F816E" />
                    </TouchableOpacity>
                  ) : null}
                </View>

                <View style={styles.lovHeaderRow}>
                  <Text style={[styles.lovHeaderText, { flex: 1.5 }]}>{lovModalConfig.codeLabel}</Text>
                  <Text style={[styles.lovHeaderText, { flex: 2.5 }]}>{lovModalConfig.nameLabel}</Text>
                </View>

                <FlatList
                  data={filteredLovData}
                  keyExtractor={(item, index) => item.code + String(index)}
                  style={{ maxHeight: 320 }}
                  ListEmptyComponent={
                    <View style={{ padding: 24, alignItems: 'center' }}>
                      <Text style={{ fontSize: 13, color: '#8F7E66', fontFamily: 'Cambria' }}>
                        No records found.
                      </Text>
                    </View>
                  }
                  renderItem={({ item, index }) => (
                    <TouchableOpacity
                      style={[styles.lovItemRow, index % 2 === 1 && { backgroundColor: '#FDFBF7' }]}
                      onPress={() => handleSelectLovItem(item)}
                      activeOpacity={0.7}
                    >
                      <Text style={[styles.lovItemText, { flex: 1.5, fontWeight: 'bold', color: GOLD_DARK }]}>
                        {item.code}
                      </Text>
                      <Text style={[styles.lovItemText, { flex: 2.5 }]}>
                        {item.name || item.code}
                      </Text>
                    </TouchableOpacity>
                  )}
                />
              </View>
            </View>
          </TouchableWithoutFeedback>
        </TouchableOpacity>
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
    backgroundColor: '#F9F2E2',
    height: 28,
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
    marginBottom: 16,
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
  statusCheckboxGroup: {
    flexDirection: 'row',
    alignItems: 'center',
    flexWrap: 'wrap',
    gap: 20,
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
    fontSize: 13,
    fontWeight: 'bold',
    color: TEXT_DARK,
    fontFamily: 'Cambria',
  },
  fixTypeRowContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 16,
    marginTop: 4,
    marginBottom: 8,
  },
  fixTypeLabel: {
    fontSize: 13,
    fontWeight: 'bold',
    color: TEXT_DARK,
    fontFamily: 'Cambria',
    minWidth: 80,
  },
  fixTypeButtonsRow: {
    flexDirection: 'row',
    alignItems: 'center',
    flexWrap: 'wrap',
    gap: 12,
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
    paddingHorizontal: 16,
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
  modalOverlay: {
    flex: 1,
    backgroundColor: 'rgba(0,0,0,0.45)',
    justifyContent: 'center',
    alignItems: 'center',
    padding: 16,
  },
  lovModalContainer: {
    width: '100%',
    maxWidth: 550,
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: GOLD_BORDER,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.15,
    shadowRadius: 10,
    elevation: 8,
    overflow: 'hidden',
  },
  modalHeader: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    paddingHorizontal: 14,
    paddingVertical: 10,
    backgroundColor: '#FBF9F5',
    borderBottomWidth: 1,
    borderBottomColor: GOLD_BORDER,
  },
  headerIconBadgeSmall: {
    width: 24,
    height: 24,
    borderRadius: 12,
    backgroundColor: '#F5EFE6',
    alignItems: 'center',
    justifyContent: 'center',
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
    borderWidth: 1,
    borderColor: '#D4C3A3',
    borderRadius: 6,
    paddingHorizontal: 10,
    height: 36,
    backgroundColor: '#FAF7F0',
    marginBottom: 10,
  },
  modalSearchInput: {
    flex: 1,
    fontSize: 12.5,
    color: TEXT_DARK,
    fontFamily: 'Cambria',
    outlineStyle: 'none',
  } as any,
  lovHeaderRow: {
    flexDirection: 'row',
    backgroundColor: '#F5EFE6',
    paddingVertical: 8,
    paddingHorizontal: 12,
    borderRadius: 4,
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    marginBottom: 4,
  },
  lovHeaderText: {
    fontSize: 12,
    fontWeight: 'bold',
    color: TEXT_DARK,
    fontFamily: 'Cambria',
  },
  lovItemRow: {
    flexDirection: 'row',
    paddingVertical: 8,
    paddingHorizontal: 12,
    borderBottomWidth: 1,
    borderBottomColor: '#F5EFE6',
    alignItems: 'center',
  },
  lovItemText: {
    fontSize: 12.5,
    color: TEXT_DARK,
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
});
