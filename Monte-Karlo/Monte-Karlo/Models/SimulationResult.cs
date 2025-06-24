using System.ComponentModel.DataAnnotations;

namespace Monte_Karlo.Models
{
    public class SimulationResult
    {
        [Key]
        public int Id { get; set; }

        public int PointsInCircle { get; set; }
        public int PointsInSegment { get; set; }
        public double MonteCarloResult { get; set; }

        public int CircleParamsId { get; set; }
        public CircleParams CircleParams { get; set; }

        public override string ToString()
        {
            return $"""
                   Id: {Id}, PointsInCircle: {PointsInCircle}, PointsInSegment: {PointsInSegment},
                   MonteCarloResult: {MonteCarloResult}, CircleParamsId: {CircleParamsId}
                   """;
        }
    }
}

