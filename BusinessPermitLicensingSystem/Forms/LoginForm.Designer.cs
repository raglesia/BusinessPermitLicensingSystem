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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogInForm));
            txtPass = new TextBox();
            btnCreate = new Button();
            txtUser = new TextBox();
            btnExit = new Button();
            btnLogIn = new Button();
            label2 = new Label();
            label1 = new Label();
            panel1 = new Panel();
            toolTip1 = new ToolTip(components);
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // txtPass
            // 
            txtPass.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPass.Location = new Point(152, 77);
            txtPass.Name = "txtPass";
            txtPass.Size = new Size(288, 39);
            txtPass.TabIndex = 2;
            txtPass.UseSystemPasswordChar = true;
            // 
            // btnCreate
            // 
            btnCreate.BackColor = SystemColors.GradientActiveCaption;
            btnCreate.BackgroundImage = Properties.Resources.icons8_create_64;
            btnCreate.BackgroundImageLayout = ImageLayout.Center;
            btnCreate.ImageAlign = ContentAlignment.MiddleLeft;
            btnCreate.Location = new Point(128, 211);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(201, 73);
            btnCreate.TabIndex = 5;
            toolTip1.SetToolTip(btnCreate, "Create Account");
            btnCreate.UseVisualStyleBackColor = false;
            btnCreate.Click += btnCreate_Click_1;
            // 
            // txtUser
            // 
            txtUser.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtUser.Location = new Point(152, 24);
            txtUser.Name = "txtUser";
            txtUser.Size = new Size(288, 39);
            txtUser.TabIndex = 1;
            // 
            // btnExit
            // 
            btnExit.BackColor = SystemColors.GradientActiveCaption;
            btnExit.BackgroundImage = Properties.Resources.icons8_exit_64__1_;
            btnExit.BackgroundImageLayout = ImageLayout.Center;
            btnExit.ImageAlign = ContentAlignment.MiddleLeft;
            btnExit.Location = new Point(239, 132);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(201, 73);
            btnExit.TabIndex = 4;
            toolTip1.SetToolTip(btnExit, "Exit");
            btnExit.UseVisualStyleBackColor = false;
            btnExit.Click += btnExit_Click;
            // 
            // btnLogIn
            // 
            btnLogIn.BackColor = SystemColors.GradientInactiveCaption;
            btnLogIn.BackgroundImage = Properties.Resources.icons8_log_in_64;
            btnLogIn.BackgroundImageLayout = ImageLayout.Center;
            btnLogIn.ImageAlign = ContentAlignment.MiddleLeft;
            btnLogIn.Location = new Point(13, 132);
            btnLogIn.Name = "btnLogIn";
            btnLogIn.Size = new Size(201, 73);
            btnLogIn.TabIndex = 3;
            toolTip1.SetToolTip(btnLogIn, "Log In");
            btnLogIn.UseVisualStyleBackColor = false;
            btnLogIn.Click += btnLogIn_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(13, 84);
            label2.Name = "label2";
            label2.Size = new Size(123, 32);
            label2.TabIndex = 6;
            label2.Text = "Password: ";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(13, 31);
            label1.Name = "label1";
            label1.Size = new Size(133, 32);
            label1.TabIndex = 5;
            label1.Text = "Username: ";
            // 
            // panel1
            // 
            panel1.Controls.Add(txtUser);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(btnCreate);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(btnExit);
            panel1.Controls.Add(txtPass);
            panel1.Controls.Add(btnLogIn);
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(484, 301);
            panel1.TabIndex = 7;
            panel1.Paint += panel1_Paint;
            // 
            // toolTip1
            // 
            toolTip1.AutoPopDelay = 3000;
            toolTip1.InitialDelay = 500;
            toolTip1.IsBalloon = true;
            toolTip1.ReshowDelay = 200;
            // 
            // LogInForm
            // 
            AcceptButton = btnLogIn;
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientActiveCaption;
            ClientSize = new Size(508, 319);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "LogInForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Masinloc - BPLS";
            Load += LogInForm_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TextBox txtPass;
        private Button btnCreate;
        private TextBox txtUser;
        private Button btnExit;
        private Button btnLogIn;
        private Label label2;
        private Label label1;
        private Panel panel1;
        private ToolTip toolTip1;
    }
}