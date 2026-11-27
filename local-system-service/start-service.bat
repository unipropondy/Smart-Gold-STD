@echo off
title SmartGold Local System Service (Port 5005)
cd /d "%~dp0"

if not exist node_modules (
    echo Installing dependencies (systeminformation, express, cors)...
    npm install
)

echo Starting Local System Service on port 5005...
node index.js
pause
