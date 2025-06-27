namespace Monte_Karlo.Models
{
    // Класс для хранения и классификации наборов точек в формате PointF
    public class PointsData
    {
        // Список всех точек (координаты с плавающей точкой)
        public List<PointF> Points { get; set; } = new();

        // Список точек, которые были включены (например, удовлетворяют некоторому условию)
        public List<PointF> IncludedPoints { get; set; } = new();

        // Список точек, которые были исключены или отсечены (например, не подходят по условию)
        public List<PointF> CuttedPoints { get; set; } = new();
    }
}
