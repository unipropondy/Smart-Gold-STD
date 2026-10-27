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
      .input("LocationCode", sql.NVarChar, LocationCode || null)
      .input("CategoryCode", sql.NVarChar, CategoryCode || null)
      .input("CatalogueCode", sql.NVarChar, CatalogueCode || null)
      .input("Weight", sql.Decimal(18, 3), Weight ? parseFloat(Weight) : 0)
      .input("ToWeight", sql.Decimal(18, 3), ToWeight ? parseFloat(ToWeight) : 0)
      .input("SizeCode", sql.NVarChar, SizeCode || null)
      .input("ToSizeCode", sql.NVarChar, ToSizeCode || null)
      .input("ReOrderLevel", sql.Decimal(18, 2), ReOrderLevel ? parseFloat(ReOrderLevel) : 0)
      .input("Remarks", sql.NVarChar, Remarks || null)
      .query(query);

    res.json({ success: true, message: "ReOrder Level added successfully" });
  } catch (error) {
    console.error("Add ReOrder Level Error:", error);
    res.status(500).json({ success: false, message: "Database Error", error: error.message });
  }
});

// PUT (Update) a ReOrder Level
router.put("/", async (req, res) => {
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

    const query = `
      UPDATE tblReOrderLevelMaster
      SET 
        Weight = @Weight,
        ToWeight = @ToWeight,
        SizeCode = @SizeCode,
        ToSizeCode = @ToSizeCode,
        ReOrderLevel = @ReOrderLevel,
        Remarks = @Remarks,
        ModifyUser = 'sys',
        ModifyDate = GETDATE()
      WHERE 
        LocationCode = @LocationCode 
        AND CategoryCode = @CategoryCode 
        AND CatalogueCode = @CatalogueCode
    `;

    await req.pool.request()
      .input("LocationCode", sql.NVarChar, LocationCode || null)
      .input("CategoryCode", sql.NVarChar, CategoryCode || null)
      .input("CatalogueCode", sql.NVarChar, CatalogueCode || null)
      .input("Weight", sql.Decimal(18, 3), Weight ? parseFloat(Weight) : 0)
      .input("ToWeight", sql.Decimal(18, 3), ToWeight ? parseFloat(ToWeight) : 0)
      .input("SizeCode", sql.NVarChar, SizeCode || null)
      .input("ToSizeCode", sql.NVarChar, ToSizeCode || null)
      .input("ReOrderLevel", sql.Decimal(18, 2), ReOrderLevel ? parseFloat(ReOrderLevel) : 0)
      .input("Remarks", sql.NVarChar, Remarks || null)
      .query(query);

    res.json({ success: true, message: "ReOrder Level updated successfully" });
  } catch (error) {
    console.error("Update ReOrder Level Error:", error);
    res.status(500).json({ success: false, message: "Database Error", error: error.message });
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

    await req.pool.request()
      .input("LocationCode", sql.NVarChar, LocationCode || "")
      .input("CategoryCode", sql.NVarChar, CategoryCode || "")
      .input("CatalogueCode", sql.NVarChar, CatalogueCode || "")
      .query(query);

    res.json({ success: true, message: "ReOrder Level deleted successfully" });
  } catch (error) {
    console.error("Delete ReOrder Level Error:", error);
    res.status(500).json({ success: false, message: "Database Error", error: error.message });
  }
});

module.exports = router;
