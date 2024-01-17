using ManageEmployees.Dtos.Department;

namespace ManageEmployees.Services.Contracts
{
    public interface IDepartementService
    {
        Task<List<ReadDepartment>> GetDepartments();
        Task<DetailDepartment> GetDepartmentByIdAsync(int departmentId);
        Task UpdateDepartmentAsync(int departmentId, UpdateDepartment department);
        Task DeleteDepartmentById(int departmentId);
        Task<ReadDepartment> CreateDepartmentAsync(CreateDepartment department);
    }
}
