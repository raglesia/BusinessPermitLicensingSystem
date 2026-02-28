using BusinessPermitLicensingSystem.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Windows.Forms;
using static BusinessPermitLicensingSystem.Helpers.InputValidator;

namespace BusinessPermitLicensingSystem.Forms
{

    public partial class ProfilingForm : Form
    {
        private DataTable dtProfiles = new DataTable();
        public ProfilingForm()
        {
            InitializeComponent();

        }

        public enum FormMode
        {
            Add,
            Edit
        }

        private bool isEditMode = false;
        private string currentSIN = "";

        private void ProfilingForm_Load(object sender, EventArgs e)
        {
            txtFName.KeyPress += (s, e) => InputValidator.AllowOnlyLetters(e, allowDot: true);
            txtBName.KeyPress += (s, e) => InputValidator.AllowLettersDigitsDotCommaSpace(e);
            txtBSection.KeyPress += (s, e) => InputValidator.AllowOnlyLetters(e, allowDot: true);
            txtSNumber.KeyPress += (s, e) => InputValidator.AllowOnlyDigits(e);
            txtSSize.KeyPress += (s, e) => InputValidator.AllowDecimalNumbers(e, txtSSize);
            txtMRental.KeyPress += (s, e) => InputValidator.AllowDecimalNumbers(e, txtMRental);

            txtBIN.Enabled = false;

            if (!isEditMode)
            {
                AssignNewBIN(); // ONLY when adding
            }
        }


        public void LoadForEdit(
            string sin,
            string fullName,
            string businessName,
            string businessSection,
            string stallNumber,
            string stallSize,
            string monthlyRental)
        {
            isEditMode = true;
            currentSIN = sin;

            // FILL TEXTBOXES //
            txtBIN.Text = sin;
            txtFName.Text = fullName;
            txtBName.Text = businessName;
            txtBSection.Text = businessSection;
            txtSNumber.Text = stallNumber;
            txtSSize.Text = stallSize;
            txtMRental.Text = monthlyRental;

            btnSave.Text = "Update Record";

            txtBIN.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string fullName = txtFName.Text.Trim();
            string businessName = txtBName.Text.Trim();
            string businessSection = txtBSection.Text.Trim();
            string stallNumber = txtSNumber.Text.Trim();
            string stallSize = txtSSize.Text.Trim();

            if (!double.TryParse(txtMRental.Text, out double monthlyRental))
            {
                MessageBox.Show("Monthly Rental must be valid.");
                return;
            }

            // ✅ Get logged-in user
            long currentUserId = Session.CurrentUserId ?? 0;

            if (isEditMode)
            {
                var result = Database.UpdateProfiling(
                    currentSIN,
                    fullName,
                    businessName,
                    businessSection,
                    stallNumber,
                    stallSize,
                    monthlyRental
                );

                if (result.Success)
                {
                    // ✅ AUDIT LOG - UPDATE
                    Database.LogAudit(
                        "Update",
                        currentSIN,
                        currentUserId,
                        $"Updated profile for {fullName}"
                    );

                    MessageBox.Show("Record updated successfully!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show(result.ErrorMessage ?? "Unknown error.");
                }
            }
            else
            {
                var result = Database.AddProfiling(
                    txtBIN.Text,
                    fullName,
                    businessName,
                    businessSection,
                    stallNumber,
                    stallSize,
                    monthlyRental
                );

                if (result.Success)
                {
                    // ✅ AUDIT LOG - ADD
                    Database.LogAudit(
                        "Add",
                        txtBIN.Text,
                        currentUserId,
                        $"Added profile for {fullName}"
                    );

                    MessageBox.Show("Record added successfully!");
                    ClearFields();
                    AssignNewBIN();
                }
            }
        }
        private void AssignNewBIN()
        {
            try
            {
                using var con = new System.Data.SQLite.SQLiteConnection("Data Source=database.db;Version=3;");
                con.Open();
                txtBIN.Text = Database.GenerateUniqueBIN(con);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to generate BIN: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DashboardForm dashboardForm = new DashboardForm();

            dashboardForm.Show();
            this.Hide();
        }

        private void ClearFields()
        {
            txtFName.Clear();
            txtBName.Clear();
            txtBSection.Clear();
            txtSNumber.Clear();
            txtSSize.Clear();
            txtMRental.Clear();
        }

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            // Switch from EDIT → ADD mode
            isEditMode = false;

            // Clear reference to old SIN
            currentSIN = "";

            // Generate NEW SIN
            AssignNewBIN();

            // Change button text back
            btnSave.Text = "Save Record";

           MessageBox.Show("Now saving as NEW record.");
        }
        private void txtMRental_Leave(object sender, EventArgs e)
        {
            if (double.TryParse(txtMRental.Text, out double value))
                txtMRental.Text = value.ToString("N2");
        }
    }
}
    