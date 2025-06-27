using System.ComponentModel.DataAnnotations;

namespace Monte_Karlo.Models
{
    // Класс, представляющий результат одной симуляции Монте-Карло
    public class SimulationResult
    {
        [Key]
        // Уникальный идентификатор результата симуляции
        public int Id { get; set; }

        // Общее количество точек, использованных в симуляции
        public int Points { get; set; }

        // Количество точек, попавших в интересующий сегмент (например, в круг)
        public int PointsInSegment { get; set; }

        // Результат оценки методом Монте-Карло (например, отношение PointsInSegment к Points)
        public double MonteCarloResult { get; set; }

        // Внешний ключ на параметры круга, к которым относится данный результат симуляции
        public int CircleParamsId { get; set; }

        // Навигационное свойство для связи с параметрами круга
        public CircleParams CircleParams { get; set; }

        // Переопределение метода ToString для удобного вывода информации о результате симуляции
        public override string ToString()
        {
            return $"""
                   Id: {Id}, Points: {Points}, PointsInSegment: {PointsInSegment}, 
                   MonteCarloResult: {MonteCarloResult}, CircleParamsId: {CircleParamsId}
                   """;
        }
    }
}
