using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monte_Karlo.Utilites.Calculators
{
    public static class StatisticCalculator
    {
        public static double CalculateMedian(List<double> values)
        {
            var sorted = values.OrderBy(x => x).ToList();
            int count = sorted.Count;

            if (count % 2 == 0)
                return (sorted[count / 2 - 1] + sorted[count / 2]) / 2;
            else
                return sorted[count / 2];
        }

        public static double CalculateRange(List<double> values) => values.Max() - values.Min();
    }
}
