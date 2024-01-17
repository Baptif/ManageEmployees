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

        // GET: api/<DepartmentsController>
        [HttpGet]
        public async Task<ActionResult<List<ReadDepartment>>> GetAll()
        {
            return await _departementService.GetDepartments();
        }

        // GET: api/Departments1/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadDepartment>> GetDepartment(int id)
        {
            if (id == 0)
            {
                return BadRequest("Echec de lecture des departement : l'ID est égale à 0");
            }

            try
            {
                var departmentReceived = await _departementService.GetDepartmentByIdAsync(id);
                return Ok(departmentReceived);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
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

        // PUT: api/Departments1/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutDepartment(int id, [FromBody] UpdateDepartment department)
        {
            if (id == 0)
            {
                return BadRequest("Echec de lecture des departement : l'id est égale à 0");
            }

            if (department == null || string.IsNullOrWhiteSpace(department.Name)
                || string.IsNullOrWhiteSpace(department.Address) || string.IsNullOrWhiteSpace(department.Description))
            {
                return BadRequest("Echec de création d'un departement : les informations sont null ou vides");
            }

            try
            {
                await _departementService.UpdateDepartmentAsync(id, department);
                return Ok("Succesfully updated department of ID : " + id);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // DELETE: api/Departments1/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDepartment(int id)
        {
            if (id == 0)
            {
                return BadRequest("Echec de suppression du departement : l'ID est égale à 0");
            }

            try
            {
                await _departementService.DeleteDepartmentById(id);
                return Ok("Succesfully deleted department of ID : "+ id);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
