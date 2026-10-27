const express = require("express");
const router = express.Router();
const { sql } = require("../db");

// GET /api/tradeingoldissue/customers
// LOV for Customers
router.get("/customers", async (req, res) => {
  try {
    const pool = req.pool;
    const query = `
      SELECT 
        CustomerCode, 
        CustomerName, 
        Address1, 
        Address2, 
        Address3
      FROM tblCustomer
      UNION ALL
      SELECT 
        MemberCode AS CustomerCode, 
        MemberName AS CustomerName, 
        Address1, 
        Address2, 
        Address3
      FROM tblMember
      ORDER BY CustomerName ASC
    `;
    const result = await pool.request().query(query);
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Customers Error:", err);
    res.status(500).json({ success: false, message: "Error fetching customers" });
  }
});

// GET /api/tradeingoldissue/costpergram/:classcode
// Get Cost per gram based on Class Code
router.get("/costpergram/:classcode", async (req, res) => {
  try {
    const pool = req.pool;
    const { classcode } = req.params;
    let queryClass = classcode;

    // Handle mapping exactly like VB code
    if (classcode === "22K") queryClass = "22K%";
    else if (classcode === "18K") queryClass = "18K%";
    else queryClass = classcode + "%";

    const query = `SELECT Rate FROM tblClass WHERE ClassCode LIKE @ClassCode`;
    const result = await pool.request()
      .input('ClassCode', sql.VarChar, queryClass)
      .query(query);

    if (result.recordset.length > 0) {
      res.json({ success: true, Rate: result.recordset[0].Rate });
    } else {
      // Fallback
      const fallbackQuery = `SELECT TOP 1 Rate FROM tblClass`;
      const fbResult = await pool.request().query(fallbackQuery);
      if (fbResult.recordset.length > 0) {
        res.json({ success: true, Rate: fbResult.recordset[0].Rate });
      } else {
        res.json({ success: true, Rate: 0 });
      }
    }
  } catch (err) {
    console.error("Cost Per Gram Error:", err);
    res.status(500).json({ success: false, message: "Error fetching cost per gram" });
  }
});

// POST /api/tradeingoldissue/save
// Save Trade In Gold Issue Transaction
router.post("/save", async (req, res) => {
  let transaction;
  try {
    const pool = req.pool;
    transaction = new sql.Transaction(pool);

    const {
      customerCode,
      customerName,
      address1,
      address2,
      address3,
      lossPerc,
      costPerGram,
      remarks,
      items // The grid items
    } = req.body;

    if (!items || items.length === 0) {
      return res.status(400).json({ success: false, message: "At least one item is required in the grid." });
    }

    await transaction.begin();

    // Generate Invoice No (TI-XXXX)
    const seqReq = new sql.Request(transaction);
    const seqRes = await seqReq.query(`
      SELECT ISNULL(NextTradeInRefNo, 1) as NextNo FROM tblCompany
    `);
    const nextNo = seqRes.recordset[0].NextNo;
    const invoiceNo = nextNo.toString();
    const inventoryCode = "TI-" + invoiceNo;

    // Update Sequence
    const updSeqReq = new sql.Request(transaction);
    await updSeqReq.query(`
      UPDATE tblCompany SET NextTradeInRefNo = ISNULL(NextTradeInRefNo, 1) + 1
    `);

    // Delete existing if any (just in case)
    const delHeader = new sql.Request(transaction);
    await delHeader.input("InventoryCode", sql.VarChar, inventoryCode)
      .query(`DELETE FROM tblTradeInHeader WHERE InventoryCode = @InventoryCode`);

    const delDetail = new sql.Request(transaction);
    await delDetail.input("InventoryCode", sql.VarChar, inventoryCode)
      .query(`DELETE FROM tblTradeInDetail WHERE InventoryCode = @InventoryCode`);

    // Calculate Totals
    let totalWeight = 0;
    let totalGoldLoss = 0;
    let totalStoneWeight = 0;
    let totalPCS = 0;
    let totalAmount = 0;
    let totalMC = 0;

    items.forEach(item => {
      totalWeight += parseFloat(item.weight) || 0;
      totalGoldLoss += parseFloat(item.goldLoss) || 0;
      totalStoneWeight += parseFloat(item.stoneWeight) || 0;
      totalPCS += 1;
      totalAmount += parseFloat(item.totalCost) || 0;
      totalMC += parseFloat(item.pmc) || 0;
    });

    // Insert Header
    const insHeader = new sql.Request(transaction);
    await insHeader
      .input("InventoryCode", sql.VarChar, inventoryCode)
      .input("InvoiceNo", sql.VarChar, invoiceNo)
      .input("CustomerCode", sql.VarChar, customerCode || "")
      .input("CustomerName", sql.VarChar, customerName || "")
      .input("Address1", sql.VarChar, address1 || "")
      .input("Address2", sql.VarChar, address2 || "")
      .input("Address3", sql.VarChar, address3 || "")
      .input("CostPerGram", sql.Numeric(18, 4), parseFloat(costPerGram) || 0)
      .input("Weight", sql.Numeric(18, 4), totalWeight)
      .input("GoldLoss", sql.Numeric(18, 4), totalGoldLoss)
      .input("StoneWeight", sql.Numeric(18, 4), totalStoneWeight)
      .input("PCS", sql.Int, totalPCS)
      .input("TotalCost", sql.Numeric(18, 2), totalAmount)
      .input("TradeInAmount", sql.Numeric(18, 2), totalAmount)
      .input("Date", sql.DateTime, new Date())
      .query(`
        INSERT INTO tblTradeInHeader (
          InventoryCode, InvoiceNo, CustomerCode, CustomerName, Address1, Address2, Address3,
          CostPerGram, Weight, GoldLoss, StoneWeight, PCS, TotalCost, TradeInAmount, Date, IssueFlag
        ) VALUES (
          @InventoryCode, @InvoiceNo, @CustomerCode, @CustomerName, @Address1, @Address2, @Address3,
          @CostPerGram, @Weight, @GoldLoss, @StoneWeight, @PCS, @TotalCost, @TradeInAmount, @Date, 'N'
        )
      `);

    // Insert Details
    let sNo = 1;
    for (const item of items) {
      const insDetail = new sql.Request(transaction);
      await insDetail
        .input("InventoryCode", sql.VarChar, inventoryCode)
        .input("SNo", sql.Int, sNo++)
        .input("Description", sql.VarChar, item.description || "")
        .input("DiamondDescription", sql.VarChar, item.stoneDescription || "")
        .input("Weight", sql.Numeric(18, 4), parseFloat(item.weight) || 0)
        .input("StoneWeight", sql.Numeric(18, 4), parseFloat(item.stoneWeight) || 0)
        .input("GoldLoss", sql.Numeric(18, 4), parseFloat(item.goldLoss) || 0)
        .input("CostPerGram", sql.Numeric(18, 4), parseFloat(item.costPerGram) || 0)
        .input("TotalCost", sql.Numeric(18, 2), parseFloat(item.totalCost) || 0)
        .input("Class", sql.VarChar, item.class || "")
        .query(`
          INSERT INTO tblTradeInDetail (
            InventoryCode, SNo, Description, DiamondDescription, Weight, StoneWeight, 
            GoldLoss, CostPerGram, TotalCost, Class
          ) VALUES (
            @InventoryCode, @SNo, @Description, @DiamondDescription, @Weight, @StoneWeight,
            @GoldLoss, @CostPerGram, @TotalCost, @Class
          )
        `);
    }

    await transaction.commit();
    res.json({ success: true, message: "Trade In Gold Saved Successfully", invoiceNo: inventoryCode });

  } catch (err) {
    if (transaction) await transaction.rollback();
    console.error("TradeIn Gold Save Error:", err);
    res.status(500).json({ success: false, message: "Error saving trade in gold", error: err.message });
  }
});

module.exports = router;
