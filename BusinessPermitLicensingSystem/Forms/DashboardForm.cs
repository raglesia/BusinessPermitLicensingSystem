using System;
using System.Drawing;
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
            new ProfilingLists().Show();
            Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new ArchivedForm().Show();
            Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new RentalRatesForm().ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var profilingForm = new ProfilingForm();

            profilingForm.FormClosed += (s, args) =>
            {
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
            Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            new AuditTrail().Show();
            Hide();
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

        // ===================== PENALTIES ===================== //
        private void CheckPenalties()
        {
            var (updated, _) = Database.ApplyPenaltiesToAll();

            if (updated > 0)
            {
                lblPenaltyNotice.ForeColor = Color.DarkRed;
                lblPenaltyNotice.Text = $"⚠️ {updated} unpaid record(s) have been charged a 25% penalty.";
            }
            else
            {
                lblPenaltyNotice.ForeColor = Color.SeaGreen;
                lblPenaltyNotice.Text = "✅ No penalty charges at this time.";
            }

            lblPenaltyNotice.Visible = true;
        }

        // ===================== DATE & TIME ===================== //
        private void UpdateDateTime()
        {
            var now = DateTime.Now;
            lblDateTime.Text = $"{now:dddd MMMM dd, yyyy}  {now:hh:mm:ss} {now.ToString("tt").ToUpper()}";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateDateTime();
        }

        private void lblDateTime_Click(object sender, EventArgs e)
        {

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