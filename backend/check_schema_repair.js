const sql = require('mssql');
require('dotenv').config({ path: 'e:/Gold/smartgold/backend/.env' });

const config = {
    user: process.env.DB_USER,
    password: process.env.DB_PASSWORD,
    server: process.env.DB_SERVER,
    database: process.env.DB_NAME,
    options: {
        encrypt: process.env.DB_ENCRYPT === 'true',
        trustServerCertificate: process.env.DB_TRUST_CERTIFICATE === 'true',
    }
};

async function checkSchema() {
    try {
        let pool = await sql.connect(config);
        const tables = [
            'tblInventoryRepairHeader', 
            'tblInventoryRepairDetail', 
            'tblInventoryRepairHeaderCancel',
            'tblInventoryRepairDetailCancel',
            'tblSalesMan'
        ];
        
        for (let table of tables) {
            console.log(`\n--- Schema for ${table} ---`);
            const result = await pool.request().query(`
                SELECT COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH
                FROM INFORMATION_SCHEMA.COLUMNS
                WHERE TABLE_NAME = '${table}'
            `);
            console.table(result.recordset);
        }
        process.exit(0);
    } catch (err) {
        console.error(err);
        process.exit(1);
    }
}

checkSchema();
