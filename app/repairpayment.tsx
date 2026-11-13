import { ThemedText } from '@/components/themed-text';
import { API_BASE_URL } from '@/constants/api';
import { useThemeColors } from '@/hooks/useThemeColors';
import { UNIPRO_LOGO_DATA_URI } from '@/utils/uniproLogo';
import { Ionicons } from '@expo/vector-icons';
import { Stack, useLocalSearchParams, useRouter } from 'expo-router';
import React, { useEffect, useRef, useState } from 'react';
import {
  ActivityIndicator,
  Alert,
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

const API_URL = `${API_BASE_URL}/repairpayment`;

export default function RepairPaymentScreen() {
  const themeColors = useThemeColors();
  const router = useRouter();
  const params = useLocalSearchParams();

  const [loading, setLoading] = useState(false);
  const [saving, setSaving] = useState(false);
  const [toast, setToast] = useState<{ message: string; type: 'success' | 'error' } | null>(null);

  const repairNoInputRef = useRef<any>(null);

  // Form State
  const [repairNo, setRepairNo] = useState('');
  const [customerCode, setCustomerCode] = useState('');
  const [customerName, setCustomerName] = useState('');
  const [address, setAddress] = useState('');
  const [customerType, setCustomerType] = useState('');
  const [remarks, setRemarks] = useState('');

  // Payment State
  const [amountPaid, setAmountPaid] = useState('0.00');

  // Footer Totals
  const [estimatedCost, setEstimatedCost] = useState('0.00');
  const [advancePaid, setAdvancePaid] = useState('0.00');
  const [balanceAmount, setBalanceAmount] = useState('0.00');

  // Trade-in
  const [tradeInOption, setTradeInOption] = useState<'Gold' | 'Cash' | 'Both'>('Gold');

  // History
  const [historyList, setHistoryList] = useState<any[]>([]);

  // RePrint Modal State
  const [isReprintModalVisible, setIsReprintModalVisible] = useState(false);
  const [reprintRepairNo, setReprintRepairNo] = useState('');
  const [isRepairModalVisible, setIsRepairModalVisible] = useState(false);
  const [repairList, setRepairList] = useState<any[]>([]);
  const [repairSearchQuery, setRepairSearchQuery] = useState('');

  const showToast = (message: string, type: 'success' | 'error' = 'success') => {
    setToast({ message, type });
    setTimeout(() => setToast(null), 3000);
  };

  const showAlert = (title: string, message: string) => {
    if (Platform.OS === 'web') {
      window.alert(`${title}: ${message}`);
    } else {
      Alert.alert(title, message);
    }
  };

  const fetchBalance = async (repNo: string) => {
    if (!repNo) return;
    try {
      setLoading(true);
      const res = await fetch(`${API_URL}/balance/${encodeURIComponent(repNo.trim())}`);
      const json = await res.json();
      if (json.success && json.data) {
        const { repair, balance, history } = json.data;
        setEstimatedCost(balance.estimatedCost ? balance.estimatedCost.toFixed(2) : '0.00');
        setAdvancePaid(balance.advancePaid ? balance.advancePaid.toFixed(2) : '0.00');
        setBalanceAmount(balance.balanceAmount > 0 ? balance.balanceAmount.toFixed(2) : '0.00');
        setHistoryList(history || []);

        if (repair) {
          setCustomerCode(repair.CustomerCode ? repair.CustomerCode.trim() : '');
          setCustomerName(repair.CustomerName ? repair.CustomerName.trim() : '');
          setAddress(`${repair.Address1 || ''} ${repair.Address2 || ''} ${repair.Address3 || ''}`.trim());
          setCustomerType(repair.CustomerType ? repair.CustomerType.trim() : '');
        }
        showToast('Repair balance & history loaded', 'success');
      } else {
        showToast(json.message || 'Repair not found', 'error');
        handleClearCustomerData();
      }
    } catch (err) {
      console.error(err);
      showToast('Error fetching repair balance', 'error');
    } finally {
      setLoading(false);
    }
  };

  const handleClearCustomerData = () => {
    setCustomerCode('');
    setCustomerName('');
    setAddress('');
    setCustomerType('');
    setEstimatedCost('0.00');
    setAdvancePaid('0.00');
    setBalanceAmount('0.00');
    setHistoryList([]);
  };

  // Handle return from payingmode after save
  useEffect(() => {
    if (params?.paymentSuccess === 'true') {
      const savedNo = (params.repairNo as string) || (params.savedInvoiceNo as string) || repairNo;
      if (savedNo && savedNo.trim()) {
        const cleanNo = savedNo.trim();
        subPrintRepair(cleanNo, false);
        fetchBalance(cleanNo);
      }
    }
  }, [params?.paymentSuccess]);

  // Original subPrintRepair implementation (matching VB.NET subPrintRepair in frmRepairPayment)
  // Crystal Report: rptRepairInvoice_AJ.rpt
  // Selection: {tblInventoryRepairHeader.RepairNo} = sInvoiceNo
  // Copies: 2
  const subPrintRepair = async (targetInvoiceNo: string, isReprintMode: boolean = false) => {
    const sInvoiceNo = (targetInvoiceNo || '').trim();
    if (!sInvoiceNo) {
      showToast('Please enter OrderNo.', 'error');
      repairNoInputRef.current?.focus?.();
      return;
    }

    try {
      setLoading(true);

      let company: any = { name: '', address1: '', address2: '', country: '', phone: '', fax: '' };
      let header: any = null;
      let details: any[] = [];
      let payments: any[] = [];
      let rates: any = { rate999: '', rate916: '', rate835: '' };

      // 1. Fetch actual data from backend SQL Server
      const res = await fetch(`${API_URL}/print-data?repairNo=${encodeURIComponent(sInvoiceNo)}`);
      const json = await res.json();

      if (json.success && json.data) {
        company = json.data.company || company;
        header = json.data.header || {};
        details = json.data.details || [];
        payments = json.data.payments || [];
        rates = json.data.rates || rates;
      } else {
        showToast(json.message || `Repair docket ${sInvoiceNo} not found for print.`, 'error');
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

      const sCompanyname = (company.name || 'SMART GOLD').trim();
      const sAddress = [company.address1, company.address2, company.country].filter(Boolean).join(' ');
      const sPhone = (company.phone || '').trim();
      const sFax = (company.fax || '').trim();
      const isDuplicateCopy = Boolean(isReprintMode && company.companyShortCode === 'HN');
      const sReportTitle = isDuplicateCopy ? 'Duplicate Copy' : 'REPAIR INVOICE / REPAIR DOCKET';

      // Customer info mapping (formula fields: Customer1, Customer2, Customer3)
      const sCustomerName = (header.CustomerName || customerName || '').trim();
      const sCustomerCode = (header.CustomerCode || customerCode || '').trim();
      const sCustomerAddress1 = (header.CustomerAddress1 || header.Address1 || '').trim();
      const sCustomerAddress2 = (header.CustomerAddress2 || header.Address2 || '').trim();
      const sCustomerAddress3 = (header.CustomerAddress3 || header.Address3 || '').trim();
      const sCustomerPhone = (header.CustomerPhone || header.Phone || '').trim();

      // Financial figures
      const totalEstimatedCost = parseFloat(header.TotalCostEstimated ?? estimatedCost) || 0;
      const advancePaidAmt = parseFloat(header.AdvancePaid ?? advancePaid) || 0;

      // Calculate total paid across payment history
      let totalInstallmentsPaid = 0;
      payments.forEach((p: any) => {
        totalInstallmentsPaid += parseFloat(p.AmountPaid) || 0;
      });
      const grandTotalPaid = advancePaidAmt + totalInstallmentsPaid;
      const balanceDue = totalEstimatedCost - grandTotalPaid;

      // Format date helper
      const formatDate = (val: any) => {
        if (!val) return '-';
        const d = new Date(val);
        if (isNaN(d.getTime())) return String(val).split('T')[0] || '-';
        return d.toLocaleDateString('en-GB', { day: '2-digit', month: '2-digit', year: 'numeric' });
      };

      const repairDateFormatted = formatDate(header.RepairDate || new Date());
      const deliveryDateFormatted = formatDate(header.DeliveryDate || header.DateOfDelivery);

      // Build Details Table Rows
      let detailRowsHtml = '';
      if (details.length > 0) {
        details.forEach((item: any, idx: number) => {
          const itemNo = item.LineNo || item.OrderInventoryCode || idx + 1;
          const desc = item.Description || item.RepairDescription || '-';
          const weight = parseFloat(item.WeightEstimated || item.Weight || 0).toFixed(3);
          const cost = parseFloat(item.EstimatedCost || item.Cost || item.TotalCost || 0).toFixed(2);
          detailRowsHtml += `
            <tr>
              <td style="text-align: center;">${itemNo}</td>
              <td style="text-align: left; font-weight: 600;">${desc}</td>
              <td style="text-align: right;">${weight}</td>
              <td style="text-align: right; font-weight: 700;">$${cost}</td>
            </tr>
          `;
        });
      } else {
        detailRowsHtml = `
          <tr>
            <td style="text-align: center;">1</td>
            <td style="text-align: left; font-weight: 600;">REPAIR / SERVICE CHARGES</td>
            <td style="text-align: right;">0.000</td>
            <td style="text-align: right; font-weight: 700;">$${totalEstimatedCost.toFixed(2)}</td>
          </tr>
        `;
      }

      // Build Payment History Breakdown
      let paymentHistoryHtml = '';
      if (payments.length > 0) {
        let pRows = '';
        payments.forEach((p: any, pIdx: number) => {
          const pNo = p.RepairPaymentNo || `P-${pIdx + 1}`;
          const pDate = formatDate(p.CreateDate);
          const pAmt = parseFloat(p.AmountPaid || 0).toFixed(2);
          const pRem = p.Remarks || 'REPAIR PAYMENT';
          pRows += `
            <tr>
              <td style="text-align: center;">${pNo}</td>
              <td style="text-align: center;">${pDate}</td>
              <td style="text-align: left;">${pRem}</td>
              <td style="text-align: right; font-weight: 600;">$${pAmt}</td>
            </tr>
          `;
        });
        paymentHistoryHtml = `
          <div style="margin-top: 10px;">
            <div style="font-size: 11px; font-weight: 700; color: #5c4a28; text-transform: uppercase; margin-bottom: 3px; letter-spacing: 0.5px;">
              Settlement / Payment Transactions:
            </div>
            <table class="report-table" style="font-size: 10.5px;">
              <thead>
                <tr>
                  <th style="width: 25%; text-align: center;">Receipt / Payment No</th>
                  <th style="width: 20%; text-align: center;">Date</th>
                  <th style="width: 35%; text-align: left;">Remarks</th>
                  <th style="width: 20%; text-align: right;">Amount Paid</th>
                </tr>
              </thead>
              <tbody>
                ${pRows}
              </tbody>
            </table>
          </div>
        `;
      }

      // Single copy page markup generator
      const generatePageMarkup = (copyLabel: string) => `
        <div class="page-sheet">
          <!-- TOP HEADER SECTION -->
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
              <div class="page-num-text">PAGE : 1</div>
              <div class="copy-tag">${copyLabel}</div>
            </div>
          </div>

          <!-- REPORT TITLE BAR -->
          <div class="title-bar">
            <span class="report-title-text">${sReportTitle}</span>
          </div>

          <!-- PRINT TIMESTAMP -->
          <div class="timestamp-row">
            <span>Printed: ${sgDateStr}, ${sgTimeStr}</span>
          </div>

          <!-- DOCKET METADATA SECTION -->
          <div class="meta-section">
            <table class="meta-table">
              <tr>
                <td class="meta-lbl" style="width: 15%;">Repair No</td>
                <td class="meta-val" style="width: 35%; font-weight: 700; color: #996515;">: &nbsp; ${sInvoiceNo}</td>
                <td class="meta-lbl" style="width: 16%;">Repair Date</td>
                <td class="meta-val" style="width: 34%;">: &nbsp; ${repairDateFormatted}</td>
              </tr>
              <tr>
                <td class="meta-lbl">Cust Code</td>
                <td class="meta-val">: &nbsp; ${sCustomerCode || '-'}</td>
                <td class="meta-lbl">Target Delivery</td>
                <td class="meta-val">: &nbsp; ${deliveryDateFormatted}</td>
              </tr>
              <tr>
                <td class="meta-lbl">Customer Name</td>
                <td class="meta-val" style="font-weight: 700;">: &nbsp; ${sCustomerName || '-'}</td>
                <td class="meta-lbl">Customer Phone</td>
                <td class="meta-val">: &nbsp; ${sCustomerPhone || '-'}</td>
              </tr>
              <tr>
                <td class="meta-lbl">Customer Address</td>
                <td class="meta-val" colspan="3">: &nbsp; ${[sCustomerAddress1, sCustomerAddress2, sCustomerAddress3].filter(Boolean).join(', ') || '-'}</td>
              </tr>
              ${header.Remarks ? `
              <tr>
                <td class="meta-lbl">Remarks</td>
                <td class="meta-val" colspan="3">: &nbsp; ${header.Remarks}</td>
              </tr>
              ` : ''}
            </table>
          </div>

          <!-- REPAIR ITEMS TABLE -->
          <div class="table-container">
            <table class="report-table">
              <thead>
                <tr>
                  <th style="width: 10%; text-align: center;">Item #</th>
                  <th style="width: 55%; text-align: left;">Item Description / Service Work</th>
                  <th style="width: 15%; text-align: right;">Weight (g)</th>
                  <th style="width: 20%; text-align: right;">Estimated Cost</th>
                </tr>
              </thead>
              <tbody>
                ${detailRowsHtml}
              </tbody>
            </table>
          </div>

          <!-- SETTLEMENT SUMMARY & FINANCIALS -->
          <div style="display: flex; gap: 16px; margin-top: 10px; align-items: flex-start;">
            <div style="flex: 1.2;">
              ${paymentHistoryHtml}
            </div>

            <div style="flex: 1; border: 1px solid #c9b084; background: #fffdf8; padding: 8px 12px; border-radius: 4px;">
              <div style="font-size: 11px; font-weight: 700; color: #5c4a28; text-transform: uppercase; margin-bottom: 6px; letter-spacing: 0.5px; border-bottom: 1px solid #e0d0b0; padding-bottom: 3px;">
                Financial Settlement Summary
              </div>
              <table style="width: 100%; font-size: 11px; font-family: Cambria, Georgia, serif;">
                <tr>
                  <td style="padding: 2px 0; color: #4a3b22;">Total Estimated Cost:</td>
                  <td style="padding: 2px 0; text-align: right; font-weight: 700;">$${totalEstimatedCost.toFixed(2)}</td>
                </tr>
                <tr>
                  <td style="padding: 2px 0; color: #4a3b22;">Advance Paid:</td>
                  <td style="padding: 2px 0; text-align: right; color: #2e7d32; font-weight: 600;">$${advancePaidAmt.toFixed(2)}</td>
                </tr>
                ${totalInstallmentsPaid > 0 ? `
                <tr>
                  <td style="padding: 2px 0; color: #4a3b22;">Subsequent Payments:</td>
                  <td style="padding: 2px 0; text-align: right; color: #2e7d32; font-weight: 600;">$${totalInstallmentsPaid.toFixed(2)}</td>
                </tr>
                ` : ''}
                <tr style="border-top: 1px dashed #c9b084;">
                  <td style="padding: 4px 0 2px; font-weight: 700; color: #3b2d17;">Total Amount Paid:</td>
                  <td style="padding: 4px 0 2px; text-align: right; font-weight: 700; color: #2e7d32;">$${grandTotalPaid.toFixed(2)}</td>
                </tr>
                <tr style="border-top: 1px solid #996515;">
                  <td style="padding: 4px 0 2px; font-weight: 700; color: #996515; font-size: 12px;">Balance Due:</td>
                  <td style="padding: 4px 0 2px; text-align: right; font-weight: 700; color: ${balanceDue > 0 ? '#b71c1c' : '#2e7d32'}; font-size: 12px;">$${(balanceDue > 0 ? balanceDue : 0).toFixed(2)}</td>
                </tr>
              </table>
            </div>
          </div>

          <!-- SIGNATURE BOXES (Customer & Authorized Store Officer) -->
          <div class="sig-section">
            <div class="sig-box">
              <div class="sig-line"></div>
              <div class="sig-label">Customer Signature</div>
            </div>

            <div class="sig-box">
              <div class="sig-line"></div>
              <div class="sig-label">Authorized Signature / Store Officer</div>
            </div>
          </div>

          <!-- PINNED FOOTER -->
          <div class="footer-pinned">
            <div class="footer-rule"></div>
            <div class="footer-gen-text">*** System Generated Report ***</div>
            <div class="footer-brand-text">Powered by Unipro SG</div>
          </div>
        </div>
      `;

      // Full HTML with 2 copies (matching CR.PrintToPrinter(2, True, 0, 0))
      const printHtml = `
        <!DOCTYPE html>
        <html>
          <head>
            <meta charset="utf-8" />
            <title>${sReportTitle} - ${sInvoiceNo}</title>
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
                page-break-after: always;
              }
              .page-sheet:last-child {
                page-break-after: auto;
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
                font-size: 24px;
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
                line-height: 1.3;
              }
              .header-right {
                width: 140px;
                text-align: right;
                display: flex;
                flex-direction: column;
                justify-content: center;
                align-items: flex-end;
              }
              .page-num-text {
                font-size: 12px;
                font-weight: 700;
                color: #3b2d17;
                letter-spacing: 0.5px;
              }
              .copy-tag {
                display: inline-block;
                margin-top: 4px;
                padding: 2px 8px;
                background-color: #f6ecdc;
                border: 1px solid #d4af37;
                border-radius: 3px;
                font-size: 10px;
                font-weight: 700;
                color: #7d5e19;
                letter-spacing: 0.8px;
                text-transform: uppercase;
              }

              /* TITLE BAR */
              .title-bar {
                text-align: center;
                margin: 6px 0 2px;
                padding: 3px 0;
              }
              .report-title-text {
                font-family: Cambria, Georgia, "Times New Roman", serif !important;
                font-size: 14px;
                font-weight: 700;
                color: #5c4a28;
                letter-spacing: 1.5px;
                text-decoration: underline;
                text-underline-offset: 3px;
                text-transform: uppercase;
              }

              /* PRINT TIMESTAMP */
              .timestamp-row {
                text-align: right;
                font-size: 10px;
                color: #6d5b3d;
                font-style: italic;
                margin-bottom: 6px;
              }

              /* METADATA CARD */
              .meta-section {
                border: 1px solid #bfa573;
                border-radius: 4px;
                background-color: #fffefb;
                padding: 6px 10px;
                margin-bottom: 10px;
              }
              .meta-table {
                width: 100%;
                border-collapse: collapse;
              }
              .meta-table td {
                padding: 2px 4px;
                font-size: 11.5px;
                vertical-align: top;
                line-height: 1.3;
              }
              .meta-lbl {
                font-weight: 700;
                color: #4a3b22;
              }
              .meta-val {
                color: #2b1f0e;
              }

              /* TABLE CONTAINER */
              .table-container {
                margin-bottom: 6px;
              }
              table.report-table {
                width: 100%;
                border-collapse: collapse;
                border: 1px solid #bfa573;
              }
              table.report-table th {
                background-color: #f6ecdc;
                color: #4a3b22;
                font-weight: 700;
                font-size: 11px;
                padding: 5px 6px;
                border: 1px solid #bfa573;
                text-transform: uppercase;
                letter-spacing: 0.5px;
              }
              table.report-table td {
                padding: 5px 6px;
                font-size: 11px;
                border: 1px solid #e8dcb8;
                color: #2b1f0e;
                vertical-align: middle;
              }
              table.report-table tbody tr:nth-child(even) {
                background-color: #fffdf9;
              }

              /* SIGNATURE SECTION */
              .sig-section {
                margin-top: 30px;
                display: flex;
                justify-content: space-between;
                padding: 0 20px;
              }
              .sig-box {
                width: 220px;
                text-align: center;
              }
              .sig-line {
                border-bottom: 1px dotted #5c4a28;
                height: 36px;
                margin-bottom: 4px;
              }
              .sig-label {
                font-size: 11px;
                font-weight: 700;
                color: #4a3b22;
                text-transform: uppercase;
                letter-spacing: 0.5px;
              }

              /* PINNED FOOTER */
              .footer-pinned {
                margin-top: auto;
                padding-top: 10px;
                text-align: center;
              }
              .footer-rule {
                border-top: 1px solid #c9b084;
                margin-bottom: 4px;
              }
              .footer-gen-text {
                font-size: 10px;
                color: #7d6847;
                letter-spacing: 1px;
                font-style: italic;
              }
              .footer-brand-text {
                font-size: 9px;
                color: #9e8d72;
                letter-spacing: 0.5px;
                margin-top: 1px;
              }

              /* PRINT OPTIMIZATION */
              @media print {
                body {
                  background: none !important;
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
                  page-break-after: always !important;
                }
                .page-sheet:last-child {
                  page-break-after: auto !important;
                }
              }
            </style>
          </head>
          <body>
            ${generatePageMarkup('CUSTOMER COPY')}
            ${generatePageMarkup('OFFICE COPY')}
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
          showToast('Repair Invoice report preview opened', 'success');
        } else {
          const blob = new Blob([printHtml], { type: 'text/html;charset=utf-8;' });
          const url = URL.createObjectURL(blob);
          const win = window.open(url, '_blank');
          if (!win) {
            showToast('Popup blocker prevented opening print preview. Please allow popups.', 'error');
          } else {
            showToast('Repair Invoice report preview opened', 'success');
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

  // RePrint button click handler (matching frmRepairPayment btnPrint_Click)
  // If txtOrderNo.Text.Trim = "" Then MsgBox("Please enter OrderNo.") : txtOrderNo.Focus() : Exit Sub
  // If Trim(txtOrderNo.Text) <> "" Then Call subPrintRepair(Trim(txtOrderNo.Text))
  const handlePrintClick = () => {
    const cleanNo = (repairNo || '').trim();
    if (!cleanNo) {
      showToast('Please enter OrderNo.', 'error');
      repairNoInputRef.current?.focus?.();
      return;
    }
    subPrintRepair(cleanNo, true);
  };

  // Modal RePrint submit handler (if user enters repair no into modal)
  const handleReprintSubmit = async () => {
    const targetNo = (reprintRepairNo || repairNo || '').trim();
    if (!targetNo) {
      showToast('Please enter OrderNo.', 'error');
      return;
    }
    setIsReprintModalVisible(false);
    setReprintRepairNo('');
    subPrintRepair(targetNo, true);
  };

  const handleOpenRepairLookup = async () => {
    setLoading(true);
    try {
      const res = await fetch(`${API_URL}/repairs`);
      const json = await res.json();
      if (json.success) {
        setRepairList(json.data || []);
        setIsRepairModalVisible(true);
      } else {
        showToast('Failed to load repairs', 'error');
      }
    } catch (err) {
      showToast('Error querying repairs', 'error');
    } finally {
      setLoading(false);
    }
  };

  const handleSelectRepair = (rep: any) => {
    const selectedNo = (rep.RepairNo || '').trim();
    setRepairNo(selectedNo);
    setIsRepairModalVisible(false);
    if (selectedNo) {
      fetchBalance(selectedNo);
    }
  };

  const handleClear = () => {
    setRepairNo('');
    setRemarks('');
    setAmountPaid('0.00');
    handleClearCustomerData();
  };

  const handleCancelPayment = async (paymentNo: string) => {
    if (!repairNo) return;
    Alert.alert('Confirm Cancel', `Are you sure you want to cancel payment ${paymentNo}?`, [
      { text: 'No', style: 'cancel' },
      {
        text: 'Yes',
        onPress: async () => {
          setLoading(true);
          try {
            const res = await fetch(`${API_URL}/pay/${encodeURIComponent(repairNo)}/${encodeURIComponent(paymentNo)}`, {
              method: 'DELETE',
            });
            const json = await res.json();
            if (json.success) {
              showToast('Payment Cancelled Successfully', 'success');
              fetchBalance(repairNo);
            } else {
              showToast(json.message || 'Failed to cancel', 'error');
            }
          } catch (err) {
            showToast('Network error while cancelling payment', 'error');
          } finally {
            setLoading(false);
          }
        },
      },
    ]);
  };

  const latestState = useRef({ repairNo, amountPaid });
  useEffect(() => {
    latestState.current = { repairNo, amountPaid };
  }, [repairNo, amountPaid]);

  const handleSave = async () => {
    const currentState = latestState.current;
    if (!currentState.repairNo.trim()) {
      showToast('Please enter OrderNo.', 'error');
      return;
    }
    const tot = parseFloat(currentState.amountPaid) || 0;
    if (tot === 0) {
      showToast('Please enter pay amount.', 'error');
      return;
    }
    if (isNaN(tot)) {
      showToast('Please enter valid Pay Amount.', 'error');
      return;
    }
    if (tot < 0) {
      showToast('Pay amount should be greater than zero.', 'error');
      return;
    }

    setSaving(true);
    try {
      // 1. Fetch next payment no
      const noRes = await fetch(`${API_URL}/next-no`);
      const noJson = await noRes.json();
      const repairPaymentNo = noJson.success ? noJson.paymentNo : `RP${Date.now()}`;

      // 2. Prepare payload for paying mode tender
      const payload = {
        repairNo: currentState.repairNo.trim(),
        repairPaymentNo,
        amountPaid: currentState.amountPaid,
        remarks: remarks.trim() || 'REPAIR PAYMENT',
        locationCode: 'HQ',
        createUser: 'ADMIN'
      };

      // Navigate to payingmode screen to allocate payment across cash/card/transfer
      router.push({
        pathname: '/payingmode',
        params: {
          totalNet: currentState.amountPaid,
          customerCode: customerCode,
          customerType: customerType || 'NORMAL',
          apiEndpoint: `${API_URL}/pay`,
          payload: JSON.stringify(payload),
          returnPath: '/repairpayment',
        },
      });

      setAmountPaid('0.00');
    } catch (err: any) {
      showToast(err.message || 'Error opening payment mode', 'error');
    } finally {
      setSaving(false);
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
            <ThemedText style={styles.headerTitle}>Jewellery Repair Payment</ThemedText>
            <ThemedText style={styles.headerSubtitle}>Repair Advance & Settlement Processing</ThemedText>
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
            <ThemedText style={styles.clearBtnText}>RePrint</ThemedText>
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
                <ThemedText style={styles.saveBtnText}>Save</ThemedText>
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
          {/* CARD 1: REPAIR IDENTIFICATION & CUSTOMER DETAILS */}
          <View style={styles.goldCard}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="construct-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitleSmall}>Repair Identification</ThemedText>
              </View>

              {repairNo ? (
                <View style={styles.repairBadge}>
                  <ThemedText style={styles.repairBadgeLabel}>Repair:</ThemedText>
                  <ThemedText style={styles.repairBadgeValue}>{repairNo}</ThemedText>
                </View>
              ) : null}
            </View>

            <View style={styles.cardBody}>
              {/* Row 1: Repair No, Customer Code, Customer Type, Customer Name */}
              <View style={styles.fourColumnGrid}>
                {/* Repair No */}
                <View style={[styles.inlineFieldRow, { flex: 1.2, minWidth: 260 }]}>
                  <ThemedText style={styles.inlineFieldLabel}>Repair No *</ThemedText>
                  <View style={styles.inputWithLookup}>
                    <TextInput
                      ref={repairNoInputRef}
                      style={[styles.formInput, styles.inputAttached, { flex: 1 }]}
                      value={repairNo}
                      onChangeText={setRepairNo}
                      onSubmitEditing={() => repairNo.trim() && fetchBalance(repairNo.trim())}
                      placeholder="Search/Enter Repair No..."
                      placeholderTextColor="#9E8D77"
                    />
                    <TouchableOpacity
                      style={styles.attachedLookupBtn}
                      onPress={handleOpenRepairLookup}
                      activeOpacity={0.7}
                    >
                      <Ionicons name="search" size={13} color="#7D5E19" />
                    </TouchableOpacity>
                  </View>
                </View>

                {/* Customer Code */}
                <View style={styles.inlineFieldRow}>
                  <ThemedText style={styles.inlineFieldLabel}>Cust Code</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1 }]}
                    value={customerCode}
                    editable={false}
                    placeholder="Auto-populated"
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* Customer Type */}
                <View style={styles.inlineFieldRow}>
                  <ThemedText style={styles.inlineFieldLabel}>Cust Type</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1 }]}
                    value={customerType}
                    editable={false}
                    placeholder="Auto-populated"
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* Customer Name */}
                <View style={styles.inlineFieldRow}>
                  <ThemedText style={styles.inlineFieldLabel}>Cust Name</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1 }]}
                    value={customerName}
                    editable={false}
                    placeholder="Auto-populated"
                    placeholderTextColor="#9E8D77"
                  />
                </View>
              </View>

              {/* Row 2: Address, Trade-In */}
              <View style={[styles.threeColumnGrid, { marginTop: 6 }]}>
                {/* Address */}
                <View style={[styles.inlineFieldRow, { flex: 2, minWidth: 280 }]}>
                  <ThemedText style={styles.inlineFieldLabel}>Address</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1 }]}
                    value={address}
                    editable={false}
                    placeholder="Auto-populated address"
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* Trade-In Options */}
                <View style={[styles.inlineFieldRow, { flex: 1.5, minWidth: 260 }]}>
                  <ThemedText style={styles.inlineFieldLabel}>Trade-In</ThemedText>
                  <View style={styles.tradeInOptionsGroup}>
                    {['Gold', 'Cash', 'Both'].map((opt) => (
                      <TouchableOpacity
                        key={opt}
                        style={[styles.pillOption, tradeInOption === opt && styles.pillOptionSelected]}
                        onPress={() => setTradeInOption(opt as any)}
                        activeOpacity={0.7}
                      >
                        <Ionicons
                          name={tradeInOption === opt ? 'radio-button-on' : 'radio-button-off'}
                          size={13}
                          color={tradeInOption === opt ? '#9E6D08' : '#7A6855'}
                        />
                        <ThemedText
                          style={[styles.pillOptionText, tradeInOption === opt && styles.pillOptionTextSelected]}
                        >
                          {opt}
                        </ThemedText>
                      </TouchableOpacity>
                    ))}

                    {tradeInOption !== 'Cash' && (
                      <TouchableOpacity
                        style={styles.tradeInGoBtn}
                        onPress={() => router.push('/tradeinsales')}
                        activeOpacity={0.8}
                      >
                        <ThemedText style={styles.tradeInGoBtnText}>Go</ThemedText>
                        <Ionicons name="arrow-forward" size={11} color="#9E6D08" />
                      </TouchableOpacity>
                    )}
                  </View>
                </View>
              </View>

              {/* Row 3: Pay Amount, Remarks */}
              <View style={[styles.twoColumnGrid, { marginTop: 6 }]}>
                {/* Pay Amount */}
                <View style={[styles.inlineFieldRow, { flex: 1, minWidth: 260 }]}>
                  <ThemedText style={styles.inlineFieldLabel}>Pay Amount ($) *</ThemedText>
                  <TextInput
                    style={[
                      styles.formInput,
                      styles.highlightTotal,
                      { flex: 1, textAlign: 'right' },
                    ]}
                    value={amountPaid}
                    onChangeText={setAmountPaid}
                    keyboardType="numeric"
                    placeholder="0.00"
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* Remarks */}
                <View style={[styles.inlineFieldRow, { flex: 2, minWidth: 280 }]}>
                  <ThemedText style={styles.inlineFieldLabel}>Remarks</ThemedText>
                  <TextInput
                    style={[styles.formInput, { flex: 1 }]}
                    value={remarks}
                    onChangeText={setRemarks}
                    placeholder="Repair payment notes..."
                    placeholderTextColor="#9E8D77"
                  />
                </View>
              </View>
            </View>
          </View>

          {/* CARD 2: REPAIR PAYMENT HISTORY TABLE (100% FULL WIDTH) */}
          <View style={[styles.goldCard, { marginTop: 10 }]}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="time-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitleSmall}>Repair Payment History</ThemedText>
                <View style={styles.counterBadge}>
                  <ThemedText style={styles.counterBadgeText}>{historyList.length} records</ThemedText>
                </View>
              </View>
            </View>

            {/* Table Header (100% Full Width) */}
            <View style={styles.tableHeadRow}>
              <ThemedText style={[styles.tableHeadCell, { flex: 1.5 }]}>Repair No</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 1.8 }]}>Payment No</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 2.2 }]}>Remarks</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 1.4, textAlign: 'right' }]}>
                Amt Paid ($)
              </ThemedText>
              <ThemedText style={[styles.tableHeadCell, { width: 70, textAlign: 'center' }]}>Action</ThemedText>
            </View>

            {/* Table Body */}
            {historyList.length === 0 ? (
              <View style={styles.emptyTableContainer}>
                <Ionicons name="receipt-outline" size={28} color="#D4C3A3" />
                <ThemedText style={styles.emptyTableText}>
                  No payment history recorded for this repair docket.
                </ThemedText>
              </View>
            ) : (
              historyList.map((item, index) => (
                <View key={index} style={styles.tableRow}>
                  <ThemedText style={[styles.tableCell, styles.codeCell, { flex: 1.5 }]}>
                    {item.RepairNo}
                  </ThemedText>
                  <ThemedText style={[styles.tableCell, styles.codeCell, { flex: 1.8 }]}>
                    {item.RepairPaymentNo}
                  </ThemedText>
                  <ThemedText style={[styles.tableCell, { flex: 2.2 }]} numberOfLines={1}>
                    {item.Remarks || '-'}
                  </ThemedText>
                  <ThemedText style={[styles.tableCell, styles.amountCell, { flex: 1.4, textAlign: 'right' }]}>
                    {parseFloat(item.AmountPaid || 0).toFixed(2)}
                  </ThemedText>
                  <View style={{ width: 70, alignItems: 'center' }}>
                    <TouchableOpacity
                      style={styles.cancelRowBtn}
                      onPress={() => handleCancelPayment(item.RepairPaymentNo)}
                      activeOpacity={0.7}
                    >
                      <ThemedText style={styles.cancelRowBtnText}>Cancel</ThemedText>
                    </TouchableOpacity>
                  </View>
                </View>
              ))
            )}
          </View>

          {/* CARD 3: REPAIR FINANCIAL SUMMARY */}
          <View style={[styles.goldCard, { marginTop: 10 }]}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="calculator-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitleSmall}>Repair Financial Summary</ThemedText>
              </View>
            </View>

            <View style={styles.cardBody}>
              <View style={styles.threeColumnGrid}>
                {/* Estimated Cost */}
                <View style={styles.inlineFieldRow}>
                  <ThemedText style={styles.inlineFieldLabel}>Estimated Cost</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1, textAlign: 'right' }]}
                    value={estimatedCost}
                    editable={false}
                  />
                </View>

                {/* Advance Paid */}
                <View style={styles.inlineFieldRow}>
                  <ThemedText style={styles.inlineFieldLabel}>Advance Paid</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1, textAlign: 'right' }]}
                    value={advancePaid}
                    editable={false}
                  />
                </View>

                {/* Balance Amount */}
                <View style={styles.inlineFieldRow}>
                  <ThemedText style={styles.inlineFieldLabel}>Balance Amount</ThemedText>
                  <TextInput
                    style={[
                      styles.formInput,
                      styles.readonlyInput,
                      styles.highlightTotal,
                      { flex: 1, textAlign: 'right' },
                    ]}
                    value={balanceAmount}
                    editable={false}
                  />
                </View>
              </View>
            </View>
          </View>
        </ScrollView>
      </KeyboardAvoidingView>

      {/* REPAIR LOOKUP MODAL */}
      <Modal visible={isRepairModalVisible} transparent animationType="fade">
        <View style={styles.modalOverlay}>
          <View style={styles.lovModalCard}>
            <View style={styles.modalHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="search" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.modalHeaderTitle}>Search Repair Order</ThemedText>
              </View>
              <TouchableOpacity
                onPress={() => setIsRepairModalVisible(false)}
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
                value={repairSearchQuery}
                onChangeText={setRepairSearchQuery}
                placeholder="Search by Repair No or Customer Code..."
                placeholderTextColor="#9E8D77"
                autoFocus
              />
              {repairSearchQuery.length > 0 && (
                <TouchableOpacity onPress={() => setRepairSearchQuery('')}>
                  <Ionicons name="close-circle" size={16} color="#9E8D77" />
                </TouchableOpacity>
              )}
            </View>

            <View style={styles.lovHeadRow}>
              <ThemedText style={[styles.lovHeadText, { flex: 1.4 }]}>Repair No</ThemedText>
              <ThemedText style={[styles.lovHeadText, { flex: 1.1 }]}>Date</ThemedText>
              <ThemedText style={[styles.lovHeadText, { flex: 1.3 }]}>Customer</ThemedText>
              <ThemedText style={[styles.lovHeadText, { flex: 1.0, textAlign: 'right' }]}>Est Cost</ThemedText>
              <ThemedText style={[styles.lovHeadText, { flex: 1.0, textAlign: 'right' }]}>Advance</ThemedText>
              <ThemedText style={[styles.lovHeadText, { flex: 1.0, textAlign: 'right' }]}>Balance</ThemedText>
            </View>

            <FlatList
              data={repairList.filter(
                (o) =>
                  (o.RepairNo || '').toLowerCase().includes(repairSearchQuery.toLowerCase()) ||
                  (o.CustomerCode || '').toLowerCase().includes(repairSearchQuery.toLowerCase()) ||
                  (o.CustomerName || '').toLowerCase().includes(repairSearchQuery.toLowerCase())
              )}
              keyExtractor={(item, index) => item.RepairNo || index.toString()}
              initialNumToRender={50}
              maxToRenderPerBatch={50}
              ListEmptyComponent={
                <View style={styles.emptyTableContainer}>
                  <Ionicons name="search-outline" size={24} color="#D4C3A3" />
                  <ThemedText style={styles.emptyTableText}>No matching repair orders found.</ThemedText>
                </View>
              }
              renderItem={({ item }) => (
                <TouchableOpacity
                  style={styles.lovItemRow}
                  onPress={() => handleSelectRepair(item)}
                  activeOpacity={0.7}
                >
                  <ThemedText style={[styles.lovItemText, styles.codeCell, { flex: 1.4 }]}>
                    {item.RepairNo}
                  </ThemedText>
                  <ThemedText style={[styles.lovItemText, { flex: 1.1 }]}>
                    {item.RepairDate ? new Date(item.RepairDate).toLocaleDateString() : ''}
                  </ThemedText>
                  <ThemedText style={[styles.lovItemText, { flex: 1.3 }]} numberOfLines={1}>
                    {item.CustomerName || item.CustomerCode || '-'}
                  </ThemedText>
                  <ThemedText style={[styles.lovItemText, styles.amountCell, { flex: 1.0, textAlign: 'right' }]}>
                    {parseFloat(item.TotalCostEstimated || 0).toFixed(2)}
                  </ThemedText>
                  <ThemedText style={[styles.lovItemText, styles.amountCell, { flex: 1.0, textAlign: 'right' }]}>
                    {parseFloat(item.AdvancePaid || 0).toFixed(2)}
                  </ThemedText>
                  <ThemedText style={[styles.lovItemText, styles.amountCell, { flex: 1.0, textAlign: 'right', color: '#B48218' }]}>
                    {parseFloat(item.BalanceAmount != null ? item.BalanceAmount : (item.TotalCostEstimated || 0) - (item.AdvancePaid || 0)).toFixed(2)}
                  </ThemedText>
                </TouchableOpacity>
              )}
            />
          </View>
        </View>
      </Modal>

      {/* REPRINT MODAL */}
      <Modal visible={isReprintModalVisible} transparent animationType="fade">
        <View style={styles.modalOverlay}>
          <View style={[styles.lovModalCard, { maxHeight: 260 }]}>
            <View style={styles.modalHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="print-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.modalHeaderTitle}>RePrint Repair Receipt</ThemedText>
              </View>
              <TouchableOpacity
                onPress={() => setIsReprintModalVisible(false)}
                activeOpacity={0.7}
                style={styles.closeModalBtn}
              >
                <Ionicons name="close" size={18} color="#3B2D17" />
              </TouchableOpacity>
            </View>

            <View style={{ padding: 16 }}>
              <ThemedText style={{ fontSize: 13, color: '#4A3B2C', marginBottom: 8, fontWeight: '600' }}>
                Enter Repair Docket Number:
              </ThemedText>
              <TextInput
                style={[styles.formInput, { height: 40, marginBottom: 16 }]}
                value={reprintRepairNo}
                onChangeText={setReprintRepairNo}
                placeholder="e.g. RP-HQ00000001"
                placeholderTextColor="#9E8D77"
                autoFocus
              />
              <View style={{ flexDirection: 'row', justifyContent: 'flex-end', gap: 10 }}>
                <TouchableOpacity
                  style={[styles.actionBtn, styles.clearBtn, { paddingHorizontal: 16, height: 36 }]}
                  onPress={() => setIsReprintModalVisible(false)}
                >
                  <ThemedText style={styles.clearBtnText}>Cancel</ThemedText>
                </TouchableOpacity>
                <TouchableOpacity
                  style={[styles.actionBtn, styles.saveBtn, { paddingHorizontal: 16, height: 36 }]}
                  onPress={handleReprintSubmit}
                >
                  <Ionicons name="print-outline" size={15} color="#9E6D08" />
                  <ThemedText style={styles.saveBtnText}>Print</ThemedText>
                </TouchableOpacity>
              </View>
            </View>
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
  repairBadge: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFF4DA',
    paddingHorizontal: 8,
    paddingVertical: 2,
    borderRadius: 4,
    borderWidth: 1,
    borderColor: '#B8903B',
  },
  repairBadgeLabel: {
    fontSize: 10,
    color: '#7D5E19',
    marginRight: 4,
  },
  repairBadgeValue: {
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

  // Attached lookup
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

  // Trade In
  tradeInOptionsGroup: {
    flex: 1,
    flexDirection: 'row',
    alignItems: 'center',
    gap: 4,
    flexWrap: 'wrap',
  },
  pillOption: {
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
  pillOptionSelected: {
    backgroundColor: '#FFF4DA',
    borderColor: '#B8903B',
  },
  pillOptionText: {
    fontSize: 11,
    fontWeight: '500',
    color: '#5C4A28',
  },
  pillOptionTextSelected: {
    color: '#9E6D08',
    fontWeight: 'bold',
  },
  tradeInGoBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFF1D6',
    borderWidth: 1,
    borderColor: '#B8903B',
    borderRadius: 4,
    paddingHorizontal: 6,
    height: 22,
    gap: 2,
  },
  tradeInGoBtnText: {
    fontSize: 10,
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
  cancelRowBtn: {
    backgroundColor: '#FFEBEE',
    borderWidth: 1,
    borderColor: '#EF9A9A',
    borderRadius: 4,
    paddingHorizontal: 8,
    paddingVertical: 2,
  },
  cancelRowBtnText: {
    fontSize: 10,
    fontWeight: 'bold',
    color: '#C62828',
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
