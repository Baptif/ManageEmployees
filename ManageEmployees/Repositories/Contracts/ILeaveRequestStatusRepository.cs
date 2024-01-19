using ManageEmployees.Entities;

namespace ManageEmployees.Repositories.Contracts
{
    public interface ILeaveRequestStatusRepository
    {
        Task<List<LeaveRequestStatus>> GetLeaveRequestStatusesAsync();
        Task<LeaveRequestStatus> GetLeaveRequestStatusByIdAsync(int leaveRequestStatusId);
        Task<LeaveRequestStatus> CreateLeaveRequestStatusAsync(LeaveRequestStatus leaveRequestStatus);
        Task UpdateLeaveRequestStatusAsync(LeaveRequestStatus leaveRequestStatus);
        Task<LeaveRequestStatus> DeleteLeaveRequestStatusAsync(int leaveRequestStatusId);
    }
}
