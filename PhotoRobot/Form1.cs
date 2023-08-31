using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoRobot
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static StringBuilder output = new StringBuilder();

        private void пускToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap src;
            output = new StringBuilder();

            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return;

            var filePath = openFileDialog1.FileName;

            src = new Bitmap(filePath);

            for (int h = 0; h < src.Height; h++)
            {
                for (int w = 0; w < src.Width; w++)
                {
                    var pixel = src.GetPixel(w, h);
                    var brightness = pixel.GetBrightness();

                    if (brightness > 0.5)
                    {
                        output.Append("@");
                    }
                    else
                    {
                        output.Append(" ");
                    }
                }
                output.AppendLine();
            }

            richTextBox1.Text = output.ToString();
        }
        private async void видеоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var fullPath = "D:\\Desktop\\Видео";

                DirectoryInfo dir = new DirectoryInfo(fullPath);
                foreach (FileInfo filePath in dir.GetFiles())
                {
                    await Task.Run(() =>
                        {
                            draw(filePath);
                        });
                }
            }
            catch
            {
                MessageBox.Show("Дождитесь окончания!");
            }
        }
        void draw(object x)
        {
            output = new StringBuilder();
            FileInfo filePath = (FileInfo)(x);

            Bitmap src = new Bitmap(filePath.FullName);

            for (int h = 0; h < src.Height; h++)
            {
                for (int w = 0; w < src.Width; w++)
                {
                    var pixel = src.GetPixel(w, h);
                    var brightness = pixel.GetBrightness();

                    if (brightness > 0.5)
                    {
                        output.Append("@");
                    }
                    else
                    {
                        output.Append(" ");
                    }
                }
                output.AppendLine();
            }
            try
            {
                this.Invoke(new Action(() =>
                {
                    try
                    {
                        richTextBox1.Text = output.ToString();
                    }
                    catch
                    {
                        MessageBox.Show("Дождитесь окончания!");
                    }
                }));
            }
            catch
            {
                MessageBox.Show("Дождитесь окончания!");
            }
        }
    }
}
