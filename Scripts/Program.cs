using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Monte_Karlo
{
    internal static class Program
    {
        // Импортируем функцию из user32.dll для установки окна на передний план
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        // Главная точка входа в приложение, с указанием STA (Single Thread Apartment) модели потока
        [STAThread]
        static void Main()
        {
            // Инициализация конфигурации приложения (настройки визуальных стилей, DPI и т.д.)
            ApplicationConfiguration.Initialize();

            // Проверяем, запущен ли уже этот же процесс (приложение)
            if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1)
            {
                // Если запущено несколько экземпляров, берём первый из них
                Process target = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName)[0];
                IntPtr handle = target.MainWindowHandle;

                // Если у найденного процесса есть главное окно,
                // переводим его в активное (на передний план)
                if (handle != IntPtr.Zero)
                {
                    SetForegroundWindow(handle);
                }

                // Завершаем текущий экземпляр, чтобы не запускать второй
                return;
            }

            // Если это первый экземпляр приложения, запускаем главное окно SplashScreenForm
            Application.Run(new SplashScreenForm());
        }
    }
}
