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

  // Print Dialog Modal (matching vbYesNo "Do you want to print ?" and subPrintTransferOut)
  const [printModalVisible, setPrintModalVisible] = useState(false);
  const [printedTransferNo, setPrintedTransferNo] = useState('');

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
        setPrintedTransferNo(assignedNo);

        // Matching VB.NET fncUpdateTransfer:
        // If MsgBox("Do you want to print ?", vbYesNo + MsgBoxStyle.Information) = MsgBoxResult.Yes Then subPrintTransferOut(Trim(txtTransferNo.Text))
        setTimeout(() => {
          if (Platform.OS === 'web' && typeof window !== 'undefined') {
            if (window.confirm('Do you want to print ?')) {
              handlePrint(assignedNo, true);
            }
          }
        }, 500);
      } else {
        showToast(data.message || 'Failed to complete transfer', 'error');
      }
    } catch (err: any) {
      showToast('Network error executing transfer', 'error');
    } finally {
      setSaving(false);
    }
  };

  const handlePrint = async (targetTransferNo?: string, alreadyConfirmed: boolean = false) => {
    const tNo = (targetTransferNo || transferNo || printedTransferNo || '').trim();
    if (!tNo) {
      showToast('Please select or enter a Transfer Number to print.', 'error');
      return;
    }

    // VB validation:
    // If txtTransferNo.Text.Trim <> "" Then
    //    If MsgBox("Do you want to print ?", vbYesNo + MsgBoxStyle.Information) = MsgBoxResult.Yes Then
    //        subPrintTransferOut(Trim(txtTransferNo.Text))
    //    End If
    // End If
    if (!alreadyConfirmed) {
      const confirmed = Platform.OS === 'web' && typeof window !== 'undefined'
        ? window.confirm('Do you want to print ?')
        : true;
      if (!confirmed) return;
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

      // 2. If dedicated route is not available, query /fetch and /company
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

      // Generate dynamic Singapore Date and Time (Asia/Singapore)
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
                padding: 10mm 12mm 10mm 12mm;
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
                <div class="report-sub-info">BRANCH STOCK TRANSFER OUT</div>
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
        showToast('Printing Transfer Note for: ' + tNo, 'success');
      }
    } catch (err: any) {
      console.error('Print Error:', err);
      showToast('Error generating print preview: ' + err.message, 'error');
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
            onPress={() => handlePrint()}
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

      {/* DISPATCH NOTE PRINT PREVIEW MODAL (matching subPrintTransferOut in frmTransferOut.vb) */}
      <Modal visible={printModalVisible} transparent animationType="fade" onRequestClose={() => setPrintModalVisible(false)}>
        <View style={styles.modalOverlay}>
          <View style={[styles.lovModalCard, { maxWidth: 640 }]}>
            <View style={styles.modalHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="print-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.modalHeaderTitle}>Transfer Out Note Preview</ThemedText>
              </View>
              <TouchableOpacity
                onPress={() => setPrintModalVisible(false)}
                activeOpacity={0.7}
                style={styles.closeModalBtn}
              >
                <Ionicons name="close" size={18} color="#3B2D17" />
              </TouchableOpacity>
            </View>

            <ScrollView style={{ padding: 16, maxHeight: 420 }}>
              <View style={{ alignItems: 'center', marginBottom: 12, borderBottomWidth: 1, borderBottomColor: '#E8DCB8', paddingBottom: 10 }}>
                <ThemedText style={{ fontSize: 16, fontWeight: 'bold', color: '#3B2D17' }}>SMART GOLD POS</ThemedText>
                <ThemedText style={{ fontSize: 13, fontWeight: 'bold', color: '#8D6409', marginTop: 2 }}>TRANSFER NOTE</ThemedText>
                <ThemedText style={{ fontSize: 11, color: '#7D5E19' }}>Transfer Voucher #{printedTransferNo || transferNo}</ThemedText>
              </View>

              <View style={{ flexDirection: 'row', justifyContent: 'space-between', marginBottom: 10 }}>
                <View>
                  <ThemedText style={{ fontSize: 11, color: '#5C4A28' }}>
                    <ThemedText style={{ fontWeight: 'bold' }}>From Location: </ThemedText>{fromLocation}
                  </ThemedText>
                  <ThemedText style={{ fontSize: 11, color: '#5C4A28' }}>
                    <ThemedText style={{ fontWeight: 'bold' }}>To Location: </ThemedText>{toLocation}
                  </ThemedText>
                </View>
                <View style={{ alignItems: 'flex-end' }}>
                  <ThemedText style={{ fontSize: 11, color: '#5C4A28' }}>
                    <ThemedText style={{ fontWeight: 'bold' }}>Date: </ThemedText>{transferDate}
                  </ThemedText>
                  <ThemedText style={{ fontSize: 11, color: '#5C4A28' }}>
                    <ThemedText style={{ fontWeight: 'bold' }}>Salesman: </ThemedText>{verifiedSalesman?.code || 'ADMIN'}
                  </ThemedText>
                </View>
              </View>

              {/* Items Summary Table */}
              <View style={[styles.tableHeadRow, { backgroundColor: '#F0E6D2' }]}>
                <ThemedText style={[styles.tableHeadCell, { flex: 1.5 }]}>Item Code</ThemedText>
                <ThemedText style={[styles.tableHeadCell, { flex: 2.5 }]}>Description</ThemedText>
                <ThemedText style={[styles.tableHeadCell, { flex: 1, textAlign: 'right' }]}>Weight</ThemedText>
                <ThemedText style={[styles.tableHeadCell, { flex: 0.8, textAlign: 'right' }]}>Qty</ThemedText>
                <ThemedText style={[styles.tableHeadCell, { flex: 1.2, textAlign: 'right' }]}>Total ($)</ThemedText>
              </View>
              {gridData.map((item, idx) => (
                <View key={idx} style={[styles.tableRow, { paddingVertical: 4 }]}>
                  <ThemedText style={[styles.tableCell, styles.codeCell, { flex: 1.5 }]}>{item.InventoryCode}</ThemedText>
                  <ThemedText style={[styles.tableCell, { flex: 2.5 }]} numberOfLines={1}>{item.Description}</ThemedText>
                  <ThemedText style={[styles.tableCell, { flex: 1, textAlign: 'right' }]}>{Number(item.Weight || 0).toFixed(3)}</ThemedText>
                  <ThemedText style={[styles.tableCell, { flex: 0.8, textAlign: 'right' }]}>{item.Pcs || item.QtyonHand || 1}</ThemedText>
                  <ThemedText style={[styles.tableCell, styles.amountCell, { flex: 1.2, textAlign: 'right' }]}>{Number(item.TotalCost || 0).toFixed(2)}</ThemedText>
                </View>
              ))}

              {/* Totals Summary */}
              <View style={{ marginTop: 12, borderTopWidth: 1, borderTopColor: '#E8DCB8', paddingTop: 8, flexDirection: 'row', justifyContent: 'space-between' }}>
                <ThemedText style={{ fontSize: 11, fontWeight: 'bold', color: '#3B2D17' }}>
                  Total Items: {gridData.length} | Total PCS: {totals.PCS}
                </ThemedText>
                <View style={{ alignItems: 'flex-end' }}>
                  <ThemedText style={{ fontSize: 11, fontWeight: 'bold', color: '#3B2D17' }}>
                    Total Weight: {totals.Weight.toFixed(3)} g
                  </ThemedText>
                  <ThemedText style={{ fontSize: 12, fontWeight: 'bold', color: '#8A5D00', marginTop: 2 }}>
                    Total Amount: ${totals.TotalCost.toFixed(2)}
                  </ThemedText>
                </View>
              </View>
            </ScrollView>

            <View style={{ padding: 12, borderTopWidth: 1, borderTopColor: '#E8DCB8', backgroundColor: '#FBF9F5', flexDirection: 'row', justifyContent: 'flex-end', gap: 10 }}>
              <TouchableOpacity
                style={[styles.actionBtn, styles.clearBtn]}
                onPress={() => setPrintModalVisible(false)}
                activeOpacity={0.7}
              >
                <ThemedText style={styles.clearBtnText}>Close</ThemedText>
              </TouchableOpacity>

              <TouchableOpacity
                style={[styles.actionBtn, styles.saveBtn]}
                onPress={() => {
                  setPrintModalVisible(false);
                  showToast('Dispatch Note sent to printer', 'success');
                }}
                activeOpacity={0.8}
              >
                <Ionicons name="print-outline" size={15} color="#9E6D08" style={{ marginRight: 4 }} />
                <ThemedText style={styles.saveBtnText}>Send to Printer</ThemedText>
              </TouchableOpacity>
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
