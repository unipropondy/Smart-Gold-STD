const express = require('express');
const cors = require('cors');
const si = require('systeminformation');

const app = express();
const PORT = 5005;

// Enable CORS for web client requests from localhost / 127.0.0.1
app.use(cors());
app.use(express.json());

/**
 * GET http://127.0.0.1:5005/system-info
 * Returns dynamic Windows PC name using systeminformation package
 */
app.get('/system-info', async (req, res) => {
  try {
    const info = await si.osInfo();
    const pcName = (info.hostname || process.env.COMPUTERNAME || '').trim();

    return res.status(200).json({
      success: true,
      pcName: pcName
    });
  } catch (error) {
    console.error('Error retrieving PC name via systeminformation:', error);
    return res.status(500).json({
      success: false,
      message: 'Failed to retrieve system information',
      error: error.message
    });
  }
});

app.listen(PORT, '127.0.0.1', () => {
  console.log(`🚀 SmartGold Local System Service running on http://127.0.0.1:${PORT}`);
  console.log(`Endpoint: http://127.0.0.1:${PORT}/system-info`);
});
