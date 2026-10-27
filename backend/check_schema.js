const { poolPromise } = require("./db");
const fs = require("fs");
const path = require("path");

async function checkSchema() {
  try {
    const pool = await poolPromise;
    
    const tables = await pool.request().query(`
      SELECT TABLE_NAME 
      FROM INFORMATION_SCHEMA.TABLES 
      WHERE TABLE_NAME LIKE '%SalesOrder%'
    `);

    const checkHeader = await pool.request().query(`
      SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tblSalesOrderHeader'
    `);

    const checkDetail = await pool.request().query(`
      SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tblSalesOrderDetail'
    `);
    
    const output = JSON.stringify({
      tables: tables.recordset,
      header: checkHeader.recordset,
      detail: checkDetail.recordset
    }, null, 2);

    fs.writeFileSync(path.join(__dirname, "check_schema_result.json"), output);
    process.exit(0);
  } catch (err) {
    fs.writeFileSync(path.join(__dirname, "check_schema_error.txt"), err.stack);
    process.exit(1);
  }
}
checkSchema();
