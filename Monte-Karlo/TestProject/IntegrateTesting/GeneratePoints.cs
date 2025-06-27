namespace TestProject.IntegrateTesting
{
    [TestClass]
    public class GeneratePointsTest
    {
        private Circle circle = new Circle();
        private int pointsCount = 100_000;

        // Проверка корректности результата при стартовых параметрах окружности по умолчанию
        [TestMethod]
        public void GeneratePoints_CheckCorrectWorkWithStartParametrs_ReturnCorrectResult()
        {
            // Arrange
            double expectedRealSquer = 10.1096d;
            double expectedMonteCarloPercentError = 0.05d;
            double expectedDelta = expectedRealSquer * expectedMonteCarloPercentError;

            // Act
            var currentPoints = LocalGenerator(circle, pointsCount);
            double realSquare = Calculator.CalculateAnalyticArea(circle);
            var roundedRealSquare = Math.Round(realSquare, 4);

            double monteCarloSquare = Calculator.CalculateMonteCarloArea(
                circle.radius,
                currentPoints.Points.Count,
                currentPoints.CuttedPoints.Count);
            var roundedMonteCarloSquare = Math.Round(monteCarloSquare, 4);

            // Assert
            Assert.AreEqual(expectedRealSquer, realSquare, 0.0001);
            Assert.AreEqual(realSquare, monteCarloSquare, expectedDelta);
        }

        // Имитация PointsGenerator GenerateRandomPointsAsync(), так как тот многопоточный класс не тестируемый
        private PointsData LocalGenerator(Circle circle, int count)
        {
            var newPoints = new PointsData();
            newPoints.Points = new List<PointF>(count);

            float radius = circle.radius;

            var parallelOptions = new ParallelOptions
            {
                MaxDegreeOfParallelism = Environment.ProcessorCount
            };

            GeneratePoints(newPoints, count, radius, parallelOptions);
            CalculateIncludedPoints(newPoints, radius, parallelOptions);
            CalculateCuttedPoints(newPoints, circle, parallelOptions);

            return newPoints;
        }

        // Генерация случайных точек
        private static void GeneratePoints(PointsData pointsData, int count, float radius, ParallelOptions parallelOptions)
        {
            var random = new ThreadLocal<Random>(() => new Random(Guid.NewGuid().GetHashCode()));

            var points = new PointF[count];
            Parallel.For(0, count, parallelOptions, i =>
            {
                float x = (float)random.Value.NextDouble() * radius * 2 - radius;
                float y = (float)random.Value.NextDouble() * radius * 2 - radius;
                points[i] = new PointF(x, y);
            });

            pointsData.Points = points.ToList();
        }

        // Фильтрация точек, попавших в окружность
        private static void CalculateIncludedPoints(PointsData pointsData, float radius, ParallelOptions parallelOptions)
        {
            float radiusSquared = radius * radius;
            var includedPoints = new ConcurrentBag<PointF>();

            Parallel.ForEach(pointsData.Points, parallelOptions, point =>
            {
                float distanceSquared = point.X * point.X + point.Y * point.Y;

                if (distanceSquared < radiusSquared)
                {
                    includedPoints.Add(point);
                }
            });

            pointsData.IncludedPoints = includedPoints.ToList();
        }

        // Фильтрация точек, попавших в больший сегмент окружности
        private static void CalculateCuttedPoints(PointsData pointsData, Circle circle, ParallelOptions parallelOptions)
        {
            if (pointsData.IncludedPoints.Count == 0)
                return;

            var cuttedPoints = new ConcurrentBag<PointF>();
            Point center = circle.circleCenter;
            float C = circle.C;

            bool lefter = C < center.X;
            float centerX = center.X;

            Parallel.ForEach(pointsData.IncludedPoints, parallelOptions, point =>
            {
                bool condition = lefter
                    ? point.X + centerX >= C
                    : point.X + centerX <= C;

                if (condition)
                {
                    cuttedPoints.Add(point);
                }
            });

            pointsData.CuttedPoints.Clear();
            pointsData.CuttedPoints = cuttedPoints.ToList();
        }
    }
}
