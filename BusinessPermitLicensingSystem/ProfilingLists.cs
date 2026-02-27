using BusinessPermitLicensingSystem.Forms;
using System;
using System.Data;
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

        private void txtFilterBIN_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           DashboardForm dashboardForm = new DashboardForm();

            dashboardForm.Show();
            this.Close();
        }
    }
}