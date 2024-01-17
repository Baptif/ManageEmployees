using ManageEmployees.Entities;

namespace ManageEmployees.Repositories.Contracts
{
    public interface IDepartementRepository
    {
        Task<List<Department>> GetDepartmentsAsync();

        Task<Department> GetDepartmentByIdAsync(int departmentId);

        Task<Department> GetDepartmentByIdWithIncludeAsync(int departmentId);

        Task<Department> GetDepartmentByNameAsync(string departmentName);

        Task UpdateDepartmentAsync(Department departmentToUpdate);

        Task<Department> CreateDepartmentAsync(Department departmentToCreate);

        Task<Department> DeleteDepartmentByIdAsync(int departmentId);
    }
}
