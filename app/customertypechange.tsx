import { ThemedText } from '@/components/themed-text';
import { API_BASE_URL } from '@/constants/api';
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
  useColorScheme,
  View,
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';
import { useThemeColors } from '@/hooks/useThemeColors';
import { useThemeContext } from '@/contexts/ThemeContext';

const GOLD_COLOR = '#D4AF37';
const API_URL = `${API_BASE_URL}/customertypechange`;



export default function CustomerTypeChangeScreen() {
  const themeColors = useThemeColors();
  const { accentColor } = useThemeContext();
  const { background: backgroundColor, card: cardColor, text: textColor, border: borderColor, subtitle: subtitleColor, primaryGold: GOLD_COLOR, primaryGold } = themeColors;

  

  
  
  

  const colorScheme = useColorScheme();
  const isDark = colorScheme === 'dark';

  const headerBgColor = isDark ? '#1a1a1a' : '#ffffff';

  const router = useRouter();

  // Search parameters
  const [customerType, setCustomerType] = useState('RUBY');
  const [customerCode, setCustomerCode] = useState('');
  const [fromPoints, setFromPoints] = useState('');
  const [toPoints, setToPoints] = useState('');

  // Grid Data
  const [gridData, setGridData] = useState<any[]>([]);
  const [loading, setLoading] = useState(false);

  // Selection
  const [selectedCustomers, setSelectedCustomers] = useState<Record<string, boolean>>({});

  // Bottom section
  const [newCustomerType, setNewCustomerType] = useState('');
  const [pointsRatio, setPointsRatio] = useState('');

  // Dropdowns state
  const [isCustomerTypePickerVisible, setIsCustomerTypePickerVisible] = useState(false);
  const [isNewCustomerTypePickerVisible, setIsNewCustomerTypePickerVisible] = useState(false);
  const [isPointsPickerVisible, setIsPointsPickerVisible] = useState(false);

  // Lookup state
  const [isLookupVisible, setIsLookupVisible] = useState(false);
  const [searchQuery, setSearchQuery] = useState('');
  const [lookupData, setLookupData] = useState<any[]>([]);
  const [loadingLookup, setLoadingLookup] = useState(false);

  const [saving, setSaving] = useState(false);
  const [toast, setToast] = useState<{ message: string, type: 'success' | 'error' } | null>(null);

  const showToast = (message: string, type: 'success' | 'error' = 'success') => {
    setToast({ message, type });
    setTimeout(() => {
      setToast(null);
    }, 3000);
  };

  const types = ['RUBY', 'EMERALD', 'DIAMOND'];
  const pointsOptions = ['5/7', '5/9', '7/5', '7/9', '9/5', '9/7'];

  const calculatePointsRatio = (oldType: string, newType: string) => {
    if (oldType === 'RUBY') {
      if (newType === 'EMERALD') return '5/7';
      if (newType === 'DIAMOND') return '5/9';
    } else if (oldType === 'EMERALD') {
      if (newType === 'RUBY') return '7/5';
      if (newType === 'DIAMOND') return '7/9';
    } else if (oldType === 'DIAMOND') {
      if (newType === 'RUBY') return '9/5';
      if (newType === 'EMERALD') return '9/7';
    }
    return '';
  };

  const handleNewCustomerTypeSelect = (type: string) => {
    setNewCustomerType(type);
    setIsNewCustomerTypePickerVisible(false);
    setPointsRatio(calculatePointsRatio(customerType, type));
  };

  const handleFetch = async () => {
    if (!customerType) {
      showToast('Customer Type Should Not Be Blank.', 'error');
      return;
    }

    try {
      setLoading(true);
      const queryParams = new URLSearchParams({
        customerType: customerType,
        customerCode: customerCode,
        fromPoints: fromPoints,
        toPoints: toPoints
      });

      const res = await fetch(`${API_URL}/fetch?${queryParams.toString()}`);
      const json = await res.json();

      if (json.success) {
        setGridData(json.data || []);
        setSelectedCustomers({});
      } else {
        showToast(json.message || 'Failed to fetch data.', 'error');
      }
    } catch (err) {
      console.error(err);
      showToast('Network error occurred while fetching.', 'error');
    } finally {
      setLoading(false);
    }
  };

  const toggleSelectAll = () => {
    const allSelected = gridData.every(item => selectedCustomers[item.CustomerCode]);
    const newSelected: Record<string, boolean> = {};
    if (!allSelected) {
      gridData.forEach(item => {
        newSelected[item.CustomerCode] = true;
      });
    }
    setSelectedCustomers(newSelected);
  };

  const toggleSelectRow = (code: string) => {
    setSelectedCustomers(prev => ({
      ...prev,
      [code]: !prev[code]
    }));
  };

  const handleSave = async () => {
    if (!customerType) {
      showToast('Customer Type Should Not Be Blank.', 'error');
      return;
    }
    if (!newCustomerType) {
      showToast('New Customer Type Should Not Be Blank.', 'error');
      return;
    }
    if (customerType === newCustomerType) {
      showToast('Customer Type Should Not Be Same.', 'error');
      return;
    }

    const selectedRows = gridData.filter(item => selectedCustomers[item.CustomerCode]);
    if (selectedRows.length === 0) {
      showToast('Select Type Should Not Be Blank.', 'error');
      return;
    }

    let fPoints = 0, tPoints = 0;
    if (pointsRatio.includes('/')) {
      const parts = pointsRatio.split('/');
      fPoints = parseInt(parts[0], 10);
      tPoints = parseInt(parts[1], 10);
    }

    try {
      setSaving(true);
      const payload = {
        customers: selectedRows.map(r => ({ CustomerCode: r.CustomerCode, TotalPoints: r.TotalRewardPoints })),
        oldCustomerType: customerType,
        newCustomerType: newCustomerType,
        fPoints,
        tPoints
      };

      const res = await fetch(`${API_URL}/save`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload)
      });
      const json = await res.json();

      if (json.success) {
        showToast('Customer detail saved successfully.', 'success');
        handleClear();
      } else {
        showToast(json.message || 'Failed to save.', 'error');
      }
    } catch (err) {
      console.error(err);
      showToast('Network error occurred while saving.', 'error');
    } finally {
      setSaving(false);
    }
  };

  const handleClear = () => {
    setCustomerType('RUBY');
    setCustomerCode('');
    setFromPoints('');
    setToPoints('');
    setGridData([]);
    setSelectedCustomers({});
    setNewCustomerType('');
    setPointsRatio('');
  };

  const openLookup = async () => {
    setIsLookupVisible(true);
    try {
      setLoadingLookup(true);
      const res = await fetch(`${API_BASE_URL}/outletcustomer`); // reuse outlet customer lookup endpoint for basic customers
      const json = await res.json();
      if (json.success) {
        setLookupData(json.data || []);
      }
    } catch (err) {
      console.error(err);
    } finally {
      setLoadingLookup(false);
    }
  };

  const getCustomerTypeBadgeStyle = (type: string) => {
    switch ((type || '').toUpperCase()) {
      case 'RUBY':
        return {
          bg: isDark ? 'rgba(239, 68, 68, 0.15)' : '#FEF2F2',
          text: '#DC2626',
          border: '#FECACA',
          icon: 'diamond',
        };
      case 'EMERALD':
        return {
          bg: isDark ? 'rgba(16, 185, 129, 0.15)' : '#ECFDF5',
          text: '#059669',
          border: '#A7F3D0',
          icon: 'leaf',
        };
      case 'DIAMOND':
        return {
          bg: isDark ? 'rgba(14, 165, 233, 0.15)' : '#F0F9FF',
          text: '#0284C7',
          border: '#BAE6FD',
          icon: 'sparkles',
        };
      default:
        return {
          bg: isDark ? 'rgba(212, 175, 55, 0.15)' : '#FDF8ED',
          text: '#B48218',
          border: '#E8DCB8',
          icon: 'star',
        };
    }
  };

  const selectedCount = gridData.filter(item => selectedCustomers[item.CustomerCode]).length;

  return (
    <SafeAreaView style={[styles.container, { backgroundColor }]} edges={['bottom', 'left', 'right']}>
      <Stack.Screen
        options={{
          headerShown: true,
          title: 'Customer Type Change',
          headerStyle: { backgroundColor: headerBgColor },
          headerTintColor: textColor,
          headerRight: () => (
            <View style={{ flexDirection: 'row', gap: 10, paddingRight: 8, alignItems: 'center' }}>
              <TouchableOpacity
                style={[styles.headerActionBtn, { backgroundColor: isDark ? '#262626' : '#F8FAFC', borderColor: borderColor }]}
                onPress={handleClear}
                activeOpacity={0.7}
              >
                <Ionicons name="refresh-outline" size={14} color={textColor} style={{ marginRight: 4 }} />
                <ThemedText style={[styles.headerActionBtnText, { color: textColor }]}>New</ThemedText>
              </TouchableOpacity>
              <TouchableOpacity
                style={[styles.headerActionBtn, { backgroundColor: GOLD_COLOR, borderColor: GOLD_COLOR }]}
                onPress={handleSave}
                disabled={saving}
                activeOpacity={0.8}
              >
                {saving ? (
                  <ActivityIndicator size="small" color="#FFFFFF" />
                ) : (
                  <>
                    <Ionicons name="checkmark-done" size={15} color="#FFFFFF" style={{ marginRight: 4 }} />
                    <ThemedText style={[styles.headerActionBtnText, { color: '#FFFFFF' }]}>Save</ThemedText>
                  </>
                )}
              </TouchableOpacity>
            </View>
          )
        }}
      />

      <ScrollView contentContainerStyle={styles.scrollContent}>
        {loading && <ActivityIndicator size="large" color={GOLD_COLOR} style={{ marginBottom: 16 }} />}

        {/* Top Search Area */}
        <View style={[styles.card, { backgroundColor: cardColor, borderColor }]}>
          <View style={[styles.cardBanner, { backgroundColor: isDark ? 'rgba(212, 175, 55, 0.12)' : '#FDF8ED', borderBottomColor: borderColor }]}>
            <View style={styles.cardBannerLeft}>
              <View style={[styles.iconBadge, { backgroundColor: GOLD_COLOR }]}>
                <Ionicons name="filter" size={13} color="#FFFFFF" />
              </View>
              <ThemedText style={[styles.cardBannerTitle, { color: textColor }]}>Search & Filter</ThemedText>
            </View>
            {/* Quick Badge indicator */}
            {customerType && (
              <View style={[styles.typeBadgeSmall, { backgroundColor: getCustomerTypeBadgeStyle(customerType).bg, borderColor: getCustomerTypeBadgeStyle(customerType).border }]}>
                <Ionicons name={getCustomerTypeBadgeStyle(customerType).icon as any} size={11} color={getCustomerTypeBadgeStyle(customerType).text} />
                <ThemedText style={[styles.typeBadgeSmallText, { color: getCustomerTypeBadgeStyle(customerType).text }]}>
                  {customerType}
                </ThemedText>
              </View>
            )}
          </View>

          <View style={styles.cardBody}>
            <View style={styles.row}>
              {/* Customer Type Picker */}
              <View style={[styles.inputGroup, { flex: 1 }]}>
                <ThemedText style={[styles.label, { color: textColor }]}>Customer Type</ThemedText>
                <TouchableOpacity
                  style={[styles.input, styles.pickerSelectBtn, { borderColor, backgroundColor: cardColor }]}
                  onPress={() => setIsCustomerTypePickerVisible(true)}
                  activeOpacity={0.7}
                >
                  <View style={{ flexDirection: 'row', alignItems: 'center', gap: 6 }}>
                    <Ionicons name={getCustomerTypeBadgeStyle(customerType).icon as any} size={13} color={getCustomerTypeBadgeStyle(customerType).text} />
                    <ThemedText style={{ color: getCustomerTypeBadgeStyle(customerType).text, fontSize: 13, fontWeight: '700' }}>
                      {customerType}
                    </ThemedText>
                  </View>
                  <Ionicons name="chevron-down" size={15} color={GOLD_COLOR} />
                </TouchableOpacity>
              </View>

              {/* Customer Code Lookup */}
              <View style={[styles.inputGroup, { flex: 1.2 }]}>
                <ThemedText style={[styles.label, { color: textColor }]}>Customer Code</ThemedText>
                <View style={{ flexDirection: 'row', flex: 1, gap: 6 }}>
                  <TextInput
                    style={[styles.input, { color: textColor, borderColor, backgroundColor: cardColor }]}
                    placeholder="Enter code"
                    placeholderTextColor={subtitleColor}
                    value={customerCode}
                    onChangeText={setCustomerCode}
                    autoCapitalize="characters"
                  />
                  <TouchableOpacity style={[styles.lookupBtn, { backgroundColor: GOLD_COLOR, borderColor: GOLD_COLOR }]} onPress={openLookup} activeOpacity={0.8}>
                    <Ionicons name="search" size={14} color="#FFFFFF" />
                  </TouchableOpacity>
                </View>
              </View>
            </View>

            <View style={styles.row}>
              <View style={[styles.inputGroup, { flex: 1 }]}>
                <ThemedText style={[styles.label, { color: textColor }]}>From Points</ThemedText>
                <TextInput
                  style={[styles.input, { color: textColor, borderColor, backgroundColor: cardColor }]}
                  value={fromPoints}
                  placeholder="0"
                  placeholderTextColor={subtitleColor}
                  onChangeText={setFromPoints}
                  keyboardType="numeric"
                />
              </View>
              <View style={[styles.inputGroup, { flex: 1 }]}>
                <ThemedText style={[styles.label, { color: textColor }]}>To Points</ThemedText>
                <TextInput
                  style={[styles.input, { color: textColor, borderColor, backgroundColor: cardColor }]}
                  value={toPoints}
                  placeholder="10000"
                  placeholderTextColor={subtitleColor}
                  onChangeText={setToPoints}
                  keyboardType="numeric"
                />
              </View>
              <TouchableOpacity
                style={[styles.fetchBtn, { backgroundColor: GOLD_COLOR, opacity: loading ? 0.6 : 1 }]}
                onPress={handleFetch}
                disabled={loading}
                activeOpacity={0.8}
              >
                <Ionicons name="arrow-down-circle" size={15} color="#FFFFFF" style={{ marginRight: 5 }} />
                <ThemedText style={styles.fetchBtnText}>Fetch</ThemedText>
              </TouchableOpacity>
            </View>
          </View>
        </View>

        {/* Data Grid */}
        <View style={[styles.gridContainer, { backgroundColor: cardColor, borderColor }]}>
          <View style={[styles.gridTitleBar, { backgroundColor: isDark ? '#262626' : '#F8FAFC', borderBottomColor: borderColor }]}>
            <View style={{ flexDirection: 'row', alignItems: 'center', gap: 6 }}>
              <Ionicons name="people" size={16} color={GOLD_COLOR} />
              <ThemedText style={[styles.gridBarTitle, { color: textColor }]}>Customers List</ThemedText>
              {gridData.length > 0 && (
                <View style={styles.countBadge}>
                  <ThemedText style={styles.countBadgeText}>{gridData.length}</ThemedText>
                </View>
              )}
            </View>
            {gridData.length > 0 && (
              <ThemedText style={{ fontSize: 11, color: selectedCount > 0 ? '#10B981' : subtitleColor, fontWeight: '600' }}>
                {selectedCount} Selected
              </ThemedText>
            )}
          </View>

          <View style={[styles.gridHeader, { backgroundColor: isDark ? '#1F2937' : '#F1F5F9', borderBottomWidth: 1, borderBottomColor: borderColor }]}>
            <TouchableOpacity style={{ flex: 0.8, flexDirection: 'row', alignItems: 'center', gap: 4 }} onPress={toggleSelectAll}>
              <View style={[
                styles.checkbox,
                {
                  borderColor: gridData.length > 0 && gridData.every(item => selectedCustomers[item.CustomerCode]) ? GOLD_COLOR : borderColor,
                  backgroundColor: gridData.length > 0 && gridData.every(item => selectedCustomers[item.CustomerCode]) ? GOLD_COLOR : 'transparent'
                }
              ]}>
                {gridData.length > 0 && gridData.every(item => selectedCustomers[item.CustomerCode]) && (
                  <Ionicons name="checkmark" size={12} color="#FFF" />
                )}
              </View>
              <ThemedText style={[styles.gridHeaderText, { color: textColor }]}>All</ThemedText>
            </TouchableOpacity>
            <ThemedText style={[styles.gridHeaderText, { flex: 2, color: textColor }]}>Customer Code</ThemedText>
            <ThemedText style={[styles.gridHeaderText, { flex: 3.5, color: textColor }]}>Customer Name</ThemedText>
            <ThemedText style={[styles.gridHeaderText, { flex: 2, textAlign: 'right', color: textColor, paddingRight: 8 }]}>Total Points</ThemedText>
          </View>

          <FlatList
            data={gridData}
            keyExtractor={(item) => item.CustomerCode}
            scrollEnabled={false}
            ListEmptyComponent={() => (
              <View style={{ padding: 32, alignItems: 'center', gap: 8 }}>
                <Ionicons name="people-outline" size={32} color={subtitleColor} />
                <ThemedText style={{ color: subtitleColor, fontSize: 13 }}>No customers found. Click Fetch above.</ThemedText>
              </View>
            )}
            renderItem={({ item, index }) => {
              const isSelected = !!selectedCustomers[item.CustomerCode];
              const isEven = index % 2 === 0;
              const rowBg = isSelected
                ? (isDark ? 'rgba(212, 175, 55, 0.12)' : '#FEF9EE')
                : (isEven ? cardColor : (isDark ? '#1C1917' : '#FBFBFC'));

              return (
                <TouchableOpacity
                  activeOpacity={0.7}
                  onPress={() => toggleSelectRow(item.CustomerCode)}
                  style={[
                    styles.gridRow,
                    {
                      backgroundColor: rowBg,
                      borderBottomWidth: 1,
                      borderBottomColor: borderColor,
                    }
                  ]}
                >
                  <View style={{ flex: 0.8, alignItems: 'flex-start' }}>
                    <View style={[
                      styles.checkbox,
                      {
                        borderColor: isSelected ? GOLD_COLOR : borderColor,
                        backgroundColor: isSelected ? GOLD_COLOR : 'transparent'
                      }
                    ]}>
                      {isSelected && <Ionicons name="checkmark" size={12} color="#FFF" />}
                    </View>
                  </View>
                  <ThemedText style={[styles.gridCellText, { flex: 2, color: textColor, fontWeight: '600' }]} numberOfLines={1}>
                    {item.CustomerCode}
                  </ThemedText>
                  <ThemedText style={[styles.gridCellText, { flex: 3.5, color: textColor }]} numberOfLines={1}>
                    {item.CustomerName}
                  </ThemedText>
                  <View style={{ flex: 2, alignItems: 'flex-end', paddingRight: 8 }}>
                    <View style={styles.pointsPill}>
                      <ThemedText style={styles.pointsPillText} numberOfLines={1}>
                        {Number(item.TotalRewardPoints || 0).toLocaleString()}
                      </ThemedText>
                    </View>
                  </View>
                </TouchableOpacity>
              );
            }}
          />
        </View>

        {/* Bottom Section */}
        <View style={[styles.bottomSection, { backgroundColor: cardColor, borderColor }]}>
          <View style={[styles.bottomSectionBanner, { backgroundColor: isDark ? 'rgba(59, 130, 246, 0.1)' : '#EFF6FF', borderBottomColor: borderColor }]}>
            <View style={{ flexDirection: 'row', alignItems: 'center', gap: 6 }}>
              <View style={[styles.iconBadge, { backgroundColor: '#3B82F6' }]}>
                <Ionicons name="swap-vertical" size={13} color="#FFFFFF" />
              </View>
              <ThemedText style={[styles.cardBannerTitle, { color: textColor }]}>Conversion Settings</ThemedText>
            </View>
          </View>

          <View style={styles.bottomSectionBody}>
            <View style={[styles.inputGroup, { flex: 1.4 }]}>
              <ThemedText style={[styles.label, { width: 130, color: textColor }]}>New Customer Type</ThemedText>
              <TouchableOpacity
                style={[styles.input, styles.pickerSelectBtn, { borderColor, backgroundColor: cardColor }]}
                onPress={() => setIsNewCustomerTypePickerVisible(true)}
                activeOpacity={0.7}
              >
                {newCustomerType ? (
                  <View style={{ flexDirection: 'row', alignItems: 'center', gap: 6 }}>
                    <Ionicons name={getCustomerTypeBadgeStyle(newCustomerType).icon as any} size={13} color={getCustomerTypeBadgeStyle(newCustomerType).text} />
                    <ThemedText style={{ color: getCustomerTypeBadgeStyle(newCustomerType).text, fontSize: 13, fontWeight: '700' }}>
                      {newCustomerType}
                    </ThemedText>
                  </View>
                ) : (
                  <ThemedText style={{ color: subtitleColor, fontSize: 13 }}>Select type...</ThemedText>
                )}
                <Ionicons name="chevron-down" size={15} color={GOLD_COLOR} />
              </TouchableOpacity>
            </View>

            <View style={[styles.inputGroup, { flex: 1 }]}>
              <ThemedText style={[styles.label, { width: 90, color: textColor }]}>Points Ratio</ThemedText>
              <TouchableOpacity
                style={[styles.input, styles.pickerSelectBtn, { borderColor, backgroundColor: cardColor }]}
                onPress={() => setIsPointsPickerVisible(true)}
                activeOpacity={0.7}
              >
                {pointsRatio ? (
                  <View style={styles.ratioPill}>
                    <ThemedText style={styles.ratioPillText}>{pointsRatio}</ThemedText>
                  </View>
                ) : (
                  <ThemedText style={{ color: subtitleColor, fontSize: 13 }}>Ratio...</ThemedText>
                )}
                <Ionicons name="chevron-down" size={15} color={GOLD_COLOR} />
              </TouchableOpacity>
            </View>
          </View>
        </View>

      </ScrollView>

      {/* Pickers & Modals */}

      {/* Customer Type Picker */}
      {isCustomerTypePickerVisible && (
        <Modal transparent animationType="fade" onRequestClose={() => setIsCustomerTypePickerVisible(false)}>
          <TouchableWithoutFeedback onPress={() => setIsCustomerTypePickerVisible(false)}>
            <View style={styles.modalOverlay}>
              <View style={[styles.pickerContainer, { backgroundColor: cardColor, borderColor }]}>
                <View style={[styles.pickerHeader, { borderBottomColor: borderColor }]}>
                  <ThemedText style={[styles.pickerHeaderTitle, { color: textColor }]}>Select Customer Type</ThemedText>
                </View>
                {types.map(t => {
                  const styleInfo = getCustomerTypeBadgeStyle(t);
                  const isCurrent = customerType === t;
                  return (
                    <TouchableOpacity
                      key={t}
                      style={[
                        styles.pickerItem,
                        {
                          borderBottomColor: borderColor,
                          backgroundColor: isCurrent ? (isDark ? 'rgba(212, 175, 55, 0.15)' : '#FEF9EE') : 'transparent'
                        }
                      ]}
                      onPress={() => {
                        setCustomerType(t);
                        setIsCustomerTypePickerVisible(false);
                        if (newCustomerType === t) {
                          setNewCustomerType('');
                          setPointsRatio('');
                        } else if (newCustomerType) {
                          setPointsRatio(calculatePointsRatio(t, newCustomerType));
                        }
                      }}
                    >
                      <View style={{ flexDirection: 'row', alignItems: 'center', gap: 10 }}>
                        <View style={[styles.modalTypeCircle, { backgroundColor: styleInfo.bg, borderColor: styleInfo.border }]}>
                          <Ionicons name={styleInfo.icon as any} size={14} color={styleInfo.text} />
                        </View>
                        <ThemedText style={{ color: textColor, fontWeight: isCurrent ? 'bold' : 'normal', fontSize: 14 }}>
                          {t}
                        </ThemedText>
                      </View>
                      {isCurrent && <Ionicons name="checkmark-circle" size={18} color={GOLD_COLOR} />}
                    </TouchableOpacity>
                  );
                })}
              </View>
            </View>
          </TouchableWithoutFeedback>
        </Modal>
      )}

      {/* New Customer Type Picker */}
      {isNewCustomerTypePickerVisible && (
        <Modal transparent animationType="fade" onRequestClose={() => setIsNewCustomerTypePickerVisible(false)}>
          <TouchableWithoutFeedback onPress={() => setIsNewCustomerTypePickerVisible(false)}>
            <View style={styles.modalOverlay}>
              <View style={[styles.pickerContainer, { backgroundColor: cardColor, borderColor }]}>
                <View style={[styles.pickerHeader, { borderBottomColor: borderColor }]}>
                  <ThemedText style={[styles.pickerHeaderTitle, { color: textColor }]}>Select New Customer Type</ThemedText>
                </View>
                {types.map(t => {
                  const styleInfo = getCustomerTypeBadgeStyle(t);
                  const isCurrent = newCustomerType === t;
                  const isSameAsOld = customerType === t;
                  return (
                    <TouchableOpacity
                      key={t}
                      disabled={isSameAsOld}
                      style={[
                        styles.pickerItem,
                        {
                          borderBottomColor: borderColor,
                          opacity: isSameAsOld ? 0.4 : 1,
                          backgroundColor: isCurrent ? (isDark ? 'rgba(212, 175, 55, 0.15)' : '#FEF9EE') : 'transparent'
                        }
                      ]}
                      onPress={() => handleNewCustomerTypeSelect(t)}
                    >
                      <View style={{ flexDirection: 'row', alignItems: 'center', gap: 10 }}>
                        <View style={[styles.modalTypeCircle, { backgroundColor: styleInfo.bg, borderColor: styleInfo.border }]}>
                          <Ionicons name={styleInfo.icon as any} size={14} color={styleInfo.text} />
                        </View>
                        <View>
                          <ThemedText style={{ color: textColor, fontWeight: isCurrent ? 'bold' : 'normal', fontSize: 14 }}>
                            {t}
                          </ThemedText>
                          {isSameAsOld && (
                            <ThemedText style={{ fontSize: 10, color: subtitleColor }}>Current Type</ThemedText>
                          )}
                        </View>
                      </View>
                      {isCurrent && <Ionicons name="checkmark-circle" size={18} color={GOLD_COLOR} />}
                    </TouchableOpacity>
                  );
                })}
              </View>
            </View>
          </TouchableWithoutFeedback>
        </Modal>
      )}

      {/* Points Ratio Picker */}
      {isPointsPickerVisible && (
        <Modal transparent animationType="fade" onRequestClose={() => setIsPointsPickerVisible(false)}>
          <TouchableWithoutFeedback onPress={() => setIsPointsPickerVisible(false)}>
            <View style={styles.modalOverlay}>
              <View style={[styles.pickerContainer, { backgroundColor: cardColor, borderColor }]}>
                <View style={[styles.pickerHeader, { borderBottomColor: borderColor }]}>
                  <ThemedText style={[styles.pickerHeaderTitle, { color: textColor }]}>Select Points Ratio</ThemedText>
                </View>
                {pointsOptions.map(p => {
                  const isCurrent = pointsRatio === p;
                  return (
                    <TouchableOpacity
                      key={p}
                      style={[
                        styles.pickerItem,
                        {
                          borderBottomColor: borderColor,
                          backgroundColor: isCurrent ? (isDark ? 'rgba(212, 175, 55, 0.15)' : '#FEF9EE') : 'transparent'
                        }
                      ]}
                      onPress={() => {
                        setPointsRatio(p);
                        setIsPointsPickerVisible(false);
                      }}
                    >
                      <View style={{ flexDirection: 'row', alignItems: 'center', gap: 10 }}>
                        <View style={styles.ratioPill}>
                          <ThemedText style={styles.ratioPillText}>{p}</ThemedText>
                        </View>
                        <ThemedText style={{ color: textColor, fontWeight: isCurrent ? 'bold' : 'normal' }}>
                          Conversion ({p})
                        </ThemedText>
                      </View>
                      {isCurrent && <Ionicons name="checkmark-circle" size={18} color={GOLD_COLOR} />}
                    </TouchableOpacity>
                  );
                })}
              </View>
            </View>
          </TouchableWithoutFeedback>
        </Modal>
      )}

      {/* Lookup Modal */}
      {isLookupVisible && (
        <Modal transparent animationType="fade" onRequestClose={() => setIsLookupVisible(false)}>
          <TouchableWithoutFeedback onPress={() => setIsLookupVisible(false)}>
            <View style={styles.modalOverlay}>
              <TouchableWithoutFeedback>
                <View style={[styles.modalContainer, { backgroundColor: backgroundColor, borderColor }]}>
                  <View style={[styles.modalHeader, { backgroundColor: isDark ? '#262626' : '#FDF8ED', borderBottomColor: borderColor }]}>
                    <View style={{ flexDirection: 'row', alignItems: 'center', gap: 8 }}>
                      <View style={[styles.iconBadge, { backgroundColor: GOLD_COLOR }]}>
                        <Ionicons name="people" size={14} color="#FFF" />
                      </View>
                      <ThemedText style={{ fontSize: 15, fontWeight: 'bold', color: textColor }}>Select Customer</ThemedText>
                    </View>
                    <TouchableOpacity onPress={() => setIsLookupVisible(false)} hitSlop={{ top: 10, bottom: 10, left: 10, right: 10 }}>
                      <Ionicons name="close-circle" size={22} color={subtitleColor} />
                    </TouchableOpacity>
                  </View>

                  <View style={[styles.modalSearchBar, { borderBottomColor: borderColor, backgroundColor: cardColor }]}>
                    <Ionicons name="search" size={16} color={subtitleColor} style={{ marginRight: 6 }} />
                    <TextInput
                      style={[styles.searchInput, { color: textColor }]}
                      placeholder="Search code or name..."
                      placeholderTextColor={subtitleColor}
                      value={searchQuery}
                      onChangeText={setSearchQuery}
                      autoFocus
                    />
                    {searchQuery.length > 0 && (
                      <TouchableOpacity onPress={() => setSearchQuery('')}>
                        <Ionicons name="close" size={16} color={subtitleColor} />
                      </TouchableOpacity>
                    )}
                  </View>

                  {loadingLookup ? (
                    <View style={{ padding: 40, alignItems: 'center' }}>
                      <ActivityIndicator size="large" color={GOLD_COLOR} />
                      <ThemedText style={{ marginTop: 12, color: subtitleColor }}>Loading customers...</ThemedText>
                    </View>
                  ) : (
                    <View style={{ flex: 1, minHeight: 300 }}>
                      <View style={[styles.gridHeader, { backgroundColor: isDark ? '#1F2937' : '#F1F5F9', borderBottomWidth: 1, borderBottomColor: borderColor }]}>
                        <ThemedText style={[styles.gridHeaderText, { flex: 1.5, color: textColor }]}>Customer Code</ThemedText>
                        <ThemedText style={[styles.gridHeaderText, { flex: 2, color: textColor }]}>Customer Name</ThemedText>
                      </View>
                      <FlatList
                        data={lookupData.filter(d =>
                          (d.CustomerCode || '').toLowerCase().includes(searchQuery.toLowerCase()) ||
                          (d.CustomerName || '').toLowerCase().includes(searchQuery.toLowerCase())
                        )}
                        keyExtractor={(item) => item.CustomerCode}
                        renderItem={({ item, index }) => (
                          <TouchableOpacity
                            style={[
                              styles.gridRow,
                              {
                                borderBottomColor: borderColor,
                                borderBottomWidth: 1,
                                backgroundColor: index % 2 === 0 ? cardColor : (isDark ? '#1C1917' : '#FBFBFC'),
                              }
                            ]}
                            onPress={() => {
                              setCustomerCode(item.CustomerCode);
                              setIsLookupVisible(false);
                            }}
                          >
                            <ThemedText style={[styles.gridCellText, { flex: 1.5, color: GOLD_COLOR, fontWeight: '700' }]} numberOfLines={1}>
                              {item.CustomerCode}
                            </ThemedText>
                            <ThemedText style={[styles.gridCellText, { flex: 2, color: textColor }]} numberOfLines={1}>
                              {item.CustomerName}
                            </ThemedText>
                          </TouchableOpacity>
                        )}
                      />
                    </View>
                  )}
                </View>
              </TouchableWithoutFeedback>
            </View>
          </TouchableWithoutFeedback>
        </Modal>
      )}

      {toast && (
        <View style={[styles.toast, { backgroundColor: toast.type === 'success' ? '#10B981' : '#EF4444' }]}>
          <Ionicons
            name={toast.type === 'success' ? 'checkmark-circle' : 'alert-circle'}
            size={18}
            color="#FFF"
            style={{ marginRight: 6 }}
          />
          <ThemedText style={styles.toastText}>{toast.message}</ThemedText>
        </View>
      )}
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  container: { flex: 1 },
  scrollContent: { padding: 16, alignItems: 'center' },
  card: {
    borderWidth: 1,
    borderRadius: 10,
    width: '100%',
    maxWidth: 720,
    marginBottom: 16,
    overflow: 'hidden',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.05,
    shadowRadius: 4,
    elevation: 2,
  },
  cardBanner: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    paddingHorizontal: 14,
    paddingVertical: 10,
    borderBottomWidth: 1,
  },
  cardBannerLeft: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 8,
  },
  cardBannerTitle: {
    fontSize: 13,
    fontWeight: '700',
  },
  iconBadge: {
    width: 22,
    height: 22,
    borderRadius: 6,
    justifyContent: 'center',
    alignItems: 'center',
  },
  typeBadgeSmall: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 4,
    paddingHorizontal: 8,
    paddingVertical: 3,
    borderRadius: 12,
    borderWidth: 1,
  },
  typeBadgeSmallText: {
    fontSize: 11,
    fontWeight: '700',
  },
  cardBody: {
    padding: 16,
    gap: 14,
  },
  row: {
    flexDirection: 'row',
    gap: 12,
    alignItems: 'center',
  },
  inputGroup: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 8,
  },
  label: {
    width: 95,
    fontWeight: '600',
    fontSize: 12,
  },
  input: {
    flex: 1,
    borderWidth: 1,
    borderRadius: 6,
    paddingHorizontal: 10,
    paddingVertical: Platform.OS === 'ios' ? 8 : 6,
    fontSize: 13,
  },
  pickerSelectBtn: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
  },
  lookupBtn: {
    width: 36,
    height: 34,
    borderRadius: 6,
    justifyContent: 'center',
    alignItems: 'center',
    borderWidth: 1,
  },
  fetchBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    paddingVertical: 8,
    paddingHorizontal: 16,
    borderRadius: 6,
    alignSelf: 'stretch',
  },
  fetchBtnText: {
    color: '#FFFFFF',
    fontWeight: '700',
    fontSize: 13,
  },
  gridContainer: {
    width: '100%',
    maxWidth: 720,
    borderWidth: 1,
    borderRadius: 10,
    minHeight: 220,
    marginBottom: 16,
    overflow: 'hidden',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.05,
    shadowRadius: 4,
    elevation: 2,
  },
  gridTitleBar: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    paddingHorizontal: 14,
    paddingVertical: 10,
    borderBottomWidth: 1,
  },
  gridBarTitle: {
    fontSize: 13,
    fontWeight: '700',
  },
  countBadge: {
    backgroundColor: '#D4AF37',
    paddingHorizontal: 6,
    paddingVertical: 1,
    borderRadius: 10,
  },
  countBadgeText: {
    color: '#FFF',
    fontSize: 11,
    fontWeight: '700',
  },
  gridHeader: {
    flexDirection: 'row',
    paddingHorizontal: 12,
    paddingVertical: 10,
    gap: 8,
    alignItems: 'center',
  },
  gridHeaderText: {
    fontWeight: '700',
    fontSize: 12,
  },
  gridRow: {
    flexDirection: 'row',
    paddingHorizontal: 12,
    paddingVertical: 10,
    gap: 8,
    alignItems: 'center',
  },
  gridCellText: {
    fontSize: 13,
  },
  pointsPill: {
    backgroundColor: 'rgba(212, 175, 55, 0.15)',
    paddingHorizontal: 8,
    paddingVertical: 2,
    borderRadius: 12,
  },
  pointsPillText: {
    fontSize: 12,
    fontWeight: '700',
    color: '#B48218',
  },
  checkbox: {
    width: 18,
    height: 18,
    borderWidth: 1.5,
    borderRadius: 4,
    justifyContent: 'center',
    alignItems: 'center',
  },
  bottomSection: {
    width: '100%',
    maxWidth: 720,
    borderWidth: 1,
    borderRadius: 10,
    overflow: 'hidden',
    marginBottom: 20,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.05,
    shadowRadius: 4,
    elevation: 2,
  },
  bottomSectionBanner: {
    paddingHorizontal: 14,
    paddingVertical: 10,
    borderBottomWidth: 1,
  },
  bottomSectionBody: {
    padding: 16,
    flexDirection: 'row',
    gap: 16,
    alignItems: 'center',
  },
  ratioPill: {
    backgroundColor: 'rgba(59, 130, 246, 0.12)',
    paddingHorizontal: 8,
    paddingVertical: 2,
    borderRadius: 6,
  },
  ratioPillText: {
    color: '#2563EB',
    fontWeight: '700',
    fontSize: 12,
  },
  headerActionBtn: {
    flexDirection: 'row',
    paddingVertical: 6,
    paddingHorizontal: 12,
    borderRadius: 8,
    borderWidth: 1,
    alignItems: 'center',
    justifyContent: 'center',
  },
  headerActionBtnText: {
    fontWeight: '700',
    fontSize: 12,
  },
  modalOverlay: {
    flex: 1,
    backgroundColor: 'rgba(0,0,0,0.5)',
    justifyContent: 'center',
    padding: 16,
  },
  pickerContainer: {
    width: 280,
    borderWidth: 1,
    borderRadius: 12,
    alignSelf: 'center',
    overflow: 'hidden',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.15,
    shadowRadius: 8,
    elevation: 5,
  },
  pickerHeader: {
    padding: 14,
    borderBottomWidth: 1,
    alignItems: 'center',
  },
  pickerHeaderTitle: {
    fontSize: 14,
    fontWeight: '700',
  },
  pickerItem: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    padding: 14,
    borderBottomWidth: 1,
  },
  modalTypeCircle: {
    width: 28,
    height: 28,
    borderRadius: 14,
    borderWidth: 1,
    justifyContent: 'center',
    alignItems: 'center',
  },
  modalContainer: {
    borderRadius: 12,
    borderWidth: 1,
    maxHeight: '80%',
    width: '100%',
    maxWidth: 600,
    alignSelf: 'center',
    overflow: 'hidden',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.2,
    shadowRadius: 10,
    elevation: 6,
  },
  modalHeader: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    paddingHorizontal: 16,
    paddingVertical: 12,
    borderBottomWidth: 1,
  },
  modalSearchBar: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 12,
    paddingVertical: 8,
    borderBottomWidth: 1,
  },
  searchInput: {
    flex: 1,
    fontSize: 13,
    paddingVertical: Platform.OS === 'ios' ? 4 : 2,
  },
  toast: {
    position: 'absolute',
    top: Platform.OS === 'web' ? 20 : 50,
    alignSelf: 'center',
    minWidth: 250,
    maxWidth: '90%',
    paddingVertical: 12,
    paddingHorizontal: 16,
    borderRadius: 24,
    zIndex: 9999,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 3 },
    shadowOpacity: 0.25,
    shadowRadius: 4,
    elevation: 6,
  },
  toastText: {
    color: '#FFF',
    fontSize: 13,
    fontWeight: '600',
    textAlign: 'center',
  },
});

