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
        private readonly EmployeeDbContext _dbContext;

        private readonly DataGridView _dataGridView = new() { Dock = DockStyle.Fill };
        private readonly ContextMenuStrip _ctx = new();
        private readonly ToolStripMenuItem _miAdd = new("Добавить");
        private readonly ToolStripMenuItem _miEdit = new("Изменить");
        private readonly ToolStripMenuItem _miDelete = new("Удалить");
        private readonly ToolStripMenuItem _miRefresh = new("Обновить");

        public EmployeeDirectoryControl(IHostServices host)
        {
            InitializeComponent();
            _host = host;
            _dbContext = host.DbContext;

            InitializeDataGridView();
            Controls.Add(_dataGridView);

            _ctx.Items.AddRange([_miAdd, _miEdit, _miDelete, new ToolStripSeparator(), _miRefresh]);
            _miAdd.ShortcutKeys = Keys.Control | Keys.A;
            _miEdit.ShortcutKeys = Keys.Control | Keys.U;
            _miDelete.ShortcutKeys = Keys.Control | Keys.D;
            _miRefresh.ShortcutKeys = Keys.F5;
            _dataGridView.ContextMenuStrip = _ctx;

            _miAdd.Click += (_, __) => CreateNew();
            _miEdit.Click += (_, __) => EditSelected();
            _miDelete.Click += (_, __) => DeleteSelected();
            _miRefresh.Click += async (_, __) => await ReloadAsync();

            Load += async (_, __) => await ReloadAsync();
            KeyDown += OnKeyDownHandler;
        }

        private void InitializeDataGridView()
        {
            _dataGridView.AutoGenerateColumns = false;
            _dataGridView.AllowUserToAddRows = false;
            _dataGridView.AllowUserToDeleteRows = false;
            _dataGridView.ReadOnly = true;
            _dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _dataGridView.MultiSelect = false;
            _dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Настраиваем колонки
            _dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "OrgName",
                DataPropertyName = "OrgName",
                HeaderText = "Подразделение",
                FillWeight = 25
            });

            _dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                DataPropertyName = "Id",
                HeaderText = "Идентификатор",
                FillWeight = 20,
                Visible = false // Скрываем ID, но оставляем для доступа
            });

            _dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FIO",
                DataPropertyName = "FIO",
                HeaderText = "ФИО",
                FillWeight = 30
            });

            _dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "WorkExp",
                DataPropertyName = "WorkExp",
                HeaderText = "Стаж работы",
                FillWeight = 15
            });

            _dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Posts",
                DataPropertyName = "Posts",
                HeaderText = "Должность",
                FillWeight = 25
            });

            // Двойной клик для редактирования
            _dataGridView.CellDoubleClick += (_, __) => EditSelected();
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
            else if (e.KeyCode == Keys.F5)
            {
                _ = ReloadAsync();
                e.Handled = true;
            }
        }

        private async Task ReloadAsync()
        {
            var list = await _dbContext.Employees
                .Include(s => s.EmployeeOrg)
                .OrderBy(s => s.EmployeeOrg.Name)
                .ThenBy(s => s.WorkExp)
                .ThenBy(s => s.FIO)
                .ToListAsync();

            var rows = list.Select(s => new ListRow
            {
                OrgName = s.EmployeeOrg.Name,
                WorkExp = s.WorkExp.ToString(),
                FIO = s.FIO,
                Id = s.Id.ToString(),
                Posts = s.Posts ?? string.Empty
            }).ToList();

            _dataGridView.DataSource = rows;

            // Обновляем заголовки с количеством записей
            UpdateHeader();
        }

        private void UpdateHeader()
        {
            var count = _dataGridView.Rows.Count;
            this.FindForm().Text = $"Сотрудники - {count} записей";
        }

        private void CreateNew()
        {
            var model = new Employee { FIO = string.Empty };
            using var dlg = new EmployeeEditForm(_host, model);

            // Убедитесь, что используется ShowDialog()
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                if (string.IsNullOrWhiteSpace(model.FIO))
                {
                    MessageBox.Show("ФИО обязательно");
                    return;
                }
                if (model.EmployeeOrgId == Guid.Empty)
                {
                    MessageBox.Show("Нужно выбрать подразделение");
                    return;
                }

                _dbContext.Employees.Add(model);
                _dbContext.SaveChanges();
                _ = ReloadAsync();
            }
        }

        private async void EditSelected()
        {
            if (_dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите запись для редактирования");
                return;
            }

            var selectedRow = _dataGridView.SelectedRows[0];
            var idText = selectedRow.Cells["Id"].Value?.ToString();

            if (!Guid.TryParse(idText, out var id))
            {
                MessageBox.Show("Не удалось получить идентификатор записи");
                return;
            }

            var tracked = await _dbContext.Employees
                .Include(s => s.EmployeeOrg)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (tracked == null)
            {
                await ReloadAsync();
                return;
            }

            using var dlg = new EmployeeEditForm(_host, tracked);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                if (string.IsNullOrWhiteSpace(tracked.FIO)) { MessageBox.Show("ФИО обязательно"); return; }
                if (tracked.EmployeeOrgId == Guid.Empty) { MessageBox.Show("Нужно выбрать подразделение"); return; }

                await _dbContext.SaveChangesAsync();
                await ReloadAsync();
            }
        }

        private async void DeleteSelected()
        {
            if (_dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите запись для удаления");
                return;
            }

            if (MessageBox.Show("Удалить выбранную запись?", "Подтверждение", MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                return;
            }

            var selectedRow = _dataGridView.SelectedRows[0];
            var idText = selectedRow.Cells["Id"].Value?.ToString();

            if (!Guid.TryParse(idText, out var id))
            {
                MessageBox.Show("Не удалось получить идентификатор записи");
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

        private sealed class ListRow
        {
            public string OrgName { get; set; } = string.Empty;
            public string WorkExp { get; set; } = string.Empty;
            public string FIO { get; set; } = string.Empty;
            public string Id { get; set; } = string.Empty;
            public string Posts { get; set; } = string.Empty;
        }
    }
}