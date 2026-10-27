const { sql, poolPromise } = require('./db');

async function checkTables() {
    try {
        const pool = await poolPromise;
        const result = await pool.request().query(`
            SELECT TABLE_NAME 
            FROM INFORMATION_SCHEMA.TABLES 
            WHERE TABLE_NAME LIKE '%TradeInTransfer%'
        `);
        console.log(result.recordset);
    } catch (err) {
        console.error(err);
    } finally {
        process.exit(0);
    }
}

checkTables();
