using ManageEmployees.Dtos.LeaveRequestStatus;
using ManageEmployees.Entities;
using ManageEmployees.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ManageEmployees.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestStatusController : ControllerBase
    {
        private readonly ILeaveRequestStatusService _leaveRequestStatusService;

        public LeaveRequestStatusController(ILeaveRequestStatusService leaveRequestStatusService)
        {
            _leaveRequestStatusService = leaveRequestStatusService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ReadLeaveRequestStatus>>> GetLeaveRequestStatuses()
        {
            try
            {
                var leaveRequestStatusList = await _leaveRequestStatusService.GetLeaveRequestStatuses();
                return Ok(leaveRequestStatusList);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{leaveRequestStatusId}")]
        public async Task<ActionResult<ReadLeaveRequestStatus>> GetLeaveRequestStatusById(int leaveRequestStatusId)
        {
            if (leaveRequestStatusId < 0)
            {
                return BadRequest("Echec de lecture du status du congé : l'ID est inférieur à 0");
            }

            try
            {
                var leaveRequestStatus = await _leaveRequestStatusService.GetLeaveRequestStatusById(leaveRequestStatusId);
                return Ok(leaveRequestStatus);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ReadLeaveRequestStatus>> CreateLeaveRequestStatus([FromBody] CreateLeaveRequestStatus leaveRequestStatus)
        {
            if (leaveRequestStatus == null 
                || string.IsNullOrWhiteSpace(leaveRequestStatus.Status))
            {
                return BadRequest("Echec de création d'un status : les informations sont null ou vides");
            }

            try
            {
                var leaveRequestStatusCreated = await _leaveRequestStatusService.CreateLeaveRequestStatusAsync(leaveRequestStatus);
                return Ok(leaveRequestStatusCreated);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut("{leaveRequestStatusId}")]
        public async Task<ActionResult> UpdateLeaveRequestStatus(int leaveRequestStatusId, [FromBody] UpdateLeaveRequestStatus updateLeaveRequestStatus)
        {
            if (leaveRequestStatusId < 0)
            {
                return BadRequest("Echec de lecture du status du congé : l'ID est inférieur à 0");
            }

            if (updateLeaveRequestStatus == null
                || string.IsNullOrWhiteSpace(updateLeaveRequestStatus.Status))
            {
                return BadRequest("Echec de création d'un status : les informations sont null ou vides");
            }

            try
            {
                await _leaveRequestStatusService.UpdateLeaveRequestStatus(leaveRequestStatusId, updateLeaveRequestStatus);
                return Ok($"Succès : Statut de la demande de congé {leaveRequestStatusId} mis à jour.");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete("{leaveRequestStatusId}")]
        public async Task<ActionResult> DeleteLeaveRequestStatus(int leaveRequestStatusId)
        {
            if (leaveRequestStatusId < 0)
            {
                return BadRequest("Echec de lecture du status du congé : l'ID est inférieur à 0");
            }

            try
            {
                await _leaveRequestStatusService.DeleteLeaveRequestStatus(leaveRequestStatusId);
                return Ok($"Succès : Statut de la demande de congé {leaveRequestStatusId} supprimé.");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
