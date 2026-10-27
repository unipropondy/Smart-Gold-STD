import { ThemedText } from '@/components/themed-text';
import { API_BASE_URL } from '@/constants/api';
import { useThemeContext } from '@/contexts/ThemeContext';
import { useThemeColors } from '@/hooks/useThemeColors';
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
const API_URL = `${API_BASE_URL}/customer`;

export default function CustomerScreen() {
  const themeColors = useThemeColors();
  const { accentColor } = useThemeContext();
  const { background: backgroundColor, card: cardColor, text: textColor, border: borderColor, primaryGold: GOLD_COLOR, subtitle: subtitleColor } = themeColors;
  const colorScheme = useColorScheme();
  const isDark = colorScheme === 'dark';
  const router = useRouter();

  const [data, setData] = useState<any[]>([]);
  const [loading, setLoading] = useState(true);
  const [saving, setSaving] = useState(false);
  const [deleting, setDeleting] = useState(false);
  const [toast, setToast] = useState<{ message: string, type: 'success' | 'error' } | null>(null);

  const showToast = (message: string, type: 'success' | 'error' = 'success') => {
    setToast({ message, type });
    setTimeout(() => {
      setToast(null);
    }, 3000);
  };

  // Modal State for Lookup
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [isEditing, setIsEditing] = useState(false);
  const [searchQuery, setSearchQuery] = useState('');

  // Form State
  const [customerCode, setCustomerCode] = useState('');
  const [customerType, setCustomerType] = useState('DIAMOND');
  const [customerName, setCustomerName] = useState('');
  const [address1, setAddress1] = useState('');
  const [address2, setAddress2] = useState('');
  const [address3, setAddress3] = useState('');
  const [address4, setAddress4] = useState(''); // Nationality
  const [nricNo, setNricNo] = useState('');
  const [remarks, setRemarks] = useState('');
  const [eMail, setEMail] = useState('');
  const [url, setUrl] = useState(''); // Web Page
  const [phone1, setPhone1] = useState('');
  const [phone2, setPhone2] = useState('');
  const [mobile, setMobile] = useState('');
  const [fax, setFax] = useState('');
  const [gender, setGender] = useState('M'); // M or F
  const [passportNo, setPassportNo] = useState('');
  const [dob, setDob] = useState('');
  const [doa, setDoa] = useState('');
  const [dateType, setDateType] = useState(''); // Religion
  const [glAccount1, setGlAccount1] = useState('Y'); // Active (Y/N)
  const [accountType, setAccountType] = useState('N'); // SMS=S, Phone=P, None=N

  // Display fields for points
  const [totalPoints, setTotalPoints] = useState(0);
  const [redeemedPoints, setRedeemedPoints] = useState(0);

  const [activeTab, setActiveTab] = useState('summary'); // summary, detail, points

  // Tab Data
  const [salesSummaryData, setSalesSummaryData] = useState<any[]>([]);
  const [salesDetailData, setSalesDetailData] = useState<any[]>([]);
  const [pointsData, setPointsData] = useState<any[]>([]);
  const headerBgColor = isDark ? '#2A2A2A' : '#F5F5F5';

  const fetchCustomers = async () => {
    try {
      const response = await fetch(`${API_URL}?t=${Date.now()}`, {
        headers: { 'Cache-Control': 'no-cache' }
      });
      const json = await response.json();
      if (json.success) {
        setData(json.data);
      }
    } catch (error) {
      console.error('Network Error:', error);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchCustomers();
  }, []);

  useEffect(() => {
    if (customerCode && isEditing) {
      // Fetch tab data
      fetch(`${API_URL}/${encodeURIComponent(customerCode)}/sales_summary`).then(r => r.json()).then(j => setSalesSummaryData(j.success ? j.data : [])).catch(() => setSalesSummaryData([]));
      fetch(`${API_URL}/${encodeURIComponent(customerCode)}/sales_detail`).then(r => r.json()).then(j => setSalesDetailData(j.success ? j.data : [])).catch(() => setSalesDetailData([]));
      fetch(`${API_URL}/${encodeURIComponent(customerCode)}/points`).then(r => r.json()).then(j => setPointsData(j.success ? j.data : [])).catch(() => setPointsData([]));
    } else {
      setSalesSummaryData([]);
      setSalesDetailData([]);
      setPointsData([]);
    }
  }, [customerCode, isEditing]);

  const handleSave = async () => {
    if (!customerCode || !customerCode.trim()) {
      showToast('Customer Code is required', 'error');
      return;
    }
    if (!customerName || !customerName.trim()) {
      showToast('Customer Name is required', 'error');
      return;
    }

    const payload = {
      CustomerCode: customerCode.trim(),
      CustomerName: customerName.trim(),
      CustomerType: customerType.trim(),
      Address1: address1.trim(),
      Address2: address2.trim(),
      Address3: address3.trim(),
      Address4: address4.trim(), // Nationality
      NRICNo: nricNo.trim(),
      Remarks: remarks.trim(),
      eMail: eMail.trim(),
      Url: url.trim(),
      Phone1: phone1.trim(),
      Phone2: phone2.trim(),
      Mobile: mobile.trim(),
      Fax: fax.trim(),
      Gender: gender,
      PassportNo: passportNo.trim(),
      DOB: dob.trim() || null,
      DOA: doa.trim() || null,
      DateType: dateType.trim(),
      GLAccount1: glAccount1, // Active
      AccountType: accountType, // Contact
    };

    setSaving(true);
    try {
      const method = isEditing ? 'PUT' : 'POST';
      const response = await fetch(API_URL, {
        method,
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload),
      });

      const json = await response.json();
      if (json.success) {
        showToast(`Customer ${isEditing ? 'updated' : 'added'} successfully`, 'success');
        fetchCustomers();
      } else {
        showToast(json.message || 'Failed to save', 'error');
      }
    } catch (error) {
      console.error('Save error:', error);
      showToast('Failed to save customer', 'error');
    } finally {
      setSaving(false);
    }
  };

  const handleDelete = () => {
    if (!customerCode) return;
    Alert.alert(
      'Confirm Delete',
      'Are you sure you want to delete this customer?',
      [
        { text: 'Cancel', style: 'cancel' },
        {
          text: 'Delete',
          style: 'destructive',
          onPress: async () => {
            setDeleting(true);
            try {
              const response = await fetch(`${API_URL}/${encodeURIComponent(customerCode)}`, { method: 'DELETE' });
              const json = await response.json();
              if (json.success) {
                showToast('Customer deleted successfully', 'success');
                handleClear();
                fetchCustomers();
              } else {
                showToast(json.message || 'Failed to delete', 'error');
              }
            } catch (error) {
              console.error('Delete error:', error);
              showToast('Failed to delete customer', 'error');
            } finally {
              setDeleting(false);
            }
          },
        },
      ]
    );
  };

  const handleClear = () => {
    setCustomerCode('');
    setCustomerName('');
    setCustomerType('DIAMOND');
    setAddress1('');
    setAddress2('');
    setAddress3('');
    setAddress4('');
    setNricNo('');
    setRemarks('');
    setEMail('');
    setUrl('');
    setPhone1('');
    setPhone2('');
    setMobile('');
    setFax('');
    setGender('M');
    setPassportNo('');
    setDob('');
    setDoa('');
    setDateType('');
    setGlAccount1('Y');
    setAccountType('N');
    setTotalPoints(0);
    setRedeemedPoints(0);
    setIsEditing(false);
  };

  const selectCustomer = (item: any) => {
    setCustomerCode(item.CustomerCode);
    setCustomerName(item.CustomerName || '');
    setCustomerType(item.CustomerType || 'DIAMOND');
    setAddress1(item.Address1 || '');
    setAddress2(item.Address2 || '');
    setAddress3(item.Address3 || '');
    setAddress4(item.Address4 || '');
    const foundNric = item.NRICNo || item.NricNo || item.nricno || item.NRICNO || '';
    setNricNo(foundNric);
    console.log('NRICNo:', foundNric);
    setRemarks(item.Remarks || '');
    setEMail(item.eMail || '');
    setUrl(item.Url || '');
    setPhone1(item.Phone1 || '');
    setPhone2(item.Phone2 || '');
    setMobile(item.Mobile || '');
    setFax(item.Fax || '');
    setGender(item.Gender || 'M');
    setPassportNo(item.PassportNo || '');
    setDob(item.DOB ? item.DOB.substring(0, 10) : '');
    setDoa(item.DOA ? item.DOA.substring(0, 10) : '');
    setDateType(item.DateType || '');
    setGlAccount1(item.GLAccount1 || 'N');
    setAccountType(item.AccountType || 'N');
    setTotalPoints(item.TotalRewardPoints || 0);
    setRedeemedPoints(item.TotalRedeemedPoints || 0);

    setIsEditing(true);
    setIsModalVisible(false);
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
          title: 'Customer Details',
          headerStyle: { backgroundColor: headerBgColor },
          headerTintColor: textColor,
          headerLeft: () => (
            <TouchableOpacity onPress={() => router.back()} style={styles.headerButton}>
              <Ionicons name="arrow-back" size={24} color={textColor} />
            </TouchableOpacity>
          ),
          headerRight: () => (
            <View style={{ flexDirection: 'row', gap: 8, marginRight: 16, alignItems: 'center' }}>
              <TouchableOpacity
                style={[styles.actionBtn, { backgroundColor: GOLD_COLOR, borderColor: GOLD_COLOR }]}
                onPress={handleSave}
                disabled={saving}
                activeOpacity={0.8}
              >
                {saving ? (
                  <ActivityIndicator size="small" color="#FFFFFF" />
                ) : (
                  <View style={{ flexDirection: 'row', alignItems: 'center', gap: 4 }}>
                    <Ionicons name="checkmark-done" size={14} color="#FFFFFF" />
                    <ThemedText style={[styles.actionBtnText, { color: '#FFFFFF' }]}>Save</ThemedText>
                  </View>
                )}
              </TouchableOpacity>
              <TouchableOpacity
                style={[
                  styles.actionBtn,
                  {
                    backgroundColor: isDark ? 'rgba(239, 68, 68, 0.15)' : '#FEF2F2',
                    borderColor: isDark ? '#DC2626' : '#FECACA',
                    opacity: !isEditing || deleting ? 0.4 : 1
                  }
                ]}
                onPress={handleDelete}
                disabled={!isEditing || deleting}
                activeOpacity={0.8}
              >
                {deleting ? (
                  <ActivityIndicator size="small" color="#DC2626" />
                ) : (
                  <View style={{ flexDirection: 'row', alignItems: 'center', gap: 4 }}>
                    <Ionicons name="trash-outline" size={14} color="#DC2626" />
                    <ThemedText style={[styles.actionBtnText, { color: '#DC2626' }]}>Delete</ThemedText>
                  </View>
                )}
              </TouchableOpacity>
              <TouchableOpacity
                style={[styles.actionBtn, { backgroundColor: isDark ? '#262626' : '#F8FAFC', borderColor: borderColor }]}
                onPress={handleClear}
                activeOpacity={0.7}
              >
                <View style={{ flexDirection: 'row', alignItems: 'center', gap: 4 }}>
                  <Ionicons name="refresh-outline" size={14} color={textColor} />
                  <ThemedText style={[styles.actionBtnText, { color: textColor }]}>Clear</ThemedText>
                </View>
              </TouchableOpacity>
            </View>
          ),
        }}
      />

      <ScrollView contentContainerStyle={styles.scrollContent}>

        {/* FORM SECTION */}
        <View style={{ flexDirection: 'row', gap: 16 }}>
          <ScrollView horizontal={true} showsHorizontalScrollIndicator={true} style={{ marginBottom: 4 }} contentContainerStyle={{ flexGrow: 1 }}>
            <View style={[styles.section, { flex: 1, minWidth: 850, backgroundColor: cardColor, borderColor }]}>
              <View style={[styles.sectionHeader, { backgroundColor: isDark ? 'rgba(212, 175, 55, 0.12)' : '#FDF8ED', borderBottomColor: borderColor }]}>
                <View style={{ flexDirection: 'row', alignItems: 'center', gap: 8 }}>
                  <View style={[styles.iconBadge, { backgroundColor: GOLD_COLOR }]}>
                    <Ionicons name="person" size={13} color="#FFFFFF" />
                  </View>
                  <ThemedText style={{ color: textColor, fontWeight: '700', fontSize: 13 }}>Customer Details</ThemedText>
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

              <View style={{ padding: 12 }}>
                <View style={styles.row}>
                  <ThemedText style={styles.label}>Customer Code</ThemedText>
                  <View style={{ width: 160, flexDirection: 'row' }}>
                    <TextInput style={[styles.input, { flex: 1, backgroundColor: cardColor, borderColor: borderColor, color: textColor }]} value={customerCode} onChangeText={setCustomerCode} editable={!isEditing} />
                    <TouchableOpacity style={[styles.lookupBtn, { backgroundColor: GOLD_COLOR, borderColor: GOLD_COLOR }]} onPress={() => setIsModalVisible(true)}>
                      <Ionicons name="search" size={12} color="#FFFFFF" />
                    </TouchableOpacity>
                  </View>
                  <ThemedText style={[styles.label, { width: 100, marginLeft: 32 }]}>Passport Number</ThemedText>
                  <TextInput style={[styles.input, { flex: 1, minWidth: 150, backgroundColor: backgroundColor, borderColor: borderColor, color: textColor }]} value={passportNo} onChangeText={setPassportNo} />
                  
                  <ThemedText style={[styles.label, { width: 100, marginLeft: 32 }]}>Customer Type</ThemedText>
                  <View style={[styles.radioGroup, { flex: 1, minWidth: 220, flexWrap: 'nowrap' }]}>
                    {['DIAMOND', 'EMERALD', 'RUBY'].map((opt) => {
                      const isSelected = customerType === opt;
                      const badge = getCustomerTypeBadgeStyle(opt);
                      return (
                        <TouchableOpacity
                          key={opt}
                          style={styles.radioOpt}
                          onPress={() => setCustomerType(opt)}
                        >
                          <Ionicons
                            name={isSelected ? "radio-button-on" : "radio-button-off"}
                            size={14}
                            color={isSelected ? badge.text : subtitleColor}
                          />
                          <ThemedText style={{ color: isSelected ? badge.text : textColor, marginLeft: 4, fontSize: 11, fontWeight: isSelected ? '700' : 'normal' }}>
                            {opt}
                          </ThemedText>
                        </TouchableOpacity>
                      );
                    })}
                  </View>
                </View>

                <View style={styles.row}>
                  <ThemedText style={styles.label}>Customer Name</ThemedText>
                  <TextInput style={[styles.input, { width: 160, backgroundColor: backgroundColor, borderColor: borderColor, color: textColor }]} value={customerName} onChangeText={setCustomerName} />
                  <ThemedText style={[styles.label, { width: 100, marginLeft: 32 }]}>Phone 1</ThemedText>
                  <TextInput style={[styles.input, { flex: 1, minWidth: 150, backgroundColor: backgroundColor, borderColor: borderColor, color: textColor }]} value={phone1} onChangeText={setPhone1} />
                  
                  <ThemedText style={[styles.label, { width: 100, marginLeft: 32 }]}>Contact</ThemedText>
                  <View style={[styles.radioGroup, { flex: 1, minWidth: 150 }]}>
                    {[{ label: 'SMS', val: 'S' }, { label: 'Phone', val: 'P' }, { label: 'None', val: 'N' }].map((opt) => (
                      <TouchableOpacity key={opt.val} style={styles.radioOpt} onPress={() => setAccountType(opt.val)}>
                        <Ionicons name={accountType === opt.val ? "radio-button-on" : "radio-button-off"} size={14} color={accountType === opt.val ? GOLD_COLOR : textColor} />
                        <ThemedText style={{ color: textColor, marginLeft: 4, fontSize: 11, fontWeight: accountType === opt.val ? '700' : 'normal' }}>{opt.label}</ThemedText>
                      </TouchableOpacity>
                    ))}
                  </View>
                </View>

                <View style={styles.row}>
                  <ThemedText style={styles.label}>Address</ThemedText>
                  <TextInput style={[styles.input, { width: 160, backgroundColor: backgroundColor, borderColor: borderColor, color: textColor }]} value={address1} onChangeText={setAddress1} />
                  <ThemedText style={[styles.label, { width: 100, marginLeft: 32 }]}>Phone 2</ThemedText>
                  <TextInput style={[styles.input, { flex: 1, minWidth: 150, backgroundColor: backgroundColor, borderColor: borderColor, color: textColor }]} value={phone2} onChangeText={setPhone2} />
                  
                  <ThemedText style={[styles.label, { width: 100, marginLeft: 32 }]}>Religion</ThemedText>
                  {Platform.OS === 'web' ? (
                    <View style={{ flex: 1, minWidth: 150, height: 28, borderWidth: 1, borderColor, borderRadius: 6, backgroundColor: cardColor }}>
                      {/* @ts-ignore */}
                      <select value={dateType ? dateType.trim().toUpperCase() : ''} onChange={(e: any) => setDateType(e.target.value)} style={{ width: '100%', height: '100%', paddingLeft: 6, paddingRight: 6, fontSize: 11, backgroundColor: 'transparent', color: textColor, outline: 'none', border: 'none' }}>
                        <option value="" style={{ color: '#000' }}></option>
                        <option value="HINDU" style={{ color: '#000' }}>HINDU</option>
                        <option value="MUSLIM" style={{ color: '#000' }}>MUSLIM</option>
                        <option value="CHRISTIAN" style={{ color: '#000' }}>CHRISTIAN</option>
                        <option value="CHINESE" style={{ color: '#000' }}>CHINESE</option>
                      </select>
                    </View>
                  ) : (
                    <TextInput style={[styles.input, { flex: 1, minWidth: 150, backgroundColor: backgroundColor, borderColor: borderColor, color: textColor }]} value={dateType} onChangeText={setDateType} placeholder="HINDU/MUSLIM..." placeholderTextColor={subtitleColor} />
                  )}
                </View>

                <View style={styles.row}>
                  <ThemedText style={styles.label}>Country</ThemedText>
                  <TextInput style={[styles.input, { width: 160, backgroundColor: backgroundColor, borderColor: borderColor, color: textColor }]} value={address2} onChangeText={setAddress2} />
                  <ThemedText style={[styles.label, { width: 100, marginLeft: 32 }]}>Mobile</ThemedText>
                  <TextInput style={[styles.input, { flex: 1, minWidth: 150, backgroundColor: backgroundColor, borderColor: borderColor, color: textColor }]} value={mobile} onChangeText={setMobile} />
                  
                  <ThemedText style={[styles.label, { width: 100, marginLeft: 32 }]}>Date Of Birth</ThemedText>
                  {Platform.OS === 'web' ? (
                    <View style={{ flex: 1, minWidth: 150, height: 30, borderWidth: 1, borderColor, borderRadius: 6, backgroundColor: cardColor, justifyContent: 'center', overflow: 'hidden' }}>
                      {/* @ts-ignore */}
                      <input type="date" value={dob || ''} onChange={(e: any) => setDob(e.target.value)} style={{ width: '100%', height: '100%', paddingLeft: 8, paddingRight: 6, fontSize: 11, backgroundColor: 'transparent', color: textColor, outline: 'none', border: 'none', boxSizing: 'border-box', cursor: 'pointer' }} />
                    </View>
                  ) : (
                    <TextInput style={[styles.input, { flex: 1, minWidth: 150, backgroundColor: backgroundColor, borderColor: borderColor, color: textColor }]} value={dob} onChangeText={setDob} placeholder="YYYY-MM-DD" placeholderTextColor={subtitleColor} />
                  )}
                </View>

                <View style={styles.row}>
                  <ThemedText style={styles.label}>Postal Code</ThemedText>
                  <TextInput style={[styles.input, { width: 160, backgroundColor: backgroundColor, borderColor: borderColor, color: textColor }]} value={address3} onChangeText={setAddress3} />
                  <ThemedText style={[styles.label, { width: 100, marginLeft: 32 }]}>Fax</ThemedText>
                  <TextInput style={[styles.input, { flex: 1, minWidth: 150, backgroundColor: backgroundColor, borderColor: borderColor, color: textColor }]} value={fax} onChangeText={setFax} />
                  
                  <ThemedText style={[styles.label, { width: 100, marginLeft: 32 }]}>Anniversary</ThemedText>
                  {Platform.OS === 'web' ? (
                    <View style={{ flex: 1, minWidth: 150, height: 30, borderWidth: 1, borderColor, borderRadius: 6, backgroundColor: cardColor, justifyContent: 'center', overflow: 'hidden' }}>
                      {/* @ts-ignore */}
                      <input type="date" value={doa || ''} onChange={(e: any) => setDoa(e.target.value)} style={{ width: '100%', height: '100%', paddingLeft: 8, paddingRight: 6, fontSize: 11, backgroundColor: 'transparent', color: textColor, outline: 'none', border: 'none', boxSizing: 'border-box', cursor: 'pointer' }} />
                    </View>
                  ) : (
                    <TextInput style={[styles.input, { flex: 1, minWidth: 150, backgroundColor: backgroundColor, borderColor: borderColor, color: textColor }]} value={doa} onChangeText={setDoa} placeholder="YYYY-MM-DD" placeholderTextColor={subtitleColor} />
                  )}
                </View>

                <View style={styles.row}>
                  <ThemedText style={styles.label}>Nationality</ThemedText>
                  <TextInput style={[styles.input, { width: 160, backgroundColor: backgroundColor, borderColor: borderColor, color: textColor }]} value={address4} onChangeText={setAddress4} />
                  <ThemedText style={[styles.label, { width: 100, marginLeft: 32 }]}>Email</ThemedText>
                  <TextInput style={[styles.input, { flex: 1, minWidth: 150, backgroundColor: backgroundColor, borderColor: borderColor, color: textColor }]} value={eMail} onChangeText={setEMail} />
                  
                  <View style={{ width: 100, marginLeft: 32 }} />
                  <TouchableOpacity style={{ flexDirection: 'row', alignItems: 'center', flex: 1, minWidth: 150 }} onPress={() => setGlAccount1(glAccount1 === 'Y' ? 'N' : 'Y')}>
                    <Ionicons name={glAccount1 === 'Y' ? "checkbox" : "square-outline"} size={18} color={glAccount1 === 'Y' ? '#10B981' : textColor} />
                    <ThemedText style={{ color: glAccount1 === 'Y' ? '#10B981' : textColor, marginLeft: 6, fontWeight: '700', fontSize: 12 }}>Account Active</ThemedText>
                  </TouchableOpacity>
                </View>

                <View style={styles.row}>
                  <ThemedText style={styles.label}>Nric Number</ThemedText>
                  <TextInput style={[styles.input, { width: 160, backgroundColor: backgroundColor, borderColor: borderColor, color: textColor }]} value={nricNo} onChangeText={setNricNo} />
                  <ThemedText style={[styles.label, { width: 100, marginLeft: 32 }]}>Gender</ThemedText>
                  <View style={[styles.radioGroup, { flex: 1, minWidth: 150 }]}>
                    {[{ label: 'Male', val: 'M' }, { label: 'Female', val: 'F' }].map((opt) => (
                      <TouchableOpacity key={opt.val} style={styles.radioOpt} onPress={() => setGender(opt.val)}>
                        <Ionicons name={gender === opt.val ? "radio-button-on" : "radio-button-off"} size={14} color={gender === opt.val ? GOLD_COLOR : textColor} />
                        <ThemedText style={{ color: textColor, marginLeft: 4, fontSize: 11, fontWeight: gender === opt.val ? '700' : 'normal' }}>{opt.label}</ThemedText>
                      </TouchableOpacity>
                    ))}
                  </View>
                  <View style={{ width: 250, marginLeft: 20 }}></View>
                </View>

                <View style={styles.row}>
                  <ThemedText style={styles.label}>Remarks</ThemedText>
                  <TextInput style={[styles.input, { flex: 1, backgroundColor: backgroundColor, borderColor: borderColor, color: textColor }]} value={remarks} onChangeText={setRemarks} />
                </View>
              </View>
            </View>
          </ScrollView>

          {/* Points Card */}
          <View style={[styles.pointsCard, { backgroundColor: isDark ? '#1C1917' : '#FDF8ED', borderColor: GOLD_COLOR }]}>
            <View style={styles.pointsCardBanner}>
              <Ionicons name="ribbon" size={14} color={GOLD_COLOR} />
              <ThemedText style={[styles.pointsCardBannerText, { color: textColor }]}>Reward Points</ThemedText>
            </View>
            <View style={styles.pointItemBox}>
              <ThemedText style={styles.pointLabel}>Total Points</ThemedText>
              <ThemedText style={[styles.pointValue, { color: '#2563EB' }]}>
                {Number(totalPoints || 0).toLocaleString()}
              </ThemedText>
            </View>
            <View style={styles.pointItemBox}>
              <ThemedText style={styles.pointLabel}>Redeemed Points</ThemedText>
              <ThemedText style={[styles.pointValue, { color: '#DC2626' }]}>
                {Number(redeemedPoints || 0).toLocaleString()}
              </ThemedText>
            </View>
            <View style={[styles.pointItemBox, { borderBottomWidth: 0, paddingBottom: 0 }]}>
              <ThemedText style={styles.pointLabel}>Balance Points</ThemedText>
              <ThemedText style={[styles.pointValue, { color: '#059669', fontSize: 17 }]}>
                {(Number(String(totalPoints).replace(/,/g, '')) || 0) - (Number(String(redeemedPoints).replace(/,/g, '')) || 0)}
              </ThemedText>
            </View>
          </View>
        </View>

        {/* TABS */}
        <View style={[styles.tabsContainer, { borderBottomColor: borderColor }]}>
          <TouchableOpacity
            style={[styles.tab, activeTab === 'summary' && [styles.activeTab, { borderBottomColor: GOLD_COLOR }]]}
            onPress={() => setActiveTab('summary')}
          >
            <View style={{ flexDirection: 'row', alignItems: 'center', gap: 6 }}>
              <Ionicons name="receipt-outline" size={14} color={activeTab === 'summary' ? GOLD_COLOR : subtitleColor} />
              <ThemedText style={[styles.tabText, { color: activeTab === 'summary' ? GOLD_COLOR : subtitleColor }]}>Sales Summary</ThemedText>
            </View>
          </TouchableOpacity>
          <TouchableOpacity
            style={[styles.tab, activeTab === 'detail' && [styles.activeTab, { borderBottomColor: GOLD_COLOR }]]}
            onPress={() => setActiveTab('detail')}
          >
            <View style={{ flexDirection: 'row', alignItems: 'center', gap: 6 }}>
              <Ionicons name="list-outline" size={14} color={activeTab === 'detail' ? GOLD_COLOR : subtitleColor} />
              <ThemedText style={[styles.tabText, { color: activeTab === 'detail' ? GOLD_COLOR : subtitleColor }]}>Sales Detail</ThemedText>
            </View>
          </TouchableOpacity>
          <TouchableOpacity
            style={[styles.tab, activeTab === 'points' && [styles.activeTab, { borderBottomColor: GOLD_COLOR }]]}
            onPress={() => setActiveTab('points')}
          >
            <View style={{ flexDirection: 'row', alignItems: 'center', gap: 6 }}>
              <Ionicons name="star-outline" size={14} color={activeTab === 'points' ? GOLD_COLOR : subtitleColor} />
              <ThemedText style={[styles.tabText, { color: activeTab === 'points' ? GOLD_COLOR : subtitleColor }]}>Customer Points</ThemedText>
            </View>
          </TouchableOpacity>
        </View>

        <View style={[styles.gridContainer, { backgroundColor: cardColor, borderColor }]}>
          {activeTab === 'summary' && (
            <View style={{ flex: 1 }}>
              <View style={[styles.gridHeader, { backgroundColor: isDark ? '#1F2937' : '#F1F5F9', borderBottomColor: borderColor, borderBottomWidth: 1 }]}>
                <ThemedText style={[styles.gridHeaderText, { color: textColor }]}>Invoice No</ThemedText>
                <ThemedText style={[styles.gridHeaderText, { color: textColor }]}>Date</ThemedText>
                <ThemedText style={[styles.gridHeaderText, { color: textColor }]}>Weight</ThemedText>
                <ThemedText style={[styles.gridHeaderText, { color: textColor }]}>PCS</ThemedText>
                <ThemedText style={[styles.gridHeaderText, { color: textColor }]}>Price</ThemedText>
              </View>
              <FlatList
                data={salesSummaryData}
                ListEmptyComponent={<View style={styles.gridEmpty}><ThemedText style={{ color: subtitleColor }}>No Sales Summary</ThemedText></View>}
                renderItem={({ item, index }) => (
                  <View style={[styles.gridRow, { borderBottomColor: borderColor, backgroundColor: index % 2 === 0 ? cardColor : (isDark ? '#1C1917' : '#FBFBFC') }]}>
                    <ThemedText style={[styles.gridCellText, { color: '#2563EB', fontWeight: '600' }]}>{item.InvoiceNo}</ThemedText>
                    <ThemedText style={[styles.gridCellText, { color: textColor }]}>{item.InvoiceDate ? item.InvoiceDate.substring(0, 10) : ''}</ThemedText>
                    <ThemedText style={[styles.gridCellText, { color: textColor }]}>{item.Weight}</ThemedText>
                    <ThemedText style={[styles.gridCellText, { color: textColor }]}>{item.PCS}</ThemedText>
                    <ThemedText style={[styles.gridCellText, { color: '#059669', fontWeight: '700' }]}>{item.Price}</ThemedText>
                  </View>
                )}
                keyExtractor={(_, i) => i.toString()}
              />
            </View>
          )}

          {activeTab === 'detail' && (
            <View style={{ flex: 1 }}>
              <View style={[styles.gridHeader, { backgroundColor: isDark ? '#1F2937' : '#F1F5F9', borderBottomColor: borderColor, borderBottomWidth: 1 }]}>
                <ThemedText style={[styles.gridHeaderText, { color: textColor, flex: 1.5 }]}>Item Code</ThemedText>
                <ThemedText style={[styles.gridHeaderText, { color: textColor, flex: 2 }]}>Description</ThemedText>
                <ThemedText style={[styles.gridHeaderText, { color: textColor }]}>Weight</ThemedText>
                <ThemedText style={[styles.gridHeaderText, { color: textColor }]}>PCS</ThemedText>
                <ThemedText style={[styles.gridHeaderText, { color: textColor }]}>Amount</ThemedText>
              </View>
              <FlatList
                data={salesDetailData}
                ListEmptyComponent={<View style={styles.gridEmpty}><ThemedText style={{ color: subtitleColor }}>No Sales Detail</ThemedText></View>}
                renderItem={({ item, index }) => (
                  <View style={[styles.gridRow, { borderBottomColor: borderColor, backgroundColor: index % 2 === 0 ? cardColor : (isDark ? '#1C1917' : '#FBFBFC') }]}>
                    <ThemedText style={[styles.gridCellText, { color: GOLD_COLOR, fontWeight: '600', flex: 1.5 }]}>{item.InventoryCode}</ThemedText>
                    <ThemedText style={[styles.gridCellText, { color: textColor, flex: 2 }]}>{item.Description}</ThemedText>
                    <ThemedText style={[styles.gridCellText, { color: textColor }]}>{item.Weight}</ThemedText>
                    <ThemedText style={[styles.gridCellText, { color: textColor }]}>{item.PCS}</ThemedText>
                    <ThemedText style={[styles.gridCellText, { color: '#059669', fontWeight: '700' }]}>{item.Amount}</ThemedText>
                  </View>
                )}
                keyExtractor={(_, i) => i.toString()}
              />
            </View>
          )}

          {activeTab === 'points' && (
            <View style={{ flex: 1 }}>
              <View style={[styles.gridHeader, { backgroundColor: isDark ? '#1F2937' : '#F1F5F9', borderBottomColor: borderColor, borderBottomWidth: 1 }]}>
                <ThemedText style={[styles.gridHeaderText, { color: textColor }]}>Invoice No</ThemedText>
                <ThemedText style={[styles.gridHeaderText, { color: textColor }]}>Date</ThemedText>
                <ThemedText style={[styles.gridHeaderText, { color: textColor }]}>Net Amt</ThemedText>
                <ThemedText style={[styles.gridHeaderText, { color: textColor }]}>Earned</ThemedText>
                <ThemedText style={[styles.gridHeaderText, { color: textColor }]}>Redeemed</ThemedText>
                <ThemedText style={[styles.gridHeaderText, { color: textColor }]}>Expiry</ThemedText>
              </View>
              <FlatList
                data={pointsData}
                ListEmptyComponent={<View style={styles.gridEmpty}><ThemedText style={{ color: subtitleColor }}>No Points Data</ThemedText></View>}
                renderItem={({ item, index }) => (
                  <View style={[styles.gridRow, { borderBottomColor: borderColor, backgroundColor: index % 2 === 0 ? cardColor : (isDark ? '#1C1917' : '#FBFBFC') }]}>
                    <ThemedText style={[styles.gridCellText, { color: '#2563EB', fontWeight: '600' }]}>{item.InvoiceNo}</ThemedText>
                    <ThemedText style={[styles.gridCellText, { color: textColor }]}>{item.InvoiceDate ? item.InvoiceDate.substring(0, 10) : ''}</ThemedText>
                    <ThemedText style={[styles.gridCellText, { color: textColor }]}>{item.NetAmount}</ThemedText>
                    <ThemedText style={[styles.gridCellText, { color: '#059669', fontWeight: '700' }]}>+{item.EarnedPts}</ThemedText>
                    <ThemedText style={[styles.gridCellText, { color: '#DC2626', fontWeight: '700' }]}>-{item.RedeemedPts}</ThemedText>
                    <ThemedText style={[styles.gridCellText, { color: subtitleColor }]}>{item.ExpiryDate ? item.ExpiryDate.substring(0, 10) : ''}</ThemedText>
                  </View>
                )}
                keyExtractor={(_, i) => i.toString()}
              />
            </View>
          )}
        </View>
      </ScrollView>

      {/* LOOKUP MODAL */}
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
  headerButton: { padding: 8, marginRight: 8 },
  scrollContent: { padding: 12 },
  formSection: { gap: 12 },
  row: { flexDirection: 'row', alignItems: 'center', marginBottom: 8 },
  column: { flex: 1, gap: 10, minWidth: 240 },
  inputGroup: { flexDirection: 'row', alignItems: 'center', gap: 6, marginBottom: 4 },
  label: { fontSize: 12, fontWeight: '600', width: 95 },
  input: {
    borderWidth: 1,
    borderRadius: 6,
    paddingHorizontal: 8,
    paddingVertical: 4,
    fontSize: 12,
    height: 30,
  },
  section: {
    borderRadius: 8,
    borderWidth: 1,
    overflow: 'hidden',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.05,
    shadowRadius: 4,
    elevation: 2,
  },
  sectionHeader: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    paddingHorizontal: 14,
    paddingVertical: 10,
    borderBottomWidth: 1,
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
  lookupBtn: {
    width: 30,
    height: 30,
    justifyContent: 'center',
    alignItems: 'center',
    borderWidth: 1,
    borderLeftWidth: 0,
    borderTopRightRadius: 6,
    borderBottomRightRadius: 6,
  },
  radioGroup: { flexDirection: 'row', gap: 8, alignItems: 'center', minHeight: 24, flexWrap: 'wrap' },
  radioOpt: { flexDirection: 'row', alignItems: 'center' },
  radioPill: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 8,
    paddingVertical: 3,
    borderRadius: 12,
    borderWidth: 1,
  },
  pointsCard: {
    width: 190,
    alignItems: 'stretch',
    borderRadius: 8,
    borderWidth: 1,
    overflow: 'hidden',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.05,
    shadowRadius: 4,
    elevation: 2,
  },
  pointsCardBanner: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 6,
    paddingHorizontal: 12,
    paddingVertical: 10,
    backgroundColor: 'rgba(212, 175, 55, 0.15)',
    borderBottomWidth: 1,
    borderBottomColor: 'rgba(212, 175, 55, 0.3)',
  },
  pointsCardBannerText: {
    fontWeight: '700',
    fontSize: 12,
  },
  pointItemBox: {
    paddingVertical: 10,
    paddingHorizontal: 14,
    alignItems: 'center',
    borderBottomWidth: 1,
    borderBottomColor: 'rgba(212, 175, 55, 0.2)',
  },
  pointLabel: {
    fontSize: 11,
    fontWeight: '600',
    opacity: 0.8,
  },
  pointValue: {
    fontWeight: '800',
    fontSize: 15,
    marginTop: 2,
  },
  tabsContainer: {
    flexDirection: 'row',
    marginTop: 18,
    marginBottom: 8,
    borderBottomWidth: 1,
    gap: 8,
  },
  tab: {
    paddingVertical: 8,
    paddingHorizontal: 14,
    borderBottomWidth: 2,
    borderBottomColor: 'transparent',
  },
  activeTab: {
    borderBottomWidth: 2,
  },
  tabText: { fontWeight: '700', fontSize: 12 },
  gridContainer: {
    borderWidth: 1,
    borderRadius: 8,
    overflow: 'hidden',
    minHeight: 180,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.05,
    shadowRadius: 4,
    elevation: 2,
  },
  gridHeader: { flexDirection: 'row', padding: 10, gap: 6 },
  gridHeaderText: { flex: 1, fontWeight: '700', fontSize: 11 },
  gridEmpty: { padding: 24, alignItems: 'center' },
  gridRow: { flexDirection: 'row', padding: 10, gap: 6, borderBottomWidth: 1, alignItems: 'center' },
  gridCellText: { flex: 1, fontSize: 11 },
  actionBtn: {
    paddingVertical: 6,
    paddingHorizontal: 12,
    borderRadius: 8,
    borderWidth: 1,
    alignItems: 'center',
    justifyContent: 'center',
  },
  actionBtnText: { fontWeight: '700', fontSize: 12 },
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
  modalTitle: { fontSize: 15, fontWeight: '700' },
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

