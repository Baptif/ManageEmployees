using ManageEmployees.Entities;
using ManageEmployees.Infrastructures.Database;
using ManageEmployees.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ManageEmployees.Repositories.Implementations
{
    public class LeaveRequestRepository : ILeaveRequestRepository
    {
        private readonly ManageEmployeeDbContext _dbContext;

        public LeaveRequestRepository(ManageEmployeeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<LeaveRequest>> GetLeaveRequestsByEmployeeIdAsync(int employeeId)
        {
            return await _dbContext.LeaveRequests
                .Where(lr => lr.EmployeeId == employeeId)
                .ToListAsync();
        }

        public async Task<LeaveRequest> GetLeaveRequestByIdAsync(int leaveRequestId)
        {
            return await _dbContext.LeaveRequests.FirstOrDefaultAsync(x => x.LeaveRequestId == leaveRequestId);
        }

        public async Task<LeaveRequest> CreateLeaveRequestAsync(LeaveRequest leaveRequest)
        {
            await _dbContext.LeaveRequests.AddAsync(leaveRequest);
            await _dbContext.SaveChangesAsync();

            return leaveRequest;
        }

        public async Task UpdateLeaveRequestAsync(LeaveRequest leaveRequest)
        {
            _dbContext.LeaveRequests.Update(leaveRequest);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<LeaveRequest> DeleteLeaveRequestByIdAsync(int leaveRequestsId)
        {
            var leaveRequestToDelete = await _dbContext.LeaveRequests.FindAsync(leaveRequestsId);
            _dbContext.LeaveRequests.Remove(leaveRequestToDelete);
            await _dbContext.SaveChangesAsync();

            return leaveRequestToDelete;
        }
    }
}

