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

        // 1. Get PayModes
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

        if (customerCode) {
            // 2 & 3. Reward and Redeemed Points
            // As per VB.NET code: GetsumRewardPointsByCustomer, GetsumRedeemedPointsByCustomer
            try {
                const pointsResult = await pool.request()
                    .input('CustomerCode', sql.VarChar, customerCode)
                    .query(`
                        SELECT 
                            ISNULL(RewardPoints, 0) as RewardPoints, 
                            ISNULL(RedeemPoints, 0) as RedeemPoints 
                        FROM tblCustomer 
                        WHERE CustomerCode = @CustomerCode
                    `);
                if (pointsResult.recordset.length > 0) {
                    rewardPoints = parseFloat(pointsResult.recordset[0].RewardPoints) || 0;
                    redeemPoints = parseFloat(pointsResult.recordset[0].RedeemPoints) || 0;
                }
            } catch (err) {
                console.error("Error fetching reward points:", err);
            }

            // Customer today redeemed points
            try {
                const redToday = await pool.request()
                    .input('CustomerCode', sql.VarChar, customerCode)
                    .query(`
                        SELECT ISNULL(SUM(RedeemPoints), 0) as RedeemedToday
                        FROM tblPosMemberTransaction
                        WHERE CustomerCode = @CustomerCode AND CAST(TransDate AS DATE) = CAST(GETDATE() AS DATE)
                    `);
                if (redToday.recordset.length > 0) {
                    redeemedPoints = parseFloat(redToday.recordset[0].RedeemedToday) || 0;
                }
            } catch (err) {
                // Table might not exist or alternate schema
            }

            // 4. Deposit Details
            try {
                const depData = await pool.request()
                    .input('CustomerCode', sql.VarChar, customerCode)
                    .query(`
                        SELECT TOP 1 DepositNo, (ISNULL(DepositAmount,0) + ISNULL(AmountPaid,0)) as DepositAmount 
                        FROM tblDeposit 
                        WHERE CustomerCode = @CustomerCode AND ISNULL(IsUsed, 0) = 0
                        ORDER BY DepositDate DESC
                    `);
                if (depData.recordset.length > 0) {
                    depositDetails = depData.recordset[0];
                }
            } catch (err) {
                // Ignore missing table
            }

            // 5. TradeIn Details
            try {
                const trdData = await pool.request()
                    .input('CustomerCode', sql.VarChar, customerCode)
                    .query(`
                        SELECT TOP 1 SalesReturnNo, ISNULL(Amount,0) as Amount 
                        FROM tblSalesReturn 
                        WHERE CustomerCode = @CustomerCode AND ReturnType = 'TRADEIN' AND ISNULL(IsUsed, 0) = 0
                        ORDER BY ReturnDate DESC
                    `);
                if (trdData.recordset.length > 0) {
                    depositTradeInDetails = trdData.recordset[0];
                }
            } catch (err) {
                // Ignore missing table
            }
        }

        res.json({
            success: true,
            payModes,
            customerData: {
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
