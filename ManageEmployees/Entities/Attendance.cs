using System;
using System.Collections.Generic;

namespace ManageEmployees.Entities;

public partial class Attendance
{
    public int AttendanceId { get; set; }

    public int EmployeeId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
