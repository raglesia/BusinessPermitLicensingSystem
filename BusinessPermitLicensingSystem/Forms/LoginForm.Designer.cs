namespace BusinessPermitLicensingSystem.Forms
{
    partial class LogInForm
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
            btnLogIn = new Button();
            btnExit = new Button();
            btnCreate = new Button();
            txtPass = new TextBox();
            txtUser = new TextBox();
            label1 = new Label();
            label2 = new Label();
            SuspendLayout();
            // 
            // btnLogIn
            // 
            btnLogIn.Location = new Point(258, 237);
            btnLogIn.Name = "btnLogIn";
            btnLogIn.Size = new Size(192, 53);
            btnLogIn.TabIndex = 3;
            btnLogIn.Text = "Log In";
            btnLogIn.UseVisualStyleBackColor = true;
            btnLogIn.Click += btnLogIn_Click;
            // 
            // btnExit
            // 
            btnExit.Location = new Point(465, 237);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(192, 53);
            btnExit.TabIndex = 4;
            btnExit.Text = "Exit";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // btnCreate
            // 
            btnCreate.Location = new Point(369, 296);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(192, 53);
            btnCreate.TabIndex = 5;
            btnCreate.Text = "Create Account";
            btnCreate.UseVisualStyleBackColor = true;
            btnCreate.Click += btnCreate_Click_1;
            // 
            // txtPass
            // 
            txtPass.Location = new Point(369, 186);
            txtPass.Name = "txtPass";
            txtPass.Size = new Size(288, 31);
            txtPass.TabIndex = 2;
            txtPass.UseSystemPasswordChar = true;
            // 
            // txtUser
            // 
            txtUser.Location = new Point(369, 129);
            txtUser.Name = "txtUser";
            txtUser.Size = new Size(288, 31);
            txtUser.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(258, 129);
            label1.Name = "label1";
            label1.Size = new Size(100, 25);
            label1.TabIndex = 5;
            label1.Text = "Username: ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(258, 189);
            label2.Name = "label2";
            label2.Size = new Size(96, 25);
            label2.TabIndex = 6;
            label2.Text = "Password: ";
            // 
            // LogInForm
            // 
            AcceptButton = btnLogIn;
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(896, 488);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtUser);
            Controls.Add(txtPass);
            Controls.Add(btnCreate);
            Controls.Add(btnExit);
            Controls.Add(btnLogIn);
            Name = "LogInForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Masinloc - BPLS";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnLogIn;
        private Button btnExit;
        private Button btnCreate;
        private TextBox txtPass;
        private TextBox txtUser;
        private Label label1;
        private Label label2;
    }
}