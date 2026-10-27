import { ThemedText } from '@/components/themed-text';
import { useThemeContext } from '@/contexts/ThemeContext';
import { useThemeColors } from '@/hooks/useThemeColors';
import { Ionicons } from '@expo/vector-icons';
import { Stack, useRouter } from 'expo-router';
import React, { useEffect, useRef, useState } from 'react';
import {
  ActivityIndicator,
  Alert,
  FlatList,
  Modal,
  Platform,
  SafeAreaView,
  ScrollView,
  StyleSheet,
  TextInput,
  TouchableOpacity,
  View
} from 'react-native';

import { API_BASE_URL } from '@/constants/api';

const API_URL = `${API_BASE_URL}`;

const getInitialHeaderState = () => ({
  JobSheetNo: '',
  InvoiceNo: '',
  InvoiceDate: new Date().toISOString().split('T')[0],
  SupplierCode: '',
  SupplierName: '',
  CurrencyCode: 'SGD',
  CurrencyName: 'SGD',
  CurrencyRate: '1.00',
  CostPerGram: '0.00',
  TotalCost: '0.00',
  TotalMC: '0.00',
  GSTPercent: '0',
  GSTAmount: '0.00',
  GSTActive: false,
  HGSTActive: false,
  Weight: '0.000',
  GoldLoss: '0.000',
  Remarks: '',
  BillClose: 'N',
  TranType: 'DP',
  TotalWeight24K: '0.000',
  TEAL: '0.00',
  TEALPercent: '.92',
  Payable: 'Cash',
  FixUnFix: 'Fix',
  Ordered: false,
  OrderNo: '',
});

const getInitialDetailState = () => ({
  CatalogueCode: '',
  CategoryCode: '',
  TrayId: '',
  CatalogueWeight: '',
  CataloguePCS: '',
  PurchaseMCType: 'PerGram',
  SalesMCType: 'PerGram',
  Auto: true,
  PCSCheck: false,
  InventoryCode: '',
  InventoryDescription: '',
  Weight: '',
  NetWeight: '',
  SizeCode: '',
  GoldLoss: '',
  CostPerGram: '',
  PMC: '',
  PMCCost: '0.00',
  SMC: '0',
  SMCCost: '0.00',
  PCS: '1',
  TotalCost: '0.00',
  ClassCode: '',
  DiamondDescription: '',
  StoneWeight: '',
  IPM: false,
  IPMType: 'Coin',
  IPMBarCoin: '',
  IPMPurity: '',
  IPMRefiner: '',
  IPMCountry: '',
  IPMGSTType: '',
  IPMBarNo: '',
  AddlDesc: false,
  Desc1: '',
  Desc2: '',
  Desc3: '',
  Desc4: '',
  Cost1: '',
  Cost2: '',
  Cost3: '',
  Cost4: '',
  SellingPrice1: '',
  SellingPrice2: '',
  SellingPrice3: '',
  SellingPrice4: '',
  Image: '',
  TradeInInventoryCode: '',
  TradeInDescription: '',
  TradeInInvoice: '',
  TradeInWeight: '',
});

const recalculateDetailCosts = (detail: any, currentAutoSMC: boolean = false, currencyRateStr: string = '1.00') => {
  const weight = parseFloat(detail.Weight) || 0;
  const stoneWeight = parseFloat(detail.StoneWeight) || 0;
  const goldLoss = parseFloat(detail.GoldLoss) || 0;
  const costPerGram = parseFloat(detail.CostPerGram) || 0;
  const pmc = parseFloat(detail.PMC) || 0;
  let smc = parseFloat(detail.SMC) || 0;
  const pcs = parseInt(detail.PCS, 10) || 1;
  const currencyRate = parseFloat(currencyRateStr) || 1;

  const netWeight = weight - stoneWeight > 0 ? weight - stoneWeight : weight;

  let pmcCost = 0;
  if (detail.PurchaseMCType === 'PerGram') {
    pmcCost = pmc * netWeight;
  } else {
    pmcCost = pmc * (pcs > 0 ? pcs : 1);
  }

  let smcCost = 0;
  if (currentAutoSMC) {
    smc = Math.round(pmc * 4);
    if (detail.SalesMCType === 'PerGram') {
      smcCost = Math.round(pmc * netWeight * 4);
    } else {
      smcCost = Math.round(pmc * (pcs > 0 ? pcs : 1) * 4);
    }
  } else {
    if (detail.SalesMCType === 'PerGram') {
      smcCost = Math.round(smc * netWeight);
    } else {
      smcCost = Math.round(smc * (pcs > 0 ? pcs : 1));
    }
  }

  let dCost = (netWeight + goldLoss) * costPerGram + pmcCost;

  if (detail.AddlDesc) {
    const c1 = parseFloat(detail.Cost1) || 0;
    const c2 = parseFloat(detail.Cost2) || 0;
    const c3 = parseFloat(detail.Cost3) || 0;
    const c4 = parseFloat(detail.Cost4) || 0;
    dCost += c1 + c2 + c3 + c4;
    pmcCost += c1 + c2 + c3 + c4;
  }

  const dLocalCost = currencyRate !== 0 ? dCost : dCost;

  return {
    ...detail,
    NetWeight: netWeight > 0 ? netWeight.toFixed(3) : '',
    SMC: currentAutoSMC ? smc.toFixed(2) : detail.SMC,
    PMCCost: pmcCost.toFixed(2),
    SMCCost: smcCost.toFixed(2),
    TotalCost: dLocalCost.toFixed(2),
  };
};

const calculateHeaderSummary = (details: any[], hgstActive: boolean = false, gstPercentStr: string = '0') => {
  let totalCost = 0;
  let totalMC = 0;
  let totalWeight = 0;
  let totalGoldLoss = 0;

  for (const d of details) {
    totalCost += parseFloat(d.TotalCost) || 0;
    totalMC += parseFloat(d.PMCCost) || 0;
    totalWeight += parseFloat(d.Weight) || 0;
    totalGoldLoss += parseFloat(d.GoldLoss) || 0;
  }

  const gstPercent = parseFloat(gstPercentStr) || 0;
  let gstAmount = 0;
  if (hgstActive && gstPercent > 0) {
    gstAmount = (totalCost * gstPercent) / 100;
  }

  return {
    TotalCost: totalCost.toFixed(2),
    TotalMC: totalMC.toFixed(2),
    GSTAmount: gstAmount.toFixed(2),
    Weight: totalWeight.toFixed(3),
    GoldLoss: totalGoldLoss.toFixed(3),
  };
};

const calculateCostPerGramFromTeal = (
  teal: any,
  tealPercent: any,
  gstActive: boolean = false,
  gstPercent: any = '0'
) => {
  const t = parseFloat(teal) || 0;
  if (t <= 0) return '0.00';

  const tp = parseFloat(tealPercent) || 0;
  const companyGst = parseFloat(gstPercent) || 0;
  const gstFactor = 1 + (companyGst / 100);

  let cost = 0;
  if (tp !== 0) {
    if (gstActive) {
      cost = (t * 0.02646 * tp) * gstFactor;
    } else {
      cost = t * 0.02646 * tp;
    }
  } else {
    if (gstActive) {
      cost = (t * 0.02646 * 0.92) * gstFactor;
    } else {
      cost = t * 0.02646 * 0.92;
    }
  }
  return cost.toFixed(2);
};

const calculateTealFromCostPerGram = (
  costPerGram: any,
  tealPercent: any,
  gstActive: boolean = false,
  gstPercent: any = '0'
) => {
  const c = parseFloat(costPerGram) || 0;
  if (c <= 0) return '0.00';

  const tp = parseFloat(tealPercent) || 0;
  const companyGst = parseFloat(gstPercent) || 0;
  const gstFactor = 1 + (companyGst / 100);

  let teal = 0;
  if (tp !== 0) {
    if (gstActive) {
      teal = ((c / gstFactor) / tp) / 0.02646;
    } else {
      teal = (c / tp) / 0.02646;
    }
  } else {
    if (gstActive) {
      teal = ((c / gstFactor) / 0.92) / 0.02646;
    } else {
      teal = (c / 0.92) / 0.02646;
    }
  }
  return Math.round(teal).toString();
};

const validateInventoryLine = (item: any, header: any, salesPCS: boolean = false) => {
  if (!item.CatalogueCode?.trim()) {
    return { isValid: false, message: 'Please select a catalogue code' };
  }
  if (!item.CategoryCode?.trim()) {
    return { isValid: false, message: 'Please select a category code' };
  }
  if (!item.InventoryCode?.trim()) {
    return { isValid: false, message: 'Inventory Code is required' };
  }
  const weight = parseFloat(item.Weight) || 0;
  if (weight <= 0 && !salesPCS) {
    return { isValid: false, message: 'Please enter a valid weight (> 0)' };
  }
  const pcs = parseInt(item.PCS, 10) || 0;
  if (pcs <= 0) {
    return { isValid: false, message: 'Please enter a valid PCS (> 0)' };
  }
  return { isValid: true, message: '' };
};

const validatePurchaseSave = (header: any, details: any[]) => {
  if (!header.SupplierCode?.trim()) {
    return { isValid: false, message: 'Please select a supplier' };
  }
  if (!header.InvoiceNo?.trim()) {
    return { isValid: false, message: 'Please enter an Invoice No.' };
  }
  if (!details || details.length === 0) {
    return { isValid: false, message: 'Please add at least one line item to the grid' };
  }
  return { isValid: true, message: '' };
};

const fetchCompanyInfo = async () => {
  try {
    const res = await fetch(`${API_URL}/company`);
    const json = await res.json();
    if (json.success && json.data) {
      return Array.isArray(json.data) ? json.data[0] : json.data;
    }
  } catch (err) {
    console.error('Error fetching company info:', err);
  }
  return null;
};

const fetchSuppliersApi = async () => {
  const res = await fetch(`${API_URL}/supplier`);
  return res.json();
};

const fetchSupplierByCodeApi = async (code: string) => {
  const res = await fetch(`${API_URL}/supplier/${encodeURIComponent(code)}`);
  return res.json();
};

const fetchCataloguesApi = async () => {
  const res = await fetch(`${API_URL}/catalogue`);
  return res.json();
};

const fetchCatalogueByCodeApi = async (code: string) => {
  const res = await fetch(`${API_URL}/catalogue/${encodeURIComponent(code)}`);
  return res.json();
};

const fetchCategoriesApi = async () => {
  const res = await fetch(`${API_URL}/category`);
  return res.json();
};

const fetchCategoryByCodeApi = async (code: string) => {
  const res = await fetch(`${API_URL}/category/${encodeURIComponent(code)}`);
  return res.json();
};

const fetchSizesApi = async () => {
  const res = await fetch(`${API_URL}/size`);
  return res.json();
};

const fetchTraysApi = async () => {
  const res = await fetch(`${API_URL}/traymaster`);
  return res.json();
};

const fetchCurrenciesApi = async () => {
  const res = await fetch(`${API_URL}/currency`);
  return res.json();
};

const fetchInventoryApi = async () => {
  const res = await fetch(`${API_URL}/inventorysearch`);
  return res.json();
};

const fetchRefinersApi = async () => {
  const res = await fetch(`${API_URL}/refiner`);
  return res.json();
};

const fetchCountriesApi = async () => {
  const res = await fetch(`${API_URL}/country`);
  return res.json();
};

const fetchTaelPercentsApi = async () => {
  const res = await fetch(`${API_URL}/taelpercent`);
  return res.json();
};

const fetchJobSheetsApi = async () => {
  const res = await fetch(`${API_URL}/jobsheet`);
  return res.json();
};

const fetchPurchasesApi = async () => {
  const res = await fetch(`${API_URL}/directpurchase`);
  return res.json();
};

const fetchNextJobSheetNoApi = async () => {
  try {
    const res = await fetch(`${API_URL}/jobsheet/next-number`);
    const json = await res.json();
    if (json.success && json.nextNo) {
      return json.nextNo;
    }
  } catch (e) {
    console.error('Error fetching next jobsheet no:', e);
  }
  return '';
};

const fetchJobSheetByNoApi = async (jsNo: string) => {
  const res = await fetch(`${API_URL}/jobsheet/${encodeURIComponent(jsNo)}`);
  return res.json();
};

const fetchPurchaseByBillNoApi = async (billNo: string) => {
  const res = await fetch(`${API_URL}/directpurchase/${encodeURIComponent(billNo)}`);
  return res.json();
};

const saveDirectPurchaseApi = async (header: any, details: any[]) => {
  const mappedDetails = details.map((d: any) => ({
    ...d,
    PMCType: d.PurchaseMCType === 'PerPCS' ? 'P' : 'W',
    SMCType: d.SalesMCType === 'PerPCS' ? 'P' : 'W',
    MC: parseFloat(d.PMCCost || d.PMC) || 0,
    Weight: parseFloat(d.Weight) || 0,
    StoneWeight: parseFloat(d.StoneWeight) || 0,
    GoldLoss: parseFloat(d.GoldLoss) || 0,
    CostPerGram: parseFloat(d.CostPerGram) || 0,
    TotalCost: parseFloat(d.TotalCost) || 0,
    PCS: parseInt(d.PCS, 10) || 1,
  }));

  const payload = {
    header: {
      ...header,
      PurchaseBillNo: header.InvoiceNo?.trim(),
      JobSheetNo: header.JobSheetNo?.trim(),
      PaymentType: header.Payable === 'Gold' ? 'G' : header.Payable === 'Both' ? 'B' : 'C',
    },
    details: mappedDetails,
  };

  const res = await fetch(`${API_URL}/directpurchase`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(payload),
  });
  return res.json();
};

const deletePurchaseBillApi = async (billNo: string) => {
  const res = await fetch(`${API_URL}/directpurchase/${encodeURIComponent(billNo)}`, {
    method: 'DELETE',
  });
  return res.json();
};

const closeJobSheetBillApi = async (jsNo: string) => {
  const res = await fetch(`${API_URL}/directpurchase/billclose`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ jobSheetNo: jsNo }),
  });
  return res.json();
};

export default function DirectPurchaseScreen() {
  const themeColors = useThemeColors();
  const { accentColor } = useThemeContext();
  const {
    background: backgroundColor,
    card: cardColor,
    text: textColor,
    border: borderColor,
    subtitle: subtitleColor,
    primaryGold: GOLD_COLOR
  } = themeColors;

  const router = useRouter();

  // Legacy Theme Colors matching VB6/VB.NET form directpurchase.txt
  const legacyYellow = '#FFBD00';
  const legacyBorder = '#CCA500';
  const legacyPanelBg = '#FFF4D6';

  const Checkbox = ({ label, selected, onPress }: { label: string; selected: boolean; onPress: () => void }) => (
    <TouchableOpacity style={styles.radioContainer} onPress={onPress}>
      <View
        style={[
          styles.checkboxSquare,
          { borderColor: borderColor, backgroundColor: selected ? '#9E6D08' : '#FFF' }
        ]}
      >
        {selected && <Ionicons name="checkmark" size={13} color="#FFF" />}
      </View>
      {label ? <ThemedText style={styles.radioLabel}>{label}</ThemedText> : null}
    </TouchableOpacity>
  );

  const Radio = ({ label, selected, onPress }: { label: string; selected: boolean; onPress: () => void }) => (
    <TouchableOpacity style={styles.radioContainer} onPress={onPress}>
      <View style={[styles.radioCircle, { borderColor: borderColor }]}>
        {selected && <View style={[styles.radioInnerCircle, { backgroundColor: '#9E6D08' }]} />}
      </View>
      {label ? <ThemedText style={styles.radioLabel}>{label}</ThemedText> : null}
    </TouchableOpacity>
  );

  // ===== STATE =====
  const [headerState, setHeaderState] = useState(getInitialHeaderState());
  const [details, setDetails] = useState<any[]>([]);

  // Detail item input state
  const [currentDetail, setCurrentDetail] = useState<any>(getInitialDetailState());

  // Auto / Manual toggle (F10 mode in directpurchase.txt)
  const [isManual, setIsManual] = useState(false);
  // Auto SMC Checkbox
  const [autoSMC, setAutoSMC] = useState(false);
  // Sales PCS Checkbox
  const [salesPCS, setSalesPCS] = useState(false);

  // Additional description sub-modal
  const [addlDescModalVisible, setAddlDescModalVisible] = useState(false);
  // Image preview modal
  const [imageModalVisible, setImageModalVisible] = useState(false);
  // Line Detail Entry Modal
  const [detailModalVisible, setDetailModalVisible] = useState(false);

  const [loading, setLoading] = useState(false);
  const [saving, setSaving] = useState(false);
  const [selectedIndex, setSelectedIndex] = useState<number | null>(null);

  // Toast notification
  const [toast, setToast] = useState({ visible: false, message: '', type: 'success' });
  const toastTimeoutRef = useRef<any>(null);

  const showToast = (message: string, type: 'success' | 'error' | 'info' = 'success') => {
    if (toastTimeoutRef.current) {
      clearTimeout(toastTimeoutRef.current);
    }
    setToast({ visible: true, message, type });
    toastTimeoutRef.current = setTimeout(() => {
      setToast({ visible: false, message: '', type: 'success' });
    }, 3500);
  };

  const showAlert = (title: string, message: string, buttons?: any[]) => {
    // If confirmation dialog with multiple action buttons
    if (buttons && buttons.length > 1) {
      if (Platform.OS === 'web') {
        const confirmed = window.confirm(`${title ? title + ':\n' : ''}${message}`);
        if (confirmed) {
          const actionBtn = buttons.find((b: any) => b.style !== 'cancel' && b.text !== 'Cancel');
          if (actionBtn && actionBtn.onPress) {
            actionBtn.onPress();
          }
        }
      } else {
        Alert.alert(title, message, buttons);
      }
      return;
    }

    // Replace all alert popups with Toast message
    const toastType: 'success' | 'error' | 'info' =
      title.toLowerCase().includes('error') ? 'error' :
      title.toLowerCase().includes('success') ? 'success' : 'info';
    showToast(message, toastType);
    if (buttons && buttons[0]?.onPress) {
      buttons[0].onPress();
    }
  };

  // LOV Modal State
  const [lovVisible, setLovVisible] = useState(false);
  const [lovTitle, setLovTitle] = useState('');
  const [lovData, setLovData] = useState<any[]>([]);
  const [filteredLovData, setFilteredLovData] = useState<any[]>([]);
  const [lovSearch, setLovSearch] = useState('');
  const [lovColumns, setLovColumns] = useState<string[]>([]);
  const [onLovSelect, setOnLovSelect] = useState<((item: any) => void) | null>(null);

  // Initial load: Fetch Company configuration (GST %, Default Currency, etc.)
  useEffect(() => {
    async function initCompany() {
      const comp = await fetchCompanyInfo();
      if (comp) {
        const gstVal = comp.GstPerc || comp.GSTPerc || comp.GSTPercent || comp.GstPercentage || comp.GST || '9';
        const currVal = comp.CurrencyCode || comp.DefaultCurrency || 'SGD';
        setHeaderState((prev: any) => ({
          ...prev,
          CurrencyCode: prev.CurrencyCode || currVal,
          GSTPercent: prev.GSTPercent === '0' && gstVal ? String(gstVal) : prev.GSTPercent || String(gstVal),
        }));
      }
    }
    initCompany();
  }, []);

  // Update Header field with automatic TEAL <-> CostPerGram conversions matching VB reference
  const updateHeader = (key: string, value: any) => {
    setHeaderState((prev: any) => {
      const next = { ...prev, [key]: value };

      if (key === 'TEAL' || key === 'TEALPercent') {
        const costG = calculateCostPerGramFromTeal(
          key === 'TEAL' ? value : next.TEAL,
          key === 'TEALPercent' ? value : next.TEALPercent,
          next.GSTActive,
          next.GSTPercent
        );
        if (parseFloat(costG) > 0) {
          next.CostPerGram = costG;
        }
      } else if (key === 'CostPerGram') {
        const teal = calculateTealFromCostPerGram(
          value,
          next.TEALPercent,
          next.GSTActive,
          next.GSTPercent
        );
        if (parseFloat(teal) > 0) {
          next.TEAL = teal;
        }
      } else if (key === 'GSTActive') {
        const costG = calculateCostPerGramFromTeal(
          next.TEAL,
          next.TEALPercent,
          value,
          next.GSTPercent
        );
        if (parseFloat(costG) > 0) {
          next.CostPerGram = costG;
        }
      }

      return next;
    });
  };

  // Update Detail line item input with instant recalculation
  const updateDetail = (keyOrUpdates: string | Record<string, any>, value?: any) => {
    setCurrentDetail((prev: any) => {
      let next = typeof keyOrUpdates === 'string' ? { ...prev, [keyOrUpdates]: value } : { ...prev, ...keyOrUpdates };
      if (next.CategoryCode && !next.ClassCode) {
        next.ClassCode = next.CategoryCode.toString().substring(0, 3);
      }
      return recalculateDetailCosts(next, autoSMC, headerState.CurrencyRate);
    });
  };

  // Recalculate detail on autoSMC toggle or currency rate changes
  useEffect(() => {
    setCurrentDetail((prev: any) => recalculateDetailCosts(prev, autoSMC, headerState.CurrencyRate));
  }, [autoSMC, headerState.CurrencyRate]);

  // Recalculate Header Summary whenever line details or Header GST (chkHGST) settings change
  useEffect(() => {
    const summary = calculateHeaderSummary(details, headerState.HGSTActive, headerState.GSTPercent);
    setHeaderState((prev: any) => ({
      ...prev,
      TotalCost: summary.TotalCost,
      TotalMC: summary.TotalMC,
      GSTAmount: summary.GSTAmount,
      Weight: summary.Weight,
      GoldLoss: summary.GoldLoss,
    }));
  }, [details, headerState.HGSTActive, headerState.GSTPercent]);

  // LOV Handlers
  const openLov = (title: string, data: any[], columns: string[], onSelect: (item: any) => void) => {
    setLovTitle(title);
    setLovData(data);
    setFilteredLovData(data);
    setLovColumns(columns);
    setOnLovSelect(() => (item: any) => {
      onSelect(item);
      setLovVisible(false);
    });
    setLovSearch('');
    setLovVisible(true);
  };

  const handleLovSearch = (text: string) => {
    setLovSearch(text);
    if (!text.trim()) {
      setFilteredLovData(lovData);
      return;
    }
    const lower = text.toLowerCase();
    const filtered = lovData.filter((item) =>
      Object.values(item).some((val) =>
        String(val ?? '').toLowerCase().includes(lower)
      )
    );
    setFilteredLovData(filtered);
  };

  const handleLovSelect = (item: any) => {
    if (onLovSelect) {
      onLovSelect(item);
    }
  };

  // Data Fetchers
  const fetchSuppliers = async () => {
    try {
      setLoading(true);
      const json = await fetchSuppliersApi();
      setLoading(false);
      if (json.success && Array.isArray(json.data)) {
        openLov('Select Supplier', json.data, ['SupplierCode', 'SupplierName'], (item) => {
          updateHeader('SupplierCode', item.SupplierCode || '');
          updateHeader('SupplierName', item.SupplierName || '');
        });
      }
    } catch (e: any) {
      setLoading(false);
      showAlert('Error', e.message || 'Error fetching suppliers');
    }
  };

  const fetchCurrencies = async () => {
    try {
      setLoading(true);
      const json = await fetchCurrenciesApi();
      setLoading(false);
      if (json.success && Array.isArray(json.data)) {
        openLov('Select Currency', json.data, ['CurrencyCode', 'CurrencyName', 'Rate'], (item) => {
          updateHeader('CurrencyCode', item.CurrencyCode || 'SGD');
          updateHeader('CurrencyName', item.CurrencyName || item.CurrencyCode || '');
          updateHeader('CurrencyRate', (item.Rate || '1.00').toString());
        });
      }
    } catch (e: any) {
      setLoading(false);
      showAlert('Error', e.message || 'Error fetching currencies');
    }
  };

  const fetchTaelPercent = async () => {
    try {
      setLoading(true);
      const json = await fetchTaelPercentsApi();
      setLoading(false);
      if (json.success && Array.isArray(json.data) && json.data.length > 0) {
        openLov('Select TEAL %', json.data, ['TaelPercent', 'TaelPercent1'], (item) => {
          updateHeader('TEALPercent', (item.TaelPercent || item.TaelPercent1 || '0.92').toString());
        });
      } else {
        // Fallback default list from directpurchase.txt cboTealPercent1
        const fallback = [
          { TaelPercent: '.92' },
          { TaelPercent: '.925' },
          { TaelPercent: '.93' },
          { TaelPercent: '.935' },
          { TaelPercent: '.999' }
        ];
        openLov('Select TEAL %', fallback, ['TaelPercent'], (item) => {
          updateHeader('TEALPercent', item.TaelPercent);
        });
      }
    } catch (e: any) {
      setLoading(false);
      showAlert('Error', e.message || 'Error fetching TEAL percentages');
    }
  };

  const fetchCatalogues = async () => {
    try {
      setLoading(true);
      const [catlgRes, catRes] = await Promise.all([
        fetchCataloguesApi(),
        fetchCategoriesApi(),
      ]);
      setLoading(false);
      const categoriesList = catRes && catRes.success && Array.isArray(catRes.data) ? catRes.data : [];

      if (catlgRes.success && Array.isArray(catlgRes.data)) {
        openLov(
          'Select Catalogue',
          catlgRes.data,
          ['CatalogueCode', 'CatalogueName', 'CategoryCode', 'TrayId'],
          (item) => {
            const trimmedItemCat = (item.CategoryCode || '').trim();
            const catCode = trimmedItemCat || (currentDetail.CategoryCode || '').trim();
            const matchedCategory = categoriesList.find(
              (c: any) => (c.CategoryCode || '').trim().toUpperCase() === catCode.toUpperCase()
            );

            const updates: any = {
              CatalogueCode: (item.CatalogueCode || '').trim(),
              CategoryCode: catCode,
              TrayId: (item.TrayId || currentDetail.TrayId || '').trim(),
              InventoryDescription: item.CatalogueName || currentDetail.InventoryDescription,
            };

            if (matchedCategory) {
              if (matchedCategory.ClassCode) {
                updates.ClassCode = matchedCategory.ClassCode.trim();
              } else if (catCode) {
                updates.ClassCode = catCode.substring(0, 3);
              }
              if (!isManual && matchedCategory.NextInventoryCode) {
                updates.InventoryCode = matchedCategory.NextInventoryCode.trim();
              }
            } else if (catCode) {
              updates.ClassCode = catCode.substring(0, 3);
            }

            updateDetail(updates);
          }
        );
      }
    } catch (e: any) {
      setLoading(false);
      showAlert('Error', e.message || 'Error fetching catalogues');
    }
  };

  const fetchCategories = async () => {
    try {
      setLoading(true);
      const json = await fetchCategoriesApi();
      setLoading(false);
      if (json.success && Array.isArray(json.data)) {
        openLov(
          'Select Category',
          json.data,
          ['CategoryCode', 'CategoryName', 'ClassCode', 'NextInventoryCode'],
          (item) => {
            const updates: any = {
              CategoryCode: (item.CategoryCode || '').trim(),
              ClassCode: (item.ClassCode || (item.CategoryCode ? item.CategoryCode.substring(0, 3) : '')).trim(),
            };
            if (!isManual && item.NextInventoryCode) {
              updates.InventoryCode = (item.NextInventoryCode || '').trim();
            }
            updateDetail(updates);
          }
        );
      }
    } catch (e: any) {
      setLoading(false);
      showAlert('Error', e.message || 'Error fetching categories');
    }
  };

  const fetchSizes = async () => {
    try {
      setLoading(true);
      const json = await fetchSizesApi();
      setLoading(false);
      if (json.success && Array.isArray(json.data)) {
        openLov('Select Size', json.data, ['SizeCode', 'SizeDescription'], (item) => {
          updateDetail('SizeCode', item.SizeCode || '');
        });
      }
    } catch (e: any) {
      setLoading(false);
      showAlert('Error', e.message || 'Error fetching sizes');
    }
  };

  const fetchTrays = async () => {
    try {
      setLoading(true);
      const json = await fetchTraysApi();
      setLoading(false);
      if (json.success && Array.isArray(json.data)) {
        openLov('Select Tray', json.data, ['TrayId', 'TrayName', 'LocationCode'], (item) => {
          updateDetail('TrayId', item.TrayId || '');
        });
      }
    } catch (e: any) {
      setLoading(false);
      showAlert('Error', e.message || 'Error fetching trays');
    }
  };

  const fetchRefiners = async () => {
    try {
      setLoading(true);
      const json = await fetchRefinersApi();
      setLoading(false);
      if (json.success && Array.isArray(json.data)) {
        openLov('Select Refiner', json.data, ['RefinerCode', 'RefinerName'], (item) => {
          updateDetail('IPMRefiner', item.RefinerName || item.RefinerCode || '');
        });
      }
    } catch (e: any) {
      setLoading(false);
      showAlert('Error', e.message || 'Error fetching refiners');
    }
  };

  const fetchCountries = async () => {
    try {
      setLoading(true);
      const json = await fetchCountriesApi();
      setLoading(false);
      if (json.success && Array.isArray(json.data)) {
        openLov('Select Country', json.data, ['CountryCode', 'CountryName'], (item) => {
          updateDetail('IPMCountry', item.CountryName || item.CountryCode || '');
        });
      }
    } catch (e: any) {
      setLoading(false);
      showAlert('Error', e.message || 'Error fetching countries');
    }
  };

  const fetchTradeInSearch = async () => {
    try {
      setLoading(true);
      const json = await fetchInventoryApi();
      setLoading(false);
      if (json.success && Array.isArray(json.data)) {
        openLov('Search Trade In Inventory', json.data, ['InventoryCode', 'Description', 'Weight', 'InvoiceNo'], (item) => {
          updateDetail({
            TradeInInventoryCode: item.InventoryCode || '',
            TradeInDescription: item.Description || '',
            TradeInInvoice: item.InvoiceNo || '',
            TradeInWeight: (item.Weight || '').toString(),
          });
        });
      }
    } catch (e: any) {
      setLoading(false);
      showAlert('Error', e.message || 'Error searching trade-in inventory');
    }
  };

  const loadJobSheetByNo = async (jsNo: string) => {
    const trimmed = (jsNo || '').trim();
    if (!trimmed) return;
    try {
      setLoading(true);
      const jsJson = await fetchJobSheetByNoApi(trimmed);
      setLoading(false);
      if (jsJson.success && jsJson.header) {
        const h = jsJson.header;
        const isClosed = (h.BillClose || '').toUpperCase() === 'Y';
        const tranType = (h.TranType || '').toUpperCase();

        if (isClosed) {
          showAlert('Warning', 'This bill is closed! Cannot modify!');
        } else if (tranType && tranType !== 'DP') {
          showAlert('Warning', 'This is not a direct purchase bill. Cannot modify!');
        }

        setHeaderState((prev: any) => ({
          ...prev,
          JobSheetNo: h.JobSheetNo || trimmed,
          InvoiceNo: h.PurchaseBillNo || prev.InvoiceNo,
          InvoiceDate: h.PurchaseBillDate ? h.PurchaseBillDate.split('T')[0] : prev.InvoiceDate,
          SupplierCode: h.SupplierCode || prev.SupplierCode,
          SupplierName: h.SupplierName || prev.SupplierName,
          CurrencyCode: h.CurrencyCode || prev.CurrencyCode,
          CurrencyName: h.CurrencyName || prev.CurrencyName,
          CurrencyRate: h.CurrencyRate?.toString() || prev.CurrencyRate,
          GSTPercent: h.GST?.toString() || prev.GSTPercent,
          GSTActive: (h.TaelGST || '').toUpperCase() === 'Y',
          HGSTActive: parseFloat(h.GST || 0) > 0,
          CostPerGram: h.CostPerGram?.toString() || prev.CostPerGram,
          TotalCost: h.TotalCost?.toString() || prev.TotalCost,
          TotalMC: h.TotalMC?.toString() || prev.TotalMC,
          Weight: h.TotalWeight24K?.toString() || prev.Weight,
          GoldLoss: h.TotalGoldLoss?.toString() || prev.GoldLoss,
          Remarks: h.Remarks || prev.Remarks,
          BillClose: isClosed ? 'Y' : 'N',
          TranType: tranType || 'DP',
          TotalWeight24K: (h.TotalWeight24K || 0).toString(),
          TEAL: (h.TaelRate || 0).toString(),
          TEALPercent: (h.TaelPercent || 0).toString(),
          Payable: h.PaymentType === 'G' ? 'Gold' : h.PaymentType === 'C' ? 'Cash' : 'Both',
        }));

        if (jsJson.details && jsJson.details.length > 0) {
          const mappedDetails = jsJson.details.map((d: any) => ({
            CatalogueCode: d.CatalogueCode || '',
            CategoryCode: d.CategoryCode || '',
            TrayId: d.TrayId || '',
            CatalogueWeight: '',
            CataloguePCS: '',
            PurchaseMCType: d.PMCType === 'P' ? 'PerPCS' : 'PerGram',
            SalesMCType: d.SMCType === 'P' ? 'PerPCS' : 'PerGram',
            Auto: true,
            PCSCheck: false,
            InventoryCode: d.NextInventoryCode || d.InventoryCode || '',
            InventoryDescription: d.Description || '',
            Weight: d.Weight?.toString() || '',
            SizeCode: d.SizeCode?.toString() || d.Size?.toString() || '',
            GoldLoss: d.GoldLoss?.toString() || '',
            CostPerGram: d.CostPerGram?.toString() || '',
            PMC: d.PMC?.toString() || d.MC?.toString() || '',
            SMC: d.SMC?.toString() || '0',
            PCS: d.PCS?.toString() || '1',
            TotalCost: d.TotalCost?.toString() || '',
            ClassCode: d.ClassCode || (d.CategoryCode ? d.CategoryCode.toString().substring(0, 3) : ''),
            NetWeight: parseFloat(d.Weight || 0) > 0
              ? (parseFloat(d.Weight || 0) - parseFloat(d.GoldLoss || 0)).toFixed(3)
              : '',
            DiamondDescription: d.DiamondDescription || '',
            StoneWeight: (d.StoneWeight || 0).toString(),
            IPM: false,
            AddlDesc: false,
          }));
          setDetails(mappedDetails);
        } else {
          setDetails([]);
        }
      } else {
        showAlert('Error', 'Job sheet no. is invalid!');
        handleClearForm();
      }
    } catch (e: any) {
      setLoading(false);
      showAlert('Error', e.message || 'Failed to load JobSheet details');
    }
  };

  const fetchJobSheet = async () => {
    try {
      setLoading(true);
      const json = await fetchJobSheetsApi();
      setLoading(false);
      if (json.success && Array.isArray(json.data)) {
        openLov(
          'Select Job Sheet',
          json.data,
          ['JobSheetNo', 'SupplierCode', 'PurchaseBillNo', 'TotalWeight24K'],
          async (item) => {
            loadJobSheetByNo(item.JobSheetNo);
          }
        );
      }
    } catch (e: any) {
      setLoading(false);
      showAlert('Error', e.message || 'Error fetching jobsheet');
    }
  };

  const fetchPurchase = async () => {
    try {
      setLoading(true);
      const json = await fetchPurchasesApi();
      setLoading(false);
      if (json.success && Array.isArray(json.data)) {
        openLov('Search Direct Purchase', json.data, ['PurchaseBillNo', 'SupplierCode', 'JobSheetNo'], async (item) => {
          try {
            setLoading(true);
            const pJson = await fetchPurchaseByBillNoApi(item.PurchaseBillNo);
            setLoading(false);
            if (pJson.success) {
              const h = pJson.header;
              setHeaderState((prev: any) => ({
                ...prev,
                JobSheetNo: h.JobSheetNo || '',
                InvoiceNo: h.PurchaseBillNo || '',
                SupplierCode: h.SupplierCode || '',
                SupplierName: h.SupplierName || '',
                CurrencyCode: h.CurrencyCode || 'SGD',
                CurrencyRate: h.CurrencyRate?.toString() || '1.00',
                Remarks: h.Remarks || '',
              }));

              if (pJson.details && pJson.details.length > 0) {
                const mappedDetails = pJson.details.map((d: any) => ({
                  CatalogueCode: d.CatalogueCode || '',
                  CategoryCode: d.CategoryCode || '',
                  TrayId: d.TrayId || '',
                  CatalogueWeight: '',
                  CataloguePCS: '',
                  PurchaseMCType: d.PurchaseType === 'P' ? 'PerPCS' : 'PerGram',
                  SalesMCType: 'PerPCS',
                  Auto: true,
                  PCSCheck: false,
                  InventoryCode: d.InventoryCode || '',
                  InventoryDescription: d.Description || '',
                  Weight: d.Weight?.toString() || '',
                  SizeCode: d.Size?.toString() || '',
                  GoldLoss: d.GoldLoss?.toString() || '',
                  CostPerGram: d.CostPerGram?.toString() || '',
                  PMC: d.MC?.toString() || '',
                  SMC: '0',
                  PCS: d.PCS?.toString() || '1',
                  TotalCost: d.TotalCost?.toString() || '',
                  ClassCode: d.ClassCode || '',
                  NetWeight: parseFloat(d.Weight || 0) > 0 ? (parseFloat(d.Weight || 0) - parseFloat(d.GoldLoss || 0)).toFixed(3) : '',
                  DiamondDescription: d.DiamondDescription || '',
                  StoneWeight: (d.StoneWeight || 0).toString(),
                  IPM: false,
                  AddlDesc: false,
                }));
                setDetails(mappedDetails);
              }
            }
          } catch (err: any) {
            setLoading(false);
            showAlert('Error', err.message || 'Failed to load purchase details');
          }
        });
      }
    } catch (e: any) {
      setLoading(false);
      showAlert('Error', e.message || 'Error fetching purchases');
    }
  };

  // Line Item Add / Update / Delete / Clear
  const handleAddOrUpdateDetail = () => {
    // Sync CostPerGram from header if left blank
    const itemToValidate = {
      ...currentDetail,
      CostPerGram: currentDetail.CostPerGram || headerState.CostPerGram || '0.00',
    };

    const validation = validateInventoryLine(itemToValidate, headerState, salesPCS);
    if (!validation.isValid) {
      showToast(validation.message, 'error');
      return;
    }

    const calculated = recalculateDetailCosts(itemToValidate, autoSMC, headerState.CurrencyRate);

    if (selectedIndex !== null) {
      const updated = [...details];
      updated[selectedIndex] = calculated;
      setDetails(updated);
      setSelectedIndex(null);
      showToast('Line item updated successfully');
    } else {
      setDetails([...details, calculated]);
      showToast('Line item added successfully');
    }

    setCurrentDetail(getInitialDetailState());
    setDetailModalVisible(false);
  };

  const handleEditDetail = (index: number) => {
    setSelectedIndex(index);
    setCurrentDetail({ ...details[index] });
    setDetailModalVisible(true);
  };

  const handleDeleteDetail = () => {
    if (selectedIndex === null) {
      showToast('Please select a line item from the grid to delete', 'info');
      return;
    }
    const updated = details.filter((_, i) => i !== selectedIndex);
    setDetails(updated);
    setSelectedIndex(null);
    setCurrentDetail(getInitialDetailState());
    showToast('Line item removed');
  };

  const handleClearDetail = () => {
    setSelectedIndex(null);
    setCurrentDetail(getInitialDetailState());
  };

  const handleClearForm = () => {
    setHeaderState(getInitialHeaderState());
    setDetails([]);
    setCurrentDetail(getInitialDetailState());
    setSelectedIndex(null);
    showToast('Form cleared', 'info');
  };

  // Full Purchase Save
  const handleSavePurchase = async () => {
    const validation = validatePurchaseSave(headerState, details);
    if (!validation.isValid) {
      showToast(validation.message, 'error');
      return;
    }

    try {
      setSaving(true);
      let jsNo = (headerState.JobSheetNo || '').trim();
      if (!jsNo) {
        jsNo = await fetchNextJobSheetNoApi();
        if (jsNo) {
          setHeaderState((prev: any) => ({ ...prev, JobSheetNo: jsNo }));
        }
      }

      const saveHeader = { ...headerState, JobSheetNo: jsNo || headerState.JobSheetNo };
      const res = await saveDirectPurchaseApi(saveHeader, details);
      setSaving(false);

      if (res.success) {
        showToast('Direct purchase saved successfully!', 'success');
      } else {
        showAlert('Error', res.message || 'Failed to save direct purchase');
      }
    } catch (e: any) {
      setSaving(false);
      showAlert('Error', e.message || 'Error occurred while saving direct purchase');
    }
  };

  // Bill Close Operation
  const handleBillClose = async () => {
    if (!headerState.JobSheetNo?.trim()) {
      showToast('Please enter or load a Job Sheet No. first', 'info');
      return;
    }

    showAlert('Confirmation', `Are you sure you want to close Job Sheet ${headerState.JobSheetNo}?`, [
      { text: 'Cancel', style: 'cancel' },
      {
        text: 'Close Bill',
        onPress: async () => {
          try {
            setLoading(true);
            const res = await closeJobSheetBillApi(headerState.JobSheetNo);
            setLoading(false);
            if (res.success) {
              setHeaderState((prev: any) => ({ ...prev, BillClose: 'Y' }));
              showToast('Bill successfully closed!');
            } else {
              showAlert('Error', res.message || 'Failed to close bill');
            }
          } catch (e: any) {
            setLoading(false);
            showAlert('Error', e.message || 'Error closing bill');
          }
        },
      },
    ]);
  };

  const isFormLocked = headerState.BillClose === 'Y';

  return (
    <SafeAreaView style={{ flex: 1, backgroundColor: '#FBF9F5' }}>
      <Stack.Screen
        options={{
          headerTitle: 'Direct Purchase',
          headerStyle: { backgroundColor: '#FFFFFF' },
          headerTintColor: '#3B2D17',
          headerShadowVisible: false,
          headerRight: () => (
            <View style={{ flexDirection: 'row', alignItems: 'center', paddingRight: 8 }}>
              {/* Auto / Manual Mode Pill Button */}
              <TouchableOpacity
                style={[
                  styles.topActionBtn,
                  {
                    backgroundColor: isManual ? '#FDE8E8' : '#E8F5E9',
                    borderColor: isManual ? '#D32F2F' : '#388E3C'
                  }
                ]}
                onPress={() => {
                  setIsManual(!isManual);
                  showToast(isManual ? 'Switched to AUTO mode' : 'Switched to MANUAL mode', 'info');
                }}
              >
                <ThemedText
                  style={{
                    color: isManual ? '#C62828' : '#2E7D32',
                    fontWeight: 'bold',
                    fontSize: 11
                  }}
                >
                  {isManual ? 'MANUAL' : 'AUTO'}
                </ThemedText>
              </TouchableOpacity>

              {/* Bill Close Button (F7) */}
              <TouchableOpacity
                style={[
                  styles.topActionBtn,
                  {
                    backgroundColor: headerState.BillClose === 'Y' ? '#E0E0E0' : '#FBE9E7',
                    borderColor: '#FF7043'
                  }
                ]}
                onPress={handleBillClose}
                disabled={headerState.BillClose === 'Y'}
              >
                <ThemedText style={{ color: '#D84315', fontWeight: 'bold', fontSize: 11 }}>
                  {headerState.BillClose === 'Y' ? 'Closed' : 'BillClose'}
                </ThemedText>
              </TouchableOpacity>

              {/* Clear Button (F6) */}
              <TouchableOpacity style={styles.topActionBtn} onPress={handleClearForm}>
                <ThemedText style={{ color: '#5C4A28', fontWeight: 'bold', fontSize: 11 }}>Clear</ThemedText>
              </TouchableOpacity>

              {/* Save Button (F8) */}
              <TouchableOpacity
                style={[
                  styles.topActionBtn,
                  { backgroundColor: '#F9F2E2', borderColor: '#B8903B' }
                ]}
                onPress={handleSavePurchase}
                disabled={saving || isFormLocked}
              >
                {saving ? (
                  <ActivityIndicator size="small" color="#9E6D08" />
                ) : (
                  <ThemedText style={{ color: '#9E6D08', fontWeight: 'bold', fontSize: 11 }}>Save</ThemedText>
                )}
              </TouchableOpacity>
            </View>
          ),
        }}
      />

      <ScrollView style={styles.container} contentContainerStyle={styles.contentContainer}>
        {/* ================= TOP SECTION: JOBSHEET DETAILS & SUMMARY ================= */}
        <ScrollView horizontal={true} showsHorizontalScrollIndicator={true} style={{ marginBottom: 14 }} contentContainerStyle={{ width: '100%' }}>
          <View style={{ flexDirection: 'row', width: '100%', minWidth: 1120 }}>
            {/* LEFT: JOB SHEET DETAILS */}
            <View style={[styles.goldCard, { flex: 7, minWidth: 740, marginRight: 14 }]}>
              <View style={styles.cardHeaderBanner}>
                <View style={styles.headerIconBadge}>
                  <Ionicons name="document-text" size={15} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitle}>Direct Purchase JobSheet Details</ThemedText>
                {isFormLocked && (
                  <View style={styles.closedBadge}>
                    <ThemedText style={styles.closedBadgeText}>CLOSED</ThemedText>
                  </View>
                )}
              </View>

              <View style={styles.cardBody}>
                {/* Row 1: JobSheet No, Invoice No, Invoice Date */}
                <View style={styles.row}>
                  <ThemedText style={styles.label}>JobSheet No.</ThemedText>
                  <View style={{ width: 140, flexDirection: 'row' }}>
                    <TextInput
                      style={[styles.input, { width: 106, borderTopRightRadius: 0, borderBottomRightRadius: 0, borderRightWidth: 0 }]}
                      value={headerState.JobSheetNo}
                      onChangeText={(t) => updateHeader('JobSheetNo', t)}
                      onBlur={() => {
                        if (headerState.JobSheetNo) loadJobSheetByNo(headerState.JobSheetNo);
                      }}
                      editable={!isFormLocked}
                    />
                    <TouchableOpacity
                      style={styles.lookupBtn}
                      onPress={fetchJobSheet}
                      disabled={isFormLocked}
                    >
                      <ThemedText style={styles.lookupBtnText}>...</ThemedText>
                    </TouchableOpacity>
                  </View>

                  <ThemedText style={[styles.labelRight, { width: 84 }]}>Invoice No</ThemedText>
                  <View style={{ flexDirection: 'row', flex: 1, minWidth: 120 }}>
                    <TextInput
                      style={[styles.input, { flex: 1, borderTopRightRadius: 0, borderBottomRightRadius: 0 }]}
                      value={headerState.InvoiceNo}
                      onChangeText={(t) => updateHeader('InvoiceNo', t)}
                      editable={!isFormLocked}
                    />
                    <TouchableOpacity
                      style={[styles.lookupBtn, { width: 28 }]}
                      onPress={fetchPurchase}
                      disabled={isFormLocked}
                    >
                      <Ionicons name="search" size={13} color="#6E5528" />
                    </TouchableOpacity>
                  </View>

                  <ThemedText style={[styles.labelRight, { width: 92 }]}>Invoice Date</ThemedText>
                  <TextInput
                    style={[styles.input, { width: 110, textAlign: 'center' }]}
                    value={headerState.InvoiceDate}
                    onChangeText={(t) => updateHeader('InvoiceDate', t)}
                    placeholder="YYYY-MM-DD"
                    editable={!isFormLocked}
                  />
                </View>

                {/* Row 2: Supplier Code, Supplier Name */}
                <View style={styles.row}>
                  <ThemedText style={styles.label}>Supplier Code</ThemedText>
                  <View style={{ width: 140, flexDirection: 'row' }}>
                    <TextInput
                      style={[styles.input, { width: 106, borderTopRightRadius: 0, borderBottomRightRadius: 0, borderRightWidth: 0 }]}
                      value={headerState.SupplierCode}
                      onChangeText={(t) => updateHeader('SupplierCode', t)}
                      editable={!isFormLocked}
                    />
                    <TouchableOpacity
                      style={styles.lookupBtn}
                      onPress={fetchSuppliers}
                      disabled={isFormLocked}
                    >
                      <ThemedText style={styles.lookupBtnText}>...</ThemedText>
                    </TouchableOpacity>
                  </View>
                  <TextInput
                    style={[styles.input, styles.readonlyInput, { flex: 1, minWidth: 338, marginLeft: 8 }]}
                    value={headerState.SupplierName}
                    editable={false}
                  />
                </View>

                {/* Row 3: Currency, Rate */}
                <View style={styles.row}>
                  <ThemedText style={styles.label}>Currency</ThemedText>
                  <View style={{ width: 140, flexDirection: 'row' }}>
                    <TextInput
                      style={[styles.input, { width: 106, borderTopRightRadius: 0, borderBottomRightRadius: 0, borderRightWidth: 0 }]}
                      value={headerState.CurrencyCode}
                      onChangeText={(t) => updateHeader('CurrencyCode', t)}
                      editable={!isFormLocked}
                    />
                    <TouchableOpacity style={styles.lookupBtn} onPress={fetchCurrencies}>
                      <ThemedText style={styles.lookupBtnText}>...</ThemedText>
                    </TouchableOpacity>
                  </View>

                  <TextInput
                    style={[styles.input, styles.readonlyInput, { flex: 1, minWidth: 130, marginLeft: 8 }]}
                    value={headerState.CurrencyName}
                    editable={false}
                  />

                  <ThemedText style={[styles.labelRight, { width: 70 }]}>Rate</ThemedText>
                  <TextInput
                    style={[styles.input, { width: 90, textAlign: 'right' }]}
                    value={headerState.CurrencyRate}
                    onChangeText={(t) => updateHeader('CurrencyRate', t)}
                    keyboardType="numeric"
                    editable={!isFormLocked}
                  />
                </View>

                {/* Row 4: Payable, GST, TEAL %, TEAL, Cost/g */}
                <View style={[styles.row, { alignItems: 'center' }]}>
                  <ThemedText style={styles.label}>Payable</ThemedText>
                  <View style={{ flexDirection: 'row', alignItems: 'center', marginRight: 10, gap: 4 }}>
                    <Radio
                      label="Gold"
                      selected={headerState.Payable === 'Gold'}
                      onPress={() => updateHeader('Payable', 'Gold')}
                    />
                    <Radio
                      label="Cash"
                      selected={headerState.Payable === 'Cash'}
                      onPress={() => updateHeader('Payable', 'Cash')}
                    />
                    <Radio
                      label="Both"
                      selected={headerState.Payable === 'Both'}
                      onPress={() => updateHeader('Payable', 'Both')}
                    />
                  </View>

                  <ThemedText style={{ marginRight: 6, color: '#3B3020', fontWeight: 'bold', fontSize: 11 }}>GST</ThemedText>
                  <Checkbox
                    label=""
                    selected={headerState.GSTActive}
                    onPress={() => updateHeader('GSTActive', !headerState.GSTActive)}
                  />

                  {/* TEAL % */}
                  <View style={{ flexDirection: 'row', marginLeft: 10, alignItems: 'center' }}>
                    <ThemedText style={{ color: '#4A3B25', fontSize: 11, marginRight: 5, fontWeight: 'bold' }}>TEAL %</ThemedText>
                    <TextInput
                      style={[styles.input, { width: 50, textAlign: 'center', borderTopRightRadius: 0, borderBottomRightRadius: 0 }]}
                      value={headerState.TEALPercent}
                      onChangeText={(t) => updateHeader('TEALPercent', t)}
                      keyboardType="numeric"
                    />
                    <TouchableOpacity style={[styles.lookupBtn, { width: 24 }]} onPress={fetchTaelPercent}>
                      <ThemedText style={styles.lookupBtnText}>...</ThemedText>
                    </TouchableOpacity>

                    <ThemedText style={{ color: '#4A3B25', fontSize: 11, marginLeft: 8, marginRight: 5, fontWeight: 'bold' }}>TEAL</ThemedText>
                    <TextInput
                      style={[styles.input, { width: 62, textAlign: 'right' }]}
                      value={headerState.TEAL}
                      onChangeText={(t) => updateHeader('TEAL', t)}
                      keyboardType="numeric"
                    />

                    <ThemedText style={{ color: '#4A3B25', fontSize: 11, marginLeft: 8, marginRight: 5, fontWeight: 'bold' }}>Cost/g</ThemedText>
                    <TextInput
                      style={[styles.input, { width: 66, textAlign: 'right' }]}
                      value={headerState.CostPerGram}
                      onChangeText={(t) => updateHeader('CostPerGram', t)}
                      keyboardType="numeric"
                    />
                  </View>
                </View>

                {/* Row 5: Remarks, Fix / UnFix, Ordered */}
                <View style={styles.row}>
                  <ThemedText style={styles.label}>Remarks</ThemedText>
                  <TextInput
                    style={[styles.input, { flex: 1, minWidth: 240 }]}
                    value={headerState.Remarks}
                    onChangeText={(t) => updateHeader('Remarks', t)}
                  />

                  {/* Fix / UnFix Pill Box matching purchase.txt style */}
                  <View style={styles.pillBox}>
                    <Radio
                      label="Fix"
                      selected={headerState.FixUnFix === 'Fix'}
                      onPress={() => updateHeader('FixUnFix', 'Fix')}
                    />
                    <Radio
                      label="UnFix"
                      selected={headerState.FixUnFix === 'UnFix'}
                      onPress={() => updateHeader('FixUnFix', 'UnFix')}
                    />
                  </View>

                  <View style={{ flexDirection: 'row', alignItems: 'center', marginLeft: 8 }}>
                    <Checkbox
                      label="Ordered"
                      selected={headerState.Ordered}
                      onPress={() => updateHeader('Ordered', !headerState.Ordered)}
                    />
                    {headerState.Ordered && (
                      <TextInput
                        style={[styles.input, { width: 80, marginLeft: 4 }]}
                        placeholder="Order No"
                        value={headerState.OrderNo}
                        onChangeText={(t) => updateHeader('OrderNo', t)}
                      />
                    )}
                  </View>
                </View>
              </View>
            </View>

            {/* RIGHT: SUMMARY CARD (matching purchase.tsx goldCard right summary) */}
            <View style={[styles.goldCard, { flex: 3.4, minWidth: 350 }]}>
              <View style={styles.cardHeaderBanner}>
                <View style={styles.headerIconBadge}>
                  <Ionicons name="calculator-outline" size={15} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitle}>Purchase Summary</ThemedText>
              </View>

              <View style={[styles.cardBody, { justifyContent: 'space-around', flex: 1 }]}>
                <View style={styles.row}>
                  <ThemedText style={[styles.label, { width: 90 }]}>Total Cost</ThemedText>
                  <TextInput
                    style={[styles.input, styles.readonlyInput, { flex: 1, textAlign: 'right', fontWeight: 'bold' }]}
                    value={headerState.TotalCost}
                    editable={false}
                  />
                </View>

                <View style={styles.row}>
                  <ThemedText style={[styles.label, { width: 90 }]}>Total MC</ThemedText>
                  <TextInput
                    style={[styles.input, styles.readonlyInput, { flex: 1, textAlign: 'right', fontWeight: 'bold' }]}
                    value={headerState.TotalMC}
                    editable={false}
                  />
                </View>

                <View style={styles.row}>
                  <View style={{ width: 90, flexDirection: 'row', alignItems: 'center' }}>
                    <ThemedText style={[styles.label, { width: undefined, marginRight: 6 }]}>
                      GST{parseFloat(headerState.GSTPercent) > 0 ? ` (${headerState.GSTPercent}%)` : ''}
                    </ThemedText>
                    <Checkbox
                      label=""
                      selected={headerState.HGSTActive}
                      onPress={() => updateHeader('HGSTActive', !headerState.HGSTActive)}
                    />
                  </View>
                  <TextInput
                    style={[styles.input, styles.readonlyInput, { flex: 1, textAlign: 'right', fontWeight: 'bold' }]}
                    value={headerState.GSTAmount}
                    editable={false}
                  />
                </View>

                <View style={styles.row}>
                  <ThemedText style={[styles.label, { width: 90 }]}>Weight</ThemedText>
                  <TextInput
                    style={[styles.input, styles.readonlyInput, { flex: 1, textAlign: 'right', fontWeight: 'bold' }]}
                    value={headerState.Weight}
                    editable={false}
                  />
                </View>

                <View style={styles.row}>
                  <ThemedText style={[styles.label, { width: 90 }]}>Gold Loss</ThemedText>
                  <View style={{ flexDirection: 'row', flex: 1 }}>
                    <TextInput
                      style={[styles.input, styles.readonlyInput, { flex: 1, textAlign: 'right', fontWeight: 'bold' }]}
                      value={headerState.GoldLoss}
                      editable={false}
                    />
                    <TouchableOpacity
                      style={[styles.lookupBtn, { width: 44, marginLeft: 4 }]}
                      onPress={() => router.push('/goldbarfix' as any)}
                    >
                      <ThemedText style={styles.lookupBtnText}>Fix</ThemedText>
                    </TouchableOpacity>
                  </View>
                </View>
              </View>
            </View>
          </View>
        </ScrollView>

        {/* ================= MIDDLE SECTION: DATA GRID (grdPurchase) ================= */}
        <View style={[styles.goldCard, { marginBottom: 14 }]}>
          <View style={[styles.cardHeaderBanner, { justifyContent: 'space-between' }]}>
            <View style={{ flexDirection: 'row', alignItems: 'center' }}>
              <View style={styles.headerIconBadge}>
                <Ionicons name="list" size={15} color="#B48218" />
              </View>
              <ThemedText style={styles.cardHeaderTitle}>Line Details</ThemedText>
            </View>

            <View style={{ flexDirection: 'row', alignItems: 'center', gap: 8 }}>
              {selectedIndex !== null && (
                <TouchableOpacity
                  style={[styles.addLineBtn, { backgroundColor: '#8E24AA' }]}
                  onPress={() => router.push('/barcode' as any)}
                >
                  <Ionicons name="barcode-outline" size={14} color="#FFF" style={{ marginRight: 4 }} />
                  <ThemedText style={styles.addLineBtnText}>BarCode</ThemedText>
                </TouchableOpacity>
              )}
              {selectedIndex !== null && (
                <TouchableOpacity
                  style={[styles.addLineBtn, { backgroundColor: '#C62828' }]}
                  onPress={handleDeleteDetail}
                >
                  <Ionicons name="trash-outline" size={14} color="#FFF" style={{ marginRight: 4 }} />
                  <ThemedText style={styles.addLineBtnText}>Delete</ThemedText>
                </TouchableOpacity>
              )}
              <TouchableOpacity
                style={styles.addLineBtn}
                onPress={() => {
                  setSelectedIndex(null);
                  setCurrentDetail(getInitialDetailState());
                  setDetailModalVisible(true);
                }}
              >
                <Ionicons name="add" size={14} color="#FFF" style={{ marginRight: 4 }} />
                <ThemedText style={styles.addLineBtnText}>New Line</ThemedText>
              </TouchableOpacity>
            </View>
          </View>

          <ScrollView horizontal contentContainerStyle={{ width: '100%' }}>
            <View style={{ width: '100%', minWidth: 1050 }}>
              <View style={styles.tableHeader}>
                <ThemedText style={[styles.tableHeaderText, { width: 120, textAlign: 'center' }]}>Item Code</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { width: 60, textAlign: 'center' }]}>Class</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { flex: 2, minWidth: 200, textAlign: 'center' }]}>Description</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { width: 70, textAlign: 'center' }]}>SizeCode</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { width: 80, textAlign: 'right' }]}>Weight</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { width: 80, textAlign: 'right' }]}>NetWeight</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { width: 80, textAlign: 'right' }]}>GoldLoss</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { width: 90, textAlign: 'right' }]}>CostPerGram</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { width: 75, textAlign: 'right' }]}>PMC</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { width: 75, textAlign: 'right' }]}>SMC</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { width: 50, textAlign: 'right' }]}>PCS</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { flex: 1, minWidth: 110, textAlign: 'right' }]}>TotalCost</ThemedText>
              </View>

              <ScrollView style={{ minHeight: 140, maxHeight: 240 }}>
                {details.length === 0 ? (
                  <View style={styles.emptyTableState}>
                    <Ionicons name="cube-outline" size={36} color="#D8C7AA" style={{ marginBottom: 6 }} />
                    <ThemedText style={{ color: '#8F7E66', fontSize: 13, fontWeight: '500' }}>No records to display</ThemedText>
                  </View>
                ) : (
                  details.map((item, idx) => (
                    <TouchableOpacity
                      key={idx}
                      style={[
                        styles.tableRow,
                        { backgroundColor: selectedIndex === idx ? '#F9F1E2' : idx % 2 === 1 ? '#FAF7F0' : '#FFFFFF' }
                      ]}
                      onPress={() => handleEditDetail(idx)}
                    >
                      <ThemedText style={[styles.tableCellText, { width: 120, fontWeight: 'bold' }]}>{item.InventoryCode}</ThemedText>
                      <ThemedText style={[styles.tableCellText, { width: 60, textAlign: 'center' }]}>{item.ClassCode}</ThemedText>
                      <ThemedText style={[styles.tableCellText, { flex: 2, minWidth: 200 }]}>{item.InventoryDescription}</ThemedText>
                      <ThemedText style={[styles.tableCellText, { width: 70, textAlign: 'center' }]}>{item.SizeCode}</ThemedText>
                      <ThemedText style={[styles.tableCellText, { width: 80, textAlign: 'right' }]}>{parseFloat(item.Weight || 0).toFixed(3)}</ThemedText>
                      <ThemedText style={[styles.tableCellText, { width: 80, textAlign: 'right' }]}>{parseFloat(item.NetWeight || item.Weight || 0).toFixed(3)}</ThemedText>
                      <ThemedText style={[styles.tableCellText, { width: 80, textAlign: 'right' }]}>{parseFloat(item.GoldLoss || 0).toFixed(3)}</ThemedText>
                      <ThemedText style={[styles.tableCellText, { width: 90, textAlign: 'right' }]}>{parseFloat(item.CostPerGram || 0).toFixed(2)}</ThemedText>
                      <ThemedText style={[styles.tableCellText, { width: 75, textAlign: 'right' }]}>{parseFloat(item.PMC || 0).toFixed(2)}</ThemedText>
                      <ThemedText style={[styles.tableCellText, { width: 75, textAlign: 'right' }]}>{parseFloat(item.SMC || 0).toFixed(2)}</ThemedText>
                      <ThemedText style={[styles.tableCellText, { width: 50, textAlign: 'right' }]}>{item.PCS || '1'}</ThemedText>
                      <ThemedText style={[styles.tableCellText, { flex: 1, minWidth: 110, textAlign: 'right', fontWeight: 'bold' }]}>{parseFloat(item.TotalCost || 0).toFixed(2)}</ThemedText>
                    </TouchableOpacity>
                  ))
                )}
              </ScrollView>

              {/* TOTALS ROW */}
              <View style={styles.tableTotalRow}>
                <ThemedText style={[styles.tableTotalCellText, { width: 120, textAlign: 'center' }]}>TOTAL :</ThemedText>
                <View style={{ width: 60 }} />
                <View style={{ flex: 2, minWidth: 200 }} />
                <View style={{ width: 70 }} />
                <ThemedText style={[styles.tableTotalCellText, { width: 80, textAlign: 'right' }]}>
                  {details.reduce((sum, i) => sum + (parseFloat(i.Weight) || 0), 0).toFixed(3)}
                </ThemedText>
                <ThemedText style={[styles.tableTotalCellText, { width: 80, textAlign: 'right' }]}>
                  {details.reduce((sum, i) => sum + (parseFloat(i.NetWeight || i.Weight) || 0), 0).toFixed(3)}
                </ThemedText>
                <ThemedText style={[styles.tableTotalCellText, { width: 80, textAlign: 'right' }]}>
                  {details.reduce((sum, i) => sum + (parseFloat(i.GoldLoss) || 0), 0).toFixed(3)}
                </ThemedText>
                <View style={{ width: 90 }} />
                <ThemedText style={[styles.tableTotalCellText, { width: 75, textAlign: 'right' }]}>
                  {details.reduce((sum, i) => sum + (parseFloat(i.PMCCost || i.PMC) || 0), 0).toFixed(2)}
                </ThemedText>
                <ThemedText style={[styles.tableTotalCellText, { width: 75, textAlign: 'right' }]}>
                  {details.reduce((sum, i) => sum + (parseFloat(i.SMCCost || i.SMC) || 0), 0).toFixed(2)}
                </ThemedText>
                <ThemedText style={[styles.tableTotalCellText, { width: 50, textAlign: 'right' }]}>
                  {details.reduce((sum, i) => sum + (parseFloat(i.PCS) || 1), 0)}
                </ThemedText>
                <ThemedText style={[styles.tableTotalCellText, { flex: 1, minWidth: 110, textAlign: 'right' }]}>
                  {details.reduce((sum, i) => sum + (parseFloat(i.TotalCost) || 0), 0).toFixed(2)}
                </ThemedText>
              </View>
            </View>
          </ScrollView>
        </View>
      </ScrollView>

      {/* ================= LINE DETAIL ENTRY MODAL ================= */}
      <Modal visible={detailModalVisible} transparent={true} animationType="fade">
        <View style={styles.modalOverlay}>

          <View style={[styles.goldCard, { width: '92%', maxWidth: 1350, maxHeight: '90%' }]}>
            <View style={[styles.cardHeaderBanner, { justifyContent: 'space-between' }]}>
              <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                <View style={styles.headerIconBadge}>
                  <Ionicons name="create-outline" size={15} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitle}>
                  {selectedIndex !== null ? 'Edit Line Detail' : 'Add Line Detail'}
                </ThemedText>
              </View>

              <View style={{ flexDirection: 'row', gap: 8, alignItems: 'center' }}>
                <TouchableOpacity
                  style={[
                    styles.addLineBtn,
                    {
                      backgroundColor: isManual ? '#FDE8E8' : '#E8F5E9',
                      borderWidth: 1,
                      borderColor: isManual ? '#E57373' : '#81C784'
                    }
                  ]}
                  onPress={() => setIsManual(!isManual)}
                >
                  <ThemedText
                    style={{
                      color: isManual ? '#C62828' : '#2E7D32',
                      fontWeight: 'bold',
                      fontSize: 11
                    }}
                  >
                    {isManual ? 'MANUAL (F10)' : 'AUTO (F10)'}
                  </ThemedText>
                </TouchableOpacity>

                <TouchableOpacity onPress={() => setDetailModalVisible(false)} style={{ padding: 4, marginLeft: 6 }}>
                  <Ionicons name="close" size={22} color="#3B2D17" />
                </TouchableOpacity>
              </View>
            </View>

            {/* Modal Toast Notification (Rendered exclusively inside modal when open) */}
            {toast.visible && (
              <View
                style={[
                  styles.toastContainer,
                  {
                    backgroundColor: toast.type === 'success' ? '#2E7D32' : toast.type === 'error' ? '#C62828' : '#1565C0',
                    top: 14,
                    zIndex: 99999
                  }
                ]}
              >
                <Ionicons
                  name={toast.type === 'success' ? 'checkmark-circle' : toast.type === 'error' ? 'alert-circle' : 'information-circle'}
                  size={18}
                  color="#fff"
                  style={{ marginRight: 8 }}
                />
                <ThemedText style={{ color: '#FFF', fontWeight: 'bold', fontSize: 13, textAlign: 'center' }}>
                  {toast.message}
                </ThemedText>
              </View>
            )}

            <ScrollView style={{ padding: 12 }}>
              {/* TABLE GRID: 2 Sublines formatted identically to purchase.tsx */}
              <ScrollView horizontal showsHorizontalScrollIndicator={true} contentContainerStyle={{ flexGrow: 1 }}>
                <View style={{ width: '100%', minWidth: 1200, borderWidth: 1, borderColor: '#D8CEBD', backgroundColor: '#FFFFFF', padding: 2, marginBottom: 8 }}>
                  {/* SUBLINE 1 - ROW 1: Headers */}
                  <View style={{ flexDirection: 'row' }}>
                    <View style={{ width: 130, backgroundColor: '#FAF4E6', borderWidth: 1, borderColor: '#D8CEBD', paddingVertical: 4, justifyContent: 'center' }}>
                      <ThemedText style={{ color: '#6E4E10', fontSize: 10.5, fontWeight: 'bold', textAlign: 'center' }}>Catalogue Code</ThemedText>
                    </View>
                    <View style={{ width: 180, backgroundColor: '#FAF4E6', borderWidth: 1, borderColor: '#D8CEBD', borderLeftWidth: 0, paddingVertical: 4, justifyContent: 'center' }}>
                      <ThemedText style={{ color: '#6E4E10', fontSize: 10.5, fontWeight: 'bold', textAlign: 'center' }}>Category Code</ThemedText>
                    </View>
                    <View style={{ width: 110, backgroundColor: '#FAF4E6', borderWidth: 1, borderColor: '#D8CEBD', borderLeftWidth: 0, paddingVertical: 4, justifyContent: 'center' }}>
                      <ThemedText style={{ color: '#6E4E10', fontSize: 10.5, fontWeight: 'bold', textAlign: 'center' }}>Tray Id</ThemedText>
                    </View>
                    <View style={{ width: 110, backgroundColor: '#FAF4E6', borderWidth: 1, borderColor: '#D8CEBD', borderLeftWidth: 0, paddingVertical: 4, justifyContent: 'center' }}>
                      <ThemedText style={{ color: '#6E4E10', fontSize: 10.5, fontWeight: 'bold', textAlign: 'center' }}>Catalogue Wt</ThemedText>
                    </View>
                    <View style={{ width: 100, backgroundColor: '#FAF4E6', borderWidth: 1, borderColor: '#D8CEBD', borderLeftWidth: 0, paddingVertical: 4, justifyContent: 'center' }}>
                      <ThemedText style={{ color: '#6E4E10', fontSize: 10.5, fontWeight: 'bold', textAlign: 'center' }}>Catalogue PCS</ThemedText>
                    </View>
                    <View style={{ width: 200, backgroundColor: '#FAF4E6', borderWidth: 1, borderColor: '#D8CEBD', borderLeftWidth: 0, paddingVertical: 4, justifyContent: 'center' }}>
                      <ThemedText style={{ color: '#6E4E10', fontSize: 10.5, fontWeight: 'bold', textAlign: 'center' }}>Purchase MC</ThemedText>
                    </View>
                    <View style={{ flex: 1, minWidth: 260, backgroundColor: '#FAF4E6', borderWidth: 1, borderColor: '#D8CEBD', borderLeftWidth: 0, paddingVertical: 4, justifyContent: 'center' }}>
                      <ThemedText style={{ color: '#6E4E10', fontSize: 10.5, fontWeight: 'bold', textAlign: 'center' }}>Sales MC</ThemedText>
                    </View>
                  </View>

                  {/* SUBLINE 1 - ROW 2: Inputs */}
                  <View style={{ flexDirection: 'row', marginBottom: 6 }}>
                    {/* Catalogue Code */}
                    <View style={{ width: 130, borderWidth: 1, borderColor: '#D8CEBD', borderTopWidth: 0, flexDirection: 'row', height: 28, backgroundColor: '#FFFFFF' }}>
                      <TextInput
                        style={{ width: 102, paddingHorizontal: 6, paddingVertical: 2, fontSize: 11.5, color: '#2B2114' }}
                        value={currentDetail.CatalogueCode}
                        onChangeText={(t) => updateDetail('CatalogueCode', t)}
                      />
                      <TouchableOpacity
                        style={{ width: 26, backgroundColor: '#FAF4E6', borderLeftWidth: 1, borderColor: '#D8CEBD', justifyContent: 'center', alignItems: 'center', flexShrink: 0 }}
                        onPress={fetchCatalogues}
                      >
                        <ThemedText style={{ color: '#6E4E10', fontWeight: 'bold', fontSize: 12 }}>...</ThemedText>
                      </TouchableOpacity>
                    </View>

                    {/* Category Code (without LOV button as requested) */}
                    <View style={{ width: 180, borderWidth: 1, borderColor: '#D8CEBD', borderTopWidth: 0, borderLeftWidth: 0, height: 28, backgroundColor: '#FFFFFF' }}>
                      <TextInput
                        style={{ flex: 1, paddingHorizontal: 6, paddingVertical: 2, fontSize: 11.5, color: '#2B2114' }}
                        value={currentDetail.CategoryCode}
                        onChangeText={(t) => updateDetail('CategoryCode', t)}
                      />
                    </View>

                    {/* Tray Id */}
                    <View style={{ width: 110, borderWidth: 1, borderColor: '#D8CEBD', borderTopWidth: 0, borderLeftWidth: 0, flexDirection: 'row', height: 28, backgroundColor: '#FFFFFF' }}>
                      <TextInput
                        style={{ width: 82, paddingHorizontal: 6, paddingVertical: 2, fontSize: 11.5, color: '#2B2114' }}
                        value={currentDetail.TrayId}
                        onChangeText={(t) => updateDetail('TrayId', t)}
                      />
                      <TouchableOpacity
                        style={{ width: 26, backgroundColor: '#FAF4E6', borderLeftWidth: 1, borderColor: '#D8CEBD', justifyContent: 'center', alignItems: 'center', flexShrink: 0 }}
                        onPress={fetchTrays}
                      >
                        <ThemedText style={{ color: '#6E4E10', fontWeight: 'bold', fontSize: 12 }}>...</ThemedText>
                      </TouchableOpacity>
                    </View>

                    {/* Catalogue Wt */}
                    <View style={{ width: 110, borderWidth: 1, borderColor: '#D8CEBD', borderTopWidth: 0, borderLeftWidth: 0, height: 28, backgroundColor: '#FFFFFF' }}>
                      <TextInput
                        style={{ flex: 1, paddingHorizontal: 6, paddingVertical: 2, fontSize: 11.5, color: '#2B2114', textAlign: 'right' }}
                        value={currentDetail.CatalogueWeight}
                        onChangeText={(t) => updateDetail('CatalogueWeight', t)}
                        keyboardType="numeric"
                      />
                    </View>

                    {/* Catalogue PCS */}
                    <View style={{ width: 100, borderWidth: 1, borderColor: '#D8CEBD', borderTopWidth: 0, borderLeftWidth: 0, height: 28, backgroundColor: '#FFFFFF' }}>
                      <TextInput
                        style={{ flex: 1, paddingHorizontal: 6, paddingVertical: 2, fontSize: 11.5, color: '#2B2114', textAlign: 'right' }}
                        value={currentDetail.CataloguePCS}
                        onChangeText={(t) => updateDetail('CataloguePCS', t)}
                        keyboardType="numeric"
                      />
                    </View>

                    {/* Purchase MC */}
                    <View style={{ width: 200, borderWidth: 1, borderColor: '#D8CEBD', borderTopWidth: 0, borderLeftWidth: 0, flexDirection: 'row', alignItems: 'center', justifyContent: 'space-around', height: 28, backgroundColor: '#FAF8F3', paddingHorizontal: 4 }}>
                      <Radio
                        label="Per Gram"
                        selected={currentDetail.PurchaseMCType === 'PerGram'}
                        onPress={() => updateDetail('PurchaseMCType', 'PerGram')}
                      />
                      <Radio
                        label="Per PCS"
                        selected={currentDetail.PurchaseMCType === 'PerPCS'}
                        onPress={() => updateDetail('PurchaseMCType', 'PerPCS')}
                      />
                    </View>

                    {/* Sales MC */}
                    <View style={{ flex: 1, minWidth: 260, borderWidth: 1, borderColor: '#D8CEBD', borderTopWidth: 0, borderLeftWidth: 0, flexDirection: 'row', alignItems: 'center', justifyContent: 'space-around', height: 28, backgroundColor: '#FAF8F3', paddingHorizontal: 4 }}>
                      <Checkbox
                        label="AUTO"
                        selected={autoSMC}
                        onPress={() => setAutoSMC(!autoSMC)}
                      />
                      <Checkbox
                        label="PCS"
                        selected={salesPCS}
                        onPress={() => setSalesPCS(!salesPCS)}
                      />
                      <Radio
                        label="Per Gram"
                        selected={currentDetail.SalesMCType === 'PerGram'}
                        onPress={() => updateDetail('SalesMCType', 'PerGram')}
                      />
                      <Radio
                        label="Per PCS"
                        selected={currentDetail.SalesMCType === 'PerPCS'}
                        onPress={() => updateDetail('SalesMCType', 'PerPCS')}
                      />
                    </View>
                  </View>

                  {/* SUBLINE 2 - ROW 3: Headers */}
                  <View style={{ flexDirection: 'row' }}>
                    <View style={{ width: 130, backgroundColor: '#FAF4E6', borderWidth: 1, borderColor: '#D8CEBD', paddingVertical: 4, justifyContent: 'center' }}>
                      <ThemedText style={{ color: '#6E4E10', fontSize: 10.5, fontWeight: 'bold', textAlign: 'center' }}>Inventory Code</ThemedText>
                    </View>
                    <View style={{ flex: 2, minWidth: 200, backgroundColor: '#FAF4E6', borderWidth: 1, borderColor: '#D8CEBD', borderLeftWidth: 0, paddingVertical: 4, justifyContent: 'center' }}>
                      <ThemedText style={{ color: '#6E4E10', fontSize: 10.5, fontWeight: 'bold', textAlign: 'center' }}>Inventory Description</ThemedText>
                    </View>
                    <View style={{ width: 85, backgroundColor: '#FAF4E6', borderWidth: 1, borderColor: '#D8CEBD', borderLeftWidth: 0, paddingVertical: 4, justifyContent: 'center' }}>
                      <ThemedText style={{ color: '#6E4E10', fontSize: 10.5, fontWeight: 'bold', textAlign: 'center' }}>Weight</ThemedText>
                    </View>
                    <View style={{ width: 95, backgroundColor: '#FAF4E6', borderWidth: 1, borderColor: '#D8CEBD', borderLeftWidth: 0, paddingVertical: 4, justifyContent: 'center' }}>
                      <ThemedText style={{ color: '#6E4E10', fontSize: 10.5, fontWeight: 'bold', textAlign: 'center' }}>Size Code</ThemedText>
                    </View>
                    <View style={{ width: 85, backgroundColor: '#FAF4E6', borderWidth: 1, borderColor: '#D8CEBD', borderLeftWidth: 0, paddingVertical: 4, justifyContent: 'center' }}>
                      <ThemedText style={{ color: '#6E4E10', fontSize: 10.5, fontWeight: 'bold', textAlign: 'center' }}>Gold Loss</ThemedText>
                    </View>
                    <View style={{ width: 95, backgroundColor: '#FAF4E6', borderWidth: 1, borderColor: '#D8CEBD', borderLeftWidth: 0, paddingVertical: 4, justifyContent: 'center' }}>
                      <ThemedText style={{ color: '#6E4E10', fontSize: 10.5, fontWeight: 'bold', textAlign: 'center' }}>Cost / g</ThemedText>
                    </View>
                    <View style={{ width: 80, backgroundColor: '#FAF4E6', borderWidth: 1, borderColor: '#D8CEBD', borderLeftWidth: 0, paddingVertical: 4, justifyContent: 'center' }}>
                      <ThemedText style={{ color: '#6E4E10', fontSize: 10.5, fontWeight: 'bold', textAlign: 'center' }}>PMC</ThemedText>
                    </View>
                    <View style={{ width: 80, backgroundColor: '#FAF4E6', borderWidth: 1, borderColor: '#D8CEBD', borderLeftWidth: 0, paddingVertical: 4, justifyContent: 'center' }}>
                      <ThemedText style={{ color: '#6E4E10', fontSize: 10.5, fontWeight: 'bold', textAlign: 'center' }}>SMC</ThemedText>
                    </View>
                    <View style={{ width: 65, backgroundColor: '#FAF4E6', borderWidth: 1, borderColor: '#D8CEBD', borderLeftWidth: 0, paddingVertical: 4, justifyContent: 'center' }}>
                      <ThemedText style={{ color: '#6E4E10', fontSize: 10.5, fontWeight: 'bold', textAlign: 'center' }}>PCS</ThemedText>
                    </View>
                    <View style={{ flex: 1, minWidth: 110, backgroundColor: '#FAF4E6', borderWidth: 1, borderColor: '#D8CEBD', borderLeftWidth: 0, paddingVertical: 4, justifyContent: 'center' }}>
                      <ThemedText style={{ color: '#6E4E10', fontSize: 10.5, fontWeight: 'bold', textAlign: 'center' }}>Total Cost</ThemedText>
                    </View>
                  </View>

                  {/* SUBLINE 2 - ROW 4: Inputs */}
                  <View style={{ flexDirection: 'row' }}>
                    {/* Inventory Code */}
                    <View style={{ width: 130, borderWidth: 1, borderColor: '#D8CEBD', borderTopWidth: 0, height: 28, backgroundColor: !isManual ? '#F8F5EE' : '#FFFFFF' }}>
                      <TextInput
                        style={{ flex: 1, paddingHorizontal: 6, paddingVertical: 2, fontSize: 11.5, color: '#2B2114' }}
                        value={currentDetail.InventoryCode}
                        onChangeText={(t) => updateDetail('InventoryCode', t)}
                        editable={isManual}
                      />
                    </View>

                    {/* Inventory Description */}
                    <View style={{ flex: 2, minWidth: 200, borderWidth: 1, borderColor: '#D8CEBD', borderTopWidth: 0, borderLeftWidth: 0, height: 28, backgroundColor: '#FFFFFF' }}>
                      <TextInput
                        style={{ flex: 1, paddingHorizontal: 6, paddingVertical: 2, fontSize: 11.5, color: '#2B2114' }}
                        value={currentDetail.InventoryDescription}
                        onChangeText={(t) => updateDetail('InventoryDescription', t)}
                      />
                    </View>

                    {/* Weight */}
                    <View style={{ width: 85, borderWidth: 1, borderColor: '#D8CEBD', borderTopWidth: 0, borderLeftWidth: 0, height: 28, backgroundColor: '#FFFFFF' }}>
                      <TextInput
                        style={{ flex: 1, paddingHorizontal: 6, paddingVertical: 2, fontSize: 11.5, color: '#2B2114', textAlign: 'right' }}
                        value={currentDetail.Weight}
                        onChangeText={(t) => updateDetail('Weight', t)}
                        keyboardType="numeric"
                      />
                    </View>

                    {/* Size Code */}
                    <View style={{ width: 95, borderWidth: 1, borderColor: '#D8CEBD', borderTopWidth: 0, borderLeftWidth: 0, flexDirection: 'row', height: 28, backgroundColor: '#FFFFFF' }}>
                      <TextInput
                        style={{ width: 67, paddingHorizontal: 6, paddingVertical: 2, fontSize: 11.5, color: '#2B2114', textAlign: 'center' }}
                        value={currentDetail.SizeCode}
                        onChangeText={(t) => updateDetail('SizeCode', t)}
                      />
                      <TouchableOpacity style={{ width: 26, backgroundColor: '#FAF4E6', borderLeftWidth: 1, borderColor: '#D8CEBD', justifyContent: 'center', alignItems: 'center', flexShrink: 0 }} onPress={fetchSizes}>
                        <ThemedText style={{ color: '#6E4E10', fontWeight: 'bold', fontSize: 12 }}>...</ThemedText>
                      </TouchableOpacity>
                    </View>

                    {/* Gold Loss */}
                    <View style={{ width: 85, borderWidth: 1, borderColor: '#D8CEBD', borderTopWidth: 0, borderLeftWidth: 0, height: 28, backgroundColor: '#FFFFFF' }}>
                      <TextInput
                        style={{ flex: 1, paddingHorizontal: 6, paddingVertical: 2, fontSize: 11.5, color: '#2B2114', textAlign: 'right' }}
                        value={currentDetail.GoldLoss}
                        onChangeText={(t) => updateDetail('GoldLoss', t)}
                        keyboardType="numeric"
                      />
                    </View>

                    {/* Cost / g */}
                    <View style={{ width: 95, borderWidth: 1, borderColor: '#D8CEBD', borderTopWidth: 0, borderLeftWidth: 0, height: 28, backgroundColor: '#FFFFFF' }}>
                      <TextInput
                        style={{ flex: 1, paddingHorizontal: 6, paddingVertical: 2, fontSize: 11.5, color: '#2B2114', textAlign: 'right' }}
                        value={currentDetail.CostPerGram}
                        onChangeText={(t) => updateDetail('CostPerGram', t)}
                        placeholder={headerState.CostPerGram}
                        keyboardType="numeric"
                      />
                    </View>

                    {/* PMC */}
                    <View style={{ width: 80, borderWidth: 1, borderColor: '#D8CEBD', borderTopWidth: 0, borderLeftWidth: 0, height: 28, backgroundColor: '#FFFFFF' }}>
                      <TextInput
                        style={{ flex: 1, paddingHorizontal: 6, paddingVertical: 2, fontSize: 11.5, color: '#2B2114', textAlign: 'right' }}
                        value={currentDetail.PMC}
                        onChangeText={(t) => updateDetail('PMC', t)}
                        keyboardType="numeric"
                      />
                    </View>

                    {/* SMC */}
                    <View style={{ width: 80, borderWidth: 1, borderColor: '#D8CEBD', borderTopWidth: 0, borderLeftWidth: 0, height: 28, backgroundColor: autoSMC ? '#F8F5EE' : '#FFFFFF' }}>
                      <TextInput
                        style={{ flex: 1, paddingHorizontal: 6, paddingVertical: 2, fontSize: 11.5, color: '#2B2114', textAlign: 'right' }}
                        value={currentDetail.SMC}
                        onChangeText={(t) => updateDetail('SMC', t)}
                        editable={!autoSMC}
                        keyboardType="numeric"
                      />
                    </View>

                    {/* PCS */}
                    <View style={{ width: 65, borderWidth: 1, borderColor: '#D8CEBD', borderTopWidth: 0, borderLeftWidth: 0, height: 28, backgroundColor: '#FFFFFF' }}>
                      <TextInput
                        style={{ flex: 1, paddingHorizontal: 6, paddingVertical: 2, fontSize: 11.5, color: '#2B2114', textAlign: 'right' }}
                        value={currentDetail.PCS}
                        onChangeText={(t) => updateDetail('PCS', t)}
                        keyboardType="numeric"
                      />
                    </View>

                    {/* Total Cost */}
                    <View style={{ flex: 1, minWidth: 110, borderWidth: 1, borderColor: '#D8CEBD', borderTopWidth: 0, borderLeftWidth: 0, height: 28, backgroundColor: '#F8F5EE' }}>
                      <TextInput
                        style={{ flex: 1, paddingHorizontal: 6, paddingVertical: 2, fontSize: 11.5, color: '#9E6D08', textAlign: 'right', fontWeight: 'bold' }}
                        value={currentDetail.TotalCost}
                        editable={false}
                      />
                    </View>
                  </View>
                </View>
              </ScrollView>

              {/* CHECKBOXES & ACTION BUTTONS ROW */}
              <View style={{ flexDirection: 'row', justifyContent: 'space-between', alignItems: 'center', marginTop: 12, borderTopWidth: 1, borderTopColor: '#F0E6D2', paddingTop: 10 }}>
                <View style={{ flexDirection: 'row', gap: 14, alignItems: 'center' }}>
                  <Checkbox
                    label="Addl. Desc"
                    selected={currentDetail.AddlDesc}
                    onPress={() => {
                      const nextVal = !currentDetail.AddlDesc;
                      updateDetail('AddlDesc', nextVal);
                      if (nextVal) setAddlDescModalVisible(true);
                    }}
                  />
                  {currentDetail.AddlDesc && (
                    <TouchableOpacity
                      style={[styles.actionBtn, { backgroundColor: '#FDF7EB', borderColor: '#B8903B' }]}
                      onPress={() => setAddlDescModalVisible(true)}
                    >
                      <ThemedText style={{ color: '#9E6D08', fontSize: 11, fontWeight: 'bold' }}>Edit Addl Desc</ThemedText>
                    </TouchableOpacity>
                  )}
                  <Checkbox
                    label="IPM"
                    selected={currentDetail.IPM}
                    onPress={() => updateDetail('IPM', !currentDetail.IPM)}
                  />
                  <Checkbox
                    label="Image"
                    selected={imageModalVisible}
                    onPress={() => setImageModalVisible(!imageModalVisible)}
                  />
                  <Checkbox
                    label="TradeIn Inventory"
                    selected={currentDetail.TradeInInventory}
                    onPress={() => updateDetail('TradeInInventory', !currentDetail.TradeInInventory)}
                  />
                </View>

                <View style={{ flexDirection: 'row', gap: 8, alignItems: 'center' }}>
                  <TouchableOpacity
                    style={[styles.actionBtn, { backgroundColor: '#F9F2E2', borderColor: '#B8903B' }]}
                    onPress={handleAddOrUpdateDetail}
                  >
                    <ThemedText style={{ color: '#9E6D08', fontWeight: 'bold', textAlign: 'center' }}>
                      {selectedIndex !== null ? 'Update' : 'Add Line'}
                    </ThemedText>
                  </TouchableOpacity>

                  <TouchableOpacity
                    style={[styles.actionBtn, { backgroundColor: '#FFFFFF', borderColor: '#D8CEBD' }]}
                    onPress={handleClearDetail}
                  >
                    <ThemedText style={{ color: '#554736', fontWeight: 'bold', textAlign: 'center' }}>Clear</ThemedText>
                  </TouchableOpacity>

                  <TouchableOpacity
                    style={[styles.actionBtn, { backgroundColor: '#FFEBEE', borderColor: '#E57373' }]}
                    onPress={handleDeleteDetail}
                  >
                    <ThemedText style={{ color: '#C62828', fontWeight: 'bold', textAlign: 'center' }}>Delete</ThemedText>
                  </TouchableOpacity>

                  <TouchableOpacity
                    style={[styles.actionBtn, { backgroundColor: '#EDE7F6', borderColor: '#9575CD' }]}
                    onPress={() => router.push('/barcode' as any)}
                  >
                    <ThemedText style={{ color: '#512DA8', fontWeight: 'bold', textAlign: 'center' }}>BarCode</ThemedText>
                  </TouchableOpacity>
                </View>
              </View>

              {/* IPM DETAILS EXPANDABLE BOX */}
              {currentDetail.IPM && (
                <View style={styles.ipmContainer}>
                  <ThemedText style={styles.ipmHeader}>IPM Details (Investment Precious Metals)</ThemedText>
                  <View style={styles.ipmRow}>
                    <View style={styles.ipmCol}>
                      <ThemedText style={styles.ipmLabel}>Type:</ThemedText>
                      <TextInput
                        style={[styles.input, { width: 90 }]}
                        value={currentDetail.IPMType}
                        onChangeText={(t) => updateDetail('IPMType', t)}
                        placeholder="Bar / Coin"
                      />
                    </View>
                    <View style={styles.ipmCol}>
                      <ThemedText style={styles.ipmLabel}>Bar No:</ThemedText>
                      <TextInput
                        style={[styles.input, { width: 100 }]}
                        value={currentDetail.IPMBarNo}
                        onChangeText={(t) => updateDetail('IPMBarNo', t)}
                      />
                    </View>
                    <View style={styles.ipmCol}>
                      <ThemedText style={styles.ipmLabel}>Purity:</ThemedText>
                      <TextInput
                        style={[styles.input, { width: 70 }]}
                        value={currentDetail.IPMPurity}
                        onChangeText={(t) => updateDetail('IPMPurity', t)}
                        placeholder="999.9"
                      />
                    </View>
                    <View style={styles.ipmCol}>
                      <ThemedText style={styles.ipmLabel}>GST Type:</ThemedText>
                      <TextInput
                        style={[styles.input, { width: 80 }]}
                        value={currentDetail.IPMGSTType}
                        onChangeText={(t) => updateDetail('IPMGSTType', t)}
                      />
                    </View>
                    <View style={styles.ipmCol}>
                      <ThemedText style={styles.ipmLabel}>Refiner:</ThemedText>
                      <View style={{ flexDirection: 'row' }}>
                        <TextInput
                          style={[styles.input, { width: 85, borderTopRightRadius: 0, borderBottomRightRadius: 0 }]}
                          value={currentDetail.IPMRefiner}
                          onChangeText={(t) => updateDetail('IPMRefiner', t)}
                        />
                        <TouchableOpacity style={[styles.lookupBtn, { width: 24 }]} onPress={fetchRefiners}>
                          <ThemedText style={{ fontSize: 10 }}>...</ThemedText>
                        </TouchableOpacity>
                      </View>
                    </View>
                    <View style={styles.ipmCol}>
                      <ThemedText style={styles.ipmLabel}>Country:</ThemedText>
                      <View style={{ flexDirection: 'row' }}>
                        <TextInput
                          style={[styles.input, { width: 85, borderTopRightRadius: 0, borderBottomRightRadius: 0 }]}
                          value={currentDetail.IPMCountry}
                          onChangeText={(t) => updateDetail('IPMCountry', t)}
                        />
                        <TouchableOpacity style={[styles.lookupBtn, { width: 24 }]} onPress={fetchCountries}>
                          <ThemedText style={{ fontSize: 10 }}>...</ThemedText>
                        </TouchableOpacity>
                      </View>
                    </View>
                  </View>
                </View>
              )}

              {/* TRADEIN INVENTORY EXPANDABLE BOX */}
              {currentDetail.TradeInInventory && (
                <View style={styles.tradeInContainer}>
                  <ThemedText style={styles.tradeInHeader}>TradeIn Inventory Details</ThemedText>
                  <View style={styles.tradeInRow}>
                    <ThemedText style={styles.tradeInLabel}>Inventory Code:</ThemedText>
                    <View style={{ flexDirection: 'row', width: 140 }}>
                      <TextInput
                        style={[styles.input, { flex: 1, borderTopRightRadius: 0, borderBottomRightRadius: 0 }]}
                        value={currentDetail.TradeInInventoryCode}
                        onChangeText={(t) => updateDetail('TradeInInventoryCode', t)}
                      />
                      <TouchableOpacity style={[styles.lookupBtn, { width: 28 }]} onPress={fetchTradeInSearch}>
                        <ThemedText style={styles.lookupBtnText}>...</ThemedText>
                      </TouchableOpacity>
                    </View>

                    <ThemedText style={styles.tradeInLabel}>Description:</ThemedText>
                    <TextInput
                      style={[styles.input, styles.readonlyInput, { flex: 1 }]}
                      value={currentDetail.TradeInDescription}
                      editable={false}
                    />

                    <ThemedText style={styles.tradeInLabel}>Weight:</ThemedText>
                    <TextInput
                      style={[styles.input, styles.readonlyInput, { width: 80, textAlign: 'right' }]}
                      value={currentDetail.TradeInWeight}
                      editable={false}
                    />

                    <ThemedText style={styles.tradeInLabel}>Invoice No:</ThemedText>
                    <TextInput
                      style={[styles.input, styles.readonlyInput, { width: 90 }]}
                      value={currentDetail.TradeInInvoice}
                      editable={false}
                    />
                  </View>
                </View>
              )}
            </ScrollView>
          </View>
        </View>
      </Modal>

      {/* ================= ADDITIONAL DESCRIPTION MODAL (grpAddlDescription) ================= */}
      <Modal visible={addlDescModalVisible} transparent={true} animationType="fade">
        <View style={styles.modalOverlay}>
          <View style={styles.modalCard}>
            <View style={styles.modalHeader}>
              <ThemedText style={styles.modalHeaderTitle}>Additional Descriptions & Costs</ThemedText>
              <TouchableOpacity onPress={() => setAddlDescModalVisible(false)}>
                <Ionicons name="close" size={24} color="#000" />
              </TouchableOpacity>
            </View>

            <View style={{ padding: 12 }}>
              <View style={styles.modalColHeaderRow}>
                <ThemedText style={[styles.modalColHeaderText, { flex: 2 }]}>Description</ThemedText>
                <ThemedText style={[styles.modalColHeaderText, { flex: 1, textAlign: 'right' }]}>Cost</ThemedText>
                <ThemedText style={[styles.modalColHeaderText, { flex: 1, textAlign: 'right' }]}>Selling Price</ThemedText>
              </View>

              {[1, 2, 3, 4].map((num) => (
                <View key={num} style={{ flexDirection: 'row', gap: 8, marginBottom: 8, alignItems: 'center' }}>
                  <TextInput
                    style={[styles.inputBox, { flex: 2 }]}
                    placeholder={`Description ${num}`}
                    value={currentDetail[`Desc${num}`]}
                    onChangeText={(t) => updateDetail(`Desc${num}`, t)}
                  />
                  <TextInput
                    style={[styles.inputBox, { flex: 1, textAlign: 'right' }]}
                    placeholder="0.00"
                    keyboardType="numeric"
                    value={currentDetail[`Cost${num}`]}
                    onChangeText={(t) => updateDetail(`Cost${num}`, t)}
                  />
                  <TextInput
                    style={[styles.inputBox, { flex: 1, textAlign: 'right' }]}
                    placeholder="0.00"
                    keyboardType="numeric"
                    value={currentDetail[`SellingPrice${num}`]}
                    onChangeText={(t) => updateDetail(`SellingPrice${num}`, t)}
                  />
                </View>
              ))}

              <View style={{ flexDirection: 'row', justifyContent: 'flex-end', marginTop: 12 }}>
                <TouchableOpacity
                  style={[styles.actionBtn, { width: 100 }]}
                  onPress={() => setAddlDescModalVisible(false)}
                >
                  <ThemedText style={styles.actionBtnText}>OK</ThemedText>
                </TouchableOpacity>
              </View>
            </View>
          </View>
        </View>
      </Modal>

      {/* ================= IMAGE MODAL (grpImage in directpurchase.txt) ================= */}
      <Modal visible={imageModalVisible} transparent={true} animationType="fade">
        <View style={styles.modalOverlay}>
          <View style={[styles.modalCard, { width: 340 }]}>
            <View style={styles.modalHeader}>
              <ThemedText style={styles.modalHeaderTitle}>Inventory Image</ThemedText>
              <TouchableOpacity onPress={() => setImageModalVisible(false)}>
                <Ionicons name="close" size={24} color="#000" />
              </TouchableOpacity>
            </View>
            <View style={{ padding: 16, alignItems: 'center' }}>
              <View style={styles.imagePreviewBox}>
                <Ionicons name="image-outline" size={64} color="#888" />
                <ThemedText style={{ color: '#888', marginTop: 8 }}>No image selected</ThemedText>
              </View>
              <View style={{ flexDirection: 'row', gap: 10, marginTop: 14 }}>
                <TouchableOpacity
                  style={[styles.actionBtn, { backgroundColor: '#F0ECE1' }]}
                  onPress={() => showToast('Browse image feature ready', 'info')}
                >
                  <ThemedText style={[styles.actionBtnText, { color: '#000' }]}>Browse</ThemedText>
                </TouchableOpacity>
                <TouchableOpacity
                  style={styles.actionBtn}
                  onPress={() => setImageModalVisible(false)}
                >
                  <ThemedText style={styles.actionBtnText}>OK</ThemedText>
                </TouchableOpacity>
              </View>
            </View>
          </View>
        </View>
      </Modal>

      {/* ================= LOV SEARCH MODAL ================= */}
      <Modal visible={lovVisible} transparent={true} animationType="fade">
        <View style={styles.modalOverlay}>
          <View style={[styles.modalCard, { width: '80%', maxWidth: 700 }]}>
            <View style={styles.modalHeader}>
              <ThemedText style={styles.modalHeaderTitle}>{lovTitle}</ThemedText>
              <TouchableOpacity onPress={() => setLovVisible(false)}>
                <Ionicons name="close" size={24} color="#000" />
              </TouchableOpacity>
            </View>
            <View style={{ padding: 12 }}>
              <TextInput
                style={[styles.inputBox, { marginBottom: 10 }]}
                placeholder="Search..."
                value={lovSearch}
                onChangeText={handleLovSearch}
              />
              {lovColumns.length > 0 && (
                <View style={styles.lovHeaderRow}>
                  {lovColumns.map((col, idx) => (
                    <ThemedText key={idx} style={styles.lovHeaderText}>{col}</ThemedText>
                  ))}
                </View>
              )}
              <FlatList
                data={filteredLovData}
                keyExtractor={(_, index) => index.toString()}
                style={{ maxHeight: 280 }}
                renderItem={({ item }) => (
                  <TouchableOpacity
                    style={styles.lovItemRow}
                    onPress={() => handleLovSelect(item)}
                  >
                    {lovColumns.map((col, idx) => (
                      <ThemedText key={idx} style={styles.lovItemText}>
                        {String(item[col] ?? '')}
                      </ThemedText>
                    ))}
                  </TouchableOpacity>
                )}
              />
            </View>
          </View>
        </View>
      </Modal>

      {/* ================= LOADING OVERLAY ================= */}
      {loading && (
        <View style={styles.loadingOverlay}>
          <ActivityIndicator size="large" color="#FFBD00" />
          <ThemedText style={{ color: '#FFF', marginTop: 10, fontWeight: 'bold' }}>Processing...</ThemedText>
        </View>
      )}

      {/* ================= TOAST NOTIFICATION (When Modal is Closed) ================= */}
      {toast.visible && !detailModalVisible && (
        <View
          style={[
            styles.toastContainer,
            { backgroundColor: toast.type === 'success' ? '#2E7D32' : toast.type === 'error' ? '#C62828' : '#1565C0', zIndex: 99999 }
          ]}
        >
          <Ionicons
            name={toast.type === 'success' ? 'checkmark-circle' : toast.type === 'error' ? 'alert-circle' : 'information-circle'}
            size={18}
            color="#fff"
            style={{ marginRight: 8 }}
          />
          <ThemedText style={{ color: '#FFF', fontWeight: 'bold', fontSize: 13, textAlign: 'center' }}>
            {toast.message}
          </ThemedText>
        </View>
      )}
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  safeArea: { flex: 1, backgroundColor: '#FBF9F5', width: '100%' },
  container: { flex: 1, width: '100%' },
  contentContainer: { width: '100%', padding: 14, paddingBottom: 36 },

  // Luxury Gold Card Styling (Matching purchase.tsx)
  goldCard: {
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    overflow: 'hidden',
    shadowColor: '#A38035',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.08,
    shadowRadius: 6,
    elevation: 2,
  },
  cardHeaderBanner: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#F5E7CA',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    paddingVertical: 7,
    paddingHorizontal: 12,
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
  cardHeaderTitle: {
    fontSize: 13,
    fontWeight: 'bold',
    color: '#3B2D17',
    letterSpacing: 0.3,
  },
  cardBody: {
    padding: 10,
  },

  // Rows and Labels (Matching purchase.tsx)
  row: { flexDirection: 'row', alignItems: 'center', marginBottom: 7, flexWrap: 'wrap' },
  label: { fontSize: 11, fontWeight: 'bold', width: 96, marginRight: 6, color: '#3A2E19' },
  labelRight: { fontSize: 11, fontWeight: 'bold', textAlign: 'right', marginRight: 8, color: '#3A2E19' },

  // Inputs & Buttons (Matching purchase.tsx)
  input: {
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderRadius: 6,
    paddingHorizontal: 8,
    paddingVertical: 3,
    fontSize: 12,
    height: 28,
    backgroundColor: '#FFFFFF',
    color: '#2C2519',
  },
  readonlyInput: {
    backgroundColor: '#F8F5EE',
    borderColor: '#EADBCA',
    color: '#554736',
  },
  lookupBtn: {
    width: 32,
    height: 28,
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderLeftWidth: 1,
    justifyContent: 'center',
    alignItems: 'center',
    borderTopRightRadius: 6,
    borderBottomRightRadius: 6,
    backgroundColor: '#FAF5EA',
  },
  lookupBtnText: {
    color: '#6E5528',
    fontWeight: 'bold',
    fontSize: 12,
  },

  // Pill Box
  pillBox: {
    flexDirection: 'row',
    alignItems: 'center',
    marginLeft: 8,
    borderWidth: 1,
    borderColor: '#DFD3BC',
    borderRadius: 14,
    paddingHorizontal: 6,
    paddingVertical: 1,
    height: 26,
    backgroundColor: '#FBF8F2',
  },

  // Top Action Buttons
  topActionBtn: {
    paddingVertical: 6,
    paddingHorizontal: 12,
    borderRadius: 5,
    borderWidth: 1,
    borderColor: '#DFD5C3',
    backgroundColor: '#FFFFFF',
    marginHorizontal: 4,
  },

  // Table Styling (Matching purchase.tsx)
  addLineBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#9E6D08',
    paddingVertical: 4,
    paddingHorizontal: 12,
    borderRadius: 5,
  },
  addLineBtnText: {
    color: '#FFFFFF',
    fontWeight: 'bold',
    fontSize: 11,
  },
  tableHeader: {
    flexDirection: 'row',
    backgroundColor: '#F5E7CA',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    paddingVertical: 8,
    paddingHorizontal: 6,
    alignItems: 'center',
    minHeight: 36,
  },
  tableHeaderText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3B2D17',
    paddingHorizontal: 4,
    textAlignVertical: 'center',
  },
  tableRow: {
    flexDirection: 'row',
    paddingVertical: 7,
    paddingHorizontal: 6,
    borderBottomWidth: 1,
    borderBottomColor: '#F0E6D2',
    alignItems: 'center',
    minHeight: 34,
  },
  tableCellText: {
    fontSize: 11,
    color: '#332717',
    paddingHorizontal: 4,
    textAlignVertical: 'center',
  },
  emptyTableState: {
    paddingVertical: 30,
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: '#FFFFFF',
  },
  tableTotalRow: {
    flexDirection: 'row',
    backgroundColor: '#F9F1DF',
    borderTopWidth: 1,
    borderTopColor: '#E5D6B6',
    paddingVertical: 8,
    paddingHorizontal: 6,
    alignItems: 'center',
    minHeight: 36,
  },
  tableTotalCellText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#32230D',
    paddingHorizontal: 4,
    textAlignVertical: 'center',
  },

  // Legacy Header Card (JobSheet Details)
  headerCard: {
    backgroundColor: '#FFFFFF',
    borderColor: '#E8DEC7',
    borderWidth: 1,
    borderRadius: 8,
    marginBottom: 12,
    padding: 14,
    shadowColor: '#8C733E',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.06,
    shadowRadius: 6,
    elevation: 2,
  },
  headerCardTitleBar: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    marginBottom: 12,
    borderBottomWidth: 1,
    borderBottomColor: '#F0EAE1',
    paddingBottom: 8,
  },
  headerCardTitle: {
    fontSize: 14,
    fontWeight: '700',
    color: '#8A6715',
    letterSpacing: 0.3,
  },
  closedBadge: {
    backgroundColor: '#DC2626',
    paddingHorizontal: 10,
    paddingVertical: 3,
    borderRadius: 4,
  },
  closedBadgeText: {
    color: '#FFF',
    fontWeight: '700',
    fontSize: 10,
    letterSpacing: 0.5,
  },
  headerCardBody: {
    flexDirection: 'row',
  },
  formRow: {
    flexDirection: 'row',
    alignItems: 'center',
    marginBottom: 8,
  },
  labelCol: {
    fontSize: 11,
    fontWeight: '600',
    color: '#52432D',
    width: 95,
  },
  inputBox: {
    backgroundColor: '#FFFFFF',
    borderColor: '#D8CEBD',
    borderWidth: 1,
    height: 28,
    paddingHorizontal: 8,
    paddingVertical: 2,
    fontSize: 11.5,
    color: '#2B2114',
    borderRadius: 4,
  },
  readOnlyBox: {
    backgroundColor: '#F7F5F0',
    color: '#655743',
    borderColor: '#E4DDD0',
  },
  ellipsisBtn: {
    backgroundColor: '#C59A3F',
    width: 28,
    height: 28,
    justifyContent: 'center',
    alignItems: 'center',
    borderColor: '#B38830',
    borderWidth: 1,
    borderLeftWidth: 0,
    borderTopRightRadius: 4,
    borderBottomRightRadius: 4,
  },
  miniEllipsisBtn: {
    backgroundColor: '#C59A3F',
    width: 22,
    height: 28,
    justifyContent: 'center',
    alignItems: 'center',
    borderColor: '#B38830',
    borderWidth: 1,
    borderLeftWidth: 0,
    borderTopRightRadius: 4,
    borderBottomRightRadius: 4,
  },
  ellipsisText: {
    fontWeight: 'bold',
    fontSize: 12,
    color: '#FFFFFF',
  },
  fixBox: {
    flexDirection: 'row',
    alignItems: 'center',
    borderWidth: 1,
    borderColor: '#D8CEBD',
    paddingHorizontal: 8,
    paddingVertical: 3,
    borderRadius: 4,
    gap: 8,
    backgroundColor: '#FAF8F5',
  },

  // Summary Panel
  summaryPanel: {
    flex: 3.5,
    backgroundColor: '#FDFBF7',
    paddingLeft: 14,
    borderLeftWidth: 1,
    borderLeftColor: '#EBE3D5',
  },
  summaryRow: {
    flexDirection: 'row',
    alignItems: 'center',
    marginBottom: 7,
  },
  summaryLabel: {
    width: 80,
    fontSize: 11,
    fontWeight: '600',
    color: '#5C4A32',
  },
  summaryInput: {
    flex: 1,
    textAlign: 'right',
    fontWeight: '700',
    color: '#8A6715',
    backgroundColor: '#FFFFFF',
  },
  fixButton: {
    backgroundColor: '#C59A3F',
    borderColor: '#B38830',
    borderWidth: 1,
    paddingHorizontal: 12,
    height: 28,
    borderRadius: 4,
    justifyContent: 'center',
    alignItems: 'center',
    marginLeft: 6,
  },
  fixButtonText: {
    fontWeight: '700',
    fontSize: 11,
    color: '#FFFFFF',
  },

  // Grid Card
  gridCard: {
    borderWidth: 1,
    borderColor: '#E8DEC7',
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    overflow: 'hidden',
    marginBottom: 12,
    shadowColor: '#8C733E',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.05,
    shadowRadius: 5,
    elevation: 2,
  },
  gridHeaderRow: {
    flexDirection: 'row',
    backgroundColor: '#FAF4E6',
    borderBottomWidth: 1.5,
    borderBottomColor: '#D8C6A5',
  },
  gridHeaderCell: {
    paddingVertical: 7,
    paddingHorizontal: 6,
    fontSize: 11,
    fontWeight: '700',
    color: '#6E4E10',
    borderRightWidth: 1,
    borderRightColor: '#E6D9C0',
  },
  gridDataRow: {
    flexDirection: 'row',
    backgroundColor: '#FFFFFF',
    borderBottomWidth: 1,
    borderBottomColor: '#F0EBE1',
  },
  selectedGridRow: {
    backgroundColor: '#FFF8E7',
  },
  gridDataCell: {
    paddingVertical: 6,
    paddingHorizontal: 6,
    fontSize: 11,
    color: '#332717',
    borderRightWidth: 1,
    borderRightColor: '#F0EBE1',
  },
  emptyGridState: {
    paddingVertical: 36,
    alignItems: 'center',
    justifyContent: 'center',
    backgroundColor: '#FFFFFF',
  },

  // Entry Panel Card
  entryPanelCard: {
    borderWidth: 1,
    borderColor: '#E8DEC7',
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    padding: 12,
    marginBottom: 12,
    shadowColor: '#8C733E',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.06,
    shadowRadius: 6,
    elevation: 2,
  },
  entrySubGrid: {},
  entryBoxHeader: {
    backgroundColor: '#FAF4E6',
    borderWidth: 1,
    borderColor: '#E2D5BE',
    borderBottomWidth: 0,
    paddingVertical: 4,
    paddingHorizontal: 4,
    justifyContent: 'center',
    alignItems: 'center',
  },
  entryHeaderText: {
    fontSize: 10.5,
    fontWeight: '700',
    color: '#6E4E10',
    textAlign: 'center',
  },

  // IPM Container
  ipmContainer: {
    backgroundColor: '#FDFBF5',
    borderColor: '#E8DEC7',
    borderWidth: 1,
    borderRadius: 6,
    padding: 10,
    marginTop: 10,
  },
  ipmHeader: {
    fontWeight: '700',
    fontSize: 11,
    color: '#8A6715',
    marginBottom: 8,
  },
  ipmRow: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    gap: 12,
    alignItems: 'center',
  },
  ipmCol: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  ipmLabel: {
    fontSize: 10.5,
    fontWeight: '600',
    color: '#5C4A32',
    marginRight: 4,
  },

  // TradeIn Container
  tradeInContainer: {
    backgroundColor: '#F9F8F5',
    borderColor: '#E4DDD0',
    borderWidth: 1,
    borderRadius: 6,
    padding: 10,
    marginTop: 10,
  },
  tradeInHeader: {
    fontWeight: '700',
    fontSize: 11,
    color: '#5C4A32',
    marginBottom: 8,
  },
  tradeInRow: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 8,
  },
  tradeInLabel: {
    fontSize: 10.5,
    fontWeight: '600',
    color: '#5C4A32',
  },

  // Action Buttons (Matching purchase.tsx)
  actionBtn: {
    backgroundColor: '#FFFFFF',
    borderColor: '#DFD5C3',
    borderWidth: 1,
    borderRadius: 5,
    paddingVertical: 5,
    paddingHorizontal: 14,
    justifyContent: 'center',
    alignItems: 'center',
    minWidth: 70,
  },
  actionBtnText: {
    fontWeight: '700',
    fontSize: 11.5,
    color: '#3B2D17',
  },
  manualBtn: {
    paddingVertical: 5,
    paddingHorizontal: 14,
    borderRadius: 4,
    justifyContent: 'center',
    alignItems: 'center',
  },
  manualBtnText: {
    color: '#FFF',
    fontWeight: '700',
    fontSize: 11,
    letterSpacing: 0.3,
  },

  // Footer Command Bar
  footerCommandBar: {
    flexDirection: 'row',
    justifyContent: 'flex-end',
    gap: 10,
    paddingVertical: 12,
  },
  footerBtn: {
    backgroundColor: '#FFFFFF',
    borderColor: '#D8CEBD',
    borderWidth: 1,
    paddingVertical: 9,
    paddingHorizontal: 18,
    borderRadius: 6,
    minWidth: 110,
    justifyContent: 'center',
    alignItems: 'center',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.05,
    shadowRadius: 2,
    elevation: 1,
  },
  footerBtnText: {
    fontWeight: '700',
    fontSize: 12,
    color: '#473926',
  },

  // Radio & Checkbox
  radioContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    marginRight: 6,
  },
  radioCircle: {
    height: 15,
    width: 15,
    borderRadius: 7.5,
    borderWidth: 1.5,
    borderColor: '#B38830',
    alignItems: 'center',
    justifyContent: 'center',
    marginRight: 5,
    backgroundColor: '#FFF',
  },
  radioInnerCircle: {
    height: 7,
    width: 7,
    borderRadius: 3.5,
    backgroundColor: '#8A6715',
  },
  checkboxSquare: {
    height: 15,
    width: 15,
    borderRadius: 3,
    borderWidth: 1.5,
    borderColor: '#B38830',
    alignItems: 'center',
    justifyContent: 'center',
    marginRight: 5,
  },
  radioLabel: {
    fontSize: 11,
    fontWeight: '600',
    color: '#3B2D19',
  },

  // Modals
  modalOverlay: {
    flex: 1,
    backgroundColor: 'rgba(0,0,0,0.5)',
    justifyContent: 'center',
    alignItems: 'center',
  },
  modalCard: {
    backgroundColor: '#FFFFFF',
    borderColor: '#E8DEC7',
    borderWidth: 1,
    borderRadius: 8,
    width: '60%',
    maxWidth: 620,
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
    backgroundColor: '#FAF4E6',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D5BE',
    paddingHorizontal: 16,
    paddingVertical: 12,
  },
  modalHeaderTitle: {
    fontWeight: '700',
    fontSize: 13.5,
    color: '#6E4E10',
  },
  modalColHeaderRow: {
    flexDirection: 'row',
    gap: 8,
    borderBottomWidth: 1,
    borderBottomColor: '#EBE3D5',
    paddingBottom: 6,
    marginBottom: 8,
  },
  modalColHeaderText: {
    fontWeight: '700',
    fontSize: 11,
    color: '#5C4A32',
  },
  imagePreviewBox: {
    width: 180,
    height: 180,
    borderWidth: 1,
    borderColor: '#D8CEBD',
    borderRadius: 6,
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: '#FAF8F5',
  },
  lovHeaderRow: {
    flexDirection: 'row',
    borderBottomWidth: 1.5,
    borderBottomColor: '#E2D5BE',
    backgroundColor: '#FAF4E6',
    paddingVertical: 8,
    paddingHorizontal: 8,
    marginBottom: 6,
    borderRadius: 4,
  },
  lovHeaderText: {
    flex: 1,
    fontWeight: '700',
    fontSize: 11,
    color: '#6E4E10',
  },
  lovItemRow: {
    flexDirection: 'row',
    paddingVertical: 9,
    paddingHorizontal: 8,
    borderBottomWidth: 1,
    borderBottomColor: '#F0EBE1',
  },
  lovItemText: {
    flex: 1,
    fontSize: 11.5,
    color: '#3B2D19',
  },

  // Toast & Loading
  loadingOverlay: {
    position: 'absolute',
    top: 0,
    left: 0,
    right: 0,
    bottom: 0,
    backgroundColor: 'rgba(0,0,0,0.3)',
    justifyContent: 'center',
    alignItems: 'center',
    zIndex: 9999,
  },
  toastContainer: {
    position: 'absolute',
    top: 60,
    left: '50%',
    transform: [{ translateX: '-50%' } as any],
    paddingVertical: 10,
    paddingHorizontal: 18,
    borderRadius: 8,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    elevation: 10,
    zIndex: 100000,
    maxWidth: '85%',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.15,
    shadowRadius: 6,
  },
});
