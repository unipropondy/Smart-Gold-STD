import { ThemedText } from '@/components/themed-text';
import { API_BASE_URL } from '@/constants/api';
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
  View,
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';

const API_URL = `${API_BASE_URL}/tradeinsplit`;

export default function TradeInSplitScreen() {
  const themeColors = useThemeColors();
  const router = useRouter();

  // Form States - Source
  const [inventoryCode, setInventoryCode] = useState('');
  const [weight, setWeight] = useState('0.000');
  const [stWeight, setStWeight] = useState('0.000');
  const [totalCost, setTotalCost] = useState('0.00');
  const [originalInvWeight, setOriginalInvWeight] = useState('');

  // Form States - Split Partition
  const [splitWeight, setSplitWeight] = useState('0.000');
  const [splitInvCode, setSplitInvCode] = useState('');
  const [splitStWeight, setSplitStWeight] = useState('0.000');
  const [splitInvWeight, setSplitInvWeight] = useState('');
  const [isScrap, setIsScrap] = useState(false);

  // UI States
  const [loading, setLoading] = useState(false);
  const [saving, setSaving] = useState(false);
  const [lovVisible, setLovVisible] = useState(false);
  const [lovData, setLovData] = useState<any[]>([]);
  const [lovSearchQuery, setLovSearchQuery] = useState('');
  const [toast, setToast] = useState<{ message: string; type: 'success' | 'error' } | null>(null);

  const showToast = (message: string, type: 'success' | 'error' = 'success') => {
    setToast({ message, type });
    setTimeout(() => setToast(null), 3000);
  };

  const fetchLOVData = async () => {
    try {
      setLoading(true);
      const res = await fetch(`${API_URL}/search`);
      const data = await res.json();
      if (data.success) {
        setLovData(data.data || []);
        setLovSearchQuery('');
        setLovVisible(true);
      } else {
        showToast('Error fetching trade-in inventory', 'error');
      }
    } catch (err: any) {
      showToast('Network error loading trade-in items', 'error');
    } finally {
      setLoading(false);
    }
  };

  const handleSelectInventory = (item: any) => {
    setInventoryCode(item.InventoryCode || '');
    setWeight(item.Weight ? parseFloat(item.Weight).toFixed(3) : '0.000');
    setStWeight(item.StoneWeight ? parseFloat(item.StoneWeight).toFixed(3) : '0.000');
    setTotalCost(item.TotalCost ? parseFloat(item.TotalCost).toFixed(2) : '0.00');
    setSplitWeight('0.000');
    setSplitInvCode('');
    setOriginalInvWeight('');
    setSplitInvWeight('');
    setSplitStWeight('0.000');
    setLovVisible(false);
  };

  const lookupInventoryByCode = async (code: string) => {
    if (!code.trim()) return;
    try {
      const res = await fetch(`${API_URL}/search`);
      const data = await res.json();
      if (data.success && Array.isArray(data.data)) {
        const found = data.data.find(
          (i: any) => (i.InventoryCode || '').trim().toLowerCase() === code.trim().toLowerCase()
        );
        if (found) {
          handleSelectInventory(found);
        }
      }
    } catch (err) {
      console.error(err);
    }
  };

  const handleSplit = async () => {
    if (!inventoryCode.trim()) {
      showToast('Please select an Inventory Code first.', 'error');
      return;
    }

    const sw = parseFloat(splitWeight);
    const swSt = parseFloat(splitStWeight);
    const w = parseFloat(weight);

    if (isNaN(sw) || sw <= 0) {
      showToast('Split weight should be greater than Zero.', 'error');
      return;
    }
    if (sw < swSt) {
      showToast('Split weight should not be less than Stone Weight.', 'error');
      return;
    }
    if (sw >= w) {
      showToast('Split Weight should be less than Inventory Weight.', 'error');
      return;
    }

    try {
      setLoading(true);
      const res = await fetch(`${API_URL}/nextCode`);
      const data = await res.json();
      if (data.success) {
        setSplitInvCode(data.data);

        if (!isScrap) {
          const w1 = w - sw;
          setOriginalInvWeight(w1.toFixed(3));
          setSplitInvWeight(sw.toFixed(3));
        } else {
          setOriginalInvWeight(w.toFixed(3));
          setSplitInvWeight(sw.toFixed(3));
          setSplitStWeight(sw.toFixed(3));
        }
        showToast(`Partition generated: ${data.data}`, 'success');
      } else {
        showToast('Error generating code: ' + data.message, 'error');
      }
    } catch (err: any) {
      showToast('Network error generating partition code', 'error');
    } finally {
      setLoading(false);
    }
  };

  const handleUpdate = async () => {
    if (!splitInvCode.trim()) {
      showToast('The split Inventory Code should not be blank. Click Split first.', 'error');
      return;
    }
    if (
      !originalInvWeight ||
      parseFloat(originalInvWeight) === 0 ||
      !splitInvWeight ||
      parseFloat(splitInvWeight) === 0
    ) {
      showToast('Invalid partition weights calculated.', 'error');
      return;
    }

    try {
      setSaving(true);
      const payload = {
        inventoryCode: inventoryCode.trim(),
        weight,
        stWeight,
        totalCost,
        originalInvWeight,
        splitInvCode: splitInvCode.trim(),
        splitInvWeight,
        splitStWeight,
        splitWeight,
        isScrap,
        userId: 'admin',
      };

      const res = await fetch(`${API_URL}/save`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload),
      });
      const data = await res.json();
      if (data.success) {
        showToast('Trade-In Split Successfully', 'success');
        handleClear();
      } else {
        showToast(data.message || 'Failed to execute split', 'error');
      }
    } catch (err: any) {
      showToast('Network error while saving split', 'error');
    } finally {
      setSaving(false);
    }
  };

  const handleClear = () => {
    setInventoryCode('');
    setWeight('0.000');
    setStWeight('0.000');
    setOriginalInvWeight('');
    setSplitInvWeight('');
    setSplitWeight('0.000');
    setSplitInvCode('');
    setSplitStWeight('0.000');
    setIsScrap(false);
    setTotalCost('0.00');
  };

  const toggleScrap = () => {
    const val = !isScrap;
    setIsScrap(val);
    if (val) {
      setSplitWeight(stWeight);
    } else {
      setSplitWeight('0.000');
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
          <View>
            <ThemedText style={styles.headerTitle}>Trade In Inventory Split</ThemedText>
            <ThemedText style={styles.headerSubtitle}>Old Gold Lot Deconstruction & Scrap Partitioning</ThemedText>
          </View>
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
            <ThemedText style={styles.clearBtnText}>Clear</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity
            style={[styles.actionBtn, styles.saveBtn]}
            onPress={handleUpdate}
            activeOpacity={0.8}
            disabled={saving || loading || !splitInvCode}
          >
            {saving ? (
              <ActivityIndicator size="small" color="#9E6D08" />
            ) : (
              <>
                <Ionicons name="checkmark-done-outline" size={15} color="#9E6D08" />
                <ThemedText style={styles.saveBtnText}>Save Split</ThemedText>
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
          {/* CARD 1: SOURCE INVENTORY LOT */}
          <View style={styles.goldCard}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="cube-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitleSmall}>Source Lot Specification</ThemedText>
              </View>

              {inventoryCode ? (
                <View style={styles.sourceBadge}>
                  <ThemedText style={styles.sourceBadgeLabel}>Source Lot:</ThemedText>
                  <ThemedText style={styles.sourceBadgeValue}>{inventoryCode}</ThemedText>
                </View>
              ) : null}
            </View>

            <View style={styles.cardBody}>
              {/* Single unified row for all 4 fields */}
              <View style={styles.fourColumnGrid}>
                {/* Inventory Code */}
                <View style={[styles.inlineFieldRow, { flex: 1.3, minWidth: 220 }]}>
                  <ThemedText style={[styles.inlineFieldLabel, { width: 105 }]}>Inventory Code *</ThemedText>
                  <View style={styles.inputWithLookup}>
                    <TextInput
                      style={[styles.formInput, styles.inputAttached, { flex: 1 }]}
                      value={inventoryCode}
                      onChangeText={setInventoryCode}
                      onBlur={() => lookupInventoryByCode(inventoryCode)}
                      placeholder="Select/Enter Code..."
                      placeholderTextColor="#9E8D77"
                    />
                    <TouchableOpacity
                      style={styles.attachedLookupBtn}
                      onPress={fetchLOVData}
                      activeOpacity={0.7}
                    >
                      <Ionicons name="search" size={13} color="#7D5E19" />
                    </TouchableOpacity>
                  </View>
                </View>

                {/* Total Gross Weight */}
                <View style={[styles.inlineFieldRow, { flex: 1, minWidth: 160 }]}>
                  <ThemedText style={[styles.inlineFieldLabel, { width: 95 }]}>Gross Weight (g)</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1, textAlign: 'right' }]}
                    value={weight}
                    editable={false}
                  />
                </View>

                {/* Stone Weight */}
                <View style={[styles.inlineFieldRow, { flex: 1, minWidth: 150 }]}>
                  <ThemedText style={[styles.inlineFieldLabel, { width: 80 }]}>Stone Wt (g)</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1, textAlign: 'right' }]}
                    value={stWeight}
                    editable={false}
                  />
                </View>

                {/* Total Cost */}
                <View style={[styles.inlineFieldRow, { flex: 1, minWidth: 150 }]}>
                  <ThemedText style={[styles.inlineFieldLabel, { width: 85 }]}>Total Cost ($)</ThemedText>
                  <TextInput
                    style={[
                      styles.formInput,
                      styles.readonlyInput,
                      styles.highlightTotal,
                      { flex: 1, textAlign: 'right' },
                    ]}
                    value={totalCost}
                    editable={false}
                  />
                </View>
              </View>
            </View>
          </View>

          {/* CARD 2: PARTITION & SPLIT CONFIGURATION */}
          <View style={[styles.goldCard, { marginTop: 10 }]}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="cut-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitleSmall}>Partition & Weight Allocation</ThemedText>
              </View>

              <TouchableOpacity
                style={styles.scrapPill}
                onPress={toggleScrap}
                activeOpacity={0.7}
              >
                <Ionicons
                  name={isScrap ? 'checkbox' : 'square-outline'}
                  size={15}
                  color={isScrap ? '#9E6D08' : '#8D6409'}
                />
                <ThemedText style={[styles.scrapPillText, isScrap && styles.scrapPillTextActive]}>
                  Scrap / Melt Out
                </ThemedText>
              </TouchableOpacity>
            </View>

            <View style={styles.cardBody}>
              {/* Row 1: Split Weight Input, Split Stone Weight, Generate Split Button */}
              <View style={styles.threeColumnGrid}>
                {/* Split Weight Input */}
                <View style={[styles.inlineFieldRow, { flex: 1.2, minWidth: 260 }]}>
                  <ThemedText style={styles.inlineFieldLabel}>Split Weight (g) *</ThemedText>
                  <TextInput
                    style={[styles.formInput, { flex: 1, textAlign: 'right' }]}
                    value={splitWeight}
                    onChangeText={setSplitWeight}
                    keyboardType="numeric"
                    editable={!isScrap}
                    placeholder="0.000"
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* Split Stone Weight */}
                <View style={[styles.inlineFieldRow, { flex: 1, minWidth: 220 }]}>
                  <ThemedText style={styles.inlineFieldLabel}>Split Stone (g)</ThemedText>
                  <TextInput
                    style={[styles.formInput, { flex: 1, textAlign: 'right' }]}
                    value={splitStWeight}
                    onChangeText={setSplitStWeight}
                    keyboardType="numeric"
                    placeholder="0.000"
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* Action Trigger Button */}
                <View style={[styles.inlineFieldRow, { flex: 0.8, minWidth: 160 }]}>
                  <TouchableOpacity
                    style={[styles.splitTriggerBtn, loading && { opacity: 0.7 }]}
                    onPress={handleSplit}
                    activeOpacity={0.8}
                    disabled={loading}
                  >
                    <Ionicons name="git-branch-outline" size={14} color="#9E6D08" style={{ marginRight: 4 }} />
                    <ThemedText style={styles.splitTriggerBtnText}>Calculate Split</ThemedText>
                  </TouchableOpacity>
                </View>
              </View>

              {/* Row 2: Generated Split Code & Post-Split Balance Weights */}
              <View style={[styles.threeColumnGrid, { marginTop: 8 }]}>
                {/* Generated Split Inv Code */}
                <View style={[styles.inlineFieldRow, { flex: 1.2, minWidth: 260 }]}>
                  <ThemedText style={styles.inlineFieldLabel}>New Split Code</ThemedText>
                  <TextInput
                    style={[
                      styles.formInput,
                      styles.readonlyInput,
                      styles.codeCell,
                      { flex: 1, fontWeight: 'bold' },
                    ]}
                    value={splitInvCode}
                    editable={false}
                    placeholder="Auto Generated"
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* Remaining Original Inv Weight */}
                <View style={[styles.inlineFieldRow, { flex: 1, minWidth: 220 }]}>
                  <ThemedText style={styles.inlineFieldLabel}>Remaining Wt (g)</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1, textAlign: 'right' }]}
                    value={originalInvWeight}
                    editable={false}
                    placeholder="Calculated remainder"
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* Split Inventory Weight */}
                <View style={[styles.inlineFieldRow, { flex: 1, minWidth: 220 }]}>
                  <ThemedText style={styles.inlineFieldLabel}>Allocated Wt (g)</ThemedText>
                  <TextInput
                    style={[
                      styles.formInput,
                      styles.readonlyInput,
                      styles.highlightTotal,
                      { flex: 1, textAlign: 'right' },
                    ]}
                    value={splitInvWeight}
                    editable={false}
                    placeholder="Calculated target"
                    placeholderTextColor="#9E8D77"
                  />
                </View>
              </View>
            </View>
          </View>
        </ScrollView>
      </KeyboardAvoidingView>

      {/* TRADE-IN INVENTORY SEARCH MODAL */}
      <Modal visible={lovVisible} transparent animationType="fade" onRequestClose={() => setLovVisible(false)}>
        <View style={styles.modalOverlay}>
          <View style={styles.lovModalCard}>
            <View style={styles.modalHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="cube-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.modalHeaderTitle}>Search Trade-In Inventory Lot</ThemedText>
              </View>
              <TouchableOpacity
                onPress={() => setLovVisible(false)}
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
                value={lovSearchQuery}
                onChangeText={setLovSearchQuery}
                placeholder="Search by Inventory Code..."
                placeholderTextColor="#9E8D77"
                autoFocus
              />
              {lovSearchQuery.length > 0 && (
                <TouchableOpacity onPress={() => setLovSearchQuery('')}>
                  <Ionicons name="close-circle" size={16} color="#9E8D77" />
                </TouchableOpacity>
              )}
            </View>

            <View style={styles.lovHeadRow}>
              <ThemedText style={[styles.lovHeadText, { flex: 1.8 }]}>Inventory Code</ThemedText>
              <ThemedText style={[styles.lovHeadText, { flex: 1.2, textAlign: 'right' }]}>Weight (g)</ThemedText>
              <ThemedText style={[styles.lovHeadText, { flex: 1.2, textAlign: 'right' }]}>Stone (g)</ThemedText>
              <ThemedText style={[styles.lovHeadText, { flex: 1.4, textAlign: 'right' }]}>Cost ($)</ThemedText>
            </View>

            <FlatList
              data={lovData.filter((item) =>
                (item.InventoryCode || '').toLowerCase().includes(lovSearchQuery.toLowerCase())
              )}
              keyExtractor={(item, index) => item.InventoryCode || index.toString()}
              initialNumToRender={50}
              maxToRenderPerBatch={50}
              ListEmptyComponent={
                <View style={styles.emptyTableContainer}>
                  <Ionicons name="search-outline" size={24} color="#D4C3A3" />
                  <ThemedText style={styles.emptyTableText}>No trade-in inventory items found.</ThemedText>
                </View>
              }
              renderItem={({ item }) => (
                <TouchableOpacity
                  style={styles.lovItemRow}
                  onPress={() => handleSelectInventory(item)}
                  activeOpacity={0.7}
                >
                  <ThemedText style={[styles.lovItemText, styles.codeCell, { flex: 1.8 }]}>
                    {item.InventoryCode}
                  </ThemedText>
                  <ThemedText style={[styles.lovItemText, { flex: 1.2, textAlign: 'right', fontWeight: 'bold' }]}>
                    {parseFloat(item.Weight || 0).toFixed(3)}
                  </ThemedText>
                  <ThemedText style={[styles.lovItemText, { flex: 1.2, textAlign: 'right', color: '#7A6855' }]}>
                    {parseFloat(item.StoneWeight || 0).toFixed(3)}
                  </ThemedText>
                  <ThemedText style={[styles.lovItemText, styles.amountCell, { flex: 1.4, textAlign: 'right' }]}>
                    {parseFloat(item.TotalCost || 0).toFixed(2)}
                  </ThemedText>
                </TouchableOpacity>
              )}
            />
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
  sourceBadge: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFF4DA',
    paddingHorizontal: 8,
    paddingVertical: 2,
    borderRadius: 4,
    borderWidth: 1,
    borderColor: '#B8903B',
  },
  sourceBadgeLabel: {
    fontSize: 10,
    color: '#7D5E19',
    marginRight: 4,
  },
  sourceBadgeValue: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#8A5D00',
  },

  // Scrap pill toggle
  scrapPill: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#E2D3B3',
    borderRadius: 4,
    paddingVertical: 3,
    paddingHorizontal: 8,
    gap: 4,
  },
  scrapPillText: {
    fontSize: 11,
    fontWeight: '500',
    color: '#5C4A28',
  },
  scrapPillTextActive: {
    fontWeight: 'bold',
    color: '#9E6D08',
  },

  // ===== GRIDS & INLINE ROWS =====
  threeColumnGrid: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    gap: 8,
  },
  fourColumnGrid: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    gap: 8,
    alignItems: 'center',
  },
  inlineFieldRow: {
    flex: 1,
    minWidth: 220,
    flexDirection: 'row',
    alignItems: 'center',
  },
  inlineFieldLabel: {
    width: 110,
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3B2D17',
    marginRight: 4,
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

  // Input Attached Lookup
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

  // Split Trigger Action Button
  splitTriggerBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    backgroundColor: '#FFF1D6',
    borderWidth: 1,
    borderColor: '#B8903B',
    borderRadius: 4,
    height: 26,
    paddingHorizontal: 12,
    width: '100%',
  },
  splitTriggerBtnText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#9E6D08',
  },

  // Code & Amount Cells
  codeCell: {
    fontFamily: Platform.OS === 'ios' ? 'Courier' : 'monospace',
    fontWeight: 'bold',
    color: '#8A5D00',
  },
  amountCell: {
    fontWeight: 'bold',
    color: '#1B5E20',
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
