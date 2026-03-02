using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BusinessPermitLicensingSystem.Forms
{
    public partial class DashboardForm : Form
    {
        public DashboardForm()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ProfilingLists profilingLists = new ProfilingLists();

            profilingLists.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ProfilingForm profilingForm = new ProfilingForm();

            profilingForm.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AuditTrail auditForm = new AuditTrail();

            auditForm.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // ✅ AUDIT: log logout
            if (Session.CurrentUserId != null)
            {
                Database.LogAudit("Logout", null, Session.CurrentUserId ?? 0, $"User '{Session.CurrentUsername}' logged out");
            }

            Application.Exit(); // exit the program
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {
            lblUsername.Text = $"Admin : {Session.CurrentFullName ?? "Unknown"}";
        }
    }
}
