using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BusinessPermitLicensingSystem.Forms
{
    public partial class LogInForm : Form
    {
        public LogInForm()
        {
            InitializeComponent();
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            string username = txtUser.Text.Trim();
            string password = txtPass.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both username and password.", "Masinloc-BPLS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var (isValid, messageOrUserId) = Database.VerifyLogin(username, password);

            if (isValid)
            {
                // Login successful
                //MessageBox.Show("Login successful!", "Masinloc-BPLS", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Optional: pass user ID or username to dashboard if needed
                // string currentUserId = messageOrUserId;

                DashboardForm dash = new DashboardForm();
                dash.Show();
                this.Hide();           // or this.Close() if you don't plan to show login again
            }
            else
            {
                // Login failed – show the reason
                MessageBox.Show(messageOrUserId ?? "Invalid username or password.", "Masinloc-BPLS", MessageBoxButtons.OK, MessageBoxIcon.Error);

                txtPass.Clear();
                txtPass.Focus();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void btnCreate_Click_1(object sender, EventArgs e)
        {
            // Create an instance of the form
            AccountCreationForm createForm = new AccountCreationForm();

            // Show it
            createForm.Show();

            // Optional: Hide login form
            this.Hide();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            AccountCreationForm form = new AccountCreationForm();
            form.ShowDialog(); // modal window
        }

    }
}

