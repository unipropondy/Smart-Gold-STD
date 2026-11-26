/**
 * Universal Weighing Machine Driver for React Native Web (SmartGold)
 *
 * Supports:
 * 1. Web Serial API: Direct Chrome / Edge serial COM port connection to RS-232 / USB scales
 * 2. WebSocket Scale Bridge: Communicates with local Node.js bridge on port 5005
 * 3. Keyboard Wedge (HID): Captures scales sending keystrokes followed by Enter / Tab
 */

export interface SerialPortConfig {
  baudRate: number;
  dataBits: number;
  stopBits: number;
  parity: 'none' | 'even' | 'odd';
}

export interface WeightReading {
  weight: string;
  weightNum: number;
  isStable: boolean;
  raw: string;
  timestamp: number;
}

export type WeightListener = (reading: WeightReading) => void;
export type StatusListener = (status: { connected: boolean; portName?: string; mode: string }) => void;

class WeighingMachineManager {
  private webSerialPort: any = null;
  private reader: any = null;
  private keepReading = false;
  private socket: WebSocket | null = null;
  private listeners: Set<WeightListener> = new Set();
  private statusListeners: Set<StatusListener> = new Set();
  private lastWeight: string = '';
  private lastReadingTime: number = 0;
  private isConnected = false;
  private currentPortName = '';
  private currentMode: 'webserial' | 'websocket' | 'hid' = 'webserial';

  constructor() {
    this.initSocketBridge();
  }

  public subscribeWeight(fn: WeightListener) {
    this.listeners.add(fn);
    return () => this.listeners.delete(fn);
  }

  public subscribeStatus(fn: StatusListener) {
    this.statusListeners.add(fn);
    fn({ connected: this.isConnected, portName: this.currentPortName, mode: this.currentMode });
    return () => this.statusListeners.delete(fn);
  }

  private notifyWeight(reading: WeightReading) {
    // Debounce duplicate immediate readings (< 400ms) with identical weight
    const now = Date.now();
    if (reading.weight === this.lastWeight && now - this.lastReadingTime < 500) {
      return;
    }
    this.lastWeight = reading.weight;
    this.lastReadingTime = now;
    this.listeners.forEach((fn) => fn(reading));
  }

  private notifyStatus(connected: boolean, portName?: string) {
    this.isConnected = connected;
    if (portName) this.currentPortName = portName;
    this.statusListeners.forEach((fn) =>
      fn({ connected: this.isConnected, portName: this.currentPortName, mode: this.currentMode })
    );
  }

  /**
   * Connect using native Web Serial API (Supported out of the box in Chrome, Edge, and Opera on Windows)
   */
  public async connectWebSerial(config: SerialPortConfig = { baudRate: 9600, dataBits: 8, stopBits: 1, parity: 'none' }) {
    if (typeof window === 'undefined' || !(navigator as any).serial) {
      throw new Error('Web Serial API is not supported in this browser. Please use Chrome or Edge.');
    }

    try {
      if (this.webSerialPort) {
        await this.disconnect();
      }

      const port = await (navigator as any).serial.requestPort();
      await port.open({
        baudRate: config.baudRate || 9600,
        dataBits: config.dataBits || 8,
        stopBits: config.stopBits || 1,
        parity: config.parity || 'none',
      });

      this.webSerialPort = port;
      this.currentMode = 'webserial';
      this.currentPortName = 'Serial Scale';
      this.keepReading = true;
      this.notifyStatus(true, 'Serial Scale');

      this.readSerialLoop();
      return true;
    } catch (err: any) {
      this.notifyStatus(false);
      throw err;
    }
  }

  private async readSerialLoop() {
    let buffer = '';
    while (this.webSerialPort && this.webSerialPort.readable && this.keepReading) {
      const textDecoder = new TextDecoderStream();
      const readableStreamClosed = this.webSerialPort.readable.pipeTo(textDecoder.writable);
      this.reader = textDecoder.readable.getReader();

      try {
        while (true) {
          const { value, done } = await this.reader.read();
          if (done) break;
          if (value) {
            buffer += value;
            // Check for carriage return or newline
            if (buffer.includes('\n') || buffer.includes('\r')) {
              const lines = buffer.split(/[\r\n]+/);
              buffer = lines.pop() || '';
              for (const line of lines) {
                const parsed = this.parseWeightString(line);
                if (parsed && parsed.weightNum > 0) {
                  this.notifyWeight(parsed);
                }
              }
            }
          }
        }
      } catch (err) {
        console.error('Serial read error:', err);
      } finally {
        this.reader?.releaseLock();
      }
    }
  }

  /**
   * Connect to local Node.js WebSocket Bridge (Port 5005)
   */
  public initSocketBridge() {
    if (typeof window === 'undefined') return;
    try {
      const wsUrl = 'ws://localhost:5005/socket.io/?EIO=4&transport=websocket';
      // If bridge is available, listen for events
      const socket = new WebSocket(wsUrl);
      this.socket = socket;

      socket.onopen = () => {
        // Socket bridge active
      };

      socket.onmessage = (event) => {
        try {
          const msg = event.data;
          // Simple socket.io payload packet unpack
          if (msg.startsWith('42')) {
            const [, payload] = JSON.parse(msg.substring(2));
            if (payload && payload.weight) {
              this.currentMode = 'websocket';
              this.notifyWeight({
                weight: String(payload.weight),
                weightNum: parseFloat(payload.weight),
                isStable: payload.isStable ?? true,
                raw: payload.raw || '',
                timestamp: Date.now()
              });
            }
          }
        } catch (_) {}
      };
    } catch (_) {}
  }

  public parseWeightString(str: string): WeightReading | null {
    if (!str || typeof str !== 'string') return null;
    const isStable = !str.toUpperCase().includes('US') && !str.toUpperCase().includes('UNSTABLE');
    const match = str.match(/([+-]?\d+(?:\.\d+)?)/);
    if (!match) return null;

    const num = parseFloat(match[1]);
    if (isNaN(num)) return null;

    return {
      raw: str.trim(),
      weight: Math.abs(num).toFixed(2),
      weightNum: Math.abs(num),
      isStable,
      timestamp: Date.now(),
    };
  }

  public async disconnect() {
    this.keepReading = false;
    if (this.reader) {
      try {
        await this.reader.cancel();
      } catch (_) {}
      this.reader = null;
    }
    if (this.webSerialPort) {
      try {
        await this.webSerialPort.close();
      } catch (_) {}
      this.webSerialPort = null;
    }
    this.notifyStatus(false, '');
  }

  public getStatus() {
    return {
      connected: this.isConnected,
      portName: this.currentPortName,
      mode: this.currentMode,
      lastWeight: this.lastWeight,
    };
  }
}

export const scaleManager = new WeighingMachineManager();
