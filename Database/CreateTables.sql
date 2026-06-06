CREATE TABLE Department
(
    DepartmentId INT IDENTITY(1,1) PRIMARY KEY,
    DepartmentName VARCHAR(100) NOT NULL
);

CREATE TABLE Employee
(
    EmployeeId INT IDENTITY(1,1) PRIMARY KEY,
    EmployeeName VARCHAR(100) NOT NULL,
    DepartmentId INT NOT NULL,
    BasicSalary DECIMAL(18,2) NOT NULL,

    CONSTRAINT FK_Employee_Department
    FOREIGN KEY (DepartmentId)
    REFERENCES Department(DepartmentId)
);

CREATE TABLE Attendance
(
    AttendanceId INT IDENTITY(1,1) PRIMARY KEY,
    EmployeeId INT NOT NULL,
    AttendanceMonth INT NOT NULL,
    AttendanceYear INT NOT NULL,
    WorkingDays INT NOT NULL,
    DaysPresent INT NOT NULL,

    CONSTRAINT FK_Attendance_Employee
    FOREIGN KEY(EmployeeId)
    REFERENCES Employee(EmployeeId)
);

CREATE TABLE PayrollRun
(
    PayrollRunId INT IDENTITY(1,1) PRIMARY KEY,
    PayrollMonth INT NOT NULL,
    PayrollYear INT NOT NULL,
    RunDate DATETIME NOT NULL DEFAULT GETDATE(),
    IsFinalized BIT NOT NULL DEFAULT 1
);

CREATE TABLE PayrollDetail
(
    PayrollDetailId INT IDENTITY(1,1) PRIMARY KEY,
    PayrollRunId INT NOT NULL,
    EmployeeId INT NOT NULL,

    BasicSalary DECIMAL(18,2),
    WorkingDays INT,
    DaysPresent INT,

    GrossPay DECIMAL(18,2),
    PFDeduction DECIMAL(18,2),
    ProfessionalTax DECIMAL(18,2),
    NetPay DECIMAL(18,2),

    CONSTRAINT FK_PayrollDetail_Run
    FOREIGN KEY(PayrollRunId)
    REFERENCES PayrollRun(PayrollRunId),

    CONSTRAINT FK_PayrollDetail_Employee
    FOREIGN KEY(EmployeeId)
    REFERENCES Employee(EmployeeId)
);