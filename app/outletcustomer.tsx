import { ThemedText } from '@/components/themed-text';
import { API_BASE_URL } from '@/constants/api';
import { useThemeColors } from '@/hooks/useThemeColors';
import { useThemeContext } from '@/contexts/ThemeContext';
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

const GOLD_COLOR = '#D4AF37';
const API_URL = `${API_BASE_URL}/outletcustomer`;



export default function OutletCustomerScreen() {
  const themeColors = useThemeColors();
  const { accentColor } = useThemeContext();
  const { background: backgroundColor, card: cardColor, text: textColor, border: borderColor, subtitle: subtitleColor, primaryGold: GOLD_COLOR, primaryGold } = themeColors;

  

  
  
  

  
  
  

  const colorScheme = useColorScheme();
  const isDark = colorScheme === 'dark';
  const router = useRouter();

  const [data, setData] = useState<any[]>([]);
  const [loading, setLoading] = useState(false);
  const [saving, setSaving] = useState(false);
  const [toast, setToast] = useState<{ message: string, type: 'success' | 'error' } | null>(null);

  const showToast = (message: string, type: 'success' | 'error' = 'success') => {
    setToast({ message, type });
    setTimeout(() => {
      setToast(null);
    }, 3000);
  };

  // Modal State for Lookup
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [searchQuery, setSearchQuery] = useState('');

  // Religion Picker State
  const [isReligionPickerVisible, setIsReligionPickerVisible] = useState(false);

  // Customer Type Picker State
  const [isCustomerTypePickerVisible, setIsCustomerTypePickerVisible] = useState(false);

  // Form State
  const [customerCode, setCustomerCode] = useState('');
  const [customerType, setCustomerType] = useState('RUBY');
  const [customerName, setCustomerName] = useState('');
  const [mobile, setMobile] = useState('');
  const [dateType, setDateType] = useState(''); // Religion
  const [eMail, setEMail] = useState('');
  const [nricNo, setNricNo] = useState('');
  const [remarks, setRemarks] = useState('');

  const [isEditing, setIsEditing] = useState(false);

  const headerBgColor = isDark ? '#1a1a1a' : '#ffffff';

  useEffect(() => {
    fetchCustomers();
  }, []);

  const fetchCustomers = async () => {
    try {
      setLoading(true);
      const res = await fetch(API_URL);
      const json = await res.json();
      if (json.success) {
        setData(json.data);
      }
    } catch (err) {
      console.error(err);
    } finally {
      setLoading(false);
    }
  };

  const loadCustomerDetails = async (code: string) => {
    try {
      setLoading(true);
      const res = await fetch(`${API_URL}/${code}`);
      const json = await res.json();
      if (json.success) {
        const item = json.data;
        setCustomerCode(item.CustomerCode);
        setCustomerName(item.CustomerName || '');
        setCustomerType(item.CustomerType || 'RUBY');
        setMobile(item.Mobile || '');
        setEMail(item.eMail || '');
        setNricNo(item.NRICNo || '');
        setRemarks(item.Remarks || '');
        setDateType(item.DateType || '');
        setIsEditing(true);
      } else {
        showToast("Customer not found", "error");
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
    if (!customerType || !customerType.trim()) {
      showToast('Customer Type is required.', 'error');
      return;
    }
    if (!customerName || !customerName.trim()) {
      showToast('Customer Name is required.', 'error');
      return;
    }
    if (!mobile || !mobile.trim()) {
      showToast('Mobile Number is required.', 'error');
      return;
    }

    try {
      setSaving(true);
      const payload = {
        CustomerCode: customerCode,
        CustomerName: customerName,
        CustomerType: customerType,
        Mobile: mobile,
        eMail,
        NRICNo: nricNo,
        Remarks: remarks,
        DateType: dateType
      };

      const res = await fetch(API_URL, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload)
      });
      const json = await res.json();

      if (json.success) {
        showToast('Customer detail saved successfully.', 'success');
        fetchCustomers();
        handleClear();
      } else {
        showToast(json.message || 'Failed to save customer', 'error');
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
    setCustomerType('RUBY');
    setMobile('');
    setEMail('');
    setNricNo('');
    setRemarks('');
    setDateType('');
    setIsEditing(false);
  };

  const selectCustomer = (item: any) => {
    setIsModalVisible(false);
    loadCustomerDetails(item.CustomerCode);
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
    const isEven = index % 2 === 0;
    return (
      <TouchableOpacity
        style={[
          styles.gridRow,
          {
            borderBottomColor: borderColor,
            borderBottomWidth: 1,
            backgroundColor: isEven ? cardColor : (isDark ? '#1C1917' : '#FBFBFC'),
          }
        ]}
        onPress={() => selectCustomer(item)}
      >
        <ThemedText style={[styles.gridCellText, { flex: 1.5, color: GOLD_COLOR, fontWeight: '700' }]} numberOfLines={1}>{item.CustomerCode}</ThemedText>
        <ThemedText style={[styles.gridCellText, { flex: 2, color: textColor }]} numberOfLines={1}>{item.CustomerName}</ThemedText>
        <ThemedText style={[styles.gridCellText, { flex: 2, color: subtitleColor }]} numberOfLines={1}>{item.Address1}</ThemedText>
        <ThemedText style={[styles.gridCellText, { flex: 2, color: subtitleColor }]} numberOfLines={1}>{item.Address2}</ThemedText>
        <ThemedText style={[styles.gridCellText, { flex: 1.5, color: textColor }]} numberOfLines={1}>{item.Mobile}</ThemedText>
      </TouchableOpacity>
    );
  };

  return (
    <SafeAreaView style={[styles.container, { backgroundColor }]} edges={['bottom', 'left', 'right']}>
      <Stack.Screen
        options={{
          headerShown: true,
          title: 'Outlet Customers',
          headerStyle: { backgroundColor: headerBgColor },
          headerTintColor: textColor,
          headerRight: () => (
            <View style={{ flexDirection: 'row', gap: 8, paddingRight: 8, alignItems: 'center' }}>
              <TouchableOpacity
                style={[styles.headerActionBtn, { backgroundColor: isDark ? '#262626' : '#F8FAFC', borderColor: borderColor }]}
                onPress={handleClear}
                activeOpacity={0.7}
              >
                <Ionicons name="add-circle-outline" size={14} color={textColor} style={{ marginRight: 4 }} />
                <ThemedText style={[styles.headerActionBtnText, { color: textColor }]}>New</ThemedText>
              </TouchableOpacity>
              <TouchableOpacity
                style={[styles.headerActionBtn, { backgroundColor: isDark ? '#262626' : '#F8FAFC', borderColor: borderColor }]}
                onPress={handleClear}
                activeOpacity={0.7}
              >
                <Ionicons name="refresh-outline" size={14} color={textColor} style={{ marginRight: 4 }} />
                <ThemedText style={[styles.headerActionBtnText, { color: textColor }]}>Clear</ThemedText>
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
        <View style={[styles.card, { backgroundColor: cardColor, borderColor }]}>
          <View style={[styles.cardBanner, { backgroundColor: isDark ? 'rgba(212, 175, 55, 0.12)' : '#FDF8ED', borderBottomColor: borderColor }]}>
            <View style={styles.cardBannerLeft}>
              <View style={[styles.iconBadge, { backgroundColor: GOLD_COLOR }]}>
                <Ionicons name="person" size={13} color="#FFFFFF" />
              </View>
              <ThemedText style={[styles.cardBannerTitle, { color: textColor }]}>Customer Profile</ThemedText>
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
            <View style={styles.inputGroup}>
              <ThemedText style={[styles.label, { color: textColor }]}>Customer Code</ThemedText>
              <View style={styles.lookupContainer}>
                <TextInput
                  style={[styles.input, { color: textColor, borderColor, backgroundColor: cardColor }]}
                  placeholder="Enter code"
                  placeholderTextColor={subtitleColor}
                  value={customerCode}
                  onChangeText={setCustomerCode}
                  editable={!isEditing}
                  autoCapitalize="characters"
                  onBlur={() => {
                    if (customerCode.trim() && !isEditing) {
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

            <View style={styles.inputGroup}>
              <ThemedText style={[styles.label, { color: textColor }]}>Customer Type</ThemedText>
              <TouchableOpacity
                style={[styles.input, { paddingHorizontal: 10, flexDirection: 'row', justifyContent: 'space-between', alignItems: 'center', borderColor, backgroundColor: cardColor }]}
                onPress={() => setIsCustomerTypePickerVisible(true)}
                activeOpacity={0.7}
              >
                <View style={{ flexDirection: 'row', alignItems: 'center', gap: 6 }}>
                  <Ionicons name={getCustomerTypeBadgeStyle(customerType).icon as any} size={13} color={getCustomerTypeBadgeStyle(customerType).text} />
                  <ThemedText style={{ color: getCustomerTypeBadgeStyle(customerType).text, fontSize: 13, fontWeight: '700' }}>
                    {customerType || 'Select Type'}
                  </ThemedText>
                </View>
                <Ionicons name="chevron-down" size={15} color={GOLD_COLOR} />
              </TouchableOpacity>
            </View>

            <View style={styles.inputGroup}>
              <ThemedText style={[styles.label, { color: textColor }]}>Customer Name</ThemedText>
              <TextInput
                style={[styles.input, { color: textColor, borderColor, backgroundColor: cardColor }]}
                placeholder="Full name"
                placeholderTextColor={subtitleColor}
                value={customerName}
                onChangeText={setCustomerName}
              />
            </View>

            <View style={styles.inputGroup}>
              <ThemedText style={[styles.label, { color: textColor }]}>Mobile</ThemedText>
              <TextInput
                style={[styles.input, { color: textColor, borderColor, backgroundColor: cardColor }]}
                placeholder="Mobile number"
                placeholderTextColor={subtitleColor}
                value={mobile}
                onChangeText={setMobile}
                keyboardType="phone-pad"
              />
            </View>

            <View style={styles.inputGroup}>
              <ThemedText style={[styles.label, { color: textColor }]}>Religion</ThemedText>
              <TouchableOpacity
                style={[styles.input, { paddingHorizontal: 10, flexDirection: 'row', justifyContent: 'space-between', alignItems: 'center', borderColor, backgroundColor: cardColor }]}
                onPress={() => setIsReligionPickerVisible(true)}
                activeOpacity={0.7}
              >
                <ThemedText style={{ color: dateType ? textColor : subtitleColor, fontSize: 13, fontWeight: dateType ? '600' : 'normal' }}>
                  {dateType || 'Select Religion'}
                </ThemedText>
                <Ionicons name="chevron-down" size={15} color={subtitleColor} />
              </TouchableOpacity>
            </View>

            <View style={styles.inputGroup}>
              <ThemedText style={[styles.label, { color: textColor }]}>Email</ThemedText>
              <TextInput
                style={[styles.input, { color: textColor, borderColor, backgroundColor: cardColor }]}
                placeholder="email@example.com"
                placeholderTextColor={subtitleColor}
                value={eMail}
                onChangeText={setEMail}
                keyboardType="email-address"
                autoCapitalize="none"
              />
            </View>

            <View style={styles.inputGroup}>
              <ThemedText style={[styles.label, { color: textColor }]}>NRIC No</ThemedText>
              <TextInput
                style={[styles.input, { color: textColor, borderColor, backgroundColor: cardColor }]}
                placeholder="NRIC or Identity No"
                placeholderTextColor={subtitleColor}
                value={nricNo}
                onChangeText={setNricNo}
              />
            </View>

            <View style={styles.inputGroup}>
              <ThemedText style={[styles.label, { color: textColor }]}>Remarks</ThemedText>
              <TextInput
                style={[styles.input, { color: textColor, borderColor, backgroundColor: cardColor }]}
                placeholder="Optional notes"
                placeholderTextColor={subtitleColor}
                value={remarks}
                onChangeText={setRemarks}
              />
            </View>
          </View>
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

                {loading ? (
                  <View style={{ padding: 40, alignItems: 'center' }}>
                    <ActivityIndicator size="large" color={GOLD_COLOR} />
                    <ThemedText style={{ marginTop: 12, color: subtitleColor }}>Loading customers...</ThemedText>
                  </View>
                ) : (
                  <View style={{ flex: 1, minHeight: 300 }}>
                    <View style={[styles.gridHeader, { backgroundColor: isDark ? '#1F2937' : '#F1F5F9', borderBottomWidth: 1, borderBottomColor: borderColor }]}>
                      <ThemedText style={[styles.gridHeaderText, { color: textColor, flex: 1.5 }]}>Customer Code</ThemedText>
                      <ThemedText style={[styles.gridHeaderText, { color: textColor, flex: 2 }]}>Customer Name</ThemedText>
                      <ThemedText style={[styles.gridHeaderText, { color: textColor, flex: 2 }]}>Address 1</ThemedText>
                      <ThemedText style={[styles.gridHeaderText, { color: textColor, flex: 2 }]}>Address 2</ThemedText>
                      <ThemedText style={[styles.gridHeaderText, { color: textColor, flex: 1.5 }]}>Mobile</ThemedText>
                    </View>
                    <FlatList
                      data={data.filter(d =>
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

      {/* Religion Picker Modal */}
      <Modal visible={isReligionPickerVisible} animationType="fade" transparent={true} onRequestClose={() => setIsReligionPickerVisible(false)}>
        <TouchableWithoutFeedback onPress={() => setIsReligionPickerVisible(false)}>
          <View style={styles.modalOverlay}>
            <TouchableWithoutFeedback>
              <View style={[styles.pickerContainer, { backgroundColor: cardColor, borderColor }]}>
                <View style={[styles.modalHeader, { backgroundColor: isDark ? '#262626' : '#FDF8ED', borderBottomColor: borderColor }]}>
                  <ThemedText style={[styles.modalTitle, { color: textColor }]}>Select Religion</ThemedText>
                  <TouchableOpacity onPress={() => setIsReligionPickerVisible(false)}>
                    <Ionicons name="close" size={22} color={textColor} />
                  </TouchableOpacity>
                </View>
                {['', 'HINDU', 'MUSLIM', 'CHRISTIAN', 'CHINESE'].map((rel, i) => (
                  <TouchableOpacity
                    key={i}
                    style={[
                      styles.pickerOption,
                      {
                        borderBottomColor: borderColor,
                        backgroundColor: dateType === rel ? (isDark ? 'rgba(212, 175, 55, 0.15)' : '#FEF9EE') : 'transparent'
                      }
                    ]}
                    onPress={() => {
                      setDateType(rel);
                      setIsReligionPickerVisible(false);
                    }}
                  >
                    <ThemedText style={{ color: dateType === rel ? GOLD_COLOR : textColor, fontSize: 13, fontWeight: dateType === rel ? '700' : 'normal' }}>
                      {rel || 'None'}
                    </ThemedText>
                    {dateType === rel && <Ionicons name="checkmark-circle" size={16} color={GOLD_COLOR} />}
                  </TouchableOpacity>
                ))}
              </View>
            </TouchableWithoutFeedback>
          </View>
        </TouchableWithoutFeedback>
      </Modal>

      {/* Customer Type Picker Modal */}
      <Modal visible={isCustomerTypePickerVisible} animationType="fade" transparent={true} onRequestClose={() => setIsCustomerTypePickerVisible(false)}>
        <TouchableWithoutFeedback onPress={() => setIsCustomerTypePickerVisible(false)}>
          <View style={styles.modalOverlay}>
            <TouchableWithoutFeedback>
              <View style={[styles.pickerContainer, { backgroundColor: cardColor, borderColor }]}>
                <View style={[styles.modalHeader, { backgroundColor: isDark ? '#262626' : '#FDF8ED', borderBottomColor: borderColor }]}>
                  <ThemedText style={[styles.modalTitle, { color: textColor }]}>Select Customer Type</ThemedText>
                  <TouchableOpacity onPress={() => setIsCustomerTypePickerVisible(false)}>
                    <Ionicons name="close" size={22} color={textColor} />
                  </TouchableOpacity>
                </View>
                {['RUBY', 'EMERALD', 'DIAMOND'].map((type, i) => {
                  const styleInfo = getCustomerTypeBadgeStyle(type);
                  const isCurrent = customerType === type;
                  return (
                    <TouchableOpacity
                      key={i}
                      style={[
                        styles.pickerOption,
                        {
                          borderBottomColor: borderColor,
                          backgroundColor: isCurrent ? (isDark ? 'rgba(212, 175, 55, 0.15)' : '#FEF9EE') : 'transparent'
                        }
                      ]}
                      onPress={() => {
                        setCustomerType(type);
                        setIsCustomerTypePickerVisible(false);
                      }}
                    >
                      <View style={{ flexDirection: 'row', alignItems: 'center', gap: 10 }}>
                        <View style={[styles.modalTypeCircle, { backgroundColor: styleInfo.bg, borderColor: styleInfo.border }]}>
                          <Ionicons name={styleInfo.icon as any} size={13} color={styleInfo.text} />
                        </View>
                        <ThemedText style={{ color: isCurrent ? styleInfo.text : textColor, fontSize: 13, fontWeight: isCurrent ? '700' : 'normal' }}>
                          {type}
                        </ThemedText>
                      </View>
                      {isCurrent && <Ionicons name="checkmark-circle" size={16} color={GOLD_COLOR} />}
                    </TouchableOpacity>
                  );
                })}
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
    maxWidth: 620,
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
    padding: 18,
    gap: 14,
  },
  inputGroup: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 14,
  },
  label: {
    width: 125,
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
    textAlign: 'left',
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
  modalContainer: {
    borderRadius: 12,
    borderWidth: 1,
    maxHeight: '80%',
    width: '100%',
    maxWidth: 800,
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
  pickerContainer: {
    borderRadius: 12,
    borderWidth: 1,
    width: 300,
    alignSelf: 'center',
    overflow: 'hidden',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.15,
    shadowRadius: 8,
    elevation: 5,
  },
  pickerOption: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    paddingHorizontal: 16,
    paddingVertical: 12,
    borderBottomWidth: 1,
    alignItems: 'center',
  },
  modalTypeCircle: {
    width: 24,
    height: 24,
    borderRadius: 12,
    borderWidth: 1,
    justifyContent: 'center',
    alignItems: 'center',
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
