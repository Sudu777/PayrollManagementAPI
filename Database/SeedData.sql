INSERT INTO Department(DepartmentName)
VALUES
('HR'),
('IT');

INSERT INTO Employee
(
    EmployeeName,
    DepartmentId,
    BasicSalary
)
VALUES
('Ravi Sharma',1,30000),
('Anjali Nair',1,35000),
('John Thomas',2,40000),
('Rahul Das',2,45000),
('Priya Menon',2,50000);

INSERT INTO Attendance
(
    EmployeeId,
    AttendanceMonth,
    AttendanceYear,
    WorkingDays,
    DaysPresent
)
VALUES
(1,6,2026,26,24),
(2,6,2026,26,25),
(3,6,2026,26,26),
(4,6,2026,26,22),
(5,6,2026,26,23);