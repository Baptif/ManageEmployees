using ManageEmployees.Entities;

namespace ManageEmployees.Repositories.Contracts
{
    public interface ILeaveRequestRepository
    {
        Task<List<LeaveRequest>> GetLeaveRequestsByEmployeeIdAsync(int employeeId);
        Task<LeaveRequest> GetLeaveRequestByIdAsync(int leaveRequestId);
        Task<LeaveRequest> CreateLeaveRequestAsync(LeaveRequest leaveRequest);
        Task UpdateLeaveRequestAsync(LeaveRequest leaveRequest);
        Task<LeaveRequest> DeleteLeaveRequestByIdAsync(int leaveRequestId);
    }
}

