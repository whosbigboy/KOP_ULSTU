
namespace EmployeeContract;

public interface ILicenseProvider
{
    AccessLevel CurrentLevel { get; }
    bool IsExpired { get; }
}
