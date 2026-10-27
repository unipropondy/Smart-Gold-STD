const { poolPromise } = require('./db');

async function check() {
    try {
        const pool = await poolPromise;
        const result = await pool.request().query(`
            SELECT COLUMN_NAME 
            FROM INFORMATION_SCHEMA.COLUMNS 
            WHERE TABLE_NAME = 'tblSalesHeader'
        `);
        console.dir(result.recordset.map(r => r.COLUMN_NAME), { maxArrayLength: null });
        process.exit(0);
    } catch (err) {
        console.error(err);
        process.exit(1);
    }
}
check();
