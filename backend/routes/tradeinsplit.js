const express = require("express");
const router = express.Router();
const { sql, poolPromise } = require("../db");

// Helper to acquire database connection pool reliably
const getPool = async (req) => {
  if (req && req.pool) return req.pool;
  if (poolPromise) return await poolPromise;
  throw new Error("Database pool is not available.");
};

// 1. GET /api/tradeinsplit/search
// LOV matching btnTradeBillNoSearch_Click in frmTradeInSplit.vb
// frmHelpSearch querying tblTradeMergeInventoryheader WHERE QtyOnHand <> 0
router.get("/search", async (req, res) => {
  try {
    const pool = await getPool(req);
    const query = `
      SELECT 
        RTRIM(LTRIM(TradeInInventoryCode)) AS InventoryCode, 
        ISNULL(TotalWeight, 0) AS Weight, 
        ISNULL(TotalStWeight, 0) AS StoneWeight, 
        ISNULL(TotalPrice, 0) AS TotalCost
      FROM tblTradeMergeInventoryheader
      WHERE ISNULL(QtyOnHand, 0) <> 0
      ORDER BY TradeInInventoryCode DESC
    `;
    const result = await pool.request().query(query);
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Trade In Inventory Search Error:", err);
    res.status(500).json({ success: false, message: "Database error", error: err.message });
  }
});

// 2. GET /api/tradeinsplit/nextCode
// Generates Next Trade-in Inventory code (matching objTII.GetNextTradeInventory)
router.get("/nextCode", async (req, res) => {
  try {
    const pool = await getPool(req);
    let nextSeq = 1;
    let loc = "HQ";

    const compRes = await pool.request().query(
      "SELECT TOP 1 LocationCode, NextTradeInSalesNo, NextPVNo FROM tblCompany"
    );
    if (compRes.recordset.length > 0) {
      const comp = compRes.recordset[0];
      nextSeq = parseInt(comp.NextTradeInSalesNo || comp.NextPVNo, 10) || 1;
      loc = (comp.LocationCode || "HQ").trim();
    }

    const nextCode = `TI-${loc}${String(nextSeq).padStart(8, "0")}`;
    res.json({ success: true, data: nextCode, rawSeq: nextSeq });
  } catch (err) {
    console.error("Get Next TradeIn Code Error:", err);
    res.status(500).json({ success: false, message: "Database error", error: err.message });
  }
});

// 3. POST /api/tradeinsplit/save
// Perform Split Save (matching btnSave_Click / fncInventorySplitSave in frmTradeInSplit.vb)
// 1) Inserts newly created split item into tblTradeMergeInventoryheader
// 2) Updates existing original inventory item in tblTradeMergeInventoryheader (new weight, remaining price)
// 3) Increments NextTradeInSalesNo in tblCompany
router.post("/save", async (req, res) => {
  let pool;
  try {
    pool = await getPool(req);
  } catch (poolErr) {
    return res.status(500).json({ success: false, message: poolErr.message });
  }

  const transaction = new sql.Transaction(pool);

  try {
    const {
      inventoryCode,
      weight,
      stWeight,
      originalInvWeight,
      splitInvCode,
      splitInvWeight,
      splitStWeight,
      isScrap,
      totalCost,
      userId
    } = req.body;

    const sInvCode = (inventoryCode || "").trim();
    const sSplitInvCode = (splitInvCode || "").trim();
    const sUser = (userId || "admin").trim();

    if (!sInvCode) {
      return res.status(400).json({ success: false, message: "Inventory Code is required." });
    }
    if (!sSplitInvCode) {
      return res.status(400).json({ success: false, message: "The split InventoryCode should not be blank." });
    }

    const nOrigWeight = parseFloat(weight) || 0;
    const nOrigCost = parseFloat(totalCost) || 0;
    const nNewOrigWeight = parseFloat(originalInvWeight) || 0;
    const nSplitWeight = parseFloat(splitInvWeight) || 0;
    const nSplitStWeight = parseFloat(splitStWeight) || 0;

    if (nNewOrigWeight === 0 || nSplitWeight === 0) {
      return res.status(400).json({ success: false, message: "Invalid weight entered." });
    }

    // Cost calculations matching VB:
    // dTemp = sTotalCost / Val(txtWeight.Text.Trim)
    // dTotalCost1 = dTemp * dWeight1 (remaining original cost)
    // dTotalcost2 = sTotalCost - dTotalCost1 (allocated split cost)
    let dTotalCost1 = 0;
    let dTotalCost2 = 0;
    if (nOrigWeight > 0) {
      const dTemp = nOrigCost / nOrigWeight;
      dTotalCost1 = Math.round(dTemp * nNewOrigWeight * 100) / 100;
      dTotalCost2 = Math.round((nOrigCost - dTotalCost1) * 100) / 100;
    }

    await transaction.begin();

    // 1. Insert newly split inventory into tblTradeMergeInventoryheader
    const reqInsert = new sql.Request(transaction);
    reqInsert.input("SplitCode", sql.VarChar(50), sSplitInvCode);
    reqInsert.input("Weight", sql.Decimal(18, 4), nSplitWeight);
    reqInsert.input("StWeight", sql.Decimal(18, 4), nSplitStWeight);
    reqInsert.input("TotalCost", sql.Decimal(18, 2), dTotalCost2);
    reqInsert.input("CreateUser", sql.VarChar(20), sUser);

    await reqInsert.query(`
      INSERT INTO tblTradeMergeInventoryheader (
        TradeInInventoryCode, TotalWeight, TotalStWeight, TotalPrice, QtyOnHand, CreateUser, CreateDate
      ) VALUES (
        @SplitCode, @Weight, @StWeight, @TotalCost, 1, @CreateUser, GETDATE()
      )
    `);

    // 2. Update original lot in tblTradeMergeInventoryheader
    const reqUpdate = new sql.Request(transaction);
    reqUpdate.input("OrigCode", sql.VarChar(50), sInvCode);
    reqUpdate.input("Weight", sql.Decimal(18, 4), nNewOrigWeight);
    reqUpdate.input("TotalCost", sql.Decimal(18, 2), dTotalCost1);
    reqUpdate.input("ModifyUser", sql.VarChar(20), sUser);

    await reqUpdate.query(`
      UPDATE tblTradeMergeInventoryheader 
      SET 
        TotalWeight = @Weight,
        TotalPrice = @TotalCost,
        ModifyUser = @ModifyUser,
        ModifyDate = GETDATE()
      WHERE RTRIM(LTRIM(TradeInInventoryCode)) = @OrigCode
    `);

    // 3. Increment counter in tblCompany
    await new sql.Request(transaction).query(
      "UPDATE tblCompany SET NextTradeInSalesNo = ISNULL(NextTradeInSalesNo, 0) + 1"
    );

    await transaction.commit();
    res.json({ success: true, message: "Updated sucessfully" });

  } catch (err) {
    try {
      if (transaction) await transaction.rollback();
    } catch (rbErr) {}
    console.error("TradeIn Split Save Error:", err);
    res.status(500).json({ success: false, message: "Error in save", error: err.message });
  }
});

module.exports = router;
