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
    const JobSheetNo = 'HQ00000066';
    const detailReq = new sql.Request()
      .input("JobSheetNo", sql.VarChar, JobSheetNo)
      .input("CatalogueCode", sql.VarChar, "CBE CH")
      .input("CategoryCode", sql.VarChar, "22K CH")
      .input("Description", sql.VarChar, "CHAIN")
      .input("CostPerGram", sql.Numeric(18, 2), 0)
      .input("Weight", sql.Numeric(18, 2), 0)
      .input("MC", sql.Numeric(18, 2), 0)
      .input("PCS", sql.Numeric(18, 2), 0)
      .input("GoldLoss", sql.Numeric(18, 2), 0)
      .input("TotalCost", sql.Numeric(18, 2), 0)
      .input("User", sql.VarChar, "ADMIN")
      .input("Date", sql.SmallDateTime, new Date());

    const result = await detailReq.query(`
      INSERT INTO tblJobSheetDetail (
        JobSheetNo, CatalogueCode, CategoryCode, Description, CostPerGram, Weight, MC, PCS, GoldLoss, TotalCost,
        CreateUser, CreateDate, ModifyUser, ModifyDate
      ) VALUES (
        @JobSheetNo, @CatalogueCode, @CategoryCode, @Description, @CostPerGram, @Weight, @MC, @PCS, @GoldLoss, @TotalCost,
        @User, @Date, @User, @Date
      )
    `);
    console.log("INSERT SUCCESS!", result);
    process.exit(0);
  } catch (err) {
    console.error("SQL ERROR:", err);
    process.exit(1);
  }
}

test();
