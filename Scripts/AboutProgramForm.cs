using System.Diagnostics;

namespace Monte_Karlo
{
    // Частичная реализация формы "О программе"
    public partial class AboutProgramForm : Form
    {
        // Конструктор формы
        public AboutProgramForm()
        {
            InitializeComponent(); // Инициализация компонентов формы (создаётся автоматически дизайнером)
        }

        // Обработчик события клика по ссылке на GitHub
        private void githubLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                // Создание процесса для открытия ссылки в браузере по умолчанию
                Process.Start(new ProcessStartInfo
                {
                    FileName = "https://github.com/rpo6oBLLjuk/EducationalPractice_ISP-304", // URL репозитория
                    UseShellExecute = true // Указывает системе использовать оболочку для открытия ссылки
                });
            }
            catch (Exception ex)
            {
                // В случае ошибки — вывод сообщения пользователю
                MessageBox.Show($"Не удалось открыть ссылку: {ex.Message}",
                                "Ошибка",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }
    }
}
