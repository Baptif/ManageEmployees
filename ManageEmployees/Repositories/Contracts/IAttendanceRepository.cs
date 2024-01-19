using ManageEmployees.Entities;

namespace ManageEmployees.Repositories.Contracts
{
    public interface IAttendanceRepository
    {
        Task<List<Attendance>> GetAttendanceByEmployeeIdAsync(int employeeId);
        Task<Attendance> GetAttendanceByIdAsync(int attendanceId);
        Task CreateAttendanceAsync(Attendance attendance);
        Task<Attendance> DeleteAttendanceByIdAsync(int attendanceId);
    }
}
