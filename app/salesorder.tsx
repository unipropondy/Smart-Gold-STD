import { ThemedText } from '@/components/themed-text';
import { API_BASE_URL } from '@/constants/api';
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
  View
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';

const API_URL = `${API_BASE_URL}/salesorder`;

interface SOItem {
  LocationCode: string;
  CategoryCode: string;
  CatalogueCode: string;
  Weight: number;
  ToWeight: number;
  SizeCode: number;
  SizeType: string;
  ToSizeCode?: number;
  ToSizeType?: string;
  Qty: number;
  ReOrderLevel: number;
  SalesOrderQty: string;
  isSelected?: boolean;
}

export default function SalesOrderScreen() {
  const router = useRouter();

  const [loading, setLoading] = useState(false);
  const [saving, setSaving] = useState(false);

  // Header State
  const [soNo, setSoNo] = useState('');
  const [locationCode, setLocationCode] = useState('HQ');
  const [supplierCode, setSupplierCode] = useState('');
  const [category, setCategory] = useState('');
  const [catalogue, setCatalogue] = useState('');
  const [remarks, setRemarks] = useState('');

  // Due Date (DD-MM-YYYY)
  const [dueDate, setDueDate] = useState('');

  // Table items state
  const [items, setItems] = useState<SOItem[]>([]);

  // Search/Lookup Modal
  const [lookupType, setLookupType] = useState<'salesorder' | 'location' | 'supplier' | 'category' | 'catalogue' | null>(null);
  const [lookupSearch, setLookupSearch] = useState('');
  const [locations, setLocations] = useState<any[]>([]);
  const [suppliers, setSuppliers] = useState<any[]>([]);
  const [categories, setCategories] = useState<any[]>([]);
  const [catalogues, setCatalogues] = useState<any[]>([]);
  const [salesOrders, setSalesOrders] = useState<any[]>([]);

  // Toast Notification State
  const [toast, setToast] = useState<{ message: string; type: 'success' | 'error' } | null>(null);

  const showToast = (message: string, type: 'success' | 'error' = 'success') => {
    setToast({ message, type });
    setTimeout(() => {
      setToast(null);
    }, 3000);
  };

  const fetchLookups = async () => {
    try {
      const locRes = await fetch(`${API_BASE_URL}/location?t=${Date.now()}`);
      if (locRes.ok) {
        const json = await locRes.json();
        if (json.success) setLocations(json.data);
      }
      const supRes = await fetch(`${API_BASE_URL}/supplier?t=${Date.now()}`);
      if (supRes.ok) {
        const json = await supRes.json();
        if (json.success) setSuppliers(json.data);
      }
      const catRes = await fetch(`${API_BASE_URL}/category?t=${Date.now()}`);
      if (catRes.ok) {
        const json = await catRes.json();
        if (json.success) setCategories(json.data);
      }
      const ctgRes = await fetch(`${API_BASE_URL}/catalogue?t=${Date.now()}`);
      if (ctgRes.ok) {
        const json = await ctgRes.json();
        if (json.success) setCatalogues(json.data);
      }
    } catch (e) {
      console.error('Lookups load failed', e);
    }
  };

  const loadNextSONo = async () => {
    try {
      const res = await fetch(`${API_URL}/nextNo?t=${Date.now()}`);
      const json = await res.json();
      if (json.success) {
        setSoNo(json.nextNo);
      }
    } catch (e) {
      console.error('Failed to load next SO no', e);
    }
  };

  useEffect(() => {
    fetchLookups();
    loadNextSONo();

    // Default due date to current date + 7 days
    const defaultDate = new Date();
    defaultDate.setDate(defaultDate.getDate() + 7);
    const dd = String(defaultDate.getDate()).padStart(2, '0');
    const mm = String(defaultDate.getMonth() + 1).padStart(2, '0');
    const yyyy = String(defaultDate.getFullYear());
    setDueDate(`${yyyy}-${mm}-${dd}`);
  }, []);

  const openLookup = async (type: 'salesorder' | 'location' | 'supplier' | 'category' | 'catalogue') => {
    setLookupSearch('');
    setLookupType(type);
    if (type === 'salesorder') {
      try {
        const res = await fetch(`${API_URL}/list?t=${Date.now()}`);
        const json = await res.json();
        if (json.success) setSalesOrders(json.data);
      } catch (e) {
        showToast('Failed to fetch sales order list', 'error');
      }
    }
  };

  const selectLookupItem = (item: any) => {
    if (lookupType === 'salesorder') {
      setSoNo(item.SalesOrderNo);
      loadSalesOrder(item.SalesOrderNo);
    } else if (lookupType === 'location') {
      setLocationCode(item.LocationCode);
    } else if (lookupType === 'supplier') {
      setSupplierCode(item.SupplierCode);
    } else if (lookupType === 'category') {
      setCategory(item.CategoryCode);
    } else if (lookupType === 'catalogue') {
      setCatalogue(item.CatalogueCode);
    }
    setLookupType(null);
  };

  const getFilteredLookupData = () => {
    const term = lookupSearch.toLowerCase();
    if (lookupType === 'location') {
      return locations.filter(
        l => l.LocationCode.toLowerCase().includes(term) || (l.LocationName || '').toLowerCase().includes(term)
      );
    }
    if (lookupType === 'supplier') {
      return suppliers.filter(
        s => s.SupplierCode.toLowerCase().includes(term) || (s.SupplierName || '').toLowerCase().includes(term)
      );
    }
    if (lookupType === 'category') {
      return categories.filter(
        c => c.CategoryCode.toLowerCase().includes(term) || (c.CategoryName || '').toLowerCase().includes(term)
      );
    }
    if (lookupType === 'catalogue') {
      return catalogues.filter(
        c => c.CatalogueCode.toLowerCase().includes(term) || (c.CatalogueName || '').toLowerCase().includes(term)
      );
    }
    if (lookupType === 'salesorder') {
      return salesOrders.filter(
        so => so.SalesOrderNo.toLowerCase().includes(term) || (so.SupplierName || '').toLowerCase().includes(term)
      );
    }
    return [];
  };

  const loadSalesOrder = async (orderNo: string) => {
    if (!orderNo.trim()) return;
    setLoading(true);
    try {
      const res = await fetch(`${API_URL}/${orderNo}`);
      const json = await res.json();
      if (json.success) {
        const { header, details } = json;
        setSoNo(header.SalesOrderNo);
        setLocationCode(header.LocationCode);
        setSupplierCode(header.SupplierCode);
        setRemarks(header.Remarks || '');

        if (header.DueDate) {
          const dt = new Date(header.DueDate);
          const yyyy = String(dt.getFullYear());
          const mm = String(dt.getMonth() + 1).padStart(2, '0');
          const dd = String(dt.getDate()).padStart(2, '0');
          setDueDate(`${yyyy}-${mm}-${dd}`);
        }

        const mapped: SOItem[] = details.map((d: any) => ({
          LocationCode: d.LocationCode,
          CategoryCode: d.CategoryCode,
          CatalogueCode: d.CatalogueCode,
          Weight: d.Weight,
          ToWeight: d.ToWeight,
          SizeCode: d.SizeCode,
          SizeType: d.SizeType,
          Qty: d.ReOrderLevel - d.SalesOrderQty,
          ReOrderLevel: d.ReOrderLevel,
          SalesOrderQty: String(d.SalesOrderQty),
          isSelected: true
        }));
        setItems(mapped);
        showToast('Sales Order loaded successfully');
      } else {
        showToast(json.message || 'Sales Order not found.', 'error');
      }
    } catch (e) {
      console.error(e);
      showToast('Failed to retrieve Sales Order details', 'error');
    } finally {
      setLoading(false);
    }
  };

  const fetchReorder = async () => {
    if (!locationCode) {
      showToast('Please select or enter Location Code', 'error');
      return;
    }
    setLoading(true);
    try {
      const qs = `?LocationCode=${encodeURIComponent(locationCode)}&CategoryCode=${encodeURIComponent(category)}&CatalogueCode=${encodeURIComponent(catalogue)}`;
      const res = await fetch(`${API_URL}/fetch-reorder${qs}`);
      const json = await res.json();
      if (json.success) {
        const mapped: SOItem[] = json.data.map((item: any) => {
          const diff = item.ReOrderLevel - item.Qty;
          return {
            ...item,
            SalesOrderQty: diff > 0 ? String(diff) : '0',
            isSelected: true
          };
        });
        setItems(mapped);
        showToast(`Loaded ${mapped.length} reorder levels`);
      } else {
        showToast(json.message || 'Failed to fetch reorder levels', 'error');
      }
    } catch (e) {
      console.error(e);
      showToast('Network error while fetching reorder data', 'error');
    } finally {
      setLoading(false);
    }
  };

  // Keyboard shortcuts (F6: New, F7: Save, F8: Delete, Esc: Close)
  useEffect(() => {
    if (Platform.OS === 'web') {
      const handleKeyDown = (e: KeyboardEvent) => {
        if (e.key === 'F6') {
          e.preventDefault();
          handleNew();
        } else if (e.key === 'F7') {
          e.preventDefault();
          handleSave();
        } else if (e.key === 'F8') {
          e.preventDefault();
          handleDelete();
        } else if (e.key === 'Escape') {
          e.preventDefault();
          router.back();
        }
      };
      window.addEventListener('keydown', handleKeyDown);
      return () => window.removeEventListener('keydown', handleKeyDown);
    }
  }, [items, soNo, locationCode, supplierCode, remarks, dueDate]);

  const handlePrint = (orderNoToPrint?: string) => {
    const targetNo = (orderNoToPrint || soNo || '').trim();
    if (!targetNo) {
      showToast('SalesOrder Number Should not be Empty', 'error');
      return;
    }

    if (Platform.OS === 'web') {
      // Build printable HTML view for the Sales Order docket
      const printWindow = window.open('', '_blank', 'width=800,height=600');
      if (printWindow) {
        const rowsHtml = items
          .filter(i => i.isSelected)
          .map(
            (item, idx) => `
          <tr>
            <td style="border: 1px solid #ccc; padding: 6px; text-align: center;">${idx + 1}</td>
            <td style="border: 1px solid #ccc; padding: 6px;">${item.CategoryCode || ''}</td>
            <td style="border: 1px solid #ccc; padding: 6px;">${item.CatalogueCode || ''}</td>
            <td style="border: 1px solid #ccc; padding: 6px; text-align: right;">${item.Weight ? Number(item.Weight).toFixed(3) : '0.000'}</td>
            <td style="border: 1px solid #ccc; padding: 6px; text-align: right;">${item.ToWeight ? Number(item.ToWeight).toFixed(3) : '0.000'}</td>
            <td style="border: 1px solid #ccc; padding: 6px; text-align: center;">${item.SizeCode ?? '-'}</td>
            <td style="border: 1px solid #ccc; padding: 6px; text-align: center;">${item.ToSizeCode ?? '-'}</td>
            <td style="border: 1px solid #ccc; padding: 6px; text-align: center;">${item.Qty ?? 0}</td>
            <td style="border: 1px solid #ccc; padding: 6px; text-align: center;">${item.ReOrderLevel ?? 0}</td>
            <td style="border: 1px solid #ccc; padding: 6px; text-align: center; font-weight: bold;">${item.SalesOrderQty ?? 0}</td>
          </tr>`
          )
          .join('');

        printWindow.document.write(`
          <html>
            <head>
              <title>Sales Order - ${targetNo}</title>
              <style>
                body { font-family: Arial, sans-serif; padding: 20px; color: #222; }
                h2 { margin: 0 0 5px 0; }
                .meta { margin-bottom: 15px; font-size: 13px; }
                table { width: 100%; border-collapse: collapse; font-size: 12px; margin-top: 10px; }
                th { background-color: #f2f2f2; border: 1px solid #ccc; padding: 6px; text-align: left; }
                @media print {
                  button { display: none; }
                }
              </style>
            </head>
            <body>
              <div style="display:flex; justify-content:space-between; align-items:center;">
                <h2>SALES ORDER</h2>
                <button onclick="window.print()" style="padding: 6px 14px; font-size: 12px; cursor: pointer;">Print</button>
              </div>
              <div class="meta">
                <div><strong>SO No:</strong> ${targetNo}</div>
                <div><strong>Location:</strong> ${locationCode || ''}</div>
                <div><strong>Supplier:</strong> ${supplierCode || ''}</div>
                <div><strong>Due Date:</strong> ${dueDate || ''}</div>
                <div><strong>Remarks:</strong> ${remarks || ''}</div>
              </div>
              <table>
                <thead>
                  <tr>
                    <th style="text-align: center;">#</th>
                    <th>Category</th>
                    <th>Catalogue</th>
                    <th style="text-align: right;">From Wt</th>
                    <th style="text-align: right;">To Wt</th>
                    <th style="text-align: center;">From Sz</th>
                    <th style="text-align: center;">To Sz</th>
                    <th style="text-align: center;">OnHand</th>
                    <th style="text-align: center;">ReOrder</th>
                    <th style="text-align: center;">SO Qty</th>
                  </tr>
                </thead>
                <tbody>
                  ${rowsHtml || '<tr><td colspan="10" style="text-align:center; padding: 10px;">No details</td></tr>'}
                </tbody>
              </table>
              <script>
                window.onload = function() { window.print(); };
              </script>
            </body>
          </html>
        `);
        printWindow.document.close();
      }
    } else {
      Alert.alert('Print Sales Order', `Sales Order ${targetNo} sent to printer.`);
    }
  };

  const handleSave = async () => {
    if (!locationCode.trim()) {
      if (Platform.OS === 'web') {
        window.alert('LocationCode Should not be Empty');
      } else {
        Alert.alert('Information', 'LocationCode Should not be Empty');
      }
      return;
    }
    if (!supplierCode.trim()) {
      if (Platform.OS === 'web') {
        window.alert('SupplierCode Should not be Empty');
      } else {
        Alert.alert('Information', 'SupplierCode Should not be Empty');
      }
      return;
    }

    const selected = items.filter(i => i.isSelected);

    const performSave = async () => {
      const payload = {
        header: {
          SalesOrderNo: soNo,
          LocationCode: locationCode,
          SupplierCode: supplierCode,
          CategoryCode: category,
          CatalogueCode: catalogue,
          Remarks: remarks,
          DueDate: dueDate
        },
        details: selected.map(i => ({
          CategoryCode: i.CategoryCode,
          CatalogueCode: i.CatalogueCode,
          Weight: i.Weight,
          ToWeight: i.ToWeight,
          SizeCode: i.SizeCode,
          SizeType: i.SizeType,
          ReOrderLevel: i.ReOrderLevel,
          SalesOrderQty: parseFloat(i.SalesOrderQty) || 0
        }))
      };

      setSaving(true);
      try {
        const res = await fetch(API_URL, {
          method: 'POST',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify(payload)
        });
        const json = await res.json();
        if (json.success) {
          const savedSoNo = json.salesOrderNo || soNo;
          if (Platform.OS === 'web') {
            window.alert('Please take note reference no. ' + savedSoNo);
            if (window.confirm('Do you want to print ?')) {
              handlePrint(savedSoNo);
            }
          } else {
            Alert.alert('Sales Order', 'Please take note reference no. ' + savedSoNo, [
              {
                text: 'OK',
                onPress: () => {
                  Alert.alert('Print', 'Do you want to print ?', [
                    { text: 'No', style: 'cancel' },
                    { text: 'Yes', onPress: () => handlePrint(savedSoNo) }
                  ]);
                }
              }
            ]);
          }
          handleNew();
        } else {
          const errDetail = json.error ? `\nDetails: ${json.error}` : '';
          const errMsg = `${json.message || 'Failed to save Sales Order'}${errDetail}`;
          if (Platform.OS === 'web') {
            window.alert(errMsg);
          }
          showToast(json.message || 'Failed to save Sales Order', 'error');
        }
      } catch (e: any) {
        console.error(e);
        showToast(e.message || 'Network error when saving Sales Order', 'error');
      } finally {
        setSaving(false);
      }
    };

    if (Platform.OS === 'web') {
      if (window.confirm('Do want to Create SalesOrder for all  these items ?')) {
        await performSave();
      }
    } else {
      Alert.alert(
        'Information',
        'Do want to Create SalesOrder for all  these items ?',
        [
          { text: 'No', style: 'cancel' },
          { text: 'Yes', onPress: performSave }
        ]
      );
    }
  };

  const handleDelete = async () => {
    if (!soNo.trim()) {
      if (Platform.OS === 'web') {
        window.alert('SalesOrder Number Should not be Empty');
      } else {
        Alert.alert('Information', 'SalesOrder Number Should not be Empty');
      }
      return;
    }

    const executeDelete = async () => {
      setLoading(true);
      try {
        const res = await fetch(`${API_URL}/${soNo}`, { method: 'DELETE' });
        const json = await res.json();
        if (json.success) {
          if (Platform.OS === 'web') {
            window.alert('Sales Order Deleted');
          } else {
            Alert.alert('GoldPOS', 'Sales Order Deleted');
          }
          handleNew();
        } else {
          if (Platform.OS === 'web') {
            window.alert(json.message || 'Could not delete Sales Order');
          } else {
            Alert.alert('Information', json.message || 'Could not delete Sales Order');
          }
        }
      } catch (e) {
        console.error(e);
        showToast('Network error when deleting Sales Order', 'error');
      } finally {
        setLoading(false);
      }
    };

    if (Platform.OS === 'web') {
      if (window.confirm(`Do you want to delete SalesOrder No ${soNo}`)) {
        executeDelete();
      }
    } else {
      Alert.alert('GoldPOS', `Do you want to delete SalesOrder No ${soNo}`, [
        { text: 'No', style: 'cancel' },
        { text: 'Yes', onPress: executeDelete }
      ]);
    }
  };

  const handleNew = () => {
    loadNextSONo();
    setSupplierCode('');
    setCategory('');
    setCatalogue('');
    setRemarks('');
    setItems([]);

    const defaultDate = new Date();
    defaultDate.setDate(defaultDate.getDate() + 7);
    const dd = String(defaultDate.getDate()).padStart(2, '0');
    const mm = String(defaultDate.getMonth() + 1).padStart(2, '0');
    const yyyy = String(defaultDate.getFullYear());
    setDueDate(`${yyyy}-${mm}-${dd}`);
  };

  const toggleSelect = (index: number) => {
    const updated = [...items];
    updated[index].isSelected = !updated[index].isSelected;
    setItems(updated);
  };

  const toggleSelectAll = () => {
    const allSelected = items.every(i => i.isSelected);
    const updated = items.map(i => ({ ...i, isSelected: !allSelected }));
    setItems(updated);
  };

  const updateItemQty = (index: number, val: string) => {
    const updated = [...items];
    updated[index].SalesOrderQty = val;
    setItems(updated);
  };

  const selectedCount = items.filter(i => i.isSelected).length;
  const totalSOQty = items
    .filter(i => i.isSelected)
    .reduce((sum, i) => sum + (parseFloat(i.SalesOrderQty) || 0), 0);

  return (
    <SafeAreaView edges={['top']} style={styles.safeArea}>
      <Stack.Screen options={{ headerShown: false }} />

      {/* Top Header Bar matching jobsheet.tsx and reference actions */}
      <View style={styles.header}>
        <View style={styles.headerLeft}>
          <TouchableOpacity onPress={() => router.back()} style={styles.backButton}>
            <Ionicons name="arrow-back" size={20} color="#3B2D17" />
          </TouchableOpacity>
          <ThemedText style={styles.headerTitle}>Sales Order Entry</ThemedText>
        </View>

        <View style={styles.headerActions}>
          <TouchableOpacity
            style={styles.headerBtnSecondary}
            onPress={() => handlePrint()}
          >
            <ThemedText style={styles.headerBtnSecondaryText}>Re Print</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity style={styles.headerBtnSecondary} onPress={handleNew}>
            <ThemedText style={styles.headerBtnSecondaryText}>New</ThemedText>
          </TouchableOpacity>

          <TouchableOpacity
            style={[styles.headerBtnPrimary, saving && { opacity: 0.7 }]}
            onPress={handleSave}
            disabled={saving}
          >
            {saving ? (
              <ActivityIndicator size="small" color="#9E6D08" />
            ) : (
              <ThemedText style={styles.headerBtnPrimaryText}>Save</ThemedText>
            )}
          </TouchableOpacity>

          <TouchableOpacity style={styles.headerBtnDelete} onPress={handleDelete}>
            <ThemedText style={styles.headerBtnDeleteText}>Delete</ThemedText>
          </TouchableOpacity>


        </View>
      </View>

      <ScrollView style={styles.container} contentContainerStyle={styles.contentContainer}>
        {/* TOP SECTION: Filters & Order Details Card (Jobsheet Gold Card) */}
        <View style={styles.goldCard}>
          {/* Card Header Banner */}
          <View style={styles.cardHeaderBanner}>
            <View style={{ flexDirection: 'row', alignItems: 'center' }}>
              <View style={styles.headerIconBadge}>
                <Ionicons name="document-text" size={15} color="#B48218" />
              </View>
              <ThemedText style={styles.cardHeaderTitle}>Sales Order Details</ThemedText>
            </View>

            <TouchableOpacity style={styles.fetchBtn} onPress={fetchReorder}>
              <Ionicons name="refresh" size={13} color="#9E6D08" style={{ marginRight: 4 }} />
              <ThemedText style={styles.fetchBtnText}>Fetch Reorder</ThemedText>
            </TouchableOpacity>
          </View>

          <View style={styles.cardBody}>
            {/* Row 1: SO No, Location Code, Supplier Code */}
            <View style={styles.row}>
              {/* SO No */}
              <ThemedText style={styles.label}>SO No</ThemedText>
              <View style={{ width: 145, flexDirection: 'row' }}>
                <TextInput
                  style={[styles.input, { width: 113, borderTopRightRadius: 0, borderBottomRightRadius: 0, borderRightWidth: 0 }]}
                  value={soNo}
                  onChangeText={setSoNo}
                  onBlur={() => loadSalesOrder(soNo)}
                  placeholder="Auto / SO#"
                />
                <TouchableOpacity style={styles.lookupBtn} onPress={() => openLookup('salesorder')}>
                  <ThemedText style={styles.lookupBtnText}>...</ThemedText>
                </TouchableOpacity>
              </View>

              {/* Location Code */}
              <ThemedText style={[styles.labelRight, { width: 95 }]}>Location</ThemedText>
              <View style={{ width: 135, flexDirection: 'row' }}>
                <TextInput
                  style={[styles.input, { width: 103, borderTopRightRadius: 0, borderBottomRightRadius: 0, borderRightWidth: 0 }]}
                  value={locationCode}
                  onChangeText={setLocationCode}
                  placeholder="Location"
                />
                <TouchableOpacity style={styles.lookupBtn} onPress={() => openLookup('location')}>
                  <ThemedText style={styles.lookupBtnText}>...</ThemedText>
                </TouchableOpacity>
              </View>

              {/* Supplier Code */}
              <ThemedText style={[styles.labelRight, { width: 95 }]}>Supplier</ThemedText>
              <View style={{ flex: 1, minWidth: 140, flexDirection: 'row' }}>
                <TextInput
                  style={[styles.input, { flex: 1, borderTopRightRadius: 0, borderBottomRightRadius: 0, borderRightWidth: 0 }]}
                  value={supplierCode}
                  onChangeText={setSupplierCode}
                  placeholder="Supplier Code"
                />
                <TouchableOpacity style={styles.lookupBtn} onPress={() => openLookup('supplier')}>
                  <ThemedText style={styles.lookupBtnText}>...</ThemedText>
                </TouchableOpacity>
              </View>
            </View>

            {/* Row 2: Category, Catalogue, Due Date */}
            <View style={styles.row}>
              {/* Category */}
              <ThemedText style={styles.label}>Category</ThemedText>
              <View style={{ width: 145, flexDirection: 'row' }}>
                <TextInput
                  style={[styles.input, { width: 113, borderTopRightRadius: 0, borderBottomRightRadius: 0, borderRightWidth: 0 }]}
                  value={category}
                  onChangeText={setCategory}
                  placeholder="Optional"
                />
                <TouchableOpacity style={styles.lookupBtn} onPress={() => openLookup('category')}>
                  <ThemedText style={styles.lookupBtnText}>...</ThemedText>
                </TouchableOpacity>
              </View>

              {/* Catalogue */}
              <ThemedText style={[styles.labelRight, { width: 95 }]}>Catalogue</ThemedText>
              <View style={{ width: 135, flexDirection: 'row' }}>
                <TextInput
                  style={[styles.input, { width: 103, borderTopRightRadius: 0, borderBottomRightRadius: 0, borderRightWidth: 0 }]}
                  value={catalogue}
                  onChangeText={setCatalogue}
                  placeholder="Optional"
                />
                <TouchableOpacity style={styles.lookupBtn} onPress={() => openLookup('catalogue')}>
                  <ThemedText style={styles.lookupBtnText}>...</ThemedText>
                </TouchableOpacity>
              </View>

              {/* Due Date */}
              <ThemedText style={[styles.labelRight, { width: 95 }]}>Due Date</ThemedText>
              <View style={{ flex: 1, minWidth: 140 }}>
                {Platform.OS === 'web' ? (
                  <input
                    type="date"
                    style={{
                      width: '100%',
                      border: '1px solid #E2D7C5',
                      borderRadius: '6px',
                      padding: '4px 8px',
                      fontSize: '12px',
                      height: '28px',
                      color: '#2C2519',
                      backgroundColor: '#FFFFFF',
                      outline: 'none',
                      boxSizing: 'border-box',
                    }}
                    value={dueDate}
                    onChange={(e: any) => setDueDate(e.target.value || '')}
                  />
                ) : (
                  <TextInput
                    style={[styles.input, { width: '100%' }]}
                    value={dueDate}
                    onChangeText={setDueDate}
                    placeholder="YYYY-MM-DD"
                  />
                )}
              </View>
            </View>

            {/* Row 3: Remarks and Fetch Button */}
            <View style={styles.row}>
              <ThemedText style={styles.label}>Remarks</ThemedText>
              <TextInput
                style={[styles.input, { flex: 1, minWidth: 200, marginRight: 8 }]}
                value={remarks}
                onChangeText={setRemarks}
                placeholder="Enter sales order remarks..."
              />
              <TouchableOpacity style={styles.fetchBtn} onPress={fetchReorder}>
                <Ionicons name="refresh" size={13} color="#9E6D08" style={{ marginRight: 4 }} />
                <ThemedText style={styles.fetchBtnText}>Fetch</ThemedText>
              </TouchableOpacity>
            </View>
          </View>
        </View>

        {/* MIDDLE SECTION: Items Grid Table (Jobsheet-style Gold Card) */}
        <View style={styles.goldCard}>
          <View style={styles.cardHeaderBanner}>
            <View style={{ flexDirection: 'row', alignItems: 'center' }}>
              <View style={styles.headerIconBadge}>
                <Ionicons name="list" size={15} color="#B48218" />
              </View>
              <ThemedText style={styles.cardHeaderTitle}>Reorder Stock Items</ThemedText>
              <View style={styles.counterBadge}>
                <ThemedText style={styles.counterBadgeText}>
                  {selectedCount} / {items.length} Selected
                </ThemedText>
              </View>
            </View>

            {items.length > 0 && (
              <TouchableOpacity style={styles.selectAllBtn} onPress={toggleSelectAll}>
                <Ionicons
                  name={items.every(i => i.isSelected) ? 'checkbox' : 'checkbox-outline'}
                  size={14}
                  color="#9E6D08"
                  style={{ marginRight: 4 }}
                />
                <ThemedText style={styles.selectAllBtnText}>
                  {items.every(i => i.isSelected) ? 'Deselect All' : 'Select All'}
                </ThemedText>
              </TouchableOpacity>
            )}
          </View>

          <ScrollView horizontal={true} showsHorizontalScrollIndicator={true} contentContainerStyle={{ flexGrow: 1 }}>
            <View style={{ width: '100%', minWidth: 920 }}>
              {/* Table Header */}
              <View style={styles.tableHeader}>
                <TouchableOpacity
                  style={[styles.tableHeaderCell, { width: 50, alignItems: 'center' }]}
                  onPress={toggleSelectAll}
                >
                  <Ionicons
                    name={items.length > 0 && items.every(i => i.isSelected) ? 'checkbox' : 'square-outline'}
                    size={16}
                    color="#3B2D17"
                  />
                </TouchableOpacity>
                <ThemedText style={[styles.tableHeaderText, { width: 110 }]}>Category</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { flex: 2 }]}>Catalogue</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { width: 85, textAlign: 'right' }]}>From Wt</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { width: 85, textAlign: 'right' }]}>To Wt</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { width: 80, textAlign: 'center' }]}>From Sz</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { width: 80, textAlign: 'center' }]}>To Sz</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { width: 75, textAlign: 'center' }]}>OnHand</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { width: 80, textAlign: 'center' }]}>ReOrder</ThemedText>
                <ThemedText style={[styles.tableHeaderText, { width: 105, textAlign: 'center' }]}>SO Qty</ThemedText>
              </View>

              {/* Table Body */}
              <ScrollView nestedScrollEnabled={true} style={{ maxHeight: 380, minHeight: items.length === 0 ? 120 : 0 }}>
                {loading ? (
                  <View style={styles.emptyTableState}>
                    <ActivityIndicator size="large" color="#B48218" />
                    <ThemedText style={{ color: '#7D6F5B', fontSize: 12, marginTop: 8 }}>
                      Loading reorder items...
                    </ThemedText>
                  </View>
                ) : items.length === 0 ? (
                  <View style={styles.emptyTableState}>
                    <Ionicons name="cube-outline" size={32} color="#D0C2A8" />
                    <ThemedText style={{ color: '#3B2D17', fontSize: 13, fontWeight: 'bold', marginTop: 4 }}>
                      No items loaded
                    </ThemedText>
                    <ThemedText style={{ color: '#7D6F5B', fontSize: 11, marginTop: 2 }}>
                      Select Location / Category / Catalogue and click 'Fetch Reorder'
                    </ThemedText>
                  </View>
                ) : (
                  items.map((item, index) => {
                    return (
                      <TouchableOpacity
                        key={`${item.CategoryCode}-${item.CatalogueCode}-${index}`}
                        activeOpacity={0.8}
                        style={[
                          styles.tableRow,
                          index % 2 === 1 && { backgroundColor: '#FCFAF6' },
                          item.isSelected && styles.tableRowSelected,
                        ]}
                        onPress={() => toggleSelect(index)}
                      >
                        {/* Checkbox */}
                        <View style={{ width: 50, alignItems: 'center' }}>
                          <View style={[styles.checkboxSquare, item.isSelected && styles.checkboxSquareActive]}>
                            {item.isSelected && <Ionicons name="checkmark" size={11} color="#FFFFFF" />}
                          </View>
                        </View>

                        {/* Category */}
                        <ThemedText style={[styles.tableCellText, { width: 110, fontWeight: 'bold' }]}>
                          {item.CategoryCode}
                        </ThemedText>

                        {/* Catalogue */}
                        <ThemedText style={[styles.tableCellText, { flex: 2 }]} numberOfLines={1}>
                          {item.CatalogueCode}
                        </ThemedText>

                        {/* From Wt */}
                        <ThemedText style={[styles.tableCellText, { width: 85, textAlign: 'right' }]}>
                          {item.Weight ? Number(item.Weight).toFixed(3) : '0.000'}
                        </ThemedText>

                        {/* To Wt */}
                        <ThemedText style={[styles.tableCellText, { width: 85, textAlign: 'right' }]}>
                          {item.ToWeight ? Number(item.ToWeight).toFixed(3) : '0.000'}
                        </ThemedText>

                        {/* From Sz */}
                        <ThemedText style={[styles.tableCellText, { width: 80, textAlign: 'center' }]}>
                          {item.SizeCode ?? '-'}
                        </ThemedText>

                        {/* To Sz */}
                        <ThemedText style={[styles.tableCellText, { width: 80, textAlign: 'center' }]}>
                          {item.ToSizeCode ?? '-'}
                        </ThemedText>

                        {/* On Hand */}
                        <ThemedText style={[styles.tableCellText, { width: 75, textAlign: 'center' }]}>
                          {item.Qty}
                        </ThemedText>

                        {/* ReOrder */}
                        <ThemedText style={[styles.tableCellText, { width: 80, textAlign: 'center', fontWeight: 'bold', color: '#554736' }]}>
                          {item.ReOrderLevel}
                        </ThemedText>

                        {/* SO Qty Input */}
                        <View style={{ width: 105, paddingHorizontal: 6 }} onStartShouldSetResponder={() => true}>
                          <TextInput
                            style={styles.gridInput}
                            value={item.SalesOrderQty}
                            onChangeText={val => updateItemQty(index, val)}
                            keyboardType="numeric"
                          />
                        </View>
                      </TouchableOpacity>
                    );
                  })
                )}
              </ScrollView>

              {/* Table Total Summary Row */}
              <View style={styles.tableTotalRow}>
                <ThemedText style={[styles.tableTotalCellText, { width: 50, textAlign: 'center' }]}>
                  {selectedCount}
                </ThemedText>
                <ThemedText style={[styles.tableTotalCellText, { width: 110 }]}>SELECTED ITEMS</ThemedText>
                <View style={{ flex: 2 }} />
                <View style={{ width: 85 }} />
                <View style={{ width: 85 }} />
                <View style={{ width: 80 }} />
                <View style={{ width: 80 }} />
                <View style={{ width: 75 }} />
                <ThemedText style={[styles.tableTotalCellText, { width: 80, textAlign: 'center' }]}>TOTAL QTY:</ThemedText>
                <ThemedText style={[styles.tableTotalCellText, { width: 105, textAlign: 'center', color: '#9E6D08' }]}>
                  {totalSOQty}
                </ThemedText>
              </View>
            </View>
          </ScrollView>
        </View>
      </ScrollView>

      {/* LOV Search Modal matching Jobsheet */}
      <Modal visible={!!lookupType} transparent animationType="fade">
        <TouchableWithoutFeedback onPress={() => setLookupType(null)}>
          <View style={styles.modalOverlay}>
            <TouchableWithoutFeedback>
              <View style={styles.modalContainer}>
                <View style={styles.modalHeader}>
                  <ThemedText style={styles.modalTitle}>Select {lookupType}</ThemedText>
                  <TouchableOpacity onPress={() => setLookupType(null)} style={styles.modalCloseIconBtn}>
                    <Ionicons name="close" size={20} color="#3B2D17" />
                  </TouchableOpacity>
                </View>

                <View style={{ padding: 12 }}>
                  <TextInput
                    style={styles.modalSearchInput}
                    placeholder="Search options..."
                    placeholderTextColor="#7D6F5B"
                    value={lookupSearch}
                    onChangeText={setLookupSearch}
                    autoFocus
                  />

                  <FlatList
                    data={getFilteredLookupData()}
                    keyExtractor={(item, index) => index.toString()}
                    style={{ maxHeight: 340 }}
                    renderItem={({ item, index }) => {
                      let code = '';
                      let name = '';
                      if (lookupType === 'location') {
                        code = item.LocationCode;
                        name = item.LocationName;
                      } else if (lookupType === 'supplier') {
                        code = item.SupplierCode;
                        name = item.SupplierName;
                      } else if (lookupType === 'category') {
                        code = item.CategoryCode;
                        name = item.CategoryName;
                      } else if (lookupType === 'catalogue') {
                        code = item.CatalogueCode;
                        name = item.CatalogueName;
                      } else if (lookupType === 'salesorder') {
                        code = item.SalesOrderNo;
                        name = item.SupplierName || 'No Supplier';
                      }

                      return (
                        <TouchableOpacity
                          activeOpacity={0.7}
                          style={[
                            styles.lovItem,
                            index % 2 === 1 && { backgroundColor: '#FCFAF6' },
                          ]}
                          onPress={() => selectLookupItem(item)}
                        >
                          <ThemedText style={[styles.lovItemText, { width: 130, fontWeight: 'bold' }]}>
                            {code}
                          </ThemedText>
                          <ThemedText style={[styles.lovItemText, { flex: 1 }]}>{name}</ThemedText>
                        </TouchableOpacity>
                      );
                    }}
                    ListEmptyComponent={
                      <View style={{ padding: 24, alignItems: 'center' }}>
                        <ThemedText style={{ color: '#7D6F5B', fontSize: 13, fontStyle: 'italic' }}>
                          No items found.
                        </ThemedText>
                      </View>
                    }
                  />
                </View>

                <View style={styles.modalFooter}>
                  <TouchableOpacity style={styles.modalCloseBtn} onPress={() => setLookupType(null)}>
                    <ThemedText style={styles.modalCloseBtnText}>Close</ThemedText>
                  </TouchableOpacity>
                </View>
              </View>
            </TouchableWithoutFeedback>
          </View>
        </TouchableWithoutFeedback>
      </Modal>

      {/* Floating Toast Notification */}
      {toast && (
        <View style={[styles.toast, { backgroundColor: toast.type === 'success' ? '#2E7D32' : '#C62828' }]}>
          <Ionicons
            name={toast.type === 'success' ? 'checkmark-circle' : 'alert-circle'}
            size={16}
            color="#FFFFFF"
            style={{ marginRight: 6 }}
          />
          <ThemedText style={styles.toastText}>{toast.message}</ThemedText>
        </View>
      )}

      {/* Full Screen Loading Overlay */}
      {saving && (
        <View style={styles.loadingOverlay}>
          <ActivityIndicator size="large" color="#B48218" />
          <ThemedText style={{ color: '#FFFFFF', fontWeight: 'bold', marginTop: 10 }}>Saving Sales Order...</ThemedText>
        </View>
      )}
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  safeArea: {
    flex: 1,
    backgroundColor: '#FBF9F5',
  },
  // Top Header Bar
  header: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingHorizontal: 16,
    paddingVertical: 10,
    borderBottomWidth: 1,
    borderBottomColor: '#EBE2D2',
    backgroundColor: '#FFFFFF',
  },
  headerLeft: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  backButton: {
    padding: 4,
    marginRight: 10,
  },
  headerTitle: {
    fontSize: 20,
    fontWeight: '400',
    letterSpacing: 1,
    color: '#3B2D17',
  },
  headerActions: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 8,
  },
  headerBtnSecondary: {
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#DFD5C3',
    paddingVertical: 6,
    paddingHorizontal: 12,
    borderRadius: 5,
  },
  headerBtnSecondaryText: {
    color: '#5C4A28',
    fontWeight: 'bold',
    fontSize: 12,
  },
  headerBtnPrimary: {
    backgroundColor: '#F9F2E2',
    borderWidth: 1,
    borderColor: '#B8903B',
    paddingVertical: 6,
    paddingHorizontal: 16,
    borderRadius: 5,
  },
  headerBtnPrimaryText: {
    color: '#9E6D08',
    fontWeight: 'bold',
    fontSize: 12,
  },
  headerBtnDelete: {
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#F2A8A2',
    paddingVertical: 6,
    paddingHorizontal: 12,
    borderRadius: 5,
  },
  headerBtnDeleteText: {
    color: '#D93829',
    fontWeight: 'bold',
    fontSize: 12,
  },
  container: {
    flex: 1,
    backgroundColor: '#FBF9F5',
  },
  contentContainer: {
    padding: 12,
    paddingBottom: 28,
  },
  // Gold Cards
  goldCard: {
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#E8DCB8',
    borderRadius: 8,
    marginBottom: 12,
    overflow: 'hidden',
    shadowColor: '#A38035',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.08,
    shadowRadius: 3,
    elevation: 2,
  },
  cardHeaderBanner: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    backgroundColor: '#F5E7CA',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    paddingVertical: 7,
    paddingHorizontal: 10,
  },
  headerIconBadge: {
    width: 22,
    height: 22,
    borderRadius: 11,
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
  counterBadge: {
    backgroundColor: '#FFFFFF',
    paddingHorizontal: 8,
    paddingVertical: 2,
    borderRadius: 10,
    borderWidth: 1,
    borderColor: '#E2D3B3',
    marginLeft: 8,
  },
  counterBadgeText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#9E6D08',
  },
  fetchBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#F9F2E2',
    borderWidth: 1,
    borderColor: '#B8903B',
    paddingVertical: 4,
    paddingHorizontal: 10,
    borderRadius: 5,
  },
  fetchBtnText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#9E6D08',
  },
  selectAllBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#DFD5C3',
    paddingVertical: 3,
    paddingHorizontal: 8,
    borderRadius: 4,
  },
  selectAllBtnText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#5C4A28',
  },
  cardBody: {
    padding: 10,
  },
  row: {
    flexDirection: 'row',
    alignItems: 'center',
    marginBottom: 7,
    flexWrap: 'wrap',
  },
  label: {
    fontSize: 11,
    fontWeight: 'bold',
    width: 80,
    marginRight: 6,
    color: '#3A2E19',
  },
  labelRight: {
    fontSize: 11,
    fontWeight: 'bold',
    textAlign: 'right',
    marginRight: 8,
    color: '#3A2E19',
  },
  input: {
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderRadius: 6,
    paddingHorizontal: 8,
    paddingVertical: 3,
    fontSize: 12,
    height: 28,
    backgroundColor: '#FFFFFF',
    color: '#2C2519',
  },
  lookupBtn: {
    width: 32,
    height: 28,
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderLeftWidth: 1,
    justifyContent: 'center',
    alignItems: 'center',
    borderTopRightRadius: 6,
    borderBottomRightRadius: 6,
    backgroundColor: '#FAF5EA',
  },
  lookupBtnText: {
    color: '#6E5528',
    fontWeight: 'bold',
    fontSize: 12,
  },
  checkboxSquare: {
    width: 16,
    height: 16,
    borderWidth: 1.5,
    borderColor: '#B8903B',
    justifyContent: 'center',
    alignItems: 'center',
    borderRadius: 3,
    backgroundColor: '#FFFFFF',
  },
  checkboxSquareActive: {
    backgroundColor: '#B8903B',
  },
  // Table
  tableHeader: {
    flexDirection: 'row',
    backgroundColor: '#F5E7CA',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    paddingVertical: 7,
    paddingHorizontal: 6,
    alignItems: 'center',
  },
  tableHeaderCell: {
    paddingHorizontal: 4,
    justifyContent: 'center',
  },
  tableHeaderText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#3B2D17',
    paddingHorizontal: 4,
  },
  tableRow: {
    flexDirection: 'row',
    paddingVertical: 5,
    paddingHorizontal: 6,
    borderBottomWidth: 1,
    borderBottomColor: '#F0E6D2',
    alignItems: 'center',
    backgroundColor: '#FFFFFF',
  },
  tableRowSelected: {
    backgroundColor: '#FFF6DF',
    borderLeftWidth: 3,
    borderLeftColor: '#B8903B',
  },
  tableCellText: {
    fontSize: 11,
    color: '#332717',
    paddingHorizontal: 4,
  },
  gridInput: {
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderRadius: 4,
    paddingHorizontal: 6,
    paddingVertical: 2,
    fontSize: 11,
    height: 24,
    textAlign: 'center',
    backgroundColor: '#FFFFFF',
    color: '#2C2519',
    fontWeight: 'bold',
  },
  emptyTableState: {
    paddingVertical: 36,
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: '#FFFFFF',
  },
  tableTotalRow: {
    flexDirection: 'row',
    backgroundColor: '#F9F1DF',
    borderTopWidth: 1,
    borderTopColor: '#E5D6B6',
    paddingVertical: 7,
    paddingHorizontal: 6,
    alignItems: 'center',
  },
  tableTotalCellText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: '#32230D',
    paddingHorizontal: 4,
  },
  // Modal
  modalOverlay: {
    flex: 1,
    backgroundColor: 'rgba(0,0,0,0.5)',
    justifyContent: 'center',
    alignItems: 'center',
    padding: 16,
  },
  modalContainer: {
    width: '90%',
    maxWidth: 580,
    maxHeight: '80%',
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#E8DCB8',
    borderRadius: 8,
    overflow: 'hidden',
  },
  modalHeader: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    backgroundColor: '#F5E7CA',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    paddingHorizontal: 12,
    paddingVertical: 10,
  },
  modalTitle: {
    fontSize: 14,
    fontWeight: 'bold',
    color: '#3B2D17',
    textTransform: 'capitalize',
  },
  modalCloseIconBtn: {
    padding: 4,
  },
  modalSearchInput: {
    borderWidth: 1,
    borderColor: '#E2D7C5',
    borderRadius: 6,
    paddingHorizontal: 10,
    paddingVertical: 5,
    fontSize: 12,
    height: 32,
    backgroundColor: '#FFFFFF',
    color: '#2C2519',
    marginBottom: 10,
  },
  lovItem: {
    flexDirection: 'row',
    paddingVertical: 9,
    paddingHorizontal: 8,
    borderBottomWidth: 1,
    borderBottomColor: '#F0E6D2',
    backgroundColor: '#FFFFFF',
    alignItems: 'center',
  },
  lovItemText: {
    fontSize: 12,
    color: '#332717',
  },
  modalFooter: {
    flexDirection: 'row',
    justifyContent: 'flex-end',
    paddingHorizontal: 12,
    paddingVertical: 8,
    borderTopWidth: 1,
    borderTopColor: '#F0E6D2',
    backgroundColor: '#FCFAF6',
  },
  modalCloseBtn: {
    paddingVertical: 5,
    paddingHorizontal: 14,
    borderRadius: 5,
    borderWidth: 1,
    borderColor: '#DFD5C3',
    backgroundColor: '#FFFFFF',
  },
  modalCloseBtnText: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#5C4A28',
  },
  // Toast & Loader
  toast: {
    position: 'absolute',
    top: 40,
    alignSelf: 'center',
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 18,
    paddingVertical: 9,
    borderRadius: 20,
    elevation: 6,
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.25,
    shadowRadius: 3.84,
    zIndex: 9999,
  },
  toastText: {
    fontSize: 13,
    fontWeight: 'bold',
    color: '#FFFFFF',
  },
  loadingOverlay: {
    ...StyleSheet.absoluteFillObject,
    backgroundColor: 'rgba(0,0,0,0.45)',
    justifyContent: 'center',
    alignItems: 'center',
    zIndex: 10000,
  },
});
