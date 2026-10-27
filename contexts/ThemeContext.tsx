import React, { createContext, useContext, useState, useEffect } from 'react';
import AsyncStorage from '@react-native-async-storage/async-storage';
import { useColorScheme } from 'react-native';
import { ThemeType } from '@/constants/theme';

interface ThemeContextType {
  theme: ThemeType;
  setTheme: (theme: ThemeType) => void;
  accentColor: string;
  setAccentColor: (color: string) => void;
}

const ThemeContext = createContext<ThemeContextType | undefined>(undefined);

export const ThemeProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const systemTheme = useColorScheme() ?? 'light';
  const [theme, setThemeState] = useState<ThemeType>(systemTheme as ThemeType);
  const [accentColor, setAccentColorState] = useState<string>('#E6AF2E');

  useEffect(() => {
    // Load saved theme and accent color on mount
    const loadTheme = async () => {
      try {
        const savedTheme = await AsyncStorage.getItem('@app_theme');
        if (savedTheme) {
          setThemeState(savedTheme as ThemeType);
        }
        const savedAccent = await AsyncStorage.getItem('@app_accent_color');
        if (savedAccent) {
          setAccentColorState(savedAccent);
        }
      } catch (error) {
        console.error('Failed to load theme:', error);
      }
    };
    loadTheme();
  }, []);

  const setTheme = async (newTheme: ThemeType) => {
    setThemeState(newTheme);
    try {
      await AsyncStorage.setItem('@app_theme', newTheme);
    } catch (error) {
      console.error('Failed to save theme:', error);
    }
  };

  const setAccentColor = async (newColor: string) => {
    setAccentColorState(newColor);
    try {
      await AsyncStorage.setItem('@app_accent_color', newColor);
    } catch (error) {
      console.error('Failed to save accent color:', error);
    }
  };

  return (
    <ThemeContext.Provider value={{ theme, setTheme, accentColor, setAccentColor }}>
      {children}
    </ThemeContext.Provider>
  );
};

export const useThemeContext = () => {
  const context = useContext(ThemeContext);
  if (context === undefined) {
    throw new Error('useThemeContext must be used within a ThemeProvider');
  }
  return context;
};
