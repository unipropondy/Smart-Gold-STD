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
const API_URL = `${API_BASE_URL}/usermaster`;

export default function UserMasterScreen() {
  const themeColors = useThemeColors();
  const { accentColor } = useThemeContext();
  const { background: backgroundColor, card: cardColor, text: textColor, border: borderColor, primaryGold: GOLD_COLOR, subtitle: subtitleColor } = themeColors;
  const colorScheme = useColorScheme();
  const isDark = colorScheme === 'dark';
  const router = useRouter();

  const [data, setData] = useState<any[]>([]);
  const [userGroups, setUserGroups] = useState<any[]>([]);
  const [loading, setLoading] = useState(true);
  const [saving, setSaving] = useState(false);

  // Modal State
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [isGroupLookupVisible, setIsGroupLookupVisible] = useState(false);
  const [isEditing, setIsEditing] = useState(false);

  // Form State
  const [userCode, setUserCode] = useState('');
  const [userName, setUserName] = useState('');
  const [userGroupCode, setUserGroupCode] = useState('');
  const [expiryDate, setExpiryDate] = useState('');
  const [daysOfWarning, setDaysOfWarning] = useState('0');

  const headerBgColor = isDark ? '#2A2A2A' : '#F5F5F5';

  const fetchData = async () => {
    try {
      const [usersRes, groupsRes] = await Promise.all([
        fetch(`${API_URL}?t=${Date.now()}`, { headers: { 'Cache-Control': 'no-cache' } }),
        fetch(`${API_URL}/groups?t=${Date.now()}`, { headers: { 'Cache-Control': 'no-cache' } })
      ]);
      const usersJson = await usersRes.json();
      const groupsJson = await groupsRes.json();

      if (usersJson.success) setData(usersJson.data);
      if (groupsJson.success) setUserGroups(groupsJson.data);
    } catch (error) {
      console.error('Network Error:', error);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchData();
  }, []);

  const formatDateForInput = (dateString: string) => {
    if (!dateString) return '';
    const d = new Date(dateString);
    if (isNaN(d.getTime())) return '';
    return d.toISOString().split('T')[0];
  };

  const handleSave = async () => {
    if (!userCode.trim()) {
      Alert.alert('Validation Error', 'User Code is required');
      return;
    }

    const payload = {
      UserCode: userCode.trim(),
      UserName: userName.trim(),
      UserGroupCode: userGroupCode.trim(),
      ExpiryDate: expiryDate ? new Date(expiryDate).toISOString() : null,
      DaysOfWarning: parseInt(daysOfWarning) || 0,
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
        Alert.alert('Success', `User ${isEditing ? 'updated' : 'added'} successfully`);
        setIsModalVisible(false);
        fetchData();
      } else {
        Alert.alert('Error', json.message || 'Failed to save');
      }
    } catch (error) {
      console.error('Save error:', error);
      Alert.alert('Error', 'Failed to save user');
    } finally {
      setSaving(false);
    }
  };

  const executeDelete = async (codeToDelete: string) => {
    try {
      const response = await fetch(`${API_URL}?id=${encodeURIComponent(codeToDelete)}`, { method: 'DELETE' });
      const json = await response.json();
      if (json.success) {
        Alert.alert('Success', 'User deleted successfully');
        fetchData();
      } else {
        Alert.alert('Error', json.message || 'Failed to delete');
      }
    } catch (error) {
      console.error('Delete error:', error);
      Alert.alert('Error', 'Failed to delete user');
    }
  };

  const handleDelete = (codeToDelete: string) => {
    if (Platform.OS === 'web') {
      if (window.confirm(`Are you sure you want to delete User ${codeToDelete}?`)) {
        executeDelete(codeToDelete);
      }
    } else {
      Alert.alert('Confirm Delete', `Are you sure you want to delete User ${codeToDelete}?`, [
        { text: 'Cancel', style: 'cancel' },
        { text: 'Delete', style: 'destructive', onPress: () => executeDelete(codeToDelete) },
      ]);
    }
  };

  const openAddModal = () => {
    setIsEditing(false);
    setUserCode('');
    setUserName('');
    setUserGroupCode('');
    setExpiryDate('');
    setDaysOfWarning('0');
    setIsModalVisible(true);
  };

  const openEditModal = (item: any) => {
    setIsEditing(true);
    setUserCode(item.UserCode);
    setUserName(item.UserName || '');
    setUserGroupCode(item.UserGroupCode || '');
    setExpiryDate(formatDateForInput(item.ExpiryDate));
    setDaysOfWarning(item.DaysOfWarning?.toString() || '0');
    setIsModalVisible(true);
  };

  const renderItem = ({ item }: { item: any }) => (
    <View style={[styles.row, { backgroundColor: cardColor, borderColor: borderColor }]}>
      <View style={[styles.cell, { flex: 1.5 }]}>
        <ThemedText type="tableText" style={{ color: textColor, fontWeight: '500', fontSize: 11 }}>{item.UserCode}</ThemedText>
      </View>
      <View style={[styles.cell, { flex: 2 }]}>
        <ThemedText type="tableText" style={{ color: textColor, fontSize: 11 }}>{item.UserName}</ThemedText>
      </View>
      <View style={[styles.cell, { flex: 1.5 }]}>
        <ThemedText type="tableText" style={{ color: textColor, fontSize: 11 }}>{item.UserGroupCode}</ThemedText>
      </View>
      <View style={styles.actionCell}>
        <TouchableOpacity style={styles.actionButton} onPress={() => openEditModal(item)}>
          <Ionicons name="pencil-outline" size={20} color={GOLD_COLOR} />
        </TouchableOpacity>
        <TouchableOpacity style={styles.actionButton} onPress={() => handleDelete(item.UserCode)}>
          <Ionicons name="trash-outline" size={20} color="#F44336" />
        </TouchableOpacity>
      </View>
    </View>
  );

  return (
    <SafeAreaView style={[styles.safeArea, { backgroundColor }]} edges={['top']}>
      <Stack.Screen options={{ headerShown: false }} />
      {/* Header */}
      <View style={[styles.header, { borderBottomColor: borderColor }]}>
        <View style={{ flexDirection: 'row', alignItems: 'center' }}>
          <TouchableOpacity style={styles.backButton} onPress={() => router.back()}>
            <Ionicons name="arrow-back" size={24} color={textColor} />
          </TouchableOpacity>
          <ThemedText style={[styles.title, { color: textColor }]}>User Master</ThemedText>
        </View>
        <TouchableOpacity style={[styles.addButton, { backgroundColor: 'rgba(212, 175, 55, 0.15)', borderColor: GOLD_COLOR }]} onPress={openAddModal}>
          <Ionicons name="add" size={20} color={GOLD_COLOR} />
          <ThemedText style={[styles.addButtonText, { color: GOLD_COLOR }]}>New</ThemedText>
        </TouchableOpacity>
      </View>

      <View style={styles.tableContainer}>
      {/* Table Header */}
      <View style={[styles.tableHeader, { backgroundColor: 'rgba(0,0,0,0.02)' }]}>
        <View style={[styles.cell, { flex: 1.5 }]}><ThemedText style={[styles.headerText, { color: subtitleColor }]}>USER CODE</ThemedText></View>
        <View style={[styles.cell, { flex: 2 }]}><ThemedText style={[styles.headerText, { color: subtitleColor }]}>USER NAME</ThemedText></View>
        <View style={[styles.cell, { flex: 1.5 }]}><ThemedText style={[styles.headerText, { color: subtitleColor }]}>GROUP</ThemedText></View>
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
          keyExtractor={(item, index) => item.UserCode || index.toString()}
          contentContainerStyle={{ paddingBottom: 20 }}
          ListEmptyComponent={
            <View style={styles.centered}>
              <ThemedText style={{ color: subtitleColor }}>No users found.</ThemedText>
            </View>
          }
        />
      )}
      </View>

      {/* Main Modal */}
      <Modal visible={isModalVisible} transparent animationType="fade">
        <TouchableOpacity style={styles.modalOverlay} activeOpacity={1} onPress={() => setIsModalVisible(false)}>
          <TouchableWithoutFeedback>
            <View style={[styles.modalContent, { backgroundColor: cardColor, borderColor }]}>
              <View style={[styles.modalHeader, { borderBottomColor: borderColor }]}>
                <ThemedText style={{ fontSize: 16, fontWeight: 'bold', color: textColor }}>
                  {isEditing ? 'Edit User' : 'Add New User'}
                </ThemedText>
                <TouchableOpacity onPress={() => setIsModalVisible(false)}>
                  <Ionicons name="close" size={24} color={textColor} />
                </TouchableOpacity>
              </View>

              <View style={styles.modalBody}>
                <View style={styles.inputGroup}>
                  <ThemedText style={[styles.label, { color: subtitleColor }]}>User Code *</ThemedText>
                  <TextInput
                    style={[styles.input, { color: textColor, borderColor, backgroundColor: isDark ? '#2A2A2A' : '#FFF' }, isEditing && { backgroundColor: isDark ? '#333' : '#F0F0F0', color: subtitleColor }]}
                    value={userCode}
                    onChangeText={setUserCode}
                    editable={!isEditing}
                    placeholder="Enter User Code"
                    placeholderTextColor={subtitleColor}
                  />
                </View>

                <View style={styles.inputGroup}>
                  <ThemedText style={[styles.label, { color: subtitleColor }]}>User Name</ThemedText>
                  <TextInput
                    style={[styles.input, { color: textColor, borderColor, backgroundColor: isDark ? '#2A2A2A' : '#FFF' }]}
                    value={userName}
                    onChangeText={setUserName}
                    placeholder="Enter User Name"
                    placeholderTextColor={subtitleColor}
                  />
                </View>

                <View style={styles.inputGroup}>
                  <ThemedText style={[styles.label, { color: subtitleColor }]}>User Group Code</ThemedText>
                  <TouchableOpacity
                    style={[styles.input, { justifyContent: 'center', backgroundColor: isDark ? '#2A2A2A' : '#FFF', borderColor }]}
                    onPress={() => setIsGroupLookupVisible(true)}
                  >
                    <ThemedText style={{ color: userGroupCode ? textColor : subtitleColor }}>
                      {userGroupCode || 'Select User Group'}
                    </ThemedText>
                  </TouchableOpacity>
                </View>

                <View style={styles.inputGroup}>
                  <ThemedText style={[styles.label, { color: subtitleColor }]}>Expired Date</ThemedText>
                  {Platform.OS === 'web' ? (
                    <input
                      type="date"
                      style={{
                        flex: 1,
                        borderWidth: 1,
                        borderRadius: 6,
                        padding: '10px 12px',
                        fontSize: '14px',
                        color: textColor,
                        backgroundColor: isDark ? '#2A2A2A' : '#FFF',
                        borderColor: borderColor,
                        outline: 'none',
                      }}
                      value={expiryDate}
                      onChange={(e) => setExpiryDate(e.target.value)}
                    />
                  ) : (
                    <TextInput
                      style={[styles.input, { color: textColor, borderColor, backgroundColor: isDark ? '#2A2A2A' : '#FFF' }]}
                      value={expiryDate}
                      onChangeText={setExpiryDate}
                      placeholder="YYYY-MM-DD"
                      placeholderTextColor={subtitleColor}
                    />
                  )}
                </View>

                <View style={styles.inputGroup}>
                  <ThemedText style={[styles.label, { color: subtitleColor }]}>Days Of Warning</ThemedText>
                  <TextInput
                    style={[styles.input, { color: textColor, borderColor, backgroundColor: isDark ? '#2A2A2A' : '#FFF' }]}
                    value={daysOfWarning}
                    onChangeText={setDaysOfWarning}
                    keyboardType="numeric"
                    placeholder="0"
                    placeholderTextColor={subtitleColor}
                  />
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
      </Modal>

      {/* Group Lookup Modal */}
      <Modal visible={isGroupLookupVisible} transparent animationType="slide">
        <View style={styles.modalOverlay}>
          <View style={[styles.lookupContent, { backgroundColor: cardColor, borderColor }]}>
            <View style={[styles.modalHeader, { borderBottomColor: borderColor }]}>
              <ThemedText style={{ fontSize: 18, fontWeight: 'bold', color: textColor }}>Select User Group</ThemedText>
              <TouchableOpacity onPress={() => setIsGroupLookupVisible(false)}>
                <Ionicons name="close" size={24} color={textColor} />
              </TouchableOpacity>
            </View>
            <FlatList
              data={userGroups}
              keyExtractor={(item) => item.UserGroupCode}
              renderItem={({ item }) => (
                <TouchableOpacity
                  style={[styles.lookupRow, { borderBottomColor: borderColor }]}
                  onPress={() => {
                    setUserGroupCode(item.UserGroupCode);
                    setIsGroupLookupVisible(false);
                  }}
                >
                  <ThemedText style={{ color: textColor, fontWeight: '500' }}>{item.UserGroupCode}</ThemedText>
                </TouchableOpacity>
              )}
              ListEmptyComponent={
                <View style={{ padding: 20, alignItems: 'center' }}>
                  <ThemedText style={{ color: subtitleColor }}>No groups found</ThemedText>
                </View>
              }
            />
          </View>
        </View>
      </Modal>

    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  safeArea: { flex: 1 },
  header: { flexDirection: 'row', alignItems: 'center', justifyContent: 'space-between', paddingHorizontal: 12, paddingVertical: 6, borderBottomWidth: 1 },
  backButton: { padding: 4, marginRight: 8 },
  title: { fontSize: 16, fontWeight: 'bold' },
  addButton: { flexDirection: 'row', alignItems: 'center', paddingHorizontal: 10, paddingVertical: 6, borderRadius: 8, borderWidth: 1 },
  addButtonText: { marginLeft: 4, fontWeight: '600', fontSize: 12 },
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
  lookupContent: {
    width: '100%',
    maxWidth: 400,
    maxHeight: '70%',
    borderWidth: 1,
    borderRadius: 12,
  },
  lookupRow: {
    flexDirection: 'row',
    padding: 16,
    borderBottomWidth: 1,
  },
  modalHeader: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    padding: 16,
    borderBottomWidth: 1,
  },
  modalBody: { padding: 16 },
  inputGroup: { 
    flexDirection: 'row', 
    alignItems: 'center',
    marginBottom: 12 
  },
  label: {
    flex: 0.4,
    fontSize: 12,
    fontWeight: '600',
    marginRight: 10,
  },
  input: {
    flex: 1,
    borderWidth: 1,
    borderRadius: 6,
    paddingHorizontal: 10,
    paddingVertical: 8,
    fontSize: 12,
  },
  modalFooter: {
    flexDirection: 'row',
    justifyContent: 'flex-end',
    gap: 12,
    padding: 16,
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
});
