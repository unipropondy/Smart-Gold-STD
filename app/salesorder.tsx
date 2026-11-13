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

  // Confirmation Modal for post-save "Do you want to print?"
  const [printConfirmOrderNo, setPrintConfirmOrderNo] = useState<string | null>(null);

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

  // subPrintSalesOrder migrated from frmSalesOrder.vb
  const subPrintSalesOrder = async (sTransferNo: string) => {
    const targetNo = (sTransferNo || '').trim();
    if (!targetNo) {
      showToast('SalesOrder Number Should not be Empty', 'error');
      return;
    }

    try {
      setLoading(true);
      const res = await fetch(`${API_URL}/print-data/${encodeURIComponent(targetNo)}`);
      const result = await res.json();

      if (!result.success || !result.data) {
        showToast(result.message || `No Sales Order records found for: ${targetNo}`, 'error');
        return;
      }

      const { company, formulas, header, details } = result.data;

      // Extract Company and Formula values matching Crystal Report:
      // CompAddress1: sCompanyname
      // CompAddress2: Address1 & " " & Address2 & " " & Country
      // CompAddress3: sPhone
      // CompAddress4: sFax
      // ReportTitle: "Transfer Note"
      // RPTSELECTION: sSelectFld
      const compAddress1 = formulas?.CompAddress1 || company?.name || 'SMART';
      const compAddress2 = formulas?.CompAddress2 || [company?.address1, company?.address2, company?.country].filter(Boolean).join(' ');
      const compAddress3 = formulas?.CompAddress3 || company?.phone || '';
      const compAddress4 = formulas?.CompAddress4 || company?.fax || '';
      const reportTitle = formulas?.ReportTitle || 'Transfer Note';

      // Format Date strings
      const rawSODate = header.SalesOrderDate || header.CreateDate;
      let soDateFormatted = '';
      if (rawSODate) {
        const d = new Date(rawSODate);
        if (!isNaN(d.getTime())) {
          soDateFormatted = `${String(d.getDate()).padStart(2, '0')}/${String(d.getMonth() + 1).padStart(2, '0')}/${d.getFullYear()}`;
        } else {
          soDateFormatted = String(rawSODate);
        }
      }

      const rawDueDate = header.DueDate;
      let dueDateFormatted = '';
      if (rawDueDate) {
        const d = new Date(rawDueDate);
        if (!isNaN(d.getTime())) {
          dueDateFormatted = `${String(d.getDate()).padStart(2, '0')}/${String(d.getMonth() + 1).padStart(2, '0')}/${d.getFullYear()}`;
        } else {
          dueDateFormatted = String(rawDueDate);
        }
      }

      const now = new Date();
      const printTimeStr = `${String(now.getDate()).padStart(2, '0')}/${String(now.getMonth() + 1).padStart(2, '0')}/${now.getFullYear()} ${now.toLocaleTimeString()}`;

      // Calculate totals
      let totalQty = 0;
      let totalWeight = 0;
      (details || []).forEach((row: any) => {
        totalQty += parseFloat(row.SalesOrderQty) || 0;
        totalWeight += parseFloat(row.Weight) || 0;
      });

      const printHtml = `
        <!DOCTYPE html>
        <html>
          <head>
            <meta charset="utf-8" />
            <title>Sales Order / ${reportTitle} - ${targetNo}</title>
            <style>
              @page {
                size: A4 portrait;
                margin: 0;
              }
              *, *:before, *:after {
                box-sizing: border-box;
              }
              body {
                font-family: 'Segoe UI', Tahoma, Arial, sans-serif;
                margin: 0;
                padding: 0;
                background-color: #525659;
                color: #222;
                -webkit-print-color-adjust: exact;
                print-color-adjust: exact;
              }
              .page-sheet {
                background: #FFFFFF;
                width: 210mm;
                min-height: 297mm;
                margin: 10mm auto;
                padding: 12mm 15mm;
                box-shadow: 0 0 14px rgba(0,0,0,0.4);
                display: flex;
                flex-direction: column;
                justify-content: space-between;
              }
              @media print {
                body {
                  background: #FFFFFF;
                }
                .page-sheet {
                  margin: 0;
                  padding: 10mm 12mm;
                  width: 100%;
                  min-height: 100%;
                  box-shadow: none;
                }
              }

              /* HEADER SECTION (clsCompany1.getCompanyName formulas) */
              .comp-header {
                text-align: center;
                border-bottom: 2px solid #9E741D;
                padding-bottom: 8px;
                margin-bottom: 10px;
              }
              .comp-name {
                font-size: 20px;
                font-weight: 800;
                letter-spacing: 1.5px;
                color: #5A3F0C;
                text-transform: uppercase;
                margin-bottom: 3px;
              }
              .comp-details {
                font-size: 11px;
                color: #444;
                line-height: 1.4;
              }

              /* TITLE BADGE */
              .doc-badge-container {
                display: flex;
                justify-content: space-between;
                align-items: center;
                background: #FBF6EC;
                border: 1px solid #E2D3B3;
                border-radius: 6px;
                padding: 8px 14px;
                margin-bottom: 12px;
              }
              .doc-title {
                font-size: 15px;
                font-weight: 800;
                color: #8C5D08;
                letter-spacing: 1px;
                text-transform: uppercase;
              }
              .doc-print-time {
                font-size: 10px;
                color: #77664B;
              }

              /* INFO GRID */
              .info-grid {
                display: grid;
                grid-template-columns: 1fr 1fr;
                gap: 12px;
                margin-bottom: 14px;
              }
              .info-card {
                border: 1px solid #E6DAC5;
                background: #FAF8F5;
                border-radius: 5px;
                padding: 8px 12px;
              }
              .card-title {
                font-size: 11px;
                font-weight: 700;
                color: #7A5716;
                border-bottom: 1px solid #ECE1CD;
                padding-bottom: 3px;
                margin-bottom: 6px;
                text-transform: uppercase;
                letter-spacing: 0.5px;
              }
              .info-row {
                display: flex;
                font-size: 11px;
                margin-bottom: 3px;
                line-height: 1.35;
              }
              .info-lbl {
                width: 110px;
                font-weight: 600;
                color: #665235;
                flex-shrink: 0;
              }
              .info-val {
                color: #1A1A1A;
                flex-grow: 1;
                word-break: break-word;
              }

              /* DETAIL TABLE (rptSalesOrderDetail) */
              .detail-table {
                width: 100%;
                border-collapse: collapse;
                margin-bottom: 12px;
                font-size: 11px;
              }
              .detail-table th {
                background: #8C5D08;
                color: #FFFFFF;
                text-align: left;
                padding: 6px 8px;
                font-weight: 700;
                border: 1px solid #734C06;
                text-transform: uppercase;
                font-size: 10px;
                letter-spacing: 0.5px;
              }
              .detail-table td {
                padding: 6px 8px;
                border: 1px solid #E8DECC;
                background: #FFFFFF;
              }
              .detail-table tr:nth-child(even) td {
                background: #FAF8F5;
              }

              /* TOTALS SECTION */
              .totals-section {
                display: flex;
                justify-content: flex-end;
                margin-bottom: 14px;
              }
              .totals-box {
                width: 260px;
                border: 1px solid #E6DAC5;
                background: #FAF8F5;
                border-radius: 5px;
                padding: 8px 12px;
              }
              .total-row {
                display: flex;
                justify-content: space-between;
                font-size: 11px;
                padding: 3px 0;
                color: #444;
              }
              .total-row-grand {
                border-top: 2px solid #8C5D08;
                margin-top: 4px;
                padding-top: 4px;
                font-weight: 800;
                font-size: 13px;
                color: #8C5D08;
              }

              /* SIGNATURES */
              .sign-grid {
                display: grid;
                grid-template-columns: 1fr 1fr;
                gap: 25px;
                margin-top: 15px;
              }
              .sign-card {
                padding: 6px 10px;
                border: 1px solid #EFE4D0;
                background: #FAF8F5;
                border-radius: 4px;
              }
              .sign-title {
                font-size: 10px;
                font-weight: 700;
                color: #7A5716;
                text-transform: uppercase;
                margin-bottom: 30px;
              }
              .sign-line {
                border-bottom: 1px solid #444;
                margin-bottom: 4px;
              }
              .sign-sub {
                font-size: 10px;
                color: #555;
                display: flex;
                justify-content: space-between;
              }

              /* FOOTER */
              .footer-bar {
                margin-top: 14px;
                text-align: center;
                border-top: 1px solid #E2D3B3;
                padding-top: 8px;
                font-size: 10px;
                color: #7C694F;
              }
            </style>
          </head>
          <body>
            <div class="page-sheet">
              <div>
                <!-- COMPANY HEADER (clsCompany1.getCompanyName) -->
                <div class="comp-header">
                  <div class="comp-name">${compAddress1}</div>
                  <div class="comp-details">
                    ${compAddress2 ? `<div>${compAddress2}</div>` : ''}
                    ${compAddress3 || compAddress4 ? `<div>${compAddress3 ? 'Tel: ' + compAddress3 : ''} ${compAddress4 ? ' | Fax: ' + compAddress4 : ''}</div>` : ''}
                  </div>
                </div>

                <!-- TITLE BADGE -->
                <div class="doc-badge-container">
                  <div class="doc-title">${reportTitle} / SALES ORDER</div>
                  <div class="doc-print-time">Printed: ${printTimeStr} (1 Copy)</div>
                </div>

                <!-- INFO GRID -->
                <div class="info-grid">
                  <!-- ORDER IDENTIFICATION -->
                  <div class="info-card">
                    <div class="card-title">SALES ORDER INFORMATION</div>
                    <div class="info-row"><span class="info-lbl">Sales Order No:</span><span class="info-val font-bold">${targetNo}</span></div>
                    <div class="info-row"><span class="info-lbl">Order Date:</span><span class="info-val">${soDateFormatted || '-'}</span></div>
                    <div class="info-row"><span class="info-lbl">Due Date:</span><span class="info-val">${dueDateFormatted || '-'}</span></div>
                    <div class="info-row"><span class="info-lbl">Location Code:</span><span class="info-val">${(header.LocationCode || '').trim() || '-'}</span></div>
                  </div>

                  <!-- SUPPLIER & AUDIT -->
                  <div class="info-card">
                    <div class="card-title">SUPPLIER & PARTICULARS</div>
                    <div class="info-row"><span class="info-lbl">Supplier Code:</span><span class="info-val font-bold">${(header.SupplierCode || '').trim() || '-'}</span></div>
                    ${header.SupplierName ? `<div class="info-row"><span class="info-lbl">Supplier Name:</span><span class="info-val">${(header.SupplierName || '').trim()}</span></div>` : ''}
                    <div class="info-row"><span class="info-lbl">Remarks:</span><span class="info-val">${header.Remarks || '-'}</span></div>
                    <div class="info-row"><span class="info-lbl">Created By:</span><span class="info-val">${(header.CreateUser || 'sys').trim()}</span></div>
                  </div>
                </div>

                <!-- DETAIL ITEMS TABLE (rptSalesOrderDetail) -->
                <table class="detail-table">
                  <thead>
                    <tr>
                      <th style="width: 5%; text-align: center;">#</th>
                      <th style="width: 18%;">Category</th>
                      <th style="width: 22%;">Catalogue</th>
                      <th style="width: 12%; text-align: right;">Weight (g)</th>
                      <th style="width: 12%; text-align: right;">To Wt (g)</th>
                      <th style="width: 10%; text-align: center;">Size</th>
                      <th style="width: 10%; text-align: right;">ReOrder</th>
                      <th style="width: 11%; text-align: right;">SO Qty</th>
                    </tr>
                  </thead>
                  <tbody>
                    ${(details || []).length > 0 ? (details || []).map((row: any, idx: number) => {
                      const catCode = (row.CategoryCode || '').trim();
                      const catgCode = (row.CatalogueCode || '').trim();
                      const catName = row.CategoryName ? ` (${row.CategoryName.trim()})` : '';
                      const catgName = row.CatalogueName ? ` (${row.CatalogueName.trim()})` : '';
                      const wt = parseFloat(row.Weight) || 0;
                      const toWt = parseFloat(row.ToWeight) || 0;
                      const sz = row.SizeCode !== null && row.SizeCode !== undefined ? row.SizeCode : '-';
                      const szType = row.SizeType ? ` ${row.SizeType.trim()}` : '';
                      const reorder = parseFloat(row.ReOrderLevel) || 0;
                      const soQty = parseFloat(row.SalesOrderQty) || 0;

                      return `
                        <tr>
                          <td style="text-align: center;">${idx + 1}</td>
                          <td><strong>${catCode}</strong>${catName}</td>
                          <td><strong>${catgCode}</strong>${catgName}</td>
                          <td style="text-align: right;">${wt.toFixed(3)}</td>
                          <td style="text-align: right;">${toWt.toFixed(3)}</td>
                          <td style="text-align: center;">${sz}${szType}</td>
                          <td style="text-align: right;">${reorder}</td>
                          <td style="text-align: right; font-weight: 700; color: #8C5D08;">${soQty}</td>
                        </tr>
                      `;
                    }).join('') : `
                      <tr>
                        <td colspan="8" style="text-align: center; padding: 14px; color: #777;">
                          No detail records in this Sales Order
                        </td>
                      </tr>
                    `}
                  </tbody>
                </table>

                <!-- TOTALS SECTION -->
                <div class="totals-section">
                  <div class="totals-box">
                    <div class="total-row">
                      <span>Total Items:</span>
                      <span style="font-weight: 600;">${(details || []).length}</span>
                    </div>
                    <div class="total-row">
                      <span>Total Weight:</span>
                      <span style="font-weight: 600;">${totalWeight.toFixed(3)} g</span>
                    </div>
                    <div class="total-row total-row-grand">
                      <span>TOTAL SO QTY:</span>
                      <span>${totalQty}</span>
                    </div>
                  </div>
                </div>
              </div>

              <div>
                <!-- SIGNATURES -->
                <div class="sign-grid">
                  <div class="sign-card">
                    <div class="sign-title">Prepared By (Staff)</div>
                    <div class="sign-line"></div>
                    <div class="sign-sub">
                      <span>Staff: ${(header.CreateUser || 'sys').trim()}</span>
                      <span>Date: ........................</span>
                    </div>
                  </div>
                  <div class="sign-card">
                    <div class="sign-title">Authorized Signatory / Supplier</div>
                    <div class="sign-line"></div>
                    <div class="sign-sub">
                      <span>Signature</span>
                      <span>Date: ........................</span>
                    </div>
                  </div>
                </div>

                <!-- FOOTER -->
                <div class="footer-bar">
                  *** Official Sales Order / Transfer Note - System Generated by SmartGold POS ***
                </div>
              </div>
            </div>

            <script>
              window.onload = function() {
                setTimeout(function() {
                  window.print();
                }, 300);
              };
            </script>
          </body>
        </html>
      `;

      if (Platform.OS === 'web' && typeof window !== 'undefined') {
        const printWindow = window.open('', '_blank', 'width=900,height=750');
        if (printWindow) {
          printWindow.document.open();
          printWindow.document.write(printHtml);
          printWindow.document.close();
          showToast(`Sales Order ${targetNo} print preview opened`);
        } else {
          const blob = new Blob([printHtml], { type: 'text/html;charset=utf-8;' });
          const url = URL.createObjectURL(blob);
          const a = document.createElement('a');
          a.href = url;
          a.target = '_blank';
          document.body.appendChild(a);
          a.click();
          document.body.removeChild(a);
          URL.revokeObjectURL(url);
          showToast(`Sales Order ${targetNo} print preview opened`);
        }
      } else {
        showToast(`Sales Order ${targetNo} sent to printer.`, 'success');
      }
    } catch (err: any) {
      console.error(err);
      showToast('Failed to load Sales Order print data', 'error');
    } finally {
      setLoading(false);
    }
  };

  // RePrint Button Click: Check whether Sales Order Number is available. If empty, do not execute.
  const handleRePrint = () => {
    const trimmedNo = (soNo || '').trim();
    if (trimmedNo !== '') {
      subPrintSalesOrder(trimmedNo);
    } else {
      showToast('SalesOrder Number Should not be Empty', 'error');
    }
  };

  const handleSave = async () => {
    if (!locationCode.trim()) {
      showToast('LocationCode Should not be Empty', 'error');
      return;
    }
    if (!supplierCode.trim()) {
      showToast('SupplierCode Should not be Empty', 'error');
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
          showToast(`Please take note reference no. ${savedSoNo}`, 'success');
          // Display the confirmation message: "Do you want to print?"
          setPrintConfirmOrderNo(savedSoNo);
          handleNew();
        } else {
          const errDetail = json.error ? ` - ${json.error}` : '';
          showToast(`${json.message || 'Failed to save Sales Order'}${errDetail}`, 'error');
        }
      } catch (e: any) {
        console.error(e);
        showToast(e.message || 'Network error when saving Sales Order', 'error');
      } finally {
        setSaving(false);
      }
    };

    await performSave();
  };

  const handleDelete = async () => {
    if (!soNo.trim()) {
      showToast('SalesOrder Number Should not be Empty', 'error');
      return;
    }

    const executeDelete = async () => {
      setLoading(true);
      try {
        const res = await fetch(`${API_URL}/${soNo}`, { method: 'DELETE' });
        const json = await res.json();
        if (json.success) {
          showToast('Sales Order Deleted', 'success');
          handleNew();
        } else {
          showToast(json.message || 'Could not delete Sales Order', 'error');
        }
      } catch (e) {
        console.error(e);
        showToast('Network error when deleting Sales Order', 'error');
      } finally {
        setLoading(false);
      }
    };

    await executeDelete();
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
            onPress={handleRePrint}
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

      {/* Confirmation Modal for Print after Save */}
      <Modal visible={printConfirmOrderNo !== null} transparent animationType="fade">
        <TouchableOpacity style={styles.modalOverlay} activeOpacity={1}>
          <TouchableWithoutFeedback>
            <View style={[styles.modalCard, { maxWidth: 360 }]}>
              <View style={styles.modalHeader}>
                <ThemedText style={styles.modalTitle}>Print Option</ThemedText>
              </View>
              <View style={{ padding: 18, alignItems: 'center' }}>
                <Ionicons name="print-outline" size={36} color="#8D6409" style={{ marginBottom: 12 }} />
                <ThemedText style={{ fontSize: 13, color: '#3B2D17', textAlign: 'center', marginBottom: 16 }}>
                  Do you want to print ?
                </ThemedText>
                <View style={{ flexDirection: 'row', gap: 12, width: '100%' }}>
                  <TouchableOpacity
                    style={[styles.modalActionBtn, styles.modalBtnCancel]}
                    onPress={() => setPrintConfirmOrderNo(null)}
                    activeOpacity={0.7}
                  >
                    <ThemedText style={styles.modalBtnCancelText}>No</ThemedText>
                  </TouchableOpacity>
                  <TouchableOpacity
                    style={[styles.modalActionBtn, styles.modalBtnPrimary]}
                    onPress={() => {
                      const orderNo = printConfirmOrderNo;
                      setPrintConfirmOrderNo(null);
                      if (orderNo) {
                        subPrintSalesOrder(orderNo);
                      }
                    }}
                    activeOpacity={0.7}
                  >
                    <ThemedText style={styles.modalBtnPrimaryText}>Yes</ThemedText>
                  </TouchableOpacity>
                </View>
              </View>
            </View>
          </TouchableWithoutFeedback>
        </TouchableOpacity>
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
  modalActionBtn: {
    flex: 1,
    paddingVertical: 8,
    borderRadius: 6,
    alignItems: 'center',
    justifyContent: 'center',
  },
  modalBtnCancel: {
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#DFD5C3',
  },
  modalBtnCancelText: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#7D6F5B',
  },
  modalBtnPrimary: {
    backgroundColor: '#B48218',
  },
  modalBtnPrimaryText: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#FFFFFF',
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
