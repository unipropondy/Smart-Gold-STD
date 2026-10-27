import { Colors } from '@/constants/theme';
import { useThemeContext } from '@/contexts/ThemeContext';

export function useThemeColors() {
  const { theme } = useThemeContext();
  // Fallback to light theme if the user's saved theme (like rose-gold or emerald) was removed
  return Colors[theme] || Colors.light;
}
