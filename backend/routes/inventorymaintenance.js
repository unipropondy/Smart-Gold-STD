/**
 * Developer: durairaj
 */

const express = require('express');
const router = express.Router();
const { poolPromise, sql } = require('../db');

// Route to fetch inventory range details matching GetInventoryRange in frmInventoryMnt.vb
router.get('/range', async (req, res) => {
    try {
        const { fromInventory, toInventory, supplierCode, purchaseBillNo } = req.query;

        if (!fromInventory || !fromInventory.trim()) {
            return res.status(400).json({ success: false, message: 'Invalid Inventory' });
        }
        const fromInv = fromInventory.trim();
        const toInv = (toInventory && toInventory.trim()) ? toInventory.trim() : fromInv;

        const pool = req.pool || (await poolPromise);

        let query = `
            SELECT 
                a.*,
                b.QtyOnHand,
                b.QtySold,
                ad.Description1, ad.Description2, ad.Description3, ad.Description4,
                ad.Cost1, ad.Cost2, ad.Cost3, ad.Cost4,
                ad.SellingPrice1, ad.SellingPrice2, ad.SellingPrice3, ad.SellingPrice4
            FROM tblInventory a
            LEFT JOIN tblInventoryStock b ON a.InventoryCode = b.InventoryCode
            LEFT JOIN tblInventoryAddlDescription ad ON a.InventoryCode = ad.InventoryCode
            WHERE a.InventoryCode >= @FromInventory AND a.InventoryCode <= @ToInventory
        `;

        const request = pool.request();
        request.input('FromInventory', sql.VarChar, fromInv);
        request.input('ToInventory', sql.VarChar, toInv);

        if (supplierCode && supplierCode.trim()) {
            query += ` AND a.SupplierCode = @SupplierCode`;
            request.input('SupplierCode', sql.VarChar, supplierCode.trim());
        }

        if (purchaseBillNo && purchaseBillNo.trim()) {
            query += ` AND a.PurchaseBillNo = @PurchaseBillNo`;
            request.input('PurchaseBillNo', sql.VarChar, purchaseBillNo.trim());
        }

        query += ` ORDER BY a.InventoryCode ASC`;

        let result;
        try {
            result = await request.query(query);
        } catch (joinErr) {
            let fbQuery = `
                SELECT 
                    a.*,
                    ISNULL(a.QtyOnHand, 1) as QtyOnHand,
                    0 as QtySold
                FROM tblInventory a
                WHERE a.InventoryCode >= @FromInventory AND a.InventoryCode <= @ToInventory
            `;
            const fbReq = pool.request();
            fbReq.input('FromInventory', sql.VarChar, fromInv);
            fbReq.input('ToInventory', sql.VarChar, toInv);
            if (supplierCode && supplierCode.trim()) {
                fbQuery += ` AND a.SupplierCode = @SupplierCode`;
                fbReq.input('SupplierCode', sql.VarChar, supplierCode.trim());
            }
            if (purchaseBillNo && purchaseBillNo.trim()) {
                fbQuery += ` AND a.PurchaseBillNo = @PurchaseBillNo`;
                fbReq.input('PurchaseBillNo', sql.VarChar, purchaseBillNo.trim());
            }
            fbQuery += ` ORDER BY a.InventoryCode ASC`;
            result = await fbReq.query(fbQuery);
        }

        res.json({ success: true, data: result.recordset });
    } catch (err) {
        console.error('Error fetching inventory range:', err);
        res.status(500).json({ success: false, message: 'Server error', error: err.message });
    }
});

// Route to fetch inventory LOV matching btnInventorySearchF / btnInventorySearchT
router.get('/lov/inventory', async (req, res) => {
    try {
        const pool = req.pool || (await poolPromise);
        const result = await pool.request().query(`
            SELECT TOP 200 
                InventoryCode, 
                Description, 
                Weight, 
                PurchaseBillNo, 
                SupplierCode 
            FROM tblInventory 
            ORDER BY InventoryCode ASC
        `);
        res.json({ success: true, data: result.recordset });
    } catch (err) {
        console.error('Error fetching inventory LOV:', err);
        res.status(500).json({ success: false, message: 'Server error', error: err.message });
    }
});

// Route to fetch size LOV matching btnSizeCode_Click (GetSizewithCode)
router.get('/lov/size', async (req, res) => {
    try {
        const { categoryCode } = req.query;
        const pool = req.pool || (await poolPromise);
        let query = `SELECT SizeCode, SizeType, CategoryCode, Remarks FROM tblSize WHERE 1=1`;
        const request = pool.request();
        
        if (categoryCode && categoryCode.trim()) {
            query += ` AND CategoryCode = @CategoryCode`;
            request.input('CategoryCode', sql.VarChar, categoryCode.trim());
        }
        
        query += ` ORDER BY SizeCode ASC`;
        const result = await request.query(query);
        res.json({ success: true, data: result.recordset });
    } catch (err) {
        console.error('Error fetching size LOV:', err);
        res.status(500).json({ success: false, message: 'Server error', error: err.message });
    }
});

// Save updates matching InventoryMnt in frmInventoryMnt.vb
router.post('/save', async (req, res) => {
    const pool = req.pool || (await poolPromise);

    try {
        const { inventoryList, commonUpdates, addlUpdates, userId } = req.body;
        
        if (!inventoryList || inventoryList.length === 0) {
            return res.status(400).json({ success: false, message: 'No items matching' });
        }

        const sysUser = userId || 'SYSTEM';

        // Dynamically inspect column names of tblInventory to prevent crashes on missing or differently named columns
        let invCols = [];
        try {
            const colRes = await pool.request().query(`
                SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tblInventory'
            `);
            invCols = colRes.recordset.map(c => c.COLUMN_NAME.toLowerCase());
        } catch (colErr) {
            console.warn("Could not query tblInventory schema:", colErr.message);
        }

        const hasCol = (name) => invCols.length === 0 || invCols.includes(name.toLowerCase());

        const transaction = new sql.Transaction(pool);
        await transaction.begin();

        try {
            for (let i = 0; i < inventoryList.length; i++) {
                const inv = inventoryList[i];
                const request = new sql.Request(transaction);

                const setClauses = [];
                const addField = (colName, sqlType, value) => {
                    if (hasCol(colName)) {
                        setClauses.push(`${colName} = @${colName}`);
                        request.input(colName, sqlType, value);
                    }
                };

                addField('Description', sql.VarChar, commonUpdates.Description || '');
                addField('DiamondDescription', sql.VarChar, commonUpdates.DiamondDescription || '');
                addField('PMC', sql.Numeric(18, 2), parseFloat(commonUpdates.PMC) || 0);
                
                // Support either SMC or SMc column
                if (hasCol('SMC')) {
                    addField('SMC', sql.Numeric(18, 2), parseFloat(commonUpdates.SMC) || 0);
                } else if (hasCol('SMc')) {
                    addField('SMc', sql.Numeric(18, 2), parseFloat(commonUpdates.SMC) || 0);
                }

                addField('PMCType', sql.VarChar, commonUpdates.PMCType || 'W');
                addField('SMCType', sql.VarChar, commonUpdates.SMCType || 'W');
                addField('TotalCost', sql.Numeric(18, 2), parseFloat(commonUpdates.TotalCost) || 0);
                addField('GoldLoss', sql.Numeric(18, 3), parseFloat(commonUpdates.GoldLoss) || 0);

                if (commonUpdates.SizeCode) {
                    if (hasCol('SizeCode')) {
                        addField('SizeCode', sql.VarChar, String(commonUpdates.SizeCode));
                    }
                    if (hasCol('Size')) {
                        addField('Size', sql.Numeric(18, 2), parseFloat(commonUpdates.SizeCode) || 0);
                    }
                }
                if (commonUpdates.SizeType && hasCol('SizeType')) {
                    addField('SizeType', sql.VarChar, commonUpdates.SizeType);
                }

                if (hasCol('ModifyDate')) {
                    setClauses.push('ModifyDate = GETDATE()');
                }
                if (hasCol('ModifyUser')) {
                    addField('ModifyUser', sql.VarChar, sysUser);
                }

                request.input('InventoryCode', sql.VarChar, inv.InventoryCode.trim());

                if (setClauses.length > 0) {
                    const updateQuery = `
                        UPDATE tblInventory 
                        SET ${setClauses.join(', ')}
                        WHERE InventoryCode = @InventoryCode
                    `;
                    await request.query(updateQuery);
                }

                // Sync tblInventoryStock if available
                try {
                    const stockReq = new sql.Request(transaction);
                    stockReq.input('InventoryCode', sql.VarChar, inv.InventoryCode.trim());
                    stockReq.input('Description', sql.VarChar, commonUpdates.Description || '');
                    stockReq.input('DiamondDescription', sql.VarChar, commonUpdates.DiamondDescription || '');
                    stockReq.input('TotalCost', sql.Numeric(18, 2), parseFloat(commonUpdates.TotalCost) || 0);
                    await stockReq.query(`
                        UPDATE tblInventoryStock SET 
                            Description = @Description,
                            DiamondDescription = @DiamondDescription,
                            TotalCost = @TotalCost,
                            ModifyDate = GETDATE()
                        WHERE InventoryCode = @InventoryCode
                    `);
                } catch (stkErr) {}

                // Update/Upsert tblInventoryAddlDescription if additional details are provided
                if (addlUpdates && (addlUpdates.Description1 || addlUpdates.Cost1 || addlUpdates.SellingPrice1)) {
                    try {
                        const addlReq = new sql.Request(transaction);
                        addlReq.input('InventoryCode', sql.VarChar, inv.InventoryCode.trim());
                        addlReq.input('Description1', sql.VarChar, addlUpdates.Description1 || '');
                        addlReq.input('Description2', sql.VarChar, addlUpdates.Description2 || '');
                        addlReq.input('Description3', sql.VarChar, addlUpdates.Description3 || '');
                        addlReq.input('Description4', sql.VarChar, addlUpdates.Description4 || '');
                        addlReq.input('Cost1', sql.Numeric(18, 2), parseFloat(addlUpdates.Cost1) || 0);
                        addlReq.input('Cost2', sql.Numeric(18, 2), parseFloat(addlUpdates.Cost2) || 0);
                        addlReq.input('Cost3', sql.Numeric(18, 2), parseFloat(addlUpdates.Cost3) || 0);
                        addlReq.input('Cost4', sql.Numeric(18, 2), parseFloat(addlUpdates.Cost4) || 0);
                        addlReq.input('SellingPrice1', sql.Numeric(18, 2), parseFloat(addlUpdates.SellingPrice1) || 0);
                        addlReq.input('SellingPrice2', sql.Numeric(18, 2), parseFloat(addlUpdates.SellingPrice2) || 0);
                        addlReq.input('SellingPrice3', sql.Numeric(18, 2), parseFloat(addlUpdates.SellingPrice3) || 0);
                        addlReq.input('SellingPrice4', sql.Numeric(18, 2), parseFloat(addlUpdates.SellingPrice4) || 0);

                        await addlReq.query(`
                            IF EXISTS (SELECT 1 FROM tblInventoryAddlDescription WHERE InventoryCode = @InventoryCode)
                            BEGIN
                                UPDATE tblInventoryAddlDescription SET
                                    Description1 = @Description1, Description2 = @Description2, Description3 = @Description3, Description4 = @Description4,
                                    Cost1 = @Cost1, Cost2 = @Cost2, Cost3 = @Cost3, Cost4 = @Cost4,
                                    SellingPrice1 = @SellingPrice1, SellingPrice2 = @SellingPrice2, SellingPrice3 = @SellingPrice3, SellingPrice4 = @SellingPrice4
                                WHERE InventoryCode = @InventoryCode
                            END
                            ELSE
                            BEGIN
                                INSERT INTO tblInventoryAddlDescription (
                                    InventoryCode, Description1, Description2, Description3, Description4,
                                    Cost1, Cost2, Cost3, Cost4, SellingPrice1, SellingPrice2, SellingPrice3, SellingPrice4
                                ) VALUES (
                                    @InventoryCode, @Description1, @Description2, @Description3, @Description4,
                                    @Cost1, @Cost2, @Cost3, @Cost4, @SellingPrice1, @SellingPrice2, @SellingPrice3, @SellingPrice4
                                )
                            END
                        `);
                    } catch (aErr) {}
                }
            }

            await transaction.commit();
            return res.json({ success: true, message: 'Inventories saved successfully!' });
        } catch (innerErr) {
            try {
                await transaction.rollback();
            } catch (rbErr) {}
            throw innerErr;
        }
    } catch (txnErr) {
        console.error('Error in inventory save:', txnErr);
        res.status(500).json({ success: false, message: 'Transaction not saved', error: txnErr.message });
    }
});

module.exports = router;
