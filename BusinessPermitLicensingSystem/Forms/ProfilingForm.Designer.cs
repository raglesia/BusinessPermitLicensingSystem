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
            menuStrip1 = new MenuStrip();
            txtFName = new TextBox();
            txtBName = new TextBox();
            txtBSection = new TextBox();
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
            btnSaveAs = new Button();
            lblUsername = new Label();
            label8 = new Label();
            cmbPaymentStatus = new ComboBox();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1341, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // txtFName
            // 
            txtFName.Location = new Point(212, 99);
            txtFName.Name = "txtFName";
            txtFName.Size = new Size(402, 31);
            txtFName.TabIndex = 1;
            // 
            // txtBName
            // 
            txtBName.Location = new Point(212, 146);
            txtBName.Name = "txtBName";
            txtBName.Size = new Size(402, 31);
            txtBName.TabIndex = 2;
            // 
            // txtBSection
            // 
            txtBSection.Location = new Point(212, 194);
            txtBSection.Name = "txtBSection";
            txtBSection.Size = new Size(402, 31);
            txtBSection.TabIndex = 3;
            // 
            // txtSNumber
            // 
            txtSNumber.Location = new Point(212, 245);
            txtSNumber.Name = "txtSNumber";
            txtSNumber.Size = new Size(402, 31);
            txtSNumber.TabIndex = 4;
            // 
            // txtSSize
            // 
            txtSSize.Location = new Point(212, 293);
            txtSSize.Name = "txtSSize";
            txtSSize.Size = new Size(402, 31);
            txtSSize.TabIndex = 5;
            // 
            // txtMRental
            // 
            txtMRental.Location = new Point(212, 342);
            txtMRental.Name = "txtMRental";
            txtMRental.Size = new Size(402, 31);
            txtMRental.TabIndex = 6;
            txtMRental.Leave += txtMRental_Leave;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(16, 105);
            label1.Name = "label1";
            label1.Size = new Size(100, 25);
            label1.TabIndex = 7;
            label1.Text = "Full Name: ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(16, 152);
            label2.Name = "label2";
            label2.Size = new Size(140, 25);
            label2.TabIndex = 8;
            label2.Text = "Business Name: ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(14, 200);
            label3.Name = "label3";
            label3.Size = new Size(142, 25);
            label3.TabIndex = 9;
            label3.Text = "Business Section";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(14, 251);
            label4.Name = "label4";
            label4.Size = new Size(123, 25);
            label4.TabIndex = 10;
            label4.Text = "Stall Number: ";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 299);
            label5.Name = "label5";
            label5.Size = new Size(89, 25);
            label5.TabIndex = 11;
            label5.Text = "Stall Size: ";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(14, 348);
            label6.Name = "label6";
            label6.Size = new Size(140, 25);
            label6.TabIndex = 12;
            label6.Text = "Monthly Rental: ";
            // 
            // txtBIN
            // 
            txtBIN.Location = new Point(212, 53);
            txtBIN.Name = "txtBIN";
            txtBIN.Size = new Size(402, 31);
            txtBIN.TabIndex = 13;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(16, 56);
            label7.Name = "label7";
            label7.Size = new Size(49, 25);
            label7.TabIndex = 14;
            label7.Text = "SIN: ";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(12, 473);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(155, 34);
            btnSave.TabIndex = 15;
            btnSave.Text = "Save Record";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // button1
            // 
            button1.Location = new Point(338, 473);
            button1.Name = "button1";
            button1.Size = new Size(145, 34);
            button1.TabIndex = 16;
            button1.Text = "Cancel";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // btnSaveAs
            // 
            btnSaveAs.Location = new Point(191, 473);
            btnSaveAs.Name = "btnSaveAs";
            btnSaveAs.Size = new Size(112, 34);
            btnSaveAs.TabIndex = 17;
            btnSaveAs.Text = "Save As";
            btnSaveAs.UseVisualStyleBackColor = true;
            btnSaveAs.Click += btnSaveAs_Click;
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(14, 672);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(0, 25);
            lblUsername.TabIndex = 18;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(16, 391);
            label8.Name = "label8";
            label8.Size = new Size(137, 25);
            label8.TabIndex = 19;
            label8.Text = "Payment Status:";
            // 
            // cmbPaymentStatus
            // 
            cmbPaymentStatus.FormattingEnabled = true;
            cmbPaymentStatus.Location = new Point(212, 391);
            cmbPaymentStatus.Name = "cmbPaymentStatus";
            cmbPaymentStatus.Size = new Size(402, 33);
            cmbPaymentStatus.TabIndex = 20;
            // 
            // ProfilingForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1341, 706);
            Controls.Add(cmbPaymentStatus);
            Controls.Add(label8);
            Controls.Add(lblUsername);
            Controls.Add(btnSaveAs);
            Controls.Add(button1);
            Controls.Add(btnSave);
            Controls.Add(label7);
            Controls.Add(txtBIN);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtMRental);
            Controls.Add(txtSSize);
            Controls.Add(txtSNumber);
            Controls.Add(txtBSection);
            Controls.Add(txtBName);
            Controls.Add(txtFName);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "ProfilingForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Masinloc - BPLS";
            WindowState = FormWindowState.Maximized;
            Load += ProfilingForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
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
        private Button btnSaveAs;
        private Label lblUsername;
        private Label label8;
        private ComboBox cmbPaymentStatus;
    }
}