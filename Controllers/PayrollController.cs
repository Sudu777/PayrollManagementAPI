using Microsoft.AspNetCore.Mvc;
using PayrollManagement.API.Models;
using PayrollManagement.API.Services;

namespace PayrollManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PayrollController : ControllerBase
    {
        private readonly IPayrollService _service;

        public PayrollController(IPayrollService service)
        {
            _service = service;
        }

        [HttpPost("run")]
        public async Task<IActionResult> RunPayroll(
            PayrollRequest request)
        {
            await _service.RunPayroll(
                request.Month,
                request.Year);

            return Ok("Payroll Generated Successfully");
        }

       
        [HttpGet("{month}/{year}")]
        public async Task<IActionResult> GetPayroll(int month, int year)
        {
            var result =
                await _service.GetPayroll(month, year);

            if (result.Count == 0)
                return NotFound();

            return Ok(result);
        }
        [HttpGet("{runId}/slip/{employeeId}")]
        public async Task<IActionResult> GetPayslip(
    int runId,
    int employeeId)
        {
            var result =
                await _service.GetPayslip(
                    runId,
                    employeeId);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}