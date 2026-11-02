/**
 * Developer: durairaj
 */

const express = require('express');
const router = express.Router();
const { sql, poolPromise } = require('../db');

// LOV for selecting inventory codes if needed
router.get('/lov/inventory', async (req, res) => {
    try {
        const pool = req.pool || (await poolPromise);
        const result = await pool.request().query(`
            SELECT TOP 200 
                InventoryCode, 
                Description, 
                Weight, 
                SupplierCode 
            FROM tblInventory 
            ORDER BY InventoryCode ASC
        `);
        res.json({ success: true, data: result.recordset });
    } catch (error) {
        console.error("Error fetching inventory LOV:", error);
        res.status(500).json({ success: false, message: "Server error", error: error.message });
    }
});

// GET /api/inventorymovement/details/:code
// Matches frmInventoryMovement.vb query on VwInventorymovement
router.get('/details/:code', async (req, res) => {
    try {
        const pool = req.pool || (await poolPromise);
        const { code } = req.params;

        if (!code || !code.trim()) {
            return res.status(400).json({ success: false, message: "Please enter the Inventory Code" });
        }

        const trimmedCode = code.trim();

        // Query VwInventorymovement exactly as in frmInventoryMovement.vb
        try {
            const query = `
                SELECT 
                    LocationCode, 
                    InventoryCode, 
                    Description, 
                    Weight, 
                    Size, 
                    CostPerGram, 
                    TotalCost, 
                    Status, 
                    RefNumber, 
                    RefDate 
                FROM VwInventorymovement 
                WHERE InventoryCode = @code
                ORDER BY modifyDate DESC
            `;
            const result = await pool.request()
                .input('code', sql.VarChar, trimmedCode)
                .query(query);

            return res.json({ success: true, data: result.recordset });
        } catch (vwErr) {
            console.warn("VwInventorymovement query failed, trying fallback:", vwErr.message);

            // Graceful fallback from tblInventory if view doesn't exist or is invalid
            const fallbackQuery = `
                SELECT 
                    LocationCode, 
                    InventoryCode, 
                    Description, 
                    Weight, 
                    Size, 
                    CostPerGram, 
                    TotalCost, 
                    Status, 
                    PurchaseBillNo AS RefNumber, 
                    CreateDate AS RefDate 
                FROM tblInventory 
                WHERE InventoryCode = @code
            `;
            const fbResult = await pool.request()
                .input('code', sql.VarChar, trimmedCode)
                .query(fallbackQuery);

            return res.json({ success: true, data: fbResult.recordset });
        }
    } catch (error) {
        console.error("Error fetching inventory movement details:", error);
        res.status(500).json({ success: false, message: "Server error", error: error.message });
    }
});

module.exports = router;
