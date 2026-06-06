using PayrollManagement.API.Models;
using PayrollManagement.API.Repositories;

namespace PayrollManagement.API.Services
{
    public class PayrollService : IPayrollService
    {
        private readonly IPayrollRepository _repository;

        public PayrollService(IPayrollRepository repository)
        {
            _repository = repository;
        }

        public async Task RunPayroll(int month, int year)
        {
            await _repository.RunPayroll(month, year);
        }

        public async Task<List<PayrollResult>> GetPayroll(int month, int year)
        {
            return await _repository.GetPayroll(month, year);
        }
        public async Task<Payslip?> GetPayslip(
    int payrollRunId,
    int employeeId)
        {
            return await _repository.GetPayslip(
                payrollRunId,
                employeeId);
        }
    }
}