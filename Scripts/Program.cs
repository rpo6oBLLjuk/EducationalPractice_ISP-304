using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Monte_Karlo
{
    internal static class Program
    {
        // ����������� ������� �� user32.dll ��� ��������� ���� �� �������� ����
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        // ������� ����� ����� � ����������, � ��������� STA (Single Thread Apartment) ������ ������
        [STAThread]
        static void Main()
        {
            // ������������� ������������ ���������� (��������� ���������� ������, DPI � �.�.)
            ApplicationConfiguration.Initialize();

            // ���������, ������� �� ��� ���� �� ������� (����������)
            if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1)
            {
                // ���� �������� ��������� �����������, ���� ������ �� ���
                Process target = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName)[0];
                IntPtr handle = target.MainWindowHandle;

                // ���� � ���������� �������� ���� ������� ����,
                // ��������� ��� � �������� (�� �������� ����)
                if (handle != IntPtr.Zero)
                {
                    SetForegroundWindow(handle);
                }

                // ��������� ������� ���������, ����� �� ��������� ������
                return;
            }

            // ���� ��� ������ ��������� ����������, ��������� ������� ���� SplashScreenForm
            Application.Run(new SplashScreenForm());
        }
    }
}
