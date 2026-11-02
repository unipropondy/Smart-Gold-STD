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
  Platform,
  ScrollView,
  StyleSheet,
  Text,
  TouchableOpacity,
  View,
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';
import DateTimePicker from '@react-native-community/datetimepicker';

// Royal Gold Design Palette (matching Customer Reports)
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
  supportsSupplier?: boolean;
  supportsClass?: boolean;
  supportsCategory?: boolean;
}

const REPORT_OPTIONS: ReportOption[] = [
  { id: 'stock_by_category', label: 'Inventory Stock By Category', supportsDetail: true, supportsClass: true, supportsCategory: true, supportsSupplier: true },
  { id: 'stock_by_catalogue', label: 'Inventory Stock By Catalogue', supportsDetail: true, supportsCategory: true },
  { id: 'stock_by_category_datewise', label: 'Inventory Stock By Category – Date Wise', supportsDetail: true, supportsClass: true, supportsCategory: true },
  { id: 'stock_by_catalogue_datewise', label: 'Inventory Stock By Catalogue – Date Wise', supportsDetail: true, supportsCategory: true },
  { id: 'movement_by_size', label: 'Inventory Movement by Size', supportsDetail: true, supportsClass: true, supportsCategory: true, supportsSupplier: true },
  { id: 'stock_in_excel', label: 'Inventory Stock in Excel' },
  { id: 'stock_in_excel_datewise', label: 'Inventory Stock in Excel – Date Wise' },
  { id: 'diamond_stock', label: 'Diamond Stock', supportsClass: true, supportsCategory: true, supportsSupplier: true },
  { id: 'deleted_inventory', label: 'Deleted Inventory' },
  { id: 'online_stock', label: 'Online Stock' },
  { id: 'reorder_level', label: 'ReOrder Level', supportsCategory: true },
  { id: 'accessories_stock', label: 'Accessories Stock' },
  { id: 'scrap_stock', label: 'Scrap Inventory Stock' },
  { id: 'sold_inventory', label: 'Sold Inventory', supportsClass: true, supportsCategory: true, supportsSupplier: true },
  { id: 'purchase_return', label: 'Purchase Return', supportsSupplier: true },
  { id: 'split_inventory', label: 'Split Inventory' },
  { id: 'merged_inventory', label: 'Merged Inventory' },
  { id: 'inventory_maintenance', label: 'Inventory Maintenance' },
];

type ActionOption = 'Preview' | 'Print' | 'PDF' | 'Excel';

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

const formatDisp = (iso: string) => {
  if (!iso) return '';
  const parts = iso.split('-');
  if (parts.length === 3) return `${parts[2]} - ${parts[1]} - ${parts[0]}`;
  return iso;
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
  const [actionOption, setActionOption] = useState<ActionOption>('Preview');
  const [reportType, setReportType] = useState<'Summary' | 'Detail'>('Summary');

  // Filter values
  const [locationCode, setLocationCode] = useState<string>('ALL');
  const [supplierCode, setSupplierCode] = useState<string>('ALL');
  const [classCode, setClassCode] = useState<string>('ALL');
  const [categoryCode, setCategoryCode] = useState<string>('ALL');

  // Dropdown open states
  const [isLocOpen, setIsLocOpen] = useState(false);
  const [isSupOpen, setIsSupOpen] = useState(false);
  const [isClassOpen, setIsClassOpen] = useState(false);
  const [isCatOpen, setIsCatOpen] = useState(false);

  // Filter options lists
  const [locations, setLocations] = useState<{ id: string; label: string }[]>([{ id: 'ALL', label: 'ALL - All Locations' }]);
  const [suppliers, setSuppliers] = useState<{ id: string; label: string }[]>([{ id: 'ALL', label: 'ALL - All Suppliers' }]);
  const [classes, setClasses] = useState<{ id: string; label: string }[]>([{ id: 'ALL', label: 'ALL - All Classes' }]);
  const [categories, setCategories] = useState<{ id: string; label: string }[]>([{ id: 'ALL', label: 'ALL - All Categories' }]);

  const closeAllDropdowns = () => {
    setIsReportDropdownOpen(false);
    setIsLocOpen(false);
    setIsSupOpen(false);
    setIsClassOpen(false);
    setIsCatOpen(false);
  };

  useEffect(() => {
    (async () => {
      try {
        const [locRes, supRes, clsRes, catRes] = await Promise.all([
          fetch(`${API_BASE_URL}/inventoryreports/locations`).then(r => r.json()).catch(() => ({ data: [] })),
          fetch(`${API_BASE_URL}/inventoryreports/suppliers`).then(r => r.json()).catch(() => ({ data: [] })),
          fetch(`${API_BASE_URL}/inventoryreports/classes`).then(r => r.json()).catch(() => ({ data: [] })),
          fetch(`${API_BASE_URL}/inventoryreports/categories`).then(r => r.json()).catch(() => ({ data: [] })),
        ]);

        if (locRes?.data) setLocations(locRes.data.map((x: any) => ({ id: x.LocationCode, label: x.LocationName || x.LocationCode })));
        if (supRes?.data) setSuppliers(supRes.data.map((x: any) => ({ id: x.SupplierCode, label: x.SupplierName || x.SupplierCode })));
        if (clsRes?.data) setClasses(clsRes.data.map((x: any) => ({ id: x.ClassCode, label: x.ClassName || x.ClassCode })));
        if (catRes?.data) setCategories(catRes.data.map((x: any) => ({ id: x.CategoryCode, label: x.CategoryName || x.CategoryCode })));
      } catch (e) {
        console.warn('Failed loading inventory filters:', e);
      }
    })();
  }, []);

  const [fromDate, setFromDate] = useState('2025-01-01');
  const [toDate, setToDate] = useState(formatDateLocal(new Date()));
  const [isLoading, setIsLoading] = useState(false);

  const [showDatePicker, setShowDatePicker] = useState<{ show: boolean; mode: 'from' | 'to' }>({
    show: false,
    mode: 'from',
  });

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

    let win: Window | null = null;
    if (
      Platform.OS === 'web' &&
      typeof window !== 'undefined' &&
      (currentAction === 'Preview' || currentAction === 'Print')
    ) {
      win = window.open('', '_blank');
    }

    try {
      const res = await fetch(`${API_BASE_URL}/inventoryreports/generate`, {
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
            classCode,
            categoryCode,
          },
        }),
      });

      const json = await res.json();

      if (!json.success || !json.data || json.data.length === 0) {
        if (win) win.close();
        showError(json.message || 'No records found for the selected criteria.');
        setIsLoading(false);
        return;
      }

      const repTitle = json.title || (selectedReportObj ? selectedReportObj.label : 'Inventory Report');
      const rawCols = json.columns || Object.keys(json.data[0]).map((k) => ({ key: k, label: k }));
      const columns = rawCols.map((c: any) => ({ ...c, label: formatColumnHeader(c.label || c.key) }));
      const cleanName = repTitle.replace(/[^a-zA-Z0-9_-]/g, '_');

      // 1. PDF DOWNLOAD ACTION
      if (currentAction === 'PDF') {
        const pdfRes = await fetch(`${API_BASE_URL}/inventoryreports/export-pdf`, {
          method: 'POST',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify({
            title: repTitle,
            columns,
            data: json.data,
            totals: json.totals || {},
            fromDate,
            toDate,
          }),
        });

        if (!pdfRes.ok) {
          throw new Error('Failed to generate PDF document');
        }

        const pdfBlob = await pdfRes.blob();
        if (Platform.OS === 'web' && typeof window !== 'undefined') {
          const url = URL.createObjectURL(pdfBlob);
          const link = document.createElement('a');
          link.href = url;
          link.setAttribute('download', `${cleanName}.pdf`);
          document.body.appendChild(link);
          link.click();
          document.body.removeChild(link);
          setTimeout(() => URL.revokeObjectURL(url), 1000);
          showSuccess('PDF downloaded successfully!');
        } else {
          showSuccess('PDF report generated!');
        }
        setIsLoading(false);
        return;
      }

      // 2. EXCEL DOWNLOAD ACTION
      if (currentAction === 'Excel') {
        const excelRes = await fetch(`${API_BASE_URL}/inventoryreports/export-excel`, {
          method: 'POST',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify({
            title: repTitle,
            columns,
            data: json.data,
            totals: json.totals || {},
            fromDate,
            toDate,
          }),
        });

        if (!excelRes.ok) {
          throw new Error('Failed to generate Excel report');
        }

        const excelBlob = await excelRes.blob();
        if (Platform.OS === 'web' && typeof window !== 'undefined') {
          const url = URL.createObjectURL(excelBlob);
          const link = document.createElement('a');
          link.href = url;
          link.setAttribute('download', `${cleanName}.xlsx`);
          document.body.appendChild(link);
          link.click();
          document.body.removeChild(link);
          setTimeout(() => URL.revokeObjectURL(url), 1000);
          showSuccess('Excel file downloaded successfully!');
        } else {
          showSuccess('Excel report generated!');
        }
        setIsLoading(false);
        return;
      }

      // 3. PREVIEW & PRINT ACTIONS (HTML)
      const htmlRes = await fetch(`${API_BASE_URL}/inventoryreports/download-html`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          title: repTitle,
          columns,
          data: json.data,
          totals: json.totals || {},
          fromDate,
          toDate,
          showPrintButton: false,
        }),
      });

      if (!htmlRes.ok) {
        throw new Error('Failed to generate report HTML template');
      }

      let htmlContent = await htmlRes.text();

      // Trigger print for Print option
      if (currentAction === 'Print') {
        htmlContent = htmlContent.replace(
          '</body>',
          '<script>window.addEventListener("load", function(){ setTimeout(function(){ window.print(); }, 500); });</script></body>'
        );
      }

      if (Platform.OS === 'web' && typeof window !== 'undefined') {
        if (win) {
          win.document.open();
          win.document.write(htmlContent);
          win.document.close();
        } else {
          const blob = new Blob([htmlContent], { type: 'text/html;charset=utf-8;' });
          const url = URL.createObjectURL(blob);
          const newTab = window.open(url, '_blank');
          if (!newTab) {
            const link = document.createElement('a');
            link.href = url;
            link.setAttribute('download', `${repTitle}.html`);
            document.body.appendChild(link);
            link.click();
            document.body.removeChild(link);
          }
        }
        if (currentAction === 'Print') {
          showSuccess('Print dialog opened.');
        } else {
          showSuccess('Report preview opened.');
        }
      } else {
        showSuccess('Report generated! (Use web to view/print)');
      }
    } catch (err: any) {
      if (win) win.close();
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
          if (isReportDropdownOpen) {
            setIsReportDropdownOpen(false);
          } else {
            router.back();
          }
        }
      };
      window.addEventListener('keydown', handleKeyDown);
      return () => window.removeEventListener('keydown', handleKeyDown);
    }
  }, [selectedReport, fromDate, toDate, actionOption, isReportDropdownOpen, reportType]);

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
            {/* Top row: Inventory Report, From Date, To Date */}
            <View style={styles.inputRow}>
              {/* Inventory Report */}
              <View style={styles.reportCol}>
                <ReportDropdown
                  label="Inventory Report"
                  options={REPORT_OPTIONS}
                  selectedId={selectedReport}
                  onSelect={(id) => {
                    setSelectedReport(id);
                    setIsReportDropdownOpen(false);
                  }}
                  isOpen={isReportDropdownOpen}
                  onToggle={() => setIsReportDropdownOpen(!isReportDropdownOpen)}
                  maxHeight={280}
                />
              </View>

              {/* From Date */}
              <ReportDatePicker
                label="From Date"
                value={fromDate}
                onChange={setFromDate}
                isIsoOutput={true}
                onOpenNative={() => setShowDatePicker({ show: true, mode: 'from' })}
              />

              {/* To Date */}
              <ReportDatePicker
                label="To Date"
                value={toDate}
                onChange={setToDate}
                isIsoOutput={true}
                onOpenNative={() => setShowDatePicker({ show: true, mode: 'to' })}
              />
            </View>

            {/* Optional Summary / Detail RadioButtons for reports that support Detail */}
            {selectedReportObj?.supportsDetail && (
              <View style={{ flexDirection: 'row', gap: 12, marginTop: 14 }}>
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
            )}

            {/* RESTORED INVENTORY FILTER OPTIONS MATCHING CUSTOMER REPORTS */}
            {(selectedReportObj?.supportsSupplier ||
              selectedReportObj?.supportsClass ||
              selectedReportObj?.supportsCategory ||
              selectedReport === 'stock_by_category') && (
              <View style={styles.filterSection}>
                <View style={[styles.inputRow, { marginTop: 12, zIndex: 15 }]}>
                  {/* Location Filter */}
                  <View style={styles.filterCol}>
                    <ReportDropdown
                      label="Location"
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

                  {/* Supplier Filter */}
                  {selectedReportObj?.supportsSupplier && (
                    <View style={styles.filterCol}>
                      <ReportDropdown
                        label="Supplier"
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
                  )}

                  {/* Class Filter */}
                  {selectedReportObj?.supportsClass && (
                    <View style={styles.filterCol}>
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
                  )}
                </View>

                {/* Secondary Row: Category Filter */}
                {selectedReportObj?.supportsCategory && (
                  <View style={[styles.inputRow, { marginTop: 12, zIndex: 14 }]}>
                    <View style={styles.filterCol}>
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
                  </View>
                )}
              </View>
            )}

            {/* Divider */}
            <View style={styles.divider} />

            {/* Actions: Preview, Print, PDF, Excel */}
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
    padding: 16,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.04,
    shadowRadius: 3,
    elevation: 2,
  },
  inputRow: {
    flexDirection: Platform.OS === 'web' ? 'row' : 'column',
    gap: 16,
    alignItems: Platform.OS === 'web' ? 'flex-start' : 'stretch',
    zIndex: 10,
  },
  reportCol: {
    flex: 1.5,
    position: 'relative',
    zIndex: 20,
  },
  dateCol: {
    flex: 1,
  },
  fieldLabel: {
    fontSize: 12,
    fontWeight: 'bold',
    color: TEXT_DARK,
    marginBottom: 6,
    fontFamily: 'Cambria',
  },
  inputBox: {
    height: 36,
    borderWidth: 1,
    borderColor: '#D4C3A3',
    borderRadius: 6,
    backgroundColor: CARD_BG,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingHorizontal: 12,
  },
  inputBoxActive: {
    borderColor: GOLD_ACCENT,
  },
  dropdownText: {
    fontSize: 12.5,
    fontWeight: '500',
    color: TEXT_DARK,
    flex: 1,
    marginRight: 6,
    fontFamily: 'Cambria',
  },
  dropdownPlaceholder: {
    color: '#9E8D77',
  },
  dropdownMenu: {
    position: 'absolute',
    top: 64,
    left: 0,
    right: 0,
    backgroundColor: CARD_BG,
    borderRadius: 6,
    borderWidth: 1,
    borderColor: '#D4C3A3',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.15,
    shadowRadius: 8,
    elevation: 9999,
    zIndex: 99999,
  },
  menuItem: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingVertical: 9,
    paddingHorizontal: 12,
    borderBottomWidth: 1,
    borderBottomColor: '#F5EFE6',
  },
  menuItemSelected: {
    backgroundColor: '#FFF4DA',
  },
  menuItemText: {
    fontSize: 12.5,
    color: TEXT_DARK,
    fontFamily: 'Cambria',
  },
  menuItemTextSelected: {
    color: GOLD_DARK,
    fontWeight: 'bold',
    fontFamily: 'Cambria',
  },
  webDateWrapper: {
    height: 36,
    borderWidth: 1,
    borderColor: '#D4C3A3',
    borderRadius: 6,
    backgroundColor: CARD_BG,
    flexDirection: 'row',
    alignItems: 'center',
    overflow: 'hidden',
  },
  webDateInput: {
    flex: 1,
    height: '100%',
    border: 'none',
    backgroundColor: 'transparent',
    outline: 'none',
    color: TEXT_DARK,
    paddingLeft: 12,
    paddingRight: 8,
    fontSize: 12.5,
    fontFamily: 'Cambria',
    cursor: 'pointer',
  } as any,
  dateText: {
    fontSize: 12.5,
    color: TEXT_DARK,
    fontFamily: 'Cambria',
  },
  dateIconBox: {
    paddingRight: 10,
    justifyContent: 'center',
    alignItems: 'center',
  },
  divider: {
    height: 1,
    backgroundColor: '#F5EFE6',
    marginVertical: 16,
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
  filterSection: {
    borderTopWidth: 1,
    borderTopColor: '#F5EFE6',
    paddingTop: 12,
    marginTop: 10,
    marginBottom: 6,
  },
  filterCol: {
    flex: 1,
    minWidth: 160,
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
