namespace Monte_Karlo
{
    partial class SplashScreenForm
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashScreenForm));
            startButton = new Button();
            timer1 = new System.Windows.Forms.Timer(components);
            pictureBox1 = new PictureBox();
            label1 = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // startButton
            // 
            startButton.BackColor = Color.FromArgb(174, 206, 180);
            startButton.Font = new Font("Segoe UI", 14F);
            startButton.Location = new Point(390, 315);
            startButton.Margin = new Padding(2);
            startButton.Name = "startButton";
            startButton.Size = new Size(320, 56);
            startButton.TabIndex = 5;
            startButton.Text = "Перейти к программе";
            startButton.UseVisualStyleBackColor = false;
            startButton.Click += startButton_Click;
            // 
            // timer1
            // 
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.None;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Margin = new Padding(0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(381, 377);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 13.8F, FontStyle.Underline, GraphicsUnit.Point, 204);
            label1.Location = new Point(390, 9);
            label1.Name = "label1";
            label1.Size = new Size(319, 74);
            label1.TabIndex = 6;
            label1.Text = "Вычислитель площади сегмента окружности";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI", 10F);
            label2.Location = new Point(390, 83);
            label2.Name = "label2";
            label2.Size = new Size(319, 225);
            label2.TabIndex = 7;
            label2.Text = "Вариант 26\r\nСтудент: Мусатов Д.Р.\r\nДанные о варианте:\r\nx0: -3\r\ny0: 1\r\nr: 2\r\nC: -2\r\nНаправление: вертикально\r\n";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // SplashScreenForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(721, 377);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Controls.Add(startButton);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(2);
            Name = "SplashScreenForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Заставка";
            Load += Screensaver_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Button startButton;
        private System.Windows.Forms.Timer timer1;
        private PictureBox pictureBox1;
        private Label label1;
        private Label label2;
    }
}
