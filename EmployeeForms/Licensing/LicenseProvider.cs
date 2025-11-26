using EmployeeContract;
using System.Text.Json;

internal sealed class LicenseProvider : ILicenseProvider
{
    private readonly AccessLevel _currentAccessLevel;
    private readonly DateTime _expires;

    public AccessLevel CurrentLevel => _currentAccessLevel;
    public bool IsExpired => DateTime.Now > _expires;

    public LicenseProvider(string licenseFilePath)
    {
        // ДОБАВЛЯЕМ ДЕТАЛЬНУЮ ИНФОРМАЦИЮ О ПУТИ
        var fullPath = Path.GetFullPath(licenseFilePath);
        Console.WriteLine($"Поиск файла лицензии по пути: {fullPath}");

        if (!File.Exists(fullPath))
        {
            // Проверяем альтернативные расположения
            var alternativePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "license.json");
            Console.WriteLine($"Альтернативный путь: {alternativePath}");

            if (File.Exists(alternativePath))
            {
                fullPath = alternativePath;
                Console.WriteLine($"Файл лицензии найден по альтернативному пути: {fullPath}");
            }
            else
            {
                throw new FileNotFoundException($"Файл лицензии не найден. Искали по пути: {fullPath}", licenseFilePath);
            }
        }

        var json = File.ReadAllText(fullPath);
        var doc = JsonDocument.Parse(json).RootElement;

        var role = doc.GetProperty("role").GetString() ?? "none";
        var expiresStr = doc.GetProperty("expires").GetString() ?? DateTime.MinValue.ToString();
        if (!DateTime.TryParse(expiresStr, out _expires))
            _expires = DateTime.MinValue;

        _currentAccessLevel = role.ToLowerInvariant() switch
        {
            "admin" => AccessLevel.Advanced,
            "user" => AccessLevel.Basic,
            _ => AccessLevel.Minimal
        };

        Console.WriteLine($"Лицензия загружена: Role={role}, AccessLevel={_currentAccessLevel}, Expires={_expires}");
    }
}