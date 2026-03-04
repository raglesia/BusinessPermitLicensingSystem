namespace BusinessPermitLicensingSystem
{
    partial class ProfilingLists
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
            txtFilterMonthlyRental = new TextBox();
            txtFilterStallSize = new TextBox();
            txtFilterStallNumber = new TextBox();
            txtFilterBusinessSection = new TextBox();
            txtFilterBusinessName = new TextBox();
            txtFilterFullName = new TextBox();
            txtFilterBIN = new TextBox();
            dataGridView1 = new DataGridView();
            button1 = new Button();
            btnDelete = new Button();
            btnExport = new Button();
            lblUsername = new Label();
            button2 = new Button();
            btnPaymentHistory = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // txtFilterMonthlyRental
            // 
            txtFilterMonthlyRental.Location = new Point(1283, 110);
            txtFilterMonthlyRental.Name = "txtFilterMonthlyRental";
            txtFilterMonthlyRental.Size = new Size(150, 31);
            txtFilterMonthlyRental.TabIndex = 30;
            // 
            // txtFilterStallSize
            // 
            txtFilterStallSize.Location = new Point(1098, 110);
            txtFilterStallSize.Name = "txtFilterStallSize";
            txtFilterStallSize.Size = new Size(150, 31);
            txtFilterStallSize.TabIndex = 29;
            // 
            // txtFilterStallNumber
            // 
            txtFilterStallNumber.Location = new Point(908, 110);
            txtFilterStallNumber.Name = "txtFilterStallNumber";
            txtFilterStallNumber.Size = new Size(150, 31);
            txtFilterStallNumber.TabIndex = 28;
            // 
            // txtFilterBusinessSection
            // 
            txtFilterBusinessSection.Location = new Point(709, 110);
            txtFilterBusinessSection.Name = "txtFilterBusinessSection";
            txtFilterBusinessSection.Size = new Size(150, 31);
            txtFilterBusinessSection.TabIndex = 27;
            // 
            // txtFilterBusinessName
            // 
            txtFilterBusinessName.Location = new Point(474, 121);
            txtFilterBusinessName.Name = "txtFilterBusinessName";
            txtFilterBusinessName.Size = new Size(150, 31);
            txtFilterBusinessName.TabIndex = 26;
            // 
            // txtFilterFullName
            // 
            txtFilterFullName.Location = new Point(285, 121);
            txtFilterFullName.Name = "txtFilterFullName";
            txtFilterFullName.Size = new Size(150, 31);
            txtFilterFullName.TabIndex = 25;
            // 
            // txtFilterBIN
            // 
            txtFilterBIN.Location = new Point(86, 110);
            txtFilterBIN.Name = "txtFilterBIN";
            txtFilterBIN.Size = new Size(173, 31);
            txtFilterBIN.TabIndex = 24;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(86, 158);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.Size = new Size(1347, 316);
            dataGridView1.TabIndex = 23;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick;
            // 
            // button1
            // 
            button1.Location = new Point(1427, 12);
            button1.Name = "button1";
            button1.Size = new Size(112, 49);
            button1.TabIndex = 31;
            button1.Text = "Main Menu";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(1124, 495);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(145, 49);
            btnDelete.TabIndex = 32;
            btnDelete.Text = "Delete Record";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnExport
            // 
            btnExport.Location = new Point(1275, 495);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(158, 49);
            btnExport.TabIndex = 33;
            btnExport.Text = "Export";
            btnExport.UseVisualStyleBackColor = true;
            btnExport.Click += btnExport_Click;
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(12, 536);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(0, 25);
            lblUsername.TabIndex = 34;
            // 
            // button2
            // 
            button2.Location = new Point(86, 495);
            button2.Name = "button2";
            button2.Size = new Size(186, 49);
            button2.TabIndex = 35;
            button2.Text = "Generate Receipt";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // btnPaymentHistory
            // 
            btnPaymentHistory.Location = new Point(285, 495);
            btnPaymentHistory.Name = "btnPaymentHistory";
            btnPaymentHistory.Size = new Size(208, 49);
            btnPaymentHistory.TabIndex = 36;
            btnPaymentHistory.Text = "Payment History";
            btnPaymentHistory.UseVisualStyleBackColor = true;
            btnPaymentHistory.Click += btnPaymentHistory_Click;
            // 
            // ProfilingLists
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1551, 570);
            Controls.Add(btnPaymentHistory);
            Controls.Add(button2);
            Controls.Add(lblUsername);
            Controls.Add(btnExport);
            Controls.Add(btnDelete);
            Controls.Add(button1);
            Controls.Add(txtFilterMonthlyRental);
            Controls.Add(txtFilterStallSize);
            Controls.Add(txtFilterStallNumber);
            Controls.Add(txtFilterBusinessSection);
            Controls.Add(txtFilterBusinessName);
            Controls.Add(txtFilterFullName);
            Controls.Add(txtFilterBIN);
            Controls.Add(dataGridView1);
            Name = "ProfilingLists";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Masinloc - BPLS";
            WindowState = FormWindowState.Maximized;
            Load += ProfilingLists_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtFilterMonthlyRental;
        private TextBox txtFilterStallSize;
        private TextBox txtFilterStallNumber;
        private TextBox txtFilterBusinessSection;
        private TextBox txtFilterBusinessName;
        private TextBox txtFilterFullName;
        private TextBox txtFilterBIN;
        private DataGridView dataGridView1;
        private Button button1;
        private Button btnDelete;
        private Button btnExport;
        private Label lblUsername;
        private Button button2;
        private Button btnPaymentHistory;
    }
}