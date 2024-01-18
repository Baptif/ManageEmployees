using ManageEmployees.Entities;
using Microsoft.VisualBasic;

namespace ManageEmployees.Dtos.Employee
{
    public class ReadEmployee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
