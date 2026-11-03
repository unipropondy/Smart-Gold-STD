/** Developer: durairaj */
const express = require('express');
const router = express.Router();
const { sql, poolPromise } = require('../db');

// Helper to acquire database connection pool reliably
const getPool = async (req) => {
    if (req && req.pool) return req.pool;
    if (poolPromise) return await poolPromise;
    throw new Error('Database pool is not available.');
};

// GET /api/paymententry/invoices
router.get('/invoices', async (req, res) => {
    try {
        const pool = await getPool(req);
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
        res.json({ success: true, data: result.recordset || [] });
    } catch (err) {
        console.error("Error fetching invoices:", err);
        res.status(500).json({ success: false, message: err.message });
    }
});

// GET /api/paymententry/etrs
router.get('/etrs', async (req, res) => {
    try {
        const pool = await getPool(req);
        const result = await pool.request()
            .query(`
                SELECT TOP 100 
                    BillNo AS InvoiceNo, 
                    CONVERT(varchar, BillDate, 103) as InvoiceDate, 
                    Remarks, 
                    Gst, 
                    NetAmount AS NetTotal 
                FROM tbleTRSStatement 
                ORDER BY BillDate DESC
            `);
        res.json({ success: true, data: result.recordset || [] });
    } catch (err) {
        console.error("Error fetching eTRS:", err);
        res.status(500).json({ success: false, message: err.message });
    }
});

// GET /api/paymententry/etrs/:invoiceNo
router.get('/etrs/:invoiceNo', async (req, res) => {
    try {
        const invoiceNo = (req.params.invoiceNo || '').trim();
        const pool = await getPool(req);
        const result = await pool.request()
            .input('InvoiceNo', sql.VarChar, invoiceNo)
            .query(`
                SELECT TOP 1 
                    RTRIM(BillNo) AS BillNo,
                    BillDate,
                    DAY(BillDate) AS BillDay,
                    MONTH(BillDate) AS BillMonth,
                    YEAR(BillDate) AS BillYear,
                    RTRIM(SalesmanCode) AS SalesmanCode,
                    RTRIM(PassPortNo) AS PassPortNo,
                    Gst,
                    NetAmount,
                    RTRIM(DocId) AS DocId,
                    eTRSIssueDate,
                    RTRIM(eTRSIssueTime) AS eTRSIssueTime,
                    RTRIM(Remarks) AS Remarks,
                    CustomerImage
                FROM tbleTRSStatement
                WHERE RTRIM(BillNo) = @InvoiceNo
            `);
        
        if (result.recordset && result.recordset.length > 0) {
            res.json({ success: true, data: result.recordset[0] });
        } else {
            res.json({ success: false, data: null, message: "No eTRS statement found for this invoice." });
        }
    } catch (err) {
        console.error("Error fetching single eTRS:", err);
        res.status(500).json({ success: false, message: err.message });
    }
});

// GET /api/paymententry/flexi
router.get('/flexi', async (req, res) => {
    try {
        const pool = await getPool(req);
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
        res.json({ success: true, data: result.recordset || [] });
    } catch (err) {
        console.error("Error fetching flexi:", err);
        res.status(500).json({ success: false, message: err.message });
    }
});

// GET /api/paymententry/paymode/:invoiceNo
router.get('/paymode/:invoiceNo', async (req, res) => {
    try {
        const invoiceNo = (req.params.invoiceNo || '').trim();
        const pool = await getPool(req);
        const result = await pool.request()
            .input('InvoiceNo', sql.VarChar, invoiceNo)
            .query(`
                SELECT Paymode, Amount, SlipNo 
                FROM tblPayModeDetail 
                WHERE InvoiceNo = @InvoiceNo
            `);
        res.json({ success: true, data: result.recordset || [] });
    } catch (err) {
        console.error("Error fetching paymodes:", err);
        res.status(500).json({ success: false, message: err.message });
    }
});

// GET /api/paymententry/paymodes-lov
router.get('/paymodes-lov', async (req, res) => {
    try {
        const pool = await getPool(req);
        const result = await pool.request()
            .query(`
                SELECT Paymode, Description 
                FROM tblPayModeMaster 
                ORDER BY Paymode ASC
            `);
        res.json({ success: true, data: result.recordset || [] });
    } catch (err) {
        // Fallback to DISTINCT from tblPayModeDetail if master table not present
        try {
            const pool = await getPool(req);
            const fallback = await pool.request()
                .query(`SELECT DISTINCT PayMode as Paymode FROM tblPayModeDetail WHERE PayMode IS NOT NULL AND PayMode <> '' ORDER BY PayMode ASC`);
            res.json({ success: true, data: fallback.recordset || [] });
        } catch (fErr) {
            console.error("Error fetching paymodes LOV:", fErr);
            res.status(500).json({ success: false, message: fErr.message });
        }
    }
});

// GET /api/paymententry/invoice/:invoiceNo
router.get('/invoice/:invoiceNo', async (req, res) => {
    try {
        const invoiceNo = (req.params.invoiceNo || '').trim();
        const pool = await getPool(req);
        const result = await pool.request()
            .input('InvoiceNo', sql.VarChar, invoiceNo)
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
                WHERE h.InvoiceNo = @InvoiceNo OR h.InvoiceNo LIKE @InvoiceNo + '%'
                ORDER BY h.InvoiceDate DESC
            `);
        res.json({ success: true, data: result.recordset || [] });
    } catch (err) {
        console.error("Error fetching single invoice:", err);
        res.status(500).json({ success: false, message: err.message });
    }
});

// POST /api/paymententry/save
router.post('/save', async (req, res) => {
    const { type, invoiceNo, paymodes, actualPaidAmount, gst, userId } = req.body;
    let pool;
    let transaction;
    let isTransactionActive = false;

    try {
        if (!invoiceNo) {
            return res.status(400).json({ success: false, message: "InvoiceNo is required" });
        }

        pool = await getPool(req);
        transaction = new sql.Transaction(pool);

        await transaction.begin();
        isTransactionActive = true;

        const sysUser = userId || 'ADMIN';

        if (type === 'Invoice') {
            // Check existing columns of tblSalesHeader to safely update BalanceAmt and avoid missing column errors
            let hasBalanceAmt = false;
            let invDate = new Date();
            let locCode = 'HQ';

            try {
                const colCheck = await new sql.Request(transaction).query(`
                    SELECT COLUMN_NAME 
                    FROM INFORMATION_SCHEMA.COLUMNS 
                    WHERE TABLE_NAME = 'tblSalesHeader' AND COLUMN_NAME = 'BalanceAmt'
                `);
                if (colCheck.recordset && colCheck.recordset.length > 0) {
                    hasBalanceAmt = true;
                }
            } catch (cErr) {
                console.warn("Could not check tblSalesHeader columns:", cErr.message);
            }

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
            updateHeaderReq.input('InvoiceNo', sql.VarChar, String(invoiceNo).trim());
            updateHeaderReq.input('UserId', sql.VarChar, sysUser);
            updateHeaderReq.input('PaidAmt', sql.Numeric(18, 4), parseFloat(actualPaidAmount) || 0);
            updateHeaderReq.input('GST', sql.Numeric(18, 4), parseFloat(gst) || 0);

            if (hasBalanceAmt) {
                await updateHeaderReq.query(`
                    UPDATE tblSalesHeader 
                    SET ModifyUser = @UserId, 
                        ModifyDate = GETDATE(),
                        PaidAmt = @PaidAmt, 
                        GST = @GST,
                        BalanceAmt = ISNULL(NetTotal, 0) - @PaidAmt
                    WHERE InvoiceNo = @InvoiceNo
                `);
            } else {
                await updateHeaderReq.query(`
                    UPDATE tblSalesHeader 
                    SET ModifyUser = @UserId, 
                        ModifyDate = GETDATE(),
                        PaidAmt = @PaidAmt, 
                        GST = @GST 
                    WHERE InvoiceNo = @InvoiceNo
                `);
            }

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
        } else if (type === 'eTRS') {
            const {
                passportNo,
                netAmount: rawNetAmount,
                gstAmount: rawGstAmount,
                etrsDocId,
                issueDate: rawIssueDate,
                issueTime,
                remarks: rawRemarks,
                staff: selectedStaff,
                photo,
                invoiceDate: rawInvoiceDate,
                day,
                month,
                year,
            } = req.body;

            // Resolve BillDate
            let billDate = new Date();
            if (rawInvoiceDate) {
                const parsed = new Date(rawInvoiceDate);
                if (!isNaN(parsed.getTime())) billDate = parsed;
            } else if (day && month && year) {
                const parsed = new Date(`${year}-${String(month).padStart(2, '0')}-${String(day).padStart(2, '0')}`);
                if (!isNaN(parsed.getTime())) billDate = parsed;
            }

            // Resolve IssueDate
            let issueDateObj = new Date();
            if (rawIssueDate) {
                const parsed = new Date(rawIssueDate);
                if (!isNaN(parsed.getTime())) issueDateObj = parsed;
            }

            const cleanBillNo = String(invoiceNo || '').trim().substring(0, 20);
            const cleanPassportNo = String(passportNo || '').trim().substring(0, 20);
            const cleanDocId = String(etrsDocId || '').trim().substring(0, 50);
            const cleanStaff = String(selectedStaff || '').trim().substring(0, 20);
            const cleanRemarks = String(rawRemarks || '').trim().substring(0, 20);
            const cleanTime = String(issueTime || '').trim().substring(0, 20);
            const numGst = parseFloat(String(rawGstAmount)) || 0;
            const numNet = parseFloat(String(rawNetAmount)) || 0;
            const imgData = photo ? String(photo).trim() : null;
            const cleanUser = String(sysUser || 'ADMIN').trim().substring(0, 20);

            const etrsReq = new sql.Request(transaction);
            etrsReq.input('BillNo', sql.VarChar(20), cleanBillNo);
            etrsReq.input('BillDate', sql.SmallDateTime, billDate);
            etrsReq.input('SalesmanCode', sql.VarChar(20), cleanStaff);
            etrsReq.input('PassportNo', sql.VarChar(20), cleanPassportNo);
            etrsReq.input('Gst', sql.Numeric(18, 2), numGst);
            etrsReq.input('NetAmount', sql.Numeric(18, 2), numNet);
            etrsReq.input('DocId', sql.VarChar(50), cleanDocId);
            etrsReq.input('eTRSIssueDate', sql.SmallDateTime, issueDateObj);
            etrsReq.input('eTRSIssueTime', sql.VarChar(20), cleanTime);
            etrsReq.input('Remarks', sql.VarChar(20), cleanRemarks);
            etrsReq.input('CustomerImage', sql.NVarChar(sql.MAX), imgData);
            etrsReq.input('UserId', sql.VarChar(20), cleanUser);

            await etrsReq.query(`
                IF NOT EXISTS (SELECT 1 FROM tbleTRSStatement WHERE RTRIM(BillNo) = @BillNo)
                BEGIN
                    INSERT INTO tbleTRSStatement (
                        BillNo, BillDate, SalesmanCode, PassPortNo, Gst, NetAmount,
                        DocId, eTRSIssueDate, eTRSIssueTime, Remarks, CustomerImage,
                        CreateUser, CreateDate, ModifyUser, ModifyDate
                    ) VALUES (
                        @BillNo, @BillDate, @SalesmanCode, @PassportNo, @Gst, @NetAmount,
                        @DocId, @eTRSIssueDate, @eTRSIssueTime, @Remarks, @CustomerImage,
                        @UserId, GETDATE(), @UserId, GETDATE()
                    )
                END
                ELSE
                BEGIN
                    UPDATE tbleTRSStatement SET
                        BillDate = @BillDate,
                        SalesmanCode = @SalesmanCode,
                        PassPortNo = @PassportNo,
                        Gst = @Gst,
                        NetAmount = @NetAmount,
                        DocId = @DocId,
                        eTRSIssueDate = @eTRSIssueDate,
                        eTRSIssueTime = @eTRSIssueTime,
                        Remarks = @Remarks,
                        CustomerImage = CASE WHEN @CustomerImage IS NOT NULL THEN @CustomerImage ELSE CustomerImage END,
                        ModifyUser = @UserId,
                        ModifyDate = GETDATE()
                    WHERE RTRIM(BillNo) = @BillNo
                END

                -- Update tblSalesHeader to mark eTRS ticket issued ('Y')
                UPDATE tblSalesHeader 
                SET JournalNo = 'Y',
                    ModifyUser = @UserId,
                    ModifyDate = GETDATE()
                WHERE RTRIM(InvoiceNo) = @BillNo
            `);
        }

        await transaction.commit();
        isTransactionActive = false;
        res.json({ success: true, message: "Payment saved successfully" });
    } catch (err) {
        console.error("Error saving payment:", err);
        if (isTransactionActive && transaction) {
            try {
                await transaction.rollback();
            } catch (rbErr) {
                console.error("Rollback error:", rbErr);
            }
        }
        res.status(500).json({ success: false, message: err.message || "Failed to save payment" });
    }
});

// POST /api/paymententry/void
router.post('/void', async (req, res) => {
    let pool;
    let transaction;
    let isTransactionActive = false;

    try {
        const { type, invoiceNo, remarks, userId } = req.body || {};
        const cleanInv = String(invoiceNo || '').trim();
        const cleanRemarks = String(remarks || '').trim();
        const sysUser = String(userId || 'ADMIN').trim().substring(0, 20);

        if (!cleanInv) {
            return res.status(400).json({ success: false, message: "Invoice No is required." });
        }

        if (type === 'eTRS_VOID' || type === 'eTRS') {
            if (!cleanRemarks) {
                return res.status(400).json({ success: false, message: "Remarks should not be blank." });
            }

            pool = await getPool(req);

            // 1. Verify existence in tbleTRSStatement
            const checkResult = await pool.request()
                .input('BillNo', sql.VarChar(20), cleanInv)
                .query(`
                    SELECT TOP 1 BillNo 
                    FROM tbleTRSStatement 
                    WHERE RTRIM(BillNo) = @BillNo
                `);

            if (!checkResult.recordset || checkResult.recordset.length === 0) {
                return res.status(404).json({ success: false, message: "eTRS Ticket not exists." });
            }

            // 2. Perform Void Transaction
            transaction = new sql.Transaction(pool);
            await transaction.begin();
            isTransactionActive = true;

            const voidReq = new sql.Request(transaction);
            voidReq.input('BillNo', sql.VarChar(20), cleanInv);
            voidReq.input('Remarks', sql.VarChar(50), ('VOID eTRS: ' + cleanRemarks).substring(0, 50));
            voidReq.input('UserId', sql.VarChar(20), sysUser);

            // Delete from tbleTRSStatement (matching original VB.NET eTRSStatementDelete logic)
            await voidReq.query(`
                DELETE FROM tbleTRSStatement 
                WHERE RTRIM(BillNo) = @BillNo;

                UPDATE tblSalesHeader 
                SET JournalNo = 'N',
                    Remarks = @Remarks,
                    ModifyUser = @UserId,
                    ModifyDate = GETDATE()
                WHERE RTRIM(InvoiceNo) = @BillNo;
            `);

            await transaction.commit();
            isTransactionActive = false;

            return res.json({ success: true, message: "eTRS Ticket Voided Successfully" });
        } else {
            return res.status(400).json({ success: false, message: `Unsupported void type: ${type}` });
        }
    } catch (err) {
        console.error("Error executing void:", err);
        if (isTransactionActive && transaction) {
            try {
                await transaction.rollback();
            } catch (rbErr) {
                console.error("Rollback error:", rbErr);
            }
        }
        return res.status(500).json({ success: false, message: err.message || "Failed to void invoice" });
    }
});

// POST /api/paymententry/cashdrawer
router.post('/cashdrawer', async (req, res) => {
    try {
        const pool = await getPool(req);
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
