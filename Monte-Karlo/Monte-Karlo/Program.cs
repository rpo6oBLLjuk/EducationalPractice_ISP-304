using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Monte_Karlo
{
    internal static class Program
    {
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            if (System.Diagnostics.Process.GetProcessesByName(System.Diagnostics.Process.GetCurrentProcess().ProcessName).Length > 1)
            {
                Process target = System.Diagnostics.Process.GetProcessesByName(System.Diagnostics.Process.GetCurrentProcess().ProcessName)[0];
                IntPtr handle = target.MainWindowHandle;

                if (handle != IntPtr.Zero)
                {
                    SetForegroundWindow(handle);
                }
                return;
            }
            Application.Run(new SplashScreenForm());
        }
    }
}