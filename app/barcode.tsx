import { ThemedText } from '@/components/themed-text';
import { API_BASE_URL } from '@/constants/api';
import { useThemeContext } from '@/contexts/ThemeContext';
import { useThemeColors } from '@/hooks/useThemeColors';
import { Ionicons } from '@expo/vector-icons';
import { Stack, useRouter } from 'expo-router';
import React, { useState } from 'react';
import {
  ActivityIndicator,
  Alert,
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
  const styles = React.useMemo(() => StyleSheet.create({
    container: { flex: 1, backgroundColor: '#FBF9F5' },
    header: {
      flexDirection: 'row',
      alignItems: 'center',
      justifyContent: 'space-between',
      paddingHorizontal: 16,
      paddingVertical: 10,
      backgroundColor: '#FFFFFF',
      borderBottomWidth: 1,
      borderBottomColor: '#EBE2D2',
    },
    backButton: {
      padding: 4,
      marginRight: 12,
    },
    title: {
      fontSize: 20,
      fontWeight: '500',
      color: '#3B2D17',
    },
    toolbarBtns: {
      flexDirection: 'row',
      gap: 8,
    },
    toolbarBtn: {
      paddingVertical: 6,
      paddingHorizontal: 14,
      borderRadius: 5,
      borderWidth: 1,
      borderColor: '#DFD5C3',
      backgroundColor: '#FFFFFF',
    },
    toolbarBtnText: {
      fontSize: 12,
      fontWeight: 'bold',
      color: '#5C4A28',
    },
    scrollContent: {
      padding: 16,
      alignItems: 'center',
      flexGrow: 1,
      paddingBottom: 60,
    },
    loader: { marginVertical: 10 },

    // Luxury Gold Card
    goldCard: {
      width: '100%',
      maxWidth: 700,
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
      marginTop: 8,
    },
    cardHeaderBanner: {
      flexDirection: 'row',
      alignItems: 'center',
      backgroundColor: '#F5E7CA',
      borderBottomWidth: 1,
      borderBottomColor: '#E2D3B3',
      paddingVertical: 8,
      paddingHorizontal: 14,
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
      padding: 16,
      gap: 8,
    },
    formRow: {
      flexDirection: 'row',
      alignItems: 'center',
    },
    formRowMulti: {
      flexDirection: 'row',
      alignItems: 'center',
      gap: 16,
    },
    flexRow: {
      flexDirection: 'row',
      alignItems: 'center',
      flex: 1,
    },
    label: {
      width: 125,
      fontSize: 12,
      fontWeight: 'bold',
      color: '#3A2E19',
      flexShrink: 0,
    },
    labelMulti: {
      width: 70,
      fontSize: 12,
      fontWeight: 'bold',
      color: '#3A2E19',
      flexShrink: 0,
    },
    input: {
      flex: 1,
      borderWidth: 1,
      borderColor: '#E2D7C5',
      borderRadius: 6,
      paddingHorizontal: 10,
      paddingVertical: 4,
      fontSize: 12,
      height: 30,
      backgroundColor: '#FFFFFF',
      color: '#2C2519',
      outlineStyle: 'none',
    } as any,
    inputDisabled: {
      flex: 1,
      borderWidth: 1,
      borderColor: '#EADBCA',
      borderRadius: 6,
      paddingHorizontal: 10,
      paddingVertical: 4,
      fontSize: 12,
      height: 30,
      backgroundColor: '#F8F5EE',
      color: '#554736',
      outlineStyle: 'none',
    } as any,
    rowInputGroup: {
      flex: 1,
      flexDirection: 'row',
      alignItems: 'center',
    },
    searchBtn: {
      width: 32,
      height: 30,
      borderWidth: 1,
      borderColor: '#E2D7C5',
      borderLeftWidth: 1,
      justifyContent: 'center',
      alignItems: 'center',
      borderTopRightRadius: 6,
      borderBottomRightRadius: 6,
      backgroundColor: '#FAF5EA',
    },
    searchBtnText: {
      color: '#6E5528',
      fontWeight: 'bold',
      fontSize: 12,
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
      width: '100%',
      maxWidth: 580,
      maxHeight: '80%',
      backgroundColor: '#FFFFFF',
      borderWidth: 1,
      borderColor: '#E8DCB8',
      borderRadius: 8,
      overflow: 'hidden',
    },
    modalHeader: {
      flexDirection: 'row',
      justifyContent: 'space-between',
      alignItems: 'center',
      paddingHorizontal: 14,
      paddingVertical: 10,
      backgroundColor: '#F5E7CA',
      borderBottomWidth: 1,
      borderBottomColor: '#E2D3B3',
    },
    modalTitle: {
      fontSize: 15,
      fontWeight: 'bold',
      color: '#3B2D17',
      textTransform: 'capitalize',
    },
    modalSearchBox: {
      flexDirection: 'row',
      alignItems: 'center',
      paddingHorizontal: 10,
      paddingVertical: 6,
      backgroundColor: '#FAF7F0',
      borderBottomWidth: 1,
      borderBottomColor: '#EADBCA',
      gap: 6,
    },
    modalSearchInput: {
      flex: 1,
      fontSize: 12,
      color: '#2C2519',
      height: 28,
      paddingHorizontal: 8,
      backgroundColor: '#FFFFFF',
      borderWidth: 1,
      borderColor: '#E2D7C5',
      borderRadius: 5,
      outlineStyle: 'none',
    } as any,
    // Table Header for Inventory LOV
    tableHeadRow: {
      flexDirection: 'row',
      alignItems: 'center',
      backgroundColor: '#F8F3E8',
      borderBottomWidth: 1,
      borderBottomColor: '#E2D3B3',
      paddingVertical: 6,
      paddingHorizontal: 10,
    },
    tableHeadText: {
      fontSize: 11,
      fontWeight: 'bold',
      color: '#5C4A28',
    },
    lovList: {
      paddingHorizontal: 0,
    },
    lovRow: {
      flexDirection: 'row',
      alignItems: 'center',
      paddingVertical: 8,
      paddingHorizontal: 10,
      borderBottomWidth: 1,
      borderBottomColor: '#F0E6D2',
    },
    tableCellText: {
      fontSize: 12,
      color: '#2C2519',
    },
    lovItem: {
      paddingVertical: 10,
      paddingHorizontal: 14,
      borderBottomWidth: 1,
      borderBottomColor: '#F0E6D2',
    },
    lovItemTitle: {
      fontSize: 13,
      fontWeight: 'bold',
      color: '#2C2519',
    },
    lovItemSub: {
      fontSize: 12,
      color: '#6E5D46',
      marginTop: 2,
    },
  }), []);

  const router = useRouter();
  const isDark = backgroundColor !== '#FFFFFF' && backgroundColor !== '#F5F5F5';

  const [company, setCompany] = useState('MILLION');
  const [barCode, setBarCode] = useState('');
  const [inventoryCode, setInventoryCode] = useState('');
  const [description, setDescription] = useState('');
  const [weight, setWeight] = useState('');
  const [category, setCategory] = useState('');
  const [goldLoss, setGoldLoss] = useState('');
  const [smc, setSmc] = useState('');
  const [classCode, setClassCode] = useState('');
  const [trayId, setTrayId] = useState('');
  const [newTrayId, setNewTrayId] = useState('');
  const [noOfLabels, setNoOfLabels] = useState('1');
  const [loading, setLoading] = useState(false);

  // LOV State
  const [lovType, setLovType] = useState<'inventory' | 'category' | 'tray' | null>(null);
  const [isLOVVisible, setIsLOVVisible] = useState(false);
  const [lovData, setLovData] = useState<any[]>([]);
  const [lovSearch, setLovSearch] = useState('');
  const [lovLoading, setLovLoading] = useState(false);

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
        Alert.alert('Error', json.message || `Failed to fetch ${type}`);
      }
    } catch (err) {
      console.error('LOV Error:', err);
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

  const fetchInventoryDetails = async (code: string) => {
    if (!code.trim()) return;
    setLoading(true);
    try {
      const res = await fetch(`${API_URL}/inventory/${encodeURIComponent(code)}`);
      const json = await res.json();
      if (json.success && json.data) {
        setBarCode(json.data.InventoryCode || '');
        setDescription(json.data.Description || '');
        setWeight(json.data.Weight?.toString() || '');
        setGoldLoss(json.data.GoldLoss?.toString() || '');
        setSmc(json.data.SMC?.toString() || json.data.SMc?.toString() || '');
        setClassCode(json.data.ClassCode || '');
        setTrayId(json.data.TrayId || '');
        if (json.data.CategoryCode) setCategory(json.data.CategoryCode);
      } else {
        const msg = json.message || 'InventoryCode Not Found';
        Platform.OS === 'web' ? window.alert(msg) : Alert.alert('Error', msg);
        handleClear(false);
      }
    } catch (err) {
      console.error(err);
      const msg = 'Failed to fetch inventory details';
      Platform.OS === 'web' ? window.alert(msg) : Alert.alert('Error', msg);
    } finally {
      setLoading(false);
    }
  };

  const handleClear = (fullClear = true) => {
    if (fullClear) {
      setCompany('MILLION');
      setInventoryCode('');
    }
    setBarCode('');
    setDescription('');
    setWeight('');
    setCategory('');
    setGoldLoss('');
    setSmc('');
    setClassCode('');
    setTrayId('');
    setNewTrayId('');
    setNoOfLabels('1');
  };

  const handlePrint = () => {
    if (!inventoryCode) {
      const msg = 'Please Enter InventoryCode';
      Platform.OS === 'web' ? window.alert(msg) : Alert.alert('Validation Error', msg);
      return;
    }
    const msg = 'Barcode printing simulated successfully!';
    Platform.OS === 'web' ? window.alert(msg) : Alert.alert('Success', msg);
    handleClear(true);
  };

  const renderLOVItem = ({ item }: { item: any }) => {
    if (lovType === 'inventory') {
      return (
        <TouchableOpacity
          style={[styles.lovRow, { borderBottomColor: borderColor }]}
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
          <ThemedText style={[styles.tableCellText, { width: 75, textAlign: 'right', fontWeight: 'bold', color: '#8D6409' }]}>
            {item.Weight != null ? Number(item.Weight).toFixed(2) : '-'}
          </ThemedText>
        </TouchableOpacity>
      );
    } else if (lovType === 'category') {
      return (
        <TouchableOpacity
          style={[styles.lovItem, { borderBottomColor: borderColor }]}
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
          style={[styles.lovItem, { borderBottomColor: borderColor }]}
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

  const inputStyle = [styles.input, { backgroundColor: backgroundColor, color: textColor, borderColor: '#CCC' }];
  const inputDisabledStyle = [styles.inputDisabled, { backgroundColor: backgroundColor, color: textColor, borderColor: '#CCC' }];
  const labelStyle = [styles.label, { color: textColor }];

  return (
    <SafeAreaView style={[styles.container, { backgroundColor: '#FBF9F5' }]} edges={['top', 'bottom', 'left', 'right']}>
      <Stack.Screen options={{ headerShown: false }} />
      {/* Custom Header */}
      <View style={styles.header}>
        <View style={{ flexDirection: 'row', alignItems: 'center' }}>
          <TouchableOpacity style={styles.backButton} onPress={() => router.back()}>
            <Ionicons name="arrow-back" size={22} color="#3B2D17" />
          </TouchableOpacity>
          <ThemedText style={styles.title}>Barcode</ThemedText>
        </View>
        <View style={styles.toolbarBtns}>
          <TouchableOpacity style={styles.toolbarBtn} onPress={() => handleClear(true)}>
            <ThemedText style={styles.toolbarBtnText}>Clear</ThemedText>
          </TouchableOpacity>
          <TouchableOpacity style={[styles.toolbarBtn, { backgroundColor: '#F9F2E2', borderColor: '#B8903B' }]} onPress={handlePrint}>
            <ThemedText style={[styles.toolbarBtnText, { color: '#9E6D08' }]}>Print</ThemedText>
          </TouchableOpacity>
        </View>
      </View>

      <ScrollView contentContainerStyle={styles.scrollContent}>
        {loading && <ActivityIndicator size="large" color="#9E6D08" style={styles.loader} />}

        {/* Luxury Gold Card */}
        <View style={styles.goldCard}>
          {/* Card Header Banner */}
          <View style={styles.cardHeaderBanner}>
            <View style={styles.headerIconBadge}>
              <Ionicons name="barcode-outline" size={15} color="#B48218" />
            </View>
            <ThemedText style={styles.cardHeaderTitle}>Barcode Generation Details</ThemedText>
          </View>

          <View style={styles.cardBody}>
            <View style={styles.formRow}>
              <ThemedText style={styles.label}>Company</ThemedText>
              <TextInput
                style={styles.input}
                value={company}
                onChangeText={setCompany}
              />
            </View>

            <View style={styles.formRow}>
              <ThemedText style={styles.label}>BarCode</ThemedText>
              <TextInput
                style={styles.inputDisabled}
                value={barCode}
                editable={false}
              />
            </View>

            <View style={styles.formRow}>
              <ThemedText style={styles.label}>Inventory Code</ThemedText>
              <View style={styles.rowInputGroup}>
                <TextInput
                  style={[styles.input, { borderTopRightRadius: 0, borderBottomRightRadius: 0, borderRightWidth: 0 }]}
                  value={inventoryCode}
                  onChangeText={setInventoryCode}
                  onBlur={() => fetchInventoryDetails(inventoryCode)}
                  autoCapitalize="characters"
                />
                <TouchableOpacity style={styles.searchBtn} onPress={() => fetchLOV('inventory')}>
                  <ThemedText style={styles.searchBtnText}>...</ThemedText>
                </TouchableOpacity>
              </View>
            </View>

            <View style={styles.formRow}>
              <ThemedText style={styles.label}>Description</ThemedText>
              <TextInput
                style={styles.inputDisabled}
                value={description}
                editable={false}
              />
            </View>

            <View style={styles.formRowMulti}>
              <View style={styles.flexRow}>
                <ThemedText style={styles.label}>Weight</ThemedText>
                <TextInput
                  style={[styles.inputDisabled, { textAlign: 'right' }]}
                  value={weight}
                  editable={false}
                />
              </View>
              <View style={styles.flexRow}>
                <ThemedText style={styles.labelMulti}>Category</ThemedText>
                <View style={styles.rowInputGroup}>
                  <TextInput
                    style={[styles.input, { borderTopRightRadius: 0, borderBottomRightRadius: 0, borderRightWidth: 0 }]}
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

            <View style={styles.formRow}>
              <ThemedText style={styles.label}>Gold Loss</ThemedText>
              <TextInput
                style={[styles.inputDisabled, { textAlign: 'right' }]}
                value={goldLoss}
                editable={false}
              />
            </View>

            <View style={styles.formRow}>
              <ThemedText style={styles.label}>SMC</ThemedText>
              <TextInput
                style={[styles.inputDisabled, { textAlign: 'right' }]}
                value={smc}
                editable={false}
              />
            </View>

            <View style={styles.formRow}>
              <ThemedText style={styles.label}>Class</ThemedText>
              <TextInput
                style={styles.inputDisabled}
                value={classCode}
                editable={false}
              />
            </View>

            <View style={styles.formRow}>
              <ThemedText style={styles.label}>Tray ID</ThemedText>
              <View style={styles.rowInputGroup}>
                <TextInput
                  style={[styles.inputDisabled, { flex: 1, marginRight: 8 }]}
                  value={trayId}
                  editable={false}
                />
                <TextInput
                  style={[styles.input, { flex: 1, borderTopRightRadius: 0, borderBottomRightRadius: 0, borderRightWidth: 0 }]}
                  value={newTrayId}
                  onChangeText={setNewTrayId}
                  placeholder="New Tray"
                  placeholderTextColor="#998973"
                />
                <TouchableOpacity style={styles.searchBtn} onPress={() => fetchLOV('tray')}>
                  <ThemedText style={styles.searchBtnText}>...</ThemedText>
                </TouchableOpacity>
              </View>
            </View>

            <View style={styles.formRow}>
              <ThemedText style={styles.label}>No of Labels</ThemedText>
              <TextInput
                style={[styles.input, { maxWidth: 120, flex: 0, width: 120 }]}
                value={noOfLabels}
                onChangeText={setNoOfLabels}
                keyboardType="numeric"
              />
            </View>
          </View>
        </View>
      </ScrollView>

      {/* LOV Modal */}
      <Modal visible={isLOVVisible} animationType="slide" transparent={true}>
        <TouchableWithoutFeedback onPress={() => setIsLOVVisible(false)}>
          <View style={styles.modalOverlay}>
            <TouchableWithoutFeedback>
              <View style={[styles.modalContainer, { backgroundColor: cardColor, borderColor }]}>
                <View style={[styles.modalHeader, { borderBottomColor: borderColor }]}>
                  <ThemedText style={styles.modalTitle}>Select {lovType}</ThemedText>
                  <TouchableOpacity onPress={() => setIsLOVVisible(false)}>
                    <Ionicons name="close" size={24} color={textColor} />
                  </TouchableOpacity>
                </View>

                {/* Filter Search Input */}
                <View style={styles.modalSearchBox}>
                  <Ionicons name="search" size={15} color="#8D6409" />
                  <TextInput
                    style={styles.modalSearchInput}
                    value={lovSearch}
                    onChangeText={setLovSearch}
                    placeholder={`Search ${lovType || 'items'}...`}
                    placeholderTextColor="#A99C89"
                  />
                  {lovSearch ? (
                    <TouchableOpacity onPress={() => setLovSearch('')}>
                      <Ionicons name="close-circle" size={16} color="#999" />
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
                  <ActivityIndicator size="large" color={GOLD_COLOR} style={{ padding: 20 }} />
                ) : filteredLovData.length === 0 ? (
                  <ThemedText style={{ padding: 20, textAlign: 'center' }}>No data found</ThemedText>
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
    </SafeAreaView>
  );
}


