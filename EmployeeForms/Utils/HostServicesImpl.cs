using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using EmployeeContract;
using EmployeeContract.Data;
using EmployeeForm.Licensing;

namespace EmployeeForm.Utils;

internal class HostServicesImpl : IHostServices
{
    private readonly ILicenseProvider _licenseProvider;
    private readonly OrganisationDbContext _dbContext;

    public HostServicesImpl(AccessLevel currentAccessLevel, IConfiguration configuration)
    {
        var optionsBuilder = new DbContextOptionsBuilder<OrganisationDbContext>();
        var connectionString = configuration.GetConnectionString("OrganisationDb");
        optionsBuilder.UseNpgsql(connectionString);

        _dbContext = new OrganisationDbContext(optionsBuilder.Options);
    }

    public ILicenseProvider License => _licenseProvider;

    public OrganisationDbContext DbContext => _dbContext;

    public object? GetService(Type serviceType)
    {
        if (serviceType == typeof(ILicenseProvider))
            return _licenseProvider;
        if (serviceType == typeof(OrganisationDbContext))
            return _dbContext;
        return null;
    }

    public T? GetService<T>() where T : class
    {
        var service = GetService(typeof(T));
        return service as T;
    }
}
