using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace EmployeeContract.Data;

public static class DbContextFactory
{
    private static IConfiguration? _configuration;

    public static void Initialize(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public static EmployeeDbContext CreateDbContext()
    {
        if (_configuration == null)
            throw new InvalidOperationException("DbContextFactory not initialized. Call Initialize() first.");

        var optionsBuilder = new DbContextOptionsBuilder<EmployeeDbContext>();
        var connectionString = _configuration.GetConnectionString("EmployeeDb")
            ?? throw new InvalidOperationException("Connection string 'EmployeeDb' not found.");

        optionsBuilder.UseNpgsql(connectionString);
        return new EmployeeDbContext(optionsBuilder.Options);
    }
}

