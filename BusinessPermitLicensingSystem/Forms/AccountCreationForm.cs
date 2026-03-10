using System;
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
            string fullname = txtFullName.Text.Trim();
            string username = txtuname.Text.Trim();
            string position = txtPosition.Text.Trim();
            string password = txtpass.Text;
            string confirmPassword = txtconpass.Text;

            if (password != confirmPassword)
            {
                MessageBox.Show(
                    "Passwords do not match. Please try again.",
                    "Masinloc BPLS - Account Creation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var result = Database.CreateAccount(fullname, username, position, password);

            if (result.Success)
            {
                MessageBox.Show(
                    "Account created successfully!",
                    "Masinloc BPLS - Account Creation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                ClearFields();

                new LogInForm().Show();
                Close();
            }
            else
            {
                MessageBox.Show(
                    result.ErrorMessage,
                    "Masinloc BPLS - Account Creation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
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

        // ===================== NAVIGATION ===================== //
        private void btnCancel_Click(object sender, EventArgs e)
        {
            new LogInForm().Show();
            Hide();
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

        protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_MOVE = 0xF010;

            if (m.Msg == WM_SYSCOMMAND && m.WParam == new IntPtr(SC_MOVE))
                return;

            base.WndProc(ref m);
        }
    }
}