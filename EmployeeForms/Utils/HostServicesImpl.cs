using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using EmployeeContract;
using EmployeeContract.Data;

namespace EmployeeForms.Utils;

internal class HostServicesImpl : IHostServices
{
    private readonly ILicenseProvider _licenseProvider;
    private readonly EmployeeDbContext _dbContext;

    // ДОБАВЛЯЕМ ПАРАМЕТР licenseFilePath
    public HostServicesImpl(AccessLevel currentAccessLevel, IConfiguration configuration, string licenseFilePath)
    {
        // Инициализируем LicenseProvider с правильным путем
        _licenseProvider = new LicenseProvider(licenseFilePath);

        // Исправляем опечатку в connection string
        var optionsBuilder = new DbContextOptionsBuilder<EmployeeDbContext>();
        var connectionString = configuration.GetConnectionString("EmployeeDb"); // Исправлено "EmployeenDb" -> "EmployeeDb"

        if (string.IsNullOrEmpty(connectionString))
        {
            // Fallback на прямое указание строки подключения
            connectionString = configuration.GetSection("Database:ConnectionString").Value
                ?? "Host=localhost;Port=5432;Database=employee_db;Username=postgres;Password=postgres";
        }

        optionsBuilder.UseNpgsql(connectionString);
        _dbContext = new EmployeeDbContext(optionsBuilder.Options);
    }

    public ILicenseProvider License => _licenseProvider;

    public EmployeeDbContext DbContext => _dbContext;

    public object? GetService(Type serviceType)
    {
        if (serviceType == typeof(ILicenseProvider))
            return _licenseProvider;
        if (serviceType == typeof(EmployeeDbContext))
            return _dbContext;
        return null;
    }

    public T? GetService<T>() where T : class
    {
        var service = GetService(typeof(T));
        return service as T;
    }
}
