using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using ImageMagick;

using System.Drawing.Imaging;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace yuogphotoconvert
{
    public partial class Form1 : Form
    {
        private string ConvertKlasor; // Dönüþtürülecek klasörün yolu
        private string HedefKlasor; // Hedef klasörün yolu

        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                DialogResult result = folderBrowserDialog.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {
                    ConvertKlasor = folderBrowserDialog.SelectedPath; // Seçilen klasör yolunu deðiþkene ata
                    textBox2.Text = ConvertKlasor; // TextBox'ta seçilen yolu göster
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                DialogResult result = folderBrowserDialog.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {
                    HedefKlasor = folderBrowserDialog.SelectedPath; // Seçilen klasör yolunu deðiþkene ata
                    textBox1.Text = HedefKlasor; // TextBox'ta seçilen yolu göster
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(ConvertKlasor) && !string.IsNullOrWhiteSpace(HedefKlasor))
            {
                if (!Directory.Exists(HedefKlasor))
                {
                    Directory.CreateDirectory(HedefKlasor);
                }

                ConvertImagesRecursively(ConvertKlasor, HedefKlasor);

                MessageBox.Show("Webp Formatýna Çevirdi ve Kaydetti!");
            }
            else
            {
                MessageBox.Show("Dönüþtürülecek ve hedef klasörler seçilmedi.");
            }
        }

        private void ConvertImagesRecursively(string sourceFolderPath, string targetFolderPath)
        {
            string[] jpgFiles = Directory.GetFiles(sourceFolderPath, "*.jpg");

            foreach (string sourcePath in jpgFiles)
            {
                string fileName = Path.GetFileName(sourcePath);
                string targetPath = Path.Combine(targetFolderPath, Path.ChangeExtension(fileName, ".webp"));

                using (MagickImage image = new MagickImage(sourcePath))
                {
                    image.Format = MagickFormat.WebP;
                    image.Quality = 75; // Kaliteyi 75 olarak ayarla
                    image.Write(targetPath);
                }
            }

            string[] subdirectories = Directory.GetDirectories(sourceFolderPath);

            foreach (string subdirectoryPath in subdirectories)
            {
                string subdirectoryName = Path.GetFileName(subdirectoryPath);
                string targetSubdirectoryPath = Path.Combine(targetFolderPath, subdirectoryName);

                if (!Directory.Exists(targetSubdirectoryPath))
                {
                    Directory.CreateDirectory(targetSubdirectoryPath);
                }

                ConvertImagesRecursively(subdirectoryPath, targetSubdirectoryPath);
            }
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}

