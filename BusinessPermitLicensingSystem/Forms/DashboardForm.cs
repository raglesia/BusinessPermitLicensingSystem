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
            button1.Focus();

            lblUsername.Text = $"{Session.CurrentPosition} | {Session.CurrentFullName}";

            UpdateDateTime();

            timer1.Start();

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
            var profilingForm = new ProfilingForm();

            profilingForm.FormClosed += (s, args) =>
            {
                // ✅ Find ProfilingLists and refresh + highlight
                foreach (Form f in Application.OpenForms)
                {
                    if (f is ProfilingLists lists)
                    {
                        lists.LoadProfiles();
                        lists.HighlightLastAdded();
                        break;
                    }
                }
            };

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
            var (updated, skipped) = Database.ApplyPenaltiesToAll();

            if (updated > 0)
            {
                lblPenaltyNotice.ForeColor = Color.DarkRed;
                lblPenaltyNotice.Text =
                    $"⚠️ {updated} unpaid record(s) have been charged a 25% penalty.";
            }
            else
            {
                lblPenaltyNotice.ForeColor = Color.SeaGreen;
                lblPenaltyNotice.Text = "✅ No penalty charges at this time.";
            }

            lblPenaltyNotice.Visible = true;
        }

        private void UpdateDateTime()
        {
            string date = DateTime.Now.ToString("dddd, MMMM dd, yyyy");
            string time = DateTime.Now.ToString("hh:mm:ss");
            string ampm = DateTime.Now.ToString("tt").ToUpper();

            lblDateTime.Text = $"{date}  {time} {ampm}";
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateDateTime();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ArchivedForm archivedForm = new ArchivedForm();
            archivedForm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var form = new RentalRatesForm();
            form.ShowDialog();
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