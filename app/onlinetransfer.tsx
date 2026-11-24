import { PrintReportModal } from '@/components/PrintReportModal';
import { ThemedText } from '@/components/themed-text';
import { API_BASE_URL } from '@/constants/api';
import { useThemeColors } from '@/hooks/useThemeColors';
import { getStandardReportCss, renderStandardReportFooter, renderStandardReportHeader } from '@/utils/standardPrintReport';
import { UNIPRO_LOGO_DATA_URI } from '@/utils/uniproLogo';
import { Ionicons } from '@expo/vector-icons';
import { Stack, useLocalSearchParams, useRouter } from 'expo-router';
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

const API_URL = `${API_BASE_URL}/onlinetransfer`;

export default function OnlineTransferScreen() {
  const themeColors = useThemeColors();
  const router = useRouter();
  const params = useLocalSearchParams();

  const [loading, setLoading] = useState(false);
  const [saving, setSaving] = useState(false);
  const [toast, setToast] = useState<{ message: string; type: 'success' | 'error' } | null>(null);

  // Form State
  const [locations, setLocations] = useState<any[]>([]);
  const [toLocation, setToLocation] = useState('');
  const [fromLocation] = useState('HQ');
  const [transferNo, setTransferNo] = useState(params.transferNo ? String(params.transferNo) : '');
  const [remarks, setRemarks] = useState('');
  const [isLocked, setIsLocked] = useState(false);

  // Location LOV modal state
  const [locationModalVisible, setLocationModalVisible] = useState(false);
  const [locationSearchQuery, setLocationSearchQuery] = useState('');

  // Past Transfer Search Modal State
  const [transferModalVisible, setTransferModalVisible] = useState(false);
  const [transferSearchQuery, setTransferSearchQuery] = useState('');
  const [pastTransfers, setPastTransfers] = useState<any[]>([]);
  const [pastTransfersLoading, setPastTransfersLoading] = useState(false);

  // Standardized Print Report Confirmation Modal State
  const [isPrintModalVisible, setIsPrintModalVisible] = useState(false);
  const [activePrintTransferNo, setActivePrintTransferNo] = useState('');
  const [activePrintIsReprint, setActivePrintIsReprint] = useState(false);

  // Date State
  const [transferDate, setTransferDate] = useState(() => {
    const d = new Date();
    const mm = String(d.getMonth() + 1).padStart(2, '0');
    const dd = String(d.getDate()).padStart(2, '0');
    const yyyy = d.getFullYear();
    return `${yyyy}-${mm}-${dd}`;
  });

  // Scanner & Entry State
  const [inventoryCode, setInventoryCode] = useState('');
  const [scannedItem, setScannedItem] = useState<any>(null);
  const [scanQty, setScanQty] = useState('1');

  // Grid State
  const [gridData, setGridData] = useState<any[]>([]);
  const [selectedRowIndex, setSelectedRowIndex] = useState<number | null>(null);

  // Totals State
  const [totals, setTotals] = useState({
    Weight: 0,
    PMC: 0,
    PCS: 0,
    SMC: 0,
    TotalCost: 0,
  });

  useEffect(() => {
    fetchLocations();
    if (params.transferNo) {
      fetchTransferDetails(String(params.transferNo));
    }
  }, []);

  useEffect(() => {
    calculateTotals();
  }, [gridData]);

  const showToast = (message: string, type: 'success' | 'error' = 'success') => {
    setToast({ message, type });
    setTimeout(() => setToast(null), 3500);
  };

  const fetchLocations = async () => {
    try {
      const res = await fetch(`${API_URL}/locations`);
      const data = await res.json();
      if (data.success) {
        setLocations(data.data || []);
      }
    } catch (err) {
      console.error('Error fetching locations', err);
    }
  };

  const fetchPastTransfers = async () => {
    try {
      setPastTransfersLoading(true);
      const res = await fetch(`${API_URL}/past-list`);
      const data = await res.json();
      if (data.success) {
        setPastTransfers(data.data || []);
      } else {
        showToast(data.message || 'Error fetching past transfers', 'error');
      }
    } catch (err) {
      showToast('Network error loading past transfers', 'error');
    } finally {
      setPastTransfersLoading(false);
    }
  };

  const openPastTransfersModal = () => {
    setTransferSearchQuery('');
    setTransferModalVisible(true);
    fetchPastTransfers();
  };

  const fetchTransferDetails = async (tNo: string) => {
    if (!tNo.trim()) return;
    try {
      setLoading(true);
      const res = await fetch(`${API_URL}/fetch?transferNo=${encodeURIComponent(tNo.trim())}`);
      const data = await res.json();

      if (data.success && data.header) {
        setTransferNo(data.header.TransferNo || tNo.trim());
        setToLocation(data.header.ToLocation || '');
        setRemarks(data.header.Remarks || '');
        if (data.header.TransferDate) {
          const rawDate = data.header.TransferDate;
          setTransferDate(new Date(rawDate).toISOString().split('T')[0]);
        }
        setGridData(data.details || []);
        setIsLocked(true);
        showToast(`Transfer details for ${tNo.trim()} loaded`, 'success');
      } else {
        showToast(data.message || 'Transfer number does not exist', 'error');
        setTransferNo('');
      }
    } catch (err: any) {
      showToast('Error loading transfer details', 'error');
    } finally {
      setLoading(false);
    }
  };

  const handleScanInventory = async () => {
    const code = inventoryCode.trim();
    if (!code) return;

    if (gridData.some((item) => String(item.InventoryCode).trim().toUpperCase() === code.toUpperCase())) {
      showToast('Item found in transfer list', 'error');
      setInventoryCode('');
      setScannedItem(null);
      return;
    }

    try {
      setLoading(true);
      const res = await fetch(`${API_URL}/inventory/${encodeURIComponent(code)}`);
      const data = await res.json();

      if (data.success && data.data) {
        const item = data.data;
        const salesType = String(item.SalesType || '').trim().toUpperCase();

        if (salesType !== 'P') {
          // Weight-based item: Auto-add to grid immediately (matching VB.NET lines 18-20)
          const newGridItem = {
            ...item,
            Qty: Number(item.QtyonHand || item.Pcs || 1),
            Pcs: Number(item.Pcs || item.QtyonHand || 1),
          };
          setGridData((prev) => [...prev, newGridItem]);
          setInventoryCode('');
          setScannedItem(null);
          showToast(`Added ${item.InventoryCode} to transfer list`, 'success');
        } else {
          // Piece-based item (SalesType === 'P'): prompt for PCS quantity
          setScannedItem(item);
          setScanQty(String(item.Pcs || item.QtyonHand || 1));
        }
      } else {
        showToast(data.message || 'Inventory Not Found ...', 'error');
        setInventoryCode('');
        setScannedItem(null);
      }
    } catch (err: any) {
      showToast('Error looking up inventory barcode', 'error');
    } finally {
      setLoading(false);
    }
  };

  const handleAddPieceItem = () => {
    if (!scannedItem) return;

    const pcsCount = Number(scanQty) || 1;
    const newItem = {
      ...scannedItem,
      Qty: pcsCount,
      Pcs: pcsCount,
      QtyonHand: pcsCount,
    };

    setGridData((prev) => [...prev, newItem]);
    setInventoryCode('');
    setScannedItem(null);
    setScanQty('1');
    showToast(`Added ${scannedItem.InventoryCode} (${pcsCount} PCS)`, 'success');
  };

  const handleLoadScrapInventory = async () => {
    if (isLocked) {
      showToast('Form is locked.', 'error');
      return;
    }
    try {
      setLoading(true);
      const res = await fetch(`${API_URL}/scrap-inventory`);
      const data = await res.json();

      if (data.success && Array.isArray(data.data) && data.data.length > 0) {
        setGridData(data.data);
        showToast(`Loaded ${data.data.length} scrap inventory items`, 'success');
      } else {
        showToast('No scrap inventory available for this location', 'error');
      }
    } catch (err) {
      showToast('Error loading scrap inventory', 'error');
    } finally {
      setLoading(false);
    }
  };

  const calculateTotals = () => {
    let weight = 0;
    let pmc = 0;
    let pcs = 0;
    let smc = 0;
    let cost = 0;

    gridData.forEach((item) => {
      weight += Number(item.Weight) || 0;
      pmc += Number(item.PMC) || 0;
      pcs += Number(item.Pcs || item.QtyonHand || item.Qty || 1);
      smc += Number(item.SMC) || 0;
      cost += Number(item.TotalCost) || 0;
    });

    setTotals({ Weight: weight, PMC: pmc, PCS: pcs, SMC: smc, TotalCost: cost });
  };

  const handleDeleteItem = (indexToDelete?: number) => {
    if (isLocked) return;
    const targetIdx = indexToDelete !== undefined ? indexToDelete : selectedRowIndex;
    if (targetIdx !== null && targetIdx !== undefined && targetIdx >= 0) {
      const removedItem = gridData[targetIdx];
      const newData = [...gridData];
      newData.splice(targetIdx, 1);
      setGridData(newData);
      setSelectedRowIndex(null);
      showToast(`Removed ${removedItem?.InventoryCode || 'item'}`, 'success');
    }
  };

  const handleClear = () => {
    const d = new Date();
    const mm = String(d.getMonth() + 1).padStart(2, '0');
    const dd = String(d.getDate()).padStart(2, '0');
    const yyyy = d.getFullYear();
    setTransferDate(`${yyyy}-${mm}-${dd}`);

    setTransferNo('');
    setToLocation('');
    setRemarks('');
    setGridData([]);
    setIsLocked(false);
    setInventoryCode('');
    setScannedItem(null);
    setScanQty('1');
    setSelectedRowIndex(null);
    showToast('Form cleared', 'success');
  };

  const handleTransfer = async () => {
    if (isLocked) {
      showToast('This transfer is already completed and locked.', 'error');
      return;
    }
    if (!toLocation) {
      showToast('Please select destination branch', 'error');
      return;
    }
    if (gridData.length === 0) {
      showToast('No Item To Transfer', 'error');
      return;
    }

    try {
      setSaving(true);
      const payload = {
        transferDate,
        fromLocation,
        toLocation,
        remarks,
        totalWeight: totals.Weight,
        totalPcs: totals.PCS,
        totalCost: totals.TotalCost,
        totalPMC: totals.PMC,
        totalSMC: totals.SMC,
        items: gridData.map((i) => ({
          ...i,
          Qty: Number(i.Pcs || i.QtyonHand || i.Qty || 1),
        })),
        userId: 'admin',
      };

      const res = await fetch(`${API_URL}/transfer`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload),
      });

      const data = await res.json();

      if (data.success) {
        setTransferNo(data.transferNo);
        setIsLocked(true);
        showToast(`Please take note reference no. ${data.transferNo}`, 'success');
        setActivePrintTransferNo(data.transferNo);
        setActivePrintIsReprint(false);
        setIsPrintModalVisible(true);
      } else {
        showToast(data.message || 'Transfer failed', 'error');
      }
    } catch (err: any) {
      showToast('Network error executing online transfer', 'error');
    } finally {
      setSaving(false);
    }
  };

  const selectedLocationObj = locations.find((l) => l.LocationCode === toLocation);

  const buildOnlineTransferHtml = (tNo: string, isReprint: boolean, printData: any) => {
    const company = printData?.company || {};
    const header = printData?.header || {};
    const details = printData?.details || [];

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

    let transferDateFormatted = header.transferDate || '';
    if (transferDateFormatted) {
      const td = new Date(transferDateFormatted);
      if (!isNaN(td.getTime())) {
        transferDateFormatted = `${String(td.getDate()).padStart(2, '0')}/${String(td.getMonth() + 1).padStart(2, '0')}/${td.getFullYear()}`;
      }
    }

    const totalGrossWeight = details.reduce((sum: number, r: any) => sum + (Number(r.grossWeight) || 0), 0);
    const totalNetWeight = details.reduce((sum: number, r: any) => sum + (Number(r.netWeight) || 0), 0);
    const totalPcsCount = details.reduce((sum: number, r: any) => sum + (Number(r.pcs) || 0), 0);
    const totalCostSum = details.reduce((sum: number, r: any) => sum + (Number(r.totalCost) || 0), 0);

    const sReportTitle = 'ONLINE TRANSFER';
    const compAddress = [company.address1, company.address2, company.city, company.state, company.country].filter(Boolean).map((s: string) => s.trim()).join(', ');

    return `
      <!DOCTYPE html>
      <html>
        <head>
          <meta charset="utf-8" />
          <title>${sReportTitle} - ${header.transferNo || tNo}</title>
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
            }
            .info-table td {
              padding: 2.5px 0;
              font-size: 11px;
              vertical-align: top;
            }
            .info-table .lbl {
              width: 115px;
              font-weight: 700;
              color: #222222;
            }
            .info-table .colon {
              width: 12px;
              font-weight: 700;
              color: #222222;
              text-align: center;
            }
            .info-table .val {
              color: #222222;
              font-weight: 700;
              word-break: break-word;
            }

            /* DETAIL ITEMS TABLE */
            .detail-table {
              width: 100%;
              border-collapse: collapse;
              margin-top: 4px;
              margin-bottom: 12px;
              font-size: 11px;
            }
            .detail-table th {
              background-color: #8C5D08;
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
              background-color: #FFFFFF;
              color: #222222;
            }
            .detail-table tr:nth-child(even) td {
              background-color: #FAF8F5;
            }

            /* TOTALS SECTION */
            .totals-section {
              display: flex;
              justify-content: flex-end;
              margin-bottom: 14px;
            }
            .totals-box {
              width: 320px;
              border: 1px solid #C49A45;
              background-color: #FFFFFF;
              border-radius: 4px;
              padding: 8px 12px;
            }
            .total-row {
              display: flex;
              justify-content: space-between;
              font-size: 11.5px;
              padding: 3px 0;
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
        name: (company?.name || 'SMART').trim(),
        address: compAddress,
        phone: company?.phone,
        fax: company?.fax,
        logoUrl: company?.logo || (company?.logoBase64 ? `data:image/png;base64,${company.logoBase64}` : '') || UNIPRO_LOGO_DATA_URI,
      },
      reportTitle: sReportTitle,
      printedDateTime: printTimestamp,
    })}

              <!-- TWO-COLUMN INFO: TRANSFER DETAILS vs DISPATCH DETAILS -->
              <div class="info-grid">
                <div class="info-card">
                  <div class="card-title">TRANSFER DETAILS</div>
                  <table class="info-table">
                    <tr>
                      <td class="lbl">Transfer No</td>
                      <td class="colon">:</td>
                      <td class="val">${header.transferNo || tNo}</td>
                    </tr>
                    <tr>
                      <td class="lbl">Transfer Date</td>
                      <td class="colon">:</td>
                      <td class="val">${transferDateFormatted || '-'}</td>
                    </tr>
                    <tr>
                      <td class="lbl">From Location</td>
                      <td class="colon">:</td>
                      <td class="val">${header.fromLocation || '-'}</td>
                    </tr>
                    <tr>
                      <td class="lbl">To Location</td>
                      <td class="colon">:</td>
                      <td class="val">${header.toLocation || '-'}</td>
                    </tr>
                  </table>
                </div>

                <div class="info-card">
                  <div class="card-title">DISPATCH & AUTHORIZATION</div>
                  <table class="info-table">
                    <tr>
                      <td class="lbl">Staff / Salesman</td>
                      <td class="colon">:</td>
                      <td class="val">${header.salesManCode || 'Staff'}</td>
                    </tr>
                    <tr>
                      <td class="lbl">Transfer Type</td>
                      <td class="colon">:</td>
                      <td class="val">ONLINE TRANSFER</td>
                    </tr>
                    <tr>
                      <td class="lbl">Remarks</td>
                      <td class="colon">:</td>
                      <td class="val">${header.remarks || '-'}</td>
                    </tr>
                  </table>
                </div>
              </div>

              <!-- DETAIL ITEMS TABLE -->
              <table class="detail-table">
                <thead>
                  <tr>
                    <th style="width: 4%; text-align: center;">#</th>
                    <th style="width: 17%;">INVENTORY CODE</th>
                    <th style="width: 25%;">DESCRIPTION</th>
                    <th style="width: 8%; text-align: center;">SIZE</th>
                    <th style="width: 9%; text-align: center;">SUPPLIER</th>
                    <th style="width: 10%; text-align: right;">GROSS WT (G)</th>
                    <th style="width: 10%; text-align: right;">NET WT (G)</th>
                    <th style="width: 5%; text-align: center;">PCS</th>
                    <th style="width: 12%; text-align: right;">COST ($)</th>
                  </tr>
                </thead>
                <tbody>
                  ${details.length > 0 ? details.map((item: any, idx: number) => `
                    <tr>
                      <td style="text-align: center; font-weight: 700;">${idx + 1}</td>
                      <td style="font-weight: 700;">${item.inventoryCode || ''}</td>
                      <td style="font-weight: 700; text-transform: uppercase;">${item.description || ''}</td>
                      <td style="text-align: center;">${item.size !== null && item.size !== undefined && item.size !== '' ? (isNaN(Number(item.size)) ? item.size : Number(item.size).toFixed(2)) : '0.00'}</td>
                      <td style="text-align: center;">${item.supplierCode || '-'}</td>
                      <td style="text-align: right; font-weight: 700;">${Number(item.grossWeight || 0).toFixed(3)}</td>
                      <td style="text-align: right; font-weight: 700;">${Number(item.netWeight || 0).toFixed(3)}</td>
                      <td style="text-align: center; font-weight: 700;">${item.pcs !== null && item.pcs !== undefined && item.pcs !== '' && Number(item.pcs) > 0 ? item.pcs : '1'}</td>
                      <td style="text-align: right; font-weight: 800; color: #8C5D08;">$ ${Number(item.totalCost || 0).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                    </tr>
                  `).join('') : `
                    <tr>
                      <td colspan="9" style="text-align: center; padding: 14px; color: #777;">
                        No detail records in this Online Transfer Voucher
                      </td>
                    </tr>
                  `}
                </tbody>
              </table>

              <!-- TOTALS SECTION -->
              <div class="totals-section">
                <div class="totals-box">
                  <div class="total-row">
                    <span>Total Gross Weight:</span>
                    <span style="font-weight: 800;">${totalGrossWeight.toFixed(3)} G</span>
                  </div>
                  <div class="total-row">
                    <span>Total Net Weight:</span>
                    <span style="font-weight: 800;">${totalNetWeight.toFixed(3)} G</span>
                  </div>
                  <div class="total-row">
                    <span>Total PCS:</span>
                    <span style="font-weight: 800;">${totalPcsCount}</span>
                  </div>
                  <div class="total-row total-row-grand">
                    <span>GRAND TOTAL COST:</span>
                    <span>$ ${totalCostSum.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</span>
                  </div>
                </div>
              </div>
            </div>

            ${renderStandardReportFooter({
      staff: {
        staffLabel: 'PREPARED BY (STAFF)',
        staffName: (header.salesManCode || 'Staff').trim(),
        receiverLabel: 'AUTHORIZED RECEIVER',
        receiverName: '-'
      }
    })}
          </div>
        </body>
      </html>
    `;
  };

  const executePrint = async (targetTransferNo?: string, isReprint: boolean = false) => {
    const tNo = (targetTransferNo || transferNo || '').trim();
    if (!tNo) {
      showToast('Please select or enter a Transfer Number to print.', 'error');
      return;
    }

    try {
      setLoading(true);
      const res = await fetch(`${API_URL}/print-data?transferNo=${encodeURIComponent(tNo)}`);
      const json = await res.json();
      if (!json.success || !json.data) {
        showToast(json.message || `Transfer record ${tNo} not found for print.`, 'error');
        return;
      }

      const htmlContent = buildOnlineTransferHtml(tNo, isReprint, json.data);
      const docTitle = isReprint ? `Online Transfer Voucher (Duplicate) - ${tNo}` : `Online Transfer Voucher - ${tNo}`;

      if (Platform.OS === 'web' && typeof window !== 'undefined') {
        const frameId = '__online_transfer_print_frame__';
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
              showToast(`Online Transfer Voucher ${tNo} print dialog opened`, 'success');
            } catch (printErr) {
              console.error('Iframe print error:', printErr);
            }
          }, 350);
        }
      } else {
        showToast('Print preview generated.', 'success');
      }
    } catch (err: any) {
      console.error('Print Error:', err);
      showToast('Error generating print: ' + (err.message || 'Error'), 'error');
    } finally {
      setLoading(false);
    }
  };

  const handleDownloadPdf = async (targetTransferNo?: string, isReprint: boolean = false) => {
    const tNo = (targetTransferNo || transferNo || '').trim();
    if (!tNo) {
      showToast('Validation Error: Transfer Number is required to download PDF', 'error');
      return;
    }

    try {
      setLoading(true);
      const res = await fetch(`${API_URL}/print-data?transferNo=${encodeURIComponent(tNo)}`);
      const json = await res.json();

      if (!json.success || !json.data) {
        showToast(json.message || `Transfer record "${tNo}" not found`, 'error');
        return;
      }

      const htmlContent = buildOnlineTransferHtml(tNo, isReprint, json.data);
      const downloadFilename = `Online_Transfer_${tNo.replace(/[^a-zA-Z0-9_-]/g, '_')}.pdf`;

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

  const handleDownloadExcel = async (targetTransferNo?: string, isReprint: boolean = false) => {
    const tNo = (targetTransferNo || transferNo || '').trim();
    if (!tNo) {
      showToast('Validation Error: Transfer Number is required to download Excel', 'error');
      return;
    }

    try {
      setLoading(true);
      const excelRes = await fetch(`${API_URL}/export-excel`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          transferNo: tNo,
          isReprint: isReprint
        })
      });

      if (!excelRes.ok) {
        const errJson = await excelRes.json().catch(() => ({}));
        throw new Error(errJson.message || 'Server error generating Excel file');
      }

      const excelBlob = await excelRes.blob();
      const downloadFilename = `Online_Transfer_${tNo.replace(/[^a-zA-Z0-9_-]/g, '_')}.xlsx`;

      if (Platform.OS === 'web' && typeof window !== 'undefined') {
        const url = URL.createObjectURL(excelBlob);
        const a = document.createElement('a');
        a.href = url;
        a.download = downloadFilename;
        document.body.appendChild(a);
        a.click();
        document.body.removeChild(a);
        setTimeout(() => URL.revokeObjectURL(url), 1000);
        showToast(`Excel downloaded successfully: ${downloadFilename}`, 'success');
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
  return (
    <SafeAreaView style={styles.safeArea} edges={['top']}>
      <Stack.Screen options={{ headerShown: false }} />

      {/* LUXURY ROYAL GOLD HEADER BAR */}
      <View style={styles.headerBar}>
        <View style={styles.headerLeft}>
          <TouchableOpacity style={styles.backBtn} onPress={() => router.back()} activeOpacity={0.7}>
            <Ionicons name="arrow-back" size={20} color="#3B2D17" />
          </TouchableOpacity>
          <ThemedText style={styles.headerTitle}>Online Transfer</ThemedText>
          {(loading || saving) && <ActivityIndicator size="small" color="#B48218" style={{ marginLeft: 8 }} />}
        </View>

        <View style={styles.headerRight}>
          {/* Clear Button */}
          <TouchableOpacity
            style={[styles.actionBtn, styles.clearBtn]}
            onPress={handleClear}
            activeOpacity={0.8}
            disabled={saving || loading}
          >
            <Ionicons name="refresh-outline" size={14} color="#5C4A28" />
            <ThemedText style={styles.clearBtnText}>Clear</ThemedText>
          </TouchableOpacity>

          {/* Print Button */}
          <TouchableOpacity
            style={[styles.actionBtn, styles.reprintBtn]}
            onPress={() => {
              const tNo = (transferNo || '').trim();
              if (!tNo) {
                showToast('Please select or enter a Transfer Number to print.', 'error');
                return;
              }
              setActivePrintTransferNo(tNo);
              setActivePrintIsReprint(isLocked);
              setIsPrintModalVisible(true);
            }}
            activeOpacity={0.8}
            disabled={saving || loading}
          >
            <Ionicons name="print-outline" size={14} color="#5C4A28" />
            <ThemedText style={styles.clearBtnText}>Print</ThemedText>
          </TouchableOpacity>

          {/* Execute Transfer Button */}
          <TouchableOpacity
            style={[styles.actionBtn, styles.saveBtn, isLocked && { opacity: 0.6 }]}
            onPress={handleTransfer}
            activeOpacity={0.8}
            disabled={saving || loading || isLocked}
          >
            {saving ? (
              <ActivityIndicator size="small" color="#9E6D08" />
            ) : (
              <>
                <Ionicons name={isLocked ? 'lock-closed-outline' : 'paper-plane-outline'} size={15} color="#9E6D08" />
                <ThemedText style={styles.saveBtnText}>
                  {isLocked ? 'Transferred' : 'Transfer'}
                </ThemedText>
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
          {/* CARD 1: ROUTING FORM */}
          <View style={styles.goldCard}>
            <View style={styles.cardBody}>
              <View style={styles.fourColumnGrid}>
                {/* Transfer No with Lookup button */}
                <View style={[styles.inlineFieldRow, { flex: 1.1, minWidth: 220 }]}>
                  <View style={styles.inputWithLookup}>
                    <TextInput
                      style={[styles.formInput, styles.inputAttached, isLocked ? styles.readonlyInput : undefined, { flex: 1 }]}
                      value={transferNo}
                      onChangeText={setTransferNo}
                      onBlur={() => transferNo && !isLocked && fetchTransferDetails(transferNo)}
                      onSubmitEditing={() => transferNo && !isLocked && fetchTransferDetails(transferNo)}
                      placeholder="Transfer No (Auto-generated)"
                      placeholderTextColor="#9E8D77"
                      editable={!isLocked}
                    />
                    <TouchableOpacity
                      style={styles.attachedLookupBtn}
                      onPress={openPastTransfersModal}
                      activeOpacity={0.7}
                      accessibilityLabel="Search Past Transfers"
                    >
                      <Ionicons name="search" size={13} color="#7D5E19" />
                    </TouchableOpacity>
                  </View>
                </View>

                {/* Transfer Date */}
                <View style={[styles.inlineFieldRow, { flex: 0.9, minWidth: 180 }]}>
                  <TextInput
                    style={[styles.formInput, isLocked ? styles.readonlyInput : undefined, { flex: 1 }]}
                    value={transferDate}
                    onChangeText={setTransferDate}
                    placeholder="YYYY-MM-DD"
                    placeholderTextColor="#9E8D77"
                    editable={!isLocked}
                  />
                </View>

                {/* From Location */}
                <View style={[styles.inlineFieldRow, { flex: 0.8, minWidth: 160 }]}>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1 }]}
                    value={`From: ${fromLocation}`}
                    editable={false}
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* To Location */}
                <View style={[styles.inlineFieldRow, { flex: 1.2, minWidth: 230 }]}>
                  <View style={styles.inputWithLookup}>
                    <TextInput
                      style={[styles.formInput, styles.inputAttached, { flex: 1 }]}
                      value={
                        selectedLocationObj
                          ? `${selectedLocationObj.LocationCode} - ${selectedLocationObj.LocationName}`
                          : toLocation
                      }
                      placeholder="Select Target Location..."
                      placeholderTextColor="#9E8D77"
                      editable={false}
                    />
                    <TouchableOpacity
                      style={styles.attachedLookupBtn}
                      onPress={() => {
                        if (!isLocked) {
                          setLocationSearchQuery('');
                          setLocationModalVisible(true);
                        }
                      }}
                      activeOpacity={0.7}
                      disabled={isLocked}
                      accessibilityLabel="Search Target Location"
                    >
                      <Ionicons name="business-outline" size={13} color="#7D5E19" />
                    </TouchableOpacity>
                  </View>
                </View>
              </View>

              {/* Remarks Full Width */}
              <View style={[styles.singleColumnRow, { marginTop: 8 }]}>
                <TextInput
                  style={[styles.formInput, isLocked ? styles.readonlyInput : undefined, { flex: 1 }]}
                  value={remarks}
                  onChangeText={setRemarks}
                  placeholder="Dispatch Remarks / Movement Notes..."
                  placeholderTextColor="#9E8D77"
                  editable={!isLocked}
                />
              </View>
            </View>
          </View>

          {/* CARD 2: CONSOLIDATED TOTALS */}
          <View style={[styles.goldCard, { marginTop: 10 }]}>
            <View style={styles.cardBody}>
              <View style={styles.fiveColumnGrid}>
                {/* Total Weight */}
                <View style={[styles.metricBox, { flex: 1, minWidth: 160 }]}>
                  <ThemedText style={styles.metricLabel}>Total Wt (g)</ThemedText>
                  <ThemedText style={styles.metricValue}>{totals.Weight.toFixed(3)}</ThemedText>
                </View>

                {/* Total PCS */}
                <View style={[styles.metricBox, { flex: 1, minWidth: 140 }]}>
                  <ThemedText style={styles.metricLabel}>Total PCS</ThemedText>
                  <ThemedText style={styles.metricValue}>{totals.PCS}</ThemedText>
                </View>

                {/* Total PMC */}
                <View style={[styles.metricBox, { flex: 1, minWidth: 160 }]}>
                  <ThemedText style={styles.metricLabel}>Total PMC ($)</ThemedText>
                  <ThemedText style={styles.metricValue}>{totals.PMC.toFixed(2)}</ThemedText>
                </View>

                {/* Total SMC */}
                <View style={[styles.metricBox, { flex: 1, minWidth: 160 }]}>
                  <ThemedText style={styles.metricLabel}>Total SMC ($)</ThemedText>
                  <ThemedText style={styles.metricValue}>{totals.SMC.toFixed(2)}</ThemedText>
                </View>

                {/* Total Cost */}
                <View style={[styles.metricBox, styles.metricBoxHighlight, { flex: 1.1, minWidth: 180 }]}>
                  <ThemedText style={styles.metricLabelHighlight}>Total Cost ($)</ThemedText>
                  <ThemedText style={styles.metricValueHighlight}>{totals.TotalCost.toFixed(2)}</ThemedText>
                </View>
              </View>
            </View>
          </View>

          {/* CARD 3: ITEMS DATA GRID */}
          <View style={[styles.goldCard, { marginTop: 10 }]}>
            {/* Table Header */}
            <View style={styles.tableHeadRow}>
              <ThemedText style={[styles.tableHeadCell, { flex: 1.6 }]}>Inventory Code</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 2.2 }]}>Description</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 1, textAlign: 'center' }]}>Class</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 1, textAlign: 'center' }]}>Category</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 1, textAlign: 'right' }]}>Weight (g)</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 0.8, textAlign: 'center' }]}>Size</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 0.7, textAlign: 'right' }]}>Qty</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 1.1, textAlign: 'right' }]}>Cost/g ($)</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 1, textAlign: 'right' }]}>PMC ($)</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 1, textAlign: 'right' }]}>SMC ($)</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 1.2, textAlign: 'right' }]}>Total ($)</ThemedText>
              {!isLocked && (
                <ThemedText style={[styles.tableHeadCell, { width: 50, textAlign: 'center' }]}>Action</ThemedText>
              )}
            </View>

            {/* Table Body */}
            {gridData.length === 0 ? (
              <View style={styles.emptyTableContainer}>
                <Ionicons name="barcode-outline" size={28} color="#D4C3A3" />
                <ThemedText style={styles.emptyTableText}>
                  No items staged for transfer. Scan or enter inventory codes below.
                </ThemedText>
              </View>
            ) : (
              gridData.map((item, index) => {
                const isSelected = selectedRowIndex === index;
                return (
                  <TouchableOpacity
                    key={`${item.InventoryCode}-${index}`}
                    style={[styles.tableRow, isSelected && styles.tableRowSelected]}
                    onPress={() => setSelectedRowIndex(index)}
                    activeOpacity={0.8}
                  >
                    <ThemedText style={[styles.tableCell, styles.codeCell, { flex: 1.6 }]}>
                      {item.InventoryCode}
                    </ThemedText>
                    <ThemedText style={[styles.tableCell, { flex: 2.2 }]} numberOfLines={1}>
                      {item.Description}
                    </ThemedText>
                    <ThemedText style={[styles.tableCell, { flex: 1, textAlign: 'center' }]}>
                      {item.ClassCode || '-'}
                    </ThemedText>
                    <ThemedText style={[styles.tableCell, { flex: 1, textAlign: 'center' }]}>
                      {item.CategoryCode || '-'}
                    </ThemedText>
                    <ThemedText style={[styles.tableCell, { flex: 1, textAlign: 'right', fontWeight: 'bold' }]}>
                      {Number(item.Weight || 0).toFixed(3)}
                    </ThemedText>
                    <ThemedText style={[styles.tableCell, { flex: 0.8, textAlign: 'center' }]}>
                      {item.SizeCode || '-'}
                    </ThemedText>
                    <ThemedText style={[styles.tableCell, { flex: 0.7, textAlign: 'right' }]}>
                      {item.Pcs || item.QtyonHand || item.Qty || 1}
                    </ThemedText>
                    <ThemedText style={[styles.tableCell, { flex: 1.1, textAlign: 'right' }]}>
                      {Number(item.CostPerGram || 0).toFixed(2)}
                    </ThemedText>
                    <ThemedText style={[styles.tableCell, { flex: 1, textAlign: 'right', color: '#7A6855' }]}>
                      {Number(item.PMC || 0).toFixed(2)}
                    </ThemedText>
                    <ThemedText style={[styles.tableCell, { flex: 1, textAlign: 'right', color: '#7A6855' }]}>
                      {Number(item.SMC || 0).toFixed(2)}
                    </ThemedText>
                    <ThemedText style={[styles.tableCell, styles.amountCell, { flex: 1.2, textAlign: 'right' }]}>
                      {Number(item.TotalCost || 0).toFixed(2)}
                    </ThemedText>
                    {!isLocked && (
                      <View style={{ width: 50, alignItems: 'center' }}>
                        <TouchableOpacity
                          style={styles.deleteRowBtn}
                          onPress={() => handleDeleteItem(index)}
                          activeOpacity={0.7}
                          accessibilityLabel="Delete item"
                        >
                          <Ionicons name="trash-outline" size={14} color="#C62828" />
                        </TouchableOpacity>
                      </View>
                    )}
                  </TouchableOpacity>
                );
              })
            )}

            {/* SCANNER SUBFORM & SCRAP INVENTORY ACTION BAR */}
            {!isLocked && (
              <View style={styles.itemEntryBar}>
                <View style={styles.itemEntryRow}>
                  {/* Scan Input */}
                  <View style={[styles.scannerInputBox, { flex: 1.8, minWidth: 190 }]}>
                    <TextInput
                      style={styles.formInput}
                      value={inventoryCode}
                      onChangeText={setInventoryCode}
                      onBlur={handleScanInventory}
                      onSubmitEditing={handleScanInventory}
                      placeholder="Scan Barcode / Code..."
                      placeholderTextColor="#9E8D77"
                    />
                  </View>

                  {/* Scanned Description */}
                  <View style={[styles.scannerInputBox, { flex: 2.2, minWidth: 200 }]}>
                    <TextInput
                      style={[styles.formInput, styles.readonlyInput]}
                      value={scannedItem?.Description || ''}
                      editable={false}
                      placeholder="Description"
                      placeholderTextColor="#9E8D77"
                    />
                  </View>

                  {/* Scanned Weight */}
                  <View style={[styles.scannerInputBox, { flex: 0.9, minWidth: 110 }]}>
                    <TextInput
                      style={[styles.formInput, styles.readonlyInput, { textAlign: 'right' }]}
                      value={scannedItem?.Weight ? Number(scannedItem.Weight).toFixed(3) : ''}
                      editable={false}
                      placeholder="0.000 Wt"
                      placeholderTextColor="#9E8D77"
                    />
                  </View>

                  {/* Piece count if SalesType === 'P' */}
                  {scannedItem && String(scannedItem.SalesType || '').toUpperCase() === 'P' && (
                    <View style={[styles.scannerInputBox, { flex: 0.8, minWidth: 90 }]}>
                      <TextInput
                        style={[styles.formInput, { textAlign: 'right' }]}
                        value={scanQty}
                        onChangeText={setScanQty}
                        keyboardType="numeric"
                        placeholder="PCS"
                        placeholderTextColor="#9E8D77"
                      />
                    </View>
                  )}

                  {/* Scanned Total Cost */}
                  <View style={[styles.scannerInputBox, { flex: 1, minWidth: 130 }]}>
                    <TextInput
                      style={[styles.formInput, styles.readonlyInput, styles.highlightTotal, { textAlign: 'right' }]}
                      value={scannedItem?.TotalCost ? Number(scannedItem.TotalCost).toFixed(2) : ''}
                      editable={false}
                      placeholder="0.00 Cost"
                      placeholderTextColor="#9E8D77"
                    />
                  </View>

                  {/* Add Button for piece item */}
                  {scannedItem && (
                    <TouchableOpacity
                      style={[styles.tableActionBtn, styles.addBtn, { height: 28, paddingHorizontal: 12 }]}
                      onPress={handleAddPieceItem}
                      activeOpacity={0.8}
                    >
                      <Ionicons name="add-circle-outline" size={14} color="#9E6D08" style={{ marginRight: 4 }} />
                      <ThemedText style={styles.addBtnText}>Add</ThemedText>
                    </TouchableOpacity>
                  )}

                  {/* Load Scrap Inventory Button (matching subAddGridScrapInventory) */}
                  <TouchableOpacity
                    style={[styles.tableActionBtn, styles.scrapBtn, { height: 28, paddingHorizontal: 10 }]}
                    onPress={handleLoadScrapInventory}
                    activeOpacity={0.8}
                  >
                    <Ionicons name="layers-outline" size={13} color="#6D4C10" style={{ marginRight: 4 }} />
                    <ThemedText style={styles.scrapBtnText}>Scrap Stock</ThemedText>
                  </TouchableOpacity>
                </View>
              </View>
            )}
          </View>
        </ScrollView>
      </KeyboardAvoidingView>

      {/* LOCATION LOOKUP MODAL */}
      <Modal
        visible={locationModalVisible}
        transparent
        animationType="fade"
        onRequestClose={() => setLocationModalVisible(false)}
      >
        <View style={styles.modalOverlay}>
          <View style={styles.lovModalCard}>
            <View style={styles.modalHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="business-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.modalHeaderTitle}>Select Target Branch</ThemedText>
              </View>
              <TouchableOpacity
                onPress={() => setLocationModalVisible(false)}
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
                value={locationSearchQuery}
                onChangeText={setLocationSearchQuery}
                placeholder="Search branches..."
                placeholderTextColor="#9E8D77"
                autoFocus
              />
              {locationSearchQuery.length > 0 && (
                <TouchableOpacity onPress={() => setLocationSearchQuery('')}>
                  <Ionicons name="close-circle" size={16} color="#9E8D77" />
                </TouchableOpacity>
              )}
            </View>

            <View style={styles.lovHeadRow}>
              <ThemedText style={[styles.lovHeadText, { flex: 1.4 }]}>Location Code</ThemedText>
              <ThemedText style={[styles.lovHeadText, { flex: 2.2 }]}>Branch Name</ThemedText>
            </View>

            <FlatList
              data={locations.filter(
                (l) =>
                  (l.LocationCode || '').toLowerCase().includes(locationSearchQuery.toLowerCase()) ||
                  (l.LocationName || '').toLowerCase().includes(locationSearchQuery.toLowerCase())
              )}
              keyExtractor={(item) => item.LocationCode}
              renderItem={({ item }) => (
                <TouchableOpacity
                  style={styles.lovItemRow}
                  onPress={() => {
                    setToLocation(item.LocationCode);
                    setLocationModalVisible(false);
                  }}
                  activeOpacity={0.7}
                >
                  <ThemedText style={[styles.lovItemText, styles.codeCell, { flex: 1.4 }]}>
                    {item.LocationCode}
                  </ThemedText>
                  <ThemedText style={[styles.lovItemText, { flex: 2.2 }]}>{item.LocationName}</ThemedText>
                </TouchableOpacity>
              )}
            />
          </View>
        </View>
      </Modal>

      {/* PAST TRANSFERS LOOKUP MODAL */}
      <Modal
        visible={transferModalVisible}
        transparent
        animationType="fade"
        onRequestClose={() => setTransferModalVisible(false)}
      >
        <View style={styles.modalOverlay}>
          <View style={styles.lovModalCard}>
            <View style={styles.modalHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="receipt-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.modalHeaderTitle}>Search Online Transfers</ThemedText>
              </View>
              <TouchableOpacity
                onPress={() => setTransferModalVisible(false)}
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
                value={transferSearchQuery}
                onChangeText={setTransferSearchQuery}
                placeholder="Search by Transfer No or Branch..."
                placeholderTextColor="#9E8D77"
                autoFocus
              />
              {transferSearchQuery.length > 0 && (
                <TouchableOpacity onPress={() => setTransferSearchQuery('')}>
                  <Ionicons name="close-circle" size={16} color="#9E8D77" />
                </TouchableOpacity>
              )}
            </View>

            <View style={styles.lovHeadRow}>
              <ThemedText style={[styles.lovHeadText, { flex: 1.6 }]}>Transfer No</ThemedText>
              <ThemedText style={[styles.lovHeadText, { flex: 1.2 }]}>Date</ThemedText>
              <ThemedText style={[styles.lovHeadText, { flex: 1.1 }]}>To Branch</ThemedText>
              <ThemedText style={[styles.lovHeadText, { flex: 1, textAlign: 'right' }]}>Weight (g)</ThemedText>
              <ThemedText style={[styles.lovHeadText, { flex: 0.8, textAlign: 'right' }]}>PCS</ThemedText>
            </View>

            {pastTransfersLoading ? (
              <View style={{ padding: 24, alignItems: 'center' }}>
                <ActivityIndicator size="small" color="#B48218" />
                <ThemedText style={{ fontSize: 12, color: '#9E8D77', marginTop: 6 }}>
                  Loading transfer vouchers...
                </ThemedText>
              </View>
            ) : (
              <FlatList
                data={pastTransfers.filter(
                  (t) =>
                    (t.TransferNo || '').toLowerCase().includes(transferSearchQuery.toLowerCase()) ||
                    (t.ToLocation || '').toLowerCase().includes(transferSearchQuery.toLowerCase())
                )}
                keyExtractor={(item) => item.TransferNo}
                renderItem={({ item }) => (
                  <TouchableOpacity
                    style={styles.lovItemRow}
                    onPress={() => {
                      setTransferModalVisible(false);
                      fetchTransferDetails(item.TransferNo);
                    }}
                    activeOpacity={0.7}
                  >
                    <ThemedText style={[styles.lovItemText, styles.codeCell, { flex: 1.6 }]}>
                      {item.TransferNo}
                    </ThemedText>
                    <ThemedText style={[styles.lovItemText, { flex: 1.2 }]}>
                      {item.TransferDate ? String(item.TransferDate).substring(0, 10) : '-'}
                    </ThemedText>
                    <ThemedText style={[styles.lovItemText, { flex: 1.1 }]}>
                      {item.ToLocation || '-'}
                    </ThemedText>
                    <ThemedText style={[styles.lovItemText, { flex: 1, textAlign: 'right' }]}>
                      {Number(item.TotalWeight || 0).toFixed(3)}
                    </ThemedText>
                    <ThemedText style={[styles.lovItemText, { flex: 0.8, textAlign: 'right' }]}>
                      {item.TotalPCS || 0}
                    </ThemedText>
                  </TouchableOpacity>
                )}
              />
            )}
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
          setActivePrintTransferNo('');
        }}
        onPdf={() => {
          const tNo = activePrintTransferNo;
          const reprint = activePrintIsReprint;
          setIsPrintModalVisible(false);
          setActivePrintTransferNo('');
          if (tNo) {
            handleDownloadPdf(tNo, reprint);
          }
        }}
        onExcel={() => {
          const tNo = activePrintTransferNo;
          const reprint = activePrintIsReprint;
          setIsPrintModalVisible(false);
          setActivePrintTransferNo('');
          if (tNo) {
            handleDownloadExcel(tNo, reprint);
          }
        }}
        onPrint={() => {
          const tNo = activePrintTransferNo;
          const reprint = activePrintIsReprint;
          setIsPrintModalVisible(false);
          setActivePrintTransferNo('');
          if (tNo) {
            executePrint(tNo, reprint);
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

  // ===== CARDS =====
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
  cardBody: {
    padding: 10,
  },

  // ===== GRIDS & INPUTS =====
  fourColumnGrid: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    gap: 10,
  },
  fiveColumnGrid: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    gap: 10,
  },
  singleColumnRow: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  inlineFieldRow: {
    flex: 1,
    flexDirection: 'row',
    alignItems: 'center',
  },
  formInput: {
    height: 28,
    width: '100%',
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#D4C3A3',
    borderRadius: 4,
    paddingHorizontal: 8,
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

  // Metric Boxes for clean aesthetic
  metricBox: {
    backgroundColor: '#FAF7F0',
    borderWidth: 1,
    borderColor: '#E8DCB8',
    borderRadius: 6,
    paddingVertical: 6,
    paddingHorizontal: 10,
    alignItems: 'center',
    justifyContent: 'center',
  },
  metricBoxHighlight: {
    backgroundColor: '#FFF4DA',
    borderColor: '#B8903B',
  },
  metricLabel: {
    fontSize: 10,
    fontWeight: '600',
    color: '#7D5E19',
    marginBottom: 2,
  },
  metricLabelHighlight: {
    fontSize: 10,
    fontWeight: 'bold',
    color: '#8A5D00',
    marginBottom: 2,
  },
  metricValue: {
    fontSize: 13,
    fontWeight: 'bold',
    color: '#3B2D17',
  },
  metricValueHighlight: {
    fontSize: 14,
    fontWeight: 'bold',
    color: '#8A5D00',
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
    height: 28,
    width: 28,
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

  // ===== TABLE SECTION =====
  tableHeadRow: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FAF5EA',
    borderBottomWidth: 1,
    borderBottomColor: '#E8DCB8',
    paddingHorizontal: 12,
    paddingVertical: 6,
  },
  tableHeadCell: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#5C4A28',
    textTransform: 'uppercase',
    letterSpacing: 0.3,
  },
  tableRow: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 12,
    paddingVertical: 6,
    borderBottomWidth: 1,
    borderBottomColor: '#F0E7D5',
    backgroundColor: '#FFFFFF',
  },
  tableRowSelected: {
    backgroundColor: '#FFF8E7',
  },
  tableCell: {
    fontSize: 11,
    color: '#3B2D17',
  },
  codeCell: {
    fontWeight: 'bold',
    color: '#8D6409',
  },
  amountCell: {
    fontWeight: 'bold',
    color: '#8A5D00',
  },
  deleteRowBtn: {
    padding: 3,
  },
  emptyTableContainer: {
    paddingVertical: 28,
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
    alignItems: 'center',
    flexWrap: 'wrap',
    gap: 10,
  },
  scannerInputBox: {
    minWidth: 0,
  },
  tableActionBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 10,
    paddingVertical: 4,
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
  scrapBtn: {
    backgroundColor: '#F5EFE6',
    borderColor: '#D4C3A3',
  },
  scrapBtnText: {
    fontSize: 11,
    fontWeight: '600',
    color: '#6D4C10',
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
    maxWidth: 620,
    maxHeight: '80%',
    backgroundColor: '#FFFFFF',
    borderRadius: 10,
    overflow: 'hidden',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.15,
    shadowRadius: 10,
    elevation: 8,
  },
  modalHeaderBanner: {
    backgroundColor: '#F5E7CA',
    paddingHorizontal: 14,
    paddingVertical: 10,
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    borderBottomWidth: 1,
    borderBottomColor: '#E8DCB8',
  },
  headerTitleContainer: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  headerIconBadgeSmall: {
    width: 22,
    height: 22,
    borderRadius: 4,
    backgroundColor: '#FFFFFF',
    justifyContent: 'center',
    alignItems: 'center',
    marginRight: 6,
    borderWidth: 1,
    borderColor: '#E8DCB8',
  },
  modalHeaderTitle: {
    fontSize: 13,
    fontWeight: 'bold',
    color: '#3B2D17',
  },
  closeModalBtn: {
    padding: 4,
  },
  modalSearchBox: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FCFAF6',
    borderBottomWidth: 1,
    borderBottomColor: '#E8DCB8',
    paddingHorizontal: 12,
    paddingVertical: 6,
  },
  modalSearchInput: {
    flex: 1,
    height: 30,
    fontSize: 12,
    color: '#3B2D17',
  },
  lovHeadRow: {
    flexDirection: 'row',
    backgroundColor: '#FAF5EA',
    paddingHorizontal: 14,
    paddingVertical: 6,
    borderBottomWidth: 1,
    borderBottomColor: '#E8DCB8',
  },
  lovHeadText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#7D5E19',
    textTransform: 'uppercase',
  },
  lovItemRow: {
    flexDirection: 'row',
    paddingHorizontal: 14,
    paddingVertical: 8,
    borderBottomWidth: 1,
    borderBottomColor: '#F5EFE6',
  },
  lovItemText: {
    fontSize: 12,
    color: '#3B2D17',
  },

  // Toasts
  toast: {
    position: 'absolute',
    top: 54,
    alignSelf: 'center',
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 16,
    paddingVertical: 8,
    borderRadius: 6,
    elevation: 4,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.15,
    shadowRadius: 4,
    zIndex: 9999,
  },
  toastSuccess: {
    backgroundColor: '#2E7D32',
  },
  toastError: {
    backgroundColor: '#C62828',
  },
  toastText: {
    color: '#FFFFFF',
    fontSize: 12,
    fontWeight: '600',
  },
});
