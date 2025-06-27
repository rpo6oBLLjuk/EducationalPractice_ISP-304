using File = System.IO.File;

namespace TestProject
{
    [TestClass]
    [DoNotParallelize]
    public class LoggerTests
    {
        private Logger _logger;
        private string _testLogDir;

        [TestInitialize]
        public void TestInitialize()
        {
            _logger = new Logger();
            _testLogDir = Path.Combine(Application.StartupPath, "Logs");

            // Очищаем директорию с логами перед каждым тестом
            if (Directory.Exists(_testLogDir))
            {
                Directory.Delete(_testLogDir, true);
            }
        }

        [TestCleanup]
        public void TestCleanup()
        {
            // Удаляем тестовую директорию после каждого теста
            if (Directory.Exists(_testLogDir))
            {
                Directory.Delete(_testLogDir, true);
            }
        }

        // Проверяет, что метод Log создает директорию для логов, если она не существует
        [TestMethod]
        public void Log_CreatesLogDirectoryIfNotExists()
        {
            // Arrange
            string testMessage = "Test log message";

            // Act
            _logger.Log(testMessage);

            // Assert
            Assert.IsTrue(Directory.Exists(_testLogDir), "Log directory should be created");
        }

        // Проверяет, что метод Log создает файл лога с текущей датой в имени
        [TestMethod]
        public void Log_CreatesLogFileWithCurrentDate()
        {
            // Arrange
            string testMessage = "Test log message";
            string expectedFileName = DateTime.Now.ToString("yyyy-MM-dd") + ".log";
            string expectedFilePath = Path.Combine(_testLogDir, expectedFileName);

            // Act
            _logger.Log(testMessage);

            // Assert
            Assert.IsTrue(File.Exists(expectedFilePath), "Log file should be created with current date");
        }

        // Проверяет, что метод Log добавляет сообщение в файл лога с правильным форматом
        [TestMethod]
        public void Log_WritesMessageWithTimestamp()
        {
            // Arrange
            string testMessage = "Test log message";
            string expectedFileName = DateTime.Now.ToString("yyyy-MM-dd") + ".log";
            string expectedFilePath = Path.Combine(_testLogDir, expectedFileName);

            // Act
            _logger.Log(testMessage);

            // Assert
            string logContent = File.ReadAllText(expectedFilePath);
            StringAssert.Contains(logContent, DateTime.Now.ToString("HH:mm:ss"));
            StringAssert.Contains(logContent, testMessage);
        }

        // Проверяет, что метод LogException создает файл ошибки с правильным именем
        [TestMethod]
        public void LogException_CreatesErrorLogFile()
        {
            // Arrange
            var exception = new Exception("Test exception");
            string testMessage = "Test error message";
            string expectedFileName = DateTime.Now.ToString("error") + ".log";
            string expectedFilePath = Path.Combine(_testLogDir, expectedFileName);

            // Act
            _logger.LogException(exception, testMessage);

            // Assert
            Assert.IsTrue(File.Exists(expectedFilePath), "Error log file should be created");
        }

        // Проверяет, что метод LogException записывает полную информацию об исключении
        [TestMethod]
        public void LogException_WritesFullExceptionDetails()
        {
            // Arrange
            var exception = new Exception("Test exception");
            string testMessage = "Test error message";
            string expectedFileName = "error.log";
            string expectedFilePath = Path.Combine(_testLogDir, expectedFileName);

            // Act
            _logger.LogException(exception, testMessage);

            // Assert
            string logContent = File.ReadAllText(expectedFilePath);
            StringAssert.Contains(logContent, exception.Message);
            StringAssert.Contains(logContent, testMessage);
        }

        // Проверяет, что метод Log обрабатывает ошибки записи в лог
        [TestMethod]
        public void Log_HandlesWriteErrorsGracefully()
        {
            // Arrange
            string testMessage = "Test log message";

            // Создаем директорию без прав на запись
            Directory.CreateDirectory(_testLogDir);
            File.SetAttributes(_testLogDir, FileAttributes.ReadOnly);

            // Act
            try
            {
                _logger.Log(testMessage);

                // Assert
                // Если мы дошли сюда, значит исключение не было брошено
                Assert.IsTrue(true);
            }
            finally
            {
                File.SetAttributes(_testLogDir, FileAttributes.Normal);
            }
        }

        // Проверяет, что метод LogException обрабатывает ошибки записи в лог
        [TestMethod]
        public void LogException_HandlesWriteErrorsGracefully()
        {
            // Arrange
            var exception = new Exception("Test exception");
            string testMessage = "Test error message";

            // Создаем директорию без прав на запись
            Directory.CreateDirectory(_testLogDir);
            File.SetAttributes(_testLogDir, FileAttributes.ReadOnly);

            // Act
            try
            {
                _logger.LogException(exception, testMessage);

                // Assert
                // Если мы дошли сюда, значит исключение не было брошено
                Assert.IsTrue(true);
            }
            finally
            {
                File.SetAttributes(_testLogDir, FileAttributes.Normal);
            }
        }
    }
}