using Microsoft.AspNetCore.Mvc;
using PayrollManagement.API.Services;

namespace PayrollManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeesController(
            IEmployeeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var employees =
                await _service.GetEmployees();

            return Ok(employees);
        }
    }
}