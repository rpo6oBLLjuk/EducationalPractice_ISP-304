using System.Diagnostics;

namespace Monte_Karlo
{
    public partial class AboutProgramForm : Form
    {
        public AboutProgramForm()
        {
            InitializeComponent();
        }

        private void githubLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "https://github.com/rpo6oBLLjuk/EducationalPractice_ISP-304",
                    UseShellExecute = true
                });

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось открыть ссылку: {ex.Message}",
                                "Ошибка",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }
    }
}
