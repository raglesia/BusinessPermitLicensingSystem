using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BusinessPermitLicensingSystem.Forms
{
    public partial class DashboardForm : Form
    {
        public DashboardForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProfilingLists profilingLists = new ProfilingLists();

            profilingLists.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ProfilingForm profilingForm = new ProfilingForm();

            profilingForm.Show();
            this.Hide();
        }
    }
}
