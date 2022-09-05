using AuthGuardCase.Services.Employee.Dtos;
using AuthGuardCase.Services.Employee.Settings;
using AuthGuardCase.Shared.Dtos;
using AutoMapper;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace AuthGuardCase.Services.Employee.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IMongoCollection<Models.Employee> _emp;

        private readonly IMapper _mapper;

        public EmployeeService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _emp = database.GetCollection<Models.Employee>(databaseSettings.EmployeeCollectionName);
            _mapper = mapper;
        }

        public async Task<Response<List<EmployeeDto>>> GetAllAsync()
        {
            var employees = await _emp.Find(x => x.UserName != "").ToListAsync();
            return Response<List<EmployeeDto>>.Success(_mapper.Map<List<EmployeeDto>>(employees), 200);
        }

        public async Task<Response<EmployeeDto>> GetByIdAsync(string id)
        {
            var employee = await _emp.Find(x => x.Id == id).FirstOrDefaultAsync();
            if (employee == null)
            {
                return Response<EmployeeDto>.Fail("Employee Not Found", 404);
            }

            return Response<EmployeeDto>.Success(_mapper.Map<EmployeeDto>(employee), 200);
        }

        public async Task<Response<EmployeeDto>> CreateAsync(Models.Employee employee)
        {
            await _emp.InsertOneAsync(employee);
            return Response<EmployeeDto>.Success(_mapper.Map<EmployeeDto>(employee), 200);
        }

        public async Task<Response<NoContent>> UpdateAsync(Models.Employee employee)
        {
            var updateEmployee = _mapper.Map<Models.Employee>(employee);
            var result = await _emp.FindOneAndReplaceAsync(x => x.Id == employee.Id, updateEmployee);

            if (result == null)
            {
                return Response<NoContent>.Fail("Employee Not Found", 404);
            }

            return Response<NoContent>.Success(204);
        }

        public async Task<Response<NoContent>> DeleteAsync(string id)
        {
            var result = await _emp.DeleteOneAsync(x => x.Id == id);

            if (result.DeletedCount > 0)
            {
                return Response<NoContent>.Success(204);
            }
            else
            {
                return Response<NoContent>.Fail("Employee Not Fount", 204);
            }
        }
    }
}