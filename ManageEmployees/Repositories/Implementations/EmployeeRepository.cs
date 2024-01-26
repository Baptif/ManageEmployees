using ManageEmployees.Entities;
using ManageEmployees.Infrastructures.Database;
using ManageEmployees.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ManageEmployees.Repositories.Implementations
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ManageEmployeeDbContext _dbContext;

        public EmployeeRepository(ManageEmployeeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            return await _dbContext.Employees.ToListAsync();
        }

        public async Task<Employee> GetEmployeeByIdAsync(int employeeId)
        {
            return await _dbContext.Employees.FirstOrDefaultAsync(x => x.EmployeeId == employeeId);
        }

        public async Task<Employee> GetEmployeeByEmailAsync(string employeeEmail)
        {
            return await _dbContext.Employees.FirstOrDefaultAsync(x => x.Email == employeeEmail);
        }

        public async Task<Employee> GetEmployeeByIdWithIncludeAsync(int employeeId)
        {
            return await _dbContext
                .Employees.Include(x => x.EmployeesDepartments)
                .ThenInclude(x => x.Department)
                .FirstOrDefaultAsync(x => x.EmployeeId == employeeId);
        }

        public async Task UpdateEmployeeAsync(Employee employeeToUpdate)
        {
            _dbContext.Employees.Update(employeeToUpdate);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddEmployeeDepartment(EmployeesDepartment employeeDepartment)
        {
            await _dbContext.EmployeesDepartments.AddAsync(employeeDepartment);
            await _dbContext.SaveChangesAsync();
        }
        
        public async Task RemoveEmployeeDepartment(int employeeId, int departmentId)
        {
            var employeeDepartment = _dbContext.EmployeesDepartments
                .FirstOrDefault(x => x.EmployeeId == employeeId && x.DepartmentId == departmentId);

            _dbContext.EmployeesDepartments.Remove(employeeDepartment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveEmployeeFromDepartments(int employeeId)
        {
            var employeeDepartments = _dbContext.EmployeesDepartments.FirstOrDefault(x => x.EmployeeId == employeeId);

            _dbContext.EmployeesDepartments.Remove(employeeDepartments);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Employee> CreateEmployeeAsync(Employee employeeToCreate)
        {
            await _dbContext.Employees.AddAsync(employeeToCreate);
            await _dbContext.SaveChangesAsync();

            return employeeToCreate;
        }

        public async Task<Employee> DeleteEmployeeByIdAsync(int employeeId)
        {
            var employeeToDelete = await _dbContext.Employees.FindAsync(employeeId);
            _dbContext.Employees.Remove(employeeToDelete);
            await _dbContext.SaveChangesAsync();
            return employeeToDelete;
        }
    }
}
