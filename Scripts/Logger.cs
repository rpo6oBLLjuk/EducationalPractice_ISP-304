namespace Monte_Karlo.Utilites
{
    public class Logger
    {
        // Путь к папке с логами — в папке "Logs" рядом с исполняемым файлом приложения
        private string logDir = Path.Combine(Application.StartupPath, "Logs");

        // Метод записи общего лог-сообщения в файл с текущей датой
        public void Log(string message)
        {
            // Путь к файлу лога для текущей даты, формат: "yyyy-MM-dd.log"
            string logFile = Path.Combine(logDir, DateTime.Now.ToString("yyyy-MM-dd") + ".log");

            try
            {
                // Создаем папку Logs, если ее нет
                if (!Directory.Exists(logDir))
                    Directory.CreateDirectory(logDir);

                // Формируем строку с временной меткой (часы:минуты:секунды) и сообщением
                string timestamp = DateTime.Now.ToString("HH:mm:ss");
                string line = $"[{timestamp}] {message}";

                // Добавляем строку в файл с новой строкой
                File.AppendAllText(logFile, line + Environment.NewLine);

                // Также выводим сообщение в отладочный вывод (Visual Studio Output)
                System.Diagnostics.Debug.WriteLine("[LOG] " + line);
            }
            catch (Exception ex)
            {
                // Если произошла ошибка при логировании, выводим сообщение об ошибке
                System.Diagnostics.Debug.WriteLine("[LOG ERROR] " + ex.Message);

                // Пытаемся зафиксировать ошибку логирования в отдельном лог-файле
                LogException(ex, message);
            }
        }

        // Метод записи исключений (ошибок) в отдельный лог
        // Можно передать дополнительное сообщение
        public void LogException(Exception exception, string message = "")
        {
            // Сначала выводим сообщение об ошибке и стек вызовов в отладочный вывод
            System.Diagnostics.Debug.WriteLine($"[ERROR] {message} {exception.Message}\n{exception.StackTrace}");

            // Путь к файлу логов ошибок (с меткой "error")
            string logFile = Path.Combine(logDir, DateTime.Now.ToString("error") + ".log");

            try
            {
                // Создаем папку Logs, если ее нет
                if (!Directory.Exists(logDir))
                    Directory.CreateDirectory(logDir);

                // Формируем строку с временной меткой и полной информацией об исключении
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd - HH:mm:ss");
                string line = $"[{timestamp}] {message} {exception.Message}\n{exception.StackTrace}";

                // Добавляем строку в файл логов ошибок
                File.AppendAllText(logFile, line + Environment.NewLine);

                // И выводим ее в отладочный вывод
                System.Diagnostics.Debug.WriteLine("[LOG] " + line);
            }
            catch (Exception ex)
            {
                // Если и здесь ошибка, выводим ее в отладочный вывод, чтобы не терять информацию
                System.Diagnostics.Debug.WriteLine("[LOG ERROR] " + ex.Message);
            }
        }
    }
}
