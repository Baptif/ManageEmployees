using AutoMapper;
using ManageEmployees.Dtos.Attendance;
using ManageEmployees.Entities;
using ManageEmployees.Repositories.Contracts;
using ManageEmployees.Repositories.Implementations;
using ManageEmployees.Services.Contracts;

namespace ManageEmployees.Services.Implementations
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public AttendanceService(IAttendanceRepository attendanceRepository, IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _attendanceRepository = attendanceRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Récupère toutes les présences d'un employé.
        /// </summary>
        /// <param name="employeeId">L'identifiant de l'employé.</param>
        /// <returns>Liste des présences de l'employé.</returns>
        /// <exception cref="Exception">Levée si l'employé n'existe pas.</exception>
        public async Task<List<ReadAttendance>> GetAttendanceByEmployeeId(int employeeId)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(employeeId);

            if (employee is null)
                throw new Exception($"Echec de lecture de la présence, l'employé n'existe pas : {employeeId}");

            var attendanceList = await _attendanceRepository.GetAttendanceByEmployeeIdAsync(employeeId);

            return _mapper.Map<List<ReadAttendance>>(attendanceList);
        }

        /// <summary>
        /// Récupère les détails d'une présence à partir de son identifiant.
        /// </summary>
        /// <param name="attendanceId">L'identifiant de la présence.</param>
        /// <returns>Les détails de la présence.</returns>
        /// <exception cref="Exception">Levée si la présence n'existe pas.</exception>
        public async Task<ReadAttendance> GetAttendanceById(int attendanceId)
        {
            var attendance = await _attendanceRepository.GetAttendanceByIdAsync(attendanceId);

            if (attendance == null)
            {
                throw new Exception($"Echec de récupération des informations de présence car elle n'existe pas : {attendanceId}");
            }

            return _mapper.Map<ReadAttendance>(attendance);
        }

        /// <summary>
        /// Crée une nouvelle présence pour un employé avec les contrôles appropriés.
        /// </summary>
        /// <param name="attendance">Les détails de la présence à créer.</param>
        /// <returns>Les détails de la présence créée.</returns>
        /// <exception cref="Exception">
        /// Levée si l'employé n'existe pas, la date de début est supérieure à la date de fin,
        /// la durée de la présence n'est pas comprise entre 6 et 10 heures,
        /// ou une présence existe déjà pour cette journée.
        /// </exception>
        public async Task<ReadAttendance> CreateAttendanceAsync(CreateAttendance attendance)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(attendance.EmployeeId);

            if (employee is null)
            {
                throw new Exception($"Echec de création de la présence, l'employé n'existe pas : {attendance.EmployeeId}");
            }

            if (attendance.StartDate >= attendance.EndDate)
            {
                throw new Exception($"Echec de création de la présence la date de début est supérieur ou égale à la date de fin");
            }

            TimeSpan presenceDuration = (TimeSpan)(attendance.EndDate - attendance.StartDate);

            if (presenceDuration < TimeSpan.FromHours(6) || presenceDuration > TimeSpan.FromHours(10))
            {
                throw new Exception($"Echec de création de la présence : la durée de la présence doit être entre 6 et 10 heures.");
            }

            var existingAttendanceList = await _attendanceRepository.GetAttendanceByEmployeeIdAsync(attendance.EmployeeId);

            if (existingAttendanceList.Any(at =>
                (attendance.StartDate >= at.StartDate && attendance.StartDate <= at.EndDate) ||
                (attendance.EndDate >= at.StartDate && attendance.EndDate <= at.EndDate) ||
                (attendance.StartDate <= at.StartDate && attendance.EndDate >= at.EndDate)))
            {
                throw new Exception($"Echec de création de la présence : une présence existe déjà pour cette journée.");
            }

            var attendanceToCreate = _mapper.Map<Attendance>(attendance);

            await _attendanceRepository.CreateAttendanceAsync(attendanceToCreate);

            return _mapper.Map<ReadAttendance>(attendanceToCreate);
        }

        /// <summary>
        /// Supprime une présence par son identifiant.
        /// </summary>
        /// <param name="attendanceId">L'identifiant de la présence à supprimer.</param>
        /// <exception cref="Exception">Levée si la présence n'existe pas.</exception>
        public async Task DeleteAttendanceById(int attendanceId)
        {
            var attendance = await _attendanceRepository.GetAttendanceByIdAsync(attendanceId);

            if (attendance == null)
            {
                throw new Exception($"Echec de suppression de la demande de présence, elle n'existe pas : {attendanceId}");
            }

            await _attendanceRepository.DeleteAttendanceByIdAsync(attendanceId);
        }
    }
}

