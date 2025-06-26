using Monte_Karlo.Models;

namespace Monte_Karlo.Utilites.View
{
    public class MonteCarloView
    {
        public int GridStep
        {
            get => _gridStep;
            set
            {
                _gridStep = value;
                _step = _gridStep * 2;
            }
        }
        private int _gridStep = 100;
        private int _step = 200;

        private static readonly int _viewPointsLimit = 1_000_000;

        private static readonly Color _backgroundColor = Color.FromArgb(20, 20, 20); //Black
        private static readonly Pen _gridPen = new(Color.Gray, 1);
        private static readonly Pen _axisPen = new(Color.FromArgb(200, 200, 200), 2); //White

        private static readonly Pen _cutterPen = new(Color.Red, 4);

        private static readonly Pen _circlePen = new(Color.Yellow, 2);
        private static readonly Pen _squarePen = new(Color.Red, 2);

        private static readonly Pen _cuttedPointsBrush = new(Color.LawnGreen, 1);

        private static readonly Color _textColor = Color.FromArgb(200, 200, 200); //White
        private static readonly Brush _textBrush = new SolidBrush(_textColor);
        private static readonly Font _textFont = new("Arial", 8);



        public void RenderToBuffer(Panel panel, PaintEventArgs e, Circle circle, PointsData pointsData)
        {
            e.Graphics.Clear(_backgroundColor);
            OnPaint(panel, e, circle.radius, circle.circleCenter, circle.C, pointsData);
        }

        private void OnPaint(Panel panel, PaintEventArgs e, float radius, Point circleCenter, float C, PointsData pointsData)
        {
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;

            float centerX = panel.Size.Width / 2;
            float centerY = panel.Size.Height / 2;
            var centerScreen = new PointF(centerX, centerY);
            float squareX = centerX - radius * _step;
            float squareY = centerY - radius * _step;
            var squarePoint = new PointF(squareX, squareY);
            float originX = centerX - circleCenter.X * _step;
            float originY = centerY + circleCenter.Y * _step;
            var origin = new PointF(originX, originY);

            DrawPoints(g, centerScreen, _step, pointsData);
            DrawGrid(panel, g, origin);
            DrawAxis(panel, g, origin);
            DrawCoordinateNumbers(panel, g, origin);
            DrawRectangle(g, squarePoint, _step * radius * 2);
            DrawEllipse(g, squarePoint, _step * radius * 2);
            DrawCutter(panel, g, origin, C);
        }

        private void DrawGrid(Panel panel, Graphics g, PointF origin)
        {
            // Вертикальные линии
            for (float x = origin.X; x >= 0; x -= _gridStep)
            {
                g.DrawLine(_gridPen, x, 0, x, panel.Height);
            }
            for (float x = origin.X; x <= panel.Width; x += _gridStep)
            {
                g.DrawLine(_gridPen, x, 0, x, panel.Height);
            }

            // Горизонтальные линии
            for (float y = origin.Y; y >= 0; y -= _gridStep)
            {
                g.DrawLine(_gridPen, 0, y, panel.Width, y);
            }
            for (float y = origin.Y; y <= panel.Height; y += _gridStep)
            {
                g.DrawLine(_gridPen, 0, y, panel.Width, y);
            }
        }

        private void DrawAxis(Panel panel, Graphics g, PointF center)
        {
            g.DrawLine(_axisPen, 0, center.Y, panel.Width, center.Y);
            g.DrawLine(_axisPen, center.X, 0, center.X, panel.Height);
        }
        private void DrawCoordinateNumbers(Panel panel, Graphics g, PointF origin)
        {
            // Числа на оси X
            // влево
            for (float x = origin.X; x >= 0; x -= _step)
            {
                int digit = (int)Math.Round((x - origin.X) / _step);
                if (digit == 0)
                    continue;

                string text = digit.ToString();
                SizeF textSize = g.MeasureString(text, _textFont);
                float textX = x - textSize.Width / 2;
                float textY = origin.Y + 5;

                if (TextInPanel(panel, textSize, textX, textY))
                {
                    g.DrawString(text, _textFont, _textBrush, textX, textY);
                }
            }

            // вправо
            for (float x = origin.X; x <= panel.Width; x += _step)
            {
                int digit = (int)Math.Round((x - origin.X) / _step);
                if (digit == 0)
                    continue;

                string text = digit.ToString();
                SizeF textSize = g.MeasureString(text, _textFont);
                float textX = x - textSize.Width / 2;
                float textY = origin.Y + 5;

                if (TextInPanel(panel, textSize, textX, textY))
                {
                    g.DrawString(text, _textFont, _textBrush, textX, textY);
                }
            }


            // Числа на оси Y
            // вверх
            for (float y = origin.Y; y >= 0; y -= _step)
            {
                int digit = -(int)Math.Round((y - origin.Y) / _step);
                if (digit == 0)
                    continue;

                string text = digit.ToString();
                SizeF textSize = g.MeasureString(text, _textFont);
                float textX = origin.X + 5;
                float textY = y - textSize.Height / 2;

                if (TextInPanel(panel, textSize, textX, textY))
                {
                    g.DrawString(text, _textFont, _textBrush, textX, textY);
                }
            }


            // вниз
            for (float y = origin.Y; y <= panel.Height; y += _step)
            {
                int digit = -(int)Math.Round((y - origin.Y) / _step);
                if (digit == 0)
                    continue;

                string text = digit.ToString();
                SizeF textSize = g.MeasureString(text, _textFont);
                float textX = origin.X + 5;
                float textY = y - textSize.Height / 2;

                if (TextInPanel(panel, textSize, textX, textY))
                {
                    g.DrawString(text, _textFont, _textBrush, textX, textY);
                }
            }

            g.DrawString("0", _textFont, _textBrush, origin.X + 5, origin.Y + 5);
        }

        private bool TextInPanel(Panel panel, SizeF textSize, float textX = 0, float textY = 0)
        {
            bool xIn = textX >= 0 && textX + textSize.Width <= panel.Width;
            bool yIn = textY >= 0 && textY + textSize.Height <= panel.Height;
            return xIn && yIn;
        }

        private void DrawRectangle(Graphics g, PointF square, float squareSize)
        {
            g.DrawRectangle(_squarePen, square.X, square.Y, squareSize, squareSize);
        }
        private void DrawEllipse(Graphics g, PointF square, float squareSize)
        {
            g.DrawEllipse(_circlePen, square.X, square.Y, squareSize, squareSize);
        }

        private void DrawCutter(Panel panel, Graphics g, PointF center, float C)
        {
            g.DrawLine(_cutterPen, center.X + _step * C, 0, center.X + _step * C, panel.Height);
        }

        private void DrawPoints(Graphics g, PointF center, float gridStep, PointsData pointsData)
        {
            if (pointsData.CuttedPoints.Count == 0)
                return;

            int pointsToDraw = Math.Min(pointsData.CuttedPoints.Count, _viewPointsLimit);
            var rectangles = new RectangleF[pointsToDraw];

            for (int i = 0; i < pointsToDraw; i++)
            {
                var point = pointsData.CuttedPoints[i];
                float screenX = center.X + point.X * gridStep;
                float screenY = center.Y - point.Y * gridStep;
                rectangles[i] = new RectangleF(screenX, screenY, 1, 1);
            }

            g.DrawRectangles(_cuttedPointsBrush, rectangles);
        }
    }
}
