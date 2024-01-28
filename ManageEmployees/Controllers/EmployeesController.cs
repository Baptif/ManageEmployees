using ManageEmployees.Dtos.Department;
using ManageEmployees.Dtos.Employee;
using ManageEmployees.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;


namespace ManageEmployees.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // GET: api/<EmployeesController>
        [HttpGet]
        public async Task<ActionResult<List<ReadEmployee>>> GetAll()
        {
            return await _employeeService.GetEmployees();
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadEmployee>> GetEmployee(int id)
        {
            if (id < 0)
            {
                return BadRequest("Echec de lecture de l'employé : l'ID est inférieur à 0");
            }

            try
            {
                var employeeReceived = await _employeeService.GetEmployeeByIdAsync(id);
                return Ok(employeeReceived);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // POST api/<EmployeesController>
        [HttpPost]
        public async Task<ActionResult<ReadEmployee>> Post([FromBody] CreateEmployee employee)
        {
            if (employee == null 
                || string.IsNullOrWhiteSpace(employee.FirstName)
                || string.IsNullOrWhiteSpace(employee.LastName) 
                || string.IsNullOrWhiteSpace(employee.Position)
                || employee.BirthDate == null)
            {
                return BadRequest("Echec de création d'un employé : les informations sont null ou vides");
            } 
            else if (!IsValidEmail(employee.Email))
            {
                return BadRequest("Echec de création d'un employé : le format de l'email n'est pas le bon");
            }
            else if (!IsValidPhoneNumber(employee.PhoneNumber))
            {
                return BadRequest("Echec de création d'un employé : le format de du numéro de téléphone n'est pas le bon");
            }

            try
            {
                var employeeCreated = await _employeeService.CreateEmployeeAsync(employee);
                return Ok(employeeCreated);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // PUT: api/Employees/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutEmployee(int id, [FromBody] UpdateEmployee employee)
        {
            if (id < 0)
            {
                return BadRequest("Echec de modification de l'employé : l'ID est inférieur à 0");
            }

            if (employee == null
                || string.IsNullOrWhiteSpace(employee.FirstName)
                || string.IsNullOrWhiteSpace(employee.LastName)
                || string.IsNullOrWhiteSpace(employee.Position)
                || employee.BirthDate == null)
            {
                return BadRequest("Echec de modification d'un employé : les informations sont null ou vides");
            }
            else if (!IsValidEmail(employee.Email))
            {
                return BadRequest("Echec de modification d'un employé : le format de l'email n'est pas le bon");
            }
            else if (!IsValidPhoneNumber(employee.PhoneNumber))
            {
                return BadRequest("Echec de modification d'un employé : le format de du numéro de téléphone n'est pas le bon");
            }

            try
            {
                await _employeeService.UpdateEmployeeAsync(id, employee);
                return Ok($"Modification des données de l'employé numéro {id} réussie");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            if (id < 0)
            {
                return BadRequest("Echec de suppression de l'employé : l'ID est égale à 0");
            }

            try
            {
                await _employeeService.DeleteEmployeeById(id);
                return Ok($"Suppression de l'employé numéro {id} réussie");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{employeeId}/departments")]
        public async Task<ActionResult<List<ReadDepartment>>> GetDepartmentsForEmployee(int employeeId)
        {
            if (employeeId < 0)
            {
                return BadRequest("Echec de la suppression d'un département à l'employé : le format de l'ID est invalide");
            }

            try
            {
                var departments = await _employeeService.GetDepartmentsForEmployee(employeeId);
                return Ok(departments);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut("{employeeId}/departments/{departmentId}")]
        public async Task<ActionResult> AddDepartmentToEmployee(int employeeId, int departmentId)
        {
            if (employeeId < 0)
            {
                return BadRequest("Echec de d'ajout d'un département à l'employé : le format de l'ID est invalide");
            }
            if (departmentId < 0)
            {
                return BadRequest("Echec de d'ajout d'un département à l'employé : le format de l'ID est invalide");
            }

            try
            {
                await _employeeService.AddDepartmentToEmployee(employeeId, departmentId);
                return Ok($"Département numéro {departmentId} ajouté avec succès à l'employé numéro {employeeId}");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete("{employeeId}/departments/{departmentId}")]
        public async Task<ActionResult> RemoveDepartmentFromEmployee(int employeeId, int departmentId)
        {
            if (employeeId < 0)
            {
                return BadRequest("Echec de la suppression d'un département à l'employé : le format de l'ID est invalide");
            }
            if (departmentId < 0)
            {
                return BadRequest("Echec de la suppression d'un département à l'employé : le format de l'ID est invalide");
            }

            try
            {
                await _employeeService.RemoveDepartmentFromEmployee(employeeId, departmentId);
                return Ok($"Département numéro {departmentId} supprimé avec succès de l'employé numéro {employeeId}");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        // Fonctions de vérifications //
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex regex = new Regex(pattern);

            return regex.IsMatch(email);
        }
        
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber) || phoneNumber.Length > 13)
            {
                return false;
            }

            string pattern = @"^\+[0-9]{1,3}-?[0-9]{1,14}$";
            Regex regex = new Regex(pattern);

            return regex.IsMatch(phoneNumber);
        }
    }
}
