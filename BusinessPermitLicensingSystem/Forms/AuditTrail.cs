using System;
using System.Data;
using System.Windows.Forms;

namespace BusinessPermitLicensingSystem.Forms
{
    public partial class AuditTrail : Form
    {
        // ===================== CONSTRUCTOR ===================== //
        public AuditTrail()
        {
            InitializeComponent();
            radioUsers.Checked = true;
            LoadAuditRecords();
        }

        // ===================== FORM LOAD ===================== //
        private void AuditTrail_Load(object sender, EventArgs e)
        {
            lblUsername.Text = $"{Session.CurrentFullName ?? "Unknown"} | {Session.CurrentPosition ?? ""}";
        }

        // ===================== DATA LOADING ===================== //
        private void LoadAuditRecords()
        {
            try
            {
                DataTable dt = radioUsers.Checked
                    ? Database.GetUserAuditTrail()
                    : Database.GetAuditTrail();

                dtAudit.DataSource = dt;
                dtAudit.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dtAudit.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dtAudit.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Failed to load audit records: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ===================== NAVIGATION ===================== //
        private void button1_Click(object sender, EventArgs e)
        {
            DashboardForm dashboardForm = new DashboardForm();
            dashboardForm.Show();
            this.Hide();
        }

        // ===================== RADIO BUTTONS ===================== //
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
    }
}