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
  useColorScheme,
  View
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';

const GOLD_COLOR = '#D4AF37';
const API_URL = `${API_BASE_URL}/company`;

const EMPTY_FORM = {
  CompanyID: '',
  Name: '',
  Address1: '',
  Address2: '',
  City: '',
  State: '',
  Country: '',
  PostalCode: '',
  Phone: '',
  Fax: '',
  Email: '',
  Url: '',
  DateFormat: '',
  TimeFormat: '',
  CurrencySign: '',
  DefaultCurrency: '',
  GSTRegNo: '',
  Message1: '',
  Message2: '',
  DisplayMessage1: '',
  DisplayMessage2: '',
  GstPerc: '',
  NextInvoice: '',
  NextReciptNo: '',
  MaxDiscountAllowed: '',
  MaxPriceChangeAllowed: '',
  NextPONo: '',
  TaxIN: '',
  NextAverageCostPercentage: '',
  ExpiredTime: '',
  NextCreditInvoice: '',
  TaxTaken: '0',
  NextTradeInSalesNo: '',
  NextPurReNo: '',
  NextPaymentNo: ''
};

export default function CompanyScreen() {
  const themeColors = useThemeColors();
  const { accentColor } = useThemeContext();
  const { background: backgroundColor, card: cardColor, text: textColor, border: borderColor, primaryGold: GOLD_COLOR, subtitle: subtitleColor } = themeColors;
  const colorScheme = useColorScheme();
  const isDark = colorScheme === 'dark';
  const router = useRouter();

  const [data, setData] = useState<any[]>([]);
  const [loading, setLoading] = useState(true);
  const [saving, setSaving] = useState(false);
  const [deletingId, setDeletingId] = useState<string | null>(null);
  const [successMsg, setSuccessMsg] = useState('');
  const [errorMsg, setErrorMsg] = useState('');

  const showSuccess = (msg: string) => {
    setSuccessMsg(msg);
    setTimeout(() => setSuccessMsg(''), 3000);
  };

  const showError = (msg: string) => {
    setErrorMsg(msg);
    setTimeout(() => setErrorMsg(''), 3000);
  };

  // Modal State
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [isEditing, setIsEditing] = useState(false);

  // Form State
  const [formData, setFormData] = useState<any>({ ...EMPTY_FORM });

  const headerBgColor = isDark ? '#2A2A2A' : '#F5F5F5';

  const fetchCompanies = async () => {
    try {
      const response = await fetch(`${API_URL}?t=${Date.now()}`, {
        headers: { 'Cache-Control': 'no-cache', 'x-db-name': 'smartgold' }
      });
      const json = await response.json();
      if (json.success) {
        setData(json.data);
      } else {
        console.error('Failed to fetch:', json.message);
      }
    } catch (error) {
      console.error('Network Error:', error);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchCompanies();
  }, []);

  const handleChange = (field: string, value: string) => {
    setFormData((prev: any) => ({ ...prev, [field]: value }));
  };

  const handleSave = async () => {
    if (!formData.CompanyID || !String(formData.CompanyID).trim()) {
      showError('Company Code is required');
      return;
    }
    if (!formData.Name || !String(formData.Name).trim()) {
      showError('Company Name is required');
      return;
    }

    setSaving(true);
    try {
      const method = isEditing ? 'PUT' : 'POST';
      const url = isEditing ? `${API_URL}/${formData.CompanyID}` : API_URL;

      const response = await fetch(url, {
        method,
        headers: {
          'Content-Type': 'application/json',
          'x-db-name': 'smartgold'
        },
        body: JSON.stringify(formData),
      });

      const json = await response.json();
      if (json.success) {
        showSuccess(`Company ${isEditing ? 'updated' : 'added'} successfully`);
        setIsModalVisible(false);
        fetchCompanies();
      } else {
        showError(json.message || 'Failed to save');
      }
    } catch (error) {
      console.error('Save error:', error);
      showError('Failed to save company');
    } finally {
      setSaving(false);
    }
  };

  const executeDelete = async (id: string) => {
    setDeletingId(id);
    try {
      const response = await fetch(`${API_URL}/${encodeURIComponent(id)}`, {
        method: 'DELETE',
        headers: { 'x-db-name': 'smartgold' }
      });
      const json = await response.json();
      if (json.success) {
        showSuccess('Company deleted successfully');
        fetchCompanies();
      } else {
        showError(json.message || 'Failed to delete');
      }
    } catch (error) {
      console.error('Delete error:', error);
      showError('Failed to delete company');
    } finally {
      setDeletingId(null);
    }
  };

  const handleDelete = (id: string) => {
    if (Platform.OS === 'web') {
      if (window.confirm(`Are you sure you want to delete Company ${id}?`)) {
        executeDelete(id);
      }
    } else {
      Alert.alert('Confirm Delete', `Are you sure you want to delete Company ${id}?`, [
        { text: 'Cancel', style: 'cancel' },
        { text: 'Delete', style: 'destructive', onPress: () => executeDelete(id) },
      ]);
    }
  };

  const openAddModal = () => {
    setIsEditing(false);
    setFormData({ ...EMPTY_FORM });
    setIsModalVisible(true);
  };

  const openEditModal = (item: any) => {
    setIsEditing(true);
    const cleanData = Object.keys(item).reduce((acc: any, key) => {
      acc[key] = item[key] === null ? '' : String(item[key]);
      return acc;
    }, {});
    setFormData({ ...EMPTY_FORM, ...cleanData });
    setIsModalVisible(true);
  };

  const renderCheckbox = (label: string, field: string, customWidth?: string) => {
    const isChecked = formData[field] === '1' || formData[field] === 1;

    return (
      <View style={[styles.inputGroup, customWidth ? { width: customWidth as any } : {}]}>
        <ThemedText style={[styles.inputLabel, { color: textColor }]}>
          {label}
        </ThemedText>
        <TouchableOpacity
          onPress={() => handleChange(field, isChecked ? '0' : '1')}
          style={{ height: 26, justifyContent: 'center' }}
          activeOpacity={0.7}
        >
          <Ionicons
            name={isChecked ? "checkbox" : "square-outline"}
            size={20}
            color={isChecked ? '#10B981' : subtitleColor}
          />
        </TouchableOpacity>
      </View>
    );
  };

  const renderInput = (label: string, field: string, maxLength?: number, customWidth?: string) => {
    const isRequired = label.includes('*');
    const labelText = label.replace('*', '').trim();
    const isCodeDisabled = isEditing && field === 'CompanyID';

    return (
      <View style={[styles.inputGroup, customWidth ? { width: customWidth as any } : {}]}>
        <ThemedText style={[styles.inputLabel, { color: textColor }]}>
          {labelText}
          {isRequired && <ThemedText style={{ color: '#EF4444', fontWeight: 'bold' }}> *</ThemedText>}
        </ThemedText>
        <TextInput
          style={[
            styles.input,
            {
              color: textColor,
              borderColor: isRequired ? (isDark ? 'rgba(212, 175, 55, 0.4)' : '#FCD34D') : borderColor,
              backgroundColor: isCodeDisabled ? (isDark ? '#262626' : '#F1F5F9') : (isDark ? '#1C1917' : '#FFFFFF'),
            },
            isCodeDisabled && { color: subtitleColor }
          ]}
          value={formData[field]}
          onChangeText={(val) => handleChange(field, val)}
          editable={!isCodeDisabled}
          maxLength={maxLength}
        />
      </View>
    );
  };

  const renderItem = ({ item }: { item: any }) => (
    <View style={[styles.row, { backgroundColor: cardColor, borderColor: borderColor }]}>
      <View style={[styles.cell, { flex: 1.5 }]}>
        <ThemedText type="tableText" style={{ color: textColor, fontWeight: '500', fontSize: 11 }}>{item.CompanyID}</ThemedText>
      </View>
      <View style={[styles.cell, { flex: 2 }]}>
        <ThemedText type="tableText" style={{ color: textColor, fontSize: 11 }}>{item.Name || '-'}</ThemedText>
      </View>
      <View style={[styles.cell, { flex: 1.5 }]}>
        <ThemedText type="tableText" style={{ color: textColor, fontSize: 11 }}>{item.City || '-'}</ThemedText>
      </View>
      <View style={[styles.cell, { flex: 1.5 }]}>
        <ThemedText type="tableText" style={{ color: textColor, fontSize: 11 }}>{item.Phone || '-'}</ThemedText>
      </View>
      <View style={styles.actionCell}>
        <TouchableOpacity style={styles.actionButton} onPress={() => openEditModal(item)} disabled={deletingId === item.CompanyID}>
          <Ionicons name="pencil-outline" size={20} color={GOLD_COLOR} />
        </TouchableOpacity>
        <TouchableOpacity style={styles.actionButton} onPress={() => handleDelete(item.CompanyID)} disabled={deletingId === item.CompanyID}>
          {deletingId === item.CompanyID ? (
            <ActivityIndicator size="small" color="#F44336" />
          ) : (
            <Ionicons name="trash-outline" size={20} color="#F44336" />
          )}
        </TouchableOpacity>
      </View>
    </View>
  );

  const renderToast = () => {
    if (!successMsg && !errorMsg) return null;
    const isError = !!errorMsg;
    return (
      <View style={[styles.toast, { backgroundColor: isError ? '#F44336' : '#4CAF50' }]}>
        <ThemedText style={styles.toastText}>{isError ? errorMsg : successMsg}</ThemedText>
      </View>
    );
  };

  return (
    <SafeAreaView style={[styles.safeArea, { backgroundColor }]} edges={['top']}>
      <Stack.Screen options={{ headerShown: false }} />
      {!isModalVisible && renderToast()}
      {/* Header */}
      <View style={[styles.header, { borderBottomColor: borderColor }]}>
        <View style={{ flexDirection: 'row', alignItems: 'center' }}>
          <TouchableOpacity style={styles.backButton} onPress={() => router.back()}>
            <Ionicons name="arrow-back" size={24} color={textColor} />
          </TouchableOpacity>
          <ThemedText style={[styles.title, { color: textColor }]}>Company Master</ThemedText>
        </View>
        <TouchableOpacity style={[styles.addButton, { backgroundColor: 'rgba(212, 175, 55, 0.15)', borderColor: GOLD_COLOR }]} onPress={openAddModal}>
          <Ionicons name="add" size={20} color={GOLD_COLOR} />
          <ThemedText style={[styles.addButtonText, { color: GOLD_COLOR }]}>New</ThemedText>
        </TouchableOpacity>
      </View>

      <View style={styles.tableContainer}>
        {/* Table Header */}
        <View style={[styles.tableHeader, { backgroundColor: 'rgba(0,0,0,0.02)' }]}>
          <View style={[styles.cell, { flex: 1.5 }]}><ThemedText style={[styles.headerText, { color: subtitleColor }]}>CODE</ThemedText></View>
          <View style={[styles.cell, { flex: 2 }]}><ThemedText style={[styles.headerText, { color: subtitleColor }]}>NAME</ThemedText></View>
          <View style={[styles.cell, { flex: 1.5 }]}><ThemedText style={[styles.headerText, { color: subtitleColor }]}>CITY</ThemedText></View>
          <View style={[styles.cell, { flex: 1.5 }]}><ThemedText style={[styles.headerText, { color: subtitleColor }]}>PHONE</ThemedText></View>
          <View style={[styles.actionCell, { width: 120 }]}>
            <ThemedText style={[styles.headerText, { color: subtitleColor, width: 55, textAlign: 'center' }]}>EDIT</ThemedText>
            <ThemedText style={[styles.headerText, { color: subtitleColor, width: 55, textAlign: 'center' }]}>DELETE</ThemedText>
          </View>
        </View>

        {loading ? (
          <View style={styles.centered}><ActivityIndicator size="large" color={GOLD_COLOR} /></View>
        ) : (
          <FlatList
            data={data}
            renderItem={renderItem}
            keyExtractor={(item, index) => item.CompanyID ? String(item.CompanyID) : index.toString()}
            contentContainerStyle={{ paddingBottom: 20 }}
            ListEmptyComponent={
              <View style={styles.centered}>
                <ThemedText style={{ color: subtitleColor }}>No companies found.</ThemedText>
              </View>
            }
          />
        )}
      </View>

      {/* Modal - Made Full Screen and Grid Layout */}
      <Modal visible={isModalVisible} transparent={false} animationType="slide">
        <SafeAreaView style={{ flex: 1, backgroundColor: isDark ? '#121212' : '#F8FAFC' }}>
          {renderToast()}
          <View style={[styles.modalHeader, { backgroundColor: isDark ? '#1F1F1F' : '#FFFFFF', borderBottomColor: borderColor }]}>
            <View style={{ flexDirection: 'row', alignItems: 'center' }}>
              <TouchableOpacity style={styles.backButton} onPress={() => setIsModalVisible(false)}>
                <Ionicons name="arrow-back" size={24} color={textColor} />
              </TouchableOpacity>
              <View style={{ flexDirection: 'row', alignItems: 'center', gap: 8 }}>
                <View style={[styles.iconBadge, { backgroundColor: GOLD_COLOR }]}>
                  <Ionicons name="business" size={14} color="#FFFFFF" />
                </View>
                <ThemedText style={{ fontSize: 16, fontWeight: 'bold', color: textColor }}>
                  {isEditing ? 'Edit Company' : 'Add New Company'}
                </ThemedText>
              </View>
            </View>
            <View style={{ flexDirection: 'row', alignItems: 'center', gap: 8 }}>
              <TouchableOpacity
                style={[
                  styles.button,
                  styles.saveButton,
                  {
                    backgroundColor: GOLD_COLOR,
                    paddingVertical: 6,
                    paddingHorizontal: 14,
                    height: 32,
                    flexDirection: 'row',
                    alignItems: 'center',
                    gap: 6,
                    borderRadius: 8,
                    shadowColor: GOLD_COLOR,
                    shadowOffset: { width: 0, height: 2 },
                    shadowOpacity: 0.25,
                    shadowRadius: 3,
                    elevation: 3,
                  }
                ]}
                onPress={handleSave}
                disabled={saving}
                activeOpacity={0.8}
              >
                {saving ? (
                  <ActivityIndicator color="#000" size="small" />
                ) : (
                  <>
                    <Ionicons name="checkmark-done" size={15} color="#000" />
                    <ThemedText style={{ color: '#000', fontWeight: 'bold', fontSize: 12 }}>Save Details</ThemedText>
                  </>
                )}
              </TouchableOpacity>
            </View>
          </View>

          <ScrollView style={styles.modalBody} contentContainerStyle={{ paddingBottom: 40 }}>
            <View style={styles.gridContainer}>
              {/* General Section */}
              <View style={[styles.sectionBlock, { borderColor, backgroundColor: cardColor }]}>
                <View style={[styles.sectionHeader, { backgroundColor: isDark ? 'rgba(212, 175, 55, 0.12)' : '#FDF8ED', borderBottomColor: borderColor }]}>
                  <View style={{ flexDirection: 'row', alignItems: 'center', gap: 6 }}>
                    <Ionicons name="business-outline" size={15} color={GOLD_COLOR} />
                    <ThemedText style={[styles.sectionTitle, { color: textColor }]}>General Details</ThemedText>
                  </View>
                </View>
                <View style={[styles.grid, { padding: 12 }]}>
                  {renderInput('Company Code *', 'CompanyID', 15)}
                  {renderInput('Company Name *', 'Name', 50)}
                  {renderInput('Address 1', 'Address1', 50)}
                  {renderInput('Address 2', 'Address2', 50)}
                  {renderInput('City', 'City', 50)}
                  {renderInput('State', 'State', 50)}
                  {renderInput('Country', 'Country', 50)}
                  {renderInput('Postal Code', 'PostalCode', 10)}
                  {renderInput('Phone', 'Phone', 35)}
                  {renderInput('Fax', 'Fax', 15)}
                  {renderInput('E-mail', 'Email', 50)}
                  {renderInput('URL', 'Url', 50)}
                </View>
              </View>

              <View style={[styles.sectionBlock, { borderColor, backgroundColor: cardColor, marginTop: 14 }]}>
                <View style={[styles.sectionHeader, { backgroundColor: isDark ? 'rgba(59, 130, 246, 0.12)' : '#EFF6FF', borderBottomColor: borderColor }]}>
                  <View style={{ flexDirection: 'row', alignItems: 'center', gap: 6 }}>
                    <Ionicons name="options-outline" size={15} color="#3B82F6" />
                    <ThemedText style={[styles.sectionTitle, { color: textColor }]}>Formatting & Defaults</ThemedText>
                  </View>
                </View>
                <View style={[styles.grid, { padding: 12 }]}>
                  {renderInput('Date Format', 'DateFormat', 25)}
                  {renderInput('Time Format', 'TimeFormat', 25)}
                  {renderInput('Currency Sign', 'CurrencySign', 3)}
                  {renderInput('Default Currency', 'DefaultCurrency', 3)}
                  {renderInput('Business Reg. No', 'GSTRegNo', 25)}
                  {renderInput('GST Percentage', 'GstPerc', 9)}
                  {renderInput('Bill Message 1', 'Message1', 50)}
                  {renderInput('Bill Message 2', 'Message2', 50)}
                  {renderInput('Display Message 1', 'DisplayMessage1', 20)}
                  {renderInput('Display Message 2', 'DisplayMessage2', 20)}
                </View>
              </View>

              <View style={{ flexDirection: 'row', gap: 14, marginTop: 14 }}>
                <View style={[styles.sectionBlock, { borderColor, backgroundColor: cardColor, flex: 1 }]}>
                  <View style={[styles.sectionHeader, { backgroundColor: isDark ? 'rgba(16, 185, 129, 0.12)' : '#ECFDF5', borderBottomColor: borderColor }]}>
                    <View style={{ flexDirection: 'row', alignItems: 'center', gap: 6 }}>
                      <Ionicons name="cart-outline" size={15} color="#10B981" />
                      <ThemedText style={[styles.sectionTitle, { color: textColor }]}>Sales Defaults</ThemedText>
                    </View>
                  </View>
                  <View style={[styles.grid, { padding: 12 }]}>
                    {renderInput('Next Invoice', 'NextInvoice', 9, '50%')}
                    {renderInput('Next Receipt No', 'NextReciptNo', 9, '50%')}
                    {renderInput('Max Discount', 'MaxDiscountAllowed', 9, '50%')}
                    {renderInput('Max Price Change', 'MaxPriceChangeAllowed', 9, '50%')}
                    {renderInput('Next PO No', 'NextPONo', 9, '50%')}
                    {renderInput('Tax IN', 'TaxIN', 1, '50%')}
                    {renderInput('Next Avg Cost %', 'NextAverageCostPercentage', 9, '50%')}
                    {renderInput('Expired Time', 'ExpiredTime', 9, '50%')}
                    {renderInput('Next Credit No', 'NextCreditInvoice', 9, '50%')}
                    {renderCheckbox('Tax Taken', 'TaxTaken', '50%')}
                    {renderInput('Trade in Sales No', 'NextTradeInSalesNo', 9, '50%')}
                  </View>
                </View>

                <View style={[styles.sectionBlock, { borderColor, backgroundColor: cardColor, flex: 1, height: '100%' }]}>
                  <View style={[styles.sectionHeader, { backgroundColor: isDark ? 'rgba(168, 85, 247, 0.12)' : '#FAF5FF', borderBottomColor: borderColor }]}>
                    <View style={{ flexDirection: 'row', alignItems: 'center', gap: 6 }}>
                      <Ionicons name="bag-check-outline" size={15} color="#8B5CF6" />
                      <ThemedText style={[styles.sectionTitle, { color: textColor }]}>Purchase Defaults</ThemedText>
                    </View>
                  </View>
                  <View style={[styles.grid, { padding: 12 }]}>
                    {renderInput('Next Return No', 'NextPurReNo', 9, '50%')}
                    {renderInput('Next Payment No', 'NextPaymentNo', 9, '50%')}
                  </View>
                </View>
              </View>
            </View>
          </ScrollView>

        </SafeAreaView>
      </Modal>
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  safeArea: {
    flex: 1,
  },
  header: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingHorizontal: 16,
    paddingVertical: 8,
    borderBottomWidth: 1,
  },
  backButton: {
    marginRight: 15,
  },
  title: {
    fontSize: 18,
    fontWeight: 'bold',
  },
  addButton: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 16,
    paddingVertical: 8,
    borderRadius: 12,
    borderWidth: 1,
  },
  addButtonText: {
    marginLeft: 5,
    fontWeight: '600',
    fontSize: 13,
  },
  tableContainer: {
    flex: 1,
    margin: 15,
    maxWidth: 1100,
    width: '100%',
    alignSelf: 'center',
    padding: 10,
  },
  tableHeader: {
    flexDirection: 'row',
    paddingVertical: 10,
    paddingHorizontal: 20,
    marginBottom: 8,
    borderRadius: 12,
  },
  headerText: {
    fontSize: 10,
    fontWeight: 'bold',
    letterSpacing: 1,
  },
  row: {
    flexDirection: 'row',
    paddingVertical: 10,
    paddingHorizontal: 20,
    marginBottom: 8,
    borderRadius: 12,
    alignItems: 'center',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.05,
    shadowRadius: 6,
    elevation: 2,
    borderWidth: 1,
  },
  cell: {
    justifyContent: 'center',
  },
  actionCell: {
    flexDirection: 'row',
    justifyContent: 'center',
    width: 120,
  },
  actionButton: {
    padding: 6,
    marginHorizontal: 4,
    borderRadius: 8,
    backgroundColor: 'rgba(0,0,0,0.03)',
    alignItems: 'center',
    justifyContent: 'center',
  },
  centered: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    padding: 20,
  },

  // Modal Full Screen Styles
  modalHeader: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    paddingHorizontal: 16,
    paddingVertical: 12,
    borderBottomWidth: 1,
  },
  modalBody: {
    padding: 16,
  },
  gridContainer: {
    width: '100%',
    maxWidth: 1200,
    alignSelf: 'center',
  },
  iconBadge: {
    width: 24,
    height: 24,
    borderRadius: 6,
    justifyContent: 'center',
    alignItems: 'center',
  },
  sectionBlock: {
    borderWidth: 1,
    borderRadius: 8,
    overflow: 'hidden',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.05,
    shadowRadius: 3,
    elevation: 2,
  },
  sectionHeader: {
    paddingVertical: 8,
    paddingHorizontal: 14,
    borderBottomWidth: 1,
  },
  sectionTitle: {
    fontSize: 12,
    fontWeight: 'bold',
  },
  grid: {
    flexDirection: 'row',
    flexWrap: 'wrap',
  },
  inputGroup: {
    width: Platform.OS === 'web' ? '33.33%' : '50%',
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 8,
    marginBottom: 6,
  },
  inputLabel: {
    width: 100,
    fontSize: 11,
    fontWeight: 'bold',
  },
  label: {
    fontSize: 11,
    fontWeight: 'bold',
    marginBottom: 4,
  },
  input: {
    flex: 1,
    borderWidth: 1,
    borderRadius: 2,
    paddingHorizontal: 6,
    paddingVertical: 2,
    fontSize: 12,
    height: 26,
  },
  modalFooter: {
    flexDirection: 'row',
    justifyContent: 'flex-end',
    padding: 16,
    borderTopWidth: 1,
    gap: 12,
  },
  button: {
    paddingVertical: 10,
    paddingHorizontal: 20,
    borderRadius: 8,
    alignItems: 'center',
    justifyContent: 'center',
  },
  cancelButton: {
    borderWidth: 1,
    backgroundColor: 'transparent',
  },
  saveButton: {
    backgroundColor: '#D4AF37',
    paddingVertical: 12,
  },
  toast: {
    position: 'absolute',
    top: Platform.OS === 'web' ? 20 : 50,
    alignSelf: 'center',
    minWidth: 250,
    maxWidth: '90%',
    padding: 15,
    borderRadius: 8,
    zIndex: 9999,
    alignItems: 'center',
    justifyContent: 'center',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.25,
    shadowRadius: 3.84,
    elevation: 5,
  },
  toastText: {
    color: '#FFF',
    fontSize: 14,
    fontWeight: '600',
    textAlign: 'center',
  },
});
