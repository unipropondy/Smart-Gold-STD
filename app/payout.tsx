/** Developer: durairaj */
import { ThemedText } from '@/components/themed-text';
import { API_BASE_URL } from '@/constants/api';
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
        const savedPayoutNo = (json.payoutNo || json.data || '').trim();
        // Automatic Print After Save matching frmPayOut.vb:
        // subPrintBill(txtPayOutNo.Text.Trim)
        if (savedPayoutNo !== '') {
          subPrintBill(savedPayoutNo);
        }
        handleClear();
      } else {
        showToast(json.message || "Failed to save payout detail.", "error");
      }
    } catch (e) {
      showToast("Server connection failed.", "error");
    } finally {
      setLoading(false);
    }
  };

  // Original Print Function matching subPrintBill(ByVal sInvoiceNo As String) in frmPayOut.vb
  // Report: Reports/rptPayOut.rpt
  // Selection: {tblPayoutdetail.PayOutNo} = @PayOutNo
  // Formula Fields: CompAddress1, CompAddress2, CompAddress3
  // Copy count: CR.PrintToPrinter(1, True, 0, 0)
  const subPrintBill = async (sInvoiceNo: string) => {
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

      const { company, payout, payModes, rates } = json.data;

      // Singapore Date & Time
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

      // Original Formula Field Values from frmPayOut.vb:
      // CompAddress1: sCompanyname
      // CompAddress2: sAddress (Address1 & " " & Address2 & " " & Country)
      // CompAddress3: sPhone & " " & sFax
      const sCompanyName = (company.name || 'SMART').trim();
      const sCompAddress2 = [company.address1, company.address2, company.country].filter(Boolean).join(' ');
      const sCompAddress3 = [company.phone, company.fax].filter(Boolean).join(' ');

      const s999 = rates?.rate999 || '-';
      const s916 = rates?.rate916 || '-';
      const s835 = rates?.rate835 || '-';

      const payoutAmt = parseFloat(payout.Amount) || 0;

      // Format date helper
      const formatDate = (val: any) => {
        if (!val) return '-';
        const d = new Date(val);
        if (isNaN(d.getTime())) return String(val).split('T')[0] || '-';
        return d.toLocaleDateString('en-GB', { day: '2-digit', month: '2-digit', year: 'numeric' });
      };

      const voucherDateFormatted = formatDate(payout.PayOutDate || payout.CreateDate);

      // Print HTML (1 single copy matching CR.PrintToPrinter(1, True, 0, 0))
      const printHtml = `
        <!DOCTYPE html>
        <html>
          <head>
            <meta charset="utf-8" />
            <title>PayOut Voucher - ${trimmedPayoutNo}</title>
            <style>
              @page {
                size: A4 portrait;
                margin: 0;
              }
              *, *:before, *:after {
                box-sizing: border-box;
              }
              body {
                font-family: 'Segoe UI', Tahoma, Arial, sans-serif;
                margin: 0;
                padding: 0;
                background-color: #525659;
                color: #222;
                -webkit-print-color-adjust: exact;
                print-color-adjust: exact;
              }
              .page-sheet {
                background: #FFFFFF;
                width: 210mm;
                min-height: 297mm;
                margin: 10mm auto;
                padding: 12mm 15mm;
                box-shadow: 0 4px 15px rgba(0, 0, 0, 0.35);
                display: flex;
                flex-direction: column;
                justify-content: space-between;
              }
              .report-header {
                display: flex;
                justify-content: space-between;
                align-items: flex-start;
                border-bottom: 2px solid #8D6409;
                padding-bottom: 8px;
              }
              .header-brand {
                display: flex;
                align-items: center;
                gap: 12px;
                max-width: 58%;
              }
              .brand-logo {
                width: 55px;
                height: 55px;
                object-fit: contain;
              }
              .company-name {
                font-size: 18px;
                font-weight: 800;
                color: #5C4A28;
                letter-spacing: 0.5px;
                text-transform: uppercase;
              }
              .company-address {
                font-size: 10px;
                color: #555;
                line-height: 1.35;
                margin-top: 2px;
              }
              .header-meta {
                text-align: right;
              }
              .report-badge {
                font-size: 13px;
                font-weight: 800;
                color: #8D6409;
                letter-spacing: 0.6px;
                text-transform: uppercase;
              }
              .copy-tag {
                display: inline-block;
                background: #FDF4D9;
                color: #7D5E19;
                font-size: 9px;
                font-weight: 700;
                padding: 2px 8px;
                border-radius: 4px;
                margin-top: 3px;
                border: 1px solid #E6D29F;
              }
              .doc-no {
                font-size: 12px;
                font-weight: 700;
                color: #111;
                margin-top: 4px;
              }
              .doc-no span {
                color: #8D6409;
                font-family: monospace;
                font-size: 13px;
              }
              .meta-date {
                font-size: 10px;
                color: #666;
                margin-top: 2px;
              }

              /* GOLD RATE STRIP */
              .gold-rate-strip {
                display: flex;
                justify-content: center;
                align-items: center;
                gap: 16px;
                background: #FAF6EB;
                border: 1px solid #E6D7AD;
                padding: 5px 12px;
                margin: 8px 0;
                border-radius: 4px;
                font-size: 10px;
              }
              .rate-lbl {
                font-weight: 600;
                color: #6E5316;
              }
              .rate-val {
                font-weight: 800;
                color: #222;
                margin-left: 4px;
              }
              .rate-divider {
                color: #D4C3A3;
              }

              /* TWO-COLUMN INFO */
              .info-grid {
                display: grid;
                grid-template-columns: 1fr 1fr;
                gap: 12px;
                margin-bottom: 8px;
              }
              .info-card {
                background: #FAF8F4;
                border: 1px solid #E5DFD4;
                border-radius: 4px;
                padding: 8px 10px;
              }
              .card-title {
                font-size: 10px;
                font-weight: 700;
                color: #7D5E19;
                border-bottom: 1px solid #E5DFD4;
                padding-bottom: 3px;
                margin-bottom: 4px;
                text-transform: uppercase;
              }
              .info-row {
                font-size: 10px;
                line-height: 1.4;
                display: flex;
              }
              .info-lbl {
                width: 32%;
                color: #666;
                font-weight: 500;
              }
              .info-val {
                width: 68%;
                color: #222;
              }
              .font-bold {
                font-weight: 700;
              }

              /* CURRENT PAYOUT CARD */
              .current-payout-card {
                background: #FFF8E1;
                border: 1.5px solid #FFE082;
                border-radius: 6px;
                padding: 10px 14px;
                text-align: center;
                margin: 6px 0 10px 0;
              }
              .payout-card-title {
                font-size: 11px;
                font-weight: 700;
                color: #7D5E19;
                letter-spacing: 0.5px;
                text-transform: uppercase;
              }
              .payout-card-amount {
                font-size: 24px;
                font-weight: 800;
                color: #B71C1C;
                margin-top: 2px;
                font-family: monospace;
              }

              .paymode-strip {
                margin-top: 4px;
                font-size: 10px;
                color: #7D5E19;
                font-weight: 600;
              }

              .section-heading {
                font-size: 11px;
                font-weight: 700;
                color: #5C4A28;
                text-transform: uppercase;
                margin: 6px 0 4px 0;
                border-bottom: 1px solid #D4C3A3;
                padding-bottom: 2px;
              }

              /* TABLE */
              .receipt-table {
                width: 100%;
                border-collapse: collapse;
                margin-bottom: 8px;
                font-size: 10px;
              }
              .receipt-table th {
                background: #F1ECE0;
                color: #4A3A1E;
                font-weight: 700;
                padding: 6px 8px;
                border: 1px solid #D8CFC0;
              }
              .receipt-table td {
                padding: 6px 8px;
                border: 1px solid #E5DFD4;
                color: #222;
              }

              /* SUMMARY */
              .summary-box {
                margin-left: auto;
                width: 60%;
                background: #FAF8F4;
                border: 1px solid #E5DFD4;
                border-radius: 4px;
                padding: 8px 12px;
                margin-bottom: 10px;
              }
              .summary-row {
                display: flex;
                justify-content: space-between;
                font-size: 10px;
                line-height: 1.6;
              }
              .summary-divider {
                border-top: 1px dashed #C9BC9F;
                margin: 4px 0;
              }
              .grand-total-row {
                padding-top: 2px;
              }

              /* SIGNATURE */
              .signature-section {
                display: grid;
                grid-template-columns: 1fr 1fr;
                gap: 20px;
                margin-top: 16px;
              }
              .sign-card {
                border: 1px dashed #B8AB91;
                border-radius: 4px;
                padding: 8px 10px;
                font-size: 10px;
                background: #FFFEFC;
              }
              .sign-header {
                font-weight: 700;
                color: #5C4A28;
                border-bottom: 1px solid #EDE6D8;
                padding-bottom: 3px;
                margin-bottom: 6px;
                font-size: 9px;
              }
              .sign-row {
                color: #333;
                line-height: 1.4;
              }

              /* FOOTER */
              .report-footer {
                margin-top: 8px;
                text-align: center;
              }
              .footer-divider {
                border-top: 1px solid #E0D7C5;
                margin-bottom: 4px;
              }
              .footer-gen-text {
                font-size: 9px;
                color: #777;
                font-style: italic;
              }
              .footer-powered {
                font-size: 8px;
                color: #999;
                margin-top: 1px;
              }

              @media print {
                body {
                  background: none !important;
                }
                .page-sheet {
                  width: 100% !important;
                  min-height: 297mm !important;
                  height: 100% !important;
                  margin: 0 !important;
                  padding: 10mm 12mm !important;
                  box-shadow: none !important;
                  display: flex !important;
                  flex-direction: column !important;
                }
              }
            </style>
          </head>
          <body>
            <div class="page-sheet">
              <!-- BRAND / HEADER -->
              <div class="report-header">
                <div class="header-brand">
                  <img src="${UNIPRO_LOGO_DATA_URI}" alt="Logo" class="brand-logo" />
                  <div class="brand-details">
                    <div class="company-name">${sCompanyName}</div>
                    <div class="company-address">${sCompAddress2}</div>
                    ${sCompAddress3 ? `<div class="company-address">${sCompAddress3}</div>` : ''}
                  </div>
                </div>
                <div class="header-meta">
                  <div class="report-badge">PAYOUT VOUCHER</div>
                  <div class="copy-tag">OFFICIAL VOUCHER</div>
                  <div class="doc-no">VOUCHER NO: <span>${trimmedPayoutNo}</span></div>
                  <div class="meta-date">Date: ${voucherDateFormatted} &nbsp;|&nbsp; ${sgTimeStr}</div>
                </div>
              </div>

              <!-- GOLD RATES BANNER -->
              <div class="gold-rate-strip">
                <div class="rate-cell"><span class="rate-lbl">Gold 999:</span> <span class="rate-val">$${s999}/g</span></div>
                <div class="rate-divider">|</div>
                <div class="rate-cell"><span class="rate-lbl">Gold 916 (22K):</span> <span class="rate-val">$${s916}/g</span></div>
                <div class="rate-divider">|</div>
                <div class="rate-cell"><span class="rate-lbl">Gold 835 (20K):</span> <span class="rate-val">$${s835}/g</span></div>
              </div>

              <!-- TWO-COLUMN INFO: PAYOUT EXPENSE & DISBURSEMENT DETAILS -->
              <div class="info-grid">
                <!-- Payout Category Card -->
                <div class="info-card">
                  <div class="card-title">EXPENSE CLASSIFICATION</div>
                  <div class="info-row"><span class="info-lbl">Expense Code:</span> <span class="info-val font-bold">${payout.Code || '-'}</span></div>
                  <div class="info-row"><span class="info-lbl">Category:</span> <span class="info-val">${payout.Description || '-'}</span></div>
                  <div class="info-row"><span class="info-lbl">Disbursement:</span> <span class="info-val font-bold">${payModes.length > 0 ? payModes.map((p: any) => p.payMode).join(', ') : 'CASH'}</span></div>
                </div>

                <!-- Voucher Specification Card -->
                <div class="info-card">
                  <div class="card-title">VOUCHER AUDIT DETAILS</div>
                  <div class="info-row"><span class="info-lbl">Voucher No:</span> <span class="info-val font-bold">${trimmedPayoutNo}</span></div>
                  <div class="info-row"><span class="info-lbl">Issued Date:</span> <span class="info-val">${voucherDateFormatted}</span></div>
                  <div class="info-row"><span class="info-lbl">Processed By:</span> <span class="info-val font-bold">${payout.CreateUser || 'sys'}</span></div>
                </div>
              </div>

              <!-- CURRENT PAYOUT HIGHLIGHT CARD -->
              <div class="current-payout-card">
                <div class="payout-card-title">TOTAL AMOUNT PAID OUT</div>
                <div class="payout-card-amount">$ ${payoutAmt.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</div>
                <div class="paymode-strip">SETTLED IN FULL VIA CASH</div>
              </div>

              <!-- PARTICULARS / EXPENSE BREAKDOWN TABLE -->
              <div class="section-heading">PAYOUT PARTICULARS & DETAILS</div>
              <table class="receipt-table">
                <thead>
                  <tr>
                    <th style="width: 10%; text-align: center;">#</th>
                    <th style="width: 25%; text-align: left;">Code / Category</th>
                    <th style="width: 45%; text-align: left;">Payout Description / Purpose</th>
                    <th style="width: 20%; text-align: right;">Amount ($)</th>
                  </tr>
                </thead>
                <tbody>
                  <tr>
                    <td style="text-align: center;">1</td>
                    <td style="text-align: left; font-weight: 600;">${payout.Code} - ${payout.Description}</td>
                    <td style="text-align: left;">${payout.PayOutDescription || '-'}</td>
                    <td style="text-align: right; font-weight: 700; color: #B71C1C;">$ ${payoutAmt.toFixed(2)}</td>
                  </tr>
                  ${
                    payout.Remarks
                      ? `
                    <tr>
                      <td style="text-align: center;">-</td>
                      <td colspan="3" style="text-align: left; font-style: italic; color: #555;"><strong>Remarks:</strong> ${payout.Remarks}</td>
                    </tr>
                  `
                      : ''
                  }
                </tbody>
              </table>

              <!-- SUMMARY BOX -->
              <div class="summary-box">
                <div class="summary-row">
                  <span class="sum-lbl">Subtotal Payout:</span>
                  <span class="sum-val">$ ${payoutAmt.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</span>
                </div>
                <div class="summary-divider"></div>
                <div class="summary-row grand-total-row">
                  <span class="sum-lbl font-bold" style="font-size: 13px;">NET CASH DISBURSED:</span>
                  <span class="sum-val font-bold" style="font-size: 15px; color: #B71C1C;">
                    $ ${payoutAmt.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}
                  </span>
                </div>
              </div>

              <!-- SIGNATURE SECTION -->
              <div class="signature-section">
                <div class="sign-card">
                  <div class="sign-header">PREPARED BY (CASHIER / STAFF)</div>
                  <div class="sign-row">Staff: ${payout.CreateUser || 'sys'}</div>
                  <div class="sign-row" style="margin-top: 18px;">Signature: ....................................................</div>
                </div>
                <div class="sign-card">
                  <div class="sign-header">RECEIVED BY / AUTHORIZED SIGNATORY</div>
                  <div class="sign-row">Payee Name: ................................................</div>
                  <div class="sign-row" style="margin-top: 18px;">Signature: ....................................................</div>
                </div>
              </div>

              <!-- FOOTER -->
              <div class="report-footer">
                <div class="footer-divider"></div>
                <div class="footer-gen-text">*** System Generated PayOut Voucher ***</div>
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
          showToast('PayOut voucher preview opened', 'success');
        } else {
          const blob = new Blob([printHtml], { type: 'text/html;charset=utf-8;' });
          const url = URL.createObjectURL(blob);
          const win = window.open(url, '_blank');
          if (!win) {
            showToast('Popup blocker prevented opening print preview. Please allow popups.', 'error');
          } else {
            showToast('PayOut voucher preview opened', 'success');
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

  // Original VB.NET btnPrint_Click:
  // If txtPayOutNo.Text.Trim <> "" Then
  //     subPrintBill(txtPayOutNo.Text.Trim)
  // End If
  const handlePrint = () => {
    if (payoutNo.trim() !== '') {
      subPrintBill(payoutNo.trim());
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
          {/* Print */}
          <TouchableOpacity
            style={[styles.actionBtn, styles.clearBtn]}
            onPress={handlePrint}
            activeOpacity={0.8}
            disabled={loading}
          >
            <Ionicons name="print-outline" size={14} color="#5C4A28" />
            <ThemedText style={styles.clearBtnText}>Print</ThemedText>
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
