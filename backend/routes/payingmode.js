const express = require('express');
const router = express.Router();
const { sql } = require('../db');

// GET /api/payingmode/initData
// Fetches initial data required for the paying mode transaction screen
router.get('/initData', async (req, res) => {
    try {
        const pool = req.pool;
        const { customerCode, date } = req.query;

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
            // 2 & 3. Reward and Redeemed Points (Simulated or Real depending on schema)
            // As per VB.NET code: GetsumRewardPointsByCustomer, GetsumRedeemedPointsByCustomer
            try {
                // If tblCustomer doesn't have these, we return 0.
                const custResult = await pool.request()
                    .input('CustomerCode', sql.VarChar, customerCode)
                    .query("SELECT * FROM sysobjects WHERE name='tblCustomer' AND xtype='U'");
                
                if (custResult.recordset.length > 0) {
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
                        rewardPoints = pointsResult.recordset[0].RewardPoints;
                        redeemPoints = pointsResult.recordset[0].RedeemPoints;
                    }
                }
            } catch (err) {
                console.error("Error fetching reward points:", err);
            }

            // 4. Deposit Details
            try {
                 const depResult = await pool.request()
                    .input('CustomerCode', sql.VarChar, customerCode)
                    .query(`
                        SELECT name FROM sysobjects WHERE name='tblDeposit' AND xtype='U'
                    `);
                 if (depResult.recordset.length > 0) {
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
                 }
            } catch (err) {
                console.error("Error fetching deposit:", err);
            }

            // 5. TradeIn Details
            try {
                 const trdResult = await pool.request()
                    .input('CustomerCode', sql.VarChar, customerCode)
                    .query(`
                        SELECT name FROM sysobjects WHERE name='tblSalesReturn' AND xtype='U'
                    `);
                 if (trdResult.recordset.length > 0) {
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
                 }
            } catch (err) {
                console.error("Error fetching tradein deposit:", err);
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
