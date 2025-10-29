using System.ComponentModel;

namespace EmployeeContract.Entities;

public class EmployeeType
{
    [Browsable(false)]
    public Guid Id { get; set; } = Guid.NewGuid();

    [DisplayName("Имя")]
    public required string Name { get; set; }
}
