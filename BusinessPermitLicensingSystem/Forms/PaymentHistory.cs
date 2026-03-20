using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace BusinessPermitLicensingSystem.Forms
{
    public partial class PaymentHistoryForm : Form
    {
        private void InitializeComponent() { }

        // ===================== FIELDS ===================== //
        private DataGridView dgvHistory;
        private Label lblTitle;
        private Label lblSummary;

        private static readonly CultureInfo PhCulture = new CultureInfo("en-PH");

        // ===================== CONSTRUCTOR ===================== //
        public PaymentHistoryForm(string sin, string fullName, string businessName, DataTable history)
        {
            SetupUI(sin, fullName, businessName, history);
        }

        // ===================== UI SETUP ===================== //
        private void SetupUI(string sin, string fullName, string businessName, DataTable history)
        {
            SetupForm();
            SetupLabels(sin, fullName, businessName);
            SetupGrid(history);
            SetupSummary(history);
            SetupButtons();
        }

        private void SetupForm()
        {
            Text = "Masinloc - BPLS";
            Size = new Size(1650, 610);
            StartPosition = FormStartPosition.CenterParent;
            BackColor = Color.White;
            MinimizeBox = false;
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Font = new Font("Segoe UI", 10);
            Icon = new Icon(Path.Combine(
                Application.StartupPath, "Resources", "MasinlocLogoIcon.ico"));
        }

        private void SetupLabels(string sin, string fullName, string businessName)
        {
            lblTitle = new Label
            {
                Text = $"Payment History — {businessName}",
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                Location = new Point(15, 15),
                AutoSize = true
            };
            Controls.Add(lblTitle);

            Controls.Add(new Label
            {
                Text = $"{fullName}  |  {sin}",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray,
                Location = new Point(15, 45),
                AutoSize = true
            });
        }

        private void SetupGrid(DataTable history)
        {
            // Grid: starts at y=70, height=380 → ends at y=450
            dgvHistory = new DataGridView
            {
                Location = new Point(15, 70),
                Size = new Size(1580, 430),
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

            typeof(DataGridView)
                .GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic)!
                .SetValue(dgvHistory, true);

            dgvHistory.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dgvHistory.RowsDefaultCellStyle.BackColor = Color.White;
            dgvHistory.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;

            dgvHistory.DataBindingComplete += (s, e) =>
            {
                if (dgvHistory.Columns["#"] != null)
                    dgvHistory.Columns["#"].Visible = false;

                foreach (string col in new[] { "Monthly Rental", "Penalty", "Amount Paid" })
                {
                    if (dgvHistory.Columns[col] == null) continue;
                    dgvHistory.Columns[col].DefaultCellStyle.Format = "C2";
                    dgvHistory.Columns[col].DefaultCellStyle.FormatProvider = PhCulture;
                }

                foreach (DataGridViewColumn col in dgvHistory.Columns)
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;
            };

            Controls.Add(dgvHistory);
        }

        private void SetupSummary(DataTable history)
        {
            decimal totalAmountPaid = 0;
            decimal totalPenalty = 0;

            foreach (DataRow row in history.Rows)
            {
                totalAmountPaid += Convert.ToDecimal(row["Amount Paid"]);
                totalPenalty += Convert.ToDecimal(row["Penalty"]);
            }

            lblSummary = new Label
            {
                Text = $"Total Payments: {history.Rows.Count}     " +
                           $"Total Amount Paid: {totalAmountPaid.ToString("C2", PhCulture)}     " +
                           $"Total Penalty: {totalPenalty.ToString("C2", PhCulture)}",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Location = new Point(10, 515),
                AutoSize = true
            };
            Controls.Add(lblSummary);
        }

        private void SetupButtons()
        {
            // Close button: y=458 (aligned with summary)
            var btnClose = new Button
            {
                Text = "Close",
                Location = new Point(1540, 510),
                Size = new Size(85, 32),
                BackColor = Color.IndianRed,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9),
                Cursor = Cursors.Hand
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, e) => this.Close();
            Controls.Add(btnClose);
        }
    }
}