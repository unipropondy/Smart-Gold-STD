const http = require('http');
const { Server } = require('socket.io');

let SerialPort = null;
let ReadlineParser = null;
try {
  const serial = require('serialport');
  SerialPort = serial.SerialPort || serial;
  ReadlineParser = require('@serialport/parser-readline').ReadlineParser;
} catch (e) {
  // If serialport is not installed globally or locally, Web Serial API in browser or socket client will still be supported
  console.log('[WeightBridge] Optional node "serialport" module not found or requires installation. Falling back to WebSocket client / Web Serial.');
}

const server = http.createServer((req, res) => {
  // Simple HTTP status and API endpoints
  res.setHeader('Access-Control-Allow-Origin', '*');
  res.setHeader('Access-Control-Allow-Methods', 'GET, POST, OPTIONS');
  res.setHeader('Access-Control-Allow-Headers', 'Content-Type');

  if (req.method === 'OPTIONS') {
    res.writeHead(204);
    res.end();
    return;
  }

  if (req.url === '/api/ports') {
    if (SerialPort && typeof SerialPort.list === 'function') {
      SerialPort.list()
        .then((ports) => {
          res.writeHead(200, { 'Content-Type': 'application/json' });
          res.end(JSON.stringify({ success: true, ports }));
        })
        .catch((err) => {
          res.writeHead(500, { 'Content-Type': 'application/json' });
          res.end(JSON.stringify({ success: false, error: err.message, ports: [] }));
        });
    } else {
      res.writeHead(200, { 'Content-Type': 'application/json' });
      res.end(JSON.stringify({
        success: true,
        ports: [
          { path: 'COM1', manufacturer: 'Standard Serial Port' },
          { path: 'COM2', manufacturer: 'Standard Serial Port' },
          { path: 'COM3', manufacturer: 'USB Serial Scale' },
          { path: 'COM4', manufacturer: 'USB Serial Scale' },
        ]
      }));
    }
    return;
  }

  if (req.url === '/status') {
    res.writeHead(200, { 'Content-Type': 'application/json' });
    res.end(JSON.stringify({
      running: true,
      activePort: activePort ? activePort.path : null,
      lastWeight,
      connectedClients: io.engine.clientsCount
    }));
    return;
  }

  res.writeHead(200, { 'Content-Type': 'text/plain' });
  res.end('SmartGold Weighing Machine Bridge Service Active');
});

const io = new Server(server, {
  cors: {
    origin: '*',
    methods: ['GET', 'POST']
  }
});

let activePort = null;
let activeParser = null;
let lastWeight = null;
let lastProcessedTime = 0;

/**
 * Robust ASCII weight extractor
 * Handles common scale formats like:
 * - "+  12.34 g"
 * - "ST,GS,  12.340g"
 * - "WT: 12.34"
 * - "12.34\r\n"
 */
function parseWeightFromAscii(dataStr) {
  if (!dataStr || typeof dataStr !== 'string') return null;

  // Check for stable/unstable markers if present
  const isStable = !dataStr.toUpperCase().includes('US') && !dataStr.toUpperCase().includes('UNSTABLE');

  // Match floating-point number, optional +/-
  const match = dataStr.match(/([+-]?\d+(?:\.\d+)?)/);
  if (!match) return null;

  const weightNum = parseFloat(match[1]);
  if (isNaN(weightNum)) return null;

  return {
    raw: dataStr.trim(),
    weight: Math.abs(weightNum).toFixed(2),
    weightNum: Math.abs(weightNum),
    isStable
  };
}

io.on('connection', (socket) => {
  console.log('[WeightBridge] Web client connected:', socket.id);

  // Send current status
  socket.emit('scale-status', {
    connected: activePort ? activePort.isOpen : false,
    port: activePort ? activePort.path : null,
    lastWeight
  });

  // Client requests list of available COM ports
  socket.on('list-ports', async (ack) => {
    try {
      let ports = [];
      if (SerialPort && typeof SerialPort.list === 'function') {
        ports = await SerialPort.list();
      } else {
        ports = [
          { path: 'COM1', manufacturer: 'Standard Serial Port' },
          { path: 'COM2', manufacturer: 'Standard Serial Port' },
          { path: 'COM3', manufacturer: 'USB Serial Scale' },
          { path: 'COM4', manufacturer: 'USB Serial Scale' },
        ];
      }
      if (typeof ack === 'function') ack({ success: true, ports });
    } catch (err) {
      if (typeof ack === 'function') ack({ success: false, error: err.message });
    }
  });

  // Client requests connecting to COM port
  socket.on('connect-port', (options, ack) => {
    const {
      port = 'COM1',
      baudRate = 9600,
      dataBits = 8,
      parity = 'none',
      stopBits = 1
    } = options || {};

    try {
      if (activePort && activePort.isOpen) {
        activePort.close();
      }

      if (!SerialPort) {
        const msg = 'Node serialport is not installed on this machine. Web Serial API in browser will be used directly.';
        socket.emit('scale-error', { message: msg });
        if (typeof ack === 'function') ack({ success: false, message: msg });
        return;
      }

      activePort = new SerialPort({
        path: port,
        baudRate: Number(baudRate) || 9600,
        dataBits: Number(dataBits) || 8,
        parity: parity,
        stopBits: Number(stopBits) || 1,
        autoOpen: true
      });

      activeParser = activePort.pipe(new ReadlineParser({ delimiter: '\r\n' }));

      activePort.on('open', () => {
        console.log(`[WeightBridge] Serial port ${port} opened successfully at ${baudRate} baud`);
        io.emit('scale-status', { connected: true, port, baudRate });
        if (typeof ack === 'function') ack({ success: true, port });
      });

      activeParser.on('data', (line) => {
        const parsed = parseWeightFromAscii(line);
        if (parsed && parsed.weightNum > 0) {
          lastWeight = parsed.weight;
          const now = Date.now();
          // Broadcast live reading to all connected web clients
          io.emit('weight-data', {
            ...parsed,
            timestamp: now
          });
        }
      });

      activePort.on('error', (err) => {
        console.error('[WeightBridge] Serial port error:', err.message);
        io.emit('scale-error', { error: err.message, port });
      });

      activePort.on('close', () => {
        console.log('[WeightBridge] Serial port closed');
        io.emit('scale-status', { connected: false, port: null });
      });

    } catch (err) {
      console.error('[WeightBridge] Failed to connect port:', err.message);
      if (typeof ack === 'function') ack({ success: false, error: err.message });
    }
  });

  // Client requests disconnect
  socket.on('disconnect-port', (ack) => {
    if (activePort && activePort.isOpen) {
      activePort.close((err) => {
        if (typeof ack === 'function') ack({ success: !err });
      });
    } else {
      if (typeof ack === 'function') ack({ success: true });
    }
  });

  // Allow software simulation of scale weights for testing or direct manual push
  socket.on('simulate-weight', (data) => {
    const parsed = parseWeightFromAscii(String(data.weight || data));
    if (parsed) {
      lastWeight = parsed.weight;
      io.emit('weight-data', {
        ...parsed,
        timestamp: Date.now()
      });
    }
  });
});

const PORT = process.env.WEIGHT_BRIDGE_PORT || 5005;
server.listen(PORT, () => {
  console.log(`[WeightBridge] Scale bridge server listening on port ${PORT}`);
});
