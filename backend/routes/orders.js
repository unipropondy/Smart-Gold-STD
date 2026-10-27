const express = require("express");
const { sql } = require("../db");
const router = express.Router();

// ================= KDS TODAY =================
router.get("/kds-today", async (req, res) => {
  try {
    const pool = req.pool;

    const result = await pool.request().query(`
      SELECT 
        o.OrderId,
        o.OrderNumber,
        o.Tableno,
        o.OrderDateTime,
        o.IsTakeAway,

        d.OrderDetailId,
        d.DishName,
        d.Quantity,
        d.ModifierDescription,
        d.Remarks,
        d.isReady,
        d.isDelivered

      FROM RestaurantOrderCur o
      INNER JOIN RestaurantOrderDetailCur d
        ON o.OrderId = d.OrderId

      WHERE
        ISNULL(d.isReady, 0) = 0
        AND CAST(o.OrderDateTime AS DATE) = CAST(GETDATE() AS DATE)

      ORDER BY o.OrderDateTime ASC
    `);

    return res.status(200).json({
      success: true,
      data: result.recordset
    });

  } catch (err) {
    console.error("🔥 KDS ERROR:", err);

    return res.status(500).json({
      success: false,
      message: "Server Error",
      error: err.message
    });
  }
});

// ================= MARK READY =================
router.post("/mark-ready", async (req, res) => {
  try {
    const { orderId } = req.body;

    // ✅ Validation
    if (!orderId) {
      return res.status(400).json({
        success: false,
        message: "orderId is required"
      });
    }

    const pool = req.pool;

    const result = await pool.request()
      .input("orderId", sql.UniqueIdentifier, orderId)
      .query(`
        UPDATE RestaurantOrderDetailCur
        SET isReady = 1
        WHERE OrderId = @orderId
      `);

    return res.status(200).json({
      success: true,
      message: "Order marked as ready",
      rowsAffected: result.rowsAffected[0]
    });

  } catch (err) {
    console.error("🔥 MARK READY ERROR:", err);

    return res.status(500).json({
      success: false,
      message: "Database update failed",
      error: err.message
    });
  }
});

// ================= CDS TODAY =================
router.get("/cds-today", async (req, res) => {
  try {

    const pool = req.pool;

    const result = await pool.request().query(`
    SELECT 
    o.OrderId,
    RIGHT(o.OrderNumber, 4) AS OrderNumber,
    o.Tableno,
    o.OrderDateTime,

    CASE 
        WHEN d.StatusCode = '2' THEN 'PREPARING'
        WHEN d.StatusCode = '3' THEN 'READY'
        ELSE 'UNKNOWN'
    END AS StatusLabel

FROM RestaurantOrderCur o
INNER JOIN RestaurantOrderDetailCur d 
    ON o.OrderId = d.OrderId

WHERE ISNULL(d.isDelivered, 0) = 0
    AND d.StatusCode IN ('2', '3')
    -- AND o.entry_status = 'q'
    -- AND CAST(o.OrderDateTime AS DATE) = CAST(GETDATE() AS DATE)
GROUP BY 
    o.OrderId,
    o.OrderNumber,
    o.Tableno,
    o.OrderDateTime,
    d.StatusCode
ORDER BY o.OrderDateTime ASC;
    `);

    res.json(result.recordset);

  } catch (err) {

    console.error("🔥 CDS ERROR:", err);

    res.status(500).json({
      success: false,
      error: err.message
    });
  }
});

// ================= CDS SELF =================
router.get("/cds-self", async (req, res) => {
  try {

    const pool = req.pool;

    const result = await pool.request().query(`
     SELECT 
    o.OrderId,
    RIGHT(o.OrderNumber, 4) AS OrderNumber,
    o.Tableno,
    o.OrderDateTime,

    CASE 
        WHEN d.StatusCode = '2' THEN 'PREPARING'
        WHEN d.StatusCode = '3' THEN 'READY'
        ELSE 'UNKNOWN'
    END AS StatusLabel

FROM RestaurantOrderCur o
INNER JOIN RestaurantOrderDetailCur d 
    ON o.OrderId = d.OrderId

WHERE ISNULL(d.isDelivered, 0) = 0
    AND d.StatusCode IN ('2', '3')
     AND o.entry_status = 'q'
    -- AND CAST(o.OrderDateTime AS DATE) = CAST(GETDATE() AS DATE)
GROUP BY 
    o.OrderId,
    o.OrderNumber,
    o.Tableno,
    o.OrderDateTime,
    d.StatusCode
ORDER BY o.OrderDateTime ASC;
    `);

    res.json(result.recordset);

  } catch (err) {

    console.error("🔥 CDS SELF ERROR:", err);

    res.status(500).json({
      success: false,
      error: err.message
    });
  }
});

module.exports = router;
