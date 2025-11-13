using EmployeeContract;
using System.ComponentModel;

namespace EmployeeForms;

public partial class LoginForm : Form
{
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public AccessLevel AccessLevel { get; private set; } = AccessLevel.Minimal;

    public LoginForm()
    {
        InitializeComponent();
        buttonSubmit.Click += (sender, e) => LoginForm_OnSubmit();
        FormClosing += LoginForm_FormClosing;
    }

    private void LoginForm_OnSubmit()
    {
        var role = textBoxLogin.Text?.Trim().ToLower();
        if (role == "admin")
        {
            AccessLevel = AccessLevel.Advanced;
        }
        else if (!string.IsNullOrWhiteSpace(role))
        {
            AccessLevel = AccessLevel.Basic;
        }
        else
        {
            AccessLevel = AccessLevel.Basic;
        }
        DialogResult = DialogResult.OK;
        Close();
    }

    private void LoginForm_FormClosing(object? sender, FormClosingEventArgs e)
    {
        if (DialogResult != DialogResult.OK)
        {
            AccessLevel = AccessLevel.Minimal;
        }
    }
}
