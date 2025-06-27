using Monte_Karlo.Models;
using System.Collections.Concurrent;

namespace Monte_Karlo.Utilites
{
    public class PointsGenerator
    {
        // Мьютекс для синхронизации доступа к _currentPoints из разных потоков
        private readonly Mutex _mutex = new();

        // Хранит текущий набор точек
        private PointsData _currentPoints = new();

        /// <summary>
        /// Асинхронно генерирует случайные точки внутри квадрата, ограниченного радиусом круга,
        /// а затем вычисляет включённые и отсеянные точки.
        /// </summary>
        /// <param name="circle">Объект круга с параметрами</param>
        /// <param name="count">Количество точек для генерации</param>
        /// <param name="token">Токен отмены для прерывания операции</param>
        /// <returns></returns>
        public async Task GenerateRandomPointsAsync(Circle circle, int count, CancellationToken token)
        {
            try
            {
                // Захватываем мьютекс для исключительного доступа
                _mutex.WaitOne();

                // Проверяем, не отменена ли операция
                token.ThrowIfCancellationRequested();

                // Создаем новый контейнер для точек
                var newPoints = new PointsData();
                newPoints.Points = new List<PointF>(count);

                float radius = circle.radius;

                // Запускаем тяжелую работу в отдельном потоке
                await Task.Run(() =>
                {
                    var parallelOptions = new ParallelOptions
                    {
                        CancellationToken = token,
                        MaxDegreeOfParallelism = Environment.ProcessorCount
                    };

                    // Генерируем точки параллельно
                    GeneratePoints(newPoints, count, radius, parallelOptions);
                    token.ThrowIfCancellationRequested();

                    // Вычисляем точки, лежащие внутри круга
                    CalculateIncludedPoints(newPoints, radius, parallelOptions);
                    token.ThrowIfCancellationRequested();

                    // Вычисляем точки, отсеянные по дополнительному условию (с учётом параметра C круга)
                    CalculateCuttedPoints(newPoints, circle, parallelOptions);
                }, token);

                // Обновляем текущее состояние точек
                _currentPoints = newPoints;
            }
            finally
            {
                // Освобождаем мьютекс независимо от результата
                _mutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// Асинхронно пересчитывает отсеянные точки для текущего набора,
        /// либо вызывает генерацию точек, если их нет.
        /// </summary>
        public async Task CalculateCuttedPointsAsync(Circle circle, int count, CancellationToken token)
        {
            try
            {
                _mutex.WaitOne();
                token.ThrowIfCancellationRequested();

                // Если нет точек, генерируем их заново
                if (_currentPoints.Points.Count == 0)
                {
                    await GenerateRandomPointsAsync(circle, count, token);
                    return;
                }

                // Иначе пересчитываем отсеянные точки на основе текущих данных
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

        // Очищает текущий набор точек
        public void ClearPoints()
        {
            _currentPoints = new PointsData();
        }

        // Возвращает текущий набор точек
        public PointsData GetCurrentPoints()
        {
            return _currentPoints;
        }

        /// <summary>
        /// Генерирует заданное количество случайных точек внутри квадрата со стороной 2*radius,
        /// центрированного в начале координат.
        /// </summary>
        private static void GeneratePoints(PointsData pointsData, int count, float radius, ParallelOptions parallelOptions)
        {
            // Для каждого потока создаём свой экземпляр Random для избежания конфликтов
            var random = new ThreadLocal<Random>(() => new Random(Guid.NewGuid().GetHashCode()));

            var points = new PointF[count];

            // Параллельный цикл генерации точек
            Parallel.For(0, count, parallelOptions, i =>
            {
                // Координаты по X и Y случайны в диапазоне [-radius, radius]
                float x = (float)random.Value.NextDouble() * radius * 2 - radius;
                float y = (float)random.Value.NextDouble() * radius * 2 - radius;
                points[i] = new PointF(x, y);
            });

            pointsData.Points = points.ToList();
        }

        /// <summary>
        /// Вычисляет и сохраняет точки, лежащие внутри круга с данным радиусом.
        /// </summary>
        private static void CalculateIncludedPoints(PointsData pointsData, float radius, ParallelOptions parallelOptions)
        {
            float radiusSquared = radius * radius;
            var includedPoints = new ConcurrentBag<PointF>();

            // Параллельно проверяем расстояние от начала координат до каждой точки
            Parallel.ForEach(pointsData.Points, parallelOptions, point =>
            {
                float distanceSquared = point.X * point.X + point.Y * point.Y;

                // Если точка внутри круга (расстояние меньше радиуса), добавляем её в результат
                if (distanceSquared < radiusSquared)
                {
                    includedPoints.Add(point);
                }
            });

            pointsData.IncludedPoints = includedPoints.ToList();
        }

        /// <summary>
        /// Вычисляет "отсеянные" точки среди включённых, по дополнительному условию,
        /// зависящему от параметра C круга.
        /// </summary>
        private static void CalculateCuttedPoints(PointsData pointsData, Circle circle, ParallelOptions parallelOptions)
        {
            // Если нет включённых точек — выходим
            if (pointsData.IncludedPoints.Count == 0)
                return;

            var cuttedPoints = new ConcurrentBag<PointF>();
            Point center = circle.circleCenter;
            float C = circle.C;

            // Определяем направление отсечения в зависимости от C и центра круга
            bool lefter = C < center.X;
            float centerX = center.X;

            // Параллельно фильтруем точки по условию отсечения
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

            // Обновляем список отсечённых точек
            pointsData.CuttedPoints.Clear();
            pointsData.CuttedPoints = cuttedPoints.ToList();
        }
    }
}
