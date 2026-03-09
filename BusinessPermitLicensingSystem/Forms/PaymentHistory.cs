using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace BusinessPermitLicensingSystem.Forms
{
    public partial class PaymentHistoryForm : Form
    {
        // ===================== CONTROLS ===================== //
        private DataGridView dgvHistory;
        private Label lblTitle;
        private Label lblSummary;

        // ===================== CONSTRUCTOR ===================== //
        public PaymentHistoryForm(string sin, string businessName, DataTable history)
        {
            SetupUI(sin, businessName, history);
        }

        // ===================== UI SETUP ===================== //
        private void SetupUI(string sin, string businessName, DataTable history)
        {
            SetupForm();
            SetupLabels(sin, businessName);
            SetupGrid(history);
            SetupSummary(history);
            SetupButtons();
        }

        private void SetupForm()
        {
            this.Text = "Masinloc - BPLS";
            this.Size = new Size(1619, 610);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.White;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Font = new Font("Segoe UI", 10);
            this.Icon = new Icon(Path.Combine(
                Application.StartupPath, "Resources", "Masinloc-Logo-HD.ico"));
        }

        private void SetupLabels(string sin, string businessName)
        {
            lblTitle = new Label
            {
                Text = $"Payment History — {businessName}",
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                Location = new Point(15, 15),
                AutoSize = true
            };
            this.Controls.Add(lblTitle);

            var lblSIN = new Label
            {
                Text = $"{sin}",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray,
                Location = new Point(15, 45),
                AutoSize = true
            };
            this.Controls.Add(lblSIN);
        }

        private void SetupGrid(DataTable history)
        {
            dgvHistory = new DataGridView
            {
                // ✅ Stretch to fill form width
                Location = new Point(15, 75),
                Size = new Size(1580, 430),
                Anchor = AnchorStyles.Top | AnchorStyles.Left |
                                        AnchorStyles.Right | AnchorStyles.Bottom,
                DataSource = history,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                Font = new Font("Segoe UI", 9)
            };

            // ✅ Double buffering
            typeof(DataGridView)
                .GetProperty("DoubleBuffered",
                    System.Reflection.BindingFlags.Instance |
                    System.Reflection.BindingFlags.NonPublic)!
                .SetValue(dgvHistory, true);

            dgvHistory.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 9, FontStyle.Bold);

            // ✅ Alternating row colors
            dgvHistory.RowsDefaultCellStyle.BackColor = Color.White;
            dgvHistory.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;

            // ✅ Format currency + hide # column
            dgvHistory.DataBindingComplete += (s, e) =>
            {
                // Hide # (ID) column
                if (dgvHistory.Columns["#"] != null)
                    dgvHistory.Columns["#"].Visible = false;

                // Format currency columns
                foreach (string col in new[] { "Amount Paid", "Penalty", "Total Paid" })
                {
                    if (dgvHistory.Columns[col] != null)
                    {
                        dgvHistory.Columns[col].DefaultCellStyle.Format = "C2";
                        dgvHistory.Columns[col].DefaultCellStyle.FormatProvider =
                            new CultureInfo("en-PH");
                    }
                }
            };

            this.Controls.Add(dgvHistory);
        }

        private void SetupSummary(DataTable history)
        {
            double totalPaid = 0;
            double totalPenalty = 0;

            foreach (DataRow row in history.Rows)
            {
                totalPaid += Convert.ToDouble(row["Amount Paid"]);
                totalPenalty += Convert.ToDouble(row["Penalty"]);
            }

            lblSummary = new Label
            {
                Text = $"Total Payments: {history.Rows.Count}     " +
                           $"Total Amount Paid: {totalPaid.ToString("C2", new CultureInfo("en-PH"))}     " +
                           $"Total Penalty: {totalPenalty.ToString("C2", new CultureInfo("en-PH"))}",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Location = new Point(15, 520),
                AutoSize = true
            };
            this.Controls.Add(lblSummary);
        }

        private void SetupButtons()
        {
            var btnClose = new Button
            {
                Text = "Close",
                Location = new Point(1509, 515),
                Size = new Size(85, 32),
                BackColor = Color.IndianRed,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9),
                Cursor = Cursors.Hand
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, e) => this.Close();
            this.Controls.Add(btnClose);
        }

        private void InitializeComponent()
        {

        }
    }
}