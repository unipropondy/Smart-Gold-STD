import { Ionicons } from '@expo/vector-icons';
import { Stack, useRouter } from 'expo-router';
import React, { useEffect, useState } from 'react';
import {
  ActivityIndicator,
  Alert,
  FlatList,
  Modal,
  StyleSheet,
  TextInput,
  TouchableOpacity,
  TouchableWithoutFeedback,
  useColorScheme,
  View,
  Platform,
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';
import { ThemedText } from '@/components/themed-text';
import { useThemeColors } from '@/hooks/useThemeColors';
import { useThemeContext } from '@/contexts/ThemeContext';
import { API_BASE_URL } from '@/constants/api';

const GOLD_COLOR = '#D4AF37';
const API_URL = `${API_BASE_URL}/salesman`;

export default function SalesmanScreen() {
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
  const [deleteConfirmCode, setDeleteConfirmCode] = useState<string | null>(null);

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
  const [salesManCode, setSalesManCode] = useState('');
  const [salesManName, setSalesManName] = useState('');
  const [password, setPassword] = useState('');
  const [salesManGroup, setSalesManGroup] = useState('');
  const [remarks, setRemarks] = useState('');
  const [sortOrder, setSortOrder] = useState('0');
  const [showInReport, setShowInReport] = useState(false);
  const [allowSalesReturn, setAllowSalesReturn] = useState(false);

  const [groups, setGroups] = useState<string[]>([]);
  const [isGroupLookupVisible, setIsGroupLookupVisible] = useState(false);

  const headerBgColor = isDark ? '#2A2A2A' : '#F5F5F5';

  const fetchSalesmen = async () => {
    try {
      const response = await fetch(`${API_URL}?t=${Date.now()}`, {
        headers: { 'Cache-Control': 'no-cache' }
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

  const fetchGroups = async () => {
    try {
      const response = await fetch(`${API_URL}/groups?t=${Date.now()}`);
      const json = await response.json();
      if (json.success) {
        setGroups(json.data);
      }
    } catch (error) {
      console.error('Failed to fetch groups:', error);
    }
  };

  useEffect(() => {
    fetchSalesmen();
    fetchGroups();
  }, []);

  const handleSalesManCodeBlur = () => {
    const trimmed = (salesManCode || '').trim();
    if (!trimmed) {
      showError('SaleManCode should not be blank.');
    }
  };

  const handleSalesManNameBlur = () => {
    const trimmed = (salesManName || '').trim();
    if (!trimmed) {
      showError('SalesManName should not be blank.');
    }
  };

  const handleSalesManGroupBlur = () => {
    const trimmed = (salesManGroup || '').trim();
    if (!trimmed) {
      showError('SalesManGroup should not be blank.');
    }
  };

  const handleSortOrderBlur = () => {
    const n = parseInt(sortOrder) || 0;
    if (n === 0) {
      showError('Sort Order Can Not Be Zero.');
    }
  };

  const handleSave = async () => {
    // Validation matching ValidateMe() in frmSalesman.vb
    const trimmedCode = (salesManCode || '').trim();
    const trimmedName = (salesManName || '').trim();
    const trimmedGroup = (salesManGroup || '').trim();
    const nSortOrder = parseInt(sortOrder) || 0;

    if (!trimmedCode) {
      showError('SaleManCode should not be blank.');
      return;
    }

    if (!trimmedName) {
      showError('SalesManName should not be blank.');
      return;
    }

    if (!trimmedGroup) {
      showError('SalesManGroup should not be blank.');
      return;
    }

    if (nSortOrder === 0) {
      showError('Sort Order Can Not Be Zero.');
      return;
    }

    // Check sort order conflict locally before saving
    const existingConflict = data.find((s: any) =>
      (s.SalesManCode || '').trim().toUpperCase() !== trimmedCode.toUpperCase() &&
      (parseInt(s.SortOrder) || 0) === nSortOrder
    );
    if (existingConflict) {
      showError('Sort Order Already Exists.');
      return;
    }

    const payload = {
      SalesManCode: trimmedCode.toUpperCase(),
      SalesManName: trimmedName,
      Password: password.trim(),
      SalesManGroup: trimmedGroup,
      Remarks: remarks.trim(),
      SortOrder: nSortOrder,
      ShowInReport: showInReport,
      AllowSalesReturn: allowSalesReturn,
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
        showSuccess(json.message || 'SalesMan detail saved sucssfully.');
        setIsModalVisible(false);
        fetchSalesmen();
      } else {
        showError(json.message || 'Error in Save');
      }
    } catch (error) {
      console.error('Save error:', error);
      showError('Error in Save');
    } finally {
      setSaving(false);
    }
  };

  const executeDelete = async (code: string) => {
    setDeletingId(code);
    try {
      const response = await fetch(`${API_URL}?id=${encodeURIComponent(code)}`, { method: 'DELETE' });
      const json = await response.json();
      if (json.success) {
        showSuccess(json.message || 'SalesMan detail deleted.');
        fetchSalesmen();
      } else {
        showError(json.message || 'Error in deleting Salesman detail.');
      }
    } catch (error) {
      console.error('Delete error:', error);
      showError('Error in deleting Salesman detail.');
    } finally {
      setDeletingId(null);
    }
  };

  const handleDelete = (code: string) => {
    setDeleteConfirmCode(code);
  };

  const openAddModal = () => {
    setIsEditing(false);
    setSalesManCode('');
    setSalesManName('');
    setPassword('');
    setSalesManGroup('');
    setRemarks('');
    setSortOrder('0');
    setShowInReport(false);
    setAllowSalesReturn(false);
    setIsModalVisible(true);
  };

  const openEditModal = (item: any) => {
    setIsEditing(true);
    setSalesManCode(item.SalesManCode);
    setSalesManName(item.SalesManName || '');
    setPassword(item.Password || '');
    setSalesManGroup(item.SalesManGroup || '');
    setRemarks(item.Remarks || '');
    setSortOrder(item.SortOrder?.toString() || '0');
    setShowInReport(item.ShowInReport === 1 || item.ShowInReport === true);
    setAllowSalesReturn(item.AllowSalesReturn === 1 || item.AllowSalesReturn === true);
    setIsModalVisible(true);
  };

  const renderItem = ({ item }: { item: any }) => (
    <View style={[styles.row, { backgroundColor: cardColor, borderColor: borderColor }]}>
      <View style={[styles.cell, { flex: 1.5 }]}>
        <ThemedText type="tableText" style={{ color: textColor, fontWeight: '500', fontSize: 11 }}>{item.SalesManCode}</ThemedText>
      </View>
      <View style={[styles.cell, { flex: 2 }]}>
        <ThemedText type="tableText" style={{ color: textColor, fontSize: 11 }}>{item.SalesManName}</ThemedText>
      </View>
      <View style={[styles.cell, { flex: 1.5 }]}>
        <ThemedText type="tableText" style={{ color: textColor, fontSize: 11 }}>{item.SalesManGroup}</ThemedText>
      </View>
      <View style={[styles.cell, { flex: 1, alignItems: 'center' }]}>
        <ThemedText type="tableText" style={{ color: textColor, fontSize: 11 }}>{item.SortOrder ?? 0}</ThemedText>
      </View>
      <View style={[styles.actionCell, { alignItems: 'center', justifyContent: 'center' }]}>
        <TouchableOpacity style={styles.actionButton} onPress={() => openEditModal(item)} disabled={deletingId === item.SalesManCode}>
          <Ionicons name="pencil-outline" size={20} color={GOLD_COLOR} />
        </TouchableOpacity>
        <TouchableOpacity style={styles.actionButton} onPress={() => handleDelete(item.SalesManCode)} disabled={deletingId === item.SalesManCode}>
          {deletingId === item.SalesManCode ? (
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
      <View
        pointerEvents="none"
        style={[styles.toast, isError ? styles.toastError : styles.toastSuccess]}
      >
        <Ionicons
          name={isError ? 'alert-circle' : 'checkmark-circle'}
          size={16}
          color="#FFFFFF"
          style={{ marginRight: 6 }}
        />
        <ThemedText style={styles.toastText}>{isError ? errorMsg : successMsg}</ThemedText>
      </View>
    );
  };

  return (
    <SafeAreaView style={[styles.safeArea, { backgroundColor }]} edges={['top']}>
      <Stack.Screen options={{ headerShown: false }} />
      {!(isModalVisible || isGroupLookupVisible || deleteConfirmCode) && renderToast()}
      {/* Header */}
      <View style={[styles.header, { borderBottomColor: borderColor }]}>
        <View style={{ flexDirection: 'row', alignItems: 'center' }}>
          <TouchableOpacity style={styles.backButton} onPress={() => router.back()}>
            <Ionicons name="arrow-back" size={24} color={textColor} />
          </TouchableOpacity>
          <ThemedText style={[styles.title, { color: textColor }]}>Salesman Master</ThemedText>
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
        <View style={[styles.cell, { flex: 1.5 }]}><ThemedText style={[styles.headerText, { color: subtitleColor }]}>GROUP</ThemedText></View>
        <View style={[styles.cell, { flex: 1, alignItems: 'center' }]}><ThemedText style={[styles.headerText, { color: subtitleColor }]}>SORT</ThemedText></View>
        <View style={[styles.actionCell, { width: 120 }]}><ThemedText style={[styles.headerText, { color: subtitleColor, width: 55, textAlign: 'center' }]}>EDIT</ThemedText><ThemedText style={[styles.headerText, { color: subtitleColor, width: 55, textAlign: 'center' }]}>DELETE</ThemedText></View>
      </View>

      {loading ? (
        <View style={styles.centered}><ActivityIndicator size="large" color={GOLD_COLOR} /></View>
      ) : (
        <FlatList
          data={data}
          renderItem={renderItem}
          keyExtractor={(item, index) => item.SalesManCode || index.toString()}
          contentContainerStyle={{ paddingBottom: 20 }}
          ListEmptyComponent={
            <View style={styles.centered}>
              <ThemedText style={{ color: subtitleColor }}>No salesmen found.</ThemedText>
            </View>
          }
        />
      )}
      </View>

      {/* Modal */}
      <Modal visible={isModalVisible} transparent animationType="fade">
        <TouchableOpacity style={styles.modalOverlay} activeOpacity={1} onPress={() => setIsModalVisible(false)}>
          <TouchableWithoutFeedback>
            <View style={[styles.modalContent, { backgroundColor: cardColor, borderColor }]}>
              {renderToast()}
              <View style={[styles.modalHeader, { borderBottomColor: borderColor }]}>
                <ThemedText style={{ fontSize: 18, fontWeight: 'bold', color: textColor }}>
                  {isEditing ? 'Edit Salesman' : 'Add New Salesman'}
                </ThemedText>
                <TouchableOpacity onPress={() => setIsModalVisible(false)}>
                  <Ionicons name="close" size={24} color={textColor} />
                </TouchableOpacity>
              </View>

              <FlatList 
                data={[{}]} 
                keyExtractor={() => "form"}
                renderItem={() => (
                <View style={styles.modalBody}>
                  <View style={styles.inputGroup}>
                    <ThemedText style={[styles.label, { color: subtitleColor }]}>SalesMan Code *</ThemedText>
                    <TextInput
                      style={[styles.input, { color: textColor, borderColor, backgroundColor: isDark ? '#2A2A2A' : '#FFF' }, isEditing && { backgroundColor: isDark ? '#333' : '#F0F0F0', color: subtitleColor }]}
                      value={salesManCode}
                      onChangeText={(t) => setSalesManCode(t.toUpperCase())}
                      onBlur={handleSalesManCodeBlur}
                      editable={!isEditing}
                      autoCapitalize="characters"
                      placeholder="Enter Salesman Code"
                      placeholderTextColor={subtitleColor}
                    />
                  </View>

                  <View style={styles.inputGroup}>
                    <ThemedText style={[styles.label, { color: subtitleColor }]}>SalesMan Name *</ThemedText>
                    <TextInput
                      style={[styles.input, { color: textColor, borderColor, backgroundColor: isDark ? '#2A2A2A' : '#FFF' }]}
                      value={salesManName}
                      onChangeText={setSalesManName}
                      onBlur={handleSalesManNameBlur}
                      placeholder="Enter Salesman Name"
                      placeholderTextColor={subtitleColor}
                    />
                  </View>

                  <View style={styles.inputGroup}>
                    <ThemedText style={[styles.label, { color: subtitleColor }]}>Password</ThemedText>
                    <TextInput
                      style={[styles.input, { color: textColor, borderColor, backgroundColor: isDark ? '#2A2A2A' : '#FFF' }]}
                      value={password}
                      onChangeText={setPassword}
                      secureTextEntry
                      placeholder="Enter Password"
                      placeholderTextColor={subtitleColor}
                    />
                  </View>

                  <View style={styles.inputGroup}>
                    <ThemedText style={[styles.label, { color: subtitleColor }]}>SalesMan Group *</ThemedText>
                    <View style={styles.lookupContainer}>
                      <TextInput
                        style={[styles.input, { color: textColor, borderColor, backgroundColor: isDark ? '#2A2A2A' : '#FFF' }]}
                        value={salesManGroup}
                        onChangeText={setSalesManGroup}
                        onBlur={handleSalesManGroupBlur}
                        placeholder="Enter Group (e.g. ADMIN)"
                        placeholderTextColor={subtitleColor}
                      />
                      <TouchableOpacity style={styles.lookupButton} onPress={() => setIsGroupLookupVisible(true)}>
                        <ThemedText style={{ color: textColor, fontWeight: 'bold' }}>▼</ThemedText>
                      </TouchableOpacity>
                    </View>
                  </View>

                  <View style={styles.inputGroup}>
                    <ThemedText style={[styles.label, { color: subtitleColor }]}>Remarks</ThemedText>
                    <TextInput
                      style={[styles.input, { color: textColor, borderColor, backgroundColor: isDark ? '#2A2A2A' : '#FFF' }]}
                      value={remarks}
                      onChangeText={setRemarks}
                      placeholder="Enter Remarks"
                      placeholderTextColor={subtitleColor}
                    />
                  </View>
                  
                  <View style={styles.inputGroup}>
                    <ThemedText style={[styles.label, { color: subtitleColor }]}>Sort Order *</ThemedText>
                    <TextInput
                      style={[styles.input, { color: textColor, borderColor, backgroundColor: isDark ? '#2A2A2A' : '#FFF' }]}
                      value={sortOrder}
                      onChangeText={setSortOrder}
                      onBlur={handleSortOrderBlur}
                      keyboardType="numeric"
                      placeholder="Sort Order"
                      placeholderTextColor={subtitleColor}
                    />
                  </View>

                  {/* Show In Report Checkbox */}
                  <View style={styles.checkboxContainer}>
                    <ThemedText style={[styles.checkboxLabel, { color: subtitleColor }]}>Show In Report</ThemedText>
                    <View style={styles.checkboxControl}>
                      <TouchableOpacity
                        style={{ flexDirection: 'row', alignItems: 'center' }}
                        onPress={() => setShowInReport(!showInReport)}
                      >
                        <View style={[styles.checkbox, { borderColor, backgroundColor: showInReport ? GOLD_COLOR : 'transparent' }]}>
                          {showInReport && <Ionicons name="checkmark" size={16} color="#FFF" />}
                        </View>
                      </TouchableOpacity>
                    </View>
                  </View>

                  {/* Allow SalesReturn Checkbox */}
                  <View style={styles.checkboxContainer}>
                    <ThemedText style={[styles.checkboxLabel, { color: subtitleColor }]}>Allow SalesReturn / Bill Cancel</ThemedText>
                    <View style={styles.checkboxControl}>
                      <TouchableOpacity
                        style={{ flexDirection: 'row', alignItems: 'center' }}
                        onPress={() => setAllowSalesReturn(!allowSalesReturn)}
                      >
                        <View style={[styles.checkbox, { borderColor, backgroundColor: allowSalesReturn ? GOLD_COLOR : 'transparent' }]}>
                          {allowSalesReturn && <Ionicons name="checkmark" size={16} color="#FFF" />}
                        </View>
                      </TouchableOpacity>
                    </View>
                  </View>

                </View>
                )} />

              <View style={[styles.modalFooter, { borderTopColor: borderColor }]}>
                <TouchableOpacity style={[styles.btn, styles.btnCancel]} onPress={() => setIsModalVisible(false)}>
                  <ThemedText style={{ color: '#666', fontWeight: '600' }}>Cancel</ThemedText>
                </TouchableOpacity>
                <TouchableOpacity style={[styles.btn, styles.btnSave, saving && { opacity: 0.7 }]} onPress={handleSave} disabled={saving}>
                  {saving ? <ActivityIndicator size="small" color="#FFF" /> : <ThemedText style={{ color: textColor, fontWeight: '600' }}>Save</ThemedText>}
                </TouchableOpacity>
              </View>
            </View>
          </TouchableWithoutFeedback>
        </TouchableOpacity>
      </Modal>

      {/* Group Lookup Modal */}
      <Modal visible={isGroupLookupVisible} transparent animationType="slide">
        <View style={styles.modalOverlay}>
          <View style={[styles.modalContent, { backgroundColor: cardColor, borderColor, height: '50%' }]}>
            <View style={[styles.modalHeader, { borderBottomColor: borderColor }]}>
              <ThemedText style={{ fontSize: 18, fontWeight: 'bold', color: textColor }}>
                Select Salesman Group
              </ThemedText>
              <TouchableOpacity onPress={() => setIsGroupLookupVisible(false)}>
                <Ionicons name="close" size={24} color={textColor} />
              </TouchableOpacity>
            </View>
            <FlatList
              data={groups}
              keyExtractor={(item, index) => item + index.toString()}
              renderItem={({ item }) => (
                <TouchableOpacity 
                  style={{ padding: 16, borderBottomWidth: 1, borderBottomColor: borderColor }}
                  onPress={() => {
                    setSalesManGroup(item);
                    setIsGroupLookupVisible(false);
                  }}
                >
                  <ThemedText style={{ color: textColor }}>{item}</ThemedText>
                </TouchableOpacity>
              )}
              ListEmptyComponent={
                <View style={[styles.centered, { marginTop: 20 }]}>
                  <ThemedText style={{ color: subtitleColor }}>No groups found.</ThemedText>
                </View>
              }
            />
          </View>
        </View>
      </Modal>

      {/* Delete Confirmation Modal */}
      <Modal visible={!!deleteConfirmCode} animationType="fade" transparent={true}>
        <TouchableWithoutFeedback onPress={() => setDeleteConfirmCode(null)}>
          <View style={styles.modalOverlay}>
            <TouchableWithoutFeedback>
              <View style={[styles.modalContent, { backgroundColor: cardColor, borderColor, maxWidth: 420, padding: 24 }]}>
                <View style={{ flexDirection: 'row', alignItems: 'center', marginBottom: 16 }}>
                  <Ionicons name="trash-outline" size={24} color="#C62828" style={{ marginRight: 10 }} />
                  <ThemedText style={{ fontSize: 18, fontWeight: 'bold', color: textColor }}>Delete Salesman</ThemedText>
                </View>
                <ThemedText style={{ color: textColor, fontSize: 14, marginBottom: 24, lineHeight: 22 }}>
                  {`Do you want to delete `}
                  <ThemedText style={{ fontWeight: 'bold' }}>{deleteConfirmCode || ''}</ThemedText>
                  {` SalesMan?`}
                </ThemedText>
                <View style={{ flexDirection: 'row', justifyContent: 'flex-end', gap: 12 }}>
                  <TouchableOpacity
                    style={[styles.btn, styles.btnCancel]}
                    onPress={() => setDeleteConfirmCode(null)}
                  >
                    <ThemedText style={{ color: '#666', fontWeight: '600' }}>Cancel</ThemedText>
                  </TouchableOpacity>
                  <TouchableOpacity
                    style={[styles.btn, { backgroundColor: '#C62828' }]}
                    onPress={() => {
                      const code = deleteConfirmCode;
                      setDeleteConfirmCode(null);
                      if (code) executeDelete(code);
                    }}
                  >
                    <ThemedText style={{ color: '#FFF', fontWeight: '600' }}>Delete</ThemedText>
                  </TouchableOpacity>
                </View>
              </View>
            </TouchableWithoutFeedback>
          </View>
        </TouchableWithoutFeedback>
        {renderToast()}
      </Modal>

    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  safeArea: { flex: 1 },
  header: { flexDirection: 'row', alignItems: 'center', justifyContent: 'space-between', paddingHorizontal: 16, paddingVertical: 8, borderBottomWidth: 1 },
  backButton: { padding: 4, marginRight: 12 },
  title: { fontSize: 20, fontWeight: '300', letterSpacing: 1.5 },
  addButton: { flexDirection: 'row', alignItems: 'center', paddingHorizontal: 12, paddingVertical: 6, borderRadius: 8, borderWidth: 1 },
  addButtonText: { marginLeft: 4, fontWeight: '600', fontSize: 14 },
  tableContainer: { flex: 1, margin: 15, maxWidth: 1100, width: '100%', alignSelf: 'center', padding: 10 },
  tableHeader: { flexDirection: 'row', paddingVertical: 10, paddingHorizontal: 20, marginBottom: 8, borderRadius: 12 },
  headerText: { fontSize: 10, fontWeight: 'bold', letterSpacing: 1 },
  row: { flexDirection: 'row', paddingVertical: 10, paddingHorizontal: 20, marginBottom: 8, borderRadius: 12, alignItems: 'center', shadowColor: '#000', shadowOffset: { width: 0, height: 2 }, shadowOpacity: 0.05, shadowRadius: 6, elevation: 2, borderWidth: 1 },
  cell: { justifyContent: 'center' },
  actionCell: { flexDirection: 'row', justifyContent: 'center', width: 120 },
  actionButton: { padding: 6, marginHorizontal: 4, borderRadius: 8, backgroundColor: 'rgba(0,0,0,0.03)', alignItems: 'center', justifyContent: 'center' },
  centered: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    marginTop: 40,
  },
  modalOverlay: {
    flex: 1,
    backgroundColor: 'rgba(0,0,0,0.5)',
    justifyContent: 'center',
    alignItems: 'center',
    padding: 20,
  },
  modalContent: {
    width: '100%',
    maxWidth: 500,
    maxHeight: '90%',
    borderWidth: 1,
    borderRadius: 12,
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.3,
    shadowRadius: 8,
    elevation: 10,
  },
  modalHeader: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    padding: 20,
    borderBottomWidth: 1,
  },
  modalBody: { padding: 20 },
  inputGroup: { 
    flexDirection: 'row', 
    alignItems: 'center',
    marginBottom: 16 
  },
  label: {
    flex: 0.4,
    fontSize: 14,
    fontWeight: '600',
    marginRight: 12,
  },
  input: {
    flex: 1,
    borderWidth: 1,
    borderRadius: 6,
    paddingHorizontal: 12,
    paddingVertical: 10,
    fontSize: 14,
  },
  lookupContainer: {
    flex: 1,
    flexDirection: 'row',
    gap: 8,
  },
  lookupButton: {
    backgroundColor: '#D4AF37',
    paddingHorizontal: 16,
    justifyContent: 'center',
    alignItems: 'center',
    borderRadius: 6,
  },
  checkbox: {
    width: 20,
    height: 20,
    borderWidth: 1,
    borderRadius: 4,
    justifyContent: 'center',
    alignItems: 'center',
  },
  checkboxContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    marginBottom: 16,
  },
  checkboxLabel: {
    flex: 0.4,
    fontSize: 14,
    fontWeight: '600',
    marginRight: 12,
  },
  checkboxControl: {
    flex: 1,
    flexDirection: 'row',
    alignItems: 'center',
  },
  modalFooter: {
    flexDirection: 'row',
    justifyContent: 'flex-end',
    gap: 12,
    padding: 20,
    borderTopWidth: 1,
  },
  btn: {
    paddingHorizontal: 20,
    paddingVertical: 10,
    borderRadius: 6,
    minWidth: 100,
    alignItems: 'center',
  },
  btnCancel: { 
    backgroundColor: 'transparent',
    borderWidth: 1,
    borderColor: '#CCC',
  },
  btnSave: { backgroundColor: '#D4AF37' },
  btnSaveText: {  fontWeight: 'bold' },
  toast: {
    position: 'absolute',
    top: Platform.OS === 'web' ? 20 : 50,
    alignSelf: 'center',
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 16,
    paddingVertical: 10,
    borderRadius: 8,
    zIndex: 999999,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.25,
    shadowRadius: 3.84,
    elevation: 20,
  },
  toastSuccess: {
    backgroundColor: '#2E7D32',
  },
  toastError: {
    backgroundColor: '#C62828',
  },
  toastText: {
    color: '#FFFFFF',
    fontSize: 13,
    fontWeight: '600',
    textAlign: 'center',
  },
});
