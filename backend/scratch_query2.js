const { poolPromise } = require('./db');

async function check() {
    try {
        const pool = await poolPromise;
        
        const result = await pool.request().query(`
            SELECT TABLE_NAME
            FROM INFORMATION_SCHEMA.TABLES
            WHERE TABLE_TYPE = 'BASE TABLE'
              AND (TABLE_NAME LIKE '%SalesHeader%'
                   OR TABLE_NAME LIKE '%Invoice%'
                   OR TABLE_NAME LIKE '%PayMode%'
                   OR TABLE_NAME LIKE '%Flexi%'
                   OR TABLE_NAME LIKE '%eTRS%')
        `);
        console.log("Tables matching criteria:");
        console.dir(result.recordset, { depth: null });
        
        process.exit(0);
    } catch (err) {
        console.error(err);
        process.exit(1);
    }
}
check();
