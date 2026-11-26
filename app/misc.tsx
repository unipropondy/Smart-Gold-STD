/** Developer: durairaj */
import { ThemedText } from '@/components/themed-text';
import { PrintReportModal } from '@/components/PrintReportModal';
import { API_BASE_URL } from '@/constants/api';
import { getStandardReportCss, renderStandardReportFooter, renderStandardReportHeader } from '@/utils/standardPrintReport';
import { Ionicons } from '@expo/vector-icons';
import { Stack, useFocusEffect, useRouter } from 'expo-router';
import React, { useCallback, useEffect, useState } from 'react';
import {
  ActivityIndicator,
  KeyboardAvoidingView,
  Modal,
  Platform,
  ScrollView,
  StyleSheet,
  TextInput,
  TouchableOpacity,
  TouchableWithoutFeedback,
  View,
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';

const API_URL = `${API_BASE_URL}/misc`;

type OptionType = {
  label: string;
  code: string;
};

export default function MiscScreen() {
  const router = useRouter();

  // Form Fields State
  const [selectedType, setSelectedType] = useState<OptionType | null>(null);
  const [voucherNo, setVoucherNo] = useState('');
  const [date, setDate] = useState('');
  const [netAmount, setNetAmount] = useState('');
  const [gstPerc, setGstPerc] = useState('0');
  const [isZeroRated, setIsZeroRated] = useState(false);
  const [amount, setAmount] = useState('0.00');
  const [gst, setGst] = useState('0.00');
  const [weight, setWeight] = useState('');
  const [person, setPerson] = useState('');
  const [remarks, setRemarks] = useState('');
  const [userId, setUserId] = useState('sys');

  // Configuration and Control State
  const [defaultGstPerc, setDefaultGstPerc] = useState('0');
  const [companyShortCode, setCompanyShortCode] = useState('');
  const [typeOptions, setTypeOptions] = useState<OptionType[]>([]);
  const [isTypeDropdownOpen, setIsTypeDropdownOpen] = useState(false);

  // Date Picker Modal State
  const [isCalendarOpen, setIsCalendarOpen] = useState(false);
  const [calendarMonth, setCalendarMonth] = useState(new Date().getMonth());
  const [calendarYear, setCalendarYear] = useState(new Date().getFullYear());

  const monthNames = [
    "January", "February", "March", "April", "May", "June",
    "July", "August", "September", "October", "November", "December"
  ];

  // Status State
  const [loading, setLoading] = useState(false);
  const [isEditing, setIsEditing] = useState(false);
  const [toast, setToast] = useState<{ message: string; type: 'success' | 'error' } | null>(null);

  // Print Report Modal State
  const [isPrintModalVisible, setIsPrintModalVisible] = useState(false);
  const [printType, setPrintType] = useState('');
  const [printVoucherNo, setPrintVoucherNo] = useState('');
  const [isReprintMode, setIsReprintMode] = useState(false);

  const showToast = (message: string, type: 'success' | 'error' = 'success') => {
    setToast({ message, type });
    setTimeout(() => {
      setToast(null);
    }, 3000);
  };

  // Keyboard Shortcuts matching VB frmMisc_KeyDown:
  // Enter/Tab, F7 (Save/Add), F6 (Cancel/Clear), Escape (Back)
  useFocusEffect(
    useCallback(() => {
      if (Platform.OS === 'web') {
        const handleKeyDown = (e: KeyboardEvent) => {
          if (e.key === 'F7') {
            e.preventDefault();
            handleSave();
          } else if (e.key === 'F6') {
            e.preventDefault();
            handleCancel();
          } else if (e.key === 'Escape') {
            e.preventDefault();
            if (isCalendarOpen) {
              setIsCalendarOpen(false);
            } else if (isTypeDropdownOpen) {
              setIsTypeDropdownOpen(false);
            } else {
              router.back();
            }
          }
        };
        window.addEventListener('keydown', handleKeyDown);
        return () => window.removeEventListener('keydown', handleKeyDown);
      }
    }, [
      selectedType,
      voucherNo,
      date,
      amount,
      netAmount,
      gst,
      gstPerc,
      weight,
      person,
      remarks,
      userId,
      isEditing,
      isCalendarOpen,
      isTypeDropdownOpen
    ])
  );

  // Fetch initial configuration on mount
  useEffect(() => {
    const fetchCompanyConfig = async () => {
      setLoading(true);
      try {
        const res = await fetch(`${API_URL}/company-info`);
        const json = await res.json();
        if (json.success) {
          const gstVal = String(json.GSTPerc);
          setGstPerc(gstVal);
          setDefaultGstPerc(gstVal);
          setCompanyShortCode(json.CompanyShortCode);

          // Configure type options based on company short code
          const shortCodeUpper = (json.CompanyShortCode || '').toUpperCase();
          if (shortCodeUpper === 'SSJ') {
            setTypeOptions([{ label: 'Services', code: 'SR' }]);
          } else {
            setTypeOptions([
              { label: 'Purchase Note', code: 'PN' },
              { label: 'Credit Note', code: 'CN' },
              { label: 'Misc.Note', code: 'MC' },
              { label: 'Petty Cash', code: 'PC' },
            ]);
          }
        }
      } catch (err) {
        showToast('Failed to load company config.', 'error');
      } finally {
        setLoading(false);
      }
    };

    // Set today's date format (dd/MM/yyyy)
    const today = new Date();
    const dd = String(today.getDate()).padStart(2, '0');
    const mm = String(today.getMonth() + 1).padStart(2, '0');
    const yyyy = today.getFullYear();
    setDate(`${dd}/${mm}/${yyyy}`);

    fetchCompanyConfig();
  }, []);

  // Fetch next voucher number when Type selection changes
  useEffect(() => {
    if (!selectedType || isEditing) return;

    const fetchNextVoucher = async () => {
      setLoading(true);
      try {
        const res = await fetch(`${API_URL}/next-voucher?type=${selectedType.code}`);
        const json = await res.json();
        if (json.success) {
          setVoucherNo(String(json.VoucherNo));
        }
      } catch (err) {
        showToast('Failed to fetch next voucher number', 'error');
      } finally {
        setLoading(false);
      }
    };

    fetchNextVoucher();
  }, [selectedType, isEditing]);

  // Handle calculation when NetAmount, GST % changes
  const handleCalculate = (netAmtValue: string, currentGstPerc: string) => {
    const netAmt = parseFloat(netAmtValue) || 0;
    const gstP = parseFloat(currentGstPerc) || 0;

    if (netAmt > 0) {
      const gstVal = netAmt - netAmt / (1 + gstP / 100);
      const amtVal = netAmt - gstVal;
      setGst(gstVal.toFixed(2));
      setAmount(amtVal.toFixed(2));
    } else {
      setGst('0.00');
      setAmount('0.00');
    }
  };

  const handleNetAmountChange = (text: string) => {
    setNetAmount(text);
    handleCalculate(text, gstPerc);
  };

  const handleZeroRatedChange = () => {
    const nextZeroState = !isZeroRated;
    setIsZeroRated(nextZeroState);

    const nextGstPerc = nextZeroState ? '0' : defaultGstPerc;
    setGstPerc(nextGstPerc);
    handleCalculate(netAmount, nextGstPerc);
  };

  // FETCH function
  const handleFetch = async () => {
    if (!selectedType) {
      showToast('Please select a Type first', 'error');
      return;
    }
    if (!voucherNo.trim()) {
      showToast('Please enter a Voucher No', 'error');
      return;
    }

    setLoading(true);
    try {
      const res = await fetch(`${API_URL}/fetch?type=${selectedType.code}&voucherNo=${voucherNo.trim()}`);
      const json = await res.json();
      if (json.success && json.data) {
        const data = json.data;

        // Map Date
        if (data.Date) {
          const d = new Date(data.Date);
          const dd = String(d.getDate()).padStart(2, '0');
          const mm = String(d.getMonth() + 1).padStart(2, '0');
          const yyyy = d.getFullYear();
          setDate(`${dd}/${mm}/${yyyy}`);
        }

        setNetAmount(data.NetAmount !== null ? String(data.NetAmount) : '');
        setGstPerc(data.GSTPerc !== null ? String(data.GSTPerc) : '0');
        setGst(data.GST !== null ? Number(data.GST).toFixed(2) : '0.00');
        setAmount(data.Amount !== null ? Number(data.Amount).toFixed(2) : '0.00');
        setWeight(data.Weight !== null ? String(data.Weight) : '');
        setPerson(data.Person || '');
        setRemarks(data.Remarks || '');
        setUserId(data.CreateUser || 'sys');
        setIsZeroRated(parseFloat(data.GSTPerc || 0) === 0);
        setIsEditing(true);

        showToast('Voucher details fetched successfully');
      } else {
        showToast(json.message || 'Voucher not found', 'error');
      }
    } catch (err) {
      showToast('Error fetching voucher details', 'error');
    } finally {
      setLoading(false);
    }
  };

  const handleSave = async () => {
    if (!selectedType) {
      showToast('Select Type', 'error');
      return;
    }
    if (!voucherNo || !voucherNo.trim()) {
      showToast('Enter VoucherNo', 'error');
      return;
    }
    if (isNaN(parseInt(voucherNo))) {
      showToast('VoucherNo must be a valid number', 'error');
      return;
    }
    if (!date || !date.trim()) {
      showToast('Select or enter Date', 'error');
      return;
    }
    const dateParts = date.split('/');
    if (dateParts.length !== 3) {
      showToast('Enter Date in dd/MM/yyyy format', 'error');
      return;
    }
    const parsedDay = parseInt(dateParts[0]);
    const parsedMonth = parseInt(dateParts[1]);
    const parsedYear = parseInt(dateParts[2]);
    if (isNaN(parsedDay) || isNaN(parsedMonth) || isNaN(parsedYear) || parsedMonth < 1 || parsedMonth > 12 || parsedDay < 1 || parsedDay > 31) {
      showToast('Enter a valid Date', 'error');
      return;
    }
    if (parseFloat(amount || '0') === 0 && (!netAmount || !netAmount.trim() || parseFloat(netAmount) === 0)) {
      showToast('Enter Amount', 'error');
      return;
    }

    let parsedDate = new Date();
    if (date) {
      const parts = date.split('/');
      if (parts.length === 3) {
        parsedDate = new Date(parseInt(parts[2]), parseInt(parts[1]) - 1, parseInt(parts[0]));
      }
    }

    const payload = {
      Type: selectedType.code,
      VoucherNo: parseInt(voucherNo),
      Date: parsedDate.toISOString(),
      Person: person.trim(),
      Amount: parseFloat(amount),
      GSTPerc: parseFloat(gstPerc),
      GST: parseFloat(gst),
      NetAmount: parseFloat(netAmount),
      Weight: parseFloat(weight) || 0,
      Remarks: remarks.trim(),
      CreateUser: userId.trim() || 'sys',
      ModifyUser: userId.trim() || 'sys',
    };

    const savedType = selectedType.code;
    const savedVoucherNo = voucherNo;

    setLoading(true);
    try {
      const response = await fetch(API_URL, {
        method: isEditing ? 'PUT' : 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload),
      });

      const json = await response.json();
      if (json.success) {
        showToast(json.message || 'Miscellaneous details saved successfully');
        handleCancel();
        // Open Print Invoice confirmation popup after successful backend save
        if (savedType && savedVoucherNo) {
          setPrintType(savedType);
          setPrintVoucherNo(savedVoucherNo);
          setIsReprintMode(false);
          setIsPrintModalVisible(true);
        }
      } else {
        showToast(json.message || 'Failed to save', 'error');
      }
    } catch (err) {
      showToast('Network error saving details', 'error');
    } finally {
      setLoading(false);
    }
  };

  // DELETE function
  const handleDelete = async () => {
    if (!selectedType) {
      showToast('Please select a Type first', 'error');
      return;
    }
    if (!voucherNo.trim()) {
      showToast('Please enter a Voucher No', 'error');
      return;
    }

    const executeDelete = async () => {
      setLoading(true);
      try {
        const response = await fetch(`${API_URL}?type=${selectedType.code}&voucherNo=${voucherNo.trim()}`, {
          method: 'DELETE',
        });
        const json = await response.json();
        if (json.success) {
          showToast('Miscellaneous details deleted');
          handleCancel();
        } else {
          showToast(json.message || 'Failed to delete', 'error');
        }
      } catch (err) {
        showToast('Error deleting entry', 'error');
      } finally {
        setLoading(false);
      }
    };

    if (Platform.OS === 'web') {
      if (typeof window !== 'undefined' && window.confirm(`Do you want to delete ${selectedType.label} Type?`)) {
        executeDelete();
      }
    } else {
      executeDelete();
    }
  };

  // Helper to build standardized HTML for Miscellaneous Entry Voucher
  const buildMiscVoucherHtml = (trimmedType: string, trimmedVoucherNo: string, isReprint: boolean, printData: any) => {
    const { company, voucher, typeName } = printData;

    // Format dates
    const formatDate = (val: any) => {
      if (!val) return '-';
      const d = new Date(val);
      if (isNaN(d.getTime())) return String(val).split('T')[0] || '-';
      return d.toLocaleDateString('en-GB', { day: '2-digit', month: '2-digit', year: 'numeric' });
    };

    const voucherDateFormatted = formatDate(voucher.Date || voucher.DateOfTransaction);
    const printTimeStr = new Date().toLocaleTimeString('en-US', { hour: '2-digit', minute: '2-digit', hour12: true });

    // Company info
    const compName = (company.name || 'SMART').trim();
    const compAddr1 = (company.address1 || '').trim();
    const compAddr2 = [company.address2, company.city, company.country].filter(Boolean).join(' ');
    const compContact = [
      company.phone ? `Tel: ${company.phone}` : '',
      company.fax ? `Fax: ${company.fax}` : ''
    ].filter(Boolean).join(' | ');

    const vAmount = parseFloat(voucher.Amount) || 0;
    const vGst = parseFloat(voucher.GST) || 0;
    const vGstPerc = parseFloat(voucher.GSTPerc) || 0;
    const vNetAmount = parseFloat(voucher.NetAmount) || (vAmount + vGst);
    const vWeight = parseFloat(voucher.Weight) || 0;
    const isZeroRated = vGstPerc === 0 && vGst === 0;

    const reportTitle = 'MISCELLANEOUS ENTRY';
    const docTitle = `Miscellaneous Entry - ${voucher.Type}-${voucher.VoucherNo}`;

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

            /* INFO GRID (2 PANELS) */
            .info-grid {
              display: grid;
              grid-template-columns: 1.15fr 0.85fr;
              gap: 16px;
              margin-top: 8px;
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
            .info-table {
              width: 100%;
              border-collapse: collapse;
              font-size: 11px;
            }
            .info-table td {
              padding: 2.5px 0;
              vertical-align: top;
              line-height: 1.35;
            }
            .info-table .lbl {
              width: 120px;
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
            .info-table .val-right {
              color: #222222;
              font-weight: 700;
              text-align: right;
            }

            /* DETAIL TABLE */
            .detail-table {
              width: 100%;
              border-collapse: collapse;
              margin-top: 6px;
              margin-bottom: 14px;
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
                printedDateTime: `${voucherDateFormatted} ${printTimeStr}`
              })}

              <!-- INFO GRID: Voucher Details & Financial Summary -->
              <div class="info-grid">
                <!-- VOUCHER DETAILS -->
                <div class="info-card">
                  <div class="card-title">VOUCHER DETAILS</div>
                  <table class="info-table">
                    <tr>
                      <td class="lbl">Type</td>
                      <td class="colon">:</td>
                      <td class="val">${typeName} (${voucher.Type})</td>
                    </tr>
                    <tr>
                      <td class="lbl">Voucher No</td>
                      <td class="colon">:</td>
                      <td class="val">${voucher.VoucherNo}</td>
                    </tr>
                    <tr>
                      <td class="lbl">Date</td>
                      <td class="colon">:</td>
                      <td class="val">${voucherDateFormatted}</td>
                    </tr>
                    <tr>
                      <td class="lbl">Person</td>
                      <td class="colon">:</td>
                      <td class="val">${voucher.Person || '-'}</td>
                    </tr>
                    <tr>
                      <td class="lbl">Weight (g)</td>
                      <td class="colon">:</td>
                      <td class="val">${vWeight > 0 ? vWeight.toFixed(3) + ' g' : '0.000 g'}</td>
                    </tr>
                    <tr>
                      <td class="lbl">Remarks</td>
                      <td class="colon">:</td>
                      <td class="val">${voucher.Remarks || '-'}</td>
                    </tr>
                    <tr>
                      <td class="lbl">User Id</td>
                      <td class="colon">:</td>
                      <td class="val">${(voucher.CreateUser || 'sys').trim()}</td>
                    </tr>
                  </table>
                </div>

                <!-- FINANCIAL SUMMARY -->
                <div class="info-card">
                  <div class="card-title">FINANCIAL SUMMARY</div>
                  <table class="info-table">
                    <tr>
                      <td class="lbl">Zero Rated</td>
                      <td class="colon">:</td>
                      <td class="val-right">${isZeroRated ? 'Yes' : 'No'}</td>
                    </tr>
                    <tr>
                      <td class="lbl">Net Amount</td>
                      <td class="colon">:</td>
                      <td class="val-right">$ ${vNetAmount.toFixed(2)}</td>
                    </tr>
                    <tr>
                      <td class="lbl">GST Rate (%)</td>
                      <td class="colon">:</td>
                      <td class="val-right">${vGstPerc}%</td>
                    </tr>
                    <tr>
                      <td class="lbl">Amount (Excl.)</td>
                      <td class="colon">:</td>
                      <td class="val-right">$ ${vAmount.toFixed(2)}</td>
                    </tr>
                    <tr>
                      <td class="lbl">GST Amount</td>
                      <td class="colon">:</td>
                      <td class="val-right">$ ${vGst.toFixed(2)}</td>
                    </tr>
                    <tr style="border-top: 1.5px solid #8C5D08;">
                      <td class="lbl" style="color: #8C5D08; font-size: 12px; padding-top: 4px;">Total Payable</td>
                      <td class="colon" style="color: #8C5D08; font-size: 12px; padding-top: 4px;">:</td>
                      <td class="val-right" style="color: #8C5D08; font-size: 12.5px; font-weight: 800; padding-top: 4px;">$ ${vNetAmount.toFixed(2)}</td>
                    </tr>
                  </table>
                </div>
              </div>

              <!-- PARTICULARS TABLE -->
              <table class="detail-table">
                <thead>
                  <tr>
                    <th style="width: 6%; text-align: center;">#</th>
                    <th style="width: 20%;">TYPE</th>
                    <th style="width: 38%;">PARTICULARS / REMARKS</th>
                    <th style="width: 18%; text-align: right;">AMOUNT (EXCL.) ($)</th>
                    <th style="width: 18%; text-align: right;">NET AMOUNT ($)</th>
                  </tr>
                </thead>
                <tbody>
                  <tr>
                    <td style="text-align: center; font-weight: 700;">1</td>
                    <td style="font-weight: 700;">${typeName}</td>
                    <td style="font-weight: 700;">${voucher.Remarks || voucher.Person || 'Miscellaneous journal transaction'}</td>
                    <td style="text-align: right; font-weight: 700;">$ ${vAmount.toFixed(2)}</td>
                    <td style="text-align: right; font-weight: 800; color: #8C5D08;">$ ${vNetAmount.toFixed(2)}</td>
                  </tr>
                </tbody>
              </table>

              <!-- TOTALS SECTION -->
              <div class="totals-section">
                <div class="totals-box">
                  <div class="total-row">
                    <span>Base Amount:</span>
                    <span>$ ${vAmount.toFixed(2)}</span>
                  </div>
                  <div class="total-row">
                    <span>GST (${vGstPerc}%):</span>
                    <span>$ ${vGst.toFixed(2)}</span>
                  </div>
                  <div class="total-row total-row-grand">
                    <span>TOTAL PAYABLE:</span>
                    <span>$ ${vNetAmount.toFixed(2)}</span>
                  </div>
                </div>
              </div>
            </div>

            ${renderStandardReportFooter({
              staff: {
                staffLabel: 'PREPARED BY (STAFF)',
                staffName: (voucher.CreateUser || 'sys').trim(),
                receiverLabel: 'SUPPLIER / AUTHORIZED RECEIVER',
                receiverName: (voucher.Person || '-').trim()
              }
            })}
          </div>
        </body>
      </html>
    `;
  };

  // Original subPrintMiscJournal matching cmdReprint_Click in frmMisc.vb
  const subPrintMiscJournal = async (sType: string, sVoucherNo: string, isReprint: boolean = false) => {
    const trimmedType = (sType || '').trim();
    const trimmedVoucherNo = (sVoucherNo || '').trim();

    try {
      setLoading(true);
      const res = await fetch(`${API_URL}/print-data?type=${encodeURIComponent(trimmedType)}&voucherNo=${encodeURIComponent(trimmedVoucherNo)}`);
      const json = await res.json();

      if (!json.success || !json.data) {
        showToast(json.message || 'Miscellaneous voucher not found for print.', 'error');
        setLoading(false);
        return;
      }

      const htmlContent = buildMiscVoucherHtml(trimmedType, trimmedVoucherNo, isReprint, json.data);
      const docTitle = `Miscellaneous Entry - ${trimmedType}-${trimmedVoucherNo}`;

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
        const frameId = '__misc_voucher_print_frame__';
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
              showToast(`Voucher ${trimmedType}-${trimmedVoucherNo} print dialog opened`, 'success');
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
            showToast(`Voucher ${trimmedType}-${trimmedVoucherNo} print preview opened`, 'success');
          } else {
            showToast('Popup blocker prevented opening print preview. Please allow popups.', 'error');
          }
        }
      } else {
        showToast(`Voucher ${trimmedType}-${trimmedVoucherNo} print preview ready`, 'success');
      }
    } catch (err: any) {
      console.error('Print Error:', err);
      showToast('Failed to generate Miscellaneous Journal print preview', 'error');
    } finally {
      setLoading(false);
    }
  };

  // Dedicated PDF Download Functionality
  const handleDownloadPdf = async (sTypeInput?: string, sVoucherNoInput?: string, isReprint: boolean = false) => {
    let sType = (sTypeInput || '').trim();
    if (!sType) {
      const typeLabel = (selectedType?.label || '').trim();
      switch (typeLabel) {
        case 'Purchase Note': sType = 'PN'; break;
        case 'Credit Note': sType = 'CN'; break;
        case 'Misc. Note': sType = 'MC'; break;
        case 'Petty Cash': sType = 'PC'; break;
        case 'Services': sType = 'SR'; break;
        default: sType = selectedType?.code || ''; break;
      }
    }

    const trimmedVoucherNo = (sVoucherNoInput || voucherNo || '').trim();
    if (!sType || !trimmedVoucherNo) {
      showToast('Validation Error: Voucher Type and Voucher No are required to download PDF', 'error');
      return;
    }

    try {
      setLoading(true);
      const res = await fetch(`${API_URL}/print-data?type=${encodeURIComponent(sType)}&voucherNo=${encodeURIComponent(trimmedVoucherNo)}`);
      const json = await res.json();

      if (!json.success || !json.data) {
        showToast(json.message || 'Miscellaneous voucher not found', 'error');
        return;
      }

      const htmlContent = buildMiscVoucherHtml(sType, trimmedVoucherNo, isReprint, json.data);
      const downloadFilename = `Miscellaneous_Entry_Voucher_${sType}_${trimmedVoucherNo}.pdf`;

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
        showToast(`Miscellaneous Voucher PDF downloaded: ${downloadFilename}`, 'success');
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
  const handleDownloadExcel = async (sTypeInput?: string, sVoucherNoInput?: string, isReprint: boolean = false) => {
    let sType = (sTypeInput || '').trim();
    if (!sType) {
      const typeLabel = (selectedType?.label || '').trim();
      switch (typeLabel) {
        case 'Purchase Note': sType = 'PN'; break;
        case 'Credit Note': sType = 'CN'; break;
        case 'Misc. Note': sType = 'MC'; break;
        case 'Petty Cash': sType = 'PC'; break;
        case 'Services': sType = 'SR'; break;
        default: sType = selectedType?.code || ''; break;
      }
    }

    const trimmedVoucherNo = (sVoucherNoInput || voucherNo || '').trim();
    if (!sType || !trimmedVoucherNo) {
      showToast('Validation Error: Voucher Type and Voucher No are required to download Excel', 'error');
      return;
    }

    try {
      setLoading(true);
      const excelRes = await fetch(`${API_URL}/export-excel`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          type: sType,
          voucherNo: trimmedVoucherNo,
          isReprint: isReprint
        })
      });

      if (!excelRes.ok) {
        const errJson = await excelRes.json().catch(() => ({}));
        throw new Error(errJson.message || 'Server error generating Excel file');
      }

      const excelBlob = await excelRes.blob();
      const downloadFilename = `Miscellaneous_Entry_Voucher_${sType}_${trimmedVoucherNo}.xlsx`;

      if (Platform.OS === 'web' && typeof window !== 'undefined') {
        const url = URL.createObjectURL(excelBlob);
        const a = document.createElement('a');
        a.href = url;
        a.download = downloadFilename;
        document.body.appendChild(a);
        a.click();
        document.body.removeChild(a);
        setTimeout(() => URL.revokeObjectURL(url), 1000);
        showToast(`Miscellaneous Voucher Excel downloaded: ${downloadFilename}`, 'success');
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

  // REPRINT function matching cmdReprint_Click() in frmMisc.vb
  const getSelectedTypeCode = () => {
    let sType = '';
    const typeLabel = (selectedType?.label || '').trim();
    switch (typeLabel) {
      case 'Purchase Note':
        sType = 'PN';
        break;
      case 'Credit Note':
        sType = 'CN';
        break;
      case 'Misc.Note':
        sType = 'MC';
        break;
      case 'Petty Cash':
        sType = 'PC';
        break;
      case 'Services':
        sType = 'SR';
        break;
      default:
        sType = selectedType?.code || '';
        break;
    }
    return sType;
  };

  const handleReprint = () => {
    const sType = getSelectedTypeCode();
    if (!sType || !voucherNo.trim()) {
      showToast('Please select a Voucher Type and enter a Voucher No to reprint', 'error');
      return;
    }
    setPrintType(sType);
    setPrintVoucherNo(voucherNo.trim());
    setIsReprintMode(true);
    setIsPrintModalVisible(true);
  };

  // CANCEL / CLEAR function
  const handleCancel = () => {
    setSelectedType(null);
    setVoucherNo('');
    const today = new Date();
    const dd = String(today.getDate()).padStart(2, '0');
    const mm = String(today.getMonth() + 1).padStart(2, '0');
    const yyyy = today.getFullYear();
    setDate(`${dd}/${mm}/${yyyy}`);

    setNetAmount('');
    setGstPerc(defaultGstPerc);
    setAmount('0.00');
    setGst('0.00');
    setWeight('');
    setPerson('');
    setRemarks('');
    setUserId('sys');
    setIsZeroRated(false);
    setIsEditing(false);
  };

  // Calendar Helper Functions
  const handlePrevMonth = () => {
    if (calendarMonth === 0) {
      setCalendarMonth(11);
      setCalendarYear(calendarYear - 1);
    } else {
      setCalendarMonth(calendarMonth - 1);
    }
  };

  const handleNextMonth = () => {
    if (calendarMonth === 11) {
      setCalendarMonth(0);
      setCalendarYear(calendarYear + 1);
    } else {
      setCalendarMonth(calendarMonth + 1);
    }
  };

  const generateCalendarDays = () => {
    const numDays = new Date(calendarYear, calendarMonth + 1, 0).getDate();
    const firstDayIndex = new Date(calendarYear, calendarMonth, 1).getDay();
    const days = [];

    for (let i = 0; i < firstDayIndex; i++) {
      days.push(null);
    }

    for (let i = 1; i <= numDays; i++) {
      days.push({ day: i });
    }

    return days;
  };

  const handleSelectDate = (day: number) => {
    const selectedMonth = String(calendarMonth + 1).padStart(2, '0');
    const selectedDay = String(day).padStart(2, '0');
    setDate(`${selectedDay}/${selectedMonth}/${calendarYear}`);
    setIsCalendarOpen(false);
  };

  return (
    <SafeAreaView style={styles.safeArea} edges={['top']}>
      <Stack.Screen options={{ headerShown: false }} />

      {/* Top Header Bar matching jobsheet & pos */}
      <View style={styles.header}>
        <View style={styles.headerLeft}>
          <TouchableOpacity style={styles.backButton} onPress={() => router.back()}>
            <Ionicons name="arrow-back" size={20} color="#3B2D17" />
          </TouchableOpacity>
          <ThemedText style={styles.headerTitle}>Miscellaneous Entry</ThemedText>
        </View>

        <View style={styles.headerActions}>
          <TouchableOpacity style={styles.headerBtnSecondary} onPress={handleReprint}>
            <Ionicons name="print-outline" size={14} color="#554428" style={{ marginRight: 4 }} />
            <ThemedText style={styles.headerBtnSecondaryText}>Reprint</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity style={styles.headerBtnSecondary} onPress={handleCancel}>
            <Ionicons name="refresh-outline" size={14} color="#554428" style={{ marginRight: 4 }} />
            <ThemedText style={styles.headerBtnSecondaryText}>Cancel</ThemedText>
          </TouchableOpacity>

          {isEditing && (
            <TouchableOpacity style={styles.headerBtnDanger} onPress={handleDelete}>
              <Ionicons name="trash-outline" size={14} color="#D93829" style={{ marginRight: 4 }} />
              <ThemedText style={styles.headerBtnDangerText}>Delete</ThemedText>
            </TouchableOpacity>
          )}

          <TouchableOpacity
            style={[styles.headerBtnPrimary, loading && { opacity: 0.7 }]}
            onPress={handleSave}
            disabled={loading}
          >
            <Ionicons name="save-outline" size={14} color="#9E6D08" style={{ marginRight: 4 }} />
            <ThemedText style={styles.headerBtnPrimaryText}>{isEditing ? 'Update' : 'Add'}</ThemedText>
          </TouchableOpacity>
        </View>
      </View>

      <KeyboardAvoidingView behavior={Platform.OS === 'ios' ? 'padding' : 'height'} style={{ flex: 1 }}>
        <ScrollView contentContainerStyle={styles.contentContainer}>
          {/* Main Card Form using 100% full width layout */}
          <View style={styles.topSplitRow}>
            {/* Left Primary Card: Voucher Header & Transaction Data */}
            <View style={[styles.goldCard, { flex: 2 }]}>
              <View style={styles.cardHeaderBanner}>
                <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                  <View style={styles.headerIconBadge}>
                    <Ionicons name="document-text-outline" size={14} color="#B48218" />
                  </View>
                  <ThemedText style={styles.cardHeaderTitle}>Voucher Details</ThemedText>
                </View>

                {isEditing && (
                  <View style={styles.counterBadge}>
                    <ThemedText style={styles.counterBadgeText}>Editing Mode</ThemedText>
                  </View>
                )}
              </View>

              <View style={styles.cardBody}>
                {/* Row 1: Type * & Voucher No * */}
                <View style={styles.formRow}>
                  {/* Type Selector */}
                  <View style={{ flex: 1.1, flexDirection: 'row', alignItems: 'center', marginRight: 12 }}>
                    <ThemedText style={styles.inlineLabel}>Type *</ThemedText>
                    <TouchableOpacity
                      style={[styles.dropdownSelector, { flex: 1 }]}
                      onPress={() => setIsTypeDropdownOpen(true)}
                      activeOpacity={0.8}
                    >
                      <ThemedText style={[styles.dropdownSelectorText, !selectedType && { color: '#9E8D77' }]} numberOfLines={1}>
                        {selectedType ? selectedType.label : 'Select Type...'}
                      </ThemedText>
                      <Ionicons name="chevron-down" size={13} color="#7D6F5B" />
                    </TouchableOpacity>
                  </View>

                  {/* Voucher No & Fetch Button */}
                  <View style={{ flex: 1, flexDirection: 'row', alignItems: 'center' }}>
                    <ThemedText style={[styles.inlineLabel, { width: 85 }]}>Voucher No *</ThemedText>
                    <View style={{ flex: 1, flexDirection: 'row', alignItems: 'center' }}>
                      <TextInput
                        style={[styles.input, { flex: 1, borderTopRightRadius: 0, borderBottomRightRadius: 0 }]}
                        value={voucherNo}
                        onChangeText={setVoucherNo}
                        keyboardType="numeric"
                        placeholder="Auto or number"
                        placeholderTextColor="#9E8D77"
                      />
                      <TouchableOpacity style={styles.searchBtn} onPress={handleFetch} activeOpacity={0.8}>
                        <Ionicons name="search" size={12} color="#7D5E19" style={{ marginRight: 3 }} />
                        <ThemedText style={styles.searchBtnText}>Fetch</ThemedText>
                      </TouchableOpacity>
                    </View>
                  </View>
                </View>

                {/* Row 2: Date * & Person */}
                <View style={styles.formRow}>
                  {/* Date Input */}
                  <View style={{ flex: 1.1, flexDirection: 'row', alignItems: 'center', marginRight: 12 }}>
                    <ThemedText style={styles.inlineLabel}>Date *</ThemedText>
                    <View style={{ flex: 1, flexDirection: 'row', alignItems: 'center' }}>
                      <TextInput
                        style={[styles.input, { flex: 1, borderTopRightRadius: 0, borderBottomRightRadius: 0 }]}
                        value={date}
                        onChangeText={setDate}
                        placeholder="dd/MM/yyyy"
                        placeholderTextColor="#9E8D77"
                      />
                      <TouchableOpacity style={styles.lookupBtn} onPress={() => setIsCalendarOpen(true)} activeOpacity={0.8}>
                        <Ionicons name="calendar-outline" size={13} color="#6E5528" />
                      </TouchableOpacity>
                    </View>
                  </View>

                  {/* Person Input */}
                  <View style={{ flex: 1, flexDirection: 'row', alignItems: 'center' }}>
                    <ThemedText style={[styles.inlineLabel, { width: 85 }]}>Person</ThemedText>
                    <TextInput
                      style={[styles.input, { flex: 1 }]}
                      value={person}
                      onChangeText={setPerson}
                      placeholder="Name of Person"
                      placeholderTextColor="#9E8D77"
                    />
                  </View>
                </View>

                {/* Row 3: Weight (g) & Remarks */}
                <View style={styles.formRow}>
                  <View style={{ flex: 1.1, flexDirection: 'row', alignItems: 'center', marginRight: 12 }}>
                    <ThemedText style={styles.inlineLabel}>Weight (g)</ThemedText>
                    <TextInput
                      style={[styles.input, { flex: 1, textAlign: 'right' }]}
                      value={weight}
                      onChangeText={setWeight}
                      keyboardType="numeric"
                      placeholder="0.00"
                      placeholderTextColor="#9E8D77"
                    />
                  </View>

                  <View style={{ flex: 1, flexDirection: 'row', alignItems: 'center' }}>
                    <ThemedText style={[styles.inlineLabel, { width: 85 }]}>Remarks</ThemedText>
                    <TextInput
                      style={[styles.input, { flex: 1 }]}
                      value={remarks}
                      onChangeText={setRemarks}
                      placeholder="Voucher remarks"
                      placeholderTextColor="#9E8D77"
                    />
                  </View>
                </View>

                {/* Row 4: User Id */}
                <View style={styles.formRow}>
                  <View style={{ flex: 1.1, flexDirection: 'row', alignItems: 'center', marginRight: 12 }}>
                    <ThemedText style={styles.inlineLabel}>User Id</ThemedText>
                    <TextInput
                      style={[styles.input, styles.readonlyInput, { flex: 1 }]}
                      value={userId}
                      onChangeText={setUserId}
                      editable={false}
                    />
                  </View>
                  <View style={{ flex: 1 }} />
                </View>
              </View>
            </View>

            {/* Right Card: Financials & Tax Calculation */}
            <View style={[styles.goldCard, { flex: 1 }]}>
              <View style={styles.cardHeaderBanner}>
                <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                  <View style={styles.headerIconBadge}>
                    <Ionicons name="calculator-outline" size={14} color="#B48218" />
                  </View>
                  <ThemedText style={styles.cardHeaderTitle}>Financial Summary</ThemedText>
                </View>

                {/* Zero Rated Checkbox */}
                <TouchableOpacity
                  style={styles.zeroRatedToggle}
                  onPress={handleZeroRatedChange}
                  activeOpacity={0.8}
                >
                  <View style={[styles.checkboxSquare, isZeroRated && styles.checkboxSquareActive]}>
                    {isZeroRated && <Ionicons name="checkmark" size={11} color="#FFFFFF" />}
                  </View>
                  <ThemedText style={styles.zeroRatedLabel}>Zero Rated</ThemedText>
                </TouchableOpacity>
              </View>

              <View style={styles.summaryBody}>
                {/* Net Amount */}
                <View style={styles.summaryRow}>
                  <ThemedText style={styles.summaryLabel}>Net Amount *</ThemedText>
                  <TextInput
                    style={[styles.input, { width: 130, textAlign: 'right', fontWeight: 'bold', borderColor: '#B8903B' }]}
                    value={netAmount}
                    onChangeText={handleNetAmountChange}
                    keyboardType="numeric"
                    placeholder="0.00"
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* GST Rate */}
                <View style={styles.summaryRow}>
                  <ThemedText style={styles.summaryLabel}>GST Rate (%)</ThemedText>
                  <TextInput
                    style={[styles.input, styles.readonlyInput, { width: 130, textAlign: 'right' }]}
                    value={`${gstPerc}%`}
                    editable={false}
                  />
                </View>

                {/* Amount (Excl. GST) */}
                <View style={styles.summaryRow}>
                  <ThemedText style={styles.summaryLabel}>Amount (Excl.)</ThemedText>
                  <TextInput
                    style={[styles.input, styles.readonlyInput, { width: 130, textAlign: 'right' }]}
                    value={amount}
                    editable={false}
                  />
                </View>

                {/* GST Amount */}
                <View style={styles.summaryRow}>
                  <ThemedText style={styles.summaryLabel}>GST Amount</ThemedText>
                  <TextInput
                    style={[styles.input, styles.readonlyInput, { width: 130, textAlign: 'right' }]}
                    value={gst}
                    editable={false}
                  />
                </View>

                {/* Total Net Banner */}
                <View style={[styles.summaryRow, styles.summaryTotalRow]}>
                  <ThemedText style={styles.summaryTotalLabel}>Total Payable</ThemedText>
                  <ThemedText style={styles.summaryTotalValue}>${parseFloat(netAmount || '0').toFixed(2)}</ThemedText>
                </View>
              </View>
            </View>
          </View>
        </ScrollView>
      </KeyboardAvoidingView>

      {/* Calendar Date Picker Modal */}
      <Modal visible={isCalendarOpen} transparent={true} animationType="fade">
        <TouchableOpacity style={styles.modalOverlay} activeOpacity={1} onPress={() => setIsCalendarOpen(false)}>
          <TouchableWithoutFeedback>
            <View style={styles.calendarModalCard}>
              <View style={styles.modalHeader}>
                <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                  <View style={styles.headerIconBadge}>
                    <Ionicons name="calendar" size={14} color="#B48218" />
                  </View>
                  <ThemedText style={styles.modalTitle}>Select Voucher Date</ThemedText>
                </View>
                <TouchableOpacity onPress={() => setIsCalendarOpen(false)} style={{ padding: 4 }}>
                  <Ionicons name="close" size={20} color="#554428" />
                </TouchableOpacity>
              </View>

              <View style={{ padding: 14 }}>
                {/* Navigation Row */}
                <View style={styles.calendarNavRow}>
                  <TouchableOpacity style={styles.navArrow} onPress={handlePrevMonth}>
                    <Ionicons name="chevron-back" size={18} color="#3B2D17" />
                  </TouchableOpacity>
                  <ThemedText style={{ fontSize: 13, fontWeight: 'bold', color: '#3B2D17' }}>
                    {monthNames[calendarMonth]} {calendarYear}
                  </ThemedText>
                  <TouchableOpacity style={styles.navArrow} onPress={handleNextMonth}>
                    <Ionicons name="chevron-forward" size={18} color="#3B2D17" />
                  </TouchableOpacity>
                </View>

                {/* Weekdays */}
                <View style={styles.weekdaysRow}>
                  {['S', 'M', 'T', 'W', 'T', 'F', 'S'].map((day, idx) => (
                    <ThemedText key={idx} style={styles.weekdayCell}>{day}</ThemedText>
                  ))}
                </View>

                {/* Days Grid */}
                <View style={styles.daysGrid}>
                  {generateCalendarDays().map((dayInfo, idx) => {
                    let isSelected = false;
                    if (dayInfo && date) {
                      const dateParts = date.split('/');
                      if (dateParts.length === 3) {
                        const dayPart = parseInt(dateParts[0]);
                        const monthPart = parseInt(dateParts[1]) - 1;
                        const yearPart = parseInt(dateParts[2]);
                        isSelected = dayInfo.day === dayPart && calendarMonth === monthPart && calendarYear === yearPart;
                      }
                    }

                    return (
                      <TouchableOpacity
                        key={idx}
                        disabled={!dayInfo}
                        style={[
                          styles.dayCell,
                          !dayInfo && styles.emptyCell,
                          isSelected && styles.dayCellSelected,
                        ]}
                        onPress={() => dayInfo && handleSelectDate(dayInfo.day)}
                      >
                        <ThemedText style={[styles.dayCellText, isSelected && styles.dayCellTextSelected]}>
                          {dayInfo ? dayInfo.day : ''}
                        </ThemedText>
                      </TouchableOpacity>
                    );
                  })}
                </View>
              </View>
            </View>
          </TouchableWithoutFeedback>
        </TouchableOpacity>
      </Modal>

      {/* Type Selection Modal */}
      <Modal visible={isTypeDropdownOpen} transparent={true} animationType="fade">
        <TouchableOpacity style={styles.modalOverlay} activeOpacity={1} onPress={() => setIsTypeDropdownOpen(false)}>
          <TouchableWithoutFeedback>
            <View style={styles.typeModalCard}>
              <View style={styles.modalHeader}>
                <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                  <View style={styles.headerIconBadge}>
                    <Ionicons name="list" size={14} color="#B48218" />
                  </View>
                  <ThemedText style={styles.modalTitle}>Select Voucher Type</ThemedText>
                </View>
                <TouchableOpacity onPress={() => setIsTypeDropdownOpen(false)} style={{ padding: 4 }}>
                  <Ionicons name="close" size={20} color="#554428" />
                </TouchableOpacity>
              </View>

              <View style={{ padding: 8 }}>
                {typeOptions.map((opt) => {
                  const isSelected = selectedType?.code === opt.code;
                  return (
                    <TouchableOpacity
                      key={opt.code}
                      style={[
                        styles.typeModalItem,
                        isSelected && styles.typeModalItemSelected,
                      ]}
                      onPress={() => {
                        setSelectedType(opt);
                        setIsTypeDropdownOpen(false);
                      }}
                      activeOpacity={0.7}
                    >
                      <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                        <View style={[styles.typeIconCircle, isSelected && styles.typeIconCircleSelected]}>
                          <Ionicons
                            name={
                              opt.code === 'PN' ? 'cart-outline' :
                              opt.code === 'CN' ? 'card-outline' :
                              opt.code === 'PC' ? 'cash-outline' : 'document-text-outline'
                            }
                            size={14}
                            color={isSelected ? '#FFFFFF' : '#8A703F'}
                          />
                        </View>
                        <ThemedText style={[styles.typeModalItemText, isSelected && styles.typeModalItemTextSelected]}>
                          {opt.label}
                        </ThemedText>
                      </View>
                      {isSelected && (
                        <Ionicons name="checkmark-circle" size={18} color="#B48218" />
                      )}
                    </TouchableOpacity>
                  );
                })}
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
          setPrintType('');
          setPrintVoucherNo('');
        }}
        onPdf={() => {
          const sType = printType || getSelectedTypeCode();
          const vNo = printVoucherNo || voucherNo;
          const reprint = isReprintMode;
          setIsPrintModalVisible(false);
          setPrintType('');
          setPrintVoucherNo('');
          if (sType && vNo) {
            handleDownloadPdf(sType, vNo, reprint);
          }
        }}
        onExcel={() => {
          const sType = printType || getSelectedTypeCode();
          const vNo = printVoucherNo || voucherNo;
          const reprint = isReprintMode;
          setIsPrintModalVisible(false);
          setPrintType('');
          setPrintVoucherNo('');
          if (sType && vNo) {
            handleDownloadExcel(sType, vNo, reprint);
          }
        }}
        onPrint={() => {
          const sType = printType || getSelectedTypeCode();
          const vNo = printVoucherNo || voucherNo;
          const reprint = isReprintMode;
          setIsPrintModalVisible(false);
          setPrintType('');
          setPrintVoucherNo('');
          if (sType && vNo) {
            subPrintMiscJournal(sType, vNo, reprint);
          }
        }}
      />

      {/* Toast Alert */}
      {toast && (
        <View style={[styles.toastContainer, toast.type === 'error' ? styles.toastError : styles.toastSuccess]}>
          <Ionicons
            name={toast.type === 'error' ? 'alert-circle' : 'checkmark-circle'}
            size={16}
            color="#FFFFFF"
            style={{ marginRight: 6 }}
          />
          <ThemedText style={styles.toastText}>{toast.message}</ThemedText>
        </View>
      )}

      {loading && (
        <View style={styles.loadingOverlay}>
          <ActivityIndicator size="large" color="#B48218" />
        </View>
      )}
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  safeArea: {
    flex: 1,
    backgroundColor: '#FBF9F5',
  },
  contentContainer: {
    padding: 8,
  },

  // ===== TOP HEADER BAR =====
  header: {
    height: 38,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    backgroundColor: '#FFFFFF',
    borderBottomWidth: 1,
    borderBottomColor: '#EBE2D2',
    paddingHorizontal: 12,
  },
  headerLeft: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  backButton: {
    padding: 3,
    marginRight: 6,
  },
  headerTitle: {
    fontSize: 14,
    fontWeight: 'bold',
    color: '#3B2D17',
    letterSpacing: 0.3,
  },
  headerActions: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 6,
  },
  headerBtnSecondary: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#DFD3BC',
    borderRadius: 4,
    paddingHorizontal: 8,
    paddingVertical: 3,
  },
  headerBtnSecondaryText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#554428',
  },
  headerBtnDanger: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFF6F6',
    borderWidth: 1,
    borderColor: '#F2A8A2',
    borderRadius: 4,
    paddingHorizontal: 8,
    paddingVertical: 3,
  },
  headerBtnDangerText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#D93829',
  },
  headerBtnPrimary: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#F5E7CA',
    borderWidth: 1,
    borderColor: '#D4C3A3',
    borderRadius: 4,
    paddingHorizontal: 10,
    paddingVertical: 3,
  },
  headerBtnPrimaryText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#9E6D08',
  },

  // ===== TOP SPLIT ROW =====
  topSplitRow: {
    flexDirection: 'row',
    gap: 8,
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
    overflow: 'visible',
  },
  cardHeaderBanner: {
    backgroundColor: '#F5E7CA',
    paddingHorizontal: 10,
    paddingVertical: 6,
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    borderTopLeftRadius: 7,
    borderTopRightRadius: 7,
  },
  headerIconBadge: {
    width: 22,
    height: 22,
    borderRadius: 5,
    backgroundColor: '#FFF8EA',
    borderWidth: 1,
    borderColor: '#E2D2AF',
    alignItems: 'center',
    justifyContent: 'center',
    marginRight: 6,
  },
  cardHeaderTitle: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#3B2D17',
    letterSpacing: 0.3,
  },
  cardBody: {
    padding: 8,
  },
  counterBadge: {
    backgroundColor: '#FAF5EA',
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderRadius: 10,
    paddingHorizontal: 7,
    paddingVertical: 1,
    marginLeft: 8,
  },
  counterBadgeText: {
    fontSize: 10,
    fontWeight: 'bold',
    color: '#7D5E19',
  },

  // ===== FORM ROWS & INPUTS =====
  formRow: {
    flexDirection: 'row',
    alignItems: 'center',
    marginBottom: 6,
  },
  inlineLabel: {
    width: 70,
    fontSize: 11,
    fontWeight: 'bold',
    color: '#4A3B2C',
  },
  inputLabel: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#4A3B2C',
    marginBottom: 2,
  },
  input: {
    height: 24,
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderRadius: 4,
    paddingHorizontal: 6,
    paddingVertical: 1,
    fontSize: 11,
    color: '#2B2118',
  },
  readonlyInput: {
    backgroundColor: '#FAF7F0',
    borderColor: '#EADBCA',
    color: '#554736',
    fontWeight: 'bold',
  },
  searchBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FAF5EA',
    borderWidth: 1,
    borderLeftWidth: 0,
    borderColor: '#DFD3BC',
    borderTopRightRadius: 4,
    borderBottomRightRadius: 4,
    paddingHorizontal: 8,
    height: 24,
  },
  searchBtnText: {
    fontSize: 10.5,
    fontWeight: 'bold',
    color: '#7D5E19',
  },
  lookupBtn: {
    width: 24,
    height: 24,
    backgroundColor: '#FAF5EA',
    borderWidth: 1,
    borderLeftWidth: 0,
    borderColor: '#D4C3A3',
    borderTopRightRadius: 4,
    borderBottomRightRadius: 4,
    alignItems: 'center',
    justifyContent: 'center',
    flexShrink: 0,
  },

  // ===== DROPDOWN SELECTOR =====
  dropdownSelector: {
    height: 24,
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderRadius: 4,
    paddingHorizontal: 6,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
  },
  dropdownSelectorText: {
    fontSize: 12,
    color: '#2B2118',
    fontWeight: '500',
  },
  typeModalCard: {
    width: 320,
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    overflow: 'hidden',
    shadowColor: '#000',
    shadowOpacity: 0.15,
    shadowRadius: 10,
    elevation: 8,
  },
  typeModalItem: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingVertical: 10,
    paddingHorizontal: 12,
    borderRadius: 6,
    marginBottom: 4,
    borderWidth: 1,
    borderColor: '#F0E8D8',
    backgroundColor: '#FCFAF6',
  },
  typeModalItemSelected: {
    backgroundColor: '#FFF8E7',
    borderColor: '#D4C3A3',
  },
  typeIconCircle: {
    width: 26,
    height: 26,
    borderRadius: 13,
    backgroundColor: '#F3ECDA',
    alignItems: 'center',
    justifyContent: 'center',
    marginRight: 10,
  },
  typeIconCircleSelected: {
    backgroundColor: '#B48218',
  },
  typeModalItemText: {
    fontSize: 13,
    fontWeight: '500',
    color: '#3B2D17',
  },
  typeModalItemTextSelected: {
    fontWeight: 'bold',
    color: '#8A5D00',
  },

  // ===== ZERO RATED CHECKBOX =====
  zeroRatedToggle: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 6,
    paddingVertical: 2,
    borderRadius: 4,
  },
  checkboxSquare: {
    width: 14,
    height: 14,
    borderWidth: 1.5,
    borderColor: '#B8903B',
    borderRadius: 3,
    backgroundColor: '#FFFFFF',
    alignItems: 'center',
    justifyContent: 'center',
    marginRight: 4,
  },
  checkboxSquareActive: {
    backgroundColor: '#B8903B',
  },
  zeroRatedLabel: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#554428',
  },

  // ===== SUMMARY PANEL =====
  summaryBody: {
    padding: 8,
    gap: 4,
  },
  summaryRow: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
  },
  summaryLabel: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#4A3B2C',
  },
  summaryTotalRow: {
    borderTopWidth: 1,
    borderTopColor: '#ECD8AB',
    backgroundColor: '#FFF4DC',
    borderRadius: 4,
    paddingHorizontal: 8,
    paddingVertical: 4,
    marginTop: 2,
  },
  summaryTotalLabel: {
    fontSize: 11.5,
    fontWeight: 'bold',
    color: '#9E6D08',
  },
  summaryTotalValue: {
    fontSize: 13.5,
    fontWeight: 'bold',
    color: '#9E6D08',
  },

  // ===== CALENDAR MODAL =====
  modalOverlay: {
    flex: 1,
    backgroundColor: 'rgba(0,0,0,0.4)',
    justifyContent: 'center',
    alignItems: 'center',
    padding: 20,
  },
  calendarModalCard: {
    width: 320,
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    overflow: 'hidden',
    shadowColor: '#000',
    shadowOpacity: 0.15,
    shadowRadius: 10,
    elevation: 6,
  },
  modalHeader: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    paddingHorizontal: 12,
    paddingVertical: 8,
    backgroundColor: '#F5E7CA',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
  },
  modalTitle: {
    fontSize: 13,
    fontWeight: 'bold',
    color: '#3B2D17',
  },
  calendarNavRow: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    marginBottom: 12,
  },
  navArrow: {
    padding: 4,
  },
  weekdaysRow: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    marginBottom: 6,
  },
  weekdayCell: {
    width: 36,
    textAlign: 'center',
    fontSize: 11,
    fontWeight: 'bold',
    color: '#7D6F5B',
  },
  daysGrid: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    justifyContent: 'space-between',
  },
  dayCell: {
    width: 36,
    height: 32,
    justifyContent: 'center',
    alignItems: 'center',
    borderRadius: 4,
    marginVertical: 2,
  },
  dayCellSelected: {
    backgroundColor: '#B8903B',
  },
  dayCellText: {
    fontSize: 11,
    color: '#2B2118',
    fontWeight: '500',
  },
  dayCellTextSelected: {
    color: '#FFFFFF',
    fontWeight: 'bold',
  },
  emptyCell: {
    opacity: 0,
  },

  // ===== TOAST & OVERLAYS =====
  loadingOverlay: {
    ...StyleSheet.absoluteFillObject,
    backgroundColor: 'rgba(255, 255, 255, 0.6)',
    justifyContent: 'center',
    alignItems: 'center',
    zIndex: 1000,
  },
  toastContainer: {
    position: 'absolute',
    bottom: 30,
    alignSelf: 'center',
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 16,
    paddingVertical: 10,
    borderRadius: 8,
    zIndex: 9999,
    shadowColor: '#000',
    shadowOpacity: 0.15,
    shadowRadius: 8,
    elevation: 4,
  },
  toastSuccess: {
    backgroundColor: '#2E7D32',
  },
  toastError: {
    backgroundColor: '#C62828',
  },
  toastText: {
    color: '#FFFFFF',
    fontWeight: 'bold',
    fontSize: 12,
  },
});
