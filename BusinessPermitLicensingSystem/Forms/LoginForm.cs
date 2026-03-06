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

        // ===================== LOGIN ===================== //
        private void btnLogIn_Click(object sender, EventArgs e)
        {
            string username = txtUser.Text.Trim();
            string password = txtPass.Text;

            // Validate input
            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show(
                    "Please enter both username and password.",
                    "Masinloc-BPLS",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // Verify credentials
            var (isValid, messageOrUserId) = Database.VerifyLogin(username, password);

            if (isValid)
            {
                // Store session
                Session.CurrentUserId = long.Parse(messageOrUserId!);
                Session.CurrentUsername = username;
                Session.CurrentFullName = Database.GetFullName(Session.CurrentUserId.Value);
                Session.CurrentPosition = Database.GetPosition(Session.CurrentUserId.Value);

                // Log login
                Database.LogAudit(
                    "Login",
                    null,
                    Session.CurrentUserId ?? 0,
                    $"User '{username}' logged in.");

                //MessageBox.Show(
                    //"Login successful!",
                   // "Masinloc-BPLS",
                   // MessageBoxButtons.OK,
                   // MessageBoxIcon.Information);

                // Navigate to dashboard
                DashboardForm dash = new DashboardForm();
                dash.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show(
                    messageOrUserId ?? "Invalid username or password.",
                    "Masinloc-BPLS",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                txtPass.Clear();
                txtPass.Focus();
            }
        }

        // ===================== NAVIGATION ===================== //
        private void btnCreate_Click(object sender, EventArgs e)
        {
            AccountCreationForm form = new AccountCreationForm();
            form.ShowDialog();
        }

        // ===================== EXIT ===================== //
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnCreate_Click_1(object sender, EventArgs e)
        {
            AccountCreationForm form = new AccountCreationForm();
            form.ShowDialog();
        }

        private void LogInForm_Load(object sender, EventArgs e)
        {
            {
                this.MaximizeBox = false;  // Hide maximize
            }
        }

        // ===================== HIDE CLOSE BUTTON ===================== //
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
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }

}