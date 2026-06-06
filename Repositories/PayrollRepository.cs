using Microsoft.Data.SqlClient;
using PayrollManagement.API.Models;
using System.Data;

namespace PayrollManagement.API.Repositories
{
    public class PayrollRepository : IPayrollRepository
    {
        private readonly string _connectionString;

        public PayrollRepository(IConfiguration configuration)
        {
            _connectionString =
                configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task RunPayroll(int month, int year)
        {
            using SqlConnection con =
                new SqlConnection(_connectionString);

            using SqlCommand cmd =
                new SqlCommand("usp_RunPayroll", con);

            cmd.CommandType =
                CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Month", month);
            cmd.Parameters.AddWithValue("@Year", year);

            await con.OpenAsync();

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<List<PayrollResult>> GetPayroll(int month, int year)
        {
            List<PayrollResult> payrolls = new();

            using SqlConnection con =
                new SqlConnection(_connectionString);

            string query = @"
    SELECT
        PD.EmployeeId,
        E.EmployeeName,
        PD.BasicSalary,
        PD.WorkingDays,
        PD.DaysPresent,
        PD.GrossPay,
        PD.PFDeduction,
        PD.ProfessionalTax,
        PD.NetPay
    FROM PayrollDetail PD
    INNER JOIN PayrollRun PR
        ON PD.PayrollRunId = PR.PayrollRunId
    INNER JOIN Employee E
        ON PD.EmployeeId = E.EmployeeId
    WHERE PR.PayrollMonth = @Month
    AND PR.PayrollYear = @Year";

            using SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@Month", month);
            cmd.Parameters.AddWithValue("@Year", year);

            await con.OpenAsync();

            using SqlDataReader reader =
                await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                payrolls.Add(new PayrollResult
                {
                    EmployeeId =
                        Convert.ToInt32(reader["EmployeeId"]),

                    EmployeeName =
                        reader["EmployeeName"].ToString()!,

                    BasicSalary =
                        Convert.ToDecimal(reader["BasicSalary"]),

                    WorkingDays =
                        Convert.ToInt32(reader["WorkingDays"]),

                    DaysPresent =
                        Convert.ToInt32(reader["DaysPresent"]),

                    GrossPay =
                        Convert.ToDecimal(reader["GrossPay"]),

                    PFDeduction =
                        Convert.ToDecimal(reader["PFDeduction"]),

                    ProfessionalTax =
                        Convert.ToDecimal(reader["ProfessionalTax"]),

                    NetPay =
                        Convert.ToDecimal(reader["NetPay"])
                });
            }

            return payrolls;
        }
        public async Task<Payslip?> GetPayslip(
    int payrollRunId,
    int employeeId)
        {
            using SqlConnection con =
                new SqlConnection(_connectionString);

            string query = @"
    SELECT
        E.EmployeeId,
        E.EmployeeName,
        PD.BasicSalary,
        PD.GrossPay,
        PD.PFDeduction,
        PD.ProfessionalTax,
        PD.NetPay
    FROM PayrollDetail PD
    INNER JOIN Employee E
        ON PD.EmployeeId = E.EmployeeId
    WHERE PD.PayrollRunId = @PayrollRunId
    AND PD.EmployeeId = @EmployeeId";

            using SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue(
                "@PayrollRunId",
                payrollRunId);

            cmd.Parameters.AddWithValue(
                "@EmployeeId",
                employeeId);

            await con.OpenAsync();

            using SqlDataReader reader =
                await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new Payslip
                {
                    EmployeeId =
                        Convert.ToInt32(reader["EmployeeId"]),

                    EmployeeName =
                        reader["EmployeeName"].ToString()!,

                    BasicSalary =
                        Convert.ToDecimal(reader["BasicSalary"]),

                    GrossPay =
                        Convert.ToDecimal(reader["GrossPay"]),

                    PFDeduction =
                        Convert.ToDecimal(reader["PFDeduction"]),

                    ProfessionalTax =
                        Convert.ToDecimal(reader["ProfessionalTax"]),

                    NetPay =
                        Convert.ToDecimal(reader["NetPay"])
                };
            }

            return null;
        }
    }
}