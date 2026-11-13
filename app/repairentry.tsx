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

const API_URL = `${API_BASE_URL}/repairentry`;

export default function RepairEntryScreen() {
  const themeColors = useThemeColors();
  const router = useRouter();
  const params = useLocalSearchParams();

  const [loading, setLoading] = useState(false);
  const [saving, setSaving] = useState(false);
  const [toast, setToast] = useState<{ message: string; type: 'success' | 'error' } | null>(null);

  const reprintInputRef = useRef<any>(null);

  // Form State - Header
  const [invoiceNo, setInvoiceNo] = useState('');
  const [repairDate, setRepairDate] = useState(new Date().toISOString().split('T')[0]);
  const [issueDate, setIssueDate] = useState('');
  const [dateOfDelivery, setDateOfDelivery] = useState('');

  // Customer State
  const [customerCode, setCustomerCode] = useState('');
  const [customerName, setCustomerName] = useState('');
  const [address, setAddress] = useState('');
  const [supplierCode, setSupplierCode] = useState('');

  const [remarks, setRemarks] = useState('');
  const [tradeInOption, setTradeInOption] = useState<'Gold' | 'Cash' | 'Both'>('Gold');
  const [tradeInAmount, setTradeInAmount] = useState('0.00');
  const [advancePaid, setAdvancePaid] = useState('0.00');
  const [isService, setIsService] = useState(false);

  // Totals
  const [totalWeight, setTotalWeight] = useState('0.00');
  const [totalCost, setTotalCost] = useState('0.00');
  const [balanceAmount, setBalanceAmount] = useState('0.00');

  // Grid State
  const [items, setItems] = useState<any[]>([]);
  const [currentDescription, setCurrentDescription] = useState('');
  const [currentWeight, setCurrentWeight] = useState('');
  const [currentCost, setCurrentCost] = useState('');

  // Password Modal
  const [isPasswordVisible, setIsPasswordVisible] = useState(true);
  const [salesmanPassword, setSalesmanPassword] = useState('');
  const [salesManCode, setSalesManCode] = useState('');

  // LOV State
  const [isCustomerModalVisible, setIsCustomerModalVisible] = useState(false);
  const [customers, setCustomers] = useState<any[]>([]);
  const [customerSearch, setCustomerSearch] = useState('');

  const [isSupplierModalVisible, setIsSupplierModalVisible] = useState(false);
  const [suppliers, setSuppliers] = useState<any[]>([]);
  const [supplierSearch, setSupplierSearch] = useState('');

  const [isInvoiceModalVisible, setIsInvoiceModalVisible] = useState(false);
  const [invoices, setInvoices] = useState<any[]>([]);
  const [invoiceSearch, setInvoiceSearch] = useState('');

  // RePrint Modal State
  const [isReprintModalVisible, setIsReprintModalVisible] = useState(false);
  const [reprintBillNo, setReprintBillNo] = useState('');

  const showToast = (message: string, type: 'success' | 'error' = 'success') => {
    setToast({ message, type });
    setTimeout(() => setToast(null), 3000);
  };

  // 1. Password Verification
  const verifyPassword = async () => {
    if (!salesmanPassword.trim()) {
      showToast('Please enter salesman password', 'error');
      return;
    }
    setLoading(true);
    try {
      const res = await fetch(`${API_URL}/validate-salesman`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ password: salesmanPassword }),
      });
      const json = await res.json();
      if (json.success) {
        setSalesManCode(json.salesmanCode || '');
        setIsPasswordVisible(false);
        handleGenerateNextNo();
      } else {
        showToast('Invalid Salesman Password', 'error');
      }
    } catch (err) {
      showToast('Network error validating password', 'error');
    } finally {
      setLoading(false);
    }
  };

  // 2. Fetch Customer LOV
  const fetchCustomers = async () => {
    setIsCustomerModalVisible(true);
    setLoading(true);
    try {
      let data: any[] = [];
      const res = await fetch(`${API_URL}/customers`);
      const json = await res.json();
      if (json.success && Array.isArray(json.data) && json.data.length > 0) {
        data = json.data;
      } else {
        // Fallback to customer API if repairentry/customers returns empty
        const fallbackRes = await fetch(`${API_BASE_URL}/customer`);
        const fallbackJson = await fallbackRes.json();
        if (fallbackJson.success && Array.isArray(fallbackJson.data) && fallbackJson.data.length > 0) {
          data = fallbackJson.data;
        } else {
          const lovRes = await fetch(`${API_BASE_URL}/installmentorderentry/lov?type=customer`);
          const lovJson = await lovRes.json();
          if (lovJson.success && Array.isArray(lovJson.data)) {
            data = lovJson.data.map((c: any) => ({
              CustomerCode: c.Code || c.CustomerCode,
              CustomerName: c.Name || c.CustomerName,
              Address1: c.Address1,
              Address2: c.Address2,
              Address3: c.Address3,
            }));
          }
        }
      }
      setCustomers(data);
    } catch (err) {
      console.error('Fetch customers error:', err);
      try {
        const fallbackRes = await fetch(`${API_BASE_URL}/customer`);
        const fallbackJson = await fallbackRes.json();
        if (fallbackJson.success && Array.isArray(fallbackJson.data)) {
          setCustomers(fallbackJson.data);
          return;
        }
      } catch (fErr) {}
      showToast('Failed to load customers', 'error');
    } finally {
      setLoading(false);
    }
  };

  const handleSelectCustomer = (cust: any) => {
    setCustomerCode(cust.CustomerCode ? String(cust.CustomerCode).trim() : '');
    setCustomerName(cust.CustomerName ? String(cust.CustomerName).trim() : '');
    setAddress(`${cust.Address1 || ''} ${cust.Address2 || ''} ${cust.Address3 || ''}`.trim());
    setIsCustomerModalVisible(false);
  };

  const lookupCustomerByCode = async (code: string) => {
    if (!code.trim()) return;
    try {
      const res = await fetch(`${API_URL}/customers`);
      const json = await res.json();
      let list = json.success && Array.isArray(json.data) ? json.data : [];
      if (list.length === 0) {
        const fallbackRes = await fetch(`${API_BASE_URL}/customer`);
        const fallbackJson = await fallbackRes.json();
        if (fallbackJson.success && Array.isArray(fallbackJson.data)) {
          list = fallbackJson.data;
        }
      }
      const found = list.find(
        (c: any) => (c.CustomerCode || '').trim().toLowerCase() === code.trim().toLowerCase()
      );
      if (found) {
        setCustomerCode(found.CustomerCode ? String(found.CustomerCode).trim() : code);
        setCustomerName(found.CustomerName ? String(found.CustomerName).trim() : '');
        setAddress(`${found.Address1 || ''} ${found.Address2 || ''} ${found.Address3 || ''}`.trim());
      }
    } catch (err) {
      console.error('Lookup customer error:', err);
    }
  };

  // 3. Fetch Supplier LOV
  const fetchSuppliers = async () => {
    setLoading(true);
    try {
      const res = await fetch(`${API_URL}/suppliers`);
      const json = await res.json();
      if (json.success) {
        setSuppliers(json.data || []);
        setIsSupplierModalVisible(true);
      }
    } catch (err) {
      showToast('Failed to load suppliers', 'error');
    } finally {
      setLoading(false);
    }
  };

  const handleSelectSupplier = (supp: any) => {
    setSupplierCode(supp.SupplierCode);
    setIsSupplierModalVisible(false);
  };

  // 3.5. Fetch Invoice LOV
  const fetchInvoices = async () => {
    setLoading(true);
    try {
      const res = await fetch(`${API_URL}/invoices`);
      const json = await res.json();
      if (json.success) {
        setInvoices(json.data || []);
        setIsInvoiceModalVisible(true);
      }
    } catch (err) {
      showToast('Failed to load invoices', 'error');
    } finally {
      setLoading(false);
    }
  };

  const handleSelectInvoice = (inv: any) => {
    setInvoiceNo(inv.RepairNo);
    setIsInvoiceModalVisible(false);
    fetchRepair(inv.RepairNo);
  };

  // 4. Fetch Existing Repair
  const fetchRepair = async (repNo: string) => {
    if (!repNo || !repNo.trim()) return;
    const cleanNo = repNo.trim();
    setLoading(true);
    try {
      const res = await fetch(`${API_URL}/${encodeURIComponent(cleanNo)}`);
      const json = await res.json();
      if (json.success && json.header) {
        const { header, details } = json;
        setInvoiceNo(cleanNo);
        setCustomerCode(header.CustomerCode ? String(header.CustomerCode).trim() : '');
        setCustomerName(header.CustomerName ? String(header.CustomerName).trim() : '');

        const addrParts = [
          header.CustomerAddress1 || header.Address1,
          header.CustomerAddress2 || header.Address2,
          header.CustomerAddress3 || header.Address3,
        ].filter(Boolean).map(s => String(s).trim());
        setAddress(addrParts.join(' '));

        setSupplierCode(header.Suppliercode ? String(header.Suppliercode).trim() : (header.SupplierCode ? String(header.SupplierCode).trim() : ''));
        setRepairDate(header.RepairDate ? String(header.RepairDate).substring(0, 10) : '');
        setIssueDate(header.IssueDate ? String(header.IssueDate).substring(0, 10) : '');
        setDateOfDelivery(header.DeliveryDate ? String(header.DeliveryDate).substring(0, 10) : (header.DateOfDelivery ? String(header.DateOfDelivery).substring(0, 10) : ''));
        setRemarks(header.Remarks ? String(header.Remarks).trim() : '');

        const adv = parseFloat(header.AdvancePaid) || 0;
        setAdvancePaid(adv.toFixed(2));

        if (Array.isArray(details) && details.length > 0) {
          const mappedItems = details.map((d: any) => ({
            description: (d.Description || '').trim(),
            weight: parseFloat(d.WeightEstimated || 0).toFixed(2),
            cost: parseFloat(d.EstimatedCost || 0).toFixed(2),
          }));
          setItems(mappedItems);
          recalculateTotals(mappedItems, adv);
        } else {
          setItems([]);
          const estCost = parseFloat(header.TotalCostEstimated) || 0;
          setTotalCost(estCost.toFixed(2));
          setTotalWeight('0.00');
          setBalanceAmount((estCost - adv).toFixed(2));
        }
        showToast('Repair details loaded', 'success');
      } else {
        showToast(json.message || 'Please Give the Correct Repair Number', 'error');
      }
    } catch (err: any) {
      showToast(err.message || 'Error loading repair', 'error');
    } finally {
      setLoading(false);
    }
  };

  const handleGenerateNextNo = async () => {
    setLoading(true);
    try {
      const res = await fetch(`${API_URL}/next-no`);
      const json = await res.json();
      if (json.success) {
        setInvoiceNo(json.repairNo);
        showToast(`Generated No: ${json.repairNo}`, 'success');
      }
    } catch (err) {
      showToast('Failed to generate next number', 'error');
    } finally {
      setLoading(false);
    }
  };

  // 5. Save Repair Entry
  const handleSave = async () => {
    if (!customerCode) {
      showToast('Customer code should not be empty!', 'error');
      return;
    }
    if (items.length === 0) {
      showToast('No Item to save.', 'error');
      return;
    }

    setSaving(true);
    try {
      let currentInvoiceNo = invoiceNo;
      if (!currentInvoiceNo || currentInvoiceNo.startsWith('RP-17') || currentInvoiceNo.startsWith('RP-18')) {
        const noRes = await fetch(`${API_URL}/next-no`);
        const noJson = await noRes.json();
        if (noJson.success && noJson.repairNo) {
          currentInvoiceNo = noJson.repairNo;
          setInvoiceNo(currentInvoiceNo);
        }
      }

      const adv = parseFloat(advancePaid) || 0;
      const payload = {
        repairNo: currentInvoiceNo,
        repairDate,
        issueDate,
        dateOfDelivery,
        customerCode,
        supplierCode,
        remarks,
        advancePaid: adv,
        totalCostEstimated: parseFloat(totalCost) || 0,
        tradeInAmount: parseFloat(tradeInAmount) || 0,
        tradeInType: tradeInOption.substring(0, 1),
        isService,
        salesManCode: salesManCode || 'ADMIN',
        items,
      };

      // If Advance Paid > 0, navigate through payingmode
      if (adv > 0) {
        router.push({
          pathname: '/payingmode',
          params: {
            totalNet: adv.toFixed(2),
            customerCode: customerCode,
            apiEndpoint: `${API_URL}/save`,
            payload: JSON.stringify(payload),
            returnPath: '/repairentry',
          },
        });
        setSaving(false);
        return;
      }

      const res = await fetch(`${API_URL}/save`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload),
      });
      const json = await res.json();
      if (json.success) {
        showToast('Repair Entry Saved Successfully', 'success');
        const savedRepairNo = (json.repairNo || payload.repairNo || invoiceNo || '').trim();
        // Original VB.NET: If txtInvoiceNo.Text.Trim <> "" Then subPrintBill(txtInvoiceNo.Text.Trim)
        if (savedRepairNo) {
          subPrintBill(savedRepairNo);
        }
        handleClear();
      } else {
        showToast(json.error || json.message || 'Failed to save', 'error');
      }
    } catch (err) {
      showToast('Network error while saving', 'error');
    } finally {
      setSaving(false);
    }
  };

  // Handle return from payingmode after save
  useEffect(() => {
    if (params?.paymentSuccess === 'true') {
      const savedNo = (params.repairNo as string) || (params.savedInvoiceNo as string) || invoiceNo;
      if (savedNo && savedNo.trim()) {
        const cleanNo = savedNo.trim();
        subPrintBill(cleanNo);
      }
    }
  }, [params?.paymentSuccess]);

  // Original Print Document Generators
  // Mode 1: subPrintBill(sInvoiceNo) -> rptRepairEntry.rpt (Job Sheet / Entry Docket)
  // Mode 2: subPrintRepair(sInvoiceNo) -> rptRepairInvoice_AJ.rpt (Invoice Docket / RePrint)
  const renderRepairReportHtml = (
    reportType: 'BILL' | 'REPAIR',
    sInvoiceNo: string,
    company: any,
    header: any,
    details: any[],
    payments: any[],
    isReprint: boolean
  ) => {
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

    // Report Title matching VB.NET formula fields
    let sReportTitle = 'REPAIR JOB DOCKET / ENTRY RECEIPT';
    if (reportType === 'REPAIR') {
      const isDuplicateCopy = Boolean(isReprint && company.companyShortCode === 'HN');
      sReportTitle = isDuplicateCopy ? 'Duplicate Copy' : 'REPAIR INVOICE / REPAIR DOCKET';
    }

    // Customer info mapping (formula fields: Customer1, Customer2, Customer3)
    const sCustomerName = (header.CustomerName || customerName || '').trim();
    const sCustomerCode = (header.CustomerCode || customerCode || '').trim();
    const sCustomerAddress1 = (header.CustomerAddress1 || header.Address1 || '').trim();
    const sCustomerAddress2 = (header.CustomerAddress2 || header.Address2 || '').trim();
    const sCustomerAddress3 = (header.CustomerAddress3 || header.Address3 || '').trim();
    const sCustomerPhone = (header.CustomerPhone || header.Phone || '').trim();

    // Financial figures
    const totalEstimatedCost = parseFloat(header.TotalCostEstimated ?? totalCost) || 0;
    const advancePaidAmt = parseFloat(header.AdvancePaid ?? advancePaid) || 0;

    let totalInstallmentsPaid = 0;
    if (payments && Array.isArray(payments)) {
      payments.forEach((p: any) => {
        totalInstallmentsPaid += parseFloat(p.AmountPaid) || 0;
      });
    }
    const grandTotalPaid = advancePaidAmt + totalInstallmentsPaid;
    const balanceDue = totalEstimatedCost - grandTotalPaid;

    const formatDate = (val: any) => {
      if (!val) return '-';
      const d = new Date(val);
      if (isNaN(d.getTime())) return String(val).split('T')[0] || '-';
      return d.toLocaleDateString('en-GB', { day: '2-digit', month: '2-digit', year: 'numeric' });
    };

    const repairDateFormatted = formatDate(header.RepairDate || new Date());
    const deliveryDateFormatted = formatDate(header.DeliveryDate || header.DateOfDelivery);
    const issueDateFormatted = formatDate(header.IssueDate);

    // Build Items Rows
    let detailRowsHtml = '';
    if (details && details.length > 0) {
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

    // Payment History breakdown (if any payments exist)
    let paymentHistoryHtml = '';
    if (payments && payments.length > 0) {
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
              <td class="meta-lbl">Issue Date</td>
              <td class="meta-val">: &nbsp; ${issueDateFormatted}</td>
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
  };

  // 1. SAVE PRINT: subPrintBill(txtInvoiceNo.Text.Trim) -> rptRepairEntry.rpt
  const subPrintBill = async (sInvoiceNo: string) => {
    const targetNo = (sInvoiceNo || '').trim();
    if (!targetNo) return;

    try {
      setLoading(true);
      const res = await fetch(`${API_URL}/print-data?repairNo=${encodeURIComponent(targetNo)}`);
      const json = await res.json();

      if (!json.success || !json.data) {
        showToast(json.message || `Repair record ${targetNo} not found for print.`, 'error');
        setLoading(false);
        return;
      }

      const { company, header, details, payments } = json.data;
      const printHtml = renderRepairReportHtml('BILL', targetNo, company || {}, header || {}, details || [], payments || [], false);

      if (Platform.OS === 'web' && typeof window !== 'undefined') {
        const printWindow = window.open('', '_blank', 'width=900,height=750');
        if (printWindow) {
          printWindow.document.open();
          printWindow.document.write(printHtml);
          printWindow.document.close();
          showToast('Repair Entry report preview opened', 'success');
        } else {
          const blob = new Blob([printHtml], { type: 'text/html;charset=utf-8;' });
          const url = URL.createObjectURL(blob);
          const win = window.open(url, '_blank');
          if (!win) {
            showToast('Popup blocker prevented opening print preview. Please allow popups.', 'error');
          } else {
            showToast('Repair Entry report preview opened', 'success');
          }
        }
      } else {
        showToast('Print preview generated.', 'success');
      }
    } catch (err: any) {
      console.error('Print Bill Error:', err);
      showToast('Error generating repair entry print: ' + (err.message || 'Error'), 'error');
    } finally {
      setLoading(false);
    }
  };

  // 2. REPRINT: subPrintRepair(Trim(txtRePrintBillNo.Text)) -> rptRepairInvoice_AJ.rpt
  const subPrintRepair = async (sInvoiceNo: string) => {
    const targetNo = (sInvoiceNo || '').trim();
    if (!targetNo) return;

    try {
      setLoading(true);
      const res = await fetch(`${API_URL}/print-data?repairNo=${encodeURIComponent(targetNo)}`);
      const json = await res.json();

      if (!json.success || !json.data) {
        showToast(json.message || `Repair docket ${targetNo} not found for reprinting.`, 'error');
        setLoading(false);
        return;
      }

      const { company, header, details, payments } = json.data;
      const printHtml = renderRepairReportHtml('REPAIR', targetNo, company || {}, header || {}, details || [], payments || [], true);

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
      console.error('RePrint Repair Error:', err);
      showToast('Error generating repair invoice reprint: ' + (err.message || 'Error'), 'error');
    } finally {
      setLoading(false);
    }
  };

  // btnRePrint_Click:
  // PnlReprint.Visible = True
  // txtRePrintBillNo.Clear()
  // txtRePrintBillNo.Focus()
  const handleOpenReprintModal = () => {
    try {
      setReprintBillNo('');
      setIsReprintModalVisible(true);
      setTimeout(() => {
        reprintInputRef.current?.focus?.();
      }, 100);
    } catch (ex: any) {
      Alert.alert('Error', ex.message || 'Error opening RePrint dialog');
    }
  };

  // btnReprintOK_Click:
  // If Trim(txtRePrintBillNo.Text) <> "" Then
  //     bReprint = True
  //     Call subPrintRepair(Trim(txtRePrintBillNo.Text))
  //     bReprint = False
  //     PnlReprint.Visible = False
  // End If
  const handleReprintSubmit = async () => {
    try {
      const cleanNo = (reprintBillNo || '').trim();
      if (cleanNo !== '') {
        setIsReprintModalVisible(false);
        setReprintBillNo('');
        await subPrintRepair(cleanNo);
      }
    } catch (ex: any) {
      Alert.alert('Error', ex.message || 'Error executing RePrint');
    }
  };

  // btnReprintCancel_Click:
  // txtRePrintBillNo.Clear()
  // PnlReprint.Visible = False
  // txtInvoiceNo.Focus()
  const handleReprintCancel = () => {
    try {
      setReprintBillNo('');
      setIsReprintModalVisible(false);
    } catch (ex: any) {
      Alert.alert('Error', ex.message || 'Error cancelling RePrint');
    }
  };

  // 6. Cancel Repair Entry
  const handleCancelRepair = async () => {
    if (!invoiceNo) {
      showToast('Please enter Invoice No to Cancel', 'error');
      return;
    }
    Alert.alert('Confirm Cancel', 'Cancel this repair entry?', [
      { text: 'No', style: 'cancel' },
      {
        text: 'Yes',
        onPress: async () => {
          setLoading(true);
          try {
            const res = await fetch(`${API_URL}/${encodeURIComponent(invoiceNo)}`, { method: 'DELETE' });
            const json = await res.json();
            if (json.success) {
              showToast('Repair Entry Cancelled', 'success');
              handleClear();
            } else {
              showToast(json.message || 'Failed to cancel', 'error');
            }
          } catch (err) {
            showToast('Error cancelling repair', 'error');
          } finally {
            setLoading(false);
          }
        },
      },
    ]);
  };

  const handleClear = () => {
    setInvoiceNo('');
    setCustomerCode('');
    setCustomerName('');
    setAddress('');
    setSupplierCode('');
    setRemarks('');
    setIssueDate('');
    setDateOfDelivery('');
    setAdvancePaid('0.00');
    setTradeInAmount('0.00');
    setItems([]);
    setTotalWeight('0.00');
    setTotalCost('0.00');
    setBalanceAmount('0.00');
    setIsService(false);
    setCurrentDescription('');
    setCurrentWeight('');
    setCurrentCost('');
    handleGenerateNextNo();
  };

  // Grid Operations
  const handleAddItem = () => {
    if (!currentDescription.trim()) {
      showToast('Please enter item description.', 'error');
      return;
    }
    const newItems = [
      ...items,
      {
        description: currentDescription.trim(),
        weight: parseFloat(currentWeight || '0').toFixed(2),
        cost: parseFloat(currentCost || '0').toFixed(2),
      },
    ];
    setItems(newItems);
    recalculateTotals(newItems, parseFloat(advancePaid) || 0);
    setCurrentDescription('');
    setCurrentWeight('');
    setCurrentCost('');
  };

  const handleDeleteItem = (index: number) => {
    const newItems = items.filter((_, i) => i !== index);
    setItems(newItems);
    recalculateTotals(newItems, parseFloat(advancePaid) || 0);
  };

  const recalculateTotals = (currentItems: any[], advance: number) => {
    let tWeight = 0;
    let tCost = 0;
    currentItems.forEach((i) => {
      tWeight += parseFloat(i.weight);
      tCost += parseFloat(i.cost);
    });
    setTotalWeight(tWeight.toFixed(2));
    setTotalCost(tCost.toFixed(2));
    setBalanceAmount((tCost - advance).toFixed(2));
  };

  useEffect(() => {
    const c = parseFloat(totalCost) || 0;
    const a = parseFloat(advancePaid) || 0;
    setBalanceAmount((c - a).toFixed(2));
  }, [advancePaid, totalCost]);

  // Date picker rendering helper
  const renderDateInput = (value: string, onChange: (v: string) => void, placeholder = 'YYYY-MM-DD') => {
    if (Platform.OS === 'web') {
      return (
        <View style={styles.datePickerInputWrap}>
          {React.createElement('input', {
            type: 'date',
            value: value,
            onChange: (e: any) => onChange(e.target.value),
            style: {
              width: '100%',
              border: 'none',
              outline: 'none',
              background: 'transparent',
              fontSize: '12px',
              fontWeight: '600',
              color: '#3B2D17',
              paddingLeft: '4px',
              paddingRight: '4px',
              margin: 0,
              boxSizing: 'border-box',
            },
          })}
        </View>
      );
    }
    return (
      <View style={styles.datePickerInputWrap}>
        <TextInput
          style={[styles.formInput, { flex: 1, paddingVertical: 2, height: 26, fontSize: 12 }]}
          value={value}
          onChangeText={onChange}
          placeholder={placeholder}
          placeholderTextColor="#9E8D77"
        />
        <Ionicons name="calendar-outline" size={14} color="#8D6409" style={{ padding: 3 }} />
      </View>
    );
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
            <ThemedText style={styles.headerTitle}>Jewellery Repair Entry</ThemedText>
            <ThemedText style={styles.headerSubtitle}>Repair Job Docket & Advance Tracking</ThemedText>
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
            style={[styles.actionBtn, styles.cancelRepairBtn]}
            onPress={handleCancelRepair}
            activeOpacity={0.8}
            disabled={saving || loading || !invoiceNo}
          >
            <Ionicons name="trash-outline" size={14} color="#C62828" />
            <ThemedText style={styles.cancelRepairBtnText}>Cancel Entry</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity
            style={[styles.actionBtn, styles.reprintBtn]}
            onPress={handleOpenReprintModal}
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

      {/* SALESMAN PASSWORD MODAL */}
      <Modal visible={isPasswordVisible} transparent animationType="fade">
        <View style={styles.modalOverlay}>
          <View style={styles.passwordModalCard}>
            <View style={styles.passwordHeader}>
              <View style={styles.lockIconBadge}>
                <Ionicons name="lock-closed" size={24} color="#9E6D08" />
              </View>
              <ThemedText style={styles.passwordTitle}>Salesman Authorization</ThemedText>
              <ThemedText style={styles.passwordSubtitle}>Enter your password to unlock Repair Entry</ThemedText>
            </View>

            <TextInput
              style={styles.passwordInput}
              secureTextEntry
              placeholder="Enter Salesman Password..."
              placeholderTextColor="#9E8D77"
              value={salesmanPassword}
              onChangeText={setSalesmanPassword}
              autoFocus
              onSubmitEditing={verifyPassword}
            />

            <View style={styles.passwordBtnRow}>
              <TouchableOpacity
                style={[styles.passwordBtn, styles.passwordCancelBtn]}
                onPress={() => router.back()}
                activeOpacity={0.7}
              >
                <ThemedText style={styles.passwordCancelText}>Cancel</ThemedText>
              </TouchableOpacity>
              <TouchableOpacity
                style={[styles.passwordBtn, styles.passwordVerifyBtn]}
                onPress={verifyPassword}
                activeOpacity={0.8}
                disabled={loading}
              >
                {loading ? (
                  <ActivityIndicator size="small" color="#FFFFFF" />
                ) : (
                  <ThemedText style={styles.passwordVerifyText}>Verify Access</ThemedText>
                )}
              </TouchableOpacity>
            </View>
          </View>
        </View>
      </Modal>

      {!isPasswordVisible && (
        <KeyboardAvoidingView behavior={Platform.OS === 'ios' ? 'padding' : undefined} style={{ flex: 1 }}>
          <ScrollView contentContainerStyle={styles.scrollContainer} keyboardShouldPersistTaps="handled">
            {/* CARD 1: REPAIR DOCKET HEADER & DATES */}
            <View style={styles.goldCard}>
              <View style={styles.cardHeaderBanner}>
                <View style={styles.headerTitleContainer}>
                  <View style={styles.headerIconBadgeSmall}>
                    <Ionicons name="construct-outline" size={13} color="#B48218" />
                  </View>
                  <ThemedText style={styles.cardHeaderTitleSmall}>Repair Docket Information</ThemedText>
                </View>

                {invoiceNo ? (
                  <View style={styles.invoiceBadge}>
                    <ThemedText style={styles.invoiceBadgeLabel}>Invoice No:</ThemedText>
                    <ThemedText style={styles.invoiceBadgeValue}>{invoiceNo}</ThemedText>
                  </View>
                ) : null}
              </View>

              <View style={styles.cardBody}>
                {/* Row 1: Invoice No (with search + New button) & Repair Date */}
                <View style={styles.twoColumnGrid}>
                  {/* Invoice No */}
                  <View style={[styles.inlineFieldRow, { flex: 1.3, minWidth: 280 }]}>
                    <ThemedText style={styles.inlineFieldLabel}>Invoice No *</ThemedText>
                    <View style={styles.inputWithCombo}>
                      <TextInput
                        style={[styles.formInput, styles.inputAttached, { flex: 1 }]}
                        value={invoiceNo}
                        onChangeText={setInvoiceNo}
                        onBlur={() => invoiceNo && fetchRepair(invoiceNo)}
                        onSubmitEditing={() => invoiceNo && fetchRepair(invoiceNo)}
                        placeholder="Search/Enter No..."
                        placeholderTextColor="#9E8D77"
                      />
                      <TouchableOpacity
                        style={styles.attachedLookupBtn}
                        onPress={fetchInvoices}
                        activeOpacity={0.7}
                      >
                        <Ionicons name="search" size={13} color="#7D5E19" />
                      </TouchableOpacity>
                      <TouchableOpacity
                        style={styles.newDocketBtn}
                        onPress={handleGenerateNextNo}
                        activeOpacity={0.8}
                      >
                        <Ionicons name="add" size={13} color="#7D5E19" />
                        <ThemedText style={styles.newDocketBtnText}>New</ThemedText>
                      </TouchableOpacity>
                    </View>
                  </View>

                  {/* Repair Date */}
                  <View style={[styles.inlineFieldRow, { flex: 0.9, minWidth: 200 }]}>
                    <ThemedText style={styles.inlineFieldLabel}>Repair Date</ThemedText>
                    {renderDateInput(repairDate, setRepairDate)}
                  </View>

                  {/* Date of Delivery */}
                  <View style={[styles.inlineFieldRow, { flex: 0.9, minWidth: 200 }]}>
                    <ThemedText style={styles.inlineFieldLabel}>Date Delivery</ThemedText>
                    {renderDateInput(dateOfDelivery, setDateOfDelivery)}
                  </View>

                  {/* Issue Date */}
                  <View style={[styles.inlineFieldRow, { flex: 0.9, minWidth: 200 }]}>
                    <ThemedText style={styles.inlineFieldLabel}>Issue Date</ThemedText>
                    {renderDateInput(issueDate, setIssueDate)}
                  </View>
                </View>

                {/* Row 2: Customer Code, Customer Name, Address */}
                <View style={[styles.threeColumnGrid, { marginTop: 6 }]}>
                  {/* Customer Code */}
                  <View style={styles.inlineFieldRow}>
                    <ThemedText style={styles.inlineFieldLabel}>Customer Code *</ThemedText>
                    <View style={styles.inputWithLookup}>
                      <TextInput
                        style={[styles.formInput, styles.inputAttached, { flex: 1 }]}
                        value={customerCode}
                        onChangeText={setCustomerCode}
                        onBlur={() => lookupCustomerByCode(customerCode)}
                        placeholder="Customer Code..."
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
                  <View style={styles.inlineFieldRow}>
                    <ThemedText style={styles.inlineFieldLabel}>Name</ThemedText>
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
                      placeholder="Auto-populated address"
                      placeholderTextColor="#9E8D77"
                    />
                  </View>
                </View>

                {/* Row 3: Supplier Code, Remarks, Trade IN */}
                <View style={[styles.threeColumnGrid, { marginTop: 6 }]}>
                  {/* Supplier Code */}
                  <View style={styles.inlineFieldRow}>
                    <ThemedText style={styles.inlineFieldLabel}>Supplier Code</ThemedText>
                    <View style={styles.inputWithLookup}>
                      <TextInput
                        style={[styles.formInput, styles.inputAttached, { flex: 1 }]}
                        value={supplierCode}
                        onChangeText={setSupplierCode}
                        placeholder="Supplier..."
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

                  {/* Remarks */}
                  <View style={styles.inlineFieldRow}>
                    <ThemedText style={styles.inlineFieldLabel}>Remarks</ThemedText>
                    <TextInput
                      style={[styles.formInput, { flex: 1 }]}
                      value={remarks}
                      onChangeText={setRemarks}
                      placeholder="Repair specifications..."
                      placeholderTextColor="#9E8D77"
                    />
                  </View>

                  {/* Trade IN */}
                  <View style={styles.inlineFieldRow}>
                    <ThemedText style={styles.inlineFieldLabel}>Trade IN</ThemedText>
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

                      <TextInput
                        style={[styles.formInput, { width: 75, textAlign: 'right' }]}
                        value={tradeInAmount}
                        onChangeText={setTradeInAmount}
                        keyboardType="numeric"
                        placeholder="0.00"
                        placeholderTextColor="#9E8D77"
                      />

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
              </View>
            </View>

            {/* CARD 2: REPAIR ITEMS TABLE (100% FULL WIDTH) */}
            <View style={[styles.goldCard, { marginTop: 10 }]}>
              <View style={styles.cardHeaderBanner}>
                <View style={styles.headerTitleContainer}>
                  <View style={styles.headerIconBadgeSmall}>
                    <Ionicons name="hammer-outline" size={13} color="#B48218" />
                  </View>
                  <ThemedText style={styles.cardHeaderTitleSmall}>Repair Item Details</ThemedText>
                  <View style={styles.counterBadge}>
                    <ThemedText style={styles.counterBadgeText}>{items.length} items</ThemedText>
                  </View>
                </View>
              </View>

              {/* Table Header (100% Full Width) */}
              <View style={styles.tableHeadRow}>
                <ThemedText style={[styles.tableHeadCell, { flex: 3 }]}>Repair Description</ThemedText>
                <ThemedText style={[styles.tableHeadCell, { flex: 1.2, textAlign: 'right' }]}>
                  Weight (g)
                </ThemedText>
                <ThemedText style={[styles.tableHeadCell, { flex: 1.4, textAlign: 'right' }]}>
                  Est. Cost ($)
                </ThemedText>
                <ThemedText style={[styles.tableHeadCell, { width: 60, textAlign: 'center' }]}>Action</ThemedText>
              </View>

              {/* Table Body */}
              {items.length === 0 ? (
                <View style={styles.emptyTableContainer}>
                  <Ionicons name="sparkles-outline" size={28} color="#D4C3A3" />
                  <ThemedText style={styles.emptyTableText}>
                    No repair items entered. Add items using the subform below.
                  </ThemedText>
                </View>
              ) : (
                items.map((item, index) => (
                  <View key={index} style={styles.tableRow}>
                    <ThemedText style={[styles.tableCell, { flex: 3 }]} numberOfLines={1}>
                      {item.description}
                    </ThemedText>
                    <ThemedText style={[styles.tableCell, { flex: 1.2, textAlign: 'right' }]}>
                      {item.weight}
                    </ThemedText>
                    <ThemedText style={[styles.tableCell, styles.amountCell, { flex: 1.4, textAlign: 'right' }]}>
                      {item.cost}
                    </ThemedText>
                    <View style={{ width: 60, alignItems: 'center' }}>
                      <TouchableOpacity
                        style={styles.deleteRowBtn}
                        onPress={() => handleDeleteItem(index)}
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
                <View style={styles.itemEntryRow}>
                  {/* Description */}
                  <View style={[styles.inlineFieldRow, { flex: 2.8, minWidth: 220 }]}>
                    <ThemedText style={styles.inlineFieldLabel}>Description *</ThemedText>
                    <TextInput
                      style={[styles.formInput, { flex: 1 }]}
                      value={currentDescription}
                      onChangeText={setCurrentDescription}
                      placeholder="e.g. Ring resizing, chain soldering..."
                      placeholderTextColor="#9E8D77"
                    />
                  </View>

                  {/* Weight */}
                  <View style={[styles.inlineFieldRow, { flex: 1.2, minWidth: 120 }]}>
                    <ThemedText style={styles.inlineFieldLabel}>Weight</ThemedText>
                    <TextInput
                      style={[styles.formInput, { flex: 1, textAlign: 'right' }]}
                      value={currentWeight}
                      onChangeText={setCurrentWeight}
                      keyboardType="numeric"
                      placeholder="0.00"
                      placeholderTextColor="#9E8D77"
                    />
                  </View>

                  {/* Cost */}
                  <View style={[styles.inlineFieldRow, { flex: 1.4, minWidth: 140 }]}>
                    <ThemedText style={styles.inlineFieldLabel}>Cost ($)</ThemedText>
                    <TextInput
                      style={[styles.formInput, { flex: 1, textAlign: 'right' }]}
                      value={currentCost}
                      onChangeText={setCurrentCost}
                      keyboardType="numeric"
                      placeholder="0.00"
                      placeholderTextColor="#9E8D77"
                    />
                  </View>
                </View>

                {/* Subform Control Bar */}
                <View style={styles.subformControlsRow}>
                  {/* Service Toggle */}
                  <TouchableOpacity
                    style={styles.checkboxPill}
                    onPress={() => setIsService(!isService)}
                    activeOpacity={0.7}
                  >
                    <Ionicons
                      name={isService ? 'checkbox' : 'square-outline'}
                      size={16}
                      color={isService ? '#9E6D08' : '#8D6409'}
                    />
                    <ThemedText style={[styles.checkboxPillText, isService && styles.checkboxPillTextSelected]}>
                      Complimentary Service / Polish
                    </ThemedText>
                  </TouchableOpacity>

                  {/* Actions */}
                  <View style={styles.subformActionBtns}>
                    <TouchableOpacity
                      style={[styles.tableActionBtn, styles.clearItemBtn]}
                      onPress={() => {
                        setCurrentDescription('');
                        setCurrentWeight('');
                        setCurrentCost('');
                      }}
                      activeOpacity={0.8}
                    >
                      <Ionicons name="close-circle-outline" size={14} color="#5C4A28" style={{ marginRight: 4 }} />
                      <ThemedText style={styles.clearItemBtnText}>Clear</ThemedText>
                    </TouchableOpacity>

                    <TouchableOpacity
                      style={[styles.tableActionBtn, styles.addBtn]}
                      onPress={handleAddItem}
                      activeOpacity={0.8}
                    >
                      <Ionicons name="add-circle-outline" size={14} color="#9E6D08" style={{ marginRight: 4 }} />
                      <ThemedText style={styles.addBtnText}>Add Item</ThemedText>
                    </TouchableOpacity>
                  </View>
                </View>
              </View>
            </View>

            {/* CARD 3: REPAIR FINANCIAL SUMMARY */}
            <View style={[styles.goldCard, { marginTop: 10 }]}>
              <View style={styles.cardHeaderBanner}>
                <View style={styles.headerTitleContainer}>
                  <View style={styles.headerIconBadgeSmall}>
                    <Ionicons name="calculator-outline" size={13} color="#B48218" />
                  </View>
                  <ThemedText style={styles.cardHeaderTitleSmall}>Repair Financial Totals</ThemedText>
                </View>
              </View>

              <View style={styles.cardBody}>
                <View style={styles.fourColumnGrid}>
                  {/* Total Weight */}
                  <View style={styles.inlineFieldRow}>
                    <ThemedText style={styles.inlineFieldLabel}>Total Weight</ThemedText>
                    <TextInput
                      style={[styles.formInput, styles.readonlyInput, { flex: 1, textAlign: 'right' }]}
                      value={totalWeight}
                      editable={false}
                    />
                  </View>

                  {/* Total Cost */}
                  <View style={styles.inlineFieldRow}>
                    <ThemedText style={styles.inlineFieldLabel}>Total Cost</ThemedText>
                    <TextInput
                      style={[styles.formInput, styles.readonlyInput, { flex: 1, textAlign: 'right' }]}
                      value={totalCost}
                      editable={false}
                    />
                  </View>

                  {/* Advance Paid */}
                  <View style={styles.inlineFieldRow}>
                    <ThemedText style={styles.inlineFieldLabel}>Advance Paid</ThemedText>
                    <TextInput
                      style={[styles.formInput, { flex: 1, textAlign: 'right' }]}
                      value={advancePaid}
                      onChangeText={setAdvancePaid}
                      keyboardType="numeric"
                      placeholder="0.00"
                      placeholderTextColor="#9E8D77"
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
      )}

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
                value={customerSearch}
                onChangeText={setCustomerSearch}
                placeholder="Search by Code or Name..."
                placeholderTextColor="#9E8D77"
                autoFocus
              />
              {customerSearch.length > 0 && (
                <TouchableOpacity onPress={() => setCustomerSearch('')}>
                  <Ionicons name="close-circle" size={16} color="#9E8D77" />
                </TouchableOpacity>
              )}
            </View>

            <View style={styles.lovHeadRow}>
              <ThemedText style={[styles.lovHeadText, { flex: 1.4 }]}>Customer Code</ThemedText>
              <ThemedText style={[styles.lovHeadText, { flex: 2.2 }]}>Customer Name</ThemedText>
            </View>

            <FlatList
              data={customers.filter(
                (c) =>
                  (c.CustomerCode || '').toLowerCase().includes(customerSearch.toLowerCase()) ||
                  (c.CustomerName || '').toLowerCase().includes(customerSearch.toLowerCase())
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

      {/* SUPPLIER LOOKUP MODAL */}
      <Modal visible={isSupplierModalVisible} transparent animationType="fade">
        <View style={styles.modalOverlay}>
          <View style={styles.lovModalCard}>
            <View style={styles.modalHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="business-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.modalHeaderTitle}>Search Supplier</ThemedText>
              </View>
              <TouchableOpacity
                onPress={() => setIsSupplierModalVisible(false)}
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
                value={supplierSearch}
                onChangeText={setSupplierSearch}
                placeholder="Search by Code or Name..."
                placeholderTextColor="#9E8D77"
                autoFocus
              />
              {supplierSearch.length > 0 && (
                <TouchableOpacity onPress={() => setSupplierSearch('')}>
                  <Ionicons name="close-circle" size={16} color="#9E8D77" />
                </TouchableOpacity>
              )}
            </View>

            <View style={styles.lovHeadRow}>
              <ThemedText style={[styles.lovHeadText, { flex: 1.4 }]}>Supplier Code</ThemedText>
              <ThemedText style={[styles.lovHeadText, { flex: 2.2 }]}>Supplier Name</ThemedText>
            </View>

            <FlatList
              data={suppliers.filter(
                (s) =>
                  (s.SupplierCode || '').toLowerCase().includes(supplierSearch.toLowerCase()) ||
                  (s.SupplierName || '').toLowerCase().includes(supplierSearch.toLowerCase())
              )}
              keyExtractor={(item) => item.SupplierCode}
              initialNumToRender={50}
              maxToRenderPerBatch={50}
              ListEmptyComponent={
                <View style={styles.emptyTableContainer}>
                  <Ionicons name="search-outline" size={24} color="#D4C3A3" />
                  <ThemedText style={styles.emptyTableText}>No matching suppliers found.</ThemedText>
                </View>
              }
              renderItem={({ item }) => (
                <TouchableOpacity
                  style={styles.lovItemRow}
                  onPress={() => handleSelectSupplier(item)}
                  activeOpacity={0.7}
                >
                  <ThemedText style={[styles.lovItemText, styles.codeCell, { flex: 1.4 }]}>
                    {item.SupplierCode}
                  </ThemedText>
                  <ThemedText style={[styles.lovItemText, { flex: 2.2 }]} numberOfLines={1}>
                    {item.SupplierName}
                  </ThemedText>
                </TouchableOpacity>
              )}
            />
          </View>
        </View>
      </Modal>

      {/* INVOICE LOOKUP MODAL */}
      <Modal visible={isInvoiceModalVisible} transparent animationType="fade">
        <View style={styles.modalOverlay}>
          <View style={styles.lovModalCard}>
            <View style={styles.modalHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="receipt-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.modalHeaderTitle}>Search Repair Invoices</ThemedText>
              </View>
              <TouchableOpacity
                onPress={() => setIsInvoiceModalVisible(false)}
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
                value={invoiceSearch}
                onChangeText={setInvoiceSearch}
                placeholder="Search by Repair No or Customer Code..."
                placeholderTextColor="#9E8D77"
                autoFocus
              />
              {invoiceSearch.length > 0 && (
                <TouchableOpacity onPress={() => setInvoiceSearch('')}>
                  <Ionicons name="close-circle" size={16} color="#9E8D77" />
                </TouchableOpacity>
              )}
            </View>

            <View style={styles.lovHeadRow}>
              <ThemedText style={[styles.lovHeadText, { flex: 1.5 }]}>Repair No</ThemedText>
              <ThemedText style={[styles.lovHeadText, { flex: 1.2 }]}>Date</ThemedText>
              <ThemedText style={[styles.lovHeadText, { flex: 1.5 }]}>Customer Code</ThemedText>
            </View>

            <FlatList
              data={invoices.filter(
                (i) =>
                  (i.RepairNo || '').toLowerCase().includes(invoiceSearch.toLowerCase()) ||
                  (i.CustomerCode || '').toLowerCase().includes(invoiceSearch.toLowerCase())
              )}
              keyExtractor={(item) => item.RepairNo}
              initialNumToRender={50}
              maxToRenderPerBatch={50}
              ListEmptyComponent={
                <View style={styles.emptyTableContainer}>
                  <Ionicons name="search-outline" size={24} color="#D4C3A3" />
                  <ThemedText style={styles.emptyTableText}>No matching repair invoices found.</ThemedText>
                </View>
              }
              renderItem={({ item }) => (
                <TouchableOpacity
                  style={styles.lovItemRow}
                  onPress={() => handleSelectInvoice(item)}
                  activeOpacity={0.7}
                >
                  <ThemedText style={[styles.lovItemText, styles.codeCell, { flex: 1.5 }]}>
                    {item.RepairNo}
                  </ThemedText>
                  <ThemedText style={[styles.lovItemText, { flex: 1.2 }]}>
                    {item.RepairDate ? item.RepairDate.substring(0, 10) : ''}
                  </ThemedText>
                  <ThemedText style={[styles.lovItemText, { flex: 1.5 }]}>{item.CustomerCode}</ThemedText>
                </TouchableOpacity>
              )}
            />
          </View>
        </View>
      </Modal>
      {/* REPRINT REPAIR DOCKET MODAL (matching PnlReprint in frmRepairEntry.vb) */}
      <Modal visible={isReprintModalVisible} transparent animationType="fade">
        <View style={styles.modalOverlay}>
          <View style={[styles.lovModalCard, { maxHeight: 250, width: '90%', maxWidth: 450 }]}>
            <View style={styles.modalHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="print-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.modalHeaderTitle}>RePrint Repair Docket</ThemedText>
              </View>
              <TouchableOpacity
                onPress={handleReprintCancel}
                activeOpacity={0.7}
                style={styles.closeModalBtn}
              >
                <Ionicons name="close" size={18} color="#3B2D17" />
              </TouchableOpacity>
            </View>

            <View style={{ padding: 16 }}>
              <ThemedText style={[styles.inlineFieldLabel, { marginBottom: 6 }]}>
                Repair Invoice / Docket No:
              </ThemedText>
              <TextInput
                ref={reprintInputRef}
                style={[styles.formInput, { height: 38 }]}
                value={reprintBillNo}
                onChangeText={setReprintBillNo}
                placeholder="e.g. RP-HQ00000001"
                placeholderTextColor="#9E8D77"
                autoFocus
                onSubmitEditing={handleReprintSubmit}
              />

              <View style={{ flexDirection: 'row', justifyContent: 'flex-end', gap: 10, marginTop: 18 }}>
                <TouchableOpacity
                  style={[styles.actionBtn, styles.clearBtn, { paddingHorizontal: 16, height: 36 }]}
                  onPress={handleReprintCancel}
                  activeOpacity={0.8}
                >
                  <ThemedText style={styles.clearBtnText}>Cancel</ThemedText>
                </TouchableOpacity>

                <TouchableOpacity
                  style={[styles.actionBtn, styles.saveBtn, { paddingHorizontal: 20, height: 36 }]}
                  onPress={handleReprintSubmit}
                  activeOpacity={0.8}
                  disabled={loading}
                >
                  <Ionicons name="print" size={14} color="#9E6D08" />
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
  cancelRepairBtn: {
    backgroundColor: '#FFEBEE',
    borderColor: '#EF9A9A',
  },
  cancelRepairBtnText: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#C62828',
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
    minWidth: 260,
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

  // Date input wrapper
  datePickerInputWrap: {
    flex: 1,
    flexDirection: 'row',
    alignItems: 'center',
    height: 26,
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#D4C3A3',
    borderRadius: 4,
    paddingHorizontal: 4,
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

  // Item Entry Bar
  itemEntryBar: {
    borderTopWidth: 1,
    borderTopColor: '#E8DCB8',
    backgroundColor: '#FCFAF6',
    padding: 10,
  },
  itemEntryRow: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    gap: 8,
  },
  subformControlsRow: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    marginTop: 8,
  },
  checkboxPill: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#E2D3B3',
    borderRadius: 4,
    paddingVertical: 4,
    paddingHorizontal: 8,
    gap: 4,
  },
  checkboxPillText: {
    fontSize: 11,
    fontWeight: '500',
    color: '#5C4A28',
  },
  checkboxPillTextSelected: {
    fontWeight: 'bold',
    color: '#9E6D08',
  },
  subformActionBtns: {
    flexDirection: 'row',
    gap: 8,
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
  clearItemBtn: {
    backgroundColor: '#FFFFFF',
    borderColor: '#DFD5C3',
  },
  clearItemBtnText: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#5C4A28',
  },

  // ===== PASSWORD MODAL =====
  passwordModalCard: {
    width: 320,
    backgroundColor: '#FFFFFF',
    borderRadius: 10,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    padding: 20,
    alignItems: 'center',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.15,
    shadowRadius: 8,
    elevation: 8,
  },
  passwordHeader: {
    alignItems: 'center',
    marginBottom: 16,
  },
  lockIconBadge: {
    width: 44,
    height: 44,
    borderRadius: 22,
    backgroundColor: '#F5E7CA',
    justifyContent: 'center',
    alignItems: 'center',
    marginBottom: 8,
    borderWidth: 1,
    borderColor: '#E2D3B3',
  },
  passwordTitle: {
    fontSize: 15,
    fontWeight: 'bold',
    color: '#3B2D17',
  },
  passwordSubtitle: {
    fontSize: 11,
    color: '#8D6409',
    marginTop: 2,
    textAlign: 'center',
  },
  passwordInput: {
    width: '100%',
    height: 38,
    backgroundColor: '#FCFAF6',
    borderWidth: 1,
    borderColor: '#D4C3A3',
    borderRadius: 6,
    paddingHorizontal: 12,
    fontSize: 14,
    color: '#2B2118',
    marginBottom: 16,
  },
  passwordBtnRow: {
    flexDirection: 'row',
    width: '100%',
    gap: 8,
  },
  passwordBtn: {
    flex: 1,
    height: 34,
    borderRadius: 6,
    justifyContent: 'center',
    alignItems: 'center',
  },
  passwordCancelBtn: {
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#DFD5C3',
  },
  passwordCancelText: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#5C4A28',
  },
  passwordVerifyBtn: {
    backgroundColor: '#B8903B',
  },
  passwordVerifyText: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#FFFFFF',
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
  codeCell: {
    fontFamily: Platform.OS === 'ios' ? 'Courier' : 'monospace',
    fontWeight: 'bold',
    color: '#8A5D00',
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
