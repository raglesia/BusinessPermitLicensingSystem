namespace BusinessPermitLicensingSystem.Forms
{
    partial class VehiclePermitLists
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
            dataGridView1 = new DataGridView();
            txtSearch = new TextBox();
            lblTotalRecords = new Label();
            lblUsername = new Label();
            lblTotalVehicles = new Label();
            lblTotalPaid = new Label();
            lblTotalUnpaid = new Label();
            btnPaymentHistory = new Button();
            btnGenerateReceipt = new Button();
            btnArchive = new Button();
            btnDelete = new Button();
            btnImport = new Button();
            btnReport = new Button();
            btnExport = new Button();
            btnMenu = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            panel1 = new Panel();
            panel2 = new Panel();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(3, 123);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.Size = new Size(1879, 458);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick;
            dataGridView1.ColumnHeaderMouseClick += dataGridView1_ColumnHeaderMouseClick;
            // 
            // txtSearch
            // 
            txtSearch.Dock = DockStyle.Fill;
            txtSearch.Location = new Point(3, 78);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(1879, 31);
            txtSearch.TabIndex = 1;
            // 
            // lblTotalRecords
            // 
            lblTotalRecords.AutoSize = true;
            lblTotalRecords.Dock = DockStyle.Right;
            lblTotalRecords.Location = new Point(1703, 0);
            lblTotalRecords.Name = "lblTotalRecords";
            lblTotalRecords.Size = new Size(176, 25);
            lblTotalRecords.TabIndex = 2;
            lblTotalRecords.Text = "Total Records: 00000";
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Dock = DockStyle.Left;
            lblUsername.Location = new Point(0, 0);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(91, 25);
            lblUsername.TabIndex = 3;
            lblUsername.Text = "Username";
            // 
            // lblTotalVehicles
            // 
            lblTotalVehicles.AutoSize = true;
            lblTotalVehicles.Location = new Point(566, 4);
            lblTotalVehicles.Name = "lblTotalVehicles";
            lblTotalVehicles.Size = new Size(175, 25);
            lblTotalVehicles.TabIndex = 15;
            lblTotalVehicles.Text = "Total Vehicles: 00000";
            // 
            // lblTotalPaid
            // 
            lblTotalPaid.AutoSize = true;
            lblTotalPaid.Location = new Point(402, 4);
            lblTotalPaid.Name = "lblTotalPaid";
            lblTotalPaid.Size = new Size(146, 25);
            lblTotalPaid.TabIndex = 16;
            lblTotalPaid.Text = "Total Paid: 00000";
            // 
            // lblTotalUnpaid
            // 
            lblTotalUnpaid.AutoSize = true;
            lblTotalUnpaid.Location = new Point(747, 7);
            lblTotalUnpaid.Name = "lblTotalUnpaid";
            lblTotalUnpaid.Size = new Size(170, 25);
            lblTotalUnpaid.TabIndex = 17;
            lblTotalUnpaid.Text = "Total Unpaid: 00000";
            // 
            // btnPaymentHistory
            // 
            btnPaymentHistory.Location = new Point(3, 0);
            btnPaymentHistory.Name = "btnPaymentHistory";
            btnPaymentHistory.Size = new Size(219, 68);
            btnPaymentHistory.TabIndex = 18;
            btnPaymentHistory.Text = "Payment History";
            btnPaymentHistory.UseVisualStyleBackColor = true;
            // 
            // btnGenerateReceipt
            // 
            btnGenerateReceipt.Location = new Point(228, 0);
            btnGenerateReceipt.Name = "btnGenerateReceipt";
            btnGenerateReceipt.Size = new Size(219, 68);
            btnGenerateReceipt.TabIndex = 19;
            btnGenerateReceipt.Text = "Generate Receipt";
            btnGenerateReceipt.UseVisualStyleBackColor = true;
            // 
            // btnArchive
            // 
            btnArchive.Location = new Point(453, 0);
            btnArchive.Name = "btnArchive";
            btnArchive.Size = new Size(219, 68);
            btnArchive.TabIndex = 20;
            btnArchive.Text = "Archive Record";
            btnArchive.UseVisualStyleBackColor = true;
            btnArchive.Click += btnArchive_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(678, 0);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(219, 68);
            btnDelete.TabIndex = 21;
            btnDelete.Text = "Delete Record";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnImport
            // 
            btnImport.Location = new Point(1210, 0);
            btnImport.Name = "btnImport";
            btnImport.Size = new Size(219, 68);
            btnImport.TabIndex = 22;
            btnImport.Text = "Import Records";
            btnImport.UseVisualStyleBackColor = true;
            btnImport.Click += btnImport_Click;
            // 
            // btnReport
            // 
            btnReport.Location = new Point(985, 0);
            btnReport.Name = "btnReport";
            btnReport.Size = new Size(219, 68);
            btnReport.TabIndex = 23;
            btnReport.Text = "Collection Report";
            btnReport.UseVisualStyleBackColor = true;
            // 
            // btnExport
            // 
            btnExport.Location = new Point(1435, 0);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(219, 68);
            btnExport.TabIndex = 24;
            btnExport.Text = "Export Records";
            btnExport.UseVisualStyleBackColor = true;
            btnExport.Click += btnExport_Click;
            // 
            // btnMenu
            // 
            btnMenu.Location = new Point(1660, 0);
            btnMenu.Name = "btnMenu";
            btnMenu.Size = new Size(219, 68);
            btnMenu.TabIndex = 25;
            btnMenu.Text = "Menu";
            btnMenu.UseVisualStyleBackColor = true;
            btnMenu.Click += btnMenu_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(panel1, 0, 0);
            tableLayoutPanel1.Controls.Add(dataGridView1, 0, 2);
            tableLayoutPanel1.Controls.Add(txtSearch, 0, 1);
            tableLayoutPanel1.Controls.Add(panel2, 0, 3);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 75F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tableLayoutPanel1.Size = new Size(1885, 619);
            tableLayoutPanel1.TabIndex = 26;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnPaymentHistory);
            panel1.Controls.Add(btnMenu);
            panel1.Controls.Add(btnGenerateReceipt);
            panel1.Controls.Add(btnExport);
            panel1.Controls.Add(btnArchive);
            panel1.Controls.Add(btnReport);
            panel1.Controls.Add(btnDelete);
            panel1.Controls.Add(btnImport);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(1879, 69);
            panel1.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Controls.Add(lblUsername);
            panel2.Controls.Add(lblTotalUnpaid);
            panel2.Controls.Add(lblTotalRecords);
            panel2.Controls.Add(lblTotalVehicles);
            panel2.Controls.Add(lblTotalPaid);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(3, 587);
            panel2.Name = "panel2";
            panel2.Size = new Size(1879, 29);
            panel2.TabIndex = 2;
            // 
            // VehiclePermitLists
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1885, 619);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MinimumSize = new Size(1024, 600);
            Name = "VehiclePermitLists";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Masinloc BPLS - Special Vehicle Permit List";
            WindowState = FormWindowState.Maximized;
            Load += VehiclePermitLists_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private TextBox txtSearch;
        private Label lblTotalRecords;
        private Label lblUsername;
        private Label lblTotalVehicles;
        private Label lblTotalPaid;
        private Label lblTotalUnpaid;
        private Button btnPaymentHistory;
        private Button btnGenerateReceipt;
        private Button btnArchive;
        private Button btnDelete;
        private Button btnImport;
        private Button btnReport;
        private Button btnExport;
        private Button btnMenu;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel1;
        private Panel panel2;
    }
}