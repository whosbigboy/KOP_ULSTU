namespace WinFormsApp1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            inputComponent1 = new KOP.InputComponent();
            listComponent1 = new KOP.ListComponent();
            SuspendLayout();
            // 
            // inputComponent1
            // 
            inputComponent1.DateText = null;
            inputComponent1.Location = new Point(482, 118);
            inputComponent1.Name = "inputComponent1";
            inputComponent1.Size = new Size(279, 172);
            inputComponent1.TabIndex = 0;
            // 
            // listComponent1
            // 
            listComponent1.Location = new Point(39, 40);
            listComponent1.Name = "listComponent1";
            listComponent1.SelectedValue = "";
            listComponent1.Size = new Size(459, 379);
            listComponent1.TabIndex = 1;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(839, 510);
            Controls.Add(listComponent1);
            Controls.Add(inputComponent1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private KOP.ListComponent listComponent1;
        private KOP.InputComponent inputComponent1;
    }
}
