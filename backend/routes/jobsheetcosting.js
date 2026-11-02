const express = require("express");
const router = express.Router();
const { sql } = require("../db");

// =========================================================================
// 1. GET LIST OF JOBSHEETS FOR LOV
// Exact reference: frmHelpSearch(0, 0, "tblJobSheetHeader", "L", 4, "JobSheetNo", "FixStatus='N'", "JobSheetNo", ...)
// Columns: JobSheetNo, CostPerGram, SupplierCode, PurchaseBillNo
// Filter: FixStatus = 'N'
// =========================================================================
router.get("/list", async (req, res) => {
    try {
        const pool = req.pool;
        
        const listResult = await pool.request().query(`
            SELECT JobSheetNo, CostPerGram, SupplierCode, PurchaseBillNo 
            FROM tblJobSheetHeader 
            WHERE FixStatus = 'N'
            ORDER BY JobSheetNo DESC
        `);

        res.json({ success: true, data: listResult.recordset });
    } catch (err) {
        console.error("Error fetching jobsheet list:", err);
        res.status(500).json({ success: false, message: "Server error", error: err.message });
    }
});

// =========================================================================
// 2. GET JOBSHEET DETAILS AND CLASSES
// Exact reference: subGetJobSheetDetail in frmFixCostJobSheet.vb
// Checks: tblJobSheetHeader where JobSheetNo = @JobSheetNo AND FixStatus = 'N'
// Returns: PurchaseBillNo, PurchaseBillDate, SupplierCode, and list of ClassName
// =========================================================================
router.get("/details", async (req, res) => {
    try {
        const pool = req.pool;
        const { JobSheetNo } = req.query;
        if (!JobSheetNo || !JobSheetNo.trim()) {
            return res.status(400).json({ success: false, message: "JobSheetNo is required." });
        }

        const trimmedJobSheetNo = JobSheetNo.trim();

        // 1. Check header
        const headerResult = await pool.request()
            .input('JobSheetNo', sql.VarChar, trimmedJobSheetNo)
            .query(`
                SELECT PurchaseBillNo, PurchaseBillDate, SupplierCode 
                FROM tblJobSheetHeader 
                WHERE JobSheetNo = @JobSheetNo AND FixStatus = 'N'
            `);

        if (headerResult.recordset.length === 0) {
            return res.status(404).json({ success: false, message: "Jobsheet No not found." });
        }

        const headerInfo = headerResult.recordset[0];

        // 2. Load classes (subLoadClassCombo)
        // Select distinct ClassName from tblJobSheetDetail joined with tblCategory and tblClass
        const classResult = await pool.request()
            .input('JobSheetNo', sql.VarChar, trimmedJobSheetNo)
            .query(`
                SELECT DISTINCT c.ClassName 
                FROM tblJobSheetDetail jsd
                INNER JOIN tblCategory cat ON jsd.CategoryCode = cat.CategoryCode
                INNER JOIN tblClass c ON cat.ClassCode = c.ClassCode
                WHERE jsd.JobSheetNo = @JobSheetNo
                ORDER BY c.ClassName ASC
            `);
        
        const classes = classResult.recordset.map(r => r.ClassName);

        res.json({ success: true, data: { ...headerInfo, classes } });
    } catch (err) {
        console.error("Error fetching jobsheet details:", err);
        res.status(500).json({ success: false, message: "Server error", error: err.message });
    }
});

// =========================================================================
// 3. GET OLD COST PER GRAM
// Exact reference: subGetOldCostPerGram in frmFixCostJobSheet.vb
// If ClassName <> "All":
//   Select CostPerGram from tblJobSheetDetail where JobSheetNo = ... And CategoryCode in 
//   (Select CategoryCode from tblCategory where ClassCode = (Select ClassCode from tblClass where ClassName = ...))
// If ClassName = "All":
//   Select CostPerGram from tblJobSheetHeader where JobSheetNo = ...
// =========================================================================
router.get("/oldcost", async (req, res) => {
    try {
        const pool = req.pool;
        const { JobSheetNo, ClassName } = req.query;
        if (!JobSheetNo || !ClassName) {
            return res.status(400).json({ success: false, message: "JobSheetNo and ClassName are required." });
        }

        const trimmedJobSheetNo = JobSheetNo.trim();
        const trimmedClassName = ClassName.trim();

        let query = "";
        if (trimmedClassName !== "All") {
            query = `
                SELECT TOP 1 CostPerGram 
                FROM tblJobSheetDetail 
                WHERE JobSheetNo = @JobSheetNo 
                AND CategoryCode IN (
                    SELECT CategoryCode FROM tblCategory 
                    WHERE ClassCode = (SELECT ClassCode FROM tblClass WHERE ClassName = @ClassName)
                )
            `;
        } else {
            query = `
                SELECT TOP 1 CostPerGram 
                FROM tblJobSheetHeader 
                WHERE JobSheetNo = @JobSheetNo
            `;
        }

        const result = await pool.request()
            .input('JobSheetNo', sql.VarChar, trimmedJobSheetNo)
            .input('ClassName', sql.VarChar, trimmedClassName)
            .query(query);

        if (result.recordset.length === 0 || result.recordset[0].CostPerGram == null) {
            return res.json({ success: true, data: { CostPerGram: 0 } });
        }

        res.json({ success: true, data: result.recordset[0] });
    } catch (err) {
        console.error("Error fetching old cost:", err);
        res.status(500).json({ success: false, message: "Server error", error: err.message });
    }
});

// =========================================================================
// 4. UPDATE COST (Save)
// Exact reference: subSave in frmFixCostJobSheet.vb
// Updates:
// - tblJobSheetDetail.CostPerGram & TotalCost
// - tblJobSheetHeader.CostPerGram & FixStatus = 'Y' & GoldValue & CashValue (if columns exist)
// - Wraps everything in a SQL Transaction with rollback on error
// =========================================================================
router.post("/updatecost", async (req, res) => {
    const pool = req.pool;
    const transaction = new sql.Transaction(pool);

    try {
        const { JobSheetNo, ClassName, OldCost, NewCost, GoldValue, CashValue } = req.body;

        if (!JobSheetNo || !JobSheetNo.trim()) {
            return res.status(400).json({ success: false, message: "JobsheeNo should not be blank." });
        }
        if (!NewCost || NewCost.toString().trim() === "") {
            return res.status(400).json({ success: false, message: "New cost should not be blank." });
        }
        if (isNaN(Number(NewCost))) {
            return res.status(400).json({ success: false, message: "New cost should be numeric." });
        }
        if (!ClassName || ClassName.toString().trim() === "") {
            return res.status(400).json({ success: false, message: "Please select the classCode." });
        }

        const trimmedJobSheetNo = JobSheetNo.trim();
        const numNewCost = parseFloat(NewCost);
        const numGoldValue = parseFloat(GoldValue) || 0;
        const numCashValue = parseFloat(CashValue) || 0;

        await transaction.begin();

        // Check columns in tblJobSheetHeader and tblJobSheetDetail
        const colCheckReq = new sql.Request(transaction);
        const headerColsResult = await colCheckReq.query(`
            SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tblJobSheetHeader'
        `);
        const headerCols = headerColsResult.recordset.map(c => c.COLUMN_NAME.toLowerCase());

        const detailColsResult = await colCheckReq.query(`
            SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tblJobSheetDetail'
        `);
        const detailCols = detailColsResult.recordset.map(c => c.COLUMN_NAME.toLowerCase());

        let classCode = null;
        if (ClassName !== "All") {
            const classReq = new sql.Request(transaction);
            const classRes = await classReq
                .input('ClassName', sql.VarChar, ClassName.trim())
                .query("SELECT ClassCode FROM tblClass WHERE ClassName = @ClassName");
            if (classRes.recordset.length > 0) {
                classCode = classRes.recordset[0].ClassCode;
            }
        }

        // 1. Update tblJobSheetDetail
        const detailUpdateReq = new sql.Request(transaction);
        detailUpdateReq.input('JobSheetNo', sql.VarChar, trimmedJobSheetNo);
        detailUpdateReq.input('NewCost', sql.Decimal(18, 4), numNewCost);

        const hasDetailTotalCost = detailCols.includes('totalcost');
        const hasDetailWeight = detailCols.includes('weight');

        let detailUpdateSql = "";
        if (classCode) {
            detailUpdateReq.input('ClassCode', sql.VarChar, classCode);
            if (hasDetailTotalCost && hasDetailWeight) {
                detailUpdateSql = `
                    UPDATE tblJobSheetDetail 
                    SET CostPerGram = @NewCost,
                        TotalCost = ROUND(ISNULL(Weight, 0) * @NewCost, 2)
                    WHERE JobSheetNo = @JobSheetNo 
                    AND CategoryCode IN (SELECT CategoryCode FROM tblCategory WHERE ClassCode = @ClassCode)
                `;
            } else {
                detailUpdateSql = `
                    UPDATE tblJobSheetDetail 
                    SET CostPerGram = @NewCost
                    WHERE JobSheetNo = @JobSheetNo 
                    AND CategoryCode IN (SELECT CategoryCode FROM tblCategory WHERE ClassCode = @ClassCode)
                `;
            }
        } else {
            // "All" classes
            if (hasDetailTotalCost && hasDetailWeight) {
                detailUpdateSql = `
                    UPDATE tblJobSheetDetail 
                    SET CostPerGram = @NewCost,
                        TotalCost = ROUND(ISNULL(Weight, 0) * @NewCost, 2)
                    WHERE JobSheetNo = @JobSheetNo
                `;
            } else {
                detailUpdateSql = `
                    UPDATE tblJobSheetDetail 
                    SET CostPerGram = @NewCost
                    WHERE JobSheetNo = @JobSheetNo
                `;
            }
        }

        await detailUpdateReq.query(detailUpdateSql);

        // 2. Update tblJobSheetHeader
        const headerUpdateReq = new sql.Request(transaction);
        headerUpdateReq.input('JobSheetNo', sql.VarChar, trimmedJobSheetNo);
        headerUpdateReq.input('NewCost', sql.Decimal(18, 4), numNewCost);
        headerUpdateReq.input('GoldValue', sql.Decimal(18, 4), numGoldValue);
        headerUpdateReq.input('CashValue', sql.Decimal(18, 4), numCashValue);

        const setClauses = ["FixStatus = 'Y'"];
        if (headerCols.includes('costpergram')) {
            setClauses.push("CostPerGram = @NewCost");
        }
        if (headerCols.includes('goldvalue')) {
            setClauses.push("GoldValue = @GoldValue");
        }
        if (headerCols.includes('cashvalue')) {
            setClauses.push("CashValue = @CashValue");
        }
        if (headerCols.includes('payablegold')) {
            setClauses.push("PayableGold = @GoldValue");
        }
        if (headerCols.includes('payablecash')) {
            setClauses.push("PayableCash = @CashValue");
        }

        const headerUpdateSql = `
            UPDATE tblJobSheetHeader
            SET ${setClauses.join(', ')}
            WHERE JobSheetNo = @JobSheetNo
        `;

        await headerUpdateReq.query(headerUpdateSql);

        await transaction.commit();
        res.json({ success: true, message: "Cost updated successfully!" });
    } catch (err) {
        try {
            await transaction.rollback();
        } catch (rbErr) {}
        console.error("Error updating cost:", err);
        res.status(500).json({ success: false, message: "Error in Save.", error: err.message });
    }
});

module.exports = router;
