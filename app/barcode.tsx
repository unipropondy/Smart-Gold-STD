import { ThemedText } from '@/components/themed-text';
import { API_BASE_URL } from '@/constants/api';
import { useThemeContext } from '@/contexts/ThemeContext';
import { useThemeColors } from '@/hooks/useThemeColors';
import { Ionicons } from '@expo/vector-icons';
import { Stack, useLocalSearchParams, useRouter } from 'expo-router';
import React, { useEffect, useRef, useState } from 'react';
import {
  ActivityIndicator,
  FlatList,
  Modal,
  Platform,
  ScrollView,
  StyleSheet,
  TextInput,
  TouchableOpacity,
  TouchableWithoutFeedback,
  View,
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';

const API_URL = `${API_BASE_URL}/barcode`;

export default function BarcodeScreen() {
  const themeColors = useThemeColors();
  const { accentColor } = useThemeContext();
  const { background: backgroundColor, card: cardColor, text: textColor, border: borderColor, primaryGold: GOLD_COLOR, subtitle: subtitleColor } = themeColors;
  const router = useRouter();
  const params = useLocalSearchParams<{ inventoryCode?: string }>();

  // Field states matching frmBarcode.vb
  const [company, setCompany] = useState('');
  const [shortCode, setShortCode] = useState('');
  const [defaultCompany, setDefaultCompany] = useState('');
  const [barCode, setBarCode] = useState('');
  const [inventoryCode, setInventoryCode] = useState(params.inventoryCode ? String(params.inventoryCode).trim() : '');
  const [description, setDescription] = useState('');
  const [weight, setWeight] = useState('');
  const [category, setCategory] = useState('');
  const [initialCategory, setInitialCategory] = useState('');
  const [goldLoss, setGoldLoss] = useState('');
  const [smc, setSmc] = useState('');
  const [classCode, setClassCode] = useState('');
  const [trayId, setTrayId] = useState('');
  const [newTrayId, setNewTrayId] = useState('');
  const [noOfLabels, setNoOfLabels] = useState('1');
  const [loading, setLoading] = useState(false);
  const [printing, setPrinting] = useState(false);

  // Print Preview Dialog
  const [showPrintModal, setShowPrintModal] = useState(false);
  const [printData, setPrintData] = useState<any>(null);

  // Focus refs
  const inventoryCodeRef = useRef<TextInput>(null);
  const newTrayIdRef = useRef<TextInput>(null);
  const noOfLabelsRef = useRef<TextInput>(null);

  // Barcode scanner buffer & listener for hardware/USB scanners
  const scannerBuffer = useRef<string>('');
  const lastKeyTime = useRef<number>(0);

  useEffect(() => {
    if (Platform.OS === 'web' && typeof window !== 'undefined') {
      const handleKeyDown = (e: KeyboardEvent) => {
        // Keyboard shortcuts matching frmBarcode.vb: F6 = Clear, F9 = Print
        if (e.key === 'F6') {
          e.preventDefault();
          handleClear(true);
          return;
        }
        if (e.key === 'F9') {
          e.preventDefault();
          handlePrint();
          return;
        }

        const now = Date.now();
        const diff = now - lastKeyTime.current;
        lastKeyTime.current = now;

        // Scanners typically send keystrokes with < 50ms intervals
        if (diff > 100) {
          scannerBuffer.current = '';
        }

        if (e.key === 'Enter') {
          if (scannerBuffer.current.length >= 3) {
            e.preventDefault();
            const scannedCode = scannerBuffer.current.trim();
            scannerBuffer.current = '';
            setInventoryCode(scannedCode);
            fetchInventoryDetails(scannedCode);
          }
        } else if (e.key.length === 1 && !e.ctrlKey && !e.altKey && !e.metaKey) {
          scannerBuffer.current += e.key;
        }
      };

      window.addEventListener('keydown', handleKeyDown);
      return () => window.removeEventListener('keydown', handleKeyDown);
    }
  }, []);

  // Fetch company details on mount matching frmBarcode.vb Load
  useEffect(() => {
    const fetchCompany = async () => {
      try {
        const res = await fetch(`${API_URL}/company`);
        const json = await res.json();
        if (json.success && json.company) {
          setCompany(json.company);
          setDefaultCompany(json.company);
          setShortCode(json.shortCode || '');
        }
      } catch (err) {
        console.warn('Failed to fetch company:', err);
      }
    };
    fetchCompany();
    if (params.inventoryCode && String(params.inventoryCode).trim()) {
      fetchInventoryDetails(String(params.inventoryCode).trim());
    }
  }, [params.inventoryCode]);

  // In-App Toast Notification
  const [toast, setToast] = useState<{ visible: boolean; message: string; type: 'success' | 'error' | 'info' }>({
    visible: false,
    message: '',
    type: 'success',
  });

  const showToast = (message: string, type: 'success' | 'error' | 'info' = 'success') => {
    setToast({ visible: true, message, type });
    setTimeout(() => {
      setToast((prev) => ({ ...prev, visible: false }));
    }, 3500);
  };

  // LOV State
  const [lovType, setLovType] = useState<'inventory' | 'category' | 'tray' | null>(null);
  const [isLOVVisible, setIsLOVVisible] = useState(false);
  const [lovData, setLovData] = useState<any[]>([]);
  const [lovSearch, setLovSearch] = useState('');
  const [lovLoading, setLovLoading] = useState(false);

  // Fetch LOVs
  const fetchLOV = async (type: 'inventory' | 'category' | 'tray') => {
    setLovType(type);
    setLovLoading(true);
    setLovSearch('');
    setIsLOVVisible(true);
    setLovData([]);
    try {
      const res = await fetch(`${API_URL}/lov/${type}`);
      const json = await res.json();
      if (json.success) {
        setLovData(json.data || []);
      } else {
        showToast(json.message || `Failed to fetch ${type}`, 'error');
      }
    } catch (err) {
      console.error('LOV Error:', err);
      showToast('Network error loading LOV', 'error');
    } finally {
      setLovLoading(false);
    }
  };

  const filteredLovData = React.useMemo(() => {
    if (!lovSearch.trim()) return lovData;
    const lower = lovSearch.toLowerCase();
    return lovData.filter((item) =>
      Object.values(item).some((val) => String(val || '').toLowerCase().includes(lower))
    );
  }, [lovData, lovSearch]);

  // subGetInventory logic matching frmBarcode.vb
  const fetchInventoryDetails = async (code: string) => {
    if (!code || !code.trim()) return;
    setLoading(true);
    try {
      const res = await fetch(`${API_URL}/inventory/${encodeURIComponent(code.trim())}`);
      const json = await res.json();
      if (json.success && json.data) {
        const item = json.data;
        setBarCode(item.InventoryCode || code.trim());
        setDescription(item.Description || '');
        setWeight(item.Weight != null ? Number(item.Weight).toFixed(2) : '');
        setGoldLoss(item.GoldLoss != null ? Number(item.GoldLoss).toFixed(2) : '0.00');
        setSmc(item.CalculatedSMC || item.SMC?.toString() || '');
        setClassCode(item.ClassCode || '');
        setTrayId(item.TrayId || '');
        setNewTrayId('');
        const cat = item.CategoryCode || '';
        setCategory(cat);
        setInitialCategory(cat);
        if (noOfLabelsRef.current) {
          setTimeout(() => noOfLabelsRef.current?.focus(), 100);
        }
      } else {
        if (json.noStock) {
          showToast('No Stock for this InventoryCode', 'error');
        } else {
          showToast(json.message || 'InventoryCode Not Found', 'error');
        }
        handleClear(false);
        setTimeout(() => inventoryCodeRef.current?.focus(), 150);
      }
    } catch (err) {
      console.error(err);
      showToast('Failed to fetch inventory details', 'error');
    } finally {
      setLoading(false);
    }
  };

  // btnNew_Click matching frmBarcode.vb
  const handleClear = (fullClear = true) => {
    if (fullClear) {
      setCompany(defaultCompany || '');
      setInventoryCode('');
    }
    setBarCode('');
    setDescription('');
    setWeight('');
    setCategory('');
    setInitialCategory('');
    setGoldLoss('');
    setSmc('');
    setClassCode('');
    setTrayId('');
    setNewTrayId('');
    setNoOfLabels('1');
    if (fullClear) {
      setTimeout(() => inventoryCodeRef.current?.focus(), 100);
    }
  };

  // btnPrint_Click matching frmBarcode.vb validations and workflows
  const handlePrint = async () => {
    if (!inventoryCode || !inventoryCode.trim()) {
      showToast('Please Enter InventoryCode', 'error');
      inventoryCodeRef.current?.focus();
      return;
    }
    if (!smc || smc.trim() === '') {
      showToast('Please Enter SMC', 'error');
      return;
    }
    if (!trayId && !newTrayId) {
      showToast('Please Enter Tray ID', 'error');
      return;
    }
    if (!classCode || !classCode.trim()) {
      showToast('Please Enter Class', 'error');
      return;
    }
    if (!description || !description.trim()) {
      showToast('Please Enter Description', 'error');
      return;
    }
    if (!weight || weight.trim() === '') {
      showToast('Please Enter Weight', 'error');
      return;
    }
    if (!barCode || !barCode.trim()) {
      showToast('Please Enter BarCode', 'error');
      return;
    }

    setPrinting(true);
    try {
      const response = await fetch(`${API_URL}/print`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          inventoryCode: inventoryCode.trim(),
          newTrayId: newTrayId.trim(),
          oldTrayId: trayId.trim(),
          categoryCode: category.trim(),
          oldCategoryCode: initialCategory.trim(),
          noOfLabels: parseInt(noOfLabels, 10) || 1,
          company: company.trim(),
          shortCode: shortCode.trim(),
          description: description.trim(),
          weight: weight.trim(),
          goldLoss: goldLoss.trim(),
          smc: smc.trim(),
          classCode: classCode.trim(),
        }),
      });

      const json = await response.json();
      if (json.success) {
        setPrintData(json);
        setShowPrintModal(true);
        showToast(json.message || 'Barcode printed successfully!', 'success');
      } else {
        showToast(json.message || 'Error in updation.', 'error');
      }
    } catch (err: any) {
      console.error('Print Error:', err);
      showToast('Error in updation.', 'error');
    } finally {
      setPrinting(false);
    }
  };

  const handlePrintConfirm = () => {
    setShowPrintModal(false);
    if (Platform.OS === 'web' && typeof window !== 'undefined') {
      window.print();
    }
    handleClear(true);
  };

  // Keyboard Shortcuts (F6: Clear, F9: Print, Esc: Close)
  useEffect(() => {
    if (Platform.OS === 'web') {
      const handleKeyDown = (e: KeyboardEvent) => {
        if (e.key === 'F6') {
          e.preventDefault();
          handleClear(true);
        } else if (e.key === 'F9') {
          e.preventDefault();
          handlePrint();
        } else if (e.key === 'Escape') {
          if (showPrintModal) {
            setShowPrintModal(false);
          } else if (isLOVVisible) {
            setIsLOVVisible(false);
          } else {
            router.back();
          }
        }
      };
      window.addEventListener('keydown', handleKeyDown);
      return () => window.removeEventListener('keydown', handleKeyDown);
    }
  }, [
    inventoryCode,
    smc,
    trayId,
    newTrayId,
    classCode,
    description,
    weight,
    company,
    barCode,
    category,
    initialCategory,
    noOfLabels,
    isLOVVisible,
    showPrintModal,
  ]);

  const renderLOVItem = ({ item }: { item: any }) => {
    if (lovType === 'inventory') {
      return (
        <TouchableOpacity
          style={styles.lovRow}
          onPress={() => {
            setInventoryCode(item.InventoryCode);
            setIsLOVVisible(false);
            fetchInventoryDetails(item.InventoryCode);
          }}
        >
          <ThemedText style={[styles.tableCellText, { width: 110, fontWeight: 'bold' }]}>
            {item.InventoryCode}
          </ThemedText>
          <ThemedText style={[styles.tableCellText, { flex: 2 }]} numberOfLines={1}>
            {item.Description || '-'}
          </ThemedText>
          <ThemedText style={[styles.tableCellText, { width: 85, textAlign: 'center' }]}>
            {item.SupplierCode || '-'}
          </ThemedText>
          <ThemedText
            style={[styles.tableCellText, { width: 75, textAlign: 'right', fontWeight: 'bold', color: '#8D6409' }]}
          >
            {item.Weight != null ? Number(item.Weight).toFixed(2) : '-'}
          </ThemedText>
        </TouchableOpacity>
      );
    } else if (lovType === 'category') {
      return (
        <TouchableOpacity
          style={styles.lovItem}
          onPress={() => {
            setCategory(item.CategoryCode);
            setIsLOVVisible(false);
          }}
        >
          <ThemedText style={styles.lovItemTitle}>{item.CategoryCode}</ThemedText>
          <ThemedText style={styles.lovItemSub}>{item.CategoryName}</ThemedText>
        </TouchableOpacity>
      );
    } else {
      return (
        <TouchableOpacity
          style={styles.lovItem}
          onPress={() => {
            setNewTrayId(item.TrayId);
            setIsLOVVisible(false);
          }}
        >
          <ThemedText style={styles.lovItemTitle}>{item.TrayId}</ThemedText>
          <ThemedText style={styles.lovItemSub}>{item.TrayName}</ThemedText>
        </TouchableOpacity>
      );
    }
  };

  return (
    <SafeAreaView style={[styles.container, { backgroundColor: '#FBF9F5' }]} edges={['top', 'bottom', 'left', 'right']}>
      <Stack.Screen options={{ headerShown: false }} />

      {/* Floating In-App Toast Banner */}
      {toast.visible && (
        <View style={[styles.toastBanner, toast.type === 'error' ? styles.toastError : styles.toastSuccess]}>
          <Ionicons
            name={toast.type === 'error' ? 'alert-circle' : 'checkmark-circle'}
            size={18}
            color="#FFFFFF"
            style={{ marginRight: 8 }}
          />
          <ThemedText style={styles.toastText}>{toast.message}</ThemedText>
        </View>
      )}

      {/* Top Header Bar */}
      <View style={styles.header}>
        <View style={styles.headerLeft}>
          <TouchableOpacity style={styles.backButton} onPress={() => router.back()}>
            <Ionicons name="arrow-back" size={18} color="#3B2D17" />
          </TouchableOpacity>
          <ThemedText style={styles.title}>Barcode</ThemedText>
        </View>
        <View style={styles.toolbarBtns}>
          <TouchableOpacity style={styles.headerBtnSecondary} onPress={() => handleClear(true)}>
            <ThemedText style={styles.headerBtnSecondaryText}>Clear</ThemedText>
          </TouchableOpacity>
          <TouchableOpacity
            style={styles.headerBtnPreview}
            onPress={() => {
              if (!inventoryCode || !inventoryCode.trim()) {
                showToast('Please Enter InventoryCode', 'error');
                inventoryCodeRef.current?.focus();
                return;
              }
              setPrintData({
                printerModel: 'On-Screen Label Preview',
                labelData: {
                  company: company || 'SRI PINANG',
                  description: description || 'N/A',
                  smc: smc || '0.00',
                  trayId: newTrayId || trayId || 'N/A',
                  classCode: classCode || 'N/A',
                  weight: weight || '0.00',
                  inventoryCode: barCode || inventoryCode,
                  noOfLabels: noOfLabels || '1'
                }
              });
              setShowPrintModal(true);
            }}
          >
            <Ionicons name="eye-outline" size={13} color="#8D6007" style={{ marginRight: 4 }} />
            <ThemedText style={styles.headerBtnPreviewText}>Preview</ThemedText>
          </TouchableOpacity>
          <TouchableOpacity
            style={[styles.headerBtnPrimary, printing && { opacity: 0.7 }]}
            onPress={handlePrint}
            disabled={printing}
          >
            <ThemedText style={styles.headerBtnPrimaryText}>
              {printing ? 'Printing...' : 'Print'}
            </ThemedText>
          </TouchableOpacity>
        </View>
      </View>

      <ScrollView contentContainerStyle={styles.scrollContent}>
        {loading && <ActivityIndicator size="large" color="#9E6D08" style={styles.loader} />}

        <View style={styles.horizontalWrapper}>
          {/* Form Card (Left) */}
          <View style={styles.formCard}>
            <View style={styles.cardHeaderBanner}>
              <View style={styles.headerIconBadge}>
                <Ionicons name="document-text-outline" size={13} color="#9E6D08" />
              </View>
              <ThemedText style={styles.cardHeaderTitle}>Barcode Generation Details</ThemedText>
            </View>

            <View style={styles.cardBody}>
              {/* Company */}
              <View style={styles.formRow}>
                <ThemedText style={styles.label}>Company</ThemedText>
                <TextInput style={styles.inputDisabled} value={company} editable={false} />
              </View>

              {/* BarCode */}
              <View style={styles.formRow}>
                <ThemedText style={styles.label}>BarCode</ThemedText>
                <TextInput style={styles.inputDisabled} value={barCode} editable={false} />
              </View>

              {/* Inventory Code */}
              <View style={styles.formRow}>
                <ThemedText style={styles.label}>Inventory Code</ThemedText>
                <View style={styles.rowInputGroup}>
                  <TextInput
                    ref={inventoryCodeRef}
                    style={[styles.input, { borderTopRightRadius: 0, borderBottomRightRadius: 0, borderRightWidth: 0 }]}
                    value={inventoryCode}
                    onChangeText={setInventoryCode}
                    onBlur={() => fetchInventoryDetails(inventoryCode)}
                    onSubmitEditing={() => fetchInventoryDetails(inventoryCode)}
                    autoCapitalize="characters"
                    placeholder="Enter or select Inventory Code"
                    placeholderTextColor="#A99C89"
                  />
                  <TouchableOpacity style={styles.searchBtn} onPress={() => fetchLOV('inventory')}>
                    <ThemedText style={styles.searchBtnText}>...</ThemedText>
                  </TouchableOpacity>
                </View>
              </View>

              {/* Description */}
              <View style={styles.formRow}>
                <ThemedText style={styles.label}>Description</ThemedText>
                <TextInput style={styles.inputDisabled} value={description} editable={false} />
              </View>

              {/* Weight & Category Row */}
              <View style={styles.formRowMulti}>
                <View style={[styles.flexRow, { flex: 1.1 }]}>
                  <ThemedText style={styles.label}>Weight</ThemedText>
                  <TextInput style={[styles.inputDisabled, { textAlign: 'right', minWidth: 60 }]} value={weight} editable={false} />
                </View>
                <View style={[styles.flexRow, { flex: 1.2, marginLeft: 10 }]}>
                  <ThemedText style={styles.labelMulti}>Category</ThemedText>
                  <View style={styles.rowInputGroup}>
                    <TextInput
                      style={[styles.input, { borderTopRightRadius: 0, borderBottomRightRadius: 0, borderRightWidth: 0, minWidth: 60 }]}
                      value={category}
                      onChangeText={setCategory}
                      autoCapitalize="characters"
                    />
                    <TouchableOpacity style={styles.searchBtn} onPress={() => fetchLOV('category')}>
                      <ThemedText style={styles.searchBtnText}>...</ThemedText>
                    </TouchableOpacity>
                  </View>
                </View>
              </View>

              {/* Gold Loss */}
              <View style={styles.formRow}>
                <ThemedText style={styles.label}>Gold Loss</ThemedText>
                <TextInput style={[styles.inputDisabled, { textAlign: 'right' }]} value={goldLoss} editable={false} />
              </View>

              {/* SMC */}
              <View style={styles.formRow}>
                <ThemedText style={styles.label}>SMC</ThemedText>
                <TextInput style={[styles.inputDisabled, { textAlign: 'right' }]} value={smc} editable={false} />
              </View>

              {/* Class */}
              <View style={styles.formRow}>
                <ThemedText style={styles.label}>Class</ThemedText>
                <TextInput style={styles.inputDisabled} value={classCode} editable={false} />
              </View>

              {/* Tray ID */}
              <View style={styles.formRow}>
                <ThemedText style={styles.label}>Tray ID</ThemedText>
                <View style={styles.rowInputGroup}>
                  <TextInput style={[styles.inputDisabled, { flex: 1, marginRight: 6 }]} value={trayId} editable={false} />
                  <TextInput
                    ref={newTrayIdRef}
                    style={[styles.input, { flex: 1, borderTopRightRadius: 0, borderBottomRightRadius: 0, borderRightWidth: 0 }]}
                    value={newTrayId}
                    onChangeText={setNewTrayId}
                    placeholder="New Tray"
                    placeholderTextColor="#A99C89"
                  />
                  <TouchableOpacity style={styles.searchBtn} onPress={() => fetchLOV('tray')}>
                    <ThemedText style={styles.searchBtnText}>...</ThemedText>
                  </TouchableOpacity>
                </View>
              </View>

              {/* No of Labels */}
              <View style={styles.formRow}>
                <ThemedText style={styles.label}>No of Labels</ThemedText>
                <TextInput
                  ref={noOfLabelsRef}
                  style={[styles.input, { maxWidth: 100, flex: 0, width: 100, textAlign: 'center' }]}
                  value={noOfLabels}
                  onChangeText={setNoOfLabels}
                  keyboardType="numeric"
                />
              </View>
            </View>
          </View>

          {/* Live Barcode Label Card (Right) - Only shows when barcode is populated/generated */}
          {barCode && barCode.trim() ? (
            <View style={styles.previewContainer}>
              <View style={styles.previewHeaderRow}>
                <View style={styles.headerIconBadge}>
                  <Ionicons name="barcode-outline" size={13} color="#9E6D08" />
                </View>
                <ThemedText style={styles.previewHeaderTitle}>Live Barcode Label (No Printer Needed)</ThemedText>
              </View>

              <View style={styles.labelCard}>
                <ThemedText style={styles.labelCompHeader}>{company || 'SRI PINANG'}</ThemedText>
                <ThemedText style={styles.labelDesc}>{description || 'ITEM DESCRIPTION'}</ThemedText>

                <View style={styles.labelDetailsRow}>
                  <ThemedText style={styles.labelDetailText}>
                    MC: <ThemedText style={{ fontWeight: 'bold' }}>{smc || '0.00'}</ThemedText>
                  </ThemedText>
                  <ThemedText style={styles.labelDetailText}>
                    Tray: <ThemedText style={{ fontWeight: 'bold' }}>{newTrayId || trayId || '-'}</ThemedText>
                  </ThemedText>
                </View>

                <View style={styles.labelDetailsRow}>
                  <ThemedText style={styles.labelDetailText}>
                    Class: <ThemedText style={{ fontWeight: 'bold' }}>{classCode || '-'}</ThemedText>
                  </ThemedText>
                  <ThemedText style={styles.labelDetailText}>
                    Wt: <ThemedText style={{ fontWeight: 'bold' }}>{weight ? `${weight} gm` : '-'}</ThemedText>
                  </ThemedText>
                </View>

                {/* Graphical Barcode visualization */}
                <View style={styles.barcodeVisual}>
                  <View style={styles.barcodeLines}>
                    {[...Array(38)].map((_, i) => (
                      <View
                        key={i}
                        style={{
                          width: (i % 4 === 0 ? 3 : i % 2 === 0 ? 2 : 1),
                          height: 38,
                          backgroundColor: '#1A1A1A',
                          marginHorizontal: 1,
                        }}
                      />
                    ))}
                  </View>
                  <ThemedText style={styles.barcodeTextVal}>{barCode}</ThemedText>
                </View>

                <View style={styles.previewFootnote}>
                  <Ionicons name="checkmark-circle-outline" size={13} color="#276749" style={{ marginRight: 5 }} />
                  <ThemedText style={styles.previewFootnoteText}>
                    Live barcode generated for {barCode} ({noOfLabels || 1} {parseInt(noOfLabels || '1', 10) > 1 ? 'copies' : 'copy'})
                  </ThemedText>
                </View>
              </View>
            </View>
          ) : null}
        </View>
      </ScrollView>

      {/* LOV Modal */}
      <Modal visible={isLOVVisible} animationType="fade" transparent={true}>
        <TouchableWithoutFeedback onPress={() => setIsLOVVisible(false)}>
          <View style={styles.modalOverlay}>
            <TouchableWithoutFeedback>
              <View style={[styles.modalContainer, { backgroundColor: '#FFFFFF', borderColor: '#E8DCB8' }]}>
                <View style={styles.modalHeader}>
                  <ThemedText style={styles.modalTitle}>Select {lovType}</ThemedText>
                  <TouchableOpacity onPress={() => setIsLOVVisible(false)}>
                    <Ionicons name="close" size={18} color="#3B2D17" />
                  </TouchableOpacity>
                </View>

                {/* Filter Search Input */}
                <View style={styles.modalSearchBox}>
                  <Ionicons name="search" size={14} color="#8D6409" />
                  <TextInput
                    style={styles.modalSearchInput}
                    value={lovSearch}
                    onChangeText={setLovSearch}
                    placeholder={`Search ${lovType || 'items'}...`}
                    placeholderTextColor="#A99C89"
                    autoFocus
                  />
                  {lovSearch ? (
                    <TouchableOpacity onPress={() => setLovSearch('')}>
                      <Ionicons name="close-circle" size={14} color="#999" />
                    </TouchableOpacity>
                  ) : null}
                </View>

                {/* Inventory Table Header */}
                {lovType === 'inventory' && (
                  <View style={styles.tableHeadRow}>
                    <ThemedText style={[styles.tableHeadText, { width: 110 }]}>InventoryCode</ThemedText>
                    <ThemedText style={[styles.tableHeadText, { flex: 2 }]}>Description</ThemedText>
                    <ThemedText style={[styles.tableHeadText, { width: 85, textAlign: 'center' }]}>SupplierCode</ThemedText>
                    <ThemedText style={[styles.tableHeadText, { width: 75, textAlign: 'right' }]}>Weight</ThemedText>
                  </View>
                )}

                {lovLoading ? (
                  <ActivityIndicator size="large" color="#9E6D08" style={{ padding: 20 }} />
                ) : filteredLovData.length === 0 ? (
                  <ThemedText style={{ padding: 20, textAlign: 'center', color: '#8F7E66' }}>No data found</ThemedText>
                ) : (
                  <FlatList
                    data={filteredLovData}
                    keyExtractor={(item, index) => index.toString()}
                    renderItem={renderLOVItem}
                    style={styles.lovList}
                    keyboardShouldPersistTaps="handled"
                  />
                )}
              </View>
            </TouchableWithoutFeedback>
          </View>
        </TouchableWithoutFeedback>
      </Modal>

      {/* Barcode Print Preview & Confirmation Modal */}
      <Modal visible={showPrintModal} animationType="fade" transparent={true}>
        <TouchableWithoutFeedback onPress={() => setShowPrintModal(false)}>
          <View style={styles.modalOverlay}>
            <TouchableWithoutFeedback>
              <View style={[styles.modalContainer, { maxWidth: 440, backgroundColor: '#FFFFFF', borderColor: '#E8DCB8' }]}>
                <View style={styles.modalHeader}>
                  <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                    <Ionicons name="print-outline" size={16} color="#3B2D17" style={{ marginRight: 6 }} />
                    <ThemedText style={styles.modalTitle}>Barcode Print Preview</ThemedText>
                  </View>
                  <TouchableOpacity onPress={() => setShowPrintModal(false)}>
                    <Ionicons name="close" size={18} color="#3B2D17" />
                  </TouchableOpacity>
                </View>

                <View style={{ padding: 16 }}>
                  {printData?.labelData && (
                    <View style={styles.printLabelPreview}>
                      <ThemedText style={styles.labelCompHeader}>{printData.labelData.company}</ThemedText>
                      <ThemedText style={styles.labelDesc}>{printData.labelData.description}</ThemedText>

                      <View style={styles.labelDetailsRow}>
                        <ThemedText style={styles.labelDetailText}>
                          MC: <ThemedText style={{ fontWeight: 'bold' }}>{printData.labelData.smc}</ThemedText>
                        </ThemedText>
                        <ThemedText style={styles.labelDetailText}>
                          Tray: <ThemedText style={{ fontWeight: 'bold' }}>{printData.labelData.trayId}</ThemedText>
                        </ThemedText>
                      </View>

                      <View style={styles.labelDetailsRow}>
                        <ThemedText style={styles.labelDetailText}>
                          Class: <ThemedText style={{ fontWeight: 'bold' }}>{printData.labelData.classCode}</ThemedText>
                        </ThemedText>
                        <ThemedText style={styles.labelDetailText}>
                          Wt: <ThemedText style={{ fontWeight: 'bold' }}>{printData.labelData.weight} gm</ThemedText>
                        </ThemedText>
                      </View>

                      {/* Barcode representation */}
                      <View style={styles.barcodeVisual}>
                        <View style={styles.barcodeLines}>
                          {[...Array(32)].map((_, i) => (
                            <View
                              key={i}
                              style={{
                                width: (i % 3 === 0 ? 3 : 1.5),
                                height: 34,
                                backgroundColor: '#000000',
                                marginHorizontal: 1,
                              }}
                            />
                          ))}
                        </View>
                        <ThemedText style={styles.barcodeTextVal}>{printData.labelData.inventoryCode}</ThemedText>
                      </View>

                      <ThemedText style={styles.printTargetInfo}>
                        Printer: {printData.printerModel || 'POSTEK G-2108'} | Copies: {printData.labelData.noOfLabels}
                      </ThemedText>
                    </View>
                  )}

                  <View style={styles.printModalFooter}>
                    <TouchableOpacity
                      style={[styles.toolbarBtn, { paddingHorizontal: 16 }]}
                      onPress={() => setShowPrintModal(false)}
                    >
                      <ThemedText style={styles.toolbarBtnText}>Cancel</ThemedText>
                    </TouchableOpacity>
                    <TouchableOpacity
                      style={[styles.toolbarBtn, { backgroundColor: '#A26E08', borderColor: '#8D6007', paddingHorizontal: 20 }]}
                      onPress={handlePrintConfirm}
                    >
                      <ThemedText style={[styles.toolbarBtnText, { color: '#FFFFFF' }]}>Send to Printer</ThemedText>
                    </TouchableOpacity>
                  </View>
                </View>
              </View>
            </TouchableWithoutFeedback>
          </View>
        </TouchableWithoutFeedback>
      </Modal>
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  container: { flex: 1, backgroundColor: '#FBF9F5' },
  header: {
    height: 48,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingHorizontal: 16,
    backgroundColor: '#FFFFFF',
    borderBottomWidth: 1,
    borderBottomColor: '#E8DCB8',
  },
  headerLeft: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  backButton: {
    width: 32,
    height: 32,
    borderRadius: 6,
    borderWidth: 1,
    borderColor: '#DFD5C3',
    backgroundColor: '#FFFFFF',
    alignItems: 'center',
    justifyContent: 'center',
    marginRight: 10,
  },
  title: {
    fontSize: 16,
    fontWeight: 'bold',
    color: '#3B2D17',
    fontFamily: Platform.OS === 'web' ? 'Cambria, Georgia, serif' : undefined,
  },
  toolbarBtns: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 8,
    flexShrink: 0,
  },
  headerBtnSecondary: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    paddingVertical: 6,
    paddingHorizontal: 14,
    borderRadius: 4,
    borderWidth: 1,
    borderColor: '#DFD5C3',
    backgroundColor: '#FFFFFF',
    minHeight: 32,
  },
  headerBtnSecondaryText: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#5C4A28',
  },
  headerBtnPreview: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    paddingVertical: 6,
    paddingHorizontal: 14,
    borderRadius: 4,
    backgroundColor: '#FAF5EA',
    borderWidth: 1,
    borderColor: '#D8C39E',
    minHeight: 32,
  },
  headerBtnPreviewText: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#8D6007',
  },
  headerBtnPrimary: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    paddingVertical: 6,
    paddingHorizontal: 16,
    borderRadius: 4,
    backgroundColor: '#A26E08',
    borderWidth: 1,
    borderColor: '#8D6007',
    minHeight: 32,
  },
  headerBtnPrimaryText: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#FFFFFF',
  },
  toolbarBtn: {
    paddingVertical: 4,
    paddingHorizontal: 12,
    borderRadius: 4,
    borderWidth: 1,
    borderColor: '#DFD5C3',
    backgroundColor: '#FFFFFF',
    alignItems: 'center',
    justifyContent: 'center',
    height: 30,
  },
  toolbarBtnText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#5C4A28',
  },
  scrollContent: {
    padding: 16,
    flexGrow: 1,
    paddingBottom: 40,
    alignItems: 'center',
  },
  loader: { marginVertical: 6 },

  // Horizontal Side-by-Side Wrapper
  horizontalWrapper: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    justifyContent: 'center',
    alignItems: 'flex-start',
    gap: 16,
    width: '100%',
    maxWidth: 1100,
  },

  // Toast Banner
  toastBanner: {
    position: 'absolute',
    top: 48,
    alignSelf: 'center',
    zIndex: 9999,
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 14,
    paddingVertical: 8,
    borderRadius: 20,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.2,
    shadowRadius: 6,
    elevation: 6,
  },
  toastError: {
    backgroundColor: '#C53030',
  },
  toastSuccess: {
    backgroundColor: '#276749',
  },
  toastText: {
    color: '#FFFFFF',
    fontSize: 12,
    fontWeight: 'bold',
  },

  // Compact Form Card (Left)
  formCard: {
    flex: 1,
    minWidth: 320,
    maxWidth: 520,
    backgroundColor: '#FFFFFF',
    borderRadius: 6,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    overflow: 'hidden',
    shadowColor: '#A38035',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.05,
    shadowRadius: 4,
    elevation: 1,
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
    fontSize: 12,
    fontWeight: 'bold',
    color: '#3B2D17',
    letterSpacing: 0.3,
  },
  cardBody: {
    padding: 14,
    gap: 8,
  },
  formRow: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  formRowMulti: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 12,
  },
  flexRow: {
    flexDirection: 'row',
    alignItems: 'center',
    flex: 1,
  },
  label: {
    width: 115,
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3A2E19',
    flexShrink: 0,
  },
  labelMulti: {
    width: 65,
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3A2E19',
    flexShrink: 0,
  },
  input: {
    flex: 1,
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderRadius: 4,
    paddingHorizontal: 8,
    paddingVertical: 2,
    fontSize: 11,
    height: 26,
    backgroundColor: '#FFFFFF',
    color: '#2C2519',
  },
  inputDisabled: {
    flex: 1,
    borderWidth: 1,
    borderColor: '#EADBCA',
    borderRadius: 4,
    paddingHorizontal: 8,
    paddingVertical: 2,
    fontSize: 11,
    height: 26,
    backgroundColor: '#F8F5EE',
    color: '#554736',
  },
  rowInputGroup: {
    flex: 1,
    flexDirection: 'row',
    alignItems: 'center',
  },
  searchBtn: {
    width: 26,
    height: 26,
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderLeftWidth: 1,
    justifyContent: 'center',
    alignItems: 'center',
    borderTopRightRadius: 4,
    borderBottomRightRadius: 4,
    backgroundColor: '#FAF5EA',
  },
  searchBtnText: {
    color: '#6E5528',
    fontWeight: 'bold',
    fontSize: 11,
  },

  // Modal
  modalOverlay: {
    flex: 1,
    backgroundColor: 'rgba(0,0,0,0.45)',
    justifyContent: 'center',
    alignItems: 'center',
    padding: 14,
  },
  modalContainer: {
    width: '100%',
    maxWidth: 520,
    maxHeight: '80%',
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#E8DCB8',
    borderRadius: 6,
    overflow: 'hidden',
  },
  modalHeader: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    paddingHorizontal: 12,
    paddingVertical: 8,
    backgroundColor: '#F5E7CA',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
  },
  modalTitle: {
    fontSize: 13,
    fontWeight: 'bold',
    color: '#3B2D17',
    textTransform: 'capitalize',
  },
  modalSearchBox: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 8,
    paddingVertical: 5,
    backgroundColor: '#FAF7F0',
    borderBottomWidth: 1,
    borderBottomColor: '#EADBCA',
    gap: 6,
  },
  modalSearchInput: {
    flex: 1,
    fontSize: 11,
    color: '#2C2519',
    height: 24,
    paddingHorizontal: 6,
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderRadius: 4,
  },
  tableHeadRow: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#F8F3E8',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    paddingVertical: 5,
    paddingHorizontal: 8,
  },
  tableHeadText: {
    fontSize: 10,
    fontWeight: 'bold',
    color: '#5C4A28',
  },
  lovList: {
    maxHeight: 280,
  },
  lovRow: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingVertical: 6,
    paddingHorizontal: 8,
    borderBottomWidth: 1,
    borderBottomColor: '#F0E6D2',
  },
  tableCellText: {
    fontSize: 11,
    color: '#2C2519',
  },
  lovItem: {
    paddingVertical: 8,
    paddingHorizontal: 12,
    borderBottomWidth: 1,
    borderBottomColor: '#F0E6D2',
  },
  lovItemTitle: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#2C2519',
  },
  lovItemSub: {
    fontSize: 11,
    color: '#6E5D46',
    marginTop: 1,
  },

  // Live Preview Section (Right)
  previewContainer: {
    flex: 1,
    minWidth: 320,
    maxWidth: 480,
    backgroundColor: '#FFFFFF',
    borderRadius: 6,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    overflow: 'hidden',
    shadowColor: '#A38035',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.05,
    shadowRadius: 4,
    elevation: 1,
  },
  previewHeaderRow: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 12,
    paddingVertical: 8,
    backgroundColor: '#F5E7CA',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
  },
  previewHeaderTitle: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3B2D17',
  },
  labelCard: {
    padding: 12,
    alignItems: 'center',
    backgroundColor: '#FFFFFF',
  },
  previewFootnote: {
    flexDirection: 'row',
    alignItems: 'center',
    marginTop: 8,
    paddingTop: 6,
    borderTopWidth: 1,
    borderTopColor: '#EBE2D2',
    width: '100%',
    justifyContent: 'center',
  },
  previewFootnoteText: {
    fontSize: 10,
    color: '#276749',
    fontWeight: '500',
  },

  // Print Label Preview
  printLabelPreview: {
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#B8903B',
    borderRadius: 6,
    padding: 12,
    alignItems: 'center',
    borderStyle: 'dashed',
    marginBottom: 14,
  },
  labelCompHeader: {
    fontSize: 13,
    fontWeight: 'bold',
    color: '#3B2D17',
    marginBottom: 2,
    letterSpacing: 0.5,
  },
  labelDesc: {
    fontSize: 11,
    color: '#4A3B22',
    marginBottom: 8,
  },
  labelDetailsRow: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    width: '100%',
    paddingHorizontal: 14,
    marginBottom: 4,
  },
  labelDetailText: {
    fontSize: 11,
    color: '#3B2D17',
  },
  barcodeVisual: {
    alignItems: 'center',
    marginTop: 8,
    marginBottom: 6,
  },
  barcodeLines: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    marginBottom: 2,
  },
  barcodeTextVal: {
    fontSize: 11,
    fontWeight: 'bold',
    letterSpacing: 1.5,
    color: '#000000',
  },
  printTargetInfo: {
    fontSize: 10,
    color: '#8A7A64',
    marginTop: 4,
  },
  printModalFooter: {
    flexDirection: 'row',
    justifyContent: 'flex-end',
    gap: 8,
    borderTopWidth: 1,
    borderTopColor: '#EBE2D2',
    paddingTop: 12,
  },
});
