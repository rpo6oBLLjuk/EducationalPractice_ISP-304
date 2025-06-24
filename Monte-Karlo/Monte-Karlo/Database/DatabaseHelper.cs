using Microsoft.EntityFrameworkCore;
using Monte_Karlo.Models;
using Monte_Karlo.Utilites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monte_Karlo.DataBase
{
    public class DatabaseHelper
    {
        private Logger logger;
        public DatabaseHelper()
        {
            logger = new Logger();
            InitializeDatabase();
        }

        public void InitializeDatabase()
        {
            using var context = new AppDbContext();
            if (context.Database.CanConnect())
                return;
            context.Database.EnsureCreated();
            logger.Log("Создание базы данных");
        }

        public void ClearDatabase()
        {
            using (var context = new AppDbContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
            logger.Log("Очистка базы данных");
        }

        public void SaveResults(Circle circle, PointsData pointsData, double analyticalResult, double monteCarloResult)
        {
            using var context = new AppDbContext();
            int totalPoints = pointsData.Points.Count;
            int pointsInCircle = pointsData.IncludedPoints.Count;
            int pointsInSegment = pointsData.CuttedPoints.Count;

            var circleParams = context.CircleParams
                .Include(cp => cp.Results)
                .FirstOrDefault(cp =>
                    cp.CenterX == circle.circleCenter.X &&
                    cp.CenterY == circle.circleCenter.Y &&
                    cp.Radius == circle.radius &&
                    cp.C == circle.C &&
                    cp.TotalPoints == totalPoints);

            if (circleParams == null)
            {
                circleParams = new CircleParams
                {
                    CenterX = circle.circleCenter.X,
                    CenterY = circle.circleCenter.Y,
                    Radius = circle.radius,
                    C = circle.C,
                    TotalPoints = totalPoints,
                    AnalyticalResult = analyticalResult
                };
                context.CircleParams.Add(circleParams);
            }

            var result = new SimulationResult
            {
                CircleParams = circleParams,
                PointsInCircle = pointsInCircle,
                PointsInSegment = pointsInSegment,
                MonteCarloResult = monteCarloResult
            };

            context.SimulationResults.Add(result);
            context.SaveChanges();
            logger.Log($"Сохранение:\n{circleParams}\n{result}");
        }

        public CircleParams GetData(Circle circle, int totalPoints)
        {
            using var context = new AppDbContext();
            var query = context.CircleParams
                .Include(cp => cp.Results)
                .Where(cp =>
                    cp.CenterX == circle.circleCenter.X &&
                    cp.CenterY == circle.circleCenter.Y &&
                    cp.Radius == circle.radius &&
                    cp.C == circle.C &&
                    cp.TotalPoints == totalPoints);
            return query.FirstOrDefault();
        }

        public CircleParams GetDataById(int selectedId)
        {
            using var context = new AppDbContext();
            var result = context.CircleParams
                .Include(cp => cp.Results)
                .FirstOrDefault(cp => cp.Id == selectedId);
            return result;
        }

        public List<CircleParams> GetAllData()
        {
            using var context = new AppDbContext();
            var results = context.CircleParams
                    .Include(cp => cp.Results)
                    .ToList();
            return results;
        }

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

        public string CreateBackup(string fileName) 
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var sourcePath = Path.Combine(currentDirectory, "DataBase.db");

            File.Copy(sourcePath, fileName, true);
            string message = $"Резервная копия создана: {Path.GetFileName(fileName)}";
            logger.Log(message);
            return Path.GetFileName(fileName);
        }
    }
}
