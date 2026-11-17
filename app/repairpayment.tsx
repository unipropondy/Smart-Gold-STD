import { ThemedText } from '@/components/themed-text';
import { PrintReportModal } from '@/components/PrintReportModal';
import { API_BASE_URL } from '@/constants/api';
import { useThemeColors } from '@/hooks/useThemeColors';
import { getStandardReportCss, renderStandardReportFooter, renderStandardReportHeader } from '@/utils/standardPrintReport';
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

  // Standardized Print Report Modal state
  const [isPrintModalVisible, setIsPrintModalVisible] = useState(false);
  const [activePrintRepairNo, setActivePrintRepairNo] = useState('');
  const [activePrintIsReprint, setActivePrintIsReprint] = useState(false);

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
        setActivePrintRepairNo(cleanNo);
        setActivePrintIsReprint(false);
        setIsPrintModalVisible(true);
        fetchBalance(cleanNo);
      }
    }
  }, [params?.paymentSuccess]);

  // Build Standardized Cambria HTML matching Purchase Return / Deposit Receipt
  const buildRepairPaymentHtml = (sInvoiceNo: string, isReprintMode: boolean, printData: any) => {
    const { company, header, details, payments, rates } = printData;

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

    const sCompanyName = (company?.name || 'SMART').trim();
    const sCompAddress2 = [company?.address1, company?.address2, company?.country].filter(Boolean).join(' ');
    const sCompAddress3 = [company?.phone, company?.fax].filter(Boolean).join(' ');

    const sCustomerName = (header?.CustomerName || customerName || '').trim();
    const sCustomerCode = (header?.CustomerCode || customerCode || '').trim();
    const sCustomerAddress1 = (header?.CustomerAddress1 || header?.Address1 || address || '').trim();
    const sCustomerAddress2 = (header?.CustomerAddress2 || header?.Address2 || '').trim();
    const sCustomerPhone = (header?.CustomerPhone || header?.Phone || '').trim();

    const s999 = rates?.rate999 || '-';
    const s916 = rates?.rate916 || '-';
    const s835 = rates?.rate835 || '-';

    const totalEstimatedCost = parseFloat(header?.TotalCostEstimated ?? estimatedCost) || 0;
    const advancePaidAmt = parseFloat(header?.AdvancePaid ?? advancePaid) || 0;

    let totalInstallmentsPaid = 0;
    (payments || []).forEach((p: any) => {
      totalInstallmentsPaid += parseFloat(p.AmountPaid) || 0;
    });
    const grandTotalPaid = advancePaidAmt + totalInstallmentsPaid;
    const balanceDue = Math.max(0, totalEstimatedCost - grandTotalPaid);

    const formatDate = (val: any) => {
      if (!val) return '-';
      const d = new Date(val);
      if (isNaN(d.getTime())) return String(val).split('T')[0] || '-';
      return d.toLocaleDateString('en-GB', { day: '2-digit', month: '2-digit', year: 'numeric' });
    };

    const repairDateFormatted = formatDate(header?.RepairDate || new Date());
    const deliveryDateFormatted = formatDate(header?.DeliveryDate || header?.DateOfDelivery);

    const sReportTitle = 'REPAIR PAYMENT';

    const generatePageMarkup = (copyLabel: string) => {
      return `
        <div class="page-sheet">
          <div class="report-main-content">
            ${renderStandardReportHeader({
              company: {
                name: sCompanyName,
                address: [sCompAddress2, sCompAddress3].filter(Boolean).join(' ')
              },
              reportTitle: `${sReportTitle} (${copyLabel})`,
              printedDateTime: `${repairDateFormatted} ${sgTimeStr}`
            })}

            <!-- TWO-COLUMN INFO: CUSTOMER vs REPAIR DOCKET -->
            <div class="info-grid">
              <div class="info-card">
                <div class="card-title">CUSTOMER DETAILS</div>
                <div class="info-row"><span class="info-lbl">Customer:</span> <span class="info-val font-bold">${sCustomerName || 'CASH CUSTOMER'}</span></div>
                <div class="info-row"><span class="info-lbl">Cust Code:</span> <span class="info-val">${sCustomerCode || '-'}</span></div>
                <div class="info-row"><span class="info-lbl">Address 1:</span> <span class="info-val">${sCustomerAddress1 || '-'}</span></div>
                ${sCustomerAddress2 ? `<div class="info-row"><span class="info-lbl">Address 2:</span> <span class="info-val">${sCustomerAddress2}</span></div>` : ''}
                ${sCustomerPhone ? `<div class="info-row"><span class="info-lbl">Contact:</span> <span class="info-val">${sCustomerPhone}</span></div>` : ''}
              </div>

              <div class="info-card">
                <div class="card-title">REPAIR DOCKET SPECIFICATION</div>
                <div class="info-row"><span class="info-lbl">Repair Docket No:</span> <span class="info-val font-bold">${sInvoiceNo}</span></div>
                <div class="info-row"><span class="info-lbl">Repair Date:</span> <span class="info-val">${repairDateFormatted}</span></div>
                <div class="info-row"><span class="info-lbl">Target Delivery:</span> <span class="info-val">${deliveryDateFormatted}</span></div>
                <div class="info-row"><span class="info-lbl">Docket Status:</span> <span class="info-val font-bold">${header?.Status || 'COMPLETED'}</span></div>
                <div class="info-row"><span class="info-lbl">Remarks:</span> <span class="info-val">${header?.Remarks || '-'}</span></div>
              </div>
            </div>

            <!-- CURRENT SUMMARY HIGHLIGHT CARD -->
            <div class="current-payment-card">
              <div class="payment-title">TOTAL ESTIMATED REPAIR COST</div>
              <div class="payment-amount">$ ${totalEstimatedCost.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</div>
            </div>

            <!-- REPAIR SERVICES & SCOPE OF WORK -->
            <div class="table-section-title">REPAIR SERVICES & SCOPE OF WORK</div>
            <table class="detail-table">
              <thead>
                <tr>
                  <th style="width: 8%; text-align: center;">#</th>
                  <th style="width: 52%; text-align: left;">SERVICE WORK / ITEM DESCRIPTION</th>
                  <th style="width: 20%; text-align: right;">WEIGHT (G)</th>
                  <th style="width: 20%; text-align: right;">ESTIMATED COST ($)</th>
                </tr>
              </thead>
              <tbody>
                ${(details && details.length > 0)
                  ? details.map((item: any, idx: number) => `
                    <tr>
                      <td style="text-align: center; font-weight: 700;">${item.LineNo || idx + 1}</td>
                      <td style="text-align: left; font-weight: 700;">${item.Description || item.RepairDescription || 'Repair Work'}</td>
                      <td style="text-align: right; font-weight: 700;">${parseFloat(item.WeightEstimated || item.Weight || 0).toFixed(3)}</td>
                      <td style="text-align: right; font-weight: 800; color: #8C5D08;">$ ${parseFloat(item.EstimatedCost || item.Cost || item.TotalCost || 0).toFixed(2)}</td>
                    </tr>
                  `).join('')
                  : `
                    <tr>
                      <td style="text-align: center; font-weight: 700;">1</td>
                      <td style="text-align: left; font-weight: 700;">REPAIR / SERVICE CHARGES</td>
                      <td style="text-align: right; font-weight: 700;">0.000</td>
                      <td style="text-align: right; font-weight: 800; color: #8C5D08;">$ ${totalEstimatedCost.toFixed(2)}</td>
                    </tr>
                  `
                }
              </tbody>
            </table>

            <!-- PAYMENT TRANSACTIONS / HISTORY TABLE -->
            <div class="table-section-title">PAYMENT TRANSACTIONS & SETTLEMENT HISTORY</div>
            <table class="detail-table">
              <thead>
                <tr>
                  <th style="width: 8%; text-align: center;">#</th>
                  <th style="width: 32%; text-align: left;">PAYMENT VOUCHER</th>
                  <th style="width: 20%; text-align: center;">DATE</th>
                  <th style="width: 20%; text-align: left;">REMARKS</th>
                  <th style="width: 20%; text-align: right;">AMOUNT PAID ($)</th>
                </tr>
              </thead>
              <tbody>
                ${(payments && payments.length > 0)
                  ? payments.map((p: any, idx: number) => `
                    <tr>
                      <td style="text-align: center; font-weight: 700;">${idx + 1}</td>
                      <td style="text-align: left; font-weight: 700;">${p.RepairPaymentNo || `P-${idx + 1}`}</td>
                      <td style="text-align: center; font-weight: 700;">${formatDate(p.CreateDate)}</td>
                      <td style="text-align: left; font-weight: 700;">${p.Remarks || '-'}</td>
                      <td style="text-align: right; font-weight: 800; color: #8C5D08;">$ ${parseFloat(p.AmountPaid || 0).toFixed(2)}</td>
                    </tr>
                  `).join('')
                  : `
                    <tr>
                      <td style="text-align: center; font-weight: 700;">1</td>
                      <td style="text-align: left; font-weight: 700;">${sInvoiceNo}</td>
                      <td style="text-align: center; font-weight: 700;">${repairDateFormatted}</td>
                      <td style="text-align: left; font-weight: 700;">Initial Advance</td>
                      <td style="text-align: right; font-weight: 800; color: #8C5D08;">$ ${advancePaidAmt.toFixed(2)}</td>
                    </tr>
                  `
                }
              </tbody>
            </table>

            <!-- SUMMARY FINANCIAL BALANCES -->
            <div class="totals-section">
              <div class="totals-box">
                <div class="total-row">
                  <span>Total Estimated Repair Cost:</span>
                  <span>$ ${totalEstimatedCost.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</span>
                </div>
                <div class="total-row">
                  <span>Initial Advance Paid:</span>
                  <span>$ ${advancePaidAmt.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</span>
                </div>
                <div class="total-row">
                  <span>Subsequent Payments Received:</span>
                  <span>$ ${totalInstallmentsPaid.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</span>
                </div>
                <div class="total-row">
                  <span>Total Received To Date:</span>
                  <span style="font-weight: 800; color: #8C5D08;">$ ${grandTotalPaid.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</span>
                </div>
                <div class="total-row total-row-grand">
                  <span>REMAINING BALANCE DUE:</span>
                  <span>$ ${balanceDue > 0 ? balanceDue.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 }) : '0.00 (SETTLED)'}</span>
                </div>
              </div>
            </div>
          </div>

          ${renderStandardReportFooter({
            staff: {
              staffLabel: 'PREPARED BY (STAFF)',
              staffName: (header?.CreateUser || 'Staff').trim(),
              receiverLabel: 'SUPPLIER / AUTHORIZED RECEIVER',
              receiverName: (sCustomerName || '-').trim()
            }
          })}
        </div>
      `;
    };

    const printPagesHtml = `
      ${generatePageMarkup('CUSTOMER COPY')}
      ${generatePageMarkup('OFFICE COPY')}
    `;

    return `
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
                page-break-after: always !important;
                break-after: page !important;
              }
              .page-sheet:last-child {
                page-break-after: auto !important;
                break-after: auto !important;
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
              padding-top: 14px !important;
              page-break-inside: avoid !important;
              break-inside: avoid !important;
            }

            *, *:before, *:after, body, div, table, tr, th, td, span, p, h1, h2, h3, h4, h5, h6 {
              font-family: 'Cambria', Georgia, 'Times New Roman', serif !important;
            }

            /* GOLD RATES BANNER */
            .gold-rate-strip {
              display: flex;
              justify-content: space-around;
              background: #FAF8F5;
              border: 1px solid #C49A45;
              border-radius: 4px;
              padding: 6px 12px;
              margin-top: 6px;
              margin-bottom: 12px;
              font-size: 11px;
              font-weight: 700;
              color: #222222;
            }
            .rate-cell span.rate-lbl {
              color: #222222;
              font-weight: 700;
            }
            .rate-cell span.rate-val {
              color: #8C5D08;
              font-weight: 800;
              margin-left: 4px;
            }

            /* TWO-COLUMN INFO */
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
            .info-row {
              display: flex;
              font-size: 11px;
              margin-bottom: 3.5px;
              line-height: 1.35;
            }
            .info-lbl {
              width: 130px;
              font-weight: 700;
              color: #222222;
              flex-shrink: 0;
            }
            .info-val {
              color: #222222;
              font-weight: 700;
              flex-grow: 1;
              word-break: break-word;
            }
            .font-bold {
              font-weight: 800;
            }

            /* CURRENT HIGHLIGHT CARD */
            .current-payment-card {
              background: #FAF8F5;
              border: 1px solid #C49A45;
              border-radius: 4px;
              padding: 10px 16px;
              text-align: center;
              margin-bottom: 12px;
            }
            .payment-title {
              font-size: 11px;
              font-weight: 800;
              color: #8C5D08;
              letter-spacing: 0.8px;
              text-transform: uppercase;
              margin-bottom: 2px;
            }
            .payment-amount {
              font-size: 24px;
              font-weight: 800;
              color: #8C5D08;
              letter-spacing: 1px;
            }

            /* TABLE */
            .table-section-title {
              font-size: 11px;
              font-weight: 800;
              color: #8C5D08;
              text-transform: uppercase;
              letter-spacing: 0.5px;
              margin-bottom: 6px;
            }
            .detail-table {
              width: 100%;
              border-collapse: collapse;
              margin-bottom: 12px;
              font-size: 10.5px;
            }
            .detail-table th {
              background: #8C5D08;
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
              background: #FFFFFF;
              color: #222222;
            }
            .detail-table tr:nth-child(even) td {
              background: #FAF8F5;
            }

            /* TOTALS SECTION */
            .totals-section {
              display: flex;
              justify-content: flex-end;
              margin-bottom: 14px;
            }
            .totals-box {
              width: 330px;
              border: 1px solid #C49A45;
              background: #FAF8F5;
              border-radius: 4px;
              padding: 8px 12px;
            }
            .total-row {
              display: flex;
              justify-content: space-between;
              font-size: 11.5px;
              padding: 2.5px 0;
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
          ${printPagesHtml}
        </body>
      </html>
    `;
  };

  // Original subPrintRepair implementation (matching VB.NET subPrintRepair in frmRepairPayment)
  const subPrintRepair = async (targetInvoiceNo: string, isReprintMode: boolean = false) => {
    const sInvoiceNo = (targetInvoiceNo || '').trim();
    if (!sInvoiceNo) {
      showToast('Please enter OrderNo.', 'error');
      repairNoInputRef.current?.focus?.();
      return;
    }

    try {
      setLoading(true);

      const res = await fetch(`${API_URL}/print-data?repairNo=${encodeURIComponent(sInvoiceNo)}`);
      const json = await res.json();

      if (!json.success || !json.data) {
        showToast(json.message || `Repair docket ${sInvoiceNo} not found for print.`, 'error');
        setLoading(false);
        return;
      }

      const htmlContent = buildRepairPaymentHtml(sInvoiceNo, isReprintMode, json.data);
      const docTitle = isReprintMode ? `Repair Invoice (Duplicate) - ${sInvoiceNo}` : `Repair Invoice - ${sInvoiceNo}`;

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
        const frameId = '__repair_payment_print_frame__';
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
          iframeDoc.title = docTitle;
          iframeDoc.close();

          setTimeout(() => {
            try {
              iframe.contentWindow?.focus();
              iframe.contentWindow?.print();
              showToast(`Repair payment ${sInvoiceNo} print dialog opened`, 'success');
            } catch (printErr) {
              console.error('Iframe print error:', printErr);
              const printWindow = window.open('', '_blank', 'width=900,height=750');
              if (printWindow) {
                printWindow.document.open();
                printWindow.document.write(printHtml);
                printWindow.document.title = docTitle;
                printWindow.document.close();
              }
            }
          }, 350);
        } else {
          const printWindow = window.open('', '_blank', 'width=900,height=750');
          if (printWindow) {
            printWindow.document.open();
            printWindow.document.write(printHtml);
            printWindow.document.title = docTitle;
            printWindow.document.close();
            showToast('Repair Invoice report preview opened', 'success');
          } else {
            showToast('Popup blocker prevented opening print preview. Please allow popups.', 'error');
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

  // Dedicated PDF Download Functionality
  const handleDownloadPdf = async (sInvoiceNo?: string, isReprint: boolean = false) => {
    const rawNo = (sInvoiceNo || '').trim();
    if (!rawNo) {
      showToast('Validation Error: Repair No is required to download PDF', 'error');
      return;
    }

    try {
      setLoading(true);
      const res = await fetch(`${API_URL}/print-data?repairNo=${encodeURIComponent(rawNo)}`);
      const json = await res.json();

      if (!json.success || !json.data) {
        showToast(json.message || `Repair record "${rawNo}" not found`, 'error');
        return;
      }

      const htmlContent = buildRepairPaymentHtml(rawNo, isReprint, json.data);
      const downloadFilename = `Repair_Payment_${rawNo.replace(/[^a-zA-Z0-9_-]/g, '_')}.pdf`;

      const pdfRes = await fetch(`${API_URL}/export-pdf`, {
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
        showToast(`PDF downloaded successfully: ${downloadFilename}`, 'success');
      } else {
        showToast('PDF file generated successfully.', 'success');
      }
    } catch (err: any) {
      console.error('PDF Export Error:', err);
      showToast('Failed to download PDF: ' + (err.message || 'Error'), 'error');
    } finally {
      setLoading(false);
    }
  };

  // Dedicated Excel Download Functionality
  const handleDownloadExcel = async (sInvoiceNo?: string, isReprint: boolean = false) => {
    const rawNo = (sInvoiceNo || '').trim();
    if (!rawNo) {
      showToast('Validation Error: Repair No is required to download Excel', 'error');
      return;
    }

    try {
      setLoading(true);
      const excelRes = await fetch(`${API_URL}/export-excel`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          repairNo: rawNo,
          isReprint: isReprint
        })
      });

      if (!excelRes.ok) {
        const errJson = await excelRes.json().catch(() => ({}));
        throw new Error(errJson.message || 'Server error generating Excel file');
      }

      const excelBlob = await excelRes.blob();
      const downloadFilename = `Repair_Payment_${rawNo.replace(/[^a-zA-Z0-9_-]/g, '_')}.xlsx`;

      if (Platform.OS === 'web' && typeof window !== 'undefined') {
        const url = URL.createObjectURL(excelBlob);
        const a = document.createElement('a');
        a.href = url;
        a.download = downloadFilename;
        document.body.appendChild(a);
        a.click();
        document.body.removeChild(a);
        setTimeout(() => URL.revokeObjectURL(url), 1000);
        showToast(`Repair Payment Excel downloaded: ${downloadFilename}`, 'success');
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

  // RePrint button click handler (matching frmRepairPayment btnPrint_Click)
  const handlePrintClick = () => {
    const cleanNo = (repairNo || '').trim();
    if (!cleanNo) {
      showToast('Please enter OrderNo.', 'error');
      repairNoInputRef.current?.focus?.();
      return;
    }
    setActivePrintRepairNo(cleanNo);
    setActivePrintIsReprint(true);
    setIsPrintModalVisible(true);
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
    setActivePrintRepairNo(targetNo);
    setActivePrintIsReprint(true);
    setIsPrintModalVisible(true);
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
                        <ThemedText style={styles.tradeInGoBtnText}>Trade IN</ThemedText>
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

      {/* STANDARDIZED PRINT REPORT CONFIRMATION MODAL */}
      <PrintReportModal
        visible={isPrintModalVisible}
        title="Print Report"
        message="Do you want to print ?"
        loading={loading}
        onCancel={() => {
          setIsPrintModalVisible(false);
          setActivePrintRepairNo('');
        }}
        onPdf={() => {
          const invNo = activePrintRepairNo;
          const reprint = activePrintIsReprint;
          setIsPrintModalVisible(false);
          setActivePrintRepairNo('');
          if (invNo) {
            handleDownloadPdf(invNo, reprint);
          }
        }}
        onExcel={() => {
          const invNo = activePrintRepairNo;
          const reprint = activePrintIsReprint;
          setIsPrintModalVisible(false);
          setActivePrintRepairNo('');
          if (invNo) {
            handleDownloadExcel(invNo, reprint);
          }
        }}
        onPrint={() => {
          const invNo = activePrintRepairNo;
          const reprint = activePrintIsReprint;
          setIsPrintModalVisible(false);
          setActivePrintRepairNo('');
          if (invNo) {
            subPrintRepair(invNo, reprint);
          }
        }}
      />
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
