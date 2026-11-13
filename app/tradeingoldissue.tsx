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

const API_URL = `${API_BASE_URL}/tradeingoldissue`;

const CLASSES = ['22K', '24K', '18K', '835', '750', '916', '875', '20K', '21K'];

export default function TradeInGoldIssueScreen() {
  const themeColors = useThemeColors();
  const router = useRouter();

  // States
  const [loading, setLoading] = useState(false);
  const [saving, setSaving] = useState(false);
  const [toast, setToast] = useState<{ message: string; type: 'success' | 'error' } | null>(null);

  // Header Details
  const [invoiceNo, setInvoiceNo] = useState('');
  const [customerCode, setCustomerCode] = useState('');
  const [customerName, setCustomerName] = useState('');
  const [address1, setAddress1] = useState('');
  const [address2, setAddress2] = useState('');
  const [address3, setAddress3] = useState('');

  // Settings
  const [lossPerc, setLossPerc] = useState('0');
  const [costPerGram, setCostPerGram] = useState('0');
  const [remarks, setRemarks] = useState('');
  const [selectedClass, setSelectedClass] = useState('22K');

  // Input fields for grid
  const [description, setDescription] = useState('22K ITEMS');
  const [diamondDescription, setDiamondDescription] = useState('');
  const [weight, setWeight] = useState('');
  const [stoneWeight, setStoneWeight] = useState('');
  const [goldLoss, setGoldLoss] = useState('');
  const [totalCost, setTotalCost] = useState('');

  // Grid Data
  const [gridData, setGridData] = useState<any[]>([]);

  // LOV State
  const [lovVisible, setLovVisible] = useState(false);
  const [lovData, setLovData] = useState<any[]>([]);
  const [lovSearch, setLovSearch] = useState('');

  // Summary
  const salesAmount = 0.0;
  const tradeInAmount = gridData.reduce((sum, item) => sum + (parseFloat(item.totalCost) || 0), 0);
  const balanceAmount = salesAmount - tradeInAmount;

  const showToast = (message: string, type: 'success' | 'error' = 'success') => {
    setToast({ message, type });
    setTimeout(() => setToast(null), 3000);
  };

  useEffect(() => {
    fetchCostPerGram(selectedClass);
    setDescription(`${selectedClass} ITEMS`);
  }, [selectedClass]);

  const fetchCostPerGram = async (cCode: string) => {
    try {
      const response = await fetch(`${API_URL}/costpergram/${encodeURIComponent(cCode)}`);
      const data = await response.json();
      if (data.success && data.Rate !== undefined) {
        setCostPerGram(String(data.Rate));
      }
    } catch (err) {
      console.error(err);
    }
  };

  const fetchCustomers = async () => {
    try {
      setLoading(true);
      const response = await fetch(`${API_URL}/customers`);
      const data = await response.json();
      if (data.success) {
        setLovData(data.data || []);
        setLovSearch('');
        setLovVisible(true);
      } else {
        showToast('Error loading customer list', 'error');
      }
    } catch (err) {
      showToast('Error fetching customers', 'error');
    } finally {
      setLoading(false);
    }
  };

  const handleSelectCustomer = (item: any) => {
    setCustomerCode(item.CustomerCode || '');
    setCustomerName(item.CustomerName || '');
    setAddress1(item.Address1 || '');
    setAddress2(item.Address2 || '');
    setAddress3(item.Address3 || '');
    setLovVisible(false);
  };

  const lookupCustomerByCode = async (code: string) => {
    if (!code.trim()) return;
    try {
      const response = await fetch(`${API_URL}/customers`);
      const data = await response.json();
      if (data.success && Array.isArray(data.data)) {
        const found = data.data.find(
          (c: any) => (c.CustomerCode || '').trim().toLowerCase() === code.trim().toLowerCase()
        );
        if (found) {
          handleSelectCustomer(found);
        }
      }
    } catch (err) {
      console.error(err);
    }
  };

  const calculateTotalCost = () => {
    const wt = parseFloat(weight) || 0;
    const costPerGm = parseFloat(costPerGram) || 0;
    let calcCost = wt * costPerGm;
    if (selectedClass === '24K') {
      calcCost = calcCost * 1.05;
    }
    setTotalCost(calcCost.toFixed(2));
  };

  const handleAddToGrid = () => {
    if (!description.trim()) {
      showToast('Description is required', 'error');
      return;
    }
    if (!weight || parseFloat(weight) <= 0) {
      showToast('Weight is invalid', 'error');
      return;
    }
    if (parseFloat(goldLoss || '0') < 0) {
      showToast('Gold Loss is invalid', 'error');
      return;
    }
    if (parseFloat(costPerGram || '0') <= 0) {
      showToast('Cost per gram is invalid', 'error');
      return;
    }

    const calculatedCost =
      totalCost && parseFloat(totalCost) > 0
        ? parseFloat(totalCost).toFixed(2)
        : (
            (parseFloat(weight) || 0) * (parseFloat(costPerGram) || 0) * (selectedClass === '24K' ? 1.05 : 1)
          ).toFixed(2);

    const newItem = {
      description: description.trim(),
      diamondDescription: diamondDescription.trim(),
      weight: parseFloat(weight).toFixed(3),
      stoneWeight: parseFloat(stoneWeight || '0').toFixed(3),
      goldLoss: parseFloat(goldLoss || '0').toFixed(3),
      costPerGram: parseFloat(costPerGram).toFixed(2),
      totalCost: calculatedCost,
      class: selectedClass,
    };

    setGridData([...gridData, newItem]);

    // Clear inputs for next item
    setDescription(`${selectedClass} ITEMS`);
    setDiamondDescription('');
    setWeight('');
    setStoneWeight('');
    setGoldLoss('');
    setTotalCost('');
    showToast('Trade-in item added', 'success');
  };

  const handleDeleteRow = (index: number) => {
    const newData = [...gridData];
    newData.splice(index, 1);
    setGridData(newData);
  };

  const handleClear = () => {
    setCustomerCode('');
    setCustomerName('');
    setAddress1('');
    setAddress2('');
    setAddress3('');
    setRemarks('');
    setGridData([]);
    setWeight('');
    setStoneWeight('');
    setGoldLoss('');
    setTotalCost('');
    setSelectedClass('22K');
    setInvoiceNo('');
  };

  const handleSave = async () => {
    if (!customerCode.trim()) {
      showToast('Please select a customer first.', 'error');
      return;
    }
    if (gridData.length === 0) {
      showToast('Please add at least one trade-in item.', 'error');
      return;
    }

    try {
      setSaving(true);
      const payload = {
        customerCode,
        customerName,
        address1,
        address2,
        address3,
        lossPerc,
        costPerGram,
        remarks,
        items: gridData,
      };

      const response = await fetch(`${API_URL}/save`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload),
      });
      const data = await response.json();

      if (data.success) {
        const assignedInvoiceNo = data.invoiceNo || 'TI-SAVED';
        setInvoiceNo(assignedInvoiceNo);
        showToast(data.message || 'Trade-in Saved Successfully', 'success');

        // Automatically prompt to print matching VB.NET subPrintBill after save
        setTimeout(() => {
          if (Platform.OS === 'web' && typeof window !== 'undefined') {
            if (window.confirm('Do you want to print Trade-In Voucher?')) {
              handlePrint(assignedInvoiceNo);
            }
          }
        }, 500);
      } else {
        showToast(data.message || 'Save failed', 'error');
      }
    } catch (err: any) {
      showToast('Network error while saving trade-in', 'error');
    } finally {
      setSaving(false);
    }
  };

  const combinedAddress = [address1, address2, address3].filter(Boolean).join(' ');

  // Print Trade-In Voucher Functionality
  const handlePrint = async (targetInvoiceNo?: string) => {
    // 1. Validation (matching original VB.NET frmTradeInOnly & frmTradeInIssue)
    const tNo = (targetInvoiceNo || invoiceNo || '').trim();

    // If no voucher number saved yet, check if there is an active assessment on screen
    const hasUnsavedItems = gridData.length > 0 && customerCode.trim() !== '';

    if (!tNo && !hasUnsavedItems) {
      showToast('Please enter/save a Trade-In Voucher or enter customer & items to print.', 'error');
      return;
    }

    try {
      setLoading(true);

      let company: any = { name: '', address1: '', address2: '', city: '', state: '', country: '', phone: '', fax: '' };
      let header: any = null;
      let details: any[] = [];
      let rates: any = { rate999: '', rate916: '', rate835: '', rate22k: '' };

      // 2. Fetch actual data from backend SQL Server
      let fetchSuccess = false;

      if (tNo) {
        try {
          const res = await fetch(`${API_URL}/print-data?invoiceNo=${encodeURIComponent(tNo)}`);
          const cType = res.headers.get('content-type') || '';
          if (res.ok && cType.includes('application/json')) {
            const json = await res.json();
            if (json.success && json.data) {
              company = json.data.company || company;
              header = json.data.header;
              details = json.data.details || [];
              rates = json.data.rates || rates;
              fetchSuccess = true;
            }
          }
        } catch (eDedicated) {
          console.warn('Dedicated print-data fetch error:', eDedicated);
        }
      }

      // If no saved voucher found in DB but user has current on-screen assessment, use current on-screen data
      if (!fetchSuccess && hasUnsavedItems) {
        // Query company info fallback
        try {
          const compRes = await fetch(`${API_BASE_URL}/company`).then(r => r.json()).catch(() => null);
          if (compRes?.success && Array.isArray(compRes.data) && compRes.data.length > 0) {
            const c = compRes.data[0];
            company = {
              name: (c.Name || c.CompanyName || '').trim(),
              address1: (c.Address1 || '').trim(),
              address2: (c.Address2 || '').trim(),
              city: (c.City || '').trim(),
              state: (c.State || '').trim(),
              country: (c.Country || '').trim(),
              phone: (c.Phone || '').trim(),
              fax: (c.Fax || '').trim(),
            };
          }
        } catch (eComp) {}

        let totalWt = 0;
        let totalLoss = 0;
        let totalSt = 0;
        let totalAmt = 0;

        const mappedDet = gridData.map((item, idx) => {
          const wt = parseFloat(item.weight) || 0;
          const loss = parseFloat(item.goldLoss) || 0;
          const st = parseFloat(item.stoneWeight) || 0;
          const cost = parseFloat(item.totalCost) || 0;
          totalWt += wt;
          totalLoss += loss;
          totalSt += st;
          totalAmt += cost;

          return {
            itemNo: idx + 1,
            description: item.description || '',
            diamondDescription: item.stoneDescription || '',
            weight: wt,
            stoneWeight: st,
            goldLoss: loss,
            costPerGram: parseFloat(item.costPerGram) || 0,
            totalCost: cost,
            class: item.class || selectedClass || '',
            remarks: '',
          };
        });

        header = {
          inventoryCode: tNo || 'TI-DRAFT',
          invoiceNo: tNo || 'DRAFT',
          customerCode: customerCode.trim(),
          customerName: customerName.trim(),
          address1: address1.trim(),
          address2: address2.trim(),
          address3: address3.trim(),
          costPerGram: parseFloat(costPerGram) || 0,
          totalWeight: totalWt,
          totalGoldLoss: totalLoss,
          totalStoneWeight: totalSt,
          totalPCS: mappedDet.length,
          totalCost: totalAmt,
          amount: totalAmt,
          remarks: remarks.trim(),
          date: new Date(),
        };

        details = mappedDet;
        fetchSuccess = true;
      }

      if (!fetchSuccess || !header) {
        showToast('Trade-in record not found for: ' + (tNo || 'active voucher'), 'error');
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

      // Voucher Date
      let voucherDateFormatted = '';
      if (header.date) {
        const vd = new Date(header.date);
        if (!isNaN(vd.getTime())) {
          voucherDateFormatted = `${String(vd.getDate()).padStart(2, '0')}/${String(vd.getMonth() + 1).padStart(2, '0')}/${vd.getFullYear()}`;
        }
      }
      if (!voucherDateFormatted) {
        voucherDateFormatted = sgDateStr;
      }

      // Totals calculation
      const totalGrossWeight = details.reduce((sum, r) => sum + (Number(r.weight) || 0), 0);
      const totalGoldLossSum = details.reduce((sum, r) => sum + (Number(r.goldLoss) || 0), 0);
      const totalStoneWtSum = details.reduce((sum, r) => sum + (Number(r.stoneWeight) || 0), 0);
      const totalCostSum = details.reduce((sum, r) => sum + (Number(r.totalCost) || 0), 0);

      const rowsHtml = details
        .map(
          (item: any, idx: number) => `
          <tr>
            <td style="text-align: center; font-weight: 700;">${item.itemNo || (idx + 1)}</td>
            <td style="text-align: left; font-weight: 600;">${item.description || ''}</td>
            <td style="text-align: center; font-weight: 700;">${item.class || '22K'}</td>
            <td style="text-align: right;">${Number(item.weight || 0).toFixed(3)}</td>
            <td style="text-align: right;">${Number(item.stoneWeight || 0).toFixed(3)}</td>
            <td style="text-align: right;">${Number(item.goldLoss || 0).toFixed(3)}</td>
            <td style="text-align: right;">$ ${Number(item.costPerGram || 0).toFixed(2)}</td>
            <td style="text-align: right; font-weight: 700;">$ ${Number(item.totalCost || 0).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
          </tr>`
        )
        .join('');

      const companyName = (company.name || 'SMART GOLD').trim();
      const compAddress = [company.address1, company.address2, company.city, company.state, company.country].filter(Boolean).join(', ');
      const compPhone = (company.phone || '').trim();
      const companyLogo = UNIPRO_LOGO_DATA_URI;
      const custAddress = [header.address1, header.address2, header.address3].filter(Boolean).join(', ');

      const printHtml = `
        <!DOCTYPE html>
        <html>
          <head>
            <meta charset="utf-8" />
            <title>Trade-In Voucher - ${header.invoiceNo || header.inventoryCode}</title>
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
              table.report-table tr.item-total-row td,
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

              /* REMARKS */
              .remarks-box {
                margin-top: 12px;
                font-size: 11.5px;
                font-weight: 700;
                color: #4a3b22;
                padding: 6px 10px;
                background-color: #fffdf8;
                border-left: 3px solid #996515;
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
                  <img src="${companyLogo}" alt="UNIPRO" class="unipro-logo-img" />
                </div>

                <div class="header-center">
                  <div class="company-title">${companyName}</div>
                  ${compAddress ? `<div class="company-address">${compAddress}</div>` : ''}
                  ${compPhone ? `<div class="company-address" style="margin-top: 2px;">TEL: ${compPhone}</div>` : ''}
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
                <div class="report-main-title">TRADE-IN VOUCHER</div>
                <div class="report-sub-info">OLD GOLD PURCHASE & SETTLEMENT</div>
                <div class="report-period-line">${printTimestamp}</div>
              </div>

              <!-- META CARDS -->
              <div class="meta-card-container">
                <div class="meta-info-card">
                  <div class="meta-item">
                    <span class="meta-label">Voucher / Invoice No</span>
                    <span class="meta-val">${header.invoiceNo || header.inventoryCode || ''}</span>
                  </div>
                  <div class="meta-item">
                    <span class="meta-label">Date</span>
                    <span class="meta-val">${voucherDateFormatted}</span>
                  </div>
                  <div class="meta-item">
                    <span class="meta-label">Customer Code</span>
                    <span class="meta-val">${header.customerCode || 'N/A'}</span>
                  </div>
                  <div class="meta-item">
                    <span class="meta-label">Customer Name</span>
                    <span class="meta-val">${header.customerName || 'WALK-IN'}</span>
                  </div>
                </div>

                ${custAddress ? `
                <div class="meta-info-card" style="background: #ffffff; padding: 6px 14px;">
                  <div class="meta-item" style="width: 100%;">
                    <span class="meta-label">Customer Address</span>
                    <span style="font-size: 11px; color: #4a3b22;">${custAddress}</span>
                  </div>
                </div>` : ''}
              </div>

              <!-- DETAIL TABLE -->
              <div class="table-container">
                <table class="report-table">
                  <colgroup>
                    <col style="width: 6%;" />
                    <col style="width: 28%;" />
                    <col style="width: 10%;" />
                    <col style="width: 12%;" />
                    <col style="width: 11%;" />
                    <col style="width: 11%;" />
                    <col style="width: 10%;" />
                    <col style="width: 12%;" />
                  </colgroup>
                  <thead>
                    <tr>
                      <th>#</th>
                      <th style="text-align: left !important; padding-left: 10px;">ITEM DESCRIPTION</th>
                      <th>PURITY / CLASS</th>
                      <th style="text-align: right !important; padding-right: 8px;">GROSS WT (g)</th>
                      <th style="text-align: right !important; padding-right: 8px;">STONE WT (g)</th>
                      <th style="text-align: right !important; padding-right: 8px;">LOSS (g)</th>
                      <th style="text-align: right !important; padding-right: 8px;">RATE / g</th>
                      <th style="text-align: right !important; padding-right: 8px;">TOTAL ($)</th>
                    </tr>
                  </thead>
                  <tbody>
                    ${rowsHtml || '<tr><td colspan="8" style="text-align:center; padding: 12px;">No trade-in items assessed</td></tr>'}

                    <!-- Total Row -->
                    <tr class="grand-total-row">
                      <td colspan="3" style="text-align: right !important; padding-right: 14px; font-weight: 700;">TOTAL SETTLEMENT:</td>
                      <td style="text-align: right !important; padding-right: 8px; font-weight: 700;">${totalGrossWeight.toFixed(3)}</td>
                      <td style="text-align: right !important; padding-right: 8px; font-weight: 700;">${totalStoneWtSum.toFixed(3)}</td>
                      <td style="text-align: right !important; padding-right: 8px; font-weight: 700;">${totalGoldLossSum.toFixed(3)}</td>
                      <td></td>
                      <td style="text-align: right !important; padding-right: 8px; font-weight: 700;">$ ${totalCostSum.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                    </tr>
                  </tbody>
                </table>
              </div>

              <!-- REMARKS -->
              <div class="remarks-box">
                REMARKS &nbsp;&nbsp; : &nbsp;&nbsp; <span style="font-weight: 500;">${header.remarks && header.remarks.trim() ? header.remarks.trim() : 'N/A'}</span>
              </div>

              <!-- SIGNATURE SECTION -->
              <div class="signature-section">
                <!-- Customer Signature -->
                <div class="sign-card">
                  <div class="sign-header">CUSTOMER ACKNOWLEDGEMENT</div>
                  <div class="sign-row">Customer Name: ${header.customerName || '...............................'}</div>
                  <div class="sign-row" style="margin-top: 14px;">Signature: ....................................................</div>
                  <div class="sign-row" style="margin-top: 14px;">Date: ..............................................................</div>
                </div>

                <!-- Verified By -->
                <div class="sign-card">
                  <div class="sign-header">AUTHORIZED STORE OFFICER</div>
                  <div class="sign-row">Assessed & Verified By: ................................</div>
                  <div class="sign-row" style="margin-top: 14px;">Officer Signature: ..........................................</div>
                  <div class="sign-row" style="margin-top: 14px;">Date: ..............................................................</div>
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
          showToast('Trade-in voucher preview opened', 'success');
        } else {
          const blob = new Blob([printHtml], { type: 'text/html;charset=utf-8;' });
          const url = URL.createObjectURL(blob);
          const win = window.open(url, '_blank');
          if (!win) {
            showToast('Popup blocker prevented opening print preview. Please allow popups.', 'error');
          } else {
            showToast('Trade-in voucher preview opened', 'success');
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
            <ThemedText style={styles.headerSubtitle}>Old Gold Purchase, Assessment & Settlement</ThemedText>
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
            onPress={() => handlePrint()}
            activeOpacity={0.8}
            disabled={saving || loading}
          >
            <Ionicons name="print-outline" size={14} color="#5C4A28" />
            <ThemedText style={styles.clearBtnText}>Print</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity
            style={[styles.actionBtn, styles.saveBtn]}
            onPress={handleSave}
            activeOpacity={0.8}
            disabled={saving || loading}
          >
            {saving ? (
              <ActivityIndicator size="small" color="#9E6D08" />
            ) : (
              <>
                <Ionicons name="checkmark-done-outline" size={15} color="#9E6D08" />
                <ThemedText style={styles.saveBtnText}>Save Trade-In</ThemedText>
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
          {/* CARD 1: CUSTOMER & INVOICE DETAILS */}
          <View style={styles.goldCard}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="person-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitleSmall}>Customer & Docket Identification</ThemedText>
              </View>

              {invoiceNo ? (
                <View style={styles.invoiceBadge}>
                  <ThemedText style={styles.invoiceBadgeLabel}>Voucher:</ThemedText>
                  <ThemedText style={styles.invoiceBadgeValue}>{invoiceNo}</ThemedText>
                </View>
              ) : null}
            </View>

            <View style={styles.cardBody}>
              {/* Row 1: Invoice No, Customer Code + Lookup, Customer Name */}
              <View style={styles.threeColumnGrid}>
                {/* Invoice No */}
                <View style={[styles.inlineFieldRow, { flex: 1, minWidth: 220 }]}>
                  <ThemedText style={styles.inlineFieldLabel}>Invoice No</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1 }]}
                    value={invoiceNo}
                    editable={false}
                    placeholder="Auto Generated"
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* Customer Code */}
                <View style={[styles.inlineFieldRow, { flex: 1.2, minWidth: 250 }]}>
                  <ThemedText style={styles.inlineFieldLabel}>Customer *</ThemedText>
                  <View style={styles.inputWithLookup}>
                    <TextInput
                      style={[styles.formInput, styles.inputAttached, { flex: 1 }]}
                      value={customerCode}
                      onChangeText={setCustomerCode}
                      onBlur={() => lookupCustomerByCode(customerCode)}
                      placeholder="Enter/Search Code..."
                      placeholderTextColor="#9E8D77"
                    />
                    <TouchableOpacity
                      style={styles.attachedLookupBtn}
                      onPress={fetchCustomers}
                      activeOpacity={0.7}
                    >
                      <Ionicons name="search" size={13} color="#7D5E19" />
                    </TouchableOpacity>
                  </View>
                </View>

                {/* Customer Name */}
                <View style={[styles.inlineFieldRow, { flex: 1.5, minWidth: 260 }]}>
                  <ThemedText style={styles.inlineFieldLabel}>Name</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1 }]}
                    value={customerName}
                    editable={false}
                    placeholder="Auto-populated"
                    placeholderTextColor="#9E8D77"
                  />
                </View>
              </View>

              {/* Row 2: Address & Remarks */}
              <View style={[styles.twoColumnGrid, { marginTop: 6 }]}>
                {/* Address */}
                <View style={[styles.inlineFieldRow, { flex: 1.8, minWidth: 280 }]}>
                  <ThemedText style={styles.inlineFieldLabel}>Address</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1 }]}
                    value={combinedAddress}
                    editable={false}
                    placeholder="Auto-populated address"
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* Remarks */}
                <View style={[styles.inlineFieldRow, { flex: 1.5, minWidth: 260 }]}>
                  <ThemedText style={styles.inlineFieldLabel}>Remarks</ThemedText>
                  <TextInput
                    style={[styles.formInput, { flex: 1 }]}
                    value={remarks}
                    onChangeText={setRemarks}
                    placeholder="Trade-in notes..."
                    placeholderTextColor="#9E8D77"
                  />
                </View>
              </View>
            </View>
          </View>

          {/* CARD 2: GOLD PURITY CLASS & VALUATION RATES */}
          <View style={[styles.goldCard, { marginTop: 10 }]}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="ribbon-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitleSmall}>Gold Purity Class & Live Valuation Rates</ThemedText>
              </View>
            </View>

            <View style={styles.cardBody}>
              <View style={styles.classAndRatesRow}>
                {/* Gold Class Pills */}
                <View style={styles.classPillsWrapper}>
                  <ThemedText style={[styles.inlineFieldLabel, { width: 55 }]}>Class</ThemedText>
                  <View style={styles.classPillsGroup}>
                    {CLASSES.map((cls) => {
                      const isSelected = selectedClass === cls;
                      return (
                        <TouchableOpacity
                          key={cls}
                          style={[styles.classPill, isSelected && styles.classPillSelected]}
                          onPress={() => setSelectedClass(cls)}
                          activeOpacity={0.7}
                        >
                          <Ionicons
                            name={isSelected ? 'radio-button-on' : 'radio-button-off'}
                            size={12}
                            color={isSelected ? '#9E6D08' : '#7A6855'}
                          />
                          <ThemedText style={[styles.classPillText, isSelected && styles.classPillTextSelected]}>
                            {cls}
                          </ThemedText>
                        </TouchableOpacity>
                      );
                    })}
                  </View>
                </View>

                {/* Rates & Loss Group */}
                <View style={styles.ratesInputGroup}>
                  {/* Loss % */}
                  <View style={[styles.inlineFieldRow, { width: 140, flex: 0 }]}>
                    <ThemedText style={[styles.inlineFieldLabel, { width: 55 }]}>Loss (%)</ThemedText>
                    <TextInput
                      style={[styles.formInput, { flex: 1, textAlign: 'right' }]}
                      value={lossPerc}
                      onChangeText={setLossPerc}
                      keyboardType="numeric"
                    />
                  </View>

                  {/* Cost Per Gram */}
                  <View style={[styles.inlineFieldRow, { width: 175, flex: 0 }]}>
                    <ThemedText style={[styles.inlineFieldLabel, { width: 75 }]}>Cost/Gram ($)</ThemedText>
                    <TextInput
                      style={[styles.formInput, styles.highlightTotal, { flex: 1, textAlign: 'right' }]}
                      value={costPerGram}
                      onChangeText={setCostPerGram}
                      keyboardType="numeric"
                    />
                  </View>
                </View>
              </View>
            </View>
          </View>

          {/* CARD 3: TRADE-IN ITEMS TABLE & ENTRY SUBFORM (100% FULL WIDTH) */}
          <View style={[styles.goldCard, { marginTop: 10 }]}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="sparkles-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitleSmall}>Trade-In Items Assessment</ThemedText>
                <View style={styles.counterBadge}>
                  <ThemedText style={styles.counterBadgeText}>{gridData.length} items</ThemedText>
                </View>
              </View>
            </View>

            {/* Table Header (100% Full Width) */}
            <View style={styles.tableHeadRow}>
              <ThemedText style={[styles.tableHeadCell, { width: 36, textAlign: 'center' }]}>#</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 2.2 }]}>Description</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { width: 70, textAlign: 'center' }]}>Class</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 1, textAlign: 'right' }]}>Weight (g)</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 1, textAlign: 'right' }]}>Stone (g)</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 1, textAlign: 'right' }]}>Loss (g)</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 1.1, textAlign: 'right' }]}>Rate ($)</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 1.3, textAlign: 'right' }]}>Total ($)</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { width: 60, textAlign: 'center' }]}>Action</ThemedText>
            </View>

            {/* Table Body */}
            {gridData.length === 0 ? (
              <View style={styles.emptyTableContainer}>
                <Ionicons name="cube-outline" size={28} color="#D4C3A3" />
                <ThemedText style={styles.emptyTableText}>
                  No trade-in items assessed. Use the form below to add gold items.
                </ThemedText>
              </View>
            ) : (
              gridData.map((item, index) => (
                <View key={index} style={styles.tableRow}>
                  <ThemedText style={[styles.tableCell, { width: 36, textAlign: 'center', color: '#8D6409' }]}>
                    {index + 1}
                  </ThemedText>
                  <View style={{ flex: 2.2, paddingHorizontal: 4 }}>
                    <ThemedText style={[styles.tableCell, { fontWeight: 'bold' }]} numberOfLines={1}>
                      {item.description}
                    </ThemedText>
                    {item.diamondDescription ? (
                      <ThemedText style={{ fontSize: 10, color: '#7A6855' }} numberOfLines={1}>
                        {item.diamondDescription}
                      </ThemedText>
                    ) : null}
                  </View>
                  <View style={{ width: 70, alignItems: 'center' }}>
                    <View style={styles.classTag}>
                      <ThemedText style={styles.classTagText}>{item.class}</ThemedText>
                    </View>
                  </View>
                  <ThemedText style={[styles.tableCell, { flex: 1, textAlign: 'right' }]}>
                    {item.weight}
                  </ThemedText>
                  <ThemedText style={[styles.tableCell, { flex: 1, textAlign: 'right', color: '#7A6855' }]}>
                    {item.stoneWeight}
                  </ThemedText>
                  <ThemedText style={[styles.tableCell, { flex: 1, textAlign: 'right', color: '#C62828' }]}>
                    {item.goldLoss}
                  </ThemedText>
                  <ThemedText style={[styles.tableCell, { flex: 1.1, textAlign: 'right' }]}>
                    {item.costPerGram}
                  </ThemedText>
                  <ThemedText style={[styles.tableCell, styles.amountCell, { flex: 1.3, textAlign: 'right' }]}>
                    {item.totalCost}
                  </ThemedText>
                  <View style={{ width: 60, alignItems: 'center' }}>
                    <TouchableOpacity
                      style={styles.deleteRowBtn}
                      onPress={() => handleDeleteRow(index)}
                      activeOpacity={0.7}
                    >
                      <Ionicons name="trash-outline" size={14} color="#C62828" />
                    </TouchableOpacity>
                  </View>
                </View>
              ))
            )}

            {/* SUBFORM: ITEM ENTRY BAR */}
            <View style={styles.itemEntryBar}>
              {/* Row 1: Description, Diamond Description */}
              <View style={styles.twoColumnGrid}>
                <View style={[styles.inlineFieldRow, { flex: 1.5, minWidth: 260 }]}>
                  <ThemedText style={styles.inlineFieldLabel}>Description *</ThemedText>
                  <TextInput
                    style={[styles.formInput, { flex: 1 }]}
                    value={description}
                    onChangeText={setDescription}
                    placeholder="e.g. 22K Bangle, Scrap Gold..."
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                <View style={[styles.inlineFieldRow, { flex: 1.5, minWidth: 260 }]}>
                  <ThemedText style={styles.inlineFieldLabel}>Diamond / Gem</ThemedText>
                  <TextInput
                    style={[styles.formInput, { flex: 1 }]}
                    value={diamondDescription}
                    onChangeText={setDiamondDescription}
                    placeholder="Diamond or gemstone details..."
                    placeholderTextColor="#9E8D77"
                  />
                </View>
              </View>

              {/* Row 2: Weight, Stone Weight, Gold Loss, Total Cost + Add Button */}
              <View style={[styles.fourColumnGrid, { marginTop: 6 }]}>
                {/* Gross Weight */}
                <View style={[styles.inlineFieldRow, { flex: 1, minWidth: 150 }]}>
                  <ThemedText style={[styles.inlineFieldLabel, { width: 70 }]}>Weight (g) *</ThemedText>
                  <TextInput
                    style={[styles.formInput, { flex: 1, textAlign: 'right' }]}
                    value={weight}
                    onChangeText={setWeight}
                    onBlur={calculateTotalCost}
                    keyboardType="numeric"
                    placeholder="0.000"
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* Stone Weight */}
                <View style={[styles.inlineFieldRow, { flex: 1, minWidth: 150 }]}>
                  <ThemedText style={[styles.inlineFieldLabel, { width: 70 }]}>Stone (g)</ThemedText>
                  <TextInput
                    style={[styles.formInput, { flex: 1, textAlign: 'right' }]}
                    value={stoneWeight}
                    onChangeText={setStoneWeight}
                    keyboardType="numeric"
                    placeholder="0.000"
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* Gold Loss */}
                <View style={[styles.inlineFieldRow, { flex: 1, minWidth: 150 }]}>
                  <ThemedText style={[styles.inlineFieldLabel, { width: 70 }]}>Loss (g)</ThemedText>
                  <TextInput
                    style={[styles.formInput, { flex: 1, textAlign: 'right' }]}
                    value={goldLoss}
                    onChangeText={setGoldLoss}
                    keyboardType="numeric"
                    placeholder="0.000"
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* Total Cost */}
                <View style={[styles.inlineFieldRow, { flex: 1.2, minWidth: 170 }]}>
                  <ThemedText style={[styles.inlineFieldLabel, { width: 75 }]}>Total Cost ($)</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.highlightTotal, { flex: 1, textAlign: 'right' }]}
                    value={totalCost}
                    onChangeText={setTotalCost}
                    keyboardType="numeric"
                    placeholder="0.00"
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* Add to Grid Button */}
                <TouchableOpacity
                  style={[styles.tableActionBtn, styles.addBtn, { height: 26, paddingHorizontal: 12 }]}
                  onPress={handleAddToGrid}
                  activeOpacity={0.8}
                >
                  <Ionicons name="add-circle-outline" size={14} color="#9E6D08" style={{ marginRight: 4 }} />
                  <ThemedText style={styles.addBtnText}>Add Item</ThemedText>
                </TouchableOpacity>
              </View>
            </View>
          </View>

          {/* CARD 4: TRADE-IN VALUATION SUMMARY (3 COLUMNS) */}
          <View style={[styles.goldCard, { marginTop: 10 }]}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="calculator-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitleSmall}>Financial Settlement Summary</ThemedText>
              </View>
            </View>

            <View style={styles.cardBody}>
              <View style={styles.threeColumnGrid}>
                {/* Sales Amount */}
                <View style={styles.inlineFieldRow}>
                  <ThemedText style={styles.inlineFieldLabel}>Sales Amount</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1, textAlign: 'right' }]}
                    value={salesAmount.toFixed(2)}
                    editable={false}
                  />
                </View>

                {/* Trade In Amount */}
                <View style={styles.inlineFieldRow}>
                  <ThemedText style={styles.inlineFieldLabel}>Trade-In Val ($)</ThemedText>
                  <TextInput
                    style={[
                      styles.formInput,
                      styles.readonlyInput,
                      styles.highlightTotal,
                      { flex: 1, textAlign: 'right', fontWeight: 'bold' },
                    ]}
                    value={tradeInAmount.toFixed(2)}
                    editable={false}
                  />
                </View>

                {/* Balance Amount */}
                <View style={styles.inlineFieldRow}>
                  <ThemedText style={styles.inlineFieldLabel}>Net Settlement ($)</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1, textAlign: 'right' }]}
                    value={balanceAmount.toFixed(2)}
                    editable={false}
                  />
                </View>
              </View>
            </View>
          </View>
        </ScrollView>
      </KeyboardAvoidingView>

      {/* CUSTOMER LOOKUP MODAL */}
      <Modal visible={lovVisible} transparent animationType="fade" onRequestClose={() => setLovVisible(false)}>
        <View style={styles.modalOverlay}>
          <View style={styles.lovModalCard}>
            <View style={styles.modalHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="person-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.modalHeaderTitle}>Search Customer Directory</ThemedText>
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
                value={lovSearch}
                onChangeText={setLovSearch}
                placeholder="Search by Code or Name..."
                placeholderTextColor="#9E8D77"
                autoFocus
              />
              {lovSearch.length > 0 && (
                <TouchableOpacity onPress={() => setLovSearch('')}>
                  <Ionicons name="close-circle" size={16} color="#9E8D77" />
                </TouchableOpacity>
              )}
            </View>

            <View style={styles.lovHeadRow}>
              <ThemedText style={[styles.lovHeadText, { flex: 1.4 }]}>Customer Code</ThemedText>
              <ThemedText style={[styles.lovHeadText, { flex: 2.2 }]}>Customer Name</ThemedText>
            </View>

            <FlatList
              data={lovData.filter(
                (item) =>
                  (item.CustomerName || '').toLowerCase().includes(lovSearch.toLowerCase()) ||
                  (item.CustomerCode || '').toLowerCase().includes(lovSearch.toLowerCase())
              )}
              keyExtractor={(item, index) => item.CustomerCode || index.toString()}
              initialNumToRender={50}
              maxToRenderPerBatch={50}
              ListEmptyComponent={
                <View style={styles.emptyTableContainer}>
                  <Ionicons name="search-outline" size={24} color="#D4C3A3" />
                  <ThemedText style={styles.emptyTableText}>No matching customers found.</ThemedText>
                </View>
              }
              renderItem={({ item }) => (
                <TouchableOpacity
                  style={styles.lovItemRow}
                  onPress={() => handleSelectCustomer(item)}
                  activeOpacity={0.7}
                >
                  <ThemedText style={[styles.lovItemText, styles.codeCell, { flex: 1.4 }]}>
                    {item.CustomerCode}
                  </ThemedText>
                  <ThemedText style={[styles.lovItemText, { flex: 2.2 }]} numberOfLines={1}>
                    {item.CustomerName}
                  </ThemedText>
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
  invoiceBadge: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFF4DA',
    paddingHorizontal: 8,
    paddingVertical: 2,
    borderRadius: 4,
    borderWidth: 1,
    borderColor: '#B8903B',
  },
  invoiceBadgeLabel: {
    fontSize: 10,
    color: '#7D5E19',
    marginRight: 4,
  },
  invoiceBadgeValue: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#8A5D00',
  },

  // ===== GRIDS & INLINE ROWS =====
  fourColumnGrid: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    gap: 8,
  },
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
    minWidth: 240,
    flexDirection: 'row',
    alignItems: 'center',
  },
  inlineFieldLabel: {
    width: 95,
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
    borderTopRightRadius: 4,
    borderBottomRightRadius: 4,
    justifyContent: 'center',
    alignItems: 'center',
    flexShrink: 0,
  },

  // Class and Rates Row
  classAndRatesRow: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    flexWrap: 'wrap',
    gap: 10,
  },
  classPillsWrapper: {
    flexDirection: 'row',
    alignItems: 'center',
    flexWrap: 'wrap',
    gap: 6,
    flex: 2,
  },
  classPillsGroup: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    gap: 5,
    flex: 1,
  },
  classPill: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FCFAF6',
    borderWidth: 1,
    borderColor: '#E2D3B3',
    borderRadius: 4,
    paddingVertical: 3,
    paddingHorizontal: 6,
    gap: 3,
  },
  classPillSelected: {
    backgroundColor: '#FFF4DA',
    borderColor: '#B8903B',
  },
  classPillText: {
    fontSize: 11,
    fontWeight: '500',
    color: '#5C4A28',
  },
  classPillTextSelected: {
    fontWeight: 'bold',
    color: '#9E6D08',
  },
  ratesInputGroup: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 8,
    flexWrap: 'wrap',
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
  classTag: {
    backgroundColor: '#FFF1D6',
    borderWidth: 1,
    borderColor: '#D4C3A3',
    borderRadius: 3,
    paddingHorizontal: 6,
    paddingVertical: 1,
  },
  classTagText: {
    fontSize: 10,
    fontWeight: 'bold',
    color: '#7D5E19',
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

  // Item Entry Bar
  itemEntryBar: {
    borderTopWidth: 1,
    borderTopColor: '#E8DCB8',
    backgroundColor: '#FCFAF6',
    padding: 10,
  },
  tableActionBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 12,
    paddingVertical: 5,
    borderRadius: 5,
    borderWidth: 1,
  },
  addBtn: {
    backgroundColor: '#F9F2E2',
    borderColor: '#B8903B',
  },
  addBtnText: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#9E6D08',
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
