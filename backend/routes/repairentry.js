const express = require("express");
const router = express.Router();
const { sql, poolPromise } = require("../db");

// Helper to acquire database connection pool reliably
const getPool = async (req) => {
  if (req && req.pool) return req.pool;
  if (poolPromise) return await poolPromise;
  throw new Error("Database pool is not available.");
};

// Helper to pad voucher numbers
function padZero(num, size) {
  let s = num + "";
  while (s.length < size) s = "0" + s;
  return s;
}

// 1. GET next repair entry number (matching objClsOrder.GetNextOrderNo in frmRepairEntry.vb)
router.get("/next-no", async (req, res) => {
  try {
    const pool = await getPool(req);
    let loc = "HQ";
    let nextNo = 1;

    // Check tblCompany for LocationCode and possible repair counter columns
    try {
      const compColsRes = await pool.request().query(`
        SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE LOWER(TABLE_NAME) = 'tblcompany'
      `);
      const compCols = compColsRes.recordset.map(r => r.COLUMN_NAME.toLowerCase());

      const compRes = await pool.request().query("SELECT TOP 1 * FROM tblCompany");
      if (compRes.recordset.length > 0) {
        const comp = compRes.recordset[0];
        loc = (comp.LocationCode || "HQ").trim();
        if (compCols.includes("nextrepairno") && comp.NextRepairNo != null) {
          nextNo = parseInt(comp.NextRepairNo, 10) || 1;
        } else if (compCols.includes("nextorderno") && comp.NextOrderNo != null) {
          nextNo = parseInt(comp.NextOrderNo, 10) || 1;
        }
      }
    } catch (e1) {
      console.warn("tblCompany next-no check warning:", e1.message);
    }

    // Also check max from tblInventoryRepairHeader if counter was 1
    if (nextNo === 1) {
      try {
        const maxRes = await pool.request().query(`
          SELECT TOP 1 RepairNo FROM tblInventoryRepairHeader ORDER BY RepairNo DESC
        `);
        if (maxRes.recordset.length > 0 && maxRes.recordset[0].RepairNo) {
          const match = maxRes.recordset[0].RepairNo.match(/\d+/);
          if (match) {
            nextNo = parseInt(match[0], 10) + 1;
          }
        }
      } catch (e2) {}
    }

    const formattedNo = `RP-${loc}${padZero(nextNo, 8)}`;
    res.json({ success: true, repairNo: formattedNo, rawNo: nextNo });
  } catch (err) {
    console.error("Fetch Next Repair No Error:", err);
    res.json({ success: true, repairNo: "RP-HQ00000001", rawNo: 1 });
  }
});

// 1.5. GET existing invoices for LOV (matching btnOrderPaymentNoSearch_Click in frmRepairEntry.vb)
// WHERE (Status = 'R' or Status = 'P') and AdvancePaid < TotalCostEstimated
router.get("/invoices", async (req, res) => {
  try {
    const pool = await getPool(req);
    const result = await pool.request().query(`
      SELECT 
        h.RepairNo, 
        h.RepairDate, 
        h.CustomerCode,
        c.CustomerName,
        h.SupplierCode,
        ISNULL(h.TotalCostEstimated, 0) AS TotalCostEstimated,
        ISNULL(h.AdvancePaid, 0) AS AdvancePaid,
        (ISNULL(h.TotalCostEstimated, 0) - ISNULL(h.AdvancePaid, 0)) AS BalanceAmount,
        h.Status
      FROM tblInventoryRepairHeader h
      LEFT JOIN tblCustomer c ON c.CustomerCode = h.CustomerCode
      WHERE (h.Status = 'R' OR h.Status = 'P' OR h.Status IS NULL)
        AND ISNULL(h.AdvancePaid, 0) < ISNULL(h.TotalCostEstimated, 0)
      ORDER BY h.RepairDate DESC, h.RepairNo DESC
    `);
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Invoices Error:", err);
    res.status(500).json({ success: false, message: "Failed to fetch invoices", error: err.message });
  }
});

// 2. GET Customers for LOV (matching btnCustomerCodeSearch_Click in frmRepairEntry.vb)
router.get("/customers", async (req, res) => {
  try {
    const pool = await getPool(req);
    const result = await pool.request().query(`
      SELECT 
        CustomerCode, 
        CustomerName, 
        Address1, 
        Address2, 
        Address3, 
        CustomerType,
        NricNo,
        Phone,
        Email 
      FROM tblCustomer
      ORDER BY CustomerName ASC
    `);
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Customers Error:", err);
    res.status(500).json({ success: false, message: "Failed to fetch customers", error: err.message });
  }
});

// 3. GET Suppliers for LOV (matching btnSupplierCodeSearch_Click in frmRepairEntry.vb)
router.get("/suppliers", async (req, res) => {
  try {
    const pool = await getPool(req);
    const result = await pool.request().query(`
      SELECT SupplierCode, SupplierName, Address1, Address2, Address3 
      FROM tblSupplier
      ORDER BY SupplierName ASC
    `);
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Suppliers Error:", err);
    res.status(500).json({ success: false, message: "Failed to fetch suppliers", error: err.message });
  }
});

// 4. Validate Salesman Password (matching subGetPassword / PnlSalesManPwd in frmRepairEntry.vb)
router.post("/validate-salesman", async (req, res) => {
  try {
    const { password } = req.body;
    if (!password) {
      return res.status(400).json({ success: false, message: "Password is required" });
    }
    const pool = await getPool(req);
    const result = await pool.request()
      .input("Password", sql.VarChar, password)
      .query("SELECT SalesmanCode, SalesmanName, SalesManGroup FROM tblSalesMan WHERE Password = @Password");

    if (result.recordset.length > 0) {
      res.json({ 
        success: true, 
        salesmanCode: result.recordset[0].SalesmanCode,
        salesmanName: result.recordset[0].SalesmanName,
        salesmanGroup: result.recordset[0].SalesManGroup
      });
    } else {
      res.status(401).json({ success: false, message: "SalesMan Password is Incorrect!" });
    }
  } catch (err) {
    console.error("Validate Salesman Error:", err);
    res.status(500).json({ success: false, message: "Error validating salesman", error: err.message });
  }
});

// 5. GET existing repair entry (matching subGetRepair & SubLoadRepairDetail in frmRepairEntry.vb)
router.get("/:repairNo", async (req, res) => {
  try {
    const { repairNo } = req.params;
    const pool = await getPool(req);

    const headerRes = await pool.request()
      .input("RepairNo", sql.VarChar(20), repairNo)
      .query(`
        SELECT 
          h.*,
          c.CustomerName,
          c.Address1,
          c.Address2,
          c.Address3,
          c.CustomerType
        FROM tblInventoryRepairHeader h
        LEFT JOIN tblCustomer c ON c.CustomerCode = h.CustomerCode
        WHERE h.RepairNo = @RepairNo
      `);

    if (headerRes.recordset.length === 0) {
      return res.status(404).json({ success: false, message: "Please Give the Correct Repair Number" });
    }

    const detailRes = await pool.request()
      .input("RepairNo", sql.VarChar(20), repairNo)
      .query("SELECT * FROM tblInventoryRepairDetail WHERE RepairNo = @RepairNo ORDER BY LineNo ASC");

    // Also get payment history from tblOrderPaymentDetail
    const paymentRes = await pool.request()
      .input("RepairNo", sql.VarChar(20), repairNo)
      .query("SELECT * FROM tblOrderPaymentDetail WHERE OrderNo = @RepairNo ORDER BY CreateDate ASC");

    res.json({ 
      success: true, 
      header: headerRes.recordset[0],
      details: detailRes.recordset,
      payments: paymentRes.recordset
    });
  } catch (err) {
    console.error("Fetch Repair Error:", err);
    res.status(500).json({ success: false, message: "Failed to fetch repair details", error: err.message });
  }
});

// 6. POST to Save Repair Entry (matching btnSave_Click, FncRepairHeaderSave, FncRepairDetailSave, fncPaymodeDetailSave)
router.post("/save", async (req, res) => {
  let pool;
  let currentStep = "Initializing database pool";
  try {
    pool = await getPool(req);
  } catch (poolErr) {
    return res.status(500).json({ success: false, message: poolErr.message, error: poolErr.message });
  }

  const transaction = new sql.Transaction(pool);
  try {
    const {
      repairNo,
      repairDate,
      issueDate,
      dateOfDelivery,
      customerCode,
      supplierCode,
      remarks,
      advancePaid,
      totalCostEstimated,
      salesManCode,
      tradeInAmount,
      tradeInType,
      tradeInGST,
      isService,
      paymodeDetails,
      items // Array of { description, weight, cost }
    } = req.body;

    currentStep = "Discovering schema for repair tables";

    // Helper to fetch schema columns with both sys and INFORMATION_SCHEMA
    const getTableSchema = async (tableName) => {
      const lowerName = tableName.toLowerCase();
      let cols = {};
      let colTypes = {};
      let colNullable = {};
      let found = false;

      try {
        const sRes = await pool.request().query(`
          SELECT 
            LOWER(c.name) AS col_name,
            LOWER(ty.name) AS data_type,
            c.max_length,
            c.is_nullable
          FROM sys.tables t
          JOIN sys.columns c ON t.object_id = c.object_id
          JOIN sys.types ty ON c.user_type_id = ty.user_type_id
          WHERE LOWER(t.name) = '${lowerName}'
        `);
        if (sRes.recordset && sRes.recordset.length > 0) {
          for (const r of sRes.recordset) {
            cols[r.col_name] = r.max_length;
            colTypes[r.col_name] = r.data_type;
            colNullable[r.col_name] = r.is_nullable === 1 || r.is_nullable === true;
          }
          found = true;
        }
      } catch (e1) {
        console.warn("sys.columns schema error:", e1.message);
      }

      if (!found) {
        try {
          const iRes = await pool.request().query(`
            SELECT 
              LOWER(COLUMN_NAME) AS col_name,
              LOWER(DATA_TYPE) AS data_type,
              CHARACTER_MAXIMUM_LENGTH AS max_length,
              CASE WHEN UPPER(IS_NULLABLE) = 'YES' THEN 1 ELSE 0 END AS is_nullable
            FROM INFORMATION_SCHEMA.COLUMNS
            WHERE LOWER(TABLE_NAME) = '${lowerName}'
          `);
          if (iRes.recordset && iRes.recordset.length > 0) {
            for (const r of iRes.recordset) {
              cols[r.col_name] = r.max_length;
              colTypes[r.col_name] = r.data_type;
              colNullable[r.col_name] = r.is_nullable === 1;
            }
            found = true;
          }
        } catch (e2) {
          console.warn("INFORMATION_SCHEMA schema error:", e2.message);
        }
      }

      return { cols, colTypes, colNullable, found };
    };

    const headerSchema = await getTableSchema("tblInventoryRepairHeader");
    const detailSchema = await getTableSchema("tblInventoryRepairDetail");

    const isNumericType = (dt) => {
      if (!dt) return false;
      const t = dt.toLowerCase();
      return (
        t.includes("int") ||
        t.includes("numeric") ||
        t.includes("decimal") ||
        t.includes("money") ||
        t.includes("float") ||
        t.includes("real")
      );
    };

    const isDateType = (dt) => {
      if (!dt) return false;
      const t = dt.toLowerCase();
      return t.includes("date") || t.includes("time");
    };

    const safeStr = (val, maxLen) => {
      if (val === undefined || val === null) return "";
      const s = String(val).trim();
      return maxLen && maxLen > 0 ? s.substring(0, maxLen) : s;
    };

    const hasHeaderCol = (name) => {
      const lower = name.toLowerCase();
      if (headerSchema.found) return headerSchema.cols.hasOwnProperty(lower);
      return true;
    };

    const getHeaderColLen = (name, def = 50) => {
      const len = headerSchema.cols[name.toLowerCase()];
      return len && len > 0 ? len : def;
    };

    const hasDetailCol = (name) => {
      const lower = name.toLowerCase();
      if (detailSchema.found) return detailSchema.cols.hasOwnProperty(lower);
      return true;
    };

    const getDetailColLen = (name, def = 50) => {
      const len = detailSchema.cols[name.toLowerCase()];
      return len && len > 0 ? len : def;
    };

    currentStep = "Generating valid Repair Number";
    const maxRepairLen = getHeaderColLen("repairno", 15);
    let finalRepairNo = repairNo ? String(repairNo).trim() : "";

    // If finalRepairNo is missing, or is a long timestamp fallback like RP-179... that exceeds maxRepairLen
    if (!finalRepairNo || finalRepairNo.length > maxRepairLen || finalRepairNo.startsWith("RP-179") || finalRepairNo.startsWith("RP-17")) {
      let loc = "HQ";
      let nextNo = 1;
      try {
        const compRes = await pool.request().query("SELECT TOP 1 * FROM tblCompany");
        if (compRes.recordset.length > 0) {
          const comp = compRes.recordset[0];
          loc = (comp.LocationCode || "HQ").trim();
          if (comp.NextRepairNo != null) nextNo = parseInt(comp.NextRepairNo, 10) || 1;
        }
      } catch (e) {}

      if (nextNo === 1) {
        try {
          const maxRes = await pool.request().query(`
            SELECT TOP 1 RepairNo FROM tblInventoryRepairHeader ORDER BY RepairNo DESC
          `);
          if (maxRes.recordset.length > 0 && maxRes.recordset[0].RepairNo) {
            const match = maxRes.recordset[0].RepairNo.match(/\d+/);
            if (match) nextNo = parseInt(match[0], 10) + 1;
          }
        } catch (e2) {}
      }

      finalRepairNo = `RP-${loc}${padZero(nextNo, 8)}`;
      if (finalRepairNo.length > maxRepairLen) {
        finalRepairNo = finalRepairNo.substring(0, maxRepairLen);
      }
    }

    const nAdvancePaid = parseFloat(advancePaid) || 0;
    const nTotalCostEstimated = parseFloat(totalCostEstimated) || 0;
    const nTradeInAmount = parseFloat(tradeInAmount) || 0;
    const nTradeInGST = parseFloat(tradeInGST) || 0;
    const sUser = salesManCode || "ADMIN";
    const sInvType = isService ? "S" : "R";

    currentStep = "Beginning transaction";
    await transaction.begin();

    // 1. Delete and Insert or Update tblInventoryRepairHeader
    currentStep = "Clearing existing Repair Header";
    const reqDelHeader = new sql.Request(transaction);
    await reqDelHeader
      .input("RepairNo", sql.VarChar(maxRepairLen), finalRepairNo)
      .query("DELETE FROM tblInventoryRepairHeader WHERE RepairNo = @RepairNo");

    currentStep = "Inserting tblInventoryRepairHeader";
    const reqHeader = new sql.Request(transaction);
    const hCols = ["RepairNo"];
    const hVals = ["@RepairNo"];
    reqHeader.input("RepairNo", sql.VarChar(maxRepairLen), finalRepairNo);

    // RepairDate
    if (hasHeaderCol("repairdate")) {
      hCols.push("RepairDate");
      hVals.push("@RepairDate");
      const d = repairDate ? new Date(repairDate) : new Date();
      reqHeader.input("RepairDate", sql.DateTime, !isNaN(d.getTime()) ? d : new Date());
    }

    // Delivery Date column name: check deliverydate or dateofdelivery
    const deliveryColName = hasHeaderCol("deliverydate") ? "DeliveryDate" : (hasHeaderCol("dateofdelivery") ? "DateOfDelivery" : null);
    if (deliveryColName) {
      hCols.push(deliveryColName);
      hVals.push("@DeliveryDate");
      const d = dateOfDelivery ? new Date(dateOfDelivery) : new Date();
      reqHeader.input("DeliveryDate", sql.DateTime, !isNaN(d.getTime()) ? d : new Date());
    }

    // Issue Date
    if (hasHeaderCol("issuedate")) {
      hCols.push("IssueDate");
      hVals.push("@IssueDate");
      const d = issueDate ? new Date(issueDate) : new Date();
      reqHeader.input("IssueDate", sql.DateTime, !isNaN(d.getTime()) ? d : new Date());
    }

    // CustomerCode
    if (hasHeaderCol("customercode")) {
      const cLen = getHeaderColLen("customercode", 20);
      hCols.push("CustomerCode");
      hVals.push("@CustomerCode");
      reqHeader.input("CustomerCode", sql.VarChar(cLen), safeStr(customerCode, cLen));
    }

    // SupplierCode
    if (hasHeaderCol("suppliercode")) {
      const sLen = getHeaderColLen("suppliercode", 20);
      hCols.push("SupplierCode");
      hVals.push("@SupplierCode");
      reqHeader.input("SupplierCode", sql.VarChar(sLen), safeStr(supplierCode, sLen));
    }

    // TradeInAmount
    if (hasHeaderCol("tradeinamount")) {
      hCols.push("TradeInAmount");
      hVals.push("@TradeInAmount");
      reqHeader.input("TradeInAmount", sql.Decimal(18, 2), nTradeInAmount);
    }

    // TradeInType
    const tradeInColName = hasHeaderCol("stradeintype") ? "sTradeInType" : (hasHeaderCol("tradeintype") ? "TradeInType" : null);
    if (tradeInColName) {
      hCols.push(tradeInColName);
      hVals.push("@TradeInType");
      reqHeader.input("TradeInType", sql.VarChar(2), safeStr(tradeInType || 'C', 2));
    }

    // RateFixed
    if (hasHeaderCol("ratefixed")) {
      const dt = headerSchema.colTypes["ratefixed"] || "varchar";
      hCols.push("RateFixed");
      hVals.push("@RateFixed");
      if (isNumericType(dt) || dt === "bit") {
        reqHeader.input("RateFixed", sql.Int, 0);
      } else {
        reqHeader.input("RateFixed", sql.VarChar(getHeaderColLen("ratefixed", 2)), "N");
      }
    }

    // TotalCostEstimated
    const totCostCol = hasHeaderCol("totalcostestimated") ? "TotalCostEstimated" : (hasHeaderCol("estimatedcost") ? "EstimatedCost" : (hasHeaderCol("totalcost") ? "TotalCost" : null));
    if (totCostCol) {
      hCols.push(totCostCol);
      hVals.push("@TotalCostEstimated");
      reqHeader.input("TotalCostEstimated", sql.Decimal(18, 2), nTotalCostEstimated);
    }

    // AdvancePaid
    if (hasHeaderCol("advancepaid")) {
      hCols.push("AdvancePaid");
      hVals.push("@AdvancePaid");
      reqHeader.input("AdvancePaid", sql.Decimal(18, 2), nAdvancePaid);
    }

    // Status
    const statusLen = getHeaderColLen("status", 2);
    hCols.push("Status");
    hVals.push("@Status");
    reqHeader.input("Status", sql.VarChar(statusLen), safeStr(sInvType, statusLen));

    // InvType (Required NOT NULL column in tblInventoryRepairHeader)
    const itType = headerSchema.colTypes["invtype"] || "varchar";
    hCols.push("InvType");
    hVals.push("@InvType");
    if (isNumericType(itType)) {
      reqHeader.input("InvType", sql.Int, isService ? 1 : 0);
    } else {
      const itLen = getHeaderColLen("invtype", 2);
      reqHeader.input("InvType", sql.VarChar(itLen), safeStr(sInvType, itLen));
    }

    // CostPerGram
    if (hasHeaderCol("costpergram")) {
      hCols.push("CostPerGram");
      hVals.push("@CostPerGram");
      reqHeader.input("CostPerGram", sql.Decimal(18, 2), 0);
    }

    // SupplierCost
    if (hasHeaderCol("suppliercost")) {
      hCols.push("SupplierCost");
      hVals.push("@SupplierCost");
      reqHeader.input("SupplierCost", sql.Decimal(18, 2), 0);
    }

    // EstimatedGST
    if (hasHeaderCol("estimatedgst")) {
      hCols.push("EstimatedGST");
      hVals.push("@EstimatedGST");
      reqHeader.input("EstimatedGST", sql.Decimal(18, 2), 0);
    }

    // Remarks
    if (hasHeaderCol("remarks")) {
      const rLen = getHeaderColLen("remarks", 150);
      hCols.push("Remarks");
      hVals.push("@Remarks");
      reqHeader.input("Remarks", sql.VarChar(rLen), safeStr(remarks, rLen));
    }

    // SalesManCode
    const smCol = hasHeaderCol("salesmancode") ? "SalesManCode" : (hasHeaderCol("salesman") ? "SalesMan" : null);
    if (smCol) {
      const smLen = getHeaderColLen(smCol, 20);
      hCols.push(smCol);
      hVals.push("@SalesManCode");
      reqHeader.input("SalesManCode", sql.VarChar(smLen), safeStr(sUser, smLen));
    }

    // TradeInGST
    if (hasHeaderCol("tradeingst")) {
      hCols.push("TradeInGST");
      hVals.push("@TradeInGST");
      reqHeader.input("TradeInGST", sql.Decimal(18, 2), nTradeInGST);
    }

    // Audit and location columns
    if (hasHeaderCol("locationcode")) {
      hCols.push("LocationCode");
      hVals.push("@LocationCode");
      reqHeader.input("LocationCode", sql.VarChar(10), "HQ");
    }
    if (hasHeaderCol("createduser")) {
      hCols.push("CreatedUser");
      hVals.push("@CreatedUser");
      reqHeader.input("CreatedUser", sql.VarChar(getHeaderColLen("createduser", 20)), safeStr(sUser, 20));
    }
    if (hasHeaderCol("createuser")) {
      hCols.push("CreateUser");
      hVals.push("@CreateUser");
      reqHeader.input("CreateUser", sql.VarChar(getHeaderColLen("createuser", 20)), safeStr(sUser, 20));
    }
    if (hasHeaderCol("createby")) {
      hCols.push("CreateBy");
      hVals.push("@CreateBy");
      reqHeader.input("CreateBy", sql.VarChar(getHeaderColLen("createby", 20)), safeStr(sUser, 20));
    }
    if (hasHeaderCol("createdate")) {
      hCols.push("CreateDate");
      hVals.push("GETDATE()");
    }
    if (hasHeaderCol("createddate")) {
      hCols.push("CreatedDate");
      hVals.push("GETDATE()");
    }

    await reqHeader.query(`
      INSERT INTO tblInventoryRepairHeader (${hCols.join(", ")})
      VALUES (${hVals.join(", ")})
    `);

    // 2. Delete existing details and Insert line items
    currentStep = "Clearing existing Repair Details";
    const reqDelDetails = new sql.Request(transaction);
    await reqDelDetails
      .input("RepairNo", sql.VarChar(maxRepairLen), finalRepairNo)
      .query("DELETE FROM tblInventoryRepairDetail WHERE RepairNo = @RepairNo");

    currentStep = "Inserting tblInventoryRepairDetail lines";
    if (items && Array.isArray(items) && items.length > 0) {
      let lineIdx = 1;
      for (const item of items) {
        const reqDetail = new sql.Request(transaction);
        const dCols = ["RepairNo"];
        const dVals = ["@RepairNo"];

        reqDetail.input("RepairNo", sql.VarChar(maxRepairLen), finalRepairNo);

        // Line sequence column: LineNo, RepairDetailCode, or OrderInventoryCode
        const lineCol = hasDetailCol("lineno") ? "LineNo" : (hasDetailCol("repairdetailcode") ? "RepairDetailCode" : (hasDetailCol("orderinventorycode") ? "OrderInventoryCode" : null));
        if (lineCol) {
          dCols.push(lineCol);
          dVals.push("@LineNo");
          reqDetail.input("LineNo", sql.Int, lineIdx);
        }

        // Description column
        const descCol = hasDetailCol("description") ? "Description" : (hasDetailCol("repairdescription") ? "RepairDescription" : null);
        if (descCol) {
          const dLen = getDetailColLen(descCol, 150);
          dCols.push(descCol);
          dVals.push("@Description");
          reqDetail.input("Description", sql.VarChar(dLen), safeStr(item.description || item.Description, dLen));
        }

        // Weight column
        const weightCol = hasDetailCol("weightestimated") ? "WeightEstimated" : (hasDetailCol("weight") ? "Weight" : (hasDetailCol("estimatedweight") ? "EstimatedWeight" : null));
        if (weightCol) {
          dCols.push(weightCol);
          dVals.push("@WeightEstimated");
          reqDetail.input("WeightEstimated", sql.Decimal(18, 3), parseFloat(item.weight || item.Weight) || 0);
        }

        // Cost column
        const costCol = hasDetailCol("estimatedcost") ? "EstimatedCost" : (hasDetailCol("cost") ? "Cost" : (hasDetailCol("totalcost") ? "TotalCost" : null));
        if (costCol) {
          dCols.push(costCol);
          dVals.push("@EstimatedCost");
          reqDetail.input("EstimatedCost", sql.Decimal(18, 2), parseFloat(item.cost || item.Cost) || 0);
        }

        // Status
        const detStatusLen = getDetailColLen("status", 2);
        dCols.push("Status");
        dVals.push("@Status");
        reqDetail.input("Status", sql.VarChar(detStatusLen), safeStr(sInvType, detStatusLen));

        // InvType
        const detItLen = getDetailColLen("invtype", 2);
        dCols.push("InvType");
        dVals.push("@InvType");
        reqDetail.input("InvType", sql.VarChar(detItLen), safeStr(sInvType, detItLen));

        // LocationCode
        if (hasDetailCol("locationcode")) {
          dCols.push("LocationCode");
          dVals.push("@LocationCode");
          reqDetail.input("LocationCode", sql.VarChar(10), "HQ");
        }

        // Audit fields for detail
        if (hasDetailCol("createduser")) {
          dCols.push("CreatedUser");
          dVals.push("@CreatedUser");
          reqDetail.input("CreatedUser", sql.VarChar(getDetailColLen("createduser", 20)), safeStr(sUser, 20));
        }
        if (hasDetailCol("createuser")) {
          dCols.push("CreateUser");
          dVals.push("@CreateUser");
          reqDetail.input("CreateUser", sql.VarChar(getDetailColLen("createuser", 20)), safeStr(sUser, 20));
        }
        if (hasDetailCol("createby")) {
          dCols.push("CreateBy");
          dVals.push("@CreateBy");
          reqDetail.input("CreateBy", sql.VarChar(getDetailColLen("createby", 20)), safeStr(sUser, 20));
        }
        if (hasDetailCol("createddate")) {
          dCols.push("CreatedDate");
          dVals.push("GETDATE()");
        }
        if (hasDetailCol("createdate")) {
          dCols.push("CreateDate");
          dVals.push("GETDATE()");
        }

        await reqDetail.query(`
          INSERT INTO tblInventoryRepairDetail (${dCols.join(", ")})
          VALUES (${dVals.join(", ")})
        `);
        lineIdx++;
      }
    }

    // 3. If Advance Paid > 0, generate payment voucher & insert into tblOrderPaymentDetail and tblPayModeDetail (matching lines 311-317 & fncPaymodeDetailSave)
    if (nAdvancePaid > 0) {
      currentStep = "Generating payment voucher";
      let nextPmNo = 1;
      try {
        const pmCounterRes = await new sql.Request(transaction).query(
          "SELECT TOP 1 NextOrderPaymentNo FROM tblCompany"
        );
        nextPmNo = pmCounterRes.recordset.length > 0 ? pmCounterRes.recordset[0].NextOrderPaymentNo || 1 : 1;
      } catch (e) {}

      const msOrderPaymentNo = `RP${nextPmNo}`;

      // Insert into tblOrderPaymentDetail
      currentStep = "Inserting tblOrderPaymentDetail";
      const reqOrderPayment = new sql.Request(transaction);
      reqOrderPayment.input("OrderNo", sql.VarChar(20), finalRepairNo);
      reqOrderPayment.input("OrderPaymentNo", sql.VarChar(20), msOrderPaymentNo);
      reqOrderPayment.input("AmountPaid", sql.Decimal(18, 2), nAdvancePaid);
      reqOrderPayment.input("Remarks", sql.VarChar(100), "REPAIRENTRY");
      reqOrderPayment.input("TradeinAmt", sql.Decimal(18, 2), nTradeInAmount);
      reqOrderPayment.input("TradeinGST", sql.Decimal(18, 2), nTradeInGST);
      reqOrderPayment.input("CreateDate", sql.DateTime, new Date());
      reqOrderPayment.input("CreateUser", sql.VarChar(20), sUser);

      await reqOrderPayment.query(`
        INSERT INTO tblOrderPaymentDetail (
          OrderNo, OrderPaymentNo, AmountPaid, Remarks, TradeinAmt, TradeinGST, CreateDate, CreateUser
        ) VALUES (
          @OrderNo, @OrderPaymentNo, @AmountPaid, @Remarks, @TradeinAmt, @TradeinGST, @CreateDate, @CreateUser
        )
      `);

      // Insert paymodes into tblPayModeDetail
      currentStep = "Inserting tblPayModeDetail";
      if (paymodeDetails && Array.isArray(paymodeDetails) && paymodeDetails.length > 0) {
        for (const pm of paymodeDetails) {
          const pmAmount = parseFloat(pm.amount || pm.Amount) || 0;
          if (pmAmount > 0) {
            const reqPm = new sql.Request(transaction);
            reqPm.input("InvoiceNo", sql.VarChar(20), finalRepairNo);
            reqPm.input("InvoiceDate", sql.DateTime, new Date());
            reqPm.input("PayMode", sql.VarChar(20), String(pm.payMode || pm.PayMode || 'CASH').trim());
            reqPm.input("Amount", sql.Decimal(18, 2), pmAmount);
            reqPm.input("CreateUser", sql.VarChar(20), sUser);
            reqPm.input("LocationCode", sql.VarChar(10), "HQ");
            reqPm.input("SalesType", sql.VarChar(20), "REPAIR");

            await reqPm.query(`
              INSERT INTO tblPayModeDetail (
                InvoiceNo, InvoiceDate, PayMode, Amount, CreateUser, CreateDate, LocationCode, SalesType, IsPaid
              ) VALUES (
                @InvoiceNo, @InvoiceDate, @PayMode, @Amount, @CreateUser, @InvoiceDate, @LocationCode, @SalesType, 1
              )
            `);
          }
        }
      } else {
        // Default to CASH advance
        const reqPm = new sql.Request(transaction);
        reqPm.input("InvoiceNo", sql.VarChar(20), finalRepairNo);
        reqPm.input("InvoiceDate", sql.DateTime, new Date());
        reqPm.input("Amount", sql.Decimal(18, 2), nAdvancePaid);
        reqPm.input("CreateUser", sql.VarChar(20), sUser);

        await reqPm.query(`
          INSERT INTO tblPayModeDetail (
            InvoiceNo, InvoiceDate, PayMode, Amount, CreateUser, CreateDate, LocationCode, SalesType, IsPaid
          ) VALUES (
            @InvoiceNo, @InvoiceDate, 'CASH', @Amount, @CreateUser, @InvoiceDate, 'HQ', 'REPAIR', 1
          )
        `);
      }

      // Increment NextOrderPaymentNo in tblCompany if column exists
      try {
        await new sql.Request(transaction).query(
          "UPDATE tblCompany SET NextOrderPaymentNo = ISNULL(NextOrderPaymentNo, 0) + 1"
        );
      } catch (e) {}
    }

    // 4. Increment NextRepairNo in tblCompany if column exists
    currentStep = "Incrementing NextRepairNo in tblCompany";
    try {
      const compColsRes = await pool.request().query(`
        SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
        WHERE LOWER(TABLE_NAME) = 'tblcompany' AND LOWER(COLUMN_NAME) = 'nextrepairno'
      `);
      if (compColsRes.recordset.length > 0) {
        await new sql.Request(transaction).query(
          "UPDATE tblCompany SET NextRepairNo = ISNULL(NextRepairNo, 0) + 1"
        );
      }
    } catch (e) {}

    currentStep = "Committing Transaction";
    await transaction.commit();
    res.json({ success: true, message: "Repair Entry Saved Successfully", repairNo: finalRepairNo });
  } catch (err) {
    if (transaction) {
      try {
        await transaction.rollback();
      } catch (rbErr) {}
    }
    console.error(`Save Repair Error at [${currentStep}]:`, err);
    res.status(500).json({ 
      success: false, 
      message: err.message || "Failed to save repair entry", 
      step: currentStep,
      error: err.message,
      detail: err.originalError ? err.originalError.message : undefined 
    });
  }
});

// 7. DELETE (Cancel) Repair Entry (matching btnInvoiceCancel_Click, subCancelRepairHeader, subCancelRepairDetail, SubCancelPaymodeDetail)
router.delete("/:repairNo", async (req, res) => {
  let pool;
  try {
    pool = await getPool(req);
  } catch (poolErr) {
    return res.status(500).json({ success: false, message: poolErr.message });
  }

  const transaction = new sql.Transaction(pool);
  try {
    const { repairNo } = req.params;
    await transaction.begin();

    // 1. Check if invoice exists
    const chkRes = await new sql.Request(transaction)
      .input("RepairNo", sql.VarChar(20), repairNo)
      .query("SELECT * FROM tblInventoryRepairHeader WHERE RepairNo = @RepairNo");

    if (chkRes.recordset.length === 0) {
      await transaction.rollback();
      return res.status(404).json({ success: false, message: "Repair invoice not found." });
    }

    // 2. Move Header to Cancel table and Delete (subCancelRepairHeader)
    await new sql.Request(transaction)
      .input("RepairNo", sql.VarChar(20), repairNo)
      .query(`
        IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tblInventoryRepairHeaderCancel')
        BEGIN
          INSERT INTO tblInventoryRepairHeaderCancel 
          SELECT * FROM tblInventoryRepairHeader WHERE RepairNo = @RepairNo
        END;
        DELETE FROM tblInventoryRepairHeader WHERE RepairNo = @RepairNo;
      `);

    // 3. Move Details to Cancel table and Delete (subCancelRepairDetail)
    await new sql.Request(transaction)
      .input("RepairNo", sql.VarChar(20), repairNo)
      .query(`
        IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tblInventoryRepairDetailCancel')
        BEGIN
          INSERT INTO tblInventoryRepairDetailCancel 
          SELECT * FROM tblInventoryRepairDetail WHERE RepairNo = @RepairNo
        END;
        DELETE FROM tblInventoryRepairDetail WHERE RepairNo = @RepairNo;
      `);

    // 4. Reverse paymode entries (SubCancelPaymodeDetail: -Amount)
    await new sql.Request(transaction)
      .input("RepairNo", sql.VarChar(20), repairNo)
      .query(`
        INSERT INTO tblPayModeDetail (InvoiceNo, InvoiceDate, PayMode, Amount, CreateUser, CreateDate, LocationCode, SalesType, IsPaid)
        SELECT InvoiceNo, GETDATE(), PayMode, -Amount, 'SYS', GETDATE(), LocationCode, 'RP CANCEL', 1
        FROM tblPayModeDetail 
        WHERE InvoiceNo = @RepairNo AND SalesType = 'REPAIR' AND Amount > 0;
      `);

    await transaction.commit();
    res.json({ success: true, message: "The Repair Entry has been cancelled" });
  } catch (err) {
    if (transaction) {
      try {
        await transaction.rollback();
      } catch (rbErr) {}
    }
    console.error("Cancel Repair Error:", err);
    res.status(500).json({ success: false, message: "Failed to cancel repair entry", error: err.message });
  }
});

// 8. RePrint Repair Entry (matching subPrintRepair / subPrintBill in frmRepairEntry.vb)
router.get("/reprint/:repairNo", async (req, res) => {
  try {
    const { repairNo } = req.params;
    const pool = await getPool(req);

    const headerRes = await pool.request()
      .input("RepairNo", sql.VarChar(20), repairNo)
      .query(`
        SELECT 
          h.*,
          c.CustomerName,
          c.Address1,
          c.Address2,
          c.Address3,
          c.CustomerType
        FROM tblInventoryRepairHeader h
        LEFT JOIN tblCustomer c ON c.CustomerCode = h.CustomerCode
        WHERE h.RepairNo = @RepairNo
      `);

    if (headerRes.recordset.length === 0) {
      return res.status(404).json({ success: false, message: "Repair invoice not found." });
    }

    const detailRes = await pool.request()
      .input("RepairNo", sql.VarChar(20), repairNo)
      .query("SELECT * FROM tblInventoryRepairDetail WHERE RepairNo = @RepairNo ORDER BY LineNo ASC");

    res.json({ 
      success: true, 
      header: headerRes.recordset[0],
      details: detailRes.recordset
    });
  } catch (err) {
    console.error("RePrint Repair Error:", err);
    res.status(500).json({ success: false, message: "Failed to load repair docket", error: err.message });
  }
});

module.exports = router;
