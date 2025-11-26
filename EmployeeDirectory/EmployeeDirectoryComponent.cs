using EmployeeContract;
using System.Reflection;

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
    {
        try
        {
            // Проверяем загрузку сборки
            var assembly = Assembly.Load("ControlsLibraryNet90");
            Console.WriteLine($"Assembly loaded from: {assembly.Location}");

            return new EmployeeDirectoryControl(host);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading assembly: {ex}");
            throw;
        }
    }
}
