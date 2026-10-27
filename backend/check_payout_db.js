const { poolPromise } = require('./db');

async function check() {
  try {
    const pool = await poolPromise;
    console.log("Searching for tables containing 'Payout' or 'Paymode'...");
    const resTables = await pool.request().query(`
      SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES
      WHERE TABLE_NAME LIKE '%Payout%' OR TABLE_NAME LIKE '%Paymode%'
    `);
    console.log("Matching tables:", JSON.stringify(resTables.recordset, null, 2));

    for (let table of resTables.recordset) {
      const name = table.TABLE_NAME;
      console.log(`Checking columns for table: ${name}`);
      const columns = await pool.request().query(`
        SELECT COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH FROM INFORMATION_SCHEMA.COLUMNS
        WHERE TABLE_NAME = '${name}'
      `);
      console.log(JSON.stringify(columns.recordset, null, 2));
    }

    process.exit(0);
  } catch (err) {
    console.error(err);
    process.exit(1);
  }
}

check();
