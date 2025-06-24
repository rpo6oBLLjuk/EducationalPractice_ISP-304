using Monte_Karlo.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Monte_Karlo.Utilites
{
    public class PointsGenerator
    {
        private readonly Mutex _mutex = new();
        private PointsData _currentPoints = new();

        public async Task GenerateRandomPointsAsync(Circle circle, int count, CancellationToken token)
        {
            try
            {
                _mutex.WaitOne();
                token.ThrowIfCancellationRequested();

                var newPoints = new PointsData();
                newPoints.Points = new List<PointF>(count);

                float radius = circle.radius;

                await Task.Run(() =>
                {
                    var parallelOptions = new ParallelOptions
                    {
                        CancellationToken = token,
                        MaxDegreeOfParallelism = Environment.ProcessorCount
                    };

                    GeneratePoints(newPoints, count, radius, parallelOptions);
                    token.ThrowIfCancellationRequested();
                    CalculateIncludedPoints(newPoints, radius, parallelOptions);
                    token.ThrowIfCancellationRequested();
                    CalculateCuttedPoints(newPoints, circle, parallelOptions);
                }, token);

                _currentPoints = newPoints;
            }
            finally
            {
                _mutex.ReleaseMutex();
            }
        }

        public async Task CalculateCuttedPointsAsync(Circle circle, int count, CancellationToken token)
        {
            try
            {
                _mutex.WaitOne();
                token.ThrowIfCancellationRequested();

                if (_currentPoints.Points.Count == 0)
                {
                    await GenerateRandomPointsAsync(circle, count, token);
                    return;
                }

                await Task.Run(() =>
                {
                    var parallelOptions = new ParallelOptions
                    {
                        CancellationToken = token,
                        MaxDegreeOfParallelism = Environment.ProcessorCount
                    };
                    CalculateCuttedPoints(_currentPoints, circle, parallelOptions);
                }, token);
            }
            finally
            {
                _mutex.ReleaseMutex();
            }
        }

        public void ClearPoints()
        {
            _currentPoints = new PointsData();
        }

        public PointsData GetCurrentPoints()
        {
            return _currentPoints;
        }

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