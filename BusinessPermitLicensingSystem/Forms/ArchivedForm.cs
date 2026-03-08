using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BusinessPermitLicensingSystem.Forms
{
    public partial class ArchivedForm : Form
    {
        // ===================== CONSTRUCTOR ===================== //
        public ArchivedForm()
        {
            InitializeComponent();
            SetupGrid();
            LoadArchived();
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
            dataGridView1.RowHeadersVisible = false;
        }

        // ===================== LOAD ===================== //
        private void LoadArchived()
        {
            dataGridView1.DataSource = Database.GetArchivedProfiles();
        }

        // ===================== RESTORE ===================== //
        private void btnRestore_Click(object sender, EventArgs e)
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
                $"Restore record for {name}?",
                "Confirm Restore",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            var result = Database.RestoreProfiling(sin);

            if (result.Success)
            {
                Database.LogAudit(
                    "Restore", sin,
                    Session.CurrentUserId ?? 0,
                    $"Restored profile for {name}.");

                MessageBox.Show("Record restored successfully.");
                LoadArchived();
            }
            else
            {
                MessageBox.Show(result.ErrorMessage);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DashboardForm dashboard = new DashboardForm();
            dashboard.Show();
            this.Close();
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

    }
}
