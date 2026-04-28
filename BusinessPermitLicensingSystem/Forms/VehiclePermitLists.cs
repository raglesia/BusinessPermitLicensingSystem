using ClosedXML.Excel;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusinessPermitLicensingSystem.Forms
{
    public partial class VehiclePermitLists : Form
    {
        // ===================== FIELDS ===================== //
        private DataTable _dt = new DataTable();
        private bool _sortAsc = true;
        private int _lastSortCol = -1;

        // ===================== CONSTRUCTOR ===================== //
        public VehiclePermitLists()
        {
            InitializeComponent();
            SetupGrid();
            SetupFilters();
            LoadRecords();
        }

        private void VehiclePermitLists_Load(object sender, EventArgs e)
        {
            lblUsername.Text = $"{Session.CurrentFullName} | {Session.CurrentPosition}";

            this.Icon = new Icon(Path.Combine(
                Application.StartupPath, "Resources", "MasinlocLogoIcon.ico"));
        }

        // ===================== SETUP ===================== //

        private void SetupGrid()
        {
            typeof(DataGridView)
                .GetProperty("DoubleBuffered",
                    System.Reflection.BindingFlags.Instance |
                    System.Reflection.BindingFlags.NonPublic)!
                .SetValue(dataGridView1, true);

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ReadOnly = true;

            dataGridView1.ColumnHeaderMouseClick += dataGridView1_ColumnHeaderMouseClick;
            dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick;
        }

        private void SetupFilters()
        {
            txtSearch.PlaceholderText = "🔍 Search records...";
            txtSearch.TextChanged += (s, e) => ApplyFilter();
        }

        public void LoadRecords()
        {
            dataGridView1.DataSource = null;
            _dt = Database.GetAllVehiclePermits();
            dataGridView1.DataSource = _dt;

            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                col.ReadOnly = true;
                col.SortMode = DataGridViewColumnSortMode.Programmatic;
            }

            AlternateRowColors();
            lblTotalRecords.Text = $"Total Records: {_dt.Rows.Count}";
            _sortAsc = true;
            _lastSortCol = -1;
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string header = dataGridView1.Columns[e.ColumnIndex].HeaderText;

            if (_lastSortCol == e.ColumnIndex)
                _sortAsc = !_sortAsc;
            else
            {
                _sortAsc = true;
                _lastSortCol = e.ColumnIndex;
            }

            string dir = _sortAsc ? "ASC" : "DESC";

            if (header == "VIN")
            {
                static int ParseSuffix(DataRow r)
                {
                    var parts = r["VIN"].ToString()!.Split('-');
                    return parts.Length >= 3 && int.TryParse(parts[2], out int n) ? n : 0;
                }

                var sorted = (_sortAsc
                    ? _dt.AsEnumerable().OrderBy(ParseSuffix)
                    : _dt.AsEnumerable().OrderByDescending(ParseSuffix))
                    .CopyToDataTable();

                dataGridView1.DataSource = sorted;
            }
            else
            {
                _dt.DefaultView.Sort = $"[{header}] {dir}";
                dataGridView1.DataSource = _dt.DefaultView.ToTable();
            }

            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                col.ReadOnly = true;
                col.SortMode = DataGridViewColumnSortMode.Programmatic;
            }

            AlternateRowColors();

            dataGridView1.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection =
                _sortAsc ? SortOrder.Ascending : SortOrder.Descending;

            lblTotalRecords.Text = $"Total Records: {dataGridView1.Rows.Count}";
        }

        private void ApplyFilter()
        {
            try
            {
                string search = txtSearch.Text.Trim().Replace("'", "''");

                if (string.IsNullOrWhiteSpace(search))
                {
                    _dt.DefaultView.RowFilter = string.Empty;
                    lblTotalRecords.Text = $"Total Records: {_dt.Rows.Count}";
                    return;
                }

                _dt.DefaultView.RowFilter = $@"
                    [VIN]          LIKE '%{search}%' OR
                    [Company Name] LIKE '%{search}%' OR
                    [Driver Name]  LIKE '%{search}%' OR
                    [Plate No]     LIKE '%{search}%' OR
                    [SEC Reg No]   LIKE '%{search}%' OR
                    [DTI Number]   LIKE '%{search}%'";

                lblTotalRecords.Text = $"Total Records: {_dt.DefaultView.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Filter error: {ex.Message}");
            }
        }

        // ===================== EDIT ===================== //
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dataGridView1.Rows[e.RowIndex];

            string vin = row.Cells["VIN"].Value?.ToString() ?? "";
            string companyName = row.Cells["Company Name"].Value?.ToString() ?? "";
            string driverName = row.Cells["Driver Name"].Value?.ToString() ?? "";
            string plateNo = row.Cells["Plate No"].Value?.ToString() ?? "";
            string secRegNo = row.Cells["SEC Reg No"].Value?.ToString() ?? "";
            string dtiNumber = row.Cells["DTI Number"].Value?.ToString() ?? "";

            var form = new VehicleProfiling();
            form.LoadForEdit(vin, companyName, driverName, plateNo, secRegNo, dtiNumber);
            form.ShowDialog();

            if (form.RecordSaved)
            {
                int firstRow = dataGridView1.FirstDisplayedScrollingRowIndex;
                LoadRecords();
                if (firstRow >= 0 && firstRow < dataGridView1.RowCount)
                    dataGridView1.FirstDisplayedScrollingRowIndex = firstRow;
                HighlightRecord(vin);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a record first.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var row = dataGridView1.SelectedRows[0];
            string vin = row.Cells["VIN"].Value?.ToString() ?? "";
            string name = row.Cells["Company Name"].Value?.ToString() ?? "";

            var confirm = MessageBox.Show(
                $"Delete record for \"{name}\"?\n\nThis cannot be undone.",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            var result = Database.DeleteVehiclePermit(vin);

            if (result.Success)
            {
                Database.LogAudit("Delete Vehicle Permit", vin,
                    Session.CurrentUserId ?? 0, $"Deleted: {name}");
                MessageBox.Show("Record deleted.", "Deleted",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadRecords();
            }
            else
            {
                MessageBox.Show(result.ErrorMessage, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnArchive_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a record first.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var row = dataGridView1.SelectedRows[0];
            string vin = row.Cells["VIN"].Value?.ToString() ?? "";
            string name = row.Cells["Company Name"].Value?.ToString() ?? "";

            var confirm = MessageBox.Show(
                $"Archive \"{name}\"?\n\nRecord will be hidden but kept in the database.",
                "Confirm Archive", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            var result = Database.ArchiveVehiclePermit(vin);

            if (result.Success)
            {
                Database.LogAudit("Archive Vehicle Permit", vin,
                    Session.CurrentUserId ?? 0, $"Archived: {name}");
                MessageBox.Show("Record archived.", "Archived",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadRecords();
            }
            else
            {
                MessageBox.Show(result.ErrorMessage, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnExport_Click(object sender, EventArgs e)
        {
            if (_dt.Rows.Count == 0)
            {
                MessageBox.Show("No data to export.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var sfd = new SaveFileDialog();
            sfd.Filter = "Excel Workbook|*.xlsx";
            sfd.FileName = "VehiclePermits.xlsx";

            if (sfd.ShowDialog() != DialogResult.OK) return;

            try
            {
                btnExport.Enabled = false;
                Cursor = Cursors.WaitCursor;

                var dt = _dt.Copy();
                await Task.Run(() =>
                {
                    using var wb = new XLWorkbook();
                    var ws = wb.Worksheets.Add("Vehicle Permits");

                    ws.Cell(1, 1).Value = "Special Vehicle Permit Records";
                    ws.Range(1, 1, 1, dt.Columns.Count).Merge();
                    ws.Cell(1, 1).Style.Font.Bold = true;
                    ws.Cell(1, 1).Style.Font.FontSize = 14;
                    ws.Cell(1, 1).Style.Alignment.Horizontal =
                        XLAlignmentHorizontalValues.Center;

                    ws.Cell(3, 1).InsertTable(dt);
                    ws.Columns().AdjustToContents();
                    wb.SaveAs(sfd.FileName);
                });

                MessageBox.Show("Export completed!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Export error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnExport.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {

        }

        // ===================== HELPERS ===================== //

        private void AlternateRowColors()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;
                row.DefaultCellStyle.BackColor = row.Index % 2 == 0
                    ? Color.White
                    : Color.AliceBlue;
            }
        }

        public void HighlightRecord(string vin)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["VIN"].Value?.ToString() == vin)
                {
                    dataGridView1.ClearSelection();
                    row.Selected = true;
                    dataGridView1.FirstDisplayedScrollingRowIndex = row.Index;
                    break;
                }
            }
        }

        public void HighLightLastAdded()
        {
            if (dataGridView1.Rows.Count == 0) return;
            dataGridView1.ClearSelection();
            dataGridView1.Rows[0].Selected = true;
            dataGridView1.FirstDisplayedScrollingRowIndex = 0;
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            new DashboardForm().Show();
            this.Hide();
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

    }
}