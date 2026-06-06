using PayrollManagement.API.Models;
using PayrollManagement.API.Repositories;

namespace PayrollManagement.API.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeService(
            IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Employee>> GetEmployees()
        {
            return await _repository.GetEmployees();
        }
    }
}