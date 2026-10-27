const express = require('express');
const router = express.Router();
const mssql = require('mssql');

// GET /api/barcode/lov/inventory
router.get('/lov/inventory', async (req, res) => {
    try {
        const pool = req.pool;
        const query = `
            SELECT TOP 100 InventoryCode, Description, SupplierCode, Weight 
            FROM tblInventory 
            ORDER BY InventoryCode
        `;
        const result = await pool.request().query(query);
        res.json({ success: true, data: result.recordset });
    } catch (error) {
        console.error("Error fetching inventory LOV:", error);
        res.status(500).json({ success: false, message: "Server error" });
    }
});

// GET /api/barcode/lov/category
router.get('/lov/category', async (req, res) => {
    try {
        const pool = req.pool;
        const query = `
            SELECT TOP 100 CategoryCode, CategoryName, ClassCode, Remarks 
            FROM tblCategory 
            ORDER BY CategoryCode
        `;
        const result = await pool.request().query(query);
        res.json({ success: true, data: result.recordset });
    } catch (error) {
        console.error("Error fetching category LOV:", error);
        res.status(500).json({ success: false, message: "Server error" });
    }
});

// GET /api/barcode/lov/tray
router.get('/lov/tray', async (req, res) => {
    try {
        const pool = req.pool;
        const query = `
            SELECT TOP 100 TrayId, TrayName, IsCustomerTray 
            FROM tblTraymaster 
            ORDER BY TrayId
        `;
        const result = await pool.request().query(query);
        res.json({ success: true, data: result.recordset });
    } catch (error) {
        console.error("Error fetching tray LOV:", error);
        res.status(500).json({ success: false, message: "Server error" });
    }
});

// GET /api/barcode/inventory/:code
router.get('/inventory/:code', async (req, res) => {
    try {
        const pool = req.pool;
        const { code } = req.params;
        const query = `
            SELECT *
            FROM tblInventory 
            WHERE InventoryCode = @code
        `;
        const result = await pool.request()
            .input('code', mssql.VarChar, code)
            .query(query);

        if (result.recordset.length > 0) {
            res.json({ success: true, data: result.recordset[0] });
        } else {
            res.json({ success: false, message: "InventoryCode Not Found" });
        }
    } catch (error) {
        console.error("Error fetching inventory details:", error);
        res.status(500).json({ success: false, message: "Server error" });
    }
});

module.exports = router;
