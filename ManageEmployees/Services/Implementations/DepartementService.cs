using AutoMapper;
using ManageEmployees.Dtos.Department;
using ManageEmployees.Entities;
using ManageEmployees.Repositories.Contracts;
using ManageEmployees.Services.Contracts;

namespace ManageEmployees.Services.Implementations
{
    public class DepartementService : IDepartementService
    {
        private readonly IDepartementRepository _departementRepository;
        private readonly IMapper _mapper;

        public DepartementService(IDepartementRepository departementRepository, IMapper mapper)
        {
            _departementRepository = departementRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Récupère la liste de tous les départements.
        /// </summary>
        /// <returns>Liste des départements.</returns>
        public async Task<List<ReadDepartment>> GetDepartments()
        {
            var departments = await _departementRepository.GetDepartmentsAsync();

            var readDepartments = _mapper.Map<List<ReadDepartment>>(departments);

            return readDepartments;
        }

        /// <summary>
        /// Récupère les détails d'un département par son identifiant.
        /// </summary>
        /// <param name="departmentId">Identifiant du département.</param>
        /// <returns>Détails du département.</returns>
        public async Task<DetailDepartment> GetDepartmentByIdAsync(int departmentId)
        {
            var department = await _departementRepository.GetDepartmentByIdAsync(departmentId);

            if (department is null)
                throw new Exception($"Echec de récupération des informations d'un département car il n'existe pas : {departmentId}");

            var detailDepartment = _mapper.Map<DetailDepartment>(department);

            return detailDepartment;
        }

        /// <summary>
        /// Met à jour les informations d'un département.
        /// </summary>
        /// <param name="departmentId">Identifiant du département.</param>
        /// <param name="department">Informations de mise à jour du département.</param>
        public async Task UpdateDepartmentAsync(int departmentId, UpdateDepartment department)
        {
            var departmentGet = await _departementRepository.GetDepartmentByIdAsync(departmentId)
                ?? throw new Exception($"Echec de mise à jour d'un département : Il n'existe aucun département avec cet identifiant : {departmentId}");

            var departmentGetByName = await _departementRepository.GetDepartmentByNameAsync(department.Name);
            if (departmentGetByName is not null && departmentId != departmentGetByName.DepartmentId)
            {
                throw new Exception($"Echec de mise à jour d'un département : Il existe déjà un département avec ce nom {department.Name}");
            }

            // Utilisation d'AutoMapper pour appliquer les modifications
            _mapper.Map(department, departmentGet);

            await _departementRepository.UpdateDepartmentAsync(departmentGet);
        }

        /// <summary>
        /// Supprime un département par son identifiant.
        /// </summary>
        /// <param name="departmentId">Identifiant du département.</param>
        public async Task DeleteDepartmentById(int departmentId)
        {
            var departmentGet = await _departementRepository.GetDepartmentByIdWithIncludeAsync(departmentId)
              ?? throw new Exception($"Echec de suppression d'un département : Il n'existe aucun département avec cet identifiant : {departmentId}");

            if (departmentGet.EmployeesDepartments.Any())
            {
                throw new Exception("Echec de suppression car ce département est lié à des employés");
            }

            await _departementRepository.DeleteDepartmentByIdAsync(departmentId);
        }

        /// <summary>
        /// Crée un nouveau département.
        /// </summary>
        /// <param name="department">Informations du nouveau département.</param>
        /// <returns>Informations du département créé.</returns>
        public async Task<ReadDepartment> CreateDepartmentAsync(CreateDepartment department)
        {
            var departmentGet = await _departementRepository.GetDepartmentByNameAsync(department.Name);
            if (departmentGet is not null)
            {
                throw new Exception($"Echec de création d'un département : Il existe déjà un département avec ce nom {department.Name}");
            }

            var departementToCreate = _mapper.Map<Department>(department);

            var departmentCreated = await _departementRepository.CreateDepartmentAsync(departementToCreate);

            var readDepartment = _mapper.Map<ReadDepartment>(departmentCreated);

            return readDepartment;
        }
    }
}