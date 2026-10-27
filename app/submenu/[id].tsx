import { Ionicons } from '@expo/vector-icons';
import { router, useLocalSearchParams } from 'expo-router';
import React from 'react';
import { Pressable, ScrollView, StyleSheet, TouchableOpacity, useColorScheme, View, Dimensions } from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';

import { ThemedText } from '@/components/themed-text';
import { MENU_STRUCTURE, MenuItem } from '@/constants/menuConfig';
import { useThemeColors } from '@/hooks/useThemeColors';
import { useThemeContext } from '@/contexts/ThemeContext';

export default function SubmenuScreen() {
  const themeColors = useThemeColors();
  const { accentColor } = useThemeContext();
  const { background: backgroundColor, card: cardColor, text: textColor, border: borderColor, subtitle: subtitleColor, primaryGold: GOLD_COLOR } = themeColors;

  
  
  

  const { id } = useLocalSearchParams();
  const menuId = typeof id === 'string' ? id : '';
  const colorScheme = useColorScheme();
  const isDark = colorScheme === 'dark';

  

  const windowWidth = Dimensions.get('window').width;
  const numColumns = windowWidth >= 1024 ? 4 : windowWidth >= 768 ? 3 : 2;
  const cardWidth = `${100 / numColumns}%`;

  const submenuItems: MenuItem[] = MENU_STRUCTURE[menuId] || [];

  const handleMenuNavigation = (item: MenuItem) => {
    if (item.hasSubmenu) {
      router.push(`/submenu/${item.id}` as any);
    } else if (item.route) {
      router.push(item.route as any);
    }
  };

  const menuTitle = menuId.replace('_submenu', '').replace(/^./, (c) => c.toUpperCase()) + ' Menu';

  return (
    <SafeAreaView style={[styles.safeArea, { backgroundColor }]} edges={['top']}>
      
      <View style={[styles.topBar, { borderBottomColor: borderColor }]}>
        <TouchableOpacity onPress={() => router.back()} style={[styles.backButton, { backgroundColor: cardColor, borderColor }]}>
          <Ionicons name="arrow-back" size={24} color={textColor} />
        </TouchableOpacity>
        <ThemedText style={[styles.headerTitle, { color: textColor }]}>
          {menuTitle}
        </ThemedText>
        <View style={{ width: 40 }} /> {/* Spacer to center title */}
      </View>

      <ScrollView style={styles.container} contentContainerStyle={{ paddingBottom: 40, paddingTop: 20 }}>
        {submenuItems.length === 0 ? (
          <View style={styles.emptyState}>
            <Ionicons name="construct-outline" size={48} color={subtitleColor} />
            <ThemedText style={{ color: subtitleColor, marginTop: 16 }}>No items found for this menu.</ThemedText>
          </View>
        ) : (
          <View style={styles.menuGridContainer}>
            {submenuItems.map((item) => (
              <View key={item.id} style={{ width: cardWidth as any, padding: 10 }}>
                <Pressable
                  onPress={() => handleMenuNavigation(item)}
                  style={({ pressed }) => [
                    styles.menuCard,
                    { backgroundColor: cardColor, borderColor },
                    pressed && { opacity: 0.7, transform: [{ scale: 0.98 }] }
                  ]}
                >
                  <View style={[styles.menuIconContainer, { backgroundColor: `${item.color}15` }]}>
                    <Ionicons name={item.icon as any} size={32} color={item.color} />
                  </View>
                  <ThemedText style={[styles.menuCardTitle, { color: textColor }]}>{item.title}</ThemedText>
                </Pressable>
              </View>
            ))}
          </View>
        )}
      </ScrollView>

    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  safeArea: {
    flex: 1,
  },
  topBar: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingHorizontal: 20,
    paddingVertical: 12,
    borderBottomWidth: 1,
  },
  backButton: {
    width: 40,
    height: 40,
    borderRadius: 12,
    borderWidth: 1,
    justifyContent: 'center',
    alignItems: 'center',
  },
  headerTitle: {
    fontSize: 20,
    fontWeight: '700',
    letterSpacing: 0.5,
  },
  container: {
    flex: 1,
  },
  menuGridContainer: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    paddingHorizontal: 10,
  },
  menuCard: {
    borderRadius: 16,
    borderWidth: 1,
    padding: 24,
    alignItems: 'center',
    justifyContent: 'center',
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.1,
    shadowRadius: 8,
    elevation: 5,
    height: 140,
  },
  menuIconContainer: {
    width: 64,
    height: 64,
    borderRadius: 32,
    alignItems: 'center',
    justifyContent: 'center',
    marginBottom: 12,
  },
  menuCardTitle: {
    fontSize: 16,
    fontWeight: '700',
    textAlign: 'center',
  },
  emptyState: {
    alignItems: 'center',
    justifyContent: 'center',
    padding: 40,
  }
});
