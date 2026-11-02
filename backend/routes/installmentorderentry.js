const express = require("express");
const router = express.Router();
const { sql, poolPromise } = require("../db");

// Helper to acquire database connection pool reliably
const getPool = async (req) => {
  if (req && req.pool) return req.pool;
  if (poolPromise) return await poolPromise;
  throw new Error("Database pool is not available.");
};

// GET Company Info (for GST and Next IDs)
router.get("/company", async (req, res) => {
  try {
    const pool = await getPool(req);
    const result = await pool.request().query("SELECT TOP 1 * FROM tblCompany");
    if (result.recordset.length > 0) {
      res.json({ success: true, company: result.recordset[0] });
    } else {
      res.json({ success: false, message: "Company not found" });
    }
  } catch (err) {
    res.status(500).json({ success: false, message: err.message });
  }
});

// GET LOV Data
router.get("/lov", async (req, res) => {
  try {
    const { type, search, mode } = req.query;
    const pool = await getPool(req);
    let query = "";

    // Simple sanitization for search
    const safeSearch = search ? search.replace(/'/g, "''") : "";

    if (type === "customer") {
      query = `SELECT TOP 50 CustomerCode AS Code, CustomerName AS Name, NricNo, Address1, Address2, Address3 FROM tblCustomer ${safeSearch ? `WHERE CustomerCode LIKE '%${safeSearch}%' OR CustomerName LIKE '%${safeSearch}%'` : ""}`;
    } else if (type === "class") {
      query = `SELECT TOP 50 ClassCode AS Code, ClassName AS Name, Rate, Remarks FROM tblClass ${safeSearch ? `WHERE ClassCode LIKE '%${safeSearch}%' OR ClassName LIKE '%${safeSearch}%'` : ""}`;
    } else if (type === "category") {
      query = `SELECT TOP 50 CategoryCode AS Code, CategoryName AS Name, ClassCode, Remarks FROM tblCategory ${safeSearch ? `WHERE CategoryCode LIKE '%${safeSearch}%' OR CategoryName LIKE '%${safeSearch}%'` : ""}`;
    } else if (type === "catalogue") {
      query = `SELECT TOP 50 CatalogueCode AS Code, CatalogueName AS Name, CategoryCode, Remarks FROM tblCatalogue ${safeSearch ? `WHERE CatalogueCode LIKE '%${safeSearch}%' OR CatalogueName LIKE '%${safeSearch}%'` : ""}`;
    } else if (type === "supplier") {
      query = `SELECT TOP 50 SupplierCode AS Code, SupplierName AS Name, Address1, Address2 FROM tblsupplier ${safeSearch ? `WHERE SupplierCode LIKE '%${safeSearch}%' OR SupplierName LIKE '%${safeSearch}%'` : ""}`;
    } else if (type === "salesman") {
      query = `SELECT TOP 50 SalesManCode AS Code, SalesManName AS Name FROM tblSalesMan ${safeSearch ? `WHERE SalesManCode LIKE '%${safeSearch}%' OR SalesManName LIKE '%${safeSearch}%'` : ""}`;
    } else if (type === "size") {
      query = `SELECT TOP 50 SizeCode AS Code FROM tblSize ${safeSearch ? `WHERE SizeCode LIKE '%${safeSearch}%'` : ""}`;
    } else if (type === "invoice") {
      if (mode === "Installment") {
        query = `SELECT TOP 50 InsAgreementNo AS Code, LocationCode AS LocationC, CustomerCode, Name FROM tblInstallmentEntryHeader ${safeSearch ? `WHERE InsAgreementNo LIKE '%${safeSearch}%' OR Name LIKE '%${safeSearch}%'` : ""}`;
      } else {
        query = `SELECT TOP 50 OrderNo AS Code, OrderDate, CustomerCode, SupplierCode FROM tblInventoryOrderHeader ${safeSearch ? `WHERE OrderNo LIKE '%${safeSearch}%'` : ""}`;
      }
    } else if (type === "inventory") {
      query = `SELECT TOP 50 InventoryCode AS Code, Description AS Name FROM tblInventory ${safeSearch ? `WHERE InventoryCode LIKE '%${safeSearch}%' OR Description LIKE '%${safeSearch}%'` : ""}`;
    }

    if (!query) {
      return res.json({ success: false, message: "Invalid LOV type" });
    }

    const result = await pool.request().query(query);
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    res.status(500).json({ success: false, message: err.message });
  }
});

// Validate Salesman Password
router.post("/validate-salesman", async (req, res) => {
  try {
    const { password } = req.body;
    if (!password) {
      return res.status(400).json({ success: false, message: "Password is required" });
    }
    const pool = await getPool(req);
    const result = await pool.request()
      .input("Password", sql.VarChar, password)
      .query("SELECT SalesmanCode FROM tblSalesMan WHERE Password = @Password");

    if (result.recordset.length > 0) {
      res.json({ success: true, salesmanCode: result.recordset[0].SalesmanCode });
    } else {
      res.status(401).json({ success: false, message: "Invalid SalesMan Password" });
    }
  } catch (err) {
    console.error("Validate Salesman Error:", err);
    res.status(500).json({ success: false, message: "Error validating salesman", error: err.message });
  }
});

// GET Inventory Info
router.get("/inventory/:inventoryCode", async (req, res) => {
  try {
    const { inventoryCode } = req.params;
    const pool = await getPool(req);
    const result = await pool.request()
      .input("InventoryCode", sql.VarChar, inventoryCode.trim())
      .query(`
        SELECT InventoryCode, Description, Weight, GoldLoss, CostPerGram, TotalCost, 
               ClassCode, CategoryCode, CatalogueCode, SalesBillNo
        FROM tblInventory 
        WHERE InventoryCode = @InventoryCode
      `);

    if (result.recordset.length > 0) {
      const item = result.recordset[0];
      if (item.SalesBillNo && item.SalesBillNo.trim() !== '') {
        return res.json({ success: false, message: "Item is already Sold out." });
      }
      item.AverageCost = item.CostPerGram || item.TotalCost;
      res.json({ success: true, item });
    } else {
      res.json({ success: false, message: "Inventory not found" });
    }
  } catch (err) {
    res.status(500).json({ success: false, message: err.message });
  }
});

// GET Existing Header
router.get("/header/:invoiceNo", async (req, res) => {
  try {
    const { invoiceNo } = req.params;
    const { type } = req.query; // 'Installment' or 'Order'
    const pool = await getPool(req);

    if (type === 'Installment') {
      const headerResult = await pool.request()
        .input("InvoiceNo", sql.VarChar, invoiceNo.trim())
        .query("SELECT * FROM tblInstallmentEntryHeader WHERE InsAgreementNo = @InvoiceNo");

      if (headerResult.recordset.length > 0) {
        const detailsResult = await pool.request()
          .input("InvoiceNo", sql.VarChar, invoiceNo.trim())
          .query("SELECT * FROM tblInstallmentEntryDetail WHERE InsAgreementNo = @InvoiceNo");

        return res.json({ success: true, header: headerResult.recordset[0], details: detailsResult.recordset });
      }
    } else {
      const headerResult = await pool.request()
        .input("InvoiceNo", sql.VarChar, invoiceNo.trim())
        .query(`
          SELECT h.*, 
                 c.CustomerName as Name, 
                 c.CustomerName,
                 c.Address1, 
                 c.Address2, 
                 c.Address3, 
                 c.CustomerType 
          FROM tblInventoryOrderHeader h
          LEFT JOIN tblCustomer c ON h.CustomerCode = c.CustomerCode
          WHERE h.OrderNo = @InvoiceNo
        `);

      if (headerResult.recordset.length > 0) {
        const detailsResult = await pool.request()
          .input("InvoiceNo", sql.VarChar, invoiceNo.trim())
          .query("SELECT * FROM tblInventoryOrderdetail WHERE OrderNo = @InvoiceNo");

        return res.json({ success: true, header: headerResult.recordset[0], details: detailsResult.recordset });
      }
    }

    res.json({ success: false, message: "Record not found" });
  } catch (err) {
    res.status(500).json({ success: false, message: err.message });
  }
});

// POST Save Installment or Order
router.post("/save", async (req, res) => {
  let currentStep = "Initializing save";
  const pool = await getPool(req);
  const transaction = new sql.Transaction(pool);
  try {
    const { mode, header, details } = req.body;
    await transaction.begin();

    if (mode === 'Installment') {
      // Check column names of tblInstallmentEntryHeader dynamically
      let insCols = [];
      try {
        const cRes = await new sql.Request(transaction).query(
          "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tblInstallmentEntryHeader'"
        );
        insCols = cRes.recordset.map(c => c.COLUMN_NAME.toLowerCase());
      } catch (e) {}
      const hasInsCol = (name) => insCols.length === 0 || insCols.includes(name.toLowerCase());

      const request = transaction.request();
      request
        .input("InsAgreementNo", sql.VarChar, header.invoiceNo || '')
        .input("Invdate", sql.DateTime, header.date || new Date())
        .input("GSTType", sql.VarChar, header.gstType || '')
        .input("LocationCode", sql.VarChar, header.locationCode || '01')
        .input("CustomerCode", sql.VarChar, header.customerCode || '')
        .input("CustomerType", sql.VarChar, header.customerType || '')
        .input("Name", sql.VarChar, header.customerName || '')
        .input("Address1", sql.VarChar, header.address1 || '')
        .input("Address2", sql.VarChar, header.address2 || '')
        .input("Address3", sql.VarChar, header.address3 || '')
        .input("SalesManCode", sql.VarChar, header.salesManCode || '')
        .input("DepositAmt", sql.Decimal(18, 2), header.depositAmt || 0)
        .input("TotalInstallment", sql.Int, header.totalInstallment || 0)
        .input("AmtPaid", sql.Decimal(18, 2), header.amountPaid || 0)
        .input("InstallmentAmt", sql.Decimal(18, 2), header.installmentAmt || 0)
        .input("ReturnAmount", sql.Decimal(18, 2), header.returnAmount || 0)
        .input("LastDate", sql.DateTime, header.lastDate || new Date())
        .input("Remarks", sql.VarChar, header.remarks || '')
        .input("TotalCost", sql.Decimal(18, 2), header.totalCost || 0)
        .input("TradeINAmount", sql.Decimal(18, 2), header.tradeInAmount || 0)
        .input("TradeInGST", sql.Decimal(18, 2), header.tradeInGst || 0)
        .input("CreateUser", sql.VarChar, header.salesManCode || '');

      const checkIns = await new sql.Request(transaction)
        .input("InsCheck", sql.VarChar, header.invoiceNo || '')
        .query("SELECT 1 FROM tblInstallmentEntryHeader WHERE InsAgreementNo = @InsCheck");

      if (checkIns.recordset.length > 0) {
        const updateSets = [
          "InvoiceDate = @Invdate",
          "GSTType = @GSTType",
          "LocationCode = @LocationCode",
          "CustomerCode = @CustomerCode",
          "Name = @Name",
          "Address1 = @Address1",
          "Address2 = @Address2",
          "Address3 = @Address3",
          "SalesMan = @SalesManCode",
          "DepositAmt = @DepositAmt",
          "TotalInstallment = @TotalInstallment",
          "AmtPaid = @AmtPaid",
          "InstallmentAmt = @InstallmentAmt",
          "LastDate = @LastDate",
          "Remarks = @Remarks",
          "TotalCost = @TotalCost",
          "TradeINAmt = @TradeINAmount",
          "TradeInGST = @TradeInGST"
        ];
        if (hasInsCol("ModifyBy")) updateSets.push("ModifyBy = @CreateUser");
        if (hasInsCol("ModifyUser")) updateSets.push("ModifyUser = @CreateUser");
        if (hasInsCol("ModifyDate")) updateSets.push("ModifyDate = GETDATE()");

        await request.query(`
          UPDATE tblInstallmentEntryHeader SET
            ${updateSets.join(", ")}
          WHERE InsAgreementNo = @InsAgreementNo
        `);
      } else {
        const insColsList = [
          "InsAgreementNo", "InvoiceDate", "GSTType", "LocationCode", "CustomerCode", "Name",
          "Address1", "Address2", "Address3", "SalesMan", "DepositAmt", "TotalInstallment", "AmtPaid",
          "InstallmentAmt", "LastDate", "Remarks", "TotalCost", "TradeINAmt", "TradeInGST"
        ];
        const insValsList = [
          "@InsAgreementNo", "@Invdate", "@GSTType", "@LocationCode", "@CustomerCode", "@Name",
          "@Address1", "@Address2", "@Address3", "@SalesManCode", "@DepositAmt", "@TotalInstallment", "@AmtPaid",
          "@InstallmentAmt", "@LastDate", "@Remarks", "@TotalCost", "@TradeINAmount", "@TradeInGST"
        ];
        if (hasInsCol("CreateBy")) { insColsList.push("CreateBy"); insValsList.push("@CreateUser"); }
        if (hasInsCol("CreateUser")) { insColsList.push("CreateUser"); insValsList.push("@CreateUser"); }
        if (hasInsCol("Createdate")) { insColsList.push("Createdate"); insValsList.push("GETDATE()"); }
        if (hasInsCol("CreateDate")) { insColsList.push("CreateDate"); insValsList.push("GETDATE()"); }

        await request.query(`
          INSERT INTO tblInstallmentEntryHeader (${insColsList.join(", ")})
          VALUES (${insValsList.join(", ")})
        `);
      }

      // Delete existing details before re-inserting
      const delReq = transaction.request();
      await delReq
        .input("InsAgreementNo", sql.VarChar, header.invoiceNo || '')
        .query(`DELETE FROM tblInstallmentEntryDetail WHERE InsAgreementNo = @InsAgreementNo`);

      for (let item of details) {
        const itemReq = transaction.request();
        await itemReq
          .input("LocationCode", sql.VarChar, header.locationCode || '01')
          .input("InsAgreementNo", sql.VarChar, header.invoiceNo || '')
          .input("InventoryCode", sql.VarChar, item.InventoryCode || '')
          .input("Description", sql.VarChar, item.Description || '')
          .input("Weight", sql.Decimal(18, 3), item.Weight || 0)
          .input("GoldLoss", sql.Decimal(18, 3), item.GoldLoss || 0)
          .input("Cost", sql.Decimal(18, 2), item.Cost || 0)
          .input("PCS", sql.Int, item.PCS || 1)
          .input("SMC", sql.Decimal(18, 2), item.SMC || 0)
          .input("GST", sql.Decimal(18, 2), item.GST || 0)
          .input("TotalValue", sql.Decimal(18, 2), item.TotalValue || 0)
          .input("DetailDiscount", sql.Decimal(18, 2), item.DetailDiscount || 0)
          .input("CreateUser", sql.VarChar, header.salesManCode || '')
          .query(`
            INSERT INTO tblInstallmentEntryDetail (
              Locationcode, InsAgreementNo, InventoryCode, Description, Weight, Goldloss, Cost, 
              PCS, SMC, GST, TotalValue, DetailDiscount, CreateUser
            ) VALUES (
              @LocationCode, @InsAgreementNo, @InventoryCode, @Description, @Weight, @GoldLoss, @Cost, 
              @PCS, @SMC, @GST, @TotalValue, @DetailDiscount, @CreateUser
            )
          `);
      }
    } else {
      // Handle Order Save
      currentStep = "Discovering Order schema";
      
      // Helper to fetch schema columns with both sys and INFORMATION_SCHEMA
      const getTableSchema = async (tableName) => {
        const lowerName = tableName.toLowerCase();
        let cols = {};
        let colTypes = {};
        let colNullable = {};
        let found = false;

        // Try 1: sys.columns + sys.tables (most accurate on MSSQL)
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

        // Try 2: INFORMATION_SCHEMA.COLUMNS
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

      const orderSchema = await getTableSchema("tblInventoryOrderHeader");
      const detailSchema = await getTableSchema("tblInventoryOrderDetail");

      console.log("Order Header Schema found:", orderSchema.found, Object.keys(orderSchema.cols).length, "cols");
      console.log("Order Detail Schema found:", detailSchema.found, Object.keys(detailSchema.cols).length, "cols");

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

      const hasOrderCol = (name) => {
        const lower = name.toLowerCase();
        if (orderSchema.found) {
          return orderSchema.cols.hasOwnProperty(lower);
        }
        // Fallback standard columns if schema lookup completely unavailable
        return true;
      };

      const getOrderColLen = (name, def = 50) => {
        const len = orderSchema.cols[name.toLowerCase()];
        return len && len > 0 ? len : def;
      };

      const getOrderColType = (name) => orderSchema.colTypes[name.toLowerCase()] || "";

      // Smart safe parameter binding for Header
      const bindHeaderParam = (reqObj, paramName, colName, rawVal, fallbackType = "varchar", fallbackLen = 50) => {
        const lower = colName.toLowerCase();
        const dt = orderSchema.colTypes[lower] || fallbackType;
        const colLen = getOrderColLen(colName, fallbackLen);
        const isNullable = orderSchema.colNullable[lower] !== false;

        if (isNumericType(dt)) {
          const num = parseFloat(rawVal);
          const isNum = !isNaN(num) && rawVal !== "" && rawVal !== null && rawVal !== undefined;
          if (dt.includes("int") || dt.includes("smallint") || dt.includes("tinyint")) {
            if (isNum) {
              reqObj.input(paramName, sql.Int, parseInt(num, 10));
            } else {
              if (isNullable) reqObj.input(paramName, sql.Int, null);
              else reqObj.input(paramName, sql.Int, 0);
            }
          } else {
            if (isNum) {
              reqObj.input(paramName, sql.Decimal(18, 4), num);
            } else {
              if (isNullable) reqObj.input(paramName, sql.Decimal(18, 4), null);
              else reqObj.input(paramName, sql.Decimal(18, 4), 0);
            }
          }
        } else if (isDateType(dt) || fallbackType === "datetime") {
          const d = rawVal ? new Date(rawVal) : new Date();
          const validDate = !isNaN(d.getTime()) ? d : new Date();
          reqObj.input(paramName, sql.DateTime, validDate);
        } else {
          // VarChar/Char
          const s = safeStr(rawVal, colLen);
          reqObj.input(paramName, sql.VarChar(colLen), s);
        }
      };

      currentStep = "Binding Header Parameters";
      const request = transaction.request();

      // Format RateFixed
      const isRateFixedY = header.rateFixed === "Y" || header.rateFixed === "1" || header.rateFixed === "Yes";
      const rateFixedType = getOrderColType("ratefixed");
      let formattedRateFixed;
      if (isNumericType(rateFixedType) || rateFixedType === "bit") {
        formattedRateFixed = isRateFixedY ? 1 : 0;
      } else {
        formattedRateFixed = isRateFixedY ? "Y" : "N";
      }

      // Format Status
      const statusType = getOrderColType("status");
      let formattedStatus;
      if (isNumericType(statusType)) {
        formattedStatus = parseInt(header.status, 10) || 1;
      } else {
        formattedStatus = header.status ? safeStr(header.status, getOrderColLen("status", 10)) : "O";
      }

      // Format InvType
      const invTypeType = getOrderColType("invtype");
      let formattedInvType;
      if (isNumericType(invTypeType)) {
        formattedInvType = parseInt(header.invType, 10) || 0;
      } else {
        formattedInvType = header.invType ? safeStr(header.invType, getOrderColLen("invtype", 2)) : "O";
      }

      // Format TradeInType
      const tradeInColName = hasOrderCol("stradeintype") ? "stradeintype" : (hasOrderCol("tradeintype") ? "tradeintype" : null);

      bindHeaderParam(request, "OrderNo", "orderno", header.invoiceNo, "varchar", 15);
      bindHeaderParam(request, "OrderDate", "orderdate", header.orderDate, "datetime");
      bindHeaderParam(request, "CustomerCode", "customercode", header.customerCode, "varchar", 15);
      bindHeaderParam(request, "SupplierCode", "suppliercode", header.supplierCode, "varchar", 15);
      bindHeaderParam(request, "DeliveryDate", "deliverydate", header.lastDate, "datetime");
      bindHeaderParam(request, "CostPerGram", "costpergram", header.costPerGram, "decimal");
      
      if (tradeInColName) {
        bindHeaderParam(request, "TradeInType", tradeInColName, header.tradeInType || "0", "varchar", 2);
      }

      bindHeaderParam(request, "RateFixed", "ratefixed", formattedRateFixed, isNumericType(rateFixedType) ? "int" : "varchar", 5);
      bindHeaderParam(request, "TotalCostEstimated", "totalcostestimated", header.totalCost, "decimal");
      bindHeaderParam(request, "AdvancePaid", "advancepaid", header.depositAmt, "decimal");
      bindHeaderParam(request, "Status", "status", formattedStatus, isNumericType(statusType) ? "int" : "varchar", 10);
      bindHeaderParam(request, "Remarks", "remarks", header.remarks, "varchar", 150);
      bindHeaderParam(request, "InvType", "invtype", formattedInvType, isNumericType(invTypeType) ? "int" : "varchar", 2);
      bindHeaderParam(request, "SupplierCost", "suppliercost", header.supplierCost, "decimal");
      bindHeaderParam(request, "EstimatedGST", "estimatedgst", header.estimatedGst, "decimal");
      bindHeaderParam(request, "IssueDate", "issuedate", header.issueDate, "datetime");
      bindHeaderParam(request, "SalesManCode", "salesmancode", header.salesManCode || "ADMIN", "varchar", 10);
      bindHeaderParam(request, "TradeINAmount", "tradeinamount", header.tradeInAmount, "decimal");
      bindHeaderParam(request, "TradeInGST", "tradeingst", header.tradeInGst, "decimal");
      bindHeaderParam(request, "CreateUser", "createuser", header.salesManCode || "ADMIN", "varchar", 20);

      currentStep = "Checking Existing Order Header";
      const checkOrder = await new sql.Request(transaction)
        .input("OrderCheck", sql.VarChar(getOrderColLen("orderno", 15)), safeStr(header.invoiceNo, getOrderColLen("orderno", 15)))
        .query("SELECT 1 FROM tblInventoryOrderHeader WHERE OrderNo = @OrderCheck");

      if (checkOrder.recordset.length > 0) {
        currentStep = "Updating tblInventoryOrderHeader";
        const updateSets = [];
        const colMap = [
          { col: "OrderDate", val: "@OrderDate" },
          { col: "CustomerCode", val: "@CustomerCode" },
          { col: "SupplierCode", val: "@SupplierCode" },
          { col: "DeliveryDate", val: "@DeliveryDate" },
          { col: "CostPerGram", val: "@CostPerGram" },
          { col: "RateFixed", val: "@RateFixed" },
          { col: "TotalCostEstimated", val: "@TotalCostEstimated" },
          { col: "AdvancePaid", val: "@AdvancePaid" },
          { col: "Status", val: "@Status" },
          { col: "Remarks", val: "@Remarks" },
          { col: "InvType", val: "@InvType" },
          { col: "SupplierCost", val: "@SupplierCost" },
          { col: "EstimatedGST", val: "@EstimatedGST" },
          { col: "IssueDate", val: "@IssueDate" },
          { col: "SalesManCode", val: "@SalesManCode" },
          { col: "TradeINAmount", val: "@TradeINAmount" },
          { col: "TradeInGST", val: "@TradeInGST" }
        ];

        if (tradeInColName) {
          const actualCol = orderSchema.found 
            ? (orderSchema.cols.hasOwnProperty("stradeintype") ? "sTradeInType" : "TradeInType")
            : "sTradeInType";
          colMap.push({ col: actualCol, val: "@TradeInType" });
        }

        for (const item of colMap) {
          if (hasOrderCol(item.col)) {
            updateSets.push(`${item.col} = ${item.val}`);
          }
        }
        if (hasOrderCol("ModifyBy")) updateSets.push("ModifyBy = @CreateUser");
        if (hasOrderCol("ModifyUser")) updateSets.push("ModifyUser = @CreateUser");
        if (hasOrderCol("ModifyDate")) updateSets.push("ModifyDate = GETDATE()");

        if (updateSets.length > 0) {
          await request.query(`
            UPDATE tblInventoryOrderHeader SET
              ${updateSets.join(", ")}
            WHERE OrderNo = @OrderNo
          `);
        }
      } else {
        currentStep = "Inserting tblInventoryOrderHeader";
        const insColsList = ["OrderNo"];
        const insValsList = ["@OrderNo"];

        const colMap = [
          { col: "OrderDate", val: "@OrderDate" },
          { col: "CustomerCode", val: "@CustomerCode" },
          { col: "SupplierCode", val: "@SupplierCode" },
          { col: "DeliveryDate", val: "@DeliveryDate" },
          { col: "CostPerGram", val: "@CostPerGram" },
          { col: "RateFixed", val: "@RateFixed" },
          { col: "TotalCostEstimated", val: "@TotalCostEstimated" },
          { col: "AdvancePaid", val: "@AdvancePaid" },
          { col: "Status", val: "@Status" },
          { col: "Remarks", val: "@Remarks" },
          { col: "InvType", val: "@InvType" },
          { col: "SupplierCost", val: "@SupplierCost" },
          { col: "EstimatedGST", val: "@EstimatedGST" },
          { col: "IssueDate", val: "@IssueDate" },
          { col: "SalesManCode", val: "@SalesManCode" },
          { col: "TradeINAmount", val: "@TradeINAmount" },
          { col: "TradeInGST", val: "@TradeInGST" }
        ];

        if (tradeInColName) {
          const actualCol = orderSchema.found 
            ? (orderSchema.cols.hasOwnProperty("stradeintype") ? "sTradeInType" : "TradeInType")
            : "sTradeInType";
          colMap.push({ col: actualCol, val: "@TradeInType" });
        }

        for (const item of colMap) {
          if (hasOrderCol(item.col)) {
            insColsList.push(item.col);
            insValsList.push(item.val);
          }
        }
        if (hasOrderCol("CreateBy")) { insColsList.push("CreateBy"); insValsList.push("@CreateUser"); }
        if (hasOrderCol("CreateUser")) { insColsList.push("CreateUser"); insValsList.push("@CreateUser"); }
        if (hasOrderCol("Createdate")) { insColsList.push("Createdate"); insValsList.push("GETDATE()"); }
        if (hasOrderCol("CreateDate")) { insColsList.push("CreateDate"); insValsList.push("GETDATE()"); }

        await request.query(`
          INSERT INTO tblInventoryOrderHeader (${insColsList.join(", ")})
          VALUES (${insValsList.join(", ")})
        `);
      }

      currentStep = "Deleting existing tblInventoryOrderDetail";
      // Delete existing details before re-inserting
      const delReq = transaction.request();
      await delReq
        .input("OrderNo", sql.VarChar(20), safeStr(header.invoiceNo, 20))
        .query(`DELETE FROM tblInventoryOrderDetail WHERE OrderNo = @OrderNo`);

      const hasDetailCol = (name) => {
        const lower = name.toLowerCase();
        if (detailSchema.found) {
          return detailSchema.cols.hasOwnProperty(lower);
        }
        // Known fallback columns
        const knownCols = [
          "description", "orderinventorycode", "stonedescription", "classcode", 
          "categorycode", "cataloguecode", "weightestimated", "stoneweight", 
          "goldloss", "mc", "costpergram", "estimatedcost", "estimatedgst", 
          "stonecost", "sizecode", "sizetype", "createduser", "createuser", "createby"
        ];
        return knownCols.includes(lower);
      };

      const getDetailColLen = (name, def = 50) => {
        const len = detailSchema.cols[name.toLowerCase()];
        return len && len > 0 ? len : def;
      };

      const getDetailColType = (name) => detailSchema.colTypes[name.toLowerCase()] || "";

      currentStep = "Inserting tblInventoryOrderDetail lines";
      for (let idx = 0; idx < (details || []).length; idx++) {
        const item = details[idx];
        const itemReq = transaction.request();
        const dCols = ["OrderNo"];
        const dVals = ["@OrderNo"];

        itemReq.input("OrderNo", sql.VarChar(getDetailColLen("orderno", 15)), safeStr(header.invoiceNo, getDetailColLen("orderno", 15)));

        const bindDetailParam = (paramName, colName, rawVal, fallbackType = "varchar", fallbackLen = 50) => {
          const lower = colName.toLowerCase();
          const dt = detailSchema.colTypes[lower] || fallbackType;
          const colLen = getDetailColLen(colName, fallbackLen);
          const isNullable = detailSchema.colNullable[lower] !== false;

          if (isNumericType(dt)) {
            const num = parseFloat(rawVal);
            const isNum = !isNaN(num) && rawVal !== "" && rawVal !== null && rawVal !== undefined;
            if (dt.includes("int") || dt.includes("smallint") || dt.includes("tinyint")) {
              if (isNum) {
                itemReq.input(paramName, sql.Int, parseInt(num, 10));
              } else {
                if (isNullable) itemReq.input(paramName, sql.Int, null);
                else itemReq.input(paramName, sql.Int, 0);
              }
            } else {
              if (isNum) {
                itemReq.input(paramName, sql.Decimal(18, 4), num);
              } else {
                if (isNullable) itemReq.input(paramName, sql.Decimal(18, 4), null);
                else itemReq.input(paramName, sql.Decimal(18, 4), 0);
              }
            }
          } else if (isDateType(dt) || fallbackType === "datetime") {
            const d = rawVal ? new Date(rawVal) : new Date();
            const validDate = !isNaN(d.getTime()) ? d : new Date();
            itemReq.input(paramName, sql.DateTime, validDate);
          } else {
            const s = safeStr(rawVal, colLen);
            itemReq.input(paramName, sql.VarChar(colLen), s);
          }
        };

        // OrderInventoryCode: Line sequence number (1, 2, 3...)
        const lineSequence = idx + 1;
        const ordInvVal = item.OrderInventoryCode || lineSequence;

        const detailFieldDefs = [
          { col: "Description", param: "Description", val: item.Description, type: "varchar", len: 80 },
          { col: "OrderInventoryCode", param: "OrderInventoryCode", val: ordInvVal, type: "int", len: 15 },
          { col: "StoneDescription", param: "StoneDescription", val: item.StoneDescription, type: "varchar", len: 80 },
          { col: "ClassCode", param: "ClassCode", val: item.ClassCode, type: "varchar", len: 10 },
          { col: "CategoryCode", param: "CategoryCode", val: item.CategoryCode, type: "varchar", len: 10 },
          { col: "CatalogueCode", param: "CatalogueCode", val: item.CatalogueCode, type: "varchar", len: 10 },
          { col: "WeightEstimated", param: "WeightEstimated", val: item.Weight, type: "decimal", len: null },
          { col: "StoneWeight", param: "StoneWeight", val: item.StoneWeight, type: "decimal", len: null },
          { col: "GoldLoss", param: "GoldLoss", val: item.GoldLoss, type: "decimal", len: null },
          { col: "MC", param: "MC", val: item.SMC, type: "decimal", len: null },
          { col: "CostPerGram", param: "CostPerGram", val: item.Cost, type: "decimal", len: null },
          { col: "EstimatedCost", param: "EstimatedCost", val: item.TotalValue, type: "decimal", len: null },
          { col: "EstimatedGst", param: "EstimatedGst", val: item.GST, type: "decimal", len: null },
          { col: "StoneCost", param: "StoneCost", val: item.StoneCost, type: "decimal", len: null },
          { col: "SizeCode", param: "SizeCode", val: item.SizeCode, type: "varchar", len: 5 },
          { col: "SizeType", param: "SizeType", val: item.SizeType, type: "varchar", len: 5 }
        ];

        // Also add InventoryCode column if tblInventoryOrderDetail has it
        if (hasDetailCol("InventoryCode")) {
          detailFieldDefs.push({
            col: "InventoryCode",
            param: "InventoryCode",
            val: item.InventoryCode || "",
            type: "varchar",
            len: 20
          });
        }

        for (const def of detailFieldDefs) {
          if (hasDetailCol(def.col)) {
            dCols.push(def.col);
            dVals.push(`@${def.param}`);
            bindDetailParam(def.param, def.col, def.val, def.type, def.len);
          }
        }

        // Handle audit and user columns in tblInventoryOrderDetail (non-null required)
        const detailUser = safeStr(header.salesManCode || "ADMIN", 20) || "ADMIN";
        if (hasDetailCol("CreatedUser")) {
          dCols.push("CreatedUser");
          dVals.push("@CreatedUser");
          itemReq.input("CreatedUser", sql.VarChar(getDetailColLen("createduser", 20)), detailUser);
        }
        if (hasDetailCol("CreateUser")) {
          dCols.push("CreateUser");
          dVals.push("@CreateUser");
          itemReq.input("CreateUser", sql.VarChar(getDetailColLen("createuser", 20)), detailUser);
        }
        if (hasDetailCol("CreateBy")) {
          dCols.push("CreateBy");
          dVals.push("@CreateBy");
          itemReq.input("CreateBy", sql.VarChar(getDetailColLen("createby", 20)), detailUser);
        }
        if (hasDetailCol("CreatedDate")) {
          dCols.push("CreatedDate");
          dVals.push("GETDATE()");
        }
        if (hasDetailCol("CreateDate")) {
          dCols.push("CreateDate");
          dVals.push("GETDATE()");
        }

        await itemReq.query(`
          INSERT INTO tblInventoryOrderDetail (${dCols.join(", ")})
          VALUES (${dVals.join(", ")})
        `);
      }
    }

    currentStep = "Committing Transaction";
    await transaction.commit();
    res.json({ success: true, message: "Saved successfully!" });
  } catch (err) {
    console.error(`Installment/Order save error at [${currentStep}]:`, err);
    try {
      await transaction.rollback();
    } catch (rbErr) {}
    res.status(500).json({
      success: false,
      message: err.message || "Failed to save",
      step: currentStep,
      sqlError: err.message,
      detail: err.originalError ? err.originalError.message : undefined,
      stack: err.stack
    });
  }
});

module.exports = router;
