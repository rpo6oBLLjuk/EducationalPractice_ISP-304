namespace TestProject.Models
{
    [TestClass]
    public class CircleTests
    {
        // Проверяем конструкторы по умолчанию
        [TestMethod]
        public void Circle_DefaultConstructor_SetsDefaultValues()
        {
            // Arrange & Act
            var circle = new Circle();

            // Assert
            Assert.AreEqual(-3, circle.circleCenter.X);
            Assert.AreEqual(1, circle.circleCenter.Y);
            Assert.AreEqual(2, circle.radius);
            Assert.AreEqual(-2, circle.C);
        }

        // Проверяем конструкторы со своими значениями
        [TestMethod]
        public void Circle_ParameterizedConstructor_SetsCorrectValues()
        {
            // Arrange
            var center = new Point(5, 5);
            float radius = 10;
            float c = 3;

            // Act
            var circle = new Circle(center, radius, c);

            // Assert
            Assert.AreEqual(center.X, circle.circleCenter.X);
            Assert.AreEqual(center.Y, circle.circleCenter.Y);
            Assert.AreEqual(radius, circle.radius);
            Assert.AreEqual(c, circle.C);
        }

        // Тестируем метод Equals для сравнения объектов
        [TestMethod]
        public void Circle_Equals_ReturnsTrueForEqualCircles()
        {
            // Arrange
            var circle1 = new Circle(new Point(1, 2), 3, 4);
            var circle2 = new Circle(new Point(1, 2), 3, 4);

            // Act & Assert
            Assert.IsTrue(circle1.Equals(circle2));
        }

        // Тестируем метод Equals для сравнения объектов
        [TestMethod]
        public void Circle_Equals_ReturnsFalseForDifferentCircles()
        {
            // Arrange
            var circle1 = new Circle(new Point(1, 2), 3, 4);
            var circle2 = new Circle(new Point(1, 2), 3, 4);
            var circle3 = new Circle(new Point(1, 3), 3, 4);
            var circle4 = new Circle(new Point(1, 2), 4, 4);
            var circle5 = new Circle(new Point(1, 2), 3, 5);

            // Act & Assert
            Assert.IsTrue(circle1.Equals(circle2));

            Assert.IsFalse(circle1.Equals(circle3));
            Assert.IsFalse(circle1.Equals(circle4));
            Assert.IsFalse(circle1.Equals(circle5));
        }

        // Тестируем метод Equals на null
        [TestMethod]
        public void Circle_Equals_ReturnsFalseForNull()
        {
            // Arrange
            var circle = new Circle();

            // Act & Assert
            Assert.IsFalse(circle.Equals(null));
        }

        // Проверяем GetHashCode для равных объектов
        [TestMethod]
        public void Circle_GetHashCode_ReturnsSameForEqualObjects()
        {
            // Arrange
            var circle1 = new Circle(new Point(1, 2), 3, 4);
            var circle2 = new Circle(new Point(1, 2), 3, 4);

            // Act & Assert
            Assert.AreEqual(circle1.GetHashCode(), circle2.GetHashCode());
        }

        // Тестируем ToString на корректность форматирования
        [TestMethod]
        public void Circle_ToString_ReturnsCorrectFormat()
        {
            // Arrange
            var circle = new Circle(new Point(1, 2), 3, 4);

            // Act
            var result = circle.ToString();

            // Assert
            StringAssert.Contains(result, "CircleCenter: {X=1,Y=2}");
            StringAssert.Contains(result, "Radius: 3");
            StringAssert.Contains(result, "C: 4");
        }
    }
}