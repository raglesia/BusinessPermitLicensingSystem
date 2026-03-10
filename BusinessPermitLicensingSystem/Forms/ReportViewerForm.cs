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
        private readonly ReportViewer _reportViewer;
        private readonly BillingReportModel _profile;

        // ===================== CONSTRUCTOR ===================== //
        public ReportViewerForm(BillingReportModel profile)
        {
            InitializeComponent();

            _profile = profile;

            _reportViewer = new ReportViewer { Dock = DockStyle.Fill };
            Controls.Add(_reportViewer);
        }

        // ===================== FORM LOAD ===================== //
        private void ReportViewerForm_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            string reportPath = FindReportPath();

            if (reportPath == null)
            {
                MessageBox.Show(
                    "BillingReport.rdlc not found.\n\n" +
                    "Make sure it's in the 'Report' or 'Reports' folder and set to 'Copy if newer'.",
                    "Report File Not Found",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            LoadReport(reportPath);
        }

        // ===================== REPORT HELPERS ===================== //
        private static string FindReportPath()
        {
            string[] folders = { "Report", "Reports" };

            foreach (string folder in folders)
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

            _reportViewer.LocalReport.ReportPath = reportPath;
            _reportViewer.LocalReport.DataSources.Clear();
            _reportViewer.LocalReport.DataSources.Add(
                new ReportDataSource("BillingReportDataSet", data));

            _reportViewer.LocalReport.SetParameters(new[]
            {
                new ReportParameter("ProcessedBy",      Session.CurrentFullName          ?? ""),
                new ReportParameter("Position",         Session.CurrentPosition          ?? ""),
                new ReportParameter("PaymentStatus",    _profile.PaymentStatus           ?? "Unpaid"),
                new ReportParameter("Penalty",          _profile.Penalty.ToString("F2")),
                new ReportParameter("AdditionalCharge", _profile.AdditionalCharge.ToString("F2")),
                new ReportParameter("TotalDue",         _profile.TotalDue.ToString("F2")),
            });

            _reportViewer.RefreshReport();
            _reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
            _reportViewer.ZoomMode = ZoomMode.Percent;
            _reportViewer.ZoomPercent = 100;
        }
    }
}