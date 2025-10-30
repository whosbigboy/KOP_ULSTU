using Microsoft.Extensions.Configuration;
using EmployeeContract;
using EmployeeForm.Composition;
using EmployeeForm.Licensing;
using EmployeeForm.Utils;

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
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();

        var builder = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        Configuration = builder.Build();

        var path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", Configuration["PluginsPath"] ?? "Plugins"));

        var licensePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", Configuration["License"] ?? "License"));
        var licenseProvider = new LicenseProvider(licensePath);
        if (!licenseProvider.IsExpired)
        {
            CurrentAccessLevel = licenseProvider.CurrentLevel;
        }

        var loader = new ComponentLoader(path, CurrentAccessLevel);
        var components = loader.LoadAll().ToDictionary(c => c.Metadata.Title, c => c);
        var host = new HostServicesImpl(CurrentAccessLevel, Configuration);

        Application.Run(new EmployeeForm(components, host));
    }
}