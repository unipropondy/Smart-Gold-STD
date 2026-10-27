const express = require('express');
const router = express.Router();
const mssql = require('mssql');

// GET /api/catalogue/search
router.get('/search', async (req, res) => {
    try {
        const pool = req.pool;
        const { category, catalogue, fromWeight, toWeight, inventoryCode } = req.query;

        let query = `
            SELECT b.Locationcode, a.InventoryCode, a.Description, 
                   CASE a.Weight WHEN 0 THEN a.StoneWeight ELSE a.Weight END as Weight, 
                   a.SizeCode, a.SizeType, 
                   CASE Status WHEN 'IN' THEN 'IN' ELSE '' END as Status 
            FROM tblInventory a 
            INNER JOIN tblInventoryStock b ON a.InventoryCode = b.InventoryCode 
            WHERE b.qtyonhand > 0
        `;
        const request = pool.request();

        if (category && category.trim() !== '') {
            query += " AND a.CategoryCode = @category";
            request.input('category', mssql.VarChar, category.trim());
        }
        if (catalogue && catalogue.trim() !== '') {
            query += " AND a.CatalogueCode = @catalogue";
            request.input('catalogue', mssql.VarChar, catalogue.trim());
        }
        if (fromWeight && toWeight && parseFloat(fromWeight) > 0 && parseFloat(toWeight) > 0) {
            query += " AND a.Weight >= @fromWeight AND a.Weight <= @toWeight";
            request.input('fromWeight', mssql.Float, parseFloat(fromWeight));
            request.input('toWeight', mssql.Float, parseFloat(toWeight));
        }
        if (inventoryCode && inventoryCode.trim() !== '') {
            query += " AND a.InventoryCode = @inventoryCode";
            request.input('inventoryCode', mssql.VarChar, inventoryCode.trim());
        }

        query += " ORDER BY b.Locationcode";

        const result = await request.query(query);

        res.json({ success: true, data: result.recordset });
    } catch (error) {
        console.error("Error fetching catalogue details:", error);
        res.status(500).json({ success: false, message: "Server error" });
    }
});

// GET /api/catalogue/lov/category
router.get('/lov/category', async (req, res) => {
    try {
        const pool = req.pool;
        const result = await pool.request().query(`
            SELECT CategoryCode, CategoryName 
            FROM tblCategory 
            ORDER BY CategoryCode
        `);
        res.json({ success: true, data: result.recordset });
    } catch (error) {
        console.error("Error fetching category LOV:", error);
        res.status(500).json({ success: false, message: "Server error" });
    }
});

// GET /api/catalogue/lov/catalogue
router.get('/lov/catalogue', async (req, res) => {
    try {
        const pool = req.pool;
        const { category } = req.query;
        let query = `
            SELECT CatalogueCode, CatalogueName, CategoryCode, GoldLoss 
            FROM tblCatalogue
        `;
        const request = pool.request();
        if (category && category.trim() !== '') {
            query += " WHERE CategoryCode = @category";
            request.input('category', mssql.VarChar, category.trim());
        }
        query += " ORDER BY CatalogueCode";
        
        const result = await request.query(query);
        res.json({ success: true, data: result.recordset });
    } catch (error) {
        console.error("Error fetching catalogue LOV:", error);
        res.status(500).json({ success: false, message: "Server error" });
    }
});

// GET /api/catalogue (Retrieve all catalogues)
router.get('/', async (req, res) => {
    try {
        const pool = req.pool;
        const result = await pool.request().query("SELECT * FROM tblCatalogue ORDER BY CatalogueCode");
        res.json({ success: true, data: result.recordset });
    } catch (error) {
        console.error("Error fetching catalogues:", error);
        res.status(500).json({ success: false, message: "Server error", error: error.message });
    }
});

// POST /api/catalogue (Insert new catalogue)
router.post('/', async (req, res) => {
    try {
        const pool = req.pool;
        const { CatalogueCode, CatalogueName, CategoryCode, Remarks, IsActive } = req.body;
        if (!CatalogueCode || !CatalogueName) {
            return res.status(400).json({ success: false, message: "CatalogueCode and CatalogueName are required." });
        }

        // Check if exists
        const checkResult = await pool.request()
            .input('CatalogueCode', mssql.VarChar, CatalogueCode)
            .query("SELECT CatalogueCode FROM tblCatalogue WHERE CatalogueCode = @CatalogueCode");
        if (checkResult.recordset.length > 0) {
            return res.status(400).json({ success: false, message: "CatalogueCode already exists." });
        }

        const query = `
            INSERT INTO tblCatalogue (
                CatalogueCode, CatalogueName, CategoryCode, Remarks, IsActive,
                CreateUser, CreateDate, ModifyUser, ModifyDate
            ) VALUES (
                @CatalogueCode, @CatalogueName, @CategoryCode, @Remarks, @IsActive,
                'sys', GETDATE(), 'sys', GETDATE()
            )
        `;
        await pool.request()
            .input('CatalogueCode', mssql.VarChar, CatalogueCode)
            .input('CatalogueName', mssql.VarChar, CatalogueName)
            .input('CategoryCode', mssql.VarChar, CategoryCode || '')
            .input('Remarks', mssql.VarChar, Remarks || '')
            .input('IsActive', mssql.Bit, IsActive !== false ? 1 : 0)
            .query(query);

        res.json({ success: true, message: "Catalogue added successfully" });
    } catch (error) {
        console.error("Error inserting catalogue:", error);
        res.status(500).json({ success: false, message: "Server error", error: error.message });
    }
});

// PUT /api/catalogue (Update catalogue)
router.put('/', async (req, res) => {
    try {
        const pool = req.pool;
        const { CatalogueCode, CatalogueName, CategoryCode, Remarks, IsActive } = req.body;
        if (!CatalogueCode || !CatalogueName) {
            return res.status(400).json({ success: false, message: "CatalogueCode and CatalogueName are required." });
        }

        const query = `
            UPDATE tblCatalogue SET
                CatalogueName = @CatalogueName,
                CategoryCode = @CategoryCode,
                Remarks = @Remarks,
                IsActive = @IsActive,
                ModifyUser = 'sys',
                ModifyDate = GETDATE()
            WHERE CatalogueCode = @CatalogueCode
        `;
        await pool.request()
            .input('CatalogueCode', mssql.VarChar, CatalogueCode)
            .input('CatalogueName', mssql.VarChar, CatalogueName)
            .input('CategoryCode', mssql.VarChar, CategoryCode || '')
            .input('Remarks', mssql.VarChar, Remarks || '')
            .input('IsActive', mssql.Bit, IsActive !== false ? 1 : 0)
            .query(query);

        res.json({ success: true, message: "Catalogue updated successfully" });
    } catch (error) {
        console.error("Error updating catalogue:", error);
        res.status(500).json({ success: false, message: "Server error", error: error.message });
    }
});

// DELETE /api/catalogue (Delete catalogue)
router.delete('/', async (req, res) => {
    try {
        const pool = req.pool;
        const { id } = req.query;
        if (!id) {
            return res.status(400).json({ success: false, message: "ID parameter is required" });
        }
        await pool.request()
            .input('CatalogueCode', mssql.VarChar, id)
            .query("DELETE FROM tblCatalogue WHERE CatalogueCode = @CatalogueCode");
        res.json({ success: true, message: "Catalogue deleted successfully" });
    } catch (error) {
        console.error("Error deleting catalogue:", error);
        res.status(500).json({ success: false, message: "Server error", error: error.message });
    }
});

module.exports = router;
