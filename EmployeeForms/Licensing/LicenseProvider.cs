using System.Text.Json;
using EmployeeContract;

namespace EmployeeForms.Licensing;

internal sealed class LicenseProvider : ILicenseProvider
{
    private readonly AccessLevel _currentAccessLevel;
    private readonly DateTime _expires;

    public AccessLevel CurrentLevel => _currentAccessLevel;
    public bool IsExpired => DateTime.Now > _expires;

    public LicenseProvider(string licenseFilePath)
    {
        if (!File.Exists(licenseFilePath))
            throw new FileNotFoundException("Файл лицензии не найден.", licenseFilePath);

        var json = File.ReadAllText(licenseFilePath);
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
    }
}
