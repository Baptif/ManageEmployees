using ManageEmployees.Entities;

namespace ManageEmployees.Repositories.Contracts
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetEmployeesAsync();

        Task<Employee> GetEmployeeByIdAsync(int employeeId);

        Task<Employee> GetEmployeeByEmailAsync(string employeeEmail);

        Task<Employee> GetEmployeeByIdWithIncludeAsync(int employeeId);

        Task UpdateEmployeeAsync(Employee employeeToUpdate);

        Task AddEmployeeDepartment(EmployeesDepartment employeeDepartment);

        Task RemoveEmployeeDepartment(int employeeId, int departmentId);

        Task<Employee> CreateEmployeeAsync(Employee employeeToCreate);

        Task<Employee> DeleteEmployeeByIdAsync(int employeeId);
    }
}
