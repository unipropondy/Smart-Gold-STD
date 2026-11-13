import { ThemedText } from '@/components/themed-text';
import { API_BASE_URL } from '@/constants/api';
import { useThemeColors } from '@/hooks/useThemeColors';
import { UNIPRO_LOGO_DATA_URI } from '@/utils/uniproLogo';
import { Ionicons } from '@expo/vector-icons';
import { Stack, useLocalSearchParams, useRouter } from 'expo-router';
import React, { useEffect, useState, useRef } from 'react';
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

  // History
  const [historyList, setHistoryList] = useState<any[]>([]);

  // Cancel & Reprint modal states
  const [isCancelModalVisible, setIsCancelModalVisible] = useState(false);
  const [cancelBillNo, setCancelBillNo] = useState('');
  const [isReprintModalVisible, setIsReprintModalVisible] = useState(false);
  const [reprintBillNo, setReprintBillNo] = useState('');

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

  // Original Print Function matching subPrintInstallment(ByVal sInvoiceNo As String) in frmInstallmentPayment.vb
  // Company specific report:
  // If Trim(sCompanyShortCode) = "AJ" Then rptInstallmentPayment_AJ.rpt
  // ElseIf Trim(sCompanyShortCode) = "MD" Then rptInstallmentPayment_MD.rpt
  // ElseIf Trim(sCompanyShortCode) = "MJ" Then rptInstallmentPayment_MJ.rpt
  // Selection: {tblInstallmentDetail.InstallmentNo} = sInvoiceNo
  // Copies: 2 (Customer Copy and Office Copy)
  const subPrintInstallment = async (targetInstallmentNo: string, isReprintMode: boolean = false) => {
    const sInvoiceNo = (targetInstallmentNo || '').trim();
    if (!sInvoiceNo) {
      return;
    }

    try {
      setLoading(true);

      let company: any = { name: '', address1: '', address2: '', city: '', state: '', country: '', phone: '', fax: '', companyShortCode: '' };
      let payment: any = null;
      let history: any[] = [];
      let rates: any = { rate999: '', rate916: '', rate835: '' };
      let payModes: any[] = [];

      // 1. Fetch actual data from backend SQL Server
      const res = await fetch(`${API_URL}/print-data?installmentNo=${encodeURIComponent(sInvoiceNo)}`);
      const json = await res.json();

      if (json.success && json.data) {
        company = json.data.company || company;
        payment = json.data.payment || {};
        history = json.data.history || [];
        rates = json.data.rates || rates;
        payModes = json.data.payModes || [];
      } else {
        showToast(json.message || `Installment payment voucher ${sInvoiceNo} not found for print.`, 'error');
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

      // Original Formula Field Values from frmInstallmentPayment.vb:
      // CompAddress1: CompName
      // CompAddress2: Address1 & " " & Address2 & " " & Country
      // CompAddress3: PhoneNo & " " & FaxNo
      // Customer1: txtCustomerName.Text
      // Customer2: txtAddress1.Text
      // Customer3: txtAddress2.Text
      // Rate1: s999
      // Rate2: s916
      // Rate3: s835
      const sCompanyName = (company.name || 'SMART').trim();
      const sCompAddress2 = [company.address1, company.address2, company.country].filter(Boolean).join(' ');
      const sCompAddress3 = [company.phone, company.fax].filter(Boolean).join(' ');

      const sCustomerName = (payment.CustomerName || customerName || '').trim();
      const sCustomerCode = (payment.CustomerCode || customerCode || '').trim();
      const sCustomerAddress1 = (payment.CustomerAddress1 || address || '').trim();
      const sCustomerAddress2 = (payment.CustomerAddress2 || '').trim();
      const sCustomerPhone = (payment.CustomerPhone || '').trim();

      const s999 = rates.rate999 || '-';
      const s916 = rates.rate916 || '-';
      const s835 = rates.rate835 || '-';

      const sAgreementNo = (payment.InsAgreementNo || agreementNo || '').trim();
      const sInstallmentNo = (payment.InstallmentNo || sInvoiceNo).trim();
      const currentAmtPaid = parseFloat(payment.AmtPaid) || 0;
      const currentInsAmt = parseFloat(payment.InstallmentAmt) || 0;
      const totalAgreementCost = parseFloat(payment.TotalCost) || parseFloat(salesAmount) || 0;
      const depositAmt = parseFloat(payment.DepositAmt) || parseFloat(depositAmount) || 0;

      // Compute total installment payments made to date from history
      let totalInstallmentsPaid = 0;
      history.forEach((h: any) => {
        totalInstallmentsPaid += parseFloat(h.AmtPaid) || 0;
      });
      const grandTotalPaid = depositAmt + totalInstallmentsPaid;
      const balanceDue = totalAgreementCost - grandTotalPaid;

      // Format date helper
      const formatDate = (val: any) => {
        if (!val) return '-';
        const d = new Date(val);
        if (isNaN(d.getTime())) return String(val).split('T')[0] || '-';
        return d.toLocaleDateString('en-GB', { day: '2-digit', month: '2-digit', year: 'numeric' });
      };

      const paymentDateFormatted = formatDate(payment.CreateDate || new Date());

      // Print copy count: 2 copies (Customer Copy and Office Copy)
      // Crystal Report Title:
      // AJ: rptInstallmentPayment_AJ.rpt | MD: rptInstallmentPayment_MD.rpt | MJ: rptInstallmentPayment_MJ.rpt
      const sReportTitle = isReprintMode ? 'INSTALLMENT PAYMENT RECEIPT (DUPLICATE COPY)' : 'INSTALLMENT PAYMENT RECEIPT';

      // Generate HTML for each copy
      const generatePageMarkup = (copyLabel: string) => {
        return `
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
                <div class="report-badge">${sReportTitle}</div>
                <div class="copy-tag">${copyLabel}</div>
                <div class="doc-no">VOUCHER NO: <span>${sInstallmentNo}</span></div>
                <div class="meta-date">Date: ${paymentDateFormatted} &nbsp;|&nbsp; ${sgTimeStr}</div>
              </div>
            </div>

            <!-- GOLD RATES BANNER (Formula Fields: Rate1, Rate2, Rate3) -->
            <div class="gold-rate-strip">
              <div class="rate-cell"><span class="rate-lbl">Gold 999:</span> <span class="rate-val">$${s999}/g</span></div>
              <div class="rate-divider">|</div>
              <div class="rate-cell"><span class="rate-lbl">Gold 916 (22K):</span> <span class="rate-val">$${s916}/g</span></div>
              <div class="rate-divider">|</div>
              <div class="rate-cell"><span class="rate-lbl">Gold 835 (20K):</span> <span class="rate-val">$${s835}/g</span></div>
            </div>

            <!-- TWO-COLUMN INFO: CUSTOMER vs INSTALLMENT AGREEMENT -->
            <div class="info-grid">
              <!-- Customer Box (Formula Fields: Customer1, Customer2, Customer3) -->
              <div class="info-card">
                <div class="card-title">CUSTOMER DETAILS</div>
                <div class="info-row"><span class="info-lbl">Customer:</span> <span class="info-val font-bold">${sCustomerName || 'CASH CUSTOMER'}</span></div>
                <div class="info-row"><span class="info-lbl">Cust Code:</span> <span class="info-val">${sCustomerCode || '-'}</span></div>
                <div class="info-row"><span class="info-lbl">Address 1:</span> <span class="info-val">${sCustomerAddress1 || '-'}</span></div>
                ${sCustomerAddress2 ? `<div class="info-row"><span class="info-lbl">Address 2:</span> <span class="info-val">${sCustomerAddress2}</span></div>` : ''}
                ${sCustomerPhone ? `<div class="info-row"><span class="info-lbl">Contact:</span> <span class="info-val">${sCustomerPhone}</span></div>` : ''}
              </div>

              <!-- Installment Agreement Box -->
              <div class="info-card">
                <div class="card-title">AGREEMENT SPECIFICATION</div>
                <div class="info-row"><span class="info-lbl">Agreement No:</span> <span class="info-val font-bold">${sAgreementNo}</span></div>
                <div class="info-row"><span class="info-lbl">Installment Paid:</span> <span class="info-val">${payment.InsPaid || 1} installment(s)</span></div>
                <div class="info-row"><span class="info-lbl">Installment Amt:</span> <span class="info-val font-bold">$ ${currentInsAmt.toFixed(2)}</span></div>
                <div class="info-row"><span class="info-lbl">Pay Mode:</span> <span class="info-val font-bold">${payModes.length > 0 ? payModes.map((p: any) => p.payMode).join(', ') : 'CASH'}</span></div>
                <div class="info-row"><span class="info-lbl">Remarks:</span> <span class="info-val">${payment.Remarks || '-'}</span></div>
              </div>
            </div>

            <!-- CURRENT PAYMENT VOUCHER HIGHLIGHT -->
            <div class="current-payment-card">
              <div class="payment-title">PAYMENT RECEIVED THIS VOUCHER</div>
              <div class="payment-amount">$ ${currentAmtPaid.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</div>
            </div>

            <!-- PAYMENT HISTORY / ACCUMULATED LEDGER -->
            <div class="section-heading">INSTALLMENT PAYMENT HISTORY / STATEMENT</div>
            <table class="receipt-table">
              <thead>
                <tr>
                  <th style="width: 8%; text-align: center;">#</th>
                  <th style="width: 25%; text-align: left;">Installment Voucher</th>
                  <th style="width: 20%; text-align: center;">Date</th>
                  <th style="width: 15%; text-align: center;">Ins Paid</th>
                  <th style="width: 17%; text-align: right;">Installment Amt ($)</th>
                  <th style="width: 15%; text-align: right;">Amount Paid ($)</th>
                </tr>
              </thead>
              <tbody>
                ${
                  history.length > 0
                    ? history
                        .map(
                          (h: any, idx: number) => `
                        <tr class="${h.InstallmentNo === sInstallmentNo ? 'highlight-row' : ''}">
                          <td style="text-align: center;">${idx + 1}</td>
                          <td style="text-align: left; font-weight: 600;">${h.InstallmentNo} ${h.InstallmentNo === sInstallmentNo ? '*(This Voucher)*' : ''}</td>
                          <td style="text-align: center;">${formatDate(h.CreateDate)}</td>
                          <td style="text-align: center;">${h.InsPaid || 1}</td>
                          <td style="text-align: right;">$${parseFloat(h.InstallmentAmt || 0).toFixed(2)}</td>
                          <td style="text-align: right; font-weight: 700;">$${parseFloat(h.AmtPaid || 0).toFixed(2)}</td>
                        </tr>
                      `
                        )
                        .join('')
                    : `<tr><td colspan="6" style="text-align: center; color: #888; padding: 10px;">Initial Deposit Voucher: $${depositAmt.toFixed(2)}</td></tr>`
                }
              </tbody>
            </table>

            <!-- SUMMARY FINANCIAL BALANCES -->
            <div class="summary-box">
              <div class="summary-row">
                <span class="sum-lbl">Total Agreement Value:</span>
                <span class="sum-val font-bold">$ ${totalAgreementCost.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</span>
              </div>
              <div class="summary-row">
                <span class="sum-lbl">Initial Deposit Paid:</span>
                <span class="sum-val">$ ${depositAmt.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</span>
              </div>
              <div class="summary-row">
                <span class="sum-lbl">Total Installments Paid To Date:</span>
                <span class="sum-val font-bold" style="color: #0F5132;">$ ${totalInstallmentsPaid.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</span>
              </div>
              <div class="summary-divider"></div>
              <div class="summary-row grand-total-row">
                <span class="sum-lbl font-bold" style="font-size: 13px;">REMAINING BALANCE DUE:</span>
                <span class="sum-val font-bold" style="font-size: 15px; color: ${balanceDue <= 0.01 ? '#0F5132' : '#842029'};">
                  $ ${balanceDue > 0 ? balanceDue.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 }) : '0.00 (FULLY SETTLED)'}
                </span>
              </div>
            </div>

            <!-- SIGNATURE SECTION -->
            <div class="signature-section">
              <div class="sign-card">
                <div class="sign-header">CUSTOMER ACKNOWLEDGEMENT</div>
                <div class="sign-row">Customer Name: ${sCustomerName || '...............................'}</div>
                <div class="sign-row" style="margin-top: 14px;">Signature: ....................................................</div>
              </div>
              <div class="sign-card">
                <div class="sign-header">AUTHORIZED STORE OFFICER</div>
                <div class="sign-row">Received By: ${payment.CreateUser || 'Staff'}</div>
                <div class="sign-row" style="margin-top: 14px;">Signature: ....................................................</div>
              </div>
            </div>

            <!-- FOOTER -->
            <div class="report-footer">
              <div class="footer-divider"></div>
              <div class="footer-gen-text">*** System Generated Installment Payment Receipt ***</div>
              <div class="footer-powered">Powered by Unipro SG</div>
            </div>
          </div>
        `;
      };

      // 2 copies (Customer Copy and Office Copy) matching CR.PrintToPrinter(2, True, 0, 0)
      const printPagesHtml = `
        ${generatePageMarkup('CUSTOMER COPY')}
        ${generatePageMarkup('OFFICE COPY')}
      `;

      const printHtml = `
        <!DOCTYPE html>
        <html>
          <head>
            <meta charset="utf-8" />
            <title>Installment Payment Receipt - ${sInstallmentNo}</title>
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

              /* CURRENT PAYMENT CARD */
              .current-payment-card {
                background: #EBF7EE;
                border: 1.5px solid #A3D9B1;
                border-radius: 6px;
                padding: 8px 14px;
                text-align: center;
                margin: 6px 0 10px 0;
              }
              .payment-title {
                font-size: 11px;
                font-weight: 700;
                color: #0F5132;
                letter-spacing: 0.5px;
                text-transform: uppercase;
              }
              .payment-amount {
                font-size: 22px;
                font-weight: 800;
                color: #0F5132;
                margin-top: 2px;
                font-family: monospace;
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
                padding: 5px 6px;
                border: 1px solid #D8CFC0;
              }
              .receipt-table td {
                padding: 5px 6px;
                border: 1px solid #E5DFD4;
                color: #222;
              }
              .highlight-row {
                background-color: #FFF9E6;
              }

              /* SUMMARY */
              .summary-box {
                margin-left: auto;
                width: 65%;
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
                line-height: 1.5;
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
                margin-top: 8px;
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
                line-height: 1.3;
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
                  page-break-after: always !important;
                }
                .page-sheet:last-child {
                  page-break-after: auto !important;
                }
              }
            </style>
          </head>
          <body>
            ${printPagesHtml}
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
          showToast('Installment Payment receipt preview opened', 'success');
        } else {
          const blob = new Blob([printHtml], { type: 'text/html;charset=utf-8;' });
          const url = URL.createObjectURL(blob);
          const win = window.open(url, '_blank');
          if (!win) {
            showToast('Popup blocker prevented opening print preview. Please allow popups.', 'error');
          } else {
            showToast('Installment Payment receipt preview opened', 'success');
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

  // Handle return from payingmode after save
  useEffect(() => {
    if (params?.paymentSuccess === 'true') {
      const savedPaymentNo = (params.installmentNo as string) || (params.savedInvoiceNo as string) || (params.paymentNo as string);
      if (savedPaymentNo && savedPaymentNo.trim()) {
        const cleanNo = savedPaymentNo.trim();
        subPrintInstallment(cleanNo, false);
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
  // subPrintInstallment(Trim(txtRePrintBillNo.Text)) with bReprint = True
  const handleReprintSubmit = () => {
    const cleanNo = reprintBillNo.trim();
    if (!cleanNo) {
      return;
    }
    setIsReprintModalVisible(false);
    subPrintInstallment(cleanNo, true);
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
              <ThemedText style={[styles.lovHeadText, { flex: 1.8 }]}>Agreement No</ThemedText>
              <ThemedText style={[styles.lovHeadText, { flex: 1.3 }]}>Cust Code</ThemedText>
              <ThemedText style={[styles.lovHeadText, { flex: 2 }]}>Customer Name</ThemedText>
            </View>

            <FlatList
              data={agreementList.filter(
                (a) =>
                  (a.InsAgreementNo || '').toLowerCase().includes(agreementSearchQuery.toLowerCase()) ||
                  (a.Name || '').toLowerCase().includes(agreementSearchQuery.toLowerCase()) ||
                  (a.CustomerCode || '').toLowerCase().includes(agreementSearchQuery.toLowerCase())
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
                  <ThemedText style={[styles.lovItemText, { flex: 1.3 }]}>{item.CustomerCode}</ThemedText>
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
