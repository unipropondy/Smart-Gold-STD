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
  const [companyTradeInLimit, setCompanyTradeInLimit] = useState('0.00');
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

  // Standardized Print Report Modal state
  const [isPrintModalVisible, setIsPrintModalVisible] = useState(false);
  const [activePrintRepairNo, setActivePrintRepairNo] = useState('');
  const [activePrintIsReprint, setActivePrintIsReprint] = useState(false);

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
      } catch (fErr) { }
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
        handleClear();
        if (savedRepairNo) {
          setActivePrintRepairNo(savedRepairNo);
          setActivePrintIsReprint(false);
          setIsPrintModalVisible(true);
        }
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
        setActivePrintRepairNo(cleanNo);
        setActivePrintIsReprint(false);
        setIsPrintModalVisible(true);
      }
    }
  }, [params?.paymentSuccess]);

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
      if (params.repairNo && !invoiceNo) {
        setInvoiceNo(String(params.repairNo));
      }
      showToast(`Trade-In applied: $${returnedTradeIn.toFixed(2)}`, 'success');
    }
  }, [params?.tradeInAmount, params?.repairNo]);

  // Navigate to Trade-In screen following frmRepairEntry.vb / frmInstallment_AJ.vb logic
  const handleNavigateToTradeIn = () => {
    let refNo = invoiceNo.trim();
    if (!refNo) {
      refNo = '00000001';
    }

    // Determine sales amount / total reference amount
    const salesAmt = parseFloat(totalCost) || parseFloat(advancePaid) || parseFloat(balanceAmount) || 0;

    const navParams: Record<string, string> = {
      calledFrom: 'REPAIRENTRY',
      sCalledFrom: 'REPAIRENTRY',
      invoiceNo: refNo,
      sInvoiceNo: refNo,
      customerCode: customerCode || '',
      customerName: customerName || '',
      customerType: 'Customer',
      address1: address || '',
      address2: '',
      address3: '',
      tradeInLimit: companyTradeInLimit || '0.00',
      salesAmount: salesAmt.toFixed(2),
      sSalesAmount: salesAmt.toFixed(2),
      remarks: remarks || '',
      mode: 'RepairEntry',
      paymentMode: tradeInOption,
    };

    router.push({
      pathname: '/tradeingold',
      params: navParams,
    });
  };

  // Standardized Cambria Report Generator for Repair Entry
  const buildRepairEntryHtml = (sInvoiceNo: string, isReprint: boolean, printData: any) => {
    const { company, header, details, payments, rates } = printData || {};

    const now = new Date();
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
    const sCustomerAddress = [
      header?.CustomerAddress1 || header?.Address1 || address,
      header?.CustomerAddress2 || header?.Address2,
      header?.CustomerAddress3 || header?.Address3,
    ].filter(Boolean).map((s: string) => String(s).trim()).join(' ');
    const sCustomerPhone = (header?.CustomerPhone || header?.Phone1 || header?.Phone || header?.Mobile || '').trim();

    const s999 = rates?.rate999 || '-';
    const s916 = rates?.rate916 || '-';
    const s835 = rates?.rate835 || '-';

    const totalEstimatedCost = parseFloat(header?.TotalCostEstimated ?? totalCost) || 0;
    const advancePaidAmt = parseFloat(header?.AdvancePaid ?? advancePaid) || 0;

    let totalInstallmentsPaid = 0;
    if (payments && Array.isArray(payments)) {
      payments.forEach((p: any) => {
        totalInstallmentsPaid += parseFloat(p.AmountPaid) || 0;
      });
    }
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
    const issueDateFormatted = formatDate(header?.IssueDate);

    const sReportTitle = isReprint ? 'REPAIR ENTRY VOUCHER (DUPLICATE COPY)' : 'REPAIR ENTRY VOUCHER';

    const generatePageMarkup = (copyLabel: string) => `
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
              <div class="card-title">CUSTOMER INFORMATION</div>
              <table class="info-table">
                <tr>
                  <td class="lbl">Customer</td>
                  <td class="colon">:</td>
                  <td class="val font-bold">${sCustomerName || 'CASH CUSTOMER'}</td>
                </tr>
                <tr>
                  <td class="lbl">Cust Code</td>
                  <td class="colon">:</td>
                  <td class="val">${sCustomerCode || '-'}</td>
                </tr>
                <tr>
                  <td class="lbl">Address</td>
                  <td class="colon">:</td>
                  <td class="val">${sCustomerAddress || '-'}</td>
                </tr>
                ${sCustomerPhone ? `
                <tr>
                  <td class="lbl">Contact</td>
                  <td class="colon">:</td>
                  <td class="val">${sCustomerPhone}</td>
                </tr>` : ''}
              </table>
            </div>

            <div class="info-card">
              <div class="card-title">REPAIR DOCKET DETAILS</div>
              <table class="info-table">
                <tr>
                  <td class="lbl">Repair Docket No</td>
                  <td class="colon">:</td>
                  <td class="val font-bold">${sInvoiceNo}</td>
                </tr>
                <tr>
                  <td class="lbl">Repair Date</td>
                  <td class="colon">:</td>
                  <td class="val">${repairDateFormatted}</td>
                </tr>
                <tr>
                  <td class="lbl">Target Delivery</td>
                  <td class="colon">:</td>
                  <td class="val">${deliveryDateFormatted}</td>
                </tr>
                ${issueDateFormatted && issueDateFormatted !== '-' ? `
                <tr>
                  <td class="lbl">Issue Date</td>
                  <td class="colon">:</td>
                  <td class="val">${issueDateFormatted}</td>
                </tr>` : ''}
                <tr>
                  <td class="lbl">Docket Status</td>
                  <td class="colon">:</td>
                  <td class="val font-bold">${(header?.Status || 'IN PROGRESS').trim()}</td>
                </tr>
                <tr>
                  <td class="lbl">Remarks</td>
                  <td class="colon">:</td>
                  <td class="val">${header?.Remarks || remarks || '-'}</td>
                </tr>
              </table>
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
                    <td style="text-align: center; font-weight: 700;">${item.LineNo || item.OrderInventoryCode || idx + 1}</td>
                    <td style="text-align: left; font-weight: 700;">${(item.Description || item.RepairDescription || 'Repair Work').trim()}</td>
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

          <!-- SUMMARY FINANCIAL BALANCES -->
          <div class="totals-section">
            <div class="totals-box">
              <div class="total-row">
                <span>Total Estimated Cost:</span>
                <span>$ ${totalEstimatedCost.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</span>
              </div>
              <div class="total-row">
                <span>Initial Advance Paid:</span>
                <span>$ ${advancePaidAmt.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</span>
              </div>
              ${totalInstallmentsPaid > 0 ? `
              <div class="total-row">
                <span>Subsequent Payments:</span>
                <span>$ ${totalInstallmentsPaid.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</span>
              </div>
              <div class="total-row">
                <span>Total Received To Date:</span>
                <span style="font-weight: 800; color: #8C5D08;">$ ${grandTotalPaid.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</span>
              </div>` : ''}
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
            staffName: (header?.CreateUser || salesManCode || 'Staff').trim(),
            receiverLabel: 'SUPPLIER / AUTHORIZED RECEIVER',
            receiverName: (sCustomerName || '-').trim()
          }
        })}
      </div>
    `;

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
            .info-table {
              width: 100%;
              border-collapse: collapse;
              font-size: 11px;
            }
            .info-table td {
              padding: 2px 0;
              vertical-align: top;
              line-height: 1.35;
            }
            .info-table .lbl {
              width: 130px;
              font-weight: 700;
              color: #222222;
            }
            .info-table .colon {
              width: 15px;
              font-weight: 700;
              color: #222222;
              text-align: center;
            }
            .info-table .val {
              color: #222222;
              font-weight: 700;
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

  // subPrintBill using hidden iframe
  const subPrintBill = async (targetInvoiceNo: string, isReprintMode: boolean = false) => {
    const sInvoiceNo = (targetInvoiceNo || '').trim();
    if (!sInvoiceNo) {
      showToast('Please enter Repair No.', 'error');
      return;
    }

    try {
      setLoading(true);
      const res = await fetch(`${API_URL}/print-data?repairNo=${encodeURIComponent(sInvoiceNo)}`);
      const json = await res.json();

      if (!json.success || !json.data) {
        showToast(json.message || `Repair record ${sInvoiceNo} not found for print.`, 'error');
        setLoading(false);
        return;
      }

      const htmlContent = buildRepairEntryHtml(sInvoiceNo, isReprintMode, json.data);
      const docTitle = isReprintMode ? `Repair Entry (Duplicate) - ${sInvoiceNo}` : `Repair Entry - ${sInvoiceNo}`;

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
        const frameId = '__repair_entry_print_frame__';
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
              showToast(`Repair entry ${sInvoiceNo} print dialog opened`, 'success');
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
            showToast('Repair entry report preview opened', 'success');
          } else {
            showToast('Popup blocker prevented opening print preview. Please allow popups.', 'error');
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

      const htmlContent = buildRepairEntryHtml(rawNo, isReprint, json.data);
      const downloadFilename = `Repair_Entry_${rawNo.replace(/[^a-zA-Z0-9_-]/g, '_')}.pdf`;

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
      const downloadFilename = `Repair_Entry_${rawNo.replace(/[^a-zA-Z0-9_-]/g, '_')}.xlsx`;

      if (Platform.OS === 'web' && typeof window !== 'undefined') {
        const url = URL.createObjectURL(excelBlob);
        const a = document.createElement('a');
        a.href = url;
        a.download = downloadFilename;
        document.body.appendChild(a);
        a.click();
        document.body.removeChild(a);
        setTimeout(() => URL.revokeObjectURL(url), 1000);
        showToast(`Repair Entry Excel downloaded: ${downloadFilename}`, 'success');
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

  // btnReprintOK_Click
  const handleReprintSubmit = async () => {
    try {
      const cleanNo = (reprintBillNo || '').trim();
      if (cleanNo !== '') {
        setIsReprintModalVisible(false);
        setReprintBillNo('');
        setActivePrintRepairNo(cleanNo);
        setActivePrintIsReprint(true);
        setIsPrintModalVisible(true);
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
            onPress={() => {
              const cleanNo = (invoiceNo || '').trim();
              if (!cleanNo) {
                handleOpenReprintModal();
                return;
              }
              setActivePrintRepairNo(cleanNo);
              setActivePrintIsReprint(true);
              setIsPrintModalVisible(true);
            }}
            activeOpacity={0.8}
            disabled={saving || loading}
          >
            <Ionicons name="print-outline" size={14} color="#5C4A28" />
            <ThemedText style={styles.clearBtnText}>Print</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity
            style={[styles.actionBtn, styles.reprintBtn]}
            onPress={handleOpenReprintModal}
            activeOpacity={0.8}
            disabled={saving || loading}
          >
            <Ionicons name="documents-outline" size={14} color="#5C4A28" />
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
            subPrintBill(invNo, reprint);
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
