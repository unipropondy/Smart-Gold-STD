const fs = require('fs');
try {
    const sql = require('mssql');
    const path = require('path');
    require('dotenv').config({ path: path.join(__dirname, '.env') });
    const config = {
        user: process.env.DB_USER,
        password: process.env.DB_PASS,
        server: process.env.DB_SERVER,
        port: Number(process.env.DB_PORT),
        database: process.env.DB_NAME || 'UNIPRO',
        options: { encrypt: false, trustServerCertificate: true }
    };

    sql.connect(config).then(pool => {
        return pool.request().query("SELECT table_name FROM information_schema.tables WHERE table_name LIKE '%Point%' OR table_name LIKE '%Reward%'");
    }).then(result => {
        fs.writeFileSync(path.join(__dirname, 'output.txt'), JSON.stringify(result.recordset, null, 2));
        process.exit(0);
    }).catch(err => {
        fs.writeFileSync(path.join(__dirname, 'error.txt'), err.message);
        process.exit(1);
    });
} catch(e) {
    fs.writeFileSync('outerror.txt', e.message);
}
