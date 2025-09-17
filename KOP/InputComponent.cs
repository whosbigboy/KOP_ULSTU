using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace KOP
{
    public partial class InputComponent : UserControl
    {
        private readonly string pattern = @"^(0[1-9]|[12][0-9]|3[01])\.(0[1-9]|1[0-2])\.(\d{4})$";

        public string? dateText
        {
            get
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    return null;
                }
                return textBox1.Text;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    textBox1.Text = string.Empty;
                }
                else
                {
                    if (ValidateInput(value))
                    {
                        textBox1.Text = value;
                    }
                }
            }
        }

        public InputComponent()
        {
            InitializeComponent();

            textBox1.Validating += textBox1_Validating;
            toolTip1.SetToolTip(textBox1, "Введите дату в формате DD.MM.YYYY или оставьте пустым");
        }

        private bool ValidateInput(string input) => Regex.IsMatch(input, pattern);

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(textBox1.Text) && !ValidateInput(textBox1.Text))
                {
                    throw new InputFormatException("Неверный формат даты. Введите дату в формате DD.MM.YYYY");
                }
            }
            catch (InputFormatException)
            {
                e.Cancel = true;
            }
        }
    }

    public class InputFormatException : Exception
    {
        public InputFormatException(string message) : base(message)
        {
            MessageBox.Show(
                message,
                "Ошибка ввода",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }
    }
}
