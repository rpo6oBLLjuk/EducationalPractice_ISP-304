﻿using Monte_Karlo.Models;
using Monte_Karlo.Utilites.Calculators;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Monte_Karlo.Utilites.View
{
    public class AnalysisView
    {
        // Цвета элементов
        private static readonly Color _analyticalColor = Color.Blue;
        private static readonly Color _pointsColor = Color.Green;
        private static readonly Color _meanColor = Color.Red;
        private static readonly Color _medianColor = Color.Purple;
        private static readonly Color _minMaxColor = Color.Orange;
        private static readonly Color _backgroundColor = Color.White;
        private static readonly Color _gridColor = Color.LightGray;
        private static readonly Padding _padding = new Padding(50, 20, 70, 40);
        private static readonly double _percentYPadding = 0.1;
        private static readonly System.Drawing.Font _textFont = SystemFonts.DefaultFont;
        private static readonly Brush _textBrush = Brushes.Black;
        private static readonly float _pointRadius = 4;

        public void RenderAnalysis(Panel panel, PaintEventArgs e, CircleParams circleParams)
        {
            var g = e.Graphics;
            g.Clear(_backgroundColor);

            if (circleParams == null || circleParams.Results == null || circleParams.Results.Count == 0)
            {
                MessageBox.Show("Нет результатов для анализа", "Оповещение");
                return;
            }

            OnPaint(panel, g, circleParams);
        }

        private void OnPaint(Panel panel, Graphics g, CircleParams circleParams)
        {
            List<double> mcResults = circleParams.Results.Select(r => r.MonteCarloResult).ToList();
            double analyticalValue = circleParams.AnalyticalResult;

            double mean = mcResults.Average();
            double median = StatisticCalculator.CalculateMedian(mcResults);
            double min = mcResults.Min();
            double max = mcResults.Max();

            Rectangle plotArea = new Rectangle(_padding.Left, _padding.Top, panel.Width - _padding.Right, panel.Height - _padding.Bottom);

            double yMin = Math.Min(analyticalValue, min);
            double yMax = Math.Max(analyticalValue, max);
            double yRange = yMax - yMin;
            yMin -= yRange * _percentYPadding;
            yMax += yRange * _percentYPadding;
            yRange = yMax - yMin;

            DrawGrid(g, plotArea, mcResults.Count, yMin, yMax);

            DrawAnalyticalLine(g, plotArea, analyticalValue, yMin, yRange);
            DrawMonteCarloPoints(g, plotArea, mcResults, yMin, yRange);
            DrawMeanLine(g, plotArea, mean, yMin, yRange);
            DrawMedianLine(g, plotArea, median, yMin, yRange);
            DrawMinMaxLines(g, plotArea, min, max, yMin, yRange);
            DrawLegend(g, plotArea, median);
        }

        private void DrawGrid(Graphics g, Rectangle plotArea, int pointsCount, double yMin, double yMax)
        {
            Pen girdPen = new Pen(_gridColor);

            g.DrawRectangle(girdPen, plotArea);

            // Вертикальные линии (каждые 10% экспериментов)
            int step = Math.Max(1, pointsCount / 10);
            for (int i = 0; i <= pointsCount; i += step)
            {
                float x = plotArea.Left + plotArea.Width * i / pointsCount;
                g.DrawLine(girdPen, x, plotArea.Top, x, plotArea.Bottom);

                // Подписи номеров экспериментов
                string text = i.ToString();
                SizeF textSize = g.MeasureString(text, _textFont);
                float textX = x - textSize.Width / 2;
                float textY = plotArea.Bottom - textSize.Height;

                // особое расположение для 0
                if (i == 0)
                    textX += textSize.Width;

                g.DrawString(text, _textFont, _textBrush, textX, textY);
            }

            // Горизонтальные линии сетки (7 линий)
            int linesCount = 7 - 1;
            for (int i = 0; i <= linesCount; i++)
            {
                float y = plotArea.Top + plotArea.Height * i / linesCount;
                g.DrawLine(girdPen, plotArea.Left, y, plotArea.Right, y);

                // Подписи значений
                double yRange = yMax - yMin;
                double value = yMax - yRange * i / linesCount;
                string text = value.ToString("F2");
                SizeF textSize = g.MeasureString(text, _textFont);
                float textX = plotArea.Left - textSize.Width;
                float textY = y - textSize.Height / 2;

                g.DrawString(text, _textFont, _textBrush, textX, textY);
            }

        }

        private void DrawAnalyticalLine(Graphics g, Rectangle area, double value, double yMin, double yRange)
        {
            float y = area.Bottom - (float)((value - yMin) / yRange * area.Height);
            g.DrawLine(new Pen(_analyticalColor, 4), area.Left, y, area.Right, y);
        }

        private void DrawMonteCarloPoints(Graphics g, Rectangle area, List<double> results, double yMin, double yRange)
        {
            if (results == null || results.Count == 0)
                return;

            int count = results.Count;
            float width = area.Width;
            float height = area.Height;
            float left = area.Left;
            float bottom = area.Bottom;

            // Предварительно вычисляем часто используемые значения
            float xStep = width / (count - 1);
            float yScale = height / (float)yRange;

            // Используем один экземпляр кисти для всех точек
            using (SolidBrush brush = new SolidBrush(_pointsColor))
            {
                float diameter = 2 * _pointRadius;

                for (int i = 0; i < count; i++)
                {
                    try
                    {
                        float x = left + i * xStep;
                        float y = bottom - (float)((results[i] - yMin) * yScale);
                        g.FillEllipse(brush, x - _pointRadius, y - _pointRadius, diameter, diameter);
                    }
                    catch (DivideByZeroException ex)
                    {
                        MessageBox.Show("Слишком мало данных измерений (минимум 2)", "Оповещение");
                        return; // Прерываем выполнение после ошибки
                    }
                }
            }
        }

        private void DrawMeanLine(Graphics g, Rectangle area, double value, double yMin, double yRange)
        {
            float y = area.Bottom - (float)((value - yMin) / yRange * area.Height);
            g.DrawLine(new Pen(_meanColor, 3) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash },
                      area.Left, y, area.Right, y);
        }

        private void DrawMedianLine(Graphics g, Rectangle area, double value, double yMin, double yRange)
        {
            float y = area.Bottom - (float)((value - yMin) / yRange * area.Height);
            g.DrawLine(new Pen(_medianColor, 3) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dot },
                      area.Left, y, area.Right, y);
        }

        private void DrawMinMaxLines(Graphics g, Rectangle area, double min, double max, double yMin, double yRange)
        {
            float yMinPos = area.Bottom - (float)((min - yMin) / yRange * area.Height);
            float yMaxPos = area.Bottom - (float)((max - yMin) / yRange * area.Height);

            g.DrawLine(new Pen(_minMaxColor, 3), area.Left, yMinPos, area.Right, yMinPos);
            g.DrawLine(new Pen(_minMaxColor, 3), area.Left, yMaxPos, area.Right, yMaxPos);
        }

        private void DrawLegend(Graphics g, Rectangle area, double mode)
        {
            SizeF textSize = g.MeasureString("Аналитическое решение", _textFont);
            float boxWidth = 20;
            float startX = area.Width - textSize.Width - boxWidth - 5;
            float startY = area.Top;
            float itemHeight = textSize.Height;

            DrawLegendItem(g, "Аналитическое решение", _analyticalColor, startX, startY, boxWidth, itemHeight);
            DrawLegendItem(g, "Точки Монте-Карло", _pointsColor, startX, startY + itemHeight, boxWidth, itemHeight);
            DrawLegendItem(g, "Среднее значение", _meanColor, startX, startY + itemHeight * 2, boxWidth, itemHeight);
            DrawLegendItem(g, "Медиана", _medianColor, startX, startY + itemHeight * 3, boxWidth, itemHeight);
            DrawLegendItem(g, "Минимум/Максимум", _minMaxColor, startX, startY + itemHeight * 4, boxWidth, itemHeight);
        }

        private void DrawLegendItem(Graphics g, string text, Color color, float x, float y, float boxWidth, float boxHeight)
        {
            SizeF textSize = g.MeasureString("Аналитическое решение", _textFont);
            g.FillRectangle(new SolidBrush(color), x, y + 1, boxWidth, boxHeight - 2);
            g.DrawRectangle(Pens.Black, x, y + 1, boxWidth, boxHeight - 2);
            g.DrawString(text, _textFont, _textBrush, x + boxWidth + 5, y);
        }
    }
}