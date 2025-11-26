using EmployeeContract;

namespace TypeDirectory;

public sealed class EmployeeTypeCatalogComponent : IComponentContract
{
    private static readonly IComponentMetadata _metadata =
            new ComponentMetadata(
                id: "EmployeeDirectoryCatalog",
                title: "Справочник отделов",
                componentType: ComponentType.Directory,
                requiredAccess: AccessLevel.Minimal);

    public IComponentMetadata Metadata => _metadata;

    public UserControl CreateControl(IHostServices hostServices)
        => new TypeDirectoryControl(hostServices);
}