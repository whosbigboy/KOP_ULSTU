using EmployeeContract;

namespace EmployeeForms
{
    public partial class EmployeeForm : Form
    {
        private readonly Dictionary<string, IComponentContract> _directories;
        private readonly Dictionary<string, UserControl> _controls = new();
        private readonly IHostServices _host;
        private Panel _activePanel;

        public EmployeeForm(Dictionary<string, IComponentContract> components, IHostServices host)
        {
            _directories = components;
            _host = host;
            InitializeComponent();
            _activePanel = new Panel { Dock = DockStyle.Fill };
            Controls.Add(_activePanel);
            try
            {
                PopulateMenu(directoriesToolStripMenuItem.DropDownItems, ComponentType.Directory);
                PopulateMenu(reportsToolStripMenuItem.DropDownItems, ComponentType.Report);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка при загрузке компонент", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateMenu(ToolStripItemCollection menuItems, ComponentType type)
        {
            var components = _directories.Values.Where(c => c.Metadata.ComponentType == type);
            foreach (var component in components)
            {
                var menuItem = new ToolStripMenuItem { Text = component.Metadata.Title };
                menuItem.Click += (sender, e) => ShowComponent(component.Metadata.Title, type == ComponentType.Directory
                    ? component.CreateControl(_host)
                    : _controls.GetValueOrDefault(component.Metadata.Id) ?? component.CreateControl(_host));
                if (type == ComponentType.Report)
                {
                    _controls.TryAdd(component.Metadata.Id, component.CreateControl(_host));
                }
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
}
