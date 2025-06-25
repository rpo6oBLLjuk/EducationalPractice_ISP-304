using System.ComponentModel.DataAnnotations;

namespace Monte_Karlo.Models
{
    public class CircleParams
    {
        [Key]
        public int Id { get; set; }
        public int TotalPoints { get; set; }
        public double AnalyticalResult { get; set; }

        public List<SimulationResult> Results { get; set; } = new();

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
