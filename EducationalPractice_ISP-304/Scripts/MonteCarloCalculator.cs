namespace EducationalPractice_ISP_304.Scripts
{
    public static class MonteCarloCalculator
    {
        public static List<PointF> Points { get; private set; } = new();

        public static List<PointF> IncludedPoints { get; private set; } = new();
        public static List<PointF> ExcludedPoints { get; private set; } = new();
        public static List<PointF> CuttedPoints { get; private set; } = new();

        public static void GenerateRandomPoints(float radius, int count, float offsetX, float C)
        {
            Random random = new();

            Points.Clear();
            IncludedPoints.Clear();
            ExcludedPoints.Clear();
            CuttedPoints.Clear();


            Points = new List<PointF>(count);

            for (int i = 0; i < count; i++)
            {
                float x = (float)random.NextDouble() * radius * 2;
                float y = (float)random.NextDouble() * radius * 2;
                Points.Add(new PointF(x, y));
            }

            CalculateIncludedPoints(radius);
            CalculateCuttedPoints(radius, offsetX, C);
        }

        private static void CalculateIncludedPoints(float radius)
        {
            foreach(PointF point in Points)
            {
                float pointX = point.X - radius;
                float pointY = point.Y - radius;

                if (Math.Pow(pointX, 2) + Math.Pow(pointY, 2) < Math.Pow(radius, 2))
                    IncludedPoints.Add(point);
                else
                    ExcludedPoints.Add(point);
            }
        }

        private static void CalculateCuttedPoints(float radius, float offsetX, float C)
        {
            foreach (PointF point in IncludedPoints)
            {
                float pointX = (point.X - radius);
                float pointY = (point.Y - radius);

                if(pointX + offsetX < C)
                {
                    CuttedPoints.Add(point);
                }
            }
        }
    }
}
