const express = require("express");
const router = express.Router();
const { sql } = require("../db");

// GET /api/tradeinsplit/search
router.get("/search", async (req, res) => {
  try {
    const pool = req.pool;
    // According to reference code, searching tblTradeMergeInventoryheader where QtyOnHand <> 0
    const query = `
      SELECT 
        TradeInInventoryCode AS InventoryCode, 
        TotalWeight AS Weight, 
        TotalStWeight AS StoneWeight, 
        TotalPrice AS TotalCost
      FROM tblTradeMergeInventoryheader
      WHERE QtyOnHand <> 0
      ORDER BY TradeInInventoryCode DESC
    `;
    const result = await pool.request().query(query);
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Trade In Inventory Search Error:", err);
    res.status(500).json({ success: false, message: "Database error", error: err.message });
  }
});

// GET /api/tradeinsplit/nextCode
router.get("/nextCode", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query("SELECT TOP 1 LocationCode, NextTradeInSalesNo FROM tblCompany");
    
    let nextSeq = 1;
    let loc = "HQ";
    if (result.recordset.length > 0) {
      nextSeq = parseInt(result.recordset[0].NextTradeInSalesNo) || 1;
      loc = (result.recordset[0].LocationCode || "HQ").trim();
    }
    const nextCode = `TI-${loc}${String(nextSeq).padStart(8, "0")}`;
    res.json({ success: true, data: nextCode });
  } catch (err) {
    console.error("Get Next TradeIn Code Error:", err);
    res.status(500).json({ success: false, message: "Database error", error: err.message });
  }
});

// POST /api/tradeinsplit/save
router.post("/save", async (req, res) => {
  const pool = req.pool;
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
      splitWeight,
      isScrap,
      totalCost,
      userId
    } = req.body;

    if (!inventoryCode || !splitInvCode) {
      return res.status(400).json({ success: false, message: "Inventory Code and Split Inventory Code are required." });
    }

    if (parseFloat(originalInvWeight) === 0 || parseFloat(splitInvWeight) === 0) {
      return res.status(400).json({ success: false, message: "Invalid weight entered." });
    }

    await transaction.begin();

    const purity = isScrap ? "S" : "";
    const dTemp = parseFloat(totalCost) / parseFloat(weight);
    const dTotalCost1 = dTemp * parseFloat(originalInvWeight);
    const dTotalCost2 = parseFloat(totalCost) - dTotalCost1;

    const reqInsert = new sql.Request(transaction);
    await reqInsert
      .input("InventoryCode", sql.VarChar, splitInvCode.trim())
      .input("Purity", sql.VarChar, purity)
      .input("Weight", sql.Numeric(18, 4), parseFloat(splitInvWeight))
      .input("StWeight", sql.Numeric(18, 4), parseFloat(splitStWeight))
      .input("TotalCost", sql.Numeric(18, 4), dTotalCost2)
      .input("CreateUser", sql.VarChar, (userId || "sys").trim())
      .query(`
        INSERT INTO tblTradeMergeInventoryheader (
          TradeInInventoryCode, TotalWeight, TotalStWeight, TotalPrice, QtyOnHand, CreateUser, CreateDate
        ) VALUES (
          @InventoryCode, @Weight, @StWeight, @TotalCost, 1, @CreateUser, GETDATE()
        )
      `);

    const reqUpdate = new sql.Request(transaction);
    await reqUpdate
      .input("InventoryCode", sql.VarChar, inventoryCode.trim())
      .input("Weight", sql.Numeric(18, 4), parseFloat(originalInvWeight))
      .input("TotalCost", sql.Numeric(18, 4), dTotalCost1)
      .input("ModifyUser", sql.VarChar, (userId || "sys").trim())
      .query(`
        UPDATE tblTradeMergeInventoryheader 
        SET 
          TotalWeight = @Weight,
          TotalPrice = @TotalCost,
          ModifyUser = @ModifyUser,
          ModifyDate = GETDATE()
        WHERE TradeInInventoryCode = @InventoryCode
      `);

    await transaction.commit();
    res.json({ success: true, message: "Updated successfully." });

  } catch (err) {
    if (transaction) await transaction.rollback();
    console.error("TradeIn Split Save Error:", err);
    res.status(500).json({ success: false, message: "Error in save", error: err.message });
  }
});

module.exports = router;
