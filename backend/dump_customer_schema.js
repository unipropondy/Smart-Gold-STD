const { getPool } = require("./db");
require("dotenv").config();

(async () => {
  try {
    const pool = await getPool(process.env.DB_NAME || "smartgold");
    const result = await pool.request().query("SELECT COLUMN_NAME, CHARACTER_MAXIMUM_LENGTH, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tblCustomer'");
    console.log(JSON.stringify(result.recordset, null, 2));
    process.exit(0);
  } catch(e) {
    console.error(e);
    process.exit(1);
  }
})();
