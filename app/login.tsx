import { API_BASE_URL } from '@/constants/api';
import { Ionicons } from '@expo/vector-icons';
import { router } from 'expo-router';
import React, { useState } from 'react';
import {
  ActivityIndicator,
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
}

const PremiumInput = ({ icon, placeholder, secureTextEntry, value, onChangeText }: PremiumInputProps) => {
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
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [loading, setLoading] = useState(false);
  const [errorMsg, setErrorMsg] = useState('');

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
        body: JSON.stringify({ username, password })
      });

      const data = await response.json();

      if (data.success) {
        router.replace({ pathname: '/(tabs)/home', params: { username } });
      } else {
        setErrorMsg(data.message || 'Invalid credentials');
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
          />

          <View style={{ height: 20 }} />

          <PremiumInput
            icon="lock-closed-outline"
            placeholder="Password"
            value={password}
            onChangeText={setPassword}
            secureTextEntry
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
});
