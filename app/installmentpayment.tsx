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

const API_URL = `${API_BASE_URL}/installmentpayment`;
const CUSTOMER_API_URL = `${API_BASE_URL}/customer`;
const FLEXI_API_URL = `${API_BASE_URL}/paymententry/flexi`;

export default function InstallmentPaymentScreen() {
  const themeColors = useThemeColors();
  const router = useRouter();
  const params = useLocalSearchParams();

  const [loading, setLoading] = useState(false);
  const [toast, setToast] = useState<{ message: string; type: 'success' | 'error' } | null>(null);

  const reprintInputRef = useRef<any>(null);

  // Form State
  const [agreementNo, setAgreementNo] = useState('');
  const [customerCode, setCustomerCode] = useState('');
  const [customerType, setCustomerType] = useState('');
  const [flexiId, setFlexiId] = useState('');
  const [customerName, setCustomerName] = useState('');
  const [address, setAddress] = useState('');
  const [remarks, setRemarks] = useState('');

  // Payment Calculation State
  const [noOfInstallment, setNoOfInstallment] = useState('1');
  const [installmentAmount, setInstallmentAmount] = useState('0.00');
  const [totalAmount, setTotalAmount] = useState('0.00');

  // Footer Totals
  const [salesAmount, setSalesAmount] = useState('0.00');
  const [depositAmount, setDepositAmount] = useState('0.00');
  const [balanceAmount, setBalanceAmount] = useState('0.00');

  // Trade-in
  const [tradeInOption, setTradeInOption] = useState<'Gold' | 'Cash' | 'Both'>('Gold');
  const [tradeInAmount, setTradeInAmount] = useState('0.00');
  const [companyTradeInLimit, setCompanyTradeInLimit] = useState('0.00');

  // History
  const [historyList, setHistoryList] = useState<any[]>([]);

  // Cancel & Reprint modal states
  const [isCancelModalVisible, setIsCancelModalVisible] = useState(false);
  const [cancelBillNo, setCancelBillNo] = useState('');
  const [isReprintModalVisible, setIsReprintModalVisible] = useState(false);
  const [reprintBillNo, setReprintBillNo] = useState('');

  // Standardized Print Report Modal state
  const [isPrintModalVisible, setIsPrintModalVisible] = useState(false);
  const [activePrintInstallmentNo, setActivePrintInstallmentNo] = useState('');
  const [activePrintIsReprint, setActivePrintIsReprint] = useState(false);

  // LOV State
  const [isAgreementModalVisible, setIsAgreementModalVisible] = useState(false);
  const [agreementList, setAgreementList] = useState<any[]>([]);
  const [agreementSearchQuery, setAgreementSearchQuery] = useState('');

  const [isCustomerModalVisible, setIsCustomerModalVisible] = useState(false);
  const [customerList, setCustomerList] = useState<any[]>([]);
  const [customerSearchQuery, setCustomerSearchQuery] = useState('');

  const [isFlexiModalVisible, setIsFlexiModalVisible] = useState(false);
  const [flexiList, setFlexiList] = useState<any[]>([]);
  const [flexiSearchQuery, setFlexiSearchQuery] = useState('');

  const showToast = (message: string, type: 'success' | 'error' = 'success') => {
    setToast({ message, type });
    setTimeout(() => setToast(null), 3000);
  };

  const fetchAgreement = async () => {
    if (!agreementNo) return;
    setLoading(true);
    try {
      const res = await fetch(`${API_URL}/agreement/${encodeURIComponent(agreementNo)}`);
      const json = await res.json();
      if (json.success && json.data) {
        const data = json.data;
        const totalCost = data.TotalCost || 0;
        const depAmt = data.DepositAmt || 0;
        const insAmt = data.InstallmentAmt || 0;
        const amtPaid = data.AmtPaid || 0;

        if (amtPaid >= totalCost && totalCost > 0) {
          showToast('Installment Amount was already fully paid!', 'error');
          handleClear();
          return;
        }

        setCustomerCode(data.CustomerCode || '');
        setCustomerName(data.Name || '');
        setCustomerType(data.CustomerType || data.MemberType || '');
        setAddress(`${data.Address1 || ''} ${data.Address2 || ''}`.trim());

        setSalesAmount(totalCost.toFixed(2));
        setDepositAmount(depAmt.toFixed(2));

        // Fetch Balance
        fetchBalance(agreementNo, totalCost, depAmt, insAmt, data.InsTotal, data.InsPaid);
        fetchHistory(agreementNo);
        showToast('Agreement details loaded', 'success');
      } else {
        showToast(json.message || 'Agreement not found', 'error');
      }
    } catch (err) {
      showToast('Error fetching agreement', 'error');
    } finally {
      setLoading(false);
    }
  };

  const fetchBalance = async (agrNo: string, totalCost: number, depositAmt: number, agreementInsAmt: number = 0, insTotal: number = 0, insPaid: number = 0) => {
    try {
      const res = await fetch(`${API_URL}/balance/${encodeURIComponent(agrNo)}`);
      const json = await res.json();
      if (json.success) {
        const amountPaid = json.data.AmountPaid || 0;
        const bal = totalCost - depositAmt - amountPaid;
        const balFixed = bal > 0 ? bal.toFixed(2) : '0.00';
        setBalanceAmount(balFixed);

        // Adjust default installment amount based on remaining balance (matching frmInstallmentPayment.vb)
        let computedInsAmt = agreementInsAmt > 0 ? agreementInsAmt : parseFloat(installmentAmount) || 0;
        const pendingIns = (insTotal || 0) - (insPaid || 0);

        if (bal > 0 && computedInsAmt > bal) {
          computedInsAmt = bal;
        } else if (bal > 0 && pendingIns === 1 && computedInsAmt < bal) {
          computedInsAmt = bal;
        }
        setInstallmentAmount(computedInsAmt.toFixed(2));
      }
    } catch (err) {
      console.error(err);
    }
  };

  const fetchHistory = async (agrNo: string) => {
    try {
      const res = await fetch(`${API_URL}/installment-details/${encodeURIComponent(agrNo)}`);
      const json = await res.json();
      if (json.success) {
        setHistoryList(json.data || []);
      }
    } catch (err) {
      console.error(err);
    }
  };

  const handleOpenAgreementLookup = async () => {
    setLoading(true);
    try {
      const res = await fetch(`${API_URL}/agreements`);
      const json = await res.json();
      if (json.success) {
        setAgreementList(json.data || []);
        setIsAgreementModalVisible(true);
      } else {
        showToast('Failed to load agreements', 'error');
      }
    } catch (err) {
      showToast('Error querying agreements', 'error');
    } finally {
      setLoading(false);
    }
  };

  const handleSelectAgreement = (agr: any) => {
    setAgreementNo(agr.InsAgreementNo);
    setIsAgreementModalVisible(false);
  };

  const handleOpenCustomerLookup = async () => {
    setLoading(true);
    try {
      const res = await fetch(CUSTOMER_API_URL);
      const json = await res.json();
      if (json.success) {
        setCustomerList(json.data || []);
        setIsCustomerModalVisible(true);
      } else {
        showToast('Failed to load customer list', 'error');
      }
    } catch (err) {
      showToast('Error querying customer directory', 'error');
    } finally {
      setLoading(false);
    }
  };

  const handleSelectCustomer = (cust: any) => {
    setCustomerCode(cust.CustomerCode ? cust.CustomerCode.trim() : '');
    setCustomerName(cust.CustomerName ? cust.CustomerName.trim() : '');
    setAddress(`${cust.Address1 || ''} ${cust.Address2 || ''}`.trim());
    setIsCustomerModalVisible(false);
  };

  const handleOpenFlexiLookup = async () => {
    setLoading(true);
    try {
      const res = await fetch(FLEXI_API_URL);
      const json = await res.json();
      if (json.success) {
        setFlexiList(json.data || []);
        setIsFlexiModalVisible(true);
      } else {
        showToast('Failed to load Flexi list', 'error');
      }
    } catch (err) {
      showToast('Error querying Flexi directory', 'error');
    } finally {
      setLoading(false);
    }
  };

  const handleSelectFlexi = (flexi: any) => {
    setFlexiId(flexi.SubscriptionID ? String(flexi.SubscriptionID).trim() : '');
    setIsFlexiModalVisible(false);
  };

  // Trigger fetch when agreementNo is selected from LOV
  useEffect(() => {
    if (agreementNo) {
      fetchAgreement();
    }
  }, [agreementNo]);

  // Fetch company trade-in limit
  useEffect(() => {
    const fetchCompanyConfig = async () => {
      try {
        const res = await fetch(`${API_BASE_URL}/installmentorderentry/company`);
        const data = await res.json();
        if (data && data.success && data.tradeInLimit !== undefined) {
          setCompanyTradeInLimit(parseFloat(data.tradeInLimit || 0).toFixed(2));
        }
      } catch (e) {
        console.warn('Failed to fetch company trade in limit', e);
      }
    };
    fetchCompanyConfig();
  }, []);

  // Handle returning from Trade-In screen
  useEffect(() => {
    if (params?.tradeInAmount) {
      const returnedTradeIn = parseFloat(params.tradeInAmount as string) || 0;
      setTradeInAmount(returnedTradeIn.toFixed(2));
      if (params.agreementNo && !agreementNo) {
        setAgreementNo(String(params.agreementNo));
      }
      showToast(`Trade-In applied: $${returnedTradeIn.toFixed(2)}`, 'success');
    }
  }, [params?.tradeInAmount, params?.agreementNo]);

  // Navigate to Trade-In screen following frmInstallment_AJ.vb / frmInstallmentPayment.vb logic
  const handleNavigateToTradeIn = () => {
    let refNo = agreementNo.trim();
    if (!refNo) {
      refNo = '00000001';
    }

    // Determine sales amount / total reference amount
    const salesAmt = parseFloat(salesAmount) || parseFloat(totalAmount) || parseFloat(balanceAmount) || 0;

    const navParams: Record<string, string> = {
      calledFrom: 'INSTALLMENTPAYMENT',
      sCalledFrom: 'INSTALLMENTPAYMENT',
      invoiceNo: refNo,
      sInvoiceNo: refNo,
      customerCode: customerCode || '',
      customerName: customerName || '',
      customerType: customerType || 'Customer',
      address1: address || '',
      address2: '',
      address3: '',
      tradeInLimit: companyTradeInLimit || '0.00',
      salesAmount: salesAmt.toFixed(2),
      sSalesAmount: salesAmt.toFixed(2),
      remarks: remarks || '',
      mode: 'InstallmentPayment',
      paymentMode: tradeInOption,
    };

    router.push({
      pathname: '/tradeingold',
      params: navParams,
    });
  };

  // Recalculate total amount when inputs change
  useEffect(() => {
    const qty = parseInt(noOfInstallment) || 0;
    const amt = parseFloat(installmentAmount) || 0;
    setTotalAmount((qty * amt).toFixed(2));
  }, [noOfInstallment, installmentAmount]);

  const handleClear = () => {
    setAgreementNo('');
    setCustomerCode('');
    setCustomerType('');
    setFlexiId('');
    setCustomerName('');
    setAddress('');
    setRemarks('');
    setNoOfInstallment('1');
    setInstallmentAmount('0.00');
    setTotalAmount('0.00');
    setSalesAmount('0.00');
    setDepositAmount('0.00');
    setBalanceAmount('0.00');
    setHistoryList([]);
  };

  const handleCancelBill = async () => {
    if (!cancelBillNo || cancelBillNo.trim() === '') {
      showToast('Please enter the Installment Bill No!', 'error');
      return;
    }
    setLoading(true);
    try {
      const res = await fetch(`${API_URL}/cancel`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ installmentNo: cancelBillNo.trim() }),
      });
      const data = await res.json();
      if (res.ok && data.success) {
        showToast(data.message || 'Bill cancelled successfully', 'success');
        setIsCancelModalVisible(false);
        setCancelBillNo('');
        if (agreementNo) {
          fetchAgreement();
        }
      } else {
        showToast(data.message || 'Failed to cancel bill', 'error');
      }
    } catch (e: any) {
      showToast(e.message || 'Error communicating with server', 'error');
    } finally {
      setLoading(false);
    }
  };

  // Build Standardized Cambria HTML matching Purchase Return / Deposit Receipt
  const buildInstallmentReceiptHtml = (sInvoiceNo: string, isReprintMode: boolean, printData: any) => {
    const { company, payment, history, rates, payModes } = printData;

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

    const sCustomerName = (payment?.CustomerName || customerName || '').trim();
    const sCustomerCode = (payment?.CustomerCode || customerCode || '').trim();
    const sCustomerAddress1 = (payment?.CustomerAddress1 || address || '').trim();
    const sCustomerAddress2 = (payment?.CustomerAddress2 || '').trim();
    const sCustomerPhone = (payment?.CustomerPhone || '').trim();

    const s999 = rates?.rate999 || '-';
    const s916 = rates?.rate916 || '-';
    const s835 = rates?.rate835 || '-';

    const sAgreementNo = (payment?.InsAgreementNo || agreementNo || '').trim();
    const sInstallmentNo = (payment?.InstallmentNo || sInvoiceNo).trim();
    const currentAmtPaid = parseFloat(payment?.AmtPaid) || 0;
    const currentInsAmt = parseFloat(payment?.InstallmentAmt) || 0;
    const totalAgreementCost = parseFloat(payment?.TotalCost) || parseFloat(salesAmount) || 0;
    const depositAmt = parseFloat(payment?.DepositAmt) || parseFloat(depositAmount) || 0;

    let totalInstallmentsPaid = 0;
    (history || []).forEach((h: any) => {
      totalInstallmentsPaid += parseFloat(h.AmtPaid) || 0;
    });
    const grandTotalPaid = depositAmt + totalInstallmentsPaid;
    const balanceDue = Math.max(0, totalAgreementCost - grandTotalPaid);

    const formatDate = (val: any) => {
      if (!val) return '-';
      const d = new Date(val);
      if (isNaN(d.getTime())) return String(val).split('T')[0] || '-';
      return d.toLocaleDateString('en-GB', { day: '2-digit', month: '2-digit', year: 'numeric' });
    };

    const paymentDateFormatted = formatDate(payment?.CreateDate || new Date());
    const sReportTitle = 'INSTALLMENT PAYMENT';

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
              printedDateTime: `${paymentDateFormatted} ${sgTimeStr}`
            })}

            <!-- TWO-COLUMN INFO: CUSTOMER vs INSTALLMENT AGREEMENT -->
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
                <div class="card-title">AGREEMENT SPECIFICATION</div>
                <div class="info-row"><span class="info-lbl">Agreement No:</span> <span class="info-val font-bold">${sAgreementNo}</span></div>
                <div class="info-row"><span class="info-lbl">Installment Paid:</span> <span class="info-val">${payment?.InsPaid || 1} installment(s)</span></div>
                <div class="info-row"><span class="info-lbl">Installment Amt:</span> <span class="info-val font-bold">$ ${currentInsAmt.toFixed(2)}</span></div>
                <div class="info-row"><span class="info-lbl">Pay Mode:</span> <span class="info-val font-bold">${payModes && payModes.length > 0 ? payModes.map((p: any) => p.payMode).join(', ') : 'CASH'}</span></div>
                <div class="info-row"><span class="info-lbl">Remarks:</span> <span class="info-val">${payment?.Remarks || '-'}</span></div>
              </div>
            </div>

            <!-- CURRENT PAYMENT VOUCHER HIGHLIGHT -->
            <div class="current-payment-card">
              <div class="payment-title">PAYMENT RECEIVED THIS VOUCHER</div>
              <div class="payment-amount">$ ${currentAmtPaid.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</div>
            </div>

            <!-- PAYMENT HISTORY / ACCUMULATED LEDGER -->
            <div class="table-section-title">INSTALLMENT PAYMENT HISTORY / STATEMENT</div>
            <table class="detail-table">
              <thead>
                <tr>
                  <th style="width: 6%; text-align: center;">#</th>
                  <th style="width: 26%; text-align: left;">INSTALLMENT VOUCHER</th>
                  <th style="width: 18%; text-align: center;">DATE</th>
                  <th style="width: 14%; text-align: center;">INS #</th>
                  <th style="width: 18%; text-align: right;">INSTALLMENT AMT ($)</th>
                  <th style="width: 18%; text-align: right;">AMOUNT PAID ($)</th>
                </tr>
              </thead>
              <tbody>
                ${(history && history.length > 0)
                  ? history.map((h: any, idx: number) => `
                    <tr class="${h.InstallmentNo === sInstallmentNo ? 'highlight-row' : ''}">
                      <td style="text-align: center; font-weight: 700;">${idx + 1}</td>
                      <td style="text-align: left; font-weight: 700;">${h.InstallmentNo} ${h.InstallmentNo === sInstallmentNo ? '<span style="color: #8C5D08;">*(This Voucher)*</span>' : ''}</td>
                      <td style="text-align: center; font-weight: 700;">${formatDate(h.CreateDate)}</td>
                      <td style="text-align: center; font-weight: 700;">${h.InsPaid || 1}</td>
                      <td style="text-align: right; font-weight: 700;">$ ${parseFloat(h.InstallmentAmt || 0).toFixed(2)}</td>
                      <td style="text-align: right; font-weight: 800; color: #8C5D08;">$ ${parseFloat(h.AmtPaid || 0).toFixed(2)}</td>
                    </tr>
                  `).join('')
                  : `
                    <tr>
                      <td style="text-align: center; font-weight: 700;">1</td>
                      <td style="text-align: left; font-weight: 700;">${sInstallmentNo}</td>
                      <td style="text-align: center; font-weight: 700;">${paymentDateFormatted}</td>
                      <td style="text-align: center; font-weight: 700;">${payment?.InsPaid || 1}</td>
                      <td style="text-align: right; font-weight: 700;">$ ${currentInsAmt.toFixed(2)}</td>
                      <td style="text-align: right; font-weight: 800; color: #8C5D08;">$ ${currentAmtPaid.toFixed(2)}</td>
                    </tr>
                  `
                }
              </tbody>
            </table>

            <!-- SUMMARY FINANCIAL BALANCES -->
            <div class="totals-section">
              <div class="totals-box">
                <div class="total-row">
                  <span>Total Agreement Value:</span>
                  <span>$ ${totalAgreementCost.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</span>
                </div>
                <div class="total-row">
                  <span>Initial Deposit Paid:</span>
                  <span>$ ${depositAmt.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</span>
                </div>
                <div class="total-row">
                  <span>Total Installments Paid:</span>
                  <span>$ ${totalInstallmentsPaid.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</span>
                </div>
                <div class="total-row">
                  <span>Total Paid To Date:</span>
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
              staffName: (payment?.CreateUser || 'Staff').trim(),
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
          <title>${sReportTitle} - ${sInstallmentNo}</title>
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
              width: 120px;
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

            /* CURRENT PAYMENT HIGHLIGHT CARD */
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
            .highlight-row td {
              background: #FFF8E7 !important;
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

  // Original Print Function matching subPrintInstallment(ByVal sInvoiceNo As String) in frmInstallmentPayment.vb
  const subPrintInstallment = async (targetInstallmentNo: string, isReprintMode: boolean = false) => {
    const sInvoiceNo = (targetInstallmentNo || '').trim();
    if (!sInvoiceNo) {
      return;
    }

    try {
      setLoading(true);

      const res = await fetch(`${API_URL}/print-data?installmentNo=${encodeURIComponent(sInvoiceNo)}`);
      const json = await res.json();

      if (!json.success || !json.data) {
        showToast(json.message || `Installment payment voucher ${sInvoiceNo} not found for print.`, 'error');
        setLoading(false);
        return;
      }

      const htmlContent = buildInstallmentReceiptHtml(sInvoiceNo, isReprintMode, json.data);
      const docTitle = isReprintMode ? `Installment Payment Receipt (Duplicate) - ${sInvoiceNo}` : `Installment Payment Receipt - ${sInvoiceNo}`;

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
        const frameId = '__installment_payment_print_frame__';
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
              showToast(`Installment payment ${sInvoiceNo} print dialog opened`, 'success');
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
            showToast(`Installment Payment receipt preview opened`, 'success');
          } else {
            showToast('Popup blocker prevented opening print preview. Please allow popups.', 'error');
          }
        }
      } else {
        showToast('Print preview generated.', 'success');
      }
    } catch (err: any) {
      console.error('Print Installment Receipt Error:', err);
      showToast(err.message || 'Failed to print installment payment receipt', 'error');
    } finally {
      setLoading(false);
    }
  };

  // Dedicated PDF Download Functionality
  const handleDownloadPdf = async (sInvoiceNo?: string, isReprint: boolean = false) => {
    const rawNo = (sInvoiceNo || '').trim();
    if (!rawNo) {
      showToast('Validation Error: Installment Voucher No is required to download PDF', 'error');
      return;
    }

    try {
      setLoading(true);
      const res = await fetch(`${API_URL}/print-data?installmentNo=${encodeURIComponent(rawNo)}`);
      const json = await res.json();

      if (!json.success || !json.data) {
        showToast(json.message || `Installment payment record "${rawNo}" not found`, 'error');
        return;
      }

      const htmlContent = buildInstallmentReceiptHtml(rawNo, isReprint, json.data);
      const downloadFilename = `Installment_Payment_${rawNo.replace(/[^a-zA-Z0-9_-]/g, '_')}.pdf`;

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
      showToast('Validation Error: Installment Voucher No is required to download Excel', 'error');
      return;
    }

    try {
      setLoading(true);
      const excelRes = await fetch(`${API_URL}/export-excel`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          installmentNo: rawNo,
          isReprint: isReprint
        })
      });

      if (!excelRes.ok) {
        const errJson = await excelRes.json().catch(() => ({}));
        throw new Error(errJson.message || 'Server error generating Excel file');
      }

      const excelBlob = await excelRes.blob();
      const downloadFilename = `Installment_Payment_${rawNo.replace(/[^a-zA-Z0-9_-]/g, '_')}.xlsx`;

      if (Platform.OS === 'web' && typeof window !== 'undefined') {
        const url = URL.createObjectURL(excelBlob);
        const a = document.createElement('a');
        a.href = url;
        a.download = downloadFilename;
        document.body.appendChild(a);
        a.click();
        document.body.removeChild(a);
        setTimeout(() => URL.revokeObjectURL(url), 1000);
        showToast(`Installment Payment Excel downloaded: ${downloadFilename}`, 'success');
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

  // Handle return from payingmode after save
  useEffect(() => {
    if (params?.paymentSuccess === 'true') {
      const savedPaymentNo = (params.installmentNo as string) || (params.savedInvoiceNo as string) || (params.paymentNo as string);
      if (savedPaymentNo && savedPaymentNo.trim()) {
        const cleanNo = savedPaymentNo.trim();
        setActivePrintInstallmentNo(cleanNo);
        setActivePrintIsReprint(false);
        setIsPrintModalVisible(true);
      }
      if (agreementNo && agreementNo.trim()) {
        fetchAgreement();
      }
    }
  }, [params?.paymentSuccess]);

  // RePrint button handler matching original btnRePrint_Click:
  // Clears txtRePrintBillNo, opens modal, focuses on input
  const handleOpenReprintModal = () => {
    setReprintBillNo('');
    setIsReprintModalVisible(true);
    setTimeout(() => {
      if (reprintInputRef.current) {
        reprintInputRef.current.focus();
      }
    }, 150);
  };

  // RePrint OK click handler matching original btnRePrintOK_Click:
  // If txtRePrintBillNo.Text.Trim = "" Then Exit Sub
  const handleReprintSubmit = () => {
    const cleanNo = reprintBillNo.trim();
    if (!cleanNo) {
      return;
    }
    setIsReprintModalVisible(false);
    setActivePrintInstallmentNo(cleanNo);
    setActivePrintIsReprint(true);
    setIsPrintModalVisible(true);
  };

  const latestState = useRef({ agreementNo, totalAmount, noOfInstallment, installmentAmount, customerCode });
  useEffect(() => {
    latestState.current = { agreementNo, totalAmount, noOfInstallment, installmentAmount, customerCode };
  }, [agreementNo, totalAmount, noOfInstallment, installmentAmount, customerCode]);

  const handleSave = async () => {
    const currentState = latestState.current;

    if (!currentState.agreementNo || currentState.agreementNo.trim() === '') {
      showToast('Please enter the Installment Agreement No!', 'error');
      return;
    }

    const qty = parseFloat(currentState.noOfInstallment);
    if (isNaN(qty) || qty <= 0) {
      showToast('No. of Installment should be greater than zero!', 'error');
      return;
    }

    const amt = parseFloat(currentState.installmentAmount);
    if (isNaN(amt) || amt <= 0) {
      showToast('Installment Amount should be greater than zero!', 'error');
      return;
    }

    const tot = parseFloat(currentState.totalAmount) || 0;
    if (tot <= 0) {
      showToast('Total amount must be greater than zero!', 'error');
      return;
    }

    const balanceLeft = parseFloat(balanceAmount) || 0;
    if (balanceLeft < 0) {
      showToast('Total Installment Amount exceeds the balance Amount!', 'error');
      return;
    }

    setLoading(true);
    try {
      const payload = {
        agreementNo: currentState.agreementNo,
        installmentNo: `IN-${Date.now()}`,
        noOfInstallment: currentState.noOfInstallment,
        installmentAmount: currentState.installmentAmount,
        totalAmount: currentState.totalAmount,
        customerCode: currentState.customerCode,
      };

      router.push({
        pathname: '/payingmode',
        params: {
          totalNet: currentState.totalAmount,
          customerCode: currentState.customerCode,
          customerType,
          apiEndpoint: `${API_URL}/pay`,
          payload: JSON.stringify(payload),
          returnPath: '/installmentpayment',
        },
      });

      handleClear();
    } catch (err: any) {
      showToast(err.message || 'Error navigating to payment', 'error');
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
            <ThemedText style={styles.headerTitle}>Installment Payment</ThemedText>
            <ThemedText style={styles.headerSubtitle}>Customer Payment Receipt & Ledger</ThemedText>
          </View>
          {loading && <ActivityIndicator size="small" color="#B48218" style={{ marginLeft: 8 }} />}
        </View>

        <View style={styles.headerRight}>
          <TouchableOpacity
            style={[styles.actionBtn, styles.clearBtn]}
            onPress={handleClear}
            activeOpacity={0.8}
            disabled={loading}
          >
            <Ionicons name="refresh-outline" size={14} color="#5C4A28" />
            <ThemedText style={styles.clearBtnText}>Clear</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity
            style={[styles.actionBtn, styles.reprintBtn]}
            onPress={() => setIsCancelModalVisible(true)}
            activeOpacity={0.8}
            disabled={loading}
          >
            <Ionicons name="close-circle-outline" size={14} color="#B71C1C" />
            <ThemedText style={[styles.clearBtnText, { color: '#B71C1C' }]}>Cancel Bill</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity
            style={[styles.actionBtn, styles.reprintBtn]}
            onPress={handleOpenReprintModal}
            activeOpacity={0.8}
          >
            <Ionicons name="print-outline" size={14} color="#5C4A28" />
            <ThemedText style={styles.clearBtnText}>RePrint</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity
            style={[styles.actionBtn, styles.saveBtn]}
            onPress={handleSave}
            activeOpacity={0.8}
            disabled={loading}
          >
            <Ionicons name="checkmark-done-outline" size={15} color="#9E6D08" />
            <ThemedText style={styles.saveBtnText}>Pay & Save</ThemedText>
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
          {/* CARD 1: CUSTOMER & AGREEMENT DETAILS */}
          <View style={styles.goldCard}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="card-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitleSmall}>Installment Agreement Search</ThemedText>
              </View>

              {agreementNo ? (
                <View style={styles.agreementBadge}>
                  <ThemedText style={styles.agreementBadgeLabel}>Agreement:</ThemedText>
                  <ThemedText style={styles.agreementBadgeValue}>{agreementNo}</ThemedText>
                </View>
              ) : null}
            </View>

            <View style={styles.cardBody}>
              {/* Row 1: Agreement No, Cust Code, Flexi ID */}
              <View style={styles.threeColumnGrid}>
                {/* Agreement No */}
                <View style={styles.inlineFieldRow}>
                  <ThemedText style={styles.inlineFieldLabel}>Agreement No *</ThemedText>
                  <View style={styles.inputWithLookup}>
                    <TextInput
                      style={[styles.formInput, styles.inputAttached, { flex: 1 }]}
                      value={agreementNo}
                      onChangeText={setAgreementNo}
                      onBlur={fetchAgreement}
                      placeholder="Search Agreement..."
                      placeholderTextColor="#9E8D77"
                    />
                    <TouchableOpacity
                      style={styles.attachedLookupBtn}
                      onPress={handleOpenAgreementLookup}
                      activeOpacity={0.7}
                    >
                      <Ionicons name="search" size={13} color="#7D5E19" />
                    </TouchableOpacity>
                  </View>
                </View>

                {/* Cust Code */}
                <View style={styles.inlineFieldRow}>
                  <ThemedText style={styles.inlineFieldLabel}>Cust Code</ThemedText>
                  <View style={styles.inputWithLookup}>
                    <TextInput
                      style={[styles.formInput, styles.inputAttached, { flex: 1 }]}
                      value={customerCode}
                      onChangeText={setCustomerCode}
                      placeholder="Customer Code..."
                      placeholderTextColor="#9E8D77"
                    />
                    <TouchableOpacity
                      style={styles.attachedLookupBtn}
                      onPress={handleOpenCustomerLookup}
                      activeOpacity={0.7}
                    >
                      <Ionicons name="search" size={13} color="#7D5E19" />
                    </TouchableOpacity>
                  </View>
                </View>

                {/* Flexi ID */}
                <View style={styles.inlineFieldRow}>
                  <ThemedText style={styles.inlineFieldLabel}>Flexi ID</ThemedText>
                  <View style={styles.inputWithLookup}>
                    <TextInput
                      style={[styles.formInput, styles.inputAttached, { flex: 1 }]}
                      value={flexiId}
                      onChangeText={setFlexiId}
                      placeholder="Flexi ID..."
                      placeholderTextColor="#9E8D77"
                    />
                    <TouchableOpacity
                      style={styles.attachedLookupBtn}
                      onPress={handleOpenFlexiLookup}
                      activeOpacity={0.7}
                    >
                      <Ionicons name="search" size={13} color="#7D5E19" />
                    </TouchableOpacity>
                  </View>
                </View>
              </View>

              {/* Row 2: Customer Name, Address, Trade-In */}
              <View style={[styles.threeColumnGrid, { marginTop: 6 }]}>
                {/* Customer Name */}
                <View style={styles.inlineFieldRow}>
                  <ThemedText style={styles.inlineFieldLabel}>Customer Name</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1 }]}
                    value={customerName}
                    editable={false}
                    placeholder="Auto-populated"
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* Address */}
                <View style={styles.inlineFieldRow}>
                  <ThemedText style={styles.inlineFieldLabel}>Address</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1 }]}
                    value={address}
                    editable={false}
                    placeholder="Auto-populated"
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* Trade-In Options */}
                <View style={styles.inlineFieldRow}>
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
                        onPress={handleNavigateToTradeIn}
                        activeOpacity={0.8}
                      >
                        <ThemedText style={styles.tradeInGoBtnText}>Trade IN</ThemedText>
                        <Ionicons name="arrow-forward" size={11} color="#9E6D08" />
                      </TouchableOpacity>
                    )}
                  </View>
                </View>
              </View>

              {/* Row 3: No of Inst, Inst Amount, Total Amount */}
              <View style={[styles.threeColumnGrid, { marginTop: 6 }]}>
                {/* No of Installment */}
                <View style={styles.inlineFieldRow}>
                  <ThemedText style={styles.inlineFieldLabel}>No of Inst *</ThemedText>
                  <TextInput
                    style={[styles.formInput, { flex: 1, textAlign: 'right' }]}
                    value={noOfInstallment}
                    onChangeText={setNoOfInstallment}
                    keyboardType="numeric"
                    placeholder="1"
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* Installment Amount */}
                <View style={styles.inlineFieldRow}>
                  <ThemedText style={styles.inlineFieldLabel}>Inst Amount *</ThemedText>
                  <TextInput
                    style={[styles.formInput, { flex: 1, textAlign: 'right' }]}
                    value={installmentAmount}
                    onChangeText={setInstallmentAmount}
                    keyboardType="numeric"
                    placeholder="0.00"
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* Total Amount to Pay */}
                <View style={styles.inlineFieldRow}>
                  <ThemedText style={styles.inlineFieldLabel}>Total Amount ($)</ThemedText>
                  <TextInput
                    style={[
                      styles.formInput,
                      styles.readonlyInput,
                      styles.highlightTotal,
                      { flex: 1, textAlign: 'right' },
                    ]}
                    value={totalAmount}
                    editable={false}
                  />
                </View>
              </View>
            </View>
          </View>

          {/* CARD 2: INSTALLMENT HISTORY DATA TABLE (FULL WIDTH) */}
          <View style={[styles.goldCard, { marginTop: 10 }]}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="time-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitleSmall}>Installment Payment History</ThemedText>
                <View style={styles.counterBadge}>
                  <ThemedText style={styles.counterBadgeText}>{historyList.length} records</ThemedText>
                </View>
              </View>
            </View>

            {/* Table Header (100% Full Width) */}
            <View style={styles.tableHeadRow}>
              <ThemedText style={[styles.tableHeadCell, { flex: 1.8 }]}>Installment No</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 1.4 }]}>Date</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 1.2, textAlign: 'right' }]}>No Of Ins</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 1.3, textAlign: 'right' }]}>
                Installment Amt ($)
              </ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 1.3, textAlign: 'right' }]}>
                Amt Paid ($)
              </ThemedText>
            </View>

            {/* Table Body */}
            {historyList.length === 0 ? (
              <View style={styles.emptyTableContainer}>
                <Ionicons name="receipt-outline" size={28} color="#D4C3A3" />
                <ThemedText style={styles.emptyTableText}>
                  No installment history found for this agreement.
                </ThemedText>
              </View>
            ) : (
              <>
                {historyList.map((item, index) => (
                  <View key={index} style={styles.tableRow}>
                    <ThemedText style={[styles.tableCell, styles.codeCell, { flex: 1.8 }]}>
                      {item.InstallmentNo}
                    </ThemedText>
                    <ThemedText style={[styles.tableCell, { flex: 1.4 }]}>
                      {item.CreateDate ? new Date(item.CreateDate).toLocaleDateString() : ''}
                    </ThemedText>
                    <ThemedText style={[styles.tableCell, { flex: 1.2, textAlign: 'right' }]}>
                      {item.InsPaid}
                    </ThemedText>
                    <ThemedText style={[styles.tableCell, { flex: 1.3, textAlign: 'right' }]}>
                      {typeof item.InstallmentAmt === 'number' ? item.InstallmentAmt.toFixed(2) : item.InstallmentAmt}
                    </ThemedText>
                    <ThemedText style={[styles.tableCell, styles.amountCell, { flex: 1.3, textAlign: 'right' }]}>
                      {typeof item.AmtPaid === 'number' ? item.AmtPaid.toFixed(2) : item.AmtPaid}
                    </ThemedText>
                  </View>
                ))}

                {/* Grid Total Row matching subTotal in VB.NET */}
                <View style={[styles.tableRow, { backgroundColor: '#FFF5F5', borderTopWidth: 2, borderTopColor: '#FFCDD2' }]}>
                  <ThemedText style={[styles.tableCell, { flex: 1.8, fontWeight: 'bold', color: '#D32F2F' }]}>
                    TOTAL:
                  </ThemedText>
                  <ThemedText style={[styles.tableCell, { flex: 1.4 }]} />
                  <ThemedText style={[styles.tableCell, { flex: 1.2, textAlign: 'right', fontWeight: 'bold', color: '#D32F2F' }]}>
                    {historyList.reduce((acc, curr) => acc + (parseFloat(curr.InsPaid) || 0), 0)}
                  </ThemedText>
                  <ThemedText style={[styles.tableCell, { flex: 1.3, textAlign: 'right', fontWeight: 'bold', color: '#D32F2F' }]}>
                    {historyList.reduce((acc, curr) => acc + (parseFloat(curr.InstallmentAmt) || 0), 0).toFixed(2)}
                  </ThemedText>
                  <ThemedText style={[styles.tableCell, { flex: 1.3, textAlign: 'right', fontWeight: 'bold', color: '#D32F2F' }]}>
                    {historyList.reduce((acc, curr) => acc + (parseFloat(curr.AmtPaid) || 0), 0).toFixed(2)}
                  </ThemedText>
                </View>
              </>
            )}
          </View>

          {/* CARD 3: FINANCIAL LEDGER TOTALS */}
          <View style={[styles.goldCard, { marginTop: 10 }]}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="calculator-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitleSmall}>Financial Ledger Balance</ThemedText>
              </View>
            </View>

            <View style={styles.cardBody}>
              <View style={styles.threeColumnGrid}>
                <View style={styles.inlineFieldRow}>
                  <ThemedText style={styles.inlineFieldLabel}>Sales Amount</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1, textAlign: 'right' }]}
                    value={salesAmount}
                    editable={false}
                  />
                </View>

                <View style={styles.inlineFieldRow}>
                  <ThemedText style={styles.inlineFieldLabel}>Deposit Amount</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1, textAlign: 'right' }]}
                    value={depositAmount}
                    editable={false}
                  />
                </View>

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

      {/* AGREEMENT LOOKUP MODAL */}
      <Modal visible={isAgreementModalVisible} transparent animationType="fade">
        <View style={styles.modalOverlay}>
          <View style={styles.lovModalCard}>
            <View style={styles.modalHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="search" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.modalHeaderTitle}>Search Agreement</ThemedText>
              </View>
              <TouchableOpacity
                onPress={() => setIsAgreementModalVisible(false)}
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
                value={agreementSearchQuery}
                onChangeText={setAgreementSearchQuery}
                placeholder="Search by Agreement No or Customer..."
                placeholderTextColor="#9E8D77"
                autoFocus
              />
              {agreementSearchQuery.length > 0 && (
                <TouchableOpacity onPress={() => setAgreementSearchQuery('')}>
                  <Ionicons name="close-circle" size={16} color="#9E8D77" />
                </TouchableOpacity>
              )}
            </View>

            <View style={styles.lovHeadRow}>
              <ThemedText style={[styles.lovHeadText, { flex: 1.8 }]}>InsAgreementNo</ThemedText>
              <ThemedText style={[styles.lovHeadText, { flex: 1.0 }]}>LocationC</ThemedText>
              <ThemedText style={[styles.lovHeadText, { flex: 1.5 }]}>CustomerCode</ThemedText>
              <ThemedText style={[styles.lovHeadText, { flex: 2 }]}>Name</ThemedText>
            </View>

            <FlatList
              data={agreementList.filter(
                (a) =>
                  (a.InsAgreementNo || '').toLowerCase().includes(agreementSearchQuery.toLowerCase()) ||
                  (a.Name || '').toLowerCase().includes(agreementSearchQuery.toLowerCase()) ||
                  (a.CustomerCode || '').toLowerCase().includes(agreementSearchQuery.toLowerCase()) ||
                  (a.LocationCode || '').toLowerCase().includes(agreementSearchQuery.toLowerCase())
              )}
              keyExtractor={(item, index) => item.InsAgreementNo || index.toString()}
              initialNumToRender={50}
              maxToRenderPerBatch={50}
              ListEmptyComponent={
                <View style={styles.emptyTableContainer}>
                  <Ionicons name="search-outline" size={24} color="#D4C3A3" />
                  <ThemedText style={styles.emptyTableText}>No matching agreements found.</ThemedText>
                </View>
              }
              renderItem={({ item }) => (
                <TouchableOpacity
                  style={styles.lovItemRow}
                  onPress={() => handleSelectAgreement(item)}
                  activeOpacity={0.7}
                >
                  <ThemedText style={[styles.lovItemText, styles.codeCell, { flex: 1.8 }]}>
                    {item.InsAgreementNo}
                  </ThemedText>
                  <ThemedText style={[styles.lovItemText, { flex: 1.0 }]}>
                    {item.LocationCode || ''}
                  </ThemedText>
                  <ThemedText style={[styles.lovItemText, { flex: 1.5 }]}>{item.CustomerCode}</ThemedText>
                  <ThemedText style={[styles.lovItemText, { flex: 2 }]} numberOfLines={1}>
                    {item.Name || ''}
                  </ThemedText>
                </TouchableOpacity>
              )}
            />
          </View>
        </View>
      </Modal>

      {/* CUSTOMER LOOKUP MODAL */}
      <Modal visible={isCustomerModalVisible} transparent animationType="fade">
        <View style={styles.modalOverlay}>
          <View style={styles.lovModalCard}>
            <View style={styles.modalHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="person-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.modalHeaderTitle}>Search Customer</ThemedText>
              </View>
              <TouchableOpacity
                onPress={() => setIsCustomerModalVisible(false)}
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
                value={customerSearchQuery}
                onChangeText={setCustomerSearchQuery}
                placeholder="Search by Code, Name, or Address..."
                placeholderTextColor="#9E8D77"
                autoFocus
              />
              {customerSearchQuery.length > 0 && (
                <TouchableOpacity onPress={() => setCustomerSearchQuery('')}>
                  <Ionicons name="close-circle" size={16} color="#9E8D77" />
                </TouchableOpacity>
              )}
            </View>

            <View style={styles.lovHeadRow}>
              <ThemedText style={[styles.lovHeadText, { flex: 1.4 }]}>Code</ThemedText>
              <ThemedText style={[styles.lovHeadText, { flex: 2 }]}>Name</ThemedText>
              <ThemedText style={[styles.lovHeadText, { flex: 2.4 }]}>Address</ThemedText>
            </View>

            <FlatList
              data={customerList.filter(
                (c) =>
                  (c.CustomerCode || '').toLowerCase().includes(customerSearchQuery.toLowerCase()) ||
                  (c.CustomerName || '').toLowerCase().includes(customerSearchQuery.toLowerCase())
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
                  <ThemedText style={[styles.lovItemText, { flex: 2 }]} numberOfLines={1}>
                    {item.CustomerName}
                  </ThemedText>
                  <ThemedText style={[styles.lovItemText, { flex: 2.4 }]} numberOfLines={1}>
                    {item.Address1 || ''} {item.Address2 || ''}
                  </ThemedText>
                </TouchableOpacity>
              )}
            />
          </View>
        </View>
      </Modal>

      {/* FLEXI LOOKUP MODAL */}
      <Modal visible={isFlexiModalVisible} transparent animationType="fade">
        <View style={styles.modalOverlay}>
          <View style={styles.lovModalCard}>
            <View style={styles.modalHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="finger-print-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.modalHeaderTitle}>Search Flexi ID</ThemedText>
              </View>
              <TouchableOpacity
                onPress={() => setIsFlexiModalVisible(false)}
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
                value={flexiSearchQuery}
                onChangeText={setFlexiSearchQuery}
                placeholder="Search by Subscription ID or NRIC..."
                placeholderTextColor="#9E8D77"
                autoFocus
              />
              {flexiSearchQuery.length > 0 && (
                <TouchableOpacity onPress={() => setFlexiSearchQuery('')}>
                  <Ionicons name="close-circle" size={16} color="#9E8D77" />
                </TouchableOpacity>
              )}
            </View>

            <View style={styles.lovHeadRow}>
              <ThemedText style={[styles.lovHeadText, { flex: 1.5 }]}>Sub ID</ThemedText>
              <ThemedText style={[styles.lovHeadText, { flex: 2 }]}>NRIC</ThemedText>
              <ThemedText style={[styles.lovHeadText, { flex: 1.5 }]}>Type</ThemedText>
            </View>

            <FlatList
              data={flexiList.filter(
                (f) =>
                  (f.SubscriptionID || '').toString().toLowerCase().includes(flexiSearchQuery.toLowerCase()) ||
                  (f.NricNo || '').toLowerCase().includes(flexiSearchQuery.toLowerCase())
              )}
              keyExtractor={(item, index) =>
                item.SubscriptionID ? item.SubscriptionID.toString() : index.toString()
              }
              initialNumToRender={50}
              maxToRenderPerBatch={50}
              ListEmptyComponent={
                <View style={styles.emptyTableContainer}>
                  <Ionicons name="search-outline" size={24} color="#D4C3A3" />
                  <ThemedText style={styles.emptyTableText}>No matching Flexi records found.</ThemedText>
                </View>
              }
              renderItem={({ item }) => (
                <TouchableOpacity
                  style={styles.lovItemRow}
                  onPress={() => handleSelectFlexi(item)}
                  activeOpacity={0.7}
                >
                  <ThemedText style={[styles.lovItemText, styles.codeCell, { flex: 1.5 }]}>
                    {item.SubscriptionID}
                  </ThemedText>
                  <ThemedText style={[styles.lovItemText, { flex: 2 }]}>{item.NricNo}</ThemedText>
                  <ThemedText style={[styles.lovItemText, { flex: 1.5 }]}>{item.FlexiType}</ThemedText>
                </TouchableOpacity>
              )}
            />
          </View>
        </View>
      </Modal>

      {/* CANCEL BILL MODAL */}
      <Modal visible={isCancelModalVisible} transparent animationType="fade">
        <View style={styles.modalOverlay}>
          <View style={[styles.lovModalCard, { maxWidth: 400 }]}>
            <View style={[styles.modalHeaderBanner, { backgroundColor: '#FFEBEE' }]}>
              <View style={styles.headerTitleContainer}>
                <Ionicons name="alert-circle-outline" size={16} color="#B71C1C" style={{ marginRight: 6 }} />
                <ThemedText style={[styles.modalHeaderTitle, { color: '#B71C1C' }]}>Cancel Installment Bill</ThemedText>
              </View>
              <TouchableOpacity
                onPress={() => setIsCancelModalVisible(false)}
                activeOpacity={0.7}
                style={styles.closeModalBtn}
              >
                <Ionicons name="close" size={18} color="#3B2D17" />
              </TouchableOpacity>
            </View>

            <View style={{ padding: 14 }}>
              <ThemedText style={{ fontSize: 12, color: '#5C4A28', marginBottom: 8 }}>
                Enter the Installment Bill No to cancel (only today's bills can be cancelled):
              </ThemedText>
              <TextInput
                style={[styles.formInput, { height: 32, fontSize: 13, marginBottom: 14 }]}
                value={cancelBillNo}
                onChangeText={setCancelBillNo}
                placeholder="e.g. IN-101"
                placeholderTextColor="#9E8D77"
                autoFocus
              />
              <View style={{ flexDirection: 'row', justifyContent: 'flex-end', gap: 8 }}>
                <TouchableOpacity
                  style={[styles.actionBtn, styles.clearBtn]}
                  onPress={() => setIsCancelModalVisible(false)}
                >
                  <ThemedText style={styles.clearBtnText}>Close</ThemedText>
                </TouchableOpacity>
                <TouchableOpacity
                  style={[styles.actionBtn, { backgroundColor: '#FFCDD2', borderColor: '#E57373' }]}
                  onPress={handleCancelBill}
                  disabled={loading}
                >
                  <ThemedText style={[styles.saveBtnText, { color: '#B71C1C' }]}>Confirm Cancel</ThemedText>
                </TouchableOpacity>
              </View>
            </View>
          </View>
        </View>
      </Modal>

      {/* REPRINT MODAL */}
      <Modal visible={isReprintModalVisible} transparent animationType="fade">
        <View style={styles.modalOverlay}>
          <View style={[styles.lovModalCard, { maxWidth: 400 }]}>
            <View style={styles.modalHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <Ionicons name="print-outline" size={16} color="#8D6409" style={{ marginRight: 6 }} />
                <ThemedText style={styles.modalHeaderTitle}>RePrint Installment Bill</ThemedText>
              </View>
              <TouchableOpacity
                onPress={() => setIsReprintModalVisible(false)}
                activeOpacity={0.7}
                style={styles.closeModalBtn}
              >
                <Ionicons name="close" size={18} color="#3B2D17" />
              </TouchableOpacity>
            </View>

            <View style={{ padding: 14 }}>
              <ThemedText style={{ fontSize: 12, color: '#5C4A28', marginBottom: 8 }}>
                Enter the Installment Bill No to RePrint:
              </ThemedText>
              <TextInput
                ref={reprintInputRef}
                style={[styles.formInput, { height: 32, fontSize: 13, marginBottom: 14 }]}
                value={reprintBillNo}
                onChangeText={setReprintBillNo}
                onSubmitEditing={handleReprintSubmit}
                placeholder="e.g. IN-101"
                placeholderTextColor="#9E8D77"
                autoFocus
              />
              <View style={{ flexDirection: 'row', justifyContent: 'flex-end', gap: 8 }}>
                <TouchableOpacity
                  style={[styles.actionBtn, styles.clearBtn]}
                  onPress={() => setIsReprintModalVisible(false)}
                >
                  <ThemedText style={styles.clearBtnText}>Close</ThemedText>
                </TouchableOpacity>
                <TouchableOpacity
                  style={[styles.actionBtn, styles.saveBtn]}
                  onPress={handleReprintSubmit}
                >
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
          setActivePrintInstallmentNo('');
        }}
        onPdf={() => {
          const invNo = activePrintInstallmentNo;
          const reprint = activePrintIsReprint;
          setIsPrintModalVisible(false);
          setActivePrintInstallmentNo('');
          if (invNo) {
            handleDownloadPdf(invNo, reprint);
          }
        }}
        onExcel={() => {
          const invNo = activePrintInstallmentNo;
          const reprint = activePrintIsReprint;
          setIsPrintModalVisible(false);
          setActivePrintInstallmentNo('');
          if (invNo) {
            handleDownloadExcel(invNo, reprint);
          }
        }}
        onPrint={() => {
          const invNo = activePrintInstallmentNo;
          const reprint = activePrintIsReprint;
          setIsPrintModalVisible(false);
          setActivePrintInstallmentNo('');
          if (invNo) {
            subPrintInstallment(invNo, reprint);
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
  agreementBadge: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFF4DA',
    paddingHorizontal: 8,
    paddingVertical: 2,
    borderRadius: 4,
    borderWidth: 1,
    borderColor: '#B8903B',
  },
  agreementBadgeLabel: {
    fontSize: 10,
    color: '#7D5E19',
    marginRight: 4,
  },
  agreementBadgeValue: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#8A5D00',
  },

  // ===== THREE COLUMN GRID =====
  threeColumnGrid: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    gap: 10,
  },
  inlineFieldRow: {
    flex: 1,
    minWidth: 260,
    flexDirection: 'row',
    alignItems: 'center',
  },
  inlineFieldLabel: {
    width: 105,
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3B2D17',
    marginRight: 4,
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

  // Input with attached lookup button
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
  },

  // Trade In Options
  tradeInOptionsGroup: {
    flex: 1,
    flexDirection: 'row',
    alignItems: 'center',
    gap: 4,
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
    paddingVertical: 6,
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

  // ===== MODALS =====
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
