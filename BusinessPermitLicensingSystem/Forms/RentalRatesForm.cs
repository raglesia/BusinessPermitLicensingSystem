using BusinessPermitLicensingSystem.Helpers;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace BusinessPermitLicensingSystem.Forms
{
    public partial class RentalRatesForm : Form
    {
        // ===================== FIELDS ===================== //
        private DataGridView dgvRates;
        private TextBox txtRatePerSqm;
        private TextBox txtFlatRate;
        private ComboBox cmbRateType;
        private Label lblSection;
        private Button btnSave;
        private string selectedSection = "";

        // ===================== CONSTRUCTOR ===================== //
        public RentalRatesForm()
        {
            SetupUI();
        }

        // ===================== UI SETUP ===================== //
        private void SetupUI()
        {
            SetupForm();
            SetupHeader();
            SetupGrid();
            SetupEditPanel();
            SetupFooter();
            LoadRates();
        }

        private void SetupForm()
        {
            this.Text = "Manage Rental Rates";
            this.Size = new Size(1050, 700);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.White;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Font = new Font("Segoe UI", 10);
            this.Icon = new Icon(Path.Combine(
                Application.StartupPath, "Resources", "MasinlocLogoIcon.ico"));
        }

        private void SetupHeader()
        {
            var pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.FromArgb(30, 60, 90)
            };
            this.Controls.Add(pnlHeader);

            var lblTitle = new Label
            {
                Text = "Manage Rental Rates",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(15, 8),
                AutoSize = true
            };
            pnlHeader.Controls.Add(lblTitle);

            var lblSubtitle = new Label
            {
                Text = "Click a section to edit its rate",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.LightSteelBlue,
                Location = new Point(15, 35),
                AutoSize = true
            };
            pnlHeader.Controls.Add(lblSubtitle);
        }

        private void SetupGrid()
        {
            dgvRates = new DataGridView
            {
                Location = new Point(15, 75),
                Size = new Size(580, 530),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                Font = new Font("Segoe UI", 9),
                MultiSelect = false
            };

            // ✅ Double buffering
            typeof(DataGridView)
                .GetProperty("DoubleBuffered",
                    System.Reflection.BindingFlags.Instance |
                    System.Reflection.BindingFlags.NonPublic)!
                .SetValue(dgvRates, true);

            dgvRates.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 9, FontStyle.Bold);
            dgvRates.RowsDefaultCellStyle.BackColor = Color.White;
            dgvRates.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;

            // ✅ Load selected row into edit panel
            dgvRates.SelectionChanged += (s, e) => LoadSelectedRate();

            this.Controls.Add(dgvRates);
        }

        private void SetupEditPanel()
        {
            var pnlEdit = new Panel
            {
                Location = new Point(615, 75),
                Size = new Size(410, 520), // ✅ Matches visible area
                BackColor = Color.FromArgb(245, 245, 245)
            };
            this.Controls.Add(pnlEdit);

            lblSection = new Label
            {
                Text = "Select a section to edit",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 60, 90),
                Location = new Point(15, 12),
                Size = new Size(380, 30),
                AutoSize = false
            };
            pnlEdit.Controls.Add(lblSection);

            var divider = new Label
            {
                Size = new Size(380, 2),
                Location = new Point(15, 47),
                BackColor = Color.FromArgb(30, 60, 90)
            };
            pnlEdit.Controls.Add(divider);

            // Rate Type
            var lblRateType = new Label
            {
                Text = "Rate Type:",
                Location = new Point(15, 58),
                AutoSize = true
            };
            pnlEdit.Controls.Add(lblRateType);

            cmbRateType = new ComboBox
            {
                Location = new Point(15, 75),
                Size = new Size(380, 30),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10)
            };
            cmbRateType.Items.AddRange(new string[] { "PerSqm", "Flat" });
            cmbRateType.SelectedIndexChanged += (s, e) => ToggleRateFields();
            pnlEdit.Controls.Add(cmbRateType);

            // Rate Per Sqm
            var lblRatePerSqm = new Label
            {
                Text = "Rate Per Sqm (₱):",
                Location = new Point(15, 115),
                AutoSize = true
            };
            pnlEdit.Controls.Add(lblRatePerSqm);

            txtRatePerSqm = new TextBox
            {
                Location = new Point(15, 133),
                Size = new Size(380, 30),
                Font = new Font("Segoe UI", 10)
            };
            txtRatePerSqm.KeyPress += (s, e) =>
                InputValidator.AllowDecimalNumbers(e, txtRatePerSqm);
            pnlEdit.Controls.Add(txtRatePerSqm);

            // Flat Rate
            var lblFlatRate = new Label
            {
                Text = "Flat Rate (₱):",
                Location = new Point(15, 173),
                AutoSize = true
            };
            pnlEdit.Controls.Add(lblFlatRate);

            txtFlatRate = new TextBox
            {
                Location = new Point(15, 191),
                Size = new Size(380, 30),
                Font = new Font("Segoe UI", 10)
            };
            txtFlatRate.KeyPress += (s, e) =>
                InputValidator.AllowDecimalNumbers(e, txtFlatRate);
            pnlEdit.Controls.Add(txtFlatRate);

            var divider2 = new Label
            {
                Size = new Size(380, 2),
                Location = new Point(15, 235),
                BackColor = Color.Silver
            };
            pnlEdit.Controls.Add(divider2);

            // ✅ Section Name label — hidden by default
            var lblNewSection = new Label
            {
                Text = "Section Name:",
                Location = new Point(15, 245),
                AutoSize = true,
                Visible = false
            };
            pnlEdit.Controls.Add(lblNewSection);

            // ✅ Section Name textbox — hidden by default
            var txtNewSection = new TextBox
            {
                Name = "txtNewSection",
                Location = new Point(15, 268),
                Size = new Size(380, 30),
                Font = new Font("Segoe UI", 10),
                Visible = false
            };
            pnlEdit.Controls.Add(txtNewSection);

            // Save button
            btnSave = new Button
            {
                Text = "Save Changes",
                Location = new Point(15, 245),
                Size = new Size(380, 40),
                BackColor = Color.FromArgb(30, 60, 90),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Enabled = false
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += btnSave_Click;
            pnlEdit.Controls.Add(btnSave);

            // Add New Section button
            var btnAddNew = new Button
            {
                Text = "+ Add New Section",
                Location = new Point(15, 300),
                Size = new Size(380, 40),
                BackColor = Color.FromArgb(40, 130, 90),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10),
                Cursor = Cursors.Hand
            };
            btnAddNew.FlatAppearance.BorderSize = 0;
            pnlEdit.Controls.Add(btnAddNew);

            btnAddNew.Click += (s, e) =>
            {
                bool isAdding = !txtNewSection.Visible;
                txtNewSection.Visible = isAdding;
                lblNewSection.Visible = isAdding;
                btnAddNew.Text = isAdding ? "✕ Cancel" : "+ Add New Section";
                btnAddNew.BackColor = isAdding
                    ? Color.IndianRed
                    : Color.FromArgb(40, 130, 90);

                if (isAdding)
                {
                    lblNewSection.Location = new Point(15, 245);
                    txtNewSection.Location = new Point(15, 268);
                    btnSave.Location = new Point(15, 310);
                    btnAddNew.Location = new Point(15, 365);

                    lblSection.Text = "New Section";
                    txtNewSection.Clear();
                    txtNewSection.Focus();
                    btnSave.Enabled = true;
                    btnSave.Text = "Save New Section";
                    selectedSection = "";
                }
                else
                {
                    btnSave.Location = new Point(15, 245);
                    btnAddNew.Location = new Point(15, 300);

                    lblSection.Text = "Select a section to edit";
                    btnSave.Enabled = false;
                    btnSave.Text = "Save Changes";
                    selectedSection = "";
                }
            };
        }

        private void SetupFooter()
        {
            var pnlFooter = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 45,
                BackColor = Color.FromArgb(240, 240, 240)
            };
            this.Controls.Add(pnlFooter);

            var btnClose = new Button
            {
                Text = "Close",
                Size = new Size(85, 28),
                BackColor = Color.IndianRed,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9),
                Cursor = Cursors.Hand,
                Location = new Point(15, 8)
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, e) => this.Close();
            pnlFooter.Controls.Add(btnClose);
        }

        // ===================== LOAD ===================== //
        private void LoadRates()
        {
            dgvRates.DataSource = Database.GetRentalRates();

            // ✅ Hide unnecessary columns
            dgvRates.DataBindingComplete += (s, e) =>
            {
                if (dgvRates.Columns["RatePerSqm"] != null)
                    dgvRates.Columns["RatePerSqm"].HeaderText = "Rate/Sqm (₱)";
                if (dgvRates.Columns["FlatRate"] != null)
                    dgvRates.Columns["FlatRate"].HeaderText = "Flat Rate (₱)";
                if (dgvRates.Columns["RateType"] != null)
                    dgvRates.Columns["RateType"].HeaderText = "Type";
            };
        }

        private void LoadSelectedRate()
        {
            if (dgvRates.SelectedRows.Count == 0) return;

            var row = dgvRates.SelectedRows[0];

            selectedSection = row.Cells["Section"].Value?.ToString() ?? "";
            string rateType = row.Cells["RateType"].Value?.ToString() ?? "PerSqm";
            double ratePerSqm = Convert.ToDouble(row.Cells["RatePerSqm"].Value ?? 0);
            double flatRate = Convert.ToDouble(row.Cells["FlatRate"].Value ?? 0);

            lblSection.Text = selectedSection;
            cmbRateType.SelectedItem = rateType;
            txtRatePerSqm.Text = ratePerSqm.ToString("N2");
            txtFlatRate.Text = flatRate.ToString("N2");
            btnSave.Enabled = true;
        }

        private void ToggleRateFields()
        {
            bool isFlat = cmbRateType.SelectedItem?.ToString() == "Flat";
            txtRatePerSqm.Enabled = !isFlat;
            txtFlatRate.Enabled = isFlat;
            txtRatePerSqm.BackColor = isFlat ? Color.LightGray : Color.White;
            txtFlatRate.BackColor = isFlat ? Color.White : Color.LightGray;

            if (isFlat)
                txtRatePerSqm.Text = "0.00";
            else
                txtFlatRate.Text = "0.00";
        }

        // ===================== SAVE ===================== //
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(txtRatePerSqm.Text, out double ratePerSqm))
                ratePerSqm = 0;
            if (!double.TryParse(txtFlatRate.Text, out double flatRate))
                flatRate = 0;

            string rateType = cmbRateType.SelectedItem?.ToString() ?? "PerSqm";

            // ✅ Validate rate values
            if (rateType == "PerSqm" && ratePerSqm <= 0)
            {
                MessageBox.Show(
                    "Rate per sqm must be greater than 0.",
                    "Invalid Rate",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (rateType == "Flat" && flatRate <= 0)
            {
                MessageBox.Show(
                    "Flat rate must be greater than 0.",
                    "Invalid Rate",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // ✅ Check if adding new or editing existing
            bool isAddingNew = string.IsNullOrWhiteSpace(selectedSection);

            if (isAddingNew)
            {
                // Find txtNewSection inside pnlEdit
                var txtNewSection = this.Controls
                    .OfType<Panel>()
                    .SelectMany(p => p.Controls.OfType<TextBox>())
                    .FirstOrDefault(t => t.Name == "txtNewSection");

                string newSection = txtNewSection?.Text.Trim() ?? "";

                if (string.IsNullOrWhiteSpace(newSection))
                {
                    MessageBox.Show(
                        "Please enter a section name.",
                        "Required",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                var confirm = MessageBox.Show(
                    $"Add new section '{newSection}'?\n\n" +
                    $"Rate Type : {rateType}\n" +
                    $"Rate/Sqm  : ₱{ratePerSqm:N2}\n" +
                    $"Flat Rate : ₱{flatRate:N2}",
                    "Confirm Add",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirm != DialogResult.Yes) return;

                var result = Database.AddRentalRate(
                    newSection, ratePerSqm, flatRate, rateType);

                if (result.Success)
                {
                    Database.LogAudit(
                        "AddRate", null,
                        Session.CurrentUserId ?? 0,
                        $"Added new section: {newSection}, {rateType}, " +
                        $"PerSqm=₱{ratePerSqm:N2}, Flat=₱{flatRate:N2}");

                    MessageBox.Show(
                        $"Section '{newSection}' added successfully!",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    dgvRates.DataSource = null;
                    LoadRates();
                }
                else
                {
                    MessageBox.Show(
                        result.ErrorMessage ?? "Unknown error.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            else
            {
                // ✅ Editing existing section
                var confirm = MessageBox.Show(
                    $"Update rate for {selectedSection}?\n\n" +
                    $"Rate Type : {rateType}\n" +
                    $"Rate/Sqm  : ₱{ratePerSqm:N2}\n" +
                    $"Flat Rate : ₱{flatRate:N2}",
                    "Confirm Update",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirm != DialogResult.Yes) return;

                var result = Database.UpdateRentalRate(
                    selectedSection, ratePerSqm, flatRate, rateType);

                if (result.Success)
                {
                    Database.LogAudit(
                        "UpdateRate", null,
                        Session.CurrentUserId ?? 0,
                        $"Updated rate for {selectedSection}: " +
                        $"{rateType}, PerSqm=₱{ratePerSqm:N2}, Flat=₱{flatRate:N2}");

                    MessageBox.Show(
                        "Rental rate updated successfully!",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    dgvRates.DataSource = null;
                    LoadRates();
                }
                else
                {
                    MessageBox.Show(
                        result.ErrorMessage ?? "Unknown error.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }
    }
}