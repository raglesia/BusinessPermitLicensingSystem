namespace BusinessPermitLicensingSystem.Forms
{
    partial class DashboardForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DashboardForm));
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            lblUsername = new Label();
            lblPenaltyNotice = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            lblDateTime = new Label();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = SystemColors.GradientActiveCaption;
            button1.BackgroundImageLayout = ImageLayout.None;
            button1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.Image = Properties.Resources.icons8_list_64;
            button1.ImageAlign = ContentAlignment.MiddleLeft;
            button1.Location = new Point(12, 15);
            button1.Name = "button1";
            button1.Size = new Size(379, 80);
            button1.TabIndex = 0;
            button1.Text = "Stall Owners";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = SystemColors.GradientActiveCaption;
            button2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            button2.ForeColor = Color.Black;
            button2.Image = Properties.Resources.icons8_archive_64;
            button2.ImageAlign = ContentAlignment.MiddleLeft;
            button2.Location = new Point(397, 101);
            button2.Name = "button2";
            button2.Size = new Size(378, 80);
            button2.TabIndex = 3;
            button2.Text = "    Archived Records";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.BackColor = SystemColors.GradientActiveCaption;
            button3.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            button3.Image = Properties.Resources.icons8_settings_64__1_;
            button3.ImageAlign = ContentAlignment.MiddleLeft;
            button3.Location = new Point(12, 101);
            button3.Name = "button3";
            button3.Size = new Size(379, 80);
            button3.TabIndex = 2;
            button3.Text = "Stall Rates";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.BackColor = SystemColors.GradientActiveCaption;
            button4.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            button4.ForeColor = Color.Black;
            button4.Image = Properties.Resources.icons8_conference_64;
            button4.ImageAlign = ContentAlignment.MiddleLeft;
            button4.Location = new Point(397, 15);
            button4.Name = "button4";
            button4.Size = new Size(379, 80);
            button4.TabIndex = 1;
            button4.Text = "Profiling";
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.BackColor = SystemColors.GradientActiveCaption;
            button5.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            button5.Image = Properties.Resources.icons8_audit_64;
            button5.ImageAlign = ContentAlignment.MiddleLeft;
            button5.Location = new Point(12, 187);
            button5.Name = "button5";
            button5.Size = new Size(379, 80);
            button5.TabIndex = 4;
            button5.Text = "Audit Logs";
            button5.UseVisualStyleBackColor = false;
            button5.Click += button5_Click;
            // 
            // button6
            // 
            button6.BackColor = SystemColors.GradientActiveCaption;
            button6.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            button6.Image = Properties.Resources.icons8_exit_64__1_;
            button6.ImageAlign = ContentAlignment.MiddleLeft;
            button6.Location = new Point(397, 187);
            button6.Name = "button6";
            button6.Size = new Size(378, 80);
            button6.TabIndex = 5;
            button6.Text = "Log Out";
            button6.UseVisualStyleBackColor = false;
            button6.Click += button6_Click;
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblUsername.Location = new Point(12, 409);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(69, 25);
            lblUsername.TabIndex = 6;
            lblUsername.Text = "lbluser";
            // 
            // lblPenaltyNotice
            // 
            lblPenaltyNotice.AutoSize = true;
            lblPenaltyNotice.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPenaltyNotice.ForeColor = Color.Red;
            lblPenaltyNotice.Location = new Point(12, 289);
            lblPenaltyNotice.Name = "lblPenaltyNotice";
            lblPenaltyNotice.Size = new Size(144, 28);
            lblPenaltyNotice.TabIndex = 7;
            lblPenaltyNotice.Text = "lblpenaltynotif";
            lblPenaltyNotice.Visible = false;
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            // 
            // lblDateTime
            // 
            lblDateTime.AutoSize = true;
            lblDateTime.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDateTime.Location = new Point(458, 409);
            lblDateTime.Name = "lblDateTime";
            lblDateTime.Size = new Size(108, 25);
            lblDateTime.TabIndex = 9;
            lblDateTime.Text = "lbldatetime";
            lblDateTime.Click += lblDateTime_Click;
            // 
            // DashboardForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientActiveCaption;
            ClientSize = new Size(779, 443);
            Controls.Add(lblDateTime);
            Controls.Add(lblPenaltyNotice);
            Controls.Add(lblUsername);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "DashboardForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Masinloc BPLS - Dashboard";
            Load += DashboardForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private Label lblUsername;
        private Label lblPenaltyNotice;
        private System.Windows.Forms.Timer timer1;
        private Label lblDateTime;
    }
}