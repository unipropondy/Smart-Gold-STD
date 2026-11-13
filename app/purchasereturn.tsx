import { ThemedText } from '@/components/themed-text';
import { API_BASE_URL } from '@/constants/api';
import { useThemeContext } from '@/contexts/ThemeContext';
import { useThemeColors } from '@/hooks/useThemeColors';
import { Ionicons } from '@expo/vector-icons';
import AsyncStorage from '@react-native-async-storage/async-storage';
import { Stack, useRouter } from 'expo-router';
import React, { useEffect, useState } from 'react';
import {
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

  // Confirmation Modal State (for duplicate items or print prompt)
  const [confirmDialog, setConfirmDialog] = useState<{
    visible: boolean;
    title?: string;
    message: string;
    yesText?: string;
    noText?: string;
    onConfirm: () => void;
  } | null>(null);

  // Past Purchase Return LOV Modal State
  const [pastReturnModalVisible, setPastReturnModalVisible] = useState(false);
  const [pastReturnList, setPastReturnList] = useState<any[]>([]);
  const [pastReturnSearch, setPastReturnSearch] = useState('');
  const [pastReturnLoading, setPastReturnLoading] = useState(false);

  // Fetch past purchase returns for selection
  const fetchPastReturns = async () => {
    try {
      setPastReturnLoading(true);
      const res = await fetch(`${API_URL}/purchasereturn/past-list`);
      const json = await res.json();
      if (json.success) {
        setPastReturnList(json.data || []);
      } else {
        showToast(json.message || 'Failed to load past purchase returns', 'error');
      }
    } catch (e: any) {
      showToast('Error loading past purchase returns', 'error');
    } finally {
      setPastReturnLoading(false);
    }
  };

  const openPastReturnLookup = () => {
    setPastReturnSearch('');
    setPastReturnModalVisible(true);
    fetchPastReturns();
  };

  // Print Report Handler matching rptPurchaseReturn.rpt layout
  const handlePrintReport = async (overrideRefNo?: string) => {
    const targetRef = (overrideRefNo || refNo || '').trim();
    if (!targetRef) {
      showToast('Please select or save a Purchase Return Ref No first!', 'error');
      return;
    }

    try {
      setLoading(true);
      const res = await fetch(`${API_URL}/purchasereturn/print-data?returnNo=${encodeURIComponent(targetRef)}`);
      const json = await res.json();

      if (!json.success || !json.data) {
        showToast(json.message || 'Purchase Return record not found', 'error');
        setLoading(false);
        return;
      }

      const { company, header, details } = json.data;

      // Format Date DD/MM/YYYY
      let formattedDate = '';
      if (header.returnDate) {
        const dt = new Date(header.returnDate);
        if (!isNaN(dt.getTime())) {
          const dd = String(dt.getDate()).padStart(2, '0');
          const mm = String(dt.getMonth() + 1).padStart(2, '0');
          const yyyy = dt.getFullYear();
          formattedDate = `${dd}/${mm}/${yyyy}`;
        }
      }

      // Calculations and totals
      const totalCostNum = Number(header.totalValue || 0);
      const gstNum = Number(header.totalGst || 0);
      const netAmountNum = Number(header.netAmount || 0);
      const totalWeightNum = Number(header.totalWeight || 0);
      const totalPcsNum = Number(header.totalPCS || 0);

      const itemsRowsHtml = details.map((d: any) => `
        <tr>
          <td style="width: 5%; text-align: right; padding-right: 6px;">${d.sNo}</td>
          <td style="width: 14%; text-align: left; padding-left: 6px;">${d.inventoryCode || ''}</td>
          <td style="width: 35%; text-align: left; padding-left: 6px;">${d.description || ''}</td>
          <td style="width: 10%; text-align: right; padding-right: 6px;">${Number(d.weight || 0).toFixed(2)}</td>
          <td style="width: 10%; text-align: right; padding-right: 6px;">${Number(d.goldLoss || 0).toFixed(2)}</td>
          <td style="width: 10%; text-align: right; padding-right: 6px;">${Number(d.mc || 0).toFixed(2)}</td>
          <td style="width: 6%; text-align: center;">${d.pcs || 1}</td>
          <td style="width: 10%; text-align: right; padding-right: 6px;">${Number(d.amount || 0).toFixed(2)}</td>
        </tr>
      `).join('');

      const printHtml = `
        <!DOCTYPE html>
        <html>
        <head>
          <meta charset="utf-8" />
          <title>Purchase Return - ${header.returnNo}</title>
          <style>
            @page {
              size: A4 portrait;
              margin: 0 !important;
            }
            * {
              box-sizing: border-box;
              -webkit-print-color-adjust: exact !important;
              print-color-adjust: exact !important;
            }
            html, body {
              margin: 0;
              padding: 0;
              background-color: #fbf9f5;
              font-family: Cambria, Georgia, "Times New Roman", serif !important;
              color: #3b2d17;
              font-size: 11px;
            }
            .page-sheet {
              width: 210mm;
              min-height: 297mm;
              margin: 15px auto;
              background: #ffffff;
              padding: 10mm 12mm;
              box-sizing: border-box;
              border: none;
              box-shadow: 0 4px 16px rgba(0,0,0,0.06);
              display: flex;
              flex-direction: column;
              justify-content: space-between;
              font-family: Cambria, Georgia, "Times New Roman", serif !important;
            }
            /* HEADER SECTION (Royal Gold Styling like Master Reports) */
            .header-row {
              display: flex;
              justify-content: space-between;
              align-items: flex-start;
              margin-bottom: 8px;
              position: relative;
            }
            .header-left {
              width: 50%;
            }
            .company-info {
              font-size: 11px;
              line-height: 1.35;
            }
            .company-title {
              font-family: Cambria, Georgia, "Times New Roman", serif !important;
              font-size: 22px;
              font-weight: 700;
              color: #996515;
              letter-spacing: 1.5px;
              text-transform: uppercase;
              margin-bottom: 3px;
              line-height: 1.15;
            }
            .company-address {
              font-size: 11px;
              color: #4a3b22;
              font-weight: 600;
              text-transform: uppercase;
            }
            .attn-box {
              margin-top: 12px;
              font-size: 11px;
              line-height: 1.35;
              background-color: #fffbf2;
              border: 1px solid #d4a742;
              border-radius: 6px;
              padding: 8px 12px;
              width: 95%;
            }
            .attn-label {
              font-style: italic;
              font-weight: 700;
              color: #996515;
              margin-bottom: 2px;
              text-transform: uppercase;
              font-size: 10px;
            }
            .supplier-title {
              font-weight: 700;
              color: #3b2d17;
              font-size: 12px;
              text-transform: uppercase;
            }
            .header-right {
              width: 45%;
              display: flex;
              flex-direction: column;
              align-items: flex-end;
            }
            .report-main-title {
              font-family: Cambria, Georgia, "Times New Roman", serif !important;
              font-size: 22px;
              font-weight: 700;
              color: #3b2d17;
              letter-spacing: 1px;
              text-transform: uppercase;
              text-align: right;
              margin-bottom: 8px;
            }
            .meta-table {
              border-collapse: collapse;
              border: 1px solid #d4a742;
              border-radius: 6px;
              overflow: hidden;
              width: 220px;
            }
            .meta-table td {
              border: 0.75px solid #d4a742;
              padding: 5px 8px;
              font-size: 11px;
            }
            .meta-table td.label-col {
              background-color: #fff8e7 !important;
              font-weight: 700;
              color: #996515;
              width: 44%;
              text-transform: uppercase;
            }
            .meta-table td.value-col {
              text-align: left;
              font-weight: 700;
              color: #3b2d17;
              background-color: #ffffff;
            }

            .header-ornament {
              text-align: center;
              margin: 10px 0 14px 0;
              color: #d4a742;
              font-size: 15px;
              position: relative;
            }
            .header-ornament::before,
            .header-ornament::after {
              content: "";
              position: absolute;
              top: 50%;
              width: calc(50% - 20px);
              height: 1.5px;
              background: #d4a742;
            }
            .header-ornament::before {
              left: 0;
            }
            .header-ornament::after {
              right: 0;
            }

            /* Main Table (Royal Gold Master Reports Theme) */
            .table-wrapper {
              margin-top: 8px;
              flex: 1;
              display: flex;
              flex-direction: column;
            }
            .data-table {
              width: 100% !important;
              border-collapse: collapse;
              font-family: Cambria, Georgia, "Times New Roman", serif !important;
              font-size: 11px;
              border: 0.75px solid #d4a742;
              box-sizing: border-box;
            }
            .data-table thead th {
              background-color: #996515 !important;
              color: #ffffff !important;
              font-weight: 700 !important;
              font-family: Cambria, Georgia, "Times New Roman", serif !important;
              padding: 8px 6px;
              letter-spacing: 0.3px;
              border: 0.75px solid #d4a742;
              font-size: 11px;
              line-height: 1.25;
              text-transform: uppercase !important;
            }
            .data-table tbody tr td {
              padding: 6px 6px;
              color: #3b2d17;
              font-family: Cambria, Georgia, "Times New Roman", serif !important;
              border: 0.75px solid #f0d9a0;
              font-size: 11px;
              line-height: 1.3;
              vertical-align: middle;
            }
            /* Alternating Gold/Cream Rows */
            .data-table tbody tr:nth-child(even) td {
              background-color: #fffbf2 !important;
            }
            .data-table tbody tr:nth-child(odd) td {
              background-color: #ffffff !important;
            }

            /* Footer Section */
            .footer-section {
              margin-top: auto;
              padding-top: 14px;
            }
            .footer-divider {
              width: 100%;
              height: 1.5px;
              background: #d4a742;
              margin-bottom: 12px;
            }
            .footer-row {
              display: flex;
              justify-content: space-between;
              align-items: flex-start;
            }
            .footer-left {
              width: 48%;
              display: flex;
              flex-direction: column;
              justify-content: space-between;
              min-height: 130px;
            }
            .remarks-box {
              background-color: #fffbf2;
              border: 1px solid #d4a742;
              border-radius: 6px;
              padding: 8px 10px;
              font-size: 11px;
            }
            .remarks-label {
              font-weight: 700;
              color: #996515;
              margin-bottom: 3px;
              text-transform: uppercase;
            }
            .remarks-val {
              color: #3b2d17;
              font-weight: 600;
            }
            .signatures {
              display: flex;
              justify-content: space-between;
              margin-top: auto;
              padding-top: 25px;
            }
            .sign-col {
              font-size: 10px;
              font-weight: 700;
              color: #5c4314;
              border-top: 1.5px solid #996515;
              width: 44%;
              padding-top: 5px;
              text-align: center;
              text-transform: uppercase;
            }

            .footer-right {
              width: 48%;
              display: flex;
              flex-direction: column;
              align-items: flex-end;
            }
            .totals-list {
              width: 100%;
              max-width: 240px;
              margin-bottom: 12px;
              background-color: #fffdf8;
              border: 1px solid #d4a742;
              border-radius: 6px;
              padding: 6px 12px;
            }
            .total-line {
              display: flex;
              justify-content: space-between;
              font-size: 11px;
              padding: 3px 0;
              color: #4a3b22;
              font-weight: 600;
            }
            .total-line.bold {
              font-weight: 700;
              font-size: 12px;
              color: #996515;
              border-top: 1px dashed #d4a742;
              margin-top: 3px;
              padding-top: 5px;
            }

            .summary-box-table {
              border-collapse: collapse;
              border: 1px solid #d4a742;
              border-radius: 6px;
              overflow: hidden;
              width: 200px;
            }
            .summary-box-table td {
              border: 0.75px solid #d4a742;
              padding: 5px 8px;
              font-size: 11px;
            }
            .summary-box-table td.header-col {
              background-color: #996515 !important;
              color: #ffffff !important;
              font-weight: 700;
              width: 55%;
              text-transform: uppercase;
            }
            .summary-box-table td.val-col {
              text-align: right;
              font-weight: 700;
              color: #3b2d17;
              background-color: #fff8e7;
            }

            .report-foot-brand {
              text-align: center;
              margin-top: 14px;
              font-size: 10px;
              color: #705832;
              font-weight: 600;
              letter-spacing: 0.5px;
            }

            .print-btn-bar {
              position: fixed;
              top: 15px;
              right: 15px;
              z-index: 9999;
            }
            .print-btn {
              background: #996515;
              color: #fff;
              border: none;
              padding: 9px 20px;
              font-family: Cambria, Georgia, serif;
              font-size: 12px;
              font-weight: 700;
              border-radius: 6px;
              cursor: pointer;
              box-shadow: 0 4px 12px rgba(153, 101, 21, 0.25);
            }
            .print-btn:hover {
              background: #7a5408;
            }
            @media print {
              .print-btn-bar {
                display: none !important;
              }
              html, body {
                background: #fff !important;
              }
              .page-sheet {
                box-shadow: none !important;
                margin: 0 !important;
                width: 100% !important;
                min-height: 297mm;
                padding: 10mm 12mm !important;
              }
            }
          </style>
        </head>
        <body>
          <div class="print-btn-bar">
            <button class="print-btn" onclick="window.print()">Print Report</button>
          </div>

          <div class="page-sheet">
            <div>
              <!-- TOP SECTION -->
              <div class="header-row">
                <div class="header-left">
                  <div class="company-info">
                    <div class="company-title">${company.name || 'SMART'}</div>
                    <div class="company-address">${company.address1 || 'SINGAPORE'}</div>
                    ${company.address2 ? `<div class="company-address">${company.address2}</div>` : ''}
                  </div>

                  <div class="attn-box">
                    <div class="attn-label">Attn:</div>
                    <div class="supplier-title">${header.supplierName || ''}</div>
                    ${header.address1 ? `<div>${header.address1}</div>` : ''}
                    ${header.address2 ? `<div>${header.address2}</div>` : ''}
                  </div>
                </div>

                <div class="header-right">
                  <div class="report-main-title">Purchase Return</div>
                  <table class="meta-table">
                    <tr>
                      <td class="label-col">Date</td>
                      <td class="value-col">${formattedDate}</td>
                    </tr>
                    <tr>
                      <td class="label-col">Return No</td>
                      <td class="value-col">${header.returnNo || ''}</td>
                    </tr>
                    <tr>
                      <td class="label-col">Page</td>
                      <td class="value-col">Page 1 of 1</td>
                    </tr>
                  </table>
                </div>
              </div>

              <div class="header-ornament">◆</div>

              <!-- ITEMS TABLE -->
              <div class="table-wrapper">
                <table class="data-table">
                  <thead>
                    <tr>
                      <th style="width: 5%; text-align: center;">SNO.</th>
                      <th style="width: 16%; text-align: left; padding-left: 8px;">INVENTORY CODE</th>
                      <th style="width: 32%; text-align: left; padding-left: 8px;">DESCRIPTION</th>
                      <th style="width: 10%; text-align: right; padding-right: 8px;">WEIGHT</th>
                      <th style="width: 10%; text-align: right; padding-right: 8px;">GOLD LOSS</th>
                      <th style="width: 10%; text-align: right; padding-right: 8px;">MC</th>
                      <th style="width: 6%; text-align: center;">PCS</th>
                      <th style="width: 11%; text-align: right; padding-right: 8px;">AMOUNT</th>
                    </tr>
                  </thead>
                  <tbody>
                    ${itemsRowsHtml}
                  </tbody>
                </table>
              </div>
            </div>

            <!-- FOOTER TOTALS & SIGNATURES -->
            <div class="footer-section">
              <div class="footer-divider"></div>
              <div class="footer-row">
                <div class="footer-left">
                  <div class="remarks-box">
                    <div class="remarks-label">For</div>
                    <div class="remarks-val">${header.remarks || 'N/A'}</div>
                  </div>

                  <div class="signatures">
                    <div class="sign-col">Authorised Signatory</div>
                    <div class="sign-col">Received By</div>
                  </div>
                </div>

                <div class="footer-right">
                  <div class="totals-list">
                    <div class="total-line">
                      <span>TOTAL</span>
                      <span>$ ${totalCostNum.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</span>
                    </div>
                    <div class="total-line">
                      <span>GST</span>
                      <span>$ ${gstNum.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</span>
                    </div>
                    <div class="total-line bold">
                      <span>NET AMOUNT</span>
                      <span>$ ${netAmountNum.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</span>
                    </div>
                  </div>

                  <table class="summary-box-table">
                    <tr>
                      <td class="header-col">Total Items</td>
                      <td class="val-col">${details.length}</td>
                    </tr>
                    <tr>
                      <td class="header-col">Total Weight</td>
                      <td class="val-col">${totalWeightNum.toFixed(2)}</td>
                    </tr>
                    <tr>
                      <td class="header-col">Total PCS</td>
                      <td class="val-col">${totalPcsNum}</td>
                    </tr>
                  </table>
                </div>
              </div>
              <div class="report-foot-brand">*** System Generated Report &bull; Powered by Unipro SG ***</div>
            </div>
          </div>

          <script>
            window.onload = function() {
              setTimeout(function() {
                window.print();
              }, 300);
            };
          </script>
        </body>
        </html>
      `;

      if (Platform.OS === 'web' && typeof window !== 'undefined') {
        const printWin = window.open('', '_blank', 'width=900,height=750');
        if (printWin) {
          printWin.document.open();
          printWin.document.write(printHtml);
          printWin.document.close();
        } else {
          const blob = new Blob([printHtml], { type: 'text/html;charset=utf-8;' });
          const url = URL.createObjectURL(blob);
          const fallbackWin = window.open(url, '_blank');
          if (!fallbackWin) {
            showToast('Popup blocker prevented opening print window. Please allow popups.', 'error');
          }
        }
      } else {
        showToast(`Print report prepared for ${targetRef}`, 'success');
      }
    } catch (err: any) {
      console.error('Print Error:', err);
      showToast('Error generating print report: ' + (err.message || 'Server error'), 'error');
    } finally {
      setLoading(false);
    }
  };
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
        const savedNo = json.returnNo;
        setRefNo(savedNo);
        showToast(json.message || `Please take Note Reference No : ${savedNo}`, 'success');
        setDetails([]);
        setSelectedIndex(null);

        // In-app prompt to print report immediately matching VB.NET frmPurchaseReturn
        setConfirmDialog({
          visible: true,
          title: 'Print Purchase Return',
          message: 'Would you like to print the Purchase Return report now?',
          yesText: 'Print',
          noText: 'Cancel',
          onConfirm: () => {
            setConfirmDialog(null);
            handlePrintReport(savedNo);
          }
        });
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
            style={[styles.printBtn, !refNo && { opacity: 0.8 }]}
            onPress={() => {
              if (refNo) {
                handlePrintReport(refNo);
              } else {
                openPastReturnLookup();
              }
            }}
          >
            <ThemedText style={styles.printBtnText}>Print</ThemedText>
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
              <View style={{ width: 140, flexDirection: 'row' }}>
                <TextInput
                  style={[styles.input, styles.readonlyInput, { width: 108, borderTopRightRadius: 0, borderBottomRightRadius: 0, borderRightWidth: 0 }]}
                  value={refNo}
                  editable={false}
                  placeholder="Auto"
                />
                <TouchableOpacity style={styles.lookupBtn} onPress={openPastReturnLookup}>
                  <ThemedText style={styles.lookupBtnText}>...</ThemedText>
                </TouchableOpacity>
              </View>

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
                <ThemedText style={{ fontSize: 16, fontWeight: 'bold', color: '#3B2D17' }}>
                  {confirmDialog.title || 'Confirmation'}
                </ThemedText>
              </View>
              <ThemedText style={{ fontSize: 13, color: '#4A3B22', lineHeight: 20, marginBottom: 20 }}>
                {confirmDialog.message}
              </ThemedText>
              <View style={{ flexDirection: 'row', justifyContent: 'flex-end', gap: 10 }}>
                <TouchableOpacity
                  style={[styles.modalCloseBtn, { paddingHorizontal: 16 }]}
                  onPress={() => setConfirmDialog(null)}
                >
                  <ThemedText style={styles.modalCloseBtnText}>{confirmDialog.noText || 'No'}</ThemedText>
                </TouchableOpacity>
                <TouchableOpacity
                  style={[styles.saveBtn, { paddingHorizontal: 16 }]}
                  onPress={confirmDialog.onConfirm}
                >
                  <ThemedText style={styles.saveBtnText}>{confirmDialog.yesText || 'Yes'}</ThemedText>
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

      {/* Past Purchase Returns LOV Modal */}
      <Modal visible={pastReturnModalVisible} transparent={true} animationType="fade">
        <TouchableWithoutFeedback onPress={() => setPastReturnModalVisible(false)}>
          <View style={styles.modalOverlay}>
            <TouchableWithoutFeedback>
              <View style={[styles.modalContainer, { maxWidth: 750 }]}>
                <View style={styles.modalHeader}>
                  <ThemedText style={styles.modalTitle}>Select Purchase Return</ThemedText>
                  <TouchableOpacity onPress={() => setPastReturnModalVisible(false)} style={styles.modalCloseIconBtn}>
                    <Ionicons name="close" size={20} color="#3B2D17" />
                  </TouchableOpacity>
                </View>

                <View style={{ padding: 12 }}>
                  <TextInput
                    style={styles.modalSearchInput}
                    placeholder="Search by Return No or Supplier..."
                    placeholderTextColor="#7D6F5B"
                    value={pastReturnSearch}
                    onChangeText={setPastReturnSearch}
                    autoFocus
                  />

                  <View style={styles.lovHeaderRow}>
                    <ThemedText style={[styles.lovHeaderText, { width: 120 }]}>Return No</ThemedText>
                    <ThemedText style={[styles.lovHeaderText, { width: 95 }]}>Date</ThemedText>
                    <ThemedText style={[styles.lovHeaderText, { flex: 2 }]}>Supplier</ThemedText>
                    <ThemedText style={[styles.lovHeaderText, { width: 75, textAlign: 'right' }]}>Weight</ThemedText>
                    <ThemedText style={[styles.lovHeaderText, { width: 50, textAlign: 'center' }]}>PCS</ThemedText>
                    <ThemedText style={[styles.lovHeaderText, { width: 85, textAlign: 'right' }]}>Net Amt</ThemedText>
                  </View>

                  {pastReturnLoading ? (
                    <View style={{ padding: 24, alignItems: 'center' }}>
                      <ThemedText style={{ color: '#7D6F5B', fontSize: 13 }}>Loading Purchase Returns...</ThemedText>
                    </View>
                  ) : (
                    <FlatList
                      data={pastReturnList.filter((item: any) => {
                        if (!pastReturnSearch.trim()) return true;
                        const s = pastReturnSearch.toLowerCase();
                        return (
                          (item.ReturnNo || '').toLowerCase().includes(s) ||
                          (item.SupplierName || '').toLowerCase().includes(s) ||
                          (item.SupplierCode || '').toLowerCase().includes(s)
                        );
                      })}
                      keyExtractor={(item, index) => index.toString()}
                      style={{ maxHeight: 320 }}
                      renderItem={({ item, index }) => {
                        let dStr = item.ReturnDate;
                        if (dStr) {
                          const dt = new Date(dStr);
                          if (!isNaN(dt.getTime())) {
                            dStr = `${String(dt.getDate()).padStart(2, '0')}/${String(dt.getMonth() + 1).padStart(2, '0')}/${dt.getFullYear()}`;
                          }
                        }
                        return (
                          <TouchableOpacity
                            activeOpacity={0.7}
                            style={[
                              styles.lovItem,
                              index % 2 === 1 && { backgroundColor: '#FCFAF6' },
                            ]}
                            onPress={() => {
                              setRefNo(item.ReturnNo);
                              setPastReturnModalVisible(false);
                              handlePrintReport(item.ReturnNo);
                            }}
                          >
                            <ThemedText style={[styles.lovItemText, { width: 120, fontWeight: 'bold' }]}>
                              {item.ReturnNo}
                            </ThemedText>
                            <ThemedText style={[styles.lovItemText, { width: 95 }]}>
                              {dStr || '-'}
                            </ThemedText>
                            <ThemedText style={[styles.lovItemText, { flex: 2 }]} numberOfLines={1}>
                              {item.SupplierName || item.SupplierCode || '-'}
                            </ThemedText>
                            <ThemedText style={[styles.lovItemText, { width: 75, textAlign: 'right' }]}>
                              {Number(item.TotalWeight || 0).toFixed(2)}
                            </ThemedText>
                            <ThemedText style={[styles.lovItemText, { width: 50, textAlign: 'center' }]}>
                              {item.TotalPCS || 0}
                            </ThemedText>
                            <ThemedText style={[styles.lovItemText, { width: 85, textAlign: 'right', fontWeight: 'bold' }]}>
                              {Number(item.NetAmount || item.TotalValue || 0).toFixed(2)}
                            </ThemedText>
                          </TouchableOpacity>
                        );
                      }}
                      ListEmptyComponent={
                        <View style={{ padding: 20, alignItems: 'center' }}>
                          <ThemedText style={{ color: '#7D6F5B', fontSize: 13, fontStyle: 'italic' }}>
                            No purchase return records found
                          </ThemedText>
                        </View>
                      }
                    />
                  )}
                </View>

                <View style={styles.modalFooter}>
                  <TouchableOpacity style={styles.modalCloseBtn} onPress={() => setPastReturnModalVisible(false)}>
                    <ThemedText style={styles.modalCloseBtnText}>Close</ThemedText>
                  </TouchableOpacity>
                </View>
              </View>
            </TouchableWithoutFeedback>
          </View>
        </TouchableWithoutFeedback>
      </Modal>

      {/* Toast Notification */}
      {toast && (
        <View style={[
          styles.toast,
          { backgroundColor: toast.type === 'success' ? '#2E7D32' : '#C62828' }
        ]}>
          <Ionicons
            name={toast.type === 'success' ? 'checkmark-circle' : 'alert-circle'}
            size={18}
            color="#FFFFFF"
            style={{ marginRight: 8 }}
          />
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
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#DFD5C3',
    paddingVertical: 6,
    paddingHorizontal: 12,
    borderRadius: 5,
  },
  clearBtnText: {
    color: '#5C4A28',
    fontWeight: 'bold',
    fontSize: 12,
  },
  saveBtn: {
    backgroundColor: '#F9F2E2',
    borderWidth: 1,
    borderColor: '#B8903B',
    paddingVertical: 6,
    paddingHorizontal: 16,
    borderRadius: 5,
  },
  saveBtnText: {
    color: '#9E6D08',
    fontWeight: 'bold',
    fontSize: 12,
  },
  printBtn: {
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#DFD5C3',
    paddingVertical: 6,
    paddingHorizontal: 12,
    borderRadius: 5,
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
  // Toast
  toast: {
    position: 'absolute',
    top: 40,
    alignSelf: 'center',
    flexDirection: 'row',
    alignItems: 'center',
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
