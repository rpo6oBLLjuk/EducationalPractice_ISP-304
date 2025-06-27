namespace TestProject
{
    [TestClass]
    [DoNotParallelize]
    public class DatabaseHelperTests
    {
        private DatabaseHelper _dbHelper;

        [TestInitialize]
        public void TestInitialize()
        {
            _dbHelper = new DatabaseHelper();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            using var context = new AppDbContext();
            context.Database.EnsureDeleted();
        }

        // Проверяет, что база данных инициализируется корректно
        [TestMethod]
        public void InitializeDatabase_CreatesDatabaseIfNotExists()
        {
            // Arrange
            using var context = new AppDbContext();
            context.Database.EnsureCreated();

            // Act
            _dbHelper.InitializeDatabase();

            // Assert
            Assert.IsTrue(context.Database.CanConnect());
        }

        // Проверяет, что метод ClearDatabase корректно очищает базу данных
        [TestMethod]
        public void ClearDatabase_DeletesAndRecreatesDatabase()
        {
            // Arrange
            _dbHelper.InitializeDatabase();

            // Act
            _dbHelper.ClearDatabase();

            // Assert
            using var context = new AppDbContext();
            Assert.IsTrue(context.Database.CanConnect());
            Assert.AreEqual(0, context.CircleParams.Count());
        }

        // Проверяет сохранение результатов в базу данных
        [TestMethod]
        public void SaveResults_SavesNewCircleParamsAndResults()
        {
            // Arrange
            var circle = new Circle(new Point(1, 2), 3, 4);
            var pointsData = new PointsData
            {
                Points = Enumerable.Repeat(new PointF(0, 0), 1000).ToList(),
                IncludedPoints = Enumerable.Repeat(new PointF(0, 0), 785).ToList(),
                CuttedPoints = Enumerable.Repeat(new PointF(0, 0), 500).ToList()
            };
            double analyticalResult = 28.2743;
            double monteCarloResult = 28.26;

            // Act
            _dbHelper.SaveResults(circle, pointsData, analyticalResult, monteCarloResult);

            // Assert
            using var context = new AppDbContext();
            var savedParams = context.CircleParams.First();
            var savedResult = context.SimulationResults.First();

            Assert.AreEqual(1, context.CircleParams.Count());
            Assert.AreEqual(1, context.SimulationResults.Count());
            Assert.AreEqual(pointsData.Points.Count, savedParams.TotalPoints);
            Assert.AreEqual(monteCarloResult, savedResult.MonteCarloResult);
        }

        // Проверяет, что при повторном сохранении с одинаковыми параметрами добавляется только новый результат
        [TestMethod]
        public void SaveResults_AddsNewResultForExistingParams()
        {
            // Arrange
            var circle = new Circle(new Point(1, 2), 3, 4);
            var pointsData = new PointsData
            {
                Points = Enumerable.Repeat(new PointF(0, 0), 1000).ToList(),
                IncludedPoints = Enumerable.Repeat(new PointF(0, 0), 785).ToList(),
                CuttedPoints = Enumerable.Repeat(new PointF(0, 0), 500).ToList()
            };

            // Первое сохранение
            _dbHelper.SaveResults(circle, pointsData, 28.2743, 28.26);

            // Act - второе сохранение с теми же параметрами
            _dbHelper.SaveResults(circle, pointsData, 28.2743, 28.30);

            // Assert
            using var context = new AppDbContext();
            Assert.AreEqual(1, context.CircleParams.Count(), string.Join("\n", context.CircleParams));
            Assert.AreEqual(2, context.SimulationResults.Count(), string.Join("\n", context.SimulationResults));
        }

        // Проверяет получение данных по параметрам круга
        [TestMethod]
        public void GetData_ReturnsCorrectCircleParams()
        {
            // Arrange
            var circle = new Circle(new Point(0, 0), 3, 4);
            var pointsData = new PointsData
            {
                Points = Enumerable.Repeat(new PointF(0, 0), 1000).ToList(),
                IncludedPoints = Enumerable.Repeat(new PointF(0, 0), 785).ToList(),
                CuttedPoints = Enumerable.Repeat(new PointF(0, 0), 500).ToList()
            };
            _dbHelper.SaveResults(circle, pointsData, 28.2743, 28.26);

            // Act
            var result = _dbHelper.GetData(circle, 1000);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Results.Count, string.Join(", ", result.Results));
        }

        // Проверяет получение данных по ID
        [TestMethod]
        public void GetDataById_ReturnsCorrectCircleParams()
        {
            // Arrange
            var circle = new Circle(new Point(1, 2), 3, 4);
            var pointsData = new PointsData
            {
                Points = Enumerable.Repeat(new PointF(0, 0), 1000).ToList(),
                IncludedPoints = Enumerable.Repeat(new PointF(0, 0), 785).ToList(),
                CuttedPoints = Enumerable.Repeat(new PointF(0, 0), 500).ToList()
            };
            _dbHelper.SaveResults(circle, pointsData, 28.2743, 28.26);
            int id = 1; // Первая запись обычно имеет ID = 1

            // Act
            var result = _dbHelper.GetDataById(id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.Id);
        }

        // Проверяет получение всех данных из базы
        [TestMethod]
        public void GetAllData_ReturnsAllSavedRecords()
        {
            // Arrange
            var circle = new Circle(new Point(1, 2), 3, 4);
            var pointsData = new PointsData
            {
                Points = Enumerable.Repeat(new PointF(0, 0), 1000).ToList(),
                IncludedPoints = Enumerable.Repeat(new PointF(0, 0), 785).ToList(),
                CuttedPoints = Enumerable.Repeat(new PointF(0, 0), 500).ToList()
            };

            _dbHelper.SaveResults(circle, pointsData, 28.2743, 28.26);

            // Act
            var results = _dbHelper.GetAllData();

            // Assert
            Assert.AreEqual(1, results.Count);
        }

        // Проверяет удаление записи по ID
        [TestMethod]
        public void RemoveCircleParamsById_DeletesRecord()
        {
            // Arrange
            var circle = new Circle(new Point(1, 2), 3, 4);
            var pointsData = new PointsData
            {
                Points = Enumerable.Repeat(new PointF(0, 0), 1000).ToList(),
                IncludedPoints = Enumerable.Repeat(new PointF(0, 0), 785).ToList(),
                CuttedPoints = Enumerable.Repeat(new PointF(0, 0), 500).ToList()
            };
            _dbHelper.SaveResults(circle, pointsData, 28.2743, 28.26);
            int id = 1;

            // Act
            _dbHelper.RemoveCircleParamsById(id);

            // Assert
            using var context = new AppDbContext();
            Assert.IsNull(context.CircleParams.Find(id));
            Assert.AreEqual(0, context.SimulationResults.Count(), string.Join("\n", context.SimulationResults));
        }
    }
}