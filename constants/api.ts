import { Platform } from 'react-native';

const getBaseUrl = (): string => {
  if (Platform.OS === 'web' && typeof window !== 'undefined') {
    const hostname = window.location.hostname;
    const protocol = window.location.protocol;
    // If running in web browser on localhost or local LAN IP or static public IP, target backend on port 3000
    if (hostname === 'localhost' || hostname === '127.0.0.1' || /^(\d{1,3}\.){3}\d{1,3}$/.test(hostname)) {
      return `${protocol}//${hostname}:3000/api`;
    }
  }
  return 'https://smart-gold-std-production.up.railway.app/api';
};

export const API_BASE_URL = getBaseUrl();


