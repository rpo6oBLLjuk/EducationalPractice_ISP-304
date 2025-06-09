namespace EducationalPractice_ISP_304.Scripts
{
    public static class MonteCarloView
    {
        public static float _gridStep = 25;

        private static readonly Pen _gridPen = new(Color.LightGray, 1);
        private static readonly Pen _axisPen = new(Color.Black, 2);

        private static readonly Pen _cutterPen = new(Color.LightGreen, 4);

        private static readonly Pen _circlePen = new(Color.Red, 2);
        private static readonly Pen _squarePen = new(Color.Blue, 2);

        private static readonly Pen _excludedPointsBrush = new(Color.Aqua, 1);
        private static readonly Pen _cuttedPointsBrush = new(Color.Yellow, 1);

        private static readonly Color _textColor = Color.Black;
        private static readonly Brush _textBrush = new SolidBrush(_textColor);
        private static readonly Font _textFont = new("Arial", 8);



        public static void RenderToBuffer(Panel panel, PaintEventArgs e, float gridStep, float radius, PointF offset, float C)
        {
            e.Graphics.Clear(Color.White);

            _gridStep = gridStep;

            //Size panelSize = panel.Size;
            //panel.Size = _viewSize;

            OnPaint(panel, e, radius, offset, C);

            //panel.Size = panelSize;
        }

        private static void OnPaint(Panel panel, PaintEventArgs e, float radius, PointF offset, float C)
        {
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;

            float centerX = panel.Size.Width / 2;
            float centerY = panel.Size.Height / 2;


            float squareX = centerX - (_gridStep * 2 * radius) + offset.X * _gridStep * 2;
            float squareY = centerY - (_gridStep * 2 * radius) - offset.Y * _gridStep * 2;

            DragRectangle(g, squareX, squareY, _gridStep * 2 * radius * 2);
            DrawEllipse(g, squareX, squareY, _gridStep * 2 * radius * 2);

            DrawPoint(g, squareX, squareY, _gridStep * 2);

            DrawGrid(panel, g, centerX, centerY);

            DrawAxis(panel, g, centerX, centerY);
            DrawCutter(panel, g, centerY, C);
            DrawCoordinateNumbers(panel, g, centerX, centerY);
        }

        private static void DrawGrid(Panel panel, Graphics g, float centerX, float centerY)
        {
            for (float x = centerX; x < panel.ClientSize.Width; x += _gridStep)
            {
                g.DrawLine(_gridPen, x, 0, x, panel.ClientSize.Height);
                g.DrawLine(_gridPen, centerX - (x - centerX), 0, centerX - (x - centerX), panel.ClientSize.Height);
            }

            for (float y = centerY; y < panel.ClientSize.Height; y += _gridStep)
            {
                g.DrawLine(_gridPen, 0, y, panel.ClientSize.Width, y);
                g.DrawLine(_gridPen, 0, centerY - (y - centerY), panel.ClientSize.Width, centerY - (y - centerY));
            }
        }
        private static void DrawAxis(Panel panel, Graphics g, float centerX, float centerY)
        {
            g.DrawLine(_axisPen, 0, centerY, panel.ClientSize.Width, centerY);
            g.DrawLine(_axisPen, centerX, 0, centerX, panel.ClientSize.Height);
        }
        private static void DrawCoordinateNumbers(Panel panel, Graphics g, float centerX, float centerY)
        {
            for (float x = centerX; x < panel.Width; x += 2 * _gridStep)
            {
                int number = (int)((x - centerX) / (2 * _gridStep));
                if (number != 0)
                {
                    string text = number.ToString();
                    SizeF textSize = g.MeasureString(text, _textFont);
                    g.DrawString(text, _textFont, _textBrush, x - textSize.Width / 2, centerY + 5);
                }

                if (x != centerX)
                {
                    string negativeText = (-number).ToString();
                    SizeF negativeTextSize = g.MeasureString(negativeText, _textFont);
                    g.DrawString(negativeText, _textFont, _textBrush, centerX - (x - centerX) - negativeTextSize.Width / 2, centerY + 5);
                }
            }

            for (float y = centerY; y < panel.Height; y += 2 * _gridStep)
            {
                int number = (int)((y - centerY) / (2 * _gridStep));
                if (number != 0)
                {
                    string text = (-number).ToString();
                    SizeF textSize = g.MeasureString(text, _textFont);
                    g.DrawString(text, _textFont, _textBrush, centerX + 5, y - textSize.Height / 2);
                }

                if (y != centerY)
                {
                    string positiveText = number.ToString();
                    SizeF positiveTextSize = g.MeasureString(positiveText, _textFont);
                    g.DrawString(positiveText, _textFont, _textBrush, centerX + 5, centerY - (y - centerY) - positiveTextSize.Height / 2);
                }
            }

            g.DrawString("0", _textFont, _textBrush, centerX + 5, centerY + 5);
        }

        private static void DragRectangle(Graphics g, float squareX, float squareY, float squareSize)
        {
            g.DrawRectangle(_squarePen, squareX, squareY, squareSize, squareSize);
        }
        private static void DrawEllipse(Graphics g, float squareX, float squareY, float squareSize)
        {
            g.DrawEllipse(_circlePen, squareX, squareY, squareSize, squareSize);
        }

        private static void DrawCutter(Panel panel, Graphics g, float centerY, float C)
        {
            g.DrawLine(_cutterPen, 0, centerY + _gridStep * 2 * -C, panel.ClientSize.Width, centerY + _gridStep * 2 * -C);
        }
        private static void DrawPoint(Graphics g, float startX, float startY, float gridStep)
        {
            foreach (var point in MonteCarloCalculator.ExcludedPoints)
            {
                g.DrawRectangle(_excludedPointsBrush, point.X * gridStep + startX, point.Y * gridStep + startY, 1, 1);
            }

            foreach (var point in MonteCarloCalculator.CuttedPoints)
            {
                g.DrawRectangle(_cuttedPointsBrush, point.X * gridStep + startX, point.Y * gridStep + startY, 1, 1);
            }
        }
    }
}
