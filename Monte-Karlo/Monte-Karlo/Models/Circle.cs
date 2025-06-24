namespace Monte_Karlo.Models
{
    public class Circle
    {
        public Point circleCenter = new Point(-3, 1);
        public float radius = 2;
        public float C = -2;

        public Circle() { }

        public Circle(Point circleCenter, float radius, float c)
        {
            this.circleCenter = circleCenter;
            this.radius = radius;
            this.C = c;
        }

        public override bool Equals(object obj)
        {
            return obj is Circle other &&
                circleCenter.X == other.circleCenter.X &&
                circleCenter.Y == other.circleCenter.Y &&
                radius == other.radius &&
                C == other.C;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(circleCenter.X, circleCenter.Y, radius, C);
        }

        public override string ToString()
        {
            return $"CircleCenter: {circleCenter}, Radius: {radius}, C: {C}";
        }
    }
}
