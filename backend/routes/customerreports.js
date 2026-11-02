const express = require("express");
const router = express.Router();
const { sql } = require("../db");
const { validateBackendDates } = require("../utils/validateReportDates");

// Helper to parse date string (YYYY-MM-DD or DD/MM/YYYY) to Date
const parseDateParam = (dateStr) => {
  if (!dateStr) return new Date();
  if (dateStr.includes("/")) {
    const parts = dateStr.split("/");
    if (parts.length === 3) {
      return new Date(`${parts[2]}-${parts[1]}-${parts[0]}`);
    }
  }
  return new Date(dateStr);
};

// Helper for inclusive date range: [FromDate, ToDateExclusive)
const getInclusiveRange = (fromDateStr, toDateStr) => {
  const from = parseDateParam(fromDateStr);
  from.setHours(0, 0, 0, 0);

  const to = parseDateParam(toDateStr);
  to.setHours(0, 0, 0, 0);

  const toExclusive = new Date(to);
  toExclusive.setDate(toExclusive.getDate() + 1);

  return { fromDate: from, toDateExclusive: toExclusive };
};

// 1. GET /api/customerreports/nationalities
// Populated from tblNationality, ordered by Description
router.get("/nationalities", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query(`
      SELECT DISTINCT RTRIM(Code) AS Code, RTRIM(Description) AS Description
      FROM tblNationality
      WHERE Description IS NOT NULL AND RTRIM(Description) <> ''
      ORDER BY Description ASC
    `);

    // Also get distinct from tblCustomer Address4 to ensure all present nationalities can be filtered
    const custNatResult = await pool.request().query(`
      SELECT DISTINCT RTRIM(Address4) AS Description
      FROM tblCustomer
      WHERE Address4 IS NOT NULL AND RTRIM(Address4) <> ''
      ORDER BY RTRIM(Address4) ASC
    `);

    const set = new Set();
    const list = [];

    // Add standard nationality master descriptions
    result.recordset.forEach((row) => {
      const desc = (row.Description || "").trim();
      if (desc && !set.has(desc.toUpperCase())) {
        set.add(desc.toUpperCase());
        list.push({ Code: (row.Code || desc).trim(), Description: desc });
      }
    });

    // Add any customer addresses not in tblNationality
    custNatResult.recordset.forEach((row) => {
      const desc = (row.Description || "").trim();
      if (desc && !set.has(desc.toUpperCase())) {
        set.add(desc.toUpperCase());
        list.push({ Code: desc, Description: desc });
      }
    });

    res.json({ success: true, data: list });
  } catch (err) {
    console.error("Error fetching nationalities:", err);
    res.status(500).json({ success: false, message: err.message });
  }
});

// 2. GET /api/customerreports/customers-search
// Search help endpoint for customer code / name
router.get("/customers-search", async (req, res) => {
  try {
    const pool = req.pool;
    const { search = "", limit = 50 } = req.query;
    const cleanSearch = (search || "").trim();

    const request = pool.request();
    let query = `
      SELECT TOP (${parseInt(limit, 10) || 50})
        RTRIM(CustomerCode) AS CustomerCode,
        RTRIM(CustomerName) AS CustomerName,
        RTRIM(CustomerType) AS CustomerType,
        RTRIM(Mobile) AS Mobile,
        RTRIM(Phone1) AS Phone,
        RTRIM(Address4) AS Nationality
      FROM tblCustomer
    `;

    if (cleanSearch) {
      request.input("searchVal", sql.VarChar, `%${cleanSearch}%`);
      query += ` WHERE CustomerCode LIKE @searchVal OR CustomerName LIKE @searchVal OR Mobile LIKE @searchVal OR NricNo LIKE @searchVal `;
    }

    query += ` ORDER BY CustomerName ASC `;

    const result = await request.query(query);
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Error searching customers:", err);
    res.status(500).json({ success: false, message: err.message });
  }
});

// 3. GET /api/customerreports/report
// Main report generation endpoint supporting all 12 reports
router.get("/report", async (req, res) => {
  try {
    const pool = req.pool;
    const {
      reportId = "customers_list",
      customerType = "ALL",
      nationality = "ALL",
      nricType = "ALL",
      religion = "ALL",
      dobType = "DOB",
      contact = "ALL",
      fromDate,
      toDate,
      customerCode = "",
    } = req.query;

    if (fromDate || toDate) {
      const dateVal = validateBackendDates(fromDate, toDate);
      if (!dateVal.isValid) {
        return res.status(400).json({ success: false, message: dateVal.message });
      }
    }

    const request = pool.request();

    // Helper: Add common customer filters to query
    const applyCustomerFilters = (alias = "c") => {
      let conditions = [];

      // Customer Type
      if (customerType && customerType !== "ALL") {
        request.input("CustomerType", sql.VarChar, customerType.trim());
        conditions.push(`RTRIM(${alias}.CustomerType) = @CustomerType`);
      }

      // Nationality (stored in Address4)
      if (nationality && nationality !== "ALL") {
        request.input("Nationality", sql.VarChar, `%${nationality.trim()}%`);
        conditions.push(`RTRIM(${alias}.Address4) LIKE @Nationality`);
      }

      // Religion (stored in DateType)
      if (religion && religion !== "ALL") {
        request.input("Religion", sql.VarChar, religion.trim());
        conditions.push(`RTRIM(${alias}.DateType) = @Religion`);
      }

      // NRIC Type: ALL, SINGAPOREAN/SPR, OTHERS
      if (nricType === "SINGAPOREAN/SPR") {
        conditions.push(
          `(${alias}.Address4 LIKE '%SINGAPORE%' OR ${alias}.NricNo LIKE '[STFGstfg]%' OR RTRIM(${alias}.AccountType) = 'S')`
        );
      } else if (nricType === "OTHERS") {
        conditions.push(
          `NOT (${alias}.Address4 LIKE '%SINGAPORE%' OR ${alias}.NricNo LIKE '[STFGstfg]%' OR RTRIM(${alias}.AccountType) = 'S')`
        );
      }

      // Contact Option: SMS ('S'), Phone ('P'), None ('N')
      if (contact === "SMS") {
        conditions.push(`RTRIM(${alias}.AccountType) = 'S'`);
      } else if (contact === "Phone") {
        conditions.push(`RTRIM(${alias}.AccountType) = 'P'`);
      } else if (contact === "None") {
        conditions.push(`RTRIM(${alias}.AccountType) = 'N'`);
      }

      return conditions.length > 0 ? ` AND ` + conditions.join(" AND ") : "";
    };

    let data = [];
    let summary = null;

    // =========================================================================
    // Report 1: Customer Master Report (rptMasterCustomer.rpt / customers_list)
    // =========================================================================
    if (reportId === "customers_list" || reportId === "master") {
      let query = `
        SELECT 
          RTRIM(c.CustomerCode) AS [Cust.Code],
          RTRIM(c.CustomerName) AS [CustomerName],
          RTRIM(c.Address1) AS Address1,
          RTRIM(c.Address2) AS Address2,
          RTRIM(c.Address3) AS Address3,
          RTRIM(c.Address4) AS Address4,
          RTRIM(c.Phone1) AS Phone1,
          RTRIM(c.Phone2) AS Phone2,
          RTRIM(c.Mobile) AS Mobile,
          RTRIM(c.Fax) AS Fax,
          RTRIM(c.eMail) AS Email,
          RTRIM(c.PassPortNo) AS PassPortNo,
          RTRIM(c.CustomerType) AS CustomerType,
          RTRIM(c.DateType) AS Religion,
          RTRIM(c.NricNo) AS NricNo,
          RTRIM(c.AccountType) AS ContactOption,
          c.CreateDate,
          c.DOB,
          c.DOA
        FROM tblCustomer c
        WHERE (c.GLAccount1 = 'Y' OR c.GLAccount1 IS NULL OR RTRIM(c.GLAccount1) = '')
      `;

      query += applyCustomerFilters("c");

      if (dobType === "DOB" && fromDate && toDate) {
        query += ` AND c.DOB >= '${fromDate.replace(/[-/]/g, '')} 00:00:00' AND c.DOB <= '${toDate.replace(/[-/]/g, '')} 23:59:59'`;
      } else if (dobType === "DOA" && fromDate && toDate) {
        query += ` AND c.DOA >= '${fromDate.replace(/[-/]/g, '')} 00:00:00' AND c.DOA <= '${toDate.replace(/[-/]/g, '')} 23:59:59'`;
      }

      query += ` ORDER BY c.CustomerName ASC`;
      const result = await request.query(query);
      
      // Transform records to the exact columns of the original Smart Gold Customer List report:
      // Cust.Code | CustomerName | Address | Phone (R1, R2, HP, Fax) | Other Detail (eMail, PP No)
      data = result.recordset.map((r) => {
        const addrLines = [r.Address1, r.Address2, r.Address3, r.Address4]
          .map((a) => (a || '').trim())
          .filter(Boolean);
        const address = addrLines.join('\n');

        const p1 = (r.Phone1 || '').trim();
        const p2 = (r.Phone2 || '').trim();
        const hp = (r.Mobile || '').trim();
        const fax = (r.Fax || '').trim();
        const phoneParts = [];
        if (p1) phoneParts.push(`R1: ${p1}`);
        if (p2) phoneParts.push(`R2: ${p2}`);
        if (hp) phoneParts.push(`HP: ${hp}`);
        if (fax) phoneParts.push(`Fax: ${fax}`);
        const phone = phoneParts.join('\n');

        const email = (r.Email || '').trim();
        const ppNo = (r.PassPortNo || '').trim();
        const otherParts = [];
        if (email) otherParts.push(`eMail: ${email}`);
        if (ppNo) otherParts.push(`PP No: ${ppNo}`);
        const otherDetail = otherParts.join('\n');

        return {
          'Cust.Code': r['Cust.Code'] || '',
          'CustomerName': r['CustomerName'] || '',
          'Address': address,
          'Phone (R1, R2, HP, Fax)': phone,
          'Other Detail (eMail, PP No)': otherDetail,
        };
      });
    }

    // =========================================================================
    // Report 2: Customer Points Adjustment (rptCustomerPointsAdjustment.rpt)
    // =========================================================================
    else if (reportId === "points_adjustment") {
      let query = `
        SELECT 
          RTRIM(CustomerCode) AS CustomerCode,
          RTRIM(CustomerName) AS CustomerName,
          RTRIM(CustomerType) AS CustomerType,
          PreviousPoints,
          AdjustmentPoints,
          RTRIM(InvoiceNo) AS InvoiceNo,
          RTRIM(CreateUser) AS CreateUser,
          CreateDate
        FROM tblCustomersPointAdjustment
        WHERE 1=1
      `;

      if (fromDate && toDate) {
        const { fromDate: fDate, toDateExclusive: tDate } = getInclusiveRange(fromDate, toDate);
        request.input("FromDate", sql.DateTime, fDate);
        request.input("ToDateExclusive", sql.DateTime, tDate);
        query += ` AND CreateDate >= @FromDate AND CreateDate < @ToDateExclusive`;
      }

      if (customerType && customerType !== "ALL") {
        request.input("CustomerType", sql.VarChar, customerType.trim());
        query += ` AND RTRIM(CustomerType) = @CustomerType`;
      }

      query += ` ORDER BY CreateDate DESC`;
      const result = await request.query(query);
      data = result.recordset;

      const totalAdj = data.reduce((acc, r) => acc + (Number(r.AdjustmentPoints) || 0), 0);
      summary = {
        totalAdjustments: data.length,
        totalAdjustmentPoints: totalAdj,
      };
    }

    // =========================================================================
    // Report 3: Customer Type Change (rptCustomerTypeChange.rpt)
    // =========================================================================
    else if (reportId === "type_change") {
      let query = `
        SELECT 
          RTRIM(CustomerCode) AS CustomerCode,
          RTRIM(CustomerName) AS CustomerName,
          RTRIM(OldCustomerType) AS OldCustomerType,
          RTRIM(NewCustomerType) AS NewCustomerType,
          TotalPoints,
          NewTotalPoints,
          RTRIM(CreateUser) AS CreateUser,
          CreateDate
        FROM tblCustomerTypeChangeLog
        WHERE 1=1
      `;

      if (fromDate && toDate) {
        const { fromDate: fDate, toDateExclusive: tDate } = getInclusiveRange(fromDate, toDate);
        request.input("FromDate", sql.DateTime, fDate);
        request.input("ToDateExclusive", sql.DateTime, tDate);
        query += ` AND CreateDate >= @FromDate AND CreateDate < @ToDateExclusive`;
      }

      query += ` ORDER BY CreateDate DESC`;
      const result = await request.query(query);
      data = result.recordset;
      summary = { totalChanges: data.length };
    }

    // =========================================================================
    // Report 4: Email and Mobile Report
    // =========================================================================
    else if (reportId === "email_mobile") {
      let query = `
        SELECT 
          RTRIM(CustomerCode) AS CustomerCode,
          RTRIM(CustomerName) AS CustomerName,
          RTRIM(NricNo) AS NricNo,
          RTRIM(Mobile) AS Mobile,
          RTRIM(eMail) AS Email
        FROM tblCustomer
        WHERE 1=1
      `;

      if (fromDate && toDate) {
        const { fromDate: fDate, toDateExclusive: tDate } = getInclusiveRange(fromDate, toDate);
        request.input("FromDate", sql.DateTime, fDate);
        request.input("ToDateExclusive", sql.DateTime, tDate);
        query += ` AND CreateDate >= @FromDate AND CreateDate < @ToDateExclusive`;
      }

      if (customerType && customerType !== "ALL") {
        request.input("CustomerType", sql.VarChar, customerType.trim());
        query += ` AND RTRIM(CustomerType) = @CustomerType`;
      }

      if (nricType === "SINGAPOREAN/SPR") {
        query += ` AND LEFT(NricNo, 1) = 'S'`;
      } else if (nricType === "OTHERS") {
        query += ` AND LEFT(NricNo, 1) <> 'S'`;
      }

      query += ` ORDER BY CustomerType, CustomerCode`;
      const result = await request.query(query);
      data = result.recordset;
      summary = { totalCustomers: data.length };
    }

    // =========================================================================
    // Report 5: Customer DOB Report
    // =========================================================================
    else if (reportId === "dob") {
      const dateCol = dobType === "DOA" ? "DOA" : "DOB";

      let query = `
        SELECT 
          RTRIM(CustomerCode) AS CustomerCode,
          RTRIM(CustomerName) AS CustomerName,
          RTRIM(NricNo) AS NricNo,
          RTRIM(Mobile) AS Mobile,
          RTRIM(eMail) AS Email,
          ${dateCol} AS DOB
        FROM tblCustomer
        WHERE (GLAccount1 = 'Y' OR GLAccount1 IS NULL OR RTRIM(GLAccount1) = '')
      `;

      if (fromDate && toDate) {
        const f = parseDateParam(fromDate);
        const t = parseDateParam(toDate);
        const fromMonth = f.getMonth() + 1;
        const fromDay = f.getDate();
        const toMonth = t.getMonth() + 1;
        const toDay = t.getDate();

        request.input("FromMonth", sql.Int, fromMonth);
        request.input("FromDay", sql.Int, fromDay);
        request.input("ToMonth", sql.Int, toMonth);
        request.input("ToDay", sql.Int, toDay);

        if (fromMonth < toMonth || (fromMonth === toMonth && fromDay <= toDay)) {
          query += `
            AND (
              (MONTH(${dateCol}) > @FromMonth OR (MONTH(${dateCol}) = @FromMonth AND DAY(${dateCol}) >= @FromDay))
              AND (MONTH(${dateCol}) < @ToMonth OR (MONTH(${dateCol}) = @ToMonth AND DAY(${dateCol}) <= @ToDay))
            )
          `;
        } else {
          // Cross year boundary
          query += `
            AND (
              (MONTH(${dateCol}) > @FromMonth OR (MONTH(${dateCol}) = @FromMonth AND DAY(${dateCol}) >= @FromDay))
              OR (MONTH(${dateCol}) < @ToMonth OR (MONTH(${dateCol}) = @ToMonth AND DAY(${dateCol}) <= @ToDay))
            )
          `;
        }
      }

      if (customerType && customerType !== "ALL") {
        request.input("CustomerType", sql.VarChar, customerType.trim());
        query += ` AND RTRIM(CustomerType) = @CustomerType`;
      }

      query += ` ORDER BY CustomerType, CustomerCode`;
      const result = await request.query(query);
      data = result.recordset;
      summary = { totalCustomers: data.length };
    }

    // =========================================================================
    // Report 6: Customer Reward Points
    // =========================================================================
    else if (reportId === "reward_points") {
      let query = `
        SELECT 
          RTRIM(CustomerCode) AS CustomerCode,
          RTRIM(CustomerName) AS CustomerName,
          RTRIM(CustomerType) AS CustomerType,
          ISNULL(TotalRewardPoints, 0) AS TotalRewardPoints,
          ISNULL(TotalRedeemedPoints, 0) AS TotalRedeemedPoints,
          (ISNULL(TotalRewardPoints, 0) - ISNULL(TotalRedeemedPoints, 0)) AS BalancePoints
        FROM tblCustomer
        WHERE 1=1
      `;

      if (nricType === "SINGAPOREAN/SPR") {
        query += ` AND LEFT(NricNo, 1) = 'S'`;
      } else if (nricType === "OTHERS") {
        query += ` AND LEFT(NricNo, 1) <> 'S'`;
      }

      if (customerType && customerType !== "ALL") {
        request.input("CustomerType", sql.VarChar, customerType.trim());
        query += ` AND RTRIM(CustomerType) = @CustomerType`;
      }

      query += ` ORDER BY CustomerType, CustomerCode`;

      const result = await request.query(query);
      data = result.recordset;

      const totalReward = data.reduce((acc, row) => acc + (Number(row.TotalRewardPoints) || 0), 0);
      const totalRedeemed = data.reduce((acc, row) => acc + (Number(row.TotalRedeemedPoints) || 0), 0);
      const totalBalance = data.reduce((acc, row) => acc + (Number(row.BalancePoints) || 0), 0);

      summary = {
        totalCustomers: data.length,
        totalRewardPoints: totalReward,
        totalRedeemedPoints: totalRedeemed,
        totalBalancePoints: totalBalance,
      };
    }

    // =========================================================================
    // Report 7: Customer Nationality Wise
    // =========================================================================
    else if (reportId === "nationality_wise") {
      let query = `
        SELECT 
          RTRIM(CustomerCode) AS CustomerCode,
          RTRIM(CustomerName) AS CustomerName,
          RTRIM(CustomerType) AS CustomerType,
          RTRIM(Address4) AS Nationality
        FROM tblCustomer
        WHERE 1=1
      `;

      if (nationality && nationality !== "ALL" && nationality.trim() !== "") {
        request.input("Nationality", sql.VarChar, nationality.trim());
        query += ` AND RTRIM(Address4) = @Nationality`;
      }

      if (customerType && customerType !== "ALL") {
        request.input("CustomerType", sql.VarChar, customerType.trim());
        query += ` AND RTRIM(CustomerType) = @CustomerType`;
      }

      query += ` ORDER BY Address4, CustomerCode`;

      const result = await request.query(query);
      data = result.recordset;
      summary = { totalCustomers: data.length };
    }

    // =========================================================================
    // Report 8: Customer Religion Wise
    // =========================================================================
    else if (reportId === "religion_wise") {
      let query = `
        SELECT 
          RTRIM(CustomerCode) AS CustomerCode,
          RTRIM(CustomerName) AS CustomerName,
          RTRIM(CustomerType) AS CustomerType,
          RTRIM(DateType) AS Religion
        FROM tblCustomer
        WHERE 1=1
      `;

      if (religion && religion !== "ALL" && religion.trim() !== "") {
        request.input("Religion", sql.VarChar, religion.trim());
        query += ` AND RTRIM(DateType) = @Religion`;
      }

      if (customerType && customerType !== "ALL") {
        request.input("CustomerType", sql.VarChar, customerType.trim());
        query += ` AND RTRIM(CustomerType) = @CustomerType`;
      }

      query += ` ORDER BY DateType, CustomerCode`;

      const result = await request.query(query);
      data = result.recordset;
      summary = { totalCustomers: data.length };
    }

    // =========================================================================
    // Report 9: Customer Mailing Address Labels (labelCustomer.rpt / mailing_address)
    // =========================================================================
    else if (reportId === "mailing_address" || reportId === "mailing") {
      let query = `
        SELECT 
          RTRIM(c.CustomerCode) AS CustomerCode,
          RTRIM(c.CustomerName) AS CustomerName,
          RTRIM(c.CustomerType) AS CustomerType,
          RTRIM(c.Address1) AS Address1,
          RTRIM(c.Address2) AS Address2,
          RTRIM(c.Address3) AS Address3,
          RTRIM(c.Address4) AS Nationality,
          RTRIM(c.Phone1) AS Phone,
          RTRIM(c.Mobile) AS Mobile,
          RTRIM(c.DateType) AS Religion,
          RTRIM(c.Remarks) AS Remarks
        FROM tblCustomer c
        WHERE (c.GLAccount1 = 'Y' OR c.GLAccount1 IS NULL OR RTRIM(c.GLAccount1) = '')
          AND ((c.Address1 IS NOT NULL AND RTRIM(c.Address1) <> '')
           OR (c.Address2 IS NOT NULL AND RTRIM(c.Address2) <> '')
           OR (c.Address3 IS NOT NULL AND RTRIM(c.Address3) <> ''))
      `;

      query += applyCustomerFilters("c");

      if (fromDate && toDate) {
        const { fromDate: fDate, toDateExclusive: tDate } = getInclusiveRange(fromDate, toDate);
        request.input("FromDate", sql.DateTime, fDate);
        request.input("ToDateExclusive", sql.DateTime, tDate);
        query += ` AND c.CreateDate >= @FromDate AND c.CreateDate < @ToDateExclusive`;
      }

      query += ` ORDER BY c.CustomerName ASC`;
      const result = await request.query(query);
      data = result.recordset;
      summary = { totalCustomers: data.length };
    }

    // =========================================================================
    // Report 10: Customer Count
    // =========================================================================
    else if (reportId === "customer_count") {
      let query = `
        SELECT 
          CASE WHEN CustomerType IS NULL OR RTRIM(CustomerType) = '' THEN 'UNSPECIFIED' ELSE RTRIM(CustomerType) END AS CustomerType,
          COUNT(*) AS [Count]
        FROM tblCustomer
        WHERE 1=1
      `;

      if (fromDate && toDate) {
        const { fromDate: fDate, toDateExclusive: tDate } = getInclusiveRange(fromDate, toDate);
        request.input("FromDate", sql.DateTime, fDate);
        request.input("ToDateExclusive", sql.DateTime, tDate);
        query += ` AND CreateDate >= @FromDate AND CreateDate < @ToDateExclusive`;
      }

      if (nricType === "SINGAPOREAN/SPR") {
        query += ` AND LEFT(NricNo, 1) = 'S'`;
      } else if (nricType === "OTHERS") {
        query += ` AND LEFT(NricNo, 1) <> 'S'`;
      }

      query += `
        GROUP BY CustomerType
        ORDER BY [Count] DESC
      `;

      const result = await request.query(query);
      data = result.recordset;

      const total = data.reduce((acc, r) => acc + (Number(r.Count) || 0), 0);
      summary = { totalCustomers: total };
    }

    // =========================================================================
    // Report 11: Customer Volume
    // =========================================================================
    else if (reportId === "customer_volume") {
      let query = `
        SELECT 
          RTRIM(a.CustomerCode) AS CustomerCode,
          RTRIM(a.CustomerName) AS CustomerName,
          COUNT(*) AS [NoOfTransactions],
          SUM(a.NetTotal) AS TotalSales
        FROM tblSalesHeader AS a
        INNER JOIN tblCustomer AS b
          ON RTRIM(a.CustomerCode) = RTRIM(b.CustomerCode)
        WHERE a.SalesType NOT IN ('CRRETN', 'CSRETN')
      `;

      if (fromDate && toDate) {
        const { fromDate: fDate, toDateExclusive: tDate } = getInclusiveRange(fromDate, toDate);
        request.input("FromDate", sql.DateTime, fDate);
        request.input("ToDateExclusive", sql.DateTime, tDate);
        query += ` AND a.InvoiceDate >= @FromDate AND a.InvoiceDate < @ToDateExclusive`;
      }

      if (nricType === "SINGAPOREAN/SPR") {
        query += ` AND LEFT(b.NricNo, 1) = 'S'`;
      } else if (nricType === "OTHERS") {
        query += ` AND LEFT(b.NricNo, 1) <> 'S'`;
      }

      if (customerType && customerType !== "ALL") {
        request.input("CustomerType", sql.VarChar, customerType.trim());
        query += ` AND RTRIM(b.CustomerType) = @CustomerType`;
      }

      query += `
        GROUP BY a.CustomerCode, a.CustomerName
        ORDER BY TotalSales DESC
      `;

      const result = await request.query(query);
      data = result.recordset;

      const totalTrans = data.reduce((acc, r) => acc + (Number(r.NoOfTransactions) || 0), 0);
      const totalSales = data.reduce((acc, r) => acc + (Number(r.TotalSales) || 0), 0);

      summary = {
        totalCustomers: data.length,
        totalTransactions: totalTrans,
        totalSales,
      };
    }

    // =========================================================================
    // Report 12: Customer Points with Expiry Date
    // =========================================================================
    else if (reportId === "points_expiry") {
      let query = `
        SELECT 
          RTRIM(m.LocationCode) AS LocationCode,
          RTRIM(m.CustomerCode) AS CustomerCode,
          RTRIM(m.InvoiceNo) AS InvoiceNo,
          CONVERT(VARCHAR(20), m.InvoiceDate, 103) AS InvoiceDate,
          m.InvoiceAmount,
          m.RewardPoints,
          m.RedeemPoints,
          CONVERT(VARCHAR(20), m.ExpiryDate, 103) AS ExpiryDate
        FROM tblmemberTransactionAJ m
        LEFT JOIN tblCustomer c ON RTRIM(m.CustomerCode) = RTRIM(c.CustomerCode)
        WHERE m.CustomerCode <> 'CASH'
          AND (c.GLAccount1 = 'Y' OR c.GLAccount1 IS NULL OR RTRIM(c.GLAccount1) = '')
      `;

      if (fromDate && toDate) {
        const { fromDate: fDate, toDateExclusive: tDate } = getInclusiveRange(fromDate, toDate);
        request.input("FromDate", sql.DateTime, fDate);
        request.input("ToDateExclusive", sql.DateTime, tDate);
        query += ` AND m.ExpiryDate >= @FromDate AND m.ExpiryDate < @ToDateExclusive`;
      }

      if (customerCode && customerCode.trim()) {
        request.input("CustomerCode", sql.VarChar, customerCode.trim());
        query += ` AND RTRIM(m.CustomerCode) = @CustomerCode`;
      }

      query += ` ORDER BY m.CustomerCode, m.InvoiceDate`;
      const result = await request.query(query);
      data = result.recordset;

      const totalReward = data.reduce((acc, r) => acc + (Number(r.RewardPoints) || 0), 0);
      const totalRedeem = data.reduce((acc, r) => acc + (Number(r.RedeemPoints) || 0), 0);

      summary = {
        totalRecords: data.length,
        totalRewardPoints: totalReward,
        totalRedeemPoints: totalRedeem,
      };
    }

    res.json({
      success: true,
      reportId,
      totalRecords: data.length,
      data,
      summary,
    });
  } catch (err) {
    console.error("Error generating customer report:", err);
    res.status(500).json({ success: false, message: err.message });
  }
});

// POST /api/customerreports/download-html
router.post("/download-html", async (req, res) => {
  try {
    const { generateReportHtml } = require("../utils/reportHtmlTemplate");
    const { title, columns, data, totals, companyInfo, fromDate, toDate, showPrintButton } = req.body;
    const html = await generateReportHtml({
      title: title || "Customer Report",
      columns,
      data,
      totals,
      companyInfo,
      fromDate,
      toDate,
      pool: req.pool,
      showPrintButton: showPrintButton !== undefined ? showPrintButton : false,
    });
    res.setHeader("Content-Type", "text/html; charset=utf-8");
    res.send(html);
  } catch (err) {
    console.error("Customer Reports Download HTML Error:", err);
    res.status(500).send("Error generating report HTML: " + err.message);
  }
});

// EXPORT EXCEL & PDF ENDPOINTS
router.post("/export-excel", (req, res) => {
  const { handleExportExcel } = require("../utils/reportExportHandlers");
  handleExportExcel({ req, res, defaultTitle: "Customer_Report" });
});

router.post("/export-pdf", (req, res) => {
  const { handleExportPdf } = require("../utils/reportExportHandlers");
  handleExportPdf({ req, res, defaultTitle: "Customer_Report" });
});

module.exports = router;
