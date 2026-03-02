using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BusinessPermitLicensingSystem.Forms
{
    public partial class AuditTrail : Form
    {
        public AuditTrail()
        {
            InitializeComponent();
            LoadAuditRecords();

            radioUsers.Checked = true;
        }

        private void LoadAuditRecords()
        {
            try
            {
                DataTable dt;

                if (radioUsers.Checked)
                {
                    dt = Database.GetUserAuditTrail();
                }
                else
                {
                    dt = Database.GetAuditTrail();
                }

                dtAudit.DataSource = dt;
                dtAudit.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dtAudit.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dtAudit.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load audit records: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DashboardForm dashboardForm = new DashboardForm();

            dashboardForm.Show();
            this.Hide();
        }

        private void radioUsers_CheckedChanged(object sender, EventArgs e)
        {
            if (radioUsers.Checked)
                LoadAuditRecords();
        }

        private void radioProfiling_CheckedChanged(object sender, EventArgs e)
        {
            if (radioProfiling.Checked)
                LoadAuditRecords();
        }

        private void AuditTrail_Load(object sender, EventArgs e)
        {
            lblUsername.Text = $"Admin : {Session.CurrentFullName ?? "Unknown"}";
        }
    }
}
