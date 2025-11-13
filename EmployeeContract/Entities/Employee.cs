using System.ComponentModel;

namespace EmployeeContract.Entities;

public sealed class Employee
{
    [Browsable(false)]
    public Guid Id { get; set; } = Guid.NewGuid();

    public required string Name { get; set; }

    public string Purpose { get; set; }

    // Связь с подразделением
    public Guid EmployeeOrgId { get; set; }

    // Дата отчёта в текущем году (может отсутствовать)
    public DateTime? ReportDate { get; set; }
}
