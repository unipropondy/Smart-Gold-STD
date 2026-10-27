const express = require('express');
const router = express.Router();
const { poolPromise, sql } = require('../db');



// Route to fetch inventory search list
router.get('/', async (req, res) => {
  try {
    const {
      Description,
      Weight,
      CatalogueCode,
      SupplierCode,
      StockStatus,
      // Reference parameters
      category,
      CategoryCode,
      catalogue,
      fromWeight,
      FromWeight,
      toWeight,
      ToWeight,
      inventoryCode,
      InventoryCode,
    } = req.query;

    const pool = req.pool || (await poolPromise);
    let query = `
      SELECT b.Locationcode,
             a.InventoryCode,
             a.Description,
             CASE a.Weight WHEN 0 THEN a.StoneWeight ELSE a.Weight END AS Weight,
             a.SizeCode,
             a.SizeType,
             CASE Status WHEN 'IN' THEN 'IN' ELSE '' END AS [Status],
             b.QtyOnHand,
             b.QtySold
      FROM tblInventory a
      INNER JOIN tblInventoryStock b ON a.InventoryCode = b.InventoryCode
      WHERE 1=1
    `;

    const request = pool.request();

    // Category filter
    const catVal = (category || CategoryCode || '').trim();
    if (catVal) {
      query += ` AND a.CategoryCode = @CategoryCode`;
      request.input('CategoryCode', sql.NVarChar, catVal);
    }

    // Catalogue filter
    const catLogVal = (catalogue || CatalogueCode || '').trim();
    if (catLogVal) {
      query += ` AND a.CatalogueCode = @CatalogueCode`;
      request.input('CatalogueCode', sql.NVarChar, catLogVal);
    }

    // From Weight & To Weight range (matching VB6: Val(txtFromWeight.Text) > 0 And Val(txtToWeight.Text) > 0)
    const fWeight = parseFloat(fromWeight || FromWeight || 0);
    const tWeight = parseFloat(toWeight || ToWeight || 0);
    if (!isNaN(fWeight) && !isNaN(tWeight) && fWeight > 0 && tWeight > 0) {
      query += ` AND a.Weight >= @FromWeight AND a.Weight <= @ToWeight`;
      request.input('FromWeight', sql.Float, fWeight);
      request.input('ToWeight', sql.Float, tWeight);
    }

    // InventoryCode filter
    const invVal = (inventoryCode || InventoryCode || '').trim();
    if (invVal) {
      query += ` AND a.InventoryCode = @InventoryCode`;
      request.input('InventoryCode', sql.NVarChar, invVal);
    }

    // Additional optional filters for backwards compatibility
    if (Description && Description.trim()) {
      query += ` AND a.Description LIKE @Description`;
      request.input('Description', sql.NVarChar, `%${Description.trim()}%`);
    }

    if (Weight && isNaN(fWeight) && isNaN(tWeight)) {
      const parsedWeight = parseFloat(Weight);
      if (!isNaN(parsedWeight)) {
        query += ` AND a.Weight BETWEEN @WeightMin AND @WeightMax`;
        request.input('WeightMin', sql.Float, parsedWeight - 0.5);
        request.input('WeightMax', sql.Float, parsedWeight + 0.5);
      }
    }

    if (SupplierCode && SupplierCode.trim()) {
      query += ` AND a.SupplierCode = @SupplierCode`;
      request.input('SupplierCode', sql.NVarChar, SupplierCode.trim());
    }

    if (StockStatus === 'Sold') {
      query += ` AND b.QtySold > 0`;
    } else {
      // Default as in VB6 reference: and b.qtyonhand > 0
      query += ` AND b.QtyOnHand > 0`;
    }

    query += ` ORDER BY b.Locationcode`;

    const result = await request.query(query);
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error('Error fetching inventory search list:', err);
    res.status(500).json({ success: false, error: err.message || 'Internal Server Error' });
  }
});

// Route to fetch inventory details
router.get('/:inventoryCode', async (req, res) => {
  try {
    const { inventoryCode } = req.params;
    const pool = await poolPromise;

    // Fetch primary inventory details (combining info from tblInventory and tblInventoryStock)
    const invQuery = `
      SELECT 
        a.*, b.QtyOnHand, b.QtySold
      FROM tblInventory a
      LEFT JOIN tblInventoryStock b ON a.InventoryCode = b.InventoryCode AND a.LocationCode = b.LocationCode
      WHERE a.InventoryCode = @InventoryCode
    `;
    
    const request = pool.request();
    request.input('InventoryCode', sql.NVarChar, inventoryCode);
    
    const invResult = await request.query(invQuery);
    
    if (invResult.recordset.length === 0) {
      return res.status(404).json({ error: 'Inventory not found' });
    }

    const inventoryDetails = invResult.recordset[0];

    // Safely query Additional Description
    let addlDesc = null;
    try {
      const addlResult = await pool.request()
        .input('invCode', sql.NVarChar, inventoryCode)
        .query('SELECT TOP 1 * FROM tblInventoryAddlDescription WHERE InventoryCode = @invCode');
      if (addlResult.recordset && addlResult.recordset.length > 0) {
        addlDesc = addlResult.recordset[0];
      }
    } catch (e) {
      console.warn('tblInventoryAddlDescription query error or table not found:', e.message);
    }

    // Safely query Split History
    let splitHistory = null;
    try {
      const spResult = await pool.request()
        .input('invCode', sql.NVarChar, inventoryCode)
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
    } catch (e) {
      console.warn('tblSplitHistory query error:', e.message);
    }

    // Safely query Merge History
    let mergeHistory = null;
    try {
      const meResult = await pool.request()
        .input('invCode', sql.NVarChar, inventoryCode)
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
    } catch (e) {
      console.warn('tblMergeHistory query error:', e.message);
    }

    res.json({
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
    res.status(500).json({ error: 'Internal Server Error' });
  }
});

module.exports = router;
