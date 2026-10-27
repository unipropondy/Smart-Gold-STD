require('dotenv').config({ path: require('path').resolve(__dirname, '.env') });
const { poolPromise } = require('./db');

async function test() {
    try {
        const pool = await poolPromise;
        const result = await pool.request().query("SELECT table_name FROM information_schema.tables WHERE table_name LIKE '%Point%' OR table_name LIKE '%Reward%' OR table_name LIKE '%Redeem%'");
        console.log("Matching tables:", result.recordset);
        
        // Also dump columns of any match
        for (let row of result.recordset) {
             const tname = row.table_name;
             const cols = await pool.request().query(`SELECT column_name FROM information_schema.columns WHERE table_name = '${tname}'`);
             console.log(`Columns for ${tname}:`, cols.recordset.map(r => r.column_name).join(', '));
        }
    } catch (e) {
        console.log("Error:", e.message);
    }
    process.exit(0);
}
test();
