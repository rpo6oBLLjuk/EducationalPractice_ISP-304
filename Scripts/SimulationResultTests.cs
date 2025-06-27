namespace TestProject.Models
{
    [TestClass]
    public class SimulationResultTests
    {
        // Проверяет корректность установки и получения значений всех свойств класса
        [TestMethod]
        public void SimulationResult_Properties_CanBeSetAndGet()
        {
            // Arrange
            var result = new SimulationResult
            {
                Id = 1,
                Points = 100,
                PointsInSegment = 50,
                MonteCarloResult = 3.14,
                CircleParamsId = 2
            };

            // Act & Assert
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual(100, result.Points);
            Assert.AreEqual(50, result.PointsInSegment);
            Assert.AreEqual(3.14, result.MonteCarloResult);
            Assert.AreEqual(2, result.CircleParamsId);
        }


        // Проверяет возможность установки и получения связанного объекта CircleParams
        [TestMethod]
        public void SimulationResult_CircleParams_CanBeSet()
        {
            // Arrange
            var result = new SimulationResult();
            var circleParams = new CircleParams { Id = 1 };

            // Act
            result.CircleParams = circleParams;

            // Assert
            Assert.IsNotNull(result.CircleParams);
            Assert.AreEqual(1, result.CircleParams.Id);
        }


        // Проверяет, что метод ToString возвращает строку с ожидаемым форматом и данными
        [TestMethod]
        public void SimulationResult_ToString_ReturnsCorrectFormat()
        {
            // Arrange
            var result = new SimulationResult
            {
                Id = 1,
                Points = 100,
                PointsInSegment = 50,
                MonteCarloResult = 3.14,
                CircleParamsId = 2
            };

            // Act
            var str = result.ToString();

            // Assert
            StringAssert.Contains(str, "Id: 1");
            StringAssert.Contains(str, "Points: 100");
            StringAssert.Contains(str, "PointsInSegment: 50");
            StringAssert.Contains(str, "MonteCarloResult: 3,14");
            StringAssert.Contains(str, "CircleParamsId: 2");
        }
    }
}