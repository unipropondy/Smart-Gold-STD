import { API_BASE_URL } from '@/constants/api';
import { useUserPermissions } from '@/contexts/UserPermissionContext';
import { Ionicons } from '@expo/vector-icons';
import AsyncStorage from '@react-native-async-storage/async-storage';
import { router } from 'expo-router';
import React, { useState } from 'react';
import {
  ActivityIndicator,
  Alert,
  Image,
  Keyboard,
  KeyboardAvoidingView,
  Platform,
  StatusBar,
  StyleSheet,
  Text,
  TextInput,
  TouchableOpacity,
  View
} from 'react-native';

// Inject Web CSS to hide native Edge/Chrome password reveal icon
if (Platform.OS === 'web' && typeof document !== 'undefined') {
  const style = document.createElement('style');
  style.textContent = `
    input::-ms-reveal,
    input::-ms-clear {
      display: none !important;
    }
  `;
  document.head.append(style);
}

interface PremiumInputProps {
  icon: React.ComponentProps<typeof Ionicons>['name'];
  placeholder: string;
  secureTextEntry?: boolean;
  value: string;
  onChangeText: (text: string) => void;
  autoComplete?: any;
  textContentType?: any;
}

const PremiumInput = ({ icon, placeholder, secureTextEntry, value, onChangeText, autoComplete, textContentType }: PremiumInputProps) => {
  const [isFocused, setIsFocused] = useState(false);
  const [showPassword, setShowPassword] = useState(false);

  const accentGold = '#E6AF2E';
  const iconColor = '#AAA';

  return (
    <View style={[
      styles.inputWrapper,
      { borderColor: isFocused ? accentGold : '#EAEAEA' }
    ]}>
      <Ionicons name={icon} size={18} color={isFocused ? accentGold : iconColor} style={styles.inputIcon} />
      <TextInput
        style={styles.input}
        value={value}
        onChangeText={onChangeText}
        secureTextEntry={secureTextEntry && !showPassword}
        onFocus={() => setIsFocused(true)}
        onBlur={() => setIsFocused(false)}
        placeholder={placeholder}
        placeholderTextColor="#999"
        autoCapitalize="none"
        autoCorrect={false}
        autoComplete={autoComplete}
        textContentType={textContentType}
        selectionColor={accentGold}
      />
      {secureTextEntry && (
        <TouchableOpacity onPress={() => setShowPassword(!showPassword)} style={styles.eyeIcon} activeOpacity={0.7}>
          <Ionicons name={showPassword ? "eye-outline" : "eye-off-outline"} size={18} color="#999" />
        </TouchableOpacity>
      )}
    </View>
  );
};

export default function LoginScreen() {
  const { setUserSession } = useUserPermissions();
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [loading, setLoading] = useState(false);
  const [errorMsg, setErrorMsg] = useState('');

  // Local PC Name state & loading
  const [retrievedPcName, setRetrievedPcName] = useState('');
  const [pcStatusMsg, setPcStatusMsg] = useState('');
  const [pcLoading, setPcLoading] = useState(false);

  // Function to call http://127.0.0.1:5005/system-info
  const handleGetPcName = async () => {
    setPcLoading(true);
    setPcStatusMsg('');
    try {
      const response = await fetch('http://127.0.0.1:5005/system-info', {
        method: 'GET',
        headers: { 'Content-Type': 'application/json' }
      });

      if (!response.ok) {
        throw new Error('Local service responded with error status');
      }

      const data = await response.json();

      if (data.success && data.pcName) {
        const pcName = String(data.pcName).trim();
        setRetrievedPcName(pcName);
        setPcStatusMsg('');
        console.log('%c====================================', 'color: #2E7D32; font-weight: bold;');
        console.log('%c💻 RETRIEVED WINDOWS PC NAME:', 'color: #2E7D32; font-size: 15px; font-weight: bold;', pcName);
        console.log('%c====================================', 'color: #2E7D32; font-weight: bold;');
      } else {
        setRetrievedPcName('');
        setPcStatusMsg('Unable to retrieve Windows PC name. Please check whether the local service is running.');
      }
    } catch (err: any) {
      console.error('Failed to connect to local service (127.0.0.1:5005):', err);
      setRetrievedPcName('');
      setPcStatusMsg('Unable to retrieve Windows PC name. Please check whether the local service is running.');
    } finally {
      setPcLoading(false);
    }
  };

  const handleLogin = async () => {
    Keyboard.dismiss();
    setErrorMsg('');

    if (!username || !password) {
      setErrorMsg('Please enter both username and password.');
      return;
    }

    setLoading(true);
    try {
      const response = await fetch(`${API_BASE_URL}/login`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          username,
          password,
        })
      });

      const data = await response.json();

      if (data.success) {
        const uCode = data.user?.userCode || username;
        const uName = data.user?.userName || username;
        const uGroup = data.user?.userGroupCode || '';
        const companyData = data.company || {};

        try {
          await AsyncStorage.setItem('@logged_user', JSON.stringify(data.user || { userCode: uCode, userName: uName, userGroupCode: uGroup }));
          await AsyncStorage.setItem('@user_id', uCode);
          await AsyncStorage.setItem('@user_name', uName);
          await AsyncStorage.setItem('@user_group', uGroup);
          if (companyData && Object.keys(companyData).length > 0) {
            await AsyncStorage.setItem('@company_config', JSON.stringify(companyData));
            if (companyData.companyId) await AsyncStorage.setItem('@company_id', String(companyData.companyId));
            if (companyData.companyName) await AsyncStorage.setItem('@company_name', companyData.companyName);
            if (companyData.gstPercentage !== undefined) await AsyncStorage.setItem('@company_gst_percentage', String(companyData.gstPercentage));
            if (companyData.locationCode) await AsyncStorage.setItem('@user_location', companyData.locationCode);
            if (companyData.defaultCurrency) await AsyncStorage.setItem('@default_currency', companyData.defaultCurrency);
          }
        } catch (_) { }

        if (Platform.OS === 'web' && typeof window !== 'undefined') {
          try {
            window.localStorage.setItem('userCode', uCode);
            window.localStorage.setItem('userId', uCode);
            window.localStorage.setItem('userName', uName);
            window.localStorage.setItem('userGroup', uGroup);
            window.localStorage.setItem('user', JSON.stringify(data.user || { userCode: uCode, userName: uName, userGroupCode: uGroup }));
            if (companyData && Object.keys(companyData).length > 0) {
              window.localStorage.setItem('companyConfig', JSON.stringify(companyData));
              if (companyData.gstPercentage !== undefined) window.localStorage.setItem('gstPercentage', String(companyData.gstPercentage));
              if (companyData.locationCode) window.localStorage.setItem('locationCode', companyData.locationCode);
            }

            window.sessionStorage.setItem('userCode', uCode);
            window.sessionStorage.setItem('userId', uCode);
            window.sessionStorage.setItem('userName', uName);
            window.sessionStorage.setItem('userGroup', uGroup);
            window.sessionStorage.setItem('user', JSON.stringify(data.user || { userCode: uCode, userName: uName, userGroupCode: uGroup }));
            if (companyData && Object.keys(companyData).length > 0) {
              window.sessionStorage.setItem('companyConfig', JSON.stringify(companyData));
            }
          } catch (_) { }
        }

        // Handle prompt to set password if new user flag is set
        if (data.changePasswordPrompt) {
          Alert.alert('Notice', 'Please set your password in settings.');
        }

        // Immediately update permissions context with newly authenticated user data before navigating
        await setUserSession(data.user || { userCode: uCode, userName: uName, userGroupCode: uGroup });

        router.replace({ pathname: '/(tabs)/home', params: { username: uCode, userId: uCode } });
      } else {
        setErrorMsg(data.message || 'Invalid Username/Password');
      }
    } catch (err) {
      console.error(err);
      setErrorMsg('Failed to connect to the server.');
    } finally {
      setLoading(false);
    }
  };

  const accentGold = '#E6AF2E';

  return (
    <View style={styles.container}>
      <StatusBar barStyle="dark-content" />

      {/* Luxury Watermark Background */}
      <Image
        source={require('../assets/images/Smart Gold ERP Luxury Branding.png')}
        style={styles.bgImage}
        resizeMode="cover"
      />

      <KeyboardAvoidingView behavior={Platform.OS === 'ios' ? 'padding' : undefined} style={styles.content}>

        {/* Branding Header */}
        <View style={styles.header}>
          <View style={styles.logoCircle}>
            <Ionicons name="diamond-outline" size={24} color={accentGold} />
          </View>
          <Text style={styles.title}>SMART <Text style={styles.titleBold}>GOLD</Text></Text>
          <Text style={styles.subtitle}>E R P   S O L U T I O N</Text>
        </View>

        {/* Glassmorphic Login Card */}
        <View style={styles.glassCard}>
          <Text style={styles.welcomeText}>Sign In</Text>
          <Text style={styles.welcomeSubtext}>Access your premium dashboard</Text>

          <PremiumInput
            icon="person-outline"
            placeholder="Username"
            value={username}
            onChangeText={setUsername}
            autoComplete="username"
            textContentType="username"
          />

          <View style={{ height: 20 }} />

          <PremiumInput
            icon="lock-closed-outline"
            placeholder="Password"
            value={password}
            onChangeText={setPassword}
            secureTextEntry
            autoComplete="current-password"
            textContentType="password"
          />

          <View style={styles.formOptions}>
            <View style={styles.rememberMe}>
              <View style={styles.checkbox}><Ionicons name="checkmark" size={12} color="#111" /></View>
              <Text style={styles.rememberText}>Remember me</Text>
            </View>
            <TouchableOpacity><Text style={styles.forgotText}>Forgot Password?</Text></TouchableOpacity>
          </View>

          {errorMsg ? <Text style={styles.errorText}>{errorMsg}</Text> : null}

          <TouchableOpacity style={[styles.loginBtn, Platform.OS === 'web' && { backgroundImage: 'linear-gradient(90deg, #E6AF2E 0%, #B67417 100%)' } as any]} onPress={handleLogin} disabled={loading} activeOpacity={0.8}>
            {loading ? <ActivityIndicator color="#FFF" /> : (
              <>
                <Text style={styles.loginBtnText}>SIGN IN</Text>
                <Ionicons name="arrow-forward" size={16} color="#FFF" style={{ marginLeft: 6 }} />
              </>
            )}
          </TouchableOpacity>

          {/* Designated Section: Retrieve Actual Windows PC Name using local service */}
          <View style={styles.pcSection}>
            <TouchableOpacity
              style={styles.pcBtn}
              onPress={handleGetPcName}
              disabled={pcLoading}
              activeOpacity={0.7}
            >
              {pcLoading ? (
                <ActivityIndicator size="small" color="#9E6D08" />
              ) : (
                <>
                  <Ionicons name="desktop-outline" size={15} color="#9E6D08" style={{ marginRight: 6 }} />
                  <Text style={styles.pcBtnText}>Get PC Name</Text>
                </>
              )}
            </TouchableOpacity>

            {/* Display Retrieved PC Name */}
            {retrievedPcName ? (
              <View style={styles.pcSuccessCard}>
                <Ionicons name="checkmark-circle" size={15} color="#2E7D32" style={{ marginRight: 6 }} />
                <Text style={styles.pcSuccessText}>
                  Windows PC Name: <Text style={{ fontWeight: 'bold' }}>{retrievedPcName}</Text>
                </Text>
              </View>
            ) : null}

            {/* Display Error if local service is not running */}
            {pcStatusMsg ? (
              <View style={styles.pcErrorCard}>
                <Ionicons name="alert-circle" size={15} color="#C62828" style={{ marginRight: 6, marginTop: 1 }} />
                <Text style={styles.pcErrorText}>{pcStatusMsg}</Text>
              </View>
            ) : null}
          </View>
        </View>

      </KeyboardAvoidingView>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    minHeight: Platform.OS === 'web' ? ('100vh' as any) : '100%',
    width: '100%',
    backgroundColor: '#faf9f6',
    justifyContent: 'center',
    alignItems: 'center',
    overflow: 'hidden',
    position: 'relative',
  },
  bgImage: {
    ...StyleSheet.absoluteFillObject,
    width: '100%',
    height: '100%',
  },
  content: {
    width: '100%',
    maxWidth: 360,
    alignItems: 'center',
    padding: 16,
    zIndex: 10,
    marginVertical: 'auto',
  },
  header: {
    alignItems: 'center',
    marginBottom: 32,
  },
  logoCircle: {
    width: 50,
    height: 50,
    borderRadius: 25,
    backgroundColor: '#FFF',
    borderWidth: 1,
    borderColor: '#E6AF2E',
    justifyContent: 'center',
    alignItems: 'center',
    marginBottom: 12,
    shadowColor: '#E6AF2E',
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.15,
    shadowRadius: 12,
    elevation: 5,
  },
  title: {
    fontSize: 22,
    color: '#111',
    letterSpacing: 4,
    fontWeight: '300',
  },
  titleBold: {
    color: '#E6AF2E',
    fontWeight: '700',
  },
  subtitle: {
    color: '#111111ff',
    fontSize: 9,
    marginTop: 6,
    letterSpacing: 3,
  },
  glassCard: {
    width: '100%',
    backgroundColor: '#ffffffba',
    borderRadius: 20,
    padding: 32,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 12 },
    shadowOpacity: 0.05,
    shadowRadius: 24,
    elevation: 8,
  } as any,
  welcomeText: {
    fontSize: 24,
    fontWeight: '700',
    color: '#111',
    marginBottom: 6,
    textAlign: 'center',
  },
  welcomeSubtext: {
    fontSize: 12,
    color: '#888',
    marginBottom: 24,
    textAlign: 'center',
  },
  inputWrapper: {
    flexDirection: 'row',
    alignItems: 'center',
    height: 44,
    borderWidth: 1,
    borderRadius: 8,
    paddingHorizontal: 16,
    backgroundColor: '#FFF',
  },
  inputIcon: {
    marginRight: 12,
  },
  input: {
    flex: 1,
    fontSize: 13,
    color: '#111',
    height: '100%',
    outlineStyle: 'none',
  } as any,
  eyeIcon: {
    padding: 8,
  },
  formOptions: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    marginTop: 12,
    marginBottom: 24,
  },
  rememberMe: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  checkbox: {
    width: 16,
    height: 16,
    borderRadius: 4,
    backgroundColor: '#E6AF2E',
    justifyContent: 'center',
    alignItems: 'center',
    marginRight: 8,
  },
  rememberText: {
    fontSize: 11,
    color: '#555',
  },
  forgotText: {
    fontSize: 11,
    color: '#E6AF2E',
    fontWeight: '600',
  },
  loginBtn: {
    backgroundColor: '#E6AF2E',
    height: 44,
    borderRadius: 8,
    flexDirection: 'row',
    justifyContent: 'center',
    alignItems: 'center',
  },
  loginBtnText: {
    color: '#FFF',
    fontSize: 13,
    fontWeight: 'bold',
    letterSpacing: 2,
  },
  errorText: {
    color: '#F44336',
    textAlign: 'center',
    marginBottom: 12,
    fontSize: 11,
  },
  pcSection: {
    marginTop: 18,
    width: '100%',
    alignItems: 'center',
    borderTopWidth: 1,
    borderTopColor: '#F0E5D0',
    paddingTop: 14,
  },
  pcBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    backgroundColor: '#FAF5EA',
    borderColor: '#E8DCB8',
    borderWidth: 1,
    borderRadius: 8,
    paddingVertical: 8,
    paddingHorizontal: 16,
    width: '100%',
  },
  pcBtnText: {
    color: '#9E6D08',
    fontSize: 12,
    fontWeight: '700',
    letterSpacing: 0.5,
  },
  pcSuccessCard: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#E8F5E9',
    borderColor: '#A5D6A7',
    borderWidth: 1,
    borderRadius: 6,
    paddingVertical: 7,
    paddingHorizontal: 10,
    marginTop: 10,
    width: '100%',
  },
  pcSuccessText: {
    color: '#1B5E20',
    fontSize: 12,
    flex: 1,
  },
  pcErrorCard: {
    flexDirection: 'row',
    alignItems: 'flex-start',
    backgroundColor: '#FFEBEE',
    borderColor: '#FFCDD2',
    borderWidth: 1,
    borderRadius: 6,
    paddingVertical: 7,
    paddingHorizontal: 10,
    marginTop: 10,
    width: '100%',
  },
  pcErrorText: {
    color: '#C62828',
    fontSize: 11,
    lineHeight: 15,
    flex: 1,
  },
});
