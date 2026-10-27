require('dotenv').config();
const express = require('express');
const cors = require('cors');
const fs = require('fs');
const path = require('path');
const { poolPromise } = require('./db');

const app = express();
const PORT = process.env.PORT || 3000;

app.use(cors());
app.use(express.json());

// Database injection middleware
app.use(async (req, res, next) => {
    try {
        req.pool = await poolPromise;
        next();
    } catch (err) {
        console.error("Database connection error in middleware:", err);
        res.status(500).json({ success: false, message: "Database connection failed" });
    }
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

app.listen(PORT, () => {
    console.log(`Backend Server running on port ${PORT}`);
});
