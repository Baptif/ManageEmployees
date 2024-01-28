using AutoMapper;
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
        private readonly IMapper _mapper;

        public EmployeeService(
            IEmployeeRepository employeeRepository,
            IDepartementRepository departementRepository,
            IAttendanceRepository attendanceRepository,
            ILeaveRequestRepository leaveRequestRepository,
            IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _departementRepository = departementRepository;
            _attendanceRepository = attendanceRepository;
            _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Récupère la liste complète des employés.
        /// </summary>
        /// <returns>Une liste contenant les informations de tous les employés.</returns>
        public async Task<List<ReadEmployee>> GetEmployees()
        {
            var employees = await _employeeRepository.GetEmployeesAsync();
            return _mapper.Map<List<ReadEmployee>>(employees);
        }

        /// <summary>
        /// Récupère les détails d'un employé en fonction de son identifiant.
        /// </summary>
        /// <param name="employeeId">L'identifiant unique de l'employé.</param>
        /// <returns>Les détails de l'employé correspondant à l'identifiant spécifié.</returns>
        /// <exception cref="Exception">Levée si l'employé n'existe pas.</exception>
        public async Task<DetailEmployee> GetEmployeeByIdAsync(int employeeId)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(employeeId)
                ?? throw new Exception($"Échec de récupération des informations de l'employé {employeeId} car il n'existe pas");

            return _mapper.Map<DetailEmployee>(employee);
        }

        /// <summary>
        /// Met à jour les informations d'un employé existant.
        /// </summary>
        /// <param name="employeeId">L'identifiant unique de l'employé à mettre à jour.</param>
        /// <param name="employee">Les nouvelles informations de l'employé.</param>
        /// <exception cref="Exception">Levée si l'employé n'existe pas ou si une autre employé possède déjà la même adresse e-mail.</exception>
        public async Task UpdateEmployeeAsync(int employeeId, UpdateEmployee employee)
        {
            var employeeGet = await _employeeRepository.GetEmployeeByIdAsync(employeeId)
                ?? throw new Exception($"Échec de mise à jour d'un employé : Il n'existe aucun employé avec cet identifiant : {employeeId}");

            var employeeGetByEmail = await _employeeRepository.GetEmployeeByEmailAsync(employee.Email);
            if (employeeGetByEmail is not null && employeeId != employeeGetByEmail.EmployeeId)
            {
                throw new Exception($"Échec de mise à jour d'un employé : Il existe déjà un employé avec cette Email {employeeGetByEmail.Email}");
            }

            _mapper.Map(employee, employeeGet);

            await _employeeRepository.UpdateEmployeeAsync(employeeGet);
        }

        /// <summary>
        /// Ajoute un département à un employé existant.
        /// </summary>
        /// <param name="employeeId">L'identifiant unique de l'employé.</param>
        /// <param name="departmentId">L'identifiant unique du département à ajouter.</param>
        /// <exception cref="Exception">Levée si l'employé ou le département n'existe pas, ou si l'employé est déjà associé à ce département.</exception>
        public async Task AddDepartmentToEmployee(int employeeId, int departmentId)
        {
            var employee = await _employeeRepository.GetEmployeeByIdWithIncludeAsync(employeeId);
            if (employee == null)
            {
                throw new Exception($"Echec d'ajout d'un département à un employé : Il n'existe aucun employé avec cet identifiant : {employeeId}");
            }

            var department = await _departementRepository.GetDepartmentByIdAsync(departmentId);
            if (department == null)
            {
                throw new Exception($"Echec d'ajout d'un département à un employé : Il n'existe aucun département avec cet identifiant : {departmentId}");
            }

            if (employee.EmployeesDepartments.Any(x => x.DepartmentId == departmentId))
            {
                throw new Exception($"L'employé est déjà associé à ce département : {departmentId}");
            }

            var employeeDepartment = new EmployeesDepartment
            {
                EmployeeId = employeeId,
                DepartmentId = departmentId
            };

            await _employeeRepository.AddEmployeeDepartment(employeeDepartment);
        }

        /// <summary>
        /// Supprime un département d'un employé existant.
        /// </summary>
        /// <param name="employeeId">L'identifiant unique de l'employé.</param>
        /// <param name="departmentId">L'identifiant unique du département à supprimer.</param>
        /// <exception cref="Exception">Levée si l'employé ou le département n'existe pas.</exception>
        public async Task RemoveDepartmentFromEmployee(int employeeId, int departmentId)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(employeeId);
            if (employee == null)
            {
                throw new Exception($"Echec d'ajout d'un département à un employé : Il n'existe aucun employé avec cet identifiant : {employeeId}");
            }

            var department = await _departementRepository.GetDepartmentByIdAsync(departmentId);
            if (department == null)
            {
                throw new Exception($"Echec d'ajout d'un département à un employé : Il n'existe aucun département avec cet identifiant : {departmentId}");
            }

            await _employeeRepository.RemoveEmployeeDepartment(employeeId, departmentId);
        }

        /// <summary>
        /// Supprime un employé en fonction de son identifiant.
        /// </summary>
        /// <param name="employeeId">L'identifiant unique de l'employé à supprimer.</param>
        /// <exception cref="Exception">
        /// Levée si l'employé n'existe pas, s'il possède des présences ou des congés, ou s'il est associé à des départements.
        /// </exception>
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

            var departments = employeeGet.EmployeesDepartments
                .Select(ed => _mapper.Map<ReadDepartment>(ed.Department))
                .ToList();

            if (departments.Any())
            {
                await _employeeRepository.RemoveEmployeeFromDepartments(employeeId);
            }

            await _employeeRepository.DeleteEmployeeByIdAsync(employeeId);
        }

        /// <summary>
        /// Crée un nouvel employé.
        /// </summary>
        /// <param name="employee">Informations du nouvel employé.</param>
        /// <returns>Informations de l'employé créé.</returns>
        /// <exception cref="Exception">Levée si un employé avec la même adresse e-mail existe déjà.</exception>
        public async Task<ReadEmployee> CreateEmployeeAsync(CreateEmployee employee)
        {
            var existingEmployee = await _employeeRepository.GetEmployeeByEmailAsync(employee.Email);
            if (existingEmployee is not null)
            {
                throw new Exception($"Échec de création d'un employé : il existe déjà un employé avec cett email {employee.Email}");
            }

            var employeeToCreate = _mapper.Map<Employee>(employee);
            var employeeCreated = await _employeeRepository.CreateEmployeeAsync(employeeToCreate);

            return _mapper.Map<ReadEmployee>(employeeCreated);
        }

        /// <summary>
        /// Récupère la liste des départements associés à un employé en fonction de son identifiant.
        /// </summary>
        /// <param name="employeeId">L'identifiant unique de l'employé.</param>
        /// <returns>Une liste contenant les informations des départements associés à l'employé.</returns>
        /// <exception cref="Exception">Levée si l'employé n'existe pas ou s'il n'est associé à aucun département.</exception>
        public async Task<List<ReadDepartment>> GetDepartmentsForEmployee(int employeeId)
        {
            var employee = await _employeeRepository.GetEmployeeByIdWithIncludeAsync(employeeId)
                ?? throw new Exception($"Échec de récupération des départements pour l'employé : L'employé avec l'ID {employeeId} n'existe pas.");

            var departments = employee.EmployeesDepartments
                .Select(ed => _mapper.Map<ReadDepartment>(ed.Department))
                .ToList();

            if (!departments.Any())
            {
                throw new Exception($"L'employé {employeeId} n'est associé à aucun départements.");
            }

            return departments;
        }
    }
}
