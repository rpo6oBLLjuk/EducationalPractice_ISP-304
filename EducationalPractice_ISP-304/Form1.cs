using EducationalPractice_ISP_304.Scripts;

namespace EducationalPractice_ISP_304
{
    public partial class Form1 : Form
    {

        private PointF offset = new(0, 0);
        private float radius = 5;
        private float C = 3;

        private int count = 1_000_000;

        private bool invalidated = false;


        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;

            MonteCarloCalculator.GenerateRandomPoints(radius, count, offset.Y, C);

            SegmentAreaCalculator.CalculateMonteCarloCegment(radius);

            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (invalidated)
                return;

            invalidated = true;
            MonteCarloView.RenderToBuffer(this, e, radius, offset, C);

            base.OnPaint(e);
        }
    }
}
