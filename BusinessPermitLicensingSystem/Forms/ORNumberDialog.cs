using System.Drawing;
using System.Windows.Forms;

namespace BusinessPermitLicensingSystem.Forms
{
    public partial class ORNumberDialog : Form
    {
        // ===================== PROPERTIES ===================== //
        public string ORNumber { get; private set; } = "";

        // ===================== CONTROLS ===================== //
        private TextBox txtORNumber;

        // ===================== CONSTRUCTOR ===================== //
        public ORNumberDialog()
        {
            InitializeComponent();
            SetupUI();
        }

        // ===================== UI SETUP ===================== //
        private void SetupUI()
        {
            // Form settings
            this.Text = "OR Number Required";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Size = new Size(380, 180);
            this.BackColor = Color.White;

            // Label
            var lbl = new Label
            {
                Text = "Enter Official Receipt (OR) Number:",
                Location = new Point(15, 20),
                AutoSize = true,
                Font = new Font("Segoe UI", 10)
            };
            this.Controls.Add(lbl);

            // TextBox
            txtORNumber = new TextBox
            {
                Location = new Point(15, 50),
                Size = new Size(335, 30),
                Font = new Font("Segoe UI", 10),
                MaxLength = 50
            };
            this.Controls.Add(txtORNumber);

            // Confirm Button
            var btnConfirm = new Button
            {
                Text = "Confirm",
                Location = new Point(170, 100),
                Size = new Size(85, 32),
                BackColor = Color.SeaGreen,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9)
            };
            btnConfirm.Click += BtnConfirm_Click;
            this.Controls.Add(btnConfirm);

            // Cancel Button
            var btnCancel = new Button
            {
                Text = "Cancel",
                Location = new Point(265, 100),
                Size = new Size(85, 32),
                BackColor = Color.IndianRed,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9)
            };
            btnCancel.Click += BtnCancel_Click;
            this.Controls.Add(btnCancel);

            // Keyboard shortcuts
            this.AcceptButton = btnConfirm;
            this.CancelButton = btnCancel;
        }

        // ===================== EVENTS ===================== //
        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtORNumber.Text))
            {
                MessageBox.Show(
                    "Please enter the OR Number.",
                    "Required",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtORNumber.Focus();
                return;
            }

            ORNumber = txtORNumber.Text.Trim();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}