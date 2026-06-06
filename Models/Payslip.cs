namespace PayrollManagement.API.Models
{
    public class Payslip
    {
        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; } = string.Empty;

        public decimal BasicSalary { get; set; }

        public decimal GrossPay { get; set; }

        public decimal PFDeduction { get; set; }

        public decimal ProfessionalTax { get; set; }

        public decimal NetPay { get; set; }
    }
}