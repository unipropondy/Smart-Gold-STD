const express = require('express');
const router = express.Router();
const { exec } = require('child_process');
const util = require('util');
const execPromise = util.promisify(exec);

// Helper function to deduce printer type and description dynamically from system info
function getPrinterTypeAndDesc(name, port) {
  const upperName = (name || '').toUpperCase();
  const upperPort = (port || '').toUpperCase();

  let type = 'LAN';
  let description = 'Receipt Printer';

  if (upperName.includes('PDF')) {
    type = 'VIRTUAL';
    description = 'PDF Printer';
  } else if (upperName.includes('ONENOTE')) {
    type = 'VIRTUAL';
    description = 'Virtual Printer';
  } else if (upperName.includes('FAX')) {
    type = 'VIRTUAL';
    description = 'Fax Printer';
  } else if (upperName.includes('TEXT')) {
    type = 'VIRTUAL';
    description = 'Text Printer';
  } else if (upperPort.includes('USB')) {
    type = 'USB';
    description = 'Receipt Printer';
  } else if (upperPort.includes('WSD') || upperPort.includes('NET') || upperPort.includes('IP_') || upperPort.includes('192.')) {
    type = 'NETWORK';
    description = 'Receipt Printer';
  } else if (upperPort.includes('PORTPROMPT') || upperPort.includes('NUL')) {
    type = 'VIRTUAL';
    description = 'Virtual Printer';
  }

  return { type, description };
}

// Helper function to dynamically discover system printers using Windows PowerShell
async function getSystemPrinters() {
  try {
    const { stdout } = await execPromise(
      `powershell -Command "Get-CimInstance -Class Win32_Printer | Select-Object Name, Default, PortName | ConvertTo-Json"`,
      { timeout: 7000 }
    );
    if (!stdout || !stdout.trim()) return [];
    const parsed = JSON.parse(stdout);
    const list = Array.isArray(parsed) ? parsed : [parsed];
    return list.map(p => ({
      name: p.Name,
      isDefault: Boolean(p.Default),
      portName: p.PortName || 'Local Port'
    })).filter(p => p.name && p.name.trim() !== '');
  } catch (err) {
    console.error('[Printer Discovery] PowerShell query failed or timed out:', err.message);
    return [];
  }
}

// Helper function to ensure PrinterSettings table exists & is synchronized with system printers
async function ensurePrinterTableAndSync(pool) {
  try {
    // 1. Ensure Table Exists
    await pool.request().query(`
      IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'PrinterSettings')
      BEGIN
          CREATE TABLE PrinterSettings
          (
              PrinterID INT IDENTITY(1,1) PRIMARY KEY,
              PrinterName NVARCHAR(255) NOT NULL,
              PrinterType NVARCHAR(50) NULL,
              Description NVARCHAR(255) NULL,
              IsDefault BIT NOT NULL DEFAULT 0,
              IsActive BIT NOT NULL DEFAULT 1,
              CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
              UpdatedDate DATETIME NULL
          );
      END
    `);

    // Ensure Description column exists if table was created previously without it
    await pool.request().query(`
      IF NOT EXISTS (
        SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
        WHERE TABLE_NAME = 'PrinterSettings' AND COLUMN_NAME = 'Description'
      )
      BEGIN
        ALTER TABLE PrinterSettings ADD Description NVARCHAR(255) NULL;
      END
    `);

    // 2. Discover System Printers
    const sysPrinters = await getSystemPrinters();
    if (sysPrinters.length > 0) {
      for (const sysP of sysPrinters) {
        const checkReq = pool.request();
        checkReq.input('PrinterName', sysP.name);
        const existing = await checkReq.query(`
          SELECT PrinterID, IsDefault FROM PrinterSettings WHERE PrinterName = @PrinterName AND IsActive = 1
        `);

        const { type, description } = getPrinterTypeAndDesc(sysP.name, sysP.portName);

        if (existing.recordset.length === 0) {
          const insReq = pool.request();
          insReq.input('PrinterName', sysP.name);
          insReq.input('PrinterType', type);
          insReq.input('Description', description);
          insReq.input('IsDefault', sysP.isDefault ? 1 : 0);
          await insReq.query(`
            INSERT INTO PrinterSettings (PrinterName, PrinterType, Description, IsDefault, IsActive, CreatedDate)
            VALUES (@PrinterName, @PrinterType, @Description, @IsDefault, 1, GETDATE())
          `);
        }
      }
    }

    // 3. Ensure at least one default printer exists if active records exist
    const defaultCheck = await pool.request().query(`
      SELECT COUNT(*) AS count FROM PrinterSettings WHERE IsDefault = 1 AND IsActive = 1
    `);
    
    if (defaultCheck.recordset[0].count === 0) {
      const activeCheck = await pool.request().query(`
        SELECT TOP 1 PrinterID FROM PrinterSettings WHERE IsActive = 1 ORDER BY PrinterID ASC
      `);
      if (activeCheck.recordset.length > 0) {
        const setDefReq = pool.request();
        setDefReq.input('PrinterID', activeCheck.recordset[0].PrinterID);
        await setDefReq.query(`
          UPDATE PrinterSettings SET IsDefault = 1, UpdatedDate = GETDATE() WHERE PrinterID = @PrinterID
        `);
      }
    }
  } catch (err) {
    console.error('[Printer Sync Error]:', err.message);
  }
}

// GET /api/printer/default - Get current default printer
router.get('/default', async (req, res) => {
  try {
    const pool = req.pool;
    await ensurePrinterTableAndSync(pool);

    const result = await pool.request().query(`
      SELECT TOP 1
        PrinterID AS printerId,
        PrinterName AS printerName,
        PrinterType AS printerType,
        Description AS description,
        IsDefault AS isDefault,
        IsActive AS isActive
      FROM PrinterSettings
      WHERE IsDefault = 1 AND IsActive = 1
    `);

    if (result.recordset.length === 0) {
      return res.json({
        success: true,
        data: null,
        message: 'No default printer configured.'
      });
    }

    const item = result.recordset[0];
    const { type, description } = getPrinterTypeAndDesc(item.printerName, item.printerType);

    res.json({
      success: true,
      data: {
        printerId: item.printerId,
        printerName: item.printerName,
        printerType: item.printerType || type,
        description: item.description || description,
        isDefault: Boolean(item.isDefault),
        isActive: Boolean(item.isActive)
      }
    });
  } catch (err) {
    console.error('Fetch Default Printer Error:', err);
    res.status(500).json({
      success: false,
      message: 'Failed to retrieve current default printer.',
      error: err.message
    });
  }
});

// GET /api/printer/list - Get all available printers
router.get('/list', async (req, res) => {
  try {
    const pool = req.pool;
    await ensurePrinterTableAndSync(pool);

    const result = await pool.request().query(`
      SELECT
        PrinterID AS printerId,
        PrinterName AS printerName,
        PrinterType AS printerType,
        Description AS description,
        IsDefault AS isDefault,
        IsActive AS isActive
      FROM PrinterSettings
      WHERE IsActive = 1
      ORDER BY IsDefault DESC, PrinterID DESC
    `);

    const printers = result.recordset.map(item => {
      const { type, description } = getPrinterTypeAndDesc(item.printerName, item.printerType);
      return {
        printerId: item.printerId,
        printerName: item.printerName,
        printerType: item.printerType || type,
        description: item.description || description,
        isDefault: Boolean(item.isDefault),
        isActive: Boolean(item.isActive)
      };
    });

    res.json({
      success: true,
      data: printers
    });
  } catch (err) {
    console.error('Fetch Available Printers Error:', err);
    res.status(500).json({
      success: false,
      message: 'Failed to retrieve available printers.',
      error: err.message
    });
  }
});

// POST /api/printer - Add a new printer
router.post('/', async (req, res) => {
  try {
    const pool = req.pool;
    await ensurePrinterTableAndSync(pool);

    const { printerName, printerType, description } = req.body;

    if (!printerName || !printerName.trim()) {
      return res.status(400).json({
        success: false,
        message: 'Please enter printer name.'
      });
    }

    if (!printerType || !printerType.trim()) {
      return res.status(400).json({
        success: false,
        message: 'Please enter printer type.'
      });
    }

    const nameTrimmed = printerName.trim();
    const typeTrimmed = printerType.trim();
    const descTrimmed = (description || '').trim() || 'Receipt Printer';

    // Check for duplicate printer name
    const checkReq = pool.request();
    checkReq.input('PrinterName', nameTrimmed);
    const checkRes = await checkReq.query(`
      SELECT PrinterID FROM PrinterSettings WHERE PrinterName = @PrinterName AND IsActive = 1
    `);

    if (checkRes.recordset.length > 0) {
      return res.status(400).json({
        success: false,
        message: 'Printer with this name already exists.'
      });
    }

    // Insert new printer
    const insReq = pool.request();
    insReq.input('PrinterName', nameTrimmed);
    insReq.input('PrinterType', typeTrimmed);
    insReq.input('Description', descTrimmed);

    const insertResult = await insReq.query(`
      INSERT INTO PrinterSettings (PrinterName, PrinterType, Description, IsDefault, IsActive, CreatedDate)
      OUTPUT INSERTED.PrinterID AS printerId
      VALUES (@PrinterName, @PrinterType, @Description, 0, 1, GETDATE())
    `);

    const newId = insertResult.recordset[0]?.printerId;

    res.json({
      success: true,
      message: 'Printer added successfully.',
      data: {
        printerId: newId,
        printerName: nameTrimmed,
        printerType: typeTrimmed,
        description: descTrimmed,
        isDefault: false,
        isActive: true
      }
    });

  } catch (err) {
    console.error('Add Printer Error:', err);
    res.status(500).json({
      success: false,
      message: 'Failed to add printer.',
      error: err.message
    });
  }
});

// PUT /api/printer/default - Set default printer
router.put('/default', async (req, res) => {
  const pool = req.pool;
  const { printerId } = req.body;

  if (!printerId || isNaN(Number(printerId))) {
    return res.status(400).json({
      success: false,
      message: 'Invalid or missing printerId.'
    });
  }

  const targetId = Number(printerId);
  const transaction = pool.transaction();

  try {
    // 1. Verify target printer exists and is active
    const checkReq = pool.request();
    checkReq.input('PrinterID', targetId);
    const checkResult = await checkReq.query(`
      SELECT PrinterID, PrinterName FROM PrinterSettings WHERE PrinterID = @PrinterID AND IsActive = 1
    `);

    if (checkResult.recordset.length === 0) {
      return res.status(404).json({
        success: false,
        message: 'Selected printer does not exist or is inactive.'
      });
    }

    // 2. Safe transaction update using mssql Transaction
    await transaction.begin();

    const req1 = transaction.request();
    await req1.query(`
      UPDATE PrinterSettings
      SET IsDefault = 0, UpdatedDate = GETDATE()
      WHERE IsDefault = 1 AND IsActive = 1
    `);

    const req2 = transaction.request();
    req2.input('PrinterID', targetId);
    await req2.query(`
      UPDATE PrinterSettings
      SET IsDefault = 1, UpdatedDate = GETDATE()
      WHERE PrinterID = @PrinterID AND IsActive = 1
    `);

    await transaction.commit();

    res.json({
      success: true,
      message: 'Printer changed successfully.'
    });
  } catch (err) {
    console.error('Change Default Printer Error:', err);
    try {
      await transaction.rollback();
    } catch (rbErr) {
      console.error('Rollback Error:', rbErr);
    }

    res.status(500).json({
      success: false,
      message: 'Failed to change default printer.',
      error: err.message
    });
  }
});

module.exports = router;
