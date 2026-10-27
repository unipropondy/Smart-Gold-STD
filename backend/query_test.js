require('dotenv').config({ path: __dirname + '/.env' });
const { poolPromise, sql } = require('./db');
const fs = require('fs');

async function test() {
  try {
    console.log("Connecting...");
    const pool = await poolPromise;
    console.log("Connected.");
    const result1 = await pool.request().query(`
      SELECT COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH 
      FROM INFORMATION_SCHEMA.COLUMNS 
      WHERE TABLE_NAME = 'tblTradeInSalesHeader'
    `);
    const result2 = await pool.request().query(`
      SELECT COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH 
      FROM INFORMATION_SCHEMA.COLUMNS 
      WHERE TABLE_NAME = 'tblTradeinSalesDetail'
    `);
    console.log("tblTradeInSalesHeader Columns:", result1.recordset);
    console.log("tblTradeinSalesDetail Columns:", result2.recordset);
  } catch(e) {
    console.error("error:", e.message);
  }
  process.exit(0);
}
test();
