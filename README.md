# Payroll Management API

## Technologies Used

* ASP.NET Core Web API
* SQL Server
* ADO.NET
* Swagger

## Setup Instructions

1. Create database ASP_Core
2. Run CreateTables.sql
3. Run SeedData.sql
4. Run usp_RunPayroll.sql
5. Update connection string in appsettings.json
6. Run the application

## API Endpoints

GET /api/employees

POST /api/payroll/run

GET /api/payroll/{month}/{year}

GET /api/payroll/{runId}/slip/{employeeId}

## Architecture

Controller → Service → Repository → SQL Server
