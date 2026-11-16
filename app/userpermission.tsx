import { Ionicons } from '@expo/vector-icons';
import { Stack, useRouter } from 'expo-router';
import React, { useEffect, useState } from 'react';
import {
  ActivityIndicator,
  Alert,
  FlatList,
  Modal,
  StyleSheet,
  TextInput,
  TouchableOpacity,
  TouchableWithoutFeedback,
  useColorScheme,
  View,
  Platform,
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';
import { ThemedText } from '@/components/themed-text';
import { useThemeColors } from '@/hooks/useThemeColors';
import { useThemeContext } from '@/contexts/ThemeContext';
import { useUserPermissions } from '@/contexts/UserPermissionContext';
import { API_BASE_URL } from '@/constants/api';
import { MENU_PAGE_NODES, MenuNode } from '@/constants/menuNodes';

const API_URL = `${API_BASE_URL}/userpermission`;

interface NodePermissionState {
  nodeId: string;
  nodeName: string; // The exact Menu Page node title used as FormCode
  category: string;
  allowRead: boolean;
}

export default function UserPermissionScreen() {
  const themeColors = useThemeColors();
  const { accentColor } = useThemeContext();
  const { background: backgroundColor, card: cardColor, text: textColor, border: borderColor, subtitle: subtitleColor } = themeColors;
  const { refreshPermissions } = useUserPermissions();

  const colorScheme = useColorScheme();
  const isDark = colorScheme === 'dark';
  const router = useRouter();

  const [userGroups, setUserGroups] = useState<any[]>([]);
  const [selectedGroupCode, setSelectedGroupCode] = useState<string>('');
  const [loading, setLoading] = useState(true);
  const [savingBatch, setSavingBatch] = useState(false);
  const [searchQuery, setSearchQuery] = useState('');
  const [selectedCategory, setSelectedCategory] = useState<string>('All');

  // Matrix of permissions for all Menu Page nodes
  const [nodePermissions, setNodePermissions] = useState<NodePermissionState[]>([]);
  const [isGroupLookupVisible, setIsGroupLookupVisible] = useState(false);

  // 1. Fetch available User Groups
  const fetchGroups = async () => {
    try {
      const res = await fetch(`${API_URL}/groups?t=${Date.now()}`);
      const json = await res.json();
      if (json.success && Array.isArray(json.data) && json.data.length > 0) {
        setUserGroups(json.data);
        if (!selectedGroupCode) {
          setSelectedGroupCode(json.data[0].UserGroupCode);
        }
      }
    } catch (e) {
      console.error('Error fetching user groups:', e);
    }
  };

  // 2. Fetch permissions for the selected User Group
  const fetchPermissionsForGroup = async (groupCode: string) => {
    if (!groupCode) return;
    setLoading(true);
    try {
      const res = await fetch(`${API_URL}?groupCode=${encodeURIComponent(groupCode)}&t=${Date.now()}`);
      const json = await res.json();
      const savedPerms: any[] = json.success ? json.data : [];

      // Map FormCode -> AllowRead ('Y' / 'N')
      const permMap = new Map<string, boolean>();
      savedPerms.forEach(p => {
        if (p.FormCode) {
          const isAllowed = p.AllowRead === 'Y' || p.AllowRead === '1' || p.AllowRead === true;
          permMap.set(String(p.FormCode).trim().toLowerCase(), isAllowed);
        }
      });

      // Build unified state matching every Menu Page node
      const list: NodePermissionState[] = MENU_PAGE_NODES.map(node => {
        const key = node.nodeName.trim().toLowerCase();
        const allowed = permMap.has(key) ? permMap.get(key)! : false;
        return {
          nodeId: node.nodeId,
          nodeName: node.nodeName,
          category: node.category,
          allowRead: allowed,
        };
      });

      setNodePermissions(list);
    } catch (e) {
      console.error('Error fetching permissions for group:', e);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchGroups();
  }, []);

  useEffect(() => {
    if (selectedGroupCode) {
      fetchPermissionsForGroup(selectedGroupCode);
    }
  }, [selectedGroupCode]);

  // Toggle single node permission
  const toggleNodePermission = (nodeName: string) => {
    setNodePermissions(prev =>
      prev.map(item => {
        if (item.nodeName.toLowerCase() === nodeName.toLowerCase()) {
          return { ...item, allowRead: !item.allowRead };
        }
        return item;
      })
    );
  };

  // Bulk select/deselect all currently visible or filtered nodes
  const handleSelectAll = (enable: boolean) => {
    const targetNames = new Set(filteredNodes.map(n => n.nodeName.toLowerCase()));
    setNodePermissions(prev =>
      prev.map(item => {
        if (targetNames.has(item.nodeName.toLowerCase())) {
          return { ...item, allowRead: enable };
        }
        return item;
      })
    );
  };

  // Save permissions to tblUserpermission using nodeName as FormCode
  const handleSaveAll = async () => {
    if (!selectedGroupCode) {
      Alert.alert('Validation Error', 'Please select a User Group first');
      return;
    }

    setSavingBatch(true);
    try {
      const payloadPermissions = nodePermissions.map(item => ({
        FormCode: item.nodeName, // Exact Menu Page node name as FormCode
        AllowRead: item.allowRead ? 'Y' : 'N',
      }));

      const res = await fetch(`${API_URL}/batch`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          UserGroupCode: selectedGroupCode,
          permissions: payloadPermissions,
        }),
      });

      const json = await res.json();
      if (json.success) {
        Alert.alert('Success', `Menu node permissions for user group '${selectedGroupCode}' saved successfully!`);
        refreshPermissions();
        fetchPermissionsForGroup(selectedGroupCode);
      } else {
        Alert.alert('Error', json.message || 'Failed to save permissions');
      }
    } catch (e: any) {
      Alert.alert('Error', e.message || 'Error saving permissions');
    } finally {
      setSavingBatch(false);
    }
  };

  const categories = ['All', ...Array.from(new Set(MENU_PAGE_NODES.map(n => n.category)))];

  const filteredNodes = nodePermissions.filter(node => {
    const matchesSearch = node.nodeName.toLowerCase().includes(searchQuery.toLowerCase());
    const matchesCategory = selectedCategory === 'All' || node.category === selectedCategory;
    return matchesSearch && matchesCategory;
  });

  return (
    <SafeAreaView style={[styles.safeArea, { backgroundColor }]} edges={['top']}>
      <Stack.Screen options={{ headerShown: false }} />

      {/* Header Bar */}
      <View style={[styles.header, { borderBottomColor: borderColor, backgroundColor: cardColor }]}>
        <View style={{ flexDirection: 'row', alignItems: 'center' }}>
          <TouchableOpacity style={styles.backButton} onPress={() => router.back()}>
            <Ionicons name="arrow-back" size={22} color={textColor} />
          </TouchableOpacity>
          <ThemedText style={[styles.title, { color: textColor }]}>User Permission – Menu Access</ThemedText>
        </View>

        <TouchableOpacity
          style={[styles.saveAllBtn, { backgroundColor: accentColor }]}
          onPress={handleSaveAll}
          disabled={savingBatch}
          activeOpacity={0.8}
        >
          {savingBatch ? (
            <ActivityIndicator size="small" color="#FFF" />
          ) : (
            <>
              <Ionicons name="save-outline" size={16} color="#FFF" />
              <ThemedText style={styles.saveAllBtnText}>Save Node Permissions</ThemedText>
            </>
          )}
        </TouchableOpacity>
      </View>

      {/* Control Bar: User Group Picker & Search */}
      <View style={[styles.controlBar, { backgroundColor: cardColor, borderBottomColor: borderColor }]}>
        <View style={styles.groupPickerRow}>
          <ThemedText style={[styles.filterLabel, { color: subtitleColor }]}>User Group:</ThemedText>
          <TouchableOpacity
            style={[styles.groupSelectBtn, { borderColor, backgroundColor: isDark ? '#2A2A2A' : '#FFF' }]}
            onPress={() => setIsGroupLookupVisible(true)}
            activeOpacity={0.7}
          >
            <ThemedText style={[styles.groupSelectText, { color: textColor }]}>
              {selectedGroupCode || 'Select Group'}
            </ThemedText>
            <Ionicons name="chevron-down" size={16} color={subtitleColor} style={{ marginLeft: 6 }} />
          </TouchableOpacity>
        </View>

        <View style={[styles.searchBox, { borderColor, backgroundColor: isDark ? '#2A2A2A' : '#FFF' }]}>
          <Ionicons name="search-outline" size={16} color={subtitleColor} style={{ marginRight: 6 }} />
          <TextInput
            style={[styles.searchInput, { color: textColor }]}
            placeholder="Search menu node..."
            placeholderTextColor={subtitleColor}
            value={searchQuery}
            onChangeText={setSearchQuery}
          />
          {searchQuery ? (
            <TouchableOpacity onPress={() => setSearchQuery('')}>
              <Ionicons name="close-circle" size={16} color={subtitleColor} />
            </TouchableOpacity>
          ) : null}
        </View>
      </View>

      {/* Category Pills */}
      <View style={[styles.categoryBar, { borderBottomColor: borderColor }]}>
        <FlatList
          horizontal
          showsHorizontalScrollIndicator={false}
          data={categories}
          keyExtractor={item => item}
          contentContainerStyle={{ paddingHorizontal: 12, gap: 6, alignItems: 'center' }}
          renderItem={({ item }) => {
            const isSelected = selectedCategory === item;
            return (
              <TouchableOpacity
                onPress={() => setSelectedCategory(item)}
                style={[
                  styles.categoryPill,
                  { borderColor },
                  isSelected && { backgroundColor: accentColor, borderColor: accentColor },
                ]}
              >
                <ThemedText style={[styles.categoryPillText, { color: isSelected ? '#FFF' : textColor }]}>
                  {item}
                </ThemedText>
              </TouchableOpacity>
            );
          }}
        />
      </View>

      {/* Nodes Table */}
      <View style={styles.tableContainer}>
        {/* Table Header */}
        <View style={[styles.tableHeader, { backgroundColor: isDark ? '#262626' : '#F5E7CA', borderBottomColor: borderColor }]}>
          <View style={{ flex: 1.5, paddingLeft: 12 }}>
            <ThemedText style={[styles.headerColText, { color: '#3B2D17' }]}>MENU PAGE NODE (FormCode)</ThemedText>
          </View>
          <View style={{ flex: 1.2 }}>
            <ThemedText style={[styles.headerColText, { color: '#3B2D17' }]}>MENU CATEGORY</ThemedText>
          </View>
          <View style={styles.permHeaderCol}>
            <TouchableOpacity
              onPress={() => {
                const allChecked = filteredNodes.every(n => n.allowRead);
                handleSelectAll(!allChecked);
              }}
              style={styles.headerToggleBtn}
            >
              <ThemedText style={[styles.headerPermLabel, { color: '#2E7D32' }]}>
                {filteredNodes.every(n => n.allowRead) ? 'UNCHECK ALL' : 'CHECK ALL (Show)'}
              </ThemedText>
            </TouchableOpacity>
          </View>
        </View>

        {loading ? (
          <View style={styles.centered}>
            <ActivityIndicator size="large" color={accentColor} />
            <ThemedText style={{ marginTop: 12, color: subtitleColor }}>Loading menu node permissions...</ThemedText>
          </View>
        ) : (
          <FlatList
            data={filteredNodes}
            keyExtractor={item => `${item.category}_${item.nodeName}`}
            contentContainerStyle={{ paddingBottom: 60 }}
            renderItem={({ item, index }) => (
              <View
                style={[
                  styles.tableRow,
                  {
                    backgroundColor: index % 2 === 1 ? (isDark ? '#1E1E1E' : '#FCFAF6') : cardColor,
                    borderBottomColor: borderColor,
                  },
                ]}
              >
                {/* Node Name (FormCode) */}
                <View style={{ flex: 1.5, paddingLeft: 12 }}>
                  <ThemedText style={[styles.nodeTitleText, { color: textColor }]}>
                    {item.nodeName}
                  </ThemedText>
                </View>

                {/* Category */}
                <View style={{ flex: 1.2 }}>
                  <View style={[styles.categoryBadge, { backgroundColor: isDark ? '#333' : '#F5EBD4' }]}>
                    <ThemedText style={[styles.categoryText, { color: '#8D6409' }]}>
                      {item.category}
                    </ThemedText>
                  </View>
                </View>

                {/* Checkbox: Show / Hide Card on Menu Page */}
                <View style={styles.permCol}>
                  <TouchableOpacity
                    style={[
                      styles.checkboxBox,
                      item.allowRead ? { backgroundColor: '#4CAF50', borderColor: '#4CAF50' } : { borderColor },
                    ]}
                    onPress={() => toggleNodePermission(item.nodeName)}
                    activeOpacity={0.7}
                  >
                    {item.allowRead && <Ionicons name="checkmark" size={14} color="#FFF" />}
                  </TouchableOpacity>
                </View>
              </View>
            )}
            ListEmptyComponent={
              <View style={styles.centered}>
                <Ionicons name="folder-open-outline" size={40} color={subtitleColor} />
                <ThemedText style={{ color: subtitleColor, marginTop: 8 }}>No menu nodes found.</ThemedText>
              </View>
            }
          />
        )}
      </View>

      {/* Group Lookup Modal */}
      <Modal visible={isGroupLookupVisible} transparent animationType="fade">
        <TouchableOpacity style={styles.modalOverlay} activeOpacity={1} onPress={() => setIsGroupLookupVisible(false)}>
          <TouchableWithoutFeedback>
            <View style={[styles.lookupContent, { backgroundColor: cardColor, borderColor }]}>
              <View style={[styles.modalHeader, { borderBottomColor: borderColor }]}>
                <ThemedText style={{ fontSize: 16, fontWeight: 'bold', color: textColor }}>Select User Group</ThemedText>
                <TouchableOpacity onPress={() => setIsGroupLookupVisible(false)}>
                  <Ionicons name="close" size={22} color={textColor} />
                </TouchableOpacity>
              </View>
              <FlatList
                data={userGroups}
                keyExtractor={item => item.UserGroupCode}
                renderItem={({ item }) => (
                  <TouchableOpacity
                    style={[styles.lookupRow, { borderBottomColor: borderColor }]}
                    onPress={() => {
                      setSelectedGroupCode(item.UserGroupCode);
                      setIsGroupLookupVisible(false);
                    }}
                  >
                    <ThemedText style={{ color: textColor, fontWeight: 'bold', fontSize: 13 }}>{item.UserGroupCode}</ThemedText>
                    {item.UserGroupName && item.UserGroupName !== item.UserGroupCode && (
                      <ThemedText style={{ color: subtitleColor, fontSize: 12 }}>{item.UserGroupName}</ThemedText>
                    )}
                  </TouchableOpacity>
                )}
              />
            </View>
          </TouchableWithoutFeedback>
        </TouchableOpacity>
      </Modal>

    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  safeArea: { flex: 1 },
  header: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingHorizontal: 16,
    paddingVertical: 10,
    borderBottomWidth: 1,
  },
  backButton: { padding: 4, marginRight: 10 },
  title: { fontSize: 16, fontWeight: 'bold' },
  saveAllBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 14,
    paddingVertical: 8,
    borderRadius: 6,
    gap: 6,
  },
  saveAllBtnText: {
    color: '#FFF',
    fontWeight: 'bold',
    fontSize: 12,
  },
  controlBar: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingHorizontal: 16,
    paddingVertical: 8,
    borderBottomWidth: 1,
    gap: 12,
  },
  groupPickerRow: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 8,
  },
  filterLabel: {
    fontSize: 12,
    fontWeight: 'bold',
  },
  groupSelectBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 12,
    paddingVertical: 6,
    borderRadius: 6,
    borderWidth: 1,
  },
  groupSelectText: {
    fontSize: 12,
    fontWeight: 'bold',
  },
  searchBox: {
    flex: 1,
    maxWidth: 320,
    flexDirection: 'row',
    alignItems: 'center',
    borderWidth: 1,
    borderRadius: 6,
    paddingHorizontal: 10,
    height: 32,
  },
  searchInput: {
    flex: 1,
    fontSize: 12,
    paddingVertical: 0,
  },
  categoryBar: {
    paddingVertical: 6,
    borderBottomWidth: 1,
    backgroundColor: 'rgba(0,0,0,0.01)',
  },
  categoryPill: {
    paddingHorizontal: 10,
    paddingVertical: 4,
    borderRadius: 14,
    borderWidth: 1,
  },
  categoryPillText: {
    fontSize: 11,
    fontWeight: '600',
  },
  tableContainer: {
    flex: 1,
    marginHorizontal: 16,
    marginVertical: 10,
    borderWidth: 1,
    borderColor: '#E8DCB8',
    borderRadius: 8,
    overflow: 'hidden',
    backgroundColor: '#FFF',
  },
  tableHeader: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingVertical: 10,
    borderBottomWidth: 1,
  },
  headerColText: {
    fontSize: 11,
    fontWeight: 'bold',
    letterSpacing: 0.5,
  },
  permHeaderCol: {
    width: 140,
    alignItems: 'center',
    justifyContent: 'center',
  },
  headerToggleBtn: {
    paddingHorizontal: 6,
    paddingVertical: 3,
    borderRadius: 4,
  },
  headerPermLabel: {
    fontSize: 10.5,
    fontWeight: '800',
  },
  tableRow: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingVertical: 8,
    borderBottomWidth: 1,
  },
  nodeTitleText: {
    fontSize: 12.5,
    fontWeight: '600',
  },
  categoryBadge: {
    alignSelf: 'flex-start',
    paddingHorizontal: 8,
    paddingVertical: 3,
    borderRadius: 4,
  },
  categoryText: {
    fontSize: 11,
    fontWeight: '600',
  },
  permCol: {
    width: 140,
    alignItems: 'center',
    justifyContent: 'center',
  },
  checkboxBox: {
    width: 22,
    height: 22,
    borderRadius: 5,
    borderWidth: 1.5,
    alignItems: 'center',
    justifyContent: 'center',
    backgroundColor: '#FFF',
  },
  centered: {
    flex: 1,
    alignItems: 'center',
    justifyContent: 'center',
    padding: 30,
  },
  modalOverlay: {
    flex: 1,
    backgroundColor: 'rgba(0,0,0,0.5)',
    justifyContent: 'center',
    alignItems: 'center',
    padding: 20,
  },
  lookupContent: {
    width: '100%',
    maxWidth: 380,
    maxHeight: 460,
    borderRadius: 8,
    borderWidth: 1,
    overflow: 'hidden',
  },
  modalHeader: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    paddingHorizontal: 16,
    paddingVertical: 12,
    borderBottomWidth: 1,
  },
  lookupRow: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    paddingHorizontal: 16,
    paddingVertical: 12,
    borderBottomWidth: 1,
  },
});
