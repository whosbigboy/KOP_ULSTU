using EmployeeContract;

namespace EmployeeDirectory;

public class EmployeeDirectoryComponent : IComponentContract
{
    private static readonly IComponentMetadata _metadata =
        new ComponentMetadata(
            id: "EmployeeDirectory",
            title: "Сотрудник",
            componentType: ComponentType.Directory,
            requiredAccess: AccessLevel.Basic);

    public IComponentMetadata Metadata => _metadata;

    public UserControl CreateControl(IHostServices host)
        => new EmployeeDirectoryControl(host);
}
