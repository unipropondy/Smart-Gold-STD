const express = require("express");
const router = express.Router();
const { sql } = require("../db");

// GET /api/transferstatus/locations
// Fetch locations for LOV
router.get("/locations", async (req, res) => {
  try {
    const pool = req.pool;
    const query = `
      SELECT RTRIM(LocationCode) as LocationCode, RTRIM(LocationName) as LocationName 
      FROM tblLocation 
      ORDER BY SortCode
    `;
    const result = await pool.request().query(query);
    res.json({ success: true, data: result.recordset });
  } catch (error) {
    console.error("Error fetching locations:", error);
    res.status(500).json({ success: false, message: "Error fetching locations: " + error.message });
  }
});

// GET /api/transferstatus/fetch
// Fetch transfers based on location, date, and type
router.get("/fetch", async (req, res) => {
  try {
    const { location, transferDate, type } = req.query;
    
    if (!location || !transferDate || !type) {
      return res.status(400).json({ success: false, message: "Location, TransferDate, and Type are required." });
    }

    const pool = req.pool;
    let query = "";

    if (type === "out") {
      query = `
        SELECT 
          TransferNo, 
          TransferDate, 
          FromLocStatus, 
          ToLocStatus, 
          TotalPCS, 
          ToLocation as Location 
        FROM tblInventoryTransferHeader 
        WHERE FromLocation = @Location 
        AND TransferDate = @TransferDate
      `;
    } else if (type === "in") {
      query = `
        SELECT 
          TransferNo, 
          TransferDate, 
          FromLocStatus, 
          ToLocStatus, 
          TotalPCS, 
          FromLocation as Location 
        FROM tblInventoryTransferHeader 
        WHERE ToLocation = @Location 
        AND TransferDate = @TransferDate
      `;
    } else {
      return res.status(400).json({ success: false, message: "Invalid type. Must be 'in' or 'out'." });
    }

    const result = await pool.request()
      .input("Location", sql.VarChar, location.trim())
      .input("TransferDate", sql.Date, transferDate)
      .query(query);

    res.json({ success: true, data: result.recordset });
  } catch (error) {
    console.error("Error fetching transfers:", error);
    res.status(500).json({ success: false, message: "Error fetching transfers: " + error.message });
  }
});

// GET /api/transferstatus/pending
// Fetch pending transfers based on location and type
router.get("/pending", async (req, res) => {
  try {
    const { location, type } = req.query;
    
    if (!location || !type) {
      return res.status(400).json({ success: false, message: "Location and Type are required." });
    }

    const pool = req.pool;
    let query = "";

    if (type === "out") {
      query = `
        SELECT 
          TransferNo, 
          TransferDate, 
          FromLocStatus, 
          ToLocStatus, 
          TotalPCS, 
          ToLocation as Location 
        FROM tblInventoryTransferHeader 
        WHERE FromLocation = @Location 
        AND TransferDate >= '1900-01-01' 
        AND ToLocStatus = 0
      `;
    } else if (type === "in") {
      query = `
        SELECT 
          TransferNo, 
          TransferDate, 
          FromLocStatus, 
          ToLocStatus, 
          TotalPCS, 
          FromLocation as Location 
        FROM tblInventoryTransferHeader 
        WHERE ToLocation = @Location 
        AND ToLocStatus = 0
      `;
    } else {
      return res.status(400).json({ success: false, message: "Invalid type. Must be 'in' or 'out'." });
    }

    const result = await pool.request()
      .input("Location", sql.VarChar, location.trim())
      .query(query);

    res.json({ success: true, data: result.recordset });
  } catch (error) {
    console.error("Error fetching pending transfers:", error);
    res.status(500).json({ success: false, message: "Error fetching pending transfers: " + error.message });
  }
});

module.exports = router;
