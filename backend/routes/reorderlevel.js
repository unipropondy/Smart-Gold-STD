const express = require("express");
const router = express.Router();
const { sql } = require("../db");

// GET all ReOrder Levels
router.get("/", async (req, res) => {
  try {
    const result = await req.pool.request().query(`
      SELECT 
        LocationCode,
        CategoryCode,
        CatalogueCode,
        Weight,
        ToWeight,
        SizeCode,
        ToSizeCode,
        ReOrderLevel,
        Remarks,
        SizeType,
        ToSizeType
      FROM tblReOrderLevelMaster
    `);
    res.json({ success: true, data: result.recordset });
  } catch (error) {
    console.error("Fetch ReOrder Level Error:", error);
    res.status(500).json({ success: false, message: "Database Error", error: error.message });
  }
});

// POST (Create) a ReOrder Level
router.post("/", async (req, res) => {
  try {
    const {
      LocationCode,
      CategoryCode,
      CatalogueCode,
      Weight,
      ToWeight,
      SizeCode,
      ToSizeCode,
      ReOrderLevel,
      Remarks
    } = req.body;

    const trimmedCatalogue = (CatalogueCode || '').trim();
    const trimmedCategory = (CategoryCode || '').trim();
    const trimmedFromWeight = (Weight !== undefined && Weight !== null) ? String(Weight).trim() : '';
    const trimmedToWeight = (ToWeight !== undefined && ToWeight !== null) ? String(ToWeight).trim() : '';

    if (!trimmedCatalogue) {
      return res.status(400).json({ success: false, message: "CatalogueCode should not be blank." });
    }

    if (!trimmedFromWeight) {
      return res.status(400).json({ success: false, message: "Weight should not be blank." });
    }

    if (!trimmedToWeight) {
      return res.status(400).json({ success: false, message: "Weight should not be blank." });
    }

    if (!trimmedCategory) {
      return res.status(400).json({ success: false, message: "CategoryCode should not be blank." });
    }

    const query = `
      INSERT INTO tblReOrderLevelMaster (
        LocationCode, 
        CategoryCode, 
        CatalogueCode, 
        Weight, 
        ToWeight, 
        SizeCode, 
        ToSizeCode, 
        ReOrderLevel, 
        Remarks,
        SizeType,
        ToSizeType,
        CreateUser,
        CreateDate,
        ModifyUser,
        ModifyDate
      )
      VALUES (
        @LocationCode, 
        @CategoryCode, 
        @CatalogueCode, 
        @Weight, 
        @ToWeight, 
        @SizeCode, 
        @ToSizeCode, 
        @ReOrderLevel, 
        @Remarks,
        NULL,
        NULL,
        'sys',
        GETDATE(),
        'sys',
        GETDATE()
      )
    `;

    await req.pool.request()
      .input("LocationCode", sql.NVarChar, LocationCode ? LocationCode.trim() : null)
      .input("CategoryCode", sql.NVarChar, trimmedCategory)
      .input("CatalogueCode", sql.NVarChar, trimmedCatalogue)
      .input("Weight", sql.Decimal(18, 3), trimmedFromWeight ? parseFloat(trimmedFromWeight) : 0)
      .input("ToWeight", sql.Decimal(18, 3), trimmedToWeight ? parseFloat(trimmedToWeight) : 0)
      .input("SizeCode", sql.NVarChar, SizeCode ? SizeCode.trim() : null)
      .input("ToSizeCode", sql.NVarChar, ToSizeCode ? ToSizeCode.trim() : null)
      .input("ReOrderLevel", sql.Decimal(18, 2), ReOrderLevel ? parseFloat(ReOrderLevel) : 0)
      .input("Remarks", sql.NVarChar, Remarks ? Remarks.trim() : null)
      .query(query);

    res.json({ success: true, message: "ReOrder detail saved sucssfully." });
  } catch (error) {
    console.error("Add ReOrder Level Error:", error);
    res.status(500).json({ success: false, message: "Error in Save", error: error.message });
  }
});

// PUT (Update) a ReOrder Level
router.put("/", async (req, res) => {
  try {
    const {
      OriginalLocationCode,
      OriginalCategoryCode,
      OriginalCatalogueCode,
      LocationCode,
      CategoryCode,
      CatalogueCode,
      Weight,
      ToWeight,
      SizeCode,
      ToSizeCode,
      ReOrderLevel,
      Remarks
    } = req.body;

    const trimmedCatalogue = (CatalogueCode || '').trim();
    const trimmedCategory = (CategoryCode || '').trim();
    const trimmedFromWeight = (Weight !== undefined && Weight !== null) ? String(Weight).trim() : '';
    const trimmedToWeight = (ToWeight !== undefined && ToWeight !== null) ? String(ToWeight).trim() : '';

    if (!trimmedCatalogue) {
      return res.status(400).json({ success: false, message: "CatalogueCode should not be blank." });
    }

    if (!trimmedFromWeight) {
      return res.status(400).json({ success: false, message: "Weight should not be blank." });
    }

    if (!trimmedToWeight) {
      return res.status(400).json({ success: false, message: "Weight should not be blank." });
    }

    if (!trimmedCategory) {
      return res.status(400).json({ success: false, message: "CategoryCode should not be blank." });
    }

    const origLocation = OriginalLocationCode !== undefined ? (OriginalLocationCode || '').trim() : (LocationCode || '').trim();
    const origCategory = OriginalCategoryCode !== undefined ? (OriginalCategoryCode || '').trim() : trimmedCategory;
    const origCatalogue = OriginalCatalogueCode !== undefined ? (OriginalCatalogueCode || '').trim() : trimmedCatalogue;

    const query = `
      UPDATE tblReOrderLevelMaster
      SET 
        LocationCode = @LocationCode,
        CategoryCode = @CategoryCode,
        CatalogueCode = @CatalogueCode,
        Weight = @Weight,
        ToWeight = @ToWeight,
        SizeCode = @SizeCode,
        ToSizeCode = @ToSizeCode,
        ReOrderLevel = @ReOrderLevel,
        Remarks = @Remarks,
        ModifyUser = 'sys',
        ModifyDate = GETDATE()
      WHERE 
        ISNULL(LocationCode, '') = @OrigLocationCode 
        AND ISNULL(CategoryCode, '') = @OrigCategoryCode 
        AND ISNULL(CatalogueCode, '') = @OrigCatalogueCode
    `;

    const result = await req.pool.request()
      .input("OrigLocationCode", sql.NVarChar, origLocation)
      .input("OrigCategoryCode", sql.NVarChar, origCategory)
      .input("OrigCatalogueCode", sql.NVarChar, origCatalogue)
      .input("LocationCode", sql.NVarChar, LocationCode ? LocationCode.trim() : null)
      .input("CategoryCode", sql.NVarChar, trimmedCategory)
      .input("CatalogueCode", sql.NVarChar, trimmedCatalogue)
      .input("Weight", sql.Decimal(18, 3), trimmedFromWeight ? parseFloat(trimmedFromWeight) : 0)
      .input("ToWeight", sql.Decimal(18, 3), trimmedToWeight ? parseFloat(trimmedToWeight) : 0)
      .input("SizeCode", sql.NVarChar, SizeCode ? SizeCode.trim() : null)
      .input("ToSizeCode", sql.NVarChar, ToSizeCode ? ToSizeCode.trim() : null)
      .input("ReOrderLevel", sql.Decimal(18, 2), ReOrderLevel ? parseFloat(ReOrderLevel) : 0)
      .input("Remarks", sql.NVarChar, Remarks ? Remarks.trim() : null)
      .query(query);

    if (result.rowsAffected[0] === 0) {
      return res.status(404).json({ success: false, message: "Error in Save" });
    }

    res.json({ success: true, message: "ReOrder detail saved sucssfully." });
  } catch (error) {
    console.error("Update ReOrder Level Error:", error);
    res.status(500).json({ success: false, message: "Error in Save", error: error.message });
  }
});

// DELETE a ReOrder Level
router.delete("/", async (req, res) => {
  try {
    const { LocationCode, CategoryCode, CatalogueCode } = req.query;

    const query = `
      DELETE FROM tblReOrderLevelMaster 
      WHERE LocationCode = @LocationCode 
        AND CategoryCode = @CategoryCode 
        AND CatalogueCode = @CatalogueCode
    `;

    const result = await req.pool.request()
      .input("LocationCode", sql.NVarChar, LocationCode || "")
      .input("CategoryCode", sql.NVarChar, CategoryCode || "")
      .input("CatalogueCode", sql.NVarChar, CatalogueCode || "")
      .query(query);

    if (result.rowsAffected[0] === 0) {
      return res.status(404).json({ success: false, message: "Error in deleting Catalogue detail." });
    }

    res.json({ success: true, message: "Catalogue detail deleted." });
  } catch (error) {
    console.error("Delete ReOrder Level Error:", error);
    res.status(500).json({ success: false, message: "Error in deleting Catalogue detail.", error: error.message });
  }
});

module.exports = router;
