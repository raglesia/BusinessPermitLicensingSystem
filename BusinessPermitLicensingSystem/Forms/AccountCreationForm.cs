using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BusinessPermitLicensingSystem.Forms
{
    public partial class AccountCreationForm : Form
    {
        public AccountCreationForm()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string fullname = txtFullName.Text.Trim();
            string username = txtuname.Text.Trim();
            string position = txtPosition.Text.Trim();
            string password = txtpass.Text;
            string confirmPassword = txtconpass.Text;
         

            lblMessage.ForeColor = Color.Red;
            lblMessage.Text = "";

            // Check confirm password
            if (password != confirmPassword)
            {
                lblMessage.Text = "Passwords do not match.";
                return;
            }

            var result = Database.CreateAccount(fullname, username, position, password);

            if (result.Success)
            {
                MessageBox.Show("Account Created Succssfully!,", "Masinloc-BPLS", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtFullName.Clear();
                txtPosition.Clear();
                txtuname.Clear();
                txtpass.Clear();
                txtconpass.Clear();
                

                LogInForm loginForm = new LogInForm();
                loginForm.Show();

                this.Close();
            }
            else
            {
                lblMessage.Text = result.ErrorMessage;
            }
        }

        private void AccountCreationForm_Load(object sender, EventArgs e)
        {
            Database.Initialize();  
        }
    }
}

