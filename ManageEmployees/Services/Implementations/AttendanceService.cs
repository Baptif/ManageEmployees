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

        public AttendanceService(IAttendanceRepository attendanceRepository, IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _attendanceRepository = attendanceRepository;
        }

        public async Task<List<ReadAttendance>> GetAttendanceByEmployeeId(int employeeId)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(employeeId);

            if (employee is null)
                throw new Exception($"Echec de lecture de la présence, l'employé n'existe pas : {employeeId}");

            var attendanceList = await _attendanceRepository.GetAttendanceByEmployeeIdAsync(employeeId);

            return attendanceList.Select(attendance => new ReadAttendance
            {
                AttendanceId = attendance.AttendanceId,
                EmployeeId = attendance.EmployeeId,
                StartDate = attendance.StartDate,
                EndDate = attendance.EndDate
            }).ToList();
        }

        public async Task<ReadAttendance> GetAttendanceById(int attendanceId)
        {
            var attendance = await _attendanceRepository.GetAttendanceByIdAsync(attendanceId);

            if (attendance == null)
            {
                throw new Exception($"Echec de récupération des informations de présence car elle n'existe pas : {attendanceId}");
            }

            return new ReadAttendance
            {
                AttendanceId = attendance.AttendanceId,
                EmployeeId = attendance.EmployeeId,
                StartDate = attendance.StartDate,
                EndDate = attendance.EndDate
            };
        }

        public async Task<ReadAttendance> CreateAttendanceAsync(CreateAttendance attendance)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(attendance.EmployeeId);

            if (employee is null)
                throw new Exception($"Echec de création de l'attendance, l'employé n'existe pas : {attendance.EmployeeId}");

            var attendanceToCreate = new Attendance
            {
                EmployeeId = attendance.EmployeeId,
                StartDate = attendance.StartDate,
                EndDate = attendance.EndDate
            };

            await _attendanceRepository.CreateAttendanceAsync(attendanceToCreate);

            return new ReadAttendance
            {
                EmployeeId = attendance.EmployeeId,
                StartDate = attendance.StartDate,
                EndDate = attendance.EndDate
            };
        }

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

