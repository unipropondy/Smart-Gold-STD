const fs = require('fs');
const { poolPromise } = require('./db');

async function check() {
  try {
    const pool = await poolPromise;
    const result = await pool.request().query("SELECT table_name FROM information_schema.tables WHERE table_name LIKE '%Sales%' OR table_name LIKE 'tblInvoice%'");
    console.log(JSON.stringify(result.recordset, null, 2));
    process.exit(0);
  } catch(e) {
    console.error(e);
    process.exit(1);
  }
}
check();
