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

// GET /api/catalogue/validate-category/:code (Check if CategoryCode is valid matching objClsCategory.GetCategoryName)
router.get('/validate-category/:code', async (req, res) => {
    try {
        const pool = req.pool;
        const { code } = req.params;
        if (!code || !code.trim()) {
            return res.status(400).json({ success: false, message: "CategoryCode should not be blank." });
        }
        const result = await pool.request()
            .input('code', mssql.VarChar, code.trim())
            .query("SELECT TOP 1 CategoryCode, CategoryName FROM tblCategory WHERE RTRIM(LTRIM(CategoryCode)) = RTRIM(LTRIM(@code))");

        if (result.recordset.length > 0) {
            res.json({ success: true, data: result.recordset[0] });
        } else {
            res.json({ success: false, message: "Pleae enter valid CatagoryCode." });
        }
    } catch (error) {
        console.error("Error validating category code:", error);
        res.status(500).json({ success: false, message: "Server error", error: error.message });
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

// GET /api/catalogue/:code (Fetch catalogue by code matching clsCatalogue.GetCatalogueCode)
router.get('/:code', async (req, res) => {
    try {
        const pool = req.pool;
        const { code } = req.params;
        if (!code || !code.trim()) {
            return res.status(400).json({ success: false, message: "Catalogue code is required" });
        }

        let result = await pool.request()
            .input('code', mssql.VarChar, code.trim())
            .query(`
                SELECT a.*, b.CategoryName, b.ClassCode, b.NextInventoryCode, b.SizeFactor, c.ClassName, c.SalesType, c.PurchaseType
                FROM tblCatalogue a WITH (NOLOCK)
                LEFT JOIN tblCategory b WITH (NOLOCK) ON a.CategoryCode = b.CategoryCode
                LEFT JOIN tblClass c WITH (NOLOCK) ON b.ClassCode = c.ClassCode
                WHERE a.CatalogueCode = @code
            `);

        if (result.recordset && result.recordset.length > 0) {
            return res.status(200).json({ success: true, data: result.recordset[0] });
        }

        const directResult = await pool.request()
            .input('code', mssql.VarChar, code.trim())
            .query(`
                SELECT a.*, b.CategoryName, b.ClassCode, b.NextInventoryCode, b.SizeFactor
                FROM tblCatalogue a WITH (NOLOCK)
                LEFT JOIN tblCategory b WITH (NOLOCK) ON a.CategoryCode = b.CategoryCode
                WHERE a.CatalogueCode = @code
            `);

        if (directResult.recordset && directResult.recordset.length > 0) {
            return res.status(200).json({ success: true, data: directResult.recordset[0] });
        }

        return res.status(200).json({ success: false, message: "Invalid catalogue code!" });
    } catch (error) {
        console.error("Error fetching catalogue by code:", error);
        return res.status(500).json({ success: false, message: "Server error", error: error.message });
    }
});

// POST /api/catalogue (Insert new catalogue)
router.post('/', async (req, res) => {
    try {
        const pool = req.pool;
        const { CatalogueCode, CatalogueName, CategoryCode, Remarks, IsActive } = req.body;
        
        // Validation matching subValidateMe in frmCatalogue.vb
        if (!CatalogueCode || !CatalogueCode.trim()) {
            return res.status(400).json({ success: false, message: "CatalogueCode should not be blank." });
        }
        if (!CatalogueName || !CatalogueName.trim()) {
            return res.status(400).json({ success: false, message: "CatalogueName should not be blank." });
        }
        if (!CategoryCode || !CategoryCode.trim()) {
            return res.status(400).json({ success: false, message: "CategoryCode should not be blank." });
        }

        // Validate CategoryCode existence matching objClsCategory.GetCategoryName
        const catCheck = await pool.request()
            .input('CategoryCode', mssql.VarChar, CategoryCode.trim())
            .query("SELECT TOP 1 CategoryCode FROM tblCategory WHERE RTRIM(LTRIM(CategoryCode)) = RTRIM(LTRIM(@CategoryCode))");
        if (catCheck.recordset.length === 0) {
            return res.status(400).json({ success: false, message: "Pleae enter valid CatagoryCode." });
        }

        // Check if exists
        const checkResult = await pool.request()
            .input('CatalogueCode', mssql.VarChar, CatalogueCode.trim())
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
            .input('CatalogueCode', mssql.VarChar, CatalogueCode.trim())
            .input('CatalogueName', mssql.VarChar, CatalogueName.trim())
            .input('CategoryCode', mssql.VarChar, CategoryCode.trim())
            .input('Remarks', mssql.VarChar, (Remarks || '').trim())
            .input('IsActive', mssql.Bit, IsActive !== false ? 1 : 0)
            .query(query);

        res.json({ success: true, message: "Catalogue detail saved sucssfully." });
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
        
        // Validation matching subValidateMe in frmCatalogue.vb
        if (!CatalogueCode || !CatalogueCode.trim()) {
            return res.status(400).json({ success: false, message: "CatalogueCode should not be blank." });
        }
        if (!CatalogueName || !CatalogueName.trim()) {
            return res.status(400).json({ success: false, message: "CatalogueName should not be blank." });
        }
        if (!CategoryCode || !CategoryCode.trim()) {
            return res.status(400).json({ success: false, message: "CategoryCode should not be blank." });
        }

        // Validate CategoryCode existence matching objClsCategory.GetCategoryName
        const catCheck = await pool.request()
            .input('CategoryCode', mssql.VarChar, CategoryCode.trim())
            .query("SELECT TOP 1 CategoryCode FROM tblCategory WHERE RTRIM(LTRIM(CategoryCode)) = RTRIM(LTRIM(@CategoryCode))");
        if (catCheck.recordset.length === 0) {
            return res.status(400).json({ success: false, message: "Pleae enter valid CatagoryCode." });
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
            .input('CatalogueCode', mssql.VarChar, CatalogueCode.trim())
            .input('CatalogueName', mssql.VarChar, CatalogueName.trim())
            .input('CategoryCode', mssql.VarChar, CategoryCode.trim())
            .input('Remarks', mssql.VarChar, (Remarks || '').trim())
            .input('IsActive', mssql.Bit, IsActive !== false ? 1 : 0)
            .query(query);

        res.json({ success: true, message: "Catalogue detail saved sucssfully." });
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
        if (!id || !id.trim()) {
            return res.status(400).json({ success: false, message: "ID parameter is required" });
        }

        // Check if catalogue has inventory matching fncGetCatalogueTran in frmCatalogue.vb
        const invCheck = await pool.request()
            .input('CatalogueCode', mssql.VarChar, id.trim())
            .query("SELECT TOP 1 InventoryCode FROM tblInventory WHERE CatalogueCode = @CatalogueCode");

        if (invCheck.recordset.length > 0) {
            return res.status(400).json({
                success: false,
                message: "Can Not Delete Catalogue , Beacuse This Catalogue Is Having Some Inventory"
            });
        }

        await pool.request()
            .input('CatalogueCode', mssql.VarChar, id.trim())
            .query("DELETE FROM tblCatalogue WHERE CatalogueCode = @CatalogueCode");
        res.json({ success: true, message: "Catalogue detail deleted." });
    } catch (error) {
        console.error("Error deleting catalogue:", error);
        res.status(500).json({ success: false, message: "Server error", error: error.message });
    }
});

module.exports = router;
