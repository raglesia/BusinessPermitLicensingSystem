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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProfilingLists));
            txtFilterMonthlyRental = new TextBox();
            txtFilterStallSize = new TextBox();
            txtFilterStallNumber = new TextBox();
            txtFilterBusinessSection = new TextBox();
            txtFilterBusinessName = new TextBox();
            txtFilterFullName = new TextBox();
            txtFilterBIN = new TextBox();
            button1 = new Button();
            btnDelete = new Button();
            btnExport = new Button();
            lblUsername = new Label();
            button2 = new Button();
            btnPaymentHistory = new Button();
            panelButtons = new Panel();
            panelFilters = new Panel();
            txtFilterDOC = new TextBox();
            txtFilterPenalty = new TextBox();
            txtFilterPayment = new TextBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            dataGridView1 = new DataGridView();
            panelButtons.SuspendLayout();
            panelFilters.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // txtFilterMonthlyRental
            // 
            txtFilterMonthlyRental.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtFilterMonthlyRental.Location = new Point(1297, 3);
            txtFilterMonthlyRental.Name = "txtFilterMonthlyRental";
            txtFilterMonthlyRental.Size = new Size(125, 31);
            txtFilterMonthlyRental.TabIndex = 30;
            // 
            // txtFilterStallSize
            // 
            txtFilterStallSize.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtFilterStallSize.Location = new Point(1162, 3);
            txtFilterStallSize.Name = "txtFilterStallSize";
            txtFilterStallSize.Size = new Size(129, 31);
            txtFilterStallSize.TabIndex = 29;
            // 
            // txtFilterStallNumber
            // 
            txtFilterStallNumber.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtFilterStallNumber.Location = new Point(947, 3);
            txtFilterStallNumber.Name = "txtFilterStallNumber";
            txtFilterStallNumber.Size = new Size(209, 31);
            txtFilterStallNumber.TabIndex = 28;
            // 
            // txtFilterBusinessSection
            // 
            txtFilterBusinessSection.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtFilterBusinessSection.Location = new Point(722, 3);
            txtFilterBusinessSection.Name = "txtFilterBusinessSection";
            txtFilterBusinessSection.Size = new Size(219, 31);
            txtFilterBusinessSection.TabIndex = 27;
            // 
            // txtFilterBusinessName
            // 
            txtFilterBusinessName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtFilterBusinessName.Location = new Point(469, 3);
            txtFilterBusinessName.Name = "txtFilterBusinessName";
            txtFilterBusinessName.Size = new Size(247, 31);
            txtFilterBusinessName.TabIndex = 26;
            // 
            // txtFilterFullName
            // 
            txtFilterFullName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtFilterFullName.Location = new Point(276, 3);
            txtFilterFullName.Name = "txtFilterFullName";
            txtFilterFullName.Size = new Size(187, 31);
            txtFilterFullName.TabIndex = 25;
            // 
            // txtFilterBIN
            // 
            txtFilterBIN.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtFilterBIN.Location = new Point(3, 3);
            txtFilterBIN.Name = "txtFilterBIN";
            txtFilterBIN.Size = new Size(267, 31);
            txtFilterBIN.TabIndex = 24;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button1.BackColor = SystemColors.GradientActiveCaption;
            button1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.Image = Properties.Resources.icons8_menu_64;
            button1.ImageAlign = ContentAlignment.MiddleLeft;
            button1.Location = new Point(1657, 3);
            button1.Name = "button1";
            button1.Size = new Size(219, 62);
            button1.TabIndex = 31;
            button1.Text = "         Main Menu";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = SystemColors.GradientActiveCaption;
            btnDelete.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnDelete.ForeColor = Color.Black;
            btnDelete.Image = Properties.Resources.icons8_remove_64;
            btnDelete.ImageAlign = ContentAlignment.MiddleLeft;
            btnDelete.Location = new Point(453, 3);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(219, 68);
            btnDelete.TabIndex = 32;
            btnDelete.Text = "           Delete Record";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnExport
            // 
            btnExport.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExport.BackColor = SystemColors.GradientActiveCaption;
            btnExport.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnExport.Image = Properties.Resources.icons8_xls_64;
            btnExport.ImageAlign = ContentAlignment.MiddleLeft;
            btnExport.Location = new Point(1432, 3);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(219, 64);
            btnExport.TabIndex = 33;
            btnExport.Text = "         Export Report";
            btnExport.UseVisualStyleBackColor = false;
            btnExport.Click += btnExport_Click;
            // 
            // lblUsername
            // 
            lblUsername.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(3, 594);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(91, 25);
            lblUsername.TabIndex = 34;
            lblUsername.Text = "Username";
            // 
            // button2
            // 
            button2.BackColor = SystemColors.GradientActiveCaption;
            button2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button2.Image = Properties.Resources.icons8_receipt_64;
            button2.ImageAlign = ContentAlignment.MiddleLeft;
            button2.Location = new Point(228, 3);
            button2.Name = "button2";
            button2.Size = new Size(219, 68);
            button2.TabIndex = 35;
            button2.Text = "Generate Receipt";
            button2.TextAlign = ContentAlignment.MiddleRight;
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // btnPaymentHistory
            // 
            btnPaymentHistory.BackColor = SystemColors.GradientActiveCaption;
            btnPaymentHistory.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnPaymentHistory.Image = Properties.Resources.icons8_payment_history_64;
            btnPaymentHistory.ImageAlign = ContentAlignment.MiddleLeft;
            btnPaymentHistory.Location = new Point(3, 3);
            btnPaymentHistory.Name = "btnPaymentHistory";
            btnPaymentHistory.Size = new Size(219, 64);
            btnPaymentHistory.TabIndex = 36;
            btnPaymentHistory.Text = "Payment History";
            btnPaymentHistory.TextAlign = ContentAlignment.MiddleRight;
            btnPaymentHistory.UseVisualStyleBackColor = false;
            btnPaymentHistory.Click += btnPaymentHistory_Click;
            // 
            // panelButtons
            // 
            panelButtons.Controls.Add(button1);
            panelButtons.Controls.Add(btnExport);
            panelButtons.Controls.Add(btnPaymentHistory);
            panelButtons.Controls.Add(btnDelete);
            panelButtons.Controls.Add(button2);
            panelButtons.Dock = DockStyle.Fill;
            panelButtons.Location = new Point(3, 3);
            panelButtons.Name = "panelButtons";
            panelButtons.Size = new Size(1879, 69);
            panelButtons.TabIndex = 38;
            // 
            // panelFilters
            // 
            panelFilters.Controls.Add(txtFilterDOC);
            panelFilters.Controls.Add(txtFilterBIN);
            panelFilters.Controls.Add(txtFilterPenalty);
            panelFilters.Controls.Add(txtFilterFullName);
            panelFilters.Controls.Add(txtFilterPayment);
            panelFilters.Controls.Add(txtFilterBusinessName);
            panelFilters.Controls.Add(txtFilterBusinessSection);
            panelFilters.Controls.Add(txtFilterMonthlyRental);
            panelFilters.Controls.Add(txtFilterStallNumber);
            panelFilters.Controls.Add(txtFilterStallSize);
            panelFilters.Dock = DockStyle.Fill;
            panelFilters.Location = new Point(3, 78);
            panelFilters.Name = "panelFilters";
            panelFilters.Size = new Size(1879, 39);
            panelFilters.TabIndex = 39;
            // 
            // txtFilterDOC
            // 
            txtFilterDOC.Location = new Point(1744, 3);
            txtFilterDOC.Name = "txtFilterDOC";
            txtFilterDOC.Size = new Size(126, 31);
            txtFilterDOC.TabIndex = 39;
            // 
            // txtFilterPenalty
            // 
            txtFilterPenalty.Location = new Point(1588, 3);
            txtFilterPenalty.Name = "txtFilterPenalty";
            txtFilterPenalty.Size = new Size(150, 31);
            txtFilterPenalty.TabIndex = 38;
            // 
            // txtFilterPayment
            // 
            txtFilterPayment.Location = new Point(1432, 3);
            txtFilterPayment.Name = "txtFilterPayment";
            txtFilterPayment.Size = new Size(150, 31);
            txtFilterPayment.TabIndex = 37;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(panelButtons, 0, 0);
            tableLayoutPanel1.Controls.Add(lblUsername, 0, 3);
            tableLayoutPanel1.Controls.Add(dataGridView1, 0, 2);
            tableLayoutPanel1.Controls.Add(panelFilters, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 75F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tableLayoutPanel1.Size = new Size(1885, 619);
            tableLayoutPanel1.TabIndex = 40;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(3, 123);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.Size = new Size(1879, 458);
            dataGridView1.TabIndex = 23;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick;
            // 
            // ProfilingLists
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1885, 619);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimumSize = new Size(1024, 600);
            Name = "ProfilingLists";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Masinloc - BPLS";
            WindowState = FormWindowState.Maximized;
            Load += ProfilingLists_Load;
            panelButtons.ResumeLayout(false);
            panelFilters.ResumeLayout(false);
            panelFilters.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TextBox txtFilterMonthlyRental;
        private TextBox txtFilterStallSize;
        private TextBox txtFilterStallNumber;
        private TextBox txtFilterBusinessSection;
        private TextBox txtFilterBusinessName;
        private TextBox txtFilterFullName;
        private TextBox txtFilterBIN;
        private Button button1;
        private Button btnDelete;
        private Button btnExport;
        private Label lblUsername;
        private Button button2;
        private Button btnPaymentHistory;
        private Panel panelButtons;
        private Panel panelFilters;
        private TableLayoutPanel tableLayoutPanel1;
        private DataGridView dataGridView1;
        private TextBox txtFilterDOC;
        private TextBox txtFilterPenalty;
        private TextBox txtFilterPayment;
    }
}