namespace EducationalPractice_ISP_304.Scripts
{
    public static class SegmentAreaCalculator
    {
        public static (double realSquare, double monteCarloSquare) CalculateMonteCarloCegment(float radius)
        {
            double defaultCircleS = Math.PI * Math.Pow(radius, 2);
            double monteCarloCircleS = MonteCarloCalculator.IncludedPoints.Count / (double)MonteCarloCalculator.Points.Count * Math.Pow(radius * 2, 2);


            //double realCirclesS = Math.Pow(radius, 2) / 2 *;
            return (defaultCircleS, monteCarloCircleS);
        }
    }
}
