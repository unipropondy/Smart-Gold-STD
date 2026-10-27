const { poolPromise } = require('./db');
poolPromise.then(pool => pool.request().query('SELECT TOP 1 * FROM tblCustomer').then(res => { console.log(Object.keys(res.recordset[0] || {})); process.exit(0); })).catch(console.error);
