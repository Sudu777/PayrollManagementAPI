USE [ASP_Core]
GO

/****** Object:  StoredProcedure [dbo].[usp_RunPayroll]    ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_RunPayroll]
(
    @Month INT,
    @Year INT
)
AS
BEGIN

    SET NOCOUNT ON;

    IF EXISTS
    (
        SELECT 1
        FROM PayrollRun
        WHERE PayrollMonth=@Month
        AND PayrollYear=@Year
    )
    BEGIN
        RAISERROR('Payroll already exists',16,1);
        RETURN;
    END

    DECLARE @PayrollRunId INT;

    INSERT INTO PayrollRun
    (
        PayrollMonth,
        PayrollYear
    )
    VALUES
    (
        @Month,
        @Year
    );

    SET @PayrollRunId=SCOPE_IDENTITY();

    INSERT INTO PayrollDetail
    (
        PayrollRunId,
        EmployeeId,
        BasicSalary,
        WorkingDays,
        DaysPresent,
        GrossPay,
        PFDeduction,
        ProfessionalTax,
        NetPay
    )
    SELECT
        @PayrollRunId,
        E.EmployeeId,
        E.BasicSalary,
        A.WorkingDays,
        A.DaysPresent,

        ROUND(
            (E.BasicSalary / A.WorkingDays)
            * A.DaysPresent,
            2
        ),

        ROUND(
            E.BasicSalary * 0.12,
            2
        ),

        200,

        ROUND(
            (
                (E.BasicSalary / A.WorkingDays)
                * A.DaysPresent
            )
            -
            (E.BasicSalary * 0.12)
            -
            200,
            2
        )

    FROM Employee E
    INNER JOIN Attendance A
        ON E.EmployeeId=A.EmployeeId
    WHERE
        A.AttendanceMonth=@Month
        AND A.AttendanceYear=@Year;

END
GO


