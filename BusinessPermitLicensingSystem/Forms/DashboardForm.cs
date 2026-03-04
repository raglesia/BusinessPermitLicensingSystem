using System;
using System.Windows.Forms;

namespace BusinessPermitLicensingSystem.Forms
{
    public partial class DashboardForm : Form
    {
        // ===================== CONSTRUCTOR ===================== //
        public DashboardForm()
        {
            InitializeComponent();
        }

        // ===================== FORM LOAD ===================== //
        private void DashboardForm_Load(object sender, EventArgs e)
        {
            lblUsername.Text = $"{Session.CurrentPosition} | {Session.CurrentFullName}";

            CheckMonthlyReset();
            CheckPenalties();
        }

        // ===================== NAVIGATION ===================== //
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

        // ===================== LOGOUT ===================== //
        private void button6_Click(object sender, EventArgs e)
        {
            if (Session.CurrentUserId != null)
            {
                Database.LogAudit(
                    "Logout",
                    null,
                    Session.CurrentUserId ?? 0,
                    $"User '{Session.CurrentUsername}' logged out.");
            }

            Application.Exit();
        }

        // ===================== PENALTY CHECKS ===================== //
        private void CheckPenalties()
        {
            if (DateTime.Today.Day <= 20) return;

            var (updated, skipped) = Database.ApplyPenaltiesToAll();

            if (updated > 0)
            {
                MessageBox.Show(
                    $"Penalty Notice:\n\n" +
                    $"{updated} unpaid record(s) have been charged a 25% penalty.\n" +
                    $"Due date was the 20th of this month.",
                    "Penalty Applied",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        // ===================== MONTHLY RESET ===================== //
        private void CheckMonthlyReset()
        {
            DateTime today = DateTime.Today;
            string currentMonthYear = today.ToString("yyyy-MM");
            string lastReset = Database.GetSetting("LastPaymentReset");

            if (lastReset == currentMonthYear) return;

            var (reset, error) = Database.ResetMonthlyPaymentStatus();

            if (reset > 0)
            {
                Database.SaveSetting("LastPaymentReset", currentMonthYear);

                Database.LogAudit(
                    "MonthlyReset",
                    null,
                    Session.CurrentUserId ?? 0,
                    $"Monthly reset — {reset} records reset to Unpaid for {today:MMMM yyyy}.");

                MessageBox.Show(
                    $"Monthly Reset:\n\n" +
                    $"{reset} record(s) have been reset to Unpaid for {today:MMMM yyyy}.",
                    "Monthly Reset",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }
    }
}