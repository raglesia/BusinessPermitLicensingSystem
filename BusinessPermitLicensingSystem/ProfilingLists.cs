using BusinessPermitLicensingSystem.Forms;
using BusinessPermitLicensingSystem.Models;
using ClosedXML.Excel;
using System;
using System.Data;
using System.Reflection.Metadata;
using System.Windows.Forms;


namespace BusinessPermitLicensingSystem
{
    public partial class ProfilingLists : Form
    {
        private DataTable dtProfiles = new DataTable();

        public ProfilingLists()
        {
            InitializeComponent();

            // Attach filters
            txtFilterBIN.TextChanged += (s, e) => ApplyFilter();
            txtFilterFullName.TextChanged += (s, e) => ApplyFilter();
            txtFilterBusinessName.TextChanged += (s, e) => ApplyFilter();
            txtFilterBusinessSection.TextChanged += (s, e) => ApplyFilter();
            txtFilterStallNumber.TextChanged += (s, e) => ApplyFilter();
            txtFilterStallSize.TextChanged += (s, e) => ApplyFilter();
            txtFilterMonthlyRental.TextChanged += (s, e) => ApplyFilter();

            dataGridView1.DataBindingComplete += (s, e) => ColorPaymentStatusColumn();

            // Configure DataGridView
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            LoadProfiles();
        }

        private void LoadProfiles()
        {
            dtProfiles = Database.GetAllProfiles(); // your method returning a DataTable
            dataGridView1.DataSource = dtProfiles; // bind once

            var rentalColumn = dataGridView1.Columns["Monthly Rental"];

            if (rentalColumn != null)
            {
                rentalColumn.DefaultCellStyle.Format = "C2";
                rentalColumn.DefaultCellStyle.FormatProvider =
                    new System.Globalization.CultureInfo("en-PH");
            }

            ColorPaymentStatusColumn();
        }

        private void ApplyFilter()
        {
            try
            {
                DataView dv = dtProfiles.DefaultView;
                List<string> filters = new List<string>();

                if (!string.IsNullOrWhiteSpace(txtFilterBIN.Text))
                    filters.Add($"Convert([SIN], 'System.String') LIKE '%{txtFilterBIN.Text.Replace("'", "''")}%'");

                if (!string.IsNullOrWhiteSpace(txtFilterFullName.Text))
                    filters.Add($"[Full Name] LIKE '%{txtFilterFullName.Text.Replace("'", "''")}%'");

                if (!string.IsNullOrWhiteSpace(txtFilterBusinessName.Text))
                    filters.Add($"[Business Name] LIKE '%{txtFilterBusinessName.Text.Replace("'", "''")}%'");

                if (!string.IsNullOrWhiteSpace(txtFilterBusinessSection.Text))
                    filters.Add($"[Business Section] LIKE '%{txtFilterBusinessSection.Text.Replace("'", "''")}%'");

                if (!string.IsNullOrWhiteSpace(txtFilterStallNumber.Text))
                    filters.Add($"Convert([Stall Number], 'System.String') LIKE '%{txtFilterStallNumber.Text.Replace("'", "''")}%'");

                if (!string.IsNullOrWhiteSpace(txtFilterStallSize.Text))
                    filters.Add($"Convert([Stall Size], 'System.String') LIKE '%{txtFilterStallSize.Text.Replace("'", "''")}%'");

                if (!string.IsNullOrWhiteSpace(txtFilterMonthlyRental.Text))
                    filters.Add($"Convert([Monthly Rental], 'System.String') LIKE '%{txtFilterMonthlyRental.Text.Replace("'", "''")}%'");

                dv.RowFilter = string.Join(" AND ", filters);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Filter error: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DashboardForm dashboardForm = new DashboardForm();

            dashboardForm.Show();
            this.Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            {
                if (e.RowIndex < 0) return;

                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                string sin = row.Cells["SIN"].Value?.ToString() ?? "";
                string fullName = row.Cells["Full Name"].Value?.ToString() ?? "";
                string businessName = row.Cells["Business Name"].Value?.ToString() ?? "";
                string businessSection = row.Cells["Business Section"].Value?.ToString() ?? "";
                string stallNumber = row.Cells["Stall Number"].Value?.ToString() ?? "";
                string stallSize = row.Cells["Stall Size"].Value?.ToString() ?? "";
                string monthlyRental = row.Cells["Monthly Rental"].Value?.ToString() ?? "";
                string paymentStatus = row.Cells["Payment Status"].Value?.ToString() ?? "Unpaid";

                var form = new Forms.ProfilingForm();

                form.LoadForEdit(
                    sin,
                    fullName,
                    businessName,
                    businessSection,
                    stallNumber,
                    stallSize,
                    monthlyRental,
                    paymentStatus
                );

                form.ShowDialog();

                // refresh list after edit
                LoadProfiles();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a record first.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var row = dataGridView1.SelectedRows[0];
            string sin = row.Cells["SIN"].Value?.ToString() ?? "";

            if (string.IsNullOrEmpty(sin))
            {
                MessageBox.Show("Selected record has no SIN.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var confirm = MessageBox.Show(
                "Are you sure you want to delete this record?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm != DialogResult.Yes) return;

            var result = Database.DeleteProfiling(sin);
            if (result.Success)
            {
                long currentUserId = Session.CurrentUserId ?? 0;
                Database.LogAudit("Delete", sin, currentUserId, $"Deleted Profile {sin}");

                MessageBox.Show("Record deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadProfiles(); // refresh grid
            }
            else
            {
                MessageBox.Show(result.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnExport_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("No data to export.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Workbook|*.xlsx";
            sfd.FileName = "Stall_Owners_Profiling.xlsx";

            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                btnExport.Enabled = false;
                Cursor = Cursors.WaitCursor;

                var dt = GetDataTableFromDGV(dataGridView1);

                // ✅ RUN HEAVY WORK IN BACKGROUND
                await Task.Run(() =>
                {
                    ExportToExcel(dt, sfd.FileName);
                });

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


        // Convert DataGridView to DataTable
        private DataTable GetDataTableFromDGV(DataGridView dgv)
        {
            DataTable dt = new DataTable();
            foreach (DataGridViewColumn col in dgv.Columns)
                dt.Columns.Add(col.HeaderText);

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (!row.IsNewRow)
                    dt.Rows.Add(row.Cells.Cast<DataGridViewCell>().Select(c => c.Value?.ToString() ?? "").ToArray());
            }
            return dt;
        }

        // Export to Excel
        private void ExportToExcel(DataTable dt, string filePath)
        {
            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Profiles");

            // Add title
            ws.Cell(1, 1).Value = "Stall Owners Profiling Report";
            ws.Range(1, 1, 1, dt.Columns.Count).Merge();
            ws.Cell(1, 1).Style.Font.Bold = true;
            ws.Cell(1, 1).Style.Font.FontSize = 16;

            // Insert table starting from row 3
            ws.Cell(3, 1).InsertTable(dt);

            // Auto-fit columns
            ws.Columns().AdjustToContents();

            workbook.SaveAs(filePath);
        }

        private void ProfilingLists_Load(object sender, EventArgs e)
        {
            lblUsername.Text = $"Admin : {Session.CurrentFullName ?? "Unknown"}";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a record first.");
                return;
            }

            var row = dataGridView1.SelectedRows[0];

            string paymentStatus = row.Cells["Payment Status"].Value?.ToString() ?? "Unknown";

            BillingReportModel selectedProfile =
                new BillingReportModel
                {
                    SIN = row.Cells["SIN"].Value.ToString(),
                    FullName = row.Cells["Full Name"].Value.ToString(),
                    BusinessName = row.Cells["Business Name"].Value.ToString(),
                    BusinessSection = row.Cells["Business Section"].Value.ToString(),
                    StallNumber = row.Cells["Stall Number"].Value.ToString(),
                    StallSize = row.Cells["Stall Size"].Value.ToString(),
                    MonthlyRental = Convert.ToDouble(row.Cells["Monthly Rental"].Value),
                    PaymentStatus = paymentStatus
                };

            ReportViewerForm rpt =
                new ReportViewerForm(selectedProfile);

            rpt.ShowDialog();
        }

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
    }
}