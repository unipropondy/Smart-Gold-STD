import { ThemedText } from '@/components/themed-text';
import { useThemeContext } from '@/contexts/ThemeContext';
import { useThemeColors } from '@/hooks/useThemeColors';
import { Ionicons } from '@expo/vector-icons';
import AsyncStorage from '@react-native-async-storage/async-storage';
import { Stack, useLocalSearchParams, useRouter } from 'expo-router';
import React, { useEffect, useRef, useState } from 'react';
import {
  ActivityIndicator,
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
import { downloadExcelReport, downloadPdfReport, printHtmlReport } from '@/utils/reportExportClient';

const API_URL = `${API_BASE_URL}`;

const getLoggedInUser = async () => {
  let uid = '';
  let uname = '';

  // Check web localStorage / sessionStorage first
  if (Platform.OS === 'web' && typeof window !== 'undefined') {
    try {
      uid = window.localStorage.getItem('userId') || window.localStorage.getItem('userCode') || '';
      uname = window.localStorage.getItem('userName') || '';
      if (!uid) {
        const uStr = window.localStorage.getItem('user');
        if (uStr) {
          const uObj = JSON.parse(uStr);
          uid = uObj.userCode || uObj.userId || uObj.userName || '';
          uname = uObj.userName || '';
        }
      }
      if (!uid) {
        uid = window.sessionStorage.getItem('userId') || window.sessionStorage.getItem('userCode') || '';
        uname = window.sessionStorage.getItem('userName') || '';
      }
    } catch (_) { }
  }

  // Check AsyncStorage
  if (!uid) {
    try {
      uid = (await AsyncStorage.getItem('@user_id')) || '';
      if (!uname) {
        uname = (await AsyncStorage.getItem('@user_name')) || '';
      }
      if (!uid) {
        const uStr = await AsyncStorage.getItem('@logged_user');
        if (uStr) {
          const uObj = JSON.parse(uStr);
          uid = uObj.userCode || uObj.userId || uObj.userName || '';
          uname = uObj.userName || '';
        }
      }
    } catch (_) { }
  }

  return { userId: uid, userName: uname };
};

const getInitialHeaderState = () => ({
  JobSheetNo: '',
  InvoiceNo: '',
  InvoiceDate: new Date().toISOString().split('T')[0],
  SupplierCode: '',
  SupplierName: '',
  CurrencyCode: '',
  CurrencyName: '',
  CurrencyRate: '',
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
  PurchaseType: 'W',
  PurchaseMCType: 'PerGram',
  TotalWeight24K: '0.000',
  TEAL: '0.00',
  TEALPercent: '',
  Payable: 'Cash',
  FixUnFix: 'UnFix',
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
  SizeFactor: false,
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
    pmcCost = pmc * weight;
  } else {
    pmcCost = pmc * (pcs > 0 ? pcs : 1);
  }

  let smcCost = 0;
  if (currentAutoSMC) {
    smc = Math.round(pmc * 4);
    if (detail.SalesMCType === 'PerGram') {
      smcCost = Math.round(pmc * weight * 4);
    } else {
      smcCost = Math.round(pmc * (pcs > 0 ? pcs : 1) * 4);
    }
  } else {
    if (detail.SalesMCType === 'PerGram') {
      smcCost = Math.round(smc * weight);
    } else {
      smcCost = Math.round(smc * (pcs > 0 ? pcs : 1));
    }
  }

  let dCost = (weight + goldLoss) * costPerGram + pmcCost;

  if (detail.AddlDesc) {
    const c1 = parseFloat(detail.Cost1) || 0;
    const c2 = parseFloat(detail.Cost2) || 0;
    const c3 = parseFloat(detail.Cost3) || 0;
    const c4 = parseFloat(detail.Cost4) || 0;
    dCost += c1 + c2 + c3 + c4;
    pmcCost += c1 + c2 + c3 + c4;

    const sp1 = parseFloat(detail.SellingPrice1) || 0;
    const sp2 = parseFloat(detail.SellingPrice2) || 0;
    const sp3 = parseFloat(detail.SellingPrice3) || 0;
    const sp4 = parseFloat(detail.SellingPrice4) || 0;
    smcCost += sp1 + sp2 + sp3 + sp4;
  }

  const dLocalCost = dCost;

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
  if (!String(item.InventoryCode ?? '').trim()) {
    return { isValid: false, message: 'Inventory Code Cannot be Blank ....' };
  }
  if (!String(item.CatalogueCode ?? '').trim()) {
    return { isValid: false, message: 'Catalogue Code Cannot be Blank ....' };
  }
  if (!String(item.CategoryCode ?? '').trim()) {
    return { isValid: false, message: 'Category Code Cannot be Blank ....' };
  }
  if (!String(item.TrayId ?? '').trim()) {
    return { isValid: false, message: 'Tray Id Cannot be Blank ....' };
  }
  if (item.SizeFactor && !String(item.SizeCode ?? '').trim()) {
    return { isValid: false, message: 'SizeCode Cannot be Empty' };
  }
  if (!String(item.InventoryDescription ?? '').trim() && !String(item.Description ?? '').trim()) {
    return { isValid: false, message: 'Description Cannot be Blank ....' };
  }

  const weight = parseFloat(item.Weight) || 0;
  if (weight < 0) {
    return { isValid: false, message: 'Invalid weight!' };
  }
  if (weight <= 0 && !salesPCS && item.PurchaseMCType !== 'PerPCS') {
    return { isValid: false, message: 'Invalid weight!' };
  }

  const pcs = parseInt(item.PCS, 10) || 0;
  if (pcs < 1) {
    return { isValid: false, message: 'Invalid pcs!' };
  }

  // CostPerGram validation
  const costPerGram = parseFloat(item.CostPerGram) || 0;
  if (costPerGram <= 0 && item.PurchaseMCType !== 'PerPCS') {
    return { isValid: false, message: 'Invalid cost per gram!' };
  }

  // IPM checks if IPM active
  if (item.IPM) {
    if (!String(item.IPMType ?? '').trim()) {
      return { isValid: false, message: 'Type Cannot be Blank ....' };
    }
    if (!String(item.IPMBarNo ?? '').trim()) {
      return { isValid: false, message: 'BarNo Cannot be Blank ....' };
    }
    if (!String(item.IPMPurity ?? '').trim()) {
      return { isValid: false, message: 'Purity Cannot be Blank ....' };
    }
    if (!String(item.IPMGSTType ?? '').trim()) {
      return { isValid: false, message: 'GST Type Cannot be Blank ....' };
    }
    if (!String(item.IPMRefiner ?? '').trim()) {
      return { isValid: false, message: 'Refiner Cannot be Blank ....' };
    }
    if (!String(item.IPMCountry ?? '').trim()) {
      return { isValid: false, message: 'Country Cannot be Blank ....' };
    }
  }

  // Ordered inventory code check
  if (header.Ordered && String(header.OrderNo ?? '').trim()) {
    if (!String(item.TradeInInventoryCode ?? '').trim() && !String(item.OrderInventoryCode ?? '').trim()) {
      return { isValid: false, message: 'Please select the order inventory code.' };
    }
  }

  return { isValid: true, message: '' };
};

const validatePurchaseSave = (header: any, details: any[]) => {
  if (!String(header.InvoiceNo ?? '').trim()) {
    return { isValid: false, message: 'Enter invoice no.' };
  }
  if (!String(header.SupplierCode ?? '').trim()) {
    return { isValid: false, message: 'Enter supplier code.' };
  }
  if (!header.InvoiceDate || isNaN(Date.parse(header.InvoiceDate))) {
    return { isValid: false, message: 'Enter valid invoice date.' };
  }
  if (!String(header.CurrencyCode ?? '').trim()) {
    return { isValid: false, message: 'Enter currency code.' };
  }
  const currRate = parseFloat(header.CurrencyRate) || 0;
  if (currRate <= 0) {
    return { isValid: false, message: 'Enter currency rate.' };
  }
  if (!String(header.Payable ?? '').trim()) {
    return { isValid: false, message: 'Select payment type.' };
  }
  const isPieceClass = header.PurchaseType === 'P' || header.PurchaseMCType === 'PerPCS';
  if (!isPieceClass) {
    if ((parseFloat(header.TEALPercent) || 0) <= 0) {
      return { isValid: false, message: 'Enter TEAL Percent.' };
    }
    if ((parseFloat(header.TEAL) || 0) <= 0) {
      return { isValid: false, message: 'Enter teal.' };
    }
    if ((parseFloat(header.CostPerGram) || 0) <= 0) {
      return { isValid: false, message: 'Enter cost per gram.' };
    }
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
  const res = await fetch(`${API_URL}/supplier?code=${encodeURIComponent(code)}`);
  return res.json();
};

const fetchCurrencyByCodeApi = async (code: string) => {
  const res = await fetch(`${API_URL}/currency?code=${encodeURIComponent(code)}`);
  return res.json();
};

const fetchCataloguesApi = async () => {
  const res = await fetch(`${API_URL}/catalogue`);
  return res.json();
};

const fetchCatalogueByCodeApi = async (code: string) => {
  try {
    const res = await fetch(`${API_URL}/catalogue/${encodeURIComponent(code)}`);
    if (!res.ok) {
      return { success: false, message: 'Invalid catalogue code!' };
    }
    return await res.json();
  } catch (_) {
    return { success: false, message: 'Invalid catalogue code!' };
  }
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

const fetchOrdersApi = async () => {
  const res = await fetch(`${API_URL}/orderinventory/orders`);
  return res.json();
};

const fetchOrderDetailsApi = async (orderNo: string) => {
  const res = await fetch(`${API_URL}/orderinventory/order-details/${encodeURIComponent(orderNo)}`);
  return res.json();
};

const fetchTradeBillsApi = async () => {
  const res = await fetch(`${API_URL}/tradeinissue/tradebills`);
  return res.json();
};

const fetchGoldLossByWeightApi = async (weight: number) => {
  try {
    const res = await fetch(`${API_URL}/directpurchase/goldloss?weight=${weight}`);
    return await res.json();
  } catch (_) {
    return { success: false, goldLoss: 0 };
  }
};

const fetchCatalogueSummaryApi = async (catalogueCode: string, jobSheetNo: string) => {
  try {
    const res = await fetch(`${API_URL}/directpurchase/catalogue-summary?catalogueCode=${encodeURIComponent(catalogueCode)}&jobSheetNo=${encodeURIComponent(jobSheetNo)}`);
    return await res.json();
  } catch (_) {
    return { success: false, weight: 0, pcs: 0 };
  }
};

const fetchCategoryDetailApi = async (categoryCode: string, jobSheetNo?: string, catalogueCode?: string) => {
  try {
    let url = `${API_URL}/directpurchase/category-detail/${encodeURIComponent(categoryCode)}`;
    const q: string[] = [];
    if (jobSheetNo) q.push(`jobSheetNo=${encodeURIComponent(jobSheetNo)}`);
    if (catalogueCode) q.push(`catalogueCode=${encodeURIComponent(catalogueCode)}`);
    if (q.length > 0) url += `?${q.join('&')}`;
    const res = await fetch(url);
    return await res.json();
  } catch (_) {
    return { success: false };
  }
};

const fetchNextJobSheetNoApi = async () => {
  try {
    let res = await fetch(`${API_URL}/jobsheet/next-number`);
    if (!res.ok) {
      res = await fetch(`${API_URL}/jobsheet/nextNo`);
    }
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
  try {
    const res = await fetch(`${API_URL}/jobsheet/${encodeURIComponent(jsNo)}`);
    if (res.ok) {
      return await res.json();
    }
    // Fallback: check /directpurchase endpoint by jobSheetNo
    const dpRes = await fetch(`${API_URL}/directpurchase/${encodeURIComponent(jsNo)}`);
    if (dpRes.ok) {
      const dpJson = await dpRes.json();
      return {
        success: true,
        header: dpJson.header,
        details: dpJson.details
      };
    }
    return { success: false, message: `Job sheet ${jsNo} not found` };
  } catch (err: any) {
    return { success: false, message: err.message || 'Error fetching jobsheet' };
  }
};

const fetchPurchaseByBillNoApi = async (billNo: string) => {
  const res = await fetch(`${API_URL}/directpurchase/${encodeURIComponent(billNo)}`);
  return res.json();
};

const saveDirectPurchaseApi = async (header: any, details: any[], userId?: string) => {
  const effectiveUserId = (userId || header.userId || header.CreateUser || header.CreatedBy || '').trim();
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
    CreatedBy: effectiveUserId,
    CreateUser: effectiveUserId,
    ModifyUser: effectiveUserId,
    ModifiedBy: effectiveUserId,
  }));

  const payload = {
    userId: effectiveUserId,
    header: {
      ...header,
      userId: effectiveUserId,
      CreatedBy: effectiveUserId,
      CreateUser: effectiveUserId,
      ModifyUser: effectiveUserId,
      ModifiedBy: effectiveUserId,
      PurchaseBillNo: (header.InvoiceNo || header.PurchaseBillNo || '').trim(),
      JobSheetNo: header.JobSheetNo?.trim(),
      PaymentType: header.Payable === 'Gold' ? 'G' : header.Payable === 'Both' ? 'B' : 'C',
    },
    details: mappedDetails,
  };

  const controller = new AbortController();
  const timeoutId = setTimeout(() => controller.abort(), 90000); // 90s timeout for remote cloud DB

  let res: Response;
  try {
    res = await fetch(`${API_URL}/directpurchase`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        ...(effectiveUserId ? { 'x-user-id': effectiveUserId } : {})
      },
      body: JSON.stringify(payload),
      signal: controller.signal,
    });
  } catch (fetchErr: any) {
    clearTimeout(timeoutId);
    if (fetchErr.name === 'AbortError') {
      return { success: false, message: 'Request timed out. Please check database connection.' };
    }
    return { success: false, message: fetchErr.message || 'Network request failed' };
  } finally {
    clearTimeout(timeoutId);
  }
  let json: any = {};
  try {
    json = await res.json();
  } catch (err: any) {
    json = { success: false, message: `Server error (${res.status}): ${res.statusText}` };
  }
  if (!res.ok) {
    json.success = false;
    if (!json.message && !json.error) {
      json.message = `HTTP ${res.status}: ${res.statusText}`;
    }
  }
  return json;
};

const deletePurchaseBillApi = async (billNo: string) => {
  const res = await fetch(`${API_URL}/directpurchase/${encodeURIComponent(billNo)}`, {
    method: 'DELETE',
  });
  return res.json();
};

const deletePurchaseItemApi = async (billNo: string, inventoryCode: string, userId?: string, supplierCode?: string) => {
  let url = `${API_URL}/directpurchase/${encodeURIComponent(billNo)}/item/${encodeURIComponent(inventoryCode)}`;
  const q: string[] = [];
  if (userId) q.push(`userId=${encodeURIComponent(userId)}`);
  if (supplierCode) q.push(`supplierCode=${encodeURIComponent(supplierCode)}`);
  if (q.length > 0) url += `?${q.join('&')}`;

  const res = await fetch(url, {
    method: 'DELETE',
    headers: userId ? { 'x-user-id': userId } : undefined
  });
  return res.json();
};

const closeJobSheetBillApi = async (jsNo: string, invNo?: string, userId?: string) => {
  try {
    const res = await fetch(`${API_URL}/directpurchase/billclose`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        ...(userId ? { 'x-user-id': userId } : {})
      },
      body: JSON.stringify({ jobSheetNo: jsNo, invoiceNo: invNo, userId: userId || '' }),
    });
    return await res.json();
  } catch (err: any) {
    return { success: false, message: err.message || 'Failed to connect to server' };
  }
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
  const params = useLocalSearchParams<{ userId?: string; username?: string }>();

  // Logged-in user authentication state
  const [currentUserId, setCurrentUserId] = useState<string>('');
  const [currentUserName, setCurrentUserName] = useState<string>('');

  useEffect(() => {
    let isMounted = true;
    getLoggedInUser().then((user) => {
      if (isMounted) {
        const uid =
          user.userId ||
          (params?.userId ? String(params.userId) : '') ||
          (params?.username ? String(params.username) : '');
        setCurrentUserId(uid);
        setCurrentUserName(user.userName || (params?.username ? String(params.username) : ''));
      }
    });
    return () => {
      isMounted = false;
    };
  }, [params?.userId, params?.username]);

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

  // Direct Purchase Report Modal States
  const [reportModalVisible, setReportModalVisible] = useState(false);
  const [reportData, setReportData] = useState<any[]>([]);
  const [reportTotals, setReportTotals] = useState<any>({});
  const [reportCompany, setReportCompany] = useState<any>({});
  const [reportLoading, setReportLoading] = useState(false);
  const [reportExporting, setReportExporting] = useState(false);

  const REPORT_COLUMNS = [
    { key: 'JobSheetNo', label: 'Job Sheet No', width: 110 },
    { key: 'PurchaseBillNo', label: 'Bill No', width: 100 },
    { key: 'InventoryCode', label: 'Inv Code', width: 105 },
    { key: 'Description', label: 'Description', width: 140 },
    { key: 'CatalogueCode', label: 'Catalogue', width: 85 },
    { key: 'Weight', label: 'Pur Wt (g)', width: 85, align: 'right', format: 'number' },
    { key: 'GoldLoss', label: 'Gold Loss', width: 80, align: 'right', format: 'number' },
    { key: 'PCS', label: 'PCS', width: 55, align: 'center' },
    { key: 'MC', label: 'MC ($)', width: 80, align: 'right', format: 'currency' },
    { key: 'TotalCost', label: 'Total Cost ($)', width: 95, align: 'right', format: 'currency' },
    { key: 'DetailWeight', label: 'Det Wt (g)', width: 85, align: 'right', format: 'number' },
    { key: 'DetailPCS', label: 'Det PCS', width: 65, align: 'center' },
    { key: 'DetailMC', label: 'Det MC ($)', width: 80, align: 'right', format: 'currency' },
    { key: 'DetailTotalCost', label: 'Det Total ($)', width: 95, align: 'right', format: 'currency' },
    { key: 'SupplierCode', label: 'Supplier', width: 85 },
    { key: 'DiamondDescription', label: 'Diamond Desc', width: 120 },
    { key: 'CreateUser', label: 'User', width: 75 },
  ];

  const handlePrintReport = async () => {
    const jsNo = headerState.JobSheetNo?.trim();
    const invNo = headerState.InvoiceNo?.trim();

    if (!jsNo && !invNo) {
      showToast('Please enter or load a JobSheet / Invoice No. first', 'info');
      return;
    }

    try {
      setReportLoading(true);
      const queryParams = new URLSearchParams();
      if (jsNo) queryParams.append('jobSheetNo', jsNo);
      if (invNo) queryParams.append('purchaseBillNo', invNo);

      const res = await fetch(`${API_URL}/directpurchase/print-report?${queryParams.toString()}`);
      const json = await res.json();

      if (json.success && Array.isArray(json.data) && json.data.length > 0) {
        setReportData(json.data);
        setReportTotals(json.totals || {});
        setReportCompany(json.company || {});
        setReportModalVisible(true);
      } else {
        showToast(json.message || 'No direct purchase records found for this JobSheet/Bill No.', 'error');
      }
    } catch (err: any) {
      console.error('Error fetching print report:', err);
      showToast('Failed to generate direct purchase report', 'error');
    } finally {
      setReportLoading(false);
    }
  };

  const handleExecutePrint = async () => {
    if (!reportData || reportData.length === 0) {
      showToast('No records available to print', 'info');
      return;
    }
    try {
      setReportExporting(true);
      await printHtmlReport({
        endpoint: 'directpurchase',
        title: `Direct Purchase Detail Report (${headerState.JobSheetNo || headerState.InvoiceNo})`,
        columns: REPORT_COLUMNS,
        data: reportData,
        totals: reportTotals,
        fromDate: reportData[0]?.JobSheetDate || reportData[0]?.PurchaseBillDate || '',
        toDate: reportData[0]?.JobSheetDate || reportData[0]?.PurchaseBillDate || '',
      });
    } catch (err: any) {
      console.error('Print Error:', err);
      showToast('Error sending document to printer', 'error');
    } finally {
      setReportExporting(false);
    }
  };

  const handleExportPdf = async () => {
    if (!reportData || reportData.length === 0) {
      showToast('No records available to export', 'info');
      return;
    }
    try {
      setReportExporting(true);
      await downloadPdfReport({
        endpoint: 'directpurchase',
        title: `Direct_Purchase_${headerState.JobSheetNo || headerState.InvoiceNo || 'Report'}`,
        columns: REPORT_COLUMNS,
        data: reportData,
        totals: reportTotals,
        fromDate: reportData[0]?.JobSheetDate || reportData[0]?.PurchaseBillDate || '',
        toDate: reportData[0]?.JobSheetDate || reportData[0]?.PurchaseBillDate || '',
      });
    } catch (err: any) {
      console.error('PDF Error:', err);
      showToast('Failed to export PDF', 'error');
    } finally {
      setReportExporting(false);
    }
  };

  const handleExportExcel = async () => {
    if (!reportData || reportData.length === 0) {
      showToast('No records available to export', 'info');
      return;
    }
    try {
      setReportExporting(true);
      await downloadExcelReport({
        endpoint: 'directpurchase',
        title: `Direct_Purchase_${headerState.JobSheetNo || headerState.InvoiceNo || 'Report'}`,
        columns: REPORT_COLUMNS,
        data: reportData,
        totals: reportTotals,
        fromDate: reportData[0]?.JobSheetDate || reportData[0]?.PurchaseBillDate || '',
        toDate: reportData[0]?.JobSheetDate || reportData[0]?.PurchaseBillDate || '',
      });
    } catch (err: any) {
      console.error('Excel Error:', err);
      showToast('Failed to export Excel', 'error');
    } finally {
      setReportExporting(false);
    }
  };

  const [confirmDialog, setConfirmDialog] = useState<{
    visible: boolean;
    title: string;
    message: string;
    onConfirm: () => void;
    confirmText?: string;
    confirmStyle?: 'danger' | 'primary';
  }>({
    visible: false,
    title: '',
    message: '',
    onConfirm: () => { },
    confirmText: 'OK',
    confirmStyle: 'primary',
  });

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
    // If confirmation dialog with multiple action buttons, use custom beautiful in-app modal (NO window.confirm or native alerts)
    if (buttons && buttons.length > 1) {
      const actionBtn = buttons.find((b: any) => b.style !== 'cancel' && b.text !== 'Cancel');
      const isDanger = actionBtn?.style === 'destructive' || title.toLowerCase().includes('delete');
      setConfirmDialog({
        visible: true,
        title: title || 'Confirmation',
        message,
        confirmText: actionBtn?.text || 'OK',
        confirmStyle: isDanger ? 'danger' : 'primary',
        onConfirm: () => {
          setConfirmDialog((prev) => ({ ...prev, visible: false }));
          if (actionBtn && actionBtn.onPress) {
            actionBtn.onPress();
          }
        },
      });
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

  // Keyboard Shortcuts (F6: BillClear, F7: BillClose, F8: Save, F9: Report, F10: Auto/Manual)
  useEffect(() => {
    if (Platform.OS === 'web' && typeof window !== 'undefined') {
      const handleKeyDown = (e: KeyboardEvent) => {
        if (e.key === 'F6') {
          e.preventDefault();
          handleClearForm();
        } else if (e.key === 'F7') {
          e.preventDefault();
          handleBillClose();
        } else if (e.key === 'F8') {
          e.preventDefault();
          handleSavePurchase();
        } else if (e.key === 'F9') {
          e.preventDefault();
          router.push({
            pathname: '/reports' as any,
            params: {
              reportName: 'PurchaseDetail',
              jobSheetNo: headerState.JobSheetNo,
              invoiceNo: headerState.InvoiceNo,
            },
          });
        } else if (e.key === 'F10') {
          e.preventDefault();
          setIsManual((prev) => {
            const next = !prev;
            showToast(next ? 'Switched to MANUAL mode' : 'Switched to AUTO mode', 'info');
            return next;
          });
        }
      };

      window.addEventListener('keydown', handleKeyDown);
      return () => window.removeEventListener('keydown', handleKeyDown);
    }
  }, [headerState, details, currentUserId]);

  // Initial load: Fetch Company configuration (GST %, etc.)
  useEffect(() => {
    async function initCompany() {
      const comp = await fetchCompanyInfo();
      if (comp) {
        const gstVal = comp.GstPerc || comp.GSTPerc || comp.GSTPercent || comp.GstPercentage || comp.GST || '';
        setHeaderState((prev: any) => ({
          ...prev,
          GSTPercent: prev.GSTPercent && prev.GSTPercent !== '0' ? prev.GSTPercent : (gstVal ? String(gstVal) : prev.GSTPercent),
        }));
      }
    }
    initCompany();
  }, []);

  // Update Header field with automatic TEAL <-> CostPerGram conversions matching VB reference
  const updateHeader = (key: string, value: any) => {
    let updatedCostPerGram: string | null = null;

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
          updatedCostPerGram = costG;
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
        updatedCostPerGram = value;
      } else if (key === 'GSTActive') {
        const costG = calculateCostPerGramFromTeal(
          next.TEAL,
          next.TEALPercent,
          value,
          next.GSTPercent
        );
        if (parseFloat(costG) > 0) {
          next.CostPerGram = costG;
          updatedCostPerGram = costG;
        }
      }

      return next;
    });

    // When Header CostPerGram changes (directly or computed from TEAL / GST),
    // automatically sync with line items and currently active line detail
    if (updatedCostPerGram !== null) {
      const cpgVal = updatedCostPerGram;
      setCurrentDetail((d: any) => recalculateDetailCosts({ ...d, CostPerGram: cpgVal }, autoSMC, headerState.CurrencyRate));
      setDetails((prevList: any[]) =>
        prevList.map((item: any) => recalculateDetailCosts({ ...item, CostPerGram: cpgVal }, autoSMC, headerState.CurrencyRate))
      );
    }
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
      showToast(e.message || 'Error fetching suppliers', 'error');
    }
  };

  const fetchCurrencies = async () => {
    try {
      setLoading(true);
      const json = await fetchCurrenciesApi();
      setLoading(false);
      if (json.success && Array.isArray(json.data)) {
        openLov('Select Currency', json.data, ['CurrencyCode', 'CurrencyName', 'CurrencyRate'], (item) => {
          updateHeader('CurrencyCode', item.CurrencyCode || '');
          updateHeader('CurrencyName', item.CurrencyName || item.CurrencyCode || '');
          updateHeader('CurrencyRate', (item.CurrencyRate ?? item.Rate ?? '').toString());
        });
      }
    } catch (e: any) {
      setLoading(false);
      showToast(e.message || 'Error fetching currencies', 'error');
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
      showToast(e.message || 'Error fetching TEAL percentages', 'error');
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
          async (item) => {
            const trimmedItemCat = (item.CategoryCode || '').trim();
            const catCode = trimmedItemCat || (currentDetail.CategoryCode || '').trim();
            const catalogueCodeVal = (item.CatalogueCode || '').trim();

            const matchedCategory = categoriesList.find(
              (c: any) => (c.CategoryCode || '').trim().toUpperCase() === catCode.toUpperCase()
            );

            // Fetch enriched category detail (SizeFactor, ClassCode, NextInventoryCode, CostPerGram from JobSheetDetail)
            let catDetailData: any = null;
            let costPerGramVal: any = null;
            if (catCode) {
              try {
                const catDetRes = await fetchCategoryDetailApi(catCode, headerState.JobSheetNo, catalogueCodeVal);
                if (catDetRes.success && catDetRes.category) {
                  catDetailData = catDetRes.category;
                  costPerGramVal = catDetRes.costPerGram;
                }
              } catch (_) { }
            }

            // Fetch Catalogue Summary (Catalogue Weight & PCS)
            let catSummary: any = { weight: 0, pcs: 0 };
            if (catalogueCodeVal && headerState.JobSheetNo) {
              try {
                catSummary = await fetchCatalogueSummaryApi(catalogueCodeVal, headerState.JobSheetNo);
              } catch (_) { }
            }

            const updates: any = {
              CatalogueCode: catalogueCodeVal,
              CategoryCode: catCode,
              TrayId: (item.TrayId || currentDetail.TrayId || '').trim(),
              InventoryDescription: item.CatalogueName || currentDetail.InventoryDescription,
              CatalogueWeight: catSummary.weight ? Number(catSummary.weight).toFixed(2) : '0.00',
              CataloguePCS: catSummary.pcs ? String(catSummary.pcs) : '0',
              SizeFactor: catDetailData ? !!catDetailData.SizeFactor : (matchedCategory ? !!matchedCategory.SizeFactor : false),
              ClassCode: catDetailData?.ClassCode || matchedCategory?.ClassCode || (catCode ? catCode.substring(0, 3) : ''),
              CostPerGram: costPerGramVal != null ? String(costPerGramVal) : (currentDetail.CostPerGram || headerState.CostPerGram || '0.00'),
              PCS: currentDetail.PCS || '1',
            };

            const nextInv = catDetailData?.NextInventoryCode || matchedCategory?.NextInventoryCode;
            if (!isManual && nextInv) {
              updates.InventoryCode = nextInv.trim();
            }

            if (item.PurchaseType === 'P' || item.SalesType === 'P') {
              updates.PurchaseMCType = item.PurchaseType === 'P' ? 'PerPCS' : currentDetail.PurchaseMCType;
              updates.SalesMCType = item.SalesType === 'P' ? 'PerPCS' : currentDetail.SalesMCType;
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
              SizeFactor: !!item.SizeFactor,
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
          updateDetail('SizeCode', String(item.SizeCode ?? ''));
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
      showToast(e.message || 'Error fetching countries', 'error');
    }
  };

  const fetchTradeInSearch = async () => {
    try {
      setLoading(true);
      const json = await fetchTradeBillsApi();
      setLoading(false);
      if (json.success && Array.isArray(json.data)) {
        openLov('Search Trade In Inventory', json.data, ['InventoryCode', 'InvoiceNo', 'Balanceweight', 'TotalWeight'], (item) => {
          updateDetail({
            TradeInInventoryCode: item.InventoryCode || '',
            TradeInDescription: item.Description || item.InventoryCode || '',
            TradeInInvoice: item.InvoiceNo || '',
            TradeInWeight: (item.Balanceweight != null ? item.Balanceweight : item.TotalWeight || '').toString(),
          });
        });
      }
    } catch (e: any) {
      setLoading(false);
      showToast(e.message || 'Error searching trade-in inventory', 'error');
    }
  };

  const fetchOrdersSearch = async () => {
    try {
      setLoading(true);
      const json = await fetchOrdersApi();
      setLoading(false);
      if (json.success && Array.isArray(json.data)) {
        openLov('Select Order No', json.data, ['OrderNo', 'CustomerCode', 'SupplierCode', 'OrderDate'], (item) => {
          updateHeader('OrderNo', item.OrderNo || '');
          if (item.SupplierCode) {
            updateHeader('SupplierCode', item.SupplierCode);
            fetchSupplierByCodeApi(item.SupplierCode).then((sJson) => {
              if (sJson.success && sJson.data) {
                updateHeader('SupplierName', sJson.data.SupplierName || '');
              }
            }).catch(() => { });
          }
        });
      }
    } catch (e: any) {
      setLoading(false);
      showToast(e.message || 'Error fetching orders', 'error');
    }
  };

  const fetchOrderInventoryDetailsSearch = async () => {
    const orderNo = (headerState.OrderNo || '').trim();
    if (!orderNo) {
      showToast('Please select or enter Order No first', 'info');
      return;
    }
    try {
      setLoading(true);
      const json = await fetchOrderDetailsApi(orderNo);
      setLoading(false);
      if (json.success && Array.isArray(json.data)) {
        openLov('Select Order Inventory Item', json.data, ['OrderInventoryCode', 'Description', 'WeightEstimated'], (item) => {
          updateDetail({
            OrderInventoryCode: item.OrderInventoryCode || '',
            InventoryDescription: item.Description || currentDetail.InventoryDescription,
            Weight: item.WeightEstimated ? String(item.WeightEstimated) : currentDetail.Weight,
          });
        });
      }
    } catch (e: any) {
      setLoading(false);
      showToast(e.message || 'Error fetching order items', 'error');
    }
  };

  const loadJobSheetByNo = async (jsNo: string, isManualBlur = false) => {
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
          FixUnFix: (h.FixStatus === 'Y' || h.FixStatus === 'Fix') ? 'Fix' : 'UnFix',
        }));

        const rawDetails = (jsJson.purchaseDetails && jsJson.purchaseDetails.length > 0)
          ? jsJson.purchaseDetails
          : ((jsJson.inventory && jsJson.inventory.length > 0)
            ? jsJson.inventory
            : (jsJson.details || []));

        if (rawDetails && rawDetails.length > 0) {
          const mappedDetails = rawDetails.map((d: any) => ({
            CatalogueCode: d.CatalogueCode || '',
            CategoryCode: d.CategoryCode || '',
            TrayId: d.TrayId || '',
            CatalogueWeight: '',
            CataloguePCS: '',
            PurchaseMCType: (d.PMCType === 'P' || d.PurchaseType === 'P') ? 'PerPCS' : 'PerGram',
            SalesMCType: d.SMCType === 'P' ? 'PerPCS' : 'PerGram',
            Auto: true,
            PCSCheck: false,
            InventoryCode: d.InventoryCode || d.NextInventoryCode || '',
            InventoryDescription: d.Description || d.InventoryDescription || '',
            Weight: d.Weight != null ? d.Weight.toString() : (d.PurchaseWeight != null ? d.PurchaseWeight.toString() : ''),
            SizeCode: d.SizeCode?.toString() || d.Size?.toString() || '',
            GoldLoss: d.GoldLoss != null ? d.GoldLoss.toString() : '0.000',
            CostPerGram: d.CostPerGram != null ? d.CostPerGram.toString() : (d.PurchaseCostPerGram != null ? d.PurchaseCostPerGram.toString() : ''),
            PMC: d.PMC != null ? d.PMC.toString() : (d.MC != null ? d.MC.toString() : '0.00'),
            PMCCost: (d.MC != null ? d.MC : (d.PMC != null ? d.PMC : '0.00')).toString(),
            SMC: d.SMC != null ? d.SMC.toString() : '0.00',
            SMCCost: (d.SMC != null ? d.SMC : '0.00').toString(),
            PCS: d.PCS != null ? d.PCS.toString() : '1',
            TotalCost: d.TotalCost != null ? d.TotalCost.toString() : '',
            ClassCode: d.ClassCode || (d.CategoryCode ? d.CategoryCode.toString().substring(0, 3) : ''),
            NetWeight: (parseFloat(d.Weight || d.PurchaseWeight || 0) - parseFloat(d.StoneWeight || 0)).toFixed(3),
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
        if (!isManualBlur) {
          showToast('Job sheet no. is invalid!', 'error');
          handleClearForm();
        }
      }
    } catch (e: any) {
      setLoading(false);
      if (!isManualBlur) {
        showToast(e.message || 'Failed to load JobSheet details', 'error');
      }
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
      showToast(e.message || 'Error fetching jobsheet', 'error');
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
              const isClosed = (h.BillClose || '').toUpperCase() === 'Y';
              const tranType = (h.TranType || '').toUpperCase();

              if (isClosed) {
                showToast('This bill is closed! Cannot modify!', 'info');
              } else if (tranType && tranType !== 'DP') {
                showToast('This is not a direct purchase bill. Cannot modify!', 'info');
              }

              setHeaderState((prev: any) => ({
                ...prev,
                JobSheetNo: h.JobSheetNo || '',
                InvoiceNo: h.PurchaseBillNo || '',
                InvoiceDate: h.PurchaseBillDate ? h.PurchaseBillDate.split('T')[0] : (h.CreateDate ? h.CreateDate.split('T')[0] : prev.InvoiceDate),
                SupplierCode: h.SupplierCode || '',
                SupplierName: h.SupplierName || '',
                CurrencyCode: h.CurrencyCode || 'SGD',
                CurrencyName: h.CurrencyName || '',
                CurrencyRate: h.CurrencyRate != null ? h.CurrencyRate.toString() : '1',
                CostPerGram: h.CostPerGram?.toString() || prev.CostPerGram,
                TotalCost: h.TotalCost?.toString() || prev.TotalCost,
                TotalMC: h.TotalMC?.toString() || prev.TotalMC,
                Weight: h.TotalWeight24K?.toString() || prev.Weight,
                GoldLoss: h.TotalGoldLoss?.toString() || prev.GoldLoss,
                GSTPercent: h.GST?.toString() || prev.GSTPercent,
                GSTActive: (h.TaelGST || '').toUpperCase() === 'Y',
                HGSTActive: parseFloat(h.GST || 0) > 0,
                TEAL: (h.TaelRate || 0).toString(),
                TEALPercent: (h.TaelPercent || 0).toString(),
                Payable: h.PaymentType === 'G' ? 'Gold' : h.PaymentType === 'C' ? 'Cash' : 'Both',
                FixUnFix: h.FixStatus === 'Y' ? 'Fix' : 'UnFix',
                Remarks: h.Remarks || '',
                BillClose: isClosed ? 'Y' : 'N',
                TranType: tranType || 'DP',
              }));

              if (pJson.details && pJson.details.length > 0) {
                const mappedDetails = pJson.details.map((d: any) => ({
                  CatalogueCode: d.CatalogueCode || '',
                  CategoryCode: d.CategoryCode || '',
                  TrayId: d.TrayId || '',
                  CatalogueWeight: '',
                  CataloguePCS: '',
                  PurchaseMCType: (d.PMCType === 'P' || d.PurchaseType === 'P') ? 'PerPCS' : 'PerGram',
                  SalesMCType: d.SMCType === 'P' ? 'PerPCS' : 'PerGram',
                  Auto: true,
                  PCSCheck: false,
                  InventoryCode: d.InventoryCode || '',
                  InventoryDescription: d.Description || d.InventoryDescription || '',
                  Weight: d.Weight?.toString() || '',
                  SizeCode: d.SizeCode?.toString() || d.Size?.toString() || '',
                  GoldLoss: d.GoldLoss != null ? d.GoldLoss.toString() : '0.000',
                  CostPerGram: d.CostPerGram != null ? d.CostPerGram.toString() : '',
                  PMC: d.PMC != null ? d.PMC.toString() : (d.MC != null ? d.MC.toString() : '0.00'),
                  PMCCost: (d.MC != null ? d.MC : (d.PMC != null ? d.PMC : '0.00')).toString(),
                  SMC: d.SMC != null ? d.SMC.toString() : '0.00',
                  SMCCost: (d.SMC != null ? d.SMC : '0.00').toString(),
                  PCS: d.PCS != null ? d.PCS.toString() : '1',
                  TotalCost: d.TotalCost != null ? d.TotalCost.toString() : '',
                  ClassCode: d.ClassCode || '',
                  NetWeight: (parseFloat(d.Weight || 0) - parseFloat(d.StoneWeight || 0)).toFixed(3),
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
            showToast(err.message || 'Failed to load purchase details', 'error');
          }
        });
      }
    } catch (e: any) {
      setLoading(false);
      showToast(e.message || 'Error fetching purchases', 'error');
    }
  };

  // Line Item Add / Update / Delete / Clear
  const handleAddOrUpdateDetail = () => {
    // Sync CostPerGram from header if left blank and normalize string values
    const itemToValidate = {
      ...currentDetail,
      SizeCode: currentDetail.SizeCode != null ? String(currentDetail.SizeCode).trim() : '',
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

    setCurrentDetail({
      ...getInitialDetailState(),
      CostPerGram: headerState.CostPerGram || '',
    });
    setDetailModalVisible(false);
  };

  const handleEditDetail = (index: number) => {
    setSelectedIndex(index);
    setCurrentDetail({ ...details[index] });
    setDetailModalVisible(true);
  };

  const handleDeleteDetail = async () => {
    if (selectedIndex === null) {
      showToast('Please select a line item from the grid to delete', 'info');
      return;
    }

    if (headerState.BillClose === 'Y') {
      showToast('This bill is closed. Cannot delete!', 'info');
      return;
    }

    if (headerState.TranType === 'JP') {
      showToast('This is not a direct purchase bill. Cannot delete!', 'info');
      return;
    }

    const itemToDelete = details[selectedIndex];
    const itemCode = (itemToDelete?.InventoryCode || '').trim();

    showAlert('Confirmation', `Do you want to delete item no. ${itemCode || selectedIndex + 1}?`, [
      { text: 'Cancel', style: 'cancel' },
      {
        text: 'Delete',
        style: 'destructive',
        onPress: async () => {
          try {
            // If the item exists in the backend under InvoiceNo, call backend delete
            if (headerState.InvoiceNo?.trim() && itemCode) {
              setLoading(true);
              let uId = currentUserId;
              if (!uId) {
                const u = await getLoggedInUser();
                uId = u.userId || (params?.userId ? String(params.userId) : '') || (params?.username ? String(params.username) : '');
                if (uId) setCurrentUserId(uId);
              }
              const delRes = await deletePurchaseItemApi(
                headerState.InvoiceNo.trim(),
                itemCode,
                uId,
                headerState.SupplierCode
              );
              setLoading(false);
              if (!delRes.success) {
                showToast(delRes.message || 'Cannot delete item!', 'error');
                return;
              }
            }

            const updated = details.filter((_, i) => i !== selectedIndex);
            setDetails(updated);
            setSelectedIndex(null);
            setCurrentDetail(getInitialDetailState());
            showToast('Item deleted successfully', 'success');
          } catch (err: any) {
            setLoading(false);
            showToast(err.message || 'Error deleting item', 'error');
          }
        },
      },
    ]);
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
    if (headerState.BillClose === 'Y') {
      showToast('This bill is closed. Cannot modify!', 'error');
      return;
    }

    const validation = validatePurchaseSave(headerState, details);
    if (!validation.isValid) {
      showToast(validation.message, 'error');
      return;
    }

    try {
      setSaving(true);
      let uId = currentUserId;
      if (!uId) {
        const u = await getLoggedInUser();
        uId =
          u.userId ||
          (params?.userId ? String(params.userId) : '') ||
          (params?.username ? String(params.username) : '');
        if (uId) setCurrentUserId(uId);
      }

      let jsNo = (headerState.JobSheetNo || '').trim();
      if (!jsNo) {
        jsNo = await fetchNextJobSheetNoApi();
        if (jsNo) {
          setHeaderState((prev: any) => ({ ...prev, JobSheetNo: jsNo }));
        }
      }

      let invoiceNo = (headerState.InvoiceNo || '').trim();
      if (!invoiceNo) {
        invoiceNo = jsNo || `DP${Date.now().toString().slice(-8)}`;
        setHeaderState((prev: any) => ({ ...prev, InvoiceNo: invoiceNo }));
      }

      const saveHeader = {
        ...headerState,
        userId: uId,
        JobSheetNo: jsNo || headerState.JobSheetNo,
        InvoiceNo: invoiceNo,
      };

      const res = await saveDirectPurchaseApi(saveHeader, details, uId);

      if (res && res.success) {
        showToast('Direct purchase saved successfully!', 'success');
        if (jsNo) {
          loadJobSheetByNo(jsNo, true);
        }
      } else {
        const errorMsg = res?.message || res?.error || 'Failed to save direct purchase';
        showToast(errorMsg, 'error');
      }
    } catch (e: any) {
      const errorMsg = e.message || 'Error occurred while saving direct purchase';
      showToast(errorMsg, 'error');
    } finally {
      setSaving(false);
    }
  };

  // Bill Close Operation
  const handleBillClose = async () => {
    const jsNo = (headerState.JobSheetNo || '').trim();
    const invNo = (headerState.InvoiceNo || '').trim();

    if (!jsNo && !invNo) {
      showToast('Please enter the invoice no.!', 'error');
      return;
    }

    if (headerState.BillClose === 'Y') {
      showToast('This Job sheet bill is already closed!', 'info');
      return;
    }

    if (headerState.TranType === 'JP') {
      showToast('This is not a direct purchase bill. Use job sheet purchase screen to close this bill!', 'info');
      return;
    }

    showAlert('Confirmation', 'You cannot make changes to closed bill. Would u like to close this purchase bill?', [
      { text: 'Cancel', style: 'cancel' },
      {
        text: 'Close Bill',
        onPress: async () => {
          try {
            setLoading(true);
            let uId = currentUserId;
            if (!uId) {
              const u = await getLoggedInUser();
              uId =
                u.userId ||
                (params?.userId ? String(params.userId) : '') ||
                (params?.username ? String(params.username) : '');
              if (uId) setCurrentUserId(uId);
            }
            const res = await closeJobSheetBillApi(jsNo, invNo, uId);
            setLoading(false);
            if (res.success) {
              setHeaderState((prev: any) => ({ ...prev, BillClose: 'Y' }));
              setCurrentDetail(getInitialDetailState());
              setSelectedIndex(null);
              setDetailModalVisible(false);
              showToast('Purchase bill closed!', 'success');
            } else {
              const msg = res.message || 'Failed to close bill';
              showToast(msg, 'error');
            }
          } catch (e: any) {
            setLoading(false);
            const msg = e.message || 'Error closing bill';
            showToast(msg, 'error');
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
              {/* <TouchableOpacity
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
              </TouchableOpacity> */}

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

              {/* Bill Clear Button (Clears entire bill and unlocks header) */}
              <TouchableOpacity style={styles.topActionBtn} onPress={handleClearForm}>
                <ThemedText style={{ color: '#5C4A28', fontWeight: 'bold', fontSize: 11 }}>Clear</ThemedText>
              </TouchableOpacity>



              {/* Print Button */}
              <TouchableOpacity
                style={[
                  styles.topActionBtn,
                  { backgroundColor: '#FFF8E1', borderColor: '#FFA000', flexDirection: 'row', alignItems: 'center', gap: 4 }
                ]}
                onPress={handlePrintReport}
                disabled={reportLoading}
              >
                {reportLoading ? (
                  <ActivityIndicator size="small" color="#B8860B" />
                ) : (
                  <>
                    <Ionicons name="print-outline" size={13} color="#8D6E63" />
                    <ThemedText style={{ color: '#8D6E63', fontWeight: 'bold', fontSize: 11 }}>Print</ThemedText>
                  </>
                )}
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
                        if (headerState.JobSheetNo) loadJobSheetByNo(headerState.JobSheetNo, true);
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
                      onBlur={() => {
                        if (!headerState.InvoiceNo?.trim()) {
                          showToast('Enter invoice no.', 'error');
                        }
                      }}
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
                    onBlur={() => {
                      if (!headerState.InvoiceDate || isNaN(Date.parse(headerState.InvoiceDate))) {
                        showToast('Enter valid invoice date.', 'error');
                      }
                    }}
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
                      onBlur={async () => {
                        const sCode = (headerState.SupplierCode || '').trim();
                        if (!sCode) {
                          showToast('Enter supplier code.', 'error');
                          return;
                        }
                        try {
                          const sJson = await fetchSupplierByCodeApi(sCode);
                          if (sJson.success && sJson.data) {
                            updateHeader('SupplierName', sJson.data.SupplierName || '');
                          } else {
                            showToast('Invalid supplier code!', 'error');
                            updateHeader('SupplierCode', '');
                            updateHeader('SupplierName', '');
                          }
                        } catch (_) { }
                      }}
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
                      onBlur={async () => {
                        const cCode = (headerState.CurrencyCode || '').trim();
                        if (!cCode) {
                          showToast('Enter currency code.', 'error');
                          return;
                        }
                        try {
                          const cJson = await fetchCurrencyByCodeApi(cCode);
                          if (cJson.success && cJson.data) {
                            updateHeader('CurrencyName', cJson.data.CurrencyName || cCode);
                            if (cJson.data.CurrencyRate != null) {
                              updateHeader('CurrencyRate', String(cJson.data.CurrencyRate));
                            }
                          } else {
                            showToast('Set Default Currency in Currency Master', 'error');
                            updateHeader('CurrencyCode', '');
                            updateHeader('CurrencyName', '');
                            updateHeader('CurrencyRate', '');
                          }
                        } catch (_) { }
                      }}
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
                    style={[styles.input, styles.readonlyInput, { width: 90, textAlign: 'right' }]}
                    value={headerState.CurrencyRate}
                    editable={false}
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
                    onPress={() => {
                      const willCheck = !headerState.GSTActive;
                      if (willCheck && (parseFloat(headerState.GSTPercent) || 0) <= 0) {
                        showToast('Set GST Percentage in Company Master', 'error');
                      }
                      updateHeader('GSTActive', willCheck);
                    }}
                  />

                  {/* TEAL % */}
                  <View style={{ flexDirection: 'row', marginLeft: 10, alignItems: 'center' }}>
                    <ThemedText style={{ color: '#4A3B25', fontSize: 11, marginRight: 5, fontWeight: 'bold' }}>TEAL %</ThemedText>
                    <TextInput
                      style={[styles.input, { width: 50, textAlign: 'center', borderTopRightRadius: 0, borderBottomRightRadius: 0 }]}
                      value={headerState.TEALPercent}
                      onChangeText={(t) => updateHeader('TEALPercent', t)}
                      onBlur={() => {
                        const isPiece = (currentDetail as any)?.PurchaseType === 'P' || (currentDetail as any)?.PurchaseMCType === 'PerPCS';
                        if (!isPiece && (!headerState.TEALPercent || (parseFloat(headerState.TEALPercent) || 0) <= 0)) {
                          showToast('Enter TEAL Percent.', 'error');
                        }
                      }}
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
                      onBlur={() => {
                        const isPiece = (currentDetail as any)?.PurchaseType === 'P' || (currentDetail as any)?.PurchaseMCType === 'PerPCS';
                        if (!isPiece && (parseFloat(headerState.TEAL) || 0) <= 0) {
                          showToast('Enter teal.', 'error');
                        }
                      }}
                      keyboardType="numeric"
                    />

                    <ThemedText style={{ color: '#4A3B25', fontSize: 11, marginLeft: 8, marginRight: 5, fontWeight: 'bold' }}>Cost/g</ThemedText>
                    <TextInput
                      style={[styles.input, { width: 66, textAlign: 'right' }]}
                      value={headerState.CostPerGram}
                      onChangeText={(t) => updateHeader('CostPerGram', t)}
                      onBlur={() => {
                        const isPiece = (currentDetail as any)?.PurchaseType === 'P' || (currentDetail as any)?.PurchaseMCType === 'PerPCS';
                        if (!isPiece && (parseFloat(headerState.CostPerGram) || 0) <= 0) {
                          showToast('Enter cost per gram.', 'error');
                        }
                      }}
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
                      <View style={{ flexDirection: 'row', marginLeft: 4 }}>
                        <TextInput
                          style={[styles.input, { width: 85, borderTopRightRadius: 0, borderBottomRightRadius: 0 }]}
                          placeholder="Order No"
                          value={headerState.OrderNo}
                          onChangeText={(t) => updateHeader('OrderNo', t)}
                        />
                        <TouchableOpacity
                          style={[styles.lookupBtn, { width: 26 }]}
                          onPress={fetchOrdersSearch}
                        >
                          <ThemedText style={styles.lookupBtnText}>...</ThemedText>
                        </TouchableOpacity>
                      </View>
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
                style={[styles.addLineBtn, isFormLocked && { backgroundColor: '#BDBDBD' }]}
                onPress={() => {
                  if (isFormLocked) {
                    showToast('This bill is closed. Cannot add lines!', 'info');
                    return;
                  }
                  setSelectedIndex(null);
                  setCurrentDetail({
                    ...getInitialDetailState(),
                    CostPerGram: headerState.CostPerGram || '',
                  });
                  setDetailModalVisible(true);
                }}
                disabled={isFormLocked}
              >
                <Ionicons name="add" size={14} color="#FFF" style={{ marginRight: 4 }} />
                <ThemedText style={styles.addLineBtnText}>New Line</ThemedText>
              </TouchableOpacity>
            </View>
          </View>

          <ScrollView horizontal contentContainerStyle={{ width: '100%' }}>
            <View style={{ width: '100%', minWidth: 1250 }}>
              <View style={styles.tableHeader}>
                <ThemedText style={[styles.tableHeaderText, { width: 120, textAlign: 'center' }]}>Item Code</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { width: 60, textAlign: 'center' }]}>Class</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { flex: 2, minWidth: 180, textAlign: 'center' }]}>Description</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { flex: 1.5, minWidth: 150, textAlign: 'center' }]}>Stone Description</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { width: 65, textAlign: 'center' }]}>SizeCode</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { width: 75, textAlign: 'right' }]}>Weight</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { width: 75, textAlign: 'right' }]}>Stone Wt</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { width: 75, textAlign: 'right' }]}>Net Wt</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { width: 75, textAlign: 'right' }]}>GoldLoss</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { width: 85, textAlign: 'right' }]}>CostPerGram</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { width: 75, textAlign: 'right' }]}>PMC</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { width: 75, textAlign: 'right' }]}>SMC</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { width: 45, textAlign: 'right' }]}>PCS</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { flex: 1, minWidth: 105, textAlign: 'right' }]}>TotalCost</ThemedText>
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
                      onPress={() => {
                        if (isFormLocked) {
                          showToast('This bill is closed. Cannot edit lines!', 'info');
                          return;
                        }
                        handleEditDetail(idx);
                      }}
                    >
                      <ThemedText style={[styles.tableCellText, { width: 120, fontWeight: 'bold' }]}>{item.InventoryCode}</ThemedText>
                      <ThemedText style={[styles.tableCellText, { width: 60, textAlign: 'center' }]}>{item.ClassCode}</ThemedText>
                      <ThemedText style={[styles.tableCellText, { flex: 2, minWidth: 180 }]}>{item.InventoryDescription}</ThemedText>
                      <ThemedText style={[styles.tableCellText, { flex: 1.5, minWidth: 150 }]}>{item.DiamondDescription || ''}</ThemedText>
                      <ThemedText style={[styles.tableCellText, { width: 65, textAlign: 'center' }]}>{item.SizeCode}</ThemedText>
                      <ThemedText style={[styles.tableCellText, { width: 75, textAlign: 'right' }]}>{parseFloat(item.Weight || 0).toFixed(3)}</ThemedText>
                      <ThemedText style={[styles.tableCellText, { width: 75, textAlign: 'right' }]}>{parseFloat(item.StoneWeight || 0).toFixed(3)}</ThemedText>
                      <ThemedText style={[styles.tableCellText, { width: 75, textAlign: 'right' }]}>{parseFloat(item.NetWeight || (parseFloat(item.Weight || 0) - parseFloat(item.StoneWeight || 0)) || 0).toFixed(3)}</ThemedText>
                      <ThemedText style={[styles.tableCellText, { width: 75, textAlign: 'right' }]}>{parseFloat(item.GoldLoss || 0).toFixed(3)}</ThemedText>
                      <ThemedText style={[styles.tableCellText, { width: 85, textAlign: 'right' }]}>{parseFloat(item.CostPerGram || 0).toFixed(2)}</ThemedText>
                      <ThemedText style={[styles.tableCellText, { width: 75, textAlign: 'right' }]}>{parseFloat(item.PMC || 0).toFixed(2)}</ThemedText>
                      <ThemedText style={[styles.tableCellText, { width: 75, textAlign: 'right' }]}>{parseFloat(item.SMC || 0).toFixed(2)}</ThemedText>
                      <ThemedText style={[styles.tableCellText, { width: 45, textAlign: 'right' }]}>{item.PCS || '1'}</ThemedText>
                      <ThemedText style={[styles.tableCellText, { flex: 1, minWidth: 105, textAlign: 'right', fontWeight: 'bold' }]}>{parseFloat(item.TotalCost || 0).toFixed(2)}</ThemedText>
                    </TouchableOpacity>
                  ))
                )}
              </ScrollView>

              {/* TOTALS ROW */}
              <View style={styles.tableTotalRow}>
                <ThemedText style={[styles.tableTotalCellText, { width: 120, textAlign: 'center' }]}>TOTAL :</ThemedText>
                <View style={{ width: 60 }} />
                <View style={{ flex: 2, minWidth: 180 }} />
                <View style={{ flex: 1.5, minWidth: 150 }} />
                <View style={{ width: 65 }} />
                <ThemedText style={[styles.tableTotalCellText, { width: 75, textAlign: 'right' }]}>
                  {details.reduce((sum, i) => sum + (parseFloat(i.Weight) || 0), 0).toFixed(3)}
                </ThemedText>
                <ThemedText style={[styles.tableTotalCellText, { width: 75, textAlign: 'right' }]}>
                  {details.reduce((sum, i) => sum + (parseFloat(i.StoneWeight) || 0), 0).toFixed(3)}
                </ThemedText>
                <ThemedText style={[styles.tableTotalCellText, { width: 75, textAlign: 'right' }]}>
                  {details.reduce((sum, i) => sum + (parseFloat(i.NetWeight || (parseFloat(i.Weight || 0) - parseFloat(i.StoneWeight || 0))) || 0), 0).toFixed(3)}
                </ThemedText>
                <ThemedText style={[styles.tableTotalCellText, { width: 75, textAlign: 'right' }]}>
                  {details.reduce((sum, i) => sum + (parseFloat(i.GoldLoss) || 0), 0).toFixed(3)}
                </ThemedText>
                <View style={{ width: 85 }} />
                <ThemedText style={[styles.tableTotalCellText, { width: 75, textAlign: 'right' }]}>
                  {details.reduce((sum, i) => sum + (parseFloat(i.PMCCost || i.PMC) || 0), 0).toFixed(2)}
                </ThemedText>
                <ThemedText style={[styles.tableTotalCellText, { width: 75, textAlign: 'right' }]}>
                  {details.reduce((sum, i) => sum + (parseFloat(i.SMCCost || i.SMC) || 0), 0).toFixed(2)}
                </ThemedText>
                <ThemedText style={[styles.tableTotalCellText, { width: 45, textAlign: 'right' }]}>
                  {details.reduce((sum, i) => sum + (parseFloat(i.PCS) || 1), 0)}
                </ThemedText>
                <ThemedText style={[styles.tableTotalCellText, { flex: 1, minWidth: 105, textAlign: 'right' }]}>
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
                        onBlur={async () => {
                          const catCode = (currentDetail.CatalogueCode || '').trim();
                          if (!catCode) {
                            showToast('Catalogue Code Cannot be Blank ....', 'error');
                            return;
                          }
                          try {
                            const cRes = await fetchCatalogueByCodeApi(catCode);
                            if (cRes.success && cRes.data) {
                              const item = cRes.data;
                              const categoryCodeVal = (item.CategoryCode || currentDetail.CategoryCode || '').trim();

                              // Fetch enriched category detail (SizeFactor, ClassCode, NextInventoryCode, CostPerGram from JobSheetDetail)
                              let catDetailData: any = null;
                              let costPerGramVal: any = null;
                              if (categoryCodeVal) {
                                try {
                                  const catDetRes = await fetchCategoryDetailApi(categoryCodeVal, headerState.JobSheetNo, item.CatalogueCode);
                                  if (catDetRes.success && catDetRes.category) {
                                    catDetailData = catDetRes.category;
                                    costPerGramVal = catDetRes.costPerGram;
                                  }
                                } catch (_) { }
                              }

                              // Fetch Catalogue Summary (Catalogue Weight & PCS)
                              let catSummary: any = { weight: 0, pcs: 0 };
                              if (item.CatalogueCode && headerState.JobSheetNo) {
                                try {
                                  catSummary = await fetchCatalogueSummaryApi(item.CatalogueCode, headerState.JobSheetNo);
                                } catch (_) { }
                              }

                              const updates: any = {
                                CatalogueCode: item.CatalogueCode || catCode,
                                CategoryCode: categoryCodeVal,
                                TrayId: (item.TrayId || currentDetail.TrayId || '').trim(),
                                InventoryDescription: item.CatalogueName || currentDetail.InventoryDescription,
                                CatalogueWeight: catSummary.weight ? Number(catSummary.weight).toFixed(2) : '0.00',
                                CataloguePCS: catSummary.pcs ? String(catSummary.pcs) : '0',
                                SizeFactor: catDetailData ? !!catDetailData.SizeFactor : false,
                                ClassCode: catDetailData?.ClassCode || (categoryCodeVal ? categoryCodeVal.substring(0, 3) : ''),
                                CostPerGram: costPerGramVal != null ? String(costPerGramVal) : (currentDetail.CostPerGram || headerState.CostPerGram || '0.00'),
                                PCS: currentDetail.PCS || '1',
                              };

                              if (!isManual && catDetailData?.NextInventoryCode) {
                                updates.InventoryCode = catDetailData.NextInventoryCode.trim();
                              }

                              if (item.PurchaseType === 'P' || item.SalesType === 'P') {
                                updates.PurchaseMCType = item.PurchaseType === 'P' ? 'PerPCS' : currentDetail.PurchaseMCType;
                                updates.SalesMCType = item.SalesType === 'P' ? 'PerPCS' : currentDetail.SalesMCType;
                              }

                              updateDetail(updates);
                            } else {
                              showToast('Invalid catalogue code!', 'error');
                              updateDetail({ CatalogueCode: '', CategoryCode: '', InventoryCode: '' });
                            }
                          } catch (_) {
                            showToast('Invalid catalogue code!', 'error');
                            updateDetail({ CatalogueCode: '', CategoryCode: '', InventoryCode: '' });
                          }
                        }}
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
                        onBlur={async () => {
                          const catg = (currentDetail.CategoryCode || '').trim();
                          if (!catg) {
                            showToast('Category Code Cannot be Blank ....', 'error');
                            return;
                          }
                          try {
                            const catDetailRes = await fetchCategoryDetailApi(catg, headerState.JobSheetNo, currentDetail.CatalogueCode);
                            if (catDetailRes.success && catDetailRes.category) {
                              const cData = catDetailRes.category;
                              const updates: any = {
                                CategoryCode: catg,
                                ClassCode: cData.ClassCode || catg.substring(0, 3),
                                SizeFactor: !!cData.SizeFactor,
                              };
                              if (!isManual && cData.NextInventoryCode) {
                                updates.InventoryCode = cData.NextInventoryCode.trim();
                              }
                              if (catDetailRes.costPerGram != null) {
                                updates.CostPerGram = String(catDetailRes.costPerGram);
                              }
                              updateDetail(updates);
                            }
                          } catch (_) { }
                        }}
                      />
                    </View>

                    {/* Tray Id */}
                    <View style={{ width: 110, borderWidth: 1, borderColor: '#D8CEBD', borderTopWidth: 0, borderLeftWidth: 0, flexDirection: 'row', height: 28, backgroundColor: '#FFFFFF' }}>
                      <TextInput
                        style={{ width: 82, paddingHorizontal: 6, paddingVertical: 2, fontSize: 11.5, color: '#2B2114' }}
                        value={currentDetail.TrayId}
                        onChangeText={(t) => updateDetail('TrayId', t)}
                        onBlur={async () => {
                          const tray = (currentDetail.TrayId || '').trim();
                          if (!tray) {
                            showToast('Tray Id Cannot be Blank ....', 'error');
                            return;
                          }
                          try {
                            const res = await fetchTraysApi();
                            if (res.success && Array.isArray(res.data)) {
                              const match = res.data.find((tr: any) => (tr.TrayId || '').trim().toUpperCase() === tray.toUpperCase());
                              if (!match) {
                                showToast('Invalid tray ID!', 'error');
                              }
                            }
                          } catch (_) { }
                        }}
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
                        onBlur={() => {
                          if (!currentDetail.InventoryCode?.trim()) {
                            showToast('Inventory Code Cannot be Blank ....', 'error');
                          }
                        }}
                        editable={isManual}
                      />
                    </View>

                    {/* Inventory Description */}
                    <View style={{ flex: 2, minWidth: 200, borderWidth: 1, borderColor: '#D8CEBD', borderTopWidth: 0, borderLeftWidth: 0, height: 28, backgroundColor: '#FFFFFF' }}>
                      <TextInput
                        style={{ flex: 1, paddingHorizontal: 6, paddingVertical: 2, fontSize: 11.5, color: '#2B2114' }}
                        value={currentDetail.InventoryDescription}
                        onChangeText={(t) => updateDetail('InventoryDescription', t)}
                        onBlur={() => {
                          if (!currentDetail.InventoryDescription?.trim() && !currentDetail.Description?.trim()) {
                            showToast('Description Cannot be Blank ....', 'error');
                          }
                        }}
                      />
                    </View>

                    {/* Weight */}
                    <View style={{ width: 85, borderWidth: 1, borderColor: '#D8CEBD', borderTopWidth: 0, borderLeftWidth: 0, height: 28, backgroundColor: '#FFFFFF' }}>
                      <TextInput
                        style={{ flex: 1, paddingHorizontal: 6, paddingVertical: 2, fontSize: 11.5, color: '#2B2114', textAlign: 'right' }}
                        value={currentDetail.Weight}
                        onChangeText={(t) => updateDetail('Weight', t)}
                        onBlur={async () => {
                          const w = parseFloat(currentDetail.Weight) || 0;
                          const isPiece = currentDetail.PurchaseMCType === 'PerPCS' || salesPCS;
                          if (w < 0) {
                            showToast('Invalid weight!', 'error');
                            return;
                          }
                          if (!isPiece && w <= 0) {
                            showToast('Invalid weight!', 'error');
                            return;
                          }
                          if (w > 0) {
                            try {
                              const glRes = await fetchGoldLossByWeightApi(w);
                              if (glRes.success && glRes.goldLoss != null) {
                                const glVal = parseFloat(glRes.goldLoss).toFixed(3);
                                updateDetail('GoldLoss', glVal);
                              }
                            } catch (_) { }
                          }
                        }}
                        keyboardType="numeric"
                      />
                    </View>

                    {/* Size Code */}
                    <View style={{ width: 95, borderWidth: 1, borderColor: '#D8CEBD', borderTopWidth: 0, borderLeftWidth: 0, flexDirection: 'row', height: 28, backgroundColor: '#FFFFFF' }}>
                      <TextInput
                        style={{ width: 67, paddingHorizontal: 6, paddingVertical: 2, fontSize: 11.5, color: '#2B2114', textAlign: 'center' }}
                        value={String(currentDetail.SizeCode ?? '')}
                        onChangeText={(t) => updateDetail('SizeCode', t)}
                        onBlur={async () => {
                          const size = String(currentDetail.SizeCode ?? '').trim();
                          if (currentDetail.SizeFactor && !size) {
                            showToast('SizeCode Cannot be Empty', 'error');
                            return;
                          }
                          if (size) {
                            try {
                              const sRes = await fetchSizesApi();
                              if (sRes.success && Array.isArray(sRes.data)) {
                                const match = sRes.data.find((sz: any) => String(sz.SizeCode ?? '').trim().toUpperCase() === size.toUpperCase());
                                if (!match) {
                                  showToast('Invalid Size Code!', 'error');
                                  updateDetail('SizeCode', '');
                                }
                              }
                            } catch (_) { }
                          }
                        }}
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
                        onBlur={() => {
                          let cpg = (currentDetail.CostPerGram || '').trim();
                          if (!cpg) {
                            cpg = headerState.CostPerGram || '0.00';
                            updateDetail('CostPerGram', cpg);
                          }
                          const val = parseFloat(cpg) || 0;
                          const isPiece = currentDetail.PurchaseMCType === 'PerPCS';
                          if (!isPiece && val <= 0) {
                            showToast('Invalid cost per gram!', 'error');
                          }
                        }}
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
                        onBlur={() => {
                          const p = parseInt(currentDetail.PCS, 10) || 0;
                          if (p < 1) {
                            showToast('Invalid pcs!', 'error');
                            updateDetail('PCS', '1');
                          } else {
                            updateDetail('PCS', String(p));
                          }
                        }}
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

                  {/* SUBLINE 3 - ROW 5: Second input row (matches VB.NET Panel1 y=92 controls) */}
                  {/* No separate header row - fields align under Subline 2 column positions */}
                  <View style={{ flexDirection: 'row' }}>
                    {/* Spacer under Inventory Code (width: 130) */}
                    <View style={{ width: 130, borderWidth: 1, borderColor: '#D8CEBD', borderTopWidth: 0, height: 28, backgroundColor: '#FAF8F3' }} />

                    {/* Diamond/Stone Description — same column as Inventory Description (flex: 2, minWidth: 200) */}
                    <View style={{ flex: 2, minWidth: 200, borderWidth: 1, borderColor: '#D8CEBD', borderTopWidth: 0, borderLeftWidth: 0, height: 28, backgroundColor: '#FFFFFF' }}>
                      <TextInput
                        style={{ flex: 1, paddingHorizontal: 6, paddingVertical: 2, fontSize: 11.5, color: '#2B2114' }}
                        value={currentDetail.DiamondDescription}
                        onChangeText={(t) => updateDetail('DiamondDescription', t)}
                        maxLength={50}
                        placeholder="Stone Description"
                        placeholderTextColor="#C0A87A"
                      />
                    </View>

                    {/* Stone Weight — same column as Weight (width: 85) */}
                    <View style={{ width: 85, borderWidth: 1, borderColor: '#D8CEBD', borderTopWidth: 0, borderLeftWidth: 0, height: 28, backgroundColor: '#FFFFFF' }}>
                      <TextInput
                        style={{ flex: 1, paddingHorizontal: 6, paddingVertical: 2, fontSize: 11.5, color: '#2B2114', textAlign: 'right' }}
                        value={currentDetail.StoneWeight}
                        onChangeText={(t) => updateDetail('StoneWeight', t)}
                        keyboardType="numeric"
                        placeholder="0.000"
                        placeholderTextColor="#C0A87A"
                      />
                    </View>

                    {/* Spacer under Size Code (width: 95) */}
                    <View style={{ width: 95, borderWidth: 1, borderColor: '#D8CEBD', borderTopWidth: 0, borderLeftWidth: 0, height: 28, backgroundColor: '#FAF8F3' }} />

                    {/* Net Weight (readonly) — same column as Gold Loss (width: 85) */}
                    <View style={{ width: 85, borderWidth: 1, borderColor: '#D8CEBD', borderTopWidth: 0, borderLeftWidth: 0, height: 28, backgroundColor: '#F8F5EE' }}>
                      <TextInput
                        style={{ flex: 1, paddingHorizontal: 6, paddingVertical: 2, fontSize: 11.5, color: '#6E4E10', textAlign: 'right', fontWeight: 'bold' }}
                        value={currentDetail.NetWeight}
                        editable={false}
                        placeholder="Net Wt"
                        placeholderTextColor="#C0A87A"
                      />
                    </View>

                    {/* Spacer under Cost / g (width: 95) */}
                    <View style={{ width: 95, borderWidth: 1, borderColor: '#D8CEBD', borderTopWidth: 0, borderLeftWidth: 0, height: 28, backgroundColor: '#FAF8F3' }} />

                    {/* PMC Cost (readonly) — same column as PMC (width: 80) */}
                    <View style={{ width: 80, borderWidth: 1, borderColor: '#D8CEBD', borderTopWidth: 0, borderLeftWidth: 0, height: 28, backgroundColor: '#F8F5EE' }}>
                      <TextInput
                        style={{ flex: 1, paddingHorizontal: 6, paddingVertical: 2, fontSize: 11.5, color: '#6E4E10', textAlign: 'right', fontWeight: 'bold' }}
                        value={currentDetail.PMCCost}
                        editable={false}
                      />
                    </View>

                    {/* SMC Cost (readonly) — same column as SMC (width: 80) */}
                    <View style={{ width: 80, borderWidth: 1, borderColor: '#D8CEBD', borderTopWidth: 0, borderLeftWidth: 0, height: 28, backgroundColor: '#F8F5EE' }}>
                      <TextInput
                        style={{ flex: 1, paddingHorizontal: 6, paddingVertical: 2, fontSize: 11.5, color: '#6E4E10', textAlign: 'right', fontWeight: 'bold' }}
                        value={currentDetail.SMCCost}
                        editable={false}
                      />
                    </View>

                    {/* Spacer under PCS (width: 65) */}
                    <View style={{ width: 65, borderWidth: 1, borderColor: '#D8CEBD', borderTopWidth: 0, borderLeftWidth: 0, height: 28, backgroundColor: '#FAF8F3' }} />

                    {/* Spacer under Total Cost (flex: 1, minWidth: 110) */}
                    <View style={{ flex: 1, minWidth: 110, borderWidth: 1, borderColor: '#D8CEBD', borderTopWidth: 0, borderLeftWidth: 0, height: 28, backgroundColor: '#FAF8F3' }} />
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
                        onBlur={() => {
                          if (currentDetail.IPM && !currentDetail.IPMType?.trim()) {
                            showToast('Type Cannot be Blank ....', 'error');
                          }
                        }}
                        placeholder="Bar / Coin"
                      />
                    </View>
                    <View style={styles.ipmCol}>
                      <ThemedText style={styles.ipmLabel}>Bar No:</ThemedText>
                      <TextInput
                        style={[styles.input, { width: 100 }]}
                        value={currentDetail.IPMBarNo}
                        onChangeText={(t) => updateDetail('IPMBarNo', t)}
                        onBlur={() => {
                          if (currentDetail.IPM && !currentDetail.IPMBarNo?.trim()) {
                            showToast('BarNo Cannot be Blank ....', 'error');
                          }
                        }}
                      />
                    </View>
                    <View style={styles.ipmCol}>
                      <ThemedText style={styles.ipmLabel}>Purity:</ThemedText>
                      <TextInput
                        style={[styles.input, { width: 70 }]}
                        value={currentDetail.IPMPurity}
                        onChangeText={(t) => updateDetail('IPMPurity', t)}
                        onBlur={() => {
                          if (currentDetail.IPM && !currentDetail.IPMPurity?.trim()) {
                            showToast('Purity Cannot be Blank ....', 'error');
                          }
                        }}
                        placeholder="999.9"
                      />
                    </View>
                    <View style={styles.ipmCol}>
                      <ThemedText style={styles.ipmLabel}>GST Type:</ThemedText>
                      <TextInput
                        style={[styles.input, { width: 80 }]}
                        value={currentDetail.IPMGSTType}
                        onChangeText={(t) => updateDetail('IPMGSTType', t)}
                        onBlur={() => {
                          if (currentDetail.IPM && !currentDetail.IPMGSTType?.trim()) {
                            showToast('GST Type Cannot be Blank ....', 'error');
                          }
                        }}
                      />
                    </View>
                    <View style={styles.ipmCol}>
                      <ThemedText style={styles.ipmLabel}>Refiner:</ThemedText>
                      <View style={{ flexDirection: 'row' }}>
                        <TextInput
                          style={[styles.input, { width: 85, borderTopRightRadius: 0, borderBottomRightRadius: 0 }]}
                          value={currentDetail.IPMRefiner}
                          onChangeText={(t) => updateDetail('IPMRefiner', t)}
                          onBlur={() => {
                            if (currentDetail.IPM && !currentDetail.IPMRefiner?.trim()) {
                              showToast('Refiner Cannot be Blank ....', 'error');
                            }
                          }}
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
                          onBlur={() => {
                            if (currentDetail.IPM && !currentDetail.IPMCountry?.trim()) {
                              showToast('Country Cannot be Blank ....', 'error');
                            }
                          }}
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

              {/* ORDER INVENTORY ITEM LINK EXPANDABLE BOX (When Ordered is checked in header) */}
              {headerState.Ordered && (
                <View style={[styles.tradeInContainer, { backgroundColor: '#F0F7FF', borderColor: '#B3D4FC' }]}>
                  <ThemedText style={[styles.tradeInHeader, { color: '#0D47A1' }]}>Linked Order Inventory Item</ThemedText>
                  <View style={styles.tradeInRow}>
                    <ThemedText style={[styles.tradeInLabel, { color: '#1565C0' }]}>Order Item Code:</ThemedText>
                    <View style={{ flexDirection: 'row', width: 170 }}>
                      <TextInput
                        style={[styles.input, { flex: 1, borderTopRightRadius: 0, borderBottomRightRadius: 0 }]}
                        value={currentDetail.OrderInventoryCode}
                        onChangeText={(t) => updateDetail('OrderInventoryCode', t)}
                        placeholder="Select item..."
                      />
                      <TouchableOpacity style={[styles.lookupBtn, { width: 28 }]} onPress={fetchOrderInventoryDetailsSearch}>
                        <ThemedText style={styles.lookupBtnText}>...</ThemedText>
                      </TouchableOpacity>
                    </View>
                    <ThemedText style={{ fontSize: 11, color: '#546E7A', marginLeft: 8 }}>
                      {currentDetail.OrderInventoryCode ? `Linked to Order ${headerState.OrderNo}` : `Pick item from Order ${headerState.OrderNo || '(Select Order No first)'}`}
                    </ThemedText>
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

      {/* ================= DIRECT PURCHASE REPORT / PRINT PREVIEW MODAL ================= */}
      <Modal visible={reportModalVisible} transparent={true} animationType="fade">
        <View style={styles.modalOverlay}>
          <View style={[styles.modalCard, { width: '95%', maxWidth: 1200, maxHeight: '90%', display: 'flex', flexDirection: 'column' }]}>
            {/* Modal Top Bar */}
            <View style={[styles.modalHeader, { backgroundColor: '#F9F4E8', borderBottomColor: '#D8CEBD', paddingVertical: 12, paddingHorizontal: 16 }]}>
              <View style={{ flex: 1 }}>
                <ThemedText style={{ fontSize: 16, fontWeight: 'bold', color: '#6E4E10' }}>
                  Direct Purchase Detail Report
                </ThemedText>
                <ThemedText style={{ fontSize: 11, color: '#8C7A6B', marginTop: 2 }}>
                  {reportCompany.name || 'SMART GOLD'} {reportCompany.city ? `• ${reportCompany.city}` : ''} {reportCompany.phone ? `• Tel: ${reportCompany.phone}` : ''}
                </ThemedText>
              </View>

              {/* Action Buttons: Print, PDF, Excel, Close */}
              <View style={{ flexDirection: 'row', alignItems: 'center', gap: 8 }}>
                <TouchableOpacity
                  style={[styles.reportActionBtn, { backgroundColor: '#FFF8E1', borderColor: '#FFA000' }]}
                  onPress={handleExecutePrint}
                  disabled={reportExporting}
                >
                  <Ionicons name="print-outline" size={15} color="#8D6E63" />
                  <ThemedText style={[styles.reportActionBtnText, { color: '#8D6E63' }]}>Print</ThemedText>
                </TouchableOpacity>

                <TouchableOpacity
                  style={[styles.reportActionBtn, { backgroundColor: '#FFEBEE', borderColor: '#E57373' }]}
                  onPress={handleExportPdf}
                  disabled={reportExporting}
                >
                  <Ionicons name="document-text-outline" size={15} color="#C62828" />
                  <ThemedText style={[styles.reportActionBtnText, { color: '#C62828' }]}>PDF</ThemedText>
                </TouchableOpacity>

                <TouchableOpacity
                  style={[styles.reportActionBtn, { backgroundColor: '#E8F5E9', borderColor: '#81C784' }]}
                  onPress={handleExportExcel}
                  disabled={reportExporting}
                >
                  <Ionicons name="download-outline" size={15} color="#2E7D32" />
                  <ThemedText style={[styles.reportActionBtnText, { color: '#2E7D32' }]}>Excel</ThemedText>
                </TouchableOpacity>

                <TouchableOpacity
                  style={[styles.reportActionBtn, { backgroundColor: '#F5F5F5', borderColor: '#BDBDBD' }]}
                  onPress={() => setReportModalVisible(false)}
                >
                  <Ionicons name="close" size={16} color="#424242" />
                  <ThemedText style={[styles.reportActionBtnText, { color: '#424242' }]}>Close</ThemedText>
                </TouchableOpacity>
              </View>
            </View>

            {/* Info Summary Strip */}
            <View style={{ flexDirection: 'row', flexWrap: 'wrap', gap: 16, paddingHorizontal: 16, paddingVertical: 10, backgroundColor: '#FCFAF6', borderBottomWidth: 1, borderBottomColor: '#EFE7DA' }}>
              <ThemedText style={{ fontSize: 11.5, color: '#3B2D19' }}>
                <ThemedText style={{ fontWeight: 'bold', color: '#6E4E10' }}>Job Sheet No: </ThemedText>
                {reportData[0]?.JobSheetNo || headerState.JobSheetNo || '—'}
              </ThemedText>
              <ThemedText style={{ fontSize: 11.5, color: '#3B2D19' }}>
                <ThemedText style={{ fontWeight: 'bold', color: '#6E4E10' }}>Purchase Bill No: </ThemedText>
                {reportData[0]?.PurchaseBillNo || headerState.InvoiceNo || '—'}
              </ThemedText>
              <ThemedText style={{ fontSize: 11.5, color: '#3B2D19' }}>
                <ThemedText style={{ fontWeight: 'bold', color: '#6E4E10' }}>Supplier: </ThemedText>
                {reportData[0]?.SupplierCode || headerState.SupplierCode || '—'}
              </ThemedText>
              <ThemedText style={{ fontSize: 11.5, color: '#3B2D19' }}>
                <ThemedText style={{ fontWeight: 'bold', color: '#6E4E10' }}>Total Records: </ThemedText>
                {reportData.length}
              </ThemedText>
            </View>

            {/* Table Area (Horizontal & Vertical Scrollable) */}
            <ScrollView horizontal style={{ flex: 1 }} contentContainerStyle={{ flexDirection: 'column' }}>
              <View style={{ minWidth: 1150 }}>
                {/* Table Header */}
                <View style={{ flexDirection: 'row', backgroundColor: '#F4ECE0', borderBottomWidth: 2, borderBottomColor: '#B8903B', paddingVertical: 8 }}>
                  <View style={{ width: 40, justifyContent: 'center', alignItems: 'center' }}>
                    <ThemedText style={styles.rptHeaderCell}>#</ThemedText>
                  </View>
                  <View style={{ width: 100, paddingHorizontal: 6, justifyContent: 'center' }}>
                    <ThemedText style={styles.rptHeaderCell}>Job Sheet</ThemedText>
                  </View>
                  <View style={{ width: 95, paddingHorizontal: 6, justifyContent: 'center' }}>
                    <ThemedText style={styles.rptHeaderCell}>Bill No</ThemedText>
                  </View>
                  <View style={{ width: 100, paddingHorizontal: 6, justifyContent: 'center' }}>
                    <ThemedText style={styles.rptHeaderCell}>Inv Code</ThemedText>
                  </View>
                  <View style={{ width: 150, paddingHorizontal: 6, justifyContent: 'center' }}>
                    <ThemedText style={styles.rptHeaderCell}>Description</ThemedText>
                  </View>
                  <View style={{ width: 85, paddingHorizontal: 6, justifyContent: 'center' }}>
                    <ThemedText style={styles.rptHeaderCell}>Catalogue</ThemedText>
                  </View>
                  <View style={{ width: 85, paddingHorizontal: 6, justifyContent: 'center', alignItems: 'flex-end' }}>
                    <ThemedText style={styles.rptHeaderCell}>Pur Wt (g)</ThemedText>
                  </View>
                  <View style={{ width: 75, paddingHorizontal: 6, justifyContent: 'center', alignItems: 'flex-end' }}>
                    <ThemedText style={styles.rptHeaderCell}>Gold Loss</ThemedText>
                  </View>
                  <View style={{ width: 55, paddingHorizontal: 6, justifyContent: 'center', alignItems: 'center' }}>
                    <ThemedText style={styles.rptHeaderCell}>PCS</ThemedText>
                  </View>
                  <View style={{ width: 75, paddingHorizontal: 6, justifyContent: 'center', alignItems: 'flex-end' }}>
                    <ThemedText style={styles.rptHeaderCell}>MC ($)</ThemedText>
                  </View>
                  <View style={{ width: 90, paddingHorizontal: 6, justifyContent: 'center', alignItems: 'flex-end' }}>
                    <ThemedText style={styles.rptHeaderCell}>Total Cost ($)</ThemedText>
                  </View>
                  <View style={{ width: 85, paddingHorizontal: 6, justifyContent: 'center', alignItems: 'flex-end' }}>
                    <ThemedText style={styles.rptHeaderCell}>Det Wt (g)</ThemedText>
                  </View>
                  <View style={{ width: 65, paddingHorizontal: 6, justifyContent: 'center', alignItems: 'center' }}>
                    <ThemedText style={styles.rptHeaderCell}>Det PCS</ThemedText>
                  </View>
                  <View style={{ width: 75, paddingHorizontal: 6, justifyContent: 'center', alignItems: 'flex-end' }}>
                    <ThemedText style={styles.rptHeaderCell}>Det MC ($)</ThemedText>
                  </View>
                  <View style={{ width: 90, paddingHorizontal: 6, justifyContent: 'center', alignItems: 'flex-end' }}>
                    <ThemedText style={styles.rptHeaderCell}>Det Total ($)</ThemedText>
                  </View>
                  <View style={{ width: 80, paddingHorizontal: 6, justifyContent: 'center' }}>
                    <ThemedText style={styles.rptHeaderCell}>Supplier</ThemedText>
                  </View>
                  <View style={{ width: 110, paddingHorizontal: 6, justifyContent: 'center' }}>
                    <ThemedText style={styles.rptHeaderCell}>Diamond Desc</ThemedText>
                  </View>
                  <View style={{ width: 75, paddingHorizontal: 6, justifyContent: 'center' }}>
                    <ThemedText style={styles.rptHeaderCell}>User</ThemedText>
                  </View>
                </View>

                {/* Table Rows */}
                <ScrollView style={{ maxHeight: 380 }}>
                  {reportData.map((row, idx) => (
                    <View
                      key={idx}
                      style={{
                        flexDirection: 'row',
                        backgroundColor: idx % 2 === 0 ? '#FFFFFF' : '#FAF8F4',
                        borderBottomWidth: 1,
                        borderBottomColor: '#EFEBE2',
                        paddingVertical: 7,
                        alignItems: 'center'
                      }}
                    >
                      <View style={{ width: 40, justifyContent: 'center', alignItems: 'center' }}>
                        <ThemedText style={styles.rptDataCell}>{idx + 1}</ThemedText>
                      </View>
                      <View style={{ width: 100, paddingHorizontal: 6 }}>
                        <ThemedText style={[styles.rptDataCell, { fontWeight: '600' }]}>{row.JobSheetNo || '—'}</ThemedText>
                      </View>
                      <View style={{ width: 95, paddingHorizontal: 6 }}>
                        <ThemedText style={styles.rptDataCell}>{row.PurchaseBillNo || '—'}</ThemedText>
                      </View>
                      <View style={{ width: 100, paddingHorizontal: 6 }}>
                        <ThemedText style={[styles.rptDataCell, { fontWeight: '600', color: '#8D6E63' }]}>{row.InventoryCode || '—'}</ThemedText>
                      </View>
                      <View style={{ width: 150, paddingHorizontal: 6 }}>
                        <ThemedText style={styles.rptDataCell} numberOfLines={1}>{row.Description || '—'}</ThemedText>
                      </View>
                      <View style={{ width: 85, paddingHorizontal: 6 }}>
                        <ThemedText style={styles.rptDataCell}>{row.CatalogueCode || '—'}</ThemedText>
                      </View>
                      <View style={{ width: 85, paddingHorizontal: 6, alignItems: 'flex-end' }}>
                        <ThemedText style={[styles.rptDataCell, { fontWeight: '600' }]}>{Number(row.Weight || 0).toFixed(3)}</ThemedText>
                      </View>
                      <View style={{ width: 75, paddingHorizontal: 6, alignItems: 'flex-end' }}>
                        <ThemedText style={styles.rptDataCell}>{Number(row.GoldLoss || 0).toFixed(3)}</ThemedText>
                      </View>
                      <View style={{ width: 55, paddingHorizontal: 6, alignItems: 'center' }}>
                        <ThemedText style={styles.rptDataCell}>{row.PCS ?? 1}</ThemedText>
                      </View>
                      <View style={{ width: 75, paddingHorizontal: 6, alignItems: 'flex-end' }}>
                        <ThemedText style={styles.rptDataCell}>{Number(row.MC || 0).toFixed(2)}</ThemedText>
                      </View>
                      <View style={{ width: 90, paddingHorizontal: 6, alignItems: 'flex-end' }}>
                        <ThemedText style={[styles.rptDataCell, { fontWeight: '600', color: '#1B5E20' }]}>{Number(row.TotalCost || 0).toFixed(2)}</ThemedText>
                      </View>
                      <View style={{ width: 85, paddingHorizontal: 6, alignItems: 'flex-end' }}>
                        <ThemedText style={styles.rptDataCell}>{Number(row.DetailWeight || 0).toFixed(3)}</ThemedText>
                      </View>
                      <View style={{ width: 65, paddingHorizontal: 6, alignItems: 'center' }}>
                        <ThemedText style={styles.rptDataCell}>{row.DetailPCS ?? 1}</ThemedText>
                      </View>
                      <View style={{ width: 75, paddingHorizontal: 6, alignItems: 'flex-end' }}>
                        <ThemedText style={styles.rptDataCell}>{Number(row.DetailMC || 0).toFixed(2)}</ThemedText>
                      </View>
                      <View style={{ width: 90, paddingHorizontal: 6, alignItems: 'flex-end' }}>
                        <ThemedText style={styles.rptDataCell}>{Number(row.DetailTotalCost || 0).toFixed(2)}</ThemedText>
                      </View>
                      <View style={{ width: 80, paddingHorizontal: 6 }}>
                        <ThemedText style={styles.rptDataCell}>{row.SupplierCode || '—'}</ThemedText>
                      </View>
                      <View style={{ width: 110, paddingHorizontal: 6 }}>
                        <ThemedText style={styles.rptDataCell} numberOfLines={1}>{row.DiamondDescription || '—'}</ThemedText>
                      </View>
                      <View style={{ width: 75, paddingHorizontal: 6 }}>
                        <ThemedText style={styles.rptDataCell}>{row.CreateUser || '—'}</ThemedText>
                      </View>
                    </View>
                  ))}
                </ScrollView>

                {/* Table Footer: Totals */}
                <View style={{ flexDirection: 'row', backgroundColor: '#F4ECE0', borderTopWidth: 2, borderTopColor: '#B8903B', paddingVertical: 8, alignItems: 'center' }}>
                  <View style={{ width: 40, justifyContent: 'center', alignItems: 'center' }}>
                    <ThemedText style={[styles.rptHeaderCell, { color: '#B8860B' }]}>Σ</ThemedText>
                  </View>
                  <View style={{ width: 440, paddingHorizontal: 6 }}>
                    <ThemedText style={[styles.rptHeaderCell, { color: '#6E4E10', fontWeight: 'bold' }]}>TOTALS:</ThemedText>
                  </View>
                  <View style={{ width: 85, paddingHorizontal: 6, alignItems: 'flex-end' }}>
                    <ThemedText style={[styles.rptHeaderCell, { color: '#6E4E10', fontWeight: 'bold' }]}>{Number(reportTotals.totalWeight || 0).toFixed(3)}</ThemedText>
                  </View>
                  <View style={{ width: 75, paddingHorizontal: 6, alignItems: 'flex-end' }}>
                    <ThemedText style={[styles.rptHeaderCell, { color: '#6E4E10', fontWeight: 'bold' }]}>{Number(reportTotals.totalGoldLoss || 0).toFixed(3)}</ThemedText>
                  </View>
                  <View style={{ width: 55, paddingHorizontal: 6, alignItems: 'center' }}>
                    <ThemedText style={[styles.rptHeaderCell, { color: '#6E4E10', fontWeight: 'bold' }]}>{reportTotals.totalPCS || 0}</ThemedText>
                  </View>
                  <View style={{ width: 75, paddingHorizontal: 6, alignItems: 'flex-end' }}>
                    <ThemedText style={[styles.rptHeaderCell, { color: '#6E4E10', fontWeight: 'bold' }]}>{Number(reportTotals.totalMC || 0).toFixed(2)}</ThemedText>
                  </View>
                  <View style={{ width: 90, paddingHorizontal: 6, alignItems: 'flex-end' }}>
                    <ThemedText style={[styles.rptHeaderCell, { color: '#1B5E20', fontWeight: 'bold' }]}>{Number(reportTotals.totalCost || 0).toFixed(2)}</ThemedText>
                  </View>
                  <View style={{ width: 85, paddingHorizontal: 6, alignItems: 'flex-end' }}>
                    <ThemedText style={[styles.rptHeaderCell, { color: '#6E4E10', fontWeight: 'bold' }]}>{Number(reportTotals.totalDetailWeight || 0).toFixed(3)}</ThemedText>
                  </View>
                  <View style={{ width: 65, paddingHorizontal: 6, alignItems: 'center' }}>
                    <ThemedText style={[styles.rptHeaderCell, { color: '#6E4E10', fontWeight: 'bold' }]}>{reportTotals.totalDetailPCS || 0}</ThemedText>
                  </View>
                  <View style={{ width: 75, paddingHorizontal: 6, alignItems: 'flex-end' }}>
                    <ThemedText style={[styles.rptHeaderCell, { color: '#6E4E10', fontWeight: 'bold' }]}>{Number(reportTotals.totalDetailMC || 0).toFixed(2)}</ThemedText>
                  </View>
                  <View style={{ width: 90, paddingHorizontal: 6, alignItems: 'flex-end' }}>
                    <ThemedText style={[styles.rptHeaderCell, { color: '#6E4E10', fontWeight: 'bold' }]}>{Number(reportTotals.totalDetailCost || 0).toFixed(2)}</ThemedText>
                  </View>
                  <View style={{ width: 265, paddingHorizontal: 6 }} />
                </View>
              </View>
            </ScrollView>

            {/* Modal Bottom Footer */}
            <View style={{ paddingVertical: 10, paddingHorizontal: 16, backgroundColor: '#FAF8F5', borderTopWidth: 1, borderTopColor: '#E8E1D5', flexDirection: 'row', justifyContent: 'space-between', alignItems: 'center' }}>
              <ThemedText style={{ fontSize: 11, color: '#888' }}>
                Query executed on tblJobSheetHeader, tblPurchase & tblJobSheetDetail
              </ThemedText>
              <TouchableOpacity
                style={[styles.actionBtn, { width: 90, height: 32 }]}
                onPress={() => setReportModalVisible(false)}
              >
                <ThemedText style={styles.actionBtnText}>Done</ThemedText>
              </TouchableOpacity>
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

      {/* ================= IN-APP CONFIRMATION MODAL (Replaces window.confirm / Alert.alert) ================= */}
      <Modal visible={confirmDialog.visible} transparent={true} animationType="fade">
        <View style={styles.modalOverlay}>
          <View style={[styles.modalCard, { width: 380, maxWidth: '90%', borderRadius: 10 }]}>
            <View style={[styles.modalHeader, { backgroundColor: '#F5E7CA', borderTopLeftRadius: 10, borderTopRightRadius: 10 }]}>
              <View style={{ flexDirection: 'row', alignItems: 'center', gap: 8 }}>
                <Ionicons
                  name={confirmDialog.confirmStyle === 'danger' ? 'warning-outline' : 'help-circle-outline'}
                  size={20}
                  color={confirmDialog.confirmStyle === 'danger' ? '#C62828' : '#8D6E63'}
                />
                <ThemedText style={[styles.modalHeaderTitle, { fontSize: 14, color: '#3B2D19' }]}>
                  {confirmDialog.title}
                </ThemedText>
              </View>
              <TouchableOpacity onPress={() => setConfirmDialog((prev) => ({ ...prev, visible: false }))}>
                <Ionicons name="close" size={20} color="#3B2D19" />
              </TouchableOpacity>
            </View>

            <View style={{ padding: 18 }}>
              <ThemedText style={{ fontSize: 13, color: '#333', lineHeight: 20 }}>
                {confirmDialog.message}
              </ThemedText>

              <View style={{ flexDirection: 'row', justifyContent: 'flex-end', gap: 10, marginTop: 20 }}>
                <TouchableOpacity
                  style={[
                    styles.actionBtn,
                    {
                      backgroundColor: '#FFFFFF',
                      borderWidth: 1,
                      borderColor: '#D8CEBD',
                      paddingHorizontal: 16,
                      height: 32,
                    }
                  ]}
                  onPress={() => setConfirmDialog((prev) => ({ ...prev, visible: false }))}
                >
                  <ThemedText style={{ color: '#555', fontWeight: '600', fontSize: 12 }}>Cancel</ThemedText>
                </TouchableOpacity>

                <TouchableOpacity
                  style={[
                    styles.actionBtn,
                    {
                      backgroundColor: confirmDialog.confirmStyle === 'danger' ? '#C62828' : '#B8860B',
                      paddingHorizontal: 16,
                      height: 32,
                    }
                  ]}
                  onPress={confirmDialog.onConfirm}
                >
                  <ThemedText style={{ color: '#FFF', fontWeight: 'bold', fontSize: 12 }}>
                    {confirmDialog.confirmText || 'OK'}
                  </ThemedText>
                </TouchableOpacity>
              </View>
            </View>
          </View>
        </View>
      </Modal>

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

  // Report Modal Styles
  reportActionBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 4,
    paddingVertical: 5,
    paddingHorizontal: 10,
    borderRadius: 5,
    borderWidth: 1,
  },
  reportActionBtnText: {
    fontSize: 11,
    fontWeight: '700',
  },
  rptHeaderCell: {
    fontSize: 10.5,
    fontWeight: '700',
    color: '#6E4E10',
  },
  rptDataCell: {
    fontSize: 11,
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
