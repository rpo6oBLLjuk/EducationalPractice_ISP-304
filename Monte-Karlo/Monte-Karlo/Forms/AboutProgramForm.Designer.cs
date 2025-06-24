using System;
using System.Windows.Forms;

namespace Monte_Karlo
{
    partial class AboutProgramForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutProgramForm));
            githubLinkLabel = new LinkLabel();
            titleLabel = new Label();
            pictureBox1 = new PictureBox();
            authorLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // githubLinkLabel
            // 
            githubLinkLabel.Font = new Font("Segoe UI", 10F);
            githubLinkLabel.Location = new Point(15, 282);
            githubLinkLabel.Margin = new Padding(2, 0, 2, 0);
            githubLinkLabel.Name = "githubLinkLabel";
            githubLinkLabel.Size = new Size(260, 27);
            githubLinkLabel.TabIndex = 6;
            githubLinkLabel.TabStop = true;
            githubLinkLabel.Text = "Актуальная версия (GitHub)\r\n";
            githubLinkLabel.LinkClicked += githubLinkLabel_LinkClicked;
            // 
            // titleLabel
            // 
            titleLabel.Font = new Font("Segoe UI", 13.2000008F, FontStyle.Bold, GraphicsUnit.Point, 204);
            titleLabel.ForeColor = Color.DarkBlue;
            titleLabel.Location = new Point(294, 13);
            titleLabel.Margin = new Padding(0, 0, 0, 7);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(235, 66);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Вычислитель площади сегмента окружности";
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(15, 13);
            pictureBox1.Margin = new Padding(2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(260, 264);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            // 
            // authorLabel
            // 
            authorLabel.Font = new Font("Segoe UI", 10F);
            authorLabel.Location = new Point(294, 102);
            authorLabel.Margin = new Padding(0, 0, 0, 3);
            authorLabel.Name = "authorLabel";
            authorLabel.Size = new Size(235, 177);
            authorLabel.TabIndex = 1;
            authorLabel.Text = "Автор: Мусатов Даниил Романович\r\nСтудент группы ИСП-304 Университетского колледжа информационных технологий им. Разумовского";
            // 
            // AboutProgramForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(561, 312);
            Controls.Add(authorLabel);
            Controls.Add(githubLinkLabel);
            Controls.Add(titleLabel);
            Controls.Add(pictureBox1);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(0, 0, 0, 10);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AboutProgramForm";
            Padding = new Padding(13);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "О программе";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private LinkLabel githubLinkLabel;
        private Label titleLabel;
        private PictureBox pictureBox1;
        private Label authorLabel;
    }
}