const express = require("express");
const router = express.Router();
const { sql, getPool: dbGetPool } = require("../db");

// Helper to acquire database connection pool reliably
const getPool = async (req) => {
  if (req && req.pool) {
    if (req.pool.connected && !req.pool.closed) {
      return req.pool;
    }
  }
  return await dbGetPool(process.env.DB_NAME || 'smartgold');
};

// Safe date formatter
function parseDateParam(dateStr) {
  if (!dateStr || !dateStr.trim()) return null;
  // If YYYY-MM-DD
  if (dateStr.includes("-")) {
    return dateStr.trim();
  }
  // If DD/MM/YYYY
  if (dateStr.includes("/")) {
    const parts = dateStr.trim().split("/");
    if (parts.length === 3) {
      const dd = parts[0].padStart(2, "0");
      const mm = parts[1].padStart(2, "0");
      const yyyy = parts[2];
      return `${yyyy}-${mm}-${dd}`;
    }
  }
  return dateStr.trim();
}

// 1. GET /api/transferstatus/locations
// Matching AddItemstoComboL in frmTransferList.vb
// Select rtrim(LocationCode) LocationCode, rtrim(LocationName) LocationName from tblLocation Order by SortCode
router.get("/locations", async (req, res) => {
  try {
    const pool = await getPool(req);
    const query = `
      SELECT 
        RTRIM(LocationCode) as LocationCode, 
        RTRIM(LocationName) as LocationName 
      FROM tblLocation 
      ORDER BY SortCode ASC
    `;
    const result = await pool.request().query(query);
    res.json({ success: true, data: result.recordset });
  } catch (error) {
    console.error("Error fetching locations for transfer list:", error);
    res.status(500).json({ success: false, message: "Error fetching locations: " + error.message });
  }
});

// 2. GET /api/transferstatus/fetch
// Matching subTransferOut & subTransferIn in frmTransferList.vb
// Transfer Out:
//   Select * from tblInventoryTransferHeader Where FromLocation = @Location And TransferDate = @TransferDate
// Transfer In:
//   Select TransferNo, TransferDate, FromLocStatus, ToLocStatus, TotalPCS, FromLocation, ToLocation
//   from tblInventoryTransferHeader Where ToLocation = @Location And TransferDate = @TransferDate
router.get("/fetch", async (req, res) => {
  try {
    const { location, transferDate, type } = req.query;
    
    if (!location || !location.trim()) {
      return res.status(400).json({ success: false, message: "Please select location." });
    }
    if (!type || !["in", "out"].includes(type.toLowerCase())) {
      return res.status(400).json({ success: false, message: "Please select Transfer In / Transfer Out" });
    }
    if (!transferDate || !transferDate.trim()) {
      return res.status(400).json({ success: false, message: "Please enter date." });
    }

    const pool = await getPool(req);
    const cleanLocation = location.trim();
    const cleanDate = parseDateParam(transferDate);

    let query = "";
    if (type.toLowerCase() === "out") {
      query = `
        SELECT 
          RTRIM(TransferNo) AS TransferNo, 
          TransferDate, 
          RTRIM(FromLocation) AS FromLocation,
          RTRIM(ToLocation) AS ToLocation,
          RTRIM(ToLocation) AS Location,
          FromLocStatus, 
          ToLocStatus, 
          ISNULL(TotalPCS, 0) AS TotalPCS,
          ISNULL(TotalWeight, 0) AS TotalWeight,
          ISNULL(TotalCost, 0) AS TotalCost,
          RTRIM(ISNULL(Remarks, '')) AS Remarks
        FROM tblInventoryTransferHeader 
        WHERE RTRIM(FromLocation) = @Location 
        AND CAST(TransferDate AS DATE) = CAST(@TransferDate AS DATE)
        ORDER BY TransferDate DESC, TransferNo DESC
      `;
    } else {
      query = `
        SELECT 
          RTRIM(TransferNo) AS TransferNo, 
          TransferDate, 
          RTRIM(FromLocation) AS FromLocation,
          RTRIM(ToLocation) AS ToLocation,
          RTRIM(FromLocation) AS Location,
          FromLocStatus, 
          ToLocStatus, 
          ISNULL(TotalPCS, 0) AS TotalPCS,
          ISNULL(TotalWeight, 0) AS TotalWeight,
          ISNULL(TotalCost, 0) AS TotalCost,
          RTRIM(ISNULL(Remarks, '')) AS Remarks
        FROM tblInventoryTransferHeader 
        WHERE RTRIM(ToLocation) = @Location 
        AND CAST(TransferDate AS DATE) = CAST(@TransferDate AS DATE)
        ORDER BY TransferDate DESC, TransferNo DESC
      `;
    }

    const result = await pool.request()
      .input("Location", sql.VarChar(50), cleanLocation)
      .input("TransferDate", sql.VarChar(30), cleanDate)
      .query(query);

    res.json({ success: true, data: result.recordset });
  } catch (error) {
    console.error("Error fetching transfers by date:", error);
    res.status(500).json({ success: false, message: "Error fetching transfers: " + error.message });
  }
});

// 3. GET /api/transferstatus/pending
// Matching subPendingTransferOut & subPendingTransferIn in frmTransferList.vb
// Pending Transfer Out:
//   Where FromLocation = @Location And TransferDate >= '1900-01-01' and ToLocStatus = 0
// Pending Transfer In:
//   Where ToLocation = @Location and ToLocStatus = 0
router.get("/pending", async (req, res) => {
  try {
    const { location, type } = req.query;
    
    if (!location || !location.trim()) {
      return res.status(400).json({ success: false, message: "Please select location." });
    }
    if (!type || !["in", "out"].includes(type.toLowerCase())) {
      return res.status(400).json({ success: false, message: "Please select Transfer In / Transfer Out" });
    }

    const pool = await getPool(req);
    const cleanLocation = location.trim();

    let query = "";
    if (type.toLowerCase() === "out") {
      query = `
        SELECT 
          RTRIM(TransferNo) AS TransferNo, 
          TransferDate, 
          RTRIM(FromLocation) AS FromLocation,
          RTRIM(ToLocation) AS ToLocation,
          RTRIM(ToLocation) AS Location,
          FromLocStatus, 
          ToLocStatus, 
          ISNULL(TotalPCS, 0) AS TotalPCS,
          ISNULL(TotalWeight, 0) AS TotalWeight,
          ISNULL(TotalCost, 0) AS TotalCost,
          RTRIM(ISNULL(Remarks, '')) AS Remarks
        FROM tblInventoryTransferHeader 
        WHERE RTRIM(FromLocation) = @Location 
        AND (ToLocStatus = 0 OR RTRIM(CAST(ToLocStatus AS VARCHAR(10))) = '0')
        ORDER BY TransferDate DESC, TransferNo DESC
      `;
    } else {
      query = `
        SELECT 
          RTRIM(TransferNo) AS TransferNo, 
          TransferDate, 
          RTRIM(FromLocation) AS FromLocation,
          RTRIM(ToLocation) AS ToLocation,
          RTRIM(FromLocation) AS Location,
          FromLocStatus, 
          ToLocStatus, 
          ISNULL(TotalPCS, 0) AS TotalPCS,
          ISNULL(TotalWeight, 0) AS TotalWeight,
          ISNULL(TotalCost, 0) AS TotalCost,
          RTRIM(ISNULL(Remarks, '')) AS Remarks
        FROM tblInventoryTransferHeader 
        WHERE RTRIM(ToLocation) = @Location 
        AND (ToLocStatus = 0 OR RTRIM(CAST(ToLocStatus AS VARCHAR(10))) = '0')
        ORDER BY TransferDate DESC, TransferNo DESC
      `;
    }

    const result = await pool.request()
      .input("Location", sql.VarChar(50), cleanLocation)
      .query(query);

    res.json({ success: true, data: result.recordset });
  } catch (error) {
    console.error("Error fetching pending transfers:", error);
    res.status(500).json({ success: false, message: "Error fetching pending transfers: " + error.message });
  }
});

// 4. GET /api/transferstatus/details/:transferNo
// Fetch line items for previewing transfer details
router.get("/details/:transferNo", async (req, res) => {
  try {
    const { transferNo } = req.params;
    if (!transferNo || !transferNo.trim()) {
      return res.status(400).json({ success: false, message: "Transfer Number is required." });
    }

    const pool = await getPool(req);
    const cleanNo = transferNo.trim();

    const detailQuery = `
      SELECT 
        RTRIM(InventoryCode) AS InventoryCode,
        RTRIM(ISNULL(Description, '')) AS Description,
        RTRIM(ISNULL(ClassCode, '')) AS ClassCode,
        RTRIM(ISNULL(CategoryCode, '')) AS CategoryCode,
        ISNULL(Weight, 0) AS Weight,
        RTRIM(ISNULL(SizeCode, '')) AS SizeCode,
        ISNULL(QtyonHand, 0) AS QtyOnHand,
        ISNULL(CostPerGram, 0) AS CostPerGram,
        ISNULL(PMC, 0) AS PMC,
        ISNULL(SMC, 0) AS SMC,
        ISNULL(TotalCost, 0) AS TotalCost
      FROM tblInventoryTransferDetail
      WHERE RTRIM(LTRIM(TransferNo)) = @TransferNo
    `;

    const result = await pool.request()
      .input("TransferNo", sql.VarChar(50), cleanNo)
      .query(detailQuery);

    res.json({ success: true, details: result.recordset });
  } catch (error) {
    console.error("Error fetching transfer line items:", error);
    res.status(500).json({ success: false, message: "Error fetching transfer line items: " + error.message });
  }
});

module.exports = router;
