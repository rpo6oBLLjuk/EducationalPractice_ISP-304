namespace Monte_Karlo
{
    public partial class SplashScreenForm : Form
    {
        private int _time = 0;           // Счётчик времени, прошедшего с запуска заставки
        private int _timeout = 3;        // Время (в секундах), через которое заставка автоматически закроется

        public SplashScreenForm()
        {
            InitializeComponent();      // Инициализация компонентов формы
        }

        // Обработчик нажатия кнопки старта — открывает главное окно и скрывает заставку
        private void startButton_Click(object sender, EventArgs e)
        {
            var form = new MainForm();  // Создание главной формы
            form.Show();                // Показ главной формы
            this.Hide();               // Скрытие текущей (заставки)
        }

        // Событие загрузки заставки — запускает таймер
        private void Screensaver_Load(object sender, EventArgs e)
        {
            timer1.Start();            // Запуск таймера
        }

        // Событие тика таймера — отслеживает прошедшее время и автоматически нажимает кнопку
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (++_time >= _timeout)   // Если прошло достаточно времени
            {
                timer1.Stop();         // Остановка таймера
                startButton.PerformClick(); // Программный клик по кнопке старта
            }
        }
    }
}
