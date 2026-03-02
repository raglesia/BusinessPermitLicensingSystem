using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static BusinessPermitLicensingSystem.Database;
using static BusinessPermitLicensingSystem.Helpers.InputValidator;

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

            // ✅ VERIFY LOGIN ONLY ONCE
            var (isValid, messageOrUserId) =
                Database.VerifyLogin(username, password);

            if (isValid)
            {
                // ✅ STORE CURRENT USER SESSION
                Session.CurrentUserId = long.Parse(messageOrUserId!);
                Session.CurrentUsername = username;

                Session.CurrentFullName = Database.GetFullName(Session.CurrentUserId.Value);

                Database.LogAudit(
                    "Login", null, Session.CurrentUserId ?? 0, $"User '{username}' logged in.");

                MessageBox.Show(
                    "Login successful!",
                    "Masinloc-BPLS",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

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

