
namespace EmployeeContract;

public sealed class ComponentMetadata(string id, string title, ComponentType componentType, AccessLevel requiredAccess) : IComponentMetadata
{
    public string Id { get; } = id ?? throw new ArgumentNullException(nameof(id));
    public string Title { get; } = title ?? throw new ArgumentNullException(nameof(title));
    public ComponentType ComponentType { get; } = componentType;
    public AccessLevel RequiredAccess { get; } = requiredAccess;
}
