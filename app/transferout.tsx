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

const API_URL = `${API_BASE_URL}/transferout`;

export default function TransferOutScreen() {
  const themeColors = useThemeColors();
  const router = useRouter();
  const params = useLocalSearchParams();

  const [loading, setLoading] = useState(false);
  const [saving, setSaving] = useState(false);
  const [toast, setToast] = useState<{ message: string; type: 'success' | 'error' } | null>(null);

  // Form State
  const [locations, setLocations] = useState<any[]>([]);
  const [toLocation, setToLocation] = useState('');
  const [fromLocation, setFromLocation] = useState('HQ');
  const [transferNo, setTransferNo] = useState(params.transferNo ? String(params.transferNo) : '');
  const [remarks, setRemarks] = useState('');
  const [isLocked, setIsLocked] = useState(false);

  // Location LOV modal state
  const [locationModalVisible, setLocationModalVisible] = useState(false);
  const [locationSearchQuery, setLocationSearchQuery] = useState('');

  // Browse Past Transfers modal state
  const [browseModalVisible, setBrowseModalVisible] = useState(false);
  const [transferList, setTransferList] = useState<any[]>([]);
  const [transferSearchQuery, setTransferSearchQuery] = useState('');

  // Salesman Password Modal (matching PnlSalesManPwd in frmTransferOut.vb)
  const [salesmanModalVisible, setSalesmanModalVisible] = useState(false);
  const [salesmanPassword, setSalesmanPassword] = useState('');
  const [verifiedSalesman, setVerifiedSalesman] = useState<{ code: string; name: string } | null>(null);

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

  // Scanner & Staged Item Detail Form State (matching txtInventoryCode, txtDescription, txtWeight, txtSize, txtPCS, txtCostPerGram, txtPMC, txtSMC, txtTotalCost in frmTransferOut.vb)
  const [inventoryCode, setInventoryCode] = useState('');
  const [itemDescription, setItemDescription] = useState('');
  const [itemClassCode, setItemClassCode] = useState('');
  const [itemCategoryCode, setCategoryCode] = useState('');
  const [itemCatalogueCode, setCatalogueCode] = useState('');
  const [itemWeight, setItemWeight] = useState('');
  const [itemSize, setItemSize] = useState('');
  const [itemPCS, setItemPCS] = useState('1');
  const [itemCostPerGram, setItemCostPerGram] = useState('');
  const [itemPMC, setItemPMC] = useState('');
  const [itemSMC, setItemSMC] = useState('');
  const [itemTotalCost, setItemTotalCost] = useState('');
  const [itemSalesType, setItemSalesType] = useState('');

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
    setTimeout(() => setToast(null), 3000);
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

  const fetchTransferList = async () => {
    try {
      setLoading(true);
      const res = await fetch(`${API_URL}/list?fromLocation=${encodeURIComponent(fromLocation)}&limit=100`);
      const data = await res.json();
      if (data.success) {
        setTransferList(data.data || []);
      }
    } catch (err) {
      console.error('Error fetching transfer list', err);
    } finally {
      setLoading(false);
    }
  };

  const fetchTransferDetails = async (tNo: string) => {
    if (!tNo.trim()) return;
    try {
      setLoading(true);
      const res = await fetch(`${API_URL}/fetch?transferNo=${encodeURIComponent(tNo.trim())}`);
      const data = await res.json();

      if (data.success) {
        setTransferNo(data.header.TransferNo);
        setToLocation(data.header.ToLocation);
        setRemarks(data.header.Remarks || '');
        if (data.header.FromLocation) {
          setFromLocation(data.header.FromLocation);
        }
        if (data.header.TransferDate) {
          setTransferDate(new Date(data.header.TransferDate).toISOString().split('T')[0]);
        }
        setGridData(data.details || []);
        setIsLocked(true);
        subClearTransfer();
        showToast('Transfer details loaded', 'success');
      } else {
        showToast(data.message || 'Transfer number does not exists.', 'error');
        setTransferNo('');
      }
    } catch (err: any) {
      showToast('Error loading transfer details', 'error');
    } finally {
      setLoading(false);
    }
  };

  const subClearTransfer = () => {
    setInventoryCode('');
    setItemDescription('');
    setItemClassCode('');
    setCategoryCode('');
    setCatalogueCode('');
    setItemWeight('0.00');
    setItemSize('0.00');
    setItemPCS('0');
    setItemCostPerGram('0.00');
    setItemPMC('0.00');
    setItemSMC('0.00');
    setItemTotalCost('0.00');
    setItemSalesType('');
  };

  // Scan or lost-focus on txtInventoryCode (matching txtInventoryCode_LostFocus & fncGetInventory in frmTransferOut.vb)
  const handleScanInventory = async (codeToScan?: string) => {
    const code = (codeToScan || inventoryCode).trim();
    if (!code) return;

    if (!toLocation || !toLocation.trim()) {
      showToast('Select the ToLocation Code', 'error');
      return;
    }

    if (gridData.some((item) => item.InventoryCode.trim().toUpperCase() === code.toUpperCase())) {
      showToast('Item found in transfer list', 'error');
      subClearTransfer();
      return;
    }

    try {
      setLoading(true);
      const res = await fetch(`${API_URL}/inventory/${encodeURIComponent(code)}?fromLocation=${encodeURIComponent(fromLocation)}`);
      const data = await res.json();

      if (res.ok && data.success && data.data) {
        const item = data.data;

        // Populate subform state
        setInventoryCode(item.InventoryCode);
        setItemDescription(item.Description || '');
        setItemClassCode(item.ClassCode || '');
        setCategoryCode(item.CategoryCode || '');
        setCatalogueCode(item.CatalogueCode || '');
        setItemWeight(Number(item.Weight || 0).toFixed(2));
        setItemSize(String(item.SizeCode || '0.00'));
        setItemPCS(String(item.PCS || item.QtyonHand || 1));
        setItemCostPerGram(Number(item.CostPerGram || 0).toFixed(2));
        setItemPMC(Number(item.PMC || 0).toFixed(2));
        setItemSMC(Number(item.SMC || 0).toFixed(2));
        setItemTotalCost(Number(item.TotalCost || 0).toFixed(2));
        setItemSalesType(item.SalesType || '');

        // In frmTransferOut.vb: If msClsSalesType <> "P" Then btnAdd_Click (auto-add to grid)
        if (item.SalesType !== 'P') {
          const gridItem = {
            InventoryCode: item.InventoryCode,
            Description: item.Description,
            ClassCode: item.ClassCode,
            CategoryCode: item.CategoryCode,
            CatalogueCode: item.CatalogueCode,
            Weight: Number(item.Weight) || 0,
            SizeCode: item.SizeCode || '',
            QtyonHand: Number(item.PCS || item.QtyonHand || 1),
            Pcs: Number(item.PCS || item.QtyonHand || 1),
            CostPerGram: Number(item.CostPerGram) || 0,
            PMC: Number(item.PMC) || 0,
            SMC: Number(item.SMC) || 0,
            TotalCost: Number(item.TotalCost) || 0,
          };
          setGridData((prev) => [...prev, gridItem]);
          subClearTransfer();
          showToast(`Item added: ${item.InventoryCode}`, 'success');
        } else {
          showToast(`Piece-based item scanned: ${item.InventoryCode}. Enter PCS and press Add.`, 'success');
        }
      } else {
        showToast(data.message || 'Inventory Not Found ...', 'error');
        subClearTransfer();
      }
    } catch (err: any) {
      showToast('Error fetching inventory item', 'error');
    } finally {
      setLoading(false);
    }
  };

  // Add Item to Grid (matching btnAdd_Click in frmTransferOut.vb)
  const handleAddScannedItem = () => {
    if (!inventoryCode.trim()) {
      showToast('Enter Inventory Code', 'error');
      return;
    }

    if (gridData.some((item) => item.InventoryCode.trim().toUpperCase() === inventoryCode.trim().toUpperCase())) {
      showToast('Item found in transfer list', 'error');
      subClearTransfer();
      return;
    }

    const pcsVal = parseInt(itemPCS, 10) || 1;
    const gridItem = {
      InventoryCode: inventoryCode.trim(),
      Description: itemDescription.trim(),
      ClassCode: itemClassCode.trim(),
      CategoryCode: itemCategoryCode.trim(),
      CatalogueCode: itemCatalogueCode.trim(),
      Weight: parseFloat(itemWeight) || 0,
      SizeCode: itemSize.trim(),
      QtyonHand: pcsVal,
      Pcs: pcsVal,
      CostPerGram: parseFloat(itemCostPerGram) || 0,
      PMC: parseFloat(itemPMC) || 0,
      SMC: parseFloat(itemSMC) || 0,
      TotalCost: parseFloat(itemTotalCost) || 0,
    };

    setGridData((prev) => [...prev, gridItem]);
    subClearTransfer();
    showToast(`Item added: ${gridItem.InventoryCode}`, 'success');
  };

  // Load Scrap Inventory (matching btnScrapInventory_Click in frmTransferOut.vb)
  const handleLoadScrapInventory = async () => {
    if (isLocked) return;
    try {
      setLoading(true);
      const res = await fetch(`${API_URL}/scrap-inventory?fromLocation=${encodeURIComponent(fromLocation)}`);
      const data = await res.json();
      if (data.success && Array.isArray(data.data)) {
        if (data.data.length === 0) {
          showToast('No scrap inventory available to load', 'error');
          return;
        }
        // In VB code: cboToLocationCode.Text = "HQ"
        setToLocation('HQ');
        const scrapGrid = data.data.map((item: any) => ({
          InventoryCode: item.InventoryCode,
          Description: item.Description,
          ClassCode: item.ClassCode,
          CategoryCode: item.CategoryCode,
          CatalogueCode: item.CatalogueCode,
          Weight: Number(item.Weight) || 0,
          SizeCode: item.SizeCode || '',
          QtyonHand: Number(item.QtyonHand || item.PCS || 1),
          Pcs: Number(item.PCS || item.QtyonHand || 1),
          CostPerGram: Number(item.CostPerGram) || 0,
          PMC: Number(item.PMC) || 0,
          SMC: Number(item.SMC) || 0,
          TotalCost: Number(item.TotalCost) || 0,
        }));
        setGridData(scrapGrid);
        subClearTransfer();
        showToast(`Loaded ${scrapGrid.length} scrap items. Destination set to HQ.`, 'success');
      } else {
        showToast(data.message || 'Error loading scrap inventory', 'error');
      }
    } catch (err: any) {
      showToast('Network error loading scrap inventory', 'error');
    } finally {
      setLoading(false);
    }
  };

  const calculateTotals = () => {
    let weight = 0,
      pmc = 0,
      pcs = 0,
      smc = 0,
      cost = 0;
    gridData.forEach((item) => {
      weight += Number(item.Weight) || 0;
      pmc += Number(item.PMC) || 0;
      pcs += Number(item.Pcs || item.QtyonHand || item.Qty || 1) || 0;
      smc += Number(item.SMC) || 0;
      cost += Number(item.TotalCost) || 0;
    });
    setTotals({ Weight: weight, PMC: pmc, PCS: pcs, SMC: smc, TotalCost: cost });
  };

  // Delete row (matching btnDelete_Click in frmTransferOut.vb)
  const handleDeleteItem = (index: number) => {
    if (isLocked) return;
    const itemToDelete = gridData[index];
    const newData = [...gridData];
    newData.splice(index, 1);
    setGridData(newData);
    if (selectedRowIndex === index) setSelectedRowIndex(null);
    showToast(`Item ${itemToDelete.InventoryCode} removed`, 'success');
  };

  // Full clear of voucher (matching btnTransferClear_Click & subClearDetail in frmTransferOut.vb)
  const handleClear = () => {
    setTransferNo('');
    setToLocation('');
    setRemarks('');
    setGridData([]);
    setIsLocked(false);
    setSelectedRowIndex(null);
    setVerifiedSalesman(null);
    setSalesmanPassword('');
    subClearTransfer();
    const d = new Date();
    const mm = String(d.getMonth() + 1).padStart(2, '0');
    const dd = String(d.getDate()).padStart(2, '0');
    const yyyy = d.getFullYear();
    setTransferDate(`${yyyy}-${mm}-${dd}`);
    showToast('Transfer voucher cleared', 'success');
  };

  // Initiation of Transfer Button (matching btnTransfer_Click in frmTransferOut.vb: prompts for salesman password)
  const handleTransferButtonClick = async () => {
    if (isLocked) {
      showToast('This transfer is already completed and locked.', 'error');
      return;
    }
    if (!toLocation.trim()) {
      showToast('Select the ToLocation Code', 'error');
      return;
    }
    if (gridData.length === 0) {
      showToast('No Item To Transfer', 'error');
      return;
    }

    // Mandatory salesman password check before transfer (matching PnlSalesManPwd.Visible = True in frmTransferOut.vb)
    setSalesmanPassword('');
    setSalesmanModalVisible(true);
  };

  // Verify Salesman Password and Execute Transfer (matching txtSalesManPWD_LostFocus & fncUpdateTransfer in frmTransferOut.vb)
  const handleVerifyAndExecuteTransfer = async () => {
    if (!salesmanPassword.trim()) {
      showToast('Please enter salesman password', 'error');
      return;
    }

    try {
      setSaving(true);
      const verifyRes = await fetch(`${API_URL}/verify-salesman`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ password: salesmanPassword.trim() }),
      });
      const verifyData = await verifyRes.json();

      if (!verifyData.success || !verifyData.salesman) {
        showToast(verifyData.message || 'SalesMan Password is Incorrect!', 'error');
        setSalesmanPassword('');
        setSaving(false);
        return;
      }

      setVerifiedSalesman({
        code: verifyData.salesman.SalesmanCode,
        name: verifyData.salesman.SalesManName,
      });
      setSalesmanModalVisible(false);

      // Execute transfer directly with verified salesman code
      executeTransferAPI(verifyData.salesman.SalesmanCode);
    } catch (err: any) {
      setSaving(false);
      showToast('Error validating salesman password', 'error');
    }
  };

  const executeTransferAPI = async (salesManCode: string) => {
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
        salesManCode: salesManCode || 'admin',
        items: gridData.map((i) => ({
          ...i,
          Qty: i.Pcs || i.QtyonHand || 1,
        })),
        userId: salesManCode || 'admin',
      };

      console.log('Sending transfer payload:', payload);

      const res = await fetch(`${API_URL}/transfer`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload),
      });

      const data = await res.json();
      console.log('Transfer response:', data);

      if (data.success) {
        const assignedNo = data.transferNo;
        setTransferNo(assignedNo);
        setIsLocked(true);
        showToast(data.message || `Please take note reference no. ${assignedNo}`, 'success');
        setActivePrintTransferNo(assignedNo);
        setActivePrintIsReprint(false);
        setIsPrintModalVisible(true);
      } else {
        showToast(data.message || 'Failed to complete transfer', 'error');
      }
    } catch (err: any) {
      showToast('Network error executing transfer', 'error');
    } finally {
      setSaving(false);
    }
  };

  const buildTransferOutHtml = (tNo: string, isReprint: boolean, printData: any) => {
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

    const sReportTitle = 'TRANSFER OUT';
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
                      <td class="val">${header.salesManCode || verifiedSalesman?.code || 'Staff'}</td>
                    </tr>
                    <tr>
                      <td class="lbl">Transfer Type</td>
                      <td class="colon">:</td>
                      <td class="val">TRANSFER OUT</td>
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
                        No detail records in this Transfer Out Voucher
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
                staffName: (header.salesManCode || verifiedSalesman?.code || 'Staff').trim(),
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

      const htmlContent = buildTransferOutHtml(tNo, isReprint, json.data);
      const docTitle = isReprint ? `Transfer Out Voucher (Duplicate) - ${tNo}` : `Transfer Out Voucher - ${tNo}`;

      if (Platform.OS === 'web' && typeof window !== 'undefined') {
        const frameId = '__transfer_out_print_frame__';
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
              showToast(`Transfer Out Voucher ${tNo} print dialog opened`, 'success');
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

      const htmlContent = buildTransferOutHtml(tNo, isReprint, json.data);
      const downloadFilename = `Transfer_Out_${tNo.replace(/[^a-zA-Z0-9_-]/g, '_')}.pdf`;

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
      const downloadFilename = `Transfer_Out_${tNo.replace(/[^a-zA-Z0-9_-]/g, '_')}.xlsx`;

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


  const selectedLocationObj = locations.find((l) => l.LocationCode === toLocation);

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
            <ThemedText style={styles.headerTitle}>Branch Stock Transfer Out</ThemedText>
            <ThemedText style={styles.headerSubtitle}>Inter-Branch & Warehouse Stock Movement Voucher</ThemedText>
          </View>
          {(loading || saving) && <ActivityIndicator size="small" color="#B48218" style={{ marginLeft: 8 }} />}
        </View>

        <View style={styles.headerRight}>
          {/* Browse Past Transfers */}
          <TouchableOpacity
            style={[styles.actionBtn, styles.clearBtn]}
            onPress={() => {
              fetchTransferList();
              setBrowseModalVisible(true);
            }}
            activeOpacity={0.8}
            disabled={saving || loading}
          >
            <Ionicons name="search-outline" size={14} color="#5C4A28" />
            <ThemedText style={styles.clearBtnText}>Search</ThemedText>
          </TouchableOpacity>

          {/* Load Scrap Inventory (matching btnScrapInventory in frmTransferOut.vb) */}
          <TouchableOpacity
            style={[styles.actionBtn, styles.scrapBtn, isLocked && { opacity: 0.5 }]}
            onPress={handleLoadScrapInventory}
            activeOpacity={0.8}
            disabled={saving || loading || isLocked}
          >
            <Ionicons name="hammer-outline" size={14} color="#B76E79" />
            <ThemedText style={styles.scrapBtnText}>Scrap Inv.</ThemedText>
          </TouchableOpacity>

          {/* Clear Button (matching btnTransferClear in frmTransferOut.vb) */}
          <TouchableOpacity
            style={[styles.actionBtn, styles.clearBtn]}
            onPress={handleClear}
            activeOpacity={0.8}
            disabled={saving || loading}
          >
            <Ionicons name="refresh-outline" size={14} color="#5C4A28" />
            <ThemedText style={styles.clearBtnText}>Clear</ThemedText>
          </TouchableOpacity>

          {/* Print Button (matching btnPrint in frmTransferOut.vb) */}
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
          >
            <Ionicons name="print-outline" size={14} color="#5C4A28" />
            <ThemedText style={styles.clearBtnText}>Print</ThemedText>
          </TouchableOpacity>

          {/* Transfer Button (matching btnTransfer in frmTransferOut.vb) */}
          <TouchableOpacity
            style={[styles.actionBtn, styles.saveBtn, isLocked && { opacity: 0.6 }]}
            onPress={handleTransferButtonClick}
            activeOpacity={0.8}
            disabled={saving || loading || isLocked}
          >
            {saving ? (
              <ActivityIndicator size="small" color="#9E6D08" />
            ) : (
              <>
                <Ionicons name="airplane-outline" size={15} color="#9E6D08" />
                <ThemedText style={styles.saveBtnText}>
                  {isLocked ? 'Transfer Locked' : 'Transfer'}
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
          {/* CARD 1: TRANSFER DOCKET & LOCATION ROUTING */}
          <View style={styles.goldCard}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="swap-horizontal-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitleSmall}>Transfer Routing & Dispatch Header</ThemedText>
              </View>

              {transferNo ? (
                <View style={styles.transferBadge}>
                  <ThemedText style={styles.transferBadgeLabel}>Transfer No:</ThemedText>
                  <ThemedText style={styles.transferBadgeValue}>{transferNo}</ThemedText>
                </View>
              ) : null}
            </View>

            <View style={styles.cardBody}>
              {/* Row 1: Transfer No, Transfer Date, From Location, To Location */}
              <View style={styles.fourColumnGrid}>
                {/* Transfer No */}
                <View style={[styles.inlineFieldRow, { flex: 1.1, minWidth: 230 }]}>
                  <ThemedText style={styles.inlineFieldLabel}>Transfer No</ThemedText>
                  <TextInput
                    style={[styles.formInput, isLocked ? styles.readonlyInput : undefined, { flex: 1 }]}
                    value={transferNo}
                    onChangeText={setTransferNo}
                    onBlur={() => transferNo && !isLocked && fetchTransferDetails(transferNo)}
                    onSubmitEditing={() => transferNo && !isLocked && fetchTransferDetails(transferNo)}
                    placeholder="Auto-generated"
                    placeholderTextColor="#9E8D77"
                    editable={!isLocked}
                  />
                </View>

                {/* Transfer Date */}
                <View style={[styles.inlineFieldRow, { flex: 0.9, minWidth: 200 }]}>
                  <ThemedText style={styles.inlineFieldLabel}>Transfer Date</ThemedText>
                  <TextInput
                    style={[styles.formInput, isLocked ? styles.readonlyInput : undefined, { flex: 1 }]}
                    value={transferDate}
                    onChangeText={setTransferDate}
                    editable={!isLocked}
                  />
                </View>

                {/* From Location */}
                <View style={[styles.inlineFieldRow, { flex: 0.9, minWidth: 180 }]}>
                  <ThemedText style={styles.inlineFieldLabel}>From Loc.</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1 }]}
                    value={fromLocation}
                    editable={false}
                  />
                </View>

                {/* To Location (With Lookup Modal & Select fallback) */}
                <View style={[styles.inlineFieldRow, { flex: 1.1, minWidth: 240 }]}>
                  <ThemedText style={styles.inlineFieldLabel}>To Location *</ThemedText>
                  <View style={styles.inputWithLookup}>
                    <TextInput
                      style={[styles.formInput, styles.inputAttached, { flex: 1 }]}
                      value={selectedLocationObj ? `${selectedLocationObj.LocationCode} - ${selectedLocationObj.LocationName}` : toLocation}
                      placeholder="Select Target Branch..."
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
                    >
                      <Ionicons name="search" size={13} color="#7D5E19" />
                    </TouchableOpacity>
                  </View>
                </View>
              </View>

              {/* Row 2: Remarks */}
              <View style={[styles.singleColumnRow, { marginTop: 6 }]}>
                <View style={styles.inlineFieldRow}>
                  <ThemedText style={styles.inlineFieldLabel}>Remarks</ThemedText>
                  <TextInput
                    style={[styles.formInput, isLocked ? styles.readonlyInput : undefined, { flex: 1 }]}
                    value={remarks}
                    onChangeText={setRemarks}
                    placeholder="Dispatch notes or transit instructions..."
                    placeholderTextColor="#9E8D77"
                    editable={!isLocked}
                  />
                </View>
              </View>
            </View>
          </View>

          {/* CARD 2: DISPATCH TOTALS & FINANCIAL SUMMARY */}
          <View style={[styles.goldCard, { marginTop: 10 }]}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="calculator-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitleSmall}>Consolidated Dispatch Totals</ThemedText>
              </View>
            </View>

            <View style={styles.cardBody}>
              <View style={styles.fiveColumnGrid}>
                {/* Total Weight */}
                <View style={[styles.inlineFieldRow, { flex: 1, minWidth: 200 }]}>
                  <ThemedText style={[styles.inlineFieldLabel, { width: 95 }]}>Total Wt (g)</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1, minWidth: 90, textAlign: 'right', fontWeight: 'bold' }]}
                    value={totals.Weight.toFixed(3)}
                    editable={false}
                  />
                </View>

                {/* Total Pieces */}
                <View style={[styles.inlineFieldRow, { flex: 1, minWidth: 180 }]}>
                  <ThemedText style={[styles.inlineFieldLabel, { width: 85 }]}>Total PCS</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1, minWidth: 70, textAlign: 'right' }]}
                    value={String(totals.PCS)}
                    editable={false}
                  />
                </View>

                {/* Total PMC */}
                <View style={[styles.inlineFieldRow, { flex: 1, minWidth: 195 }]}>
                  <ThemedText style={[styles.inlineFieldLabel, { width: 95 }]}>Total PMC ($)</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1, minWidth: 80, textAlign: 'right' }]}
                    value={totals.PMC.toFixed(2)}
                    editable={false}
                  />
                </View>

                {/* Total SMC */}
                <View style={[styles.inlineFieldRow, { flex: 1, minWidth: 195 }]}>
                  <ThemedText style={[styles.inlineFieldLabel, { width: 95 }]}>Total SMC ($)</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1, minWidth: 80, textAlign: 'right' }]}
                    value={totals.SMC.toFixed(2)}
                    editable={false}
                  />
                </View>

                {/* Total Cost */}
                <View style={[styles.inlineFieldRow, { flex: 1, minWidth: 205 }]}>
                  <ThemedText style={[styles.inlineFieldLabel, { width: 95 }]}>Total Cost ($)</ThemedText>
                  <TextInput
                    style={[
                      styles.formInput,
                      styles.readonlyInput,
                      styles.highlightTotal,
                      { flex: 1, minWidth: 90, textAlign: 'right', fontWeight: 'bold' },
                    ]}
                    value={totals.TotalCost.toFixed(2)}
                    editable={false}
                  />
                </View>
              </View>
            </View>
          </View>

          {/* CARD 3: STAGED INVENTORY ITEMS TABLE (100% FULL WIDTH) */}
          <View style={[styles.goldCard, { marginTop: 10 }]}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="cube-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitleSmall}>Staged Items for Transfer</ThemedText>
                <View style={styles.counterBadge}>
                  <ThemedText style={styles.counterBadgeText}>{gridData.length} items</ThemedText>
                </View>
              </View>
            </View>

            {/* Table Header (100% Full Width) */}
            <View style={styles.tableHeadRow}>
              <ThemedText style={[styles.tableHeadCell, { flex: 1.6 }]}>Inventory Code</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 2.4 }]}>Description</ThemedText>
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
                  No items staged for transfer. Scan barcodes using the entry bar below.
                </ThemedText>
              </View>
            ) : (
              gridData.map((item, index) => {
                const isSelected = selectedRowIndex === index;
                return (
                  <TouchableOpacity
                    key={index}
                    style={[styles.tableRow, isSelected && styles.tableRowSelected]}
                    onPress={() => setSelectedRowIndex(index)}
                    activeOpacity={0.8}
                  >
                    <ThemedText style={[styles.tableCell, styles.codeCell, { flex: 1.6 }]}>
                      {item.InventoryCode}
                    </ThemedText>
                    <ThemedText style={[styles.tableCell, { flex: 2.4 }]} numberOfLines={1}>
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
                        >
                          <Ionicons name="trash-outline" size={14} color="#C62828" />
                        </TouchableOpacity>
                      </View>
                    )}
                  </TouchableOpacity>
                );
              })
            )}

            {/* SCANNER & ITEM DETAIL ENTRY SUBFORM (matching frmTransferOut.vb single item fields) */}
            {!isLocked && (
              <View style={styles.itemEntryBar}>
                {/* Row 1: Barcode scan input, Item Description */}
                <View style={[styles.itemEntryRow, { marginBottom: 6 }]}>
                  {/* Scan Input */}
                  <View style={[styles.inlineFieldRow, { flex: 1.4, minWidth: 220 }]}>
                    <ThemedText style={[styles.inlineFieldLabel, { width: 95 }]}>Scan Barcode *</ThemedText>
                    <TextInput
                      style={[styles.formInput, { flex: 1, minWidth: 110 }]}
                      value={inventoryCode}
                      onChangeText={setInventoryCode}
                      onBlur={() => inventoryCode && handleScanInventory()}
                      onSubmitEditing={() => inventoryCode && handleScanInventory()}
                      placeholder="Scan or enter code..."
                      placeholderTextColor="#9E8D77"
                    />
                  </View>

                  {/* Description */}
                  <View style={[styles.inlineFieldRow, { flex: 2.2, minWidth: 260 }]}>
                    <ThemedText style={[styles.inlineFieldLabel, { width: 85 }]}>Description</ThemedText>
                    <TextInput
                      style={[styles.formInput, styles.readonlyInput, { flex: 1 }]}
                      value={itemDescription}
                      editable={false}
                      placeholder="Item description"
                      placeholderTextColor="#9E8D77"
                    />
                  </View>

                  {/* Class Code */}
                  <View style={[styles.inlineFieldRow, { flex: 0.9, minWidth: 140 }]}>
                    <ThemedText style={[styles.inlineFieldLabel, { width: 55 }]}>Class</ThemedText>
                    <TextInput
                      style={[styles.formInput, styles.readonlyInput, { flex: 1, textAlign: 'center' }]}
                      value={itemClassCode}
                      editable={false}
                    />
                  </View>
                </View>

                {/* Row 2: Weight, Size, PCS, Cost/g, PMC, SMC, Total Cost, Add Item Button */}
                <View style={styles.itemEntryRow}>
                  {/* Weight */}
                  <View style={styles.subformField}>
                    <ThemedText style={styles.subformLabel}>Weight</ThemedText>
                    <TextInput
                      style={[styles.formInput, styles.readonlyInput, { textAlign: 'right' }]}
                      value={itemWeight}
                      editable={false}
                      placeholder="0.00"
                      placeholderTextColor="#9E8D77"
                    />
                  </View>

                  {/* Size */}
                  <View style={styles.subformField}>
                    <ThemedText style={styles.subformLabel}>Size</ThemedText>
                    <TextInput
                      style={[styles.formInput, styles.readonlyInput, { textAlign: 'center' }]}
                      value={itemSize}
                      editable={false}
                    />
                  </View>

                  {/* PCS (Editable for piece-sales or when msClsSalesType = 'P') */}
                  <View style={styles.subformField}>
                    <ThemedText style={styles.subformLabel}>PCS *</ThemedText>
                    <TextInput
                      style={[styles.formInput, { textAlign: 'right' }]}
                      value={itemPCS}
                      onChangeText={(val) => {
                        setItemPCS(val);
                        if (itemSalesType === 'P') {
                          const pcsNum = parseFloat(val) || 0;
                          const smcNum = parseFloat(itemSMC) || 0;
                          setItemTotalCost((pcsNum * smcNum).toFixed(2));
                        }
                      }}
                      keyboardType="numeric"
                      placeholder="1"
                    />
                  </View>

                  {/* Cost Per Gram */}
                  <View style={styles.subformField}>
                    <ThemedText style={styles.subformLabel}>Cost/g</ThemedText>
                    <TextInput
                      style={[styles.formInput, styles.readonlyInput, { textAlign: 'right' }]}
                      value={itemCostPerGram}
                      editable={false}
                    />
                  </View>

                  {/* PMC */}
                  <View style={styles.subformField}>
                    <ThemedText style={styles.subformLabel}>PMC</ThemedText>
                    <TextInput
                      style={[styles.formInput, styles.readonlyInput, { textAlign: 'right' }]}
                      value={itemPMC}
                      editable={false}
                    />
                  </View>

                  {/* SMC */}
                  <View style={styles.subformField}>
                    <ThemedText style={styles.subformLabel}>SMC</ThemedText>
                    <TextInput
                      style={[styles.formInput, styles.readonlyInput, { textAlign: 'right' }]}
                      value={itemSMC}
                      editable={false}
                    />
                  </View>

                  {/* Total Cost */}
                  <View style={[styles.subformField, { flex: 1.2 }]}>
                    <ThemedText style={styles.subformLabel}>Total ($)</ThemedText>
                    <TextInput
                      style={[styles.formInput, styles.readonlyInput, styles.highlightTotal, { textAlign: 'right', fontWeight: 'bold' }]}
                      value={itemTotalCost}
                      editable={false}
                    />
                  </View>

                  {/* Action Buttons */}
                  <View style={{ flexDirection: 'row', alignItems: 'center', gap: 6, alignSelf: 'flex-end', paddingBottom: 1 }}>
                    <TouchableOpacity
                      style={[styles.tableActionBtn, styles.addBtn, { height: 26, paddingHorizontal: 12 }]}
                      onPress={handleAddScannedItem}
                      activeOpacity={0.8}
                    >
                      <Ionicons name="add-circle-outline" size={14} color="#9E6D08" style={{ marginRight: 4 }} />
                      <ThemedText style={styles.addBtnText}>Add</ThemedText>
                    </TouchableOpacity>

                    <TouchableOpacity
                      style={[styles.tableActionBtn, styles.clearBtn, { height: 26, paddingHorizontal: 8 }]}
                      onPress={subClearTransfer}
                      activeOpacity={0.8}
                    >
                      <Ionicons name="close-outline" size={14} color="#5C4A28" />
                    </TouchableOpacity>
                  </View>
                </View>
              </View>
            )}
          </View>
        </ScrollView>
      </KeyboardAvoidingView>

      {/* LOCATION LOOKUP MODAL */}
      <Modal visible={locationModalVisible} transparent animationType="fade" onRequestClose={() => setLocationModalVisible(false)}>
        <View style={styles.modalOverlay}>
          <View style={styles.lovModalCard}>
            <View style={styles.modalHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="business-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.modalHeaderTitle}>Select Target Branch Location</ThemedText>
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

      {/* BROWSE PAST TRANSFERS MODAL */}
      <Modal visible={browseModalVisible} transparent animationType="fade" onRequestClose={() => setBrowseModalVisible(false)}>
        <View style={styles.modalOverlay}>
          <View style={[styles.lovModalCard, { maxWidth: 700 }]}>
            <View style={styles.modalHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="document-text-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.modalHeaderTitle}>Search Past Transfer Out Vouchers</ThemedText>
              </View>
              <TouchableOpacity
                onPress={() => setBrowseModalVisible(false)}
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
                placeholder="Search Transfer No, To Location, Date..."
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
              <ThemedText style={[styles.lovHeadText, { flex: 1.8 }]}>Transfer No</ThemedText>
              <ThemedText style={[styles.lovHeadText, { flex: 1.3 }]}>Date</ThemedText>
              <ThemedText style={[styles.lovHeadText, { flex: 1.2 }]}>To Loc</ThemedText>
              <ThemedText style={[styles.lovHeadText, { flex: 1, textAlign: 'right' }]}>Weight (g)</ThemedText>
              <ThemedText style={[styles.lovHeadText, { flex: 0.8, textAlign: 'right' }]}>PCS</ThemedText>
              <ThemedText style={[styles.lovHeadText, { flex: 1.2, textAlign: 'right' }]}>Total ($)</ThemedText>
            </View>

            <FlatList
              data={transferList.filter(
                (t) =>
                  (t.TransferNo || '').toLowerCase().includes(transferSearchQuery.toLowerCase()) ||
                  (t.ToLocation || '').toLowerCase().includes(transferSearchQuery.toLowerCase()) ||
                  (t.TransferDate || '').toLowerCase().includes(transferSearchQuery.toLowerCase())
              )}
              keyExtractor={(item) => item.TransferNo}
              renderItem={({ item }) => (
                <TouchableOpacity
                  style={styles.lovItemRow}
                  onPress={() => {
                    setBrowseModalVisible(false);
                    fetchTransferDetails(item.TransferNo);
                  }}
                  activeOpacity={0.7}
                >
                  <ThemedText style={[styles.lovItemText, styles.codeCell, { flex: 1.8 }]}>
                    {item.TransferNo}
                  </ThemedText>
                  <ThemedText style={[styles.lovItemText, { flex: 1.3 }]}>
                    {item.TransferDate ? new Date(item.TransferDate).toISOString().split('T')[0] : ''}
                  </ThemedText>
                  <ThemedText style={[styles.lovItemText, { flex: 1.2 }]}>{item.ToLocation}</ThemedText>
                  <ThemedText style={[styles.lovItemText, { flex: 1, textAlign: 'right' }]}>
                    {Number(item.TotalWeight || 0).toFixed(3)}
                  </ThemedText>
                  <ThemedText style={[styles.lovItemText, { flex: 0.8, textAlign: 'right' }]}>
                    {item.TotalPcs || 0}
                  </ThemedText>
                  <ThemedText style={[styles.lovItemText, styles.amountCell, { flex: 1.2, textAlign: 'right' }]}>
                    {Number(item.TotalCost || 0).toFixed(2)}
                  </ThemedText>
                </TouchableOpacity>
              )}
            />
          </View>
        </View>
      </Modal>

      {/* SALESMAN PASSWORD AUTH MODAL (matching PnlSalesManPwd in frmTransferOut.vb) */}
      <Modal visible={salesmanModalVisible} transparent animationType="fade" onRequestClose={() => setSalesmanModalVisible(false)}>
        <View style={styles.modalOverlay}>
          <View style={[styles.lovModalCard, { maxWidth: 400 }]}>
            <View style={styles.modalHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="lock-closed-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.modalHeaderTitle}>Salesman Authorization</ThemedText>
              </View>
              <TouchableOpacity
                onPress={() => setSalesmanModalVisible(false)}
                activeOpacity={0.7}
                style={styles.closeModalBtn}
              >
                <Ionicons name="close" size={18} color="#3B2D17" />
              </TouchableOpacity>
            </View>

            <View style={{ padding: 16 }}>
              <ThemedText style={{ fontSize: 12, color: '#5C4A28', marginBottom: 8, fontWeight: 'bold' }}>
                Enter Salesman Password to authorize transfer:
              </ThemedText>

              <TextInput
                style={[styles.formInput, { height: 34, fontSize: 14, marginBottom: 16 }]}
                value={salesmanPassword}
                onChangeText={setSalesmanPassword}
                secureTextEntry
                placeholder="Password"
                placeholderTextColor="#9E8D77"
                onSubmitEditing={handleVerifyAndExecuteTransfer}
                autoFocus
              />

              <View style={{ flexDirection: 'row', justifyContent: 'flex-end', gap: 10 }}>
                <TouchableOpacity
                  style={[styles.actionBtn, styles.clearBtn, { paddingVertical: 8, paddingHorizontal: 16 }]}
                  onPress={() => setSalesmanModalVisible(false)}
                  activeOpacity={0.7}
                >
                  <ThemedText style={styles.clearBtnText}>Cancel</ThemedText>
                </TouchableOpacity>

                <TouchableOpacity
                  style={[styles.actionBtn, styles.saveBtn, { paddingVertical: 8, paddingHorizontal: 18 }]}
                  onPress={handleVerifyAndExecuteTransfer}
                  activeOpacity={0.8}
                  disabled={saving}
                >
                  {saving ? (
                    <ActivityIndicator size="small" color="#9E6D08" />
                  ) : (
                    <>
                      <Ionicons name="checkmark-circle-outline" size={15} color="#9E6D08" style={{ marginRight: 4 }} />
                      <ThemedText style={styles.saveBtnText}>Authorize & Transfer</ThemedText>
                    </>
                  )}
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
  scrapBtn: {
    backgroundColor: '#FFF0F2',
    borderColor: '#D9Aab0',
  },
  scrapBtnText: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#8A3B47',
    marginLeft: 4,
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
  transferBadge: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFF4DA',
    paddingHorizontal: 8,
    paddingVertical: 2,
    borderRadius: 4,
    borderWidth: 1,
    borderColor: '#B8903B',
  },
  transferBadgeLabel: {
    fontSize: 10,
    color: '#7D5E19',
    marginRight: 4,
  },
  transferBadgeValue: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#8A5D00',
  },

  // ===== GRIDS & INLINE ROWS =====
  fourColumnGrid: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    gap: 10,
  },
  fiveColumnGrid: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    gap: 12,
  },
  twoColumnGrid: {
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
    minWidth: 215,
    flexDirection: 'row',
    alignItems: 'center',
  },
  inlineFieldLabel: {
    width: 95,
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3B2D17',
    marginRight: 8,
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
  tableRowSelected: {
    backgroundColor: '#FFF8E7',
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
    alignItems: 'center',
    flexWrap: 'wrap',
    gap: 12,
  },
  subformField: {
    flex: 1,
    minWidth: 85,
  },
  subformLabel: {
    fontSize: 10,
    fontWeight: 'bold',
    color: '#3B2D17',
    marginBottom: 3,
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
