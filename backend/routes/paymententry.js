const express = require('express');
const router = express.Router();
const { sql } = require('../db');

// GET /api/paymententry/invoices
router.get('/invoices', async (req, res) => {
    try {
        const pool = req.pool;
        const result = await pool.request()
            .query(`
                SELECT TOP 100 
                    h.InvoiceNo, 
                    CONVERT(varchar, h.InvoiceDate, 103) as InvoiceDate, 
                    h.CustomerCode, 
                    h.TerminalCode, 
                    h.SalesManCode as SalesManName, 
                    h.NetTotal, 
                    h.PaidAmt, 
                    h.BalanceAmt, 
                    h.GST,
                    (
                        SELECT STUFF((
                            SELECT ' , ' + p.Paymode
                            FROM tblPayModeDetail p
                            WHERE p.InvoiceNo = h.InvoiceNo
                            FOR XML PATH(''), TYPE
                        ).value('.', 'NVARCHAR(MAX)'), 1, 3, '')
                    ) as InvoicePaymode
                FROM tblSalesHeader h
                ORDER BY h.InvoiceDate DESC
            `);
        res.json({ success: true, data: result.recordset });
    } catch (err) {
        console.error("Error fetching invoices:", err);
        res.status(500).json({ success: false, message: err.message });
    }
});

// GET /api/paymententry/etrs
router.get('/etrs', async (req, res) => {
    try {
        const pool = req.pool;
        const result = await pool.request()
            .query(`
                SELECT TOP 100 
                    InvoiceNo, 
                    CONVERT(varchar, InvoiceDate, 103) as InvoiceDate, 
                    Remarks, 
                    Gst, 
                    NetTotal 
                FROM tbleTRSStatement 
                ORDER BY InvoiceDate DESC
            `);
        res.json({ success: true, data: result.recordset });
    } catch (err) {
        console.error("Error fetching eTRS:", err);
        res.status(500).json({ success: false, message: err.message });
    }
});

// GET /api/paymententry/flexi
router.get('/flexi', async (req, res) => {
    try {
        const pool = req.pool;
        const result = await pool.request()
            .query(`
                SELECT TOP 100 
                    SubscriptionID, 
                    NricNo, 
                    FlexiType, 
                    InvoiceNo, 
                    PayMode, 
                    Amount, 
                    CreateUser 
                FROM tblPayModeDetailForFlexi 
                ORDER BY InvoiceNo DESC
            `);
        res.json({ success: true, data: result.recordset });
    } catch (err) {
        console.error("Error fetching flexi:", err);
        res.status(500).json({ success: false, message: err.message });
    }
});

// GET /api/paymententry/paymode/:invoiceNo
router.get('/paymode/:invoiceNo', async (req, res) => {
    try {
        const invoiceNo = req.params.invoiceNo;
        const pool = req.pool;
        const result = await pool.request()
            .input('InvoiceNo', invoiceNo)
            .query(`
                SELECT Paymode, Amount, SlipNo 
                FROM tblPayModeDetail 
                WHERE InvoiceNo = @InvoiceNo
            `);
        res.json({ success: true, data: result.recordset });
    } catch (err) {
        console.error("Error fetching paymodes:", err);
        res.status(500).json({ success: false, message: err.message });
    }
});

// GET /api/paymententry/paymodes-lov
router.get('/paymodes-lov', async (req, res) => {
    try {
        const pool = req.pool;
        const result = await pool.request()
            .query(`
                SELECT Paymode, Description 
                FROM tblPayModeMaster 
                ORDER BY Paymode ASC
            `);
        res.json({ success: true, data: result.recordset });
    } catch (err) {
        // Fallback to DISTINCT from tblPayModeDetail if master table not present
        try {
            const fallback = await req.pool.request()
                .query(`SELECT DISTINCT PayMode as Paymode FROM tblPayModeDetail WHERE PayMode IS NOT NULL AND PayMode <> '' ORDER BY PayMode ASC`);
            res.json({ success: true, data: fallback.recordset });
        } catch (fErr) {
            console.error("Error fetching paymodes LOV:", fErr);
            res.status(500).json({ success: false, message: fErr.message });
        }
    }
});

// GET /api/paymententry/invoice/:invoiceNo
router.get('/invoice/:invoiceNo', async (req, res) => {
    try {
        const invoiceNo = req.params.invoiceNo;
        const pool = req.pool;
        const result = await pool.request()
            .input('InvoiceNo', invoiceNo)
            .query(`
                SELECT 
                    h.InvoiceNo, 
                    CONVERT(varchar, h.InvoiceDate, 103) as InvoiceDate, 
                    h.CustomerCode, 
                    h.TerminalCode, 
                    h.SalesManCode as SalesManName, 
                    h.NetTotal, 
                    h.PaidAmt, 
                    h.BalanceAmt, 
                    h.GST,
                    (
                        SELECT STUFF((
                            SELECT ' , ' + p.Paymode
                            FROM tblPayModeDetail p
                            WHERE p.InvoiceNo = h.InvoiceNo
                            FOR XML PATH(''), TYPE
                        ).value('.', 'NVARCHAR(MAX)'), 1, 3, '')
                    ) as InvoicePaymode
                FROM tblSalesHeader h
                WHERE h.InvoiceNo LIKE '%' + @InvoiceNo + '%'
                ORDER BY h.InvoiceDate DESC
            `);
        res.json({ success: true, data: result.recordset });
    } catch (err) {
        console.error("Error fetching single invoice:", err);
        res.status(500).json({ success: false, message: err.message });
    }
});

// POST /api/paymententry/save
router.post('/save', async (req, res) => {
    const { type, invoiceNo, paymodes, actualPaidAmount, gst, userId } = req.body;
    const pool = req.pool;
    const transaction = new sql.Transaction(pool);
    let isTransactionActive = false;

    try {
        if (!invoiceNo) {
            return res.status(400).json({ success: false, message: "InvoiceNo is required" });
        }

        await transaction.begin();
        isTransactionActive = true;

        const sysUser = userId || 'ADMIN';
        const curDate = new Date();

        if (type === 'Invoice') {
            // 0. Fetch existing Invoice Date and LocationCode if available
            let invDate = new Date();
            let locCode = 'HQ';
            try {
                const headerCheck = await new sql.Request(transaction)
                    .input('InvoiceNo', sql.VarChar, String(invoiceNo).trim())
                    .query(`SELECT TOP 1 InvoiceDate, LocationCode FROM tblSalesHeader WHERE InvoiceNo = @InvoiceNo`);
                if (headerCheck.recordset && headerCheck.recordset.length > 0) {
                    if (headerCheck.recordset[0].InvoiceDate) {
                        invDate = new Date(headerCheck.recordset[0].InvoiceDate);
                    }
                    if (headerCheck.recordset[0].LocationCode) {
                        locCode = String(headerCheck.recordset[0].LocationCode).trim();
                    }
                }
            } catch (hErr) {
                console.warn("Could not query existing header meta:", hErr.message);
            }

            // 1. Update tblSalesHeader Modify details & amounts
            const updateHeaderReq = new sql.Request(transaction);
            await updateHeaderReq
                .input('InvoiceNo', sql.VarChar, String(invoiceNo).trim())
                .input('UserId', sql.VarChar, sysUser)
                .input('PaidAmt', sql.Numeric(18, 4), parseFloat(actualPaidAmount) || 0)
                .input('GST', sql.Numeric(18, 4), parseFloat(gst) || 0)
                .query(`
                    UPDATE tblSalesHeader 
                    SET ModifyUser = @UserId, ModifyDate = GETDATE(),
                        PaidAmt = @PaidAmt, GST = @GST 
                    WHERE InvoiceNo = @InvoiceNo
                `);

            // 2. Remove old paymodes to replace with updated paymodes
            const deletePmReq = new sql.Request(transaction);
            await deletePmReq
                .input('InvoiceNo', sql.VarChar, String(invoiceNo).trim())
                .query(`DELETE FROM tblPayModeDetail WHERE InvoiceNo = @InvoiceNo`);

            // 3. Insert updated paymodes
            if (paymodes && paymodes.length > 0) {
                for (const pm of paymodes) {
                    const rawAmt = pm.NewAmount !== undefined ? pm.NewAmount : pm.Amount;
                    const amt = parseFloat(String(rawAmt)) || 0;
                    const slip = String((pm.NewSlipNo !== undefined ? pm.NewSlipNo : pm.SlipNo) || '').trim();
                    const modeName = String(pm.Paymode || pm.PayMode || '').trim();

                    if (amt !== 0 && modeName) {
                        const insertPmReq = new sql.Request(transaction);
                        await insertPmReq
                            .input('InvoiceNo', sql.VarChar, String(invoiceNo).trim())
                            .input('InvoiceDate', sql.SmallDateTime, invDate)
                            .input('PayMode', sql.VarChar, modeName)
                            .input('Amount', sql.Numeric(18, 4), amt)
                            .input('SlipNo', sql.VarChar, slip)
                            .input('SalesType', sql.VarChar, 'CASH')
                            .input('CreateUser', sql.VarChar, sysUser)
                            .input('LocationCode', sql.VarChar, locCode)
                            .query(`
                                INSERT INTO tblPayModeDetail 
                                (InvoiceNo, InvoiceDate, PayMode, Amount, Commision, SlipNo, SalesType, CreateUser, CreateDate, ModifyUser, ModifyDate, LocationCode, IsPaid) 
                                VALUES (@InvoiceNo, @InvoiceDate, @PayMode, @Amount, 0, @SlipNo, @SalesType, @CreateUser, GETDATE(), @CreateUser, GETDATE(), @LocationCode, 1)
                            `);
                    }
                }
            }
        } else if (type === 'Flexi') {
            const flexiReq = new sql.Request(transaction);
            await flexiReq
                .input('InvoiceNo', sql.VarChar, invoiceNo)
                .input('UserId', sql.VarChar, sysUser)
                .query(`
                    UPDATE tblPayModeDetailForFlexi
                    SET CreateUser = @UserId
                    WHERE InvoiceNo = @InvoiceNo
                `);
        }

        await transaction.commit();
        isTransactionActive = false;
        res.json({ success: true, message: "Payment saved successfully" });
    } catch (err) {
        console.error("Error saving payment:", err);
        if (isTransactionActive) {
            try {
                await transaction.rollback();
            } catch (rbErr) {
                console.error("Rollback error:", rbErr);
            }
        }
        res.status(500).json({ success: false, message: err.message || "Failed to save payment" });
    }
});

// POST /api/paymententry/cashdrawer
router.post('/cashdrawer', async (req, res) => {
    try {
        const pool = req.pool;
        const { terminalId, terminalCode, userId } = req.body || {};
        try {
            await pool.request()
                .input('TerminalID', terminalId || '1')
                .input('TerminalCode', terminalCode || 'HQ')
                .input('UserId', userId || 'SYSTEM')
                .query(`
                    INSERT INTO tblCashDrawerLog (TerminalID, TerminalCode, CounterCode, UserId, ActionDate, Remarks)
                    VALUES (@TerminalID, @TerminalCode, @TerminalCode, @UserId, GETDATE(), 'Cash Drawer')
                `);
        } catch (logErr) {
            // tblCashDrawerLog might not exist or have different structure; keep non-fatal
            console.warn("Cash drawer log table skipped or errored:", logErr.message);
        }
        res.json({ success: true, message: "Cash drawer command executed successfully." });
    } catch (err) {
        res.status(500).json({ success: false, message: err.message });
    }
});

module.exports = router;
