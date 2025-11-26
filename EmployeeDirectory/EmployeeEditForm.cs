using EmployeeContract;
using EmployeeContract.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeDirectory;

public partial class EmployeeEditForm : Form
{
    private readonly IHostServices _host;
    private readonly Employee _model;
    private bool _dirty;

    public EmployeeEditForm(IHostServices host, Employee model)
    {
        InitializeComponent();
        _host = host;
        _model = model;

        Load += async (_, __) => await InitAsync();
        FormClosing += OnFormClosing;
    }

    private async Task InitAsync()
    {
        var types = await _host.DbContext.EmployeeOrgs.ToListAsync() ?? [];

        if (comboBoxOrg != null)
        {
            comboBoxOrg.DisplayMember = nameof(EmployeeOrg.Name);
            comboBoxOrg.ValueMember = nameof(EmployeeOrg.Id);
            comboBoxOrg.DataSource = types;
            comboBoxOrg.DropDownStyle = ComboBoxStyle.DropDownList;

            // ИСПРАВЛЕННАЯ ЛОГИКА ВЫБОРА:
            var match = types.FirstOrDefault(t => t.Id == _model.EmployeeOrgId);
            if (match != null)
            {
                comboBoxOrg.SelectedItem = match; // Используем SelectedItem вместо поиска по индексу
            }
            else
            {
                comboBoxOrg.SelectedIndex = -1;
            }
        }


        if (textBoxFIO != null)
            textBoxFIO.Text = _model.FIO;
        if (textBoxPosts != null)
            textBoxPosts.Text = _model.Posts ?? string.Empty;
        if (numericUpDownWorkExp != null && _model.WorkExp > 0
            && _model.WorkExp <= 30)
        {
            numericUpDownWorkExp.Value = _model.WorkExp;
        }

        if (textBoxFIO != null)
            textBoxFIO.TextChanged += (_, __) => _dirty = true;
        if (textBoxPosts != null)
            textBoxPosts.TextChanged += (_, __) => _dirty = true;
        if (comboBoxOrg != null)
            comboBoxOrg.SelectedIndexChanged += (_, __) => _dirty = true;
        if (numericUpDownWorkExp != null)
            numericUpDownWorkExp.ValueChanged += (_, __) => _dirty = true;
    }

    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);
        AcceptButton = Controls.OfType<Button>().FirstOrDefault(b => b.DialogResult == DialogResult.OK);
        CancelButton = Controls.OfType<Button>().FirstOrDefault(b => b.DialogResult == DialogResult.Cancel);
    }

    private void OnFormClosing(object? sender, FormClosingEventArgs e)
    {
        if (DialogResult == DialogResult.OK)
        {
            // Перенос значений в модель
            if (textBoxFIO != null)
                _model.FIO = textBoxFIO.Text.Trim();

            if (textBoxPosts != null)
                _model.Posts = string.IsNullOrWhiteSpace(textBoxPosts.Text) ? null
                    : textBoxPosts.Text.Trim();

            if (comboBoxOrg != null)
            {
                // ИСПРАВЛЕННАЯ ЛОГИКА:
                if (comboBoxOrg.SelectedItem is EmployeeOrg selectedOrg)
                {
                    _model.EmployeeOrgId = selectedOrg.Id;
                }
                else
                {
                    MessageBox.Show("Нужно выбрать подразделение", "Валидация",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                    return;
                }
            }

            // Также исправляем логику для WorkExp
            if (numericUpDownWorkExp != null)
            {
                _model.WorkExp = (int)numericUpDownWorkExp.Value;
            }

            return;
        }

        if (_dirty)
        {
            var res = MessageBox.Show(
                "Есть несохранённые изменения. Закрыть без сохранения?",
                "Подтверждение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (res != DialogResult.Yes)
            {
                e.Cancel = true;
            }
        }
    }
}
