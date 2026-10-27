const fs = require('fs');
const { poolPromise } = require('./db');

async function check() {
  try {
    const pool = await poolPromise;
    const result = await pool.request().query("SELECT table_name, column_name FROM information_schema.columns WHERE column_name LIKE '%Reward%' OR column_name LIKE '%Redeem%' OR column_name = 'InvoiceAmount'");
    fs.writeFileSync('schema_out.json', JSON.stringify(result.recordset, null, 2));
    process.exit(0);
  } catch(e) {
    fs.writeFileSync('schema_err.txt', e.toString());
    process.exit(1);
  }
}
check();
