namespace TestProject.Models
{
    [TestClass]
    public class CircleParamsTests
    {
        // Проверяет, что конструктор по умолчанию инициализирует коллекцию Results
        [TestMethod]
        public void CircleParams_DefaultConstructor_InitializesResultsList()
        {
            // Arrange & Act
            var circleParams = new CircleParams();

            // Assert
            Assert.IsNotNull(circleParams.Results);
            Assert.AreEqual(0, circleParams.Results.Count);
        }

        // Проверяет корректность установки и получения значений всех свойств класса
        [TestMethod]
        public void CircleParams_Properties_CanBeSetAndGet()
        {
            // Arrange
            var circleParams = new CircleParams
            {
                Id = 1,
                TotalPoints = 10000,
                AnalyticalResult = 78.54
            };

            // Act & Assert
            Assert.AreEqual(1, circleParams.Id);
            Assert.AreEqual(10000, circleParams.TotalPoints);
            Assert.AreEqual(78.54, circleParams.AnalyticalResult);
        }

        // Проверяет возможность добавления элементов в коллекцию Results
        [TestMethod]
        public void CircleParams_Results_CanBeAdded()
        {
            // Arrange
            var circleParams = new CircleParams();
            var result = new SimulationResult { Id = 1 };

            // Act
            circleParams.Results.Add(result);

            // Assert
            Assert.AreEqual(1, circleParams.Results.Count);
            Assert.AreEqual(1, circleParams.Results[0].Id);
        }

        // Проверяет, что метод ToString возвращает строку с ожидаемым форматом и данными
        [TestMethod]
        public void CircleParams_ToString_ReturnsCorrectFormat()
        {
            // Arrange
            var circleParams = new CircleParams
            {
                Id = 1,
                TotalPoints = 10000,
                AnalyticalResult = 78.54
            };

            // Act
            var result = circleParams.ToString();

            // Assert
            StringAssert.Contains(result, "Id: 1");
            StringAssert.Contains(result, "TotalPoints: 10000");
            StringAssert.Contains(result, "AnalyticalResult: 78,54");
            StringAssert.Contains(result, "ResultsCount: 0");
        }
    }
}