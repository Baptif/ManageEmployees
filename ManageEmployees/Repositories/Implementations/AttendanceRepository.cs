using ManageEmployees.Entities;
using ManageEmployees.Infrastructures.Database;
using ManageEmployees.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ManageEmployees.Repositories.Implementations
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly ManageEmployeeDbContext _dbContext;

        public AttendanceRepository(ManageEmployeeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Attendance>> GetAttendanceByEmployeeIdAsync(int employeeId)
        {
            return await _dbContext.Attendances
                .Where(a => a.EmployeeId == employeeId)
                .ToListAsync();
        }

        public async Task<Attendance> GetAttendanceByIdAsync(int attendanceId)
        {
            return await _dbContext.Attendances.FirstOrDefaultAsync(x => x.AttendanceId == attendanceId);
        }

        public async Task CreateAttendanceAsync(Attendance attendance)
        {
            await _dbContext.Attendances.AddAsync(attendance);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Attendance> DeleteAttendanceByIdAsync(int attendanceId)
        {
            var attendanceToDelete = await _dbContext.Attendances.FindAsync(attendanceId);
            _dbContext.Attendances.Remove(attendanceToDelete);
            await _dbContext.SaveChangesAsync();

            return attendanceToDelete;
        }
    }
}
