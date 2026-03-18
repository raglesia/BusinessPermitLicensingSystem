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
        // ===================== FIELDS ===================== //
        private DataGridView dgvHistory;
        private Label lblTitle;
        private Label lblSummary;

        private static readonly CultureInfo PhCulture = new CultureInfo("en-PH");

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
            Text = "Masinloc - BPLS";
            Size = new Size(1619, 610);
            StartPosition = FormStartPosition.CenterParent;
            BackColor = Color.White;
            MinimizeBox = false;
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Font = new Font("Segoe UI", 10);
            Icon = new Icon(Path.Combine(
                Application.StartupPath, "Resources", "MasinlocLogoIcon.ico"));
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
            Controls.Add(lblTitle);

            var lblSIN = new Label
            {
                Text = sin,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray,
                Location = new Point(15, 45),
                AutoSize = true
            };
            Controls.Add(lblSIN);
        }

        private void SetupGrid(DataTable history)
        {
            dgvHistory = new DataGridView
            {
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

                foreach (string col in new[] { "Amount Paid", "Penalty", "Total Paid" })
                {
                    if (dgvHistory.Columns[col] == null) continue;
                    dgvHistory.Columns[col].DefaultCellStyle.Format = "C2";
                    dgvHistory.Columns[col].DefaultCellStyle.FormatProvider = PhCulture;
                }
            };

            Controls.Add(dgvHistory);
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
                       $"Total Amount Paid: {totalPaid.ToString("C2", PhCulture)}     " +
                       $"Total Penalty: {totalPenalty.ToString("C2", PhCulture)}",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Location = new Point(15, 520),
                AutoSize = true
            };
            Controls.Add(lblSummary);
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
            btnClose.Click += (s, e) => Close();
            Controls.Add(btnClose);
        }

        private void InitializeComponent() { }
    }
}