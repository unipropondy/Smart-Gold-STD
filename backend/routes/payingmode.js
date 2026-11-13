const express = require('express');
const router = express.Router();
const { sql, poolPromise } = require('../db');

// Helper to acquire database connection pool reliably
const getPool = async (req) => {
  if (req && req.pool) return req.pool;
  if (poolPromise) return await poolPromise;
  throw new Error("Database pool is not available.");
};

// GET /api/payingmode/initData
// Fetches initial data required for the paying mode transaction screen
router.get('/initData', async (req, res) => {
    try {
        const pool = await getPool(req);
        const { customerCode, customerType, date } = req.query;

        // Fetch company short code to handle company-specific rules (e.g. 'AJ', 'MD', 'HN')
        let companyShortCode = 'AJ';
        try {
            const compRes = await pool.request().query("SELECT TOP 1 ShortCode, CompanyCode FROM tblCompany");
            if (compRes.recordset.length > 0) {
                companyShortCode = (compRes.recordset[0].ShortCode || compRes.recordset[0].CompanyCode || 'AJ').trim().toUpperCase();
            }
        } catch (_) {}

        // 1. Get PayModes (objPay.GetPayMode() -> "SELECT * FROM tblPaymode ORDER BY POSITION")
        let payModes = [];
        try {
            const paymodeResult = await pool.request().query("SELECT * FROM tblPaymode ORDER BY Position ASC");
            payModes = paymodeResult.recordset;
        } catch (err) {
            console.error("Error fetching paymodes (maybe table missing):", err);
        }

        // Initialize variables for customer details
        let rewardPoints = 0;
        let redeemPoints = 0;
        let redeemedPoints = 0;
        let depositDetails = null;
        let depositTradeInDetails = null;

        if (customerCode && String(customerCode).trim().toUpperCase() !== 'CASH') {
            const cleanCustCode = String(customerCode).trim();

            // 2 & 3. Reward and Redeemed Points
            // As per VB.NET objCustomer.GetsumRewardPointsByCustomer:
            // "Select CustomerCode,sum(RewardPoints)RewardPoints, Sum(RedeemPoints)RedeemPoints from tblMemberTransactionaj where CustomerCode = ... group by CustomerCode"
            try {
                const pointsResult = await pool.request()
                    .input('CustomerCode', sql.VarChar, cleanCustCode)
                    .query(`
                        SELECT 
                            ISNULL(SUM(RewardPoints), 0) as RewardPoints, 
                            ISNULL(SUM(RedeemPoints), 0) as RedeemPoints 
                        FROM tblMemberTransactionaj 
                        WHERE CustomerCode = @CustomerCode
                        GROUP BY CustomerCode
                    `);
                if (pointsResult.recordset.length > 0) {
                    rewardPoints = parseFloat(pointsResult.recordset[0].RewardPoints) || 0;
                    redeemPoints = parseFloat(pointsResult.recordset[0].RedeemPoints) || 0;
                } else {
                    // Fallback to tblCustomer if member transactions table is empty for customer
                    const custResult = await pool.request()
                        .input('CustomerCode', sql.VarChar, cleanCustCode)
                        .query(`
                            SELECT 
                                ISNULL(RewardPoints, 0) as RewardPoints, 
                                ISNULL(RedeemPoints, 0) as RedeemPoints 
                            FROM tblCustomer 
                            WHERE CustomerCode = @CustomerCode
                        `);
                    if (custResult.recordset.length > 0) {
                        rewardPoints = parseFloat(custResult.recordset[0].RewardPoints) || 0;
                        redeemPoints = parseFloat(custResult.recordset[0].RedeemPoints) || 0;
                    }
                }
            } catch (err) {
                console.error("Error fetching reward points:", err);
            }

            // Customer today redeemed points
            // As per VB.NET objCustomer.GetsumRedeemedPointsByCustomer:
            // "Select CustomerCode,Sum(RedeemPoints)RedeemPoints from tblMemberTransactionaj where CustomerCode = ... and Invoicedate = ... group by CustomerCode"
            try {
                const effectiveDate = date ? new Date(date) : new Date();
                const redToday = await pool.request()
                    .input('CustomerCode', sql.VarChar, cleanCustCode)
                    .input('TransDate', sql.Date, effectiveDate)
                    .query(`
                        SELECT ISNULL(SUM(RedeemPoints), 0) as RedeemedToday
                        FROM tblMemberTransactionaj
                        WHERE CustomerCode = @CustomerCode AND CAST(InvoiceDate AS DATE) = CAST(@TransDate AS DATE)
                        GROUP BY CustomerCode
                    `);
                if (redToday.recordset.length > 0) {
                    redeemedPoints = parseFloat(redToday.recordset[0].RedeemedToday) || 0;
                } else {
                    const redToday2 = await pool.request()
                        .input('CustomerCode', sql.VarChar, cleanCustCode)
                        .input('TransDate', sql.Date, effectiveDate)
                        .query(`
                            SELECT ISNULL(SUM(RedeemPoints), 0) as RedeemedToday
                            FROM tblPosMemberTransaction
                            WHERE CustomerCode = @CustomerCode AND CAST(TransDate AS DATE) = CAST(@TransDate AS DATE)
                            GROUP BY CustomerCode
                        `);
                    if (redToday2.recordset.length > 0) {
                        redeemedPoints = parseFloat(redToday2.recordset[0].RedeemedToday) || 0;
                    }
                }
            } catch (err) {
                console.error("Error fetching redeemed points today:", err);
            }

            // 4. Deposit Details (ClsSalesReturn.GetDepositDetails:
            // "SELECT * FROM tblDepositSave WHERE CustomerCode = ... and DepositUsed='N'")
            try {
                let depData = await pool.request()
                    .input('CustomerCode', sql.VarChar, cleanCustCode)
                    .query(`
                        SELECT TOP 1 DepositNo, (ISNULL(DepositAmount,0) + ISNULL(AmountPaid,0)) as DepositAmount 
                        FROM tblDepositSave 
                        WHERE CustomerCode = @CustomerCode AND ISNULL(DepositUsed, 'N') = 'N'
                        ORDER BY DepositDate DESC
                    `);
                if (depData.recordset.length === 0) {
                    depData = await pool.request()
                        .input('CustomerCode', sql.VarChar, cleanCustCode)
                        .query(`
                            SELECT TOP 1 DepositNo, (ISNULL(DepositAmount,0) + ISNULL(AmountPaid,0)) as DepositAmount 
                            FROM tblCustomerDeposit 
                            WHERE CustomerCode = @CustomerCode AND ISNULL(DepositUsed, 'N') = 'N'
                            ORDER BY DepositDate DESC
                        `);
                }
                if (depData.recordset.length === 0) {
                    depData = await pool.request()
                        .input('CustomerCode', sql.VarChar, cleanCustCode)
                        .query(`
                            SELECT TOP 1 DepositNo, (ISNULL(DepositAmount,0) + ISNULL(AmountPaid,0)) as DepositAmount 
                            FROM tblDeposit 
                            WHERE CustomerCode = @CustomerCode AND (ISNULL(IsUsed, 0) = 0 OR ISNULL(DepositUsed, 'N') = 'N')
                            ORDER BY DepositDate DESC
                        `);
                }
                if (depData.recordset.length > 0) {
                    depositDetails = depData.recordset[0];
                }
            } catch (err) {
                console.error("Error fetching deposit details:", err);
            }

            // 5. TradeIn Details (ClsSalesReturn.GetDepositTradeInDetails:
            // "SELECT * FROM tblDeposit WHERE SalesType='TRADEIN' and CustomerCode = ... and DepositUsed='N'")
            try {
                let trdData = await pool.request()
                    .input('CustomerCode', sql.VarChar, cleanCustCode)
                    .query(`
                        SELECT TOP 1 SalesReturnNo, ISNULL(Amount,0) as Amount 
                        FROM tblDeposit 
                        WHERE CustomerCode = @CustomerCode AND SalesType = 'TRADEIN' AND ISNULL(DepositUsed, 'N') = 'N'
                        ORDER BY DepositDate DESC
                    `);
                if (trdData.recordset.length === 0) {
                    trdData = await pool.request()
                        .input('CustomerCode', sql.VarChar, cleanCustCode)
                        .query(`
                            SELECT TOP 1 SalesReturnNo, ISNULL(Amount,0) as Amount 
                            FROM tblSalesReturn 
                            WHERE CustomerCode = @CustomerCode AND ReturnType = 'TRADEIN' AND ISNULL(IsUsed, 0) = 0
                            ORDER BY ReturnDate DESC
                        `);
                }
                if (trdData.recordset.length > 0) {
                    depositTradeInDetails = trdData.recordset[0];
                }
            } catch (err) {
                console.error("Error fetching trade-in details:", err);
            }
        }

        res.json({
            success: true,
            companyShortCode,
            payModes,
            customerData: {
                customerType: customerType || '',
                rewardPoints,
                redeemPoints,
                redeemedPoints,
                depositDetails,
                depositTradeInDetails
            }
        });
    } catch (error) {
        console.error("PayingMode initData error:", error);
        res.status(500).json({ success: false, message: "Server Error", error: error.message });
    }
});

module.exports = router;
