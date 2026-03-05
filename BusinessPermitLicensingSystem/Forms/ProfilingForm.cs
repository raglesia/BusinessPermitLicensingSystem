using BusinessPermitLicensingSystem.Helpers;
using System;
using System.Data.SQLite;
using System.Windows.Forms;
using static BusinessPermitLicensingSystem.Helpers.InputValidator;

namespace BusinessPermitLicensingSystem.Forms
{
    public partial class ProfilingForm : Form
    {
        // ===================== FIELDS ===================== //
        private bool isEditMode = false;
        private string currentSIN = "";

        // ===================== CONSTRUCTOR ===================== //
        public ProfilingForm(ProfilingLists caller = null)
        {
            InitializeComponent();
        }

        // ===================== FORM LOAD ===================== //
        private void ProfilingForm_Load(object sender, EventArgs e)
        {
            lblUsername.Text = $"{Session.CurrentPosition} | {Session.CurrentFullName}";

            SetupInputValidators();
            SetupControls();

            if (!isEditMode)
                AssignNewBIN();
        }

        // ===================== SETUP ===================== //
        private void SetupInputValidators()
        {
            txtFName.KeyPress += (s, e) => InputValidator.AllowOnlyLetters(e, allowDot: true);
            txtBName.KeyPress += (s, e) => InputValidator.AllowLettersDigitsDotCommaSpace(e);
            txtBSection.KeyPress += (s, e) => InputValidator.AllowOnlyLetters(e, allowDot: true);
            txtSNumber.KeyPress += (s, e) => InputValidator.AllowOnlyDigits(e);
            txtSSize.KeyPress += (s, e) => InputValidator.AllowDecimalNumbers(e, txtSSize);
            txtMRental.KeyPress += (s, e) => InputValidator.AllowDecimalNumbers(e, txtMRental);
        }

        private void SetupControls()
        {
            txtBIN.Enabled = false;

            cmbPaymentStatus.Items.Clear();
            cmbPaymentStatus.Items.AddRange(new string[] { "Unpaid", "Partial", "Paid" });
            cmbPaymentStatus.SelectedIndex = 0;

            dtpStartDate.Value = DateTime.Today;
            dtpStartDate.Format = DateTimePickerFormat.Short;
        }

        // ===================== LOAD FOR EDIT ===================== //
        public void LoadForEdit(
            string sin,
            string fullName,
            string businessName,
            string businessSection,
            string stallNumber,
            string stallSize,
            string monthlyRental,
            string paymentStatus,
            string startDate)
        {
            isEditMode = true;
            currentSIN = sin;

            // Fill fields
            txtBIN.Text = sin;
            txtFName.Text = fullName;
            txtBName.Text = businessName;
            txtBSection.Text = businessSection;
            txtSNumber.Text = stallNumber;
            txtSSize.Text = stallSize;
            txtMRental.Text = monthlyRental;
            cmbPaymentStatus.SelectedItem = paymentStatus ?? "Unpaid";
            btnSave.Text = "Update Record";
            txtBIN.Enabled = false;

            // Set start date
            if (!string.IsNullOrWhiteSpace(startDate) &&
                DateTime.TryParse(startDate, out DateTime parsedDate))
                dtpStartDate.Value = parsedDate;
            else
                dtpStartDate.Value = DateTime.Today;
        }

        // ===================== SAVE ===================== //
        private void btnSave_Click(object sender, EventArgs e)
        {
            string fullName = txtFName.Text.Trim();
            string businessName = txtBName.Text.Trim();
            string businessSection = txtBSection.Text.Trim();
            string stallNumber = txtSNumber.Text.Trim();
            string stallSize = txtSSize.Text.Trim();
            string paymentStatus = cmbPaymentStatus.SelectedItem?.ToString() ?? "Unpaid";
            string startDate = dtpStartDate.Value.ToString("yyyy-MM-dd");

            if (!double.TryParse(txtMRental.Text, out double monthlyRental))
            {
                MessageBox.Show("Monthly Rental must be a valid number.");
                return;
            }

            long currentUserId = Session.CurrentUserId ?? 0;
            string orNumber = "";
            double penalty = 0;

            // ✅ Only ask for OR Number in EDIT mode when marking as Paid
            if (isEditMode && paymentStatus == "Paid")
            {
                penalty = Database.CalculatePenalty(monthlyRental, paymentStatus, startDate);

                var dialog = new ORNumberDialog();
                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    MessageBox.Show(
                        "OR Number is required when marking as Paid.",
                        "OR Number Missing",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                orNumber = dialog.ORNumber;
            }

            if (isEditMode)
                SaveEdit(fullName, businessName, businessSection,
                    stallNumber, stallSize, monthlyRental,
                    startDate, paymentStatus, orNumber,
                    penalty, currentUserId);
            else
                SaveNew(fullName, businessName, businessSection,
                    stallNumber, stallSize, monthlyRental,
                    startDate, paymentStatus, orNumber,
                    penalty, currentUserId);
        }

        private void SaveEdit(
            string fullName, string businessName, string businessSection,
            string stallNumber, string stallSize, double monthlyRental,
            string startDate, string paymentStatus, string orNumber,
            double penalty, long currentUserId)
        {
            var result = Database.UpdateProfiling(
                currentSIN, fullName, businessName,
                businessSection, stallNumber,
                stallSize, monthlyRental, startDate);

            if (result.Success)
            {
                Database.UpdatePaymentStatus(
                    currentSIN, paymentStatus,
                    orNumber, monthlyRental + penalty,
                    penalty, currentUserId);

                Database.LogAudit(
                    "Update", currentSIN, currentUserId,
                    $"Updated profile for {fullName}, Status: {paymentStatus}" +
                    (paymentStatus == "Paid" ? $", OR#: {orNumber}" : ""));

                MessageBox.Show("Record updated successfully!");
                this.Close();
            }
            else
            {
                MessageBox.Show(result.ErrorMessage ?? "Unknown error.");
            }
        }

        private void SaveNew(
            string fullName, string businessName, string businessSection,
            string stallNumber, string stallSize, double monthlyRental,
            string startDate, string paymentStatus, string orNumber,
            double penalty, long currentUserId)
        {
            var result = Database.AddProfiling(
                txtBIN.Text, fullName, businessName,
                businessSection, stallNumber,
                stallSize, monthlyRental, startDate);

            if (result.Success)
            {
                Database.UpdatePaymentStatus(
                    txtBIN.Text, paymentStatus,
                    orNumber, monthlyRental + penalty,
                    penalty, currentUserId);

                Database.LogAudit(
                    "Add", txtBIN.Text, currentUserId,
                    $"Added profile for {fullName}, Status: {paymentStatus}");

                MessageBox.Show("Record added successfully!");
                ClearFields();
                AssignNewBIN();
            }
        }

        // ===================== HELPERS ===================== //
        private void AssignNewBIN()
        {
            try
            {
                using var con = new SQLiteConnection("Data Source=database.db;Version=3;");
                con.Open();
                txtBIN.Text = Database.GenerateUniqueBIN(con);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Failed to generate SIN: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ClearFields()
        {
            txtFName.Clear();
            txtBName.Clear();
            txtBSection.Clear();
            txtSNumber.Clear();
            txtSSize.Clear();
            txtMRental.Clear();
            cmbPaymentStatus.SelectedIndex = 0;
            dtpStartDate.Value = DateTime.Today;
        }

        // ===================== NAVIGATION ===================== //
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // ===================== EVENTS ===================== //
        private void txtMRental_Leave(object sender, EventArgs e)
        {
            if (double.TryParse(txtMRental.Text, out double value))
                txtMRental.Text = value.ToString("N2");
        }
    }
}