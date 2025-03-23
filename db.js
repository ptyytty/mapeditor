const express = require('express');
const mysql = require('mysql2');
const app = express();
const port = 3000;

// MariaDB 연결 설정 (SSL 비활성화)
const connection = mysql.createConnection({
  host: 'localhost',
  user: 'root',
  password: 'park',
  database: 'hoi',
  ssl: false  // SSL 비활성화
});

// 서버 설정
app.get('/getData', (req, res) => {
  connection.query('SELECT * FROM job', (err, results) => {
    if (err) {
      res.status(500).send('Error retrieving data');
      return;
    }
    res.json(results);  // Unity로 데이터를 JSON 형식으로 응답
  });
});

// 서버 시작
app.listen(port, '0.0.0.0', () => {
  console.log(`Server running at http:/0.0.0.0:${port}`);
});
