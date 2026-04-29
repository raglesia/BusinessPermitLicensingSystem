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
            panel1 = new Panel();
            panel2 = new Panel();
            button7 = new Button();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = SystemColors.GradientActiveCaption;
            button1.BackgroundImageLayout = ImageLayout.None;
            button1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.Image = Properties.Resources.icons8_list_64;
            button1.ImageAlign = ContentAlignment.MiddleLeft;
            button1.Location = new Point(13, 16);
            button1.Name = "button1";
            button1.Size = new Size(379, 80);
            button1.TabIndex = 1;
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
            button2.Location = new Point(408, 188);
            button2.Name = "button2";
            button2.Size = new Size(379, 80);
            button2.TabIndex = 6;
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
            button3.Location = new Point(408, 102);
            button3.Name = "button3";
            button3.Size = new Size(379, 80);
            button3.TabIndex = 4;
            button3.Text = "Update Rates";
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
            button4.Location = new Point(13, 102);
            button4.Name = "button4";
            button4.Size = new Size(379, 80);
            button4.TabIndex = 3;
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
            button5.Location = new Point(13, 188);
            button5.Name = "button5";
            button5.Size = new Size(379, 80);
            button5.TabIndex = 5;
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
            button6.Location = new Point(208, 274);
            button6.Name = "button6";
            button6.Size = new Size(378, 80);
            button6.TabIndex = 7;
            button6.Text = "Log Out";
            button6.UseVisualStyleBackColor = false;
            button6.Click += button6_Click;
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Dock = DockStyle.Left;
            lblUsername.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblUsername.Location = new Point(0, 0);
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
            lblPenaltyNotice.Location = new Point(3, 449);
            lblPenaltyNotice.Name = "lblPenaltyNotice";
            lblPenaltyNotice.Size = new Size(144, 28);
            lblPenaltyNotice.TabIndex = 7;
            lblPenaltyNotice.Text = "lblpenaltynotif";
            lblPenaltyNotice.Visible = false;
            lblPenaltyNotice.Click += lblPenaltyNotice_Click;
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
            lblDateTime.Dock = DockStyle.Right;
            lblDateTime.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDateTime.Location = new Point(698, 0);
            lblDateTime.Name = "lblDateTime";
            lblDateTime.Size = new Size(108, 25);
            lblDateTime.TabIndex = 9;
            lblDateTime.Text = "lbldatetime";
            // 
            // panel1
            // 
            panel1.Controls.Add(lblDateTime);
            panel1.Controls.Add(lblUsername);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 496);
            panel1.Name = "panel1";
            panel1.Size = new Size(806, 36);
            panel1.TabIndex = 10;
            // 
            // panel2
            // 
            panel2.Controls.Add(button7);
            panel2.Controls.Add(button1);
            panel2.Controls.Add(button2);
            panel2.Controls.Add(button3);
            panel2.Controls.Add(button6);
            panel2.Controls.Add(button4);
            panel2.Controls.Add(button5);
            panel2.Location = new Point(3, 82);
            panel2.Name = "panel2";
            panel2.Size = new Size(797, 364);
            panel2.TabIndex = 11;
            // 
            // button7
            // 
            button7.BackColor = SystemColors.GradientActiveCaption;
            button7.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button7.ForeColor = Color.Black;
            button7.Image = Properties.Resources.icons8_container_truck_64;
            button7.ImageAlign = ContentAlignment.MiddleLeft;
            button7.Location = new Point(408, 16);
            button7.Name = "button7";
            button7.Size = new Size(379, 80);
            button7.TabIndex = 2;
            button7.Text = "Delivery Permit";
            button7.UseVisualStyleBackColor = false;
            button7.Click += button7_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Britannic Bold", 17F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.MidnightBlue;
            label1.Location = new Point(3, 30);
            label1.Name = "label1";
            label1.Size = new Size(578, 37);
            label1.TabIndex = 12;
            label1.Text = "Business Permit and Licensing Office";
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Image = Properties.Resources.Untitled_1__1_;
            pictureBox1.Location = new Point(580, 16);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(220, 67);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 13;
            pictureBox1.TabStop = false;
            // 
            // DashboardForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientActiveCaption;
            ClientSize = new Size(806, 532);
            Controls.Add(pictureBox1);
            Controls.Add(label1);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(lblPenaltyNotice);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "DashboardForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Masinloc BPLS - Dashboard";
            Load += DashboardForm_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
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
        private Panel panel1;
        private Panel panel2;
        private Button button7;
        private Label label1;
        private PictureBox pictureBox1;
    }
}