import React, { useState, useEffect, useRef } from 'react';
import {
  View,
  Text,
  TouchableOpacity,
  ScrollView,
  StyleSheet,
  Platform,
} from 'react-native';
import { Ionicons } from '@expo/vector-icons';

export interface ReportDropdownOption {
  id: string;
  label: string;
}

interface ReportDropdownProps {
  label?: string;
  options: ReportDropdownOption[];
  selectedId: string;
  onSelect: (id: string) => void;
  isOpen: boolean;
  onToggle: () => void;
  onClose?: () => void;
  placeholder?: string;
  maxHeight?: number;
  containerStyle?: any;
}

export const ReportDropdown: React.FC<ReportDropdownProps> = ({
  label = 'Report',
  options,
  selectedId,
  onSelect,
  isOpen,
  onToggle,
  onClose,
  placeholder = '-- Select --',
  maxHeight = 240,
  containerStyle,
}) => {
  const [hoveredId, setHoveredId] = useState<string | null>(null);
  const containerRef = useRef<any>(null);
  const selectedOption = options.find((o) => o.id === selectedId);

  // Outside click listener for web
  useEffect(() => {
    if (!isOpen || Platform.OS !== 'web') return;

    const handlePointerDown = (event: MouseEvent | TouchEvent) => {
      const node = containerRef.current;
      if (node) {
        // In react-native-web, refs on View resolve directly to the DOM element
        if (node.contains && node.contains(event.target as Node)) {
          return; // Click was inside
        }
      }
      // Clicked outside: close dropdown
      if (onClose) {
        onClose();
      } else {
        onToggle();
      }
    };

    document.addEventListener('mousedown', handlePointerDown);
    document.addEventListener('touchstart', handlePointerDown);

    return () => {
      document.removeEventListener('mousedown', handlePointerDown);
      document.removeEventListener('touchstart', handlePointerDown);
    };
  }, [isOpen, onToggle, onClose]);

  return (
    <View
      ref={containerRef}
      style={[
        styles.container,
        isOpen && styles.containerOpen,
        containerStyle,
      ]}
    >
      {label ? <Text style={styles.fieldLabel}>{label}</Text> : null}

      <TouchableOpacity
        style={[styles.selectorBox, isOpen && styles.selectorBoxActive]}
        onPress={onToggle}
        activeOpacity={0.8}
      >
        <Text
          style={[styles.selectorText, !selectedOption && styles.selectorPlaceholder]}
          numberOfLines={1}
        >
          {selectedOption ? selectedOption.label : placeholder}
        </Text>
        <Ionicons
          name={isOpen ? 'chevron-up' : 'chevron-down'}
          size={16}
          color="#B8860B"
        />
      </TouchableOpacity>

      {isOpen && (
        <View style={styles.dropdownMenu}>
          <ScrollView
            style={{ maxHeight }}
            nestedScrollEnabled
            keyboardShouldPersistTaps="handled"
          >
            {options.map((item) => {
              const isSelected = selectedId === item.id;
              const isHovered = hoveredId === item.id;

              return (
                <TouchableOpacity
                  key={item.id}
                  style={[
                    styles.menuItem,
                    isSelected && styles.menuItemSelected,
                    isHovered && !isSelected && styles.menuItemHovered,
                  ]}
                  onPress={() => {
                    onSelect(item.id);
                  }}
                  activeOpacity={0.7}
                  {...(Platform.OS === 'web'
                    ? ({
                        onMouseEnter: () => setHoveredId(item.id),
                        onMouseLeave: () => setHoveredId(null),
                      } as any)
                    : {})}
                >
                  <Text
                    style={[
                      styles.menuItemText,
                      isSelected && styles.menuItemTextSelected,
                      isHovered && !isSelected && styles.menuItemTextHovered,
                    ]}
                  >
                    {item.label}
                  </Text>
                  {isSelected && (
                    <Ionicons name="checkmark" size={16} color="#B8860B" />
                  )}
                </TouchableOpacity>
              );
            })}
          </ScrollView>
        </View>
      )}
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    position: 'relative',
    zIndex: 10,
  },
  containerOpen: {
    zIndex: 999999,
  },
  fieldLabel: {
    fontSize: 12,
    fontWeight: '700',
    color: '#3B2D17',
    marginBottom: 6,
    fontFamily: 'Cambria, serif',
  },
  selectorBox: {
    height: 38,
    borderWidth: 1,
    borderColor: '#D4C3A3',
    borderRadius: 6,
    backgroundColor: '#FFFFFF',
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingHorizontal: 12,
    cursor: 'pointer',
  } as any,
  selectorBoxActive: {
    borderColor: '#B8860B',
  },
  selectorText: {
    fontSize: 13,
    color: '#3B2D17',
    fontFamily: 'Cambria, serif',
    flex: 1,
    marginRight: 6,
  },
  selectorPlaceholder: {
    color: '#9E8D77',
  },
  dropdownMenu: {
    position: 'absolute',
    top: 66,
    left: 0,
    right: 0,
    backgroundColor: '#FFFFFF',
    borderRadius: 6,
    borderWidth: 1,
    borderColor: '#D4C3A3',
    shadowColor: '#000000',
    shadowOffset: { width: 0, height: 6 },
    shadowOpacity: 0.18,
    shadowRadius: 10,
    elevation: 9999,
    zIndex: 999999,
    ...(Platform.OS === 'web'
      ? ({
          boxShadow: '0 8px 24px rgba(0, 0, 0, 0.18)',
        } as any)
      : {}),
  },
  menuItem: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingVertical: 10,
    paddingHorizontal: 14,
    borderBottomWidth: 1,
    borderBottomColor: '#F5EFE6',
    cursor: 'pointer',
    backgroundColor: '#FFFFFF',
  } as any,
  menuItemHovered: {
    backgroundColor: '#FFFBF2',
  },
  menuItemSelected: {
    backgroundColor: '#FFF4DA',
  },
  menuItemText: {
    fontSize: 13,
    color: '#3B2D17',
    fontFamily: 'Cambria, serif',
  },
  menuItemTextHovered: {
    color: '#B8860B',
    fontWeight: '700',
  },
  menuItemTextSelected: {
    color: '#B8860B',
    fontWeight: '700',
  },
});
