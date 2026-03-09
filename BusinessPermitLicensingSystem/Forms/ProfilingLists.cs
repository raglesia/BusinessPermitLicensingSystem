using BusinessPermitLicensingSystem.Forms;
using BusinessPermitLicensingSystem.Models;
using ClosedXML.Excel;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace BusinessPermitLicensingSystem
{
    public partial class ProfilingLists : Form
    {
        // ===================== FIELDS ===================== //
        private DataTable dtProfiles = new DataTable();

        // ===================== CONSTRUCTOR ===================== //
        public ProfilingLists()
        {
            InitializeComponent();
            SetupGrid();
            SetupFilters();
            LoadProfiles();
        }

        // ===================== FORM LOAD ===================== //
        private void ProfilingLists_Load(object sender, EventArgs e)
        {
            lblUsername.Text = $"{Session.CurrentPosition} | {Session.CurrentFullName}";
            btnPaymentHistory.Focus();

        }

        // ===================== SETUP ===================== //
        private void SetupGrid()
        {
            // ✅ Enable double buffering first
            typeof(DataGridView)
                .GetProperty("DoubleBuffered",
                    System.Reflection.BindingFlags.Instance |
                    System.Reflection.BindingFlags.NonPublic)!
                .SetValue(dataGridView1, true);

            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.DataBindingComplete += (s, e) =>

            {
                dataGridView1.Columns["SIN"].DisplayIndex = 0;
                dataGridView1.Columns["Full Name"].DisplayIndex = 1;
                dataGridView1.Columns["Business Name"].DisplayIndex = 2;
                dataGridView1.Columns["Business Section"].DisplayIndex = 3;
                dataGridView1.Columns["Stall Number"].DisplayIndex = 4;
                dataGridView1.Columns["Stall Size"].DisplayIndex = 5;
                dataGridView1.Columns["Date of Occupancy"].DisplayIndex = 6;
                dataGridView1.Columns["Monthly Rental"].DisplayIndex = 7;
                dataGridView1.Columns["Penalty"].DisplayIndex = 8;
                dataGridView1.Columns["Additional Charge"].DisplayIndex = 9;
                dataGridView1.Columns["Payment Status"].DisplayIndex = 10;

                ColorPaymentStatusColumn();

            };
        }

        private void SetupFilters()
        {
            txtSearch.PlaceholderText = "🔍 Search Records: ";
            txtSearch.TextChanged += (s, e) => ApplyFilter();
        }

        // ===================== DATA LOADING ===================== //
        public void LoadProfiles()
        {
            dataGridView1.SuspendLayout();
            dataGridView1.DataSource = null;

            dtProfiles = Database.GetAllProfiles();
            dataGridView1.DataSource = dtProfiles;

            FormatCurrencyColumn("Monthly Rental");
            FormatCurrencyColumn("Penalty");
            FormatCurrencyColumn("Additional Charge");
            ColorPaymentStatusColumn();

            lblTotalRecords.Text = $"Total Records: {dtProfiles.Rows.Count}";

            dataGridView1.ResumeLayout();

        }

        private void FormatCurrencyColumn(string columnName)
        {
            var column = dataGridView1.Columns[columnName];
            if (column == null) return;

            column.DefaultCellStyle.Format = "C2";
            column.DefaultCellStyle.FormatProvider = new CultureInfo("en-PH");
        }

        // ===================== FILTERING ===================== //
        private void ApplyFilter()
        {
            try
            {
                string search = txtSearch.Text.Trim().Replace("'", "''");

                if (string.IsNullOrWhiteSpace(search))
                {
                    dtProfiles.DefaultView.RowFilter = string.Empty;
                    lblTotalRecords.Text = $"Total Records: {dtProfiles.Rows.Count}";
                    return;
                }

                // ✅ Payment Status uses exact match to differentiate Paid vs Unpaid
                dtProfiles.DefaultView.RowFilter = $@"
            Convert([SIN], 'System.String')            LIKE '%{search}%' OR
            [Full Name]                                LIKE '%{search}%' OR
            [Business Name]                            LIKE '%{search}%' OR
            [Business Section]                         LIKE '%{search}%' OR
            Convert([Stall Number], 'System.String')   LIKE '%{search}%' OR
            Convert([Stall Size], 'System.String')     LIKE '%{search}%' OR
            Convert([Monthly Rental], 'System.String') LIKE '%{search}%' OR
            [Payment Status]                           = '{search}'       OR
            Convert([Penalty], 'System.String')        LIKE '%{search}%' OR
            [Date of Occupancy]                        LIKE '%{search}%'";


            lblTotalRecords.Text = $"Total Records: {dtProfiles.DefaultView.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Filter error: {ex.Message}");
            }
        }

        private void AddFilter(List<string> filters, string column, string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
                filters.Add($"{column} LIKE '%{value.Replace("'", "''")}%'");
        }

        // ===================== EDIT RECORD ===================== //
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dataGridView1.Rows[e.RowIndex];

            string sin = row.Cells["SIN"].Value?.ToString() ?? "";
            string fullName = row.Cells["Full Name"].Value?.ToString() ?? "";
            string businessName = row.Cells["Business Name"].Value?.ToString() ?? "";
            string businessSection = row.Cells["Business Section"].Value?.ToString() ?? "";
            string stallNumber = row.Cells["Stall Number"].Value?.ToString() ?? "";
            string stallSize = row.Cells["Stall Size"].Value?.ToString() ?? "";
            string monthlyRental = row.Cells["Monthly Rental"].Value?.ToString() ?? "";
            string paymentStatus = row.Cells["Payment Status"].Value?.ToString() ?? "Unpaid";
            string startDate = row.Cells["Date of Occupancy"].Value?.ToString() ?? "";
            double additionalCharge = Convert.ToDouble(row.Cells["Additional Charge"].Value ?? 0); // ✅

            var form = new ProfilingForm();
            form.LoadForEdit(sin, fullName, businessName, businessSection,
                stallNumber, stallSize, monthlyRental, paymentStatus,
                startDate, additionalCharge); // ✅
            form.ShowDialog();

            LoadProfiles();
            HighlightRecord(sin);
        }

        // ===================== DELETE RECORD ===================== //
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Please select a record first.",
                    "No Selection",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var row = dataGridView1.SelectedRows[0];
            string sin = row.Cells["SIN"].Value?.ToString() ?? "";

            if (string.IsNullOrEmpty(sin))
            {
                MessageBox.Show(
                    "Selected record has no SIN.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            var confirm = MessageBox.Show(
                "Are you sure you want to delete this record?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            var result = Database.DeleteProfiling(sin);

            if (result.Success)
            {
                Database.LogAudit(
                    "Delete", sin,
                    Session.CurrentUserId ?? 0,
                    $"Deleted profile {sin}.");

                MessageBox.Show(
                    "Record deleted successfully.",
                    "Deleted",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LoadProfiles();
            }
            else
            {
                MessageBox.Show(
                    result.ErrorMessage,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ===================== GENERATE RECEIPT ===================== //
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a record first.");
                return;
            }

            var row = dataGridView1.SelectedRows[0];
            string paymentStatus = row.Cells["Payment Status"].Value?.ToString() ?? "Unknown";

            if (paymentStatus == "Paid")
            {
                MessageBox.Show(
                    "This stall owner has already paid. No receipt needed.",
                    "Already Paid",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            string startDate = row.Cells["Date of Occupancy"].Value?.ToString() ?? "";
            double monthlyRental = Convert.ToDouble(row.Cells["Monthly Rental"].Value);
            double penalty = Convert.ToDouble(row.Cells["Penalty"].Value ?? 0);

            var selectedProfile = new BillingReportModel
            {
                SIN = row.Cells["SIN"].Value.ToString()!,
                FullName = row.Cells["Full Name"].Value.ToString()!,
                BusinessName = row.Cells["Business Name"].Value.ToString()!,
                BusinessSection = row.Cells["Business Section"].Value.ToString()!,
                StallNumber = row.Cells["Stall Number"].Value.ToString()!,
                StallSize = row.Cells["Stall Size"].Value.ToString()!,
                MonthlyRental = monthlyRental,
                PaymentStatus = paymentStatus,
                Penalty = penalty,
                AdditionalCharge = Convert.ToDouble(row.Cells["Additional Charge"].Value ?? 0),
                StartDate = startDate,
            };

            var rpt = new ReportViewerForm(selectedProfile);
            rpt.ShowDialog();
        }

        // ===================== PAYMENT HISTORY ===================== //
        private void btnPaymentHistory_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Please select a record first.",
                    "No Selection",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var row = dataGridView1.SelectedRows[0];
            string sin = row.Cells["SIN"].Value?.ToString() ?? "";
            string businessName = row.Cells["Business Name"].Value?.ToString() ?? "";

            DataTable history = Database.GetPaymentHistory(sin);

            if (history.Rows.Count == 0)
            {
                MessageBox.Show(
                    $"No payment history found for {businessName}.",
                    "No Records",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            var historyForm = new PaymentHistoryForm(sin, businessName, history);
            historyForm.ShowDialog();
        }

        // ===================== EXPORT TO EXCEL ===================== //
        private async void btnExport_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show(
                    "No data to export.",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            using SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Workbook|*.xlsx";
            sfd.FileName = "Stall_Owners_Profiling.xlsx";

            if (sfd.ShowDialog() != DialogResult.OK) return;

            try
            {
                btnExport.Enabled = false;
                Cursor = Cursors.WaitCursor;

                var dt = GetDataTableFromDGV(dataGridView1);
                await Task.Run(() => ExportToExcel(dt, sfd.FileName));

                MessageBox.Show(
                    "Export completed successfully!",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error exporting file: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                btnExport.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private DataTable GetDataTableFromDGV(DataGridView dgv)
        {
            var dt = new DataTable();

            foreach (DataGridViewColumn col in dgv.Columns)
                dt.Columns.Add(col.HeaderText);

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (!row.IsNewRow)
                    dt.Rows.Add(row.Cells
                        .Cast<DataGridViewCell>()
                        .Select(c => c.Value?.ToString() ?? "")
                        .ToArray());
            }

            return dt;
        }

        private void ExportToExcel(DataTable dt, string filePath)
        {
            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Profiles");

            ws.Cell(1, 1).Value = "Stall Owners Profiling Report";
            ws.Range(1, 1, 1, dt.Columns.Count).Merge();
            ws.Cell(1, 1).Style.Font.Bold = true;
            ws.Cell(1, 1).Style.Font.FontSize = 16;

            ws.Cell(3, 1).InsertTable(dt);
            ws.Columns().AdjustToContents();

            workbook.SaveAs(filePath);
        }

        // ===================== NAVIGATION ===================== //
        private void button1_Click(object sender, EventArgs e)
        {
            DashboardForm dashboardForm = new DashboardForm();
            dashboardForm.Show();
            this.Hide();
        }

        // ===================== COLOR CODING ===================== //
        private void ColorPaymentStatusColumn()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;

                // ✅ Apply alternating row color to entire row first
                Color rowColor = row.Index % 2 == 0 ? Color.White : Color.AliceBlue;
                row.DefaultCellStyle.BackColor = rowColor;

                // ✅ Then override only the Payment Status cell
                var cell = row.Cells["Payment Status"];
                if (cell?.Value == null) continue;

                switch (cell.Value.ToString())
                {
                    case "Paid":
                        cell.Style.BackColor = Color.LightGreen;
                        cell.Style.ForeColor = Color.DarkGreen;
                        break;
                    case "Unpaid":
                        cell.Style.BackColor = Color.LightCoral;
                        cell.Style.ForeColor = Color.DarkRed;
                        break;
                }
            }
        }



        // ===================== WINDOW SETTINGS ===================== //
        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_NOCLOSE = 0x200;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_NOCLOSE;
                return cp;
            }
        }

        // ARCHIVE RECORD (SOFT DELETE) //
        private void btnArchive_Click(object sender, EventArgs e)
        {
            {
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show(
                        "Please select a record first.",
                        "No Selection",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                var row = dataGridView1.SelectedRows[0];
                string sin = row.Cells["SIN"].Value?.ToString() ?? "";
                string name = row.Cells["Full Name"].Value?.ToString() ?? "";

                var confirm = MessageBox.Show(
                    $"Archive record for {name}?\n\nArchived records are hidden from the list but kept in the database.",
                    "Confirm Archive",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirm != DialogResult.Yes) return;

                var result = Database.ArchiveProfiling(sin);

                if (result.Success)
                {
                    Database.LogAudit(
                        "Archive", sin,
                        Session.CurrentUserId ?? 0,
                        $"Archived profile for {name}.");

                    MessageBox.Show(
                        "Record archived successfully.",
                        "Archived",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    LoadProfiles();
                }
                else
                {
                    MessageBox.Show(
                        result.ErrorMessage,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }
        public void HighlightRecord(string sin)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["SIN"].Value?.ToString() == sin)
                {
                    dataGridView1.ClearSelection();
                    row.Selected = true;
                    dataGridView1.FirstDisplayedScrollingRowIndex = row.Index;
                    break;
                }
            }
        }
        public void HighlightLastAdded()
        {
            if (dataGridView1.Rows.Count == 0) return;

            dataGridView1.ClearSelection();
            dataGridView1.Rows[0].Selected = true;
            dataGridView1.FirstDisplayedScrollingRowIndex = 0;
        }
    }
}