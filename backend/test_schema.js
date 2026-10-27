require('dotenv').config({ path: 'e:/Gold/smartgold/backend/.env' });
const { sql } = require('./db.js');

async function run() {
  try {
    const pool = await sql.connect();
    const result = await pool.request().query("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME='tblCompany'");
    console.log("COLUMNS:");
    console.log(result.recordset.map(x => x.COLUMN_NAME).filter(c => c.toLowerCase().includes('next') || c.toLowerCase().includes('repair') || c.toLowerCase().includes('order')).join('\n'));
    process.exit(0);
  } catch (err) {
    console.error(err);
    process.exit(1);
  }
}

run();
