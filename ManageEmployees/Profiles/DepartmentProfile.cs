using AutoMapper;
using ManageEmployees.Dtos.Department;
using ManageEmployees.Entities;

namespace ManageEmployees.Profiles
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile() 
        {
            CreateMap<Department, ReadDepartment>();
            CreateMap<Department, DetailDepartment>();
            CreateMap<CreateDepartment, Department>();
            CreateMap<UpdateDepartment, Department>();
            CreateMap<Department, ReadDepartment>();
        }
    }
}
