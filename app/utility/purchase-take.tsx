import { Ionicons } from '@expo/vector-icons';
import { Stack, useRouter } from 'expo-router';
import React, { useEffect, useMemo, useRef, useState } from 'react';
import {
  ActivityIndicator,
  Modal,
  Platform,
  ScrollView,
  StyleSheet,
  TextInput,
  TouchableOpacity,
  View,
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';

import { ThemedText } from '@/components/themed-text';
import { useThemeContext } from '@/contexts/ThemeContext';
import { useThemeColors } from '@/hooks/useThemeColors';

import { API_BASE_URL } from '@/constants/api';

interface PurchaseItem {
  trayId: string;
  inventoryCode: string;
  catalogueCode?: string;
  description: string;
  weight: number;
  costPerGram: number;
  totalCost: number;
  categoryCode?: string;
  pcs: number;
  scanned?: boolean;
}

interface CategoryOption {
  categoryCode: string;
  categoryName: string;
}

interface SummaryData {
  totalWeight: string;
  totalPcs: number;
  scannedWeight: string;
  scannedPcs: number;
  unscannedWeight: string;
  unscannedPcs: number;
}

export default function PurchaseTakeScreen() {
  const router = useRouter();
  const themeColors = useThemeColors();
  const { accentColor } = useThemeContext();
  const { background: backgroundColor, card: cardColor, text: textColor, border: borderColor, subtitle: subtitleColor } = themeColors;

  // Table & Summary State
  const [purchaseList, setPurchaseList] = useState<PurchaseItem[]>([]);
  const [summary, setSummary] = useState<SummaryData>({
    totalWeight: '0.00',
    totalPcs: 0,
    scannedWeight: '0.00',
    scannedPcs: 0,
    unscannedWeight: '0.00',
    unscannedPcs: 0,
  });

  // Toolbar Search, Filter & Sort State
  const [searchQuery, setSearchQuery] = useState('');
  const [filterCategory, setFilterCategory] = useState<string>('ALL');
  const [isFilterCategoryOpen, setIsFilterCategoryOpen] = useState(false);

  const [sortField, setSortField] = useState<'trayId' | 'inventoryCode' | 'description' | 'weight' | 'costPerGram' | 'pcs' | 'totalCost'>('trayId');
  const [sortDirection, setSortDirection] = useState<'asc' | 'desc'>('asc');
  const [isSortDropdownOpen, setIsSortDropdownOpen] = useState(false);

  // Setup / Download Modal State
  const [isSetupModalOpen, setIsSetupModalOpen] = useState(false);
  const [passwordInput, setPasswordInput] = useState('');
  const [userRole, setUserRole] = useState('');
  const [userName, setUserName] = useState('');
  const [isPasswordValidated, setIsPasswordValidated] = useState(false);
  const [passwordError, setPasswordError] = useState<string | null>(null);

  const [categories, setCategories] = useState<CategoryOption[]>([]);
  const [selectedCategory, setSelectedCategory] = useState<string>('ALL');
  const [isCategoryDropdownOpen, setIsCategoryDropdownOpen] = useState(false);

  const filteredCategories = useMemo(() => {
    if (!selectedCategory || !selectedCategory.trim() || selectedCategory.trim().toUpperCase() === 'ALL') {
      return categories;
    }
    const q = selectedCategory.toLowerCase().trim();
    return categories.filter(
      (c) =>
        c.categoryCode.toLowerCase().includes(q) ||
        c.categoryName.toLowerCase().includes(q)
    );
  }, [categories, selectedCategory]);

  // Scan Input State
  const [trayInput, setTrayInput] = useState('');
  const [inventoryCodeInput, setInventoryCodeInput] = useState('');
  const [isPrepared, setIsPrepared] = useState(false);

  const [isLoading, setIsLoading] = useState(false);
  const [isSaving, setIsSaving] = useState(false);
  const [statusMessage, setStatusMessage] = useState<{ type: 'error' | 'success' | 'info'; text: string } | null>(null);

  const trayInputRef = useRef<TextInput>(null);
  const inventoryInputRef = useRef<TextInput>(null);

  // Auto-dismiss status notification banner after 3.5 seconds
  useEffect(() => {
    if (statusMessage) {
      const timer = setTimeout(() => {
        setStatusMessage(null);
      }, 3500);
      return () => clearTimeout(timer);
    }
  }, [statusMessage]);

  // Fetch categories & default salesman role
  const fetchCategories = async () => {
    try {
      const response = await fetch(`${API_BASE_URL}/purchasetake/categories`);
      const result = await response.json();
      if (result.success) {
        if (Array.isArray(result.categories)) {
          setCategories(result.categories);
        }
        if (result.defaultUserRole) setUserRole(result.defaultUserRole);
        if (result.defaultUserName) setUserName(result.defaultUserName);
      }
    } catch (err) {
      console.error('Fetch Categories Error:', err);
    }
  };

  const handleOpenSetup = () => {
    setPasswordInput('');
    setUserRole('');
    setUserName('');
    setIsPasswordValidated(false);
    setPasswordError(null);
    setSelectedCategory('ALL');
    fetchCategories();
    setIsSetupModalOpen(true);
  };

  const handleValidatePassword = async (): Promise<boolean> => {
    if (!passwordInput.trim()) {
      setPasswordError('Password is required');
      setIsPasswordValidated(false);
      setUserRole('');
      setUserName('');
      return false;
    }
    setPasswordError(null);
    try {
      const response = await fetch(`${API_BASE_URL}/purchasetake/validate-password`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ password: passwordInput.trim() }),
      });
      const result = await response.json();
      if (response.ok && result.success) {
        const name = result.salesManName || result.salesManCode || '';
        setIsPasswordValidated(true);
        setUserRole(name);
        setUserName(name);
        setPasswordError(null);
        return true;
      } else {
        setIsPasswordValidated(false);
        setUserRole('');
        setUserName('');
        setPasswordError(result.message || 'Invalid Password.');
        return false;
      }
    } catch (err: any) {
      setIsPasswordValidated(false);
      setUserRole('');
      setUserName('');
      setPasswordError('Unable to connect to server.');
      return false;
    }
  };

  // State for Purchase Take Already Started dialog
  const [showAlreadyStartedModal, setShowAlreadyStartedModal] = useState(false);

  const executeProceed = async (forceNew?: boolean) => {
    setIsLoading(true);
    setStatusMessage(null);
    try {
      const bodyPayload: any = {
        password: passwordInput.trim(),
        categoryCode: selectedCategory,
      };
      if (forceNew !== undefined) {
        bodyPayload.forceNew = forceNew;
      }

      const response = await fetch(`${API_BASE_URL}/purchasetake/proceed`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(bodyPayload),
      });
      const result = await response.json();
      if (result.success) {
        if (result.alreadyStarted && forceNew === undefined) {
          if (result.summary) setSummary(result.summary);
          if (result.scannedList && Array.isArray(result.scannedList)) setPurchaseList(result.scannedList);
          if (result.userRole) setUserRole(result.userRole);
          if (result.userName) setUserName(result.userName);
          setShowAlreadyStartedModal(true);
          return;
        }

        setIsPrepared(true);
        if (result.summary) setSummary(result.summary);
        if (result.userRole) setUserRole(result.userRole);
        if (result.userName) setUserName(result.userName);

        if (forceNew === true) {
          setPurchaseList([]);
        } else if (result.scannedList && Array.isArray(result.scannedList)) {
          setPurchaseList(result.scannedList);
        }

        setShowAlreadyStartedModal(false);
        setIsSetupModalOpen(false);
        setStatusMessage({ type: 'success', text: 'Purchase initialized! Ready for scanning.' });
      } else {
        setStatusMessage({ type: 'error', text: result.message || 'Failed to proceed Purchase.' });
      }
    } catch (err) {
      setStatusMessage({ type: 'error', text: 'Error connecting to server for Proceed Purchase.' });
    } finally {
      setIsLoading(false);
    }
  };

  const handleProceedPurchase = async () => {
    if (!passwordInput.trim()) {
      setPasswordError('Please verify password first.');
      return;
    }
    if (!isPasswordValidated) {
      const isValid = await handleValidatePassword();
      if (!isValid) return;
    }
    await executeProceed();
  };

  // Perform Purchase Scan
  const handleScanSearch = async () => {
    if (!trayInput.trim() || !inventoryCodeInput.trim()) {
      setStatusMessage({ type: 'error', text: 'Please enter both Tray ID and Inventory Code.' });
      return;
    }

    setIsLoading(true);
    setStatusMessage(null);

    try {
      const response = await fetch(`${API_BASE_URL}/purchasetake/scan`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          trayId: trayInput.trim(),
          inventoryCode: inventoryCodeInput.trim(),
        }),
      });
      const result = await response.json();

      if (result.scannedList && Array.isArray(result.scannedList)) {
        setPurchaseList(result.scannedList);
      }
      if (result.summary) setSummary(result.summary);

      if (result.success) {
        if (result.alreadyScanned) {
          setStatusMessage({ type: 'info', text: result.message || 'Item already scanned.' });
        } else {
          setStatusMessage({ type: 'success', text: result.message || `Item ${inventoryCodeInput.trim()} scanned successfully!` });
        }
        setInventoryCodeInput('');
        inventoryInputRef.current?.focus();
      } else {
        setStatusMessage({ type: 'error', text: result.message || 'Matching purchase item not found.' });
      }
    } catch (err) {
      setStatusMessage({ type: 'error', text: 'Network/server error while scanning purchase.' });
    } finally {
      setIsLoading(false);
    }
  };

  // Edit Item Modal
  const [editingItem, setEditingItem] = useState<any>(null);
  const [editTrayId, setEditTrayId] = useState('');
  const [editWeight, setEditWeight] = useState('');
  const [editPcs, setEditPcs] = useState('');
  const [isEditModalOpen, setIsEditModalOpen] = useState(false);

  const handleOpenEditModal = (item: any) => {
    if (!item) return;
    setEditingItem(item);
    setEditTrayId(item.trayId || '');
    setEditWeight(String(item.weight != null ? item.weight : ''));
    setEditPcs(String(item.pcs != null ? item.pcs : '1'));
    setIsEditModalOpen(true);
  };

  const handleSaveItemEdit = async () => {
    if (!editingItem || !editingItem.inventoryCode) return;
    setIsLoading(true);
    try {
      const response = await fetch(`${API_BASE_URL}/purchasetake/update-item`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          inventoryCode: editingItem.inventoryCode,
          trayId: editTrayId.trim(),
          weight: editWeight.trim(),
          pcs: editPcs.trim(),
          currentViewTrayId: trayInput.trim(),
        }),
      });
      const result = await response.json();
      if (response.ok && result.success) {
        const updatedTrayId = result.updatedItem && result.updatedItem.trayId ? result.updatedItem.trayId : editTrayId.trim();
        setTrayInput(updatedTrayId);
        if (result.scannedList && Array.isArray(result.scannedList)) setPurchaseList(result.scannedList);
        if (result.summary) setSummary(result.summary);

        setIsEditModalOpen(false);
        setEditingItem(null);
        setStatusMessage({ type: 'success', text: result.message || `Item ${editingItem.inventoryCode} updated successfully!` });
      } else {
        setStatusMessage({ type: 'error', text: result.message || 'Failed to update item.' });
      }
    } catch (err) {
      setStatusMessage({ type: 'error', text: 'Error connecting to server to update item.' });
    } finally {
      setIsLoading(false);
    }
  };

  // Save Purchase
  const handleSavePurchase = async () => {
    if (isSaving) return;
    if (!isPrepared) {
      setStatusMessage({ type: 'error', text: 'No active Purchase session to save. Please start a session first.' });
      return;
    }

    setIsSaving(true);
    setStatusMessage(null);

    try {
      const response = await fetch(`${API_BASE_URL}/purchasetake/save`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          trayId: trayInput.trim(),
        }),
      });
      const result = await response.json();

      if (response.ok && result.success) {
        setIsPrepared(false);
        setTrayInput('');
        setInventoryCodeInput('');
        setPurchaseList(result.scannedList && Array.isArray(result.scannedList) ? result.scannedList : []);
        if (result.summary) setSummary(result.summary);
        setStatusMessage({ type: 'success', text: result.message || 'Purchase saved to database successfully!' });
      } else {
        setStatusMessage({ type: 'error', text: result.message || 'Failed to save Purchase.' });
      }
    } catch (err: any) {
      setStatusMessage({ type: 'error', text: 'Network/server error while saving Purchase.' });
    } finally {
      setIsSaving(false);
    }
  };

  // List button handler
  const handleListPress = async () => {
    const trimmedTray = trayInput.trim();

    if (!trimmedTray) {
      setIsLoading(true);
      setStatusMessage(null);
      try {
        const response = await fetch(`${API_BASE_URL}/purchasetake/summary`);
        const result = await response.json();
        if (result.success) {
          setPurchaseList(result.scannedList && Array.isArray(result.scannedList) ? result.scannedList : []);
          if (result.summary) setSummary(result.summary);
          setStatusMessage({
            type: 'success',
            text: `Displaying all ${result.scannedList ? result.scannedList.length : 0} scanned items.`,
          });
        }
      } catch (err) {
        setStatusMessage({ type: 'error', text: 'Failed to fetch scanned items list.' });
      } finally {
        setIsLoading(false);
      }
      return;
    }

    setIsLoading(true);
    setStatusMessage(null);
    try {
      const response = await fetch(`${API_BASE_URL}/purchasetake/list?trayId=${encodeURIComponent(trimmedTray)}`);
      const result = await response.json();
      if (result.success) {
        setPurchaseList(result.scannedList && Array.isArray(result.scannedList) ? result.scannedList : []);
        if (result.summary) setSummary(result.summary);
        setStatusMessage({
          type: 'success',
          text: result.message || `Displaying ${result.scannedList.length} items for Tray ${trimmedTray}.`,
        });
      } else {
        setPurchaseList([]);
        setStatusMessage({ type: 'error', text: result.message || 'Tray Not Found!' });
      }
    } catch (err) {
      setStatusMessage({ type: 'error', text: 'Failed to fetch purchase list for specified Tray ID.' });
    } finally {
      setIsLoading(false);
    }
  };

  const fetchInitialSummary = async () => {
    try {
      const response = await fetch(`${API_BASE_URL}/purchasetake/summary`);
      const result = await response.json();
      if (result.success) {
        if (result.isPrepared !== undefined) setIsPrepared(result.isPrepared);
        if (result.isPrepared) {
          if (result.summary) setSummary(result.summary);
          if (result.scannedList) setPurchaseList(result.scannedList);
        }
      }
    } catch (err) {
      console.error('Fetch Initial Summary Error:', err);
    }
  };

  useEffect(() => {
    fetchCategories();
    fetchInitialSummary();
  }, []);

  const handleClose = () => {
    if (router.canGoBack()) {
      router.back();
    } else {
      router.push('/submenu/inventory_submenu' as any);
    }
  };

  // Header Sort Toggle
  const handleHeaderSortToggle = (field: typeof sortField) => {
    if (sortField === field) {
      setSortDirection((prev) => (prev === 'asc' ? 'desc' : 'asc'));
    } else {
      setSortField(field);
      setSortDirection('asc');
    }
  };

  const handleClearToolbar = () => {
    setSearchQuery('');
    setFilterCategory('ALL');
    setSortField('trayId');
    setSortDirection('asc');
  };

  // Processed list based on Search, Filter & Sort
  const processedPurchaseList = useMemo(() => {
    let result = [...purchaseList];

    // 1. Search filter
    if (searchQuery.trim()) {
      const q = searchQuery.toLowerCase().trim();
      result = result.filter(
        (item) =>
          (item.trayId && item.trayId.toLowerCase().includes(q)) ||
          (item.inventoryCode && item.inventoryCode.toLowerCase().includes(q)) ||
          (item.description && item.description.toLowerCase().includes(q))
      );
    }

    // 2. Category filter
    if (filterCategory && filterCategory !== 'ALL') {
      result = result.filter((item) => item.categoryCode === filterCategory);
    }

    // 3. Sorting
    result.sort((a, b) => {
      let valA: any = a[sortField];
      let valB: any = b[sortField];

      if (valA == null) valA = '';
      if (valB == null) valB = '';

      if (typeof valA === 'number' && typeof valB === 'number') {
        return sortDirection === 'asc' ? valA - valB : valB - valA;
      }
      const strA = String(valA).toLowerCase();
      const strB = String(valB).toLowerCase();
      return sortDirection === 'asc' ? strA.localeCompare(strB) : strB.localeCompare(strA);
    });

    return result;
  }, [purchaseList, searchQuery, filterCategory, sortField, sortDirection]);

  // Bottom Footer Totals calculation
  const tableFooterTotals = useMemo(() => {
    let totalWeightNum = 0;
    let totalPcsNum = 0;
    let totalCostNum = 0;

    processedPurchaseList.forEach((item) => {
      totalWeightNum += Number(item.weight) || 0;
      totalPcsNum += Number(item.pcs) || 0;
      totalCostNum += Number(item.totalCost) || 0;
    });

    return {
      totalRecords: processedPurchaseList.length,
      totalWeight: totalWeightNum.toFixed(3),
      totalPcs: totalPcsNum,
      totalCost: totalCostNum.toLocaleString('en-IN', { minimumFractionDigits: 2, maximumFractionDigits: 2 }),
    };
  }, [processedPurchaseList]);

  return (
    <SafeAreaView style={[styles.safeArea, { backgroundColor: '#FBF9F5' }]} edges={['top', 'bottom']}>
      <Stack.Screen options={{ headerShown: false }} />

      <View style={styles.container}>
        {/* 1. Header Bar */}
        <View style={styles.headerBar}>
          <View style={styles.headerTitleGroup}>
            <TouchableOpacity style={styles.backBtn} onPress={handleClose}>
              <Ionicons name="arrow-back" size={18} color="#3B2D17" />
            </TouchableOpacity>
            <ThemedText style={styles.headerTitle}>Purchase Take</ThemedText>
          </View>

          <View style={styles.headerActions}>
            <TouchableOpacity style={styles.btnDownload} onPress={handleOpenSetup} disabled={isLoading}>
              <Ionicons name="cloud-download-outline" size={16} color="#FFFFFF" style={{ marginRight: 6 }} />
              <ThemedText style={styles.btnDownloadText}>Download Purchase Take</ThemedText>
            </TouchableOpacity>
          </View>
        </View>

        {/* Status Message Banner */}
        {statusMessage && (
          <View
            style={[
              styles.statusBanner,
              {
                backgroundColor:
                  statusMessage.type === 'error' ? '#FDF2F2' : statusMessage.type === 'success' ? '#F4F9F2' : '#FFFDF7',
                borderColor:
                  statusMessage.type === 'error' ? '#F3C5C5' : statusMessage.type === 'success' ? '#C2E5BD' : '#E8DCB8',
              },
            ]}
          >
            <Ionicons
              name={
                statusMessage.type === 'error'
                  ? 'alert-circle-outline'
                  : statusMessage.type === 'success'
                  ? 'checkmark-circle-outline'
                  : 'information-circle-outline'
              }
              size={18}
              color={statusMessage.type === 'error' ? '#B91C1C' : statusMessage.type === 'success' ? '#2E7D32' : '#9E6D08'}
              style={{ marginRight: 8 }}
            />
            <ThemedText
              style={{
                color: statusMessage.type === 'error' ? '#B91C1C' : statusMessage.type === 'success' ? '#2E7D32' : '#3B2D17',
                fontSize: 13,
                flex: 1,
              }}
            >
              {statusMessage.text}
            </ThemedText>
          </View>
        )}

        {/* Purchase Setup Inline Panel */}
        {isSetupModalOpen && (
          <View style={[styles.goldCard, { marginHorizontal: 16, marginTop: 8, zIndex: 1000 }]}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerIconBadge}>
                <Ionicons name="cloud-download-outline" size={14} color="#9E6D08" />
              </View>
              <ThemedText style={styles.cardHeaderTitle}>Download Purchase Take Setup</ThemedText>
              <TouchableOpacity onPress={() => setIsSetupModalOpen(false)} style={{ marginLeft: 'auto' }}>
                <Ionicons name="close" size={18} color="#3B2D17" />
              </TouchableOpacity>
            </View>

            <View style={{ padding: 14, flexDirection: 'row', flexWrap: 'wrap', gap: 12, alignItems: 'flex-start' }}>
              {/* Password Input & Verify */}
              <View style={{ flex: 1, minWidth: 240 }}>
                <ThemedText style={styles.inputLabel}>Password</ThemedText>
                <View style={{ flexDirection: 'row', gap: 8 }}>
                  <View style={[styles.inputBox, { flex: 1, height: 38 }]}>
                    <Ionicons name="key-outline" size={16} color="#8F7E66" style={{ marginRight: 8 }} />
                    <TextInput
                      style={styles.textInput}
                      placeholder="Enter Password"
                      placeholderTextColor="#A39682"
                      secureTextEntry
                      value={passwordInput}
                      onChangeText={(val) => {
                        setPasswordInput(val);
                        setIsPasswordValidated(false);
                        setPasswordError(null);
                      }}
                      onSubmitEditing={handleValidatePassword}
                    />
                  </View>
                  <TouchableOpacity
                    style={[
                      styles.btnSecondary,
                      {
                        borderColor: isPasswordValidated ? '#2E7D32' : '#9E6D08',
                        backgroundColor: isPasswordValidated ? '#F4F9F2' : '#FFFDF7',
                        height: 38,
                      },
                    ]}
                    onPress={handleValidatePassword}
                  >
                    <ThemedText style={{ fontSize: 12, fontWeight: '700', color: isPasswordValidated ? '#2E7D32' : '#9E6D08' }}>
                      {isPasswordValidated ? 'Verified ✓' : 'Verify'}
                    </ThemedText>
                  </TouchableOpacity>
                </View>
                {passwordError && (
                  <ThemedText style={{ color: '#B91C1C', fontSize: 12, marginTop: 4 }}>{passwordError}</ThemedText>
                )}
              </View>

              {/* User / Role Field */}
              <View style={{ flex: 1, minWidth: 200 }}>
                <ThemedText style={styles.inputLabel}>User / Role</ThemedText>
                <View style={[styles.inputBox, { backgroundColor: '#FAF7F0', height: 38 }]}>
                  <Ionicons name="person-outline" size={16} color="#8F7E66" style={{ marginRight: 8 }} />
                  <TextInput
                    style={[styles.textInput, { color: '#3B2D17', fontWeight: '600' }]}
                    value={userRole ? (userName && userName !== userRole ? `${userRole} (${userName})` : userRole) : (userName || '')}
                    editable={false}
                  />
                </View>
              </View>

              {/* Category Dropdown */}
              <View style={{ flex: 1, minWidth: 200, zIndex: 3000 }}>
                <ThemedText style={styles.inputLabel}>Category</ThemedText>
                <View style={[styles.inputBox, { justifyContent: 'space-between', height: 38 }]}>
                  <TextInput
                    style={[styles.textInput, { height: '100%' }]}
                    placeholder="ALL"
                    placeholderTextColor="#A39682"
                    value={selectedCategory}
                    onChangeText={(val) => {
                      setSelectedCategory(val);
                      setIsCategoryDropdownOpen(true);
                    }}
                    onFocus={() => setIsCategoryDropdownOpen(true)}
                  />
                  {selectedCategory ? (
                    <TouchableOpacity
                      onPress={() => {
                        setSelectedCategory('');
                        setIsCategoryDropdownOpen(true);
                      }}
                      style={{ padding: 4 }}
                    >
                      <Ionicons name="close-circle" size={16} color="#8F7E66" />
                    </TouchableOpacity>
                  ) : null}
                  <TouchableOpacity onPress={() => setIsCategoryDropdownOpen(!isCategoryDropdownOpen)} style={{ padding: 4 }}>
                    <Ionicons name={isCategoryDropdownOpen ? "chevron-up" : "chevron-down"} size={16} color="#8F7E66" />
                  </TouchableOpacity>
                </View>

                {isCategoryDropdownOpen && (
                  <View style={styles.dropdownMenu}>
                    <ScrollView nestedScrollEnabled keyboardShouldPersistTaps="handled">
                      <TouchableOpacity
                        style={[styles.dropdownItem, selectedCategory === 'ALL' && { backgroundColor: '#FDF7EB' }]}
                        onPress={() => {
                          setSelectedCategory('ALL');
                          setIsCategoryDropdownOpen(false);
                        }}
                      >
                        <ThemedText style={{ fontSize: 13, color: '#3B2D17', fontWeight: '700' }}>ALL</ThemedText>
                      </TouchableOpacity>
                      {filteredCategories.map((cat) => (
                        <TouchableOpacity
                          key={cat.categoryCode}
                          style={[styles.dropdownItem, selectedCategory === cat.categoryCode && { backgroundColor: '#FDF7EB' }]}
                          onPress={() => {
                            setSelectedCategory(cat.categoryCode);
                            setIsCategoryDropdownOpen(false);
                          }}
                        >
                          <ThemedText style={{ fontSize: 13, color: '#3B2D17' }}>
                            {cat.categoryCode} - {cat.categoryName}
                          </ThemedText>
                        </TouchableOpacity>
                      ))}
                    </ScrollView>
                  </View>
                )}
              </View>

              {/* Proceed Purchase Button */}
              <View style={{ justifyContent: 'flex-end', minWidth: 160 }}>
                <TouchableOpacity
                  style={[styles.btnPrimary, { height: 38 }, (!isPasswordValidated || !passwordInput.trim()) && { opacity: 0.6 }]}
                  onPress={handleProceedPurchase}
                  disabled={!isPasswordValidated || !passwordInput.trim() || isLoading}
                >
                  {isLoading ? (
                    <ActivityIndicator size="small" color="#FFFFFF" style={{ marginRight: 6 }} />
                  ) : (
                    <Ionicons name="play-outline" size={16} color="#FFFFFF" style={{ marginRight: 6 }} />
                  )}
                  <ThemedText style={styles.btnTextPrimary}>Proceed Purchase</ThemedText>
                </TouchableOpacity>
              </View>
            </View>
          </View>
        )}

        {/* 2. Summary Cards */}
        <View style={styles.summaryRow}>
          {/* Total Card */}
          <View style={styles.summaryCard}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerIconBadge}>
                <Ionicons name="cube-outline" size={14} color="#9E6D08" />
              </View>
              <ThemedText style={styles.cardHeaderTitle}>Total</ThemedText>
            </View>
            <View style={styles.cardMetricRow}>
              <View style={{ flex: 1.2 }}>
                <ThemedText style={styles.cardSubLabel}>Weight</ThemedText>
                <ThemedText style={styles.cardVal}>{summary.totalWeight || '0.00'}</ThemedText>
              </View>
              <View style={styles.metricDivider} />
              <View style={{ flex: 1 }}>
                <ThemedText style={styles.cardSubLabel}>PCS</ThemedText>
                <ThemedText style={styles.cardVal}>{summary.totalPcs != null ? summary.totalPcs : 0}</ThemedText>
              </View>
            </View>
          </View>

          {/* Scanned Card */}
          <View style={styles.summaryCard}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerIconBadge}>
                <Ionicons name="checkmark-circle-outline" size={14} color="#9E6D08" />
              </View>
              <ThemedText style={styles.cardHeaderTitle}>Scanned</ThemedText>
            </View>
            <View style={styles.cardMetricRow}>
              <View style={{ flex: 1.2 }}>
                <ThemedText style={styles.cardSubLabel}>Weight</ThemedText>
                <ThemedText style={[styles.cardVal, { color: '#2E7D32' }]}>{summary.scannedWeight || '0.00'}</ThemedText>
              </View>
              <View style={styles.metricDivider} />
              <View style={{ flex: 1 }}>
                <ThemedText style={styles.cardSubLabel}>PCS</ThemedText>
                <ThemedText style={[styles.cardVal, { color: '#2E7D32' }]}>{summary.scannedPcs != null ? summary.scannedPcs : 0}</ThemedText>
              </View>
            </View>
          </View>

          {/* Unscanned Card */}
          <View style={styles.summaryCard}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerIconBadge}>
                <Ionicons name="time-outline" size={14} color="#9E6D08" />
              </View>
              <ThemedText style={styles.cardHeaderTitle}>Unscanned</ThemedText>
            </View>
            <View style={styles.cardMetricRow}>
              <View style={{ flex: 1.2 }}>
                <ThemedText style={styles.cardSubLabel}>Weight</ThemedText>
                <ThemedText style={[styles.cardVal, { color: '#B91C1C' }]}>{summary.unscannedWeight || '0.00'}</ThemedText>
              </View>
              <View style={styles.metricDivider} />
              <View style={{ flex: 1 }}>
                <ThemedText style={styles.cardSubLabel}>PCS</ThemedText>
                <ThemedText style={[styles.cardVal, { color: '#B91C1C' }]}>{summary.unscannedPcs != null ? summary.unscannedPcs : 0}</ThemedText>
              </View>
            </View>
          </View>
        </View>

        {/* 3. Tray ID and Inventory Code Entry Row */}
        <View style={styles.entryRow}>
          {/* Tray ID */}
          <View style={styles.trayCol}>
            <ThemedText style={styles.inputLabelText}>Tray ID</ThemedText>
            <View style={styles.inputControlBox}>
              <Ionicons name="scan-outline" size={16} color="#8F7E66" style={{ marginRight: 6 }} />
              <TextInput
                ref={trayInputRef}
                style={styles.textInputControl}
                placeholder="Tray ID (e.g. D)"
                placeholderTextColor="#A39682"
                value={trayInput}
                onChangeText={setTrayInput}
                autoCapitalize="characters"
              />
              {trayInput.length > 0 && (
                <TouchableOpacity onPress={() => setTrayInput('')} style={{ padding: 2 }}>
                  <Ionicons name="close-circle" size={16} color="#8F7E66" />
                </TouchableOpacity>
              )}
            </View>
          </View>

          {/* Inventory Code */}
          <View style={styles.codeCol}>
            <ThemedText style={styles.inputLabelText}>Inventory Code</ThemedText>
            <View style={styles.inputControlBox}>
              <Ionicons name="barcode-outline" size={16} color="#8F7E66" style={{ marginRight: 6 }} />
              <TextInput
                ref={inventoryInputRef}
                style={styles.textInputControl}
                placeholder="Inventory Code (e.g. 1190000006)"
                placeholderTextColor="#A39682"
                value={inventoryCodeInput}
                onChangeText={setInventoryCodeInput}
                onSubmitEditing={handleScanSearch}
                returnKeyType="search"
              />
              {inventoryCodeInput.length > 0 && (
                <TouchableOpacity onPress={() => setInventoryCodeInput('')} style={{ padding: 2 }}>
                  <Ionicons name="close-circle" size={16} color="#8F7E66" />
                </TouchableOpacity>
              )}
            </View>
          </View>

          {/* Action Buttons */}
          <View style={styles.entryActionGroup}>
            <TouchableOpacity
              style={[styles.btnAction, { backgroundColor: '#9E6D08' }]}
              onPress={handleScanSearch}
              disabled={isLoading || isSaving}
            >
              {isLoading ? (
                <ActivityIndicator size="small" color="#FFFFFF" style={{ marginRight: 4 }} />
              ) : (
                <Ionicons name="add" size={18} color="#FFFFFF" style={{ marginRight: 2 }} />
              )}
              <ThemedText style={[styles.btnActionText, { color: '#FFFFFF' }]}>Add</ThemedText>
            </TouchableOpacity>

            <TouchableOpacity
              style={[styles.btnAction, styles.btnActionSecondary, (isSaving || !isPrepared) && { opacity: 0.6 }]}
              onPress={handleSavePurchase}
              disabled={isSaving || !isPrepared}
            >
              {isSaving ? (
                <ActivityIndicator size="small" color="#9E6D08" style={{ marginRight: 4 }} />
              ) : (
                <Ionicons name="save-outline" size={16} color="#3B2D17" style={{ marginRight: 4 }} />
              )}
              <ThemedText style={[styles.btnActionText, { color: '#3B2D17' }]}>Save</ThemedText>
            </TouchableOpacity>

            <TouchableOpacity style={[styles.btnAction, styles.btnActionSecondary]} onPress={handleListPress}>
              <Ionicons name="list-outline" size={16} color="#3B2D17" style={{ marginRight: 4 }} />
              <ThemedText style={[styles.btnActionText, { color: '#3B2D17' }]}>List</ThemedText>
            </TouchableOpacity>
          </View>
        </View>

        {/* 4. Search, Sort, and Filter Toolbar */}
        <View style={styles.toolbarRow}>
          {/* Search Box */}
          <View style={styles.searchBox}>
            <Ionicons name="search-outline" size={16} color="#8F7E66" style={{ marginRight: 6 }} />
            <TextInput
              style={styles.textInputControl}
              placeholder="Search by Tray ID, Inventory Code or Description..."
              placeholderTextColor="#A39682"
              value={searchQuery}
              onChangeText={setSearchQuery}
            />
            {searchQuery.length > 0 && (
              <TouchableOpacity onPress={() => setSearchQuery('')} style={{ padding: 2 }}>
                <Ionicons name="close-circle" size={16} color="#8F7E66" />
              </TouchableOpacity>
            )}
          </View>

          {/* Category Filter Dropdown */}
          <View style={{ position: 'relative', zIndex: 2000 }}>
            <TouchableOpacity
              style={styles.toolbarFilterBtn}
              onPress={() => setIsFilterCategoryOpen(!isFilterCategoryOpen)}
            >
              <Ionicons name="cube-outline" size={15} color="#8F7E66" style={{ marginRight: 6 }} />
              <ThemedText style={styles.toolbarBtnText} numberOfLines={1}>
                {filterCategory === 'ALL' ? 'All Categories' : filterCategory}
              </ThemedText>
              <Ionicons name="chevron-down" size={14} color="#8F7E66" style={{ marginLeft: 6 }} />
            </TouchableOpacity>

            {isFilterCategoryOpen && (
              <View style={styles.toolbarDropdown}>
                <ScrollView nestedScrollEnabled keyboardShouldPersistTaps="handled">
                  <TouchableOpacity
                    style={[styles.dropdownItem, filterCategory === 'ALL' && { backgroundColor: '#FDF7EB' }]}
                    onPress={() => {
                      setFilterCategory('ALL');
                      setIsFilterCategoryOpen(false);
                    }}
                  >
                    <ThemedText style={{ fontSize: 12.5, color: '#3B2D17', fontWeight: '700' }}>All Categories</ThemedText>
                  </TouchableOpacity>
                  {categories.map((cat) => (
                    <TouchableOpacity
                      key={cat.categoryCode}
                      style={[styles.dropdownItem, filterCategory === cat.categoryCode && { backgroundColor: '#FDF7EB' }]}
                      onPress={() => {
                        setFilterCategory(cat.categoryCode);
                        setIsFilterCategoryOpen(false);
                      }}
                    >
                      <ThemedText style={{ fontSize: 12.5, color: '#3B2D17' }}>
                        {cat.categoryCode} - {cat.categoryName}
                      </ThemedText>
                    </TouchableOpacity>
                  ))}
                </ScrollView>
              </View>
            )}
          </View>

          {/* Quick Sort Buttons */}
          <TouchableOpacity
            style={[styles.toolbarSortBtn, sortField === 'weight' && { backgroundColor: '#FDF7EB', borderColor: '#C8A86B' }]}
            onPress={() => handleHeaderSortToggle('weight')}
          >
            <ThemedText style={styles.toolbarBtnText}>Weight</ThemedText>
            <Ionicons
              name={sortField === 'weight' ? (sortDirection === 'asc' ? 'arrow-up' : 'arrow-down') : 'swap-vertical-outline'}
              size={13}
              color={sortField === 'weight' ? '#9E6D08' : '#8F7E66'}
              style={{ marginLeft: 4 }}
            />
          </TouchableOpacity>

          <TouchableOpacity
            style={[styles.toolbarSortBtn, sortField === 'totalCost' && { backgroundColor: '#FDF7EB', borderColor: '#C8A86B' }]}
            onPress={() => handleHeaderSortToggle('totalCost')}
          >
            <ThemedText style={styles.toolbarBtnText}>Total Cost</ThemedText>
            <Ionicons
              name={sortField === 'totalCost' ? (sortDirection === 'asc' ? 'arrow-up' : 'arrow-down') : 'swap-vertical-outline'}
              size={13}
              color={sortField === 'totalCost' ? '#9E6D08' : '#8F7E66'}
              style={{ marginLeft: 4 }}
            />
          </TouchableOpacity>

          <TouchableOpacity
            style={[styles.toolbarSortBtn, sortField === 'pcs' && { backgroundColor: '#FDF7EB', borderColor: '#C8A86B' }]}
            onPress={() => handleHeaderSortToggle('pcs')}
          >
            <ThemedText style={styles.toolbarBtnText}>PCS</ThemedText>
            <Ionicons
              name={sortField === 'pcs' ? (sortDirection === 'asc' ? 'arrow-up' : 'arrow-down') : 'swap-vertical-outline'}
              size={13}
              color={sortField === 'pcs' ? '#9E6D08' : '#8F7E66'}
              style={{ marginLeft: 4 }}
            />
          </TouchableOpacity>

          {/* Sort By Dropdown */}
          <View style={{ position: 'relative', zIndex: 2000 }}>
            <TouchableOpacity
              style={styles.toolbarSortDropdownBtn}
              onPress={() => setIsSortDropdownOpen(!isSortDropdownOpen)}
            >
              <ThemedText style={[styles.toolbarBtnText, { color: '#8F7E66', marginRight: 4 }]}>Sort By</ThemedText>
              <ThemedText style={[styles.toolbarBtnText, { fontWeight: '700', color: '#3B2D17' }]} numberOfLines={1}>
                {sortField === 'trayId' ? `Tray ID (${sortDirection === 'asc' ? 'A - Z' : 'Z - A'})` : sortField}
              </ThemedText>
              <Ionicons name="chevron-down" size={14} color="#8F7E66" style={{ marginLeft: 6 }} />
            </TouchableOpacity>

            {isSortDropdownOpen && (
              <View style={[styles.toolbarDropdown, { right: 0, left: undefined, minWidth: 180 }]}>
                <ScrollView nestedScrollEnabled keyboardShouldPersistTaps="handled">
                  <TouchableOpacity
                    style={[styles.dropdownItem, sortField === 'trayId' && sortDirection === 'asc' && { backgroundColor: '#FDF7EB' }]}
                    onPress={() => {
                      setSortField('trayId');
                      setSortDirection('asc');
                      setIsSortDropdownOpen(false);
                    }}
                  >
                    <ThemedText style={{ fontSize: 12.5, color: '#3B2D17' }}>Tray ID (A - Z)</ThemedText>
                  </TouchableOpacity>

                  <TouchableOpacity
                    style={[styles.dropdownItem, sortField === 'trayId' && sortDirection === 'desc' && { backgroundColor: '#FDF7EB' }]}
                    onPress={() => {
                      setSortField('trayId');
                      setSortDirection('desc');
                      setIsSortDropdownOpen(false);
                    }}
                  >
                    <ThemedText style={{ fontSize: 12.5, color: '#3B2D17' }}>Tray ID (Z - A)</ThemedText>
                  </TouchableOpacity>

                  <TouchableOpacity
                    style={[styles.dropdownItem, sortField === 'inventoryCode' && { backgroundColor: '#FDF7EB' }]}
                    onPress={() => {
                      setSortField('inventoryCode');
                      setSortDirection('asc');
                      setIsSortDropdownOpen(false);
                    }}
                  >
                    <ThemedText style={{ fontSize: 12.5, color: '#3B2D17' }}>Inventory Code</ThemedText>
                  </TouchableOpacity>

                  <TouchableOpacity
                    style={[styles.dropdownItem, sortField === 'description' && { backgroundColor: '#FDF7EB' }]}
                    onPress={() => {
                      setSortField('description');
                      setSortDirection('asc');
                      setIsSortDropdownOpen(false);
                    }}
                  >
                    <ThemedText style={{ fontSize: 12.5, color: '#3B2D17' }}>Description</ThemedText>
                  </TouchableOpacity>
                </ScrollView>
              </View>
            )}
          </View>

          {/* Clear Toolbar Filter Button */}
          <TouchableOpacity style={styles.toolbarClearBtn} onPress={handleClearToolbar}>
            <Ionicons name="refresh-outline" size={15} color="#9E6D08" style={{ marginRight: 4 }} />
            <ThemedText style={styles.toolbarClearText}>Clear</ThemedText>
          </TouchableOpacity>
        </View>

        {/* 5. Purchase Table Container */}
        <View style={styles.tableCardContainer}>
          {/* Section header banner matching Direct Purchase Line Details */}
          <View style={styles.cardHeaderBanner}>
            <View style={styles.headerIconBadge}>
              <Ionicons name="list-outline" size={14} color="#9E6D08" />
            </View>
            <ThemedText style={styles.cardHeaderTitle}>Purchase Take Details</ThemedText>
          </View>

          <ScrollView horizontal style={{ flex: 1 }} contentContainerStyle={{ minWidth: '100%' }}>
            <View style={{ flex: 1, minWidth: 900 }}>
              {/* Sticky Table Header */}
              <View style={styles.stickyTableHeader}>
                <TouchableOpacity style={[styles.thCell, { width: 100 }]} onPress={() => handleHeaderSortToggle('trayId')}>
                  <ThemedText style={styles.thText}>TRAY ID</ThemedText>
                  <Ionicons
                    name={sortField === 'trayId' ? (sortDirection === 'asc' ? 'arrow-up' : 'arrow-down') : 'swap-vertical-outline'}
                    size={12}
                    color="#8F7E66"
                    style={{ marginLeft: 4 }}
                  />
                </TouchableOpacity>

                <TouchableOpacity style={[styles.thCell, { width: 160 }]} onPress={() => handleHeaderSortToggle('inventoryCode')}>
                  <ThemedText style={styles.thText}>INVENTORY CODE</ThemedText>
                  <Ionicons
                    name={sortField === 'inventoryCode' ? (sortDirection === 'asc' ? 'arrow-up' : 'arrow-down') : 'swap-vertical-outline'}
                    size={12}
                    color="#8F7E66"
                    style={{ marginLeft: 4 }}
                  />
                </TouchableOpacity>

                <TouchableOpacity style={[styles.thCell, { flex: 1, minWidth: 180 }]} onPress={() => handleHeaderSortToggle('description')}>
                  <ThemedText style={styles.thText}>DESCRIPTION</ThemedText>
                  <Ionicons
                    name={sortField === 'description' ? (sortDirection === 'asc' ? 'arrow-up' : 'arrow-down') : 'swap-vertical-outline'}
                    size={12}
                    color="#8F7E66"
                    style={{ marginLeft: 4 }}
                  />
                </TouchableOpacity>

                <TouchableOpacity style={[styles.thCell, { width: 110, justifyContent: 'flex-end' }]} onPress={() => handleHeaderSortToggle('weight')}>
                  <ThemedText style={styles.thText}>WEIGHT</ThemedText>
                  <Ionicons
                    name={sortField === 'weight' ? (sortDirection === 'asc' ? 'arrow-up' : 'arrow-down') : 'swap-vertical-outline'}
                    size={12}
                    color="#8F7E66"
                    style={{ marginLeft: 4 }}
                  />
                </TouchableOpacity>

                <TouchableOpacity style={[styles.thCell, { width: 120, justifyContent: 'flex-end' }]} onPress={() => handleHeaderSortToggle('costPerGram')}>
                  <ThemedText style={styles.thText}>COST / GRAM</ThemedText>
                  <Ionicons
                    name={sortField === 'costPerGram' ? (sortDirection === 'asc' ? 'arrow-up' : 'arrow-down') : 'swap-vertical-outline'}
                    size={12}
                    color="#8F7E66"
                    style={{ marginLeft: 4 }}
                  />
                </TouchableOpacity>

                <TouchableOpacity style={[styles.thCell, { width: 80, justifyContent: 'flex-end' }]} onPress={() => handleHeaderSortToggle('pcs')}>
                  <ThemedText style={styles.thText}>PCS</ThemedText>
                  <Ionicons
                    name={sortField === 'pcs' ? (sortDirection === 'asc' ? 'arrow-up' : 'arrow-down') : 'swap-vertical-outline'}
                    size={12}
                    color="#8F7E66"
                    style={{ marginLeft: 4 }}
                  />
                </TouchableOpacity>

                <TouchableOpacity style={[styles.thCell, { width: 140, justifyContent: 'flex-end' }]} onPress={() => handleHeaderSortToggle('totalCost')}>
                  <ThemedText style={styles.thText}>TOTAL COST</ThemedText>
                  <Ionicons
                    name={sortField === 'totalCost' ? (sortDirection === 'asc' ? 'arrow-up' : 'arrow-down') : 'swap-vertical-outline'}
                    size={12}
                    color="#8F7E66"
                    style={{ marginLeft: 4 }}
                  />
                </TouchableOpacity>

                <View style={[styles.thCell, { width: 90, justifyContent: 'center' }]}>
                  <ThemedText style={styles.thText}>ACTIONS</ThemedText>
                </View>
              </View>

              {/* Scrollable Table Body */}
              <ScrollView style={styles.tableBodyScrollView} contentContainerStyle={{ flexGrow: 1 }}>
                {isLoading && processedPurchaseList.length === 0 ? (
                  <View style={styles.emptyBox}>
                    <ActivityIndicator size="small" color="#9E6D08" />
                  </View>
                ) : processedPurchaseList.length === 0 ? (
                  <View style={styles.emptyBox}>
                    <Ionicons name="cube-outline" size={40} color="#D8C7AA" style={{ marginBottom: 12 }} />
                    <ThemedText style={[styles.emptyText, { color: '#8F7E66' }]}>
                      {!isPrepared
                        ? 'Table is empty. Click "Download Purchase Take" to initialize.'
                        : 'No matching purchase records found.'}
                    </ThemedText>
                  </View>
                ) : (
                  processedPurchaseList.map((item, index) => {
                    const weightNum = Number(item.weight) || 0;
                    const totalCostNum = Number(item.totalCost) || 0;
                    const costPerGram = item.costPerGram || (weightNum > 0 ? totalCostNum / weightNum : 0);

                    return (
                      <View
                        key={item.inventoryCode ? `${item.inventoryCode}-${index}` : `item-${index}`}
                        style={[
                          styles.tableBodyRow,
                          index % 2 === 1 ? { backgroundColor: '#FAF7F0' } : { backgroundColor: '#FFFFFF' },
                        ]}
                      >
                        {/* Tray ID */}
                        <View style={[styles.tdCell, { width: 100 }]}>
                          <ThemedText style={styles.trayText}>{item.trayId || '-'}</ThemedText>
                        </View>

                        {/* Inventory Code */}
                        <View style={[styles.tdCell, { width: 160 }]}>
                          <ThemedText style={styles.tdCodeText}>{item.inventoryCode}</ThemedText>
                        </View>

                        {/* Description */}
                        <View style={[styles.tdCell, { flex: 1, minWidth: 180 }]}>
                          <ThemedText style={styles.tdText} numberOfLines={1}>
                            {item.description}
                          </ThemedText>
                        </View>

                        {/* Weight */}
                        <View style={[styles.tdCell, { width: 110, justifyContent: 'flex-end' }]}>
                          <ThemedText style={styles.tdNumText}>{weightNum.toFixed(3)}</ThemedText>
                        </View>

                        {/* Cost / Gram */}
                        <View style={[styles.tdCell, { width: 120, justifyContent: 'flex-end' }]}>
                          <ThemedText style={styles.tdNumText}>
                            {costPerGram.toLocaleString('en-IN', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}
                          </ThemedText>
                        </View>

                        {/* PCS */}
                        <View style={[styles.tdCell, { width: 80, justifyContent: 'flex-end' }]}>
                          <ThemedText style={styles.tdNumText}>{item.pcs}</ThemedText>
                        </View>

                        {/* Total Cost */}
                        <View style={[styles.tdCell, { width: 140, justifyContent: 'flex-end' }]}>
                          <ThemedText style={styles.tdNumText}>
                            {totalCostNum.toLocaleString('en-IN', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}
                          </ThemedText>
                        </View>

                        {/* Actions */}
                        <View style={[styles.tdCell, { width: 90, justifyContent: 'center' }]}>
                          <TouchableOpacity
                            style={styles.editActionBtn}
                            onPress={() => handleOpenEditModal(item)}
                          >
                            <Ionicons name="pencil" size={15} color="#8F7E66" />
                          </TouchableOpacity>
                        </View>
                      </View>
                    );
                  })
                )}
              </ScrollView>
            </View>
          </ScrollView>
        </View>

        {/* 6. Fixed Table Footer */}
        <View style={styles.fixedFooterBar}>
          <View style={styles.footerLeft}>
            <ThemedText style={styles.footerLabelText}>
              Total Records: <ThemedText style={styles.footerValBold}>{tableFooterTotals.totalRecords}</ThemedText>
            </ThemedText>
          </View>

          <View style={styles.footerRightRow}>
            <ThemedText style={styles.footerMetricText}>
              Total Weight: <ThemedText style={styles.footerGoldVal}>{tableFooterTotals.totalWeight}</ThemedText>
            </ThemedText>
            <ThemedText style={styles.footerDivider}>|</ThemedText>
            <ThemedText style={styles.footerMetricText}>
              Total PCS: <ThemedText style={styles.footerGoldVal}>{tableFooterTotals.totalPcs}</ThemedText>
            </ThemedText>
            <ThemedText style={styles.footerDivider}>|</ThemedText>
            <ThemedText style={styles.footerMetricText}>
              Total Cost: <ThemedText style={styles.footerDarkVal}>{tableFooterTotals.totalCost}</ThemedText>
            </ThemedText>
          </View>
        </View>
      </View>

      {/* Edit Item Modal */}
      <Modal visible={isEditModalOpen} transparent animationType="fade" onRequestClose={() => setIsEditModalOpen(false)}>
        <View style={styles.modalOverlay}>
          <View style={[styles.modalCard, styles.goldCard]}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerIconBadge}>
                <Ionicons name="pencil-outline" size={14} color="#9E6D08" />
              </View>
              <ThemedText style={styles.cardHeaderTitle}>
                Edit Item: {editingItem?.inventoryCode}
              </ThemedText>
              <TouchableOpacity onPress={() => setIsEditModalOpen(false)} style={{ marginLeft: 'auto' }}>
                <Ionicons name="close" size={18} color="#3B2D17" />
              </TouchableOpacity>
            </View>

            <View style={{ padding: 16 }}>
              <View style={{ gap: 12, marginBottom: 18 }}>
                <View>
                  <ThemedText style={styles.inputLabel}>Tray ID</ThemedText>
                  <View style={[styles.inputBox, { height: 38 }]}>
                    <TextInput
                      style={styles.textInput}
                      value={editTrayId}
                      onChangeText={setEditTrayId}
                      placeholder="Enter Tray ID"
                      placeholderTextColor="#A39682"
                    />
                  </View>
                </View>

                <View style={{ flexDirection: 'row', gap: 12 }}>
                  <View style={{ flex: 1 }}>
                    <ThemedText style={styles.inputLabel}>Weight (g)</ThemedText>
                    <View style={[styles.inputBox, { height: 38 }]}>
                      <TextInput
                        style={styles.textInput}
                        value={editWeight}
                        onChangeText={setEditWeight}
                        keyboardType="numeric"
                        placeholder="0.000"
                        placeholderTextColor="#A39682"
                      />
                    </View>
                  </View>

                  <View style={{ flex: 1 }}>
                    <ThemedText style={styles.inputLabel}>PCS</ThemedText>
                    <View style={[styles.inputBox, { height: 38 }]}>
                      <TextInput
                        style={styles.textInput}
                        value={editPcs}
                        onChangeText={setEditPcs}
                        keyboardType="numeric"
                        placeholder="1"
                        placeholderTextColor="#A39682"
                      />
                    </View>
                  </View>
                </View>

                {editingItem?.description ? (
                  <View>
                    <ThemedText style={styles.inputLabel}>Description</ThemedText>
                    <ThemedText style={{ fontSize: 12.5, color: '#3B2D17', fontWeight: '500' }}>
                      {editingItem.description}
                    </ThemedText>
                  </View>
                ) : null}
              </View>

              <View style={{ flexDirection: 'row', justifyContent: 'flex-end', gap: 10 }}>
                <TouchableOpacity style={styles.modalCancelBtn} onPress={() => setIsEditModalOpen(false)}>
                  <ThemedText style={{ fontSize: 13, color: '#3B2D17', fontWeight: '600' }}>Cancel</ThemedText>
                </TouchableOpacity>

                <TouchableOpacity style={styles.modalSaveBtn} onPress={handleSaveItemEdit} disabled={isLoading}>
                  {isLoading ? (
                    <ActivityIndicator size="small" color="#FFFFFF" />
                  ) : (
                    <ThemedText style={{ fontSize: 13, color: '#FFFFFF', fontWeight: '700' }}>Update Item</ThemedText>
                  )}
                </TouchableOpacity>
              </View>
            </View>
          </View>
        </View>
      </Modal>

      {/* Session In Progress Modal */}
      <Modal visible={showAlreadyStartedModal} transparent animationType="fade" onRequestClose={() => setShowAlreadyStartedModal(false)}>
        <View style={styles.modalOverlay}>
          <View style={[styles.modalCard, styles.goldCard]}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerIconBadge}>
                <Ionicons name="time-outline" size={14} color="#9E6D08" />
              </View>
              <ThemedText style={styles.cardHeaderTitle}>Purchase Take In Progress</ThemedText>
              <TouchableOpacity onPress={() => setShowAlreadyStartedModal(false)} style={{ marginLeft: 'auto' }}>
                <Ionicons name="close" size={18} color="#3B2D17" />
              </TouchableOpacity>
            </View>

            <View style={{ padding: 16 }}>
              <ThemedText style={{ fontSize: 13, color: '#8F7E66', lineHeight: 20, marginBottom: 18 }}>
                An active purchase take session exists. Would you like to start a new purchase take session or resume your current session?
              </ThemedText>

              <View style={{ flexDirection: 'row', justifyContent: 'flex-end', gap: 10 }}>
                <TouchableOpacity style={styles.modalCancelBtn} onPress={() => setShowAlreadyStartedModal(false)}>
                  <ThemedText style={{ fontSize: 13, fontWeight: '600', color: '#3B2D17' }}>Cancel</ThemedText>
                </TouchableOpacity>

                <TouchableOpacity style={[styles.modalCancelBtn, { borderColor: '#F3C5C5', backgroundColor: '#FDF2F2' }]} onPress={() => executeProceed(true)}>
                  <ThemedText style={{ fontSize: 13, fontWeight: '600', color: '#B91C1C' }}>Start New</ThemedText>
                </TouchableOpacity>

                <TouchableOpacity style={styles.modalSaveBtn} onPress={() => executeProceed(false)}>
                  <ThemedText style={{ fontSize: 13, fontWeight: '700', color: '#FFFFFF' }}>Resume Session</ThemedText>
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
  container: {
    flex: 1,
    display: 'flex',
    flexDirection: 'column',
    overflow: 'hidden',
    backgroundColor: '#FBF9F5',
    padding: 14,
    gap: 10,
  },
  goldCard: {
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    overflow: 'hidden',
  },
  cardHeaderBanner: {
    backgroundColor: '#F5E7CA',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    paddingVertical: 7,
    paddingHorizontal: 12,
    flexDirection: 'row',
    alignItems: 'center',
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
  filterCard: {
    padding: 16,
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    backgroundColor: '#FFFFFF',
  },
  // 1. Header Styling
  headerBar: {
    height: 52,
    paddingHorizontal: 16,
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    backgroundColor: '#FFFFFF',
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
  },
  headerTitleGroup: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  backBtn: {
    width: 34,
    height: 34,
    borderRadius: 6,
    borderWidth: 1,
    borderColor: '#DFD5C3',
    backgroundColor: '#FFFFFF',
    alignItems: 'center',
    justifyContent: 'center',
    marginRight: 10,
  },
  headerTitle: {
    fontSize: 18,
    fontWeight: 'bold',
    color: '#3B2D17',
    fontFamily: Platform.OS === 'web' ? 'Cambria, Georgia, serif' : undefined,
  },
  headerActions: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  btnDownload: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#9E6D08',
    paddingHorizontal: 14,
    height: 36,
    borderRadius: 6,
  },
  btnDownloadText: {
    color: '#FFFFFF',
    fontSize: 12.5,
    fontWeight: 'bold',
  },
  statusBanner: {
    borderWidth: 1,
    borderRadius: 8,
    padding: 10,
    flexDirection: 'row',
    alignItems: 'center',
  },
  // 2. Summary Cards Styling
  summaryRow: {
    flexDirection: 'row',
    gap: 12,
  },
  summaryCard: {
    flex: 1,
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    backgroundColor: '#FFFFFF',
    overflow: 'hidden',
  },
  cardMetricRow: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingVertical: 10,
    paddingHorizontal: 14,
  },
  cardSubLabel: {
    fontSize: 11,
    color: '#8F7E66',
    fontWeight: 'bold',
    marginBottom: 2,
    textTransform: 'uppercase',
  },
  cardVal: {
    fontSize: 18,
    fontWeight: 'bold',
    color: '#3B2D17',
    fontVariant: ['tabular-nums'],
  },
  metricDivider: {
    width: 1,
    height: 28,
    backgroundColor: '#EADBCA',
    marginHorizontal: 12,
  },
  // 3. Entry Row Styling
  entryRow: {
    flexDirection: 'row',
    alignItems: 'flex-end',
    gap: 10,
  },
  trayCol: {
    width: 220,
  },
  codeCol: {
    flex: 1,
  },
  inputLabelText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3B2D17',
    marginBottom: 4,
    textTransform: 'uppercase',
  },
  inputControlBox: {
    flexDirection: 'row',
    alignItems: 'center',
    borderWidth: 1,
    borderColor: '#DFD5C3',
    borderRadius: 6,
    paddingHorizontal: 12,
    height: 38,
    backgroundColor: '#FFFFFF',
  },
  textInputControl: {
    flex: 1,
    fontSize: 12.5,
    color: '#3B2D17',
    height: '100%',
    ...(Platform.OS === 'web' ? ({ outlineStyle: 'none' } as any) : {}),
  },
  entryActionGroup: {
    flexDirection: 'row',
    gap: 8,
  },
  btnAction: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    paddingHorizontal: 18,
    height: 38,
    borderRadius: 6,
  },
  btnActionSecondary: {
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#DFD5C3',
  },
  btnActionText: {
    fontSize: 12.5,
    fontWeight: 'bold',
  },
  // 4. Toolbar Styling
  toolbarRow: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 8,
    zIndex: 10,
  },
  searchBox: {
    flex: 1,
    flexDirection: 'row',
    alignItems: 'center',
    borderWidth: 1,
    borderColor: '#DFD5C3',
    borderRadius: 6,
    paddingHorizontal: 12,
    height: 36,
    backgroundColor: '#FFFFFF',
  },
  toolbarFilterBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    borderWidth: 1,
    borderColor: '#DFD5C3',
    borderRadius: 6,
    paddingHorizontal: 12,
    height: 36,
    backgroundColor: '#FFFFFF',
  },
  toolbarSortBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    borderWidth: 1,
    borderColor: '#DFD5C3',
    borderRadius: 6,
    paddingHorizontal: 10,
    height: 36,
    backgroundColor: '#FFFFFF',
  },
  toolbarSortDropdownBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    borderWidth: 1,
    borderColor: '#DFD5C3',
    borderRadius: 6,
    paddingHorizontal: 12,
    height: 36,
    backgroundColor: '#FFFFFF',
  },
  toolbarBtnText: {
    fontSize: 11.5,
    color: '#3B2D17',
    fontWeight: '500',
  },
  toolbarClearBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    borderWidth: 1,
    borderColor: '#DFD5C3',
    borderRadius: 6,
    paddingHorizontal: 14,
    height: 36,
    backgroundColor: '#FFFFFF',
  },
  toolbarClearText: {
    fontSize: 11.5,
    fontWeight: 'bold',
    color: '#3B2D17',
  },
  toolbarDropdown: {
    position: 'absolute',
    top: 40,
    left: 0,
    minWidth: 180,
    maxHeight: 200,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    borderRadius: 6,
    backgroundColor: '#FFFFFF',
    zIndex: 9999,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.08,
    shadowRadius: 8,
    elevation: 8,
  },
  dropdownItem: {
    paddingVertical: 8,
    paddingHorizontal: 12,
    borderBottomWidth: 1,
    borderBottomColor: '#FAF7F0',
  },
  dropdownMenu: {
    position: 'absolute',
    top: 42,
    left: 0,
    right: 0,
    maxHeight: 200,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    borderRadius: 6,
    backgroundColor: '#FFFFFF',
    zIndex: 9999,
    elevation: 10,
  },
  // 5. Table Container Styling
  tableCardContainer: {
    flex: 1,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    borderRadius: 8,
    backgroundColor: '#FFFFFF',
    overflow: 'hidden',
  },
  stickyTableHeader: {
    flexDirection: 'row',
    alignItems: 'center',
    minHeight: 36,
    backgroundColor: '#F5E7CA',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    paddingHorizontal: 12,
  },
  thCell: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingVertical: 6,
  },
  thText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3B2D17',
    letterSpacing: 0.3,
  },
  tableBodyScrollView: {
    flex: 1,
  },
  tableBodyRow: {
    flexDirection: 'row',
    alignItems: 'center',
    minHeight: 34,
    paddingHorizontal: 12,
    borderBottomWidth: 1,
    borderBottomColor: '#F0E6D2',
  },
  tdCell: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingVertical: 4,
  },
  trayText: {
    fontSize: 11.5,
    fontWeight: '600',
    color: '#332717',
  },
  tdText: {
    fontSize: 11.5,
    color: '#332717',
  },
  tdCodeText: {
    fontSize: 11.5,
    fontWeight: '600',
    color: '#332717',
  },
  tdNumText: {
    fontSize: 11.5,
    color: '#332717',
    fontVariant: ['tabular-nums'],
  },
  editActionBtn: {
    width: 26,
    height: 26,
    borderRadius: 4,
    borderWidth: 1,
    borderColor: '#DFD5C3',
    alignItems: 'center',
    justifyContent: 'center',
    backgroundColor: '#FAF7F0',
  },
  emptyBox: {
    padding: 40,
    alignItems: 'center',
    justifyContent: 'center',
    flex: 1,
  },
  emptyText: {
    fontSize: 12.5,
    fontWeight: '500',
  },
  // 6. Fixed Table Footer Styling
  fixedFooterBar: {
    minHeight: 38,
    backgroundColor: '#F9F1DF',
    borderTopWidth: 1,
    borderTopColor: '#E5D6B6',
    paddingHorizontal: 16,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
  },
  footerLeft: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  footerRightRow: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  footerLabelText: {
    fontSize: 11.5,
    color: '#32230D',
    fontWeight: 'bold',
  },
  footerValBold: {
    fontWeight: 'bold',
    color: '#32230D',
  },
  footerMetricText: {
    fontSize: 11.5,
    color: '#32230D',
    fontWeight: 'bold',
  },
  footerGoldVal: {
    fontWeight: 'bold',
    color: '#9E6D08',
    fontVariant: ['tabular-nums'],
  },
  footerDarkVal: {
    fontWeight: 'bold',
    color: '#32230D',
    fontVariant: ['tabular-nums'],
  },
  footerDivider: {
    marginHorizontal: 12,
    color: '#D8C7AA',
    fontSize: 11,
  },
  // Modals Styling
  modalOverlay: {
    flex: 1,
    backgroundColor: 'rgba(0,0,0,0.4)',
    justifyContent: 'center',
    alignItems: 'center',
    padding: 16,
  },
  modalCard: {
    width: '100%',
    maxWidth: 440,
  },
  modalHeaderRow: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    marginBottom: 14,
  },
  inputBox: {
    flexDirection: 'row',
    alignItems: 'center',
    borderWidth: 1,
    borderColor: '#DFD5C3',
    borderRadius: 6,
    paddingHorizontal: 10,
    backgroundColor: '#FFFFFF',
  },
  textInput: {
    flex: 1,
    fontSize: 12.5,
    color: '#3B2D17',
    height: '100%',
    ...(Platform.OS === 'web' ? ({ outlineStyle: 'none' } as any) : {}),
  },
  inputLabel: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3B2D17',
    marginBottom: 4,
    textTransform: 'uppercase',
  },
  btnPrimary: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    paddingHorizontal: 14,
    borderRadius: 6,
    backgroundColor: '#9E6D08',
  },
  btnTextPrimary: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#FFFFFF',
  },
  btnSecondary: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    paddingHorizontal: 14,
    borderRadius: 6,
    borderWidth: 1,
  },
  modalCancelBtn: {
    paddingHorizontal: 16,
    height: 34,
    borderRadius: 6,
    borderWidth: 1,
    borderColor: '#DFD5C3',
    alignItems: 'center',
    justifyContent: 'center',
    backgroundColor: '#FFFFFF',
  },
  modalSaveBtn: {
    paddingHorizontal: 18,
    height: 34,
    borderRadius: 6,
    backgroundColor: '#9E6D08',
    alignItems: 'center',
    justifyContent: 'center',
  },
});
