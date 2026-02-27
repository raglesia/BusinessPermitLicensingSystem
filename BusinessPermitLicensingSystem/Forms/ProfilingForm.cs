using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BusinessPermitLicensingSystem.Forms
{

    public partial class ProfilingForm : Form
    {
        private DataTable dtProfiles = new DataTable();
        public ProfilingForm()
        {
            InitializeComponent();

        }
        private void ProfilingForm_Load(object sender, EventArgs e)
        {
            AssignNewBIN();
            txtBIN.Focus();
            txtBIN.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            {
                string fullName = txtFName.Text.Trim();
                string businessName = txtBName.Text.Trim();
                string businessSection = txtBSection.Text.Trim();
                string stallNumber = txtSNumber.Text.Trim();
                string stallSize = txtSSize.Text.Trim();

                if (!double.TryParse(txtMRental.Text, out double monthlyRental))
                {
                    MessageBox.Show("Monthly Rental must be a valid number.","Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var result = Database.AddProfiling(
                    fullName,
                    businessName,
                    businessSection,
                    stallNumber,
                    stallSize,
                    monthlyRental
                );

                if (result.Success)
                {
                    MessageBox.Show("Record added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Optional: Clear fields after save
                    txtFName.Clear();
                    txtBName.Clear();
                    txtBSection.Clear();
                    txtSNumber.Clear();
                    txtSSize.Clear();
                    txtMRental.Clear();
                    AssignNewBIN();
                }
                else
                {
                    MessageBox.Show(result.ErrorMessage,
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
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
    }
}
    