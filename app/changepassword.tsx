import { Ionicons } from '@expo/vector-icons';
import { router, Stack, useLocalSearchParams } from 'expo-router';
import React, { useState } from 'react';
import { ActivityIndicator, Alert, KeyboardAvoidingView, Platform, ScrollView, StyleSheet, TextInput, TouchableOpacity, useColorScheme, View } from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';

import { ThemedText } from '@/components/themed-text';
import { API_BASE_URL } from '@/constants/api';
import { useThemeColors } from '@/hooks/useThemeColors';
import { useThemeContext } from '@/contexts/ThemeContext';

const GOLD_COLOR = '#D4AF37';
const API_URL = `${API_BASE_URL}/changepassword`;

export default function ChangePasswordScreen() {
  const themeColors = useThemeColors();
  const { accentColor } = useThemeContext();
  const { background: backgroundColor, card: cardColor, text: textColor, border: borderColor, primaryGold: GOLD_COLOR, subtitle: subtitleColor } = themeColors;
  const colorScheme = useColorScheme();
  const isDark = colorScheme === 'dark';

  const { username } = useLocalSearchParams();
  const initialUserId = typeof username === 'string' && username !== 'Alexander' ? username : '';
  const [userId, setUserId] = useState(initialUserId);
  const [currentPassword, setCurrentPassword] = useState('');
  const [newPassword, setNewPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [saving, setSaving] = useState(false);

  const inputBg = isDark ? '#222' : '#F5F5F5';

  const handleUpdate = async () => {
    if (!userId) {
      return Alert.alert('Validation Error', 'User ID is missing.');
    }
    if (!currentPassword) {
      return Alert.alert('Validation Error', 'Current Password is required.');
    }
    if (!newPassword) {
      return Alert.alert('Validation Error', 'New Password is required.');
    }
    if (!confirmPassword) {
      return Alert.alert('Validation Error', 'Confirm Password is required.');
    }

    if (newPassword !== confirmPassword) {
      Alert.alert('Validation Error', 'New Password and Confirm Password do not match.');
      return;
    }

    setSaving(true);
    try {
      const response = await fetch(API_URL, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ userId, currentPassword, newPassword }),
      });

      const json = await response.json();
      if (json.success) {
        Alert.alert('Success', 'Password changed successfully', [
          { text: 'OK', onPress: () => router.back() }
        ]);
      } else {
        Alert.alert('Error', json.message || 'Failed to change password');
      }
    } catch (error) {
      console.error('Update error:', error);
      Alert.alert('Error', 'Network error connecting to API');
    } finally {
      setSaving(false);
    }
  };

  return (
    <SafeAreaView style={[styles.safeArea, { backgroundColor }]} edges={['top']}>
      <Stack.Screen options={{ headerShown: false }} />
      {/* Header */}
      <View style={[styles.header, { borderBottomColor: borderColor }]}>
        <View style={{ flexDirection: 'row', alignItems: 'center' }}>
          <TouchableOpacity style={styles.backButton} onPress={() => router.back()}>
            <Ionicons name="arrow-back" size={24} color={textColor} />
          </TouchableOpacity>
          <ThemedText style={[styles.title, { color: textColor }]}>Change Password</ThemedText>
        </View>
      </View>

      <KeyboardAvoidingView 
        style={{ flex: 1 }} 
        behavior={Platform.OS === 'ios' ? 'padding' : undefined}
      >
        <ScrollView contentContainerStyle={styles.container} keyboardShouldPersistTaps="handled">
          <View style={[styles.card, { backgroundColor: cardColor, borderColor, shadowColor: textColor }]}>
            
            <View style={styles.inputGroup}>
              <ThemedText style={[styles.label, { color: textColor }]}>User ID :</ThemedText>
              <TextInput
                style={[styles.input, { backgroundColor: inputBg, color: subtitleColor, borderColor }]}
                value={userId}
                editable={false}
                placeholder="User ID"
                placeholderTextColor={subtitleColor}
              />
            </View>

            <View style={styles.inputGroup}>
              <ThemedText style={[styles.label, { color: textColor }]}>Current Password :</ThemedText>
              <TextInput
                style={[styles.input, { backgroundColor: inputBg, color: textColor, borderColor }]}
                value={currentPassword}
                onChangeText={setCurrentPassword}
                placeholder="Enter Current Password"
                placeholderTextColor={subtitleColor}
                secureTextEntry
                autoCapitalize="none"
              />
            </View>

            <View style={styles.inputGroup}>
              <ThemedText style={[styles.label, { color: textColor }]}>New Password :</ThemedText>
              <TextInput
                style={[styles.input, { backgroundColor: inputBg, color: textColor, borderColor }]}
                value={newPassword}
                onChangeText={setNewPassword}
                placeholder="Enter New Password"
                placeholderTextColor={subtitleColor}
                secureTextEntry
                autoCapitalize="none"
              />
            </View>

            <View style={styles.inputGroup}>
              <ThemedText style={[styles.label, { color: textColor }]}>Confirm Password :</ThemedText>
              <TextInput
                style={[styles.input, { backgroundColor: inputBg, color: textColor, borderColor }]}
                value={confirmPassword}
                onChangeText={setConfirmPassword}
                placeholder="Confirm New Password"
                placeholderTextColor={subtitleColor}
                secureTextEntry
                autoCapitalize="none"
              />
            </View>

            <View style={styles.footer}>
              <TouchableOpacity style={styles.cancelBtn} onPress={() => router.back()}>
                <ThemedText style={{ color: subtitleColor, fontWeight: '600' }}>Cancel</ThemedText>
              </TouchableOpacity>
              <TouchableOpacity style={[styles.saveBtn, { backgroundColor: '#D4AF37' }]} onPress={handleUpdate} disabled={saving}>
                {saving ? (
                  <ActivityIndicator size="small" color="#FFF" />
                ) : (
                  <ThemedText style={{ color: textColor, fontWeight: '600' }}>Update Password</ThemedText>
                )}
              </TouchableOpacity>
            </View>
            
          </View>
        </ScrollView>
      </KeyboardAvoidingView>
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  safeArea: { flex: 1 },
  header: {
    flexDirection: 'row', alignItems: 'center', justifyContent: 'space-between', 
    paddingHorizontal: 16, paddingVertical: 8, borderBottomWidth: 1 
  },
  backButton: { padding: 4, marginRight: 12 },
  title: { fontSize: 20, fontWeight: '300', letterSpacing: 1.5 },
  container: {
    flexGrow: 1,
    padding: 16,
    alignItems: 'center',
    justifyContent: 'center',
  },
  card: {
    width: '100%',
    maxWidth: 600,
    borderRadius: 12,
    borderWidth: 1,
    padding: 24,
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.1,
    shadowRadius: 10,
    elevation: 4,
  },
  inputGroup: {
    flexDirection: 'row',
    alignItems: 'center',
    marginBottom: 20,
  },
  label: {
    flex: 0.35,
    fontSize: 16,
    fontWeight: '600',
  },
  input: {
    flex: 0.65,
    borderWidth: 1,
    borderRadius: 8,
    paddingHorizontal: 16,
    paddingVertical: 12,
    fontSize: 16,
  },
  footer: {
    flexDirection: 'row',
    justifyContent: 'flex-end',
    marginTop: 20,
    gap: 16,
  },
  cancelBtn: {
    paddingHorizontal: 20,
    paddingVertical: 12,
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#CCC',
    justifyContent: 'center',
  },
  saveBtn: {
    paddingHorizontal: 24,
    paddingVertical: 12,
    borderRadius: 8,
    justifyContent: 'center',
    minWidth: 120,
    alignItems: 'center',
  },
});
