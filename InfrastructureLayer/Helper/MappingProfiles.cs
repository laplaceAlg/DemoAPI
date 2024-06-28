using ApplicationLayer.Dtos.Departments;
using ApplicationLayer.Dtos.Employees;
using AutoMapper;
using DomainLayer.Entities;

namespace InfrastructureLayer.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Department, DepartmentDto>()
                .ForMember(dest => dest.Employees, opt => opt.MapFrom(src => src.Dept_Emps.Select(de => de.Employee.Name).ToList()));

            CreateMap<CreateDepartmentDto, Department>();
            CreateMap<Employee, EmployeeDto>()
               .ForMember(dest => dest.Departments,
                          opt => opt.MapFrom(src => src.Dept_Emps.Select(de => de.Department.Name).ToList()));
            CreateMap<EmployeeDto, Employee>();
        }
    }
}
