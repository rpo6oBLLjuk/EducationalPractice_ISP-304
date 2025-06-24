using System.Windows.Forms;

namespace Monte_Karlo.Forms
{
    partial class DataManagementForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataManagementForm));
            dgvExperiments = new DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            btnBackup = new Button();
            btnClearAll = new Button();
            btnClearSelected = new Button();
            btnanalysisOfResults = new Button();
            statusStrip = new StatusStrip();
            lblStatus = new ToolStripStatusLabel();
            tableLayoutPanel1 = new TableLayoutPanel();
            btn1000Experiments = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvExperiments).BeginInit();
            statusStrip.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // dgvExperiments
            // 
            dgvExperiments.AllowUserToAddRows = false;
            dgvExperiments.AllowUserToDeleteRows = false;
            dgvExperiments.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvExperiments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.LightSteelBlue;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvExperiments.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvExperiments.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvExperiments.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2, Column3 });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvExperiments.DefaultCellStyle = dataGridViewCellStyle2;
            dgvExperiments.EnableHeadersVisualStyles = false;
            dgvExperiments.Location = new Point(0, 0);
            dgvExperiments.MultiSelect = false;
            dgvExperiments.Name = "dgvExperiments";
            dgvExperiments.ReadOnly = true;
            dgvExperiments.RowHeadersVisible = false;
            dgvExperiments.RowHeadersWidth = 72;
            dgvExperiments.RowTemplate.Height = 25;
            dgvExperiments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvExperiments.Size = new Size(1406, 706);
            dgvExperiments.TabIndex = 0;
            dgvExperiments.ColumnHeaderMouseClick += dgvExperiments_ColumnHeaderMouseClick;
            // 
            // Column1
            // 
            Column1.HeaderText = "Column1";
            Column1.MinimumWidth = 9;
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            // 
            // Column2
            // 
            Column2.HeaderText = "Column2";
            Column2.MinimumWidth = 9;
            Column2.Name = "Column2";
            Column2.ReadOnly = true;
            // 
            // Column3
            // 
            Column3.HeaderText = "Column3";
            Column3.MinimumWidth = 9;
            Column3.Name = "Column3";
            Column3.ReadOnly = true;
            // 
            // btnBackup
            // 
            btnBackup.Dock = DockStyle.Fill;
            btnBackup.Location = new Point(30, 10);
            btnBackup.Margin = new Padding(30, 10, 30, 10);
            btnBackup.Name = "btnBackup";
            btnBackup.Size = new Size(221, 80);
            btnBackup.TabIndex = 1;
            btnBackup.Text = "Создать резервную копию";
            btnBackup.Click += btnBackup_Click;
            // 
            // btnClearAll
            // 
            btnClearAll.Dock = DockStyle.Fill;
            btnClearAll.Location = new Point(311, 10);
            btnClearAll.Margin = new Padding(30, 10, 30, 10);
            btnClearAll.Name = "btnClearAll";
            btnClearAll.Size = new Size(221, 80);
            btnClearAll.TabIndex = 2;
            btnClearAll.Text = "Очистить все данные";
            btnClearAll.Click += btnClearAll_Click;
            // 
            // btnClearSelected
            // 
            btnClearSelected.Dock = DockStyle.Fill;
            btnClearSelected.Location = new Point(592, 10);
            btnClearSelected.Margin = new Padding(30, 10, 30, 10);
            btnClearSelected.Name = "btnClearSelected";
            btnClearSelected.Size = new Size(221, 80);
            btnClearSelected.TabIndex = 3;
            btnClearSelected.Text = "Удалить эксперимент";
            btnClearSelected.Click += btnClearSelected_Click;
            // 
            // btnanalysisOfResults
            // 
            btnanalysisOfResults.Dock = DockStyle.Fill;
            btnanalysisOfResults.Location = new Point(873, 10);
            btnanalysisOfResults.Margin = new Padding(30, 10, 30, 10);
            btnanalysisOfResults.Name = "btnanalysisOfResults";
            btnanalysisOfResults.Size = new Size(221, 80);
            btnanalysisOfResults.TabIndex = 4;
            btnanalysisOfResults.Text = "Анализ результата";
            btnanalysisOfResults.Click += btnanalysisOfResults_Click;
            // 
            // statusStrip
            // 
            statusStrip.ImageScalingSize = new Size(28, 28);
            statusStrip.Items.AddRange(new ToolStripItem[] { lblStatus });
            statusStrip.Location = new Point(0, 822);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(1406, 26);
            statusStrip.TabIndex = 5;
            // 
            // lblStatus
            // 
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(49, 20);
            lblStatus.Text = "          ";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 5;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.Controls.Add(btn1000Experiments, 4, 0);
            tableLayoutPanel1.Controls.Add(btnBackup, 0, 0);
            tableLayoutPanel1.Controls.Add(btnClearAll, 1, 0);
            tableLayoutPanel1.Controls.Add(btnanalysisOfResults, 3, 0);
            tableLayoutPanel1.Controls.Add(btnClearSelected, 2, 0);
            tableLayoutPanel1.Dock = DockStyle.Bottom;
            tableLayoutPanel1.Location = new Point(0, 722);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1406, 100);
            tableLayoutPanel1.TabIndex = 6;
            // 
            // btn1000Experiments
            // 
            btn1000Experiments.Dock = DockStyle.Fill;
            btn1000Experiments.Location = new Point(1154, 10);
            btn1000Experiments.Margin = new Padding(30, 10, 30, 10);
            btn1000Experiments.Name = "btn1000Experiments";
            btn1000Experiments.Size = new Size(222, 80);
            btn1000Experiments.TabIndex = 5;
            btn1000Experiments.Text = "Провести 1000 эксперементов";
            btn1000Experiments.Click += btn1000Experiments_Click;
            // 
            // DataManagementForm
            // 
            ClientSize = new Size(1406, 848);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(dgvExperiments);
            Controls.Add(statusStrip);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "DataManagementForm";
            Text = "Управление экспериментами";
            FormClosed += DataManagementForm_FormClosed;
            Load += DataManagementForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvExperiments).EndInit();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvExperiments;
        private Button btnBackup;
        private Button btnClearAll;
        private Button btnClearSelected;
        private Button btnanalysisOfResults;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel lblStatus;
        private TableLayoutPanel tableLayoutPanel1;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private Button btn1000Experiments;
    }
}