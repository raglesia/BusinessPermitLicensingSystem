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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProfilingLists));
            panelFilters = new Panel();
            txtSearch = new TextBox();
            dataGridView1 = new DataGridView();
            panelButtons = new Panel();
            btnMonthlyReport = new Button();
            btnArchive = new Button();
            btnDelete = new Button();
            lblTotalRecords = new Label();
            button2 = new Button();
            btnPaymentHistory = new Button();
            button1 = new Button();
            btnExport = new Button();
            lblUsername = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            panelFilters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panelButtons.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // panelFilters
            // 
            panelFilters.Controls.Add(txtSearch);
            panelFilters.Dock = DockStyle.Fill;
            panelFilters.Location = new Point(3, 78);
            panelFilters.Name = "panelFilters";
            panelFilters.Size = new Size(1879, 39);
            panelFilters.TabIndex = 39;
            // 
            // txtSearch
            // 
            txtSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtSearch.BackColor = SystemColors.GradientActiveCaption;
            txtSearch.Location = new Point(3, 3);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(1873, 31);
            txtSearch.TabIndex = 0;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(3, 123);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.Size = new Size(1879, 458);
            dataGridView1.TabIndex = 23;
            dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick;
            // 
            // panelButtons
            // 
            panelButtons.Controls.Add(btnMonthlyReport);
            panelButtons.Controls.Add(btnArchive);
            panelButtons.Controls.Add(btnDelete);
            panelButtons.Controls.Add(lblTotalRecords);
            panelButtons.Controls.Add(button2);
            panelButtons.Controls.Add(btnPaymentHistory);
            panelButtons.Controls.Add(button1);
            panelButtons.Controls.Add(btnExport);
            panelButtons.Dock = DockStyle.Fill;
            panelButtons.Location = new Point(3, 3);
            panelButtons.Name = "panelButtons";
            panelButtons.Size = new Size(1879, 69);
            panelButtons.TabIndex = 38;
            // 
            // btnMonthlyReport
            // 
            btnMonthlyReport.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMonthlyReport.BackColor = SystemColors.GradientActiveCaption;
            btnMonthlyReport.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnMonthlyReport.Image = Properties.Resources.icons8_ledger_64;
            btnMonthlyReport.ImageAlign = ContentAlignment.MiddleLeft;
            btnMonthlyReport.Location = new Point(1288, 1);
            btnMonthlyReport.Name = "btnMonthlyReport";
            btnMonthlyReport.Size = new Size(192, 68);
            btnMonthlyReport.TabIndex = 42;
            btnMonthlyReport.Text = "           Collection\r            Report\r\n";
            btnMonthlyReport.UseVisualStyleBackColor = false;
            btnMonthlyReport.Click += btnMonthlyReport_Click_1;
            // 
            // btnArchive
            // 
            btnArchive.BackColor = SystemColors.GradientActiveCaption;
            btnArchive.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnArchive.Image = Properties.Resources.icons8_archive_64;
            btnArchive.ImageAlign = ContentAlignment.MiddleLeft;
            btnArchive.Location = new Point(453, 0);
            btnArchive.Name = "btnArchive";
            btnArchive.Size = new Size(219, 68);
            btnArchive.TabIndex = 3;
            btnArchive.Text = "Archive Record";
            btnArchive.TextAlign = ContentAlignment.MiddleRight;
            btnArchive.UseVisualStyleBackColor = false;
            btnArchive.Click += btnArchive_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = SystemColors.GradientActiveCaption;
            btnDelete.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnDelete.ForeColor = Color.Black;
            btnDelete.Image = Properties.Resources.icons8_remove_64;
            btnDelete.ImageAlign = ContentAlignment.MiddleLeft;
            btnDelete.Location = new Point(678, 0);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(219, 68);
            btnDelete.TabIndex = 4;
            btnDelete.Text = "           Delete Record";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // lblTotalRecords
            // 
            lblTotalRecords.AutoSize = true;
            lblTotalRecords.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotalRecords.Location = new Point(1000, 19);
            lblTotalRecords.Name = "lblTotalRecords";
            lblTotalRecords.Size = new Size(198, 28);
            lblTotalRecords.TabIndex = 41;
            lblTotalRecords.Text = "Total Records: 00000";
            // 
            // button2
            // 
            button2.BackColor = SystemColors.GradientActiveCaption;
            button2.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button2.Image = Properties.Resources.icons8_receipt_64;
            button2.ImageAlign = ContentAlignment.MiddleLeft;
            button2.Location = new Point(228, 1);
            button2.Name = "button2";
            button2.Size = new Size(219, 68);
            button2.TabIndex = 2;
            button2.Text = "Generate Receipt";
            button2.TextAlign = ContentAlignment.MiddleRight;
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // btnPaymentHistory
            // 
            btnPaymentHistory.BackColor = SystemColors.GradientActiveCaption;
            btnPaymentHistory.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnPaymentHistory.Image = Properties.Resources.icons8_payment_history_64;
            btnPaymentHistory.ImageAlign = ContentAlignment.MiddleLeft;
            btnPaymentHistory.Location = new Point(3, 1);
            btnPaymentHistory.Name = "btnPaymentHistory";
            btnPaymentHistory.Size = new Size(219, 68);
            btnPaymentHistory.TabIndex = 1;
            btnPaymentHistory.Text = "Payment History";
            btnPaymentHistory.TextAlign = ContentAlignment.MiddleRight;
            btnPaymentHistory.UseVisualStyleBackColor = false;
            btnPaymentHistory.Click += btnPaymentHistory_Click;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button1.BackColor = SystemColors.GradientActiveCaption;
            button1.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.Image = Properties.Resources.icons8_menu_64;
            button1.ImageAlign = ContentAlignment.MiddleLeft;
            button1.Location = new Point(1684, 1);
            button1.Name = "button1";
            button1.Size = new Size(192, 68);
            button1.TabIndex = 7;
            button1.Text = "            Main Menu";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // btnExport
            // 
            btnExport.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExport.BackColor = SystemColors.GradientActiveCaption;
            btnExport.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnExport.Image = Properties.Resources.icons8_xls_64;
            btnExport.ImageAlign = ContentAlignment.MiddleLeft;
            btnExport.Location = new Point(1486, 1);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(192, 68);
            btnExport.TabIndex = 6;
            btnExport.Text = "          Export Report";
            btnExport.UseVisualStyleBackColor = false;
            btnExport.Click += btnExport_Click;
            // 
            // lblUsername
            // 
            lblUsername.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblUsername.Location = new Point(3, 594);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(101, 25);
            lblUsername.TabIndex = 34;
            lblUsername.Text = "Username ";
            lblUsername.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Controls.Add(panelButtons, 0, 0);
            tableLayoutPanel1.Controls.Add(dataGridView1, 0, 2);
            tableLayoutPanel1.Controls.Add(lblUsername, 0, 3);
            tableLayoutPanel1.Controls.Add(panelFilters, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 75F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(1885, 619);
            tableLayoutPanel1.TabIndex = 40;
            // 
            // ProfilingLists
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientActiveCaption;
            ClientSize = new Size(1885, 619);
            Controls.Add(tableLayoutPanel1);
            Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimumSize = new Size(1024, 600);
            Name = "ProfilingLists";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Masinloc BPLS - Stall Owners List";
            WindowState = FormWindowState.Maximized;
            Load += ProfilingLists_Load;
            panelFilters.ResumeLayout(false);
            panelFilters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panelButtons.ResumeLayout(false);
            panelButtons.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelFilters;
        private TextBox txtSearch;
        private DataGridView dataGridView1;
        private Panel panelButtons;
        private Button btnArchive;
        private Label lblUsername;
        private Button btnDelete;
        private Button button2;
        private Button btnPaymentHistory;
        private Button button1;
        private Button btnExport;
        private TableLayoutPanel tableLayoutPanel1;
        private Label lblTotalRecords;
        private Button btnMonthlyReport;
    }
}