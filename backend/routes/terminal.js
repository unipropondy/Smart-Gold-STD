const express = require("express");
const router = express.Router();

// GET all terminals
router.get("/", async (req, res) => {
  try {
    const pool = req.pool;
    const result = await pool.request().query("SELECT * FROM tblTerminal ORDER BY TerminalCode ASC");
    res.json({ success: true, data: result.recordset });
  } catch (err) {
    console.error("Fetch Terminals Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

// POST insert new terminal
router.post("/", async (req, res) => {
  try {
    const pool = req.pool;
    const body = req.body;
    const request = pool.request();
    
    if (body.TerminalCode) {
       const check = await pool.request()
          .input('TerminalCode', body.TerminalCode)
          .query("SELECT * FROM tblTerminal WHERE TerminalCode = @TerminalCode");
       if (check.recordset.length > 0) {
          return res.status(400).json({ success: false, message: "Terminal Code already exists" });
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
    
    const query = `INSERT INTO tblTerminal (${columns}) VALUES (${values})`;
    await request.query(query);
    
    res.json({ success: true, message: "Terminal created successfully" });
  } catch (err) {
    console.error("Create Terminal Error:", err);
    res.status(500).json({ success: false, message: err.message, error: err.message });
  }
});

// PUT update terminal
router.put("/:id", async (req, res) => {
  try {
    const pool = req.pool;
    const { id } = req.params;
    const body = req.body;
    const request = pool.request();
    
    request.input('id', id);

    const check = await pool.request().input('id', id).query("SELECT * FROM tblTerminal WHERE TerminalCode = @id");
    if (check.recordset.length === 0) {
      return res.status(404).json({ success: false, message: "Terminal not found" });
    }

    // Set mandatory fields for update
    body.ModifyUser = 'sys';
    body.ModifyDate = new Date().toISOString();

    const excludedFields = ['TerminalCode', 'CreateUser', 'CreateDate'];
    const fieldsToUpdate = Object.keys(body).filter(k => !excludedFields.includes(k) && body[k] !== undefined);

    fieldsToUpdate.forEach(key => {
      request.input(key, body[key] === '' ? null : body[key]);
    });

    if (fieldsToUpdate.length === 0) {
      return res.json({ success: true, message: "No fields to update" });
    }

    const setClauses = fieldsToUpdate.map(k => `[${k}] = @${k}`).join(", ");
    const query = `UPDATE tblTerminal SET ${setClauses} WHERE TerminalCode = @id`;
    
    await request.query(query);
    res.json({ success: true, message: "Terminal updated successfully" });
  } catch (err) {
    console.error("Update Terminal Error:", err);
    res.status(500).json({ success: false, message: err.message, error: err.message });
  }
});

// DELETE terminal
router.delete("/:id", async (req, res) => {
  try {
    const pool = req.pool;
    const { id } = req.params;
    
    const request = pool.request();
    request.input('id', id);
    
    const result = await request.query("DELETE FROM tblTerminal WHERE TerminalCode = @id");
    
    if (result.rowsAffected[0] > 0) {
      res.json({ success: true, message: "Terminal deleted successfully" });
    } else {
      res.status(404).json({ success: false, message: "Terminal not found" });
    }
  } catch (err) {
    console.error("Delete Terminal Error:", err);
    res.status(500).json({ success: false, message: "Server error", error: err.message });
  }
});

module.exports = router;
