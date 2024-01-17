using System;
using System.Collections.Generic;

namespace ManageEmployees.Entities;

public partial class LeaveRequest
{
    public int LeaveRequestId { get; set; }

    public int EmployeeId { get; set; }

    public int LeaveRequestStatusId { get; set; }

    public DateTime RequestDate { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual LeaveRequestStatus LeaveRequestStatus { get; set; } = null!;
}
