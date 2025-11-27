using EmployeeContract;
using EmployeeForms.Utils;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace EmployeeForms;

internal static class Program
{
    public static IConfiguration Configuration { get; private set; }
    public static AccessLevel CurrentAccessLevel { get; private set; } = AccessLevel.Minimal;

    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        var builder = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        Configuration = builder.Build();

        // Путь к лицензии
        var licensePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "license.json");

        var licenseProvider = new LicenseProvider(licensePath);
        if (!licenseProvider.IsExpired)
        {
            CurrentAccessLevel = licenseProvider.CurrentLevel;
        }

        // Путь к плагинам
        var pluginsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Configuration["PluginsPath"] ?? "Plugins");

        // Проверяем наличие DLL файлов
        var dllFiles = Directory.GetFiles(pluginsPath, "*.dll");
        foreach (var dll in dllFiles)
        {
            Debug.WriteLine($"  - {Path.GetFileName(dll)}");
        }

        var loader = new ComponentLoader(pluginsPath, CurrentAccessLevel);
        var components = loader.LoadAll().ToList();

        var host = new HostServicesImpl(CurrentAccessLevel, Configuration, licensePath);
        Application.Run(new EmployeeForm(components.ToDictionary(c => c.Metadata.Title, c => c), host));
    }
}
