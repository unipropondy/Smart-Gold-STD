const { poolPromise } = require("./db");
const fs = require("fs");

async function run() {
  try {
    const pool = await poolPromise;
    const result = await pool.request().query(`
      SELECT TABLE_NAME 
      FROM INFORMATION_SCHEMA.TABLES 
      WHERE TABLE_NAME LIKE '%SalesOrder%' OR TABLE_NAME LIKE '%ReOrder%' OR TABLE_NAME LIKE '%Company%'
    `);
    
    const checkHeader = await pool.request().query(`
      SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tblSalesOrderHeader'
    `);

    const checkDetail = await pool.request().query(`
      SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tblSalesOrderDetail'
    `);
    
    const output = JSON.stringify({
      tables: result.recordset,
      header: checkHeader.recordset,
      detail: checkDetail.recordset
    }, null, 2);

    fs.writeFileSync("e:/Gold/smartgold/backend/routes/schema_output.txt", output);
    console.log("Success! File written to schema_output.txt");
    process.exit(0);
  } catch (err) {
    fs.writeFileSync("e:/Gold/smartgold/backend/routes/schema_error.txt", err.stack);
    console.error(err);
    process.exit(1);
  }
}
run();
