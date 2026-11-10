import { ThemedText } from '@/components/themed-text';
import { API_BASE_URL } from '@/constants/api';
import { useThemeColors } from '@/hooks/useThemeColors';
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

const API_URL = `${API_BASE_URL}/stocktradeintransfer`;

export default function StockTradeInTransferScreen() {
  const themeColors = useThemeColors();
  const router = useRouter();
  const params = useLocalSearchParams();

  const [loading, setLoading] = useState(false);
  const [saving, setSaving] = useState(false);
  const [toast, setToast] = useState<{ message: string; type: 'success' | 'error' } | null>(null);

  // Salesman Password State
  const [showPasswordModal, setShowPasswordModal] = useState(false);
  const [salesManPassword, setSalesManPassword] = useState('');

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

  // Date State
  const [transferDate, setTransferDate] = useState(() => {
    const d = new Date();
    const mm = String(d.getMonth() + 1).padStart(2, '0');
    const dd = String(d.getDate()).padStart(2, '0');
    const yyyy = d.getFullYear();
    return `${yyyy}-${mm}-${dd}`;
  });

  // Scanner & Item Entry State
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
        setPastTransfers(data.data || data.list || []);
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

    if (!toLocation) {
      showToast('Select the ToLocation Code', 'error');
      return;
    }

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
          // Weight-based item: Auto-add to grid immediately (matching VB lines 27-29)
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

  const handleTransfer = () => {
    if (isLocked) {
      showToast('This transfer is locked and completed.', 'error');
      return;
    }
    if (!toLocation) {
      showToast('Select the ToLocation Code', 'error');
      return;
    }
    if (gridData.length === 0) {
      showToast('No Item To Transfer', 'error');
      return;
    }
    setShowPasswordModal(true);
  };

  const verifyPasswordAndTransfer = async () => {
    if (!salesManPassword.trim()) {
      showToast('Please enter salesman password', 'error');
      return;
    }
    try {
      setLoading(true);
      const res = await fetch(`${API_URL}/verifypassword`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ password: salesManPassword.trim() }),
      });
      const data = await res.json();
      if (data.success) {
        setShowPasswordModal(false);
        setSalesManPassword('');
        await processTransfer(data.salesman?.SalesManCode || 'admin');
      } else {
        showToast(data.message || 'SalesMan Password is Incorrect!', 'error');
        setSalesManPassword('');
      }
    } catch (err: any) {
      showToast('Error verifying salesman authorization', 'error');
    } finally {
      setLoading(false);
    }
  };

  const processTransfer = async (salesManCode: string) => {
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
        salesManCode,
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
        // Automatically prompt to print matching VB.NET subPrintTransferOut:
        // If MsgBox("Do you want to print ?", vbYesNo + MsgBoxStyle.Information) = MsgBoxResult.Yes
        setTimeout(() => {
          handlePrint(data.transferNo);
        }, 500);
      } else {
        showToast(data.message || 'Transfer failed', 'error');
      }
    } catch (err: any) {
      showToast('Network error executing trade-in transfer', 'error');
    } finally {
      setSaving(false);
    }
  };

  const selectedLocationObj = locations.find((l) => l.LocationCode === toLocation);

  const handlePrint = async (targetTransferNo?: string) => {
    const tNo = (targetTransferNo || transferNo || '').trim();
    if (!tNo) {
      showToast('Please select or enter a Transfer Number to print.', 'error');
      return;
    }

    try {
      setLoading(true);

      let company: any = { name: '', address1: '', address2: '', city: '', state: '', country: '', phone: '', fax: '', logo: '' };
      let header: any = null;
      let details: any[] = [];

      // 1. Attempt to fetch dedicated print-data from backend
      let fetchSuccess = false;
      try {
        const res = await fetch(`${API_URL}/print-data?transferNo=${encodeURIComponent(tNo)}`);
        const cType = res.headers.get('content-type') || '';
        if (res.ok && cType.includes('application/json')) {
          const json = await res.json();
          if (json.success && json.data) {
            company = json.data.company || company;
            header = json.data.header;
            details = json.data.details || [];
            fetchSuccess = true;
          }
        }
      } catch (eDedicated) {
        // Fallback below
      }

      // 2. If dedicated route is not available (e.g. deployed backend not yet updated),
      // query the active endpoints: /api/stocktradeintransfer/fetch and /api/company
      if (!fetchSuccess) {
        const [fetchRes, compRes] = await Promise.all([
          fetch(`${API_URL}/fetch?transferNo=${encodeURIComponent(tNo)}`).then(r => {
            const ct = r.headers.get('content-type') || '';
            return ct.includes('application/json') ? r.json() : null;
          }).catch(() => null),
          fetch(`${API_BASE_URL}/company`).then(r => {
            const ct = r.headers.get('content-type') || '';
            return ct.includes('application/json') ? r.json() : null;
          }).catch(() => null),
        ]);

        if (compRes?.success && Array.isArray(compRes.data) && compRes.data.length > 0) {
          const c = compRes.data[0];
          company = {
            name: (c.Name || c.CompanyName || '').trim(),
            address1: (c.Address1 || '').trim(),
            address2: (c.Address2 || '').trim(),
            city: (c.City || '').trim(),
            state: (c.State || '').trim(),
            country: (c.Country || '').trim(),
            phone: (c.Phone || '').trim(),
            fax: (c.Fax || '').trim(),
            logo: '',
          };
        }

        if (fetchRes?.success && fetchRes?.header) {
          const h = fetchRes.header;
          header = {
            transferNo: String(h.TransferNo || tNo).trim(),
            transferDate: h.TransferDate,
            fromLocation: String(h.FromLocation || '').trim(),
            toLocation: String(h.ToLocation || '').trim(),
            remarks: String(h.Remarks || '').trim(),
            totalWeight: parseFloat(h.TotalWeight || 0),
            totalPcs: parseInt(h.TotalPcs || 0, 10),
            totalCost: parseFloat(h.TotalCost || 0),
          };

          const rawDetails = fetchRes.details || [];
          // Retrieve supplier codes if missing
          const mappedDetails = await Promise.all(
            rawDetails.map(async (row: any) => {
              let sCode = (row.SupplierCode || '').trim();
              if (!sCode && row.InventoryCode) {
                try {
                  const invRes = await fetch(
                    `${API_BASE_URL}/inventorymaintenance/lov/inventory?search=${encodeURIComponent(row.InventoryCode.trim())}`
                  );
                  const invJson = await invRes.json();
                  if (invJson?.success && Array.isArray(invJson.data)) {
                    const match = invJson.data.find(
                      (x: any) => (x.InventoryCode || '').trim() === row.InventoryCode.trim()
                    );
                    if (match && match.SupplierCode) {
                      sCode = match.SupplierCode.trim();
                    }
                  }
                } catch (eLov) {}
              }

              const w = parseFloat(row.Weight || 0);
              const sz = parseFloat(row.SizeCode || 0);
              const rawP = row.Pcs !== undefined && row.Pcs !== null ? row.Pcs : row.QtyOnHand;
              return {
                inventoryCode: String(row.InventoryCode || '').trim(),
                description: String(row.Description || '').trim(),
                size: sz.toFixed(2),
                supplierCode: sCode,
                grossWeight: w,
                netWeight: w,
                pcs: rawP !== undefined && rawP !== null && rawP !== '' ? rawP : '',
                totalCost: parseFloat(row.TotalCost || 0),
              };
            })
          );
          details = mappedDetails;
          fetchSuccess = true;
        }
      }

      if (!fetchSuccess || !header) {
        showToast('Transfer record not found for Transfer No: ' + tNo, 'error');
        setLoading(false);
        return;
      }

      // Singapore Print Date & Time (Asia/Singapore) matching other Smart Gold reports
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

      // Transfer Date formatting
      let transferDateFormatted = header.transferDate || '';
      if (transferDateFormatted) {
        const td = new Date(transferDateFormatted);
        if (!isNaN(td.getTime())) {
          transferDateFormatted = `${String(td.getDate()).padStart(2, '0')}/${String(td.getMonth() + 1).padStart(2, '0')}/${td.getFullYear()}`;
        }
      }

      // Compute Totals directly from details if available
      const totalGrossWeight = details.reduce((sum: number, r: any) => sum + (Number(r.grossWeight) || 0), 0);
      const totalNetWeight = details.reduce((sum: number, r: any) => sum + (Number(r.netWeight) || 0), 0);
      const totalPcsCount = details.reduce((sum: number, r: any) => sum + (Number(r.pcs) || 0), 0);
      const totalCostSum = details.reduce((sum: number, r: any) => sum + (Number(r.totalCost) || 0), 0);

      const rowsHtml = details
        .map(
          (item: any) => `
          <tr>
            <td style="text-align: center; font-weight: 700;">${item.inventoryCode || ''}</td>
            <td style="text-align: left; font-weight: 600;">${item.description || ''}</td>
            <td style="text-align: center;">${item.size !== null && item.size !== undefined && item.size !== '' ? (isNaN(Number(item.size)) ? item.size : Number(item.size).toFixed(2)) : '0.00'}</td>
            <td style="text-align: center;">${item.supplierCode || ''}</td>
            <td style="text-align: right;">${Number(item.grossWeight || 0).toFixed(2)}</td>
            <td style="text-align: right;">${Number(item.netWeight || 0).toFixed(2)}</td>
            <td style="text-align: center;">${item.pcs !== null && item.pcs !== undefined && item.pcs !== '' && Number(item.pcs) > 0 ? item.pcs : ''}</td>
            <td style="text-align: right;">$ ${Number(item.totalCost || 0).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
          </tr>`
        )
        .join('');

      const companyName = (company.name || '').trim();
      const compAddress = [company.address1, company.address2, company.city, company.state, company.country].filter(Boolean).join(', ');
      const compPhone = (company.phone || '').trim();
      const companyLogo = company.logo || (company.logoBase64 ? `data:image/png;base64,${company.logoBase64}` : '') || UNIPRO_LOGO_DATA_URI;

      const printHtml = `
        <!DOCTYPE html>
        <html>
          <head>
            <meta charset="utf-8" />
            <title>Transfer Note - ${header.transferNo}</title>
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
                font-size: 26px;
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
              }
              .header-right {
                width: 140px;
                text-align: right;
                font-size: 11px;
                color: #705832;
                font-weight: 600;
              }
              .header-right table {
                float: right;
                font-size: 10.5px;
                border-collapse: collapse;
              }
              .header-right td {
                padding: 1px 3px;
              }
              .header-ornament {
                text-align: center;
                margin: 6px 0 10px 0;
                color: #d4a742;
                font-size: 14px;
                position: relative;
              }
              .header-ornament::before,
              .header-ornament::after {
                content: "";
                position: absolute;
                top: 50%;
                width: calc(50% - 18px);
                height: 1.5px;
                background: #d4a742;
              }
              .header-ornament::before { left: 0; }
              .header-ornament::after { right: 0; }

              /* REPORT TITLE & TRANSFER INFO */
              .report-title-area {
                text-align: center;
                margin-bottom: 12px;
              }
              .report-main-title {
                font-family: Cambria, Georgia, "Times New Roman", serif !important;
                font-size: 20px;
                font-weight: 700;
                color: #3b2d17;
                letter-spacing: 1px;
                text-transform: uppercase;
                margin-bottom: 4px;
              }
              .report-sub-info {
                font-family: Cambria, Georgia, "Times New Roman", serif !important;
                font-size: 11.5px;
                color: #705832;
                font-weight: 600;
                margin-bottom: 2px;
              }
              .report-period-line {
                font-family: Cambria, Georgia, "Times New Roman", serif !important;
                font-size: 11.5px;
                color: #705832;
                font-weight: 600;
                margin-top: 3px;
              }

              /* TRANSFER META INFO BAR */
              .transfer-meta-card {
                display: flex;
                justify-content: space-between;
                background: #fffbf2;
                border: 1px solid #d4a742;
                border-radius: 6px;
                padding: 8px 14px;
                margin-bottom: 14px;
                font-size: 11.5px;
              }
              .meta-item {
                display: flex;
                flex-direction: column;
              }
              .meta-label {
                font-size: 10px;
                font-weight: 700;
                color: #705832;
                text-transform: uppercase;
                letter-spacing: 0.5px;
                margin-bottom: 2px;
              }
              .meta-val {
                font-size: 12.5px;
                font-weight: 700;
                color: #996515;
              }

              /* DETAIL TABLE MATCHING SMART GOLD TEMPLATE */
              .table-container {
                width: 100%;
                overflow: hidden;
                margin-bottom: 10px;
              }
              table.report-table {
                width: 100% !important;
                border-collapse: collapse;
                font-family: Cambria, Georgia, "Times New Roman", serif !important;
                font-size: 11px;
                border: 0.75px solid #d4a742;
              }
              table.report-table thead th {
                background-color: #996515 !important;
                color: #ffffff !important;
                font-weight: 700 !important;
                font-family: Cambria, Georgia, "Times New Roman", serif !important;
                padding: 7px 6px;
                letter-spacing: 0.4px;
                border: 0.75px solid #d4a742;
                font-size: 11px;
                line-height: 1.2;
                text-align: center !important;
                text-transform: uppercase !important;
              }
              table.report-table tbody td {
                padding: 6px 6px;
                color: #3b2d17;
                font-family: Cambria, Georgia, "Times New Roman", serif !important;
                border: 0.75px solid #f0d9a0;
                font-size: 11px;
                line-height: 1.25;
              }
              table.report-table tbody tr:nth-child(even) td {
                background-color: #fffbf2 !important;
              }
              table.report-table tbody tr:nth-child(odd) td {
                background-color: #ffffff !important;
              }
              table.report-table tr.item-total-row td,
              table.report-table tr.grand-total-row td {
                background-color: #fff8e7 !important;
                font-weight: 700 !important;
                font-family: Cambria, Georgia, "Times New Roman", serif !important;
                color: #996515 !important;
                border: 0.75px solid #d4a742 !important;
                border-top: 1.5px solid #d4a742 !important;
                border-bottom: 1.5px solid #d4a742 !important;
                padding: 7px 6px;
                font-size: 11px;
              }

              /* REMARKS */
              .remarks-box {
                margin-top: 12px;
                font-size: 11.5px;
                font-weight: 700;
                color: #4a3b22;
                padding: 6px 10px;
                background-color: #fffdf8;
                border-left: 3px solid #996515;
              }

              /* SIGNATURE SECTION */
              .signature-section {
                margin-top: 30px;
                width: 100%;
                display: flex;
                justify-content: space-between;
                font-size: 11px;
                page-break-inside: avoid;
                break-inside: avoid;
              }
              .sign-card {
                width: 31%;
                border: 1px solid #d4a742;
                border-radius: 6px;
                background-color: #ffffff;
                padding: 10px 12px;
                box-sizing: border-box;
              }
              .sign-header {
                font-size: 11.5px;
                font-weight: 700;
                color: #996515;
                text-transform: uppercase;
                margin-bottom: 10px;
                border-bottom: 1px dashed #d4a742;
                padding-bottom: 4px;
              }
              .sign-row {
                margin-bottom: 8px;
                color: #4a3b22;
                font-size: 11px;
                white-space: nowrap;
              }

              /* FOOTER */
              .report-footer {
                margin-top: auto !important;
                text-align: center;
                padding-top: 14px;
                padding-bottom: 2mm;
                page-break-inside: avoid;
                break-inside: avoid;
              }
              .footer-divider {
                width: 100%;
                height: 1.5px;
                background: #d4a742;
                margin-bottom: 10px;
              }
              .footer-gen-text {
                font-size: 11px;
                color: #705832;
                font-weight: 600;
                margin-bottom: 3px;
              }
              .footer-powered {
                font-size: 12px;
                color: #996515;
                font-weight: 700;
              }

              /* TOOLBAR */
              .print-toolbar {
                position: fixed;
                top: 16px;
                right: 20px;
                z-index: 1000;
              }
              .btn-action {
                background: #996515;
                color: #fff;
                border: none;
                padding: 9px 20px;
                font-family: Cambria, Georgia, serif;
                font-size: 12px;
                font-weight: 700;
                border-radius: 6px;
                cursor: pointer;
                box-shadow: 0 4px 12px rgba(153, 101, 21, 0.25);
              }
              .btn-action:hover {
                background: #7a5408;
              }

              @media print {
                @page {
                  margin: 0 !important;
                  size: A4 portrait;
                }
                html, body {
                  background: #fff !important;
                  padding: 0 !important;
                  margin: 0 !important;
                  height: 100% !important;
                }
                .print-toolbar {
                  display: none !important;
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
                }
                table.report-table {
                  page-break-inside: auto;
                }
                tr {
                  page-break-inside: avoid;
                  break-inside: avoid;
                }
                thead {
                  display: table-header-group;
                }
                tfoot {
                  display: table-footer-group;
                }
                .signature-section {
                  page-break-inside: avoid;
                  break-inside: avoid;
                }
                .report-footer {
                  margin-top: auto !important;
                  page-break-inside: avoid !important;
                  break-inside: avoid !important;
                  padding-bottom: 4mm !important;
                }
              }
            </style>
          </head>
          <body>
            <div class="print-toolbar">
              <button class="btn-action" onclick="window.print()">Print Report</button>
            </div>

            <div class="page-sheet">
              <!-- HEADER -->
              <div class="header-row">
                <div class="header-left">
                  <img src="${companyLogo}" alt="UNIPRO" class="unipro-logo-img" />
                </div>

                <div class="header-center">
                  <div class="company-title">${companyName}</div>
                  ${compAddress ? `<div class="company-address">${compAddress}</div>` : ''}
                  ${compPhone ? `<div class="company-address" style="margin-top: 2px;">TEL: ${compPhone}</div>` : ''}
                </div>

                <div class="header-right">
                  <table>
                    <tr><td>PAGE</td><td>:</td><td style="font-weight: 700;">1</td></tr>
                  </table>
                </div>
              </div>

              <div class="header-ornament">◆</div>

              <!-- REPORT TITLE -->
              <div class="report-title-area">
                <div class="report-main-title">TRANSFER NOTE</div>
                <div class="report-sub-info">STOCK TRADE-IN TRANSFER</div>
                <div class="report-period-line">${printTimestamp}</div>
              </div>

              <!-- TRANSFER META CARD -->
              <div class="transfer-meta-card">
                <div class="meta-item">
                  <span class="meta-label">Transfer Number</span>
                  <span class="meta-val">${header.transferNo || ''}</span>
                </div>
                <div class="meta-item">
                  <span class="meta-label">Transfer Date</span>
                  <span class="meta-val">${transferDateFormatted || ''}</span>
                </div>
                <div class="meta-item">
                  <span class="meta-label">From Location</span>
                  <span class="meta-val">${header.fromLocation || ''}</span>
                </div>
                <div class="meta-item">
                  <span class="meta-label">To Location</span>
                  <span class="meta-val">${header.toLocation || ''}</span>
                </div>
              </div>

              <!-- DETAIL TABLE -->
              <div class="table-container">
                <table class="report-table">
                  <colgroup>
                    <col style="width: 14%;" />
                    <col style="width: 24%;" />
                    <col style="width: 8%;" />
                    <col style="width: 12%;" />
                    <col style="width: 11%;" />
                    <col style="width: 11%;" />
                    <col style="width: 6%;" />
                    <col style="width: 14%;" />
                  </colgroup>
                  <thead>
                    <tr>
                      <th>INV. CODE</th>
                      <th style="text-align: left !important; padding-left: 10px;">DESC.</th>
                      <th>SIZE</th>
                      <th>SUPP. CODE</th>
                      <th style="text-align: right !important; padding-right: 8px;">GROSS WT.</th>
                      <th style="text-align: right !important; padding-right: 8px;">NET WT.</th>
                      <th>PCS</th>
                      <th style="text-align: right !important; padding-right: 8px;">TOTAL COST</th>
                    </tr>
                  </thead>
                  <tbody>
                    ${rowsHtml || '<tr><td colspan="8" style="text-align:center; padding: 12px;">No inventory records found</td></tr>'}

                    <!-- Item-level Total Row -->
                    <tr class="item-total-row">
                      <td colspan="4" style="text-align: right !important; padding-right: 14px; font-weight: 700;">TOTAL:</td>
                      <td style="text-align: right !important; padding-right: 8px; font-weight: 700;">${totalGrossWeight.toFixed(2)}</td>
                      <td style="text-align: right !important; padding-right: 8px; font-weight: 700;">${totalNetWeight.toFixed(2)}</td>
                      <td style="text-align: center;">${totalPcsCount > 0 ? totalPcsCount : ''}</td>
                      <td style="text-align: right !important; padding-right: 8px; font-weight: 700;">$ ${totalCostSum.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                    </tr>

                    <!-- Grand Total Row -->
                    <tr class="grand-total-row">
                      <td colspan="4" style="text-align: left !important; padding-left: 10px; font-weight: 700;">GRAND TOTAL :</td>
                      <td style="text-align: right !important; padding-right: 8px; font-weight: 700;">${totalGrossWeight.toFixed(2)}</td>
                      <td style="text-align: right !important; padding-right: 8px; font-weight: 700;">${totalNetWeight.toFixed(2)}</td>
                      <td style="text-align: center;">${totalPcsCount > 0 ? totalPcsCount : ''}</td>
                      <td style="text-align: right !important; padding-right: 8px; font-weight: 700;">$ ${totalCostSum.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                    </tr>
                  </tbody>
                </table>
              </div>

              <!-- REMARKS -->
              <div class="remarks-box">
                REMARKS &nbsp;&nbsp; : &nbsp;&nbsp; <span style="font-weight: 500;">${header.remarks && header.remarks.trim() ? header.remarks.trim() : 'N/A'}</span>
              </div>

              <!-- SIGNATURE SECTION -->
              <div class="signature-section">
                <!-- Send Stock From -->
                <div class="sign-card">
                  <div class="sign-header">SEND STOCK FROM: ${header.fromLocation || ''}</div>
                  <div class="sign-row">No. of Items: ...........................</div>
                  <div class="sign-row">Name: .....................................</div>
                  <div class="sign-row">Sign: .......................................</div>
                  <div class="sign-row">Date: .......................................</div>
                </div>

                <!-- Received Stock At -->
                <div class="sign-card">
                  <div class="sign-header">RECEIVED STOCK AT: ${header.toLocation || header.fromLocation || ''}</div>
                  <div class="sign-row">No. of Items: ...........................</div>
                  <div class="sign-row">Name: .....................................</div>
                  <div class="sign-row">Sign: .......................................</div>
                  <div class="sign-row">Date: .......................................</div>
                </div>

                <!-- Cross Verification -->
                <div class="sign-card">
                  <div class="sign-header">CROSS VERIFICATION</div>
                  <div class="sign-row">Weight Cross Checked By: ..........</div>
                  <div class="sign-row">No. of PCS Checked: ...............</div>
                  <div class="sign-row">Sign & Date: .............................</div>
                </div>
              </div>

              <!-- FOOTER -->
              <div class="report-footer">
                <div class="footer-divider"></div>
                <div class="footer-gen-text">*** System Generated Report ***</div>
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
        } else {
          // If popup is blocked, write blob and trigger
          const blob = new Blob([printHtml], { type: 'text/html;charset=utf-8;' });
          const url = URL.createObjectURL(blob);
          const win = window.open(url, '_blank');
          if (!win) {
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

  return (
    <SafeAreaView style={styles.safeArea} edges={['top']}>
      <Stack.Screen options={{ headerShown: false }} />

      {/* LUXURY ROYAL GOLD HEADER BAR */}
      <View style={styles.headerBar}>
        <View style={styles.headerLeft}>
          <TouchableOpacity style={styles.backBtn} onPress={() => router.back()} activeOpacity={0.7}>
            <Ionicons name="arrow-back" size={20} color="#3B2D17" />
          </TouchableOpacity>
          <ThemedText style={styles.headerTitle}>Stock Trade-In Transfer</ThemedText>
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
            <ThemedText style={styles.clearBtnText}>Clear (F6)</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity
            style={[styles.actionBtn, styles.reprintBtn]}
            onPress={() => handlePrint()}
            activeOpacity={0.8}
            disabled={saving || loading}
          >
            <Ionicons name="print-outline" size={14} color="#5C4A28" />
            <ThemedText style={styles.clearBtnText}>Print</ThemedText>
          </TouchableOpacity>

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
                <Ionicons name={isLocked ? 'lock-closed-outline' : 'swap-horizontal-outline'} size={15} color="#9E6D08" />
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

          {/* CARD 2: CONSOLIDATED METRICS */}
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

          {/* CARD 3: ITEMS DATA GRID & SCANNER */}
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
                <ThemedText style={styles.modalHeaderTitle}>Search Stock Trade-In Transfers</ThemedText>
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

      {/* SALESMAN PASSWORD AUTHORIZATION MODAL */}
      <Modal
        visible={showPasswordModal}
        transparent={true}
        animationType="fade"
        onRequestClose={() => setShowPasswordModal(false)}
      >
        <View style={styles.modalOverlay}>
          <View style={styles.passwordModalCard}>
            <View style={styles.passwordModalHeader}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="lock-closed" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.modalHeaderTitle}>Salesman Authorization</ThemedText>
              </View>
              <TouchableOpacity
                onPress={() => {
                  setShowPasswordModal(false);
                  setSalesManPassword('');
                }}
                activeOpacity={0.7}
              >
                <Ionicons name="close" size={18} color="#3B2D17" />
              </TouchableOpacity>
            </View>

            <View style={{ padding: 16 }}>
              <ThemedText style={styles.passwordInstruction}>
                Enter salesman password to authorize trade-in transfer:
              </ThemedText>
              <TextInput
                style={[styles.formInput, { height: 32, marginBottom: 14 }]}
                value={salesManPassword}
                onChangeText={setSalesManPassword}
                secureTextEntry
                autoFocus
                placeholder="Enter password..."
                placeholderTextColor="#9E8D77"
                onSubmitEditing={verifyPasswordAndTransfer}
              />
              <View style={{ flexDirection: 'row', gap: 10, justifyContent: 'flex-end' }}>
                <TouchableOpacity
                  style={[styles.actionBtn, styles.clearBtn]}
                  onPress={() => {
                    setShowPasswordModal(false);
                    setSalesManPassword('');
                  }}
                  activeOpacity={0.8}
                >
                  <ThemedText style={styles.clearBtnText}>Cancel</ThemedText>
                </TouchableOpacity>
                <TouchableOpacity
                  style={[styles.actionBtn, styles.saveBtn]}
                  onPress={verifyPasswordAndTransfer}
                  activeOpacity={0.8}
                >
                  <ThemedText style={styles.saveBtnText}>Authorize</ThemedText>
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

  // Metric Boxes
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

  // ===== SALESMAN PASSWORD MODAL =====
  passwordModalCard: {
    width: '90%',
    maxWidth: 400,
    backgroundColor: '#FFFFFF',
    borderRadius: 10,
    overflow: 'hidden',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.15,
    shadowRadius: 10,
    elevation: 8,
  },
  passwordModalHeader: {
    backgroundColor: '#F5E7CA',
    paddingHorizontal: 14,
    paddingVertical: 10,
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    borderBottomWidth: 1,
    borderBottomColor: '#E8DCB8',
  },
  passwordInstruction: {
    fontSize: 12,
    color: '#5C4A28',
    marginBottom: 10,
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
