namespace BusinessPermitLicensingSystem.Forms
{
    partial class VehicleProfiling
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VehicleProfiling));
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            txtVIN = new TextBox();
            txtCompanyName = new TextBox();
            txtDriverName = new TextBox();
            txtPlateNumber = new TextBox();
            txtSec = new TextBox();
            txtDTI = new TextBox();
            groupBox1 = new GroupBox();
            btnSave = new Button();
            btnCancel = new Button();
            panel1 = new Panel();
            lblUsername = new Label();
            groupBox1.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label1.Location = new Point(6, 22);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(43, 23);
            label1.TabIndex = 0;
            label1.Text = "VIN:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label2.Location = new Point(6, 49);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(139, 23);
            label2.TabIndex = 1;
            label2.Text = "Company Name:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label3.Location = new Point(6, 84);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(133, 23);
            label3.TabIndex = 2;
            label3.Text = "Name of Driver:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label4.Location = new Point(6, 115);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(121, 23);
            label4.TabIndex = 3;
            label4.Text = "Plate Number:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label5.Location = new Point(6, 146);
            label5.Margin = new Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new Size(209, 23);
            label5.TabIndex = 4;
            label5.Text = "SEC Registration Number:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label6.Location = new Point(6, 177);
            label6.Margin = new Padding(2, 0, 2, 0);
            label6.Name = "label6";
            label6.Size = new Size(108, 23);
            label6.TabIndex = 5;
            label6.Text = "DTI Number:";
            // 
            // txtVIN
            // 
            txtVIN.Location = new Point(272, 18);
            txtVIN.Margin = new Padding(2);
            txtVIN.Name = "txtVIN";
            txtVIN.Size = new Size(329, 27);
            txtVIN.TabIndex = 6;
            // 
            // txtCompanyName
            // 
            txtCompanyName.Location = new Point(272, 49);
            txtCompanyName.Margin = new Padding(2);
            txtCompanyName.Name = "txtCompanyName";
            txtCompanyName.Size = new Size(329, 27);
            txtCompanyName.TabIndex = 7;
            // 
            // txtDriverName
            // 
            txtDriverName.Location = new Point(272, 80);
            txtDriverName.Margin = new Padding(2);
            txtDriverName.Name = "txtDriverName";
            txtDriverName.Size = new Size(329, 27);
            txtDriverName.TabIndex = 8;
            // 
            // txtPlateNumber
            // 
            txtPlateNumber.Location = new Point(272, 111);
            txtPlateNumber.Margin = new Padding(2);
            txtPlateNumber.Name = "txtPlateNumber";
            txtPlateNumber.Size = new Size(329, 27);
            txtPlateNumber.TabIndex = 9;
            // 
            // txtSec
            // 
            txtSec.Location = new Point(272, 142);
            txtSec.Margin = new Padding(2);
            txtSec.Name = "txtSec";
            txtSec.Size = new Size(329, 27);
            txtSec.TabIndex = 10;
            // 
            // txtDTI
            // 
            txtDTI.Location = new Point(272, 173);
            txtDTI.Margin = new Padding(2);
            txtDTI.Name = "txtDTI";
            txtDTI.Size = new Size(329, 27);
            txtDTI.TabIndex = 11;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(txtDTI);
            groupBox1.Controls.Add(txtSec);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(txtPlateNumber);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(txtDriverName);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(txtCompanyName);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(txtVIN);
            groupBox1.Controls.Add(label6);
            groupBox1.Location = new Point(10, 10);
            groupBox1.Margin = new Padding(2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(2);
            groupBox1.Size = new Size(605, 206);
            groupBox1.TabIndex = 12;
            groupBox1.TabStop = false;
            // 
            // btnSave
            // 
            btnSave.BackColor = SystemColors.GradientActiveCaption;
            btnSave.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSave.Location = new Point(135, 235);
            btnSave.Margin = new Padding(2);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(162, 51);
            btnSave.TabIndex = 13;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = SystemColors.GradientActiveCaption;
            btnCancel.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCancel.Location = new Point(301, 235);
            btnCancel.Margin = new Padding(2);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(162, 51);
            btnCancel.TabIndex = 14;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(lblUsername);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 291);
            panel1.Name = "panel1";
            panel1.Size = new Size(623, 32);
            panel1.TabIndex = 15;
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Dock = DockStyle.Bottom;
            lblUsername.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblUsername.Location = new Point(0, 12);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(78, 20);
            lblUsername.TabIndex = 16;
            lblUsername.Text = "Username";
            // 
            // VehicleProfiling
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientActiveCaption;
            ClientSize = new Size(623, 323);
            Controls.Add(panel1);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(groupBox1);
            ForeColor = SystemColors.Desktop;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(2);
            MaximizeBox = false;
            Name = "VehicleProfiling";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Masinloc BPLS - Special Vehicle Permit Profiling";
            Load += VehicleProfiling_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private TextBox txtVIN;
        private TextBox txtCompanyName;
        private TextBox txtDriverName;
        private TextBox txtPlateNumber;
        private TextBox txtSec;
        private TextBox txtDTI;
        private GroupBox groupBox1;
        private Button btnSave;
        private Button btnCancel;
        private Panel panel1;
        private Label lblUsername;
    }
}