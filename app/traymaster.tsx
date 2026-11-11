import { Ionicons } from '@expo/vector-icons';
import { Stack, useRouter } from 'expo-router';
import React, { useEffect, useState } from 'react';
import {
  ActivityIndicator,
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
const API_URL = `${API_BASE_URL}/traymaster`;

export default function TrayMasterScreen() {
  const themeColors = useThemeColors();
  const { accentColor } = useThemeContext();
  const { background: backgroundColor, card: cardColor, text: textColor, border: borderColor, subtitle: subtitleColor, primaryGold: GOLD_COLOR } = themeColors;

  
  
  
  const colorScheme = useColorScheme();
  const isDark = colorScheme === 'dark';
  const router = useRouter();

  const [data, setData] = useState<any[]>([]);
  const [loading, setLoading] = useState(true);
  const [saving, setSaving] = useState(false);
  const [deletingId, setDeletingId] = useState<string | null>(null);
  const [toast, setToast] = useState<{ message: string; type: 'success' | 'error' } | null>(null);
  const [deleteConfirmId, setDeleteConfirmId] = useState<string | null>(null);

  const showToast = (message: string, type: 'success' | 'error' = 'success') => {
    setToast({ message, type });
    setTimeout(() => setToast(null), 3500);
  };

  const showSuccess = (msg: string) => {
    showToast(msg, 'success');
  };

  const showError = (msg: string) => {
    showToast(msg, 'error');
  };

  // Modal State
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [isEditing, setIsEditing] = useState(false);

  // Form State
  const [trayID, setTrayID] = useState('');
  const [trayName, setTrayName] = useState('');
  const [trayGroup, setTrayGroup] = useState('');
  const [salesManID, setSalesManID] = useState('');
  const [salesManName, setSalesManName] = useState('');
  const [isCustomerTray, setIsCustomerTray] = useState(false);

  const [trayGroups, setTrayGroups] = useState<any[]>([]);
  const [salesmen, setSalesmen] = useState<any[]>([]);
  const [isGroupLookupVisible, setIsGroupLookupVisible] = useState(false);
  const [isSalesmanLookupVisible, setIsSalesmanLookupVisible] = useState(false);

  const headerBgColor = isDark ? '#2A2A2A' : '#F5F5F5';

  const fetchTrays = async () => {
    try {
      const response = await fetch(`${API_URL}?t=${Date.now()}`);
      const json = await response.json();
      if (json.success) setData(json.data);
    } catch (error) {
      console.error('Network Error:', error);
    } finally {
      setLoading(false);
    }
  };

  const fetchTrayGroups = async () => {
    try {
      const response = await fetch(`${API_BASE_URL}/traygroup?t=${Date.now()}`);
      const json = await response.json();
      if (json.success) setTrayGroups(json.data);
    } catch (error) {
      console.error('Fetch Tray Groups Error:', error);
    }
  };

  const fetchSalesmen = async () => {
    try {
      const response = await fetch(`${API_BASE_URL}/salesman?t=${Date.now()}`);
      const json = await response.json();
      if (json.success) setSalesmen(json.data);
    } catch (error) {
      console.error('Fetch Salesmen Error:', error);
    }
  };

  useEffect(() => {
    fetchTrays();
    fetchTrayGroups();
    fetchSalesmen();
  }, []);

  // 1. txtTrayID blur validation
  const handleTrayIDBlur = () => {
    if (!trayID || !trayID.trim()) {
      showError('TrayID should not be blank.');
    }
  };

  // 2. txtTrayName blur validation
  const handleTrayNameBlur = () => {
    if (!trayName || !trayName.trim()) {
      showError('TrayName should not be blank.');
    }
  };

  // 3. txtSalesmanID blur validation & auto-fill name
  const handleSalesmanIDBlur = async () => {
    const trimmed = (salesManID || '').trim();
    if (!trimmed) {
      showError('SalesmanID should not be blank.');
      setSalesManName('');
      return;
    }

    try {
      const res = await fetch(`${API_URL}/validate-salesman/${encodeURIComponent(trimmed)}`);
      const json = await res.json();
      if (json.success && json.data) {
        setSalesManName(json.data.SalesManName || '');
      } else {
        showError('Please enter valid SalesmanID.');
        setSalesManID('');
        setSalesManName('');
      }
    } catch (e) {
      const matched = salesmen.find((s: any) => (s.SalesManCode || s.SalesManID || '').trim().toUpperCase() === trimmed.toUpperCase());
      if (matched) {
        setSalesManName(matched.SalesManName || '');
      } else {
        showError('Please enter valid SalesmanID.');
        setSalesManID('');
        setSalesManName('');
      }
    }
  };

  // 4. txtTrayGroup blur validation
  const handleTrayGroupBlur = () => {
    if (!trayGroup || !trayGroup.trim()) {
      showError('TrayGroup should not be blank.');
    }
  };

  const handleSave = async () => {
    // Exact validations from ValidateMe in frmTray.vb (lines 415-447)
    if (!trayID || !trayID.trim()) {
      showError('TrayID should not be blank.');
      return;
    }
    if (!trayName || !trayName.trim()) {
      showError('TrayName should not be blank.');
      return;
    }
    if (!salesManID || !salesManID.trim()) {
      showError('SalesmanID should not be blank.');
      return;
    }
    if (!salesManName || !salesManName.trim()) {
      showError('SalesmanName should not be blank.');
      return;
    }
    if (!trayGroup || !trayGroup.trim()) {
      showError('TrayGroup should not be blank.');
      return;
    }

    const payload = {
      TrayID: trayID.trim().toUpperCase(),
      TrayName: trayName.trim(),
      TrayGroup: trayGroup.trim().toUpperCase(),
      SalesManID: salesManID.trim().toUpperCase(),
      SalesManName: salesManName.trim(),
      IsCustomerTray: isCustomerTray ? 'Y' : 'N'
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
        showSuccess(json.message || 'Tray detail saved sucssfully.');
        setIsModalVisible(false);
        fetchTrays();
      } else {
        showError(json.message || 'Error in Save');
      }
    } catch (error) {
      console.error('Save error:', error);
      showError('Failed to save tray');
    } finally {
      setSaving(false);
    }
  };

  const executeDelete = async (id: string) => {
    setDeletingId(id);
    try {
      const response = await fetch(`${API_URL}?id=${encodeURIComponent(id)}`, { method: 'DELETE' });
      const json = await response.json();
      if (json.success) {
        showSuccess(json.message || 'Tray detail deleted.');
        fetchTrays();
      } else {
        showError(json.message || 'Error in deleting Category detail.');
      }
    } catch (error) {
      console.error('Delete error:', error);
      showError('Failed to delete tray');
    } finally {
      setDeletingId(null);
    }
  };

  const handleDelete = (id: string) => {
    setDeleteConfirmId(id);
  };

  const openAddModal = () => {
    setIsEditing(false);
    setTrayID('');
    setTrayName('');
    setTrayGroup('');
    setSalesManID('');
    setSalesManName('');
    setIsCustomerTray(false);
    setIsModalVisible(true);
  };

  const openEditModal = (item: any) => {
    setIsEditing(true);
    setTrayID(item.TrayID);
    setTrayName(item.TrayName || '');
    setTrayGroup(item.TrayGroup || '');
    setSalesManID(item.SalesManID || '');
    setSalesManName(item.SalesManName || '');
    setIsCustomerTray(item.IsCustomerTray === 'Y');
    setIsModalVisible(true);
  };

  const renderItem = ({ item }: { item: any }) => (
    <View style={[styles.row, { backgroundColor: cardColor, borderColor: borderColor }]}>
      <View style={[styles.cell, { flex: 1 }]}>
        <ThemedText type="tableText" style={{ color: textColor, fontWeight: '500', fontSize: 11 }}>{item.TrayID}</ThemedText>
      </View>
      <View style={[styles.cell, { flex: 2 }]}>
        <ThemedText type="tableText" style={{ color: textColor, fontSize: 11 }}>{item.TrayName}</ThemedText>
      </View>
      <View style={[styles.cell, { flex: 1.5 }]}>
        <ThemedText type="tableText" style={{ color: textColor, fontSize: 11 }}>{item.TrayGroup}</ThemedText>
      </View>
      <View style={[styles.cell, { flex: 1.5 }]}>
        <ThemedText type="tableText" style={{ color: textColor, fontSize: 11 }}>{item.SalesManID}</ThemedText>
      </View>
      <View style={[styles.cell, { flex: 1, alignItems: 'center' }]}>
        {item.IsCustomerTray === 'Y' ? (
          <Ionicons name="checkmark-circle" size={20} color="#4CAF50" />
        ) : (
          <Ionicons name="close-circle-outline" size={20} color={subtitleColor} />
        )}
      </View>
      <View style={[styles.actionCell, { alignItems: 'center', justifyContent: 'center' }]}>
        <TouchableOpacity style={styles.actionButton} onPress={() => openEditModal(item)} disabled={deletingId === item.TrayID}>
          <Ionicons name="pencil-outline" size={20} color={GOLD_COLOR} />
        </TouchableOpacity>
        <TouchableOpacity style={styles.actionButton} onPress={() => handleDelete(item.TrayID)} disabled={deletingId === item.TrayID}>
          {deletingId === item.TrayID ? (
            <ActivityIndicator size="small" color="#F44336" />
          ) : (
            <Ionicons name="trash-outline" size={20} color="#F44336" />
          )}
        </TouchableOpacity>
      </View>
    </View>
  );

  const renderToast = () => {
    if (!toast) return null;
    const isError = toast.type === 'error';
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
        <ThemedText style={styles.toastText}>{toast.message}</ThemedText>
      </View>
    );
  };

  return (
    <SafeAreaView style={[styles.safeArea, { backgroundColor }]} edges={['top']}>
      <Stack.Screen options={{ headerShown: false }} />
      {!isModalVisible && !deleteConfirmId && !isGroupLookupVisible && !isSalesmanLookupVisible && renderToast()}
      {/* Header */}
      <View style={[styles.header, { borderBottomColor: borderColor }]}>
        <View style={{ flexDirection: 'row', alignItems: 'center' }}>
          <TouchableOpacity style={styles.backButton} onPress={() => router.back()}>
            <Ionicons name="arrow-back" size={24} color={textColor} />
          </TouchableOpacity>
          <ThemedText style={[styles.title, { color: textColor }]}>Tray Master</ThemedText>
        </View>
        <TouchableOpacity style={[styles.addButton, { backgroundColor: 'rgba(212, 175, 55, 0.15)', borderColor: GOLD_COLOR }]} onPress={openAddModal}>
          <Ionicons name="add" size={20} color={GOLD_COLOR} />
          <ThemedText style={[styles.addButtonText, { color: GOLD_COLOR }]}>New</ThemedText>
        </TouchableOpacity>
      </View>

      <View style={styles.tableContainer}>
      {/* Table Header */}
      <View style={[styles.tableHeader, { backgroundColor: 'rgba(0,0,0,0.02)' }]}>
        <View style={[styles.cell, { flex: 1 }]}><ThemedText style={[styles.headerText, { color: subtitleColor }]}>TRAY ID</ThemedText></View>
        <View style={[styles.cell, { flex: 2 }]}><ThemedText style={[styles.headerText, { color: subtitleColor }]}>TRAY NAME</ThemedText></View>
        <View style={[styles.cell, { flex: 1.5 }]}><ThemedText style={[styles.headerText, { color: subtitleColor }]}>GROUP</ThemedText></View>
        <View style={[styles.cell, { flex: 1.5 }]}><ThemedText style={[styles.headerText, { color: subtitleColor }]}>SALESMAN</ThemedText></View>
        <View style={[styles.cell, { flex: 1, alignItems: 'center' }]}><ThemedText style={[styles.headerText, { color: subtitleColor }]}>CUST TRAY</ThemedText></View>
        <View style={[styles.actionCell, { width: 120 }]}><ThemedText style={[styles.headerText, { color: subtitleColor, width: 55, textAlign: 'center' }]}>EDIT</ThemedText><ThemedText style={[styles.headerText, { color: subtitleColor, width: 55, textAlign: 'center' }]}>DELETE</ThemedText></View>
      </View>

      {loading ? (
        <View style={styles.centered}><ActivityIndicator size="large" color={GOLD_COLOR} /></View>
      ) : (
        <FlatList
          data={data}
          renderItem={renderItem}
          keyExtractor={(item) => item.TrayID}
          contentContainerStyle={{ paddingBottom: 20 }}
          ListEmptyComponent={
            <View style={styles.centered}>
              <ThemedText style={{ color: subtitleColor }}>No trays found.</ThemedText>
            </View>
          }
        />
      )}
      </View>

      {/* Modal */}
      <Modal visible={isModalVisible} transparent animationType="fade">
        <TouchableOpacity style={styles.modalOverlay} activeOpacity={1} onPress={() => setIsModalVisible(false)}>
          {renderToast()}
          <TouchableWithoutFeedback>
            <View style={[styles.modalContent, { backgroundColor: cardColor, borderColor }]}>
              <View style={[styles.modalHeader, { borderBottomColor: borderColor }]}>
                <ThemedText style={{ fontSize: 18, fontWeight: 'bold', color: textColor }}>
                  {isEditing ? 'Edit Tray' : 'Add New Tray'}
                </ThemedText>
                <TouchableOpacity onPress={() => setIsModalVisible(false)}>
                  <Ionicons name="close" size={24} color={textColor} />
                </TouchableOpacity>
              </View>

              <View style={styles.modalBody}>
                <View style={styles.inputGroup}>
                  <ThemedText style={[styles.label, { color: subtitleColor }]}>Tray ID *</ThemedText>
                  <TextInput
                    style={[styles.input, { color: textColor, borderColor, backgroundColor: isDark ? '#2A2A2A' : '#FFF' }, isEditing && { backgroundColor: isDark ? '#333' : '#F0F0F0', color: subtitleColor }]}
                    value={trayID}
                    onChangeText={(t) => setTrayID(t.toUpperCase())}
                    onBlur={handleTrayIDBlur}
                    editable={!isEditing}
                    placeholder="Enter Tray ID"
                    placeholderTextColor={subtitleColor}
                    autoCapitalize="characters"
                  />
                </View>

                <View style={styles.inputGroup}>
                  <ThemedText style={[styles.label, { color: subtitleColor }]}>Tray Name *</ThemedText>
                  <TextInput
                    style={[styles.input, { color: textColor, borderColor, backgroundColor: isDark ? '#2A2A2A' : '#FFF' }]}
                    value={trayName}
                    onChangeText={setTrayName}
                    onBlur={handleTrayNameBlur}
                    placeholder="Tray Name"
                    placeholderTextColor={subtitleColor}
                  />
                </View>

                <View style={styles.inputGroup}>
                  <ThemedText style={[styles.label, { color: subtitleColor }]}>Tray Group *</ThemedText>
                  <View style={styles.lookupContainer}>
                    <TextInput
                      style={[styles.input, { color: textColor, borderColor, backgroundColor: isDark ? '#2A2A2A' : '#FFF' }]}
                      value={trayGroup}
                      onChangeText={(t) => setTrayGroup(t.toUpperCase())}
                      onBlur={handleTrayGroupBlur}
                      placeholder="Enter Tray Group"
                      placeholderTextColor={subtitleColor}
                      autoCapitalize="characters"
                    />
                    <TouchableOpacity style={styles.lookupButton} onPress={() => setIsGroupLookupVisible(true)}>
                      <ThemedText style={{ color: textColor, fontWeight: 'bold' }}>...</ThemedText>
                    </TouchableOpacity>
                  </View>
                </View>

                <View style={styles.inputGroup}>
                  <ThemedText style={[styles.label, { color: subtitleColor }]}>Salesman ID *</ThemedText>
                  <View style={styles.lookupContainer}>
                    <TextInput
                      style={[styles.input, { color: textColor, borderColor, backgroundColor: isDark ? '#2A2A2A' : '#FFF' }]}
                      value={salesManID}
                      onChangeText={(t) => setSalesManID(t.toUpperCase())}
                      onBlur={handleSalesmanIDBlur}
                      placeholder="Enter Salesman ID"
                      placeholderTextColor={subtitleColor}
                      autoCapitalize="characters"
                    />
                    <TouchableOpacity style={styles.lookupButton} onPress={() => setIsSalesmanLookupVisible(true)}>
                      <ThemedText style={{ color: textColor, fontWeight: 'bold' }}>...</ThemedText>
                    </TouchableOpacity>
                  </View>
                </View>

                <View style={styles.inputGroup}>
                  <ThemedText style={[styles.label, { color: subtitleColor }]}>Salesman Name *</ThemedText>
                  <TextInput
                    style={[styles.input, { color: subtitleColor, borderColor, backgroundColor: isDark ? '#333' : '#F0F0F0' }]}
                    value={salesManName}
                    editable={false}
                    placeholder="Salesman Name"
                    placeholderTextColor={subtitleColor}
                  />
                </View>

                <View style={styles.inputGroup}>
                  <ThemedText style={[styles.label, { color: subtitleColor }]}>Tray for outside d</ThemedText>
                  <View style={{ flex: 1, alignItems: 'flex-start' }}>
                    <TouchableOpacity 
                      style={[styles.checkbox, { borderColor }, isCustomerTray && { backgroundColor: GOLD_COLOR, borderColor: GOLD_COLOR }]}
                      onPress={() => setIsCustomerTray(!isCustomerTray)}
                    >
                      {isCustomerTray && <Ionicons name="checkmark" size={16} color="#FFF" />}
                    </TouchableOpacity>
                  </View>
                </View>
              </View>

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
        {renderToast()}
      </Modal>

      {/* Tray Group Lookup Modal */}
      <Modal visible={isGroupLookupVisible} transparent animationType="slide">
        <View style={styles.modalOverlay}>
          <View style={[styles.modalContent, { backgroundColor: cardColor, borderColor, height: '60%' }]}>
            <View style={[styles.modalHeader, { borderBottomColor: borderColor }]}>
              <ThemedText style={{ fontSize: 18, fontWeight: 'bold', color: textColor }}>
                Select Tray Group
              </ThemedText>
              <TouchableOpacity onPress={() => setIsGroupLookupVisible(false)}>
                <Ionicons name="close" size={24} color={textColor} />
              </TouchableOpacity>
            </View>
            <View style={{ flex: 1, marginTop: 10 }}>
              {trayGroups.length > 0 ? (
                <FlatList
                  data={trayGroups}
                  keyExtractor={(item) => item.TrayGroupCode}
                  renderItem={({ item }) => (
                    <TouchableOpacity
                      style={{ padding: 16, borderBottomWidth: 1, borderBottomColor: borderColor }}
                      onPress={() => {
                        setTrayGroup(item.TrayGroupCode);
                        setIsGroupLookupVisible(false);
                      }}
                    >
                      <ThemedText style={{ color: textColor, fontWeight: 'bold' }}>{item.TrayGroupCode}</ThemedText>
                      <ThemedText style={{ color: subtitleColor, fontSize: 12 }}>{item.TrayGroupName}</ThemedText>
                    </TouchableOpacity>
                  )}
                />
              ) : (
                <ThemedText style={{ color: subtitleColor }}>No tray groups available yet.</ThemedText>
              )}
            </View>
          </View>
        </View>
      </Modal>

      {/* Salesman Lookup Modal */}
      <Modal visible={isSalesmanLookupVisible} transparent animationType="slide">
        <View style={styles.modalOverlay}>
          <View style={[styles.modalContent, { backgroundColor: cardColor, borderColor, height: '60%' }]}>
            <View style={[styles.modalHeader, { borderBottomColor: borderColor }]}>
              <ThemedText style={{ fontSize: 18, fontWeight: 'bold', color: textColor }}>
                Select Salesman
              </ThemedText>
              <TouchableOpacity onPress={() => setIsSalesmanLookupVisible(false)}>
                <Ionicons name="close" size={24} color={textColor} />
              </TouchableOpacity>
            </View>
            <View style={{ flex: 1, marginTop: 10 }}>
              {salesmen.length > 0 ? (
                <FlatList
                  data={salesmen}
                  keyExtractor={(item) => item.SalesManCode}
                  renderItem={({ item }) => (
                    <TouchableOpacity
                      style={{ padding: 16, borderBottomWidth: 1, borderBottomColor: borderColor }}
                      onPress={() => {
                        setSalesManID(item.SalesManCode);
                        setSalesManName(item.SalesManName);
                        setIsSalesmanLookupVisible(false);
                      }}
                    >
                      <ThemedText style={{ color: textColor, fontWeight: 'bold' }}>{item.SalesManCode}</ThemedText>
                      <ThemedText style={{ color: subtitleColor, fontSize: 12 }}>{item.SalesManName}</ThemedText>
                    </TouchableOpacity>
                  )}
                />
              ) : (
                <ThemedText style={{ color: subtitleColor }}>No salesmen available yet.</ThemedText>
              )}
            </View>
          </View>
        </View>
      </Modal>

      {/* Delete Confirmation Modal */}
      <Modal visible={!!deleteConfirmId} animationType="fade" transparent={true}>
        <TouchableWithoutFeedback onPress={() => setDeleteConfirmId(null)}>
          <View style={styles.modalOverlay}>
            <TouchableWithoutFeedback>
              <View style={[styles.modalContent, { backgroundColor: cardColor, borderColor, maxWidth: 420, padding: 24 }]}>
                <View style={{ flexDirection: 'row', alignItems: 'center', marginBottom: 16 }}>
                  <Ionicons name="trash-outline" size={24} color="#C62828" style={{ marginRight: 10 }} />
                  <ThemedText style={{ fontSize: 18, fontWeight: 'bold', color: textColor }}>Delete Tray</ThemedText>
                </View>
                <ThemedText style={{ color: textColor, fontSize: 14, marginBottom: 24, lineHeight: 22 }}>
                  {`Do you want to delete `}
                  <ThemedText style={{ fontWeight: 'bold' }}>{deleteConfirmId || ''}</ThemedText>
                  {` Category?`}
                </ThemedText>
                <View style={{ flexDirection: 'row', justifyContent: 'flex-end', gap: 12 }}>
                  <TouchableOpacity
                    style={[styles.btn, styles.btnCancel]}
                    onPress={() => setDeleteConfirmId(null)}
                  >
                    <ThemedText style={{ color: '#666', fontWeight: '600' }}>Cancel</ThemedText>
                  </TouchableOpacity>
                  <TouchableOpacity
                    style={[styles.btn, { backgroundColor: '#C62828' }]}
                    onPress={() => {
                      const id = deleteConfirmId;
                      setDeleteConfirmId(null);
                      if (id) executeDelete(id);
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
    backgroundColor: '#F5B041',
    paddingHorizontal: 16,
    justifyContent: 'center',
    alignItems: 'center',
    borderRadius: 6,
  },
  checkbox: {
    width: 24,
    height: 24,
    borderWidth: 1,
    borderRadius: 4,
    justifyContent: 'center',
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
