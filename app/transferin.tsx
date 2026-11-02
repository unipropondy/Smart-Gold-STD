import { ThemedText } from '@/components/themed-text';
import { API_BASE_URL } from '@/constants/api';
import { useThemeColors } from '@/hooks/useThemeColors';
import { Ionicons } from '@expo/vector-icons';
import { Stack, useRouter } from 'expo-router';
import React, { useEffect, useRef, useState } from 'react';
import {
  ActivityIndicator,
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

const API_URL = `${API_BASE_URL}/transferin`;

interface TransferDetailItem {
  TransferNo: string;
  InventoryCode: string;
  Description: string;
  ClassCode?: string;
  CategoryCode?: string;
  CatalogueCode?: string;
  QtyOnHand: number;
  Weight: number;
  SizeCode?: string | number;
  CostPerGram: number;
  PMC: number;
  SMC: number;
  PCS: number;
  TotalCost: number;
  TransferTag: boolean;
}

interface PendingTransferSummary {
  TransferNo: string;
  TransferDate: string;
  FromLocation: string;
  ToLocation: string;
  TotalPcs: number;
  TotalWeight: number;
  TotalCost: number;
  Remarks: string;
}

export default function TransferInScreen() {
  const themeColors = useThemeColors();
  const router = useRouter();

  const [loading, setLoading] = useState(false);
  const [toast, setToast] = useState<{ message: string; type: 'success' | 'error' | 'warning' } | null>(null);

  // Header state
  const [transferNo, setTransferNo] = useState('');
  const [transferDate, setTransferDate] = useState('');
  const [fromLocation, setFromLocation] = useState('');
  const [remarks, setRemarks] = useState('');
  const [toLocation, setToLocation] = useState('HQ');

  // Grid state
  const [gridData, setGridData] = useState<TransferDetailItem[]>([]);
  const [inventoryCodeInput, setInventoryCodeInput] = useState('');
  const [selectedRowIndex, setSelectedRowIndex] = useState<number | null>(null);

  // Salesman Password Modal (Required before accepting)
  const [showSalesmanModal, setShowSalesmanModal] = useState(false);
  const [salesmanPassword, setSalesmanPassword] = useState('');
  const [verifyingSalesman, setVerifyingSalesman] = useState(false);

  // Search / Browse Pending Transfers Modal
  const [showSearchModal, setShowSearchModal] = useState(false);
  const [pendingList, setPendingList] = useState<PendingTransferSummary[]>([]);
  const [pendingSearchQuery, setPendingSearchQuery] = useState('');
  const [loadingPendingList, setLoadingPendingList] = useState(false);

  const barcodeInputRef = useRef<TextInput>(null);
  const transferNoInputRef = useRef<TextInput>(null);
  const passwordInputRef = useRef<TextInput>(null);

  const showToast = (message: string, type: 'success' | 'error' | 'warning' = 'success') => {
    setToast({ message, type });
    setTimeout(() => setToast(null), 3500);
  };

  const handleClear = () => {
    setTransferNo('');
    setTransferDate('');
    setFromLocation('');
    setRemarks('');
    setGridData([]);
    setInventoryCodeInput('');
    setSelectedRowIndex(null);
    setSalesmanPassword('');
    setShowSalesmanModal(false);
    showToast('Transfer In form cleared', 'success');
  };

  // --- Fetch Inbound Transfer Details ---
  const handleFetch = async (targetTransferNo?: string) => {
    const noToFetch = (targetTransferNo || transferNo).trim();
    if (!noToFetch) {
      showToast('Please enter the transfer no.', 'error');
      transferNoInputRef.current?.focus();
      return;
    }

    try {
      setLoading(true);
      const res = await fetch(`${API_URL}/fetch?transferNo=${encodeURIComponent(noToFetch)}&location=${encodeURIComponent(toLocation)}`);

      let data;
      const contentType = res.headers.get('content-type');
      if (contentType && contentType.indexOf('application/json') !== -1) {
        data = await res.json();
      } else {
        throw new Error(`Server returned status ${res.status}: ${res.statusText}`);
      }

      if (data.success && data.data) {
        setTransferNo(data.data.header?.TransferNo || noToFetch);
        setFromLocation(data.data.header?.FromLocation || '');

        if (data.data.header?.TransferDate) {
          const dateObj = new Date(data.data.header.TransferDate);
          if (!isNaN(dateObj.getTime())) {
            const formatted = `${String(dateObj.getDate()).padStart(2, '0')}/${String(
              dateObj.getMonth() + 1
            ).padStart(2, '0')}/${dateObj.getFullYear()}`;
            setTransferDate(formatted);
          } else {
            setTransferDate(String(data.data.header.TransferDate).substring(0, 10));
          }
        }

        setRemarks(data.data.header?.Remarks || '');
        const details: TransferDetailItem[] = (data.data.details || []).map((item: any) => ({
          TransferNo: item.TransferNo || noToFetch,
          InventoryCode: item.InventoryCode,
          Description: item.Description || '',
          ClassCode: item.ClassCode || '',
          CategoryCode: item.CategoryCode || '',
          CatalogueCode: item.CatalogueCode || '',
          QtyOnHand: Number(item.QtyOnHand || item.QtyonHand || item.PCS || 1),
          Weight: Number(item.Weight || 0),
          SizeCode: item.SizeCode || '',
          CostPerGram: Number(item.CostPerGram || 0),
          PMC: Number(item.PMC || 0),
          SMC: Number(item.SMC || 0),
          PCS: Number(item.PCS || item.QtyOnHand || item.QtyonHand || 1),
          TotalCost: Number(item.TotalCost || 0),
          TransferTag: Boolean(item.TransferTag),
        }));
        setGridData(details);

        showToast(`Loaded voucher #${data.data.header?.TransferNo} (${details.length} items)`, 'success');

        setTimeout(() => {
          barcodeInputRef.current?.focus();
        }, 150);
      } else {
        showToast(data.message || 'Transfer docket not found', 'error');
        if (data.isAlreadyAccepted) {
          // If already accepted, we keep the transfer no
          setTransferNo(noToFetch);
        }
      }
    } catch (err: any) {
      showToast(err.message || 'Network error fetching transfer', 'error');
    } finally {
      setLoading(false);
    }
  };

  // --- Open Browse Pending Transfers Modal ---
  const handleOpenSearchModal = async () => {
    setShowSearchModal(true);
    setLoadingPendingList(true);
    try {
      const res = await fetch(`${API_URL}/pending-list?location=${encodeURIComponent(toLocation)}&search=${encodeURIComponent(pendingSearchQuery)}`);
      const data = await res.json();
      if (data.success) {
        setPendingList(data.list || []);
      } else {
        showToast(data.message || 'Error loading pending transfers', 'error');
      }
    } catch (e: any) {
      showToast(e.message || 'Failed to retrieve pending transfers', 'error');
    } finally {
      setLoadingPendingList(false);
    }
  };

  const handleFilterPending = async (query: string) => {
    setPendingSearchQuery(query);
    try {
      const res = await fetch(`${API_URL}/pending-list?location=${encodeURIComponent(toLocation)}&search=${encodeURIComponent(query)}`);
      const data = await res.json();
      if (data.success) {
        setPendingList(data.list || []);
      }
    } catch (e) { }
  };

  const handleSelectPendingTransfer = (p: PendingTransferSummary) => {
    setShowSearchModal(false);
    setTransferNo(p.TransferNo);
    handleFetch(p.TransferNo);
  };

  // --- Initiate Accept Process (Requires Salesman Password Check First) ---
  const handleInitiateAccept = () => {
    if (!transferNo.trim()) {
      showToast('Enter Transfer Number', 'error');
      return;
    }
    if (gridData.length === 0) {
      showToast('No items loaded. Please fetch a valid transfer voucher first.', 'error');
      return;
    }
    if (selectedCount !== totalCount) {
      showToast(`Please verify all items before accepting (${selectedCount}/${totalCount} scanned)`, 'warning');
      return;
    }

    // Open Salesman Password Modal
    setSalesmanPassword('');
    setShowSalesmanModal(true);
    setTimeout(() => {
      passwordInputRef.current?.focus();
    }, 150);
  };

  // --- Verify Salesman and Execute Accept ---
  const handleVerifySalesmanAndAccept = async () => {
    if (!salesmanPassword.trim()) {
      showToast('Salesman password is required', 'error');
      return;
    }

    try {
      setVerifyingSalesman(true);
      const verifyRes = await fetch(`${API_URL}/verify-salesman`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ password: salesmanPassword.trim() }),
      });
      const verifyData = await verifyRes.json();

      if (!verifyData.success) {
        showToast(verifyData.message || 'SalesMan Password is Incorrect!', 'error');
        setSalesmanPassword('');
        passwordInputRef.current?.focus();
        setVerifyingSalesman(false);
        return;
      }

      const salesmanCode = verifyData.salesman?.SalesmanCode || 'ADMIN';
      setShowSalesmanModal(false);

      // Now call accept endpoint
      setLoading(true);
      const res = await fetch(`${API_URL}/accept`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          transferNo: transferNo.trim(),
          location: toLocation,
          userId: 'ADMIN',
          salesmanCode: salesmanCode,
          items: gridData,
        }),
      });

      const acceptData = await res.json();
      if (acceptData.success) {
        showToast('Inventory Transferred & Accepted Successfully', 'success');
        handleClear();
      } else {
        showToast(acceptData.message || 'Inventory acceptance failed', 'error');
      }
    } catch (err: any) {
      showToast(err.message || 'Error executing inventory accept', 'error');
    } finally {
      setVerifyingSalesman(false);
      setLoading(false);
    }
  };

  // --- Scan or Enter Barcode to Verify ---
  const handleScanInventory = () => {
    if (!inventoryCodeInput.trim()) return;

    const code = inventoryCodeInput.trim().toUpperCase();
    const index = gridData.findIndex(
      (item) => String(item.InventoryCode).trim().toUpperCase() === code
    );

    if (index === -1) {
      showToast(`Inventory code '${code}' not found in this transfer list`, 'error');
    } else {
      if (gridData[index].TransferTag) {
        showToast(`Inventory already Exists in this Transfer List (${code})`, 'warning');
      } else {
        const newData = [...gridData];
        newData[index].TransferTag = true;
        setGridData(newData);
        setSelectedRowIndex(index);
        showToast(`Verified: ${code}`, 'success');
      }
    }

    setInventoryCodeInput('');
    setTimeout(() => {
      barcodeInputRef.current?.focus();
    }, 50);
  };

  const toggleRowSelection = (index: number) => {
    const newData = [...gridData];
    newData[index].TransferTag = !newData[index].TransferTag;
    setGridData(newData);
    setSelectedRowIndex(index);
  };

  // Keyboard Shortcuts (F6=Clear, F7=Accept, Esc=Back)
  useEffect(() => {
    const handleKeyDown = (e: KeyboardEvent) => {
      if (e.key === 'F6') {
        e.preventDefault();
        handleClear();
      }
      if (e.key === 'F7' && canAccept) {
        e.preventDefault();
        handleInitiateAccept();
      }
      if (e.key === 'Escape') {
        e.preventDefault();
        if (showSalesmanModal) setShowSalesmanModal(false);
        else if (showSearchModal) setShowSearchModal(false);
        else router.back();
      }
    };
    if (typeof window !== 'undefined') {
      window.addEventListener('keydown', handleKeyDown);
      return () => window.removeEventListener('keydown', handleKeyDown);
    }
  }, [gridData, transferNo, showSalesmanModal, showSearchModal]);

  const totalCount = gridData.length;
  const selectedCount = gridData.filter((item) => item.TransferTag).length;
  const canAccept = totalCount > 0 && totalCount === selectedCount;

  // Totals calculations
  const totalWeight = gridData.reduce((sum, item) => sum + (Number(item.Weight) || 0), 0);
  const verifiedWeight = gridData
    .filter((item) => item.TransferTag)
    .reduce((sum, item) => sum + (Number(item.Weight) || 0), 0);
  const totalCostSum = gridData.reduce((sum, item) => sum + (Number(item.TotalCost) || 0), 0);

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
            <ThemedText style={styles.headerTitle}>Branch Stock Transfer In</ThemedText>
            <ThemedText style={styles.headerSubtitle}>
              Receive, Barcode Scan & Accept Inter-Branch Inventory
            </ThemedText>
          </View>
          {loading && <ActivityIndicator size="small" color="#B48218" style={{ marginLeft: 8 }} />}
        </View>

        <View style={styles.headerRight}>
          <TouchableOpacity
            style={[styles.actionBtn, styles.searchBtn]}
            onPress={handleOpenSearchModal}
            activeOpacity={0.8}
            disabled={loading}
          >
            <Ionicons name="list-outline" size={14} color="#5C4A28" />
            <ThemedText style={styles.clearBtnText}>Browse Pending</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity
            style={[styles.actionBtn, styles.clearBtn]}
            onPress={handleClear}
            activeOpacity={0.8}
            disabled={loading}
          >
            <Ionicons name="refresh-outline" size={14} color="#5C4A28" />
            <ThemedText style={styles.clearBtnText}>Clear</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity
            style={[styles.actionBtn, styles.fetchBtn]}
            onPress={() => handleFetch()}
            activeOpacity={0.8}
            disabled={loading}
          >
            <Ionicons name="cloud-download-outline" size={14} color="#5C4A28" />
            <ThemedText style={styles.clearBtnText}>Fetch</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity
            style={[styles.actionBtn, styles.saveBtn, (!canAccept || loading) && { opacity: 0.55 }]}
            onPress={handleInitiateAccept}
            activeOpacity={0.8}
            disabled={!canAccept || loading}
          >
            <Ionicons name="checkmark-done-circle-outline" size={15} color="#9E6D08" />
            <ThemedText style={styles.saveBtnText}>
              {canAccept ? 'Accept Transfer (F7)' : `Verify All (${selectedCount}/${totalCount})`}
            </ThemedText>
          </TouchableOpacity>
        </View>
      </View>

      {/* TOAST NOTIFICATION BANNER */}
      {toast && (
        <View
          style={[
            styles.toast,
            toast.type === 'error'
              ? styles.toastError
              : toast.type === 'warning'
                ? styles.toastWarning
                : styles.toastSuccess,
          ]}
        >
          <Ionicons
            name={
              toast.type === 'error'
                ? 'alert-circle'
                : toast.type === 'warning'
                  ? 'warning-outline'
                  : 'checkmark-circle'
            }
            size={16}
            color="#FFFFFF"
            style={{ marginRight: 6 }}
          />
          <ThemedText style={styles.toastText}>{toast.message}</ThemedText>
        </View>
      )}

      <KeyboardAvoidingView behavior={Platform.OS === 'ios' ? 'padding' : undefined} style={{ flex: 1 }}>
        <ScrollView contentContainerStyle={styles.scrollContainer} keyboardShouldPersistTaps="handled">
          {/* CARD 1: INBOUND TRANSFER DOCKET HEADER */}
          <View style={styles.goldCard}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="document-text-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitleSmall}>Inbound Dispatch Voucher Details</ThemedText>
              </View>

              {totalCount > 0 && (
                <View style={[styles.transferBadge, canAccept && styles.transferBadgeSuccess]}>
                  <ThemedText style={styles.transferBadgeLabel}>Verification Status:</ThemedText>
                  <ThemedText style={styles.transferBadgeValue}>
                    {selectedCount} / {totalCount} Items Scanned
                  </ThemedText>
                </View>
              )}
            </View>

            <View style={styles.cardBody}>
              {/* Row 1: Transfer No + Fetch, Transfer Date, From Location, Progress */}
              <View style={styles.fourColumnGrid}>
                {/* Transfer No */}
                <View style={[styles.inlineFieldRow, { flex: 1.2, minWidth: 260 }]}>
                  <ThemedText style={[styles.inlineFieldLabel, { width: 95 }]}>Transfer No *</ThemedText>
                  <View style={styles.inputWithLookup}>
                    <TextInput
                      ref={transferNoInputRef}
                      style={[styles.formInput, styles.inputAttached, { flex: 1 }]}
                      value={transferNo}
                      onChangeText={setTransferNo}
                      onSubmitEditing={() => handleFetch()}
                      placeholder="Enter Transfer No..."
                      placeholderTextColor="#9E8D77"
                    />
                    <TouchableOpacity
                      style={styles.attachedLookupBtn}
                      onPress={() => handleFetch()}
                      activeOpacity={0.7}
                      disabled={loading}
                    >
                      <Ionicons name="search" size={13} color="#7D5E19" />
                    </TouchableOpacity>
                  </View>
                </View>

                {/* Transfer Date */}
                <View style={[styles.inlineFieldRow, { flex: 0.9, minWidth: 190 }]}>
                  <ThemedText style={[styles.inlineFieldLabel, { width: 90 }]}>Transfer Date</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1, minWidth: 80 }]}
                    value={transferDate}
                    editable={false}
                    placeholder="DD/MM/YYYY"
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* From Location */}
                <View style={[styles.inlineFieldRow, { flex: 0.9, minWidth: 190 }]}>
                  <ThemedText style={[styles.inlineFieldLabel, { width: 90 }]}>From Location</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1, minWidth: 80 }]}
                    value={fromLocation}
                    editable={false}
                    placeholder="Source Location"
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                {/* Verification Progress Badge */}
                <View style={[styles.inlineFieldRow, { flex: 1, minWidth: 190 }]}>
                  <ThemedText style={[styles.inlineFieldLabel, { width: 85 }]}>Progress</ThemedText>
                  <View
                    style={[
                      styles.statusPill,
                      canAccept ? styles.statusPillReady : styles.statusPillPending,
                    ]}
                  >
                    <Ionicons
                      name={canAccept ? 'shield-checkmark' : 'time-outline'}
                      size={12}
                      color={canAccept ? '#2E7D32' : '#C62828'}
                      style={{ marginRight: 4 }}
                    />
                    <ThemedText
                      style={[
                        styles.statusPillText,
                        canAccept ? styles.statusPillTextReady : styles.statusPillTextPending,
                      ]}
                    >
                      {totalCount === 0
                        ? 'Awaiting Fetch'
                        : canAccept
                          ? 'Ready to Accept'
                          : `${totalCount - selectedCount} Remaining`}
                    </ThemedText>
                  </View>
                </View>
              </View>

              {/* Row 2: Remarks */}
              <View style={[styles.singleColumnRow, { marginTop: 8 }]}>
                <View style={styles.inlineFieldRow}>
                  <ThemedText style={[styles.inlineFieldLabel, { width: 95 }]}>Remarks</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1 }]}
                    value={remarks}
                    editable={false}
                    placeholder="Dispatch notes or transit instructions..."
                    placeholderTextColor="#9E8D77"
                  />
                </View>
              </View>
            </View>
          </View>

          {/* CARD 2: CONSOLIDATED INBOUND TOTALS */}
          <View style={[styles.goldCard, { marginTop: 10 }]}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="calculator-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitleSmall}>Inbound Verification Summary</ThemedText>
              </View>
            </View>

            <View style={styles.cardBody}>
              <View style={styles.fourColumnGrid}>
                {/* Total Manifest Wt */}
                <View style={[styles.inlineFieldRow, { flex: 1, minWidth: 200 }]}>
                  <ThemedText style={[styles.inlineFieldLabel, { width: 95 }]}>Manifest Wt (g)</ThemedText>
                  <TextInput
                    style={[
                      styles.formInput,
                      styles.readonlyInput,
                      { flex: 1, minWidth: 90, textAlign: 'right', fontWeight: 'bold' },
                    ]}
                    value={totalWeight.toFixed(3)}
                    editable={false}
                  />
                </View>

                {/* Verified Wt */}
                <View style={[styles.inlineFieldRow, { flex: 1, minWidth: 200 }]}>
                  <ThemedText style={[styles.inlineFieldLabel, { width: 95 }]}>Verified Wt (g)</ThemedText>
                  <TextInput
                    style={[
                      styles.formInput,
                      styles.readonlyInput,
                      styles.highlightTotal,
                      { flex: 1, minWidth: 90, textAlign: 'right', fontWeight: 'bold' },
                    ]}
                    value={verifiedWeight.toFixed(3)}
                    editable={false}
                  />
                </View>

                {/* Total Pieces */}
                <View style={[styles.inlineFieldRow, { flex: 1, minWidth: 180 }]}>
                  <ThemedText style={[styles.inlineFieldLabel, { width: 85 }]}>Total Items</ThemedText>
                  <TextInput
                    style={[styles.formInput, styles.readonlyInput, { flex: 1, minWidth: 70, textAlign: 'right' }]}
                    value={`${selectedCount} / ${totalCount}`}
                    editable={false}
                  />
                </View>

                {/* Total Cost Value */}
                <View style={[styles.inlineFieldRow, { flex: 1, minWidth: 205 }]}>
                  <ThemedText style={[styles.inlineFieldLabel, { width: 95 }]}>Total Value ($)</ThemedText>
                  <TextInput
                    style={[
                      styles.formInput,
                      styles.readonlyInput,
                      styles.highlightTotal,
                      { flex: 1, minWidth: 90, textAlign: 'right', fontWeight: 'bold' },
                    ]}
                    value={totalCostSum.toFixed(2)}
                    editable={false}
                  />
                </View>
              </View>
            </View>
          </View>

          {/* CARD 3: INVENTORY ITEMS TO RECEIVE */}
          <View style={[styles.goldCard, { marginTop: 10 }]}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerTitleContainer}>
                <View style={styles.headerIconBadgeSmall}>
                  <Ionicons name="cube-outline" size={13} color="#B48218" />
                </View>
                <ThemedText style={styles.cardHeaderTitleSmall}>Incoming Items for Intake</ThemedText>
                <View style={styles.counterBadge}>
                  <ThemedText style={styles.counterBadgeText}>{gridData.length} items</ThemedText>
                </View>
              </View>

              <ThemedText style={styles.cardInstruction}>
                Tip: Click row or scan barcode to toggle verified status
              </ThemedText>
            </View>

            {/* Table Header */}
            <View style={styles.tableHeadRow}>
              <ThemedText style={[styles.tableHeadCell, { flex: 1.8 }]}>Inventory Code</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 2.6 }]}>Description</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 1, textAlign: 'right' }]}>Weight (g)</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 0.9, textAlign: 'center' }]}>Size</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { flex: 1.2, textAlign: 'right' }]}>Total Cost ($)</ThemedText>
              <ThemedText style={[styles.tableHeadCell, { width: 90, textAlign: 'center' }]}>Verified</ThemedText>
            </View>

            {/* Table Body */}
            {gridData.length === 0 ? (
              <View style={styles.emptyTableContainer}>
                <Ionicons name="archive-outline" size={32} color="#D4C3A3" />
                <ThemedText style={styles.emptyTableText}>
                  No items loaded. Enter a Transfer No above and click "Fetch" to load incoming stock.
                </ThemedText>
              </View>
            ) : (
              gridData.map((item, index) => {
                const isSelected = selectedRowIndex === index;
                const isVerified = Boolean(item.TransferTag);
                return (
                  <TouchableOpacity
                    key={`${item.InventoryCode}-${index}`}
                    style={[
                      styles.tableRow,
                      isSelected && styles.tableRowSelected,
                      isVerified && styles.tableRowVerified,
                    ]}
                    onPress={() => toggleRowSelection(index)}
                    activeOpacity={0.8}
                  >
                    <ThemedText style={[styles.tableCell, styles.codeCell, { flex: 1.8 }]}>
                      {item.InventoryCode}
                    </ThemedText>
                    <ThemedText style={[styles.tableCell, { flex: 2.6 }]} numberOfLines={1}>
                      {item.Description || '-'}
                    </ThemedText>
                    <ThemedText style={[styles.tableCell, { flex: 1, textAlign: 'right', fontWeight: 'bold' }]}>
                      {Number(item.Weight || 0).toFixed(3)}
                    </ThemedText>
                    <ThemedText style={[styles.tableCell, { flex: 0.9, textAlign: 'center' }]}>
                      {item.SizeCode || '-'}
                    </ThemedText>
                    <ThemedText style={[styles.tableCell, styles.amountCell, { flex: 1.2, textAlign: 'right' }]}>
                      {Number(item.TotalCost || 0).toFixed(2)}
                    </ThemedText>
                    <View style={{ width: 90, alignItems: 'center', justifyContent: 'center' }}>
                      {isVerified ? (
                        <View style={styles.verifiedBadge}>
                          <Ionicons name="checkmark-circle" size={13} color="#2E7D32" style={{ marginRight: 3 }} />
                          <ThemedText style={styles.verifiedText}>Scanned</ThemedText>
                        </View>
                      ) : (
                        <View style={styles.pendingBadge}>
                          <Ionicons name="ellipse-outline" size={11} color="#C62828" style={{ marginRight: 3 }} />
                          <ThemedText style={styles.pendingText}>Pending</ThemedText>
                        </View>
                      )}
                    </View>
                  </TouchableOpacity>
                );
              })
            )}

            {/* SCANNER SUBFORM ENTRY BAR */}
            <View style={styles.itemEntryBar}>
              <View style={styles.itemEntryRow}>
                <View style={[styles.inlineFieldRow, { flex: 1.6, minWidth: 260 }]}>
                  <ThemedText style={[styles.inlineFieldLabel, { width: 110 }]}>Scan Barcode *</ThemedText>
                  <TextInput
                    ref={barcodeInputRef}
                    style={[styles.formInput, { flex: 1, minWidth: 120 }]}
                    value={inventoryCodeInput}
                    onChangeText={setInventoryCodeInput}
                    onSubmitEditing={handleScanInventory}
                    placeholder="Scan or enter inventory code to verify..."
                    placeholderTextColor="#9E8D77"
                  />
                </View>

                <TouchableOpacity
                  style={[styles.tableActionBtn, styles.verifyBtn, { height: 26, paddingHorizontal: 14 }]}
                  onPress={handleScanInventory}
                  activeOpacity={0.8}
                >
                  <Ionicons name="barcode-outline" size={14} color="#9E6D08" style={{ marginRight: 4 }} />
                  <ThemedText style={styles.verifyBtnText}>Verify Item</ThemedText>
                </TouchableOpacity>

                <TouchableOpacity
                  style={[
                    styles.tableActionBtn,
                    styles.verifyAllBtn,
                    { height: 26, paddingHorizontal: 12, marginLeft: 4 },
                  ]}
                  onPress={() => {
                    const allVerified = gridData.map((it) => ({ ...it, TransferTag: true }));
                    setGridData(allVerified);
                    showToast('Marked all items as verified', 'success');
                  }}
                  activeOpacity={0.8}
                  disabled={gridData.length === 0}
                >
                  <Ionicons name="checkmark-done-outline" size={14} color="#7D5E19" style={{ marginRight: 4 }} />
                  <ThemedText style={styles.verifyAllBtnText}>Verify All</ThemedText>
                </TouchableOpacity>
              </View>
            </View>
          </View>
        </ScrollView>
      </KeyboardAvoidingView>

      {/* SALESMAN PASSWORD AUTHORIZATION MODAL */}
      <Modal
        visible={showSalesmanModal}
        transparent={true}
        animationType="fade"
        onRequestClose={() => setShowSalesmanModal(false)}
      >
        <View style={styles.modalOverlay}>
          <View style={styles.modalDialog}>
            <View style={styles.modalHeader}>
              <Ionicons name="shield-checkmark" size={18} color="#B48218" style={{ marginRight: 8 }} />
              <ThemedText style={styles.modalTitle}>Salesman Authorization Required</ThemedText>
            </View>

            <ThemedText style={styles.modalDescription}>
              Please enter the salesman authorization password to confirm intake and transfer inventory into local branch stock.
            </ThemedText>

            <View style={{ marginVertical: 14 }}>
              <ThemedText style={styles.modalLabel}>Salesman Password *</ThemedText>
              <TextInput
                ref={passwordInputRef}
                style={styles.modalInput}
                value={salesmanPassword}
                onChangeText={setSalesmanPassword}
                secureTextEntry={true}
                placeholder="Enter password..."
                placeholderTextColor="#9E8D77"
                onSubmitEditing={handleVerifySalesmanAndAccept}
              />
            </View>

            <View style={styles.modalActions}>
              <TouchableOpacity
                style={[styles.modalBtn, styles.modalCancelBtn]}
                onPress={() => setShowSalesmanModal(false)}
                activeOpacity={0.8}
                disabled={verifyingSalesman}
              >
                <ThemedText style={styles.modalCancelBtnText}>Cancel</ThemedText>
              </TouchableOpacity>

              <TouchableOpacity
                style={[styles.modalBtn, styles.modalConfirmBtn]}
                onPress={handleVerifySalesmanAndAccept}
                activeOpacity={0.8}
                disabled={verifyingSalesman}
              >
                {verifyingSalesman ? (
                  <ActivityIndicator size="small" color="#FFFFFF" />
                ) : (
                  <>
                    <Ionicons name="checkmark-done" size={14} color="#FFFFFF" style={{ marginRight: 4 }} />
                    <ThemedText style={styles.modalConfirmBtnText}>Verify & Accept</ThemedText>
                  </>
                )}
              </TouchableOpacity>
            </View>
          </View>
        </View>
      </Modal>

      {/* BROWSE PENDING TRANSFERS MODAL */}
      <Modal
        visible={showSearchModal}
        transparent={true}
        animationType="fade"
        onRequestClose={() => setShowSearchModal(false)}
      >
        <View style={styles.modalOverlay}>
          <View style={[styles.modalDialog, { maxWidth: 640 }]}>
            <View style={styles.modalHeader}>
              <Ionicons name="list-outline" size={18} color="#B48218" style={{ marginRight: 8 }} />
              <ThemedText style={styles.modalTitle}>Pending Inbound Transfer Vouchers</ThemedText>
            </View>

            <View style={{ marginTop: 10, marginBottom: 8 }}>
              <TextInput
                style={styles.modalInput}
                value={pendingSearchQuery}
                onChangeText={handleFilterPending}
                placeholder="Search by transfer no, source location or remarks..."
                placeholderTextColor="#9E8D77"
              />
            </View>

            <ScrollView style={{ maxHeight: 320, marginVertical: 6 }}>
              {loadingPendingList ? (
                <View style={{ padding: 24, alignItems: 'center' }}>
                  <ActivityIndicator size="small" color="#B48218" />
                  <ThemedText style={{ color: '#7D5E19', fontSize: 12, marginTop: 8 }}>Loading pending transfers...</ThemedText>
                </View>
              ) : pendingList.length === 0 ? (
                <View style={{ padding: 24, alignItems: 'center' }}>
                  <Ionicons name="checkmark-circle-outline" size={32} color="#B48218" />
                  <ThemedText style={{ color: '#7D5E19', fontSize: 13, marginTop: 6, fontWeight: 'bold' }}>
                    No pending inbound transfers found.
                  </ThemedText>
                </View>
              ) : (
                pendingList.map((p) => (
                  <TouchableOpacity
                    key={p.TransferNo}
                    style={styles.pendingItemCard}
                    onPress={() => handleSelectPendingTransfer(p)}
                    activeOpacity={0.8}
                  >
                    <View style={{ flexDirection: 'row', justifyContent: 'space-between', alignItems: 'center' }}>
                      <ThemedText style={{ fontSize: 13, fontWeight: 'bold', color: '#9E6D08' }}>
                        #{p.TransferNo}
                      </ThemedText>
                      <ThemedText style={{ fontSize: 11, color: '#6A5636' }}>
                        {p.TransferDate ? String(p.TransferDate).substring(0, 10) : ''}
                      </ThemedText>
                    </View>
                    <View style={{ flexDirection: 'row', justifyContent: 'space-between', marginTop: 4 }}>
                      <ThemedText style={{ fontSize: 11, color: '#4A3B22' }}>
                        From: <ThemedText style={{ fontWeight: 'bold' }}>{p.FromLocation}</ThemedText> → To: <ThemedText style={{ fontWeight: 'bold' }}>{p.ToLocation || 'HQ'}</ThemedText>
                      </ThemedText>
                      <ThemedText style={{ fontSize: 11, color: '#4A3B22', fontWeight: 'bold' }}>
                        {p.TotalPcs} pcs | {Number(p.TotalWeight || 0).toFixed(3)}g
                      </ThemedText>
                    </View>
                    {Boolean(p.Remarks) && (
                      <ThemedText style={{ fontSize: 10, color: '#887352', fontStyle: 'italic', marginTop: 2 }} numberOfLines={1}>
                        Note: {p.Remarks}
                      </ThemedText>
                    )}
                  </TouchableOpacity>
                ))
              )}
            </ScrollView>

            <View style={[styles.modalActions, { marginTop: 10 }]}>
              <TouchableOpacity
                style={[styles.modalBtn, styles.modalCancelBtn, { width: 100 }]}
                onPress={() => setShowSearchModal(false)}
                activeOpacity={0.8}
              >
                <ThemedText style={styles.modalCancelBtnText}>Close</ThemedText>
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
  searchBtn: {
    backgroundColor: '#FFFFFF',
    borderColor: '#DFD5C3',
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
  fetchBtn: {
    backgroundColor: '#FFFFFF',
    borderColor: '#DFD5C3',
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
    paddingHorizontal: 12,
    paddingVertical: 7,
    borderBottomWidth: 1,
    borderBottomColor: '#E8DCB8',
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    flexWrap: 'wrap',
    gap: 6,
  },
  headerTitleContainer: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  headerIconBadgeSmall: {
    width: 22,
    height: 22,
    borderRadius: 4,
    backgroundColor: '#FFFFFF',
    justifyContent: 'center',
    alignItems: 'center',
    marginRight: 6,
    borderWidth: 1,
    borderColor: '#E8DCB8',
  },
  cardHeaderTitleSmall: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#3B2D17',
    letterSpacing: 0.3,
  },
  cardInstruction: {
    fontSize: 10,
    color: '#7D5E19',
    fontStyle: 'italic',
  },
  transferBadge: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFF8E7',
    paddingHorizontal: 8,
    paddingVertical: 2,
    borderRadius: 4,
    borderWidth: 1,
    borderColor: '#E2CA8E',
  },
  transferBadgeSuccess: {
    backgroundColor: '#E8F5E9',
    borderColor: '#81C784',
  },
  transferBadgeLabel: {
    fontSize: 10,
    color: '#7D5E19',
    marginRight: 4,
  },
  transferBadgeValue: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#9E6D08',
  },
  counterBadge: {
    backgroundColor: '#FFFFFF',
    paddingHorizontal: 6,
    paddingVertical: 1,
    borderRadius: 10,
    marginLeft: 6,
    borderWidth: 1,
    borderColor: '#E8DCB8',
  },
  counterBadgeText: {
    fontSize: 10,
    fontWeight: 'bold',
    color: '#9E6D08',
  },
  cardBody: {
    padding: 10,
  },

  // ===== FORM GRID & INPUTS =====
  fourColumnGrid: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    gap: 8,
  },
  singleColumnRow: {
    width: '100%',
  },
  inlineFieldRow: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  inlineFieldLabel: {
    fontSize: 11,
    fontWeight: '600',
    color: '#4A3B22',
  },
  formInput: {
    height: 28,
    borderWidth: 1,
    borderColor: '#D4C3A3',
    borderRadius: 4,
    paddingHorizontal: 8,
    fontSize: 11,
    color: '#2A1F10',
    backgroundColor: '#FFFFFF',
  },
  inputWithLookup: {
    flexDirection: 'row',
    alignItems: 'center',
    flex: 1,
  },
  inputAttached: {
    borderTopRightRadius: 0,
    borderBottomRightRadius: 0,
    borderRightWidth: 0,
  },
  attachedLookupBtn: {
    width: 28,
    height: 28,
    backgroundColor: '#F5E7CA',
    borderWidth: 1,
    borderColor: '#D4C3A3',
    borderTopRightRadius: 4,
    borderBottomRightRadius: 4,
    justifyContent: 'center',
    alignItems: 'center',
  },
  readonlyInput: {
    backgroundColor: '#F7F3EB',
    color: '#5C4A28',
    borderColor: '#E2D5C0',
  },
  highlightTotal: {
    backgroundColor: '#FFF9E6',
    borderColor: '#D4AF37',
    color: '#9E6D08',
  },

  // ===== PROGRESS PILL =====
  statusPill: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 8,
    paddingVertical: 4,
    borderRadius: 4,
    borderWidth: 1,
  },
  statusPillPending: {
    backgroundColor: '#FFEBEE',
    borderColor: '#FFCDD2',
  },
  statusPillReady: {
    backgroundColor: '#E8F5E9',
    borderColor: '#C8E6C9',
  },
  statusPillText: {
    fontSize: 10,
    fontWeight: 'bold',
  },
  statusPillTextPending: {
    color: '#C62828',
  },
  statusPillTextReady: {
    color: '#2E7D32',
  },

  // ===== TABLE DESIGN =====
  tableHeadRow: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#F9F5EC',
    paddingVertical: 6,
    paddingHorizontal: 10,
    borderBottomWidth: 1,
    borderBottomColor: '#E8DCB8',
  },
  tableHeadCell: {
    fontSize: 10,
    fontWeight: 'bold',
    color: '#6A5636',
    textTransform: 'uppercase',
  },
  tableRow: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingVertical: 6,
    paddingHorizontal: 10,
    borderBottomWidth: 1,
    borderBottomColor: '#F0E8D8',
    backgroundColor: '#FFFFFF',
  },
  tableRowSelected: {
    backgroundColor: '#FFF8E6',
  },
  tableRowVerified: {
    backgroundColor: '#F6FCF7',
  },
  tableCell: {
    fontSize: 11,
    color: '#3B2D17',
  },
  codeCell: {
    fontWeight: 'bold',
    color: '#9E6D08',
  },
  amountCell: {
    fontWeight: '600',
    color: '#2E7D32',
  },
  verifiedBadge: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#E8F5E9',
    borderColor: '#A5D6A7',
    borderWidth: 1,
    paddingHorizontal: 6,
    paddingVertical: 2,
    borderRadius: 4,
  },
  verifiedText: {
    fontSize: 9,
    fontWeight: 'bold',
    color: '#2E7D32',
  },
  pendingBadge: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFEBEE',
    borderColor: '#FFCDD2',
    borderWidth: 1,
    paddingHorizontal: 6,
    paddingVertical: 2,
    borderRadius: 4,
  },
  pendingText: {
    fontSize: 9,
    fontWeight: 'bold',
    color: '#C62828',
  },
  emptyTableContainer: {
    padding: 24,
    alignItems: 'center',
    justifyContent: 'center',
  },
  emptyTableText: {
    fontSize: 11,
    color: '#9E8D77',
    textAlign: 'center',
    marginTop: 6,
  },

  // ===== ITEM ENTRY BAR =====
  itemEntryBar: {
    backgroundColor: '#F9F6F0',
    borderTopWidth: 1,
    borderTopColor: '#E8DCB8',
    padding: 8,
  },
  itemEntryRow: {
    flexDirection: 'row',
    alignItems: 'center',
    flexWrap: 'wrap',
    gap: 6,
  },
  tableActionBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    borderRadius: 4,
    borderWidth: 1,
  },
  verifyBtn: {
    backgroundColor: '#F5E7CA',
    borderColor: '#B8903B',
  },
  verifyBtnText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#9E6D08',
  },
  verifyAllBtn: {
    backgroundColor: '#FFFFFF',
    borderColor: '#DFD5C3',
  },
  verifyAllBtnText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#7D5E19',
  },

  // ===== TOAST NOTIFICATION =====
  toast: {
    position: 'absolute',
    top: 56,
    left: '25%',
    right: '25%',
    zIndex: 9999,
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 14,
    paddingVertical: 8,
    borderRadius: 6,
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
  toastWarning: {
    backgroundColor: '#F57C00',
  },
  toastText: {
    color: '#FFFFFF',
    fontSize: 12,
    fontWeight: '600',
    flex: 1,
  },

  // ===== MODAL STYLING =====
  modalOverlay: {
    flex: 1,
    backgroundColor: 'rgba(0,0,0,0.5)',
    justifyContent: 'center',
    alignItems: 'center',
    padding: 16,
  },
  modalDialog: {
    width: '100%',
    maxWidth: 440,
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    padding: 16,
    elevation: 8,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.15,
    shadowRadius: 8,
  },
  modalHeader: {
    flexDirection: 'row',
    alignItems: 'center',
    borderBottomWidth: 1,
    borderBottomColor: '#E8DCB8',
    paddingBottom: 8,
  },
  modalTitle: {
    fontSize: 14,
    fontWeight: 'bold',
    color: '#3B2D17',
  },
  modalDescription: {
    fontSize: 11,
    color: '#6A5636',
    marginTop: 8,
    lineHeight: 16,
  },
  modalLabel: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#4A3B22',
    marginBottom: 4,
  },
  modalInput: {
    height: 32,
    borderWidth: 1,
    borderColor: '#D4C3A3',
    borderRadius: 4,
    paddingHorizontal: 8,
    fontSize: 12,
    color: '#2A1F10',
    backgroundColor: '#FFFFFF',
  },
  modalActions: {
    flexDirection: 'row',
    justifyContent: 'flex-end',
    gap: 8,
    marginTop: 10,
  },
  modalBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    paddingHorizontal: 14,
    paddingVertical: 6,
    borderRadius: 4,
    borderWidth: 1,
  },
  modalCancelBtn: {
    backgroundColor: '#FFFFFF',
    borderColor: '#DFD5C3',
  },
  modalCancelBtnText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#5C4A28',
  },
  modalConfirmBtn: {
    backgroundColor: '#B48218',
    borderColor: '#8D6409',
  },
  modalConfirmBtnText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#FFFFFF',
  },
  pendingItemCard: {
    backgroundColor: '#FCFAF6',
    borderWidth: 1,
    borderColor: '#E8DCB8',
    borderRadius: 6,
    padding: 8,
    marginBottom: 6,
  },
});
