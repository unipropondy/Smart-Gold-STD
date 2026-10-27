import { ThemedText } from '@/components/themed-text';
import { API_BASE_URL } from '@/constants/api';
import { useThemeContext } from '@/contexts/ThemeContext';
import { useThemeColors } from '@/hooks/useThemeColors';
import { Ionicons } from '@expo/vector-icons';
import { Stack, useRouter } from 'expo-router';
import React, { useState } from 'react';
import {
  Alert,
  FlatList,
  Modal,
  Platform,
  ScrollView,
  StyleSheet,
  TextInput,
  TouchableOpacity,
  View,
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';

interface DetailItem {
  JobSheetNo?: string;
  CatalogueCode: string;
  CategoryCode: string;
  Description: string;
  Weight: string;
  CostPerGram: string;
  MC: string;
  PCS: string;
  GoldLoss: string;
  TotalCost: string;
}

const API_URL = `${API_BASE_URL}/jobsheet`;

export default function JobSheetScreen() {
  const themeColors = useThemeColors();
  const { accentColor } = useThemeContext();
  const { background: backgroundColor, card: cardColor, text: textColor, border: borderColor, primaryGold: GOLD_COLOR, subtitle: subtitleColor } = themeColors;
  const router = useRouter();

  // Header State perfectly matching the tblJobSheetHeader schema
  const [headerState, setHeaderState] = useState({
    JobSheetNo: '', JobSheetDate: '', PurchaseBillNo: '', PurchaseBillDate: '', SupplierCode: '', SupplierName: '',
    TotalWeight24K: '', TotalMC: '', TotalPCS: '', TotalGoldLoss: '', CostPerGram: '',
    CurrencyCode: '', CurrencyName: '', CurrencyRate: '', GST: 22, TotalCost: '', PaymentType: 'C',
    BillClose: false, Remarks: '', TranType: '', TotalLocalCost: '', TotalForeignCost: '',
    ManualLocalCost: '', ManualForeignCost: '', ManualMC: '', ManualWeight: '', ManualGoldLoss: '',
    LocationCode: '', TaelGST: false, ManualAdjustment: '', FixStatus: 'U', SplCategory: false,
    Accessories: false, MatchJobSheetNo: '', MatchWeight: '', MatchPCS: '', SalesOrderNo: '',
    TaelPercent: '', TaelRate: '', ClassType: '', PaidAmount: '', BalanceAmount: '', DueDate: '',
    GSTAmountDisplay: '' // display only
  });

  // Detail State
  const [details, setDetails] = useState<DetailItem[]>([]);
  const [currentDetail, setCurrentDetail] = useState<DetailItem>({
    CatalogueCode: '', CategoryCode: '', Description: '', Weight: '', CostPerGram: '', MC: '', PCS: '', GoldLoss: '', TotalCost: ''
  });

  // JobSheet LOV
  const [lovVisible, setLovVisible] = useState(false);
  const [lovData, setLovData] = useState<any[]>([]);

  // Supplier LOV
  const [supplierLovVisible, setSupplierLovVisible] = useState(false);
  const [supplierLovData, setSupplierLovData] = useState<any[]>([]);

  // Currency LOV
  const [currencyLovVisible, setCurrencyLovVisible] = useState(false);
  const [currencyLovData, setCurrencyLovData] = useState<any[]>([]);

  // Catalogue LOV
  const [catalogueLovVisible, setCatalogueLovVisible] = useState(false);
  const [catalogueLovData, setCatalogueLovData] = useState<any[]>([]);

  // TaelPercent LOV
  const [taelPercentLovVisible, setTaelPercentLovVisible] = useState(false);
  const [taelPercentLovData, setTaelPercentLovData] = useState<any[]>([]);

  const [selectedIndex, setSelectedIndex] = useState<number | null>(null);
  const [detailModalVisible, setDetailModalVisible] = useState(false);
  const [toast, setToast] = useState<{ message: string, type: 'success' | 'error' } | null>(null);

  const showToast = (message: string, type: 'success' | 'error' = 'success') => {
    setToast({ message, type });
    setTimeout(() => {
      setToast(null);
    }, 3000);
  };

  const [chkGST1, setChkGST1] = useState(false);
  const [companyGstPerc, setCompanyGstPerc] = useState(9); // Default Singapore 9% GST

  // Fetch company GST rate from company or misc config
  React.useEffect(() => {
    const fetchCompanyGst = async () => {
      try {
        const res = await fetch(`${API_BASE_URL}/company`);
        const data = await res.json();
        if (data.success && data.data && data.data.length > 0) {
          const compGst = parseFloat(data.data[0].GstPerc || data.data[0].GSTPerc || 0);
          if (compGst > 0) {
            setCompanyGstPerc(compGst);
          }
        }
      } catch (e) {
        console.log('Error fetching company GST rate:', e);
      }
    };
    fetchCompanyGst();
  }, []);

  // 1. First GST Checkbox (ChkGST1) Calculation Logic
  // VB Reference:
  // If Val(cboTealPercent.Text) <> 0 Then
  //     If ChkGST1.Checked = True And opt22K.Checked = True Then
  //         txtCostPerGram.Text = Format((Val(txtTAEL.Text) * 0.02646 * Val(cboTealPercent.Text) * Val(GstTaelPercentage)), "0.00")
  //     ElseIf ChkGST1.Checked = True And opt24K.Checked = True Then
  //         txtCostPerGram.Text = Format((Val(txtTAEL.Text) * 0.02646), "0.00")
  //     ElseIf ChkGST1.Checked = False And opt24K.Checked = True Then
  //         txtCostPerGram.Text = Format((Val(txtTAEL.Text) * 0.02646), "0.00")
  //     Else
  //         txtCostPerGram.Text = Format((Val(txtTAEL.Text) * 0.02646 * Val(cboTealPercent.Text)), "0.00")
  //     End If
  // Else
  //     If ChkGST1.Checked = True And opt22K.Checked = True Then
  //         txtCostPerGram.Text = Format((Val(txtTAEL.Text) * 0.02646 * 0.92 * Val(GstTaelPercentage)), "0.00")
  //     ElseIf ChkGST1.Checked = True And opt24K.Checked = True Then
  //         txtCostPerGram.Text = Format((Val(txtTAEL.Text) * 0.02646), "0.00")
  //     ElseIf ChkGST1.Checked = False And opt24K.Checked = True Then
  //         txtCostPerGram.Text = Format((Val(txtTAEL.Text) * 0.02646), "0.00")
  //     Else
  //         txtCostPerGram.Text = Format((Val(txtTAEL.Text) * 0.02646 * 0.92), "0.00")
  //     End If
  // End If
  const calculateCostPerGram = (
    isChkGST1: boolean,
    targetHeaderState?: typeof headerState
  ) => {
    const currentState = targetHeaderState || headerState;
    const taelVal = parseFloat(currentState.TaelRate) || 0;
    const tealPercent = parseFloat(currentState.TaelPercent) || 0;
    const is22K = Number(currentState.GST) === 22;
    const is24K = Number(currentState.GST) === 24;
    const gstTaelPercentage = 1 + (companyGstPerc / 100);

    let calculatedCost = 0;

    if (tealPercent !== 0) {
      if (isChkGST1 && is22K) {
        calculatedCost = taelVal * 0.02646 * tealPercent * gstTaelPercentage;
      } else if (isChkGST1 && is24K) {
        calculatedCost = taelVal * 0.02646;
      } else if (!isChkGST1 && is24K) {
        calculatedCost = taelVal * 0.02646;
      } else {
        calculatedCost = taelVal * 0.02646 * tealPercent;
      }
    } else {
      if (isChkGST1 && is22K) {
        calculatedCost = taelVal * 0.02646 * 0.92 * gstTaelPercentage;
      } else if (isChkGST1 && is24K) {
        calculatedCost = taelVal * 0.02646;
      } else if (!isChkGST1 && is24K) {
        calculatedCost = taelVal * 0.02646;
      } else {
        calculatedCost = taelVal * 0.02646 * 0.92;
      }
    }

    return calculatedCost > 0 ? calculatedCost.toFixed(2) : (currentState.CostPerGram || '');
  };

  const calculateTaelFromCost = (
    isChkGST1: boolean,
    targetHeaderState?: typeof headerState
  ) => {
    const currentState = targetHeaderState || headerState;
    const costVal = parseFloat(currentState.CostPerGram) || 0;
    if (costVal <= 0) return currentState.TaelRate || '';

    const tealPercent = parseFloat(currentState.TaelPercent) || 0;
    const is22K = Number(currentState.GST) === 22;
    const is24K = Number(currentState.GST) === 24;
    const gstTaelPercentage = 1 + (companyGstPerc / 100);

    let calculatedTael = 0;
    if (tealPercent !== 0) {
      if (isChkGST1 && is22K) {
        calculatedTael = costVal / (0.02646 * tealPercent * gstTaelPercentage);
      } else if (is24K) {
        calculatedTael = costVal / 0.02646;
      } else {
        calculatedTael = costVal / (0.02646 * tealPercent);
      }
    } else {
      if (isChkGST1 && is22K) {
        calculatedTael = costVal / (0.02646 * 0.92 * gstTaelPercentage);
      } else if (is24K) {
        calculatedTael = costVal / 0.02646;
      } else {
        calculatedTael = costVal / (0.02646 * 0.92);
      }
    }

    return calculatedTael > 0 ? Math.round(calculatedTael).toString() : (currentState.TaelRate || '');
  };

  const handleChkGST1 = (checked: boolean) => {
    setChkGST1(checked);
    setHeaderState(prev => {
      const tael = parseFloat(prev.TaelRate) || 0;
      const cost = parseFloat(prev.CostPerGram) || 0;

      if (tael > 0) {
        const updatedCost = calculateCostPerGram(checked, prev);
        return { ...prev, CostPerGram: updatedCost };
      } else if (cost > 0) {
        const updatedTael = calculateTaelFromCost(checked, prev);
        return { ...prev, TaelRate: updatedTael };
      }
      return prev;
    });
  };

  // 2. Second GST Checkbox (ChkGST in Cost Summary) Calculation Logic
  // VB Reference:
  // If ChkGST.Checked = True Then
  //     If OptCash.Checked = True Then
  //         txtGST.Text = Format((Val(Objuser.GstPercentage) * (Val(txtGoldValueMC.Text) - Val(txtPayableCash.Text))) / 100, "#0.00")
  //         txtManualCost.Text = Format(Val(txtGoldValueMC.Text) + Val(txtGST.Text.Trim), "#0.00")
  //     ElseIf OptGoldAC.Checked Then
  //         txtGST.Text = Format((Val(Objuser.GstPercentage) * (Val(txtGoldValueMC.Text) - Val(txtPayableCash.Text))) / 100, "#0.00")
  //         txtManualCost.Text = Format(Val(txtGoldValueMC.Text) + Val(txtGST.Text.Trim), "#0.00")
  //     Else
  //         txtGST.Text = Format((Val(Objuser.GstPercentage) * (Val(txtGoldValueMC.Text) - Val(txtPayableCash.Text))) / 100, "#0.00")
  //         txtManualCost.Text = Format(Val(txtGoldValueMC.Text) + Val(txtGST.Text.Trim), "#0.00")
  //     End If
  // Else
  //     txtManualCost.Text = Format(Val(txtManualCost.Text.Trim) - Val(txtGST.Text.Trim), "#0.00")
  //     txtGST.Text = "0.00"
  // End If
  const handleChkGST = (checked: boolean) => {
    setHeaderState(prev => {
      const totalCostVal = details.reduce((sum, item) => sum + (parseFloat(item.TotalCost) || 0), 0);
      const totalMC = details.reduce((sum, item) => sum + (parseFloat(item.MC) || 0), 0);
      const goldValue = totalCostVal - totalMC;
      const adjustment = parseFloat(prev.ManualAdjustment) || 0;
      const goldValueMC = goldValue + totalMC + adjustment;

      let gstText = "0.00";
      let manualCost = "0.00";

      if (checked) {
        const payableCash = parseFloat(prev.PaidAmount) || 0;
        const gstVal = (companyGstPerc * (goldValueMC - payableCash)) / 100;
        gstText = (gstVal > 0 ? gstVal : 0).toFixed(2);
        manualCost = (goldValueMC + (gstVal > 0 ? gstVal : 0)).toFixed(2);
      } else {
        gstText = "0.00";
        manualCost = goldValueMC.toFixed(2);
      }

      return {
        ...prev,
        TaelGST: checked,
        GSTAmountDisplay: gstText,
        TotalCost: manualCost,
        TotalLocalCost: goldValue.toFixed(2),
        TotalMC: totalMC.toFixed(2)
      };
    });
  };

  const updateHeader = (key: string, value: any) => {
    setHeaderState(prev => {
      const next = { ...prev, [key]: value };

      // Recalculate CostPerGram or TaelRate if inputs change
      if (key === 'TaelRate') {
        if (parseFloat(next.TaelRate) > 0) {
          next.CostPerGram = calculateCostPerGram(chkGST1, next);
        }
      } else if (key === 'CostPerGram') {
        if (parseFloat(next.CostPerGram) > 0) {
          next.TaelRate = calculateTaelFromCost(chkGST1, next);
        }
      } else if (key === 'TaelPercent' || key === 'GST') {
        if (parseFloat(next.TaelRate) > 0) {
          next.CostPerGram = calculateCostPerGram(chkGST1, next);
        } else if (parseFloat(next.CostPerGram) > 0) {
          next.TaelRate = calculateTaelFromCost(chkGST1, next);
        }
      }

      // Calculate totals based on latest state and details
      const totalWeight = details.reduce((sum, item) => sum + (parseFloat(item.Weight) || 0), 0);
      const totalMC = details.reduce((sum, item) => sum + (parseFloat(item.MC) || 0), 0);
      const totalCostVal = details.reduce((sum, item) => sum + (parseFloat(item.TotalCost) || 0), 0);

      const goldValue = totalCostVal - totalMC;
      const adjustment = parseFloat(next.ManualAdjustment) || 0;
      const goldValueMC = goldValue + totalMC + adjustment;

      let gstAmount = 0;
      if (next.TaelGST) {
        const payableCash = parseFloat(next.PaidAmount) || 0;
        gstAmount = (companyGstPerc * (goldValueMC - payableCash)) / 100;
        if (gstAmount < 0) gstAmount = 0;
      }

      const finalTotalCost = goldValueMC + gstAmount;

      next.TotalLocalCost = goldValue.toFixed(2);
      next.TotalMC = totalMC.toFixed(2);
      next.TotalCost = finalTotalCost.toFixed(2);
      next.GSTAmountDisplay = gstAmount.toFixed(2);

      return next;
    });
  };

  // Recalculate totals whenever details list changes or companyGstPerc changes
  React.useEffect(() => {
    setHeaderState(prev => {
      const totalWeight = details.reduce((sum, item) => sum + (parseFloat(item.Weight) || 0), 0);
      const totalMC = details.reduce((sum, item) => sum + (parseFloat(item.MC) || 0), 0);
      const totalCostVal = details.reduce((sum, item) => sum + (parseFloat(item.TotalCost) || 0), 0);
      const totalPCS = details.reduce((sum, item) => sum + (parseInt(item.PCS) || 0), 0);

      const goldValue = totalCostVal - totalMC;
      const adjustment = parseFloat(prev.ManualAdjustment) || 0;
      const goldValueMC = goldValue + totalMC + adjustment;

      let gstAmount = 0;
      if (prev.TaelGST) {
        const payableCash = parseFloat(prev.PaidAmount) || 0;
        gstAmount = (companyGstPerc * (goldValueMC - payableCash)) / 100;
        if (gstAmount < 0) gstAmount = 0;
      }

      const finalTotalCost = goldValueMC + gstAmount;

      return {
        ...prev,
        TotalWeight24K: totalWeight.toFixed(3),
        TotalMC: totalMC.toFixed(2),
        TotalPCS: totalPCS.toString(),
        TotalLocalCost: goldValue.toFixed(2),
        TotalCost: finalTotalCost.toFixed(2),
        GSTAmountDisplay: gstAmount.toFixed(2)
      };
    });
  }, [details, companyGstPerc]);

  const updateDetail = (key: string, value: any) => {
    setCurrentDetail(prev => {
      const next = { ...prev, [key]: value };
      if (['Weight', 'CostPerGram', 'MC', 'PCS'].includes(key)) {
        const w = parseFloat(next.Weight) || 0;
        const c = parseFloat(next.CostPerGram) || 0;
        const mc = parseFloat(next.MC) || 0;
        const pcs = parseFloat(next.PCS) || 1;
        const total = (w * c) + (mc * pcs);
        next.TotalCost = total > 0 ? total.toFixed(2) : '';
      }
      return next;
    });
  };

  const addDetail = async () => {
    if (!currentDetail.CatalogueCode?.trim()) {
      showToast('Catalogue Code is required', 'error');
      return;
    }
    if (!currentDetail.CategoryCode?.trim()) {
      showToast('Category Code is required', 'error');
      return;
    }
    const weight = parseFloat(currentDetail.Weight) || 0;
    if (weight <= 0) {
      showToast('Weight is required and must be greater than zero', 'error');
      return;
    }
    const costPerGram = parseFloat(currentDetail.CostPerGram) || 0;
    if (costPerGram <= 0) {
      showToast('Cost Per Gram is required and must be greater than zero', 'error');
      return;
    }
    const pcs = parseInt(currentDetail.PCS) || 0;
    if (pcs <= 0) {
      showToast('PCS is required and must be greater than zero', 'error');
      return;
    }

    let activeJobSheetNo = headerState.JobSheetNo?.trim();
    if (!activeJobSheetNo) {
      activeJobSheetNo = await generateJobSheetNo();
      if (!activeJobSheetNo) {
        showToast('Could not generate JobSheet No', 'error');
        return;
      }
    }

    const detailToSave = { ...currentDetail, JobSheetNo: activeJobSheetNo };
    let newDetails = [];
    if (selectedIndex !== null) {
      newDetails = [...details];
      newDetails[selectedIndex] = detailToSave;
    } else {
      newDetails = [...details, detailToSave];
    }
    setDetails(newDetails);
    setSelectedIndex(null);
    setCurrentDetail({ CatalogueCode: '', CategoryCode: '', Description: '', Weight: '', CostPerGram: '', MC: '', PCS: '', GoldLoss: '', TotalCost: '' });
    await saveJobSheet(newDetails);
    setDetailModalVisible(false);
  };

  const deleteSelectedDetail = async () => {
    if (selectedIndex !== null) {
      const newDetails = details.filter((_, i) => i !== selectedIndex);
      setDetails(newDetails);
      setSelectedIndex(null);
      setCurrentDetail({ CatalogueCode: '', CategoryCode: '', Description: '', Weight: '', CostPerGram: '', MC: '', PCS: '', GoldLoss: '', TotalCost: '' });
      await saveJobSheet(newDetails);
      showToast('Line detail deleted successfully', 'success');
      setDetailModalVisible(false);
    } else {
      showToast('Please select a detail to delete', 'error');
    }
  };

  const clearForm = () => {
    setChkGST1(false);
    setHeaderState({
      JobSheetNo: '', JobSheetDate: '', PurchaseBillNo: '', PurchaseBillDate: '', SupplierCode: '', SupplierName: '',
      TotalWeight24K: '', TotalMC: '', TotalPCS: '', TotalGoldLoss: '', CostPerGram: '',
      CurrencyCode: '', CurrencyName: '', CurrencyRate: '', GST: 22, TotalCost: '', PaymentType: 'C',
      BillClose: false, Remarks: '', TranType: '', TotalLocalCost: '', TotalForeignCost: '',
      ManualLocalCost: '', ManualForeignCost: '', ManualMC: '', ManualWeight: '', ManualGoldLoss: '',
      LocationCode: '', TaelGST: false, ManualAdjustment: '', FixStatus: 'U', SplCategory: false,
      Accessories: false, MatchJobSheetNo: '', MatchWeight: '', MatchPCS: '', SalesOrderNo: '',
      TaelPercent: '', TaelRate: '', ClassType: '', PaidAmount: '', BalanceAmount: '', DueDate: '', GSTAmountDisplay: ''
    });
    setDetails([]);
    setSelectedIndex(null);
    setCurrentDetail({ CatalogueCode: '', CategoryCode: '', Description: '', Weight: '', CostPerGram: '', MC: '', PCS: '', GoldLoss: '', TotalCost: '' });
    generateJobSheetNo();
  };

  const openLov = async () => {
    try {
      console.log('Fetching LOV from', API_URL);
      const res = await fetch(API_URL);
      const data = await res.json();
      console.log('LOV Data received:', data);
      if (data.success) {
        setLovData(data.data);
        setLovVisible(true);
      } else {
        alert('Error: ' + (data.message || 'Failed to load list'));
      }
    } catch (err) {
      console.error('LOV Fetch Error:', err);
      alert('Error fetching JobSheet LOV');
    }
  };

  const openSupplierLov = async () => {
    try {
      const res = await fetch(`${API_BASE_URL}/supplier`);
      const data = await res.json();
      if (data.success) {
        setSupplierLovData(data.data);
        setSupplierLovVisible(true);
      } else {
        alert(data.message || 'Failed to fetch suppliers');
      }
    } catch (err) {
      console.error(err);
      alert('Error fetching Supplier LOV');
    }
  };

  const openCurrencyLov = async () => {
    try {
      const res = await fetch(`${API_BASE_URL}/currency`);
      const data = await res.json();
      if (data.success) {
        setCurrencyLovData(data.data);
        setCurrencyLovVisible(true);
      } else {
        alert(data.message || 'Failed to fetch currencies');
      }
    } catch (err) {
      console.error(err);
      alert('Error fetching Currency LOV');
    }
  };

  const openCatalogueLov = async () => {
    try {
      const res = await fetch(`${API_BASE_URL}/catalogue`);
      const data = await res.json();
      if (data.success) {
        setCatalogueLovData(data.data);
        setCatalogueLovVisible(true);
      } else {
        alert(data.message || 'Failed to fetch catalogue');
      }
    } catch (err) {
      console.error(err);
      alert('Error fetching Catalogue LOV');
    }
  };

  const openTaelPercentLov = async () => {
    try {
      const res = await fetch(`${API_BASE_URL}/jobsheet/lov/taelpercent`);
      const data = await res.json();
      if (data.success) {
        setTaelPercentLovData(data.data);
        setTaelPercentLovVisible(true);
      } else {
        alert(data.message || 'Failed to fetch tael percent data');
      }
    } catch (err) {
      console.error(err);
      alert('Error fetching TaelPercent LOV');
    }
  };

  const formatDateToDDMMYYYY = (isoString: string) => {
    if (!isoString) return '';
    const d = new Date(isoString);
    if (isNaN(d.getTime())) return isoString;
    return `${d.getDate().toString().padStart(2, '0')}-${(d.getMonth() + 1).toString().padStart(2, '0')}-${d.getFullYear()}`;
  };

  const generateJobSheetNo = async () => {
    try {
      const res = await fetch(`${API_URL}/nextNo`);
      const data = await res.json();
      if (data.success && data.nextNo) {
        updateHeader('JobSheetNo', data.nextNo);
        return data.nextNo;
      }
    } catch (err) {
      console.error('Error generating JobSheet No:', err);
    }
    return '';
  };

  React.useEffect(() => {
    generateJobSheetNo();
  }, []);

  const loadJobSheet = async (jobSheetNo: string) => {
    const noToLoad = jobSheetNo || headerState.JobSheetNo;
    if (!noToLoad) {
      showToast('Please enter a JobSheet No to load', 'error');
      return;
    }
    try {
      const res = await fetch(`${API_URL}/${noToLoad}`);
      const data = await res.json();
      if (data.success) {
        const h = data.header;
        setHeaderState({
          ...headerState,
          ...h,
          JobSheetDate: formatDateToDDMMYYYY(h.JobSheetDate),
          PurchaseBillDate: formatDateToDDMMYYYY(h.PurchaseBillDate),
          DueDate: formatDateToDDMMYYYY(h.DueDate),
          PurchaseBillNo: h.PurchaseBillNo || '',
          SupplierCode: h.SupplierCode || '',
          SupplierName: h.SupplierName || '',
          CurrencyName: h.CurrencyName || '',
          TaelGST: h.TaelGST === 'Y',
          SplCategory: h.SplCategory === 'Y',
          Accessories: h.Accessories === 'Y',
          BillClose: h.BillClose === 'Y',
          FixStatus: h.FixStatus || 'U',
          TaelPercent: h.TaelPercent === 0 ? '' : (h.TaelPercent?.toString() || '')
        });
        setDetails(data.details || []);
        showToast('JobSheet loaded successfully');
      } else {
        showToast(data.message || 'JobSheet not found', 'error');
      }
    } catch (err) {
      showToast('Failed to load JobSheet', 'error');
    }
  };

  const parseDDMMYYYY = (dateStr: string) => {
    if (!dateStr) return null;
    const parts = dateStr.split('-');
    if (parts.length === 3) {
      return `${parts[2]}-${parts[1]}-${parts[0]}`; // Convert to YYYY-MM-DD
    }
    return dateStr;
  };

  const fncCheckValidity = (activeDetails: DetailItem[]) => {
    if (!headerState.PurchaseBillNo?.trim()) {
      showToast('Invoice no. (Purchase Bill No) cannot be empty!', 'error');
      return false;
    }
    if (!headerState.SupplierCode?.trim()) {
      showToast('Supplier Code cannot be empty!', 'error');
      return false;
    }
    if (!headerState.CurrencyCode?.trim()) {
      showToast('Currency Code cannot be blank!', 'error');
      return false;
    }
    const rate = parseFloat(headerState.CurrencyRate) || 0;
    if (rate <= 0) {
      showToast('Currency Rate must be greater than zero!', 'error');
      return false;
    }
    const costPerGram = parseFloat(headerState.CostPerGram) || 0;
    if (!headerState.CostPerGram?.toString().trim() || costPerGram <= 0) {
      showToast('Cost/Gram cannot be empty and must be greater than zero!', 'error');
      return false;
    }
    if (activeDetails.length < 1) {
      showToast('Please enter at least one line detail!', 'error');
      return false;
    }
    return true;
  };

  const saveJobSheet = async (overrideDetails?: DetailItem[]) => {
    const activeDetails = overrideDetails || details;
    if (!fncCheckValidity(activeDetails)) {
      return;
    }

    let activeJobSheetNo = headerState.JobSheetNo?.trim();
    let isNew = false;
    if (!activeJobSheetNo) {
      isNew = true;
      activeJobSheetNo = await generateJobSheetNo();
      if (!activeJobSheetNo) {
        showToast('Could not generate JobSheet No', 'error');
        return;
      }
    }

    try {
      const payloadToSave = {
        ...headerState,
        JobSheetNo: activeJobSheetNo,
        JobSheetDate: parseDDMMYYYY(headerState.JobSheetDate) || new Date().toISOString().split('T')[0],
        PurchaseBillDate: parseDDMMYYYY(headerState.PurchaseBillDate) || new Date().toISOString().split('T')[0],
        DueDate: parseDDMMYYYY(headerState.DueDate)
      };

      const payload = {
        header: payloadToSave,
        details: overrideDetails || details,
        userId: 'ADMIN'
      };

      const res = await fetch(API_URL, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload)
      });
      const data = await res.json();
      if (data.success) {
        showToast(`JobSheet saved successfully! Number: ${activeJobSheetNo}`);
        if (isNew) {
          clearForm();
        } else {
          loadJobSheet(activeJobSheetNo);
        }
      } else {
        showToast('Save failed: ' + data.message, 'error');
      }
    } catch (err) {
      showToast('Failed to save JobSheet', 'error');
    }
  };

  const deleteJobSheet = async () => {
    if (!headerState.JobSheetNo) {
      showToast('JobSheet No is required', 'error');
      return;
    }
    Alert.alert('Confirm Delete', `Are you sure you want to delete JobSheet ${headerState.JobSheetNo}?`, [
      { text: 'Cancel', style: 'cancel' },
      {
        text: 'Delete', style: 'destructive', onPress: async () => {
          try {
            const res = await fetch(`${API_URL}/${headerState.JobSheetNo}`, { method: 'DELETE' });
            const data = await res.json();
            if (data.success) {
              showToast('JobSheet deleted successfully');
              clearForm();
            } else {
              showToast('Delete failed: ' + data.message, 'error');
            }
          } catch (err) {
            showToast('Failed to delete JobSheet', 'error');
          }
        }
      }
    ]);
  };

  const inputStyle = [styles.input, { borderColor: borderColor, color: textColor, backgroundColor: backgroundColor }];
  const readonlyInputStyle = [styles.input, styles.readonlyInput, { borderColor: borderColor, color: textColor }];

  const renderField = (label: string, key: string, value: string, isReadOnly = false, isNumeric = false, width: any = '100%', isDate = false) => {
    let dateVal = '';
    if (isDate && value) {
      const parts = value.split('-');
      if (parts.length === 3 && parts[2].length === 4) {
        dateVal = `${parts[2]}-${parts[1]}-${parts[0]}`;
      }
    }

    return (
      <View style={[styles.fieldContainer, { width }]}>
        <ThemedText style={styles.label}>{label}</ThemedText>
        {isDate && Platform.OS === 'web' ? (
          <input
            type="date"
            // @ts-ignore
            style={{ ...StyleSheet.flatten(isReadOnly ? readonlyInputStyle : inputStyle), outline: 'none', width: '100%', boxSizing: 'border-box' }}
            value={dateVal}
            onChange={(e: any) => {
              const val = e.target.value;
              if (val) {
                const [y, m, d] = val.split('-');
                updateHeader(key, `${d}-${m}-${y}`);
              } else {
                updateHeader(key, '');
              }
            }}
            disabled={isReadOnly}
          />
        ) : (
          <TextInput
            style={isReadOnly ? readonlyInputStyle : inputStyle}
            value={value}
            onChangeText={(text) => updateHeader(key, text)}
            editable={!isReadOnly}
            keyboardType={isNumeric ? 'numeric' : 'default'}
            placeholder={isDate ? 'DD-MM-YYYY' : undefined}
          />
        )}
      </View>
    );
  };

  const renderLookupField = (label: string, key: string, value: string, width: any = '31%', secondaryValue?: string) => (
    <View style={[styles.fieldContainer, { width }]}>
      <ThemedText style={[styles.label, { color: subtitleColor }]}>{label}</ThemedText>
      <View style={{ flexDirection: 'row' }}>
        <TextInput
          style={[inputStyle, { flex: 1, borderTopRightRadius: 0, borderBottomRightRadius: 0 }]}
          value={value}
          onChangeText={(text) => updateHeader(key, text)}
          onBlur={() => {
            if (key === 'JobSheetNo' && value.trim() !== '') {
              loadJobSheet(value);
            }
          }}
          onSubmitEditing={() => {
            if (key === 'JobSheetNo' && value.trim() !== '') {
              loadJobSheet(value);
            }
          }}
        />
        <TouchableOpacity
          style={[styles.lookupBtn, { backgroundColor: backgroundColor, borderColor: borderColor }]}
          onPress={() => {
            if (key === 'JobSheetNo') openLov();
            if (key === 'SupplierCode') openSupplierLov();
            if (key === 'CurrencyCode') openCurrencyLov();
            if (key === 'TaelPercent') openTaelPercentLov();
          }}
        >
          <ThemedText style={{ color: textColor, fontWeight: 'bold' }}>...</ThemedText>
        </TouchableOpacity>
        {secondaryValue !== undefined && (
          <TextInput
            style={[inputStyle, { flex: 2, marginLeft: 8, backgroundColor: textColor === '#FFF' ? '#333' : '#EFEFEF', color: subtitleColor }]}
            value={secondaryValue}
            editable={false}
          />
        )}
      </View>
    </View>
  );

  const renderRightSideField = (label: string, key: string, value: string, isReadOnly = false) => (
    <View style={{ flexDirection: 'row', alignItems: 'center', marginBottom: 7, width: '100%' }}>
      <ThemedText style={{ fontSize: 11, color: '#3C2E19', fontWeight: 'bold', width: 95 }}>{label}</ThemedText>
      <TextInput
        style={[styles.input, isReadOnly ? styles.readonlyInput : null, { flex: 1, textAlign: 'right' }]}
        value={value}
        onChangeText={(text) => updateHeader(key, text)}
        editable={!isReadOnly}
        keyboardType="numeric"
      />
    </View>
  );

  const RadioButton = ({ label, selected, onPress }: { label: string, selected: boolean, onPress: () => void }) => (
    <TouchableOpacity style={styles.radioContainer} onPress={onPress}>
      <View style={[styles.radioCircle, { borderColor: selected ? accentColor : borderColor }]}>
        {selected && <View style={[styles.radioInner, { backgroundColor: accentColor }]} />}
      </View>
      <ThemedText style={styles.radioLabel}>{label}</ThemedText>
    </TouchableOpacity>
  );

  const Checkbox = ({ label, selected, onPress }: { label: string, selected: boolean, onPress: () => void }) => (
    <TouchableOpacity style={styles.radioContainer} onPress={onPress}>
      <View style={[styles.checkboxSquare, { borderColor: borderColor, backgroundColor: selected ? accentColor : 'transparent' }]}>
        {selected && <Ionicons name="checkmark" size={14} color="#FFF" />}
      </View>
      <ThemedText style={styles.radioLabel}>{label}</ThemedText>
    </TouchableOpacity>
  );

  return (
    <SafeAreaView style={[styles.safeArea, { backgroundColor: '#FBF9F5' }]} edges={['top']}>
      <Stack.Screen options={{ headerShown: false }} />
      <View style={[styles.header, { borderBottomColor: '#EBE2D2', backgroundColor: '#FFFFFF' }]}>
        <View style={{ flexDirection: 'row', alignItems: 'center' }}>
          <TouchableOpacity style={styles.backButton} onPress={() => router.back()}>
            <Ionicons name="arrow-back" size={22} color="#3B2D17" />
          </TouchableOpacity>
          <ThemedText style={[styles.title, { color: '#3B2D17', fontWeight: '500' }]}>JobSheet</ThemedText>
        </View>
        <View style={{ flexDirection: 'row', flexWrap: 'wrap', justifyContent: 'flex-end', alignItems: 'center', flex: 1 }}>
          {/* <TouchableOpacity style={[styles.mainActionBtn, { backgroundColor: cardColor, borderColor: borderColor, marginBottom: 0, paddingVertical: 6, paddingHorizontal: 10 }]}>
              <ThemedText style={{ color: textColor, fontWeight: 'bold', fontSize: 12 }}>GoldBar Issue</ThemedText>
           </TouchableOpacity> */}
          {/* <TouchableOpacity style={[styles.mainActionBtn, { backgroundColor: cardColor, borderColor: borderColor, marginBottom: 0, paddingVertical: 6, paddingHorizontal: 10 }]}>
              <ThemedText style={{ color: textColor, fontWeight: 'bold', fontSize: 12 }}>GoldBar Fix</ThemedText>
           </TouchableOpacity> */}
          <TouchableOpacity style={[styles.mainActionBtn, { backgroundColor: '#FFFFFF', borderColor: '#DFD5C3', marginBottom: 0 }]} onPress={clearForm}>
            <ThemedText style={{ color: '#5C4A28', fontWeight: 'bold', fontSize: 12 }}>Clear</ThemedText>
          </TouchableOpacity>
          <TouchableOpacity style={[styles.mainActionBtn, { backgroundColor: '#F9F2E2', borderColor: '#B8903B', marginBottom: 0 }]} onPress={() => saveJobSheet()}>
            <ThemedText style={{ color: '#9E6D08', fontWeight: 'bold', fontSize: 12 }}>Save</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity style={[styles.mainActionBtn, { backgroundColor: '#FFFFFF', borderColor: '#F2A8A2', marginBottom: 0 }]} onPress={deleteJobSheet}>
            <ThemedText style={{ color: '#D93829', fontWeight: 'bold', fontSize: 12 }}>Delete</ThemedText>
          </TouchableOpacity>

        </View>
      </View>

      <ScrollView style={[styles.container, { backgroundColor: '#FBF9F5' }]} contentContainerStyle={styles.contentContainer}>
        {/* TOP SECTION: Form Header */}
        <ScrollView horizontal={true} showsHorizontalScrollIndicator={true} style={{ marginBottom: 14 }} contentContainerStyle={{ flexGrow: 1 }}>
          <View style={{ flexDirection: 'row', width: '100%', minWidth: 1100 }}>
            {/* LEFT: JOBSHEET DETAILS CARD */}
            <View style={[styles.goldCard, { flex: 7, minWidth: 730, marginRight: 14 }]}>
              {/* Card Header Banner */}
              <View style={styles.cardHeaderBanner}>
                <View style={styles.headerIconBadge}>
                  <Ionicons name="document-text" size={15} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitle}>JobSheet Details</ThemedText>
              </View>

              <View style={styles.cardBody}>
                {/* Row 1: JobSheet No, Invoice No, Invoice Date */}
                <View style={styles.row}>
                  <ThemedText style={styles.label}>JobSheet No</ThemedText>
                  <View style={{ width: 136, flexDirection: 'row' }}>
                    <TextInput
                      style={[styles.input, { width: 104, borderTopRightRadius: 0, borderBottomRightRadius: 0, borderRightWidth: 0 }]}
                      value={headerState.JobSheetNo}
                      onChangeText={(text) => updateHeader('JobSheetNo', text)}
                      onBlur={() => headerState.JobSheetNo.trim() !== '' && loadJobSheet(headerState.JobSheetNo)}
                    />
                    <TouchableOpacity style={styles.lookupBtn} onPress={openLov}>
                      <ThemedText style={styles.lookupBtnText}>...</ThemedText>
                    </TouchableOpacity>
                  </View>

                  <ThemedText style={[styles.labelRight, { width: 84 }]}>Invoice No</ThemedText>
                  <TextInput
                    style={[styles.input, { flex: 1, minWidth: 120 }]}
                    value={headerState.PurchaseBillNo}
                    onChangeText={(text) => updateHeader('PurchaseBillNo', text)}
                  />

                  <ThemedText style={[styles.labelRight, { width: 92 }]}>Invoice Date</ThemedText>
                  {Platform.OS === 'web' ? (
                    <input
                      type="date"
                      style={{
                        flex: 1,
                        minWidth: 110,
                        border: '1px solid #E2D7C5',
                        borderRadius: '6px',
                        padding: '4px 8px',
                        fontSize: '12px',
                        height: '28px',
                        color: '#2C2519',
                        backgroundColor: '#FFFFFF',
                        outline: 'none',
                      }}
                      value={headerState.PurchaseBillDate ? (() => {
                        const parts = headerState.PurchaseBillDate.split('-');
                        return parts.length === 3 && parts[2].length === 4 ? `${parts[2]}-${parts[1]}-${parts[0]}` : '';
                      })() : ''}
                      onChange={(e: any) => {
                        const val = e.target.value;
                        if (val) {
                          const [y, m, d] = val.split('-');
                          updateHeader('PurchaseBillDate', `${d}-${m}-${y}`);
                        } else {
                          updateHeader('PurchaseBillDate', '');
                        }
                      }}
                    />
                  ) : (
                    <TextInput
                      style={[styles.input, { flex: 1, minWidth: 110 }]}
                      value={headerState.PurchaseBillDate}
                      onChangeText={(text) => updateHeader('PurchaseBillDate', text)}
                      placeholder="DD-MM-YYYY"
                    />
                  )}
                </View>

                {/* Row 2: Supplier Code, Supplier Name, SO No */}
                <View style={styles.row}>
                  <ThemedText style={styles.label}>Supplier Code</ThemedText>
                  <View style={{ width: 136, flexDirection: 'row' }}>
                    <TextInput
                      style={[styles.input, { width: 104, borderTopRightRadius: 0, borderBottomRightRadius: 0, borderRightWidth: 0 }]}
                      value={headerState.SupplierCode}
                      onChangeText={(text) => updateHeader('SupplierCode', text)}
                    />
                    <TouchableOpacity style={styles.lookupBtn} onPress={openSupplierLov}>
                      <ThemedText style={styles.lookupBtnText}>...</ThemedText>
                    </TouchableOpacity>
                  </View>
                  <TextInput
                    style={[styles.input, styles.readonlyInput, { flex: 1, minWidth: 200, marginLeft: 8 }]}
                    value={headerState.SupplierName}
                    editable={false}
                  />

                  <ThemedText style={[styles.labelRight, { width: 60 }]}>SO No</ThemedText>
                  <TextInput
                    style={[styles.input, { flex: 0.8, minWidth: 90 }]}
                    value={headerState.SalesOrderNo}
                    onChangeText={(text) => updateHeader('SalesOrderNo', text)}
                  />
                </View>

                {/* Row 3: Currency Code, Currency Name, Rate */}
                <View style={styles.row}>
                  <ThemedText style={styles.label}>Currency Code</ThemedText>
                  <View style={{ width: 136, flexDirection: 'row' }}>
                    <TextInput
                      style={[styles.input, { width: 104, borderTopRightRadius: 0, borderBottomRightRadius: 0, borderRightWidth: 0 }]}
                      value={headerState.CurrencyCode}
                      onChangeText={(text) => updateHeader('CurrencyCode', text)}
                    />
                    <TouchableOpacity style={styles.lookupBtn} onPress={openCurrencyLov}>
                      <ThemedText style={styles.lookupBtnText}>...</ThemedText>
                    </TouchableOpacity>
                  </View>
                  <TextInput
                    style={[styles.input, styles.readonlyInput, { flex: 1, minWidth: 200, marginLeft: 8 }]}
                    value={headerState.CurrencyName}
                    editable={false}
                  />

                  <ThemedText style={[styles.labelRight, { width: 60 }]}>Rate</ThemedText>
                  <TextInput
                    style={[styles.input, { flex: 0.8, minWidth: 90 }]}
                    value={headerState.CurrencyRate}
                    onChangeText={(text) => updateHeader('CurrencyRate', text)}
                  />
                </View>

                {/* Row 4: Payable, GST, TEAL %, Cost/g */}
                <View style={[styles.row, { alignItems: 'center' }]}>
                  <ThemedText style={styles.label}>Payable</ThemedText>
                  <View style={{ flexDirection: 'row', alignItems: 'center', marginRight: 10 }}>
                    <RadioButton label="Gold" selected={headerState.PaymentType === 'G'} onPress={() => updateHeader('PaymentType', 'G')} />
                    <RadioButton label="Cash" selected={headerState.PaymentType === 'C'} onPress={() => updateHeader('PaymentType', 'C')} />
                    <RadioButton label="Both" selected={headerState.PaymentType === 'B'} onPress={() => updateHeader('PaymentType', 'B')} />
                  </View>

                  <ThemedText style={{ marginRight: 6, color: '#3B3020', fontWeight: 'bold', fontSize: 11 }}>GST</ThemedText>
                  <Checkbox label="" selected={chkGST1} onPress={() => handleChkGST1(!chkGST1)} />

                  {/* 22K / 24 Pill Box */}
                  <View style={styles.pillBox}>
                    <RadioButton
                      label="22K"
                      selected={Number(headerState.GST) === 22}
                      onPress={() => updateHeader('GST', 22)}
                    />
                    <RadioButton
                      label="24"
                      selected={Number(headerState.GST) === 24}
                      onPress={() => updateHeader('GST', 24)}
                    />
                  </View>

                  {/* TEAL % */}
                  <View style={{ flexDirection: 'row', marginLeft: 12, alignItems: 'center' }}>
                    <ThemedText style={{ color: '#4A3B25', fontSize: 11, marginRight: 5, fontWeight: 'bold' }}>TEAL %</ThemedText>
                    <View style={{ width: 76, flexDirection: 'row' }}>
                      <TextInput
                        style={[styles.input, { width: 50, borderTopRightRadius: 0, borderBottomRightRadius: 0, borderRightWidth: 0 }]}
                        value={headerState.TaelPercent}
                        onChangeText={(text) => updateHeader('TaelPercent', text)}
                      />
                      <TouchableOpacity style={[styles.lookupBtn, { width: 26 }]} onPress={openTaelPercentLov}>
                        <ThemedText style={styles.lookupBtnText}>...</ThemedText>
                      </TouchableOpacity>
                    </View>

                    <ThemedText style={{ color: '#4A3B25', fontSize: 11, marginLeft: 8, marginRight: 5, fontWeight: 'bold' }}>TAEL</ThemedText>
                    <TextInput
                      style={[styles.input, { width: 58 }]}
                      value={headerState.TaelRate}
                      onChangeText={(text) => updateHeader('TaelRate', text)}
                      keyboardType="numeric"
                    />

                    <ThemedText style={{ color: '#4A3B25', fontSize: 11, marginLeft: 8, marginRight: 5, fontWeight: 'bold' }}>
                      Cost/Gram <ThemedText style={{ color: '#EF4444' }}>*</ThemedText>
                    </ThemedText>
                    <TextInput
                      style={[styles.input, { width: 68 }]}
                      value={headerState.CostPerGram}
                      onChangeText={(text) => updateHeader('CostPerGram', text)}
                      keyboardType="numeric"
                    />
                  </View>
                </View>

                {/* Row 5: Options, Fix/UnFix, Due Date */}
                <View style={[styles.row, { alignItems: 'center' }]}>
                  <ThemedText style={styles.label}>Options</ThemedText>
                  <Checkbox label="Accessories" selected={headerState.Accessories} onPress={() => updateHeader('Accessories', !headerState.Accessories)} />
                  <Checkbox label="Spl. Category" selected={headerState.SplCategory} onPress={() => updateHeader('SplCategory', !headerState.SplCategory)} />

                  {/* Fix / UnFix Pill Box */}
                  <View style={styles.pillBox}>
                    <RadioButton label="Fix" selected={headerState.FixStatus === 'F'} onPress={() => updateHeader('FixStatus', 'F')} />
                    <RadioButton label="UnFix" selected={headerState.FixStatus === 'U' || headerState.FixStatus === 'N'} onPress={() => updateHeader('FixStatus', 'U')} />
                  </View>

                  <ThemedText style={[styles.labelRight, { width: 70 }]}>Due Date</ThemedText>
                  {Platform.OS === 'web' ? (
                    <input
                      type="date"
                      style={{
                        flex: 1,
                        minWidth: 110,
                        border: '1px solid #E2D7C5',
                        borderRadius: '6px',
                        padding: '4px 8px',
                        fontSize: '12px',
                        height: '28px',
                        color: '#2C2519',
                        backgroundColor: '#FFFFFF',
                        outline: 'none',
                      }}
                      value={headerState.DueDate ? (() => {
                        const parts = headerState.DueDate.split('-');
                        return parts.length === 3 && parts[2].length === 4 ? `${parts[2]}-${parts[1]}-${parts[0]}` : '';
                      })() : ''}
                      onChange={(e: any) => {
                        const val = e.target.value;
                        if (val) {
                          const [y, m, d] = val.split('-');
                          updateHeader('DueDate', `${d}-${m}-${y}`);
                        } else {
                          updateHeader('DueDate', '');
                        }
                      }}
                    />
                  ) : (
                    <TextInput
                      style={[styles.input, { flex: 1, minWidth: 110 }]}
                      value={headerState.DueDate}
                      onChangeText={(text) => updateHeader('DueDate', text)}
                      placeholder="DD-MM-YYYY"
                    />
                  )}
                </View>

                {/* Row 6: Payable Cash, Remarks */}
                <View style={styles.row}>
                  <ThemedText style={styles.label}>Payable Cash</ThemedText>
                  <TextInput
                    style={[styles.input, { width: 136 }]}
                    value={headerState.PaidAmount}
                    onChangeText={(text) => updateHeader('PaidAmount', text)}
                  />
                  <ThemedText style={[styles.labelRight, { width: 80 }]}>Remarks</ThemedText>
                  <TextInput
                    style={[styles.input, { flex: 1, minWidth: 200 }]}
                    value={headerState.Remarks}
                    onChangeText={(text) => updateHeader('Remarks', text)}
                  />
                </View>
              </View>
            </View>

            {/* RIGHT: COST SUMMARY CARD */}
            <View style={[styles.goldCard, { flex: 3.4, minWidth: 350 }]}>
              {/* Header Banner */}
              <View style={styles.cardHeaderBanner}>
                <View style={styles.headerIconBadge}>
                  <Ionicons name="layers" size={15} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitle}>Cost Summary</ThemedText>
              </View>

              <View style={[styles.cardBody, { justifyContent: 'space-between', flex: 1 }]}>
                <View>
                  {renderRightSideField('Gold Value', 'TotalLocalCost', headerState.TotalLocalCost, true)}
                  {renderRightSideField('Total MC', 'TotalMC', headerState.TotalMC, true)}
                  {renderRightSideField('Adjustment', 'ManualAdjustment', headerState.ManualAdjustment, false)}
                  {renderRightSideField('GoldValue+MC', 'TotalCost', headerState.TotalCost, true)}

                  {/* GST Field */}
                  <View style={{ flexDirection: 'row', alignItems: 'center', marginBottom: 7, width: '100%' }}>
                    <ThemedText style={{ fontSize: 11, color: '#3C2E19', fontWeight: 'bold', width: 95 }}>GST</ThemedText>
                    <Checkbox label="" selected={headerState.TaelGST} onPress={() => handleChkGST(!headerState.TaelGST)} />
                    <TextInput
                      style={[styles.input, styles.readonlyInput, { flex: 1, marginLeft: 4 }]}
                      editable={false}
                      value={headerState.GSTAmountDisplay}
                    />
                  </View>
                </View>

                {/* Total Cost Highlight Banner */}
                <View style={styles.totalCostBanner}>
                  <View style={styles.totalCostLabelGroup}>
                    <View style={styles.totalCostIconBadge}>
                      <ThemedText style={{ color: '#FFFFFF', fontWeight: 'bold', fontSize: 13 }}>$</ThemedText>
                    </View>
                    <ThemedText style={styles.totalCostTitle}>Total Cost</ThemedText>
                  </View>
                  <View style={styles.totalCostValueBox}>
                    <ThemedText style={styles.totalCostValueText}>
                      {headerState.TotalCost ? parseFloat(headerState.TotalCost).toFixed(2) : '0.00'}
                    </ThemedText>
                  </View>
                </View>
              </View>
            </View>
          </View>
        </ScrollView>

        {/* LINE DETAILS SECTION */}
        <ScrollView horizontal style={{ marginBottom: 14 }} contentContainerStyle={{ flexGrow: 1 }}>
          <View style={{ width: '100%', minWidth: 1000 }}>
            {/* Card wrapper for Line Details */}
            <View style={styles.goldCard}>
              {/* Card Header Banner with Add Line button */}
              <View style={[styles.cardHeaderBanner, { justifyContent: 'space-between' }]}>
                <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                  <View style={styles.headerIconBadge}>
                    <Ionicons name="list" size={15} color="#B48218" />
                  </View>
                  <ThemedText style={styles.cardHeaderTitle}>Line Details</ThemedText>
                </View>

                <TouchableOpacity
                  style={styles.addLineBtn}
                  onPress={() => {
                    setSelectedIndex(null);
                    setCurrentDetail({ CatalogueCode: '', CategoryCode: '', Description: '', Weight: '', CostPerGram: '', MC: '', PCS: '', GoldLoss: '', TotalCost: '' });
                    setDetailModalVisible(true);
                  }}
                >
                  <Ionicons name="add" size={14} color="#FFF" style={{ marginRight: 4 }} />
                  <ThemedText style={styles.addLineBtnText}>Add Line</ThemedText>
                </TouchableOpacity>
              </View>

              {/* Table Header */}
              <View style={styles.tableHeader}>
                <ThemedText style={[styles.tableHeaderText, { flex: 15 }]}>Catalogue Code</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { flex: 13 }]}>Category Code</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { flex: 20 }]}>Description</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { flex: 10 }]}>Weight (gm)</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { flex: 12 }]}>Cost Per Gram</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { flex: 10 }]}>MC</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { flex: 8 }]}>PCS</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { flex: 12 }]}>Total Cost</ThemedText>
              </View>

              {/* Table Rows or Empty State */}
              {details.length === 0 ? (
                <View style={styles.emptyTableState}>
                  <Ionicons name="cube-outline" size={38} color="#D8C7AA" style={{ marginBottom: 6 }} />
                  <ThemedText style={{ color: '#8F7E66', fontSize: 13, fontWeight: '500' }}>No records to display</ThemedText>
                </View>
              ) : (
                <ScrollView style={{ maxHeight: 220 }}>
                  {details.map((item, index) => (
                    <TouchableOpacity
                      key={index}
                      style={[
                        styles.tableRow,
                        { backgroundColor: selectedIndex === index ? '#F9F1E2' : index % 2 === 1 ? '#FAF7F0' : '#FFFFFF' }
                      ]}
                      onPress={() => {
                        setSelectedIndex(index);
                        setCurrentDetail(item);
                        setDetailModalVisible(true);
                      }}
                    >
                      <ThemedText style={[styles.tableCellText, { flex: 15 }]}>{item.CatalogueCode}</ThemedText>
                      <ThemedText style={[styles.tableCellText, { flex: 13 }]}>{item.CategoryCode}</ThemedText>
                      <ThemedText style={[styles.tableCellText, { flex: 20 }]}>{item.Description}</ThemedText>
                      <ThemedText style={[styles.tableCellText, { flex: 10 }]}>{item.Weight}</ThemedText>
                      <ThemedText style={[styles.tableCellText, { flex: 12 }]}>{item.CostPerGram}</ThemedText>
                      <ThemedText style={[styles.tableCellText, { flex: 10 }]}>{item.MC}</ThemedText>
                      <ThemedText style={[styles.tableCellText, { flex: 8 }]}>{item.PCS}</ThemedText>
                      <ThemedText style={[styles.tableCellText, { flex: 12, fontWeight: 'bold' }]}>{item.TotalCost}</ThemedText>
                    </TouchableOpacity>
                  ))}
                </ScrollView>
              )}

              {/* Totals Row */}
              <View style={styles.tableTotalRow}>
                <ThemedText style={[styles.tableTotalCellText, { flex: 15 }]}></ThemedText>
                <ThemedText style={[styles.tableTotalCellText, { flex: 13 }]}></ThemedText>
                <ThemedText style={[styles.tableTotalCellText, { flex: 20, textAlign: 'right' }]}>Total : </ThemedText>
                <ThemedText style={[styles.tableTotalCellText, { flex: 10 }]}>
                  {details.reduce((sum, item) => sum + (parseFloat(item.Weight) || 0), 0).toFixed(3)}
                </ThemedText>
                <ThemedText style={[styles.tableTotalCellText, { flex: 12 }]}>
                  {details.reduce((sum, item) => sum + (parseFloat(item.CostPerGram) || 0), 0).toFixed(2)}
                </ThemedText>
                <ThemedText style={[styles.tableTotalCellText, { flex: 10 }]}>
                  {details.reduce((sum, item) => sum + (parseFloat(item.MC) || 0), 0).toFixed(2)}
                </ThemedText>
                <ThemedText style={[styles.tableTotalCellText, { flex: 8 }]}>
                  {details.reduce((sum, item) => sum + (parseFloat(item.PCS) || 0), 0)}
                </ThemedText>
                <ThemedText style={[styles.tableTotalCellText, { flex: 12 }]}>
                  {details.reduce((sum, item) => sum + (parseFloat(item.TotalCost) || 0), 0).toFixed(2)}
                </ThemedText>
              </View>
            </View>
          </View>
        </ScrollView>


        {/* BOTTOM SECTION: Actions */}
        <View style={styles.footerSection}>
          <View style={{ flex: 1 }}>
            <View style={{ flexDirection: 'row', alignItems: 'center' }}>
              <Checkbox label="Match Jobsheet" selected={!!headerState.MatchJobSheetNo} onPress={() => updateHeader('MatchJobSheetNo', headerState.MatchJobSheetNo ? '' : 'Y')} />
            </View>
          </View>
        </View>

      </ScrollView>

      {/* Detail Modal */}
      <Modal visible={detailModalVisible} transparent={true} animationType="fade">
        <View style={styles.modalOverlay}>
          <View style={[styles.modalContainer, { backgroundColor: cardColor, borderColor: borderColor, width: '90%' }]}>
            <View style={{ flexDirection: 'row', justifyContent: 'space-between', alignItems: 'center', marginBottom: 12 }}>
              <ThemedText style={[styles.modalTitle, { color: textColor, marginBottom: 0 }]}>{selectedIndex !== null ? 'Edit Line Detail' : 'Add Line Detail'}</ThemedText>
              <TouchableOpacity onPress={() => setDetailModalVisible(false)}>
                <Ionicons name="close" size={24} color={textColor} />
              </TouchableOpacity>
            </View>
            <View style={[styles.gridHeader, { backgroundColor: backgroundColor, borderBottomColor: borderColor }]}>
              <ThemedText style={[styles.gridHeaderText, { flex: 15, color: textColor }]} >Catalogue Cod</ThemedText>
              <ThemedText style={[styles.gridHeaderText, { flex: 13, color: textColor }]} >Category Code</ThemedText>
              <ThemedText style={[styles.gridHeaderText, { flex: 20, color: textColor }]} >Description</ThemedText>
              <ThemedText style={[styles.gridHeaderText, { flex: 10, color: textColor }]} >Weight</ThemedText>
              <ThemedText style={[styles.gridHeaderText, { flex: 12, color: textColor }]} >Cost/Gm</ThemedText>
              <ThemedText style={[styles.gridHeaderText, { flex: 10, color: textColor }]} >MC</ThemedText>
              <ThemedText style={[styles.gridHeaderText, { flex: 8, color: textColor }]} >PCS</ThemedText>
              <ThemedText style={[styles.gridHeaderText, { flex: 12, color: textColor }]} >Total Cost</ThemedText>
            </View>
            <View style={[styles.gridRow, { borderBottomWidth: 0, paddingVertical: 8 }]}>
              <View style={{ flex: 15, paddingRight: 4 }}>
                <View style={styles.lookupContainer}>
                  <TextInput style={[inputStyle, { flex: 1, padding: 4, height: 30 }]} value={currentDetail.CatalogueCode} onChangeText={(t: string) => updateDetail('CatalogueCode', t)} />
                  <TouchableOpacity style={[styles.lookupBtn, { backgroundColor: cardColor, borderColor: borderColor, width: 24, height: 30 }]} onPress={openCatalogueLov}><ThemedText style={{ color: textColor, fontSize: 10 }}>...</ThemedText></TouchableOpacity>
                </View>
              </View>
              <View style={{ flex: 13, paddingRight: 4 }}>
                <TextInput style={[inputStyle, { flex: 1, padding: 4, height: 30 }]} value={currentDetail.CategoryCode} onChangeText={(t: string) => updateDetail('CategoryCode', t)} />
              </View>
              <View style={{ flex: 20, paddingRight: 4 }}>
                <TextInput style={[inputStyle, { flex: 1, padding: 4, height: 30 }]} value={currentDetail.Description} onChangeText={(t: string) => updateDetail('Description', t)} />
              </View>
              <View style={{ flex: 10, paddingRight: 4 }}>
                <TextInput style={[inputStyle, { flex: 1, padding: 4, height: 30 }]} value={currentDetail.Weight} onChangeText={(t: string) => updateDetail('Weight', t)} keyboardType="numeric" />
              </View>
              <View style={{ flex: 12, paddingRight: 4 }}>
                <TextInput style={[inputStyle, { flex: 1, padding: 4, height: 30 }]} value={currentDetail.CostPerGram} onChangeText={(t: string) => updateDetail('CostPerGram', t)} keyboardType="numeric" />
              </View>
              <View style={{ flex: 10, paddingRight: 4 }}>
                <TextInput style={[inputStyle, { flex: 1, padding: 4, height: 30 }]} value={currentDetail.MC} onChangeText={(t: string) => updateDetail('MC', t)} keyboardType="numeric" />
              </View>
              <View style={{ flex: 8, paddingRight: 4 }}>
                <TextInput style={[inputStyle, { flex: 1, padding: 4, height: 30 }]} value={currentDetail.PCS} onChangeText={(t: string) => updateDetail('PCS', t)} keyboardType="numeric" />
              </View>
              <View style={{ flex: 12 }}>
                <TextInput style={[inputStyle, { flex: 1, padding: 4, height: 30 }]} value={currentDetail.TotalCost} onChangeText={(t: string) => updateDetail('TotalCost', t)} keyboardType="numeric" />
              </View>
            </View>
            <View style={[styles.row, { justifyContent: 'flex-end', marginTop: 12, paddingRight: 8 }]}>
              <TouchableOpacity style={[styles.actionBtn, { backgroundColor: cardColor, borderColor: accentColor }]} onPress={addDetail}>
                <ThemedText style={{ color: accentColor, fontWeight: 'bold' }}>{selectedIndex !== null ? 'Update' : 'Add'}</ThemedText>
              </TouchableOpacity>
              <TouchableOpacity style={[styles.actionBtn, { backgroundColor: cardColor, borderColor: '#ff3b30' }]} onPress={deleteSelectedDetail}>
                <ThemedText style={{ color: '#ff3b30', fontWeight: 'bold' }}>Delete</ThemedText>
              </TouchableOpacity>
              <TouchableOpacity style={[styles.actionBtn, { backgroundColor: cardColor, borderColor: borderColor }]} onPress={() => { setSelectedIndex(null); setCurrentDetail({ CatalogueCode: '', CategoryCode: '', Description: '', Weight: '', CostPerGram: '', MC: '', PCS: '', GoldLoss: '', TotalCost: '' }); }}>
                <ThemedText style={{ color: textColor, fontWeight: 'bold' }}>Clear</ThemedText>
              </TouchableOpacity>
            </View>
          </View>
        </View>
      </Modal>

      {/* LOV Modal */}
      <Modal visible={lovVisible} transparent={true} animationType="fade">
        <View style={styles.modalOverlay}>
          <View style={[styles.modalContainer, { backgroundColor: cardColor, borderColor: borderColor, width: '90%' }]}>
            <View style={{ flexDirection: 'row', justifyContent: 'space-between', alignItems: 'center', marginBottom: 12 }}>
              <ThemedText style={[styles.modalTitle, { color: textColor, marginBottom: 0 }]}>Select JobSheet</ThemedText>
              <TouchableOpacity onPress={() => setLovVisible(false)}>
                <Ionicons name="close" size={24} color={textColor} />
              </TouchableOpacity>
            </View>

            {/* LOV Header */}
            <View style={{ flexDirection: 'row', borderBottomWidth: 1, borderBottomColor: borderColor, paddingBottom: 8, marginBottom: 8 }}>
              <ThemedText style={{ flex: 1.5, fontWeight: 'bold', color: textColor, fontSize: 12 }}>JobSheetNo</ThemedText>
              <ThemedText style={{ flex: 1, fontWeight: 'bold', color: textColor, fontSize: 12 }}>SupplierCode</ThemedText>
              <ThemedText style={{ flex: 1, fontWeight: 'bold', color: textColor, fontSize: 12 }}>PurchaseBillNo</ThemedText>
              <ThemedText style={{ flex: 1, fontWeight: 'bold', color: textColor, fontSize: 12 }}>TotalCost</ThemedText>
            </View>

            <FlatList
              data={lovData}
              keyExtractor={(item, index) => item.JobSheetNo ? item.JobSheetNo.toString() : index.toString()}
              renderItem={({ item }) => (
                <TouchableOpacity
                  style={[styles.lovItem, { borderBottomColor: borderColor, flexDirection: 'row', paddingVertical: 10 }]}
                  onPress={() => {
                    setLovVisible(false);
                    loadJobSheet(item.JobSheetNo);
                  }}
                >
                  <ThemedText style={{ flex: 1.5, color: textColor, fontSize: 12 }}>{item.JobSheetNo}</ThemedText>
                  <ThemedText style={{ flex: 1, color: textColor, fontSize: 12 }}>{item.SupplierCode}</ThemedText>
                  <ThemedText style={{ flex: 1, color: textColor, fontSize: 12 }}>{item.PurchaseBillNo}</ThemedText>
                  <ThemedText style={{ flex: 1, color: textColor, fontSize: 12 }}>{item.TotalCost}</ThemedText>
                </TouchableOpacity>
              )}
            />
          </View>
        </View>
      </Modal>

      {/* Supplier LOV Modal */}
      <Modal visible={supplierLovVisible} transparent={true} animationType="fade">
        <View style={styles.modalOverlay}>
          <View style={[styles.modalContainer, { backgroundColor: cardColor, borderColor: borderColor, width: '90%' }]}>
            <View style={{ flexDirection: 'row', justifyContent: 'space-between', alignItems: 'center', marginBottom: 12 }}>
              <ThemedText style={[styles.modalTitle, { color: textColor, marginBottom: 0 }]}>Select Supplier</ThemedText>
              <TouchableOpacity onPress={() => setSupplierLovVisible(false)}>
                <Ionicons name="close" size={24} color={textColor} />
              </TouchableOpacity>
            </View>

            <View style={{ flexDirection: 'row', borderBottomWidth: 1, borderBottomColor: borderColor, paddingBottom: 8, marginBottom: 8 }}>
              <ThemedText style={{ flex: 1, fontWeight: 'bold', color: textColor, fontSize: 12 }}>SupplierCode</ThemedText>
              <ThemedText style={{ flex: 2, fontWeight: 'bold', color: textColor, fontSize: 12 }}>SupplierName</ThemedText>
            </View>

            <FlatList
              data={supplierLovData}
              keyExtractor={(item, index) => item.SupplierCode ? item.SupplierCode.toString() : index.toString()}
              renderItem={({ item }) => (
                <TouchableOpacity
                  style={[styles.lovItem, { borderBottomColor: borderColor, flexDirection: 'row', paddingVertical: 10 }]}
                  onPress={() => {
                    setSupplierLovVisible(false);
                    setHeaderState(prev => ({ ...prev, SupplierCode: item.SupplierCode, SupplierName: item.SupplierName }));
                  }}
                >
                  <ThemedText style={{ flex: 1, color: textColor, fontSize: 12 }}>{item.SupplierCode}</ThemedText>
                  <ThemedText style={{ flex: 2, color: textColor, fontSize: 12 }}>{item.SupplierName}</ThemedText>
                </TouchableOpacity>
              )}
            />
          </View>
        </View>
      </Modal>

      {/* Currency LOV Modal */}
      <Modal visible={currencyLovVisible} transparent={true} animationType="fade">
        <View style={styles.modalOverlay}>
          <View style={[styles.modalContainer, { backgroundColor: cardColor, borderColor: borderColor, width: '90%' }]}>
            <View style={{ flexDirection: 'row', justifyContent: 'space-between', alignItems: 'center', marginBottom: 12 }}>
              <ThemedText style={[styles.modalTitle, { color: textColor, marginBottom: 0 }]}>Select Currency</ThemedText>
              <TouchableOpacity onPress={() => setCurrencyLovVisible(false)}>
                <Ionicons name="close" size={24} color={textColor} />
              </TouchableOpacity>
            </View>

            <View style={{ flexDirection: 'row', borderBottomWidth: 1, borderBottomColor: borderColor, paddingBottom: 8, marginBottom: 8 }}>
              <ThemedText style={{ flex: 1, fontWeight: 'bold', color: textColor, fontSize: 12 }}>Code</ThemedText>
              <ThemedText style={{ flex: 2, fontWeight: 'bold', color: textColor, fontSize: 12 }}>Name</ThemedText>
              <ThemedText style={{ flex: 1, fontWeight: 'bold', color: textColor, fontSize: 12 }}>Rate</ThemedText>
            </View>

            <FlatList
              data={currencyLovData}
              keyExtractor={(item, index) => item.CurrencyCode ? item.CurrencyCode.toString() : index.toString()}
              renderItem={({ item }) => (
                <TouchableOpacity
                  style={[styles.lovItem, { borderBottomColor: borderColor, flexDirection: 'row', paddingVertical: 10 }]}
                  onPress={() => {
                    setCurrencyLovVisible(false);
                    setHeaderState(prev => ({
                      ...prev,
                      CurrencyCode: item.CurrencyCode,
                      CurrencyName: item.CurrencyName,
                      CurrencyRate: item.CurrencyRate != null ? item.CurrencyRate.toString() : '1.0'
                    }));
                  }}
                >
                  <ThemedText style={{ flex: 1, color: textColor, fontSize: 12 }}>{item.CurrencyCode}</ThemedText>
                  <ThemedText style={{ flex: 2, color: textColor, fontSize: 12 }}>{item.CurrencyName}</ThemedText>
                  <ThemedText style={{ flex: 1, color: textColor, fontSize: 12 }}>{item.CurrencyRate}</ThemedText>
                </TouchableOpacity>
              )}
            />
          </View>
        </View>
      </Modal>

      {/* Catalogue LOV Modal */}
      <Modal visible={catalogueLovVisible} transparent={true} animationType="fade">
        <View style={styles.modalOverlay}>
          <View style={[styles.modalContainer, { backgroundColor: cardColor, borderColor: borderColor, width: '90%' }]}>
            <View style={{ flexDirection: 'row', justifyContent: 'space-between', alignItems: 'center', marginBottom: 12 }}>
              <ThemedText style={[styles.modalTitle, { color: textColor, marginBottom: 0 }]}>Select Catalogue</ThemedText>
              <TouchableOpacity onPress={() => setCatalogueLovVisible(false)}>
                <Ionicons name="close" size={24} color={textColor} />
              </TouchableOpacity>
            </View>

            <View style={{ flexDirection: 'row', borderBottomWidth: 1, borderBottomColor: borderColor, paddingBottom: 8, marginBottom: 8 }}>
              <ThemedText style={{ flex: 1, fontWeight: 'bold', color: textColor, fontSize: 12 }}>CatalogueCode</ThemedText>
              <ThemedText style={{ flex: 1, fontWeight: 'bold', color: textColor, fontSize: 12 }}>CategoryCode</ThemedText>
              <ThemedText style={{ flex: 2, fontWeight: 'bold', color: textColor, fontSize: 12 }}>CatalogueName</ThemedText>
            </View>

            <FlatList
              data={catalogueLovData}
              keyExtractor={(item, index) => item.CatalogueCode ? item.CatalogueCode.toString() : index.toString()}
              renderItem={({ item }) => (
                <TouchableOpacity
                  style={[styles.lovItem, { borderBottomColor: borderColor, flexDirection: 'row', paddingVertical: 10 }]}
                  onPress={() => {
                    setCatalogueLovVisible(false);
                    updateDetail('CatalogueCode', item.CatalogueCode);
                    if (item.CatalogueName) updateDetail('Description', item.CatalogueName);
                    if (item.CategoryCode) updateDetail('CategoryCode', item.CategoryCode);
                  }}
                >
                  <ThemedText style={{ flex: 1, color: textColor, fontSize: 12 }}>{item.CatalogueCode}</ThemedText>
                  <ThemedText style={{ flex: 1, color: textColor, fontSize: 12 }}>{item.CategoryCode}</ThemedText>
                  <ThemedText style={{ flex: 2, color: textColor, fontSize: 12 }}>{item.CatalogueName}</ThemedText>
                </TouchableOpacity>
              )}
            />
          </View>
        </View>
      </Modal>

      {/* TaelPercent LOV Modal */}
      <Modal visible={taelPercentLovVisible} transparent={true} animationType="fade">
        <View style={styles.modalOverlay}>
          <View style={[styles.modalContainer, { backgroundColor: cardColor, borderColor: borderColor, width: '90%' }]}>
            <View style={{ flexDirection: 'row', justifyContent: 'space-between', alignItems: 'center', marginBottom: 12 }}>
              <ThemedText style={[styles.modalTitle, { color: textColor, marginBottom: 0 }]}>Select Tael Percent</ThemedText>
              <TouchableOpacity onPress={() => setTaelPercentLovVisible(false)}>
                <Ionicons name="close" size={24} color={textColor} />
              </TouchableOpacity>
            </View>

            <View style={{ flexDirection: 'row', borderBottomWidth: 1, borderBottomColor: borderColor, paddingBottom: 8, marginBottom: 8 }}>
              <ThemedText style={{ flex: 1, fontWeight: 'bold', color: textColor, fontSize: 12 }}>TaelPercent</ThemedText>
              <ThemedText style={{ flex: 1, fontWeight: 'bold', color: textColor, fontSize: 12 }}>TaelPercent1</ThemedText>
              <ThemedText style={{ flex: 1, fontWeight: 'bold', color: textColor, fontSize: 12 }}>TaelPercent2</ThemedText>
              <ThemedText style={{ flex: 1, fontWeight: 'bold', color: textColor, fontSize: 12 }}>TaelPercent3</ThemedText>
            </View>

            <FlatList
              data={taelPercentLovData}
              keyExtractor={(item, index) => item.TaelPercent ? item.TaelPercent.toString() : index.toString()}
              renderItem={({ item }) => (
                <TouchableOpacity
                  style={[styles.lovItem, { borderBottomColor: borderColor, flexDirection: 'row', paddingVertical: 10 }]}
                  onPress={() => {
                    setTaelPercentLovVisible(false);
                    updateHeader('TaelPercent', item.TaelPercent?.toString() || '');
                  }}
                >
                  <ThemedText style={{ flex: 1, color: textColor, fontSize: 12 }}>{item.TaelPercent}</ThemedText>
                  <ThemedText style={{ flex: 1, color: textColor, fontSize: 12 }}>{item.TaelPercent1}</ThemedText>
                  <ThemedText style={{ flex: 1, color: textColor, fontSize: 12 }}>{item.TaelPercent2}</ThemedText>
                  <ThemedText style={{ flex: 1, color: textColor, fontSize: 12 }}>{item.TaelPercent3}</ThemedText>
                </TouchableOpacity>
              )}
            />
          </View>
        </View>
      </Modal>
      {toast && (
        <View style={[styles.toast, { backgroundColor: toast.type === 'success' ? '#4CAF50' : '#F44336' }]}>
          <ThemedText style={styles.toastText}>{toast.message}</ThemedText>
        </View>
      )}

    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  safeArea: { flex: 1 },
  header: { flexDirection: 'row', alignItems: 'center', justifyContent: 'space-between', paddingHorizontal: 16, paddingVertical: 10, borderBottomWidth: 1 },
  backButton: { padding: 4, marginRight: 12 },
  title: { fontSize: 20, fontWeight: '400', letterSpacing: 1 },
  container: { flex: 1, paddingVertical: 12, paddingHorizontal: 16 },
  contentContainer: { paddingBottom: 40 },

  // Luxury Gold Card Styling
  goldCard: {
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    overflow: 'hidden',
    shadowColor: '#A38035',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.08,
    shadowRadius: 6,
    elevation: 2,
  },
  cardHeaderBanner: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#F5E7CA',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    paddingVertical: 7,
    paddingHorizontal: 12,
  },
  headerIconBadge: {
    width: 24,
    height: 24,
    borderRadius: 5,
    backgroundColor: '#FFF8EA',
    borderWidth: 1,
    borderColor: '#E2D2AF',
    justifyContent: 'center',
    alignItems: 'center',
    marginRight: 8,
  },
  cardHeaderTitle: {
    fontSize: 13,
    fontWeight: 'bold',
    color: '#3B2D17',
    letterSpacing: 0.3,
  },
  cardBody: {
    padding: 10,
  },

  // Rows and Labels
  row: { flexDirection: 'row', alignItems: 'center', marginBottom: 7, flexWrap: 'wrap' },
  fieldContainer: { marginBottom: 2, paddingHorizontal: 4 },
  label: { fontSize: 11, fontWeight: 'bold', width: 96, marginRight: 6, color: '#3A2E19' },
  labelRight: { fontSize: 11, fontWeight: 'bold', textAlign: 'right', marginRight: 8, color: '#3A2E19' },

  // Rounded Inputs
  input: {
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderRadius: 6,
    paddingHorizontal: 8,
    paddingVertical: 3,
    fontSize: 12,
    height: 28,
    backgroundColor: '#FFFFFF',
    color: '#2C2519',
  },
  readonlyInput: {
    backgroundColor: '#F8F5EE',
    borderColor: '#EADBCA',
    color: '#554736',
  },

  // Lookups & Buttons
  lookupContainer: { flexDirection: 'row' },
  lookupBtn: {
    width: 32,
    height: 28,
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderLeftWidth: 1,
    justifyContent: 'center',
    alignItems: 'center',
    borderTopRightRadius: 6,
    borderBottomRightRadius: 6,
    backgroundColor: '#FAF5EA',
  },
  lookupBtnText: {
    color: '#6E5528',
    fontWeight: 'bold',
    fontSize: 12,
  },

  // Pill Box for Grouped Choices
  pillBox: {
    flexDirection: 'row',
    alignItems: 'center',
    marginLeft: 8,
    borderWidth: 1,
    borderColor: '#DFD3BC',
    borderRadius: 14,
    paddingHorizontal: 6,
    paddingVertical: 1,
    height: 26,
    backgroundColor: '#FBF8F2',
  },

  // Radios and Checkboxes
  radioContainer: { flexDirection: 'row', alignItems: 'center', marginVertical: 2, marginRight: 10 },
  radioCircle: {
    width: 15,
    height: 15,
    borderRadius: 8,
    borderWidth: 1.5,
    borderColor: '#B8903B',
    justifyContent: 'center',
    alignItems: 'center',
    marginRight: 5,
    backgroundColor: '#FFFFFF',
  },
  radioInner: {
    width: 7,
    height: 7,
    borderRadius: 4,
    backgroundColor: '#B8903B',
  },
  radioLabel: { fontSize: 11, color: '#362B1A', fontWeight: '500' },
  checkboxSquare: {
    width: 15,
    height: 15,
    borderWidth: 1.5,
    borderColor: '#B8903B',
    justifyContent: 'center',
    alignItems: 'center',
    marginRight: 5,
    borderRadius: 3,
  },

  // Total Cost Highlight Banner
  totalCostBanner: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    backgroundColor: '#FFF4DC',
    borderWidth: 1,
    borderColor: '#ECD8AB',
    borderRadius: 6,
    paddingVertical: 5,
    paddingHorizontal: 10,
    marginTop: 8,
  },
  totalCostLabelGroup: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  totalCostIconBadge: {
    width: 22,
    height: 22,
    borderRadius: 11,
    backgroundColor: '#9B6E16',
    justifyContent: 'center',
    alignItems: 'center',
    marginRight: 8,
  },
  totalCostTitle: {
    fontSize: 13,
    fontWeight: 'bold',
    color: '#473210',
  },
  totalCostValueBox: {
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#E8D4A2',
    borderRadius: 5,
    paddingVertical: 3,
    paddingHorizontal: 16,
    minWidth: 110,
    alignItems: 'flex-end',
  },
  totalCostValueText: {
    fontSize: 14,
    fontWeight: 'bold',
    color: '#281B08',
  },

  // Table Styling
  addLineBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#9E6D08',
    paddingVertical: 4,
    paddingHorizontal: 12,
    borderRadius: 5,
  },
  addLineBtnText: {
    color: '#FFFFFF',
    fontWeight: 'bold',
    fontSize: 11,
  },
  tableHeader: {
    flexDirection: 'row',
    backgroundColor: '#F5E7CA',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    paddingVertical: 7,
    paddingHorizontal: 6,
  },
  tableHeaderText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3B2D17',
    paddingHorizontal: 4,
  },
  tableRow: {
    flexDirection: 'row',
    paddingVertical: 6,
    paddingHorizontal: 6,
    borderBottomWidth: 1,
    borderBottomColor: '#F0E6D2',
    alignItems: 'center',
  },
  tableCellText: {
    fontSize: 11,
    color: '#332717',
    paddingHorizontal: 4,
  },
  emptyTableState: {
    paddingVertical: 30,
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: '#FFFFFF',
  },
  tableTotalRow: {
    flexDirection: 'row',
    backgroundColor: '#F9F1DF',
    borderTopWidth: 1,
    borderTopColor: '#E5D6B6',
    paddingVertical: 7,
    paddingHorizontal: 6,
    alignItems: 'center',
  },
  tableTotalCellText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#32230D',
    paddingHorizontal: 4,
  },

  // Legacy Grid / Modals & Actions
  gridSection: { borderWidth: 1, borderRadius: 6, marginBottom: 12 },
  gridHeader: { flexDirection: 'row', paddingVertical: 6, paddingHorizontal: 4, borderBottomWidth: 1 },
  gridHeaderText: { fontSize: 12, fontWeight: 'bold', paddingHorizontal: 4 },
  gridRow: { flexDirection: 'row', paddingVertical: 4, paddingHorizontal: 4, borderBottomWidth: 1 },
  gridCellText: { fontSize: 12, paddingHorizontal: 4 },
  entrySection: { marginBottom: 16 },
  actionBtn: { paddingVertical: 6, paddingHorizontal: 16, borderRadius: 4, marginLeft: 8, borderWidth: 1 },
  footerSection: { flexDirection: 'row', justifyContent: 'space-between', alignItems: 'flex-start', paddingTop: 8, borderTopWidth: 1, borderTopColor: '#EADBCA' },
  mainActionBtn: { paddingVertical: 6, paddingHorizontal: 12, borderRadius: 5, marginLeft: 8, borderWidth: 1 },
  modalOverlay: { flex: 1, backgroundColor: 'rgba(0,0,0,0.5)', justifyContent: 'center', alignItems: 'center' },
  modalContainer: { width: '80%', maxHeight: '80%', borderWidth: 1, borderRadius: 8, padding: 16 },
  modalTitle: { fontSize: 18, fontWeight: 'bold', marginBottom: 12 },
  lovItem: { paddingVertical: 12, borderBottomWidth: 1 },
  toast: {
    position: 'absolute',
    top: 40,
    alignSelf: 'center',
    paddingHorizontal: 24,
    paddingVertical: 12,
    borderRadius: 8,
    elevation: 5,
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.25,
    shadowRadius: 3.84,
    zIndex: 9999,
  },
  toastText: {
    fontSize: 16,
    fontWeight: 'bold',
    color: '#FFFFFF',
  },
});
