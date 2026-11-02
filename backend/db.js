const sql = require("mssql");
const path = require("path");
require("dotenv").config({ path: path.join(__dirname, ".env") });

// ✅ POOL CACHE
const pools = new Map();

// Helper to configure connection
const createPoolConfig = (dbname) => ({
  user: process.env.DB_USER,
  password: process.env.DB_PASS,
  server: process.env.DB_SERVER,
  port: Number(process.env.DB_PORT),
  database: dbname,
  connectionTimeout: 60000,
  requestTimeout: 120000,
  pool: {
    max: 10,
    min: 0,
    idleTimeoutMillis: 30000
  },
  options: {
    encrypt: false,
    trustServerCertificate: true
  }
});

// ✅ DYNAMIC CONNECTION FUNCTION WITH AUTO-RECONNECT
const getPool = async (dbname) => {
  const targetDb = dbname || process.env.DB_NAME || 'smartgold';
  let pool = pools.get(targetDb);

  if (pool && pool.connected && !pool.closed) {
    return pool;
  }

  // If pool exists but closed/connecting/erroring, close and remove from cache
  if (pool) {
    try {
      await pool.close();
    } catch (_) {}
    pools.delete(targetDb);
  }

  try {
    const config = createPoolConfig(targetDb);
    pool = await new sql.ConnectionPool(config).connect();

    // Attach error handler to prevent crashing process and handle disconnects
    pool.on('error', (err) => {
      console.error(`⚠️ SQL Pool Error on [${targetDb}]:`, err.message);
      try {
        pool.close();
      } catch (_) {}
      pools.delete(targetDb);
    });

    console.log(`✅ SQL Connected : ${targetDb}`);
    pools.set(targetDb, pool);
    return pool;

  } catch (err) {
    console.error(`❌ DB Connection Failed [${targetDb}]:`, err.message);
    pools.delete(targetDb);
    throw err;
  }
};

// Default pool accessor getter
const getValidPool = async () => {
  return await getPool(process.env.DB_NAME || 'smartgold');
};

const poolPromise = getValidPool();

module.exports = {
  sql,
  getPool,
  getValidPool,
  poolPromise
};
