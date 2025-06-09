namespace EducationalPractice_ISP_304.Scripts
{
    public static class MonteCarloView
    {
        private static float _gridStep = 25;
        private static float _gridSize = 5;

        private static Pen _gridPen = new Pen(Color.LightGray, 1);
        private static Pen _axisPen = new Pen(Color.Black, 2);
        private static Pen _circlePen = new Pen(Color.Red, 2);
        private static Pen _squarePen = new Pen(Color.Blue, 2);
        private static Color _textColor = Color.Black;

        private static Brush _textBrush;
        private static Font _textFont = new Font("Arial", 8);



        public static void RenderToBuffer(Form form, PaintEventArgs e, float radius, PointF offset)
        {
            e.Graphics.Clear(Color.White);

            _textBrush = new SolidBrush(_textColor);
            OnPaint(form, e, radius, offset);
        }

        private static void OnPaint(Form form, PaintEventArgs e, float radius, PointF offset)
        {
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            float centerX = form.ClientSize.Width / 2;
            float centerY = form.ClientSize.Height / 2;

            DrawGrid(form, g, centerX, centerY);

            float squareX = centerX - (_gridStep * 2 * radius) + offset.X * _gridStep * 2;
            float squareY = centerY - (_gridStep * 2 * radius) - offset.Y * _gridStep * 2;

            DragRectangle(g, squareX, squareY, _gridStep * 2 * radius * 2);
            DrawEllipse(g, squareX, squareY, _gridStep * 2 * radius * 2);

            DrawPoint(g, squareX, squareY, _gridStep * 2);

            DrawAxis(form, g, centerX, centerY);
            DrawCoordinateNumbers(form, g, centerX, centerY);
        }

        private static void DrawGrid(Form form, Graphics g, float centerX, float centerY)
        {
            // Вертикальные линии (сетка)
            for (float x = centerX; x < form.ClientSize.Width; x += _gridStep)
            {
                g.DrawLine(_gridPen, x, 0, x, form.ClientSize.Height);
                g.DrawLine(_gridPen, centerX - (x - centerX), 0, centerX - (x - centerX), form.ClientSize.Height);
            }

            // Горизонтальные линии (сетка)
            for (float y = centerY; y < form.ClientSize.Height; y += _gridStep)
            {
                g.DrawLine(_gridPen, 0, y, form.ClientSize.Width, y);
                g.DrawLine(_gridPen, 0, centerY - (y - centerY), form.ClientSize.Width, centerY - (y - centerY));
            }
        }
        private static void DrawAxis(Form form, Graphics g, float centerX, float centerY)
        {
            g.DrawLine(_axisPen, 0, centerY, form.ClientSize.Width, centerY);
            g.DrawLine(_axisPen, centerX, 0, centerX, form.ClientSize.Height);


        }
        private static void DrawCoordinateNumbers(Form form, Graphics g, float centerX, float centerY)
        {
            for (float x = centerX; x < form.Width; x += 2 * _gridStep)
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

            for (float y = centerY; y < form.Height; y += 2 * _gridStep)
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

        private static void DrawPoint(Graphics g, float startX, float startY, float gridStep)
        {
            foreach (var point in MonteCarloCalculator.Points)
            {
                g.FillRectangle(Brushes.Black, point.X * gridStep + startX, point.Y * gridStep + startY, 1, 1);
            }
        }
    }
}
