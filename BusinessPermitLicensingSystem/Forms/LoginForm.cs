using System;
using System.Windows.Forms;

namespace BusinessPermitLicensingSystem.Forms
{
    public partial class LogInForm : Form
    {
        // ===================== CONSTRUCTOR ===================== //
        public LogInForm()
        {
            InitializeComponent();
        }

        // ===================== FORM LOAD ===================== //
        private void LogInForm_Load(object sender, EventArgs e)
        {
            MaximizeBox = false;
            txtUser.Focus();
            SetupSecurity();

            this.Icon = new Icon(Path.Combine(
                Application.StartupPath, "Resources", "MasinlocLogoIcon.ico"));
        }

        // ===================== LOGIN ===================== //
        private void btnLogIn_Click(object sender, EventArgs e)
        {
            string username = txtUser.Text.Trim();
            string password = txtPass.Text;

            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show(
                    "Please enter both username and password.",
                    "Masinloc BPLS - Log In",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var (isValid, messageOrUserId) = Database.VerifyLogin(username, password);

            if (isValid)
            {
                Session.CurrentUserId = long.Parse(messageOrUserId!);
                Session.CurrentUsername = username;
                Session.CurrentFullName = Database.GetFullName(Session.CurrentUserId.Value);
                Session.CurrentPosition = Database.GetPosition(Session.CurrentUserId.Value);

                Database.LogAudit(
                    "Login",
                    null,
                    Session.CurrentUserId ?? 0,
                    $"User '{username}' logged in.");

                new DashboardForm().Show();
                Hide();
            }
            else
            {
                MessageBox.Show(
                    messageOrUserId ?? "Invalid Username or Password.",
                    "Masinloc BPLS - Log In",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                txtPass.Clear();
                txtPass.Focus();
            }
        }

        // ===================== NAVIGATION ===================== //
        private void btnCreate_Click_1(object sender, EventArgs e)
        {
            new AccountCreationForm().Show();
            Hide();
        }

        // ===================== EXIT ===================== //
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // ===================== SECURITY ===================== //
        private void SetupSecurity()
        {
            txtUser.KeyDown += (s, e) => { if (e.Control && e.KeyCode == Keys.V) e.SuppressKeyPress = true; };
            txtPass.KeyDown += (s, e) => { if (e.Control && e.KeyCode == Keys.V) e.SuppressKeyPress = true; };

            txtUser.ContextMenuStrip = new ContextMenuStrip();
            txtPass.ContextMenuStrip = new ContextMenuStrip();
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