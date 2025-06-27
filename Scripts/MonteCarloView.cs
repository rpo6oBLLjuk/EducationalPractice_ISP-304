using Monte_Karlo.Models;

namespace Monte_Karlo.Utilites.View
{
    public class MonteCarloView
    {
        // Свойство для установки шага сетки.
        // При изменении GridStep также пересчитывается _step — масштаб для отрисовки
        public int GridStep
        {
            get => _gridStep;
            set
            {
                _gridStep = value;
                _step = _gridStep * 2;  // Масштаб равен двойному шагу сетки
            }
        }
        private int _gridStep = 100; // Начальный шаг сетки в пикселях
        private int _step = 200;     // Масштабный шаг (используется для позиционирования элементов)

        private static readonly int _viewPointsLimit = 1_000_000; // Максимальное количество точек для отрисовки

        // Цвет фона панели (темно-серый)
        private static readonly Color _backgroundColor = Color.FromArgb(20, 20, 20); //Black

        // Карандаши для рисования различных элементов
        private static readonly Pen _gridPen = new(Color.Gray, 1);                 // Сетка
        private static readonly Pen _axisPen = new(Color.FromArgb(200, 200, 200), 2); // Оси (светло-серый)

        private static readonly Pen _cutterPen = new(Color.Red, 4);                 // "Cutter" — красная вертикальная линия

        private static readonly Pen _circlePen = new(Color.Yellow, 2);              // Окружность — желтая
        private static readonly Pen _squarePen = new(Color.Red, 2);                 // Квадрат — красный

        private static readonly Pen _cuttedPointsBrush = new(Color.LawnGreen, 1);   // Точки (цвет газонно-зеленый)

        private static readonly Color _textColor = Color.FromArgb(200, 200, 200);   // Цвет текста — светло-серый
        private static readonly Brush _textBrush = new SolidBrush(_textColor);      // Кисть для текста
        private static readonly Font _textFont = new("Arial", 8);                   // Шрифт для координат

        // Основной метод отрисовки: очищает фон и вызывает внутренний метод OnPaint
        public void RenderToBuffer(Panel panel, PaintEventArgs e, Circle circle, PointsData pointsData)
        {
            e.Graphics.Clear(_backgroundColor);
            OnPaint(panel, e, circle.radius, circle.circleCenter, circle.C, pointsData);
        }

        // Основная отрисовка всех элементов на панели
        private void OnPaint(Panel panel, PaintEventArgs e, float radius, Point circleCenter, float C, PointsData pointsData)
        {
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed; // Быстрая отрисовка без сглаживания

            // Центр экрана (панели)
            float centerX = panel.Size.Width / 2;
            float centerY = panel.Size.Height / 2;
            var centerScreen = new PointF(centerX, centerY);

            // Верхний левый угол квадрата, который вписывает круг с данным радиусом
            float squareX = centerX - radius * _step;
            float squareY = centerY - radius * _step;
            var squarePoint = new PointF(squareX, squareY);

            // Проекция центра круга на экран (с учетом масштаба и направления осей)
            float originX = centerX - circleCenter.X * _step;
            float originY = centerY + circleCenter.Y * _step;
            var origin = new PointF(originX, originY);

            // Отрисовка точек, сетки, осей, подписей, квадрата, окружности и линии "cutter"
            DrawPoints(g, centerScreen, _step, pointsData);
            DrawGrid(panel, g, origin);
            DrawAxis(panel, g, origin);
            DrawCoordinateNumbers(panel, g, origin);
            DrawRectangle(g, squarePoint, _step * radius * 2);
            DrawEllipse(g, squarePoint, _step * radius * 2);
            DrawCutter(panel, g, origin, C);
        }

        // Метод рисует сетку по заданному "origin" — точке с координатами (0,0) на экране
        private void DrawGrid(Panel panel, Graphics g, PointF origin)
        {
            // Вертикальные линии сетки: слева и справа от origin.X
            for (float x = origin.X; x >= 0; x -= _gridStep)
            {
                g.DrawLine(_gridPen, x, 0, x, panel.Height);
            }
            for (float x = origin.X; x <= panel.Width; x += _gridStep)
            {
                g.DrawLine(_gridPen, x, 0, x, panel.Height);
            }

            // Горизонтальные линии сетки: выше и ниже origin.Y
            for (float y = origin.Y; y >= 0; y -= _gridStep)
            {
                g.DrawLine(_gridPen, 0, y, panel.Width, y);
            }
            for (float y = origin.Y; y <= panel.Height; y += _gridStep)
            {
                g.DrawLine(_gridPen, 0, y, panel.Width, y);
            }
        }

        // Рисует оси X и Y через центр (origin)
        private void DrawAxis(Panel panel, Graphics g, PointF center)
        {
            g.DrawLine(_axisPen, 0, center.Y, panel.Width, center.Y);  // Ось X
            g.DrawLine(_axisPen, center.X, 0, center.X, panel.Height); // Ось Y
        }

        // Рисует числа координат по осям вокруг origin (точки (0,0))
        private void DrawCoordinateNumbers(Panel panel, Graphics g, PointF origin)
        {
            // Числа по оси X слева от origin
            for (float x = origin.X; x >= 0; x -= _step)
            {
                int digit = (int)Math.Round((x - origin.X) / _step);
                if (digit == 0) // Не рисуем ноль здесь, чтобы не дублировать
                    continue;

                string text = digit.ToString();
                SizeF textSize = g.MeasureString(text, _textFont);
                float textX = x - textSize.Width / 2; // Центрируем по горизонтали
                float textY = origin.Y + 5;           // Немного ниже оси X

                if (TextInPanel(panel, textSize, textX, textY))
                {
                    g.DrawString(text, _textFont, _textBrush, textX, textY);
                }
            }

            // Числа по оси X справа от origin
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

            // Числа по оси Y вверх от origin
            for (float y = origin.Y; y >= 0; y -= _step)
            {
                int digit = -(int)Math.Round((y - origin.Y) / _step);
                if (digit == 0)
                    continue;

                string text = digit.ToString();
                SizeF textSize = g.MeasureString(text, _textFont);
                float textX = origin.X + 5;            // Немного правее оси Y
                float textY = y - textSize.Height / 2; // Центрируем по вертикали

                if (TextInPanel(panel, textSize, textX, textY))
                {
                    g.DrawString(text, _textFont, _textBrush, textX, textY);
                }
            }

            // Числа по оси Y вниз от origin
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

            // Рисуем "0" в начале координат с отступом
            g.DrawString("0", _textFont, _textBrush, origin.X + 5, origin.Y + 5);
        }

        // Проверка, что текст помещается полностью в пределах панели
        private bool TextInPanel(Panel panel, SizeF textSize, float textX = 0, float textY = 0)
        {
            bool xIn = textX >= 0 && textX + textSize.Width <= panel.Width;
            bool yIn = textY >= 0 && textY + textSize.Height <= panel.Height;
            return xIn && yIn;
        }

        // Рисует квадрат по заданной точке и размеру
        private void DrawRectangle(Graphics g, PointF square, float squareSize)
        {
            g.DrawRectangle(_squarePen, square.X, square.Y, squareSize, squareSize);
        }

        // Рисует окружность (эллипс с равными сторонами) по заданной точке и размеру
        private void DrawEllipse(Graphics g, PointF square, float squareSize)
        {
            g.DrawEllipse(_circlePen, square.X, square.Y, squareSize, squareSize);
        }

        // Рисует вертикальную красную линию "cutter" по параметру C относительно origin
        private void DrawCutter(Panel panel, Graphics g, PointF center, float C)
        {
            g.DrawLine(_cutterPen, center.X + _step * C, 0, center.X + _step * C, panel.Height);
        }

        // Рисует точки из списка pointsData.CuttedPoints
        private void DrawPoints(Graphics g, PointF center, float gridStep, PointsData pointsData)
        {
            if (pointsData.CuttedPoints.Count == 0)
                return;

            int pointsToDraw = Math.Min(pointsData.CuttedPoints.Count, _viewPointsLimit);

            // Создаем массив маленьких прямоугольников размером 1x1 пиксель для каждой точки
            var rectangles = new RectangleF[pointsToDraw];

            for (int i = 0; i < pointsToDraw; i++)
            {
                var point = pointsData.CuttedPoints[i];
                float screenX = center.X + point.X * gridStep;   // Перевод координаты X в экранную систему
                float screenY = center.Y - point.Y * gridStep;   // Перевод координаты Y (обратите внимание на минус — экранная система Y вниз)

                rectangles[i] = new RectangleF(screenX, screenY, 1, 1);
            }

            // Рисуем все точки одним вызовом
            g.DrawRectangles(_cuttedPointsBrush, rectangles);
        }
    }
}
