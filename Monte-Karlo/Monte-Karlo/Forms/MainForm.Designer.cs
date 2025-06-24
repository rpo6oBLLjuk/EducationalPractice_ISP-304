namespace Monte_Karlo
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            paintPanel = new Panel();
            panel1 = new Panel();
            showMessageCheckBox = new CheckBox();
            scaleLabel = new Label();
            scaleTrackBar = new TrackBar();
            controlPanel = new Panel();
            label1 = new Label();
            pointsCountUpdown = new NumericUpDown();
            pointsCountLabel = new Label();
            monteCarloSquareLabel = new Label();
            realSquareLabel = new Label();
            btnClear = new Button();
            btnGeneratePoints = new Button();
            controlPanelLabel = new Label();
            menuStrip = new MenuStrip();
            programHelpToolStripMenuItem = new ToolStripMenuItem();
            aboutProgramToolStripMenuItem = new ToolStripMenuItem();
            analysisOfResultsToolStripMenuItem = new ToolStripMenuItem();
            ExperementsControl = new ToolStripMenuItem();
            closeProgramToolStripMenuItem = new ToolStripMenuItem();
            paintPanel.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)scaleTrackBar).BeginInit();
            controlPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pointsCountUpdown).BeginInit();
            menuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // paintPanel
            // 
            paintPanel.BorderStyle = BorderStyle.FixedSingle;
            paintPanel.Controls.Add(panel1);
            paintPanel.Controls.Add(controlPanel);
            paintPanel.Dock = DockStyle.Fill;
            paintPanel.Location = new Point(0, 26);
            paintPanel.Name = "paintPanel";
            paintPanel.Size = new Size(950, 677);
            paintPanel.TabIndex = 0;
            paintPanel.Paint += paintPanel_Paint;
            paintPanel.Resize += paintPanel_Resize;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            panel1.BackColor = SystemColors.ControlDarkDark;
            panel1.Controls.Add(showMessageCheckBox);
            panel1.Controls.Add(scaleLabel);
            panel1.Controls.Add(scaleTrackBar);
            panel1.Location = new Point(625, 585);
            panel1.Name = "panel1";
            panel1.Size = new Size(320, 87);
            panel1.TabIndex = 8;
            // 
            // showMessageCheckBox
            // 
            showMessageCheckBox.BackColor = Color.Transparent;
            showMessageCheckBox.CheckAlign = ContentAlignment.MiddleRight;
            showMessageCheckBox.ForeColor = SystemColors.Control;
            showMessageCheckBox.Location = new Point(3, 54);
            showMessageCheckBox.Margin = new Padding(2);
            showMessageCheckBox.Name = "showMessageCheckBox";
            showMessageCheckBox.Size = new Size(314, 27);
            showMessageCheckBox.TabIndex = 6;
            showMessageCheckBox.Text = "Показывать результат вычислений";
            showMessageCheckBox.UseVisualStyleBackColor = false;
            // 
            // scaleLabel
            // 
            scaleLabel.BackColor = Color.Transparent;
            scaleLabel.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            scaleLabel.ForeColor = SystemColors.Control;
            scaleLabel.Location = new Point(3, 0);
            scaleLabel.Name = "scaleLabel";
            scaleLabel.Size = new Size(314, 27);
            scaleLabel.TabIndex = 2;
            scaleLabel.Text = "Масштаб";
            scaleLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // scaleTrackBar
            // 
            scaleTrackBar.AutoSize = false;
            scaleTrackBar.BackColor = SystemColors.ControlDarkDark;
            scaleTrackBar.Location = new Point(0, 23);
            scaleTrackBar.Maximum = 250;
            scaleTrackBar.Minimum = 10;
            scaleTrackBar.Name = "scaleTrackBar";
            scaleTrackBar.Size = new Size(314, 28);
            scaleTrackBar.SmallChange = 5;
            scaleTrackBar.TabIndex = 0;
            scaleTrackBar.TickFrequency = 5;
            scaleTrackBar.TickStyle = TickStyle.None;
            scaleTrackBar.UseWaitCursor = true;
            scaleTrackBar.Value = 15;
            scaleTrackBar.Scroll += scaleTrackbar_Scroll;
            // 
            // controlPanel
            // 
            controlPanel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            controlPanel.BackColor = SystemColors.ControlDarkDark;
            controlPanel.Controls.Add(label1);
            controlPanel.Controls.Add(pointsCountUpdown);
            controlPanel.Controls.Add(pointsCountLabel);
            controlPanel.Controls.Add(monteCarloSquareLabel);
            controlPanel.Controls.Add(realSquareLabel);
            controlPanel.Controls.Add(btnClear);
            controlPanel.Controls.Add(btnGeneratePoints);
            controlPanel.Controls.Add(controlPanelLabel);
            controlPanel.Location = new Point(625, 3);
            controlPanel.Name = "controlPanel";
            controlPanel.Size = new Size(320, 261);
            controlPanel.TabIndex = 1;
            // 
            // label1
            // 
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label1.ForeColor = SystemColors.Control;
            label1.Location = new Point(20, 166);
            label1.Name = "label1";
            label1.Size = new Size(280, 27);
            label1.TabIndex = 7;
            label1.Text = "Площадь секции";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pointsCountUpdown
            // 
            pointsCountUpdown.BackColor = SystemColors.ControlDarkDark;
            pointsCountUpdown.ForeColor = SystemColors.Control;
            pointsCountUpdown.Location = new Point(177, 61);
            pointsCountUpdown.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            pointsCountUpdown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            pointsCountUpdown.Name = "pointsCountUpdown";
            pointsCountUpdown.Size = new Size(123, 27);
            pointsCountUpdown.TabIndex = 3;
            pointsCountUpdown.TextAlign = HorizontalAlignment.Right;
            pointsCountUpdown.ThousandsSeparator = true;
            pointsCountUpdown.Value = new decimal(new int[] { 1, 0, 0, 0 });
            pointsCountUpdown.ValueChanged += pointsCountUpdown_ValueChanged;
            // 
            // pointsCountLabel
            // 
            pointsCountLabel.BackColor = Color.Transparent;
            pointsCountLabel.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            pointsCountLabel.ForeColor = SystemColors.Control;
            pointsCountLabel.Location = new Point(20, 60);
            pointsCountLabel.Name = "pointsCountLabel";
            pointsCountLabel.Size = new Size(137, 27);
            pointsCountLabel.TabIndex = 2;
            pointsCountLabel.Text = "Количество точек:";
            pointsCountLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // monteCarloSquareLabel
            // 
            monteCarloSquareLabel.BackColor = Color.Transparent;
            monteCarloSquareLabel.Font = new Font("Segoe UI", 9F);
            monteCarloSquareLabel.ForeColor = SystemColors.Control;
            monteCarloSquareLabel.Location = new Point(20, 225);
            monteCarloSquareLabel.Name = "monteCarloSquareLabel";
            monteCarloSquareLabel.Size = new Size(280, 27);
            monteCarloSquareLabel.TabIndex = 3;
            monteCarloSquareLabel.Text = "Методом Монте-Карло:";
            // 
            // realSquareLabel
            // 
            realSquareLabel.BackColor = Color.Transparent;
            realSquareLabel.Font = new Font("Segoe UI", 9F);
            realSquareLabel.ForeColor = SystemColors.Control;
            realSquareLabel.Location = new Point(20, 197);
            realSquareLabel.Name = "realSquareLabel";
            realSquareLabel.Size = new Size(280, 27);
            realSquareLabel.TabIndex = 2;
            realSquareLabel.Text = "Аналитически:";
            // 
            // btnClear
            // 
            btnClear.BackColor = Color.Transparent;
            btnClear.FlatAppearance.MouseDownBackColor = Color.Black;
            btnClear.FlatAppearance.MouseOverBackColor = Color.FromArgb(64, 64, 64);
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.ForeColor = SystemColors.Control;
            btnClear.Location = new Point(162, 100);
            btnClear.Margin = new Padding(2);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(138, 55);
            btnClear.TabIndex = 1;
            btnClear.Text = "Очистить\r\nточки";
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += btnClear_Click;
            // 
            // btnGeneratePoints
            // 
            btnGeneratePoints.BackColor = Color.Transparent;
            btnGeneratePoints.FlatAppearance.MouseDownBackColor = Color.Black;
            btnGeneratePoints.FlatAppearance.MouseOverBackColor = Color.FromArgb(64, 64, 64);
            btnGeneratePoints.FlatStyle = FlatStyle.Flat;
            btnGeneratePoints.ForeColor = SystemColors.Control;
            btnGeneratePoints.Location = new Point(20, 100);
            btnGeneratePoints.Margin = new Padding(2);
            btnGeneratePoints.Name = "btnGeneratePoints";
            btnGeneratePoints.Size = new Size(138, 55);
            btnGeneratePoints.TabIndex = 0;
            btnGeneratePoints.Text = "Генерировать\r\nточки";
            btnGeneratePoints.UseVisualStyleBackColor = false;
            btnGeneratePoints.Click += btnGeneratePoints_Click;
            // 
            // controlPanelLabel
            // 
            controlPanelLabel.BackColor = Color.Transparent;
            controlPanelLabel.Font = new Font("Segoe UI", 12F, FontStyle.Underline, GraphicsUnit.Point, 204);
            controlPanelLabel.ForeColor = SystemColors.Control;
            controlPanelLabel.Location = new Point(10, 9);
            controlPanelLabel.Name = "controlPanelLabel";
            controlPanelLabel.Size = new Size(300, 37);
            controlPanelLabel.TabIndex = 0;
            controlPanelLabel.Text = "Панель управления";
            controlPanelLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // menuStrip
            // 
            menuStrip.ImageScalingSize = new Size(28, 28);
            menuStrip.Items.AddRange(new ToolStripItem[] { programHelpToolStripMenuItem, aboutProgramToolStripMenuItem, analysisOfResultsToolStripMenuItem, ExperementsControl, closeProgramToolStripMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Padding = new Padding(4, 1, 0, 1);
            menuStrip.Size = new Size(950, 26);
            menuStrip.TabIndex = 4;
            menuStrip.Text = "menuStrip1";
            // 
            // programHelpToolStripMenuItem
            // 
            programHelpToolStripMenuItem.Name = "programHelpToolStripMenuItem";
            programHelpToolStripMenuItem.Size = new Size(81, 24);
            programHelpToolStripMenuItem.Text = "Справка";
            programHelpToolStripMenuItem.Click += programHelpToolStripMenuItem_Click;
            // 
            // aboutProgramToolStripMenuItem
            // 
            aboutProgramToolStripMenuItem.Name = "aboutProgramToolStripMenuItem";
            aboutProgramToolStripMenuItem.Size = new Size(118, 24);
            aboutProgramToolStripMenuItem.Text = "О программе";
            aboutProgramToolStripMenuItem.Click += aboutProgramToolStripMenuItem_Click;
            // 
            // analysisOfResultsToolStripMenuItem
            // 
            analysisOfResultsToolStripMenuItem.Name = "analysisOfResultsToolStripMenuItem";
            analysisOfResultsToolStripMenuItem.Size = new Size(162, 24);
            analysisOfResultsToolStripMenuItem.Text = "Анализ результатов";
            analysisOfResultsToolStripMenuItem.Click += analysisOfResultsToolStripMenuItem_Click;
            // 
            // ExperementsControl
            // 
            ExperementsControl.Name = "ExperementsControl";
            ExperementsControl.Size = new Size(229, 24);
            ExperementsControl.Text = "Управление эксперементами";
            ExperementsControl.Click += ExperementsControlToolStripMenuItem_Click;
            // 
            // closeProgramToolStripMenuItem
            // 
            closeProgramToolStripMenuItem.Name = "closeProgramToolStripMenuItem";
            closeProgramToolStripMenuItem.Size = new Size(173, 24);
            closeProgramToolStripMenuItem.Text = "Закрыть приложение";
            closeProgramToolStripMenuItem.Click += closeProgramToolStripMenuItem_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(950, 703);
            Controls.Add(paintPanel);
            Controls.Add(menuStrip);
            DoubleBuffered = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Основное окно";
            FormClosed += MainForm_FormClosed;
            paintPanel.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)scaleTrackBar).EndInit();
            controlPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pointsCountUpdown).EndInit();
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel paintPanel;
        private Panel controlPanel;
        private Label controlPanelLabel;
        private Label pointsCountLabel;
        public TrackBar scaleTrackBar;
        private Label scaleLabel;
        private NumericUpDown pointsCountUpdown;
        private Label realSquareLabel;
        private Label monteCarloSquareLabel;
        private MenuStrip menuStrip;
        private ToolStripMenuItem programHelpToolStripMenuItem;
        private ToolStripMenuItem aboutProgramToolStripMenuItem;
        private ToolStripMenuItem closeProgramToolStripMenuItem;
        private CheckBox showMessageCheckBox;
        private ToolStripMenuItem analysisOfResultsToolStripMenuItem;
        private Button btnGeneratePoints;
        private Button btnClear;
        private ToolStripMenuItem ExperementsControl;
        private Label label1;
        private Panel panel1;
    }
}