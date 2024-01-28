using AutoMapper;
using ManageEmployees.Dtos.Department;
using ManageEmployees.Dtos.Employee;
using ManageEmployees.Entities;

namespace ManageEmployees.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, ReadEmployee>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.EmployeeId))
                .ReverseMap();

            CreateMap<Employee, DetailEmployee>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.EmployeeId))
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthdDate))
                .ReverseMap();

            CreateMap<CreateEmployee, Employee>()
                .ForMember(dest => dest.BirthdDate, opt => opt.MapFrom(src => src.BirthDate))
                .ReverseMap();

            CreateMap<UpdateEmployee, Employee>()
                .ForMember(dest => dest.BirthdDate, opt => opt.MapFrom(src => src.BirthDate))
                .ReverseMap();

            CreateMap<ReadDepartment, EmployeesDepartment>()
                .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.DepartmentId))
                .ReverseMap();
        }
    }
}
