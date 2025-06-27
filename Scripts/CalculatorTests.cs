namespace TestProject
{
    [TestClass]
    public class CalculatorTests
    {
        // Проверяем правильность вычисления площади круга
        [TestMethod]
        public void CircleSuare_CalculatesCorrectArea()
        {
            // Arrange
            double radius = 5;
            double expected = Math.PI * 25;

            // Act
            double result = Calculator.CircleSuare(radius);

            // Assert
            Assert.AreEqual(expected, result);
        }

        // Проверяем правильность вычисления метод Монте-Карло
        [TestMethod]
        public void CalculateMonteCarloArea_ReturnsCorrectValue()
        {
            // Arrange
            float radius = 2;
            int allPoints = 1000;
            int cuttedPoints = 785;
            double expected = (785d / 1000d) * 16; // (cutted/all) * (4*radius^2)

            // Act
            double result = Calculator.CalculateMonteCarloArea(radius, allPoints, cuttedPoints);

            // Assert
            Assert.AreEqual(expected, result);
        }

        // Проверяем правильность вычисления абсолютной погрешности
        [TestMethod]
        public void CalculateAbsoluteError_ReturnsCorrectValue()
        {
            // Arrange
            double expected = 10.5;
            double actual = 9.8;
            double expectedError = 0.7;

            // Act
            double result = Calculator.CalculateAbsoluteError(expected, actual);

            // Assert
            Assert.AreEqual(expectedError, result, 0.0001);
        }

        // Проверяем правильность вычисления относительной погрешности
        [TestMethod]
        public void CalculateRelativeError_ReturnsCorrectPercentage()
        {
            // Arrange
            double expected = 100;
            double actual = 95;
            double expectedError = 5; // 5%

            // Act
            double result = Calculator.CalculateRelativeError(expected, actual);

            // Assert
            Assert.AreEqual(expectedError, result, 0.0001);
        }

        // Проверяем реакцию вычисления погрешности при некорректных значениях
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateRelativeError_ThrowsForZeroExpected()
        {
            // Arrange
            double expected = 0;
            double actual = 95;

            // Act
            Calculator.CalculateRelativeError(expected, actual);
        }

        // Проверяем правильность округления до двух значащих цифр
        [TestMethod]
        public void RoundToTwoSignificantDigits_RoundsCorrectly()
        {
            // Arrange
            double value1 = 123.456;
            double value2 = 0.0123456;
            double expected1 = 120;
            double expected2 = 0.012;

            // Act
            double result1 = Calculator.RoundToTwoSignificantDigits(value1, 2);
            double result2 = Calculator.RoundToTwoSignificantDigits(value2, 2);

            // Assert
            Assert.AreEqual(expected1, result1);
            Assert.AreEqual(expected2, result2);
        }

        // Проверяем вычисление площади, когда секущая линия вне окружности
        [TestMethod]
        public void CalculateAnalyticArea_HorizontalCut_ReturnsFullCircleWhenLineOutside()
        {
            // Arrange
            var circle = new Circle(new Point(0, 0), 5, 10);
            double expected = Math.PI * 25;

            // Act
            double result = Calculator.CalculateAnalyticArea(circle);

            // Assert
            Assert.AreEqual(expected, result, 0.0001);
        }

        // Проверяем вычисление площади, когда секущая линия проходит через центр окружности
        [TestMethod]
        public void CalculateAnalyticArea_VerticalCut_ReturnsHalfCircleWhenLineThroughCenter()
        {
            // Arrange
            var circle = new Circle(new Point(0, 0), 5, 0);
            double expected = Math.PI * 25 / 2;

            // Act
            double result = Calculator.CalculateAnalyticArea(circle);

            // Assert
            Assert.AreEqual(expected, result, 0.0001);
        }

        // Проверяем правильность вычисление площади большей секции окружности разделённой секущей линией
        // параллельной оси OX
        [TestMethod]
        public void CalculateAnalyticArea_HorizontalCut_ReturnsCorrectSegmentArea()
        {
            // Arrange
            var circle = new Circle(new Point(0, 0), 5, 3);
            double circleArea = Math.PI * 25;
            double segmentArea = 25 * Math.Acos(3.0 / 5.0) - 3 * Math.Sqrt(25 - 9);
            double expected = circleArea - segmentArea;

            // Act
            double result = Calculator.CalculateAnalyticArea(circle);

            // Assert
            Assert.AreEqual(expected, result, 0.0001);
        }

        // Проверяем рекцию вычисления площади при некорректных значениях
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateAnalyticArea_ThrowsForZeroRadius()
        {
            // Arrange
            var circle = new Circle(new Point(0, 0), 0, 0);

            // Act
            Calculator.CalculateAnalyticArea(circle);
        }
    }
}