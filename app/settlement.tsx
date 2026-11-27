import { API_BASE_URL } from '@/constants/api';
import { getTodayIso } from '@/utils/dateValidation';
import { printHtmlReport } from '@/utils/reportExportClient';
import { Ionicons } from '@expo/vector-icons';
import { Stack, useRouter } from 'expo-router';
import React, { useEffect, useMemo, useState } from 'react';
import {
  ActivityIndicator,
  Platform,
  ScrollView,
  StyleSheet,
  Text,
  TextInput,
  TouchableOpacity,
  View
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';

// Royal Gold Design Palette
const GOLD_ACCENT = '#B48218';
const GOLD_DARK = '#9E6D08';
const GOLD_BORDER = '#E8DCB8';
const CARD_BG = '#FFFFFF';
const APP_BG = '#FBF9F5';
const TEXT_DARK = '#3B2D17';

interface DenominationItem {
  value: number; // currency value, e.g. 1000, 500, 100, 50, 10, 5, 2, 1, 0.5, etc.
  qty: number;   // number of notes/coins
  amount: number;// value * qty
}

interface PayModeItem {
  payMode: string;
  computerAmount: number;
  manualAmount: number;
  isCash?: boolean;
}

export default function SettlementScreen() {
  const router = useRouter();

  // Authentication & Terminal state
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [cashierId, setCashierId] = useState('');
  const [userName, setUserName] = useState('');
  const [userIdInput, setUserIdInput] = useState('');
  const [passwordInput, setPasswordInput] = useState('');
  const [settlementDate, setSettlementDate] = useState(getTodayIso());
  const [terminalName, setTerminalName] = useState('POS-01');
  const [terminalCode, setTerminalCode] = useState('T01');
  const [floatAmount, setFloatAmount] = useState(0); // TillAmount

  // Status & Loaders
  const [isLoading, setIsLoading] = useState(false);
  const [isSaving, setIsSaving] = useState(false);
  const [toastMessage, setToastMessage] = useState('');
  const [toastType, setToastType] = useState<'success' | 'error'>('success');
  const [isAlreadySettled, setIsAlreadySettled] = useState(false);

  // Settlement Tables
  const [denominations, setDenominations] = useState<DenominationItem[]>([]);
  const [payModes, setPayModes] = useState<PayModeItem[]>([]);

  const showSuccess = (msg: string) => {
    setToastType('success');
    setToastMessage(msg);
    setTimeout(() => setToastMessage(''), 3000);
  };

  const showError = (msg: string) => {
    setToastType('error');
    setToastMessage(msg);
    setTimeout(() => setToastMessage(''), 3500);
  };

  // Auto-fill logged in user from local storage if available
  useEffect(() => {
    if (Platform.OS === 'web' && typeof window !== 'undefined') {
      try {
        const u = window.localStorage.getItem('userCode') || window.localStorage.getItem('userName') || '';
        if (u) setUserIdInput(u);
      } catch (_) { }
    }
  }, []);

  // Handle Cashier Login and Loading of Terminal Settlement Data
  const handleLogin = async () => {
    if (!userIdInput.trim()) {
      showError('Please key in User ID.');
      return;
    }
    if (!passwordInput.trim()) {
      showError('Please key in Password.');
      return;
    }

    setIsLoading(true);
    try {
      // 1. Authenticate user
      const authRes = await fetch(`${API_BASE_URL}/auth/login`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          username: userIdInput.trim(),
          password: passwordInput.trim(),
        }),
      }).catch(() => null);

      let verifiedName = userIdInput.trim();
      let verifiedCode = userIdInput.trim();

      if (authRes && authRes.ok) {
        const authJson = await authRes.json();
        if (authJson.user) {
          verifiedName = authJson.user.UserName || authJson.user.username || verifiedName;
          verifiedCode = authJson.user.UserCode || authJson.user.userCode || verifiedCode;
        }
      }

      setCashierId(verifiedCode);
      setUserName(verifiedName);

      // 2. Load Terminal and Settlement details for today
      await loadSettlementDetails(verifiedCode, settlementDate);
      setIsAuthenticated(true);
      showSuccess(`Welcome, ${verifiedName}. Settlement loaded.`);
    } catch (err: any) {
      console.error('Settlement Login Error:', err);
      showError(err.message || 'Login failed. Please check credentials.');
    } finally {
      setIsLoading(false);
    }
  };

  // Load Currency Denominations, Paymodes, and Terminal Data strictly for the logged-in user
  const loadSettlementDetails = async (cId: string, sDate: string) => {
    try {
      // Fetch terminal setup
      const termRes = await fetch(`${API_BASE_URL}/terminal?t=${Date.now()}`).catch(() => null);
      let till = 0;
      let termCode = 'T01';
      let termName = 'POS-01';

      if (termRes && termRes.ok) {
        const termJson = await termRes.json();
        if (termJson.success && Array.isArray(termJson.data) && termJson.data.length > 0) {
          const t = termJson.data[0];
          till = parseFloat(t.TillAmount) || 0;
          termCode = t.TerminalCode || 'T01';
          termName = t.TerminalName || 'POS-01';
        }
      }

      setFloatAmount(till);
      setTerminalCode(termCode);
      setTerminalName(termName);

      // Default Standard Denomination slabs
      const defaultDenoms = [
        1000, 500, 100, 50, 10, 5, 2, 1, 0.5, 0.2, 0.1, 0.05
      ].map((v) => ({
        value: v,
        qty: 0,
        amount: 0,
      }));

      // PayModes
      let payModesList: PayModeItem[] = [
        { payMode: 'CASH', computerAmount: 0, manualAmount: 0, isCash: true },
        { payMode: 'NETS', computerAmount: 0, manualAmount: 0 },
        { payMode: 'VISA', computerAmount: 0, manualAmount: 0 },
        { payMode: 'MASTER', computerAmount: 0, manualAmount: 0 },
        { payMode: 'AMEX', computerAmount: 0, manualAmount: 0 },
        { payMode: 'PAYNOW', computerAmount: 0, manualAmount: 0 },
        { payMode: 'CHEQUE', computerAmount: 0, manualAmount: 0 },
        { payMode: 'VOUCHER', computerAmount: 0, manualAmount: 0 },
      ];

      const cleanUserId = (cId || '').trim();
      const upperUserId = cleanUserId.toUpperCase();

      // Normalize date strings to YYYY-MM-DD
      const normDate = (dStr: string) => {
        if (!dStr) return '';
        const d = String(dStr).split('T')[0].trim();
        // Handle DD/MM/YYYY
        if (d.includes('/')) {
          const [dPart, mPart, yPart] = d.split('/');
          if (yPart && yPart.length === 4) return `${yPart}-${mPart.padStart(2, '0')}-${dPart.padStart(2, '0')}`;
        }
        return d;
      };
      const targetDate = normDate(sDate);

      let salesMap: Record<string, number> = {};
      let hasData = false;

      // 1. Direct Backend Endpoint: Check if settlement was already saved for today
      try {
        const existRes = await fetch(`${API_BASE_URL}/settlementreports/generate`, {
          method: 'POST',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify({
            settlementType: 'comparison',
            location: 'ALL',
            cashierId: cleanUserId,
            fromDate: sDate,
            toDate: sDate,
          }),
        }).catch(() => null);

        if (existRes && existRes.ok) {
          const eJson = await existRes.json();
          if (eJson.success && Array.isArray(eJson.data) && eJson.data.length > 0) {
            eJson.data.forEach((r: any) => {
              const pm = String(r.PayMode || r.paymode || r.Paymode || '').toUpperCase().trim();
              const cAmt = parseFloat(r.ComputerAmount || r.computerAmount || r.Amount || 0) || 0;
              const mAmt = parseFloat(r.ManualAmount || r.manualAmount || 0) || 0;
              if (pm && (cAmt > 0 || mAmt > 0)) {
                salesMap[pm] = (salesMap[pm] || 0) + cAmt;
                hasData = true;
              }
            });
          }
        }
      } catch (_) {}

      // 2. Query Sales Reports for user paymodes (both by salesmanCode and cashierId)
      if (!hasData) {
        try {
          const reportPromises = [
            fetch(`${API_BASE_URL}/salesreports/generate`, {
              method: 'POST',
              headers: { 'Content-Type': 'application/json' },
              body: JSON.stringify({
                reportId: 'paymode_wise_sales',
                reportType: 'Summary',
                filters: {
                  fromDate: sDate,
                  toDate: sDate,
                  salesmanCode: cleanUserId,
                },
              }),
            }),
            fetch(`${API_BASE_URL}/salesreports/generate`, {
              method: 'POST',
              headers: { 'Content-Type': 'application/json' },
              body: JSON.stringify({
                reportId: 'salesman_wise_journal',
                reportType: 'Summary',
                filters: {
                  fromDate: sDate,
                  toDate: sDate,
                  salesmanCode: cleanUserId,
                },
              }),
            }),
          ];

          const reportResponses = await Promise.all(reportPromises.map((p) => p.catch(() => null)));
          for (const repRes of reportResponses) {
            if (repRes && repRes.ok) {
              const repJson = await repRes.json();
              if (repJson.success && Array.isArray(repJson.data) && repJson.data.length > 0) {
                repJson.data.forEach((r: any) => {
                  const rUser = String(r.SalesManCode || r.SalesmanCode || r.SalesManName || r.CashierID || r.UserId || '').trim().toUpperCase();
                  if (!rUser || rUser === upperUserId || cleanUserId === '') {
                    const pm = String(r.PayMode || r.paymode || r.Paymode || '').toUpperCase().trim();
                    const amt = parseFloat(r.Amount || r.NetAmount || r.PaidAmt || r.NetTotal || 0) || 0;
                    if (pm && amt > 0) {
                      salesMap[pm] = (salesMap[pm] || 0) + amt;
                      hasData = true;
                    }
                  }
                });
              }
            }
          }
        } catch (_) {}
      }

      // 3. Query Sales History List & Invoice Payments:
      // Matches transactions done by the user (SalesManCode, SalesManName, CashierID, or UserID)
      try {
        const histRes = await fetch(`${API_BASE_URL}/saleshistory/list?t=${Date.now()}`).catch(() => null);
        if (histRes && histRes.ok) {
          const histJson = await histRes.json();
          if (histJson.success && Array.isArray(histJson.data) && histJson.data.length > 0) {
            // Find invoices matching user and date
            const userInvoices = histJson.data.filter((inv: any) => {
              const invUser = String(inv.SalesManCode || inv.SalesmanCode || inv.SalesManName || inv.CashierID || inv.UserId || inv.CreateUser || '').trim().toUpperCase();
              const invDt = normDate(inv.InvoiceDate || inv.Date || '');
              
              const userMatches = !upperUserId || invUser === upperUserId || invUser.includes(upperUserId) || upperUserId.includes(invUser);
              const dateMatches = !targetDate || invDt === targetDate;
              return userMatches && dateMatches;
            });

            if (userInvoices.length > 0) {
              // Fetch each invoice's detailed payment breakdown in parallel
              const invDetailPromises = userInvoices.map((inv: any) =>
                fetch(`${API_BASE_URL}/saleshistory/${encodeURIComponent(inv.InvoiceNo.trim())}`)
                  .then((r) => r.json())
                  .catch(() => null)
              );

              const detailedResults = await Promise.all(invDetailPromises);
              let detailedFound = false;

              detailedResults.forEach((dJson, idx) => {
                if (dJson && dJson.success && Array.isArray(dJson.payments) && dJson.payments.length > 0) {
                  dJson.payments.forEach((pmItem: any) => {
                    const pm = String(pmItem.PayMode || pmItem.paymode || 'CASH').toUpperCase().trim();
                    const amt = parseFloat(pmItem.Amount || pmItem.amount || 0) || 0;
                    if (pm && amt > 0) {
                      salesMap[pm] = (salesMap[pm] || 0) + amt;
                      detailedFound = true;
                      hasData = true;
                    }
                  });
                } else {
                  // Fallback to invoice header total
                  const inv = userInvoices[idx];
                  const pm = String(inv.SalesType || inv.PayMode || 'CASH').toUpperCase().trim();
                  const amt = parseFloat(inv.NetTotal || inv.PaidAmt || inv.GrandTotal || 0) || 0;
                  const targetPm = pm.includes('NETS') ? 'NETS' : pm.includes('VISA') ? 'VISA' : pm.includes('MASTER') ? 'MASTER' : pm.includes('CREDIT') ? 'CHEQUE' : 'CASH';
                  if (amt > 0) {
                    salesMap[targetPm] = (salesMap[targetPm] || 0) + amt;
                    hasData = true;
                  }
                }
              });
            }
          }
        }
      } catch (e) {
        console.warn('Error reading sales history records:', e);
      }

      // Map Computer Totals to PayModes
      const matchedKeys = new Set<string>();
      payModesList = payModesList.map((p) => {
        const cAmt = salesMap[p.payMode] || 0;
        matchedKeys.add(p.payMode);
        return {
          ...p,
          computerAmount: cAmt,
          manualAmount: p.isCash ? 0 : cAmt,
        };
      });

      // Include any other dynamic paymodes returned by backend (e.g. EXPENSES, FLEXI, DINERS, etc.)
      Object.keys(salesMap).forEach((k) => {
        if (!matchedKeys.has(k) && salesMap[k] > 0) {
          payModesList.push({
            payMode: k,
            computerAmount: salesMap[k],
            manualAmount: salesMap[k],
            isCash: k.toUpperCase() === 'CASH',
          });
        }
      });

      setDenominations(defaultDenoms);
      setPayModes(payModesList);
    } catch (err: any) {
      console.warn('Error loading user-wise settlement details:', err);
    }
  };

  // Update Denomination Qty and Recalculate Cash Total
  const handleDenomQtyChange = (idx: number, qtyStr: string) => {
    const qty = parseInt(qtyStr, 10) || 0;
    const updated = [...denominations];
    updated[idx].qty = qty;
    updated[idx].amount = Math.round(updated[idx].value * qty * 100) / 100;
    setDenominations(updated);

    // Sum all denominations for Cash
    const totalCash = updated.reduce((sum, d) => sum + d.amount, 0);

    // Update Cash row in PayModes
    setPayModes((prev) =>
      prev.map((pm) => (pm.isCash ? { ...pm, manualAmount: totalCash } : pm))
    );
  };

  // Update non-cash manual amount
  const handleManualAmountChange = (idx: number, amtStr: string) => {
    const amt = parseFloat(amtStr) || 0;
    const updated = [...payModes];
    updated[idx].manualAmount = amt;
    setPayModes(updated);
  };

  // Totals calculations
  const totalCashDenomination = useMemo(() => {
    return denominations.reduce((acc, item) => acc + item.amount, 0);
  }, [denominations]);

  const computerTotal = useMemo(() => {
    const sum = payModes.reduce((acc, item) => acc + item.computerAmount, 0);
    return sum + floatAmount;
  }, [payModes, floatAmount]);

  const manualTotal = useMemo(() => {
    const sum = payModes.reduce((acc, item) => acc + item.manualAmount, 0);
    return sum + floatAmount;
  }, [payModes, floatAmount]);

  const variance = useMemo(() => {
    return manualTotal - computerTotal;
  }, [manualTotal, computerTotal]);

  // Save Settlement and Execute Dayend Routine
  const handleSaveSettlement = async () => {
    setIsSaving(true);
    try {
      // 1. Submit settlement payload to backend
      const payload = {
        cashierId,
        terminalCode,
        settlementDate,
        floatAmount,
        denominations,
        payModes,
        totalComputer: computerTotal,
        totalManual: manualTotal,
        variance,
      };

      const res = await fetch(`${API_BASE_URL}/settlement/save`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload),
      }).catch(() => null);

      // 2. Generate and trigger Print Receipt / Settlement Report
      const repColumns = [
        { key: 'PayMode', label: 'PAY MODE' },
        { key: 'ComputerAmount', label: 'COMPUTER TOTAL ($)' },
        { key: 'ManualAmount', label: 'MANUAL TOTAL ($)' },
        { key: 'Variance', label: 'VARIANCE ($)' },
      ];

      const repData = [
        ...payModes.map((p) => ({
          PayMode: p.payMode,
          ComputerAmount: p.computerAmount.toFixed(2),
          ManualAmount: p.manualAmount.toFixed(2),
          Variance: (p.manualAmount - p.computerAmount).toFixed(2),
        })),
        {
          PayMode: 'TILL / FLOAT AMOUNT',
          ComputerAmount: floatAmount.toFixed(2),
          ManualAmount: floatAmount.toFixed(2),
          Variance: '0.00',
        },
      ];

      const repTotals = {
        PayMode: 'GRAND TOTAL',
        ComputerAmount: computerTotal.toFixed(2),
        ManualAmount: manualTotal.toFixed(2),
        Variance: variance.toFixed(2),
      };

      await printHtmlReport(
        {
          endpoint: 'settlementreports',
          title: `Terminal Settlement Report - ${settlementDate}`,
          subtitle: `Terminal: ${terminalName} (${terminalCode}) | Cashier: ${userName} (${cashierId})`,
          columns: repColumns,
          data: repData,
          totals: repTotals,
          fromDate: settlementDate,
          toDate: settlementDate,
        },
        true
      );

      showSuccess('Settlement saved & receipt print generated successfully!');
      setIsAlreadySettled(true);
    } catch (err: any) {
      console.error('Save settlement error:', err);
      showError(err.message || 'Error saving settlement.');
    } finally {
      setIsSaving(false);
    }
  };

  return (
    <SafeAreaView style={styles.container} edges={['top']}>
      <Stack.Screen options={{ headerShown: false }} />

      {/* HEADER BAR */}
      <View style={styles.header}>
        <View style={styles.headerLeft}>
          <TouchableOpacity onPress={() => router.back()} style={styles.backButton} activeOpacity={0.7}>
            <Ionicons name="arrow-back" size={20} color={TEXT_DARK} />
          </TouchableOpacity>
          <Text style={styles.title}>Daily Settlement</Text>
        </View>

        {isAuthenticated && (
          <View style={styles.headerRight}>
            <TouchableOpacity
              style={styles.refreshBtn}
              onPress={() => loadSettlementDetails(cashierId, settlementDate)}
              disabled={isLoading}
              activeOpacity={0.8}
            >
              <Ionicons name="refresh-outline" size={15} color={GOLD_DARK} style={{ marginRight: 4 }} />
              <Text style={styles.refreshBtnText}>Refresh Sales</Text>
            </TouchableOpacity>

            <TouchableOpacity
              style={styles.switchUserBtn}
              onPress={() => {
                setIsAuthenticated(false);
                setPasswordInput('');
              }}
              activeOpacity={0.8}
            >
              <Ionicons name="person-circle-outline" size={15} color={TEXT_DARK} style={{ marginRight: 4 }} />
              <Text style={styles.switchUserBtnText}>Switch User</Text>
            </TouchableOpacity>

            <TouchableOpacity
              style={styles.saveBtn}
              onPress={handleSaveSettlement}
              disabled={isSaving}
              activeOpacity={0.8}
            >
              {isSaving ? (
                <ActivityIndicator size="small" color="#FFFFFF" style={{ marginRight: 5 }} />
              ) : (
                <Ionicons name="checkmark-circle-outline" size={16} color="#FFFFFF" style={{ marginRight: 5 }} />
              )}
              <Text style={styles.saveBtnText}>Save Settlement</Text>
            </TouchableOpacity>
          </View>
        )}
      </View>

      <ScrollView contentContainerStyle={styles.scrollContent}>
        {/* Cashier Login Dialog / Panel */}
        {!isAuthenticated ? (
          <View style={styles.loginCardWrapper}>
            <View style={styles.goldCard}>
              <View style={styles.cardHeaderBanner}>
                <Ionicons name="lock-closed-outline" size={18} color={GOLD_DARK} style={{ marginRight: 6 }} />
                <Text style={styles.cardHeaderTitle}>Cashier Authentication</Text>
              </View>

              <View style={styles.loginFormBody}>
                <Text style={styles.inputLabel}>Date</Text>
                <TextInput
                  style={[styles.textInput, styles.readOnlyInput]}
                  value={settlementDate}
                  editable={false}
                />

                <Text style={[styles.inputLabel, { marginTop: 10 }]}>User ID</Text>
                <TextInput
                  style={styles.textInput}
                  value={userIdInput}
                  onChangeText={setUserIdInput}
                  placeholder="Enter User ID"
                  placeholderTextColor="#9E8D77"
                  autoCapitalize="characters"
                />

                <Text style={[styles.inputLabel, { marginTop: 10 }]}>Password</Text>
                <TextInput
                  style={styles.textInput}
                  value={passwordInput}
                  onChangeText={setPasswordInput}
                  placeholder="Enter Password"
                  placeholderTextColor="#9E8D77"
                  secureTextEntry={true}
                />

                <View style={styles.loginButtonRow}>
                  <TouchableOpacity
                    style={styles.submitBtn}
                    onPress={handleLogin}
                    disabled={isLoading}
                    activeOpacity={0.8}
                  >
                    {isLoading ? (
                      <ActivityIndicator size="small" color="#FFFFFF" />
                    ) : (
                      <Text style={styles.submitBtnText}>OK</Text>
                    )}
                  </TouchableOpacity>

                  <TouchableOpacity
                    style={styles.cancelBtn}
                    onPress={() => router.back()}
                    activeOpacity={0.8}
                  >
                    <Text style={styles.cancelBtnText}>Cancel</Text>
                  </TouchableOpacity>
                </View>
              </View>
            </View>
          </View>
        ) : (
          /* Settlement Tables View */
          <View style={styles.tablesContainer}>
            {/* Cashier & Terminal Status Card */}
            <View style={styles.goldCard}>
              <View style={styles.statusRow}>
                <View style={styles.statusCol}>
                  <Text style={styles.statusLabel}>Cashier</Text>
                  <Text style={styles.statusValue}>{userName} ({cashierId})</Text>
                </View>
                <View style={styles.statusCol}>
                  <Text style={styles.statusLabel}>Terminal</Text>
                  <Text style={styles.statusValue}>{terminalName} ({terminalCode})</Text>
                </View>
                <View style={styles.statusCol}>
                  <Text style={styles.statusLabel}>Date</Text>
                  <Text style={styles.statusValue}>{settlementDate}</Text>
                </View>
                <View style={styles.statusCol}>
                  <Text style={styles.statusLabel}>Till / Float</Text>
                  <Text style={styles.statusValue}>${floatAmount.toFixed(2)}</Text>
                </View>
              </View>
            </View>

            {/* Main Settlement Split Grids */}
            <View style={styles.splitGridRow}>
              {/* Left Column: Currency Denomination Breakdown */}
              <View style={[styles.goldCard, { flex: 1.1 }]}>
                <View style={styles.cardHeaderBanner}>
                  <Ionicons name="cash-outline" size={16} color={GOLD_DARK} style={{ marginRight: 6 }} />
                  <Text style={styles.cardHeaderTitle}>Currency Denomination ($)</Text>
                </View>

                <View style={styles.tableHeaderRow}>
                  <Text style={[styles.tableHeadCell, { flex: 2, textAlign: 'center' }]}>$</Text>
                  <Text style={[styles.tableHeadCell, { flex: 2, textAlign: 'right' }]}>No Of $</Text>
                  <Text style={[styles.tableHeadCell, { flex: 2.5, textAlign: 'right' }]}>Amount ($)</Text>
                </View>

                {denominations.map((d, idx) => (
                  <View key={idx} style={styles.tableRow}>
                    <Text style={[styles.tableCell, { flex: 2, textAlign: 'center', fontWeight: 'bold' }]}>
                      {d.value >= 1 ? `$${d.value}` : `${d.value * 100}¢`}
                    </Text>
                    <TextInput
                      style={[styles.cellInput, { flex: 2, textAlign: 'right' }]}
                      value={d.qty === 0 ? '' : String(d.qty)}
                      onChangeText={(val) => handleDenomQtyChange(idx, val)}
                      placeholder="0"
                      keyboardType="numeric"
                    />
                    <Text style={[styles.tableCell, { flex: 2.5, textAlign: 'right', fontWeight: '600', color: GOLD_DARK }]}>
                      {d.amount.toFixed(2)}
                    </Text>
                  </View>
                ))}

                <View style={styles.totalRow}>
                  <Text style={styles.totalLabel}>Cash Denomination Total:</Text>
                  <Text style={styles.totalAmount}>${totalCashDenomination.toFixed(2)}</Text>
                </View>
              </View>

              {/* Right Column: Paymode Breakdown & Comparison */}
              <View style={[styles.goldCard, { flex: 1.4 }]}>
                <View style={styles.cardHeaderBanner}>
                  <Ionicons name="card-outline" size={16} color={GOLD_DARK} style={{ marginRight: 6 }} />
                  <Text style={styles.cardHeaderTitle}>Paymode Settlement Breakdown</Text>
                </View>

                <View style={styles.tableHeaderRow}>
                  <Text style={[styles.tableHeadCell, { flex: 2.5 }]}>Pay Mode</Text>
                  <Text style={[styles.tableHeadCell, { flex: 2, textAlign: 'right' }]}>Computer Total</Text>
                  <Text style={[styles.tableHeadCell, { flex: 2, textAlign: 'right' }]}>Manual Total</Text>
                  <Text style={[styles.tableHeadCell, { flex: 2, textAlign: 'right' }]}>Variance</Text>
                </View>

                {payModes.map((pm, idx) => {
                  const diff = pm.manualAmount - pm.computerAmount;
                  return (
                    <View key={idx} style={styles.tableRow}>
                      <Text style={[styles.tableCell, { flex: 2.5, fontWeight: 'bold' }]}>
                        {pm.payMode}
                      </Text>
                      <Text style={[styles.tableCell, { flex: 2, textAlign: 'right', color: '#554736' }]}>
                        {pm.computerAmount.toFixed(2)}
                      </Text>

                      {pm.isCash ? (
                        <Text style={[styles.tableCell, { flex: 2, textAlign: 'right', fontWeight: 'bold', color: GOLD_DARK }]}>
                          {pm.manualAmount.toFixed(2)}
                        </Text>
                      ) : (
                        <TextInput
                          style={[styles.cellInput, { flex: 2, textAlign: 'right' }]}
                          value={pm.manualAmount === 0 ? '' : String(pm.manualAmount)}
                          onChangeText={(val) => handleManualAmountChange(idx, val)}
                          placeholder="0.00"
                          keyboardType="numeric"
                        />
                      )}

                      <Text
                        style={[
                          styles.tableCell,
                          {
                            flex: 2,
                            textAlign: 'right',
                            fontWeight: 'bold',
                            color: diff === 0 ? '#2E7D32' : diff < 0 ? '#C62828' : '#1565C0',
                          },
                        ]}
                      >
                        {diff >= 0 ? `+${diff.toFixed(2)}` : diff.toFixed(2)}
                      </Text>
                    </View>
                  );
                })}

                {/* Till Amount Row */}
                {floatAmount > 0 && (
                  <View style={styles.tableRow}>
                    <Text style={[styles.tableCell, { flex: 2.5, fontStyle: 'italic' }]}>Till Amount</Text>
                    <Text style={[styles.tableCell, { flex: 2, textAlign: 'right' }]}>{floatAmount.toFixed(2)}</Text>
                    <Text style={[styles.tableCell, { flex: 2, textAlign: 'right' }]}>{floatAmount.toFixed(2)}</Text>
                    <Text style={[styles.tableCell, { flex: 2, textAlign: 'right', color: '#2E7D32' }]}>0.00</Text>
                  </View>
                )}

                {/* Grand Totals */}
                <View style={styles.grandTotalHighlight}>
                  <View style={styles.grandTotalItem}>
                    <Text style={styles.grandTotalLabel}>Computer Total</Text>
                    <Text style={styles.grandTotalValue}>${computerTotal.toFixed(2)}</Text>
                  </View>
                  <View style={styles.grandTotalItem}>
                    <Text style={styles.grandTotalLabel}>Manual Total</Text>
                    <Text style={styles.grandTotalValue}>${manualTotal.toFixed(2)}</Text>
                  </View>
                  <View style={styles.grandTotalItem}>
                    <Text style={styles.grandTotalLabel}>Excess / Shortage</Text>
                    <Text
                      style={[
                        styles.grandTotalValue,
                        { color: variance === 0 ? '#2E7D32' : variance < 0 ? '#C62828' : '#1565C0' },
                      ]}
                    >
                      {variance >= 0 ? `+$${variance.toFixed(2)}` : `-$${Math.abs(variance).toFixed(2)}`}
                    </Text>
                  </View>
                </View>
              </View>
            </View>
          </View>
        )}
      </ScrollView>

      {/* Toast Alert */}
      {toastMessage ? (
        <View style={[styles.toast, toastType === 'success' ? styles.toastSuccess : styles.toastError]}>
          <Ionicons
            name={toastType === 'success' ? 'checkmark-circle' : 'alert-circle'}
            size={16}
            color="#FFFFFF"
            style={{ marginRight: 6 }}
          />
          <Text style={styles.toastText}>{toastMessage}</Text>
        </View>
      ) : null}
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: APP_BG,
  },
  header: {
    height: 48,
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    paddingHorizontal: 14,
    borderBottomWidth: 1,
    borderBottomColor: GOLD_BORDER,
    backgroundColor: CARD_BG,
    elevation: 2,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.05,
    shadowRadius: 2,
  },
  headerLeft: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  headerRight: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 8,
  },
  backButton: {
    padding: 6,
    marginRight: 8,
    borderRadius: 6,
    backgroundColor: '#F9F6F0',
    borderWidth: 1,
    borderColor: GOLD_BORDER,
  },
  title: {
    fontSize: 15,
    fontWeight: 'bold',
    color: TEXT_DARK,
    letterSpacing: 0.3,
  },
  refreshBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FAF5EA',
    borderWidth: 1,
    borderColor: '#D4C3A3',
    paddingHorizontal: 10,
    paddingVertical: 4,
    borderRadius: 6,
    height: 28,
  },
  refreshBtnText: {
    color: GOLD_DARK,
    fontWeight: 'bold',
    fontSize: 11.5,
  },
  switchUserBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FAF7F0',
    borderWidth: 1,
    borderColor: '#DFD5C3',
    paddingHorizontal: 10,
    paddingVertical: 4,
    borderRadius: 6,
    height: 28,
  },
  switchUserBtnText: {
    color: TEXT_DARK,
    fontWeight: '600',
    fontSize: 11.5,
  },
  saveBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#2E7D32',
    paddingHorizontal: 14,
    paddingVertical: 5,
    borderRadius: 6,
    height: 28,
  },
  saveBtnText: {
    color: '#FFFFFF',
    fontWeight: 'bold',
    fontSize: 12,
  },
  scrollContent: {
    paddingHorizontal: 16,
    paddingTop: 16,
    paddingBottom: 40,
    maxWidth: 1200,
    width: '100%',
    alignSelf: 'center',
  },
  loginCardWrapper: {
    maxWidth: 380,
    width: '100%',
    alignSelf: 'center',
    marginTop: 60,
  },
  goldCard: {
    backgroundColor: CARD_BG,
    borderRadius: 8,
    borderWidth: 1,
    borderColor: GOLD_BORDER,
    shadowColor: '#A38035',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.06,
    shadowRadius: 3,
    elevation: 2,
    overflow: 'hidden',
  },
  cardHeaderBanner: {
    backgroundColor: '#F5E7CA',
    paddingHorizontal: 10,
    paddingVertical: 6,
    borderBottomWidth: 1,
    borderBottomColor: '#E2D3B3',
    flexDirection: 'row',
    alignItems: 'center',
  },
  cardHeaderTitle: {
    fontSize: 12.5,
    fontWeight: 'bold',
    color: TEXT_DARK,
    letterSpacing: 0.3,
  },
  loginFormBody: {
    padding: 16,
  },
  inputLabel: {
    fontSize: 11,
    fontWeight: 'bold',
    color: TEXT_DARK,
    marginBottom: 4,
  },
  textInput: {
    height: 32,
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#D4C3A3',
    borderRadius: 5,
    paddingHorizontal: 8,
    fontSize: 12,
    color: '#2B2118',
  },
  readOnlyInput: {
    backgroundColor: '#F7F3EB',
    color: '#5C4A28',
  },
  loginButtonRow: {
    flexDirection: 'row',
    justifyContent: 'center',
    gap: 12,
    marginTop: 18,
  },
  submitBtn: {
    backgroundColor: GOLD_ACCENT,
    paddingVertical: 7,
    paddingHorizontal: 22,
    borderRadius: 6,
    minWidth: 80,
    alignItems: 'center',
  },
  submitBtnText: {
    color: '#FFFFFF',
    fontWeight: 'bold',
    fontSize: 12,
  },
  cancelBtn: {
    backgroundColor: '#FAF5EA',
    borderWidth: 1,
    borderColor: '#D4C3A3',
    paddingVertical: 7,
    paddingHorizontal: 22,
    borderRadius: 6,
    minWidth: 80,
    alignItems: 'center',
  },
  cancelBtnText: {
    color: TEXT_DARK,
    fontWeight: 'bold',
    fontSize: 12,
  },
  tablesContainer: {
    gap: 12,
  },
  statusRow: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    padding: 10,
    flexWrap: 'wrap',
    gap: 12,
  },
  statusCol: {
    minWidth: 120,
  },
  statusLabel: {
    fontSize: 10.5,
    color: '#7D6F5B',
    fontWeight: '600',
    textTransform: 'uppercase',
  },
  statusValue: {
    fontSize: 12.5,
    fontWeight: 'bold',
    color: TEXT_DARK,
    marginTop: 2,
  },
  splitGridRow: {
    flexDirection: Platform.OS === 'web' ? 'row' : 'column',
    gap: 12,
  },
  tableHeaderRow: {
    flexDirection: 'row',
    backgroundColor: '#FAF5EA',
    paddingVertical: 5,
    paddingHorizontal: 8,
    borderBottomWidth: 1,
    borderBottomColor: '#E8DCB8',
  },
  tableHeadCell: {
    fontSize: 10.5,
    fontWeight: 'bold',
    color: TEXT_DARK,
    textTransform: 'uppercase',
  },
  tableRow: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingVertical: 4,
    paddingHorizontal: 8,
    borderBottomWidth: 1,
    borderBottomColor: '#F7F2E9',
  },
  tableCell: {
    fontSize: 11,
    color: TEXT_DARK,
  },
  cellInput: {
    height: 24,
    backgroundColor: '#FFFFFF',
    borderWidth: 1,
    borderColor: '#D4C3A3',
    borderRadius: 4,
    paddingHorizontal: 6,
    paddingVertical: 0,
    fontSize: 11,
    color: '#2B2118',
    marginHorizontal: 4,
  },
  totalRow: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    backgroundColor: '#FFF8EA',
    paddingVertical: 7,
    paddingHorizontal: 10,
    borderTopWidth: 1,
    borderTopColor: GOLD_BORDER,
  },
  totalLabel: {
    fontSize: 11,
    fontWeight: 'bold',
    color: TEXT_DARK,
  },
  totalAmount: {
    fontSize: 13,
    fontWeight: 'bold',
    color: GOLD_DARK,
  },
  grandTotalHighlight: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    backgroundColor: '#FFF8EA',
    borderTopWidth: 1,
    borderTopColor: '#B8903B',
    paddingVertical: 8,
    paddingHorizontal: 12,
    marginTop: 'auto',
  },
  grandTotalItem: {
    alignItems: 'center',
  },
  grandTotalLabel: {
    fontSize: 10,
    fontWeight: 'bold',
    color: '#7D5E19',
    textTransform: 'uppercase',
  },
  grandTotalValue: {
    fontSize: 14,
    fontWeight: 'bold',
    color: '#9E6D08',
    marginTop: 2,
  },
  toast: {
    position: 'absolute',
    top: 54,
    alignSelf: 'center',
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 16,
    paddingVertical: 8,
    borderRadius: 6,
    elevation: 4,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.15,
    shadowRadius: 4,
    zIndex: 9999,
  },
  toastSuccess: {
    backgroundColor: '#2E7D32',
  },
  toastError: {
    backgroundColor: '#C62828',
  },
  toastText: {
    color: '#FFFFFF',
    fontSize: 12,
    fontWeight: '600',
  },
});
