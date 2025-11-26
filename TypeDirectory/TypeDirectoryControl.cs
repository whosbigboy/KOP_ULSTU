using Microsoft.EntityFrameworkCore;
using EmployeeContract;
using EmployeeContract.Data;
using EmployeeContract.Entities;
using System.ComponentModel;

namespace TypeDirectory;

public partial class TypeDirectoryControl : UserControl
{
    private readonly IHostServices _host;
    private readonly EmployeeDbContext _dbContext;
    private readonly BindingSource _bindingSource = new BindingSource();

    public TypeDirectoryControl(IHostServices host)
    {
        InitializeComponent();
        _host = host;
        _dbContext = host.DbContext;

        Load += async (_, __) => await LoadDataAsync();
        dataGridViewCustom.DataSource = _bindingSource;

        dataGridViewCustom.KeyDown += DataGridViewCustom_OnKeyDown;
        dataGridViewCustom.CellEndEdit += DataGridView_CellEndEdit;
    }

    private async void DataGridViewCustom_OnKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Insert)
        {
            // Создаём новый объект с временным именем
            var newOrg = new EmployeeOrg { Id = Guid.NewGuid(), Name = "Новая запись" };
            _bindingSource.Add(newOrg);

            BeginInvoke((Delegate)(() =>
            {
                if (dataGridViewCustom.Rows.Count > 0)
                {
                    dataGridViewCustom.CurrentCell = dataGridViewCustom.Rows[^1].Cells["NameColumn"];
                    dataGridViewCustom.BeginEdit(true);
                }
            }));

            e.Handled = true;
        }
        else if (e.KeyCode == Keys.Delete && dataGridViewCustom.CurrentRow is not null)
        {
            if (MessageBox.Show("Удалить выбранную запись?", "Подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (_bindingSource.Current is EmployeeOrg current)
                {
                    // Проверяем, существует ли объект в базе
                    var entityInDb = await _dbContext.EmployeeOrgs.FirstOrDefaultAsync(x => x.Id == current.Id);
                    if (entityInDb != null)
                    {
                        _dbContext.EmployeeOrgs.Remove(entityInDb);
                        await _dbContext.SaveChangesAsync();
                    }

                    _bindingSource.RemoveCurrent();
                }
            }
            e.Handled = true;
        }
    }

    private async void DataGridView_CellEndEdit(object? sender, DataGridViewCellEventArgs e)
    {
        if (dataGridViewCustom.Rows[e.RowIndex].DataBoundItem is not EmployeeOrg subDiv)
            return;

        if (string.IsNullOrWhiteSpace(subDiv.Name))
        {
            dataGridViewCustom.Rows[e.RowIndex].ErrorText = "Пустая строка не допускается";
            dataGridViewCustom.CancelEdit();
            return;
        }

        dataGridViewCustom.Rows[e.RowIndex].ErrorText = null;

        try
        {
            if (_dbContext.Entry(subDiv).State == EntityState.Detached)
            {
                _dbContext.EmployeeOrgs.Add(subDiv);
            }

            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async Task LoadDataAsync()
    {
        try
        {
            var orgs = await _dbContext.EmployeeOrgs.ToListAsync();
            _bindingSource.DataSource = new BindingList<EmployeeOrg>(orgs);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
