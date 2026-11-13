import { ThemedText } from '@/components/themed-text';
import { API_BASE_URL } from '@/constants/api';
import { useThemeColors } from '@/hooks/useThemeColors';
import { UNIPRO_LOGO_DATA_URI } from '@/utils/uniproLogo';
import { Ionicons } from '@expo/vector-icons';
import { Stack, useRouter } from 'expo-router';
import React, { useEffect, useState } from 'react';
import {
  ActivityIndicator,
  FlatList,
  KeyboardAvoidingView,
  Modal,
  Platform,
  ScrollView,
  StyleSheet,
  TextInput,
  TouchableOpacity,
  View,
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';

const API_URL = `${API_BASE_URL}/tradeinissue`;

export default function TradeInIssueScreen() {
  const themeColors = useThemeColors();
  const router = useRouter();

  // State
  const [loading, setLoading] = useState(false);
  const [saving, setSaving] = useState(false);
  const [toast, setToast] = useState<{ message: string; type: 'success' | 'error' } | null>(null);

  const [issueNo, setIssueNo] = useState('');
  const [issueDate, setIssueDate] = useState(() => {
    const d = new Date();
    return `${d.getDate().toString().padStart(2, '0')}-${(d.getMonth() + 1).toString().padStart(2, '0')}-${d.getFullYear()}`;
  });

  const [supplierCode, setSupplierCode] = useState('');
  const [supplierName, setSupplierName] = useState('');
  const [isProduction, setIsProduction] = useState(false);
  const [isExport, setIsExport] = useState(false);

  const [totalIssuedWt, setTotalIssuedWt] = useState('0.00');
  const [totalPrice, setTotalPrice] = useState('0.00');
  const [costPerGram, setCostPerGram] = useState('0.00');

  const [totalWt, setTotalWt] = useState('0.00');
  const [purity, setPurity] = useState('');
  const [remarks, setRemarks] = useState('');

  const [tradeBillNo, setTradeBillNo] = useState('');
  const [gridData, setGridData] = useState<any[]>([]);

  // LOV State
  const [lovVisible, setLovVisible] = useState(false);
  const [lovData, setLovData] = useState<any[]>([]);
  const [lovType, setLovType] = useState<'supplier' | 'tradebill' | 'history'>('supplier');
  const [lovSearchQuery, setLovSearchQuery] = useState('');

  const showToast = (message: string, type: 'success' | 'error' = 'success') => {
    setToast({ message, type });
    setTimeout(() => setToast(null), 3000);
  };

  const fetchNextIssueNo = async () => {
    try {
      setLoading(true);
      const res = await fetch(`${API_URL}/nextIssueNo`);
      const data = await res.json();
      if (data.success) {
        setIssueNo(data.data);
        showToast(`Next Issue No: ${data.data}`, 'success');
      }
    } catch (err: any) {
      showToast('Error generating next issue number', 'error');
    } finally {
      setLoading(false);
    }
  };

  const fetchSuppliers = async () => {
    try {
      setLoading(true);
      const res = await fetch(`${API_URL}/suppliers`);
      const data = await res.json();
      if (data.success) {
        setLovData(data.data || []);
        setLovType('supplier');
        setLovSearchQuery('');
        setLovVisible(true);
      } else {
        showToast('Error fetching suppliers', 'error');
      }
    } catch (err: any) {
      showToast('Network error loading suppliers', 'error');
    } finally {
      setLoading(false);
    }
  };

  const fetchTradeBills = async () => {
    try {
      setLoading(true);
      const res = await fetch(`${API_URL}/tradebills`);
      const data = await res.json();
      if (data.success) {
        setLovData(data.data || []);
        setLovType('tradebill');
        setLovSearchQuery('');
        setLovVisible(true);
      } else {
        showToast('Error fetching trade bills', 'error');
      }
    } catch (err: any) {
      showToast('Network error loading trade bills', 'error');
    } finally {
      setLoading(false);
    }
  };

  const fetchHistory = async () => {
    try {
      setLoading(true);
      const res = await fetch(`${API_URL}/history`);
      const data = await res.json();
      if (data.success) {
        setLovData(data.data || []);
        setLovType('history');
        setLovSearchQuery('');
        setLovVisible(true);
      } else {
        showToast('Error fetching history', 'error');
      }
    } catch (err: any) {
      showToast('Network error loading history', 'error');
    } finally {
      setLoading(false);
    }
  };

  const handleSelectLovItem = (item: any) => {
    setLovVisible(false);
    if (lovType === 'supplier') {
      setSupplierCode(item.SupplierCode || '');
      setSupplierName(item.SupplierName || '');
    } else if (lovType === 'tradebill') {
      if (gridData.some((r) => r.InvoiceNo === item.InvoiceNo)) {
        showToast('Trade bill already added to list', 'error');
        return;
      }
      const newBill = {
        InvoiceNo: item.InvoiceNo,
        Date: new Date(item.ModifyDate || new Date()).toLocaleDateString(),
        InventoryCode: item.InventoryCode || '-',
        PurchaseWeight: item.TotalWeight || 0,
        IssuedWeight: item.IssuedWeight || 0,
        BalanceWeight: item.Balanceweight || 0,
      };
      const newGrid = [...gridData, newBill];
      setGridData(newGrid);

      const addedWt = parseFloat(newBill.PurchaseWeight || newBill.BalanceWeight || 0);
      const newIssued = (parseFloat(totalIssuedWt) + addedWt).toFixed(2);
      setTotalIssuedWt(newIssued);
      recalculateTotal(newIssued, costPerGram);
      setTradeBillNo('');
      showToast(`Added bill ${item.InvoiceNo}`, 'success');
    } else if (lovType === 'history') {
      setIssueNo(item.TradeIssueNo || '');
      setSupplierCode(item.SupplierCode || '');
      setSupplierName(item.SupplierName || '');
    }
  };

  const handleDeleteGridItem = (index: number) => {
    const itemToDelete = gridData[index];
    const newGrid = gridData.filter((_, i) => i !== index);
    setGridData(newGrid);

    const deductedWt = parseFloat(itemToDelete.PurchaseWeight || itemToDelete.BalanceWeight || 0);
    const newIssued = Math.max(0, parseFloat(totalIssuedWt) - deductedWt).toFixed(2);
    setTotalIssuedWt(newIssued);
    recalculateTotal(newIssued, costPerGram);
  };

  const recalculateTotal = (issued: string, cost: string) => {
    const valIssued = parseFloat(issued) || 0;
    const valCost = parseFloat(cost) || 0;
    const calc = valIssued * valCost;
    setTotalPrice(calc.toFixed(2));

    const netIssue = (valIssued * 0.895) / 0.92;
    setTotalWt(netIssue.toFixed(2));
  };

  const handleCostChange = (text: string) => {
    setCostPerGram(text);
    recalculateTotal(totalIssuedWt, text);
  };

  const handleIssuedWtChange = (text: string) => {
    setTotalIssuedWt(text);
    recalculateTotal(text, costPerGram);
  };

  const handleClear = () => {
    setSupplierCode('');
    setSupplierName('');
    setTotalIssuedWt('0.00');
    setTotalPrice('0.00');
    setCostPerGram('0.00');
    setTotalWt('0.00');
    setRemarks('');
    setPurity('');
    setIsProduction(false);
    setIsExport(false);
    setGridData([]);
    setTradeBillNo('');
    fetchNextIssueNo();
  };

  const handleProceed = async () => {
    if (!supplierCode.trim()) {
      showToast('Supplier cannot be blank!', 'error');
      return;
    }
    if (parseFloat(totalIssuedWt) <= 0) {
      showToast('Gold Issued Actual Weight Should Not Be 0 !!!', 'error');
      return;
    }
    if (gridData.length === 0) {
      showToast('Please add at least one Trade Bill in the grid!', 'error');
      return;
    }

    try {
      setSaving(true);
      const payload = {
        tradeIssueNo: issueNo,
        issueDate: issueDate,
        supplierCode,
        noOfBills: gridData.length,
        totalIssuedWt,
        costPerGram,
        totalPrice,
        totalWt,
        purity,
        remarks,
        isProduction,
        isExport,
        userId: 'admin',
        tradeBills: gridData,
      };

      const res = await fetch(`${API_URL}/proceed`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload),
      });
      const data = await res.json();

      if (data.success) {
        showToast('Tradein Gold Issued Sucessfully', 'success');
        const assignedNo = data.tradeIssueNo || issueNo;
        // Automatically prompt to print matching VB.NET subPrint after issue proceed
        if (assignedNo && assignedNo.trim()) {
          setTimeout(() => {
            subPrint(assignedNo.trim());
          }, 400);
        }
        handleClear();
      } else {
        showToast(data.message || 'Error in save', 'error');
      }
    } catch (err: any) {
      showToast('Network error while issuing trade-in gold', 'error');
    } finally {
      setSaving(false);
    }
  };

  // Original subPrint() implementation (matching VB.NET subPrint in frmTradeInIssue)
  // Selection: {tblTradeIssueHistory.TradeIssueNo} = sTradeInIssueNo
  // Report Title: "Trade In Issue Report For " & sTradeInIssueNo
  const subPrint = async (targetIssueNo: string) => {
    const sTradeInIssueNo = (targetIssueNo || '').trim();
    if (!sTradeInIssueNo) return;

    try {
      setLoading(true);

      let company: any = { name: '', address1: '', address2: '', country: '', phone: '', fax: '' };
      let header: any = null;
      let historyRows: any[] = [];
      let tradeBills: any[] = [];

      // 1. Fetch print data from backend SQL Server
      const res = await fetch(`${API_URL}/print-data?issueNo=${encodeURIComponent(sTradeInIssueNo)}`);
      const json = await res.json();

      if (json.success && json.data) {
        company = json.data.company || company;
        header = json.data.header || {};
        historyRows = json.data.history || [];
        tradeBills = json.data.tradeBills || [];
      } else {
        showToast(json.message || 'Trade In Issue record not found for print.', 'error');
        setLoading(false);
        return;
      }

      // Singapore Print Date & Time (Asia/Singapore)
      const now = new Date();
      const sgDateStr = now.toLocaleDateString('en-GB', {
        timeZone: 'Asia/Singapore',
        day: '2-digit',
        month: '2-digit',
        year: 'numeric',
      });
      const sgTimeStr = now.toLocaleTimeString('en-US', {
        timeZone: 'Asia/Singapore',
        hour: '2-digit',
        minute: '2-digit',
        hour12: true,
      });
      const printTimestamp = `Printed: ${sgDateStr}, ${sgTimeStr}`;

      // Issue Date
      let issueDateFormatted = '';
      const rawDate = header.TradeIssueDate || (historyRows[0] && historyRows[0].CreateDate);
      if (rawDate) {
        const vd = new Date(rawDate);
        if (!isNaN(vd.getTime())) {
          issueDateFormatted = `${String(vd.getDate()).padStart(2, '0')}/${String(vd.getMonth() + 1).padStart(2, '0')}/${vd.getFullYear()}`;
        }
      }
      if (!issueDateFormatted) {
        issueDateFormatted = sgDateStr;
      }

      // Detail rows
      const itemsToRender = historyRows.length > 0 ? historyRows : tradeBills;
      let totalWt = 0;
      let totalAmount = 0;

      const rowsHtml = itemsToRender.map((row: any, idx: number) => {
        const invNo = String(row.InvoiceNo || row.InventoryCode || `ITEM-${idx + 1}`).trim();
        const cust = String(row.CustomerName || '').trim();
        const remarks = String(row.Remarks || '').trim();
        const wt = parseFloat(row.TotalWeight || row.IssuedWeight || 0);
        const rate = parseFloat(row.CostPerGram || header.CostPerGram || 0);
        const amt = parseFloat(row.TotalCost || row.Amount || (wt * rate) || 0);

        totalWt += wt;
        totalAmount += amt;

        return `
          <tr>
            <td style="text-align: center; font-weight: 700;">${idx + 1}</td>
            <td style="text-align: center; font-weight: 600;">${invNo || '-'}</td>
            <td style="text-align: left; font-weight: 500;">${cust || 'REFINERY / WHOLESALE'}</td>
            <td style="text-align: right;">${wt.toFixed(3)}</td>
            <td style="text-align: right;">${rate > 0 ? `$ ${rate.toFixed(2)}` : '-'}</td>
            <td style="text-align: right; font-weight: 700;">$ ${amt.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
            <td style="text-align: left; font-size: 10px;">${remarks || '-'}</td>
          </tr>
        `;
      }).join('');

      const sCompanyname = (company.name || 'SMART GOLD').trim();
      const sAddress = [company.address1, company.address2, company.country].filter(Boolean).join(' ');
      const sPhone = (company.phone || '').trim();
      const sFax = (company.fax || '').trim();
      const sReportTitle = `Trade In Issue Report For ${sTradeInIssueNo}`;

      const printHtml = `
        <!DOCTYPE html>
        <html>
          <head>
            <meta charset="utf-8" />
            <title>${sReportTitle}</title>
            <style>
              @page {
                size: A4 portrait;
                margin: 0 !important;
              }
              html, body {
                margin: 0;
                padding: 0;
                background-color: #f7f5f0;
                font-family: Cambria, Georgia, "Times New Roman", serif !important;
                color: #3b2d17;
                -webkit-print-color-adjust: exact !important;
                print-color-adjust: exact !important;
              }
              .page-sheet {
                width: 210mm;
                min-height: 297mm;
                margin: 15px auto;
                background: #ffffff;
                padding: 10mm 12mm;
                box-sizing: border-box;
                display: flex;
                flex-direction: column;
                font-family: Cambria, Georgia, "Times New Roman", serif !important;
                box-shadow: 0 4px 14px rgba(0,0,0,0.08);
              }

              /* HEADER SECTION */
              .header-row {
                display: flex;
                justify-content: space-between;
                align-items: center;
                margin-bottom: 6px;
                position: relative;
              }
              .header-left {
                width: 140px;
                display: flex;
                align-items: center;
              }
              .header-left img.unipro-logo-img {
                max-height: 48px;
                max-width: 135px;
                object-fit: contain;
                display: block;
              }
              .header-center {
                flex: 1;
                text-align: center;
                padding: 0 10px;
              }
              .company-title {
                font-family: Cambria, Georgia, "Times New Roman", serif !important;
                font-size: 26px;
                font-weight: 700;
                color: #996515;
                letter-spacing: 2px;
                text-transform: uppercase;
                margin-bottom: 3px;
                line-height: 1.15;
              }
              .company-address {
                font-family: Cambria, Georgia, "Times New Roman", serif !important;
                font-size: 11px;
                color: #4a3b22;
                font-weight: 700;
                letter-spacing: 0.5px;
                text-transform: uppercase;
              }
              .header-right {
                width: 140px;
                text-align: right;
                font-size: 11px;
                color: #705832;
                font-weight: 600;
              }
              .header-right table {
                float: right;
                font-size: 10.5px;
                border-collapse: collapse;
              }
              .header-right td {
                padding: 1px 3px;
              }
              .header-ornament {
                text-align: center;
                margin: 6px 0 10px 0;
                color: #d4a742;
                font-size: 14px;
                position: relative;
              }
              .header-ornament::before,
              .header-ornament::after {
                content: "";
                position: absolute;
                top: 50%;
                width: calc(50% - 18px);
                height: 1.5px;
                background: #d4a742;
              }
              .header-ornament::before { left: 0; }
              .header-ornament::after { right: 0; }

              /* REPORT TITLE AREA */
              .report-title-area {
                text-align: center;
                margin-bottom: 12px;
              }
              .report-main-title {
                font-family: Cambria, Georgia, "Times New Roman", serif !important;
                font-size: 20px;
                font-weight: 700;
                color: #3b2d17;
                letter-spacing: 1px;
                text-transform: uppercase;
                margin-bottom: 4px;
              }
              .report-sub-info {
                font-family: Cambria, Georgia, "Times New Roman", serif !important;
                font-size: 11.5px;
                color: #705832;
                font-weight: 600;
                margin-bottom: 2px;
              }
              .report-period-line {
                font-family: Cambria, Georgia, "Times New Roman", serif !important;
                font-size: 11.5px;
                color: #705832;
                font-weight: 600;
                margin-top: 3px;
              }

              /* META CARD */
              .meta-card-container {
                display: flex;
                flex-direction: column;
                gap: 8px;
                margin-bottom: 14px;
              }
              .meta-info-card {
                display: flex;
                justify-content: space-between;
                background: #fffbf2;
                border: 1px solid #d4a742;
                border-radius: 6px;
                padding: 8px 14px;
                font-size: 11.5px;
              }
              .meta-item {
                display: flex;
                flex-direction: column;
              }
              .meta-label {
                font-size: 10px;
                font-weight: 700;
                color: #705832;
                text-transform: uppercase;
                letter-spacing: 0.5px;
                margin-bottom: 2px;
              }
              .meta-val {
                font-size: 12px;
                font-weight: 700;
                color: #996515;
              }

              /* DETAIL TABLE */
              .table-container {
                width: 100%;
                overflow: hidden;
                margin-bottom: 10px;
              }
              table.report-table {
                width: 100% !important;
                border-collapse: collapse;
                font-family: Cambria, Georgia, "Times New Roman", serif !important;
                font-size: 11px;
                border: 0.75px solid #d4a742;
              }
              table.report-table thead th {
                background-color: #996515 !important;
                color: #ffffff !important;
                font-weight: 700 !important;
                font-family: Cambria, Georgia, "Times New Roman", serif !important;
                padding: 7px 6px;
                letter-spacing: 0.4px;
                border: 0.75px solid #d4a742;
                font-size: 11px;
                line-height: 1.2;
                text-align: center !important;
                text-transform: uppercase !important;
              }
              table.report-table tbody td {
                padding: 6px 6px;
                color: #3b2d17;
                font-family: Cambria, Georgia, "Times New Roman", serif !important;
                border: 0.75px solid #f0d9a0;
                font-size: 11px;
                line-height: 1.25;
              }
              table.report-table tbody tr:nth-child(even) td {
                background-color: #fffbf2 !important;
              }
              table.report-table tbody tr:nth-child(odd) td {
                background-color: #ffffff !important;
              }
              table.report-table tr.grand-total-row td {
                background-color: #fff8e7 !important;
                font-weight: 700 !important;
                font-family: Cambria, Georgia, "Times New Roman", serif !important;
                color: #996515 !important;
                border: 0.75px solid #d4a742 !important;
                border-top: 1.5px solid #d4a742 !important;
                border-bottom: 1.5px solid #d4a742 !important;
                padding: 7px 6px;
                font-size: 11px;
              }

              /* SIGNATURE SECTION */
              .signature-section {
                margin-top: 30px;
                width: 100%;
                display: flex;
                justify-content: space-between;
                font-size: 11px;
                page-break-inside: avoid;
                break-inside: avoid;
              }
              .sign-card {
                width: 48%;
                border: 1px solid #d4a742;
                border-radius: 6px;
                background-color: #ffffff;
                padding: 10px 14px;
                box-sizing: border-box;
              }
              .sign-header {
                font-size: 11.5px;
                font-weight: 700;
                color: #996515;
                text-transform: uppercase;
                margin-bottom: 10px;
                border-bottom: 1px dashed #d4a742;
                padding-bottom: 4px;
              }
              .sign-row {
                margin-bottom: 8px;
                color: #4a3b22;
                font-size: 11px;
                white-space: nowrap;
              }

              /* FOOTER */
              .report-footer {
                margin-top: auto !important;
                text-align: center;
                padding-top: 14px;
                padding-bottom: 2mm;
                page-break-inside: avoid;
                break-inside: avoid;
              }
              .footer-divider {
                width: 100%;
                height: 1.5px;
                background: #d4a742;
                margin-bottom: 10px;
              }
              .footer-gen-text {
                font-size: 11px;
                color: #705832;
                font-weight: 600;
                margin-bottom: 3px;
              }
              .footer-powered {
                font-size: 12px;
                color: #996515;
                font-weight: 700;
              }

              /* TOOLBAR */
              .print-toolbar {
                position: fixed;
                top: 16px;
                right: 20px;
                z-index: 1000;
              }
              .btn-action {
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
              .btn-action:hover {
                background: #7a5408;
              }

              @media print {
                @page {
                  margin: 0 !important;
                  size: A4 portrait;
                }
                html, body {
                  background: #fff !important;
                  padding: 0 !important;
                  margin: 0 !important;
                  height: 100% !important;
                }
                .print-toolbar {
                  display: none !important;
                }
                .page-sheet {
                  width: 100% !important;
                  min-height: 297mm !important;
                  height: 100% !important;
                  margin: 0 !important;
                  padding: 10mm 12mm 10mm 12mm !important;
                  box-shadow: none !important;
                  box-sizing: border-box !important;
                  display: flex !important;
                  flex-direction: column !important;
                }
                table.report-table {
                  page-break-inside: auto;
                }
                tr {
                  page-break-inside: avoid;
                  break-inside: avoid;
                }
                thead {
                  display: table-header-group;
                }
                tfoot {
                  display: table-footer-group;
                }
                .signature-section {
                  page-break-inside: avoid;
                  break-inside: avoid;
                }
                .report-footer {
                  margin-top: auto !important;
                  page-break-inside: avoid !important;
                  break-inside: avoid !important;
                  padding-bottom: 4mm !important;
                }
              }
            </style>
          </head>
          <body>
            <div class="print-toolbar">
              <button class="btn-action" onclick="window.print()">Print Report</button>
            </div>

            <div class="page-sheet">
              <!-- HEADER -->
              <div class="header-row">
                <div class="header-left">
                  <img src="${UNIPRO_LOGO_DATA_URI}" alt="UNIPRO" class="unipro-logo-img" />
                </div>

                <div class="header-center">
                  <div class="company-title">${sCompanyname}</div>
                  ${sAddress ? `<div class="company-address">${sAddress}</div>` : ''}
                  ${sPhone ? `<div class="company-address" style="margin-top: 2px;">TEL: ${sPhone}${sFax ? ` &nbsp; FAX: ${sFax}` : ''}</div>` : ''}
                </div>

                <div class="header-right">
                  <table>
                    <tr><td>PAGE</td><td>:</td><td style="font-weight: 700;">1</td></tr>
                  </table>
                </div>
              </div>

              <div class="header-ornament">◆</div>

              <!-- REPORT TITLE -->
              <div class="report-title-area">
                <div class="report-main-title">TRADE IN ISSUE REPORT</div>
                <div class="report-sub-info">ISSUE NO: ${sTradeInIssueNo}</div>
                <div class="report-period-line">${printTimestamp}</div>
              </div>

              <!-- META CARD -->
              <div class="meta-card-container">
                <div class="meta-info-card">
                  <div class="meta-item">
                    <span class="meta-label">Trade Issue No</span>
                    <span class="meta-val">${sTradeInIssueNo}</span>
                  </div>
                  <div class="meta-item">
                    <span class="meta-label">Issue Date</span>
                    <span class="meta-val">${issueDateFormatted}</span>
                  </div>
                  <div class="meta-item">
                    <span class="meta-label">Supplier Code</span>
                    <span class="meta-val">${header.SupplierCode || supplierCode || 'N/A'}</span>
                  </div>
                  <div class="meta-item">
                    <span class="meta-label">Supplier Name</span>
                    <span class="meta-val">${header.SupplierName || supplierName || 'REFINERY / WHOLESALE'}</span>
                  </div>
                </div>
              </div>

              <!-- DETAIL TABLE -->
              <div class="table-container">
                <table class="report-table">
                  <colgroup>
                    <col style="width: 6%;" />
                    <col style="width: 18%;" />
                    <col style="width: 26%;" />
                    <col style="width: 14%;" />
                    <col style="width: 12%;" />
                    <col style="width: 12%;" />
                    <col style="width: 12%;" />
                  </colgroup>
                  <thead>
                    <tr>
                      <th>#</th>
                      <th>BILL / INVOICE NO</th>
                      <th style="text-align: left !important; padding-left: 8px;">PARTICULARS</th>
                      <th style="text-align: right !important; padding-right: 8px;">ISSUED WT (g)</th>
                      <th style="text-align: right !important; padding-right: 8px;">RATE / g</th>
                      <th style="text-align: right !important; padding-right: 8px;">AMOUNT ($)</th>
                      <th style="text-align: left !important; padding-left: 8px;">REMARKS</th>
                    </tr>
                  </thead>
                  <tbody>
                    ${rowsHtml || '<tr><td colspan="7" style="text-align:center; padding: 12px;">No issue line items found</td></tr>'}

                    <!-- Grand Total Row -->
                    <tr class="grand-total-row">
                      <td colspan="3" style="text-align: right !important; padding-right: 14px; font-weight: 700;">TOTAL ISSUED:</td>
                      <td style="text-align: right !important; padding-right: 8px; font-weight: 700;">${totalWt.toFixed(3)}</td>
                      <td></td>
                      <td style="text-align: right !important; padding-right: 8px; font-weight: 700;">$ ${totalAmount.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                      <td></td>
                    </tr>
                  </tbody>
                </table>
              </div>

              <!-- SIGNATURE SECTION -->
              <div class="signature-section">
                <!-- Supplier Signature -->
                <div class="sign-card">
                  <div class="sign-header">RECEIVED BY (SUPPLIER / REFINERY)</div>
                  <div class="sign-row">Authorized Sign: ...........................................</div>
                  <div class="sign-row" style="margin-top: 14px;">Name: ................................................................</div>
                  <div class="sign-row" style="margin-top: 14px;">Date: ................................................................</div>
                </div>

                <!-- Store Officer -->
                <div class="sign-card">
                  <div class="sign-header">DISPATCHED BY (STORE OFFICER)</div>
                  <div class="sign-row">Authorized Sign: ...........................................</div>
                  <div class="sign-row" style="margin-top: 14px;">Name: ................................................................</div>
                  <div class="sign-row" style="margin-top: 14px;">Date: ................................................................</div>
                </div>
              </div>

              <!-- FOOTER -->
              <div class="report-footer">
                <div class="footer-divider"></div>
                <div class="footer-gen-text">*** System Generated Report ***</div>
                <div class="footer-powered">Powered by Unipro SG</div>
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
        const printWindow = window.open('', '_blank', 'width=900,height=750');
        if (printWindow) {
          printWindow.document.open();
          printWindow.document.write(printHtml);
          printWindow.document.close();
          showToast('Trade In Issue report preview opened', 'success');
        } else {
          const blob = new Blob([printHtml], { type: 'text/html;charset=utf-8;' });
          const url = URL.createObjectURL(blob);
          const win = window.open(url, '_blank');
          if (!win) {
            showToast('Popup blocker prevented opening print preview. Please allow popups.', 'error');
          } else {
            showToast('Trade In Issue report preview opened', 'success');
          }
        }
      } else {
        showToast('Print preview generated.', 'success');
      }

    } catch (err: any) {
      console.error('Print Error:', err);
      showToast('Error generating print preview: ' + (err.message || 'Error'), 'error');
    } finally {
      setLoading(false);
    }
  };

  // Exact VB.NET btnPrint_Click validation:
  // If txtTradeIssueNo.Text.Trim <> "" Then
  //     subPrint(txtTradeIssueNo.Text.Trim)
  // End If
  const handlePrintClick = () => {
    try {
      if (issueNo && issueNo.trim() !== '') {
        subPrint(issueNo.trim());
      }
    } catch (ex: any) {
      showToast(ex.message || 'Error executing print', 'error');
    }
  };

  return (
    <SafeAreaView style={styles.safeArea} edges={['top']}>
      <Stack.Screen options={{ headerShown: false }} />

      {/* LUXURY ROYAL GOLD HEADER BAR */}
      <View style={styles.headerBar}>
        <View style={styles.headerLeft}>
          <TouchableOpacity style={styles.backBtn} onPress={() => router.back()} activeOpacity={0.7}>
            <Ionicons name="arrow-back" size={20} color="#3B2D17" />
          </TouchableOpacity>
          <View>
            <ThemedText style={styles.headerTitle}>Trade In Gold Issue</ThemedText>
            <ThemedText style={styles.headerSubtitle}>Refining, Export & Production Dispatch Voucher</ThemedText>
          </View>
          {(loading || saving) && <ActivityIndicator size="small" color="#B48218" style={{ marginLeft: 8 }} />}
        </View>

        <View style={styles.headerRight}>
          <TouchableOpacity
            style={[styles.actionBtn, styles.clearBtn]}
            onPress={handleClear}
            activeOpacity={0.8}
            disabled={saving || loading}
          >
            <Ionicons name="refresh-outline" size={14} color="#5C4A28" />
            <ThemedText style={styles.clearBtnText}>Clear</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity
            style={[styles.actionBtn, styles.reprintBtn]}
            onPress={handlePrintClick}
            activeOpacity={0.8}
            disabled={saving || loading}
          >
            <Ionicons name="print-outline" size={14} color="#5C4A28" />
            <ThemedText style={styles.clearBtnText}>Print</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity
            style={[styles.actionBtn, styles.saveBtn]}
            onPress={handleProceed}
            activeOpacity={0.8}
            disabled={saving || loading}
          >
            {saving ? (
              <ActivityIndicator size="small" color="#9E6D08" />
            ) : (
              <>
                <Ionicons name="checkmark-done-outline" size={15} color="#9E6D08" />
                <ThemedText style={styles.saveBtnText}>Proceed Issue</ThemedText>
              </>
            )}
          </TouchableOpacity>
        </View>
      </View>

      {/* TOAST ALERTS */}
      {toast && (
        <View style={[styles.toast, toast.type === 'error' ? styles.toastError : styles.toastSuccess]}>
          <Ionicons
            name={toast.type === 'error' ? 'alert-circle' : 'checkmark-circle'}
            size={16}
            color="#FFFFFF"
            style={{ marginRight: 6 }}
          />
          <ThemedText style={styles.toastText}>{toast.message}</ThemedText>
        </View>
      )}

      <KeyboardAvoidingView behavior={Platform.OS === 'ios' ? 'padding' : undefined} style={{ flex: 1 }}>
        <ScrollView contentContainerStyle={styles.scrollContainer} keyboardShouldPersistTaps="handled">
          {/* CARD 1: ISSUE HEADER & SUPPLIER INFORMATION */}
          <View style={styles.goldCard}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="document-text-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitleSmall}>Issue Voucher & Supplier Details</ThemedText>
              </View>

              {issueNo ? (
                <View style={styles.issueBadge}>
                  <ThemedText style={styles.issueBadgeLabel}>Issue No:</ThemedText>
                  <ThemedText style={styles.issueBadgeValue}>{issueNo}</ThemedText>
                </View>
              ) : null}
            </View>

            <View style={styles.cardBody}>
              {/* Row 1: Issue No + History Lookup, Issue Date, Destination Checkbox Flags */}
              <View style={styles.threeColumnGrid}>
                {/* Issue No */}
                <View style={[styles.inlineFieldRow, { flex: 1.2, minWidth: 260 }]}>
                  <ThemedText style={styles.inlineFieldLabel}>Issue No *</ThemedText>
                  <View style={styles.inputWithCombo}>
                    <TextInput
                      style={[styles.formInput, styles.inputAttached, { flex: 1 }]}
                      value={issueNo}
                      onChangeText={setIssueNo}
                      placeholder="Enter or generate..."
                      placeholderTextColor="#9E8D77"
                    />
                    <TouchableOpacity
                      style={styles.attachedLookupBtn}
                      onPress={fetchHistory}
                      activeOpacity={0.7}
                    >
                      <Ionicons name="search" size={13} color="#7D5E19" />
                    </TouchableOpacity>
                    <TouchableOpacity
                      style={styles.newDocketBtn}
                      onPress={fetchNextIssueNo}
                      activeOpacity={0.8}
                    >
                      <Ionicons name="add" size={13} color="#7D5E19" />
                      <ThemedText style={styles.newDocketBtnText}>New</ThemedText>
                    </TouchableOpacity>
                  </View>
                </View>

                {/* Issue Date */}
                <View style={[styles.inlineFieldRow, { flex: 0.9, minWidth: 200 }]}>
                  <ThemedText style={styles.inlineFieldLabel}>Issue Date</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1 }]}
                    value={issueDate}
                    editable={false}
                  />
                </View>

                {/* Flags: Production & Export */}
                <View style={[styles.inlineFieldRow, { flex: 1, minWidth: 200, gap: 10 }]}>
                  <TouchableOpacity
                    style={[styles.checkboxPill, isProduction && styles.checkboxPillActive]}
                    onPress={() => setIsProduction(!isProduction)}
                    activeOpacity={0.7}
                  >
                    <Ionicons
                      name={isProduction ? 'checkbox' : 'square-outline'}
                      size={15}
                      color={isProduction ? '#9E6D08' : '#8D6409'}
                    />
                    <ThemedText style={[styles.checkboxPillText, isProduction && styles.checkboxPillTextActive]}>
                      Production
                    </ThemedText>
                  </TouchableOpacity>

                  <TouchableOpacity
                    style={[styles.checkboxPill, isExport && styles.checkboxPillActive]}
                    onPress={() => setIsExport(!isExport)}
                    activeOpacity={0.7}
                  >
                    <Ionicons
                      name={isExport ? 'checkbox' : 'square-outline'}
                      size={15}
                      color={isExport ? '#9E6D08' : '#8D6409'}
                    />
                    <ThemedText style={[styles.checkboxPillText, isExport && styles.checkboxPillTextActive]}>
                      Export
                    </ThemedText>
                  </TouchableOpacity>
                </View>
              </View>

              {/* Row 2: Supplier Code + Lookup, Supplier Name */}
              <View style={[styles.twoColumnGrid, { marginTop: 6 }]}>
                {/* Supplier Code */}
                <View style={[styles.inlineFieldRow, { flex: 1, minWidth: 260 }]}>
                  <ThemedText style={styles.inlineFieldLabel}>Supplier Code *</ThemedText>
                  <View style={styles.inputWithLookup}>
                    <TextInput
                      style={[styles.formInput, styles.inputAttached, { flex: 1 }]}
                      value={supplierCode}
                      onChangeText={setSupplierCode}
                      placeholder="Select/Enter Supplier..."
                      placeholderTextColor="#9E8D77"
                    />
                    <TouchableOpacity
                      style={styles.attachedLookupBtn}
                      onPress={fetchSuppliers}
                      activeOpacity={0.7}
                    >
                      <Ionicons name="search" size={13} color="#7D5E19" />
                    </TouchableOpacity>
                  </View>
                </View>

                {/* Supplier Name */}
                <View style={[styles.inlineFieldRow, { flex: 1.5, minWidth: 260 }]}>
                  <ThemedText style={styles.inlineFieldLabel}>Supplier Name</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1 }]}
                    value={supplierName}
                    editable={false}
                    placeholder="Auto-populated supplier"
                    placeholderTextColor="#9E8D77"
                  />
                </View>
              </View>
            </View>
          </View>

          {/* CARD 2: WEIGHTS & COST SUMMARY */}
          <View style={[styles.goldCard, { marginTop: 10 }]}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="calculator-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitleSmall}>Issue Weight & Pricing Valuation</ThemedText>
              </View>
            </View>

            <View style={styles.cardBody}>
              {/* Row 1: Total Issued Wt, Cost Per Gram, Total Price */}
              <View style={styles.threeColumnGrid}>
                {/* Total Issued Wt */}
                <View style={styles.inlineFieldRow}>
                  <ThemedText style={styles.inlineFieldLabel}>Total Issued (g)</ThemedText>
                  <TextInput
                    style={[styles.formInput, { flex: 1, textAlign: 'right' }]}
                    value={totalIssuedWt}
                    onChangeText={handleIssuedWtChange}
                    keyboardType="numeric"
                    placeholder="0.00"
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* Cost Per Gram */}
                <View style={styles.inlineFieldRow}>
                  <ThemedText style={styles.inlineFieldLabel}>Cost/Gram ($)</ThemedText>
                  <TextInput
                    style={[styles.formInput, { flex: 1, textAlign: 'right' }]}
                    value={costPerGram}
                    onChangeText={handleCostChange}
                    keyboardType="numeric"
                    placeholder="0.00"
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* Total Price */}
                <View style={styles.inlineFieldRow}>
                  <ThemedText style={styles.inlineFieldLabel}>Total Price ($)</ThemedText>
                  <TextInput
                    style={[
                      styles.formInput,
                      styles.readonlyInput,
                      styles.highlightTotal,
                      { flex: 1, textAlign: 'right' },
                    ]}
                    value={totalPrice}
                    editable={false}
                  />
                </View>
              </View>

              {/* Row 2: Standard Gold Equivalent Total Wt, Purity, Remarks */}
              <View style={[styles.threeColumnGrid, { marginTop: 6 }]}>
                {/* Total Wt (920 Gold Equiv) */}
                <View style={styles.inlineFieldRow}>
                  <ThemedText style={styles.inlineFieldLabel}>Net Conv Wt (g)</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1, textAlign: 'right' }]}
                    value={totalWt}
                    editable={false}
                  />
                </View>

                {/* Purity */}
                <View style={styles.inlineFieldRow}>
                  <ThemedText style={styles.inlineFieldLabel}>Purity / Fineness</ThemedText>
                  <TextInput
                    style={[styles.formInput, { flex: 1 }]}
                    value={purity}
                    onChangeText={setPurity}
                    placeholder="e.g. 916, 999..."
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* Remarks */}
                <View style={styles.inlineFieldRow}>
                  <ThemedText style={styles.inlineFieldLabel}>Remarks</ThemedText>
                  <TextInput
                    style={[styles.formInput, { flex: 1 }]}
                    value={remarks}
                    onChangeText={setRemarks}
                    placeholder="Dispatch notes..."
                    placeholderTextColor="#9E8D77"
                  />
                </View>
              </View>
            </View>
          </View>

          {/* CARD 3: TRADE BILLS TABLE (100% FULL WIDTH) */}
          <View style={[styles.goldCard, { marginTop: 10 }]}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="receipt-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitleSmall}>Trade-In Gold Bills Allocated</ThemedText>
                <View style={styles.counterBadge}>
                  <ThemedText style={styles.counterBadgeText}>{gridData.length} bills</ThemedText>
                </View>
              </View>

              {/* Add Trade Bill Quick Action */}
              <TouchableOpacity
                style={styles.addBillHeaderBtn}
                onPress={fetchTradeBills}
                activeOpacity={0.8}
              >
                <Ionicons name="add-circle" size={14} color="#9E6D08" style={{ marginRight: 4 }} />
                <ThemedText style={styles.addBillHeaderBtnText}>Add Trade Bill</ThemedText>
              </TouchableOpacity>
            </View>

            {/* Table Header (100% Full Width) */}
            <View style={styles.tableHeadRow}>
              <ThemedText style={[styles.tableHeadCell, { flex: 1.6 }]}>Invoice No</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 1.2 }]}>Date</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 1.6 }]}>Inventory Code</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 1.3, textAlign: 'right' }]}>
                Purchase Wt (g)
              </ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 1.3, textAlign: 'right' }]}>
                Issued Wt (g)
              </ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 1.3, textAlign: 'right' }]}>
                Balance Wt (g)
              </ThemedText>
              <ThemedText style={[styles.tableHeadCell, { width: 60, textAlign: 'center' }]}>Action</ThemedText>
            </View>

            {/* Table Body */}
            {gridData.length === 0 ? (
              <View style={styles.emptyTableContainer}>
                <Ionicons name="receipt-outline" size={28} color="#D4C3A3" />
                <ThemedText style={styles.emptyTableText}>
                  No trade bills allocated. Click "+ Add Trade Bill" above to select.
                </ThemedText>
              </View>
            ) : (
              gridData.map((item, index) => (
                <View key={index} style={styles.tableRow}>
                  <ThemedText style={[styles.tableCell, styles.codeCell, { flex: 1.6 }]}>
                    {item.InvoiceNo}
                  </ThemedText>
                  <ThemedText style={[styles.tableCell, { flex: 1.2 }]}>{item.Date}</ThemedText>
                  <ThemedText style={[styles.tableCell, { flex: 1.6 }]}>{item.InventoryCode}</ThemedText>
                  <ThemedText style={[styles.tableCell, { flex: 1.3, textAlign: 'right', fontWeight: 'bold' }]}>
                    {parseFloat(item.PurchaseWeight || 0).toFixed(2)}
                  </ThemedText>
                  <ThemedText style={[styles.tableCell, { flex: 1.3, textAlign: 'right', color: '#7A6855' }]}>
                    {parseFloat(item.IssuedWeight || 0).toFixed(2)}
                  </ThemedText>
                  <ThemedText
                    style={[
                      styles.tableCell,
                      styles.amountCell,
                      { flex: 1.3, textAlign: 'right' },
                    ]}
                  >
                    {parseFloat(item.BalanceWeight || 0).toFixed(2)}
                  </ThemedText>
                  <View style={{ width: 60, alignItems: 'center' }}>
                    <TouchableOpacity
                      style={styles.deleteRowBtn}
                      onPress={() => handleDeleteGridItem(index)}
                      activeOpacity={0.7}
                    >
                      <Ionicons name="trash-outline" size={14} color="#C62828" />
                    </TouchableOpacity>
                  </View>
                </View>
              ))
            )}
          </View>
        </ScrollView>
      </KeyboardAvoidingView>

      {/* LOV MODAL */}
      <Modal visible={lovVisible} transparent animationType="fade" onRequestClose={() => setLovVisible(false)}>
        <View style={styles.modalOverlay}>
          <View style={styles.lovModalCard}>
            <View style={styles.modalHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons
                    name={
                      lovType === 'supplier'
                        ? 'business-outline'
                        : lovType === 'history'
                          ? 'time-outline'
                          : 'receipt-outline'
                    }
                    size={13}
                    color="#B48218"
                  />
                </View>
                <ThemedText style={styles.modalHeaderTitle}>
                  {lovType === 'supplier'
                    ? 'Select Supplier'
                    : lovType === 'history'
                      ? 'Select Issue History'
                      : 'Select Trade Bill to Add'}
                </ThemedText>
              </View>
              <TouchableOpacity
                onPress={() => setLovVisible(false)}
                activeOpacity={0.7}
                style={styles.closeModalBtn}
              >
                <Ionicons name="close" size={18} color="#3B2D17" />
              </TouchableOpacity>
            </View>

            <View style={styles.modalSearchBox}>
              <Ionicons name="search-outline" size={16} color="#8D6409" style={{ marginRight: 6 }} />
              <TextInput
                style={styles.modalSearchInput}
                value={lovSearchQuery}
                onChangeText={setLovSearchQuery}
                placeholder="Search..."
                placeholderTextColor="#9E8D77"
                autoFocus
              />
              {lovSearchQuery.length > 0 && (
                <TouchableOpacity onPress={() => setLovSearchQuery('')}>
                  <Ionicons name="close-circle" size={16} color="#9E8D77" />
                </TouchableOpacity>
              )}
            </View>

            <View style={styles.lovHeadRow}>
              {lovType === 'supplier' && (
                <>
                  <ThemedText style={[styles.lovHeadText, { flex: 1.4 }]}>Supplier Code</ThemedText>
                  <ThemedText style={[styles.lovHeadText, { flex: 2.2 }]}>Supplier Name</ThemedText>
                </>
              )}
              {lovType === 'history' && (
                <>
                  <ThemedText style={[styles.lovHeadText, { flex: 1.6 }]}>Issue No</ThemedText>
                  <ThemedText style={[styles.lovHeadText, { flex: 2.2 }]}>Supplier</ThemedText>
                </>
              )}
              {lovType === 'tradebill' && (
                <>
                  <ThemedText style={[styles.lovHeadText, { flex: 1.5 }]}>Invoice No</ThemedText>
                  <ThemedText style={[styles.lovHeadText, { flex: 1.4 }]}>Item Code</ThemedText>
                  <ThemedText style={[styles.lovHeadText, { flex: 1.2, textAlign: 'right' }]}>
                    Balance Wt
                  </ThemedText>
                </>
              )}
            </View>

            <FlatList
              data={lovData.filter((item) => {
                const q = lovSearchQuery.toLowerCase();
                if (lovType === 'supplier') {
                  return (
                    (item.SupplierCode || '').toLowerCase().includes(q) ||
                    (item.SupplierName || '').toLowerCase().includes(q)
                  );
                }
                if (lovType === 'history') {
                  return (
                    (item.TradeIssueNo || '').toLowerCase().includes(q) ||
                    (item.SupplierName || '').toLowerCase().includes(q)
                  );
                }
                return (
                  (item.InvoiceNo || '').toLowerCase().includes(q) ||
                  (item.InventoryCode || '').toLowerCase().includes(q)
                );
              })}
              keyExtractor={(item, index) =>
                item.SupplierCode || item.TradeIssueNo || item.InvoiceNo || index.toString()
              }
              initialNumToRender={50}
              maxToRenderPerBatch={50}
              ListEmptyComponent={
                <View style={styles.emptyTableContainer}>
                  <Ionicons name="search-outline" size={24} color="#D4C3A3" />
                  <ThemedText style={styles.emptyTableText}>No matching records found.</ThemedText>
                </View>
              }
              renderItem={({ item }) => (
                <TouchableOpacity
                  style={styles.lovItemRow}
                  onPress={() => handleSelectLovItem(item)}
                  activeOpacity={0.7}
                >
                  {lovType === 'supplier' && (
                    <>
                      <ThemedText style={[styles.lovItemText, styles.codeCell, { flex: 1.4 }]}>
                        {item.SupplierCode}
                      </ThemedText>
                      <ThemedText style={[styles.lovItemText, { flex: 2.2 }]} numberOfLines={1}>
                        {item.SupplierName}
                      </ThemedText>
                    </>
                  )}
                  {lovType === 'history' && (
                    <>
                      <ThemedText style={[styles.lovItemText, styles.codeCell, { flex: 1.6 }]}>
                        {item.TradeIssueNo}
                      </ThemedText>
                      <ThemedText style={[styles.lovItemText, { flex: 2.2 }]} numberOfLines={1}>
                        {item.SupplierName || '-'}
                      </ThemedText>
                    </>
                  )}
                  {lovType === 'tradebill' && (
                    <>
                      <ThemedText style={[styles.lovItemText, styles.codeCell, { flex: 1.5 }]}>
                        {item.InvoiceNo}
                      </ThemedText>
                      <ThemedText style={[styles.lovItemText, { flex: 1.4 }]}>
                        {item.InventoryCode || '-'}
                      </ThemedText>
                      <ThemedText style={[styles.lovItemText, styles.amountCell, { flex: 1.2, textAlign: 'right' }]}>
                        {parseFloat(item.Balanceweight || 0).toFixed(2)}
                      </ThemedText>
                    </>
                  )}
                </TouchableOpacity>
              )}
            />
          </View>
        </View>
      </Modal>
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  safeArea: {
    flex: 1,
    backgroundColor: '#FBF9F5',
  },
  scrollContainer: {
    paddingHorizontal: 12,
    paddingTop: 8,
    paddingBottom: 24,
  },

  // ===== ROYAL GOLD HEADER BAR =====
  headerBar: {
    height: 48,
    backgroundColor: '#FFFFFF',
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingHorizontal: 14,
    borderBottomWidth: 1,
    borderBottomColor: '#E8DCB8',
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
  backBtn: {
    padding: 6,
    marginRight: 8,
    borderRadius: 6,
    backgroundColor: '#F9F6F0',
    borderWidth: 1,
    borderColor: '#E8DCB8',
  },
  headerTitle: {
    fontSize: 15,
    fontWeight: 'bold',
    color: '#3B2D17',
    letterSpacing: 0.3,
  },
  headerSubtitle: {
    fontSize: 10,
    color: '#8D6409',
    fontWeight: '500',
    marginTop: -1,
  },
  headerRight: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 8,
  },
  actionBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 12,
    paddingVertical: 5,
    borderRadius: 6,
    borderWidth: 1,
  },
  clearBtn: {
    backgroundColor: '#FFFFFF',
    borderColor: '#DFD5C3',
  },
  clearBtnText: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#5C4A28',
    marginLeft: 4,
  },
  reprintBtn: {
    backgroundColor: '#FFFFFF',
    borderColor: '#DFD5C3',
  },
  saveBtn: {
    backgroundColor: '#F9F2E2',
    borderColor: '#B8903B',
  },
  saveBtnText: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#9E6D08',
    marginLeft: 4,
  },

  // ===== CARDS & BANNERS =====
  goldCard: {
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    overflow: 'hidden',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.04,
    shadowRadius: 3,
    elevation: 2,
  },
  cardHeaderBanner: {
    backgroundColor: '#F5E7CA',
    paddingVertical: 6,
    paddingHorizontal: 10,
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
  },
  headerTitleContainer: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  headerIconBadgeSmall: {
    width: 20,
    height: 20,
    borderRadius: 4,
    backgroundColor: '#FFFFFF',
    justifyContent: 'center',
    alignItems: 'center',
    marginRight: 6,
    borderWidth: 1,
    borderColor: '#E2D3B3',
  },
  cardHeaderTitleSmall: {
    fontSize: 13,
    fontWeight: 'bold',
    color: '#3B2D17',
    letterSpacing: 0.2,
  },
  cardBody: {
    padding: 10,
  },
  issueBadge: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFF4DA',
    paddingHorizontal: 8,
    paddingVertical: 2,
    borderRadius: 4,
    borderWidth: 1,
    borderColor: '#B8903B',
  },
  issueBadgeLabel: {
    fontSize: 10,
    color: '#7D5E19',
    marginRight: 4,
  },
  issueBadgeValue: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#8A5D00',
  },

  // Checkbox Pills
  checkboxPill: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FCFAF6',
    borderWidth: 1,
    borderColor: '#E2D3B3',
    borderRadius: 4,
    paddingVertical: 3,
    paddingHorizontal: 8,
    gap: 4,
  },
  checkboxPillActive: {
    backgroundColor: '#FFF4DA',
    borderColor: '#B8903B',
  },
  checkboxPillText: {
    fontSize: 11,
    fontWeight: '500',
    color: '#5C4A28',
  },
  checkboxPillTextActive: {
    fontWeight: 'bold',
    color: '#9E6D08',
  },

  // ===== GRIDS & INLINE ROWS =====
  twoColumnGrid: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    gap: 8,
  },
  threeColumnGrid: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    gap: 8,
  },
  inlineFieldRow: {
    flex: 1,
    minWidth: 230,
    flexDirection: 'row',
    alignItems: 'center',
  },
  inlineFieldLabel: {
    width: 105,
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3B2D17',
    marginRight: 4,
    flexShrink: 0,
  },
  formInput: {
    height: 26,
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#D4C3A3',
    borderRadius: 4,
    paddingHorizontal: 6,
    paddingVertical: 2,
    fontSize: 12,
    color: '#2B2118',
  },
  readonlyInput: {
    backgroundColor: '#F5EFE6',
    color: '#5C4A28',
  },
  highlightTotal: {
    backgroundColor: '#FFF4DA',
    color: '#8A5D00',
    fontWeight: 'bold',
    borderColor: '#D4C3A3',
  },

  // Input Combo
  inputWithCombo: {
    flex: 1,
    flexDirection: 'row',
    alignItems: 'center',
    flexShrink: 0,
  },
  inputWithLookup: {
    flex: 1,
    flexDirection: 'row',
    alignItems: 'center',
  },
  inputAttached: {
    borderTopRightRadius: 0,
    borderBottomRightRadius: 0,
  },
  attachedLookupBtn: {
    height: 26,
    width: 26,
    backgroundColor: '#F5E7CA',
    borderWidth: 1,
    borderLeftWidth: 0,
    borderColor: '#D4C3A3',
    justifyContent: 'center',
    alignItems: 'center',
    flexShrink: 0,
  },
  newDocketBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    height: 26,
    backgroundColor: '#FFF1D6',
    borderWidth: 1,
    borderLeftWidth: 0,
    borderColor: '#D4C3A3',
    borderTopRightRadius: 4,
    borderBottomRightRadius: 4,
    paddingHorizontal: 7,
    flexShrink: 0,
  },
  newDocketBtnText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#7D5E19',
    marginLeft: 2,
  },

  // Add bill button in card header
  addBillHeaderBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#B8903B',
    borderRadius: 4,
    paddingHorizontal: 8,
    paddingVertical: 2,
  },
  addBillHeaderBtnText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#9E6D08',
  },

  // ===== TABLE SECTION =====
  counterBadge: {
    backgroundColor: '#FFFFFF',
    paddingHorizontal: 6,
    paddingVertical: 2,
    borderRadius: 10,
    marginLeft: 6,
    borderWidth: 1,
    borderColor: '#E2D3B3',
  },
  counterBadgeText: {
    fontSize: 10,
    fontWeight: 'bold',
    color: '#8D6409',
  },
  tableHeadRow: {
    flexDirection: 'row',
    backgroundColor: '#F5E7CA',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    paddingVertical: 6,
    paddingHorizontal: 8,
    width: '100%',
  },
  tableHeadCell: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3B2D17',
    paddingHorizontal: 4,
  },
  tableRow: {
    flexDirection: 'row',
    alignItems: 'center',
    borderBottomWidth: 1,
    borderBottomColor: '#F3EBDD',
    paddingVertical: 5,
    paddingHorizontal: 8,
    backgroundColor: '#FFFFFF',
    width: '100%',
  },
  tableCell: {
    fontSize: 11,
    color: '#3B2D17',
    paddingHorizontal: 4,
  },
  codeCell: {
    fontFamily: Platform.OS === 'ios' ? 'Courier' : 'monospace',
    fontWeight: 'bold',
    color: '#8A5D00',
  },
  amountCell: {
    fontWeight: 'bold',
    color: '#1B5E20',
  },
  deleteRowBtn: {
    padding: 3,
  },
  emptyTableContainer: {
    paddingVertical: 24,
    alignItems: 'center',
    justifyContent: 'center',
    width: '100%',
  },
  emptyTableText: {
    fontSize: 12,
    color: '#9E8D77',
    marginTop: 4,
  },

  // ===== LOV MODALS =====
  modalOverlay: {
    flex: 1,
    backgroundColor: 'rgba(0, 0, 0, 0.45)',
    justifyContent: 'center',
    alignItems: 'center',
    padding: 16,
  },
  lovModalCard: {
    width: '92%',
    maxWidth: 600,
    maxHeight: '80%',
    backgroundColor: '#FFFFFF',
    borderRadius: 10,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    overflow: 'hidden',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.15,
    shadowRadius: 8,
    elevation: 8,
  },
  modalHeaderBanner: {
    backgroundColor: '#F5E7CA',
    paddingVertical: 8,
    paddingHorizontal: 12,
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
  },
  modalHeaderTitle: {
    fontSize: 13,
    fontWeight: 'bold',
    color: '#3B2D17',
  },
  closeModalBtn: {
    padding: 3,
  },
  modalSearchBox: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FCFAF6',
    margin: 8,
    paddingHorizontal: 8,
    height: 32,
    borderWidth: 1,
    borderColor: '#D4C3A3',
    borderRadius: 6,
  },
  modalSearchInput: {
    flex: 1,
    fontSize: 12,
    color: '#2B2118',
    paddingVertical: 0,
  },
  lovHeadRow: {
    flexDirection: 'row',
    backgroundColor: '#F8F2E4',
    paddingVertical: 5,
    paddingHorizontal: 10,
    borderBottomWidth: 1,
    borderBottomColor: '#E8DCB8',
  },
  lovHeadText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#8D6409',
  },
  lovItemRow: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingVertical: 8,
    paddingHorizontal: 10,
    borderBottomWidth: 1,
    borderBottomColor: '#F3EBDD',
  },
  lovItemText: {
    fontSize: 11,
    color: '#3B2D17',
  },

  // ===== TOAST =====
  toast: {
    position: 'absolute',
    top: 56,
    alignSelf: 'center',
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 16,
    paddingVertical: 8,
    borderRadius: 20,
    zIndex: 9999,
    elevation: 6,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.15,
    shadowRadius: 4,
  },
  toastSuccess: {
    backgroundColor: '#2E7D32',
  },
  toastError: {
    backgroundColor: '#D32F2F',
  },
  toastText: {
    color: '#FFFFFF',
    fontWeight: 'bold',
    fontSize: 12,
  },
});
