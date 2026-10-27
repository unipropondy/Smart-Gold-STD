const { sql, poolPromise } = require("./db");

async function checkSchema() {
  const pool = await poolPromise;
  const tables = await pool.request().query(`
    SELECT TABLE_NAME 
    FROM INFORMATION_SCHEMA.TABLES 
    WHERE TABLE_NAME LIKE '%WholeSale%' OR TABLE_NAME LIKE '%SalesHeader%' OR TABLE_NAME LIKE '%SalesDetail%'
  `);
  console.log("Tables:", tables.recordset);
  
  const cols = await pool.request().query(`
    SELECT COLUMN_NAME, TABLE_NAME
    FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'tblSalesHeader' OR TABLE_NAME = 'tblWholeSaleHeader'
  `);
  console.log("Columns:", cols.recordset);
  process.exit(0);
}
checkSchema();
