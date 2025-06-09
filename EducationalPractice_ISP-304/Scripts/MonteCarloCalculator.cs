using System;

namespace EducationalPractice_ISP_304.Scripts
{
    public static class MonteCarloCalculator
    {
        public static List<PointF> Points { get; private set; } = new();

        public static void GenerateRandomPoints(float radius, int count)
        {
            Random random = new();

            Points.Clear();
            Points = new List<PointF>(count);

            for (int i = 0; i < count; i++)
            {
                float x = (float)random.NextDouble() * radius * 2;
                float y = (float)random.NextDouble() * radius * 2;
                Points.Add(new PointF(x, y));
            }
        }
    }
}
