const { getDefaultConfig } = require('expo/metro-config');

const config = getDefaultConfig(__dirname);

// Exclude any non-route helper files or backup files inside app from being registered as routes
config.resolver.blockList = [
  ...Array.isArray(config.resolver.blockList) ? config.resolver.blockList : [],
  /.*\/app\/directpurchase\.js$/,
];

module.exports = config;
