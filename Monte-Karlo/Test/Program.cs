using Monte_Karlo.Utilites.Calculators;
using Monte_Karlo.DataBase;
using Monte_Karlo.Models;
using Monte_Karlo.Utilites;
using Monte_Karlo.Utilites.Calculators;
using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Test
{
    internal class Program
    {
        private static DatabaseHelper _bd;
        private static PointsGenerator _pg = new PointsGenerator();
        private static Stopwatch[] stopwatchs = new Stopwatch[7];

        static async Task Main(string[] args)
        {
            for (int i = 0; i < stopwatchs.Length; i++)
            {
                stopwatchs[i] = new Stopwatch();
                stopwatchs[i].Start();
            }

            var random = new ThreadLocal<Random>(() => new Random(Guid.NewGuid().GetHashCode()));
            var radius = 2;
            var parallelOptions = new ParallelOptions
            {
                MaxDegreeOfParallelism = Environment.ProcessorCount
            };
            Point center = new Point(3, 1);
            Direction direction = Direction.horizontal;
            float C = 2;
            int count = 100_000_000;
            var points = new PointF[count];
            Parallel.For(0, count, parallelOptions, i =>
            {
                float x = (float)random.Value.NextDouble() * radius * 2 - radius;
                float y = (float)random.Value.NextDouble() * radius * 2 - radius;
                points[i] = new PointF(x, y);
            });
            stopwatchs[0].Stop();
            PointsData pointsData = new PointsData();
            pointsData.Points = points.ToList();
            stopwatchs[1].Stop();


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
            stopwatchs[2].Stop();

            pointsData.IncludedPoints = includedPoints.ToList();
            stopwatchs[3].Stop();


            var cuttedPoints = new ConcurrentBag<PointF>();
            if (direction == Direction.vertical)
            {
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
            }
            else // horizontal
            {
                bool downer = C < center.Y;
                float centerY = center.Y;

                Parallel.ForEach(pointsData.IncludedPoints, parallelOptions, point =>
                {
                    bool condition = downer
                        ? centerY + point.Y >= C
                        : centerY + point.Y <= C;

                    if (condition)
                    {
                        cuttedPoints.Add(point);
                    }
                });
            }
            stopwatchs[4].Stop();
            pointsData.CuttedPoints = cuttedPoints.ToList();
            stopwatchs[5].Stop();

            stopwatchs[6].Restart();
            await _pg.GenerateRandomPointsAsync(new Circle(), count, CancellationToken.None);
            stopwatchs[6].Stop();
            var results = stopwatchs.Select(sw => sw.Elapsed).ToList();
            Console.WriteLine(string.Join($"\n", results));
        }
    }
}
