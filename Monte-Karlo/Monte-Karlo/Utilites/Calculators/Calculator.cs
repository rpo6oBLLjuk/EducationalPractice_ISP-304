using Monte_Karlo.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monte_Karlo.Utilites.Calculators
{
    public static class Calculator
    {
        public static double CalculateAnalyticArea(Circle circle)
        {
            Point center = circle.circleCenter;
            double R = circle.radius;
            double C = circle.C;

            if (R == 0)
                throw new ArgumentException("R == 0");

            double xLine = C;
            double d = Math.Abs(center.X - xLine);  // расстояние от центра до хорды
            double h = Math.Abs(R - d);             // расстояние от хорды до окружности
            double CircleArea = Math.PI * R * R;

            if (d >= R)
                return CircleArea;
            if (h == R)
                return CircleArea / 2;

            double segmentArea = GetSegmentArea(R, d);
            return CircleArea - segmentArea;
        }

        // https://en.wikipedia.org/wiki/Circular_segment
        private static double GetSegmentArea(double R, double d)
        {
            return R * R * Math.Acos(d / R) - d * Math.Sqrt(R * R - d * d);
        }

        public static double CircleSuare(double R) => Math.PI * R * R;

        public static double CalculateMonteCarloArea(float radius, int allPoints, int cuttedPoints)
        {
            double squareArea = 4 * radius * radius;
            return cuttedPoints / (double)allPoints * squareArea;
        }

        public static double CalculateAbsoluteError(double expectedResult, double actualResult)
        {
            var result = expectedResult - actualResult;
            result = RoundToTwoSignificantDigits(result, 2);
            return result;
        }

        public static double CalculateRelativeError(double expectedResult, double actualResult)
        {
            if (expectedResult <= 0)
                throw new ArgumentException("Ожидаемое значение не может быть <= 0");
            if (actualResult < 0)
                throw new ArgumentException("Полученное значение не может быть < 0");
            var result = Math.Abs(CalculateAbsoluteError(expectedResult, actualResult)) / expectedResult * 100d;
            result = RoundToTwoSignificantDigits(result, 2);
            return result;
        }

        public static double RoundToTwoSignificantDigits(double value, int significantDigits)
        {
            if (value == 0.0)
                return 0.0;

            int log10 = (int)Math.Floor(Math.Log10(Math.Abs(value)));
            double scale = Math.Pow(10, significantDigits - log10 - 1);
            double rounded = Math.Round(value * scale) / scale;

            // Убираем возможные артефакты округления (например, 0.30000000000000004)
            return BitConverter.Int64BitsToDouble(BitConverter.DoubleToInt64Bits(rounded));
        }
    }
}
