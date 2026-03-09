using BusinessPermitLicensingSystem.Helpers;
using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using static BusinessPermitLicensingSystem.Helpers.InputValidator;

namespace BusinessPermitLicensingSystem.Forms
{
    public partial class ProfilingForm : Form
    {
        // ===================== FIELDS ===================== //
        private bool isEditMode = false;
        private string currentSIN = "";
        private bool isLoading = true;

        // ✅ Store edit data temporarily
        private string _editFullName = "";
        private string _editBusinessName = "";
        private string _editBusinessSection = "";
        private string _editStallNumber = "";
        private string _editStallSize = "";
        private string _editMonthlyRental = "";
        private string _editPaymentStatus = "";
        private string _editStartDate = "";
        private double _editAdditionalCharge = 0;

        // ===================== CONSTRUCTOR ===================== //
        public ProfilingForm()
        {
            InitializeComponent();

            this.Size = new Size(700, 696);
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        // ===================== FORM LOAD ===================== //
        private void ProfilingForm_Load(object sender, EventArgs e)
        {
            isLoading = true;

            lblUsername.Text = $"{Session.CurrentPosition} | {Session.CurrentFullName}";

            SetupInputValidators();
            SetupControls();
            SetupSecurity();

            if (isEditMode)
                PopulateEditFields();
            else
                AssignNewBIN();

            isLoading = false;
        }

        // ===================== SETUP ===================== //
        private void SetupInputValidators()
        {
            txtFName.KeyPress += (s, e) => InputValidator.AllowOnlyLetters(e, allowDot: true);
            txtBName.KeyPress += (s, e) => InputValidator.AllowLettersDigitsDotCommaSpace(e);
            txtSNumber.KeyPress += (s, e) => InputValidator.AllowOnlyDigits(e);
            txtSSize.KeyPress += (s, e) => InputValidator.AllowDecimalNumbers(e, txtSSize);
            txtAdditionalCharge.KeyPress += (s, e) => InputValidator.AllowDecimalNumbers(e, txtAdditionalCharge);

            // Auto-compute when section or stall size changes
            cmbBSection.SelectedIndexChanged += (s, e) => ComputeMonthlyRental();
            txtSSize.TextChanged += (s, e) => ComputeMonthlyRental();
            txtAdditionalCharge.TextChanged += (s, e) => ComputeMonthlyRental();

            // Toggle additional charges
            chkAdditional.CheckedChanged += (s, e) =>
            {
                txtAdditionalCharge.Enabled = chkAdditional.Checked;

                if (!chkAdditional.Checked)
                    txtAdditionalCharge.Text = "0.00";

                ComputeMonthlyRental();
            };
        }

        private void SetupControls()
        {
            txtBIN.Enabled = false;
            txtMRental.Enabled = false;

            // ✅ Load sections from RentalRates table
            cmbBSection.Items.Clear();
            DataTable rates = Database.GetRentalRates();
            foreach (DataRow row in rates.Rows)
                cmbBSection.Items.Add(row["Section"].ToString());

            cmbPaymentStatus.Items.Clear();
            cmbPaymentStatus.Items.AddRange(new string[] { "Unpaid", "Paid" });
            cmbPaymentStatus.SelectedIndex = 0;

            dtpStartDate.Value = DateTime.Today;
            dtpStartDate.Format = DateTimePickerFormat.Short;

            // Additional charges — disabled by default
            txtAdditionalCharge.Enabled = false;
            txtAdditionalCharge.Text = "0.00";

            txtAdditionalCharge.Leave += (s, e) =>
            {
                if (double.TryParse(txtAdditionalCharge.Text, out double value))
                    txtAdditionalCharge.Text = value.ToString("N2");
            };
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
            string startDate,
            double additionalCharge = 0)
        {
            isEditMode = true;
            currentSIN = sin;
            _editFullName = fullName;
            _editBusinessName = businessName;
            _editBusinessSection = businessSection;
            _editStallNumber = stallNumber;
            _editStallSize = stallSize;
            _editMonthlyRental = monthlyRental;
            _editPaymentStatus = paymentStatus;
            _editStartDate = startDate;
            _editAdditionalCharge = additionalCharge;
        }

        // ===================== POPULATE EDIT FIELDS ===================== //
        private void PopulateEditFields()
        {
            txtBIN.Text = currentSIN;
            txtBIN.Enabled = false;
            txtFName.Text = _editFullName;
            txtBName.Text = _editBusinessName;

            // ✅ Match combobox item by text
            cmbBSection.SelectedIndex = -1;
            foreach (var item in cmbBSection.Items)
            {
                if (item.ToString() == _editBusinessSection)
                {
                    cmbBSection.SelectedItem = item;
                    break;
                }
            }

            txtSNumber.Text = _editStallNumber;
            txtSSize.Text = _editStallSize;
            txtMRental.Text = _editMonthlyRental;

            // ✅ Match payment status
            cmbPaymentStatus.SelectedIndex = -1;
            foreach (var item in cmbPaymentStatus.Items)
            {
                if (item.ToString() == _editPaymentStatus)
                {
                    cmbPaymentStatus.SelectedItem = item;
                    break;
                }
            }

            btnSave.Text = "           Update Record";

            // ✅ Additional Charge
            if (_editAdditionalCharge > 0)
            {
                chkAdditional.Checked = true;
                txtAdditionalCharge.Enabled = true;
                txtAdditionalCharge.Text = _editAdditionalCharge.ToString("N2");
            }
            else
            {
                chkAdditional.Checked = false;
                txtAdditionalCharge.Enabled = false;
                txtAdditionalCharge.Text = "0.00";
            }

            // ✅ Start Date
            if (!string.IsNullOrWhiteSpace(_editStartDate) &&
                DateTime.TryParse(_editStartDate, out DateTime parsedDate))
                dtpStartDate.Value = parsedDate;
            else
                dtpStartDate.Value = DateTime.Today;
        }

        // ===================== COMPUTE ===================== //
        private void ComputeMonthlyRental()
        {
            if (isLoading) return;

            string section = cmbBSection.SelectedItem?.ToString() ?? "";
            if (string.IsNullOrWhiteSpace(section)) return;

            var (ratePerSqm, flatRate, rateType) = Database.GetRateBySection(section);

            double baseRental = 0;

            if (rateType == "Flat")
            {
                baseRental = flatRate;
                txtSSize.Enabled = false;
                txtSSize.Text = "0";
            }
            else
            {
                txtSSize.Enabled = true;

                // ✅ Only clear when switching FROM Flat — not on every keystroke
                if (!txtSSize.Enabled || txtSSize.Text == "0")
                {
                    txtSSize.Text = "";
                    txtMRental.Text = "0.00";
                }

                if (!double.TryParse(txtSSize.Text, out double stallSize)) return;
                if (stallSize <= 0) return;

                baseRental = stallSize * ratePerSqm;
            }

            double.TryParse(txtAdditionalCharge.Text, out double additional);
            double total = baseRental + (chkAdditional.Checked ? additional : 0);

            txtMRental.Text = total.ToString("N2");
        }

        // ===================== SAVE ===================== //
        private void btnSave_Click(object sender, EventArgs e)
        {
            string fullName = txtFName.Text.Trim();
            string businessName = txtBName.Text.Trim();
            string businessSection = cmbBSection.SelectedItem?.ToString() ?? "";
            string stallNumber = txtSNumber.Text.Trim();
            string stallSize = txtSSize.Text.Trim();
            string paymentStatus = cmbPaymentStatus.SelectedItem?.ToString() ?? "Unpaid";
            string startDate = dtpStartDate.Value.ToString("yyyy-MM-dd");

            // ✅ Validate required fields
            if (string.IsNullOrWhiteSpace(fullName))
            {
                MessageBox.Show("Full Name is required.", "Required Field",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(businessName))
            {
                MessageBox.Show("Business Name is required.", "Required Field",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBName.Focus();
                return;
            }

            if (cmbBSection.SelectedIndex == -1)
            {
                MessageBox.Show("Business Section is required.", "Required Field",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbBSection.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(stallNumber))
            {
                MessageBox.Show("Stall Number is required.", "Required Field",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSNumber.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(stallSize) || stallSize == "0")
            {
                var (_, _, rateType) = Database.GetRateBySection(businessSection);
                if (rateType != "Flat")
                {
                    MessageBox.Show("Stall Size is required.", "Required Field",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSSize.Focus();
                    return;
                }
            }

            // ✅ Check duplicate stall number
            string excludeSIN = isEditMode ? currentSIN : "";
            if (Database.StallNumberExists(stallNumber, excludeSIN))
            {
                MessageBox.Show(
                    $"Stall Number '{stallNumber}' is already assigned to another stall owner.\n\n" +
                    "Please enter a different Stall Number.",
                    "Duplicate Stall Number",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtSNumber.Focus();
                return;
            }

            if (!double.TryParse(txtMRental.Text, out double monthlyRental))
            {
                MessageBox.Show("Monthly Rental must be a valid number.");
                return;
            }

            double.TryParse(txtAdditionalCharge.Text, out double additionalCharge);

            long currentUserId = Session.CurrentUserId ?? 0;
            string orNumber = "";
            double penalty = 0;

            // Only ask for OR Number in EDIT mode when marking as Paid
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
                    penalty, additionalCharge, currentUserId);
            else
                SaveNew(fullName, businessName, businessSection,
                    stallNumber, stallSize, monthlyRental,
                    startDate, paymentStatus, orNumber,
                    penalty, additionalCharge, currentUserId);
        }

        private void SaveEdit(
            string fullName, string businessName, string businessSection,
            string stallNumber, string stallSize, double monthlyRental,
            string startDate, string paymentStatus, string orNumber,
            double penalty, double additionalCharge, long currentUserId)
        {
            var result = Database.UpdateProfiling(
                currentSIN, fullName, businessName,
                businessSection, stallNumber,
                stallSize, monthlyRental, startDate,
                additionalCharge);

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
            double penalty, double additionalCharge, long currentUserId)
        {
            var result = Database.AddProfiling(
                txtBIN.Text, fullName, businessName,
                businessSection, stallNumber,
                stallSize, monthlyRental, startDate,
                additionalCharge);

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
            else
            {
                MessageBox.Show(result.ErrorMessage ?? "Unknown error.");
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
            cmbBSection.SelectedIndex = -1;
            txtSNumber.Clear();
            txtSSize.Clear();
            txtMRental.Clear();
            txtAdditionalCharge.Text = "0.00";
            chkAdditional.Checked = false;
            cmbPaymentStatus.SelectedIndex = 0;
            dtpStartDate.Value = DateTime.Today;
        }

        // ===================== NAVIGATION ===================== //
        private void button1_Click(object sender, EventArgs e)
        {
            if (isEditMode)
                this.Close();
            else
            {
                var dashboard = new DashboardForm();
                dashboard.Show();
                this.Close();
            }
        }

        // ===================== EVENTS ===================== //
        private void txtMRental_Leave(object sender, EventArgs e)
        {
            if (double.TryParse(txtMRental.Text, out double value))
                txtMRental.Text = value.ToString("N2");
        }

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

        private void SetupSecurity()
        {
            txtBIN.KeyDown += (s, e) => { if (e.Control && e.KeyCode == Keys.V) e.SuppressKeyPress = true; };
            txtFName.KeyDown += (s, e) => { if (e.Control && e.KeyCode == Keys.V) e.SuppressKeyPress = true; };
            txtBName.KeyDown += (s, e) => { if (e.Control && e.KeyCode == Keys.V) e.SuppressKeyPress = true; };
            txtSNumber.KeyDown += (s, e) => { if (e.Control && e.KeyCode == Keys.V) e.SuppressKeyPress = true; };
            txtSSize.KeyDown += (s, e) => { if (e.Control && e.KeyCode == Keys.V) e.SuppressKeyPress = true; };
            txtAdditionalCharge.KeyDown += (s, e) => { if (e.Control && e.KeyCode == Keys.V) e.SuppressKeyPress = true; };

            txtBIN.ContextMenuStrip = new ContextMenuStrip();
            txtFName.ContextMenuStrip = new ContextMenuStrip();
            txtBName.ContextMenuStrip = new ContextMenuStrip();
            txtSNumber.ContextMenuStrip = new ContextMenuStrip();
            txtSSize.ContextMenuStrip = new ContextMenuStrip();
            txtAdditionalCharge.ContextMenuStrip = new ContextMenuStrip();
        }
    }
}