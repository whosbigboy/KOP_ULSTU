using EmployeeContract.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EmployeeContract.Data;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<EmployeeDbContext>
{
    public EmployeeDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<EmployeeDbContext>();
        var connectionString = configuration.GetConnectionString("EmployeeDb")
            ?? "Host=localhost;Port=5432;Database=employee_db;Username=postgres;Password=postgres";

        optionsBuilder.UseNpgsql(connectionString);
        return new EmployeeDbContext(optionsBuilder.Options);
    }
}

