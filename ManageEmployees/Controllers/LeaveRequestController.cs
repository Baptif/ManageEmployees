using ManageEmployees.Dtos.LeaveRequest;
using ManageEmployees.Entities;
using ManageEmployees.Services.Contracts;
using ManageEmployees.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace ManageEmployees.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestController : ControllerBase
    {
        private readonly ILeaveRequestService _leaveRequestService;

        public LeaveRequestController(ILeaveRequestService leaveRequestService)
        {
            _leaveRequestService = leaveRequestService;
        }

        [HttpGet("employee/{employeeId}")]
        public async Task<ActionResult<List<ReadLeaveRequest>>> GetLeaveRequestsByEmployeeId(int employeeId)
        {
            if (employeeId < 0)
            {
                return BadRequest("Echec de lecture des congés de l'employé : l'ID est inférieur à 0");
            }

            try
            {
                var leaveRequestList = await _leaveRequestService.GetLeaveRequestsByEmployeeId(employeeId);
                return Ok(leaveRequestList);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{leaveRequestId}")]
        public async Task<ActionResult<ReadLeaveRequest>> GetLeaveRequestById(int leaveRequestId)
        {
            if (leaveRequestId < 0)
            {
                return BadRequest("Echec de lecture du congé : l'ID est inférieur à 0");
            }

            try
            {
                var leaveRequest = await _leaveRequestService.GetLeaveRequestById(leaveRequestId);
                return Ok(leaveRequest);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ReadLeaveRequest>> CreateLeaveRequest([FromBody] CreateLeaveRequest leaveRequest)
        {
            if (leaveRequest == null
                || leaveRequest.EmployeeId < 0
                || leaveRequest.StartDate == null
                || leaveRequest.EndDate == null
            )
            {
                return BadRequest("Echec de création d'un congé : les informations sont null ou vides");
            }

            try
            {
                var leaveRequestCrea = await _leaveRequestService.CreateLeaveRequestAsync(leaveRequest);
                return Ok(leaveRequestCrea);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut("status/{leaveRequestId}")]
        public async Task<ActionResult> UpdateLeaveRequestStatus(int leaveRequestId, [FromBody] UpdateLeaveRequest updateLeaveRequestStatus)
        {
            if (leaveRequestId < 0)
            {
                return BadRequest("Echec de lecture du congé : l'ID est inférieur à 0");
            }

            if (updateLeaveRequestStatus == null
                || updateLeaveRequestStatus.LeaveRequestStatusId < 0
            )
            {
                return BadRequest("Echec de création d'un congé : les informations sont null ou vides");
            }

            try
            {
                await _leaveRequestService.UpdateLeaveRequestStatus(leaveRequestId, updateLeaveRequestStatus);
                return Ok($"Succès : Statut de la demande de congé {leaveRequestId} mis à jour.");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLeaveRequest(int id)
        {
            if (id < 0)
            {
                return BadRequest("Echec de suppression du congé : l'ID est inférieur à 0");
            }

            try
            {
                await _leaveRequestService.DeleteLeaveRequestById(id);
                return Ok("Congé supprimé d'ID : " + id);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}

