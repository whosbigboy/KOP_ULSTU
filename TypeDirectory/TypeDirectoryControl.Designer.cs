namespace TypeDirectory
{
    partial class TypeDirectoryControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dataGridViewCustom = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridViewCustom).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewCustom
            // 
            dataGridViewCustom.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCustom.Dock = DockStyle.Fill;
            dataGridViewCustom.Location = new Point(0, 0);
            dataGridViewCustom.Name = "dataGridViewCustom";
            dataGridViewCustom.RowHeadersWidth = 51;
            dataGridViewCustom.Size = new Size(800, 450);
            dataGridViewCustom.TabIndex = 0;
            // 
            // TypeDirectoryControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(dataGridViewCustom);
            Name = "TypeDirectoryControl";
            Size = new Size(800, 450);
            ((System.ComponentModel.ISupportInitialize)dataGridViewCustom).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridViewCustom;
    }
}
