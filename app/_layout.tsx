import { DarkTheme, DefaultTheme, ThemeProvider as NavigationThemeProvider } from '@react-navigation/native';
import { Stack } from 'expo-router';
import { StatusBar } from 'expo-status-bar';
import 'react-native-reanimated';

import { useColorScheme } from '@/hooks/use-color-scheme';
import { Text, TextInput } from 'react-native';
import { ThemeProvider as CustomThemeProvider } from '@/contexts/ThemeContext';
import { useThemeColors } from '@/hooks/useThemeColors';
import { useThemeContext } from '@/contexts/ThemeContext';

// Set global font family
const applyGlobalFont = () => {
  const customProps = { style: { fontFamily: 'Cambria' } };

  // @ts-ignore
  if (Text.defaultProps == null) Text.defaultProps = {};
  // @ts-ignore
  Text.defaultProps.style = { ...Text.defaultProps.style, fontFamily: 'Cambria' };

  // @ts-ignore
  if (TextInput.defaultProps == null) TextInput.defaultProps = {};
  // @ts-ignore
  TextInput.defaultProps.style = { ...TextInput.defaultProps.style, fontFamily: 'Cambria' };
};

applyGlobalFont();

export const unstable_settings = {
  initialRouteName: 'login',
};

export default function RootLayout() {
  // Theme hooks cannot be used here because CustomThemeProvider is rendered below
  const colorScheme = useColorScheme();

  return (
    <CustomThemeProvider>
      <NavigationThemeProvider value={colorScheme === 'dark' ? DarkTheme : DefaultTheme}>
        <Stack initialRouteName="login">
          <Stack.Screen name="index" options={{ headerShown: false }} />
          <Stack.Screen name="login" options={{ headerShown: false }} />
          <Stack.Screen name="(tabs)" options={{ headerShown: false }} />
          <Stack.Screen name="class" options={{ headerShown: false }} />
          <Stack.Screen name="modal" options={{ presentation: 'modal', title: 'Modal' }} />
        </Stack>
        <StatusBar style="auto" />
      </NavigationThemeProvider>
    </CustomThemeProvider>
  );
}
