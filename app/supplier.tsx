import { ThemedText } from '@/components/themed-text';
import { API_BASE_URL } from '@/constants/api';
import { useThemeContext } from '@/contexts/ThemeContext';
import { useThemeColors } from '@/hooks/useThemeColors';
import { Ionicons } from '@expo/vector-icons';
import { Stack, useRouter } from 'expo-router';
import React, { createElement, useEffect, useState } from 'react';
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
const API_URL = `${API_BASE_URL}/supplier`;

export default function SupplierScreen() {
  const themeColors = useThemeColors();
  const { accentColor } = useThemeContext();
  const { background: backgroundColor, card: cardColor, text: textColor, border: borderColor, subtitle: subtitleColor, primaryGold: GOLD_COLOR } = themeColors;




  const colorScheme = useColorScheme();
  const isDark = colorScheme === 'dark';
  const router = useRouter();

  const [data, setData] = useState<any[]>([]);
  const [loading, setLoading] = useState(true);
  const [saving, setSaving] = useState(false);
  const [deleting, setDeleting] = useState(false);

  // Modal State for Lookup
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [isEditing, setIsEditing] = useState(false);
  const [searchQuery, setSearchQuery] = useState('');

  // Form State
  const [supplierCode, setSupplierCode] = useState('');
  const [supplierName, setSupplierName] = useState('');
  const [address1, setAddress1] = useState('');
  const [address2, setAddress2] = useState('');
  const [address3, setAddress3] = useState('');
  const [address4, setAddress4] = useState(''); // Mapping to Nationality
  const [nricNo, setNricNo] = useState(''); // Mapping to BR No
  const [remarks, setRemarks] = useState('');
  const [eMail, setEMail] = useState('');
  const [url, setUrl] = useState(''); // Web Page
  const [phone1, setPhone1] = useState('');
  const [phone2, setPhone2] = useState('');
  const [mobile, setMobile] = useState('');
  const [fax, setFax] = useState('');
  const [gender, setGender] = useState('Male'); // Default
  const [passportNo, setPassportNo] = useState('');
  const [dob, setDob] = useState(''); // DOE
  const [glAccount1, setGlAccount1] = useState('');
  const [glAccount2, setGlAccount2] = useState('');
  const [terms, setTerms] = useState('');
  const [accountType, setAccountType] = useState('Cash'); // Gold, Cash, Both
  const [gstType, setGstType] = useState('Zero Rated'); // Zero Rated, Inclusive, Exclusive

  const [activeTab, setActiveTab] = useState('summary'); // summary, detail
  const [purchases, setPurchases] = useState<any[]>([]);
  const [purchaseDetails, setPurchaseDetails] = useState<any[]>([]);
  const [selectedJobSheetNo, setSelectedJobSheetNo] = useState('');
  const [toast, setToast] = useState<{ message: string, type: 'success' | 'error' } | null>(null);

  const showToast = (message: string, type: 'success' | 'error' = 'success') => {
    setToast({ message, type });
    setTimeout(() => {
      setToast(null);
    }, 3000);
  };
  const headerBgColor = isDark ? '#2A2A2A' : '#F5F5F5';

  const fetchSuppliers = async () => {
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

  const fetchPurchases = async (code: string) => {
    try {
      const response = await fetch(`${API_URL}/${encodeURIComponent(code)}/purchases?t=${Date.now()}`);
      const json = await response.json();
      if (json.success) {
        setPurchases(json.data);
        if (json.data && json.data.length > 0) {
          const first = json.data[0];
          loadPurchaseDetails(first.JobSheetNo, first.PurchaseBillNo, first.PurchaseBillDate, false, code);
        } else {
          setPurchaseDetails([]);
          setSelectedJobSheetNo('');
        }
      } else {
        setPurchases([]);
        setPurchaseDetails([]);
        setSelectedJobSheetNo('');
      }
    } catch (e) {
      console.error(e);
    }
  };

  const loadPurchaseDetails = async (jobSheetNo: string, billNo: string, purchaseDate: string, switchTab = true, overrideSupplierCode?: string) => {
    try {
      const currentSupplierCode = overrideSupplierCode || supplierCode;
      setSelectedJobSheetNo(jobSheetNo);
      if (switchTab) setActiveTab('detail');
      const response = await fetch(`${API_URL}/${encodeURIComponent(currentSupplierCode)}/purchases/${encodeURIComponent(jobSheetNo)}?t=${Date.now()}`);
      const json = await response.json();
      if (json.success) {
        const enriched = json.data.map((d: any) => ({
          ...d,
          BillNo: billNo,
          PurchaseDate: purchaseDate
        }));
        setPurchaseDetails(enriched);
      } else {
        setPurchaseDetails([]);
      }
    } catch (e) {
      console.error(e);
    }
  };

  useEffect(() => {
    fetchSuppliers();
  }, []);

  const handleSave = async () => {
    if (!supplierCode || !supplierCode.trim()) {
      showToast('Supplier Code is required', 'error');
      return;
    }

    const payload = {
      SupplierCode: supplierCode.trim(),
      SupplierName: supplierName.trim(),
      Address1: address1.trim(),
      Address2: address2.trim(),
      Address3: address3.trim(),
      Address4: address4.trim(),
      NRICNo: nricNo.trim(),
      Remarks: remarks.trim(),
      eMail: eMail.trim(),
      Url: url.trim(),
      Phone1: phone1.trim(),
      Phone2: phone2.trim(),
      Mobile: mobile.trim(),
      Fax: fax.trim(),
      Gender: gender === 'Female' ? 'F' : 'M',
      PassportNo: passportNo.trim(),
      DOB: dob.trim() || null,
      GLAccount1: glAccount1.trim(),
      GLAccount2: glAccount2.trim(),
      Terms: terms.trim(),
      AccountType: accountType === 'Gold' ? 'G' : (accountType === 'Cash' ? 'C' : 'B'),
      GSTType: gstType === 'Exclusive' ? 'E' : (gstType === 'Inclusive' ? 'I' : ''),
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
        showToast(`Supplier ${isEditing ? 'updated' : 'added'} successfully`, 'success');
        fetchSuppliers();
      } else {
        showToast(json.message || 'Failed to save', 'error');
      }
    } catch (error) {
      console.error('Save error:', error);
      showToast('Failed to save supplier', 'error');
    } finally {
      setSaving(false);
    }
  };

  const handleDelete = () => {
    if (!supplierCode) return;
    Alert.alert(
      'Confirm Delete',
      'Are you sure you want to delete this supplier?',
      [
        { text: 'Cancel', style: 'cancel' },
        {
          text: 'Delete',
          style: 'destructive',
          onPress: async () => {
            setDeleting(true);
            try {
              const response = await fetch(`${API_URL}?id=${encodeURIComponent(supplierCode)}`, { method: 'DELETE' });
              const json = await response.json();
              if (json.success) {
                showToast('Supplier deleted successfully', 'success');
                handleClear();
                fetchSuppliers();
              } else {
                showToast(json.message || 'Failed to delete', 'error');
              }
            } catch (error) {
              console.error('Delete error:', error);
              showToast('Failed to delete supplier', 'error');
            } finally {
              setDeleting(false);
            }
          },
        },
      ]
    );
  };

  const handleClear = () => {
    setSupplierCode('');
    setSupplierName('');
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
    setGender('Male');
    setPassportNo('');
    setDob('');
    setGlAccount1('');
    setGlAccount2('');
    setTerms('');
    setAccountType('Cash');
    setGstType('Zero Rated');
    setIsEditing(false);
    setPurchases([]);
    setPurchaseDetails([]);
    setSelectedJobSheetNo('');
  };

  const selectSupplier = (item: any) => {
    setSupplierCode(item.SupplierCode);
    setSupplierName(item.SupplierName || '');
    setAddress1(item.Address1 || '');
    setAddress2(item.Address2 || '');
    setAddress3(item.Address3 || '');
    setAddress4(item.Address4 || '');
    setNricNo(item.NRICNo || '');
    setRemarks(item.Remarks || '');
    setEMail(item.eMail || '');
    setUrl(item.Url || '');
    setPhone1(item.Phone1 || '');
    setPhone2(item.Phone2 || '');
    setMobile(item.Mobile || '');
    setFax(item.Fax || '');
    const dbGender = item.Gender ? item.Gender.toUpperCase().trim() : '';
    setGender(dbGender === 'F' ? 'Female' : 'Male');

    const dbAccount = item.AccountType ? item.AccountType.toUpperCase().trim() : '';
    if (dbAccount === 'GL' || dbAccount === 'G') setAccountType('Gold');
    else if (dbAccount === 'C') setAccountType('Cash');
    else if (dbAccount === 'B') setAccountType('Both');
    else setAccountType('Cash');

    const dbGst = item.GSTType ? item.GSTType.toUpperCase().trim() : '';
    if (dbGst === 'E') setGstType('Exclusive');
    else if (dbGst === 'I') setGstType('Inclusive');
    else setGstType('Zero Rated');

    setPassportNo(item.PassportNo || '');
    setDob(item.DOB ? item.DOB.substring(0, 10) : '');
    setGlAccount1(item.GLAccount1 || '');
    setGlAccount2(item.GLAccount2 || '');
    setTerms(item.Terms || '');
    setIsEditing(true);
    setIsModalVisible(false);
    fetchPurchases(item.SupplierCode);
  };

  const renderLookupItem = ({ item, index }: { item: any; index: number }) => {
    const isEven = index % 2 === 0;
    return (
      <TouchableOpacity
        style={[
          styles.gridRow,
          {
            borderBottomColor: borderColor,
            backgroundColor: isEven ? cardColor : (isDark ? '#1C1917' : '#FBFBFC')
          }
        ]}
        onPress={() => selectSupplier(item)}
      >
        <ThemedText style={[styles.gridCell, { color: GOLD_COLOR, fontWeight: '700' }]}>{item.SupplierCode}</ThemedText>
        <ThemedText style={[styles.gridCell, { color: textColor, flex: 2, fontWeight: '600' }]}>{item.SupplierName}</ThemedText>
        <ThemedText style={[styles.gridCell, { color: subtitleColor, flex: 2 }]} numberOfLines={1}>{item.Address1}</ThemedText>
        <ThemedText style={[styles.gridCell, { color: subtitleColor, flex: 2 }]} numberOfLines={1}>{item.Address2}</ThemedText>
      </TouchableOpacity>
    );
  };

  return (
    <SafeAreaView style={[styles.container, { backgroundColor }]} edges={['bottom', 'left', 'right']}>
      <Stack.Screen
        options={{
          headerShown: true,
          title: 'Supplier Master',
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
        <ScrollView horizontal={true} showsHorizontalScrollIndicator={true} style={{ marginBottom: 4 }} contentContainerStyle={{ flexGrow: 1 }}>
          <View style={[styles.section, { width: '100%', minWidth: 900, backgroundColor: cardColor, borderColor }]}>
            <View style={[styles.sectionHeader, { backgroundColor: isDark ? 'rgba(212, 175, 55, 0.12)' : '#FDF8ED', borderBottomColor: borderColor }]}>
              <View style={{ flexDirection: 'row', alignItems: 'center', gap: 8 }}>
                <View style={[styles.iconBadge, { backgroundColor: GOLD_COLOR }]}>
                  <Ionicons name="business" size={13} color="#FFFFFF" />
                </View>
                <ThemedText style={{ color: textColor, fontWeight: '700', fontSize: 13 }}>Supplier Details</ThemedText>
              </View>
              {supplierCode ? (
                <View style={styles.codeBadge}>
                  <Ionicons name="shield-checkmark" size={12} color="#D4AF37" />
                  <ThemedText style={styles.codeBadgeText}>{supplierCode}</ThemedText>
                </View>
              ) : null}
            </View>

            <View style={{ padding: 12 }}>
              <View style={styles.row}>
                <ThemedText style={styles.label}>Supplier Code</ThemedText>
                <View style={{ width: 160, flexDirection: 'row' }}>
                  <TextInput style={[styles.input, { flex: 1, backgroundColor: cardColor, borderColor: borderColor, color: textColor }]} value={supplierCode} onChangeText={setSupplierCode} editable={!isEditing} />
                  <TouchableOpacity style={[styles.lookupBtn, { backgroundColor: GOLD_COLOR, borderColor: GOLD_COLOR }]} onPress={() => setIsModalVisible(true)}>
                    <Ionicons name="search" size={12} color="#FFFFFF" />
                  </TouchableOpacity>
                </View>
                <ThemedText style={[styles.label, { width: 80, marginLeft: 32 }]}>EMail</ThemedText>
                <TextInput style={[styles.input, { flex: 1, minWidth: 150, backgroundColor: backgroundColor, borderColor: borderColor, color: textColor }]} value={eMail} onChangeText={setEMail} keyboardType="email-address" autoCapitalize="none" />
                <ThemedText style={[styles.label, { width: 100, marginLeft: 20 }]}>GL Account 1</ThemedText>
                <TextInput style={[styles.input, { width: 220, backgroundColor: backgroundColor, borderColor: borderColor, color: textColor }]} value={glAccount1} onChangeText={setGlAccount1} />
              </View>

              <View style={styles.row}>
                <ThemedText style={styles.label}>Supplier Name</ThemedText>
                <TextInput style={[styles.input, { width: 160, backgroundColor: backgroundColor, borderColor: borderColor, color: textColor }]} value={supplierName} onChangeText={setSupplierName} />
                <ThemedText style={[styles.label, { width: 80, marginLeft: 32 }]}>Web Page</ThemedText>
                <TextInput style={[styles.input, { flex: 1, minWidth: 150, backgroundColor: backgroundColor, borderColor: borderColor, color: textColor }]} value={url} onChangeText={setUrl} autoCapitalize="none" />
                <ThemedText style={[styles.label, { width: 100, marginLeft: 20 }]}>GL Account 2</ThemedText>
                <TextInput style={[styles.input, { width: 220, backgroundColor: backgroundColor, borderColor: borderColor, color: textColor }]} value={glAccount2} onChangeText={setGlAccount2} />
              </View>

              <View style={styles.row}>
                <ThemedText style={styles.label}>Address</ThemedText>
                <TextInput style={[styles.input, { width: 160, backgroundColor: backgroundColor, borderColor: borderColor, color: textColor }]} value={address1} onChangeText={setAddress1} />
                <ThemedText style={[styles.label, { width: 80, marginLeft: 32 }]}>Phone 1</ThemedText>
                <TextInput style={[styles.input, { flex: 1, minWidth: 150, backgroundColor: backgroundColor, borderColor: borderColor, color: textColor }]} value={phone1} onChangeText={setPhone1} keyboardType="phone-pad" />
                <ThemedText style={[styles.label, { width: 100, marginLeft: 20 }]}>Terms</ThemedText>
                <TextInput style={[styles.input, { width: 220, backgroundColor: backgroundColor, borderColor: borderColor, color: textColor }]} value={terms} onChangeText={setTerms} />
              </View>

              <View style={styles.row}>
                <ThemedText style={styles.label}></ThemedText>
                <TextInput style={[styles.input, { width: 160, backgroundColor: backgroundColor, borderColor: borderColor, color: textColor }]} value={address2} onChangeText={setAddress2} />
                <ThemedText style={[styles.label, { width: 80, marginLeft: 32 }]}>Phone 2</ThemedText>
                <TextInput style={[styles.input, { flex: 1, minWidth: 150, backgroundColor: backgroundColor, borderColor: borderColor, color: textColor }]} value={phone2} onChangeText={setPhone2} keyboardType="phone-pad" />

                <ThemedText style={[styles.label, { width: 100, marginLeft: 20 }]}>Account Type</ThemedText>
                <View style={[styles.radioGroup, { width: 220 }]}>
                  {['Gold', 'Cash', 'Both'].map((opt) => {
                    const isSelected = accountType === opt;
                    return (
                      <TouchableOpacity key={opt} style={styles.radioOpt} onPress={() => setAccountType(opt)}>
                        <Ionicons name={isSelected ? "radio-button-on" : "radio-button-off"} size={14} color={isSelected ? GOLD_COLOR : subtitleColor} />
                        <ThemedText style={{ color: isSelected ? GOLD_COLOR : textColor, marginLeft: 4, fontSize: 11, fontWeight: isSelected ? '700' : 'normal' }}>{opt}</ThemedText>
                      </TouchableOpacity>
                    );
                  })}
                </View>
              </View>

              <View style={styles.row}>
                <ThemedText style={styles.label}></ThemedText>
                <TextInput style={[styles.input, { width: 160, backgroundColor: backgroundColor, borderColor: borderColor, color: textColor }]} value={address3} onChangeText={setAddress3} />
                <ThemedText style={[styles.label, { width: 80, marginLeft: 32 }]}>Mobile</ThemedText>
                <TextInput style={[styles.input, { flex: 1, minWidth: 150, backgroundColor: backgroundColor, borderColor: borderColor, color: textColor }]} value={mobile} onChangeText={setMobile} keyboardType="phone-pad" />

                <ThemedText style={[styles.label, { width: 100, marginLeft: 20 }]}>Invoice Type</ThemedText>
                <View style={[styles.radioGroup, { width: 220 }]}>
                  {['Zero Rated', 'Inclusive', 'Exclusive'].map((opt) => {
                    const isSelected = gstType === opt;
                    return (
                      <TouchableOpacity key={opt} style={styles.radioOpt} onPress={() => setGstType(opt)}>
                        <Ionicons name={isSelected ? "radio-button-on" : "radio-button-off"} size={14} color={isSelected ? GOLD_COLOR : subtitleColor} />
                        <ThemedText style={{ color: isSelected ? GOLD_COLOR : textColor, marginLeft: 4, fontSize: 11, fontWeight: isSelected ? '700' : 'normal' }}>{opt}</ThemedText>
                      </TouchableOpacity>
                    );
                  })}
                </View>
              </View>

              <View style={styles.row}>
                <ThemedText style={styles.label}>Nationality</ThemedText>
                <TextInput style={[styles.input, { width: 160, backgroundColor: backgroundColor, borderColor: borderColor, color: textColor }]} value={address4} onChangeText={setAddress4} />
                <ThemedText style={[styles.label, { width: 80, marginLeft: 32 }]}>Fax</ThemedText>
                <TextInput style={[styles.input, { flex: 1, minWidth: 150, backgroundColor: backgroundColor, borderColor: borderColor, color: textColor }]} value={fax} onChangeText={setFax} />

                <ThemedText style={[styles.label, { width: 100, marginLeft: 20 }]}>Passport</ThemedText>
                <TextInput style={[styles.input, { width: 220, backgroundColor: backgroundColor, borderColor: borderColor, color: textColor }]} value={passportNo} onChangeText={setPassportNo} />
              </View>

              <View style={styles.row}>
                <ThemedText style={styles.label}>BR No</ThemedText>
                <TextInput style={[styles.input, { width: 160, backgroundColor: backgroundColor, borderColor: borderColor, color: textColor }]} value={nricNo} onChangeText={setNricNo} />
                <ThemedText style={[styles.label, { width: 80, marginLeft: 32 }]}>Gender</ThemedText>
                <View style={[styles.radioGroup, { flex: 1, minWidth: 150 }]}>
                  {['Male', 'Female'].map((opt) => {
                    const isSelected = gender === opt;
                    return (
                      <TouchableOpacity key={opt} style={styles.radioOpt} onPress={() => setGender(opt)}>
                        <Ionicons name={isSelected ? "radio-button-on" : "radio-button-off"} size={14} color={isSelected ? GOLD_COLOR : subtitleColor} />
                        <ThemedText style={{ color: isSelected ? GOLD_COLOR : textColor, marginLeft: 4, fontSize: 11, fontWeight: isSelected ? '700' : 'normal' }}>{opt}</ThemedText>
                      </TouchableOpacity>
                    );
                  })}
                </View>
                <ThemedText style={[styles.label, { width: 100, marginLeft: 20 }]}>DOE</ThemedText>
                {Platform.OS === 'web' ? (
                  createElement('input', {
                    type: 'date',
                    value: dob,
                    onChange: (e: any) => setDob(e.target.value),
                    style: { width: 220, paddingHorizontal: 6, borderWidth: 1, borderColor, backgroundColor, color: textColor, borderRadius: 6, outline: 'none', height: 26, fontSize: 11, boxSizing: 'border-box' }
                  })
                ) : (
                  <TextInput style={[styles.input, { width: 220, backgroundColor: backgroundColor, borderColor: borderColor, color: textColor }]} value={dob} onChangeText={setDob} placeholder="YYYY-MM-DD" placeholderTextColor={subtitleColor} />
                )}
              </View>

              <View style={styles.row}>
                <ThemedText style={styles.label}>Remarks</ThemedText>
                <TextInput style={[styles.input, { flex: 1, backgroundColor: backgroundColor, borderColor: borderColor, color: textColor }]} value={remarks} onChangeText={setRemarks} />
              </View>
            </View>
          </View>
        </ScrollView>

        {/* TABS */}
        <View style={[styles.tabsContainer, { borderBottomColor: borderColor }]}>
          <TouchableOpacity
            style={[styles.tab, activeTab === 'summary' && [styles.activeTab, { borderBottomColor: GOLD_COLOR }]]}
            onPress={() => setActiveTab('summary')}
          >
            <View style={{ flexDirection: 'row', alignItems: 'center', gap: 6 }}>
              <Ionicons name="newspaper-outline" size={14} color={activeTab === 'summary' ? GOLD_COLOR : subtitleColor} />
              <ThemedText style={[styles.tabText, { color: activeTab === 'summary' ? GOLD_COLOR : subtitleColor }]}>Purchase Summary</ThemedText>
            </View>
          </TouchableOpacity>
          <TouchableOpacity
            style={[styles.tab, activeTab === 'detail' && [styles.activeTab, { borderBottomColor: GOLD_COLOR }]]}
            onPress={() => setActiveTab('detail')}
          >
            <View style={{ flexDirection: 'row', alignItems: 'center', gap: 6 }}>
              <Ionicons name="list-outline" size={14} color={activeTab === 'detail' ? GOLD_COLOR : subtitleColor} />
              <ThemedText style={[styles.tabText, { color: activeTab === 'detail' ? GOLD_COLOR : subtitleColor }]}>Purchase Detail</ThemedText>
            </View>
          </TouchableOpacity>
        </View>

        {activeTab === 'summary' && (
          <View style={[styles.gridContainer, { backgroundColor: cardColor, borderColor }]}>
            <View style={[styles.gridHeader, { backgroundColor: isDark ? '#1F2937' : '#F1F5F9', borderBottomWidth: 1, borderBottomColor: borderColor }]}>
              <ThemedText style={[styles.gridHeaderText, { color: textColor }]}>Job Sheet</ThemedText>
              <ThemedText style={[styles.gridHeaderText, { color: textColor }]}>Bill No</ThemedText>
              <ThemedText style={[styles.gridHeaderText, { color: textColor }]}>Purchase Date</ThemedText>
              <ThemedText style={[styles.gridHeaderText, { color: textColor }]}>Weight</ThemedText>
              <ThemedText style={[styles.gridHeaderText, { color: textColor }]}>PCS</ThemedText>
              <ThemedText style={[styles.gridHeaderText, { color: textColor }]}>Local Cost</ThemedText>
            </View>
            {purchases.length === 0 ? (
              <View style={styles.gridEmpty}>
                <Ionicons name="receipt-outline" size={28} color={subtitleColor} style={{ marginBottom: 6 }} />
                <ThemedText style={{ color: subtitleColor }}>No purchases found.</ThemedText>
              </View>
            ) : (
              purchases.map((item, index) => (
                <TouchableOpacity
                  key={index}
                  style={[styles.gridRow, { borderBottomColor: borderColor, backgroundColor: index % 2 === 0 ? cardColor : (isDark ? '#1C1917' : '#FBFBFC') }]}
                  onPress={() => loadPurchaseDetails(item.JobSheetNo, item.PurchaseBillNo, item.PurchaseBillDate)}
                >
                  <ThemedText style={[styles.gridCell, { color: '#3B82F6', fontWeight: '700' }]}>{item.JobSheetNo}</ThemedText>
                  <ThemedText style={[styles.gridCell, { color: textColor }]}>{item.PurchaseBillNo}</ThemedText>
                  <ThemedText style={[styles.gridCell, { color: textColor }]}>{item.PurchaseBillDate ? item.PurchaseBillDate.substring(0, 10) : ''}</ThemedText>
                  <ThemedText style={[styles.gridCell, { color: textColor }]}>{item.TotalWeight24K}</ThemedText>
                  <ThemedText style={[styles.gridCell, { color: textColor }]}>{item.TotalPCS}</ThemedText>
                  <ThemedText style={[styles.gridCell, { color: '#059669', fontWeight: '700' }]}>{item.TotalLocalCost}</ThemedText>
                </TouchableOpacity>
              ))
            )}
          </View>
        )}

        {activeTab === 'detail' && (
          <View style={[styles.gridContainer, { backgroundColor: cardColor, borderColor }]}>
            <View style={[styles.gridHeader, { backgroundColor: isDark ? '#1F2937' : '#F1F5F9', borderBottomWidth: 1, borderBottomColor: borderColor }]}>
              <ThemedText style={[styles.gridHeaderText, { color: textColor }]}>Bill No</ThemedText>
              <ThemedText style={[styles.gridHeaderText, { color: textColor }]}>Purchase Date</ThemedText>
              <ThemedText style={[styles.gridHeaderText, { color: textColor }]}>Item Code</ThemedText>
              <ThemedText style={[styles.gridHeaderText, { color: textColor }]}>Description</ThemedText>
              <ThemedText style={[styles.gridHeaderText, { color: textColor }]}>Weight</ThemedText>
              <ThemedText style={[styles.gridHeaderText, { color: textColor }]}>PCS</ThemedText>
              <ThemedText style={[styles.gridHeaderText, { color: textColor }]}>Price</ThemedText>
            </View>
            {purchaseDetails.length === 0 ? (
              <View style={styles.gridEmpty}>
                <Ionicons name="list-outline" size={28} color={subtitleColor} style={{ marginBottom: 6 }} />
                <ThemedText style={{ color: subtitleColor }}>No details found.</ThemedText>
              </View>
            ) : (
              purchaseDetails.map((item, index) => (
                <View key={index} style={[styles.gridRow, { borderBottomColor: borderColor, backgroundColor: index % 2 === 0 ? cardColor : (isDark ? '#1C1917' : '#FBFBFC') }]}>
                  <ThemedText style={[styles.gridCell, { color: '#3B82F6', fontWeight: '600' }]}>{item.BillNo}</ThemedText>
                  <ThemedText style={[styles.gridCell, { color: textColor }]}>{item.PurchaseDate ? item.PurchaseDate.substring(0, 10) : ''}</ThemedText>
                  <ThemedText style={[styles.gridCell, { color: GOLD_COLOR, fontWeight: '700' }]}>{item.Inventorycode}</ThemedText>
                  <ThemedText style={[styles.gridCell, { color: textColor }]}>{item.Description}</ThemedText>
                  <ThemedText style={[styles.gridCell, { color: textColor }]}>{item.Weight}</ThemedText>
                  <ThemedText style={[styles.gridCell, { color: textColor }]}>{item.Pcs}</ThemedText>
                  <ThemedText style={[styles.gridCell, { color: '#059669', fontWeight: '700' }]}>{item.TotalCost}</ThemedText>
                </View>
              ))
            )}
          </View>
        )}

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
                      <Ionicons name="business" size={14} color="#FFF" />
                    </View>
                    <ThemedText style={[styles.modalTitle, { color: textColor }]}>Select Supplier</ThemedText>
                  </View>
                  <TouchableOpacity onPress={() => setIsModalVisible(false)} hitSlop={{ top: 10, bottom: 10, left: 10, right: 10 }}>
                    <Ionicons name="close-circle" size={22} color={subtitleColor} />
                  </TouchableOpacity>
                </View>

                <View style={[styles.modalSearchBar, { borderBottomColor: borderColor, backgroundColor: cardColor }]}>
                  <Ionicons name="search" size={16} color={subtitleColor} style={{ marginRight: 6 }} />
                  <TextInput
                    style={[styles.searchInput, { color: textColor }]}
                    placeholder="Search code or supplier name..."
                    placeholderTextColor={subtitleColor}
                    value={searchQuery}
                    onChangeText={setSearchQuery}
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
                    <ThemedText style={{ marginTop: 12, color: subtitleColor }}>Loading suppliers...</ThemedText>
                  </View>
                ) : (
                  <>
                    <View style={[styles.gridHeader, { backgroundColor: isDark ? '#1F2937' : '#F1F5F9', borderBottomWidth: 1, borderBottomColor: borderColor }]}>
                      <ThemedText style={[styles.gridHeaderText, { color: textColor }]}>Supplier Code</ThemedText>
                      <ThemedText style={[styles.gridHeaderText, { color: textColor, flex: 2 }]}>Supplier Name</ThemedText>
                      <ThemedText style={[styles.gridHeaderText, { color: textColor, flex: 2 }]}>Address 1</ThemedText>
                      <ThemedText style={[styles.gridHeaderText, { color: textColor, flex: 2 }]}>Address 2</ThemedText>
                    </View>
                    <FlatList
                      data={data.filter(s =>
                        (s.SupplierCode || '').toLowerCase().includes(searchQuery.toLowerCase()) ||
                        (s.SupplierName || '').toLowerCase().includes(searchQuery.toLowerCase())
                      )}
                      keyExtractor={(item) => item.SupplierCode}
                      renderItem={renderLookupItem}
                      contentContainerStyle={{ paddingBottom: 20 }}
                    />
                  </>
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
  headerButton: { padding: 4, marginRight: 8 },
  scrollContent: { padding: 16 },
  input: {
    borderWidth: 1,
    borderRadius: 6,
    paddingHorizontal: 8,
    paddingVertical: 2,
    fontSize: 11,
    height: 28,
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
  codeBadge: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 4,
    backgroundColor: 'rgba(212, 175, 55, 0.15)',
    paddingHorizontal: 8,
    paddingVertical: 3,
    borderRadius: 12,
  },
  codeBadgeText: {
    color: '#B48218',
    fontWeight: '700',
    fontSize: 11,
  },
  row: { flexDirection: 'row', alignItems: 'center', marginBottom: 6 },
  label: { fontSize: 11, fontWeight: '600', width: 95 },
  lookupContainer: { flex: 1, flexDirection: 'row', gap: 8 },
  lookupBtn: {
    width: 28,
    height: 28,
    justifyContent: 'center',
    alignItems: 'center',
    borderWidth: 1,
    borderLeftWidth: 0,
    borderTopRightRadius: 6,
    borderBottomRightRadius: 6,
  },
  lookupButton: {
    paddingHorizontal: 12,
    justifyContent: 'center',
    alignItems: 'center',
    borderRadius: 6,
  },
  radioGroup: { flexDirection: 'row', gap: 8, alignItems: 'center', minHeight: 20 },
  radioOpt: { flexDirection: 'row', alignItems: 'center' },
  borderGroup: {
    flex: 1,
    borderWidth: 1,
    borderRadius: 6,
    padding: 6,
    marginTop: 6,
    position: 'relative'
  },
  groupLabel: {
    position: 'absolute',
    top: -8,
    left: 8,
    paddingHorizontal: 4,
    fontSize: 11,
    fontWeight: '700',
  },
  tabsContainer: {
    flexDirection: 'row',
    marginTop: 14,
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
    minHeight: 160,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.05,
    shadowRadius: 4,
    elevation: 2,
  },
  gridHeader: { flexDirection: 'row', padding: 10, gap: 6 },
  gridHeaderText: { flex: 1, fontWeight: '700', fontSize: 11 },
  gridRow: { flexDirection: 'row', padding: 10, gap: 6, borderBottomWidth: 1, alignItems: 'center' },
  gridCell: { flex: 1, fontSize: 11 },
  gridEmpty: { padding: 24, alignItems: 'center' },
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
  modalSearchBar: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 12,
    paddingVertical: 8,
    borderBottomWidth: 1,
  },
  searchInput: {
    flex: 1,
    height: 32,
    fontSize: 13,
    paddingHorizontal: 6,
  },
});
