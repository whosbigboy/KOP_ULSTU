using EmployeeContract.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EmployeeContract.Data;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<OrganisationDbContext>
{
    public OrganisationDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<OrganisationDbContext>();
        var connectionString = configuration.GetConnectionString("OrganisationDb")
            ?? "Host=localhost;Port=5432;Database=organisation_db;Username=postgres;Password=admin123";

        optionsBuilder.UseNpgsql(connectionString);
        return new OrganisationDbContext(optionsBuilder.Options);
    }
}

