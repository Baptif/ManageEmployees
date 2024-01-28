using AutoMapper;
using ManageEmployees.Dtos.LeaveRequest;
using ManageEmployees.Entities;
using ManageEmployees.Repositories.Contracts;
using ManageEmployees.Repositories.Implementations;
using ManageEmployees.Services.Contracts;

namespace ManageEmployees.Services.Implementations
{
    public class LeaveRequestService : ILeaveRequestService
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILeaveRequestStatusRepository _leaveRequestStatusRepository;
        private readonly IMapper _mapper;

        public LeaveRequestService(ILeaveRequestRepository leaveRequestRepository, IEmployeeRepository employeeRepository, ILeaveRequestStatusRepository leaveRequestStatusRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _leaveRequestRepository = leaveRequestRepository;
            _leaveRequestStatusRepository = leaveRequestStatusRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Récupère toutes les demandes de congé pour un employé.
        /// </summary>
        /// <param name="employeeId">ID de l'employé.</param>
        /// <returns>Liste des demandes de congé pour l'employé.</returns>
        public async Task<List<ReadLeaveRequest>> GetLeaveRequestsByEmployeeId(int employeeId)
        {
            var leaveRequestList = await _leaveRequestRepository.GetLeaveRequestsByEmployeeIdAsync(employeeId);

            // Utiliser AutoMapper pour mapper la liste de LeaveRequest à une liste de ReadLeaveRequest
            return _mapper.Map<List<ReadLeaveRequest>>(leaveRequestList);
        }

        /// <summary>
        /// Récupère une demande de congé par son ID.
        /// </summary>
        /// <param name="leaveRequestId">ID de la demande de congé.</param>
        /// <returns>Informations détaillées de la demande de congé.</returns>
        public async Task<ReadLeaveRequest> GetLeaveRequestById(int leaveRequestId)
        {
            var leaveRequest = await _leaveRequestRepository.GetLeaveRequestByIdAsync(leaveRequestId);

            if (leaveRequest == null)
            {
                throw new Exception($"Echec de récupération des informations de demande de congé car elle n'existe pas : {leaveRequestId}");
            }

            return _mapper.Map<ReadLeaveRequest>(leaveRequest);
        }

        /// <summary>
        /// Crée une nouvelle demande de congé.
        /// </summary>
        /// <param name="leaveRequest">Informations de la nouvelle demande de congé.</param>
        /// <returns>Informations de la demande de congé créée.</returns>
        public async Task<ReadLeaveRequest> CreateLeaveRequestAsync(CreateLeaveRequest leaveRequest)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(leaveRequest.EmployeeId);
            if (employee is null)
            {
                throw new Exception($"Echec de création de congé, l'employé n'existe pas : {leaveRequest.EmployeeId}");
            }

            if (leaveRequest.StartDate >= leaveRequest.EndDate)
            {
                throw new Exception($"Echec de création de congé la date de début est supérieur ou égale à la date de fin");
            }

            var existingLeaveRequests = await _leaveRequestRepository.GetLeaveRequestsByEmployeeIdAsync(leaveRequest.EmployeeId);

            if (existingLeaveRequests.Any(lr =>
                (leaveRequest.StartDate >= lr.StartDate && leaveRequest.StartDate <= lr.EndDate) ||
                (leaveRequest.EndDate >= lr.StartDate && leaveRequest.EndDate <= lr.EndDate) ||
                (leaveRequest.StartDate <= lr.StartDate && leaveRequest.EndDate >= lr.EndDate)))
            {
                throw new Exception($"Echec de création de congé : l'employé a déjà une demande de congé pour cette période.");
            }

            var leaveRequestEntity = _mapper.Map<LeaveRequest>(leaveRequest);
            leaveRequestEntity.LeaveRequestStatusId = 1;
            leaveRequestEntity.RequestDate = DateTime.Now;

            var leaveRequestCreated = await _leaveRequestRepository.CreateLeaveRequestAsync(leaveRequestEntity);

            return _mapper.Map<ReadLeaveRequest>(leaveRequestCreated);
        }

        /// <summary>
        /// Met à jour le statut d'une demande de congé.
        /// </summary>
        /// <param name="leaveRequestId">ID de la demande de congé.</param>
        /// <param name="updateLeaveRequestStatus">Informations de mise à jour du statut de la demande de congé.</param>
        public async Task UpdateLeaveRequestStatus(int leaveRequestId, UpdateLeaveRequest updateLeaveRequestStatus)
        {
            var leaveRequest = await _leaveRequestRepository.GetLeaveRequestByIdAsync(leaveRequestId);
            if (leaveRequest == null)
            {
                throw new Exception($"Echec de mise à jour du statut de la demande de congé : La demande de congé n'existe pas : {leaveRequestId}");
            }

            var leaveRequestStatus = await _leaveRequestStatusRepository.GetLeaveRequestStatusByIdAsync(updateLeaveRequestStatus.LeaveRequestStatusId);
            if (leaveRequestStatus == null)
            {
                throw new Exception($"Echec de mise à jour du statut de la demande de congé : Le statut n'existe n'existe pas : {updateLeaveRequestStatus.LeaveRequestStatusId}");
            }

            if (updateLeaveRequestStatus.LeaveRequestStatusId <= leaveRequest.LeaveRequestStatusId)
            {
                throw new Exception($"Echec de mise à jour du statut de la demande de congé : Le statut ne peut que évoluer");
            }

            leaveRequest.LeaveRequestStatusId = updateLeaveRequestStatus.LeaveRequestStatusId;
            await _leaveRequestRepository.UpdateLeaveRequestAsync(leaveRequest);
        }

        /// <summary>
        /// Supprime une demande de congé par son ID.
        /// </summary>
        /// <param name="leaveRequestId">ID de la demande de congé à supprimer.</param>
        public async Task DeleteLeaveRequestById(int leaveRequestId)
        {
            var leaveRequest = await _leaveRequestRepository.GetLeaveRequestByIdAsync(leaveRequestId);

            if (leaveRequest == null)
            {
                throw new Exception($"Echec de suppression de la demande de congé, elle n'existe pas : {leaveRequestId}");
            }

            await _leaveRequestRepository.DeleteLeaveRequestByIdAsync(leaveRequestId);
        }
    }
}