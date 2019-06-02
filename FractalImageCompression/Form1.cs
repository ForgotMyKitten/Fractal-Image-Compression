using System;
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

namespace FractalImageCompression
{
    public partial class Form1 : Form
    {
        private const int numberOfPictures = 10;
        private PictureBox[] pictures = new PictureBox[numberOfPictures];
        private Bitmap image;
        private string fileName;
        private CancellationTokenSource tokenSource;

        public Form1()
        {
            InitializeComponent();
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            comboBoxDomainBlockSize.SelectedIndex = 1;
            comboBoxRankBlockSize.SelectedIndex = 1;

            for(int i = 0; i < numberOfPictures; i++)
            {
                pictures[i] = CreatePictureBox(i);
            }
            comboBoxRankBlockSize.Enabled = false;
            comboBoxDomainBlockSize.Enabled = false;
        }

        public PictureBox CreatePictureBox(int i)
        {
            int width = flowLayoutPanelStages.Size.Width;
            int size = width / 3;

            var pb = new PictureBox();
            pb.Location = new Point(i * size, 20);
            pb.Size = new Size(size - 20, size - 20);
            pb.BackColor = SystemColors.ButtonHighlight;
            pb.SizeMode = PictureBoxSizeMode.Zoom;
            flowLayoutPanelStages.Controls.Add(pb);

            return pb;
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            buttonCompress.Enabled = false;
            buttonDecompress.Enabled = false;
            textBoxPath.Text = "";
            comboBoxRankBlockSize.Enabled = false;
            comboBoxDomainBlockSize.Enabled = false;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    fileName = openFileDialog1.FileName;
                    Data.Path = textBoxPath.Text = fileName;
                    image = new Bitmap(fileName);
                    pictureBoxOriginal.Image = image;
                    buttonCompress.Enabled = true;
                    comboBoxRankBlockSize.Enabled = true;
                    comboBoxDomainBlockSize.Enabled = true;
                }
                catch (Exception ex)
                {
                    if (ex is ArgumentException)
                    {
                        buttonDecompress.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Ошибка чтения");
                    }
                }
            }
        }

        private async void buttonCompress_Click(object sender, EventArgs e)
        {
            Data.RankBlockSize = Convert.ToInt32(comboBoxRankBlockSize.SelectedItem);
            Data.DomainBlockSize = Convert.ToInt32(comboBoxDomainBlockSize.SelectedItem);
            if (!Data.Validate())
            {
                MessageBox.Show("Размер рангового блока должен быть меньше доменного");
                return;
            }

            Lock(true);
            Coder coder = new Coder();
            tokenSource = new CancellationTokenSource();
            var progress = new Progress<int>(s => progressBarStatus.Value = s);

            await Task.Factory.StartNew(() => coder.Compress(image, progress, tokenSource.Token),
                                                     tokenSource.Token);
            Lock(false);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            tokenSource.Cancel();
        }

        public void Lock(bool _lock)
        {
            buttonCompress.Enabled = !_lock;
            comboBoxRankBlockSize.Enabled = !_lock;
            comboBoxDomainBlockSize.Enabled = !_lock;
            buttonOpen.Enabled = !_lock;
            checkBoxFastCompression.Enabled = !_lock;
            checkBoxRandomImage.Enabled = !_lock;
            buttonCancel.Enabled = _lock;
        }

        private void checkBoxFastCompression_CheckedChanged(object sender, EventArgs e)
        {
            Data.FastCompression = checkBoxFastCompression.Checked;
        }

        private void buttonDecompress_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
                Coder coder = new Coder();
                Bitmap[] decompressedImages = coder.Decompress(fileName, numberOfPictures, checkBoxRandomImage.Checked);
                for (int i = 0; i < numberOfPictures; i++)
                {
                    pictures[i].Image = decompressedImages[i];
                }
                pictureBoxRestored.Image = decompressedImages[numberOfPictures - 1];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Clear()
        {
            for(int i = 0; i < numberOfPictures; i++)
            {
                pictures[i].Image = null;
            }
            pictureBoxRestored.Image = null;
        }

    }
}
