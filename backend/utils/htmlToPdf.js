const fs = require('fs');
const path = require('path');
const os = require('os');
const { execFile } = require('child_process');

function findBrowserPath() {
  const possiblePaths = [
    // Windows Chrome & Edge
    'C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe',
    'C:\\Program Files (x86)\\Google\\Chrome\\Application\\chrome.exe',
    process.env.LOCALAPPDATA + '\\Google\\Chrome\\Application\\chrome.exe',
    'C:\\Program Files (x86)\\Microsoft\\Edge\\Application\\msedge.exe',
    'C:\\Program Files\\Microsoft\\Edge\\Application\\msedge.exe',
    // Linux / Cloud / Docker / Railway Chrome & Chromium
    '/usr/bin/google-chrome',
    '/usr/bin/google-chrome-stable',
    '/usr/bin/chromium',
    '/usr/bin/chromium-browser',
    '/snap/bin/chromium',
    '/usr/bin/msedge',
  ];

  for (const p of possiblePaths) {
    if (p && fs.existsSync(p)) {
      return p;
    }
  }
  return null;
}

/**
 * Converts exact HTML into a PDF buffer using headless Chrome / Edge
 */
function convertHtmlToPdf(htmlString) {
  return new Promise((resolve, reject) => {
    const browserPath = findBrowserPath();
    if (!browserPath) {
      return reject(new Error('No Chrome or Edge browser executable found on system.'));
    }

    const tempDir = os.tmpdir();
    const uniqueId = `report_${Date.now()}_${Math.random().toString(36).substring(2, 8)}`;
    const tempHtmlPath = path.join(tempDir, `${uniqueId}.html`);
    const tempPdfPath = path.join(tempDir, `${uniqueId}.pdf`);
    const tempUserDataDir = path.join(tempDir, `browser_data_${uniqueId}`);

    fs.writeFileSync(tempHtmlPath, htmlString, 'utf8');

    const args = [
      '--headless=new',
      '--disable-gpu',
      '--no-sandbox',
      '--disable-setuid-sandbox',
      '--disable-dev-shm-usage',
      '--no-pdf-header-footer',
      '--run-all-compositor-stages-before-draw',
      `--user-data-dir=${tempUserDataDir}`,
      `--print-to-pdf=${tempPdfPath}`,
      `file:///${tempHtmlPath.replace(/\\/g, '/')}`,
    ];

    execFile(browserPath, args, { timeout: 30000 }, (error) => {
      // Clean up HTML
      try {
        if (fs.existsSync(tempHtmlPath)) fs.unlinkSync(tempHtmlPath);
      } catch (e) {}

      // Clean up browser temp data dir
      try {
        if (fs.existsSync(tempUserDataDir)) {
          fs.rmSync(tempUserDataDir, { recursive: true, force: true });
        }
      } catch (e) {}

      if (error && !fs.existsSync(tempPdfPath)) {
        return reject(error);
      }

      try {
        if (fs.existsSync(tempPdfPath)) {
          const pdfBuffer = fs.readFileSync(tempPdfPath);
          fs.unlinkSync(tempPdfPath);
          resolve(pdfBuffer);
        } else {
          reject(new Error('PDF generation produced no output file.'));
        }
      } catch (readErr) {
        reject(readErr);
      }
    });
  });
}

module.exports = { convertHtmlToPdf };
