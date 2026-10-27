const sql = require('mssql');
require('dotenv').config();

const config = {
  user: process.env.DB_USER,
  password: process.env.DB_PASSWORD,
  server: process.env.DB_SERVER,
  database: process.env.DB_DATABASE,
  options: { encrypt: false, trustServerCertificate: true }
};

async function test() {
  try {
    const pool = await sql.connect(config);
    const result = await pool.request().query("SELECT TOP 5 * FROM tblJobSheetDetail ORDER BY ModifyDate DESC");
    console.log(result.recordset);
    process.exit(0);
  } catch (err) {
    console.error(err);
    process.exit(1);
  }
}

test();
