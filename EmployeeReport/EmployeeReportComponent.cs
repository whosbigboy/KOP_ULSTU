using EmployeeContract;
using EmployeeReport;

namespace EmployeenReport;

public class EmployeeReportComponent : IComponentContract
{
    private static readonly IComponentMetadata _metadata =
        new ComponentMetadata(
            id: "EmployeeReport",
            title: "Отчет по сотрудникам",
            componentType: ComponentType.Report,
            requiredAccess: AccessLevel.Advanced);

    public IComponentMetadata Metadata => _metadata;

    public UserControl CreateControl(IHostServices host)
        => new EmployeeReportControl(host);
}
