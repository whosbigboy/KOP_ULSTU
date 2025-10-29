using System.ComponentModel;

namespace EmployeeContract.Entities;

public sealed class Employee
{
    [Browsable(false)]
    public Guid Id { get; set; } = Guid.NewGuid();

    public required string Name { get; set; }

    public string? Purpose { get; set; }

    // Связь с типом подразделения
    public Guid EmployeeTypeId { get; set; }
    public EmployeeType EmployeeType { get; set; } = null!;

    // Дата отчёта в текущем году (может отсутствовать)
    public DateTime? ReportDate { get; set; }

    // Для иерархии подразделений
    public Guid? ParentId { get; set; }
    public Employee? Parent { get; set; }
    public ICollection<Employee> Children { get; set; } = [];
}
