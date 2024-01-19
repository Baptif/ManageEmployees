using ManageEmployees.Dtos.Department;
using ManageEmployees.Dtos.Employee;
using ManageEmployees.Entities;
using ManageEmployees.Repositories.Contracts;
using ManageEmployees.Repositories.Implementations;
using ManageEmployees.Services.Contracts;
using System.Collections.Generic;

namespace ManageEmployees.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartementRepository _departementRepository;
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly ILeaveRequestRepository _leaveRequestRepository;

        public EmployeeService(IEmployeeRepository employeeRepository, IDepartementRepository departementRepository, 
            IAttendanceRepository attendanceRepository, ILeaveRequestRepository leaveRequestRepository)
        {
            _employeeRepository = employeeRepository;
            _departementRepository = departementRepository;
            _attendanceRepository = attendanceRepository;
            _leaveRequestRepository = leaveRequestRepository;
        }

        public async Task<List<ReadEmployee>> GetEmployees()
        {
            var employees = await _employeeRepository.GetEmployeesAsync();

            List<ReadEmployee> readEmployees = new List<ReadEmployee>();

            foreach (var employee in employees)
            {
                readEmployees.Add(new ReadEmployee()
                {
                    Id = employee.EmployeeId,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Email = employee.Email,
                });
            }

            return readEmployees;
        }


        public async Task<DetailEmployee> GetEmployeeByIdAsync(int employeeId)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(employeeId);

            if (employee is null)
                throw new Exception($"Echec de recupération des informations de l'employé car il n'existe pas : {employeeId}");

            return new DetailEmployee()
            {
                Id = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                BirthDate = employee.BirthdDate,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                Position = employee.Position
            };
        }

        public async Task UpdateEmployeeAsync(int employeeId, UpdateEmployee employee)
        {
            var employeeGet = await _employeeRepository.GetEmployeeByIdAsync(employeeId)
                ?? throw new Exception($"Echec de mise à jour d'un employé : Il n'existe aucun employé avec cet identifiant : {employeeId}");

            var employeeGetByEmail = await _employeeRepository.GetEmployeeByEmailAsync(employee.Email);
            if (employeeGetByEmail is not null && employeeId != employeeGetByEmail.EmployeeId)
            {
                throw new Exception($"Echec de mise à jour d'un employé : Il existe déjà un employé avec cette Email {employeeGetByEmail.Email}");
            }

            employeeGet.FirstName = employee.FirstName;
            employeeGet.LastName = employee.LastName;
            employeeGet.BirthdDate = employee.BirthDate;
            employeeGet.Email = employee.Email;
            employeeGet.PhoneNumber = employee.PhoneNumber;
            employeeGet.Position = employee.Position;

            await _employeeRepository.UpdateEmployeeAsync(employeeGet);
        }

        public async Task AddDepartmentToEmployee(int employeeId, int departmentId)
        {
            var employee = await _employeeRepository.GetEmployeeByIdWithIncludeAsync(employeeId);
            if (employee == null)
            {
                throw new Exception($"Echec d'ajout d'un département à un employé : Il n'existe aucun employé avec cet identifiant : {employeeId}");
            }

            if (employee.EmployeesDepartments.Any(x => x.DepartmentId == departmentId))
            {
                throw new Exception($"L'employé est déjà associé à ce département : {departmentId}");
            }

            var department = _departementRepository.GetDepartmentByIdAsync(departmentId);
            if (department == null)
            {
                throw new Exception($"Echec d'ajout d'un département à un employé : Il n'existe aucun département avec cet identifiant : {departmentId}");
            }

            var employeeDepartment = new EmployeesDepartment
            {
                EmployeeId = employeeId,
                DepartmentId = departmentId
            };

            await _employeeRepository.AddEmployeeDepartment(employeeDepartment);
        }

        public async Task RemoveDepartmentFromEmployee(int employeeId, int departmentId)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(employeeId);
            if (employee == null)
            {
                throw new Exception($"Echec d'ajout d'un département à un employé : Il n'existe aucun employé avec cet identifiant : {employeeId}");
            }

            var department = _departementRepository.GetDepartmentByIdAsync(departmentId);
            if (department == null)
            {
                throw new Exception($"Echec d'ajout d'un département à un employé : Il n'existe aucun département avec cet identifiant : {departmentId}");
            }

            await _employeeRepository.RemoveEmployeeDepartment(employeeId, departmentId);
        }

        public async Task DeleteEmployeeById(int employeeId)
        {
            var employeeGet = await _employeeRepository.GetEmployeeByIdAsync(employeeId)
                ?? throw new Exception($"Echec de suppression d'un employé : Il n'existe aucun employé avec cet identifiant : {employeeId}");

            var attendances = await _attendanceRepository.GetAttendanceByEmployeeIdAsync(employeeId);
            if (attendances.Any())
            {
                throw new Exception($"Echec de suppression d'un employé : il possède des présences");
            }

            var leaveRequest = await _leaveRequestRepository.GetLeaveRequestsByEmployeeIdAsync(employeeId);
            if (leaveRequest.Any())
            {
                throw new Exception($"Echec de suppression d'un employé : il possède des congés");
            }

            // TODO : Supprimer les départements de l'employé avant

            await _employeeRepository.DeleteEmployeeByIdAsync(employeeId);
        }

        public async Task<ReadEmployee> CreateEmployeeAsync(CreateEmployee employee)
        {
            var employeeGet = await _employeeRepository.GetEmployeeByEmailAsync(employee.Email);
            if (employeeGet is not null)
            {
                throw new Exception($"Echec de création d'un employé : il existe déjà un employé avec cette email {employee.Email}");
            }

            var employeeTocreate = new Employee()
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                BirthdDate = employee.BirthDate,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                Position = employee.Position,
            };

            var employeeCreated = await _employeeRepository.CreateEmployeeAsync(employeeTocreate);

            return new ReadEmployee()
            {
                Id = employeeCreated.EmployeeId,
                FirstName = employeeCreated.FirstName,
                LastName = employeeCreated.LastName,
                Email = employeeCreated.Email,
            };
        }
    }
}
