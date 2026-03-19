using System;
using System.Drawing;
using System.IO;
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

            this.Icon = new Icon(Path.Combine(
                Application.StartupPath, "Resources", "MasinlocLogoIcon.ico"));

            UpdateDateTime();
            timer1.Start();
            CheckPenalties();
        }

        // ===================== NAVIGATION ===================== //
        private void button1_Click(object sender, EventArgs e)
        {
            new ProfilingLists().Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new ArchivedForm().Show();
            this.Hide();
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
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (Session.CurrentFullName != "Jeason S. Barnachia")
            {
                MessageBox.Show("Contact Administrator to access Audit Logs.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            new AuditTrail().Show();
            this.Hide();
        }

        // ===================== LOGOUT ===================== //
        private void button6_Click(object sender, EventArgs e)
        {
            if (Session.CurrentUserId != null)
            {
                Database.LogAudit(
                    "Logout", null,
                    Session.CurrentUserId ?? 0,
                    $"User '{Session.CurrentUsername}' logged out.");
            }

            Application.Exit();
        }

        // ===================== PENALTIES ===================== //
        private void CheckPenalties()
        {
            try
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
            catch (Exception ex)
            {
                lblPenaltyNotice.ForeColor = Color.DarkRed;
                lblPenaltyNotice.Text = $"⚠️ Penalty check failed: {ex.Message}";
                lblPenaltyNotice.Visible = true;
            }
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