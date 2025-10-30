namespace EmployeeForm
{
    partial class LoginForm
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
            labelLogin = new Label();
            textBoxLogin = new TextBox();
            buttonSubmit = new Button();
            SuspendLayout();
            // 
            // labelLogin
            // 
            labelLogin.AutoSize = true;
            labelLogin.Location = new Point(81, 21);
            labelLogin.Name = "labelLogin";
            labelLogin.Size = new Size(142, 20);
            labelLogin.TabIndex = 0;
            labelLogin.Text = "Введите ваш логин";
            // 
            // textBoxLogin
            // 
            textBoxLogin.Location = new Point(12, 66);
            textBoxLogin.Name = "textBoxLogin";
            textBoxLogin.Size = new Size(287, 27);
            textBoxLogin.TabIndex = 1;
            // 
            // buttonSubmit
            // 
            buttonSubmit.Location = new Point(81, 114);
            buttonSubmit.Name = "buttonSubmit";
            buttonSubmit.Size = new Size(142, 39);
            buttonSubmit.TabIndex = 2;
            buttonSubmit.Text = "ОК";
            buttonSubmit.UseVisualStyleBackColor = true;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(311, 178);
            Controls.Add(buttonSubmit);
            Controls.Add(textBoxLogin);
            Controls.Add(labelLogin);
            Name = "LoginForm";
            Text = "LoginForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelLogin;
        private TextBox textBoxLogin;
        private Button buttonSubmit;
    }
}