import { ThemedText } from '@/components/themed-text';
import { API_BASE_URL } from '@/constants/api';
import { useThemeContext } from '@/contexts/ThemeContext';
import { useThemeColors } from '@/hooks/useThemeColors';
import { Ionicons } from '@expo/vector-icons';
import { Stack, useRouter } from 'expo-router';
import React, { useEffect, useState } from 'react';
import {
  FlatList,
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

const API_URL = `${API_BASE_URL}`;

export default function PurchaseReturnScreen() {
  const themeColors = useThemeColors();
  const { accentColor } = useThemeContext();
  const { background: backgroundColor, card: cardColor, text: textColor, border: borderColor, subtitle: subtitleColor, primaryGold: GOLD_COLOR } = themeColors;
  const router = useRouter();

  // ===== STATE =====
  const [refNo, setRefNo] = useState('');
  const [returnDate, setReturnDate] = useState(new Date().toISOString().split('T')[0]);
  const [supplierCode, setSupplierCode] = useState('');
  const [supplierName, setSupplierName] = useState('');
  const [remarks, setRemarks] = useState('');

  // Total costs
  const [gstActive, setGstActive] = useState(false);
  const [totalCost, setTotalCost] = useState(0);
  const [gstAmount, setGstAmount] = useState(0);
  const [totalMc, setTotalMc] = useState(0);

  const [totalWeight, setTotalWeight] = useState(0);
  const [totalPcs, setTotalPcs] = useState(0);

  // Table details array
  const [details, setDetails] = useState<any[]>([]);
  const [selectedIndex, setSelectedIndex] = useState<number | null>(null);

  // Bottom Add row form
  const [inventoryCode, setInventoryCode] = useState('');
  const [description, setDescription] = useState('');
  const [weight, setWeight] = useState('');
  const [goldLoss, setGoldLoss] = useState('');
  const [mc, setMc] = useState('');
  const [costPerGram, setCostPerGram] = useState('');
  const [pcs, setPcs] = useState('');
  const [value, setValue] = useState('');
  const [purchaseType, setPurchaseType] = useState('W');
  const [qtyOnHand, setQtyOnHand] = useState(0);

  const [saving, setSaving] = useState(false);
  const [loading, setLoading] = useState(false);
  const [toast, setToast] = useState<{ message: string; type: 'success' | 'error' } | null>(null);

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

  useEffect(() => {
    calculateTotal(details, gstActive);
  }, [gstActive]);

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
    const filtered = lovData.filter(item =>
      Object.values(item).some(val => String(val).toLowerCase().includes(lower))
    );
    setFilteredLovData(filtered);
  };

  const handleLovSelect = (item: any) => {
    if (onLovSelect) onLovSelect(item);
    setLovVisible(false);
  };

  const fetchSuppliers = async () => {
    setLoading(true);
    try {
      const res = await fetch(`${API_URL}/supplier`);
      const json = await res.json();
      if (json.success) {
        openLov('Select Supplier', json.data, ['SupplierCode', 'SupplierName'], item => {
          setSupplierCode(item.SupplierCode);
          setSupplierName(item.SupplierName);
        });
      } else {
        showToast('Failed to fetch suppliers', 'error');
      }
    } catch (e) {
      showToast('Error connecting to server', 'error');
    } finally {
      setLoading(false);
    }
  };

  const fetchInventoryItems = async () => {
    setLoading(true);
    try {
      const res = await fetch(`${API_URL}/purchasereturn/lov/inventory?SupplierCode=${supplierCode}`);
      const json = await res.json();
      if (json.success) {
        openLov(
          'Select Inventory Stock',
          json.data,
          ['InventoryCode', 'Description', 'Weight', 'QtyOnHand'],
          item => {
            setInventoryCode(item.InventoryCode);
            loadInventoryDetail(item.InventoryCode);
          }
        );
      } else {
        showToast('Failed to fetch inventory stock list', 'error');
      }
    } catch (e) {
      showToast('Error connecting to server', 'error');
    } finally {
      setLoading(false);
    }
  };

  const loadInventoryDetail = async (code: string) => {
    try {
      const res = await fetch(`${API_URL}/purchasereturn/inventoryDetail?InventoryCode=${code}`);
      const json = await res.json();
      if (json.success) {
        const item = json.data;
        setDescription(item.Description || '');
        setWeight(item.Weight ? item.Weight.toString() : '0');
        setGoldLoss(item.GoldLoss ? item.GoldLoss.toString() : '0');
        setCostPerGram(item.CostPerGram ? item.CostPerGram.toString() : '0');
        setMc(item.PMC ? item.PMC.toString() : '0');
        setPcs(item.QtyOnHand ? item.QtyOnHand.toString() : '1');
        setValue(item.TotalCost ? item.TotalCost.toString() : '0');
        setPurchaseType(item.PurchaseType || 'W');
        setQtyOnHand(item.QtyOnHand || 0);
      } else {
        showToast(json.message || 'Failed to load inventory info', 'error');
      }
    } catch (e) {
      showToast('Error fetching inventory details', 'error');
    }
  };

  const handleAddRow = () => {
    if (!inventoryCode.trim()) {
      showToast('Select Inventory Code!', 'error');
      return;
    }
    const valPcs = parseInt(pcs) || 0;
    if (valPcs < 1) {
      showToast('PCS should be greater than or equal to one!', 'error');
      return;
    }
    if (valPcs > qtyOnHand) {
      showToast(`Maximum available PCS for this Inventory is ${qtyOnHand}`, 'error');
      return;
    }

    const existingIdx = details.findIndex(d => d.InventoryCode === inventoryCode);
    let updatedDetails = [...details];

    if (existingIdx !== -1) {
      const exist = updatedDetails[existingIdx];
      const newPcs = exist.PCS + valPcs;
      if (newPcs > qtyOnHand) {
        showToast(`Maximum available PCS for this Inventory is ${qtyOnHand}`, 'error');
        return;
      }
      exist.PCS = newPcs;
      exist.Weight = exist.Weight + (parseFloat(weight) || 0);
      exist.GoldLoss = exist.GoldLoss + (parseFloat(goldLoss) || 0);
      exist.MC = exist.MC + (parseFloat(mc) || 0);
      exist.TotalCost = (exist.Weight + exist.GoldLoss) * exist.CostPerGram + exist.MC;
    } else {
      updatedDetails.push({
        InventoryCode: inventoryCode,
        Description: description,
        Weight: parseFloat(weight) || 0,
        GoldLoss: parseFloat(goldLoss) || 0,
        MC: parseFloat(mc) || 0,
        CostPerGram: parseFloat(costPerGram) || 0,
        PCS: valPcs,
        TotalCost: parseFloat(value) || 0
      });
    }

    setDetails(updatedDetails);
    calculateTotal(updatedDetails, gstActive);
    clearItemFields();
    showToast('Added item to list', 'success');
  };

  const handleDeleteRow = () => {
    if (selectedIndex === null) {
      showToast('Select an item row from grid to delete', 'error');
      return;
    }
    const updated = details.filter((_, i) => i !== selectedIndex);
    setDetails(updated);
    setSelectedIndex(null);
    calculateTotal(updated, gstActive);
    showToast('Deleted item row', 'success');
  };

  const calculateTotal = (list: any[], checkGst: boolean) => {
    let totWt = 0;
    let totPcs = 0;
    let totVal = 0;

    list.forEach(item => {
      totWt += item.Weight;
      totPcs += item.PCS;
      totVal += item.TotalCost;
    });

    setTotalWeight(totWt);
    setTotalPcs(totPcs);
    setTotalCost(totVal);

    if (checkGst) {
      const gstVal = Number((totVal * 0.09).toFixed(2));
      setGstAmount(gstVal);
      setTotalMc(totVal + gstVal);
    } else {
      setGstAmount(0);
      setTotalMc(totVal);
    }
  };

  const clearItemFields = () => {
    setInventoryCode('');
    setDescription('');
    setWeight('');
    setGoldLoss('');
    setMc('');
    setCostPerGram('');
    setPcs('');
    setValue('');
    setQtyOnHand(0);
  };

  const handleClearAll = () => {
    setRefNo('');
    setReturnDate(new Date().toISOString().split('T')[0]);
    setSupplierCode('');
    setSupplierName('');
    setRemarks('');
    setGstActive(false);
    setTotalCost(0);
    setGstAmount(0);
    setTotalMc(0);
    setTotalWeight(0);
    setTotalPcs(0);
    setDetails([]);
    setSelectedIndex(null);
    clearItemFields();
    showToast('Form Cleared', 'success');
  };

  const handleSave = async () => {
    if (!supplierCode) {
      showToast('Supplier Code is required', 'error');
      return;
    }
    if (details.length === 0) {
      showToast('There is no inventory in the Purchase Return List!', 'error');
      return;
    }
    if (refNo) {
      showToast('You can only print old reference transactions!', 'error');
      return;
    }

    setSaving(true);
    try {
      const res = await fetch(`${API_URL}/purchasereturn`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          header: {
            SupplierCode: supplierCode,
            ReturnDate: returnDate,
            TotalWeight: totalWeight,
            TotalPCS: totalPcs,
            TotalValue: totalCost,
            TotalGST: gstAmount,
            NetTotal: totalMc,
            Remarks: remarks,
            LocationCode: 'HQ'
          },
          details: details
        })
      });
      const json = await res.json();
      if (json.success) {
        setRefNo(json.returnNo);
        showToast(`Saved. Reference No: ${json.returnNo}`, 'success');
        setDetails([]);
        setSelectedIndex(null);
      } else {
        showToast(json.message || 'Failed to save purchase return', 'error');
      }
    } catch (e) {
      showToast('Server connection failed', 'error');
    } finally {
      setSaving(false);
    }
  };

  const Checkbox = ({ label, selected, onPress }: { label: string; selected: boolean; onPress: () => void }) => (
    <TouchableOpacity style={styles.radioContainer} onPress={onPress}>
      <View style={[styles.checkboxSquare, selected && styles.checkboxSquareActive]}>
        {selected && <Ionicons name="checkmark" size={10} color="#FFFFFF" />}
      </View>
      {label ? <ThemedText style={styles.radioLabel}>{label}</ThemedText> : null}
    </TouchableOpacity>
  );

  return (
    <SafeAreaView edges={['top']} style={styles.safeArea}>
      <Stack.Screen options={{ headerShown: false }} />

      {/* Top Header Bar matching jobsheet.tsx */}
      <View style={styles.header}>
        <View style={styles.headerLeft}>
          <TouchableOpacity onPress={() => router.back()} style={styles.backButton}>
            <Ionicons name="arrow-back" size={20} color="#3B2D17" />
          </TouchableOpacity>
          <ThemedText style={styles.headerTitle}>Purchase Return</ThemedText>
        </View>

        <View style={styles.headerActions}>
          <TouchableOpacity style={styles.clearBtn} onPress={handleClearAll}>
            <ThemedText style={styles.clearBtnText}>Clear</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity
            style={styles.saveBtn}
            onPress={handleSave}
            disabled={saving}
          >
            <ThemedText style={styles.saveBtnText}>{saving ? 'Saving...' : 'Save'}</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity
            style={[styles.printBtn, !refNo && { opacity: 0.5 }]}
            onPress={() => refNo && showToast(`Printing Ref: ${refNo}`, 'success')}
            disabled={!refNo}
          >
            <ThemedText style={styles.printBtnText}>Print</ThemedText>
          </TouchableOpacity>
        </View>
      </View>

      <ScrollView style={styles.container} contentContainerStyle={styles.contentContainer}>
        {/* TOP SECTION: Form Header (JobSheet-style Gold Card) */}
        <View style={styles.goldCard}>
          {/* Card Header Banner */}
          <View style={[styles.cardHeaderBanner, { justifyContent: 'flex-start' }]}>
            <View style={styles.headerIconBadge}>
              <Ionicons name="document-text" size={15} color="#B48218" />
            </View>
            <ThemedText style={styles.cardHeaderTitle}>Purchase Return Details</ThemedText>
          </View>

          <View style={styles.cardBody}>
            {/* Row 1: Ref. No, Return Date, Total Cost */}
            <View style={styles.row}>
              <ThemedText style={styles.label}>Ref. No.</ThemedText>
              <TextInput
                style={[styles.input, styles.readonlyInput, { width: 140 }]}
                value={refNo}
                editable={false}
                placeholder="Auto"
              />

              <ThemedText style={[styles.labelRight, { width: 95 }]}>Return Date</ThemedText>
              {Platform.OS === 'web' ? (
                <input
                  type="date"
                  style={{
                    width: 140,
                    border: '1px solid #E2D7C5',
                    borderRadius: '6px',
                    padding: '4px 8px',
                    fontSize: '12px',
                    height: '28px',
                    color: '#2C2519',
                    backgroundColor: '#FFFFFF',
                    outline: 'none',
                    boxSizing: 'border-box',
                  }}
                  value={returnDate}
                  onChange={(e: any) => setReturnDate(e.target.value || '')}
                />
              ) : (
                <TextInput
                  style={[styles.input, { width: 140 }]}
                  value={returnDate}
                  onChangeText={setReturnDate}
                  placeholder="YYYY-MM-DD"
                />
              )}

              <ThemedText style={[styles.labelRight, { width: 85 }]}>Total Cost</ThemedText>
              <TextInput
                style={[styles.input, styles.readonlyInput, { width: 120, textAlign: 'right', fontWeight: 'bold' }]}
                value={totalCost.toFixed(2)}
                editable={false}
              />
            </View>

            {/* Row 2: Supplier Code, Supplier Name, GST Checkbox & Amount */}
            <View style={styles.row}>
              <ThemedText style={styles.label}>Supplier Code</ThemedText>
              <View style={{ width: 140, flexDirection: 'row' }}>
                <TextInput
                  style={[styles.input, { width: 108, borderTopRightRadius: 0, borderBottomRightRadius: 0, borderRightWidth: 0 }]}
                  value={supplierCode}
                  onChangeText={setSupplierCode}
                />
                <TouchableOpacity style={styles.lookupBtn} onPress={fetchSuppliers}>
                  <ThemedText style={styles.lookupBtnText}>...</ThemedText>
                </TouchableOpacity>
              </View>

              <TextInput
                style={[styles.input, styles.readonlyInput, { flex: 1, minWidth: 180, marginLeft: 8 }]}
                value={supplierName}
                editable={false}
                placeholder="Supplier Name"
              />

              <View style={{ flexDirection: 'row', alignItems: 'center', marginLeft: 16 }}>
                <Checkbox label="GST" selected={gstActive} onPress={() => setGstActive(!gstActive)} />
                <TextInput
                  style={[styles.input, styles.readonlyInput, { width: 90, textAlign: 'right' }]}
                  value={gstAmount.toFixed(2)}
                  editable={false}
                />
              </View>
            </View>

            {/* Row 3: Remarks, Total MC */}
            <View style={styles.row}>
              <ThemedText style={styles.label}>Remarks</ThemedText>
              <TextInput
                style={[styles.input, { flex: 1, minWidth: 200 }]}
                value={remarks}
                onChangeText={setRemarks}
                placeholder="Enter remarks..."
              />

              <ThemedText style={[styles.labelRight, { width: 85 }]}>Total MC</ThemedText>
              <TextInput
                style={[styles.input, styles.readonlyInput, { width: 120, textAlign: 'right', fontWeight: 'bold', color: '#9E6D08' }]}
                value={totalMc.toFixed(2)}
                editable={false}
              />
            </View>
          </View>
        </View>

        {/* MIDDLE SECTION: Items Grid Table (JobSheet-style Table) */}
        <View style={styles.goldCard}>

          <ScrollView horizontal={true} showsHorizontalScrollIndicator={true} contentContainerStyle={{ flexGrow: 1 }}>
            <View style={{ width: '100%', minWidth: 920 }}>
              {/* Table Header */}
              <View style={styles.tableHeader}>
                <ThemedText style={[styles.tableHeaderText, { width: 140 }]}>Inventory Code</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { flex: 2 }]}>Description</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { width: 90, textAlign: 'right' }]}>Weight</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { width: 90, textAlign: 'right' }]}>Gold Loss</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { width: 100, textAlign: 'right' }]}>Cost/Gram</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { width: 90, textAlign: 'right' }]}>MC</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { width: 70, textAlign: 'center' }]}>PCS</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { width: 110, textAlign: 'right' }]}>Value</ThemedText>
              </View>

              {/* Table Body */}
              <ScrollView nestedScrollEnabled={true} style={{ maxHeight: 220, minHeight: details.length === 0 ? 80 : 0 }}>
                {details.length === 0 ? (
                  <View style={styles.emptyTableState}>
                    <Ionicons name="cube-outline" size={28} color="#D0C2A8" />
                    <ThemedText style={{ color: '#7D6F5B', fontSize: 12, marginTop: 4 }}>
                      No items added to return list yet
                    </ThemedText>
                  </View>
                ) : (
                  details.map((item, idx) => {
                    const isSelected = selectedIndex === idx;
                    return (
                      <TouchableOpacity
                        key={idx}
                        activeOpacity={0.7}
                        style={[
                          styles.tableRow,
                          idx % 2 === 1 && { backgroundColor: '#FCFAF6' },
                          isSelected && styles.tableRowSelected,
                        ]}
                        onPress={() => setSelectedIndex(isSelected ? null : idx)}
                      >
                        <ThemedText style={[styles.tableCellText, { width: 140, fontWeight: 'bold' }]}>
                          {item.InventoryCode}
                        </ThemedText>
                        <ThemedText style={[styles.tableCellText, { flex: 2 }]} numberOfLines={1}>
                          {item.Description}
                        </ThemedText>
                        <ThemedText style={[styles.tableCellText, { width: 90, textAlign: 'right' }]}>
                          {item.Weight ? Number(item.Weight).toFixed(3) : '0.000'}
                        </ThemedText>
                        <ThemedText style={[styles.tableCellText, { width: 90, textAlign: 'right' }]}>
                          {item.GoldLoss ? Number(item.GoldLoss).toFixed(3) : '0.000'}
                        </ThemedText>
                        <ThemedText style={[styles.tableCellText, { width: 100, textAlign: 'right' }]}>
                          {item.CostPerGram ? Number(item.CostPerGram).toFixed(2) : '0.00'}
                        </ThemedText>
                        <ThemedText style={[styles.tableCellText, { width: 90, textAlign: 'right' }]}>
                          {item.MC ? Number(item.MC).toFixed(2) : '0.00'}
                        </ThemedText>
                        <ThemedText style={[styles.tableCellText, { width: 70, textAlign: 'center', fontWeight: 'bold' }]}>
                          {item.PCS}
                        </ThemedText>
                        <ThemedText style={[styles.tableCellText, { width: 110, textAlign: 'right', fontWeight: 'bold', color: '#9E6D08' }]}>
                          {item.TotalCost ? Number(item.TotalCost).toFixed(2) : '0.00'}
                        </ThemedText>
                      </TouchableOpacity>
                    );
                  })
                )}
              </ScrollView>

              {/* Table Total Summary Row */}
              <View style={styles.tableTotalRow}>
                <ThemedText style={[styles.tableTotalCellText, { width: 140 }]}>TOTAL SUMMARY</ThemedText>
                <View style={{ flex: 2 }} />
                <ThemedText style={[styles.tableTotalCellText, { width: 90, textAlign: 'right' }]}>
                  {totalWeight.toFixed(3)} g
                </ThemedText>
                <View style={{ width: 90 }} />
                <View style={{ width: 100 }} />
                <View style={{ width: 90 }} />
                <ThemedText style={[styles.tableTotalCellText, { width: 70, textAlign: 'center' }]}>
                  {totalPcs}
                </ThemedText>
                <ThemedText style={[styles.tableTotalCellText, { width: 110, textAlign: 'right', color: '#9E6D08' }]}>
                  ${totalCost.toFixed(2)}
                </ThemedText>
              </View>
            </View>
          </ScrollView>
        </View>

        {/* BOTTOM SECTION: Add / Edit Item Form (JobSheet-style Gold Card) */}
        <View style={styles.goldCard}>
          <View style={styles.cardHeaderBanner}>
            <View style={styles.headerIconBadge}>
              <Ionicons name="add-circle" size={15} color="#B48218" />
            </View>
            <ThemedText style={styles.cardHeaderTitle}>Add Item To Return List</ThemedText>
          </View>

          <View style={styles.cardBody}>
            {/* Row 1: Code, Desc, Weight, Gold Loss, MC, Cost/Gram, PCS, Value */}
            <View style={styles.row}>
              {/* Inventory Code */}
              <View style={{ width: 150, marginRight: 8 }}>
                <ThemedText style={styles.inputSubLabel}>Inventory Code</ThemedText>
                <View style={{ flexDirection: 'row' }}>
                  <TextInput
                    style={[styles.input, { flex: 1, borderTopRightRadius: 0, borderBottomRightRadius: 0, borderRightWidth: 0 }]}
                    value={inventoryCode}
                    onChangeText={setInventoryCode}
                  />
                  <TouchableOpacity style={styles.lookupBtn} onPress={fetchInventoryItems}>
                    <ThemedText style={styles.lookupBtnText}>...</ThemedText>
                  </TouchableOpacity>
                </View>
              </View>

              {/* Description */}
              <View style={{ flex: 2, minWidth: 150, marginRight: 8 }}>
                <ThemedText style={styles.inputSubLabel}>Description</ThemedText>
                <TextInput style={[styles.input, styles.readonlyInput]} value={description} editable={false} />
              </View>

              {/* Weight */}
              <View style={{ width: 78, marginRight: 8 }}>
                <ThemedText style={styles.inputSubLabel}>Weight</ThemedText>
                <TextInput style={[styles.input, styles.readonlyInput, { textAlign: 'right' }]} value={weight} editable={false} />
              </View>

              {/* Gold Loss */}
              <View style={{ width: 78, marginRight: 8 }}>
                <ThemedText style={styles.inputSubLabel}>Gold Loss</ThemedText>
                <TextInput style={[styles.input, styles.readonlyInput, { textAlign: 'right' }]} value={goldLoss} editable={false} />
              </View>

              {/* MC */}
              <View style={{ width: 75, marginRight: 8 }}>
                <ThemedText style={styles.inputSubLabel}>MC</ThemedText>
                <TextInput style={[styles.input, styles.readonlyInput, { textAlign: 'right' }]} value={mc} editable={false} />
              </View>

              {/* Cost/Gram */}
              <View style={{ width: 85, marginRight: 8 }}>
                <ThemedText style={styles.inputSubLabel}>Cost/Gram</ThemedText>
                <TextInput style={[styles.input, styles.readonlyInput, { textAlign: 'right' }]} value={costPerGram} editable={false} />
              </View>

              {/* PCS */}
              <View style={{ width: 68, marginRight: 8 }}>
                <ThemedText style={styles.inputSubLabel}>PCS</ThemedText>
                <TextInput
                  style={[
                    styles.input,
                    purchaseType === 'W' && styles.readonlyInput,
                    { textAlign: 'center', fontWeight: 'bold' },
                  ]}
                  value={pcs}
                  onChangeText={setPcs}
                  keyboardType="numeric"
                  editable={purchaseType !== 'W'}
                />
              </View>

              {/* Value */}
              <View style={{ width: 95 }}>
                <ThemedText style={styles.inputSubLabel}>Value</ThemedText>
                <TextInput
                  style={[styles.input, styles.readonlyInput, { textAlign: 'right', fontWeight: 'bold' }]}
                  value={value}
                  editable={false}
                />
              </View>
            </View>

            {/* Bottom Action Buttons */}
            <View style={styles.actionButtonRow}>
              <TouchableOpacity style={styles.clearMiniBtn} onPress={clearItemFields}>
                <ThemedText style={styles.clearMiniBtnText}>Clear</ThemedText>
              </TouchableOpacity>

              <View style={{ flexDirection: 'row', gap: 8 }}>
                <TouchableOpacity
                  style={[styles.deleteMiniBtn, selectedIndex === null && { opacity: 0.5 }]}
                  onPress={handleDeleteRow}
                  disabled={selectedIndex === null}
                >
                  <ThemedText style={styles.deleteMiniBtnText}>Delete</ThemedText>
                </TouchableOpacity>

                <TouchableOpacity style={styles.addMiniBtn} onPress={handleAddRow}>
                  <ThemedText style={styles.addMiniBtnText}>Add</ThemedText>
                </TouchableOpacity>
              </View>
            </View>
          </View>
        </View>
      </ScrollView>

      {/* Modern LOV Modal matching Jobsheet */}
      <Modal visible={lovVisible} transparent={true} animationType="fade">
        <TouchableWithoutFeedback onPress={() => setLovVisible(false)}>
          <View style={styles.modalOverlay}>
            <TouchableWithoutFeedback>
              <View style={styles.modalContainer}>
                <View style={styles.modalHeader}>
                  <ThemedText style={styles.modalTitle}>{lovTitle}</ThemedText>
                  <TouchableOpacity onPress={() => setLovVisible(false)} style={styles.modalCloseIconBtn}>
                    <Ionicons name="close" size={20} color="#3B2D17" />
                  </TouchableOpacity>
                </View>

                <View style={{ padding: 12 }}>
                  <TextInput
                    style={styles.modalSearchInput}
                    placeholder="Search options..."
                    placeholderTextColor="#7D6F5B"
                    value={lovSearch}
                    onChangeText={handleLovSearch}
                    autoFocus
                  />

                  {lovColumns.length > 0 && (
                    <View style={styles.lovHeaderRow}>
                      {lovColumns.map((col, idx) => (
                        <ThemedText key={idx} style={[styles.lovHeaderText, { flex: col === 'Description' ? 2 : 1 }]}>
                          {col}
                        </ThemedText>
                      ))}
                    </View>
                  )}

                  <FlatList
                    data={filteredLovData}
                    keyExtractor={(item, index) => index.toString()}
                    style={{ maxHeight: 320 }}
                    renderItem={({ item, index }) => (
                      <TouchableOpacity
                        activeOpacity={0.7}
                        style={[
                          styles.lovItem,
                          index % 2 === 1 && { backgroundColor: '#FCFAF6' },
                        ]}
                        onPress={() => handleLovSelect(item)}
                      >
                        {lovColumns.length > 0 ? (
                          lovColumns.map((col, idx) => (
                            <ThemedText
                              key={idx}
                              style={[
                                styles.lovItemText,
                                { flex: col === 'Description' ? 2 : 1 },
                                idx === 0 && { fontWeight: 'bold' },
                              ]}
                            >
                              {String(item[col] ?? '')}
                            </ThemedText>
                          ))
                        ) : (
                          <ThemedText style={styles.lovItemText}>{Object.values(item).join(' - ')}</ThemedText>
                        )}
                      </TouchableOpacity>
                    )}
                    ListEmptyComponent={
                      <View style={{ padding: 20, alignItems: 'center' }}>
                        <ThemedText style={{ color: '#7D6F5B', fontSize: 13, fontStyle: 'italic' }}>
                          No records found
                        </ThemedText>
                      </View>
                    }
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

      {/* Toast Notification */}
      {toast && (
        <View style={[styles.toast, { backgroundColor: toast.type === 'success' ? '#2E7D32' : '#C62828' }]}>
          <ThemedText style={styles.toastText}>{toast.message}</ThemedText>
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
  // Top Header Bar
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
  headerLeft: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  backButton: {
    padding: 4,
    marginRight: 10,
  },
  headerTitle: {
    fontSize: 20,
    fontWeight: '400',
    letterSpacing: 1,
    color: '#3B2D17',
  },
  headerActions: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 8,
  },
  clearBtn: {
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#DFD5C3',
    paddingVertical: 6,
    paddingHorizontal: 12,
    borderRadius: 5,
  },
  clearBtnText: {
    color: '#5C4A28',
    fontWeight: 'bold',
    fontSize: 12,
  },
  saveBtn: {
    backgroundColor: '#F9F2E2',
    borderWidth: 1,
    borderColor: '#B8903B',
    paddingVertical: 6,
    paddingHorizontal: 16,
    borderRadius: 5,
  },
  saveBtnText: {
    color: '#9E6D08',
    fontWeight: 'bold',
    fontSize: 12,
  },
  printBtn: {
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#DFD5C3',
    paddingVertical: 6,
    paddingHorizontal: 12,
    borderRadius: 5,
  },
  printBtnText: {
    color: '#5C4A28',
    fontWeight: 'bold',
    fontSize: 12,
  },
  container: {
    flex: 1,
    backgroundColor: '#FBF9F5',
  },
  contentContainer: {
    padding: 12,
    paddingBottom: 28,
  },
  // Gold Cards
  goldCard: {
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#E8DCB8',
    borderRadius: 8,
    marginBottom: 12,
    overflow: 'hidden',
    shadowColor: '#A38035',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.08,
    shadowRadius: 3,
    elevation: 2,
  },
  cardHeaderBanner: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    backgroundColor: '#F5E7CA',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    paddingVertical: 7,
    paddingHorizontal: 10,
  },
  headerIconBadge: {
    width: 22,
    height: 22,
    borderRadius: 11,
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
  counterBadge: {
    backgroundColor: '#FFFFFF',
    paddingHorizontal: 8,
    paddingVertical: 2,
    borderRadius: 10,
    borderWidth: 1,
    borderColor: '#E2D3B3',
  },
  counterBadgeText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#9E6D08',
  },
  cardBody: {
    padding: 10,
  },
  row: {
    flexDirection: 'row',
    alignItems: 'center',
    marginBottom: 7,
    flexWrap: 'wrap',
  },
  label: {
    fontSize: 11,
    fontWeight: 'bold',
    width: 96,
    marginRight: 6,
    color: '#3A2E19',
  },
  labelRight: {
    fontSize: 11,
    fontWeight: 'bold',
    textAlign: 'right',
    marginRight: 8,
    color: '#3A2E19',
  },
  inputSubLabel: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3A2E19',
    marginBottom: 3,
  },
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
  radioContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    marginVertical: 2,
    marginRight: 8,
  },
  checkboxSquare: {
    width: 15,
    height: 15,
    borderWidth: 1.5,
    borderColor: '#B8903B',
    justifyContent: 'center',
    alignItems: 'center',
    marginRight: 5,
    borderRadius: 3,
    backgroundColor: '#FFFFFF',
  },
  checkboxSquareActive: {
    backgroundColor: '#B8903B',
  },
  radioLabel: {
    fontSize: 11,
    color: '#362B1A',
    fontWeight: 'bold',
  },
  // Table
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
    backgroundColor: '#FFFFFF',
  },
  tableRowSelected: {
    backgroundColor: '#FFF6DF',
    borderLeftWidth: 3,
    borderLeftColor: '#B8903B',
  },
  tableCellText: {
    fontSize: 11,
    color: '#332717',
    paddingHorizontal: 4,
  },
  emptyTableState: {
    paddingVertical: 24,
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
  // Action Buttons for Item Add Form
  actionButtonRow: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    marginTop: 10,
    paddingTop: 8,
    borderTopWidth: 1,
    borderTopColor: '#F2ECE0',
  },
  clearMiniBtn: {
    paddingVertical: 5,
    paddingHorizontal: 12,
    borderRadius: 5,
    borderWidth: 1,
    borderColor: '#DFD5C3',
    backgroundColor: '#FFFFFF',
  },
  clearMiniBtnText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#5C4A28',
  },
  deleteMiniBtn: {
    paddingVertical: 5,
    paddingHorizontal: 12,
    borderRadius: 5,
    borderWidth: 1,
    borderColor: '#F2A8A2',
    backgroundColor: '#FFFFFF',
  },
  deleteMiniBtnText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#D93829',
  },
  addMiniBtn: {
    paddingVertical: 5,
    paddingHorizontal: 16,
    borderRadius: 5,
    borderWidth: 1,
    borderColor: '#B8903B',
    backgroundColor: '#F9F2E2',
  },
  addMiniBtnText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#9E6D08',
  },
  // Modal
  modalOverlay: {
    flex: 1,
    backgroundColor: 'rgba(0,0,0,0.5)',
    justifyContent: 'center',
    alignItems: 'center',
    padding: 16,
  },
  modalContainer: {
    width: '90%',
    maxWidth: 620,
    maxHeight: '80%',
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#E8DCB8',
    borderRadius: 8,
    overflow: 'hidden',
  },
  modalHeader: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    backgroundColor: '#F5E7CA',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    paddingHorizontal: 12,
    paddingVertical: 10,
  },
  modalTitle: {
    fontSize: 14,
    fontWeight: 'bold',
    color: '#3B2D17',
  },
  modalCloseIconBtn: {
    padding: 4,
  },
  modalSearchInput: {
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderRadius: 6,
    paddingHorizontal: 10,
    paddingVertical: 5,
    fontSize: 12,
    height: 32,
    backgroundColor: '#FFFFFF',
    color: '#2C2519',
    marginBottom: 10,
  },
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
    color: '#332717',
  },
  modalFooter: {
    flexDirection: 'row',
    justifyContent: 'flex-end',
    paddingHorizontal: 12,
    paddingVertical: 8,
    borderTopWidth: 1,
    borderTopColor: '#F0E6D2',
    backgroundColor: '#FCFAF6',
  },
  modalCloseBtn: {
    paddingVertical: 5,
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
  // Toast
  toast: {
    position: 'absolute',
    top: 40,
    alignSelf: 'center',
    paddingHorizontal: 20,
    paddingVertical: 10,
    borderRadius: 8,
    elevation: 5,
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
});
