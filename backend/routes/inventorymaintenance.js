const express = require('express');
const router = express.Router();
const { poolPromise, sql } = require('../db');

// Route to fetch inventory range details
router.get('/range', async (req, res) => {
    try {
        const { fromInventory, toInventory, supplierCode, purchaseBillNo } = req.query;

        if (!fromInventory) {
            return res.status(400).json({ success: false, message: 'From Inventory is required' });
        }
        const toInv = toInventory || fromInventory;

        const pool = await poolPromise;
        let query = `
            SELECT a.*, b.QtyOnHand, b.QtySold
            FROM tblInventory a
            LEFT JOIN tblInventoryStock b ON a.InventoryCode = b.InventoryCode AND a.LocationCode = b.LocationCode
            WHERE a.InventoryCode >= @FromInventory AND a.InventoryCode <= @ToInventory
            ORDER BY a.InventoryCode ASC
        `;

        const request = pool.request();
        request.input('FromInventory', sql.NVarChar, fromInventory);
        request.input('ToInventory', sql.NVarChar, toInv);

        if (supplierCode) {
            query += ` AND a.SupplierCode = @SupplierCode`;
            request.input('SupplierCode', sql.NVarChar, supplierCode);
        }

        if (purchaseBillNo) {
            query += ` AND a.PurchaseBillNo = @PurchaseBillNo`;
            request.input('PurchaseBillNo', sql.NVarChar, purchaseBillNo);
        }

        const result = await request.query(query);
        res.json({ success: true, data: result.recordset });
    } catch (err) {
        console.error('Error fetching inventory range:', err);
        res.status(500).json({ success: false, message: 'Internal Server Error' });
    }
});

// Route to fetch inventory LOV (similar to search but simpler)
router.get('/lov/inventory', async (req, res) => {
    try {
        const pool = await poolPromise;
        const result = await pool.request().query(`
            SELECT TOP 200 InventoryCode, Description, Weight, PurchaseBillNo, SupplierCode 
            FROM tblInventory 
            ORDER BY InventoryCode
        `);
        res.json({ success: true, data: result.recordset });
    } catch (err) {
        console.error('Error fetching inventory LOV:', err);
        res.status(500).json({ success: false, message: 'Internal Server Error' });
    }
});

// Route to fetch size LOV
router.get('/lov/size', async (req, res) => {
    try {
        const { categoryCode } = req.query;
        const pool = await poolPromise;
        let query = `SELECT SizeCode, SizeType, CategoryCode, Remarks FROM tblSize WHERE 1=1`;
        const request = pool.request();
        
        if (categoryCode) {
            query += ` AND CategoryCode = @CategoryCode`;
            request.input('CategoryCode', sql.NVarChar, categoryCode);
        }
        
        const result = await request.query(query);
        res.json({ success: true, data: result.recordset });
    } catch (err) {
        console.error('Error fetching size LOV:', err);
        res.status(500).json({ success: false, message: 'Internal Server Error' });
    }
});

// Route to update inventory
router.post('/save', async (req, res) => {
    try {
        const { inventoryList, commonUpdates, addlUpdates } = req.body;
        
        if (!inventoryList || inventoryList.length === 0) {
            return res.status(400).json({ success: false, message: 'No inventory items provided for update.' });
        }

        const pool = await poolPromise;
        const transaction = new sql.Transaction(pool);
        await transaction.begin();

        try {
            for (let i = 0; i < inventoryList.length; i++) {
                const inv = inventoryList[i];
                const request = new sql.Request(transaction);
                
                // Construct Update Query for tblInventory
                let updateQuery = `
                    UPDATE tblInventory SET 
                        Description = @Description,
                        DiamondDescription = @DiamondDescription,
                        PMC = @PMC,
                        SMC = @SMC,
                        PMCType = @PMCType,
                        SMCType = @SMCType,
                        TotalCost = @TotalCost,
                        GoldLoss = @GoldLoss,
                        SizeCode = @SizeCode,
                        SizeType = @SizeType
                    WHERE InventoryCode = @InventoryCode
                `;
                
                request.input('Description', sql.NVarChar, commonUpdates.Description);
                request.input('DiamondDescription', sql.NVarChar, commonUpdates.DiamondDescription || '');
                request.input('PMC', sql.Float, commonUpdates.PMC || 0);
                request.input('SMC', sql.Float, commonUpdates.SMC || 0);
                request.input('PMCType', sql.NVarChar, commonUpdates.PMCType || 'W');
                request.input('SMCType', sql.NVarChar, commonUpdates.SMCType || 'W');
                request.input('TotalCost', sql.Float, commonUpdates.TotalCost || 0);
                request.input('GoldLoss', sql.Float, commonUpdates.GoldLoss || 0);
                request.input('SizeCode', sql.NVarChar, commonUpdates.SizeCode || '');
                request.input('SizeType', sql.NVarChar, commonUpdates.SizeType || '');
                request.input('InventoryCode', sql.NVarChar, inv.InventoryCode);
                
                await request.query(updateQuery);
            }
            
            await transaction.commit();
            res.json({ success: true, message: 'Inventories saved successfully!' });
        } catch (txnErr) {
            await transaction.rollback();
            console.error('Transaction rollback in inventory save:', txnErr);
            res.status(500).json({ success: false, message: 'Error saving transaction.' });
        }
        
    } catch (err) {
        console.error('Error in /save:', err);
        res.status(500).json({ success: false, message: 'Internal Server Error' });
    }
});

module.exports = router;
