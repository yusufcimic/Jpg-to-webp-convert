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
        private string ConvertKlasor; // D�n��t�r�lecek klas�r�n yolu
        private string HedefKlasor; // Hedef klas�r�n yolu

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
                    ConvertKlasor = folderBrowserDialog.SelectedPath; // Se�ilen klas�r yolunu de�i�kene ata
                    textBox2.Text = ConvertKlasor; // TextBox'ta se�ilen yolu g�ster
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
                    HedefKlasor = folderBrowserDialog.SelectedPath; // Se�ilen klas�r yolunu de�i�kene ata
                    textBox1.Text = HedefKlasor; // TextBox'ta se�ilen yolu g�ster
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

                MessageBox.Show("Webp Format�na �evirdi ve Kaydetti!");
            }
            else
            {
                MessageBox.Show("D�n��t�r�lecek ve hedef klas�rler se�ilmedi.");
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

