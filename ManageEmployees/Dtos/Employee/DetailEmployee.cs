using ManageEmployees.Dtos.Department;
using ManageEmployees.Entities;

namespace ManageEmployees.Dtos.Employee
{
    public class DetailEmployee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Position { get; set; }

        //public List<Attendance> Attendances { get; set; }

        //public ICollection<ReadDepartment> EmployeesDepartments { get; set; }

        //public List<LeaveRequest> LeaveRequests { get; set; }
    }
}

