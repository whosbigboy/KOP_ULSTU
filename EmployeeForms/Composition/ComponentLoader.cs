using EmployeeContract;
using System.Reflection;

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
        var errorMessages = new List<string>();

        foreach (var dll in Directory.EnumerateFiles(_pluginsPath, "*.dll"))
        {
            try
            {
                var assembly = Assembly.LoadFrom(dll);
                var types = assembly.GetTypes()
                    .Where(t => !t.IsAbstract && typeof(IComponentContract).IsAssignableFrom(t));

                foreach (var type in types)
                {
                    try
                    {
                        if (Activator.CreateInstance(type) is IComponentContract instance)
                        {
                            // ВРЕМЕННО ОТКЛЮЧАЕМ ПРОВЕРКУ ДОСТУПА
                            // if (instance.Metadata.RequiredAccess <= _currentAccessLevel)
                            // {
                            result.Add(instance);
                            // }
                            // else
                            // {
                            //     errorMessages.Add($"Доступ запрещен: {instance.Metadata.Title} (требуется {instance.Metadata.RequiredAccess})");
                            // }
                        }
                    }
                    catch (Exception ex)
                    {
                        errorMessages.Add($"Ошибка создания {type.Name}: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessages.Add($"Ошибка загрузки {Path.GetFileName(dll)}: {ex.Message}");
            }
        }

        // Показываем ошибки если есть
        if (errorMessages.Any())
        {
            MessageBox.Show(
                "Ошибки при загрузке компонентов:\n\n" + string.Join("\n", errorMessages),
                "Ошибки загрузки",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }

        return result;
    }
}
