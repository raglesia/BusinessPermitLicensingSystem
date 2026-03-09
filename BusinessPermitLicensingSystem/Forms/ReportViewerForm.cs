using BusinessPermitLicensingSystem.Models;
using Microsoft.Reporting.WinForms;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace BusinessPermitLicensingSystem
{
    public partial class ReportViewerForm : Form
    {
        // ===================== FIELDS ===================== //
        private ReportViewer reportViewer1;
        private BillingReportModel _profile;

        // ===================== CONSTRUCTOR ===================== //
        public ReportViewerForm(BillingReportModel profile)
        {
            InitializeComponent();

            _profile = profile;
            reportViewer1 = new ReportViewer { Dock = DockStyle.Fill };

            Controls.Add(reportViewer1);
        }

        // ===================== FORM LOAD ===================== //
        private void ReportViewerForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            string reportPath = FindReportPath();

            if (reportPath == null)
            {
                MessageBox.Show(
                    "BillingReport.rdlc not found. Make sure it's in the 'Report' or 'Reports' folder and set to 'Copy if newer'.",
                    "File Not Found",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            LoadReport(reportPath);
        }

        // ===================== REPORT SETUP ===================== //
        private string FindReportPath()
        {
            string[] possibleFolders = { "Report", "Reports" };

            foreach (var folder in possibleFolders)
            {
                string path = Path.Combine(
                    Application.StartupPath, folder, "BillingReport.rdlc");

                if (File.Exists(path))
                    return path;
            }

            return null;
        }
       private void LoadReport(string reportPath)
        {
            var data = new List<BillingReportModel> { _profile };

            reportViewer1.LocalReport.ReportPath = reportPath;
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("BillingReportDataSet", data));

            reportViewer1.LocalReport.SetParameters(new[]
            {
        new ReportParameter("ProcessedBy",      Session.CurrentFullName  ?? ""),
        new ReportParameter("Position",         Session.CurrentPosition  ?? ""),
        new ReportParameter("PaymentStatus",    _profile.PaymentStatus   ?? "Unpaid"),
        new ReportParameter("Penalty",          _profile.Penalty.ToString("F2")),
        new ReportParameter("AdditionalCharge", _profile.AdditionalCharge.ToString("F2")),
        new ReportParameter("TotalDue",         _profile.TotalDue.ToString("F2")),
    });

            reportViewer1.RefreshReport();
            reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            reportViewer1.ZoomMode = ZoomMode.Percent;
            reportViewer1.ZoomPercent = 100;
        }
    }
}