using System.Drawing;
using System.Windows.Forms;

namespace BusinessPermitLicensingSystem.Forms
{
    public partial class AccountCreationForm : Form
    {
        // ===================== CONSTRUCTOR ===================== //
        public AccountCreationForm()
        {
            InitializeComponent();
        }

        // ===================== FORM LOAD ===================== //
        private void AccountCreationForm_Load(object sender, EventArgs e)
        {
            Database.Initialize();
        }

        // ===================== CREATE ACCOUNT ===================== //
        private void btnCreate_Click(object sender, EventArgs e)
        {

            // Get input values
            string fullname = txtFullName.Text.Trim();
            string username = txtuname.Text.Trim();
            string position = txtPosition.Text.Trim();
            string password = txtpass.Text;
            string confirmPassword = txtconpass.Text;

            // Validate confirm password
            if (password != confirmPassword)
            {
                MessageBox.Show(
                    "Passwords do not match. Please try again.",
                    "Masinloc BPLS - Account Creation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // Attempt account creation
            var result = Database.CreateAccount(fullname, username, position, password);

            if (result.Success)
            {
                MessageBox.Show(
                    "Account created successfully!",
                    "Masinloc BPLS - Account Creation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                ClearFields();

                // Return to login
                LogInForm loginForm = new LogInForm();
                loginForm.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show(result.ErrorMessage);
            }
        }

        // ===================== HELPERS ===================== //
        private void ClearFields()
        {
            txtFullName.Clear();
            txtPosition.Clear();
            txtuname.Clear();
            txtpass.Clear();
            txtconpass.Clear();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            LogInForm loginForm = new LogInForm();

            loginForm.Show();
            this.Hide();
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

        // ===================== PREVENT MOVING ===================== //
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0112)
            {
                if (m.WParam == new IntPtr(0xF010))
                    return;
            }
            base.WndProc(ref m);
        }
    }
}