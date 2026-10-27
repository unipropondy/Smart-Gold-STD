import { Ionicons } from '@expo/vector-icons';
import { router, useLocalSearchParams } from 'expo-router';
import React from 'react';
import {
  Animated,
  Dimensions,
  Easing,
  Image,
  LayoutAnimation,
  Modal,
  Platform,
  Pressable,
  ScrollView,
  StyleSheet,
  TextInput,
  TouchableOpacity,
  UIManager,
  View
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';

if (Platform.OS === 'android') {
  if (UIManager.setLayoutAnimationEnabledExperimental) {
    UIManager.setLayoutAnimationEnabledExperimental(true);
  }
}

import { ThemedText } from '@/components/themed-text';
import { API_BASE_URL } from '@/constants/api';
import { MENU_STRUCTURE } from '@/constants/menuConfig';
import { useThemeContext } from '@/contexts/ThemeContext';
import { useThemeColors } from '@/hooks/useThemeColors';

export default function HomeScreen() {
  const themeColors = useThemeColors();
  const theme = themeColors;
  const { accentColor, setAccentColor, setTheme } = useThemeContext();
  const { background: backgroundColor, card: cardColor, text: textColor, border: borderColor, subtitle: subtitleColor, primaryGold: GOLD_COLOR, primaryGold, card, text, border, subtitle, background } = themeColors;




  const { username } = useLocalSearchParams();
  const displayUsername = typeof username === 'string' && username ? username : 'Admin';

  const [activeMenu, setActiveMenu] = React.useState('master');
  const [viewMode, setViewMode] = React.useState<'grid' | 'list'>('grid');
  const [searchQuery, setSearchQuery] = React.useState('');


  const windowWidth = Dimensions.get('window').width;
  const isDesktop = windowWidth >= 1024;
  const [isSidebarOpen, setIsSidebarOpen] = React.useState(isDesktop);

  React.useEffect(() => {
    setIsSidebarOpen(isDesktop);
  }, [isDesktop]);

  const toggleSidebar = () => {
    LayoutAnimation.configureNext(LayoutAnimation.Presets.easeInEaseOut);
    setIsSidebarOpen(!isSidebarOpen);
  };

  const [expandedCards, setExpandedCards] = React.useState<Record<string, boolean>>({});
  const [isLogoutModalVisible, setIsLogoutModalVisible] = React.useState(false);
  const [isSettingsOpen, setIsSettingsOpen] = React.useState(false);

  // Initial load animation
  const fadeAnim = React.useRef(new Animated.Value(0)).current;
  const translateYAnim = React.useRef(new Animated.Value(20)).current;

  React.useEffect(() => {
    Animated.parallel([
      Animated.timing(fadeAnim, {
        toValue: 1,
        duration: 500,
        useNativeDriver: true,
      }),
      Animated.timing(translateYAnim, {
        toValue: 0,
        duration: 500,
        useNativeDriver: true,
      })
    ]).start();
  }, []);

  // Notification Pulse Animation
  const pulseAnim = React.useRef(new Animated.Value(1)).current;
  React.useEffect(() => {
    Animated.loop(
      Animated.sequence([
        Animated.timing(pulseAnim, { toValue: 1.2, duration: 800, useNativeDriver: true }),
        Animated.timing(pulseAnim, { toValue: 1, duration: 800, useNativeDriver: true })
      ])
    ).start();
  }, []);

  const getSubmenuItems = (menuId: string) => {
    return MENU_STRUCTURE[menuId] || [];
  };

  const activeGridItems = MENU_STRUCTURE[activeMenu] || [];
  const filteredGridItems = activeGridItems.filter(item =>
    item.title.toLowerCase().includes(searchQuery.toLowerCase())
  );
  const activeMenuTitle = MENU_STRUCTURE.home?.find(m => m.id === activeMenu)?.title || 'Menu';

  // Custom theme colors for cards based on the image
  const cardThemes: Record<string, { bg: string, text: string, border: string }> = {
    company_submenu: { bg: '#F4F0FF', text: '#7C3AED', border: '#EDE9FE' }, // Purple
    inventory_submenu: { bg: '#ECFDF5', text: '#059669', border: '#D1FAE5' }, // Green
    procurement_submenu: { bg: '#EFF6FF', text: '#2563EB', border: '#DBEAFE' }, // Blue
    sales_submenu: { bg: '#FFF7ED', text: '#EA580C', border: '#FFEDD5' }, // Orange
    ipm_submenu: { bg: '#F4F0FF', text: '#7C3AED', border: '#EDE9FE' }, // Purple
    currency: { bg: '#F0FDFA', text: '#0D9488', border: '#CCFBF1' }, // Teal
    payoutmaster: { bg: '#EFF6FF', text: '#2563EB', border: '#DBEAFE' }, // Blue
    reorderlevel: { bg: '#FDF2F8', text: '#DB2777', border: '#FCE7F3' }, // Pink
    schememaster: { bg: '#FEF2F2', text: '#DC2626', border: '#FEE2E2' }, // Red
  };

  return (
    <SafeAreaView style={[styles.safeArea, { backgroundColor: backgroundColor }]} edges={['top']}>
      <View style={styles.container}>

        {/* Sidebar */}
        {isSidebarOpen && (
          <View style={[styles.sidebar, { backgroundColor: cardColor, borderRightColor: borderColor }]}>
            {/* Logo */}
            <View style={styles.logoContainer}>
              <ThemedText style={[styles.logoText, { color: textColor }]}>
                SMART <ThemedText style={{ color: accentColor, fontWeight: '800' }}>GOLD</ThemedText>
              </ThemedText>
            </View>

            {/* Menu List */}
            <ScrollView style={styles.sidebarMenu} showsVerticalScrollIndicator={false}>
              {MENU_STRUCTURE.home?.map((item) => {
                const isActive = activeMenu === item.id;
                return (
                  <Pressable
                    key={item.id}
                    onPress={() => {
                      if (item.hasSubmenu) {
                        LayoutAnimation.configureNext(LayoutAnimation.Presets.easeInEaseOut);
                        setActiveMenu(item.id);
                      } else if (item.route) {
                        router.push(item.route as any);
                      }
                    }}
                    style={[
                      styles.sidebarMenuItem,
                      isActive && { backgroundColor: accentColor }
                    ]}
                  >
                    <Ionicons
                      name={item.icon as any}
                      size={18}
                      color={isActive ? '#FFFFFF' : item.color}
                      style={styles.sidebarMenuIcon}
                    />
                    <ThemedText style={[
                      styles.sidebarMenuText,
                      { color: isActive ? '#FFFFFF' : text, fontWeight: isActive ? '600' : '500' }
                    ]}>
                      {item.title}
                    </ThemedText>
                    {item.hasSubmenu && (
                      <Ionicons
                        name="chevron-forward"
                        size={16}
                        color={isActive ? '#FFFFFF' : subtitleColor}
                        style={styles.sidebarMenuArrow}
                      />
                    )}
                  </Pressable>
                )
              })}
            </ScrollView>

            {/* Settings Section (Theme & Accent) */}
            <View style={styles.sidebarThemeSelector}>
              <TouchableOpacity
                style={[styles.sidebarMenuItem, { paddingHorizontal: 0, marginBottom: isSettingsOpen ? 12 : 0 }]}
                onPress={() => {
                  LayoutAnimation.configureNext(LayoutAnimation.Presets.easeInEaseOut);
                  setIsSettingsOpen(!isSettingsOpen);
                }}
              >
                <Ionicons name="color-palette-outline" size={18} color={subtitleColor} style={[styles.sidebarMenuIcon, { marginLeft: 16 }]} />
                <ThemedText style={[styles.sidebarMenuText, { color: textColor, flex: 1 }]}>Appearance</ThemedText>
                <Ionicons name={isSettingsOpen ? "chevron-up" : "chevron-down"} size={16} color={subtitleColor} style={{ marginRight: 16 }} />
              </TouchableOpacity>

              {isSettingsOpen && (
                <View style={{ paddingHorizontal: 16 }}>
                  {/* Theme Selector */}
                  <View style={{ marginBottom: 12 }}>
                    <ThemedText style={[styles.sidebarSectionTitle, { color: subtitleColor }]}>THEME</ThemedText>
                    <View style={[styles.themeOptionsGroup, { backgroundColor: backgroundColor, borderColor: borderColor }]}>
                      {/* White */}
                      <TouchableOpacity
                        style={[styles.themeOptionBtn, theme === 'light' && { backgroundColor: cardColor }]}
                        onPress={() => setTheme('light')}
                      >
                        <View style={[styles.themeColorDot, { backgroundColor: cardColor, borderColor: '#E5E7EB', borderWidth: 1 }]} />
                        <ThemedText style={[styles.themeOptionText, { color: textColor, fontWeight: theme === 'light' ? '600' : '400' }]}>White</ThemedText>
                      </TouchableOpacity>

                      {/* Gold */}
                      <TouchableOpacity
                        style={[styles.themeOptionBtn, theme === 'gold' && { backgroundColor: cardColor }]}
                        onPress={() => setTheme('gold')}
                      >
                        <View style={[styles.themeColorDot, { backgroundColor: '#D4AF37' }]} />
                        <ThemedText style={[styles.themeOptionText, { color: textColor, fontWeight: theme === 'gold' ? '600' : '400' }]}>Gold</ThemedText>
                      </TouchableOpacity>
                    </View>
                  </View>

                  {/* Accent Color Selector */}
                  <View>
                    <ThemedText style={[styles.sidebarSectionTitle, { color: subtitleColor }]}>ACCENT COLOR</ThemedText>
                    <View style={[styles.themeOptionsGroup, { backgroundColor: backgroundColor, borderColor: borderColor }]}>
                      {['#e7a60fff', '#3B82F6', '#10B981', '#F43F5E', '#8B5CF6'].map(color => (
                        <TouchableOpacity
                          key={color}
                          style={[styles.themeOptionBtn, accentColor === color && { backgroundColor: cardColor }]}
                          onPress={() => setAccentColor(color)}
                        >
                          <View style={[styles.themeColorDot, { backgroundColor: color, marginRight: 0 }]} />
                        </TouchableOpacity>
                      ))}
                    </View>
                  </View>
                </View>
              )}
            </View>

            {/* Logout Button */}
            <TouchableOpacity
              style={[styles.sidebarMenuItem, { marginTop: 4, marginBottom: 4 }]}
              onPress={() => setIsLogoutModalVisible(true)}
            >
              <Ionicons name="log-out-outline" size={18} color="#EF4444" style={styles.sidebarMenuIcon} />
              <ThemedText style={[styles.sidebarMenuText, { color: '#EF4444', fontWeight: '500' }]}>
                Log Out
              </ThemedText>
            </TouchableOpacity>

            {/* Footer Version */}
            <View style={[styles.sidebarFooter, { borderTopColor: borderColor }]}>
              <Image
                source={require('@/assets/images/unipro-logo.png')}
                style={{ width: 36, height: 36, resizeMode: 'contain', marginRight: 12 }}
              />
              <View style={styles.versionTextContainer}>
                <ThemedText style={[styles.versionLabel, { color: subtitleColor }]}>Powered By</ThemedText>
                <ThemedText style={[styles.versionValue, { color: textColor, fontSize: 12 }]} numberOfLines={2}>Unipro Softwares SG Pte Ltd</ThemedText>
              </View>
            </View>
          </View>
        )}

        {/* Main Content */}
        <View style={styles.mainContent}>
          {/* Header (Combined with Subheader) */}
          <View style={[styles.header, { backgroundColor: cardColor, borderBottomColor: border, paddingVertical: 12 }]}>
            <View style={[styles.headerLeft, { flex: 1 }]}>
              <TouchableOpacity onPress={toggleSidebar} style={[styles.iconButton, { marginRight: 16 }]}>
                <Ionicons name="menu-outline" size={24} color={textColor} />
              </TouchableOpacity>

              <View style={[styles.accentLine, { backgroundColor: accentColor }]} />
              <ThemedText style={[styles.subHeaderTitle, { color: textColor, marginLeft: 8 }]}>{activeMenuTitle} Menu</ThemedText>
            </View>

            <View style={[styles.headerRight, { flex: 3, justifyContent: 'flex-end' }]}>

              {/* View Toggle */}
              <View style={[styles.viewToggleGroup, { backgroundColor: backgroundColor, borderColor: borderColor, marginRight: 16 }]}>
                <TouchableOpacity
                  style={[styles.viewToggleButton, viewMode === 'grid' && { backgroundColor: `${accentColor}20` }]}
                  onPress={() => {
                    LayoutAnimation.configureNext(LayoutAnimation.Presets.easeInEaseOut);
                    setViewMode('grid');
                  }}
                >
                  <Ionicons name="grid" size={16} color={viewMode === 'grid' ? accentColor : subtitleColor} />
                  <ThemedText style={[styles.viewToggleText, { color: viewMode === 'grid' ? accentColor : subtitle, fontWeight: viewMode === 'grid' ? '600' : '400' }]}>Grid View</ThemedText>
                </TouchableOpacity>
                <TouchableOpacity
                  style={[styles.viewToggleButton, viewMode === 'list' && { backgroundColor: `${accentColor}20` }]}
                  onPress={() => {
                    LayoutAnimation.configureNext(LayoutAnimation.Presets.easeInEaseOut);
                    setViewMode('list');
                  }}
                >
                  <Ionicons name="list" size={16} color={viewMode === 'list' ? accentColor : subtitleColor} />
                  <ThemedText style={[styles.viewToggleText, { color: viewMode === 'list' ? accentColor : subtitle, fontWeight: viewMode === 'list' ? '600' : '400' }]}>List View</ThemedText>
                </TouchableOpacity>
              </View>

              {/* Search */}
              <View style={[styles.subSearchBar, { borderColor: borderColor, marginRight: 16, width: 250 }]}>
                <Ionicons name="search-outline" size={16} color={subtitleColor} style={styles.searchIcon} />
                <TextInput
                  style={[styles.searchInput, { color: textColor, paddingVertical: 8 }]}
                  placeholder={`Search in ${activeMenuTitle.toLowerCase()}...`}
                  placeholderTextColor={subtitleColor}
                  value={searchQuery}
                  onChangeText={(text) => {
                    LayoutAnimation.configureNext(LayoutAnimation.Presets.easeInEaseOut);
                    setSearchQuery(text);
                  }}
                />
              </View>

              {/* Notifications */}
              <TouchableOpacity style={[styles.iconButton, { marginRight: 16 }]}>
                <Ionicons name="notifications-outline" size={22} color={textColor} />
                <Animated.View style={[styles.notificationBadge, { transform: [{ scale: pulseAnim }] }]}>
                  <ThemedText style={styles.notificationText}>3</ThemedText>
                </Animated.View>
              </TouchableOpacity>

              {/* User Profile */}
              <View style={styles.userProfile}>
                <View style={[styles.avatar, { backgroundColor: accentColor }]}>
                  <ThemedText style={styles.avatarText}>{displayUsername.charAt(0).toUpperCase()}</ThemedText>
                </View>
                <View style={styles.userInfo}>
                  <ThemedText style={[styles.userName, { color: textColor }]}>{displayUsername}</ThemedText>
                  <ThemedText style={[styles.userRole, { color: subtitleColor }]}>Administrator</ThemedText>
                </View>
              </View>

            </View>
          </View>

          {/* Dashboard Body */}
          <Animated.ScrollView
            style={[styles.dashboardBody, { backgroundColor: backgroundColor, opacity: fadeAnim, transform: [{ translateY: translateYAnim }] }]}
            contentContainerStyle={styles.dashboardBodyContent}
            showsVerticalScrollIndicator={false}
          >


            {/* Live Gold Rates Marquee */}
            <GoldRateMarquee accentColor={accentColor} card={cardColor} border={borderColor} text={textColor} backgroundColor={backgroundColor} windowWidth={windowWidth} />

            {/* Grid Area */}
            <View style={styles.cardsGrid}>
              {filteredGridItems.map((item, index) => {
                return (
                  <AnimatedGridCard
                    key={item.id}
                    item={item}
                    index={index}
                    viewMode={viewMode}
                    cardThemes={cardThemes}
                    expandedCards={expandedCards}
                    setExpandedCards={setExpandedCards}
                    router={router}
                    styles={styles}
                    card={cardColor}
                    border={borderColor}
                    text={textColor}
                    subtitle={subtitleColor}
                    getSubmenuItems={getSubmenuItems}
                  />
                );
              })}
            </View>

          </Animated.ScrollView>
        </View>
      </View>

      {/* Logout Modal */}
      <Modal
        visible={isLogoutModalVisible}
        transparent={true}
        animationType="fade"
        onRequestClose={() => setIsLogoutModalVisible(false)}
      >
        <View style={styles.modalOverlay}>
          <View style={[styles.modalContent, { backgroundColor: cardColor, borderColor: borderColor }]}>
            <View style={styles.modalHeader}>
              <Ionicons name="warning" size={24} color={accentColor} style={{ marginRight: 12 }} />
              <ThemedText style={[styles.modalTitle, { color: textColor }]}>Confirm Logout</ThemedText>
            </View>
            <ThemedText style={[styles.modalMessage, { color: subtitleColor }]}>
              Are you sure you want to log out?
            </ThemedText>
            <View style={styles.modalActions}>
              <TouchableOpacity
                style={[styles.modalBtn, styles.modalBtnCancel, { borderColor: borderColor }]}
                onPress={() => setIsLogoutModalVisible(false)}
              >
                <ThemedText style={[styles.modalBtnText, { color: textColor }]}>Cancel</ThemedText>
              </TouchableOpacity>
              <TouchableOpacity
                style={[styles.modalBtn, styles.modalBtnConfirm]}
                onPress={() => {
                  setIsLogoutModalVisible(false);
                  router.replace('/login');
                }}
              >
                <ThemedText style={styles.modalBtnTextConfirm}>Log Out</ThemedText>
              </TouchableOpacity>
            </View>
          </View>
        </View>
      </Modal>
    </SafeAreaView>
  );
}

const GoldRateMarquee = ({ accentColor, card: cardColor, border: borderColor, text: textColor, backgroundColor, windowWidth }: any) => {
  const [rates, setRates] = React.useState<any[]>([]);
  const marqueeAnim = React.useRef(new Animated.Value(0)).current;
  const [singleWidth, setSingleWidth] = React.useState(0);
  const [containerWidth, setContainerWidth] = React.useState(0);

  React.useEffect(() => {
    const fetchRates = async () => {
      try {
        const res = await fetch(`${API_BASE_URL}/dailyclassrate/today`);
        const json = await res.json();
        if (json.success && json.data) {
          setRates(json.data);
        }
      } catch (err) {
        console.error('Failed to fetch gold rates:', err);
      }
    };
    fetchRates();
    const interval = setInterval(fetchRates, 60000); // refresh every minute
    return () => clearInterval(interval);
  }, []);

  React.useEffect(() => {
    if (rates.length === 0 || singleWidth === 0 || containerWidth === 0) return;

    marqueeAnim.stopAnimation();
    marqueeAnim.setValue(0);

    const overflow = singleWidth - containerWidth;

    // If the textColor perfectly fits inside the screen, we don't need to animate it.
    if (overflow <= 0) return;

    // Constant speed (e.g. 25ms per pixel)
    const duration = (overflow + 32) * 25;

    Animated.loop(
      Animated.sequence([
        Animated.delay(1500),
        Animated.timing(marqueeAnim, {
          toValue: -overflow - 32, // Add some padding so the last item is fully visible
          duration: duration,
          useNativeDriver: true,
          easing: Easing.inOut(Easing.ease)
        }),
        Animated.delay(1500),
        Animated.timing(marqueeAnim, {
          toValue: 0,
          duration: duration,
          useNativeDriver: true,
          easing: Easing.inOut(Easing.ease)
        })
      ])
    ).start();
  }, [rates, singleWidth, containerWidth]);

  if (rates.length === 0) return null;

  // Render a single item block
  const renderItem = (rate: any, key: string, isLast: boolean) => (
    <View key={key} style={styles.marqueeItem}>
      <Ionicons name="sparkles" size={14} color={accentColor} style={{ marginRight: 6 }} />
      <ThemedText style={[styles.marqueeText, { color: textColor }]}>
        {rate.ClassName ? rate.ClassName.trim() : ''}
      </ThemedText>
      <View style={[styles.pricePill, { backgroundColor: `${accentColor}15` }]}>
        <ThemedText style={[styles.marqueeValue, { color: accentColor }]}>
          ${Number(rate.Rate).toFixed(2)}
        </ThemedText>
      </View>
      {!isLast && <View style={[styles.marqueeSeparator, { backgroundColor: borderColor }]} />}
    </View>
  );

  return (
    <View style={[styles.tickerWrapper, { backgroundColor: cardColor, borderColor: borderColor }]}>
      {/* Fixed LIVE Badge (Absolute positioning ensures it never gets pushed out) */}
      <View style={[styles.tickerBadge, { backgroundColor: backgroundColor }]}>
        <View style={[styles.pulsingDot, { backgroundColor: '#10B981' }]} />
        <ThemedText style={[styles.tickerBadgeText, { color: accentColor }]}>Live Rates</ThemedText>
      </View>

      {/* Scrolling Area */}
      <View
        style={styles.marqueeContainer}
        onLayout={(e) => setContainerWidth(e.nativeEvent.layout.width)}
      >
        <Animated.View
          style={[
            styles.marqueeContent,
            singleWidth > 0 && singleWidth <= containerWidth ? { width: '100%', justifyContent: 'space-around' } : {},
            { transform: [{ translateX: marqueeAnim }] }
          ]}
          onLayout={(e) => {
            if (singleWidth === 0) setSingleWidth(e.nativeEvent.layout.width);
          }}
        >
          {rates.map((rate, index) => renderItem(rate, `visible-${index}`, index === rates.length - 1))}
        </Animated.View>
      </View>
    </View>
  );
};

const AnimatedGridCard = ({ item, index, viewMode, cardThemes, expandedCards, setExpandedCards, router, styles, card: cardColor, border: borderColor, text: textColor, subtitle: subtitleColor, getSubmenuItems }: any) => {
  const fadeAnim = React.useRef(new Animated.Value(0)).current;
  const translateYAnim = React.useRef(new Animated.Value(20)).current;
  const scaleAnim = React.useRef(new Animated.Value(1)).current;
  const titleTranslateX = React.useRef(new Animated.Value(0)).current;

  React.useEffect(() => {
    fadeAnim.setValue(0);
    translateYAnim.setValue(20);
    Animated.parallel([
      Animated.timing(fadeAnim, {
        toValue: 1,
        duration: 400,
        delay: index * 40,
        useNativeDriver: true,
      }),
      Animated.timing(translateYAnim, {
        toValue: 0,
        duration: 400,
        delay: index * 40,
        useNativeDriver: true,
      })
    ]).start();
  }, [item.id]);

  const subItems = getSubmenuItems(item.id);
  const count = subItems.length;
  const cardStyle = { bg: `${item.color}15`, text: item.color, border: item.color };
  const isExpanded = expandedCards[item.id] ?? false;

  return (
    <Animated.View style={[styles.card, { backgroundColor: cardColor, borderColor: borderColor, opacity: fadeAnim, transform: [{ translateY: translateYAnim }, { scale: scaleAnim }] }, viewMode === 'list' && { width: '100%' }]}>
      <Pressable
        style={styles.cardHeader}
        onHoverIn={() => {
          if (Platform.OS === 'web') {
            Animated.parallel([
              Animated.spring(scaleAnim, { toValue: 1.02, useNativeDriver: true }),
              Animated.spring(titleTranslateX, { toValue: 4, useNativeDriver: true })
            ]).start();
          }
        }}
        onHoverOut={() => {
          if (Platform.OS === 'web') {
            Animated.parallel([
              Animated.spring(scaleAnim, { toValue: 1, useNativeDriver: true }),
              Animated.spring(titleTranslateX, { toValue: 0, useNativeDriver: true })
            ]).start();
          }
        }}
        onPressIn={() => Animated.spring(scaleAnim, { toValue: 0.97, useNativeDriver: true }).start()}
        onPressOut={() => {
          Animated.spring(scaleAnim, { toValue: 1, useNativeDriver: true }).start();
        }}
        onPress={() => {
          if (count > 0 || item.hasSubmenu) {
            LayoutAnimation.configureNext(LayoutAnimation.Presets.easeInEaseOut);
            setExpandedCards((prev: any) => ({ ...prev, [item.id]: !(prev[item.id] ?? false) }));
          } else if (item.route) {
            router.push(item.route as any);
          }
        }}
      >
        <View style={styles.cardHeaderLeft}>
          <View style={[styles.cardIconBox, { backgroundColor: cardStyle.bg }]}>
            <Ionicons name={item.icon as any} size={20} color={cardStyle.text} />
          </View>
          <Animated.View style={{ transform: [{ translateX: titleTranslateX }] }}>
            <ThemedText style={[styles.cardTitle, { color: textColor }]}>{item.title}</ThemedText>
          </Animated.View>
        </View>
        <View style={[styles.cardCountBadge, { backgroundColor: cardStyle.bg }]}>
          <ThemedText style={[styles.cardCountText, { color: cardStyle.text }]}>{count}</ThemedText>
        </View>
      </Pressable>

      {isExpanded && (
        <View>
          <View style={styles.cardBody}>
            {subItems.slice(0, 6).map((subItem: any) => (
              <Pressable
                key={subItem.id}
                style={({ pressed }) => [styles.subItemRow, pressed && { opacity: 0.6 }]}
                onPress={() => {
                  if (subItem.route) router.push(subItem.route as any);
                }}
              >
                <View style={[styles.subItemDot, { backgroundColor: cardStyle.text }]} />
                <ThemedText style={[styles.subItemText, { color: textColor }]} numberOfLines={1}>{subItem.title}</ThemedText>
                <Ionicons name="chevron-forward" size={14} color={subtitleColor} style={{ marginLeft: 'auto' }} />
              </Pressable>
            ))}
            {subItems.length === 0 && <View style={{ height: 60 }} />}
          </View>
        </View>
      )}
    </Animated.View>
  );
};

const styles = StyleSheet.create({
  safeArea: {
    flex: 1,
    ...(Platform.OS === 'web' ? { zoom: '0.8' } : {}),
  } as any,
  container: {
    flex: 1,
    flexDirection: 'row',
  },
  sidebar: {
    width: 250,
    borderRightWidth: 1,
    display: 'flex',
    flexDirection: 'column',
  },
  logoContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    paddingHorizontal: 16,
    paddingTop: 16,
    paddingBottom: 8,
  },
  logoIcon: {
    width: 36,
    height: 36,
    borderRadius: 12,
    backgroundColor: '#E6AF2E',
    justifyContent: 'center',
    alignItems: 'center',
    marginRight: 12,
  },
  logoIconText: {

    fontSize: 18,
    fontWeight: 'bold',
  },
  logoText: {
    fontSize: 24,
    fontWeight: 'bold',
    letterSpacing: 1,
  },
  sidebarMenu: {
    flex: 1,
    paddingRight: 16,
    marginTop: 16,
  },
  sidebarMenuItem: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingVertical: 8,
    paddingLeft: 24,
    paddingRight: 12,
    borderTopRightRadius: 8,
    borderBottomRightRadius: 8,
    marginBottom: 2,
  },
  sidebarMenuIcon: {
    marginRight: 12,
  },
  sidebarMenuText: {
    flex: 1,
    fontSize: 13,
  },
  sidebarMenuArrow: {
    opacity: 0.7,
  },
  sidebarThemeSelector: {
    paddingVertical: 16,
    borderTopWidth: 1,
    borderTopColor: '#E5E7EB',
  },
  sidebarSectionTitle: {
    fontSize: 10,
    fontWeight: '700',
    letterSpacing: 1,
    marginBottom: 8,
  },
  themeOptionsGroup: {
    flexDirection: 'row',
    borderRadius: 6,
    borderWidth: 1,
    padding: 2,
    justifyContent: 'space-between',
  },
  themeOptionBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingVertical: 6,
    paddingHorizontal: 4,
    borderRadius: 6,
    flex: 1,
    justifyContent: 'center',
  },
  themeColorDot: {
    width: 12,
    height: 12,
    borderRadius: 6,
    marginRight: 4,
  },
  themeOptionText: {
    fontSize: 11,
  },
  sidebarFooter: {
    flexDirection: 'row',
    alignItems: 'center',
    padding: 16,
    borderTopWidth: 1,
  },
  versionBadge: {
    width: 32,
    height: 32,
    borderRadius: 8,
    justifyContent: 'center',
    alignItems: 'center',
    marginRight: 12,
  },
  versionTextContainer: {
    flex: 1,
  },
  versionLabel: {
    fontSize: 11,
    marginBottom: 2,
  },
  versionValue: {
    fontSize: 13,
    fontWeight: '600',
  },
  mainContent: {
    flex: 1,
    flexDirection: 'column',
  },
  header: {
    height: 70,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingHorizontal: 24,
    borderBottomWidth: 1,
  },
  headerLeft: {
    flexDirection: 'row',
    alignItems: 'center',
    flex: 1,
  },
  iconButton: {
    padding: 8,
    justifyContent: 'center',
    alignItems: 'center',
  },
  searchBar: {
    flexDirection: 'row',
    alignItems: 'center',
    marginLeft: 16,
    borderRadius: 8,
    borderWidth: 1,
    paddingHorizontal: 12,
    height: 40,
    width: 400,
    maxWidth: '100%',
  },
  searchIcon: {
    marginRight: 8,
  },
  searchInput: {
    flex: 1,
    fontSize: 14,
    ...(Platform.OS === 'web' ? { outlineStyle: 'none' } : {}),
  } as any,
  shortcutBadge: {
    borderWidth: 1,
    borderRadius: 4,
    paddingHorizontal: 6,
    paddingVertical: 2,
    marginLeft: 8,
  },
  shortcutText: {
    fontSize: 10,
    fontWeight: '600',
  },
  headerRight: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  themeToggle: {
    marginRight: 8,
  },
  notificationBadge: {
    position: 'absolute',
    top: 4,
    right: 6,
    backgroundColor: '#EF4444',
    borderRadius: 10,
    minWidth: 16,
    height: 16,
    justifyContent: 'center',
    alignItems: 'center',
  },
  notificationText: {
    color: '#FFFFFF',
    fontSize: 9,
    fontWeight: 'bold',
  },
  userProfile: {
    flexDirection: 'row',
    alignItems: 'center',
    marginLeft: 16,
    paddingLeft: 16,
    borderLeftWidth: 1,
    borderLeftColor: '#E5E7EB',
  },
  avatar: {
    width: 36,
    height: 36,
    borderRadius: 18,
    justifyContent: 'center',
    alignItems: 'center',
    marginRight: 12,
  },
  avatarText: {
    color: '#FFFFFF',
    fontSize: 16,
    fontWeight: 'bold',
  },
  userInfo: {
    justifyContent: 'center',
  },
  userName: {
    fontSize: 14,
    fontWeight: '700',
  },
  userRole: {
    fontSize: 11,
    marginTop: 2,
  },
  dashboardBody: {
    flex: 1,
  },
  dashboardBodyContent: {
    padding: 24,
    paddingBottom: 48,
  },
  tickerWrapper: {
    position: 'relative',
    height: 52,
    borderRadius: 16,
    borderWidth: 1,
    marginBottom: 24,
    overflow: 'hidden',
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.05,
    shadowRadius: 10,
    elevation: 3,
  },
  tickerBadge: {
    position: 'absolute',
    left: 0,
    top: 0,
    bottom: 0,
    width: 130,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    zIndex: 10,
    borderTopRightRadius: 16,
    borderBottomRightRadius: 16,
    shadowOffset: { width: 2, height: 0 },
    shadowOpacity: 0.1,
    shadowRadius: 4,
    elevation: 5,
  },
  pulsingDot: {
    width: 12,
    height: 12,
    borderRadius: 6,

    marginRight: 10,
  },
  tickerBadgeText: {

    fontWeight: '800',
    fontSize: 16,
    letterSpacing: 0.5,
  },
  marqueeContainer: {
    flex: 1,
    marginLeft: 130, // Make room for the absolute badge
    overflow: 'hidden',
    height: '100%',
    justifyContent: 'center',
  },
  marqueeContent: {
    flexDirection: 'row',
    alignItems: 'center',
    position: 'absolute',
    height: '100%',
  },
  marqueeItem: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 12,
  },
  marqueeText: {
    fontSize: 14,
    fontWeight: '600',
    letterSpacing: 0.5,
  },
  pricePill: {
    paddingHorizontal: 10,
    paddingVertical: 4,
    borderRadius: 8,
    marginLeft: 8,
  },
  marqueeValue: {
    fontSize: 14,
    fontWeight: '800',
  },
  marqueeSeparator: {
    width: 4,
    height: 4,
    borderRadius: 2,
    marginLeft: 12,
  },

  subHeader: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    padding: 16,
    borderRadius: 12,
    borderWidth: 1,
    marginBottom: 24,
  },
  subHeaderLeft: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  accentLine: {
    width: 4,
    height: 24,
    borderRadius: 2,
    marginRight: 12,
  },
  subHeaderTitle: {
    fontSize: 16,
    fontWeight: '700',
  },
  subHeaderRight: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  viewToggleGroup: {
    flexDirection: 'row',
    alignItems: 'center',
    borderRadius: 8,
    borderWidth: 1,
    padding: 4,
    marginRight: 16,
  },
  viewToggleButton: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 12,
    paddingVertical: 6,
    borderRadius: 6,
  },
  viewToggleText: {
    fontSize: 13,
    marginLeft: 6,
  },
  subSearchBar: {
    flexDirection: 'row',
    alignItems: 'center',
    borderWidth: 1,
    borderRadius: 8,
    paddingHorizontal: 12,
    width: 250,
  },
  cardsGrid: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    gap: 20,
  },
  card: {
    width: '31%',
    minWidth: 280,
    borderRadius: 12,
    borderWidth: 1,
    overflow: 'hidden',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.05,
    shadowRadius: 8,
    elevation: 2,
    flexGrow: 1,
  },
  cardHeader: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    padding: 16,
  },
  cardHeaderLeft: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  cardIconBox: {
    width: 36,
    height: 36,
    borderRadius: 8,
    justifyContent: 'center',
    alignItems: 'center',
    marginRight: 16,
  },
  cardTitle: {
    fontSize: 14,
    fontWeight: '700',
  },
  cardCountBadge: {
    paddingHorizontal: 8,
    paddingVertical: 4,
    borderRadius: 12,
  },
  cardCountText: {
    fontSize: 12,
    fontWeight: 'bold',
  },
  cardBody: {
    paddingHorizontal: 20,
    paddingBottom: 20,
  },
  subItemRow: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingVertical: 8,
  },
  subItemDot: {
    width: 6,
    height: 6,
    borderRadius: 3,
    marginRight: 12,
  },
  subItemText: {
    fontSize: 13,
    fontWeight: '500',
    flex: 1,
  },
  modalOverlay: {
    flex: 1,
    backgroundColor: 'rgba(0, 0, 0, 0.5)',
    justifyContent: 'center',
    alignItems: 'center',
  },
  modalContent: {
    width: '90%',
    maxWidth: 400,
    borderRadius: 12,
    padding: 24,
    borderWidth: 1,
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.1,
    shadowRadius: 12,
    elevation: 8,
  },
  modalHeader: {
    flexDirection: 'row',
    alignItems: 'center',
    marginBottom: 16,
  },
  modalTitle: {
    fontSize: 20,
    fontWeight: '700',
  },
  modalMessage: {
    fontSize: 15,
    marginBottom: 24,
    lineHeight: 22,
  },
  modalActions: {
    flexDirection: 'row',
    justifyContent: 'flex-end',
    gap: 12,
  },
  modalBtn: {
    paddingVertical: 10,
    paddingHorizontal: 20,
    borderRadius: 8,
    justifyContent: 'center',
    alignItems: 'center',
  },
  modalBtnCancel: {
    borderWidth: 1,
  },
  modalBtnConfirm: {
    backgroundColor: '#EF4444',
  },
  modalBtnText: {
    fontSize: 14,
    fontWeight: '600',
  },
  modalBtnTextConfirm: {
    color: '#FFFFFF',
    fontSize: 14,
    fontWeight: '600',
  }
});
