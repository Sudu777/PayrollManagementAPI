using PayrollManagement.API.Models;

namespace PayrollManagement.API.Repositories
{
    public interface IPayrollRepository
    {
        Task RunPayroll(int month, int year);

        Task<List<PayrollResult>> GetPayroll(int month, int year);

        Task<Payslip?> GetPayslip(
    int payrollRunId,
    int employeeId);
    }
}