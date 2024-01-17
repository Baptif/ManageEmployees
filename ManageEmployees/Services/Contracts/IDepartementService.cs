using ManageEmployees.Dtos.Department;

namespace ManageEmployees.Services.Contracts
{
    public interface IDepartementService
    {
        Task<ReadDepartment> CreateDepartmentAsync(CreateDepartment department);
    }
}
