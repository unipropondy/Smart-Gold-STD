import { ThemedText } from '@/components/themed-text';
import { API_BASE_URL } from '@/constants/api';
import { useThemeContext } from '@/contexts/ThemeContext';
import { useThemeColors } from '@/hooks/useThemeColors';
import { Ionicons } from '@expo/vector-icons';
import { Stack, useRouter } from 'expo-router';
import React, { useEffect, useRef, useState } from 'react';
import {
  ActivityIndicator,
  Alert,
  FlatList,
  Modal,
  Platform,
  SafeAreaView,
  ScrollView,
  StyleSheet,
  TextInput,
  TouchableOpacity,
  View
} from 'react-native';

const API_URL = `${API_BASE_URL}`;

export default function PurchaseScreen() {
  const themeColors = useThemeColors();
  const { accentColor } = useThemeContext();
  const {
    background: backgroundColor,
    card: cardColor,
    text: textColor,
    border: borderColor,
    subtitle: subtitleColor,
    primaryGold: GOLD_COLOR
  } = themeColors;

  const router = useRouter();

  const Checkbox = ({ label, selected, onPress }: { label: string; selected: boolean; onPress: () => void }) => (
    <TouchableOpacity style={styles.radioContainer} onPress={onPress}>
      <View
        style={[
          styles.checkboxSquare,
          { borderColor: borderColor, backgroundColor: selected ? accentColor : 'transparent' }
        ]}
      >
        {selected && <Ionicons name="checkmark" size={14} color="#FFF" />}
      </View>
      {label ? <ThemedText style={styles.radioLabel}>{label}</ThemedText> : null}
    </TouchableOpacity>
  );

  // ===== STATE =====
  const [headerState, setHeaderState] = useState({
    JobSheetNo: '',
    InvoiceNo: '',
    InvoiceDate: new Date().toISOString().split('T')[0],
    SupplierCode: '',
    SupplierName: '',
    Currency: 'SGD',
    Rate: '1',
    PayableGold: false,
    PayableCash: true,
    GST: false,
    Payable22K: true,
    Payable24K: false,
    TaelPercent: '0',
    TaelCostPerGram: '0',
    TaelValue: '0',
    Remarks: '',
    BillClose: 'N',
    TranType: '',
    TotalWeight24K: '0',
  });

  const [details, setDetails] = useState<any[]>([]);
  const [jobSheetDetails, setJobSheetDetails] = useState<any[]>([]);

  // Auto / Manual toggle (F10 mode in purchase.txt)
  const [isAutoManual, setIsAutoManual] = useState(false);
  // Auto SMC Checkbox in purchase.txt
  const [autoSMC, setAutoSMC] = useState(false);
  // Sales PCS Checkbox in purchase.txt
  const [salesPCS, setSalesPCS] = useState(false);

  // Additional description sub-modal
  const [addlDescModalVisible, setAddlDescModalVisible] = useState(false);
  // Image preview modal
  const [imageModalVisible, setImageModalVisible] = useState(false);

  const [currentDetail, setCurrentDetail] = useState<any>({
    CatalogueCode: '',
    CategoryCode: '',
    TrayId: '',
    BillWeight: '',
    BillPCS: '',
    InvWeight: '',
    InvPCS: '',
    PurchaseMC: '',
    SalesMC: '',
    PMCCost: '',
    SMCCost: '',
    Auto: false,
    PCSCheck: false,
    InventoryCode: '',
    InventoryDescription: '',
    Weight: '',
    Size: '',
    GoldLoss: '',
    CostPerGram: '',
    PMC: '',
    SMC: '',
    PCS: '1',
    TotalCost: '',
    DiamondDescription: '',
    StoneWeight: '',
    PurchaseMCType: 'PerGram', // 'PerGram' or 'PerPCS'
    SalesMCType: 'PerGram', // 'PerGram' or 'PerPCS'
    ClassCode: '',
    SizeType: '',
    IPM: false,
    IPMType: 'Coin',
    IPMBarCoin: '',
    IPMPurity: '',
    IPMRefiner: '',
    IPMCountry: '',
    IPMGSTType: '',
    IPMBarNo: '',
    AddlDesc: false,
    Desc1: '',
    Desc2: '',
    Desc3: '',
    Desc4: '',
    Cost1: '',
    Cost2: '',
    Cost3: '',
    Cost4: '',
    SellingPrice1: '',
    SellingPrice2: '',
    SellingPrice3: '',
    SellingPrice4: '',
    Image: '',
  });

  const [loading, setLoading] = useState(false);
  const [toast, setToast] = useState({ visible: false, message: '', type: 'success' });
  const toastTimeoutRef = useRef<any>(null);

  const showToast = (message: string, type: 'success' | 'error' | 'info' = 'success') => {
    if (toastTimeoutRef.current) {
      clearTimeout(toastTimeoutRef.current);
    }
    setToast({ visible: true, message, type });
    toastTimeoutRef.current = setTimeout(() => {
      setToast(prev => ({ ...prev, visible: false }));
      toastTimeoutRef.current = null;
    }, 3000);
  };

  const showAlert = (title: string, message: string, buttons?: { text: string; style?: 'cancel' | 'destructive' | 'default'; onPress?: () => void }[]) => {
    // If confirmation dialog with multiple action buttons
    if (buttons && buttons.length > 1) {
      if (Platform.OS === 'web') {
        const confirmBtn = buttons.find(b => b.style !== 'cancel') || buttons[1];
        const cancelBtn = buttons.find(b => b.style === 'cancel');
        if (window.confirm(`${title ? title + ':\n' : ''}${message}`)) {
          confirmBtn?.onPress?.();
        } else {
          cancelBtn?.onPress?.();
        }
      } else {
        Alert.alert(title, message, buttons);
      }
      return;
    }

    // Replace all alert popups with Toast message
    const toastType: 'success' | 'error' | 'info' =
      title.toLowerCase().includes('error') ? 'error' :
      title.toLowerCase().includes('success') ? 'success' : 'info';
    showToast(message, toastType);
    if (buttons && buttons[0]?.onPress) {
      buttons[0].onPress();
    }
  };

  // ================= RECALCULATE DETAIL COSTS (PMC Cost, SMC Cost, Total Cost) =================
  const recalculateDetailCosts = (detail: any, currentAutoSMC: boolean = autoSMC) => {
    const weight = parseFloat(detail.Weight) || 0;
    const stoneWeight = parseFloat(detail.StoneWeight) || 0;
    const goldLoss = parseFloat(detail.GoldLoss) || 0;
    const costPerGram = parseFloat(detail.CostPerGram) || 0;
    const pmc = parseFloat(detail.PMC) || 0;
    let smc = parseFloat(detail.SMC) || 0;
    const pcs = parseInt(detail.PCS, 10) || 1;
    const currencyRate = parseFloat(headerState.Rate) || 1;

    const netWeight = weight - stoneWeight > 0 ? weight - stoneWeight : weight;

    // PMC Cost calculation
    let pmcCost = 0;
    if (detail.PurchaseMCType === 'PerGram') {
      pmcCost = pmc * netWeight;
    } else {
      pmcCost = pmc * (pcs > 0 ? pcs : 1);
    }

    // Auto SMC calculation (chkAutoSMC in purchase.txt: SMC = PMC * 4)
    let smcCost = 0;
    if (currentAutoSMC) {
      smc = Math.round(pmc * 4);
      if (detail.SalesMCType === 'PerGram') {
        smcCost = Math.round(pmc * netWeight * 4);
      } else {
        smcCost = Math.round(pmc * (pcs > 0 ? pcs : 1) * 4);
      }
    } else {
      if (detail.SalesMCType === 'PerGram') {
        smcCost = Math.round(smc * netWeight);
      } else {
        smcCost = Math.round(smc * (pcs > 0 ? pcs : 1));
      }
    }

    // Total Cost calculation from purchase.txt FillTotalCost:
    // dCost = (netWeight + goldLoss) * costPerGram + pmcCost
    let dCost = (netWeight + goldLoss) * costPerGram + pmcCost;

    // Add Additional Desc costs if enabled
    if (detail.AddlDesc) {
      const c1 = parseFloat(detail.Cost1) || 0;
      const c2 = parseFloat(detail.Cost2) || 0;
      const c3 = parseFloat(detail.Cost3) || 0;
      const c4 = parseFloat(detail.Cost4) || 0;
      dCost += c1 + c2 + c3 + c4;
      pmcCost += c1 + c2 + c3 + c4;
    }

    const dLocalCost = currencyRate !== 0 ? dCost : dCost;

    return {
      ...detail,
      SMC: currentAutoSMC ? smc.toFixed(2) : detail.SMC,
      PMCCost: pmcCost.toFixed(2),
      SMCCost: smcCost.toFixed(2),
      TotalCost: dLocalCost.toFixed(2),
    };
  };

  // Keep Detail costs calculated on dependent field changes
  useEffect(() => {
    const updated = recalculateDetailCosts(currentDetail, autoSMC);
    if (
      updated.PMCCost !== currentDetail.PMCCost ||
      updated.SMCCost !== currentDetail.SMCCost ||
      updated.TotalCost !== currentDetail.TotalCost ||
      (autoSMC && updated.SMC !== currentDetail.SMC)
    ) {
      setCurrentDetail((prev: any) => ({
        ...prev,
        PMCCost: updated.PMCCost,
        SMCCost: updated.SMCCost,
        TotalCost: updated.TotalCost,
        ...(autoSMC ? { SMC: updated.SMC } : {})
      }));
    }
  }, [
    currentDetail.Weight,
    currentDetail.StoneWeight,
    currentDetail.GoldLoss,
    currentDetail.CostPerGram,
    currentDetail.PMC,
    currentDetail.SMC,
    currentDetail.PCS,
    currentDetail.PurchaseMCType,
    currentDetail.SalesMCType,
    currentDetail.AddlDesc,
    currentDetail.Cost1,
    currentDetail.Cost2,
    currentDetail.Cost3,
    currentDetail.Cost4,
    autoSMC,
    headerState.Rate
  ]);

  const [selectedIndex, setSelectedIndex] = useState<number | null>(null);
  const [detailModalVisible, setDetailModalVisible] = useState(false);

  // LOV State
  const [lovVisible, setLovVisible] = useState(false);
  const [lovTitle, setLovTitle] = useState('');
  const [lovData, setLovData] = useState<any[]>([]);
  const [filteredLovData, setFilteredLovData] = useState<any[]>([]);
  const [lovSearch, setLovSearch] = useState('');
  const [lovColumns, setLovColumns] = useState<string[]>([]);
  const [onLovSelect, setOnLovSelect] = useState<((item: any) => void) | null>(null);

  // Computed totals for Category summary grid
  const categoryTotals = jobSheetDetails.reduce((acc, curr) => {
    if (!curr.CategoryCode) return acc;
    const catCode = curr.CategoryCode.trim();
    if (!acc[catCode]) {
      acc[catCode] = { CategoryCode: catCode, BillWeight: 0, InventoryWeight: 0 };
    }
    acc[catCode].BillWeight += parseFloat(curr.Weight) || 0;
    return acc;
  }, {} as Record<string, any>);

  // Also include inventory weights from the details
  details.forEach(curr => {
    if (!curr.CategoryCode) return;
    const catCode = curr.CategoryCode.trim();
    if (!categoryTotals[catCode]) {
      categoryTotals[catCode] = { CategoryCode: catCode, BillWeight: 0, InventoryWeight: 0 };
    }
    categoryTotals[catCode].InventoryWeight += parseFloat(curr.Weight) || 0;
  });

  const categorySummaryList = Object.values(categoryTotals).map((cat: any) => ({
    ...cat,
    Balance: (parseFloat(cat.BillWeight) || 0) - (parseFloat(cat.InventoryWeight) || 0)
  }));

  // ===== FUNCTIONS =====
  const updateHeader = (key: string, value: any) => setHeaderState(prev => ({ ...prev, [key]: value }));
  const updateDetail = (key: string, value: any) => setCurrentDetail((prev: any) => ({ ...prev, [key]: value }));

  const fetchSuppliers = async () => {
    try {
      const res = await fetch(`${API_URL}/supplier`);
      const json = await res.json();
      if (json.success) {
        openLov('Select Supplier', json.data, ['SupplierCode', 'SupplierName'], (item) => {
          updateHeader('SupplierCode', item.SupplierCode || '');
          updateHeader('SupplierName', item.SupplierName || '');
        });
      }
    } catch (e) {
      showAlert("Error", "Error fetching suppliers");
    }
  };

  const fetchJobsheets = async () => {
    try {
      const res = await fetch(`${API_URL}/jobsheet`);
      const json = await res.json();
      if (json.success) {
        openLov('Select Job Sheet', json.data, ['JobSheetNo', 'SupplierCode', 'PurchaseBillNo', 'TotalWeight24K'], async (item) => {
          await loadJobSheetByNo(item.JobSheetNo);
        });
      }
    } catch (e) {
      showAlert("Error", "Error fetching jobsheets");
    }
  };

  const loadJobSheetByNo = async (jsNo: string) => {
    if (!jsNo || !jsNo.trim()) return;
    try {
      setLoading(true);
      const detailRes = await fetch(`${API_URL}/jobsheet/${jsNo.trim()}`);
      const detailJson = await detailRes.json();
      setLoading(false);

      if (detailJson.success && detailJson.header) {
        const h = detailJson.header;
        const isClosed = String(h.BillClose || '').toUpperCase() === 'Y';
        const isDirectPur = String(h.TranType || '').toUpperCase() === 'DP';

        if (isClosed) {
          showAlert("Notice", "This bill is closed! Cannot modify!");
        } else if (isDirectPur) {
          showAlert("Notice", "This is a direct purchase bill. Cannot modify!");
        }

        setHeaderState(prev => ({
          ...prev,
          JobSheetNo: h.JobSheetNo || jsNo.trim(),
          InvoiceNo: h.PurchaseBillNo || prev.InvoiceNo,
          InvoiceDate: h.PurchaseBillDate ? h.PurchaseBillDate.split('T')[0] : prev.InvoiceDate,
          SupplierCode: h.SupplierCode || '',
          SupplierName: h.SupplierName || '',
          Currency: h.CurrencyCode || 'SGD',
          Rate: (h.CurrencyRate || 1).toString(),
          PayableGold: h.PaymentType === 'G',
          PayableCash: h.PaymentType !== 'G',
          GST: h.TaelGST === 'Y',
          Payable22K: h.ClassType === '22K',
          Payable24K: h.ClassType === '24K',
          TaelValue: (h.TaelRate || 0).toString(),
          Remarks: h.Remarks || '',
          TaelPercent: (h.TaelPercent || 0).toString(),
          TaelCostPerGram: (h.CostPerGram || 0).toString(),
          BillClose: h.BillClose || 'N',
          TranType: h.TranType || '',
          TotalWeight24K: (h.TotalWeight24K || 0).toString(),
        }));

        if (detailJson.details && detailJson.details.length > 0) {
          setJobSheetDetails(detailJson.details);
        } else {
          setJobSheetDetails([]);
        }

        if (detailJson.inventory && detailJson.inventory.length > 0) {
          const mapped = detailJson.inventory.map((d: any) => ({
            CatalogueCode: d.CatalogueCode || '',
            CategoryCode: d.CategoryCode || '',
            InventoryDescription: d.Description || '',
            Description: d.Description || '',
            Weight: (d.Weight || 0).toString(),
            CostPerGram: (d.CostPerGram || 0).toString(),
            PMC: (d.PMC || 0).toString(),
            SMC: (d.SMC || 0).toString(),
            PCS: (d.PCS || 1).toString(),
            GoldLoss: (d.GoldLoss || 0).toString(),
            TotalCost: (d.TotalCost || 0).toString(),
            ClassCode: d.ClassCode || '',
            TrayId: d.TrayId || '',
            Size: (d.Size || '').toString(),
            InventoryCode: d.InventoryCode || '',
            DiamondDescription: d.DiamondDescription || '',
            StoneWeight: (d.StoneWeight || 0).toString(),
            PurchaseMCType: d.PMCType === 'P' ? 'PerPCS' : 'PerGram',
            SalesMCType: d.SMCType === 'P' ? 'PerPCS' : 'PerGram',
            InvWeight: '',
            InvPCS: '',
            BillWeight: '',
            BillPCS: '',
            PurchaseMC: (d.PMC || 0).toString(),
            SalesMC: (d.SMC || 0).toString(),
            PMCCost: (d.PMC || 0).toString(),
            SMCCost: (d.SMC || 0).toString(),
            Auto: false,
            PCSCheck: false,
            IPM: false,
            AddlDesc: false
          }));
          setDetails(mapped);
        } else {
          setDetails([]);
        }
      } else {
        showAlert("Error", "Job sheet no. is invalid!");
        clearAll();
      }
    } catch (e) {
      setLoading(false);
      showAlert("Error", "Error fetching jobsheet details");
    }
  };

  const fetchCatalogue = async () => {
    try {
      const res = await fetch(`${API_URL}/catalogue/lov/catalogue`);
      const json = await res.json();
      if (json.success) {
        // If JobSheet details are available, filter catalogue items that belong to the job sheet if possible
        const validCatalogues = json.data;
        openLov('Select Catalogue', validCatalogues, ['CatalogueCode', 'CatalogueName', 'CategoryCode', 'GoldLoss'], (item) => {
          applySelectedCatalogue(item);
        });
      }
    } catch (e) {
      showAlert("Error", "Error fetching catalogue");
    }
  };

  const applySelectedCatalogue = async (item: any) => {
    let itemCategoryCode = (item.CategoryCode || item.Categorycode || '').trim();
    const itemCatalogueCode = (item.CatalogueCode || item.Cataloguecode || '').trim();

    // If item doesn't have CategoryCode, look it up from jobSheetDetails or fetch catalogue
    if (!itemCategoryCode) {
      const matchInJS = jobSheetDetails.find(
        j => (j.CatalogueCode || '').trim().toUpperCase() === itemCatalogueCode.toUpperCase()
      );
      if (matchInJS && matchInJS.CategoryCode) {
        itemCategoryCode = matchInJS.CategoryCode.trim();
      } else {
        try {
          const res = await fetch(`${API_URL}/catalogue/lov/catalogue`);
          const json = await res.json();
          if (json.success && Array.isArray(json.data)) {
            const found = json.data.find((c: any) => (c.CatalogueCode || '').trim().toUpperCase() === itemCatalogueCode.toUpperCase());
            if (found && found.CategoryCode) {
              itemCategoryCode = found.CategoryCode.trim();
              if (!item.CatalogueName) item.CatalogueName = found.CatalogueName;
            }
          }
        } catch (e) {
          console.error("Error looking up catalogue category:", e);
        }
      }
    }

    // Validate catalogue belongs to selected JobSheet if JobSheet has detail lines
    if (jobSheetDetails.length > 0) {
      const isCatalogueInJS = jobSheetDetails.some(
        j => (j.CatalogueCode || '').trim().toUpperCase() === itemCatalogueCode.toUpperCase()
      );
      if (!isCatalogueInJS) {
        showToast("Please select the suitable catalogue for the selected job sheet!", "error");
        return;
      }
    }

    const catSummary = categorySummaryList.find(c => (c.CategoryCode || '').trim() === itemCategoryCode);
    const jsDetail = jobSheetDetails.find(j => (j.CatalogueCode || '').trim() === itemCatalogueCode)
      || jobSheetDetails.find(j => (j.CategoryCode || '').trim() === itemCategoryCode);

    let nextInvCode = jsDetail?.NextInventoryCode || '';
    let classCode = jsDetail?.ClassCode || '';

    // If NextInventoryCode is not yet available, fetch category directly to get NextInventoryCode and ClassCode
    if (!nextInvCode && itemCategoryCode) {
      try {
        const catRes = await fetch(`${API_URL}/category`);
        const catJson = await catRes.json();
        if (catJson.success && Array.isArray(catJson.data)) {
          const catObj = catJson.data.find((c: any) => (c.CategoryCode || '').trim().toUpperCase() === itemCategoryCode.toUpperCase());
          if (catObj) {
            nextInvCode = catObj.NextInventoryCode || '';
            if (catObj.ClassCode) classCode = catObj.ClassCode;
          }
        }
      } catch (err) {
        console.error("Error fetching next inventory code:", err);
      }
    }

    const jsCost = jsDetail?.CostPerGram ?? jsDetail?.CostperGram;
    const jsPCS = jsDetail?.PCS ?? jsDetail?.Pcs ?? jsDetail?.pcs;
    const jsGoldLoss = jsDetail?.GoldLoss ?? jsDetail?.Goldloss;
    const jsWeight = jsDetail?.Weight ?? jsDetail?.weight;
    const jsSize = jsDetail?.Size ?? jsDetail?.size;
    const jsPMC = jsDetail?.MC ?? jsDetail?.PMC ?? jsDetail?.pmc;
    const jsSMC = jsDetail?.SMC ?? jsDetail?.smc ?? jsDetail?.MC;

    setCurrentDetail((prev: any) => {
      const updated = {
        ...prev,
        CatalogueCode: itemCatalogueCode || prev.CatalogueCode || '',
        CategoryCode: itemCategoryCode || prev.CategoryCode || '',
        ClassCode: classCode || jsDetail?.ClassCode || prev.ClassCode || '',
        InventoryDescription: jsDetail?.Description || prev.InventoryDescription || item.CatalogueName || item.Cataloguename || '',
        Description: jsDetail?.Description || prev.Description || item.CatalogueName || item.Cataloguename || '',
        CostPerGram: jsCost !== undefined ? jsCost.toString() : (headerState.TaelCostPerGram || prev.CostPerGram || ''),
        InventoryCode: nextInvCode || prev.InventoryCode || '',
        BillWeight: catSummary?.BillWeight ? catSummary.BillWeight.toFixed(2) : '0.00',
        BillPCS: jsPCS !== undefined ? jsPCS.toString() : '0',
        InvWeight: catSummary?.InventoryWeight ? catSummary.InventoryWeight.toFixed(2) : '0.00',
        InvPCS: details.filter(d => (d.CategoryCode || '').trim() === itemCategoryCode).reduce((sum, d) => sum + (parseInt(d.PCS, 10) || 1), 0).toString(),
        PMC: jsPMC !== undefined ? jsPMC.toString() : (prev.PMC || ''),
        SMC: jsSMC !== undefined ? jsSMC.toString() : (prev.SMC || ''),
        PCS: jsPCS !== undefined ? jsPCS.toString() : (prev.PCS || '1'),
        GoldLoss: jsGoldLoss !== undefined ? jsGoldLoss.toString() : (prev.GoldLoss || ''),
        Size: jsSize !== undefined ? jsSize.toString() : (prev.Size || ''),
        Weight: jsWeight !== undefined ? jsWeight.toString() : (prev.Weight || ''),
        PurchaseMCType: jsDetail?.PMCType === 'P' || jsDetail?.MCType === 'P' ? 'PerPCS' : 'PerGram',
        SalesMCType: jsDetail?.SMCType === 'P' || jsDetail?.SMcType === 'P' ? 'PerPCS' : 'PerGram'
      };
      return recalculateDetailCosts(updated);
    });
  };

  const applySelectedCategory = async (catCode: string, itemObj?: any) => {
    const trimmedCat = (catCode || '').trim();
    if (!trimmedCat) return;

    if (jobSheetDetails.length > 0) {
      const isValid = jobSheetDetails.some(j => (j.CategoryCode || '').trim().toUpperCase() === trimmedCat.toUpperCase());
      if (!isValid) {
        showToast("Please select the suitable category for the selected job sheet!", "error");
        return;
      }
    }

    const jsDetail = jobSheetDetails.find(j => (j.CategoryCode || '').trim().toUpperCase() === trimmedCat.toUpperCase());
    let nextInvCode = itemObj?.NextInventoryCode || jsDetail?.NextInventoryCode || '';
    let classCode = itemObj?.ClassCode || jsDetail?.ClassCode || '';

    if (!nextInvCode) {
      try {
        const catRes = await fetch(`${API_URL}/category`);
        const catJson = await catRes.json();
        if (catJson.success && Array.isArray(catJson.data)) {
          const catObj = catJson.data.find((c: any) => (c.CategoryCode || '').trim().toUpperCase() === trimmedCat.toUpperCase());
          if (catObj) {
            nextInvCode = catObj.NextInventoryCode || '';
            if (catObj.ClassCode) classCode = catObj.ClassCode;
          }
        }
      } catch (err) {
        console.error("Error fetching category next inventory code:", err);
      }
    }

    setCurrentDetail((prev: any) => {
      const updated = {
        ...prev,
        CategoryCode: trimmedCat,
        ...(classCode ? { ClassCode: classCode } : {}),
        ...(nextInvCode ? { InventoryCode: nextInvCode } : {}),
        ...(jsDetail?.CostPerGram ? { CostPerGram: jsDetail.CostPerGram.toString() } : {}),
        ...(jsDetail?.Description ? { InventoryDescription: jsDetail.Description, Description: jsDetail.Description } : {})
      };
      return recalculateDetailCosts(updated);
    });
  };

  const fetchCategories = async () => {
    try {
      const res = await fetch(`${API_URL}/category`);
      const json = await res.json();
      if (json.success) {
        openLov('Select Category', json.data, ['CategoryCode', 'CategoryName', 'ClassCode'], (item) => {
          applySelectedCategory(item.CategoryCode || '', item);
        });
      }
    } catch (e) {
      showAlert("Error", "Error fetching categories");
    }
  };

  const fetchTrays = async () => {
    try {
      const res = await fetch(`${API_URL}/traymaster`);
      const json = await res.json();
      if (json.success) {
        openLov('Select Tray ID', json.data, ['TrayID', 'TrayName'], (item) => {
          const selectedTray = item.TrayID || item.TrayId || item.trayid || item.TRAYID || '';
          updateDetail('TrayId', String(selectedTray).trim());
        });
      }
    } catch (e) {
      showAlert("Error", "Error fetching trays");
    }
  };

  const fetchSizes = async () => {
    try {
      const res = await fetch(`${API_URL}/size`);
      const json = await res.json();
      if (json.success) {
        openLov('Select Size', json.data, ['SizeCode', 'Remarks'], (item) => {
          const selectedSize = item.SizeCode || item.Sizecode || item.sizecode || item.SIZECODE || '';
          updateDetail('Size', String(selectedSize).trim());
          if (item.SizeType) updateDetail('SizeType', item.SizeType.trim());
        });
      }
    } catch (e) {
      showAlert("Error", "Error fetching sizes");
    }
  };

  const fetchCurrencies = async () => {
    try {
      const res = await fetch(`${API_URL}/currency`);
      const json = await res.json();
      if (json.success) {
        openLov('Select Currency', json.data, ['CurrencyCode', 'CurrencyName', 'CurrencyRate'], (item) => {
          updateHeader('Currency', item.CurrencyCode);
          updateHeader('Rate', (item.CurrencyRate || 1).toString());
        });
      }
    } catch (e) {
      showAlert("Error", "Error fetching currencies");
    }
  };

  const fetchRefiners = async () => {
    try {
      const res = await fetch(`${API_URL}/refiner`);
      const json = await res.json();
      if (json.success) {
        openLov('Select Refiner', json.data, ['RefinerCode', 'RefinerName'], (item) => {
          updateDetail('IPMRefiner', item.RefinerName || item.RefinerCode);
        });
      }
    } catch (e) {
      showAlert("Error", "Error fetching refiners");
    }
  };

  const fetchCountries = async () => {
    try {
      const res = await fetch(`${API_URL}/country`);
      const json = await res.json();
      if (json.success) {
        openLov('Select Country', json.data, ['CountryCode', 'CountryName'], (item) => {
          updateDetail('IPMCountry', item.CountryName || item.CountryCode);
        });
      }
    } catch (e) {
      showAlert("Error", "Error fetching countries");
    }
  };

  const openLov = (title: string, data: any[], columns: string[], onSelect: (item: any) => void) => {
    setLovTitle(title);
    setLovData(data);
    setFilteredLovData(data);
    setLovColumns(columns);
    setLovSearch('');
    setOnLovSelect(() => onSelect);
    setLovVisible(true);
  };

  const handleLovSearch = (text: string) => {
    setLovSearch(text);
    if (!text) {
      setFilteredLovData(lovData);
      return;
    }
    const lower = text.toLowerCase();
    const filtered = lovData.filter(item => {
      return Object.values(item).some(val => String(val).toLowerCase().includes(lower));
    });
    setFilteredLovData(filtered);
  };

  const handleLovSelect = (item: any) => {
    if (onLovSelect) onLovSelect(item);
    setLovVisible(false);
  };

  // ================= VALIDATIONS BASED ON purchase.txt fncValidateInventory =================
  const validateInventory = (): boolean => {
    if (headerState.BillClose === 'Y') {
      showToast("This bill is closed! Cannot modify!", "error");
      return false;
    }
    if (headerState.TranType === 'DP') {
      showToast("This is a direct purchase bill. Cannot modify!", "error");
      return false;
    }
    if (!currentDetail.InventoryCode?.trim()) {
      showToast("Inventory Code Cannot be Blank ....", "error");
      return false;
    }
    if (!currentDetail.CatalogueCode?.trim()) {
      showToast("Catalogue Code Cannot be Blank ....", "error");
      return false;
    }
    if (!currentDetail.CategoryCode?.trim()) {
      showToast("Category Code Cannot be Blank ....", "error");
      return false;
    }
    if (!currentDetail.TrayId?.trim()) {
      showToast("Tray Id Cannot be Blank ....", "error");
      return false;
    }
    if (!currentDetail.InventoryDescription?.trim()) {
      showToast("Description Cannot be Blank ....", "error");
      return false;
    }
    const weightVal = parseFloat(currentDetail.Weight) || 0;
    if (salesPCS) {
      if (weightVal < 0) {
        showToast("Invalid weight!", "error");
        return false;
      }
    } else {
      if (weightVal <= 0) {
        showToast("Invalid weight!", "error");
        return false;
      }
    }
    const costPerGramVal = parseFloat(currentDetail.CostPerGram) || 0;
    if (costPerGramVal <= 0) {
      showToast("Invalid cost per gram!", "error");
      return false;
    }
    const pcsVal = parseInt(currentDetail.PCS, 10) || 0;
    if (pcsVal < 1) {
      showToast("Invalid pcs!", "error");
      return false;
    }

    // IPM Validations (from purchase.txt)
    if (currentDetail.IPM) {
      if (!currentDetail.IPMType?.trim()) {
        showToast("Type Cannot be Blank ....", "error");
        return false;
      }
      if (!currentDetail.IPMBarNo?.trim()) {
        showToast("BarNo Cannot be Blank ....", "error");
        return false;
      }
      if (!currentDetail.IPMPurity?.trim()) {
        showToast("Purity Cannot be Blank ....", "error");
        return false;
      }
      if (!currentDetail.IPMGSTType?.trim()) {
        showToast("GST Type Cannot be Blank ....", "error");
        return false;
      }
      if (!currentDetail.IPMRefiner?.trim()) {
        showToast("Refiner Cannot be Blank ....", "error");
        return false;
      }
      if (!currentDetail.IPMCountry?.trim()) {
        showToast("Country Cannot be Blank ....", "error");
        return false;
      }
    }

    return true;
  };

  // ================= CHECK WEIGHT / PCS (fncCheckWtPCS in purchase.txt) =================
  const checkWtPCS = (): boolean => {
    const catCode = (currentDetail.CatalogueCode || '').trim();
    const currWt = (parseFloat(currentDetail.Weight) || 0) - (parseFloat(currentDetail.StoneWeight) || 0);

    // Sum previous lines for this catalogue
    let totalWt = currWt;
    details.forEach((d, idx) => {
      if (selectedIndex !== null && idx === selectedIndex) return; // skip currently edited line
      if ((d.CatalogueCode || '').trim().toUpperCase() === catCode.toUpperCase()) {
        const netW = (parseFloat(d.Weight) || 0) - (parseFloat(d.StoneWeight) || 0);
        totalWt += netW;
      }
    });

    // Lookup max weight from JobSheetDetail
    const jsDetail = jobSheetDetails.find(j => (j.CatalogueCode || '').trim().toUpperCase() === catCode.toUpperCase());
    if (jsDetail) {
      const maxWt = parseFloat(jsDetail.Weight) || 0;
      // Allow 5% tolerance as in purchase.txt (dPurWtVarPerc default 5%)
      const allMaxWt = maxWt + (maxWt * 0.05);

      if (maxWt > 0 && totalWt > allMaxWt) {
        showToast(
          `Notice: Inventory Weight(${totalWt.toFixed(2)} gm) exceeds the Catalogue Weight(${maxWt.toFixed(2)} gm)!`,
          "info"
        );
      }
    }

    return true;
  };

  // ================= ADD / UPDATE DETAIL =================
  const addDetail = () => {
    if (!validateInventory()) return;
    if (!checkWtPCS()) return;

    const calculatedDetail = recalculateDetailCosts(currentDetail, autoSMC);
    let newDetails = [...details];

    if (selectedIndex !== null) {
      newDetails[selectedIndex] = calculatedDetail;
      showToast("Item updated successfully", "success");
    } else {
      newDetails.push(calculatedDetail);
      showToast("Item added successfully", "success");
    }

    setDetails(newDetails);
    setSelectedIndex(null);
    clearCurrentDetail();
    setDetailModalVisible(false);
  };

  // ================= DELETE DETAIL (from grid or modal) =================
  const deleteSelectedDetail = () => {
    if (selectedIndex === null) {
      showAlert("Validation", "Select item for deletion!");
      return;
    }

    if (headerState.BillClose === 'Y') {
      showAlert("Warning", "This bill is closed. Cannot delete!");
      return;
    }
    if (headerState.TranType === 'DP') {
      showAlert("Warning", "This is a direct purchase bill. Cannot delete!");
      return;
    }

    const itemToDelete = details[selectedIndex];
    showAlert(
      "Confirm Deletion",
      `Do you want to delete item no. ${itemToDelete?.InventoryCode || selectedIndex + 1}?`,
      [
        { text: "No", style: "cancel" },
        {
          text: "Yes",
          style: "destructive",
          onPress: () => {
            let newDetails = [...details];
            newDetails.splice(selectedIndex, 1);
            setDetails(newDetails);
            setSelectedIndex(null);
            clearCurrentDetail();
            showToast("Item deleted successfully", "success");
            setDetailModalVisible(false);
          }
        }
      ]
    );
  };

  const clearCurrentDetail = () => {
    setCurrentDetail({
      CatalogueCode: '',
      CategoryCode: '',
      TrayId: '',
      BillWeight: '',
      BillPCS: '',
      InvWeight: '',
      InvPCS: '',
      PurchaseMC: '',
      SalesMC: '',
      PMCCost: '',
      SMCCost: '',
      Auto: false,
      PCSCheck: false,
      InventoryCode: '',
      InventoryDescription: '',
      Weight: '',
      Size: '',
      GoldLoss: '',
      CostPerGram: '',
      PMC: '',
      SMC: '',
      PCS: '1',
      TotalCost: '',
      DiamondDescription: '',
      StoneWeight: '',
      PurchaseMCType: 'PerGram',
      SalesMCType: 'PerGram',
      ClassCode: '',
      SizeType: '',
      IPM: false,
      IPMType: 'Coin',
      IPMBarCoin: '',
      IPMPurity: '',
      IPMRefiner: '',
      IPMCountry: '',
      IPMGSTType: '',
      IPMBarNo: '',
      AddlDesc: false,
      Desc1: '',
      Desc2: '',
      Desc3: '',
      Desc4: '',
      Cost1: '',
      Cost2: '',
      Cost3: '',
      Cost4: '',
      SellingPrice1: '',
      SellingPrice2: '',
      SellingPrice3: '',
      SellingPrice4: '',
      Image: '',
    });
  };

  // ================= SAVE PURCHASE =================
  const savePurchase = async () => {
    if (!headerState.InvoiceNo?.trim()) {
      showToast("Invoice No is required", "error");
      return;
    }

    if (details.length === 0) {
      showToast("At least one line item is required", "error");
      return;
    }

    setLoading(true);
    try {
      const mappedDetails = details.map(d => ({
        ...d,
        PMCType: d.PurchaseMCType === 'PerPCS' ? 'P' : 'W',
        SMCType: d.SalesMCType === 'PerPCS' ? 'P' : 'W',
        MC: parseFloat(d.PMCCost || d.PMC) || 0,
        Weight: parseFloat(d.Weight) || 0,
        StoneWeight: parseFloat(d.StoneWeight) || 0,
        GoldLoss: parseFloat(d.GoldLoss) || 0,
        CostPerGram: parseFloat(d.CostPerGram) || 0,
        TotalCost: parseFloat(d.TotalCost) || 0,
        PCS: parseInt(d.PCS, 10) || 1,
      }));

      const payload = {
        header: {
          ...headerState,
          PurchaseBillNo: headerState.InvoiceNo.trim(),
          JobSheetNo: headerState.JobSheetNo.trim(),
          PaymentType: headerState.PayableGold ? 'G' : 'C',
        },
        details: mappedDetails
      };

      const res = await fetch(`${API_URL}/purchase`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(payload)
      });
      const json = await res.json();
      setLoading(false);

      if (json.success) {
        showToast("Purchase saved successfully!", "success");
        clearAll();
      } else {
        showToast(json.message || "Error saving purchase", "error");
      }
    } catch (e) {
      setLoading(false);
      showToast("Error saving purchase", "error");
    }
  };

  // ================= CLOSE BILL (btnBillClose in purchase.txt) =================
  const handleBillClose = async () => {
    if (!headerState.JobSheetNo?.trim()) {
      showAlert("Validation", "Please enter the job sheet no.!");
      return;
    }
    if (headerState.BillClose === 'Y') {
      showAlert("Notice", "This Job sheet bill is already closed!");
      return;
    }
    if (headerState.TranType === 'DP') {
      showAlert("Notice", "This is a direct purchase bill. Use direct purchase screen to close this bill!");
      return;
    }

    // Compare total job sheet weight vs total purchase weight
    const totJobSheetWt = parseFloat(headerState.TotalWeight24K) || 0;
    const totPurWt = details.reduce((sum, item) => sum + (parseFloat(item.Weight) || 0), 0);
    const wtDiff = Math.abs(totJobSheetWt - totPurWt);
    const allowedDiff = (totJobSheetWt * 0.05); // 5% tolerance

    if (totJobSheetWt > 0 && wtDiff > allowedDiff) {
      showAlert(
        "Cannot Close Bill",
        `Cannot close bill. The Inventory weight differs by more than 5.00% of the job sheet weight!`
      );
      return;
    }

    showAlert(
      "Close Job Sheet Bill",
      "You cannot make changes to closed bill. Would you like to close this job sheet bill?",
      [
        { text: "Cancel", style: "cancel" },
        {
          text: "OK",
          onPress: async () => {
            try {
              setLoading(true);
              const res = await fetch(`${API_URL}/purchase/billclose`, {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ jobSheetNo: headerState.JobSheetNo.trim() })
              });
              const json = await res.json();
              setLoading(false);

              if (json.success) {
                updateHeader('BillClose', 'Y');
                showToast("Job sheet bill closed!", "success");
              } else {
                showToast(json.message || "Failed to close bill", "error");
              }
            } catch (err) {
              setLoading(false);
              showToast("Error closing bill", "error");
            }
          }
        }
      ]
    );
  };

  // ================= CLEAR ALL (btnBillClear in purchase.txt) =================
  const clearAll = () => {
    setHeaderState({
      JobSheetNo: '',
      InvoiceNo: '',
      InvoiceDate: new Date().toISOString().split('T')[0],
      SupplierCode: '',
      SupplierName: '',
      Currency: 'SGD',
      Rate: '1',
      PayableGold: false,
      PayableCash: true,
      GST: false,
      Payable22K: true,
      Payable24K: false,
      TaelPercent: '0',
      TaelCostPerGram: '0',
      TaelValue: '0',
      Remarks: '',
      BillClose: 'N',
      TranType: '',
      TotalWeight24K: '0',
    });
    setDetails([]);
    setJobSheetDetails([]);
    setSelectedIndex(null);
    clearCurrentDetail();
  };

  // Navigate to barcode screen
  const navigateToBarcode = (invCode?: string) => {
    const code = invCode || currentDetail.InventoryCode || (selectedIndex !== null ? details[selectedIndex]?.InventoryCode : '');
    if (!code) {
      showAlert("Notice", "Please select an item with an Inventory Code first");
      return;
    }
    router.push({ pathname: '/barcode', params: { inventoryCode: code } });
  };

  const isFormLocked = headerState.BillClose === 'Y' || headerState.TranType === 'DP';

  return (
    <SafeAreaView style={{ flex: 1, backgroundColor: '#FBF9F5' }}>
      <Stack.Screen
        options={{
          headerTitle: 'JobSheet Purchase',
          headerStyle: { backgroundColor: '#FFFFFF' },
          headerTintColor: '#3B2D17',
          headerShadowVisible: false,
          headerRight: () => (
            <View style={{ flexDirection: 'row', alignItems: 'center', paddingRight: 8 }}>
              {/* Auto / Manual Mode Pill Button */}
              <TouchableOpacity
                style={[
                  styles.topActionBtn,
                  {
                    backgroundColor: isAutoManual ? '#FDE8E8' : '#E8F5E9',
                    borderColor: isAutoManual ? '#D32F2F' : '#388E3C'
                  }
                ]}
                onPress={() => {
                  setIsAutoManual(!isAutoManual);
                  showToast(isAutoManual ? "Switched to MANUAL mode" : "Switched to AUTO mode", "info");
                }}
              >
                <ThemedText
                  style={{
                    color: isAutoManual ? '#C62828' : '#2E7D32',
                    fontWeight: 'bold',
                    fontSize: 11
                  }}
                >
                  {isAutoManual ? 'AUTO' : 'MANUAL'}
                </ThemedText>
              </TouchableOpacity>

              {/* Bill Close Button (F7) */}
              <TouchableOpacity
                style={[
                  styles.topActionBtn,
                  {
                    backgroundColor: headerState.BillClose === 'Y' ? '#E0E0E0' : '#FBE9E7',
                    borderColor: '#FF7043'
                  }
                ]}
                onPress={handleBillClose}
                disabled={headerState.BillClose === 'Y'}
              >
                <ThemedText style={{ color: '#D84315', fontWeight: 'bold', fontSize: 11 }}>
                  {headerState.BillClose === 'Y' ? 'Closed' : 'BillClose'}
                </ThemedText>
              </TouchableOpacity>

              {/* Clear Button (F6) */}
              <TouchableOpacity style={styles.topActionBtn} onPress={clearAll}>
                <ThemedText style={{ color: '#5C4A28', fontWeight: 'bold', fontSize: 11 }}>Clear</ThemedText>
              </TouchableOpacity>

              {/* Save Button */}
              <TouchableOpacity
                style={[
                  styles.topActionBtn,
                  { backgroundColor: '#F9F2E2', borderColor: '#B8903B' }
                ]}
                onPress={savePurchase}
                disabled={isFormLocked}
              >
                <ThemedText style={{ color: '#9E6D08', fontWeight: 'bold', fontSize: 11 }}>Save</ThemedText>
              </TouchableOpacity>
            </View>
          ),
        }}
      />

      <ScrollView style={styles.container} contentContainerStyle={styles.contentContainer}>
        {/* HEADER AREA */}
        <ScrollView horizontal={true} showsHorizontalScrollIndicator={true} style={{ marginBottom: 14 }} contentContainerStyle={{ flexGrow: 1 }}>
          <View style={{ flexDirection: 'row', width: '100%', minWidth: 1120 }}>
            {/* LEFT: JOB SHEET DETAILS */}
            <View style={[styles.goldCard, { flex: 7, minWidth: 740, marginRight: 14 }]}>
              <View style={styles.cardHeaderBanner}>
                <View style={styles.headerIconBadge}>
                  <Ionicons name="document-text" size={15} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitle}>Purchase JobSheet Details</ThemedText>
                {headerState.BillClose === 'Y' && (
                  <View style={styles.closedBadge}>
                    <ThemedText style={styles.closedBadgeText}>CLOSED</ThemedText>
                  </View>
                )}
              </View>

              <View style={styles.cardBody}>
                {/* Row 1: Job Sheet No, Invoice No, Invoice Date */}
                <View style={styles.row}>
                  <ThemedText style={styles.label}>Job Sheet No.</ThemedText>
                  <View style={{ width: 140, flexDirection: 'row' }}>
                    <TextInput
                      style={[styles.input, { width: 106, borderTopRightRadius: 0, borderBottomRightRadius: 0, borderRightWidth: 0 }]}
                      value={headerState.JobSheetNo}
                      onChangeText={(t) => updateHeader('JobSheetNo', t)}
                      onBlur={() => {
                        if (headerState.JobSheetNo) loadJobSheetByNo(headerState.JobSheetNo);
                      }}
                      editable={!isFormLocked}
                    />
                    <TouchableOpacity
                      style={styles.lookupBtn}
                      onPress={fetchJobsheets}
                      disabled={isFormLocked}
                    >
                      <ThemedText style={styles.lookupBtnText}>...</ThemedText>
                    </TouchableOpacity>
                  </View>

                  <ThemedText style={[styles.labelRight, { width: 84 }]}>Invoice No</ThemedText>
                  <TextInput
                    style={[styles.input, { flex: 1, minWidth: 120 }]}
                    value={headerState.InvoiceNo}
                    onChangeText={(t) => updateHeader('InvoiceNo', t)}
                    editable={!isFormLocked}
                  />

                  <ThemedText style={[styles.labelRight, { width: 92 }]}>Invoice Date</ThemedText>
                  <TextInput
                    style={[styles.input, { flex: 1, minWidth: 100 }]}
                    value={headerState.InvoiceDate}
                    onChangeText={(t) => updateHeader('InvoiceDate', t)}
                    placeholder="YYYY-MM-DD"
                  />
                </View>

                {/* Row 2: Supplier Code, Supplier Name */}
                <View style={styles.row}>
                  <ThemedText style={styles.label}>Supplier Code</ThemedText>
                  <View style={{ width: 140, flexDirection: 'row' }}>
                    <TextInput
                      style={[styles.input, { width: 106, borderTopRightRadius: 0, borderBottomRightRadius: 0, borderRightWidth: 0 }]}
                      value={headerState.SupplierCode}
                      onChangeText={(t) => updateHeader('SupplierCode', t)}
                      editable={!isFormLocked}
                    />
                    <TouchableOpacity
                      style={styles.lookupBtn}
                      onPress={fetchSuppliers}
                      disabled={isFormLocked}
                    >
                      <ThemedText style={styles.lookupBtnText}>...</ThemedText>
                    </TouchableOpacity>
                  </View>
                  <TextInput
                    style={[styles.input, styles.readonlyInput, { flex: 1, minWidth: 338, marginLeft: 8 }]}
                    value={headerState.SupplierName}
                    editable={false}
                  />
                </View>

                {/* Row 3: Currency, Rate */}
                <View style={styles.row}>
                  <ThemedText style={styles.label}>Currency</ThemedText>
                  <View style={{ width: 140, flexDirection: 'row' }}>
                    <TextInput
                      style={[styles.input, { width: 106, borderTopRightRadius: 0, borderBottomRightRadius: 0, borderRightWidth: 0 }]}
                      value={headerState.Currency}
                      onChangeText={(t) => updateHeader('Currency', t)}
                    />
                    <TouchableOpacity style={styles.lookupBtn} onPress={fetchCurrencies}>
                      <ThemedText style={styles.lookupBtnText}>...</ThemedText>
                    </TouchableOpacity>
                  </View>
                  <TextInput
                    style={[styles.input, styles.readonlyInput, { flex: 1, minWidth: 130, marginLeft: 8 }]}
                    value={headerState.Currency}
                    editable={false}
                  />

                  <ThemedText style={[styles.labelRight, { width: 70 }]}>Rate</ThemedText>
                  <TextInput
                    style={[styles.input, { flex: 0.8, minWidth: 90 }]}
                    value={headerState.Rate}
                    onChangeText={(t) => updateHeader('Rate', t)}
                    keyboardType="numeric"
                  />
                </View>

                {/* Row 4: Payable, GST, 22K/24K, TEAL %, TEAL, Cost/g */}
                <View style={[styles.row, { alignItems: 'center' }]}>
                  <ThemedText style={styles.label}>Payable</ThemedText>
                  <View style={{ flexDirection: 'row', alignItems: 'center', marginRight: 10 }}>
                    <Checkbox
                      label="Gold"
                      selected={headerState.PayableGold}
                      onPress={() => {
                        updateHeader('PayableGold', true);
                        updateHeader('PayableCash', false);
                      }}
                    />
                    <Checkbox
                      label="Cash"
                      selected={headerState.PayableCash}
                      onPress={() => {
                        updateHeader('PayableCash', true);
                        updateHeader('PayableGold', false);
                      }}
                    />
                  </View>

                  <ThemedText style={{ marginRight: 6, color: '#3B3020', fontWeight: 'bold', fontSize: 11 }}>GST</ThemedText>
                  <Checkbox
                    label=""
                    selected={headerState.GST}
                    onPress={() => updateHeader('GST', !headerState.GST)}
                  />

                  {/* 22K / 24K Pill Box */}
                  <View style={styles.pillBox}>
                    <Checkbox
                      label="22K"
                      selected={headerState.Payable22K}
                      onPress={() => {
                        updateHeader('Payable22K', true);
                        updateHeader('Payable24K', false);
                      }}
                    />
                    <Checkbox
                      label="24K"
                      selected={headerState.Payable24K}
                      onPress={() => {
                        updateHeader('Payable24K', true);
                        updateHeader('Payable22K', false);
                      }}
                    />
                  </View>

                  {/* TEAL % */}
                  <View style={{ flexDirection: 'row', marginLeft: 12, alignItems: 'center' }}>
                    <ThemedText style={{ color: '#4A3B25', fontSize: 11, marginRight: 5, fontWeight: 'bold' }}>TEAL %</ThemedText>
                    <TextInput
                      style={[styles.input, { width: 50, marginRight: 8 }]}
                      value={headerState.TaelPercent}
                      onChangeText={(t) => updateHeader('TaelPercent', t)}
                      keyboardType="numeric"
                    />

                    <ThemedText style={{ color: '#4A3B25', fontSize: 11, marginRight: 5, fontWeight: 'bold' }}>TEAL</ThemedText>
                    <TextInput
                      style={[styles.input, { width: 62, marginRight: 8 }]}
                      value={headerState.TaelValue}
                      onChangeText={(t) => updateHeader('TaelValue', t)}
                      keyboardType="numeric"
                    />

                    <ThemedText style={{ color: '#4A3B25', fontSize: 11, marginRight: 5, fontWeight: 'bold' }}>Cost/g</ThemedText>
                    <TextInput
                      style={[styles.input, { width: 66 }]}
                      value={headerState.TaelCostPerGram}
                      onChangeText={(t) => updateHeader('TaelCostPerGram', t)}
                      keyboardType="numeric"
                    />
                  </View>
                </View>

                {/* Row 5: Remarks */}
                <View style={styles.row}>
                  <ThemedText style={styles.label}>Remarks</ThemedText>
                  <TextInput
                    style={[styles.input, { flex: 1, minWidth: 598 }]}
                    value={headerState.Remarks}
                    onChangeText={(t) => updateHeader('Remarks', t)}
                  />
                </View>
              </View>
            </View>

            {/* RIGHT: CATEGORY SUMMARY */}
            <View style={[styles.goldCard, { flex: 3.4, minWidth: 350 }]}>
              <View style={styles.cardHeaderBanner}>
                <View style={styles.headerIconBadge}>
                  <Ionicons name="pie-chart" size={15} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitle}>Category Summary</ThemedText>
              </View>

              <View style={{ padding: 0, flex: 1 }}>
                <View style={[styles.tableHeader, { paddingVertical: 6 }]}>
                  <ThemedText style={[styles.tableHeaderText, { flex: 1.5, textAlign: 'center' }]}>Category</ThemedText>
                  <ThemedText style={[styles.tableHeaderText, { flex: 1.2, textAlign: 'right' }]}>Bill Wt</ThemedText>
                  <ThemedText style={[styles.tableHeaderText, { flex: 1.5, textAlign: 'right' }]}>Inv Wt</ThemedText>
                  <ThemedText style={[styles.tableHeaderText, { flex: 1.2, textAlign: 'right' }]}>Balance</ThemedText>
                </View>
                <ScrollView style={{ minHeight: 120, maxHeight: 180 }}>
                  {categorySummaryList.length === 0 ? (
                    <View style={[styles.emptyTableState, { paddingVertical: 24 }]}>
                      <ThemedText style={{ color: '#8F7E66', fontSize: 12 }}>No category data</ThemedText>
                    </View>
                  ) : (
                    categorySummaryList.map((cat, idx) => {
                      const jsDetail = jobSheetDetails.find(j => (j.CategoryCode || '').trim() === cat.CategoryCode.trim());
                      return (
                        <TouchableOpacity
                          key={idx}
                          style={[
                            styles.tableRow,
                            { backgroundColor: idx % 2 === 1 ? '#FAF7F0' : '#FFFFFF', paddingVertical: 6 }
                          ]}
                          onPress={async () => {
                            if (jsDetail) {
                              setSelectedIndex(null);
                              await applySelectedCatalogue({
                                CatalogueCode: jsDetail.CatalogueCode || '',
                                CategoryCode: cat.CategoryCode,
                                CatalogueName: jsDetail.Description || ''
                              });
                              setDetailModalVisible(true);
                            } else {
                              setSelectedIndex(null);
                              await applySelectedCategory(cat.CategoryCode);
                              setDetailModalVisible(true);
                            }
                          }}
                        >
                          <ThemedText style={[styles.tableCellText, { flex: 1.5, textAlign: 'center', fontWeight: 'bold' }]}>{cat.CategoryCode}</ThemedText>
                          <ThemedText style={[styles.tableCellText, { flex: 1.2, textAlign: 'right' }]}>{cat.BillWeight.toFixed(2)}</ThemedText>
                          <ThemedText style={[styles.tableCellText, { flex: 1.5, textAlign: 'right' }]}>{cat.InventoryWeight.toFixed(2)}</ThemedText>
                          <ThemedText style={[styles.tableCellText, { flex: 1.2, textAlign: 'right', fontWeight: 'bold', color: cat.Balance !== 0 ? '#9E6D08' : '#332717' }]}>
                            {cat.Balance.toFixed(2)}
                          </ThemedText>
                        </TouchableOpacity>
                      );
                    })
                  )}
                </ScrollView>
              </View>
            </View>
          </View>
        </ScrollView>

        {/* MIDDLE SECTION: LINE DETAILS GRID */}
        <ScrollView horizontal style={{ marginBottom: 14 }} contentContainerStyle={{ flexGrow: 1 }}>
          <View style={{ width: '100%', minWidth: 1400 }}>
            <View style={styles.goldCard}>
              <View style={[styles.cardHeaderBanner, { justifyContent: 'space-between' }]}>
                <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                  <View style={styles.headerIconBadge}>
                    <Ionicons name="list" size={15} color="#B48218" />
                  </View>
                  <ThemedText style={styles.cardHeaderTitle}>Line Details</ThemedText>
                </View>

                <View style={{ flexDirection: 'row', alignItems: 'center', gap: 8 }}>
                  {selectedIndex !== null && (
                    <TouchableOpacity
                      style={[styles.addLineBtn, { backgroundColor: '#8E24AA' }]}
                      onPress={() => navigateToBarcode()}
                    >
                      <Ionicons name="barcode-outline" size={14} color="#FFF" style={{ marginRight: 4 }} />
                      <ThemedText style={styles.addLineBtnText}>BarCode</ThemedText>
                    </TouchableOpacity>
                  )}
                  {selectedIndex !== null && (
                    <TouchableOpacity
                      style={[styles.addLineBtn, { backgroundColor: '#C62828' }]}
                      onPress={deleteSelectedDetail}
                    >
                      <Ionicons name="trash-outline" size={14} color="#FFF" style={{ marginRight: 4 }} />
                      <ThemedText style={styles.addLineBtnText}>Delete</ThemedText>
                    </TouchableOpacity>
                  )}
                  <TouchableOpacity
                    style={styles.addLineBtn}
                    onPress={() => {
                      setSelectedIndex(null);
                      clearCurrentDetail();
                      setDetailModalVisible(true);
                    }}
                    disabled={isFormLocked}
                  >
                    <Ionicons name="add" size={14} color="#FFF" style={{ marginRight: 4 }} />
                    <ThemedText style={styles.addLineBtnText}>Add Line</ThemedText>
                  </TouchableOpacity>
                </View>
              </View>

              {/* Table Header */}
              <View style={styles.tableHeader}>
                <ThemedText style={[styles.tableHeaderText, { width: 130, textAlign: 'center' }]}>ItemCode</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { width: 70, textAlign: 'center' }]}>Class</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { width: 190, textAlign: 'center' }]}>Description</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { width: 110, textAlign: 'center' }]}>Weight</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { width: 110, textAlign: 'center' }]}>Net weight</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { width: 110, textAlign: 'center' }]}>Size</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { width: 80, textAlign: 'center' }]}>Gold Loss</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { width: 110, textAlign: 'center' }]}>Cost Per Gram</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { width: 90, textAlign: 'center' }]}>PMC</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { width: 150, textAlign: 'center' }]}>SMC</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { width: 80, textAlign: 'center' }]}>PCS</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { flex: 1, minWidth: 140, textAlign: 'center' }]}>TotalCost</ThemedText>
              </View>

              {/* Rows */}
              {details.length === 0 ? (
                <View style={styles.emptyTableState}>
                  <Ionicons name="cube-outline" size={38} color="#D8C7AA" style={{ marginBottom: 6 }} />
                  <ThemedText style={{ color: '#8F7E66', fontSize: 13, fontWeight: '500' }}>No records to display</ThemedText>
                </View>
              ) : (
                <ScrollView style={{ minHeight: 120, maxHeight: 220 }}>
                  {details.map((item, idx) => (
                    <TouchableOpacity
                      key={idx}
                      style={[
                        styles.tableRow,
                        { backgroundColor: selectedIndex === idx ? '#F9F1E2' : idx % 2 === 1 ? '#FAF7F0' : '#FFFFFF' }
                      ]}
                      onPress={() => {
                        setSelectedIndex(idx);
                        setCurrentDetail(item);
                        setDetailModalVisible(true);
                      }}
                    >
                      <ThemedText style={[styles.tableCellText, { width: 130, fontWeight: 'bold' }]}>{item.InventoryCode}</ThemedText>
                      <ThemedText style={[styles.tableCellText, { width: 70, textAlign: 'center' }]}>{item.ClassCode || item.CategoryCode}</ThemedText>
                      <ThemedText style={[styles.tableCellText, { width: 190 }]}>{item.Description || item.InventoryDescription}</ThemedText>
                      <ThemedText style={[styles.tableCellText, { width: 110, textAlign: 'right' }]}>{parseFloat(item.Weight || 0).toFixed(2)}</ThemedText>
                      <ThemedText style={[styles.tableCellText, { width: 110, textAlign: 'right' }]}>
                        {((parseFloat(item.Weight) || 0) - (parseFloat(item.StoneWeight) || 0)).toFixed(2)}
                      </ThemedText>
                      <ThemedText style={[styles.tableCellText, { width: 110, textAlign: 'right' }]}>{item.Size}</ThemedText>
                      <ThemedText style={[styles.tableCellText, { width: 80, textAlign: 'right' }]}>{parseFloat(item.GoldLoss || 0).toFixed(2)}</ThemedText>
                      <ThemedText style={[styles.tableCellText, { width: 110, textAlign: 'right' }]}>{parseFloat(item.CostPerGram || 0).toFixed(2)}</ThemedText>
                      <ThemedText style={[styles.tableCellText, { width: 90, textAlign: 'right' }]}>{parseFloat(item.PMCCost || item.PMC || 0).toFixed(2)}</ThemedText>
                      <ThemedText style={[styles.tableCellText, { width: 150, textAlign: 'right' }]}>{parseFloat(item.SMCCost || item.SMC || 0).toFixed(2)}</ThemedText>
                      <ThemedText style={[styles.tableCellText, { width: 80, textAlign: 'right' }]}>{item.PCS || 1}</ThemedText>
                      <ThemedText style={[styles.tableCellText, { flex: 1, minWidth: 140, textAlign: 'right', fontWeight: 'bold' }]}>
                        {parseFloat(item.TotalCost || 0).toFixed(2)}
                      </ThemedText>
                    </TouchableOpacity>
                  ))}
                </ScrollView>
              )}

              {/* TOTALS ROW (from purchase.txt subTotal) */}
              <View style={styles.tableTotalRow}>
                <View style={{ width: 390 }}>
                  <ThemedText style={[styles.tableTotalCellText, { textAlign: 'right', paddingRight: 10 }]}>TOTAL :</ThemedText>
                </View>
                <ThemedText style={[styles.tableTotalCellText, { width: 110, textAlign: 'right' }]}>
                  {details.reduce((sum, i) => sum + (parseFloat(i.Weight) || 0), 0).toFixed(2)}
                </ThemedText>
                <ThemedText style={[styles.tableTotalCellText, { width: 110, textAlign: 'right' }]}>
                  {details.reduce((sum, i) => sum + ((parseFloat(i.Weight) || 0) - (parseFloat(i.StoneWeight) || 0)), 0).toFixed(2)}
                </ThemedText>
                <View style={{ width: 300 }} />
                <ThemedText style={[styles.tableTotalCellText, { width: 90, textAlign: 'right' }]}>
                  {details.reduce((sum, i) => sum + (parseFloat(i.PMCCost || i.PMC) || 0), 0).toFixed(2)}
                </ThemedText>
                <ThemedText style={[styles.tableTotalCellText, { width: 150, textAlign: 'right' }]}>
                  {details.reduce((sum, i) => sum + (parseFloat(i.SMCCost || i.SMC) || 0), 0).toFixed(2)}
                </ThemedText>
                <ThemedText style={[styles.tableTotalCellText, { width: 80, textAlign: 'right' }]}>
                  {details.reduce((sum, i) => sum + (parseFloat(i.PCS) || 1), 0)}
                </ThemedText>
                <ThemedText style={[styles.tableTotalCellText, { flex: 1, minWidth: 140, textAlign: 'right' }]}>
                  {details.reduce((sum, i) => sum + (parseFloat(i.TotalCost) || 0), 0).toFixed(2)}
                </ThemedText>
              </View>
            </View>
          </View>
        </ScrollView>
      </ScrollView>

      {/* ================= DETAIL MODAL ================= */}
      <Modal visible={detailModalVisible} transparent={true} animationType="fade">
        <View style={styles.modalOverlay}>
          {/* Modal-level Toast Notification */}
          {toast.visible && (
            <View
              style={[
                styles.toastContainer,
                { backgroundColor: toast.type === 'success' ? '#2E7D32' : toast.type === 'error' ? '#C62828' : '#1565C0', zIndex: 10001 }
              ]}
            >
              <Ionicons
                name={toast.type === 'success' ? 'checkmark-circle' : toast.type === 'error' ? 'alert-circle' : 'information-circle'}
                size={18}
                color="#fff"
                style={{ marginRight: 8 }}
              />
              <ThemedText style={{ color: '#FFF', fontWeight: 'bold', fontSize: 13, textAlign: 'center' }}>
                {toast.message}
              </ThemedText>
            </View>
          )}

          <View style={[styles.modalContainer, { backgroundColor: cardColor, borderColor: borderColor, width: '92%', maxWidth: 1350 }]}>
            <View style={{ flexDirection: 'row', justifyContent: 'space-between', alignItems: 'center', marginBottom: 12 }}>
              <ThemedText style={[styles.modalTitle, { color: textColor, marginBottom: 0 }]}>
                {selectedIndex !== null ? 'Edit Line Detail' : 'Add Line Detail'}
              </ThemedText>
              <TouchableOpacity onPress={() => setDetailModalVisible(false)}>
                <Ionicons name="close" size={24} color={textColor} />
              </TouchableOpacity>
            </View>

            <View style={[styles.section, { backgroundColor: backgroundColor, padding: 8, borderColor: borderColor }]}>
              <ScrollView horizontal={true} showsHorizontalScrollIndicator={true} contentContainerStyle={{ flexGrow: 1 }}>
                <View style={{ width: '100%', minWidth: 1300, borderWidth: 1, borderColor: '#aaa', backgroundColor: backgroundColor, padding: 2, marginBottom: 8 }}>
                  {/* ROW 1: Headers */}
                  <View style={{ flexDirection: 'row' }}>
                    <View style={{ width: 130, backgroundColor: cardColor, borderWidth: 1, borderColor: '#aaa', justifyContent: 'center' }}>
                      <ThemedText style={{ color: textColor, fontSize: 11, fontWeight: 'bold', textAlign: 'center' }}>Catalogue Code</ThemedText>
                    </View>
                    <View style={{ width: 180, backgroundColor: cardColor, borderWidth: 1, borderColor: '#aaa', borderLeftWidth: 0, justifyContent: 'center' }}>
                      <ThemedText style={{ color: textColor, fontSize: 11, fontWeight: 'bold', textAlign: 'center' }}>Category Code</ThemedText>
                    </View>
                    <View style={{ width: 120, backgroundColor: cardColor, borderWidth: 1, borderColor: '#aaa', borderLeftWidth: 0, justifyContent: 'center' }}>
                      <ThemedText style={{ color: textColor, fontSize: 11, fontWeight: 'bold', textAlign: 'center' }}>Tray Id</ThemedText>
                    </View>
                    <View style={{ width: 120, backgroundColor: cardColor, borderWidth: 1, borderColor: '#aaa', borderLeftWidth: 0, justifyContent: 'center' }}>
                      <ThemedText style={{ color: textColor, fontSize: 11, fontWeight: 'bold', textAlign: 'center' }}>Bill Weight</ThemedText>
                    </View>
                    <View style={{ width: 80, backgroundColor: cardColor, borderWidth: 1, borderColor: '#aaa', borderLeftWidth: 0, justifyContent: 'center' }}>
                      <ThemedText style={{ color: textColor, fontSize: 11, fontWeight: 'bold', textAlign: 'center' }}>Bill PCS</ThemedText>
                    </View>
                    <View style={{ width: 100, backgroundColor: cardColor, borderWidth: 1, borderColor: '#aaa', borderLeftWidth: 0, justifyContent: 'center' }}>
                      <ThemedText style={{ color: textColor, fontSize: 11, fontWeight: 'bold', textAlign: 'center' }}>Inv. Weight</ThemedText>
                    </View>
                    <View style={{ width: 90, backgroundColor: cardColor, borderWidth: 1, borderColor: '#aaa', borderLeftWidth: 0, justifyContent: 'center' }}>
                      <ThemedText style={{ color: textColor, fontSize: 11, fontWeight: 'bold', textAlign: 'center' }}>Inv. PCS</ThemedText>
                    </View>
                    <View style={{ width: 170, backgroundColor: cardColor, borderWidth: 1, borderColor: '#aaa', borderLeftWidth: 0, paddingLeft: 4, justifyContent: 'center' }}>
                      <ThemedText style={{ color: textColor, fontSize: 11, fontWeight: 'bold' }}>Purchase MC</ThemedText>
                    </View>
                    <View style={{ width: 170, backgroundColor: cardColor, borderWidth: 1, borderColor: '#aaa', borderLeftWidth: 0, paddingLeft: 4, justifyContent: 'center' }}>
                      <ThemedText style={{ color: textColor, fontSize: 11, fontWeight: 'bold' }}>Sales MC</ThemedText>
                    </View>
                    <View style={{ flex: 1, minWidth: 140, backgroundColor: cardColor, borderWidth: 1, borderColor: '#aaa', borderLeftWidth: 0, flexDirection: 'row', alignItems: 'center', justifyContent: 'space-around' }}>
                      <Checkbox label="AUTO" selected={autoSMC} onPress={() => setAutoSMC(!autoSMC)} />
                      <Checkbox label="PCS" selected={salesPCS} onPress={() => setSalesPCS(!salesPCS)} />
                    </View>
                  </View>

                  {/* ROW 2: Inputs */}
                  <View style={{ flexDirection: 'row' }}>
                    <View style={{ width: 130, borderWidth: 1, borderColor: '#aaa', borderTopWidth: 0, flexDirection: 'row' }}>
                      <TextInput
                        style={{ width: 104, padding: 2, fontSize: 11, color: textColor }}
                        value={currentDetail.CatalogueCode}
                        onChangeText={(t) => updateDetail('CatalogueCode', t)}
                        onBlur={() => {
                          if (currentDetail.CatalogueCode) {
                            applySelectedCatalogue({ CatalogueCode: currentDetail.CatalogueCode });
                          }
                        }}
                      />
                      <TouchableOpacity
                        style={{ width: 24, backgroundColor: cardColor, borderLeftWidth: 1, borderColor: '#aaa', justifyContent: 'center', alignItems: 'center', flexShrink: 0 }}
                        onPress={fetchCatalogue}
                      >
                        <ThemedText style={{ color: textColor, fontWeight: 'bold', fontSize: 10 }}>...</ThemedText>
                      </TouchableOpacity>
                    </View>

                    <View style={{ width: 180, borderWidth: 1, borderColor: '#aaa', borderTopWidth: 0, borderLeftWidth: 0, flexDirection: 'row' }}>
                      <TextInput
                        style={{ flex: 1, padding: 2, fontSize: 11, color: textColor }}
                        value={currentDetail.CategoryCode}
                        onChangeText={(t) => updateDetail('CategoryCode', t)}
                        onBlur={() => {
                          if (currentDetail.CategoryCode) {
                            applySelectedCategory(currentDetail.CategoryCode);
                          }
                        }}
                      />
                      <TouchableOpacity
                        style={{ width: 24, backgroundColor: cardColor, borderLeftWidth: 1, borderColor: '#aaa', justifyContent: 'center', alignItems: 'center', flexShrink: 0 }}
                        onPress={fetchCategories}
                      >
                        <ThemedText style={{ color: textColor, fontWeight: 'bold', fontSize: 10 }}>...</ThemedText>
                      </TouchableOpacity>
                    </View>

                    <View style={{ width: 120, borderWidth: 1, borderColor: '#aaa', borderTopWidth: 0, borderLeftWidth: 0, flexDirection: 'row' }}>
                      <TextInput
                        style={{ width: 94, padding: 2, fontSize: 11, color: textColor }}
                        value={currentDetail.TrayId}
                        onChangeText={(t) => updateDetail('TrayId', t)}
                      />
                      <TouchableOpacity
                        style={{ width: 24, backgroundColor: cardColor, borderLeftWidth: 1, borderColor: '#aaa', justifyContent: 'center', alignItems: 'center', flexShrink: 0 }}
                        onPress={fetchTrays}
                      >
                        <ThemedText style={{ color: textColor, fontWeight: 'bold', fontSize: 10 }}>...</ThemedText>
                      </TouchableOpacity>
                    </View>

                    <View style={{ width: 120, borderWidth: 1, borderColor: '#aaa', borderTopWidth: 0, borderLeftWidth: 0 }}>
                      <TextInput
                        style={{ flex: 1, padding: 2, fontSize: 11, color: textColor, textAlign: 'right', backgroundColor: '#F8F5EE' }}
                        value={currentDetail.BillWeight}
                        editable={false}
                      />
                    </View>

                    <View style={{ width: 80, borderWidth: 1, borderColor: '#aaa', borderTopWidth: 0, borderLeftWidth: 0 }}>
                      <TextInput
                        style={{ flex: 1, padding: 2, fontSize: 11, color: textColor, textAlign: 'right', backgroundColor: '#F8F5EE' }}
                        value={currentDetail.BillPCS}
                        editable={false}
                      />
                    </View>

                    <View style={{ width: 100, borderWidth: 1, borderColor: '#aaa', borderTopWidth: 0, borderLeftWidth: 0 }}>
                      <TextInput
                        style={{ flex: 1, padding: 2, fontSize: 11, color: textColor, textAlign: 'right', backgroundColor: '#F8F5EE' }}
                        value={currentDetail.InvWeight}
                        editable={false}
                      />
                    </View>

                    <View style={{ width: 90, borderWidth: 1, borderColor: '#aaa', borderTopWidth: 0, borderLeftWidth: 0 }}>
                      <TextInput
                        style={{ flex: 1, padding: 2, fontSize: 11, color: textColor, textAlign: 'right', backgroundColor: '#F8F5EE' }}
                        value={currentDetail.InvPCS}
                        editable={false}
                      />
                    </View>

                    <View style={{ width: 170, borderWidth: 1, borderColor: '#aaa', borderTopWidth: 0, borderLeftWidth: 0, flexDirection: 'row', alignItems: 'center', justifyContent: 'space-around', paddingVertical: 2 }}>
                      <TextInput
                        style={{ width: 44, borderWidth: 1, borderColor: borderColor, padding: 1, fontSize: 10, textAlign: 'right' }}
                        value={currentDetail.PMC}
                        onChangeText={(t) => updateDetail('PMC', t)}
                        keyboardType="numeric"
                      />
                      <Checkbox
                        label="Gram"
                        selected={currentDetail.PurchaseMCType === 'PerGram'}
                        onPress={() => updateDetail('PurchaseMCType', 'PerGram')}
                      />
                      <Checkbox
                        label="PCS"
                        selected={currentDetail.PurchaseMCType === 'PerPCS'}
                        onPress={() => updateDetail('PurchaseMCType', 'PerPCS')}
                      />
                    </View>

                    <View style={{ width: 170, borderWidth: 1, borderColor: '#aaa', borderTopWidth: 0, borderLeftWidth: 0, flexDirection: 'row', alignItems: 'center', justifyContent: 'space-around', paddingVertical: 2 }}>
                      <TextInput
                        style={{ width: 44, borderWidth: 1, borderColor: borderColor, padding: 1, fontSize: 10, textAlign: 'right' }}
                        value={currentDetail.SMC}
                        onChangeText={(t) => updateDetail('SMC', t)}
                        keyboardType="numeric"
                        editable={!autoSMC}
                      />
                      <Checkbox
                        label="Gram"
                        selected={currentDetail.SalesMCType === 'PerGram'}
                        onPress={() => updateDetail('SalesMCType', 'PerGram')}
                      />
                      <Checkbox
                        label="PCS"
                        selected={currentDetail.SalesMCType === 'PerPCS'}
                        onPress={() => updateDetail('SalesMCType', 'PerPCS')}
                      />
                    </View>

                    <View style={{ flex: 1, minWidth: 140, borderWidth: 1, borderColor: '#aaa', borderTopWidth: 0, borderLeftWidth: 0, backgroundColor: backgroundColor }} />
                  </View>

                  {/* ROW 3: Headers */}
                  <View style={{ flexDirection: 'row' }}>
                    <View style={{ width: 130, backgroundColor: cardColor, borderWidth: 1, borderColor: '#aaa', borderTopWidth: 0, justifyContent: 'center' }}>
                      <ThemedText style={{ color: textColor, fontSize: 11, fontWeight: 'bold', textAlign: 'center' }}>Inventory Code</ThemedText>
                    </View>
                    <View style={{ width: 180, backgroundColor: cardColor, borderWidth: 1, borderColor: '#aaa', borderTopWidth: 0, borderLeftWidth: 0, justifyContent: 'center' }}>
                      <ThemedText style={{ color: textColor, fontSize: 11, fontWeight: 'bold', textAlign: 'center' }}>Inventory Description</ThemedText>
                    </View>
                    <View style={{ width: 120, backgroundColor: cardColor, borderWidth: 1, borderColor: '#aaa', borderTopWidth: 0, borderLeftWidth: 0, justifyContent: 'center' }}>
                      <ThemedText style={{ color: textColor, fontSize: 11, fontWeight: 'bold', textAlign: 'center' }}>Weight</ThemedText>
                    </View>
                    <View style={{ width: 120, backgroundColor: cardColor, borderWidth: 1, borderColor: '#aaa', borderTopWidth: 0, borderLeftWidth: 0, justifyContent: 'center' }}>
                      <ThemedText style={{ color: textColor, fontSize: 11, fontWeight: 'bold', textAlign: 'center' }}>Size</ThemedText>
                    </View>
                    <View style={{ width: 80, backgroundColor: cardColor, borderWidth: 1, borderColor: '#aaa', borderTopWidth: 0, borderLeftWidth: 0, justifyContent: 'center' }}>
                      <ThemedText style={{ color: textColor, fontSize: 11, fontWeight: 'bold', textAlign: 'center' }}>Gold Loss</ThemedText>
                    </View>
                    <View style={{ width: 100, backgroundColor: cardColor, borderWidth: 1, borderColor: '#aaa', borderTopWidth: 0, borderLeftWidth: 0, justifyContent: 'center' }}>
                      <ThemedText style={{ color: textColor, fontSize: 11, fontWeight: 'bold', textAlign: 'center' }}>Cost Per Gram</ThemedText>
                    </View>
                    <View style={{ width: 90, backgroundColor: cardColor, borderWidth: 1, borderColor: '#aaa', borderTopWidth: 0, borderLeftWidth: 0, justifyContent: 'center' }}>
                      <ThemedText style={{ color: textColor, fontSize: 11, fontWeight: 'bold', textAlign: 'center' }}>PMC</ThemedText>
                    </View>
                    <View style={{ width: 170, backgroundColor: cardColor, borderWidth: 1, borderColor: '#aaa', borderTopWidth: 0, borderLeftWidth: 0, justifyContent: 'center' }}>
                      <ThemedText style={{ color: textColor, fontSize: 11, fontWeight: 'bold', textAlign: 'center' }}>SMC</ThemedText>
                    </View>
                    <View style={{ width: 170, backgroundColor: cardColor, borderWidth: 1, borderColor: '#aaa', borderTopWidth: 0, borderLeftWidth: 0, justifyContent: 'center' }}>
                      <ThemedText style={{ color: textColor, fontSize: 11, fontWeight: 'bold', textAlign: 'center' }}>PCS</ThemedText>
                    </View>
                    <View style={{ flex: 1, minWidth: 140, backgroundColor: cardColor, borderWidth: 1, borderColor: '#aaa', borderTopWidth: 0, borderLeftWidth: 0, justifyContent: 'center' }}>
                      <ThemedText style={{ color: textColor, fontSize: 11, fontWeight: 'bold', textAlign: 'center' }}>Total Cost</ThemedText>
                    </View>
                  </View>

                  {/* ROW 4: Inputs */}
                  <View style={{ flexDirection: 'row' }}>
                    <View style={{ width: 130, borderWidth: 1, borderColor: '#aaa', borderTopWidth: 0 }}>
                      <TextInput
                        style={{ flex: 1, padding: 2, fontSize: 11, color: textColor }}
                        value={currentDetail.InventoryCode}
                        onChangeText={(t) => updateDetail('InventoryCode', t)}
                      />
                    </View>
                    <View style={{ width: 180, borderWidth: 1, borderColor: '#aaa', borderTopWidth: 0, borderLeftWidth: 0 }}>
                      <TextInput
                        style={{ flex: 1, padding: 2, fontSize: 11, color: textColor }}
                        value={currentDetail.InventoryDescription}
                        onChangeText={(t) => updateDetail('InventoryDescription', t)}
                      />
                    </View>
                    <View style={{ width: 120, borderWidth: 1, borderColor: '#aaa', borderTopWidth: 0, borderLeftWidth: 0 }}>
                      <TextInput
                        style={{ flex: 1, padding: 2, fontSize: 11, color: textColor, textAlign: 'right' }}
                        value={currentDetail.Weight}
                        onChangeText={(t) => updateDetail('Weight', t)}
                        keyboardType="numeric"
                      />
                    </View>
                    <View style={{ width: 120, borderWidth: 1, borderColor: '#aaa', borderTopWidth: 0, borderLeftWidth: 0, flexDirection: 'row' }}>
                      <TextInput
                        style={{ width: 94, padding: 2, fontSize: 11, color: textColor, textAlign: 'right' }}
                        value={currentDetail.Size}
                        onChangeText={(t) => updateDetail('Size', t)}
                        keyboardType="numeric"
                      />
                      <TouchableOpacity
                        style={{ width: 24, backgroundColor: cardColor, borderLeftWidth: 1, borderColor: '#aaa', justifyContent: 'center', alignItems: 'center', flexShrink: 0 }}
                        onPress={fetchSizes}
                      >
                        <ThemedText style={{ color: textColor, fontWeight: 'bold', fontSize: 10 }}>...</ThemedText>
                      </TouchableOpacity>
                    </View>
                    <View style={{ width: 80, borderWidth: 1, borderColor: '#aaa', borderTopWidth: 0, borderLeftWidth: 0 }}>
                      <TextInput
                        style={{ flex: 1, padding: 2, fontSize: 11, color: textColor, textAlign: 'right' }}
                        value={currentDetail.GoldLoss}
                        onChangeText={(t) => updateDetail('GoldLoss', t)}
                        keyboardType="numeric"
                      />
                    </View>
                    <View style={{ width: 100, borderWidth: 1, borderColor: '#aaa', borderTopWidth: 0, borderLeftWidth: 0 }}>
                      <TextInput
                        style={{ flex: 1, padding: 2, fontSize: 11, color: textColor, textAlign: 'right' }}
                        value={currentDetail.CostPerGram}
                        onChangeText={(t) => updateDetail('CostPerGram', t)}
                        keyboardType="numeric"
                      />
                    </View>
                    <View style={{ width: 90, borderWidth: 1, borderColor: '#aaa', borderTopWidth: 0, borderLeftWidth: 0, backgroundColor: '#F8F5EE' }}>
                      <TextInput
                        style={{ flex: 1, padding: 2, fontSize: 11, color: textColor, textAlign: 'right' }}
                        value={currentDetail.PMCCost || currentDetail.PMC}
                        editable={false}
                      />
                    </View>
                    <View style={{ width: 170, borderWidth: 1, borderColor: '#aaa', borderTopWidth: 0, borderLeftWidth: 0, backgroundColor: '#F8F5EE' }}>
                      <TextInput
                        style={{ flex: 1, padding: 2, fontSize: 11, color: textColor, textAlign: 'right' }}
                        value={currentDetail.SMCCost || currentDetail.SMC}
                        editable={false}
                      />
                    </View>
                    <View style={{ width: 170, borderWidth: 1, borderColor: '#aaa', borderTopWidth: 0, borderLeftWidth: 0 }}>
                      <TextInput
                        style={{ flex: 1, padding: 2, fontSize: 11, color: textColor, textAlign: 'right' }}
                        value={currentDetail.PCS}
                        onChangeText={(t) => updateDetail('PCS', t)}
                        keyboardType="numeric"
                      />
                    </View>
                    <View style={{ flex: 1, minWidth: 140, borderWidth: 1, borderColor: '#aaa', borderTopWidth: 0, borderLeftWidth: 0, backgroundColor: '#F8F5EE' }}>
                      <TextInput
                        style={{ flex: 1, padding: 2, fontSize: 11, color: textColor, textAlign: 'right', fontWeight: 'bold' }}
                        value={currentDetail.TotalCost}
                        editable={false}
                      />
                    </View>
                  </View>

                  {/* ROW 5: Headers (Diamond Description & Stone Weight) */}
                  <View style={{ flexDirection: 'row' }}>
                    <View style={{ width: 310, backgroundColor: cardColor, borderWidth: 1, borderColor: '#aaa', borderTopWidth: 0, justifyContent: 'center' }}>
                      <ThemedText style={{ color: textColor, fontSize: 11, fontWeight: 'bold', textAlign: 'center' }}>Diamond Description</ThemedText>
                    </View>
                    <View style={{ width: 120, backgroundColor: cardColor, borderWidth: 1, borderColor: '#aaa', borderTopWidth: 0, borderLeftWidth: 0, justifyContent: 'center' }}>
                      <ThemedText style={{ color: textColor, fontSize: 11, fontWeight: 'bold', textAlign: 'center' }}>Stone Weight</ThemedText>
                    </View>
                    <View style={{ flex: 1, minWidth: 870, backgroundColor: cardColor, borderWidth: 1, borderColor: '#aaa', borderTopWidth: 0, borderLeftWidth: 0 }} />
                  </View>

                  {/* ROW 6: Inputs */}
                  <View style={{ flexDirection: 'row', marginBottom: 8 }}>
                    <View style={{ width: 310, borderWidth: 1, borderColor: '#aaa', borderTopWidth: 0 }}>
                      <TextInput
                        style={{ flex: 1, padding: 2, fontSize: 11, color: textColor }}
                        value={currentDetail.DiamondDescription}
                        onChangeText={(t) => updateDetail('DiamondDescription', t)}
                      />
                    </View>
                    <View style={{ width: 120, borderWidth: 1, borderColor: '#aaa', borderTopWidth: 0, borderLeftWidth: 0 }}>
                      <TextInput
                        style={{ flex: 1, padding: 2, fontSize: 11, color: textColor, textAlign: 'right' }}
                        value={currentDetail.StoneWeight}
                        onChangeText={(t) => updateDetail('StoneWeight', t)}
                        keyboardType="numeric"
                      />
                    </View>
                    <View style={{ flex: 1, minWidth: 870, borderWidth: 1, borderColor: '#aaa', borderTopWidth: 0, borderLeftWidth: 0, backgroundColor: backgroundColor }} />
                  </View>
                </View>
              </ScrollView>

              {/* IPM Section (from purchase.txt gbIPM) */}
              {currentDetail.IPM && (
                <View style={styles.ipmBox}>
                  <ThemedText style={{ fontSize: 11, fontWeight: 'bold', color: '#B48218', marginBottom: 6 }}>
                    IPM Details (Investment Precious Metals)
                  </ThemedText>
                  <View style={{ flexDirection: 'row', flexWrap: 'wrap', gap: 8, alignItems: 'center' }}>
                    <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                      <ThemedText style={styles.ipmLabel}>Type:</ThemedText>
                      <TextInput
                        style={[styles.input, { width: 70 }]}
                        value={currentDetail.IPMType}
                        onChangeText={(t) => updateDetail('IPMType', t)}
                        placeholder="Coin/Bar"
                      />
                    </View>

                    <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                      <ThemedText style={styles.ipmLabel}>Bar No:</ThemedText>
                      <TextInput
                        style={[styles.input, { width: 90 }]}
                        value={currentDetail.IPMBarNo}
                        onChangeText={(t) => updateDetail('IPMBarNo', t)}
                      />
                    </View>

                    <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                      <ThemedText style={styles.ipmLabel}>Purity:</ThemedText>
                      <TextInput
                        style={[styles.input, { width: 70 }]}
                        value={currentDetail.IPMPurity}
                        onChangeText={(t) => updateDetail('IPMPurity', t)}
                        placeholder="999.9"
                      />
                    </View>

                    <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                      <ThemedText style={styles.ipmLabel}>GST Type:</ThemedText>
                      <TextInput
                        style={[styles.input, { width: 70 }]}
                        value={currentDetail.IPMGSTType}
                        onChangeText={(t) => updateDetail('IPMGSTType', t)}
                      />
                    </View>

                    <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                      <ThemedText style={styles.ipmLabel}>Refiner:</ThemedText>
                      <View style={{ flexDirection: 'row' }}>
                        <TextInput
                          style={[styles.input, { width: 80, borderTopRightRadius: 0, borderBottomRightRadius: 0 }]}
                          value={currentDetail.IPMRefiner}
                          onChangeText={(t) => updateDetail('IPMRefiner', t)}
                        />
                        <TouchableOpacity style={[styles.lookupBtn, { width: 22, height: 28 }]} onPress={fetchRefiners}>
                          <ThemedText style={{ fontSize: 10 }}>...</ThemedText>
                        </TouchableOpacity>
                      </View>
                    </View>

                    <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                      <ThemedText style={styles.ipmLabel}>Country:</ThemedText>
                      <View style={{ flexDirection: 'row' }}>
                        <TextInput
                          style={[styles.input, { width: 80, borderTopRightRadius: 0, borderBottomRightRadius: 0 }]}
                          value={currentDetail.IPMCountry}
                          onChangeText={(t) => updateDetail('IPMCountry', t)}
                        />
                        <TouchableOpacity style={[styles.lookupBtn, { width: 22, height: 28 }]} onPress={fetchCountries}>
                          <ThemedText style={{ fontSize: 10 }}>...</ThemedText>
                        </TouchableOpacity>
                      </View>
                    </View>
                  </View>
                </View>
              )}

              {/* Bottom Options & Actions */}
              <View style={{ flexDirection: 'row', justifyContent: 'space-between', alignItems: 'center', marginTop: 8 }}>
                <View style={{ flexDirection: 'row', alignItems: 'center', gap: 12 }}>
                  <Checkbox
                    label="IPM"
                    selected={currentDetail.IPM}
                    onPress={() => updateDetail('IPM', !currentDetail.IPM)}
                  />
                  <Checkbox
                    label="Addl. Desc"
                    selected={currentDetail.AddlDesc}
                    onPress={() => {
                      const nextVal = !currentDetail.AddlDesc;
                      updateDetail('AddlDesc', nextVal);
                      if (nextVal) setAddlDescModalVisible(true);
                    }}
                  />
                  {currentDetail.AddlDesc && (
                    <TouchableOpacity
                      style={[styles.actionBtn, { backgroundColor: '#FDF7EB', borderColor: '#B8903B' }]}
                      onPress={() => setAddlDescModalVisible(true)}
                    >
                      <ThemedText style={{ color: '#9E6D08', fontSize: 11, fontWeight: 'bold' }}>Edit Addl Desc</ThemedText>
                    </TouchableOpacity>
                  )}
                  <Checkbox
                    label="Image"
                    selected={imageModalVisible}
                    onPress={() => setImageModalVisible(!imageModalVisible)}
                  />
                </View>

                <View style={{ flexDirection: 'row', gap: 8 }}>
                  <TouchableOpacity
                    style={[styles.actionBtn, { backgroundColor: '#F9F2E2', borderColor: '#cc9900' }]}
                    onPress={addDetail}
                  >
                    <ThemedText style={{ color: '#9E6D08', fontWeight: 'bold', textAlign: 'center' }}>
                      {selectedIndex !== null ? 'Update' : 'Add'}
                    </ThemedText>
                  </TouchableOpacity>

                  <TouchableOpacity
                    style={[styles.actionBtn, { backgroundColor: cardColor, borderColor: '#aaa' }]}
                    onPress={() => {
                      setSelectedIndex(null);
                      clearCurrentDetail();
                    }}
                  >
                    <ThemedText style={{ color: textColor, fontWeight: 'bold', textAlign: 'center' }}>Clear</ThemedText>
                  </TouchableOpacity>

                  <TouchableOpacity
                    style={[styles.actionBtn, { backgroundColor: '#FFEBEE', borderColor: '#E57373' }]}
                    onPress={deleteSelectedDetail}
                  >
                    <ThemedText style={{ color: '#C62828', fontWeight: 'bold', textAlign: 'center' }}>Delete</ThemedText>
                  </TouchableOpacity>

                  <TouchableOpacity
                    style={[styles.actionBtn, { backgroundColor: '#EDE7F6', borderColor: '#9575CD' }]}
                    onPress={() => navigateToBarcode()}
                  >
                    <ThemedText style={{ color: '#512DA8', fontWeight: 'bold', textAlign: 'center' }}>BarCode</ThemedText>
                  </TouchableOpacity>
                </View>
              </View>
            </View>
          </View>
        </View>
      </Modal>

      {/* ================= ADDITIONAL DESCRIPTION MODAL (grpAddlDescription in purchase.txt) ================= */}
      <Modal visible={addlDescModalVisible} transparent={true} animationType="fade">
        <View style={styles.modalOverlay}>
          <View style={[styles.modalContainer, { backgroundColor: cardColor, borderColor: borderColor, width: '70%', maxWidth: 650 }]}>
            <View style={{ flexDirection: 'row', justifyContent: 'space-between', alignItems: 'center', marginBottom: 12 }}>
              <ThemedText style={[styles.modalTitle, { color: textColor }]}>Additional Description & Cost</ThemedText>
              <TouchableOpacity onPress={() => setAddlDescModalVisible(false)}>
                <Ionicons name="close" size={24} color={textColor} />
              </TouchableOpacity>
            </View>

            <View style={{ marginBottom: 12 }}>
              <View style={{ flexDirection: 'row', borderBottomWidth: 1, borderColor: borderColor, paddingBottom: 4, marginBottom: 8 }}>
                <ThemedText style={{ flex: 2, fontWeight: 'bold', color: textColor, fontSize: 12 }}>Description</ThemedText>
                <ThemedText style={{ flex: 1, fontWeight: 'bold', color: textColor, fontSize: 12, textAlign: 'right', marginRight: 8 }}>Cost</ThemedText>
                <ThemedText style={{ flex: 1, fontWeight: 'bold', color: textColor, fontSize: 12, textAlign: 'right' }}>Selling Price</ThemedText>
              </View>

              {[1, 2, 3, 4].map((num) => (
                <View key={num} style={{ flexDirection: 'row', alignItems: 'center', marginBottom: 6 }}>
                  <TextInput
                    style={[styles.input, { flex: 2, marginRight: 8 }]}
                    placeholder={`Description ${num}`}
                    value={currentDetail[`Desc${num}`]}
                    onChangeText={(t) => updateDetail(`Desc${num}`, t)}
                  />
                  <TextInput
                    style={[styles.input, { flex: 1, marginRight: 8, textAlign: 'right' }]}
                    placeholder="0.00"
                    keyboardType="numeric"
                    value={currentDetail[`Cost${num}`]}
                    onChangeText={(t) => updateDetail(`Cost${num}`, t)}
                  />
                  <TextInput
                    style={[styles.input, { flex: 1, textAlign: 'right' }]}
                    placeholder="0.00"
                    keyboardType="numeric"
                    value={currentDetail[`SellingPrice${num}`]}
                    onChangeText={(t) => updateDetail(`SellingPrice${num}`, t)}
                  />
                </View>
              ))}
            </View>

            <View style={{ flexDirection: 'row', justifyContent: 'flex-end', gap: 8 }}>
              <TouchableOpacity
                style={[styles.actionBtn, { backgroundColor: '#F9F2E2', borderColor: '#cc9900' }]}
                onPress={() => setAddlDescModalVisible(false)}
              >
                <ThemedText style={{ color: '#9E6D08', fontWeight: 'bold' }}>OK</ThemedText>
              </TouchableOpacity>
            </View>
          </View>
        </View>
      </Modal>

      {/* ================= IMAGE PREVIEW MODAL (grpImage in purchase.txt) ================= */}
      <Modal visible={imageModalVisible} transparent={true} animationType="fade">
        <View style={styles.modalOverlay}>
          <View style={[styles.modalContainer, { backgroundColor: cardColor, borderColor: borderColor, width: '50%', maxWidth: 400 }]}>
            <View style={{ flexDirection: 'row', justifyContent: 'space-between', alignItems: 'center', marginBottom: 12 }}>
              <ThemedText style={[styles.modalTitle, { color: textColor }]}>Item Image</ThemedText>
              <TouchableOpacity onPress={() => setImageModalVisible(false)}>
                <Ionicons name="close" size={24} color={textColor} />
              </TouchableOpacity>
            </View>

            <View style={{ height: 220, borderWidth: 1, borderColor: borderColor, borderRadius: 6, justifyContent: 'center', alignItems: 'center', backgroundColor: '#FAF8F5', marginBottom: 12 }}>
              <Ionicons name="image-outline" size={60} color="#C4B59D" />
              <ThemedText style={{ color: '#8F7E66', fontSize: 12, marginTop: 8 }}>No image selected</ThemedText>
            </View>

            <View style={{ flexDirection: 'row', justifyContent: 'flex-end', gap: 8 }}>
              <TouchableOpacity
                style={[styles.actionBtn, { backgroundColor: cardColor, borderColor: '#aaa' }]}
                onPress={() => showToast("Browse image feature ready", "info")}
              >
                <ThemedText style={{ color: textColor, fontWeight: 'bold' }}>Browse</ThemedText>
              </TouchableOpacity>
              <TouchableOpacity
                style={[styles.actionBtn, { backgroundColor: '#F9F2E2', borderColor: '#cc9900' }]}
                onPress={() => setImageModalVisible(false)}
              >
                <ThemedText style={{ color: '#9E6D08', fontWeight: 'bold' }}>OK</ThemedText>
              </TouchableOpacity>
            </View>
          </View>
        </View>
      </Modal>

      {/* ================= LOV MODAL ================= */}
      <Modal visible={lovVisible} transparent={true} animationType="fade">
        <View style={styles.modalOverlay}>
          <View style={[styles.modalContainer, { backgroundColor: cardColor, borderColor: borderColor }]}>
            <View style={{ flexDirection: 'row', justifyContent: 'space-between', alignItems: 'center', marginBottom: 12 }}>
              <ThemedText style={[styles.modalTitle, { color: textColor, marginBottom: 0 }]}>{lovTitle}</ThemedText>
              <TouchableOpacity onPress={() => setLovVisible(false)}>
                <Ionicons name="close" size={24} color={textColor} />
              </TouchableOpacity>
            </View>
            <TextInput
              style={[styles.searchInput, { borderColor: borderColor, color: textColor, backgroundColor: backgroundColor }]}
              placeholder="Search..."
              placeholderTextColor="#888"
              value={lovSearch}
              onChangeText={handleLovSearch}
            />

            {/* LOV Header */}
            {lovColumns.length > 0 && (
              <View style={{ flexDirection: 'row', borderBottomWidth: 1, borderBottomColor: borderColor, paddingBottom: 8, marginBottom: 8 }}>
                {lovColumns.map((col, idx) => (
                  <ThemedText key={idx} style={{ flex: 1, fontWeight: 'bold', color: textColor, fontSize: 13 }}>
                    {col}
                  </ThemedText>
                ))}
              </View>
            )}

            <FlatList
              data={filteredLovData}
              keyExtractor={(item, index) => index.toString()}
              style={{ maxHeight: 300 }}
              renderItem={({ item }) => (
                <TouchableOpacity
                  style={[styles.lovItem, { borderBottomColor: borderColor, flexDirection: 'row', alignItems: 'center' }]}
                  onPress={() => handleLovSelect(item)}
                >
                  {lovColumns.length > 0 ? (
                    lovColumns.map((col, idx) => (
                      <ThemedText key={idx} style={{ flex: 1, color: textColor, fontSize: 13 }}>
                        {String(item[col] ?? '')}
                      </ThemedText>
                    ))
                  ) : (
                    <ThemedText style={{ color: textColor }}>{Object.values(item).join(' - ')}</ThemedText>
                  )}
                </TouchableOpacity>
              )}
            />
          </View>
        </View>
      </Modal>

      {/* Loading Overlay */}
      {loading && (
        <View style={styles.loadingOverlay}>
          <ActivityIndicator size="large" color="#B8903B" />
          <ThemedText style={{ color: '#FFF', marginTop: 10, fontWeight: 'bold' }}>Processing...</ThemedText>
        </View>
      )}

      {/* Toast Notification (screen-level, shown when modal is not visible) */}
      {toast.visible && !detailModalVisible && (
        <View
          style={[
            styles.toastContainer,
            { backgroundColor: toast.type === 'success' ? '#2E7D32' : toast.type === 'error' ? '#C62828' : '#1565C0' }
          ]}
        >
          <Ionicons
            name={toast.type === 'success' ? 'checkmark-circle' : toast.type === 'error' ? 'alert-circle' : 'information-circle'}
            size={18}
            color="#fff"
            style={{ marginRight: 8 }}
          />
          <ThemedText style={{ color: '#FFF', fontWeight: 'bold', fontSize: 13, textAlign: 'center' }}>
            {toast.message}
          </ThemedText>
        </View>
      )}
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  container: { flex: 1, paddingVertical: 12, paddingHorizontal: 16 },
  contentContainer: { paddingBottom: 32 },

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
  closedBadge: {
    marginLeft: 12,
    backgroundColor: '#C62828',
    paddingHorizontal: 8,
    paddingVertical: 2,
    borderRadius: 4,
  },
  closedBadgeText: {
    color: '#FFFFFF',
    fontSize: 10,
    fontWeight: 'bold',
  },
  cardBody: {
    padding: 10,
  },

  // Rows and Labels
  row: { flexDirection: 'row', alignItems: 'center', marginBottom: 7, flexWrap: 'wrap' },
  label: { fontSize: 11, fontWeight: 'bold', width: 96, marginRight: 6, color: '#3A2E19' },
  labelRight: { fontSize: 11, fontWeight: 'bold', textAlign: 'right', marginRight: 8, color: '#3A2E19' },

  // Inputs & Buttons
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

  // Pill Box
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

  // Checkboxes & Radios
  radioContainer: { flexDirection: 'row', alignItems: 'center', marginRight: 10 },
  checkboxSquare: {
    width: 15,
    height: 15,
    borderWidth: 1.5,
    borderColor: '#B8903B',
    borderRadius: 3,
    justifyContent: 'center',
    alignItems: 'center',
    marginRight: 5,
  },
  radioLabel: { fontSize: 11, color: '#362B1A', fontWeight: '500' },

  // Top Action Buttons
  topActionBtn: {
    paddingVertical: 6,
    paddingHorizontal: 12,
    borderRadius: 5,
    borderWidth: 1,
    borderColor: '#DFD5C3',
    backgroundColor: '#FFFFFF',
    marginHorizontal: 4,
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

  // IPM Box
  ipmBox: {
    borderWidth: 1,
    borderColor: '#E8DCB8',
    backgroundColor: '#FCF9F2',
    borderRadius: 6,
    padding: 8,
    marginTop: 6,
    marginBottom: 4,
  },
  ipmLabel: {
    fontSize: 10,
    fontWeight: 'bold',
    color: '#4A3B25',
    marginRight: 4,
  },

  // Modal & Generic Action Buttons
  section: { borderRadius: 4, borderWidth: 1, padding: 8 },
  actionBtn: { paddingVertical: 5, paddingHorizontal: 12, borderRadius: 4, borderWidth: 1, minWidth: 70, justifyContent: 'center', alignItems: 'center' },
  modalOverlay: { flex: 1, backgroundColor: 'rgba(0,0,0,0.5)', justifyContent: 'center', alignItems: 'center' },
  modalContainer: { width: '80%', borderRadius: 8, padding: 16, borderWidth: 1 },
  modalTitle: { fontSize: 16, fontWeight: 'bold', marginBottom: 12 },
  searchInput: { borderWidth: 1, borderRadius: 4, padding: 8, marginBottom: 12 },
  lovItem: { paddingVertical: 12, borderBottomWidth: 1 },

  // Loading & Toast
  loadingOverlay: {
    position: 'absolute',
    top: 0,
    left: 0,
    right: 0,
    bottom: 0,
    backgroundColor: 'rgba(0,0,0,0.4)',
    justifyContent: 'center',
    alignItems: 'center',
    zIndex: 9999
  },
  toastContainer: {
    position: 'absolute',
    top: 80,
    alignSelf: 'center',
    paddingVertical: 10,
    paddingHorizontal: 16,
    borderRadius: 8,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.25,
    shadowRadius: 3.84,
    elevation: 5,
    zIndex: 10000,
    maxWidth: '85%',
    minWidth: 200
  }
});
