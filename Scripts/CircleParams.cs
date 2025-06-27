namespace Monte_Karlo.Models
{
    // Класс, представляющий параметры окружности и связанные с ней результаты моделирования
    public class CircleParams
    {
        // Атрибут [Key] указывает, что это первичный ключ для базы данных
        public int Id { get; set; }

        // Общее количество точек, используемых в эксперименте или симуляции
        public int TotalPoints { get; set; }

        // Аналитический (теоретический) результат, рассчитанный для окружности
        public double AnalyticalResult { get; set; }

        // Коллекция результатов моделирования (список объектов SimulationResult)
        public List<SimulationResult> Results { get; set; } = new();

        // Переопределённый метод ToString для удобного вывода основных свойств объекта
        public override string ToString()
        {
            return $"""
                   Id: {Id},
                   TotalPoints: {TotalPoints},
                   AnalyticalResult: {AnalyticalResult},
                   ResultsCount: {Results.Count}
                   """;
        }
    }
}
