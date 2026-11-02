import { Ionicons } from '@expo/vector-icons';
import { Stack, useRouter } from 'expo-router';
import React, { useEffect, useState, useCallback } from 'react';
import {
  ActivityIndicator,
  FlatList,
  Modal,
  Platform,
  StyleSheet,
  TextInput,
  TouchableOpacity,
  TouchableWithoutFeedback,
  View,
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';
import { ThemedText } from '@/components/themed-text';
import { API_BASE_URL } from '@/constants/api';

interface Printer {
  printerId: number;
  printerName: string;
  printerType: string;
  description: string;
  isDefault: boolean;
  isActive: boolean;
}

const API_URL = `${API_BASE_URL}/printer`;

// Exact Color Palette matching Direct Purchase Theme
const COLORS = {
  bg: '#FBF9F5',
  cardBg: '#FFFFFF',
  cardBorder: '#E8DCB8',
  headerBanner: '#F5E7CA',
  headerBannerBorder: '#E2D3B3',
  headerIconBadgeBg: '#FFF8EA',
  headerIconBadgeBorder: '#E2D2AF',
  primaryGold: '#9E6D08',
  darkText: '#3B2D17',
  secondaryText: '#8F7E66',
  cellText: '#332717',
  border: '#DFD5C3',
  rowBorder: '#F0E6D2',
  selectedRow: '#FAF7F0',
  successGreen: '#2E7D32',
};

export default function ChangePrinterScreen() {
  const router = useRouter();

  // State Management
  const [currentPrinter, setCurrentPrinter] = useState<Printer | null>(null);
  const [availablePrinters, setAvailablePrinters] = useState<Printer[]>([]);
  const [selectedPrinter, setSelectedPrinter] = useState<Printer | null>(null);

  const [loading, setLoading] = useState<boolean>(true);
  const [saving, setSaving] = useState<boolean>(false);
  const [toast, setToast] = useState<{ message: string; type: 'success' | 'error' } | null>(null);

  // Add Printer Modal State
  const [isAddModalVisible, setIsAddModalVisible] = useState<boolean>(false);
  const [newPrinterName, setNewPrinterName] = useState<string>('');
  const [newPrinterType, setNewPrinterType] = useState<string>('');
  const [newDescription, setNewDescription] = useState<string>('');
  const [adding, setAdding] = useState<boolean>(false);

  const showToast = (message: string, type: 'success' | 'error' = 'success') => {
    setToast({ message, type });
    setTimeout(() => {
      setToast(null);
    }, 3500);
  };

  // Fetch Current Default Printer and Printer List
  const loadPrinterData = useCallback(async () => {
    setLoading(true);
    try {
      // 1. Fetch Current Default Printer
      const defRes = await fetch(`${API_URL}/default?t=${Date.now()}`, {
        headers: { 'Cache-Control': 'no-cache' },
      });
      const defJson = await defRes.json();
      if (defJson.success) {
        setCurrentPrinter(defJson.data || null);
      }

      // 2. Fetch All Available Printers
      const listRes = await fetch(`${API_URL}/list?t=${Date.now()}`, {
        headers: { 'Cache-Control': 'no-cache' },
      });
      const listJson = await listRes.json();

      if (listJson.success && Array.isArray(listJson.data)) {
        setAvailablePrinters(listJson.data);

        // Pre-select current default printer, or first printer if none set
        setSelectedPrinter((prev) => {
          if (prev) {
            const found = listJson.data.find((p: Printer) => p.printerId === prev.printerId);
            if (found) return found;
          }
          return listJson.data.find((p: Printer) => p.isDefault) || listJson.data[0] || null;
        });
      } else {
        setAvailablePrinters([]);
        showToast(listJson.message || 'No printers available.', 'error');
      }
    } catch (error) {
      console.error('Error loading printer data:', error);
      showToast('Network error loading printer settings.', 'error');
    } finally {
      setLoading(false);
    }
  }, []);

  useEffect(() => {
    loadPrinterData();
  }, [loadPrinterData]);

  // Handle ESC key on Web
  useEffect(() => {
    if (Platform.OS === 'web') {
      const handleKeyDown = (e: KeyboardEvent) => {
        if (e.key === 'Escape') {
          if (isAddModalVisible) {
            setIsAddModalVisible(false);
          } else {
            router.back();
          }
        }
      };
      window.addEventListener('keydown', handleKeyDown);
      return () => window.removeEventListener('keydown', handleKeyDown);
    }
  }, [isAddModalVisible, router]);

  // Save / Update Default Printer
  const handleSetAsDefault = async () => {
    if (!selectedPrinter) {
      showToast('Please select a printer from the list.', 'error');
      return;
    }

    setSaving(true);
    try {
      const response = await fetch(`${API_URL}/default`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ printerId: selectedPrinter.printerId }),
      });

      const json = await response.json();
      if (json.success) {
        showToast('Printer changed successfully.', 'success');
        // Reload printer data to update default statuses
        await loadPrinterData();
      } else {
        showToast(json.message || 'Failed to update default printer.', 'error');
      }
    } catch (error) {
      console.error('Set Default Printer Error:', error);
      showToast('Failed to connect to backend server.', 'error');
    } finally {
      setSaving(false);
    }
  };

  // Open Add Printer Modal
  const openAddModal = () => {
    setNewPrinterName('');
    setNewPrinterType('');
    setNewDescription('');
    setIsAddModalVisible(true);
  };

  // Save New Printer
  const handleSaveNewPrinter = async () => {
    if (!newPrinterName.trim()) {
      showToast('Please enter printer name.', 'error');
      return;
    }

    if (!newPrinterType.trim()) {
      showToast('Please enter printer type.', 'error');
      return;
    }

    setAdding(true);
    try {
      const response = await fetch(API_URL, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          printerName: newPrinterName.trim(),
          printerType: newPrinterType.trim(),
          description: newDescription.trim(),
        }),
      });

      const json = await response.json();
      if (json.success) {
        showToast('Printer added successfully.', 'success');
        setIsAddModalVisible(false);
        // Reload list automatically
        await loadPrinterData();
      } else {
        showToast(json.message || 'Failed to add printer.', 'error');
      }
    } catch (error) {
      console.error('Add Printer Error:', error);
      showToast('Failed to connect to backend server.', 'error');
    } finally {
      setAdding(false);
    }
  };

  const renderTableRow = ({ item }: { item: Printer }) => {
    const isSelected = selectedPrinter?.printerId === item.printerId;

    return (
      <TouchableOpacity
        style={[
          styles.tableRow,
          isSelected ? styles.selectedRow : styles.normalRow,
        ]}
        onPress={() => setSelectedPrinter(item)}
        activeOpacity={0.8}
      >
        {/* Column 1: SELECT */}
        <View style={[styles.colSelect, styles.cell]}>
          <Ionicons
            name={isSelected ? 'radio-button-on' : 'radio-button-off'}
            size={18}
            color={isSelected ? '#9E6D08' : '#C4B59D'}
          />
        </View>

        {/* Column 2: PRINTER NAME */}
        <View style={[styles.colPrinterName, styles.cell]}>
          <ThemedText numberOfLines={1} style={[styles.rowText, styles.printerNameText]}>
            {item.printerName}
          </ThemedText>
        </View>

        {/* Column 3: TYPE */}
        <View style={[styles.colType, styles.cell]}>
          <ThemedText numberOfLines={1} style={styles.rowText}>
            {item.printerType}
          </ThemedText>
        </View>

        {/* Column 4: DESCRIPTION */}
        <View style={[styles.colDesc, styles.cell]}>
          <ThemedText numberOfLines={1} style={styles.rowText}>
            {item.description}
          </ThemedText>
        </View>
      </TouchableOpacity>
    );
  };

  return (
    <SafeAreaView style={styles.safeArea} edges={['top']}>
      <Stack.Screen options={{ headerShown: false }} />

      {/* Floating Toast Alert */}
      {toast && (
        <View
          style={[
            styles.toast,
            { backgroundColor: toast.type === 'success' ? '#2E7D32' : '#B91C1C' },
          ]}
        >
          <Ionicons
            name={toast.type === 'success' ? 'checkmark-circle-outline' : 'alert-circle-outline'}
            size={20}
            color="#FFF"
            style={{ marginRight: 8 }}
          />
          <ThemedText style={styles.toastText}>{toast.message}</ThemedText>
        </View>
      )}

      {/* HEADER */}
      <View style={styles.header}>
        <View style={styles.headerLeft}>
          <TouchableOpacity style={styles.backButton} onPress={() => router.back()}>
            <Ionicons name="arrow-back" size={18} color="#3B2D17" />
          </TouchableOpacity>
          <ThemedText style={styles.headerTitle}>Change Printer</ThemedText>
        </View>

        <TouchableOpacity style={styles.newButton} onPress={openAddModal} activeOpacity={0.8}>
          <Ionicons name="add" size={16} color="#FFFFFF" style={{ marginRight: 2 }} />
          <ThemedText style={styles.newButtonText}>New</ThemedText>
        </TouchableOpacity>
      </View>

      {/* MAIN CONTENT WRAPPER */}
      <View style={styles.container}>
        {/* CURRENT DEFAULT PRINTER CARD */}
        <View style={styles.currentCard}>
          {/* Section Header Banner matching Direct Purchase */}
          <View style={styles.cardHeaderBanner}>
            <View style={styles.headerIconBadge}>
              <Ionicons name="document-text-outline" size={14} color="#9E6D08" />
            </View>
            <ThemedText style={styles.cardHeaderTitle}>Default Printer</ThemedText>
          </View>

          <View style={styles.cardBodyContent}>
            <View style={styles.currentCardLeft}>
              <View style={styles.currentDetailsBox}>
                {loading ? (
                  <ActivityIndicator size="small" color="#9E6D08" style={{ alignSelf: 'flex-start', marginTop: 2 }} />
                ) : currentPrinter ? (
                  <>
                    <ThemedText style={styles.currentPrinterName}>
                      {currentPrinter.printerName}
                    </ThemedText>
                    <ThemedText style={styles.currentPrinterType}>
                      Type : {currentPrinter.printerType || 'LAN Printer'}
                    </ThemedText>
                  </>
                ) : (
                  <ThemedText style={styles.noDefaultText}>
                    No default printer currently set.
                  </ThemedText>
                )}
              </View>
            </View>

            {/* Right Section: Status & Description */}
            {currentPrinter && !loading && (
              <View style={styles.currentCardRight}>
                {/* Status Section */}
                <View style={styles.infoCol}>
                  <ThemedText style={styles.infoColLabel}>Status</ThemedText>
                  <View style={styles.statusBadgeRow}>
                    <View style={styles.greenDot} />
                    <ThemedText style={styles.statusActiveText}>Active</ThemedText>
                  </View>
                </View>

                {/* Vertical Divider */}
                <View style={styles.verticalDivider} />

                {/* Description Section */}
                <View style={styles.infoCol}>
                  <ThemedText style={styles.infoColLabel}>Description</ThemedText>
                  <ThemedText style={styles.descValueText}>
                    {currentPrinter.description || 'Receipt Printer'}
                  </ThemedText>
                </View>
              </View>
            )}
          </View>
        </View>

        {/* PRINTER TABLE CONTAINER */}
        <View style={styles.tableCard}>
          {/* Table Header */}
          <View style={styles.tableHeader}>
            <View style={[styles.colSelect, styles.headerCell]}>
              <ThemedText style={styles.headerLabel}>SELECT</ThemedText>
            </View>
            <View style={[styles.colPrinterName, styles.headerCell]}>
              <ThemedText style={styles.headerLabel}>PRINTER NAME</ThemedText>
            </View>
            <View style={[styles.colType, styles.headerCell]}>
              <ThemedText style={styles.headerLabel}>TYPE</ThemedText>
            </View>
            <View style={[styles.colDesc, styles.headerCell]}>
              <ThemedText style={styles.headerLabel}>DESCRIPTION</ThemedText>
            </View>
          </View>

          {/* Table Content */}
          {loading ? (
            <View style={styles.centeredLoading}>
              <ActivityIndicator size="large" color="#9E6D08" />
              <ThemedText style={styles.loadingText}>Loading available printers...</ThemedText>
            </View>
          ) : availablePrinters.length === 0 ? (
            <View style={styles.emptyContainer}>
              <ThemedText style={styles.emptyText}>No printers available.</ThemedText>
            </View>
          ) : (
            <FlatList
              data={availablePrinters}
              renderItem={renderTableRow}
              keyExtractor={(item) => item.printerId.toString()}
              contentContainerStyle={{ paddingBottom: 2 }}
              style={{ flex: 1 }}
            />
          )}
        </View>

        {/* BOTTOM ACTION AREA */}
        <View style={styles.bottomSection}>
          <View style={styles.buttonGroup}>
            <TouchableOpacity
              style={[
                styles.primaryButton,
                saving && { opacity: 0.7 },
              ]}
              onPress={handleSetAsDefault}
              disabled={saving}
              activeOpacity={0.8}
            >
              {saving ? (
                <ActivityIndicator size="small" color="#FFFFFF" />
              ) : (
                <ThemedText style={styles.primaryButtonText}>Set as Default</ThemedText>
              )}
            </TouchableOpacity>

            <TouchableOpacity
              style={styles.secondaryButton}
              onPress={() => router.back()}
              activeOpacity={0.8}
            >
              <ThemedText style={styles.secondaryButtonText}>Close</ThemedText>
            </TouchableOpacity>
          </View>
        </View>
      </View>

      {/* ADD PRINTER MODAL */}
      <Modal visible={isAddModalVisible} transparent animationType="fade">
        <TouchableOpacity
          style={styles.modalOverlay}
          activeOpacity={1}
          onPress={() => setIsAddModalVisible(false)}
        >
          <TouchableWithoutFeedback>
            <View style={styles.modalContent}>
              {/* Modal Header */}
              <View style={styles.modalHeader}>
                <ThemedText style={styles.modalTitle}>Add Printer</ThemedText>
                <TouchableOpacity onPress={() => setIsAddModalVisible(false)}>
                  <Ionicons name="close" size={22} color={COLORS.darkText} />
                </TouchableOpacity>
              </View>

              {/* Modal Body */}
              <View style={styles.modalBody}>
                {/* Printer Name */}
                <View style={styles.inputGroup}>
                  <ThemedText style={styles.inputLabel}>Printer Name *</ThemedText>
                  <TextInput
                    style={styles.textInput}
                    value={newPrinterName}
                    onChangeText={setNewPrinterName}
                    placeholder="Enter printer name"
                    placeholderTextColor={COLORS.secondaryText}
                  />
                </View>

                {/* Printer Type */}
                <View style={styles.inputGroup}>
                  <ThemedText style={styles.inputLabel}>Printer Type *</ThemedText>
                  <TextInput
                    style={styles.textInput}
                    value={newPrinterType}
                    onChangeText={setNewPrinterType}
                    placeholder="Enter printer type (e.g. LAN, USB, NETWORK)"
                    placeholderTextColor={COLORS.secondaryText}
                  />
                </View>

                {/* Description */}
                <View style={styles.inputGroup}>
                  <ThemedText style={styles.inputLabel}>Description</ThemedText>
                  <TextInput
                    style={styles.textInput}
                    value={newDescription}
                    onChangeText={setNewDescription}
                    placeholder="Enter description (e.g. Receipt Printer)"
                    placeholderTextColor={COLORS.secondaryText}
                  />
                </View>
              </View>

              {/* Modal Footer */}
              <View style={styles.modalFooter}>
                <TouchableOpacity
                  style={[styles.modalBtn, styles.modalCloseBtn]}
                  onPress={() => setIsAddModalVisible(false)}
                  disabled={adding}
                >
                  <ThemedText style={styles.modalCloseBtnText}>Close</ThemedText>
                </TouchableOpacity>

                <TouchableOpacity
                  style={[styles.modalBtn, styles.modalSaveBtn, adding && { opacity: 0.7 }]}
                  onPress={handleSaveNewPrinter}
                  disabled={adding}
                >
                  {adding ? (
                    <ActivityIndicator size="small" color={COLORS.darkText} />
                  ) : (
                    <ThemedText style={styles.modalSaveBtnText}>Save</ThemedText>
                  )}
                </TouchableOpacity>
              </View>
            </View>
          </TouchableWithoutFeedback>
        </TouchableOpacity>
      </Modal>
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  safeArea: {
    flex: 1,
    backgroundColor: '#FBF9F5',
  },
  header: {
    height: 52,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingHorizontal: 16,
    backgroundColor: '#FFFFFF',
    borderBottomWidth: 1,
    borderBottomColor: '#E8DCB8',
  },
  headerLeft: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  backButton: {
    width: 34,
    height: 34,
    borderRadius: 6,
    borderWidth: 1,
    borderColor: '#DFD5C3',
    backgroundColor: '#FFFFFF',
    alignItems: 'center',
    justifyContent: 'center',
    marginRight: 10,
  },
  headerTitle: {
    fontSize: 18,
    fontWeight: 'bold',
    color: '#3B2D17',
    fontFamily: Platform.OS === 'web' ? 'Cambria, Georgia, serif' : undefined,
  },
  newButton: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 14,
    height: 34,
    borderRadius: 6,
    backgroundColor: '#9E6D08',
  },
  newButtonText: {
    color: '#FFFFFF',
    fontWeight: 'bold',
    fontSize: 12.5,
  },
  container: {
    flex: 1,
    padding: 14,
    backgroundColor: '#FBF9F5',
    gap: 12,
  },
  // CURRENT DEFAULT PRINTER CARD (matching Direct Purchase goldCard + cardHeaderBanner)
  currentCard: {
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#E8DCB8',
    borderRadius: 8,
    overflow: 'hidden',
  },
  cardHeaderBanner: {
    backgroundColor: '#F5E7CA',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    paddingVertical: 7,
    paddingHorizontal: 12,
    flexDirection: 'row',
    alignItems: 'center',
  },
  headerIconBadge: {
    width: 24,
    height: 24,
    borderRadius: 5,
    backgroundColor: '#FFF8EA',
    borderWidth: 1,
    borderColor: '#E2D2AF',
    justifyContent: 'center',
    alignItems: 'center',
    marginRight: 8,
  },
  cardHeaderTitle: {
    fontSize: 13,
    fontWeight: 'bold',
    color: '#3B2D17',
    letterSpacing: 0.3,
  },
  cardBodyContent: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    paddingHorizontal: 16,
    paddingVertical: 12,
  },
  currentCardLeft: {
    flexDirection: 'row',
    alignItems: 'center',
    flex: 1,
  },
  currentDetailsBox: {
    justifyContent: 'center',
  },
  currentPrinterName: {
    fontSize: 15,
    fontWeight: 'bold',
    color: '#3B2D17',
  },
  currentPrinterType: {
    fontSize: 12,
    color: '#8F7E66',
    marginTop: 2,
  },
  noDefaultText: {
    fontSize: 12.5,
    color: '#8F7E66',
    marginTop: 2,
    fontStyle: 'italic',
  },
  currentCardRight: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  infoCol: {
    paddingHorizontal: 16,
    justifyContent: 'center',
  },
  infoColLabel: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#8F7E66',
    marginBottom: 4,
    textTransform: 'uppercase',
  },
  statusBadgeRow: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  greenDot: {
    width: 8,
    height: 8,
    borderRadius: 4,
    backgroundColor: '#2E7D32',
    marginRight: 6,
  },
  statusActiveText: {
    fontSize: 13,
    fontWeight: 'bold',
    color: '#2E7D32',
  },
  descValueText: {
    fontSize: 13,
    fontWeight: 'bold',
    color: '#3B2D17',
  },
  verticalDivider: {
    width: 1,
    height: 32,
    backgroundColor: '#EADBCA',
  },
  // PRINTER TABLE STYLES (matching Direct Purchase Line Details table)
  tableCard: {
    flex: 1,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    borderRadius: 8,
    backgroundColor: '#FFFFFF',
    overflow: 'hidden',
  },
  tableHeader: {
    minHeight: 36,
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#F5E7CA',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
  },
  tableRow: {
    minHeight: 36,
    flexDirection: 'row',
    alignItems: 'center',
    borderBottomWidth: 1,
    borderBottomColor: '#F0E6D2',
  },
  headerCell: {
    justifyContent: 'center',
    paddingVertical: 6,
  },
  headerLabel: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3B2D17',
    letterSpacing: 0.3,
  },
  normalRow: {
    backgroundColor: '#FFFFFF',
  },
  selectedRow: {
    backgroundColor: '#FAF7F0',
  },
  cell: {
    justifyContent: 'center',
    paddingVertical: 4,
  },
  colSelect: {
    width: 90,
    alignItems: 'center',
    justifyContent: 'center',
    borderRightWidth: 1,
    borderRightColor: '#F0E6D2',
  },
  colPrinterName: {
    flex: 1.2,
    justifyContent: 'center',
    paddingHorizontal: 12,
    borderRightWidth: 1,
    borderRightColor: '#F0E6D2',
  },
  colType: {
    flex: 1.5,
    justifyContent: 'center',
    paddingHorizontal: 12,
    borderRightWidth: 1,
    borderRightColor: '#F0E6D2',
  },
  colDesc: {
    flex: 1,
    justifyContent: 'center',
    paddingHorizontal: 12,
  },
  rowText: {
    fontSize: 12,
    color: '#332717',
  },
  printerNameText: {
    fontWeight: 'bold',
    color: '#3B2D17',
  },
  centeredLoading: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    padding: 20,
  },
  loadingText: {
    marginTop: 8,
    color: '#8F7E66',
    fontSize: 12.5,
  },
  emptyContainer: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    padding: 20,
  },
  emptyText: {
    marginTop: 8,
    fontSize: 12.5,
    color: '#8F7E66',
    fontWeight: '500',
  },
  // BOTTOM ACTION AREA
  bottomSection: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'flex-end',
    height: 44,
  },
  buttonGroup: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 10,
  },
  primaryButton: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    backgroundColor: '#9E6D08',
    paddingHorizontal: 20,
    height: 38,
    borderRadius: 6,
  },
  primaryButtonText: {
    color: '#FFFFFF',
    fontWeight: 'bold',
    fontSize: 13,
  },
  secondaryButton: {
    alignItems: 'center',
    justifyContent: 'center',
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#DFD5C3',
    paddingHorizontal: 22,
    height: 38,
    borderRadius: 6,
  },
  secondaryButtonText: {
    color: '#3B2D17',
    fontWeight: 'bold',
    fontSize: 13,
  },
  toast: {
    position: 'absolute',
    top: 60,
    alignSelf: 'center',
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 16,
    paddingVertical: 10,
    borderRadius: 6,
    zIndex: 9999,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.15,
    shadowRadius: 4,
    elevation: 6,
  },
  toastText: {
    color: '#FFF',
    fontWeight: 'bold',
    fontSize: 13,
  },
  // ADD PRINTER MODAL STYLES
  modalOverlay: {
    flex: 1,
    backgroundColor: 'rgba(0,0,0,0.4)',
    justifyContent: 'center',
    alignItems: 'center',
    padding: 20,
  },
  modalContent: {
    width: '100%',
    maxWidth: 460,
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.1,
    shadowRadius: 8,
    elevation: 8,
    overflow: 'hidden',
  },
  modalHeader: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    paddingHorizontal: 14,
    paddingVertical: 10,
    backgroundColor: '#F5E7CA',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
  },
  modalTitle: {
    fontSize: 14,
    fontWeight: 'bold',
    color: '#3B2D17',
  },
  modalBody: {
    padding: 18,
  },
  inputGroup: {
    marginBottom: 12,
  },
  inputLabel: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3B2D17',
    marginBottom: 5,
    textTransform: 'uppercase',
  },
  textInput: {
    height: 38,
    borderWidth: 1,
    borderColor: '#DFD5C3',
    borderRadius: 6,
    paddingHorizontal: 12,
    fontSize: 12.5,
    color: '#3B2D17',
    backgroundColor: '#FFFFFF',
    ...(Platform.OS === 'web' ? ({ outlineStyle: 'none' } as any) : {}),
  },
  modalFooter: {
    flexDirection: 'row',
    justifyContent: 'flex-end',
    gap: 10,
    paddingHorizontal: 16,
    paddingVertical: 12,
    borderTopWidth: 1,
    borderTopColor: '#EADBCA',
    backgroundColor: '#FAF7F0',
  },
  modalBtn: {
    height: 34,
    paddingHorizontal: 16,
    borderRadius: 6,
    justifyContent: 'center',
    alignItems: 'center',
    minWidth: 80,
  },
  modalCloseBtn: {
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#DFD5C3',
  },
  modalCloseBtnText: {
    color: '#3B2D17',
    fontWeight: 'bold',
    fontSize: 12.5,
  },
  modalSaveBtn: {
    backgroundColor: '#9E6D08',
  },
  modalSaveBtnText: {
    color: '#FFFFFF',
    fontWeight: 'bold',
    fontSize: 12.5,
  },
});
