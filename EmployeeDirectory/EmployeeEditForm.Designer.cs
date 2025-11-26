namespace EmployeeDirectory
{
    partial class EmployeeEditForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            labelFIO = new Label();
            labelPosts = new Label();
            labelOrg = new Label();
            labelWorkExp = new Label();
            textBoxFIO = new TextBox();
            textBoxPosts = new TextBox();
            buttonOk = new Button();
            buttonCancel = new Button();
            comboBoxOrg = new ComboBox();
            numericUpDownWorkExp = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)numericUpDownWorkExp).BeginInit();
            SuspendLayout();
            // 
            // labelFIO
            // 
            labelFIO.AutoSize = true;
            labelFIO.Location = new Point(39, 49);
            labelFIO.Name = "labelFIO";
            labelFIO.Size = new Size(31, 20);
            labelFIO.TabIndex = 0;
            labelFIO.Text = "FIO";
            // 
            // labelPosts
            // 
            labelPosts.AutoSize = true;
            labelPosts.Location = new Point(39, 103);
            labelPosts.Name = "labelPosts";
            labelPosts.Size = new Size(42, 20);
            labelPosts.TabIndex = 1;
            labelPosts.Text = "Posts";
            // 
            // labelOrg
            // 
            labelOrg.AutoSize = true;
            labelOrg.Location = new Point(39, 156);
            labelOrg.Name = "labelOrg";
            labelOrg.Size = new Size(34, 20);
            labelOrg.TabIndex = 2;
            labelOrg.Text = "Org";
            // 
            // labelWorkExp
            // 
            labelWorkExp.AutoSize = true;
            labelWorkExp.Location = new Point(39, 219);
            labelWorkExp.Name = "labelWorkExp";
            labelWorkExp.Size = new Size(67, 20);
            labelWorkExp.TabIndex = 3;
            labelWorkExp.Text = "WorkExp";
            // 
            // textBoxFIO
            // 
            textBoxFIO.Location = new Point(127, 49);
            textBoxFIO.Name = "textBoxFIO";
            textBoxFIO.Size = new Size(326, 27);
            textBoxFIO.TabIndex = 4;
            // 
            // textBoxPosts
            // 
            textBoxPosts.Location = new Point(127, 103);
            textBoxPosts.Name = "textBoxPosts";
            textBoxPosts.Size = new Size(326, 27);
            textBoxPosts.TabIndex = 5;
            // 
            // buttonOk
            // 
            buttonOk.Location = new Point(247, 266);
            buttonOk.Name = "buttonOk";
            buttonOk.Size = new Size(94, 29);
            buttonOk.TabIndex = 8;
            buttonOk.Text = "OK";
            buttonOk.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            buttonCancel.Location = new Point(359, 266);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(94, 29);
            buttonCancel.TabIndex = 9;
            buttonCancel.Text = "Отмена";
            buttonCancel.UseVisualStyleBackColor = true;
            // 
            // comboBoxOrg
            // 
            comboBoxOrg.FormattingEnabled = true;
            comboBoxOrg.Location = new Point(127, 156);
            comboBoxOrg.Name = "comboBoxOrg";
            comboBoxOrg.Size = new Size(326, 28);
            comboBoxOrg.TabIndex = 10;
            // 
            // numericUpDownWorkExp
            // 
            numericUpDownWorkExp.Location = new Point(127, 212);
            numericUpDownWorkExp.Maximum = new decimal(new int[] { 30, 0, 0, 0 });
            numericUpDownWorkExp.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownWorkExp.Name = "numericUpDownWorkExp";
            numericUpDownWorkExp.Size = new Size(326, 27);
            numericUpDownWorkExp.TabIndex = 11;
            numericUpDownWorkExp.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // EmployeeEditForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(488, 323);
            Controls.Add(numericUpDownWorkExp);
            Controls.Add(comboBoxOrg);
            Controls.Add(buttonCancel);
            Controls.Add(buttonOk);
            Controls.Add(textBoxPosts);
            Controls.Add(textBoxFIO);
            Controls.Add(labelWorkExp);
            Controls.Add(labelOrg);
            Controls.Add(labelPosts);
            Controls.Add(labelFIO);
            Name = "EmployeeEditForm";
            Text = "EmployeeEditForm";
            ((System.ComponentModel.ISupportInitialize)numericUpDownWorkExp).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelFIO;
        private Label labelPosts;
        private Label labelOrg;
        private Label labelWorkExp;
        private TextBox textBoxFIO;
        private TextBox textBoxPosts;
        private Button buttonOk;
        private Button buttonCancel;
        private ComboBox comboBoxOrg;
        private NumericUpDown numericUpDownWorkExp;
    }
}