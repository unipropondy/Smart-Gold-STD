import { ThemedText } from '@/components/themed-text';
import { API_BASE_URL } from '@/constants/api';
import { useThemeColors } from '@/hooks/useThemeColors';
import { useThemeContext } from '@/contexts/ThemeContext';
import { Ionicons } from '@expo/vector-icons';
import { Stack, useRouter } from 'expo-router';
import React, { useEffect, useMemo, useState } from 'react';
import {
  ActivityIndicator,
  FlatList,
  Modal,
  ScrollView,
  StyleSheet,
  TextInput,
  TouchableOpacity,
  TouchableWithoutFeedback,
  View,
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';

export default function InventoryMaintenanceScreen() {
  const themeColors = useThemeColors();
  const { accentColor } = useThemeContext();
  const router = useRouter();

  const [loading, setLoading] = useState(false);
  const [saving, setSaving] = useState(false);
  const [toastMsg, setToastMsg] = useState({ title: '', message: '', visible: false });

  // Search Fields
  const [fromInventory, setFromInventory] = useState('');
  const [toInventory, setToInventory] = useState('');
  const [supplierCode, setSupplierCode] = useState('');
  const [purchaseBillNo, setPurchaseBillNo] = useState('');

  // Old Data
  const [oldData, setOldData] = useState({
    Description: '', DiamondDescription: '', Weight: '0.00', GoldLoss: '0.00', CostPerGram: '0.00',
    PMC: '0.00', SMC: '0.00', PCS: '0', SizeCode: '', TotalCost: '0.00'
  });

  // Editable Data
  const [editableData, setEditableData] = useState({
    Description: '', DiamondDescription: '', Weight: '0.00', GoldLoss: '0.00', CostPerGram: '0.00',
    PMC: '0.00', SMC: '0.00', PCS: '0', SizeCode: '', TotalCost: '0.00'
  });

  // Additional Descriptions
  const [addlData, setAddlData] = useState({
    Description1: '', Description2: '', Description3: '', Description4: '',
    Cost1: '0.00', Cost2: '0.00', Cost3: '0.00', Cost4: '0.00',
    SellingPrice1: '0.00', SellingPrice2: '0.00', SellingPrice3: '0.00', SellingPrice4: '0.00'
  });

  const [pmcType, setPmcType] = useState<'W' | 'P'>('W');
  const [smcType, setSmcType] = useState<'W' | 'P'>('W');
  const [showAddlDesc, setShowAddlDesc] = useState(false);

  // LOVs
  const [lovVisible, setLovVisible] = useState(false);
  const [lovType, setLovType] = useState<'fromInventory' | 'toInventory' | 'size'>('fromInventory');
  const [lovData, setLovData] = useState<any[]>([]);

  // Current working list of inventories fetched
  const [fetchedInventoryList, setFetchedInventoryList] = useState<any[]>([]);

  const showToast = (title: string, msg: string) => {
    setToastMsg({ title, message: msg, visible: true });
    setTimeout(() => {
      setToastMsg(prev => ({ ...prev, visible: false }));
    }, 3000);
  };

  const showAlert = (title: string, msg: string) => {
    showToast(title, msg);
  };

  const fetchLOV = async (type: 'fromInventory' | 'toInventory' | 'size') => {
    setLovType(type);
    setLovVisible(true);
    setLoading(true);
    try {
      let url = `${API_BASE_URL}/inventorymaintenance/lov/inventory`;
      if (type === 'size') {
        url = `${API_BASE_URL}/inventorymaintenance/lov/size`;
      }
      const res = await fetch(url);
      const json = await res.json();
      if (json.success) setLovData(json.data);
      else showAlert('Error', 'Failed to load LOV');
    } catch (e) {
      console.error(e);
      showAlert('Error', 'Failed to load LOV');
    } finally {
      setLoading(false);
    }
  };

  const handleSelectLOV = (item: any) => {
    if (lovType === 'fromInventory') {
      const code = item.InventoryCode ? String(item.InventoryCode).trim() : '';
      setFromInventory(code);
      setToInventory(code);
      fetchInventoryRange(code, code, '', '');
    } else if (lovType === 'toInventory') {
      const code = item.InventoryCode ? String(item.InventoryCode).trim() : '';
      setToInventory(code);
    } else if (lovType === 'size') {
      const sizeCode = item.SizeCode ? String(item.SizeCode).trim() : '';
      setEditableData(prev => ({ ...prev, SizeCode: sizeCode }));
    }
    setLovVisible(false);
  };

  const fetchInventoryRange = async (from: string, to: string, supplier: string = supplierCode, billNo: string = purchaseBillNo) => {
    let fromTrimmed = from ? from.trim() : '';
    let toTrimmed = to ? to.trim() : '';

    if (!fromTrimmed) {
      showAlert('Error', 'Invalid Inventory');
      return;
    }
    if (!toTrimmed || fromTrimmed > toTrimmed) {
      toTrimmed = fromTrimmed;
      setToInventory(toTrimmed);
    }

    setLoading(true);
    try {
      const q = new URLSearchParams({ fromInventory: fromTrimmed, toInventory: toTrimmed });
      if (supplier) q.append('supplierCode', supplier);
      if (billNo) q.append('purchaseBillNo', billNo);

      const res = await fetch(`${API_BASE_URL}/inventorymaintenance/range?${q.toString()}`);
      const json = await res.json();

      if (json.success && json.data && json.data.length > 0) {
        setFetchedInventoryList(json.data);
        const first = json.data[0];

        // Populate Old Data
        setOldData({
          Description: first.Description || '',
          DiamondDescription: first.DiamondDescription || '',
          Weight: (first.Weight || 0).toFixed(2),
          GoldLoss: (first.GoldLoss || 0).toFixed(2),
          CostPerGram: (first.CostperGram || 0).toFixed(2),
          PMC: (first.PMC || 0).toFixed(2),
          SMC: (first.sMc || 0).toFixed(2),
          PCS: String(first.Pcs || 0),
          SizeCode: first.SizeCode || '',
          TotalCost: (first.TotalCost || 0).toFixed(2)
        });

        // Populate Editable Data
        setEditableData({
          Description: first.Description || '',
          DiamondDescription: first.DiamondDescription || '',
          Weight: (first.Weight || 0).toFixed(2),
          GoldLoss: (first.GoldLoss || 0).toFixed(2),
          CostPerGram: (first.CostperGram || 0).toFixed(2),
          PMC: (first.PMC || 0).toFixed(2),
          SMC: (first.sMc || 0).toFixed(2),
          PCS: String(first.Pcs || 0),
          SizeCode: first.SizeCode || '',
          TotalCost: (first.TotalCost || 0).toFixed(2)
        });

        setPmcType(first.PMCType === 'P' ? 'P' : 'W');
        setSmcType(first.SMCType === 'P' ? 'P' : 'W');

        setSupplierCode(first.SupplierCode || '');
        setPurchaseBillNo(first.PurchaseBillNo || '');

        setAddlData({
          Description1: first.Description1 || '', Description2: first.Description2 || '',
          Description3: first.Description3 || '', Description4: first.Description4 || '',
          Cost1: (first.Cost1 || 0).toFixed(2), Cost2: (first.Cost2 || 0).toFixed(2),
          Cost3: (first.Cost3 || 0).toFixed(2), Cost4: (first.Cost4 || 0).toFixed(2),
          SellingPrice1: (first.SellingPrice1 || 0).toFixed(2), SellingPrice2: (first.SellingPrice2 || 0).toFixed(2),
          SellingPrice3: (first.SellingPrice3 || 0).toFixed(2), SellingPrice4: (first.SellingPrice4 || 0).toFixed(2)
        });

      } else {
        showAlert('Info', 'No items matching this range.');
        handleClear(false);
      }
    } catch (e) {
      console.error(e);
      showAlert('Error', 'Failed to fetch inventory');
    } finally {
      setLoading(false);
    }
  };

  const handleClear = (clearSearchFields = true) => {
    if (clearSearchFields) {
      setFromInventory(''); setToInventory(''); setSupplierCode(''); setPurchaseBillNo('');
    }
    const empty = {
      Description: '', DiamondDescription: '', Weight: '0.00', GoldLoss: '0.00', CostPerGram: '0.00',
      PMC: '0.00', SMC: '0.00', PCS: '0', SizeCode: '', TotalCost: '0.00'
    };
    setOldData(empty); setEditableData(empty);
    setFetchedInventoryList([]);
    setPmcType('W'); setSmcType('W');
  };

  const handleSave = async () => {
    if (fetchedInventoryList.length === 0) {
      showAlert('Info', 'No items matching.');
      return;
    }

    // Check mandatory fields
    if (!editableData.Description) {
      showAlert('Validation Error', 'Description is mandatory.');
      return;
    }
    if (parseFloat(editableData.PMC) < 0 || isNaN(parseFloat(editableData.PMC))) {
      showAlert('Validation Error', 'Invalid Making cost!');
      return;
    }
    if (parseFloat(editableData.SMC) < 0 || isNaN(parseFloat(editableData.SMC))) {
      showAlert('Validation Error', 'Invalid sales making cost!');
      return;
    }

    setSaving(true);
    try {
      const payload = {
        inventoryList: fetchedInventoryList.map(i => ({ InventoryCode: i.InventoryCode })),
        commonUpdates: {
          ...editableData,
          PMC: parseFloat(editableData.PMC) || 0,
          SMC: parseFloat(editableData.SMC) || 0,
          TotalCost: parseFloat(editableData.TotalCost) || 0,
          GoldLoss: parseFloat(editableData.GoldLoss) || 0,
          PMCType: pmcType,
          SMCType: smcType,
        },
        addlUpdates: {
          ...addlData,
          Cost1: parseFloat(addlData.Cost1) || 0, Cost2: parseFloat(addlData.Cost2) || 0, Cost3: parseFloat(addlData.Cost3) || 0, Cost4: parseFloat(addlData.Cost4) || 0,
          SellingPrice1: parseFloat(addlData.SellingPrice1) || 0, SellingPrice2: parseFloat(addlData.SellingPrice2) || 0, SellingPrice3: parseFloat(addlData.SellingPrice3) || 0, SellingPrice4: parseFloat(addlData.SellingPrice4) || 0,
        }
      };

      const res = await fetch(`${API_BASE_URL}/inventorymaintenance/save`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload)
      });
      const json = await res.json();
      if (json.success) {
        showAlert('Success', 'Inventories saved successfully!');
        handleClear();
      } else {
        showAlert('Error', json.message || 'Failed to save');
      }
    } catch (e) {
      console.error(e);
      showAlert('Error', 'Exception while saving');
    } finally {
      setSaving(false);
    }
  };

  useEffect(() => {
    // Recalculate Total Cost based on PMC/Weight
    let weight = parseFloat(editableData.Weight) || 0;
    let pcs = parseFloat(editableData.PCS) || 0;
    let costPerGram = parseFloat(editableData.CostPerGram) || 0;
    let pmc = parseFloat(editableData.PMC) || 0;

    let total = (weight * costPerGram);
    if (pmcType === 'W') {
      total += (pmc * weight);
    } else {
      total += (pmc * pcs);
    }

    setEditableData(prev => prev.TotalCost !== total.toFixed(2) ? { ...prev, TotalCost: total.toFixed(2) } : prev);
  }, [editableData.Weight, editableData.PCS, editableData.CostPerGram, editableData.PMC, pmcType]);

  const totalPmc = useMemo(() => {
    let weight = parseFloat(editableData.Weight) || 0;
    let pcs = parseFloat(editableData.PCS) || 0;
    let pmc = parseFloat(editableData.PMC) || 0;
    return pmcType === 'W' ? (pmc * weight).toFixed(2) : (pmc * pcs).toFixed(2);
  }, [editableData.Weight, editableData.PCS, editableData.PMC, pmcType]);

  const totalSmc = useMemo(() => {
    let weight = parseFloat(editableData.Weight) || 0;
    let pcs = parseFloat(editableData.PCS) || 0;
    let smc = parseFloat(editableData.SMC) || 0;
    return smcType === 'W' ? (smc * weight).toFixed(2) : (smc * pcs).toFixed(2);
  }, [editableData.Weight, editableData.PCS, editableData.SMC, smcType]);

  const renderRadio = (label: string, selected: boolean, onPress: () => void) => (
    <TouchableOpacity style={styles.radioBtn} onPress={onPress}>
      <View style={[styles.radioCircle, selected && styles.radioCircleActive]}>
        {selected && <View style={styles.radioDot} />}
      </View>
      <ThemedText style={[styles.radioLabel, selected && styles.radioLabelActive]}>{label}</ThemedText>
    </TouchableOpacity>
  );

  return (
    <SafeAreaView style={styles.safeArea} edges={['top']}>
      <Stack.Screen options={{ headerShown: false }} />

      {/* Top App Header Bar */}
      <View style={styles.header}>
        <View style={{ flexDirection: 'row', alignItems: 'center' }}>
          <TouchableOpacity style={styles.backButton} onPress={() => router.back()}>
            <Ionicons name="arrow-back" size={22} color="#3B2D17" />
          </TouchableOpacity>
          <ThemedText style={styles.title}>Inventory Maintenance</ThemedText>
        </View>
        <View style={{ flexDirection: 'row', gap: 8, alignItems: 'center' }}>
          <TouchableOpacity style={styles.clearBtn} onPress={() => handleClear()}>
            <Ionicons name="refresh-outline" size={14} color="#5C4A28" style={{ marginRight: 4 }} />
            <ThemedText style={styles.clearBtnText}>Clear</ThemedText>
          </TouchableOpacity>
          <TouchableOpacity style={styles.saveBtn} onPress={handleSave}>
            <Ionicons name="save-outline" size={14} color="#9E6D08" style={{ marginRight: 4 }} />
            <ThemedText style={styles.saveBtnText}>{saving ? 'Saving...' : 'Save'}</ThemedText>
          </TouchableOpacity>
        </View>
      </View>

      <ScrollView contentContainerStyle={{ flexGrow: 1 }} style={styles.container}>
        {/* Top Range Selection Card */}
        <View style={styles.searchCard}>
          <View style={styles.cardHeaderBanner}>
            <View style={styles.headerIconBadge}>
              <Ionicons name="swap-horizontal" size={13} color="#B48218" />
            </View>
            <ThemedText style={styles.cardHeaderTitle}>Select Inventory Range</ThemedText>
          </View>

          <View style={styles.searchFilterBody}>
            <View style={styles.searchRow}>
              {/* From Inventory */}
              <View style={styles.fieldGroup}>
                <ThemedText style={styles.label}>From Inventory</ThemedText>
                <View style={styles.inputWithBtn}>
                  <TextInput
                    style={[styles.input, { width: 110, borderTopRightRadius: 0, borderBottomRightRadius: 0 }]}
                    value={fromInventory}
                    onChangeText={setFromInventory}
                    onBlur={() => fetchInventoryRange(fromInventory, toInventory)}
                    autoCapitalize="characters"
                  />
                  <TouchableOpacity style={styles.lovBtn} onPress={() => fetchLOV('fromInventory')}>
                    <ThemedText style={styles.lovBtnText}>...</ThemedText>
                  </TouchableOpacity>
                </View>
              </View>

              {/* To Inventory */}
              <View style={styles.fieldGroup}>
                <ThemedText style={styles.label}>To Inventory</ThemedText>
                <View style={styles.inputWithBtn}>
                  <TextInput
                    style={[styles.input, { width: 110, borderTopRightRadius: 0, borderBottomRightRadius: 0 }]}
                    value={toInventory}
                    onChangeText={setToInventory}
                    autoCapitalize="characters"
                  />
                  <TouchableOpacity style={styles.lovBtn} onPress={() => fetchLOV('toInventory')}>
                    <ThemedText style={styles.lovBtnText}>...</ThemedText>
                  </TouchableOpacity>
                </View>
              </View>

              {/* Supplier Code */}
              <View style={styles.fieldGroup}>
                <ThemedText style={styles.label}>Supplier Code</ThemedText>
                <TextInput
                  style={[styles.input, styles.readonlyInput, { width: 110 }]}
                  value={supplierCode}
                  editable={false}
                />
              </View>

              {/* Purchase Bill No */}
              <View style={styles.fieldGroup}>
                <ThemedText style={styles.label}>Purchase Bill No</ThemedText>
                <TextInput
                  style={[styles.input, styles.readonlyInput, { width: 120 }]}
                  value={purchaseBillNo}
                  editable={false}
                />
              </View>
            </View>
          </View>
        </View>

        {/* Side by Side Comparison Panels */}
        <View style={styles.panelsContainer}>
          {/* Old Data Panel (Left) */}
          <View style={[styles.panelCard, { flex: 1 }]}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerIconBadge}>
                <Ionicons name="time-outline" size={13} color="#B48218" />
              </View>
              <ThemedText style={styles.cardHeaderTitle}>Old Data (Current)</ThemedText>
            </View>
            <View style={styles.panelBody}>
              <View style={styles.panelFieldRow}>
                <ThemedText style={styles.panelLabel}>Description</ThemedText>
                <TextInput style={styles.panelInputReadonly} value={oldData.Description} editable={false} />
              </View>
              <View style={styles.panelFieldRow}>
                <ThemedText style={styles.panelLabel}>Diamond Description</ThemedText>
                <TextInput style={styles.panelInputReadonly} value={oldData.DiamondDescription} editable={false} />
              </View>
              <View style={styles.panelFieldRow}>
                <ThemedText style={styles.panelLabel}>Weight</ThemedText>
                <TextInput style={[styles.panelInputReadonly, { textAlign: 'right' }]} value={oldData.Weight} editable={false} />
              </View>
              <View style={styles.panelFieldRow}>
                <ThemedText style={styles.panelLabel}>Gold Loss</ThemedText>
                <TextInput style={[styles.panelInputReadonly, { textAlign: 'right' }]} value={oldData.GoldLoss} editable={false} />
              </View>
              <View style={styles.panelFieldRow}>
                <ThemedText style={styles.panelLabel}>Cost/gm</ThemedText>
                <TextInput style={[styles.panelInputReadonly, { textAlign: 'right' }]} value={oldData.CostPerGram} editable={false} />
              </View>
              <View style={styles.panelFieldRow}>
                <ThemedText style={styles.panelLabel}>PMC</ThemedText>
                <TextInput style={[styles.panelInputReadonly, { textAlign: 'right' }]} value={oldData.PMC} editable={false} />
              </View>
              <View style={styles.panelFieldRow}>
                <ThemedText style={styles.panelLabel}>SMC</ThemedText>
                <TextInput style={[styles.panelInputReadonly, { textAlign: 'right' }]} value={oldData.SMC} editable={false} />
              </View>
              <View style={styles.panelFieldRow}>
                <ThemedText style={styles.panelLabel}>PCS</ThemedText>
                <TextInput style={[styles.panelInputReadonly, { textAlign: 'right' }]} value={oldData.PCS} editable={false} />
              </View>
              <View style={styles.panelFieldRow}>
                <ThemedText style={styles.panelLabel}>Size</ThemedText>
                <TextInput style={styles.panelInputReadonly} value={oldData.SizeCode} editable={false} />
              </View>
              <View style={styles.panelFieldRow}>
                <ThemedText style={styles.panelLabel}>Total Cost</ThemedText>
                <TextInput style={[styles.panelInputReadonly, { textAlign: 'right', fontWeight: 'bold' }]} value={oldData.TotalCost} editable={false} />
              </View>
            </View>
          </View>

          {/* Editable Data Panel (Right) */}
          <View style={[styles.panelCard, { flex: 1 }]}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerIconBadge}>
                <Ionicons name="create-outline" size={13} color="#B48218" />
              </View>
              <ThemedText style={styles.cardHeaderTitle}>Editable Data (Updates)</ThemedText>
            </View>
            <View style={styles.panelBody}>
              <View style={styles.panelFieldRow}>
                <ThemedText style={styles.panelLabel}>Description</ThemedText>
                <TextInput
                  style={styles.panelInput}
                  value={editableData.Description}
                  onChangeText={v => setEditableData(p => ({ ...p, Description: v }))}
                  autoCapitalize="characters"
                />
              </View>
              <View style={styles.panelFieldRow}>
                <ThemedText style={styles.panelLabel}>Diamond Description</ThemedText>
                <TextInput
                  style={styles.panelInput}
                  value={editableData.DiamondDescription}
                  onChangeText={v => setEditableData(p => ({ ...p, DiamondDescription: v }))}
                  autoCapitalize="characters"
                />
              </View>
              <View style={styles.panelFieldRow}>
                <ThemedText style={styles.panelLabel}>Weight</ThemedText>
                <TextInput style={[styles.panelInputReadonly, { textAlign: 'right' }]} value={editableData.Weight} editable={false} />
              </View>
              <View style={styles.panelFieldRow}>
                <ThemedText style={styles.panelLabel}>Gold Loss</ThemedText>
                <TextInput
                  style={[styles.panelInput, { textAlign: 'right' }]}
                  value={editableData.GoldLoss}
                  onChangeText={v => setEditableData(p => ({ ...p, GoldLoss: v }))}
                  keyboardType="numeric"
                />
              </View>
              <View style={styles.panelFieldRow}>
                <ThemedText style={styles.panelLabel}>Cost/gm</ThemedText>
                <TextInput style={[styles.panelInputReadonly, { textAlign: 'right' }]} value={editableData.CostPerGram} editable={false} />
              </View>
              <View style={styles.panelFieldRow}>
                <ThemedText style={styles.panelLabel}>PMC</ThemedText>
                <TextInput
                  style={[styles.panelInput, { textAlign: 'right' }]}
                  value={editableData.PMC}
                  onChangeText={v => setEditableData(p => ({ ...p, PMC: v }))}
                  keyboardType="numeric"
                />
              </View>
              <View style={styles.panelFieldRow}>
                <ThemedText style={styles.panelLabel}>SMC</ThemedText>
                <TextInput
                  style={[styles.panelInput, { textAlign: 'right' }]}
                  value={editableData.SMC}
                  onChangeText={v => setEditableData(p => ({ ...p, SMC: v }))}
                  keyboardType="numeric"
                />
              </View>
              <View style={styles.panelFieldRow}>
                <ThemedText style={styles.panelLabel}>PCS</ThemedText>
                <TextInput style={[styles.panelInputReadonly, { textAlign: 'right' }]} value={editableData.PCS} editable={false} />
              </View>
              <View style={styles.panelFieldRow}>
                <ThemedText style={styles.panelLabel}>Size</ThemedText>
                <View style={styles.inputWithBtn}>
                  <TextInput
                    style={[styles.panelInput, { flex: 1, borderTopRightRadius: 0, borderBottomRightRadius: 0 }]}
                    value={editableData.SizeCode}
                    editable={false}
                  />
                  <TouchableOpacity style={styles.lovBtn} onPress={() => fetchLOV('size')}>
                    <ThemedText style={styles.lovBtnText}>...</ThemedText>
                  </TouchableOpacity>
                </View>
              </View>
              <View style={styles.panelFieldRow}>
                <ThemedText style={styles.panelLabel}>Total Cost</ThemedText>
                <TextInput style={[styles.panelInputReadonly, { textAlign: 'right', fontWeight: 'bold' }]} value={editableData.TotalCost} editable={false} />
              </View>
            </View>
          </View>
        </View>

        {/* Footer Configuration Card */}
        <View style={styles.footerCard}>
          <View style={styles.cardHeaderBanner}>
            <View style={styles.headerIconBadge}>
              <Ionicons name="calculator-outline" size={13} color="#B48218" />
            </View>
            <ThemedText style={styles.cardHeaderTitle}>Calculation & Options</ThemedText>
          </View>

          <View style={styles.footerBody}>
            <View style={styles.footerRow}>
              {/* Additional Description Action Button */}
              <TouchableOpacity style={styles.outlineActionBtn} onPress={() => setShowAddlDesc(true)}>
                <Ionicons name="create-outline" size={14} color="#8D6511" style={{ marginRight: 5 }} />
                <ThemedText style={styles.outlineActionBtnText}>Addl. Desc</ThemedText>
              </TouchableOpacity>

              <View style={styles.verticalDivider} />

              {/* Purchase MC Calculation Pill Box */}
              <View style={styles.calcPillGroup}>
                <ThemedText style={styles.calcGroupLabel}>Purchase MC:</ThemedText>
                <View style={styles.pillBox}>
                  {renderRadio('Per Gram', pmcType === 'W', () => setPmcType('W'))}
                  {renderRadio('Per PCS', pmcType === 'P', () => setPmcType('P'))}
                </View>
                <View style={styles.calcDisplayBadge}>
                  <ThemedText style={styles.calcDisplayText}>{totalPmc}</ThemedText>
                </View>
              </View>

              <View style={styles.verticalDivider} />

              {/* Sales MC Calculation Pill Box */}
              <View style={styles.calcPillGroup}>
                <ThemedText style={styles.calcGroupLabel}>Sales MC:</ThemedText>
                <View style={styles.pillBox}>
                  {renderRadio('Per Gram', smcType === 'W', () => setSmcType('W'))}
                  {renderRadio('Per PCS', smcType === 'P', () => setSmcType('P'))}
                </View>
                <View style={styles.calcDisplayBadge}>
                  <ThemedText style={styles.calcDisplayText}>{totalSmc}</ThemedText>
                </View>
              </View>
            </View>
          </View>
        </View>
      </ScrollView>

      {/* LOV Modal */}
      <Modal visible={lovVisible} transparent animationType="fade" onRequestClose={() => setLovVisible(false)}>
        <TouchableWithoutFeedback onPress={() => setLovVisible(false)}>
          <View style={styles.modalOverlay}>
            <TouchableWithoutFeedback>
              <View style={styles.modalContent}>
                <View style={styles.modalHeader}>
                  <ThemedText style={styles.modalTitle}>
                    Select {lovType === 'size' ? 'Size' : 'Inventory'}
                  </ThemedText>
                  <TouchableOpacity onPress={() => setLovVisible(false)}>
                    <Ionicons name="close-circle-outline" size={24} color="#7A684C" />
                  </TouchableOpacity>
                </View>
                {loading ? (
                  <View style={{ padding: 30, alignItems: 'center' }}>
                    <ActivityIndicator size="large" color="#B48218" />
                  </View>
                ) : (
                  <FlatList
                    data={lovData}
                    keyExtractor={(i, idx) => idx.toString()}
                    style={{ marginTop: 8, maxHeight: 350 }}
                    renderItem={({ item }) => (
                      <TouchableOpacity style={styles.modalItem} onPress={() => handleSelectLOV(item)}>
                        {lovType === 'size' ? (
                          <View style={styles.modalRow}>
                            <ThemedText style={[styles.modalText, { fontWeight: 'bold' }]}>{item.SizeCode}</ThemedText>
                            <ThemedText style={[styles.modalText, { color: '#7A684C' }]}>{item.SizeType}</ThemedText>
                          </View>
                        ) : (
                          <View style={styles.modalRow}>
                            <ThemedText style={[styles.modalText, { fontWeight: 'bold', flex: 1.5 }]}>{item.InventoryCode}</ThemedText>
                            <ThemedText style={[styles.modalText, { flex: 2.5 }]}>{item.Description}</ThemedText>
                            <ThemedText style={[styles.modalText, { flex: 1, textAlign: 'right' }]}>{item.Weight}</ThemedText>
                          </View>
                        )}
                      </TouchableOpacity>
                    )}
                  />
                )}
                <TouchableOpacity style={styles.modalCloseBtn} onPress={() => setLovVisible(false)}>
                  <ThemedText style={styles.modalCloseBtnText}>Close</ThemedText>
                </TouchableOpacity>
              </View>
            </TouchableWithoutFeedback>
          </View>
        </TouchableWithoutFeedback>
      </Modal>

      {/* Additional Description Modal */}
      <Modal visible={showAddlDesc} transparent animationType="fade" onRequestClose={() => setShowAddlDesc(false)}>
        <TouchableWithoutFeedback onPress={() => setShowAddlDesc(false)}>
          <View style={styles.modalOverlay}>
            <TouchableWithoutFeedback>
              <View style={styles.addlDescModalContent}>
                <View style={styles.modalHeader}>
                  <ThemedText style={styles.modalTitle}>Additional Descriptions</ThemedText>
                  <TouchableOpacity onPress={() => setShowAddlDesc(false)}>
                    <Ionicons name="close-circle-outline" size={24} color="#7A684C" />
                  </TouchableOpacity>
                </View>

                <View style={styles.addlGridHeader}>
                  <ThemedText style={[styles.addlHeaderLabel, { flex: 2 }]}>Description</ThemedText>
                  <ThemedText style={[styles.addlHeaderLabel, { flex: 1, textAlign: 'right', paddingRight: 6 }]}>Cost</ThemedText>
                  <ThemedText style={[styles.addlHeaderLabel, { flex: 1, textAlign: 'right', paddingRight: 6 }]}>Selling Price</ThemedText>
                </View>

                {[1, 2, 3, 4].map(idx => (
                  <View key={'addl' + idx} style={styles.addlGridRow}>
                    <TextInput
                      style={styles.addlGridInputDesc}
                      value={addlData[`Description${idx}` as keyof typeof addlData]}
                      onChangeText={v => setAddlData(p => ({ ...p, [`Description${idx}`]: v }))}
                      placeholder={`Description ${idx}`}
                      placeholderTextColor="#A89E8D"
                    />
                    <TextInput
                      style={styles.addlGridInputAmount}
                      value={addlData[`Cost${idx}` as keyof typeof addlData]}
                      onChangeText={v => setAddlData(p => ({ ...p, [`Cost${idx}`]: v }))}
                      keyboardType="numeric"
                    />
                    <TextInput
                      style={styles.addlGridInputAmount}
                      value={addlData[`SellingPrice${idx}` as keyof typeof addlData]}
                      onChangeText={v => setAddlData(p => ({ ...p, [`SellingPrice${idx}`]: v }))}
                      keyboardType="numeric"
                    />
                  </View>
                ))}

                <TouchableOpacity style={styles.modalCloseBtn} onPress={() => setShowAddlDesc(false)}>
                  <ThemedText style={styles.modalCloseBtnText}>Done</ThemedText>
                </TouchableOpacity>
              </View>
            </TouchableWithoutFeedback>
          </View>
        </TouchableWithoutFeedback>
      </Modal>

      {/* Global Loading Overlay */}
      {(loading || saving) && (
        <View style={[StyleSheet.absoluteFill, { backgroundColor: 'rgba(0,0,0,0.3)', justifyContent: 'center', alignItems: 'center', zIndex: 999 }]}>
          <ActivityIndicator size="large" color="#B48218" />
          <ThemedText style={{ marginTop: 10, color: '#FFFFFF', fontWeight: 'bold', fontSize: 13 }}>
            {saving ? 'Saving updates...' : 'Loading inventory...'}
          </ThemedText>
        </View>
      )}

      {/* Toast Message */}
      {toastMsg.visible && (
        <View style={styles.toastBox}>
          <ThemedText style={styles.toastTitle}>{toastMsg.title}</ThemedText>
          <ThemedText style={styles.toastMessage}>{toastMsg.message}</ThemedText>
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
    height: 48,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingHorizontal: 16,
    borderBottomWidth: 1,
    borderBottomColor: '#EBE2D2',
    backgroundColor: '#FFFFFF',
  },
  backButton: {
    marginRight: 10,
    padding: 4,
  },
  title: {
    fontSize: 16,
    fontWeight: 'bold',
    color: '#3B2D17',
  },
  clearBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#DFD5C3',
    paddingVertical: 5,
    paddingHorizontal: 12,
    borderRadius: 6,
  },
  clearBtnText: {
    color: '#5C4A28',
    fontWeight: 'bold',
    fontSize: 12,
  },
  saveBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#F9F2E2',
    borderWidth: 1,
    borderColor: '#B8903B',
    paddingVertical: 5,
    paddingHorizontal: 14,
    borderRadius: 6,
  },
  saveBtnText: {
    color: '#9E6D08',
    fontWeight: 'bold',
    fontSize: 12,
  },

  container: {
    flex: 1,
    padding: 12,
    backgroundColor: '#FBF9F5',
  },

  // Search Filter Card
  searchCard: {
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    marginBottom: 12,
    overflow: 'hidden',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.05,
    shadowRadius: 2,
    elevation: 2,
  },
  searchFilterBody: {
    padding: 12,
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
    width: 20,
    height: 20,
    borderRadius: 10,
    backgroundColor: '#FFF8EA',
    borderWidth: 1,
    borderColor: '#E2D2AF',
    justifyContent: 'center',
    alignItems: 'center',
    marginRight: 8,
  },
  cardHeaderTitle: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#3B2D17',
    letterSpacing: 0.3,
  },

  searchRow: {
    flexDirection: 'row',
    alignItems: 'center',
    flexWrap: 'wrap',
    gap: 14,
  },
  fieldGroup: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 6,
  },
  inputWithBtn: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  label: {
    fontWeight: 'bold',
    color: '#4C3B24',
    fontSize: 11,
  },
  input: {
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderRadius: 6,
    paddingHorizontal: 8,
    height: 28,
    fontSize: 12,
    backgroundColor: '#FFFFFF',
    color: '#2C2519',
  },
  readonlyInput: {
    backgroundColor: '#F8F5EE',
    borderColor: '#EADBCA',
    color: '#554736',
  },
  lovBtn: {
    borderWidth: 1,
    borderLeftWidth: 0,
    borderColor: '#E2D7C5',
    backgroundColor: '#FAF5EA',
    paddingHorizontal: 8,
    height: 28,
    justifyContent: 'center',
    alignItems: 'center',
    borderTopRightRadius: 6,
    borderBottomRightRadius: 6,
  },
  lovBtnText: {
    fontWeight: 'bold',
    color: '#6E5528',
    fontSize: 12,
  },

  // Side-by-side Panels
  panelsContainer: {
    flexDirection: 'row',
    gap: 12,
    marginBottom: 12,
  },
  panelCard: {
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    overflow: 'hidden',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.05,
    shadowRadius: 2,
    elevation: 2,
  },
  panelBody: {
    padding: 12,
    gap: 6,
  },
  panelFieldRow: {
    flexDirection: 'row',
    alignItems: 'center',
    marginBottom: 4,
  },
  panelLabel: {
    width: 140,
    fontSize: 11,
    fontWeight: 'bold',
    color: '#4C3B24',
  },
  panelInput: {
    flex: 1,
    height: 26,
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderRadius: 6,
    paddingHorizontal: 6,
    fontSize: 12,
    color: '#2C2519',
    backgroundColor: '#FFFFFF',
  },
  panelInputReadonly: {
    flex: 1,
    height: 26,
    borderWidth: 1,
    borderColor: '#EADBCA',
    borderRadius: 6,
    paddingHorizontal: 6,
    fontSize: 12,
    color: '#554736',
    backgroundColor: '#F8F5EE',
  },

  // Footer Card
  footerCard: {
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    marginBottom: 12,
    overflow: 'hidden',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.05,
    shadowRadius: 2,
    elevation: 2,
  },
  footerBody: {
    padding: 12,
  },
  footerRow: {
    flexDirection: 'row',
    alignItems: 'center',
    flexWrap: 'wrap',
    gap: 12,
  },
  outlineActionBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    borderWidth: 1,
    borderColor: '#DFD3BC',
    backgroundColor: '#FBF8F2',
    paddingHorizontal: 12,
    height: 28,
    borderRadius: 6,
  },
  outlineActionBtnText: {
    fontWeight: 'bold',
    fontSize: 11,
    color: '#6E5528',
  },
  verticalDivider: {
    width: 1,
    height: 22,
    backgroundColor: '#E8DCB8',
  },
  calcPillGroup: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 6,
  },
  calcGroupLabel: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#4C3B24',
  },
  pillBox: {
    flexDirection: 'row',
    alignItems: 'center',
    borderWidth: 1,
    borderColor: '#DFD3BC',
    borderRadius: 14,
    paddingHorizontal: 8,
    height: 28,
    backgroundColor: '#FBF8F2',
    gap: 8,
  },
  calcDisplayBadge: {
    backgroundColor: '#FFF4DC',
    borderWidth: 1,
    borderColor: '#ECD8AB',
    borderRadius: 6,
    paddingHorizontal: 8,
    height: 28,
    justifyContent: 'center',
    alignItems: 'center',
    minWidth: 55,
  },
  calcDisplayText: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#8A5D07',
  },

  // Radios
  radioBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 4,
  },
  radioCircle: {
    height: 14,
    width: 14,
    borderRadius: 7,
    borderWidth: 1.5,
    borderColor: '#C7B797',
    alignItems: 'center',
    justifyContent: 'center',
    backgroundColor: '#FFFFFF',
  },
  radioCircleActive: {
    borderColor: '#B8903B',
  },
  radioDot: {
    height: 6,
    width: 6,
    borderRadius: 3,
    backgroundColor: '#B8903B',
  },
  radioLabel: {
    fontSize: 11,
    color: '#554736',
    fontWeight: '500',
  },
  radioLabelActive: {
    color: '#3B2D17',
    fontWeight: 'bold',
  },

  // Modals
  modalOverlay: {
    flex: 1,
    backgroundColor: 'rgba(0,0,0,0.4)',
    justifyContent: 'center',
    alignItems: 'center',
  },
  modalContent: {
    width: '80%',
    maxWidth: 550,
    maxHeight: '80%',
    backgroundColor: '#FFFFFF',
    padding: 16,
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.15,
    shadowRadius: 8,
    elevation: 5,
  },
  modalHeader: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    borderBottomWidth: 1,
    borderBottomColor: '#E8DCB8',
    paddingBottom: 8,
    marginBottom: 8,
  },
  modalTitle: {
    fontSize: 14,
    fontWeight: 'bold',
    color: '#3B2D17',
  },
  modalItem: {
    paddingVertical: 8,
    paddingHorizontal: 8,
    borderBottomWidth: 1,
    borderBottomColor: '#EFE7D8',
    backgroundColor: '#FFFFFF',
  },
  modalRow: {
    flexDirection: 'row',
    gap: 8,
    alignItems: 'center',
  },
  modalText: {
    fontSize: 11,
    color: '#2C2519',
  },
  modalCloseBtn: {
    alignSelf: 'flex-end',
    marginTop: 12,
    paddingVertical: 6,
    paddingHorizontal: 16,
    borderRadius: 6,
    backgroundColor: '#F9F2E2',
    borderWidth: 1,
    borderColor: '#B8903B',
  },
  modalCloseBtnText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#9E6D08',
  },

  // Addl Desc Modal
  addlDescModalContent: {
    width: '90%',
    maxWidth: 620,
    backgroundColor: '#FFFFFF',
    padding: 16,
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.15,
    shadowRadius: 8,
    elevation: 5,
  },
  addlGridHeader: {
    flexDirection: 'row',
    marginBottom: 6,
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    backgroundColor: '#F5E7CA',
    paddingVertical: 6,
    paddingHorizontal: 8,
    borderRadius: 4,
  },
  addlHeaderLabel: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3B2D17',
  },
  addlGridRow: {
    flexDirection: 'row',
    marginBottom: 8,
    gap: 8,
  },
  addlGridInputDesc: {
    flex: 2,
    height: 28,
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderRadius: 6,
    fontSize: 11,
    paddingHorizontal: 6,
    backgroundColor: '#FFFFFF',
    color: '#2C2519',
  },
  addlGridInputAmount: {
    flex: 1,
    height: 28,
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderRadius: 6,
    fontSize: 11,
    paddingHorizontal: 6,
    backgroundColor: '#FFFFFF',
    color: '#2C2519',
    textAlign: 'right',
  },

  // Toast
  toastBox: {
    position: 'absolute',
    bottom: 40,
    alignSelf: 'center',
    width: '80%',
    maxWidth: 400,
    backgroundColor: '#2E2413',
    padding: 12,
    borderRadius: 8,
    zIndex: 1000,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.25,
    shadowRadius: 4,
    elevation: 5,
    borderWidth: 1,
    borderColor: '#B8903B',
  },
  toastTitle: {
    color: '#FFBD00',
    fontWeight: 'bold',
    fontSize: 12,
    marginBottom: 3,
  },
  toastMessage: {
    color: '#FFFFFF',
    fontSize: 11,
  },
});
