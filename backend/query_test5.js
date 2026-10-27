const fs = require('fs');
const { poolPromise } = require('./db');

async function check() {
  try {
    const pool = await poolPromise;
    const result = await pool.request().query("SELECT TOP 1 * FROM tblInventory");
    console.log(Object.keys(result.recordset[0]).join(", "));
    process.exit(0);
  } catch(e) {
    console.error(e);
    process.exit(1);
  }
}
check();
