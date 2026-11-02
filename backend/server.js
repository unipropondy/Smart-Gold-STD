require('dotenv').config({ path: require('path').join(__dirname, '.env') });
const express = require('express');
const cors = require('cors');
const fs = require('fs');
const path = require('path');
const { getPool } = require('./db');

const app = express();
const PORT = process.env.PORT || 3000;

app.use(cors());
app.use(express.json({ limit: '50mb' }));
app.use(express.urlencoded({ extended: true, limit: '50mb' }));

// Database injection middleware with dynamic healthy pool retrieval
app.use(async (req, res, next) => {
    try {
        req.pool = await getPool(process.env.DB_NAME || 'smartgold');
        next();
    } catch (err) {
        console.error("Database connection error in middleware:", err.message);
        res.status(500).json({ success: false, message: "Database connection failed", error: err.message });
    }
});

// Root & Health Check routes
app.get('/', (req, res) => {
    res.json({ status: 'ok', service: 'smartgold-api', timestamp: new Date().toISOString() });
});

app.get('/api', (req, res) => {
    res.json({ status: 'ok', message: 'SmartGold POS API Service Running' });
});

// Auto-load all routes
const routesPath = path.join(__dirname, 'routes');
if (fs.existsSync(routesPath)) {
    fs.readdirSync(routesPath).forEach(file => {
        if (file.endsWith('.js')) {
            const routeName = file.split('.')[0];
            const routeModule = require(path.join(routesPath, file));
            if (typeof routeModule === 'function') {
                if (routeName === 'auth') {
                    app.use('/api', routeModule); // special case for /api/login and /api/changepassword
                } else {
                    app.use(`/api/${routeName}`, routeModule);
                }
            } else {
                console.warn(`[Warning] Skipping route ${file} because it does not export a router function.`);
            }
        }
    });
}

// Prevent server crashes on unhandled connection drops/timeouts
process.on('uncaughtException', (err) => {
    console.error('🚨 Uncaught Exception:', err.message || err);
});

process.on('unhandledRejection', (reason, promise) => {
    console.error('🚨 Unhandled Rejection:', reason);
});

app.listen(PORT, () => {
    console.log(`Server running on port ${PORT}`);
});
