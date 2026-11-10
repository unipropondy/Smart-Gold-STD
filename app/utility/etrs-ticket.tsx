import { Ionicons, MaterialCommunityIcons } from '@expo/vector-icons';
import { Stack, useRouter } from 'expo-router';
import React, { useEffect, useState } from 'react';
import {
  ActivityIndicator,
  Alert,
  Image,
  Platform,
  ScrollView,
  StyleSheet,
  Text,
  TextInput,
  TouchableOpacity,
  View,
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';
import { API_BASE_URL } from '@/constants/api';

export default function ETRSTicketScreen() {
  const router = useRouter();

  // Form fields
  const [invoiceNo, setInvoiceNo] = useState('');
  const [invoiceDay, setInvoiceDay] = useState('');
  const [invoiceMonth, setInvoiceMonth] = useState('');
  const [invoiceYear, setInvoiceYear] = useState('');
  const [passportNo, setPassportNo] = useState('');
  const [netAmount, setNetAmount] = useState('');
  const [gstAmount, setGstAmount] = useState('');
  const [etrsDocId, setEtrsDocId] = useState('');
  
  // Date & Time pickers
  const today = new Date();
  const defaultDateStr = `${today.getDate()} ${today.toLocaleString('default', { month: 'long' })} ${today.getFullYear()}`;
  const defaultTimeStr = today.toTimeString().split(' ')[0]; // HH:MM:SS
  
  const [issueDate, setIssueDate] = useState(defaultDateStr);
  const [issueTime, setIssueTime] = useState(defaultTimeStr);
  const [remarks, setRemarks] = useState('');

  // Right Section
  const [sales7000OrMore, setSales7000OrMore] = useState<'Yes' | 'No'>('No');
  const [isSealedBag, setIsSealedBag] = useState<'Yes' | 'No'>('No');
  const [sealedBagSerial, setSealedBagSerial] = useState('');
  const [selectedStaff, setSelectedStaff] = useState('');
  const [staffList, setStaffList] = useState<{ SalesManCode: string; SalesManName: string }[]>([]);
  const [staffDropdownOpen, setStaffDropdownOpen] = useState(false);
  const [photoUri, setPhotoUri] = useState<string | null>(null);

  // States
  const [loadingInvoice, setLoadingInvoice] = useState(false);
  const [saving, setSaving] = useState(false);

  // Load Staff List on Mount
  useEffect(() => {
    fetchStaffList();
  }, []);

  // Keyboard Shortcuts Listener for Web
  useEffect(() => {
    if (Platform.OS === 'web') {
      const handleKeyDown = (e: KeyboardEvent) => {
        if (e.key === 'F6') {
          e.preventDefault();
          handleClear();
        } else if (e.key === 'F7') {
          e.preventDefault();
          handleSave();
        } else if (e.key === 'Escape') {
          e.preventDefault();
          router.back();
        }
      };
      window.addEventListener('keydown', handleKeyDown);
      return () => window.removeEventListener('keydown', handleKeyDown);
    }
  }, [invoiceNo, passportNo, etrsDocId, remarks, isSealedBag, sealedBagSerial, selectedStaff, photoUri, issueDate, issueTime]);

  const fetchStaffList = async () => {
    try {
      const res = await fetch(`${API_BASE_URL}/salesman`);
      const json = await res.json();
      if (json && json.success && Array.isArray(json.data)) {
        setStaffList(json.data);
      } else {
        setStaffList([]);
      }
    } catch (err) {
      console.error('Error fetching staff list:', err);
      setStaffList([]);
    }
  };

  // Search Invoice
  const handleSearchInvoice = async () => {
    if (!invoiceNo.trim()) {
      if (Platform.OS === 'web') {
        window.alert('Please enter an Invoice No.');
      } else {
        Alert.alert('Validation', 'Please enter an Invoice No.');
      }
      return;
    }

    setLoadingInvoice(true);
    try {
      // 0. Check if an eTRS statement already exists in tbleTRSStatement for this Invoice/Bill No
      let existingEtrsFound = false;
      try {
        const etrsRes = await fetch(`${API_BASE_URL}/paymententry/etrs/${encodeURIComponent(invoiceNo.trim())}`);
        const etrsJson = await etrsRes.json();
        if (etrsJson.success && etrsJson.data) {
          existingEtrsFound = true;
          const ed = etrsJson.data;
          if (ed.PassPortNo) setPassportNo(ed.PassPortNo);
          if (ed.DocId) setEtrsDocId(ed.DocId);
          if (ed.NetAmount != null) setNetAmount(String(ed.NetAmount));
          if (ed.Gst != null) setGstAmount(String(ed.Gst));
          if (ed.BillDay && ed.BillMonth && ed.BillYear) {
            setInvoiceDay(String(ed.BillDay).padStart(2, '0'));
            setInvoiceMonth(String(ed.BillMonth).padStart(2, '0'));
            setInvoiceYear(String(ed.BillYear));
          } else if (ed.BillDate) {
            const bd = new Date(ed.BillDate);
            if (!isNaN(bd.getTime())) {
              setInvoiceDay(String(bd.getDate()).padStart(2, '0'));
              setInvoiceMonth(String(bd.getMonth() + 1).padStart(2, '0'));
              setInvoiceYear(String(bd.getFullYear()));
            }
          }
          if (ed.eTRSIssueDate) {
            const id = new Date(ed.eTRSIssueDate);
            if (!isNaN(id.getTime())) {
              setIssueDate(`${id.getDate()} ${id.toLocaleString('default', { month: 'long' })} ${id.getFullYear()}`);
            } else {
              setIssueDate(String(ed.eTRSIssueDate));
            }
          }
          if (ed.eTRSIssueTime) setIssueTime(ed.eTRSIssueTime);
          if (ed.Remarks) setRemarks(ed.Remarks);
          if (ed.SalesmanCode) setSelectedStaff(ed.SalesmanCode);
          if (ed.CustomerImage) {
            // Restore saved customer image data URL
            setPhotoUri(ed.CustomerImage);
          }
        }
      } catch (e) {
        console.warn('Error fetching existing eTRS statement:', e);
      }

      // 1. Fetch Sales Header
      const res = await fetch(`${API_BASE_URL}/saleshistory/${encodeURIComponent(invoiceNo.trim())}`);
      const json = await res.json();

      if (json.success && json.header) {
        const h = json.header;
        setNetAmount(h.NetTotal ? String(h.NetTotal) : '');
        setGstAmount(h.GST ? String(h.GST) : '');

        if (h.InvoiceDate) {
          const d = new Date(h.InvoiceDate);
          if (!isNaN(d.getTime())) {
            setInvoiceDay(String(d.getDate()).padStart(2, '0'));
            setInvoiceMonth(String(d.getMonth() + 1).padStart(2, '0'));
            setInvoiceYear(String(d.getFullYear()));
          }
        }

        // Fetch Customer info if CustomerCode exists
        if (h.CustomerCode) {
          try {
            const custRes = await fetch(`${API_BASE_URL}/customer`);
            const custJson = await custRes.json();
            if (custJson.success && Array.isArray(custJson.data)) {
              const cust = custJson.data.find((c: any) => c.CustomerCode === h.CustomerCode);
              if (cust && cust.PassportNo) {
                setPassportNo((prev) => prev || cust.PassportNo);
              }
            }
          } catch (e) {
            console.warn('Error fetching customer for passport:', e);
          }

          // Check last 3 months sales for this customer
          try {
            const summaryRes = await fetch(`${API_BASE_URL}/customer/${encodeURIComponent(h.CustomerCode)}/sales_summary`);
            const summaryJson = await summaryRes.json();
            if (summaryJson.success && Array.isArray(summaryJson.data)) {
              const threeMonthsAgo = new Date();
              threeMonthsAgo.setMonth(threeMonthsAgo.getMonth() - 3);

              const total3Months = summaryJson.data
                .filter((item: any) => new Date(item.InvoiceDate) >= threeMonthsAgo)
                .reduce((sum: number, item: any) => sum + (parseFloat(item.Price) || 0), 0);

              setSales7000OrMore(total3Months >= 7000 ? 'Yes' : 'No');
            }
          } catch (e) {
            console.warn('Error fetching sales summary:', e);
          }
        }
      } else {
        // Fallback check paymententry/invoice endpoint
        const peRes = await fetch(`${API_BASE_URL}/paymententry/invoice/${encodeURIComponent(invoiceNo.trim())}`);
        const peJson = await peRes.json();
        if (peJson.success && peJson.data && peJson.data.length > 0) {
          const inv = peJson.data[0];
          setNetAmount(inv.NetTotal ? String(inv.NetTotal) : '');
          setGstAmount(inv.GST ? String(inv.GST) : '');
          if (inv.InvoiceDate) {
            const parts = String(inv.InvoiceDate).split('/');
            if (parts.length === 3) {
              setInvoiceDay(parts[0]);
              setInvoiceMonth(parts[1]);
              setInvoiceYear(parts[2]);
            }
          }
        } else if (!existingEtrsFound) {
          if (Platform.OS === 'web') {
            window.alert('Invoice not found.');
          } else {
            Alert.alert('Not Found', 'Invoice not found.');
          }
        }
      }
    } catch (err) {
      console.error('Error searching invoice:', err);
      if (Platform.OS === 'web') {
        window.alert('Failed to fetch invoice details.');
      } else {
        Alert.alert('Error', 'Failed to fetch invoice details.');
      }
    } finally {
      setLoadingInvoice(false);
    }
  };

  // Image Upload Handler
  const handleBrowsePhoto = () => {
    if (Platform.OS === 'web') {
      const input = document.createElement('input');
      input.type = 'file';
      input.accept = 'image/*';
      input.onchange = (e: any) => {
        const file = e.target.files?.[0];
        if (file) {
          const reader = new FileReader();
          reader.onload = (event) => {
            setPhotoUri(event.target?.result as string);
          };
          reader.readAsDataURL(file);
        }
      };
      input.click();
    } else {
      Alert.alert('Image Picker', 'Image upload is available on web platform.');
    }
  };

  // Clear Form
  const handleClear = () => {
    setInvoiceNo('');
    setInvoiceDay('');
    setInvoiceMonth('');
    setInvoiceYear('');
    setPassportNo('');
    setNetAmount('');
    setGstAmount('');
    setEtrsDocId('');
    setIssueDate(defaultDateStr);
    setIssueTime(defaultTimeStr);
    setRemarks('');
    setSales7000OrMore('No');
    setIsSealedBag('No');
    setSealedBagSerial('');
    setSelectedStaff('');
    setPhotoUri(null);
  };

  // Save Form
  const handleSave = async () => {
    if (!invoiceNo.trim()) {
      const msg = 'Invoice No is required.';
      Platform.OS === 'web' ? window.alert(msg) : Alert.alert('Validation Error', msg);
      return;
    }
    if (!passportNo.trim()) {
      const msg = 'Please enter Passport No';
      Platform.OS === 'web' ? window.alert(msg) : Alert.alert('Validation Error', msg);
      return;
    }
    if (!etrsDocId.trim()) {
      const msg = 'eTRS Ticket Doc ID is required.';
      Platform.OS === 'web' ? window.alert(msg) : Alert.alert('Validation Error', msg);
      return;
    }

    setSaving(true);
    try {
      // Save via paymententry or custom endpoint if available
      const payload = {
        type: 'eTRS',
        invoiceNo: invoiceNo.trim(),
        day: invoiceDay,
        month: invoiceMonth,
        year: invoiceYear,
        passportNo: passportNo.trim(),
        netAmount,
        gstAmount,
        etrsDocId: etrsDocId.trim(),
        issueDate,
        issueTime,
        remarks,
        sales7000OrMore,
        isSealedBag,
        sealedBagSerial,
        staff: selectedStaff,
        photo: photoUri,
      };

      const res = await fetch(`${API_BASE_URL}/paymententry/save`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload),
      });
      const json = await res.json();

      if (json.success) {
        // Re-fetch saved statement to verify persistence and ensure UI reflects actual DB state
        try {
          const verifyRes = await fetch(`${API_BASE_URL}/paymententry/etrs/${encodeURIComponent(invoiceNo.trim())}`);
          const verifyJson = await verifyRes.json();
          if (verifyJson.success && verifyJson.data && verifyJson.data.CustomerImage) {
            setPhotoUri(verifyJson.data.CustomerImage);
          }
        } catch (vErr) {
          console.warn('Post-save verification query error:', vErr);
        }
        const msg = 'eTRS Statement saved successfully!';
        Platform.OS === 'web' ? window.alert(msg) : Alert.alert('Success', msg);
      } else {
        const msg = json.message || 'Failed to save eTRS Statement.';
        Platform.OS === 'web' ? window.alert(msg) : Alert.alert('Error', msg);
      }
    } catch (err) {
      console.error('Error saving eTRS statement:', err);
      const msg = 'Error saving eTRS Statement.';
      Platform.OS === 'web' ? window.alert(msg) : Alert.alert('Error', msg);
    } finally {
      setSaving(false);
    }
  };

  return (
    <SafeAreaView style={styles.safeArea} edges={['top', 'left', 'right']}>
      <Stack.Screen options={{ headerShown: false }} />

      {/* TOP HEADER BAR */}
      <View style={styles.topHeader}>
        <View style={styles.headerLeft}>
          <TouchableOpacity style={styles.backButton} onPress={() => router.back()}>
            <Ionicons name="arrow-back" size={18} color="#3B2D17" />
          </TouchableOpacity>
          <Text style={styles.headerTitle}>eTRS Statement</Text>
        </View>
      </View>

      {/* MAIN TWO-COLUMN CONTENT */}
      <View style={styles.mainGridContainer}>
        <View style={styles.mainGrid}>
          
          {/* LEFT SECTION: Invoice Details */}
          <View style={styles.cardSection}>
            <View style={styles.sectionHeader}>
              <View style={styles.headerIconBadge}>
                <MaterialCommunityIcons name="file-document-outline" size={14} color="#9E6D08" />
              </View>
              <Text style={styles.sectionTitle}>Invoice Details</Text>
            </View>

            <View style={styles.cardBody}>
              
              {/* 1. Invoice No * */}
              <View style={styles.formRow}>
                <Text style={styles.label}>
                  Invoice No <Text style={styles.requiredStar}>*</Text>
                </Text>
                <View style={styles.inputSearchWrapper}>
                  <TextInput
                    style={[styles.input, { flex: 1, borderTopRightRadius: 0, borderBottomRightRadius: 0 }]}
                    placeholder="Enter Invoice No"
                    placeholderTextColor="#A39682"
                    value={invoiceNo}
                    onChangeText={setInvoiceNo}
                    onSubmitEditing={handleSearchInvoice}
                  />
                  <TouchableOpacity
                    style={styles.searchButton}
                    onPress={handleSearchInvoice}
                    disabled={loadingInvoice}
                  >
                    {loadingInvoice ? (
                      <ActivityIndicator size="small" color="#FFFFFF" />
                    ) : (
                      <Ionicons name="search" size={16} color="#FFFFFF" />
                    )}
                  </TouchableOpacity>
                </View>
              </View>

              {/* 2. Date of Invoice */}
              <View style={styles.formRow}>
                <Text style={styles.label}>Date of Invoice</Text>
                <View style={styles.dateTripleRow}>
                  <View style={styles.dateTripleBox}>
                    <Text style={styles.dateTripleText}>{invoiceDay || 'DD'}</Text>
                    <Ionicons name="caret-down" size={12} color="#8F7E66" />
                  </View>
                  <Text style={styles.dateSeparator}>-</Text>
                  <View style={styles.dateTripleBox}>
                    <Text style={styles.dateTripleText}>{invoiceMonth || 'MM'}</Text>
                    <Ionicons name="caret-down" size={12} color="#8F7E66" />
                  </View>
                  <Text style={styles.dateSeparator}>-</Text>
                  <View style={styles.dateTripleBox}>
                    <Text style={styles.dateTripleText}>{invoiceYear || 'YYYY'}</Text>
                    <Ionicons name="caret-down" size={12} color="#8F7E66" />
                  </View>
                </View>
              </View>

              {/* 3. Passport No * */}
              <View style={styles.formRow}>
                <Text style={styles.label}>
                  Passport No <Text style={styles.requiredStar}>*</Text>
                </Text>
                <TextInput
                  style={styles.input}
                  placeholder="Enter Passport No"
                  placeholderTextColor="#A39682"
                  value={passportNo}
                  onChangeText={setPassportNo}
                />
              </View>

              {/* 4. Net Amount */}
              <View style={styles.formRow}>
                <Text style={styles.label}>Net Amount</Text>
                <TextInput
                  style={[styles.input, styles.readOnlyInput]}
                  placeholder="Enter Net Amount"
                  placeholderTextColor="#A39682"
                  value={netAmount}
                  editable={false}
                />
              </View>

              {/* 5. GST Amount */}
              <View style={styles.formRow}>
                <Text style={styles.label}>GST Amount</Text>
                <TextInput
                  style={[styles.input, styles.readOnlyInput]}
                  placeholder="Enter GST Amount"
                  placeholderTextColor="#A39682"
                  value={gstAmount}
                  editable={false}
                />
              </View>

              {/* 6. eTRS Ticket Doc ID * */}
              <View style={styles.formRow}>
                <Text style={styles.label}>
                  eTRS Ticket Doc ID <Text style={styles.requiredStar}>*</Text>
                </Text>
                <TextInput
                  style={styles.input}
                  placeholder="Enter eTRS Ticket Doc ID"
                  placeholderTextColor="#A39682"
                  value={etrsDocId}
                  onChangeText={setEtrsDocId}
                />
              </View>

              {/* 7. eTRS Issue Date * */}
              <View style={styles.formRow}>
                <Text style={styles.label}>
                  eTRS Issue Date <Text style={styles.requiredStar}>*</Text>
                </Text>
                <View style={styles.iconInputWrapper}>
                  <TextInput
                    style={[styles.input, { flex: 1 }]}
                    value={issueDate}
                    onChangeText={setIssueDate}
                    placeholderTextColor="#A39682"
                  />
                  <View style={styles.rightInputIconBtn}>
                    <Ionicons name="calendar-outline" size={16} color="#8F7E66" />
                  </View>
                </View>
              </View>

              {/* 8. eTRS Issue Time * */}
              <View style={styles.formRow}>
                <Text style={styles.label}>
                  eTRS Issue Time <Text style={styles.requiredStar}>*</Text>
                </Text>
                <View style={styles.iconInputWrapper}>
                  <TextInput
                    style={[styles.input, { flex: 1 }]}
                    value={issueTime}
                    onChangeText={setIssueTime}
                    placeholderTextColor="#A39682"
                  />
                  <View style={styles.rightInputIconBtn}>
                    <Ionicons name="time-outline" size={16} color="#8F7E66" />
                  </View>
                </View>
              </View>

              {/* 9. Remarks */}
              <View style={[styles.formRow, styles.remarksRow]}>
                <Text style={[styles.label, styles.remarksLabel]}>Remarks</Text>
                <TextInput
                  style={[styles.input, styles.multilineInput]}
                  placeholder="Enter Remarks"
                  placeholderTextColor="#A39682"
                  multiline
                  numberOfLines={2}
                  value={remarks}
                  onChangeText={setRemarks}
                />
              </View>

            </View>
          </View>


          {/* RIGHT SECTION: Additional Information */}
          <View style={styles.cardSection}>
            <View style={styles.sectionHeader}>
              <View style={styles.headerIconBadge}>
                <MaterialCommunityIcons name="shield-outline" size={14} color="#9E6D08" />
              </View>
              <Text style={styles.sectionTitle}>Additional Information</Text>
            </View>

            <View style={styles.cardBody}>

              {/* 1. Total Value of Sales Last 3 Months */}
              <View style={styles.radioRowLayout}>
                <Text style={[styles.label, { flex: 1, paddingRight: 10 }]}>
                  Total Value of the Sales for Last 3 Months is $7000 or More
                </Text>
                <View style={styles.radioOptionsGroup}>
                  <TouchableOpacity
                    style={styles.radioButton}
                    onPress={() => setSales7000OrMore('Yes')}
                  >
                    <View style={[styles.radioOuter, sales7000OrMore === 'Yes' && styles.radioOuterSelected]}>
                      {sales7000OrMore === 'Yes' && <View style={styles.radioInner} />}
                    </View>
                    <Text style={styles.radioText}>Yes</Text>
                  </TouchableOpacity>

                  <TouchableOpacity
                    style={styles.radioButton}
                    onPress={() => setSales7000OrMore('No')}
                  >
                    <View style={[styles.radioOuter, sales7000OrMore === 'No' && styles.radioOuterSelected]}>
                      {sales7000OrMore === 'No' && <View style={styles.radioInner} />}
                    </View>
                    <Text style={styles.radioText}>No</Text>
                  </TouchableOpacity>
                </View>
              </View>

              {/* 2. Are Jewel Items Packed in Sealed bag */}
              <View style={styles.radioRowLayout}>
                <Text style={[styles.label, { flex: 1, paddingRight: 10 }]}>
                  Are Jewel Items Packed in Sealed bag
                </Text>
                <View style={styles.radioOptionsGroup}>
                  <TouchableOpacity
                    style={styles.radioButton}
                    onPress={() => setIsSealedBag('Yes')}
                  >
                    <View style={[styles.radioOuter, isSealedBag === 'Yes' && styles.radioOuterSelected]}>
                      {isSealedBag === 'Yes' && <View style={styles.radioInner} />}
                    </View>
                    <Text style={styles.radioText}>Yes</Text>
                  </TouchableOpacity>

                  <TouchableOpacity
                    style={styles.radioButton}
                    onPress={() => {
                      setIsSealedBag('No');
                      setSealedBagSerial('');
                    }}
                  >
                    <View style={[styles.radioOuter, isSealedBag === 'No' && styles.radioOuterSelected]}>
                      {isSealedBag === 'No' && <View style={styles.radioInner} />}
                    </View>
                    <Text style={styles.radioText}>No</Text>
                  </TouchableOpacity>
                </View>
              </View>

              {/* 3. Serial Number of Sealed Bag (If Applicable) */}
              <View style={styles.formRow}>
                <Text style={styles.label}>Serial Number of Sealed Bag (If Applicable)</Text>
                <TextInput
                  style={[
                    styles.input,
                    isSealedBag === 'No' && styles.disabledInput,
                  ]}
                  placeholder="Enter Serial Number"
                  placeholderTextColor="#A39682"
                  value={sealedBagSerial}
                  onChangeText={setSealedBagSerial}
                  editable={isSealedBag === 'Yes'}
                />
              </View>

              {/* 4. Name of Staff (eTRS IC) */}
              <View style={[styles.formRow, { zIndex: 100 }]}>
                <Text style={styles.label}>Name of Staff (eTRS IC)</Text>
                <View style={{ flex: 1, zIndex: 100 }}>
                  <TouchableOpacity
                    style={styles.dropdownSelect}
                    onPress={() => setStaffDropdownOpen(!staffDropdownOpen)}
                  >
                    <Text style={selectedStaff ? styles.dropdownSelectedText : styles.dropdownPlaceholderText}>
                      {selectedStaff || 'Select Staff'}
                    </Text>
                    <Ionicons name="chevron-down" size={16} color="#8F7E66" />
                  </TouchableOpacity>

                  {staffDropdownOpen && (
                    <View style={styles.dropdownListContainer}>
                      <ScrollView nestedScrollEnabled style={{ maxHeight: 150 }}>
                        <TouchableOpacity
                          style={styles.dropdownItem}
                          onPress={() => {
                            setSelectedStaff('');
                            setStaffDropdownOpen(false);
                          }}
                        >
                          <Text style={styles.dropdownItemText}>Select Staff</Text>
                        </TouchableOpacity>
                        {staffList.map((item, idx) => {
                          const name = item.SalesManName || item.SalesManCode;
                          return (
                            <TouchableOpacity
                              key={idx}
                              style={styles.dropdownItem}
                              onPress={() => {
                                setSelectedStaff(name);
                                setStaffDropdownOpen(false);
                              }}
                            >
                              <Text style={styles.dropdownItemText}>{name}</Text>
                            </TouchableOpacity>
                          );
                        })}
                      </ScrollView>
                    </View>
                  )}
                </View>
              </View>

              {/* 5. Customer Photo Area */}
              <View style={styles.photoContainerRow}>
                <Text style={[styles.label, { marginTop: 8 }]}>Customer Photo</Text>

                <View style={styles.photoUploadDashedBox}>
                  {photoUri ? (
                    <View style={styles.photoPreviewWrapper}>
                      <Image source={{ uri: photoUri }} style={styles.photoPreviewImage} />
                      <TouchableOpacity style={styles.browseButton} onPress={handleBrowsePhoto}>
                        <MaterialCommunityIcons name="image" size={16} color="#FFFFFF" style={{ marginRight: 4 }} />
                        <Text style={styles.browseButtonText}>Replace</Text>
                      </TouchableOpacity>
                    </View>
                  ) : (
                    <View style={styles.photoEmptyContent}>
                      <View style={styles.photoIconCircle}>
                        <MaterialCommunityIcons name="image-outline" size={26} color="#9E6D08" />
                      </View>
                      <Text style={styles.noImageText}>No image selected</Text>
                      <TouchableOpacity style={styles.browseButton} onPress={handleBrowsePhoto}>
                        <MaterialCommunityIcons name="folder-open" size={16} color="#FFFFFF" style={{ marginRight: 6 }} />
                        <Text style={styles.browseButtonText}>Browse</Text>
                      </TouchableOpacity>
                    </View>
                  )}
                </View>
              </View>

            </View>
          </View>

        </View>
      </View>

      {/* BOTTOM ACTION BAR */}
      <View style={styles.bottomActionBar}>
        <View style={styles.actionButtonsRow}>
          
          {/* CLEAR (F6) */}
          <TouchableOpacity style={styles.clearButton} onPress={handleClear}>
            <Text style={styles.clearButtonText}>Clear</Text>
          </TouchableOpacity>

          {/* SAVE (F7) */}
          <TouchableOpacity style={styles.saveButton} onPress={handleSave} disabled={saving}>
            {saving && (
              <ActivityIndicator size="small" color="#FFFFFF" style={{ marginRight: 6 }} />
            )}
            <Text style={styles.saveButtonText}>Save</Text>
          </TouchableOpacity>

          {/* CLOSE (Esc) */}
          <TouchableOpacity style={styles.closeButton} onPress={() => router.back()}>
            <Text style={styles.closeButtonText}>Close</Text>
          </TouchableOpacity>

        </View>
      </View>

    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  safeArea: {
    flex: 1,
    backgroundColor: '#FBF9F5',
  },
  topHeader: {
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
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: '#FFFFFF',
    marginRight: 10,
  },
  headerTitle: {
    fontSize: 18,
    fontWeight: 'bold',
    color: '#3B2D17',
    fontFamily: Platform.OS === 'web' ? 'Cambria, Georgia, serif' : undefined,
  },
  mainGridContainer: {
    flex: 1,
    padding: 14,
  },
  mainGrid: {
    flex: 1,
    flexDirection: 'row',
    gap: 14,
  },
  cardSection: {
    flex: 1,
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    overflow: 'visible',
  },
  sectionHeader: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 12,
    paddingVertical: 7,
    backgroundColor: '#F5E7CA',
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    borderTopLeftRadius: 7,
    borderTopRightRadius: 7,
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
  sectionTitle: {
    fontSize: 13,
    fontWeight: 'bold',
    color: '#3B2D17',
    letterSpacing: 0.3,
  },
  cardBody: {
    paddingHorizontal: 14,
    paddingTop: 10,
    paddingBottom: 14,
    gap: 7,
    flex: 1,
    justifyContent: 'flex-start',
  },
  formRow: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
  },
  label: {
    fontSize: 11.5,
    fontWeight: 'bold',
    color: '#3B2D17',
    flex: 0.42,
    paddingRight: 6,
  },
  requiredStar: {
    color: '#B91C1C',
  },
  input: {
    flex: 0.58,
    height: 32,
    borderWidth: 1,
    borderColor: '#DFD5C3',
    borderRadius: 6,
    paddingHorizontal: 10,
    fontSize: 12,
    color: '#3B2D17',
    backgroundColor: '#FFFFFF',
    ...(Platform.OS === 'web' ? ({ outlineStyle: 'none' } as any) : {}),
  },
  readOnlyInput: {
    backgroundColor: '#FAF7F0',
    color: '#8F7E66',
    borderColor: '#EADBCA',
  },
  disabledInput: {
    backgroundColor: '#FAF7F0',
    borderColor: '#EADBCA',
    color: '#A39682',
  },
  remarksRow: {
    alignItems: 'flex-start',
  },
  remarksLabel: {
    paddingTop: 6,
  },
  multilineInput: {
    height: 44,
    textAlignVertical: 'top',
    paddingTop: 6,
    paddingBottom: 6,
  },
  inputSearchWrapper: {
    flex: 0.58,
    flexDirection: 'row',
    alignItems: 'center',
  },
  searchButton: {
    width: 38,
    height: 32,
    backgroundColor: '#9E6D08',
    borderTopRightRadius: 6,
    borderBottomRightRadius: 6,
    justifyContent: 'center',
    alignItems: 'center',
  },
  dateTripleRow: {
    flex: 0.58,
    flexDirection: 'row',
    alignItems: 'center',
    gap: 4,
  },
  dateTripleBox: {
    flex: 1,
    height: 32,
    borderWidth: 1,
    borderColor: '#DFD5C3',
    borderRadius: 6,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingHorizontal: 8,
    backgroundColor: '#FFFFFF',
  },
  dateTripleText: {
    fontSize: 11.5,
    color: '#8F7E66',
    fontWeight: '500',
  },
  dateSeparator: {
    fontSize: 12,
    color: '#8F7E66',
    fontWeight: 'bold',
  },
  iconInputWrapper: {
    flex: 0.58,
    flexDirection: 'row',
    alignItems: 'center',
    position: 'relative',
  },
  rightInputIconBtn: {
    position: 'absolute',
    right: 8,
    height: 32,
    justifyContent: 'center',
    alignItems: 'center',
  },
  radioRowLayout: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
  },
  radioOptionsGroup: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 20,
  },
  radioButton: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  radioOuter: {
    width: 16,
    height: 16,
    borderRadius: 8,
    borderWidth: 1.5,
    borderColor: '#C4B59D',
    justifyContent: 'center',
    alignItems: 'center',
    marginRight: 6,
  },
  radioOuterSelected: {
    borderColor: '#9E6D08',
  },
  radioInner: {
    width: 8,
    height: 8,
    borderRadius: 4,
    backgroundColor: '#9E6D08',
  },
  radioText: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#3B2D17',
  },
  dropdownSelect: {
    height: 32,
    borderWidth: 1,
    borderColor: '#DFD5C3',
    borderRadius: 6,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingHorizontal: 10,
    backgroundColor: '#FFFFFF',
  },
  dropdownSelectedText: {
    fontSize: 12,
    color: '#3B2D17',
    fontWeight: '500',
  },
  dropdownPlaceholderText: {
    fontSize: 12,
    color: '#A39682',
  },
  dropdownListContainer: {
    position: 'absolute',
    top: 40,
    left: 0,
    right: 0,
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#E8DCB8',
    borderRadius: 6,
    elevation: 8,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.08,
    shadowRadius: 8,
    zIndex: 1000,
  },
  dropdownItem: {
    paddingHorizontal: 12,
    paddingVertical: 8,
    borderBottomWidth: 1,
    borderBottomColor: '#FAF7F0',
  },
  dropdownItemText: {
    fontSize: 12,
    color: '#3B2D17',
  },
  photoContainerRow: {
    flexDirection: 'row',
    alignItems: 'flex-start',
    justifyContent: 'space-between',
    marginTop: 4,
  },
  photoUploadDashedBox: {
    flex: 0.58,
    height: 120,
    borderRadius: 8,
    borderWidth: 1.5,
    borderColor: '#E8DCB8',
    borderStyle: 'dashed',
    backgroundColor: '#FAF7F0',
    justifyContent: 'center',
    alignItems: 'center',
    padding: 8,
  },
  photoEmptyContent: {
    alignItems: 'center',
  },
  photoIconCircle: {
    width: 38,
    height: 38,
    borderRadius: 19,
    backgroundColor: '#FFF8EA',
    borderWidth: 1,
    borderColor: '#E2D2AF',
    justifyContent: 'center',
    alignItems: 'center',
    marginBottom: 4,
  },
  noImageText: {
    fontSize: 11,
    color: '#8F7E66',
    marginBottom: 6,
    fontWeight: '500',
  },
  photoPreviewWrapper: {
    alignItems: 'center',
    width: '100%',
    height: '100%',
    justifyContent: 'center',
  },
  photoPreviewImage: {
    width: 70,
    height: 70,
    borderRadius: 6,
    marginBottom: 4,
  },
  browseButton: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#9E6D08',
    paddingHorizontal: 14,
    paddingVertical: 6,
    borderRadius: 6,
  },
  browseButtonText: {
    color: '#FFFFFF',
    fontSize: 12,
    fontWeight: 'bold',
  },
  bottomActionBar: {
    height: 50,
    paddingHorizontal: 16,
    backgroundColor: '#FFFFFF',
    borderTopWidth: 1,
    borderTopColor: '#E8DCB8',
    alignItems: 'flex-end',
    justifyContent: 'center',
  },
  actionButtonsRow: {
    flexDirection: 'row',
    gap: 10,
  },
  clearButton: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 18,
    height: 36,
    borderRadius: 6,
    borderWidth: 1,
    borderColor: '#DFD5C3',
    backgroundColor: '#FFFFFF',
  },
  clearButtonText: {
    fontSize: 12.5,
    fontWeight: 'bold',
    color: '#3B2D17',
  },
  saveButton: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 22,
    height: 36,
    borderRadius: 6,
    backgroundColor: '#9E6D08',
  },
  saveButtonText: {
    fontSize: 12.5,
    fontWeight: 'bold',
    color: '#FFFFFF',
  },
  closeButton: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 18,
    height: 36,
    borderRadius: 6,
    borderWidth: 1,
    borderColor: '#DFD5C3',
    backgroundColor: '#FFFFFF',
  },
  closeButtonText: {
    fontSize: 12.5,
    fontWeight: 'bold',
    color: '#3B2D17',
  },
});

