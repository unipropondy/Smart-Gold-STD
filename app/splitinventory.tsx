import { ThemedText } from '@/components/themed-text';
import { API_BASE_URL } from '@/constants/api';
import { useThemeContext } from '@/contexts/ThemeContext';
import { useThemeColors } from '@/hooks/useThemeColors';
import { Ionicons } from '@expo/vector-icons';
import { Stack, useRouter } from 'expo-router';
import React, { useState } from 'react';
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
  TouchableWithoutFeedback,
  View
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';

const API_URL = `${API_BASE_URL}/splitinventory`;

export default function SplitInventoryScreen() {
  const themeColors = useThemeColors();
  const { accentColor } = useThemeContext();
  const router = useRouter();

  // ===== STATE =====
  const [loading, setLoading] = useState(false);
  const [toast, setToast] = useState<{ message: string, type: 'success' | 'error' } | null>(null);

  // Top Section
  const [inventoryCode, setInventoryCode] = useState('');
  const [description, setDescription] = useState('');
  const [weight, setWeight] = useState('');
  const [splitWeight, setSplitWeight] = useState('');
  const [pmc, setPmc] = useState('');
  const [smc, setSmc] = useState('');
  const [stoneWeight, setStoneWeight] = useState('');
  const [stoneSplitWeight, setStoneSplitWeight] = useState('');

  // Item details for original item retrieved
  const [originalDetails, setOriginalDetails] = useState<any>(null);
  const [panelsEnabled, setPanelsEnabled] = useState(false);

  // Left Section - Original Item
  const [orgCategory, setOrgCategory] = useState('');
  const [orgCatalogue, setOrgCatalogue] = useState('');
  const [originalInvCode, setOriginalInvCode] = useState('');
  const [originalInvWeight, setOriginalInvWeight] = useState('');
  const [originalStWeight, setOriginalStWeight] = useState('');
  const [orgDescription, setOrgDescription] = useState('');
  const [originalNewSMC, setOriginalNewSMC] = useState('');
  const [orgSizeCode, setOrgSizeCode] = useState('');
  const [orgSizeType, setOrgSizeType] = useState('');

  // Right Section - Split Item
  const [splitCategory, setSplitCategory] = useState('');
  const [splitCatalogue, setSplitCatalogue] = useState('');
  const [splitInvCode, setSplitInvCode] = useState('');
  const [splitInvWeight, setSplitInvWeight] = useState('');
  const [splitStWeight, setSplitStWeight] = useState('');
  const [splitNewDescription, setSplitNewDescription] = useState('');
  const [splitNewSMC, setSplitNewSMC] = useState('');
  const [splitSizeCode, setSplitSizeCode] = useState('');
  const [splitSizeType, setSplitSizeType] = useState('');
  const [isScrap, setIsScrap] = useState(false);
  const [isIPM, setIsIPM] = useState(false);

  // Additional Description Section
  const [chkAdditionalDesc, setChkAdditionalDesc] = useState(false);
  const [addlVisible, setAddlVisible] = useState(false);
  const [desc1, setDesc1] = useState('');
  const [desc2, setDesc2] = useState('');
  const [desc3, setDesc3] = useState('');
  const [desc4, setDesc4] = useState('');
  const [cost1, setCost1] = useState('');
  const [cost2, setCost2] = useState('');
  const [cost3, setCost3] = useState('');
  const [cost4, setCost4] = useState('');
  const [sp1, setSp1] = useState('');
  const [sp2, setSp2] = useState('');
  const [sp3, setSp3] = useState('');
  const [sp4, setSp4] = useState('');

  // LOV State
  const [lovVisible, setLovVisible] = useState(false);
  const [lovTitle, setLovTitle] = useState('');
  const [lovData, setLovData] = useState<any[]>([]);
  const [filteredLovData, setFilteredLovData] = useState<any[]>([]);
  const [lovSearch, setLovSearch] = useState('');
  const [lovColumns, setLovColumns] = useState<string[]>([]);
  const [onLovSelect, setOnLovSelect] = useState<((item: any) => void) | null>(null);

  const showToast = (message: string, type: 'success' | 'error' = 'success') => {
    setToast({ message, type });
    setTimeout(() => {
      setToast(null);
    }, 3000);
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
    const filtered = lovData.filter(item =>
      Object.values(item).some(val =>
        String(val).toLowerCase().includes(text.toLowerCase())
      )
    );
    setFilteredLovData(filtered);
  };

  // ===== API CALLS =====
  const safeTrim = (str: any) => (str ? String(str).trim() : '');

  const handleFetchDetail = async (codeOverride?: string) => {
    const code = codeOverride || inventoryCode;
    if (!code.trim()) return null;
    setLoading(true);
    try {
      const res = await fetch(`${API_URL}/detail?code=${code.trim()}`);
      const json = await res.json();
      if (json.success) {
        const data = json.data;
        setDescription(safeTrim(data.Description));
        setWeight(data.Weight !== null && data.Weight !== undefined ? String(data.Weight) : '0.00');
        setPmc(data.PMC !== null && data.PMC !== undefined ? String(data.PMC) : '0.00');
        setSmc(data.sMc !== null && data.sMc !== undefined ? String(data.sMc) : '0.00');
        setStoneWeight(data.StoneWeight !== null && data.StoneWeight !== undefined ? String(data.StoneWeight) : '0.00');
        setOriginalDetails(data);

        // Prep inputs
        setOriginalInvCode(safeTrim(data.InventoryCode));
        setOrgCategory(safeTrim(data.CategoryCode));
        setOrgCatalogue(safeTrim(data.CatalogueCode));
        setOrgDescription(safeTrim(data.Description));
        setOriginalNewSMC(data.sMc !== null && data.sMc !== undefined ? String(data.sMc) : '0.00');

        // Prepopulate sizes if present
        setOrgSizeCode(String(data.SizeCode || ''));
        setOrgSizeType(safeTrim(data.SizeType));
        setSplitSizeCode(String(data.SizeCode || ''));
        setSplitSizeType(safeTrim(data.SizeType));

        // Prep split description
        setSplitNewDescription(safeTrim(data.Description));

        // Prep split defaults immediately on load
        const cat = safeTrim(data.CategoryCode);
        const catal = safeTrim(data.CatalogueCode);
        setSplitCategory(cat);
        setSplitCatalogue(catal);
        setSplitNewSMC(data.sMc !== null && data.sMc !== undefined ? String(data.sMc) : '0.00');

        // Fetch next inventory code for split item immediately
        try {
          const nextCodeRes = await fetch(`${API_URL}/nextCode?categoryCode=${cat}`);
          const nextCodeJson = await nextCodeRes.json();
          if (nextCodeJson.success) {
            setSplitInvCode(nextCodeJson.data);
          }
        } catch (e) {
          console.error("Failed to load next inventory code on fetch detail", e);
        }

        if (json.additionalDescription) {
          const addl = json.additionalDescription;
          setDesc1(safeTrim(addl.Description1));
          setDesc2(safeTrim(addl.Description2));
          setDesc3(safeTrim(addl.Description3));
          setDesc4(safeTrim(addl.Description4));
          setCost1(addl.Cost1 !== null && addl.Cost1 !== undefined ? String(addl.Cost1) : '');
          setCost2(addl.Cost2 !== null && addl.Cost2 !== undefined ? String(addl.Cost2) : '');
          setCost3(addl.Cost3 !== null && addl.Cost3 !== undefined ? String(addl.Cost3) : '');
          setCost4(addl.Cost4 !== null && addl.Cost4 !== undefined ? String(addl.Cost4) : '');
          setSp1(addl.SellingPrice1 !== null && addl.SellingPrice1 !== undefined ? String(addl.SellingPrice1) : '');
          setSp2(addl.SellingPrice2 !== null && addl.SellingPrice2 !== undefined ? String(addl.SellingPrice2) : '');
          setSp3(addl.SellingPrice3 !== null && addl.SellingPrice3 !== undefined ? String(addl.SellingPrice3) : '');
          setSp4(addl.SellingPrice4 !== null && addl.SellingPrice4 !== undefined ? String(addl.SellingPrice4) : '');
        }
        showToast("Inventory Details Loaded.", "success");
        return data;
      } else {
        showToast(json.message || "Failed to load inventory details.", "error");
        handleClearAll();
        return null;
      }
    } catch (e) {
      showToast("Server connection failed.", "error");
      return null;
    } finally {
      setLoading(false);
    }
  };

  const handleSplit = async () => {
    let currentDetails = originalDetails;
    let currentWeight = parseFloat(weight) || 0;
    let currentStoneWeight = parseFloat(stoneWeight) || 0;
    let currentSMC = parseFloat(smc) || 0;

    if (!currentDetails && inventoryCode.trim()) {
      const fetched = await handleFetchDetail(inventoryCode);
      if (fetched) {
        currentDetails = fetched;
        currentWeight = parseFloat(fetched.Weight) || 0;
        currentStoneWeight = parseFloat(fetched.StoneWeight) || 0;
        currentSMC = parseFloat(fetched.sMc) || 0;
      }
    }

    if (!currentDetails) {
      showToast("Inventory Details must be loaded first.", "error");
      return;
    }

    const splitWt = parseFloat(splitWeight) || 0;
    const stoneSplitWt = parseFloat(stoneSplitWeight) || 0;

    if (splitWt <= 0) {
      showToast("Split weight must be greater than Zero.", "error");
      return;
    }
    if (splitWt >= currentWeight) {
      showToast("Split Weight should be less than Inventory Weight.", "error");
      return;
    }

    const calculatedOrgW = currentWeight - splitWt;
    const calculatedSplitW = splitWt;
    const calculatedOrgStW = currentStoneWeight - stoneSplitWt;
    const calculatedSplitStW = stoneSplitWt;

    setOriginalInvWeight(calculatedOrgW.toFixed(3));
    setSplitInvWeight(calculatedSplitW.toFixed(3));
    setOriginalStWeight(calculatedOrgStW.toFixed(3));
    setSplitStWeight(calculatedSplitStW.toFixed(3));

    // SMC Calculation
    const smcPerGram = currentSMC / currentWeight;
    setOriginalNewSMC((smcPerGram * calculatedOrgW).toFixed(2));
    setSplitNewSMC((smcPerGram * calculatedSplitW).toFixed(2));

    // Assign categories/catalogues and details
    const defaultCat = currentDetails.CategoryCode.trim();
    const defaultCatal = currentDetails.CatalogueCode.trim();
    setSplitCategory(defaultCat);
    setSplitCatalogue(defaultCatal);

    // Fetch next inventory code for split item
    setLoading(true);
    try {
      const res = await fetch(`${API_URL}/nextCode?categoryCode=${defaultCat}`);
      const json = await res.json();
      if (json.success) {
        setSplitInvCode(json.data);
      }
    } catch (e) {
      console.error("Failed to load next inventory code for split item", e);
    } finally {
      setLoading(false);
    }

    setPanelsEnabled(true);
    showToast("Calculated Split Weights.", "success");
  };

  // LOV Category Org
  const handleOrgCategoryLov = async () => {
    if (!panelsEnabled) return;
    setLoading(true);
    try {
      const res = await fetch(`${API_URL}/categories`);
      const json = await res.json();
      if (json.success) {
        openLov("Select Category", json.data, ["CategoryCode", "CategoryName"], (item) => {
          setOrgCategory(item.CategoryCode.trim());
          if (item.CategoryCode.trim() !== originalDetails.CategoryCode.trim()) {
            setOriginalInvCode(item.NextInventoryCode.trim());
          } else {
            setOriginalInvCode(originalDetails.InventoryCode.trim());
          }
          setLovVisible(false);
        });
      }
    } catch (e) {
      showToast("Failed to load category LOV", "error");
    } finally {
      setLoading(false);
    }
  };

  // LOV Category Split
  const handleSplitCategoryLov = async () => {
    if (!panelsEnabled || isScrap) return;
    setLoading(true);
    try {
      const res = await fetch(`${API_URL}/categories`);
      const json = await res.json();
      if (json.success) {
        openLov("Select Category", json.data, ["CategoryCode", "CategoryName"], (item) => {
          setSplitCategory(item.CategoryCode.trim());
          setSplitInvCode(item.NextInventoryCode.trim());
          setLovVisible(false);
        });
      }
    } catch (e) {
      showToast("Failed to load category LOV", "error");
    } finally {
      setLoading(false);
    }
  };

  // LOV Catalogue Org
  const handleOrgCatalogueLov = async () => {
    if (!panelsEnabled) return;
    setLoading(true);
    try {
      const res = await fetch(`${API_URL}/catalogues?categoryCode=${orgCategory}`);
      const json = await res.json();
      if (json.success) {
        openLov("Select Catalogue", json.data, ["CatalogueCode", "CatalogueName"], (item) => {
          setOrgCatalogue(item.CatalogueCode.trim());
          setOrgDescription(item.CatalogueName.trim());
          setLovVisible(false);
        });
      }
    } catch (e) {
      showToast("Failed to load catalogue LOV", "error");
    } finally {
      setLoading(false);
    }
  };

  // LOV Catalogue Split
  const handleSplitCatalogueLov = async () => {
    if (!panelsEnabled || isScrap) return;
    setLoading(true);
    try {
      const res = await fetch(`${API_URL}/catalogues?categoryCode=${splitCategory}`);
      const json = await res.json();
      if (json.success) {
        openLov("Select Catalogue", json.data, ["CatalogueCode", "CatalogueName"], (item) => {
          setSplitCatalogue(item.CatalogueCode.trim());
          setSplitNewDescription(item.CatalogueName.trim());
          setLovVisible(false);
        });
      }
    } catch (e) {
      showToast("Failed to load catalogue LOV", "error");
    } finally {
      setLoading(false);
    }
  };

  // Size LOVs
  const handleOrgSizeLov = async () => {
    if (!panelsEnabled) return;
    setLoading(true);
    try {
      const res = await fetch(`${API_URL}/sizes?categoryCode=${orgCategory}`);
      const json = await res.json();
      if (json.success) {
        openLov("Select Size", json.data, ["SizeCode", "SizeType"], (item) => {
          setOrgSizeCode(String(item.SizeCode));
          setOrgSizeType(item.SizeType.trim());
          setLovVisible(false);
        });
      }
    } catch (e) {
      showToast("Failed to load size LOV", "error");
    } finally {
      setLoading(false);
    }
  };

  const handleSplitSizeLov = async () => {
    if (!panelsEnabled || isScrap) return;
    setLoading(true);
    try {
      const res = await fetch(`${API_URL}/sizes?categoryCode=${splitCategory}`);
      const json = await res.json();
      if (json.success) {
        openLov("Select Size", json.data, ["SizeCode", "SizeType"], (item) => {
          setSplitSizeCode(String(item.SizeCode));
          setSplitSizeType(item.SizeType.trim());
          setLovVisible(false);
        });
      }
    } catch (e) {
      showToast("Failed to load size LOV", "error");
    } finally {
      setLoading(false);
    }
  };

  const handleScrapToggle = async (val: boolean) => {
    setIsScrap(val);
    if (val) {
      setSplitCategory("SCRAP");
      setSplitCatalogue("SCRAP");
      setSplitNewDescription("SCRAP INVENTORY");
      setSplitSizeCode("");
      setSplitSizeType("");

      // Fetch scrap code from backend
      setLoading(true);
      try {
        const res = await fetch(`${API_URL}/nextScrapCode`);
        const json = await res.json();
        if (json.success) {
          setSplitInvCode(json.data);
        }
      } catch (e) {
        showToast("Failed to generate scrap code", "error");
      } finally {
        setLoading(false);
      }
    } else {
      setSplitCategory("");
      setSplitCatalogue("");
      setSplitInvCode("");
      setSplitNewDescription("");
    }
  };

  const handleClearAll = () => {
    setInventoryCode('');
    setDescription('');
    setWeight('');
    setSplitWeight('');
    setPmc('');
    setSmc('');
    setStoneWeight('');
    setStoneSplitWeight('');
    setOriginalDetails(null);
    setPanelsEnabled(false);

    // Org
    setOrgCategory('');
    setOrgCatalogue('');
    setOriginalInvCode('');
    setOriginalInvWeight('');
    setOriginalStWeight('');
    setOrgDescription('');
    setOriginalNewSMC('');
    setOrgSizeCode('');
    setOrgSizeType('');

    // Split
    setSplitCategory('');
    setSplitCatalogue('');
    setSplitInvCode('');
    setSplitInvWeight('');
    setSplitStWeight('');
    setSplitNewDescription('');
    setSplitNewSMC('');
    setSplitSizeCode('');
    setSplitSizeType('');
    setIsScrap(false);
    setIsIPM(false);

    // Addl
    setChkAdditionalDesc(false);
    setDesc1('');
    setDesc2('');
    setDesc3('');
    setDesc4('');
    setCost1('');
    setCost2('');
    setCost3('');
    setCost4('');
    setSp1('');
    setSp2('');
    setSp3('');
    setSp4('');
  };

  const handleUpdateSave = async () => {
    if (!originalDetails) {
      showToast("Select and load inventory details first.", "error");
      return;
    }
    if (!originalInvCode || !splitInvCode) {
      showToast("Both inventory split codes are required.", "error");
      return;
    }
    if (!originalInvWeight || !splitInvWeight) {
      showToast("Weights must be calculated before saving.", "error");
      return;
    }

    setLoading(true);
    try {
      const payload = {
        selectedInventoryCode: originalDetails.InventoryCode,
        originalInvCode,
        splitInvCode,
        originalItem: {
          categoryCode: orgCategory,
          catalogueCode: orgCatalogue,
          weight: originalInvWeight,
          stoneWeight: originalStWeight,
          smc: originalNewSMC,
          description: orgDescription,
          sizeCode: orgSizeCode,
          sizeType: orgSizeType
        },
        splitItem: {
          categoryCode: splitCategory,
          catalogueCode: splitCatalogue,
          weight: splitInvWeight,
          stoneWeight: splitStWeight,
          smc: splitNewSMC,
          description: splitNewDescription,
          sizeCode: splitSizeCode,
          sizeType: splitSizeType,
          isScrap,
          isIPM,
          pmc: parseFloat(pmc) * (parseFloat(splitInvWeight) / parseFloat(weight)) // Proportionate PMC
        },
        originalDetails,
        additionalDescription: chkAdditionalDesc ? {
          desc1, desc2, desc3, desc4,
          cost1, cost2, cost3, cost4,
          sp1, sp2, sp3, sp4
        } : null,
        userId: "sys"
      };

      const res = await fetch(`${API_URL}/split`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(payload)
      });
      const json = await res.json();
      if (json.success) {
        showToast(json.message || "Split Saved Successfully.", "success");
        handleClearAll();
      } else {
        showToast(json.message || "Failed to split inventory.", "error");
      }
    } catch (e) {
      showToast("Network save failed.", "error");
    } finally {
      setLoading(false);
    }
  };

  return (
    <SafeAreaView style={styles.safeArea} edges={['top']}>
      <Stack.Screen options={{ headerShown: false }} />

      {/* Toast Alert */}
      {toast && (
        <View
          style={[
            styles.toast,
            {
              backgroundColor: toast.type === 'success' ? '#2E7D32' : '#D32F2F',
            },
          ]}
        >
          <ThemedText style={styles.toastText}>{toast.message}</ThemedText>
        </View>
      )}

      {/* Top Header Bar */}
      <View style={styles.header}>
        <View style={{ flexDirection: 'row', alignItems: 'center' }}>
          <TouchableOpacity style={styles.backButton} onPress={() => router.back()}>
            <Ionicons name="arrow-back" size={22} color="#3B2D17" />
          </TouchableOpacity>
          <ThemedText style={styles.title}>Split & Return Inventory</ThemedText>
        </View>
        <View style={{ flexDirection: 'row', alignItems: 'center' }}>
          <TouchableOpacity style={styles.clearBtn} onPress={handleClearAll}>
            <Ionicons name="refresh-outline" size={14} color="#5C4A28" style={{ marginRight: 4 }} />
            <ThemedText style={styles.clearBtnText}>Clear</ThemedText>
          </TouchableOpacity>
          <TouchableOpacity style={styles.updateBtn} onPress={handleUpdateSave}>
            <Ionicons name="save-outline" size={14} color="#9E6D08" style={{ marginRight: 4 }} />
            <ThemedText style={styles.updateBtnText}>Update</ThemedText>
          </TouchableOpacity>
        </View>
      </View>

      <KeyboardAvoidingView behavior={Platform.OS === 'ios' ? 'padding' : 'height'} style={{ flex: 1 }}>
        <ScrollView style={styles.container} contentContainerStyle={styles.contentContainer}>
          {/* Top Gold Card: Query Inputs */}
          <View style={styles.goldCard}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerIconBadge}>
                <Ionicons name="cut-outline" size={14} color="#B48218" />
              </View>
              <ThemedText style={styles.cardHeaderTitle}>Source Inventory & Split Weights</ThemedText>
            </View>

            <View style={styles.cardBody}>
              <View style={styles.formRowWrap}>
                {/* Left Sub-Column */}
                <View style={styles.subColumn}>
                  {/* Inventory Code */}
                  <View style={styles.fieldRow}>
                    <ThemedText style={styles.fieldLabel}>Inventory Code</ThemedText>
                    <View style={styles.inputWithAction}>
                      <TextInput
                        style={[styles.input, { flex: 1, borderTopRightRadius: 0, borderBottomRightRadius: 0 }]}
                        value={inventoryCode}
                        onChangeText={setInventoryCode}
                        onBlur={() => handleFetchDetail()}
                        onSubmitEditing={() => handleFetchDetail()}
                        placeholder="Scan / Type Code"
                        placeholderTextColor="#A89D8B"
                      />
                      <TouchableOpacity style={styles.lookupBtn} onPress={() => handleFetchDetail()}>
                        <Ionicons name="search" size={13} color="#6E5528" />
                      </TouchableOpacity>
                    </View>
                  </View>

                  {/* Weight */}
                  <View style={styles.fieldRow}>
                    <ThemedText style={styles.fieldLabel}>Total Weight</ThemedText>
                    <TextInput style={[styles.input, styles.readonlyInput, { flex: 1, textAlign: 'right' }]} value={weight} editable={false} />
                  </View>

                  {/* PMC */}
                  <View style={styles.fieldRow}>
                    <ThemedText style={styles.fieldLabel}>PMC</ThemedText>
                    <TextInput style={[styles.input, styles.readonlyInput, { flex: 1, textAlign: 'right' }]} value={pmc} editable={false} />
                  </View>

                  {/* Stone Weight */}
                  <View style={styles.fieldRow}>
                    <ThemedText style={styles.fieldLabel}>Stone Weight</ThemedText>
                    <TextInput style={[styles.input, styles.readonlyInput, { flex: 1, textAlign: 'right' }]} value={stoneWeight} editable={false} />
                  </View>
                </View>

                {/* Right Sub-Column */}
                <View style={styles.subColumn}>
                  {/* Description */}
                  <View style={styles.fieldRow}>
                    <ThemedText style={styles.fieldLabel}>Description</ThemedText>
                    <TextInput style={[styles.input, styles.readonlyInput, { flex: 1 }]} value={description} editable={false} />
                  </View>

                  {/* Split Weight */}
                  <View style={styles.fieldRow}>
                    <ThemedText style={styles.fieldLabel}>Split Weight</ThemedText>
                    <TextInput
                      style={[styles.input, { flex: 1, textAlign: 'right' }]}
                      value={splitWeight}
                      onChangeText={setSplitWeight}
                      keyboardType="numeric"
                      placeholder="0.000"
                      placeholderTextColor="#A89D8B"
                    />
                  </View>

                  {/* SMC */}
                  <View style={styles.fieldRow}>
                    <ThemedText style={styles.fieldLabel}>SMC</ThemedText>
                    <TextInput style={[styles.input, styles.readonlyInput, { flex: 1, textAlign: 'right' }]} value={smc} editable={false} />
                  </View>

                  {/* Stone Split Weight & Split Trigger Button */}
                  <View style={styles.fieldRow}>
                    <ThemedText style={styles.fieldLabel}>Stone Split Wt</ThemedText>
                    <TextInput
                      style={[styles.input, { flex: 1, textAlign: 'right' }]}
                      value={stoneSplitWeight}
                      onChangeText={setStoneSplitWeight}
                      keyboardType="numeric"
                      placeholder="0.000"
                      placeholderTextColor="#A89D8B"
                    />
                    <TouchableOpacity style={styles.splitBtn} onPress={handleSplit}>
                      <Ionicons name="git-branch-outline" size={13} color="#9E6D08" style={{ marginRight: 4 }} />
                      <ThemedText style={styles.splitBtnText}>Calculate</ThemedText>
                    </TouchableOpacity>
                  </View>
                </View>
              </View>
            </View>
          </View>

          {/* Double Column Split Panels */}
          <View style={styles.columnsContainer}>
            {/* Left Panel: Original Item */}
            <View
              style={[styles.goldCard, styles.splitColumnCard, { opacity: panelsEnabled ? 1 : 0.6 }]}
              pointerEvents={panelsEnabled ? 'auto' : 'none'}
            >
              <View style={styles.cardHeaderBanner}>
                <View style={styles.headerIconBadge}>
                  <Ionicons name="document-text-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitle}>Original Item (Remaining)</ThemedText>
              </View>

              <View style={styles.cardBody}>
                {/* Category */}
                <View style={styles.fieldRow}>
                  <ThemedText style={styles.columnFieldLabel}>Category</ThemedText>
                  <View style={styles.inputWithAction}>
                    <TextInput style={[styles.input, styles.readonlyInput, { flex: 1, borderTopRightRadius: 0, borderBottomRightRadius: 0 }]} value={orgCategory} editable={false} />
                    <TouchableOpacity style={styles.lookupBtn} onPress={handleOrgCategoryLov}>
                      <ThemedText style={styles.lookupBtnText}>...</ThemedText>
                    </TouchableOpacity>
                  </View>
                </View>

                {/* Catalogue */}
                <View style={styles.fieldRow}>
                  <ThemedText style={styles.columnFieldLabel}>Catalogue</ThemedText>
                  <View style={styles.inputWithAction}>
                    <TextInput style={[styles.input, styles.readonlyInput, { flex: 1, borderTopRightRadius: 0, borderBottomRightRadius: 0 }]} value={orgCatalogue} editable={false} />
                    <TouchableOpacity style={styles.lookupBtn} onPress={handleOrgCatalogueLov}>
                      <ThemedText style={styles.lookupBtnText}>...</ThemedText>
                    </TouchableOpacity>
                  </View>
                </View>

                {/* Inventory Code */}
                <View style={styles.fieldRow}>
                  <ThemedText style={styles.columnFieldLabel}>Inventory Code</ThemedText>
                  <TextInput style={[styles.input, styles.readonlyInput, { flex: 1, fontWeight: 'bold' }]} value={originalInvCode} editable={false} />
                </View>

                {/* Weight */}
                <View style={styles.fieldRow}>
                  <ThemedText style={styles.columnFieldLabel}>Remaining Wt</ThemedText>
                  <TextInput style={[styles.input, styles.readonlyInput, { flex: 1, textAlign: 'right', fontWeight: 'bold' }]} value={originalInvWeight} editable={false} />
                </View>

                {/* Stone Weight */}
                <View style={styles.fieldRow}>
                  <ThemedText style={styles.columnFieldLabel}>Stone Weight</ThemedText>
                  <TextInput style={[styles.input, styles.readonlyInput, { flex: 1, textAlign: 'right' }]} value={originalStWeight} editable={false} />
                </View>

                {/* New Desc */}
                <View style={styles.fieldRow}>
                  <ThemedText style={styles.columnFieldLabel}>New Desc.</ThemedText>
                  <TextInput style={[styles.input, { flex: 1 }]} value={orgDescription} onChangeText={setOrgDescription} />
                </View>

                {/* SMC */}
                <View style={styles.fieldRow}>
                  <ThemedText style={styles.columnFieldLabel}>Adjusted SMC</ThemedText>
                  <TextInput style={[styles.input, { flex: 1, textAlign: 'right' }]} value={originalNewSMC} onChangeText={setOriginalNewSMC} keyboardType="numeric" />
                </View>

                {/* Size */}
                <View style={styles.fieldRow}>
                  <ThemedText style={styles.columnFieldLabel}>Size Code</ThemedText>
                  <View style={styles.inputWithAction}>
                    <TextInput style={[styles.input, styles.readonlyInput, { flex: 1, borderTopRightRadius: 0, borderBottomRightRadius: 0 }]} value={orgSizeCode} editable={false} />
                    <TouchableOpacity style={styles.lookupBtn} onPress={handleOrgSizeLov}>
                      <ThemedText style={styles.lookupBtnText}>...</ThemedText>
                    </TouchableOpacity>
                  </View>
                </View>
              </View>
            </View>

            {/* Right Panel: Split Item */}
            <View
              style={[styles.goldCard, styles.splitColumnCard, { opacity: panelsEnabled ? 1 : 0.6 }]}
              pointerEvents={panelsEnabled ? 'auto' : 'none'}
            >
              <View style={styles.cardHeaderBanner}>
                <View style={styles.headerIconBadge}>
                  <Ionicons name="sparkles-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitle}>Split Item (New)</ThemedText>
                {isScrap && (
                  <View style={styles.scrapBadge}>
                    <ThemedText style={styles.scrapBadgeText}>SCRAP</ThemedText>
                  </View>
                )}
              </View>

              <View style={styles.cardBody}>
                {/* Category & Scrap Toggle */}
                <View style={styles.fieldRow}>
                  <ThemedText style={styles.columnFieldLabel}>Category</ThemedText>
                  <View style={styles.inputWithAction}>
                    <TextInput style={[styles.input, styles.readonlyInput, { flex: 1, borderTopRightRadius: 0, borderBottomRightRadius: 0 }]} value={splitCategory} editable={false} />
                    <TouchableOpacity style={styles.lookupBtn} onPress={handleSplitCategoryLov} disabled={isScrap}>
                      <ThemedText style={styles.lookupBtnText}>...</ThemedText>
                    </TouchableOpacity>
                  </View>
                  <TouchableOpacity
                    onPress={() => handleScrapToggle(!isScrap)}
                    style={[styles.checkboxContainer, { marginLeft: 10 }]}
                  >
                    <View style={[styles.checkboxSquare, isScrap && styles.checkboxSquareChecked]}>
                      {isScrap && <Ionicons name="checkmark" size={12} color="#FFF" />}
                    </View>
                    <ThemedText style={styles.checkboxLabel}>Scrap</ThemedText>
                  </TouchableOpacity>
                </View>

                {/* Catalogue */}
                <View style={styles.fieldRow}>
                  <ThemedText style={styles.columnFieldLabel}>Catalogue</ThemedText>
                  <View style={styles.inputWithAction}>
                    <TextInput style={[styles.input, styles.readonlyInput, { flex: 1, borderTopRightRadius: 0, borderBottomRightRadius: 0 }]} value={splitCatalogue} editable={false} />
                    <TouchableOpacity style={styles.lookupBtn} onPress={handleSplitCatalogueLov} disabled={isScrap}>
                      <ThemedText style={styles.lookupBtnText}>...</ThemedText>
                    </TouchableOpacity>
                  </View>
                </View>

                {/* Inventory Code */}
                <View style={styles.fieldRow}>
                  <ThemedText style={styles.columnFieldLabel}>New Inv Code</ThemedText>
                  <TextInput style={[styles.input, styles.readonlyInput, { flex: 1, fontWeight: 'bold' }]} value={splitInvCode} editable={false} />
                </View>

                {/* Weight */}
                <View style={styles.fieldRow}>
                  <ThemedText style={styles.columnFieldLabel}>Split Weight</ThemedText>
                  <TextInput style={[styles.input, styles.readonlyInput, { flex: 1, textAlign: 'right', fontWeight: 'bold', color: '#9E6D08' }]} value={splitInvWeight} editable={false} />
                </View>

                {/* Stone Weight */}
                <View style={styles.fieldRow}>
                  <ThemedText style={styles.columnFieldLabel}>Stone Weight</ThemedText>
                  <TextInput style={[styles.input, styles.readonlyInput, { flex: 1, textAlign: 'right' }]} value={splitStWeight} editable={false} />
                </View>

                {/* New Desc */}
                <View style={styles.fieldRow}>
                  <ThemedText style={styles.columnFieldLabel}>New Desc.</ThemedText>
                  <TextInput style={[styles.input, { flex: 1 }]} value={splitNewDescription} onChangeText={setSplitNewDescription} />
                </View>

                {/* SMC & IPM Checkbox */}
                <View style={styles.fieldRow}>
                  <ThemedText style={styles.columnFieldLabel}>New SMC</ThemedText>
                  <TextInput style={[styles.input, { flex: 1, textAlign: 'right' }]} value={splitNewSMC} onChangeText={setSplitNewSMC} keyboardType="numeric" />
                  <TouchableOpacity
                    onPress={() => setIsIPM(!isIPM)}
                    style={[styles.checkboxContainer, { marginLeft: 10 }]}
                  >
                    <View style={[styles.checkboxSquare, isIPM && styles.checkboxSquareChecked]}>
                      {isIPM && <Ionicons name="checkmark" size={12} color="#FFF" />}
                    </View>
                    <ThemedText style={styles.checkboxLabel}>IPM</ThemedText>
                  </TouchableOpacity>
                </View>

                {/* Size */}
                <View style={styles.fieldRow}>
                  <ThemedText style={styles.columnFieldLabel}>Size Code</ThemedText>
                  <View style={styles.inputWithAction}>
                    <TextInput style={[styles.input, styles.readonlyInput, { flex: 1, borderTopRightRadius: 0, borderBottomRightRadius: 0 }]} value={splitSizeCode} editable={false} />
                    <TouchableOpacity style={styles.lookupBtn} onPress={handleSplitSizeLov} disabled={isScrap}>
                      <ThemedText style={styles.lookupBtnText}>...</ThemedText>
                    </TouchableOpacity>
                  </View>
                </View>
              </View>
            </View>
          </View>

          {/* Additional Description Trigger Card */}
          <View style={[styles.goldCard, { marginTop: 12 }]}>
            <View style={[styles.cardHeaderBanner, { justifyContent: 'space-between' }]}>
              <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                <TouchableOpacity
                  onPress={() => {
                    const next = !chkAdditionalDesc;
                    setChkAdditionalDesc(next);
                    if (next) setAddlVisible(true);
                  }}
                  style={styles.checkboxContainer}
                >
                  <View style={[styles.checkboxSquare, chkAdditionalDesc && styles.checkboxSquareChecked]}>
                    {chkAdditionalDesc && <Ionicons name="checkmark" size={12} color="#FFF" />}
                  </View>
                  <ThemedText style={styles.cardHeaderTitle}>Enable Additional Descriptions</ThemedText>
                </TouchableOpacity>
              </View>
              {chkAdditionalDesc && (
                <TouchableOpacity style={styles.editAddlBtn} onPress={() => setAddlVisible(true)}>
                  <Ionicons name="create-outline" size={13} color="#9E6D08" style={{ marginRight: 4 }} />
                  <ThemedText style={styles.editAddlBtnText}>Edit Lines</ThemedText>
                </TouchableOpacity>
              )}
            </View>
          </View>
        </ScrollView>
      </KeyboardAvoidingView>

      {/* Additional Description Modal */}
      <Modal visible={addlVisible} transparent={true} animationType="fade">
        <TouchableWithoutFeedback onPress={() => setAddlVisible(false)}>
          <View style={styles.modalOverlay}>
            <TouchableWithoutFeedback>
              <View style={styles.modalContainer}>
                <View style={styles.modalHeader}>
                  <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                    <View style={styles.headerIconBadge}>
                      <Ionicons name="list" size={14} color="#B48218" />
                    </View>
                    <ThemedText style={styles.modalTitle}>Additional Descriptions</ThemedText>
                  </View>
                  <TouchableOpacity onPress={() => setAddlVisible(false)} style={{ padding: 4 }}>
                    <Ionicons name="close-circle-outline" size={22} color="#7A684C" />
                  </TouchableOpacity>
                </View>

                <View style={{ padding: 14 }}>
                  {/* Table Header */}
                  <View style={styles.addlTableHeader}>
                    <ThemedText style={[styles.addlHeaderText, { flex: 2.2 }]}>Description</ThemedText>
                    <ThemedText style={[styles.addlHeaderText, { flex: 1, textAlign: 'right' }]}>Cost</ThemedText>
                    <ThemedText style={[styles.addlHeaderText, { flex: 1, textAlign: 'right' }]}>Selling Price</ThemedText>
                  </View>

                  {/* Rows */}
                  {[
                    { desc: desc1, setDesc: setDesc1, cost: cost1, setCost: setCost1, sp: sp1, setSp: setSp1, label: 'Line 1' },
                    { desc: desc2, setDesc: setDesc2, cost: cost2, setCost: setCost2, sp: sp2, setSp: setSp2, label: 'Line 2' },
                    { desc: desc3, setDesc: setDesc3, cost: cost3, setCost: setCost3, sp: sp3, setSp: setSp3, label: 'Line 3' },
                    { desc: desc4, setDesc: setDesc4, cost: cost4, setCost: setCost4, sp: sp4, setSp: setSp4, label: 'Line 4' },
                  ].map((row, idx) => (
                    <View key={idx} style={styles.addlTableRow}>
                      <TextInput
                        style={[styles.input, { flex: 2.2, marginRight: 8 }]}
                        value={row.desc}
                        onChangeText={row.setDesc}
                        placeholder={`Description ${idx + 1}`}
                        placeholderTextColor="#A89D8B"
                      />
                      <TextInput
                        style={[styles.input, { flex: 1, textAlign: 'right', marginRight: 8 }]}
                        value={row.cost}
                        onChangeText={row.setCost}
                        placeholder="0.00"
                        placeholderTextColor="#A89D8B"
                        keyboardType="numeric"
                      />
                      <TextInput
                        style={[styles.input, { flex: 1, textAlign: 'right' }]}
                        value={row.sp}
                        onChangeText={row.setSp}
                        placeholder="0.00"
                        placeholderTextColor="#A89D8B"
                        keyboardType="numeric"
                      />
                    </View>
                  ))}
                </View>

                <View style={styles.modalFooter}>
                  <TouchableOpacity style={styles.modalSaveBtn} onPress={() => setAddlVisible(false)}>
                    <ThemedText style={styles.modalSaveBtnText}>OK / Done</ThemedText>
                  </TouchableOpacity>
                </View>
              </View>
            </TouchableWithoutFeedback>
          </View>
        </TouchableWithoutFeedback>
      </Modal>

      {/* LOV Selection Modal */}
      <Modal visible={lovVisible} transparent={true} animationType="fade">
        <TouchableWithoutFeedback onPress={() => setLovVisible(false)}>
          <View style={styles.modalOverlay}>
            <TouchableWithoutFeedback>
              <View style={styles.modalContainer}>
                <View style={styles.modalHeader}>
                  <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                    <View style={styles.headerIconBadge}>
                      <Ionicons name="list" size={14} color="#B48218" />
                    </View>
                    <ThemedText style={styles.modalTitle}>{lovTitle}</ThemedText>
                  </View>
                  <TouchableOpacity onPress={() => setLovVisible(false)} style={{ padding: 4 }}>
                    <Ionicons name="close-circle-outline" size={22} color="#7A684C" />
                  </TouchableOpacity>
                </View>

                <View style={{ padding: 12 }}>
                  <TextInput
                    style={[styles.input, { marginBottom: 10, height: 32 }]}
                    placeholder="Search options..."
                    placeholderTextColor="#A89D8B"
                    value={lovSearch}
                    onChangeText={handleLovSearch}
                  />

                  {lovColumns.length > 0 && (
                    <View style={styles.lovHeaderRow}>
                      {lovColumns.map((col, idx) => (
                        <ThemedText key={idx} style={[styles.lovHeaderText, { flex: 1 }]}>
                          {col}
                        </ThemedText>
                      ))}
                    </View>
                  )}

                  <FlatList
                    data={filteredLovData}
                    keyExtractor={(item, index) => index.toString()}
                    style={{ maxHeight: 340 }}
                    renderItem={({ item, index }) => (
                      <TouchableOpacity
                        style={[styles.lovItem, index % 2 === 1 && { backgroundColor: '#FCFAF6' }]}
                        onPress={() => onLovSelect && onLovSelect(item)}
                      >
                        {lovColumns.length > 0 ? (
                          lovColumns.map((col, idx) => (
                            <ThemedText key={idx} style={[styles.lovItemText, { flex: 1 }]}>
                              {String(item[col] || '')}
                            </ThemedText>
                          ))
                        ) : (
                          <ThemedText style={styles.lovItemText}>{Object.values(item).join(' - ')}</ThemedText>
                        )}
                      </TouchableOpacity>
                    )}
                    ListEmptyComponent={() => (
                      <View style={{ padding: 24, alignItems: 'center' }}>
                        <ThemedText style={{ color: '#7A684C', fontSize: 13 }}>No options found</ThemedText>
                      </View>
                    )}
                  />
                </View>

                <View style={styles.modalFooter}>
                  <TouchableOpacity style={styles.modalCloseBtn} onPress={() => setLovVisible(false)}>
                    <ThemedText style={styles.modalCloseBtnText}>Close</ThemedText>
                  </TouchableOpacity>
                </View>
              </View>
            </TouchableWithoutFeedback>
          </View>
        </TouchableWithoutFeedback>
      </Modal>

      {/* Loading Overlay */}
      {loading && (
        <View style={styles.loaderOverlay}>
          <ActivityIndicator size="large" color="#B48218" />
          <ThemedText style={{ color: '#FFFFFF', fontWeight: 'bold', marginTop: 10 }}>Processing...</ThemedText>
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
  header: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingHorizontal: 16,
    paddingVertical: 10,
    borderBottomWidth: 1,
    borderBottomColor: '#EBE2D2',
    backgroundColor: '#FFFFFF',
  },
  backButton: {
    padding: 4,
    marginRight: 12,
  },
  title: {
    fontSize: 20,
    fontWeight: '400',
    letterSpacing: 1,
    color: '#3B2D17',
  },
  clearBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#DFD5C3',
    paddingVertical: 6,
    paddingHorizontal: 12,
    borderRadius: 5,
    marginRight: 8,
  },
  clearBtnText: {
    color: '#5C4A28',
    fontWeight: 'bold',
    fontSize: 12,
  },
  updateBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#F9F2E2',
    borderWidth: 1,
    borderColor: '#B8903B',
    paddingVertical: 6,
    paddingHorizontal: 14,
    borderRadius: 5,
  },
  updateBtnText: {
    color: '#9E6D08',
    fontWeight: 'bold',
    fontSize: 12,
  },
  container: {
    flex: 1,
    backgroundColor: '#FBF9F5',
  },
  contentContainer: {
    padding: 16,
    paddingBottom: 40,
    width: '100%',
  },

  // Luxury Gold Card
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
    paddingVertical: 8,
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
    padding: 12,
  },

  // Form Layout
  formRowWrap: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    gap: 20,
  },
  subColumn: {
    flex: 1,
    minWidth: 280,
  },
  fieldRow: {
    flexDirection: 'row',
    alignItems: 'center',
    marginBottom: 8,
  },
  fieldLabel: {
    width: 115,
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3A2E19',
  },
  columnFieldLabel: {
    width: 110,
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3A2E19',
  },
  input: {
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderRadius: 6,
    paddingHorizontal: 8,
    paddingVertical: 3,
    fontSize: 12,
    height: 30,
    backgroundColor: '#FFFFFF',
    color: '#2C2519',
  },
  readonlyInput: {
    backgroundColor: '#F8F5EE',
    borderColor: '#EADBCA',
    color: '#554736',
  },
  inputWithAction: {
    flex: 1,
    flexDirection: 'row',
    alignItems: 'center',
  },
  lookupBtn: {
    width: 32,
    height: 30,
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderLeftWidth: 0,
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
  splitBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#F9F2E2',
    borderWidth: 1,
    borderColor: '#B8903B',
    paddingVertical: 5,
    paddingHorizontal: 12,
    borderRadius: 6,
    marginLeft: 8,
    height: 30,
  },
  splitBtnText: {
    color: '#9E6D08',
    fontWeight: 'bold',
    fontSize: 11,
  },

  // Split Columns
  columnsContainer: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    gap: 14,
    marginTop: 14,
    width: '100%',
  },
  splitColumnCard: {
    flex: 1,
    minWidth: 320,
  },
  scrapBadge: {
    marginLeft: 'auto',
    backgroundColor: '#FFEBEE',
    borderWidth: 1,
    borderColor: '#FFCDD2',
    borderRadius: 4,
    paddingHorizontal: 6,
    paddingVertical: 2,
  },
  scrapBadgeText: {
    fontSize: 10,
    fontWeight: 'bold',
    color: '#C62828',
  },

  // Checkbox
  checkboxContainer: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  checkboxSquare: {
    width: 16,
    height: 16,
    borderWidth: 1.5,
    borderColor: '#B8903B',
    borderRadius: 3,
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: '#FFFFFF',
    marginRight: 6,
  },
  checkboxSquareChecked: {
    backgroundColor: '#B8903B',
  },
  checkboxLabel: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#362B1A',
  },

  // Additional Description
  editAddlBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#E2D2AF',
    paddingVertical: 3,
    paddingHorizontal: 8,
    borderRadius: 4,
  },
  editAddlBtnText: {
    color: '#9E6D08',
    fontSize: 11,
    fontWeight: 'bold',
  },
  addlTableHeader: {
    flexDirection: 'row',
    backgroundColor: '#F5E7CA',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    paddingVertical: 6,
    paddingHorizontal: 8,
    borderRadius: 4,
    marginBottom: 8,
  },
  addlHeaderText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3B2D17',
  },
  addlTableRow: {
    flexDirection: 'row',
    alignItems: 'center',
    marginBottom: 8,
  },

  // Modal
  modalOverlay: {
    flex: 1,
    backgroundColor: 'rgba(0,0,0,0.45)',
    justifyContent: 'center',
    alignItems: 'center',
  },
  modalContainer: {
    width: '85%',
    maxWidth: 540,
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#E8DCB8',
    borderRadius: 8,
    overflow: 'hidden',
    shadowColor: '#A38035',
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.15,
    shadowRadius: 10,
    elevation: 5,
  },
  modalHeader: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    backgroundColor: '#F5E7CA',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    paddingVertical: 9,
    paddingHorizontal: 12,
  },
  modalTitle: {
    fontSize: 14,
    fontWeight: 'bold',
    color: '#3B2D17',
  },
  modalFooter: {
    padding: 10,
    borderTopWidth: 1,
    borderTopColor: '#EADBCA',
    flexDirection: 'row',
    justifyContent: 'flex-end',
    backgroundColor: '#FAF7F0',
  },
  modalCloseBtn: {
    paddingVertical: 6,
    paddingHorizontal: 14,
    borderRadius: 5,
    borderWidth: 1,
    borderColor: '#DFD5C3',
    backgroundColor: '#FFFFFF',
  },
  modalCloseBtnText: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#5C4A28',
  },
  modalSaveBtn: {
    paddingVertical: 6,
    paddingHorizontal: 16,
    borderRadius: 5,
    borderWidth: 1,
    borderColor: '#B8903B',
    backgroundColor: '#F9F2E2',
  },
  modalSaveBtnText: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#9E6D08',
  },

  // LOV items
  lovHeaderRow: {
    flexDirection: 'row',
    backgroundColor: '#F5E7CA',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    paddingVertical: 6,
    paddingHorizontal: 8,
    borderRadius: 4,
    marginBottom: 4,
  },
  lovHeaderText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3B2D17',
  },
  lovItem: {
    flexDirection: 'row',
    paddingVertical: 8,
    paddingHorizontal: 8,
    borderBottomWidth: 1,
    borderBottomColor: '#F0E6D2',
    backgroundColor: '#FFFFFF',
  },
  lovItemText: {
    fontSize: 12,
    color: '#362B1A',
  },

  // Toast & Loader
  toast: {
    position: 'absolute',
    top: 50,
    alignSelf: 'center',
    paddingHorizontal: 20,
    paddingVertical: 10,
    borderRadius: 6,
    elevation: 6,
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.25,
    shadowRadius: 3.84,
    zIndex: 9999,
  },
  toastText: {
    fontSize: 13,
    fontWeight: 'bold',
    color: '#FFFFFF',
  },
  loaderOverlay: {
    ...StyleSheet.absoluteFillObject,
    backgroundColor: 'rgba(0,0,0,0.45)',
    justifyContent: 'center',
    alignItems: 'center',
    zIndex: 99999,
  },
});



