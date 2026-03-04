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
            this.Text = "Payment History";
            this.Size = new Size(850, 500);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.White;
            this.MinimizeBox = false;
        }

        private void SetupLabels(string sin, string businessName)
        {
            // Title
            lblTitle = new Label
            {
                Text = $"Payment History — {businessName}",
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                Location = new Point(15, 15),
                AutoSize = true
            };
            this.Controls.Add(lblTitle);

            // SIN
            var lblSIN = new Label
            {
                Text = $"SIN: {sin}",
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
                Location = new Point(15, 75),
                Size = new Size(805, 330),
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

            dgvHistory.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 9, FontStyle.Bold);

            // Format currency columns
            dgvHistory.DataBindingComplete += (s, e) =>
            {
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
            // Calculate totals
            double totalPaid = 0;
            double totalPenalty = 0;

            foreach (DataRow row in history.Rows)
            {
                totalPaid += Convert.ToDouble(row["Amount Paid"]);
                totalPenalty += Convert.ToDouble(row["Penalty"]);
            }

            // Summary label
            lblSummary = new Label
            {
                Text =
                    $"Total Payments: {history.Rows.Count}     " +
                    $"Total Amount Paid: {totalPaid.ToString("C2", new CultureInfo("en-PH"))}     " +
                    $"Total Penalty: {totalPenalty.ToString("C2", new CultureInfo("en-PH"))}",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Location = new Point(15, 415),
                AutoSize = true
            };
            this.Controls.Add(lblSummary);
        }

        private void SetupButtons()
        {
            var btnClose = new Button
            {
                Text = "Close",
                Location = new Point(735, 410),
                Size = new Size(85, 32),
                BackColor = Color.IndianRed,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9)
            };
            btnClose.Click += (s, e) => this.Close();
            this.Controls.Add(btnClose);
        }
    }
}