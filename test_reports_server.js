require('dotenv').config({ path: './backend/.env' });
const express = require('express');
const customerReportsRouter = require('./backend/routes/customerreports');
const { getPool } = require('./backend/db');

(async () => {
  try {
    const pool = await getPool('smartgold');
    const app = express();
    app.use(express.json());
    app.use((req, res, next) => {
      req.pool = pool;
      next();
    });
    app.use('/api/customerreports', customerReportsRouter);

    const server = app.listen(3333, async () => {
      console.log('Test server running on port 3333');
      const reports = [
        { id: 'customers_list', query: 'reportId=customers_list' },
        { id: 'points_adjustment', query: 'reportId=points_adjustment&fromDate=2020-01-01&toDate=2026-12-31' },
        { id: 'type_change', query: 'reportId=type_change&fromDate=2020-01-01&toDate=2026-12-31' },
        { id: 'email_mobile', query: 'reportId=email_mobile&fromDate=2020-01-01&toDate=2026-12-31' },
        { id: 'dob', query: 'reportId=dob&fromDate=2020-01-01&toDate=2026-12-31&dobType=DOB' },
        { id: 'reward_points', query: 'reportId=reward_points' },
        { id: 'nationality_wise', query: 'reportId=nationality_wise' },
        { id: 'religion_wise', query: 'reportId=religion_wise' },
        { id: 'mailing_address', query: 'reportId=mailing_address' },
        { id: 'customer_count', query: 'reportId=customer_count' },
        { id: 'customer_volume', query: 'reportId=customer_volume&fromDate=2020-01-01&toDate=2026-12-31' },
        { id: 'points_expiry', query: 'reportId=points_expiry&fromDate=2019-01-01&toDate=2026-12-31' },
      ];

      for (const r of reports) {
        try {
          const res = await fetch(`http://localhost:3333/api/customerreports/report?${r.query}`);
          const json = await res.json();
          console.log(`REPORT [${r.id}]: success=${json.success}, totalRecords=${json.totalRecords}, columns=${json.data && json.data[0] ? Object.keys(json.data[0]).join(', ') : 'none'}`);
        } catch (err) {
          console.error(`REPORT [${r.id}] ERROR:`, err.message);
        }
      }

      // Also test nationalities
      const natRes = await fetch('http://localhost:3333/api/customerreports/nationalities');
      const natJson = await natRes.json();
      console.log(`NATIONALITIES: success=${natJson.success}, count=${natJson.data?.length}`);

      // Also test customer search
      const searchRes = await fetch('http://localhost:3333/api/customerreports/customers-search?search=');
      const searchJson = await searchRes.json();
      console.log(`SEARCH: success=${searchJson.success}, count=${searchJson.data?.length}`);

      server.close();
      process.exit(0);
    });
  } catch (e) {
    console.error('Fatal error:', e);
    process.exit(1);
  }
})();

