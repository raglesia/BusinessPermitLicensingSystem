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
            // Clear previous messages
            lblMessage.ForeColor = Color.Red;
            lblMessage.Text = "";

            // Get input values
            string fullname = txtFullName.Text.Trim();
            string username = txtuname.Text.Trim();
            string position = txtPosition.Text.Trim();
            string password = txtpass.Text;
            string confirmPassword = txtconpass.Text;

            // Validate confirm password
            if (password != confirmPassword)
            {
                lblMessage.Text = "Passwords do not match.";
                return;
            }

            // Attempt account creation
            var result = Database.CreateAccount(fullname, username, position, password);

            if (result.Success)
            {
                MessageBox.Show(
                    "Account created successfully!",
                    "Masinloc-BPLS",
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
                lblMessage.Text = result.ErrorMessage;
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
    }
}