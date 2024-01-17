using System;
using System.Collections.Generic;

namespace ManageEmployees.Entities;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string Name { get; set; } = null!;

    public string? Address { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<EmployeesDepartment> EmployeesDepartments { get; set; } = new List<EmployeesDepartment>();
}
