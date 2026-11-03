const { getPool } = require("./backend/db");
require("dotenv").config({ path: "backend/.env" });

(async () => {
  try {
    const pool = await getPool("smartgold");

    // 1. Search tables for Type, KYC, Points, Customer
    const tables = await pool.request().query(`
      SELECT TABLE_NAME 
      FROM INFORMATION_SCHEMA.TABLES 
      WHERE TABLE_NAME LIKE '%Cust%' 
         OR TABLE_NAME LIKE '%KYC%' 
         OR TABLE_NAME LIKE '%Point%'
         OR TABLE_NAME LIKE '%Reward%'
      ORDER BY TABLE_NAME
    `);
    console.log("=== MATCHING TABLES ===");
    console.log(tables.recordset.map(r => r.TABLE_NAME));

    // 2. Check Customer Types
    try {
      const custTypes = await pool.request().query("SELECT DISTINCT CustomerType FROM tblCustomer WHERE CustomerType IS NOT NULL AND LTRIM(RTRIM(CustomerType)) <> ''");
      console.log("=== CUSTOMER TYPES IN tblCustomer ===");
      console.log(custTypes.recordset.map(r => r.CustomerType.trim()));
    } catch(e) { console.log("err custTypes", e.message); }

    // Check if there is a Customer Type Master table (e.g. tblCustomerType, tblCustType)
    for (const t of ["tblCustomerType", "tblCustType", "tblType", "tblCustomerTypes"]) {
      try {
        const res = await pool.request().query(`SELECT * FROM ${t}`);
        console.log(`=== FOUND ${t} ===`, res.recordset);
      } catch(e) {}
    }

    // 3. Check KYC tables and structure
    for (const t of ["tblKYC", "tblCustomerKYC", "tblCustKYC", "tblKYCDetails", "tblCustomerDoc"]) {
      try {
        const res = await pool.request().query(`SELECT TOP 2 * FROM ${t}`);
        console.log(`=== FOUND KYC TABLE ${t} ===`);
        const cols = await pool.request().query(`SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '${t}'`);
        console.log(cols.recordset);
      } catch(e) {}
    }

    // 4. Check Customer Code format / latest customer codes
    const latestCust = await pool.request().query(`SELECT TOP 10 CustomerCode, CustomerName, CustomerType, CreateDate FROM tblCustomer ORDER BY CreateDate DESC`);
    console.log("=== LATEST 10 CUSTOMERS ===");
    console.log(latestCust.recordset);

    const maxCode = await pool.request().query(`SELECT TOP 10 CustomerCode FROM tblCustomer WHERE CustomerCode NOT LIKE '%CASH%' ORDER BY LEN(CustomerCode) DESC, CustomerCode DESC`);
    console.log("=== TOP CUSTOMER CODES ===");
    console.log(maxCode.recordset);

    // 5. Check Sales Summary / Sales Detail / Points tables
    for (const t of ["tblSalesHeader", "tblSalesDetail", "tblCustomerPoints", "VwMemberPoints"]) {
      try {
        const cols = await pool.request().query(`SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '${t}'`);
        console.log(`=== COLS OF ${t} ===`, cols.recordset.map(c => c.COLUMN_NAME).join(", "));
      } catch(e) {
        console.log(`err cols ${t}:`, e.message);
      }
    }

    process.exit(0);
  } catch (err) {
    console.error(err);
    process.exit(1);
  }
})();

