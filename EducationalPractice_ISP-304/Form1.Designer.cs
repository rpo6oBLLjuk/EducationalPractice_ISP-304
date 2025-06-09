namespace EducationalPractice_ISP_304
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            panel2 = new Panel();
            panel6 = new Panel();
            cTrackbar = new TrackBar();
            cLabel = new Label();
            GeneratePointsButton = new Button();
            panel5 = new Panel();
            sizeTrackbar = new TrackBar();
            sizeLabel = new Label();
            panel4 = new Panel();
            pointsCountUpdown = new NumericUpDown();
            pointsCountLabel = new Label();
            panel3 = new Panel();
            radiusSlider = new TrackBar();
            radiusLabel = new Label();
            label1 = new Label();
            panel2.SuspendLayout();
            panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)cTrackbar).BeginInit();
            panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)sizeTrackbar).BeginInit();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pointsCountUpdown).BeginInit();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)radiusSlider).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Location = new Point(290, 10);
            panel1.Name = "panel1";
            panel1.Size = new Size(980, 935);
            panel1.TabIndex = 0;
            panel1.Paint += panel1_Paint;
            // 
            // panel2
            // 
            panel2.BorderStyle = BorderStyle.Fixed3D;
            panel2.Controls.Add(panel6);
            panel2.Controls.Add(GeneratePointsButton);
            panel2.Controls.Add(panel5);
            panel2.Controls.Add(panel4);
            panel2.Controls.Add(panel3);
            panel2.Controls.Add(label1);
            panel2.Location = new Point(21, 298);
            panel2.Name = "panel2";
            panel2.Size = new Size(250, 550);
            panel2.TabIndex = 1;
            // 
            // panel6
            // 
            panel6.Controls.Add(cTrackbar);
            panel6.Controls.Add(cLabel);
            panel6.Location = new Point(6, 245);
            panel6.Name = "panel6";
            panel6.Size = new Size(240, 96);
            panel6.TabIndex = 3;
            // 
            // cTrackbar
            // 
            cTrackbar.Location = new Point(3, 34);
            cTrackbar.Maximum = 21;
            cTrackbar.Minimum = -21;
            cTrackbar.Name = "cTrackbar";
            cTrackbar.Size = new Size(234, 56);
            cTrackbar.TabIndex = 0;
            cTrackbar.Value = 1;
            cTrackbar.ValueChanged += cTrackbar_ValueChanged;
            // 
            // cLabel
            // 
            cLabel.BackColor = Color.Transparent;
            cLabel.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            cLabel.Location = new Point(0, 0);
            cLabel.Name = "cLabel";
            cLabel.Size = new Size(240, 31);
            cLabel.TabIndex = 2;
            cLabel.Text = "C";
            cLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // GeneratePointsButton
            // 
            GeneratePointsButton.Location = new Point(6, 492);
            GeneratePointsButton.Name = "GeneratePointsButton";
            GeneratePointsButton.Size = new Size(234, 51);
            GeneratePointsButton.TabIndex = 4;
            GeneratePointsButton.Text = "Generate Points";
            GeneratePointsButton.UseVisualStyleBackColor = true;
            GeneratePointsButton.Click += GeneratePointsButton_Click;
            // 
            // panel5
            // 
            panel5.Controls.Add(sizeTrackbar);
            panel5.Controls.Add(sizeLabel);
            panel5.Location = new Point(6, 390);
            panel5.Name = "panel5";
            panel5.Size = new Size(240, 96);
            panel5.TabIndex = 3;
            // 
            // sizeTrackbar
            // 
            sizeTrackbar.AutoSize = false;
            sizeTrackbar.Location = new Point(3, 34);
            sizeTrackbar.Maximum = 120;
            sizeTrackbar.Minimum = 10;
            sizeTrackbar.Name = "sizeTrackbar";
            sizeTrackbar.Size = new Size(234, 56);
            sizeTrackbar.SmallChange = 5;
            sizeTrackbar.TabIndex = 0;
            sizeTrackbar.TickFrequency = 5;
            sizeTrackbar.UseWaitCursor = true;
            sizeTrackbar.Value = 15;
            sizeTrackbar.Scroll += sizeTrackbar_Scroll;
            // 
            // sizeLabel
            // 
            sizeLabel.BackColor = Color.Transparent;
            sizeLabel.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            sizeLabel.Location = new Point(0, 0);
            sizeLabel.Name = "sizeLabel";
            sizeLabel.Size = new Size(240, 31);
            sizeLabel.TabIndex = 2;
            sizeLabel.Text = "Size";
            sizeLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel4
            // 
            panel4.Controls.Add(pointsCountUpdown);
            panel4.Controls.Add(pointsCountLabel);
            panel4.Location = new Point(3, 173);
            panel4.Name = "panel4";
            panel4.Size = new Size(240, 66);
            panel4.TabIndex = 3;
            // 
            // pointsCountUpdown
            // 
            pointsCountUpdown.Location = new Point(3, 34);
            pointsCountUpdown.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            pointsCountUpdown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            pointsCountUpdown.Name = "pointsCountUpdown";
            pointsCountUpdown.Size = new Size(234, 27);
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
            pointsCountLabel.Location = new Point(0, 0);
            pointsCountLabel.Name = "pointsCountLabel";
            pointsCountLabel.Size = new Size(240, 31);
            pointsCountLabel.TabIndex = 2;
            pointsCountLabel.Text = "Points Count";
            pointsCountLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            panel3.Controls.Add(radiusSlider);
            panel3.Controls.Add(radiusLabel);
            panel3.Location = new Point(3, 71);
            panel3.Name = "panel3";
            panel3.Size = new Size(240, 96);
            panel3.TabIndex = 1;
            // 
            // radiusSlider
            // 
            radiusSlider.Location = new Point(3, 34);
            radiusSlider.Maximum = 20;
            radiusSlider.Minimum = 1;
            radiusSlider.Name = "radiusSlider";
            radiusSlider.Size = new Size(234, 56);
            radiusSlider.TabIndex = 0;
            radiusSlider.Value = 1;
            radiusSlider.Scroll += radiusSlider_Scroll;
            // 
            // radiusLabel
            // 
            radiusLabel.BackColor = Color.Transparent;
            radiusLabel.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            radiusLabel.Location = new Point(0, 0);
            radiusLabel.Name = "radiusLabel";
            radiusLabel.Size = new Size(240, 31);
            radiusLabel.TabIndex = 2;
            radiusLabel.Text = "Radius";
            radiusLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Underline, GraphicsUnit.Point, 204);
            label1.Location = new Point(-2, 0);
            label1.Name = "label1";
            label1.Size = new Size(250, 44);
            label1.TabIndex = 0;
            label1.Text = "Control panel";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1282, 953);
            Controls.Add(panel2);
            Controls.Add(panel1);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            panel2.ResumeLayout(false);
            panel6.ResumeLayout(false);
            panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)cTrackbar).EndInit();
            panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)sizeTrackbar).EndInit();
            panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pointsCountUpdown).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)radiusSlider).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Label label1;
        private Panel panel3;
        private Label radiusLabel;
        public TrackBar radiusSlider;
        private Panel panel4;
        private Label pointsCountLabel;
        private Panel panel5;
        public TrackBar sizeTrackbar;
        private Label sizeLabel;
        private NumericUpDown pointsCountUpdown;
        private Button GeneratePointsButton;
        private Panel panel6;
        public TrackBar cTrackbar;
        private Label cLabel;
    }
}
