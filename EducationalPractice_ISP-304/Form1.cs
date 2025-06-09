using EducationalPractice_ISP_304.Scripts;

namespace EducationalPractice_ISP_304
{
    public partial class Form1 : Form
    {

        private PointF offset = new(0, 0);
        private float radius = 4;
        private int count = 10000;


        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;

            MonteCarloCalculator.GenerateRandomPoints(radius, count);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            MonteCarloView.RenderToBuffer(this, e, radius, offset);

            base.OnPaint(e);
        }
    }
}
