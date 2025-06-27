using Monte_Karlo.Models;
using Monte_Karlo.Utilites.Calculators;
using Monte_Karlo.Utilites.View;
using System.ComponentModel;
using System.Data;

namespace Monte_Karlo
{
    // Форма анализа результатов расчетов Монте-Карло
    public partial class AnalysisForm : Form
    {
        // Список результатов симуляций, отображаемых в таблице
        private List<SimulationResult> _results = new List<SimulationResult>();

        // Текущие параметры круга (входные данные и результаты)
        private CircleParams _currentParams;

        // Представление для визуализации анализа
        private AnalysisView _view;

        // Конструктор по умолчанию
        public AnalysisForm()
        {
            InitializeComponent(); // Инициализация компонентов формы
            _view = new AnalysisView(); // Создание объекта визуализации
        }

        // Конструктор с параметрами круга
        public AnalysisForm(CircleParams circleParams) : this()
        {
            if (circleParams is not null)
            {
                _currentParams = circleParams;      // Сохраняем текущие параметры
                _results = circleParams.Results;    // Загружаем список результатов
            }
        }

        // Обработчик события загрузки формы
        private void AnalysisForm_Load(object sender, EventArgs e)
        {
            SetupDataGridView();   // Настройка таблицы для отображения результатов
            CalculateStatistics(); // Вычисление и отображение статистики
        }

        // Настройка столбцов и данных DataGridView
        private void SetupDataGridView()
        {
            dataGridViewResults.Columns.Clear(); // Очистка всех столбцов перед настройкой

            // Добавление столбцов с разными параметрами: имя, заголовок, имя свойства данных, формат и т.д.
            AddColumn("Id", "№", "Id", true, "D2", null, DataGridViewAutoSizeColumnMode.DisplayedCells);
            AddColumn("PointsInCircle", "Всего Точек", "Points", true, "N0", AutoSizeMode: DataGridViewAutoSizeColumnMode.Fill);
            AddColumn("PointsInSegment", "Точек в сегменте", "PointsInSegment", true, "N0", AutoSizeMode: DataGridViewAutoSizeColumnMode.Fill);
            AddColumn("AnalyticalResult", "Аналитический резльтат", "AnalyticalResult", true, "F4",
                     _currentParams?.AnalyticalResult.ToString("F4") ?? "N/A", AutoSizeMode: DataGridViewAutoSizeColumnMode.Fill);
            AddColumn("MonteCarloResult", "Результат Монте-Карло", "MonteCarloResult", true, "F4", AutoSizeMode: DataGridViewAutoSizeColumnMode.Fill);
            AddColumn("AbsoluteError", "Абсолютная погрешность", "AbsoluteError", true, "F2", AutoSizeMode: DataGridViewAutoSizeColumnMode.Fill);
            AddColumn("RelativeError", "Ошибка (%)", "RelativeError", true, "F2", AutoSizeMode: DataGridViewAutoSizeColumnMode.Fill);

            // Если есть параметры и результаты, заполняем таблицу данными
            if (_currentParams != null && _currentParams.Results.Any())
            {
                int id = 0;
                var displayResults = _currentParams.Results
                    .OrderByDescending(r => r.Id) // Сортируем по убыванию Id
                    .Select(r => new
                    {
                        Id = ++id, // Порядковый номер записи для отображения
                        r.Points,
                        r.PointsInSegment,
                        _currentParams.AnalyticalResult,
                        r.MonteCarloResult,
                        // Вычисление абсолютной и относительной ошибки с помощью калькулятора
                        AbsoluteError = Calculator.CalculateAbsoluteError(_currentParams.AnalyticalResult, r.MonteCarloResult).ToString(),
                        RelativeError = Calculator.CalculateRelativeError(_currentParams.AnalyticalResult, r.MonteCarloResult).ToString()
                    })
                    .ToList();

                // Создаем источник данных для DataGridView и присваиваем его
                var bindingSource = new BindingSource() { DataSource = displayResults };
                dataGridViewResults.DataSource = bindingSource;
            }
            else
            {
                dataGridViewResults.DataSource = null; // Если данных нет - очищаем таблицу
            }
        }

        // Форматирование ячеек, например, цвет текста в зависимости от значения ошибки
        private void DataGridViewResults_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dataGridViewResults.Columns["RelativeError"].Index && e.Value != null)
            {
                double error = Convert.ToDouble(e.Value);
                // Меняем цвет текста: красный при ошибке > 10%, оранжевый > 5%, иначе зеленый
                e.CellStyle.ForeColor = error switch
                {
                    > 10 => Color.Red,
                    > 5 => Color.Orange,
                    _ => Color.Green
                };
            }
        }

        // Метод добавления столбца в DataGridView с параметрами форматирования и отображения
        private void AddColumn(string name, string header, string dataPropertyName, bool isReadOnly, string format,
                               object defaultValue = null, DataGridViewAutoSizeColumnMode AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells)
        {
            var col = new DataGridViewTextBoxColumn
            {
                Name = name,                    // Имя столбца
                HeaderText = header,            // Текст заголовка
                DataPropertyName = dataPropertyName, // Свойство объекта для отображения
                ReadOnly = isReadOnly,          // Только для чтения
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = format,             // Формат отображения данных (числовой, текстовый и т.д.)
                    Alignment = DataGridViewContentAlignment.MiddleCenter, // Выравнивание по центру
                },
                SortMode = DataGridViewColumnSortMode.Programmatic, // Сортировка программная (обрабатывается вручную)
                AutoSizeMode = AutoSizeMode
            };

            // Если указан дефолтный текст для пустых значений
            if (defaultValue != null)
            {
                col.DefaultCellStyle.NullValue = defaultValue;
            }

            dataGridViewResults.Columns.Add(col); // Добавляем столбец в таблицу
        }

        // Обработчик клика по заголовку столбца для сортировки данных
        private void DataGridViewResults_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewColumn column = dataGridViewResults.Columns[e.ColumnIndex];

            // Определяем направление сортировки (переключаем между Ascending и Descending)
            ListSortDirection direction = column.HeaderCell.SortGlyphDirection == SortOrder.Ascending ?
                ListSortDirection.Descending :
                ListSortDirection.Ascending;

            // Выполняем сортировку по выбранному столбцу и направлению
            SortData(column.Name, direction);

            // Сброс иконок сортировки у всех столбцов
            dataGridViewResults.Columns.Cast<DataGridViewColumn>()
                .ToList()
                .ForEach(c => c.HeaderCell.SortGlyphDirection = SortOrder.None);

            // Устанавливаем иконку сортировки для текущего столбца
            dataGridViewResults.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = direction == ListSortDirection.Ascending ?
                SortOrder.Ascending :
                SortOrder.Descending;
        }

        // Метод сортировки данных по имени столбца и направлению сортировки
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
                        // По умолчанию сортируем по Id
                        bindingSource.DataSource = direction == ListSortDirection.Ascending ?
                            data.OrderBy(x => x.Id).ToList() :
                            data.OrderByDescending(x => x.Id).ToList();
                        break;
                }
            }
        }

        // Вычисление и отображение статистических характеристик результатов
        private void CalculateStatistics()
        {
            if (_results == null || _results.Count == 0)
                return; // Если результатов нет - выход

            var mcResults = _results.Select(r => r.MonteCarloResult).ToList();

            // Отображение аналитического результата
            lblAnalisicResult.Text = _currentParams.AnalyticalResult.ToString("F4");

            // Среднее значение результатов Монте-Карло
            lblMean.Text = mcResults.Average().ToString("F4");

            // Медиана
            lblMedian.Text = StatisticCalculator.CalculateMedian(mcResults).ToString("F4");

            // Минимальное значение
            lblMinimum.Text = mcResults.Min().ToString("F4");

            // Максимальное значение
            lblMaximum.Text = mcResults.Max().ToString("F4");

            // Размах (максимум - минимум)
            lblRange.Text = StatisticCalculator.CalculateRange(mcResults).ToString("F4");
        }

        // Отрисовка панели с визуализацией анализа
        private void paintPanel_Paint(object sender, PaintEventArgs e)
        {
            _view.RenderAnalysis(paintPanel, e, _currentParams); // Рендер визуализации на панели
            dataGridViewResults.Invalidate(true); // Перерисовка таблицы для обновления визуала

            base.OnPaint(e); // Вызов базового обработчика отрисовки
        }
    }
}
