using ManageEmployees.Dtos.Department;
using ManageEmployees.Entities;
using ManageEmployees.Services.Contracts;
using ManageEmployees.Services.Implementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ManageEmployees.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartementService _departementService;

        public DepartmentsController(IDepartementService departementService)
        {
            _departementService = departementService;
        }

        // POST api/<DepartmentsController>
        [HttpPost]
        public async Task<ActionResult<ReadDepartment>> Post([FromBody] CreateDepartment department)
        {
            if (department == null || string.IsNullOrWhiteSpace(department.Name)
                || string.IsNullOrWhiteSpace(department.Address) || string.IsNullOrWhiteSpace(department.Description))
            {
                return BadRequest("Echec de création d'un departement : les informations sont null ou vides");
            }

            try
            {
                var departmentCreated = await _departementService.CreateDepartmentAsync(department);
                return Ok(departmentCreated);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
