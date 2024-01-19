using ManageEmployees.Dtos.Attendance;
using ManageEmployees.Services.Contracts;
using ManageEmployees.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace ManageEmployees.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;

        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [HttpGet("employee/{employeeId}")]
        public async Task<ActionResult<List<ReadAttendance>>> GetAttendanceByEmployeeId(int employeeId)
        {
            if (employeeId < 0)
            {
                return BadRequest("Echec de lecture des présences de l'employé : l'ID est inférieur à 0");
            }

            try
            {
                var attendanceList = await _attendanceService.GetAttendanceByEmployeeId(employeeId);
                return Ok(attendanceList);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{attendanceId}")]
        public async Task<ActionResult<ReadAttendance>> GetAttendanceById(int attendanceId)
        {
            if (attendanceId < 0)
            {
                return BadRequest("Echec de lecture des présences : l'ID est inférieur à 0");
            }

            try
            {
                var attendance = await _attendanceService.GetAttendanceById(attendanceId);
                return Ok(attendance);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ReadAttendance>> CreateAttendance([FromBody] CreateAttendance attendance)
        {
            if (
                attendance == null
                || attendance.EmployeeId == null
                || attendance.EndDate == null
                || attendance.StartDate == null
            )
            {
                return BadRequest("Echec de création d'une attendance : les informations sont null ou vides");
            }

            try
            {
                var attendanceId = await _attendanceService.CreateAttendanceAsync(attendance);
                return Ok(attendanceId);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAttendance(int id)
        {
            if (id < 0)
            {
                return BadRequest("Echec de suppression de la présence : l'ID est inférieur à 0");
            }

            try
            {
                await _attendanceService.DeleteAttendanceById(id);
                return Ok("Présence supprimé d'ID : " + id);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}

