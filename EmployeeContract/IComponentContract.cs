
namespace EmployeeContract;

public interface IComponentContract
{
    IComponentMetadata Metadata { get; }
    UserControl CreateControl(IHostServices host);
}
