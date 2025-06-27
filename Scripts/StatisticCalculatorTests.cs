namespace TestProject
{
    [TestClass]
    public class StatisticCalculatorTests
    {
        // Проверяет правильность вычисления медианы при нечётном количестве элементов
        [TestMethod]
        public void CalculateMedian_ReturnsCorrectForOddCount()
        {
            // Arrange
            var values = new List<double> { 1, 3, 5, 7, 9 };
            double expected = 5;

            // Act
            double result = StatisticCalculator.CalculateMedian(values);

            // Assert
            Assert.AreEqual(expected, result);
        }

        // Проверяет правильность вычисления медианы при чётном количестве элементов
        [TestMethod]
        public void CalculateMedian_ReturnsCorrectForEvenCount()
        {
            // Arrange
            var values = new List<double> { 1, 3, 5, 7, 9, 11 };
            double expected = 6;

            // Act
            double result = StatisticCalculator.CalculateMedian(values);

            // Assert
            Assert.AreEqual(expected, result);
        }

        // Проверяет правильность вычисления размаха
        [TestMethod]
        public void CalculateRange_ReturnsDifferenceBetweenMaxAndMin()
        {
            // Arrange
            var values = new List<double> { 1, 5, 3, 9, 2 };
            double expected = 8;

            // Act
            double result = StatisticCalculator.CalculateRange(values);

            // Assert
            Assert.AreEqual(expected, result);
        }
    }
}