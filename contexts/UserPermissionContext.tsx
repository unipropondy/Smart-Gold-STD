import React, { createContext, useContext, useEffect, useState } from 'react';
import AsyncStorage from '@react-native-async-storage/async-storage';
import { Platform } from 'react-native';
import { API_BASE_URL } from '@/constants/api';
import { MENU_STRUCTURE, MenuItem } from '@/constants/menuConfig';

interface UserPermissionContextType {
  userGroupCode: string;
  isAdmin: boolean;
  loading: boolean;
  permittedNodes: Set<string>; // Set of allowed node titles / FormCodes (lowercase)
  isNodeVisible: (nodeNameOrItem: string | MenuItem) => boolean;
  isCategoryVisible: (categoryKey: string) => boolean;
  refreshPermissions: () => Promise<void>;
  setUserSession: (user: { userCode?: string; userName?: string; userGroupCode?: string; admin?: any }) => Promise<void>;
  clearPermissions: () => Promise<void>;
}

const UserPermissionContext = createContext<UserPermissionContextType>({
  userGroupCode: '',
  isAdmin: false,
  loading: true,
  permittedNodes: new Set<string>(),
  isNodeVisible: () => false,
  isCategoryVisible: () => false,
  refreshPermissions: async () => {},
  setUserSession: async () => {},
  clearPermissions: async () => {},
});

export const UserPermissionProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const [userGroupCode, setUserGroupCode] = useState<string>('');
  const [isAdmin, setIsAdmin] = useState<boolean>(false);
  const [permittedNodes, setPermittedNodes] = useState<Set<string>>(new Set<string>());
  const [loading, setLoading] = useState<boolean>(true);

  const fetchPermissionsForGroup = async (groupCode: string) => {
    if (!groupCode) return new Set<string>();
    try {
      const res = await fetch(`${API_BASE_URL}/userpermission/by-group/${encodeURIComponent(groupCode)}?t=${Date.now()}`);
      const json = await res.json();
      if (json.success && Array.isArray(json.data)) {
        const allowed = new Set<string>();
        json.data.forEach((p: any) => {
          const formCode = String(p.FormCode || '').trim();
          const allowRead = p.AllowRead === 'Y' || p.AllowRead === '1' || p.AllowRead === true;
          if (allowRead && formCode) {
            allowed.add(formCode.toLowerCase());
          }
        });
        return allowed;
      }
    } catch (e) {
      console.error('Error fetching group node permissions:', e);
    }
    return new Set<string>();
  };

  // Reset all state on logout or user switch
  const clearPermissions = async () => {
    setUserGroupCode('');
    setIsAdmin(false);
    setPermittedNodes(new Set<string>());
    setLoading(false);
  };

  // Directly set user and immediately fetch its permissions without relying on race conditions
  const setUserSession = async (user: { userCode?: string; userName?: string; userGroupCode?: string; admin?: any }) => {
    setLoading(true);
    try {
      const uGroup = (user.userGroupCode || '').trim();
      const upperGroup = uGroup.toUpperCase();
      const isAdm = !!user.admin || upperGroup === 'ADMIN' || upperGroup === 'SUPER ADMIN';

      setUserGroupCode(uGroup);
      setIsAdmin(isAdm);

      if (uGroup) {
        const nodes = await fetchPermissionsForGroup(uGroup);
        setPermittedNodes(nodes);
      } else {
        setPermittedNodes(new Set<string>());
      }
    } finally {
      setLoading(false);
    }
  };

  const loadUserAndPermissions = async () => {
    setLoading(true);
    try {
      let uGroup = '';
      let isAdm = false;

      // 1. Web LocalStorage / SessionStorage
      if (Platform.OS === 'web' && typeof window !== 'undefined') {
        try {
          const userStr = window.localStorage.getItem('user') || window.sessionStorage.getItem('user');
          if (userStr) {
            const uObj = JSON.parse(userStr);
            uGroup = uObj.userGroupCode || uObj.UserGroupCode || window.localStorage.getItem('userGroup') || '';
            isAdm = !!(uObj.admin || uObj.Admin);
          }
          if (!uGroup) {
            uGroup = window.localStorage.getItem('userGroup') || window.sessionStorage.getItem('userGroup') || '';
          }
        } catch (_) {}
      }

      // 2. Mobile AsyncStorage
      if (!uGroup) {
        try {
          const userStr = await AsyncStorage.getItem('@logged_user');
          if (userStr) {
            const uObj = JSON.parse(userStr);
            uGroup = uObj.userGroupCode || uObj.UserGroupCode || '';
            isAdm = !!(uObj.admin || uObj.Admin);
          }
          if (!uGroup) {
            uGroup = (await AsyncStorage.getItem('@user_group')) || '';
          }
        } catch (_) {}
      }

      uGroup = (uGroup || '').trim();
      const upperGroup = uGroup.toUpperCase();
      if (upperGroup === 'ADMIN' || upperGroup === 'SUPER ADMIN') {
        isAdm = true;
      }

      setUserGroupCode(uGroup);
      setIsAdmin(isAdm);

      if (uGroup) {
        const nodes = await fetchPermissionsForGroup(uGroup);
        setPermittedNodes(nodes);
      } else {
        setPermittedNodes(new Set<string>());
      }
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    loadUserAndPermissions();
  }, []);

  // Helper: check if a raw title is directly allowed in permittedNodes
  const hasDirectPermission = (title: string): boolean => {
    if (!title) return false;
    const clean = title.trim().toLowerCase();
    if (permittedNodes.has(clean)) return true;
    // Also test prefix match for truncated FormCode in DB (e.g. 'Customer Outlets' vs 'Customer Out')
    for (const allowed of permittedNodes) {
      if (clean.startsWith(allowed) || allowed.startsWith(clean)) {
        return true;
      }
    }
    return false;
  };

  // Helper: check if a category (like 'master', 'sales_submenu') has ANY permitted items inside it
  const isCategoryVisible = (categoryKey: string): boolean => {
    if (isAdmin) return true;
    if (!userGroupCode) return false;

    // Direct check if category title matches
    const mainItem = (MENU_STRUCTURE.home || []).find(h => h.id === categoryKey);
    if (mainItem && hasDirectPermission(mainItem.title)) return true;

    // Check items inside the category
    const items = MENU_STRUCTURE[categoryKey] || [];
    for (const item of items) {
      if (hasDirectPermission(item.title)) return true;
      // If item is itself a submenu (e.g. sales_submenu), check its children recursively
      if (item.hasSubmenu && MENU_STRUCTURE[item.id]) {
        if (isCategoryVisible(item.id)) return true;
      }
    }
    return false;
  };

  // Show/Hide check strictly based on Menu Page node name or MenuItem object
  const isNodeVisible = (nodeNameOrItem: string | MenuItem): boolean => {
    if (isAdmin) return true; // Administrator sees all cards
    if (!userGroupCode) return false;

    let title = '';
    let submenuId: string | undefined;

    if (typeof nodeNameOrItem === 'string') {
      title = nodeNameOrItem;
      // Look up if this title corresponds to a submenu container in MENU_STRUCTURE
      for (const key of Object.keys(MENU_STRUCTURE)) {
        const found = MENU_STRUCTURE[key]?.find(i => i.title.toLowerCase() === title.trim().toLowerCase());
        if (found && found.hasSubmenu) {
          submenuId = found.id;
          break;
        }
      }
    } else if (nodeNameOrItem && typeof nodeNameOrItem === 'object') {
      title = nodeNameOrItem.title;
      if (nodeNameOrItem.hasSubmenu) {
        submenuId = nodeNameOrItem.id;
      }
    }

    // 1. Direct permission on the node itself
    if (hasDirectPermission(title)) return true;

    // 2. If it's a submenu container (like 'Sales' / sales_submenu or 'Master'), check if ANY child item is permitted
    if (submenuId && MENU_STRUCTURE[submenuId]) {
      return isCategoryVisible(submenuId);
    }

    return false;
  };

  return (
    <UserPermissionContext.Provider
      value={{
        userGroupCode,
        isAdmin,
        loading,
        permittedNodes,
        isNodeVisible,
        isCategoryVisible,
        refreshPermissions: loadUserAndPermissions,
        setUserSession,
        clearPermissions,
      }}
    >
      {children}
    </UserPermissionContext.Provider>
  );
};

export const useUserPermissions = () => useContext(UserPermissionContext);


