using Microsoft.Data.SqlClient;
using PayrollManagement.API.Models;

namespace PayrollManagement.API.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string _connectionString;

        public EmployeeRepository(IConfiguration configuration)
        {
            _connectionString =
                configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<List<Employee>> GetEmployees()
        {
            List<Employee> employees = new();

            using SqlConnection con =
                new SqlConnection(_connectionString);

            string query = @"
            SELECT
                EmployeeId,
                EmployeeName,
                BasicSalary,
                DepartmentId
            FROM Employee";

            using SqlCommand cmd =
                new SqlCommand(query, con);

            await con.OpenAsync();

            using SqlDataReader reader =
                await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                employees.Add(new Employee
                {
                    EmployeeId =
                        Convert.ToInt32(reader["EmployeeId"]),

                    EmployeeName =
                        reader["EmployeeName"].ToString()!,

                    BasicSalary =
                        Convert.ToDecimal(reader["BasicSalary"]),

                    DepartmentId =
                        Convert.ToInt32(reader["DepartmentId"])
                });
            }

            return employees;
        }
    }
}