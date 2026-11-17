/** Developer: durairaj */
import { ThemedText } from '@/components/themed-text';
import { PrintReportModal } from '@/components/PrintReportModal';
import { API_BASE_URL } from '@/constants/api';
import { getStandardReportCss, renderStandardReportFooter, renderStandardReportHeader } from '@/utils/standardPrintReport';
import { UNIPRO_LOGO_DATA_URI } from '@/utils/uniproLogo';
import { Ionicons } from '@expo/vector-icons';
import { Stack, useFocusEffect, useRouter } from 'expo-router';
import React, { useCallback, useEffect, useState } from 'react';
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
  TouchableWithoutFeedback,
  View
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';

const API_URL = `${API_BASE_URL}`;

export default function PayOutScreen() {
  const router = useRouter();

  // ===== STATE =====
  const [loading, setLoading] = useState(false);
  const [toast, setToast] = useState<{ message: string; type: 'success' | 'error' } | null>(null);

  // Field values
  const [payoutNo, setPayoutNo] = useState('');
  const [payoutCode, setPayoutCode] = useState('');
  const [codeDescription, setCodeDescription] = useState('');
  const [payoutDescription, setPayoutDescription] = useState('');
  const [amount, setAmount] = useState('');
  const [remarks, setRemarks] = useState('');

  // LOV State
  const [lovVisible, setLovVisible] = useState(false);
  const [lovData, setLovData] = useState<any[]>([]);
  const [filteredLovData, setFilteredLovData] = useState<any[]>([]);
  const [lovSearch, setLovSearch] = useState('');

  // Print Report Modal State
  const [isPrintModalVisible, setIsPrintModalVisible] = useState(false);
  const [printVoucherNo, setPrintVoucherNo] = useState('');
  const [isReprintMode, setIsReprintMode] = useState(false);

  const showToast = (message: string, type: 'success' | 'error' = 'success') => {
    setToast({ message, type });
    setTimeout(() => {
      setToast(null);
    }, 3000);
  };

  // Keyboard Shortcuts: Enter to Next, F7 to Save, F6 to Clear/New, Escape to Back
  useFocusEffect(
    useCallback(() => {
      if (Platform.OS === 'web') {
        const handleKeyDown = (e: KeyboardEvent) => {
          if (e.key === 'F7') {
            e.preventDefault();
            handleSave();
          } else if (e.key === 'F6') {
            e.preventDefault();
            handleClear();
          } else if (e.key === 'Escape') {
            e.preventDefault();
            if (lovVisible) {
              setLovVisible(false);
            } else {
              router.back();
            }
          }
        };
        window.addEventListener('keydown', handleKeyDown);
        return () => window.removeEventListener('keydown', handleKeyDown);
      }
    }, [payoutCode, payoutDescription, amount, remarks, codeDescription, lovVisible])
  );

  // Fetch next payout sequence number
  const fetchNextCode = async () => {
    setLoading(true);
    try {
      const res = await fetch(`${API_URL}/payout/nextCode`);
      const json = await res.json();
      if (json.success) {
        setPayoutNo(json.data);
      }
    } catch (e) {
      showToast("Failed to fetch next payout number.", "error");
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchNextCode();
  }, []);

  // Fetch payout master data for LOV selection
  const handleOpenMasterLov = async () => {
    setLoading(true);
    try {
      const res = await fetch(`${API_URL}/payoutmaster`);
      const json = await res.json();
      if (json.success) {
        setLovData(json.data);
        setFilteredLovData(json.data);
        setLovSearch('');
        setLovVisible(true);
      } else {
        showToast("Failed to load payout master entries.", "error");
      }
    } catch (e) {
      showToast("Server connection error.", "error");
    } finally {
      setLoading(false);
    }
  };

  const handleLovSearch = (text: string) => {
    setLovSearch(text);
    if (!text) {
      setFilteredLovData(lovData);
      return;
    }
    const filtered = lovData.filter(item =>
      (item.Code && item.Code.toLowerCase().includes(text.toLowerCase())) ||
      (item.Description && item.Description.toLowerCase().includes(text.toLowerCase())) ||
      (item.Remarks && item.Remarks.toLowerCase().includes(text.toLowerCase()))
    );
    setFilteredLovData(filtered);
  };

  const handleSelectMaster = (item: any) => {
    setPayoutCode(item.Code ? item.Code.trim() : '');
    setCodeDescription(item.Description ? item.Description.trim() : '');
    setLovVisible(false);
  };

  const handleClear = () => {
    setPayoutCode('');
    setCodeDescription('');
    setPayoutDescription('');
    setAmount('');
    setRemarks('');
    fetchNextCode();
  };

  const handleSave = async () => {
    if (!payoutCode.trim()) {
      showToast("PayOut Code should not be blank.", "error");
      return;
    }
    if (!payoutDescription.trim()) {
      showToast("PayOut Description should not be blank.", "error");
      return;
    }
    const parsedAmount = parseFloat(amount);
    if (isNaN(parsedAmount) || parsedAmount <= 0) {
      showToast("Amount must be a numeric value greater than zero.", "error");
      return;
    }

    setLoading(true);
    try {
      const res = await fetch(`${API_URL}/payout/save`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          Code: payoutCode,
          sDescription: codeDescription,
          PayOutDescription: payoutDescription,
          Amount: amount,
          Remarks: remarks,
          userId: "sys"
        })
      });
      const json = await res.json();
      if (json.success) {
        showToast("PayOut detail saved successfully.", "success");
        const savedPayoutNo = (json.payoutNo || json.data || payoutNo || '').trim();
        handleClear();
        // Open Print Invoice confirmation popup after successful backend save
        if (savedPayoutNo !== '') {
          setPrintVoucherNo(savedPayoutNo);
          setIsReprintMode(false);
          setIsPrintModalVisible(true);
        }
      } else {
        showToast(json.message || "Failed to save payout detail.", "error");
      }
    } catch (e) {
      showToast("Server connection failed.", "error");
    } finally {
      setLoading(false);
    }
  };

  // Helper to build standardized HTML for Payout Voucher
  const buildPayoutVoucherHtml = (trimmedPayoutNo: string, isReprint: boolean, printData: any) => {
    const { company, payout, payModes, rates } = printData;

    // Singapore Date & Time
    const now = new Date();
    const sgTimeStr = now.toLocaleTimeString('en-US', {
      hour: '2-digit',
      minute: '2-digit',
      hour12: true,
    });

    const formatDate = (val: any) => {
      if (!val) return '-';
      const d = new Date(val);
      if (isNaN(d.getTime())) return String(val).split('T')[0] || '-';
      return d.toLocaleDateString('en-GB', { day: '2-digit', month: '2-digit', year: 'numeric' });
    };

    const voucherDateFormatted = formatDate(payout.PayOutDate || payout.CreateDate);

    // Company Information
    const compName = (company.name || 'SMART').trim();
    const compAddr1 = (company.address1 || '').trim();
    const compAddr2 = [company.address2, company.city, company.country].filter(Boolean).join(' ');
    const compContact = [
      company.phone ? `Tel: ${company.phone}` : '',
      company.fax ? `Fax: ${company.fax}` : ''
    ].filter(Boolean).join(' | ');

    // Daily Gold Rates
    const s999 = rates?.rate999 ? parseFloat(rates.rate999).toFixed(2) : '190.00';
    const s916 = rates?.rate916 ? parseFloat(rates.rate916).toFixed(2) : '176.00';
    const s835 = rates?.rate835 && parseFloat(rates.rate835) > 0 ? parseFloat(rates.rate835).toFixed(2) : '-';

    const pAmount = parseFloat(payout.Amount) || 0;
    const disbursementText = payModes && payModes.length > 0
      ? payModes.map((pm: any) => pm.payMode).join(', ')
      : 'CASH';

    const reportTitle = 'PAYOUT';
    const docTitle = `Payout - ${trimmedPayoutNo}`;

    return `
      <!DOCTYPE html>
      <html>
        <head>
          <meta charset="utf-8" />
          <title>${docTitle}</title>
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
              padding-top: 14px !important;
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

            /* GOLD RATES STRIP */
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
            .rate-cell {
              display: inline-flex;
              align-items: center;
              gap: 4px;
            }
            .rate-lbl {
              color: #444444;
            }
            .rate-val {
              color: #8C5D08;
              font-weight: 800;
            }

            /* INFO GRID */
            .info-grid {
              display: grid;
              grid-template-columns: 1fr 1fr;
              gap: 16px;
              margin-bottom: 14px;
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
              width: 120px;
              font-weight: 700;
              color: #222222;
              flex-shrink: 0;
            }
            .info-colon {
              width: 15px;
              font-weight: 700;
              color: #222222;
              text-align: center;
              flex-shrink: 0;
            }
            .info-val {
              color: #222222;
              font-weight: 700;
              flex-grow: 1;
              word-break: break-word;
            }

            /* TOTAL AMOUNT PAID OUT BANNER */
            .payout-banner {
              background: #FAF8F5;
              border: 1px solid #C49A45;
              border-radius: 4px;
              padding: 12px 18px;
              text-align: center;
              margin-bottom: 14px;
            }
            .payout-lbl {
              font-size: 11px;
              font-weight: 800;
              color: #8C5D08;
              letter-spacing: 1px;
              text-transform: uppercase;
              margin-bottom: 4px;
            }
            .payout-val {
              font-size: 26px;
              font-weight: 800;
              color: #8C5D08;
              margin-bottom: 3px;
            }
            .payout-sub {
              font-size: 10.5px;
              font-weight: 700;
              color: #555555;
              letter-spacing: 0.5px;
            }

            /* DETAIL TABLE */
            .detail-table {
              width: 100%;
              border-collapse: collapse;
              margin-top: 4px;
              margin-bottom: 12px;
              font-size: 11px;
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
              width: 290px;
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
          <div class="page-sheet">
            <div class="report-main-content">
              ${renderStandardReportHeader({
                company: {
                  name: compName,
                  address: [compAddr1, compAddr2, compContact].filter(Boolean).join(' ')
                },
                reportTitle: reportTitle,
                printedDateTime: `${voucherDateFormatted} ${sgTimeStr}`
              })}

              <!-- INFO GRID: Expense Classification & Voucher Audit Details -->
              <div class="info-grid">
                <!-- EXPENSE CLASSIFICATION -->
                <div class="info-card">
                  <div class="card-title">EXPENSE CLASSIFICATION</div>
                  <div class="info-row"><span class="info-lbl">Expense Code</span><span class="info-colon">:</span><span class="info-val font-bold">${payout.Code || '-'}</span></div>
                  <div class="info-row"><span class="info-lbl">Category</span><span class="info-colon">:</span><span class="info-val font-bold">${payout.Description || '-'}</span></div>
                  <div class="info-row"><span class="info-lbl">Disbursement</span><span class="info-colon">:</span><span class="info-val font-bold">${disbursementText}</span></div>
                </div>

                <!-- VOUCHER AUDIT DETAILS -->
                <div class="info-card">
                  <div class="card-title">VOUCHER AUDIT DETAILS</div>
                  <div class="info-row"><span class="info-lbl">Voucher No</span><span class="info-colon">:</span><span class="info-val font-bold">${payout.PayOutNo || trimmedPayoutNo}</span></div>
                  <div class="info-row"><span class="info-lbl">Issued Date</span><span class="info-colon">:</span><span class="info-val font-bold">${voucherDateFormatted}</span></div>
                  <div class="info-row"><span class="info-lbl">Processed By</span><span class="info-colon">:</span><span class="info-val">${payout.CreateUser || 'sys'}</span></div>
                </div>
              </div>

              <!-- TOTAL AMOUNT PAID OUT SECTION -->
              <div class="payout-banner">
                <div class="payout-lbl">TOTAL AMOUNT PAID OUT</div>
                <div class="payout-val">$ ${pAmount.toFixed(2)}</div>
                <div class="payout-sub">SETTLED IN FULL VIA ${disbursementText}</div>
              </div>

              <!-- PAYOUT PARTICULARS & DETAILS TABLE -->
              <table class="detail-table">
                <thead>
                  <tr>
                    <th style="width: 6%; text-align: center;">#</th>
                    <th style="width: 32%;">CODE / CATEGORY</th>
                    <th style="width: 44%;">PAYOUT DESCRIPTION / PURPOSE</th>
                    <th style="width: 18%; text-align: right;">AMOUNT ($)</th>
                  </tr>
                </thead>
                <tbody>
                  <tr>
                    <td style="text-align: center; font-weight: 700;">1</td>
                    <td style="font-weight: 700;">${payout.Code ? `${payout.Code} - ${payout.Description}` : payout.Description || '-'}</td>
                    <td style="font-weight: 700;">${payout.PayOutDescription || payout.Remarks || payout.Description || '-'}</td>
                    <td style="text-align: right; font-weight: 800; color: #8C5D08;">$ ${pAmount.toFixed(2)}</td>
                  </tr>
                </tbody>
              </table>

              <!-- PAYOUT TOTALS SECTION -->
              <div class="totals-section">
                <div class="totals-box">
                  <div class="total-row">
                    <span>Subtotal Payout:</span>
                    <span>$ ${pAmount.toFixed(2)}</span>
                  </div>
                  <div class="total-row total-row-grand">
                    <span>NET CASH DISBURSED:</span>
                    <span>$ ${pAmount.toFixed(2)}</span>
                  </div>
                </div>
              </div>
            </div>

            ${renderStandardReportFooter({
              staff: {
                staffLabel: 'PREPARED BY (STAFF)',
                staffName: (payout.CreateUser || 'sys').trim(),
                receiverLabel: 'SUPPLIER / AUTHORIZED RECEIVER',
                receiverName: '-'
              }
            })}
          </div>
        </body>
      </html>
    `;
  };

  // Original Print Function matching subPrintBill in frmPayOut.vb
  const subPrintBill = async (sInvoiceNo: string, isReprint: boolean = false) => {
    const trimmedPayoutNo = (sInvoiceNo || '').trim();
    if (!trimmedPayoutNo) {
      return;
    }

    try {
      setLoading(true);
      const res = await fetch(`${API_URL}/payout/print-data?payoutNo=${encodeURIComponent(trimmedPayoutNo)}`);
      const json = await res.json();

      if (!json.success || !json.data) {
        showToast(json.message || `PayOut voucher "${trimmedPayoutNo}" not found for print.`, 'error');
        setLoading(false);
        return;
      }

      const htmlContent = buildPayoutVoucherHtml(trimmedPayoutNo, isReprint, json.data);
      const docTitle = isReprint ? `Payout Voucher (Duplicate) - ${trimmedPayoutNo}` : `Payout Voucher - ${trimmedPayoutNo}`;

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
        const frameId = '__payout_voucher_print_frame__';
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
              showToast(`PayOut voucher ${trimmedPayoutNo} print dialog opened`, 'success');
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
            showToast(`PayOut voucher ${trimmedPayoutNo} preview opened`, 'success');
          } else {
            showToast('Popup blocker prevented opening print preview. Please allow popups.', 'error');
          }
        }
      } else {
        showToast('Print preview generated.', 'success');
      }

    } catch (err: any) {
      console.error('Print PayOut Voucher Error:', err);
      showToast(err.message || 'Failed to print payout voucher', 'error');
    } finally {
      setLoading(false);
    }
  };

  // Dedicated PDF Download Functionality
  const handleDownloadPdf = async (sInvoiceNo?: string, isReprint: boolean = false) => {
    const trimmedPayoutNo = (sInvoiceNo || payoutNo || '').trim();
    if (!trimmedPayoutNo) {
      showToast('Validation Error: PayOut No is required to download PDF', 'error');
      return;
    }

    try {
      setLoading(true);
      const res = await fetch(`${API_URL}/payout/print-data?payoutNo=${encodeURIComponent(trimmedPayoutNo)}`);
      const json = await res.json();

      if (!json.success || !json.data) {
        showToast(json.message || `PayOut voucher "${trimmedPayoutNo}" not found`, 'error');
        return;
      }

      const htmlContent = buildPayoutVoucherHtml(trimmedPayoutNo, isReprint, json.data);
      const downloadFilename = `Payout_Voucher_${trimmedPayoutNo.replace(/[^a-zA-Z0-9_-]/g, '_')}.pdf`;

      const pdfRes = await fetch(`${API_URL}/payout/export-pdf`, {
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
        showToast(`Payout Voucher PDF downloaded: ${downloadFilename}`, 'success');
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

  // Dedicated Excel Download Functionality
  const handleDownloadExcel = async (sInvoiceNo?: string, isReprint: boolean = false) => {
    const trimmedPayoutNo = (sInvoiceNo || payoutNo || '').trim();
    if (!trimmedPayoutNo) {
      showToast('Validation Error: PayOut No is required to download Excel', 'error');
      return;
    }

    try {
      setLoading(true);
      const excelRes = await fetch(`${API_URL}/payout/export-excel`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          payoutNo: trimmedPayoutNo,
          isReprint: isReprint
        })
      });

      if (!excelRes.ok) {
        const errJson = await excelRes.json().catch(() => ({}));
        throw new Error(errJson.message || 'Server error generating Excel file');
      }

      const excelBlob = await excelRes.blob();
      const downloadFilename = `Payout_Voucher_${trimmedPayoutNo.replace(/[^a-zA-Z0-9_-]/g, '_')}.xlsx`;

      if (Platform.OS === 'web' && typeof window !== 'undefined') {
        const url = URL.createObjectURL(excelBlob);
        const a = document.createElement('a');
        a.href = url;
        a.download = downloadFilename;
        document.body.appendChild(a);
        a.click();
        document.body.removeChild(a);
        setTimeout(() => URL.revokeObjectURL(url), 1000);
        showToast(`Payout Voucher Excel downloaded: ${downloadFilename}`, 'success');
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

  // Original VB.NET btnPrint_Click:
  // If txtPayOutNo.Text.Trim <> "" Then
  //     subPrintBill(txtPayOutNo.Text.Trim)
  // End If
  const handlePrint = () => {
    if (payoutNo.trim() !== '') {
      setPrintVoucherNo(payoutNo.trim());
      setIsReprintMode(true);
      setIsPrintModalVisible(true);
    } else {
      showToast('Please enter or select a PayOut Voucher No', 'error');
    }
  };

  return (
    <SafeAreaView style={styles.container} edges={['top']}>
      <Stack.Screen options={{ headerShown: false }} />

      {/* LUXURY GOLD HEADER BAR */}
      <View style={styles.headerBar}>
        <View style={styles.headerLeft}>
          <TouchableOpacity
            style={styles.backBtn}
            onPress={() => router.back()}
            activeOpacity={0.7}
          >
            <Ionicons name="arrow-back" size={20} color="#3B2D17" />
          </TouchableOpacity>
          <ThemedText style={styles.headerTitle}>Pay Out</ThemedText>
          {loading && <ActivityIndicator size="small" color="#B48218" style={{ marginLeft: 6 }} />}
        </View>

        <View style={styles.headerRight}>
          {/* Reprint / Print */}
          <TouchableOpacity
            style={[styles.actionBtn, styles.clearBtn]}
            onPress={handlePrint}
            activeOpacity={0.8}
            disabled={loading}
          >
            <Ionicons name="print-outline" size={14} color="#5C4A28" />
            <ThemedText style={styles.clearBtnText}>Reprint</ThemedText>
          </TouchableOpacity>

          {/* New / Clear */}
          <TouchableOpacity
            style={[styles.actionBtn, styles.clearBtn]}
            onPress={handleClear}
            activeOpacity={0.8}
            disabled={loading}
          >
            <Ionicons name="add-circle-outline" size={14} color="#5C4A28" />
            <ThemedText style={styles.clearBtnText}>New</ThemedText>
          </TouchableOpacity>

          {/* Save */}
          <TouchableOpacity
            style={[styles.actionBtn, styles.saveBtn]}
            onPress={handleSave}
            activeOpacity={0.8}
            disabled={loading}
          >
            {loading ? (
              <ActivityIndicator size="small" color="#9E6D08" />
            ) : (
              <>
                <Ionicons name="checkmark-done-outline" size={14} color="#9E6D08" />
                <ThemedText style={styles.saveBtnText}>Save</ThemedText>
              </>
            )}
          </TouchableOpacity>
        </View>
      </View>

      <KeyboardAvoidingView behavior={Platform.OS === 'ios' ? 'padding' : undefined} style={{ flex: 1 }}>
        <ScrollView contentContainerStyle={styles.contentContainer}>
          <View style={styles.formCenterWrapper}>
            {/* LUXURY CARD */}
            <View style={styles.goldCard}>
              <View style={styles.cardHeaderBanner}>
                <View style={styles.headerTitleContainer}>
                  <View style={styles.headerIconBadgeSmall}>
                    <Ionicons name="cash-outline" size={13} color="#B48218" />
                  </View>
                  <ThemedText style={styles.cardHeaderTitleSmall}>Payout Voucher Entry</ThemedText>
                </View>

                {/* Payout Entry No Badge */}
                <View style={styles.payoutBadge}>
                  <ThemedText style={styles.payoutBadgeLabel}>Voucher #</ThemedText>
                  <ThemedText style={styles.payoutBadgeValue}>{payoutNo || '...'}</ThemedText>
                </View>
              </View>

              <View style={styles.cardBody}>
                {/* Code with LOV */}
                <View style={styles.formGridRow}>
                  <ThemedText style={styles.formLabel}>Code *</ThemedText>
                  <View style={styles.inputWithLookup}>
                    <TextInput
                      style={[styles.formInput, styles.inputAttached]}
                      value={payoutCode}
                      onChangeText={setPayoutCode}
                      placeholder="Select Code..."
                      placeholderTextColor="#9E8D77"
                    />
                    <TouchableOpacity
                      style={styles.attachedLookupBtn}
                      onPress={handleOpenMasterLov}
                      activeOpacity={0.7}
                    >
                      <ThemedText style={styles.attachedLookupText}>...</ThemedText>
                    </TouchableOpacity>
                  </View>
                </View>

                {/* Description */}
                <View style={styles.formGridRow}>
                  <ThemedText style={styles.formLabel}>Description</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput]}
                    value={codeDescription}
                    editable={false}
                    placeholder="Auto-populated from code"
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* Specific Payout Description */}
                <View style={[styles.formGridRow, { alignItems: 'flex-start', paddingTop: 8 }]}>
                  <ThemedText style={[styles.formLabel, { marginTop: 4 }]}>Payout Desc *</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.multilineInput]}
                    value={payoutDescription}
                    onChangeText={setPayoutDescription}
                    multiline={true}
                    numberOfLines={3}
                    placeholder="Enter specific payout details or expense reason..."
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* Amount */}
                <View style={styles.formGridRow}>
                  <ThemedText style={styles.formLabel}>Amount *</ThemedText>
                  <View style={styles.amountInputContainer}>
                    <ThemedText style={styles.currencyPrefix}>$</ThemedText>
                    <TextInput
                      style={[styles.formInput, styles.amountInput]}
                      value={amount}
                      onChangeText={setAmount}
                      keyboardType="numeric"
                      placeholder="0.00"
                      placeholderTextColor="#9E8D77"
                    />
                  </View>
                </View>

                {/* Remarks */}
                <View style={[styles.formGridRow, { borderBottomWidth: 0 }]}>
                  <ThemedText style={styles.formLabel}>Remarks</ThemedText>
                  <TextInput
                    style={styles.formInput}
                    value={remarks}
                    onChangeText={setRemarks}
                    placeholder="Optional notes..."
                    placeholderTextColor="#9E8D77"
                  />
                </View>
              </View>
            </View>
          </View>
        </ScrollView>
      </KeyboardAvoidingView>

      {/* LOV Selection Modal */}
      <Modal visible={lovVisible} transparent={true} animationType="fade" onRequestClose={() => setLovVisible(false)}>
        <TouchableOpacity style={styles.modalOverlay} activeOpacity={1} onPress={() => setLovVisible(false)}>
          <TouchableWithoutFeedback>
            <View style={styles.lovModalContainer}>
              <View style={styles.modalHeader}>
                <View style={{ flexDirection: 'row', alignItems: 'center', gap: 6 }}>
                  <View style={styles.headerIconBadgeSmall}>
                    <Ionicons name="list" size={14} color="#B48218" />
                  </View>
                  <ThemedText style={styles.modalTitle}>Select Payout Code</ThemedText>
                </View>
                <TouchableOpacity onPress={() => setLovVisible(false)} style={styles.modalCloseBtn}>
                  <Ionicons name="close" size={20} color="#3B2D17" />
                </TouchableOpacity>
              </View>

              <View style={{ padding: 12 }}>
                <View style={styles.modalSearchBox}>
                  <Ionicons name="search-outline" size={15} color="#8F816E" style={{ marginRight: 6 }} />
                  <TextInput
                    style={styles.modalSearchInput}
                    placeholder="Search by code, description or remarks..."
                    placeholderTextColor="#8F816E"
                    value={lovSearch}
                    onChangeText={handleLovSearch}
                    autoFocus
                  />
                </View>

                <View style={styles.lovHeaderRow}>
                  <ThemedText style={[styles.lovHeaderText, { flex: 1.2 }]}>Code</ThemedText>
                  <ThemedText style={[styles.lovHeaderText, { flex: 2.5 }]}>Description</ThemedText>
                  <ThemedText style={[styles.lovHeaderText, { flex: 2.3 }]}>Remarks</ThemedText>
                </View>

                {loading ? (
                  <View style={{ padding: 30, alignItems: 'center' }}>
                    <ActivityIndicator size="large" color="#B48218" />
                  </View>
                ) : (
                  <FlatList
                    data={filteredLovData}
                    keyExtractor={(_, index) => index.toString()}
                    style={{ maxHeight: 320 }}
                    renderItem={({ item, index }) => (
                      <TouchableOpacity
                        style={[
                          styles.lovItemRow,
                          index % 2 === 1 && { backgroundColor: '#FCFAF6' }
                        ]}
                        activeOpacity={0.7}
                        onPress={() => handleSelectMaster(item)}
                      >
                        <ThemedText style={[styles.lovItemText, styles.codeCell, { flex: 1.2 }]}>
                          {String(item.Code || '')}
                        </ThemedText>
                        <ThemedText style={[styles.lovItemText, { flex: 2.5 }]}>
                          {String(item.Description || '')}
                        </ThemedText>
                        <ThemedText style={[styles.lovItemText, { flex: 2.3, color: '#7D6F5B' }]}>
                          {String(item.Remarks || '')}
                        </ThemedText>
                      </TouchableOpacity>
                    )}
                  />
                )}
              </View>
            </View>
          </TouchableWithoutFeedback>
        </TouchableOpacity>
      </Modal>

      {/* PRINT INVOICE CONFIRMATION POPUP */}
      <PrintReportModal
        visible={isPrintModalVisible}
        title="Print Invoice"
        message="Do you want to print ?"
        loading={loading}
        onCancel={() => {
          setIsPrintModalVisible(false);
          setPrintVoucherNo('');
        }}
        onPdf={() => {
          const vNo = printVoucherNo || payoutNo;
          setIsPrintModalVisible(false);
          setPrintVoucherNo('');
          if (vNo) {
            handleDownloadPdf(vNo, isReprintMode);
          }
        }}
        onExcel={() => {
          const vNo = printVoucherNo || payoutNo;
          setIsPrintModalVisible(false);
          setPrintVoucherNo('');
          if (vNo) {
            handleDownloadExcel(vNo, isReprintMode);
          }
        }}
        onPrint={() => {
          const vNo = printVoucherNo || payoutNo;
          setIsPrintModalVisible(false);
          setPrintVoucherNo('');
          if (vNo) {
            subPrintBill(vNo, isReprintMode);
          }
        }}
      />

      {/* TOAST ALERT */}
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
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#FBF9F5',
  },
  contentContainer: {
    padding: 12,
    paddingVertical: 16,
  },
  formCenterWrapper: {
    maxWidth: 680,
    width: '100%',
    alignSelf: 'center',
  },

  // ===== HEADER BAR =====
  headerBar: {
    height: 48,
    backgroundColor: '#FFFFFF',
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingHorizontal: 12,
    borderBottomWidth: 1,
    borderBottomColor: '#EBE2D2',
    elevation: 2,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.05,
    shadowRadius: 2,
  },
  headerLeft: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 8,
  },
  backBtn: {
    padding: 6,
    borderRadius: 6,
    backgroundColor: '#FAF5EA',
    borderWidth: 1,
    borderColor: '#E8DCB8',
  },
  headerTitle: {
    fontSize: 18,
    fontWeight: 'bold',
    color: '#3B2D17',
    letterSpacing: 0.5,
  },
  headerRight: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 6,
  },

  // ===== ACTION BUTTONS =====
  actionBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    paddingHorizontal: 12,
    paddingVertical: 5,
    borderRadius: 6,
    borderWidth: 1,
    gap: 4,
    minHeight: 28,
  },
  saveBtn: {
    backgroundColor: '#F9F2E2',
    borderColor: '#B8903B',
  },
  saveBtnText: {
    color: '#9E6D08',
    fontSize: 12,
    fontWeight: 'bold',
  },
  clearBtn: {
    backgroundColor: '#FFFFFF',
    borderColor: '#DFD5C3',
  },
  clearBtnText: {
    color: '#5C4A28',
    fontSize: 12,
    fontWeight: 'bold',
  },

  // ===== LUXURY GOLD CARD =====
  goldCard: {
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    shadowColor: '#A38035',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.08,
    shadowRadius: 4,
    elevation: 2,
    overflow: 'hidden',
  },
  cardHeaderBanner: {
    backgroundColor: '#F5E7CA',
    paddingHorizontal: 12,
    paddingVertical: 8,
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
  },
  headerTitleContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 6,
  },
  headerIconBadgeSmall: {
    width: 22,
    height: 22,
    borderRadius: 5,
    backgroundColor: '#FFF8EA',
    borderWidth: 1,
    borderColor: '#E2D2AF',
    alignItems: 'center',
    justifyContent: 'center',
  },
  cardHeaderTitleSmall: {
    fontSize: 13,
    fontWeight: 'bold',
    color: '#3B2D17',
    letterSpacing: 0.3,
  },
  payoutBadge: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFF8EA',
    borderWidth: 1,
    borderColor: '#D8C59A',
    borderRadius: 12,
    paddingHorizontal: 10,
    paddingVertical: 2,
    gap: 6,
  },
  payoutBadgeLabel: {
    fontSize: 10,
    fontWeight: '600',
    color: '#7D6F5B',
    textTransform: 'uppercase',
  },
  payoutBadgeValue: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#8D6409',
  },
  cardBody: {
    padding: 12,
  },

  // ===== FORM ROWS & INPUTS =====
  formGridRow: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingVertical: 8,
    borderBottomWidth: 1,
    borderBottomColor: '#F7F2E9',
    gap: 12,
  },
  formLabel: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#4A3B2C',
    width: 110,
    flexShrink: 0,
  },
  inputWithLookup: {
    flexDirection: 'row',
    flex: 1,
    alignItems: 'center',
    minWidth: 0,
  },
  formInput: {
    height: 32,
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderRadius: 6,
    paddingHorizontal: 10,
    paddingVertical: 0,
    fontSize: 12,
    color: '#2B2118',
    flex: 1,
  },
  readonlyInput: {
    backgroundColor: '#F8F5EE',
    borderColor: '#EADBCA',
    color: '#554736',
  },
  inputAttached: {
    borderTopRightRadius: 0,
    borderBottomRightRadius: 0,
  },
  attachedLookupBtn: {
    width: 32,
    height: 32,
    flexShrink: 0,
    backgroundColor: '#FAF5EA',
    borderWidth: 1,
    borderColor: '#D4C3A3',
    borderRadius: 6,
    marginLeft: 4,
    alignItems: 'center',
    justifyContent: 'center',
  },
  attachedLookupText: {
    fontSize: 14,
    fontWeight: 'bold',
    color: '#6E5528',
    letterSpacing: 1,
    marginTop: -2,
  },
  multilineInput: {
    height: 72,
    paddingTop: 8,
    paddingBottom: 8,
    textAlignVertical: 'top',
  },
  amountInputContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    flex: 1,
    maxWidth: 200,
  },
  currencyPrefix: {
    fontSize: 14,
    fontWeight: 'bold',
    color: '#8D6409',
    marginRight: 6,
  },
  amountInput: {
    fontWeight: 'bold',
    color: '#9E6D08',
    fontSize: 14,
  },

  // ===== MODALS & LOV =====
  modalOverlay: {
    flex: 1,
    backgroundColor: 'rgba(30, 20, 10, 0.45)',
    justifyContent: 'center',
    alignItems: 'center',
    padding: 16,
  },
  lovModalContainer: {
    width: '100%',
    maxWidth: 640,
    backgroundColor: '#FFFFFF',
    borderRadius: 10,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    overflow: 'hidden',
    shadowColor: '#000',
    shadowOpacity: 0.15,
    shadowRadius: 10,
    elevation: 6,
  },
  modalHeader: {
    backgroundColor: '#F5E7CA',
    paddingHorizontal: 14,
    paddingVertical: 10,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
  },
  modalTitle: {
    fontSize: 13,
    fontWeight: 'bold',
    color: '#3B2D17',
  },
  modalCloseBtn: {
    padding: 4,
  },
  modalSearchBox: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderRadius: 6,
    paddingHorizontal: 10,
    height: 32,
    marginBottom: 8,
  },
  modalSearchInput: {
    flex: 1,
    fontSize: 12,
    color: '#2B2118',
    paddingVertical: 0,
  },
  lovHeaderRow: {
    flexDirection: 'row',
    backgroundColor: '#F5E7CA',
    paddingVertical: 6,
    paddingHorizontal: 8,
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
  },
  lovHeaderText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3B2D17',
  },
  lovItemRow: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingVertical: 8,
    paddingHorizontal: 8,
    borderBottomWidth: 1,
    borderBottomColor: '#F3EBDD',
  },
  lovItemText: {
    fontSize: 12,
    color: '#3B2D17',
  },
  codeCell: {
    fontWeight: 'bold',
    color: '#8D6409',
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
