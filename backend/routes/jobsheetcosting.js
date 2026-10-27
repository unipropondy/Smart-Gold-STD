const express = require("express");
const router = express.Router();
const { sql } = require("../db");

// Get List of JobSheets for LOV
router.get("/list", async (req, res) => {
    try {
        const pool = req.pool;
        
        const listResult = await pool.request().query(`
            SELECT JobSheetNo, CostPerGram, SupplierCode, PurchaseBillNo 
            FROM tblJobSheetHeader 
            WHERE FixStatus = 'N'
        `);

        res.json({ success: true, data: listResult.recordset });
    } catch (err) {
        console.error("Error fetching jobsheet list:", err);
        res.status(500).json({ success: false, message: "Server error", error: err.message });
    }
});

// Get JobSheet details and classes
router.get("/details", async (req, res) => {
    try {
        const pool = req.pool;
        const { JobSheetNo } = req.query;
        if (!JobSheetNo) {
            return res.status(400).json({ success: false, message: "JobSheetNo is required." });
        }

        // Get header info matching FixStatus='N'
        const headerResult = await pool.request()
            .input('JobSheetNo', sql.VarChar, JobSheetNo)
            .query(`
                SELECT PurchaseBillNo, PurchaseBillDate, SupplierCode 
                FROM tblJobSheetHeader 
                WHERE JobSheetNo = @JobSheetNo AND FixStatus = 'N'
            `);

        if (headerResult.recordset.length === 0) {
            return res.status(404).json({ success: false, message: "Jobsheet No not found or FixStatus is not 'N'." });
        }

        const headerInfo = headerResult.recordset[0];

        // Get classes for this jobsheet
        const classResult = await pool.request()
            .input('JobSheetNo', sql.VarChar, JobSheetNo)
            .query(`
                SELECT DISTINCT c.ClassName 
                FROM tblJobSheetDetail jsd
                INNER JOIN tblCategory cat ON jsd.CategoryCode = cat.CategoryCode
                INNER JOIN tblClass c ON cat.ClassCode = c.ClassCode
                WHERE jsd.JobSheetNo = @JobSheetNo
            `);
        
        const classes = classResult.recordset.map(r => r.ClassName);

        res.json({ success: true, data: { ...headerInfo, classes } });
    } catch (err) {
        console.error("Error fetching jobsheet details:", err);
        res.status(500).json({ success: false, message: "Server error", error: err.message });
    }
});

// Get Old Cost Per Gram
router.get("/oldcost", async (req, res) => {
    try {
        const pool = req.pool;
        const { JobSheetNo, ClassName } = req.query;
        if (!JobSheetNo || !ClassName) {
            return res.status(400).json({ success: false, message: "JobSheetNo and ClassName are required." });
        }

        let query = "";
        if (ClassName !== "All") {
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
            .input('JobSheetNo', sql.VarChar, JobSheetNo)
            .input('ClassName', sql.VarChar, ClassName)
            .query(query);

        if (result.recordset.length === 0) {
            return res.json({ success: true, data: { CostPerGram: 0 } });
        }

        res.json({ success: true, data: result.recordset[0] });
    } catch (err) {
        console.error("Error fetching old cost:", err);
        res.status(500).json({ success: false, message: "Server error", error: err.message });
    }
});

// Save/Update Cost
router.post("/updatecost", async (req, res) => {
    try {
        const pool = req.pool;
        const { JobSheetNo, ClassName, OldCost, NewCost, GoldValue, CashValue } = req.body;

        if (!JobSheetNo || !NewCost) {
            return res.status(400).json({ success: false, message: "JobSheetNo and NewCost are required." });
        }

        let classCode = null;
        if (ClassName && ClassName !== "All") {
            const classRes = await pool.request()
                .input('ClassName', sql.VarChar, ClassName)
                .query("SELECT ClassCode FROM tblClass WHERE ClassName = @ClassName");
            if (classRes.recordset.length > 0) {
                classCode = classRes.recordset[0].ClassCode;
            }
        }

        // Note: Full exact logic of objClsJobsheet.UpdateCost in VB.NET is abstracted,
        // so we update tblJobSheetHeader and/or tblJobSheetDetail.
        if (classCode) {
            await pool.request()
                .input('JobSheetNo', sql.VarChar, JobSheetNo)
                .input('ClassCode', sql.VarChar, classCode)
                .input('NewCost', sql.Decimal(18, 4), parseFloat(NewCost))
                .query(`
                    UPDATE tblJobSheetDetail 
                    SET CostPerGram = @NewCost 
                    WHERE JobSheetNo = @JobSheetNo 
                    AND CategoryCode IN (SELECT CategoryCode FROM tblCategory WHERE ClassCode = @ClassCode)
                `);
        } else {
            await pool.request()
                .input('JobSheetNo', sql.VarChar, JobSheetNo)
                .input('NewCost', sql.Decimal(18, 4), parseFloat(NewCost))
                .query(`
                    UPDATE tblJobSheetHeader 
                    SET CostPerGram = @NewCost 
                    WHERE JobSheetNo = @JobSheetNo
                `);
            
            await pool.request()
                .input('JobSheetNo', sql.VarChar, JobSheetNo)
                .input('NewCost', sql.Decimal(18, 4), parseFloat(NewCost))
                .query(`
                    UPDATE tblJobSheetDetail 
                    SET CostPerGram = @NewCost 
                    WHERE JobSheetNo = @JobSheetNo
                `);
        }

        res.json({ success: true, message: "Cost updated successfully!" });
    } catch (err) {
        console.error("Error updating cost:", err);
        res.status(500).json({ success: false, message: "Failed to update cost", error: err.message });
    }
});

module.exports = router;
