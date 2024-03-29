﻿using ManageEmployees.Dtos.Department;
using ManageEmployees.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

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

        // GET: api/Departments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadDepartment>> GetDepartment(int id)
        {
            if (id < 0)
            {
                return BadRequest("Echec de lecture des départements : l'ID est égal à 0");
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

        // PUT: api/Departments/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutDepartment(int id, [FromBody] UpdateDepartment department)
        {
            if (id < 0)
            {
                return BadRequest("Echec de modification d'un departement : l'ID est égal à 0");
            }

            if (department == null || string.IsNullOrWhiteSpace(department.Name)
                || string.IsNullOrWhiteSpace(department.Address) || string.IsNullOrWhiteSpace(department.Description))
            {
                return BadRequest("Echec de modification d'un departement : les informations sont null ou vides");
            }

            try
            {
                await _departementService.UpdateDepartmentAsync(id, department);
                return Ok($"La modification du département numéro {id} a réussie");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // DELETE: api/Departments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDepartment(int id)
        {
            if (id < 0)
            {
                return BadRequest("Echec de suppression du departement : l'ID est égale à 0");
            }

            try
            {
                await _departementService.DeleteDepartmentById(id);
                return Ok($"La suppression du département numéro {id} a réussie");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
