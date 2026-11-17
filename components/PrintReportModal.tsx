import React from 'react';
import {
  Modal,
  View,
  TouchableOpacity,
  TouchableWithoutFeedback,
  StyleSheet,
  ActivityIndicator,
} from 'react-native';
import { ThemedText } from './themed-text';
import { Ionicons } from '@expo/vector-icons';

export interface PrintReportModalProps {
  visible: boolean;
  title?: string;
  message?: string;
  loading?: boolean;
  onCancel: () => void;
  onPdf: () => void;
  onExcel: () => void;
  onPrint: () => void;
}

export const PrintReportModal: React.FC<PrintReportModalProps> = ({
  visible,
  title = 'Print Report',
  message = 'Do you want to print ?',
  loading = false,
  onCancel,
  onPdf,
  onExcel,
  onPrint,
}) => {
  return (
    <Modal
      visible={visible}
      transparent
      animationType="fade"
      onRequestClose={onCancel}
    >
      <TouchableOpacity
        style={styles.modalOverlay}
        activeOpacity={1}
        onPress={onCancel}
      >
        <TouchableWithoutFeedback>
          <View style={styles.modalCard}>
            {/* Modal Header */}
            <View style={styles.modalHeader}>
              <View style={styles.headerTitleGroup}>
                <View style={styles.headerIconBadge}>
                  <Ionicons name="print-outline" size={14} color="#B48218" />
                </View>
                <ThemedText style={styles.modalTitle}>{title}</ThemedText>
              </View>
              <TouchableOpacity
                onPress={onCancel}
                style={styles.closeBtn}
                hitSlop={{ top: 8, bottom: 8, left: 8, right: 8 }}
              >
                <Ionicons name="close" size={18} color="#3B2D17" />
              </TouchableOpacity>
            </View>

            {/* Modal Body */}
            <View style={styles.modalBody}>
              <Ionicons
                name="print-outline"
                size={38}
                color="#8D6409"
                style={styles.bodyIcon}
              />
              <ThemedText style={styles.bodyMessage}>{message}</ThemedText>

              {/* Action Buttons Row */}
              <View style={styles.actionRow}>
                <TouchableOpacity
                  style={[styles.btnSecondary, { flex: 1 }]}
                  onPress={onCancel}
                  activeOpacity={0.7}
                  disabled={loading}
                >
                  <ThemedText style={styles.btnSecondaryText}>Cancel</ThemedText>
                </TouchableOpacity>

                <TouchableOpacity
                  style={[styles.btnSecondary, { flex: 1 }]}
                  onPress={onPdf}
                  activeOpacity={0.7}
                  disabled={loading}
                >
                  <ThemedText style={styles.btnSecondaryText}>PDF</ThemedText>
                </TouchableOpacity>

                <TouchableOpacity
                  style={[styles.btnSecondary, { flex: 1 }]}
                  onPress={onExcel}
                  activeOpacity={0.7}
                  disabled={loading}
                >
                  <ThemedText style={styles.btnSecondaryText}>Excel</ThemedText>
                </TouchableOpacity>

                <TouchableOpacity
                  style={[styles.btnPrimary, { flex: 1.2 }]}
                  onPress={onPrint}
                  activeOpacity={0.7}
                  disabled={loading}
                >
                  {loading ? (
                    <ActivityIndicator size="small" color="#9E6D08" />
                  ) : (
                    <ThemedText style={styles.btnPrimaryText}>Print</ThemedText>
                  )}
                </TouchableOpacity>
              </View>
            </View>
          </View>
        </TouchableWithoutFeedback>
      </TouchableOpacity>
    </Modal>
  );
};

const styles = StyleSheet.create({
  modalOverlay: {
    flex: 1,
    backgroundColor: 'rgba(0,0,0,0.4)',
    justifyContent: 'center',
    alignItems: 'center',
    padding: 20,
  },
  modalCard: {
    width: '100%',
    maxWidth: 320,
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E2D3B3',
    overflow: 'hidden',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.15,
    shadowRadius: 10,
    elevation: 6,
  },
  modalHeader: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    paddingHorizontal: 12,
    paddingVertical: 8,
    backgroundColor: '#F5E7CA',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
  },
  headerTitleGroup: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  headerIconBadge: {
    width: 22,
    height: 22,
    borderRadius: 5,
    backgroundColor: '#FFF8EA',
    borderWidth: 1,
    borderColor: '#E6D3AF',
    justifyContent: 'center',
    alignItems: 'center',
    marginRight: 6,
  },
  modalTitle: {
    fontSize: 13,
    fontWeight: 'bold',
    color: '#3B2D17',
  },
  closeBtn: {
    padding: 4,
  },
  modalBody: {
    padding: 18,
    alignItems: 'center',
  },
  bodyIcon: {
    marginBottom: 12,
  },
  bodyMessage: {
    fontSize: 13,
    color: '#3B2D17',
    textAlign: 'center',
    marginBottom: 16,
    fontWeight: '500',
  },
  actionRow: {
    flexDirection: 'row',
    gap: 8,
    width: '100%',
  },
  btnSecondary: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#DFD3BC',
    borderRadius: 6,
    paddingHorizontal: 8,
    paddingVertical: 7,
  },
  btnSecondaryText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#554428',
  },
  btnPrimary: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    backgroundColor: '#F5E7CA',
    borderWidth: 1,
    borderColor: '#D4C3A3',
    borderRadius: 6,
    paddingHorizontal: 10,
    paddingVertical: 7,
  },
  btnPrimaryText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#9E6D08',
  },
});
