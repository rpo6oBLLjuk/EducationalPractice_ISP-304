namespace Monte_Karlo.Models
{
    public class PointsData
    {
        public List<PointF> Points { get; set; } = new();
        public List<PointF> IncludedPoints { get; set; } = new();
        public List<PointF> CuttedPoints { get; set; } = new();
    }
}