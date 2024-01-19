using ManageEmployees.Dtos.Attendance;

namespace ManageEmployees.Services.Contracts
{
    public interface IAttendanceService
    {
        Task<List<ReadAttendance>> GetAttendanceByEmployeeId(int employeeId);
        Task<ReadAttendance> GetAttendanceById(int attendanceId);
        Task<ReadAttendance> CreateAttendanceAsync(CreateAttendance attendance);
        Task DeleteAttendanceById(int attendanceId);
    }
}
