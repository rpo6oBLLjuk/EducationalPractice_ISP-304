using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Monte_Karlo.DataBase;
using Monte_Karlo.Models;
using Monte_Karlo.Utilites;
using Monte_Karlo.Utilites.Calculators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace Monte_Karlo.Forms
{
    public partial class DataManagementForm : Form
    {
        private DatabaseHelper databaseHelper = new DatabaseHelper();
        private Logger logger = new Logger();
        private CancellationTokenSource _cts;

        public DataManagementForm()
        {
            InitializeComponent();
        }

        private void DataManagementForm_Load(object sender, EventArgs e)
        {
            LoadExperiments();
        }

        private void LoadExperiments()
        {
            try
            {
                dgvExperiments.Columns.Clear();
                var data = databaseHelper.GetAllData();
                var experiments = data
                    .Select(cp => new
                    {
                        ID = cp.Id,
                        Всего_точек = cp.TotalPoints,
                        Аналитический_результат = cp.AnalyticalResult,
                        Количество_экспериментов = cp.Results.Count
                    })
                    .ToList();

                var bindingSource = new BindingSource() { DataSource = experiments };
                dgvExperiments.DataSource = bindingSource;
                dgvExperiments.Columns["ID"].Visible = false; // Скрываем ID
                lblStatus.Text = $"Загружено экспериментов: {experiments.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.LogException(ex, "Ошибка загрузки данных");
            }
        }

        //private void btnBackup_Click(object sender, EventArgs e)
        //{
        //    using (var saveDialog = new SaveFileDialog())
        //    {
        //        saveDialog.Filter = "SQLite база данных|*.db";
        //        saveDialog.Title = "Создание резервной копии";
        //        saveDialog.FileName = $"MonteCarlo_Backup_{DateTime.Now:yyyyMMdd_HHmmss}.db";

        //        if (saveDialog.ShowDialog() == DialogResult.OK)
        //        {
        //            try
        //            {
        //                databaseHelper.CreateBackup(saveDialog.FileName);
        //                lblStatus.Text = $"Резервная копия создана: {Path.GetFileName(saveDialog.FileName)}";
        //                MessageBox.Show("Резервное копирование выполнено успешно!", "Успех",
        //                    MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            }
        //            catch (Exception ex)
        //            {
        //                MessageBox.Show($"Ошибка резервного копирования: {ex.Message}", "Ошибка",
        //                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                logger.LogException(ex, "Ошибка резервного копирования");
        //            }
        //        }
        //    }
        //}

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить ВСЕ данные экспериментов? Это действие нельзя отменить.",
                "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    databaseHelper.ClearDatabase();
                    LoadExperiments();
                    lblStatus.Text = "Все данные экспериментов удалены";
                    MessageBox.Show("Все данные успешно удалены.", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления данных: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    logger.LogException(ex, "Ошибка удаления данных");
                }
            }
        }

        private void btnClearSelected_Click(object sender, EventArgs e)
        {
            if (dgvExperiments.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите эксперимент для удаления", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedId = (int)dgvExperiments.SelectedRows[0].Cells["ID"].Value;
            var centerX = dgvExperiments.SelectedRows[0].Cells["Центр_X"].Value;
            var centerY = dgvExperiments.SelectedRows[0].Cells["Центр_Y"].Value;

            if (MessageBox.Show($"Вы уверены, что хотите удалить все данные для эксперимента с центром ({centerX}, {centerY})?",
                    "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    databaseHelper.RemoveCircleParamsById(selectedId);
                    LoadExperiments();
                    lblStatus.Text = "Выбранный эксперимент удалён";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления эксперимента: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    logger.LogException(ex, "Ошибка удаления эксперимента");
                }
            }
        }

        private void btnanalysisOfResults_Click(object sender, EventArgs e)
        {
            if (dgvExperiments.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите эксперимент для анализа", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedId = (int)dgvExperiments.SelectedRows[0].Cells["ID"].Value;
            var circleParam = databaseHelper.GetDataById(selectedId);
            var form = new AnalysisForm(circleParam);
            form.ShowDialog();
        }

        private void dgvExperiments_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dgvExperiments.RowCount == 0)
                return;

            DataGridViewColumn column = dgvExperiments.Columns[e.ColumnIndex];

            // Определяем направление сортировки
            ListSortDirection direction = column.HeaderCell.SortGlyphDirection == SortOrder.Ascending ?
                ListSortDirection.Descending :
                ListSortDirection.Ascending;

            // Сортируем данные
            SortData(column.Name, direction);

            // Обновляем иконку сортировки
            dgvExperiments.Columns.Cast<DataGridViewColumn>()
                .ToList()
                .ForEach(c => c.HeaderCell.SortGlyphDirection = SortOrder.None);

            dgvExperiments.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = direction == ListSortDirection.Ascending ?
                SortOrder.Ascending :
                SortOrder.Descending;
        }

        private void SortData(string columnName, ListSortDirection direction)
        {
            if (dgvExperiments.DataSource is BindingSource bindingSource)
            {
                var data = bindingSource.List.Cast<dynamic>().ToList();

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
                        bindingSource.DataSource = direction == ListSortDirection.Ascending ?
                            data.OrderBy(x => x.ID).ToList() :
                            data.OrderByDescending(x => x.ID).ToList();
                        break;
                }
            }
        }

        private void DataManagementForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }
    }
}
