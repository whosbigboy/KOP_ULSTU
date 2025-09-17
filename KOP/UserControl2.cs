using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace KOP
{
    public partial class UserControl2 : UserControl
    {
        public event EventHandler TextChanged;

        private string pattern = @"^(0[1-9]|[12][0-9]|3[01])\.(0[1-9]|1[0-2])\.(\d{4})$";

        public string? dateText
        {
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    textBox1.Text = string.Empty;
                }
                else
                {
                    if (ValidateInput(value))
                        textBox1.Text = value;
                    else
                        throw new InvalidDateException("Текст не соответствует шаблону.");
                }
            }
            get
            {
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    return null;
                }

                if (ValidateInput(textBox1.Text))
                    return textBox1.Text;

                throw new InvalidDateException("Значение не соответствует формату даты DD.MM.YYYY.");
            }
        }

        public bool ValidateInput(string? input)
        {
            if (string.IsNullOrEmpty(input)) return true;
            return Regex.IsMatch(input, pattern);
        }

        public class InvalidDateException : Exception
        {
            public InvalidDateException(string message) : base(message) { }
        }

        public UserControl2()
        {
            InitializeComponent();

            textBox1.TextChanged += textBox1_TextChanged;
            textBox1.Validating += textBox1_Validating;

            toolTip1.SetToolTip(textBox1, "Введите дату в формате DD.MM.YYYY или оставьте пустым");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            TextChanged?.Invoke(this, EventArgs.Empty);
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text) && !ValidateInput(textBox1.Text))
            {
                MessageBox.Show(
                    "Неверный формат даты. Введите дату в формате DD.MM.YYYY",
                    "Ошибка ввода",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}
