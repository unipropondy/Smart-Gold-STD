import { Ionicons } from '@expo/vector-icons';
import { router } from 'expo-router';
import React, { useEffect, useState } from 'react';
import { ActivityIndicator, Alert, FlatList, Modal, Platform, ScrollView, StyleSheet, Text, TextInput, TouchableOpacity, useColorScheme, View } from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';
import { ThemedText } from '@/components/themed-text';
import { useThemeColors } from '@/hooks/useThemeColors';
import { useThemeContext } from '@/contexts/ThemeContext';
import { API_BASE_URL } from '@/constants/api';

const API_URL = `${API_BASE_URL}/class`;

export default function ClassScreen() {
  const themeColors = useThemeColors();
  const { accentColor } = useThemeContext();
  const { background: backgroundColor, card: cardColor, text: textColor, border: borderColor, primaryGold: GOLD_COLOR, subtitle: subtitleColor } = themeColors;
  const colorScheme = useColorScheme();
  const isDark = colorScheme === 'dark';
  const [data, setData] = useState<any[]>([]);
  const [loading, setLoading] = useState(true);
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

  // Modal state
  const [modalVisible, setModalVisible] = useState(false);
  const [isEditing, setIsEditing] = useState(false);
  const [currentClassCode, setCurrentClassCode] = useState('');
  const [className, setClassName] = useState('');
  const [salesType, setSalesType] = useState('W');
  const [purchaseType, setPurchaseType] = useState('W');
  const [goldLoss, setGoldLoss] = useState(false);
  const [displayCost, setDisplayCost] = useState(false);
  const [goldBarCode, setGoldBarCode] = useState(false);
  const [remarks, setRemarks] = useState('');
  const [rate, setRate] = useState('0');
  const [mcDiscount, setMcDiscount] = useState('0');

  const [saving, setSaving] = useState(false);

  const inputBg = isDark ? '#222' : '#F5F5F5';

  const fetchClasses = async () => {
    try {
      setLoading(true);
      const res = await fetch(API_URL);
      const json = await res.json();
      if (json.success) {
        setData(json.data);
      } else {
        Alert.alert("Error", json.message || "Failed to fetch classes");
      }
    } catch (err) {
      console.error(err);
      Alert.alert("Error", "Network error connecting to API");
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchClasses();
  }, []);

  const performDelete = async (id: string) => {
    setDeletingId(id);
    try {
      const res = await fetch(`${API_URL}/${encodeURIComponent(id)}`, { method: 'DELETE' });
      const json = await res.json();
      if (json.success) {
        showSuccess('Class deleted successfully');
        setData(data.filter(item => item.ClassCode !== id));
      } else {
        showError(json.message || "Failed to delete");
      }
    } catch (err) {
      console.error(err);
      showError("Failed to delete class");
    } finally {
      setDeletingId(null);
    }
  };

  const handleDelete = async (id: string) => {
    if (Platform.OS === 'web') {
      if (window.confirm("Are you sure you want to delete this class?")) {
        performDelete(id);
      }
    } else {
      Alert.alert("Confirm Delete", "Are you sure you want to delete this class?", [
        { text: "Cancel", style: "cancel" },
        { text: "Delete", style: "destructive", onPress: () => performDelete(id) }
      ]);
    }
  };

  const handleSave = async () => {
    if (!currentClassCode || !currentClassCode.trim()) {
      showError("Class Code is required.");
      return;
    }
    if (!className || !className.trim()) {
      showError("Class Name is required.");
      return;
    }

    const payload = {
      ClassCode: currentClassCode,
      ClassName: className,
      SalesType: salesType,
      PurchaseType: purchaseType,
      GoldLoss: goldLoss ? 'Y' : 'N',
      DisplayCost: displayCost ? 'Y' : 'N',
      GoldBarCode: goldBarCode ? 'Y' : 'N',
      Remarks: remarks,
      Rate: parseFloat(rate) || 0,
      MCDiscount: parseFloat(mcDiscount) || 0
    };

    try {
      setSaving(true);
      if (isEditing) {
        // UPDATE
        const res = await fetch(`${API_URL}/${encodeURIComponent(currentClassCode)}`, {
          method: 'PUT',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify(payload)
        });
        const json = await res.json();
        if (json.success) {
          showSuccess('Class updated successfully');
          fetchClasses();
          setModalVisible(false);
        } else {
          showError(json.message || "Failed to update");
        }
      } else {
        // INSERT
        const res = await fetch(API_URL, {
          method: 'POST',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify(payload)
        });
        const json = await res.json();
        if (json.success) {
          showSuccess('Class added successfully');
          fetchClasses();
          setModalVisible(false);
        } else {
          showError(json.message || "Failed to insert");
        }
      }
    } catch (err) {
      console.error(err);
      showError("Failed to save class");
    } finally {
      setSaving(false);
    }
  };

  const openAddModal = () => {
    setIsEditing(false);
    setCurrentClassCode('');
    setClassName('');
    setSalesType('W');
    setPurchaseType('W');
    setGoldLoss(false);
    setDisplayCost(false);
    setGoldBarCode(false);
    setRemarks('');
    setRate('0');
    setMcDiscount('0');
    setModalVisible(true);
  };

  const openEditModal = (item: any) => {
    setIsEditing(true);
    setCurrentClassCode(item.ClassCode);
    setClassName(item.ClassName);
    setSalesType(item.SalesType || 'W');
    setPurchaseType(item.PurchaseType || 'W');
    setGoldLoss(item.GoldLoss === 'Y');
    setDisplayCost(item.DisplayCost === 'Y');
    setGoldBarCode(item.GoldBarCode === 'Y');
    setRemarks(item.Remarks || '');
    setRate(item.Rate ? item.Rate.toString() : '0');
    setMcDiscount(item.MCDiscount ? item.MCDiscount.toString() : '0');
    setModalVisible(true);
  };
  const CustomRadio = ({ label, selected, onPress }: { label: string, selected: boolean, onPress: () => void }) => (
    <TouchableOpacity style={{ flexDirection: 'row', alignItems: 'center', marginRight: 16 }} onPress={onPress}>
      <View style={{ height: 18, width: 18, borderRadius: 9, borderWidth: 2, borderColor: selected ? GOLD_COLOR : borderColor, alignItems: 'center', justifyContent: 'center', marginRight: 8 }}>
        {selected && <View style={{ height: 10, width: 10, borderRadius: 5, backgroundColor: GOLD_COLOR }} />}
      </View>
      <ThemedText style={{ color: textColor }}>{label}</ThemedText>
    </TouchableOpacity>
  );

  const CustomCheckbox = ({ label, selected, onPress }: { label: string, selected: boolean, onPress: () => void }) => (
    <TouchableOpacity style={{ flexDirection: 'row', alignItems: 'center' }} onPress={onPress}>
      <View style={{ height: 20, width: 20, borderRadius: 4, borderWidth: 2, borderColor: selected ? GOLD_COLOR : borderColor, alignItems: 'center', justifyContent: 'center', marginRight: label ? 12 : 0, backgroundColor: selected ? GOLD_COLOR : 'transparent' }}>
        {selected && <Ionicons name="checkmark" size={16} color="#FFF" />}
      </View>
      {label ? <ThemedText style={{ color: textColor, fontWeight: '500' }}>{label}</ThemedText> : null}
    </TouchableOpacity>
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
      {!modalVisible && renderToast()}
      {/* Header */}
      <View style={[styles.header, { borderBottomColor: borderColor }]}>
        <View style={{ flexDirection: 'row', alignItems: 'center' }}>
          <TouchableOpacity style={styles.backButton} onPress={() => router.back()}>
            <Ionicons name="arrow-back" size={24} color={textColor} />
          </TouchableOpacity>
          <ThemedText style={[styles.title, { color: textColor }]}>Class Master</ThemedText>
        </View>
        <TouchableOpacity style={[styles.addButton, { backgroundColor: 'rgba(212, 175, 55, 0.15)', borderColor: GOLD_COLOR }]} onPress={openAddModal}>
          <Ionicons name="add" size={20} color={GOLD_COLOR} />
          <Text style={[styles.addButtonText, { color: GOLD_COLOR }]}>New</Text>
        </TouchableOpacity>
      </View>

      <View style={styles.tableContainer}>
      {/* Table Header */}
      <View style={[styles.tableHeader, { backgroundColor: 'rgba(0,0,0,0.02)' }]}>
        <View style={[styles.cell, { flex: 1 }]}>
          <ThemedText style={[styles.headerText, { color: subtitleColor }]}>CLASS CODE</ThemedText>
        </View>
        <View style={[styles.cell, { flex: 2 }]}>
          <ThemedText style={[styles.headerText, { color: subtitleColor }]}>CLASS NAME</ThemedText>
        </View>
        <View style={[styles.actionCell, { width: 120 }]}><ThemedText style={[styles.headerText, { color: subtitleColor, width: 55, textAlign: 'center' }]}>EDIT</ThemedText><ThemedText style={[styles.headerText, { color: subtitleColor, width: 55, textAlign: 'center' }]}>DELETE</ThemedText></View>
      </View>

      {/* Table Body */}
      {loading ? (
        <View style={{ flex: 1, justifyContent: 'center', alignItems: 'center' }}>
          <ActivityIndicator size="large" color={GOLD_COLOR} />
        </View>
      ) : (
        <FlatList
          data={data}
          keyExtractor={(item) => item.ClassCode}
          renderItem={({ item }) => (
            <View style={[styles.row, { backgroundColor: cardColor, borderColor: borderColor }]}>
              <View style={[styles.cell, { flex: 1 }]}>
                <ThemedText type="tableText" style={{ color: textColor, fontWeight: '500', fontSize: 11 }}>{item.ClassCode}</ThemedText>
              </View>
              <View style={[styles.cell, { flex: 2 }]}>
                <ThemedText type="tableText" style={{ color: textColor, fontSize: 11 }}>{item.ClassName}</ThemedText>
              </View>
              <View style={styles.actionCell}>
                <TouchableOpacity style={styles.actionButton} onPress={() => openEditModal(item)} disabled={deletingId === item.ClassCode}>
                  <Ionicons name="pencil-outline" size={20} color={GOLD_COLOR} />
                </TouchableOpacity>
                <TouchableOpacity style={styles.actionButton} onPress={() => handleDelete(item.ClassCode)} disabled={deletingId === item.ClassCode}>
                  {deletingId === item.ClassCode ? (
                    <ActivityIndicator size="small" color="#F44336" />
                  ) : (
                    <Ionicons name="trash-outline" size={20} color="#F44336" />
                  )}
                </TouchableOpacity>
              </View>
            </View>
          )}
          contentContainerStyle={{ paddingBottom: 20 }}
          ListEmptyComponent={
            <View style={{ padding: 40, alignItems: 'center' }}>
              <ThemedText style={{ color: subtitleColor }}>No classes found.</ThemedText>
            </View>
          }
        />
      )}
      </View>
      {/* Modal */}
      <Modal
        animationType="slide"
        transparent={true}
        visible={modalVisible}
        onRequestClose={() => setModalVisible(false)}
      >
        <View style={styles.modalOverlay}>
          {renderToast()}
          <View style={[styles.modalContent, { backgroundColor: cardColor, borderColor }]}>
            <View style={styles.modalHeader}>
              <ThemedText style={[styles.modalTitle, { color: textColor }]}>
                {isEditing ? 'Edit Class' : 'New Class'}
              </ThemedText>
              <TouchableOpacity onPress={() => setModalVisible(false)}>
                <Ionicons name="close" size={24} color={textColor} />
              </TouchableOpacity>
            </View>

            <ScrollView style={styles.modalBody}>
              <View style={{ flexDirection: 'row', marginBottom: 12 }}>
                <View style={{ flex: 1, flexDirection: 'row', alignItems: 'center', paddingRight: 10 }}>
                  <ThemedText style={[styles.inputLabel, { color: subtitleColor, width: 90, marginBottom: 0 }]}>Class Code</ThemedText>
                  <TextInput
                    style={[styles.input, { backgroundColor: inputBg, color: textColor, borderColor, height: 40, flex: 1 }]}
                    value={currentClassCode}
                    onChangeText={setCurrentClassCode}
                    placeholder="Enter Code"
                    placeholderTextColor={subtitleColor}
                    editable={!isEditing} 
                  />
                </View>
                <View style={{ flex: 1, flexDirection: 'row', alignItems: 'center', paddingLeft: 10 }}>
                  <ThemedText style={[styles.inputLabel, { color: subtitleColor, width: 90, marginBottom: 0 }]}>Class Name</ThemedText>
                  <TextInput
                    style={[styles.input, { backgroundColor: inputBg, color: textColor, borderColor, height: 40, flex: 1 }]}
                    value={className}
                    onChangeText={setClassName}
                    placeholder="Enter Name"
                    placeholderTextColor={subtitleColor}
                  />
                </View>
              </View>

              <View style={{ flexDirection: 'row', marginBottom: 14, alignItems: 'center' }}>
                <View style={{ flex: 1, flexDirection: 'row', alignItems: 'center', paddingRight: 10 }}>
                  <ThemedText style={[styles.inputLabel, { color: subtitleColor, width: 90, marginBottom: 0 }]}>Sales Type</ThemedText>
                  <View style={{ flexDirection: 'row', flex: 1, alignItems: 'center', gap: 24 }}>
                    <CustomRadio label="WT" selected={salesType === 'W'} onPress={() => setSalesType('W')} />
                    <CustomRadio label="PCS" selected={salesType === 'P'} onPress={() => setSalesType('P')} />
                    <CustomRadio label="Both" selected={salesType === 'B'} onPress={() => setSalesType('B')} />
                  </View>
                </View>
              </View>

              <View style={{ flexDirection: 'row', marginBottom: 14, alignItems: 'center' }}>
                <View style={{ flex: 1, flexDirection: 'row', alignItems: 'center', paddingRight: 10 }}>
                  <ThemedText style={[styles.inputLabel, { color: subtitleColor, width: 90, marginBottom: 0 }]}>Purchase Type</ThemedText>
                  <View style={{ flexDirection: 'row', flex: 1, alignItems: 'center', gap: 24 }}>
                    <CustomRadio label="WT" selected={purchaseType === 'W'} onPress={() => setPurchaseType('W')} />
                    <CustomRadio label="PCS" selected={purchaseType === 'P'} onPress={() => setPurchaseType('P')} />
                    <CustomRadio label="Both" selected={purchaseType === 'B'} onPress={() => setPurchaseType('B')} />
                  </View>
                </View>
              </View>

              <View style={{ flexDirection: 'row', marginBottom: 14, alignItems: 'center' }}>
                <View style={{ flex: 1, flexDirection: 'row', alignItems: 'center' }}>
                  <ThemedText style={[styles.inputLabel, { color: subtitleColor, width: 90, marginBottom: 0 }]}>Gold Loss</ThemedText>
                  <CustomCheckbox label="" selected={goldLoss} onPress={() => setGoldLoss(!goldLoss)} />
                </View>
                <View style={{ flex: 1, flexDirection: 'row', alignItems: 'center' }}>
                  <ThemedText style={[styles.inputLabel, { color: subtitleColor, width: 90, marginBottom: 0 }]}>Display Cost</ThemedText>
                  <CustomCheckbox label="" selected={displayCost} onPress={() => setDisplayCost(!displayCost)} />
                </View>
                <View style={{ flex: 1, flexDirection: 'row', alignItems: 'center' }}>
                  <ThemedText style={[styles.inputLabel, { color: subtitleColor, width: 100, marginBottom: 0 }]}>Gold Bar Code</ThemedText>
                  <CustomCheckbox label="" selected={goldBarCode} onPress={() => setGoldBarCode(!goldBarCode)} />
                </View>
              </View>

              <View style={{ flexDirection: 'row', marginBottom: 12 }}>
                <View style={{ flex: 1, flexDirection: 'row', alignItems: 'center', paddingRight: 10 }}>
                  <ThemedText style={[styles.inputLabel, { color: subtitleColor, width: 90, marginBottom: 0 }]}>Rate</ThemedText>
                  <TextInput
                    style={[styles.input, { backgroundColor: inputBg, color: textColor, borderColor, height: 40, flex: 1 }]}
                    value={rate}
                    onChangeText={setRate}
                    placeholder="0.00"
                    placeholderTextColor={subtitleColor}
                    keyboardType="numeric"
                  />
                </View>
                <View style={{ flex: 1, flexDirection: 'row', alignItems: 'center', paddingLeft: 10 }}>
                  <ThemedText style={[styles.inputLabel, { color: subtitleColor, width: 90, marginBottom: 0 }]}>MC Discount</ThemedText>
                  <TextInput
                    style={[styles.input, { backgroundColor: inputBg, color: textColor, borderColor, height: 40, flex: 1 }]}
                    value={mcDiscount}
                    onChangeText={setMcDiscount}
                    placeholder="0.00"
                    placeholderTextColor={subtitleColor}
                    keyboardType="numeric"
                  />
                </View>
              </View>

              <View style={{ flexDirection: 'row', marginBottom: 12 }}>
                <View style={{ flex: 1, flexDirection: 'row', alignItems: 'center' }}>
                  <ThemedText style={[styles.inputLabel, { color: subtitleColor, width: 90, marginBottom: 0 }]}>Remarks</ThemedText>
                  <TextInput
                    style={[styles.input, { backgroundColor: inputBg, color: textColor, borderColor, height: 40, flex: 1 }]}
                    value={remarks}
                    onChangeText={setRemarks}
                    placeholder="Enter Remarks"
                    placeholderTextColor={subtitleColor}
                  />
                </View>
              </View>
            </ScrollView>

            <View style={styles.modalFooter}>
              <TouchableOpacity style={styles.cancelBtn} onPress={() => setModalVisible(false)}>
                <Text style={[styles.cancelBtnText, { color: subtitleColor }]}>Cancel</Text>
              </TouchableOpacity>
              <TouchableOpacity style={[styles.saveBtn, { backgroundColor: '#D4AF37' }]} onPress={handleSave} disabled={saving}>
                {saving ? <ActivityIndicator size="small" color="#FFF" /> : <Text style={styles.saveBtnText}>Save</Text>}
              </TouchableOpacity>
            </View>
          </View>
        </View>
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
  modalOverlay: { flex: 1, backgroundColor: 'rgba(0,0,0,0.5)', justifyContent: 'center', alignItems: 'center', padding: 20 },
  modalContent: { width: '100%', maxWidth: 820, borderRadius: 16, borderWidth: 1, overflow: 'hidden', shadowOffset: { width: 0, height: 10 }, shadowOpacity: 0.25, shadowRadius: 10, elevation: 5, maxHeight: '90%' },
  modalHeader: { flexDirection: 'row', justifyContent: 'space-between', alignItems: 'center', padding: 20, borderBottomWidth: 1, borderBottomColor: 'rgba(150,150,150,0.1)' },
  modalTitle: { fontSize: 18, fontWeight: '600' },
  modalBody: { padding: 20 },
  inputLabel: { fontSize: 13, fontWeight: '500', marginBottom: 8 },
  input: { borderWidth: 1, borderRadius: 8, paddingHorizontal: 16, height: 48, fontSize: 15 },
  modalFooter: { flexDirection: 'row', justifyContent: 'flex-end', padding: 20, borderTopWidth: 1, borderTopColor: 'rgba(150,150,150,0.1)' },
  cancelBtn: { paddingHorizontal: 20, paddingVertical: 12, justifyContent: 'center' },
  cancelBtnText: { fontSize: 15, fontWeight: '500' },
  saveBtn: { paddingHorizontal: 24, paddingVertical: 12, borderRadius: 8, justifyContent: 'center', alignItems: 'center', minWidth: 100 },
  saveBtnText: {  fontSize: 15, fontWeight: '600' },
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
