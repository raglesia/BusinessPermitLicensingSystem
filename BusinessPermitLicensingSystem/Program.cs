using BusinessPermitLicensingSystem.Forms;

namespace BusinessPermitLicensingSystem
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // ✅ Fix blurry fonts on high DPI screens
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Database.Initialize();

            Application.Run(new LogInForm());
        }
    }
}