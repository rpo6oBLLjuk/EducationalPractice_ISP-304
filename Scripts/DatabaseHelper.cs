using Microsoft.EntityFrameworkCore;
using Monte_Karlo.Models;
using Monte_Karlo.Utilites;

namespace Monte_Karlo.DataBase
{
    public class DatabaseHelper
    {
        private Logger logger;

        public DatabaseHelper()
        {
            logger = new Logger(); // Инициализация логгера
            InitializeDatabase();  // Проверка или создание базы данных
        }

        // Инициализация базы данных
        public void InitializeDatabase()
        {
            using var context = new AppDbContext();
            if (context.Database.CanConnect()) // Проверка подключения
                return;
            context.Database.EnsureCreated(); // Создание базы данных при отсутствии
            logger.Log("Создание базы данных");
        }

        // Полная очистка базы данных с пересозданием
        public void ClearDatabase()
        {
            using (var context = new AppDbContext())
            {
                context.Database.EnsureDeleted(); // Удаление существующей базы
                context.Database.EnsureCreated(); // Создание новой базы
            }
            logger.Log("Очистка базы данных");
        }

        // Сохранение результатов моделирования в базу данных
        public void SaveResults(Circle circle, PointsData pointsData, double analyticalResult, double monteCarloResult)
        {
            using var context = new AppDbContext();
            int totalPoints = pointsData.Points.Count; // Общее количество точек
            int pointsInSegment = pointsData.CuttedPoints.Count; // Количество точек в сегменте

            // Попытка найти уже существующие параметры круга
            var circleParams = context.CircleParams
                .Include(cp => cp.Results)
                .FirstOrDefault(cp =>
                    cp.TotalPoints == totalPoints);

            // Если не найдено — создаём новый объект
            if (circleParams == null)
            {
                circleParams = new CircleParams
                {
                    TotalPoints = totalPoints,
                    AnalyticalResult = analyticalResult
                };
                context.CircleParams.Add(circleParams);
            }

            // Создание результата моделирования
            var result = new SimulationResult
            {
                CircleParams = circleParams,
                Points = totalPoints,
                PointsInSegment = pointsInSegment,
                MonteCarloResult = monteCarloResult
            };

            context.SimulationResults.Add(result); // Добавление в базу
            context.SaveChanges(); // Сохранение изменений
            logger.Log($"Сохранение:\n{circleParams}\n{result}");
        }

        // Получение данных по количеству точек
        public CircleParams GetData(Circle circle, int totalPoints)
        {
            using var context = new AppDbContext();
            var query = context.CircleParams
                .Include(cp => cp.Results)
                .Where(cp =>
                    cp.TotalPoints == totalPoints);
            return query.FirstOrDefault();
        }

        // Получение данных по ID записи
        public CircleParams GetDataById(int selectedId)
        {
            using var context = new AppDbContext();
            var result = context.CircleParams
                .Include(cp => cp.Results)
                .FirstOrDefault(cp => cp.Id == selectedId);
            return result;
        }

        // Получение всех записей из базы
        public List<CircleParams> GetAllData()
        {
            using var context = new AppDbContext();
            var results = context.CircleParams
                    .Include(cp => cp.Results)
                    .ToList();
            return results;
        }

        // Удаление записи по ID
        public void RemoveCircleParamsById(int selectedId)
        {
            using var context = new AppDbContext();
            var experiment = context.CircleParams
                .Include(cp => cp.Results)
                .FirstOrDefault(cp => cp.Id == selectedId);

            if (experiment != null)
            {
                context.CircleParams.Remove(experiment);
                context.SaveChanges();
            }
            logger.Log($"Удаление:\n{experiment}");
        }

        //// Создание резервной копии файла базы данных
        //public string CreateBackup(string fileName) 
        //{
        //    var currentDirectory = Directory.GetCurrentDirectory();
        //    var sourcePath = Path.Combine(currentDirectory, "DataBase.db");

        //    File.Copy(sourcePath, fileName, true);
        //    string message = $"Резервная копия создана: {Path.GetFileName(fileName)}";
        //    logger.Log(message);
        //    return Path.GetFileName(fileName);
        //}
    }
}
