using AutoMapper;
using MyEmployees.Api.DTOs;
using MyEmployees.Api.Models;

namespace MyEmployees.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name));
            CreateMap<CreateEmployeeDto, Employee>();

            CreateMap<Department, DepartmentDto>();
            CreateMap<CreateDepartmentDto, Department>();
        }
    }
}