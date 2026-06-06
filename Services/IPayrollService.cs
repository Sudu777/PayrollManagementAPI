using PayrollManagement.API.Models;

namespace PayrollManagement.API.Services
{
    public interface IPayrollService
    {
        Task RunPayroll(int month, int year);

        Task<List<PayrollResult>> GetPayroll(int month, int year);

        Task<Payslip?> GetPayslip(
    int payrollRunId,
    int employeeId);
    }
}