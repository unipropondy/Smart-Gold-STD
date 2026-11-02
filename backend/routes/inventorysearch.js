/**
 * Developer: durairaj
 */

const express = require('express');
const router = express.Router();
const { poolPromise, sql } = require('../db');

// Route to fetch inventory search list matching VB6 FetchInventoryDetails()
router.get('/', async (req, res) => {
  try {
    const {
      category,
      CategoryCode,
      catalogue,
      CatalogueCode,
      fromWeight,
      FromWeight,
      toWeight,
      ToWeight,
      inventoryCode,
      InventoryCode,
      Description,
      SupplierCode,
      StockStatus,
    } = req.query;

    const pool = req.pool || (await poolPromise);

    let query = `
      SELECT b.Locationcode,
             a.InventoryCode,
             a.Description,
             CASE a.Weight WHEN 0 THEN a.StoneWeight ELSE a.Weight END AS Weight,
             a.SizeCode,
             a.SizeType,
             CASE b.Status WHEN 'IN' THEN 'IN' ELSE '' END AS [Status],
             b.QtyOnHand,
             b.QtySold
      FROM tblInventory a
      INNER JOIN tblInventoryStock b ON a.InventoryCode = b.InventoryCode
      WHERE 1=1
    `;

    const request = pool.request();

    const catVal = (category || CategoryCode || '').trim();
    if (catVal) {
      query += ` AND a.CategoryCode = @CategoryCode`;
      request.input('CategoryCode', sql.VarChar, catVal);
    }

    const catLogVal = (catalogue || CatalogueCode || '').trim();
    if (catLogVal) {
      query += ` AND a.CatalogueCode = @CatalogueCode`;
      request.input('CatalogueCode', sql.VarChar, catLogVal);
    }

    const fWeight = parseFloat(fromWeight || FromWeight || 0);
    const tWeight = parseFloat(toWeight || ToWeight || 0);
    if (!isNaN(fWeight) && !isNaN(tWeight) && fWeight > 0 && tWeight > 0) {
      query += ` AND a.Weight >= @FromWeight AND a.Weight <= @ToWeight`;
      request.input('FromWeight', sql.Float, fWeight);
      request.input('ToWeight', sql.Float, tWeight);
    }

    const invVal = (inventoryCode || InventoryCode || '').trim();
    if (invVal) {
      query += ` AND a.InventoryCode = @InventoryCode`;
      request.input('InventoryCode', sql.VarChar, invVal);
    }

    if (Description && Description.trim()) {
      query += ` AND a.Description LIKE @Description`;
      request.input('Description', sql.VarChar, `%${Description.trim()}%`);
    }

    if (SupplierCode && SupplierCode.trim()) {
      query += ` AND a.SupplierCode = @SupplierCode`;
      request.input('SupplierCode', sql.VarChar, SupplierCode.trim());
    }

    if (StockStatus === 'Sold') {
      query += ` AND b.QtySold > 0`;
    } else {
      query += ` AND b.QtyOnHand > 0`;
    }

    query += ` ORDER BY b.Locationcode ASC, a.InventoryCode ASC`;

    let result;
    try {
      result = await request.query(query);
    } catch (joinErr) {
      // Safe fallback directly to tblInventory if tblInventoryStock is missing in database
      let fallbackQuery = `
        SELECT a.LocationCode as Locationcode,
               a.InventoryCode,
               a.Description,
               CASE a.Weight WHEN 0 THEN a.StoneWeight ELSE a.Weight END AS Weight,
               a.SizeCode,
               a.SizeType,
               'IN' AS [Status],
               ISNULL(a.QtyOnHand, 1) as QtyOnHand,
               0 as QtySold
        FROM tblInventory a
        WHERE 1=1
      `;
      const fallbackReq = pool.request();
      if (catVal) {
        fallbackQuery += ` AND a.CategoryCode = @CategoryCode`;
        fallbackReq.input('CategoryCode', sql.VarChar, catVal);
      }
      if (catLogVal) {
        fallbackQuery += ` AND a.CatalogueCode = @CatalogueCode`;
        fallbackReq.input('CatalogueCode', sql.VarChar, catLogVal);
      }
      if (!isNaN(fWeight) && !isNaN(tWeight) && fWeight > 0 && tWeight > 0) {
        fallbackQuery += ` AND a.Weight >= @FromWeight AND a.Weight <= @ToWeight`;
        fallbackReq.input('FromWeight', sql.Float, fWeight);
        fallbackReq.input('ToWeight', sql.Float, tWeight);
      }
      if (invVal) {
        fallbackQuery += ` AND a.InventoryCode = @InventoryCode`;
        fallbackReq.input('InventoryCode', sql.VarChar, invVal);
      }
      fallbackQuery += ` ORDER BY a.InventoryCode ASC`;
      result = await fallbackReq.query(fallbackQuery);
    }

    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error('Error fetching inventory search list:', err);
    res.status(500).json({ success: false, message: 'Server error', error: err.message });
  }
});

// Category LOV matching btnCatagorySearch_Click
router.get('/lov/category', async (req, res) => {
  try {
    const pool = req.pool || (await poolPromise);
    const result = await pool.request().query(`
      SELECT CategoryCode, CategoryName, ClassCode, Remarks 
      FROM tblCategory 
      ORDER BY CategoryCode ASC
    `);
    res.json({ success: true, data: result.recordset });
  } catch (error) {
    console.error("Error fetching category LOV:", error);
    res.status(500).json({ success: false, message: "Server error", error: error.message });
  }
});

// Catalogue LOV matching btnCatalogueSearch_Click
router.get('/lov/catalogue', async (req, res) => {
  try {
    const pool = req.pool || (await poolPromise);
    const { category } = req.query;
    let query = `
      SELECT CatalogueCode, CatalogueName, CategoryCode, GoldLoss 
      FROM tblCatalogue
    `;
    const request = pool.request();
    if (category && category.trim() !== '') {
      query += " WHERE CategoryCode = @category";
      request.input('category', sql.VarChar, category.trim());
    }
    query += " ORDER BY CatalogueName ASC";

    const result = await request.query(query);
    res.json({ success: true, data: result.recordset });
  } catch (error) {
    console.error("Error fetching catalogue LOV:", error);
    res.status(500).json({ success: false, message: "Server error", error: error.message });
  }
});

// Route to fetch detailed inventory info
router.get('/:inventoryCode', async (req, res) => {
  try {
    const { inventoryCode } = req.params;
    const pool = req.pool || (await poolPromise);

    const invQuery = `
      SELECT a.*, b.QtyOnHand, b.QtySold, b.Status as StockStatus
      FROM tblInventory a
      LEFT JOIN tblInventoryStock b ON a.InventoryCode = b.InventoryCode
      WHERE a.InventoryCode = @InventoryCode
    `;
    
    let invResult;
    try {
      invResult = await pool.request()
        .input('InventoryCode', sql.VarChar, inventoryCode)
        .query(invQuery);
    } catch (e) {
      invResult = await pool.request()
        .input('InventoryCode', sql.VarChar, inventoryCode)
        .query(`SELECT * FROM tblInventory WHERE InventoryCode = @InventoryCode`);
    }
    
    if (invResult.recordset.length === 0) {
      return res.status(404).json({ success: false, error: 'Inventory not found' });
    }

    const inventoryDetails = invResult.recordset[0];

    let addlDesc = null;
    try {
      const addlResult = await pool.request()
        .input('invCode', sql.VarChar, inventoryCode)
        .query('SELECT TOP 1 * FROM tblInventoryAddlDescription WHERE InventoryCode = @invCode');
      if (addlResult.recordset && addlResult.recordset.length > 0) {
        addlDesc = addlResult.recordset[0];
      }
    } catch (e) {}

    let splitHistory = null;
    try {
      const spResult = await pool.request()
        .input('invCode', sql.VarChar, inventoryCode)
        .query(`
          SELECT TOP 1 
            InventoryCode, Description, OriginalWeight, OriginalPMC, OriginalSMC, OriginalGoldLoss, OriginalTotalCost,
            Weight1, PMC1, SMC1, GoldLoss1, TotalCost1,
            DestInventoryCode, DestDescription, Weight2, PMC2, SMC2, GoldLoss2, TotalCost2,
            UserID, SplitDate, Locationcode, InventoryCode1
          FROM tblSplitHistory
          WHERE InventoryCode = @invCode OR InventoryCode1 = @invCode OR DestInventoryCode = @invCode
          ORDER BY SplitDate DESC
        `);
      if (spResult.recordset && spResult.recordset.length > 0) {
        splitHistory = spResult.recordset[0];
      }
    } catch (e) {}

    let mergeHistory = null;
    try {
      const meResult = await pool.request()
        .input('invCode', sql.VarChar, inventoryCode)
        .query(`
          SELECT TOP 1 
            InventoryCode1, Weight1, PMC1, SMC1, GoldLoss1, TotalCost1,
            InventoryCode2, Weight2, PMC2, SMC2, GoldLoss2, TotalCost2,
            InventoryCodeN, WeightN, PMCN, SMCN, GoldLossN, TotalCostN, DescriptionNew,
            JobSheetNo, ClassCode, UserID, LocationCode, MergedDate
          FROM tblMergeHistory
          WHERE InventoryCode1 = @invCode OR InventoryCode2 = @invCode OR InventoryCodeN = @invCode
          ORDER BY MergedDate DESC
        `);
      if (meResult.recordset && meResult.recordset.length > 0) {
        mergeHistory = meResult.recordset[0];
      }
    } catch (e) {}

    res.json({
      success: true,
      Inventory: inventoryDetails,
      JobSheetDate: null,
      SalesManName: null,
      SalesReturn: null,
      PurchaseReturn: null,
      SplitHistory: splitHistory,
      MergeHistory: mergeHistory,
      AdditionalDesc: addlDesc
    });
  } catch (err) {
    console.error('Error fetching inventory details:', err);
    res.status(500).json({ success: false, error: 'Internal Server Error', message: err.message });
  }
});

module.exports = router;
