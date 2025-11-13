using System.ComponentModel;

namespace EmployeeContract.Entities;

public sealed class Employee
{
    [Browsable(false)]
    public Guid Id { get; set; } = Guid.NewGuid();

    public required string FIO { get; set; }

    public string? Posts { get; set; }

    // Связь с подразделением
    public Guid EmployeeOrgId { get; set; }
    public EmployeeOrg EmployeeOrg { get; set; } = null; 

    public int WorkExp { get; set; }
}
