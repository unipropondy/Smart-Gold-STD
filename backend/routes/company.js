const express = require("express");
const router = express.Router();

// GET all companies
router.get("/", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query("SELECT * FROM tblCompany ORDER BY CompanyID ASC");
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Companies Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// POST insert new company
router.post("/", async (req, res) => {
  try {
    const pool = req.pool;
    const body = req.body;
    const request = pool.request();
    
    // Check if company code already exists if we're inserting manually
    if (body.CompanyID) {
       const check = await pool.request()
          .input('CompanyID', body.CompanyID)
          .query("SELECT * FROM tblCompany WHERE CompanyID = @CompanyID");
       if (check.recordset.length > 0) {
          return res.status(400).json({ success: false, message: "Company ID already exists" });
       }
    }

    const excludedFields = ['CompanyID'];
    
    // Add missing mandatory fields if not present
    if (body.UseSalesQty === undefined) body.UseSalesQty = '';
    if (body.Rental === undefined) body.Rental = 0;

    const fieldsToInsert = Object.keys(body).filter(k => body[k] !== undefined && body[k] !== '');
    
    // Explicitly add fields that were provided as defaults but might be empty strings
    if (!fieldsToInsert.includes('UseSalesQty')) fieldsToInsert.push('UseSalesQty');
    if (!fieldsToInsert.includes('Rental')) fieldsToInsert.push('Rental');

    fieldsToInsert.forEach(key => {
      request.input(key, body[key]);
    });

    const columns = fieldsToInsert.map(k => `[${k}]`).join(", ");
    const values = fieldsToInsert.map(k => `@${k}`).join(", ");
    
    const query = `INSERT INTO tblCompany (${columns}) VALUES (${values})`;
    await request.query(query);
    
    res.json({ success: true, message: "Company created successfully" });
  } catch (err) {
    console.error("Create Company Error:", err);
    res.status(500).json({ success: false, message: err.message, error: err.message });
  }
});

// PUT update company
router.put("/:id", async (req, res) => {
  try {
    const pool = req.pool;
    const { id } = req.params;
    const body = req.body;
    const request = pool.request();
    
    request.input('id', id);

    const check = await pool.request().input('id', id).query("SELECT * FROM tblCompany WHERE CompanyID = @id");
    if (check.recordset.length === 0) {
      return res.status(404).json({ success: false, message: "Company not found" });
    }

    const excludedFields = ['CompanyID'];
    const fieldsToUpdate = Object.keys(body).filter(k => !excludedFields.includes(k) && body[k] !== undefined);

    fieldsToUpdate.forEach(key => {
      request.input(key, body[key] === '' ? null : body[key]);
    });

    if (fieldsToUpdate.length === 0) {
      return res.json({ success: true, message: "No fields to update" });
    }

    const setClauses = fieldsToUpdate.map(k => `[${k}] = @${k}`).join(", ");
    const query = `UPDATE tblCompany SET ${setClauses} WHERE CompanyID = @id`;
    
    await request.query(query);
    res.json({ success: true, message: "Company updated successfully" });
  } catch (err) {
    console.error("Update Company Error:", err);
    res.status(500).json({ success: false, message: err.message, error: err.message });
  }
});

// DELETE company
router.delete("/:id", async (req, res) => {
  try {
    const pool = req.pool;
    const { id } = req.params;
    
    const request = pool.request();
    request.input('id', id);
    
    const result = await request.query("DELETE FROM tblCompany WHERE CompanyID = @id");
    
    if (result.rowsAffected[0] > 0) {
      res.json({ success: true, message: "Company deleted successfully" });
    } else {
      res.status(404).json({ success: false, message: "Company not found" });
    }
  } catch (err) {
    console.error("Delete Company Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

module.exports = router;
