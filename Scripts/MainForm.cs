﻿using Monte_Karlo.DataBase;
using Monte_Karlo.Forms;
using Monte_Karlo.Models;
using Monte_Karlo.Utilites;
using Monte_Karlo.Utilites.Calculators;
using Monte_Karlo.Utilites.View;
using System.Diagnostics;
using System.Reflection;

namespace Monte_Karlo
{
    // Главная форма приложения
    public partial class MainForm : Form
    {
        // Коэффициенты масштабирования и деления
        private float cofficient = 2;
        private float divisionScale = 0.5f;

        // Модель круга, по которому будет проводиться расчёт
        private Circle circle = new Circle();

        // Количество точек для метода Монте-Карло
        private int pointsCount = 100_000;

        // Служебные переменные и вспомогательные классы
        private CancellationTokenSource _generationCts;
        private PointsGenerator _pointsGenerator;
        private MonteCarloView _view;
        private DatabaseHelper _databaseHelper;
        private Logger _logger;

        // Конструктор формы
        public MainForm()
        {
            InitializeComponent();
            _pointsGenerator = new PointsGenerator();
            _databaseHelper = new DatabaseHelper();
            _view = new MonteCarloView();
            _logger = new Logger();

            // Включаем двойную буферизацию для избежания мерцания
            DoubleBuffered = true;
            typeof(Panel).InvokeMember("DoubleBuffered",
                BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                null, paintPanel, new object[] { true });

            InitializeControlPanel();
            _databaseHelper.InitializeDatabase();
            _logger.Log("Приложение запущено");

            FirstCalculation();
        }

        // Инициализация элементов управления панели управления
        private void InitializeControlPanel()
        {
            scaleTrackBar.Value = _view.GridStep;
            scaleLabel.Text = $"Масштаб: {scaleTrackBar.Value}";
            pointsCountUpdown.Value = pointsCount;
        }

        // Первоначальный запуск вычислений
        private async void FirstCalculation()
        {
            await Task.Delay(100);
            await MonteCarloCalculate(true);
        }

        // Отрисовка панели с результатами расчёта
        private void paintPanel_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                _view.RenderToBuffer(
                    paintPanel,
                    e,
                    circle,
                    _pointsGenerator.GetCurrentPoints()
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка из-за частой переотрисовки графика.\n" +
                    "Пожалуйста, дайте время на переотрисовку графика", "Ошибка");
                Thread.Sleep(100);
                _view.RenderToBuffer(
                    paintPanel,
                    e,
                    circle,
                    _pointsGenerator.GetCurrentPoints()
                );
            }

            base.OnPaint(e);
        }

        // Обработка изменения масштаба через трекбар
        private void scaleTrackbar_Scroll(object sender, EventArgs e)
        {
            scaleLabel.Text = $"Масштаб: {scaleTrackBar.Value}";
            _view.GridStep = scaleTrackBar.Value;
            paintPanel.Invalidate();
        }

        // Изменение количества точек через numeric updown
        private async void pointsCountUpdown_ValueChanged(object sender, EventArgs e)
        {
            pointsCount = (int)pointsCountUpdown.Value;
            await MonteCarloCalculate(true);
        }

        // Кнопка запуска генерации точек
        private async void btnGeneratePoints_Click(object sender, EventArgs e)
        {
            await MonteCarloCalculate(true);
        }

        // Основной метод вычисления площади методом Монте-Карло
        private async Task MonteCarloCalculate(bool generateNewPoints)
        {
            if (this.Visible != true)
                return;

            _generationCts?.Cancel();
            _generationCts = new CancellationTokenSource();

            try
            {
                var token = _generationCts.Token;

                if (generateNewPoints)
                {
                    await _pointsGenerator.GenerateRandomPointsAsync(circle, pointsCount, token);
                }
                else
                {
                    await _pointsGenerator.CalculateCuttedPointsAsync(circle, pointsCount, token);
                }

                if (token.IsCancellationRequested)
                    return;

                paintPanel.Invalidate();

                double realSquare = Calculator.CalculateAnalyticArea(circle);
                var roundedRealSquare = Math.Round(realSquare, 6);

                var currentPoints = _pointsGenerator.GetCurrentPoints();
                double monteCarloSquare = Calculator.CalculateMonteCarloArea(
                    circle.radius,
                    currentPoints.Points.Count,
                    currentPoints.CuttedPoints.Count);
                var roundedMonteCarloSquare = Math.Round(monteCarloSquare, 6);

                ShowAnswereMessage(realSquare, monteCarloSquare, currentPoints);
                _logger.Log($"Сделаны расчёты с параметрами: {circle.ToString()} и количеством точек {pointsCount}");

                WriteResultOnLabels(roundedRealSquare, roundedMonteCarloSquare);
                _databaseHelper.SaveResults(
                    circle,
                    currentPoints,
                    realSquare,
                    monteCarloSquare);
            }
            catch (OperationCanceledException)
            {
                // Игнорируем отмену задачи
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        // Отображение исключения в виде окна сообщения
        private void ShowException(Exception ex)
        {
            MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            _logger.LogException(ex);
        }

        // Вывод результатов расчётов в элементы управления
        private void WriteResultOnLabels(double? realSquare, double monteCarloSquare)
        {
            if (realSquare.HasValue)
                realSquareLabel.Text = $"Аналитически: {realSquare:F6}";
            monteCarloSquareLabel.Text = $"Методом Монте-Карло: {monteCarloSquare:F6}";
        }

        // Показать сообщение с результатами расчёта, если включено отображение
        private void ShowAnswereMessage(double realSquare, double monteCarloSquare, PointsData currentPoints)
        {
            if (!showMessageCheckBox.Checked)
                return;

            double absoluteError = Calculator.CalculateAbsoluteError(realSquare, monteCarloSquare);
            double relativeError = Calculator.CalculateRelativeError(realSquare, monteCarloSquare);
            double roundAbsoluteError = Math.Round(absoluteError, 6);
            double roundRelativeError = Math.Round(relativeError, 6);
            double maxAccuracy = 1 / (double)pointsCount;

            string message = $"""
            Всего точек: {currentPoints.Points.Count}
            Количество точек попавших в круг {currentPoints.IncludedPoints.Count}
            Количество точек в большей секции: {currentPoints.CuttedPoints.Count}
            ---------------------------------------------------------------------
            Площадь круга: {Calculator.CircleSuare(circle.radius):F8}
            Площадь секции аналитически: {realSquare:F8}
            Площадь секции методом Монте-Карло: {monteCarloSquare:F8}
            ---------------------------------------------------------------------
            Абсолютаня погрешность вычислений: {roundAbsoluteError}
            Относительная погрешность вычислений: {roundRelativeError}%
            Максимальная точность при заданном количестве точек: {maxAccuracy}
            """;

            MessageBox.Show(message, "Результат вычислений");
        }

        // Обработка закрытия формы
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _generationCts?.Cancel();
            _logger.Log("Приложение закрыто");
            Application.Exit();
        }

        // Открытие файла справки
        private void programHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string helpFile = Path.Combine(Application.StartupPath, "Help", "annotatsiya.htm");

                // Открываем справку в браузере по умолчанию
                Process.Start(new ProcessStartInfo
                {
                    FileName = helpFile,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), $"Не удалось открыть справку");
            }
        }

        // Открытие окна "О программе"
        private void aboutProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var form = new AboutProgramForm();
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        // Очистка всех точек и графика
        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                _pointsGenerator.ClearPoints();
                paintPanel.Invalidate();
                WriteResultOnLabels(null, 0);
                _logger.Log("Очищение точек");
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        // Выход из приложения через меню
        private void closeProgramToolStripMenuItem_Click(object sender, EventArgs e) => this.Close();

        // Открытие формы анализа результатов
        private void analysisOfResultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var circleParam = _databaseHelper.GetData(circle, pointsCount);
                var form = new AnalysisForm(circleParam);
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        // Обработка изменения размеров панели отрисовки
        private void paintPanel_Resize(object sender, EventArgs e)
        {
            paintPanel.Invalidate();
        }

        // Открытие окна управления экспериментами
        private void ExperementsControlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var form = new DataManagementForm();
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }
    }
}