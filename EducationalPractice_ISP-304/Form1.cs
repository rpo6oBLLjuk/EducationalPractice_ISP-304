using EducationalPractice_ISP_304.Scripts;
using System.Reflection;

namespace EducationalPractice_ISP_304
{
    public partial class Form1 : Form
    {

        private PointF offset = new(-3, 1);
        private float radius = 2;
        private float C = -2;

        private int pointsCount = 100_000;

        private int gridStep;


        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;

            typeof(Panel).InvokeMember("DoubleBuffered",
                BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                null, panel1, new object[] { true });


            radiusSlider.Value = (int)radius;
            radiusLabel.Text = $"Radius: {radiusSlider.Value}";

            gridStep = (int)MonteCarloView._gridStep;
            sizeTrackbar.Value = (int)MonteCarloView._gridStep;
            sizeLabel.Text = $"Size: {sizeTrackbar.Value}";

            cTrackbar.Value = (int)C;
            cLabel.Text = $"C: {C}";

            pointsCountUpdown.Value = pointsCount;

            GenerateRandomPoints();

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            MonteCarloView.RenderToBuffer(panel1, e, gridStep, radius, offset, C);

            base.OnPaint(e);
        }

        private void radiusSlider_Scroll(object sender, EventArgs e)
        {
            radiusLabel.Text = $"Radius: {radiusSlider.Value}";
            radius = (float)radiusSlider.Value;

            GenerateRandomPoints();
        }

        private void sizeTrackbar_Scroll(object sender, EventArgs e)
        {
            sizeLabel.Text = $"Size: {sizeTrackbar.Value}";
            gridStep = sizeTrackbar.Value;

            GenerateRandomPoints();
        }

        private void pointsCountUpdown_ValueChanged(object sender, EventArgs e)
        {
            pointsCount = (int)pointsCountUpdown.Value;
        }

        private void cTrackbar_ValueChanged(object sender, EventArgs e)
        {
            C = (int)cTrackbar.Value;
            cLabel.Text = $"C: {C}";

            GenerateRandomPoints();
        }

        private void GeneratePointsButton_Click(object sender, EventArgs e)
        {
            GenerateRandomPoints();
        }

        private void GenerateRandomPoints()
        {
            MonteCarloCalculator.GenerateRandomPoints(radius, pointsCount, offset.X, C);
            panel1.Invalidate();

            var squares = SegmentAreaCalculator.CalculateMonteCarloCegment(radius);
            realSquareLabel.Text = $"Real Square: {squares.realSquare:F6}";
            MonteCarloSquare.Text = $"Monte Carlo Square: {squares.monteCarloSquare:F6}";
        }
    }
}
