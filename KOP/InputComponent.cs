using KOP.Exceptions;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace KOP
{
    public partial class InputComponent : UserControl
    {
        public event Action? ChangeText;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string DateExample { get; private set; } = string.Empty;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DateTime? DateText
        {
            get
            {
                if (checkBox1.Checked) return null;

                if (!checkBox1.Checked && string.IsNullOrEmpty(textBox1.Text))
                {
                    throw new EmptyValueWithoutCheckBoxException();
                }

                string dateString = textBox1.Text;

                if (DateTime.TryParse(dateString, out DateTime date)) return date;
                else throw new IncorrectStringException();

            }
            set
            {
                if (value == null)
                {
                    checkBox1.Checked = true;
                    textBox1.Enabled = false;
                }
                else
                {
                    checkBox1.Checked = false;
                    textBox1.Enabled = true;

                    DateTime date = value.Value;
                    textBox1.Text = date.ToString("dd.MM.yyyy");
                }
            }
        }

        public InputComponent()
        {
            InitializeComponent();
            SetDateExample("12.12.2012");
            toolTip1.SetToolTip(textBox1, DateExample);
            textBox1.TextChanged += (object? sender, EventArgs e) => ChangeText?.Invoke();
            checkBox1.CheckedChanged += CheckBoxCheckedChanged;
        }

        private void CheckBoxCheckedChanged(object sender, EventArgs e) // событие на изменение чекбокса
        {
            if (checkBox1.Checked)
            {
                textBox1.Enabled = false;
            }
            else
            {
                textBox1.Enabled = true;
            }
            ChangeText?.Invoke();
        }

        public void SetDateExample(string example)
        {
            if (!string.IsNullOrEmpty(example)) DateExample = example;
        }
    }
}
