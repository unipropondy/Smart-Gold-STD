import { Platform } from 'react-native';

// Base colors
const tintColorLight = '#0a7ea4';
const tintColorDark = '#fff';

export type ThemeType = 'light' | 'dark' | 'gold';

export const Colors: Record<ThemeType, any> = {
  light: {
    text: '#1A1A1A',        
    background: '#F9F9F9',  
    card: '#FFFFFF',        
    subtitle: '#8A817C',    
    border: '#EAE6DF',      
    primaryGold: '#D4AF37', 
    tint: tintColorLight,
    icon: '#687076',
    tabIconDefault: '#687076',
    tabIconSelected: tintColorLight,
  },
  dark: {
    text: '#FFFFFF',        
    background: '#0B0F19',  
    card: '#151A28',        
    subtitle: '#9CA3AF',    
    border: '#232A3B',      
    primaryGold: '#F2A900', 
    tint: tintColorDark,
    icon: '#9BA1A6',
    tabIconDefault: '#9BA1A6',
    tabIconSelected: tintColorDark,
  },
  gold: {
    text: '#382D16',
    background: '#FAF2E6', // Very soft warm gold tint background
    card: '#FFFFFF',
    subtitle: '#A68A56',
    border: '#EEDCBA',
    primaryGold: '#D4AF37', // Pure gold
    tint: '#D4AF37',
    icon: '#A68A56',
    tabIconDefault: '#A68A56',
    tabIconSelected: '#D4AF37',
  }
};

export const Fonts = Platform.select({
  ios: {
    sans: 'Cambria',
    serif: 'Cambria',
    rounded: 'Cambria',
    mono: 'Cambria',
  },
  default: {
    sans: 'Cambria',
    serif: 'Cambria',
    rounded: 'Cambria',
    mono: 'Cambria',
  },
  web: {
    sans: 'Cambria',
    serif: 'Cambria',
    rounded: 'Cambria',
    mono: 'Cambria',
  },
});
