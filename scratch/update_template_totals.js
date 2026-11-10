const fs = require('fs');

let tmplPath = 'backend/utils/reportHtmlTemplate.js';
let tmpl = fs.readFileSync(tmplPath, 'utf8');

// Replace the totals logic in reportHtmlTemplate.js
const targetStart = `              // Helper: Determine if a column is strictly non-numeric / descriptive where totals MUST NOT appear`;
const targetEnd = `              // Do NOT display a TOTAL row if there are no meaningful numeric totals!`;

// Find where the entire totals block is
const startIdx = tmpl.indexOf(targetStart);
if (startIdx === -1) {
  console.error("Could not find targetStart in template!");
  process.exit(1);
}

// Find enclosing function
const prevSnippet = `\${
            (() => {
              if (!totals || typeof totals !== "object" || Object.keys(totals).length === 0) return "";`;

const nextSnippet = `return \`
                <tr class="totals-row">
                  \${(columns || [])
                    .map((c, cIdx) => {
                      const alignStyle = c.align ? \`text-align: \${c.align} !important;\` : (cIdx === 0 ? 'text-align: left !important;' : '');
                      const val = colValues[cIdx];
                      return \`<td style="\${alignStyle}">\${val}</td>\`;
                    })
                    .join("")}
                </tr>
              \`;
            })()
          }`;

// Let's create the robust, unified totals computation helper:
const updatedBlock = `\${
            (() => {
              if (!totals || typeof totals !== "object" || Object.keys(totals).length === 0) return "";

              // Helper: Strict check - determine if a column is non-additive/descriptive where TOTAL must NEVER appear
              const isNonSummableCol = (colKey) => {
                if (!colKey) return true;
                const k = colKey.toLowerCase().replace(/[\\s_\\-\\.]/g, "");
                return (
                  k.includes("cust") || k.includes("name") || k.includes("code") ||
                  k.includes("desc") || k.includes("type") || k.includes("user") ||
                  k.includes("date") || k.includes("time") || k.includes("invno") ||
                  k.includes("billno") || k.includes("docno") || k.includes("orderno") ||
                  k.includes("voucher") || k.includes("category") || k.includes("class") ||
                  k.includes("address") || k.includes("phone") || k.includes("mobile") ||
                  k.includes("email") || k.includes("nric") || k.includes("passport") ||
                  k.includes("status") || k.includes("remark") || k.includes("tray") ||
                  k.includes("location") || k.includes("religion") || k.includes("nationality") ||
                  k.includes("serial") || k.includes("item") || k.includes("id")
                );
              };

              // Helper: Check if a column key represents an additive numerical measure
              const isAdditiveMetric = (colKey) => {
                if (!colKey || isNonSummableCol(colKey)) return false;
                const k = colKey.toLowerCase().replace(/[\\s_\\-\\.]/g, "");
                return (
                  k.includes("point") || k.includes("pts") || k.includes("qty") ||
                  k.includes("pcs") || k.includes("wt") || k.includes("weight") ||
                  k.includes("cost") || k.includes("amount") || k.includes("total") ||
                  k.includes("price") || k.includes("sales") || k.includes("smc") ||
                  k.includes("pmc") || k.includes("gst") || k.includes("tax") ||
                  k.includes("discount") || k.includes("disc") || k.includes("sold") ||
                  k.includes("balance") || k.includes("stock") || k.includes("count")
                );
              };

              // Helper: Get numerical sum value for an additive column from totals object or data fallback
              const getColTotalVal = (c) => {
                const colKey = c.key || "";
                if (isNonSummableCol(colKey) || !isAdditiveMetric(colKey)) return "";

                const k = colKey.toLowerCase().replace(/[\\s_\\-\\.]/g, "");

                // Points & Adjustment Points
                if (k === "adjustmentpoints" || k === "adjpts" || k === "adjpoints") {
                  const pts = totals.totalAdjustmentPoints !== undefined ? totals.totalAdjustmentPoints : totals.adjustmentPoints;
                  if (pts !== undefined) return Number(pts).toLocaleString();
                }
                if (k === "previouspoints" || k === "prevpts" || k === "prevpoints") {
                  const pts = totals.totalPreviousPoints !== undefined ? totals.totalPreviousPoints : totals.previousPoints;
                  if (pts !== undefined) return Number(pts).toLocaleString();
                }
                if (k === "totalpoints") {
                  const pts = totals.totalPoints !== undefined ? totals.totalPoints : totals.TotalPoints;
                  if (pts !== undefined) return Number(pts).toLocaleString();
                }
                if (k === "newtotalpoints") {
                  const pts = totals.newTotalPoints !== undefined ? totals.newTotalPoints : totals.NewTotalPoints;
                  if (pts !== undefined) return Number(pts).toLocaleString();
                }
                if (k === "rewardpoints" || k === "totalrewardpoints") {
                  const pts = totals.totalRewardPoints !== undefined ? totals.totalRewardPoints : totals.rewardPoints;
                  if (pts !== undefined) return Number(pts).toLocaleString();
                }
                if (k === "redeemedpoints" || k === "totalredeemedpoints" || k === "redeempoints") {
                  const pts = totals.totalRedeemedPoints !== undefined ? totals.totalRedeemedPoints : totals.redeemedPoints;
                  if (pts !== undefined) return Number(pts).toLocaleString();
                }
                if (k === "balancepoints") {
                  const pts = totals.totalBalancePoints !== undefined ? totals.totalBalancePoints : totals.balancePoints;
                  if (pts !== undefined) return Number(pts).toLocaleString();
                }

                // Pieces & Quantities
                if (k === "pcs" || k === "totalpcs" || k === "qty" || k === "totalqty" || k === "sold" || k === "qtysold") {
                  const q = totals.totalPcs !== undefined ? totals.totalPcs : totals.totalQty !== undefined ? totals.totalQty : totals.qty !== undefined ? totals.qty : totals.pcs;
                  if (q !== undefined) return Number(q).toLocaleString();
                }

                // Weights
                if (k.includes("weight") || k.includes("wt")) {
                  const wt = totals.totalWeight !== undefined ? totals.totalWeight : totals.totalNetWeight !== undefined ? totals.totalNetWeight : totals.weight;
                  if (wt !== undefined) return Number(wt).toFixed(3);
                }

                // Costs & Amounts
                if (k === "cost" || k === "totalcost") {
                  if (totals.totalCost !== undefined) return "$ " + Number(totals.totalCost).toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 });
                }
                if (k === "smc" || k === "totalsmc") {
                  const smc = totals.totalSMC !== undefined ? totals.totalSMC : totals.totalCost !== undefined ? totals.totalCost : totals.smc;
                  if (smc !== undefined) return "$ " + Number(smc).toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 });
                }
                if (k === "pmc" || k === "totalpmc") {
                  if (totals.totalPmc !== undefined) return "$ " + Number(totals.totalPmc).toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 });
                }
                if (k === "amount" || k === "totalamount" || k === "nettotal" || k === "totalsales" || k === "netamount" || k === "salesamount" || k === "netsales") {
                  const amt = totals.totalAmount !== undefined ? totals.totalAmount : totals.totalSales !== undefined ? totals.totalSales : totals.netSales !== undefined ? totals.netSales : totals.salesAmount;
                  if (amt !== undefined) return "$ " + Number(amt).toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 });
                }
                if (k === "gst" || k === "totaltax" || k === "tax") {
                  const g = totals.totalGST !== undefined ? totals.totalGST : totals.totalTax !== undefined ? totals.totalTax : totals.gst;
                  if (g !== undefined) return "$ " + Number(g).toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 });
                }
                if (k === "discount" || k === "totaldiscount" || k === "disc") {
                  const d = totals.totalDiscount !== undefined ? totals.totalDiscount : totals.discount;
                  if (d !== undefined) return "$ " + Number(d).toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 });
                }

                // Direct key match in totals object (only if numerical)
                if (totals[colKey] !== undefined) {
                  const num = Number(totals[colKey]);
                  if (!isNaN(num)) {
                    return num.toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 });
                  }
                }

                // If not found in totals object, compute from data rows for additive numeric columns
                if (Array.isArray(data) && data.length > 0) {
                  const hasValues = data.some(row => row[colKey] !== null && row[colKey] !== undefined && row[colKey] !== "");
                  if (hasValues) {
                    const sum = data.reduce((acc, row) => acc + (Number(row[colKey]) || 0), 0);
                    if (!isNaN(sum)) {
                      if (k.includes("amount") || k.includes("cost") || k.includes("price") || k.includes("sales")) {
                        return "$ " + sum.toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 });
                      }
                      if (k.includes("weight") || k.includes("wt")) {
                        return sum.toFixed(3);
                      }
                      return sum.toLocaleString();
                    }
                  }
                }

                return "";
              };

              // Identify columns with valid additive totals
              const numericTotals = (columns || []).map(c => getColTotalVal(c));
              const firstNumericIdx = numericTotals.findIndex(v => v !== "" && v !== null && v !== undefined);

              // Do NOT display a TOTAL row if there are no meaningful numeric totals!
              if (firstNumericIdx === -1) return "";

              // Place "TOTAL:" label directly in the cell immediately preceding the first numeric total column,
              // or in column 0 if the first column itself is numeric.
              const labelIdx = firstNumericIdx > 0 ? firstNumericIdx - 1 : 0;

              return \`
                <tr class="totals-row">
                  \${(columns || [])
                    .map((c, cIdx) => {
                      const alignStyle = c.align ? \`text-align: \${c.align} !important;\` : '';
                      if (cIdx === labelIdx && numericTotals[cIdx] === "") {
                        return \`<td style="text-align: right !important; font-weight: 700;">TOTAL:</td>\`;
                      }
                      const val = numericTotals[cIdx];
                      return \`<td style="\${alignStyle}">\${val}</td>\`;
                    })
                    .join("")}
                </tr>
              \`;
            })()
          }`;

// Check if we can find the block to replace
const cleanNormalize = (s) => s.replace(/\\r\\n/g, '\\n');
const targetBlockRegex = /\\$\\{\\s*\\(\\(\\)\\s*=>\\s*\\{[\\s\\S]*?if\\s*\\(!totals[\\s\\S]*?\\}\\)\\(\\)\\s*\\}/;

if (targetBlockRegex.test(tmpl)) {
  tmpl = tmpl.replace(targetBlockRegex, updatedBlock);
  fs.writeFileSync(tmplPath, tmpl, 'utf8');
  console.log('reportHtmlTemplate.js totals block updated successfully!');
} else {
  console.warn('Could not match targetBlockRegex in reportHtmlTemplate.js');
}

