using EmployeeContract;

namespace EmployeeForms;
public partial class EmployeeForm : Form
{
    private readonly Dictionary<string, IComponentContract> _components;
    private readonly IHostServices _host;

    public EmployeeForm(Dictionary<string, IComponentContract> components, IHostServices host)
    {
        _components = components;
        _host = host;
        InitializeComponent();

        try
        {
            PopulateMenu(directoriesToolStripMenuItem.DropDownItems, ComponentType.Directory);
            PopulateMenu(reportsToolStripMenuItem.DropDownItems, ComponentType.Report);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при создании меню: {ex.Message}", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void PopulateMenu(ToolStripItemCollection menuItems, ComponentType type)
    {
        var components = _components.Values.Where(c => c.Metadata.ComponentType == type).ToList();

        foreach (var component in components)
        {
            var menuItem = new ToolStripMenuItem { Text = component.Metadata.Title };
            menuItem.Click += (sender, e) =>
            {
                try
                {
                    var control = component.CreateControl(_host);
                    ShowComponent(component.Metadata.Title, control);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка создания компонента: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            menuItems.Add(menuItem);
        }
    }

    private void ShowComponent(string title, UserControl control)
    {
        panel1.Controls.Clear();
        control.Dock = DockStyle.Fill;
        panel1.Controls.Add(control);
        Text = $"Учет сотрудников - {title}";
    }
}