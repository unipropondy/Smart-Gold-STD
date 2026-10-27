import { ThemedText } from '@/components/themed-text';
import { API_BASE_URL } from '@/constants/api';
import { Ionicons } from '@expo/vector-icons';
import { Stack, useRouter } from 'expo-router';
import React, { useEffect, useState } from 'react';
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
const API_URL = `${API_BASE_URL}/customerpointsadjustment`;



export default function CustomerPointsAdjustmentScreen() {
  const themeColors = useThemeColors();
  const { accentColor } = useThemeContext();
  const { background: backgroundColor, card: cardColor, text: textColor, border: borderColor, subtitle: subtitleColor, primaryGold: GOLD_COLOR, primaryGold } = themeColors;

  

  
  
  

  const colorScheme = useColorScheme();
  const isDark = colorScheme === 'dark';

  const headerBgColor = isDark ? '#1a1a1a' : '#ffffff';
  const router = useRouter();

  // Lookup Modal State
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [searchQuery, setSearchQuery] = useState('');
  const [lookupData, setLookupData] = useState<any[]>([]);
  const [loadingLookup, setLoadingLookup] = useState(false);

  // Form State
  const [customerCode, setCustomerCode] = useState('');
  const [customerName, setCustomerName] = useState('');
  const [customerType, setCustomerType] = useState('');
  const [previousPoints, setPreviousPoints] = useState('0');
  const [invoiceNo, setInvoiceNo] = useState('');
  const [totalValue, setTotalValue] = useState('');
  const [adjustmentPoints, setAdjustmentPoints] = useState('');

  // Sales History State
  const [salesData, setSalesData] = useState<any[]>([]);

  const [loading, setLoading] = useState(false);
  const [saving, setSaving] = useState(false);
  const [toast, setToast] = useState<{ message: string, type: 'success' | 'error' } | null>(null);

  const showToast = (message: string, type: 'success' | 'error' = 'success') => {
    setToast({ message, type });
    setTimeout(() => {
      setToast(null);
    }, 3000);
  };

  useEffect(() => {
    fetchCustomersForLookup();
  }, []);

  const fetchCustomersForLookup = async () => {
    try {
      setLoadingLookup(true);
      const res = await fetch(`${API_URL}/customers`);
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

  const loadCustomerDetails = async (code: string) => {
    if (!code) return;
    try {
      setLoading(true);
      const res = await fetch(`${API_URL}/customer/${encodeURIComponent(code)}`);
      const json = await res.json();

      if (json.success && json.customer) {
        setCustomerCode(json.customer.CustomerCode);
        setCustomerName(json.customer.CustomerName || '');
        setCustomerType(json.customer.CustomerType || '');
        setPreviousPoints((json.customer.TotalRewardPoints || 0).toString());
        setSalesData(json.sales || []);
      } else {
        showToast("Customer not found", "error");
        handleClear();
      }
    } catch (err) {
      console.error(err);
      showToast("Failed to load customer details", "error");
    } finally {
      setLoading(false);
    }
  };

  const handleSave = async () => {
    if (!customerCode || !customerCode.trim()) {
      showToast('Customer Code is required.', 'error');
      return;
    }
    if (!invoiceNo || !invoiceNo.trim()) {
      showToast('Invoice No should not be blank.', 'error');
      return;
    }
    if (!totalValue || !totalValue.trim()) {
      showToast('Total Value should not be blank.', 'error');
      return;
    }
    if (!adjustmentPoints || !adjustmentPoints.trim()) {
      showToast('Adjustment Points should not be blank.', 'error');
      return;
    }

    try {
      setSaving(true);
      const payload = {
        CustomerCode: customerCode,
        CustomerName: customerName,
        CustomerType: customerType,
        PreviousPoints: parseFloat(previousPoints) || 0,
        AdjustmentPoints: parseFloat(adjustmentPoints) || 0,
        InvoiceNo: invoiceNo
      };

      const res = await fetch(API_URL, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload)
      });
      const json = await res.json();

      if (json.success) {
        showToast('Customer detail saved successfully.', 'success');
        handleClear();
      } else {
        showToast(json.message || 'Failed to save adjustment', 'error');
      }
    } catch (err) {
      console.error(err);
      showToast('Network error occurred while saving.', 'error');
    } finally {
      setSaving(false);
    }
  };

  const handleClear = () => {
    setCustomerCode('');
    setCustomerName('');
    setCustomerType('');
    setPreviousPoints('0');
    setInvoiceNo('');
    setTotalValue('');
    setAdjustmentPoints('');
    setSalesData([]);
  };

  const selectCustomer = (item: any) => {
    setIsModalVisible(false);
    loadCustomerDetails(item.CustomerCode);
  };

  const selectInvoice = (item: any) => {
    setInvoiceNo(item.InvoiceNo || '');
    setTotalValue((item.NetTotal || 0).toString());
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

  const renderLookupItem = ({ item, index }: { item: any; index: number }) => {
    const badgeStyle = getCustomerTypeBadgeStyle(item.CustomerType);
    return (
      <TouchableOpacity
        style={[
          styles.gridRow,
          {
            borderBottomColor: borderColor,
            borderBottomWidth: 1,
            backgroundColor: index % 2 === 0 ? cardColor : (isDark ? '#1C1917' : '#FBFBFC'),
          }
        ]}
        onPress={() => selectCustomer(item)}
      >
        <ThemedText style={[styles.gridCellText, { flex: 1.5, color: GOLD_COLOR, fontWeight: '700' }]} numberOfLines={1}>{item.CustomerCode}</ThemedText>
        <ThemedText style={[styles.gridCellText, { flex: 2, color: textColor }]} numberOfLines={1}>{item.CustomerName}</ThemedText>
        <View style={{ flex: 1.5, alignItems: 'flex-start' }}>
          <View style={[styles.typeBadgeSmall, { backgroundColor: badgeStyle.bg, borderColor: badgeStyle.border }]}>
            <ThemedText style={[styles.typeBadgeSmallText, { color: badgeStyle.text }]}>{item.CustomerType || 'N/A'}</ThemedText>
          </View>
        </View>
        <ThemedText style={[styles.gridCellText, { flex: 1.5, color: subtitleColor }]} numberOfLines={1}>{item.Mobile}</ThemedText>
      </TouchableOpacity>
    );
  };

  const renderSalesItem = ({ item, index }: { item: any; index: number }) => {
    const isSelected = invoiceNo && invoiceNo === item.InvoiceNo;
    const isEven = index % 2 === 0;
    const rowBg = isSelected
      ? (isDark ? 'rgba(212, 175, 55, 0.15)' : '#FEF9EE')
      : (isEven ? cardColor : (isDark ? '#1C1917' : '#FBFBFC'));

    return (
      <TouchableOpacity
        key={item.InvoiceNo || index}
        activeOpacity={0.7}
        style={[
          styles.gridRow,
          {
            backgroundColor: rowBg,
            borderBottomColor: borderColor,
            borderBottomWidth: 1,
          }
        ]}
        onPress={() => selectInvoice(item)}
      >
        <View style={{ flex: 2, flexDirection: 'row', alignItems: 'center', gap: 6 }}>
          <Ionicons name="document-text-outline" size={14} color="#3B82F6" />
          <ThemedText style={[styles.gridCellText, { color: '#3B82F6', fontWeight: '700' }]} numberOfLines={1}>{item.InvoiceNo}</ThemedText>
        </View>
        <ThemedText style={[styles.gridCellText, { flex: 2, color: textColor }]} numberOfLines={1}>
          {item.InvoiceDate ? item.InvoiceDate.substring(0, 10) : ''}
        </ThemedText>
        <ThemedText style={[styles.gridCellText, { flex: 1.5, color: textColor, textAlign: 'right' }]} numberOfLines={1}>
          {Number(item.TotalWeight || 0).toFixed(2)} g
        </ThemedText>
        <ThemedText style={[styles.gridCellText, { flex: 1, color: textColor, textAlign: 'center' }]} numberOfLines={1}>
          {item.TotalPCS || 0}
        </ThemedText>
        <View style={{ flex: 2, alignItems: 'flex-end', paddingRight: 6 }}>
          <View style={styles.pricePill}>
            <ThemedText style={styles.pricePillText} numberOfLines={1}>
              ${Number(item.NetTotal || 0).toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 })}
            </ThemedText>
          </View>
        </View>
      </TouchableOpacity>
    );
  };

  return (
    <SafeAreaView style={[styles.container, { backgroundColor }]} edges={['bottom', 'left', 'right']}>
      <Stack.Screen
        options={{
          headerShown: true,
          title: 'Customer Points Adjustment',
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

        {/* Customer & Points Adjustment Card */}
        <View style={[styles.card, { backgroundColor: cardColor, borderColor }]}>
          <View style={[styles.cardBanner, { backgroundColor: isDark ? 'rgba(212, 175, 55, 0.12)' : '#FDF8ED', borderBottomColor: borderColor }]}>
            <View style={styles.cardBannerLeft}>
              <View style={[styles.iconBadge, { backgroundColor: GOLD_COLOR }]}>
                <Ionicons name="person" size={13} color="#FFFFFF" />
              </View>
              <ThemedText style={[styles.cardBannerTitle, { color: textColor }]}>Customer Details</ThemedText>
            </View>
            {customerType ? (
              <View style={[styles.typeBadgeSmall, { backgroundColor: getCustomerTypeBadgeStyle(customerType).bg, borderColor: getCustomerTypeBadgeStyle(customerType).border }]}>
                <Ionicons name={getCustomerTypeBadgeStyle(customerType).icon as any} size={11} color={getCustomerTypeBadgeStyle(customerType).text} />
                <ThemedText style={[styles.typeBadgeSmallText, { color: getCustomerTypeBadgeStyle(customerType).text }]}>
                  {customerType}
                </ThemedText>
              </View>
            ) : null}
          </View>

          <View style={styles.cardBody}>
            {/* Customer Code */}
            <View style={styles.inputGroup}>
              <ThemedText style={[styles.label, { color: textColor }]}>Customer Code</ThemedText>
              <View style={styles.lookupContainer}>
                <TextInput
                  style={[styles.input, { color: textColor, borderColor, backgroundColor: cardColor }]}
                  placeholder="Enter or select customer..."
                  placeholderTextColor={subtitleColor}
                  value={customerCode}
                  onChangeText={setCustomerCode}
                  autoCapitalize="characters"
                  onBlur={() => {
                    if (customerCode.trim()) {
                      loadCustomerDetails(customerCode.trim());
                    }
                  }}
                />
                <TouchableOpacity
                  style={[styles.lookupButton, { backgroundColor: GOLD_COLOR, borderColor: GOLD_COLOR }]}
                  onPress={() => setIsModalVisible(true)}
                  activeOpacity={0.8}
                >
                  <Ionicons name="search" size={14} color="#FFFFFF" />
                </TouchableOpacity>
              </View>
            </View>

            {/* Customer Name */}
            <View style={styles.inputGroup}>
              <ThemedText style={[styles.label, { color: textColor }]}>Customer Name</ThemedText>
              <TextInput
                style={[styles.input, styles.readonlyInput, { color: textColor, borderColor, backgroundColor: isDark ? '#262626' : '#F8FAFC' }]}
                value={customerName}
                placeholder="Auto-filled customer name"
                placeholderTextColor={subtitleColor}
                editable={false}
              />
            </View>

            {/* Customer Type */}
            <View style={styles.inputGroup}>
              <ThemedText style={[styles.label, { color: textColor }]}>Customer Type</ThemedText>
              <View style={{ flex: 1, flexDirection: 'row', alignItems: 'center' }}>
                {customerType ? (
                  <View style={[styles.typePillBadge, { backgroundColor: getCustomerTypeBadgeStyle(customerType).bg, borderColor: getCustomerTypeBadgeStyle(customerType).border }]}>
                    <Ionicons name={getCustomerTypeBadgeStyle(customerType).icon as any} size={14} color={getCustomerTypeBadgeStyle(customerType).text} />
                    <ThemedText style={[styles.typePillBadgeText, { color: getCustomerTypeBadgeStyle(customerType).text }]}>
                      {customerType}
                    </ThemedText>
                  </View>
                ) : (
                  <TextInput
                    style={[styles.input, styles.readonlyInput, { color: textColor, borderColor, backgroundColor: isDark ? '#262626' : '#F8FAFC' }]}
                    value="None"
                    editable={false}
                  />
                )}
              </View>
            </View>

            {/* Previous Points */}
            <View style={styles.inputGroup}>
              <ThemedText style={[styles.label, { color: textColor }]}>Previous Points</ThemedText>
              <View style={{ flex: 1, flexDirection: 'row', alignItems: 'center' }}>
                <View style={styles.pointsBadge}>
                  <Ionicons name="ribbon-outline" size={14} color="#D97706" style={{ marginRight: 6 }} />
                  <ThemedText style={styles.pointsBadgeText}>
                    {Number(previousPoints || 0).toLocaleString()} pts
                  </ThemedText>
                </View>
              </View>
            </View>

            {/* Invoice No */}
            <View style={styles.inputGroup}>
              <ThemedText style={[styles.label, { color: textColor }]}>Invoice No</ThemedText>
              <TextInput
                style={[styles.input, { color: textColor, borderColor, backgroundColor: cardColor }]}
                placeholder="Click invoice below or type here..."
                placeholderTextColor={subtitleColor}
                value={invoiceNo}
                onChangeText={setInvoiceNo}
              />
            </View>

            {/* Total Value */}
            <View style={styles.inputGroup}>
              <ThemedText style={[styles.label, { color: textColor }]}>Total Value</ThemedText>
              <TextInput
                style={[styles.input, { color: textColor, borderColor, backgroundColor: cardColor }]}
                placeholder="0.00"
                placeholderTextColor={subtitleColor}
                value={totalValue}
                onChangeText={setTotalValue}
                keyboardType="numeric"
              />
            </View>

            {/* Adjustment Points */}
            <View style={styles.inputGroup}>
              <ThemedText style={[styles.label, { color: textColor }]}>Adjustment Points(+/-)</ThemedText>
              <View style={{ flex: 1, flexDirection: 'row', alignItems: 'center', gap: 8 }}>
                <TextInput
                  style={[styles.input, { color: textColor, borderColor, backgroundColor: cardColor, fontWeight: '700' }]}
                  placeholder="+10 or -10"
                  placeholderTextColor={subtitleColor}
                  value={adjustmentPoints}
                  onChangeText={setAdjustmentPoints}
                  keyboardType="numeric"
                />
                {adjustmentPoints ? (
                  <View style={[
                    styles.adjustPill,
                    {
                      backgroundColor: Number(adjustmentPoints) >= 0 ? (isDark ? 'rgba(16, 185, 129, 0.2)' : '#ECFDF5') : (isDark ? 'rgba(239, 68, 68, 0.2)' : '#FEF2F2'),
                      borderColor: Number(adjustmentPoints) >= 0 ? '#10B981' : '#EF4444',
                    }
                  ]}>
                    <Ionicons
                      name={Number(adjustmentPoints) >= 0 ? "trending-up" : "trending-down"}
                      size={13}
                      color={Number(adjustmentPoints) >= 0 ? '#059669' : '#DC2626'}
                    />
                    <ThemedText style={{
                      fontSize: 11,
                      fontWeight: '700',
                      color: Number(adjustmentPoints) >= 0 ? '#059669' : '#DC2626'
                    }}>
                      {Number(adjustmentPoints) >= 0 ? `+${adjustmentPoints}` : adjustmentPoints}
                    </ThemedText>
                  </View>
                ) : null}
              </View>
            </View>

          </View>
        </View>

        {/* Sales History Grid */}
        <View style={[styles.gridContainer, { backgroundColor: cardColor, borderColor }]}>
          <View style={[styles.gridTitleBar, { backgroundColor: isDark ? 'rgba(59, 130, 246, 0.1)' : '#EFF6FF', borderBottomColor: borderColor }]}>
            <View style={{ flexDirection: 'row', alignItems: 'center', gap: 6 }}>
              <View style={[styles.iconBadge, { backgroundColor: '#3B82F6' }]}>
                <Ionicons name="receipt" size={13} color="#FFFFFF" />
              </View>
              <ThemedText style={[styles.gridBarTitle, { color: textColor }]}>Sales History</ThemedText>
              {salesData.length > 0 && (
                <View style={[styles.countBadge, { backgroundColor: '#3B82F6' }]}>
                  <ThemedText style={styles.countBadgeText}>{salesData.length}</ThemedText>
                </View>
              )}
            </View>
            <ThemedText style={{ fontSize: 11, color: subtitleColor }}>
              Tap any row to select invoice
            </ThemedText>
          </View>

          <View style={[styles.gridHeader, { backgroundColor: isDark ? '#1F2937' : '#F1F5F9', borderBottomWidth: 1, borderBottomColor: borderColor }]}>
            <ThemedText style={[styles.gridHeaderText, { color: textColor, flex: 2 }]}>Invoice No</ThemedText>
            <ThemedText style={[styles.gridHeaderText, { color: textColor, flex: 2 }]}>Date</ThemedText>
            <ThemedText style={[styles.gridHeaderText, { color: textColor, flex: 1.5, textAlign: 'right' }]}>Weight</ThemedText>
            <ThemedText style={[styles.gridHeaderText, { color: textColor, flex: 1, textAlign: 'center' }]}>PCS</ThemedText>
            <ThemedText style={[styles.gridHeaderText, { color: textColor, flex: 2, textAlign: 'right', paddingRight: 6 }]}>Price</ThemedText>
          </View>

          {salesData.length === 0 ? (
            <View style={{ padding: 32, alignItems: 'center', gap: 8 }}>
              <Ionicons name="receipt-outline" size={32} color={subtitleColor} />
              <ThemedText style={{ color: subtitleColor, fontSize: 13 }}>No sales history found for this customer.</ThemedText>
            </View>
          ) : (
            salesData.map((item, index) => renderSalesItem({ item, index }))
          )}
        </View>

      </ScrollView>

      {/* Customer Lookup Modal */}
      <Modal visible={isModalVisible} animationType="fade" transparent={true} onRequestClose={() => setIsModalVisible(false)}>
        <TouchableWithoutFeedback onPress={() => setIsModalVisible(false)}>
          <View style={styles.modalOverlay}>
            <TouchableWithoutFeedback>
              <View style={[styles.modalContainer, { backgroundColor: backgroundColor, borderColor }]}>
                <View style={[styles.modalHeader, { backgroundColor: isDark ? '#262626' : '#FDF8ED', borderBottomColor: borderColor }]}>
                  <View style={{ flexDirection: 'row', alignItems: 'center', gap: 8 }}>
                    <View style={[styles.iconBadge, { backgroundColor: GOLD_COLOR }]}>
                      <Ionicons name="people" size={14} color="#FFF" />
                    </View>
                    <ThemedText style={[styles.modalTitle, { color: textColor }]}>Select Customer</ThemedText>
                  </View>
                  <TouchableOpacity onPress={() => setIsModalVisible(false)} hitSlop={{ top: 10, bottom: 10, left: 10, right: 10 }}>
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
                      <ThemedText style={[styles.gridHeaderText, { color: textColor, flex: 1.5 }]}>Customer Code</ThemedText>
                      <ThemedText style={[styles.gridHeaderText, { color: textColor, flex: 2 }]}>Customer Name</ThemedText>
                      <ThemedText style={[styles.gridHeaderText, { color: textColor, flex: 1.5 }]}>Type</ThemedText>
                      <ThemedText style={[styles.gridHeaderText, { color: textColor, flex: 1.5 }]}>Mobile</ThemedText>
                    </View>
                    <FlatList
                      data={lookupData.filter(d =>
                        (d.CustomerCode || '').toLowerCase().includes(searchQuery.toLowerCase()) ||
                        (d.CustomerName || '').toLowerCase().includes(searchQuery.toLowerCase())
                      )}
                      keyExtractor={(item) => item.CustomerCode}
                      renderItem={renderLookupItem}
                      contentContainerStyle={{ paddingBottom: 20 }}
                    />
                  </View>
                )}
              </View>
            </TouchableWithoutFeedback>
          </View>
        </TouchableWithoutFeedback>
      </Modal>

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
    maxWidth: 680,
    marginBottom: 20,
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
    paddingHorizontal: 16,
    paddingVertical: 12,
    borderBottomWidth: 1,
  },
  cardBannerLeft: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 8,
  },
  cardBannerTitle: {
    fontSize: 14,
    fontWeight: '700',
  },
  iconBadge: {
    width: 22,
    height: 22,
    borderRadius: 6,
    justifyContent: 'center',
    alignItems: 'center',
  },
  cardBody: {
    padding: 16,
    gap: 14,
  },
  inputGroup: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 14,
  },
  label: {
    width: 155,
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
  readonlyInput: {
    opacity: 0.9,
  },
  lookupContainer: {
    flex: 1,
    flexDirection: 'row',
    gap: 8,
  },
  lookupButton: {
    width: 36,
    height: 34,
    borderRadius: 6,
    justifyContent: 'center',
    alignItems: 'center',
    borderWidth: 1,
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
  typePillBadge: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 6,
    paddingHorizontal: 10,
    paddingVertical: 6,
    borderRadius: 8,
    borderWidth: 1,
  },
  typePillBadgeText: {
    fontSize: 12,
    fontWeight: '700',
  },
  pointsBadge: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: 'rgba(217, 119, 6, 0.12)',
    paddingHorizontal: 10,
    paddingVertical: 6,
    borderRadius: 6,
    borderWidth: 1,
    borderColor: '#FDE68A',
  },
  pointsBadgeText: {
    fontSize: 13,
    fontWeight: '700',
    color: '#B45309',
  },
  adjustPill: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 4,
    paddingHorizontal: 8,
    paddingVertical: 4,
    borderRadius: 6,
    borderWidth: 1,
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
  gridContainer: {
    width: '100%',
    maxWidth: 680,
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
    fontSize: 12,
  },
  pricePill: {
    backgroundColor: 'rgba(16, 185, 129, 0.12)',
    paddingHorizontal: 8,
    paddingVertical: 3,
    borderRadius: 6,
    borderWidth: 1,
    borderColor: 'rgba(16, 185, 129, 0.25)',
  },
  pricePillText: {
    fontSize: 12,
    fontWeight: '700',
    color: '#059669',
  },
  modalOverlay: {
    flex: 1,
    backgroundColor: 'rgba(0,0,0,0.5)',
    justifyContent: 'center',
    padding: 16,
  },
  modalContainer: {
    borderRadius: 12,
    borderWidth: 1,
    maxHeight: '80%',
    width: '100%',
    maxWidth: 750,
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
  modalTitle: {
    fontSize: 15,
    fontWeight: '700',
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

