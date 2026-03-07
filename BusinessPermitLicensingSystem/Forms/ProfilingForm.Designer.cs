namespace BusinessPermitLicensingSystem.Forms
{
    partial class ProfilingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProfilingForm));
            txtFName = new TextBox();
            txtBName = new TextBox();
            txtSNumber = new TextBox();
            txtSSize = new TextBox();
            txtMRental = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            txtBIN = new TextBox();
            label7 = new Label();
            btnSave = new Button();
            button1 = new Button();
            lblUsername = new Label();
            label8 = new Label();
            cmbPaymentStatus = new ComboBox();
            dtpStartDate = new DateTimePicker();
            label9 = new Label();
            cmbBSection = new ComboBox();
            chkAdditional = new CheckBox();
            txtAdditionalCharge = new TextBox();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            groupBox3 = new GroupBox();
            panel1 = new Panel();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // txtFName
            // 
            txtFName.BackColor = SystemColors.GradientActiveCaption;
            txtFName.Location = new Point(238, 75);
            txtFName.Name = "txtFName";
            txtFName.Size = new Size(410, 34);
            txtFName.TabIndex = 1;
            // 
            // txtBName
            // 
            txtBName.BackColor = SystemColors.GradientActiveCaption;
            txtBName.Location = new Point(238, 118);
            txtBName.Name = "txtBName";
            txtBName.Size = new Size(410, 34);
            txtBName.TabIndex = 2;
            // 
            // txtSNumber
            // 
            txtSNumber.BackColor = SystemColors.GradientActiveCaption;
            txtSNumber.Location = new Point(238, 70);
            txtSNumber.Name = "txtSNumber";
            txtSNumber.Size = new Size(410, 34);
            txtSNumber.TabIndex = 4;
            // 
            // txtSSize
            // 
            txtSSize.BackColor = SystemColors.GradientActiveCaption;
            txtSSize.Location = new Point(238, 107);
            txtSSize.Name = "txtSSize";
            txtSSize.Size = new Size(410, 34);
            txtSSize.TabIndex = 5;
            // 
            // txtMRental
            // 
            txtMRental.BackColor = SystemColors.GradientActiveCaption;
            txtMRental.Location = new Point(238, 74);
            txtMRental.Name = "txtMRental";
            txtMRental.Size = new Size(410, 34);
            txtMRental.TabIndex = 6;
            txtMRental.Leave += txtMRental_Leave;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(6, 75);
            label1.Name = "label1";
            label1.Size = new Size(115, 28);
            label1.TabIndex = 7;
            label1.Text = "Full Name: ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(6, 112);
            label2.Name = "label2";
            label2.Size = new Size(162, 28);
            label2.TabIndex = 8;
            label2.Text = "Business Name: ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(6, 39);
            label3.Name = "label3";
            label3.Size = new Size(169, 28);
            label3.TabIndex = 9;
            label3.Text = "Business Section:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label4.Location = new Point(6, 76);
            label4.Name = "label4";
            label4.Size = new Size(142, 28);
            label4.TabIndex = 10;
            label4.Text = "Stall Number: ";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label5.Location = new Point(6, 113);
            label5.Name = "label5";
            label5.Size = new Size(102, 28);
            label5.TabIndex = 11;
            label5.Text = "Stall Size: ";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label6.Location = new Point(6, 77);
            label6.Name = "label6";
            label6.Size = new Size(161, 28);
            label6.TabIndex = 12;
            label6.Text = "Monthly Rental: ";
            // 
            // txtBIN
            // 
            txtBIN.BackColor = SystemColors.GradientActiveCaption;
            txtBIN.Location = new Point(238, 38);
            txtBIN.Name = "txtBIN";
            txtBIN.Size = new Size(410, 34);
            txtBIN.TabIndex = 13;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.Location = new Point(6, 37);
            label7.Name = "label7";
            label7.Size = new Size(55, 28);
            label7.TabIndex = 14;
            label7.Text = "SIN: ";
            // 
            // btnSave
            // 
            btnSave.BackColor = SystemColors.GradientActiveCaption;
            btnSave.Image = Properties.Resources.icons8_save_64;
            btnSave.ImageAlign = ContentAlignment.MiddleLeft;
            btnSave.Location = new Point(101, 535);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(202, 64);
            btnSave.TabIndex = 15;
            btnSave.Text = "          Save Record";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // button1
            // 
            button1.BackColor = SystemColors.GradientActiveCaption;
            button1.Image = Properties.Resources.icons8_back_64;
            button1.ImageAlign = ContentAlignment.MiddleLeft;
            button1.Location = new Point(370, 535);
            button1.Name = "button1";
            button1.Size = new Size(202, 64);
            button1.TabIndex = 16;
            button1.Text = "      BACK";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblUsername.Location = new Point(3, 10);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(96, 25);
            lblUsername.TabIndex = 18;
            lblUsername.Text = "Username";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label8.Location = new Point(6, 119);
            label8.Name = "label8";
            label8.Size = new Size(157, 28);
            label8.TabIndex = 19;
            label8.Text = "Payment Status:";
            // 
            // cmbPaymentStatus
            // 
            cmbPaymentStatus.BackColor = SystemColors.GradientActiveCaption;
            cmbPaymentStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPaymentStatus.FormattingEnabled = true;
            cmbPaymentStatus.Location = new Point(238, 111);
            cmbPaymentStatus.Name = "cmbPaymentStatus";
            cmbPaymentStatus.Size = new Size(410, 36);
            cmbPaymentStatus.TabIndex = 20;
            // 
            // dtpStartDate
            // 
            dtpStartDate.CalendarMonthBackground = SystemColors.GradientActiveCaption;
            dtpStartDate.Location = new Point(238, 147);
            dtpStartDate.Name = "dtpStartDate";
            dtpStartDate.Size = new Size(410, 34);
            dtpStartDate.TabIndex = 21;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(6, 152);
            label9.Name = "label9";
            label9.Size = new Size(203, 28);
            label9.TabIndex = 22;
            label9.Text = "Date of Occupancy: ";
            // 
            // cmbBSection
            // 
            cmbBSection.BackColor = SystemColors.GradientActiveCaption;
            cmbBSection.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbBSection.FormattingEnabled = true;
            cmbBSection.Location = new Point(238, 31);
            cmbBSection.Name = "cmbBSection";
            cmbBSection.Size = new Size(410, 36);
            cmbBSection.TabIndex = 23;
            // 
            // chkAdditional
            // 
            chkAdditional.AutoSize = true;
            chkAdditional.CheckAlign = ContentAlignment.MiddleRight;
            chkAdditional.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            chkAdditional.Location = new Point(6, 39);
            chkAdditional.Name = "chkAdditional";
            chkAdditional.Size = new Size(221, 32);
            chkAdditional.TabIndex = 24;
            chkAdditional.Text = "Additional Charges: ";
            chkAdditional.TextAlign = ContentAlignment.MiddleCenter;
            chkAdditional.UseVisualStyleBackColor = true;
            // 
            // txtAdditionalCharge
            // 
            txtAdditionalCharge.BackColor = SystemColors.GradientActiveCaption;
            txtAdditionalCharge.Location = new Point(238, 37);
            txtAdditionalCharge.Name = "txtAdditionalCharge";
            txtAdditionalCharge.Size = new Size(410, 34);
            txtAdditionalCharge.TabIndex = 26;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(txtBIN);
            groupBox1.Controls.Add(txtFName);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(txtBName);
            groupBox1.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox1.Location = new Point(12, 1);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(654, 155);
            groupBox1.TabIndex = 27;
            groupBox1.TabStop = false;
            groupBox1.Text = "STALL OWNER INFORMATION";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(cmbBSection);
            groupBox2.Controls.Add(txtSNumber);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(txtSSize);
            groupBox2.Controls.Add(label9);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(dtpStartDate);
            groupBox2.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox2.Location = new Point(12, 162);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(654, 190);
            groupBox2.TabIndex = 28;
            groupBox2.TabStop = false;
            groupBox2.Text = "STALL INFORMATION";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(txtAdditionalCharge);
            groupBox3.Controls.Add(chkAdditional);
            groupBox3.Controls.Add(txtMRental);
            groupBox3.Controls.Add(label8);
            groupBox3.Controls.Add(cmbPaymentStatus);
            groupBox3.Controls.Add(label6);
            groupBox3.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox3.Location = new Point(12, 358);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(654, 157);
            groupBox3.TabIndex = 29;
            groupBox3.TabStop = false;
            groupBox3.Text = "RENTAL INFORMATION";
            // 
            // panel1
            // 
            panel1.Controls.Add(lblUsername);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 605);
            panel1.Name = "panel1";
            panel1.Size = new Size(670, 35);
            panel1.TabIndex = 30;
            // 
            // ProfilingForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientActiveCaption;
            ClientSize = new Size(670, 640);
            Controls.Add(panel1);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(button1);
            Controls.Add(btnSave);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "ProfilingForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Masinloc - BPLS";
            Load += ProfilingForm_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private TextBox txtFName;
        private TextBox txtBName;
        private TextBox txtBSection;
        private TextBox txtSNumber;
        private TextBox txtSSize;
        private TextBox txtMRental;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private TextBox txtBIN;
        private Label label7;
        private Button btnSave;
        private Button button1;
        private Label lblUsername;
        private Label label8;
        private ComboBox cmbPaymentStatus;
        private DateTimePicker dtpStartDate;
        private Label label9;
        private ComboBox cmbBSection;
        private CheckBox chkAdditional;
        private TextBox txtAdditionalCharge;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private Panel panel1;
    }
}