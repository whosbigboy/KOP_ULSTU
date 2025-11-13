using EmployeeContract;
using System.Reflection;

namespace EmployeeForms.Composition;

internal sealed class ComponentLoader
{
    private readonly string _pluginsPath;
    private readonly AccessLevel _currentAccessLevel;

    public ComponentLoader(string pluginsPath, AccessLevel currentAccessLevel)
    {
        _pluginsPath = Path.GetFullPath(pluginsPath);
        _currentAccessLevel = currentAccessLevel;
        Directory.CreateDirectory(_pluginsPath);
    }

    public IReadOnlyList<IComponentContract> LoadAll()
    {
        var result = new List<IComponentContract>();

        foreach (var dll in Directory.EnumerateFiles(_pluginsPath, "*.dll"))
        {
            try
            {
                var assembly = Assembly.LoadFrom(dll);
                var types = assembly.GetTypes()
                    .Where(t => !t.IsAbstract && t.IsAssignableTo(typeof(IComponentContract)));

                foreach (var type in types)
                {
                    try
                    {
                        if (Activator.CreateInstance(type) is IComponentContract instance)
                        {
                            if (instance.Metadata.RequiredAccess <= _currentAccessLevel)
                                result.Add(instance);
                        }
                    }
                    catch { }
                }
            }
            catch { }
        }

        return result;
    }

}
