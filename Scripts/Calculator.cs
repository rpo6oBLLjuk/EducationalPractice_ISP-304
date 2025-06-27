using Monte_Karlo.Models;

namespace Monte_Karlo.Utilites.Calculators
{
    // Статический класс для вычислений, связанных с кругом и методом Монте-Карло
    public static class Calculator
    {
        // Метод для аналитического вычисления площади фигуры, образованной пересечением круга и вертикальной прямой x = C
        public static double CalculateAnalyticArea(Circle circle)
        {
            Point center = circle.circleCenter;
            double R = circle.radius;
            double C = circle.C;

            if (R == 0)
                throw new ArgumentException("R == 0"); // Радиус не может быть нулём

            double xLine = C;                      // Положение вертикальной линии x = C
            double d = Math.Abs(center.X - xLine);  // Расстояние от центра круга до этой линии (хорды)
            double h = Math.Abs(R - d);              // Расстояние от хорды до окружности (высота сегмента)
            double CircleArea = Math.PI * R * R;     // Полная площадь круга

            if (d >= R)
                return CircleArea;  // Если линия не пересекает круг, площадь равна полной площади круга
            if (h == R)
                return CircleArea / 2;  // Если линия проходит через центр круга (хорда — диаметр), площадь — половина круга

            double segmentArea = GetSegmentArea(R, d);  // Площадь сегмента круга, отсекаемого линией
            return CircleArea - segmentArea;  // Площадь части круга слева от линии (или справа, в зависимости от контекста)
        }

        // Вспомогательный метод для вычисления площади сегмента круга по радиусу и расстоянию до хорды (https://en.wikipedia.org/wiki/Circular_segment)
        private static double GetSegmentArea(double R, double d)
        {
            // Формула площади сегмента круга
            return R * R * Math.Acos(d / R) - d * Math.Sqrt(R * R - d * d);
        }

        // Метод для вычисления площади круга по радиусу
        public static double CircleSuare(double R) => Math.PI * R * R;

        // Метод для оценки площади методом Монте-Карло
        // radius - радиус круга, allPoints - всего точек, cuttedPoints - точек, попавших внутрь круга
        public static double CalculateMonteCarloArea(float radius, int allPoints, int cuttedPoints)
        {
            double squareArea = 4 * radius * radius; // Площадь квадратной области, в которой генерируются точки (-R до R по X и Y)
            return cuttedPoints / (double)allPoints * squareArea; // Оценка площади круга через отношение точек внутри к общему числу, умноженная на площадь квадрата
        }

        // Метод для вычисления абсолютной ошибки между ожидаемым и полученным результатом
        public static double CalculateAbsoluteError(double expectedResult, double actualResult)
        {
            var result = expectedResult - actualResult;
            result = RoundToTwoSignificantDigits(result, 2);  // Округление результата до 2 значимых цифр
            return result;
        }

        // Метод для вычисления относительной ошибки в процентах
        public static double CalculateRelativeError(double expectedResult, double actualResult)
        {
            if (expectedResult <= 0)
                throw new ArgumentException("Ожидаемое значение не может быть <= 0");  // Проверка корректности ожидаемого результата
            if (actualResult < 0)
                throw new ArgumentException("Полученное значение не может быть < 0");     // Проверка корректности полученного результата

            var result = Math.Abs(CalculateAbsoluteError(expectedResult, actualResult)) / expectedResult * 100d; // Относительная ошибка в процентах
            result = RoundToTwoSignificantDigits(result, 2);  // Округление до 2 значимых цифр
            return result;
        }

        // Метод округления числа до заданного количества значимых цифр
        public static double RoundToTwoSignificantDigits(double value, int significantDigits)
        {
            if (value == 0.0)
                return 0.0;

            int log10 = (int)Math.Floor(Math.Log10(Math.Abs(value))); // Позиция первой значимой цифры
            double scale = Math.Pow(10, significantDigits - log10 - 1); // Масштаб для округления
            double rounded = Math.Round(value * scale) / scale;

            // Убираем возможные артефакты округления (например, 0.30000000000000004)
            return BitConverter.Int64BitsToDouble(BitConverter.DoubleToInt64Bits(rounded));
        }
    }
}
