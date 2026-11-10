const fs = require('fs');
const path = require('path');

const filePath = path.resolve(__dirname, '../app/stocktakereports.tsx');
let content = fs.readFileSync(filePath, 'utf8');

const oldBlockStart = '      if (currentAction === \'Preview\') {';
const oldBlockEnd = '      } else if (currentAction === \'PDF\') {';

const newBlock = `      if (currentAction === 'Preview') {
        await printHtmlReport(
          {
            endpoint: 'stocktake',
            title: reportTitle,
            subtitle: subtitleText,
            columns,
            data: reportData,
            totals: reportTotals,
            fromDate,
            toDate,
          },
          false
        );
        showSuccess('Report preview opened.');
      } else if (currentAction === 'Print') {
        await printHtmlReport(
          {
            endpoint: 'stocktake',
            title: reportTitle,
            subtitle: subtitleText,
            columns,
            data: reportData,
            totals: reportTotals,
            fromDate,
            toDate,
          },
          true
        );
        showSuccess('Print dialog opened.');
      } else if (currentAction === 'PDF') {`;

let sIdx = content.indexOf(oldBlockStart);
let eIdx = content.indexOf(oldBlockEnd);

if (sIdx !== -1 && eIdx !== -1) {
  content = content.substring(0, sIdx) + newBlock + content.substring(eIdx + oldBlockEnd.length);
  fs.writeFileSync(filePath, content, 'utf8');
  console.log('stocktakereports.tsx standardized with Master Reports printHtmlReport!');
} else {
  console.error('Could not find boundaries in stocktakereports.tsx: sIdx=' + sIdx + ', eIdx=' + eIdx);
}

