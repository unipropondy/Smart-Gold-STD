/**
 * Developer: durairaj
 */

const express = require('express');
const router = express.Router();
const { sql } = require('../db');

// GET Company Name matching frmBarcode.vb Load event
router.get('/company', async (req, res) => {
    try {
        const pool = req.pool;
        let companyName = "MILLION";
        let shortCode = "";
        
        try {
            const compRes = await pool.request().query("SELECT TOP 1 CompanyID, Name, ShortCode FROM tblCompany ORDER BY CompanyID ASC");
            if (compRes.recordset.length > 0) {
                const comp = compRes.recordset[0];
                shortCode = (comp.ShortCode || '').toUpperCase().trim();
                const dbName = (comp.Name || '').toUpperCase().trim();

                if (shortCode === "ES") {
                    companyName = "SRI PINANG";
                } else if (shortCode === "ML") {
                    companyName = "MILLION";
                } else if (shortCode === "AS") {
                    companyName = "APPOLLO";
                } else if (shortCode === "GMT") {
                    companyName = "GMT JEWELLERS PTE LTD";
                } else if (shortCode === "HN" && dbName.includes("HANIFFA")) {
                    companyName = "HANIFFA TEXTILES PTE LTD";
                } else if (shortCode === "HN" && dbName.includes("JEWEL PALACE")) {
                    companyName = "JEWEL PALACE PTE LTD";
                } else if (shortCode === "SSJ") {
                    companyName = "SS JEWELLERS";
                } else if (shortCode === "RJ") {
                    companyName = "ROYAL JEWELS";
                } else if (comp.Name) {
                    companyName = comp.Name.trim();
                }
            }
        } catch (dbErr) {
            console.warn("Could not read tblCompany for barcode:", dbErr.message);
        }

        res.json({ success: true, company: companyName, shortCode });
    } catch (error) {
        console.error("Error fetching company:", error);
        res.status(500).json({ success: false, message: "Server error", company: "MILLION" });
    }
});

router.get('/lov/inventory', async (req, res) => {
    try {
        const pool = req.pool;
        let query = `
            SELECT TOP 200 
                InventoryCode, 
                Description, 
                SupplierCode, 
                Weight 
            FROM tblInventory 
            ORDER BY InventoryCode ASC
        `;
        const result = await pool.request().query(query);
        res.json({ success: true, data: result.recordset });
    } catch (error) {
        console.error("Error fetching inventory LOV:", error);
        res.status(500).json({ success: false, message: "Server error", error: error.message });
    }
});

router.get('/lov/category', async (req, res) => {
    try {
        const pool = req.pool;
        const query = `
            SELECT TOP 200 
                CategoryCode, 
                CategoryName, 
                ClassCode, 
                Remarks 
            FROM tblCategory 
            ORDER BY CategoryCode ASC
        `;
        const result = await pool.request().query(query);
        res.json({ success: true, data: result.recordset });
    } catch (error) {
        console.error("Error fetching category LOV:", error);
        res.status(500).json({ success: false, message: "Server error", error: error.message });
    }
});

router.get('/lov/tray', async (req, res) => {
    try {
        const pool = req.pool;
        const query = `
            SELECT TOP 200 
                TrayId, 
                TrayName, 
                IsCustomerTray 
            FROM tblTraymaster 
            ORDER BY TrayName ASC
        `;
        const result = await pool.request().query(query);
        res.json({ success: true, data: result.recordset });
    } catch (error) {
        console.error("Error fetching tray LOV:", error);
        res.status(500).json({ success: false, message: "Server error", error: error.message });
    }
});

router.get('/inventory/:code', async (req, res) => {
    try {
        const pool = req.pool;
        const code = (req.params.code || '').trim();

        if (!code) {
            return res.status(400).json({ success: false, message: "InventoryCode is required" });
        }

        const invResult = await pool.request()
            .input('code', sql.VarChar, code)
            .query(`SELECT * FROM tblInventory WHERE InventoryCode = @code`);

        if (invResult.recordset.length > 0) {
            const row = invResult.recordset[0];
            const qty = parseFloat(row.QtyOnHand != null ? row.QtyOnHand : 1);

            if (qty <= 0) {
                return res.json({
                    success: false,
                    noStock: true,
                    message: "No Stock for this InventoryCode"
                });
            }

            let displaySMC = parseFloat(row.SMC || row.SMc || 0);
            let displayPMC = parseFloat(row.PMC || 0);
            const pcs = parseFloat(row.PCS || row.Pcs || 1);

            if (row.SMCType === 'P' && pcs > 0) {
                displaySMC = (displaySMC / pcs);
                displayPMC = (displayPMC / pcs);
            }

            let classDetails = {};
            if (row.ClassCode) {
                try {
                    const clsRes = await pool.request()
                        .input('ClassCode', sql.VarChar, row.ClassCode.trim())
                        .query(`SELECT ClassName, SalesType, PurchaseType FROM tblClass WHERE ClassCode = @ClassCode`);
                    if (clsRes.recordset.length > 0) {
                        classDetails = clsRes.recordset[0];
                    }
                } catch (e) {}
            }

            let addlDesc = {};
            try {
                const addlResult = await pool.request()
                    .input('code', sql.VarChar, code)
                    .query(`SELECT * FROM tblInventoryAddlDescription WHERE InventoryCode = @code`);
                if (addlResult.recordset.length > 0) {
                    addlDesc = addlResult.recordset[0];
                }
            } catch (e) {}

            return res.json({
                success: true,
                isTradeIn: false,
                data: {
                    ...row,
                    ...classDetails,
                    CalculatedSMC: displaySMC.toFixed(2),
                    CalculatedPMC: displayPMC.toFixed(2),
                    AddlDescription: addlDesc
                }
            });
        }

        try {
            const tradeResult = await pool.request()
                .input('code', sql.VarChar, code)
                .query(`
                    SELECT * 
                    FROM tblTradeIn 
                    WHERE InvoiceNo = @code OR TradeInNo = @code
                `);

            if (tradeResult.recordset.length > 0) {
                const tRow = tradeResult.recordset[0];
                return res.json({
                    success: true,
                    isTradeIn: true,
                    data: {
                        InventoryCode: tRow.InvoiceNo || tRow.TradeInNo,
                        Description: "Trade in Items",
                        Weight: tRow.TotalWeight || 0,
                        GoldLoss: tRow.TotalGoldLoss || 0,
                        SMC: tRow.TotalMC || 0,
                        CalculatedSMC: (parseFloat(tRow.TotalMC) || 0).toFixed(2),
                        CreateDate: tRow.CreateDate
                    }
                });
            }
        } catch (tErr) {}

        return res.json({ success: false, message: "InventoryCode Not Found" });
    } catch (error) {
        console.error("Error fetching inventory details:", error);
        res.status(500).json({ success: false, message: "Server error", error: error.message });
    }
});

router.post('/print', async (req, res) => {
    const pool = req.pool;
    const transaction = new sql.Transaction(pool);

    try {
        const {
            inventoryCode,
            newTrayId,
            oldTrayId,
            categoryCode,
            oldCategoryCode,
            noOfLabels,
            company
        } = req.body;

        if (!inventoryCode || !inventoryCode.trim()) {
            return res.status(400).json({ success: false, message: "Please Enter InventoryCode" });
        }

        const trimmedCode = inventoryCode.trim();
        const trimmedNewTray = (newTrayId || '').trim();
        const trimmedCategory = (categoryCode || '').trim();

        await transaction.begin();

        if (trimmedNewTray && trimmedNewTray !== (oldTrayId || '').trim()) {
            await new sql.Request(transaction)
                .input('InventoryCode', sql.VarChar, trimmedCode)
                .input('NewTrayId', sql.VarChar, trimmedNewTray)
                .query(`
                    UPDATE tblInventory 
                    SET TrayId = @NewTrayId, ModifyDate = GETDATE()
                    WHERE InventoryCode = @InventoryCode;
                `);

            try {
                await new sql.Request(transaction)
                    .input('InventoryCode', sql.VarChar, trimmedCode)
                    .input('NewTrayId', sql.VarChar, trimmedNewTray)
                    .query(`
                        UPDATE tblInventoryStock 
                        SET TrayId = @NewTrayId, ModifyDate = GETDATE()
                        WHERE InventoryCode = @InventoryCode;
                    `);
            } catch (e) {}
        }

        if (trimmedCategory && trimmedCategory !== (oldCategoryCode || '').trim()) {
            await new sql.Request(transaction)
                .input('InventoryCode', sql.VarChar, trimmedCode)
                .input('CategoryCode', sql.VarChar, trimmedCategory)
                .query(`
                    UPDATE tblInventory 
                    SET CategoryCode = @CategoryCode, ModifyDate = GETDATE()
                    WHERE InventoryCode = @InventoryCode;
                `);
        }

        await transaction.commit();

        // Generate EPL / ZPL / Generic printer script commands matching clsBarcode / frmBarcode.vb
        const qtyLabels = parseInt(noOfLabels, 10) || 1;
        const eplCommands = [
            "I8,1,001",
            "q200",
            "S5",
            "H20",
            "ZB",
            "R0,0",
            "N",
            `B68,179,0,1,1,3,38,N,"${trimmedCode}"`,
            `A17,93,0,2,1,1,N,"MC:${req.body.smc || ''}"`,
            `A17,72,0,2,1,1,N,"${trimmedNewTray || req.body.oldTrayId || ''}"`,
            `A17,53,0,2,1,1,N,"${req.body.classCode || ''}"`,
            `A17,33,0,2,1,1,N,"${(req.body.description || '').substring(0, 16)}"`,
            `A17,14,0,2,1,1,N,"Gm:${parseFloat(req.body.weight || 0).toFixed(2)}"`,
            `A25,149,0,3,1,1,N,"${company || 'MILLION'}"`,
            `A57,225,0,3,1,1,N,"${trimmedCode}"`,
            `W1,${qtyLabels}`
        ].join('\r\n');

        res.json({
            success: true,
            message: "Barcode generated & printed successfully!",
            printerCommands: eplCommands,
            labelData: {
                inventoryCode: trimmedCode,
                company: company || 'MILLION',
                description: req.body.description || '',
                weight: parseFloat(req.body.weight || 0).toFixed(2),
                goldLoss: parseFloat(req.body.goldLoss || 0).toFixed(2),
                smc: req.body.smc || '',
                classCode: req.body.classCode || '',
                trayId: trimmedNewTray || req.body.oldTrayId || '',
                categoryCode: trimmedCategory || req.body.oldCategoryCode || '',
                noOfLabels: qtyLabels
            }
        });
    } catch (error) {
        try {
            await transaction.rollback();
        } catch (rbErr) {}
        console.error("Print barcode error:", error);
        res.status(500).json({ success: false, message: "Error in updation.", error: error.message });
    }
});

module.exports = router;
