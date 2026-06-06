namespace PayrollManagement.API.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; } = string.Empty;

        public decimal BasicSalary { get; set; }

        public int DepartmentId { get; set; }
    }
}
