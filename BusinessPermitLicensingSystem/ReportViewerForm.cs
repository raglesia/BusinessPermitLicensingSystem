using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BusinessPermitLicensingSystem.Models;
using BusinessPermitLicensingSystem.Report;
using Microsoft.Reporting.WinForms; 

namespace BusinessPermitLicensingSystem
{
    public partial class ReportViewerForm : Form
    {
        private ReportViewer reportViewer1;

        private BillingReportModel _profile;

        public ReportViewerForm(BillingReportModel profile)
        {
            InitializeComponent();

            _profile = profile;

            reportViewer1 = new ReportViewer();
            reportViewer1.Dock = DockStyle.Fill;

            Controls.Add(reportViewer1);
        }



        private void ReportViewerForm_Load(object sender, EventArgs e)
        {
            // Open maximized
            this.WindowState = FormWindowState.Maximized;

            // Allow proper text encoding (RDLC requirement)
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            // Wrap the selected record in a List (RDLC expects a collection)
            var data = new List<BillingReportModel> { _profile };

            // Look for the RDLC in either "Report" or "Reports" folder
            string[] possibleFolders = { "Report", "Reports" };
            string reportPath = null;

            foreach (var folder in possibleFolders)
            {
                string path = Path.Combine(Application.StartupPath, folder, "BillingReport.rdlc");
                if (File.Exists(path))
                {
                    reportPath = path;
                    break;
                }
            }

            if (reportPath == null)
            {
                MessageBox.Show(
                    "BillingReport.rdlc not found. Make sure it's in the 'Report' or 'Reports' folder and set to 'Copy if newer'.",
                    "File Not Found",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            // Assign report path
            reportViewer1.LocalReport.ReportPath = reportPath;

            // Clear existing data sources and add the selected record
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("BillingReportDataSet", data)); // must match your RDLC DataSet name

            reportViewer1.LocalReport.SetParameters(new[]
            {
                new ReportParameter("ProcessedBy", Session.CurrentFullName ?? ""),
                new ReportParameter("Position", Session.CurrentPosition ?? "")
            });

            // Refresh report
            reportViewer1.RefreshReport();

            // Optional: professional print preview
            reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            reportViewer1.ZoomMode = ZoomMode.PageWidth;

        }
    }
}
