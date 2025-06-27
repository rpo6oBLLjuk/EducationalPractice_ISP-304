namespace Monte_Karlo.Utilites.Calculators
{
    // Статический класс для базовых статистических вычислений
    public static class StatisticCalculator
    {
        // Метод для вычисления медианы списка чисел (центрального значения)
        public static double CalculateMedian(List<double> values)
        {
            var sorted = values.OrderBy(x => x).ToList(); // Сортируем входной список по возрастанию
            int count = sorted.Count;

            if (count % 2 == 0) // Если количество элементов чётное
                // Возвращаем среднее арифметическое двух центральных элементов
                return (sorted[count / 2 - 1] + sorted[count / 2]) / 2;
            else
                // Если нечётное — возвращаем центральный элемент
                return sorted[count / 2];
        }

        // Метод для вычисления размаха (range) — разницы между максимальным и минимальным значением
        public static double CalculateRange(List<double> values) => values.Max() - values.Min();
    }
}
