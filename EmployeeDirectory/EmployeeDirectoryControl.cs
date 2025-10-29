using ControlsLibraryNet90.Data;
using ControlsLibraryNet90.Models;
using EmployeeContract;
using EmployeeContract.Data;
using EmployeeContract.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDirectory
{
    public partial class EmployeeDirectoryControl : UserControl
    {
        private readonly IHostServices _host;
        private readonly OrganisationDbContext _dbContext;

        private readonly ControlDataTreeData _tree = new() { Dock = DockStyle.Fill };
        private readonly ContextMenuStrip _ctx = new();
        private readonly ToolStripMenuItem _miAdd = new("Добавить");
        private readonly ToolStripMenuItem _miEdit = new("Изменить");
        private readonly ToolStripMenuItem _miDelete = new("Удалить");

        public EmployeeDirectoryControl(IHostServices host)
        {
            InitializeComponent();
            _host = host;
            _dbContext = host.DbContext;

            Controls.Add(_tree);
            _ctx.Items.AddRange([_miAdd, _miEdit, _miDelete]);
            _miAdd.ShortcutKeys = Keys.Control | Keys.A;
            _miEdit.ShortcutKeys = Keys.Control | Keys.U;
            _miDelete.ShortcutKeys = Keys.Control | Keys.D;
            _tree.ContextMenuStrip = _ctx;

            _miAdd.Click += (_, __) => CreateNew();
            _miEdit.Click += (_, __) => EditSelected();
            _miDelete.Click += (_, __) => DeleteSelected();

            var config = new DataTreeNodeConfig
            {
                NodeNames = new Queue<string>(["TypeName", "ReportDate", "Name", "Id"]),
                UseProperites = true
            };
            var baseType = typeof(ControlDataTreeData).BaseType;
            var prop = baseType?.GetProperty("Levels", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            prop?.SetValue(_tree, config);

            Load += async (_, __) => await ReloadAsync();
            KeyDown += OnKeyDownHandler;
        }

        private void OnKeyDownHandler(object? sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                CreateNew();
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.U)
            {
                EditSelected();
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.D)
            {
                DeleteSelected();
                e.Handled = true;
            }
        }

        private async Task ReloadAsync()
        {
            var list = await _dbContext.Employees
                .Include(s => s.EmployeeType)
                .Include(s => s.Parent)
                .OrderBy(s => s.EmployeeType.Name)
                .ThenBy(s => s.ReportDate)
                .ThenBy(s => s.Name)
                .ToListAsync();

            var rows = list.Select(s => new TreeRow
            {
                TypeName = s.EmployeeType.Name,
                ReportDate = s.ReportDate?.ToString("yyyy-MM-dd") ?? "",
                Name = s.Name,
                Id = s.Id.ToString()
            }).ToList();

            var tv = GetInnerTreeView(_tree);
            if (tv is null)
            {
                return;
            }
            tv.BeginUpdate();
            tv.Nodes.Clear();
            if (rows.Count > 0)
            {
                _tree.AddData(rows);
            }
            tv.EndUpdate();
        }

        private static TreeView? GetInnerTreeView(ControlDataTreeData tree)
        {
            var baseType = typeof(ControlDataTreeData).BaseType;
            var fld = baseType?.GetField("treeView", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            return fld?.GetValue(tree) as TreeView;
        }

        private async void CreateNew()
        {
            var model = new Employee { Name = string.Empty };
            using var dlg = new EmployeeEditForm(_host, model);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                if (string.IsNullOrWhiteSpace(model.Name)) { MessageBox.Show("Наименование обязательно"); return; }
                if (model.EmployeeTypeId == Guid.Empty) { MessageBox.Show("Нужно выбрать тип"); return; }

                _dbContext.Employees.Add(model);
                await _dbContext.SaveChangesAsync();
                await ReloadAsync();
            }
        }

        private async void EditSelected()
        {
            var tv = GetInnerTreeView(_tree);
            if (tv == null || tv.SelectedNode == null)
            {
                MessageBox.Show("Выберите элемент дерева (Id)");
                return;
            }
            var idText = tv.SelectedNode.Text;
            if (!Guid.TryParse(idText, out var id))
            {
                MessageBox.Show("Выберите конечный узел с идентификатором");
                return;
            }
            var tracked = await _dbContext.Employees
                .Include(s => s.EmployeeType)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (tracked == null)
            {
                await ReloadAsync();
                return;
            }

            using var dlg = new EmployeeEditForm(_host, tracked);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                if (string.IsNullOrWhiteSpace(tracked.Name)) { MessageBox.Show("Наименование обязательно"); return; }
                if (tracked.EmployeeTypeId == Guid.Empty) { MessageBox.Show("Нужно выбрать тип"); return; }

                await _dbContext.SaveChangesAsync();
                await ReloadAsync();
            }
        }

        private async void DeleteSelected()
        {
            var tv = GetInnerTreeView(_tree);
            if (tv == null || tv.SelectedNode == null)
            {
                MessageBox.Show("Выберите элемент дерева (Id)");
                return;
            }
            if (MessageBox.Show("Удалить выбранную запись?", "Подтверждение", MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                return;
            }
            var idText = tv.SelectedNode.Text;
            if (!Guid.TryParse(idText, out var id))
            {
                MessageBox.Show("Выберите конечный узел с идентификатором");
                return;
            }

            var entity = await _dbContext.Employees.FindAsync(id);
            if (entity != null)
            {
                _dbContext.Employees.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }

            await ReloadAsync();
        }

        private sealed class TreeRow
        {
            public string TypeName { get; set; } = string.Empty;
            public string ReportDate { get; set; } = string.Empty;
            public string Name { get; set; } = string.Empty;
            public string Id { get; set; } = string.Empty;
        }
    }
}
