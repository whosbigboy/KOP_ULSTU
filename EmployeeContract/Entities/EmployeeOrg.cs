using System.ComponentModel;

namespace EmployeeContract.Entities;

public class EmployeeOrg
{
    [Browsable(false)]
    public Guid Id { get; set; } = Guid.NewGuid();

    [DisplayName("Наименование")]
    public string Name { get; set; }
}
