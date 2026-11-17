import { ThemedText } from '@/components/themed-text';
import { API_BASE_URL } from '@/constants/api';
import { useThemeContext } from '@/contexts/ThemeContext';
import { useThemeColors } from '@/hooks/useThemeColors';
import { getStandardReportCss, renderStandardReportFooter, renderStandardReportHeader } from '@/utils/standardPrintReport';
import { Ionicons } from '@expo/vector-icons';
import AsyncStorage from '@react-native-async-storage/async-storage';
import { Stack, useRouter } from 'expo-router';
import React, { useEffect, useState } from 'react';
import {
  Alert,
  FlatList,
  Modal,
  Platform,
  ScrollView,
  StyleSheet,
  TextInput,
  TouchableOpacity,
  TouchableWithoutFeedback,
  View
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';

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

  return { userId: uid || 'sys', userName: uname || 'System' };
};

export default function PurchaseReturnScreen() {
  const themeColors = useThemeColors();
  const { accentColor } = useThemeContext();
  const { background: backgroundColor, card: cardColor, text: textColor, border: borderColor, subtitle: subtitleColor, primaryGold: GOLD_COLOR } = themeColors;
  const router = useRouter();

  // ===== STATE =====
  const [refNo, setRefNo] = useState('');
  const [returnDate, setReturnDate] = useState(new Date().toISOString().split('T')[0]);
  const [supplierCode, setSupplierCode] = useState('');
  const [supplierName, setSupplierName] = useState('');
  const [remarks, setRemarks] = useState('');

  // Total costs
  const [gstActive, setGstActive] = useState(false);
  const [gstPercentage, setGstPercentage] = useState<number>(9);
  const [totalCost, setTotalCost] = useState(0);
  const [gstAmount, setGstAmount] = useState(0);
  const [totalMc, setTotalMc] = useState(0);

  const [totalWeight, setTotalWeight] = useState(0);
  const [totalPcs, setTotalPcs] = useState(0);

  // Table details array
  const [details, setDetails] = useState<any[]>([]);
  const [selectedIndex, setSelectedIndex] = useState<number | null>(null);

  // Bottom Add row form
  const [inventoryCode, setInventoryCode] = useState('');
  const [description, setDescription] = useState('');
  const [weight, setWeight] = useState('');
  const [goldLoss, setGoldLoss] = useState('');
  const [mc, setMc] = useState('');
  const [costPerGram, setCostPerGram] = useState('');
  const [pcs, setPcs] = useState('');
  const [value, setValue] = useState('');
  const [purchaseType, setPurchaseType] = useState('W');
  const [qtyOnHand, setQtyOnHand] = useState(0);

  const [saving, setSaving] = useState(false);
  const [loading, setLoading] = useState(false);
  const [toast, setToast] = useState<{ message: string; type: 'success' | 'error' } | null>(null);

  // Confirmation Modal State (for duplicate items)
  const [confirmDialog, setConfirmDialog] = useState<{
    visible: boolean;
    message: string;
    onConfirm: () => void;
  } | null>(null);

  // Print Confirmation Modal State after save: "Do you want to print ?"
  const [printConfirmReturnNo, setPrintConfirmReturnNo] = useState<string | null>(null);

  // LOV State
  const [lovVisible, setLovVisible] = useState(false);
  const [lovTitle, setLovTitle] = useState('');
  const [lovData, setLovData] = useState<any[]>([]);
  const [filteredLovData, setFilteredLovData] = useState<any[]>([]);
  const [lovSearch, setLovSearch] = useState('');
  const [lovColumns, setLovColumns] = useState<string[]>([]);
  const [onLovSelect, setOnLovSelect] = useState<((item: any) => void) | null>(null);

  const showToast = (message: string, type: 'success' | 'error' = 'success') => {
    setToast({ message, type });
    setTimeout(() => {
      setToast(null);
    }, 3000);
  };

  // Fetch company GST percentage
  useEffect(() => {
    const fetchCompanyConfig = async () => {
      try {
        const res = await fetch(`${API_URL}/company`);
        const json = await res.json();
        if (json.success && Array.isArray(json.data) && json.data.length > 0) {
          const comp = json.data[0];
          const perc = parseFloat(comp.GSTPerc || comp.GSTPercentage || comp.GstPercentage || '9');
          if (!isNaN(perc) && perc > 0) {
            setGstPercentage(perc);
          }
        }
      } catch (_) {}
    };
    fetchCompanyConfig();
  }, []);

  useEffect(() => {
    calculateTotal(details, gstActive, gstPercentage);
  }, [gstActive, gstPercentage]);

  const openLov = (title: string, data: any[], columns: string[], onSelect: (item: any) => void) => {
    setLovTitle(title);
    setLovData(data);
    setFilteredLovData(data);
    setLovColumns(columns);
    setLovSearch('');
    setOnLovSelect(() => onSelect);
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
      Object.values(item).some(val => String(val).toLowerCase().includes(lower))
    );
    setFilteredLovData(filtered);
  };

  const handleLovSelect = (item: any) => {
    if (onLovSelect) onLovSelect(item);
    setLovVisible(false);
  };

  const fetchSuppliers = async () => {
    setLoading(true);
    try {
      const res = await fetch(`${API_URL}/purchasereturn/lov/supplier`);
      const json = await res.json();
      if (json.success) {
        openLov('Select Supplier', json.data, ['SupplierCode', 'SupplierName', 'Address1', 'Address2'], item => {
          setSupplierCode(item.SupplierCode);
          setSupplierName(item.SupplierName);
        });
      } else {
        const detail = json.error ? ` (${json.error})` : '';
        showToast(`Failed to fetch suppliers${detail}`, 'error');
      }
    } catch (e: any) {
      showToast(`Error connecting to server: ${e?.message || e}`, 'error');
    } finally {
      setLoading(false);
    }
  };

  const handleSupplierCodeBlur = async () => {
    const code = supplierCode.trim();
    if (!code) {
      setSupplierName('');
      return;
    }
    try {
      const res = await fetch(`${API_URL}/purchasereturn/lov/supplier?code=${encodeURIComponent(code)}`);
      const json = await res.json();
      if (json.success && json.data) {
        setSupplierCode(json.data.SupplierCode);
        setSupplierName(json.data.SupplierName);
      } else {
        showToast('Invalid supplier code!', 'error');
        setSupplierCode('');
        setSupplierName('');
      }
    } catch (e) {
      showToast('Error validating supplier code', 'error');
    }
  };

  const fetchInventoryItems = async () => {
    setLoading(true);
    try {
      const res = await fetch(`${API_URL}/purchasereturn/lov/inventory?SupplierCode=${encodeURIComponent(supplierCode.trim())}`);
      const json = await res.json();
      if (json.success) {
        openLov(
          'Select Inventory Stock',
          json.data,
          ['InventoryCode', 'Description', 'Weight', 'QtyOnHand'],
          item => {
            setInventoryCode(item.InventoryCode);
            loadInventoryDetail(item.InventoryCode);
          }
        );
      } else {
        showToast('Failed to fetch inventory stock list', 'error');
      }
    } catch (e) {
      showToast('Error connecting to server', 'error');
    } finally {
      setLoading(false);
    }
  };

  const loadInventoryDetail = async (code: string) => {
    if (!code || !code.trim()) return;
    try {
      const res = await fetch(
        `${API_URL}/purchasereturn/inventoryDetail?InventoryCode=${encodeURIComponent(code.trim())}&SupplierCode=${encodeURIComponent(supplierCode.trim())}`
      );
      const json = await res.json();
      if (json.success && json.data) {
        const item = json.data;
        setInventoryCode(item.InventoryCode || code.trim());
        setDescription(item.Description || '');
        setWeight(item.Weight !== undefined && item.Weight !== null ? String(item.Weight) : '0');
        setGoldLoss(item.GoldLoss !== undefined && item.GoldLoss !== null ? String(item.GoldLoss) : '0');
        setCostPerGram(item.CostPerGram !== undefined && item.CostPerGram !== null ? String(item.CostPerGram) : '0');
        setMc(item.PMC !== undefined && item.PMC !== null ? String(item.PMC) : '0');

        const pType = (item.PurchaseType || 'W').toUpperCase();
        setPurchaseType(pType);
        const qoh = Number(item.QtyOnHand) || 0;
        setQtyOnHand(qoh);

        if (pType === 'W') {
          setPcs('1');
        } else {
          setPcs(qoh > 0 ? '1' : '0');
        }

        setValue(item.TotalCost !== undefined && item.TotalCost !== null ? String(item.TotalCost) : '0');

        // Auto-populate supplier if header supplier is empty
        if (!supplierCode.trim() && item.SupplierCode) {
          setSupplierCode(item.SupplierCode);
          if (item.SupplierName) {
            setSupplierName(item.SupplierName);
          }
        }
      } else {
        showToast(json.message || 'This Inventory Not Found... ', 'error');
        clearItemFields();
      }
    } catch (e) {
      showToast('Error fetching inventory details', 'error');
      clearItemFields();
    }
  };

  const handleInventoryCodeBlur = async () => {
    const code = inventoryCode.trim();
    if (!code) {
      clearItemFields();
      return;
    }
    await loadInventoryDetail(code);
  };

  const handlePcsChange = (text: string) => {
    const numericText = text.replace(/[^0-9]/g, '');
    setPcs(numericText);
  };

  const handlePcsBlur = () => {
    if (!inventoryCode.trim()) return;
    const valPcs = parseInt(pcs) || 0;
    if (valPcs < 1) {
      showToast('PCS should be greater than or equal to one!', 'error');
      setPcs('1');
      return;
    }
    if (valPcs > qtyOnHand) {
      showToast(`Maximum available PCS for this Inventory is ${qtyOnHand}`, 'error');
      setPcs(String(qtyOnHand));
      return;
    }
  };

  const confirmAddDuplicate = (existPcs: number, addPcs: number): Promise<boolean> => {
    const promptMsg = `${existPcs} PCS for this item found in Return list (Do you want to add ${addPcs} Pieces )`;
    return new Promise(resolve => {
      setConfirmDialog({
        visible: true,
        message: promptMsg,
        onConfirm: () => {
          setConfirmDialog(null);
          resolve(true);
        }
      });
    });
  };

  const handleAddRow = async () => {
    if (!inventoryCode.trim()) {
      showToast('Select InventoryCode!', 'error');
      return;
    }
    const valPcs = parseInt(pcs) || 0;
    if (valPcs < 1) {
      showToast('PCS should be greater than or equal to one!', 'error');
      return;
    }
    if (valPcs > qtyOnHand) {
      showToast(`Maximum available PCS for this Inventory is ${qtyOnHand}`, 'error');
      return;
    }

    const existingIdx = details.findIndex(
      d => d.InventoryCode.trim().toUpperCase() === inventoryCode.trim().toUpperCase()
    );
    let updatedDetails = [...details];

    if (existingIdx !== -1) {
      const exist = updatedDetails[existingIdx];
      const confirmed = await confirmAddDuplicate(exist.PCS, valPcs);
      if (!confirmed) {
        return;
      }
      if (valPcs > (qtyOnHand - exist.PCS)) {
        showToast(`Maximum available PCS for this Inventory is ${qtyOnHand}`, 'error');
        return;
      }
      const newWeight = (Number(exist.Weight) || 0) + (parseFloat(weight) || 0);
      const newGoldLoss = (Number(exist.GoldLoss) || 0) + (parseFloat(goldLoss) || 0);
      const newMc = (Number(exist.MC) || 0) + (parseFloat(mc) || 0);
      const cpg = parseFloat(costPerGram) || Number(exist.CostPerGram) || 0;
      const newTotalCost = (newWeight + newGoldLoss) * cpg + newMc;

      exist.PCS = exist.PCS + valPcs;
      exist.Weight = Number(newWeight.toFixed(3));
      exist.GoldLoss = Number(newGoldLoss.toFixed(3));
      exist.MC = Number(newMc.toFixed(2));
      exist.CostPerGram = Number(cpg.toFixed(2));
      exist.TotalCost = Number(newTotalCost.toFixed(2));
    } else {
      updatedDetails.push({
        InventoryCode: inventoryCode.trim(),
        Description: description,
        Weight: parseFloat(weight) || 0,
        GoldLoss: parseFloat(goldLoss) || 0,
        MC: parseFloat(mc) || 0,
        CostPerGram: parseFloat(costPerGram) || 0,
        PCS: valPcs,
        TotalCost: parseFloat(value) || 0
      });
    }

    setDetails(updatedDetails);
    calculateTotal(updatedDetails, gstActive, gstPercentage);
    clearItemFields();
    showToast('Added item to list', 'success');
  };

  const handleDeleteRow = () => {
    if (selectedIndex === null) {
      showToast('Select an item row from grid to delete', 'error');
      return;
    }
    const updated = details.filter((_, i) => i !== selectedIndex);
    setDetails(updated);
    setSelectedIndex(null);
    calculateTotal(updated, gstActive, gstPercentage);
    showToast('Deleted item row', 'success');
  };

  const calculateTotal = (list: any[], checkGst: boolean, gstPerc: number = gstPercentage) => {
    let totWt = 0;
    let totPcs = 0;
    let totVal = 0;

    list.forEach(item => {
      totWt += Number(item.Weight) || 0;
      totPcs += Number(item.PCS) || 0;
      totVal += Number(item.TotalCost) || 0;
    });

    setTotalWeight(totWt);
    setTotalPcs(totPcs);
    setTotalCost(totVal);

    if (checkGst && totVal !== 0) {
      const gstVal = Number(((totVal * gstPerc) / 100).toFixed(2));
      setGstAmount(gstVal);
      setTotalMc(Number((totVal + gstVal).toFixed(2)));
    } else {
      setGstAmount(0);
      setTotalMc(totVal);
    }
  };

  const clearItemFields = () => {
    setInventoryCode('');
    setDescription('');
    setWeight('');
    setGoldLoss('');
    setMc('');
    setCostPerGram('');
    setPcs('');
    setValue('');
    setQtyOnHand(0);
  };

  // Helper to build the exact Purchase Return HTML for Print preview and PDF download
  const buildPurchaseReturnHtml = (targetNo: string, printData: any) => {
    const { company, formulas, header, details: itemsList } = printData;

    const compAddress1 = formulas?.CompAddress1 || company?.name || 'SMART';
    const compAddress2 = formulas?.CompAddress2 || [company?.address1, company?.address2, company?.city].filter(Boolean).join(' ');
    const compAddress3 = formulas?.CompAddress3 || company?.phone || '';
    const compAddress4 = formulas?.CompAddress4 || company?.fax || '';

    const rawRetDate = header.ReturnDate || header.CreateDate;
    let returnDateFormatted = '';
    if (rawRetDate) {
      const d = new Date(rawRetDate);
      if (!isNaN(d.getTime())) {
        returnDateFormatted = `${String(d.getDate()).padStart(2, '0')}/${String(d.getMonth() + 1).padStart(2, '0')}/${d.getFullYear()}`;
      } else {
        returnDateFormatted = String(rawRetDate);
      }
    }

    const now = new Date();
    const printTimeStr = `${String(now.getDate()).padStart(2, '0')}/${String(now.getMonth() + 1).padStart(2, '0')}/${now.getFullYear()} ${now.toLocaleTimeString()}`;

    const totalWt = parseFloat(header.TotalWeight) || (itemsList || []).reduce((acc: number, it: any) => acc + (parseFloat(it.Weight) || 0), 0);
    const totalPcsCount = parseFloat(header.TotalPCS) || (itemsList || []).reduce((acc: number, it: any) => acc + (parseFloat(it.PCS) || 0), 0);
    const totalVal = parseFloat(header.TotalValue) || (itemsList || []).reduce((acc: number, it: any) => acc + (parseFloat(it.TotalValue) || 0), 0);
    const totalGstVal = parseFloat(header.TotalGst) || 0;
    const netAmountVal = parseFloat(header.NetAmount) || (totalVal + totalGstVal);

    return `
      <!DOCTYPE html>
      <html>
        <head>
          <meta charset="utf-8" />
          <title>Purchase Return - ${targetNo}</title>
          <style>
            @page {
              size: A4 portrait;
              margin: 0 !important;
            }
            ${getStandardReportCss()}

            @media print {
              @page {
                margin: 0 !important;
              }
              html, body {
                width: 210mm !important;
                height: 100% !important;
                margin: 0 !important;
                padding: 0 !important;
                background: #FFFFFF !important;
                -webkit-print-color-adjust: exact !important;
                print-color-adjust: exact !important;
              }
              .page-sheet {
                width: 210mm !important;
                min-height: 297mm !important;
                height: 297mm !important;
                margin: 0 !important;
                padding: 12mm 14mm !important;
                box-shadow: none !important;
                display: flex !important;
                flex-direction: column !important;
                justify-content: space-between !important;
                box-sizing: border-box !important;
                page-break-after: avoid !important;
                break-after: avoid !important;
              }
            }

            html, body {
              height: 100%;
              margin: 0;
              padding: 0;
            }

            .page-sheet {
              width: 210mm;
              min-height: 297mm;
              padding: 12mm 14mm;
              display: flex;
              flex-direction: column;
              justify-content: space-between;
              box-sizing: border-box;
            }

            .report-main-content {
              flex-grow: 1;
            }

            .std-footer-container {
              margin-top: auto !important;
              padding-top: 16px !important;
              page-break-inside: avoid !important;
              break-inside: avoid !important;
            }

            /* Cambria Typography across all elements */
            *, *:before, *:after, body, div, table, tr, th, td, span, p, h1, h2, h3, h4, h5, h6 {
              font-family: 'Cambria', Georgia, 'Times New Roman', serif !important;
            }

            body {
              font-family: 'Cambria', Georgia, 'Times New Roman', serif !important;
            }

            .std-company-name {
              font-family: 'Cambria', Georgia, serif !important;
              font-size: 24px !important;
              font-weight: 800 !important;
              letter-spacing: 2px !important;
              color: #A37416 !important;
            }

            .std-company-address {
              font-family: 'Cambria', Georgia, serif !important;
              font-size: 11px !important;
              font-weight: 700 !important;
              color: #1A1A1A !important;
              letter-spacing: 0.5px !important;
            }

            .std-report-title {
              font-family: 'Cambria', Georgia, serif !important;
              font-size: 22px !important;
              font-weight: 800 !important;
              letter-spacing: 1.5px !important;
              color: #4A3511 !important;
            }

            .std-printed-time {
              font-family: 'Cambria', Georgia, serif !important;
              font-size: 11.5px !important;
              font-weight: 700 !important;
              color: #222222 !important;
            }

            /* Custom overrides for Purchase Return layout */
            .info-grid {
              display: grid;
              grid-template-columns: 1fr 1fr;
              gap: 16px;
              margin-bottom: 12px;
            }
            .info-card {
              border: 1px solid #C49A45;
              background: #FAF8F5;
              border-radius: 4px;
              padding: 10px 14px;
            }
            .card-title {
              font-size: 11.5px;
              font-weight: 800;
              color: #8C5D08;
              border-bottom: 1px dotted #DDC89A;
              padding-bottom: 5px;
              margin-bottom: 8px;
              text-transform: uppercase;
              letter-spacing: 0.5px;
            }
            .info-table {
              width: 100%;
              border-collapse: collapse;
              font-size: 11px;
            }
            .info-table td {
              padding: 2.5px 0;
              vertical-align: top;
              line-height: 1.35;
            }
            .info-table .lbl {
              width: 130px;
              font-weight: 700;
              color: #222222;
            }
            .info-table .colon {
              width: 15px;
              font-weight: 700;
              color: #222222;
              text-align: center;
            }
            .info-table .val {
              color: #222222;
              font-weight: 700;
              word-break: break-word;
            }

            /* DETAIL ITEMS TABLE */
            .detail-table {
              width: 100%;
              border-collapse: collapse;
              margin-top: 4px;
              margin-bottom: 12px;
              font-size: 11px;
            }
            .detail-table th {
              background-color: #8C5D08;
              color: #FFFFFF;
              text-align: left;
              padding: 6px 8px;
              font-weight: 800;
              border: 1px solid #734C06;
              text-transform: uppercase;
              font-size: 10px;
              letter-spacing: 0.5px;
            }
            .detail-table td {
              padding: 6px 8px;
              border: 1px solid #D9C8A5;
              background-color: #FFFFFF;
              color: #222222;
            }
            .detail-table tr:nth-child(even) td {
              background-color: #FAF8F5;
            }

            /* TOTALS SECTION */
            .totals-section {
              display: flex;
              justify-content: flex-end;
              margin-bottom: 14px;
            }
            .totals-box {
              width: 290px;
              border: 1px solid #C49A45;
              background-color: #FFFFFF;
              border-radius: 4px;
              padding: 8px 12px;
            }
            .total-row {
              display: flex;
              justify-content: space-between;
              font-size: 11.5px;
              padding: 3px 0;
              color: #222222;
              font-weight: 700;
            }
            .total-row-grand {
              border-top: 2px solid #8C5D08;
              margin-top: 4px;
              padding-top: 4px;
              font-weight: 800;
              font-size: 13.5px;
              color: #8C5D08;
            }
          </style>
        </head>
        <body>
          <div class="page-sheet">
            <div class="report-main-content">
              ${renderStandardReportHeader({
                company: {
                  name: compAddress1,
                  address: compAddress2,
                  phone: compAddress3,
                  fax: compAddress4
                },
                reportTitle: 'PURCHASE RETURN',
                printedDateTime: printTimeStr
              })}

              <!-- INFO GRID -->
              <div class="info-grid">
                <!-- RETURN REFERENCE DETAILS -->
                <div class="info-card">
                  <div class="card-title">RETURN REFERENCE DETAILS</div>
                  <table class="info-table">
                    <tr>
                      <td class="lbl">Return Reference<br/>Number</td>
                      <td class="colon">:</td>
                      <td class="val">${targetNo}</td>
                    </tr>
                    <tr>
                      <td class="lbl">Return Date</td>
                      <td class="colon">:</td>
                      <td class="val">${returnDateFormatted || '-'}</td>
                    </tr>
                    <tr>
                      <td class="lbl">Location Code</td>
                      <td class="colon">:</td>
                      <td class="val">${(header.LocationCode || '').trim() || '-'}</td>
                    </tr>
                    <tr>
                      <td class="lbl">Created By</td>
                      <td class="colon">:</td>
                      <td class="val">${(header.CreateUser || 'sys').trim()}</td>
                    </tr>
                  </table>
                </div>

                <!-- SUPPLIER DETAILS -->
                <div class="info-card">
                  <div class="card-title">SUPPLIER DETAILS</div>
                  <table class="info-table">
                    <tr>
                      <td class="lbl">Supplier Code</td>
                      <td class="colon">:</td>
                      <td class="val">${(header.SupplierCode || '').trim() || '-'}</td>
                    </tr>
                    <tr>
                      <td class="lbl">Supplier Name</td>
                      <td class="colon">:</td>
                      <td class="val">${(header.SupplierName || '-').trim()}</td>
                    </tr>
                    <tr>
                      <td class="lbl">Supplier Address</td>
                      <td class="colon">:</td>
                      <td class="val">${[header.SupplierAddress1, header.SupplierAddress2].filter(Boolean).map((s: string) => s.trim()).join(', ') || '-'}</td>
                    </tr>
                    <tr>
                      <td class="lbl">Remarks</td>
                      <td class="colon">:</td>
                      <td class="val">${(header.Remarks || '').trim() || '-'}</td>
                    </tr>
                  </table>
                </div>
              </div>

              <!-- DETAIL ITEMS TABLE -->
              <table class="detail-table">
                <thead>
                  <tr>
                    <th style="width: 4%; text-align: center;">#</th>
                    <th style="width: 17%;">INVENTORY CODE</th>
                    <th style="width: 25%;">DESCRIPTION</th>
                    <th style="width: 12%; text-align: right;">WEIGHT (G)</th>
                    <th style="width: 11%; text-align: right;">GOLD LOSS</th>
                    <th style="width: 10%; text-align: right;">MC</th>
                    <th style="width: 6%; text-align: center;">PCS</th>
                    <th style="width: 15%; text-align: right;">VALUE ($)</th>
                  </tr>
                </thead>
                <tbody>
                  ${(itemsList || []).length > 0 ? (itemsList || []).map((row: any, idx: number) => {
                    const invCode = (row.InventoryCode || '').trim();
                    const desc = (row.Description || '').trim();
                    const wt = parseFloat(row.Weight) || 0;
                    const gloss = parseFloat(row.GoldLoss) || 0;
                    const costPerGm = parseFloat(row.CostPerGram) || parseFloat(row.MC) || 0;
                    const pces = parseInt(row.PCS) || 0;
                    const val = parseFloat(row.TotalValue) || 0;

                    return `
                      <tr>
                        <td style="text-align: center; font-weight: 700;">${idx + 1}</td>
                        <td style="font-weight: 700;">${invCode}</td>
                        <td style="font-weight: 700; text-transform: uppercase;">${desc || '-'}</td>
                        <td style="text-align: right; font-weight: 700;">${wt.toFixed(3)}</td>
                        <td style="text-align: right;">${gloss.toFixed(3)}</td>
                        <td style="text-align: right;">$ ${costPerGm.toFixed(2)}</td>
                        <td style="text-align: center; font-weight: 700;">${pces}</td>
                        <td style="text-align: right; font-weight: 800; color: #8C5D08;">$ ${val.toFixed(2)}</td>
                      </tr>
                    `;
                  }).join('') : `
                    <tr>
                      <td colspan="8" style="text-align: center; padding: 14px; color: #777;">
                        No detail records in this Purchase Return
                      </td>
                    </tr>
                  `}
                </tbody>
              </table>

              <!-- TOTALS SECTION -->
              <div class="totals-section">
                <div class="totals-box">
                  <div class="total-row">
                    <span>Total Weight:</span>
                    <span style="font-weight: 800;">${totalWt.toFixed(3)} G</span>
                  </div>
                  <div class="total-row">
                    <span>Total PCS:</span>
                    <span style="font-weight: 800;">${totalPcsCount}</span>
                  </div>
                  <div class="total-row">
                    <span>Total Cost:</span>
                    <span style="font-weight: 800;">$ ${totalVal.toFixed(2)}</span>
                  </div>
                  ${totalGstVal > 0 ? `
                  <div class="total-row">
                    <span>GST Amount:</span>
                    <span style="font-weight: 800;">$ ${totalGstVal.toFixed(2)}</span>
                  </div>` : ''}
                  <div class="total-row total-row-grand">
                    <span>NET TOTAL:</span>
                    <span>$ ${netAmountVal.toFixed(2)}</span>
                  </div>
                </div>
              </div>
            </div>

            ${renderStandardReportFooter({
              staff: {
                staffLabel: 'PREPARED BY (STAFF)',
                staffName: (header.CreateUser || 'UPS').trim(),
                receiverLabel: 'SUPPLIER / AUTHORIZED RECEIVER',
                receiverName: (header.SupplierName || '-').trim()
              }
            })}
          </div>
        </body>
      </html>
    `;
  };

  // subPrintPurchaseReturn migrated from btnReport_Click in frmPurchaseReturn.vb
  const subPrintPurchaseReturn = async (returnNoToPrint?: string) => {
    const targetNo = (returnNoToPrint || refNo || '').trim();
    if (!targetNo) {
      showToast('Return Reference Number is required', 'error');
      return;
    }

    try {
      setLoading(true);
      const res = await fetch(`${API_URL}/purchasereturn/print-data/${encodeURIComponent(targetNo)}`);
      const result = await res.json();

      if (!result.success || !result.data) {
        showToast(result.message || `No Purchase Return records found for: ${targetNo}`, 'error');
        return;
      }

      const htmlContent = buildPurchaseReturnHtml(targetNo, result.data);

      const printScript = `
        <script>
          window.onload = function() {
            setTimeout(function() {
              window.print();
            }, 300);
          };
        </script>
      `;
      const printHtml = htmlContent.replace('</body>', `${printScript}</body>`);

      if (Platform.OS === 'web' && typeof window !== 'undefined') {
        const frameId = '__purchase_return_print_frame__';
        let iframe = document.getElementById(frameId) as HTMLIFrameElement;
        if (!iframe) {
          iframe = document.createElement('iframe');
          iframe.id = frameId;
          iframe.style.position = 'fixed';
          iframe.style.right = '0';
          iframe.style.bottom = '0';
          iframe.style.width = '0';
          iframe.style.height = '0';
          iframe.style.border = '0';
          iframe.style.visibility = 'hidden';
          document.body.appendChild(iframe);
        }

        const iframeDoc = iframe.contentDocument || iframe.contentWindow?.document;
        if (iframeDoc && iframe.contentWindow) {
          iframeDoc.open();
          iframeDoc.write(htmlContent);
          iframeDoc.title = `Purchase Return - ${targetNo}`;
          iframeDoc.close();

          setTimeout(() => {
            try {
              iframe.contentWindow?.focus();
              iframe.contentWindow?.print();
              showToast(`Purchase Return ${targetNo} print dialog opened`);
            } catch (printErr) {
              console.error('Iframe print error:', printErr);
              // Fallback to window.open if iframe printing is blocked
              const printWindow = window.open('', '_blank', 'width=900,height=750');
              if (printWindow) {
                printWindow.document.open();
                printWindow.document.write(printHtml);
                printWindow.document.title = `Purchase Return - ${targetNo}`;
                printWindow.document.close();
              }
            }
          }, 350);
        } else {
          const printWindow = window.open('', '_blank', 'width=900,height=750');
          if (printWindow) {
            printWindow.document.open();
            printWindow.document.write(printHtml);
            printWindow.document.title = `Purchase Return - ${targetNo}`;
            printWindow.document.close();
            showToast(`Purchase Return ${targetNo} print preview opened`);
          }
        }
      } else {
        showToast(`Purchase Return ${targetNo} sent to printer.`, 'success');
      }
    } catch (err: any) {
      console.error(err);
      showToast('Failed to load Purchase Return print data', 'error');
    } finally {
      setLoading(false);
    }
  };

  // Direct PDF Download functionality
  const handleDownloadPdf = async (returnNoToExport?: string) => {
    const targetNo = (returnNoToExport || refNo || '').trim();
    if (!targetNo) {
      showToast('Return Reference Number is required', 'error');
      return;
    }

    try {
      setLoading(true);
      const res = await fetch(`${API_URL}/purchasereturn/print-data/${encodeURIComponent(targetNo)}`);
      const result = await res.json();

      if (!result.success || !result.data) {
        showToast(result.message || `No Purchase Return records found for: ${targetNo}`, 'error');
        return;
      }

      const htmlContent = buildPurchaseReturnHtml(targetNo, result.data);
      const downloadFilename = `Purchase_Return_${targetNo}.pdf`;

      // Request PDF conversion from backend headless browser
      const pdfRes = await fetch(`${API_URL}/purchasereturn/export-pdf`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          html: htmlContent,
          filename: downloadFilename
        })
      });

      if (!pdfRes.ok) {
        throw new Error('Server returned error while generating PDF');
      }

      const pdfBlob = await pdfRes.blob();

      if (Platform.OS === 'web' && typeof window !== 'undefined') {
        const url = URL.createObjectURL(pdfBlob);
        const a = document.createElement('a');
        a.href = url;
        a.download = downloadFilename;
        document.body.appendChild(a);
        a.click();
        document.body.removeChild(a);
        setTimeout(() => URL.revokeObjectURL(url), 1000);
        showToast(`Purchase Return PDF downloaded: ${downloadFilename}`, 'success');
      } else {
        showToast('PDF generated successfully.', 'success');
      }
    } catch (err: any) {
      console.error('PDF Export Error:', err);
      showToast('Failed to download PDF: ' + (err.message || 'Error'), 'error');
    } finally {
      setLoading(false);
    }
  };

  // Direct Excel Download functionality
  const handleDownloadExcel = async (returnNoToExport?: string) => {
    const targetNo = (returnNoToExport || refNo || '').trim();
    if (!targetNo) {
      showToast('Return Reference Number is required', 'error');
      return;
    }

    try {
      setLoading(true);
      const excelRes = await fetch(`${API_URL}/purchasereturn/export-excel`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          returnNo: targetNo
        })
      });

      if (!excelRes.ok) {
        const errJson = await excelRes.json().catch(() => ({}));
        throw new Error(errJson.message || 'Server error generating Excel file');
      }

      const excelBlob = await excelRes.blob();
      const downloadFilename = `Purchase_Return_${targetNo}.xlsx`;

      if (Platform.OS === 'web' && typeof window !== 'undefined') {
        const url = URL.createObjectURL(excelBlob);
        const a = document.createElement('a');
        a.href = url;
        a.download = downloadFilename;
        document.body.appendChild(a);
        a.click();
        document.body.removeChild(a);
        setTimeout(() => URL.revokeObjectURL(url), 1000);
        showToast(`Purchase Return Excel downloaded: ${downloadFilename}`, 'success');
      } else {
        showToast('Excel file generated successfully.', 'success');
      }
    } catch (err: any) {
      console.error('Excel Export Error:', err);
      showToast('Failed to download Excel: ' + (err.message || 'Error'), 'error');
    } finally {
      setLoading(false);
    }
  };

  const handleClearAll = () => {
    setRefNo('');
    setReturnDate(new Date().toISOString().split('T')[0]);
    setSupplierCode('');
    setSupplierName('');
    setRemarks('');
    setGstActive(false);
    setTotalCost(0);
    setGstAmount(0);
    setTotalMc(0);
    setTotalWeight(0);
    setTotalPcs(0);
    setDetails([]);
    setSelectedIndex(null);
    clearItemFields();
    showToast('Form Cleared', 'success');
  };

  const handleSave = async () => {
    if (!returnDate || isNaN(new Date(returnDate).getTime())) {
      showToast('Please enter the valid Return Date!', 'error');
      return;
    }
    if (!supplierCode.trim()) {
      showToast('Supplier Code is required', 'error');
      return;
    }
    if (details.length === 0) {
      showToast('There is no inventory in the Purchase Return List!', 'error');
      return;
    }
    if (refNo) {
      showToast('You can take only the print out for old Purchase Return!', 'error');
      return;
    }

    setSaving(true);
    try {
      const user = await getLoggedInUser();
      const res = await fetch(`${API_URL}/purchasereturn`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          header: {
            SupplierCode: supplierCode.trim(),
            ReturnDate: returnDate,
            TotalWeight: totalWeight,
            TotalPCS: totalPcs,
            TotalValue: totalCost,
            TotalGST: gstAmount,
            NetTotal: totalMc,
            Remarks: remarks,
            LocationCode: 'HQ'
          },
          details: details,
          userId: user.userId
        })
      });
      const json = await res.json();
      if (json.success) {
        setRefNo(json.returnNo);
        showToast(json.message || `Please take Note Reference No : ${json.returnNo}`, 'success');
        setDetails([]);
        setSelectedIndex(null);
        // Prompt confirmation: "Do you want to print ?"
        setPrintConfirmReturnNo(json.returnNo);
      } else {
        const errorDetail = json.error ? `\nDetails: ${json.error}` : '';
        showToast(`${json.message || 'Failed to save purchase return'}${errorDetail}`, 'error');
      }
    } catch (e) {
      showToast('Server connection failed', 'error');
    } finally {
      setSaving(false);
    }
  };

  const Checkbox = ({ label, selected, onPress }: { label: string; selected: boolean; onPress: () => void }) => (
    <TouchableOpacity style={styles.radioContainer} onPress={onPress}>
      <View style={[styles.checkboxSquare, selected && styles.checkboxSquareActive]}>
        {selected && <Ionicons name="checkmark" size={10} color="#FFFFFF" />}
      </View>
      {label ? <ThemedText style={styles.radioLabel}>{label}</ThemedText> : null}
    </TouchableOpacity>
  );

  return (
    <SafeAreaView edges={['top']} style={styles.safeArea}>
      <Stack.Screen options={{ headerShown: false }} />

      {/* Top Header Bar matching jobsheet.tsx */}
      <View style={styles.header}>
        <View style={styles.headerLeft}>
          <TouchableOpacity onPress={() => router.back()} style={styles.backButton}>
            <Ionicons name="arrow-back" size={20} color="#3B2D17" />
          </TouchableOpacity>
          <ThemedText style={styles.headerTitle}>Purchase Return</ThemedText>
        </View>

        <View style={styles.headerActions}>
          <TouchableOpacity style={styles.clearBtn} onPress={handleClearAll}>
            <ThemedText style={styles.clearBtnText}>Clear</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity
            style={styles.saveBtn}
            onPress={handleSave}
            disabled={saving}
          >
            <ThemedText style={styles.saveBtnText}>{saving ? 'Saving...' : 'Save'}</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity
            style={[styles.printBtn, !refNo && { opacity: 0.5 }]}
            onPress={() => subPrintPurchaseReturn(refNo)}
            disabled={!refNo}
          >
            <Ionicons name="print-outline" size={14} color="#5C4A28" />
            <ThemedText style={styles.printBtnText}>Print</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity
            style={[styles.printBtn, !refNo && { opacity: 0.5 }]}
            onPress={() => handleDownloadPdf(refNo)}
            disabled={!refNo}
          >
            <Ionicons name="download-outline" size={14} color="#B3261E" />
            <ThemedText style={[styles.printBtnText, { color: '#B3261E' }]}>PDF</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity
            style={[styles.printBtn, !refNo && { opacity: 0.5 }]}
            onPress={() => handleDownloadExcel(refNo)}
            disabled={!refNo}
          >
            <Ionicons name="grid-outline" size={14} color="#2E7D32" />
            <ThemedText style={[styles.printBtnText, { color: '#2E7D32' }]}>Excel</ThemedText>
          </TouchableOpacity>
        </View>
      </View>

      <ScrollView style={styles.container} contentContainerStyle={styles.contentContainer}>
        {/* TOP SECTION: Form Header (JobSheet-style Gold Card) */}
        <View style={styles.goldCard}>
          {/* Card Header Banner */}
          <View style={[styles.cardHeaderBanner, { justifyContent: 'flex-start' }]}>
            <View style={styles.headerIconBadge}>
              <Ionicons name="document-text" size={15} color="#B48218" />
            </View>
            <ThemedText style={styles.cardHeaderTitle}>Purchase Return Details</ThemedText>
          </View>

          <View style={styles.cardBody}>
            {/* Row 1: Ref. No, Return Date, Total Cost */}
            <View style={styles.row}>
              <ThemedText style={styles.label}>Ref. No.</ThemedText>
              <TextInput
                style={[styles.input, styles.readonlyInput, { width: 140 }]}
                value={refNo}
                editable={false}
                placeholder="Auto"
              />

              <ThemedText style={[styles.labelRight, { width: 95 }]}>Return Date</ThemedText>
              {Platform.OS === 'web' ? (
                <input
                  type="date"
                  style={{
                    width: 140,
                    border: '1px solid #E2D7C5',
                    borderRadius: '6px',
                    padding: '4px 8px',
                    fontSize: '12px',
                    height: '28px',
                    color: '#2C2519',
                    backgroundColor: '#FFFFFF',
                    outline: 'none',
                    boxSizing: 'border-box',
                  }}
                  value={returnDate}
                  onChange={(e: any) => setReturnDate(e.target.value || '')}
                />
              ) : (
                <TextInput
                  style={[styles.input, { width: 140 }]}
                  value={returnDate}
                  onChangeText={setReturnDate}
                  placeholder="YYYY-MM-DD"
                />
              )}

              <ThemedText style={[styles.labelRight, { width: 85 }]}>Total Cost</ThemedText>
              <TextInput
                style={[styles.input, styles.readonlyInput, { width: 120, textAlign: 'right', fontWeight: 'bold' }]}
                value={totalCost.toFixed(2)}
                editable={false}
              />
            </View>

            {/* Row 2: Supplier Code, Supplier Name, GST Checkbox & Amount */}
            <View style={styles.row}>
              <ThemedText style={styles.label}>Supplier Code</ThemedText>
              <View style={{ width: 140, flexDirection: 'row' }}>
                <TextInput
                  style={[styles.input, { width: 108, borderTopRightRadius: 0, borderBottomRightRadius: 0, borderRightWidth: 0 }]}
                  value={supplierCode}
                  onChangeText={setSupplierCode}
                  onBlur={handleSupplierCodeBlur}
                  placeholder="Code"
                />
                <TouchableOpacity style={styles.lookupBtn} onPress={fetchSuppliers}>
                  <ThemedText style={styles.lookupBtnText}>...</ThemedText>
                </TouchableOpacity>
              </View>

              <TextInput
                style={[styles.input, styles.readonlyInput, { flex: 1, minWidth: 180, marginLeft: 8 }]}
                value={supplierName}
                editable={false}
                placeholder="Supplier Name"
              />

              <View style={{ flexDirection: 'row', alignItems: 'center', marginLeft: 16 }}>
                <Checkbox label="GST" selected={gstActive} onPress={() => setGstActive(!gstActive)} />
                <TextInput
                  style={[styles.input, styles.readonlyInput, { width: 90, textAlign: 'right' }]}
                  value={gstAmount.toFixed(2)}
                  editable={false}
                />
              </View>
            </View>

            {/* Row 3: Remarks, Total MC */}
            <View style={styles.row}>
              <ThemedText style={styles.label}>Remarks</ThemedText>
              <TextInput
                style={[styles.input, { flex: 1, minWidth: 200 }]}
                value={remarks}
                onChangeText={setRemarks}
                placeholder="Enter remarks..."
              />

              <ThemedText style={[styles.labelRight, { width: 85 }]}>Total MC</ThemedText>
              <TextInput
                style={[styles.input, styles.readonlyInput, { width: 120, textAlign: 'right', fontWeight: 'bold', color: '#9E6D08' }]}
                value={totalMc.toFixed(2)}
                editable={false}
              />
            </View>
          </View>
        </View>

        {/* MIDDLE SECTION: Items Grid Table (JobSheet-style Table) */}
        <View style={styles.goldCard}>

          <ScrollView horizontal={true} showsHorizontalScrollIndicator={true} contentContainerStyle={{ flexGrow: 1 }}>
            <View style={{ width: '100%', minWidth: 920 }}>
              {/* Table Header */}
              <View style={styles.tableHeader}>
                <ThemedText style={[styles.tableHeaderText, { width: 140 }]}>Inventory Code</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { flex: 2 }]}>Description</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { width: 90, textAlign: 'right' }]}>Weight</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { width: 90, textAlign: 'right' }]}>Gold Loss</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { width: 100, textAlign: 'right' }]}>Cost/Gram</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { width: 90, textAlign: 'right' }]}>MC</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { width: 70, textAlign: 'center' }]}>PCS</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { width: 110, textAlign: 'right' }]}>Value</ThemedText>
              </View>

              {/* Table Body */}
              <ScrollView nestedScrollEnabled={true} style={{ maxHeight: 220, minHeight: details.length === 0 ? 80 : 0 }}>
                {details.length === 0 ? (
                  <View style={styles.emptyTableState}>
                    <Ionicons name="cube-outline" size={28} color="#D0C2A8" />
                    <ThemedText style={{ color: '#7D6F5B', fontSize: 12, marginTop: 4 }}>
                      No items added to return list yet
                    </ThemedText>
                  </View>
                ) : (
                  details.map((item, idx) => {
                    const isSelected = selectedIndex === idx;
                    return (
                      <TouchableOpacity
                        key={idx}
                        activeOpacity={0.7}
                        style={[
                          styles.tableRow,
                          idx % 2 === 1 && { backgroundColor: '#FCFAF6' },
                          isSelected && styles.tableRowSelected,
                        ]}
                        onPress={() => setSelectedIndex(isSelected ? null : idx)}
                      >
                        <ThemedText style={[styles.tableCellText, { width: 140, fontWeight: 'bold' }]}>
                          {item.InventoryCode}
                        </ThemedText>
                        <ThemedText style={[styles.tableCellText, { flex: 2 }]} numberOfLines={1}>
                          {item.Description}
                        </ThemedText>
                        <ThemedText style={[styles.tableCellText, { width: 90, textAlign: 'right' }]}>
                          {item.Weight ? Number(item.Weight).toFixed(3) : '0.000'}
                        </ThemedText>
                        <ThemedText style={[styles.tableCellText, { width: 90, textAlign: 'right' }]}>
                          {item.GoldLoss ? Number(item.GoldLoss).toFixed(3) : '0.000'}
                        </ThemedText>
                        <ThemedText style={[styles.tableCellText, { width: 100, textAlign: 'right' }]}>
                          {item.CostPerGram ? Number(item.CostPerGram).toFixed(2) : '0.00'}
                        </ThemedText>
                        <ThemedText style={[styles.tableCellText, { width: 90, textAlign: 'right' }]}>
                          {item.MC ? Number(item.MC).toFixed(2) : '0.00'}
                        </ThemedText>
                        <ThemedText style={[styles.tableCellText, { width: 70, textAlign: 'center', fontWeight: 'bold' }]}>
                          {item.PCS}
                        </ThemedText>
                        <ThemedText style={[styles.tableCellText, { width: 110, textAlign: 'right', fontWeight: 'bold', color: '#9E6D08' }]}>
                          {item.TotalCost ? Number(item.TotalCost).toFixed(2) : '0.00'}
                        </ThemedText>
                      </TouchableOpacity>
                    );
                  })
                )}
              </ScrollView>

              {/* Table Total Summary Row */}
              <View style={styles.tableTotalRow}>
                <ThemedText style={[styles.tableTotalCellText, { width: 140 }]}>TOTAL SUMMARY</ThemedText>
                <View style={{ flex: 2 }} />
                <ThemedText style={[styles.tableTotalCellText, { width: 90, textAlign: 'right' }]}>
                  {totalWeight.toFixed(3)} g
                </ThemedText>
                <View style={{ width: 90 }} />
                <View style={{ width: 100 }} />
                <View style={{ width: 90 }} />
                <ThemedText style={[styles.tableTotalCellText, { width: 70, textAlign: 'center' }]}>
                  {totalPcs}
                </ThemedText>
                <ThemedText style={[styles.tableTotalCellText, { width: 110, textAlign: 'right', color: '#9E6D08' }]}>
                  ${totalCost.toFixed(2)}
                </ThemedText>
              </View>
            </View>
          </ScrollView>
        </View>

        {/* BOTTOM SECTION: Add / Edit Item Form (JobSheet-style Gold Card) */}
        <View style={styles.goldCard}>
          <View style={styles.cardBody}>
            {/* Row 1: Code, Desc, Weight, Gold Loss, MC, Cost/Gram, PCS, Value */}
            <View style={styles.row}>
              {/* Inventory Code */}
              <View style={{ width: 160, marginRight: 8 }}>
                <ThemedText style={styles.inputSubLabel}>Inventory Code</ThemedText>
                <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                  <TextInput
                    style={[
                      styles.input,
                      {
                        flex: 1,
                        borderTopRightRadius: 0,
                        borderBottomRightRadius: 0,
                        borderRightWidth: 0,
                        minWidth: 0,
                      },
                    ]}
                    value={inventoryCode}
                    onChangeText={setInventoryCode}
                    onBlur={handleInventoryCodeBlur}
                    placeholder="Code"
                  />
                  <TouchableOpacity
                    style={styles.lookupBtn}
                    onPress={fetchInventoryItems}
                    activeOpacity={0.7}
                  >
                    <ThemedText style={styles.lookupBtnText}>...</ThemedText>
                  </TouchableOpacity>
                </View>
              </View>

              {/* Description */}
              <View style={{ flex: 2, minWidth: 150, marginRight: 8 }}>
                <ThemedText style={styles.inputSubLabel}>Description</ThemedText>
                <TextInput style={[styles.input, styles.readonlyInput]} value={description} editable={false} />
              </View>

              {/* Weight */}
              <View style={{ width: 78, marginRight: 8 }}>
                <ThemedText style={styles.inputSubLabel}>Weight</ThemedText>
                <TextInput style={[styles.input, styles.readonlyInput, { textAlign: 'right' }]} value={weight} editable={false} />
              </View>

              {/* Gold Loss */}
              <View style={{ width: 78, marginRight: 8 }}>
                <ThemedText style={styles.inputSubLabel}>Gold Loss</ThemedText>
                <TextInput style={[styles.input, styles.readonlyInput, { textAlign: 'right' }]} value={goldLoss} editable={false} />
              </View>

              {/* MC */}
              <View style={{ width: 75, marginRight: 8 }}>
                <ThemedText style={styles.inputSubLabel}>MC</ThemedText>
                <TextInput style={[styles.input, styles.readonlyInput, { textAlign: 'right' }]} value={mc} editable={false} />
              </View>

              {/* Cost/Gram */}
              <View style={{ width: 85, marginRight: 8 }}>
                <ThemedText style={styles.inputSubLabel}>Cost/Gram</ThemedText>
                <TextInput style={[styles.input, styles.readonlyInput, { textAlign: 'right' }]} value={costPerGram} editable={false} />
              </View>

              {/* PCS */}
              <View style={{ width: 68, marginRight: 8 }}>
                <ThemedText style={styles.inputSubLabel}>PCS</ThemedText>
                <TextInput
                  style={[
                    styles.input,
                    purchaseType === 'W' && styles.readonlyInput,
                    { textAlign: 'center', fontWeight: 'bold' },
                  ]}
                  value={pcs}
                  onChangeText={handlePcsChange}
                  onBlur={handlePcsBlur}
                  keyboardType="numeric"
                  editable={purchaseType !== 'W'}
                />
              </View>

              {/* Value */}
              <View style={{ width: 95 }}>
                <ThemedText style={styles.inputSubLabel}>Value</ThemedText>
                <TextInput
                  style={[styles.input, styles.readonlyInput, { textAlign: 'right', fontWeight: 'bold' }]}
                  value={value}
                  editable={false}
                />
              </View>
            </View>

            {/* Bottom Action Buttons */}
            <View style={styles.actionButtonRow}>
              <TouchableOpacity
                style={[styles.deleteMiniBtn, selectedIndex === null && { opacity: 0.5 }]}
                onPress={handleDeleteRow}
                disabled={selectedIndex === null}
              >
                <ThemedText style={styles.deleteMiniBtnText}>Delete</ThemedText>
              </TouchableOpacity>

              <TouchableOpacity style={styles.clearMiniBtn} onPress={clearItemFields}>
                <ThemedText style={styles.clearMiniBtnText}>Clear</ThemedText>
              </TouchableOpacity>

              <TouchableOpacity style={styles.addMiniBtn} onPress={handleAddRow}>
                <ThemedText style={styles.addMiniBtnText}>Add</ThemedText>
              </TouchableOpacity>
            </View>
          </View>
        </View>
      </ScrollView>

      {/* Confirmation Modal */}
      {confirmDialog && confirmDialog.visible && (
        <Modal visible={true} transparent={true} animationType="fade">
          <View style={styles.modalOverlay}>
            <View style={[styles.modalContainer, { maxWidth: 440, padding: 20 }]}>
              <View style={{ flexDirection: 'row', alignItems: 'center', marginBottom: 12 }}>
                <Ionicons name="help-circle-outline" size={24} color="#B48218" style={{ marginRight: 8 }} />
                <ThemedText style={{ fontSize: 16, fontWeight: 'bold', color: '#3B2D17' }}>Confirmation</ThemedText>
              </View>
              <ThemedText style={{ fontSize: 13, color: '#4A3B22', lineHeight: 20, marginBottom: 20 }}>
                {confirmDialog.message}
              </ThemedText>
              <View style={{ flexDirection: 'row', justifyContent: 'flex-end', gap: 10 }}>
                <TouchableOpacity
                  style={[styles.modalCloseBtn, { paddingHorizontal: 16 }]}
                  onPress={() => setConfirmDialog(null)}
                >
                  <ThemedText style={styles.modalCloseBtnText}>No</ThemedText>
                </TouchableOpacity>
                <TouchableOpacity
                  style={[styles.saveBtn, { paddingHorizontal: 16 }]}
                  onPress={confirmDialog.onConfirm}
                >
                  <ThemedText style={styles.saveBtnText}>Yes</ThemedText>
                </TouchableOpacity>
              </View>
            </View>
          </View>
        </Modal>
      )}

      {/* Modern LOV Modal matching Jobsheet */}
      <Modal visible={lovVisible} transparent={true} animationType="fade">
        <TouchableWithoutFeedback onPress={() => setLovVisible(false)}>
          <View style={styles.modalOverlay}>
            <TouchableWithoutFeedback>
              <View style={styles.modalContainer}>
                <View style={styles.modalHeader}>
                  <ThemedText style={styles.modalTitle}>{lovTitle}</ThemedText>
                  <TouchableOpacity onPress={() => setLovVisible(false)} style={styles.modalCloseIconBtn}>
                    <Ionicons name="close" size={20} color="#3B2D17" />
                  </TouchableOpacity>
                </View>

                <View style={{ padding: 12 }}>
                  <TextInput
                    style={styles.modalSearchInput}
                    placeholder="Search options..."
                    placeholderTextColor="#7D6F5B"
                    value={lovSearch}
                    onChangeText={handleLovSearch}
                    autoFocus
                  />

                  {lovColumns.length > 0 && (
                    <View style={styles.lovHeaderRow}>
                      {lovColumns.map((col, idx) => (
                        <ThemedText key={idx} style={[styles.lovHeaderText, { flex: col === 'Description' ? 2 : 1 }]}>
                          {col}
                        </ThemedText>
                      ))}
                    </View>
                  )}

                  <FlatList
                    data={filteredLovData}
                    keyExtractor={(item, index) => index.toString()}
                    style={{ maxHeight: 320 }}
                    renderItem={({ item, index }) => (
                      <TouchableOpacity
                        activeOpacity={0.7}
                        style={[
                          styles.lovItem,
                          index % 2 === 1 && { backgroundColor: '#FCFAF6' },
                        ]}
                        onPress={() => handleLovSelect(item)}
                      >
                        {lovColumns.length > 0 ? (
                          lovColumns.map((col, idx) => (
                            <ThemedText
                              key={idx}
                              style={[
                                styles.lovItemText,
                                { flex: col === 'Description' ? 2 : 1 },
                                idx === 0 && { fontWeight: 'bold' },
                              ]}
                            >
                              {String(item[col] ?? '')}
                            </ThemedText>
                          ))
                        ) : (
                          <ThemedText style={styles.lovItemText}>{Object.values(item).join(' - ')}</ThemedText>
                        )}
                      </TouchableOpacity>
                    )}
                    ListEmptyComponent={
                      <View style={{ padding: 20, alignItems: 'center' }}>
                        <ThemedText style={{ color: '#7D6F5B', fontSize: 13, fontStyle: 'italic' }}>
                          No records found
                        </ThemedText>
                      </View>
                    }
                  />
                </View>

                <View style={styles.modalFooter}>
                  <TouchableOpacity style={styles.modalCloseBtn} onPress={() => setLovVisible(false)}>
                    <ThemedText style={styles.modalCloseBtnText}>Close</ThemedText>
                  </TouchableOpacity>
                </View>
              </View>
            </TouchableWithoutFeedback>
          </View>
        </TouchableWithoutFeedback>
      </Modal>

      {/* Confirmation Modal for Print after Save */}
      <Modal visible={printConfirmReturnNo !== null} transparent animationType="fade">
        <TouchableOpacity style={styles.modalOverlay} activeOpacity={1}>
          <TouchableWithoutFeedback>
            <View style={[styles.modalContainer, { maxWidth: 390 }]}>
              <View style={styles.modalHeader}>
                <ThemedText style={styles.modalTitle}>Purchase Return Saved</ThemedText>
              </View>
              <View style={{ padding: 18, alignItems: 'center' }}>
                <Ionicons name="document-text-outline" size={36} color="#8D6409" style={{ marginBottom: 12 }} />
                <ThemedText style={{ fontSize: 13, color: '#3B2D17', textAlign: 'center', marginBottom: 16 }}>
                  Reference No: <ThemedText style={{ fontWeight: 'bold' }}>{printConfirmReturnNo}</ThemedText>
                  {'\n'}Choose an option below:
                </ThemedText>
                <View style={{ flexDirection: 'row', gap: 8, width: '100%', marginBottom: 10 }}>
                  <TouchableOpacity
                    style={[styles.modalActionBtn, styles.modalBtnPrimary, { flex: 1, paddingVertical: 8 }]}
                    onPress={() => {
                      const returnNo = printConfirmReturnNo;
                      setPrintConfirmReturnNo(null);
                      if (returnNo) {
                        subPrintPurchaseReturn(returnNo);
                      }
                    }}
                    activeOpacity={0.7}
                  >
                    <Ionicons name="print-outline" size={13} color="#FFFFFF" style={{ marginRight: 4 }} />
                    <ThemedText style={styles.modalBtnPrimaryText}>Print</ThemedText>
                  </TouchableOpacity>

                  <TouchableOpacity
                    style={[styles.modalActionBtn, { flex: 1, backgroundColor: '#B3261E', paddingVertical: 8 }]}
                    onPress={() => {
                      const returnNo = printConfirmReturnNo;
                      setPrintConfirmReturnNo(null);
                      if (returnNo) {
                        handleDownloadPdf(returnNo);
                      }
                    }}
                    activeOpacity={0.7}
                  >
                    <Ionicons name="download-outline" size={13} color="#FFFFFF" style={{ marginRight: 4 }} />
                    <ThemedText style={[styles.modalBtnPrimaryText, { color: '#FFFFFF' }]}>PDF</ThemedText>
                  </TouchableOpacity>

                  <TouchableOpacity
                    style={[styles.modalActionBtn, { flex: 1, backgroundColor: '#2E7D32', paddingVertical: 8 }]}
                    onPress={() => {
                      const returnNo = printConfirmReturnNo;
                      setPrintConfirmReturnNo(null);
                      if (returnNo) {
                        handleDownloadExcel(returnNo);
                      }
                    }}
                    activeOpacity={0.7}
                  >
                    <Ionicons name="grid-outline" size={13} color="#FFFFFF" style={{ marginRight: 4 }} />
                    <ThemedText style={[styles.modalBtnPrimaryText, { color: '#FFFFFF' }]}>Excel</ThemedText>
                  </TouchableOpacity>
                </View>

                <TouchableOpacity
                  style={[styles.modalActionBtn, styles.modalBtnCancel, { width: '100%', paddingVertical: 8 }]}
                  onPress={() => setPrintConfirmReturnNo(null)}
                  activeOpacity={0.7}
                >
                  <ThemedText style={styles.modalBtnCancelText}>Close</ThemedText>
                </TouchableOpacity>
              </View>
            </View>
          </TouchableWithoutFeedback>
        </TouchableOpacity>
      </Modal>

      {/* Toast Notification */}
      {toast && (
        <View style={[styles.toast, { backgroundColor: toast.type === 'success' ? '#2E7D32' : '#C62828' }]}>
          <ThemedText style={styles.toastText}>{toast.message}</ThemedText>
        </View>
      )}
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  safeArea: {
    flex: 1,
    backgroundColor: '#FBF9F5',
  },
  // Top Header Bar
  header: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingHorizontal: 16,
    paddingVertical: 10,
    borderBottomWidth: 1,
    borderBottomColor: '#EBE2D2',
    backgroundColor: '#FFFFFF',
  },
  headerLeft: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  backButton: {
    padding: 4,
    marginRight: 10,
  },
  headerTitle: {
    fontSize: 20,
    fontWeight: '400',
    letterSpacing: 1,
    color: '#3B2D17',
  },
  headerActions: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 8,
  },
  clearBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#DFD5C3',
    width: 68,
    height: 34,
    borderRadius: 6,
    gap: 4,
  },
  clearBtnText: {
    color: '#5C4A28',
    fontWeight: 'bold',
    fontSize: 12,
  },
  saveBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    backgroundColor: '#F9F2E2',
    borderWidth: 1,
    borderColor: '#B8903B',
    width: 68,
    height: 34,
    borderRadius: 6,
    gap: 4,
  },
  saveBtnText: {
    color: '#9E6D08',
    fontWeight: 'bold',
    fontSize: 12,
  },
  printBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#DFD5C3',
    width: 68,
    height: 34,
    borderRadius: 6,
    gap: 4,
  },
  printBtnText: {
    color: '#5C4A28',
    fontWeight: 'bold',
    fontSize: 12,
  },
  container: {
    flex: 1,
    backgroundColor: '#FBF9F5',
  },
  contentContainer: {
    padding: 12,
    paddingBottom: 28,
  },
  // Gold Cards
  goldCard: {
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#E8DCB8',
    borderRadius: 8,
    marginBottom: 12,
    overflow: 'hidden',
    shadowColor: '#A38035',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.08,
    shadowRadius: 3,
    elevation: 2,
  },
  cardHeaderBanner: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    backgroundColor: '#F5E7CA',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    paddingVertical: 7,
    paddingHorizontal: 10,
  },
  headerIconBadge: {
    width: 22,
    height: 22,
    borderRadius: 11,
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
  counterBadge: {
    backgroundColor: '#FFFFFF',
    paddingHorizontal: 8,
    paddingVertical: 2,
    borderRadius: 10,
    borderWidth: 1,
    borderColor: '#E2D3B3',
  },
  counterBadgeText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#9E6D08',
  },
  cardBody: {
    padding: 10,
  },
  row: {
    flexDirection: 'row',
    alignItems: 'center',
    marginBottom: 7,
    flexWrap: 'wrap',
  },
  label: {
    fontSize: 11,
    fontWeight: 'bold',
    width: 96,
    marginRight: 6,
    color: '#3A2E19',
  },
  labelRight: {
    fontSize: 11,
    fontWeight: 'bold',
    textAlign: 'right',
    marginRight: 8,
    color: '#3A2E19',
  },
  inputSubLabel: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3A2E19',
    marginBottom: 3,
  },
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
    ...(Platform.OS === 'web' ? { outlineStyle: 'none', boxSizing: 'border-box' } : {}),
  } as any,
  readonlyInput: {
    backgroundColor: '#F8F5EE',
    borderColor: '#EADBCA',
    color: '#554736',
  },
  lookupBtn: {
    width: 32,
    height: 28,
    flexShrink: 0,
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
    fontSize: 14,
    lineHeight: 14,
    textAlign: 'center',
  },
  radioContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    marginVertical: 2,
    marginRight: 8,
  },
  checkboxSquare: {
    width: 15,
    height: 15,
    borderWidth: 1.5,
    borderColor: '#B8903B',
    justifyContent: 'center',
    alignItems: 'center',
    marginRight: 5,
    borderRadius: 3,
    backgroundColor: '#FFFFFF',
  },
  checkboxSquareActive: {
    backgroundColor: '#B8903B',
  },
  radioLabel: {
    fontSize: 11,
    color: '#362B1A',
    fontWeight: 'bold',
  },
  // Table
  tableHeader: {
    flexDirection: 'row',
    backgroundColor: '#F5E7CA',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    paddingVertical: 7,
    paddingHorizontal: 6,
  },
  tableHeaderText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3B2D17',
    paddingHorizontal: 4,
  },
  tableRow: {
    flexDirection: 'row',
    paddingVertical: 6,
    paddingHorizontal: 6,
    borderBottomWidth: 1,
    borderBottomColor: '#F0E6D2',
    alignItems: 'center',
    backgroundColor: '#FFFFFF',
  },
  tableRowSelected: {
    backgroundColor: '#FFF6DF',
    borderLeftWidth: 3,
    borderLeftColor: '#B8903B',
  },
  tableCellText: {
    fontSize: 11,
    color: '#332717',
    paddingHorizontal: 4,
  },
  emptyTableState: {
    paddingVertical: 24,
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: '#FFFFFF',
  },
  tableTotalRow: {
    flexDirection: 'row',
    backgroundColor: '#F9F1DF',
    borderTopWidth: 1,
    borderTopColor: '#E5D6B6',
    paddingVertical: 7,
    paddingHorizontal: 6,
    alignItems: 'center',
  },
  tableTotalCellText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#32230D',
    paddingHorizontal: 4,
  },
  // Action Buttons for Item Add Form
  actionButtonRow: {
    flexDirection: 'row',
    justifyContent: 'flex-end',
    alignItems: 'center',
    gap: 8,
    marginTop: 10,
    paddingTop: 8,
    borderTopWidth: 1,
    borderTopColor: '#F2ECE0',
  },
  clearMiniBtn: {
    paddingVertical: 5,
    paddingHorizontal: 12,
    borderRadius: 5,
    borderWidth: 1,
    borderColor: '#DFD5C3',
    backgroundColor: '#FFFFFF',
  },
  clearMiniBtnText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#5C4A28',
  },
  deleteMiniBtn: {
    paddingVertical: 5,
    paddingHorizontal: 12,
    borderRadius: 5,
    borderWidth: 1,
    borderColor: '#F2A8A2',
    backgroundColor: '#FFFFFF',
  },
  deleteMiniBtnText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#D93829',
  },
  addMiniBtn: {
    paddingVertical: 5,
    paddingHorizontal: 16,
    borderRadius: 5,
    borderWidth: 1,
    borderColor: '#B8903B',
    backgroundColor: '#F9F2E2',
  },
  addMiniBtnText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#9E6D08',
  },
  // Modal
  modalOverlay: {
    flex: 1,
    backgroundColor: 'rgba(0,0,0,0.5)',
    justifyContent: 'center',
    alignItems: 'center',
    padding: 16,
  },
  modalContainer: {
    width: '90%',
    maxWidth: 620,
    maxHeight: '80%',
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#E8DCB8',
    borderRadius: 8,
    overflow: 'hidden',
  },
  modalHeader: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    backgroundColor: '#F5E7CA',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    paddingHorizontal: 12,
    paddingVertical: 10,
  },
  modalTitle: {
    fontSize: 14,
    fontWeight: 'bold',
    color: '#3B2D17',
  },
  modalCloseIconBtn: {
    padding: 4,
  },
  modalSearchInput: {
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderRadius: 6,
    paddingHorizontal: 10,
    paddingVertical: 5,
    fontSize: 12,
    height: 32,
    backgroundColor: '#FFFFFF',
    color: '#2C2519',
    marginBottom: 10,
  },
  lovHeaderRow: {
    flexDirection: 'row',
    backgroundColor: '#F5E7CA',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    paddingVertical: 6,
    paddingHorizontal: 8,
    borderRadius: 4,
    marginBottom: 4,
  },
  lovHeaderText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3B2D17',
  },
  lovItem: {
    flexDirection: 'row',
    paddingVertical: 8,
    paddingHorizontal: 8,
    borderBottomWidth: 1,
    borderBottomColor: '#F0E6D2',
    backgroundColor: '#FFFFFF',
  },
  lovItemText: {
    fontSize: 12,
    color: '#332717',
  },
  modalFooter: {
    flexDirection: 'row',
    justifyContent: 'flex-end',
    paddingHorizontal: 12,
    paddingVertical: 8,
    borderTopWidth: 1,
    borderTopColor: '#F0E6D2',
    backgroundColor: '#FCFAF6',
  },
  modalCloseBtn: {
    paddingVertical: 5,
    paddingHorizontal: 14,
    borderRadius: 5,
    borderWidth: 1,
    borderColor: '#DFD5C3',
    backgroundColor: '#FFFFFF',
  },
  modalCloseBtnText: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#5C4A28',
  },
  modalActionBtn: {
    flex: 1,
    paddingVertical: 8,
    borderRadius: 6,
    alignItems: 'center',
    justifyContent: 'center',
  },
  modalBtnCancel: {
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#DFD5C3',
  },
  modalBtnCancelText: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#7D6F5B',
  },
  modalBtnPrimary: {
    backgroundColor: '#B48218',
  },
  modalBtnPrimaryText: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#FFFFFF',
  },
  // Toast
  toast: {
    position: 'absolute',
    top: 40,
    alignSelf: 'center',
    paddingHorizontal: 20,
    paddingVertical: 10,
    borderRadius: 8,
    elevation: 5,
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.25,
    shadowRadius: 3.84,
    zIndex: 9999,
  },
  toastText: {
    fontSize: 13,
    fontWeight: 'bold',
    color: '#FFFFFF',
  },
});
