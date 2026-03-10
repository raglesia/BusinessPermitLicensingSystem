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
        private bool _sortAscending = true;  // ✅ Track sort direction
        private int _lastSortedCol = -1;     // ✅ Track last sorted column

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

                // ✅ Disable built-in sorting — we handle it manually
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                    col.SortMode = DataGridViewColumnSortMode.Programmatic;

                ColorPaymentStatusColumn();
            };

            // ✅ Custom sort on header click
            dataGridView1.ColumnHeaderMouseClick += DataGridView1_ColumnHeaderMouseClick;
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

            // ✅ Disable built-in sorting after DataSource is set
            foreach (DataGridViewColumn col in dataGridView1.Columns)
                col.SortMode = DataGridViewColumnSortMode.Programmatic;

            FormatCurrencyColumn("Monthly Rental");
            FormatCurrencyColumn("Penalty");
            FormatCurrencyColumn("Additional Charge");
            ColorPaymentStatusColumn();

            lblTotalRecords.Text = $"Total Records: {dtProfiles.Rows.Count}";

            // ✅ Reset sort trackers
            _sortAscending = true;
            _lastSortedCol = -1;

            dataGridView1.ResumeLayout();
        }

        private void FormatCurrencyColumn(string columnName)
        {
            var column = dataGridView1.Columns[columnName];
            if (column == null) return;

            column.DefaultCellStyle.Format = "C2";
            column.DefaultCellStyle.FormatProvider = new CultureInfo("en-PH");
        }

        // ===================== SORTING ===================== //
        private void DataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string headerText = dataGridView1.Columns[e.ColumnIndex].HeaderText;

            if (_lastSortedCol == e.ColumnIndex)
                _sortAscending = !_sortAscending;
            else
            {
                _sortAscending = true;
                _lastSortedCol = e.ColumnIndex;
            }

            string sortDir = _sortAscending ? "ASC" : "DESC";

            // ✅ Use database query to sort SIN numerically
            if (headerText == "SIN")
            {
                var sorted = dtProfiles.AsEnumerable()
                    .OrderBy(r => _sortAscending
                        ? int.Parse(r["SIN"].ToString()!.Split('-')[2])
                        : int.MaxValue - int.Parse(r["SIN"].ToString()!.Split('-')[2]))
                    .CopyToDataTable();

                dataGridView1.DataSource = sorted;
            }
            else
            {
                dtProfiles.DefaultView.Sort = $"[{headerText}] {sortDir}";
                dataGridView1.DataSource = dtProfiles.DefaultView.ToTable();
            }

            // ✅ Disable built-in sorting after re-binding
            foreach (DataGridViewColumn col in dataGridView1.Columns)
                col.SortMode = DataGridViewColumnSortMode.Programmatic;

            FormatCurrencyColumn("Monthly Rental");
            FormatCurrencyColumn("Penalty");
            FormatCurrencyColumn("Additional Charge");
            ColorPaymentStatusColumn();

            dataGridView1.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection =
                _sortAscending ? SortOrder.Ascending : SortOrder.Descending;

            lblTotalRecords.Text = $"Total Records: {dataGridView1.Rows.Count}";
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
            double additionalCharge = Convert.ToDouble(row.Cells["Additional Charge"].Value ?? 0);

            var form = new ProfilingForm();
            form.LoadForEdit(sin, fullName, businessName, businessSection,
                stallNumber, stallSize, monthlyRental, paymentStatus,
                startDate, additionalCharge);
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

        // ===================== ARCHIVE RECORD ===================== //
        private void btnArchive_Click(object sender, EventArgs e)
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

        // ===================== HIGHLIGHT HELPERS ===================== //
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

        private void GenerateMonthlyReport(
    int month, int year, string monthName, string filePath)
        {
            // ✅ Get data from database
            DataTable dt = Database.GetMonthlyReport(month, year);

            // ✅ Compute summary
            int totalRecords = dt.Rows.Count;
            int totalPaid = dt.Select("[Payment Status] = 'Paid'").Length;
            int totalUnpaid = dt.Select("[Payment Status] = 'Unpaid'").Length;
            double totalCollected = dt.Select("[Payment Status] = 'Paid'")
                                         .Sum(r => Convert.ToDouble(r["Monthly Rental"]));
            double totalUncollected = dt.Select("[Payment Status] = 'Unpaid'")
                                         .Sum(r => Convert.ToDouble(r["Monthly Rental"]));
            double totalPenalty = dt.Select("[Payment Status] = 'Unpaid'")
                                         .Sum(r => Convert.ToDouble(r["Penalty"]));
            double grandTotal = totalCollected + totalPenalty;

            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Monthly Report");

            // ===================== HEADER ===================== //
            ws.Cell("A1").Value = "Municipality of Masinloc";
            ws.Cell("A1").Style.Font.Bold = true;
            ws.Cell("A1").Style.Font.FontSize = 14;
            ws.Range("A1:H1").Merge();
            ws.Cell("A1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            ws.Cell("A2").Value = "Business Permit & Licensing Office";
            ws.Cell("A2").Style.Font.Bold = true;
            ws.Cell("A2").Style.Font.FontSize = 12;
            ws.Range("A2:H2").Merge();
            ws.Cell("A2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            ws.Cell("A3").Value = $"Monthly Collection Summary Report — {monthName} {year}";
            ws.Cell("A3").Style.Font.Bold = true;
            ws.Cell("A3").Style.Font.FontSize = 11;
            ws.Range("A3:H3").Merge();
            ws.Cell("A3").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            ws.Cell("A4").Value = $"Generated by: {Session.CurrentFullName} | {Session.CurrentPosition}";
            ws.Range("A4:H4").Merge();
            ws.Cell("A4").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            ws.Cell("A5").Value = $"Date Generated: {DateTime.Now:MMMM dd, yyyy hh:mm tt}";
            ws.Range("A5:H5").Merge();
            ws.Cell("A5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            // ===================== SUMMARY ===================== //
            int row = 7;
            var headerFill = XLColor.FromArgb(0, 51, 102);
            var headerFont = XLColor.White;

            ws.Cell(row, 1).Value = "SUMMARY";
            ws.Cell(row, 1).Style.Font.Bold = true;
            ws.Cell(row, 1).Style.Font.FontSize = 11;
            ws.Cell(row, 1).Style.Font.FontColor = headerFont;
            ws.Cell(row, 1).Style.Fill.BackgroundColor = headerFill;
            ws.Range(row, 1, row, 8).Merge();
            row++;

            void AddSummaryRow(string label, string value)
            {
                ws.Cell(row, 1).Value = label;
                ws.Cell(row, 1).Style.Font.Bold = true;
                ws.Range(row, 1, row, 4).Merge();
                ws.Cell(row, 5).Value = value;
                ws.Range(row, 5, row, 8).Merge();
                row++;
            }

            AddSummaryRow("Total Stall Owners", totalRecords.ToString());
            AddSummaryRow("Total Paid", totalPaid.ToString());
            AddSummaryRow("Total Unpaid", totalUnpaid.ToString());
            AddSummaryRow("Total Collected", totalCollected.ToString("C2", new System.Globalization.CultureInfo("en-PH")));
            AddSummaryRow("Total Uncollected", totalUncollected.ToString("C2", new System.Globalization.CultureInfo("en-PH")));
            AddSummaryRow("Total Penalty Collected", totalPenalty.ToString("C2", new System.Globalization.CultureInfo("en-PH")));
            AddSummaryRow("Grand Total", grandTotal.ToString("C2", new System.Globalization.CultureInfo("en-PH")));

            row++; // ✅ Blank row before detail

            // ===================== DETAIL HEADER ===================== //
            string[] headers = {
        "SIN", "Full Name", "Business Name",
        "Business Section", "Monthly Rental",
        "Penalty", "Additional Charge", "Payment Status"
    };

            for (int col = 0; col < headers.Length; col++)
            {
                var cell = ws.Cell(row, col + 1);
                cell.Value = headers[col];
                cell.Style.Font.Bold = true;
                cell.Style.Font.FontColor = headerFont;
                cell.Style.Fill.BackgroundColor = headerFill;
                cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            }
            row++;

            // ===================== DETAIL ROWS ===================== //
            bool alternate = false;
            foreach (DataRow dr in dt.Rows)
            {
                var rowColor = alternate
                    ? XLColor.FromArgb(240, 244, 255)
                    : XLColor.White;

                ws.Cell(row, 1).Value = dr["SIN"].ToString();
                ws.Cell(row, 2).Value = dr["Full Name"].ToString();
                ws.Cell(row, 3).Value = dr["Business Name"].ToString();
                ws.Cell(row, 4).Value = dr["Business Section"].ToString();
                ws.Cell(row, 5).Value = Convert.ToDouble(dr["Monthly Rental"]);
                ws.Cell(row, 5).Style.NumberFormat.Format = "₱#,##0.00";
                ws.Cell(row, 6).Value = Convert.ToDouble(dr["Penalty"]);
                ws.Cell(row, 6).Style.NumberFormat.Format = "₱#,##0.00";
                ws.Cell(row, 7).Value = Convert.ToDouble(dr["Additional Charge"]);
                ws.Cell(row, 7).Style.NumberFormat.Format = "₱#,##0.00";
                ws.Cell(row, 8).Value = dr["Payment Status"].ToString();

                // ✅ Color payment status cell
                var statusCell = ws.Cell(row, 8);
                if (dr["Payment Status"].ToString() == "Paid")
                {
                    statusCell.Style.Fill.BackgroundColor = XLColor.LightGreen;
                    statusCell.Style.Font.FontColor = XLColor.DarkGreen;
                }
                else
                {
                    statusCell.Style.Fill.BackgroundColor = XLColor.LightCoral;
                    statusCell.Style.Font.FontColor = XLColor.DarkRed;
                }

                // ✅ Alternating row color
                for (int col = 1; col <= 7; col++)
                    ws.Cell(row, col).Style.Fill.BackgroundColor = rowColor;

                alternate = !alternate;
                row++;
            }

            ws.Columns().AdjustToContents();
            workbook.SaveAs(filePath);
        }

        private void btnMonthlyReport_Click_1(object sender, EventArgs e)
        {
            {
                // ✅ Simple Month/Year picker using a small dialog
                using var form = new Form();
                form.Text = "Select Month & Year";
                form.Size = new Size(300, 200);
                form.BackColor = Color.White;
                form.StartPosition = FormStartPosition.CenterParent;
                form.FormBorderStyle = FormBorderStyle.FixedSingle;
                form.MaximizeBox = false;
                form.MinimizeBox = false;
                form.Icon = new Icon(Path.Combine(
                Application.StartupPath, "Resources", "Masinloc-Logo-HD.ico"));

                var lblMonth = new Label { Text = "Month:", Location = new Point(15, 18), AutoSize = true };
                var lblYear = new Label { Text = "Year:", Location = new Point(15, 55), AutoSize = true };

                var cmbMonth = new ComboBox
                {
                    Location = new Point(80, 15),
                    Width = 180,
                    DropDownStyle = ComboBoxStyle.DropDownList
                };

                var cmbYear = new ComboBox
                {
                    Location = new Point(80, 52),
                    Width = 180,
                    DropDownStyle = ComboBoxStyle.DropDownList
                };

                // ✅ Populate months
                string[] months = {
        "January", "February", "March", "April",
        "May", "June", "July", "August",
        "September", "October", "November", "December"
    };
                cmbMonth.Items.AddRange(months);
                cmbMonth.SelectedIndex = DateTime.Now.Month - 1;

                // ✅ Populate years (5 years back to current)
                for (int y = DateTime.Now.Year; y >= DateTime.Now.Year - 5; y--)
                    cmbYear.Items.Add(y);
                cmbYear.SelectedIndex = 0;

                var btnGenerate = new Button
                {
                    Text = "Generate",
                    Location = new Point(80, 88),
                    Width = 180,
                    Height = 30,
                    BackColor = Color.SeaGreen,
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 9)

                };


                btnGenerate.Click += (s, ev) => form.DialogResult = DialogResult.OK;

                form.Controls.AddRange(new Control[] {
        lblMonth, lblYear, cmbMonth, cmbYear, btnGenerate
    });

                if (form.ShowDialog() != DialogResult.OK) return;

                int selectedMonth = cmbMonth.SelectedIndex + 1;
                int selectedYear = (int)cmbYear.SelectedItem!;
                string monthName = cmbMonth.SelectedItem!.ToString()!;

                // ✅ Save file dialog
                using var sfd = new SaveFileDialog();
                sfd.Filter = "Excel Workbook|*.xlsx";
                sfd.FileName = $"MonthlyReport_{monthName}_{selectedYear}.xlsx";

                if (sfd.ShowDialog() != DialogResult.OK) return;

                try
                {
                    Cursor = Cursors.WaitCursor;
                    GenerateMonthlyReport(selectedMonth, selectedYear, monthName, sfd.FileName);
                    MessageBox.Show(
                        "Monthly Report exported successfully!",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"Error generating report: {ex.Message}",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                finally
                {
                    Cursor = Cursors.Default;
                }

            }
        }
    }
}