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
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label1.Location = new Point(8, 27);
            label1.Name = "label1";
            label1.Size = new Size(51, 28);
            label1.TabIndex = 0;
            label1.Text = "VIN:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label2.Location = new Point(8, 69);
            label2.Name = "label2";
            label2.Size = new Size(163, 28);
            label2.TabIndex = 1;
            label2.Text = "Company Name:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label3.Location = new Point(8, 108);
            label3.Name = "label3";
            label3.Size = new Size(131, 28);
            label3.TabIndex = 2;
            label3.Text = "Driver Name:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label4.Location = new Point(8, 149);
            label4.Name = "label4";
            label4.Size = new Size(144, 28);
            label4.TabIndex = 3;
            label4.Text = "Plate Number:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label5.Location = new Point(8, 190);
            label5.Name = "label5";
            label5.Size = new Size(246, 28);
            label5.TabIndex = 4;
            label5.Text = "SEC Registration Number:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label6.Location = new Point(8, 231);
            label6.Name = "label6";
            label6.Size = new Size(129, 28);
            label6.TabIndex = 5;
            label6.Text = "DTI Number:";
            // 
            // txtVIN
            // 
            txtVIN.Location = new Point(255, 24);
            txtVIN.Name = "txtVIN";
            txtVIN.Size = new Size(410, 31);
            txtVIN.TabIndex = 6;
            // 
            // txtCompanyName
            // 
            txtCompanyName.Location = new Point(255, 66);
            txtCompanyName.Name = "txtCompanyName";
            txtCompanyName.Size = new Size(410, 31);
            txtCompanyName.TabIndex = 7;
            // 
            // txtDriverName
            // 
            txtDriverName.Location = new Point(255, 105);
            txtDriverName.Name = "txtDriverName";
            txtDriverName.Size = new Size(410, 31);
            txtDriverName.TabIndex = 8;
            // 
            // txtPlateNumber
            // 
            txtPlateNumber.Location = new Point(255, 146);
            txtPlateNumber.Name = "txtPlateNumber";
            txtPlateNumber.Size = new Size(410, 31);
            txtPlateNumber.TabIndex = 9;
            // 
            // txtSec
            // 
            txtSec.Location = new Point(255, 187);
            txtSec.Name = "txtSec";
            txtSec.Size = new Size(410, 31);
            txtSec.TabIndex = 10;
            // 
            // txtDTI
            // 
            txtDTI.Location = new Point(255, 228);
            txtDTI.Name = "txtDTI";
            txtDTI.Size = new Size(410, 31);
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
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(671, 268);
            groupBox1.TabIndex = 12;
            groupBox1.TabStop = false;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(144, 504);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(202, 64);
            btnSave.TabIndex = 13;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(352, 504);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(202, 64);
            btnCancel.TabIndex = 14;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // VehicleProfiling
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(695, 648);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(groupBox1);
            Name = "VehicleProfiling";
            Text = "Masinloc BPLS - Special Vehicle Permit Profiling";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
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
    }
}