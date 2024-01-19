using ManageEmployees.Dtos.LeaveRequestStatus;
using ManageEmployees.Entities;
using ManageEmployees.Repositories.Contracts;
using ManageEmployees.Services.Contracts;

namespace ManageEmployees.Services.Implementations
{
    public class LeaveRequestStatusService : ILeaveRequestStatusService
    {
        private readonly ILeaveRequestStatusRepository _leaveRequestStatusRepository;

        public LeaveRequestStatusService(ILeaveRequestStatusRepository leaveRequestStatusRepository)
        {
            _leaveRequestStatusRepository = leaveRequestStatusRepository;
        }

        public async Task<List<ReadLeaveRequestStatus>> GetLeaveRequestStatuses()
        {
            var leaveRequestStatuses = await _leaveRequestStatusRepository.GetLeaveRequestStatusesAsync();

            List<ReadLeaveRequestStatus> readLeaveRequestStatuses = new List<ReadLeaveRequestStatus>();

            foreach (var status in leaveRequestStatuses)
            {
                readLeaveRequestStatuses.Add(new ReadLeaveRequestStatus()
                {
                    Id = status.LeaveRequestStatusId,
                    Status = status.Status,
                });
            }

            return readLeaveRequestStatuses;
        }

        public async Task<ReadLeaveRequestStatus> GetLeaveRequestStatusById(int leaveRequestStatusId)
        {
            var leaveRequestStatus = await _leaveRequestStatusRepository.GetLeaveRequestStatusByIdAsync(leaveRequestStatusId);

            if (leaveRequestStatus is null)
            {
                throw new Exception($"Echec de récupération des informations du statut de la demande de congé car le statut n'existe pas : {leaveRequestStatusId}");
            }

            return new ReadLeaveRequestStatus()
            {
                Id = leaveRequestStatus.LeaveRequestStatusId,
                Status = leaveRequestStatus.Status,
            };
        }

        public async Task<ReadLeaveRequestStatus> CreateLeaveRequestStatusAsync(CreateLeaveRequestStatus leaveRequestStatus)
        {
            var newLeaveRequestStatus = new LeaveRequestStatus()
            {
                Status = leaveRequestStatus.Status,
            };

            var createdLeaveRequestStatus = await _leaveRequestStatusRepository.CreateLeaveRequestStatusAsync(newLeaveRequestStatus);

            return new ReadLeaveRequestStatus()
            {
                Id = createdLeaveRequestStatus.LeaveRequestStatusId,
                Status = createdLeaveRequestStatus.Status,
            };
        }

        public async Task UpdateLeaveRequestStatus(int leaveRequestStatusId, UpdateLeaveRequestStatus updateLeaveRequestStatus)
        {
            var leaveRequestStatus = await _leaveRequestStatusRepository.GetLeaveRequestStatusByIdAsync(leaveRequestStatusId);

            if (leaveRequestStatus == null)
            {
                throw new Exception($"Echec de mise à jour du statut de la demande de congé : Le statut n'existe pas : {leaveRequestStatusId}");
            }

            leaveRequestStatus.Status = updateLeaveRequestStatus.Status;

            await _leaveRequestStatusRepository.UpdateLeaveRequestStatusAsync(leaveRequestStatus);
        }

        public async Task DeleteLeaveRequestStatus(int leaveRequestStatusId)
        {
            var leaveRequestStatus = await _leaveRequestStatusRepository.GetLeaveRequestStatusByIdAsync(leaveRequestStatusId);

            if (leaveRequestStatus == null)
            {
                throw new Exception($"Echec de suppression du statut de la demande de congé : Le statut n'existe pas : {leaveRequestStatusId}");
            }

            await _leaveRequestStatusRepository.DeleteLeaveRequestStatusAsync(leaveRequestStatusId);
        }
    }
}
