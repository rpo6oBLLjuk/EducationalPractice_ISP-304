using Monte_Karlo.Models;
using Monte_Karlo.Utilites.Calculators;
using System.Drawing.Drawing2D;

namespace Monte_Karlo.Utilites.View
{
    // Класс, отвечающий за отображение (визуализацию) результатов анализа на элементе управления Panel
    public class AnalysisView
    {
        // Цвета для различных элементов графика
        private static readonly Color _analyticalColor = Color.LawnGreen;       // Цвет линии аналитического решения
        private static readonly Color _pointsColor = Color.Black;               // Цвет точек Монте-Карло
        private static readonly Color _pointsLinesColor = Color.LightSlateGray; // Цвет линий, соединяющих точки
        private static readonly Color _meanColor = Color.Orange;                // Цвет линии среднего значения
        private static readonly Color _medianColor = Color.Blue;                // Цвет линии медианы
        private static readonly Color _minMaxColor = Color.Red;                 // Цвет линий минимума и максимума
        private static readonly Color _backgroundColor = Color.White;           // Цвет фона панели
        private static readonly Color _gridColor = Color.LightGray;             // Цвет сетки графика
        private static readonly Color _legendBackgroundColor = Color.LightGray; // Цвет фона легенды
        private static readonly Padding _padding = new Padding(100, 20, 70, 40); // Отступы графика внутри панели (слева, сверху, справа, снизу)
        private static readonly double _percentYPadding = 0.1;                  // Дополнительный отступ по оси Y (10%)
        private static readonly Font _textFont = new Font("SegoUI", emSize: 8);  // Шрифт для текста
        private static readonly Brush _textBrush = Brushes.Black;               // Кисть для текста
        private static readonly float _pointRadius = 5;                         // Радиус точек

        // Основной публичный метод для рендера анализа на панели
        public void RenderAnalysis(Panel panel, PaintEventArgs e, CircleParams circleParams)
        {
            var g = e.Graphics;
            g.Clear(_backgroundColor); // Очистка области рисования белым цветом

            // Проверка, есть ли результаты для отображения
            if (circleParams == null || circleParams.Results == null || circleParams.Results.Count == 0)
            {
                MessageBox.Show("Нет результатов для анализа", "Оповещение");
                return;
            }

            OnPaint(panel, g, circleParams); // Отрисовка графика
        }

        // Вспомогательный приватный метод, который выполняет всю отрисовку
        private void OnPaint(Panel panel, Graphics g, CircleParams circleParams)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias; // Включаем сглаживание для более красивой отрисовки линий и фигур

            // Извлекаем результаты Монте-Карло
            List<double> mcResults = circleParams.Results.Select(r => r.MonteCarloResult).ToList();
            double analyticalValue = circleParams.AnalyticalResult;

            // Вычисляем основные статистики
            double mean = mcResults.Average();
            double median = StatisticCalculator.CalculateMedian(mcResults);
            double min = mcResults.Min();
            double max = mcResults.Max();

            // Задаём область отрисовки графика с учётом отступов
            Rectangle plotArea = new Rectangle(_padding.Left, _padding.Top, panel.Width - _padding.Right, panel.Height - _padding.Bottom);

            // Рассчитываем диапазон по оси Y с дополнительным паддингом (отступом)
            double yMin = Math.Min(analyticalValue, min);
            double yMax = Math.Max(analyticalValue, max);
            double yRange = yMax - yMin;
            yMin -= yRange * _percentYPadding;
            yMax += yRange * _percentYPadding;
            yRange = yMax - yMin;

            DrawGrid(g, plotArea, mcResults.Count, yMin, yMax);              // Рисуем сетку
            DrawAnalyticalLine(g, plotArea, analyticalValue, yMin, yRange); // Рисуем линию аналитического решения
            DrawMeanLine(g, plotArea, mean, yMin, yRange);                  // Рисуем линию среднего значения
            DrawMedianLine(g, plotArea, median, yMin, yRange);              // Рисуем линию медианы
            DrawMinMaxLines(g, plotArea, min, max, yMin, yRange);           // Рисуем линии минимума и максимума
            DrawMonteCarloPoints(g, plotArea, mcResults, yMin, yRange);     // Рисуем точки Монте-Карло и соединяющие линии
            DrawLegend(g, plotArea, median);                                // Рисуем легенду
        }

        // Метод отрисовки сетки графика с подписями по оси X (номер опыта) и Y (значения)
        private void DrawGrid(Graphics g, Rectangle plotArea, int pointsCount, double yMin, double yMax)
        {
            Pen girdPen = new Pen(_gridColor);

            g.DrawRectangle(girdPen, plotArea); // Рисуем рамку области графика

            // Вертикальные линии — каждые 10% от количества точек
            int step = Math.Max(1, pointsCount / 10);
            for (int i = 0; i < pointsCount; i += step)
            {
                float x = plotArea.Left + plotArea.Width * i / pointsCount;
                g.DrawLine(girdPen, x, plotArea.Top, x, plotArea.Bottom);

                // Подписываем номера экспериментов под осью X
                string text = (i + 1).ToString();
                SizeF textSize = g.MeasureString(text, _textFont);
                float textX = x - textSize.Width / 2;
                float textY = plotArea.Bottom - textSize.Height + 20;

                // Для первой линии сдвигаем подпись вправо, чтобы не залезала на край
                if (i == 0)
                    textX += textSize.Width;

                g.DrawString(text, _textFont, _textBrush, textX, textY);
            }

            // Горизонтальные линии сетки (6 интервалов — 7 линий)
            int linesCount = 7 - 1;
            for (int i = 0; i <= linesCount; i++)
            {
                float y = plotArea.Top + plotArea.Height * i / linesCount;
                g.DrawLine(girdPen, plotArea.Left, y, plotArea.Right, y);

                // Подписи значений по оси Y
                double yRange = yMax - yMin;
                double value = yMax - yRange * i / linesCount;
                string text = value.ToString("F6"); // Формат с 6 знаками после запятой
                SizeF textSize = g.MeasureString(text, _textFont);
                float textX = plotArea.Left - textSize.Width - 15;
                float textY = y - textSize.Height / 2;

                g.DrawString(text, _textFont, _textBrush, textX, textY);
            }
        }

        // Рисует горизонтальную линию аналитического решения по всей ширине графика
        private void DrawAnalyticalLine(Graphics g, Rectangle area, double value, double yMin, double yRange)
        {
            float y = area.Bottom - (float)((value - yMin) / yRange * area.Height);
            g.DrawLine(new Pen(_analyticalColor, 4), area.Left, y, area.Right, y);
        }

        // Отрисовка точек Монте-Карло и линий, соединяющих их
        private void DrawMonteCarloPoints(Graphics g, Rectangle area, List<double> results, double yMin, double yRange)
        {
            if (results == null || results.Count == 0)
                return;

            int count = results.Count;
            float width = area.Width;
            float height = area.Height;
            float left = area.Left;
            float bottom = area.Bottom;

            float xStep = Math.Max(1f, count);
            float yScale = height / (float)yRange;

            float currentX = 0;
            float currentY = 0;
            float previousX;
            float previousY;

            Pen pointsLinesPen = new(_pointsLinesColor, 1);

            using (SolidBrush brush = new SolidBrush(_pointsColor))
            {
                float diameter = 2 * _pointRadius;

                // Рисуем линии между точками
                for (int i = 0; i < count; i++)
                {
                    try
                    {
                        previousX = currentX + 1;
                        previousY = currentY;

                        currentX = left + width * i / xStep - 0.5f;
                        currentY = bottom - (float)((results[i] - yMin) * yScale);

                        if (i == 0)
                        {
                            previousX = currentX;
                            previousY = currentY;
                        }

                        g.DrawLine(pointsLinesPen, new PointF(previousX, previousY), new PointF(currentX, currentY));
                    }
                    catch (DivideByZeroException)
                    {
                        MessageBox.Show("Слишком мало данных измерений (минимум 2)", "Оповещение");
                        return; // Останавливаем отрисовку при ошибке
                    }
                }

                // Рисуем сами точки
                for (int i = 0; i < count; i++)
                {
                    try
                    {
                        currentX = left + width * i / xStep;
                        currentY = bottom - (float)((results[i] - yMin) * yScale);

                        g.FillEllipse(brush, currentX - _pointRadius, currentY - _pointRadius, diameter, diameter);
                    }
                    catch (DivideByZeroException)
                    {
                        MessageBox.Show("Слишком мало данных измерений (минимум 2)", "Оповещение");
                        return;
                    }
                }
            }
        }

        // Рисует пунктирную линию среднего значения
        private void DrawMeanLine(Graphics g, Rectangle area, double value, double yMin, double yRange)
        {
            float y = area.Bottom - (float)((value - yMin) / yRange * area.Height);
            g.DrawLine(new Pen(_meanColor, 3) { DashStyle = DashStyle.Dash },
                      area.Left, y, area.Right, y);
        }

        // Рисует пунктирную точечную линию медианы
        private void DrawMedianLine(Graphics g, Rectangle area, double value, double yMin, double yRange)
        {
            float y = area.Bottom - (float)((value - yMin) / yRange * area.Height);
            g.DrawLine(new Pen(_medianColor, 3) { DashStyle = DashStyle.Dot },
                      area.Left, y, area.Right, y);
        }

        // Рисует линии минимума и максимума значений
        private void DrawMinMaxLines(Graphics g, Rectangle area, double min, double max, double yMin, double yRange)
        {
            float yMinPos = area.Bottom - (float)((min - yMin) / yRange * area.Height);
            float yMaxPos = area.Bottom - (float)((max - yMin) / yRange * area.Height);

            g.DrawLine(new Pen(_minMaxColor, 3), area.Left, yMinPos, area.Right, yMinPos);
            g.DrawLine(new Pen(_minMaxColor, 3), area.Left, yMaxPos, area.Right, yMaxPos);
        }

        // Отрисовка легенды с пояснениями цветов и линий
        private void DrawLegend(Graphics g, Rectangle area, double mode)
        {
            SizeF textSize = g.MeasureString("Аналитическое решение", _textFont);
            float boxWidth = 20;
            float startX = area.Width - textSize.Width - boxWidth - 5;
            float startY = area.Top;
            float itemHeight = textSize.Height;

            // Фон легенды
            g.FillRectangle(new SolidBrush(_legendBackgroundColor), startX, startY, 260, 100);

            // Заголовок легенды
            g.DrawString("Легенда", _textFont, _textBrush, startX + 5, startY);

            // Пояснения с цветами и стилями
            DrawLegendItem(g, "Аналитическое решение", _analyticalColor, startX, startY + 1 * itemHeight + 5, 4, DashStyle.Solid);
            DrawLegendItem(g, "Среднее значение", _meanColor, startX, startY + 2 * itemHeight + 5, 3, DashStyle.Dash);
            DrawLegendItem(g, "Медиана", _medianColor, startX, startY + 3 * itemHeight + 5, 3, DashStyle.Dot);
            DrawLegendItem(g, "Минимум/Максимум", _minMaxColor, startX, startY + 4 * itemHeight + 5, 3, DashStyle.Solid);
            DrawLegendItem(g, "Точки Монте-Карло", _pointsColor, startX, startY + 5 * itemHeight + 5, 10, DashStyle.Solid, true);
        }

        // Вспомогательный метод отрисовки одного элемента легенды
        private void DrawLegendItem(Graphics g, string text, Color color, float x, float y, int lineWidth, DashStyle dashStyle, bool drawCircle = false)
        {
            using (Pen pen = new Pen(color, lineWidth) { DashStyle = dashStyle })
            {
                if (drawCircle)
                {
                    g.FillEllipse(new SolidBrush(color), x + 5, y + 5, 10, 10);
                }
                else
                {
                    g.DrawLine(pen, x + 5, y + 10, x + 25, y + 10);
                }
                g.DrawString(text, _textFont, _textBrush, x + 30, y);
            }
        }
    }
}
