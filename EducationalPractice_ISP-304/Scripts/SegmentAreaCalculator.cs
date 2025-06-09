namespace EducationalPractice_ISP_304.Scripts
{
    public static class SegmentAreaCalculator
    {
        public static double CalculateMonteCarloCegment(float radius)
        {
            double defaultCircleS = Math.PI * Math.Pow(radius, 2);
            double monteCarloCircleS = MonteCarloCalculator.IncludedPoints.Count / (double)MonteCarloCalculator.Points.Count * 100;

            MessageBox.Show($"Default: {defaultCircleS}\nMonteCarlo: {monteCarloCircleS:F6}");

            return 0;
        }
    }
}
