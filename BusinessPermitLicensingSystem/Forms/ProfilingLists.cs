using BusinessPermitLicensingSystem.Forms;
using BusinessPermitLicensingSystem.Models;
using ClosedXML.Excel;
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
        }

        // ===================== SETUP ===================== //
        private void SetupGrid()
        {
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.DataBindingComplete += (s, e) => ColorPaymentStatusColumn();
            dataGridView1.RowHeadersVisible = false;

            {
                ColorPaymentStatusColumn();
                AlignFiltersToColumns();

            };
            dataGridView1.ColumnWidthChanged += (s, e) => AlignFiltersToColumns(); // ✅
        }

        private void SetupFilters()
        {
            txtFilterBIN.TextChanged += (s, e) => ApplyFilter();
            txtFilterFullName.TextChanged += (s, e) => ApplyFilter();
            txtFilterBusinessName.TextChanged += (s, e) => ApplyFilter();
            txtFilterBusinessSection.TextChanged += (s, e) => ApplyFilter();
            txtFilterStallNumber.TextChanged += (s, e) => ApplyFilter();
            txtFilterStallSize.TextChanged += (s, e) => ApplyFilter();
            txtFilterMonthlyRental.TextChanged += (s, e) => ApplyFilter();
            txtFilterPayment.TextChanged += (s, e) => ApplyFilter(); 
            txtFilterPenalty.TextChanged += (s, e) => ApplyFilter(); 
            txtFilterDOC.TextChanged += (s, e) => ApplyFilter(); 
        }

        // ===================== DATA LOADING ===================== //
        private void LoadProfiles()
        {
            dtProfiles = Database.GetAllProfiles();
            dataGridView1.DataSource = dtProfiles;

            FormatCurrencyColumn("Monthly Rental");
            FormatCurrencyColumn("Penalty");
            ColorPaymentStatusColumn();

            AlignFiltersToColumns(); // ✅
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
                var filters = new List<string>();

                AddFilter(filters, "Convert([SIN], 'System.String')", txtFilterBIN.Text);
                AddFilter(filters, "[Full Name]", txtFilterFullName.Text);
                AddFilter(filters, "[Business Name]", txtFilterBusinessName.Text);
                AddFilter(filters, "[Business Section]", txtFilterBusinessSection.Text);
                AddFilter(filters, "Convert([Stall Number], 'System.String')", txtFilterStallNumber.Text);
                AddFilter(filters, "Convert([Stall Size], 'System.String')", txtFilterStallSize.Text);
                AddFilter(filters, "Convert([Monthly Rental], 'System.String')", txtFilterMonthlyRental.Text);
                AddFilter(filters, "[Payment Status]", txtFilterPayment.Text);  
                AddFilter(filters, "Convert([Penalty], 'System.String')", txtFilterPenalty.Text);  
                AddFilter(filters, "[Date of Occupancy]", txtFilterDOC.Text);     


                dtProfiles.DefaultView.RowFilter = string.Join(" AND ", filters);
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

        // ===================== COLOR CODING ===================== //
        private void ColorPaymentStatusColumn()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;

                var cell = row.Cells["Payment Status"];
                if (cell?.Value == null) continue;

                switch (cell.Value.ToString())
                {
                    case "Paid":
                        cell.Style.BackColor = Color.LightGreen;
                        cell.Style.ForeColor = Color.DarkGreen;
                        break;
                    case "Partial":
                        cell.Style.BackColor = Color.LightYellow;
                        cell.Style.ForeColor = Color.DarkOrange;
                        break;
                    case "Unpaid":
                        cell.Style.BackColor = Color.LightCoral;
                        cell.Style.ForeColor = Color.DarkRed;
                        break;
                }
            }
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

            var form = new ProfilingForm();
            form.LoadForEdit(sin, fullName, businessName, businessSection,
                stallNumber, stallSize, monthlyRental, paymentStatus, startDate);
            form.ShowDialog();

            LoadProfiles();
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
                SIN = row.Cells["SIN"].Value.ToString(),
                FullName = row.Cells["Full Name"].Value.ToString(),
                BusinessName = row.Cells["Business Name"].Value.ToString(),
                BusinessSection = row.Cells["Business Section"].Value.ToString(),
                StallNumber = row.Cells["Stall Number"].Value.ToString(),
                StallSize = row.Cells["Stall Size"].Value.ToString(),
                MonthlyRental = monthlyRental,
                PaymentStatus = paymentStatus,
                Penalty = penalty,
                StartDate = startDate
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

        // ===================== RESIZE ===================== //
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            AlignFiltersToColumns(); // ✅
        }

        // ===================== HELPERS ===================== //
        private void AlignFiltersToColumns()
        {
            if (dataGridView1.Columns.Count == 0) return;

            int xOffset = dataGridView1.Left + dataGridView1.RowHeadersWidth;

            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                TextBox filter = col.HeaderText switch
                {
                    "SIN" => txtFilterBIN,
                    "Full Name" => txtFilterFullName,
                    "Business Name" => txtFilterBusinessName,
                    "Business Section" => txtFilterBusinessSection,
                    "Stall Number" => txtFilterStallNumber,
                    "Stall Size" => txtFilterStallSize,
                    "Monthly Rental" => txtFilterMonthlyRental,
                    "Payment Status" => txtFilterPayment,
                    "Penalty" => txtFilterPenalty,
                    "Date of Occupancy" => txtFilterDOC,

                    _ => null
                };

                if (filter == null) continue;

                // ✅ Use GetColumnDisplayRectangle instead of ContentBounds
                Rectangle rect = dataGridView1.GetColumnDisplayRectangle(col.Index, true);

                if (rect.Width == 0) continue; // Column not visible yet

                filter.Left = dataGridView1.Left + rect.Left;
                filter.Width = rect.Width;
                filter.Visible = true;
            }
        }
        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_NOCLOSE = 0x200;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_NOCLOSE; // ✅ Disables X button
                return cp;
            }
        }

        // ===================== UNUSED EVENTS ===================== //
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}