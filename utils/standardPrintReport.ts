import { UNIPRO_LOGO_DATA_URI } from './uniproLogo';

export interface ReportCompanyInfo {
  name?: string;
  address?: string;
  phone?: string;
  fax?: string;
  logoUrl?: string;
}

export interface ReportFooterStaffInfo {
  staffLabel?: string;      // default: "PREPARED BY (STAFF)"
  staffName?: string;       // default: "UPS" or CreateUser or "-"
  receiverLabel?: string;   // default: "SUPPLIER / AUTHORIZED RECEIVER"
  receiverName?: string;    // default: SupplierName / CustomerName / "-"
  signText?: string;        // default: "Signature: ___________"
  dateText?: string;        // default: "Date: ___________"
}

export interface StandardReportHeaderProps {
  company: ReportCompanyInfo;
  reportTitle: string;
  printedDateTime?: string;
}

export interface StandardReportFooterProps {
  staff?: ReportFooterStaffInfo;
}

/**
 * Standard CSS rules to be included in the <style> block of any printable HTML document.
 * Ensures consistent layout across screen preview and paper print, supports multiple pages,
 * repeats the running header if table tags or standard page headers are used, and formats
 * the header and footer to match the exact design in the reference image.
 */
export const getStandardReportCss = (): string => `
  @page {
    size: A4 portrait;
    margin: 10mm 12mm;
  }
  *, *:before, *:after {
    box-sizing: border-box;
  }
  body {
    font-family: 'Segoe UI', Tahoma, Arial, sans-serif;
    margin: 0;
    padding: 0;
    background-color: #525659;
    color: #222;
    -webkit-print-color-adjust: exact !important;
    print-color-adjust: exact !important;
  }
  .page-sheet {
    background: #FFFFFF;
    width: 210mm;
    min-height: 297mm;
    margin: 10mm auto;
    padding: 12mm 14mm;
    box-shadow: 0 0 14px rgba(0,0,0,0.4);
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    box-sizing: border-box;
  }
  @media print {
    body {
      background: #FFFFFF !important;
    }
    .page-sheet {
      margin: 0 !important;
      padding: 0 !important;
      width: 100% !important;
      min-height: 100% !important;
      box-shadow: none !important;
    }
    .no-print, .print-btn-bar, .print-toolbar {
      display: none !important;
    }
  }

  /* ==================== STANDARDIZED HEADER ==================== */
  .std-header-container {
    width: 100%;
    margin-bottom: 12px;
  }
  .std-top-row {
    position: relative;
    min-height: 54px;
    margin-bottom: 6px;
    padding-left: 110px;
    padding-right: 110px;
  }
  .std-logo-left {
    position: absolute;
    left: 0;
    top: 0;
    width: 105px;
    display: flex;
    align-items: center;
  }
  .std-logo-img {
    max-height: 48px;
    max-width: 105px;
    object-fit: contain;
    display: block;
  }
  .std-company-center {
    text-align: center;
    width: 100%;
  }
  .std-company-name {
    font-size: 21px;
    font-weight: 800;
    letter-spacing: 1.5px;
    color: #B48218;
    text-transform: uppercase;
    margin-bottom: 3px;
    line-height: 1.2;
    font-family: 'Segoe UI', Tahoma, Arial, sans-serif;
  }
  .std-company-address {
    font-size: 10px;
    font-weight: 700;
    color: #222222;
    letter-spacing: 0.5px;
    text-transform: uppercase;
    line-height: 1.35;
  }
  .std-company-contacts {
    font-size: 9.5px;
    color: #444444;
    margin-top: 2px;
  }

  /* Gold Divider with Center Diamond */
  .std-diamond-divider {
    display: flex;
    align-items: center;
    justify-content: center;
    margin: 8px 0 10px 0;
    position: relative;
    width: 100%;
  }
  .std-divider-line {
    flex: 1;
    height: 1.5px;
    background-color: #B48218;
  }
  .std-diamond-symbol {
    color: #B48218;
    font-size: 11px;
    padding: 0 10px;
    line-height: 1;
    font-family: Arial, sans-serif;
  }

  /* Report Title & Printed Timestamp */
  .std-title-section {
    text-align: center;
    margin-bottom: 12px;
  }
  .std-report-title {
    font-size: 20px;
    font-weight: 800;
    letter-spacing: 1.2px;
    color: #4A3511;
    text-transform: uppercase;
    margin-bottom: 3px;
    line-height: 1.2;
    font-family: 'Segoe UI', Tahoma, Arial, sans-serif;
  }
  .std-printed-time {
    font-size: 11px;
    font-weight: 600;
    color: #222222;
  }

  /* ==================== STANDARDIZED FOOTER ==================== */
  .std-footer-container {
    width: 100%;
    margin-top: auto;
    padding-top: 14px;
    page-break-inside: avoid;
    break-inside: avoid;
  }
  .std-sign-grid {
    display: grid;
    grid-template-columns: 1fr 1fr;
    gap: 16px;
    margin-bottom: 12px;
  }
  .std-sign-card {
    border: 1px solid #C49A45;
    border-radius: 4px;
    background-color: #FFFFFF;
    padding: 8px 12px 10px 12px;
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    min-height: 82px;
  }
  .std-sign-title {
    font-size: 10.5px;
    font-weight: 800;
    color: #9E741D;
    text-transform: uppercase;
    letter-spacing: 0.5px;
    padding-bottom: 5px;
    border-bottom: 1px dotted #DDC89A;
    margin-bottom: 6px;
  }
  .std-sign-name-row {
    font-size: 10.5px;
    color: #222222;
    font-weight: 700;
    margin-bottom: 12px;
    white-space: nowrap;
    overflow: hidden;
    text-overflow: ellipsis;
  }
  .std-sign-bottom-row {
    display: flex;
    justify-content: space-between;
    align-items: flex-end;
    font-size: 10px;
    color: #222222;
  }
  .std-sign-line-box {
    display: flex;
    align-items: baseline;
    gap: 4px;
  }
  .std-sign-line {
    display: inline-block;
    border-bottom: 1px solid #555555;
    min-width: 110px;
    height: 1px;
  }
  .std-date-line {
    display: inline-block;
    border-bottom: 1px solid #555555;
    min-width: 80px;
    height: 1px;
  }

  .std-footer-gold-divider {
    height: 1.5px;
    background-color: #B48218;
    margin-bottom: 8px;
    width: 100%;
  }
  .std-system-generated {
    text-align: center;
    font-size: 10px;
    font-weight: 700;
    color: #555555;
    margin-bottom: 2px;
    letter-spacing: 0.5px;
  }
  .std-powered-by {
    text-align: center;
    font-size: 10px;
    font-weight: 700;
    color: #555555;
  }
`;

/**
 * Generates the standardized header HTML matching Reference Image 1 exactly.
 */
export const renderStandardReportHeader = (props: StandardReportHeaderProps): string => {
  const { company, reportTitle, printedDateTime } = props;
  const compName = (company.name || 'SMART').trim();
  const compAddr = (company.address || '').trim();
  const compContacts = [
    company.phone ? `TEL: ${company.phone.trim()}` : '',
    company.fax ? `FAX: ${company.fax.trim()}` : ''
  ].filter(Boolean).join(' &nbsp;|&nbsp; ');

  const logoSrc = company.logoUrl || UNIPRO_LOGO_DATA_URI;

  let printLine = '';
  if (printedDateTime) {
    printLine = printedDateTime.startsWith('Printed:') ? printedDateTime : `Printed: ${printedDateTime}`;
  } else {
    const now = new Date();
    const dd = String(now.getDate()).padStart(2, '0');
    const mm = String(now.getMonth() + 1).padStart(2, '0');
    const yyyy = now.getFullYear();
    let hours = now.getHours();
    const minutes = String(now.getMinutes()).padStart(2, '0');
    const ampm = hours >= 12 ? 'PM' : 'AM';
    hours = hours % 12;
    hours = hours ? hours : 12;
    printLine = `Printed: ${dd}/${mm}/${yyyy} ${hours}:${minutes} ${ampm}`;
  }

  return `
    <div class="std-header-container">
      <div class="std-top-row">
        <div class="std-logo-left">
          <img src="${logoSrc}" alt="Unipro" class="std-logo-img" />
        </div>
        <div class="std-company-center">
          <div class="std-company-name">${compName}</div>
          ${compAddr ? `<div class="std-company-address">${compAddr}</div>` : ''}
          ${compContacts ? `<div class="std-company-contacts">${compContacts}</div>` : ''}
        </div>
      </div>

      <div class="std-diamond-divider">
        <div class="std-divider-line"></div>
        <div class="std-diamond-symbol">&#9670;</div>
        <div class="std-divider-line"></div>
      </div>

      <div class="std-title-section">
        <div class="std-report-title">${reportTitle}</div>
        <div class="std-printed-time">${printLine}</div>
      </div>
    </div>
  `;
};

/**
 * Generates the standardized footer HTML matching Reference Image 1 exactly.
 */
export const renderStandardReportFooter = (props?: StandardReportFooterProps): string => {
  const staff = props?.staff;
  const staffLabel = staff?.staffLabel || 'PREPARED BY (STAFF)';
  const staffName = (staff?.staffName || 'UPS').trim();
  const receiverLabel = staff?.receiverLabel || 'SUPPLIER / AUTHORIZED RECEIVER';
  const receiverName = (staff?.receiverName || '-').trim();

  return `
    <div class="std-footer-container">
      <div class="std-sign-grid">
        <!-- Staff Box -->
        <div class="std-sign-card">
          <div class="std-sign-title">${staffLabel}</div>
          <div class="std-sign-name-row">Staff Name: ${staffName}</div>
          <div class="std-sign-bottom-row">
            <div class="std-sign-line-box">Signature: <span class="std-sign-line"></span></div>
            <div class="std-sign-line-box">Date: <span class="std-date-line"></span></div>
          </div>
        </div>

        <!-- Supplier / Receiver Box -->
        <div class="std-sign-card">
          <div class="std-sign-title">${receiverLabel}</div>
          <div class="std-sign-name-row">Receiver Name: ${receiverName}</div>
          <div class="std-sign-bottom-row">
            <div class="std-sign-line-box">Signature: <span class="std-sign-line"></span></div>
            <div class="std-sign-line-box">Date: <span class="std-date-line"></span></div>
          </div>
        </div>
      </div>

      <div class="std-footer-gold-divider"></div>
      <div class="std-system-generated">*** System Generated Report ***</div>
      <div class="std-powered-by">Powered by Unipro SG</div>
    </div>
  `;
};
