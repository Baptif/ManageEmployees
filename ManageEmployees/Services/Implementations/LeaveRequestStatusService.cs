using AutoMapper;
using ManageEmployees.Dtos.LeaveRequestStatus;
using ManageEmployees.Entities;
using ManageEmployees.Repositories.Contracts;
using ManageEmployees.Services.Contracts;

namespace ManageEmployees.Services.Implementations
{
    public class LeaveRequestStatusService : ILeaveRequestStatusService
    {
        private readonly ILeaveRequestStatusRepository _leaveRequestStatusRepository;
        private readonly IMapper _mapper;

        public LeaveRequestStatusService(ILeaveRequestStatusRepository leaveRequestStatusRepository, IMapper mapper)
        {
            _leaveRequestStatusRepository = leaveRequestStatusRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Récupère tous les statuts de demande de congé.
        /// </summary>
        /// <returns>Liste des statuts de demande de congé.</returns>
        public async Task<List<ReadLeaveRequestStatus>> GetLeaveRequestStatuses()
        {
            var leaveRequestStatuses = await _leaveRequestStatusRepository.GetLeaveRequestStatusesAsync();

            return _mapper.Map<List<ReadLeaveRequestStatus>>(leaveRequestStatuses);
        }

        /// <summary>
        /// Récupère les informations d'un statut de demande de congé par son identifiant.
        /// </summary>
        /// <param name="leaveRequestStatusId">L'identifiant du statut de demande de congé.</param>
        /// <returns>Informations du statut de demande de congé.</returns>
        /// <exception cref="Exception">Levée si le statut de demande de congé n'existe pas.</exception>
        public async Task<ReadLeaveRequestStatus> GetLeaveRequestStatusById(int leaveRequestStatusId)
        {
            var leaveRequestStatus = await _leaveRequestStatusRepository.GetLeaveRequestStatusByIdAsync(leaveRequestStatusId);

            if (leaveRequestStatus is null)
            {
                throw new Exception($"Echec de récupération des informations du statut de la demande de congé car le statut n'existe pas : {leaveRequestStatusId}");
            }

            return _mapper.Map<ReadLeaveRequestStatus>(leaveRequestStatus);
        }

        /// <summary>
        /// Crée un nouveau statut de demande de congé.
        /// </summary>
        /// <param name="leaveRequestStatus">Informations du nouveau statut de demande de congé.</param>
        /// <returns>Informations du statut de demande de congé créé.</returns>
        public async Task<ReadLeaveRequestStatus> CreateLeaveRequestStatusAsync(CreateLeaveRequestStatus leaveRequestStatus)
        {
            var newLeaveRequestStatus = _mapper.Map<LeaveRequestStatus>(leaveRequestStatus);

            var createdLeaveRequestStatus = await _leaveRequestStatusRepository.CreateLeaveRequestStatusAsync(newLeaveRequestStatus);

            return _mapper.Map<ReadLeaveRequestStatus>(createdLeaveRequestStatus);
        }

        /// <summary>
        /// Met à jour les informations d'un statut de demande de congé.
        /// </summary>
        /// <param name="leaveRequestStatusId">L'identifiant du statut de demande de congé à mettre à jour.</param>
        /// <param name="updateLeaveRequestStatus">Informations mises à jour du statut de demande de congé.</param>
        /// <exception cref="Exception">Levée si le statut de demande de congé ou les nouvelles informations sont invalides.</exception>
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

        /// <summary>
        /// Supprime un statut de demande de congé par son identifiant.
        /// </summary>
        /// <param name="leaveRequestStatusId">L'identifiant du statut de demande de congé à supprimer.</param>
        /// <exception cref="Exception">Levée si le statut de demande de congé n'existe pas.</exception>
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
