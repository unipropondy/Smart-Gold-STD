import { ThemedText } from '@/components/themed-text';
import { API_BASE_URL } from '@/constants/api';
import { useThemeContext } from '@/contexts/ThemeContext';
import { useThemeColors } from '@/hooks/useThemeColors';
import { Ionicons } from '@expo/vector-icons';
import { Stack, useRouter } from 'expo-router';
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
  TouchableWithoutFeedback,
  View
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';

const API_URL = `${API_BASE_URL}/mergeinventory`;

export default function MergeInventoryScreen() {
  const themeColors = useThemeColors();
  const { accentColor } = useThemeContext();
  const router = useRouter();

  // ===== STATE =====
  const [loading, setLoading] = useState(false);
  const [toast, setToast] = useState<{ message: string, type: 'success' | 'error' } | null>(null);

  // Panel 1: First Item State
  const [item1Code, setItem1Code] = useState('');
  const [item1Details, setItem1Details] = useState<any>(null);

  // Panel 2: Second Item State
  const [item2Code, setItem2Code] = useState('');
  const [item2Details, setItem2Details] = useState<any>(null);
  const [isMergeWeightOnly, setIsMergeWeightOnly] = useState(false);
  const [mergeWeight, setMergeWeight] = useState('');

  // Panel 3: Merged Item state
  const [newCategoryCode, setNewCategoryCode] = useState('');
  const [newCatalogueCode, setNewCatalogueCode] = useState('');
  const [newInventoryCode, setNewInventoryCode] = useState('');
  const [newDescription, setNewDescription] = useState('');
  const [newDiamondDescription, setNewDiamondDescription] = useState('');
  const [newSizeCode, setNewSizeCode] = useState('');
  const [newSizeType, setNewSizeType] = useState('');

  // Values
  const [mergedWeight, setMergedWeight] = useState('0.00');
  const [mergedStoneWeight, setMergedStoneWeight] = useState('0.00');
  const [mergedPMC, setMergedPMC] = useState('0.00');
  const [mergedSMC, setMergedSMC] = useState('0.00');
  const [mergedGoldLoss, setMergedGoldLoss] = useState('0.00');
  const [mergedCostPerGram, setMergedCostPerGram] = useState('0.00');
  const [mergedPCS, setMergedPCS] = useState('1');
  const [mergedTotalCost, setMergedTotalCost] = useState('0.00');
  const [remarks, setRemarks] = useState('');

  // Lists of Values (dropdowns data)
  const [categories, setCategories] = useState<any[]>([]);
  const [catalogues, setCatalogues] = useState<any[]>([]);

  // Additional Description Switch & Modal state
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

  // Shared LOV state
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

  // Helper safe string trim
  const safeTrim = (str: any) => (str ? String(str).trim() : '');

  // ===== DATA FETCH FUNCTIONS =====

  // Load details of Item 1
  const handleFetchItem1 = async () => {
    if (!item1Code.trim()) return;
    setLoading(true);
    try {
      const res = await fetch(`${API_URL}/detail?code=${item1Code.trim()}`);
      const json = await res.json();
      if (json.success) {
        setItem1Details(json.data);
        showToast("Item 1 Details Loaded.", "success");

        // Load additional details if present
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

        // Fetch categories matching the class of Item 1
        const catRes = await fetch(`${API_URL}/categories?classCode=${safeTrim(json.data.ClassCode)}`);
        const catJson = await catRes.json();
        if (catJson.success) {
          setCategories(catJson.data);
        }
      } else {
        showToast(json.message || "Failed to load Item 1 details.", "error");
        handleClearAll();
      }
    } catch (e) {
      showToast("Server connection failed.", "error");
    } finally {
      setLoading(false);
    }
  };

  // Load details of Item 2
  const handleFetchItem2 = async () => {
    if (!item2Code.trim()) return;
    if (safeTrim(item1Code).toLowerCase() === safeTrim(item2Code).toLowerCase()) {
      showToast("Same inventory cannot be merged!", "error");
      setItem2Code('');
      return;
    }
    setLoading(true);
    try {
      const res = await fetch(`${API_URL}/detail?code=${item2Code.trim()}`);
      const json = await res.json();
      if (json.success) {
        setItem2Details(json.data);
        showToast("Item 2 Details Loaded.", "success");
      } else {
        showToast(json.message || "Failed to load Item 2 details.", "error");
        setItem2Code('');
        setItem2Details(null);
      }
    } catch (e) {
      showToast("Server connection failed.", "error");
    } finally {
      setLoading(false);
    }
  };

  // Run Calculations on states change
  useEffect(() => {
    if (!item1Details) return;

    const w1 = parseFloat(item1Details.Weight) || 0;
    const gl1 = parseFloat(item1Details.GoldLoss) || 0;
    const tc1 = parseFloat(item1Details.TotalCost) || 0;
    const st1 = parseFloat(item1Details.StoneWeight) || 0;
    const pmc1 = parseFloat(item1Details.PMC) || 0;
    const smc1 = parseFloat(item1Details.sMc) || 0;
    const cpg1 = parseFloat(item1Details.CostperGram) || 0;
    const pcs1 = parseFloat(item1Details.PCS) || 1;

    if (isMergeWeightOnly) {
      const mw = parseFloat(mergeWeight) || 0;
      const finalWeight = w1 + mw;
      setMergedWeight(finalWeight.toFixed(3));
      setMergedGoldLoss(gl1.toFixed(3));
      setMergedTotalCost(tc1.toFixed(2));
      setMergedStoneWeight(st1.toFixed(3));
      setMergedPCS(String(pcs1));
      setMergedCostPerGram(cpg1.toFixed(2));
      setMergedSMC(smc1.toFixed(2));
      setMergedPMC(pmc1.toFixed(2));
    } else if (item2Details) {
      const w2 = parseFloat(item2Details.Weight) || 0;
      const gl2 = parseFloat(item2Details.GoldLoss) || 0;
      const tc2 = parseFloat(item2Details.TotalCost) || 0;
      const st2 = parseFloat(item2Details.StoneWeight) || 0;
      const pmc2 = parseFloat(item2Details.PMC) || 0;
      const smc2 = parseFloat(item2Details.sMc) || 0;

      const finalWeight = w1 + w2;
      const finalGoldLoss = gl1 + gl2;
      const finalTotalCost = tc1 + tc2;
      const finalStoneWeight = st1 + st2;
      const finalSMC = smc1 + smc2;
      const finalPMC = pmc1 + pmc2;

      setMergedWeight(finalWeight.toFixed(3));
      setMergedGoldLoss(finalGoldLoss.toFixed(3));
      setMergedTotalCost(finalTotalCost.toFixed(2));
      setMergedStoneWeight(finalStoneWeight.toFixed(3));
      setMergedPCS(String(pcs1));
      setMergedCostPerGram(cpg1.toFixed(2));
      setMergedSMC(finalSMC.toFixed(2));
      setMergedPMC(finalPMC.toFixed(2));
    }
  }, [item1Details, item2Details, isMergeWeightOnly, mergeWeight]);

  // Load new codes when Category is selected
  const handleCategorySelect = async (catCode: string) => {
    setNewCategoryCode(catCode);
    setLoading(true);
    try {
      // 1. Fetch next inventory code
      const codeRes = await fetch(`${API_URL}/nextCode?categoryCode=${catCode}`);
      const codeJson = await codeRes.json();
      if (codeJson.success) {
        setNewInventoryCode(codeJson.data);
      }

      // 2. Fetch Catalogues for this category
      const catlgRes = await fetch(`${API_URL}/catalogues?categoryCode=${catCode}`);
      const catlgJson = await catlgRes.json();
      if (catlgJson.success) {
        setCatalogues(catlgJson.data);
        if (catlgJson.data.length > 0) {
          setNewCatalogueCode(catlgJson.data[0].CatalogueCode.trim());
          setNewDescription(catlgJson.data[0].CatalogueName.trim());
        } else {
          setNewCatalogueCode('');
          setNewDescription('');
        }
      }
    } catch (e) {
      console.error("Failed to load details for category selection", e);
    } finally {
      setLoading(false);
    }
  };

  const handleCatalogueSelect = (catlg: any) => {
    setNewCatalogueCode(catlg.CatalogueCode.trim());
    setNewDescription(catlg.CatalogueName.trim());
  };

  const handleCategoryLov = () => {
    if (!item1Details) {
      showToast("First Inventory Code is required!", "error");
      return;
    }
    openLov("Select Category", categories, ["CategoryCode", "CategoryName"], (item) => {
      handleCategorySelect(item.CategoryCode.trim());
      setLovVisible(false);
    });
  };

  const handleCatalogueLov = () => {
    if (!newCategoryCode) {
      showToast("Select Category first.", "error");
      return;
    }
    openLov("Select Catalogue", catalogues, ["CatalogueCode", "CatalogueName"], (item) => {
      handleCatalogueSelect(item);
      setLovVisible(false);
    });
  };

  // Size LOV lookup
  const handleSizeLov = async () => {
    if (!newCategoryCode) {
      showToast("Select Category first.", "error");
      return;
    }
    setLoading(true);
    try {
      const res = await fetch(`${API_URL}/sizes?categoryCode=${newCategoryCode}`);
      const json = await res.json();
      if (json.success) {
        openLov("Select Size", json.data, ["SizeCode", "SizeType"], (item) => {
          setNewSizeCode(String(item.SizeCode));
          setNewSizeType(item.SizeType.trim());
          setLovVisible(false);
        });
      }
    } catch (e) {
      showToast("Failed to load sizes.", "error");
    } finally {
      setLoading(false);
    }
  };

  // Clear Form State
  const handleClearAll = () => {
    setItem1Code('');
    setItem1Details(null);
    setItem2Code('');
    setItem2Details(null);
    setIsMergeWeightOnly(false);
    setMergeWeight('');

    setNewCategoryCode('');
    setNewCatalogueCode('');
    setNewInventoryCode('');
    setNewDescription('');
    setNewDiamondDescription('');
    setNewSizeCode('');
    setNewSizeType('');

    setMergedWeight('0.00');
    setMergedStoneWeight('0.00');
    setMergedPMC('0.00');
    setMergedSMC('0.00');
    setMergedGoldLoss('0.00');
    setMergedCostPerGram('0.00');
    setMergedPCS('1');
    setMergedTotalCost('0.00');
    setRemarks('');

    setCategories([]);
    setCatalogues([]);

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

  // Execute Merge Operation
  const handleSaveMerge = async () => {
    if (!item1Details) {
      showToast("First Inventory Code is required!", "error");
      return;
    }
    if (!isMergeWeightOnly && !item2Details) {
      showToast("Second Inventory Code is required!", "error");
      return;
    }
    if (!newInventoryCode) {
      showToast("New Inventory Code is empty!", "error");
      return;
    }
    if (!newCategoryCode) {
      showToast("Select Category!", "error");
      return;
    }
    if (!mergedSMC.trim()) {
      showToast("Enter SMC value!", "error");
      return;
    }

    setLoading(true);
    try {
      const payload = {
        item1Code: item1Details.InventoryCode,
        item2Code: item2Details ? item2Details.InventoryCode : null,
        isMergeWeightOnly,
        mergeWeight,
        newCategoryCode,
        newCatalogueCode,
        newInventoryCode,
        newDescription,
        newDiamondDescription,
        newSizeCode,
        newSizeType,
        newSMC: mergedSMC,
        remarks,
        item1Details,
        item2Details,
        mergedCalculations: {
          weight: mergedWeight,
          stoneWeight: mergedStoneWeight,
          costPerGram: mergedCostPerGram,
          pmc: mergedPMC,
          smc: mergedSMC,
          pcs: mergedPCS,
          goldLoss: mergedGoldLoss,
          totalCost: mergedTotalCost
        },
        additionalDescription: chkAdditionalDesc ? {
          desc1, desc2, desc3, desc4,
          cost1, cost2, cost3, cost4,
          sp1, sp2, sp3, sp4
        } : null,
        userId: "sys"
      };

      const res = await fetch(`${API_URL}/merge`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(payload)
      });
      const json = await res.json();
      if (json.success) {
        showToast(json.message || "Inventory Merged Successfully!", "success");
        handleClearAll();
      } else {
        showToast(json.message || "Error merging inventory.", "error");
      }
    } catch (e) {
      showToast("Server connection failed.", "error");
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
          <ThemedText style={styles.title}>Merge Inventory</ThemedText>
        </View>
        <View style={{ flexDirection: 'row', alignItems: 'center' }}>
          <TouchableOpacity style={styles.clearBtn} onPress={handleClearAll}>
            <Ionicons name="refresh-outline" size={14} color="#5C4A28" style={{ marginRight: 4 }} />
            <ThemedText style={styles.clearBtnText}>Clear</ThemedText>
          </TouchableOpacity>
          <TouchableOpacity style={styles.updateBtn} onPress={handleSaveMerge}>
            <Ionicons name="git-merge-outline" size={14} color="#9E6D08" style={{ marginRight: 4 }} />
            <ThemedText style={styles.updateBtnText}>Merge</ThemedText>
          </TouchableOpacity>
        </View>
      </View>

      <KeyboardAvoidingView behavior={Platform.OS === 'ios' ? 'padding' : 'height'} style={{ flex: 1 }}>
        <ScrollView style={styles.container} contentContainerStyle={styles.contentContainer}>
          {/* Dual Columns: First Item & Second Item */}
          <View style={styles.columnsContainer}>
            {/* Left Panel: First Item */}
            <View style={[styles.goldCard, styles.splitColumnCard]}>
              <View style={styles.cardHeaderBanner}>
                <View style={styles.headerIconBadge}>
                  <Ionicons name="cube-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitle}>First Item</ThemedText>
              </View>

              <View style={styles.cardBody}>
                {/* Inventory Code */}
                <View style={styles.fieldRow}>
                  <ThemedText style={styles.columnFieldLabel}>Inventory Code</ThemedText>
                  <View style={styles.inputWithAction}>
                    <TextInput
                      style={[styles.input, { flex: 1, borderTopRightRadius: 0, borderBottomRightRadius: 0 }]}
                      value={item1Code}
                      onChangeText={setItem1Code}
                      onBlur={handleFetchItem1}
                      onSubmitEditing={handleFetchItem1}
                      placeholder="Scan / Type Code 1"
                      placeholderTextColor="#A89D8B"
                    />
                    <TouchableOpacity style={styles.lookupBtn} onPress={handleFetchItem1}>
                      <Ionicons name="search" size={13} color="#6E5528" />
                    </TouchableOpacity>
                  </View>
                </View>

                {/* Description */}
                <View style={styles.fieldRow}>
                  <ThemedText style={styles.columnFieldLabel}>Description</ThemedText>
                  <TextInput style={[styles.input, styles.readonlyInput, { flex: 1 }]} value={item1Details ? safeTrim(item1Details.Description) : ''} editable={false} />
                </View>

                {/* Diamond Desc */}
                <View style={styles.fieldRow}>
                  <ThemedText style={styles.columnFieldLabel}>Diamond Desc.</ThemedText>
                  <TextInput style={[styles.input, styles.readonlyInput, { flex: 1 }]} value={item1Details ? safeTrim(item1Details.DiamondDescription) : ''} editable={false} />
                </View>

                {/* Weight */}
                <View style={styles.fieldRow}>
                  <ThemedText style={styles.columnFieldLabel}>Weight</ThemedText>
                  <TextInput style={[styles.input, styles.readonlyInput, { flex: 1, textAlign: 'right', fontWeight: 'bold' }]} value={item1Details ? String(item1Details.Weight) : '0.000'} editable={false} />
                </View>

                {/* Stone Weight */}
                <View style={styles.fieldRow}>
                  <ThemedText style={styles.columnFieldLabel}>Stone Weight</ThemedText>
                  <TextInput style={[styles.input, styles.readonlyInput, { flex: 1, textAlign: 'right' }]} value={item1Details ? String(item1Details.StoneWeight) : '0.000'} editable={false} />
                </View>

                {/* Cost Per Gram */}
                <View style={styles.fieldRow}>
                  <ThemedText style={styles.columnFieldLabel}>Cost / Gram</ThemedText>
                  <TextInput style={[styles.input, styles.readonlyInput, { flex: 1, textAlign: 'right' }]} value={item1Details ? String(item1Details.CostperGram) : '0.00'} editable={false} />
                </View>

                {/* PCS */}
                <View style={styles.fieldRow}>
                  <ThemedText style={styles.columnFieldLabel}>PCS</ThemedText>
                  <TextInput style={[styles.input, styles.readonlyInput, { flex: 1, textAlign: 'right' }]} value={item1Details ? String(item1Details.PCS) : '0'} editable={false} />
                </View>

                {/* PMC */}
                <View style={styles.fieldRow}>
                  <ThemedText style={styles.columnFieldLabel}>PMC</ThemedText>
                  <TextInput style={[styles.input, styles.readonlyInput, { flex: 1, textAlign: 'right' }]} value={item1Details ? String(item1Details.PMC) : '0.00'} editable={false} />
                </View>

                {/* SMC */}
                <View style={styles.fieldRow}>
                  <ThemedText style={styles.columnFieldLabel}>SMC</ThemedText>
                  <TextInput style={[styles.input, styles.readonlyInput, { flex: 1, textAlign: 'right' }]} value={item1Details ? String(item1Details.sMc) : '0.00'} editable={false} />
                </View>

                {/* Gold Loss */}
                <View style={styles.fieldRow}>
                  <ThemedText style={styles.columnFieldLabel}>Gold Loss</ThemedText>
                  <TextInput style={[styles.input, styles.readonlyInput, { flex: 1, textAlign: 'right' }]} value={item1Details ? String(item1Details.GoldLoss) : '0.00'} editable={false} />
                </View>

                {/* Total Cost */}
                <View style={styles.fieldRow}>
                  <ThemedText style={styles.columnFieldLabel}>Total Cost</ThemedText>
                  <TextInput style={[styles.input, styles.readonlyInput, { flex: 1, textAlign: 'right', fontWeight: 'bold' }]} value={item1Details ? String(item1Details.TotalCost) : '0.00'} editable={false} />
                </View>

                {/* Size */}
                <View style={styles.fieldRow}>
                  <ThemedText style={styles.columnFieldLabel}>Size Code</ThemedText>
                  <TextInput style={[styles.input, styles.readonlyInput, { flex: 1 }]} value={item1Details ? String(item1Details.SizeCode || '') : ''} editable={false} />
                </View>
              </View>
            </View>

            {/* Right Panel: Second Item */}
            <View style={[styles.goldCard, styles.splitColumnCard]}>
              <View style={[styles.cardHeaderBanner, { justifyContent: 'space-between' }]}>
                <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                  <View style={styles.headerIconBadge}>
                    <Ionicons name="cube-outline" size={13} color="#B48218" />
                  </View>
                  <ThemedText style={styles.cardHeaderTitle}>Second Item</ThemedText>
                </View>
                <TouchableOpacity
                  onPress={() => {
                    setIsMergeWeightOnly(!isMergeWeightOnly);
                    setItem2Code('');
                    setItem2Details(null);
                  }}
                  style={styles.checkboxContainer}
                >
                  <View style={[styles.checkboxSquare, isMergeWeightOnly && styles.checkboxSquareChecked]}>
                    {isMergeWeightOnly && <Ionicons name="checkmark" size={12} color="#FFF" />}
                  </View>
                  <ThemedText style={styles.checkboxLabel}>Weight Only</ThemedText>
                </TouchableOpacity>
              </View>

              <View style={styles.cardBody}>
                {isMergeWeightOnly ? (
                  <View style={{ flex: 1, justifyContent: 'center' }}>
                    <View style={[styles.fieldRow, { marginTop: 16 }]}>
                      <ThemedText style={styles.columnFieldLabel}>Merge Weight</ThemedText>
                      <TextInput
                        style={[styles.input, { flex: 1, textAlign: 'right', fontSize: 13, fontWeight: 'bold' }]}
                        value={mergeWeight}
                        onChangeText={setMergeWeight}
                        keyboardType="numeric"
                        placeholder="0.000"
                        placeholderTextColor="#A89D8B"
                      />
                    </View>
                    <View style={{ paddingVertical: 40, alignItems: 'center' }}>
                      <Ionicons name="scale-outline" size={36} color="#C4B79D" style={{ marginBottom: 8 }} />
                      <ThemedText style={{ fontSize: 12, color: '#8C7A5C', textAlign: 'center', maxWidth: 280 }}>
                        Manual weight-only merge active. Enter extra gold weight to combine with the first item.
                      </ThemedText>
                    </View>
                  </View>
                ) : (
                  <View style={{ flex: 1 }}>
                    {/* Inventory Code */}
                    <View style={styles.fieldRow}>
                      <ThemedText style={styles.columnFieldLabel}>Inventory Code</ThemedText>
                      <View style={styles.inputWithAction}>
                        <TextInput
                          style={[styles.input, { flex: 1, borderTopRightRadius: 0, borderBottomRightRadius: 0 }]}
                          value={item2Code}
                          onChangeText={setItem2Code}
                          onBlur={handleFetchItem2}
                          onSubmitEditing={handleFetchItem2}
                          placeholder="Scan / Type Code 2"
                          placeholderTextColor="#A89D8B"
                        />
                        <TouchableOpacity style={styles.lookupBtn} onPress={handleFetchItem2}>
                          <Ionicons name="search" size={13} color="#6E5528" />
                        </TouchableOpacity>
                      </View>
                    </View>

                    {/* Description */}
                    <View style={styles.fieldRow}>
                      <ThemedText style={styles.columnFieldLabel}>Description</ThemedText>
                      <TextInput style={[styles.input, styles.readonlyInput, { flex: 1 }]} value={item2Details ? safeTrim(item2Details.Description) : ''} editable={false} />
                    </View>

                    {/* Diamond Desc */}
                    <View style={styles.fieldRow}>
                      <ThemedText style={styles.columnFieldLabel}>Diamond Desc.</ThemedText>
                      <TextInput style={[styles.input, styles.readonlyInput, { flex: 1 }]} value={item2Details ? safeTrim(item2Details.DiamondDescription) : ''} editable={false} />
                    </View>

                    {/* Weight */}
                    <View style={styles.fieldRow}>
                      <ThemedText style={styles.columnFieldLabel}>Weight</ThemedText>
                      <TextInput style={[styles.input, styles.readonlyInput, { flex: 1, textAlign: 'right', fontWeight: 'bold' }]} value={item2Details ? String(item2Details.Weight) : '0.000'} editable={false} />
                    </View>

                    {/* Stone Weight */}
                    <View style={styles.fieldRow}>
                      <ThemedText style={styles.columnFieldLabel}>Stone Weight</ThemedText>
                      <TextInput style={[styles.input, styles.readonlyInput, { flex: 1, textAlign: 'right' }]} value={item2Details ? String(item2Details.StoneWeight) : '0.000'} editable={false} />
                    </View>

                    {/* Cost Per Gram */}
                    <View style={styles.fieldRow}>
                      <ThemedText style={styles.columnFieldLabel}>Cost / Gram</ThemedText>
                      <TextInput style={[styles.input, styles.readonlyInput, { flex: 1, textAlign: 'right' }]} value={item2Details ? String(item2Details.CostperGram) : '0.00'} editable={false} />
                    </View>

                    {/* PCS */}
                    <View style={styles.fieldRow}>
                      <ThemedText style={styles.columnFieldLabel}>PCS</ThemedText>
                      <TextInput style={[styles.input, styles.readonlyInput, { flex: 1, textAlign: 'right' }]} value={item2Details ? String(item2Details.PCS) : '0'} editable={false} />
                    </View>

                    {/* PMC */}
                    <View style={styles.fieldRow}>
                      <ThemedText style={styles.columnFieldLabel}>PMC</ThemedText>
                      <TextInput style={[styles.input, styles.readonlyInput, { flex: 1, textAlign: 'right' }]} value={item2Details ? String(item2Details.PMC) : '0.00'} editable={false} />
                    </View>

                    {/* SMC */}
                    <View style={styles.fieldRow}>
                      <ThemedText style={styles.columnFieldLabel}>SMC</ThemedText>
                      <TextInput style={[styles.input, styles.readonlyInput, { flex: 1, textAlign: 'right' }]} value={item2Details ? String(item2Details.sMc) : '0.00'} editable={false} />
                    </View>

                    {/* Gold Loss */}
                    <View style={styles.fieldRow}>
                      <ThemedText style={styles.columnFieldLabel}>Gold Loss</ThemedText>
                      <TextInput style={[styles.input, styles.readonlyInput, { flex: 1, textAlign: 'right' }]} value={item2Details ? String(item2Details.GoldLoss) : '0.00'} editable={false} />
                    </View>

                    {/* Total Cost */}
                    <View style={styles.fieldRow}>
                      <ThemedText style={styles.columnFieldLabel}>Total Cost</ThemedText>
                      <TextInput style={[styles.input, styles.readonlyInput, { flex: 1, textAlign: 'right', fontWeight: 'bold' }]} value={item2Details ? String(item2Details.TotalCost) : '0.00'} editable={false} />
                    </View>

                    {/* Size */}
                    <View style={styles.fieldRow}>
                      <ThemedText style={styles.columnFieldLabel}>Size Code</ThemedText>
                      <TextInput style={[styles.input, styles.readonlyInput, { flex: 1 }]} value={item2Details ? String(item2Details.SizeCode || '') : ''} editable={false} />
                    </View>
                  </View>
                )}
              </View>
            </View>
          </View>

          {/* Panel 3: Merged Item Section */}
          <View style={[styles.goldCard, { marginTop: 14 }]}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerIconBadge}>
                <Ionicons name="sparkles" size={13} color="#B48218" />
              </View>
              <ThemedText style={styles.cardHeaderTitle}>Merged Item Details & Outputs</ThemedText>
            </View>

            <View style={styles.cardBody}>
              <View style={styles.formRowWrap}>
                {/* Left Column: Classification & Identity */}
                <View style={styles.subColumn}>
                  {/* Category */}
                  <View style={styles.fieldRow}>
                    <ThemedText style={styles.fieldLabel}>Category</ThemedText>
                    <View style={styles.inputWithAction}>
                      <TextInput style={[styles.input, styles.readonlyInput, { flex: 1, borderTopRightRadius: 0, borderBottomRightRadius: 0 }]} value={newCategoryCode} editable={false} />
                      <TouchableOpacity style={styles.lookupBtn} onPress={handleCategoryLov}>
                        <ThemedText style={styles.lookupBtnText}>...</ThemedText>
                      </TouchableOpacity>
                    </View>
                  </View>

                  {/* Catalogue */}
                  <View style={styles.fieldRow}>
                    <ThemedText style={styles.fieldLabel}>Catalogue</ThemedText>
                    <View style={styles.inputWithAction}>
                      <TextInput style={[styles.input, styles.readonlyInput, { flex: 1, borderTopRightRadius: 0, borderBottomRightRadius: 0 }]} value={newCatalogueCode} editable={false} />
                      <TouchableOpacity style={styles.lookupBtn} onPress={handleCatalogueLov}>
                        <ThemedText style={styles.lookupBtnText}>...</ThemedText>
                      </TouchableOpacity>
                    </View>
                  </View>

                  {/* Inventory Code */}
                  <View style={styles.fieldRow}>
                    <ThemedText style={styles.fieldLabel}>New Inv Code</ThemedText>
                    <TextInput style={[styles.input, styles.readonlyInput, { flex: 1, fontWeight: 'bold' }]} value={newInventoryCode} editable={false} />
                  </View>

                  {/* Description */}
                  <View style={styles.fieldRow}>
                    <ThemedText style={styles.fieldLabel}>Description</ThemedText>
                    <TextInput style={[styles.input, { flex: 1 }]} value={newDescription} onChangeText={setNewDescription} placeholder="Merged description" placeholderTextColor="#A89D8B" />
                  </View>

                  {/* Diamond Desc */}
                  <View style={styles.fieldRow}>
                    <ThemedText style={styles.fieldLabel}>Diamond Desc.</ThemedText>
                    <TextInput style={[styles.input, { flex: 1 }]} value={newDiamondDescription} onChangeText={setNewDiamondDescription} placeholder="Diamond info" placeholderTextColor="#A89D8B" />
                  </View>

                  {/* Size */}
                  <View style={styles.fieldRow}>
                    <ThemedText style={styles.fieldLabel}>Size Code</ThemedText>
                    <View style={styles.inputWithAction}>
                      <TextInput style={[styles.input, styles.readonlyInput, { flex: 1, borderTopRightRadius: 0, borderBottomRightRadius: 0 }]} value={newSizeCode} editable={false} />
                      <TouchableOpacity style={styles.lookupBtn} onPress={handleSizeLov}>
                        <ThemedText style={styles.lookupBtnText}>...</ThemedText>
                      </TouchableOpacity>
                    </View>
                  </View>
                </View>

                {/* Right Column: Values & Costing */}
                <View style={styles.subColumn}>
                  {/* Weight */}
                  <View style={styles.fieldRow}>
                    <ThemedText style={styles.fieldLabel}>Total Weight</ThemedText>
                    <TextInput style={[styles.input, styles.readonlyInput, { flex: 1, textAlign: 'right', fontWeight: 'bold', color: '#9E6D08' }]} value={mergedWeight} editable={false} />
                  </View>

                  {/* Cost Per Gram */}
                  <View style={styles.fieldRow}>
                    <ThemedText style={styles.fieldLabel}>Cost / Gram</ThemedText>
                    <TextInput style={[styles.input, styles.readonlyInput, { flex: 1, textAlign: 'right' }]} value={mergedCostPerGram} editable={false} />
                  </View>

                  {/* Gold Loss */}
                  <View style={styles.fieldRow}>
                    <ThemedText style={styles.fieldLabel}>Gold Loss</ThemedText>
                    <TextInput style={[styles.input, styles.readonlyInput, { flex: 1, textAlign: 'right' }]} value={mergedGoldLoss} editable={false} />
                  </View>

                  {/* Stone Wt */}
                  <View style={styles.fieldRow}>
                    <ThemedText style={styles.fieldLabel}>Stone Wt.</ThemedText>
                    <TextInput style={[styles.input, styles.readonlyInput, { flex: 1, textAlign: 'right' }]} value={mergedStoneWeight} editable={false} />
                  </View>

                  {/* PCS */}
                  <View style={styles.fieldRow}>
                    <ThemedText style={styles.fieldLabel}>Total PCS</ThemedText>
                    <TextInput style={[styles.input, styles.readonlyInput, { flex: 1, textAlign: 'right' }]} value={mergedPCS} editable={false} />
                  </View>

                  {/* PMC */}
                  <View style={styles.fieldRow}>
                    <ThemedText style={styles.fieldLabel}>PMC</ThemedText>
                    <TextInput style={[styles.input, styles.readonlyInput, { flex: 1, textAlign: 'right' }]} value={mergedPMC} editable={false} />
                  </View>

                  {/* SMC */}
                  <View style={styles.fieldRow}>
                    <ThemedText style={styles.fieldLabel}>SMC</ThemedText>
                    <TextInput style={[styles.input, { flex: 1, textAlign: 'right' }]} value={mergedSMC} onChangeText={setMergedSMC} keyboardType="numeric" />
                  </View>

                  {/* Total Cost */}
                  <View style={styles.fieldRow}>
                    <ThemedText style={styles.fieldLabel}>Total Cost</ThemedText>
                    <TextInput style={[styles.input, styles.readonlyInput, { flex: 1, textAlign: 'right', fontWeight: 'bold' }]} value={mergedTotalCost} editable={false} />
                  </View>

                  {/* Remarks */}
                  <View style={styles.fieldRow}>
                    <ThemedText style={styles.fieldLabel}>Remarks</ThemedText>
                    <TextInput style={[styles.input, { flex: 1 }]} value={remarks} onChangeText={setRemarks} placeholder="Optional notes" placeholderTextColor="#A89D8B" />
                  </View>
                </View>
              </View>
            </View>
          </View>

          {/* Additional Description Trigger Card */}
          <View style={[styles.goldCard, { marginTop: 14 }]}>
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

  // Split Columns
  columnsContainer: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    gap: 14,
    width: '100%',
  },
  splitColumnCard: {
    flex: 1,
    minWidth: 320,
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



