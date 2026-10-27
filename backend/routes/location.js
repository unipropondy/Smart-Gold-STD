const express = require("express");
const router = express.Router();

// GET all locations
router.get("/", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query("SELECT * FROM tblLocation ORDER BY LocationCode ASC");
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Locations Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// POST insert new location
router.post("/", async (req, res) => {
  try {
    const pool = req.pool;
    const body = req.body;
    const request = pool.request();
    
    if (body.LocationCode) {
       const check = await pool.request()
          .input('LocationCode', body.LocationCode)
          .query("SELECT * FROM tblLocation WHERE LocationCode = @LocationCode");
       if (check.recordset.length > 0) {
          return res.status(400).json({ success: false, message: "Location Code already exists" });
       }
    }

    // Set mandatory fields for new record
    body.CreateUser = body.CreateUser || 'sys';
    body.CreateDate = body.CreateDate || new Date().toISOString();
    body.ModifyUser = body.ModifyUser || 'sys';
    body.ModifyDate = body.ModifyDate || new Date().toISOString();

    const fieldsToInsert = Object.keys(body).filter(k => body[k] !== undefined && body[k] !== '');
    
    fieldsToInsert.forEach(key => {
      request.input(key, body[key]);
    });

    const columns = fieldsToInsert.map(k => `[${k}]`).join(", ");
    const values = fieldsToInsert.map(k => `@${k}`).join(", ");
    
    const query = `INSERT INTO tblLocation (${columns}) VALUES (${values})`;
    await request.query(query);
    
    res.json({ success: true, message: "Location created successfully" });
  } catch (err) {
    require('fs').writeFileSync('E:\\Gold\\smartgold\\backend\\sql_error.txt', err.message);
    console.error("Create Location Error:", err);
    res.status(500).json({ success: false, message: err.message, error: err.message });
  }
});

// PUT update location
router.put("/:id", async (req, res) => {
  try {
    const pool = req.pool;
    const { id } = req.params;
    const body = req.body;
    const request = pool.request();
    
    request.input('id', id);

    const check = await pool.request().input('id', id).query("SELECT * FROM tblLocation WHERE LocationCode = @id");
    if (check.recordset.length === 0) {
      return res.status(404).json({ success: false, message: "Location not found" });
    }
    
    // Set mandatory fields for update
    body.ModifyUser = 'sys';
    body.ModifyDate = new Date().toISOString();

    const excludedFields = ['LocationCode'];
    const fieldsToUpdate = Object.keys(body).filter(k => !excludedFields.includes(k) && body[k] !== undefined);

    fieldsToUpdate.forEach(key => {
      request.input(key, body[key] === '' ? null : body[key]);
    });

    if (fieldsToUpdate.length === 0) {
      return res.json({ success: true, message: "No fields to update" });
    }

    const setClauses = fieldsToUpdate.map(k => `[${k}] = @${k}`).join(", ");
    const query = `UPDATE tblLocation SET ${setClauses} WHERE LocationCode = @id`;
    
    await request.query(query);
    res.json({ success: true, message: "Location updated successfully" });
  } catch (err) {
    console.error("Update Location Error:", err);
    res.status(500).json({ success: false, message: err.message, error: err.message });
  }
});

// DELETE location
router.delete("/:id", async (req, res) => {
  try {
    const pool = req.pool;
    const { id } = req.params;
    
    const request = pool.request();
    request.input('id', id);
    
    const result = await request.query("DELETE FROM tblLocation WHERE LocationCode = @id");
    
    if (result.rowsAffected[0] > 0) {
      res.json({ success: true, message: "Location deleted successfully" });
    } else {
      res.status(404).json({ success: false, message: "Location not found" });
    }
  } catch (err) {
    console.error("Delete Location Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

module.exports = router;
