namespace KOP
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
            listBox1.SelectedIndexChanged += ListBox1_SelectedIndexChanged;
        }

        public ListBox.ObjectCollection Items => listBox1.Items;

        public void Clear()
        {
            listBox1?.Items.Clear();
        }

        public string SelectedValue
        {
            get
            {
                return listBox1.SelectedItem?.ToString() ?? string.Empty;
            }
            set
            {
                if (listBox1.Items.Contains(value))
                {
                    listBox1.SelectedItem = value;
                }
            }
        }

        public event EventHandler SelectedValueChanged;

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedValueChanged?.Invoke(this, EventArgs.Empty);
        }

    }
}
