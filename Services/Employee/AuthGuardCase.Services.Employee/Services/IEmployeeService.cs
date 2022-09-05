using AuthGuardCase.Services.Employee.Dtos;
using AuthGuardCase.Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthGuardCase.Services.Employee.Services
{
    public interface IEmployeeService
    {
        Task<Response<List<EmployeeDto>>> GetAllAsync();
        Task<Response<EmployeeDto>> GetByIdAsync(string id);
        Task<Response<EmployeeDto>> CreateAsync(Models.Employee employee);
        Task<Response<NoContent>> UpdateAsync(Models.Employee employee);
        Task<Response<NoContent>> DeleteAsync(string id);
    }
}