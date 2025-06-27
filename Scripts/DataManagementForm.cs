using Monte_Karlo.DataBase;
using Monte_Karlo.Utilites;
using System.ComponentModel;
using System.Data;

namespace Monte_Karlo.Forms
{
    // Класс формы для управления экспериментальными данными
    public partial class DataManagementForm : Form
    {
        // Объект для работы с базой данных
        private DatabaseHelper databaseHelper = new DatabaseHelper();
        // Объект для логирования ошибок и информации
        private Logger logger = new Logger();
        // Источник отмены для возможных асинхронных операций (не используется явно в коде)
        private CancellationTokenSource _cts;

        // Конструктор формы
        public DataManagementForm()
        {
            InitializeComponent(); // Инициализация компонентов формы
        }

        // Обработчик события загрузки формы
        private void DataManagementForm_Load(object sender, EventArgs e)
        {
            LoadExperiments(); // Загружаем данные экспериментов при старте формы
        }

        // Метод для загрузки списка экспериментов и отображения в таблице
        private void LoadExperiments()
        {
            try
            {
                dgvExperiments.Columns.Clear(); // Очищаем столбцы в DataGridView
                var data = databaseHelper.GetAllData(); // Получаем все данные из базы
                var experiments = data
                    .Select(cp => new
                    {
                        ID = cp.Id, // Идентификатор эксперимента
                        Всего_точек = cp.TotalPoints, // Общее количество точек в эксперименте
                        Аналитический_результат = cp.AnalyticalResult, // Результат аналитического расчёта
                        Количество_экспериментов = cp.Results.Count // Количество отдельных результатов внутри эксперимента
                    })
                    .ToList();

                // Привязываем данные к таблице через BindingSource для удобства обновления
                var bindingSource = new BindingSource() { DataSource = experiments };
                dgvExperiments.DataSource = bindingSource;

                dgvExperiments.Columns["ID"].Visible = false; // Скрываем колонку с ID, чтобы не показывать пользователю
                lblStatus.Text = $"Загружено экспериментов: {experiments.Count}"; // Обновляем статус внизу формы
            }
            catch (Exception ex)
            {
                // При ошибке загрузки показываем сообщение и логируем исключение
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.LogException(ex, "Ошибка загрузки данных");
            }
        }


        // Обработчик кнопки "Удалить все данные"
        private void btnClearAll_Click(object sender, EventArgs e)
        {
            // Спрашиваем подтверждение удаления всех данных
            if (MessageBox.Show("Вы уверены, что хотите удалить ВСЕ данные экспериментов? Это действие нельзя отменить.",
                "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    databaseHelper.ClearDatabase(); // Очищаем всю базу данных
                    LoadExperiments(); // Обновляем таблицу
                    lblStatus.Text = "Все данные экспериментов удалены"; // Обновляем статус
                    MessageBox.Show("Все данные успешно удалены.", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    // При ошибке удаления показываем сообщение и логируем
                    MessageBox.Show($"Ошибка удаления данных: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    logger.LogException(ex, "Ошибка удаления данных");
                }
            }
        }

        // Обработчик кнопки "Удалить выбранный эксперимент"
        private void btnClearSelected_Click(object sender, EventArgs e)
        {
            // Если не выбран ни один эксперимент - предупреждаем пользователя
            if (dgvExperiments.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите эксперимент для удаления", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Получаем ID выбранного эксперимента
            var selectedId = (int)dgvExperiments.SelectedRows[0].Cells["ID"].Value;

            // Спрашиваем подтверждение удаления выбранного эксперимента
            if (MessageBox.Show($"Вы уверены, что хотите удалить все данные для эксперимента?",
                    "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    databaseHelper.RemoveCircleParamsById(selectedId); // Удаляем данные по ID
                    LoadExperiments(); // Обновляем таблицу
                    lblStatus.Text = "Выбранный эксперимент удалён"; // Обновляем статус
                }
                catch (Exception ex)
                {
                    // При ошибке удаления показываем сообщение и логируем
                    MessageBox.Show($"Ошибка удаления эксперимента: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    logger.LogException(ex, "Ошибка удаления эксперимента");
                }
            }
        }

        // Обработчик кнопки "Анализ результатов"
        private void btnanalysisOfResults_Click(object sender, EventArgs e)
        {
            // Проверяем, выбран ли эксперимент для анализа
            if (dgvExperiments.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите эксперимент для анализа", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Получаем ID выбранного эксперимента
            var selectedId = (int)dgvExperiments.SelectedRows[0].Cells["ID"].Value;
            // Получаем данные эксперимента из базы
            var circleParam = databaseHelper.GetDataById(selectedId);
            // Открываем форму анализа с этими данными
            var form = new AnalysisForm(circleParam);
            form.ShowDialog();
        }

        // Обработчик клика по заголовку столбца для сортировки
        private void dgvExperiments_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Если таблица пустая - выходим
            if (dgvExperiments.RowCount == 0)
                return;

            // Определяем столбец, по которому кликнули
            DataGridViewColumn column = dgvExperiments.Columns[e.ColumnIndex];

            // Определяем направление сортировки (переключаем между Asc и Desc)
            ListSortDirection direction = column.HeaderCell.SortGlyphDirection == SortOrder.Ascending ?
                ListSortDirection.Descending :
                ListSortDirection.Ascending;

            // Вызываем сортировку данных по выбранному столбцу и направлению
            SortData(column.Name, direction);

            // Сбрасываем все иконки сортировки на None
            dgvExperiments.Columns.Cast<DataGridViewColumn>()
                .ToList()
                .ForEach(c => c.HeaderCell.SortGlyphDirection = SortOrder.None);

            // Устанавливаем иконку сортировки у выбранного столбца
            dgvExperiments.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = direction == ListSortDirection.Ascending ?
                SortOrder.Ascending :
                SortOrder.Descending;
        }

        // Метод сортировки данных в таблице
        private void SortData(string columnName, ListSortDirection direction)
        {
            // Проверяем, что источник данных является BindingSource
            if (dgvExperiments.DataSource is BindingSource bindingSource)
            {
                // Получаем текущий список данных
                var data = bindingSource.List.Cast<dynamic>().ToList();

                // Сортируем по нужному столбцу и направлению
                switch (columnName)
                {
                    case "ID":
                        bindingSource.DataSource = direction == ListSortDirection.Ascending ?
                            data.OrderBy(x => x.ID).ToList() :
                            data.OrderByDescending(x => x.ID).ToList();
                        break;
                    case "Центр_X":
                        bindingSource.DataSource = direction == ListSortDirection.Ascending ?
                            data.OrderBy(x => x.Центр_X).ToList() :
                            data.OrderByDescending(x => x.Центр_X).ToList();
                        break;
                    case "Центр_Y":
                        bindingSource.DataSource = direction == ListSortDirection.Ascending ?
                            data.OrderBy(x => x.Центр_Y).ToList() :
                            data.OrderByDescending(x => x.Центр_Y).ToList();
                        break;
                    case "Радиус":
                        bindingSource.DataSource = direction == ListSortDirection.Ascending ?
                            data.OrderBy(x => x.Радиус).ToList() :
                            data.OrderByDescending(x => x.Радиус).ToList();
                        break;
                    case "Направление":
                        bindingSource.DataSource = direction == ListSortDirection.Ascending ?
                            data.OrderBy(x => x.Направление).ToList() :
                            data.OrderByDescending(x => x.Направление).ToList();
                        break;
                    case "Параметр_C":
                        bindingSource.DataSource = direction == ListSortDirection.Ascending ?
                            data.OrderBy(x => x.Параметр_C).ToList() :
                            data.OrderByDescending(x => x.Параметр_C).ToList();
                        break;
                    case "Всего_точек":
                        bindingSource.DataSource = direction == ListSortDirection.Ascending ?
                            data.OrderBy(x => x.Всего_точек).ToList() :
                            data.OrderByDescending(x => x.Всего_точек).ToList();
                        break;
                    case "Аналитический_результат":
                        bindingSource.DataSource = direction == ListSortDirection.Ascending ?
                            data.OrderBy(x => x.Аналитический_результат).ToList() :
                            data.OrderByDescending(x => x.Аналитический_результат).ToList();
                        break;
                    case "Количество_экспериментов":
                        bindingSource.DataSource = direction == ListSortDirection.Ascending ?
                            data.OrderBy(x => x.Количество_экспериментов).ToList() :
                            data.OrderByDescending(x => x.Количество_экспериментов).ToList();
                        break;
                    default:
                        // По умолчанию сортируем по ID
                        bindingSource.DataSource = direction == ListSortDirection.Ascending ?
                            data.OrderBy(x => x.ID).ToList() :
                            data.OrderByDescending(x => x.ID).ToList();
                        break;
                }
            }
        }

        // Обработчик закрытия формы
        private void DataManagementForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Отменяем и освобождаем ресурсы CancellationTokenSource, если он используется
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }
    }
}
