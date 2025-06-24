using Microsoft.EntityFrameworkCore;
using Monte_Karlo.DataBase;
using Monte_Karlo.Models;
using Monte_Karlo.Utilites;
using Monte_Karlo.Utilites.Calculators;
using Monte_Karlo.Utilites.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Monte_Karlo
{
    public partial class AnalysisForm : Form
    {
        private List<SimulationResult> _results = new List<SimulationResult>();
        private CircleParams _currentParams;
        private AnalysisView _view;

        public AnalysisForm()
        {
            InitializeComponent();
            _view = new AnalysisView();
        }

        public AnalysisForm(CircleParams circleParams) : this()
        {
            if (circleParams is not null)
            {
                _currentParams = circleParams;
                _results = circleParams.Results;
            }
        }

        private void AnalysisForm_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            CalculateStatistics();
        }

        private void SetupDataGridView()
        {
            dataGridViewResults.Columns.Clear();
            AddColumn("Id", "№", "Id", true, "D2", null, DataGridViewAutoSizeColumnMode.DisplayedCells);
            AddColumn("PointsInCircle", "Точек в окружности", "PointsInCircle", true, "N0");
            AddColumn("PointsInSegment", "Точек в сегменте", "PointsInSegment", true, "N0");
            AddColumn("AnalyticalResult", "Аналитический резльтат", "AnalyticalResult", true, "F4",
                     _currentParams?.AnalyticalResult.ToString("F4") ?? "N/A");
            AddColumn("MonteCarloResult", "Результат Монте-Карло", "MonteCarloResult", true, "F4");
            AddColumn("AbsoluteError", "Абсолютная ошибка", "AbsoluteError", true, "F2");
            AddColumn("RelativeError", "Ошибка (%)", "RelativeError", true, "F2");

            if (_currentParams != null && _currentParams.Results.Any())
            {
                int id = 0;
                var displayResults = _currentParams.Results
                    .OrderByDescending(r => r.Id)
                    .Select(r => new
                    {
                        Id = ++id,
                        r.PointsInCircle,
                        r.PointsInSegment,
                        AnalyticalResult = _currentParams.AnalyticalResult,
                        r.MonteCarloResult,
                        AbsoluteError = Calculator.CalculateAbsoluteError(_currentParams.AnalyticalResult, r.MonteCarloResult).ToString(),
                        RelativeError = Calculator.CalculateRelativeError(_currentParams.AnalyticalResult, r.MonteCarloResult).ToString()
                    })
                    .ToList();

                var bindingSource = new BindingSource() { DataSource = displayResults };
                dataGridViewResults.DataSource = bindingSource;
            }
            else
            {
                dataGridViewResults.DataSource = null;
            }
        }

        // Настраиваем цветовое форматирование для колонки с ошибкой
        private void DataGridViewResults_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            {
                if (e.ColumnIndex == dataGridViewResults.Columns["RelativeError"].Index && e.Value != null)
                {
                    double error = Convert.ToDouble(e.Value);
                    e.CellStyle.ForeColor = error switch
                    {
                        > 10 => Color.Red,
                        > 5 => Color.Orange,
                        _ => Color.Green
                    };
                }
            };
        }

        private void AddColumn(string name, string header, string dataPropertyName, bool isReadOnly, string format,
                               object defaultValue = null, DataGridViewAutoSizeColumnMode AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells)
        {
            var col = new DataGridViewTextBoxColumn
            {
                Name = name,
                HeaderText = header,
                DataPropertyName = dataPropertyName,
                ReadOnly = isReadOnly,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = format,
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                },
                SortMode = DataGridViewColumnSortMode.Programmatic,
                AutoSizeMode = AutoSizeMode
            };

            if (defaultValue != null)
            {
                col.DefaultCellStyle.NullValue = defaultValue;
            }

            dataGridViewResults.Columns.Add(col);

            dataGridViewResults.Columns.Cast<DataGridViewColumn>()
                .ToList()
                .ForEach(c => c.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill);

            this.Invalidate(true);
        }

        private void DataGridViewResults_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewColumn column = dataGridViewResults.Columns[e.ColumnIndex];

            // Определяем направление сортировки
            ListSortDirection direction = column.HeaderCell.SortGlyphDirection == SortOrder.Ascending ?
                ListSortDirection.Descending :
                ListSortDirection.Ascending;

            // Сортируем данные
            SortData(column.Name, direction);

            // Обновляем иконку сортировки
            dataGridViewResults.Columns.Cast<DataGridViewColumn>()
                .ToList()
                .ForEach(c => c.HeaderCell.SortGlyphDirection = SortOrder.None);

            dataGridViewResults.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = direction == ListSortDirection.Ascending ?
                SortOrder.Ascending :
                SortOrder.Descending;
        }

        private void SortData(string columnName, ListSortDirection direction)
        {
            if (dataGridViewResults.DataSource is BindingSource bindingSource)
            {
                var data = bindingSource.List.Cast<dynamic>().ToList();

                switch (columnName)
                {
                    case "PointsInCircle":
                        bindingSource.DataSource = direction == ListSortDirection.Ascending ?
                            data.OrderBy(x => x.PointsInCircle).ToList() :
                            data.OrderByDescending(x => x.PointsInCircle).ToList();
                        break;
                    case "PointsInSegment":
                        bindingSource.DataSource = direction == ListSortDirection.Ascending ?
                            data.OrderBy(x => x.PointsInSegment).ToList() :
                            data.OrderByDescending(x => x.PointsInSegment).ToList();
                        break;
                    case "MonteCarloResult":
                        bindingSource.DataSource = direction == ListSortDirection.Ascending ?
                            data.OrderBy(x => x.MonteCarloResult).ToList() :
                            data.OrderByDescending(x => x.MonteCarloResult).ToList();
                        break;
                    case "AnalyticalResult":
                        bindingSource.DataSource = direction == ListSortDirection.Ascending ?
                            data.OrderBy(x => x.AnalyticalResult).ToList() :
                            data.OrderByDescending(x => x.AnalyticalResult).ToList();
                        break;
                    case "AbsoluteError":
                        bindingSource.DataSource = direction == ListSortDirection.Ascending ?
                            data.OrderBy(x => Convert.ToDouble(x.AbsoluteError)).ToList() :
                            data.OrderByDescending(x => Convert.ToDouble(x.AbsoluteError)).ToList();
                        break;
                    case "RelativeError":
                        bindingSource.DataSource = direction == ListSortDirection.Ascending ?
                            data.OrderBy(x => Convert.ToDouble(x.RelativeError)).ToList() :
                            data.OrderByDescending(x => Convert.ToDouble(x.RelativeError)).ToList();
                        break;
                    default:
                        bindingSource.DataSource = direction == ListSortDirection.Ascending ?
                            data.OrderBy(x => x.Id).ToList() :
                            data.OrderByDescending(x => x.Id).ToList();
                        break;
                }
            }
        }

        private void CalculateStatistics()
        {
            if (_results == null || _results.Count == 0)
                return;

            var mcResults = _results.Select(r => r.MonteCarloResult).ToList();

            lblAnalisicResult.Text = _currentParams.AnalyticalResult.ToString("F4");
            lblMean.Text = mcResults.Average().ToString("F4");
            lblMedian.Text = StatisticCalculator.CalculateMedian(mcResults).ToString("F4");
            lblMinimum.Text = mcResults.Min().ToString("F4");
            lblMaximum.Text = mcResults.Max().ToString("F4");
            lblRange.Text = StatisticCalculator.CalculateRange(mcResults).ToString("F4");
        }

        private void paintPanel_Paint(object sender, PaintEventArgs e)
        {
            _view.RenderAnalysis(paintPanel, e, _currentParams);
            dataGridViewResults.Invalidate(true);

            base.OnPaint(e);
        }
    }
}
