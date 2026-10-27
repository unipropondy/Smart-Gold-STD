const express = require("express");
const router = express.Router();
const { sql } = require("../db");

// GET all TaelPercent
router.get("/", async (req, res) => {
  try {
    const result = await req.pool.request().query(`
      SELECT 
        TaelPercent,
        TaelPercent1,
        TaelPercent2,
        TaelPercent3
      FROM TblTaelPercent
    `);
    res.json({ success: true, data: result.recordset });
  } catch (error) {
    console.error("Fetch TaelPercent Error:", error);
    res.status(500).json({ success: false, message: "Database Error", error: error.message });
  }
});

module.exports = router;
