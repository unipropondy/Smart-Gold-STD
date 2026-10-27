const express = require('express');
const router = express.Router();
const mssql = require('mssql');

// GET /api/inventorymovement/details/:code
router.get('/details/:code', async (req, res) => {
    try {
        const pool = req.pool;
        const { code } = req.params;
        const query = `
            SELECT LocationCode, InventoryCode, Description, Weight, Size, CostPerGram, 
                   TotalCost, Status, RefNumber, RefDate 
            FROM VwInventorymovement 
            WHERE InventoryCode = @code
            ORDER BY modifyDate DESC
        `;
        const result = await pool.request()
            .input('code', mssql.VarChar, code)
            .query(query);

        res.json({ success: true, data: result.recordset });
    } catch (error) {
        console.error("Error fetching inventory movement details:", error);
        res.status(500).json({ success: false, message: "Server error" });
    }
});

module.exports = router;
