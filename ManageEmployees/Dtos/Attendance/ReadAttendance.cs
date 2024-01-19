namespace ManageEmployees.Dtos.Attendance
{
    public class ReadAttendance
    {
        public int AttendanceId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
