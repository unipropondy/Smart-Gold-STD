const express = require('express');
const router = express.Router();

// GET /api/scheme/customers - Dynamic Customer Help Search with Whitelist & Parameterized Query
router.get('/customers', async (req, res) => {
    try {
        const pool = req.pool;
        const { searchField = 'CustomerName', searchText = '', searchMode = 'Start', page, limit } = req.query;

        // Whitelist allowed search columns
        const allowedFields = {
            'CustomerName': 'CustomerName',
            'CustomerCode': 'CustomerCode',
            'NricNo': 'NricNo',
            'Address1': 'Address1'
        };
        const col = allowedFields[searchField] || 'CustomerName';

        let searchPattern = '%';
        const cleanText = searchText.trim();

        if (cleanText) {
            if (searchMode === 'Start') {
                searchPattern = `${cleanText}%`;
            } else if (searchMode === 'End') {
                searchPattern = `%${cleanText}`;
            } else {
                searchPattern = `%${cleanText}%`;
            }
        }

        const countRequest = pool.request();
        let countQuery = `SELECT COUNT(*) AS total FROM tblCustomer`;
        if (cleanText) {
            countRequest.input('SearchText', searchPattern);
            countQuery += ` WHERE ${col} LIKE @SearchText`;
        }
        const countRes = await countRequest.query(countQuery);
        const total = countRes.recordset[0].total;

        const dataRequest = pool.request();
        let dataQuery = `
            SELECT 
                RTRIM(CustomerCode) AS CustomerCode, 
                RTRIM(CustomerName) AS CustomerName, 
                RTRIM(NricNo) AS NricNo, 
                RTRIM(Address1) AS Address1 
            FROM tblCustomer
        `;

        if (cleanText) {
            dataRequest.input('SearchText', searchPattern);
            dataQuery += ` WHERE ${col} LIKE @SearchText`;
        }
        dataQuery += ` ORDER BY CustomerName`;

        // Optional pagination support if page and limit provided
        if (page && limit) {
            const pageNum = Math.max(1, parseInt(page, 10) || 1);
            const limitNum = Math.max(1, parseInt(limit, 10) || 50);
            const offset = (pageNum - 1) * limitNum;
            dataQuery += ` OFFSET ${offset} ROWS FETCH NEXT ${limitNum} ROWS ONLY`;
        }

        const dataRes = await dataRequest.query(dataQuery);

        res.json({
            success: true,
            total,
            data: dataRes.recordset
        });
    } catch (err) {
        console.error("Error searching customers for scheme:", err);
        res.status(500).json({ success: false, message: "Error performing customer search" });
    }
});

// GET /api/scheme/customer-detail/:customerCode - Customer Lookup with HQ logic fallback
router.get('/customer-detail/:customerCode', async (req, res) => {
    try {
        const pool = req.pool;
        const customerCode = req.params.customerCode ? req.params.customerCode.trim() : '';

        if (!customerCode) {
            return res.json({
                success: true,
                customer: {
                    customerCode: 'CASH',
                    customerName: 'CASH CUSTOMER',
                    customerType: 'RETAIL',
                    address1: '',
                    address2: '',
                    address3: ''
                }
            });
        }

        const result = await pool.request()
            .input('CustomerCode', customerCode)
            .query(`
                SELECT 
                    RTRIM(CustomerCode) AS CustomerCode, 
                    RTRIM(CustomerName) AS CustomerName, 
                    RTRIM(CustomerType) AS CustomerType, 
                    RTRIM(Address1) AS Address1, 
                    RTRIM(Address2) AS Address2, 
                    RTRIM(Address3) AS Address3 
                FROM tblCustomer 
                WHERE CustomerCode = @CustomerCode
            `);

        if (result.recordset.length > 0) {
            const c = result.recordset[0];
            res.json({
                success: true,
                customer: {
                    customerCode: (c.CustomerCode || '').trim(),
                    customerName: (c.CustomerName || '').trim(),
                    customerType: (c.CustomerType || '').trim(),
                    address1: (c.Address1 || '').trim(),
                    address2: (c.Address2 || '').trim(),
                    address3: (c.Address3 || '').trim()
                }
            });
        } else {
            // Fallback as per legacy behavior when customer is not found
            res.json({
                success: true,
                customer: {
                    customerCode: 'CASH',
                    customerName: 'CASH CUSTOMER',
                    customerType: 'RETAIL',
                    address1: '',
                    address2: '',
                    address3: ''
                }
            });
        }
    } catch (err) {
        console.error("Error fetching customer for scheme:", err);
        res.status(500).json({ success: false, message: "Error fetching customer details" });
    }
});

// GET /api/scheme/master - Dynamic Scheme Master Help Search
router.get('/master', async (req, res) => {
    try {
        const pool = req.pool;
        const { searchField = 'Description', searchText = '', searchMode = 'Start' } = req.query;

        const allowedFields = {
            'Description': 'Description',
            'Code': 'Code',
            'Remarks': 'Remarks',
            'CreateUser': 'CreateUser'
        };
        const col = allowedFields[searchField] || 'Description';

        let searchPattern = '%';
        const cleanText = searchText.trim();

        if (cleanText) {
            if (searchMode === 'Start') {
                searchPattern = `${cleanText}%`;
            } else if (searchMode === 'End') {
                searchPattern = `%${cleanText}`;
            } else {
                searchPattern = `%${cleanText}%`;
            }
        }

        const countRequest = pool.request();
        let countQuery = `SELECT COUNT(*) AS total FROM tblSchemeMaster`;
        if (cleanText) {
            countRequest.input('SearchText', searchPattern);
            countQuery += ` WHERE ${col} LIKE @SearchText`;
        }
        const countRes = await countRequest.query(countQuery);
        const total = countRes.recordset[0].total;

        const dataRequest = pool.request();
        let dataQuery = `
            SELECT 
                RTRIM(Code) AS Code, 
                RTRIM(Description) AS Description, 
                RTRIM(Remarks) AS Value, 
                RTRIM(CreateUser) AS CreateUser 
            FROM tblSchemeMaster
        `;

        if (cleanText) {
            dataRequest.input('SearchText', searchPattern);
            dataQuery += ` WHERE ${col} LIKE @SearchText`;
        }
        dataQuery += ` ORDER BY Description`;

        const dataRes = await dataRequest.query(dataQuery);

        res.json({
            success: true,
            total,
            data: dataRes.recordset
        });
    } catch (err) {
        console.error("Error fetching scheme master:", err);
        res.status(500).json({ success: false, message: "Error fetching scheme master" });
    }
});

// GET /api/scheme/master/:code - Fetch single scheme by Code
router.get('/master/:code', async (req, res) => {
    try {
        const pool = req.pool;
        const code = req.params.code ? req.params.code.trim() : '';
        const result = await pool.request()
            .input('Code', code)
            .query(`
                SELECT 
                    RTRIM(Code) AS Code, 
                    RTRIM(Description) AS Description, 
                    RTRIM(Remarks) AS Remarks 
                FROM tblSchemeMaster 
                WHERE Code = @Code
            `);
        if (result.recordset.length > 0) {
            const s = result.recordset[0];
            res.json({
                success: true,
                scheme: {
                    code: s.Code,
                    description: s.Description,
                    value: s.Remarks || '0'
                }
            });
        } else {
            res.status(404).json({ success: false, message: "Scheme not found" });
        }
    } catch (err) {
        console.error("Error fetching single scheme:", err);
        res.status(500).json({ success: false, message: "Error fetching scheme" });
    }
});

// POST /api/scheme/salesman/validate - Validate Salesman password against tblSalesMan
router.post('/salesman/validate', async (req, res) => {
    try {
        const { password } = req.body;
        if (!password || !password.trim()) {
            return res.status(400).json({ success: false, message: "Please enter Sales Man password." });
        }

        const pool = req.pool;
        const result = await pool.request()
            .input('Password', password.trim())
            .query(`
                SELECT 
                    RTRIM(SalesManCode) AS SalesManCode, 
                    RTRIM(SalesManName) AS SalesManName 
                FROM tblSalesMan 
                WHERE Password = @Password
            `);

        if (result.recordset.length > 0) {
            const sm = result.recordset[0];
            res.json({
                success: true,
                salesManName: sm.SalesManName || sm.SalesManCode,
                salesmanCode: sm.SalesManCode
            });
        } else {
            res.status(401).json({ success: false, message: "Invalid Password." });
        }
    } catch (err) {
        console.error("Error validating salesman:", err);
        res.status(500).json({ success: false, message: "Error validating salesman password" });
    }
});

// GET /api/scheme/next-number - Generate next Scheme No in "GS-XXXXX" format
router.get('/next-number', async (req, res) => {
    try {
        const pool = req.pool;
        // Query max number from existing transactions or counter
        const result = await pool.request()
            .query(`
                SELECT TOP 1 InvoiceNo 
                FROM tblMemberTransactionAJ 
                WHERE InvoiceNo LIKE 'GS-%' 
                ORDER BY InvoiceNo DESC
            `);

        let nextNum = 10001;
        if (result.recordset.length > 0 && result.recordset[0].InvoiceNo) {
            const raw = result.recordset[0].InvoiceNo.replace('GS-', '');
            const parsed = parseInt(raw, 10);
            if (!isNaN(parsed)) {
                nextNum = parsed + 1;
            }
        }
        res.json({ success: true, schemeNo: `GS-${nextNum}` });
    } catch (err) {
        console.error("Error getting next scheme number:", err);
        res.json({ success: true, schemeNo: `GS-${Date.now().toString().slice(-5)}` });
    }
});

// GET /api/scheme/transactions/:customerCode - Fetch customer's transactions matching legacy vwMemberTransationaj
router.get('/transactions/:customerCode', async (req, res) => {
    try {
        const pool = req.pool;
        const customerCode = req.params.customerCode ? req.params.customerCode.trim() : '';

        const result = await pool.request()
            .input('CustomerCode', customerCode)
            .query(`
                SELECT DISTINCT
                    RTRIM(m.InvoiceNo) AS InvoiceNo,
                    CONVERT(varchar, m.InvoiceDate, 103) AS InvoiceDate,
                    ISNULL(m.InvoiceAmount, 0) AS SchemeAmount,
                    ISNULL(sd.DailyRate, 0) AS DailyRate,
                    ISNULL(sd.Weight, ISNULL(m.RewardPoints, 0)) AS WeightAccured
                FROM tblMemberTransactionAJ m
                LEFT JOIN tblSalesDetail sd ON RTRIM(m.InvoiceNo) = RTRIM(sd.InvoiceNo)
                WHERE RTRIM(m.CustomerCode) = @CustomerCode
                ORDER BY InvoiceDate DESC
            `);

        res.json({ success: true, data: result.recordset });
    } catch (err) {
        console.error("Error fetching member transactions:", err);
        res.status(500).json({ success: false, message: "Error fetching transaction history" });
    }
});


// POST /api/scheme/transaction - Save Scheme Transaction with SQL Transaction (Commit/Rollback)
router.post('/transaction', async (req, res) => {
    const { 
        customerCode, customerName, customerType, 
        salesmanCode, schemeCode, schemeValue, 
        mode, paymentMode, paymentAmount, payments 
    } = req.body;

    if (!salesmanCode) {
        return res.status(400).json({ success: false, message: "Sales Man is required." });
    }
    if (!customerCode) {
        return res.status(400).json({ success: false, message: "Customer Code is required." });
    }
    if (!schemeCode) {
        return res.status(400).json({ success: false, message: "Scheme Code is required." });
    }

    const numericSchemeVal = parseFloat(schemeValue);
    if (isNaN(numericSchemeVal) || numericSchemeVal <= 0) {
        return res.status(400).json({ success: false, message: "Scheme Value must be greater than zero." });
    }

    let transaction;
    try {
        const pool = req.pool;
        transaction = pool.transaction();
        await transaction.begin();

        // 1. Get Next Scheme No inside transaction
        const nextRes = await transaction.request()
            .query(`
                SELECT TOP 1 InvoiceNo 
                FROM tblMemberTransactionAJ 
                WHERE InvoiceNo LIKE 'GS-%' 
                ORDER BY InvoiceNo DESC
            `);

        let nextNum = 10001;
        if (nextRes.recordset.length > 0 && nextRes.recordset[0].InvoiceNo) {
            const raw = nextRes.recordset[0].InvoiceNo.replace('GS-', '');
            const parsed = parseInt(raw, 10);
            if (!isNaN(parsed)) nextNum = parsed + 1;
        }
        const schemeNo = `GS-${nextNum}`;

        // 2. Insert into tblMemberTransactionAJ
        await transaction.request()
            .input('LocationCode', 'HQ')
            .input('CustomerCode', customerCode)
            .input('InvoiceNo', schemeNo)
            .input('InvoiceDate', new Date())
            .input('InvoiceAmount', numericSchemeVal)
            .input('RewardPoints', mode === 'GRAM' ? numericSchemeVal : 0)
            .input('RedeemPoints', 0)
            .input('CreateUser', salesmanCode || 'SYSTEM')
            .input('Remarks', `Mode: ${mode || 'VALUE'}`)
            .query(`
                INSERT INTO tblMemberTransactionAJ (
                    LocationCode, CustomerCode, InvoiceNo, InvoiceDate, 
                    InvoiceAmount, RewardPoints, RedeemPoints, CreateUser, CreateDate, Remarks
                ) VALUES (
                    @LocationCode, @CustomerCode, @InvoiceNo, @InvoiceDate, 
                    @InvoiceAmount, @RewardPoints, @RedeemPoints, @CreateUser, GETDATE(), @Remarks
                )
            `);

        // 3. Save Payment Modes into tblPayModeDetail
        const paymentList = Array.isArray(payments) && payments.length > 0 
            ? payments 
            : [{ paymode: paymentMode || 'CASH', amount: parseFloat(paymentAmount) || numericSchemeVal }];

        for (const p of paymentList) {
            const pAmt = parseFloat(p.amount) || 0;
            if (pAmt > 0) {
                const pm = p.paymode || 'CASH';
                await transaction.request()
                    .input('InvoiceNo', schemeNo)
                    .input('InvoiceDate', new Date())
                    .input('Paymode', pm)
                    .input('Amount', pAmt)
                    .input('SalesType', 'SCHEME')
                    .input('CreateUser', salesmanCode || 'SYSTEM')
                    .query(`
                        IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tblPayModeDetail')
                        BEGIN
                            INSERT INTO tblPayModeDetail (
                                InvoiceNo, InvoiceDate, Paymode, Amount, SalesType, CreateUser, CreateDate
                            ) VALUES (
                                @InvoiceNo, @InvoiceDate, @Paymode, @Amount, @SalesType, @CreateUser, GETDATE()
                            )
                        END
                    `);
            }
        }

        await transaction.commit();
        res.json({ 
            success: true, 
            message: "Scheme Transaction saved successfully!", 
            schemeNo: schemeNo 
        });

    } catch (err) {
        console.error("Scheme Transaction Save Failed:", {
            message: err.message,
            code: err.code,
            number: err.number,
            originalError: err.originalError ? err.originalError.message : null
        });
        if (transaction) {
            try {
                await transaction.rollback();
            } catch (rbErr) {
                console.error("Rollback error:", rbErr);
            }
        }
        res.status(500).json({ 
            success: false, 
            message: err.message || "Failed to save Scheme Transaction. Rollback performed.",
            errorDetails: process.env.NODE_ENV === 'development' || true ? {
                message: err.message,
                number: err.number,
                code: err.code
            } : undefined
        });
    }
});

// POST /api/scheme/cancel
router.post('/cancel', async (req, res) => {
    try {
        const { schemeNo } = req.body;
        if (!schemeNo) {
            return res.status(400).json({ success: false, message: "Scheme No is required." });
        }
        const pool = req.pool;
        await pool.request()
            .input('InvoiceNo', schemeNo)
            .query("DELETE FROM tblMemberTransactionAJ WHERE InvoiceNo = @InvoiceNo");

        res.json({ success: true, message: "Scheme Transaction cancelled successfully." });
    } catch (err) {
        console.error("Error cancelling scheme transaction:", err);
        res.status(500).json({ success: false, message: "Error cancelling transaction" });
    }
});

// GET /api/scheme/reprint/:invoiceNo
router.get('/reprint/:invoiceNo', async (req, res) => {
    try {
        const pool = req.pool;
        const invoiceNo = req.params.invoiceNo ? req.params.invoiceNo.trim() : '';

        console.log("Reprint request received for InvoiceNo:", invoiceNo);

        let result = await pool.request()
            .input('InvoiceNo', invoiceNo)
            .query(`
                SELECT TOP 1 * 
                FROM vwMemberTransationaj 
                WHERE RTRIM(InvoiceNo) = @InvoiceNo
            `);

        if (result.recordset.length === 0) {
            result = await pool.request()
                .input('InvoiceNo', invoiceNo)
                .query(`
                    SELECT TOP 1 * 
                    FROM tblMemberTransactionAJ 
                    WHERE RTRIM(InvoiceNo) = @InvoiceNo
                `);
        }

        console.log("Reprint lookup result count:", result.recordset.length);

        if (result.recordset.length > 0) {
            res.json({ success: true, data: result.recordset[0] });
        } else {
            res.status(404).json({ success: false, message: `Transaction record ${invoiceNo} not found for reprint.` });
        }
    } catch (err) {
        console.error("Error fetching reprint data:", err);
        res.status(500).json({ success: false, message: "Error fetching reprint data" });
    }
});

module.exports = router;




