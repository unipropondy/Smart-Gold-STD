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
}

const REPORT_OPTIONS: ReportOption[] = [
  { id: 'stock_by_category', label: 'Inventory Stock By Category' },
  { id: 'stock_by_catalogue', label: 'Inventory Stock By Catalogue' },
  { id: 'stock_by_category_datewise', label: 'Inventory Stock By Category – Date Wise' },
  { id: 'stock_by_catalogue_datewise', label: 'Inventory Stock By Catalogue – Date Wise' },
  { id: 'movement_by_size', label: 'Inventory Movement by Size' },
  { id: 'stock_in_excel', label: 'Inventory Stock in Excel' },
  { id: 'stock_in_excel_datewise', label: 'Inventory Stock in Excel – Date Wise' },
  { id: 'diamond_stock', label: 'Diamond Stock' },
  { id: 'deleted_inventory', label: 'Deleted Inventory' },
  { id: 'online_stock', label: 'Online Stock' },
  { id: 'reorder_level', label: 'ReOrder Level' },
  { id: 'accessories_stock', label: 'Accessories Stock' },
  { id: 'scrap_stock', label: 'Scrap Inventory Stock' },
  { id: 'sold_inventory', label: 'Sold Inventory' },
  { id: 'purchase_return', label: 'Purchase Return' },
  { id: 'split_inventory', label: 'Split Inventory' },
  { id: 'merged_inventory', label: 'Merged Inventory' },
  { id: 'inventory_maintenance', label: 'Inventory Maintenance' },
];

type ActionOption = 'Preview' | 'Print' | 'PDF' | 'Excel';
type LovType = 'class' | 'category' | 'catalogue' | 'size' | 'classType';

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

export default function InventoryReportsScreen() {
  const router = useRouter();

  const [toastMessage, setToastMessage] = useState('');
  const [toastType, setToastType] = useState<'success' | 'error'>('success');

  const [selectedReport, setSelectedReport] = useState<string>('stock_by_category');
  const [isReportDropdownOpen, setIsReportDropdownOpen] = useState(false);
  const [reportType, setReportType] = useState<'Summary' | 'Detail'>('Summary');

  // Dates
  const [fromDate, setFromDate] = useState(formatDateLocal(new Date()));
  const [toDate, setToDate] = useState(formatDateLocal(new Date()));
  const [isLoading, setIsLoading] = useState(false);

  // Dropdown filter values
  const [locationCode, setLocationCode] = useState<string>('');
  const [supplierCode, setSupplierCode] = useState<string>('');
  const [isLocOpen, setIsLocOpen] = useState(false);
  const [isSupOpen, setIsSupOpen] = useState(false);

  // Lookup text inputs
  const [classCode, setClassCode] = useState<string>('');
  const [categoryCode, setCategoryCode] = useState<string>('');
  const [catalogueCode, setCatalogueCode] = useState<string>('');
  const [sizeCode, setSizeCode] = useState<string>('');
  const [classType, setClassType] = useState<string>('');
  const [includeImage, setIncludeImage] = useState<boolean>(false);

  // Bottom action radio buttons
  const [actionOption, setActionOption] = useState<ActionOption>('Preview');

  // Master data lists for lookups and dropdowns
  const [locations, setLocations] = useState<{ id: string; label: string }[]>([
    { id: '', label: '-- Select --' },
  ]);
  const [suppliers, setSuppliers] = useState<{ id: string; label: string }[]>([
    { id: '', label: '-- Select --' },
  ]);
  const [classesList, setClassesList] = useState<{ code: string; name: string }[]>([]);
  const [categoriesList, setCategoriesList] = useState<{ code: string; name: string }[]>([]);
  const [cataloguesList, setCataloguesList] = useState<{ code: string; name: string }[]>([]);
  const [sizesList, setSizesList] = useState<{ code: string; name: string }[]>([]);
  const [classTypesList, setClassTypesList] = useState<{ code: string; name: string }[]>([]);

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
    setIsLocOpen(false);
    setIsSupOpen(false);
  };

  useEffect(() => {
    (async () => {
      try {
        const [locRes, supRes, clsRes, catRes, catlgRes, szRes, ctRes] = await Promise.all([
          fetch(`${API_BASE_URL}/inventoryreports/locations`).then((r) => r.json()).catch(() => ({ data: [] })),
          fetch(`${API_BASE_URL}/inventoryreports/suppliers`).then((r) => r.json()).catch(() => ({ data: [] })),
          fetch(`${API_BASE_URL}/inventoryreports/classes`).then((r) => r.json()).catch(() => ({ data: [] })),
          fetch(`${API_BASE_URL}/inventoryreports/categories`).then((r) => r.json()).catch(() => ({ data: [] })),
          fetch(`${API_BASE_URL}/inventoryreports/catalogues`).then((r) => r.json()).catch(() => ({ data: [] })),
          fetch(`${API_BASE_URL}/inventoryreports/sizes`).then((r) => r.json()).catch(() => ({ data: [] })),
          fetch(`${API_BASE_URL}/inventoryreports/class-types`).then((r) => r.json()).catch(() => ({ data: [] })),
        ]);

        if (locRes?.data) {
          const locs = locRes.data.map((x: any) => ({
            id: x.LocationCode,
            label: x.LocationCode === 'ALL' ? '-- Select --' : `${x.LocationCode} - ${x.LocationName || x.LocationCode}`,
          }));
          setLocations([{ id: '', label: '-- Select --' }, ...locs.filter((l: any) => l.id !== 'ALL')]);
        }
        if (supRes?.data) {
          const sups = supRes.data.map((x: any) => ({
            id: x.SupplierCode,
            label: x.SupplierCode === 'ALL' ? '-- Select --' : `${x.SupplierCode} - ${x.SupplierName || x.SupplierCode}`,
          }));
          setSuppliers([{ id: '', label: '-- Select --' }, ...sups.filter((s: any) => s.id !== 'ALL')]);
        }
        if (clsRes?.data) {
          setClassesList(clsRes.data.map((x: any) => ({ code: x.ClassCode, name: x.ClassName || x.ClassCode })));
        }
        if (catRes?.data) {
          setCategoriesList(catRes.data.map((x: any) => ({ code: x.CategoryCode, name: x.CategoryName || x.CategoryCode })));
        }
        if (catlgRes?.data) {
          setCataloguesList(catlgRes.data.map((x: any) => ({ code: x.CatalogueCode, name: x.CatalogueName || x.CatalogueCode })));
        }
        if (szRes?.data) {
          setSizesList(szRes.data.map((x: any) => ({ code: x.SizeCode, name: x.SizeType || x.SizeCode })));
        }
        if (ctRes?.data) {
          setClassTypesList(ctRes.data.map((x: any) => ({ code: x.ClassType, name: x.ClassType })));
        }
      } catch (e) {
        console.warn('Failed loading inventory filter data:', e);
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
      case 'classType':
        return {
          title: 'Select ClassType',
          codeLabel: 'ClassType',
          nameLabel: 'Description',
          data: classTypesList,
        };
    }
  }, [lovType, classesList, categoriesList, cataloguesList, sizesList, classTypesList]);

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
      case 'classType':
        setClassType(item.code);
        break;
    }
    setIsLovModalVisible(false);
  };

  const handleGetReport = async (overrideAction?: ActionOption) => {
    const currentAction = overrideAction || actionOption;

    if (!selectedReport) {
      showError('Please select an Inventory Report.');
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
      const res = await fetch(`${API_BASE_URL}/inventoryreports/generate`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          reportId: selectedReport,
          reportType: reportType,
          filters: {
            fromDate,
            toDate,
            locationCode: locationCode || undefined,
            supplierCode: supplierCode || undefined,
            classCode: classCode || undefined,
            categoryCode: categoryCode || undefined,
            catalogueCode: catalogueCode || undefined,
            sizeCode: sizeCode || undefined,
            classType: classType || undefined,
            includeImage,
          },
        }),
      });

      const json = await res.json();

      if (!json.success || !json.data || json.data.length === 0) {
        showError(json.message || 'No records found for the selected criteria.');
        setIsLoading(false);
        return;
      }

      const repTitle = json.title || (selectedReportObj ? selectedReportObj.label : 'Inventory Report');
      const rawCols = json.columns || Object.keys(json.data[0]).map((k) => ({ key: k, label: k }));
      const columns = rawCols.map((c: any) => ({ ...c, label: formatColumnHeader(c.label || c.key) }));
      const repTotals = json.summary || {};

      if (currentAction === 'Preview') {
        await printHtmlReport(
          {
            endpoint: 'inventoryreports',
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
            endpoint: 'inventoryreports',
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
          endpoint: 'inventoryreports',
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
          endpoint: 'inventoryreports',
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
          } else if (isReportDropdownOpen || isLocOpen || isSupOpen) {
            closeAllDropdowns();
          } else {
            router.back();
          }
        }
      };
      window.addEventListener('keydown', handleKeyDown);
      return () => window.removeEventListener('keydown', handleKeyDown);
    }
  }, [
    selectedReport,
    fromDate,
    toDate,
    actionOption,
    isReportDropdownOpen,
    isLocOpen,
    isSupOpen,
    reportType,
    isLovModalVisible,
  ]);

  return (
    <SafeAreaView style={styles.container} edges={['top']}>
      <Stack.Screen options={{ headerShown: false }} />

      {/* HEADER */}
      <View style={styles.header}>
        <View style={styles.headerLeft}>
          <TouchableOpacity onPress={() => router.back()} style={styles.backButton} activeOpacity={0.7}>
            <Ionicons name="arrow-back" size={20} color={TEXT_DARK} />
          </TouchableOpacity>
          <Text style={styles.title}>Inventory Reports</Text>
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
            {/* ROW 1: Inventory Report | From Date | To Date */}
            <View style={[styles.filterGridRow, { zIndex: 100 }]}>
              {/* Col 1: Inventory Report */}
              <View style={styles.gridCol}>
                <ReportDropdown
                  label="Inventory Report"
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

            {/* ROW 2: Summary / Detail Radio Buttons */}
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

            {/* ROW 3 (Filters Grid Row 1): Location Code | Supplier Code | Class [...] */}
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

              {/* Col 3: Class */}
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
            </View>

            {/* ROW 4 (Filters Grid Row 2): Category [...] | Catalogue [...] | Size [...] */}
            <View style={[styles.filterGridRow, { zIndex: 80 }]}>
              {/* Col 1: Category */}
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

              {/* Col 2: Catalogue */}
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

              {/* Col 3: Size */}
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
            </View>

            {/* ROW 5 (Filters Grid Row 3): ClassType [...] | Image Checkbox | Empty Col */}
            <View style={[styles.filterGridRow, { zIndex: 70 }]}>
              {/* Col 1: ClassType */}
              <View style={styles.gridCol}>
                <Text style={styles.fieldLabel}>ClassType</Text>
                <View style={styles.lookupInputWrapper}>
                  <TextInput
                    style={styles.lookupTextInput}
                    value={classType}
                    onChangeText={setClassType}
                    placeholder=""
                  />
                  <TouchableOpacity
                    style={styles.lookupBtn}
                    onPress={() => openLovModal('classType')}
                    activeOpacity={0.8}
                  >
                    <Text style={styles.lookupBtnText}>...</Text>
                  </TouchableOpacity>
                </View>
              </View>

              {/* Col 2: Image Checkbox */}
              <View style={[styles.gridCol, styles.checkboxColWrapper]}>
                <TouchableOpacity
                  style={styles.checkboxRow}
                  onPress={() => setIncludeImage(!includeImage)}
                  activeOpacity={0.7}
                >
                  <View style={[styles.checkboxBox, includeImage && styles.checkboxBoxChecked]}>
                    {includeImage && <Ionicons name="checkmark" size={14} color="#FFFFFF" />}
                  </View>
                  <Text style={styles.checkboxLabel}>Image</Text>
                </TouchableOpacity>
              </View>

              {/* Col 3: Empty placeholder to align columns perfectly */}
              <View style={styles.gridCol} />
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
  reportTypeRow: {
    flexDirection: 'row',
    gap: 12,
    marginTop: 2,
    marginBottom: 16,
  },
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
  checkboxColWrapper: {
    justifyContent: 'flex-start',
    paddingTop: 24, // Aligns checkbox vertically with adjacent TextInput fields whose label is above them
  },
  checkboxRow: {
    flexDirection: 'row',
    alignItems: 'center',
    height: 36,
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
