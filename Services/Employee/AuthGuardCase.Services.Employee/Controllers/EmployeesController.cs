using AuthGuardCase.Services.Employee.Services;
using AuthGuardCase.Shared.ControllerBases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AuthGuardCase.Services.Employee.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeesController : CustomBaseController
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        [Authorize(Policy = "Read")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _employeeService.GetAllAsync();
            return CreateActionResultInstance(response);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "Read")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _employeeService.GetByIdAsync(id);
            return CreateActionResultInstance(response);
        }

        [HttpPost]
        [Authorize(Policy = "Write")]
        public async Task<IActionResult> Create(Models.Employee emp)
        {
            var response = await _employeeService.CreateAsync(emp);
            return CreateActionResultInstance(response);
        }

        [HttpPut]
        [Authorize(Policy = "Write")]
        public async Task<IActionResult> Update(Models.Employee emp)
        {
            var response = await _employeeService.UpdateAsync(emp);
            return CreateActionResultInstance(response);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "Write")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _employeeService.DeleteAsync(id);
            return CreateActionResultInstance(response);
        }
    }
}