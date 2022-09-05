using AuthGuardCase.Services.Employee.Dtos;
using AutoMapper;

namespace AuthGuardCase.Services.Employee.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Models.Employee, EmployeeDto>().ReverseMap();
        }
    }
}