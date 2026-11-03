const { getPool } = require("./backend/db");
require("dotenv").config({ path: "backend/.env" });

(async () => {
  try {
    const pool = await getPool("smartgold");
    const tbls = await pool.request().query(`
      SELECT TABLE_NAME 
      FROM INFORMATION_SCHEMA.TABLES 
      WHERE TABLE_NAME LIKE '%Auto%' 
         OR TABLE_NAME LIKE '%Prefix%' 
         OR TABLE_NAME LIKE '%Num%' 
         OR TABLE_NAME LIKE '%Serial%'
         OR TABLE_NAME LIKE '%Sequence%'
      ORDER BY TABLE_NAME
    `);
    console.log("=== AUTO NUMBER / PREFIX TABLES ===");
    console.log(tbls.recordset.map(r => r.TABLE_NAME));

    for (const t of tbls.recordset.map(r => r.TABLE_NAME)) {
      try {
        const rows = await pool.request().query(`SELECT TOP 5 * FROM ${t}`);
        console.log(`=== DATA FOR ${t} ===`, rows.recordset);
      } catch(e) {}
    }

    process.exit(0);
  } catch(e) {
    console.error(e);
    process.exit(1);
  }
})();

